using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewOdometerTrackingData
    {
        public System.Guid Id { get; set; }
        public int Odometer { get; set; }
        public DateTime TrackingTime { get; set; }
        public string TrackingAction { get; set; }
        public string ActionDesc { get; set; }
        public string ShipmentNo { get; set; }
        public string Driver { get; set; }
        public string DriverName { get; set; }
        public int TruckId { get; set; }
        public string TruckCode { get; set; }
    }
}
