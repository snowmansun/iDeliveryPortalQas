using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewDeliveryNoteData
    {
        public string ShipmentNo { get; set; }
        public string DeliveryNo { get; set; }
        public string DeliveryType { get; set; }
        public string DeliveryDate { get; set; }
        public string Driver { get; set; }
        public string TruckCode { get; set; }
        public string Customer { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public Nullable<System.Int32> Products { get; set; }
        public string ProductQty { get; set; }
        public Nullable<System.Decimal> Invoice { get; set; }
        public string Status { get; set; }
    }
}
