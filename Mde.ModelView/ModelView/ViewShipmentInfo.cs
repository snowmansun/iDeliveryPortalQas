using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewShipmentInfo
    {
        public int TruckId { get; set; }
        public string TruckCode { get; set; }
        public int ShipmentCnt { get; set; }
        public string StockOnTruck { get; set; }
        public string CustomerAndVisited { get; set; }
        public List<ShipmentFinanceList> FinanceList { get; set; }
        public ViewShipmentInfo()
        {
            FinanceList = new List<ShipmentFinanceList>();
        }
    }

    public class ShipmentFinanceList
    {
        public string PaymentTypeValue { get; set; }
        public string PaymentTypeName { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}
