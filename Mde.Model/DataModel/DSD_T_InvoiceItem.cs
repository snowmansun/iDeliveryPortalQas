//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mde.Model.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class DSD_T_InvoiceItem
    {
        public string InvoiceNo { get; set; }
        public string ItemNo { get; set; }
        public string ProductCode { get; set; }
        public Nullable<decimal> Unit_Price { get; set; }
        public Nullable<decimal> InvoiceQty { get; set; }
        public Nullable<decimal> NetPrice { get; set; }
        public Nullable<decimal> PlanQty { get; set; }
        public Nullable<decimal> ActualQty { get; set; }
        public Nullable<decimal> ReceivableDeposit { get; set; }
        public Nullable<decimal> ActualDeposit { get; set; }
        public Nullable<decimal> DepositPrice { get; set; }
        public string ReturnFlag { get; set; }
        public string PromotionCode { get; set; }
        public string Remark { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
    
        public virtual DSD_T_Invoice DSD_T_Invoice { get; set; }
        public virtual MD_Product MD_Product { get; set; }
    }
}
