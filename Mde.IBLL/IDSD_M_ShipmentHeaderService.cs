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
    public partial interface IDSD_M_ShipmentHeaderService:IBaseService<DSD_M_ShipmentHeader>
    {
        List<ViewShipmentListData> GetShipmentListData(string ShipmentDate, string ShipmentNo, string Warehouse, string Driver, string TruckNo, string Status, ParameterQuery param);

        DSD_T_ShipmentAssign CheckAssign(string shipmentNo);

        void ChangeDriverAndTruck(DSD_T_ShipmentAssign item, string mode);

        List<ViewShipmentItemData> GetShipmentItemData(string ShipmentNo);

        List<ViewDriverPosition> GetDriverPosition(string ClanderDate, string userCode);

        List<ViewShipmentInfo> GetDriverRouteMapData(string ClanderDate);

        List<ViewDriverRouteCustomerData> GetDriverRouteCustomerData(DateTime ShipmentDate, string DriverId);

        List<ViewAccountOrderData> GetAccountOrderData(string DeliveryDate, string AccountNumber);

        ViewAccountOrderData GetAccountARData(string DeliveryDate, string AccountNumber);
    }
}
