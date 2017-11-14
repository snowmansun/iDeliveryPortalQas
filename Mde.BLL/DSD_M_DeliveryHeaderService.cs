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
    public partial class DSD_M_DeliveryHeaderService : BaseService<DSD_M_DeliveryHeader>, IDSD_M_DeliveryHeaderService
    {
        public List<ViewDeliveryNoteData> GetDeliveryNoteData(string DeliveryDate, string ShipmentNo, string DeliveryNo, string Driver, string TruckNo, string Status,string DeliveryType, ParameterQuery param)
        {
            string sql = @"SELECT mdh.ShipmentNo,mdh.DeliveryNo,CONVERT(VARCHAR(10),mdh.PlanDeliveryDate,120) DeliveryDate,tsa.Driver,mt.TruckCode,ma.Name Customer,mdh.DeliveryAddress Address
		                            ,mdh.Contact,COUNT(DISTINCT mdi.ProductCode) Products,ISNULL(mdh.NetPrice,0) Invoice,md.Description Status,mdType.Description DeliveryType,
                                    CAST(SUM(CASE WHEN mdi.ProductUnit='CS' THEN mdi.PlanQty ELSE 0 END) AS VARCHAR) + '/'
                                    + CAST(SUM(CASE WHEN mdi.ProductUnit='EA' THEN mdi.PlanQty ELSE 0 END) AS VARCHAR) ProductQty
                            FROM dbo.DSD_M_DeliveryHeader mdh 
	                            LEFT JOIN dbo.DSD_T_ShipmentAssign tsa ON mdh.ShipmentNo=tsa.ShipmentNO
	                            LEFT JOIN dbo.DSD_M_Truck mt ON mt.ID=tsa.TruckID
	                            LEFT JOIN dbo.DSD_M_DeliveryItem mdi ON mdh.DeliveryNo=mdi.DeliveryNo
	                            LEFT JOIN dbo.MD_Dictionary md ON md.Category='DeliveryStatus' AND md.Value=mdh.DeliveryStatus 
                                LEFT JOIN dbo.MD_Dictionary mdType ON mdType.Category='DeliveryNoteType' AND mdType.Value=mdh.DeliveryType 
	                            LEFT JOIN dbo.MD_Account ma ON ma.AccountNumber=mdh.AccountNumber";

            if (!string.IsNullOrEmpty(Driver))
            {
                sql = sql + "  LEFT JOIN dbo.MD_Person mp ON mp.UserCode=tsa.Driver ";
            }
            sql = sql + " WHERE 1=1 ";
            List<DbParameter> listParams = new List<DbParameter>();
            SqlParameter paramQ;
            if (!string.IsNullOrEmpty(DeliveryDate))
            {
                sql = sql + " AND mdh.PlanDeliveryDate=@DeliveryDate ";
                paramQ = new SqlParameter()
                {
                    ParameterName = "DeliveryDate",
                    Value = DateTime.Parse(DeliveryDate)
                };
                listParams.Add(paramQ);
            }
            if (!string.IsNullOrEmpty(ShipmentNo))
            {
                sql = sql + " AND mdh.ShipmentNo LIKE '%'+@ShipmentNo+'%' ";
                paramQ = new SqlParameter()
                {
                    ParameterName = "ShipmentNo",
                    Value = ShipmentNo
                };
                listParams.Add(paramQ);
            }
            if (!string.IsNullOrEmpty(DeliveryNo))
            {
                sql = sql + " AND mdh.DeliveryNo LIKE '%'+@DeliveryNo+'%' ";
                paramQ = new SqlParameter()
                {
                    ParameterName = "DeliveryNo",
                    Value = DeliveryNo
                };
                listParams.Add(paramQ);
            }
            if (!string.IsNullOrEmpty(Driver))
            {
                sql = sql + " AND (tsa.Driver LIKE '%'+@Driver+'%' OR (mp.FirstName+mp.LastName) LIKE '%'+@Driver+'%') ";
                paramQ = new SqlParameter()
                {
                    ParameterName = "Driver",
                    Value = Driver
                };
                listParams.Add(paramQ);
            }
            if (!string.IsNullOrEmpty(TruckNo))
            {
                sql = sql + " AND mt.TruckCode LIKE '%'+@TruckNo+'%' ";
                paramQ = new SqlParameter()
                {
                    ParameterName = "TruckNo",
                    Value = TruckNo
                };
                listParams.Add(paramQ);
            }
            if (!string.IsNullOrEmpty(Status))
            {
                sql = sql + " AND mdh.DeliveryStatus = @Status ";
                paramQ = new SqlParameter()
                {
                    ParameterName = "Status",
                    Value = Status
                };
                listParams.Add(paramQ);
            }
            if (!string.IsNullOrEmpty(DeliveryType))
            {
                sql = sql + " AND mdh.DeliveryType = @DeliveryType";
                paramQ = new SqlParameter()
                {
                    ParameterName = "DeliveryType",
                    Value = DeliveryType
                };
                listParams.Add(paramQ);
            }

            sql = sql + " GROUP BY mdh.ShipmentNo,mdh.DeliveryNo,mdh.PlanDeliveryDate,tsa.Driver,mt.TruckCode,mdh.DeliveryAddress,mdh.Contact,mdh.NetPrice,md.Description,mdType.Description,ma.Name ";
            IList<ViewDeliveryNoteData> LstData = _dbSession.ExecuteSqlNonQuery<ViewDeliveryNoteData>(sql, listParams.ToArray());
            var data = from d in LstData
                       select new ViewDeliveryNoteData
                       {
                           ShipmentNo = d.ShipmentNo,
                           DeliveryNo = d.DeliveryNo,
                           DeliveryType = d.DeliveryType,
                           DeliveryDate = d.DeliveryDate,
                           Driver = d.Driver,
                           TruckCode = d.TruckCode,
                           Customer = d.Customer,
                           Address = d.Address,
                           Contact = d.Contact,
                           Products = d.Products,
                           ProductQty = d.ProductQty,
                           Invoice = d.Invoice,
                           Status = d.Status
                       };

            param.total = data.Count();

            if (param.pageIndex > 0)
            {
                data = data.OrderBy(p => p.ShipmentNo)
                    .Skip((param.pageIndex - 1) * param.pageSize)
                    .Take(param.pageSize);
            }
            return LstData.AsQueryable().ToList();
        }

        public List<ViewDeliveryItemData> GetDeliveryItemData(string DeliveryNo)
        {
            var data = _dbSession.DSD_M_DeliveryItemRepository.Entities.Where(p => p.DeliveryNo == DeliveryNo);
            var dataPro = _dbSession.MD_ProductRepository.Entities;

            var LstData = from d in data
                          join p in dataPro on d.ProductCode equals p.ProductCode
                          select new ViewDeliveryItemData
                          {
                              ProductCode = d.ProductCode,
                              ProductName = p.Name,
                              ProductUnit = d.ProductUnit,
                              PlanQty = d.PlanQty
                          };
            return LstData.ToList();
        }
    }
}
