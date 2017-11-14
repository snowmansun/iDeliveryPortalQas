using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewWarehouseData
    {
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public int Sequence { get; set; }
        public bool Valid { get; set; }
        public string Barcode { get; set; }
        public string Remark { get; set; }
    }
}
