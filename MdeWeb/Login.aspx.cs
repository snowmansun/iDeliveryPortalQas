using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datapool.BLL;
using Datapool.IBLL;
using Datapool.Model;
using Datapool.Model.DataModel;
using Datapool.Utility;

namespace DatapoolWeb
{
    public partial class Login : BasePage
    {
        //实例化需要的对象
        IUserService _userService = new UserService();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //用户名
            string userName = txtUserName.Value.Trim().Replace("'", "");

            //密码
            string password = txtPassword.Value.Trim().Replace("'", "");

            SYS_USERS sysUser = _userService.IsLoginSuccess(userName, password);
            if (sysUser != null)
            {
                if (sysUser.PASSWORD != Encrypt.MD5Encrypt(password)) ShowMessage("密码不正确！");
                else
                {
                    //赋值全局信息
                    var user = _userService.LoadUserInfo(new GetModelQuery() { con1 = "", con2 = userName }).First();
                    UserStateInfo.Name = user.PERSON_NM;
                    UserStateInfo.UserID = user.ID;
                    UserStateInfo.Code = user.CODE;
                    UserStateInfo.PersonID = user.PERSON_ID;
                    UserStateInfo.Position = user.PERSON_NM;
                    UserStateInfo.DomainID = 1;
                    UserStateInfo.GroupID = user.GROUP_ID.Value;
                    UserStateInfo.GroupName = user.GROUP_NM;
                    Response.Redirect(System.Web.Security.FormsAuthentication.GetRedirectUrl(userName, true));
                }
            }
            else
            {
                ShowMessage("用户名不存在！");
            }
        }

        //protected void ddlLanguage_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(ddlLanguage.SelectedValue))
        //    {
        //        MyStateManager.Language = ddlLanguage.SelectedValue;
        //    }
        //    Response.Redirect(Request.Url.AbsolutePath);
        //}
    }
}