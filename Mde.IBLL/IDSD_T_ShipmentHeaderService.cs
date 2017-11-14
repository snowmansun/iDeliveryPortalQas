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
    public partial interface IDSD_T_ShipmentHeaderService:IBaseService<DSD_T_ShipmentHeader>
    {
        List<ViewShipmentTrackingData> LoadTrackingData(string ShipmentNo, string Driver, string ShipmentDate, string Status,string ShipmentType, ParameterQuery param);

        List<ViewShipmentTrackingItemData> LoadTrackingItemData(string HeaderId, string Category);
    }
}
