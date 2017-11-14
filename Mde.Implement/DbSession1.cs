 
using Mde.Interface;

namespace Mde.Implement
{
    public partial class DbSession:IDbSession  
    {   
	



	public IDSD_M_DeliveryHeaderRepository DSD_M_DeliveryHeaderRepository { get { return new DSD_M_DeliveryHeaderRepository(); } }

	



	public IDSD_M_DeliveryItemRepository DSD_M_DeliveryItemRepository { get { return new DSD_M_DeliveryItemRepository(); } }

	



	public IDSD_M_DeliveryOrderRepository DSD_M_DeliveryOrderRepository { get { return new DSD_M_DeliveryOrderRepository(); } }

	



	public IDSD_M_OrgSystemConfigRepository DSD_M_OrgSystemConfigRepository { get { return new DSD_M_OrgSystemConfigRepository(); } }

	



	public IDSD_M_ShipmentFinanceRepository DSD_M_ShipmentFinanceRepository { get { return new DSD_M_ShipmentFinanceRepository(); } }

	



	public IDSD_M_ShipmentHeaderRepository DSD_M_ShipmentHeaderRepository { get { return new DSD_M_ShipmentHeaderRepository(); } }

	



	public IDSD_M_ShipmentHelperRepository DSD_M_ShipmentHelperRepository { get { return new DSD_M_ShipmentHelperRepository(); } }

	



	public IDSD_M_ShipmentItemRepository DSD_M_ShipmentItemRepository { get { return new DSD_M_ShipmentItemRepository(); } }

	



	public IDSD_M_ShipmentVanSalesRouteRepository DSD_M_ShipmentVanSalesRouteRepository { get { return new DSD_M_ShipmentVanSalesRouteRepository(); } }

	



	public IDSD_M_SystemConfigRepository DSD_M_SystemConfigRepository { get { return new DSD_M_SystemConfigRepository(); } }

	



	public IDSD_M_TruckRepository DSD_M_TruckRepository { get { return new DSD_M_TruckRepository(); } }

	



	public IDSD_M_TruckCheckAssignRepository DSD_M_TruckCheckAssignRepository { get { return new DSD_M_TruckCheckAssignRepository(); } }

	



	public IDSD_M_TruckCheckListRepository DSD_M_TruckCheckListRepository { get { return new DSD_M_TruckCheckListRepository(); } }

	



	public IDSD_M_TruckDriverRepository DSD_M_TruckDriverRepository { get { return new DSD_M_TruckDriverRepository(); } }

	



	public IDSD_T_DayTimeTrackingRepository DSD_T_DayTimeTrackingRepository { get { return new DSD_T_DayTimeTrackingRepository(); } }

	



	public IDSD_T_DeliveryBillingRepository DSD_T_DeliveryBillingRepository { get { return new DSD_T_DeliveryBillingRepository(); } }

	



	public IDSD_T_DeliveryHeaderRepository DSD_T_DeliveryHeaderRepository { get { return new DSD_T_DeliveryHeaderRepository(); } }

	



	public IDSD_T_DeliveryItemRepository DSD_T_DeliveryItemRepository { get { return new DSD_T_DeliveryItemRepository(); } }

	



	public IDSD_T_InvoiceRepository DSD_T_InvoiceRepository { get { return new DSD_T_InvoiceRepository(); } }

	



	public IDSD_T_InvoiceItemRepository DSD_T_InvoiceItemRepository { get { return new DSD_T_InvoiceItemRepository(); } }

	



	public IDSD_T_OdometerTrackingRepository DSD_T_OdometerTrackingRepository { get { return new DSD_T_OdometerTrackingRepository(); } }

	



	public IDSD_T_ShipmentAssignRepository DSD_T_ShipmentAssignRepository { get { return new DSD_T_ShipmentAssignRepository(); } }

	



	public IDSD_T_ShipmentFinanceRepository DSD_T_ShipmentFinanceRepository { get { return new DSD_T_ShipmentFinanceRepository(); } }

	



	public IDSD_T_ShipmentHeaderRepository DSD_T_ShipmentHeaderRepository { get { return new DSD_T_ShipmentHeaderRepository(); } }

	



	public IDSD_T_ShipmentHelperRepository DSD_T_ShipmentHelperRepository { get { return new DSD_T_ShipmentHelperRepository(); } }

	



	public IDSD_T_ShipmentItemRepository DSD_T_ShipmentItemRepository { get { return new DSD_T_ShipmentItemRepository(); } }

	



	public IDSD_T_TruckCheckResultRepository DSD_T_TruckCheckResultRepository { get { return new DSD_T_TruckCheckResultRepository(); } }

	



	public IDSD_T_TruckStockRepository DSD_T_TruckStockRepository { get { return new DSD_T_TruckStockRepository(); } }

	



	public IDSD_T_TruckStockTrackingRepository DSD_T_TruckStockTrackingRepository { get { return new DSD_T_TruckStockTrackingRepository(); } }

	



	public IDSD_T_VisitRepository DSD_T_VisitRepository { get { return new DSD_T_VisitRepository(); } }

	



	public IMD_AccountRepository MD_AccountRepository { get { return new MD_AccountRepository(); } }

	



	public IMD_AccountARRepository MD_AccountARRepository { get { return new MD_AccountARRepository(); } }

	



	public IMD_CompanyRepository MD_CompanyRepository { get { return new MD_CompanyRepository(); } }

	



	public IMD_ContactRepository MD_ContactRepository { get { return new MD_ContactRepository(); } }

	



	public IMD_DictionaryRepository MD_DictionaryRepository { get { return new MD_DictionaryRepository(); } }

	



	public IMD_OrganizationRepository MD_OrganizationRepository { get { return new MD_OrganizationRepository(); } }

	



	public IMD_PersonRepository MD_PersonRepository { get { return new MD_PersonRepository(); } }

	



	public IMD_ProdListConfigRepository MD_ProdListConfigRepository { get { return new MD_ProdListConfigRepository(); } }

	



	public IMD_ProductRepository MD_ProductRepository { get { return new MD_ProductRepository(); } }

	



	public IMD_ProductUOMRepository MD_ProductUOMRepository { get { return new MD_ProductUOMRepository(); } }

	



	public IMD_UserRepository MD_UserRepository { get { return new MD_UserRepository(); } }

	



	public IMD_WarehouseRepository MD_WarehouseRepository { get { return new MD_WarehouseRepository(); } }

	



	public ISyncLogRepository SyncLogRepository { get { return new SyncLogRepository(); } }

	



	public ISys_ModuleRepository Sys_ModuleRepository { get { return new Sys_ModuleRepository(); } }

	



	public ISys_ModuleConfigurationRepository Sys_ModuleConfigurationRepository { get { return new Sys_ModuleConfigurationRepository(); } }

	



	public ISys_RoleRepository Sys_RoleRepository { get { return new Sys_RoleRepository(); } }

	



	public ISys_UserRoleLinkRepository Sys_UserRoleLinkRepository { get { return new Sys_UserRoleLinkRepository(); } }

	



	public IsysdiagramRepository sysdiagramRepository { get { return new sysdiagramRepository(); } }

	}
}