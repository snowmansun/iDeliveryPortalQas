using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DatapoolWeb.Account
{
    public partial class ChangePassword : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ShowMessage("config:" + txtConfirmPwd.Text);
            //Response.Write(txtConfirmPwd.Text
        }
    }
}
