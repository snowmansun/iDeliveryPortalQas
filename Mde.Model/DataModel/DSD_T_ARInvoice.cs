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
    
    public partial class DSD_T_ARInvoice
    {
        public string ID { get; set; }
        public bool ebMobile__IsActive__c { get; set; }
        public string ebMobile__ARCollection__c { get; set; }
        public string ebMobile__GUID__c { get; set; }
        public Nullable<System.DateTime> ebMobile__InvoiceDate__c { get; set; }
        public string ebMobile__InvoiceNumber__c { get; set; }
        public Nullable<decimal> ebMobile__OffsetNumber__c { get; set; }
        public Nullable<decimal> ebMobile__RecordAction__c { get; set; }
        public string ebMobile__VisitId__c { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
    
        public virtual DSD_T_ARCollection DSD_T_ARCollection { get; set; }
    }
}
