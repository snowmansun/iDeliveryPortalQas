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
    public partial interface IDSD_M_TruckService:IBaseService<DSD_M_Truck>
    {
        List<ViewComboboxIntData> GetTruckList();

        List<ViewTruckDriverData> GetTruckDriverList(string CompanyCode, string TruckType, string TruckNumber, string Status, ParameterQuery param);

        void UpdateTruckDriver(DSD_M_TruckDriver item);

        DSD_M_TruckDriver CheckData(int TruckId);

        void SaveTruckDriver(DSD_M_TruckDriver item);
    }
}
