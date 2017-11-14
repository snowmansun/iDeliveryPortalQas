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
    public partial interface IDSD_T_TruckStockService : IBaseService<DSD_T_TruckStock>
    {
        List<ViewTruckStockData> GetTruckStockData(string Truck, string ShipmentNo, string Product,string StockDate, ParameterQuery param);

        List<ViewStockTrackingData> GetTruckStockTrackingData(int TruckId, string ShipmentNo, string ProductCode);
    }
}
