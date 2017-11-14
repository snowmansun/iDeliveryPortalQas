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
    public partial class DSD_T_OdometerTrackingService : BaseService<DSD_T_OdometerTracking>, IDSD_T_OdometerTrackingService
    {
        public List<ViewOdometerTrackingData> LoadOdometerData(string Truck,string DateFrom,string DateTo,string Status,ParameterQuery param)
        {
            var tData = _dbSession.DSD_M_TruckRepository.Entities;
            var pData = _dbSession.MD_PersonRepository.Entities;
            var dData = _dbSession.MD_DictionaryRepository.Entities.Where(p => p.Category == "BizModel");
            var oData = _dbSession.DSD_T_OdometerTrackingRepository.Entities;

            if (!string.IsNullOrEmpty(Truck))
            {
                tData = tData.Where(p => p.TruckCode.Contains(Truck));
            }
            if (!string.IsNullOrEmpty(DateFrom))
            {
                DateTime date = DateTime.Parse(DateFrom);
                oData = oData.Where(p => p.TrackingTime >= date);
            }
            if (!string.IsNullOrEmpty(DateTo))
            {
                DateTime date = DateTime.Parse(DateTo).AddDays(1);
                oData = oData.Where(p => p.TrackingTime < date);
            }
            if (!string.IsNullOrEmpty(Status))
            {
                oData = oData.Where(p => p.TrackingAction == Status);
            }

            var data = from o in oData
                       join t in tData on o.TruckId equals t.ID
                       join d in dData on o.TrackingAction equals d.Value
                       join p in pData on o.Driver equals p.UserCode
                       select new ViewOdometerTrackingData
                       {
                           Id = o.Id,
                           Odometer = o.Odometer,
                           TrackingTime = o.TrackingTime,
                           TrackingAction = o.TrackingAction,
                           ActionDesc = d.Description,
                           ShipmentNo = o.ShipmentNo,
                           Driver = p.UserCode,
                           DriverName = p.FirstName+" "+p.LastName,
                           TruckId = o.TruckId,
                           TruckCode = t.TruckCode
                       };
            param.total = data.Count();
            if (param.pageIndex > 0)
            {
                data = data.OrderByDescending(p => p.TrackingTime)
                    .ThenBy(p => p.TruckCode)
                    .Skip((param.pageIndex - 1) * param.pageSize)
                    .Take(param.pageSize);
            }
            return data.ToList();

        }
    }
}
