using Mde.Common;
using Mde.IBLL;
using Mde.Model.DataModel;
using Mde.ModelView.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.BLL
{
    public partial class MD_AccountService:BaseService<MD_Account>,IMD_AccountService
    {
        public List<ViewAccountData> GetAccoutListData(string CompanyCode,string Account,ParameterQuery param)
        {
            var data = _dbSession.MD_AccountRepository.Entities;
            if (!string.IsNullOrEmpty(CompanyCode))
            {
                data = data.Where(p => p.ebMobile__CompanyCode__c == CompanyCode);
            }
            if (!string.IsNullOrEmpty(Account))
            {
                data = data.Where(p => p.AccountNumber.Contains(Account) || p.Name.Contains(Account));
            }
            var dicData = _dbSession.MD_DictionaryRepository.Entities;
            var shippingData = dicData.Where(p => p.Category == "ShippingCondition");
            var LstData = from d in data
                          join s in shippingData on d.ebMobile__ShippingCondition__c equals s.Value into tmp
                          from tt in tmp.DefaultIfEmpty()
                          select new ViewAccountData { 
                            AccountName=d.Name,
                            AccountName1=d.ebMobile__AccountName1__c,
                            AccountName2=d.ebMobile__AccountName2__c,
                            Owner=d.Owner,
                            RecordType=d.RecordType,
                            AccountGroup=d.ebMobile__AccountGroup__c,
                            AccountParent=d.Parent,
                            AccountNumber=d.AccountNumber,
                            ERPAccountNumber=d.ebMobile__ERPAccountNumber__c,
                            TaxNumber=d.ebMobile__TaxNumber__c,
                            VATNumber=d.ebMobile__VATNumber__c,
                            SuspressionCode=d.ebMobile__SuspressionCode__c,
                            SuppressionReason=d.ebMobile__SuppressionReason__c,
                            CompanyCode=d.ebMobile__CompanyCode__c,
                            SalesOrg=d.ebMobile__SalesOrg__c,
                            SalesOrgCode=d.ebMobile__SalesOrgCode__c,
                            SalesDistrict=d.ebMobile__SalesDistrict__c,
                            SalesGroup=d.ebMobile__SaleGroup__c,
                            SalesOffice=d.ebMobile__SalesOffice__c,
                            VisitStartDate=d.ebMobile__VisitStartDate__c,
                            SubChannel=d.ebMobile__SubTradeChannel__c,
                            SalesRoute=d.ebMobile__SalesRoute__c,
                            ShippingCondition=d.ebMobile__ShippingCondition__c,
                            InvoiceCalendar=d.ebMobile__InvoiceCalendar__c,
                            DeliveryDate=d.ebMobile__DeliveryDate__c,
                            DeliveryDays=d.ebMobile__DeliveryDays__c,
                            DiscountIndicator=d.ebMobile__DiscountIndicator__c,
                            BillTo=d.ebMobile__BillTo__c,
                            IsPush=d.IsPush__c,
                            RedIndecator=d.RedIndecator__c,
                            Ownership=d.Ownership,
                            AccountSource=d.AccountSource,
                            //Classification=,
                            TradeChannel=d.ebMobile__TradeChannel__c,
                            SubTradeChannel=d.ebMobile__SubTradeChannel__c,
                            TradeName=d.ebMobile__TradeName__c,
                            Currency=d.ebMobile__Currency__c,
                            DeliveringPlant=d.ebMobile__DeliveringPlant__c,
                            MEPCustomerNumber=d.ebMobile__MEPCustomerNumber__c,
                            //BusinessType=,
                            BusinessTypeExtension=d.ebMobile__BusinessTypeExtension__c,
                            Vendor=d.ebMobile__Vendor__c,
                            OperationalTradeChannel=d.ebMobile__OperationalTradeChannel__c,
                            OperationalMarketType=d.ebMobile__OperationalMarketType__c,
                            PricingProcedure=d.ebMobile__PricingProcedure__c,
                            PriceListType=d.ebMobile__PriceListType__c,
                            CustomerConditionGroup=d.ebMobile__CustomerConditionGroup__c,
                            PriceGroup=d.ebMobile__PriceGroup__c,
                            BlockByCredit=d.ebMobile__BlockByCredit__c,
                            OrderBlock=d.ebMobile__OrderBlock__c,
                            Active=d.ebMobile__IsActive__c,
                            HasUpdateAR=d.HasUpdatedAR__c,
                            AccountPartner=d.ebMobile__AccountPartner__c,
                            ContactPersonFunction=d.ebMobile__ContactPersonFunction__c,
                            PartnerFunction=d.ebMobile__PartnerFunction__c,
                            TradeGroup=d.ebMobile__TradeGroup__c,
                            Last3MonthsVolume=d.Last3MonthsVolume__c,
                            P3MAVGSales=d.ebMobile__P3MAVGSales__c,
                            GeoCode=d.ebMobile__GeoCode__c,
                            Address=d.ebMobile__Address__c,
                            CountryCode=d.ebMobile__CountryCode__c,
                            Country=d.ebMobile__Country__c,
                            Region=d.ebMobile__Region__c,
                            District=d.ebMobile__District__c,
                            CityCode=d.ebMobile__CityCode__c,
                            Postal=d.ebMobile__Postal__c,
                            HouseNumber=d.ebMobile__HouseNumber__c,
                            StreetNumber=d.ebMobile__StreetNumber__c,
                            Street=d.ebMobile__Street__c,
                            Street4=d.ebMobile__Street4__c,
                            Street5=d.ebMobile__Street5__c,
                            PaymentTerms=d.ebMobile__PaymentTerms__c,
                            CreditLimit=d.ebMobile__CreditLimit__c,
                            OpenItemAgingStart=d.ebMobile__OpenItemsAgingStart__c,
                            CreditDays=d.ebMobile__CreditDays__c,
                            TotalOpenBalance = d.ebMobile__AvailableBalance__c
                            //CreditExposure=
                          };

            param.total = LstData.Count();
            if (param.pageIndex > 0)
            {
                LstData = LstData.OrderBy(p => p.AccountNumber)
                    .Skip((param.pageIndex - 1) * param.pageSize)
                    .Take(param.pageSize);
            }
            return LstData.ToList();
        }
    }
}
