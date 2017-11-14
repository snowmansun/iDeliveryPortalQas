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

namespace DatapoolWeb.View.Store
{
    public partial class StoreRelation : BasePage
    {
        private IStoreService _storeService = new StoreService();
        private IStoreApproveService _storeApproveService = new StoreApproveService();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
            }
        }

        /// <summary>
        /// SR选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSR_SelectedIndexChanged(object sender, EventArgs e)
        {
            //绑定门店数据
            BindData();

            //绑定SR

            //IQueryable<PersonView> list = ddlSR.DataSource;

            //foreach (PersonView item in list)
            //{
            //    ShowMessage(item.ID);
            //}

            ////ddlTrunsfelSr.DataTextField = "SHOW_NAME";
            ////ddlTrunsfelSr.DataValueField = "ID";
            ////ddlTrunsfelSr.DataBind();
            ////ddlTrunsfelSr.Items.Insert(0, new ListItem("", ""));

            ddlTrunsfelSr.Items.Remove(new ListItem(ddlSR.SelectedItem.Text, ddlSR.SelectedValue));


        }


        private void BindData()
        {
            GetModelQuery condition = new GetModelQuery();
            condition.con18 = ddlSR.SelectedValue.Trim();

            List<StoreView> storeList = _storeService.LoadStoreInfo(condition).ToList();
            gvStore.DataSource = storeList;
            gvStore.DataBind();
        }


        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            BindSR();

            GetModelQuery query = new GetModelQuery();
            query.con18 = UserStateInfo.PersonID;
            List<StoreView> storeList = _storeService.LoadStoreInfo(query).ToList();
            gvStore.DataSource = storeList;
            gvStore.DataBind();

        }

        /// <summary>
        /// 绑定城市下拉框
        /// </summary>
        public void BindSR()
        {
            IPersonService personService = new PersonService();
            ddlSR.DataSource = personService.LoadPersonRelationInfo(UserStateInfo.PersonID);
            ddlSR.DataTextField = "SHOW_NAME";
            ddlSR.DataValueField = "ID";
            ddlSR.DataBind();
            ddlSR.Items.Insert(0, new ListItem("", ""));

            ddlTrunsfelSr.DataSource = personService.LoadPersonRelationInfo(UserStateInfo.PersonID);
            ddlTrunsfelSr.DataTextField = "SHOW_NAME";
            ddlTrunsfelSr.DataValueField = "ID";
            ddlTrunsfelSr.DataBind();
            ddlTrunsfelSr.Items.Insert(0, new ListItem("", ""));
        }

        //编辑门店
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindData();
        }

        //分页
        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindData();
            this.gvStore.PageIndex = 0;
        }

        ////转移
        //protected void btnTransfer_Click(object sender, EventArgs e)
        //{

        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript", "$(\"#btnTransfer\").click(popTips);", true);

        //if (gvStore.Rows.Count <= 0)
        //    return;

        ////CheckBox chkheader = this.GridView1.HeaderRow.FindControl("chkAll") as CheckBox;

        //for (int i = 0; i < gvStore.Rows.Count; i++)
        //{
        //    string storeCode = gvStore.DataKeys[i].Value.ToString();

        //    CheckBox cbox = (CheckBox)gvStore.Rows[i].FindControl("cbxIsSelect");
        //    if (cbox != null && cbox.Checked == true)
        //    {
        //        MASTER_STORE master_Store = _storeService.LoadStoreInfoByCode(storeCode);
        //        if (master_Store != null)
        //        {//存在
        //            bool IsNew = false;

        //            //人员门店表中是否存在
        //            STORE_APPROVE master_Approve = _storeApproveService.LoadStoreApproveInfoByCode(storeCode);

        //            //判断是否在审核中

        //            //........
        //            if (master_Approve == null)
        //            {
        //                master_Approve = new STORE_APPROVE();
        //                IsNew = true;
        //            }

        //            master_Approve.STORE_CODE = master_Store.STORE_CODE;
        //            master_Approve.ADDRESS = master_Store.ADDRESS;
        //            master_Approve.AFFILIATE = master_Store.AFFILIATE;
        //            master_Approve.AREA = master_Store.AREA;
        //            master_Approve.BANNER_CODE = master_Store.BANNER_CODE;
        //            master_Approve.BANNER_GROUP_CODE = master_Store.BANNER_GROUP_CODE;
        //            master_Approve.CHANNEL = master_Store.CHANNEL;
        //            master_Approve.CITY_CODE = master_Store.CITY_CODE;
        //            master_Approve.CODE = master_Store.CODE;
        //            master_Approve.CONTACT_PERSON = master_Store.CONTACT_PERSON;
        //            master_Approve.DELIVERY = master_Store.DELIVERY;
        //            master_Approve.DISTRIBUTOR_CODE = master_Store.DISTRIBUTOR_CODE;
        //            master_Approve.DISTRIBUTOR_CODE_2 = master_Store.DISTRIBUTOR_CODE_2.Value == true ? "1" : "0";
        //            master_Approve.DISTRICT = master_Store.DISTRICT;
        //            master_Approve.DSM = master_Store.DSM;
        //            master_Approve.KA = master_Store.KA == "1" ? true : false;
        //            master_Approve.REFRIGERATOR = master_Store.REFRIGERATOR.Value == true ? "1" : "0";
        //            master_Approve.SR = master_Store.SR;
        //            master_Approve.SR_CODE = master_Store.SR_CODE;
        //            master_Approve.SR_TYPE = master_Store.SR_TYPE;
        //            master_Approve.STATUS = master_Store.STATUS;
        //            master_Approve.STORE_CODE_SHORT = master_Store.STORE_CODE_SHORT;
        //            master_Approve.STORE_NAME = master_Store.STORE_NAME;
        //            master_Approve.TEL_NO = master_Store.TEL_NO;
        //            master_Approve.TYPE = master_Store.TYPE;

        //            if (IsNew)
        //                //新增
        //                _storeApproveService.AddEntities(master_Approve);
        //            else
        //                //更新
        //                _storeApproveService.UpdateEntities(master_Approve);
        //        }

        //    }
        //}
        //}

        //新增
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            if (this.ddlSR.SelectedValue.Trim().Equals(""))
                ShowMessage("请选择SR！");
            else
                Response.Redirect("StoreRelationAdd.aspx?sr=" + this.ddlSR.SelectedValue.Trim());
        }

        //Command 时间
        protected void gvStore_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //object objID = e.CommandArgument;
            //if (objID != null)
            //{
            //    if (e.CommandName == "TransferCustomer")
            //    {
            //        //转移门店

            //    }
            //    else if (e.CommandName == "DeleteCustomer")
            //    {
            //        //删除门店
            //        MASTER_STORE master_Store = _storeService.LoadStoreInfoByCode(objID.ToString());
            //        if (master_Store != null)
            //        {
            //            master_Store.SR = null;
            //            master_Store.SR_CODE = null;
            //            master_Store.SR_TYPE = null;
            //            //删除
            //            _storeService.UpdateEntities(master_Store);
            //        }

            //    }
            //}
        }


        //删除
        protected void btnDel_Click(object sender, EventArgs e)
        {
            if (gvStore.Rows.Count <= 0)
                return;

            int col = 0;

            //CheckBox chkheader = this.GridView1.HeaderRow.FindControl("chkAll") as CheckBox;

            for (int i = 0; i < gvStore.Rows.Count; i++)
            {
                //门店CODE
                string storeCode = gvStore.DataKeys[i].Value.ToString();

                CheckBox cbox = (CheckBox)gvStore.Rows[i].FindControl("cbxIsSelect");
                if (cbox != null && cbox.Checked == true)//勾选
                {
                    col++;

                    MASTER_STORE master_Store = _storeService.LoadStoreInfoByCode(storeCode);
                    if (master_Store != null)
                    {//存在

                        master_Store.SR = null;
                        master_Store.SR_CODE = null;
                        master_Store.SR_TYPE = null;

                        //删除
                        _storeService.UpdateEntities(master_Store);

                    }
                }
            }

            BindData();


            if (col == 0)
                ShowMessage("请选择数据！");
        }

        //转移
        protected void btnComfrim_Click(object sender, EventArgs e)
        {
            if (gvStore.Rows.Count <= 0)
                return;

            if (hidSr.Value.Trim().Equals(""))
            {
                ShowMessage("请选择将转移到的SR！");
                return;
            }


            int col = 0;

            for (int i = 0; i < gvStore.Rows.Count; i++)
            {
                string storeCode = gvStore.DataKeys[i].Value.ToString();
                CheckBox cbox = (CheckBox)gvStore.Rows[i].FindControl("cbxIsSelect");
                if (cbox != null && cbox.Checked == true)
                {
                    col++;

                    MASTER_STORE master_Store = _storeService.LoadStoreInfoByCode(storeCode);
                    if (master_Store != null)
                    {//存在

                        master_Store.SR = this.hidSr.Value;

                        _storeService.UpdateEntities(master_Store);
                    }
                }
            }

            BindData();

            if (col == 0)
                ShowMessage("请选择数据！");

        }

    }
}