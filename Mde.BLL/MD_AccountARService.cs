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
    public partial class MD_AccountARService:BaseService<MD_AccountAR>,IMD_AccountARService
    {
        public List<ViewAccountARData> GetAccountARList(string account,ParameterQuery param)
        {
            var dataA = _dbSession.MD_AccountRepository.Entities;
            var dataAr = _dbSession.MD_AccountARRepository.Entities;
            if (!string.IsNullOrEmpty(account))
            {
                dataA = dataA.Where(p => p.AccountNumber.Contains(account) || p.Name.Contains(account));
            }
            var LstData = from a in dataA
                          join ar in dataAr on a.AccountNumber equals ar.ebMobile__Number__c
                          select new ViewAccountARData
                          {
                              AccountNumber = a.AccountNumber,
                              AccountName = a.Name,
                              CreditLimit = ar.ebMobile__CreditLimit__c,
                              InvoiceNumber = ar.ebMobile__InvoiceNumber__c,
                              InvoiceAmount = ar.ebMobile__InvoiceAmount__c,
                              InvoiceDate = ar.ebMobile__InvoiceDate__c,
                              OpenAmount = ar.ebMobile__OpenAmount__c,
                              TotalOpenAmount = ar.ebMobile__TotalOpenAmount__c,
                              BlockByCredit = ar.ebMobile__BlockByCredit__c
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
