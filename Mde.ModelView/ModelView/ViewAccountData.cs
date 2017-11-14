using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.ModelView.ModelView
{
    public class ViewAccountData
    {
        public string AccountName { get; set; }
        public string AccountName1 { get; set; }
        public string AccountName2 { get; set; }
        public string Owner { get; set; }
        public string RecordType { get; set; }
        public string AccountGroup { get; set; }
        public string AccountParent { get; set; }
        public string AccountNumber { get; set; }
        public string ERPAccountNumber { get; set; }
        public string TaxNumber { get; set; }
        public string VATNumber { get; set; }
        public string SuspressionCode { get; set; }
        public string SuppressionReason { get; set; }
        public string CompanyCode { get; set; }
        public string SalesOrg { get; set; }
        public string SalesOrgCode { get; set; }
        public string SalesDistrict { get; set; }
        public string SalesGroup { get; set; }
        public string SalesOffice { get; set; }
        public Nullable<System.DateTime> VisitStartDate { get; set; }
        public string SubChannel { get; set; }
        public string SalesRoute { get; set; }
        public string ShippingCondition { get; set; }
        public string InvoiceCalendar { get; set; }
        public string DeliveryDate { get; set; }
        public Nullable<decimal> DeliveryDays { get; set; }
        public string DiscountIndicator { get; set; }
        public string BillTo { get; set; }
        public Nullable<bool> IsPush { get; set; }
        public Nullable<bool> RedIndecator { get; set; }
        public string Ownership { get; set; }
        public string AccountSource { get; set; }
        public string Classification { get; set; }
        public string TradeChannel { get; set; }
        public string SubTradeChannel { get; set; }
        public string TradeName { get; set; }
        public string Currency { get; set; }
        public string DeliveringPlant { get; set; }
        public string MEPCustomerNumber { get; set; }
        public string BusinessType { get; set; }
        public string BusinessTypeExtension { get; set; }
        public string Vendor { get; set; }
        public string OperationalTradeChannel { get; set; }
        public string OperationalMarketType { get; set; }
        public string PricingProcedure { get; set; }
        public string PriceListType { get; set; }
        public string CustomerConditionGroup { get; set; }
        public string PriceGroup { get; set; }
        public Nullable<bool> BlockByCredit { get; set; }
        public Nullable<bool> OrderBlock { get; set; }
        public bool Active { get; set; }
        public Nullable<bool> HasUpdateAR { get; set; }
        public string AccountPartner { get; set; }
        public string ContactPersonFunction { get; set; }
        public string PartnerFunction { get; set; }
        public string TradeGroup { get; set; }
        public Nullable<decimal> Last3MonthsVolume { get; set; }
        public Nullable<decimal> P3MAVGSales { get; set; }
        //Address
        public Nullable<decimal> GeoCode { get; set; }
        public string Address { get; set; }
        public string CountryCode { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string CityCode { get; set; }
        public string Postal { get; set; }
        public string HouseNumber { get; set; }
        public string StreetNumber { get; set; }
        public string Street { get; set; }
        public string Street4 { get; set; }
        public string Street5 { get; set; }
        //Financial
        public string PaymentTerms { get; set; }
        public Nullable<decimal> CreditLimit { get; set; }
        public string OpenItemAgingStart { get; set; }
        public Nullable<decimal> CreditDays { get; set; }
        public Nullable<decimal> TotalOpenBalance { get; set; }
        public string CreditExposure { get; set; }
    }
}
