using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datapool.BLL;
using Datapool.IBLL;
using Datapool.Model.DataModel;
using System.Security.Cryptography;
using Datapool.Model;

namespace DatapoolWeb.View.Users
{
    public partial class SysUserEdit : BasePage
    {
        #region Page event
        IUserService userService = new UserService();
        IPersonService personService = new PersonService();
        IGroupService groupService = new GroupService();
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPersons();
                BindGroups();

                string queryID = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(queryID))
                {
                    var userInfo = userService.LoadUserInfoWithID(Convert.ToDecimal(queryID));
                    SetModelToForm(userInfo);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (userService.CheckPersonIDInUser(string.IsNullOrEmpty(hidID.Value) ? (decimal)-1 : Convert.ToDecimal(hidID.Value), ddlPersons.SelectedValue))
                {
                    ShowMessage("该关联用户已经被选取， 请重新选择");
                    return;
                }

                // 保存用户数据
                SaveSysUser();


                Response.Redirect("sysuserview.aspx?status=u");
            }
            catch (Exception exp)
            {
                ShowMessage("保存时发生异常:" + exp.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPersons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlPersons.SelectedValue))
                txtCode.Text = personService.LoadEntities(x => x.ID == ddlPersons.SelectedValue).FirstOrDefault().ACCOUNT_CODE;
        }

        #region Custom method
        void BindPersons()
        {
            if (string.IsNullOrEmpty(Request.QueryString["id"]))
                ddlPersons.DataSource = personService.LoadPersonWithOutSysUser();
            else
                ddlPersons.DataSource = personService.LoadEntities(x => true).OrderBy(x => x.ACCOUNT_CODE);
            ddlPersons.DataTextField = "NAME";
            ddlPersons.DataValueField = "ID";
            ddlPersons.DataBind();

            ddlPersons.Items.Insert(0, new ListItem("", ""));
        }

        void BindGroups()
        {
            ddlGroup.DataSource = groupService.LoadEntities(x => x.IsActive.HasValue && x.IsActive.Value);
            ddlGroup.DataTextField = "CODE";
            ddlGroup.DataValueField = "ID";
            ddlGroup.DataBind();
        }

        /// <summary>
        /// 保存系统用户
        /// </summary>
        private void SaveSysUser()
        {
            SYS_USERS user = new SYS_USERS();
            user.PERSON_ID = ddlPersons.SelectedValue;
            user.CODE = personService.LoadEntities(x => x.ID == user.PERSON_ID).FirstOrDefault().ACCOUNT_CODE;
            user.REC_TIME_STAMP = DateTime.Now;
            user.REC_USER_CODE = "system";
            user.VALID = chkValid.Checked ? "1" : "0";
            user.DOMAIN_ID = 1;
            user.FORCE_EXPORT_ALL = "N";
            user.FAILLOGINCOUNT = 0;
            user.GROUP_ID = Convert.ToDecimal(ddlGroup.SelectedValue);
            user.PASSWORD = GetMd5Hash(txtPwd.Text.Trim()).ToUpper();

            //更新的情况
            if (!string.IsNullOrEmpty(hidID.Value))
            {
                user.ID = Convert.ToDecimal(hidID.Value);
                //如果密码没有填写， 就使用原密码
                if (string.IsNullOrEmpty(txtPwd.Text))
                    user.PASSWORD = hidPwd.Value;
            }


            //更新或添加
            if (string.IsNullOrEmpty(Request.QueryString["id"])) userService.AddEntities(user);
            else
            {
                SYS_USERS tmpUser = userService.LoadEntities(x => x.ID == user.ID).FirstOrDefault();
                tmpUser.PERSON_ID = ddlPersons.SelectedValue;
                //tmpUser.CODE = txtCode.Text.Trim();

                tmpUser.REC_TIME_STAMP = DateTime.Now;
                tmpUser.REC_USER_CODE = "system";
                tmpUser.VALID = chkValid.Checked ? "1" : "0";
                tmpUser.DOMAIN_ID = 1;
                tmpUser.FORCE_EXPORT_ALL = "N";
                tmpUser.FAILLOGINCOUNT = 0;
                tmpUser.GROUP_ID = Convert.ToDecimal(ddlGroup.SelectedValue);
                tmpUser.PASSWORD = GetMd5Hash(txtPwd.Text.Trim()).ToUpper();

                userService.UpdateEntities(tmpUser);
            }
        }

        void SetModelToForm(UserView view)
        {
            ddlPersons.SelectedValue = view.PERSON_ID.ToString();
            hidID.Value = view.ID.ToString();
            hidPwd.Value = view.PASSWORD;
            txtCode.Text = view.CODE;
            ddlGroup.SelectedValue = view.GROUP_ID.ToString();

            ddlPersons.Enabled = false;
        }

        string GetMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        bool verifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(input);

            // Create a StringComparer an comare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}