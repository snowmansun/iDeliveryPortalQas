using Datapool.BLL;
using Datapool.IBLL;
using Datapool.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datapool.Model;

namespace DatapoolWeb.View.Users
{
    public partial class SysUserView : BasePage
    {

        #region Page Event

        IUserService userService = new UserService();
        string PAGE_QUERY = "UserQuery";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                InitData();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("sysuseredit.aspx");
        }

        protected void gvChannelType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUserList.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
            gvUserList.PageIndex = 0;
        }
        #endregion

        #region Custom Method
        void BindData()
        {
            GetModelQuery query = GetCondition();
            Session[PAGE_QUERY] = query;
            var data = userService.LoadUserInfo(query).ToList();
            gvUserList.DataSource = data;
            gvUserList.DataBind();
        }

        void InitData()
        {
            if (!string.IsNullOrEmpty(Request["status"]))
            {
                GetModelQuery conditions = Session[PAGE_QUERY] as GetModelQuery;
                if (conditions != null)
                {
                    SetCondition(conditions);
                    BindData();
                }
            }

        }

        GetModelQuery GetCondition()
        {
            string personName = txtPersonName.Text.Trim();
            string userCode = txtUserCode.Text.Trim();
            GetModelQuery query = new GetModelQuery() { con1 = personName, con2 = userCode };
            return query;
        }

        void SetCondition(GetModelQuery query)
        {
            txtPersonName.Text = query.con1;
            txtUserCode.Text = query.con2;
            gvUserList.PageIndex = query.pageIndex;
        }
        #endregion

    }
}