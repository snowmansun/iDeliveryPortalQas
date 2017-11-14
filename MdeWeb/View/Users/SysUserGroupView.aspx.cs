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

namespace DatapoolWeb.View.Users
{
    public partial class SysUserGroupView : BasePage
    {
        #region 页面事件
        IGroupService groupService = new GroupService();
        string PAGE_QUERY = "GroupQuery";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) InitData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
            gvGroupList.PageIndex = 0;
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("sysusergroupedit.aspx");
        }

        protected void gvChannelType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvGroupList.PageIndex = e.NewPageIndex;
            BindData();
        }
        #endregion

        #region 自定义方法
        void BindData()
        {
            GetModelQuery query = new GetModelQuery();
            query.con1 = txtCode.Text.Trim();
            Session[PAGE_QUERY] = query;
            var data = groupService.LoadGroupInfo(query);
            gvGroupList.DataSource = data.ToList();
            gvGroupList.DataBind();
        }

        private void InitData()
        {
            if (!string.IsNullOrEmpty(Request["status"]))
            {
                GetModelQuery conditions = GetCondition();
                conditions = (GetModelQuery)Session[PAGE_QUERY];
                //
                SetCondition(conditions);
                //
                BindData();
            }
        }

        /// <summary>
        /// 查询参数
        /// </summary>
        /// <returns></returns>
        private GetModelQuery GetCondition()
        {
            GetModelQuery condition = new GetModelQuery();
            condition.con1 = txtCode.Text.Trim();
            condition.pageIndex = this.gvGroupList.PageIndex;
            return condition;
        }

        /// <summary>
        /// 设置查询条件 
        /// </summary>
        /// <param name="condition"></param>
        private void SetCondition(GetModelQuery condition)
        {
            txtCode.Text = condition.con1;
            this.gvGroupList.PageIndex = condition.pageIndex;
        }
        #endregion

    }
}