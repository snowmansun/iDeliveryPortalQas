using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewTruckCheckListData
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string TruckType { get; set; }
        public string TruckTypeDesc { get; set; }
        public bool Valid { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
    }
}
