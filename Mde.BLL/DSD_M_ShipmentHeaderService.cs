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
using System.Transactions;

namespace Mde.BLL
{
    public partial class DSD_M_ShipmentHeaderService:BaseService<DSD_M_ShipmentHeader>,IDSD_M_ShipmentHeaderService
    {
        #region
        public const string sqlShipmentInfo = @"SELECT a.*,b.StockOnTruck,CAST(c.Customer AS VARCHAR)+'/'+CAST(c.Visited AS VARCHAR) CustomerAndVisited
                            FROM (
                                SELECT tsa.TruckID,t.TruckCode,COUNT(tsa.ShipmentNO) ShipmentCnt
                                FROM dbo.DSD_T_ShipmentAssign tsa
	                                INNER JOIN dbo.DSD_M_Truck t ON tsa.TruckID=t.ID
                                WHERE tsa.WorkDay=@ShipmentDate --and tsa.TruckId='1005'
                                GROUP BY tsa.TruckID,t.TruckCode
                            ) a
                            LEFT JOIN (
                                SELECT b.TruckId,CAST(SUM(b.CSCNT) AS VARCHAR)+' CS ' + CAST(SUM(CAST(b.EACNT AS INT)) AS VARCHAR) + ' EA' StockOnTruck
                                FROM (
		                            SELECT a.TruckId,a.ProductCode,SUM(CAST(a.Stock AS INT)/CAST(a.ebMobile__Denominator__c AS INT)) CSCNT
			                            ,SUM(a.Stock%a.ebMobile__Denominator__c) EACNT
		                            FROM (
				                            SELECT ts.TruckId,ts.ProductCode,ts.ProductUnit,SUM(ts.StockQty) Stock,pu.ebMobile__Denominator__c
				                            FROM dbo.DSD_T_TruckStock ts
						                            INNER JOIN dbo.MD_ProductUOM pu ON ts.ProductCode=pu.ebMobile__ProductCode__c AND ts.ProductUnit=pu.ebMobile__UOMCode__c
						                            INNER JOIN DSD_T_ShipmentAssign tsa ON tsa.TruckID=ts.TruckId
				                            WHERE ts.StockQty >0 AND tsa.WorkDay=@ShipmentDate --and tsa.TruckId='1005'
				                            GROUP BY ts.TruckId,ts.ProductCode,ts.ProductUnit,pu.ebMobile__Denominator__c
			                            ) a 
		                            GROUP BY a.ProductCode,a.TruckId
	                            ) b
                            GROUP BY b.TruckId
                            ) b ON a.TruckId=b.TruckID
                            LEFT JOIN (
                                SELECT tsa.TruckId,COUNT(DISTINCT mdh.AccountNumber) Customer,COUNT(DISTINCT v.AccountNumber) Visited
                                FROM DSD_T_ShipmentAssign tsa 
		                                LEFT JOIN dbo.DSD_M_DeliveryHeader mdh ON mdh.ShipmentNo = tsa.ShipmentNo
		                                LEFT JOIN dbo.DSD_T_Visit v ON v.ShipmentNo = tsa.ShipmentNO
                                WHERE  tsa.WorkDay=@ShipmentDate
                                GROUP BY tsa.TruckID
                            ) c ON a.TruckId=c.TruckID";

        public const string sqlFinance = @"SELECT a.PaymentType,dic.Description PaymentTypeName,CAST(SUM(a.PaymentAmount+ISNULL(b.PaymentAmount,0)+ISNULL(c.PaymentAmount,0)) AS DECIMAL(18,2)) PaymentAmount
                                            FROM 
                                            (
                                                SELECT tsh.ShipmentNo,tsf.Type PaymentType,tsf.ActualAmount PaymentAmount
                                                FROM dbo.DSD_T_ShipmentAssign tsa
	                                                INNER JOIN dbo.DSD_T_ShipmentHeader tsh ON tsa.ShipmentNO=tsh.ShipmentNo
	                                                INNER JOIN dbo.DSD_T_ShipmentFinance tsf ON tsh.Id = tsf.HeaderId
                                                WHERE tsa.WorkDay=@ShipmentDate AND tsa.TruckID=@TruckId
                                            ) a
                                            LEFT JOIN (
                                                SELECT tsh.ShipmentNo,tdb.PaymentType,CAST(SUM(tdb.PaymentAmount+tdb.PaymentDeposit) AS DECIMAL(18,2)) PaymentAmount
                                                FROM dbo.DSD_T_ShipmentAssign tsa
	                                                INNER JOIN dbo.DSD_T_ShipmentHeader tsh ON tsa.ShipmentNO=tsh.ShipmentNo
	                                                INNER JOIN dbo.DSD_T_DeliveryHeader tdh ON tdh.ShipmentNo=tsh.ShipmentNo
	                                                INNER JOIN dbo.DSD_T_DeliveryBilling tdb ON tdb.DeliveryNo=tdh.DeliveryNo
                                                WHERE tsa.WorkDay=@ShipmentDate AND tsa.TruckID=@TruckId
                                                GROUP BY tsh.ShipmentNo,tdb.PaymentType
                                            ) b ON a.ShipmentNo=b.ShipmentNo AND a.PaymentType=b.PaymentType
                                            LEFT JOIN (
                                                SELECT tsa.ShipmentNO,tp.ebMobile__PaymentType__c PayentType,tp.ebMobile__Amount__c PaymentAmount
                                                FROM DSD_T_ShipmentAssign tsa 
	                                                INNER JOIN dbo.DSD_T_Visit v ON tsa.ShipmentNO=v.ShipmentNo
	                                                INNER JOIN dbo.DSD_T_ARCollection tac ON tac.ebMobile__VisitID__c=v.VisitId
	                                                INNER JOIN dbo.DSD_T_Payment tp ON tp.ebMobile__ARCollection__c=tac.ID
                                                WHERE tsa.WorkDay=@ShipmentDate AND tsa.TruckID=@TruckId
                                            ) c ON a.ShipmentNo=c.ShipmentNO AND a.PaymentType=c.PayentType
                                            LEFT JOIN dbo.MD_Dictionary dic ON a.PaymentType=dic.Value AND dic.Category='PaymentMethod'
                                            GROUP BY a.PaymentType,dic.Description";

        public const string DriverRouteCustomerData = @"SELECT a.AccountNumber CustomerId,a.Name CustomerName,td.Driver DriverId,v.Latitude,v.Longitude,v.VisitStatus
                                                        FROM DSD_T_Visit v
		                                                        INNER JOIN dbo.DSD_M_TruckDriver td ON td.Driver=v.UserCode
		                                                        INNER JOIN dbo.MD_Account a ON v.AccountNumber=a.AccountNumber
		                                                        INNER JOIN dbo.MD_Person p ON td.Driver=p.UserCode
                                                        WHERE CONVERT(VARCHAR(10),v.StartTime,120)=@ShipmentDate AND td.TruckId=@TruckId
                                                        ORDER BY v.StartTime";
        #endregion

        /// <summary>
        /// 获取shipment list
        /// </summary>
        /// <param name="ShipmentDate"></param>
        /// <param name="ShipmentNo"></param>
        /// <param name="Warehouse"></param>
        /// <param name="Driver"></param>
        /// <param name="TruckNo"></param>
        /// <param name="Status"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<ViewShipmentListData> GetShipmentListData(string ShipmentDate, string ShipmentNo, string Warehouse, string Driver, string TruckNo, string Status,ParameterQuery param)
        {
            string sql = @"SELECT msh.ShipmentNo,w.Name WarehouseName,tsa.Driver,mt.ID TruckID,mt.TruckCode,msh.ShipmentType,
		                        COUNT(DISTINCT msi.ProductCode) ProductNum,CAST(SUM(CASE WHEN msi.ProductUnit='CS' then msi.PlanQty ELSE 0 END) AS VARCHAR)+'/'
		                        +CAST(SUM(CASE WHEN msi.ProductUnit='EA' THEN msi.PlanQty ELSE 0 END) AS VARCHAR) ProductQty
                                ,ISNULL(a.Customers,0) CustomerNum,d.Description StatusDesc,d.Value StatusValue
                        FROM dbo.DSD_M_ShipmentHeader msh
		                        LEFT JOIN dbo.MD_Warehouse w ON msh.WarehouseCode=w.Code
		                        LEFT JOIN dbo.DSD_T_ShipmentAssign tsa ON msh.ShipmentNo=tsa.ShipmentNo
                                LEFT JOIN dbo.MD_Person p on tsa.Driver = p.UserCode
		                        LEFT JOIN dbo.DSD_M_Truck mt ON mt.ID=tsa.TruckId
		                        LEFT JOIN dbo.DSD_M_ShipmentItem msi ON msi.ShipmentNo=msh.ShipmentNo
		                        LEFT JOIN dbo.DSD_T_ShipmentHeader tsh ON tsh.ShipmentNo=msh.ShipmentNo
		                        LEFT JOIN dbo.MD_Dictionary d ON d.Category='ShipmentStatus' AND d.Value=isnull(tsh.Status,'RELE')
		                        LEFT JOIN (
			                        SELECT mdh.ShipmentNo,COUNT(DISTINCT mdh.AccountNumber) Customers
			                        FROM dbo.DSD_M_DeliveryHeader mdh
			                        GROUP BY mdh.ShipmentNo
		                        ) a ON a.ShipmentNo=msh.ShipmentNo
                        WHERE 1=1";
            List<DbParameter> listParams = new List<DbParameter>();
            if (!string.IsNullOrEmpty(ShipmentDate))
            {
                SqlParameter paramQ = new SqlParameter() { ParameterName = "ShipmentDate", Value = DateTime.Parse(ShipmentDate) };
                listParams.Add(paramQ);
                sql = sql + " AND msh.ShipmentDate=@ShipmentDate";
            }
            if (!string.IsNullOrEmpty(ShipmentNo))
            {
                SqlParameter paramQ = new SqlParameter() { ParameterName = "ShipmentNo", Value = ShipmentNo };
                listParams.Add(paramQ);
                sql = sql + " AND msh.ShipmentNo LIKE '%'+@ShipmentNo+'%'";
            }
            if (!string.IsNullOrEmpty(Warehouse))
            {
                SqlParameter paramQ = new SqlParameter() { ParameterName = "Warehouse", Value = Warehouse };
                listParams.Add(paramQ);
                sql = sql + " AND msh.WarehouseCode = @Warehouse";
            }
            if (!string.IsNullOrEmpty(Driver))
            {
                SqlParameter paramQ = new SqlParameter() { ParameterName = "Driver", Value = Driver };
                listParams.Add(paramQ);
                sql = sql + " AND (tsa.Driver LIKE '%'+@Driver+'%' OR p.FirstName+p.LastName LIKE '%'+@Driver+'%')";
            }
            if (!string.IsNullOrEmpty(TruckNo))
            {
                SqlParameter paramQ = new SqlParameter() { ParameterName = "TruckNo", Value = TruckNo };
                listParams.Add(paramQ);
                sql = sql + " AND mt.TruckCode LIKE '%'+@TruckNo+'%'";
            }
            if (!string.IsNullOrEmpty(Status))
            {
                SqlParameter paramQ = new SqlParameter() { ParameterName = "Status", Value = Status };
                listParams.Add(paramQ);
                sql = sql + " AND ISNULL(tsh.Status,'RELE')=@Status";
            }
            sql = sql + " GROUP BY msh.ShipmentNo,w.Name,tsa.Driver,mt.ID,mt.TruckCode,msh.ShipmentType,tsh.Status,msh.ReleaseStatus,d.Description,d.Value,a.Customers";
            IList<ViewShipmentListData> LstData = _dbSession.ExecuteSqlNonQuery<ViewShipmentListData>(sql, listParams.ToArray());
            var data = from d in LstData
                       select new ViewShipmentListData
                       {
                           ShipmentNo = d.ShipmentNo,
                           WarehouseName = d.WarehouseName,
                           Driver = d.Driver,
                           TruckID = d.TruckID,
                           TruckCode = d.TruckCode,
                           ShipmentType = d.ShipmentType,
                           ProductNum = d.ProductNum,
                           ProductQty = d.ProductQty,
                           CustomerNum = d.CustomerNum,
                           StatusDesc = d.StatusDesc,
                           StatusValue=d.StatusValue
                       };

            param.total = data.Count();

            if (param.pageIndex > 0)
            {
                data = data.OrderBy(p => p.ShipmentNo)
                    .Skip((param.pageIndex - 1) * param.pageSize)
                    .Take(param.pageSize);
            }
            return data.AsQueryable().ToList();
        }

        public List<ViewShipmentItemData> GetShipmentItemData(string ShipmentNo)
        {
            var data = _dbSession.DSD_M_ShipmentItemRepository.Entities.Where(p => p.ShipmentNo == ShipmentNo);
            var dataPro = _dbSession.MD_ProductRepository.Entities;
            var LstData = from d in data
                          join p in dataPro on d.ProductCode equals p.ProductCode
                          select new ViewShipmentItemData
                          {
                              ProductName=p.Name,
                              ProductCode=d.ProductCode,
                              ProductUnit=d.ProductUnit,
                              PlanQty=d.PlanQty
                          };
            return LstData.ToList();
        }

        public DSD_T_ShipmentAssign CheckAssign(string shipmentNo)
        {
            DSD_T_ShipmentAssign data = _dbSession.DSD_T_ShipmentAssignRepository.Entities.Where(p => p.ShipmentNO == shipmentNo).FirstOrDefault();
            return data;
        }

        public void ChangeDriverAndTruck(DSD_T_ShipmentAssign item,string mode)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (mode == "Add")
                {
                    _dbSession.DSD_T_ShipmentAssignRepository.AddEntities(item);
                }
                else
                {
                    _dbSession.DSD_T_ShipmentAssignRepository.UpdateEntities(item);
                }
                _dbSession.DSD_T_ShipmentAssignRepository.Commit();

                scope.Complete();
            }
        }

        /// <summary>
        /// 获取Driver当前位置信息及已拜访门店数量
        /// </summary>
        /// <returns></returns>
        public List<ViewDriverPosition> GetDriverPosition(string ShipmentDate,string userCode)
        {
            string DriverPosition = @"SELECT *
                                        FROM (
                                        SELECT ROW_NUMBER() OVER(PARTITION BY td.TruckId,v.UserCode ORDER BY v.StartTime DESC) num
		                                        ,td.TruckId,v.UserCode DriverId,p2.FirstName +' '+p2.LastName DriverName,a.AccountNumber CustomerId,a.Name CustomerName
		                                        ,a.ShippingAddress Address,v.Longitude,v.Latitude
                                        FROM dbo.MD_Person p1
	                                        INNER JOIN dbo.MD_Person p2 ON p1.OrgId=p2.OrgId
	                                        INNER JOIN dbo.DSD_T_Visit v ON p2.UserCode=v.UserCode
	                                        INNER JOIN dbo.MD_Account a ON a.AccountNumber=v.AccountNumber
	                                        INNER JOIN dbo.DSD_M_TruckDriver td ON td.Driver=v.UserCode
                                        WHERE CONVERT(VARCHAR(10),StartTime,120)=@ShipmentDate AND p1.UserCode=@UserCode
                                        ) a WHERE a.num=1";
            string sql = string.Format(DriverPosition, ShipmentDate, userCode);
            List<DbParameter> listParams = new List<DbParameter>();
            SqlParameter paramQ;
            paramQ = new SqlParameter()
            {
                ParameterName = "ShipmentDate",
                Value = DateTime.Parse(ShipmentDate)
            };
            listParams.Add(paramQ);
            paramQ = new SqlParameter()
            {
                ParameterName = "UserCode",
                Value = userCode
            };
            listParams.Add(paramQ);
            IList<ViewDriverPosition> LstInfo = _dbSession.ExecuteSqlNonQuery<ViewDriverPosition>(sql, listParams.ToArray());
            var data = from x in LstInfo
                       select new ViewDriverPosition
                       {
                           DriverId = x.DriverId,
                           DriverName = x.DriverName,
                           CustomerId = x.CustomerId,
                           CustomerName = x.CustomerName,
                           Address = x.Address,
                           Latitude = x.Latitude,
                           Longitude = x.Longitude
                       };

            return data.ToList();
        }

        public List<ViewShipmentInfo> GetDriverRouteMapData(string ClanderDate)
        {
            

            List<DbParameter> listParams = new List<DbParameter>();
            SqlParameter paramQ;
            paramQ = new SqlParameter()
            {
                ParameterName = "ShipmentDate",
                Value = DateTime.Parse(ClanderDate)
            };
            listParams.Add(paramQ);
            IList<ViewShipmentInfo> LstInfo = _dbSession.ExecuteSqlNonQuery<ViewShipmentInfo>(sqlShipmentInfo, listParams.ToArray());
            var data = from x in LstInfo
                       select new ViewShipmentInfo
                       {
                           TruckId = x.TruckId,
                           TruckCode = x.TruckCode,
                           ShipmentCnt = x.ShipmentCnt,
                           StockOnTruck = x.StockOnTruck,
                           CustomerAndVisited = x.CustomerAndVisited,
                           FinanceList = GetShipmentFinanceList(DateTime.Parse(ClanderDate), x.TruckId)
                       };
            return data.ToList();
        }

        public List<ShipmentFinanceList> GetShipmentFinanceList(DateTime ShipmentDate, int TruckId)
        {
            List<DbParameter>  listParams = new List<DbParameter>();
            SqlParameter paramQ;
            paramQ = new SqlParameter()
            {
                ParameterName = "ShipmentDate",
                Value = ShipmentDate
            };
            listParams.Add(paramQ);
            paramQ = new SqlParameter()
            {
                ParameterName = "TruckId",
                Value = TruckId
            };
            listParams.Add(paramQ);
            IList<ShipmentFinanceList> lstFList = _dbSession.ExecuteSqlNonQuery<ShipmentFinanceList>(sqlFinance, listParams.ToArray());
            var data = from a in lstFList
                       select new ShipmentFinanceList
                       {
                           PaymentTypeValue = a.PaymentTypeValue,
                           PaymentTypeName = a.PaymentTypeName,
                           PaymentAmount = a.PaymentAmount
                       };
            return data.ToList();
        }

        /// <summary>
        /// 取得指定Driver拜访线路及门店信息
        /// </summary>
        /// <returns></returns>
        public List<ViewDriverRouteCustomerData> GetDriverRouteCustomerData(DateTime ShipmentDate, string TruckId)
        {
            List<DbParameter> listParams = new List<DbParameter>();
            SqlParameter paramQ;
            paramQ = new SqlParameter()
            {
                ParameterName = "ShipmentDate",
                Value = ShipmentDate
            };
            listParams.Add(paramQ);
            paramQ = new SqlParameter()
            {
                ParameterName = "TruckId",
                Value = TruckId
            };
            listParams.Add(paramQ);
            IList<ViewDriverRouteCustomerData> LstInfo = _dbSession.ExecuteSqlNonQuery<ViewDriverRouteCustomerData>(DriverRouteCustomerData, listParams.ToArray());

            var data = from x in LstInfo
                       select new ViewDriverRouteCustomerData
                       {
                           CustomerId = x.CustomerId,
                           CustomerName = x.CustomerName,
                           DriverId = x.DriverId,
                           Latitude = x.Latitude,
                           Longitude = x.Longitude,
                           VisitStatus = x.VisitStatus
                       };

            return data.ToList();
        }

        /// <summary>
        /// 获取指定门店的拜访信息
        /// </summary>
        /// <param name="DeliveryDate"></param>
        /// <param name="AccountNumber"></param>
        /// <returns></returns>
        public List<ViewAccountOrderData> GetAccountOrderData(string DeliveryDate, string AccountNumber)
        {
            //string sql = @"EXEC search_GetAccountOrderData @AccountNumber,@DeliveryDate";
            string sql = @"SELECT a.DeliveryType,a.QtyEACS,b.PlanAmount,c.ActualAmount
                            FROM (
                                SELECT tdh.DeliveryType
	                                ,CAST(SUM(CASE tdi.ProductUnit WHEN 'CS' THEN tdi.ActualQty ELSE 0 END) AS VARCHAR)+'/'
	                                +CAST(SUM(CASE tdi.ProductUnit WHEN 'EA' THEN tdi.ActualQty ELSE 0 END) AS VARCHAR) QtyEACS
                                FROM dbo.DSD_T_DeliveryHeader tdh
	                                LEFT JOIN dbo.DSD_T_DeliveryItem tdi ON tdh.DeliveryNo=tdi.DeliveryNo
                                WHERE AccountNumber=@AccountNumber AND DeliveryDate=@DeliveryDate
                                GROUP BY tdh.DeliveryType
                            ) a
                            LEFT JOIN(
                                SELECT tdh.DeliveryType,SUM(ISNULL(tdh.NetPrice,0))
                                *CASE WHEN tdh.DeliveryType='Empty Return' OR tdh.DeliveryType='Trade Return' THEN -1 ELSE 1 END AS PlanAmount
                                FROM  dbo.DSD_T_DeliveryHeader tdh
                                WHERE AccountNumber=@AccountNumber AND DeliveryDate=@DeliveryDate
                                GROUP BY tdh.DeliveryType
                            ) b ON a.DeliveryType = b.DeliveryType
                            LEFT JOIN(	
                                SELECT  tdh.DeliveryType ,SUM(ISNULL(tdb.PaymentAmount,0)+ISNULL(tdb.PaymentDeposit,0))
                                *CASE WHEN tdh.DeliveryType='Empty Return' OR tdh.DeliveryType='Trade Return' THEN -1 ELSE 1 END AS ActualAmount
                                FROM dbo.DSD_T_DeliveryHeader tdh
	                                LEFT JOIN DSD_T_DeliveryBilling tdb ON tdb.DeliveryNo=tdh.DeliveryNo
                                WHERE AccountNumber=@AccountNumber AND DeliveryDate=@DeliveryDate
                                GROUP BY tdh.DeliveryType
                            ) c ON b.DeliveryType = c.DeliveryType";
            List<DbParameter> listParams = new List<DbParameter>();
            SqlParameter paramQ;
            paramQ = new SqlParameter()
            {
                ParameterName = "AccountNumber",
                Value = AccountNumber
            };
            listParams.Add(paramQ);
            paramQ = new SqlParameter()
            {
                ParameterName = "DeliveryDate",
                Value = DeliveryDate
            };
            listParams.Add(paramQ);
            IList<ViewAccountOrderData> lstFList = _dbSession.ExecuteSqlNonQuery<ViewAccountOrderData>(sql, listParams.ToArray());
            var data = from a in lstFList
                       select new ViewAccountOrderData
                       {
                           DeliveryType = a.DeliveryType,
                           QtyEACS = a.QtyEACS,
                           PlanAmount = a.PlanAmount,
                           ActualAmount = a.ActualAmount,
                           FinanceList = GetAccountFinanceList(AccountNumber, DeliveryDate, a.DeliveryType)
                       };
            return data.ToList();
        }

        /// <summary>
        /// 获取Delivery的付款明细
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <param name="DeliveryDate"></param>
        /// <param name="DeliveryType"></param>
        /// <returns></returns>
        public List<AccountFinanceList> GetAccountFinanceList(string AccountNumber,string DeliveryDate,string DeliveryType)
        {
            string sql = @"SELECT tdh.DeliveryType,dic.Value PaymentTypeValue,dic.Description PaymentTypeName
                                ,SUM(tdb.PaymentAmount+tdb.PaymentDeposit)
                                *CASE WHEN tdh.DeliveryType='Empty Return' OR tdh.DeliveryType='Trade Return' THEN -1 ELSE 1 END AS PaymentAmount
                            FROM dbo.DSD_T_DeliveryHeader tdh
	                            INNER JOIN dbo.DSD_T_DeliveryBilling tdb ON tdb.DeliveryNo=tdh.DeliveryNo
	                            INNER JOIN dbo.MD_Dictionary dic ON dic.Category='paymentmethod' AND dic.Value=tdb.PaymentType
                            WHERE AccountNumber=@AccountNumber AND DeliveryDate=@DeliveryDate AND tdh.DeliveryType=@DeliveryType
                            GROUP BY tdh.DeliveryType,dic.Value,dic.Description";
            List<DbParameter> listParams = new List<DbParameter>();
            SqlParameter paramQ;
            paramQ = new SqlParameter()
            {
                ParameterName = "AccountNumber",
                Value = AccountNumber
            };
            listParams.Add(paramQ);
            paramQ = new SqlParameter()
            {
                ParameterName = "DeliveryDate",
                Value = DeliveryDate
            };
            listParams.Add(paramQ);
            paramQ = new SqlParameter()
            {
                ParameterName = "DeliveryType",
                Value = DeliveryType
            };
            listParams.Add(paramQ);
            IList<AccountFinanceList> lstFList = _dbSession.ExecuteSqlNonQuery<AccountFinanceList>(sql, listParams.ToArray());
            var data = from a in lstFList
                       select new AccountFinanceList
                       {
                           PaymentTypeValue = a.PaymentTypeValue,
                           PaymentTypeName = a.PaymentTypeName,
                           PaymentAmount = a.PaymentAmount
                       };
            return data.ToList();
        }

        /// <summary>
        /// 获取AR数据合计
        /// </summary>
        /// <param name="DeliveryDate"></param>
        /// <param name="AccountNumber"></param>
        /// <returns></returns>
        public ViewAccountOrderData GetAccountARData(string DeliveryDate, string AccountNumber)
        {
            string sql = @"SELECT ISNULL(SUM(ebMobile__Amount__c),0) ActualAmount
                            FROM dbo.DSD_T_Payment  tp
		                        INNER JOIN DSD_T_ARCollection ta ON tp.ebMobile__ARCollection__c=ta.ID
                            WHERE tp.LastUpdateTime>=@DeliveryDate AND tp.LastUpdateTime<(CAST(@DeliveryDate AS DATETIME)+1 )
                            AND ebMobile__Account__c=@AccountNumber";
            List<DbParameter> listParams = new List<DbParameter>();
            SqlParameter paramQ;
            paramQ = new SqlParameter()
            {
                ParameterName = "DeliveryDate",
                Value = DeliveryDate
            };
            listParams.Add(paramQ);
            paramQ = new SqlParameter()
            {
                ParameterName = "AccountNumber",
                Value = AccountNumber
            };
            listParams.Add(paramQ);
            IList<ViewAccountOrderData> lstFList = _dbSession.ExecuteSqlNonQuery<ViewAccountOrderData>(sql, listParams.ToArray());
            var data = from a in lstFList
                       select new ViewAccountOrderData
                       {
                           DeliveryType = "ARCollection",
                           ActualAmount = a.ActualAmount,
                           FinanceList = GetAccountARFinanceData(AccountNumber, DeliveryDate)
                       };
            return data.FirstOrDefault();
        }

        /// <summary>
        /// 获取AR明细
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <param name="DeliveryDate"></param>
        /// <returns></returns>
        public List<AccountFinanceList> GetAccountARFinanceData(string AccountNumber, string DeliveryDate)
        {
            string sql = @"SELECT ebMobile__PaymentType__c PaymentTypeValue,dic.Description PaymentTypeName,SUM(tp.ebMobile__Amount__c) PaymentAmount
                            FROM dbo.DSD_T_Payment  tp
	                            INNER JOIN DSD_T_ARCollection ta ON tp.ebMobile__ARCollection__c=ta.ID
	                            INNER JOIN dbo.MD_Dictionary dic ON dic.Category='paymentmethod' AND dic.Value=tp.ebMobile__PaymentType__c
                            WHERE tp.LastUpdateTime>=@DeliveryDate AND tp.LastUpdateTime<(CAST(@DeliveryDate AS DATETIME)+1 )
                                AND ebMobile__Account__c=@AccountNumber
                            GROUP BY ebMobile__PaymentType__c,dic.Description";
            List<DbParameter> listParams = new List<DbParameter>();
            SqlParameter paramQ;
            paramQ = new SqlParameter()
            {
                ParameterName = "DeliveryDate",
                Value = DeliveryDate
            };
            listParams.Add(paramQ);
            paramQ = new SqlParameter()
            {
                ParameterName = "AccountNumber",
                Value = AccountNumber
            };
            listParams.Add(paramQ);
            IList<AccountFinanceList> lstFList = _dbSession.ExecuteSqlNonQuery<AccountFinanceList>(sql, listParams.ToArray());
            var data = from a in lstFList
                       select new AccountFinanceList
                       {
                           PaymentTypeValue = a.PaymentTypeValue,
                           PaymentTypeName = a.PaymentTypeName,
                           PaymentAmount = a.PaymentAmount
                       };
            return data.ToList();
        }
    }
}
