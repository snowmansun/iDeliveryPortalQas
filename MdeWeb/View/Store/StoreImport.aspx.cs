using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datapool.BLL;
using Datapool.IBLL;

namespace DatapoolWeb.View.Store
{
    public partial class StoreImport : BasePage
    {
        #region Field
        IStoreService personService = new StoreService();
        #endregion

        #region 画面事件
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion
    }
}