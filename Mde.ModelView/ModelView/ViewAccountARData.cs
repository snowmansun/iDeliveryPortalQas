using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewAccountARData
    {
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public Nullable<decimal> CreditLimit { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<decimal> InvoiceAmount { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<decimal> OpenAmount { get; set; }
        public Nullable<decimal> TotalOpenAmount { get; set; }
        public Nullable<bool> BlockByCredit { get; set; }
    }
}
