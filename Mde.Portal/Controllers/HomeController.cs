using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Threading;
using System.Web.Mvc;
using Mde.Portal.Model;
using Mde.IBLL;
using Mde.BLL;
using Mde.Model.DataModel;
using System.Web.Caching;
using Mde.Portal.Common;
using Mde.ModelView.ModelView;
using System.Resources;

namespace Mde.Portal.Controllers
{
    public class HomeController : BaseController
    {
        private ISys_ModuleService _sysModuleService = new Sys_ModuleService();
        private IMD_UserService _mdUserService = new MD_UserService();

        /// <summary>
        /// 加载左侧的导航菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadMenu()
        {
            //SecurityService service = new SecurityService();
            //var data = service.GetPortalMenu(CurrentUserInfo.RoleID);
            //List<Sys_Person> person = _sysPersonService.LoadSysPersonInfo("Admin");

            var module = _sysModuleService.GetMenuList(CurrentUserInfo.RoleID);
            var topMenus = module.Where(p => string.IsNullOrWhiteSpace(p.ParentModule)).OrderBy(p => p.Sequence).ToList();
            List<NavigationMenuModel> listMenuModel = new List<NavigationMenuModel>();

            List<ViewButtonRoleData> lstRole = new List<ViewButtonRoleData>();
            ViewButtonRoleData role = new ViewButtonRoleData();
            CacheHelper.RemoveAllCache("Role");
            foreach (var topMenu in topMenus)
            {
                NavigationMenuModel topModel = new NavigationMenuModel();
                topModel.menu_id = topMenu.ModuleID.ToString();
                topModel.name = topMenu.ModuleName;
                topModel.url = topMenu.Url;
                topModel.icon = topMenu.Icon;

                var subMenus = module.Where(p => p.ParentModule == topMenu.ModuleCode).OrderBy(p => p.Sequence).ToList();
                foreach (var subMenu in subMenus)
                {
                    NavigationMenuModel subModel = new NavigationMenuModel();
                    subModel.menu_id = subMenu.ModuleID.ToString();
                    subModel.name = subMenu.ModuleName;
                    subModel.url = subMenu.Url;
                    subModel.icon = subMenu.Icon;

                    topModel.children.Add(subModel);
                    role = new ViewButtonRoleData();
                    role.ModuleID = subMenu.ModuleID.ToString();
                    role.Action = subMenu.Action;
                    role.ButtonRole = subMenu.ButtonRole;
                    lstRole.Add(role);
                }

                if (topModel.children.Count > 0)
                    listMenuModel.Add(topModel);
            }
            CacheHelper.SetCache("Role", lstRole);

            return Json(listMenuModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            ViewBag.OldPassword = Request.QueryString["OldPwd"];

            if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
                ViewBag.Pic = "banner.en-us.png";
            else
                ViewBag.Pic = "banner.jpg";

            string action = Request.QueryString["action"];
            if (string.IsNullOrWhiteSpace(action))
            {
                ViewBag.UserInfo = CurrentUserInfo;
                return View();
            }
            else if (action.ToLower() == "logout")
            {
                Session.Remove("UserInfo");
                return Redirect("~/Login");
            }
            else if (action.ToLower() == "keep_live")
                return Content("ok");

            return null;

        }

        public ActionResult Main()
        {
            ViewBag.User = false;
            ViewBag.Customer = false;
            ViewBag.CustomerCoverage = false;
            ViewBag.Product = false;

            ViewBag.SaleRecord = false;
            ViewBag.SaleOrder = false;
            ViewBag.CustomerAudit = false;

            ViewBag.PhotoView = false;
            ViewBag.VisitResult = false;
            ViewBag.SalesVolumeReport = false;

            //SecurityService service = new SecurityService();
            //var data = service.GetPortalMenu(CurrentUserInfo.RoleID);

            //for (int i = 0; i < data.Count; i++)
            //{
            //    switch (data[i].ModuleCode)
            //    {
            //        case "0901":
            //            ViewBag.User = true;
            //            break;
            //        case "0102":
            //            ViewBag.Customer = true;
            //            break;
            //        case "0103":
            //            ViewBag.CustomerCoverage = true;
            //            break;
            //        case "0202":
            //            ViewBag.Product = true;
            //            break;

            //        case "0502":
            //            ViewBag.SaleRecord = true;
            //            break;
            //        case "0503":
            //            ViewBag.SaleOrder = true;
            //            break;
            //        case "0101":
            //            ViewBag.CustomerAudit = true;
            //            break;

            //        case "0601":
            //            ViewBag.PhotoView = true;
            //            break;
            //        case "0703":
            //            ViewBag.VisitResult = true;
            //            break;
            //        case "1102":
            //            ViewBag.SalesVolumeReport = true;
            //            break;
            //    }
            //}

            return View();
        }



        /// <summary>
        /// 登录后修改密码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdatePwd(FormCollection value)
        {
            string oldPwd = value["old_pwd"];
            string newPwd = value["new_pwd"];
            string EncryptOldPwd = Common.Encrypt.MD5Encrypt(oldPwd).ToUpper();
            string EncryptNewPwd = Common.Encrypt.MD5Encrypt(newPwd).ToUpper();

            //判断原密码输入是否正确
            MD_User data = _mdUserService.LoadUserInfo(CurrentUserInfo.LoginName, EncryptOldPwd);
            if (data == null)
            {
                return Json(new { result = "failed" }, JsonRequestBehavior.AllowGet);
            }

            data.Password = EncryptNewPwd;
            data.LastChangePassword = DateTime.Now;
            data.LastUpdateTime = DateTime.Now;
            data.LastUpdateUser = CurrentUserInfo.LoginName;

            _mdUserService.UpdateEntities(data);

            var postJson = new { result = "succeed" };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 初次登录强制修改密码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangePassword(FormCollection value)
        {
            //string UserName = CurrentUserInfo.UserName;
            //string newPwd = Common.Encrypt.MD5Encrypt(value["NewPwd"]).ToUpper();

            //Sys_Person sysPerson = _sysPersonService.GetPersonByUserName(UserName);
            //sysPerson.Password = newPwd;
            //sysPerson.Modifier = UserName;
            //sysPerson.ModifiedTime = DateTime.Now;

            //_sysPersonService.UpdatePwd(sysPerson);
            var postJson = new { result = "succeed" };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }
    }
}
