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
    public partial class DSD_T_DayTimeTrackingService : BaseService<DSD_T_DayTimeTracking>, IDSD_T_DayTimeTrackingService
    {
        public List<ViewSignatureData> GetStartOfDayPhotoList(string driver, DateTime date)
        {
            var dataTD = _dbSession.DSD_T_DayTimeTrackingRepository.Entities;
            var dataP = _dbSession.MD_PersonRepository.Entities;
            var dataR = _dbSession.Sys_RoleRepository.Entities;
            var dataRL= _dbSession.Sys_UserRoleLinkRepository.Entities;
            if (!string.IsNullOrEmpty(driver))
            {
                dataP = dataP.Where(p => p.UserCode.Contains(driver) || (p.FirstName + p.LastName).Contains(driver));
            }
            if (!string.IsNullOrEmpty(date.ToString()))
            {
                dataTD = dataTD.Where(p => p.TrackingTime >= date && p.TrackingTime < date.AddDays(1));
            }
            var LstData = from t in dataTD
                          join p in dataP on t.UserCode equals p.UserCode
                          join rl in dataRL on p.UserCode equals rl.UserCode
                          join r in dataR on rl.RoleID equals r.RoleID
                          select new ViewSignatureData
                          {
                              Title = r.Name,
                              Name = p.FirstName + ' ' + p.LastName,
                              Date = t.TrackingTime,
                              ImgName = t.SignImg
                          };

            return null;
        }
    }
}
