using Mde.Common;
using Mde.IBLL;
using Mde.Model.DataModel;
using Mde.Model.ModelView;
using Mde.ModelView.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.BLL
{
    public partial class MD_WarehouseService:BaseService<MD_Warehouse>,IMD_WarehouseService
    {
        public List<ViewComboboxStringData> GetWareHouseComboBoxData(string CompanyCode)
        {
            var data = _dbSession.MD_WarehouseRepository.Entities;
            if (!string.IsNullOrEmpty(CompanyCode))
            {
                data = data.Where(p => p.CompanyCode == CompanyCode);
            }
            var LstData = from d in data
                          select new ViewComboboxStringData
                          {
                              name = d.Name,
                              value = d.Code
                          };
            return LstData.AsQueryable().ToList();
        }

        public List<ViewWarehouseData> GetWarehouseList(string Code, string Name, string CompanyCode, string Status, ParameterQuery param)
        {
            var dataW = _dbSession.MD_WarehouseRepository.Entities;
            var dataC = _dbSession.MD_CompanyRepository.Entities;
            if (!string.IsNullOrEmpty(Code))
            {
                dataW = dataW.Where(p => p.Code.Contains(Code));
            }
            if (!string.IsNullOrEmpty(Name))
            {
                dataW = dataW.Where(p => p.Name.Contains(Name));
            }
            if (!string.IsNullOrEmpty(CompanyCode))
            {
                dataW = dataW.Where(p => p.CompanyCode == CompanyCode);
            }
            if (!string.IsNullOrEmpty(Status))
            {
                dataW = dataW.Where(p => p.Valid == (Status == "1" ? true : false));
            }
            var data = from w in dataW
                       join c in dataC on w.CompanyCode equals c.CompanyCode into tmp
                       from tt in tmp.DefaultIfEmpty()
                       select new ViewWarehouseData
                       {
                           WarehouseCode = w.Code,
                           WarehouseName = w.Name,
                           CompanyCode = w.CompanyCode,
                           CompanyName = tt.CompanyName,
                           Barcode = w.Barcode,
                           Valid = w.Valid,
                           Remark = w.Remark
                       };
            param.total = data.Count();
            if (param.pageIndex > 0)
            {
                data = data.OrderBy(p => p.CompanyCode)
                    .Skip((param.pageIndex - 1) * param.pageSize)
                    .Take(param.pageSize);
            }
            return data.ToList();
        }
    }
}
