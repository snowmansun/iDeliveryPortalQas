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
    public partial class DSD_T_ShipmentHeaderService:BaseService<DSD_T_ShipmentHeader>,IDSD_T_ShipmentHeaderService
    {
        public List<ViewShipmentTrackingData> LoadTrackingData(string ShipmentNo, string Driver, string ShipmentDate, string Status,string ShipmentType, ParameterQuery param)
        {
            var shData = _dbSession.DSD_T_ShipmentHeaderRepository.Entities;
            var tData = _dbSession.DSD_M_TruckRepository.Entities;
            var wData = _dbSession.MD_WarehouseRepository.Entities;
            var cData = _dbSession.MD_CompanyRepository.Entities;
            var pData = _dbSession.MD_PersonRepository.Entities;
            var dData = _dbSession.MD_DictionaryRepository.Entities.Where(p => p.Category == "ShipmentStatus");
            var mshData = _dbSession.DSD_M_ShipmentHeaderRepository.Entities;
            var dtData = _dbSession.MD_DictionaryRepository.Entities.Where(p => p.Category == "ShipmentType");

            if (!string.IsNullOrEmpty(ShipmentNo))
            {
                shData = shData.Where(p => p.ShipmentNo.Contains(ShipmentNo));
            }
            if (!string.IsNullOrEmpty(Driver))
            {
                pData = pData.Where(p => p.UserCode.Contains(Driver) || (p.LastName + " " + p.FirstName).Contains(Driver));
            }
            if (!string.IsNullOrEmpty(ShipmentDate))
            {
                DateTime date = DateTime.Parse(ShipmentDate);
                shData = shData.Where(p => p.ShipmentDate == date);
            }
            if (!string.IsNullOrEmpty(Status))
            {
                shData = shData.Where(p => p.Status == Status);
            }
            if (!string.IsNullOrEmpty(ShipmentType))
            {
                mshData = mshData.Where(p => p.ShipmentType == ShipmentType);
            }

            var LstData = from s in shData
                          join t in tData on s.TruckId equals t.ID into tmpT
                          from tt in tmpT.DefaultIfEmpty()
                          join w in wData on s.WarehouseCode equals w.Code into tmpW
                          from tw in tmpW.DefaultIfEmpty()
                          join c in cData on tw.CompanyCode equals c.CompanyCode into tmpC
                          from tc in tmpC.DefaultIfEmpty()
                          join p in pData on s.Driver equals p.UserCode
                          join d in dData on s.Status equals d.Value
                          join m in mshData on s.ShipmentNo equals m.ShipmentNo
                          join e in dtData on m.ShipmentType equals e.Value
                          select new ViewShipmentTrackingData
                          {
                              HeaderId = s.Id,
                              ShipmentDate = s.ShipmentDate,
                              ShipmentNo = s.ShipmentNo,
                              ShipmentType = e.Description,
                              Driver = (p.LastName + " " + p.FirstName),
                              Truck = tt.TruckCode,
                              Company = tc.CompanyName,
                              Warehouse = tw.Name,
                              Status = s.Status,
                              StatusDesc = d.Description,
                              CheckerConfirmTime = s.EndTime,
                              Checker = s.Checker,
                              CheckerSignImg = s.CheckerSignImg,
                              DCheckerSignImg = s.DCheckerSignImg,
                              Cashier = s.Cashier,
                              CashierSignImg = s.CashierSignImg,
                              DCashierSignImg = s.DCashierSignImg,
                              Gatekeeper = s.Gatekeeper,
                              GKSignImg = s.GKSignImg,
                              DGKSignImg = s.DGKSignImg
                          };
            param.total = LstData.Count();
            if (param.pageIndex > 0)
            {
                LstData = LstData.OrderBy(p => p.ShipmentDate)
                    .Skip((param.pageIndex - 1) * param.pageSize)
                    .Take(param.pageSize);
            }
            return LstData.ToList();
        }

        public List<ViewShipmentTrackingItemData> LoadTrackingItemData(string HeaderId, string Category)
        {
            string sql = @"SELECT p.ProductCode,p.Name ProductName,dic.Description DiffReason
	                            ,CAST(MAX(CASE si.ProductUnit WHEN 'CS' THEN si.PlanQty ELSE 0 END) AS VARCHAR)  +'/'+
	                            CAST(MAX(CASE si.ProductUnit WHEN 'EA' THEN si.PlanQty ELSE 0 END) AS VARCHAR) AS PlanQty
	                            ,CAST(MAX(CASE si.ProductUnit WHEN 'CS' THEN si.ActualQty ELSE 0 END) AS VARCHAR)  +'/'+
	                            CAST(MAX(CASE si.ProductUnit WHEN 'EA' THEN si.ActualQty ELSE 0 END) AS VARCHAR) AS ActualQty
	                            ,CAST(MAX(CASE si.ProductUnit WHEN 'CS' THEN si.DifferenceQty ELSE 0 END) AS VARCHAR)  +'/'+
	                            CAST(MAX(CASE si.ProductUnit WHEN 'EA' THEN si.DifferenceQty ELSE 0 END) AS VARCHAR) AS DifferenceQty
                            FROM dbo.DSD_T_ShipmentItem si
	                            INNER JOIN dbo.MD_Product p ON si.ProductCode=p.ProductCode
	                            LEFT JOIN dbo.MD_Dictionary dic ON dic.Value=si.DifferenceReason AND dic.Category=@Category
                            WHERE HeaderId=@HeaderId 
                            GROUP BY p.ProductCode,p.Name,dic.Description";
            List<DbParameter> listParams = new List<DbParameter>();
            SqlParameter paramQ = new SqlParameter() { ParameterName = "HeaderId", Value = HeaderId };
            listParams.Add(paramQ);
            paramQ = new SqlParameter() { ParameterName = "Category", Value = Category };
            listParams.Add(paramQ);
            IList<ViewShipmentTrackingItemData> LstData = _dbSession.ExecuteSqlNonQuery<ViewShipmentTrackingItemData>(sql, listParams.ToArray());
            var data = from d in LstData
                       select new ViewShipmentTrackingItemData
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
