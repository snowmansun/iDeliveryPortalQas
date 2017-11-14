 


namespace Mde.Interface
{
    public partial interface IDbSession
    {
   
	  

		IDSD_M_DeliveryHeaderRepository DSD_M_DeliveryHeaderRepository { get; }
	  

		IDSD_M_DeliveryItemRepository DSD_M_DeliveryItemRepository { get; }
	  

		IDSD_M_DeliveryOrderRepository DSD_M_DeliveryOrderRepository { get; }
	  

		IDSD_M_OrgSystemConfigRepository DSD_M_OrgSystemConfigRepository { get; }
	  

		IDSD_M_ShipmentFinanceRepository DSD_M_ShipmentFinanceRepository { get; }
	  

		IDSD_M_ShipmentHeaderRepository DSD_M_ShipmentHeaderRepository { get; }
	  

		IDSD_M_ShipmentHelperRepository DSD_M_ShipmentHelperRepository { get; }
	  

		IDSD_M_ShipmentItemRepository DSD_M_ShipmentItemRepository { get; }
	  

		IDSD_M_ShipmentVanSalesRouteRepository DSD_M_ShipmentVanSalesRouteRepository { get; }
	  

		IDSD_M_SystemConfigRepository DSD_M_SystemConfigRepository { get; }
	  

		IDSD_M_TruckRepository DSD_M_TruckRepository { get; }
	  

		IDSD_M_TruckCheckAssignRepository DSD_M_TruckCheckAssignRepository { get; }
	  

		IDSD_M_TruckCheckListRepository DSD_M_TruckCheckListRepository { get; }
	  

		IDSD_M_TruckDriverRepository DSD_M_TruckDriverRepository { get; }
	  

		IDSD_T_DayTimeTrackingRepository DSD_T_DayTimeTrackingRepository { get; }
	  

		IDSD_T_DeliveryBillingRepository DSD_T_DeliveryBillingRepository { get; }
	  

		IDSD_T_DeliveryHeaderRepository DSD_T_DeliveryHeaderRepository { get; }
	  

		IDSD_T_DeliveryItemRepository DSD_T_DeliveryItemRepository { get; }
	  

		IDSD_T_InvoiceRepository DSD_T_InvoiceRepository { get; }
	  

		IDSD_T_InvoiceItemRepository DSD_T_InvoiceItemRepository { get; }
	  

		IDSD_T_OdometerTrackingRepository DSD_T_OdometerTrackingRepository { get; }
	  

		IDSD_T_ShipmentAssignRepository DSD_T_ShipmentAssignRepository { get; }
	  

		IDSD_T_ShipmentFinanceRepository DSD_T_ShipmentFinanceRepository { get; }
	  

		IDSD_T_ShipmentHeaderRepository DSD_T_ShipmentHeaderRepository { get; }
	  

		IDSD_T_ShipmentHelperRepository DSD_T_ShipmentHelperRepository { get; }
	  

		IDSD_T_ShipmentItemRepository DSD_T_ShipmentItemRepository { get; }
	  

		IDSD_T_TruckCheckResultRepository DSD_T_TruckCheckResultRepository { get; }
	  

		IDSD_T_TruckStockRepository DSD_T_TruckStockRepository { get; }
	  

		IDSD_T_TruckStockTrackingRepository DSD_T_TruckStockTrackingRepository { get; }
	  

		IDSD_T_VisitRepository DSD_T_VisitRepository { get; }
	  

		IMD_AccountRepository MD_AccountRepository { get; }
	  

		IMD_AccountARRepository MD_AccountARRepository { get; }
	  

		IMD_CompanyRepository MD_CompanyRepository { get; }
	  

		IMD_ContactRepository MD_ContactRepository { get; }
	  

		IMD_DictionaryRepository MD_DictionaryRepository { get; }
	  

		IMD_OrganizationRepository MD_OrganizationRepository { get; }
	  

		IMD_PersonRepository MD_PersonRepository { get; }
	  

		IMD_ProdListConfigRepository MD_ProdListConfigRepository { get; }
	  

		IMD_ProductRepository MD_ProductRepository { get; }
	  

		IMD_ProductUOMRepository MD_ProductUOMRepository { get; }
	  

		IMD_UserRepository MD_UserRepository { get; }
	  

		IMD_WarehouseRepository MD_WarehouseRepository { get; }
	  

		ISyncLogRepository SyncLogRepository { get; }
	  

		ISys_ModuleRepository Sys_ModuleRepository { get; }
	  

		ISys_ModuleConfigurationRepository Sys_ModuleConfigurationRepository { get; }
	  

		ISys_RoleRepository Sys_RoleRepository { get; }
	  

		ISys_UserRoleLinkRepository Sys_UserRoleLinkRepository { get; }
	  

		IsysdiagramRepository sysdiagramRepository { get; }
	}
}