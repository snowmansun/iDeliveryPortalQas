using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewRouteMapInfo
    {
        public DateTime ShipmentDate { get; set; }
        public int DriverCnt { get; set; }
        public int CUSDCnt { get; set; }
        public int DONECnt { get; set; }
        public decimal FinanceCard { get; set; }
        public decimal FinanceCash { get; set; }
        public decimal FinanceCred { get; set; }
        public string CarsMaterial { get; set; }
    }
}
