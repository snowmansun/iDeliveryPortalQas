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
    public partial class MD_ProductService : BaseService<MD_Product>, IMD_ProductService
    {
        public List<ViewProductData> GetProductList(string Product, string Brand, string Pack, string Status, bool IsEmpty, bool Equipment, ParameterQuery param)
        {
            var dataProduct = _dbSession.MD_ProductRepository.Entities;
            var dataDictionary = _dbSession.MD_DictionaryRepository.Entities;
            if (!string.IsNullOrEmpty(Product))
            {
                dataProduct = dataProduct.Where(p => p.ProductCode.Contains(Product) || p.Name.Contains(Product));
            }
            if (!string.IsNullOrEmpty(Status))
            {
                dataProduct = dataProduct.Where(p => p.IsActive == (Status=="1"?true:false));
            }
            if (IsEmpty)
            {
                dataProduct = dataProduct.Where(p => p.ebMobile__IsEmpty__c == true);
            }
            if (!string.IsNullOrEmpty(Brand))
            {
                dataProduct = dataProduct.Where(p => p.ebMobile__Brand__c == Brand);
            }
            if (!string.IsNullOrEmpty(Pack))
            {
                dataProduct = dataProduct.Where(p => p.ebMobile__Pack__c == Pack);
            }

            var dataBrand = dataDictionary.Where(p => p.Category == "Brand");
            var dataPack = dataDictionary.Where(p => p.Category == "Pack");
            var dataFlavor = dataDictionary.Where(p => p.Category == "Flavor");
            var dataPackType = dataDictionary.Where(p => p.Category == "PackType");
            var dataCategory = dataDictionary.Where(p => p.Category == "BeverageCategory");
            var dataGroup = dataDictionary.Where(p => p.Category == "BeverageProduct");

            var LstData = from p in dataProduct
                       join a in dataBrand on p.ebMobile__Brand__c equals a.Value into brandTmp
                       from t1 in brandTmp.DefaultIfEmpty()
                       join b in dataPack on p.ebMobile__Pack__c equals b.Value into packTmp
                       from t2 in packTmp.DefaultIfEmpty()
                       join c in dataFlavor on p.ebMobile__Flavor__c equals c.Value into flavorTmp
                       from t3 in flavorTmp.DefaultIfEmpty()
                       join d in dataPackType on p.ebMobile__PackType__c equals d.Value into packTypeTmp
                       from t4 in packTypeTmp.DefaultIfEmpty()
                       join e in dataCategory on p.ebMobile__BeverageCategory__c equals e.Value into categoryTmp
                       from t5 in categoryTmp.DefaultIfEmpty()
                       join f in dataGroup on p.ebMobile__BeverageProduct__c equals f.Value into groupTmp
                       from t6 in groupTmp.DefaultIfEmpty()
                       select new ViewProductData
                       {
                           ProductCode = p.ProductCode,
                           ProductName = p.Name,
                           Brand = t1.Description,
                           Pack = t2.Description,
                           Flavor = t3.Description,
                           PackType = t4.Description,
                           IsEmpty = p.ebMobile__IsEmpty__c,
                           ProductGroup = t6.Description,
                           Category = t5.Description,
                           BaseUom = p.ebMobile__BaseUOM__c,
                           Package = p.ebMobile__Package__c,
                           Description = p.Description,
                           Status = p.IsActive,
                           CreateBy = p.CreatedBy,
                           LastModifiedBy = p.LastModifiedBy
                       };

            param.total = LstData.Count();
            if (param.pageIndex > 0)
            {
                LstData = LstData.OrderBy(p => p.ProductCode)
                    .Skip((param.pageIndex - 1) * param.pageSize)
                    .Take(param.pageSize);
            }
            return LstData.ToList();
            
        }
    }
}
