using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewDriverRouteCustomerData
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public string DriverId { get; set; }
        public Nullable<System.Int32> VisitStatus { get; set; }
    }
}
