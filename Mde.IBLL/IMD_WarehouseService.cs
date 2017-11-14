using Mde.Common;
using Mde.Model.DataModel;
using Mde.Model.ModelView;
using Mde.ModelView.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.IBLL
{
    public partial interface IMD_WarehouseService:IBaseService<MD_Warehouse>
    {
        List<ViewComboboxStringData> GetWareHouseComboBoxData(string CompanyCode);

        List<ViewWarehouseData> GetWarehouseList(string Code, string Name, string CompanyCode, string Status, ParameterQuery param);
    }
}
