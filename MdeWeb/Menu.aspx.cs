using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace DatapoolWeb
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected string LoadMenuItem()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("    <script language=\"JavaScript\">");
            builder.Append(" function additemsto() {");
            builder.Append("");

            DataTable dt = null;
            dt = new RoleManager().GetUserRole(MyStateManager.UserID);
            string TempModule = string.Empty;
            string Module = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                Module = row["MODULE_NAME"].ToString();
                if (Module != TempModule)
                {
                    builder.Append(string.Format("t=outlookbar.addtitle('{0}','{1}');", row["MODULE_NAME"], row["MODULE_ICON"]));
                    builder.Append(string.Format("outlookbar.additem('{0}',t,'{1}');", row["ITEM_NAME"], row["ITEM_LINK"]));
                    TempModule = Module;
                }
                else
                {
                    builder.Append(string.Format("outlookbar.additem('{0}',t,'{1}');", row["ITEM_NAME"], row["ITEM_LINK"]));
                }
            }

            builder.Append("}    </script>");

            return builder.ToString();

        }

        protected string LoadTopMenu()
        {
            DataTable dt = null;
            dt = new RoleManager().GetUserRole(MyStateManager.UserID);
            string TempModule = string.Empty;

            if (dt.Rows.Count == 0)
            {
                return "<script>alert('未给该户分配角色')</script>";
            }
            else
                return "<script type=\"text/javascript\" language=\"javascript\">additemsto();locatefold(\"\");outlookbar.show();</script>";
        }
    }
}