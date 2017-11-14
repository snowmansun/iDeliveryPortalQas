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

namespace Mde.Portal.Controllers
{
    public class LoginController : Controller
    {
        //private ISys_PersonService _sysPersonService = new Sys_PersonService();
        private IMD_UserService _mdUserService = new MD_UserService();

        //
        // GET: /Login/

        public ActionResult Index()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-us");
            Response.Cookies["CurrentUICulture"].Value = "en-us";
            string languge = Thread.CurrentThread.CurrentUICulture.Name;
            ViewBag.CurrentLanguage = languge.ToLower();

            return View();
        }

        [HttpGet]
        public ActionResult GetCurrentLanguage()
        {
            string languge = Thread.CurrentThread.CurrentUICulture.Name;
            return Content(languge.ToLower());
        }

        public ActionResult SetLanguage(string language)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(language);
            Response.Cookies["CurrentUICulture"].Value = language;

            return View("Index");
        }
        

        public ActionResult Login(FormCollection values)
        {
            AjaxResult ajaxResult = new AjaxResult();
            string LoginName = values["LoginName"];
            string Password = Encrypt.MD5Encrypt(values["Password"]).ToUpper();
            string SecurityCode = values["Scd"];

            string currentSecurityCode = (string)Session["SecurityCode"];
            if (SecurityCode != currentSecurityCode)
            {
                ajaxResult.Message = Resources.Message.msg_ValidateCodeError;
                return Json(ajaxResult, JsonRequestBehavior.AllowGet);
            }
            //Sys_UserRoleLink
            MD_User user = _mdUserService.LoadUserInfo(LoginName, Password);//_sysPersonService.LoadSysPersonInfo("Admin");
            //用户账号密码错误
            if (user == null)
            {
                ajaxResult.Message = Resources.Message.msg_LoginFailed;
                return Json(ajaxResult, JsonRequestBehavior.AllowGet);
            }
            //帐号被锁
            if (user.IsLock)
            {
                ajaxResult.Message = Resources.Message.msg_LoginLocked;
                return Json(ajaxResult, JsonRequestBehavior.AllowGet);
            }
            MD_Person person = _mdUserService.LoadPerson(user.UserCode);

            Sys_UserRoleLink resultUserRole = user.Sys_UserRoleLink.Where(u => u.UserCode == user.UserCode).FirstOrDefault();
            //用户没有权限
            if (resultUserRole == null)
            {
                ajaxResult.Message = Resources.Message.msg_NoRole;
                return Json(ajaxResult, JsonRequestBehavior.AllowGet);
            }

            UserInfo userInfo = new UserInfo();
            userInfo.LoginName = user.UserCode;
            userInfo.FirstName = person.FirstName;
            userInfo.LastName = person.LastName;
            userInfo.RoleID = resultUserRole.RoleID;
            userInfo.UserName = person.LastName + ' ' + person.FirstName;
            userInfo.OrgId = person.OrgId;
            Session.Add("UserInfo", userInfo);
            ajaxResult.Success = true;
            
            return Json(ajaxResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateSecurityCode()
        {
            ValidateCode code = new ValidateCode();
            string newCode = code.CreateValidateCode(6);
            Session["SecurityCode"] = newCode;
            Byte[] image = code.CreateValidateGraphic(newCode);
            return File(image, "image/jpeg");
        }

    }
}
