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
    
    public partial class DSD_T_ShipmentFinance
    {
        public System.Guid HeaderId { get; set; }
        public string Type { get; set; }
        public string Currency { get; set; }
        public decimal ActualAmount { get; set; }
        public Nullable<int> ChequeQty { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public string FinanceType { get; set; }
    
        public virtual DSD_T_ShipmentHeader DSD_T_ShipmentHeader { get; set; }
    }
}
