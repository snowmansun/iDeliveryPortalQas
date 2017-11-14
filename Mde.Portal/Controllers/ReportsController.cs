using Mde.BLL;
using Mde.Common;
using Mde.IBLL;
using Mde.ModelView.ModelView;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Mde.Portal.Controllers
{
    public class ReportsController : BaseController
    {
        private IDSD_T_ShipmentHeaderService _dsdShipmentHServer = new DSD_T_ShipmentHeaderService();
        private IDSD_T_DeliveryHeaderService _dsdDeliveryHServer = new DSD_T_DeliveryHeaderService();
        public IDSD_T_OdometerTrackingService _dsdTOdometerTServer = new DSD_T_OdometerTrackingService();

        #region ShipmentTracking

        public ActionResult ShipmentTracking()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoadTrackingData(FormCollection value)
        {
            ParameterQuery param = SetPageSize();
            var ShipmentDate = value["ShipmentDate"];
            var Driver = value["Driver"];
            var ShipmentNo = value["ShipmentNo"];
            var Status = value["Status"];
            var ShipmentType = value["ShipmentType"];

            var LstData = _dsdShipmentHServer.LoadTrackingData(ShipmentNo, Driver, ShipmentDate, Status, ShipmentType, param);

            var postJson = new { total = param.total, rows = LstData };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LoadTrackingItemData(FormCollection value)
        {
            var HeaderId = value["HeaderId"];
            var Category = value["Category"];
            List<ViewShipmentTrackingItemData> LstData = _dsdShipmentHServer.LoadTrackingItemData(HeaderId, Category);

            var postJson = new { total = LstData.Count(), rows = LstData };
            return Json(postJson, JsonRequestBehavior.AllowGet); 
        }

        
        #endregion

        #region DeliveryTracking

        public ActionResult DeliveryTracking()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoadDelTrackingData(FormCollection value)
        {
            ParameterQuery param = SetPageSize();
            string ShipmentNo = value["ShipmentNo"];
            string DeliveryNo = value["DeliveryNo"];
            string Account = value["Account"];
            string DeliveryDate = value["DeliveryDate"];
            string Status = value["Status"];
            string DeliveryType = value["DeliveryType"];

            List<ViewDeliveryHeaderData> LstData = _dsdDeliveryHServer.LoadDelTrackingData(ShipmentNo, DeliveryNo, DeliveryDate, Account, Status, DeliveryType,param);

            var postJson = new { total = param.total, rows = LstData };
            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult LoadDelTrackingItemData(FormCollection value)
        {
            var DeliveryNo = value["DeliveryNo"];
            List<ViewDicTrackingData> LstData = _dsdDeliveryHServer.LoadDelTrackingItemData(DeliveryNo);

            var postJson = new { total = LstData.Count(), rows = LstData };
            return Json(postJson, JsonRequestBehavior.AllowGet); 
        }

        #endregion

        #region OdometerTracking

        public ActionResult OdometerTracking()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoadOdometerData(FormCollection value)
        {
            ParameterQuery param = SetPageSize();
            string Truck = value["Truck"];
            string DateFrom = value["DateFrom"];
            string DateTo = value["DateTo"];
            string Status = value["Status"];

            List<ViewOdometerTrackingData> LstData = _dsdTOdometerTServer.LoadOdometerData(Truck, DateFrom, DateTo, Status,param);

            var postJson = new { total = param.total, rows = LstData };
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
