using Mde.Common;
using Mde.IBLL;
using Mde.Model.DataModel;
using Mde.Model.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.BLL
{
    public partial class MD_CompanyService : BaseService<MD_Company>,IMD_CompanyService
    {
        public List<ViewComboboxStringData> GetCompanyComboboxData()
        {
            var data = _dbSession.MD_CompanyRepository.Entities;
            var LstData = from d in data
                          select new ViewComboboxStringData
                          {
                              name = d.CompanyName,
                              value = d.CompanyCode
                          };
            return LstData.AsQueryable().ToList();
        }

        public List<MD_Company> GetCompanyList(string CompanyCode, string CompanyName, string Status,ParameterQuery param)
        {
            var data = _dbSession.MD_CompanyRepository.Entities;
            if (!string.IsNullOrEmpty(CompanyCode))
            {
                data = data.Where(p => p.CompanyCode.Contains(CompanyCode));
            }
            if (!string.IsNullOrEmpty(CompanyName))
            {
                data = data.Where(p => p.CompanyName.Contains(CompanyName));
            }
            if (!string.IsNullOrEmpty(Status))
            {
                data = data.Where(p => p.IsActive == (Status == "1" ? true : false));
            }
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

