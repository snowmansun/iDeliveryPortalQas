using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mde.Portal.Model
{
    public class VisitTrackingModel
    {
        public int CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerTel { get; set; }
        public string CustomerContact { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public string Type { get; set; }
    }
}