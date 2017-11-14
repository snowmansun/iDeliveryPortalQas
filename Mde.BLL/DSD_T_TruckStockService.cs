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
    public partial class DSD_T_TruckStockService : BaseService<DSD_T_TruckStock>, IDSD_T_TruckStockService
    {
        public List<ViewTruckStockData> GetTruckStockData(string Truck, string ShipmentNo, string Product,string StockDate, ParameterQuery param)
        {
            string sql = @"SELECT ts.TruckId,t.TruckCode,ts.ShipmentNo,p.ProductCode,p.Name ProductName
	                        ,CAST(MAX(CASE ts.ProductUnit WHEN 'CS' THEN ts.StockQty ELSE 0 END) AS VARCHAR) + '/'
	                        +CAST(MAX(CASE ts.ProductUnit WHEN 'EA' THEN ts.StockQty ELSE 0 END) AS VARCHAR) StockQty
                        FROM dbo.DSD_T_TruckStock ts
                            LEFT JOIN dbo.DSD_M_Truck t ON ts.TruckId=t.ID
                            LEFT JOIN dbo.MD_Product p ON ts.ProductCode=p.ProductCode
                        WHERE 1=1 ";
            List<DbParameter> listParams = new List<DbParameter>();
            if (!string.IsNullOrEmpty(Truck))
            {
                SqlParameter paramQ = new SqlParameter() { ParameterName = "Truck", Value = Truck };
                listParams.Add(paramQ);
                sql = sql + "AND t.TruckCode LIKE '%'+@Truck+'%' ";
            }
            if (!string.IsNullOrEmpty(ShipmentNo))
            {
                SqlParameter paramQ = new SqlParameter() { ParameterName = "ShipmentNo", Value = ShipmentNo };
                listParams.Add(paramQ);
                sql = sql + "AND ts.ShipmentNo LIKE '%'+ @ShipmentNo +'%' ";
            }
            if (!string.IsNullOrEmpty(Product))
            {
                SqlParameter paramQ = new SqlParameter() { ParameterName = "Product", Value = Product };
                listParams.Add(paramQ);
                sql = sql + "AND (p.ProductCode LIKE '%'+@Product+'%' OR p.Name LIKE '%'+@Product+'%') ";
            }
            if (!string.IsNullOrEmpty(StockDate))
            {
                DateTime sDate = DateTime.Parse(StockDate);
                SqlParameter paramQ = new SqlParameter() { ParameterName = "StockDate", Value = sDate };
                listParams.Add(paramQ);
                sql = sql + "AND ts.CreateTime >= @StockDate  AND ts.CreateTime < (@StockDate+1)";

            }
            sql = sql + " GROUP BY ts.TruckId,t.TruckCode,ts.ShipmentNo,p.ProductCode,p.Name";
            IList<ViewTruckStockData> LstData = _dbSession.ExecuteSqlNonQuery<ViewTruckStockData>(sql, listParams.ToArray());
            var data = from d in LstData
                       select new ViewTruckStockData
                       {
                           TruckId = d.TruckId,
                           TruckCode = d.TruckCode,
                           ShipmentNo = d.ShipmentNo,
                           ProductCode = d.ProductCode,
                           ProductName = d.ProductName,
                           StockQty = d.StockQty
                       };
            param.total = data.Count();
            if (param.pageIndex > 0)
            {
                data = data.OrderByDescending(p => p.ShipmentNo)
                    .Skip((param.pageIndex - 1) * param.pageSize)
                    .Take(param.pageSize);
            }
            return data.AsQueryable().ToList();
        }

        public List<ViewStockTrackingData> GetTruckStockTrackingData(int TruckId, string ShipmentNo, string ProductCode)
        {
            var tData = _dbSession.DSD_T_TruckStockTrackingRepository.Entities;
            tData = tData.Where(p => p.TruckId == TruckId);
            tData = tData.Where(p => p.ShipmentNo == ShipmentNo);
            tData = tData.Where(p => p.ProductCode == ProductCode);
            var dicData = _dbSession.MD_DictionaryRepository.Entities.Where(p => p.Category == "StockTracking");

            var data = from t in tData
                       join d in dicData on t.ChangeAction equals d.Value
                       select new ViewStockTrackingData
                       {
                           ProductCode = t.ProductCode,
                           ProductUnit = t.ProductUnit,
                           ChangeAction = d.Description,
                           TrackingTime = t.TrackingTime,
                           FromQty = t.FromQty,
                           ToQty = t.ToQty,
                           ChangeQuantity = t.ChangeQuantity
                       };
            data = data.OrderBy(p => p.TrackingTime);
            return data.ToList();
        }
    }
}
