using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datapool.BLL;
using Datapool.IBLL;
using Datapool.Model;
using Datapool.Model.DataModel;
using Datapool.Utility;

namespace DatapoolWeb.View.Store
{
    public partial class StoreList1 : BasePage
    {
        #region Field
        //实例化需要的对象
        private IStoreService _storeService = new StoreService();
        private IRegionService _regionService = new RegionService();
        private ICityService _cityService = new CityService();
        private IBannerService _bannerService = new BannerService();
        private ICodeService _codeService = new CodeService();
        private IPersonService _personService = new PersonService();
        #endregion

        #region 画面事件
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 
                InitData();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GridViewToolbarUserControl1_OnGVTBSetColumnsButtonEvent(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 查询按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            // 
            BindData();
            this.gvStore.PageIndex = 0;
        }

        /// <summary>
        /// 导出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            gvStore.AllowPaging = false;
            BindData();
            CommonHelp.ConvertToExcel(gvStore, "UTF-8", "门店数据.xls");

            gvStore.AllowPaging = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {          
            BindCluster(ddlCluster, true, ddlRegion.SelectedValue);
            BindCity(ddlCity, true, ddlRegion.SelectedValue, ddlCluster.SelectedValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCluster_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCity(ddlCity, true, ddlRegion.SelectedValue, ddlCluster.SelectedValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBannerByStore(ddlBanner, ddlCity.SelectedValue, true);
        }

         /// <summary>
        /// 翻页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = (GridView)sender;
            int newPageIndex = 0;
            if (-2 == e.NewPageIndex)
            {
                TextBox tb = null;
                GridViewRow gvr = gv.BottomPagerRow;
                if (gvr != null)
                {
                    tb = (TextBox)gvr.FindControl("txtNum");

                }
                if (tb != null)
                {
                    newPageIndex = int.Parse(tb.Text) - 1;
                }
            }
            else
            {
                newPageIndex = e.NewPageIndex;
            }
            newPageIndex = newPageIndex <= 0 ? 0 : newPageIndex;
            this.gvStore.PageIndex = newPageIndex;
            //分页后重新绑定数据源
            this.BindData();
        }

        #endregion

        #region 自定义方法

        /// <summary>
        /// 查询参数
        /// </summary>
        /// <returns></returns>
        private GetModelQuery GetCondition()
        {
            GetModelQuery condition = new GetModelQuery();
            condition.con1 = txtStoreCode.Text.Trim();
            condition.con2 = txtStoreName.Text.Trim();
            condition.con3 = ddlChannal.SelectedValue;
            condition.con4 = ddlRegion.SelectedValue;
            condition.con5 = ddlCity.SelectedValue;
            condition.con6 = txtSR.Text.Trim();
            //condition.con7 = ddlBannerGroupCode.SelectedValue;
            condition.con8 = ddlBanner.SelectedValue;
            condition.con9 = txtDSM.Text.Trim();
            condition.con10 = ddlDistributor.Text.Trim();
            //condition.con11 = txtDistributor2.Text.Trim();
            condition.con12 = ddlArea.SelectedValue;
            condition.con13 = ddlNonKA.SelectedValue;
            condition.con14 = ddlStatus.SelectedValue;
            condition.con15 = ddlDelivery.SelectedValue;
            condition.con16 = ddlCluster.SelectedValue;
            condition.pageIndex = this.gvStore.PageIndex;
            condition.GroupId = UserStateInfo.GroupID;
            return condition;
        }

        /// <summary>
        /// 设置查询条件 
        /// </summary>
        /// <param name="condition"></param>
        private void SetCondition(GetModelQuery condition)
        {
            txtStoreCode.Text = condition.con1;
            txtStoreName.Text = condition.con2;
            ddlChannal.SelectedValue = condition.con3;
            ddlRegion.SelectedValue = condition.con4;
            ddlCity.SelectedValue = condition.con5;
            txtSR.Text = condition.con6;
            //ddlBannerGroupCode.SelectedValue = condition.con7;
            ddlBanner.SelectedValue = condition.con8;
            txtDSM.Text = condition.con9;
            ddlDistributor.Text = condition.con10;
            //txtDistributor2.Text = condition.con11;
            ddlArea.SelectedValue = condition.con12;
            ddlNonKA.SelectedValue = condition.con13;
            ddlStatus.SelectedValue = condition.con14;
            ddlDelivery.SelectedValue = condition.con15;
            ddlCluster.SelectedValue = condition.con16;
            this.gvStore.PageIndex = condition.pageIndex;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            //ddlCity.DataSource = _cityService.LoadCityInfo("");
            //ddlCity.DataTextField = "CITY_NM_CN";
            //ddlCity.DataValueField = "CITY_CODE";
            //ddlCity.DataBind();

            // 系统管理员
            if (UserStateInfo.GroupID == 1)
            {
                BindRegion(ddlRegion, true);

                BindCluster(ddlCluster, true, ddlRegion.SelectedValue);

                BindCity(ddlCity, true, ddlRegion.SelectedValue, ddlCluster.SelectedValue);
            }
            else
            {
                // 权限管理
                Competence();
            }

            //
            BindDistributor(ddlDistributor, true, string.Empty);

            //
            BindBanner(ddlBanner, true);
            // BindBannerByStore(ddlBanner, ddlCity.SelectedValue, true);
            //
            //BindBannerGroup(ddlBannerGroupCode, true);

            //
            //BindPerson(ddlPerson, true);

            // 绑定CHANNEL
            BindCode(ddlChannal, "2", true);

            // 绑定Delivery
            BindCode(ddlDelivery, "3", true);

            // 绑定Area
            BindCode(ddlArea, "4", true);

            // 绑定Ka
            BindCode(ddlNonKA, "5", true);

            // 绑定Status
            BindCode(ddlStatus, "6", true);

            if (!string.IsNullOrEmpty(Request["status"]))
            {
                GetModelQuery conditions = (GetModelQuery)Session["StoreQuery"];
                if (conditions != null)
                {
                    //
                    SetCondition(conditions);
                    //
                    BindData();
                }
            }

            //ddlCity.Items.Insert(0, new ListItem("--所有--", ""));
            //ddlRegion.Items.Insert(0, new ListItem("--所有--", ""));
            //ddlBanner.Items.Insert(0, new ListItem("--所有--", ""));
            //ddlPerson.Items.Insert(0, new ListItem("--所有--", ""));
            //ddlChannal.Items.Insert(0, new ListItem("--所有--", ""));
            //ddlType.Items.Insert(0, new ListItem("--所有--", ""));
            //ddlDelivery.Items.Insert(0, new ListItem("--所有--", ""));

            //ddlRegion.DataSource = new RegionManager().GetRegion(MyStateManager.DomainID, 0);
            //ddlRegion.DataTextField = "NAME";
            //ddlRegion.DataValueField = "ID";
            //ddlRegion.DataBind();
            //ddlRegion.Items.Insert(0, new ListItem(Resources.language.ddl_All, ""));

            //this.ddlRegion_SelectedIndexChanged(null, EventArgs.Empty);
        }

        /// <summary>
        /// 权限数据管理
        /// </summary>
        private void Competence()
        {
            // 新增按钮控制
            if (UserStateInfo.GroupID == 4 ||
                UserStateInfo.GroupID == 5 ||
                UserStateInfo.GroupID == 6)
            {
                this.btnNew.Visible = true;
            }
            else
            {
                this.btnNew.Visible = false;
            }

            // 组织架构
            IPersonService personService = new PersonService();
            List<PERSON_MANAGE> personManageList = personService.LoadPersonManage(UserStateInfo.PersonID).ToList();
            if (personManageList.Count == 1)
            {
                PERSON_MANAGE personManage = personManageList[0];
                if (personManage.MANAGE_LEAVE == 1)
                {
                    IRegionService regionService = new RegionService();
                    ddlRegion.DataSource = regionService.LoadEntities(p => p.REGION_CODE == personManage.MANAGE_AREA);
                    ddlRegion.DataTextField = "REGION_NM_CN";
                    ddlRegion.DataValueField = "REGION_CODE";
                    ddlRegion.DataBind();
                    //ddlRegion.Items.Insert(0, new ListItem("--全部--", ""));

                    BindCluster(ddlCluster, true, ddlRegion.SelectedValue);

                    BindCity(ddlCity, true, ddlRegion.SelectedValue, ddlCluster.SelectedValue);

                }
                else if (personManage.MANAGE_LEAVE == 2)
                {
                    IClusterService clusterService = new ClusterService();
                    ddlCluster.DataSource = clusterService.LoadEntities(p => p.CLUSTER_CODE == personManage.MANAGE_AREA);
                    ddlCluster.DataTextField = "CLUSTER_NM_CN";
                    ddlCluster.DataValueField = "CLUSTER_CODE";
                    ddlCluster.DataBind();
                    //ddlCluster.Items.Insert(0, new ListItem("--全部--", ""));

                    IRegionService regionService = new RegionService();
                    string regionCode = clusterService.LoadEntities(p => p.CLUSTER_CODE == personManage.MANAGE_AREA).FirstOrDefault().REGION_CODE;
                    ddlRegion.DataSource = regionService.LoadEntities(p => p.REGION_CODE == regionCode);
                    ddlRegion.DataTextField = "REGION_NM_CN";
                    ddlRegion.DataValueField = "REGION_CODE";
                    ddlRegion.DataBind();
                    //ddlRegion.Items.Insert(0, new ListItem("--全部--", ""));

                    BindCity(ddlCity, true, ddlRegion.SelectedValue, ddlCluster.SelectedValue);
                }
                if (personManage.MANAGE_LEAVE == 3)
                {
                    ICityService _cityService = new CityService();
                    ddlCity.DataSource = _cityService.LoadEntities(p => p.CITY_CODE.Contains(personManage.MANAGE_AREA) && p.CITY_CODE_ABB != null);
                    ddlCity.DataTextField = "CITY_NM_CN";
                    ddlCity.DataValueField = "CITY_CODE";
                    ddlCity.DataBind();
                    ddlCity.Items.Insert(0, new ListItem("--全部--", ""));

                    IClusterService clusterService = new ClusterService();
                    //MASTER_CITY mcity = _cityService.LoadEntities(p => p.CITY_CODE == personManage.MANAGE_AREA).FirstOrDefault();
                    string cluCode = _cityService.LoadEntities(p => p.CITY_CODE == personManage.MANAGE_AREA).FirstOrDefault().CLUSTER_CODE;
                    ddlCluster.DataSource = clusterService.LoadEntities(p => p.CLUSTER_CODE == cluCode);
                    ddlCluster.DataTextField = "CLUSTER_NM_CN";
                    ddlCluster.DataValueField = "CLUSTER_CODE";
                    ddlCluster.DataBind();
                    //ddlCluster.Items.Insert(0, new ListItem("--全部--", ""));


                    IRegionService regionService = new RegionService();
                    string regionCode = clusterService.LoadEntities(p => p.CLUSTER_CODE == cluCode).FirstOrDefault().REGION_CODE;
                    ddlRegion.DataSource = regionService.LoadEntities(p => p.REGION_CODE == regionCode);
                    ddlRegion.DataTextField = "REGION_NM_CN";
                    ddlRegion.DataValueField = "REGION_CODE";
                    ddlRegion.DataBind();
                    //ddlRegion.Items.Insert(0, new ListItem("--全部--", ""));
                }            
            }
            else
            {
                PERSON_MANAGE personManage = personManageList[0];
                string[] manageArea = new string[personManageList.Count];

                int i = 0 ;
                foreach (PERSON_MANAGE pm in personManageList)
                {
                    manageArea[i] = pm.MANAGE_AREA;
                    i++;
                    //manageArea += pm.MANAGE_AREA + ",";
                }
                //manageArea = manageArea.TrimEnd(',');
                
                ICityService _cityService = new CityService();
                var city = _cityService.LoadEntities(p => true);
                var cityList = (from a in city
                               where (manageArea).Contains(a.CITY_CODE)
                               select a).ToList();
                //ddlCity.DataSource = _cityService.LoadEntities(p => p.CITY_CODE.Contains(manageArea));
                ddlCity.DataSource = cityList;
                ddlCity.DataTextField = "CITY_NM_CN";
                ddlCity.DataValueField = "CITY_CODE";
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, new ListItem("--全部--", ""));

                IClusterService clusterService = new ClusterService();
                //MASTER_CITY mcity = _cityService.LoadEntities(p => p.CITY_CODE == personManage.MANAGE_AREA).FirstOrDefault();
                string cluCode = _cityService.LoadEntities(p => p.CITY_CODE == personManage.MANAGE_AREA).FirstOrDefault().CLUSTER_CODE;
                ddlCluster.DataSource = clusterService.LoadEntities(p => p.CLUSTER_CODE == cluCode);
                ddlCluster.DataTextField = "CLUSTER_NM_CN";
                ddlCluster.DataValueField = "CLUSTER_CODE";
                ddlCluster.DataBind();
                //ddlCluster.Items.Insert(0, new ListItem("--全部--", ""));

                IRegionService regionService = new RegionService();
                string regionCode = clusterService.LoadEntities(p => p.CLUSTER_CODE == cluCode).FirstOrDefault().REGION_CODE;
                ddlRegion.DataSource = regionService.LoadEntities(p => p.REGION_CODE == regionCode);
                ddlRegion.DataTextField = "REGION_NM_CN";
                ddlRegion.DataValueField = "REGION_CODE";
                ddlRegion.DataBind();
                //ddlRegion.Items.Insert(0, new ListItem("--全部--", ""));
            }
        }

        /// <summary>
        /// 查询经销商
        /// </summary>
        private void BindData()
        {
            GetModelQuery conditions = GetCondition();
            Session["StoreQuery"] = conditions;

            List<StoreView> storeList = _storeService.LoadStoreInfo(conditions).ToList();
            gvStore.DataSource = storeList;
            gvStore.DataBind();
        }
        #endregion

     
    }
}