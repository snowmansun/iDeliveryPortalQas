using Datapool.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DatapoolWeb.View.Users
{
    public partial class ChangePassword : BasePage
    {
        Datapool.IBLL.IUserService userService = new Datapool.BLL.UserService();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string oldpwd = txtOldPwd.Value.Trim();
            string newpwd = txtNewPwd.Value.Trim();
            string newconfirmpwd = txtConfirmPwd.Value.Trim();

            if (newpwd.Length < 6 || newconfirmpwd.Length < 6)
            {
                ShowMessage("修改失败,密码长度不能小于6");
                return;
            }
            else if (newpwd != newconfirmpwd)
            {
                ShowMessage("确认密码不一致，请重新修改");
                return;
            }

            //var userInfo = userService.LoadUserInfoWithID(base.UserStateInfo.UserID);
            if (userService.IsLoginSuccess(UserStateInfo.Code, oldpwd) != null)
            {
                //登陆成功修改密码
                var userinfo = userService.LoadEntities(x => x.ID == UserStateInfo.UserID).First();
                userinfo.PASSWORD = Encrypt.MD5Encrypt(newpwd);
                userService.UpdateEntities(userinfo);
                ShowMessage("密码修改成功");
            }
            else
            {
                ShowMessage("原始密码验证失败， 请重新输入");
            }
            //ShowMessage("config:" + txtConfirmPwd.Text);
            //Response.Write(txtConfirmPwd.Text
        }
    }
}
