using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewStockTrackingData
    {
        public string ProductCode { get; set; }
        public string ProductUnit { get; set; }
        public string ChangeAction { get; set; }
        public DateTime TrackingTime { get; set; }
        public int FromQty { get; set; }
        public int ToQty { get; set; }
        public int ChangeQuantity { get; set; }
    }
}
