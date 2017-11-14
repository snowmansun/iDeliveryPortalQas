using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewShipmentTrackingData
    {
        public System.Guid HeaderId { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string ShipmentNo { get; set; }
        public string ShipmentType { get; set; }
        public string Driver { get; set; }
        public string Truck { get; set; }
        public string Company { get; set; }
        public string Warehouse { get; set; }
        public string Status { get; set; }
        public string StatusDesc { get; set; }
        public Nullable<System.DateTime> CheckerConfirmTime { get; set; }
        public string Checker { get; set; }
        public string CheckerSignImg { get; set; }
        public string DCheckerSignImg { get; set; }
        public string Cashier { get; set; }
        public string CashierSignImg { get; set; }
        public string DCashierSignImg { get; set; }
        public string Gatekeeper { get; set; }
        public string GKSignImg { get; set; }
        public string DGKSignImg { get; set; }
    }
}
