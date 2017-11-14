using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewShipmentTrackingItemData
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string PlanQty { get; set; }
        public string ActualQty { get; set; }
        public string DifferenceQty { get; set; }
        public string DiffReason { get; set; }
    }
}
