using Mde.Common;
using Mde.Model.DataModel;
using Mde.Model.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.IBLL
{
    public partial interface IMD_CompanyService:IBaseService<MD_Company>
    {
        List<ViewComboboxStringData> GetCompanyComboboxData();

        List<MD_Company> GetCompanyList(string CompanyCode, string CompanyName, string Status,ParameterQuery param);
    }
}
