// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Export;
using PX.Api.Models;
using PX.CarrierService;
using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.DependencyInjection;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.LicensePolicy;
using PX.Objects.AR;
using PX.Objects.AR.MigrationMode;
using PX.Objects.AR.Standalone;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Scopes;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.IN.InventoryRelease;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.RQ;
using PX.Objects.SO.Attributes;
using PX.Objects.SO.Exceptions;
using PX.Objects.SO.GraphExtensions.CarrierRates;
using PX.Objects.SO.GraphExtensions.SOOrderEntryExt;
using PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;
using PX.Objects.SO.POCreateExt;
using PX.Objects.SO.Standalone;
using PX.Objects.TX;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

#nullable enable
namespace PX.Objects.SO;

[Serializable]
public class SOOrderEntry : 
  PXGraph<
  #nullable disable
  SOOrderEntry, SOOrder>,
  PXImportAttribute.IPXPrepareItems,
  PXImportAttribute.IPXProcess,
  IGraphWithInitialization,
  IPXReusableGraph
{
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> bAccountBasic;
  [PXHidden]
  public PXSelect<BAccountR> bAccountRBasic;
  public ToggleCurrency<SOOrder> CurrencyView;
  public PXFilter<PX.Objects.AP.Vendor> _Vendor;
  [PXViewName("Sales Order")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (SOOrder.showDiscountsTab)})]
  public PXSelectJoin<SOOrder, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<SOOrder.customerID>>>, Where<SOOrder.orderType, Equal<Optional<SOOrder.orderType>>, And<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>> Document;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (SOOrder.cancelled), typeof (SOOrder.ownerID), typeof (SOOrder.workgroupID), typeof (SOOrder.extRefNbr), typeof (SOOrder.updateNextNumber), typeof (SOOrder.emailed), typeof (SOOrder.orchestrationStatus)})]
  public PXSelect<SOOrder, Where<SOOrder.orderType, Equal<Current<SOOrder.orderType>>, And<SOOrder.orderNbr, Equal<Current<SOOrder.orderNbr>>>>> CurrentDocument;
  public PXSelect<RQRequisitionOrder> rqrequisitionorder;
  public PXSelect<RQRequisition, Where<RQRequisition.reqNbr, Equal<Required<RQRequisition.reqNbr>>>> rqrequisition;
  public PXSelect<SOOrderSite, Where<SOOrderSite.orderType, Equal<Current<SOOrder.orderType>>, And<SOOrderSite.orderNbr, Equal<Current<SOOrder.orderNbr>>>>> SiteList;
  public PXSelect<SOOrderSite, Where<SOOrderSite.orderType, Equal<Required<SOOrderSite.orderType>>, And<SOOrderSite.orderNbr, Equal<Required<SOOrderSite.orderNbr>>, And<SOOrderSite.siteID, Equal<Required<SOOrderSite.siteID>>>>>> OrderSite;
  [PXViewName("Sales Order Line")]
  [PXImport(typeof (SOOrder))]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (SOLine.completed), typeof (SOLine.isLegacyDropShip), typeof (SOLine.curyUnbilledAmt), typeof (SOLine.pOLinkActive), typeof (SOLine.isOrchestratedLine), typeof (SOLine.orchestrationOriginalLineNbr), typeof (SOLine.orchestrationPlanID), typeof (SOLine.orchestrationOriginalSiteID)})]
  public PXOrderedSelect<SOOrder, SOLine, Where<SOLine.orderType, Equal<Current<SOOrder.orderType>>, And<SOLine.orderNbr, Equal<Current<SOOrder.orderNbr>>>>, OrderBy<Asc<SOLine.orderType, Asc<SOLine.orderNbr, Asc<SOLine.sortOrder, Asc<SOLine.lineNbr>>>>>> Transactions;
  public PXSelectReadonly<INItemCost, Where<INItemCost.inventoryID, Equal<Required<SOLine.inventoryID>>, And<INItemCost.curyID, Equal<Required<PX.Objects.GL.Branch.baseCuryID>>>>> initemcost;
  public PXSelectReadonly<INItemStats, Where<INItemStats.inventoryID, Equal<Required<SOLine.inventoryID>>, And<INItemStats.siteID, Equal<Required<SOLine.siteID>>>>> initemstats;
  public PXSelect<PX.Objects.AR.ExternalTransaction, Where<PX.Objects.AR.ExternalTransaction.origRefNbr, Equal<Current<SOOrder.orderNbr>>, And<PX.Objects.AR.ExternalTransaction.origDocType, Equal<Current<SOOrder.orderType>>>>, OrderBy<Desc<PX.Objects.AR.ExternalTransaction.transactionID>>> ExternalTran;
  public PXSelectOrderBy<CCProcTran, OrderBy<Desc<CCProcTran.tranNbr>>> ccProcTran;
  public PXSelect<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter> sitestatusview;
  public PXSelect<INItemSite> initemsite;
  public PXSelect<SOTax, Where<SOTax.orderType, Equal<Current<SOOrder.orderType>>, And<SOTax.orderNbr, Equal<Current<SOOrder.orderNbr>>>>, OrderBy<Asc<SOTax.orderType, Asc<SOTax.orderNbr, Asc<SOTax.taxID>>>>> Tax_Rows;
  public PXSelectJoin<SOTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<SOTaxTran.taxID>>>, Where<SOTaxTran.orderType, Equal<Current<SOOrder.orderType>>, And<SOTaxTran.orderNbr, Equal<Current<SOOrder.orderNbr>>>>> Taxes;
  [PXViewIncludesArchivedRecords]
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<SOOrderShipment, LeftJoin<SOShipment, On<SOShipment.shipmentNbr, Equal<SOOrderShipment.shipmentNbr>, And<SOShipment.shipmentType, Equal<SOOrderShipment.shipmentType>>>>, Where<SOOrderShipment.orderType, Equal<Current<SOOrder.orderType>>, And<SOOrderShipment.orderNbr, Equal<Current<SOOrder.orderNbr>>>>, OrderBy<Asc<SOOrderShipment.shipmentNbr>>> shipmentlist;
  public PXSelect<INItemSiteSettings, Where<INItemSiteSettings.inventoryID, Equal<Required<INItemSiteSettings.inventoryID>>, And<INItemSiteSettings.siteID, Equal<Required<INItemSiteSettings.siteID>>>>> initemsettings;
  public PXSelect<PX.Objects.AR.ARRegister> arregister;
  [PXViewName("Billing Address")]
  public PXSelect<SOBillingAddress, Where<SOBillingAddress.addressID, Equal<Current<SOOrder.billAddressID>>>> Billing_Address;
  [PXViewName("Shipping Address")]
  public PXSelect<SOShippingAddress, Where<SOShippingAddress.addressID, Equal<Current<SOOrder.shipAddressID>>>> Shipping_Address;
  [PXViewName("Billing Contact")]
  public PXSelect<SOBillingContact, Where<SOBillingContact.contactID, Equal<Current<SOOrder.billContactID>>>> Billing_Contact;
  [PXViewName("Shipping Contact")]
  public PXSelect<SOShippingContact, Where<SOShippingContact.contactID, Equal<Current<SOOrder.shipContactID>>>> Shipping_Contact;
  public PXSelect<SOSetupApproval, Where<SOSetupApproval.orderType, Equal<Optional<SOOrder.orderType>>, And<SOSetupApproval.isActive, Equal<True>>>> SetupApproval;
  [PXViewName("Approval")]
  public SOOrderApprovalAutomation Approval;
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<SOOrder.curyInfoID>>>> currencyinfo;
  public PXSetup<ARSetup> arsetup;
  [PXViewName("Customer")]
  public PXSetup<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Optional<SOOrder.customerID>>>> customer;
  public PXSetup<CustomerClass, Where<CustomerClass.customerClassID, Equal<Current<PX.Objects.AR.Customer.customerClassID>>>> customerclass;
  public PXSetup<PX.Objects.TX.TaxZone, Where<PX.Objects.TX.TaxZone.taxZoneID, Equal<Current<SOOrder.taxZoneID>>>> taxzone;
  public PXSetup<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Optional<SOOrder.customerLocationID>>>>> location;
  public PXSelect<ARBalances> arbalances;
  public PXSetup<SOOrderType, Where<SOOrderType.orderType, Equal<Optional<SOOrder.orderType>>>> soordertype;
  public PXSetup<SOOrderTypeOperation, Where<SOOrderTypeOperation.orderType, Equal<Optional<SOOrderType.orderType>>, And<SOOrderTypeOperation.operation, Equal<Optional<SOOrderType.defaultOperation>>>>> sooperation;
  [PXCopyPasteHiddenView]
  [PXFilterable(new System.Type[] {})]
  public PXSelect<SOLineSplit, Where<SOLineSplit.orderType, Equal<Current<SOLine.orderType>>, And<SOLineSplit.orderNbr, Equal<Current<SOLine.orderNbr>>, And<SOLineSplit.lineNbr, Equal<Current<SOLine.lineNbr>>>>>> splits;
  public PXSelect<SOLine, Where<SOLine.orderType, Equal<Current<SOOrder.orderType>>, And<SOLine.orderNbr, Equal<Current<SOOrder.orderNbr>>, And<SOLine.isFree, Equal<boolTrue>>>>, OrderBy<Asc<SOLine.orderType, Asc<SOLine.orderNbr, Asc<SOLine.lineNbr>>>>> FreeItems;
  public PXSelect<SOOrderDiscountDetail, Where<SOOrderDiscountDetail.orderType, Equal<Current<SOOrder.orderType>>, And<SOOrderDiscountDetail.orderNbr, Equal<Current<SOOrder.orderNbr>>>>, OrderBy<Asc<SOOrderDiscountDetail.lineNbr>>> DiscountDetails;
  public PXSetup<INSetup> insetup;
  public PXSetup<SOSetup> sosetup;
  public PXSetup<PX.Objects.GL.Branch, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.branchID, Equal<PX.Objects.GL.Branch.branchID>>>, Where<PX.Objects.IN.INSite.siteID, Equal<Optional<SOOrder.destinationSiteID>>>> Company;
  public PXSetupOptional<CommonSetup> commonsetup;
  public PXSelect<PX.Objects.CM.CurrencyInfo> DummyCuryInfo;
  public PXFilter<SOParamFilter> soparamfilter;
  public PXFilter<CopyParamFilter> copyparamfilter;
  public PXFilter<RecalcDiscountsParamFilter> recalcdiscountsfilter;
  public PXSelect<INTranSplit> intransplit;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<SOLineSplit2, LeftJoin<INItemPlan, On<INItemPlan.planID, Equal<SOLineSplit2.planID>>>, Where<SOLineSplit2.sOOrderType, Equal<Optional<SOLineSplit.orderType>>, And<SOLineSplit2.sOOrderNbr, Equal<Optional<SOLineSplit.orderNbr>>, And<SOLineSplit2.sOLineNbr, Equal<Optional<SOLineSplit.lineNbr>>, And<SOLineSplit2.sOSplitLineNbr, Equal<Optional<SOLineSplit.splitLineNbr>>>>>>> sodemand;
  public PXSelect<SOSalesPerTran, Where<SOSalesPerTran.orderType, Equal<Current<SOOrder.orderType>>, And<SOSalesPerTran.orderNbr, Equal<Current<SOOrder.orderNbr>>>>> SalesPerTran;
  public PXSelect<SOOrder, Where<SOOrder.orderType, Equal<Current<SOOrder.orderType>>, And<SOOrder.orderNbr, Equal<Current<SOOrder.orderNbr>>>>> DocumentProperties;
  [PXCopyPasteHiddenView]
  public PXSelect<SOPackageInfoEx, Where<SOPackageInfoEx.orderType, Equal<Current<SOOrder.orderType>>, And<SOPackageInfoEx.orderNbr, Equal<Current<SOOrder.orderNbr>>>>> Packages;
  public PXSelect<INReplenishmentOrder> Replenihment;
  public PXSelectJoin<INReplenishmentLine, InnerJoin<INItemPlan, On<INItemPlan.planID, Equal<INReplenishmentLine.planID>>>, Where<INReplenishmentLine.sOType, Equal<Current<SOOrder.orderType>>, And<INReplenishmentLine.sONbr, Equal<Current<SOOrder.orderNbr>>>>> ReplenishmentLinesWithPlans;
  [PXFilterable(new System.Type[] {})]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<SOAdjust, LeftJoin<PX.Objects.AR.ARPayment, On<PX.Objects.AR.ARPayment.docType, Equal<SOAdjust.adjgDocType>, And<PX.Objects.AR.ARPayment.refNbr, Equal<SOAdjust.adjgRefNbr>>>, LeftJoin<PX.Objects.AR.ExternalTransaction, On<PX.Objects.AR.ExternalTransaction.transactionID, Equal<PX.Objects.AR.ARPayment.cCActualExternalTransactionID>>>>, Where<SOAdjust.adjdOrderType, Equal<Current<SOOrder.orderType>>, And<SOAdjust.adjdOrderNbr, Equal<Current<SOOrder.orderNbr>>>>> Adjustments;
  public PXSelectJoin<SOAdjust, LeftJoin<ARRegisterAlias, On<ARRegisterAlias.docType, Equal<SOAdjust.adjgDocType>, And<ARRegisterAlias.refNbr, Equal<SOAdjust.adjgRefNbr>>>, LeftJoinSingleTable<PX.Objects.AR.ARPayment, On<ARRegisterAlias.docType, Equal<PX.Objects.AR.ARPayment.docType>, And<ARRegisterAlias.refNbr, Equal<PX.Objects.AR.ARPayment.refNbr>>>, LeftJoin<PX.Objects.AR.ExternalTransaction, On<PX.Objects.AR.ExternalTransaction.transactionID, Equal<PX.Objects.AR.ARPayment.cCActualExternalTransactionID>>>>>, Where<SOAdjust.adjdOrderType, Equal<Current<SOOrder.orderType>>, And<SOAdjust.adjdOrderNbr, Equal<Current<SOOrder.orderNbr>>>>> Adjustments_Raw;
  [PXViewName("Sales Person")]
  public PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.salesPersonID, Equal<Current<SOOrder.salesPersonID>>>> SalesPerson;
  [PXViewName("Main Contact")]
  public PXSelect<PX.Objects.CR.Contact> DefaultCompanyContact;
  [PXViewName("Employee")]
  public PXSelect<SOOwner, Where<PX.Objects.EP.EPEmployee.defContactID, Equal<Current<SOOrder.ownerID>>>> Employee;
  public PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Optional<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>>>> PaymentMethodDef;
  public PXSelect<PX.Objects.IN.InventoryItem> dummy_stockitem_for_redirect_newitem;
  public PXInitializeState<SOOrder> initializeState;
  public PXAction<SOOrder> createShipment;
  public PXAction<SOOrder> createShipmentIssue;
  public PXAction<SOOrder> createShipmentReceipt;
  public PXAction<SOOrder> applyAssignmentRules;
  public PXAction<SOOrder> createPurchaseOrder;
  public PXAction<SOOrder> createTransferOrder;
  public PXAction<SOOrder> reopenOrder;
  public PXAction<SOOrder> openOrder;
  public PXAction<SOOrder> cancelOrder;
  public PXAction<SOOrder> putOnHold;
  public PXAction<SOOrder> releaseFromHold;
  public PXAction<SOOrder> releaseFromCreditHold;
  public PXAction<SOOrder> placeOnBackOrder;
  public PXAction<SOOrder> completeOrder;
  public PXAction<SOOrder> inquiry;
  public PXAction<SOOrder> report;
  public PXAction<SOOrder> printSalesOrder;
  public PXAction<SOOrder> printQuote;
  public PXAction<SOOrder> notification;
  public PXAction<SOOrder> emailSalesOrder;
  public PXAction<SOOrder> emailQuote;
  public PXAction<SOOrder> prepareInvoice;
  public PXAction<SOOrder> addInvoice;
  public PXAction<SOOrder> checkCopyParams;
  public PXAction<SOOrder> copyOrderQT;
  public PXAction<SOOrder> copyOrder;
  public PXAction<SOOrder> itemAvailability;
  public PXAction<SOOrder> calculateFreight;
  public PXAction<SOOrder> validateAddresses;
  public PXAction<SOOrder> recalculateDiscountsAction;
  [Obsolete("Use RecalculateDiscountsAction instead. Fill in RecalcDiscountsParamFilter to modify default price and discount recalculation behavior when calling RecalculateDiscountsAction() from import scenarios or when using CB-API")]
  public PXAction<SOOrder> RecalculateDiscountsFromImport;
  [Obsolete("Use RecalculateDiscountsAction instead. Fill in RecalcDiscountsParamFilter to modify default price and discount recalculation behavior when calling RecalculateDiscountsAction() from import scenarios or when using CB-API")]
  public PXAction<SOOrder> RecalculatePricesAndDiscountsFromImport;
  public PXAction<SOOrder> recalcOk;
  public PXAction<SOOrder> viewBlanketOrder;
  public PXAction<SOOrder> viewOrigOrder;
  public PXWorkflowEventHandler<SOOrder> OnOrderDeleted_ReopenQuote;
  public PXWorkflowEventHandler<SOOrder> OnShipmentCreationFailed;
  public PXWorkflowEventHandler<SOOrder> OnPaymentRequirementsSatisfied;
  public PXWorkflowEventHandler<SOOrder> OnPaymentRequirementsViolated;
  public PXWorkflowEventHandler<SOOrder> OnObtainedPaymentInPendingProcessing;
  public PXWorkflowEventHandler<SOOrder> OnLostLastPaymentInPendingProcessing;
  public PXWorkflowEventHandler<SOOrder> OnCreditLimitSatisfied;
  public PXWorkflowEventHandler<SOOrder> OnCreditLimitViolated;
  public PXWorkflowEventHandler<SOOrder> OnBlanketCompleted;
  public PXWorkflowEventHandler<SOOrder> OnBlanketReopened;
  public PXWorkflowEventHandler<SOOrder, SOOrderShipment, SOInvoice> OnInvoiceLinked;
  public PXWorkflowEventHandler<SOOrder, SOOrderShipment, SOInvoice> OnInvoiceUnlinked;
  public PXWorkflowEventHandler<SOOrder, SOOrderShipment, SOShipment> OnShipmentLinked;
  public PXWorkflowEventHandler<SOOrder, SOOrderShipment, SOShipment> OnShipmentUnlinked;
  public PXWorkflowEventHandler<SOOrder, SOInvoice> OnInvoiceReleased;
  public PXWorkflowEventHandler<SOOrder, SOInvoice> OnInvoiceCancelled;
  public PXWorkflowEventHandler<SOOrder> OnShipmentConfirmed;
  public PXWorkflowEventHandler<SOOrder> OnShipmentCorrected;
  protected SOOrder _LastSelected;
  protected readonly string viewInventoryID;
  private int _persistNesting;
  protected HashSet<string> prefetched = new HashSet<string>();

  private DiscountEngine<SOLine, SOOrderDiscountDetail> _discountEngine
  {
    get => DiscountEngineProvider.GetEngineFor<SOLine, SOOrderDiscountDetail>();
  }

  public SOOrderLineSplittingExtension LineSplittingExt
  {
    get => ((PXGraph) this).FindImplementation<SOOrderLineSplittingExtension>();
  }

  public SOOrderLineSplittingAllocatedExtension LineSplittingAllocatedExt
  {
    get => ((PXGraph) this).FindImplementation<SOOrderLineSplittingAllocatedExtension>();
  }

  public SOOrderItemAvailabilityExtension ItemAvailabilityExt
  {
    get => ((PXGraph) this).FindImplementation<SOOrderItemAvailabilityExtension>();
  }

  /// <summary>
  /// If true the SO-PO Link dialog will display PO Orders on hold.
  /// </summary>
  /// <remarks>This setting is used when linking On-Hold PO Orders with SO created through RQRequisitionEntry.</remarks>
  public bool SOPOLinkShowDocumentsOnHold { get; set; }

  [PXOptimizationBehavior(IgnoreBqlDelegate = true)]
  protected virtual IEnumerable transactions()
  {
    this.PrefetchWithDetails();
    PXSelectBase<SOLine> pxSelectBase = (PXSelectBase<SOLine>) new PXSelectReadonly2<SOLine, InnerJoin<INItemCost, On<INItemCost.inventoryID, Equal<SOLine.inventoryID>, And<INItemCost.curyID, EqualBaseCuryID<Current2<SOOrder.branchID>>>>, InnerJoin<INItemStats, On<INItemStats.inventoryID, Equal<SOLine.inventoryID>, And<INItemStats.siteID, Equal<SOLine.siteID>>>>>, Where<SOLine.orderType, Equal<Current<SOOrder.orderType>>, And<SOLine.orderNbr, Equal<Current<SOOrder.orderNbr>>>>, OrderBy<Asc<SOLine.orderType, Asc<SOLine.orderNbr, Asc<SOLine.sortOrder, Asc<SOLine.lineNbr>>>>>>((PXGraph) this);
    using (new PXFieldScope(((PXSelectBase) pxSelectBase).View, new System.Type[2]
    {
      typeof (INItemCost),
      typeof (INItemStats)
    }))
    {
      int startRow = PXView.StartRow;
      int num = 0;
      foreach (PXResult<SOLine, INItemCost, INItemStats> pxResult in ((PXSelectBase) pxSelectBase).View.Select(PXView.Currents, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num))
      {
        ((PXSelectBase<INItemCost>) this.initemcost).StoreResult((IBqlTable) PXResult<SOLine, INItemCost, INItemStats>.op_Implicit(pxResult));
        ((PXSelectBase<INItemStats>) this.initemstats).StoreResult((IBqlTable) PXResult<SOLine, INItemCost, INItemStats>.op_Implicit(pxResult));
      }
    }
    return (IEnumerable) null;
  }

  public virtual void PrefetchWithDetails() => this.LoadEntityDiscounts();

  public IEnumerable CcProcTran()
  {
    SOOrderEntry soOrderEntry = this;
    PXResultset<PX.Objects.AR.ExternalTransaction> pxResultset = ((PXSelectBase<PX.Objects.AR.ExternalTransaction>) soOrderEntry.ExternalTran).Select(Array.Empty<object>());
    PXSelect<CCProcTran, Where<CCProcTran.transactionID, Equal<Required<CCProcTran.transactionID>>>> query = new PXSelect<CCProcTran, Where<CCProcTran.transactionID, Equal<Required<CCProcTran.transactionID>>>>((PXGraph) soOrderEntry);
    foreach (PXResult<PX.Objects.AR.ExternalTransaction> pxResult1 in pxResultset)
    {
      PX.Objects.AR.ExternalTransaction externalTransaction = PXResult<PX.Objects.AR.ExternalTransaction>.op_Implicit(pxResult1);
      PXSelect<CCProcTran, Where<CCProcTran.transactionID, Equal<Required<CCProcTran.transactionID>>>> pxSelect = query;
      object[] objArray = new object[1]
      {
        (object) externalTransaction.TransactionID
      };
      foreach (PXResult<CCProcTran> pxResult2 in ((PXSelectBase<CCProcTran>) pxSelect).Select(objArray))
        yield return (object) PXResult<CCProcTran>.op_Implicit(pxResult2);
    }
  }

  public virtual void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers)
  {
    EnumerableExtensions.ForEach<Command>(script.Where<Command>((Func<Command, bool>) (_ => _.ObjectName.StartsWith("Transactions"))), (Action<Command>) (_ => _.Commit = false));
    script.Where<Command>((Func<Command, bool>) (_ => _.ObjectName.StartsWith("Transactions"))).Last<Command>().Commit = true;
    this.ProcessAtTheEnd(script, containers, "CustomerOrderNbr");
  }

  protected virtual void ProcessAtTheEnd(
    List<Command> script,
    List<Container> containers,
    string fieldName)
  {
    int index = script.FindIndex((Predicate<Command>) (_ => _.FieldName == fieldName));
    if (index == -1)
      return;
    Command command = script[index];
    Container container = containers[index];
    script.Remove(command);
    containers.Remove(container);
    script.Add(command);
    containers.Add(container);
  }

  protected virtual IEnumerable defaultCompanyContact()
  {
    return (IEnumerable) OrganizationMaint.GetDefaultContactForCurrentOrganization((PXGraph) this);
  }

  [PXOptimizationBehavior(IgnoreBqlDelegate = true)]
  public virtual IEnumerable adjustments()
  {
    PX.Objects.CM.CurrencyInfo inv_info = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Select(Array.Empty<object>()));
    ((PXSelectBase) this.Adjustments_Raw).View.Clear();
    foreach (PXResult<SOAdjust, ARRegisterAlias, PX.Objects.AR.ARPayment> pxResult in ((PXSelectBase<SOAdjust>) this.Adjustments_Raw).Select(Array.Empty<object>()))
    {
      PX.Objects.AR.ARPayment payment = PXResult<SOAdjust, ARRegisterAlias, PX.Objects.AR.ARPayment>.op_Implicit(pxResult);
      SOAdjust adj = PXResult<SOAdjust, ARRegisterAlias, PX.Objects.AR.ARPayment>.op_Implicit(pxResult);
      if (adj != null)
      {
        if (payment != null)
          PXCache<PX.Objects.AR.ARRegister>.RestoreCopy((PX.Objects.AR.ARRegister) payment, (PX.Objects.AR.ARRegister) PXResult<SOAdjust, ARRegisterAlias, PX.Objects.AR.ARPayment>.op_Implicit(pxResult));
        this.CalculateApplicationBalance(inv_info, payment, adj);
        yield return (object) pxResult;
      }
    }
  }

  public virtual void CalculateApplicationBalance(
    PX.Objects.CM.CurrencyInfo inv_info,
    PX.Objects.AR.ARPayment payment,
    SOAdjust adj)
  {
    Decimal paymentBalance = this.GetPaymentBalance(inv_info, payment, adj);
    if (adj.Voided.GetValueOrDefault())
      return;
    Decimal? curyAdjdAmt = adj.CuryAdjdAmt;
    Decimal num1 = paymentBalance;
    if (curyAdjdAmt.GetValueOrDefault() > num1 & curyAdjdAmt.HasValue)
    {
      adj.CuryDocBal = new Decimal?(0M);
    }
    else
    {
      SOAdjust soAdjust = adj;
      Decimal num2 = paymentBalance;
      curyAdjdAmt = adj.CuryAdjdAmt;
      Decimal? nullable = curyAdjdAmt.HasValue ? new Decimal?(num2 - curyAdjdAmt.GetValueOrDefault()) : new Decimal?();
      soAdjust.CuryDocBal = nullable;
    }
  }

  public virtual Decimal GetPaymentBalance(PX.Objects.CM.CurrencyInfo inv_info, PX.Objects.AR.ARPayment payment, SOAdjust adj)
  {
    PX.Objects.AR.ARPayment copy = PXCache<PX.Objects.AR.ARPayment>.CreateCopy(payment);
    this.CalculatePaymentBalance(copy, adj);
    Decimal curyval;
    if (string.Equals(payment.CuryID, inv_info.CuryID))
    {
      curyval = copy.CuryDocBal.Value;
    }
    else
    {
      Decimal? docBal = payment.DocBal;
      Decimal? nullable1 = copy.DocBal;
      Decimal? nullable2 = docBal.HasValue & nullable1.HasValue ? new Decimal?(docBal.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
      nullable1 = payment.Released.GetValueOrDefault() ? payment.DocBal : payment.OrigDocAmt;
      Decimal baseval = nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault();
      PX.Objects.CM.PXDBCurrencyAttribute.CuryConvCury(((PXSelectBase) this.Adjustments).Cache, inv_info, baseval, out curyval);
    }
    return curyval;
  }

  protected virtual void CalculatePaymentBalance(PX.Objects.AR.ARPayment payment, SOAdjust adj)
  {
    SOAdjust soAdjust = PXResultset<SOAdjust>.op_Implicit(PXSelectBase<SOAdjust, PXSelectGroupBy<SOAdjust, Where<SOAdjust.adjgDocType, Equal<Required<SOAdjust.adjgDocType>>, And<SOAdjust.adjgRefNbr, Equal<Required<SOAdjust.adjgRefNbr>>, And<Where<SOAdjust.adjdOrderType, NotEqual<Required<SOAdjust.adjdOrderType>>, Or<SOAdjust.adjdOrderNbr, NotEqual<Required<SOAdjust.adjdOrderNbr>>>>>>>, Aggregate<GroupBy<SOAdjust.adjgDocType, GroupBy<SOAdjust.adjgRefNbr, Sum<SOAdjust.curyAdjgAmt, Sum<SOAdjust.adjAmt>>>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) adj.AdjgDocType,
      (object) adj.AdjgRefNbr,
      (object) adj.AdjdOrderType,
      (object) adj.AdjdOrderNbr
    }));
    Decimal? nullable1;
    Decimal? nullable2;
    if (soAdjust != null && soAdjust.AdjdOrderNbr != null)
    {
      PX.Objects.AR.ARPayment arPayment1 = payment;
      nullable1 = arPayment1.CuryDocBal;
      nullable2 = soAdjust.CuryAdjgAmt;
      Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
      Decimal? nullable3;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable3 = nullable2;
      }
      else
        nullable3 = new Decimal?(nullable1.GetValueOrDefault() - valueOrDefault1);
      arPayment1.CuryDocBal = nullable3;
      PX.Objects.AR.ARPayment arPayment2 = payment;
      nullable1 = arPayment2.DocBal;
      nullable2 = soAdjust.AdjAmt;
      Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
      Decimal? nullable4;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable4 = nullable2;
      }
      else
        nullable4 = new Decimal?(nullable1.GetValueOrDefault() - valueOrDefault2);
      arPayment2.DocBal = nullable4;
    }
    PX.Objects.AR.ARAdjust arAdjust = PXResultset<PX.Objects.AR.ARAdjust>.op_Implicit(PXSelectBase<PX.Objects.AR.ARAdjust, PXSelectGroupBy<PX.Objects.AR.ARAdjust, Where<PX.Objects.AR.ARAdjust.adjgDocType, Equal<Required<PX.Objects.AR.ARAdjust.adjgDocType>>, And<PX.Objects.AR.ARAdjust.adjgRefNbr, Equal<Required<PX.Objects.AR.ARAdjust.adjgRefNbr>>, And<PX.Objects.AR.ARAdjust.released, Equal<boolFalse>>>>, Aggregate<GroupBy<PX.Objects.AR.ARAdjust.adjgDocType, GroupBy<PX.Objects.AR.ARAdjust.adjgRefNbr, Sum<PX.Objects.AR.ARAdjust.curyAdjgAmt, Sum<PX.Objects.AR.ARAdjust.adjAmt>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) adj.AdjgDocType,
      (object) adj.AdjgRefNbr
    }));
    if (arAdjust == null || arAdjust.AdjdRefNbr == null)
      return;
    PX.Objects.AR.ARPayment arPayment3 = payment;
    nullable1 = arPayment3.CuryDocBal;
    nullable2 = arAdjust.CuryAdjgAmt;
    Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
    Decimal? nullable5;
    if (!nullable1.HasValue)
    {
      nullable2 = new Decimal?();
      nullable5 = nullable2;
    }
    else
      nullable5 = new Decimal?(nullable1.GetValueOrDefault() - valueOrDefault3);
    arPayment3.CuryDocBal = nullable5;
    PX.Objects.AR.ARPayment arPayment4 = payment;
    nullable1 = arPayment4.DocBal;
    nullable2 = arAdjust.AdjAmt;
    Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
    Decimal? nullable6;
    if (!nullable1.HasValue)
    {
      nullable2 = new Decimal?();
      nullable6 = nullable2;
    }
    else
      nullable6 = new Decimal?(nullable1.GetValueOrDefault() - valueOrDefault4);
    arPayment4.DocBal = nullable6;
  }

  [PXMergeAttributes]
  [PXParent(typeof (Select<SOOrder, Where<SOOrder.orderType, Equal<Current<CCProcTran.origDocType>>, And<SOOrder.orderNbr, Equal<Current<CCProcTran.origRefNbr>>>>>))]
  protected virtual void CCProcTran_RefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXParent(typeof (Select<SOOrder, Where<SOOrder.orderType, Equal<Current<RQRequisitionOrder.orderType>>, And<SOOrder.orderNbr, Equal<Current<RQRequisitionOrder.orderNbr>>>>>))]
  protected virtual void RQRequisitionOrder_OrderNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXParent(typeof (RQRequisitionOrder.FK.Requisition))]
  protected virtual void _(PX.Data.Events.CacheAttached<RQRequisitionOrder.reqNbr> e)
  {
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable CreateShipment(
    PXAdapter adapter,
    [PXDate] DateTime? shipDate,
    [PXInt] int? siteID,
    [PXDate] DateTime? endDate,
    [SOOperation.List] string operation)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SOOrderEntry.\u003C\u003Ec__DisplayClass89_0 cDisplayClass890 = new SOOrderEntry.\u003C\u003Ec__DisplayClass89_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass890.endDate = endDate;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass890.operation = operation;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass890.list = adapter.Get<SOOrder>().ToList<SOOrder>();
    if (shipDate.HasValue)
      ((PXSelectBase<SOParamFilter>) this.soparamfilter).Current.ShipDate = shipDate;
    if (siteID.HasValue)
      ((PXSelectBase<SOParamFilter>) this.soparamfilter).Current.SiteID = siteID;
    if (!((PXSelectBase<SOParamFilter>) this.soparamfilter).Current.ShipDate.HasValue)
      ((PXSelectBase<SOParamFilter>) this.soparamfilter).Current.ShipDate = ((PXGraph) this).Accessinfo.BusinessDate;
    if (!adapter.MassProcess)
    {
      if (!((PXSelectBase<SOParamFilter>) this.soparamfilter).Current.SiteID.HasValue)
        ((PXSelectBase<SOParamFilter>) this.soparamfilter).Current.SiteID = this.GetPreferedSiteID();
      if (adapter.ExternalCall)
        ((PXSelectBase<SOParamFilter>) this.soparamfilter).AskExt(true);
    }
    if (((PXSelectBase<SOParamFilter>) this.soparamfilter).Current.SiteID.HasValue || adapter.MassProcess)
    {
      try
      {
        this.RecalculateExternalTaxesSync = true;
        ((PXAction) this.Save).Press();
      }
      finally
      {
        this.RecalculateExternalTaxesSync = false;
      }
      // ISSUE: reference to a compiler-generated field
      cDisplayClass890.filter = ((PXSelectBase<SOParamFilter>) this.soparamfilter).Current;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass890.adapterSlice = (adapter.MassProcess, adapter.QuickProcessFlow, adapter.AllowRedirect);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass890.userName = ((PXGraph) this).Accessinfo.UserName;
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass890, __methodptr(\u003CCreateShipment\u003Eb__0)));
    }
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass890.list;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable CreateShipmentIssue(
    PXAdapter adapter,
    [PXDate] DateTime? shipDate,
    [PXInt] int? siteID,
    [PXDate] DateTime? endDate)
  {
    return this.CreateShipment(adapter, shipDate, siteID, endDate, "I");
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable CreateShipmentReceipt(
    PXAdapter adapter,
    [PXDate] DateTime? shipDate,
    [PXInt] int? siteID,
    [PXDate] DateTime? endDate)
  {
    return this.CreateShipment(adapter, shipDate, siteID, endDate, "R");
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ApplyAssignmentRules(PXAdapter adapter)
  {
    if (!((PXSelectBase<SOSetup>) this.sosetup).Current.DefaultOrderAssignmentMapID.HasValue)
      throw new PXSetPropertyException("Default Sales Order Assignment Map is not entered in Sales Orders Preferences", new object[1]
      {
        (object) "Sales Orders Preferences"
      });
    List<SOOrder> list = adapter.Get<SOOrder>().ToList<SOOrder>();
    PXGraph.CreateInstance<EPAssignmentProcessor<SOOrder>>().Assign(((PXSelectBase<SOOrder>) this.Document).Current, ((PXSelectBase<SOSetup>) this.sosetup).Current.DefaultOrderAssignmentMapID);
    ((PXSelectBase<SOOrder>) this.Document).Update(((PXSelectBase<SOOrder>) this.Document).Current);
    return (IEnumerable) list;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable CreatePurchaseOrder(PXAdapter adapter)
  {
    List<SOOrder> list = adapter.Get<SOOrder>().ToList<SOOrder>();
    if (list.Count > 0)
    {
      ((PXAction) this.Save).Press();
      POCreate instance = PXGraph.CreateInstance<POCreate>();
      ((PXSelectBase<POCreate.POCreateFilter>) instance.Filter).Current.BranchID = (int?) ((PXSelectBase<SOOrder>) this.Document).Current?.BranchID;
      PXCacheEx.GetExtension<SOxPOCreateFilter>((IBqlTable) ((PXSelectBase<POCreate.POCreateFilter>) instance.Filter).Current).OrderType = list[0].OrderType;
      PXCacheEx.GetExtension<SOxPOCreateFilter>((IBqlTable) ((PXSelectBase<POCreate.POCreateFilter>) instance.Filter).Current).OrderNbr = list[0].OrderNbr;
      throw new PXRedirectRequiredException((PXGraph) instance, "Purchase Order '{0}' created.");
    }
    return (IEnumerable) list;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable CreateTransferOrder(PXAdapter adapter)
  {
    List<SOOrder> list = adapter.Get<SOOrder>().ToList<SOOrder>();
    if (list.Count > 0)
    {
      ((PXAction) this.Save).Press();
      SOCreate instance = PXGraph.CreateInstance<SOCreate>();
      ((PXSelectBase<SOCreate.SOCreateFilter>) instance.Filter).Current.OrderType = list[0].OrderType;
      ((PXSelectBase<SOCreate.SOCreateFilter>) instance.Filter).Current.OrderNbr = list[0].OrderNbr;
      throw new PXRedirectRequiredException((PXGraph) instance, "Transfer Order '{0}' created.");
    }
    return (IEnumerable) list;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable ReopenOrder(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable OpenOrder(PXAdapter adapter)
  {
    return (IEnumerable) adapter.Get<SOOrder>();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable CancelOrder(PXAdapter adapter)
  {
    return (IEnumerable) adapter.Get<SOOrder>();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold")]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter)
  {
    return (IEnumerable) adapter.Get<SOOrder>();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Remove Hold")]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter)
  {
    return (IEnumerable) adapter.Get<SOOrder>();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReleaseFromCreditHold(PXAdapter adapter)
  {
    return (IEnumerable) adapter.Get<SOOrder>();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable PlaceOnBackOrder(PXAdapter adapter)
  {
    return (IEnumerable) adapter.Get<SOOrder>();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable CompleteOrder(PXAdapter adapter)
  {
    int? openLineCntr = ((PXSelectBase<SOOrder>) this.Document).Current.OpenLineCntr;
    int num = 0;
    if (openLineCntr.GetValueOrDefault() > num & openLineCntr.HasValue)
    {
      foreach (SOLine soLine in ((IEnumerable<SOLine>) ((PXSelectBase<SOLine>) this.Transactions).SelectMain(Array.Empty<object>())).Where<SOLine>((Func<SOLine, bool>) (l => !l.Completed.GetValueOrDefault())))
      {
        soLine.Completed = new bool?(true);
        ((PXSelectBase<SOLine>) this.Transactions).Update(soLine);
      }
    }
    return (IEnumerable) adapter.Get<SOOrder>();
  }

  private int? GetPreferedSiteID()
  {
    int? preferedSiteId = new int?();
    PXResultset<SOOrderSite> pxResultset = PXSelectBase<SOOrderSite, PXSelectJoin<SOOrderSite, InnerJoin<PX.Objects.IN.INSite, On<SOOrderSite.FK.Site>>, Where<SOOrderSite.orderType, Equal<Current<SOOrder.orderType>>, And<SOOrderSite.orderNbr, Equal<Current<SOOrder.orderNbr>>, And<SOOrderSite.openLineCntr, Greater<int0>, And<SOOrderSite.openShipmentCntr, Equal<int0>, And<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    if (pxResultset.Count == 1)
    {
      preferedSiteId = PXResultset<SOOrderSite>.op_Implicit(pxResultset).SiteID;
    }
    else
    {
      SOOrderSite soOrderSite;
      if ((soOrderSite = PXResultset<SOOrderSite>.op_Implicit(PXSelectBase<SOOrderSite, PXSelectJoin<SOOrderSite, InnerJoin<PX.Objects.IN.INSite, On<SOOrderSite.FK.Site>>, Where<SOOrderSite.orderType, Equal<Current<SOOrder.orderType>>, And<SOOrderSite.orderNbr, Equal<Current<SOOrder.orderNbr>>, And<SOOrderSite.siteID, Equal<Current<SOOrder.defaultSiteID>>, And<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))) != null)
        preferedSiteId = soOrderSite.SiteID;
    }
    return preferedSiteId;
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Inquiry(PXAdapter adapter, [PXInt, PXIntList(new int[] {}, new string[] {})] int? inquiryID, [PXString] string ActionName)
  {
    if (!string.IsNullOrEmpty(ActionName))
    {
      PXAction action = ((PXGraph) this).Actions[ActionName];
      if (action != null)
      {
        ((PXAction) this.Save).Press();
        foreach (object obj in action.Press(adapter))
          ;
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Report(PXAdapter adapter, [PXString(8, InputMask = "CC.CC.CC.CC")] string reportID)
  {
    List<SOOrder> list = adapter.Get<SOOrder>().ToList<SOOrder>();
    if (!string.IsNullOrEmpty(reportID))
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      string actualReportID = (string) null;
      PXReportRequiredException ex = (PXReportRequiredException) null;
      Dictionary<PrintSettings, PXReportRequiredException> reportsToPrint = new Dictionary<PrintSettings, PXReportRequiredException>();
      PXProcessing<SOOrder>.ProcessRecords((IEnumerable<SOOrder>) list, adapter.MassProcess, (Action<SOOrder>) (order =>
      {
        parameters = new Dictionary<string, string>();
        parameters["SOOrder.OrderType"] = order.OrderType;
        parameters["SOOrder.OrderNbr"] = order.OrderNbr;
        actualReportID = new NotificationUtility((PXGraph) this).SearchCustomerReport(reportID, order.CustomerID, order.BranchID);
        ex = PXReportRequiredException.CombineReport(ex, actualReportID, parameters, OrganizationLocalizationHelper.GetCurrentLocalization((PXGraph) this));
        ((PXBaseRedirectException) ex).Mode = (PXBaseRedirectException.WindowMode) 2;
        reportsToPrint = SMPrintJobMaint.AssignPrintJobToPrinter(reportsToPrint, parameters, adapter, new Func<string, string, int?, Guid?>(new NotificationUtility((PXGraph) this).SearchPrinter), "Customer", reportID, actualReportID, order.BranchID, OrganizationLocalizationHelper.GetCurrentLocalization((PXGraph) this));
      }), (Action<SOOrder>) null, (Func<SOOrder, Exception, bool, bool?>) null, (Action<SOOrder>) null, (Action<SOOrder>) null);
      if (ex != null)
        ((PXGraph) this).LongOperationManager.StartAsyncOperation((Func<CancellationToken, System.Threading.Tasks.Task>) (async ct =>
        {
          int num = await SMPrintJobMaint.CreatePrintJobGroups(reportsToPrint, ct) ? 1 : 0;
          throw ex;
        }));
    }
    return (IEnumerable) list;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable PrintSalesOrder(PXAdapter adapter, string reportID = null)
  {
    return this.Report(adapter.Apply<PXAdapter>((Action<PXAdapter>) (it => it.Menu = "Print Sales Order")), reportID ?? "SO641010");
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable PrintQuote(PXAdapter adapter, string reportID = null)
  {
    return this.Report(adapter.Apply<PXAdapter>((Action<PXAdapter>) (it => it.Menu = "Print Quote")), reportID ?? "SO641000");
  }

  [PXUIField(DisplayName = "Notifications", Visible = false)]
  [PXButton(ImageKey = "DataEntryF")]
  public virtual IEnumerable Notification(PXAdapter adapter, [PXString] string notificationCD)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SOOrderEntry.\u003C\u003Ec__DisplayClass126_0 displayClass1260 = new SOOrderEntry.\u003C\u003Ec__DisplayClass126_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1260.notificationCD = notificationCD;
    // ISSUE: reference to a compiler-generated field
    displayClass1260.orders = adapter.Get<SOOrder>().ToArray<SOOrder>();
    // ISSUE: reference to a compiler-generated field
    displayClass1260.parameters = new MassEmailingActionParameters(adapter);
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1260, __methodptr(\u003CNotification\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) displayClass1260.orders;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable EmailSalesOrder(PXAdapter adapter, [PXString] string notificationCD = null)
  {
    return this.Notification(adapter, notificationCD ?? "SALES ORDER");
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable EmailQuote(PXAdapter adapter, [PXString] string notificationCD = null)
  {
    return this.Notification(adapter, notificationCD ?? "QUOTE");
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable PrepareInvoice(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SOOrderEntry.\u003C\u003Ec__DisplayClass132_0 displayClass1320 = new SOOrderEntry.\u003C\u003Ec__DisplayClass132_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1320.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    displayClass1320.list = adapter.Get<SOOrder>().ToList<SOOrder>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated method
    PXProcessing<SOOrder>.ProcessRecords((IEnumerable<SOOrder>) displayClass1320.list, adapter.MassProcess, new Action<SOOrder>(displayClass1320.\u003CPrepareInvoice\u003Eb__0), (Action<SOOrder>) null, (Func<SOOrder, Exception, bool, bool?>) null, (Action<SOOrder>) null, (Action<SOOrder>) null);
    if (!adapter.MassProcess)
    {
      try
      {
        this.RecalculateExternalTaxesSync = true;
        ((PXAction) this.Save).Press();
      }
      finally
      {
        this.RecalculateExternalTaxesSync = false;
      }
    }
    // ISSUE: reference to a compiler-generated field
    displayClass1320.arguments = adapter.Arguments;
    // ISSUE: reference to a compiler-generated field
    displayClass1320.massProcess = adapter.MassProcess;
    // ISSUE: reference to a compiler-generated field
    displayClass1320.quickProcessFlow = adapter.QuickProcessFlow;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1320, __methodptr(\u003CPrepareInvoice\u003Eb__1)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) displayClass1320.list;
  }

  protected virtual void InvoiceOrders(
    List<SOOrder> list,
    Dictionary<string, object> arguments,
    bool massProcess,
    PXQuickProcess.ActionFlow quickProcessFlow)
  {
    SOShipmentEntry shipmentEntry = PXGraph.CreateInstance<SOShipmentEntry>();
    SOOrderExtension soOrderExt = ((PXGraph) shipmentEntry).GetExtension<SOOrderExtension>();
    InvoiceList created = new InvoiceList((PXGraph) shipmentEntry);
    this.InvoiceOrder(arguments, (IEnumerable<SOOrder>) list, created, massProcess, quickProcessFlow, false);
    if (massProcess)
      list.ForEach((Action<SOOrder>) (o => ((PXSelectBase) soOrderExt.soorder).Cache.RestoreCopy((object) o, (object) PrimaryKeyOf<SOOrder>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<SOOrder.orderType, SOOrder.orderNbr>>.Find((PXGraph) shipmentEntry, (TypeArrayOf<IBqlField>.IFilledWith<SOOrder.orderType, SOOrder.orderNbr>) o, (PKFindOptions) 0))));
    if (massProcess || created.Count <= 0)
      return;
    using (new PXTimeStampScope((byte[]) null))
    {
      SOInvoiceEntry instance = PXGraph.CreateInstance<SOInvoiceEntry>();
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARInvoice.docType, PX.Objects.AR.ARInvoice.refNbr>((object) PXResult<PX.Objects.AR.ARInvoice, SOInvoice>.op_Implicit(created[0]).DocType, (object) PXResult<PX.Objects.AR.ARInvoice, SOInvoice>.op_Implicit(created[0]).RefNbr, new object[1]
      {
        (object) PXResult<PX.Objects.AR.ARInvoice, SOInvoice>.op_Implicit(created[0]).DocType
      }));
      throw new PXRedirectRequiredException((PXGraph) instance, "Invoice");
    }
  }

  [PXUIField]
  [PXLookupButton(IgnoresArchiveDisabling = true)]
  public virtual IEnumerable CheckCopyParams(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true, IgnoresArchiveDisabling = true)]
  [PXUIField]
  public virtual IEnumerable CopyOrderQT(PXAdapter adapter) => this.CopyOrder(adapter);

  [PXButton(CommitChanges = true, IgnoresArchiveDisabling = true)]
  [PXUIField]
  public virtual IEnumerable CopyOrder(PXAdapter adapter)
  {
    List<SOOrder> list = adapter.Get<SOOrder>().ToList<SOOrder>();
    // ISSUE: method pointer
    WebDialogResult webDialogResult = ((PXSelectBase<CopyParamFilter>) this.copyparamfilter).AskExt(new PXView.InitializePanel((object) this, __methodptr(setStateFilter)), true);
    if (webDialogResult != 1 && (!((PXGraph) this).IsContractBasedAPI || webDialogResult != 6) || string.IsNullOrEmpty(((PXSelectBase<CopyParamFilter>) this.copyparamfilter).Current.OrderType))
      return (IEnumerable) list;
    ((PXAction) this.Save).Press();
    SOOrder copy = PXCache<SOOrder>.CreateCopy(((PXSelectBase<SOOrder>) this.Document).Current);
    this.IsCopyOrder = true;
    try
    {
      using (((PXGraph) this).IsArchiveContext.GetValueOrDefault() ? new PXReadThroughArchivedScope() : (PXReadThroughArchivedScope) null)
        this.CopyOrderProc(copy, ((PXSelectBase<CopyParamFilter>) this.copyparamfilter).Current);
    }
    finally
    {
      this.IsCopyOrder = false;
    }
    return (IEnumerable) new List<SOOrder>()
    {
      ((PXSelectBase<SOOrder>) this.Document).Current
    };
  }

  private void setStateFilter(PXGraph aGraph, string ViewName)
  {
    ((PXAction) this.checkCopyParams).SetEnabled(!string.IsNullOrEmpty(((PXSelectBase<CopyParamFilter>) this.copyparamfilter).Current.OrderType) && !string.IsNullOrEmpty(((PXSelectBase<CopyParamFilter>) this.copyparamfilter).Current.OrderNbr));
  }

  public virtual void CopyOrderProc(SOOrder sourceOrder, CopyParamFilter copyFilter)
  {
    string orderType = copyFilter.OrderType;
    string orderNbr = copyFilter.OrderNbr;
    bool flag1 = copyFilter.RecalcUnitPrices.Value;
    bool flag2 = copyFilter.OverrideManualPrices.Value;
    bool flag3 = copyFilter.RecalcDiscounts.Value;
    bool flag4 = copyFilter.OverrideManualDiscounts.Value;
    bool flag5 = false;
    Dictionary<string, object> dictionary = ((IEnumerable<string>) ((PXSelectBase) this.Document).Cache.Fields).Where<string>(new Func<string, bool>(((PXSelectBase) this.Document).Cache.IsKvExtAttribute)).ToDictionary<string, string, object>((Func<string, string>) (udField => udField), (Func<string, object>) (udField => ((PXFieldState) ((PXSelectBase) this.Document).Cache.GetValueExt((object) sourceOrder, udField))?.Value));
    SOOrderType soOrderType = PXResultset<SOOrderType>.op_Implicit(((PXSelectBase<SOOrderType>) this.soordertype).SelectWindowed(0, 1, new object[1]
    {
      (object) sourceOrder.OrderType
    }));
    ((PXGraph) this).Clear((PXClearOption) 1);
    foreach (PXResult<SOOrder, PX.Objects.CM.CurrencyInfo> pxResult in PXSelectBase<SOOrder, PXSelectJoin<SOOrder, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<SOOrder.curyInfoID>>>, Where<SOOrder.orderType, Equal<Required<SOOrder.orderType>>, And<SOOrder.orderNbr, Equal<Required<SOOrder.orderNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) sourceOrder.OrderType,
      (object) sourceOrder.OrderNbr
    }))
    {
      SOOrder order = PXResult<SOOrder, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
      PX.Objects.CM.CurrencyInfo currencyInfo1 = PXResult<SOOrder, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
      if (order.Behavior == "QT")
      {
        order.MarkCompleted();
        GraphHelper.MarkUpdated(((PXSelectBase) this.Document).Cache, (object) order, true);
      }
      PX.Objects.CM.CurrencyInfo copy1 = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(currencyInfo1);
      copy1.CuryInfoID = new long?();
      copy1.IsReadOnly = new bool?(false);
      PX.Objects.CM.CurrencyInfo currencyInfo2 = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Insert(copy1);
      PX.Objects.CM.CurrencyInfo copy2 = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(currencyInfo2);
      SOOrder soOrder1 = new SOOrder()
      {
        CuryInfoID = currencyInfo2.CuryInfoID,
        OrderType = orderType,
        OrderNbr = orderNbr,
        GroundCollect = order.GroundCollect
      };
      if (PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
        soOrder1.BranchID = order.BranchID;
      SOOrder soOrder2 = ((PXSelectBase<SOOrder>) this.Document).Insert(soOrder1);
      SOOrder soOrder3 = PXResultset<SOOrder>.op_Implicit(((PXSelectBase<SOOrder>) this.Document).Search<SOOrder.orderNbr>((object) soOrder2.OrderNbr, new object[1]
      {
        (object) soOrder2.OrderType
      }));
      TaxBaseAttribute.SetTaxCalc<SOOrder.freightTaxCategoryID>(((PXSelectBase) this.Document).Cache, (object) null, TaxCalc.ManualCalc);
      SOOrder soOrder4 = PXCache<SOOrder>.CreateCopy(order);
      soOrder4.OwnerID = soOrder3.OwnerID;
      soOrder4.WorkgroupID = new int?();
      soOrder4.OrderType = soOrder3.OrderType;
      soOrder4.OrderNbr = soOrder3.OrderNbr;
      soOrder4.Behavior = soOrder3.Behavior;
      soOrder4.ARDocType = soOrder3.ARDocType;
      soOrder4.DefaultOperation = soOrder3.DefaultOperation;
      soOrder4.DefaultTranType = soOrder3.DefaultTranType;
      soOrder4.ShipAddressID = soOrder3.ShipAddressID;
      soOrder4.ShipContactID = soOrder3.ShipContactID;
      soOrder4.BillAddressID = soOrder3.BillAddressID;
      soOrder4.BillContactID = soOrder3.BillContactID;
      soOrder4.AllowsRequiredPrepayment = soOrder3.AllowsRequiredPrepayment;
      soOrder4.IsCashSaleOrder = soOrder3.IsCashSaleOrder;
      soOrder4.IsCreditMemoOrder = soOrder3.IsCreditMemoOrder;
      soOrder4.IsDebitMemoOrder = soOrder3.IsDebitMemoOrder;
      soOrder4.IsInvoiceOrder = soOrder3.IsInvoiceOrder;
      soOrder4.IsNoAROrder = soOrder3.IsNoAROrder;
      soOrder4.IsPaymentInfoEnabled = soOrder3.IsPaymentInfoEnabled;
      soOrder4.IsRMAOrder = soOrder3.IsRMAOrder;
      soOrder4.IsMixedOrder = soOrder3.IsMixedOrder;
      soOrder4.IsTransferOrder = soOrder3.IsTransferOrder;
      soOrder4.IsUserInvoiceNumbering = soOrder3.IsUserInvoiceNumbering;
      soOrder4.OrigOrderType = order.OrderType;
      soOrder4.OrigOrderNbr = order.OrderNbr;
      soOrder4.ShipmentCntr = new int?(0);
      soOrder4.OpenShipmentCntr = new int?(0);
      soOrder4.OpenSiteCntr = new int?(0);
      soOrder4.OpenLineCntr = new int?(0);
      soOrder4.ReleasedCntr = new int?(0);
      soOrder4.BilledCntr = new int?(0);
      soOrder4.OrderQty = new Decimal?(0M);
      soOrder4.OrderWeight = new Decimal?(0M);
      soOrder4.OrderVolume = new Decimal?(0M);
      soOrder4.OpenOrderQty = new Decimal?(0M);
      soOrder4.UnbilledOrderQty = new Decimal?(0M);
      soOrder4.CuryInfoID = soOrder3.CuryInfoID;
      soOrder4.PrepaymentReqSatisfied = soOrder3.PrepaymentReqSatisfied;
      soOrder4.Status = soOrder3.Status;
      soOrder4.Hold = soOrder3.Hold;
      soOrder4.Approved = soOrder3.Approved;
      soOrder4.DontApprove = soOrder3.DontApprove;
      soOrder4.Rejected = soOrder3.Rejected;
      soOrder4.CreditHold = soOrder3.CreditHold;
      soOrder4.Completed = soOrder3.Completed;
      soOrder4.Cancelled = soOrder3.Cancelled;
      soOrder4.InclCustOpenOrders = soOrder3.InclCustOpenOrders;
      soOrder4.OrderDate = soOrder3.OrderDate;
      soOrder4.CancelDate = soOrder3.CancelDate;
      soOrder4.CuryGoodsExtPriceTotal = new Decimal?(0M);
      soOrder4.CuryMiscTot = new Decimal?(0M);
      soOrder4.CuryMiscExtPriceTotal = new Decimal?(0M);
      soOrder4.CuryDetailExtPriceTotal = new Decimal?(0M);
      soOrder4.CuryUnbilledMiscTot = new Decimal?(0M);
      soOrder4.CuryLineTotal = new Decimal?(0M);
      soOrder4.CuryOpenLineTotal = new Decimal?(0M);
      soOrder4.CuryUnbilledLineTotal = new Decimal?(0M);
      soOrder4.CuryVatExemptTotal = new Decimal?(0M);
      soOrder4.CuryVatTaxableTotal = new Decimal?(0M);
      soOrder4.CuryTaxTotal = new Decimal?(0M);
      soOrder4.CuryOrderTotal = new Decimal?(0M);
      soOrder4.CuryOpenOrderTotal = new Decimal?(0M);
      soOrder4.CuryOpenTaxTotal = new Decimal?(0M);
      soOrder4.CuryUnbilledOrderTotal = new Decimal?(0M);
      soOrder4.CuryUnbilledTaxTotal = new Decimal?(0M);
      soOrder4.CuryUnbilledDiscTotal = new Decimal?(0M);
      soOrder4.CuryOpenDiscTotal = new Decimal?(0M);
      soOrder4.CuryPaymentTotal = new Decimal?(0M);
      soOrder4.CuryUnreleasedPaymentAmt = new Decimal?(0M);
      soOrder4.CuryCCAuthorizedAmt = new Decimal?(0M);
      soOrder4.CuryPaidAmt = new Decimal?(0M);
      soOrder4.CuryBilledPaymentTotal = new Decimal?(0M);
      soOrder4.CuryPaymentOverall = new Decimal?(0M);
      soOrder4.CurySalesCostTotal = new Decimal?(0M);
      soOrder4.CuryNetSalesTotal = new Decimal?(0M);
      soOrder4.CuryLineDiscTotal = new Decimal?(0M);
      soOrder4.CuryGroupDiscTotal = new Decimal?(0M);
      soOrder4.FreightTaxCategoryID = (string) null;
      soOrder4.CreatedByID = soOrder3.CreatedByID;
      soOrder4.CreatedByScreenID = soOrder3.CreatedByScreenID;
      soOrder4.CreatedDateTime = soOrder3.CreatedDateTime;
      soOrder4.DisableAutomaticDiscountCalculation = order.DisableAutomaticDiscountCalculation;
      soOrder4.ApprovedCredit = new bool?(false);
      soOrder4.ApprovedCreditByPayment = new bool?(false);
      soOrder4.ApprovedCreditAmt = new Decimal?(0M);
      soOrder4.PackageWeight = new Decimal?(0M);
      soOrder4.Emailed = new bool?(false);
      soOrder4.Printed = new bool?(false);
      soOrder4.BlanketLineCntr = new int?(0);
      soOrder4.ChildLineCntr = new int?(0);
      soOrder4.QtyOnOrders = new Decimal?(0M);
      soOrder4.BlanketOpenQty = new Decimal?(0M);
      soOrder4.CuryTransferredToChildrenPaymentTotal = new Decimal?(0M);
      soOrder4.TransferredToChildrenPaymentTotal = new Decimal?(0M);
      DateTime? requestDate = soOrder4.RequestDate;
      DateTime? orderDate1 = soOrder3.OrderDate;
      if ((requestDate.HasValue & orderDate1.HasValue ? (requestDate.GetValueOrDefault() < orderDate1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        soOrder4.RequestDate = soOrder3.RequestDate;
      DateTime? shipDate = soOrder4.ShipDate;
      DateTime? orderDate2 = soOrder3.OrderDate;
      if ((shipDate.HasValue & orderDate2.HasValue ? (shipDate.GetValueOrDefault() < orderDate2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        soOrder4.ShipDate = soOrder3.ShipDate;
      if (order.Behavior == "QT")
      {
        soOrder4.BillSeparately = soOrder3.BillSeparately;
        soOrder4.ShipSeparately = soOrder3.ShipSeparately;
      }
      if (order.Behavior != "QT")
        ((PXSelectBase) this.Document).Cache.SetDefaultExt<SOOrder.disableAutomaticTaxCalculation>((object) soOrder4);
      flag5 = soOrder4.DisableAutomaticTaxCalculation.GetValueOrDefault();
      soOrder4.IsOrchestrationAllowed = new bool?();
      ((PXSelectBase) this.Document).Cache.SetDefaultExt<SOOrder.isOrchestrationAllowed>((object) soOrder4);
      ((PXSelectBase) this.Document).Cache.SetDefaultExt<SOOrder.orchestrationStatus>((object) soOrder4);
      if (!soOrder4.IsOrchestrationAllowed.GetValueOrDefault() || !order.IsOrchestrationAllowed.GetValueOrDefault())
      {
        soOrder4.OrchestrationStrategy = soOrder3.OrchestrationStrategy;
        soOrder4.LimitWarehouse = soOrder3.LimitWarehouse;
        soOrder4.NumberOfWarehouses = soOrder3.NumberOfWarehouses;
      }
      ((PXSelectBase) this.Document).Cache.SetDefaultExt<SOOrder.invoiceDate>((object) soOrder4);
      ((PXSelectBase) this.Document).Cache.SetDefaultExt<SOOrder.finPeriodID>((object) soOrder4);
      soOrder4.ExtRefNbr = (string) null;
      soOrder4.NoteID = new Guid?();
      ((PXSelectBase) this.Document).Cache.ForceExceptionHandling = true;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PXFieldDefaulting pxFieldDefaulting = SOOrderEntry.\u003C\u003Ec.\u003C\u003E9__142_2 ?? (SOOrderEntry.\u003C\u003Ec.\u003C\u003E9__142_2 = new PXFieldDefaulting((object) SOOrderEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCopyOrderProc\u003Eb__142_2)));
      ((PXGraph) this).FieldDefaulting.AddHandler<SOOrder.shipTermsID>(pxFieldDefaulting);
      try
      {
        soOrder4 = ((PXSelectBase<SOOrder>) this.Document).Update(soOrder4);
      }
      finally
      {
        ((PXGraph) this).FieldDefaulting.RemoveHandler<SOOrder.shipTermsID>(pxFieldDefaulting);
      }
      PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.Document).Cache, (object) order, ((PXSelectBase) this.Document).Cache, (object) soOrder4, (PXNoteAttribute.IPXCopySettings) soOrderType);
      if (EnumerableExtensions.IsIn<string>(order.Behavior, "QT", soOrder4.Behavior))
      {
        foreach (KeyValuePair<string, object> keyValuePair in dictionary)
        {
          string str1;
          object obj1;
          EnumerableExtensions.Deconstruct<string, object>(keyValuePair, ref str1, ref obj1);
          string str2 = str1;
          object obj2 = obj1;
          ((PXSelectBase) this.Document).Cache.SetValueExt((object) soOrder4, str2, obj2);
        }
      }
      if (currencyInfo2 != null)
      {
        currencyInfo2.CuryID = copy2.CuryID;
        currencyInfo2.CuryEffDate = copy2.CuryEffDate;
        currencyInfo2.CuryRateTypeID = copy2.CuryRateTypeID;
        currencyInfo2.CuryRate = copy2.CuryRate;
        currencyInfo2.RecipRate = copy2.RecipRate;
        currencyInfo2.CuryMultDiv = copy2.CuryMultDiv;
        ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Update(currencyInfo2);
      }
    }
    SharedRecordAttribute.CopyRecord<SOOrder.billAddressID>(((PXSelectBase) this.Document).Cache, (object) ((PXSelectBase<SOOrder>) this.Document).Current, (object) sourceOrder, false);
    SOBillingAddress soBillingAddress = SOBillingAddress.PK.Find((PXGraph) this, sourceOrder.BillAddressID);
    if (soBillingAddress != null && soBillingAddress.IsValidated.GetValueOrDefault() && ((PXSelectBase<SOBillingAddress>) this.Billing_Address).Current != null)
      ((PXSelectBase<SOBillingAddress>) this.Billing_Address).Current.IsValidated = soBillingAddress.IsValidated;
    SharedRecordAttribute.CopyRecord<SOOrder.billContactID>(((PXSelectBase) this.Document).Cache, (object) ((PXSelectBase<SOOrder>) this.Document).Current, (object) sourceOrder, false);
    SharedRecordAttribute.CopyRecord<SOOrder.shipAddressID>(((PXSelectBase) this.Document).Cache, (object) ((PXSelectBase<SOOrder>) this.Document).Current, (object) sourceOrder, false);
    SOShippingAddress soShippingAddress = SOShippingAddress.PK.Find((PXGraph) this, sourceOrder.ShipAddressID);
    if (soShippingAddress != null && soShippingAddress.IsValidated.GetValueOrDefault() && ((PXSelectBase<SOShippingAddress>) this.Shipping_Address).Current != null)
      ((PXSelectBase<SOShippingAddress>) this.Shipping_Address).Current.IsValidated = soShippingAddress.IsValidated;
    SharedRecordAttribute.CopyRecord<SOOrder.shipContactID>(((PXSelectBase) this.Document).Cache, (object) ((PXSelectBase<SOOrder>) this.Document).Current, (object) sourceOrder, false);
    this.OrderCreated(((PXSelectBase<SOOrder>) this.Document).Current, sourceOrder);
    bool flag6 = false;
    TaxBaseAttribute.SetTaxCalc<SOLine.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
    this.ReloadCustomerCreditRule();
    string[] source1 = new string[4]
    {
      "QT",
      "CM",
      "IN",
      "MO"
    };
    string[] source2 = new string[3]{ "CM", "IN", "MO" };
    foreach (PXResult<SOLine> pxResult in PXSelectBase<SOLine, PXSelect<SOLine, Where<SOLine.orderType, Equal<Required<SOLine.orderType>>, And<SOLine.orderNbr, Equal<Required<SOLine.orderNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) sourceOrder.OrderType,
      (object) sourceOrder.OrderNbr
    }))
    {
      SOLine soLine1 = PXResult<SOLine>.op_Implicit(pxResult);
      SOLine soLine2 = PXCache<SOLine>.CreateCopy(soLine1);
      if (sourceOrder.Behavior == "QT")
      {
        soLine1.Completed = new bool?(true);
        GraphHelper.MarkUpdated(((PXSelectBase) this.Transactions).Cache, (object) soLine1);
      }
      soLine2.OrigOrderType = soLine2.OrderType;
      soLine2.OrigOrderNbr = soLine2.OrderNbr;
      soLine2.OrigLineNbr = soLine2.LineNbr;
      soLine2.Behavior = (string) null;
      soLine2.OrderType = (string) null;
      soLine2.OrderNbr = (string) null;
      soLine2.InvtMult = new short?();
      soLine2.CuryInfoID = new long?();
      soLine2.PlanType = (string) null;
      soLine2.TranType = (string) null;
      SOLine soLine3 = soLine2;
      bool? nullable1 = new bool?();
      bool? nullable2 = nullable1;
      soLine3.RequireShipping = nullable2;
      SOLine soLine4 = soLine2;
      nullable1 = new bool?();
      bool? nullable3 = nullable1;
      soLine4.RequireAllocation = nullable3;
      SOLine soLine5 = soLine2;
      nullable1 = new bool?();
      bool? nullable4 = nullable1;
      soLine5.RequireLocation = nullable4;
      SOLine soLine6 = soLine2;
      nullable1 = new bool?();
      bool? nullable5 = nullable1;
      soLine6.RequireReasonCode = nullable5;
      SOLine soLine7 = soLine2;
      nullable1 = new bool?();
      bool? nullable6 = nullable1;
      soLine7.OpenLine = nullable6;
      soLine2.Completed = new bool?(false);
      soLine2.Cancelled = new bool?(false);
      SOLine soLine8 = soLine2;
      DateTime? nullable7 = new DateTime?();
      DateTime? nullable8 = nullable7;
      soLine8.CancelDate = nullable8;
      soLine2.IsLegacyDropShip = new bool?(false);
      SOLine soLine9 = soLine2;
      nullable7 = new DateTime?();
      DateTime? nullable9 = nullable7;
      soLine9.OrderDate = nullable9;
      SOLine soLine10 = soLine2;
      nullable1 = new bool?();
      bool? nullable10 = nullable1;
      soLine10.POCreated = nullable10;
      soLine2.LineType = (string) null;
      SOLine soLine11 = soLine2;
      nullable1 = new bool?();
      bool? nullable11 = nullable1;
      soLine11.IsStockItem = nullable11;
      soLine2.AutomaticDiscountsDisabled = new bool?(((PXSelectBase<SOOrder>) this.Document).Current.Behavior == "BL");
      soLine2.BlanketType = (string) null;
      soLine2.BlanketNbr = (string) null;
      soLine2.BlanketLineNbr = new int?();
      soLine2.BlanketSplitLineNbr = new int?();
      soLine2.QtyOnOrders = new Decimal?(0M);
      soLine2.BaseQtyOnOrders = new Decimal?(0M);
      soLine2.ChildLineCntr = new int?(0);
      soLine2.OpenChildLineCntr = new int?(0);
      nullable7 = soLine2.RequestDate;
      DateTime? nullable12 = ((PXSelectBase<SOOrder>) this.Document).Current.OrderDate;
      if ((nullable7.HasValue & nullable12.HasValue ? (nullable7.GetValueOrDefault() < nullable12.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        SOLine soLine12 = soLine2;
        nullable12 = new DateTime?();
        DateTime? nullable13 = nullable12;
        soLine12.RequestDate = nullable13;
      }
      nullable12 = soLine2.ShipDate;
      nullable7 = ((PXSelectBase<SOOrder>) this.Document).Current.OrderDate;
      if ((nullable12.HasValue & nullable7.HasValue ? (nullable12.GetValueOrDefault() < nullable7.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        SOLine soLine13 = soLine2;
        nullable7 = new DateTime?();
        DateTime? nullable14 = nullable7;
        soLine13.ShipDate = nullable14;
      }
      if (((IEnumerable<string>) source1).Contains<string>(sourceOrder.Behavior) || ((IEnumerable<string>) source1).Contains<string>(((PXSelectBase<SOOrder>) this.Document).Current.Behavior))
      {
        SOLine soLine14 = soLine2;
        nullable1 = new bool?();
        bool? nullable15 = nullable1;
        soLine14.POCreate = nullable15;
        soLine2.POSource = (string) null;
      }
      nullable1 = ((PXSelectBase<SOOrderType>) this.soordertype).Current.RequireLocation;
      if (nullable1.GetValueOrDefault() && soLine2.ShipComplete == "B")
        soLine2.ShipComplete = (string) null;
      nullable1 = ((PXSelectBase<SOOrderType>) this.soordertype).Current.RequireLocation;
      bool flag7 = false;
      if (nullable1.GetValueOrDefault() == flag7 & nullable1.HasValue)
      {
        soLine2.LocationID = new int?();
        soLine2.LotSerialNbr = (string) null;
        SOLine soLine15 = soLine2;
        nullable7 = new DateTime?();
        DateTime? nullable16 = nullable7;
        soLine15.ExpireDate = nullable16;
      }
      soLine2.InvoiceType = (string) null;
      soLine2.InvoiceNbr = (string) null;
      soLine2.InvoiceLineNbr = new int?();
      soLine2.InvoiceUOM = (string) null;
      SOLine soLine16 = soLine2;
      nullable7 = new DateTime?();
      DateTime? nullable17 = nullable7;
      soLine16.InvoiceDate = nullable17;
      soLine2.DefaultOperation = (string) null;
      soLine2.Operation = (string) null;
      soLine2.LineSign = new short?();
      nullable1 = soLine2.IsFree;
      int num1;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = soLine2.ManualDisc;
        bool flag8 = false;
        num1 = nullable1.GetValueOrDefault() == flag8 & nullable1.HasValue ? 1 : 0;
      }
      else
        num1 = 0;
      int num2 = flag3 ? 1 : 0;
      if ((num1 & num2) == 0)
      {
        if (flag4)
          soLine2.ManualDisc = new bool?(false);
        if (flag1)
        {
          nullable1 = soLine2.ManualPrice;
          if (!nullable1.GetValueOrDefault())
          {
            soLine2.CuryUnitPrice = new Decimal?();
            soLine2.CuryExtPrice = new Decimal?();
          }
        }
        if (flag2)
          soLine2.ManualPrice = new bool?(false);
        if (!flag3)
        {
          soLine2.ManualDisc = new bool?(true);
          soLine2.SkipDisc = new bool?(true);
        }
        int? nullable18 = ((PXSelectBase<SOOrderType>) this.soordertype).Current.ActiveOperationsCntr;
        int num3 = 1;
        Decimal? nullable19;
        if (nullable18.GetValueOrDefault() <= num3 & nullable18.HasValue)
        {
          string str = (string) PXFormulaAttribute.Evaluate<SOLine.lineType>(((PXSelectBase) this.Transactions).Cache, (object) soLine2);
          nullable19 = soLine2.OrderQty;
          Decimal num4 = 0M;
          if (nullable19.GetValueOrDefault() < num4 & nullable19.HasValue && EnumerableExtensions.IsIn<string>(str, "GI", "GN"))
          {
            SOLine soLine17 = soLine2;
            nullable19 = soLine2.OrderQty;
            Decimal? nullable20 = nullable19.HasValue ? new Decimal?(-nullable19.GetValueOrDefault()) : new Decimal?();
            soLine17.OrderQty = nullable20;
            SOLine soLine18 = soLine2;
            nullable19 = soLine2.CuryExtPrice;
            Decimal? nullable21 = nullable19.HasValue ? new Decimal?(-nullable19.GetValueOrDefault()) : new Decimal?();
            soLine18.CuryExtPrice = nullable21;
            SOLine soLine19 = soLine2;
            nullable19 = soLine2.CuryDiscAmt;
            Decimal? nullable22 = nullable19.HasValue ? new Decimal?(-nullable19.GetValueOrDefault()) : new Decimal?();
            soLine19.CuryDiscAmt = nullable22;
          }
          soLine2.AutoCreateIssueLine = new bool?(false);
        }
        soLine2.UnassignedQty = new Decimal?(0M);
        SOLine soLine20 = soLine2;
        nullable19 = new Decimal?();
        Decimal? nullable23 = nullable19;
        soLine20.OpenQty = nullable23;
        soLine2.ClosedQty = new Decimal?(0M);
        soLine2.BilledQty = new Decimal?(0M);
        SOLine soLine21 = soLine2;
        nullable19 = new Decimal?();
        Decimal? nullable24 = nullable19;
        soLine21.UnbilledQty = nullable24;
        soLine2.ShippedQty = new Decimal?(0M);
        soLine2.CuryBilledAmt = new Decimal?(0M);
        SOLine soLine22 = soLine2;
        nullable19 = new Decimal?();
        Decimal? nullable25 = nullable19;
        soLine22.CuryUnbilledAmt = nullable25;
        SOLine soLine23 = soLine2;
        nullable19 = new Decimal?();
        Decimal? nullable26 = nullable19;
        soLine23.CuryOpenAmt = nullable26;
        SOLine soLine24 = soLine2;
        nullable19 = new Decimal?();
        Decimal? nullable27 = nullable19;
        soLine24.CuryLineAmt = nullable27;
        SOLine soLine25 = soLine2;
        nullable19 = new Decimal?();
        Decimal? nullable28 = nullable19;
        soLine25.CuryNetSales = nullable28;
        SOLine soLine26 = soLine2;
        nullable19 = new Decimal?();
        Decimal? nullable29 = nullable19;
        soLine26.CuryMarginAmt = nullable29;
        SOLine soLine27 = soLine2;
        nullable19 = new Decimal?();
        Decimal? nullable30 = nullable19;
        soLine27.MarginPct = nullable30;
        soLine2.IsOrchestratedLine = new bool?(false);
        SOLine soLine28 = soLine2;
        nullable18 = new int?();
        int? nullable31 = nullable18;
        soLine28.OrchestrationOriginalLineNbr = nullable31;
        soLine2.OrchestrationPlanID = (string) null;
        SOLine soLine29 = soLine2;
        nullable18 = new int?();
        int? nullable32 = nullable18;
        soLine29.OrchestrationOriginalSiteID = nullable32;
        if (flag3)
        {
          soLine2.DocumentDiscountRate = new Decimal?((Decimal) 1);
          soLine2.GroupDiscountRate = new Decimal?((Decimal) 1);
          soLine2.DiscountsAppliedToLine = new ushort[0];
        }
        soLine2.NoteID = new Guid?();
        if (EnumerableExtensions.IsNotIn<string>(((PXSelectBase<SOOrder>) this.Document).Current.Behavior, "SO", "RM", "QT") || soLine2.Operation == "R")
          soLine2.IsSpecialOrder = new bool?(false);
        SOLine soLine30 = soLine2;
        nullable18 = new int?();
        int? nullable33 = nullable18;
        soLine30.CostCenterID = nullable33;
        soLine2.IsBeingCopied = new bool?(true);
        try
        {
          PXGraph.FieldUpdatedEvents fieldUpdated = ((PXGraph) this).FieldUpdated;
          SOOrderEntry soOrderEntry = this;
          // ISSUE: virtual method pointer
          PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) soOrderEntry, __vmethodptr(soOrderEntry, SOLine_DiscountID_FieldUpdated));
          fieldUpdated.RemoveHandler<SOLine.discountID>(pxFieldUpdated);
          try
          {
            try
            {
              ((PXSelectBase) this.Transactions).Cache.ForceExceptionHandling = true;
              soLine2 = ((PXSelectBase<SOLine>) this.Transactions).Insert(soLine2);
            }
            catch (AlternatieIDNotUniqueException ex)
            {
              soLine2.AlternateID = (string) null;
              soLine2 = ((PXSelectBase<SOLine>) this.Transactions).Insert(soLine2);
              if (soLine2 != null)
                ((PXSelectBase) this.Transactions).Cache.RaiseExceptionHandling<SOLine.alternateID>((object) soLine2, (object) null, (Exception) ex);
            }
            if (soLine2 != null)
            {
              PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.Transactions).Cache, (object) soLine1, ((PXSelectBase) this.Transactions).Cache, (object) soLine2, (PXNoteAttribute.IPXCopySettings) soOrderType);
              if ((((IEnumerable<string>) source2).Contains<string>(sourceOrder.Behavior) || ((IEnumerable<string>) source1).Contains<string>(((PXSelectBase<SOOrder>) this.Document).Current.Behavior) ? 1 : (soLine2.Operation == "R" ? 1 : 0)) != 0)
              {
                soLine2.POCreate = new bool?(false);
                soLine2.POSource = (string) null;
              }
              if (soLine2.Operation == "I")
                soLine2.AutoCreateIssueLine = new bool?(false);
              if (!soLine2.IsSpecialOrder.GetValueOrDefault())
                ((PXSelectBase) this.Transactions).Cache.SetDefaultExt<SOLine.unitCost>((object) soLine2);
              ((PXSelectBase<SOLine>) this.Transactions).Update(soLine2);
            }
          }
          catch (PXSetPropertyException ex)
          {
            flag6 = true;
          }
        }
        finally
        {
          if (soLine2 != null)
            soLine2.IsBeingCopied = new bool?(false);
          PXGraph.FieldUpdatedEvents fieldUpdated = ((PXGraph) this).FieldUpdated;
          SOOrderEntry soOrderEntry = this;
          // ISSUE: virtual method pointer
          PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) soOrderEntry, __vmethodptr(soOrderEntry, SOLine_DiscountID_FieldUpdated));
          fieldUpdated.AddHandler<SOLine.discountID>(pxFieldUpdated);
        }
      }
    }
    bool flag9 = flag6 | flag3 | flag1 | flag4 | flag2 && !flag5;
    if (flag9)
      TaxBaseAttribute.SetTaxCalc<SOLine.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
    foreach (PXResult<SOTaxTran> pxResult in PXSelectBase<SOTaxTran, PXSelect<SOTaxTran, Where<SOTaxTran.orderType, Equal<Required<SOTaxTran.orderType>>, And<SOTaxTran.orderNbr, Equal<Required<SOTaxTran.orderNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) sourceOrder.OrderType,
      (object) sourceOrder.OrderNbr
    }))
    {
      SOTaxTran soTaxTran1 = PXResult<SOTaxTran>.op_Implicit(pxResult);
      SOTaxTran soTaxTran2 = new SOTaxTran();
      soTaxTran2.OrderType = ((PXSelectBase<SOOrder>) this.Document).Current.OrderType;
      soTaxTran2.OrderNbr = ((PXSelectBase<SOOrder>) this.Document).Current.OrderNbr;
      soTaxTran2.LineNbr = new int?(int.MaxValue);
      soTaxTran2.TaxID = soTaxTran1.TaxID;
      SOTaxTran soTaxTran3 = ((PXSelectBase<SOTaxTran>) this.Taxes).Insert(soTaxTran2);
      if (!flag9 && soTaxTran3 != null)
      {
        SOTaxTran copy = PXCache<SOTaxTran>.CreateCopy(soTaxTran3);
        copy.TaxRate = soTaxTran1.TaxRate;
        copy.CuryTaxableAmt = soTaxTran1.CuryTaxableAmt;
        copy.CuryExemptedAmt = soTaxTran1.CuryExemptedAmt;
        copy.CuryTaxAmt = soTaxTran1.CuryTaxAmt;
        copy.CuryUnshippedTaxableAmt = soTaxTran1.CuryTaxableAmt;
        copy.CuryUnshippedTaxAmt = soTaxTran1.CuryTaxAmt;
        copy.CuryUnbilledTaxableAmt = soTaxTran1.CuryTaxableAmt;
        copy.CuryUnbilledTaxAmt = soTaxTran1.CuryTaxAmt;
        ((PXSelectBase<SOTaxTran>) this.Taxes).Update(copy);
      }
    }
    if (sourceOrder.FreightTaxCategoryID != null)
    {
      TaxBaseAttribute.SetTaxCalc<SOOrder.freightTaxCategoryID>(((PXSelectBase) this.Document).Cache, (object) null, TaxCalc.ManualLineCalc);
      ((PXSelectBase<SOOrder>) this.Document).Current.FreightTaxCategoryID = sourceOrder.FreightTaxCategoryID;
      ((PXSelectBase<SOOrder>) this.Document).Update(((PXSelectBase<SOOrder>) this.Document).Current);
    }
    if (flag9)
      TaxBaseAttribute.SetTaxCalc<SOLine.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.ManualLineCalc);
    if (!this.DisableGroupDocDiscount)
    {
      foreach (PXResult<SOOrderDiscountDetail> pxResult in PXSelectBase<SOOrderDiscountDetail, PXSelect<SOOrderDiscountDetail, Where<SOOrderDiscountDetail.orderType, Equal<Required<SOOrderDiscountDetail.orderType>>, And<SOOrderDiscountDetail.orderNbr, Equal<Required<SOOrderDiscountDetail.orderNbr>>, And<SOOrderDiscountDetail.freeItemID, IsNull>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) sourceOrder.OrderType,
        (object) sourceOrder.OrderNbr
      }))
      {
        SOOrderDiscountDetail orderDiscountDetail = PXResult<SOOrderDiscountDetail>.op_Implicit(pxResult);
        if (!flag3 || orderDiscountDetail.IsManual.GetValueOrDefault())
        {
          SOOrderDiscountDetail copy = PXCache<SOOrderDiscountDetail>.CreateCopy(orderDiscountDetail);
          copy.OrderType = ((PXSelectBase<SOOrder>) this.Document).Current.OrderType;
          copy.OrderNbr = ((PXSelectBase<SOOrder>) this.Document).Current.OrderNbr;
          copy.IsManual = new bool?(true);
          this._discountEngine.InsertDiscountDetail(((PXSelectBase) this.DiscountDetails).Cache, (PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails, copy);
        }
      }
    }
    RecalcDiscountsParamFilter current = ((PXSelectBase<RecalcDiscountsParamFilter>) this.recalcdiscountsfilter).Current;
    current.OverrideManualDiscounts = new bool?(flag4);
    current.OverrideManualDocGroupDiscounts = new bool?(flag4);
    current.OverrideManualPrices = new bool?(flag2);
    current.RecalcDiscounts = new bool?(flag3);
    current.RecalcUnitPrices = new bool?(flag1);
    current.RecalcTarget = "ALL";
    this._discountEngine.RecalculatePricesAndDiscounts(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<SOLine>) this.Transactions, ((PXSelectBase<SOLine>) this.Transactions).Current, (PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails, ((PXSelectBase<SOOrder>) this.Document).Current.CustomerLocationID, ((PXSelectBase<SOOrder>) this.Document).Current.OrderDate, current, DiscountEngine.DiscountCalculationOptions.DisableAPDiscountsCalculation);
    this.RecalculateTotalDiscount();
    this.RefreshFreeItemLines(((PXSelectBase) this.Transactions).Cache);
  }

  public virtual void ReloadCustomerCreditRule()
  {
    if (((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current == null || ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.CreditRule != null)
      return;
    PX.Objects.AR.Customer customer = PX.Objects.AR.Customer.PK.Find((PXGraph) this, (int?) ((PXSelectBase<SOOrder>) this.Document).Current?.CustomerID);
    if (customer == null)
      return;
    ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.CreditRule = customer.CreditRule;
  }

  protected virtual void OrderCreated(SOOrder document, SOOrder source)
  {
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ItemAvailability(PXAdapter adapter)
  {
    PXCache cache = ((PXSelectBase) this.Transactions).Cache;
    SOLine current = ((PXSelectBase<SOLine>) this.Transactions).Current;
    if (current == null)
      return adapter.Get();
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, current.InventoryID);
    if (inventoryItem != null && inventoryItem.StkItem.GetValueOrDefault())
    {
      INSubItem inSubItem = (INSubItem) PXSelectorAttribute.Select<SOLine.subItemID>(cache, (object) current);
      InventoryAllocDetEnq.Redirect(inventoryItem.InventoryID, inSubItem?.SubItemCD, current.LotSerialNbr, current.SiteID, current.LocationID, (PXBaseRedirectException.WindowMode) 2);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable CalculateFreight(PXAdapter adapter)
  {
    if (((PXSelectBase<SOOrder>) this.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<SOOrder>) this.Document).Current.IsManualPackage;
      if (!nullable.GetValueOrDefault())
      {
        nullable = ((PXSelectBase<SOOrder>) this.Document).Current.IsPackageValid;
        if (!nullable.GetValueOrDefault())
          this.CarrierRatesExt.RecalculatePackagesForOrder(((PXSelectBase<SOOrder>) this.Document).Current);
      }
    }
    this.CalculateFreightCost(false);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
  {
    SOOrderEntry soOrderEntry = this;
    foreach (SOOrder soOrder in adapter.Get<SOOrder>())
    {
      if (soOrder != null)
        ((PXGraph) soOrderEntry).FindAllImplementations<IAddressValidationHelper>().ValidateAddresses();
      yield return (object) soOrder;
    }
  }

  [PXUIField]
  [PXButton(CommitChanges = true, DisplayOnMainToolbar = false)]
  public virtual IEnumerable RecalculateDiscountsAction(PXAdapter adapter)
  {
    if (adapter.MassProcess)
    {
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CRecalculateDiscountsAction\u003Eb__153_0)));
    }
    else if ((((PXGraph) this).IsImport || ((PXGraph) this).IsContractBasedAPI) && ((PXSelectBase<SOOrder>) this.Document).Current != null)
      this.RecalculatePricesAndDiscountsProc();
    else if (!adapter.ExternalCall)
      this.RecalculateDiscountsProc(true);
    else if (((PXSelectBase<RecalcDiscountsParamFilter>) this.recalcdiscountsfilter).AskExt() == 1)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) new SOOrderEntry.\u003C\u003Ec__DisplayClass153_0()
      {
        clone = GraphHelper.Clone<SOOrderEntry>(this)
      }, __methodptr(\u003CRecalculateDiscountsAction\u003Eb__1)));
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Recalculate Discounts on Import", Visible = true)]
  [PXButton(DisplayOnMainToolbar = false)]
  [Obsolete("Use RecalculateDiscountsAction instead. Fill in RecalcDiscountsParamFilter to modify default price and discount recalculation behavior when calling RecalculateDiscountsAction() from import scenarios or when using CB-API")]
  public void recalculateDiscountsFromImport()
  {
    if (((PXSelectBase<SOOrder>) this.Document).Current == null)
      return;
    try
    {
      ((PXSelectBase<SOOrder>) this.Document).Current.DeferPriceDiscountRecalculation = new bool?(false);
      this._discountEngine.AutoRecalculatePricesAndDiscounts(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<SOLine>) this.Transactions, (SOLine) null, (PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails, ((PXSelectBase<SOOrder>) this.Document).Current.CustomerLocationID, ((PXSelectBase<SOOrder>) this.Document).Current.OrderDate, this.GetDefaultSODiscountCalculationOptions(((PXSelectBase<SOOrder>) this.Document).Current, true) | DiscountEngine.DiscountCalculationOptions.DisablePriceCalculation | DiscountEngine.DiscountCalculationOptions.CalculateDiscountsFromImport);
    }
    finally
    {
      ((PXSelectBase<SOOrder>) this.Document).Current.DeferPriceDiscountRecalculation = ((PXSelectBase<SOOrderType>) this.soordertype).Current.DeferPriceDiscountRecalculation;
    }
    ((PXAction) this.Save).Press();
  }

  [PXUIField(DisplayName = "Recalculate Prices and Discounts on Import", Visible = true)]
  [PXButton(DisplayOnMainToolbar = false)]
  [Obsolete("Use RecalculateDiscountsAction instead. Fill in RecalcDiscountsParamFilter to modify default price and discount recalculation behavior when calling RecalculateDiscountsAction() from import scenarios or when using CB-API")]
  public void recalculatePricesAndDiscountsFromImport()
  {
    if (((PXSelectBase<SOOrder>) this.Document).Current == null)
      return;
    try
    {
      ((PXSelectBase<SOOrder>) this.Document).Current.DeferPriceDiscountRecalculation = new bool?(false);
      this._discountEngine.AutoRecalculatePricesAndDiscounts(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<SOLine>) this.Transactions, (SOLine) null, (PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails, ((PXSelectBase<SOOrder>) this.Document).Current.CustomerLocationID, ((PXSelectBase<SOOrder>) this.Document).Current.OrderDate, this.GetDefaultSODiscountCalculationOptions(((PXSelectBase<SOOrder>) this.Document).Current, true) | DiscountEngine.DiscountCalculationOptions.CalculateDiscountsFromImport);
    }
    finally
    {
      ((PXSelectBase<SOOrder>) this.Document).Current.DeferPriceDiscountRecalculation = ((PXSelectBase<SOOrderType>) this.soordertype).Current.DeferPriceDiscountRecalculation;
    }
    ((PXAction) this.Save).Press();
  }

  protected virtual void RecalculateDiscountsProc(bool redirect)
  {
    this._discountEngine.RecalculatePricesAndDiscounts(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<SOLine>) this.Transactions, ((PXSelectBase<SOLine>) this.Transactions).Current, (PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails, ((PXSelectBase<SOOrder>) this.Document).Current.CustomerLocationID, ((PXSelectBase<SOOrder>) this.Document).Current.OrderDate, ((PXSelectBase<RecalcDiscountsParamFilter>) this.recalcdiscountsfilter).Current, DiscountEngine.DiscountCalculationOptions.DisableAPDiscountsCalculation);
    if (redirect)
      PXLongOperation.SetCustomInfo((object) this);
    else
      ((PXAction) this.Save).Press();
  }

  protected virtual void RecalculatePricesAndDiscountsProc()
  {
    try
    {
      ((PXSelectBase<SOOrder>) this.Document).Current.DeferPriceDiscountRecalculation = new bool?(false);
      this._discountEngine.RecalculatePricesAndDiscounts(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<SOLine>) this.Transactions, ((PXSelectBase<SOLine>) this.Transactions).Current, (PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails, ((PXSelectBase<SOOrder>) this.Document).Current.CustomerLocationID, ((PXSelectBase<SOOrder>) this.Document).Current.OrderDate, ((PXSelectBase<RecalcDiscountsParamFilter>) this.recalcdiscountsfilter).Current, this.GetDefaultSODiscountCalculationOptions(((PXSelectBase<SOOrder>) this.Document).Current, true) | DiscountEngine.DiscountCalculationOptions.CalculateDiscountsFromImport);
    }
    finally
    {
      ((PXSelectBase<SOOrder>) this.Document).Current.DeferPriceDiscountRecalculation = ((PXSelectBase<SOOrderType>) this.soordertype).Current.DeferPriceDiscountRecalculation;
    }
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable RecalcOk(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewBlanketOrder(PXAdapter adapter)
  {
    PXRedirectHelper.TryRedirect(((PXSelectBase) this.Document).Cache, (object) SOOrder.PK.Find((PXGraph) this, ((PXSelectBase<SOLine>) this.Transactions).Current?.BlanketType, ((PXSelectBase<SOLine>) this.Transactions).Current?.BlanketNbr), "View Parent Order", (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewOrigOrder(PXAdapter adapter)
  {
    PXRedirectHelper.TryRedirect(((PXSelectBase) this.Document).Cache, (object) SOOrder.PK.Find((PXGraph) this, ((PXSelectBase<SOLine>) this.Transactions).Current?.OrigOrderType, ((PXSelectBase<SOLine>) this.Transactions).Current?.OrigOrderNbr), "View Original Order", (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  protected virtual void CurrencyInfo_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    if (e.Row != null && this.IsCopyOrder)
    {
      e.NewValue = (object) (((PX.Objects.CM.CurrencyInfo) e.Row).CuryID ?? ((PXSelectBase<PX.Objects.AR.Customer>) this.customer)?.Current?.CuryID);
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      if (((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current == null || string.IsNullOrEmpty(((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.CuryID))
        return;
      e.NewValue = (object) ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.CuryID;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void CurrencyInfo_CuryRateTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    if (e.Row != null && this.IsCopyOrder)
    {
      e.NewValue = (object) ((PX.Objects.CM.CurrencyInfo) e.Row).CuryRateTypeID;
      ((CancelEventArgs) e).Cancel = true;
    }
    else if (((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current != null && !string.IsNullOrEmpty(((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.CuryRateTypeID))
    {
      e.NewValue = (object) ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.CuryRateTypeID;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      CMSetup cmSetup = PXResultset<CMSetup>.op_Implicit(PXSelectBase<CMSetup, PXSelect<CMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (cmSetup == null)
        return;
      e.NewValue = (object) cmSetup.ARRateTypeDflt;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void CurrencyInfo_CuryEffDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase) this.Document).Cache.Current == null)
      return;
    e.NewValue = (object) ((SOOrder) ((PXSelectBase) this.Document).Cache.Current).OrderDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CM.CurrencyInfo row))
      return;
    bool flag = row.AllowUpdate(((PXSelectBase) this.Transactions).Cache);
    if (((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current != null && !((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.AllowOverrideRate.Value)
      flag = false;
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyRateTypeID>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyEffDate>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.sampleCuryRate>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.sampleRecipRate>(sender, (object) row, flag);
  }

  public virtual void InitCacheMapping(Dictionary<System.Type, System.Type> map)
  {
    ((PXGraph) this).InitCacheMapping(map);
    ((PXGraph) this).Caches.AddCacheMapping(typeof (INSiteStatusByCostCenter), typeof (INSiteStatusByCostCenter));
    ((PXGraph) this).Caches.AddCacheMapping(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter), typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter));
    ((PXGraph) this).Caches.AddCacheMapping(typeof (PX.Objects.AR.CustomerPaymentMethod), typeof (PX.Objects.AR.CustomerPaymentMethod));
  }

  protected virtual void ParentFieldUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.ObjectsEqual<SOOrder.orderDate, SOOrder.curyID>(e.Row, e.OldRow))
      return;
    foreach (PXResult<SOLine> pxResult in ((PXSelectBase<SOLine>) this.Transactions).Select(Array.Empty<object>()))
      GraphHelper.MarkUpdated(((PXSelectBase) this.Transactions).Cache, (object) PXResult<SOLine>.op_Implicit(pxResult), true);
  }

  public SOOrderEntry()
  {
    PXGraph.RowUpdatedEvents rowUpdated = ((PXGraph) this).RowUpdated;
    SOOrderEntry soOrderEntry = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) soOrderEntry, __vmethodptr(soOrderEntry, ParentFieldUpdated));
    rowUpdated.AddHandler<SOOrder>(pxRowUpdated);
    ((PXSelectBase) this.shipmentlist).Cache.AllowUpdate = false;
    PXUIFieldAttribute.SetVisible<SOOrderShipment.operation>(((PXSelectBase) this.shipmentlist).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<SOOrderShipment.orderType>(((PXSelectBase) this.shipmentlist).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<SOOrderShipment.orderNbr>(((PXSelectBase) this.shipmentlist).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<SOOrderShipment.shipmentNbr>(((PXSelectBase) this.shipmentlist).Cache, (object) null, false);
    SOSetup current = ((PXSelectBase<SOSetup>) this.sosetup).Current;
    ARSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) this);
    this.viewInventoryID = ((PXFieldState) ((PXSelectBase) this.Transactions).Cache.GetStateExt<SOLine.inventoryID>((object) null))?.ViewName;
    PXUIFieldAttribute.SetVisible<SOLine.taskID>(((PXSelectBase) this.Transactions).Cache, (object) null, ProjectAttribute.IsPMVisible("SO"));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(SOOrderEntry.\u003C\u003Ec.\u003C\u003E9__190_0 ?? (SOOrderEntry.\u003C\u003Ec.\u003C\u003E9__190_0 = new PXFieldDefaulting((object) SOOrderEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__190_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<PX.Objects.CR.Location.locType>(SOOrderEntry.\u003C\u003Ec.\u003C\u003E9__190_1 ?? (SOOrderEntry.\u003C\u003Ec.\u003C\u003E9__190_1 = new PXFieldDefaulting((object) SOOrderEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__190_1))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<PX.Objects.IN.InventoryItem.stkItem>(SOOrderEntry.\u003C\u003Ec.\u003C\u003E9__190_2 ?? (SOOrderEntry.\u003C\u003Ec.\u003C\u003E9__190_2 = new PXFieldDefaulting((object) SOOrderEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__190_2))));
    if (!PXAccess.FeatureInstalled<FeaturesSet.carrierIntegration>())
      ((PXAction) this.CarrierRatesExt.shopRates).SetCaption(PXMessages.LocalizeNoPrefix(nameof (Packages)));
    ((PXAction) this.CurrencyView).SetCommitChanges(true);
  }

  [InjectDependency]
  protected ILicenseLimitsService _licenseLimits { get; set; }

  void IGraphWithInitialization.Initialize()
  {
    if (this._licenseLimits == null)
      return;
    ((PXGraph) this).OnBeforeCommit += this._licenseLimits.GetCheckerDelegate<SOOrder>(new TableQuery[2]
    {
      new TableQuery((TransactionTypes) 108, typeof (SOLine), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[2]
      {
        (PXDataFieldValue) new PXDataFieldValue<SOLine.orderType>((PXDbType) 3, (object) ((PXSelectBase<SOOrder>) ((SOOrderEntry) graph).Document).Current?.OrderType),
        (PXDataFieldValue) new PXDataFieldValue<SOLine.orderNbr>((object) ((PXSelectBase<SOOrder>) ((SOOrderEntry) graph).Document).Current?.OrderNbr)
      })),
      new TableQuery((TransactionTypes) 115, typeof (SOLineSplit), (Func<PXGraph, PXDataFieldValue[]>) (graph =>
      {
        SOOrder current = ((PXSelectBase<SOOrder>) ((SOOrderEntry) graph).Document).Current;
        bool flag = current != null && EnumerableExtensions.IsIn<string>(current.Behavior, "CM", "IN", "MO");
        return new PXDataFieldValue[2]
        {
          (PXDataFieldValue) new PXDataFieldValue<SOLineSplit.orderType>((PXDbType) 3, flag ? (object) current?.OrderType : (object) (string) null),
          (PXDataFieldValue) new PXDataFieldValue<SOLineSplit.orderNbr>(flag ? (object) current?.OrderNbr : (object) (string) null)
        };
      }))
    });
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDBDefault(typeof (SOOrder.customerID))]
  [PXUIField]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOAdjust.customerID> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  [PXDBDefault(typeof (SOOrder.orderType))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOAdjust.adjdOrderType> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  [PXRemoveBaseAttribute(typeof (PXRestrictorAttribute))]
  [PXDBDefault(typeof (SOOrder.orderNbr))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOAdjust.adjdOrderNbr> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (Switch<Case<Where<Current<SOOrderType.canHaveRefunds>, Equal<True>>, ARDocType.refund>, ARDocType.payment>))]
  [ARPaymentType.SOList]
  [PXUIField(DisplayName = "Doc. Type")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOAdjust.adjgDocType> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDBDefaultAttribute))]
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Reference Nbr.")]
  [ARPaymentType.AdjgRefNbr(typeof (Search<PX.Objects.AR.ARPayment.refNbr, Where<PX.Objects.AR.ARPayment.customerID, In3<Current<SOOrder.customerID>, Current<PX.Objects.AR.Customer.consolidatingBAccountID>>, And<PX.Objects.AR.ARPayment.docType, Equal<Optional<SOAdjust.adjgDocType>>, And<PX.Objects.AR.ARPayment.openDoc, Equal<True>>>>>), Filterable = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOAdjust.adjgRefNbr> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDBDecimalAttribute))]
  [PXDBCurrency(typeof (SOAdjust.adjdCuryInfoID), typeof (SOAdjust.adjAmt))]
  [PXUIField(DisplayName = "Applied To Order")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOAdjust.curyAdjdAmt> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PX.Objects.CM.Extensions.PXDBCurrencyAttribute))]
  [PXRemoveBaseAttribute(typeof (PXUIFieldAttribute))]
  [PXDBDecimal(4)]
  [PXFormula(typeof (Maximum<Sub<SOAdjust.curyOrigAdjgAmt, SOAdjust.curyAdjgBilledAmt>, decimal0>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOAdjust.curyAdjgAmt> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXDBLong]
  [CurrencyInfo(typeof (SOOrder.curyInfoID), ModuleCode = "SO", CuryIDField = "AdjdOrigCuryID")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOAdjust.adjdOrigCuryInfoID> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PX.Objects.CM.Extensions.CurrencyInfo))]
  [CurrencyInfo(ModuleCode = "SO", CuryIDField = "AdjgCuryID")]
  [PXDefault]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOAdjust.adjgCuryInfoID> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXDBLong]
  [CurrencyInfo(typeof (SOOrder.curyInfoID), ModuleCode = "SO", CuryIDField = "AdjdCuryID")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOAdjust.adjdCuryInfoID> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDBDefault(typeof (SOOrder.orderDate))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOAdjust.adjgDocDate> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDBDefault(typeof (SOOrder.orderDate))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOAdjust.adjdOrderDate> eventArgs)
  {
  }

  [PXMergeAttributes]
  [DenormalizedFrom(new System.Type[] {typeof (PX.Objects.AR.ARPayment.isCCPayment), typeof (PX.Objects.AR.ARPayment.released), typeof (PX.Objects.AR.ARPayment.isCCAuthorized), typeof (PX.Objects.AR.ARPayment.isCCCaptured), typeof (PX.Objects.AR.ARPayment.voided), typeof (PX.Objects.AR.ARPayment.hold), typeof (PX.Objects.AR.ARPayment.adjDate), typeof (PX.Objects.AR.ARPayment.paymentMethodID), typeof (PX.Objects.AR.ARPayment.cashAccountID), typeof (PX.Objects.AR.ARPayment.pMInstanceID), typeof (PX.Objects.AR.ARPayment.processingCenterID), typeof (PX.Objects.AR.ARPayment.extRefNbr), typeof (PX.Objects.AR.ARRegister.docDesc), typeof (PX.Objects.AR.ARPayment.curyOrigDocAmt), typeof (PX.Objects.AR.ARPayment.origDocAmt), typeof (PX.Objects.AR.ARPayment.syncLock), typeof (PX.Objects.AR.ARPayment.syncLockReason), typeof (PX.Objects.AR.ARPayment.pendingPayment)}, new System.Type[] {typeof (SOAdjust.isCCPayment), typeof (SOAdjust.paymentReleased), typeof (SOAdjust.isCCAuthorized), typeof (SOAdjust.isCCCaptured), typeof (SOAdjust.voided), typeof (SOAdjust.hold), typeof (SOAdjust.adjgDocDate), typeof (SOAdjust.paymentMethodID), typeof (SOAdjust.cashAccountID), typeof (SOAdjust.pMInstanceID), typeof (SOAdjust.processingCenterID), typeof (SOAdjust.extRefNbr), typeof (SOAdjust.docDesc), typeof (SOAdjust.curyOrigDocAmt), typeof (SOAdjust.origDocAmt), typeof (SOAdjust.syncLock), typeof (SOAdjust.syncLockReason), typeof (SOAdjust.pendingPayment)}, null, typeof (SOAdjust.adjgRefNbr))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOAdjust.isCCPayment> eventArgs)
  {
  }

  [PXMergeAttributes]
  [CashAccount(typeof (SOOrder.branchID), typeof (Search<PX.Objects.CA.CashAccount.cashAccountID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOAdjust.cashAccountID> eventArgs)
  {
  }

  protected virtual void SOAdjust_AdjgRefNbr_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.FillSOAdjustByPayment((SOAdjust) e.Row);
  }

  protected virtual void FillSOAdjustByPayment(SOAdjust adj)
  {
    PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<SOOrder.curyInfoID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    foreach (PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.CurrencyInfo> pxResult in ((PXSelectBase<PX.Objects.AR.ARPayment>) new PXSelectReadonly2<PX.Objects.AR.ARPayment, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<PX.Objects.AR.ARPayment.curyInfoID>>>, Where<PX.Objects.AR.ARPayment.customerID, In3<Current<SOOrder.customerID>, Current<PX.Objects.AR.Customer.consolidatingBAccountID>>, And<PX.Objects.AR.ARPayment.docType, In3<ARDocType.payment, ARDocType.prepayment, ARDocType.creditMemo, ARDocType.refund, ARDocType.prepaymentInvoice>, And<PX.Objects.AR.ARPayment.openDoc, Equal<boolTrue>, And<PX.Objects.AR.ARPayment.docType, Equal<Required<PX.Objects.AR.ARPayment.docType>>, And<PX.Objects.AR.ARPayment.refNbr, Equal<Required<PX.Objects.AR.ARPayment.refNbr>>>>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) adj.AdjgDocType,
      (object) adj.AdjgRefNbr
    }))
    {
      PX.Objects.AR.ARPayment copy = PXCache<PX.Objects.AR.ARPayment>.CreateCopy(PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult));
      PX.Objects.CM.CurrencyInfo paymentCurrencyInfo = PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
      this.InitializeAdjustFields(adj, copy, paymentCurrencyInfo);
    }
  }

  protected virtual void InitializeAdjustFields(
    SOAdjust adjustToFill,
    PX.Objects.AR.ARPayment paymentToBeAdapted,
    PX.Objects.CM.CurrencyInfo paymentCurrencyInfo)
  {
    adjustToFill.CustomerID = ((PXSelectBase<SOOrder>) this.Document).Current.CustomerID;
    adjustToFill.AdjdOrderType = ((PXSelectBase<SOOrder>) this.Document).Current.OrderType;
    adjustToFill.AdjdOrderNbr = ((PXSelectBase<SOOrder>) this.Document).Current.OrderNbr;
    adjustToFill.AdjgDocType = paymentToBeAdapted.DocType;
    adjustToFill.AdjgRefNbr = paymentToBeAdapted.RefNbr;
    this.CalculatePaymentBalance(paymentToBeAdapted, adjustToFill);
    if (((PXSelectBase) this.Adjustments).Cache.Locate((object) adjustToFill) != null)
      return;
    adjustToFill.AdjgCuryInfoID = paymentToBeAdapted.CuryInfoID;
    adjustToFill.AdjdOrigCuryInfoID = ((PXSelectBase<SOOrder>) this.Document).Current.CuryInfoID;
    adjustToFill.AdjdCuryInfoID = ((PXSelectBase<SOOrder>) this.Document).Current.CuryInfoID;
    adjustToFill.CuryDocBal = new Decimal?(this.GetConvertedDocumentBalanceFor(paymentToBeAdapted, paymentCurrencyInfo, ((PXSelectBase) this.Adjustments).Cache));
  }

  protected virtual void SOAdjust_Hold_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) true;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOAdjust_CuryAdjdAmt_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    SOAdjust row = (SOAdjust) e.Row;
    if ((Decimal) e.NewValue < 0M)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
      {
        (object) 0.ToString()
      });
    PX.Objects.CS.Terms terms = PXResultset<PX.Objects.CS.Terms>.op_Implicit(PXSelectBase<PX.Objects.CS.Terms, PXSelect<PX.Objects.CS.Terms, Where<PX.Objects.CS.Terms.termsID, Equal<Current<SOOrder.termsID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (terms != null && terms.InstallmentType != "S" && (Decimal) e.NewValue > 0M)
      throw new PXSetPropertyException("No applications can be created for documents with multiple installment credit terms specified.");
    bool flag = this.IsBalanceRecalculationRequired(row);
    if (flag && !this.TryRecalculateBalanceAndCuryInfo(row, sender))
      return;
    long? nullable1 = row.AdjdCuryInfoID;
    if (nullable1.HasValue)
    {
      nullable1 = row.AdjdOrigCuryInfoID;
      if (nullable1.HasValue)
        goto label_9;
    }
    row.AdjdCuryInfoID = ((PXSelectBase<SOOrder>) this.Document).Current.CuryInfoID;
    row.AdjdOrigCuryInfoID = ((PXSelectBase<SOOrder>) this.Document).Current.CuryInfoID;
label_9:
    Decimal? nullable2 = row.CuryDocBal;
    Decimal num1 = nullable2.Value;
    nullable2 = row.CuryAdjdAmt;
    Decimal num2 = nullable2.Value;
    Decimal num3 = num1 + num2;
    Decimal num4 = this.NewBalanceCalculation(row, (Decimal) e.NewValue);
    if (flag)
    {
      nullable2 = row.CuryDocBal;
      Decimal num5 = 0M;
      if (nullable2.GetValueOrDefault() < num5 & nullable2.HasValue)
        row.CuryDocBal = new Decimal?(0M);
    }
    if (num4 < 0M)
      throw new PXSetPropertyException("The amount must be less than or equal to {0}.", new object[1]
      {
        (object) num3.ToString()
      });
  }

  protected virtual bool IsBalanceRecalculationRequired(SOAdjust adj)
  {
    return !adj.AdjgCuryInfoID.HasValue || !adj.CuryDocBal.HasValue;
  }

  protected virtual bool TryRecalculateBalanceAndCuryInfo(SOAdjust adj, PXCache sender)
  {
    PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.CurrencyInfo> andCurrencyInfoFor = this.GetRelatedPaymentAndCurrencyInfoFor(adj);
    PX.Objects.AR.ARPayment copy = PXCache<PX.Objects.AR.ARPayment>.CreateCopy(PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.CurrencyInfo>.op_Implicit(andCurrencyInfoFor));
    if (copy == null && sender.Graph.IsContractBasedAPI)
      return false;
    this.CalculatePaymentBalance(copy, adj);
    Decimal documentBalanceFor = this.GetConvertedDocumentBalanceFor(copy, PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.CurrencyInfo>.op_Implicit(andCurrencyInfoFor), sender);
    SOAdjust soAdjust = adj;
    Decimal num = documentBalanceFor;
    Decimal? curyAdjdAmt = adj.CuryAdjdAmt;
    Decimal? nullable = curyAdjdAmt.HasValue ? new Decimal?(num - curyAdjdAmt.GetValueOrDefault()) : new Decimal?();
    soAdjust.CuryDocBal = nullable;
    adj.AdjgCuryInfoID = copy.CuryInfoID;
    return true;
  }

  protected virtual PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.CurrencyInfo> GetRelatedPaymentAndCurrencyInfoFor(
    SOAdjust adj)
  {
    return (PXResult<PX.Objects.AR.ARPayment, PX.Objects.CM.CurrencyInfo>) PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(PXSelectBase<PX.Objects.AR.ARPayment, PXSelectJoin<PX.Objects.AR.ARPayment, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<PX.Objects.AR.ARPayment.curyInfoID>>>, Where<PX.Objects.AR.ARPayment.docType, Equal<Required<PX.Objects.AR.ARPayment.docType>>, And<PX.Objects.AR.ARPayment.refNbr, Equal<Required<PX.Objects.AR.ARPayment.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) adj.AdjgDocType,
      (object) adj.AdjgRefNbr
    }));
  }

  protected virtual Decimal GetConvertedDocumentBalanceFor(
    PX.Objects.AR.ARPayment payment,
    PX.Objects.CM.CurrencyInfo paymentCuryInfo,
    PXCache sender)
  {
    PX.Objects.CM.CurrencyInfo info = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<SOOrder.curyInfoID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    Decimal curyval;
    if (string.Equals(paymentCuryInfo.CuryID, info.CuryID))
    {
      curyval = payment.CuryDocBal.Value;
    }
    else
    {
      Decimal valueOrDefault = (payment.Released.GetValueOrDefault() ? payment.DocBal : payment.OrigDocAmt).GetValueOrDefault();
      PX.Objects.CM.PXDBCurrencyAttribute.CuryConvCury(sender, info, valueOrDefault, out curyval);
    }
    return curyval;
  }

  protected virtual Decimal NewBalanceCalculation(SOAdjust adj, Decimal newValue)
  {
    Decimal? nullable = adj.CuryDocBal;
    Decimal num1 = nullable.Value;
    nullable = adj.CuryAdjdAmt;
    Decimal num2 = nullable.Value;
    return num1 + num2 - newValue;
  }

  protected virtual void SOAdjust_CuryAdjdAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    SOAdjust row1 = (SOAdjust) e.Row;
    if (row1.AdjgRefNbr == null && sender.Graph.IsContractBasedAPI)
      return;
    PXCache sender1 = sender;
    object row2 = e.Row;
    Decimal? curyAdjdAmt = row1.CuryAdjdAmt;
    Decimal curyval1 = curyAdjdAmt.Value;
    Decimal baseval1;
    ref Decimal local = ref baseval1;
    PX.Objects.CM.PXDBCurrencyAttribute.CuryConvBase<SOAdjust.adjdCuryInfoID>(sender1, row2, curyval1, out local);
    PX.Objects.CM.CurrencyInfo currencyInfo1 = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<SOAdjust.adjgCuryInfoID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row1
    }, Array.Empty<object>()));
    PX.Objects.CM.CurrencyInfo currencyInfo2 = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<SOAdjust.adjdCuryInfoID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row1
    }, Array.Empty<object>()));
    Decimal curyval2;
    if (string.Equals(currencyInfo1.CuryID, currencyInfo2.CuryID))
    {
      curyAdjdAmt = row1.CuryAdjdAmt;
      curyval2 = curyAdjdAmt.Value;
    }
    else
      PX.Objects.CM.PXDBCurrencyAttribute.CuryConvCury<SOAdjust.adjgCuryInfoID>(sender, e.Row, baseval1, out curyval2);
    Decimal baseval2;
    if (object.Equals((object) currencyInfo1.CuryID, (object) currencyInfo2.CuryID) && object.Equals((object) currencyInfo1.CuryRate, (object) currencyInfo2.CuryRate) && object.Equals((object) currencyInfo1.CuryMultDiv, (object) currencyInfo2.CuryMultDiv))
      baseval2 = baseval1;
    else
      PX.Objects.CM.PXDBCurrencyAttribute.CuryConvBase<SOAdjust.adjgCuryInfoID>(sender, e.Row, curyval2, out baseval2);
    row1.CuryAdjgAmt = new Decimal?(curyval2);
    row1.AdjAmt = new Decimal?(baseval1);
    row1.RGOLAmt = new Decimal?(baseval2 - baseval1);
    this.UpdateBalanceOnCuryAdjdAmtUpdated(sender, e);
  }

  protected virtual void UpdateBalanceOnCuryAdjdAmtUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    SOAdjust row = (SOAdjust) e.Row;
    Decimal? curyDocBal = row.CuryDocBal;
    Decimal? nullable1 = (Decimal?) e.OldValue;
    Decimal? nullable2 = curyDocBal.HasValue & nullable1.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? curyAdjdAmt = row.CuryAdjdAmt;
    Decimal? nullable3;
    if (!(nullable2.HasValue & curyAdjdAmt.HasValue))
    {
      nullable1 = new Decimal?();
      nullable3 = nullable1;
    }
    else
      nullable3 = new Decimal?(nullable2.GetValueOrDefault() - curyAdjdAmt.GetValueOrDefault());
    row.CuryDocBal = nullable3;
  }

  protected virtual void SOAdjust_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is SOAdjust row))
      return;
    PXUIFieldAttribute.SetEnabled<SOAdjust.adjgDocType>(sender, (object) row, row.AdjgRefNbr == null);
    int num;
    if (sender.Graph.IsContractBasedAPI && ((PXSelectBase<SOOrder>) this.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<SOOrder>) this.Document).Current.Completed;
      if (!nullable.GetValueOrDefault())
      {
        nullable = ((PXSelectBase<SOOrder>) this.Document).Current.Cancelled;
        num = !nullable.GetValueOrDefault() ? 1 : 0;
        goto label_5;
      }
    }
    num = 0;
label_5:
    bool flag = num != 0;
    PXUIFieldAttribute.SetEnabled<SOAdjust.extRefNbr>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<SOAdjust.paymentMethodID>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<SOAdjust.pMInstanceID>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<SOAdjust.processingCenterID>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<SOAdjust.cashAccountID>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<SOAdjust.curyOrigDocAmt>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<SOAdjust.docDesc>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<SOAdjust.hold>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<SOAdjust.adjgDocDate>(sender, (object) row, flag);
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  public virtual void _(PX.Data.Events.CacheAttached<SOShippingAddress.latitude> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  public virtual void _(
    PX.Data.Events.CacheAttached<SOShippingAddress.longitude> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDBGuidAttribute))]
  [CopiedShipmentNoteID(IsKey = true)]
  protected virtual void SOOrderShipment_ShippingRefNoteID_CacheAttached(PXCache sender)
  {
  }

  [PXDBGuid(false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<SOOrderShipment.orderNoteID> args)
  {
  }

  protected virtual void SOOrderShipment_ShipmentNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOOrderShipment_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXUIFieldAttribute.SetEnabled(sender, e.Row, false);
    PXUIFieldAttribute.SetEnabled<SOOrderShipment.selected>(sender, e.Row, true);
    SOOrderShipment row = (SOOrderShipment) e.Row;
    if (row == null || row.InvoiceNbr == null)
      return;
    PX.Objects.AR.ARInvoice arInvoice = PX.Objects.AR.ARInvoice.PK.Find((PXGraph) this, row.InvoiceType, row.InvoiceNbr);
    PXSetPropertyException propertyException;
    if ((arInvoice != null ? (arInvoice.Rejected.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      propertyException = (PXSetPropertyException) null;
    else
      propertyException = new PXSetPropertyException("The {0} invoice has been rejected.", (PXErrorLevel) 2, new object[1]
      {
        (object) row.InvoiceNbr
      });
    PXException pxException = (PXException) propertyException;
    sender.RaiseExceptionHandling<SOOrderShipment.invoiceNbr>((object) row, (object) row.InvoiceNbr, (Exception) pxException);
  }

  [PXFormula(typeof (Selector<SOOrder.orderType, SOOrderType.deferPriceDiscountRecalculation>))]
  [PXMergeAttributes]
  public virtual void SOOrder_DeferPriceDiscountRecalculation_CacheAttached(PXCache sender)
  {
  }

  protected virtual void SOOrder_Cancelled_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    SOOrder row = (SOOrder) e.Row;
    if (e.Row == null || !((bool?) e.NewValue).GetValueOrDefault())
      return;
    SOOrderShipment soOrderShipment = PXResultset<SOOrderShipment>.op_Implicit(PXSelectBase<SOOrderShipment, PXSelectReadonly<SOOrderShipment, Where<SOOrderShipment.orderType, Equal<Current<SOOrder.orderType>>, And<SOOrderShipment.orderNbr, Equal<Current<SOOrder.orderNbr>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    if (soOrderShipment != null)
    {
      PXException pxException;
      if (!soOrderShipment.Confirmed.GetValueOrDefault())
        pxException = new PXException("The {0} {1} sales order cannot be cancelled because it already has the open {2} shipment.", new object[3]
        {
          (object) row.OrderNbr,
          (object) row.OrderType,
          (object) soOrderShipment.ShipmentNbr
        });
      else
        pxException = new PXException("The {0} sales order cannot be canceled because some items have been shipped.", new object[1]
        {
          (object) row.OrderNbr
        });
      throw pxException;
    }
  }

  protected virtual void SOOrder_TaxZoneID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    SOOrder row = e.Row as SOOrder;
    e.NewValue = (object) this.GetDefaultTaxZone(row);
  }

  public virtual string GetDefaultTaxZone(SOOrder row)
  {
    string defaultTaxZone = (string) null;
    if (row != null)
      defaultTaxZone = string.IsNullOrEmpty(row.TaxZoneID) || !row.OverrideTaxZone.GetValueOrDefault() ? this.GetDefaultTaxZone(PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) this.location).Select(Array.Empty<object>())), true, row.ShipVia, row.BranchID) : row.TaxZoneID;
    return defaultTaxZone;
  }

  public virtual string GetDefaultTaxZone(
    PX.Objects.CR.Location customerLocation,
    bool useOrderAddress,
    string shipVia,
    int? branchID)
  {
    if (!string.IsNullOrEmpty(customerLocation?.CTaxZoneID))
    {
      if (PXResultset<PX.Objects.TX.TaxZone>.op_Implicit(PXSelectBase<PX.Objects.TX.TaxZone, PXSelect<PX.Objects.TX.TaxZone, Where<PX.Objects.TX.TaxZone.taxZoneID, Equal<Required<PX.Objects.TX.TaxZone.taxZoneID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) customerLocation.CTaxZoneID
      })) != null)
        return customerLocation.CTaxZoneID;
    }
    if (this.IsCommonCarrier(shipVia))
    {
      IAddressBase adrress;
      if (useOrderAddress)
        adrress = (IAddressBase) PXResultset<SOShippingAddress>.op_Implicit(((PXSelectBase<SOShippingAddress>) this.Shipping_Address).Select(Array.Empty<object>()));
      else
        adrress = (IAddressBase) PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.bAccountID, Equal<Current<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Address.addressID, Equal<Current<PX.Objects.CR.Location.defAddressID>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) new PX.Objects.CR.Location[1]
        {
          customerLocation
        }, Array.Empty<object>()));
      if (adrress != null)
        return TaxBuilderEngine.GetTaxZoneByAddress((PXGraph) this, adrress);
    }
    else
    {
      PX.Objects.CR.BAccount baccount = (PX.Objects.CR.BAccount) PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXSelectJoin<BAccountR, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.bAccountID>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) branchID
      }));
      if (baccount != null)
      {
        PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) baccount.BAccountID,
          (object) baccount.DefLocationID
        }));
        if (location != null)
          return location.VTaxZoneID;
      }
    }
    return (string) null;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<SOOrder, SOOrder.branchID> e)
  {
    SOOrder row = e.Row;
    if (row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOOrder, SOOrder.branchID>>) e).Cache.SetDefaultExt<SOOrder.taxZoneID>((object) row);
    SOOrder.paymentMethodID parent = PrimaryKeyOf<PX.Objects.CA.PaymentMethod>.By<PX.Objects.CA.PaymentMethod.paymentMethodID>.ForeignKeyOf<SOOrder>.By<SOOrder.paymentMethodID>.FindParent((PXGraph) this, (SOOrder.paymentMethodID) row, (PKFindOptions) 0);
    if ((parent != null ? (EnumerableExtensions.IsIn<string>(((PX.Objects.CA.PaymentMethod) parent).PaymentType, "CCD", "EFT") ? 1 : 0) : 0) == 0)
      return;
    SOOrder copy = (SOOrder) ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOOrder, SOOrder.branchID>>) e).Cache.CreateCopy((object) row);
    copy.BranchID = (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<SOOrder, SOOrder.branchID>, SOOrder, object>) e).OldValue;
    object obj;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOOrder, SOOrder.branchID>>) e).Cache.RaiseFieldDefaulting<SOOrder.pMInstanceID>((object) copy, ref obj);
    int? nullable = (int?) obj;
    int? pmInstanceId = row.PMInstanceID;
    if (!(nullable.GetValueOrDefault() == pmInstanceId.GetValueOrDefault() & nullable.HasValue == pmInstanceId.HasValue))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOOrder, SOOrder.branchID>>) e).Cache.SetDefaultExt<SOOrder.pMInstanceID>((object) row);
  }

  protected virtual void SOOrder_CreditHold_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SOOrder row) || !PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() || PXResultset<SOSetupApproval>.op_Implicit(((PXSelectBase<SOSetupApproval>) this.SetupApproval).Select(Array.Empty<object>())) == null)
      return;
    sender.RaiseFieldUpdated<SOOrder.hold>((object) row, (object) true);
  }

  protected virtual bool IsCommonCarrier(string carrierID)
  {
    if (string.IsNullOrEmpty(carrierID))
      return false;
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, carrierID);
    return carrier != null && carrier.IsCommonCarrier.GetValueOrDefault();
  }

  protected virtual void SOOrder_DestinationSiteID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    SOOrder row = e.Row as SOOrder;
    if (e.Row != null && (row != null ? (!row.IsTransferOrder.GetValueOrDefault() ? 1 : 0) : 1) == 0)
      return;
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOOrder_DestinationSiteID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    SOOrder row = e.Row as SOOrder;
    ((PXSetup<PX.Objects.GL.Branch, Where<PX.Objects.IN.INSite.siteID, Equal<Optional<SOOrder.destinationSiteID>>>>) this.Company).RaiseFieldUpdated(sender, e.Row);
    string str = string.Empty;
    using (new PXReadBranchRestrictedScope())
    {
      PX.Objects.GL.Branch branch = PXResultset<PX.Objects.GL.Branch>.op_Implicit(((PXSelectBase<PX.Objects.GL.Branch>) this.Company).Select(Array.Empty<object>()));
      if (row != null && row.IsTransferOrder.GetValueOrDefault() && branch != null)
        sender.SetValueExt<SOOrder.customerID>(e.Row, (object) branch.BranchCD);
      try
      {
        SharedRecordAttribute.DefaultRecord<SOOrder.shipAddressID>(sender, e.Row);
      }
      catch (SharedRecordMissingException ex)
      {
        PXSetPropertyException propertyException = new PXSetPropertyException("The document cannot be saved, address is not specified for selected destination warehouse.", (PXErrorLevel) 4);
        if (((PXGraph) this).UnattendedMode)
          throw propertyException;
        sender.RaiseExceptionHandling<SOOrder.destinationSiteID>(e.Row, sender.GetValueExt<SOOrder.destinationSiteID>(e.Row), (Exception) propertyException);
        str = "The document cannot be saved, address is not specified for selected destination warehouse.";
        sender.SetValueExt<SOOrder.shipAddressID>(e.Row, (object) null);
      }
      try
      {
        SharedRecordAttribute.DefaultRecord<SOOrder.shipContactID>(sender, e.Row);
      }
      catch (SharedRecordMissingException ex)
      {
        PXSetPropertyException propertyException = new PXSetPropertyException("The document cannot be saved, contact is not specified for selected destination warehouse.", (PXErrorLevel) 4);
        if (((PXGraph) this).UnattendedMode)
          throw propertyException;
        sender.RaiseExceptionHandling<SOOrder.destinationSiteID>(e.Row, sender.GetValueExt<SOOrder.destinationSiteID>(e.Row), (Exception) propertyException);
        str = "The document cannot be saved, contact is not specified for selected destination warehouse.";
        sender.SetValueExt<SOOrder.shipContactID>(e.Row, (object) null);
      }
    }
    sender.SetValueExt<SOOrder.destinationSiteIdErrorMessage>(e.Row, (object) str);
  }

  protected virtual void SOOrder_CustomerLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<SOOrder.salesPersonID>(e.Row);
    sender.SetDefaultExt<SOOrder.taxCalcMode>(e.Row);
    sender.SetDefaultExt<SOOrder.externalTaxExemptionNumber>(e.Row);
    sender.SetDefaultExt<SOOrder.avalaraCustomerUsageType>(e.Row);
    sender.SetDefaultExt<SOOrder.workgroupID>(e.Row);
    sender.SetDefaultExt<SOOrder.shipVia>(e.Row);
    sender.SetDefaultExt<SOOrder.fOBPoint>(e.Row);
    sender.SetDefaultExt<SOOrder.resedential>(e.Row);
    sender.SetDefaultExt<SOOrder.saturdayDelivery>(e.Row);
    sender.SetDefaultExt<SOOrder.groundCollect>(e.Row);
    sender.SetDefaultExt<SOOrder.insurance>(e.Row);
    sender.SetDefaultExt<SOOrder.shipTermsID>(e.Row);
    sender.SetDefaultExt<SOOrder.shipZoneID>(e.Row);
    sender.SetDefaultExt<SOOrder.projectID>(e.Row);
    if (PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      sender.SetDefaultExt<SOOrder.defaultSiteID>(e.Row);
    sender.SetDefaultExt<SOOrder.priority>(e.Row);
    if (this.CustomerChanged)
    {
      if (!this.HasDetailRecords())
        sender.SetDefaultExt<SOOrder.shipComplete>(e.Row);
    }
    else
      sender.SetDefaultExt<SOOrder.shipComplete>(e.Row);
    sender.SetDefaultExt<SOOrder.shipDate>(e.Row);
    try
    {
      try
      {
        SharedRecordAttribute.DefaultRecord<SOOrder.shipAddressID>(sender, e.Row);
      }
      catch (SharedRecordMissingException ex)
      {
        PXSetPropertyException propertyException = new PXSetPropertyException("The document cannot be saved, address is not specified for selected destination warehouse.", (PXErrorLevel) 4);
        if (((PXGraph) this).UnattendedMode)
          throw propertyException;
        sender.RaiseExceptionHandling<SOOrder.destinationSiteID>(e.Row, sender.GetValueExt<SOOrder.destinationSiteID>(e.Row), (Exception) propertyException);
      }
      try
      {
        SharedRecordAttribute.DefaultRecord<SOOrder.shipContactID>(sender, e.Row);
      }
      catch (SharedRecordMissingException ex)
      {
        PXSetPropertyException propertyException = new PXSetPropertyException("The document cannot be saved, contact is not specified for selected destination warehouse.", (PXErrorLevel) 4);
        if (((PXGraph) this).UnattendedMode)
          throw propertyException;
        sender.RaiseExceptionHandling<SOOrder.destinationSiteID>(e.Row, sender.GetValueExt<SOOrder.destinationSiteID>(e.Row), (Exception) propertyException);
      }
    }
    catch (PXFieldValueProcessingException ex)
    {
      string locationCd = ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current.LocationCD;
      ((PXSetPropertyException) ex).ErrorValue = (object) locationCd;
      throw;
    }
    foreach (PXResult<SOLine> pxResult in ((PXSelectBase<SOLine>) this.Transactions).Select(Array.Empty<object>()))
    {
      SOLine soLine = PXResult<SOLine>.op_Implicit(pxResult);
      try
      {
        ((PXSelectBase) this.Transactions).Cache.SetDefaultExt<SOLine.salesAcctID>((object) soLine);
        ((PXSelectBase) this.Transactions).Cache.SetDefaultExt<SOLine.avalaraCustomerUsageType>((object) soLine);
      }
      catch (PXSetPropertyException ex)
      {
        ((PXSelectBase) this.Transactions).Cache.SetValue<SOLine.salesAcctID>((object) soLine, (object) null);
      }
    }
  }

  protected virtual void SOOrder_CustomerID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is SOOrder row) || object.Equals(e.NewValue, (object) row.CustomerID) || !this.HasDetailRecords())
      return;
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    }));
    if (customer != null)
    {
      bool? nullable = customer.AllowOverrideCury;
      if (!nullable.GetValueOrDefault() && customer.CuryID != row.CuryID && !string.IsNullOrEmpty(customer.CuryID))
        this.RaiseCustomerIDSetPropertyException(sender, row, e.NewValue, "Customer cannot be changed. Currency is not allowed for the new customer.");
      if (((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Current != null && ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Current.CuryID != ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Current.BaseCuryID)
      {
        nullable = customer.AllowOverrideRate;
        if (!nullable.GetValueOrDefault() && (!string.IsNullOrEmpty(customer.CuryRateTypeID) ? customer.CuryRateTypeID : ((PXSelectBase<CMSetup>) new PXSetup<CMSetup>((PXGraph) this)).Current.ARRateTypeDflt) != ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Current.CuryRateTypeID)
          this.RaiseCustomerIDSetPropertyException(sender, row, e.NewValue, "Customer cannot be changed. Currency rate type is not allowed for the new customer.");
      }
    }
    if (((PXSelectBase) this.Document).Cache.GetStatus((object) ((PXSelectBase<SOOrder>) this.Document).Current) != 2)
    {
      if (((PXSelectBase<SOOrderShipment>) this.shipmentlist).SelectSingle(Array.Empty<object>()) != null)
        this.RaiseCustomerIDSetPropertyException(sender, row, e.NewValue, "Customer cannot be changed. Either linked documents or transactions exist for the order.");
      if (PXResultset<PX.Objects.PO.POLine>.op_Implicit(((PXSelectBase<PX.Objects.PO.POLine>) new PXSelectJoin<PX.Objects.PO.POLine, InnerJoin<SOLineSplit, On<SOLineSplit.pOType, Equal<PX.Objects.PO.POLine.orderType>, And<SOLineSplit.pONbr, Equal<PX.Objects.PO.POLine.orderNbr>, And<SOLineSplit.pOLineNbr, Equal<PX.Objects.PO.POLine.lineNbr>>>>>, Where<SOLineSplit.orderType, Equal<Current<SOOrder.orderType>>, And<SOLineSplit.orderNbr, Equal<Current<SOOrder.orderNbr>>, And<PX.Objects.PO.POLine.orderType, Equal<POOrderType.dropShip>>>>>((PXGraph) this)).SelectWindowed(0, 1, Array.Empty<object>())) != null)
        this.RaiseCustomerIDSetPropertyException(sender, row, e.NewValue, "Customer cannot be changed. Drop-ship purchase order is linked to the order.");
      if (((PXSelectBase<SOOrder>) this.Document).Current.Behavior == "QT")
      {
        SOOrder soOrder = PXResultset<SOOrder>.op_Implicit(PXSelectBase<SOOrder, PXSelect<SOOrder, Where<SOOrder.origOrderType, Equal<Current<SOOrder.orderType>>, And<SOOrder.origOrderNbr, Equal<Current<SOOrder.orderNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
        if (soOrder != null)
        {
          int? customerId = soOrder.CustomerID;
          int? newValue = (int?) e.NewValue;
          if (!(customerId.GetValueOrDefault() == newValue.GetValueOrDefault() & customerId.HasValue == newValue.HasValue))
            this.RaiseCustomerIDSetPropertyException(sender, row, e.NewValue, "Customer cannot be changed.The quote is referred from an order.");
        }
      }
      if (((PXSelectBase<SOAdjust>) this.Adjustments).SelectSingle(Array.Empty<object>()) != null)
        this.RaiseCustomerIDSetPropertyException(sender, row, e.NewValue, "Customer cannot be changed. Payment is applied to the order.");
    }
    if (PXResult<SOLine>.op_Implicit(((IEnumerable<PXResult<SOLine>>) ((PXSelectBase<SOLine>) this.Transactions).Select(Array.Empty<object>())).AsEnumerable<PXResult<SOLine>>().Where<PXResult<SOLine>>((Func<PXResult<SOLine>, bool>) (res => !string.IsNullOrEmpty(PXResult<SOLine>.op_Implicit(res).InvoiceNbr))).FirstOrDefault<PXResult<SOLine>>()) == null)
      return;
    this.RaiseCustomerIDSetPropertyException(sender, row, e.NewValue, "Customer cannot be changed. Order line refers to an invoice.");
  }

  public virtual void RaiseCustomerIDSetPropertyException(
    PXCache sender,
    SOOrder order,
    object newCustomerID,
    string error)
  {
    BAccountR baccountR = (BAccountR) PXSelectorAttribute.Select<SOOrder.customerID>(sender, (object) order, newCustomerID);
    throw new PXSetPropertyException(error)
    {
      ErrorValue = (object) baccountR?.AcctCD
    };
  }

  public virtual bool HasDetailRecords()
  {
    if (((PXSelectBase<SOLine>) this.Transactions).Current != null)
      return true;
    return ((PXSelectBase) this.Document).Cache.GetStatus((object) ((PXSelectBase<SOOrder>) this.Document).Current) == 2 ? ((PXSelectBase) this.Transactions).Cache.IsDirty : ((PXSelectBase<SOLine>) this.Transactions).Select(Array.Empty<object>()).Count > 0;
  }

  public bool CustomerChanged { get; protected set; }

  [CustomerOrderNbr]
  [PXMergeAttributes]
  protected virtual void SOOrder_CustomerOrderNbr_CacheAttached(PXCache sender)
  {
  }

  [PopupMessage]
  [PXMergeAttributes]
  protected virtual void SOOrder_CustomerID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void SOOrder_CustomerID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    int? oldValue = (int?) e.OldValue;
    int num;
    if (oldValue.HasValue)
    {
      int? customerId = ((SOOrder) e.Row).CustomerID;
      int? nullable = oldValue;
      num = !(customerId.GetValueOrDefault() == nullable.GetValueOrDefault() & customerId.HasValue == nullable.HasValue) ? 1 : 0;
    }
    else
      num = 0;
    this.CustomerChanged = num != 0;
    if (this.CustomerChanged && ((SOOrder) e.Row).CreditHold.GetValueOrDefault())
      ((SelectedEntityEvent<SOOrder>) PXEntityEventBase<SOOrder>.Container<SOOrder.Events>.Select((Expression<Func<SOOrder.Events, PXEntityEvent<SOOrder.Events>>>) (ev => ev.CreditLimitSatisfied))).FireOn((PXGraph) this, (SOOrder) e.Row);
    sender.SetValue<SOOrder.extRefNbr>(e.Row, (object) null);
    sender.SetValue<SOOrder.approvedCredit>(e.Row, (object) false);
    sender.SetValue<SOOrder.approvedCreditAmt>(e.Row, (object) 0M);
    sender.SetValue<SOOrder.overrideTaxZone>(e.Row, (object) false);
    sender.SetDefaultExt<SOOrder.paymentMethodID>(e.Row);
    sender.SetDefaultExt<SOOrder.billSeparately>(e.Row);
    sender.SetDefaultExt<SOOrder.shipSeparately>(e.Row);
    if (SOOrderType.PK.Find((PXGraph) this, ((SOOrder) e.Row).OrderType).Behavior != "TR")
    {
      sender.SetValue<SOOrder.origOrderType>(e.Row, (object) null);
      sender.SetValue<SOOrder.origOrderNbr>(e.Row, (object) null);
    }
    if (this.CustomerChanged)
      sender.SetValue<SOOrder.customerRefNbr>(e.Row, (object) null);
    if (!e.ExternalCall && ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current != null)
      ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.CreditRule = (string) null;
    if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>() && (e.ExternalCall || sender.GetValuePending<SOOrder.curyID>(e.Row) == null))
    {
      if (!oldValue.HasValue || this.CustomerChanged && !this.HasDetailRecords())
      {
        PX.Objects.CM.CurrencyInfo currencyInfo = PX.Objects.CM.CurrencyInfoAttribute.SetDefaults<SOOrder.curyInfoID>(sender, e.Row);
        string error = PXUIFieldAttribute.GetError<PX.Objects.CM.CurrencyInfo.curyEffDate>(((PXSelectBase) this.currencyinfo).Cache, (object) currencyInfo);
        if (!string.IsNullOrEmpty(error))
          sender.RaiseExceptionHandling<SOOrder.orderDate>(e.Row, (object) ((SOOrder) e.Row).OrderDate, (Exception) new PXSetPropertyException(error, (PXErrorLevel) 2));
        if (currencyInfo != null)
          sender.SetValue<SOOrder.curyID>(e.Row, (object) currencyInfo.CuryID);
      }
      else
        PX.Objects.CM.CurrencyInfoAttribute.SetEffectiveDate<SOOrder.orderDate>(sender, e);
    }
    sender.SetDefaultExt<SOOrder.customerLocationID>(e.Row);
    if (e.ExternalCall || sender.GetValuePending<SOOrder.termsID>(e.Row) == null)
    {
      bool flag = ((PXSelectBase<SOOrderType>) this.soordertype).Current.ARDocType == "CRM";
      if (!flag || flag && ((PXSelectBase<ARSetup>) this.arsetup).Current.TermsInCreditMemos.GetValueOrDefault())
        sender.SetDefaultExt<SOOrder.termsID>(e.Row);
      else
        sender.SetValueExt<SOOrder.termsID>(e.Row, (object) null);
    }
    try
    {
      SharedRecordAttribute.DefaultRecord<SOOrder.billAddressID>(sender, e.Row);
      SharedRecordAttribute.DefaultRecord<SOOrder.billContactID>(sender, e.Row);
    }
    catch (PXFieldValueProcessingException ex)
    {
      string acctCd = ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.AcctCD;
      ((PXSetPropertyException) ex).ErrorValue = (object) acctCd;
      throw;
    }
    if (e.Row is SOOrder row)
      sender.SetDefaultExt<SOOrder.ownerID>((object) row);
    sender.SetDefaultExt<SOOrder.taxZoneID>(e.Row);
    sender.SetValue<SOOrder.emailed>((object) row, (object) false);
  }

  protected virtual void SOOrder_PaymentMethodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<SOOrder.pMInstanceID>(e.Row);
    sender.SetDefaultExt<SOOrder.cashAccountID>(e.Row);
  }

  protected virtual void SOOrder_PMInstanceID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<SOOrder.cashAccountID>(e.Row);
  }

  protected virtual void SOOrder_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    SOOrder row = (SOOrder) e.Row;
    bool? nullable1;
    int num1;
    if (row.IsInvoiceOrder.GetValueOrDefault())
    {
      nullable1 = row.BillSeparately;
      num1 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag1 = num1 != 0;
    PXDefaultAttribute.SetPersistingCheck<SOOrder.invoiceDate>(sender, (object) row, flag1 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<SOOrder.finPeriodID>(sender, (object) row, flag1 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXCache pxCache1 = sender;
    SOOrder soOrder1 = row;
    int num2;
    if (flag1)
    {
      nullable1 = row.IsUserInvoiceNumbering;
      if (nullable1.GetValueOrDefault())
      {
        num2 = 1;
        goto label_7;
      }
    }
    num2 = 2;
label_7:
    PXDefaultAttribute.SetPersistingCheck<SOOrder.invoiceNbr>(pxCache1, (object) soOrder1, (PXPersistingCheck) num2);
    PXDefaultAttribute.SetPersistingCheck<SOOrder.termsID>(sender, (object) row, !(row.ARDocType != "UND") || !(row.ARDocType != "CRM") ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    bool flag2 = false;
    nullable1 = row.IsPaymentInfoEnabled;
    if (nullable1.GetValueOrDefault() && !string.IsNullOrEmpty(row.PaymentMethodID))
    {
      PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.PaymentMethodID
      }));
      flag2 = paymentMethod != null && paymentMethod.PaymentType == "CCD";
    }
    PXDefaultAttribute.SetPersistingCheck<SOOrder.dueDate>(sender, (object) row, !flag1 || !(row.ARDocType != "CRM") ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXDefaultAttribute.SetPersistingCheck<SOOrder.discDate>(sender, (object) row, !flag1 || !(row.ARDocType != "CRM") ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXDefaultAttribute.SetPersistingCheck<SOOrder.paymentMethodID>(sender, (object) row, !flag1 || !(row.ARDocType == "CSL") && !(row.ARDocType == "RCS") ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXDefaultAttribute.SetPersistingCheck<SOOrder.cashAccountID>(sender, (object) row, !flag1 || !(row.ARDocType == "CSL") && !(row.ARDocType == "RCS") || string.IsNullOrEmpty(row.PaymentMethodID) ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXCache pxCache2 = sender;
    SOOrder soOrder2 = row;
    int num3;
    if (flag1 && (row.ARDocType == "CSL" || row.ARDocType == "RCS") && !flag2)
    {
      nullable1 = ((PXSelectBase<ARSetup>) this.arsetup).Current.RequireExtRef;
      if (nullable1.GetValueOrDefault())
      {
        num3 = 1;
        goto label_13;
      }
    }
    num3 = 2;
label_13:
    PXDefaultAttribute.SetPersistingCheck<SOOrder.extRefNbr>(pxCache2, (object) soOrder2, (PXPersistingCheck) num3);
    Decimal? nullable2;
    if (e.Operation == 2 || e.Operation == 1)
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      {
        nullable1 = row.IsTransferOrder;
        if (!nullable1.GetValueOrDefault() && row.CuryInfoID.HasValue)
        {
          PX.Objects.CM.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).SelectWindowed(0, 1, new object[1]
          {
            (object) row.CuryInfoID
          }));
          if (PXResultset<CMSetup>.op_Implicit(PXSelectBase<CMSetup, PXSelect<CMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>())) != null && currencyInfo != null && currencyInfo.BaseCuryID != currencyInfo.CuryID && currencyInfo.CuryRateTypeID == null)
            throw new PXRowPersistingException(typeof (SOOrder.curyID).Name, (object) row.CuryID, "Currency rate type is not specified for the document.");
        }
      }
      nullable2 = row.CuryDiscTot;
      Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
      nullable2 = row.CuryLineTotal;
      Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
      nullable2 = row.CuryMiscTot;
      Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
      Decimal num4 = Math.Abs(valueOrDefault2 + valueOrDefault3);
      if (valueOrDefault1 > num4 && sender.RaiseExceptionHandling<SOOrder.curyDiscTot>(e.Row, (object) row.CuryDiscTot, (Exception) new PXSetPropertyException("The total amount of line and document discounts cannot exceed the Detail Total amount.", (PXErrorLevel) 4)))
        throw new PXRowPersistingException(typeof (SOOrder.curyDiscTot).Name, (object) null, "The total amount of line and document discounts cannot exceed the Detail Total amount.");
      if (row.Status == "C")
      {
        nullable1 = row.Hold;
        if (nullable1.GetValueOrDefault())
          throw new PXRowPersistingException(typeof (SOOrder.status).Name, (object) null, PXMessages.LocalizeFormatNoPrefixNLA("Sales Order {0} is on Hold and cannot be completed. Operation aborted.", new object[1]
          {
            (object) row.OrderNbr
          }));
      }
      if (row != null)
      {
        nullable1 = row.IsTransferOrder;
        if (nullable1.GetValueOrDefault() && PXUIFieldAttribute.GetError<SOOrder.destinationSiteID>(sender, e.Row) == null)
        {
          string siteIdErrorMessage = row.DestinationSiteIdErrorMessage;
          if (!string.IsNullOrWhiteSpace(siteIdErrorMessage))
            throw new PXRowPersistingException(typeof (SOOrder.destinationSiteID).Name, sender.GetValueExt<SOOrder.destinationSiteID>(e.Row), siteIdErrorMessage);
        }
      }
      if (row != null)
      {
        nullable1 = row.DisableAutomaticTaxCalculation;
        if (nullable1.GetValueOrDefault())
        {
          if (row.TaxCalcMode == "G")
            throw new PXRowPersistingException(typeof (SOOrder.taxCalcMode).Name, (object) null, PXMessages.LocalizeNoPrefix("The Disable Automatic Tax Calculation check box and the Gross tax calculation mode on the Financial tab of the Sales Orders (SO301000) form cannot be selected simultaneously for a sales order."));
          if (row.TaxCalcMode != "N")
          {
            foreach (PXResult<SOTaxTran, PX.Objects.TX.Tax> pxResult in ((PXSelectBase<SOTaxTran>) this.Taxes).Select(Array.Empty<object>()))
            {
              if (PXResult<SOTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult)?.TaxCalcLevel == "0")
                throw new PXRowPersistingException(typeof (SOOrder.disableAutomaticTaxCalculation).Name, (object) row.DisableAutomaticTaxCalculation, PXMessages.LocalizeNoPrefix("The Disable Automatic Tax Calculation check box cannot be selected on the Financial tab of the Sales Orders (SO301000) form for a sales order that has at least one tax with the Inclusive Line-Level or Inclusive Document-Level calculation rule specified on the Taxes (TX205000) form."));
            }
          }
        }
      }
      if (row != null && !string.IsNullOrEmpty(row.ShipVia))
      {
        PX.Objects.CS.Carrier carrier = (PX.Objects.CS.Carrier) PXSelectorAttribute.Select<SOOrder.shipVia>(sender, (object) row);
        int num5;
        if (carrier == null)
        {
          num5 = 0;
        }
        else
        {
          nullable1 = carrier.IsActive;
          bool flag3 = false;
          num5 = nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue ? 1 : 0;
        }
        if (num5 != 0)
          throw new PXRowPersistingException(typeof (SOOrder.shipVia).Name, (object) null, "The Ship Via code is not active.");
      }
    }
    if (e.Operation == 1)
    {
      int? nullable3 = row.ShipmentCntr;
      int num6 = 0;
      if (!(nullable3.GetValueOrDefault() < num6 & nullable3.HasValue))
      {
        nullable3 = row.OpenShipmentCntr;
        int num7 = 0;
        if (!(nullable3.GetValueOrDefault() < num7 & nullable3.HasValue))
        {
          nullable3 = row.ShipmentCntr;
          int? billedCntr = row.BilledCntr;
          int? releasedCntr = row.ReleasedCntr;
          int? nullable4 = billedCntr.HasValue & releasedCntr.HasValue ? new int?(billedCntr.GetValueOrDefault() + releasedCntr.GetValueOrDefault()) : new int?();
          if (!(nullable3.GetValueOrDefault() < nullable4.GetValueOrDefault() & nullable3.HasValue & nullable4.HasValue) || !(row.Behavior == "SO"))
            goto label_51;
        }
      }
      throw new InvalidShipmentCountersException();
    }
label_51:
    if (((PXGraph) this).IsMobile)
    {
      nullable1 = row.Hold;
      bool flag4 = false;
      if (nullable1.GetValueOrDefault() == flag4 & nullable1.HasValue)
      {
        nullable1 = row.Completed;
        bool flag5 = false;
        if (nullable1.GetValueOrDefault() == flag5 & nullable1.HasValue)
        {
          nullable2 = row.CuryOrderTotal;
          Decimal? nullable5 = row.CuryControlTotal;
          if (!(nullable2.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable2.HasValue == nullable5.HasValue))
          {
            sender.RaiseExceptionHandling<SOOrder.curyControlTotal>(e.Row, (object) row.CuryControlTotal, (Exception) new PXSetPropertyException("The document is out of the balance."));
          }
          else
          {
            nullable5 = row.CuryOrderTotal;
            Decimal num8 = 0M;
            if (nullable5.GetValueOrDefault() < num8 & nullable5.HasValue && row.ARDocType != "UND" && EnumerableExtensions.IsNotIn<string>(row.Behavior, "RM", "MO"))
            {
              nullable1 = ((PXSelectBase<SOOrderType>) this.soordertype).Current.RequireControlTotal;
              if (nullable1.GetValueOrDefault())
                sender.RaiseExceptionHandling<SOOrder.curyControlTotal>(e.Row, (object) row.CuryControlTotal, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
              else
                sender.RaiseExceptionHandling<SOOrder.curyOrderTotal>(e.Row, (object) row.CuryOrderTotal, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
            }
            else
            {
              nullable1 = ((PXSelectBase<SOOrderType>) this.soordertype).Current.RequireControlTotal;
              if (nullable1.GetValueOrDefault())
                sender.RaiseExceptionHandling<SOOrder.curyControlTotal>(e.Row, (object) null, (Exception) null);
              else
                sender.RaiseExceptionHandling<SOOrder.curyOrderTotal>(e.Row, (object) null, (Exception) null);
            }
          }
        }
      }
    }
    this.ValidateControlTotal(sender, row, true);
  }

  protected virtual void SOOrder_OrderDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    PX.Objects.CM.CurrencyInfoAttribute.SetEffectiveDate<SOOrder.orderDate>(sender, e);
  }

  protected virtual void SOOrder_BillSeparately_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<SOOrder.invoiceDate>(e.Row);
    sender.SetDefaultExt<SOOrder.invoiceNbr>(e.Row);
    sender.SetDefaultExt<SOOrder.extRefNbr>(e.Row);
    bool? billSeparately = ((SOOrder) e.Row).BillSeparately;
    bool flag = false;
    if (!(billSeparately.GetValueOrDefault() == flag & billSeparately.HasValue))
      return;
    sender.SetValuePending<SOOrder.invoiceDate>(e.Row, (object) null);
    sender.SetValuePending<SOOrder.invoiceNbr>(e.Row, (object) null);
    sender.SetValuePending<SOOrder.extRefNbr>(e.Row, (object) null);
    sender.SetValuePending<SOOrder.finPeriodID>(e.Row, (object) null);
  }

  protected virtual void SOOrder_FinPeriodID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    SOOrder row = e.Row as SOOrder;
    if (e.Row != null)
    {
      bool? nullable = row.BillSeparately;
      bool flag1 = false;
      if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue) && ((PXSelectBase<SOOrderType>) this.soordertype).Current != null)
      {
        nullable = row.IsInvoiceOrder;
        bool flag2 = false;
        if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
          return;
      }
    }
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOOrder_InvoiceDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    SOOrder row = e.Row as SOOrder;
    if (e.Row != null)
    {
      bool? nullable = row.BillSeparately;
      bool flag1 = false;
      if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue) && ((PXSelectBase<SOOrderType>) this.soordertype).Current != null)
      {
        nullable = row.IsInvoiceOrder;
        bool flag2 = false;
        if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
        {
          PXFieldDefaultingEventArgs defaultingEventArgs = e;
          nullable = ((PXSelectBase<SOSetup>) this.sosetup).Current.UseShipDateForInvoiceDate;
          object obj = nullable.GetValueOrDefault() ? sender.GetValue<SOOrder.shipDate>(e.Row) : sender.GetValue<SOOrder.orderDate>(e.Row);
          defaultingEventArgs.NewValue = obj;
          goto label_5;
        }
      }
    }
    e.NewValue = (object) null;
label_5:
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOOrder_InvoiceNbr_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    SOOrder row = e.Row as SOOrder;
    if (e.Row != null)
    {
      bool? nullable = row.BillSeparately;
      bool flag1 = false;
      if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue) && ((PXSelectBase<SOOrderType>) this.soordertype).Current != null)
      {
        nullable = row.IsInvoiceOrder;
        bool flag2 = false;
        if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
          return;
      }
    }
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOOrder_Priority_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is SOOrder) || ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current == null || !((PXSelectBase<PX.Objects.CR.Location>) this.location).Current.COrderPriority.HasValue)
      return;
    e.NewValue = (object) ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current.COrderPriority.GetValueOrDefault();
  }

  protected virtual void SOOrder_ShipComplete_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is SOOrder))
      return;
    e.NewValue = ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current == null || string.IsNullOrEmpty(((PXSelectBase<PX.Objects.CR.Location>) this.location).Current.CShipComplete) ? (object) "L" : (object) ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current.CShipComplete;
    if (!((string) e.NewValue == "B") || ((PXSelectBase<SOOrderType>) this.soordertype).Current == null || !((PXSelectBase<SOOrderType>) this.soordertype).Current.RequireLocation.GetValueOrDefault())
      return;
    e.NewValue = (object) "L";
  }

  protected virtual void SOOrder_PMInstanceID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    SOOrder row = e.Row as SOOrder;
    if (e.Row != null && ((PXSelectBase<SOOrderType>) this.soordertype).Current != null)
    {
      bool? paymentInfoEnabled = row.IsPaymentInfoEnabled;
      bool flag = false;
      if (!(paymentInfoEnabled.GetValueOrDefault() == flag & paymentInfoEnabled.HasValue))
        return;
    }
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOOrder_PaymentMethodID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    SOOrder row = e.Row as SOOrder;
    if (e.Row != null && ((PXSelectBase<SOOrderType>) this.soordertype).Current != null)
    {
      bool? paymentInfoEnabled = row.IsPaymentInfoEnabled;
      bool flag = false;
      if (!(paymentInfoEnabled.GetValueOrDefault() == flag & paymentInfoEnabled.HasValue))
        return;
    }
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOOrder_CashAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    SOOrder row = e.Row as SOOrder;
    if (e.Row != null && ((PXSelectBase<SOOrderType>) this.soordertype).Current != null)
    {
      bool? paymentInfoEnabled = row.IsPaymentInfoEnabled;
      bool flag = false;
      if (!(paymentInfoEnabled.GetValueOrDefault() == flag & paymentInfoEnabled.HasValue))
        return;
    }
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOOrder_CashAccountID_ExceptionHandling(
    PXCache sender,
    PXExceptionHandlingEventArgs e)
  {
    if (e.Exception == null || !(e.Row is SOOrder row))
      return;
    ((CancelEventArgs) e).Cancel = true;
    int? nullable = new int?();
    row.CashAccountID = nullable;
  }

  protected virtual void SOOrder_OverrideTaxZone_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SOOrder row) || row.OverrideTaxZone.GetValueOrDefault())
      return;
    sender.SetDefaultExt<SOOrder.taxZoneID>(e.Row);
  }

  protected virtual void SOOrder_ShipVia_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SOOrder row))
      return;
    if (!row.OverrideTaxZone.GetValueOrDefault() && (e.OldValue == null || e.OldValue != null && this.IsCommonCarrier(e.OldValue.ToString()) != this.IsCommonCarrier(row.ShipVia)))
      sender.SetDefaultExt<SOOrder.taxZoneID>(e.Row);
    sender.SetDefaultExt<SOOrder.freightTaxCategoryID>(e.Row);
    row.UseCustomerAccount = new bool?(this.CanUseCustomerAccount(row));
  }

  protected virtual void SOOrder_IsManualPackage_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SOOrder row) || row.IsManualPackage.GetValueOrDefault())
      return;
    foreach (PXResult<SOPackageInfoEx> pxResult in ((PXSelectBase<SOPackageInfoEx>) this.Packages).Select(Array.Empty<object>()))
      ((PXSelectBase<SOPackageInfoEx>) this.Packages).Delete(PXResult<SOPackageInfoEx>.op_Implicit(pxResult));
    row.PackageWeight = new Decimal?(0M);
    sender.SetValue<SOOrder.isPackageValid>((object) row, (object) false);
  }

  [Obsolete("Event handler is kept to avoid breaking changes.")]
  protected virtual bool CanUseCustomerAccount(SOOrder row)
  {
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, row.ShipVia);
    if (carrier != null && !string.IsNullOrEmpty(carrier.CarrierPluginID))
    {
      foreach (PXResult<CarrierPluginCustomer> pxResult in PXSelectBase<CarrierPluginCustomer, PXSelect<CarrierPluginCustomer, Where<CarrierPluginCustomer.carrierPluginID, Equal<Required<CarrierPluginCustomer.carrierPluginID>>, And<CarrierPluginCustomer.customerID, Equal<Required<CarrierPluginCustomer.customerID>>, And<CarrierPluginCustomer.isActive, Equal<True>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) carrier.CarrierPluginID,
        (object) row.CustomerID
      }))
      {
        CarrierPluginCustomer carrierPluginCustomer = PXResult<CarrierPluginCustomer>.op_Implicit(pxResult);
        if (!string.IsNullOrEmpty(carrierPluginCustomer.CarrierAccount))
        {
          int? customerLocationId1 = carrierPluginCustomer.CustomerLocationID;
          int? customerLocationId2 = row.CustomerLocationID;
          if (!(customerLocationId1.GetValueOrDefault() == customerLocationId2.GetValueOrDefault() & customerLocationId1.HasValue == customerLocationId2.HasValue))
          {
            customerLocationId2 = carrierPluginCustomer.CustomerLocationID;
            if (customerLocationId2.HasValue)
              continue;
          }
          return true;
        }
      }
    }
    return false;
  }

  protected virtual bool CanUseGroundCollect(SOOrder row)
  {
    if (string.IsNullOrEmpty(row.ShipVia))
      return false;
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, row.ShipVia);
    return (carrier != null ? (!carrier.IsExternal.GetValueOrDefault() ? 1 : 0) : 1) == 0 && !string.IsNullOrEmpty(carrier?.CarrierPluginID) && CarrierPluginMaint.GetCarrierPluginAttributes((PXGraph) this, carrier.CarrierPluginID).Contains("COLLECT");
  }

  protected virtual void SOOrder_UseCustomerAccount_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is SOOrder row))
      return;
    bool flag = this.CanUseCustomerAccount(row);
    if (e.NewValue != null && (bool) e.NewValue && !flag)
    {
      e.NewValue = (object) false;
      throw new PXSetPropertyException("Customer Account is not configured. Please setup the Carrier Account on the Carrier Plug-in screen.");
    }
  }

  protected virtual void SOOrder_ShipDate_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    cache.SetDefaultExt<SOOrder.invoiceDate>(e.Row);
    DateTime? oldValue = (DateTime?) e.OldValue;
    DateTime? shipDate = ((SOOrder) e.Row).ShipDate;
    if ((oldValue.HasValue == shipDate.HasValue ? (oldValue.HasValue ? (oldValue.GetValueOrDefault() != shipDate.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0 || ((PXSelectBase) this.Document).View.Answer != null || ((PXGraph) this).IsMobile || !(((SOOrder) e.Row).ShipComplete == "B") || !this.HasDetailRecords())
      return;
    ((PXSelectBase<SOOrder>) this.Document).Ask("Confirmation", "Do you want to update order lines with changed requested date and recalculate scheduled shipment date?", (MessageButtons) 4);
  }

  protected virtual void SOOrder_ExtRefNbr_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    SOOrder row = e.Row as SOOrder;
    if (e.Row != null)
    {
      bool? nullable = row.BillSeparately;
      bool flag1 = false;
      if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue) && ((PXSelectBase<SOOrderType>) this.soordertype).Current != null)
      {
        nullable = row.IsInvoiceOrder;
        bool flag2 = false;
        if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue) && (!(((PXSelectBase<SOOrderType>) this.soordertype).Current.ARDocType != "CSL") || !(((PXSelectBase<SOOrderType>) this.soordertype).Current.ARDocType != "RCS")))
          return;
      }
    }
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  public bool IsCopyOrder { get; protected set; }

  /// <summary>
  /// The flag indicates that group and document discounts are disabled.
  /// </summary>
  protected virtual bool DisableGroupDocDiscount
  {
    get
    {
      SOOrder current1 = ((PXSelectBase<SOOrder>) this.Document).Current;
      bool? nullable;
      int num1;
      if (current1 == null)
      {
        num1 = 0;
      }
      else
      {
        nullable = current1.IsRMAOrder;
        num1 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      if (num1 == 0)
      {
        SOOrder current2 = ((PXSelectBase<SOOrder>) this.Document).Current;
        int num2;
        if (current2 == null)
        {
          num2 = 0;
        }
        else
        {
          nullable = current2.IsTransferOrder;
          num2 = nullable.GetValueOrDefault() ? 1 : 0;
        }
        if (num2 == 0)
          return false;
      }
      return ((PXSelectBase<SOOrderType>) this.soordertype).Current.ARDocType == "UND";
    }
  }

  protected virtual void SOOrder_DontApprove_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    SOOrder row = (SOOrder) e.Row;
    if (string.IsNullOrEmpty(row?.OrderType))
      return;
    if (PXResultset<SOSetupApproval>.op_Implicit(((PXSelectBase<SOSetupApproval>) this.SetupApproval).Select(new object[1]
    {
      (object) row.OrderType
    })) == null)
      return;
    e.NewValue = (object) false;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOOrder_RowSelecting(PXCache cache, PXRowSelectingEventArgs e)
  {
    SOOrder row = (SOOrder) e.Row;
    if (string.IsNullOrEmpty(row?.OrderType))
      return;
    if (PXResultset<SOSetupApproval>.op_Implicit(((PXSelectBase<SOSetupApproval>) this.SetupApproval).Select(new object[1]
    {
      (object) row.OrderType
    })) == null)
      return;
    row.DontApprove = new bool?(false);
  }

  protected virtual void SOOrder_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    SOOrder doc = e.Row as SOOrder;
    if (doc == null)
      return;
    bool? nullable1 = doc.DeferPriceDiscountRecalculation;
    if (nullable1.GetValueOrDefault() && !this.IsCopyOrder)
      TaxBaseAttribute.SetTaxCalc<SOLine.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.ManualCalc | TaxCalc.RedefaultAlways);
    int? nullable2;
    if (doc != this._LastSelected)
    {
      PXCache cache1 = ((PXSelectBase) this.Transactions).Cache;
      int? activeOperationsCntr = ((PXSelectBase<SOOrderType>) this.soordertype).Current.ActiveOperationsCntr;
      int num1 = 1;
      int num2 = activeOperationsCntr.GetValueOrDefault() > num1 & activeOperationsCntr.HasValue ? 1 : 0;
      PXUIFieldAttribute.SetVisible<SOLine.operation>(cache1, (object) null, num2 != 0);
      PXCache cache2 = ((PXSelectBase) this.Packages).Cache;
      nullable2 = ((PXSelectBase<SOOrderType>) this.soordertype).Current.ActiveOperationsCntr;
      int num3 = 1;
      int num4 = nullable2.GetValueOrDefault() > num3 & nullable2.HasValue ? 1 : 0;
      PXUIFieldAttribute.SetVisible<SOPackageInfo.operation>(cache2, (object) null, num4 != 0);
      PXCache cache3 = ((PXSelectBase) this.Transactions).Cache;
      nullable2 = ((PXSelectBase<SOOrderType>) this.soordertype).Current.ActiveOperationsCntr;
      int num5 = 1;
      int num6 = nullable2.GetValueOrDefault() > num5 & nullable2.HasValue ? 1 : 0;
      PXUIFieldAttribute.SetVisible<SOLine.autoCreateIssueLine>(cache3, (object) null, num6 != 0);
      PXCache cache4 = ((PXSelectBase) this.Transactions).Cache;
      nullable2 = ((PXSelectBase<SOOrderType>) this.soordertype).Current.ActiveOperationsCntr;
      int num7 = 1;
      int num8 = nullable2.GetValueOrDefault() > num7 & nullable2.HasValue ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<SOLine.autoCreateIssueLine>(cache4, (object) null, num8 != 0);
      PXUIFieldAttribute.SetVisible<SOLine.curyUnitCost>(((PXSelectBase) this.Transactions).Cache, (object) null, this.IsCuryUnitCostVisible(doc));
      this._LastSelected = doc;
    }
    PXCache pxCache1 = cache;
    SOOrder soOrder1 = doc;
    int num9;
    if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
    {
      nullable1 = doc.IsTransferOrder;
      num9 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num9 = 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyID>(pxCache1, (object) soOrder1, num9 != 0);
    PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>();
    if (!((PXGraph) this).IsImport || ((PXGraph) this).IsMobile)
      this.ValidateControlTotal(cache, doc);
    bool flag1 = this.IsCurrencyEnabled(doc);
    bool flag2 = false;
    nullable1 = doc.IsFreightAvailable;
    bool valueOrDefault = nullable1.GetValueOrDefault();
    PXAction<SOOrder> prepareInvoice = this.prepareInvoice;
    nullable1 = doc.Hold;
    bool flag3 = false;
    int num10;
    if (nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue)
    {
      nullable1 = doc.Cancelled;
      bool flag4 = false;
      if (nullable1.GetValueOrDefault() == flag4 & nullable1.HasValue && (((PXSelectBase<SOOrderType>) this.soordertype).Current.ARDocType != "UND" || doc.Behavior == "BL"))
      {
        int? shipmentCntr = doc.ShipmentCntr;
        int? openShipmentCntr = doc.OpenShipmentCntr;
        int? nullable3 = shipmentCntr.HasValue & openShipmentCntr.HasValue ? new int?(shipmentCntr.GetValueOrDefault() - openShipmentCntr.GetValueOrDefault()) : new int?();
        int? billedCntr = doc.BilledCntr;
        int? nullable4 = nullable3.HasValue & billedCntr.HasValue ? new int?(nullable3.GetValueOrDefault() - billedCntr.GetValueOrDefault()) : new int?();
        int? releasedCntr = doc.ReleasedCntr;
        nullable2 = nullable4.HasValue & releasedCntr.HasValue ? new int?(nullable4.GetValueOrDefault() - releasedCntr.GetValueOrDefault()) : new int?();
        int num11 = 0;
        if (!(nullable2.GetValueOrDefault() > num11 & nullable2.HasValue) && !this.HasMiscLinesToInvoice(doc))
        {
          nullable1 = SOBehavior.GetRequireShipmentValue(doc.Behavior, ((PXSelectBase<SOOrderType>) this.soordertype).Current.RequireShipping);
          bool flag5 = false;
          num10 = nullable1.GetValueOrDefault() == flag5 & nullable1.HasValue ? 1 : 0;
          goto label_16;
        }
        num10 = 1;
        goto label_16;
      }
    }
    num10 = 0;
label_16:
    ((PXAction) prepareInvoice).SetEnabled(num10 != 0);
    bool flag6 = this.AllowAllocation();
    if (doc != null)
    {
      nullable1 = doc.Completed;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = doc.Cancelled;
        if (!nullable1.GetValueOrDefault() && flag6)
        {
          int num12 = doc.ARDocType == "RCS" ? 1 : 0;
          PXUIFieldAttribute.SetEnabled(cache, (object) doc, true);
          PXUIFieldAttribute.SetEnabled<SOOrder.status>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.orderQty>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.orderWeight>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.orderVolume>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.packageWeight>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyOrderTotal>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyUnpaidBalance>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyLineTotal>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyGoodsExtPriceTotal>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyMiscTot>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyMiscExtPriceTotal>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyDetailExtPriceTotal>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyFreightCost>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.freightCostIsValid>(cache, (object) doc, false);
          PXCache pxCache2 = cache;
          SOOrder soOrder2 = doc;
          nullable1 = doc.OverrideFreightAmount;
          int num13 = nullable1.GetValueOrDefault() ? 1 : 0;
          PXUIFieldAttribute.SetEnabled<SOOrder.curyFreightAmt>(pxCache2, (object) soOrder2, num13 != 0);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyTaxTotal>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.openOrderQty>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyOpenOrderTotal>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyOpenLineTotal>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyOpenTaxTotal>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.unbilledOrderQty>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyUnbilledOrderTotal>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyUnbilledLineTotal>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyUnbilledTaxTotal>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyPaymentTotal>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyID>(cache, (object) doc, flag1);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyUnreleasedPaymentAmt>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyCCAuthorizedAmt>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyPaidAmt>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyBilledPaymentTotal>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.origOrderType>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.origOrderNbr>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyVatExemptTotal>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.curyVatTaxableTotal>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.overrideFreightAmount>(cache, (object) doc, this.AllowChangingOverrideFreightAmount(doc));
          PXUIFieldAttribute.SetEnabled<SOOrder.freightAmountSource>(cache, (object) doc, false);
          PXCache pxCache3 = cache;
          SOOrder soOrder3 = doc;
          nullable2 = doc.BilledCntr;
          int num14 = 0;
          int num15;
          if (nullable2.GetValueOrDefault() == num14 & nullable2.HasValue)
          {
            nullable2 = doc.ReleasedCntr;
            int num16 = 0;
            num15 = nullable2.GetValueOrDefault() == num16 & nullable2.HasValue ? 1 : 0;
          }
          else
            num15 = 0;
          PXUIFieldAttribute.SetEnabled<SOOrder.disableAutomaticTaxCalculation>(pxCache3, (object) soOrder3, num15 != 0);
          PXUIFieldAttribute.SetEnabled<SOOrder.emailed>(cache, (object) doc, false);
          PXUIFieldAttribute.SetEnabled<SOOrder.printed>(cache, (object) doc, false);
          if (((PXSelectBase<SOOrderType>) this.soordertype).Current != null)
          {
            nullable1 = doc.IsInvoiceOrder;
            int num17;
            if (nullable1.GetValueOrDefault())
            {
              nullable1 = doc.BillSeparately;
              num17 = nullable1.GetValueOrDefault() ? 1 : 0;
            }
            else
              num17 = 0;
            bool flag7 = num17 != 0;
            bool flag8 = false;
            bool flag9 = !string.IsNullOrEmpty(doc.PaymentMethodID);
            nullable1 = doc.IsPaymentInfoEnabled;
            if (nullable1.GetValueOrDefault() & flag9)
            {
              PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
              {
                (object) doc.PaymentMethodID
              }));
              if (paymentMethod != null)
              {
                nullable1 = paymentMethod.IsAccountNumberRequired;
                flag8 = nullable1.GetValueOrDefault();
                flag2 = paymentMethod.PaymentType == "CCD";
              }
            }
            PXCache pxCache4 = cache;
            SOOrder soOrder4 = doc;
            nullable1 = doc.IsNoAROrder;
            int num18 = nullable1.GetValueOrDefault() ? 0 : (((PXSelectBase<SOOrderType>) this.soordertype).Current.Behavior != "MO" ? 1 : 0);
            PXUIFieldAttribute.SetEnabled<SOOrder.billSeparately>(pxCache4, (object) soOrder4, num18 != 0);
            PXCache pxCache5 = cache;
            SOOrder soOrder5 = doc;
            int num19;
            if (doc.ARDocType == "CRM")
            {
              nullable1 = ((PXSelectBase<ARSetup>) this.arsetup).Current.TermsInCreditMemos;
              num19 = nullable1.GetValueOrDefault() ? 1 : 0;
            }
            else
              num19 = 1;
            PXUIFieldAttribute.SetEnabled<SOOrder.termsID>(pxCache5, (object) soOrder5, num19 != 0);
            PXUIFieldAttribute.SetEnabled<SOOrder.invoiceDate>(cache, (object) doc, flag7);
            PXCache pxCache6 = cache;
            int num20;
            if (!flag7 || !(((PXSelectBase<SOOrderType>) this.soordertype).Current.Behavior == "IN"))
            {
              if (doc.ARDocType == "CRM")
              {
                nullable1 = doc.BillSeparately;
                num20 = nullable1.GetValueOrDefault() ? 1 : 0;
              }
              else
                num20 = 0;
            }
            else
              num20 = 1;
            PXUIFieldAttribute.SetRequired<SOOrder.invoiceDate>(pxCache6, num20 != 0);
            PXUIFieldAttribute.SetEnabled<SOOrder.invoiceNbr>(cache, (object) doc, flag7);
            PXUIFieldAttribute.SetEnabled<SOOrder.finPeriodID>(cache, (object) doc, flag7);
            PXCache pxCache7 = cache;
            int num21;
            if (!flag7 || !(((PXSelectBase<SOOrderType>) this.soordertype).Current.Behavior == "IN"))
            {
              if (doc.ARDocType == "CRM")
              {
                nullable1 = doc.BillSeparately;
                num21 = nullable1.GetValueOrDefault() ? 1 : 0;
              }
              else
                num21 = 0;
            }
            else
              num21 = 1;
            PXUIFieldAttribute.SetRequired<SOOrder.finPeriodID>(pxCache7, num21 != 0);
            nullable1 = doc.IsPaymentInfoEnabled;
            int num22;
            if (nullable1.GetValueOrDefault())
            {
              nullable2 = doc.CustomerID;
              num22 = nullable2.HasValue ? 1 : 0;
            }
            else
              num22 = 0;
            bool flag10 = num22 != 0;
            PXUIFieldAttribute.SetEnabled<SOOrder.paymentMethodID>(cache, (object) doc, flag10);
            PXCache pxCache8 = cache;
            nullable1 = doc.BillSeparately;
            int num23 = !nullable1.GetValueOrDefault() ? 0 : (EnumerableExtensions.IsIn<string>(doc.ARDocType, "CSL", "RCS") ? 1 : 0);
            PXUIFieldAttribute.SetRequired<SOOrder.paymentMethodID>(pxCache8, num23 != 0);
            PXUIFieldAttribute.SetEnabled<SOOrder.pMInstanceID>(cache, (object) doc, flag10 & flag8);
            PXCache pxCache9 = cache;
            SOOrder soOrder6 = doc;
            nullable1 = doc.IsPaymentInfoEnabled;
            int num24 = nullable1.GetValueOrDefault() & flag9 ? 1 : 0;
            PXUIFieldAttribute.SetEnabled<SOOrder.cashAccountID>(pxCache9, (object) soOrder6, num24 != 0);
            PXCache pxCache10 = cache;
            nullable1 = doc.IsPaymentInfoEnabled;
            int num25;
            if (nullable1.GetValueOrDefault() & flag9)
            {
              nullable1 = doc.IsInvoiceOrder;
              if (nullable1.GetValueOrDefault())
              {
                nullable1 = doc.BillSeparately;
                if (nullable1.GetValueOrDefault())
                {
                  num25 = EnumerableExtensions.IsIn<string>(doc.ARDocType, "CSL", "RCS") ? 1 : 0;
                  goto label_51;
                }
              }
            }
            num25 = 0;
label_51:
            PXUIFieldAttribute.SetRequired<SOOrder.cashAccountID>(pxCache10, num25 != 0);
            PXCache pxCache11 = cache;
            SOOrder soOrder7 = doc;
            nullable1 = doc.IsCashSaleOrder;
            int num26 = nullable1.GetValueOrDefault() ? 1 : 0;
            PXUIFieldAttribute.SetEnabled<SOOrder.extRefNbr>(pxCache11, (object) soOrder7, num26 != 0);
            PXCache pxCache12 = cache;
            nullable1 = doc.IsCashSaleOrder;
            int num27;
            if (nullable1.GetValueOrDefault())
            {
              nullable1 = doc.IsInvoiceOrder;
              if (nullable1.GetValueOrDefault())
              {
                nullable1 = doc.BillSeparately;
                if (nullable1.GetValueOrDefault() && !flag2)
                {
                  nullable1 = ((PXSelectBase<ARSetup>) this.arsetup).Current.RequireExtRef;
                  num27 = nullable1.GetValueOrDefault() ? 1 : 0;
                  goto label_56;
                }
              }
            }
            num27 = 0;
label_56:
            PXUIFieldAttribute.SetRequired<SOOrder.extRefNbr>(pxCache12, num27 != 0);
            if (flag7 && doc.InvoiceDate.HasValue)
              OpenPeriodAttribute.SetValidatePeriod<SOOrder.finPeriodID>(cache, (object) doc, PeriodValidation.DefaultSelectUpdate);
            else
              OpenPeriodAttribute.SetValidatePeriod<SOOrder.finPeriodID>(cache, (object) doc, PeriodValidation.Nothing);
            bool flag11 = ((PXSelectBase<SOOrderType>) this.soordertype).Current.ARDocType == "CRM";
            PXUIFieldAttribute.SetEnabled<SOOrder.dueDate>(cache, (object) doc, flag7 && (!flag11 || flag11 && doc.TermsID != null));
            PXCache pxCache13 = cache;
            nullable1 = doc.BillSeparately;
            int num28 = !nullable1.GetValueOrDefault() ? 0 : (((PXSelectBase<SOOrderType>) this.soordertype).Current.ARDocType == "CSL" || ((PXSelectBase<SOOrderType>) this.soordertype).Current.ARDocType == "RCS" || ((PXSelectBase<SOOrderType>) this.soordertype).Current.ARDocType == "INV" ? 1 : (!flag11 ? 0 : (doc.TermsID != null ? 1 : 0)));
            PXUIFieldAttribute.SetRequired<SOOrder.dueDate>(pxCache13, num28 != 0);
            PXUIFieldAttribute.SetEnabled<SOOrder.discDate>(cache, (object) doc, flag7 && (!flag11 || flag11 && doc.TermsID != null));
            PXCache pxCache14 = cache;
            nullable1 = doc.BillSeparately;
            int num29 = !nullable1.GetValueOrDefault() ? 0 : (((PXSelectBase<SOOrderType>) this.soordertype).Current.ARDocType == "CSL" || ((PXSelectBase<SOOrderType>) this.soordertype).Current.ARDocType == "RCS" || ((PXSelectBase<SOOrderType>) this.soordertype).Current.ARDocType == "INV" ? 1 : (!flag11 ? 0 : (doc.TermsID != null ? 1 : 0)));
            PXUIFieldAttribute.SetRequired<SOOrder.discDate>(pxCache14, num29 != 0);
            cache.GetStatus((object) doc);
          }
          cache.AllowUpdate = true;
          cache.AllowDelete = doc.Status != "B";
          ((PXSelectBase) this.Transactions).Cache.AllowDelete = true;
          ((PXSelectBase) this.Transactions).Cache.AllowUpdate = true;
          PXCache cache5 = ((PXSelectBase) this.Transactions).Cache;
          nullable2 = doc.CustomerID;
          int num30;
          if (nullable2.HasValue)
          {
            nullable2 = doc.CustomerLocationID;
            if (nullable2.HasValue)
            {
              nullable2 = doc.ProjectID;
              num30 = nullable2.HasValue ? 1 : (!ProjectAttribute.IsPMVisible("SO") ? 1 : 0);
              goto label_64;
            }
          }
          num30 = 0;
label_64:
          cache5.AllowInsert = num30 != 0;
          PXUIFieldAttribute.SetEnabled<SOOrder.curyDiscTot>(cache, (object) doc, !PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>());
          PXUIFieldAttribute.SetEnabled<SOOrder.curyPremiumFreightAmt>(cache, (object) doc, valueOrDefault);
          ((PXSelectBase) this.Taxes).Cache.AllowUpdate = true;
          ((PXSelectBase) this.SalesPerTran).Cache.AllowUpdate = true;
          nullable1 = ((PXSelectBase<SOOrderType>) this.soordertype).Current.CanHavePayments;
          int num31;
          if (!nullable1.GetValueOrDefault())
          {
            nullable1 = ((PXSelectBase<SOOrderType>) this.soordertype).Current.CanHaveRefunds;
            if (!nullable1.GetValueOrDefault())
            {
              num31 = 0;
              goto label_68;
            }
          }
          num31 = doc.Behavior != "MO" ? 1 : 0;
label_68:
          bool flag12 = num31 != 0;
          ((PXSelectBase) this.Adjustments).Cache.AllowInsert = this.IsAddingPaymentsAllowed(doc, ((PXSelectBase<SOOrderType>) this.soordertype).Current) && doc.Behavior != "MO";
          ((PXSelectBase) this.Adjustments).Cache.AllowDelete = flag12;
          ((PXSelectBase) this.Adjustments).Cache.AllowUpdate = flag12;
          ((PXSelectBase) this.DiscountDetails).Cache.AllowDelete = true;
          ((PXSelectBase) this.DiscountDetails).Cache.AllowUpdate = !this.DisableGroupDocDiscount;
          ((PXSelectBase) this.DiscountDetails).Cache.AllowInsert = !this.DisableGroupDocDiscount;
          goto label_69;
        }
      }
    }
    PXUIFieldAttribute.SetEnabled(cache, (object) doc, false);
    cache.AllowDelete = false;
    cache.AllowUpdate = flag6;
    ((PXSelectBase) this.Transactions).Cache.AllowDelete = false;
    ((PXSelectBase) this.Transactions).Cache.AllowUpdate = false;
    ((PXSelectBase) this.Transactions).Cache.AllowInsert = false;
    ((PXGraph) this).Caches.SubscribeCacheCreated(((PXSelectBase<SOAdjust>) this.Adjustments).GetItemType(), (System.Action) (() =>
    {
      ((PXSelectBase) this.Adjustments).Cache.AllowInsert = false;
      ((PXSelectBase) this.Adjustments).Cache.AllowUpdate = false;
      ((PXSelectBase) this.Adjustments).Cache.AllowDelete = false;
    }));
    ((PXSelectBase) this.DiscountDetails).Cache.AllowDelete = false;
    ((PXSelectBase) this.DiscountDetails).Cache.AllowUpdate = false;
    ((PXSelectBase) this.DiscountDetails).Cache.AllowInsert = false;
    ((PXSelectBase) this.Taxes).Cache.AllowUpdate = false;
    ((PXSelectBase) this.SalesPerTran).Cache.AllowUpdate = false;
label_69:
    ((PXSelectBase) this.splits).Cache.AllowInsert = ((PXSelectBase) this.Transactions).Cache.AllowInsert;
    ((PXSelectBase) this.splits).Cache.AllowUpdate = ((PXSelectBase) this.Transactions).Cache.AllowUpdate;
    ((PXSelectBase) this.splits).Cache.AllowDelete = ((PXSelectBase) this.Transactions).Cache.AllowDelete;
    PXUIFieldAttribute.SetEnabled<SOOrder.orderType>(cache, (object) doc);
    PXUIFieldAttribute.SetEnabled<SOOrder.orderNbr>(cache, (object) doc);
    PXCache cache6 = ((PXSelectBase) this.Transactions).Cache;
    nullable1 = doc.IsCreditMemoOrder;
    int num32;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = doc.IsRMAOrder;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = doc.IsMixedOrder;
        num32 = nullable1.GetValueOrDefault() ? 1 : 0;
        goto label_73;
      }
    }
    num32 = 1;
label_73:
    PXUIFieldAttribute.SetVisible<SOLine.invoiceType>(cache6, (object) null, num32 != 0);
    PXCache cache7 = ((PXSelectBase) this.Transactions).Cache;
    nullable1 = doc.IsCreditMemoOrder;
    int num33;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = doc.IsRMAOrder;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = doc.IsMixedOrder;
        num33 = nullable1.GetValueOrDefault() ? 1 : 0;
        goto label_77;
      }
    }
    num33 = 1;
label_77:
    PXUIFieldAttribute.SetVisible<SOLine.invoiceNbr>(cache7, (object) null, num33 != 0);
    PXUIFieldAttribute.SetEnabled<SOLine.reasonCode>(((PXSelectBase) this.Transactions).Cache, (object) null, true);
    ((PXSelectBase) this.Taxes).Cache.AllowDelete = ((PXSelectBase) this.Transactions).Cache.AllowDelete;
    ((PXSelectBase) this.Taxes).Cache.AllowInsert = ((PXSelectBase) this.Transactions).Cache.AllowInsert;
    PXNoteAttribute.SetTextFilesActivitiesRequired<SOLine.noteID>(((PXSelectBase) this.Transactions).Cache, (object) null, true, true, false);
    PXCache cache8 = ((PXSelectBase) this.Transactions).Cache;
    nullable1 = doc.IsTransferOrder;
    int num34 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOLine.branchID>(cache8, (object) null, num34 != 0);
    PXCache cache9 = ((PXSelectBase) this.Transactions).Cache;
    nullable1 = doc.IsTransferOrder;
    int num35 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SOLine.branchID>(cache9, (object) null, num35 != 0);
    PXCache cache10 = ((PXSelectBase) this.Transactions).Cache;
    nullable1 = doc.IsTransferOrder;
    int num36 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOLine.curyLineAmt>(cache10, (object) null, num36 != 0);
    PXCache cache11 = ((PXSelectBase) this.Transactions).Cache;
    nullable1 = doc.IsTransferOrder;
    int num37 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOLine.curyUnitPrice>(cache11, (object) null, num37 != 0);
    PXCache cache12 = ((PXSelectBase) this.Transactions).Cache;
    nullable1 = doc.IsTransferOrder;
    int num38 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOLine.curyExtCost>(cache12, (object) null, num38 != 0);
    PXCache cache13 = ((PXSelectBase) this.Transactions).Cache;
    nullable1 = doc.IsTransferOrder;
    int num39 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOLine.curyExtPrice>(cache13, (object) null, num39 != 0);
    PXCache cache14 = ((PXSelectBase) this.Transactions).Cache;
    nullable1 = doc.IsTransferOrder;
    int num40 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOLine.discPct>(cache14, (object) null, num40 != 0);
    PXCache cache15 = ((PXSelectBase) this.Transactions).Cache;
    nullable1 = doc.IsTransferOrder;
    int num41 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOLine.discAmt>(cache15, (object) null, num41 != 0);
    PXCache cache16 = ((PXSelectBase) this.Transactions).Cache;
    nullable1 = doc.IsTransferOrder;
    int num42 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOLine.curyDiscAmt>(cache16, (object) null, num42 != 0);
    PXCache cache17 = ((PXSelectBase) this.Transactions).Cache;
    nullable1 = doc.IsTransferOrder;
    int num43 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOLine.curyDiscPrice>(cache17, (object) null, num43 != 0);
    PXCache cache18 = ((PXSelectBase) this.Transactions).Cache;
    nullable1 = doc.IsTransferOrder;
    int num44 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOLine.manualDisc>(cache18, (object) null, num44 != 0);
    PXCache cache19 = ((PXSelectBase) this.Transactions).Cache;
    nullable1 = doc.IsTransferOrder;
    int num45 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOLine.discountID>(cache19, (object) null, num45 != 0);
    PXCache cache20 = ((PXSelectBase) this.Transactions).Cache;
    nullable1 = doc.IsTransferOrder;
    int num46 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SOLine.discountID>(cache20, (object) null, num46 != 0);
    PXCache cache21 = ((PXSelectBase) this.Transactions).Cache;
    nullable1 = doc.IsTransferOrder;
    int num47 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOLine.curyUnbilledAmt>(cache21, (object) null, num47 != 0);
    PXCache cache22 = ((PXSelectBase) this.Transactions).Cache;
    nullable1 = doc.IsTransferOrder;
    int num48 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOLine.salesPersonID>(cache22, (object) null, num48 != 0);
    PXCache cache23 = ((PXSelectBase) this.Transactions).Cache;
    nullable1 = doc.IsTransferOrder;
    int num49 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOLine.taxCategoryID>(cache23, (object) null, num49 != 0);
    PXCache cache24 = ((PXSelectBase) this.Transactions).Cache;
    nullable1 = doc.IsTransferOrder;
    int num50 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOLine.commissionable>(cache24, (object) null, num50 != 0);
    PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.Transactions).Cache, (object) null).For<SOLine.origOrderType>((Action<PXUIFieldAttribute>) (a => a.Visible = EnumerableExtensions.IsIn<string>(doc.Behavior, "RM", "CM", "MO", "IN"))).SameFor<SOLine.origOrderNbr>();
    if (((PXSelectBase<SOOrderType>) this.soordertype).Current != null)
    {
      PXCache pxCache15 = cache;
      object row = e.Row;
      nullable1 = ((PXSelectBase<SOOrderType>) this.soordertype).Current.RequireControlTotal;
      int num51 = nullable1.GetValueOrDefault() ? 1 : 0;
      PXUIFieldAttribute.SetVisible<SOOrder.curyControlTotal>(pxCache15, row, num51 != 0);
    }
    nullable1 = ((PXSelectBase<SOOrderType>) this.soordertype).Current.RequireLocation;
    if (nullable1.GetValueOrDefault())
      PXStringListAttribute.SetList<SOLine.shipComplete>(((PXSelectBase) this.Transactions).Cache, (object) null, new string[2]
      {
        "L",
        "C"
      }, new string[2]
      {
        "Cancel Remainder",
        "Ship Complete"
      });
    else
      PXStringListAttribute.SetList<SOLine.shipComplete>(((PXSelectBase) this.Transactions).Cache, (object) null, new string[3]
      {
        "B",
        "L",
        "C"
      }, new string[3]
      {
        "Back Order Allowed",
        "Cancel Remainder",
        "Ship Complete"
      });
    PXCache pxCache16 = cache;
    object row1 = e.Row;
    nullable1 = doc.IsTransferOrder;
    int num52 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.destinationSiteID>(pxCache16, row1, num52 != 0);
    PXCache pxCache17 = cache;
    object row2 = e.Row;
    nullable1 = doc.IsTransferOrder;
    int num53 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.customerOrderNbr>(pxCache17, row2, num53 != 0);
    PXCache cache25 = ((PXSelectBase) this.Packages).Cache;
    nullable1 = ((SOOrder) e.Row).IsManualPackage;
    int num54 = nullable1.GetValueOrDefault() ? 1 : 0;
    cache25.AllowInsert = num54 != 0;
    PXCache cache26 = ((PXSelectBase) this.Packages).Cache;
    nullable1 = ((SOOrder) e.Row).IsManualPackage;
    int num55 = nullable1.GetValueOrDefault() ? 1 : 0;
    cache26.AllowDelete = num55 != 0;
    PXCache cache27 = ((PXSelectBase) this.Packages).Cache;
    nullable1 = ((SOOrder) e.Row).IsManualPackage;
    int num56 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SOPackageInfo.inventoryID>(cache27, (object) null, num56 != 0);
    PXCache cache28 = ((PXSelectBase) this.Packages).Cache;
    nullable1 = ((SOOrder) e.Row).IsManualPackage;
    int num57 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SOPackageInfo.boxID>(cache28, (object) null, num57 != 0);
    PXCache cache29 = ((PXSelectBase) this.Packages).Cache;
    nullable1 = ((SOOrder) e.Row).IsManualPackage;
    int num58 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SOPackageInfo.declaredValue>(cache29, (object) null, num58 != 0);
    PXCache cache30 = ((PXSelectBase) this.Packages).Cache;
    nullable1 = ((SOOrder) e.Row).IsManualPackage;
    int num59 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SOPackageInfo.length>(cache30, (object) null, num59 != 0);
    PXCache cache31 = ((PXSelectBase) this.Packages).Cache;
    nullable1 = ((SOOrder) e.Row).IsManualPackage;
    int num60 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SOPackageInfo.width>(cache31, (object) null, num60 != 0);
    PXCache cache32 = ((PXSelectBase) this.Packages).Cache;
    nullable1 = ((SOOrder) e.Row).IsManualPackage;
    int num61 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SOPackageInfo.height>(cache32, (object) null, num61 != 0);
    if (!string.IsNullOrEmpty(((SOOrder) e.Row).ShipVia))
    {
      PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, ((SOOrder) e.Row).ShipVia);
      if (carrier != null)
      {
        PXUIFieldAttribute.SetVisible<SOPackageInfo.declaredValue>(((PXSelectBase) this.Packages).Cache, (object) null, carrier.PluginMethod != null);
        PXUIFieldAttribute.SetVisible<SOPackageInfo.cOD>(((PXSelectBase) this.Packages).Cache, (object) null, carrier.PluginMethod != null);
        PXUIFieldAttribute.SetEnabled<SOOrder.curyFreightCost>(cache, (object) doc, valueOrDefault && carrier.CalcMethod == "M");
      }
      string errorOnly = PXUIFieldAttribute.GetErrorOnly<SOOrder.curyFreightCost>(cache, (object) doc);
      if (carrier != null)
      {
        nullable1 = carrier.IsExternal;
        if (nullable1.GetValueOrDefault() && string.IsNullOrEmpty(errorOnly))
        {
          PXCache pxCache18 = cache;
          object row3 = e.Row;
          nullable1 = doc.FreightCostIsValid;
          bool flag13 = false;
          string str = nullable1.GetValueOrDefault() == flag13 & nullable1.HasValue ? "The freight cost is not up to date." : (string) null;
          PXUIFieldAttribute.SetWarning<SOOrder.curyFreightCost>(pxCache18, row3, str);
        }
      }
    }
    cache.RaiseExceptionHandling<SOOrder.shipVia>((object) doc, (object) doc.ShipVia, (Exception) this.BuildShipViaException(doc));
    PXCache pxCache19 = cache;
    object row4 = e.Row;
    nullable1 = ((SOOrder) e.Row).OverrideTaxZone;
    int num62 = nullable1.GetValueOrDefault() ? 1 : (((PXGraph) this).IsContractBasedAPI ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<SOOrder.taxZoneID>(pxCache19, row4, num62 != 0);
    if (!((PXGraph) this).UnattendedMode)
    {
      PXAction<SOOrder> validateAddresses = this.validateAddresses;
      nullable1 = doc.Completed;
      bool flag14 = false;
      int num63;
      if (nullable1.GetValueOrDefault() == flag14 & nullable1.HasValue)
      {
        nullable1 = doc.Cancelled;
        bool flag15 = false;
        if (nullable1.GetValueOrDefault() == flag15 & nullable1.HasValue)
        {
          num63 = ((PXGraph) this).FindAllImplementations<IAddressValidationHelper>().RequiresValidation() ? 1 : 0;
          goto label_93;
        }
      }
      num63 = 0;
label_93:
      ((PXAction) validateAddresses).SetEnabled(num63 != 0);
    }
    PXUIFieldAttribute.SetVisible<SOOrder.groundCollect>(cache, (object) doc, this.CanUseGroundCollect(doc));
    PXCache pxCache20 = cache;
    SOOrder soOrder8 = doc;
    nullable1 = doc.IsTransferOrder;
    int num64 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SOOrder.customerID>(pxCache20, (object) soOrder8, num64 != 0);
    PXCache pxCache21 = cache;
    SOOrder soOrder9 = doc;
    nullable1 = doc.IsTransferOrder;
    int num65 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SOOrder.customerLocationID>(pxCache21, (object) soOrder9, num65 != 0);
    PXCache pxCache22 = cache;
    SOOrder soOrder10 = doc;
    nullable1 = doc.IsTransferOrder;
    int num66 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SOOrder.contactID>(pxCache22, (object) soOrder10, num66 != 0);
    PXCache pxCache23 = cache;
    SOOrder soOrder11 = doc;
    nullable1 = doc.IsTransferOrder;
    int num67 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SOOrder.salesPersonID>(pxCache23, (object) soOrder11, num67 != 0);
    PXCache pxCache24 = cache;
    SOOrder soOrder12 = doc;
    nullable1 = doc.IsTransferOrder;
    int num68 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.customerID>(pxCache24, (object) soOrder12, num68 != 0);
    PXCache pxCache25 = cache;
    SOOrder soOrder13 = doc;
    nullable1 = doc.IsTransferOrder;
    int num69 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.customerLocationID>(pxCache25, (object) soOrder13, num69 != 0);
    PXCache pxCache26 = cache;
    SOOrder soOrder14 = doc;
    nullable1 = doc.IsTransferOrder;
    int num70 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.contactID>(pxCache26, (object) soOrder14, num70 != 0);
    PXCache pxCache27 = cache;
    SOOrder soOrder15 = doc;
    nullable1 = doc.IsTransferOrder;
    int num71 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyVatExemptTotal>(pxCache27, (object) soOrder15, num71 != 0);
    PXCache pxCache28 = cache;
    SOOrder soOrder16 = doc;
    nullable1 = doc.IsTransferOrder;
    int num72 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyVatTaxableTotal>(pxCache28, (object) soOrder16, num72 != 0);
    PXCache pxCache29 = cache;
    SOOrder soOrder17 = doc;
    nullable1 = doc.IsTransferOrder;
    int num73 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyTaxTotal>(pxCache29, (object) soOrder17, num73 != 0);
    PXCache pxCache30 = cache;
    SOOrder soOrder18 = doc;
    nullable1 = doc.IsTransferOrder;
    int num74 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyOrderTotal>(pxCache30, (object) soOrder18, num74 != 0);
    PXCache pxCache31 = cache;
    SOOrder soOrder19 = doc;
    nullable1 = doc.IsTransferOrder;
    int num75 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.taxZoneID>(pxCache31, (object) soOrder19, num75 != 0);
    PXCache pxCache32 = cache;
    SOOrder soOrder20 = doc;
    nullable1 = doc.IsTransferOrder;
    int num76 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.overrideTaxZone>(pxCache32, (object) soOrder20, num76 != 0);
    PXCache pxCache33 = cache;
    SOOrder soOrder21 = doc;
    nullable1 = doc.IsTransferOrder;
    int num77 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.externalTaxExemptionNumber>(pxCache33, (object) soOrder21, num77 != 0);
    PXCache pxCache34 = cache;
    SOOrder soOrder22 = doc;
    nullable1 = doc.IsTransferOrder;
    int num78 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.avalaraCustomerUsageType>(pxCache34, (object) soOrder22, num78 != 0);
    PXCache pxCache35 = cache;
    SOOrder soOrder23 = doc;
    nullable1 = doc.IsTransferOrder;
    int num79 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.billSeparately>(pxCache35, (object) soOrder23, num79 != 0);
    PXCache pxCache36 = cache;
    SOOrder soOrder24 = doc;
    nullable1 = doc.IsTransferOrder;
    int num80 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.invoiceNbr>(pxCache36, (object) soOrder24, num80 != 0);
    PXCache pxCache37 = cache;
    SOOrder soOrder25 = doc;
    nullable1 = doc.IsTransferOrder;
    int num81 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.invoiceDate>(pxCache37, (object) soOrder25, num81 != 0);
    PXCache pxCache38 = cache;
    SOOrder soOrder26 = doc;
    nullable1 = doc.IsTransferOrder;
    int num82 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.termsID>(pxCache38, (object) soOrder26, num82 != 0);
    PXCache pxCache39 = cache;
    SOOrder soOrder27 = doc;
    nullable1 = doc.IsTransferOrder;
    int num83 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.dueDate>(pxCache39, (object) soOrder27, num83 != 0);
    PXCache pxCache40 = cache;
    SOOrder soOrder28 = doc;
    nullable1 = doc.IsTransferOrder;
    int num84 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.discDate>(pxCache40, (object) soOrder28, num84 != 0);
    PXCache pxCache41 = cache;
    SOOrder soOrder29 = doc;
    nullable1 = doc.IsTransferOrder;
    int num85 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.finPeriodID>(pxCache41, (object) soOrder29, num85 != 0);
    PXCache pxCache42 = cache;
    SOOrder soOrder30 = doc;
    nullable1 = doc.IsTransferOrder;
    int num86 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.salesPersonID>(pxCache42, (object) soOrder30, num86 != 0);
    PXCache pxCache43 = cache;
    SOOrder soOrder31 = doc;
    nullable1 = doc.IsTransferOrder;
    int num87 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyLineTotal>(pxCache43, (object) soOrder31, num87 != 0);
    PXCache pxCache44 = cache;
    SOOrder soOrder32 = doc;
    nullable1 = doc.IsTransferOrder;
    int num88 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyGoodsExtPriceTotal>(pxCache44, (object) soOrder32, num88 != 0);
    PXCache pxCache45 = cache;
    SOOrder soOrder33 = doc;
    nullable1 = doc.IsTransferOrder;
    int num89 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyMiscTot>(pxCache45, (object) soOrder33, num89 != 0);
    PXCache pxCache46 = cache;
    SOOrder soOrder34 = doc;
    nullable1 = doc.IsTransferOrder;
    int num90 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyMiscExtPriceTotal>(pxCache46, (object) soOrder34, num90 != 0);
    PXCache pxCache47 = cache;
    SOOrder soOrder35 = doc;
    nullable1 = doc.IsFreightAvailable;
    int num91 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyFreightCost>(pxCache47, (object) soOrder35, num91 != 0);
    PXCache pxCache48 = cache;
    SOOrder soOrder36 = doc;
    nullable1 = doc.IsFreightAvailable;
    int num92 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.freightCostIsValid>(pxCache48, (object) soOrder36, num92 != 0);
    PXCache pxCache49 = cache;
    SOOrder soOrder37 = doc;
    nullable1 = doc.IsFreightAvailable;
    int num93 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.overrideFreightAmount>(pxCache49, (object) soOrder37, num93 != 0);
    PXCache pxCache50 = cache;
    SOOrder soOrder38 = doc;
    nullable1 = doc.IsFreightAvailable;
    int num94 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.freightAmountSource>(pxCache50, (object) soOrder38, num94 != 0);
    PXCache pxCache51 = cache;
    SOOrder soOrder39 = doc;
    nullable1 = doc.IsFreightAvailable;
    int num95 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyFreightAmt>(pxCache51, (object) soOrder39, num95 != 0);
    PXCache pxCache52 = cache;
    SOOrder soOrder40 = doc;
    nullable1 = doc.IsFreightAvailable;
    int num96 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyPremiumFreightAmt>(pxCache52, (object) soOrder40, num96 != 0);
    PXCache pxCache53 = cache;
    SOOrder soOrder41 = doc;
    nullable1 = doc.IsFreightAvailable;
    int num97 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.freightTaxCategoryID>(pxCache53, (object) soOrder41, num97 != 0);
    PXCache pxCache54 = cache;
    SOOrder soOrder42 = doc;
    nullable1 = doc.IsTransferOrder;
    int num98 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyOpenOrderTotal>(pxCache54, (object) soOrder42, num98 != 0);
    PXCache pxCache55 = cache;
    SOOrder soOrder43 = doc;
    nullable1 = doc.IsTransferOrder;
    int num99 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.unbilledOrderQty>(pxCache55, (object) soOrder43, num99 != 0);
    PXCache pxCache56 = cache;
    SOOrder soOrder44 = doc;
    nullable1 = doc.IsTransferOrder;
    int num100 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyUnbilledOrderTotal>(pxCache56, (object) soOrder44, num100 != 0);
    PXCache pxCache57 = cache;
    SOOrder soOrder45 = doc;
    nullable1 = doc.IsTransferOrder;
    int num101;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = doc.IsCashSaleOrder;
      num101 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num101 = 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyUnreleasedPaymentAmt>(pxCache57, (object) soOrder45, num101 != 0);
    PXCache pxCache58 = cache;
    SOOrder soOrder46 = doc;
    nullable1 = doc.IsTransferOrder;
    int num102;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = doc.IsCashSaleOrder;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = ((PXSelectBase<SOOrderType>) this.soordertype).Current.CanHaveRefunds;
        num102 = !nullable1.GetValueOrDefault() ? 1 : 0;
        goto label_101;
      }
    }
    num102 = 0;
label_101:
    PXUIFieldAttribute.SetVisible<SOOrder.curyCCAuthorizedAmt>(pxCache58, (object) soOrder46, num102 != 0);
    PXCache pxCache59 = cache;
    SOOrder soOrder47 = doc;
    nullable1 = doc.IsTransferOrder;
    int num103;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = doc.IsCashSaleOrder;
      num103 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num103 = 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyPaidAmt>(pxCache59, (object) soOrder47, num103 != 0);
    PXCache pxCache60 = cache;
    SOOrder soOrder48 = doc;
    nullable1 = doc.IsTransferOrder;
    int num104;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = doc.IsCashSaleOrder;
      num104 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num104 = 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyBilledPaymentTotal>(pxCache60, (object) soOrder48, num104 != 0);
    PXCache pxCache61 = cache;
    SOOrder soOrder49 = doc;
    nullable1 = doc.IsTransferOrder;
    int num105 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyPaymentTotal>(pxCache61, (object) soOrder49, num105 != 0);
    PXCache pxCache62 = cache;
    SOOrder soOrder50 = doc;
    nullable1 = doc.IsTransferOrder;
    int num106 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyUnpaidBalance>(pxCache62, (object) soOrder50, num106 != 0);
    PXCache pxCache63 = cache;
    SOOrder soOrder51 = doc;
    nullable1 = doc.IsTransferOrder;
    int num107 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.disableAutomaticTaxCalculation>(pxCache63, (object) soOrder51, num107 != 0);
    PXCache pxCache64 = cache;
    SOOrder soOrder52 = doc;
    nullable1 = doc.IsTransferOrder;
    int num108 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyLineDiscTotal>(pxCache64, (object) soOrder52, num108 != 0);
    PXCache pxCache65 = cache;
    SOOrder soOrder53 = doc;
    nullable1 = doc.IsTransferOrder;
    int num109 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyDiscTot>(pxCache65, (object) soOrder53, num109 != 0);
    PXCache pxCache66 = cache;
    SOOrder soOrder54 = doc;
    nullable1 = doc.IsTransferOrder;
    int num110 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyFreightTot>(pxCache66, (object) soOrder54, num110 != 0);
    PXCache pxCache67 = cache;
    SOOrder soOrder55 = doc;
    nullable1 = doc.IsTransferOrder;
    int num111 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.curyDetailExtPriceTotal>(pxCache67, (object) soOrder55, num111 != 0);
    PXAction<SOOrder> calculateFreight = this.calculateFreight;
    nullable1 = doc.IsFreightAvailable;
    int num112 = nullable1.GetValueOrDefault() ? 1 : 0;
    ((PXAction) calculateFreight).SetVisible(num112 != 0);
    PXView view1 = ((PXSelectBase) this.Taxes).View;
    nullable1 = doc.IsTransferOrder;
    int num113 = !nullable1.GetValueOrDefault() ? 1 : 0;
    view1.AllowSelect = num113 != 0;
    PXView view2 = ((PXSelectBase) this.DiscountDetails).View;
    nullable1 = doc.IsTransferOrder;
    int num114 = !nullable1.GetValueOrDefault() ? 1 : 0;
    view2.AllowSelect = num114 != 0;
    PXView view3 = ((PXSelectBase) this.Adjustments).View;
    nullable1 = doc.IsTransferOrder;
    int num115;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = doc.IsCashSaleOrder;
      num115 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num115 = 0;
    view3.AllowSelect = num115 != 0;
    PXSelect<SOSalesPerTran, Where<SOSalesPerTran.orderType, Equal<Current<SOOrder.orderType>>, And<SOSalesPerTran.orderNbr, Equal<Current<SOOrder.orderNbr>>>>> salesPerTran1 = this.SalesPerTran;
    nullable1 = doc.IsTransferOrder;
    int num116 = !nullable1.GetValueOrDefault() ? 1 : 0;
    ((PXSelectBase) salesPerTran1).AllowSelect = num116 != 0;
    PXSelect<SOSalesPerTran, Where<SOSalesPerTran.orderType, Equal<Current<SOOrder.orderType>>, And<SOSalesPerTran.orderNbr, Equal<Current<SOOrder.orderNbr>>>>> salesPerTran2 = this.SalesPerTran;
    nullable1 = doc.IsTransferOrder;
    int num117 = !nullable1.GetValueOrDefault() ? 1 : 0;
    ((PXSelectBase) salesPerTran2).AllowInsert = num117 != 0;
    PXCache pxCache68 = cache;
    SOOrder soOrder56 = doc;
    nullable1 = doc.IsTransferOrder;
    int num118 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.paymentMethodID>(pxCache68, (object) soOrder56, num118 != 0);
    PXCache pxCache69 = cache;
    SOOrder soOrder57 = doc;
    nullable1 = doc.IsTransferOrder;
    int num119 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.pMInstanceID>(pxCache69, (object) soOrder57, num119 != 0);
    PXCache pxCache70 = cache;
    SOOrder soOrder58 = doc;
    nullable1 = doc.IsTransferOrder;
    int num120 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.cashAccountID>(pxCache70, (object) soOrder58, num120 != 0);
    PXCache pxCache71 = cache;
    SOOrder soOrder59 = doc;
    nullable1 = doc.IsTransferOrder;
    int num121;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = doc.IsInvoiceOrder;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = doc.IsCashSaleOrder;
        num121 = nullable1.GetValueOrDefault() ? 1 : 0;
        goto label_114;
      }
    }
    num121 = 0;
label_114:
    PXUIFieldAttribute.SetVisible<SOOrder.extRefNbr>(pxCache71, (object) soOrder59, num121 != 0);
    PXUIFieldAttribute.SetRequired<SOOrder.termsID>(cache, doc.ARDocType != "UND" && doc.ARDocType != "CRM");
    PXCache pxCache72 = cache;
    SOOrder soOrder60 = doc;
    nullable1 = doc.DontApprove;
    int num122 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOOrder.approved>(pxCache72, (object) soOrder60, num122 != 0);
    SOOrderApprovalAutomation approval = this.Approval;
    nullable1 = doc.DontApprove;
    int num123 = !nullable1.GetValueOrDefault() ? 1 : 0;
    ((PXSelectBase) approval).AllowSelect = num123 != 0;
    nullable1 = doc.Hold;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = doc.DontApprove;
      if (!nullable1.GetValueOrDefault())
      {
        PXUIFieldAttribute.SetEnabled(cache, (object) doc, false);
        PXUIFieldAttribute.SetEnabled<SOOrder.orderType>(cache, (object) doc, true);
        PXUIFieldAttribute.SetEnabled<SOOrder.orderNbr>(cache, (object) doc, true);
        ((PXSelectBase) this.Transactions).Cache.AllowDelete = false;
        ((PXSelectBase) this.Transactions).Cache.AllowUpdate = false;
        ((PXSelectBase) this.Transactions).Cache.AllowInsert = false;
        ((PXSelectBase) this.splits).Cache.AllowDelete = false;
        ((PXSelectBase) this.splits).Cache.AllowUpdate = false;
        ((PXSelectBase) this.splits).Cache.AllowInsert = false;
        ((PXSelectBase) this.Adjustments).Cache.AllowInsert = false;
        ((PXSelectBase) this.Adjustments).Cache.AllowUpdate = false;
        ((PXSelectBase) this.Adjustments).Cache.AllowDelete = false;
        ((PXSelectBase) this.DiscountDetails).Cache.AllowDelete = false;
        ((PXSelectBase) this.DiscountDetails).Cache.AllowUpdate = false;
        ((PXSelectBase) this.DiscountDetails).Cache.AllowInsert = false;
        ((PXSelectBase) this.Taxes).Cache.AllowInsert = false;
        ((PXSelectBase) this.Taxes).Cache.AllowUpdate = false;
        ((PXSelectBase) this.Taxes).Cache.AllowDelete = false;
        ((PXSelectBase) this.SalesPerTran).Cache.AllowUpdate = false;
      }
    }
    nullable1 = doc.DisableAutomaticTaxCalculation;
    if (nullable1.GetValueOrDefault())
    {
      nullable2 = doc.BilledCntr;
      int num124 = 0;
      if (!(nullable2.GetValueOrDefault() > num124 & nullable2.HasValue))
      {
        nullable2 = doc.ReleasedCntr;
        int num125 = 0;
        if (!(nullable2.GetValueOrDefault() > num125 & nullable2.HasValue))
          goto label_121;
      }
      ((PXSelectBase) this.Taxes).Cache.AllowInsert = false;
      ((PXSelectBase) this.Taxes).Cache.AllowUpdate = false;
      ((PXSelectBase) this.Taxes).Cache.AllowDelete = false;
    }
label_121:
    PXAction<SOOrder> addInvoice = this.addInvoice;
    nullable1 = doc.IsCreditMemoOrder;
    int num126;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = doc.IsRMAOrder;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = doc.IsMixedOrder;
        if (!nullable1.GetValueOrDefault())
        {
          num126 = 0;
          goto label_126;
        }
      }
    }
    num126 = ((PXSelectBase) this.Transactions).Cache.AllowInsert ? 1 : 0;
label_126:
    ((PXAction) addInvoice).SetEnabled(num126 != 0);
    if (((PXSelectBase<SOOrderType>) this.soordertype).Current != null)
    {
      nullable1 = ((PXSelectBase<SOOrderType>) this.soordertype).Current.Active;
      if (nullable1.GetValueOrDefault())
        goto label_129;
    }
    this.SetReadOnly(true);
    cache.RaiseExceptionHandling<SOOrder.orderType>((object) doc, (object) doc.OrderType, (Exception) new PXSetPropertyException("Order Type is inactive.", (PXErrorLevel) 2));
label_129:
    if (!PXPreserveScope.IsScoped() && PXLongOperation.GetStatus(((PXGraph) this).UID) == 1 && !((PXGraph) this).IsImportFromExcel)
      this.SetReadOnly(true);
    SOOrder soOrder61 = doc;
    int num127;
    if (soOrder61 == null)
    {
      num127 = 0;
    }
    else
    {
      nullable1 = soOrder61.IsTransferOrder;
      num127 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    if (num127 != 0 && PXUIFieldAttribute.GetError<SOOrder.destinationSiteID>(cache, e.Row) == null)
    {
      string siteIdErrorMessage = doc.DestinationSiteIdErrorMessage;
      if (!string.IsNullOrWhiteSpace(siteIdErrorMessage))
      {
        if (((PXGraph) this).UnattendedMode)
          throw new PXSetPropertyException(siteIdErrorMessage, (PXErrorLevel) 4);
        cache.RaiseExceptionHandling<PX.Objects.PO.POOrder.siteID>(e.Row, cache.GetValueExt<PX.Objects.PO.POOrder.siteID>(e.Row), (Exception) new PXSetPropertyException(siteIdErrorMessage, (PXErrorLevel) 4));
      }
    }
    PXUIFieldAttribute.SetVisible<SOOrder.emailed>(cache, (object) doc, (!((IEnumerable<string>) new string[2]
    {
      "RM",
      "CM"
    }).Contains<string>(doc.Behavior) ? 1 : 0) != 0);
    nullable1 = doc.IsCashSaleOrder;
    if (nullable1.GetValueOrDefault() && doc.PaymentMethodID != null)
    {
      PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethodDef).Select(new object[1]
      {
        (object) doc.PaymentMethodID
      }));
      int num128;
      if (paymentMethod?.PaymentType == "CCD")
      {
        nullable1 = paymentMethod.IsAccountNumberRequired;
        num128 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
      else
        num128 = 0;
      bool flag16 = num128 != 0;
      PX.Objects.AR.PaymentRefAttribute.SetAllowAskUpdateLastRefNbr<SOOrder.extRefNbr>(cache, !flag16);
    }
    else
      PX.Objects.AR.PaymentRefAttribute.SetAllowAskUpdateLastRefNbr<SOOrder.extRefNbr>(cache, false);
    nullable2 = doc.OpenSiteCntr;
    int num129 = 0;
    bool flag17 = nullable2.GetValueOrDefault() > num129 & nullable2.HasValue;
    ((PXAction) this.createShipmentIssue).SetEnabled(flag17);
    ((PXAction) this.createShipmentReceipt).SetEnabled(flag17);
  }

  public virtual bool IsAddingPaymentsAllowed(SOOrder order, SOOrderType orderType)
  {
    if (orderType != null && orderType.CanHavePayments.GetValueOrDefault())
      return true;
    return orderType != null && orderType.CanHaveRefunds.GetValueOrDefault() && orderType.AllowRefundBeforeReturn.GetValueOrDefault();
  }

  protected virtual bool IsCurrencyEnabled(SOOrder order)
  {
    return ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current == null || ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.AllowOverrideCury.GetValueOrDefault();
  }

  protected virtual bool IsCuryUnitCostEnabled(SOLine line, SOOrder order)
  {
    return order.IsRMAOrder.GetValueOrDefault() || order.IsCreditMemoOrder.GetValueOrDefault() || order.IsMixedOrder.GetValueOrDefault();
  }

  protected virtual bool IsCuryUnitCostVisible(SOOrder order)
  {
    return order.IsRMAOrder.GetValueOrDefault() || order.IsCreditMemoOrder.GetValueOrDefault() || order.IsMixedOrder.GetValueOrDefault();
  }

  protected virtual PXException BuildShipViaException(SOOrder order)
  {
    if (string.IsNullOrEmpty(order.ShipVia))
      return (PXException) null;
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, order.ShipVia);
    if ((carrier != null ? (!carrier.IsExternal.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      return (PXException) null;
    CarrierPlugin plugin = CarrierPlugin.PK.Find((PXGraph) this, carrier.CarrierPluginID);
    CarrierPlugin carrierPlugin = plugin;
    if ((carrierPlugin != null ? (!carrierPlugin.SiteID.HasValue ? 1 : 0) : 1) != 0)
      return (PXException) null;
    return !GraphHelper.RowCast<SOOrderSite>((IEnumerable) ((PXSelectBase<SOOrderSite>) this.SiteList).Select(Array.Empty<object>())).All<SOOrderSite>((Func<SOOrderSite, bool>) (s =>
    {
      int? siteId1 = s.SiteID;
      int? siteId2 = plugin.SiteID;
      return siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue;
    })) ? (PXException) new PXSetPropertyException("The ship via code selected in the sales order is not applicable to the warehouses selected in sales order lines.", (PXErrorLevel) 2) : (PXException) null;
  }

  protected virtual bool AllowChangingOverrideFreightAmount(SOOrder doc)
  {
    PX.Objects.CS.ShipTerms shipTerms = PX.Objects.CS.ShipTerms.PK.Find((PXGraph) this, doc.ShipTermsID);
    if (shipTerms == null)
    {
      int? shipmentCntr = doc.ShipmentCntr;
      int num = 0;
      if (shipmentCntr.GetValueOrDefault() <= num & shipmentCntr.HasValue)
        return true;
    }
    return shipTerms?.FreightAmountSource == "O";
  }

  protected virtual void SOOrder_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (PXResultset<SOOrderShipment>.op_Implicit(PXSelectBase<SOOrderShipment, PXSelectReadonly<SOOrderShipment, Where<SOOrderShipment.orderType, Equal<Current<SOOrder.orderType>>, And<SOOrderShipment.orderNbr, Equal<Current<SOOrder.orderNbr>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) (SOOrder) e.Row
    }, Array.Empty<object>())) != null)
      throw new PXException("The sales order cannot be deleted because it has a shipment.");
    if (((PXSelectBase<SOAdjust>) this.Adjustments).Select(Array.Empty<object>()).Count <= 0 || ((PXSelectBase<SOOrder>) this.Document).Ask("Warning", "The Sales Order has a payment applied. Deleting the Sales Order will delete the payment reservation. Continue?", (MessageButtons) 1) == 1)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowDeleted<SOOrder> e)
  {
    if (e.Row == null)
      return;
    ((SelectedEntityEvent<SOOrder>) PXEntityEventBase<SOOrder>.Container<SOOrder.Events>.Select((Expression<Func<SOOrder.Events, PXEntityEvent<SOOrder.Events>>>) (ev => ev.OrderDeleted))).FireOn((PXGraph) this, e.Row);
  }

  protected virtual void RQRequisitionOrder_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    RQRequisitionOrder row = (RQRequisitionOrder) e.Row;
    if (!(PXResultset<SOOrderType>.op_Implicit(((PXSelectBase<SOOrderType>) this.soordertype).SelectWindowed(0, 1, new object[1]
    {
      (object) row.OrderType
    })).Behavior == "QT"))
      return;
    RQRequisition rqRequisition = PXParentAttribute.SelectParent<RQRequisition>(sender, (object) row);
    rqRequisition.Quoted = new bool?(false);
    GraphHelper.MarkUpdated(((PXSelectBase) this.rqrequisition).Cache, (object) rqRequisition, true);
  }

  public virtual void UpdateControlTotal(PXCache sender, PXRowUpdatedEventArgs e)
  {
    SOOrder row = e.Row as SOOrder;
    bool? completed = row.Completed;
    bool flag1 = false;
    if (!(completed.GetValueOrDefault() == flag1 & completed.HasValue))
      return;
    bool? requireControlTotal = ((PXSelectBase<SOOrderType>) this.soordertype).Current.RequireControlTotal;
    bool flag2 = false;
    if (!(requireControlTotal.GetValueOrDefault() == flag2 & requireControlTotal.HasValue))
      return;
    Decimal? curyOrderTotal = row.CuryOrderTotal;
    Decimal? nullable = row.CuryControlTotal;
    if (curyOrderTotal.GetValueOrDefault() == nullable.GetValueOrDefault() & curyOrderTotal.HasValue == nullable.HasValue)
      return;
    nullable = row.CuryOrderTotal;
    if (nullable.HasValue)
    {
      nullable = row.CuryOrderTotal;
      Decimal num = 0M;
      if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
      {
        sender.SetValueExt<SOOrder.curyControlTotal>(e.Row, (object) row.CuryOrderTotal);
        return;
      }
    }
    sender.SetValueExt<SOOrder.curyControlTotal>(e.Row, (object) 0M);
  }

  public virtual void ValidateControlTotal(PXCache sender, SOOrder row, bool isRowPersisting = false)
  {
    if (row == null)
      return;
    bool flag1 = false;
    bool? nullable1 = row.Hold;
    bool flag2 = false;
    if (nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue)
    {
      nullable1 = row.Completed;
      bool flag3 = false;
      if (nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue)
      {
        nullable1 = row.Cancelled;
        bool flag4 = false;
        if (nullable1.GetValueOrDefault() == flag4 & nullable1.HasValue)
        {
          nullable1 = ((PXSelectBase<SOOrderType>) this.soordertype).Current.RequireControlTotal;
          if (nullable1.GetValueOrDefault())
          {
            Decimal? curyOrderTotal = row.CuryOrderTotal;
            Decimal? nullable2 = row.CuryControlTotal;
            if (!(curyOrderTotal.GetValueOrDefault() == nullable2.GetValueOrDefault() & curyOrderTotal.HasValue == nullable2.HasValue))
            {
              RaiseExceptionHandling<SOOrder.curyControlTotal>(sender, row, "The document is out of the balance.");
              flag1 = true;
            }
            else
            {
              nullable2 = row.CuryOrderTotal;
              Decimal num = 0M;
              if (nullable2.GetValueOrDefault() < num & nullable2.HasValue && row.ARDocType != "UND" && EnumerableExtensions.IsNotIn<string>(row.Behavior, "RM", "MO"))
              {
                RaiseExceptionHandling<SOOrder.curyControlTotal>(sender, row, "Document balance will become negative. The document will not be released.");
                flag1 = true;
              }
            }
          }
          else
          {
            Decimal? curyOrderTotal = row.CuryOrderTotal;
            Decimal num = 0M;
            if (curyOrderTotal.GetValueOrDefault() < num & curyOrderTotal.HasValue && row.ARDocType != "UND" && EnumerableExtensions.IsNotIn<string>(row.Behavior, "RM", "MO"))
            {
              RaiseExceptionHandling<SOOrder.curyOrderTotal>(sender, row, "Document balance will become negative. The document will not be released.");
              flag1 = true;
            }
          }
        }
      }
    }
    if (flag1)
      return;
    nullable1 = ((PXSelectBase<SOOrderType>) this.soordertype).Current.RequireControlTotal;
    if (nullable1.GetValueOrDefault())
      sender.RaiseExceptionHandling<SOOrder.curyControlTotal>((object) row, (object) null, (Exception) null);
    else
      sender.RaiseExceptionHandling<SOOrder.curyOrderTotal>((object) row, (object) null, (Exception) null);

    void RaiseExceptionHandling<TField>(PXCache cache, SOOrder row, string errorMessage) where TField : IBqlField
    {
      PXSetPropertyException propertyException = new PXSetPropertyException((IBqlTable) row, errorMessage);
      if (sender.RaiseExceptionHandling<TField>((object) row, cache.GetValue<TField>((object) row), (Exception) propertyException) & isRowPersisting)
        throw new PXRowPersistingException(nameof (TField), (object) null, errorMessage);
    }
  }

  protected virtual void SOOrder_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    SOOrder row = e.Row as SOOrder;
    SOOrder oldRow = e.OldRow as SOOrder;
    if (row == null)
      return;
    bool flag1 = !sender.ObjectsEqual<SOOrder.customerID>((object) oldRow, (object) row);
    bool flag2 = !sender.ObjectsEqual<SOOrder.projectID>((object) oldRow, (object) row);
    if (flag1 | flag2)
    {
      foreach (PXResult<SOLine> pxResult in ((PXSelectBase<SOLine>) this.Transactions).Select(Array.Empty<object>()))
      {
        SOLine soLine = PXResult<SOLine>.op_Implicit(pxResult);
        if (flag1)
        {
          soLine.CustomerID = ((PXSelectBase<SOOrder>) this.Document).Current.CustomerID;
          ((PXSelectBase) this.Transactions).Cache.SetDefaultExt<SOLine.customerLocationID>((object) soLine);
        }
        soLine.ProjectID = ((PXSelectBase<SOOrder>) this.Document).Current.ProjectID;
        ((PXSelectBase<SOLine>) this.Transactions).Update(soLine);
      }
    }
    if (!sender.ObjectsEqual<SOOrder.hold>((object) oldRow, (object) row))
      ((PXSelectBase) this.Transactions).Cache.ClearItemAttributes();
    bool? nullable1 = row.DeferPriceDiscountRecalculation;
    if (nullable1.GetValueOrDefault() && !sender.ObjectsEqual<SOOrder.orderDate, SOOrder.taxZoneID>((object) oldRow, (object) row))
      row.IsPriceAndDiscountsValid = new bool?(false);
    if (!sender.ObjectsEqual<SOOrder.shipVia>(e.OldRow, e.Row) && (oldRow.ShipVia == null || !(oldRow.ShipVia == row.ShipVia)))
    {
      nullable1 = row.IsManualPackage;
      if (!nullable1.GetValueOrDefault())
      {
        if (string.IsNullOrEmpty(row.ShipVia))
        {
          foreach (PXResult<SOPackageInfoEx> pxResult in ((PXSelectBase<SOPackageInfoEx>) this.Packages).Select(Array.Empty<object>()))
            ((PXSelectBase<SOPackageInfoEx>) this.Packages).Delete(PXResult<SOPackageInfoEx>.op_Implicit(pxResult));
          row.PackageWeight = new Decimal?(0M);
        }
        else
          this.CarrierRatesExt.RecalculatePackagesForOrder(((PXSelectBase<SOOrder>) this.Document).Current);
      }
    }
    if (e.ExternalCall && !sender.ObjectsEqual<SOOrder.disableAutomaticDiscountCalculation>(e.OldRow, e.Row))
    {
      nullable1 = row.DisableAutomaticDiscountCalculation;
      if (nullable1.GetValueOrDefault())
      {
        foreach (PXResult<SOOrderDiscountDetail> pxResult in ((PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails).Select(Array.Empty<object>()))
        {
          SOOrderDiscountDetail orderDiscountDetail = PXResult<SOOrderDiscountDetail>.op_Implicit(pxResult);
          orderDiscountDetail.IsManual = new bool?(true);
          ((PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails).Update(orderDiscountDetail);
        }
        foreach (PXResult<SOLine> pxResult in ((PXSelectBase<SOLine>) this.Transactions).Select(Array.Empty<object>()))
        {
          SOLine soLine = PXResult<SOLine>.op_Implicit(pxResult);
          if (!soLine.IsFree.GetValueOrDefault() && soLine.LineType != "DS")
          {
            ((PXSelectBase) this.Transactions).Cache.SetValueExt<SOLine.manualDisc>((object) soLine, (object) true);
            GraphHelper.MarkUpdated(((PXSelectBase) this.Transactions).Cache, (object) soLine);
          }
        }
        ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<SOOrder.disableAutomaticDiscountCalculation>((object) row, (object) null, (Exception) new PXSetPropertyException("In the current document, all discounts are now manual.", (PXErrorLevel) 2));
      }
    }
    bool flag3 = false;
    if (!sender.ObjectsEqual<SOOrder.customerLocationID, SOOrder.orderDate>(e.OldRow, e.Row))
    {
      using (this.GetPriceCalculationScope().AppendContext<SOOrder.customerLocationID, SOOrder.orderDate>())
        this._discountEngine.AutoRecalculatePricesAndDiscounts(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<SOLine>) this.Transactions, (SOLine) null, (PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails, row.CustomerLocationID, row.OrderDate, this.GetDefaultSODiscountCalculationOptions(((PXSelectBase<SOOrder>) this.Document).Current));
      flag3 = true;
    }
    if (!this._discountEngine.IsInternalDiscountEngineCall && (e.ExternalCall || FlaggedModeScopeBase<SOOrderEntry.SkipCalculateTotalDocDiscountScope>.IsActive) && sender.GetStatus((object) row) != 3 && !sender.ObjectsEqual<SOOrder.curyDiscTot>(e.OldRow, e.Row))
    {
      this._discountEngine.SetTotalDocDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<SOLine>) this.Transactions, (PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails, ((PXSelectBase<SOOrder>) this.Document).Current.CuryDiscTot, DiscountEngine.DiscountCalculationOptions.DisableAPDiscountsCalculation);
      flag3 = true;
    }
    if (flag3)
      this.RecalculateTotalDiscount();
    int? nullable2 = row.CustomerID;
    Decimal? nullable3;
    Decimal? nullable4;
    if (nullable2.HasValue)
    {
      nullable3 = row.CuryDiscTot;
      if (nullable3.HasValue)
      {
        nullable3 = row.CuryDiscTot;
        Decimal num1 = 0M;
        if (nullable3.GetValueOrDefault() > num1 & nullable3.HasValue)
        {
          nullable3 = row.CuryLineTotal;
          if (nullable3.HasValue)
          {
            nullable3 = row.CuryLineTotal;
            Decimal num2 = 0M;
            if (!(nullable3.GetValueOrDefault() > num2 & nullable3.HasValue))
            {
              nullable3 = row.CuryMiscTot;
              Decimal num3 = 0M;
              if (!(nullable3.GetValueOrDefault() > num3 & nullable3.HasValue))
                goto label_72;
            }
            DiscountEngine<SOLine, SOOrderDiscountDetail> discountEngine = this._discountEngine;
            PXCache cache = sender;
            int? customerId = row.CustomerID;
            nullable2 = new int?();
            int? vendorID = nullable2;
            Decimal discountLimit = discountEngine.GetDiscountLimit(cache, customerId, vendorID);
            Decimal? curyLineTotal = row.CuryLineTotal;
            Decimal? nullable5 = row.CuryMiscTot;
            Decimal? nullable6 = curyLineTotal.HasValue & nullable5.HasValue ? new Decimal?(curyLineTotal.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
            Decimal num4 = (Decimal) 100;
            Decimal? nullable7;
            if (!nullable6.HasValue)
            {
              nullable5 = new Decimal?();
              nullable7 = nullable5;
            }
            else
              nullable7 = new Decimal?(nullable6.GetValueOrDefault() / num4);
            Decimal? nullable8 = nullable7;
            Decimal num5 = discountLimit;
            Decimal? nullable9;
            if (!nullable8.HasValue)
            {
              nullable6 = new Decimal?();
              nullable9 = nullable6;
            }
            else
              nullable9 = new Decimal?(nullable8.GetValueOrDefault() * num5);
            nullable3 = nullable9;
            nullable4 = row.CuryDiscTot;
            if (nullable3.GetValueOrDefault() < nullable4.GetValueOrDefault() & nullable3.HasValue & nullable4.HasValue)
              PXUIFieldAttribute.SetWarning<SOOrder.curyDiscTot>(sender, (object) row, PXMessages.LocalizeFormatNoPrefix("The total amount of group and document discounts cannot exceed the limit specified for the customer class ({0:F2}%) on the Customer Classes (AR201000) form.", new object[1]
              {
                (object) discountLimit
              }));
          }
        }
      }
    }
label_72:
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find(sender.Graph, row.ShipVia);
    if (carrier != null)
    {
      nullable1 = carrier.IsExternal;
      if (nullable1.GetValueOrDefault() && !sender.ObjectsEqual<SOOrder.resedential, SOOrder.saturdayDelivery, SOOrder.insurance, SOOrder.shipAddressID, SOOrder.groundCollect>(e.Row, e.OldRow))
        ((PXSelectBase<SOOrder>) this.Document).Current.FreightCostIsValid = new bool?(false);
    }
    if (!sender.ObjectsEqual<SOOrder.lineTotal, SOOrder.orderWeight, SOOrder.packageWeight, SOOrder.orderVolume, SOOrder.shipTermsID, SOOrder.shipZoneID, SOOrder.shipVia, SOOrder.useCustomerAccount>(e.OldRow, e.Row) || !sender.ObjectsEqual<SOOrder.curyFreightCost, SOOrder.overrideFreightAmount>(e.OldRow, e.Row))
    {
      if (!sender.ObjectsEqual<SOOrder.shipVia>(e.OldRow, e.Row) && carrier?.CalcMethod == "M" && !this.IsCopyOrder)
        row.FreightCost = new Decimal?(0M);
      if (!((PXGraph) this).IsImportFromExcel)
        this.CalcFreight(row);
    }
    nullable1 = row.IsManualPackage;
    if (!nullable1.GetValueOrDefault())
    {
      nullable4 = row.OrderWeight;
      nullable3 = oldRow.OrderWeight;
      if (!(nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue) || row.ShipVia != oldRow.ShipVia && !string.IsNullOrEmpty(oldRow.ShipVia))
        sender.SetValue<SOOrder.isPackageValid>((object) row, (object) false);
    }
    if (!sender.ObjectsEqual<SOOrder.curyFreightTot, SOOrder.curyUnbilledFreightTot, SOOrder.freightTaxCategoryID>(e.OldRow, e.Row))
    {
      TaxCalc isTaxCalc = TaxCalc.Calc;
      if (((PXGraph) this).IsCopyPasteContext)
      {
        isTaxCalc = TaxBaseAttribute.GetTaxCalc<SOOrder.freightTaxCategoryID>(sender, (object) null);
        TaxBaseAttribute.SetTaxCalc<SOOrder.freightTaxCategoryID>(sender, (object) null, TaxCalc.ManualCalc);
      }
      TaxBaseAttribute.Calculate<SOOrder.freightTaxCategoryID>(sender, e);
      if (((PXGraph) this).IsCopyPasteContext)
        TaxBaseAttribute.SetTaxCalc<SOOrder.freightTaxCategoryID>(sender, (object) null, isTaxCalc);
    }
    if (!sender.ObjectsEqual<SOOrder.hold>(e.Row, e.OldRow))
    {
      nullable1 = row.Hold;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = ((PXSelectBase<SOOrderType>) this.soordertype).Current.RequireShipping;
        if (nullable1.GetValueOrDefault() && ((PXSelectBase<SOOrderType>) this.soordertype).Current.ARDocType != "UND")
        {
          foreach (PXResult<SOLine> pxResult in ((PXSelectBase<SOLine>) this.Transactions).Select(Array.Empty<object>()))
          {
            SOLine soLine = PXResult<SOLine>.op_Implicit(pxResult);
            nullable2 = soLine.SalesAcctID;
            if (nullable2.HasValue)
            {
              nullable2 = soLine.SalesSubID;
              if (nullable2.HasValue)
                goto label_96;
            }
            GraphHelper.MarkUpdated(((PXSelectBase) this.Transactions).Cache, (object) soLine, true);
label_96:
            PXDefaultAttribute.SetPersistingCheck<SOLine.salesAcctID>(((PXSelectBase) this.Transactions).Cache, (object) soLine, (PXPersistingCheck) 1);
            PXDefaultAttribute.SetPersistingCheck<SOLine.salesSubID>(((PXSelectBase) this.Transactions).Cache, (object) soLine, (PXPersistingCheck) 1);
          }
        }
      }
    }
    this.UpdateControlTotal(sender, e);
    this.UpdateCustomerBalances(sender, row, oldRow);
    if (sender.ObjectsEqual<SOOrder.completed, SOOrder.cancelled>(e.Row, e.OldRow))
      return;
    nullable1 = row.Completed;
    if (nullable1.GetValueOrDefault())
    {
      nullable2 = row.BilledCntr;
      int num = 0;
      if (nullable2.GetValueOrDefault() == num & nullable2.HasValue)
      {
        nullable2 = row.ShipmentCntr;
        int? billedCntr = row.BilledCntr;
        int? releasedCntr = row.ReleasedCntr;
        int? nullable10 = billedCntr.HasValue & releasedCntr.HasValue ? new int?(billedCntr.GetValueOrDefault() + releasedCntr.GetValueOrDefault()) : new int?();
        if (nullable2.GetValueOrDefault() <= nullable10.GetValueOrDefault() & nullable2.HasValue & nullable10.HasValue)
          goto label_107;
      }
    }
    nullable1 = row.Cancelled;
    if (!nullable1.GetValueOrDefault())
      return;
label_107:
    foreach (PXResult<SOAdjust> pxResult in ((PXSelectBase<SOAdjust>) this.Adjustments_Raw).Select(Array.Empty<object>()))
    {
      SOAdjust copy = PXCache<SOAdjust>.CreateCopy(PXResult<SOAdjust>.op_Implicit(pxResult));
      copy.CuryAdjdAmt = new Decimal?(0M);
      copy.CuryAdjgAmt = new Decimal?(0M);
      copy.AdjAmt = new Decimal?(0M);
      ((PXSelectBase<SOAdjust>) this.Adjustments).Update(copy);
    }
  }

  protected virtual void UpdateCustomerBalances(PXCache cache, SOOrder row, SOOrder oldRow)
  {
  }

  protected virtual void CalcFreight(SOOrder row)
  {
    SOOrderType current = ((PXSelectBase<SOOrderType>) this.soordertype).Current;
    int num;
    if (current == null)
    {
      num = 1;
    }
    else
    {
      bool? calculateFreight = current.CalculateFreight;
      bool flag = false;
      num = !(calculateFreight.GetValueOrDefault() == flag & calculateFreight.HasValue) ? 1 : 0;
    }
    if (num == 0)
      return;
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, row.ShipVia);
    FreightCalculator freightCalculator = this.CreateFreightCalculator();
    this.CalcFreightCost(row, carrier, freightCalculator);
    this.ApplyFreightTerms(row, carrier, freightCalculator);
  }

  protected virtual void CalcFreightCost(
    SOOrder row,
    PX.Objects.CS.Carrier carrier,
    FreightCalculator freightCalculator)
  {
    if ((carrier != null ? (!carrier.IsExternal.GetValueOrDefault() ? 1 : 0) : 1) != 0)
    {
      freightCalculator.CalcFreightCost<SOOrder, SOOrder.curyFreightCost>(((PXSelectBase) this.Document).Cache, row);
      row.FreightCostIsValid = new bool?(true);
    }
    else
      row.FreightCostIsValid = new bool?(false);
  }

  protected virtual void ApplyFreightTerms(
    SOOrder row,
    PX.Objects.CS.Carrier carrier,
    FreightCalculator freightCalculator)
  {
    if (row.OverrideFreightAmount.GetValueOrDefault() || (carrier != null ? (!carrier.IsExternal.GetValueOrDefault() ? 1 : 0) : 1) == 0 && !freightCalculator.IsFlatRate<SOOrder>(((PXSelectBase) this.Document).Cache, row))
      return;
    freightCalculator.ApplyFreightTerms<SOOrder, SOOrder.curyFreightAmt>(((PXSelectBase) this.Document).Cache, row, new Lazy<int?>((Func<int?>) (() => new int?(((PXSelectBase<SOLine>) this.Transactions).Select(Array.Empty<object>()).Count))));
  }

  protected virtual void SOOrder_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    SOOrder row = (SOOrder) e.Row;
    if (row == null || PXAccess.FeatureInstalled<FeaturesSet.autoPackaging>())
      return;
    row.IsManualPackage = new bool?(true);
  }

  protected virtual void SOOrder_TermsID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    PX.Objects.CS.Terms terms = (PX.Objects.CS.Terms) PXSelectorAttribute.Select<SOOrder.termsID>(sender, e.Row);
    if (terms == null || !(terms.InstallmentType != "S"))
      return;
    PXResultset<SOAdjust> pxResultset = ((PXSelectBase<SOAdjust>) this.Adjustments).Select(Array.Empty<object>());
    if (pxResultset.Count <= 0)
      return;
    PXUIFieldAttribute.SetWarning<SOOrder.termsID>(sender, e.Row, "All applications have been unlinked because multiple installment credit terms were specified.");
    foreach (PXResult<SOAdjust> pxResult in pxResultset)
      ((PXSelectBase) this.Adjustments).Cache.Delete((object) PXResult<SOAdjust>.op_Implicit(pxResult));
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current<SOOrder.shipmentCntr>, Equal<int0>, And<Current<SOOrder.overrideFreightAmount>, Equal<False>, Or<PX.Objects.CS.ShipTerms.freightAmountSource, Equal<Current<SOOrder.freightAmountSource>>>>>), "Cannot select shipping terms with Invoice Freight Price Based On set to {0}.", new System.Type[] {typeof (PX.Objects.CS.ShipTerms.freightAmountSource)})]
  protected virtual void SOOrder_ShipTermsID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void SOOrder_CuryFreightAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.ShowWarningIfPartiallyInvoiced<SOOrder.curyFreightAmt>(sender, (SOOrder) e.Row);
  }

  protected virtual void SOOrder_CuryPremiumFreightAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.ShowWarningIfPartiallyInvoiced<SOOrder.curyPremiumFreightAmt>(sender, (SOOrder) e.Row);
  }

  protected virtual void ShowWarningIfPartiallyInvoiced<amtField>(PXCache sender, SOOrder doc) where amtField : IBqlField
  {
    if (((PXGraph) this).UnattendedMode || !(((PXSelectBase<SOSetup>) this.sosetup).Current.FreightAllocation == "A"))
      return;
    int? billedCntr = (int?) doc?.BilledCntr;
    int? releasedCntr = (int?) doc?.ReleasedCntr;
    int? nullable = billedCntr.HasValue & releasedCntr.HasValue ? new int?(billedCntr.GetValueOrDefault() + releasedCntr.GetValueOrDefault()) : new int?();
    int num = 0;
    if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
      return;
    sender.RaiseExceptionHandling<amtField>((object) doc, sender.GetValue<amtField>((object) doc), (Exception) new PXSetPropertyException("Please adjust the {0} manually in the corresponding invoice or invoices.", (PXErrorLevel) 2, new object[1]
    {
      (object) PXUIFieldAttribute.GetDisplayName<amtField>(sender)
    }));
  }

  public object GetValue<Field>(object data) where Field : IBqlField
  {
    return ((PXGraph) this).Caches[BqlCommand.GetItemType(typeof (Field))].GetValue(data, typeof (Field).Name);
  }

  [PXBool]
  [DRTerms.Dates(typeof (SOLine.dRTermStartDate), typeof (SOLine.dRTermEndDate), typeof (SOLine.inventoryID), VerifyDatesPresent = false)]
  protected virtual void SOLine_ItemRequiresTerms_CacheAttached(PXCache sender)
  {
  }

  protected virtual void SOLine_SiteID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    int? nullable1;
    if (((PXSelectBase<SOOrder>) this.Document).Current != null)
    {
      nullable1 = ((PXSelectBase<SOOrder>) this.Document).Current.DefaultSiteID;
      if (nullable1.HasValue)
      {
        e.NewValue = (object) ((PXSelectBase<SOOrder>) this.Document).Current.DefaultSiteID;
        ((CancelEventArgs) e).Cancel = true;
        return;
      }
    }
    SOLine row = (SOLine) e.Row;
    if (row == null)
      return;
    PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this, row.BranchID);
    InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find((PXGraph) this, row.InventoryID, branch?.BaseCuryID);
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
    if (inventoryItem == null || inventoryItem.StkItem.GetValueOrDefault())
      return;
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    int? nullable2;
    if (itemCurySettings == null)
    {
      nullable1 = new int?();
      nullable2 = nullable1;
    }
    else
      nullable2 = itemCurySettings.DfltSiteID;
    // ISSUE: variable of a boxed type
    __Boxed<int?> local = (ValueType) nullable2;
    defaultingEventArgs.NewValue = (object) local;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOLine_InventoryID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    SOLine row = (SOLine) e.Row;
    if (row == null)
      return;
    object obj = sender.GetValue<SOLine.inventoryID>((object) row);
    if (obj != null)
    {
      if (row.InvoiceNbr != null)
      {
        e.NewValue = obj;
      }
      else
      {
        foreach (PXResult<SOLineSplit> pxResult in ((PXSelectBase<SOLineSplit>) this.splits).Select(Array.Empty<object>()))
        {
          SOLineSplit soLineSplit = PXResult<SOLineSplit>.op_Implicit(pxResult);
          if (soLineSplit.Completed.GetValueOrDefault())
          {
            e.NewValue = obj;
            sender.RaiseExceptionHandling<SOLine.inventoryID>((object) row, obj, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefixNLA("You cannot change Inventory ID for the allocation line {0}, because the line has been already completed.", new object[1]
            {
              (object) soLineSplit.SplitLineNbr.ToString()
            }), (PXErrorLevel) 2));
          }
        }
      }
    }
    if (!(row.Operation != "R") || e.NewValue == obj)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, (int?) e.NewValue);
    if (inventoryItem == null)
      return;
    bool? nullable = inventoryItem.StkItem;
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    nullable = inventoryItem.KitItem;
    if (nullable.GetValueOrDefault())
    {
      PXViewOf<INKitSpecStkDet>.BasedOn<SelectFromBase<INKitSpecStkDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<INKitSpecStkDet.compInventoryID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INKitSpecStkDet.kitInventoryID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.IN.InventoryItem.itemStatus, IBqlString>.IsEqual<InventoryItemStatus.noSales>>>>.ReadOnly readOnly1 = new PXViewOf<INKitSpecStkDet>.BasedOn<SelectFromBase<INKitSpecStkDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<INKitSpecStkDet.compInventoryID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INKitSpecStkDet.kitInventoryID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.IN.InventoryItem.itemStatus, IBqlString>.IsEqual<InventoryItemStatus.noSales>>>>.ReadOnly((PXGraph) this);
      PXViewOf<INKitSpecNonStkDet>.BasedOn<SelectFromBase<INKitSpecNonStkDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<INKitSpecNonStkDet.compInventoryID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INKitSpecNonStkDet.kitInventoryID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.IN.InventoryItem.itemStatus, IBqlString>.IsEqual<InventoryItemStatus.noSales>>>>.ReadOnly readOnly2 = new PXViewOf<INKitSpecNonStkDet>.BasedOn<SelectFromBase<INKitSpecNonStkDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<INKitSpecNonStkDet.compInventoryID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INKitSpecNonStkDet.kitInventoryID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.IN.InventoryItem.itemStatus, IBqlString>.IsEqual<InventoryItemStatus.noSales>>>>.ReadOnly((PXGraph) this);
      object[] objArray = new object[1]
      {
        (object) inventoryItem.InventoryID
      };
      if (((PXSelectBase<INKitSpecStkDet>) readOnly1).SelectWindowed(0, 1, objArray).Count == 0)
      {
        if (((PXSelectBase<INKitSpecNonStkDet>) readOnly2).SelectWindowed(0, 1, new object[1]
        {
          (object) inventoryItem.InventoryID
        }).Count == 0)
          return;
      }
      PXSetPropertyException<SOLine.inventoryID> propertyException = new PXSetPropertyException<SOLine.inventoryID>("The document cannot be saved because it includes a line with a non-stock kit that has components with the No Sales status.");
      ((PXSetPropertyException) propertyException).ErrorValue = (object) inventoryItem.InventoryCD;
      throw propertyException;
    }
  }

  protected virtual void SOLine_SubItemID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    SOLine row = (SOLine) e.Row;
    if (row == null || row.InvoiceNbr == null)
      return;
    object obj = sender.GetValue<SOLine.subItemID>(e.Row);
    if (obj == null)
      return;
    e.NewValue = obj;
  }

  protected virtual void SOLine_SubItemID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    SOLine row = (SOLine) e.Row;
    if (row == null || row.InvoiceNbr == null)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOLine_UOM_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (((SOLine) e.Row)?.InvoiceNbr == null)
      return;
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOLine_UOM_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    SOLine row = (SOLine) e.Row;
    if (row == null || row.InvoiceNbr == null)
      return;
    object obj = sender.GetValue<SOLine.uOM>((object) row);
    if (obj == null)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
    if (inventoryItem != null && row.InvoiceUOM != null)
    {
      INUnit inUnit = INUnit.UK.ByInventory.Find((PXGraph) this, inventoryItem.InventoryID, (string) e.NewValue);
      if (inUnit != null)
      {
        if (EnumerableExtensions.IsIn<string>(inUnit.FromUnit, row.InvoiceUOM, inUnit.ToUnit))
          return;
        throw new PXSetPropertyException("The UOM can be changed only to a base one or to UOM originally used in the invoice.");
      }
    }
    e.NewValue = obj;
  }

  protected virtual void SOLine_SalesAcctID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    SOLine row = (SOLine) e.Row;
    if (row == null)
      return;
    SOOrder current1 = ((PXSelectBase<SOOrder>) this.Document).Current;
    bool? nullable;
    int num;
    if (current1 == null)
    {
      num = 0;
    }
    else
    {
      nullable = current1.IsTransferOrder;
      bool flag = false;
      num = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    }
    if (num == 0)
      return;
    nullable = row.IsBeingCopied;
    if (nullable.GetValueOrDefault())
      return;
    PX.Objects.IN.InventoryItem data1 = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
    if (data1 == null)
      return;
    switch (((PXSelectBase<SOOrderType>) this.soordertype).Current.SalesAcctDefault)
    {
      case "I":
        e.NewValue = this.GetValue<PX.Objects.IN.InventoryItem.salesAcctID>((object) data1);
        ((CancelEventArgs) e).Cancel = true;
        break;
      case "W":
        PX.Objects.IN.INSite data2 = PX.Objects.IN.INSite.PK.Find((PXGraph) this, row.SiteID);
        if (data2 == null)
          break;
        e.NewValue = this.GetValue<PX.Objects.IN.INSite.salesAcctID>((object) data2);
        ((CancelEventArgs) e).Cancel = true;
        break;
      case "P":
        INPostClass data3 = INPostClass.PK.Find((PXGraph) this, data1.PostClassID) ?? new INPostClass();
        e.NewValue = this.GetValue<INPostClass.salesAcctID>((object) data3);
        ((CancelEventArgs) e).Cancel = true;
        break;
      case "L":
        PX.Objects.CR.Location current2 = ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current;
        e.NewValue = this.GetValue<PX.Objects.CR.Location.cSalesAcctID>((object) current2);
        ((CancelEventArgs) e).Cancel = true;
        break;
      case "R":
        PX.Objects.CS.ReasonCode data4 = PX.Objects.CS.ReasonCode.PK.Find((PXGraph) this, row.ReasonCode);
        e.NewValue = this.GetValue<PX.Objects.CS.ReasonCode.salesAcctID>((object) data4);
        ((CancelEventArgs) e).Cancel = true;
        break;
    }
  }

  protected virtual void SOLine_SalesSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    SOLine row = (SOLine) e.Row;
    if (row == null)
      return;
    SOOrder current1 = ((PXSelectBase<SOOrder>) this.Document).Current;
    bool? nullable;
    int num;
    if (current1 == null)
    {
      num = 0;
    }
    else
    {
      nullable = current1.IsTransferOrder;
      bool flag = false;
      num = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    }
    if (num == 0 || !row.SalesAcctID.HasValue)
      return;
    nullable = row.IsBeingCopied;
    if (nullable.GetValueOrDefault())
      return;
    PX.Objects.IN.InventoryItem data1 = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
    PX.Objects.IN.INSite data2 = PX.Objects.IN.INSite.PK.Find((PXGraph) this, row.SiteID);
    PX.Objects.CS.ReasonCode data3 = PX.Objects.CS.ReasonCode.PK.Find((PXGraph) this, row.ReasonCode);
    PX.Objects.AR.SalesPerson data4 = (PX.Objects.AR.SalesPerson) PXSelectorAttribute.Select<SOLine.salesPersonID>(sender, e.Row);
    INPostClass data5 = INPostClass.PK.Find((PXGraph) this, data1?.PostClassID) ?? new INPostClass();
    PX.Objects.EP.EPEmployee data6 = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.defContactID, Equal<Current<SOOrder.ownerID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    PX.Objects.CR.Standalone.Location data7 = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelectJoin<PX.Objects.CR.Standalone.Location, InnerJoin<BAccountR, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<PX.Objects.GL.Branch, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<SOLine.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.BranchID
    }));
    PX.Objects.CR.Location current2 = ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current;
    object obj1 = this.GetValue<PX.Objects.IN.InventoryItem.salesSubID>((object) data1);
    object obj2 = this.GetValue<PX.Objects.IN.INSite.salesSubID>((object) data2);
    object obj3 = this.GetValue<INPostClass.salesSubID>((object) data5);
    object obj4 = this.GetValue<PX.Objects.CR.Location.cSalesSubID>((object) current2);
    object obj5 = this.GetValue<PX.Objects.EP.EPEmployee.salesSubID>((object) data6);
    object obj6 = this.GetValue<PX.Objects.CR.Standalone.Location.cMPSalesSubID>((object) data7);
    object obj7 = this.GetValue<PX.Objects.AR.SalesPerson.salesSubID>((object) data4);
    object obj8 = this.GetValue<PX.Objects.CS.ReasonCode.salesSubID>((object) data3);
    object obj9 = (object) null;
    bool flag1 = false;
    try
    {
      obj9 = (object) SOSalesSubAccountMaskAttribute.MakeSub<SOOrderType.salesSubMask>((PXGraph) this, ((PXSelectBase<SOOrderType>) this.soordertype).Current.SalesSubMask, new object[8]
      {
        obj1,
        obj2,
        obj3,
        obj4,
        obj5,
        obj6,
        obj7,
        obj8
      }, new System.Type[8]
      {
        typeof (PX.Objects.IN.InventoryItem.salesSubID),
        typeof (PX.Objects.IN.INSite.salesSubID),
        typeof (INPostClass.salesSubID),
        typeof (PX.Objects.CR.Location.cSalesSubID),
        typeof (PX.Objects.EP.EPEmployee.salesSubID),
        typeof (PX.Objects.CR.Location.cMPSalesSubID),
        typeof (PX.Objects.AR.SalesPerson.salesSubID),
        typeof (PX.Objects.CS.ReasonCode.subID)
      });
      sender.RaiseFieldUpdating<SOLine.salesSubID>((object) row, ref obj9);
    }
    catch (PXMaskArgumentException ex)
    {
      sender.RaiseExceptionHandling<SOLine.salesSubID>(e.Row, (object) null, (Exception) new PXSetPropertyException(((Exception) ex).Message));
      obj9 = (object) null;
      flag1 = true;
    }
    catch (PXSetPropertyException ex)
    {
      sender.RaiseExceptionHandling<SOLine.salesSubID>(e.Row, obj9, (Exception) ex);
      obj9 = (object) null;
      flag1 = true;
    }
    if (!flag1 && (((PXGraph) this).IsImportFromExcel || ((PXGraph) this).IsImport || ((PXGraph) this).IsCopyPasteContext || ((PXGraph) this).IsContractBasedAPI))
      sender.RaiseExceptionHandling<SOLine.salesSubID>(e.Row, obj9, (Exception) null);
    e.NewValue = (object) (int?) obj9;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOLine_Completed_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null || !(((SOLine) e.Row).LineType == "MI"))
      return;
    e.NewValue = (object) true;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOLine_ShipComplete_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SOLine row))
      return;
    bool? nullable = row.Completed;
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      return;
    foreach (SOLineSplit selectChild in PXParentAttribute.SelectChildren(((PXSelectBase) this.splits).Cache, e.Row, typeof (SOLine)))
    {
      nullable = selectChild.Completed;
      if (!nullable.GetValueOrDefault())
      {
        nullable = selectChild.POCompleted;
        if (!nullable.GetValueOrDefault())
        {
          nullable = selectChild.POCancelled;
          if (!nullable.GetValueOrDefault())
            ((PXSelectBase) this.splits).Cache.SetValue<SOLineSplit.shipComplete>((object) selectChild, (object) row.ShipComplete);
        }
      }
    }
  }

  protected virtual void SOLine_LocationID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    SOLine row = (SOLine) e.Row;
    if (row == null || row.RequireLocation.GetValueOrDefault())
      return;
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOLineSplit_LocationID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    SOLineSplit row = (SOLineSplit) e.Row;
    if (row == null || row.RequireLocation.GetValueOrDefault())
      return;
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOLine_Completed_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    SOLine row = (SOLine) e.Row;
    sender.SetValueExt<SOLine.closedQty>(e.Row, (object) (row.Completed.GetValueOrDefault() ? row.OrderQty : row.ShippedQty));
  }

  protected virtual void SiteStatusByCostCenter_RowInserted(
    PXCache sender,
    PXRowInsertedEventArgs e)
  {
    if (this.ItemAvailabilityExt.IsFetching)
      return;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter row = (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter) e.Row;
    if (row == null)
      return;
    INSiteStatusByCostCenter statusByCostCenter = INSiteStatusByCostCenter.PK.Find((PXGraph) this, row.InventoryID, row.SubItemID, row.SiteID, row.CostCenterID);
    if (((PXSelectBase<SOOrder>) this.Document).Current == null || !(((PXSelectBase<SOOrder>) this.Document).Current.Behavior != "QT") || !row.SiteID.HasValue || statusByCostCenter != null)
      return;
    row.InitSiteStatus = new bool?(true);
  }

  protected virtual void SOLineSplit_LotSerialNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    SOLineSplit row = (SOLineSplit) e.Row;
    if (row == null || row.RequireLocation.GetValueOrDefault())
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOLine_UOM_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    SOLine row = (SOLine) e.Row;
    string oldValue = (string) e.OldValue;
    if (row == null || !(oldValue != row.UOM))
      return;
    sender.SetDefaultExt<SOLine.curyUnitPrice>((object) row);
    sender.SetDefaultExt<SOLine.unitCost>((object) row);
    PXCache pxCache1 = sender;
    SOLine soLine1 = row;
    Decimal? nullable = row.BaseQty;
    Decimal? unitWeigth = row.UnitWeigth;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local1 = (ValueType) (nullable.HasValue & unitWeigth.HasValue ? new Decimal?(nullable.GetValueOrDefault() * unitWeigth.GetValueOrDefault()) : new Decimal?());
    pxCache1.SetValueExt<SOLine.extWeight>((object) soLine1, (object) local1);
    PXCache pxCache2 = sender;
    SOLine soLine2 = row;
    Decimal? baseQty = row.BaseQty;
    nullable = row.UnitVolume;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local2 = (ValueType) (baseQty.HasValue & nullable.HasValue ? new Decimal?(baseQty.GetValueOrDefault() * nullable.GetValueOrDefault()) : new Decimal?());
    pxCache2.SetValueExt<SOLine.extVolume>((object) soLine2, (object) local2);
  }

  protected virtual void SOLine_Operation_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<SOLine.tranType>(e.Row);
    sender.SetDefaultExt<SOLine.invtMult>(e.Row);
    sender.SetDefaultExt<SOLine.planType>(e.Row);
    sender.SetDefaultExt<SOLine.requireReasonCode>(e.Row);
    sender.SetDefaultExt<SOLine.autoCreateIssueLine>(e.Row);
    SOLine row = (SOLine) e.Row;
    if (row == null)
      return;
    sender.SetValueExt<SOLine.curyUnitPrice>(e.Row, (object) row.CuryUnitPrice);
    sender.SetValueExt<SOLine.discPct>(e.Row, (object) row.DiscPct);
    sender.SetValueExt<SOLine.curyDiscAmt>(e.Row, (object) row.CuryDiscAmt);
  }

  protected virtual void SOLine_SalesPersonID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<SOLine.salesSubID>(e.Row);
  }

  protected virtual void SOLine_ReasonCode_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is SOLine row))
      return;
    PX.Objects.CS.ReasonCode reasonCode = PX.Objects.CS.ReasonCode.PK.Find((PXGraph) this, (string) e.NewValue);
    if (reasonCode == null)
      return;
    if ((!(row.TranType == "TRX") || !(reasonCode.Usage == "T")) && (!(row.TranType != "TRX") || !(reasonCode.Usage == "I")) && (!(row.TranType != "III") || !(row.TranType != "RET") || !(reasonCode.Usage == "S")))
      throw new PXSetPropertyException("The usage type of the reason code does not match the document type.");
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOLine_ReasonCode_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<SOLine.salesAcctID>(e.Row);
    try
    {
      sender.SetDefaultExt<SOLine.salesSubID>(e.Row);
    }
    catch (PXSetPropertyException ex)
    {
      sender.SetValue<SOLine.salesSubID>(e.Row, (object) null);
    }
  }

  protected virtual void SOLine_SiteID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    SOLine row = (SOLine) e.Row;
    row.AvgCost = new Decimal?();
    sender.SetDefaultExt<SOLine.salesAcctID>((object) row);
    try
    {
      sender.SetDefaultExt<SOLine.salesSubID>((object) row);
    }
    catch (PXSetPropertyException ex)
    {
      sender.SetValue<SOLine.salesSubID>((object) row, (object) null);
    }
    if (string.IsNullOrEmpty(row.InvoiceNbr))
      sender.SetDefaultExt<SOLine.unitCost>((object) row);
    sender.SetDefaultExt<SOLine.pOSiteID>((object) row);
    using (this.GetPriceCalculationScope().AppendContext<SOLine.siteID>())
      sender.SetDefaultExt<SOLine.curyUnitPrice>((object) row);
  }

  [PopupMessage]
  [PXMergeAttributes]
  protected virtual void SOLine_InventoryID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void SOLine_InventoryID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    SOLine row = e.Row as SOLine;
    row.AvgCost = new Decimal?();
    sender.SetDefaultExt<SOLine.lineType>(e.Row);
    if (row.Operation == null)
      sender.SetDefaultExt<SOLine.operation>(e.Row);
    sender.SetDefaultExt<SOLine.tranType>(e.Row);
    sender.RaiseExceptionHandling<SOLine.uOM>(e.Row, (object) null, (Exception) null);
    sender.SetDefaultExt<SOLine.uOM>(e.Row);
    sender.SetValue<SOLine.closedQty>(e.Row, (object) 0M);
    sender.SetDefaultExt<SOLine.orderQty>(e.Row);
    if (((PXGraph) this).IsImport)
    {
      sender.SetDefaultExt<SOLine.salesPersonID>(e.Row);
      sender.SetDefaultExt<SOLine.reasonCode>(e.Row);
    }
    sender.SetDefaultExt<SOLine.salesAcctID>(e.Row);
    try
    {
      sender.SetDefaultExt<SOLine.salesSubID>(e.Row);
    }
    catch (PXSetPropertyException ex)
    {
      sender.SetValue<SOLine.salesSubID>(e.Row, (object) null);
    }
    sender.SetDefaultExt<SOLine.tranDesc>(e.Row);
    sender.SetDefaultExt<SOLine.taxCategoryID>(e.Row);
    sender.SetDefaultExt<SOLine.vendorID>(e.Row);
    sender.SetDefaultExt<SOLine.unitCost>(e.Row);
    sender.SetDefaultExt<SOLine.unitWeigth>(e.Row);
    sender.SetDefaultExt<SOLine.unitVolume>(e.Row);
    sender.SetDefaultExt<SOLine.pOSiteID>(e.Row);
    sender.SetDefaultExt<SOLine.completeQtyMin>(e.Row);
    sender.SetDefaultExt<SOLine.completeQtyMax>(e.Row);
  }

  protected virtual void SOLine_OrderQty_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    SOLine row = e.Row as SOLine;
    Decimal? oldValue = (Decimal?) e.OldValue;
    if (row == null)
      return;
    Decimal? qty = row.Qty;
    Decimal? nullable = oldValue;
    if (qty.GetValueOrDefault() == nullable.GetValueOrDefault() & qty.HasValue == nullable.HasValue)
      return;
    nullable = row.Qty;
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
    {
      sender.SetValueExt<SOLine.curyDiscAmt>((object) row, (object) 0M);
      sender.SetValueExt<SOLine.discPct>((object) row, (object) 0M);
    }
    using (this.GetPriceCalculationScope().AppendContext<SOLine.orderQty>())
      sender.SetDefaultExt<SOLine.curyUnitPrice>((object) row);
  }

  protected virtual void SOLine_ManualPrice_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SOLine row))
      return;
    sender.SetDefaultExt<SOLine.curyUnitPrice>((object) row);
    bool? nullable = row.ManualPrice;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = row.SkipLineDiscounts;
    if (!nullable.GetValueOrDefault())
      return;
    sender.SetValue<SOLine.skipLineDiscounts>((object) row, (object) false);
  }

  protected virtual void SOLine_CustomerID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SOLine))
      return;
    sender.SetDefaultExt<SOLine.salesPersonID>(e.Row);
    try
    {
      sender.SetDefaultExt<SOLine.salesSubID>(e.Row);
    }
    catch (PXSetPropertyException ex)
    {
      sender.SetValue<SOLine.salesSubID>(e.Row, (object) null);
    }
  }

  public virtual bool RecalculatePriceAndDiscount() => true;

  protected virtual void SOLine_OrderQty_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    SOLine row = (SOLine) e.Row;
    if (EnumerableExtensions.IsNotIn<string>(row.LineType, "GI", "GN"))
      return;
    Decimal? newValue = (Decimal?) e.NewValue;
    Decimal? nullable1 = row.RequireShipping.GetValueOrDefault() ? row.ClosedQty : new Decimal?(0M);
    int? nullable2 = ((PXSelectBase<SOOrderType>) this.soordertype).Current.ActiveOperationsCntr;
    int num1 = 1;
    if (nullable2.GetValueOrDefault() <= num1 & nullable2.HasValue)
    {
      Decimal? nullable3 = newValue;
      Decimal? nullable4 = nullable1;
      if (nullable3.GetValueOrDefault() < nullable4.GetValueOrDefault() & nullable3.HasValue & nullable4.HasValue && row.TranType != "UND")
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
        {
          (object) nullable1.Value.ToString("0.####")
        });
    }
    else
    {
      Decimal? nullable5 = newValue;
      Decimal num2 = 0M;
      Decimal? nullable6;
      if (nullable5.GetValueOrDefault() >= num2 & nullable5.HasValue)
      {
        nullable6 = newValue;
        Decimal? nullable7 = nullable1;
        if (nullable6.GetValueOrDefault() < nullable7.GetValueOrDefault() & nullable6.HasValue & nullable7.HasValue)
          throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
          {
            (object) nullable1.Value.ToString("0.####")
          });
      }
      Decimal? nullable8 = newValue;
      Decimal num3 = 0M;
      if (nullable8.GetValueOrDefault() < num3 & nullable8.HasValue)
      {
        Decimal? nullable9 = newValue;
        nullable6 = nullable1;
        if (nullable9.GetValueOrDefault() > nullable6.GetValueOrDefault() & nullable9.HasValue & nullable6.HasValue)
          throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
          {
            (object) nullable1.Value.ToString("0.####")
          });
      }
      if (string.IsNullOrEmpty(row.InvoiceNbr) || !(row.Operation == "R"))
        return;
      nullable6 = newValue;
      Decimal num4 = 0M;
      short? lineSign;
      if (nullable6.GetValueOrDefault() > num4 & nullable6.HasValue)
      {
        nullable6 = row.OrderQty;
        Decimal num5 = 0M;
        if (nullable6.GetValueOrDefault() <= num5 & nullable6.HasValue)
        {
          lineSign = row.LineSign;
          nullable2 = lineSign.HasValue ? new int?((int) lineSign.GetValueOrDefault()) : new int?();
          int num6 = 0;
          if (nullable2.GetValueOrDefault() < num6 & nullable2.HasValue)
            throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
            {
              (object) 0M.ToString()
            });
        }
      }
      nullable6 = newValue;
      Decimal num7 = 0M;
      if (!(nullable6.GetValueOrDefault() < num7 & nullable6.HasValue))
        return;
      nullable6 = row.OrderQty;
      Decimal num8 = 0M;
      if (!(nullable6.GetValueOrDefault() >= num8 & nullable6.HasValue))
        return;
      lineSign = row.LineSign;
      nullable2 = lineSign.HasValue ? new int?((int) lineSign.GetValueOrDefault()) : new int?();
      int num9 = 0;
      if (nullable2.GetValueOrDefault() > num9 & nullable2.HasValue)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
        {
          (object) 0M.ToString()
        });
    }
  }

  protected virtual void SOLine_DiscPct_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    object row = e.Row;
  }

  protected virtual void SOLine_DiscPct_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is SOLine row))
      return;
    sender.RaiseExceptionHandling<SOLine.discPct>((object) row, (object) null, (Exception) null);
    if (this.GetMinGrossProfitValidationOption(sender, row) == "N")
      return;
    SOOrderEntry.MinGrossProfitClass mgpc = new SOOrderEntry.MinGrossProfitClass()
    {
      DiscPct = (Decimal?) e.NewValue,
      CuryDiscAmt = row.CuryDiscAmt,
      CuryUnitPrice = row.CuryUnitPrice
    };
    this.SOLineValidateMinGrossProfit(sender, row, mgpc);
    e.NewValue = (object) mgpc.DiscPct;
  }

  protected virtual void SOLine_CuryDiscAmt_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is SOLine row))
      return;
    sender.RaiseExceptionHandling<SOLine.curyDiscAmt>((object) row, (object) null, (Exception) null);
    if (this.GetMinGrossProfitValidationOption(sender, row) == "N")
      return;
    SOOrderEntry.MinGrossProfitClass mgpc = new SOOrderEntry.MinGrossProfitClass()
    {
      DiscPct = row.DiscPct,
      CuryDiscAmt = (Decimal?) e.NewValue,
      CuryUnitPrice = row.CuryUnitPrice
    };
    this.SOLineValidateMinGrossProfit(sender, row, mgpc);
    e.NewValue = (object) mgpc.CuryDiscAmt;
  }

  /// <summary>
  /// Checks if ManualPrice flag should be set automatically on import from Excel.
  /// This method is intended to be called from _FieldVerifying event handler.
  /// </summary>
  protected virtual bool IsManualPriceFlagNeeded(PXCache sender, SOLine row)
  {
    if (row != null && !row.ManualPrice.GetValueOrDefault() && (sender.Graph.IsImportFromExcel || sender.Graph.IsContractBasedAPI))
    {
      object valuePending1 = sender.GetValuePending<SOLine.curyUnitPrice>((object) row);
      object valuePending2 = sender.GetValuePending<SOLine.curyExtPrice>((object) row);
      object valuePending3 = sender.GetValuePending<SOLine.manualPrice>((object) row);
      Decimal result;
      if ((valuePending1 != PXCache.NotSetValue && valuePending1 != null && Decimal.TryParse(valuePending1.ToString(), out result) || valuePending2 != PXCache.NotSetValue && valuePending2 != null && Decimal.TryParse(valuePending2.ToString(), out result)) && (valuePending3 == PXCache.NotSetValue || valuePending3 == null))
        return true;
    }
    return false;
  }

  protected virtual void SOLine_CuryExtPrice_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is SOLine row) || !this.IsManualPriceFlagNeeded(sender, row))
      return;
    row.ManualPrice = new bool?(true);
  }

  public virtual SOOrderPriceCalculationScope CheckSourceChange()
  {
    return new SOOrderPriceCalculationScope();
  }

  protected virtual void _(
    PX.Data.Events.ExceptionHandling<SOAdjust.adjgCuryInfoID> e)
  {
    ((PX.Data.Events.ExceptionHandlingBase<PX.Data.Events.ExceptionHandling<SOAdjust.adjgCuryInfoID>>) e).Cancel = true;
  }

  protected virtual void SOLine_CuryUnitPrice_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is SOLine row))
      return;
    bool isPriceTypeValidationNeeded = true;
    if (e.NewValue != null && !this.IsCopyOrder)
    {
      using (SOOrderPriceCalculationScope calculationScope = this.CheckSourceChange())
        isPriceTypeValidationNeeded = calculationScope.Any();
    }
    if (!row.IsFree.GetValueOrDefault() && this.GetMinGrossProfitValidationOption(sender, row, isPriceTypeValidationNeeded) != "N")
    {
      sender.RaiseExceptionHandling<SOLine.curyUnitPrice>((object) row, (object) null, (Exception) null);
      SOOrderEntry.MinGrossProfitClass mgpc = new SOOrderEntry.MinGrossProfitClass()
      {
        DiscPct = row.DiscPct,
        CuryDiscAmt = row.CuryDiscAmt,
        CuryUnitPrice = (Decimal?) e.NewValue
      };
      this.SOLineValidateMinGrossProfit(sender, row, mgpc, isPriceTypeValidationNeeded);
      e.NewValue = (object) mgpc.CuryUnitPrice;
    }
    if (!this.IsManualPriceFlagNeeded(sender, row))
      return;
    row.ManualPrice = new bool?(true);
  }

  protected virtual void SOLine_CuryUnitPrice_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    SOLine row = (SOLine) e.Row;
    if (row == null)
      return;
    bool flag;
    using (UpdateIfFieldsChangedScope calculationScope = this.GetPriceCalculationScope())
      flag = calculationScope.IsUpdateNeeded<SOLine.inventoryID>();
    if (row.TranType == "TRX")
      e.NewValue = (object) 0M;
    else if (((!row.InventoryID.HasValue || row.ManualPrice.GetValueOrDefault() && !sender.Graph.IsCopyPasteContext || row.IsFree.GetValueOrDefault() ? 0 : (sender.Graph.IsCopyPasteContext.Implies((Func<bool>) (() => !((PXFieldState) sender.GetStateExt<SOLine.curyUnitPrice>(e.Row)).Enabled)) ? 1 : 0)) & (flag ? 1 : 0)) != 0)
    {
      string custPriceClass = "BASE";
      PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) this.location).Select(Array.Empty<object>()));
      if (!string.IsNullOrEmpty(location?.CPriceClassID))
        custPriceClass = location.CPriceClassID;
      PX.Objects.CM.CurrencyInfo currencyinfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Select(Array.Empty<object>()));
      ARSalesPriceMaint arSalesPriceMaint = ARSalesPriceMaint.SingleARSalesPriceMaint;
      bool baseCurrencySetting = arSalesPriceMaint.GetAlwaysFromBaseCurrencySetting(sender);
      ARSalesPriceMaint.SalesPriceItem spItem = (ARSalesPriceMaint.SalesPriceItem) null;
      try
      {
        spItem = arSalesPriceMaint.FindSalesPrice(sender, custPriceClass, row.CustomerID, row.InventoryID, row.LotSerialNbr, row.SiteID, currencyinfo.BaseCuryID, baseCurrencySetting ? currencyinfo.BaseCuryID : currencyinfo.CuryID, new Decimal?(Math.Abs(row.Qty.GetValueOrDefault())), row.UOM, ((PXSelectBase<SOOrder>) this.Document).Current.OrderDate.Value, false, ((PXSelectBase<SOOrder>) this.Document).Current.TaxCalcMode);
        row.SkipLineDiscountsBuffer = spItem?.SkipLineDiscounts;
      }
      catch (PXUnitConversionException ex)
      {
      }
      Decimal? newValue = arSalesPriceMaint.AdjustSalesPrice(sender, spItem, row.InventoryID, currencyinfo, row.UOM);
      e.NewValue = (object) newValue.GetValueOrDefault();
      ARSalesPriceMaint.CheckNewUnitPrice<SOLine, SOLine.curyUnitPrice>(sender, row, (object) newValue);
      if (spItem?.UOM != row.UOM || spItem?.CuryID != ((PXSelectBase<SOOrder>) this.Document).Current.CuryID)
        spItem = (ARSalesPriceMaint.SalesPriceItem) null;
      row.PriceType = spItem?.PriceType;
      row.IsPromotionalPrice = new bool?(spItem != null && spItem.IsPromotionalPrice);
    }
    else
      e.NewValue = (object) row.CuryUnitPrice.GetValueOrDefault();
  }

  protected virtual void SOLine_IsFree_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SOLine row))
      return;
    if (row.IsFree.GetValueOrDefault())
    {
      sender.SetValueExt<SOLine.curyUnitPrice>((object) row, (object) 0M);
      sender.SetValueExt<SOLine.discPct>((object) row, (object) 0M);
      sender.SetValueExt<SOLine.curyDiscAmt>((object) row, (object) 0M);
      if (!e.ExternalCall)
        return;
      sender.SetValueExt<SOLine.manualDisc>((object) row, (object) true);
    }
    else
    {
      if (!e.ExternalCall)
        return;
      sender.SetDefaultExt<SOLine.curyUnitPrice>((object) row);
      sender.SetValueExt<SOLine.manualPrice>((object) row, (object) false);
    }
  }

  protected virtual void SOLine_IsPromotionalPrice_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    SOLine row = (SOLine) e.Row;
    if (row == null)
      return;
    int num;
    if (((bool?) e.OldValue).GetValueOrDefault())
    {
      bool? promotionalPrice = row.IsPromotionalPrice;
      bool flag = false;
      num = promotionalPrice.GetValueOrDefault() == flag & promotionalPrice.HasValue ? 1 : 0;
    }
    else
      num = 0;
    if (num == 0)
      return;
    object obj = (object) null;
    sender.RaiseFieldVerifying<SOLine.curyUnitPrice>((object) row, ref obj);
    sender.RaiseFieldVerifying<SOLine.discPct>((object) row, ref obj);
    sender.RaiseFieldVerifying<SOLine.curyDiscAmt>((object) row, ref obj);
  }

  protected virtual void SOLine_PriceType_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    SOLine row = (SOLine) e.Row;
    if (row == null || (!EnumerableExtensions.IsIn<string>((string) e.OldValue, "C", "P") ? 0 : (EnumerableExtensions.IsNotIn<string>(row.PriceType, "C", "P") ? 1 : 0)) == 0)
      return;
    object obj = (object) null;
    using (SOOrderPriceCalculationScope calculationScope = this.CheckSourceChange())
    {
      if (calculationScope.Any())
        sender.RaiseFieldVerifying<SOLine.curyUnitPrice>((object) row, ref obj);
    }
    sender.RaiseFieldVerifying<SOLine.discPct>((object) row, ref obj);
    sender.RaiseFieldVerifying<SOLine.curyDiscAmt>((object) row, ref obj);
  }

  protected virtual void SOLine_DiscountID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    SOLine row = e.Row as SOLine;
    if (!e.ExternalCall)
      return;
    if (row == null)
      return;
    try
    {
      ((PXSelectBase<SOOrder>) this.Document).Current.DeferPriceDiscountRecalculation = new bool?(false);
      this._discountEngine.UpdateManualLineDiscount(sender, (PXSelectBase<SOLine>) this.Transactions, row, (PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails, ((PXSelectBase<SOOrder>) this.Document).Current.BranchID, ((PXSelectBase<SOOrder>) this.Document).Current.CustomerLocationID, ((PXSelectBase<SOOrder>) this.Document).Current.OrderDate, this.GetDefaultSODiscountCalculationOptions(((PXSelectBase<SOOrder>) this.Document).Current, true));
    }
    finally
    {
      ((PXSelectBase<SOOrder>) this.Document).Current.DeferPriceDiscountRecalculation = ((PXSelectBase<SOOrderType>) this.soordertype).Current.DeferPriceDiscountRecalculation;
    }
  }

  protected virtual void SOLine_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    SOLine row = (SOLine) e.Row;
    if (row == null)
      return;
    bool flag1 = row.LineType == "GI";
    bool? nullable1 = row.IsStockItem;
    bool? nullable2 = nullable1.HasValue ? new bool?(!nullable1.GetValueOrDefault()) : new bool?();
    int num1;
    if (nullable2.GetValueOrDefault())
    {
      nullable2 = row.IsKit;
      num1 = nullable2.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag2 = num1 != 0;
    PXUIFieldAttribute.SetEnabled<SOLine.subItemID>(sender, (object) row, flag1 && !flag2);
    PXUIFieldAttribute.SetEnabled<SOLine.locationID>(sender, (object) row, flag1 && this.LineSplittingExt.IsLocationEnabled);
    PXCache pxCache1 = sender;
    SOLine soLine = row;
    nullable1 = row.IsFree;
    int num2 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<SOLine.curyUnitPrice>(pxCache1, (object) soLine, num2 != 0);
    nullable1 = row.ManualDisc;
    int num3;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = row.IsFree;
      num3 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 0;
    bool flag3 = num3 != 0;
    nullable1 = row.IsFree;
    bool valueOrDefault = nullable1.GetValueOrDefault();
    PXCache pxCache2 = sender;
    object row1 = e.Row;
    int num4;
    if (!valueOrDefault)
    {
      if (((PXSelectBase<SOOrder>) this.Document).Current != null)
      {
        nullable1 = ((PXSelectBase<SOOrder>) this.Document).Current.DisableAutomaticDiscountCalculation;
        num4 = !nullable1.GetValueOrDefault() ? 1 : 0;
      }
      else
        num4 = 0;
    }
    else
      num4 = 0;
    PXUIFieldAttribute.SetEnabled<SOLine.manualDisc>(pxCache2, row1, num4 != 0);
    PXUIFieldAttribute.SetEnabled<SOLine.orderQty>(sender, e.Row, !flag3);
    PXUIFieldAttribute.SetEnabled<SOLine.isFree>(sender, e.Row, !flag3 && row.InventoryID.HasValue);
    PXUIFieldAttribute.SetEnabled<SOLine.skipLineDiscounts>(sender, e.Row, ((PXGraph) this).IsCopyPasteContext);
    bool? completed = ((SOLine) e.Row).Completed;
    if (!(row.POSource != "D"))
    {
      nullable1 = row.IsLegacyDropShip;
      if (!nullable1.GetValueOrDefault())
        goto label_16;
    }
    PXUIFieldAttribute.SetEnabled<SOLine.pOLinkActive>(sender, e.Row, false);
label_16:
    Decimal? nullable3 = ((SOLine) e.Row).ShippedQty;
    Decimal num5 = 0M;
    if (!(nullable3.GetValueOrDefault() == num5 & nullable3.HasValue))
    {
      PXUIFieldAttribute.SetEnabled(sender, e.Row, false);
      PXUIFieldAttribute.SetEnabled<SOLine.tranDesc>(sender, e.Row);
      PXCache pxCache3 = sender;
      object row2 = e.Row;
      nullable1 = completed;
      bool flag4 = false;
      int num6 = nullable1.GetValueOrDefault() == flag4 & nullable1.HasValue ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<SOLine.orderQty>(pxCache3, row2, num6 != 0);
      PXCache pxCache4 = sender;
      object row3 = e.Row;
      nullable1 = completed;
      bool flag5 = false;
      int num7 = nullable1.GetValueOrDefault() == flag5 & nullable1.HasValue ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<SOLine.shipComplete>(pxCache4, row3, num7 != 0);
      PXCache pxCache5 = sender;
      object row4 = e.Row;
      nullable1 = completed;
      bool flag6 = false;
      int num8 = nullable1.GetValueOrDefault() == flag6 & nullable1.HasValue ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<SOLine.completeQtyMin>(pxCache5, row4, num8 != 0);
      PXCache pxCache6 = sender;
      object row5 = e.Row;
      nullable1 = completed;
      bool flag7 = false;
      int num9 = nullable1.GetValueOrDefault() == flag7 & nullable1.HasValue ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<SOLine.completeQtyMax>(pxCache6, row5, num9 != 0);
      PXUIFieldAttribute.SetEnabled<SOLine.completed>(sender, e.Row, true);
    }
    PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.Transactions).Cache, (object) row).For<SOLine.vendorID>((Action<PXUIFieldAttribute>) (a => a.Enabled = row.POCreate.GetValueOrDefault())).SameFor<SOLine.pOSiteID>();
    PXCache pxCache7 = sender;
    object row6 = e.Row;
    int num10;
    if (row.Operation != "I")
    {
      int? activeOperationsCntr = ((PXSelectBase<SOOrderType>) this.soordertype).Current.ActiveOperationsCntr;
      int num11 = 1;
      num10 = activeOperationsCntr.GetValueOrDefault() > num11 & activeOperationsCntr.HasValue ? 1 : 0;
    }
    else
      num10 = 0;
    PXUIFieldAttribute.SetEnabled<SOLine.autoCreateIssueLine>(pxCache7, row6, num10 != 0);
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
    int num12;
    if (inventoryItem != null)
    {
      nullable1 = inventoryItem.IsConverted;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = row.IsStockItem;
        if (nullable1.HasValue)
        {
          nullable1 = inventoryItem.StkItem;
          nullable2 = row.IsStockItem;
          num12 = !(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) ? 1 : 0;
          goto label_26;
        }
      }
    }
    num12 = 0;
label_26:
    bool flag8 = num12 != 0;
    if (flag8)
      PXUIFieldAttribute.SetEnabled(sender, e.Row, false);
    ((PXSelectBase) this.splits).Cache.AllowInsert = ((PXSelectBase) this.Transactions).Cache.AllowInsert && !completed.GetValueOrDefault() && !flag8;
    ((PXSelectBase) this.splits).Cache.AllowUpdate = ((PXSelectBase) this.Transactions).Cache.AllowUpdate && !completed.GetValueOrDefault() && !flag8;
    ((PXSelectBase) this.splits).Cache.AllowDelete = ((PXSelectBase) this.Transactions).Cache.AllowDelete && !completed.GetValueOrDefault() && !flag8;
    SOOrder current = ((PXSelectBase<SOOrder>) this.Document).Current;
    if (current != null && !flag8)
    {
      PXUIFieldAttribute.SetEnabled<SOLine.shipDate>(sender, (object) row, current.ShipComplete == "B");
      nullable2 = current.Hold;
      if (!nullable2.GetValueOrDefault())
      {
        nullable2 = current.DontApprove;
        if (!nullable2.GetValueOrDefault())
          PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
      }
      PXUIFieldAttribute.SetEnabled<SOLine.curyUnitCost>(sender, (object) row, this.IsCuryUnitCostEnabled(row, current));
    }
    if (row != null)
    {
      nullable3 = row.CuryUnitPrice;
      if (nullable3.HasValue)
      {
        nullable3 = row.DiscPct;
        if (nullable3.HasValue)
        {
          nullable3 = row.DiscAmt;
          if (nullable3.HasValue)
            this.SOLineValidateMinGrossProfit(sender, row, new SOOrderEntry.MinGrossProfitClass()
            {
              CuryDiscAmt = row.CuryDiscAmt,
              CuryUnitPrice = row.CuryUnitPrice,
              DiscPct = row.DiscPct
            });
        }
      }
    }
    if (row.InvoiceNbr == null)
      return;
    PXUIFieldAttribute.SetEnabled<SOLine.operation>(sender, (object) row, false);
  }

  protected virtual void SOLine_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is SOLine row))
      return;
    bool? nullable1;
    if (((PXSelectBase<SOOrder>) this.Document).Current != null)
    {
      nullable1 = ((PXSelectBase<SOOrder>) this.Document).Current.DisableAutomaticDiscountCalculation;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = row.IsFree;
        if (!nullable1.GetValueOrDefault())
          row.ManualDisc = new bool?(true);
      }
    }
    if (sender.Graph.IsCopyPasteContext)
    {
      nullable1 = row.RequireLocation;
      bool flag = false;
      if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
        row.LocationID = new int?();
      nullable1 = row.ManualDisc;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = row.IsFree;
        if (nullable1.GetValueOrDefault())
        {
          this.ResetQtyOnFreeItem(sender, row);
          this._discountEngine.ClearDiscount(sender, row);
        }
      }
      this.RecalculateDiscounts(sender, row);
      nullable1 = row.ManualDisc;
      if (!nullable1.GetValueOrDefault())
      {
        ARDiscount arDiscount = (ARDiscount) PXSelectorAttribute.Select<SOLine.discountID>(sender, (object) row);
        SOLine soLine = row;
        Decimal? nullable2;
        if (arDiscount != null)
        {
          nullable1 = arDiscount.IsAppliedToDR;
          if (nullable1.GetValueOrDefault())
          {
            nullable2 = row.DiscPct;
            goto label_16;
          }
        }
        nullable2 = new Decimal?(0.0M);
label_16:
        soLine.DiscPctDR = nullable2;
      }
      row.ManualPrice = new bool?(true);
      TaxBaseAttribute.Calculate<SOLine.taxCategoryID>(sender, e);
      DirtyFormulaAttribute.RaiseRowUpdated<SOLine.openLine>(sender, new PXRowUpdatedEventArgs(e.Row, (object) new SOLine(), e.ExternalCall));
    }
    else
    {
      nullable1 = row.SkipLineDiscountsBuffer;
      if (nullable1.HasValue)
        row.SkipLineDiscounts = row.SkipLineDiscountsBuffer;
      if (!((PXGraph) this).IsImportFromExcel)
        this.RecalculateDiscounts(sender, (SOLine) e.Row);
      TaxBaseAttribute.Calculate<SOLine.taxCategoryID>(sender, e);
    }
  }

  protected virtual void SOLine_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    SOLine row1 = (SOLine) e.Row;
    PXDefaultAttribute.SetPersistingCheck<SOLine.salesAcctID>(sender, e.Row, ((PXSelectBase<SOOrderType>) this.soordertype).Current == null || ((PXSelectBase<SOOrder>) this.Document).Current == null || ((PXSelectBase<SOOrderType>) this.soordertype).Current.ARDocType == "UND" || ((PXSelectBase<SOOrder>) this.Document).Current.Hold.GetValueOrDefault() ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXDefaultAttribute.SetPersistingCheck<SOLine.salesSubID>(sender, e.Row, ((PXSelectBase<SOOrderType>) this.soordertype).Current == null || ((PXSelectBase<SOOrder>) this.Document).Current == null || ((PXSelectBase<SOOrderType>) this.soordertype).Current.ARDocType == "UND" || ((PXSelectBase<SOOrder>) this.Document).Current.Hold.GetValueOrDefault() ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXCache pxCache1 = sender;
    object row2 = e.Row;
    bool? nullable1;
    int num1;
    if (((PXSelectBase<SOOrderType>) this.soordertype).Current != null && !((PXSelectBase<SOOrderType>) this.soordertype).Current.RequireLocation.GetValueOrDefault() && !(row1.LineType != "GI"))
    {
      nullable1 = row1.IsStockItem;
      bool? nullable2 = nullable1.HasValue ? new bool?(!nullable1.GetValueOrDefault()) : new bool?();
      if (nullable2.GetValueOrDefault())
      {
        nullable2 = row1.IsKit;
        if (nullable2.GetValueOrDefault())
          goto label_5;
      }
      num1 = 1;
      goto label_6;
    }
label_5:
    num1 = 2;
label_6:
    PXDefaultAttribute.SetPersistingCheck<SOLine.subItemID>(pxCache1, row2, (PXPersistingCheck) num1);
    PXCache pxCache2 = sender;
    object row3 = e.Row;
    nullable1 = row1.RequireReasonCode;
    int num2 = nullable1.GetValueOrDefault() ? 1 : 2;
    PXDefaultAttribute.SetPersistingCheck<SOLine.reasonCode>(pxCache2, row3, (PXPersistingCheck) num2);
    PXDefaultAttribute.SetPersistingCheck<SOLine.taskID>(sender, e.Row, ProjectDefaultAttribute.IsProject((PXGraph) this, ((SOLine) e.Row).ProjectID) ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    this.ItemAvailabilityExt.MemoCheck(row1, true);
  }

  protected virtual void SOLine_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    SOLine row = e.Row as SOLine;
    SOLine oldRow = e.OldRow as SOLine;
    bool? nullable1;
    if (row != null)
    {
      nullable1 = row.RequireLocation;
      bool flag = false;
      if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
        row.LocationID = new int?();
    }
    nullable1 = ((PXSelectBase<SOOrder>) this.Document).Current.DeferPriceDiscountRecalculation;
    if (nullable1.GetValueOrDefault() && !sender.ObjectsEqual<SOLine.taxCategoryID>((object) oldRow, (object) row))
      ((PXSelectBase<SOOrder>) this.Document).Current.IsPriceAndDiscountsValid = new bool?(false);
    if ((e.ExternalCall || sender.Graph.IsImport) && sender.ObjectsEqual<SOLine.customerID>(e.Row, e.OldRow) && sender.ObjectsEqual<SOLine.inventoryID>(e.Row, e.OldRow) && sender.ObjectsEqual<SOLine.uOM>(e.Row, e.OldRow) && sender.ObjectsEqual<SOLine.orderQty>(e.Row, e.OldRow) && sender.ObjectsEqual<SOLine.branchID>(e.Row, e.OldRow) && sender.ObjectsEqual<SOLine.siteID>(e.Row, e.OldRow) && sender.ObjectsEqual<SOLine.lotSerialNbr>(e.Row, e.OldRow) && sender.ObjectsEqual<SOLine.manualPrice>(e.Row, e.OldRow) && (!sender.ObjectsEqual<SOLine.curyUnitPrice>(e.Row, e.OldRow) || !sender.ObjectsEqual<SOLine.curyExtPrice>(e.Row, e.OldRow)))
    {
      row.ManualPrice = new bool?(true);
      sender.SetValue<SOLine.skipLineDiscounts>((object) row, (object) false);
      sender.SetValueExt<SOLine.priceType>((object) row, (object) null);
      sender.SetValueExt<SOLine.isPromotionalPrice>((object) row, (object) false);
    }
    else
    {
      nullable1 = row.SkipLineDiscountsBuffer;
      if (nullable1.HasValue)
      {
        sender.SetValue<SOLine.skipLineDiscounts>((object) row, (object) row.SkipLineDiscountsBuffer);
        SOLine soLine = row;
        nullable1 = new bool?();
        bool? nullable2 = nullable1;
        soLine.SkipLineDiscountsBuffer = nullable2;
      }
    }
    if (!sender.ObjectsEqual<SOLine.branchID>(e.Row, e.OldRow) || !sender.ObjectsEqual<SOLine.customerID>(e.Row, e.OldRow) || !sender.ObjectsEqual<SOLine.inventoryID>(e.Row, e.OldRow) || !sender.ObjectsEqual<SOLine.siteID>(e.Row, e.OldRow) || !sender.ObjectsEqual<SOLine.baseOrderQty>(e.Row, e.OldRow) || !sender.ObjectsEqual<SOLine.isFree>(e.Row, e.OldRow) || !sender.ObjectsEqual<SOLine.curyUnitPrice>(e.Row, e.OldRow) || !sender.ObjectsEqual<SOLine.curyExtPrice>(e.Row, e.OldRow) || !sender.ObjectsEqual<SOLine.curyLineAmt>(e.Row, e.OldRow) || !sender.ObjectsEqual<SOLine.curyDiscAmt>(e.Row, e.OldRow) || !sender.ObjectsEqual<SOLine.discPct>(e.Row, e.OldRow) || !sender.ObjectsEqual<SOLine.skipLineDiscounts>(e.Row, e.OldRow) || !sender.ObjectsEqual<SOLine.manualDisc>(e.Row, e.OldRow) || !sender.ObjectsEqual<SOLine.discountID>(e.Row, e.OldRow))
    {
      nullable1 = row.ManualDisc;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = oldRow.ManualDisc;
        if (nullable1.GetValueOrDefault())
        {
          nullable1 = row.IsFree;
          if (nullable1.GetValueOrDefault())
            this.ResetQtyOnFreeItem(sender, row);
        }
        nullable1 = row.IsFree;
        if (nullable1.GetValueOrDefault())
          this._discountEngine.ClearDiscount(sender, row);
        this.RecalculateDiscounts(sender, row);
      }
      else
        this.RecalculateDiscounts(sender, row, oldRow);
    }
    nullable1 = row.ManualDisc;
    if (!nullable1.GetValueOrDefault())
    {
      ARDiscount arDiscount = (ARDiscount) PXSelectorAttribute.Select<SOLine.discountID>(sender, (object) row);
      SOLine soLine = row;
      Decimal? nullable3;
      if (arDiscount != null)
      {
        nullable1 = arDiscount.IsAppliedToDR;
        if (nullable1.GetValueOrDefault())
        {
          nullable3 = row.DiscPct;
          goto label_23;
        }
      }
      nullable3 = new Decimal?(0.0M);
label_23:
      soLine.DiscPctDR = nullable3;
    }
    nullable1 = row.ManualPrice;
    if (!nullable1.GetValueOrDefault())
      row.CuryUnitPriceDR = row.CuryUnitPrice;
    TaxBaseAttribute.Calculate<SOLine.taxCategoryID>(sender, e);
    DirtyFormulaAttribute.RaiseRowUpdated<SOLine.openLine>(sender, e);
    if ((e.ExternalCall || sender.Graph.IsImport) && !sender.ObjectsEqual<SOLine.completed>(e.Row, e.OldRow))
    {
      nullable1 = ((SOLine) e.Row).Completed;
      if (!nullable1.GetValueOrDefault() && ((SOLine) e.Row).ShipComplete != "B")
      {
        foreach (SOLineSplit selectChild in PXParentAttribute.SelectChildren(((PXSelectBase) this.splits).Cache, e.Row, typeof (SOLine)))
        {
          if (selectChild.ShipmentNbr == null)
          {
            Decimal? shippedQty = selectChild.ShippedQty;
            Decimal num = 0M;
            if (!(shippedQty.GetValueOrDefault() > num & shippedQty.HasValue))
              continue;
          }
          sender.SetValueExt<SOLine.shipComplete>(e.Row, (object) "B");
        }
      }
    }
    if (!sender.Graph.IsMobile)
      return;
    object obj = sender.Locate(e.Row);
    sender.Current = obj;
  }

  protected virtual void SOLine_RowDeleting(PXCache sedner, PXRowDeletingEventArgs e)
  {
    if (e.Row is SOLine row)
    {
      Decimal? shippedQty = row.ShippedQty;
      Decimal num = 0M;
      if (shippedQty.GetValueOrDefault() > num & shippedQty.HasValue || ((IEnumerable<PXResult<SOLineSplit>>) ((PXSelectBase<SOLineSplit>) this.splits).Select(Array.Empty<object>())).AsEnumerable<PXResult<SOLineSplit>>().Where<PXResult<SOLineSplit>>((Func<PXResult<SOLineSplit>, bool>) (x => PXResult<SOLineSplit>.op_Implicit(x).ShipmentNbr != null)).Count<PXResult<SOLineSplit>>() > 0)
        throw new PXException("The line cannot be deleted because it is linked to a shipment.");
    }
    if (!(row?.LineType == "MI"))
      return;
    PX.Objects.AR.ARTran arTran = PXResultset<PX.Objects.AR.ARTran>.op_Implicit(PXSelectBase<PX.Objects.AR.ARTran, PXViewOf<PX.Objects.AR.ARTran>.BasedOn<SelectFromBase<PX.Objects.AR.ARTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<PX.Objects.AR.ARTran.sOOrderType>.IsRelatedTo<SOLine.orderType>, Field<PX.Objects.AR.ARTran.sOOrderNbr>.IsRelatedTo<SOLine.orderNbr>, Field<PX.Objects.AR.ARTran.sOOrderLineNbr>.IsRelatedTo<SOLine.lineNbr>>.WithTablesOf<SOLine, PX.Objects.AR.ARTran>, SOLine, PX.Objects.AR.ARTran>.SameAsCurrent>, And<BqlOperand<PX.Objects.AR.ARTran.canceled, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<PX.Objects.AR.ARTran.isCancellation, IBqlBool>.IsEqual<False>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) new SOLine[1]
    {
      row
    }, Array.Empty<object>()));
    if (arTran != null)
      throw new PXException("The line cannot be deleted because it is linked to the {0} invoice.", new object[1]
      {
        (object) arTran.RefNbr
      });
  }

  protected virtual void SOLineSplit_LotSerialNbr_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    SOLineSplit row = (SOLineSplit) e.Row;
    if (row == null)
      return;
    SOOrderType current = ((PXSelectBase<SOOrderType>) this.soordertype).Current;
    if ((current != null ? (current.RequireShipping.GetValueOrDefault() ? 1 : 0) : 0) == 0 || !(((PXSelectBase<SOOrderType>) this.soordertype).Current.INDocType != "UND") || !(row.Operation == "I") || row.LotSerialNbr == null)
      return;
    INLotSerClass inLotSerClass = INLotSerClass.PK.Find((PXGraph) this, PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID)?.LotSerClassID);
    if (inLotSerClass == null || !(inLotSerClass.LotSerAssign != "R"))
      return;
    ((PXSelectBase) this.splits).Cache.RaiseExceptionHandling<SOLineSplit.lotSerialNbr>((object) row, (object) null, (Exception) new PXSetPropertyException("Lot/Serial Nbr. can be selected for items with When Received assignment method only.", (PXErrorLevel) 2));
    row.LotSerialNbr = (string) null;
  }

  protected virtual void SOLineSplit_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (e.Row is SOLineSplit row)
    {
      Decimal? shippedQty = row.ShippedQty;
      Decimal num = 0M;
      if (shippedQty.GetValueOrDefault() > num & shippedQty.HasValue || row.ShipmentNbr != null)
        throw new PXException("The line cannot be deleted because it is linked to a shipment.");
    }
    if (row.POSource != "D" && this.CheckPOLinked(row, sender))
      throw new PXException("The line cannot be deleted because it is linked to a purchase order.");
  }

  /// <summary>
  /// Checks if SOLineSplit line is linked to a purchase order.
  /// Returns true if it is linked to a PO
  /// </summary>
  protected virtual bool CheckPOLinked(SOLineSplit row, PXCache cache)
  {
    if (row != null)
    {
      Decimal? receivedQty = row.ReceivedQty;
      Decimal num = 0M;
      if ((receivedQty.GetValueOrDefault() > num & receivedQty.HasValue || row.PONbr != null) && PXParentAttribute.SelectParent<SOLine>(cache, (object) row) != null)
        return true;
    }
    return false;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (SOLineSplit.sOOrderNbr))]
  [PXDBDefault(typeof (SOOrder.orderNbr))]
  protected virtual void SOLineSplit2_SOOrderNbr_CacheAttached(PXCache sender)
  {
  }

  protected virtual void SOLineSplit_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    SOOrder current = ((PXSelectBase<SOOrder>) this.Document).Current;
    if ((current != null ? (!current.IsTransferOrder.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      return;
    PXView view = ((PXSelectBase) this.sodemand).View;
    object[] objArray1 = new object[1]{ e.Row };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<SOLineSplit2, INItemPlan> pxResult in view.SelectMultiBound(objArray1, objArray2))
    {
      SOLineSplit2 copy = PXCache<SOLineSplit2>.CreateCopy(PXResult<SOLineSplit2, INItemPlan>.op_Implicit(pxResult));
      INItemPlan inItemPlan = PXResult<SOLineSplit2, INItemPlan>.op_Implicit(pxResult);
      copy.SOOrderType = (string) null;
      copy.SOOrderNbr = (string) null;
      copy.SOLineNbr = new int?();
      copy.SOSplitLineNbr = new int?();
      copy.RefNoteID = new Guid?();
      copy.CostCenterID = inItemPlan.CostCenterID;
      SOLineSplit2 soLineSplit2 = ((PXSelectBase<SOLineSplit2>) this.sodemand).Update(copy);
      if (inItemPlan.PlanType != null)
      {
        inItemPlan.SiteID = soLineSplit2.SiteID;
        inItemPlan.SourceSiteID = soLineSplit2.SiteID;
        inItemPlan.PlanType = soLineSplit2.IsAllocated.GetValueOrDefault() ? "61" : "60";
        inItemPlan.SupplyPlanID = new long?();
        inItemPlan.FixedSource = "T";
        sender.Graph.Caches[typeof (INItemPlan)].Update((object) inItemPlan);
      }
    }
    SOLineSplit row = (SOLineSplit) e.Row;
    if (row == null || !row.PlanID.HasValue)
      return;
    INItemPlan inItemPlan1 = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelectReadonly<INItemPlan, Where<INItemPlan.supplyPlanID, Equal<Required<INItemPlan.supplyPlanID>>, And<INItemPlan.planType, Equal<INPlanConstants.plan94>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.PlanID
    }));
    if (inItemPlan1 == null)
      return;
    ((PXGraph) this).Caches[typeof (INItemPlan)].Delete((object) inItemPlan1);
  }

  protected virtual void SOLine_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    SOLine row = e.Row as SOLine;
    if (((PXSelectBase<SOOrder>) this.Document).Current != null && ((PXSelectBase) this.Document).Cache.GetStatus((object) ((PXSelectBase<SOOrder>) this.Document).Current) != 3 && ((PXSelectBase) this.Document).Cache.GetStatus((object) ((PXSelectBase<SOOrder>) this.Document).Current) != 4)
    {
      bool? nullable = row.IsFree;
      if (nullable.GetValueOrDefault())
      {
        nullable = row.ManualDisc;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
          goto label_7;
      }
      if (!this.DisableGroupDocDiscount)
      {
        try
        {
          ((PXSelectBase<SOOrder>) this.Document).Current.DeferPriceDiscountRecalculation = new bool?(false);
          this._discountEngine.RecalculateGroupAndDocumentDiscounts(sender, (PXSelectBase<SOLine>) this.Transactions, (SOLine) null, (PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails, ((PXSelectBase<SOOrder>) this.Document).Current.BranchID, ((PXSelectBase<SOOrder>) this.Document).Current.CustomerLocationID, ((PXSelectBase<SOOrder>) this.Document).Current.OrderDate, this.GetDefaultSODiscountCalculationOptions(((PXSelectBase<SOOrder>) this.Document).Current, true));
        }
        finally
        {
          ((PXSelectBase<SOOrder>) this.Document).Current.DeferPriceDiscountRecalculation = ((PXSelectBase<SOOrderType>) this.soordertype).Current.DeferPriceDiscountRecalculation;
        }
      }
      this.RecalculateTotalDiscount();
      this.RefreshFreeItemLines(sender);
    }
label_7:
    if (((PXSelectBase<SOOrder>) this.Document).Current == null)
      return;
    GraphHelper.MarkUpdated(((PXSelectBase) this.Document).Cache, (object) ((PXSelectBase<SOOrder>) this.Document).Current, true);
  }

  protected virtual void SOLine_AvgCost_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    Decimal? nullable1 = (Decimal?) sender.GetValue<SOLine.avgCost>(e.Row);
    if (!nullable1.HasValue)
    {
      SOLine row = (SOLine) e.Row;
      INItemStats inItemStats = PXResultset<INItemStats>.op_Implicit(((PXSelectBase<INItemStats>) this.initemstats).Select(new object[2]
      {
        (object) row.InventoryID,
        (object) row.SiteID
      }));
      if (inItemStats == null)
        return;
      SOLine soLine = row;
      Decimal? qtyOnHand1 = inItemStats.QtyOnHand;
      Decimal num = 0M;
      Decimal? nullable2;
      if (!(qtyOnHand1.GetValueOrDefault() == num & qtyOnHand1.HasValue))
      {
        Decimal? totalCost = inItemStats.TotalCost;
        Decimal? qtyOnHand2 = inItemStats.QtyOnHand;
        nullable2 = totalCost.HasValue & qtyOnHand2.HasValue ? new Decimal?(totalCost.GetValueOrDefault() / qtyOnHand2.GetValueOrDefault()) : new Decimal?();
      }
      else
        nullable2 = new Decimal?();
      soLine.AvgCost = nullable2;
      nullable1 = row.AvgCost;
      if (!nullable1.HasValue)
        return;
    }
    nullable1 = new Decimal?(INUnitAttribute.ConvertToBase<SOLine.inventoryID, SOLine.uOM>(sender, e.Row, nullable1.Value, INPrecision.UNITCOST));
    if (!sender.Graph.Accessinfo.CuryViewState)
    {
      Decimal curyval;
      PX.Objects.CM.PXCurrencyAttribute.CuryConvCury(sender, e.Row, nullable1.Value, out curyval, CommonSetupDecPl.PrcCst);
      e.ReturnValue = (object) curyval;
    }
    else
      e.ReturnValue = (object) nullable1;
  }

  protected virtual void SOLineValidateMinGrossProfit(
    PXCache sender,
    SOLine row,
    SOOrderEntry.MinGrossProfitClass mgpc)
  {
    this.SOLineValidateMinGrossProfit(sender, row, mgpc, true);
  }

  protected virtual void SOLineValidateMinGrossProfit(
    PXCache sender,
    SOLine row,
    SOOrderEntry.MinGrossProfitClass mgpc,
    bool isPriceTypeValidationNeeded)
  {
    if (row == null)
      return;
    string validationOption = this.GetMinGrossProfitValidationOption(sender, row, isPriceTypeValidationNeeded);
    if (validationOption == "N")
      return;
    bool? nullable1 = row.IsFree;
    if (nullable1.GetValueOrDefault() || sender.Graph.UnattendedMode)
      return;
    SOOrder current = ((PXSelectBase<SOOrder>) this.Document).Current;
    int num1;
    if (current == null)
    {
      num1 = 0;
    }
    else
    {
      nullable1 = current.IsTransferOrder;
      num1 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    if (num1 != 0 || row.Operation == "R")
      return;
    int? nullable2 = row.InventoryID;
    if (!nullable2.HasValue || row.UOM == null)
      return;
    Decimal? curyUnitPrice = mgpc.CuryUnitPrice;
    Decimal num2 = 0M;
    if (!(curyUnitPrice.GetValueOrDefault() >= num2 & curyUnitPrice.HasValue))
      return;
    nullable2 = row.BranchID;
    if (!nullable2.HasValue)
      return;
    PX.Objects.IN.InventoryItem inItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
    PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this, row.BranchID);
    INItemCost inItemCost = PXResultset<INItemCost>.op_Implicit(((PXSelectBase<INItemCost>) this.initemcost).Select(new object[2]
    {
      (object) row.InventoryID,
      (object) branch.BaseCuryID
    }));
    mgpc.CuryUnitPrice = MinGrossProfitValidator<SOLine>.ValidateUnitPrice<SOLine.curyInfoID, SOLine.inventoryID, SOLine.uOM>(sender, row, inItem, inItemCost, mgpc.CuryUnitPrice, validationOption);
    Decimal? nullable3 = mgpc.DiscPct;
    Decimal num3 = 0M;
    if (!(nullable3.GetValueOrDefault() == num3 & nullable3.HasValue))
      mgpc.DiscPct = MinGrossProfitValidator<SOLine>.ValidateDiscountPct<SOLine.inventoryID, SOLine.uOM>(sender, row, inItem, inItemCost, row.UnitPrice, mgpc.DiscPct, validationOption);
    nullable3 = mgpc.CuryDiscAmt;
    Decimal num4 = 0M;
    if (nullable3.GetValueOrDefault() == num4 & nullable3.HasValue)
      return;
    nullable3 = row.Qty;
    if (!nullable3.HasValue)
      return;
    nullable3 = row.Qty;
    if (!(Math.Abs(nullable3.GetValueOrDefault()) != 0M))
      return;
    mgpc.CuryDiscAmt = MinGrossProfitValidator<SOLine>.ValidateDiscountAmt<SOLine.inventoryID, SOLine.uOM>(sender, row, inItem, inItemCost, row.UnitPrice, mgpc.CuryDiscAmt, validationOption);
  }

  public virtual string GetMinGrossProfitValidationOption(PXCache sender, SOLine row)
  {
    return this.GetMinGrossProfitValidationOption(sender, row, true);
  }

  public virtual string GetMinGrossProfitValidationOption(
    PXCache sender,
    SOLine row,
    bool isPriceTypeValidationNeeded)
  {
    return isPriceTypeValidationNeeded && (row.IsPromotionalPrice.GetValueOrDefault() && ((PXSelectBase<SOSetup>) this.sosetup).Current.IgnoreMinGrossProfitPromotionalPrice.GetValueOrDefault() || row.PriceType == "C" && ((PXSelectBase<SOSetup>) this.sosetup).Current.IgnoreMinGrossProfitCustomerPrice.GetValueOrDefault() || row.PriceType == "P" && ((PXSelectBase<SOSetup>) this.sosetup).Current.IgnoreMinGrossProfitCustomerPriceClass.GetValueOrDefault()) ? "N" : ((PXSelectBase<SOSetup>) this.sosetup).Current.MinGrossProfitValidation;
  }

  protected virtual void SOOrderDiscountDetail_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    SOOrderDiscountDetail row = (SOOrderDiscountDetail) e.Row;
  }

  protected virtual void SOOrderDiscountDetail_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    SOOrderDiscountDetail row = (SOOrderDiscountDetail) e.Row;
    if (!this._discountEngine.IsInternalDiscountEngineCall)
    {
      if (row != null)
      {
        try
        {
          ((PXSelectBase<SOOrder>) this.Document).Current.DeferPriceDiscountRecalculation = new bool?(false);
          if (row.DiscountID != null)
          {
            this._discountEngine.InsertManualDocGroupDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<SOLine>) this.Transactions, (PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails, row, row.DiscountID, (string) null, ((PXSelectBase<SOOrder>) this.Document).Current.BranchID, ((PXSelectBase<SOOrder>) this.Document).Current.CustomerLocationID, ((PXSelectBase<SOOrder>) this.Document).Current.OrderDate, this.GetDefaultSODiscountCalculationOptions(((PXSelectBase<SOOrder>) this.Document).Current, true));
            this.RefreshTotalsAndFreeItems(sender);
          }
          if (this._discountEngine.SetExternalManualDocDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<SOLine>) this.Transactions, (PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails, row, (SOOrderDiscountDetail) null, this.GetDefaultSODiscountCalculationOptions(((PXSelectBase<SOOrder>) this.Document).Current, true)))
            this.RecalculateTotalDiscount();
        }
        finally
        {
          ((PXSelectBase<SOOrder>) this.Document).Current.DeferPriceDiscountRecalculation = ((PXSelectBase<SOOrderType>) this.soordertype).Current.DeferPriceDiscountRecalculation;
        }
      }
    }
    if (row == null || row.DiscountID == null || row.DiscountSequenceID == null || row.Description != null)
      return;
    object obj = (object) null;
    sender.RaiseFieldDefaulting<SOOrderDiscountDetail.description>((object) row, ref obj);
    sender.SetValue<SOOrderDiscountDetail.description>((object) row, obj);
  }

  protected virtual void SOOrderDiscountDetail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    SOOrderDiscountDetail row = (SOOrderDiscountDetail) e.Row;
    SOOrderDiscountDetail oldRow = (SOOrderDiscountDetail) e.OldRow;
    if (this._discountEngine.IsInternalDiscountEngineCall)
      return;
    if (row == null)
      return;
    try
    {
      ((PXSelectBase<SOOrder>) this.Document).Current.DeferPriceDiscountRecalculation = new bool?(false);
      if (!sender.ObjectsEqual<SOOrderDiscountDetail.skipDiscount>(e.Row, e.OldRow))
      {
        this._discountEngine.UpdateDocumentDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<SOLine>) this.Transactions, (PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails, ((PXSelectBase<SOOrder>) this.Document).Current.BranchID, ((PXSelectBase<SOOrder>) this.Document).Current.CustomerLocationID, ((PXSelectBase<SOOrder>) this.Document).Current.OrderDate, row.Type != "D", this.GetDefaultSODiscountCalculationOptions(((PXSelectBase<SOOrder>) this.Document).Current, true));
        this.RefreshTotalsAndFreeItems(sender);
      }
      if (!sender.ObjectsEqual<SOOrderDiscountDetail.discountID>(e.Row, e.OldRow) || !sender.ObjectsEqual<SOOrderDiscountDetail.discountSequenceID>(e.Row, e.OldRow))
      {
        this._discountEngine.UpdateManualDocGroupDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<SOLine>) this.Transactions, (PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails, row, row.DiscountID, sender.ObjectsEqual<SOOrderDiscountDetail.discountID>(e.Row, e.OldRow) ? row.DiscountSequenceID : (string) null, ((PXSelectBase<SOOrder>) this.Document).Current.BranchID, ((PXSelectBase<SOOrder>) this.Document).Current.CustomerLocationID, ((PXSelectBase<SOOrder>) this.Document).Current.OrderDate, this.GetDefaultSODiscountCalculationOptions(((PXSelectBase<SOOrder>) this.Document).Current, true));
        this.RefreshTotalsAndFreeItems(sender);
      }
      if (!this._discountEngine.SetExternalManualDocDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<SOLine>) this.Transactions, (PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails, row, oldRow, this.GetDefaultSODiscountCalculationOptions(((PXSelectBase<SOOrder>) this.Document).Current, true)))
        return;
      this.RecalculateTotalDiscount();
    }
    finally
    {
      ((PXSelectBase<SOOrder>) this.Document).Current.DeferPriceDiscountRecalculation = ((PXSelectBase<SOOrderType>) this.soordertype).Current.DeferPriceDiscountRecalculation;
    }
  }

  protected virtual void SOOrderDiscountDetail_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    SOOrderDiscountDetail row = (SOOrderDiscountDetail) e.Row;
    if (!this._discountEngine.IsInternalDiscountEngineCall && row != null)
    {
      if (!this.DisableGroupDocDiscount)
      {
        try
        {
          ((PXSelectBase<SOOrder>) this.Document).Current.DeferPriceDiscountRecalculation = new bool?(false);
          this._discountEngine.UpdateDocumentDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<SOLine>) this.Transactions, (PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails, ((PXSelectBase<SOOrder>) this.Document).Current.BranchID, ((PXSelectBase<SOOrder>) this.Document).Current.CustomerLocationID, ((PXSelectBase<SOOrder>) this.Document).Current.OrderDate, row.Type != null && row.Type != "D" && row.Type != "B", this.GetDefaultSODiscountCalculationOptions(((PXSelectBase<SOOrder>) this.Document).Current, true));
        }
        finally
        {
          ((PXSelectBase<SOOrder>) this.Document).Current.DeferPriceDiscountRecalculation = ((PXSelectBase<SOOrderType>) this.soordertype).Current.DeferPriceDiscountRecalculation;
        }
      }
    }
    if (this._discountEngine.IsInternalDiscountEngineCall && ((PXSelectBase<SOOrder>) this.Document).Current != null && ((PXSelectBase<SOOrder>) this.Document).Current.DisableAutomaticDiscountCalculation.GetValueOrDefault())
      ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<SOOrder.disableAutomaticDiscountCalculation>((object) ((PXSelectBase<SOOrder>) this.Document).Current, (object) ((PXSelectBase<SOOrder>) this.Document).Current.DisableAutomaticDiscountCalculation, (Exception) new PXSetPropertyException("All discounts that are not applicable to the current document have been deleted.", (PXErrorLevel) 2));
    this.RefreshTotalsAndFreeItems(sender);
  }

  protected virtual void SOOrderDiscountDetail_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    SOOrderDiscountDetail row = (SOOrderDiscountDetail) e.Row;
    bool flag = row.Type == "B";
    PXDefaultAttribute.SetPersistingCheck<SOOrderDiscountDetail.discountID>(sender, (object) row, flag ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXDefaultAttribute.SetPersistingCheck<SOOrderDiscountDetail.discountSequenceID>(sender, (object) row, flag ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (SOOrder.orderNbr))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<ARPaymentTotals.adjdOrderNbr> eventArgs)
  {
  }

  [PXDefault]
  [PXUIField(DisplayName = "Customer Tax Zone", Enabled = false)]
  [PXMergeAttributes]
  protected virtual void SOTaxTran_TaxZoneID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void SOTaxTran_TaxZoneID_ExceptionHandling(
    PXCache sender,
    PXExceptionHandlingEventArgs e)
  {
    Exception exception = (Exception) (e.Exception as PXSetPropertyException);
    if (exception == null)
      return;
    ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<SOOrder.taxZoneID>((object) ((PXSelectBase<SOOrder>) this.Document).Current, (object) null, exception);
  }

  protected virtual void SOTaxTran_TaxZoneID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<SOOrder>) this.Document).Current == null || !(((PXSelectBase<SOOrder>) this.Document).Current.Behavior != "BL"))
      return;
    e.NewValue = (object) ((PXSelectBase<SOOrder>) this.Document).Current.TaxZoneID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOTaxTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is SOTaxTran))
      return;
    PXUIFieldAttribute.SetEnabled<SOTaxTran.taxID>(sender, e.Row, sender.GetStatus(e.Row) == 2);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SOShippingAddress> e)
  {
    SOShippingAddress row = e.Row;
    SOShippingAddress oldRow = e.OldRow;
    if (row == null || !(oldRow?.CountryID != row.CountryID) && !(oldRow?.PostalCode != row.PostalCode) && !(oldRow?.State != row.State))
      return;
    ((PXSelectBase) this.Document).Cache.SetDefaultExt<SOOrder.taxZoneID>((object) ((PXSelectBase<SOOrder>) this.Document).Current);
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, ((PXSelectBase<SOOrder>) this.Document).Current.ShipVia);
    if ((carrier != null ? (carrier.IsExternal.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ((PXSelectBase<SOOrder>) this.Document).Current.FreightCostIsValid = new bool?(false);
  }

  protected virtual void SOPackageInfoEx_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is SOPackageInfo row) || ((PXSelectBase<SOOrder>) this.Document).Current == null)
      return;
    row.WeightUOM = ((PXSelectBase<CommonSetup>) this.commonsetup).Current.WeightUOM;
    PXUIFieldAttribute.SetEnabled<SOPackageInfo.inventoryID>(sender, e.Row, ((PXSelectBase<SOOrder>) this.Document).Current.IsManualPackage.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<SOPackageInfo.siteID>(sender, e.Row, ((PXSelectBase<SOOrder>) this.Document).Current.IsManualPackage.GetValueOrDefault());
  }

  protected virtual void SOPackageInfoEx_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is SOPackageInfo row))
      return;
    PXCache pxCache = sender;
    int num = PXAccess.FeatureInstalled<FeaturesSet.inventory>() ? 1 : 2;
    PXDefaultAttribute.SetPersistingCheck<SOPackageInfo.siteID>(pxCache, (object) row, (PXPersistingCheck) num);
  }

  protected virtual void SOPackageInfoEx_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is SOPackageInfo row))
      return;
    PX.Objects.CS.CSBox csBox = PXResultset<PX.Objects.CS.CSBox>.op_Implicit(PXSelectBase<PX.Objects.CS.CSBox, PXSelect<PX.Objects.CS.CSBox, Where<PX.Objects.CS.CSBox.boxID, Equal<Required<PX.Objects.CS.CSBox.boxID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.BoxID
    }));
    if (csBox == null)
      return;
    Decimal? maxWeight = csBox.MaxWeight;
    Decimal? grossWeight = row.GrossWeight;
    if (!(maxWeight.GetValueOrDefault() < grossWeight.GetValueOrDefault() & maxWeight.HasValue & grossWeight.HasValue))
      return;
    sender.RaiseExceptionHandling<SOPackageInfo.grossWeight>((object) row, (object) row.GrossWeight, (Exception) new PXSetPropertyException("The weight specified exceeds the max. weight of the box. Choose a bigger box or use multiple boxes."));
  }

  protected virtual void RecalculateDiscounts(PXCache sender, SOLine line)
  {
    this.RecalculateDiscounts(sender, line, (SOLine) null);
  }

  protected virtual void RecalculateDiscounts(PXCache sender, SOLine line, SOLine oldline)
  {
    DiscountEngine.DiscountCalculationOptions calculationOptions = this.GetDefaultSODiscountCalculationOptions(((PXSelectBase<SOOrder>) this.Document).Current);
    if (line.CalculateDiscountsOnImport.GetValueOrDefault())
      calculationOptions |= DiscountEngine.DiscountCalculationOptions.CalculateDiscountsFromImport;
    if (PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>() && (line.InventoryID.HasValue || sender.Graph.IsImportFromExcel && !line.SkipDisc.GetValueOrDefault()) && line.Qty.HasValue && line.CuryLineAmt.HasValue && (!line.IsFree.GetValueOrDefault() || oldline != null && !sender.ObjectsEqual<SOLine.isFree>((object) line, (object) oldline)))
    {
      this._discountEngine.SetDiscounts(sender, (PXSelectBase<SOLine>) this.Transactions, line, (PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails, ((PXSelectBase<SOOrder>) this.Document).Current.BranchID, ((PXSelectBase<SOOrder>) this.Document).Current.CustomerLocationID, ((PXSelectBase<SOOrder>) this.Document).Current.CuryID, ((PXSelectBase<SOOrder>) this.Document).Current.OrderDate, ((PXSelectBase<RecalcDiscountsParamFilter>) this.recalcdiscountsfilter).Current, calculationOptions);
      this.RecalculateTotalDiscount();
      this.RefreshFreeItemLines(sender);
    }
    else
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>() || ((PXSelectBase<SOOrder>) this.Document).Current == null)
        return;
      this._discountEngine.CalculateDocumentDiscountRate(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<SOLine>) this.Transactions, line, (PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails, calculationOptions);
    }
  }

  public virtual DiscountEngine.DiscountCalculationOptions GetDefaultSODiscountCalculationOptions(
    SOOrder doc)
  {
    return this.GetDefaultSODiscountCalculationOptions(doc, false);
  }

  public virtual DiscountEngine.DiscountCalculationOptions GetDefaultSODiscountCalculationOptions(
    SOOrder doc,
    bool doNotDeferDiscountCalculation)
  {
    int num1 = 16 /*0x10*/ | (this.DisableGroupDocDiscount ? 2 : 0);
    bool? nullable;
    int num2;
    if (doc != null)
    {
      nullable = doc.DisableAutomaticDiscountCalculation;
      if (nullable.GetValueOrDefault())
      {
        num2 = 4;
        goto label_4;
      }
    }
    num2 = 0;
label_4:
    DiscountEngine.DiscountCalculationOptions calculationOptions = (DiscountEngine.DiscountCalculationOptions) (num1 | num2);
    nullable = doc.DeferPriceDiscountRecalculation;
    if (!nullable.GetValueOrDefault() || doNotDeferDiscountCalculation)
      return calculationOptions;
    doc.IsPriceAndDiscountsValid = new bool?(false);
    return calculationOptions | DiscountEngine.DiscountCalculationOptions.DisablePriceCalculation | DiscountEngine.DiscountCalculationOptions.DisableGroupAndDocumentDiscounts | DiscountEngine.DiscountCalculationOptions.DisableARDiscountsCalculation | DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation;
  }

  protected virtual void RefreshFreeItemLines(PXCache sender)
  {
    if (sender.Graph.IsCopyPasteContext || sender.Graph.IsImportFromExcel)
      return;
    Dictionary<int, Decimal> dictionary1 = new Dictionary<int, Decimal>();
    Dictionary<int, string> dictionary2 = new Dictionary<int, string>();
    foreach (PXResult<SOOrderDiscountDetail, PX.Objects.IN.InventoryItem> pxResult in ((PXSelectBase<SOOrderDiscountDetail>) new PXSelectJoin<SOOrderDiscountDetail, InnerJoin<PX.Objects.IN.InventoryItem, On<SOOrderDiscountDetail.FK.FreeInventoryItem>>, Where<SOOrderDiscountDetail.orderType, Equal<Current<SOOrder.orderType>>, And<SOOrderDiscountDetail.orderNbr, Equal<Current<SOOrder.orderNbr>>, And<SOOrderDiscountDetail.skipDiscount, NotEqual<boolTrue>>>>>((PXGraph) this)).Select(Array.Empty<object>()))
    {
      SOOrderDiscountDetail orderDiscountDetail = PXResult<SOOrderDiscountDetail, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<SOOrderDiscountDetail, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      if (orderDiscountDetail.FreeItemID.HasValue)
      {
        if (dictionary1.ContainsKey(orderDiscountDetail.FreeItemID.Value))
        {
          dictionary1[orderDiscountDetail.FreeItemID.Value] += orderDiscountDetail.FreeItemQty.GetValueOrDefault();
        }
        else
        {
          dictionary1.Add(orderDiscountDetail.FreeItemID.Value, orderDiscountDetail.FreeItemQty.GetValueOrDefault());
          dictionary2.Add(inventoryItem.InventoryID.Value, inventoryItem.BaseUnit);
        }
      }
    }
    bool flag1 = false;
    foreach (PXResult<SOLine> pxResult in ((PXSelectBase<SOLine>) this.FreeItems).Select(Array.Empty<object>()))
    {
      SOLine soLine = PXResult<SOLine>.op_Implicit(pxResult);
      bool? manualDisc = soLine.ManualDisc;
      bool flag2 = false;
      if (manualDisc.GetValueOrDefault() == flag2 & manualDisc.HasValue)
      {
        int? inventoryId = soLine.InventoryID;
        if (inventoryId.HasValue)
        {
          Decimal? shippedQty = soLine.ShippedQty;
          Decimal num = 0M;
          if (shippedQty.GetValueOrDefault() == num & shippedQty.HasValue)
          {
            Dictionary<int, Decimal> dictionary3 = dictionary1;
            inventoryId = soLine.InventoryID;
            int key1 = inventoryId.Value;
            if (dictionary3.ContainsKey(key1))
            {
              Dictionary<int, Decimal> dictionary4 = dictionary1;
              inventoryId = soLine.InventoryID;
              int key2 = inventoryId.Value;
              if (dictionary4[key2] == 0M)
              {
                ((PXSelectBase<SOLine>) this.FreeItems).Delete(soLine);
                flag1 = true;
              }
            }
            else
            {
              ((PXSelectBase<SOLine>) this.FreeItems).Delete(soLine);
              flag1 = true;
            }
          }
          else
          {
            PXUIFieldAttribute.SetWarning<SOLine.orderQty>(((PXSelectBase) this.FreeItems).Cache, (object) soLine, "Applied free item discount was not recalculated because it has already been partially or completely shipped.");
            flag1 = true;
          }
        }
      }
    }
    int? defaultWarehouse = this.GetDefaultWarehouse();
    foreach (KeyValuePair<int, Decimal> keyValuePair in dictionary1)
    {
      SOLine current = ((PXSelectBase<SOLine>) this.Transactions).Current;
      SOLine freeLineByItemId = this.GetFreeLineByItemID(new int?(keyValuePair.Key));
      if (freeLineByItemId == null)
      {
        if (keyValuePair.Value > 0M)
        {
          SOLine soLine = new SOLine();
          soLine.InventoryID = new int?(keyValuePair.Key);
          soLine.IsFree = new bool?(true);
          soLine.SiteID = defaultWarehouse;
          soLine.OrderQty = new Decimal?(keyValuePair.Value);
          if (((PXSelectBase<ARSetup>) this.arsetup).Current.ApplyQuantityDiscountBy == "B")
            soLine.UOM = dictionary2[soLine.InventoryID.Value];
          ((PXSelectBase<SOLine>) this.FreeItems).Insert(soLine);
          flag1 = true;
        }
      }
      else
      {
        Decimal? nullable = freeLineByItemId.ShippedQty;
        Decimal num1 = 0M;
        if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
        {
          nullable = freeLineByItemId.OrderQty;
          Decimal num2 = keyValuePair.Value;
          if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
          {
            SOLine copy = PXCache<SOLine>.CreateCopy(freeLineByItemId);
            copy.OrderQty = new Decimal?(keyValuePair.Value);
            ((PXSelectBase) this.FreeItems).Cache.Update((object) copy);
            flag1 = true;
          }
        }
        else
        {
          PXUIFieldAttribute.SetWarning<SOLine.orderQty>(((PXSelectBase) this.FreeItems).Cache, (object) freeLineByItemId, "Applied free item discount was not recalculated because it has already been partially or completely shipped.");
          flag1 = true;
        }
      }
      if (current != null && current != ((PXSelectBase<SOLine>) this.Transactions).Current)
        ((PXSelectBase<SOLine>) this.Transactions).Current = current;
    }
    if (!flag1)
      return;
    ((PXSelectBase) this.Transactions).View.RequestRefresh();
  }

  private SOLine GetFreeLineByItemID(int? inventoryID)
  {
    return PXResultset<SOLine>.op_Implicit(PXSelectBase<SOLine, PXSelect<SOLine, Where<SOLine.orderType, Equal<Current<SOOrder.orderType>>, And<SOLine.orderNbr, Equal<Current<SOOrder.orderNbr>>, And<SOLine.isFree, Equal<boolTrue>, And<SOLine.inventoryID, Equal<Required<SOLine.inventoryID>>, And<SOLine.manualDisc, Equal<boolFalse>>>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) inventoryID
    }));
  }

  private void ResetQtyOnFreeItem(PXCache sender, SOLine line)
  {
    PXSelect<SOOrderDiscountDetail, Where<SOOrderDiscountDetail.orderType, Equal<Current<SOOrder.orderType>>, And<SOOrderDiscountDetail.orderNbr, Equal<Current<SOOrder.orderNbr>>, And<SOOrderDiscountDetail.freeItemID, Equal<Required<SOOrderDiscountDetail.freeItemID>>>>>> pxSelect = new PXSelect<SOOrderDiscountDetail, Where<SOOrderDiscountDetail.orderType, Equal<Current<SOOrder.orderType>>, And<SOOrderDiscountDetail.orderNbr, Equal<Current<SOOrder.orderNbr>>, And<SOOrderDiscountDetail.freeItemID, Equal<Required<SOOrderDiscountDetail.freeItemID>>>>>>((PXGraph) this);
    Decimal? nullable1 = new Decimal?(0M);
    object[] objArray = new object[1]
    {
      (object) line.InventoryID
    };
    foreach (PXResult<SOOrderDiscountDetail> pxResult in ((PXSelectBase<SOOrderDiscountDetail>) pxSelect).Select(objArray))
    {
      SOOrderDiscountDetail orderDiscountDetail = PXResult<SOOrderDiscountDetail>.op_Implicit(pxResult);
      if (!orderDiscountDetail.SkipDiscount.GetValueOrDefault() && orderDiscountDetail.FreeItemID.HasValue)
      {
        Decimal? nullable2 = orderDiscountDetail.FreeItemQty;
        if (nullable2.HasValue)
        {
          nullable2 = orderDiscountDetail.FreeItemQty;
          if (nullable2.Value > 0M)
          {
            nullable2 = nullable1;
            Decimal num = orderDiscountDetail.FreeItemQty.Value;
            nullable1 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num) : new Decimal?();
          }
        }
      }
    }
    sender.SetValueExt<SOLine.orderQty>((object) line, (object) nullable1);
  }

  /// <summary>
  /// If all lines are from one site/warehouse - return this warehouse otherwise null;
  /// </summary>
  /// <returns>Default Wartehouse for Free Item</returns>
  private int? GetDefaultWarehouse()
  {
    PXResultset<SOOrderSite> pxResultset = PXSelectBase<SOOrderSite, PXSelectJoin<SOOrderSite, InnerJoin<PX.Objects.IN.INSite, On<SOOrderSite.FK.Site>>, Where<SOOrderSite.orderType, Equal<Current<SOOrder.orderType>>, And<SOOrderSite.orderNbr, Equal<Current<SOOrder.orderNbr>>, And<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    return pxResultset.Count == 1 ? PXResultset<SOOrderSite>.op_Implicit(pxResultset).SiteID : new int?();
  }

  private void RecalculateTotalDiscount()
  {
    if (((PXSelectBase<SOOrder>) this.Document).Current == null || ((PXSelectBase) this.Document).Cache.GetStatus((object) ((PXSelectBase<SOOrder>) this.Document).Current) == 3 || ((PXSelectBase) this.Document).Cache.GetStatus((object) ((PXSelectBase<SOOrder>) this.Document).Current) == 4)
      return;
    SOOrder copy = PXCache<SOOrder>.CreateCopy(((PXSelectBase<SOOrder>) this.Document).Current);
    (Decimal groupDiscountTotal, Decimal documentDiscountTotal, Decimal discountTotal) discountTotals = this._discountEngine.GetDiscountTotals<SOOrderDiscountDetail>((PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails);
    ((PXSelectBase) this.Document).Cache.SetValueExt<SOOrder.curyGroupDiscTotal>((object) ((PXSelectBase<SOOrder>) this.Document).Current, (object) discountTotals.groupDiscountTotal);
    ((PXSelectBase) this.Document).Cache.SetValueExt<SOOrder.curyDocumentDiscTotal>((object) ((PXSelectBase<SOOrder>) this.Document).Current, (object) discountTotals.documentDiscountTotal);
    ((PXSelectBase) this.Document).Cache.SetValueExt<SOOrder.curyDiscTot>((object) ((PXSelectBase<SOOrder>) this.Document).Current, (object) discountTotals.discountTotal);
    ((PXSelectBase) this.Document).Cache.RaiseRowUpdated((object) ((PXSelectBase<SOOrder>) this.Document).Current, (object) copy);
  }

  private void RefreshTotalsAndFreeItems(PXCache sender)
  {
    this.RecalculateTotalDiscount();
    this.RefreshFreeItemLines(sender);
  }

  protected virtual bool CollectFreight
  {
    get
    {
      if (((PXSelectBase<SOOrder>) this.DocumentProperties).Current != null)
      {
        bool? nullable = ((PXSelectBase<SOOrder>) this.DocumentProperties).Current.UseCustomerAccount;
        if (nullable.GetValueOrDefault())
          return false;
        nullable = ((PXSelectBase<SOOrder>) this.DocumentProperties).Current.GroundCollect;
        if (nullable.GetValueOrDefault() && this.CanUseGroundCollect(((PXSelectBase<SOOrder>) this.DocumentProperties).Current))
          return false;
      }
      return true;
    }
  }

  private void CalculateFreightCost(bool supressErrors)
  {
    if (((PXSelectBase<SOOrder>) this.Document).Current.ShipVia == null)
      return;
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, ((PXSelectBase<SOOrder>) this.Document).Current.ShipVia);
    if (carrier == null || !carrier.IsExternal.GetValueOrDefault())
      return;
    bool? isActive = carrier.IsActive;
    bool flag = false;
    if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
      throw new PXException("The Ship Via code is not active.");
    ICarrierService result = CarrierPluginMaint.CreateCarrierService((PXGraph) this, CarrierPlugin.PK.Find((PXGraph) this, carrier.CarrierPluginID), true).Result;
    result.Method = carrier.PluginMethod;
    CarrierResult<RateQuote> rateQuote = result.GetRateQuote(this.CarrierRatesExt.BuildRateRequest(((PXSelectBase<SOOrder>) this.Document).Current));
    if (rateQuote == null)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    foreach (Message message in (IEnumerable<Message>) rateQuote.Messages)
      stringBuilder.AppendFormat("{0}:{1} ", (object) message.Code, (object) message.Description);
    if (rateQuote.IsSuccess)
    {
      this.SetFreightCost(this.ConvertAmtToBaseCury(rateQuote.Result.Currency, ((PXSelectBase<ARSetup>) this.arsetup).Current.DefaultRateTypeID, ((PXSelectBase<SOOrder>) this.Document).Current.OrderDate.Value, rateQuote.Result.Amount));
      if (rateQuote.Messages.Count <= 0)
        return;
      if (!supressErrors)
        ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<SOOrder.curyFreightCost>((object) ((PXSelectBase<SOOrder>) this.Document).Current, (object) ((PXSelectBase<SOOrder>) this.Document).Current.CuryFreightCost, (Exception) new PXSetPropertyException(stringBuilder.ToString(), (PXErrorLevel) 2));
      else
        PXTrace.WriteWarning(stringBuilder.ToString());
    }
    else
    {
      if (!supressErrors)
      {
        ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<SOOrder.curyFreightCost>((object) ((PXSelectBase<SOOrder>) this.Document).Current, (object) ((PXSelectBase<SOOrder>) this.Document).Current.CuryFreightCost, (Exception) new PXSetPropertyException("Carrier Service returned error. {0}", (PXErrorLevel) 4, new object[1]
        {
          (object) stringBuilder.ToString()
        }));
        throw new PXException("Carrier Service returned error. {0}", new object[1]
        {
          (object) stringBuilder.ToString()
        });
      }
      PXTrace.WriteError($"Carrier Service returned error. {stringBuilder.ToString()}");
    }
  }

  public virtual FreightCalculator CreateFreightCalculator()
  {
    return new FreightCalculator((PXGraph) this);
  }

  protected virtual void SetFreightCost(Decimal baseCost)
  {
    SOOrder copy = (SOOrder) ((PXSelectBase) this.Document).Cache.CreateCopy((object) ((PXSelectBase<SOOrder>) this.Document).Current);
    if (((PXSelectBase<SOOrderType>) this.soordertype).Current != null)
    {
      bool? calculateFreight = ((PXSelectBase<SOOrderType>) this.soordertype).Current.CalculateFreight;
      bool flag = false;
      if (calculateFreight.GetValueOrDefault() == flag & calculateFreight.HasValue)
      {
        copy.FreightCost = new Decimal?(0M);
        PX.Objects.CM.PXCurrencyAttribute.CuryConvCury<SOOrder.curyFreightCost>(((PXSelectBase) this.Document).Cache, (object) copy);
        goto label_7;
      }
    }
    if (!this.CollectFreight)
      baseCost = 0M;
    copy.FreightCost = new Decimal?(baseCost);
    PX.Objects.CM.PXCurrencyAttribute.CuryConvCury<SOOrder.curyFreightCost>(((PXSelectBase) this.Document).Cache, (object) copy);
    if (!copy.OverrideFreightAmount.GetValueOrDefault())
    {
      PXResultset<SOLine> pxResultset = ((PXSelectBase<SOLine>) this.Transactions).Select(Array.Empty<object>());
      this.CreateFreightCalculator().ApplyFreightTerms<SOOrder, SOOrder.curyFreightAmt>(((PXSelectBase) this.Document).Cache, copy, new int?(pxResultset.Count));
    }
label_7:
    SOOrder soOrder = ((PXSelectBase<SOOrder>) this.Document).Update(copy);
    soOrder.FreightCostIsValid = new bool?(true);
    ((PXSelectBase<SOOrder>) this.Document).Update(soOrder);
  }

  private Decimal ConvertAmtToBaseCury(
    string from,
    string rateType,
    DateTime effectiveDate,
    Decimal amount)
  {
    Decimal baseval = amount;
    using (new ReadOnlyScope(new PXCache[1]
    {
      ((PXSelectBase) this.DummyCuryInfo).Cache
    }))
    {
      PX.Objects.CM.CurrencyInfo info = new PX.Objects.CM.CurrencyInfo();
      info.CuryRateTypeID = rateType;
      info.CuryID = from;
      object obj;
      ((PXSelectBase) this.DummyCuryInfo).Cache.RaiseFieldDefaulting<PX.Objects.CM.CurrencyInfo.baseCuryID>((object) info, ref obj);
      ((PXSelectBase) this.DummyCuryInfo).Cache.SetValue<PX.Objects.CM.CurrencyInfo.baseCuryID>((object) info, obj);
      ((PXSelectBase) this.DummyCuryInfo).Cache.RaiseFieldDefaulting<PX.Objects.CM.CurrencyInfo.basePrecision>((object) info, ref obj);
      ((PXSelectBase) this.DummyCuryInfo).Cache.SetValue<PX.Objects.CM.CurrencyInfo.basePrecision>((object) info, obj);
      ((PXSelectBase) this.DummyCuryInfo).Cache.RaiseFieldDefaulting<PX.Objects.CM.CurrencyInfo.curyPrecision>((object) info, ref obj);
      ((PXSelectBase) this.DummyCuryInfo).Cache.SetValue<PX.Objects.CM.CurrencyInfo.curyPrecision>((object) info, obj);
      ((PXSelectBase) this.DummyCuryInfo).Cache.RaiseFieldDefaulting<PX.Objects.CM.CurrencyInfo.curyRate>((object) info, ref obj);
      ((PXSelectBase) this.DummyCuryInfo).Cache.SetValue<PX.Objects.CM.CurrencyInfo.curyRate>((object) info, obj);
      ((PXSelectBase) this.DummyCuryInfo).Cache.RaiseFieldDefaulting<PX.Objects.CM.CurrencyInfo.recipRate>((object) info, ref obj);
      ((PXSelectBase) this.DummyCuryInfo).Cache.SetValue<PX.Objects.CM.CurrencyInfo.recipRate>((object) info, obj);
      ((PXSelectBase) this.DummyCuryInfo).Cache.RaiseFieldDefaulting<PX.Objects.CM.CurrencyInfo.curyMultDiv>((object) info, ref obj);
      ((PXSelectBase) this.DummyCuryInfo).Cache.SetValue<PX.Objects.CM.CurrencyInfo.curyMultDiv>((object) info, obj);
      info.SetCuryEffDate(((PXSelectBase) this.DummyCuryInfo).Cache, (object) effectiveDate);
      PX.Objects.CM.PXCurrencyAttribute.CuryConvBase(((PXSelectBase) this.DummyCuryInfo).Cache, info, amount, out baseval);
    }
    return baseval;
  }

  /// <summary>
  /// <see cref="M:PX.Objects.TX.ExternalTaxBase`2.IsExternalTax(System.String)" />
  /// </summary>
  public virtual bool IsExternalTax(string TaxZoneID) => false;

  public virtual SOOrder CalculateExternalTax(SOOrder order) => order;

  public bool RecalculateExternalTaxesSync { get; set; }

  protected virtual void RecalculateExternalTaxes()
  {
  }

  public virtual void RecalcUnbilledTax()
  {
  }

  protected virtual void InsertImportedTaxes()
  {
  }

  public virtual PXResultset<SOOrderType> GetOrderShipments(PXGraph docgraph, SOOrder order)
  {
    return PXSelectBase<SOOrderType, PXSelectReadonly2<SOOrderType, LeftJoin<SOOrderShipment, On2<SOOrderShipment.FK.OrderType, And<SOOrderShipment.orderNbr, Equal<Required<SOOrder.orderNbr>>, And<SOOrderShipment.confirmed, Equal<True>, And<SOOrderShipment.canceled, Equal<False>, And<SOOrderShipment.invoiceNbr, IsNull>>>>>, LeftJoin<SOOrderTypeOperation, On2<SOOrderTypeOperation.FK.OrderType, And<Where2<Where<SOOrderShipment.operation, IsNull, And<SOOrderTypeOperation.operation, Equal<SOOrderType.defaultOperation>>>, Or<Where<SOOrderTypeOperation.operation, Equal<SOOrderShipment.operation>>>>>>, LeftJoin<PX.Objects.PO.POReceipt, On<SOOrderShipment.shipmentType, Equal<INDocType.dropShip>, And<PX.Objects.PO.POReceipt.receiptType, Equal<POReceiptType.poreceipt>, And<PX.Objects.PO.POReceipt.receiptNbr, Equal<SOOrderShipment.shipmentNbr>>>>, CrossJoin<PX.Objects.CM.CurrencyInfo, CrossJoin<SOAddress, CrossJoin<SOContact, CrossJoin<PX.Objects.AR.Customer>>>>>>>, Where<SOOrderType.orderType, Equal<Required<SOOrder.orderType>>, And<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<SOOrder.curyInfoID>>, And<SOAddress.addressID, Equal<Required<SOOrder.billAddressID>>, And<SOContact.contactID, Equal<Required<SOOrder.billContactID>>, And<PX.Objects.AR.Customer.bAccountID, Equal<Required<SOOrder.customerID>>, And<Where<PX.Objects.PO.POReceipt.receiptNbr, IsNull, Or<PX.Objects.PO.POReceipt.canceled, Equal<False>>>>>>>>>>.Config>.Select(docgraph, new object[6]
    {
      (object) order.OrderNbr,
      (object) order.OrderType,
      (object) order.CuryInfoID,
      (object) order.BillAddressID,
      (object) order.BillContactID,
      (object) order.CustomerID
    });
  }

  public virtual void InvoiceOrder(
    Dictionary<string, object> parameters,
    IEnumerable<SOOrder> list,
    InvoiceList created,
    bool isMassProcess,
    PXQuickProcess.ActionFlow quickProcessFlow,
    bool groupByCustomerOrderNumber)
  {
    SOShipmentEntry docgraph = PXGraph.CreateInstance<SOShipmentEntry>();
    SOInvoiceEntry invoiceEntry = PXGraph.CreateInstance<SOInvoiceEntry>();
    PXProcessing<SOOrder>.ProcessRecords((IEnumerable<SOOrder>) list.OrderBy<SOOrder, string>((Func<SOOrder, string>) (o => o.OrderType)).ThenBy<SOOrder, string>((Func<SOOrder, string>) (o => o.OrderNbr)), isMassProcess, (Action<SOOrder>) (order =>
    {
      ((PXGraph) invoiceEntry).Clear();
      ((PXGraph) invoiceEntry).Clear((PXClearOption) 4);
      ((PXSelectBase<ARSetup>) invoiceEntry.ARSetup).Current.RequireControlTotal = new bool?(false);
      List<PXResult<SOOrderShipment>> source = new List<PXResult<SOOrderShipment>>();
      PXResultset<SOShipLine, SOLine> pxResultset = (PXResultset<SOShipLine, SOLine>) null;
      string str = (string) null;
      bool flag1 = false;
      foreach (PXResult<SOOrderType, SOOrderShipment, SOOrderTypeOperation, PX.Objects.PO.POReceipt, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact, PX.Objects.AR.Customer> orderShipment in this.GetOrderShipments((PXGraph) docgraph, order))
      {
        SOOrderShipment shipment = PXResult<SOOrderType, SOOrderShipment, SOOrderTypeOperation, PX.Objects.PO.POReceipt, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact, PX.Objects.AR.Customer>.op_Implicit(orderShipment);
        PX.Objects.PO.POReceipt poReceipt = PXResult<SOOrderType, SOOrderShipment, SOOrderTypeOperation, PX.Objects.PO.POReceipt, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact, PX.Objects.AR.Customer>.op_Implicit(orderShipment);
        bool? nullable;
        if (poReceipt != null)
        {
          nullable = poReceipt.IsUnderCorrection;
          if (nullable.GetValueOrDefault())
          {
            str = poReceipt.ReceiptNbr;
            continue;
          }
        }
        flag1 = true;
        nullable = PXResult<SOOrderType, SOOrderShipment, SOOrderTypeOperation, PX.Objects.PO.POReceipt, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact, PX.Objects.AR.Customer>.op_Implicit(orderShipment).RequireShipping;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue || PXResult<SOOrderType, SOOrderShipment, SOOrderTypeOperation, PX.Objects.PO.POReceipt, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact, PX.Objects.AR.Customer>.op_Implicit(orderShipment).INDocType == "UND")
        {
          if (shipment.ShipmentNbr == null)
          {
            shipment = SOOrderShipment.FromSalesOrder(order);
            shipment.ShipmentType = INTranType.DocType(PXResult<SOOrderType, SOOrderShipment, SOOrderTypeOperation, PX.Objects.PO.POReceipt, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact, PX.Objects.AR.Customer>.op_Implicit(orderShipment).INDocType);
          }
          if (pxResultset == null)
            pxResultset = new PXResultset<SOShipLine, SOLine>();
          foreach (PXResult<SOLine> pxResult in PXSelectBase<SOLine, PXSelectJoin<SOLine, InnerJoin<PX.Objects.IN.InventoryItem, On<SOLine.FK.InventoryItem>>, Where<SOLine.orderType, Equal<Required<SOLine.orderType>>, And<SOLine.orderNbr, Equal<Required<SOLine.orderNbr>>, And<SOLine.lineType, NotEqual<SOLineType.miscCharge>>>>>.Config>.Select((PXGraph) docgraph, new object[2]
          {
            (object) order.OrderType,
            (object) order.OrderNbr
          }))
          {
            SOLine soLine = PXResult<SOLine>.op_Implicit(pxResult);
            ((PXResultset<SOShipLine>) pxResultset).Add((PXResult<SOShipLine>) new PXResult<SOShipLine, SOLine>(SOShipLine.FromSOLine(soLine), soLine));
          }
        }
        else if (this.HasMiscLinesToInvoice(order) && shipment.ShipmentNbr == null)
        {
          shipment = SOOrderShipment.FromSalesOrder(order, true);
          shipment.ShipmentType = "N";
        }
        if (shipment.ShipmentType == "H")
        {
          pxResultset = pxResultset ?? new PXResultset<SOShipLine, SOLine>();
          ((PXResultset<SOShipLine>) pxResultset).AddRange((IEnumerable<PXResult<SOShipLine>>) docgraph.CollectDropshipDetails(shipment));
        }
        if (shipment.ShipmentNbr != null)
          source.Add((PXResult<SOOrderShipment>) new PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact, SOOrderType, SOOrderTypeOperation, PX.Objects.AR.Customer>(shipment, order, PXResult<SOOrderType, SOOrderShipment, SOOrderTypeOperation, PX.Objects.PO.POReceipt, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact, PX.Objects.AR.Customer>.op_Implicit(orderShipment), PXResult<SOOrderType, SOOrderShipment, SOOrderTypeOperation, PX.Objects.PO.POReceipt, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact, PX.Objects.AR.Customer>.op_Implicit(orderShipment), PXResult<SOOrderType, SOOrderShipment, SOOrderTypeOperation, PX.Objects.PO.POReceipt, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact, PX.Objects.AR.Customer>.op_Implicit(orderShipment), PXResult<SOOrderType, SOOrderShipment, SOOrderTypeOperation, PX.Objects.PO.POReceipt, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact, PX.Objects.AR.Customer>.op_Implicit(orderShipment), PXResult<SOOrderType, SOOrderShipment, SOOrderTypeOperation, PX.Objects.PO.POReceipt, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact, PX.Objects.AR.Customer>.op_Implicit(orderShipment), PXResult<SOOrderType, SOOrderShipment, SOOrderTypeOperation, PX.Objects.PO.POReceipt, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact, PX.Objects.AR.Customer>.op_Implicit(orderShipment)));
      }
      if (str != null && !flag1)
        throw new PXException("The invoice cannot be created because the {0} drop shipment is under correction.", new object[1]
        {
          (object) str
        });
      foreach (PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact, SOOrderType, SOOrderTypeOperation> order1 in new List<PXResult<SOOrderShipment>>((IEnumerable<PXResult<SOOrderShipment>>) source.OrderBy<PXResult<SOOrderShipment>, int>((Func<PXResult<SOOrderShipment>, int>) (s => !(PXResult.Unwrap<SOOrderShipment>((object) s).Operation == PXResult.Unwrap<SOOrderType>((object) s).DefaultOperation) ? 1 : 0)).ThenBy<PXResult<SOOrderShipment>, string>((Func<PXResult<SOOrderShipment>, string>) (s => PXResult.Unwrap<SOOrderShipment>((object) s).ShipmentNbr))))
      {
        ((PXGraph) this).Clear();
        SOOrder soOrder = PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact, SOOrderType, SOOrderTypeOperation>.op_Implicit(order1);
        ((PXSelectBase<SOOrder>) this.Document).Current = PXResultset<SOOrder>.op_Implicit(((PXSelectBase<SOOrder>) this.Document).Search<SOOrder.orderNbr>((object) soOrder.OrderNbr, new object[1]
        {
          (object) soOrder.OrderType
        }));
        bool? nullable = WorkflowAction.HasWorkflowActionEnabled<SOOrderEntry, SOOrder>(this, (Expression<Func<SOOrderEntry, PXAction<SOOrder>>>) (g => g.prepareInvoice), ((PXSelectBase<SOOrder>) this.Document).Current);
        bool flag3 = false;
        if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
          throw new PXInvalidOperationException("The {0} action is not available in the {1} document at the moment. The document is being used by another process.", new object[2]
          {
            (object) ((PXAction) this.prepareInvoice).GetCaption(),
            (object) ((PXSelectBase) this.Document).Cache.GetRowDescription((object) ((PXSelectBase<SOOrder>) this.Document).Current)
          });
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          invoiceEntry.InvoiceOrder(new InvoiceOrderArgs((PXResult<SOOrderShipment, SOOrder, PX.Objects.CM.CurrencyInfo, SOAddress, SOContact>) order1)
          {
            InvoiceDate = ((PXGraph) invoiceEntry).Accessinfo.BusinessDate.Value,
            Customer = ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current,
            List = created,
            Details = pxResultset,
            QuickProcessFlow = quickProcessFlow,
            GroupByDefaultOperation = !isMassProcess,
            GroupByCustomerOrderNumber = groupByCustomerOrderNumber,
            OptimizeExternalTaxCalc = true
          });
          ((PXGraph) this).Clear();
          transactionScope.Complete();
        }
      }
    }), (Action<SOOrder>) null, (Func<SOOrder, Exception, bool, bool?>) null, (Action<SOOrder>) null, (Action<SOOrder>) null);
    invoiceEntry.CompleteProcessingImpl(created);
  }

  public virtual void PostOrder(
    INIssueEntry docgraph,
    SOOrder order,
    DocumentList<PX.Objects.IN.INRegister> list,
    SOOrderShipment orderShipment)
  {
    ((PXGraph) this).Clear();
    ((PXGraph) docgraph).Clear();
    bool flag1 = orderShipment != null;
    List<INItemPlan> reattachedPlans = new List<INItemPlan>();
    using (docgraph.TranSplitPlanExt.ReleaseModeScope())
    {
      ((PXSelectBase<INSetup>) docgraph.insetup).Current.HoldEntry = new bool?(false);
      ((PXSelectBase<INSetup>) docgraph.insetup).Current.RequireControlTotal = new bool?(false);
      ((PXSelectBase<SOOrder>) this.Document).Current = PXResultset<SOOrder>.op_Implicit(((PXSelectBase<SOOrder>) this.Document).Search<SOOrder.orderNbr>((object) order.OrderNbr, new object[1]
      {
        (object) order.OrderType
      }));
      if (!flag1)
        orderShipment = PXResultset<SOOrderShipment>.op_Implicit(PXSelectBase<SOOrderShipment, PXSelect<SOOrderShipment, Where<SOOrderShipment.orderType, Equal<Current<SOOrder.orderType>>, And<SOOrderShipment.orderNbr, Equal<Current<SOOrder.orderNbr>>, And<SOOrderShipment.invtRefNbr, IsNull>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) order
        }));
      bool? nullable1;
      if (orderShipment != null && orderShipment.ShipmentType != "H" && orderShipment.ShipmentNbr != "<NEW>")
      {
        nullable1 = orderShipment.Confirmed;
        if (!nullable1.GetValueOrDefault())
          throw new PXException("The system cannot process the unconfirmed shipment {0}.", new object[1]
          {
            (object) orderShipment.ShipmentNbr
          });
      }
      PX.Objects.AR.ARRegister arRegister = (PX.Objects.AR.ARRegister) null;
      if (orderShipment != null)
        arRegister = PXResultset<PX.Objects.AR.ARRegister>.op_Implicit(PXSelectBase<PX.Objects.AR.ARRegister, PXSelect<PX.Objects.AR.ARRegister, Where<PX.Objects.AR.ARRegister.docType, Equal<Required<PX.Objects.AR.ARRegister.docType>>, And<PX.Objects.AR.ARRegister.refNbr, Equal<Required<PX.Objects.AR.ARRegister.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) orderShipment.InvoiceType,
          (object) orderShipment.InvoiceNbr
        }));
      PX.Objects.IN.INRegister inRegister1 = new PX.Objects.IN.INRegister();
      int? nullable2 = (int?) arRegister?.BranchID;
      inRegister1.BranchID = nullable2 ?? order.BranchID;
      inRegister1.DocType = INTranType.DocType(((PXSelectBase<SOOrderTypeOperation>) this.sooperation).Current.INDocType);
      nullable2 = new int?();
      inRegister1.SiteID = nullable2;
      inRegister1.TranDate = (DateTime?) arRegister?.DocDate;
      inRegister1.FinPeriodID = arRegister?.FinPeriodID;
      inRegister1.OrigModule = "SO";
      PX.Objects.IN.INRegister inRegister2 = inRegister1;
      if (((PXSelectBase<PX.Objects.IN.INRegister>) docgraph.issue).Insert(inRegister2) == null)
        return;
      SOLine soLine = (SOLine) null;
      INTran inTran1 = (INTran) null;
      Decimal? nullable3;
      string str1;
      if (!(order.ARDocType == "INC"))
      {
        if (!(order.ARDocType == "CSR"))
        {
          str1 = order.ARDocType;
        }
        else
        {
          nullable3 = order.CuryOrderTotal;
          Decimal num = 0M;
          str1 = nullable3.GetValueOrDefault() >= num & nullable3.HasValue ? "CSL" : "RCS";
        }
      }
      else
      {
        nullable3 = order.CuryOrderTotal;
        Decimal num = 0M;
        str1 = nullable3.GetValueOrDefault() >= num & nullable3.HasValue ? "INV" : "CRM";
      }
      string str2 = str1;
      foreach (PXResult<SOLine, SOLineSplit, PX.Objects.AR.ARTran, INTran, INItemPlan> pxResult in PXSelectBase<SOLine, PXSelectJoin<SOLine, LeftJoin<SOLineSplit, On<SOLineSplit.FK.OrderLine>, LeftJoin<PX.Objects.AR.ARTran, On<PX.Objects.AR.ARTran.sOOrderType, Equal<SOLine.orderType>, And<PX.Objects.AR.ARTran.sOOrderNbr, Equal<SOLine.orderNbr>, And<PX.Objects.AR.ARTran.sOOrderLineNbr, Equal<SOLine.lineNbr>, And<PX.Objects.AR.ARTran.lineType, Equal<SOLine.lineType>>>>>, LeftJoin<INTran, On<INTran.sOOrderType, Equal<SOLine.orderType>, And<INTran.sOOrderNbr, Equal<SOLine.orderNbr>, And<INTran.sOOrderLineNbr, Equal<SOLine.lineNbr>>>>, LeftJoin<INItemPlan, On<INItemPlan.planID, Equal<SOLineSplit.planID>>>>>>, Where<SOLine.orderType, Equal<Current<SOOrder.orderType>>, And<SOLine.orderNbr, Equal<Current<SOOrder.orderNbr>>, And<SOLine.lineType, Equal<SOLineType.inventory>, And2<Where<Required<PX.Objects.AR.ARTran.tranType>, Equal<ARDocType.noUpdate>, Or<PX.Objects.AR.ARTran.tranType, Equal<Required<PX.Objects.AR.ARTran.tranType>>, And<PX.Objects.AR.ARTran.refNbr, IsNotNull>>>, And<INTran.refNbr, IsNull>>>>>, OrderBy<Asc<SOLine.orderType, Asc<SOLine.orderNbr, Asc<SOLine.lineNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) str2,
        (object) str2
      }))
      {
        SOLine line = PXResult<SOLine, SOLineSplit, PX.Objects.AR.ARTran, INTran, INItemPlan>.op_Implicit(pxResult);
        nullable2 = PXResult<SOLine, SOLineSplit, PX.Objects.AR.ARTran, INTran, INItemPlan>.op_Implicit(pxResult).SplitLineNbr;
        SOLineSplit split = nullable2.HasValue ? PXResult<SOLine, SOLineSplit, PX.Objects.AR.ARTran, INTran, INItemPlan>.op_Implicit(pxResult) : SOLineSplit.FromSOLine(line);
        INItemPlan inItemPlan = PXResult<SOLine, SOLineSplit, PX.Objects.AR.ARTran, INTran, INItemPlan>.op_Implicit(pxResult);
        INPlanType inPlanType1 = INPlanType.PK.Find((PXGraph) this, inItemPlan.PlanType);
        if ((object) inPlanType1 == null)
          inPlanType1 = new INPlanType();
        INPlanType inPlanType2 = inPlanType1;
        PX.Objects.AR.ARTran arTran = PXResult<SOLine, SOLineSplit, PX.Objects.AR.ARTran, INTran, INItemPlan>.op_Implicit(pxResult);
        long? nullable4 = inItemPlan.PlanID;
        if (nullable4.HasValue)
          ((PXGraph) this).Caches[typeof (INItemPlan)].SetStatus((object) inItemPlan, (PXEntryStatus) 0);
        bool flag2 = false;
        nullable1 = inPlanType2.DeleteOnEvent;
        if (nullable1.GetValueOrDefault())
        {
          flag2 = true;
          GraphHelper.MarkUpdated(((PXGraph) this).Caches[typeof (SOLineSplit)], (object) split, true);
          split = (SOLineSplit) ((PXGraph) this).Caches[typeof (SOLineSplit)].Locate((object) split);
          if (split != null)
          {
            SOLineSplit soLineSplit = split;
            nullable4 = new long?();
            long? nullable5 = nullable4;
            soLineSplit.PlanID = nullable5;
            split.Completed = new bool?(true);
          }
          ((PXGraph) this).Caches[typeof (SOLineSplit)].IsDirty = true;
        }
        else if (!string.IsNullOrEmpty(inPlanType2.ReplanOnEvent))
        {
          inItemPlan = PXCache<INItemPlan>.CreateCopy(inItemPlan);
          inItemPlan.PlanType = inPlanType2.ReplanOnEvent;
          ((PXGraph) this).Caches[typeof (INItemPlan)].Update((object) inItemPlan);
          GraphHelper.MarkUpdated(((PXGraph) this).Caches[typeof (SOLineSplit)], (object) split, true);
          ((PXGraph) this).Caches[typeof (SOLineSplit)].IsDirty = true;
        }
        int num1 = !((PXGraph) this).Caches[typeof (SOLine)].ObjectsEqual((object) soLine, (object) line) ? 1 : 0;
        if (num1 != 0)
        {
          line.Completed = new bool?(true);
          GraphHelper.MarkUpdated(((PXSelectBase) this.Transactions).Cache, (object) line);
          ((PXSelectBase) this.Transactions).Cache.IsDirty = true;
        }
        bool flag3 = line.Operation == "R" && PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, line.InventoryID)?.ValMethod == "S" && !string.IsNullOrEmpty(split.LotSerialNbr);
        int? nullable6;
        int num2;
        if (num1 == 0)
        {
          nullable2 = line.InventoryID;
          nullable6 = split.InventoryID;
          num2 = !(nullable2.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable2.HasValue == nullable6.HasValue) ? 1 : 0;
        }
        else
          num2 = 1;
        int num3 = flag3 ? 1 : 0;
        short? nullable7;
        if ((num2 | num3) != 0)
        {
          nullable1 = split.IsStockItem;
          if (nullable1.GetValueOrDefault())
          {
            nullable3 = line.Qty;
            Decimal num4 = 0M;
            if (!(nullable3.GetValueOrDefault() == num4 & nullable3.HasValue))
            {
              INTran tran = new INTran();
              INTran inTran2 = tran;
              nullable6 = arTran.BranchID;
              int? nullable8 = nullable6 ?? line.BranchID;
              inTran2.BranchID = nullable8;
              tran.TranType = line.TranType;
              tran.SOShipmentNbr = "<NEW>";
              tran.SOShipmentType = ((PXSelectBase<PX.Objects.IN.INRegister>) docgraph.issue).Current.DocType;
              INTran inTran3 = tran;
              nullable6 = new int?();
              int? nullable9 = nullable6;
              inTran3.SOShipmentLineNbr = nullable9;
              tran.SOOrderType = line.OrderType;
              tran.SOOrderNbr = line.OrderNbr;
              tran.SOOrderLineNbr = line.LineNbr;
              tran.SOLineType = line.LineType;
              tran.ARDocType = arTran.TranType;
              tran.ARRefNbr = arTran.RefNbr;
              tran.ARLineNbr = arTran.LineNbr;
              tran.AcctID = arTran.AccountID;
              tran.SubID = arTran.SubID;
              tran.IsStockItem = split.IsStockItem;
              tran.InventoryID = split.InventoryID;
              tran.SiteID = line.SiteID;
              tran.BAccountID = line.CustomerID;
              INTran inTran4 = tran;
              nullable3 = line.OrderQty;
              Decimal num5 = 0M;
              short? nullable10;
              if (!(nullable3.GetValueOrDefault() < num5 & nullable3.HasValue))
              {
                nullable10 = line.InvtMult;
              }
              else
              {
                nullable7 = line.InvtMult;
                nullable10 = nullable7.HasValue ? new short?(-nullable7.GetValueOrDefault()) : new short?();
              }
              inTran4.InvtMult = nullable10;
              tran.Qty = new Decimal?(0M);
              tran.ProjectID = line.ProjectID;
              tran.TaskID = line.TaskID;
              tran.CostCodeID = line.CostCodeID;
              tran.IsSpecialOrder = line.IsSpecialOrder;
              INTran inTran5 = tran;
              nullable6 = line.InventoryID;
              nullable2 = split.InventoryID;
              int num6;
              if (!(nullable6.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable6.HasValue == nullable2.HasValue))
              {
                nullable1 = line.IsKit;
                num6 = nullable1.GetValueOrDefault() ? 1 : 0;
              }
              else
                num6 = 0;
              bool? nullable11 = new bool?(num6 != 0);
              inTran5.IsComponentItem = nullable11;
              nullable2 = line.InventoryID;
              nullable6 = split.InventoryID;
              if (!(nullable2.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable2.HasValue == nullable6.HasValue))
              {
                tran.SubItemID = split.SubItemID;
                tran.UOM = split.UOM;
                tran.UnitPrice = new Decimal?(0M);
                tran.UnitCost = this.GetINTranUnitCost(line, split, false);
                tran.TranDesc = (string) null;
              }
              else if (flag3)
              {
                tran.SubItemID = split.SubItemID;
                tran.UOM = split.UOM;
                INTran inTran6 = tran;
                PXCache cache = ((PXSelectBase) this.Transactions).Cache;
                int? inventoryId = arTran.InventoryID;
                string uom = arTran.UOM;
                nullable3 = arTran.UnitPrice;
                Decimal valueOrDefault = nullable3.GetValueOrDefault();
                Decimal? nullable12 = new Decimal?(INUnitAttribute.ConvertFromBase(cache, inventoryId, uom, valueOrDefault, INPrecision.UNITCOST));
                inTran6.UnitPrice = nullable12;
                tran.UnitCost = this.GetINTranUnitCost(line, split, true);
                tran.TranDesc = line.TranDesc;
                tran.ReasonCode = line.ReasonCode;
              }
              else
              {
                tran.SubItemID = line.SubItemID;
                tran.UOM = line.UOM;
                INTran inTran7 = tran;
                nullable3 = arTran.UnitPrice;
                Decimal? nullable13 = new Decimal?(nullable3.GetValueOrDefault());
                inTran7.UnitPrice = nullable13;
                tran.UnitCost = line.UnitCost;
                tran.TranDesc = line.TranDesc;
                tran.ReasonCode = line.ReasonCode;
              }
              docgraph.CostCenterDispatcherExt?.SetInventorySource(tran);
              inTran1 = docgraph.LineSplittingExt.lsselect.Insert(tran);
            }
          }
        }
        soLine = line;
        nullable1 = split.IsStockItem;
        if (nullable1.GetValueOrDefault())
        {
          nullable3 = split.Qty;
          Decimal num7 = 0M;
          if (!(nullable3.GetValueOrDefault() == num7 & nullable3.HasValue))
          {
            INTranSplit inTranSplit1 = INTranSplit.FromINTran(inTran1);
            INTranSplit inTranSplit2 = inTranSplit1;
            nullable6 = new int?();
            int? nullable14 = nullable6;
            inTranSplit2.SplitLineNbr = nullable14;
            inTranSplit1.SubItemID = split.SubItemID;
            inTranSplit1.LocationID = split.LocationID;
            inTranSplit1.LotSerialNbr = split.LotSerialNbr;
            inTranSplit1.ExpireDate = split.ExpireDate;
            inTranSplit1.UOM = split.UOM;
            inTranSplit1.Qty = split.Qty;
            INTranSplit inTranSplit3 = inTranSplit1;
            nullable3 = new Decimal?();
            Decimal? nullable15 = nullable3;
            inTranSplit3.BaseQty = nullable15;
            if (flag2)
            {
              inTranSplit1.PlanID = inItemPlan.PlanID;
              reattachedPlans.Add(inItemPlan);
            }
            ((PXSelectBase<INTranSplit>) docgraph.splits).Insert(inTranSplit1);
          }
          else
            ((PXGraph) this).Caches[typeof (INItemPlan)].Delete((object) inItemPlan);
          nullable6 = line.InventoryID;
          nullable2 = split.InventoryID;
          if (nullable6.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable6.HasValue == nullable2.HasValue && !flag3)
          {
            nullable3 = line.Qty;
            Decimal num8 = 0M;
            if (!(nullable3.GetValueOrDefault() == num8 & nullable3.HasValue))
            {
              bool flag4 = arTran.DrCr == "C" && arTran.SOOrderLineOperation == "R" || arTran.DrCr == "D" && arTran.SOOrderLineOperation == "I";
              INTran inTran8 = inTran1;
              nullable7 = line.LineSign;
              nullable3 = nullable7.HasValue ? new Decimal?((Decimal) nullable7.GetValueOrDefault()) : new Decimal?();
              Decimal? nullable16 = line.ExtCost;
              Decimal? nullable17 = nullable3.HasValue & nullable16.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * nullable16.GetValueOrDefault()) : new Decimal?();
              inTran8.TranCost = nullable17;
              INTran inTran9 = inTran1;
              Decimal? nullable18;
              if (!flag4)
              {
                nullable18 = arTran.TranAmt;
              }
              else
              {
                nullable16 = arTran.TranAmt;
                if (!nullable16.HasValue)
                {
                  nullable3 = new Decimal?();
                  nullable18 = nullable3;
                }
                else
                  nullable18 = new Decimal?(-nullable16.GetValueOrDefault());
              }
              nullable16 = nullable18;
              Decimal? nullable19 = new Decimal?(nullable16.GetValueOrDefault());
              inTran9.TranAmt = nullable19;
            }
          }
        }
        else
        {
          nullable1 = inPlanType2.DeleteOnEvent;
          if (nullable1.GetValueOrDefault())
            ((PXGraph) this).Caches[typeof (INItemPlan)].Delete((object) inItemPlan);
        }
      }
    }
    PX.Objects.IN.INRegister copy = PXCache<PX.Objects.IN.INRegister>.CreateCopy(((PXSelectBase<PX.Objects.IN.INRegister>) docgraph.issue).Current);
    PXFormulaAttribute.CalcAggregate<INTran.qty>(((PXSelectBase) docgraph.transactions).Cache, (object) copy);
    PXFormulaAttribute.CalcAggregate<INTran.tranAmt>(((PXSelectBase) docgraph.transactions).Cache, (object) copy);
    PXFormulaAttribute.CalcAggregate<INTran.tranCost>(((PXSelectBase) docgraph.transactions).Cache, (object) copy);
    ((PXSelectBase<PX.Objects.IN.INRegister>) docgraph.issue).Update(copy);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (((PXSelectBase) docgraph.transactions).Cache.IsDirty)
      {
        ((PXAction) docgraph.Save).Press();
        PXSelectBase<SOOrderShipment> pxSelectBase = (PXSelectBase<SOOrderShipment>) new PXSelect<SOOrderShipment, Where<SOOrderShipment.orderType, Equal<Current<SOOrder.orderType>>, And<SOOrderShipment.orderNbr, Equal<Current<SOOrder.orderNbr>>>>>((PXGraph) this);
        if (flag1)
          pxSelectBase.WhereAnd<Where<SOOrderShipment.shippingRefNoteID, Equal<Current<SOOrderShipment.shippingRefNoteID>>>>();
        else
          pxSelectBase.WhereAnd<Where<SOOrderShipment.invtRefNbr, IsNull>>();
        PXView view = ((PXSelectBase) pxSelectBase).View;
        object[] objArray1 = new object[2]
        {
          (object) order,
          (object) orderShipment
        };
        object[] objArray2 = Array.Empty<object>();
        foreach (SOOrderShipment orderShipment1 in view.SelectMultiBound(objArray1, objArray2))
        {
          orderShipment1.InvtDocType = ((PXSelectBase<PX.Objects.IN.INRegister>) docgraph.issue).Current.DocType;
          orderShipment1.InvtRefNbr = ((PXSelectBase<PX.Objects.IN.INRegister>) docgraph.issue).Current.RefNbr;
          orderShipment1.InvtNoteID = ((PXSelectBase<PX.Objects.IN.INRegister>) docgraph.issue).Current.NoteID;
          ((PXSelectBase) this.shipmentlist).Cache.Update((object) orderShipment1);
          this.UpdatePlansRefNoteID(orderShipment1, orderShipment1.InvtNoteID, (IEnumerable<INItemPlan>) reattachedPlans);
        }
        ((PXAction) this.Save).Press();
        PX.Objects.IN.INRegister inRegister;
        if ((inRegister = list.Find((object) ((PXSelectBase<PX.Objects.IN.INRegister>) docgraph.issue).Current)) == null)
          list.Add(((PXSelectBase<PX.Objects.IN.INRegister>) docgraph.issue).Current);
        else
          ((PXSelectBase) docgraph.issue).Cache.RestoreCopy((object) inRegister, (object) ((PXSelectBase<PX.Objects.IN.INRegister>) docgraph.issue).Current);
      }
      transactionScope.Complete();
    }
  }

  protected virtual Decimal? GetINTranUnitCost(
    SOLine line,
    SOLineSplit split,
    bool isReturnSpecific)
  {
    if (!(line.Operation == "R"))
      return new Decimal?(0M);
    if (isReturnSpecific)
    {
      List<INTranCost> list = GraphHelper.RowCast<INTranCost>((IEnumerable) PXSelectBase<INTranCost, PXViewOf<INTranCost>.BasedOn<SelectFromBase<INTranCost, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INTran>.On<INTranCost.FK.Tran>>, FbqlJoins.Inner<PX.Objects.AR.ARTran>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.tranType, Equal<INTran.aRDocType>>>>, And<BqlOperand<PX.Objects.AR.ARTran.refNbr, IBqlString>.IsEqual<INTran.aRRefNbr>>>>.And<BqlOperand<PX.Objects.AR.ARTran.lineNbr, IBqlInt>.IsEqual<INTran.aRLineNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTranCost.lotSerialNbr, Equal<P.AsString>>>>, And<BqlOperand<PX.Objects.AR.ARTran.tranType, IBqlString>.IsEqual<P.AsString.ASCII>>>, And<BqlOperand<PX.Objects.AR.ARTran.refNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<PX.Objects.AR.ARTran.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[4]
      {
        (object) split.LotSerialNbr,
        (object) line.InvoiceType,
        (object) line.InvoiceNbr,
        (object) line.InvoiceLineNbr
      })).ToList<INTranCost>();
      Decimal? nullable = list.Sum<INTranCost>((Func<INTranCost, Decimal?>) (c => c.Qty));
      if (nullable.GetValueOrDefault() != 0M)
        return new Decimal?(PXPriceCostAttribute.Round(list.Sum<INTranCost>((Func<INTranCost, Decimal?>) (c => c.TranCost)).Value / nullable.Value));
    }
    INTran inTran = PXResultset<INTran>.op_Implicit(PXSelectBase<INTran, PXViewOf<INTran>.BasedOn<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.sOOrderType, Equal<BqlField<SOLine.origOrderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<INTran.sOOrderNbr, IBqlString>.IsEqual<BqlField<SOLine.origOrderNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<INTran.sOOrderLineNbr, IBqlInt>.IsEqual<BqlField<SOLine.origLineNbr, IBqlInt>.FromCurrent>>>>.And<BqlOperand<INTran.inventoryID, IBqlInt>.IsEqual<BqlField<SOLineSplit.inventoryID, IBqlInt>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) this, new object[2]
    {
      (object) line,
      (object) split
    }, Array.Empty<object>()));
    if (inTran != null)
    {
      PXCache cache = ((PXSelectBase) this.Transactions).Cache;
      int? inventoryId = inTran.InventoryID;
      string uom = inTran.UOM;
      Decimal? tranCost = inTran.TranCost;
      Decimal? qty = inTran.Qty;
      Decimal valueOrDefault = (tranCost.HasValue & qty.HasValue ? new Decimal?(tranCost.GetValueOrDefault() / qty.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
      return new Decimal?(INUnitAttribute.ConvertFromBase(cache, inventoryId, uom, valueOrDefault, INPrecision.UNITCOST));
    }
    INItemSite inItemSite = INItemSite.PK.Find((PXGraph) this, split.InventoryID, split.SiteID);
    if (inItemSite != null && inItemSite.TranUnitCost.HasValue)
      return inItemSite.TranUnitCost;
    PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this, line.BranchID);
    return new Decimal?(((Decimal?) INItemCost.PK.Find((PXGraph) this, split.InventoryID, branch?.BaseCuryID)?.TranUnitCost).GetValueOrDefault());
  }

  public virtual void UpdatePlansRefNoteID(
    SOOrderShipment orderShipment,
    Guid? refNoteID,
    IEnumerable<INItemPlan> reattachedPlans)
  {
    if (!reattachedPlans.Any<INItemPlan>())
      return;
    ((PXGraph) this).Caches[typeof (INItemPlan)].Persist((PXDBOperation) 3);
    ((PXGraph) this).Caches[typeof (INItemPlan)].Persisted(false);
    PXUpdateJoin<Set<INItemPlan.refNoteID, Required<INItemPlan.refNoteID>, Set<INItemPlan.refEntityType, PX.Objects.Common.Constants.DACName<PX.Objects.IN.INRegister>, Set<INItemPlan.kitInventoryID, Null>>>, INItemPlan, InnerJoin<SOLineSplit, On<SOLineSplit.planID, Equal<INItemPlan.planID>>>, Where<SOLineSplit.orderType, Equal<Required<SOLineSplit.orderType>>, And<SOLineSplit.orderNbr, Equal<Required<SOLineSplit.orderNbr>>>>>.Update((PXGraph) this, new object[3]
    {
      (object) refNoteID,
      (object) orderShipment.OrderType,
      (object) orderShipment.OrderNbr
    });
    byte[] numArray = PXDatabase.SelectTimeStamp();
    foreach (INItemPlan reattachedPlan in reattachedPlans)
      PXTimeStampScope.PutPersisted(((PXGraph) this).Caches[typeof (INItemPlan)], (object) reattachedPlan, new object[1]
      {
        (object) numArray
      });
  }

  [PXDefault(typeof (SOOrder.orderDate))]
  [PXMergeAttributes]
  protected virtual void EPApproval_DocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (SOOrder.customerID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_BAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (SOOrder.ownerID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_DocumentOwnerID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (SOOrder.orderDesc))]
  [PXMergeAttributes]
  protected virtual void EPApproval_Descr_CacheAttached(PXCache sender)
  {
  }

  [CurrencyInfo(typeof (SOOrder.curyInfoID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (SOOrder.curyOrderTotal))]
  [PXMergeAttributes]
  protected virtual void EPApproval_CuryTotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (SOOrder.orderTotal))]
  [PXMergeAttributes]
  protected virtual void EPApproval_TotalAmount_CacheAttached(PXCache sender)
  {
  }

  public bool AllowAllocation()
  {
    return ((PXSelectBase<SOOrderType>) this.soordertype).Current != null && !((PXSelectBase<SOOrderType>) this.soordertype).Current.RequireAllocation.GetValueOrDefault() || PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>() || PXAccess.FeatureInstalled<FeaturesSet.lotSerialTracking>() || PXAccess.FeatureInstalled<FeaturesSet.subItem>() || PXAccess.FeatureInstalled<FeaturesSet.replenishment>() || PXAccess.FeatureInstalled<FeaturesSet.sOToPOLink>();
  }

  protected virtual void RemoveOrphanReplenishmentLines()
  {
    if (((PXGraph) this).UnattendedMode)
      return;
    HashSet<long?> hashSet = ((PXGraph) this).Caches[typeof (INItemPlan)].Deleted.Cast<INItemPlan>().Select<INItemPlan, long?>((Func<INItemPlan, long?>) (p => p.PlanID)).ToHashSet<long?>();
    if (hashSet.Count == 0)
      return;
    foreach (PXResult<INReplenishmentLine> pxResult in ((PXSelectBase<INReplenishmentLine>) this.ReplenishmentLinesWithPlans).Select(Array.Empty<object>()))
    {
      INItemPlan inItemPlan = ((PXResult) pxResult).GetItem<INItemPlan>();
      if ((inItemPlan != null ? (!inItemPlan.SupplyPlanID.HasValue ? 1 : 0) : 1) == 0 && hashSet.Contains(inItemPlan.SupplyPlanID))
      {
        ((PXSelectBase<INReplenishmentLine>) this.ReplenishmentLinesWithPlans).Delete(PXResult<INReplenishmentLine>.op_Implicit(pxResult));
        ((PXGraph) this).Caches[typeof (INItemPlan)].Delete((object) inItemPlan);
      }
    }
  }

  public virtual void Persist()
  {
    try
    {
      ++this._persistNesting;
      this.PersistImpl();
    }
    finally
    {
      --this._persistNesting;
    }
  }

  protected virtual void RecalculatePricesAndDiscountsOnPersist(IEnumerable<SOOrder> orders)
  {
    foreach (SOOrder doc in orders.Where<SOOrder>((Func<SOOrder, bool>) (doc =>
    {
      if (!doc.DeferPriceDiscountRecalculation.GetValueOrDefault())
        return false;
      bool? andDiscountsValid = doc.IsPriceAndDiscountsValid;
      bool flag = false;
      return andDiscountsValid.GetValueOrDefault() == flag & andDiscountsValid.HasValue;
    })))
    {
      if (((PXSelectBase<SOOrder>) this.Document).Current != doc)
        ((PXSelectBase<SOOrder>) this.Document).Current = doc;
      TaxBaseAttribute.SetTaxCalc<SOLine.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.ManualLineCalc | TaxCalc.RecalculateAlways);
      doc.DeferPriceDiscountRecalculation = new bool?(false);
      try
      {
        this._discountEngine.AutoRecalculatePricesAndDiscounts(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<SOLine>) this.Transactions, (SOLine) null, (PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails, ((PXSelectBase<SOOrder>) this.Document).Current.CustomerLocationID, ((PXSelectBase<SOOrder>) this.Document).Current.OrderDate, this.GetDefaultSODiscountCalculationOptions(doc, true));
        doc.IsPriceAndDiscountsValid = new bool?(true);
      }
      finally
      {
        doc.DeferPriceDiscountRecalculation = new bool?(true);
      }
    }
  }

  protected virtual void PersistImpl()
  {
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    this.RemoveOrphanReplenishmentLines();
    foreach (IGrouping<\u003C\u003Ef__AnonymousType106<int?, int?>, PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter> grouping in ((PXSelectBase) this.sitestatusview).Cache.Inserted.Cast<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>().Where<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>((Func<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter, bool>) (status => status.InitSiteStatus.GetValueOrDefault())).GroupBy(status => new
    {
      InventoryID = status.InventoryID,
      SiteID = status.SiteID
    }))
    {
      if (INReleaseProcess.SelectItemSite((PXGraph) this, grouping.Key.InventoryID, grouping.Key.SiteID) == null)
      {
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, grouping.Key.InventoryID);
        if (inventoryItem.StkItem.GetValueOrDefault())
        {
          PX.Objects.IN.INSite site = PX.Objects.IN.INSite.PK.Find((PXGraph) this, grouping.Key.SiteID);
          INPostClass postclass = INPostClass.PK.Find((PXGraph) this, inventoryItem.PostClassID);
          InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find((PXGraph) this, inventoryItem.InventoryID, site.BaseCuryID);
          INItemSite itemsite = new INItemSite();
          itemsite.InventoryID = grouping.Key.InventoryID;
          itemsite.SiteID = grouping.Key.SiteID;
          INItemSiteMaint.DefaultItemSiteByItem((PXGraph) this, itemsite, inventoryItem, site, postclass, itemCurySettings);
          ((PXSelectBase<INItemSite>) this.initemsite).Insert(itemsite);
        }
      }
    }
    IEnumerable<SOOrder> soOrders = NonGenericIEnumerableExtensions.Concat_(((PXSelectBase) this.Document).Cache.Inserted, ((PXSelectBase) this.Document).Cache.Updated).Cast<SOOrder>();
    this.RecalculatePricesAndDiscountsOnPersist(soOrders);
    foreach (SOOrder doc in soOrders.Where<SOOrder>((Func<SOOrder, bool>) (doc =>
    {
      bool? completed = doc.Completed;
      bool flag = false;
      return completed.GetValueOrDefault() == flag & completed.HasValue;
    })))
      this.VerifyAppliedToOrderAmount(doc);
    bool? nullable;
    if (((PXSelectBase<SOOrder>) this.Document).Current != null)
    {
      nullable = ((PXSelectBase<SOOrder>) this.Document).Current.IsPackageValid;
      if (!nullable.GetValueOrDefault() && !string.IsNullOrEmpty(((PXSelectBase<SOOrder>) this.Document).Current.ShipVia))
      {
        nullable = ((PXSelectBase<SOOrderType>) this.soordertype).Current.RequireShipping;
        if (nullable.GetValueOrDefault() || ((PXSelectBase<SOOrderType>) this.soordertype).Current?.Behavior == "QT")
        {
          try
          {
            nullable = ((PXSelectBase<SOOrder>) this.Document).Current.IsManualPackage;
            if (!nullable.GetValueOrDefault())
              this.CarrierRatesExt.RecalculatePackagesForOrder(((PXSelectBase<SOOrder>) this.Document).Current);
          }
          catch (Exception ex)
          {
            PXTrace.WriteError(ex);
          }
        }
      }
    }
    if (((PXSelectBase<SOOrder>) this.Document).Current != null)
    {
      nullable = ((PXSelectBase<SOOrder>) this.Document).Current.FreightCostIsValid;
      if (!nullable.GetValueOrDefault())
      {
        SOOrderType current = ((PXSelectBase<SOOrderType>) this.soordertype).Current;
        int num;
        if (current == null)
        {
          num = 0;
        }
        else
        {
          nullable = current.CalculateFreight;
          num = nullable.GetValueOrDefault() ? 1 : 0;
        }
        if (num != 0)
        {
          if (!string.IsNullOrEmpty(((PXSelectBase<SOOrder>) this.Document).Current.ShipVia))
          {
            try
            {
              this.CalculateFreightCost(true);
            }
            catch (Exception ex)
            {
              PXTrace.WriteError(ex);
            }
          }
        }
      }
    }
    foreach (SOOrder order in ((PXSelectBase) this.Document).Cache.Updated)
      this.TryAutoCompleteOrder(order);
    this._discountEngine.ValidateDiscountDetails((PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails);
    if (this.RecalculateExternalTaxesSync)
      this.RecalculateExternalTaxes();
    this.InsertImportedTaxes();
    ((PXGraph) this).Persist();
    if (!this.RecalculateExternalTaxesSync && this._persistNesting == 1)
      this.RecalculateExternalTaxes();
    stopwatch.Stop();
  }

  protected virtual void VerifyAppliedToOrderAmount(SOOrder doc)
  {
    SOOrderType soOrderType1;
    if (!(((PXSelectBase<SOOrderType>) this.soordertype).Current?.OrderType == doc.OrderType))
      soOrderType1 = PXResultset<SOOrderType>.op_Implicit(((PXSelectBase<SOOrderType>) this.soordertype).Select(new object[1]
      {
        (object) doc.OrderType
      }));
    else
      soOrderType1 = ((PXSelectBase<SOOrderType>) this.soordertype).Current;
    SOOrderType soOrderType2 = soOrderType1;
    if (!soOrderType2.CanHavePayments.GetValueOrDefault() && !soOrderType2.CanHaveRefunds.GetValueOrDefault())
      return;
    Decimal? nullable1 = new Decimal?(0M);
    bool flag1 = false;
    foreach (PXResult<SOAdjust, ARRegisterAlias, PX.Objects.AR.ARPayment> pxResult in ((PXSelectBase) this.Adjustments_Raw).View.SelectMultiBound(new object[1]
    {
      (object) doc
    }, Array.Empty<object>()))
    {
      SOAdjust soAdjust = PXResult<SOAdjust, ARRegisterAlias, PX.Objects.AR.ARPayment>.op_Implicit(pxResult);
      if (soAdjust != null)
      {
        bool? voided = soAdjust.Voided;
        bool flag2 = false;
        if (voided.GetValueOrDefault() == flag2 & voided.HasValue)
        {
          Decimal? nullable2 = nullable1;
          Decimal? nullable3 = soAdjust.CuryAdjdAmt;
          Decimal? nullable4;
          Decimal? nullable5;
          if (!(nullable2.HasValue & nullable3.HasValue))
          {
            nullable4 = new Decimal?();
            nullable5 = nullable4;
          }
          else
            nullable5 = new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault());
          nullable1 = nullable5;
          if (EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.Adjustments).Cache.GetStatus((object) soAdjust), (PXEntryStatus) 1, (PXEntryStatus) 2))
            flag1 = true;
          int num1;
          if (doc.Behavior == "MO")
          {
            nullable3 = doc.CuryOrderTotal;
            Decimal num2 = 0M;
            if (!(nullable3.GetValueOrDefault() >= num2 & nullable3.HasValue) || !(ARPaymentType.DrCr(soAdjust.AdjgDocType) == "C"))
            {
              nullable3 = doc.CuryOrderTotal;
              Decimal num3 = 0M;
              num1 = !(nullable3.GetValueOrDefault() < num3 & nullable3.HasValue) ? 0 : (ARPaymentType.DrCr(soAdjust.AdjgDocType) == "D" ? 1 : 0);
            }
            else
              num1 = 1;
          }
          else
            num1 = 0;
          if (num1 == 0)
          {
            nullable2 = doc.CuryDocBal;
            nullable4 = nullable1;
            nullable3 = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
            Decimal num4 = 0M;
            if (nullable3.GetValueOrDefault() < num4 & nullable3.HasValue)
            {
              nullable3 = nullable1;
              Decimal num5 = 0M;
              if (nullable3.GetValueOrDefault() > num5 & nullable3.HasValue && !ExternalTaxRecalculationScope.IsScoped())
              {
                if (!flag1)
                {
                  nullable3 = (Decimal?) ((PXSelectBase) this.Document).Cache.GetValueOriginal<SOOrder.curyOrderTotal>((object) doc);
                  nullable4 = doc.CuryOrderTotal;
                  if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                    continue;
                }
              }
              else
                continue;
            }
            else
              continue;
          }
          string str = doc.Behavior == "MO" ? (ARPaymentType.DrCr(soAdjust.AdjgDocType) == "C" ? "The applied amount cannot exceed the unrefunded balance. Delete the refund, or remove the refund application on the Payments and Applications (AR302000) form." : "The applied amount cannot exceed the unbilled balance. Delete or void the payment, or remove the payment application on the Payments and Applications (AR302000) form.") : "The applied amount cannot exceed the unbilled balance.";
          GraphHelper.MarkUpdated(((PXSelectBase) this.Adjustments).Cache, (object) soAdjust, true);
          ((PXSelectBase) this.Adjustments).Cache.RaiseExceptionHandling<SOAdjust.curyAdjdAmt>((object) soAdjust, (object) soAdjust.CuryAdjdAmt, (Exception) new PXSetPropertyException(str));
          throw new PXException(str);
        }
      }
    }
  }

  protected virtual bool TryAutoCompleteOrder(SOOrder order)
  {
    if (EnumerableExtensions.IsIn<string>(order.Behavior, "SO", "TR", "RM", "BL"))
    {
      int? shipmentCntr = order.ShipmentCntr;
      int num1 = 0;
      if (shipmentCntr.GetValueOrDefault() > num1 & shipmentCntr.HasValue)
      {
        int? openShipmentCntr = order.OpenShipmentCntr;
        int num2 = 0;
        if (openShipmentCntr.GetValueOrDefault() == num2 & openShipmentCntr.HasValue)
        {
          int? openLineCntr = order.OpenLineCntr;
          int num3 = 0;
          if (openLineCntr.GetValueOrDefault() == num3 & openLineCntr.HasValue)
          {
            if (!order.ForceCompleteOrder.GetValueOrDefault())
            {
              int? valueOriginal = (int?) ((PXSelectBase) this.Document).Cache.GetValueOriginal<SOOrder.openLineCntr>((object) order);
              int num4 = 0;
              if (!(valueOriginal.GetValueOrDefault() > num4 & valueOriginal.HasValue))
                goto label_7;
            }
            order.Approved = new bool?(this.Approval.IsApproved(order));
            order.Hold = new bool?(false);
            order.ForceCompleteOrder = new bool?(false);
            order.CreditHold = new bool?(false);
            order.InclCustOpenOrders = new bool?(true);
            order.MarkCompleted();
            ((PXSelectBase<SOOrder>) this.Document).Update(order);
            ((PXSelectBase<SOOrder>) this.Document).Search<SOOrder.orderNbr>((object) ((PXSelectBase<SOOrder>) this.Document).Current.OrderNbr, new object[1]
            {
              (object) ((PXSelectBase<SOOrder>) this.Document).Current.OrderType
            });
            return true;
          }
        }
      }
    }
label_7:
    return false;
  }

  protected void SetReadOnly(bool isReadOnly)
  {
    bool allowInsert = ((PXSelectBase) this.Document).Cache.AllowInsert;
    PXCache[] array = new PXCache[((Dictionary<System.Type, PXCache>) ((PXGraph) this).Caches).Count];
    try
    {
      ((Dictionary<System.Type, PXCache>) ((PXGraph) this).Caches).Values.CopyTo(array, 0);
    }
    catch (ArgumentException ex)
    {
      array = new PXCache[((Dictionary<System.Type, PXCache>) ((PXGraph) this).Caches).Count + 5];
      ((Dictionary<System.Type, PXCache>) ((PXGraph) this).Caches).Values.CopyTo(array, 0);
    }
    foreach (PXCache pxCache in array)
    {
      if (pxCache != null)
      {
        pxCache.AllowDelete = !isReadOnly;
        pxCache.AllowUpdate = !isReadOnly;
        pxCache.AllowInsert = !isReadOnly;
      }
    }
    ((PXSelectBase) this.Document).Cache.AllowInsert = allowInsert;
  }

  protected virtual void CopyParamFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    CopyParamFilter row = e.Row as CopyParamFilter;
    if (row.OrderType != null)
    {
      Numbering numbering = PXResultset<Numbering>.op_Implicit(PXSelectBase<Numbering, PXSelect<Numbering, Where<Numbering.numberingID, Equal<Required<Numbering.numberingID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) SOOrderType.PK.Find((PXGraph) this, row.OrderType).OrderNumberingID
      }));
      PXUIFieldAttribute.SetEnabled<CopyParamFilter.orderNbr>(sender, e.Row, numbering.UserNumbering.GetValueOrDefault());
    }
    else
      PXUIFieldAttribute.SetEnabled<CopyParamFilter.orderNbr>(sender, e.Row, false);
    ((PXAction) this.checkCopyParams).SetEnabled(!string.IsNullOrEmpty(row.OrderType) && !string.IsNullOrEmpty(row.OrderNbr));
    if (string.IsNullOrEmpty(row.OrderType))
      PXUIFieldAttribute.SetWarning<CopyParamFilter.orderType>(sender, e.Row, PXMessages.LocalizeFormatNoPrefix("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CopyParamFilter.orderType>(sender)
      }));
    else
      PXUIFieldAttribute.SetWarning<CopyParamFilter.orderType>(sender, e.Row, (string) null);
    if (string.IsNullOrEmpty(row.OrderNbr))
      PXUIFieldAttribute.SetWarning<CopyParamFilter.orderNbr>(sender, e.Row, PXMessages.LocalizeFormatNoPrefix("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CopyParamFilter.orderNbr>(sender)
      }));
    else
      PXUIFieldAttribute.SetWarning<CopyParamFilter.orderNbr>(sender, e.Row, (string) null);
    PXUIFieldAttribute.SetEnabled<CopyParamFilter.overrideManualDiscounts>(sender, (object) row, row.RecalcDiscounts.GetValueOrDefault() && ((PXSelectBase<SOOrder>) this.Document).Current != null && !((PXSelectBase<SOOrder>) this.Document).Current.DisableAutomaticDiscountCalculation.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CopyParamFilter.overrideManualPrices>(sender, (object) row, row.RecalcUnitPrices.GetValueOrDefault());
    sender.IsDirty = false;
  }

  protected virtual void CopyParamFilter_OrderType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CopyParamFilter row))
      return;
    if (row.OrderType != null)
      sender.SetDefaultExt<CopyParamFilter.orderNbr>(e.Row);
    else
      row.OrderNbr = (string) null;
  }

  public MultiDuplicatesSearchEngine<SOLine> DuplicateFinder { get; set; }

  private bool DontUpdateExistRecords
  {
    get
    {
      object obj;
      return ((PXGraph) this).IsImportFromExcel && PXExecutionContext.Current.Bag.TryGetValue("_DONT_UPDATE_EXIST_RECORDS", out obj) && true.Equals(obj);
    }
  }

  protected virtual System.Type[] GetAlternativeKeyFields()
  {
    List<System.Type> typeList = new List<System.Type>()
    {
      typeof (SOLine.branchID),
      typeof (SOLine.inventoryID),
      typeof (SOLine.siteID),
      typeof (SOLine.locationID),
      typeof (SOLine.alternateID),
      typeof (SOLine.invoiceNbr)
    };
    if (PXAccess.FeatureInstalled<FeaturesSet.subItem>())
      typeList.Add(typeof (SOLine.subItemID));
    return typeList.ToArray();
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (string.Compare(viewName, "Transactions", true) == 0)
    {
      if (values.Contains((object) "orderType"))
        values[(object) "orderType"] = (object) ((PXSelectBase<SOOrder>) this.Document).Current.OrderType;
      else
        values.Add((object) "orderType", (object) ((PXSelectBase<SOOrder>) this.Document).Current.OrderType);
      if (values.Contains((object) "orderNbr"))
        values[(object) "orderNbr"] = (object) ((PXSelectBase<SOOrder>) this.Document).Current.OrderNbr;
      else
        values.Add((object) "orderNbr", (object) ((PXSelectBase<SOOrder>) this.Document).Current.OrderNbr);
      if (!this.DontUpdateExistRecords)
      {
        if (this.DuplicateFinder == null)
        {
          SOLine[] items = ((PXSelectBase<SOLine>) this.Transactions).SelectMain(Array.Empty<object>());
          this.DuplicateFinder = new MultiDuplicatesSearchEngine<SOLine>(((PXSelectBase) this.Transactions).Cache, (IEnumerable<System.Type>) this.GetAlternativeKeyFields(), (ICollection<SOLine>) items);
        }
        SOLine soLine = this.DuplicateFinder.Find(values);
        if (soLine != null)
        {
          this.DuplicateFinder.RemoveItem(soLine);
          if (keys.Contains((object) "lineNbr"))
            keys[(object) "LineNbr"] = (object) soLine.LineNbr;
          else
            keys.Add((object) "LineNbr", (object) soLine.LineNbr);
        }
        else if (keys.Contains((object) "lineNbr"))
        {
          bool flag = false;
          object key = keys[(object) "lineNbr"];
          if (((PXSelectBase) this.Transactions).Cache.RaiseFieldUpdating<SOLine.lineNbr>((object) null, ref key) && key is int num)
            flag = ((PXSelectBase) this.Transactions).Cache.Locate((object) new SOLine()
            {
              OrderType = ((PXSelectBase<SOOrder>) this.Document).Current.OrderType,
              OrderNbr = ((PXSelectBase<SOOrder>) this.Document).Current.OrderNbr,
              LineNbr = new int?(num)
            }) != null;
          if (flag)
            keys.Remove((object) "lineNbr");
        }
      }
    }
    return true;
  }

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public virtual void ImportDone(PXImportAttribute.ImportMode.Value mode)
  {
    this.DuplicateFinder = (MultiDuplicatesSearchEngine<SOLine>) null;
    SOOrder current = ((PXSelectBase<SOOrder>) this.Document).Current;
    if (current == null)
      return;
    this.CalcFreight(current);
    ((PXSelectBase<SOOrder>) this.Document).UpdateCurrent();
    try
    {
      ((PXSelectBase<SOOrder>) this.Document).Current.DeferPriceDiscountRecalculation = new bool?(false);
      this._discountEngine.AutoRecalculatePricesAndDiscounts(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<SOLine>) this.Transactions, (SOLine) null, (PXSelectBase<SOOrderDiscountDetail>) this.DiscountDetails, ((PXSelectBase<SOOrder>) this.Document).Current.CustomerLocationID, ((PXSelectBase<SOOrder>) this.Document).Current.OrderDate, this.GetDefaultSODiscountCalculationOptions(((PXSelectBase<SOOrder>) this.Document).Current, true));
    }
    finally
    {
      ((PXSelectBase<SOOrder>) this.Document).Current.DeferPriceDiscountRecalculation = ((PXSelectBase<SOOrderType>) this.soordertype).Current.DeferPriceDiscountRecalculation;
    }
  }

  protected virtual void LoadEntityDiscounts()
  {
    if (((PXSelectBase<SOOrder>) this.Document).Current?.OrderNbr == null || !PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>() || this.prefetched.Contains(((PXSelectBase<SOOrder>) this.Document).Current.OrderType + ((PXSelectBase<SOOrder>) this.Document).Current.OrderNbr) || PXView.MaximumRows == 1)
      return;
    PXViewOf<SOLineShort>.BasedOn<SelectFromBase<SOLineShort, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<DiscountItem>.On<BqlOperand<DiscountItem.inventoryID, IBqlInt>.IsEqual<SOLineShort.inventoryID>>>, FbqlJoins.Left<PX.Objects.AR.DiscountSequence>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.DiscountSequence.isActive, Equal<True>>>>>.And<DiscountItem.FK.DiscountSequence>>>>.Where<KeysRelation<CompositeKey<Field<SOLineShort.orderType>.IsRelatedTo<SOOrder.orderType>, Field<SOLineShort.orderNbr>.IsRelatedTo<SOOrder.orderNbr>>.WithTablesOf<SOOrder, SOLineShort>, SOOrder, SOLineShort>.SameAsCurrent>>.ReadOnly readOnly = new PXViewOf<SOLineShort>.BasedOn<SelectFromBase<SOLineShort, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<DiscountItem>.On<BqlOperand<DiscountItem.inventoryID, IBqlInt>.IsEqual<SOLineShort.inventoryID>>>, FbqlJoins.Left<PX.Objects.AR.DiscountSequence>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.DiscountSequence.isActive, Equal<True>>>>>.And<DiscountItem.FK.DiscountSequence>>>>.Where<KeysRelation<CompositeKey<Field<SOLineShort.orderType>.IsRelatedTo<SOOrder.orderType>, Field<SOLineShort.orderNbr>.IsRelatedTo<SOOrder.orderNbr>>.WithTablesOf<SOOrder, SOLineShort>, SOOrder, SOLineShort>.SameAsCurrent>>.ReadOnly((PXGraph) this);
    Dictionary<int, HashSet<DiscountSequenceKey>> items = new Dictionary<int, HashSet<DiscountSequenceKey>>();
    using (new PXFieldScope(((PXSelectBase) readOnly).View, new System.Type[3]
    {
      typeof (SOLineShort.inventoryID),
      typeof (PX.Objects.AR.DiscountSequence.discountID),
      typeof (PX.Objects.AR.DiscountSequence.discountSequenceID)
    }))
    {
      foreach (PXResult<SOLineShort, DiscountItem, PX.Objects.AR.DiscountSequence> pxResult in ((PXSelectBase<SOLineShort>) readOnly).Select(Array.Empty<object>()))
      {
        SOLineShort soLineShort = PXResult<SOLineShort, DiscountItem, PX.Objects.AR.DiscountSequence>.op_Implicit(pxResult);
        PX.Objects.AR.DiscountSequence discountSequence = PXResult<SOLineShort, DiscountItem, PX.Objects.AR.DiscountSequence>.op_Implicit(pxResult);
        int? inventoryId = soLineShort.InventoryID;
        if (inventoryId.HasValue)
        {
          Dictionary<int, HashSet<DiscountSequenceKey>> dictionary1 = items;
          inventoryId = soLineShort.InventoryID;
          int key1 = inventoryId.Value;
          HashSet<DiscountSequenceKey> discountSequenceKeySet1;
          ref HashSet<DiscountSequenceKey> local = ref discountSequenceKeySet1;
          if (!dictionary1.TryGetValue(key1, out local))
          {
            Dictionary<int, HashSet<DiscountSequenceKey>> dictionary2 = items;
            inventoryId = soLineShort.InventoryID;
            int key2 = inventoryId.Value;
            HashSet<DiscountSequenceKey> discountSequenceKeySet2;
            discountSequenceKeySet1 = discountSequenceKeySet2 = new HashSet<DiscountSequenceKey>();
            dictionary2.Add(key2, discountSequenceKeySet2);
          }
          if (discountSequence.DiscountID != null && discountSequence.DiscountSequenceID != null)
            discountSequenceKeySet1.Add(new DiscountSequenceKey(discountSequence.DiscountID, discountSequence.DiscountSequenceID));
        }
      }
    }
    DiscountEngine.UpdateEntityCache();
    DiscountEngine.PutEntityDiscountsToSlot<DiscountItem, int>((ICollection<KeyValuePair<int, HashSet<DiscountSequenceKey>>>) items);
    this.prefetched.Add(((PXSelectBase<SOOrder>) this.Document).Current.OrderType + ((PXSelectBase<SOOrder>) this.Document).Current.OrderNbr);
  }

  public virtual UpdateIfFieldsChangedScope GetPriceCalculationScope()
  {
    return (UpdateIfFieldsChangedScope) new SOOrderPriceCalculationScope();
  }

  public virtual void ConfirmSingleLine(
    SOLine line,
    SOShipLine shipline,
    string lineShippingRule,
    ref bool backorderExists)
  {
    if (line.POSource == "D")
      return;
    using (this.LineSplittingExt.SuppressedModeScope(true))
    {
      short? lineSign = line.LineSign;
      Decimal? nullable1 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable2 = line.BaseShippedQty;
      Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
      lineSign = line.LineSign;
      nullable2 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
      nullable1 = line.BaseOrderQty;
      Decimal? nullable4 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?();
      bool? nullable5 = line.IsFree;
      if (nullable5.GetValueOrDefault())
      {
        nullable5 = line.ManualDisc;
        bool flag = false;
        if (nullable5.GetValueOrDefault() == flag & nullable5.HasValue)
        {
          nullable1 = nullable3;
          Decimal? nullable6 = nullable4;
          Decimal? completeQtyMin = line.CompleteQtyMin;
          nullable2 = nullable6.HasValue & completeQtyMin.HasValue ? new Decimal?(nullable6.GetValueOrDefault() * completeQtyMin.GetValueOrDefault() / 100M) : new Decimal?();
          if (nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue || !backorderExists)
          {
            line.OpenQty = new Decimal?(0M);
            line.Completed = new bool?(true);
            line.ClosedQty = line.OrderQty;
            line.BaseClosedQty = line.BaseOrderQty;
            line.OpenLine = new bool?(false);
            line = ((PXSelectBase<SOLine>) this.Transactions).Update(line);
            this.LineSplittingAllocatedExt.CompleteSchedules(line);
            return;
          }
          SOLine soLine1 = line;
          nullable2 = line.OrderQty;
          nullable1 = line.ShippedQty;
          Decimal? nullable7 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
          soLine1.OpenQty = nullable7;
          SOLine soLine2 = line;
          nullable1 = line.BaseOrderQty;
          nullable2 = line.BaseShippedQty;
          Decimal? nullable8 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
          soLine2.BaseOpenQty = nullable8;
          line.ClosedQty = line.ShippedQty;
          line.BaseClosedQty = line.BaseShippedQty;
          line = ((PXSelectBase<SOLine>) this.Transactions).Update(line);
          return;
        }
      }
      if (lineShippingRule == "B")
      {
        nullable2 = nullable3;
        Decimal? nullable9 = nullable4;
        Decimal? completeQtyMin = line.CompleteQtyMin;
        nullable1 = nullable9.HasValue & completeQtyMin.HasValue ? new Decimal?(nullable9.GetValueOrDefault() * completeQtyMin.GetValueOrDefault() / 100M) : new Decimal?();
        if (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
        {
          SOLine soLine3 = line;
          nullable1 = line.OrderQty;
          nullable2 = line.ShippedQty;
          Decimal? nullable10 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
          soLine3.OpenQty = nullable10;
          SOLine soLine4 = line;
          nullable2 = line.BaseOrderQty;
          nullable1 = line.BaseShippedQty;
          Decimal? nullable11 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
          soLine4.BaseOpenQty = nullable11;
          line.ClosedQty = line.ShippedQty;
          line.BaseClosedQty = line.BaseShippedQty;
          line = ((PXSelectBase<SOLine>) this.Transactions).Update(line);
          backorderExists = true;
          return;
        }
      }
      if (shipline.ShipmentNbr == null && !(lineShippingRule != "C"))
        return;
      int? openLineCntr = ((PXSelectBase<SOOrder>) this.Document).Current.OpenLineCntr;
      int num = 0;
      if (openLineCntr.GetValueOrDefault() <= num & openLineCntr.HasValue)
        ((PXSelectBase<SOOrder>) this.Document).Current.Completed = new bool?(true);
      line.OpenQty = new Decimal?(0M);
      line.ClosedQty = line.OrderQty;
      line.BaseClosedQty = line.BaseOrderQty;
      line.OpenLine = new bool?(false);
      line.Completed = new bool?(true);
      line = ((PXSelectBase<SOLine>) this.Transactions).Update(line);
      this.LineSplittingAllocatedExt.CompleteSchedules(line);
    }
  }

  public virtual SOLine CorrectSingleLine(
    SOLine line,
    SOShipLine shipLine,
    bool lineSwitched,
    Dictionary<int?, (SOLine, Decimal?, Decimal?)> lineOpenQuantities)
  {
    line = PXCache<SOLine>.CreateCopy(line);
    line.Completed = new bool?(false);
    Decimal? nullable1 = new Decimal?(0M);
    Decimal? nullable2 = new Decimal?(0M);
    Decimal? nullable3;
    Decimal? nullable4;
    Decimal? nullable5;
    if (shipLine.ShippedQty.GetValueOrDefault() == 0M)
    {
      SOLine soLine1 = line;
      Decimal? orderQty = line.OrderQty;
      nullable3 = line.ShippedQty;
      Decimal? nullable6 = orderQty.HasValue & nullable3.HasValue ? new Decimal?(orderQty.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
      soLine1.OpenQty = nullable6;
      short? lineSign1 = line.LineSign;
      nullable3 = lineSign1.HasValue ? new Decimal?((Decimal) lineSign1.GetValueOrDefault()) : new Decimal?();
      nullable4 = line.OpenQty;
      nullable1 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * nullable4.GetValueOrDefault()) : new Decimal?();
      SOLine soLine2 = line;
      nullable4 = line.BaseOrderQty;
      nullable3 = line.BaseShippedQty;
      Decimal? nullable7 = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
      soLine2.BaseOpenQty = nullable7;
      short? lineSign2 = line.LineSign;
      nullable3 = lineSign2.HasValue ? new Decimal?((Decimal) lineSign2.GetValueOrDefault()) : new Decimal?();
      nullable4 = line.BaseOpenQty;
      nullable2 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * nullable4.GetValueOrDefault()) : new Decimal?();
      line.ClosedQty = line.ShippedQty;
      line.BaseClosedQty = line.BaseShippedQty;
    }
    else
    {
      if (lineSwitched)
      {
        SOLine soLine3 = line;
        Decimal? orderQty = line.OrderQty;
        nullable3 = line.ShippedQty;
        Decimal? nullable8 = orderQty.HasValue & nullable3.HasValue ? new Decimal?(orderQty.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
        soLine3.OpenQty = nullable8;
        short? lineSign3 = line.LineSign;
        nullable3 = lineSign3.HasValue ? new Decimal?((Decimal) lineSign3.GetValueOrDefault()) : new Decimal?();
        nullable4 = line.OpenQty;
        nullable1 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * nullable4.GetValueOrDefault()) : new Decimal?();
        SOLine soLine4 = line;
        SOLine soLine5 = line;
        nullable3 = line.BaseOrderQty;
        nullable5 = line.BaseShippedQty;
        Decimal? nullable9;
        nullable4 = nullable9 = nullable3.HasValue & nullable5.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
        soLine5.BaseOpenQty = nullable9;
        Decimal? nullable10 = nullable4;
        soLine4.BaseClosedQty = nullable10;
        short? lineSign4 = line.LineSign;
        Decimal? nullable11;
        if (!lineSign4.HasValue)
        {
          nullable3 = new Decimal?();
          nullable11 = nullable3;
        }
        else
          nullable11 = new Decimal?((Decimal) lineSign4.GetValueOrDefault());
        nullable4 = nullable11;
        nullable5 = line.BaseOpenQty;
        Decimal? nullable12;
        if (!(nullable4.HasValue & nullable5.HasValue))
        {
          nullable3 = new Decimal?();
          nullable12 = nullable3;
        }
        else
          nullable12 = new Decimal?(nullable4.GetValueOrDefault() * nullable5.GetValueOrDefault());
        nullable2 = nullable12;
        line.ClosedQty = line.ShippedQty;
        line.BaseClosedQty = line.BaseShippedQty;
      }
      SOLine soLine6 = line;
      nullable5 = soLine6.BaseOpenQty;
      short? soLineSign1 = shipLine.SOLineSign;
      nullable3 = soLineSign1.HasValue ? new Decimal?((Decimal) soLineSign1.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable13 = shipLine.BaseShippedQty;
      nullable4 = nullable3.HasValue & nullable13.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * nullable13.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable14;
      if (!(nullable5.HasValue & nullable4.HasValue))
      {
        nullable13 = new Decimal?();
        nullable14 = nullable13;
      }
      else
        nullable14 = new Decimal?(nullable5.GetValueOrDefault() + nullable4.GetValueOrDefault());
      soLine6.BaseOpenQty = nullable14;
      nullable4 = line.BaseOpenQty;
      nullable5 = line.BaseOrderQty;
      if (nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue)
        line.OpenQty = line.OrderQty;
      else
        PXDBQuantityAttribute.CalcTranQty<SOLine.openQty>(((PXSelectBase) this.Transactions).Cache, (object) line);
      SOLine soLine7 = line;
      nullable5 = soLine7.BaseClosedQty;
      short? soLineSign2 = shipLine.SOLineSign;
      nullable13 = soLineSign2.HasValue ? new Decimal?((Decimal) soLineSign2.GetValueOrDefault()) : new Decimal?();
      nullable3 = shipLine.BaseShippedQty;
      nullable4 = nullable13.HasValue & nullable3.HasValue ? new Decimal?(nullable13.GetValueOrDefault() * nullable3.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable15;
      if (!(nullable5.HasValue & nullable4.HasValue))
      {
        nullable3 = new Decimal?();
        nullable15 = nullable3;
      }
      else
        nullable15 = new Decimal?(nullable5.GetValueOrDefault() - nullable4.GetValueOrDefault());
      soLine7.BaseClosedQty = nullable15;
      PXQuantityAttribute.CalcTranQty<SOLine.closedQty>(((PXSelectBase) this.Transactions).Cache, (object) line);
    }
    line = ((PXSelectBase<SOLine>) this.Transactions).Update(line);
    line.OpenLine = (bool?) PXFormulaAttribute.Evaluate<SOLine.openLine>(((PXSelectBase) this.Transactions).Cache, (object) line);
    if (!lineOpenQuantities.ContainsKey(line.LineNbr))
    {
      bool? nullable16 = line.POCreate;
      if (nullable16.GetValueOrDefault())
      {
        nullable4 = line.ShippedQty;
        nullable5 = line.Qty;
        if (!(nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue))
        {
          foreach (SOLineSplit selectChild in PXParentAttribute.SelectChildren(((PXSelectBase) this.splits).Cache, (object) line, typeof (SOLine)))
          {
            nullable16 = selectChild.POCreate;
            if (nullable16.GetValueOrDefault())
            {
              nullable16 = selectChild.POCompleted;
              if (!nullable16.GetValueOrDefault())
              {
                nullable16 = selectChild.POCancelled;
                if (!nullable16.GetValueOrDefault() && selectChild.POReceiptNbr == null)
                {
                  nullable16 = selectChild.Completed;
                  if (!nullable16.GetValueOrDefault())
                  {
                    nullable16 = selectChild.IsAllocated;
                    if (!nullable16.GetValueOrDefault())
                    {
                      Decimal num1;
                      if (!(line.UOM == selectChild.UOM))
                      {
                        PXCache cache = ((PXSelectBase) this.splits).Cache;
                        int? inventoryId = selectChild.InventoryID;
                        string uom = line.UOM;
                        nullable5 = selectChild.BaseUnreceivedQty;
                        Decimal valueOrDefault = nullable5.GetValueOrDefault();
                        num1 = INUnitAttribute.ConvertFromBase(cache, inventoryId, uom, valueOrDefault, INPrecision.QUANTITY);
                      }
                      else
                      {
                        nullable5 = selectChild.UnreceivedQty;
                        num1 = nullable5.GetValueOrDefault();
                      }
                      nullable5 = nullable2;
                      nullable4 = selectChild.BaseUnreceivedQty;
                      Decimal? nullable17;
                      if (!(nullable5.HasValue & nullable4.HasValue))
                      {
                        nullable3 = new Decimal?();
                        nullable17 = nullable3;
                      }
                      else
                        nullable17 = new Decimal?(nullable5.GetValueOrDefault() - nullable4.GetValueOrDefault());
                      nullable2 = nullable17;
                      nullable4 = nullable1;
                      Decimal num2 = num1;
                      Decimal? nullable18;
                      if (!nullable4.HasValue)
                      {
                        nullable5 = new Decimal?();
                        nullable18 = nullable5;
                      }
                      else
                        nullable18 = new Decimal?(nullable4.GetValueOrDefault() - num2);
                      nullable1 = nullable18;
                    }
                  }
                }
              }
            }
          }
        }
      }
      lineOpenQuantities.Add(line.LineNbr, (line, nullable2, nullable1));
    }
    return line;
  }

  protected virtual bool HasMiscLinesToInvoice(SOOrder order)
  {
    Decimal? orderQty = order.OrderQty;
    Decimal num1 = 0M;
    if (!(orderQty.GetValueOrDefault() == num1 & orderQty.HasValue))
    {
      int? openLineCntr = order.OpenLineCntr;
      int num2 = 0;
      if (openLineCntr.GetValueOrDefault() == num2 & openLineCntr.HasValue)
      {
        bool? legacyMiscBilling = order.IsLegacyMiscBilling;
        bool flag = false;
        if (legacyMiscBilling.GetValueOrDefault() == flag & legacyMiscBilling.HasValue)
          goto label_3;
      }
      return false;
    }
label_3:
    Decimal? curyUnbilledMiscTot = order.CuryUnbilledMiscTot;
    Decimal num3 = 0M;
    if (!(curyUnbilledMiscTot.GetValueOrDefault() == num3 & curyUnbilledMiscTot.HasValue))
      return true;
    Decimal? unbilledOrderQty = order.UnbilledOrderQty;
    Decimal num4 = 0M;
    return unbilledOrderQty.GetValueOrDefault() > num4 & unbilledOrderQty.HasValue;
  }

  public SOOrderEntry.SOQuickProcess SOQuickProcessExt
  {
    get => ((PXGraph) this).FindImplementation<SOOrderEntry.SOQuickProcess>();
  }

  public SOOrderEntry.CarrierRates CarrierRatesExt
  {
    get => ((PXGraph) this).FindImplementation<SOOrderEntry.CarrierRates>();
  }

  public delegate void OrderCreatedDelegate(SOOrder document, SOOrder source);

  public class SkipCalculateTotalDocDiscountScope : 
    FlaggedModeScopeBase<SOOrderEntry.SkipCalculateTotalDocDiscountScope>
  {
  }

  public class MinGrossProfitClass
  {
    public Decimal? CuryUnitPrice { get; set; }

    public Decimal? CuryDiscAmt { get; set; }

    public Decimal? DiscPct { get; set; }
  }

  public class SOQuickProcess : PXGraphExtension<SOOrderEntry>
  {
    public PXQuickProcess.Action<SOOrder>.ConfiguredBy<SOQuickProcessParameters> quickProcess;
    public PXAction<SOOrder> quickProcessOk;
    public PXFilter<SOQuickProcessParameters> QuickProcessParameters;

    public static bool IsActive() => true;

    [PXButton(CommitChanges = true)]
    [PXUIField(DisplayName = "Quick Process")]
    protected virtual IEnumerable QuickProcess(PXAdapter adapter)
    {
      // ISSUE: method pointer
      ((PXSelectBase<SOQuickProcessParameters>) this.QuickProcessParameters).AskExt(new PXView.InitializePanel((object) null, __methodptr(InitQuickProcessPanel)));
      ((PXAction) this.Base.Save).Press();
      PXQuickProcess.Start<SOOrderEntry, SOOrder, SOQuickProcessParameters>(this.Base, ((PXSelectBase<SOOrder>) this.Base.Document).Current, ((PXSelectBase<SOQuickProcessParameters>) this.QuickProcessParameters).Current);
      return (IEnumerable) new SOOrder[1]
      {
        ((PXSelectBase<SOOrder>) this.Base.Document).Current
      };
    }

    [PXButton]
    [PXUIField(DisplayName = "OK")]
    public virtual IEnumerable QuickProcessOk(PXAdapter adapter) => adapter.Get();

    [Obsolete]
    protected virtual void SOOrder_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
    {
    }

    /// <summary><see cref="P:PX.Objects.SO.SOLine.SiteID" /> Updated</summary>
    protected virtual void SOLine_SiteID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
    {
      SOOrderType current = ((PXSelectBase<SOOrderType>) this.Base.soordertype).Current;
      if ((current != null ? (current.AllowQuickProcess.GetValueOrDefault() ? 1 : 0) : 0) == 0 || string.IsNullOrEmpty(((PXSelectBase<SOQuickProcessParameters>) this.QuickProcessParameters).Current?.OrderType))
        return;
      ((PXSelectBase<SOQuickProcessParameters>) this.QuickProcessParameters).Current.SiteID = new int?();
    }

    [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
    [PXCustomizeBaseAttribute(typeof (PXDBStringAttribute), "IsKey", false)]
    protected virtual void SOQuickProcessParameters_OrderType_CacheAttached(PXCache cache)
    {
    }

    /// <summary><see cref="P:PX.Objects.SO.SOQuickProcessParameters.SiteID" /> Updated</summary>
    protected virtual void SOQuickProcessParameters_SiteID_FieldUpdated(
      PXCache sender,
      PXFieldUpdatedEventArgs e)
    {
      this.RecalculateAvailabilityStatus((SOQuickProcessParameters) e.Row);
      this.EnsureSiteID(sender, (SOQuickProcessParameters) e.Row);
    }

    /// <summary><see cref="!:SOQuickProcessParameters.ShipDate" /> Updated</summary>
    protected virtual void SOQuickProcessParameters_ShipDate_FieldUpdated(
      PXCache sender,
      PXFieldUpdatedEventArgs e)
    {
      this.RecalculateAvailabilityStatus((SOQuickProcessParameters) e.Row);
    }

    /// <summary><see cref="T:PX.Objects.SO.SOQuickProcessParameters" /> Inserted</summary>
    protected virtual void SOQuickProcessParameters_RowInserted(
      PXCache sender,
      PXRowInsertedEventArgs e)
    {
      sender.IsDirty = false;
    }

    /// <summary><see cref="T:PX.Objects.SO.SOQuickProcessParameters" /> Updated</summary>
    protected virtual void SOQuickProcessParameters_RowUpdated(
      PXCache sender,
      PXRowUpdatedEventArgs e)
    {
      sender.IsDirty = false;
    }

    /// <summary><see cref="T:PX.Objects.SO.SOQuickProcessParameters" /> Selected</summary>
    protected virtual void SOQuickProcessParameters_RowSelected(
      PXCache sender,
      PXRowSelectedEventArgs e)
    {
      ((PXAction) this.quickProcessOk).SetEnabled(false);
      SOQuickProcessParameters row = (SOQuickProcessParameters) e.Row;
      if (row == null)
        return;
      SOOrderType current = ((PXSelectBase<SOOrderType>) this.Base.soordertype).Current;
      if ((current != null ? (current.AllowQuickProcess.GetValueOrDefault() ? 1 : 0) : 0) == 0 || string.IsNullOrEmpty(row.OrderType))
        return;
      this.DisplayAvailabilityStatusMessage(row);
      this.VerifyPrepareInvoice(row);
      this.VerifyPrintInvoice(row);
      this.VerifyEmailInvoice(row);
      if (row.CreateShipment.GetValueOrDefault())
      {
        Decimal? openOrderQty = ((PXSelectBase<SOOrder>) this.Base.Document).Current.OpenOrderQty;
        Decimal num = 0M;
        bool flag1 = (openOrderQty.GetValueOrDefault() > num & openOrderQty.HasValue && PXAccess.FeatureInstalled<FeaturesSet.warehouse>()).Implies(row.SiteID.HasValue);
        bool flag2 = EnumerableExtensions.IsIn<string>(((PXSelectBase) this.QuickProcessParameters).Cache.GetExtension<SOQuickProcessParametersAvailabilityExt>((object) row).AvailabilityStatus, "AL", "PB", "PC");
        PXCache pxCache = sender;
        SOQuickProcessParameters processParameters = row;
        // ISSUE: variable of a boxed type
        __Boxed<int?> siteId = (ValueType) row.SiteID;
        PXSetPropertyException propertyException;
        if (!flag1)
          propertyException = new PXSetPropertyException("'{0}' cannot be empty.", new object[2]
          {
            (object) PXUIFieldAttribute.GetDisplayName<SOQuickProcessParameters.siteID>(sender),
            (object) (PXErrorLevel) 4
          });
        else
          propertyException = (PXSetPropertyException) null;
        pxCache.RaiseExceptionHandling<SOQuickProcessParameters.siteID>((object) processParameters, (object) siteId, (Exception) propertyException);
        sender.RaiseExceptionHandling<SOQuickProcessParameters.createShipment>((object) row, (object) row.CreateShipment, flag2 & flag1 ? (Exception) null : (Exception) new PXSetPropertyException("The order cannot be shipped. See availability section for more info.", (PXErrorLevel) 4));
        ((PXAction) this.quickProcessOk).SetEnabled(flag2 & flag1);
      }
      else
        ((PXAction) this.quickProcessOk).SetEnabled(true);
    }

    protected virtual void DisplayAvailabilityStatusMessage(SOQuickProcessParameters row)
    {
      SOQuickProcessParametersAvailabilityExt extension = PXCacheEx.GetExtension<SOQuickProcessParametersAvailabilityExt>((IBqlTable) row);
      if (string.IsNullOrEmpty(extension.AvailabilityMessage))
        return;
      bool flag1 = EnumerableExtensions.IsIn<string>(extension.AvailabilityStatus, "NA", "NT", "NI");
      bool flag2 = EnumerableExtensions.IsIn<string>(extension.AvailabilityStatus, "PC", "PB") || extension.AvailabilityStatus == "AL" && !string.IsNullOrEmpty(extension.SkipByDateMsg);
      int num = !(extension.AvailabilityStatus == "AL") ? 0 : (string.IsNullOrEmpty(extension.SkipByDateMsg) ? 1 : 0);
      ((PXSelectBase) this.QuickProcessParameters).Cache.RaiseExceptionHandling<SOQuickProcessParametersAvailabilityExt.availabilityStatusMessage>((object) row, (object) extension.AvailabilityMessage, (Exception) new PXSetPropertyException(extension.AvailabilityMessage, flag1 ? (PXErrorLevel) 5 : (flag2 ? (PXErrorLevel) 3 : (PXErrorLevel) 1)));
    }

    protected virtual void EnsureSiteID(PXCache sender, SOQuickProcessParameters row)
    {
      if (row.SiteID.HasValue)
        return;
      int? preferedSiteId = this.Base.GetPreferedSiteID();
      if (!preferedSiteId.HasValue)
        return;
      sender.SetValueExt<SOQuickProcessParameters.siteID>((object) row, (object) preferedSiteId);
    }

    protected virtual void VerifyPrepareInvoice(SOQuickProcessParameters row)
    {
      if (row == null || ((PXSelectBase<SOOrder>) this.Base.Document).Current == null)
        return;
      SOOrder current = ((PXSelectBase<SOOrder>) this.Base.Document).Current;
      int? shipmentCntr = current.ShipmentCntr;
      int? nullable1 = current.OpenShipmentCntr;
      int? nullable2 = shipmentCntr.HasValue & nullable1.HasValue ? new int?(shipmentCntr.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new int?();
      int? nullable3 = current.BilledCntr;
      int? nullable4;
      if (!(nullable2.HasValue & nullable3.HasValue))
      {
        nullable1 = new int?();
        nullable4 = nullable1;
      }
      else
        nullable4 = new int?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
      int? nullable5 = nullable4;
      int? releasedCntr = current.ReleasedCntr;
      int? nullable6;
      if (!(nullable5.HasValue & releasedCntr.HasValue))
      {
        nullable3 = new int?();
        nullable6 = nullable3;
      }
      else
        nullable6 = new int?(nullable5.GetValueOrDefault() - releasedCntr.GetValueOrDefault());
      int? nullable7 = nullable6;
      int num = 0;
      bool flag = nullable7.GetValueOrDefault() > num & nullable7.HasValue;
      ((PXSelectBase) this.QuickProcessParameters).Cache.RaiseExceptionHandling<SOQuickProcessParameters.prepareInvoiceFromShipment>((object) row, (object) row.PrepareInvoiceFromShipment, row.PrepareInvoiceFromShipment.GetValueOrDefault() & flag ? (Exception) new PXSetPropertyException<SOQuickProcessParameters.prepareInvoiceFromShipment>("Only the shipment created by this process will be invoiced.", (PXErrorLevel) 2) : (Exception) null);
    }

    protected virtual void VerifyEmailInvoice(SOQuickProcessParameters row)
    {
      if (row == null || ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current == null)
        return;
      PXCache cache = ((PXSelectBase) this.QuickProcessParameters).Cache;
      SOQuickProcessParameters processParameters = row;
      // ISSUE: variable of a boxed type
      __Boxed<bool?> emailInvoice1 = (ValueType) row.EmailInvoice;
      bool? emailInvoice2 = row.EmailInvoice;
      bool? nullable = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current.MailInvoices;
      PXSetPropertyException<SOQuickProcessParameters.emailInvoice> propertyException;
      if (emailInvoice2.GetValueOrDefault() == nullable.GetValueOrDefault() & emailInvoice2.HasValue == nullable.HasValue)
      {
        propertyException = (PXSetPropertyException<SOQuickProcessParameters.emailInvoice>) null;
      }
      else
      {
        nullable = row.EmailInvoice;
        propertyException = new PXSetPropertyException<SOQuickProcessParameters.emailInvoice>(nullable.GetValueOrDefault() ? "Invoice will be emailed during quick processing though the {0} customer does not require sending invoices by email." : "Invoice emailing will be skipped during quick processing though the {0} customer requires sending invoices by email.", (PXErrorLevel) 2, new object[1]
        {
          (object) ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current.AcctCD
        });
      }
      cache.RaiseExceptionHandling<SOQuickProcessParameters.emailInvoice>((object) processParameters, (object) emailInvoice1, (Exception) propertyException);
    }

    protected virtual void VerifyPrintInvoice(SOQuickProcessParameters row)
    {
      if (row == null || ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current == null)
        return;
      bool? printInvoice = ((PXSelectBase) this.QuickProcessParameters).Cache.GetExtension<SOQuickProcessParametersReportsExt>((object) row).PrintInvoice;
      PXCache cache = ((PXSelectBase) this.QuickProcessParameters).Cache;
      SOQuickProcessParameters processParameters = row;
      // ISSUE: variable of a boxed type
      __Boxed<bool?> local = (ValueType) printInvoice;
      bool? nullable = printInvoice;
      bool? printInvoices = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current.PrintInvoices;
      PXSetPropertyException<SOQuickProcessParametersReportsExt.printInvoice> propertyException;
      if (nullable.GetValueOrDefault() == printInvoices.GetValueOrDefault() & nullable.HasValue == printInvoices.HasValue)
        propertyException = (PXSetPropertyException<SOQuickProcessParametersReportsExt.printInvoice>) null;
      else
        propertyException = new PXSetPropertyException<SOQuickProcessParametersReportsExt.printInvoice>(printInvoice.GetValueOrDefault() ? "Invoice will be printed during quick processing though the {0} customer does not require printing invoices." : "Invoice printing will be skipped during quick processing though the {0} customer requires printing invoices.", (PXErrorLevel) 2, new object[1]
        {
          (object) ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current.AcctCD
        });
      cache.RaiseExceptionHandling<SOQuickProcessParametersReportsExt.printInvoice>((object) processParameters, (object) local, (Exception) propertyException);
    }

    protected virtual Tuple<string, int> OrderAvailabilityStatus(int? siteID, DateTime? shipDate)
    {
      if (!siteID.HasValue || !shipDate.HasValue)
        return Tuple.Create<string, int>("NT", 0);
      List<SOLineSplit> list1 = GraphHelper.RowCast<SOLineSplit>((IEnumerable) PXSelectBase<SOLineSplit, PXViewOf<SOLineSplit>.BasedOn<SelectFromBase<SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<SOLineSplit.orderType>.IsRelatedTo<SOOrder.orderType>, Field<SOLineSplit.orderNbr>.IsRelatedTo<SOOrder.orderNbr>>.WithTablesOf<SOOrder, SOLineSplit>, SOOrder, SOLineSplit>.SameAsCurrent.And<BqlOperand<SOLineSplit.completed, IBqlBool>.IsEqual<False>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).Where<SOLineSplit>((Func<SOLineSplit, bool>) (s =>
      {
        int? siteId = s.SiteID;
        int? nullable1 = siteID;
        if (!(siteId.GetValueOrDefault() == nullable1.GetValueOrDefault() & siteId.HasValue == nullable1.HasValue))
        {
          int? toSiteId = s.ToSiteID;
          int? nullable2 = siteID;
          if (!(toSiteId.GetValueOrDefault() == nullable2.GetValueOrDefault() & toSiteId.HasValue == nullable2.HasValue) || !s.IsAllocated.GetValueOrDefault())
            goto label_4;
        }
        if (EnumerableExtensions.IsIn<string>(s.LineType, "GI", "GN") && s.RequireShipping.GetValueOrDefault())
          return s.Operation == "I";
label_4:
        return false;
      })).ToList<SOLineSplit>();
      List<\u003C\u003Ef__AnonymousType108<\u003C\u003Ef__AnonymousType107<int?, int?, int?, bool, bool, bool, bool, bool, int?>, Decimal?, Decimal?, Decimal?, Decimal?>> list2 = list1.GroupBy(t =>
      {
        int? inventoryId = t.InventoryID;
        int? siteId = t.SiteID;
        int? subItemId = t.SubItemID;
        int? nullable3;
        int? nullable4;
        int num1;
        if (t.IsAllocated.GetValueOrDefault())
        {
          nullable3 = t.SiteID;
          nullable4 = siteID;
          num1 = nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue ? 1 : 0;
        }
        else
          num1 = 0;
        bool? nullable5 = t.IsAllocated;
        int num2;
        if (nullable5.GetValueOrDefault())
        {
          nullable4 = t.SiteID;
          nullable3 = siteID;
          num2 = !(nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue) ? 1 : 0;
        }
        else
          num2 = 0;
        nullable5 = t.POCreate;
        int num3 = nullable5.GetValueOrDefault() ? 1 : 0;
        int num4 = t.LineType == "GN" ? 1 : 0;
        DateTime? shipDate1 = t.ShipDate;
        DateTime? nullable6 = shipDate;
        int num5 = shipDate1.HasValue & nullable6.HasValue ? (shipDate1.GetValueOrDefault() > nullable6.GetValueOrDefault() ? 1 : 0) : 0;
        int? costCenterId = t.CostCenterID;
        return new
        {
          InventoryID = inventoryId,
          SiteID = siteId,
          SubItemID = subItemId,
          Allocated = num1 != 0,
          RemoteAllocated = num2 != 0,
          MarkedForPO = num3 != 0,
          NonStock = num4 != 0,
          FutureShipments = num5 != 0,
          CostCenterID = costCenterId
        };
      }).Select(tg => new
      {
        Item = tg.Key,
        SumBaseQty = tg.Sum<SOLineSplit>((Func<SOLineSplit, Decimal?>) (q => q.BaseQty)),
        SumBaseReceivedQty = tg.Sum<SOLineSplit>((Func<SOLineSplit, Decimal?>) (q => q.BaseReceivedQty)),
        SumDeduction = tg.Key.Allocated || tg.Key.MarkedForPO || tg.Key.NonStock || tg.Key.FutureShipments ? new Decimal?(0M) : tg.Sum<SOLineSplit>((Func<SOLineSplit, Decimal?>) (q => q.BaseQty)),
        AvailableForShipping = tg.Key.RemoteAllocated || tg.Key.MarkedForPO || tg.Key.NonStock || tg.Key.FutureShipments ? new Decimal?(0M) : availabilityFetch(tg.Key.InventoryID, tg.Key.SubItemID, siteID, tg.Key.CostCenterID)
      }).ToList();
      IEnumerable<\u003C\u003Ef__AnonymousType108<\u003C\u003Ef__AnonymousType107<int?, int?, int?, bool, bool, bool, bool, bool, int?>, Decimal?, Decimal?, Decimal?, Decimal?>> source1 = list2.Where(s =>
      {
        if (!s.Item.FutureShipments)
        {
          Decimal? nullable = s.SumBaseQty;
          Decimal num6 = 0M;
          if (nullable.GetValueOrDefault() > num6 & nullable.HasValue)
          {
            nullable = s.AvailableForShipping;
            Decimal num7 = 0M;
            return nullable.GetValueOrDefault() > num7 & nullable.HasValue || s.Item.NonStock || s.Item.Allocated;
          }
        }
        return false;
      });
      IEnumerable<\u003C\u003Ef__AnonymousType108<\u003C\u003Ef__AnonymousType107<int?, int?, int?, bool, bool, bool, bool, bool, int?>, Decimal?, Decimal?, Decimal?, Decimal?>> source2 = list2.Where(s =>
      {
        if (!s.Item.FutureShipments)
        {
          Decimal? sumBaseQty = s.SumBaseQty;
          Decimal num = 0M;
          if (sumBaseQty.GetValueOrDefault() > num & sumBaseQty.HasValue)
            return s.Item.RemoteAllocated;
        }
        return false;
      });
      IEnumerable<\u003C\u003Ef__AnonymousType108<\u003C\u003Ef__AnonymousType107<int?, int?, int?, bool, bool, bool, bool, bool, int?>, Decimal?, Decimal?, Decimal?, Decimal?>> source3 = list2.Where(s =>
      {
        if (!s.Item.FutureShipments)
        {
          Decimal? sumBaseQty = s.SumBaseQty;
          Decimal? sumBaseReceivedQty = s.SumBaseReceivedQty;
          if (sumBaseQty.GetValueOrDefault() > sumBaseReceivedQty.GetValueOrDefault() & sumBaseQty.HasValue & sumBaseReceivedQty.HasValue)
            return s.Item.MarkedForPO;
        }
        return false;
      });
      IEnumerable<\u003C\u003Ef__AnonymousType108<\u003C\u003Ef__AnonymousType107<int?, int?, int?, bool, bool, bool, bool, bool, int?>, Decimal?, Decimal?, Decimal?, Decimal?>> source4 = list2.Where(s =>
      {
        Decimal? sumBaseQty = s.SumBaseQty;
        Decimal num = 0M;
        return sumBaseQty.GetValueOrDefault() > num & sumBaseQty.HasValue && s.Item.FutureShipments;
      });
      IEnumerable<\u003C\u003Ef__AnonymousType108<\u003C\u003Ef__AnonymousType107<int?, int?, int?, bool, bool, bool, bool, bool, int?>, Decimal?, Decimal?, Decimal?, Decimal?>> source5 = list2.Where(s =>
      {
        if (!s.Item.FutureShipments)
        {
          Decimal? sumDeduction = s.SumDeduction;
          Decimal? availableForShipping = s.AvailableForShipping;
          if (sumDeduction.GetValueOrDefault() > availableForShipping.GetValueOrDefault() & sumDeduction.HasValue & availableForShipping.HasValue)
            return !s.Item.NonStock;
        }
        return false;
      });
      if (!source1.Any())
        return Tuple.Create<string, int>("NT", 0);
      int num8 = 0;
      if (source4.Any())
        num8 = list1.Where<SOLineSplit>((Func<SOLineSplit, bool>) (s =>
        {
          DateTime? shipDate2 = s.ShipDate;
          DateTime? nullable = shipDate;
          return shipDate2.HasValue & nullable.HasValue && shipDate2.GetValueOrDefault() > nullable.GetValueOrDefault();
        })).GroupBy<SOLineSplit, int?>((Func<SOLineSplit, int?>) (s => s.LineNbr)).Count<IGrouping<int?, SOLineSplit>>();
      if (source5.Any() || source3.Any() || source2.Any())
      {
        switch (((PXSelectBase<SOOrder>) this.Base.Document).Current.ShipComplete)
        {
          case "C":
            return Tuple.Create<string, int>("NI", num8);
          case "L":
            return Tuple.Create<string, int>("PC", num8);
          case "B":
            return Tuple.Create<string, int>("PB", num8);
        }
      }
      return Tuple.Create<string, int>("AL", num8);

      Decimal? availabilityFetch(int? inventoryID, int? subItemID, int? siteID, int? costCenterID)
      {
        if (!PXTransactionScope.IsScoped)
        {
          using (new PXConnectionScope())
          {
            using (new PXTransactionScope())
            {
              try
              {
                ((PXGraph) this.Base).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial)].PersistInserted();
                ((PXGraph) this.Base).Caches[typeof (SiteLotSerial)].PersistInserted();
                ((PXGraph) this.Base).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter)].PersistInserted();
              }
              catch (PXException ex)
              {
                return new Decimal?(Decimal.MinValue);
              }
            }
          }
        }
        PXCache<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter> pxCache1 = GraphHelper.Caches<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>((PXGraph) this.Base);
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter1;
        using (new ReadOnlyScope(new PXCache[1]
        {
          (PXCache) pxCache1
        }))
        {
          PXCache<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter> pxCache2 = pxCache1;
          PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter2 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter();
          statusByCostCenter2.InventoryID = inventoryID;
          statusByCostCenter2.SubItemID = subItemID;
          statusByCostCenter2.SiteID = siteID;
          statusByCostCenter2.CostCenterID = costCenterID;
          statusByCostCenter1 = pxCache2.Insert(statusByCostCenter2);
        }
        bool? nullable = statusByCostCenter1.NegQty;
        int num;
        if (nullable.GetValueOrDefault())
        {
          nullable = ((PXSelectBase<SOOrderType>) this.Base.soordertype).Current.ShipFullIfNegQtyAllowed;
          num = nullable.GetValueOrDefault() ? 1 : 0;
        }
        else
          num = 0;
        if (num != 0)
        {
          INLotSerClass inLotSerClass = INLotSerClass.PK.Find((PXGraph) this.Base, statusByCostCenter1.LotSerClassID);
          if (inLotSerClass != null && (inLotSerClass.LotSerTrack == "N" || inLotSerClass.LotSerAssign == "U"))
            return new Decimal?(Decimal.MaxValue);
        }
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, inventoryID);
        if (inventoryItem != null)
        {
          nullable = inventoryItem.StkItem;
          if (!nullable.GetValueOrDefault())
          {
            nullable = inventoryItem.KitItem;
            if (nullable.GetValueOrDefault())
              return new Decimal?(Decimal.MaxValue);
          }
        }
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter3 = PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter.PK.Find((PXGraph) this.Base, inventoryID, subItemID, siteID, costCenterID);
        if (statusByCostCenter3 == null)
          return statusByCostCenter1.QtyHardAvail;
        Decimal? qtyHardAvail1 = statusByCostCenter3.QtyHardAvail;
        Decimal? qtyHardAvail2 = statusByCostCenter1.QtyHardAvail;
        return !(qtyHardAvail1.HasValue & qtyHardAvail2.HasValue) ? new Decimal?() : new Decimal?(qtyHardAvail1.GetValueOrDefault() + qtyHardAvail2.GetValueOrDefault());
      }
    }

    private void RecalculateAvailabilityStatus(SOQuickProcessParameters row)
    {
      DateTime? shipDate = ((PXSelectBase) this.QuickProcessParameters).Cache.GetExtension<SOQuickProcessParametersShipDateExt>((object) row).ShipDate;
      Tuple<string, int> tuple = this.OrderAvailabilityStatus(row.SiteID, shipDate);
      ((PXSelectBase) this.QuickProcessParameters).Cache.SetValueExt<SOQuickProcessParametersAvailabilityExt.availabilityStatus>((object) row, (object) tuple.Item1);
      PXCache cache = ((PXSelectBase) this.QuickProcessParameters).Cache;
      SOQuickProcessParameters processParameters = row;
      string str;
      if (tuple.Item2 != 0)
        str = PXLocalizer.LocalizeFormat("This date selection will skip {0} open sales order lines from shipment.", new object[1]
        {
          (object) tuple.Item2
        });
      else
        str = "";
      cache.SetValueExt<SOQuickProcessParametersAvailabilityExt.skipByDateMsg>((object) processParameters, (object) str);
      ((PXSelectBase) this.QuickProcessParameters).Cache.RaiseRowSelected((object) ((PXSelectBase<SOQuickProcessParameters>) this.QuickProcessParameters).Current);
    }

    public static void InitQuickProcessPanel(PXGraph graph, string viewName)
    {
      SOOrderEntry.SOQuickProcess soQuickProcessExt = ((SOOrderEntry) graph).SOQuickProcessExt;
      if (string.IsNullOrEmpty(((PXSelectBase<SOQuickProcessParameters>) soQuickProcessExt.QuickProcessParameters).Current.OrderType))
      {
        ((PXSelectBase) soQuickProcessExt.QuickProcessParameters).Cache.Clear();
        ((PXSelectBase<SOQuickProcessParameters>) soQuickProcessExt.QuickProcessParameters).Insert(PXResultset<SOQuickProcessParameters>.op_Implicit(PXSelectBase<SOQuickProcessParameters, PXSelectReadonly<SOQuickProcessParameters, Where<SOQuickProcessParameters.orderType, Equal<Current<SOOrder.orderType>>>>.Config>.Select((PXGraph) soQuickProcessExt.Base, Array.Empty<object>())));
      }
      if (!((PXSelectBase<SOQuickProcessParameters>) soQuickProcessExt.QuickProcessParameters).Current.CreateShipment.GetValueOrDefault())
        return;
      soQuickProcessExt.EnsureSiteID(((PXSelectBase) soQuickProcessExt.QuickProcessParameters).Cache, ((PXSelectBase<SOQuickProcessParameters>) soQuickProcessExt.QuickProcessParameters).Current);
      DateTime? businessDate = ((PXGraph) soQuickProcessExt.Base).Accessinfo.BusinessDate;
      DateTime? shipDate = ((PXSelectBase<SOOrder>) soQuickProcessExt.Base.Document).Current.ShipDate;
      DateTime? nullable = (businessDate.HasValue & shipDate.HasValue ? (businessDate.GetValueOrDefault() > shipDate.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? ((PXGraph) soQuickProcessExt.Base).Accessinfo.BusinessDate : ((PXSelectBase<SOOrder>) soQuickProcessExt.Base.Document).Current.ShipDate;
      SOQuickProcessParametersShipDateExt.SetDate(((PXSelectBase) soQuickProcessExt.QuickProcessParameters).Cache, ((PXSelectBase<SOQuickProcessParameters>) soQuickProcessExt.QuickProcessParameters).Current, nullable.Value);
      soQuickProcessExt.RecalculateAvailabilityStatus(((PXSelectBase<SOQuickProcessParameters>) soQuickProcessExt.QuickProcessParameters).Current);
    }

    [PXLocalizable]
    public static class Msg
    {
      public const string DoNotEmail = "Invoice will be emailed during quick processing though the {0} customer does not require sending invoices by email.";
      public const string DoEmail = "Invoice emailing will be skipped during quick processing though the {0} customer requires sending invoices by email.";
      public const string DoNotPrint = "Invoice will be printed during quick processing though the {0} customer does not require printing invoices.";
      public const string DoPrint = "Invoice printing will be skipped during quick processing though the {0} customer requires printing invoices.";
      public const string CannotShip = "The order cannot be shipped. See availability section for more info.";
      public const string OnlyCurrentShipmentWillBeInvoiced = "Only the shipment created by this process will be invoiced.";
      public const string SomeLinesWillBeSkipedDueToDateSelection = "This date selection will skip {0} open sales order lines from shipment.";
    }
  }

  public class CarrierRates : CarrierRatesExtension<SOOrderEntry, SOOrder>
  {
    public virtual void RecalculatePackagesForOrder(SOOrder order)
    {
      this.RecalculatePackagesForOrder(((PXSelectBase) this.Documents).Cache.GetExtension<PX.Objects.SO.GraphExtensions.CarrierRates.Document>((object) order));
    }

    public virtual CarrierRequest BuildRateRequest(SOOrder order)
    {
      return this.BuildRateRequest(((PXSelectBase) this.Documents).Cache.GetExtension<PX.Objects.SO.GraphExtensions.CarrierRates.Document>((object) order));
    }

    public virtual CarrierRequest BuildQuoteRequest(SOOrder order, CarrierPlugin plugin)
    {
      return this.BuildQuoteRequest(((PXSelectBase) this.Documents).Cache.GetExtension<PX.Objects.SO.GraphExtensions.CarrierRates.Document>((object) order), plugin);
    }

    protected override CarrierRatesExtension<SOOrderEntry, SOOrder>.DocumentMapping GetDocumentMapping()
    {
      return new CarrierRatesExtension<SOOrderEntry, SOOrder>.DocumentMapping(typeof (SOOrder))
      {
        DocumentDate = typeof (SOOrder.orderDate)
      };
    }

    protected override CarrierRatesExtension<SOOrderEntry, SOOrder>.DocumentPackageMapping GetDocumentPackageMapping()
    {
      return new CarrierRatesExtension<SOOrderEntry, SOOrder>.DocumentPackageMapping(typeof (SOPackageInfoEx));
    }

    protected override void CalculateFreightCost(PX.Objects.SO.GraphExtensions.CarrierRates.Document doc)
    {
      this.Base.CalculateFreightCost(true);
    }

    protected override CarrierRequest GetCarrierRequest(
      PX.Objects.SO.GraphExtensions.CarrierRates.Document doc,
      UnitsType unit,
      List<string> methods,
      List<CarrierBoxEx> boxes)
    {
      SOOrder main = (SOOrder) ((PXSelectBase) this.Documents).Cache.GetMain<PX.Objects.SO.GraphExtensions.CarrierRates.Document>(doc);
      SOShippingAddress soShippingAddress = PXResultset<SOShippingAddress>.op_Implicit(((PXSelectBase<SOShippingAddress>) this.Base.Shipping_Address).Select(Array.Empty<object>()));
      PX.Objects.CR.BAccount baccount = (PX.Objects.CR.BAccount) PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXSelectJoin<BAccountR, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.bAccountID>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((PXGraph) this.Base).Accessinfo.BranchID
      }));
      PX.Objects.CR.Address address = PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) baccount.DefAddressID
      }));
      SOShippingContact soShippingContact = PXResultset<SOShippingContact>.op_Implicit(((PXSelectBase<SOShippingContact>) this.Base.Shipping_Contact).Select(Array.Empty<object>()));
      PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) baccount.DefContactID
      }));
      CarrierRequest carrierRequest = new CarrierRequest(unit, main.CuryID);
      carrierRequest.Shipper = (IAddressBase) address;
      carrierRequest.Origin = (IAddressBase) null;
      carrierRequest.Destination = (IAddressBase) soShippingAddress;
      carrierRequest.PackagesEx = (IList<CarrierBoxEx>) boxes;
      carrierRequest.Resedential = main.Resedential.GetValueOrDefault();
      carrierRequest.SaturdayDelivery = main.SaturdayDelivery.GetValueOrDefault();
      carrierRequest.Insurance = main.Insurance.GetValueOrDefault();
      carrierRequest.ShipDate = Tools.Max<DateTime>(((PXGraph) this.Base).Accessinfo.BusinessDate.Value.Date, main.ShipDate.Value);
      carrierRequest.Methods = (IList<string>) methods;
      carrierRequest.Attributes = (IList<string>) new List<string>();
      carrierRequest.InvoiceLineTotal = ((PXSelectBase<SOOrder>) this.Base.Document).Current.CuryLineTotal.GetValueOrDefault();
      carrierRequest.ShipperContact = (IContactBase) contact;
      carrierRequest.DestinationContact = (IContactBase) soShippingContact;
      if (main.GroundCollect.GetValueOrDefault() && this.Base.CanUseGroundCollect(main))
        carrierRequest.Attributes.Add("COLLECT");
      return carrierRequest;
    }

    protected override IList<SOPackageEngine.PackSet> GetPackages(PX.Objects.SO.GraphExtensions.CarrierRates.Document doc, bool suppressRecalc)
    {
      SOOrder main = (SOOrder) ((PXSelectBase) this.Documents).Cache.GetMain<PX.Objects.SO.GraphExtensions.CarrierRates.Document>(doc);
      return ((main.IsPackageValid.GetValueOrDefault() ? 1 : (main.IsManualPackage.GetValueOrDefault() ? 1 : 0)) | (suppressRecalc ? 1 : 0)) == 0 ? this.CalculatePackages(doc, (string) null) : this.GetPackages(main);
    }

    protected virtual IList<SOPackageEngine.PackSet> GetPackages(SOOrder order)
    {
      Dictionary<int, SOPackageEngine.PackSet> dictionary1 = new Dictionary<int, SOPackageEngine.PackSet>();
      PXView view = ((PXSelectBase) this.Base.Packages).View;
      object[] objArray1 = new object[1]{ (object) order };
      object[] objArray2 = Array.Empty<object>();
      foreach (SOPackageInfoEx soPackageInfoEx in view.SelectMultiBound(objArray1, objArray2))
      {
        Dictionary<int, SOPackageEngine.PackSet> dictionary2 = dictionary1;
        int? siteId = soPackageInfoEx.SiteID;
        int key1 = siteId.Value;
        SOPackageEngine.PackSet packSet;
        if (!dictionary2.ContainsKey(key1))
        {
          siteId = soPackageInfoEx.SiteID;
          packSet = new SOPackageEngine.PackSet(siteId.Value);
          dictionary1.Add(packSet.SiteID, packSet);
        }
        else
        {
          Dictionary<int, SOPackageEngine.PackSet> dictionary3 = dictionary1;
          siteId = soPackageInfoEx.SiteID;
          int key2 = siteId.Value;
          packSet = dictionary3[key2];
        }
        packSet.Packages.Add(soPackageInfoEx);
      }
      return (IList<SOPackageEngine.PackSet>) dictionary1.Values.ToList<SOPackageEngine.PackSet>();
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<SOPackageInfoEx, SOPackageInfoEx.boxID> e)
    {
      if (e.Row == null)
        return;
      ((PXSelectBase) this.Base.Packages).Cache.SetDefaultExt<SOPackageInfoEx.description>((object) e.Row);
      ((PXSelectBase) this.Base.Packages).Cache.SetDefaultExt<SOPackageInfo.length>((object) e.Row);
      ((PXSelectBase) this.Base.Packages).Cache.SetDefaultExt<SOPackageInfo.width>((object) e.Row);
      ((PXSelectBase) this.Base.Packages).Cache.SetDefaultExt<SOPackageInfo.height>((object) e.Row);
      ((PXSelectBase) this.Base.Packages).Cache.SetDefaultExt<SOPackageInfoEx.boxWeight>((object) e.Row);
      ((PXSelectBase) this.Base.Packages).Cache.SetDefaultExt<SOPackageInfoEx.maxWeight>((object) e.Row);
    }

    protected override void ValidatePackages()
    {
      if (!((PXSelectBase<SOOrder>) this.Base.Document).Current.IsManualPackage.GetValueOrDefault())
        return;
      PXResultset<SOPackageInfoEx> pxResultset = ((PXSelectBase<SOPackageInfoEx>) this.Base.Packages).Select(Array.Empty<object>());
      if (pxResultset.Count == 0)
        throw new PXException("When using 'Manual Packaging' option at least one package must be defined before a Rate Quote can be requested from the Carriers.");
      bool flag = false;
      foreach (PXResult<SOPackageInfoEx> pxResult in pxResultset)
      {
        SOPackageInfoEx soPackageInfoEx = PXResult<SOPackageInfoEx>.op_Implicit(pxResult);
        if (!soPackageInfoEx.SiteID.HasValue)
        {
          ((PXSelectBase) this.Base.Packages).Cache.RaiseExceptionHandling<SOPackageInfoEx.siteID>((object) soPackageInfoEx, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
          {
            (object) "[siteID]"
          }));
          flag = true;
        }
      }
      if (flag)
        throw new PXException("Warehouse must be defined for all packages before a Rate Quote can be requested from the Carriers.");
    }

    protected override void RateHasBeenSelected(SOCarrierRate cr)
    {
      if (!this.Base.CollectFreight)
        return;
      this.Base.SetFreightCost(this.Base.ConvertAmtToBaseCury(((PXSelectBase<SOOrder>) this.Base.Document).Current.CuryID, ((PXSelectBase<ARSetup>) this.arsetup).Current.DefaultRateTypeID, ((PXSelectBase<SOOrder>) this.Base.Document).Current.OrderDate.Value, cr.Amount.Value));
    }

    protected override WebDialogResult AskForRateSelection()
    {
      return ((PXSelectBase<SOOrder>) this.Base.DocumentProperties).AskExt();
    }

    protected override void ClearPackages(PX.Objects.SO.GraphExtensions.CarrierRates.Document doc)
    {
      foreach (SOPackageInfoEx soPackageInfoEx in ((PXSelectBase) this.Base.Packages).View.SelectMultiBound(new object[1]
      {
        ((PXSelectBase) this.Documents).Cache.GetMain<PX.Objects.SO.GraphExtensions.CarrierRates.Document>(doc)
      }, Array.Empty<object>()))
        ((PXSelectBase<SOPackageInfoEx>) this.Base.Packages).Delete(soPackageInfoEx);
    }

    protected override void InsertPackages(IEnumerable<SOPackageInfoEx> packages)
    {
      foreach (SOPackageInfoEx package in packages)
        ((PXSelectBase<SOPackageInfoEx>) this.Base.Packages).Insert(package);
    }

    protected override IEnumerable<Tuple<CarrierRatesExtension<SOOrderEntry, SOOrder>.ILineInfo, PX.Objects.IN.InventoryItem>> GetLines(
      PX.Objects.SO.GraphExtensions.CarrierRates.Document doc)
    {
      SOOrder main = (SOOrder) ((PXSelectBase) this.Documents).Cache.GetMain<PX.Objects.SO.GraphExtensions.CarrierRates.Document>(doc);
      return ((IEnumerable<PXResult<SOLine>>) PXSelectBase<SOLine, PXSelectJoin<SOLine, InnerJoin<PX.Objects.IN.InventoryItem, On<SOLine.FK.InventoryItem>>, Where<SOLine.orderType, Equal<Required<SOOrder.orderType>>, And<SOLine.orderNbr, Equal<Required<SOOrder.orderNbr>>>>, OrderBy<Asc<SOLine.orderType, Asc<SOLine.orderNbr, Asc<SOLine.lineNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) main.OrderType,
        (object) main.OrderNbr
      })).AsEnumerable<PXResult<SOLine>>().Cast<PXResult<SOLine, PX.Objects.IN.InventoryItem>>().Select<PXResult<SOLine, PX.Objects.IN.InventoryItem>, Tuple<CarrierRatesExtension<SOOrderEntry, SOOrder>.ILineInfo, PX.Objects.IN.InventoryItem>>((Func<PXResult<SOLine, PX.Objects.IN.InventoryItem>, Tuple<CarrierRatesExtension<SOOrderEntry, SOOrder>.ILineInfo, PX.Objects.IN.InventoryItem>>) (r => Tuple.Create<CarrierRatesExtension<SOOrderEntry, SOOrder>.ILineInfo, PX.Objects.IN.InventoryItem>((CarrierRatesExtension<SOOrderEntry, SOOrder>.ILineInfo) new SOOrderEntry.CarrierRates.LineInfo(PXResult<SOLine, PX.Objects.IN.InventoryItem>.op_Implicit(r)), PXResult<SOLine, PX.Objects.IN.InventoryItem>.op_Implicit(r))));
    }

    protected override IEnumerable<CarrierPlugin> GetApplicableCarrierPlugins()
    {
      Lazy<SOOrderSite[]> orderSites = new Lazy<SOOrderSite[]>((Func<SOOrderSite[]>) (() => GraphHelper.RowCast<SOOrderSite>((IEnumerable) ((PXSelectBase<SOOrderSite>) this.Base.SiteList).Select(Array.Empty<object>())).ToArray<SOOrderSite>()));
      return base.GetApplicableCarrierPlugins().Where<CarrierPlugin>((Func<CarrierPlugin, bool>) (p => !p.SiteID.HasValue || ((IEnumerable<SOOrderSite>) orderSites.Value).Any<SOOrderSite>((Func<SOOrderSite, bool>) (s =>
      {
        int? siteId1 = s.SiteID;
        int? siteId2 = p.SiteID;
        return siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue;
      }))));
    }

    private class LineInfo : CarrierRatesExtension<SOOrderEntry, SOOrder>.ILineInfo
    {
      private SOLine _line;

      public LineInfo(SOLine line) => this._line = line;

      public Decimal? BaseQty => this._line.BaseQty;

      public Decimal? CuryLineAmt => this._line.CuryLineAmt;

      public Decimal? ExtWeight => this._line.ExtWeight;

      public int? SiteID => this._line.SiteID;

      public string Operation => this._line.Operation;
    }
  }

  /// <exclude />
  public class SOOrderEntryAddressLookupExtension : 
    AddressLookupExtension<SOOrderEntry, SOOrder, SOBillingAddress>
  {
    protected override string AddressView => "Billing_Address";
  }

  /// <exclude />
  public class SOOrderEntryShippingAddressLookupExtension : 
    AddressLookupExtension<SOOrderEntry, SOOrder, SOShippingAddress>
  {
    protected override string AddressView => "Shipping_Address";
  }

  public class SOOrderEntryBillingAddressCachingHelper : 
    AddressValidationExtension<SOOrderEntry, SOBillingAddress>
  {
    protected override IEnumerable<PXSelectBase<SOBillingAddress>> AddressSelects()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      SOOrderEntry.SOOrderEntryBillingAddressCachingHelper addressCachingHelper = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (PXSelectBase<SOBillingAddress>) addressCachingHelper.Base.Billing_Address;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }
  }

  public class SOOrderEntryShippingAddressCachingHelper : 
    AddressValidationExtension<SOOrderEntry, SOShippingAddress>
  {
    protected override IEnumerable<PXSelectBase<SOShippingAddress>> AddressSelects()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      SOOrderEntry.SOOrderEntryShippingAddressCachingHelper addressCachingHelper = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (PXSelectBase<SOShippingAddress>) addressCachingHelper.Base.Shipping_Address;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }
  }

  /// <exclude />
  public class ExtensionSort : 
    SortExtensionsBy<TypeArrayOf<PXGraphExtension<SOOrderEntry>>.FilledWith<SOLineSplitPlan, Blanket>>
  {
  }
}
