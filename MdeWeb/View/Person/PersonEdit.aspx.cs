using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datapool.BLL;
using Datapool.IBLL;
using Datapool.Model.DataModel;

namespace DatapoolWeb.View.Person
{
    public partial class PersonEdit : BasePage
    {
        #region Field
        IPersonService personService = new PersonService();
        #endregion

        #region 画面事件
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 初始化页面
                InitData();
            }
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
                if (SavePerson()) ShowAndRedirect(this, "保存成功", "PersonView.aspx?status=u");
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }
        #endregion

        #region 自定义方法
        /// <summary>
        /// 初始化页面数据
        /// </summary>
        private void InitData()
        {
            BindCity(ddlCity, false, string.Empty, string.Empty);
            BindPerson(ddlPerPerson, false);

            string id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                MASTER_PERSON info = personService.LoadEntities(x => x.ID == id).First();
                SetModelToForm(info);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        private void SetModelToForm(MASTER_PERSON info)
        {
            txtID.Text = info.ID;
            txtCode.Text = info.ACCOUNT_CODE;
            txtNameEn.Text = info.NAME_EN;
            txtName.Text = info.NAME;
            txtPosition.Text = info.POSITION;
            txtOrgNo.Text = info.ORG_NO;
            txtOrgNm.Text = info.ORG_NM;
            txtCostCtr.Text = info.COST_CTR;
            txtCostCenter.Text = info.COST_CENTER;
            txtCid.Text = info.CID;
            txtEmail.Text = info.LONG_ID;
            ddlPerPerson.SelectedValue = info.PAR_ID;
            ddlCity.SelectedValue = info.CITY_CODE;

            txtID.ReadOnly = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private MASTER_PERSON SetFormToModel()
        {
            MASTER_PERSON person = new MASTER_PERSON();
           
            person.ID = txtID.Text.Trim();
            person.ACCOUNT_CODE = txtCode.Text.Trim();
            person.NAME = txtName.Text.Trim();
            person.NAME_EN = txtNameEn.Text;
            person.POSITION = txtPosition.Text;
            person.ORG_NO = txtOrgNo.Text;
            person.ORG_NM = txtOrgNm.Text;
            person.COST_CTR = txtCostCtr.Text;
            person.COST_CENTER = txtCostCenter.Text;
            person.CID = txtCid.Text;
            person.LONG_ID = txtEmail.Text;
            person.CITY_CODE = ddlCity.SelectedValue;
            person.PAR_ID = ddlPerPerson.SelectedValue;
            
            return person;
        }

        /// <summary>
        /// 保存人员信息
        /// </summary>
        private bool SavePerson()
        {
            MASTER_PERSON person = SetFormToModel();
           
            if (string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                //判断是否有重复
                if (personService.LoadEntities(x => x.ID == txtID.Text.Trim()).Count() == 0)
                    personService.AddEntities(person);
                else
                {
                    ShowMessage("当前编号已经被使用， 请重新输入");
                    return false;
                }
            }
            else personService.UpdateEntities(person);
            return true;
        }
        #endregion
    }
}