using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datapool.BLL;
using Datapool.IBLL;
using Datapool.Utility;

namespace DatapoolWeb.View.Person
{
    public partial class PersonImport : BasePage
    {
        #region Field
        IPersonService personService = new PersonService();
        #endregion

        #region 画面事件
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        ///// <summary>
        ///// 导入事件按钮
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnImport_Click(object sender, EventArgs e)
        //{
        //    lblMessage.Text = string.Empty;

        //    //获取文件路径  
        //    string filePath = this.AsyncFileUpload.PostedFile.FileName;
        //    if (filePath != "")
        //    {
        //        if (filePath.Contains("xls"))//判断文件是否存在  
        //        {
        //            ImportData(filePath);
        //            lblMessage.Text = "导入成功！";
        //        }
        //        else
        //        {
        //            lblMessage.Text = "请检查您选择的文件是否为Excel文件！";
        //        }
        //    }
        //    else
        //    {
        //        lblMessage.Text = "请先选择导入文件后，再执行导入！";
        //    }
        //}
        #endregion

        #region 自定义方法
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ImportData(string argFilePath)
        {
            DataSet ds = CommonHelp.ToDataTable(argFilePath);
            personService.ImportData(ds);

            return true;
        }

        #endregion
    }
}