using Mde.BLL;
using Mde.Common;
using Mde.IBLL;
using Mde.Model.DataModel;
using Mde.Model.ModelView;
using Mde.ModelView.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mde.Portal.Controllers
{
    public class ShipmentController : BaseController
    {
        private OperationResult result = new OperationResult();
        private IMD_CompanyService _mdCompanyService = new MD_CompanyService();
        private IMD_WarehouseService _mdWarehouseService = new MD_WarehouseService();
        private IDSD_M_ShipmentHeaderService _dsdMShipmentHeaderService = new DSD_M_ShipmentHeaderService();
        private IMD_UserService _mdUserService = new MD_UserService();
        private IDSD_M_TruckService _dsdMTruckService = new DSD_M_TruckService();
        private IDSD_M_DeliveryHeaderService _dsdMDeliveryService = new DSD_M_DeliveryHeaderService();
        private IDSD_T_TruckStockService _dsdTruckStockService = new DSD_T_TruckStockService();
        private IDSD_M_SystemConfigService _sysConfigService = new DSD_M_SystemConfigService();

        #region Shipment
        public ActionResult ShipmentList()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetCompanyComboboxData(FormCollection value)
        {
            bool NeedAll;
            bool.TryParse(Request.QueryString["NeedAll"], out NeedAll);
            List<ViewComboboxStringData> LstData = _mdCompanyService.GetCompanyComboboxData();
            if (NeedAll)
            {
                ViewComboboxStringData EmptyData = new ViewComboboxStringData();
                EmptyData.name = "All";
                EmptyData.value = "";
                LstData.Insert(0, EmptyData);
            }
            return Json(LstData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetWarehouseComboboxData(FormCollection value)
        {
            bool NeedAll;
            bool.TryParse(Request.QueryString["NeedAll"], out NeedAll);
            bool NeedBlank;
            bool.TryParse(Request.QueryString["NeedBlank"], out NeedBlank);
            string CompanyCode = "";
            if (Request.QueryString["CompanyCode"] != null && Request.QueryString["CompanyCode"].ToString() != "")
                CompanyCode = Request.QueryString["CompanyCode"].ToString();
            List<ViewComboboxStringData> LstData = _mdWarehouseService.GetWareHouseComboBoxData(CompanyCode);
            if (NeedAll)
            {
                ViewComboboxStringData EmptyData = new ViewComboboxStringData();
                EmptyData.name = "All";
                EmptyData.value = "";
                LstData.Insert(0, EmptyData);
            }
            if (NeedBlank)
            {
                ViewComboboxStringData EmptyData = new ViewComboboxStringData();
                EmptyData.name = "";
                EmptyData.value = "";
                LstData.Insert(0, EmptyData);
            }
            return Json(LstData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取shipment list data
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetShipmentList(FormCollection value)
        {
            ParameterQuery param = SetPageSize();
            string ShipmentDate = value["ShipmentDate"];
            string ShipmentNo = value["ShipmentNo"];
            string Warehouse = value["Warehouse"];
            string Driver = value["Driver"];
            string TruckNo = value["TruckNo"];
            string Status = value["Status"];

            List<ViewShipmentListData> LstData = _dsdMShipmentHeaderService.GetShipmentListData(ShipmentDate, ShipmentNo, Warehouse, Driver, TruckNo, Status, param);

            var postJson = new { total = param.total, rows = LstData };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据ShipmentNO获取ShipmentItem
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetShipmentItemList(FormCollection value)
        {
            string ShipmentNo = value["ShipmentNo"];

            List<ViewShipmentItemData> LstData = _dsdMShipmentHeaderService.GetShipmentItemData(ShipmentNo);
            return Json(LstData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取Truck combobox
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetTruckList(FormCollection value)
        {
            List<ViewComboboxIntData> LstData = _dsdMTruckService.GetTruckList();
            if (Request.QueryString["NeedSel"] != null && bool.Parse(Request.QueryString["NeedSel"]))
            {
                ViewComboboxIntData selRow = new ViewComboboxIntData();
                selRow.name = "--Select Truck--";
                selRow.value = 0;
                LstData.Insert(0, selRow);
            }
            return Json(LstData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取Driver信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetPersonList(FormCollection value)
        {
            string UserType = "";
            if (Request.QueryString["UserType"] != null)
                UserType = Request.QueryString["UserType"].ToString();

            bool NeedSum;
            bool.TryParse(Request.QueryString["NeedSum"], out NeedSum);
            List<ViewComboboxStringData> LstData = _mdUserService.GetPersonList(UserType, NeedSum);
            if (Request.QueryString["NeedSel"] != null && bool.Parse(Request.QueryString["NeedSel"]))
            {
                ViewComboboxStringData EmptyData = new ViewComboboxStringData();
                EmptyData.name = "--Select Driver--";
                EmptyData.value = "";
                LstData.Insert(0, EmptyData);
            }
            return Json(LstData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// change driver and truck
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangeDriverAndTruck(FormCollection value)
        {
            string ShipmentNo = value["ShipmentNo"];
            string NewDriver = value["NewDriver"];
            string NewTruck = value["NewTruck"];

            try
            {
                DSD_T_ShipmentAssign assign = _dsdMShipmentHeaderService.CheckAssign(ShipmentNo);
                string mode = "";
                if (assign == null)
                {
                    mode = "Add";
                    assign = new DSD_T_ShipmentAssign();
                    assign.ShipmentNO = ShipmentNo;
                    assign.WorkDay = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                    assign.Driver = NewDriver;
                    assign.TruckID = int.Parse(NewTruck);
                    assign.CreateTime = DateTime.Now;
                    assign.CreateUser = CurrentUserInfo.LoginName;
                    assign.LastUpdateTime = DateTime.Now;
                    assign.LastUpdateUser = CurrentUserInfo.LoginName;
                }
                else
                {
                    mode = "Eidt";
                    assign.Driver = NewDriver;
                    assign.TruckID = int.Parse(NewTruck);
                    assign.LastUpdateTime = DateTime.Now;
                    assign.LastUpdateUser = CurrentUserInfo.LoginName;
                }
                _dsdMShipmentHeaderService.ChangeDriverAndTruck(assign, mode);

                result.ResultType = OperationResultType.succeed;
            }
            catch (Exception ex)
            {
                result.ResultType = OperationResultType.failed;
                result.Message = ex.Message;
            }
            var postJson = new { result = result.ResultType.ToString(), msg = result.Message };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Delivery
        public ActionResult DeliveryNote()
        {
            return View();
        }

        /// <summary>
        /// 获取Delivery Note Data
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDeliveryNoteData(FormCollection value)
        {
            ParameterQuery param = SetPageSize();
            string DeliveryDate = value["DeliveryDate"];
            string ShipmentNo = value["ShipmentNo"];
            string DeliveryNo = value["DeliveryNo"];
            string Driver = value["Driver"];
            string TruckNo = value["TruckNo"];
            string Status = value["Status"];
            string DeliveryType = value["DeliveryType"];

            List<ViewDeliveryNoteData> LstData = _dsdMDeliveryService.GetDeliveryNoteData(DeliveryDate, ShipmentNo, DeliveryNo, Driver, TruckNo, Status,DeliveryType, param);

            var postJson = new { total = param.total, rows = LstData };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取Delivery Item Data
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDeliveryItemList(FormCollection value)
        {
            string DeliveryNo = value["DeliveryNo"];
            List<ViewDeliveryItemData> LstData = _dsdMDeliveryService.GetDeliveryItemData(DeliveryNo);
            return Json(LstData, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region StockTracking

        public ActionResult StockTracking()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetTruckStockData(FormCollection value)
        {
            var Truck = value["Truck"];
            var ShipmentNo = value["ShipmentNo"];
            var Product = value["Product"];
            var StockDate = value["StockDate"];

            ParameterQuery param = SetPageSize();
            List<ViewTruckStockData> LstData = _dsdTruckStockService.GetTruckStockData(Truck, ShipmentNo, Product, StockDate,param);


            var postJson = new { total = param.total, rows = LstData };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetTruckStockTrackingData(FormCollection value)
        {
            int TruckId;
            int.TryParse(value["TruckId"], out TruckId);
            var ShipmentNo = value["ShipmentNo"];
            var ProductCode = value["ProductCode"];

            List<ViewStockTrackingData> LstData = _dsdTruckStockService.GetTruckStockTrackingData(TruckId, ShipmentNo, ProductCode);
            return Json(LstData, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Route Map
        public ActionResult RouteMap()
        {
            ViewData["ListShipment"] = _dsdMShipmentHeaderService.GetDriverRouteMapData(DateTime.Now.ToString("yyyy-MM-dd"));
            List<ViewConfigData> data = _sysConfigService.GetConfigData("CommonFunction", "Currency", int.Parse(CurrentUserInfo.OrgId.ToString()));
            ViewData["Currency"] = data == null || data.Count() == 0 ? "$" : data[0].Value;
            return View();
        }

        /// <summary>
        /// 取得地图默认Driver位置数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDriverPostitionData()
        {
            string ClanderDate = DateTime.Parse(Request.QueryString["ClanderDate"]).ToString("yyyy-MM-dd");
            //ClanderDate = "2014-12-18 00:00:00";
            List<ViewDriverPosition> LstData = _dsdMShipmentHeaderService.GetDriverPosition(ClanderDate, CurrentUserInfo.LoginName);
            var postJson = new { rows = LstData };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetDriverRouteMapData(FormCollection value)
        {
            string ClanderDate = DateTime.Parse(Request.QueryString["ClanderDate"]).ToString("yyyy-MM-dd");
            List<ViewShipmentInfo> LstData = _dsdMShipmentHeaderService.GetDriverRouteMapData(ClanderDate);
            ViewData["ListShipment"] = LstData;
            var postJson = new { rows = LstData };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 取得指定Truck拜访线路及门店信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetDriverRouteCustomerData()
        {
            string ClanderDate = Request.QueryString["ClanderDate"];
            string TruckId = Request.QueryString["TruckId"];
            List<ViewDriverRouteCustomerData> LstData = _dsdMShipmentHeaderService.GetDriverRouteCustomerData(DateTime.Parse(ClanderDate), TruckId);
            var postJson = new { rows = LstData };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAccountOrderData()
        {
            string ClanderDate = Request.QueryString["ClanderDate"];
            string AccountNumber = Request.QueryString["AccountNumber"];
            List<ViewAccountOrderData> LstData = _dsdMShipmentHeaderService.GetAccountOrderData(ClanderDate, AccountNumber);
            ViewAccountOrderData arData = _dsdMShipmentHeaderService.GetAccountARData(ClanderDate, AccountNumber);
            if (arData != null)
                LstData.Add(arData);
            var postJson = new { rows = LstData };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region "pageSize"
        private ParameterQuery SetPageSize()
        {
            int pageIndex, pageSize;
            int.TryParse(Request["page"], out pageIndex);
            int.TryParse(Request["rows"], out pageSize);

            ParameterQuery param = new ParameterQuery();
            param.pageIndex = pageIndex;
            param.pageSize = pageSize;

            return param;
        }
        #endregion
    }
}
