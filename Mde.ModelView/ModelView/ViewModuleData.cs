using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewModuleData
    {
        public int ModuleID { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public string Description { get; set; }
        public string ParentModule { get; set; }
        public string ModulePath { get; set; }
        public int Sequence { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public Nullable<int> ClientScope { get; set; }
        public string Action { get; set; }
        public Nullable<int> ActionMode { get; set; }
        public string ButtonRole { get; set; }
    }
}
