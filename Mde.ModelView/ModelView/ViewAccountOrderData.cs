using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewAccountOrderData
    {
        public string DeliveryType { get; set; }
        public string QtyEACS { get; set; }
        public decimal PlanAmount { get; set; }
        public decimal ActualAmount { get; set; }
        public List<AccountFinanceList> FinanceList { get; set; }
        public ViewAccountOrderData()
        {
            FinanceList = new List<AccountFinanceList>();
        }
    }

    public class AccountFinanceList
    {
        public string DeliveryType { get; set; }
        public string PaymentTypeValue { get; set; }
        public string PaymentTypeName { get; set; }
        public decimal PaymentAmount { get; set; }
    }
}
