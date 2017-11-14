using Mde.Common;
using Mde.IBLL;
using Mde.Model.DataModel;
using Mde.ModelView.ModelView;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.BLL
{
    public partial class DSD_T_DeliveryHeaderService:BaseService<DSD_T_DeliveryHeader>,IDSD_T_DeliveryHeaderService
    {
        public List<ViewDeliveryHeaderData> LoadDelTrackingData(string ShipmentNo, string DeliveryNo, string DeliveryDate, string Account, string Status,string DeliveryType,ParameterQuery param)
        {
            var dhData = _dbSession.DSD_T_DeliveryHeaderRepository.Entities;
            var aData = _dbSession.MD_AccountRepository.Entities;
            var dicData = _dbSession.MD_DictionaryRepository.Entities.Where(p => p.Category == "DeliveryStatus");
            var pData = _dbSession.MD_PersonRepository.Entities;
            //var sData = _dbSession.DSD_T_ShipmentHeaderRepository.Entities;
            var dicType = _dbSession.MD_DictionaryRepository.Entities.Where(p => p.Category == "DeliveryNoteType");

            if (!string.IsNullOrEmpty(ShipmentNo))
            {
                dhData = dhData.Where(p => p.ShipmentNo.Contains(ShipmentNo));
            }
            if (!string.IsNullOrEmpty(DeliveryNo))
            {
                dhData = dhData.Where(p => p.DeliveryNo.Contains(DeliveryNo));
            }
            if (!string.IsNullOrEmpty(DeliveryDate))
            {
                DateTime date = DateTime.Parse(DeliveryDate);
                dhData = dhData.Where(p => p.DeliveryDate == date);
            }
            if (!string.IsNullOrEmpty(Account))
            {
                aData = aData.Where(p => p.AccountNumber.Contains(Account) || p.Name.Contains(Account));
            }
            if (!string.IsNullOrEmpty(Status))
                dhData = dhData.Where(p => p.DeliveryStatus == Status);

            if (!string.IsNullOrEmpty(DeliveryType))
            {
                dhData = dhData.Where(p => p.DeliveryType == DeliveryType);
            }

            var LstData = from dh in dhData
                          join a in aData on dh.AccountNumber equals a.AccountNumber
                          join d in dicData on dh.DeliveryStatus equals d.Value
                          join p in pData on dh.CreateUser equals p.UserCode
                          //join s in sData on dh.ShipmentNo equals s.ShipmentNo
                          join t in dicType on dh.DeliveryType equals t.Value
                          select new ViewDeliveryHeaderData
                          {
                              ShipmentNo = dh.ShipmentNo,
                              DeliveryNo = dh.DeliveryNo,
                              DeliveryDate = dh.DeliveryDate,
                              AccountNumber = dh.AccountNumber,
                              AccountName = a.Name,
                              Status = dh.DeliveryStatus,
                              StatusDesc = d.Description,
                              CustomerSignDate = dh.EndTime,
                              CustomerSignImg = dh.CustomerSignImg,
                              DriverSignImg = dh.DriverSignImg,
                              DriverName = p.LastName + " " + p.FirstName,
                              DeliveryType = t.Description
                          };
            param.total = LstData.Count();
            if (param.pageIndex > 0)
            {
                LstData = LstData.OrderBy(p => p.DeliveryNo)
                    .Skip((param.pageIndex - 1) * param.pageSize)
                    .Take(param.pageSize);
            }
            return LstData.ToList();
        }

        public List<ViewDicTrackingData> LoadDelTrackingItemData(string DeliveryNo)
        {

            string sql = @"SELECT p.ProductCode,p.Name ProductName,dic.Description DiffReason
	                            ,CAST(MAX(CASE si.ProductUnit WHEN 'CS' THEN si.PlanQty ELSE 0 END) AS VARCHAR)  +'/'+
	                            CAST(MAX(CASE si.ProductUnit WHEN 'EA' THEN si.PlanQty ELSE 0 END) AS VARCHAR) AS PlanQty
	                            ,CAST(MAX(CASE si.ProductUnit WHEN 'CS' THEN si.ActualQty ELSE 0 END) AS VARCHAR)  +'/'+
	                            CAST(MAX(CASE si.ProductUnit WHEN 'EA' THEN si.ActualQty ELSE 0 END) AS VARCHAR) AS ActualQty
	                            ,CAST(MAX(CASE si.ProductUnit WHEN 'CS' THEN si.DifferenceQty ELSE 0 END) AS VARCHAR)  +'/'+
	                            CAST(MAX(CASE si.ProductUnit WHEN 'EA' THEN si.DifferenceQty ELSE 0 END) AS VARCHAR) AS DifferenceQty
                            FROM dbo.DSD_T_DeliveryItem si
	                            INNER JOIN dbo.MD_Product p ON si.ProductCode=p.ProductCode
	                            LEFT JOIN dbo.MD_Dictionary dic ON dic.Value=si.Reason AND dic.Category='DelDiffReason'
                            WHERE si.DeliveryNo=@DeliveryNo
                            GROUP BY p.ProductCode,p.Name,dic.Description";
            List<DbParameter> listParams = new List<DbParameter>();
            SqlParameter paramQ = new SqlParameter() { ParameterName = "DeliveryNo", Value = DeliveryNo };
            listParams.Add(paramQ);
            IList<ViewDicTrackingData> LstData = _dbSession.ExecuteSqlNonQuery<ViewDicTrackingData>(sql, listParams.ToArray());
            var data = from d in LstData
                       select new ViewDicTrackingData
                       {
                           ProductCode = d.ProductCode,
                           ProductName = d.ProductName,
                           PlanQty = d.PlanQty,
                           ActualQty = d.ActualQty,
                           DifferenceQty = d.DifferenceQty,
                           DiffReason = d.DiffReason
                       };
            return data.AsQueryable().ToList();
        }
    }
}
