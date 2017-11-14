using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DatapoolWeb
{
    public partial class Head : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblName.Text = UserStateInfo.Name;
            //lblDomain.Text = UserStateInfo.GroupName;

            if (!IsPostBack)
            {
                this.LoginOut.Attributes.Add("onclick", "if(window.confirm('你确定要注销出系统吗？')) {window.top.location.replace('login.aspx');} else return false; ");
            }
        }
    }
}