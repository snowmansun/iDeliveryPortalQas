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
    public partial interface IMD_AccountARService:IBaseService<MD_AccountAR>
    {
        List<ViewAccountARData> GetAccountARList(string account, ParameterQuery param);
    }
}
