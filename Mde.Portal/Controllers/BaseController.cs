using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Threading;
using Mde.Portal.Common;
using Mde.Model.ModelView;
using Mde.IBLL;
using Mde.BLL;
using Mde.ModelView.ModelView;
using System.Web.Caching;
using System.IO;
using System.Net;
using Mde.Model.DataModel;

namespace Mde.Portal.Controllers
{
    public class BaseController : Controller
    {
        private IMDOrganizationService _mdOrgService = new MD_OrganizationService();
        private IMD_DictionaryService _mdDictionary = new MD_DictionaryService();
        private ISys_RoleService _sysRoleService = new Sys_RoleService();
        private IDSD_M_SystemConfigService _sysConfigService = new DSD_M_SystemConfigService();
        private IMD_DictionaryService _mdDictionService = new MD_DictionaryService();

        //定义一个基类的UserInfo对象
        public UserInfo CurrentUserInfo { get; set; }

        public AjaxResult AjaxResult { get; set; }

        public object cache { get; set; }
        
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            AjaxResult = new Common.AjaxResult();
            CurrentUserInfo = Session["UserInfo"] as UserInfo;

            var controller = filterContext.RouteData.Values["controller"].ToString(); 
            var action = filterContext.RouteData.Values["action"].ToString();

            //Flash上传文件不过滤 兼容非IE浏览器
            if (controller == "Upload" && action == "UploadExcel")
            {
                return;
            }
            cache = CacheHelper.GetCache("Role");

            //检验用户是否已经登录，如果登录则不执行，否则则执行下面的跳转代码
            if (CurrentUserInfo == null)
                Response.Redirect("~/Login");

        }

        /// <summary>
        /// 根据moduleId从cache中读取页面按钮权限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRoleFromCache(FormCollection value)
        {
            string moduleId = value["ModuleId"];
            List<ViewButtonRoleData> dictionary = (List<ViewButtonRoleData>)cache;
            dictionary = dictionary.Where(p => p.ModuleID == moduleId).ToList();
            
            return Json(dictionary, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Sys_OrgUnit下拉框数据取得
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSysOrgUnitComboboxData()
        {
            string hasEmptyRow = Request.QueryString["hasEmptyRow"];
            //string DomainName = Request.QueryString["DomainName"];
            List<ViewComboboxIntData> data = _mdOrgService.GetComboboxData();

            if (!string.IsNullOrEmpty(hasEmptyRow))
            {
                ViewComboboxIntData EmptyRow = new ViewComboboxIntData();
                EmptyRow.value = 0;
                EmptyRow.name = "All Org Unit";
                data.Insert(0, EmptyRow);
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取Dictionary表数据绑定下拉框
        /// </summary>
        /// <param name="Category"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDictionaryComboxData()
        {
            string Category = Request.QueryString["Category"];
            string Code = Request.QueryString["Code"] == null ? "" : Request.QueryString["Code"].ToString();
            bool needAll = Request.QueryString["NeedAll"] == null ? false : bool.Parse(Request.QueryString["NeedAll"].ToString());
            List<ViewComboboxStringData> data = _mdDictionary.GetDictionaryData(Category, Code);
            ViewComboboxStringData EmptyRow = new ViewComboboxStringData();
            if (needAll)
            {
                EmptyRow.value = "";
                EmptyRow.name = "All";
                data.Insert(0, EmptyRow);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Sys_Role取得下拉框数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSysRoleComboboxData()
        {
            List<ViewComboboxIntData> data = _sysRoleService.GetComboboxData();
            ViewComboboxIntData EmptyRow = new ViewComboboxIntData();
            EmptyRow.value = 0;
            EmptyRow.name = "All";
            data.Insert(0, EmptyRow);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Category取得下拉框数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCategoryComboxData()
        {
            List<ViewComboboxStringData> data = _sysConfigService.GetCategoryComboxData();

            var newCategory = "";
            if (Request.QueryString["NewCategory"] != null)
            {
                newCategory = Request.QueryString["NewCategory"].ToString();
                ViewComboboxStringData AddRow = new ViewComboboxStringData();
                AddRow.value = newCategory;
                AddRow.name = newCategory;
                data.Insert(data.Count(), AddRow);
            }
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
                EmptyRow.name = "<font color='red'>New Category</font>";
                data.Insert(0, EmptyRow);
            }
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult GetKeyNameComboxData()
        {
            string category = Request.QueryString["Category"].ToString();
            bool needAll;
            bool.TryParse(Request.QueryString["NeedAll"], out needAll);
            bool needAdd;
            bool.TryParse(Request.QueryString["NeedAdd"], out needAdd);

            List<ViewComboboxStringData> data = _sysConfigService.GetKeyNameComboxData(category);
            if (Request.QueryString["NewKeyName"] != null)
            {

                ViewComboboxStringData newRow = new ViewComboboxStringData();
                newRow.value = Request.QueryString["NewKeyName"];
                newRow.name = Request.QueryString["NewKeyName"];
                data.Insert(data.Count(), newRow);
            }

            if (needAll)
            {
                ViewComboboxStringData EmptyRow = new ViewComboboxStringData();
                EmptyRow.value = "";
                EmptyRow.name = "All";
                data.Insert(0, EmptyRow);
            }

            if (needAdd)
            {
                ViewComboboxStringData EmptyRow = new ViewComboboxStringData();
                EmptyRow.value = "";
                EmptyRow.name = "<font color='red'>New Code</font>";
                data.Insert(0, EmptyRow);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetDisctionaryCategoryComboxData()
        {
            List<ViewComboboxStringData> data = _mdDictionService.GetDictionaryCategoryList();
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
                EmptyRow.name = "<font color='red'>New Category</font>";
                data.Insert(0, EmptyRow);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //根据照片名称到服务器读取照片
        [HttpGet]
        public FileResult GetImage()
        {
            var photoName = Request.QueryString["PhotoName"].ToString();
            string image = getPhoto(photoName);
            MemoryStream ms = new MemoryStream(Convert.FromBase64String(image.Replace("\"", "").Replace("\\", "")));
            return File(ms.ToArray(), "image/jpg");
        }

        private string getPhoto(string photoName)
        {
            try
            {
                if (string.IsNullOrEmpty(photoName))
                {
                    return null;
                }
                MD_Dictionary data = _mdDictionary.CheckData("FileServer", "");
                if (data == null)
                    return null;

                string fileServer = data.Value;

                var webClient = new WebClient();
                string path = fileServer + "/AndroidFileService.svc/GetFileByName/";
                return webClient.UploadString(path + photoName, "POST", string.Empty);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
