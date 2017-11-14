using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewDeliveryHeaderData
    {
        public string ShipmentNo { get; set; }
        public string DeliveryNo { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string Status { get; set; }
        public string StatusDesc { get; set; }
        public Nullable<System.DateTime> CustomerSignDate { get; set; }
        public string CustomerSignImg { get; set; }
        public string DriverSignImg { get; set; }
        public string DriverName { get; set; }
        public string DeliveryType { get; set; }

    }
}
