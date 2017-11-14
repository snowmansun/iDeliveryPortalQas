using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datapool.BLL;
using Datapool.IBLL;
using Datapool.Model.DataModel;

namespace DatapoolWeb.View.Users
{
    public partial class SysUserGroupEdit : BasePage
    {
        #region 页面事件
        IGroupService groupService = new GroupService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveGroup();
                ShowAndRedirect(this, "保存成功", "sysusergroupview.aspx?status=u");
            }
            catch (Exception exp)
            {
                ShowMessage("提交数据发生异常：" + exp.Message);
            }
        }
        #endregion



        #region 自定义方法
        /// <summary>
        /// 初始化页面数据
        /// </summary>
        private void InitData()
        {
            string id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                SYS_USER_GROUPS info = groupService.LoadEntities(x => x.ID == Convert.ToDecimal(id)).First();
                SetModelToForm(info);
            }
        }

        void SetModelToForm(SYS_USER_GROUPS group)
        {
            hidID.Value = group.ID.ToString();
            txtCODE.Text = group.CODE;
            chkValid.Checked = group.IsActive.HasValue ? group.IsActive.Value : false;
        }

        SYS_USER_GROUPS SetFormToModel()
        {
            SYS_USER_GROUPS group = new SYS_USER_GROUPS();
            if (!string.IsNullOrEmpty(hidID.Value)) group.ID = Convert.ToDecimal(hidID.Value);
            group.CODE = txtCODE.Text.Trim();
            group.DOMAIN_ID = 1;
            group.IsActive = chkValid.Checked;
            group.REC_TIME_STAMP = System.DateTime.Now;
            group.REC_USER_CODE = UserStateInfo.UserID.ToString();
            return group;
        }

        void SaveGroup()
        {
            SYS_USER_GROUPS group = SetFormToModel();

            if (string.IsNullOrEmpty(Request.QueryString["id"]))
                groupService.AddEntities(group);
            else
                groupService.UpdateEntities(group);
        }

        #endregion
    }
}