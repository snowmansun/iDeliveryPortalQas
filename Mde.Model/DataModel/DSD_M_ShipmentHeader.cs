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
    
    public partial class DSD_M_ShipmentHeader
    {
        public DSD_M_ShipmentHeader()
        {
            this.DSD_M_DeliveryHeader = new HashSet<DSD_M_DeliveryHeader>();
            this.DSD_M_ShipmentFinance = new HashSet<DSD_M_ShipmentFinance>();
            this.DSD_T_DeliveryHeader = new HashSet<DSD_T_DeliveryHeader>();
            this.DSD_M_ShipmentHelper = new HashSet<DSD_M_ShipmentHelper>();
            this.DSD_M_ShipmentVanSalesRoute = new HashSet<DSD_M_ShipmentVanSalesRoute>();
            this.DSD_T_ShipmentHeader = new HashSet<DSD_T_ShipmentHeader>();
            this.DSD_M_ShipmentItem = new HashSet<DSD_M_ShipmentItem>();
            this.DSD_T_TruckStockTracking = new HashSet<DSD_T_TruckStockTracking>();
            this.DSD_T_ShipmentVanSalesRoute = new HashSet<DSD_T_ShipmentVanSalesRoute>();
        }
    
        public string ShipmentNo { get; set; }
        public Nullable<System.DateTime> ShipmentDate { get; set; }
        public string ShipmentType { get; set; }
        public string Route { get; set; }
        public string Description { get; set; }
        public string ReleaseStatus { get; set; }
        public string ReleaseUser { get; set; }
        public Nullable<System.DateTime> ReleaseTime { get; set; }
        public string CompletionStatus { get; set; }
        public Nullable<System.DateTime> CompletionTime { get; set; }
        public string Driver1 { get; set; }
        public string Driver2 { get; set; }
        public string TruckCode { get; set; }
        public string TruckType { get; set; }
        public Nullable<int> LoadingSequence { get; set; }
        public string WarehouseCode { get; set; }
        public string OutWarehouse { get; set; }
        public Nullable<int> TotalProductQty { get; set; }
        public Nullable<decimal> TotalItemAmount { get; set; }
        public Nullable<decimal> TotalWeight { get; set; }
        public string WeightUnit { get; set; }
        public string DataSource { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
    
        public virtual ICollection<DSD_M_DeliveryHeader> DSD_M_DeliveryHeader { get; set; }
        public virtual ICollection<DSD_M_ShipmentFinance> DSD_M_ShipmentFinance { get; set; }
        public virtual ICollection<DSD_T_DeliveryHeader> DSD_T_DeliveryHeader { get; set; }
        public virtual ICollection<DSD_M_ShipmentHelper> DSD_M_ShipmentHelper { get; set; }
        public virtual ICollection<DSD_M_ShipmentVanSalesRoute> DSD_M_ShipmentVanSalesRoute { get; set; }
        public virtual ICollection<DSD_T_ShipmentHeader> DSD_T_ShipmentHeader { get; set; }
        public virtual MD_Warehouse MD_Warehouse { get; set; }
        public virtual ICollection<DSD_M_ShipmentItem> DSD_M_ShipmentItem { get; set; }
        public virtual DSD_T_ShipmentAssign DSD_T_ShipmentAssign { get; set; }
        public virtual MD_User MD_User { get; set; }
        public virtual MD_User MD_User1 { get; set; }
        public virtual ICollection<DSD_T_TruckStockTracking> DSD_T_TruckStockTracking { get; set; }
        public virtual ICollection<DSD_T_ShipmentVanSalesRoute> DSD_T_ShipmentVanSalesRoute { get; set; }
    }
}
