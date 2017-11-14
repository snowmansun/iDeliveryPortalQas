using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewSystemConfigData
    {
        public int SystemConfigID { get; set; }
        public string Category { get; set; }
        public string KeyName { get; set; }
        public string KeyValue { get; set; }
        public string Description { get; set; }
        public string OrgIDs { get; set; }
        public string OrgNames { get; set; }
    }
}
