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
using System.Transactions;

namespace Mde.BLL
{
    public partial class DSD_M_TruckService:BaseService<DSD_M_Truck>,IDSD_M_TruckService
    {
        public List<ViewComboboxIntData> GetTruckList()
        {
            var data = _dbSession.DSD_M_TruckRepository.Entities;
            var LstData = (from d in data
                           select new ViewComboboxIntData
                           {
                               name = d.TruckCode,
                               value = d.ID
                           }).ToList();
            DateTime nowDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            foreach (ViewComboboxIntData item in LstData)
            {
                var assign = _dbSession.DSD_T_ShipmentAssignRepository.Entities.Where(p => p.TruckID == item.value && p.WorkDay == nowDate);
                item.name = item.name + "(" + assign.Count() + ")";
            }
            return LstData;
        }

        public List<ViewTruckDriverData> GetTruckDriverList(string CompanyCode, string TruckType, string TruckNumber, string Status, ParameterQuery param)
        {
            var dataT = _dbSession.DSD_M_TruckRepository.Entities;
            var dataTD = _dbSession.DSD_M_TruckDriverRepository.Entities;
            var dataC = _dbSession.MD_CompanyRepository.Entities;
            var dataD = _dbSession.MD_DictionaryRepository.Entities.Where(p => p.Category == "TruckType");
            if (!string.IsNullOrEmpty(CompanyCode))
            {
                dataT = dataT.Where(p => p.CompanyCode == CompanyCode);
            }
            if (!string.IsNullOrEmpty(TruckType))
            {
                dataT = dataT.Where(p => p.Type == TruckType);
            }
            if (!string.IsNullOrEmpty(TruckNumber))
            {
                dataT = dataT.Where(p => p.TruckCode.Contains(TruckNumber));
            }
            if (!string.IsNullOrEmpty(Status))
            {
                dataT = dataT.Where(p => p.Status == (Status == "1" ? true : false));
            }
            var LstData = from t in dataT
                          join td in dataTD on t.ID equals td.TruckId
                          //from ttTD in tmpTD.DefaultIfEmpty()
                          join d in dataD on t.Type equals d.Value into tmpD
                          from ttD in tmpD.DefaultIfEmpty()
                          join c in dataC on t.CompanyCode equals c.CompanyCode into tmp
                          from tt in tmp.DefaultIfEmpty()
                          select new ViewTruckDriverData
                          {
                              TruckId = t.ID,
                              CompanyCode = t.CompanyCode,
                              Company = tt.CompanyName,
                              TruckNumber = t.TruckCode,
                              Driver = td.Driver,
                              Type = ttD.Value,
                              Capacity = t.Capacity,
                              Volume = t.Volume,
                              Status = t.Status
                          };
            param.total = LstData.Count();

            if (param.pageIndex > 0)
            {
                LstData = LstData.OrderBy(p => p.TruckNumber)
                    .Skip((param.pageIndex - 1) * param.pageSize)
                    .Take(param.pageSize);
            }
            return LstData.ToList();
        }

        public DSD_M_TruckDriver CheckData(int TruckId)
        {
            return _dbSession.DSD_M_TruckDriverRepository.Entities.Where(p => p.TruckId == TruckId).FirstOrDefault();
        }

        public void UpdateTruckDriver(DSD_M_TruckDriver item)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var data = _dbSession.DSD_M_TruckDriverRepository.Entities.Where(p => p.Driver == item.Driver).FirstOrDefault();
                if (data != null)
                {
                    data.Driver = null;
                    _dbSession.DSD_M_TruckDriverRepository.UpdateEntities(data);
                    _dbSession.DSD_M_TruckDriverRepository.Commit();
                }

                _dbSession.DSD_M_TruckDriverRepository.UpdateEntities(item);
                _dbSession.DSD_M_TruckDriverRepository.Commit();

                scope.Complete();
            }
        }


        public void SaveTruckDriver(DSD_M_TruckDriver item)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var data = _dbSession.DSD_M_TruckDriverRepository.Entities.Where(p => p.Driver == item.Driver).FirstOrDefault();
                if (data != null)
                {
                    data.Driver = null;
                    _dbSession.DSD_M_TruckDriverRepository.UpdateEntities(data);
                    _dbSession.DSD_M_TruckDriverRepository.Commit();
                }

                _dbSession.DSD_M_TruckDriverRepository.AddEntities(item);
                _dbSession.DSD_M_TruckDriverRepository.Commit();

                scope.Complete();
            }
        }
    }
}
