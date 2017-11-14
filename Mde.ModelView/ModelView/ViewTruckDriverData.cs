using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewTruckDriverData
    {
        public int TruckId { get; set; }
        public string CompanyCode { get; set; }
        public string Company { get; set; }
        public string TruckNumber { get; set; }
        public string Driver { get; set; }
        public string Type { get; set; }
        public Nullable<decimal> Capacity { get; set; }
        public Nullable<decimal> Volume { get; set; }
        public bool Status { get; set; }
    }
}
