using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewShipmentListData
    {
        public string ShipmentNo { set; get; }
        public string WarehouseName { set; get; }
        public string Driver { set; get; }
        public Nullable<System.Int32> TruckID { set; get; }
        public string TruckCode { set; get; }
        public string ShipmentType { set; get; }
        public int ProductNum { set; get; }
        public string ProductQty { set; get; }
        public int CustomerNum { set; get; }
        public string StatusValue { set; get; }
        public string StatusDesc { get; set; }
    }
}
