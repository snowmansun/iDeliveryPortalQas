 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mde.IBLL;
using Mde.Model.DataModel;

namespace Mde.BLL
{
	
	public partial class DSD_M_DeliveryHeaderService:BaseService<DSD_M_DeliveryHeader>,IBLL.IDSD_M_DeliveryHeaderService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_M_DeliveryHeaderRepository;
        }  
    }
	
	public partial class DSD_M_DeliveryItemService:BaseService<DSD_M_DeliveryItem>,IBLL.IDSD_M_DeliveryItemService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_M_DeliveryItemRepository;
        }  
    }
	
	public partial class DSD_M_DeliveryOrderService:BaseService<DSD_M_DeliveryOrder>,IBLL.IDSD_M_DeliveryOrderService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_M_DeliveryOrderRepository;
        }  
    }
	
	public partial class DSD_M_OrgSystemConfigService:BaseService<DSD_M_OrgSystemConfig>,IBLL.IDSD_M_OrgSystemConfigService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_M_OrgSystemConfigRepository;
        }  
    }
	
	public partial class DSD_M_ShipmentFinanceService:BaseService<DSD_M_ShipmentFinance>,IBLL.IDSD_M_ShipmentFinanceService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_M_ShipmentFinanceRepository;
        }  
    }
	
	public partial class DSD_M_ShipmentHeaderService:BaseService<DSD_M_ShipmentHeader>,IBLL.IDSD_M_ShipmentHeaderService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_M_ShipmentHeaderRepository;
        }  
    }
	
	public partial class DSD_M_ShipmentHelperService:BaseService<DSD_M_ShipmentHelper>,IBLL.IDSD_M_ShipmentHelperService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_M_ShipmentHelperRepository;
        }  
    }
	
	public partial class DSD_M_ShipmentItemService:BaseService<DSD_M_ShipmentItem>,IBLL.IDSD_M_ShipmentItemService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_M_ShipmentItemRepository;
        }  
    }
	
	public partial class DSD_M_ShipmentVanSalesRouteService:BaseService<DSD_M_ShipmentVanSalesRoute>,IBLL.IDSD_M_ShipmentVanSalesRouteService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_M_ShipmentVanSalesRouteRepository;
        }  
    }
	
	public partial class DSD_M_SystemConfigService:BaseService<DSD_M_SystemConfig>,IBLL.IDSD_M_SystemConfigService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_M_SystemConfigRepository;
        }  
    }
	
	public partial class DSD_M_TruckService:BaseService<DSD_M_Truck>,IBLL.IDSD_M_TruckService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_M_TruckRepository;
        }  
    }
	
	public partial class DSD_M_TruckCheckAssignService:BaseService<DSD_M_TruckCheckAssign>,IBLL.IDSD_M_TruckCheckAssignService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_M_TruckCheckAssignRepository;
        }  
    }
	
	public partial class DSD_M_TruckCheckListService:BaseService<DSD_M_TruckCheckList>,IBLL.IDSD_M_TruckCheckListService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_M_TruckCheckListRepository;
        }  
    }
	
	public partial class DSD_M_TruckDriverService:BaseService<DSD_M_TruckDriver>,IBLL.IDSD_M_TruckDriverService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_M_TruckDriverRepository;
        }  
    }
	
	public partial class DSD_T_DayTimeTrackingService:BaseService<DSD_T_DayTimeTracking>,IBLL.IDSD_T_DayTimeTrackingService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_T_DayTimeTrackingRepository;
        }  
    }
	
	public partial class DSD_T_DeliveryBillingService:BaseService<DSD_T_DeliveryBilling>,IBLL.IDSD_T_DeliveryBillingService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_T_DeliveryBillingRepository;
        }  
    }
	
	public partial class DSD_T_DeliveryHeaderService:BaseService<DSD_T_DeliveryHeader>,IBLL.IDSD_T_DeliveryHeaderService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_T_DeliveryHeaderRepository;
        }  
    }
	
	public partial class DSD_T_DeliveryItemService:BaseService<DSD_T_DeliveryItem>,IBLL.IDSD_T_DeliveryItemService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_T_DeliveryItemRepository;
        }  
    }
	
	public partial class DSD_T_InvoiceService:BaseService<DSD_T_Invoice>,IBLL.IDSD_T_InvoiceService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_T_InvoiceRepository;
        }  
    }
	
	public partial class DSD_T_InvoiceItemService:BaseService<DSD_T_InvoiceItem>,IBLL.IDSD_T_InvoiceItemService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_T_InvoiceItemRepository;
        }  
    }
	
	public partial class DSD_T_OdometerTrackingService:BaseService<DSD_T_OdometerTracking>,IBLL.IDSD_T_OdometerTrackingService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_T_OdometerTrackingRepository;
        }  
    }
	
	public partial class DSD_T_ShipmentAssignService:BaseService<DSD_T_ShipmentAssign>,IBLL.IDSD_T_ShipmentAssignService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_T_ShipmentAssignRepository;
        }  
    }
	
	public partial class DSD_T_ShipmentFinanceService:BaseService<DSD_T_ShipmentFinance>,IBLL.IDSD_T_ShipmentFinanceService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_T_ShipmentFinanceRepository;
        }  
    }
	
	public partial class DSD_T_ShipmentHeaderService:BaseService<DSD_T_ShipmentHeader>,IBLL.IDSD_T_ShipmentHeaderService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_T_ShipmentHeaderRepository;
        }  
    }
	
	public partial class DSD_T_ShipmentHelperService:BaseService<DSD_T_ShipmentHelper>,IBLL.IDSD_T_ShipmentHelperService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_T_ShipmentHelperRepository;
        }  
    }
	
	public partial class DSD_T_ShipmentItemService:BaseService<DSD_T_ShipmentItem>,IBLL.IDSD_T_ShipmentItemService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_T_ShipmentItemRepository;
        }  
    }
	
	public partial class DSD_T_TruckCheckResultService:BaseService<DSD_T_TruckCheckResult>,IBLL.IDSD_T_TruckCheckResultService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_T_TruckCheckResultRepository;
        }  
    }
	
	public partial class DSD_T_TruckStockService:BaseService<DSD_T_TruckStock>,IBLL.IDSD_T_TruckStockService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_T_TruckStockRepository;
        }  
    }
	
	public partial class DSD_T_TruckStockTrackingService:BaseService<DSD_T_TruckStockTracking>,IBLL.IDSD_T_TruckStockTrackingService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_T_TruckStockTrackingRepository;
        }  
    }
	
	public partial class DSD_T_VisitService:BaseService<DSD_T_Visit>,IBLL.IDSD_T_VisitService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.DSD_T_VisitRepository;
        }  
    }
	
	public partial class MD_AccountService:BaseService<MD_Account>,IBLL.IMD_AccountService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.MD_AccountRepository;
        }  
    }
	
	public partial class MD_AccountARService:BaseService<MD_AccountAR>,IBLL.IMD_AccountARService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.MD_AccountARRepository;
        }  
    }
	
	public partial class MD_CompanyService:BaseService<MD_Company>,IBLL.IMD_CompanyService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.MD_CompanyRepository;
        }  
    }
	
	public partial class MD_ContactService:BaseService<MD_Contact>,IBLL.IMD_ContactService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.MD_ContactRepository;
        }  
    }
	
	public partial class MD_DictionaryService:BaseService<MD_Dictionary>,IBLL.IMD_DictionaryService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.MD_DictionaryRepository;
        }  
    }
	
	public partial class MD_OrganizationService:BaseService<MD_Organization>,IBLL.IMD_OrganizationService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.MD_OrganizationRepository;
        }  
    }
	
	public partial class MD_PersonService:BaseService<MD_Person>,IBLL.IMD_PersonService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.MD_PersonRepository;
        }  
    }
	
	public partial class MD_ProdListConfigService:BaseService<MD_ProdListConfig>,IBLL.IMD_ProdListConfigService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.MD_ProdListConfigRepository;
        }  
    }
	
	public partial class MD_ProductService:BaseService<MD_Product>,IBLL.IMD_ProductService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.MD_ProductRepository;
        }  
    }
	
	public partial class MD_ProductUOMService:BaseService<MD_ProductUOM>,IBLL.IMD_ProductUOMService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.MD_ProductUOMRepository;
        }  
    }
	
	public partial class MD_UserService:BaseService<MD_User>,IBLL.IMD_UserService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.MD_UserRepository;
        }  
    }
	
	public partial class MD_WarehouseService:BaseService<MD_Warehouse>,IBLL.IMD_WarehouseService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.MD_WarehouseRepository;
        }  
    }
	
	public partial class SyncLogService:BaseService<SyncLog>,IBLL.ISyncLogService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.SyncLogRepository;
        }  
    }
	
	public partial class Sys_ModuleService:BaseService<Sys_Module>,IBLL.ISys_ModuleService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.Sys_ModuleRepository;
        }  
    }
	
	public partial class Sys_ModuleConfigurationService:BaseService<Sys_ModuleConfiguration>,IBLL.ISys_ModuleConfigurationService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.Sys_ModuleConfigurationRepository;
        }  
    }
	
	public partial class Sys_RoleService:BaseService<Sys_Role>,IBLL.ISys_RoleService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.Sys_RoleRepository;
        }  
    }
	
	public partial class Sys_UserRoleLinkService:BaseService<Sys_UserRoleLink>,IBLL.ISys_UserRoleLinkService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.Sys_UserRoleLinkRepository;
        }  
    }
	
	public partial class sysdiagramService:BaseService<sysdiagram>,IBLL.IsysdiagramService	
    { 
		public override void SetCurrentRepository()
        {
            CurrentRepository = _dbSession.sysdiagramRepository;
        }  
    }
	
}