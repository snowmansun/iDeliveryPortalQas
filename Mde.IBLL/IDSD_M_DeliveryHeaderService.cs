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
    public partial interface IDSD_M_DeliveryHeaderService : IBaseService<DSD_M_DeliveryHeader>
    {
        List<ViewDeliveryNoteData> GetDeliveryNoteData(string DeliveryDate, string ShipmentNo, string DeliveryNo, string Driver, string TruckNo, string Status, string DeliveryType, ParameterQuery param);

        List<ViewDeliveryItemData> GetDeliveryItemData(string DeliveryNo);
    }
}
