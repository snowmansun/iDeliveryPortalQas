using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datapool.BLL;
using Datapool.IBLL;
using Datapool.Model.DataModel;

namespace DatapoolWeb.View.Store
{
    public partial class StoreEdit : BasePage
    {
        #region Field
        //实例化需要的对象
        private IStoreService _storeService = new StoreService();
        private IRegionService _regionService = new RegionService();
        private ICityService _cityService = new CityService();
        private IBannerService _bannerService = new BannerService();
        private IBannerGroupService _bannerGroupService = new BannerGroupService();
        private ICodeService _codeService = new CodeService();
        private IPersonService _personService = new PersonService();
        private IDistributorService _distributorService = new DistributorService();
        private ISeqService _seqService = new SeqService();
        #endregion

        #region 初始化
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // 初始化页面数据
                InitData();
            }
        }

        /// <summary>
        /// 初始化页面数据
        /// </summary>
        private void InitData()
        {
            //
            BindBanner(ddlBanner, false);

            //
            BindDistributor(ddlDistributor, false, "");

            // 
            //BindBannerGroup(ddlBannerGroup, false);

            // 绑定TYPE
            BindCode(ddlType, "1", false);

            // 绑定CHANNEL
            BindCode(ddlChannel, "2", false);

            // 绑定Delivery
            BindCode(ddlDelivery, "3", false);

            //// 绑定Area
            //BindCode(ddlArea, "4", false);

            // 绑定Ka
            BindCode(ddlNonKA, "5", false);

            // 绑定Status
            BindCode(ddlStatus, "6", false);

            // 绑定SRType
            BindCode(ddlSRType, "7", false);
          
            // 管理员
            if (UserStateInfo.GroupID == 1)
            {
                BindCity(ddlCity, false, string.Empty, string.Empty);
                BindPerson(ddlDSM, false);
                BindPerson(ddlSR, false);
            }
            else 
            {
                //  
                Competence();
            }
            
            // 更新
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                SetForm(Request["id"]);
            }
        }
        #endregion

        #region 画面事件
        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlCity.Items.Clear();
            //if (ddlProvince.SelectedValue != string.Empty)
            //{
            //    ddlCity.DataSource = new RegionManager().GetRegion(MyStateManager.DomainID, decimal.Parse(ddlProvince.SelectedValue));
            //    ddlCity.DataTextField = "NAME";
            //    ddlCity.DataValueField = "ID";
            //    ddlCity.DataBind();
            //}
            //ddlCity.Items.Insert(0, new ListItem(Resources.language.ddl_Select, ""));

        }

        protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlProvince.Items.Clear();
            //if (ddlRegion.SelectedValue != string.Empty)
            //{
            //    ddlProvince.DataSource = new RegionManager().GetRegion(MyStateManager.DomainID, decimal.Parse(ddlRegion.SelectedValue));
            //    ddlProvince.DataTextField = "NAME";
            //    ddlProvince.DataValueField = "ID";
            //    ddlProvince.DataBind();
            //}
            //ddlProvince.Items.Insert(0, new ListItem(Resources.language.ddl_Select, ""));
            //this.ddlProvince_SelectedIndexChanged(null, EventArgs.Empty);

        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 保存数据
                SaveStore();
                ShowAndRedirect(this, "保存成功", "StoreList1.aspx?status=u");
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }

        //protected void txtOrg_TextChanged(object sender, EventArgs e)
        //{
        //    InitCustomerProduct();
        //}
        #endregion

        #region 自定义方法

        /// <summary>
        /// 页面赋值
        /// </summary>
        private void SetForm(string storeCode)
        {
            //BindPerson(ddlSR, string.Empty, ddlDSM.SelectedValue, true);

            MASTER_STORE store = _storeService.LoadStoreInfoByCode(storeCode);
            txtAddress.Text = store.ADDRESS;
            chkAff.Checked = store.AFFILIATE.Value;
            txtArea.Text = store.AREA;
            //ddlArea.SelectedValue = store.AREA;
            ddlBanner.SelectedValue = store.BANNER_CODE;
            //ddlBannerGroup.SelectedValue = store.BANNER_GROUP_CODE;
            ddlChannel.SelectedValue = store.CHANNEL;
            ddlCity.SelectedValue = store.CITY_CODE;
            txtContactPerson.Text = store.CONTACT_PERSON;
            ddlDelivery.SelectedValue = store.DELIVERY;
            ddlDistributor.SelectedValue = store.DISTRIBUTOR_CODE;
            if (store.DISTRIBUTOR_CODE_2 != null) chkDistributor2.Checked = store.DISTRIBUTOR_CODE_2.Value;
            txtDistrict.Text = store.DISTRICT;
            ddlDSM.SelectedValue = store.DSM;
            ddlNonKA.SelectedValue = store.KA;
            if (store.REFRIGERATOR != null) chkRef.Checked = store.REFRIGERATOR.Value;
            ddlSR.SelectedValue = store.SR;
            //txtSRCode.Text = store.SR_CODE;
            ddlSRType.SelectedValue = store.SR_TYPE;
            ddlStatus.SelectedValue = store.STATUS;
            //txtStoreCode.Text = store.STORE_CODE;
            txtStoreNm.Text = store.STORE_NAME;
            txtTelNo.Text = store.TEL_NO;
            ddlType.SelectedValue = store.TYPE;
        }

        /// <summary>
        /// 保存门店信息
        /// </summary>
        private void SaveStore()
        {
            MASTER_STORE store = new MASTER_STORE();
            store.ADDRESS = txtAddress.Text;
            store.AFFILIATE = chkAff.Checked;
            store.AREA = txtArea.Text;
            store.BANNER_CODE = ddlBanner.SelectedValue;
            //store.BANNER_GROUP_CODE = ddlBannerGroup.SelectedValue;
            store.CHANNEL = ddlChannel.SelectedValue;
            store.CITY_CODE = ddlCity.SelectedValue;
            store.CODE = "";
            store.CONTACT_PERSON = txtContactPerson.Text;
            store.DELIVERY = ddlDelivery.SelectedValue;
            store.DISTRIBUTOR_CODE = ddlDistributor.SelectedValue;
            store.DISTRIBUTOR_CODE_2 = chkDistributor2.Checked;
            store.DISTRICT = txtDistrict.Text;
            store.DSM = ddlDSM.SelectedValue;
            store.KA = ddlNonKA.SelectedValue;
            store.REFRIGERATOR = chkRef.Checked;
            store.SR = ddlSR.SelectedValue;
            //store.SR_CODE = txtSRCode.Text;
            store.SR_TYPE = ddlSRType.SelectedValue;
            store.STATUS = ddlStatus.SelectedValue;
         
            store.STORE_CODE_SHORT = "";
            store.STORE_NAME = txtStoreNm.Text;
            store.TEL_NO = txtTelNo.Text;
            store.TYPE = ddlType.SelectedValue;

            // 新增
            if (string.IsNullOrEmpty(Request["id"]))
            {
                bool flag = true;
                while (flag)
                {
                    string seqId = _seqService.GetSeq("Store");
                    store.STORE_CODE = _cityService.LoadEntities(p => p.CITY_CODE == store.CITY_CODE).FirstOrDefault().CITY_CODE_ABB +
                                        store.BANNER_CODE + seqId;

                    if (_storeService.LoadEntities(p => p.STORE_CODE == store.STORE_CODE).FirstOrDefault() == null) flag = false;
                }

                store.CREATE_DATE = System.DateTime.Now;
                store.CREATE_USER = UserStateInfo.PersonID;
                _storeService.AddEntities(store);
            }
            // 更新
            else
            {
                store.STORE_CODE = Request["id"];
                store.UPDATE_DATE = System.DateTime.Now;
                store.UPDATE_USER = UserStateInfo.PersonID;
                _storeService.UpdateEntities(store);
            }
        }

        /// <summary>
        /// 权限数据管理
        /// </summary>
        private void Competence()
        {
            // 保存按钮控制
            if (UserStateInfo.GroupID == 4 ||
                UserStateInfo.GroupID == 5 ||
                UserStateInfo.GroupID == 6)
            {
                this.btnSave.Visible = true;

                // 组织架构
                IPersonService personService = new PersonService();
                List<PERSON_MANAGE> personManageList = personService.LoadPersonManage(UserStateInfo.PersonID).ToList();
                if (personManageList.Count == 1)
                {
                    PERSON_MANAGE personManage = personManageList[0];

                    if (personManage.MANAGE_LEAVE == 3)
                    {
                        ICityService _cityService = new CityService();
                        List<MASTER_CITY> cityList = _cityService.LoadEntities(p => p.CITY_CODE.Contains(personManage.MANAGE_AREA) && p.CITY_CODE_ABB != null).ToList();
                        ddlCity.DataSource = cityList;
                        ddlCity.DataTextField = "CITY_NM_CN";
                        ddlCity.DataValueField = "CITY_CODE";
                        ddlCity.DataBind();
                        ddlCity.Items.Insert(0, new ListItem("", ""));

                        ddlSR.DataSource = personService.LoadEntities(x => x.CITY_CODE == cityList[0].CITY_CODE && x.POSITION == "SR");
                        ddlSR.DataTextField = "NAME";
                        ddlSR.DataValueField = "ID";
                        ddlSR.DataBind();
                        ddlSR.Items.Insert(0, new ListItem("", ""));

                        string cluster = _cityService.LoadEntities(x => x.CITY_CODE == cityList[0].CITY_CODE).FirstOrDefault().CLUSTER_CODE;

                        List<MASTER_CITY> cityListS = _cityService.LoadEntities(x => x.CLUSTER_CODE == cluster).ToList();
                        string[] cityS = new string[cityListS.Count];
                        int i = 0;
                        foreach (MASTER_CITY mc in cityListS)
                        {
                            cityS[i] = mc.CITY_CODE;
                            i++;
                            //manageArea += pm.MANAGE_AREA + ",";
                        }

                        List<MASTER_PERSON> personForDsm = personService.LoadEntities(x => true && x.POSITION == "DSM").ToList();
                        var personListForDSM = (from a in personForDsm
                                                where (cityS).Contains(a.CITY_CODE)
                                                select a).ToList();
                        ddlDSM.DataSource = personListForDSM;
                        ddlDSM.DataTextField = "NAME";
                        ddlDSM.DataValueField = "ID";
                        ddlDSM.DataBind();
                        ddlDSM.Items.Insert(0, new ListItem("", ""));
                    }
                }
                else
                {
                    // 管辖城市
                    PERSON_MANAGE personManage = personManageList[0];
                    string[] manageArea = new string[personManageList.Count];

                    int i = 0;
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
                    ddlCity.Items.Insert(0, new ListItem("", ""));

                    // 人员
                    List<MASTER_PERSON> personForSr = personService.LoadEntities(x => true && x.POSITION == "SR").ToList();
                    var personListForSr = (from a in personForSr
                                           where (manageArea).Contains(a.CITY_CODE)
                                           select a).ToList();

                    ddlSR.DataSource = personListForSr;
                    ddlSR.DataTextField = "NAME";
                    ddlSR.DataValueField = "ID";
                    ddlSR.DataBind();
                    ddlSR.Items.Insert(0, new ListItem("", ""));

                    string cluster = _cityService.LoadEntities(x => x.CITY_CODE == cityList[0].CITY_CODE).FirstOrDefault().CLUSTER_CODE;

                    List<MASTER_CITY> cityListS = _cityService.LoadEntities(x => x.CLUSTER_CODE == cluster).ToList();
                    string[] cityS = new string[cityListS.Count];
                    int j = 0;
                    foreach (MASTER_CITY mc in cityListS)
                    {
                        cityS[j] = mc.CITY_CODE;
                        j++;
                        //manageArea += pm.MANAGE_AREA + ",";
                    }

                    List<MASTER_PERSON> personForDsm = personService.LoadEntities(x => true && x.POSITION == "DSM").ToList();
                    var personListForDSM = (from a in personForDsm
                                            where (cityS).Contains(a.CITY_CODE)
                                            select a).ToList();

                    ddlDSM.DataSource = personListForDSM;
                    ddlDSM.DataTextField = "NAME";
                    ddlDSM.DataValueField = "ID";
                    ddlDSM.DataBind();
                    ddlDSM.Items.Insert(0, new ListItem("", ""));
                }

                // 更新
                if (!string.IsNullOrEmpty(Request["id"]))
                {
                    // 
                    BindCity(ddlCity, false, string.Empty, string.Empty);

                    SetForm(Request["id"]);

                    ///**  控件状态管理 **/
                    //if (UserStateInfo.GroupID == 4)
                    //{
                    //    ddlSR.Enabled = true;
                    //    ddlDSM.Enabled = true;
                    //}
                    //else
                    //{
                    //    ddlSR.Enabled = false;
                    //    ddlDSM.Enabled = false;
                    //}
                    ddlBanner.Enabled = false;
                    ddlCity.Enabled = false;
                }
                // 新增
                else
                { }
            }
            else
            {
                this.btnSave.Visible = false;

                BindPerson(ddlDSM, false);
                BindPerson(ddlSR, false);
                BindCity(ddlCity, false, string.Empty, string.Empty);
            }
        }
        #endregion
    }
}