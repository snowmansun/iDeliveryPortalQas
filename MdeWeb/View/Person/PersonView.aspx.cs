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

namespace DatapoolWeb.View.Person
{
    public partial class PersonView : BasePage
    {
        #region Feild
        IPersonService personService = new PersonService();
        ICityService cityService = new CityService();
        #endregion

        #region 画面事件
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
            }
        }

        /// <summary>
        /// 查询事件按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // 绑定数据
            BindData();
            this.gvPerson.PageIndex = 0;
        }

        /// <summary>
        /// 新建按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("personedit.aspx");
        }

        /// <summary>
        /// 
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
            this.gvPerson.PageIndex = newPageIndex;
            //分页后重新绑定数据源
            this.BindData();
        }
        #endregion

        #region 自定义方法

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            //ddlCity.DataSource = _cityService.LoadCityInfo("");
            //ddlCity.DataTextField = "CITY_NM_CN";
            //ddlCity.DataValueField = "CITY_CODE";
            //ddlCity.DataBind();

            // 
            BindCity(ddlCity, true, string.Empty, string.Empty);

            if (!string.IsNullOrEmpty(Request["status"]))
            {
                GetModelQuery conditions = (GetModelQuery)Session["PersonQuery"];
                if (conditions != null)
                {
                    //
                    SetCondition(conditions);
                    //
                    BindData();
                }
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            GetModelQuery conditions = GetCondition();
            Session["PersonQuery"] = conditions;

            List<Datapool.Model.PersonView> rs = personService.LoadPersonInfo(conditions).ToList();
            gvPerson.DataSource = rs;
            gvPerson.DataBind();
        }

        /// <summary>
        /// 查询参数
        /// </summary>
        /// <returns></returns>
        private GetModelQuery GetCondition()
        {
            GetModelQuery condition = new GetModelQuery();
            condition.con1 = txtID.Text.Trim();
            condition.con2 = txtAccountCode.Text.Trim();
            condition.con3 = txtNameEn.Text.Trim();
            condition.con4 = txtName.Text.Trim();
            condition.con5 = ddlCity.SelectedValue;
            condition.con6 = txtPostion.Text.Trim();
            condition.con7 = txtOrgNm.Text.Trim();
            condition.con8 = txtPar.Text.Trim();
            condition.pageIndex = this.gvPerson.PageIndex;
            return condition;
        }

        /// <summary>
        /// 设置查询条件 
        /// </summary>
        /// <param name="condition"></param>
        private void SetCondition(GetModelQuery condition)
        {
            txtID.Text = condition.con1;
            txtAccountCode.Text = condition.con2;
            txtNameEn.Text = condition.con3;
            txtName.Text = condition.con4;
            ddlCity.SelectedValue = condition.con5;
            txtPostion.Text = condition.con6;
            txtOrgNm.Text = condition.con7;
            txtPar.Text = condition.con8;
            this.gvPerson.PageIndex = condition.pageIndex;
        }
        #endregion
    }
}