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
    public partial class StoreRelationDSM : BasePage
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
        /// DSM选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSR_SelectedIndexChanged(object sender, EventArgs e)
        {
            //绑定门店数据
            BindData();

            ddlTrunsfelSr.Items.Remove(new ListItem(ddlSR.SelectedItem.Text, ddlSR.SelectedValue));


        }


        private void BindData()
        {
            GetModelQuery condition = new GetModelQuery();
            condition.con19 = ddlSR.SelectedValue.Trim();

            List<StoreView> storeList = _storeService.LoadStoreInfo(condition).ToList();
            gvStore.DataSource = storeList;
            gvStore.DataBind();
        }


        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            BindDSM();

            GetModelQuery query = new GetModelQuery();
            query.con18 = UserStateInfo.PersonID;
            List<StoreView> storeList = _storeService.LoadStoreInfo(query).ToList();
            gvStore.DataSource = storeList;
            gvStore.DataBind();

        }

        /// <summary>
        /// 绑定城市下拉框
        /// </summary>
        public void BindDSM()
        {
            IPersonService personService = new PersonService();
            ddlSR.DataSource = personService.LoadPersonRelationInfo(UserStateInfo.PersonID).Where(p => p.POSITION == "DSM").ToList();
            ddlSR.DataTextField = "SHOW_NAME";
            ddlSR.DataValueField = "ID";
            ddlSR.DataBind();
            ddlSR.Items.Insert(0, new ListItem("", ""));

            ddlTrunsfelSr.DataSource = personService.LoadPersonRelationInfo(UserStateInfo.PersonID).Where(p => p.POSITION == "DSM").ToList();
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

        //新增
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            if (this.ddlSR.SelectedValue.Trim().Equals(""))
                ShowMessage("请选择DSM！");
            else
                Response.Redirect("StoreRelationDSMAdd.aspx?dsm=" + this.ddlSR.SelectedValue.Trim());
        }

        //Command 时间
        protected void gvStore_RowCommand(object sender, GridViewCommandEventArgs e)
        {

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

                        master_Store.DSM = null;

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
                ShowMessage("请选择将转移到的DSM！");
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

                        master_Store.DSM = this.hidSr.Value;

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