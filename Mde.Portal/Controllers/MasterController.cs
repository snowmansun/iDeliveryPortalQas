using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Globalization;
using Mde.Portal.Common;
using Mde.IBLL;
using Mde.BLL;
using Mde.Model.DataModel;
using Mde.Common;
using Mde.ModelView.ModelView;
using Mde.Portal.Model;
using Newtonsoft.Json;
using Mde.Model.ModelView;

namespace Mde.Portal.Controllers
{
    public class MasterController : BaseController
    {
        private IMD_UserService _userService = new MD_UserService();
        private IMDOrganizationService _mdOrgService = new MD_OrganizationService();
        private OperationResult result = new OperationResult();
        private ISys_RoleService _sysRoleService = new Sys_RoleService();
        private ISys_ModuleService _sysModuleService = new Sys_ModuleService();
        private ISys_ModuleConfigurationService _sysModuleConfigurationService = new Sys_ModuleConfigurationService();
        private IDSD_M_SystemConfigService _sysConfigService = new DSD_M_SystemConfigService();
        private ISys_UserRoleLinkService _sysUserRoleLinkService = new Sys_UserRoleLinkService();
        private IMD_DictionaryService _mdDictionary = new MD_DictionaryService();
        private IMD_ProductService _mdProductService = new MD_ProductService();
        private IMD_AccountService _mdAccountService = new MD_AccountService();
        private IDSD_M_TruckService _dsdMTruckService = new DSD_M_TruckService();
        private IMD_CompanyService _mdCompanyService = new MD_CompanyService();
        private IMD_WarehouseService _mdWarehouseService = new MD_WarehouseService();
        private IDSD_M_TruckCheckListService _dsdMTruckCheckListService = new DSD_M_TruckCheckListService();
        private IMD_AccountARService _mdAccountArService = new MD_AccountARService();
        private IMD_UserService _mdUserService = new MD_UserService();

        #region User
        public ActionResult User()
        {
            return View();
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveUser(FormCollection value)
        {
            try
            {
                var userCode = value["UserCode"].ToString().Trim();
                var firstName = value["FirstName"].ToString().Trim();
                var lastName = value["LastName"].ToString().Trim();
                var type = value["UserType"].ToString().Trim();
                var company = value["Company"].ToString();
                var warehouse = value["Warehouse"].ToString();
                var email = value["Email"].ToString().Trim();
                var mobile = value["Mobile"].ToString().Trim();
                bool valid = bool.Parse(value["Valid"]);
                bool locked = bool.Parse(value["Locked"]);
                bool needChangePwd = bool.Parse(value["NeedChangePwd"]);
                int orgId; ;
                int.TryParse(value["OrgId"], out orgId);
                var mode = value["Mode"].ToString();

                var roleIds = value["RoleIds"];
                //新role
                List<Sys_UserRoleLink> userRoleListNew = new List<Sys_UserRoleLink>();
                Sys_UserRoleLink item;
                if (roleIds != null && roleIds != "")
                {
                    string[] roleArr = roleIds.Split(',');
                    for (var i = 0; i < roleIds.Split(',').Length; i++)
                    {
                        item = new Sys_UserRoleLink();
                        item.UserCode = userCode;
                        item.RoleID = int.Parse(roleIds.Split(',')[i].ToString());
                        item.DeleteFlag = false;
                        item.Creator = CurrentUserInfo.UserName;
                        item.CreatedTime = DateTime.Now;
                        item.Modifier = CurrentUserInfo.UserName;
                        item.ModifiedTime = DateTime.Now;

                        userRoleListNew.Add(item);
                    }
                }

                MD_User user = _userService.CheckData(userCode);
                MD_Person person = _userService.GetPersonByCode(userCode);
                if (mode == "Add")
                {
                    #region Add User
                    if (user != null)
                        return Json(new { result = "failed", msg = Resources.Message.msg_ExistSameCode }, JsonRequestBehavior.AllowGet);

                    //获取default password
                    List<ViewConfigData> data = _sysConfigService.GetConfigData("Password", "Default", int.Parse(orgId.ToString()));
                    var defaultPwd = "111111";
                    if (data != null && data.Count() > 0)
                    {
                        defaultPwd = data[0].Value;
                    }

                    user = new MD_User();
                    user.UserCode = userCode;
                    user.Password = Common.Encrypt.MD5Encrypt(defaultPwd.ToString()).ToUpper();
                    user.NeedChangePassword = needChangePwd;
                    user.LastChangePassword = DateTime.Now;
                    user.IsValid = valid;
                    user.IsLock = locked;
                    user.CreateTime = DateTime.Now;
                    user.CreateUser = CurrentUserInfo.UserName;
                    user.LastUpdateTime = DateTime.Now;
                    user.LastUpdateUser = CurrentUserInfo.UserName;

                    person = new MD_Person();
                    person.UserCode = userCode;
                    person.FirstName = firstName;
                    person.LastName = lastName;
                    person.Type = type;
                    person.CompanyCode = company;
                    person.WarehouseCode = warehouse;
                    person.OrgId = orgId;
                    person.Email = email;
                    person.Mobile = mobile;
                    person.CreateTime = DateTime.Now;
                    person.CreateUser = CurrentUserInfo.UserName;
                    person.LastUpdateTime = DateTime.Now;
                    person.LastUpdateUser = CurrentUserInfo.UserName;

                    _userService.AddUser(user, person, userRoleListNew);
                    #endregion
                }
                else
                {
                    #region Modify User
                    if (user == null)
                        return Json(new { result = "failed", msg = Resources.Message.msg_HasbeenDeleted }, JsonRequestBehavior.AllowGet);


                    user.UserCode = userCode;
                    user.NeedChangePassword = needChangePwd;
                    user.IsValid = valid;
                    user.IsLock = locked;
                    user.LastUpdateTime = DateTime.Now;
                    user.LastUpdateUser = CurrentUserInfo.UserName;

                    person.FirstName = firstName;
                    person.LastName = lastName;
                    person.Type = type;
                    person.CompanyCode = company;
                    person.WarehouseCode = warehouse;
                    person.OrgId = orgId;
                    person.Email = email;
                    person.Mobile = mobile;
                    person.LastUpdateTime = DateTime.Now;
                    person.LastUpdateUser = CurrentUserInfo.UserName;

                    //取出old role
                    List<Sys_UserRoleLink> userRoleListOld = _sysUserRoleLinkService.GetDataByUserCode(userCode);
                    
                    _userService.ModifiedUser(user, person, userRoleListNew, userRoleListOld);

                    #endregion
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


        /// <summary>
        /// 获取Org Tree Data
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ActionResult GetOrganizationTreeData(FormCollection value)
        {
            bool IsAll = true;
            if (Request.QueryString["IsAll"] != null)
            {
                IsAll = bool.Parse(Request.QueryString["IsAll"]);
            }
            List<object> data = _mdOrgService.GetOrganizationTreeData(0, IsAll);
            string jsonstring = MyJson.ConvertToJson(data);
            jsonstring = jsonstring.Replace("[[", "[").Replace("]]", "]");
            return Content(jsonstring);
            //return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除User --逻辑删除
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteUser(FormCollection value)
        {
            string UserCode = value["UserCode"].ToString();
            MD_User userData = _userService.CheckData(UserCode);
            if (userData == null)
            {
                return Json(new { result = "failed", msg = Resources.Message.msg_HasbeenDeleted }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                userData.IsValid = false;
                userData.LastUpdateUser = CurrentUserInfo.UserName;
                userData.LastUpdateTime = DateTime.Now;

                _userService.DeleteUser(userData);

                var postJson = new { result = result.ResultType.ToString(), msg = result.Message };
                return Json(postJson, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 恢复User
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ReValidUser(FormCollection value)
        {
            string UserCode = value["UserCode"].ToString();
            MD_User userData = _userService.CheckData(UserCode);
            if (userData == null)
            {
                return Json(new { result = "failed", msg = Resources.Message.msg_HasbeenDeleted }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                userData.IsValid = true;
                userData.LastUpdateUser = CurrentUserInfo.UserName;
                userData.LastUpdateTime = DateTime.Now;

                _userService.DeleteUser(userData);

                var postJson = new { result = result.ResultType.ToString(), msg = result.Message };
                return Json(postJson, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ResetPassword(FormCollection value)
        {
            string UserCode = value["UserCode"].ToString();
            MD_User userData = _userService.CheckData(UserCode);
            if (userData == null)
            {
                return Json(new { result = "failed", msg = Resources.Message.msg_HasbeenDeleted }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int OrgId;
                int.TryParse(value["OrgID"].ToString(), out OrgId);
                //获取default password
                List<ViewConfigData> data = _sysConfigService.GetConfigData("Password", "Default", OrgId);
                var defaultPwd = "111111";
                if (data != null && data.Count() > 0)
                {
                    defaultPwd = data[0].Value;
                }

                userData.Password = Common.Encrypt.MD5Encrypt(defaultPwd.ToString()).ToUpper();
                userData.LastUpdateUser = CurrentUserInfo.UserName;
                userData.LastUpdateTime = DateTime.Now;

                _userService.DeleteUser(userData);

                var postJson = new { result = result.ResultType.ToString(), msg = result.Message };
                return Json(postJson, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 获取User List
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUserDataList(FormCollection value)
        {
            ParameterQuery param = SetPageSize();
            string orgId = value["OrgID"] == null ? "" : value["OrgID"].ToString();
            string userCode = value["UserCode"].ToString();
            string type = value["Type"].ToString();
            string roleId = value["RoleId"].ToString();
            string status = value["Status"].ToString();

            List<ViewUserData> data = _userService.LoadUserList(orgId, userCode, type, roleId, status, param);

            var postJson = new { total = param.total, rows = data };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取role list
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRoleDataList(FormCollection value)
        {
            var UserCode = value["UserCode"].ToString();
            ParameterQuery param = SetPageSize();
            List<ViewUserRoleData> data = _sysRoleService.GetRoleData(param, UserCode);

            var postJson = new { total = param.total, rows = data };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Role

        public ActionResult Role()
        {
            ViewBag.Module = GetModuleList();
            return View();
        }

        /// <summary>
        /// 加载所有用户角色
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllRole(FormCollection value)
        {
            List<Sys_Role> lstRole = _sysRoleService.LoadSysRoleInfo("");
            var data = from x in lstRole
                       select new
                       {
                           x.RoleID,
                           x.Name,
                           x.Description,
                           x.DeleteFlag
                       };

            var postJson = new { total = data.Count(), rows = data };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost]
        public ActionResult GetUserForRole(FormCollection value)
        {
            ParameterQuery param = SetPageSize();
            int roleId;
            int.TryParse(value["RoleId"], out roleId);
            var UserCode = value["UserCode"].ToString();

            List<ViewUserData> data = _userService.LoadUserForRole(roleId, UserCode, param);

            var postJson = new { total = param.total, rows = data };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        public List<ModuleModel> GetModuleList()
        {
            var data = _sysModuleService.LoadSysModuleInfo();

            var result = from x in data
                         select new ModuleModel
                         {
                             id = x.ModuleID,
                             code = x.ModuleCode,
                             name = x.ModuleName,
                             parent_code = x.ParentModule,
                             seq = x.Sequence,
                             action_mode = x.ActionMode,
                             client_scope = x.ClientScope,
                             action = GetKeyValueByKey(x.Action)//JsonConvert.DeserializeObject<ModuleActionTypeModel>(x.Action)
                         };
            
            List<ModuleModel> listModule = result
                .Where(p => string.IsNullOrWhiteSpace(p.parent_code))
                .OrderBy(p => p.seq)
                .ToList();

            for (int i = 0; i < listModule.Count(); i++)
            {
                List<ModuleModel> subModule = result
                    .Where(p => p.parent_code == listModule[i].code)
                    .OrderBy(p => p.seq)
                    .ToList();

                for (int j = 0; j < subModule.Count(); j++)
                    listModule[i].children.Add(subModule[j]);
            }

            return listModule;
        }

        [NonAction]
        public ModuleActionTypeModel GetKeyValueByKey(string Action)
        {
            ModuleActionTypeModel item = JsonConvert.DeserializeObject<ModuleActionTypeModel>(Action);
            if (item == null)
                return new ModuleActionTypeModel();
            List<ModuleKeyValueModel> configs = item.configuration;
            foreach (ModuleKeyValueModel data in configs)
            {
                data.value = Resources.Language.ResourceManager.GetString(data.value);
            }
            return item;
        }

        /// <summary>
        /// 获取角色已有权限
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSelectedModule(int RoleId)
        {
            List<Sys_ModuleConfiguration> LstData = _sysModuleConfigurationService.LoadSysModuleConfigurationInfo(RoleId);

            var result = from x in LstData
                         select new
                         {
                             module_id = x.ModuleID,
                             action_value = x.Action
                         };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 插入更新数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveRoleData(FormCollection value)
        {
            //接收Post提交的参数
            string Name = value["Name"];
            int RoleId;
            int.TryParse(Request.Form["RoleId"], out RoleId);
            string Description = value["Description"];

            //检测是否存在Code重复
            Sys_Role data = _sysRoleService.CheckData(RoleId);
            if (RoleId == 0)
            {
                if (data != null)
                    return Json(new { result = "failed", msg = Resources.Message.msg_ExistSameCode }, JsonRequestBehavior.AllowGet);

                data = new Sys_Role();
                data.RoleID = RoleId;
                data.Name = Name;
                data.DeleteFlag = false;
                data.Description = Description;
                data.Creator = CurrentUserInfo.UserName;
                data.CreatedTime = DateTime.Now;
                data.Modifier = CurrentUserInfo.UserName;
                data.ModifiedTime = DateTime.Now;
                _sysRoleService.AddData(data);
            }
            else
            {
                if (data == null)
                    return Json(new { result = "failed", msg = Resources.Message.msg_NoData }, JsonRequestBehavior.AllowGet);

                data.Name = Name;
                data.Description = Description;
                data.Modifier = CurrentUserInfo.UserName;
                data.ModifiedTime = DateTime.Now;
                _sysRoleService.UpdateData(data);
            }

            var postJson = new { result = "succeed" };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 数据无效化(逻辑删除)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteRoleData(FormCollection value)
        {
            int RoleId;
            int.TryParse(Request.Form["RoleId"], out RoleId);
            Sys_Role data = _sysRoleService.CheckData(RoleId);
            if (data == null)
                return Json(new { result = "failed", msg = Resources.Message.msg_NoData }, JsonRequestBehavior.AllowGet);
            else
            {
                data.DeleteFlag = true;
                data.Modifier = CurrentUserInfo.UserName;
                data.ModifiedTime = DateTime.Now;
                _sysRoleService.UpdateData(data);
            }

            var postJson = new { result = "succeed" };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 恢复无效化数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ReValidRoleData(FormCollection value)
        {
            int RoleId;
            int.TryParse(Request.Form["RoleId"], out RoleId);
            Sys_Role data = _sysRoleService.CheckData(RoleId);
            if (data == null)
                return Json(new { result = "failed", msg = Resources.Message.msg_NoData }, JsonRequestBehavior.AllowGet);
            else
            {
                data.DeleteFlag = false;
                data.Modifier = CurrentUserInfo.UserName;
                data.ModifiedTime = DateTime.Now;
                _sysRoleService.UpdateData(data);
            }

            var postJson = new { result = "succeed" };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateModuleConfiguration()
        {
            //必填
            int RoleId, ModuleId;
            bool IsValid;
            string Action = Request.Form["Action"] == null ? "" : Request.Form["Action"];
            int.TryParse(Request.Form["RoleId"], out RoleId);
            int.TryParse(Request.Form["ModuleId"], out ModuleId);
            bool.TryParse(Request.Form["IsValid"], out IsValid);

            Sys_ModuleConfiguration data = _sysModuleConfigurationService.CheckData(RoleId, ModuleId);

            if (data == null)
            {
                data = new Sys_ModuleConfiguration();
                data.RoleID = RoleId;
                data.ModuleID = ModuleId;
                data.Action = Action.ToString() ;
                data.Activated = IsValid;
                data.Creator = CurrentUserInfo.UserName;
                data.CreatedTime = DateTime.Now;
                data.Modifier = CurrentUserInfo.UserName;
                data.ModifiedTime = DateTime.Now;
                _sysModuleConfigurationService.AddData(data);
            }
            else
            {
                data.Action = Action.ToString();
                data.Activated = IsValid;
                data.Modifier = CurrentUserInfo.UserName;
                data.ModifiedTime = DateTime.Now;
                _sysModuleConfigurationService.UpdateData(data);
            }

            var postJson = new { result = "succeed" };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveUserRoleLink(FormCollection value)
        {
            var UserCode = value["UserCode"].ToString();
            int RoleID = int.Parse(value["RoleID"].ToString());
            bool IsChecked = bool.Parse(value["IsChecked"].ToString());

            try
            {
                Sys_UserRoleLink item = _sysUserRoleLinkService.GetData(UserCode, RoleID);
                if (item == null)
                {
                    item = new Sys_UserRoleLink();
                    item.UserCode = UserCode;
                    item.RoleID = RoleID;
                    item.DeleteFlag = false;
                    item.Creator = CurrentUserInfo.UserName;
                    item.CreatedTime = DateTime.Now;
                    item.Modifier = CurrentUserInfo.UserName;
                    item.ModifiedTime = DateTime.Now;

                    _sysUserRoleLinkService.AddUserRoleLink(item);
                }
                else
                {
                    item.DeleteFlag = !IsChecked;
                    item.Modifier = CurrentUserInfo.UserName;
                    item.ModifiedTime = DateTime.Now;

                    _sysUserRoleLinkService.UpdateUserRoleLink(item);
                }
                var postJson = new { result = "succeed" };
                return Json(postJson, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var postJson = new { result = "failed" };
                return Json(postJson, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Product

        public ActionResult Product()
        {
            return View();
        }

        /// <summary>
        /// 获取product list
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetProductList(FormCollection value)
        {
            string Product = value["Product"].ToString();
            string Brand = value["Brand"].ToString();
            string Pack = value["Pack"].ToString();
            string Status = value["Status"];
            bool isEmpty;
            bool.TryParse(value["IsEmpty"], out isEmpty);
            bool Equipment;
            bool.TryParse(value["Equipment"], out Equipment);
            ParameterQuery param = SetPageSize();

            List<ViewProductData> data = _mdProductService.GetProductList(Product, Brand, Pack, Status, isEmpty, Equipment, param);

            var postJson = new { total = param.total, rows = data };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Account

        public ActionResult Account()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetAccountDataList(FormCollection value)
        {
            ParameterQuery param = SetPageSize();
            string CompanyCode = value["CompanyCode"];
            string Account = value["Account"].ToString();

            List<ViewAccountData> data = _mdAccountService.GetAccoutListData(CompanyCode, Account, param);

            var postJson = new { total = param.total, rows = data };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Truck

        public ActionResult Truck(FormCollection value)
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetTruckDriverList(FormCollection value)
        {
            ParameterQuery param = SetPageSize();
            string CompanyCode = value["CompanyCode"];
            string TruckType = value["TruckType"];
            string TruckNumber = value["TruckNumber"];
            string Status = value["Status"];

            List<ViewTruckDriverData> LstData = _dsdMTruckService.GetTruckDriverList(CompanyCode, TruckType, TruckNumber, Status, param);

            var postJson = new { total = param.total, rows = LstData };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveTruckDriver(FormCollection value)
        {
            int TruckId;
            int.TryParse(value["TruckId"], out TruckId);
            string Driver = value["Driver"];

            try
            {
                DSD_M_TruckDriver item = _dsdMTruckService.CheckData(TruckId);
                if (item == null)
                {
                    //return Json(new { result = "failed", msg = Resources.Message.msg_HasbeenDeleted }, JsonRequestBehavior.AllowGet);
                    item = new DSD_M_TruckDriver();
                    item.TruckId = TruckId;
                    item.Driver = Driver;
                    item.CreateTime = DateTime.Now;
                    item.CreateUser = CurrentUserInfo.LoginName;
                    item.LastUpdateTime = DateTime.Now;
                    item.LastUpdateUser = CurrentUserInfo.LoginName;

                    _dsdMTruckService.SaveTruckDriver(item);
                }
                else
                {
                    item.Driver = Driver;
                    item.LastUpdateTime = DateTime.Now;
                    item.LastUpdateUser = CurrentUserInfo.LoginName;
                    _dsdMTruckService.UpdateTruckDriver(item);
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

        /// <summary>
        /// 获取Driver信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDriverList(FormCollection value)
        {
            List<ViewComboboxStringData> LstData = _mdUserService.GetDriverList();
            return Json(LstData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Company

        public ActionResult Company(FormCollection value)
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetCompanyList(FormCollection value)
        {
            ParameterQuery param = SetPageSize();
            var CompanyCode = value["CompanyCode"];
            var CompanyName = value["CompanyName"];
            var Status = value["Status"];

            List<MD_Company> LstData = _mdCompanyService.GetCompanyList(CompanyCode, CompanyName, Status, param);
            var jsonData = from d in LstData
                           select new
                           {
                               d.CompanyCode,
                               d.CompanyName,
                               d.Remark,
                               d.IsActive
                           };

            var postJson = new { total = param.total, rows = jsonData };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Warehouse

        public ActionResult Warehouse(FormCollection value)
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetWarehouseList(FormCollection value)
        {
            ParameterQuery param = SetPageSize();
            var Code = value["WarehouseCode"];
            var Name = value["WarehouseName"];
            var CompanyCode = value["CompanyCode"];
            var Status = value["Status"];
            List<ViewWarehouseData> LstData = _mdWarehouseService.GetWarehouseList(Code, Name, CompanyCode, Status, param);
            var postJson = new { total = param.total, rows = LstData };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region TruckCheckList

        public ActionResult TruckCheckList(FormCollection value)
        {
            TruckCheckListData ItemData = new TruckCheckListData();
            ViewBag.CheckItems = ItemData.LstQuestion;
            return View();
        }

        [HttpPost]
        public ActionResult GetTruckCheckList(FormCollection value)
        {
            ParameterQuery param = SetPageSize();
            var Content = value["Content"];
            var TruckType = value["TruckType"];
            List<ViewTruckCheckListData> LstData = _dsdMTruckCheckListService.GetTruckCheckList(Content, TruckType, param);
            var postJson = new { total = param.total, rows = LstData };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetTruckTypeList(FormCollection value)
        {
            ParameterQuery param = SetPageSize();
            int ListId = int.Parse(value["ListId"]);
            List<ViewTruckTypeData> LstData = _dsdMTruckCheckListService.GetTruckTypeList(ListId, param);
            var postJson = new { total = param.total, rows = LstData };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveTruckCheckAssign(FormCollection value)
        {
            int ListId = int.Parse(value["ListId"]);
            var types = value["types"];

            try
            {
                List<DSD_M_TruckCheckAssign> itemLst = new List<DSD_M_TruckCheckAssign>();
                DSD_M_TruckCheckAssign item;
                if (types != null && types != "")
                {
                    string[] typeLst = types.Split(',');
                    for (int i = 0; i < typeLst.Length; i++)
                    {
                        item = new DSD_M_TruckCheckAssign();
                        item.TruckType = typeLst[i];
                        item.CheckListId = ListId;
                        item.Valid = true;
                        item.CreateTime = DateTime.Now;
                        item.CreateUser = CurrentUserInfo.LoginName;
                        item.LastUpdateTime = DateTime.Now;
                        item.LastUpdateUser = CurrentUserInfo.LoginName;

                        itemLst.Add(item);
                    }

                    _dsdMTruckCheckListService.SaveTruckAssign(itemLst, ListId);
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
        public ActionResult SaveTruckCheckData(FormCollection value)
        {
            try
            {
                var Title = value["Content"];
                var Description = value["Description"];
                int Id;
                int.TryParse(value["hidId"], out Id);

                TruckCheckListData item = new TruckCheckListData();
                item.CheckListId = Id;
                item.Title = Title;
                item.Description = Description;
                item.StartDate = DateTime.Parse(value["StartDate"]);
                item.EndDate = DateTime.Parse(value["EndDate"]);

                List<Questions> LstQuestion = new List<Questions>();
                List<Answers> LstAnswer;
                Questions question;
                Answers answer;

                int MaxQIndex;
                int.TryParse(value["MaxQIndex"], out MaxQIndex);
                for (int i = 1; i <= MaxQIndex; i++)
                {
                    if (value["Question_" + i] != null && value["Question_" + i] != "")
                    {
                        bool MustToDo = false;
                        if (value["MustToDo_" + i] != null && value["MustToDo_" + i] != "")
                        {
                            MustToDo = value["MustToDo_" + i] == "on" ? true : false;
                        }
                        question = new Questions();
                        question.Question = value["Question_" + i];
                        question.InputeType = value["CheckType_" + i];
                        question.MustToDo = MustToDo;
                        question.Id = int.Parse(value["QuestionId_" + i]);

                        LstAnswer = new List<Answers>();
                        for (int j = 0; j < value.AllKeys.Length; j++)
                        {
                            if (value.AllKeys[j].Contains("Question_" + i + "_"))
                            {
                                answer = new Answers();
                                answer.Answer = value[value.AllKeys[j]];
                                answer.Id = int.Parse(value[value.AllKeys[j].Replace("Question", "QuestionId").Replace("Answer", "AnswerId")]);
                                LstAnswer.Add(answer);
                            }
                        }
                        question.LstAnswer = LstAnswer;

                        LstQuestion.Add(question);
                    }
                }
                item.LstQuestion = LstQuestion;
                string mode = "";
                if (Id == 0)
                    mode = "Add";
                else
                    mode = "Edit";
                _dsdMTruckCheckListService.SaveTruckCheckList(item, CurrentUserInfo.LoginName, mode);

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
        public ActionResult GetTruckCheckDataById(FormCollection value)
        {
            int id;
            int.TryParse(value["id"], out id);
            TruckCheckListData checkData = _dsdMTruckCheckListService.GetTruckCheckDataById(id);
            int qIndex = 1;
            string divHtml = "";
            foreach (Questions q in checkData.LstQuestion)
            {
                string checkedStr = "";
                if (q.MustToDo != null && q.MustToDo == true)
                {
                    checkedStr = " checked=\"checked\"";
                }
                string addNewBtnShow = "";
                if (q.InputeType == "1")
                {
                    addNewBtnShow = " style=\"display:none\"";
                }
                divHtml += "<div style=\"border: 1px solid #ebe2e2; height: auto; padding: 5px 0 5px 20px; margin: 5px 0;\">" +
                         "   <ul>" +
                         "       <li>" +
                         "           <label class=\"cs-label\" style=\"display: inline-block;\">Question(" + q.InputeTypeDesc + ")</label>" +
                         "           <input type=\"text\" style=\"width: 330px\" value=\""+q.Question+"\" class=\"easyui-validatebox\" id=\"Question_" + qIndex + "\" name=\"Question_" + qIndex + "\" data-options=\"validType:['length[0,100]']\" />" +
                         "           <a href=\"#\" onclick=\"AddNewAnswer(this,0)\" class=\"imgClass\" id=\"addNewAnswer_" + qIndex + "\" " + addNewBtnShow + ">" +
                         "               <img src=\"../Images/icons/edit_add.png\" /></a>" +
                         "           <a href=\"#\" onclick=\"DeleteQuestion(this)\" class=\"imgClass\">" +
                         "               <img src=\"../Images/icons/edit_del.png\" /></a>" +
                         "           <input type=\"checkbox\" name=\"MustToDo_" + qIndex + "\" " + checkedStr + "  />" +
                         "           <input type=\"hidden\" id=\"CheckType_" + qIndex + "\" name=\"CheckType_" + qIndex + "\" value=\"" + q.InputeType + "\" />" +
                         "           <input type=\"hidden\" id=\"QuestionId_" + qIndex + "\" name=\"QuestionId_" + qIndex + "\" value=\"" + q.Id + "\" />" +
                         "           <input type=\"hidden\" name=\"CurQIndex\" value=\"" + qIndex + "\" />" +
                         "           <input type=\"hidden\" name=\"MaxAIndex\" value=\"" + (q.AnswerCount+1) + "\" />" +
                         "       </li>" +
                         "   </ul>";
                int aIndex = 1;
                foreach (Answers a in q.LstAnswer)
                {
                    divHtml += "<ul>" +
                             "   <li>" +
                             "       <label class=\"cs-label\" style=\"display: inline-block;\">" +
                             "           Answer" +
                             "       </label>" +
                             "       <input type=\"text\" style=\"width: 350px\" value=\"" + a.Answer + "\" class=\"easyui-validatebox\" id=\"Question_" + qIndex + "_Answer_" + aIndex + "\" name=\"Question_" + qIndex + "_Answer_" + aIndex + "\" data-options=\"validType:['length[0,100]']\" />" +
                             "       <a href=\"#\" onclick=\"AddNewAnswer(this,1)\" class=\"imgClass\">" +
                             "           <img src=\"../Images/icons/edit_add.png\" /></a>" +
                             "       <a href=\"#\" onclick=\"DeleteAnswer(this)\" class=\"imgClass\">" +
                             "           <img src=\"../Images/icons/edit_del.png\" /></a>" +
                             "       <input type=\"hidden\" id=\"QuestionId_" + qIndex + "_AnswerId_" + aIndex + "\" name=\"QuestionId_" + qIndex + "_AnswerId_" + aIndex + "\" value=\"" + a.Id + "\" />" +
                             "   </li>" +
                             "</ul>";
                    aIndex++;
                }
                divHtml += "</div>";
                qIndex++;
            }
            result.ResultType = OperationResultType.succeed;
            var postJson = new { result = result.ResultType.ToString(), QuestionCount = checkData.LstQuestion.Count, msg = divHtml };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteTruckList(FormCollection value)
        {
            int Id;
            int.TryParse(value["Id"], out Id);

            DSD_M_TruckCheckList item = _dsdMTruckCheckListService.GetDataById(Id);
            if (item == null)
            {
                return Json(new { result = "failed", msg = Resources.Message.msg_HasbeenDeleted }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                item.Valid = false;
                item.LastUpdateTime = DateTime.Now;
                item.LastUpdateUser = CurrentUserInfo.LoginName;

                _dsdMTruckCheckListService.UpdateEntities(item);

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
        public ActionResult ReValidTruckList(FormCollection value)
        {
            int Id;
            int.TryParse(value["Id"], out Id);

            DSD_M_TruckCheckList item = _dsdMTruckCheckListService.GetDataById(Id);
            if (item == null)
            {
                return Json(new { result = "failed", msg = Resources.Message.msg_HasbeenDeleted }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                item.Valid = true;
                item.LastUpdateTime = DateTime.Now;
                item.LastUpdateUser = CurrentUserInfo.LoginName;

                _dsdMTruckCheckListService.UpdateEntities(item);

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

        #region Organization

        public ActionResult Organization()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoadOrganizationData(FormCollection value)
        {
            List<ViewTreeGridData> LstData = _mdOrgService.LoadOrganizationData();
            return Json(LstData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveOrganization(FormCollection value)
        {
            int Id;
            int.TryParse(value["ID"], out Id);
            string Code = value["Code"];
            string Name = value["Name"];
            int ParentId;
            int.TryParse(value["ParentId"], out ParentId);
            bool Valid;
            bool.TryParse(value["Valid"], out Valid);
            int Level;
            int.TryParse(value["Level"], out Level);
            string Mode = value["Mode"];

            try
            {
                MD_Organization data;
                if (Mode == "Add")
                {
                    data = _mdOrgService.CheckData(Code);
                    if (data != null)
                        return Json(new { result = "failed", msg = Resources.Message.msg_ExistSameCode }, JsonRequestBehavior.AllowGet);

                    data = new MD_Organization();
                    data.ParentId = Id;
                    data.Code = Code;
                    data.Name = Name;
                    data.Level = Level + 1;
                    data.Valid = Valid;
                    data.CreateUser = CurrentUserInfo.LoginName;
                    data.CreateTime = DateTime.Now;
                    data.LastUpdateTime = DateTime.Now;
                    data.LastUpdateUser = CurrentUserInfo.LoginName;

                    _mdOrgService.AddEntities(data);
                }
                else
                {
                    data = _mdOrgService.GetDataById(Id);
                    data.Name = Name;
                    data.ParentId = ParentId;
                    data.Valid = Valid;
                    data.Level = _mdOrgService.GetLevelById(ParentId) + 1;
                    data.LastUpdateUser = CurrentUserInfo.LoginName;
                    data.LastUpdateTime = DateTime.Now;

                    _mdOrgService.UpdateEntities(data);
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

        #region AccountAR

        public ActionResult AccountAR(FormCollection value)
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetAccountARList(FormCollection value)
        {
            ParameterQuery param = SetPageSize();
            string account = value["Account"];
            List<ViewAccountARData> LstData = _mdAccountArService.GetAccountARList(account, param);

            var postJson = new { total = param.total, rows = LstData };
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
