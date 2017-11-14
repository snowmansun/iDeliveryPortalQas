using Mde.BLL;
using Mde.Common;
using Mde.IBLL;
using Mde.ModelView.ModelView;
using Mde.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mde.Model.DataModel;
using Mde.Model.ModelView;
using Newtonsoft.Json;

namespace Mde.Portal.Controllers
{
    public class ConfigurationController : BaseController
    {
        private IDSD_M_SystemConfigService _sysConfigService = new DSD_M_SystemConfigService();
        private IMDOrganizationService _mdOrgService = new MD_OrganizationService();
        private OperationResult result = new OperationResult();
        private IMD_DictionaryService _mdDictionService = new MD_DictionaryService();

        #region Dictionary

        public ActionResult Dictionary()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetDictionaryList(FormCollection value)
        {
            ParameterQuery param = SetPageSize();
            var category = value["Category"].ToString();
            var description = value["Description"].ToString();
            string status = value["Status"].ToString();

            List<MD_Dictionary> LstData = _mdDictionService.GetDictionaryList(category, description, status, param);
            var data = from a in LstData
                       select new
                       {
                           a.ID,
                           a.Category,
                           a.Description,
                           a.Value,
                           a.Valid,
                           a.Sequence
                       };
            var postJson = new { total = data.Count(), rows = data };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetDictionCodeComboxData(FormCollection value)
        {
            var Category = "";
            if (Request.QueryString["Category"] != null)
                Category = Request.QueryString["Category"].ToString();
            List<ViewComboboxStringData> data = _mdDictionService.GetDictionaryData(Category,"");
            if (Request.QueryString["NeedAll"] != null && bool.Parse(Request.QueryString["NeedAll"]))
            {
                ViewComboboxStringData EmptyRow = new ViewComboboxStringData();
                EmptyRow.value = "";
                EmptyRow.name = "All";
                data.Insert(0, EmptyRow);
            }
            if (Request.QueryString["NeedAdd"] != null && bool.Parse(Request.QueryString["NeedAdd"]))
            { 
                ViewComboboxStringData EmptyRow = new ViewComboboxStringData();
                EmptyRow.value = "";
                EmptyRow.name = "<font color='red'>New Code</font>";
                data.Insert(data.Count(), EmptyRow);
            }
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveDictionary(FormCollection value)
        {
            string Category = value["Category"].ToString();
            string Value = value["Value"];
            string Description = value["Description"];
            bool Valid;
            bool.TryParse(value["Valid"], out Valid);
            int Id;
            int.TryParse(value["ID"], out Id);

            try
            {
                MD_Dictionary data;
                if (Id == 0)
                {
                    data = _mdDictionService.CheckData(Category, Value);
                    if (data != null)
                        return Json(new { result = "failed", msg = Resources.Message.msg_ExistSameCode }, JsonRequestBehavior.AllowGet);

                    data = new MD_Dictionary();
                    data.Category = Category;
                    data.Value = Value;
                    data.Valid = Valid;
                    data.Description = Description;
                    data.Sequence = _mdDictionService.GetMaxSequence(Category);
                    data.CreateTime = DateTime.Now;
                    data.CreateUser = CurrentUserInfo.LoginName;
                    data.LastUpdateTime = DateTime.Now;
                    data.LastUpdateUser = CurrentUserInfo.LoginName;

                    _mdDictionService.AddEntities(data);
                }
                else
                {
                    data = _mdDictionService.GetDataById(Id);
                    data.Description = Description;
                    data.Valid = Valid;
                    data.LastUpdateTime = DateTime.Now;
                    data.LastUpdateUser = CurrentUserInfo.LoginName;
                    if (!Valid)
                        data.Sequence = 0;

                    _mdDictionService.UpdateEntities(data);
                }
                result.ResultType = OperationResultType.succeed;
            }
            catch (Exception ex)
            {
                result.ResultType = OperationResultType.failed;
                result.Message = ex.Message;
            }
            var postJson = new { result = result.ResultType.ToString(), msg = result.Message };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditData(FormCollection value)
        {
            try
            {
                string jsonData = Request.Form[0].ToString().Replace("\"Valid\":\"1\"", "\"Valid\":true").Replace("\"Valid\":\"0\"", "\"Valid\":false");

                //后台拿到字符串时直接反序列化。根据需要自己处理
                var list = JsonConvert.DeserializeObject<List<MD_Dictionary>>(jsonData);
                if(list.Count()>0)
                    _mdDictionService.EditData(list, CurrentUserInfo.LoginName);

                result.ResultType = OperationResultType.succeed;
            }
            catch (Exception ex)
            {
                result.ResultType = OperationResultType.failed;
                result.Message = ex.Message;
            }
            var postJson = new { result = result.ResultType.ToString(), msg = result.Message };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangeSequence(FormCollection value)
        {
            try
            {
                string jsonData = Request.Form[0].ToString().Replace("\"Valid\":\"1\"", "\"Valid\":true").Replace("\"Valid\":\"0\"", "\"Valid\":false");

                //后台拿到字符串时直接反序列化。根据需要自己处理
                List<MD_Dictionary> listData = JsonConvert.DeserializeObject<List<MD_Dictionary>>(jsonData);
                if (listData.Count() > 0)
                {
                    _mdDictionService.ChangeSequence(listData, CurrentUserInfo.LoginName);
                }
                result.ResultType = OperationResultType.succeed;
            }
            catch (Exception ex)
            {
                result.ResultType = OperationResultType.failed;
                result.Message = ex.Message;
            }
            var postJson = new { result = result.ResultType.ToString(), msg = result.Message };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Configuration
        //
        // GET: /Configuration/

        public ActionResult Configuration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetSystemConfigData(FormCollection value)
        {
            var Category = value["Category"].ToString();
            var KeyName = value["KeyName"].ToString();

            List<ViewSystemConfigData> data = _sysConfigService.GetSystemConfigData(Category, KeyName);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 获取Org Tree Data
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ActionResult GetOrganizationTreeData(FormCollection value)
        {
            string orgIDs = null;
            if (Request.QueryString["OrgIDs"] != null && Request.QueryString["OrgIDs"].ToString() != "-1")
            {
                orgIDs = Request.QueryString["OrgIDs"].ToString();
            }
            List<object> data = _mdOrgService.GetOrganizationTreeData(0, orgIDs);
            string jsonstring = MyJson.ConvertToJson(data).Replace("_ischecked", "checked");
            jsonstring = jsonstring.Replace("[[", "[").Replace("]]", "]");
            return Content(jsonstring);
            //return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveConfigData(FormCollection value)
        {
            int SystemConfigID;
            int.TryParse(value["SystemConfigID"], out SystemConfigID);
            var OrgIDs = value["OrgIDs"].ToString();
            var Category = value["Category"].ToString();
            var KeyName = value["KeyName"].ToString();
            var Description = value["Description"];
            var KeyValue = value["KeyValue"].ToString();
            var OldOrgIDs = value["OldOrgIDs"].ToString();
            var UserCode = CurrentUserInfo.UserName;

            //try
            //{
                _sysConfigService.SaveConfigData(SystemConfigID, Category, KeyName, Description, KeyValue, OrgIDs, OldOrgIDs, UserCode);
               
                result.ResultType = OperationResultType.succeed;
            //}
            //catch (Exception ex)
            //{
            //    result.ResultType = OperationResultType.failed;
            //    result.Message = ex.Message;
            //}
            var postJson = new { result = result.ResultType.ToString(), msg = result.Message };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region "pageSize"
        private ParameterQuery SetPageSize()
        {
            int pageIndex, pageSize;
            int.TryParse(Request["page"], out pageIndex);
            int.TryParse(Request["rows"], out pageSize);

            ParameterQuery param = new ParameterQuery();
            param.pageIndex = pageIndex;
            param.pageSize = pageSize;

            return param;
        }
        #endregion
    }
}
