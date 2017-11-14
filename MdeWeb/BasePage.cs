using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datapool.BLL;
using Datapool.IBLL;
using Datapool.Model;
using Datapool.Model.DataModel;

namespace DatapoolWeb
{
    public class BasePage : Page
    {
        private UserState userStateWithCookie;
        public UserState UserStateInfo
        {
            get { return userStateWithCookie; }
        }

        public BasePage()
        {
            userStateWithCookie = new UserState();

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);   
        }

        /// <summary>
        /// 是否登录
        /// </summary>
        /// <returns></returns>
        protected void IsLogined()
        {
            if (string.IsNullOrEmpty(userStateWithCookie.Code))
            {
                //Response.Redirect("~/Account/Login.aspx");
                //AlertTrip("请先登录！");
                //Thread.Sleep(2000);

                //Response.Write("<script>parent.window.location.href('../index.aspx')</script>");

                //string message = "请先登录！";
                //string strMsg = message.Replace("\n", "\\n");
                //strMsg = strMsg.Replace("\r", "\\r");

                //string s = "art.dialog({content: '登录客户中心后才可申请！请先登录！',lock:true, disabled: true,ok: function () { parent.window.location.href = '../index.aspx';return false;}}); \n ";
                //ClientScript.RegisterStartupScript(this.GetType(), "", s, true); 

                Response.Redirect("~/Login.aspx");
            }
        }


        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="message"></param>
        protected void Alert(string message)
        {
            StringBuilder strBuilder = new StringBuilder();
            string strMsg = message.Replace("\n", "\\n");
            strMsg = strMsg.Replace("\r", "\\r");
            //strBuilder.Append("art.dialog({content: \"" + strMsg + "\"});");
            strBuilder.Append(" art.dialog.alert(\"" + strMsg + "\");");
            ScriptManager.RegisterStartupScript(this, typeof(Login), "Alert", strBuilder.ToString(), true);
        }

        /// <summary>
        /// 显示alert提示框
        /// </summary>
        /// <param name="message"></param>
        public void ShowMessage(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "", string.Format("alert(\"{0}\")", message.Replace("\"", "'")), true);
        }

        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public void ShowAndRedirect(System.Web.UI.Page page, string msg, string url)
        {
            System.Text.StringBuilder Builder = new System.Text.StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg.Replace("\"", "'"));
            Builder.AppendFormat("self.location.href='{0}'", url);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());

        }

        /// <summary>
        /// GRIDVIEW选择样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#F0CDAD',this.style.fontWeight='';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
        }

        /// <summary>
        /// 绑定城市下拉框
        /// </summary>
        public void BindCity(DropDownList ddl, bool allFlag, string regionCode, string cluCode)
        {
            ICityService _cityService = new CityService();
            ddl.DataSource = _cityService.LoadCityInfo(regionCode, cluCode);
            ddl.DataTextField = "CITY_NM_CN";
            ddl.DataValueField = "CITY_CODE";
            ddl.DataBind();
            if (allFlag) ddl.Items.Insert(0, new ListItem("--全部--", ""));
            else ddl.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// 绑定Cluster下拉框
        /// </summary>
        public void BindCluster(DropDownList ddl, bool allFlag, string regionCode)
        {
            IClusterService clusterService = new ClusterService();
            ddl.DataSource = clusterService.LoadClusterInfo(regionCode);
            ddl.DataTextField = "CLUSTER_NM_CN";
            ddl.DataValueField = "CLUSTER_CODE";
            ddl.DataBind();
            if (allFlag) ddl.Items.Insert(0, new ListItem("--全部--", ""));
            else ddl.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// 绑定区域下拉框
        /// </summary>
        public void BindRegion(DropDownList ddl, bool allFlag)
        {
            IRegionService regionService = new RegionService();
            ddl.DataSource = regionService.LoadRegionInfo();
            //ddl.DataSource = regionService.LoadEntities(p => p.REGION_CODE == regionCode);
            ddl.DataTextField = "REGION_NM_CN";
            ddl.DataValueField = "REGION_CODE";
            ddl.DataBind();
            if (allFlag) ddl.Items.Insert(0, new ListItem("--全部--", ""));
            else ddl.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// 绑定STORE_BANNER下拉框
        /// </summary>
        public void BindBannerByStore(DropDownList ddl, string cityCode, bool allFlag)
        {
            IBannerService bannerService = new BannerService();
            ddl.DataSource = bannerService.LoadBannerByStore(cityCode);
            ddl.DataTextField = "BANNER_NM_CN";
            ddl.DataValueField = "BANNER_CODE";
            ddl.DataBind();
            if (allFlag) ddl.Items.Insert(0, new ListItem("--全部--", ""));
            else ddl.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// 绑定BANNER下拉框
        /// </summary>
        public void BindBanner(DropDownList ddl, bool allFlag)
        {
            IBannerService bannerService = new BannerService();
            ddl.DataSource = bannerService.LoadBannerInfo();
            ddl.DataTextField = "BANNER_NM_CN";
            ddl.DataValueField = "BANNER_CODE";
            ddl.DataBind();
            if (allFlag) ddl.Items.Insert(0, new ListItem("--全部--", ""));
            else ddl.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// 绑定人员下拉框
        /// </summary>
        public void BindPerson(DropDownList ddl, bool allFlag)
        {
            IPersonService personService = new PersonService();
            ddl.DataSource = personService.LoadPersonInfo(new GetModelQuery());
            ddl.DataTextField = "NAME";
            ddl.DataValueField = "ID";
            ddl.DataBind();
            if (allFlag) ddl.Items.Insert(0, new ListItem("--全部--", ""));
            else ddl.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// 绑定Person下拉框
        /// </summary>
        public void BindPerson(DropDownList ddl, string id, string pid, bool allFlag)
        {
            IPersonService personService = new PersonService();
            if (id != string.Empty)
                ddl.DataSource = personService.LoadEntities(p => p.ID == id);
            else if (pid != string.Empty)
                ddl.DataSource = personService.LoadEntities(p => p.PAR_ID == id);
            ddl.DataTextField = "CODE_NM";
            ddl.DataValueField = "CODE_ID";
            ddl.DataBind();
            if (allFlag) ddl.Items.Insert(0, new ListItem("--全部--", ""));
            else ddl.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// 绑定经销商下拉框
        /// </summary>
        public void BindDistributor(DropDownList ddl, bool allFlag, string cityCode)
        {
            IDistributorService distributorService = new DistributorService();
            ddl.DataSource = distributorService.LoadDistributorInfo(cityCode);
            ddl.DataTextField = "NAME1";
            ddl.DataValueField = "KUNNR";
            ddl.DataBind();
            if (allFlag) ddl.Items.Insert(0, new ListItem("--全部--", ""));
            else ddl.Items.Insert(0, new ListItem("", ""));
        }

        /// <summary>
        /// 绑定BANNER GROUP下拉框
        /// </summary>
        public void BindBannerGroup(DropDownList ddl, bool allFlag)
        {
            IBannerGroupService bannerGroupService = new BannerGroupService();
            ddl.DataSource = bannerGroupService.LoadBannerGroupInfo();
            ddl.DataTextField = "BANNER_GROUP_NM";
            ddl.DataValueField = "BANNER_GROUP_CODE";
            ddl.DataBind();
            if (allFlag) ddl.Items.Insert(0, new ListItem("--全部--", ""));
            else ddl.Items.Insert(0, new ListItem("", ""));
        }

     

        /// <summary>
        /// 绑定CODE下拉框
        /// </summary>
        public void BindCode(DropDownList ddl, string type, bool allFlag)
        {
            ICodeService codeService = new CodeService();
            ddl.DataSource = codeService.LoadCodeInfo(type);
            ddl.DataTextField = "CODE_NM";
            ddl.DataValueField = "CODE_ID";
            ddl.DataBind();
            if (allFlag) ddl.Items.Insert(0, new ListItem("--全部--", ""));
            else ddl.Items.Insert(0, new ListItem("", ""));
        }
    }
}