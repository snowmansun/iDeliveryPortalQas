using Mde.Common;
using Mde.Model.DataModel;
using Mde.ModelView.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.IBLL
{
    public partial interface IMD_AccountService:IBaseService<MD_Account>
    {
        List<ViewAccountData> GetAccoutListData(string CompanyCode, string Account, ParameterQuery param);
    }
}
