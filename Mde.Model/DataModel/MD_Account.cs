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
    
    public partial class MD_Account
    {
        public MD_Account()
        {
            this.DSD_M_DeliveryHeader = new HashSet<DSD_M_DeliveryHeader>();
            this.DSD_M_ShipmentVanSalesRoute = new HashSet<DSD_M_ShipmentVanSalesRoute>();
            this.DSD_T_DeliveryHeader = new HashSet<DSD_T_DeliveryHeader>();
            this.MD_Contact = new HashSet<MD_Contact>();
            this.DSD_T_ShipmentVanSalesRoute = new HashSet<DSD_T_ShipmentVanSalesRoute>();
            this.DSD_T_Visit = new HashSet<DSD_T_Visit>();
        }
    
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public string Owner { get; set; }
        public string RecordType { get; set; }
        public string Site { get; set; }
        public string AccountSource { get; set; }
        public Nullable<decimal> AnnualRevenue { get; set; }
        public string BillingAddress { get; set; }
        public string CreatedBy { get; set; }
        public string Jigsaw { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> NumberOfEmployees { get; set; }
        public string Fax { get; set; }
        public string Industry { get; set; }
        public string LastModifiedBy { get; set; }
        public string Ownership { get; set; }
        public string Parent { get; set; }
        public string Phone { get; set; }
        public string Rating { get; set; }
        public string ShippingAddress { get; set; }
        public string Sic { get; set; }
        public string SicDesc { get; set; }
        public string TickerSymbol { get; set; }
        public string Type { get; set; }
        public string Website { get; set; }
        public string ebMobile__AccountGroup__c { get; set; }
        public string ebMobile__AccountName1__c { get; set; }
        public string ebMobile__AccountName2__c { get; set; }
        public string ebMobile__AccountPartner__c { get; set; }
        public string ebMobile__AccountPhotoId__c { get; set; }
        public Nullable<System.DateTime> ebMobile__ActivationDate__c { get; set; }
        public bool ebMobile__IsActive__c { get; set; }
        public string ebMobile__Address__c { get; set; }
        public Nullable<System.DateTime> ebMobile__ApprovalStepsDate__c { get; set; }
        public string ebMobile__BillTo__c { get; set; }
        public Nullable<bool> ebMobile__BlockByCredit__c { get; set; }
        public string ebMobile__DeliveryType__c { get; set; }
        public string ebMobile__BusinessTypeExtension__c { get; set; }
        public string ebMobile__CallFrequency__c { get; set; }
        public string CallFrequencyCode__c { get; set; }
        public string ebMobile__Category__c { get; set; }
        public string ebMobile__IsChangeByInterface__c { get; set; }
        public string ebMobile__CityCode__c { get; set; }
        public string ebMobile__Segment__c { get; set; }
        public Nullable<decimal> ebMobile__CloseDay__c { get; set; }
        public string ebMobile__ClosingTime__c { get; set; }
        public string ebMobile__Company__c { get; set; }
        public string ebMobile__CompanyCode__c { get; set; }
        public string ebMobile__CompanyDeleteFlag__c { get; set; }
        public string ebMobile__ContactEmail__c { get; set; }
        public string ebMobile__ContactMobile__c { get; set; }
        public string ebMobile__ContactPersonFunction__c { get; set; }
        public string ebMobile__ContactPersonName__c { get; set; }
        public string ebMobile__ContactPhone__c { get; set; }
        public string ebMobile__Country__c { get; set; }
        public string ebMobile__CountryCode__c { get; set; }
        public string ebMobile__CountryKey__c { get; set; }
        public Nullable<decimal> ebMobile__CreditDays__c { get; set; }
        public Nullable<decimal> ebMobile__AvailableBalance__c { get; set; }
        public Nullable<decimal> ebMobile__CreditLimit__c { get; set; }
        public string ebMobile__Currency__c { get; set; }
        public string ebMobile__CustomerConditionGroup__c { get; set; }
        public string ebMobile__DeliveringPlant__c { get; set; }
        public string ebMobile__DeliveryDate__c { get; set; }
        public Nullable<decimal> ebMobile__DeliveryDays__c { get; set; }
        public string ebMobile__DiscountIndicator__c { get; set; }
        public string ebMobile__DistributionChannel__c { get; set; }
        public string ebMobile__Distributor__c { get; set; }
        public string ebMobile__District__c { get; set; }
        public string ebMobile__Division__c { get; set; }
        public string ebMobile__ERPAccountNumber__c { get; set; }
        public string ExecuteBatchFlag__c { get; set; }
        public Nullable<decimal> ebMobile__GeoCode__c { get; set; }
        public Nullable<System.Guid> ebMobile__GUID__c { get; set; }
        public Nullable<bool> HasUpdatedAR__c { get; set; }
        public string ebMobile__HouseNumber__c { get; set; }
        public Nullable<decimal> ebMobile__InvestmentValue__c { get; set; }
        public string ebMobile__InvoiceCalendar__c { get; set; }
        public Nullable<bool> IsPush__c { get; set; }
        public string ebMobile__KeyAccount__c { get; set; }
        public string KeyAccount__c { get; set; }
        public Nullable<decimal> Last3MonthsVolume__c { get; set; }
        public Nullable<decimal> ebMobile__Last3MonthsVolume__c { get; set; }
        public Nullable<System.DateTime> ebMobile__LastCallDate__c { get; set; }
        public Nullable<System.DateTime> ebMobile__LastOrderDate__c { get; set; }
        public Nullable<decimal> ebMobile__LastOrderTotalPrice__c { get; set; }
        public Nullable<decimal> ebMobile__LastOrderTotalQuantity__c { get; set; }
        public string ebMobile__MEPCustomerNumber__c { get; set; }
        public string ebMobile__OpeningTime__c { get; set; }
        public string ebMobile__OpenItemsAgingStart__c { get; set; }
        public string ebMobile__OperationalMarketType__c { get; set; }
        public string ebMobile__OperationalTradeChannel__c { get; set; }
        public Nullable<bool> ebMobile__OrderBlock__c { get; set; }
        public string ebMobile__Owner_Role__c { get; set; }
        public Nullable<decimal> ebMobile__P3MAVGSales__c { get; set; }
        public string ebMobile__PartnerFunction__c { get; set; }
        public string ebMobile__Payer__c { get; set; }
        public string ebMobile__PaymentTerms__c { get; set; }
        public string ebMobile__PONumber__c { get; set; }
        public string ebMobile__Postal__c { get; set; }
        public string ebMobile__PriceAttribute__c { get; set; }
        public string ebMobile__PriceClassID__c { get; set; }
        public string ebMobile__PriceGroup__c { get; set; }
        public string ebMobile__PriceListType__c { get; set; }
        public string ebMobile__PricingProcedure__c { get; set; }
        public Nullable<decimal> ebMobile__RecordAction__c { get; set; }
        public string ebMobile__RecordTypeName__c { get; set; }
        public Nullable<bool> RedIndecator__c { get; set; }
        public string ebMobile__Region__c { get; set; }
        public string ebMobile__RegistrationNumber__c { get; set; }
        public string ebMobile__SalesDistrict__c { get; set; }
        public string ebMobile__SaleGroup__c { get; set; }
        public string ebMobile__SalesOffice__c { get; set; }
        public string ebMobile__SalesOrg__c { get; set; }
        public string ebMobile__SalesOrgCode__c { get; set; }
        public string ebMobile__User__c { get; set; }
        public string ebMobile__SalesRoute__c { get; set; }
        public string ebMobile__ShippingCondition__c { get; set; }
        public string ShippingConditionCode__c { get; set; }
        public string ebMobile__ShipTo__c { get; set; }
        public string ebMobile__State__c { get; set; }
        public string ebMobile__Street__c { get; set; }
        public string ebMobile__Street4__c { get; set; }
        public string ebMobile__Street5__c { get; set; }
        public string ebMobile__StreetNumber__c { get; set; }
        public string ebMobile__SubTradeChannel__c { get; set; }
        public string ebMobile__SuppressionReason__c { get; set; }
        public string ebMobile__SuspressionCode__c { get; set; }
        public Nullable<System.DateTime> ebMobile__SuspressionDate__c { get; set; }
        public string ebMobile__TargetGroup__c { get; set; }
        public string ebMobile__TaxCode__c { get; set; }
        public string ebMobile__TaxNumber__c { get; set; }
        public Nullable<decimal> ebMobile__TotalOpenAmount__c { get; set; }
        public string ebMobile__TradeChannel__c { get; set; }
        public string ebMobile__TradeGroup__c { get; set; }
        public string ebMobile__TradeName__c { get; set; }
        public string TransportationZone__c { get; set; }
        public string ebMobile__UniqueKey__c { get; set; }
        public string ebMobile__VATNumber__c { get; set; }
        public string ebMobile__Vendor__c { get; set; }
        public string CCSM_VendorNo__c { get; set; }
        public Nullable<System.DateTime> ebMobile__VisitStartDate__c { get; set; }
        public string ebMobile__ZAPartner__c { get; set; }
        public string ebMobile__ZBPartner__c { get; set; }
        public string ebMobile__ZGPartner__c { get; set; }
        public string ebMobile__ZHPartner__c { get; set; }
        public string ebMobile__ZIPartner__c { get; set; }
        public string ebMobile__ZNPartner__c { get; set; }
        public string ebMobile__ZQPartner__c { get; set; }
        public string ebMobile__ZRPartner__c { get; set; }
        public string ebMobile__ZTPartner__c { get; set; }
        public string ebMobile__ZXCrossDock__c { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
        public Nullable<decimal> Geo_Longitude { get; set; }
        public Nullable<decimal> Geo_Latitude { get; set; }
    
        public virtual ICollection<DSD_M_DeliveryHeader> DSD_M_DeliveryHeader { get; set; }
        public virtual ICollection<DSD_M_ShipmentVanSalesRoute> DSD_M_ShipmentVanSalesRoute { get; set; }
        public virtual ICollection<DSD_T_DeliveryHeader> DSD_T_DeliveryHeader { get; set; }
        public virtual ICollection<MD_Contact> MD_Contact { get; set; }
        public virtual ICollection<DSD_T_ShipmentVanSalesRoute> DSD_T_ShipmentVanSalesRoute { get; set; }
        public virtual ICollection<DSD_T_Visit> DSD_T_Visit { get; set; }
    }
}
