using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewTreeGridData
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string ParentName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public Nullable<bool> Valid { get; set; }
        public List<ViewTreeGridData> children { get; set; }

        public ViewTreeGridData()
        {
            children = new List<ViewTreeGridData>();
        }
    }
}
