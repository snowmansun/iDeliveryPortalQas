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
    public partial interface IDSD_T_DeliveryHeaderService:IBaseService<DSD_T_DeliveryHeader>
    {
        List<ViewDeliveryHeaderData> LoadDelTrackingData(string ShipmentNo, string DeliveryNo, string DeliveryDate, string Account, string Status, string DeliveryType ,ParameterQuery param);

        List<ViewDicTrackingData> LoadDelTrackingItemData(string DeliveryNo);
    }
}
