// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Export;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.DependencyInjection;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.LicensePolicy;
using PX.Objects.AP;
using PX.Objects.AP.MigrationMode;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Exceptions;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.Extensions.CostAccrual;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.IN.InventoryRelease;
using PX.Objects.IN.Services;
using PX.Objects.PM;
using PX.Objects.PO.GraphExtensions.POReceiptEntryExt;
using PX.Objects.PO.LandedCosts;
using PX.Objects.PO.Scopes;
using PX.Objects.PO.WMS;
using PX.Objects.SO;
using PX.Objects.SO.Models;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable enable
namespace PX.Objects.PO;

public class POReceiptEntry : PXGraph<
#nullable disable
POReceiptEntry, POReceipt>, IGraphWithInitialization
{
  [PXViewName("Purchase Receipt")]
  public PXSelectJoin<POReceipt, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<POReceipt.vendorID>>>, Where<POReceipt.receiptType, Equal<Optional<POReceipt.receiptType>>, And<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>> Document;
  public PXSelect<POReceipt, Where<POReceipt.receiptType, Equal<Current<POReceipt.receiptType>>, And<POReceipt.receiptNbr, Equal<Current<POReceipt.receiptNbr>>>>> CurrentDocument;
  [PXViewName("Purchase Receipt Line")]
  public PXOrderedSelect<POReceipt, POReceiptLine, Where<POReceiptLine.receiptType, Equal<Current<POReceipt.receiptType>>, And<POReceiptLine.receiptNbr, Equal<Current<POReceipt.receiptNbr>>>>, OrderBy<Asc<POReceiptLine.receiptType, Asc<POReceiptLine.receiptNbr, Asc<POReceiptLine.sortOrder, Asc<POReceiptLine.lineNbr>>>>>> transactions;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (POReceiptLine.receivedToDateQty)})]
  public PXSelectJoin<POReceiptLine, LeftJoin<POLine, On<POLine.orderType, Equal<POReceiptLine.pOType>, And<POLine.orderNbr, Equal<POReceiptLine.pONbr>, And<POLine.lineNbr, Equal<POReceiptLine.pOLineNbr>>>>>, Where<POReceiptLine.receiptType, Equal<Current<POReceipt.receiptType>>, And<POReceiptLine.receiptNbr, Equal<Current<POReceipt.receiptNbr>>>>, OrderBy<Asc<POReceiptLine.receiptType, Asc<POReceiptLine.receiptNbr, Asc<POReceiptLine.lineNbr>>>>> transactionsPOLine;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<POReceiptLineSplit.receiptType>.IsRelatedTo<POReceiptLine.receiptType>, Field<POReceiptLineSplit.receiptNbr>.IsRelatedTo<POReceiptLine.receiptNbr>, Field<POReceiptLineSplit.lineNbr>.IsRelatedTo<POReceiptLine.lineNbr>>.WithTablesOf<POReceiptLine, POReceiptLineSplit>, POReceiptLine, POReceiptLineSplit>.SameAsCurrent.And<BqlChainableConditionLite<SetOfConstants<string, Equal<IBqlOperand>, SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<POLineType.goodsForInventory, POLineType.goodsForDropShip, POLineType.goodsForSalesOrder, POLineType.goodsForServiceOrder, POLineType.goodsForReplenishment, POLineType.goodsForManufacturing, POLineType.goodsForProject>>.Provider>.Contains<BqlField<
  #nullable enable
  POReceiptLine.lineType, IBqlString>.FromCurrent>>.Or<
  #nullable disable
  BqlChainableConditionLite<SetOfConstants<string, Equal<IBqlOperand>, SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<POLineType.nonStock, POLineType.nonStockForDropShip, POLineType.nonStockForSalesOrder, POLineType.nonStockForServiceOrder, POLineType.service, POLineType.nonStockForManufacturing, POLineType.nonStockForProject>>.Provider>.Contains<BqlField<
  #nullable enable
  POReceiptLine.lineType, IBqlString>.FromCurrent>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  POLineType.service, IBqlString>.IsNotEqual<
  #nullable disable
  BqlField<
  #nullable enable
  POReceiptLine.lineType, IBqlString>.FromCurrent>>>>>, 
  #nullable disable
  POReceiptLineSplit>.View splits;
  public PXSetup<POSetup> posetup;
  public PXSetup<APSetup> apsetup;
  public PXSetupOptional<INSetup> insetup;
  public PXSetupOptional<CommonSetup> commonsetup;
  public PXSetup<Company> company;
  public PXSetup<PX.Objects.GL.Branch>.Where<BqlOperand<
  #nullable enable
  PX.Objects.GL.Branch.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  POReceipt.vendorID, IBqlInt>.AsOptional>> branch;
  public 
  #nullable disable
  PXSetupOptional<POReceivePutAwaySetup, Where<BqlOperand<
  #nullable enable
  POReceivePutAwaySetup.branchID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AccessInfo.branchID, IBqlInt>.FromCurrent>>> rpaSetup;
  public 
  #nullable disable
  PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<POReceipt.curyInfoID>>>> Current_currencyinfo;
  [PXViewName("Vendor")]
  public PXSetup<PX.Objects.AP.Vendor>.Where<BqlOperand<
  #nullable enable
  PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  POReceipt.vendorID, IBqlInt>.AsOptional>> vendor;
  [PXViewName("Vendor Class")]
  public 
  #nullable disable
  PXSetup<VendorClass>.Where<BqlOperand<
  #nullable enable
  VendorClass.vendorClassID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.AP.Vendor.vendorClassID, IBqlString>.FromCurrent>> vendorclass;
  [PXViewName("Vendor Location")]
  public 
  #nullable disable
  PXSetup<PX.Objects.CR.Location>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.CR.Location.bAccountID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  POReceipt.vendorID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PX.Objects.CR.Location.locationID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  POReceipt.vendorLocationID, IBqlInt>.AsOptional>>> location;
  [PXCopyPasteHiddenView]
  public 
  #nullable disable
  PXSelect<POOrderReceipt, Where<POOrderReceipt.receiptType, Equal<Current<POReceipt.receiptType>>, And<POOrderReceipt.receiptNbr, Equal<Current<POReceipt.receiptNbr>>>>> ReceiptOrders;
  [PXCopyPasteHiddenView]
  public PXSelect<POOrderReceiptLink, Where<POOrderReceiptLink.receiptType, Equal<Current<POReceipt.receiptType>>, And<POOrderReceiptLink.receiptNbr, Equal<Current<POReceipt.receiptNbr>>>>> ReceiptOrdersLink;
  [PXCopyPasteHiddenView]
  public PXSelectReadonly<PX.Objects.IN.INRegister, Where<PX.Objects.IN.INRegister.docType, Equal<INDocType.transfer>, And<PX.Objects.IN.INRegister.transferType, Equal<INTransferType.oneStep>, And<PX.Objects.IN.INRegister.pOReceiptType, Equal<Current<POReceipt.receiptType>>, And<PX.Objects.IN.INRegister.pOReceiptNbr, Equal<Current<POReceipt.receiptNbr>>>>>>, OrderBy<Desc<PX.Objects.IN.INRegister.createdDateTime>>> RelatedTransfers;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<POReceiptToShipmentLink, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<
  #nullable enable
  POReceiptToShipmentLink.receiptType>.IsRelatedTo<
  #nullable disable
  POReceipt.receiptType>, Field<
  #nullable enable
  POReceiptToShipmentLink.receiptNbr>.IsRelatedTo<
  #nullable disable
  POReceipt.receiptNbr>>.WithTablesOf<POReceipt, 
  #nullable enable
  POReceiptToShipmentLink>, 
  #nullable disable
  POReceipt, 
  #nullable enable
  POReceiptToShipmentLink>.SameAsCurrent>, 
  #nullable disable
  POReceiptToShipmentLink>.View RelatedShipments;
  public PXSelect<POOrder, Where<POOrder.orderType, NotEqual<POOrderType.regularSubcontract>>> poOrderUPD;
  public PXSelect<POLine> poline;
  public PXSelect<POReceiptLineReturnUpdate> poReceiptReturnUpdate;
  public PXSelect<POItemCostManager.POVendorInventoryPriceUpdate> priceStatus;
  public PXSelect<INItemXRef> xrefs;
  public PXSelect<POReceiptLandedCostDetail, Where<POReceiptLandedCostDetail.pOReceiptType, Equal<Current<POReceipt.receiptType>>, And<POReceiptLandedCostDetail.pOReceiptNbr, Equal<Current<POReceipt.receiptNbr>>>>, OrderBy<Desc<POReceiptLandedCostDetail.docDate, Desc<POReceiptLandedCostDetail.lCRefNbr>>>> landedCosts;
  public PXSelect<POReceiptAPDoc> apDocs;
  public PXSelect<POReceiptPOOriginal> receiptHistory;
  public PXFilter<POReceiptEntry.POOrderFilter> filter;
  public PXFilter<POReceiptEntry.POReceiptLineS> addReceipt;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<POReceiptEntry.POLineS, InnerJoin<POOrder, On<POOrder.orderType, Equal<POReceiptEntry.POLineS.orderType>, And<POOrder.orderNbr, Equal<POReceiptEntry.POLineS.orderNbr>>>>, Where<POReceiptEntry.POLineS.orderType, Equal<Current<POReceiptEntry.POOrderFilter.orderType>>, And<POReceiptEntry.POLineS.lineType, NotEqual<POLineType.description>, And2<Where<POReceiptEntry.POLineS.orderType, NotEqual<POOrderType.projectDropShip>, Or<POLine.dropshipReceiptProcessing, Equal<DropshipReceiptProcessingOption.generateReceipt>>>, And2<Where<Current<POReceiptEntry.POOrderFilter.vendorID>, IsNull, Or<POReceiptEntry.POLineS.vendorID, Equal<Current<POReceiptEntry.POOrderFilter.vendorID>>>>, And2<Where<Current<POReceiptEntry.POOrderFilter.vendorLocationID>, IsNull, Or<POOrder.vendorLocationID, Equal<Current<POReceiptEntry.POOrderFilter.vendorLocationID>>>>, And2<Where<Current<POReceiptEntry.POOrderFilter.orderNbr>, IsNull, Or<POReceiptEntry.POLineS.orderNbr, Equal<Current<POReceiptEntry.POOrderFilter.orderNbr>>>>, And2<Where<Current<POReceiptEntry.POOrderFilter.inventoryID>, IsNull, Or<POReceiptEntry.POLineS.inventoryID, Equal<Current<POReceiptEntry.POOrderFilter.inventoryID>>>>, And2<Where<Current<POReceiptEntry.POOrderFilter.subItemID>, IsNull, Or<POReceiptEntry.POLineS.subItemID, Equal<Current<POReceiptEntry.POOrderFilter.subItemID>>, Or<Not<FeatureInstalled<FeaturesSet.subItem>>>>>, And<POOrder.status, Equal<POOrderStatus.open>, And<POReceiptEntry.POLineS.completed, Equal<boolFalse>, And<POReceiptEntry.POLineS.cancelled, Equal<boolFalse>, And<POOrder.curyID, Equal<Current<POReceipt.curyID>>>>>>>>>>>>>>, OrderBy<Asc<POLine.sortOrder>>> poLinesSelection;
  [PXReadOnlyView]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<INTran, InnerJoin<PX.Objects.IN.INRegister, On<INTran.FK.INRegister>, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INRegister.FK.ToSite>, InnerJoin<INTranInTransit, On<INTranType.transfer, Equal<INTran.tranType>, And<INTranInTransit.refNbr, Equal<INTran.refNbr>, And<INTranInTransit.lineNbr, Equal<INTran.lineNbr>>>>, LeftJoin<INTran2, On<INTran2.released, NotEqual<True>, And<INTran2.origLineNbr, Equal<INTranInTransit.lineNbr>, And<INTran2.origRefNbr, Equal<INTranInTransit.refNbr>, And<INTran2.origTranType, Equal<INTranType.transfer>>>>>, LeftJoin<POReceiptLine, On<POReceiptLine.released, NotEqual<True>, And<POReceiptLine.origLineNbr, Equal<INTranInTransit.lineNbr>, And<POReceiptLine.origRefNbr, Equal<INTranInTransit.refNbr>, And<POReceiptLine.origTranType, Equal<INTranType.transfer>>>>>>>>>>, Where<PX.Objects.IN.INRegister.origModule, Equal<BatchModule.moduleSO>, And<POReceiptLine.receiptNbr, IsNull, And<INTran2.refNbr, IsNull, And<PX.Objects.IN.INRegister.docType, Equal<INDocType.transfer>, And<PX.Objects.IN.INRegister.released, Equal<True>, And<Current<POReceipt.receiptType>, Equal<POReceiptType.transferreceipt>, And<PX.Objects.IN.INRegister.toSiteID, Equal<Current<POReceipt.siteID>>, And2<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>, And2<Where<Current<POReceiptEntry.POOrderFilter.shipFromSiteID>, IsNull, Or<PX.Objects.IN.INRegister.siteID, Equal<Current<POReceiptEntry.POOrderFilter.shipFromSiteID>>>>, And2<Where<Current<POReceiptEntry.POOrderFilter.sOOrderNbr>, IsNull, Or<INTran.sOOrderType, Equal<Current<POReceiptEntry.POOrderFilter.sOOrderType>>, And<INTran.sOOrderNbr, Equal<Current<POReceiptEntry.POOrderFilter.sOOrderNbr>>>>>, And2<Where<Current<POReceiptEntry.POOrderFilter.inventoryID>, IsNull, Or<INTran.inventoryID, Equal<Current<POReceiptEntry.POOrderFilter.inventoryID>>>>, And<Where<Current<POReceiptEntry.POOrderFilter.subItemID>, IsNull, Or<INTran.subItemID, Equal<Current<POReceiptEntry.POOrderFilter.subItemID>>>>>>>>>>>>>>>>> intranSelection;
  [PXCopyPasteHiddenView]
  public PXSelect<POReceiptEntry.POOrderS, Where<POReceiptEntry.POOrderS.hold, Equal<boolFalse>, And<POOrder.status, NotEqual<POOrderStatus.awaitingLink>, And<POOrder.orderType, NotEqual<POOrderType.regularSubcontract>, And2<Where<Current<POReceipt.vendorID>, IsNull, Or<POOrder.vendorID, Equal<Current<POReceipt.vendorID>>>>, And2<Where<Current<POReceipt.vendorLocationID>, IsNull, Or<POOrder.vendorLocationID, Equal<Current<POReceipt.vendorLocationID>>>>, And2<Where<Current<POReceiptEntry.POOrderFilter.vendorID>, IsNull, Or<POOrder.vendorID, Equal<Current<POReceiptEntry.POOrderFilter.vendorID>>>>, And2<Where<Current<POReceiptEntry.POOrderFilter.vendorLocationID>, IsNull, Or<POOrder.vendorLocationID, Equal<Current<POReceiptEntry.POOrderFilter.vendorLocationID>>>>, And2<Where2<Where<Current<POReceiptEntry.POOrderFilter.orderType>, IsNull, And<Where<POReceiptEntry.POOrderS.orderType, Equal<POOrderType.regularOrder>, Or<POReceiptEntry.POOrderS.orderType, Equal<POOrderType.dropShip>>>>>, Or<POReceiptEntry.POOrderS.orderType, Equal<Current<POReceiptEntry.POOrderFilter.orderType>>>>, And2<Where<POReceiptEntry.POOrderS.orderType, NotEqual<POOrderType.projectDropShip>, Or<POOrder.dropshipReceiptProcessing, Equal<DropshipReceiptProcessingOption.generateReceipt>>>, And2<Where<POOrder.shipToBAccountID, Equal<Current<POReceiptEntry.POOrderFilter.shipToBAccountID>>, Or<Current<POReceiptEntry.POOrderFilter.shipToBAccountID>, IsNull>>, And2<Where<POOrder.shipToLocationID, Equal<Current<POReceiptEntry.POOrderFilter.shipToLocationID>>, Or<Current<POReceiptEntry.POOrderFilter.shipToLocationID>, IsNull>>, And<POReceiptEntry.POOrderS.cancelled, Equal<boolFalse>, And<POOrder.status, Equal<POOrderStatus.open>, And<POOrder.curyID, Equal<Current<POReceipt.curyID>>>>>>>>>>>>>>>>, OrderBy<Asc<POOrder.orderDate>>> openOrders;
  public PXFilter<POReceiptReturnFilter> returnFilter;
  [PXCopyPasteHiddenView]
  public PXSelectOrderBy<POReceiptReturn, OrderBy<Desc<POReceiptReturn.receiptNbr>>> poReceiptReturn;
  [PXCopyPasteHiddenView]
  public PXSelectOrderBy<POReceiptLineReturn, OrderBy<Desc<POReceiptLineReturn.receiptNbr, Asc<POReceiptLineReturn.lineNbr>>>> poReceiptLineReturn;
  public PXInitializeState<POReceipt> initializeState;
  public PXAction<POReceipt> putOnHold;
  public PXAction<POReceipt> releaseFromHold;
  public PXAction<POReceipt> Cancel;
  public PXAction<POReceipt> createReturn;
  public PXAction<POReceipt> release;
  [PXViewName("Main Contact")]
  public PXSelect<PX.Objects.CR.Contact> DefaultCompanyContact;
  [PXViewName("Vendor Contact")]
  public PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.AP.Vendor.defContactID>>>, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<POReceipt.vendorID>>>> contact;
  public PXAction<POReceipt> notification;
  public PXAction<POReceipt> emailPurchaseReceipt;
  public PXAction<POReceipt> report;
  public PXAction<POReceipt> printPurchaseReceipt;
  public PXAction<POReceipt> printBillingDetail;
  public PXAction<POReceipt> printAllocated;
  public PXAction<POReceipt> assign;
  public PXAction<POReceipt> setAddLineFilterToSource;
  public PXAction<POReceipt> addPOOrder;
  public PXAction<POReceipt> addPOOrder2;
  public PXAction<POReceipt> addINTran;
  public PXAction<POReceipt> addINTran2;
  public PXAction<POReceipt> addPOOrderLine;
  public PXAction<POReceipt> addPOOrderLine2;
  public PXAction<POReceipt> addPOReceiptLine;
  public PXAction<POReceipt> addPOReceiptLine2;
  public PXAction<POReceipt> addPOReceiptReturn;
  public PXAction<POReceipt> addPOReceiptReturn2;
  public PXAction<POReceipt> addPOReceiptLineReturn;
  public PXAction<POReceipt> addPOReceiptLineReturn2;
  public PXAction<POReceipt> viewPOOrder;
  public PXAction<POReceipt> createAPDocument;
  public PXAction<POReceipt> createLCDocument;
  public PXWorkflowEventHandler<POReceipt> OnInventoryReceiptCreatedFromPOReceipt;
  public PXWorkflowEventHandler<POReceipt> OnInventoryIssueCreatedFromPOReturn;
  public PXWorkflowEventHandler<POReceipt> OnInventoryReceiptCreatedFromPOTransfer;
  public PXWorkflowEventHandler<POReceipt> OnConfirmReceipt;
  public PXWorkflowEventHandler<POReceipt> OnCorrectionReceiptCreated;
  public PXWorkflowEventHandler<POReceipt> OnCorrectionReceiptDeleted;
  public PXWorkflowEventHandler<POReceipt> OnCorrectionReceiptReleased;
  public PXWorkflowEventHandler<POReceipt> OnReceiptCancelled;
  private bool isDeleting;
  public POReceiptEntry.PopupHandler addLinePopupHandler;
  private bool inventoryIDChanging;
  private bool _skipUIUpdate;
  private bool _forceAccrualAcctDefaulting;

  public virtual POReceiptEntry.MultiCurrency MultiCurrencyExt
  {
    get => ((PXGraph) this).FindImplementation<POReceiptEntry.MultiCurrency>();
  }

  public ReleaseContext ReleaseContextExt => ((PXGraph) this).FindImplementation<ReleaseContext>();

  [InjectDependency]
  public IInventoryAccountService InventoryAccountService { get; set; }

  [PXCustomizeBaseAttribute(typeof (AccountAttribute), "Visible", false)]
  protected virtual void POReceiptLine_POAccrualAcctID_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (SubAccountAttribute), "Visible", false)]
  protected virtual void POReceiptLine_POAccrualSubID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (POReceiptLine.receiptQty))]
  protected virtual void _(PX.Data.Events.CacheAttached<POReceiptLine.unbilledQty> e)
  {
  }

  [PXMergeAttributes]
  [PXParent(typeof (POReceiptLine.FK.OrderLine))]
  protected virtual void _(PX.Data.Events.CacheAttached<POReceiptLine.pOLineNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUnboundFormula(typeof (BqlFunction<Sub<POReceiptLine.baseMultReceiptQty, BqlOperand<POReceiptLine.baseOrigQty, IBqlDecimal>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLine.receiptType, Equal<POReceiptType.poreceipt>>>>>.And<BqlOperand<POReceiptLine.baseOrigQty, IBqlDecimal>.IsNotNull>>.Else<decimal0>>, IBqlDecimal>.Subtract<BqlOperand<POReceiptLine.baseMultReceiptQty, IBqlDecimal>.When<BqlOperand<POReceiptLine.canceledWithoutCorrection, IBqlBool>.IsEqual<True>>.Else<decimal0>>), typeof (SumCalc<POLine.baseReceivedQty>), ValidateAggregateCalculation = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<POReceiptLine.baseMultReceiptQty> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDBDefault(typeof (POReceipt.receiptType))]
  protected virtual void _(PX.Data.Events.CacheAttached<POReceiptLine.receiptType> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDBDefault(typeof (POReceipt.receiptNbr))]
  protected virtual void _(PX.Data.Events.CacheAttached<POReceiptLine.receiptNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDBDefault(typeof (POReceipt.vendorID))]
  protected virtual void _(PX.Data.Events.CacheAttached<POReceiptLine.vendorID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDBDefault(typeof (POReceipt.receiptDate))]
  protected virtual void _(PX.Data.Events.CacheAttached<POReceiptLine.receiptDate> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (DBConditionalQuantityAttribute), "DecimalVerifyUnits", InventoryUnitType.PurchaseUnit)]
  [PXCustomizeBaseAttribute(typeof (DBConditionalQuantityAttribute), "ConvertToDecimalVerifyUnits", false)]
  protected virtual void _(PX.Data.Events.CacheAttached<POReceiptLine.receiptQty> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDBQuantityAttribute))]
  [PXDBQuantity]
  protected virtual void _(PX.Data.Events.CacheAttached<POLine.receivedQty> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDBDecimalAttribute))]
  [PXDBBaseQtyWithOrigQty(typeof (POLine.uOM), typeof (POLine.receivedQty), typeof (POLine.uOM), typeof (POLine.baseOrderQty), typeof (POLine.orderQty), HandleEmptyKey = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<POLine.baseReceivedQty> e)
  {
  }

  public POReceiptLineSplittingExtension LineSplittingExt
  {
    get => ((PXGraph) this).FindImplementation<POReceiptLineSplittingExtension>();
  }

  [PXOptimizationBehavior(IgnoreBqlDelegate = true)]
  protected virtual IEnumerable Transactions()
  {
    this.PrefetchWithDetails();
    return (IEnumerable) null;
  }

  public virtual void PrefetchWithDetails()
  {
  }

  public virtual IEnumerable ApDocs()
  {
    if (((PXSelectBase<POReceipt>) this.Document).Current == null)
      return (IEnumerable) Enumerable.Empty<POReceiptAPDoc>();
    IEnumerable<PXResult<POReceiptLine>> source = ((IEnumerable<PXResult<POReceiptLine>>) PXSelectBase<POReceiptLine, PXSelectJoin<POReceiptLine, InnerJoin<PX.Objects.AP.APTran, On<PX.Objects.AP.APTran.pOAccrualType, Equal<POReceiptLine.pOAccrualType>, And<PX.Objects.AP.APTran.pOAccrualRefNoteID, Equal<POReceiptLine.pOAccrualRefNoteID>, And<PX.Objects.AP.APTran.pOAccrualLineNbr, Equal<POReceiptLine.pOAccrualLineNbr>>>>, LeftJoin<POAccrualSplit, On<POAccrualSplit.type, Equal<PX.Objects.AP.APTran.pOAccrualType>, And<POAccrualSplit.refNoteID, Equal<PX.Objects.AP.APTran.pOAccrualRefNoteID>, And<POAccrualSplit.lineNbr, Equal<PX.Objects.AP.APTran.pOAccrualLineNbr>, And<POAccrualSplit.aPDocType, Equal<PX.Objects.AP.APTran.tranType>, And<POAccrualSplit.aPRefNbr, Equal<PX.Objects.AP.APTran.refNbr>, And<POAccrualSplit.aPLineNbr, Equal<PX.Objects.AP.APTran.lineNbr>, And<POAccrualSplit.FK.ReceiptLine>>>>>>>>>, Where<KeysRelation<CompositeKey<Field<POReceiptLine.receiptType>.IsRelatedTo<POReceipt.receiptType>, Field<POReceiptLine.receiptNbr>.IsRelatedTo<POReceipt.receiptNbr>>.WithTablesOf<POReceipt, POReceiptLine>, POReceipt, POReceiptLine>.SameAsCurrent>>.Config>.Select((PXGraph) this, Array.Empty<object>())).AsEnumerable<PXResult<POReceiptLine>>();
    POReceipt current1 = ((PXSelectBase<POReceipt>) this.Document).Current;
    if (current1 != null)
    {
      string receiptType = current1.ReceiptType;
    }
    POReceipt current2 = ((PXSelectBase<POReceipt>) this.Document).Current;
    if (current2 != null)
    {
      string receiptNbr = current2.ReceiptNbr;
    }
    List<PX.Objects.AP.APTran> aptrans = source.Select<PXResult<POReceiptLine>, PX.Objects.AP.APTran>((Func<PXResult<POReceiptLine>, PX.Objects.AP.APTran>) (a => PXResult.Unwrap<PX.Objects.AP.APTran>((object) a))).ToList<PX.Objects.AP.APTran>();
    List<PXResult<POReceiptLine>> filtered = source.AsEnumerable<PXResult<POReceiptLine>>().Where<PXResult<POReceiptLine>>((Func<PXResult<POReceiptLine>, bool>) (a =>
    {
      POReceiptLine poReceiptLine = PXResult.Unwrap<POReceiptLine>((object) a);
      PX.Objects.AP.APTran apTran = PXResult.Unwrap<PX.Objects.AP.APTran>((object) a);
      POAccrualSplit poAccrualSplit = PXResult.Unwrap<POAccrualSplit>((object) a);
      if (!(poReceiptLine.POAccrualType != "R") && (!(apTran.ReceiptNbr == poReceiptLine.ReceiptNbr) || !(apTran.ReceiptType == poReceiptLine.ReceiptType)))
        return false;
      if (poReceiptLine.POAccrualType == "R" || poAccrualSplit.POReceiptNbr != null && poAccrualSplit.APRefNbr != null)
        return true;
      Decimal? nullable = poReceiptLine.UnbilledQty;
      Decimal num1 = 0M;
      if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
        return false;
      nullable = apTran.Qty;
      Decimal num2 = 0M;
      if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
      {
        bool? released = apTran.Released;
        bool flag = false;
        if (released.GetValueOrDefault() == flag & released.HasValue)
          return true;
      }
      nullable = apTran.UnreceivedQty;
      Decimal num3 = 0M;
      return !(nullable.GetValueOrDefault() == num3 & nullable.HasValue);
    })).ToList<PXResult<POReceiptLine>>();
    List<POReceiptAPDoc> list = filtered.Select<PXResult<POReceiptLine>, PX.Objects.AP.APTran>((Func<PXResult<POReceiptLine>, PX.Objects.AP.APTran>) (a => PXResult.Unwrap<PX.Objects.AP.APTran>((object) a))).GroupBy(a => new
    {
      DocType = a.TranType,
      RefNbr = a.RefNbr
    }).Select<IGrouping<\u003C\u003Ef__AnonymousType0<string, string>, PX.Objects.AP.APTran>, POReceiptAPDoc>(a =>
    {
      PX.Objects.AP.APInvoice apInvoice = PX.Objects.AP.APInvoice.PK.Find((PXGraph) this, a.Key.DocType, a.Key.RefNbr);
      return new POReceiptAPDoc()
      {
        DocType = a.Key.DocType,
        RefNbr = a.Key.RefNbr,
        Status = apInvoice?.Status,
        DocDate = (DateTime?) apInvoice?.DocDate,
        AccruedQty = new Decimal?(filtered.Select<PXResult<POReceiptLine>, POAccrualSplit>((Func<PXResult<POReceiptLine>, POAccrualSplit>) (b => PXResult.Unwrap<POAccrualSplit>((object) b))).Where<POAccrualSplit>((Func<POAccrualSplit, bool>) (b => b.APDocType == a.Key.DocType && b.APRefNbr == a.Key.RefNbr)).Sum<POAccrualSplit>((Func<POAccrualSplit, Decimal?>) (b => b.BaseAccruedQty)).GetValueOrDefault()),
        AccruedAmt = new Decimal?(filtered.Select<PXResult<POReceiptLine>, POAccrualSplit>((Func<PXResult<POReceiptLine>, POAccrualSplit>) (b => PXResult.Unwrap<POAccrualSplit>((object) b))).Where<POAccrualSplit>((Func<POAccrualSplit, bool>) (b => b.APDocType == a.Key.DocType && b.APRefNbr == a.Key.RefNbr)).Sum<POAccrualSplit>((Func<POAccrualSplit, Decimal?>) (b => b.AccruedCost)).GetValueOrDefault()),
        TotalPPVAmt = new Decimal?(filtered.Select<PXResult<POReceiptLine>, POAccrualSplit>((Func<PXResult<POReceiptLine>, POAccrualSplit>) (b => PXResult.Unwrap<POAccrualSplit>((object) b))).Where<POAccrualSplit>((Func<POAccrualSplit, bool>) (b => b.APDocType == a.Key.DocType && b.APRefNbr == a.Key.RefNbr)).Sum<POAccrualSplit>((Func<POAccrualSplit, Decimal?>) (b => b.PPVAmt)).GetValueOrDefault()),
        TotalQty = aptrans.Where<PX.Objects.AP.APTran>((Func<PX.Objects.AP.APTran, bool>) (b => b.TranType == a.Key.DocType && b.RefNbr == a.Key.RefNbr)).Sum<PX.Objects.AP.APTran>((Func<PX.Objects.AP.APTran, Decimal?>) (b =>
        {
          Decimal? baseQty = b.BaseQty;
          Decimal sign = b.Sign;
          return !baseQty.HasValue ? new Decimal?() : new Decimal?(baseQty.GetValueOrDefault() * sign);
        })),
        TotalAmt = aptrans.Where<PX.Objects.AP.APTran>((Func<PX.Objects.AP.APTran, bool>) (b => b.TranType == a.Key.DocType && b.RefNbr == a.Key.RefNbr)).Sum<PX.Objects.AP.APTran>((Func<PX.Objects.AP.APTran, Decimal?>) (b =>
        {
          Decimal? tranAmt = b.TranAmt;
          Decimal sign = b.Sign;
          return !tranAmt.HasValue ? new Decimal?() : new Decimal?(tranAmt.GetValueOrDefault() * sign);
        }))
      };
    }).ToList<POReceiptAPDoc>();
    object obj = (object) null;
    ((PXGraph) this).Caches[typeof (PX.Objects.AP.APTran)].RaiseFieldSelecting<PX.Objects.AP.APTran.tranAmt>((object) aptrans.FirstOrDefault<PX.Objects.AP.APTran>(), ref obj, true);
    int precision = obj is PXDecimalState pxDecimalState ? ((PXFieldState) pxDecimalState).Precision : 0;
    string str;
    if (PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      str = PXMessages.LocalizeFormatNoPrefix("Total Billed Qty. {0}, Total Billed Amt. {1}, Total Accrued Qty. {2}, Total Accrued Amt. {3}, Total PPV Amt. {4}", new object[5]
      {
        (object) this.FormatQty(list.Sum<POReceiptAPDoc>((Func<POReceiptAPDoc, Decimal?>) (t => t.TotalQty))),
        (object) this.FormatAmt(new Decimal?(list.Sum<POReceiptAPDoc>((Func<POReceiptAPDoc, Decimal?>) (t => t.TotalAmt)).Value), precision),
        (object) this.FormatQty(list.Sum<POReceiptAPDoc>((Func<POReceiptAPDoc, Decimal?>) (t => t.AccruedQty))),
        (object) this.FormatAmt(new Decimal?(list.Sum<POReceiptAPDoc>((Func<POReceiptAPDoc, Decimal?>) (t => t.AccruedAmt)).Value), precision),
        (object) this.FormatAmt(new Decimal?(list.Sum<POReceiptAPDoc>((Func<POReceiptAPDoc, Decimal?>) (t => t.TotalPPVAmt)).Value), precision)
      });
    else
      str = PXMessages.LocalizeFormatNoPrefix("Total Billed Qty. {0}, Total Billed Amt. {1}, Total Accrued Qty. {2}, Total Accrued Amt. {3}", new object[4]
      {
        (object) this.FormatQty(list.Sum<POReceiptAPDoc>((Func<POReceiptAPDoc, Decimal?>) (t => t.TotalQty))),
        (object) this.FormatAmt(new Decimal?(list.Sum<POReceiptAPDoc>((Func<POReceiptAPDoc, Decimal?>) (t => t.TotalAmt)).Value), precision),
        (object) this.FormatQty(list.Sum<POReceiptAPDoc>((Func<POReceiptAPDoc, Decimal?>) (t => t.AccruedQty))),
        (object) this.FormatAmt(new Decimal?(list.Sum<POReceiptAPDoc>((Func<POReceiptAPDoc, Decimal?>) (t => t.AccruedAmt)).Value), precision)
      });
    foreach (POReceiptAPDoc poReceiptApDoc in list)
      poReceiptApDoc.StatusText = str;
    return (IEnumerable) list;
  }

  public IEnumerable ReceiptHistory()
  {
    ParameterExpression parameterExpression;
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    // ISSUE: method reference
    return (IEnumerable) ((IQueryable<PXResult<POReceipt>>) ((PXSelectBase<POReceipt>) new PXSelectJoinGroupBy<POReceipt, InnerJoin<POReceiptLine, On<POReceiptLine.FK.Receipt>>, Where<POReceiptLine.origReceiptType, Equal<Current<POReceipt.receiptType>>, And<POReceiptLine.origReceiptNbr, Equal<Current<POReceipt.receiptNbr>>>>, Aggregate<GroupBy<POReceipt.receiptType, GroupBy<POReceipt.receiptNbr, Sum<POReceiptLine.baseReceiptQty>>>>>((PXGraph) this)).Select(Array.Empty<object>())).Select<PXResult<POReceipt>, PXResult<POReceipt, POReceiptLine>>((Expression<Func<PXResult<POReceipt>, PXResult<POReceipt, POReceiptLine>>>) (t => (PXResult<POReceipt, POReceiptLine>) t)).Select<PXResult<POReceipt, POReceiptLine>, POReceiptPOOriginal>(Expression.Lambda<Func<PXResult<POReceipt, POReceiptLine>, POReceiptPOOriginal>>((Expression) Expression.MemberInit(Expression.New(typeof (POReceiptPOOriginal)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (POReceiptPOOriginal.set_ReceiptType)), )))); // Unable to render the statement
  }

  public virtual string FormatQty(Decimal? value)
  {
    return value.HasValue ? value.Value.ToString("N" + CommonSetupDecPl.Qty.ToString(), (IFormatProvider) NumberFormatInfo.CurrentInfo) : string.Empty;
  }

  public virtual string FormatAmt(Decimal? value, int precision)
  {
    return value.HasValue ? value.Value.ToString("N" + precision.ToString(), (IFormatProvider) NumberFormatInfo.CurrentInfo) : string.Empty;
  }

  protected virtual IEnumerable pOReceiptReturn()
  {
    PXSelectJoinGroupBy<POReceiptReturn, LeftJoin<POOrderReceipt, On<POOrderReceipt.receiptType, Equal<POReceiptReturn.receiptType>, And<POOrderReceipt.receiptNbr, Equal<POReceiptReturn.receiptNbr>>>, InnerJoin<POReceiptLineReturn, On<POReceiptLineReturn.receiptType, Equal<POReceiptReturn.receiptType>, And<POReceiptLineReturn.receiptNbr, Equal<POReceiptReturn.receiptNbr>>>, LeftJoin<POReceiptLine, On<POReceiptLine.origReceiptType, Equal<POReceiptLineReturn.receiptType>, And<POReceiptLine.origReceiptNbr, Equal<POReceiptLineReturn.receiptNbr>, And<POReceiptLine.origReceiptLineNbr, Equal<POReceiptLineReturn.lineNbr>, And<POReceiptLine.released, Equal<False>>>>>>>>, Where<POReceiptReturn.receiptType, Equal<POReceiptType.poreceipt>, And<POReceiptReturn.released, Equal<True>, And<POReceiptReturn.vendorID, Equal<Current<POReceipt.vendorID>>, And<POReceiptReturn.vendorLocationID, Equal<Current<POReceipt.vendorLocationID>>, And2<Where<POReceiptLine.receiptNbr, IsNull, Or<KeysRelation<CompositeKey<Field<POReceiptLine.receiptType>.IsRelatedTo<POReceipt.receiptType>, Field<POReceiptLine.receiptNbr>.IsRelatedTo<POReceipt.receiptNbr>>.WithTablesOf<POReceipt, POReceiptLine>, POReceipt, POReceiptLine>.SameAsCurrent>>, And2<Where<Current<POReceiptReturnFilter.orderType>, IsNull, Or<POOrderReceipt.pOType, Equal<Current<POReceiptReturnFilter.orderType>>>>, And2<Where<Current<POReceiptReturnFilter.orderNbr>, IsNull, Or<POOrderReceipt.pONbr, Equal<Current<POReceiptReturnFilter.orderNbr>>>>, And<Where<Current<POReceiptReturnFilter.receiptNbr>, IsNull, Or<POReceiptReturn.receiptNbr, Equal<Current<POReceiptReturnFilter.receiptNbr>>>>>>>>>>>>, Aggregate<GroupBy<POReceiptReturn.receiptNbr, GroupBy<POReceiptReturn.released, Count<POReceiptLineReturn.lineNbr>>>>, OrderBy<Desc<POReceiptReturn.receiptNbr>>> selectJoinGroupBy = new PXSelectJoinGroupBy<POReceiptReturn, LeftJoin<POOrderReceipt, On<POOrderReceipt.receiptType, Equal<POReceiptReturn.receiptType>, And<POOrderReceipt.receiptNbr, Equal<POReceiptReturn.receiptNbr>>>, InnerJoin<POReceiptLineReturn, On<POReceiptLineReturn.receiptType, Equal<POReceiptReturn.receiptType>, And<POReceiptLineReturn.receiptNbr, Equal<POReceiptReturn.receiptNbr>>>, LeftJoin<POReceiptLine, On<POReceiptLine.origReceiptType, Equal<POReceiptLineReturn.receiptType>, And<POReceiptLine.origReceiptNbr, Equal<POReceiptLineReturn.receiptNbr>, And<POReceiptLine.origReceiptLineNbr, Equal<POReceiptLineReturn.lineNbr>, And<POReceiptLine.released, Equal<False>>>>>>>>, Where<POReceiptReturn.receiptType, Equal<POReceiptType.poreceipt>, And<POReceiptReturn.released, Equal<True>, And<POReceiptReturn.vendorID, Equal<Current<POReceipt.vendorID>>, And<POReceiptReturn.vendorLocationID, Equal<Current<POReceipt.vendorLocationID>>, And2<Where<POReceiptLine.receiptNbr, IsNull, Or<KeysRelation<CompositeKey<Field<POReceiptLine.receiptType>.IsRelatedTo<POReceipt.receiptType>, Field<POReceiptLine.receiptNbr>.IsRelatedTo<POReceipt.receiptNbr>>.WithTablesOf<POReceipt, POReceiptLine>, POReceipt, POReceiptLine>.SameAsCurrent>>, And2<Where<Current<POReceiptReturnFilter.orderType>, IsNull, Or<POOrderReceipt.pOType, Equal<Current<POReceiptReturnFilter.orderType>>>>, And2<Where<Current<POReceiptReturnFilter.orderNbr>, IsNull, Or<POOrderReceipt.pONbr, Equal<Current<POReceiptReturnFilter.orderNbr>>>>, And<Where<Current<POReceiptReturnFilter.receiptNbr>, IsNull, Or<POReceiptReturn.receiptNbr, Equal<Current<POReceiptReturnFilter.receiptNbr>>>>>>>>>>>>, Aggregate<GroupBy<POReceiptReturn.receiptNbr, GroupBy<POReceiptReturn.released, Count<POReceiptLineReturn.lineNbr>>>>, OrderBy<Desc<POReceiptReturn.receiptNbr>>>((PXGraph) this);
    \u003C\u003Ef__AnonymousType84<string, string, int>[] currentReceiptNbrs = GraphHelper.RowCast<POReceiptLine>((IEnumerable) ((PXSelectBase<POReceiptLine>) this.transactions).Select(Array.Empty<object>())).GroupBy(t => new
    {
      ReceiptType = t.OrigReceiptType,
      ReceiptNbr = t.OrigReceiptNbr
    }).Select(t => new
    {
      ReceiptType = t.Key.ReceiptType,
      ReceiptNbr = t.Key.ReceiptNbr,
      RowCount = t.Count<POReceiptLine>()
    }).ToArray();
    int startRow = PXView.StartRow;
    int num1 = 0;
    int maximumRows = PXView.MaximumRows;
    if (currentReceiptNbrs.Any())
      maximumRows += currentReceiptNbrs.Length;
    List<object> source = ((PXSelectBase) selectJoinGroupBy).View.Select(PXView.Currents, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, maximumRows, ref num1);
    PXView.StartRow = 0;
    return (IEnumerable) source.Select<object, PXResult<POReceiptReturn, POOrderReceipt, POReceiptLineReturn>>((Func<object, PXResult<POReceiptReturn, POOrderReceipt, POReceiptLineReturn>>) (o => (PXResult<POReceiptReturn, POOrderReceipt, POReceiptLineReturn>) o)).Where<PXResult<POReceiptReturn, POOrderReceipt, POReceiptLineReturn>>((Func<PXResult<POReceiptReturn, POOrderReceipt, POReceiptLineReturn>, bool>) (t =>
    {
      int? rowCount = ((PXResult) t).RowCount;
      int num2 = 0;
      return rowCount.GetValueOrDefault() > num2 & rowCount.HasValue;
    })).Where<PXResult<POReceiptReturn, POOrderReceipt, POReceiptLineReturn>>((Func<PXResult<POReceiptReturn, POOrderReceipt, POReceiptLineReturn>, bool>) (t => !currentReceiptNbrs.Any(m => m.ReceiptType == PXResult<POReceiptReturn, POOrderReceipt, POReceiptLineReturn>.op_Implicit(t).ReceiptType && m.ReceiptNbr == PXResult<POReceiptReturn, POOrderReceipt, POReceiptLineReturn>.op_Implicit(t).ReceiptNbr && m.RowCount >= ((PXResult) t).RowCount.Value))).Where<PXResult<POReceiptReturn, POOrderReceipt, POReceiptLineReturn>>((Func<PXResult<POReceiptReturn, POOrderReceipt, POReceiptLineReturn>, bool>) (c => PXResult<POReceiptReturn, POOrderReceipt, POReceiptLineReturn>.op_Implicit(c).CuryID == ((PXSelectBase<POReceipt>) this.Document).Current?.CuryID)).Select<PXResult<POReceiptReturn, POOrderReceipt, POReceiptLineReturn>, POReceiptReturn>((Func<PXResult<POReceiptReturn, POOrderReceipt, POReceiptLineReturn>, POReceiptReturn>) (o => PXResult<POReceiptReturn, POOrderReceipt, POReceiptLineReturn>.op_Implicit(o))).Select<POReceiptReturn, POReceiptReturn>((Func<POReceiptReturn, POReceiptReturn>) (rct => GraphHelper.RowCast<POReceiptReturn>(((PXSelectBase) this.poReceiptReturn).Cache.Updated).FirstOrDefault<POReceiptReturn>((Func<POReceiptReturn, bool>) (u => u.ReceiptType == rct.ReceiptType && u.ReceiptNbr == rct.ReceiptNbr)) ?? rct)).ToList<POReceiptReturn>();
  }

  protected virtual IEnumerable PoReceiptLineReturn()
  {
    PXSelectJoin<POReceiptLineReturn, LeftJoin<POReceiptLine, On<POReceiptLine.origReceiptType, Equal<POReceiptLineReturn.receiptType>, And<POReceiptLine.origReceiptNbr, Equal<POReceiptLineReturn.receiptNbr>, And<POReceiptLine.origReceiptLineNbr, Equal<POReceiptLineReturn.lineNbr>, And<POReceiptLine.released, Equal<False>>>>>>, Where<POReceiptLineReturn.vendorID, Equal<Current<POReceipt.vendorID>>, And<POReceiptLineReturn.vendorLocationID, Equal<Current<POReceipt.vendorLocationID>>, And2<Where<POReceiptLine.receiptNbr, IsNull, Or<KeysRelation<CompositeKey<Field<POReceiptLine.receiptType>.IsRelatedTo<POReceipt.receiptType>, Field<POReceiptLine.receiptNbr>.IsRelatedTo<POReceipt.receiptNbr>>.WithTablesOf<POReceipt, POReceiptLine>, POReceipt, POReceiptLine>.SameAsCurrent>>, And2<Where<Current<POReceiptReturnFilter.orderType>, IsNull, Or<POReceiptLineReturn.pOType, Equal<Current<POReceiptReturnFilter.orderType>>>>, And2<Where<Current<POReceiptReturnFilter.orderNbr>, IsNull, Or<POReceiptLineReturn.pONbr, Equal<Current<POReceiptReturnFilter.orderNbr>>>>, And2<Where<Current<POReceiptReturnFilter.receiptNbr>, IsNull, Or<POReceiptLineReturn.receiptNbr, Equal<Current<POReceiptReturnFilter.receiptNbr>>>>, And<Where<Current<POReceiptReturnFilter.inventoryID>, IsNull, Or<POReceiptLineReturn.inventoryID, Equal<Current<POReceiptReturnFilter.inventoryID>>>>>>>>>>>, OrderBy<Desc<POReceiptLineReturn.receiptNbr, Asc<POReceiptLineReturn.lineNbr>>>> pxSelectJoin = new PXSelectJoin<POReceiptLineReturn, LeftJoin<POReceiptLine, On<POReceiptLine.origReceiptType, Equal<POReceiptLineReturn.receiptType>, And<POReceiptLine.origReceiptNbr, Equal<POReceiptLineReturn.receiptNbr>, And<POReceiptLine.origReceiptLineNbr, Equal<POReceiptLineReturn.lineNbr>, And<POReceiptLine.released, Equal<False>>>>>>, Where<POReceiptLineReturn.vendorID, Equal<Current<POReceipt.vendorID>>, And<POReceiptLineReturn.vendorLocationID, Equal<Current<POReceipt.vendorLocationID>>, And2<Where<POReceiptLine.receiptNbr, IsNull, Or<KeysRelation<CompositeKey<Field<POReceiptLine.receiptType>.IsRelatedTo<POReceipt.receiptType>, Field<POReceiptLine.receiptNbr>.IsRelatedTo<POReceipt.receiptNbr>>.WithTablesOf<POReceipt, POReceiptLine>, POReceipt, POReceiptLine>.SameAsCurrent>>, And2<Where<Current<POReceiptReturnFilter.orderType>, IsNull, Or<POReceiptLineReturn.pOType, Equal<Current<POReceiptReturnFilter.orderType>>>>, And2<Where<Current<POReceiptReturnFilter.orderNbr>, IsNull, Or<POReceiptLineReturn.pONbr, Equal<Current<POReceiptReturnFilter.orderNbr>>>>, And2<Where<Current<POReceiptReturnFilter.receiptNbr>, IsNull, Or<POReceiptLineReturn.receiptNbr, Equal<Current<POReceiptReturnFilter.receiptNbr>>>>, And<Where<Current<POReceiptReturnFilter.inventoryID>, IsNull, Or<POReceiptLineReturn.inventoryID, Equal<Current<POReceiptReturnFilter.inventoryID>>>>>>>>>>>, OrderBy<Desc<POReceiptLineReturn.receiptNbr, Asc<POReceiptLineReturn.lineNbr>>>>((PXGraph) this);
    \u003C\u003Ef__AnonymousType85<string, string, int?>[] currentReceiptLines = GraphHelper.RowCast<POReceiptLine>((IEnumerable) ((PXSelectBase<POReceiptLine>) this.transactions).Select(Array.Empty<object>())).Select(t => new
    {
      POReceiptType = t.OrigReceiptType,
      POReceiptNbr = t.OrigReceiptNbr,
      POReceiptLineNbr = t.OrigReceiptLineNbr
    }).ToArray();
    int startRow = PXView.StartRow;
    int num = 0;
    int maximumRows = PXView.MaximumRows;
    if (currentReceiptLines.Any() && maximumRows > 0)
      maximumRows += currentReceiptLines.Length;
    List<POReceiptLineReturn> list = GraphHelper.RowCast<POReceiptLineReturn>((IEnumerable) ((PXSelectBase) pxSelectJoin).View.Select(PXView.Currents, PXView.Parameters, new object[PXView.SortColumns.Length], PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, maximumRows, ref num)).ToList<POReceiptLineReturn>();
    PXView.StartRow = 0;
    Func<POReceiptLineReturn, bool> predicate = (Func<POReceiptLineReturn, bool>) (t => !currentReceiptLines.Contains(new
    {
      POReceiptType = t.ReceiptType,
      POReceiptNbr = t.ReceiptNbr,
      POReceiptLineNbr = t.LineNbr
    }));
    return (IEnumerable) list.Where<POReceiptLineReturn>(predicate).Where<POReceiptLineReturn>((Func<POReceiptLineReturn, bool>) (c => c.CuryID == ((PXSelectBase<POReceipt>) this.Document).Current?.CuryID)).ToList<POReceiptLineReturn>();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold")]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter)
  {
    return (IEnumerable) adapter.Get<POReceipt>();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Remove Hold")]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter)
  {
    return (IEnumerable) adapter.Get<POReceipt>();
  }

  [PXUIField]
  [PXCancelButton]
  protected virtual IEnumerable cancel(PXAdapter adapter)
  {
    POReceiptEntry poReceiptEntry = this;
    foreach (object obj in ((PXAction) new PXCancel<POReceipt>((PXGraph) poReceiptEntry, "Cancel")).Press(adapter))
    {
      POReceipt poReceipt1 = PXResult.Unwrap<POReceipt>(obj);
      if (!string.IsNullOrEmpty(poReceipt1.ReceiptNbr))
      {
        POReceipt poReceipt2 = (POReceipt) PrimaryKeyOf<POReceipt>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<POReceipt.receiptType, POReceipt.receiptNbr>>.Find((PXGraph) poReceiptEntry, (TypeArrayOf<IBqlField>.IFilledWith<POReceipt.receiptType, POReceipt.receiptNbr>) poReceipt1, (PKFindOptions) 0);
        if (poReceipt2 != null && poReceipt2.ReceiptType != poReceipt1.ReceiptType)
        {
          string str = new POReceiptType.ListAttribute().ValueLabelDic[poReceipt1.ReceiptType];
          ((PXSelectBase) poReceiptEntry.Document).Cache.RaiseExceptionHandling<POReceipt.receiptNbr>((object) poReceipt1, (object) poReceipt1.ReceiptNbr, (Exception) new PXSetPropertyException("Document with number {0} already exists but it has a type {1}. Document Number must be unique for all the PO Receipts regardless of the type", new object[3]
          {
            (object) poReceipt1.ReceiptNbr,
            (object) str,
            (object) poReceipt2.ReceiptType
          }));
          ((PXSelectBase<POReceipt>) poReceiptEntry.Document).Current.ReceiptNbr = (string) null;
        }
      }
      yield return obj;
    }
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CreateReturn(PXAdapter adapter)
  {
    POReceipt current = ((PXSelectBase<POReceipt>) this.Document).Current;
    if ((current != null ? (!current.Released.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      throw new PXException("Document Status is invalid for processing.");
    List<POReceiptLine> list = GraphHelper.RowCast<POReceiptLine>(((PXSelectBase) this.transactions).Cache.Updated).Where<POReceiptLine>((Func<POReceiptLine, bool>) (l => l.Selected.GetValueOrDefault())).OrderBy<POReceiptLine, int?>((Func<POReceiptLine, int?>) (l => l.SortOrder)).ThenBy<POReceiptLine, int?>((Func<POReceiptLine, int?>) (l => l.LineNbr)).ToList<POReceiptLine>();
    if (!list.Any<POReceiptLine>())
      throw new PXException("Select lines for processing.");
    POReceiptEntry instance = PXGraph.CreateInstance<POReceiptEntry>();
    POReceipt poReceipt1 = ((PXSelectBase<POReceipt>) instance.Document).Insert(new POReceipt()
    {
      ReceiptType = "RN"
    });
    poReceipt1.BranchID = current.BranchID;
    poReceipt1.VendorID = current.VendorID;
    poReceipt1.VendorLocationID = current.VendorLocationID;
    poReceipt1.ProjectID = current.ProjectID;
    POReceipt poReceipt2 = ((PXSelectBase<POReceipt>) instance.Document).Update(poReceipt1);
    if (list.Any<POReceiptLine>((Func<POReceiptLine, bool>) (x => POLineType.IsProjectDropShip(x.LineType))))
      poReceipt2.ReturnInventoryCostMode = "O";
    else if (list.Any<POReceiptLine>((Func<POReceiptLine, bool>) (x => x.IsSpecialOrder.GetValueOrDefault())))
      poReceipt2.ReturnInventoryCostMode = "I";
    if (!(poReceipt2.CuryID != current.CuryID))
    {
      int? branchId1 = poReceipt2.BranchID;
      int? branchId2 = current.BranchID;
      if (branchId1.GetValueOrDefault() == branchId2.GetValueOrDefault() & branchId1.HasValue == branchId2.HasValue)
        goto label_11;
    }
    POReceipt copy = (POReceipt) ((PXSelectBase) instance.Document).Cache.CreateCopy((object) poReceipt2);
    copy.CuryID = current.CuryID;
    copy.BranchID = current.BranchID;
    poReceipt2 = ((PXSelectBase<POReceipt>) instance.Document).Update(copy);
label_11:
    instance.CopyReceiptCurrencyInfoToReturn(this.GetCurrencyInfo(current.CuryInfoID), instance.GetCurrencyInfo(poReceipt2.CuryInfoID), poReceipt2.ReturnInventoryCostMode == "O");
    foreach (POReceiptLine origLine in list)
    {
      POReceiptLine poReceiptLine = instance.AddReturnLine((IPOReturnLineSource) origLine);
      poReceiptLine.BranchID = origLine.BranchID;
      ((PXSelectBase<POReceiptLine>) instance.transactions).Update(poReceiptLine);
    }
    throw new PXRedirectRequiredException((PXGraph) instance, "Switch to PO Receipt");
  }

  public virtual PX.Objects.CM.Extensions.CurrencyInfo GetCurrencyInfo(long? curyInfoID)
  {
    return this.MultiCurrencyExt.GetCurrencyInfo(curyInfoID);
  }

  public virtual bool IsSameCurrencyInfo(PX.Objects.CM.Extensions.CurrencyInfo curyA, PX.Objects.CM.Extensions.CurrencyInfo curyB)
  {
    if (curyA == null)
      throw new ArgumentNullException(nameof (curyA));
    if (curyB == null)
      throw new ArgumentNullException(nameof (curyB));
    if (curyA.CuryID == curyB.CuryID)
    {
      DateTime? curyEffDate1 = curyA.CuryEffDate;
      DateTime? curyEffDate2 = curyB.CuryEffDate;
      if ((curyEffDate1.HasValue == curyEffDate2.HasValue ? (curyEffDate1.HasValue ? (curyEffDate1.GetValueOrDefault() == curyEffDate2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      {
        Decimal? curyRate1 = curyA.CuryRate;
        Decimal? curyRate2 = curyB.CuryRate;
        if (curyRate1.GetValueOrDefault() == curyRate2.GetValueOrDefault() & curyRate1.HasValue == curyRate2.HasValue)
        {
          if (!(curyA.CuryMultDiv == curyB.CuryMultDiv))
          {
            Decimal? curyRate3 = curyA.CuryRate;
            Decimal num1 = 1M;
            if (curyRate3.GetValueOrDefault() == num1 & curyRate3.HasValue)
            {
              curyRate3 = curyB.CuryRate;
              Decimal num2 = 1M;
              if (!(curyRate3.GetValueOrDefault() == num2 & curyRate3.HasValue))
                goto label_11;
            }
            else
              goto label_11;
          }
          return curyA.CuryRateTypeID == curyB.CuryRateTypeID;
        }
      }
    }
label_11:
    return false;
  }

  public virtual void CopyReceiptCurrencyInfoToReturn(
    long? receipt_CuryInfoID,
    long? return_CuryInfoID,
    bool copyCuryRate = true)
  {
    this.CopyReceiptCurrencyInfoToReturn(this.GetCurrencyInfo(receipt_CuryInfoID), this.GetCurrencyInfo(return_CuryInfoID), copyCuryRate);
  }

  public virtual void CopyReceiptCurrencyInfoToReturn(
    PX.Objects.CM.Extensions.CurrencyInfo receipt_CuryInfo,
    PX.Objects.CM.Extensions.CurrencyInfo return_CuryInfo,
    bool copyCuryRate = true)
  {
    if (receipt_CuryInfo == null || return_CuryInfo == null)
      return;
    return_CuryInfo.BaseCuryID = receipt_CuryInfo.BaseCuryID;
    return_CuryInfo.CuryID = receipt_CuryInfo.CuryID;
    return_CuryInfo.CuryRateTypeID = receipt_CuryInfo.CuryRateTypeID;
    if (copyCuryRate)
    {
      return_CuryInfo.CuryEffDate = receipt_CuryInfo.CuryEffDate;
      return_CuryInfo.CuryRate = receipt_CuryInfo.CuryRate;
      return_CuryInfo.RecipRate = receipt_CuryInfo.RecipRate;
      return_CuryInfo.CuryMultDiv = receipt_CuryInfo.CuryMultDiv;
    }
    ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.MultiCurrencyExt.currencyinfo).Update(return_CuryInfo);
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable Release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    POReceiptEntry.\u003C\u003Ec__DisplayClass82_0 cDisplayClass820 = new POReceiptEntry.\u003C\u003Ec__DisplayClass82_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass820.list = new List<POReceipt>();
    foreach (POReceipt poReceipt in adapter.Get<POReceipt>())
    {
      bool? nullable = poReceipt.Hold;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = poReceipt.Released;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
        {
          // ISSUE: reference to a compiler-generated field
          cDisplayClass820.list.Add(((PXSelectBase<POReceipt>) this.Document).Update(poReceipt));
        }
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass820.list.Count == 0)
      throw new PXException("Document Status is invalid for processing.");
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass820, __methodptr(\u003CRelease\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass820.list;
  }

  protected virtual IEnumerable defaultCompanyContact()
  {
    return (IEnumerable) OrganizationMaint.GetDefaultContactForCurrentOrganization((PXGraph) this);
  }

  [PXUIField(DisplayName = "Notifications", Visible = false)]
  [PXButton(ImageKey = "DataEntryF")]
  protected virtual IEnumerable Notification(PXAdapter adapter, [PXString] string notificationCD)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    POReceiptEntry.\u003C\u003Ec__DisplayClass87_0 cDisplayClass870 = new POReceiptEntry.\u003C\u003Ec__DisplayClass87_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass870.notificationCD = notificationCD;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass870.massProcess = adapter.MassProcess;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass870.receipts = adapter.Get<POReceipt>().ToArray<POReceipt>();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass870, __methodptr(\u003CNotification\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass870.receipts;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable EmailPurchaseReceipt(PXAdapter adapter, [PXString] string notificationCD = null)
  {
    return this.Notification(adapter, notificationCD ?? "PURCHASE RECEIPT");
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Report(PXAdapter adapter, [PXString(8, InputMask = "CC.CC.CC.CC"), POReceiptEntryReports] string reportID)
  {
    List<POReceipt> list = adapter.Get<POReceipt>().ToList<POReceipt>();
    if (!string.IsNullOrEmpty(reportID))
    {
      ((PXAction) this.Save).Press();
      int num = 0;
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (POReceipt poReceipt in list)
      {
        if (reportID == "PO632000")
        {
          dictionary["FinPeriodID"] = (string) ((PXSelectBase<POReceipt>) this.Document).GetValueExt<POReceipt.finPeriodID>(poReceipt);
          dictionary["ReceiptNbr"] = poReceipt.ReceiptNbr;
        }
        else
        {
          dictionary["ReceiptType"] = poReceipt.ReceiptType;
          dictionary["ReceiptNbr"] = poReceipt.ReceiptNbr;
        }
        ++num;
      }
      if (num > 0)
        throw new PXReportRequiredException(dictionary, reportID, (PXBaseRedirectException.WindowMode) 2, $"Report {reportID}", (CurrentLocalization) null);
    }
    return (IEnumerable) list;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable PrintPurchaseReceipt(PXAdapter adapter)
  {
    return this.Report(adapter.Apply<PXAdapter>((Action<PXAdapter>) (a => a.Menu = "Print Purchase Receipt")), "PO646000");
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable PrintBillingDetail(PXAdapter adapter)
  {
    return this.Report(adapter.Apply<PXAdapter>((Action<PXAdapter>) (a => a.Menu = "Purchase Receipt Billing History")), "PO632000");
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable PrintAllocated(PXAdapter adapter)
  {
    return this.Report(adapter.Apply<PXAdapter>((Action<PXAdapter>) (a => a.Menu = "Purchase Receipt Allocated and Backordered")), "PO622000");
  }

  [PXUIField]
  [PXButton(ImageKey = "Process")]
  public virtual IEnumerable Assign(PXAdapter adapter)
  {
    if (!((PXSelectBase<POSetup>) this.posetup).Current.DefaultReceiptAssignmentMapID.HasValue)
      throw new PXSetPropertyException("Default Purchase Order Receipt Assignment Map is not entered in PO setup", new object[1]
      {
        (object) "PO Setup"
      });
    new EPAssignmentProcessor<POReceipt>().Assign(((PXSelectBase<POReceipt>) this.Document).Current, ((PXSelectBase<POSetup>) this.posetup).Current.DefaultReceiptAssignmentMapID);
    ((PXSelectBase<POReceipt>) this.Document).Update(((PXSelectBase<POReceipt>) this.Document).Current);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable SetAddLineFilterToSource(PXAdapter adapter)
  {
    object sourceItem = this.addLinePopupHandler.GetSourceItem();
    if (sourceItem != null)
      this.addLinePopupHandler.SetFilterToSource(((PXSelectBase) this.addReceipt).Cache, ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current, sourceItem);
    else
      this.addLinePopupHandler.SetFilterToError(((PXSelectBase) this.addReceipt).Cache, ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current);
    this.addLinePopupHandler.View.RequestRefresh();
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddPOOrder(PXAdapter adapter)
  {
    if (((PXSelectBase<POReceipt>) this.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<POReceipt>) this.Document).Current.Released;
      if (!nullable.GetValueOrDefault())
      {
        nullable = ((PXSelectBase<POReceiptEntry.POOrderFilter>) this.filter).Current.ResetFilter;
        if (nullable.GetValueOrDefault())
        {
          ((PXSelectBase) this.filter).Cache.Remove((object) ((PXSelectBase<POReceiptEntry.POOrderFilter>) this.filter).Current);
          ((PXSelectBase) this.filter).Cache.Insert((object) new POReceiptEntry.POOrderFilter());
        }
        else
          ((PXSelectBase) this.filter).Cache.RaiseRowSelected((object) ((PXSelectBase<POReceiptEntry.POOrderFilter>) this.filter).Current);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        if (((PXSelectBase<POReceiptEntry.POOrderS>) this.openOrders).AskExt(POReceiptEntry.\u003C\u003Ec.\u003C\u003E9__103_0 ?? (POReceiptEntry.\u003C\u003Ec.\u003C\u003E9__103_0 = new PXView.InitializePanel((object) POReceiptEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CAddPOOrder\u003Eb__103_0))), true) == 1)
          return this.AddPOOrder2(adapter);
      }
    }
    ((PXSelectBase) this.openOrders).Cache.Clear();
    ((PXSelectBase) this.openOrders).Cache.ClearQueryCache();
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddPOOrder2(PXAdapter adapter)
  {
    if (((PXSelectBase<POReceipt>) this.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<POReceipt>) this.Document).Current.Released;
      if (!nullable.GetValueOrDefault())
      {
        bool flag = false;
        foreach (POReceiptEntry.POOrderS aOrder in ((PXSelectBase) this.openOrders).Cache.Updated)
        {
          nullable = aOrder.Selected;
          if (nullable.GetValueOrDefault() && POReceiptEntry.IsPassingFilter(((PXSelectBase<POReceiptEntry.POOrderFilter>) this.filter).Current, (POOrder) aOrder))
          {
            if (!this.CanAddOrder((POOrder) aOrder, out string _))
              throw new PXException("Selected Purchase Orders cannot be added. Selected Orders must have same Currency, Type and Shipping Destinations.");
            try
            {
              this.AddPurchaseOrder((POOrder) aOrder);
            }
            catch (PXException ex)
            {
              PXTrace.WriteError((Exception) ex);
              ((PXSelectBase) this.openOrders).Cache.RaiseExceptionHandling<POReceiptEntry.POOrderS.selected>((object) aOrder, (object) aOrder.Selected, (Exception) new PXSetPropertyException<POReceiptEntry.POLineS.selected>(((Exception) ex).Message, (PXErrorLevel) 5));
              flag = true;
            }
            aOrder.Selected = new bool?(false);
          }
        }
        if (flag)
          throw new PXException("Failed to add one or more purchase orders. Please check the Trace for details.");
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddINTran(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddINTran2(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddPOOrderLine(PXAdapter adapter)
  {
    if (((PXSelectBase<POReceipt>) this.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<POReceipt>) this.Document).Current.Released;
      if (!nullable.GetValueOrDefault())
      {
        nullable = ((PXSelectBase<POReceiptEntry.POOrderFilter>) this.filter).Current.ResetFilter;
        if (nullable.GetValueOrDefault())
        {
          ((PXSelectBase) this.filter).Cache.Remove((object) ((PXSelectBase<POReceiptEntry.POOrderFilter>) this.filter).Current);
          ((PXSelectBase) this.filter).Cache.Insert((object) new POReceiptEntry.POOrderFilter());
        }
        else
          ((PXSelectBase) this.filter).Cache.RaiseRowSelected((object) ((PXSelectBase<POReceiptEntry.POOrderFilter>) this.filter).Current);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        if (((PXSelectBase<POReceiptEntry.POLineS>) this.poLinesSelection).AskExt(POReceiptEntry.\u003C\u003Ec.\u003C\u003E9__111_0 ?? (POReceiptEntry.\u003C\u003Ec.\u003C\u003E9__111_0 = new PXView.InitializePanel((object) POReceiptEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CAddPOOrderLine\u003Eb__111_0))), true) == 1)
          return this.AddPOOrderLine2(adapter);
      }
    }
    ((PXSelectBase) this.poLinesSelection).Cache.Clear();
    ((PXSelectBase) this.poLinesSelection).Cache.ClearQueryCache();
    return adapter.Get();
  }

  private void ClearSelectionCache<TField>(PXCache cache) where TField : IBqlField
  {
    if (!((PXGraph) this).IsImport)
    {
      cache.Clear();
    }
    else
    {
      foreach (object obj in ((PXSelectBase) this.poLinesSelection).Cache.Updated)
        cache.SetValue<TField>(obj, (object) false);
    }
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddPOOrderLine2(PXAdapter adapter)
  {
    if (((PXSelectBase<POReceipt>) this.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<POReceipt>) this.Document).Current.Released;
      if (!nullable.GetValueOrDefault())
      {
        bool flag = false;
        foreach (POLine poLine in ((PXSelectBase) this.poLinesSelection).Cache.Updated)
        {
          nullable = poLine.Selected;
          if (nullable.GetValueOrDefault())
          {
            try
            {
              this._skipUIUpdate = true;
              if (this.AddPOLine(poLine) != null)
                this.AddPOOrderReceipt(poLine.OrderType, poLine.OrderNbr);
            }
            catch (PXException ex)
            {
              ErrorProcessingEntityException processingEntityException = new ErrorProcessingEntityException(((PXSelectBase) this.poLinesSelection).Cache, (IBqlTable) poLine, ex);
              PXTrace.WriteError((Exception) processingEntityException);
              ((PXSelectBase) this.poLinesSelection).Cache.RaiseExceptionHandling<POReceiptEntry.POLineS.selected>((object) poLine, (object) poLine.Selected, (Exception) new PXSetPropertyException<POReceiptEntry.POLineS.selected>(((Exception) ex).Message, (PXErrorLevel) 5));
              flag = true;
              if (((PXGraph) this).IsContractBasedAPI)
                throw processingEntityException;
            }
            finally
            {
              this._skipUIUpdate = false;
            }
            poLine.Selected = new bool?(false);
          }
        }
        if (flag)
          throw new PXException("One purchase order line or multiple purchase order lines cannot be added to the bill. See Trace Log for details.");
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddPOReceiptLine(PXAdapter adapter)
  {
    if (((PXSelectBase<POReceipt>) this.Document).Current != null && !((PXSelectBase<POReceipt>) this.Document).Current.Released.GetValueOrDefault())
    {
      ((PXSelectBase<POReceiptEntry.POOrderFilter>) this.filter).Current.AllowAddLine = new bool?(false);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      if (((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).AskExt(POReceiptEntry.\u003C\u003Ec.\u003C\u003E9__116_0 ?? (POReceiptEntry.\u003C\u003Ec.\u003C\u003E9__116_0 = new PXView.InitializePanel((object) POReceiptEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CAddPOReceiptLine\u003Eb__116_0)))) == 1)
      {
        try
        {
          this._skipUIUpdate = ((PXGraph) this).IsImport;
          this.AddReceiptLine();
        }
        finally
        {
          this._skipUIUpdate = false;
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddPOReceiptLine2(PXAdapter adapter)
  {
    if (((PXSelectBase<POReceipt>) this.Document).Current != null && !((PXSelectBase<POReceipt>) this.Document).Current.Released.GetValueOrDefault() && this.AddReceiptLine())
    {
      this.ResetReceiptFilter(true);
      ((PXSelectBase) this.addReceipt).View.RequestRefresh();
    }
    return adapter.Get();
  }

  private void ResetReceiptFilter(bool keepDescription)
  {
    using (new ReadOnlyScope(new PXCache[1]
    {
      ((PXSelectBase) this.addReceipt).Cache
    }))
    {
      POReceiptEntry.POReceiptLineS current = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current;
      ((PXSelectBase) this.addReceipt).Cache.Remove((object) current);
      ((PXSelectBase) this.addReceipt).Cache.Insert((object) new POReceiptEntry.POReceiptLineS());
      ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.ByOne = current.ByOne;
      ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.AutoAddLine = current.AutoAddLine;
      if (keepDescription)
        ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.Description = current.Description;
      ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.ReceiptType = ((PXSelectBase<POReceipt>) this.Document).Current != null ? ((PXSelectBase<POReceipt>) this.Document).Current.ReceiptType : current.ReceiptType;
      ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.ReceiptVendorID = ((PXSelectBase<POReceipt>) this.Document).Current != null ? ((PXSelectBase<POReceipt>) this.Document).Current.VendorID : current.ReceiptVendorID;
      ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.ReceiptVendorLocationID = ((PXSelectBase<POReceipt>) this.Document).Current != null ? ((PXSelectBase<POReceipt>) this.Document).Current.VendorLocationID : current.ReceiptVendorLocationID;
    }
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddPOReceiptReturn(PXAdapter adapter)
  {
    if (((PXSelectBase<POReceipt>) this.Document).Current != null && !((PXSelectBase<POReceipt>) this.Document).Current.Released.GetValueOrDefault())
    {
      this.ResetReturnFilter();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      if (((PXSelectBase<POReceiptReturn>) this.poReceiptReturn).AskExt(POReceiptEntry.\u003C\u003Ec.\u003C\u003E9__121_0 ?? (POReceiptEntry.\u003C\u003Ec.\u003C\u003E9__121_0 = new PXView.InitializePanel((object) POReceiptEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CAddPOReceiptReturn\u003Eb__121_0))), true) == 1)
        return this.AddPOReceiptReturn2(adapter);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddPOReceiptReturn2(PXAdapter adapter)
  {
    if (((PXSelectBase<POReceipt>) this.Document).Current != null && !((PXSelectBase<POReceipt>) this.Document).Current.Released.GetValueOrDefault())
    {
      IEnumerable updated = ((PXSelectBase) this.poReceiptReturn).Cache.Updated;
      string[] array = GraphHelper.RowCast<POReceiptReturn>(((PXSelectBase) this.poReceiptReturn).Cache.Updated).Where<POReceiptReturn>((Func<POReceiptReturn, bool>) (r => r.Selected.GetValueOrDefault())).Select<POReceiptReturn, string>((Func<POReceiptReturn, string>) (r => r.ReceiptNbr)).ToArray<string>();
      if (((IEnumerable<string>) array).Any<string>())
      {
        foreach (PXResult<POReceiptLine> pxResult in PXSelectBase<POReceiptLine, PXSelectReadonly<POReceiptLine, Where<POReceiptLine.receiptType, Equal<POReceiptType.poreceipt>, And<POReceiptLine.receiptNbr, In<Required<POReceiptLine.receiptNbr>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) array
        }))
        {
          POReceiptLine origLine = PXResult<POReceiptLine>.op_Implicit(pxResult);
          try
          {
            this.AddReturnLine((IPOReturnLineSource) origLine);
          }
          catch (PXAlreadyCreatedException ex)
          {
            PXTrace.WriteWarning((Exception) ex);
          }
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddPOReceiptLineReturn(PXAdapter adapter)
  {
    if (((PXSelectBase<POReceipt>) this.Document).Current != null && !((PXSelectBase<POReceipt>) this.Document).Current.Released.GetValueOrDefault())
    {
      this.ResetReturnFilter();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      if (((PXSelectBase<POReceiptLineReturn>) this.poReceiptLineReturn).AskExt(POReceiptEntry.\u003C\u003Ec.\u003C\u003E9__125_0 ?? (POReceiptEntry.\u003C\u003Ec.\u003C\u003E9__125_0 = new PXView.InitializePanel((object) POReceiptEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CAddPOReceiptLineReturn\u003Eb__125_0))), true) == 1)
        return this.AddPOReceiptLineReturn2(adapter);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddPOReceiptLineReturn2(PXAdapter adapter)
  {
    if (((PXSelectBase<POReceipt>) this.Document).Current != null && !((PXSelectBase<POReceipt>) this.Document).Current.Released.GetValueOrDefault())
    {
      foreach (POReceiptLineReturn origLine in GraphHelper.RowCast<POReceiptLineReturn>(((PXSelectBase) this.poReceiptLineReturn).Cache.Updated).Where<POReceiptLineReturn>((Func<POReceiptLineReturn, bool>) (r => r.Selected.GetValueOrDefault())))
      {
        this.AddReturnLine((IPOReturnLineSource) origLine);
        origLine.Selected = new bool?(false);
      }
    }
    return adapter.Get();
  }

  protected virtual void ResetReturnFilter()
  {
    ((PXSelectBase) this.returnFilter).Cache.Remove((object) ((PXSelectBase<POReceiptReturnFilter>) this.returnFilter).Current);
    ((PXSelectBase) this.returnFilter).Cache.Insert();
    ((PXSelectBase) this.returnFilter).Cache.IsDirty = false;
  }

  protected virtual bool AddReceiptLine()
  {
    if (((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.VendorLocationID.HasValue || ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.OrigRefNbr != null)
    {
      Decimal? nullable1 = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.Qty;
      Decimal num1 = 0M;
      if (!(nullable1.GetValueOrDefault() <= num1 & nullable1.HasValue))
      {
        POReceiptEntry.POLineS aLine = PXResultset<POReceiptEntry.POLineS>.op_Implicit(((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.PONbr == null || !((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.POLineNbr.HasValue ? (PXResultset<POReceiptEntry.POLineS>) null : ((PXSelectBase<POReceiptEntry.POLineS>) this.poLinesSelection).Search<POReceiptEntry.POLineS.orderType, POReceiptEntry.POLineS.orderNbr, POReceiptEntry.POLineS.lineNbr>((object) ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.POType, (object) ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.PONbr, (object) ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.POLineNbr, Array.Empty<object>()));
        int? inventoryId = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.InventoryID;
        if (!inventoryId.HasValue && aLine != null)
          inventoryId = aLine.InventoryID;
        if (!inventoryId.HasValue)
          return false;
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, inventoryId);
        if (inventoryItem == null)
          return false;
        POReceiptLine poReceiptLine1 = (POReceiptLine) null;
        int? nullable2 = ((PXSelectBase<POReceipt>) this.Document).Current.VendorID;
        if (!nullable2.HasValue)
        {
          nullable2 = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.VendorID;
          if (nullable2.HasValue)
          {
            POReceipt copy = PXCache<POReceipt>.CreateCopy(((PXSelectBase<POReceipt>) this.Document).Current);
            copy.VendorID = aLine != null ? aLine.VendorID : ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.VendorID;
            copy.VendorLocationID = aLine != null ? aLine.VendorLocationID : ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.VendorLocationID;
            ((PXSelectBase<POReceipt>) this.Document).Update(copy);
          }
        }
        nullable2 = ((PXSelectBase<POReceipt>) this.Document).Current.SiteID;
        if (!nullable2.HasValue)
        {
          nullable2 = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.SiteID;
          if (nullable2.HasValue)
          {
            POReceipt copy = PXCache<POReceipt>.CreateCopy(((PXSelectBase<POReceipt>) this.Document).Current);
            copy.SiteID = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.SiteID;
            ((PXSelectBase<POReceipt>) this.Document).Update(copy);
          }
        }
        if (aLine != null)
        {
          poReceiptLine1 = PXResultset<POReceiptLine>.op_Implicit(PXSelectBase<POReceiptLine, PXSelect<POReceiptLine, Where<POReceiptLine.receiptType, Equal<Current<POReceipt.receiptType>>, And<POReceiptLine.receiptNbr, Equal<Current<POReceipt.receiptNbr>>, And<POReceiptLine.pOType, Equal<Current<POLine.orderType>>, And<POReceiptLine.pONbr, Equal<Current<POLine.orderNbr>>, And<POReceiptLine.pOLineNbr, Equal<Current<POLine.lineNbr>>>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
          {
            (object) aLine
          }, Array.Empty<object>()));
          if (poReceiptLine1 != null)
          {
            nullable2 = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.SiteID;
            if (nullable2.HasValue)
            {
              nullable2 = poReceiptLine1.SiteID;
              int? siteId = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.SiteID;
              if (!(nullable2.GetValueOrDefault() == siteId.GetValueOrDefault() & nullable2.HasValue == siteId.HasValue))
                poReceiptLine1 = (POReceiptLine) null;
            }
          }
          if (poReceiptLine1 == null)
          {
            nullable1 = aLine.BaseOrderQty;
            Decimal? baseReceiptQty = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.BaseReceiptQty;
            if ((!(nullable1.GetValueOrDefault() == baseReceiptQty.GetValueOrDefault() & nullable1.HasValue == baseReceiptQty.HasValue) ? 1 : (aLine.UOM != ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.UOM ? 1 : 0)) != 0)
            {
              aLine.OrderedQtyAltered = new bool?(true);
              aLine.OverridenUOM = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.UOM;
              aLine.OverridenQty = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.ReceiptQty;
              aLine.BaseOverridenQty = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.BaseReceiptQty;
            }
            poReceiptLine1 = this.AddPOLine((POLine) aLine, true);
            if (poReceiptLine1 != null)
              this.AddPOOrderReceipt(aLine.OrderType, aLine.OrderNbr);
          }
        }
        INTran inTran = PXResultset<INTran>.op_Implicit(PXSelectBase<INTran, PXSelect<INTran, Where<INTran.docType, Equal<Required<INTran.docType>>, And<INTran.refNbr, Equal<Required<INTran.refNbr>>, And<INTran.lineNbr, Equal<Required<INTran.lineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
        {
          (object) "T",
          (object) ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.OrigRefNbr,
          (object) ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.OrigLineNbr
        }));
        bool? nullable3;
        if (inTran != null)
        {
          poReceiptLine1 = PXResultset<POReceiptLine>.op_Implicit(PXSelectBase<POReceiptLine, PXSelect<POReceiptLine, Where<POReceiptLine.receiptType, Equal<Required<POReceipt.receiptType>>, And<POReceiptLine.receiptNbr, Equal<Required<POReceipt.receiptNbr>>, And<POReceiptLine.origDocType, Equal<Required<POReceiptLine.origDocType>>, And<POReceiptLine.origRefNbr, Equal<Required<POReceiptLine.origRefNbr>>, And<POReceiptLine.origLineNbr, Equal<Required<POReceiptLine.origLineNbr>>>>>>>>.Config>.Select((PXGraph) this, new object[5]
          {
            (object) ((PXSelectBase<POReceipt>) this.Document).Current.ReceiptType,
            (object) ((PXSelectBase<POReceipt>) this.Document).Current.ReceiptNbr,
            (object) inTran.DocType,
            (object) inTran.RefNbr,
            (object) inTran.LineNbr
          }));
          if (poReceiptLine1 == null)
          {
            poReceiptLine1 = ((PXGraph) this).GetExtension<AddTransferDialog>().AddTransferLine(inTran);
            nullable3 = inventoryItem.StkItem;
            if (nullable3.GetValueOrDefault() && poReceiptLine1 != null && ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.LocationID.HasValue)
            {
              foreach (PXResult<POReceiptLineSplit> pxResult in ((PXSelectBase<POReceiptLineSplit>) this.splits).Select(Array.Empty<object>()))
              {
                POReceiptLineSplit receiptLineSplit = PXResult<POReceiptLineSplit>.op_Implicit(pxResult);
                receiptLineSplit.LocationID = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.LocationID;
                ((PXSelectBase<POReceiptLineSplit>) this.splits).Update(receiptLineSplit);
              }
            }
          }
        }
        if (poReceiptLine1 == null && ((PXSelectBase<POReceipt>) this.Document).Current != null && ((PXSelectBase<POReceipt>) this.Document).Current.ReceiptType == "RX")
          throw new PXException("The record cannot be inserted.");
        Decimal? nullable4;
        if (poReceiptLine1 == null)
        {
          nullable3 = inventoryItem.StkItem;
          string str = !nullable3.GetValueOrDefault() ? "NS" : "GI";
          poReceiptLine1 = PXResultset<POReceiptLine>.op_Implicit(PXSelectBase<POReceiptLine, PXSelect<POReceiptLine, Where<POReceiptLine.receiptType, Equal<Current<POReceipt.receiptType>>, And<POReceiptLine.receiptNbr, Equal<Current<POReceipt.receiptNbr>>, And<POReceiptLine.lineType, Equal<Required<POReceiptLine.lineType>>, And<POReceiptLine.inventoryID, Equal<Required<POReceiptLine.inventoryID>>, And<POReceiptLine.subItemID, Equal<Required<POReceiptLine.subItemID>>, And<POReceiptLine.pOLineNbr, IsNull, And2<Where<Current<POReceiptEntry.POReceiptLineS.siteID>, IsNull, Or<POReceiptLine.siteID, Equal<Current<POReceiptEntry.POReceiptLineS.siteID>>>>, And<Where<Current<POReceiptEntry.POReceiptLineS.unitCost>, Equal<decimal0>, Or<POReceiptLine.unitCost, Equal<Current<POReceiptEntry.POReceiptLineS.unitCost>>>>>>>>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[3]
          {
            (object) str,
            (object) ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.InventoryID,
            (object) ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.SubItemID
          }));
          if (poReceiptLine1 == null)
          {
            POReceiptLine copy1 = PXCache<POReceiptLine>.CreateCopy(((PXSelectBase<POReceiptLine>) this.transactions).Insert(new POReceiptLine()));
            copy1.LineType = str;
            copy1.InventoryID = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.InventoryID;
            copy1.UOM = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.UOM;
            if (((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.SubItemID.HasValue)
              copy1.SubItemID = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.SubItemID;
            if (((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.SiteID.HasValue)
              copy1.SiteID = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.SiteID;
            POReceiptLine copy2 = PXCache<POReceiptLine>.CreateCopy(((PXSelectBase<POReceiptLine>) this.transactions).Update(copy1));
            nullable4 = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.UnitCost;
            Decimal num2 = 0M;
            if (nullable4.GetValueOrDefault() > num2 & nullable4.HasValue)
              copy2.UnitCost = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.UnitCost;
            poReceiptLine1 = PXCache<POReceiptLine>.CreateCopy(((PXSelectBase<POReceiptLine>) this.transactions).Update(copy2));
          }
        }
        string str1 = string.Empty;
        int? nullable5;
        if (poReceiptLine1 != null && inTran == null)
        {
          poReceiptLine1 = PXCache<POReceiptLine>.CreateCopy(poReceiptLine1);
          ((PXSelectBase<POReceiptLine>) this.transactions).Current = poReceiptLine1;
          nullable3 = inventoryItem.StkItem;
          if (nullable3.GetValueOrDefault())
          {
            POReceiptLineSplit receiptLineSplit = new POReceiptLineSplit();
            receiptLineSplit.Qty = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.BaseReceiptQty;
            nullable5 = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.LocationID;
            if (nullable5.HasValue)
              receiptLineSplit.LocationID = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.LocationID;
            if (((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.LotSerialNbr != null)
              receiptLineSplit.LotSerialNbr = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.LotSerialNbr;
            if (((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.ExpireDate.HasValue)
              receiptLineSplit.ExpireDate = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.ExpireDate;
            ((PXSelectBase<POReceiptLineSplit>) this.splits).Insert(receiptLineSplit);
            if (!((PXGraph) this).IsImport)
            {
              nullable3 = ((PXSelectBase<INSetup>) this.insetup).Current.UseInventorySubItem;
              string str2;
              if (nullable3.GetValueOrDefault())
              {
                nullable5 = receiptLineSplit.SubItemID;
                if (nullable5.HasValue)
                {
                  str2 = ":" + ((PXSelectBase<POReceiptLineSplit>) this.splits).GetValueExt<POReceiptLineSplit.subItemID>(receiptLineSplit)?.ToString();
                  goto label_59;
                }
              }
              str2 = string.Empty;
label_59:
              str1 = str2;
            }
          }
          else
          {
            POReceiptLine poReceiptLine2 = poReceiptLine1;
            nullable4 = poReceiptLine2.ReceiptQty;
            nullable1 = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.ReceiptQty;
            poReceiptLine2.ReceiptQty = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
            ((PXSelectBase<POReceiptLine>) this.transactions).Update(poReceiptLine1);
          }
        }
        if (poReceiptLine1 != null)
        {
          if (!((PXGraph) this).IsImport)
          {
            POReceiptEntry.POReceiptLineS current = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current;
            string str3;
            if (aLine == null)
              str3 = PXMessages.LocalizeFormatNoPrefixNLA("Item {0}{1} receipted {2} {3}", new object[4]
              {
                (object) ((PXSelectBase<POReceiptLine>) this.transactions).GetValueExt<POReceiptLine.inventoryID>(poReceiptLine1).ToString().Trim(),
                (object) str1,
                (object) ((PXSelectBase) this.addReceipt).Cache.GetValueExt<POReceiptEntry.POReceiptLineS.receiptQty>((object) ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current).ToString(),
                (object) ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.UOM
              });
            else
              str3 = PXMessages.LocalizeFormatNoPrefixNLA("Item {0}{1} receipted {2} {3} for Purchase Order {4}", new object[5]
              {
                (object) ((PXSelectBase<POReceiptLine>) this.transactions).GetValueExt<POReceiptLine.inventoryID>(poReceiptLine1).ToString().Trim(),
                (object) str1,
                (object) ((PXSelectBase) this.addReceipt).Cache.GetValueExt<POReceiptEntry.POReceiptLineS.receiptQty>((object) ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current).ToString(),
                (object) ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.UOM,
                (object) aLine.OrderNbr
              });
            current.Description = str3;
          }
          if (((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.BarCode != null)
          {
            nullable5 = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.SubItemID;
            if (nullable5.HasValue && PXResultset<INItemXRef>.op_Implicit(PXSelectBase<INItemXRef, PXSelect<INItemXRef, Where<INItemXRef.inventoryID, Equal<Current<POReceiptEntry.POReceiptLineS.inventoryID>>, And<INItemXRef.alternateID, Equal<Current<POReceiptEntry.POReceiptLineS.barCode>>, And<INItemXRef.alternateType, In3<INAlternateType.barcode, INAlternateType.gIN>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>())) == null)
              ((PXSelectBase<INItemXRef>) this.xrefs).Insert(new INItemXRef()
              {
                InventoryID = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.InventoryID,
                AlternateID = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.BarCode,
                AlternateType = "BAR",
                SubItemID = ((PXSelectBase<POReceiptEntry.POReceiptLineS>) this.addReceipt).Current.SubItemID,
                BAccountID = new int?(0)
              });
          }
        }
        return poReceiptLine1 != null;
      }
    }
    return false;
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewPOOrder(PXAdapter adapter)
  {
    if (((PXSelectBase<POReceiptLine>) this.transactions).Current == null)
      return adapter.Get();
    POReceiptLine current = ((PXSelectBase<POReceiptLine>) this.transactions).Current;
    if (string.IsNullOrEmpty(current.POType) || string.IsNullOrEmpty(current.PONbr))
      throw new PXException("This line does not reference PO Order");
    POOrderEntry instance = PXGraph.CreateInstance<POOrderEntry>();
    ((PXSelectBase<POOrder>) instance.Document).Current = PXResultset<POOrder>.op_Implicit(((PXSelectBase<POOrder>) instance.Document).Search<POOrder.orderNbr>((object) current.PONbr, new object[1]
    {
      (object) current.POType
    }));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Document");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable CreateAPDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<POReceipt>) this.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<POReceipt>) this.Document).Current.Released;
      if (nullable.GetValueOrDefault())
      {
        POReceipt current = ((PXSelectBase<POReceipt>) this.Document).Current;
        Decimal? unbilledQty = current.UnbilledQty;
        Decimal num = 0M;
        if (!(unbilledQty.GetValueOrDefault() == num & unbilledQty.HasValue))
        {
          APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
          DocumentList<PX.Objects.AP.APInvoice> list = new DocumentList<PX.Objects.AP.APInvoice>((PXGraph) instance);
          nullable = ((PXSelectBase<APSetup>) this.apsetup).Current.RetainTaxes;
          bool flag = nullable.GetValueOrDefault() && this.HasOrderWithRetainage(current);
          instance.InvoicePOReceipt(current, list, keepOrderTaxes: !flag);
          instance.AttachPrepayment();
          throw new PXRedirectRequiredException((PXGraph) instance, "Enter AP Bill");
        }
        throw new PXException("AP documents are already created for all the lines of this document.");
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable CreateLCDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<POReceipt>) this.Document).Current != null && ((PXSelectBase<POReceipt>) this.Document).Current.Released.GetValueOrDefault())
    {
      POReceipt current = ((PXSelectBase<POReceipt>) this.Document).Current;
      POLandedCostDocEntry instance = PXGraph.CreateInstance<POLandedCostDocEntry>();
      this.InsertNewLandedCostDoc(instance, ((PXSelectBase<POReceipt>) this.Document).Current);
      DocumentList<PX.Objects.AP.APInvoice> documentList = new DocumentList<PX.Objects.AP.APInvoice>((PXGraph) instance);
      List<POReceipt> receipts = new List<POReceipt>()
      {
        current
      };
      instance.AddPurchaseReceipts((IEnumerable<POReceipt>) receipts);
      throw new PXRedirectRequiredException((PXGraph) instance, "Enter Landed Costs");
    }
    return adapter.Get();
  }

  protected virtual void InsertNewLandedCostDoc(POLandedCostDocEntry lcGraph, POReceipt receipt)
  {
    ((PXSelectBase<POLandedCostDoc>) lcGraph.Document).Insert(new POLandedCostDoc());
  }

  public POReceiptEntry()
  {
    APSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) this);
    POSetup current = ((PXSelectBase<POSetup>) this.posetup).Current;
    ((PXGraph) this).Actions.Move("Insert", nameof (Cancel));
    PXGraph.RowUpdatedEvents rowUpdated = ((PXGraph) this).RowUpdated;
    POReceiptEntry poReceiptEntry = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) poReceiptEntry, __vmethodptr(poReceiptEntry, ParentFieldUpdated));
    rowUpdated.AddHandler<POReceipt>(pxRowUpdated);
    ((PXSelectBase) this.poLinesSelection).Cache.AllowInsert = false;
    ((PXSelectBase) this.poLinesSelection).Cache.AllowDelete = false;
    ((PXSelectBase) this.poLinesSelection).Cache.AllowUpdate = true;
    ((PXSelectBase) this.openOrders).Cache.AllowInsert = false;
    ((PXSelectBase) this.openOrders).Cache.AllowDelete = false;
    ((PXSelectBase) this.openOrders).Cache.AllowUpdate = true;
    ((PXSelectBase) this.ReceiptOrdersLink).Cache.AllowInsert = false;
    ((PXSelectBase) this.ReceiptOrdersLink).Cache.AllowDelete = false;
    ((PXSelectBase) this.ReceiptOrdersLink).Cache.AllowUpdate = true;
    bool flag1 = ((PXSelectBase<APSetup>) this.apsetup).Current.RequireVendorRef.Value;
    OpenPeriodAttribute.SetValidatePeriod<POReceipt.finPeriodID>(((PXSelectBase) this.Document).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    PXDefaultAttribute.SetPersistingCheck<POReceipt.invoiceNbr>(((PXSelectBase) this.Document).Cache, (object) null, flag1 ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetRequired<POReceipt.invoiceNbr>(((PXSelectBase) this.Document).Cache, flag1);
    PXDefaultAttribute.SetPersistingCheck<POReceipt.invoiceDate>(((PXSelectBase) this.Document).Cache, (object) null, (PXPersistingCheck) 1);
    PXUIFieldAttribute.SetRequired<POReceipt.invoiceDate>(((PXSelectBase) this.Document).Cache, true);
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.openOrders).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<POReceiptEntry.POOrderS.selected>(((PXSelectBase) this.openOrders).Cache, (object) null, true);
    PXDimensionSelectorAttribute.SetValidCombo<POReceiptLine.subItemID>(((PXSelectBase) this.transactions).Cache, false);
    PXDimensionSelectorAttribute.SetValidCombo<POReceiptLineSplit.subItemID>(((PXSelectBase) this.splits).Cache, false);
    bool flag2 = ProjectAttribute.IsPMVisible("PO");
    PXUIFieldAttribute.SetVisible<POReceiptLine.projectID>(((PXSelectBase) this.transactions).Cache, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<POReceiptLine.taskID>(((PXSelectBase) this.transactions).Cache, (object) null, flag2);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(POReceiptEntry.\u003C\u003Ec.\u003C\u003E9__145_0 ?? (POReceiptEntry.\u003C\u003Ec.\u003C\u003E9__145_0 = new PXFieldDefaulting((object) POReceiptEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__145_0))));
    ((PXGraph) this).Views.Caches.Remove(typeof (POReceiptEntry.POLineS));
    ((PXGraph) this).Views.Caches.Remove(typeof (POReceiptEntry.POOrderS));
    ((PXGraph) this).Views.Caches.Remove(typeof (POReceiptReturn));
    ((PXGraph) this).Views.Caches.Remove(typeof (POReceiptLineReturn));
    PXNoteAttribute.ForcePassThrow<POOrderReceiptLink.orderNoteID>(((PXSelectBase) this.ReceiptOrdersLink).Cache);
  }

  [InjectDependency]
  protected ILicenseLimitsService _licenseLimits { get; set; }

  void IGraphWithInitialization.Initialize()
  {
    if (this._licenseLimits == null)
      return;
    ((PXGraph) this).OnBeforeCommit += this._licenseLimits.GetCheckerDelegate<POReceipt>(new TableQuery[2]
    {
      new TableQuery((TransactionTypes) 108, typeof (POReceiptLine), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[2]
      {
        (PXDataFieldValue) new PXDataFieldValue<POReceiptLine.receiptType>((PXDbType) 3, (object) ((PXSelectBase<POReceipt>) ((POReceiptEntry) graph).Document).Current?.ReceiptType),
        (PXDataFieldValue) new PXDataFieldValue<POReceiptLine.receiptNbr>((object) ((PXSelectBase<POReceipt>) ((POReceiptEntry) graph).Document).Current?.ReceiptNbr)
      })),
      new TableQuery((TransactionTypes) 115, typeof (POReceiptLineSplit), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[2]
      {
        (PXDataFieldValue) new PXDataFieldValue<POReceiptLineSplit.receiptType>((PXDbType) 3, (object) ((PXSelectBase<POReceipt>) ((POReceiptEntry) graph).Document).Current?.ReceiptType),
        (PXDataFieldValue) new PXDataFieldValue<POReceiptLineSplit.receiptNbr>((object) ((PXSelectBase<POReceipt>) ((POReceiptEntry) graph).Document).Current?.ReceiptNbr)
      }))
    });
  }

  protected virtual void POReceipt_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    bool flag1 = ((PXSelectBase<POSetup>) this.posetup).Current.RequireReceiptControlTotal.Value;
    POReceipt row = (POReceipt) e.Row;
    POReceipt oldRow = (POReceipt) e.OldRow;
    if (row == null)
      return;
    if (!flag1)
    {
      if (row.OrderQty.HasValue)
      {
        Decimal? orderQty = row.OrderQty;
        Decimal num = 0M;
        if (!(orderQty.GetValueOrDefault() == num & orderQty.HasValue))
        {
          sender.SetValue<POReceipt.controlQty>((object) row, (object) row.OrderQty);
          goto label_7;
        }
      }
      sender.SetValue<POReceipt.controlQty>((object) row, (object) 0M);
    }
label_7:
    bool flag2 = row.Released.Value;
    bool? hold = row.Hold;
    bool flag3 = false;
    if (hold.GetValueOrDefault() == flag3 & hold.HasValue && !flag2)
    {
      if (flag1)
      {
        Decimal? orderQty = row.OrderQty;
        Decimal? controlQty = row.ControlQty;
        if (!(orderQty.GetValueOrDefault() == controlQty.GetValueOrDefault() & orderQty.HasValue == controlQty.HasValue))
          sender.RaiseExceptionHandling<POReceipt.controlQty>((object) row, (object) row.ControlQty, (Exception) new PXSetPropertyException("Document is out of balance."));
        else
          sender.RaiseExceptionHandling<POReceipt.controlQty>((object) row, (object) row.ControlQty, (Exception) null);
      }
    }
    else
      sender.RaiseExceptionHandling<POReceipt.controlQty>((object) row, (object) null, (Exception) null);
    if (row.ReceiptType == "RN" && oldRow != null && !sender.ObjectsEqual<POReceipt.returnInventoryCostMode>((object) row, (object) oldRow))
    {
      if (row.ReturnInventoryCostMode == "O")
        this.SetOriginalAmountsAllReturnLines(sender, row, oldRow, true);
      else if (oldRow.ReturnInventoryCostMode == "O")
      {
        this.ResetAmountsAllReturnLines(sender, row);
        sender.RaiseFieldUpdated<POReceipt.receiptDate>((object) row, (object) null);
      }
    }
    hold = row.Hold;
    bool flag4 = false;
    if (!(hold.GetValueOrDefault() == flag4 & hold.HasValue) || sender.ObjectsEqual<POReceipt.hold>(e.Row, e.OldRow))
      return;
    foreach (PXResult<POReceiptLine> pxResult in ((PXSelectBase<POReceiptLine>) this.transactions).Select(Array.Empty<object>()))
    {
      POReceiptLine poReceiptLine = PXResult<POReceiptLine>.op_Implicit(pxResult);
      Decimal? receiptQty = poReceiptLine.ReceiptQty;
      Decimal num = 0M;
      if (receiptQty.GetValueOrDefault() <= num & receiptQty.HasValue && ((PXSelectBase) this.transactions).Cache.GetStatus((object) poReceiptLine) == null)
        GraphHelper.MarkUpdated(((PXSelectBase) this.transactions).Cache, (object) poReceiptLine, true);
    }
  }

  protected virtual void POReceipt_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    POReceipt row = (POReceipt) e.Row;
    if (row == null || PXDBOperationExt.Command(e.Operation) == 3 || !(row.ReturnInventoryCostMode == "I"))
      return;
    foreach (PXResult<POReceiptLine> pxResult in ((PXSelectBase<POReceiptLine>) this.transactions).Select(Array.Empty<object>()))
    {
      if (POLineType.IsProjectDropShip(PXResult<POReceiptLine>.op_Implicit(pxResult).LineType))
      {
        ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<POReceipt.returnInventoryCostMode>((object) row, (object) row.ReturnInventoryCostMode, (Exception) new PXSetPropertyException("The Cost by Issue Strategy option cannot be selected because at least one line on the Details tab has been drop-shipped for a project.", (PXErrorLevel) 4));
        break;
      }
    }
  }

  /// <summary>
  /// Updates all the return lines with the original receipt amounts
  /// </summary>
  public virtual void SetOriginalAmountsAllReturnLines(
    PXCache sender,
    POReceipt row,
    POReceipt oldRow,
    bool updateCurencyInfo)
  {
    bool flag1 = false;
    PXResultset<POReceiptLine> pxResultset = PXSelectBase<POReceiptLine, PXViewOf<POReceiptLine>.BasedOn<SelectFromBase<POReceiptLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<POReceiptLine2>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLine2.receiptType, Equal<POReceiptLine.origReceiptType>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLine2.receiptNbr, Equal<POReceiptLine.origReceiptNbr>>>>>.And<BqlOperand<POReceiptLine2.lineNbr, IBqlInt>.IsEqual<POReceiptLine.origReceiptLineNbr>>>>>, FbqlJoins.Left<POReceipt>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceipt.receiptType, Equal<POReceiptLine2.receiptType>>>>>.And<BqlOperand<POReceipt.receiptNbr, IBqlString>.IsEqual<POReceiptLine2.receiptNbr>>>>, FbqlJoins.Left<PX.Objects.CM.Extensions.CurrencyInfo>.On<BqlOperand<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, IBqlLong>.IsEqual<POReceipt.curyInfoID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLine.receiptType, Equal<POReceiptType.poreturn>>>>, And<BqlOperand<POReceiptLine.receiptNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<POReceiptLine.origReceiptNbr, IBqlString>.IsNotNull>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ReceiptNbr
    });
    if (row.ReturnInventoryCostMode == "O")
    {
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = this.GetCurrencyInfo((long?) ((PXSelectBase<POReceipt>) this.Document).Current?.CuryInfoID);
      PX.Objects.CM.Extensions.CurrencyInfo curyA = (PX.Objects.CM.Extensions.CurrencyInfo) null;
      bool flag2 = false;
      bool flag3 = false;
      bool flag4 = false;
      foreach (PXResult<POReceiptLine, POReceiptLine2, POReceipt, PX.Objects.CM.Extensions.CurrencyInfo> pxResult in pxResultset)
      {
        POReceiptLine poReceiptLine = PXResult<POReceiptLine, POReceiptLine2, POReceipt, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
        POReceiptLine2 poReceiptLine2 = PXResult<POReceiptLine, POReceiptLine2, POReceipt, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
        POReceipt poReceipt = PXResult<POReceiptLine, POReceiptLine2, POReceipt, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
        PX.Objects.CM.Extensions.CurrencyInfo curyB = PXResult<POReceiptLine, POReceiptLine2, POReceipt, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
        if (poReceiptLine2.ReceiptNbr == null || poReceipt.ReceiptNbr == null || !curyB.CuryInfoID.HasValue)
          flag2 = true;
        if (currencyInfo.CuryID != curyB.CuryID || curyA != null && !this.IsSameCurrencyInfo(curyA, curyB))
          flag3 = true;
        if (POLineType.IsProjectDropShip(poReceiptLine.LineType))
          flag4 = true;
        if (flag4)
        {
          if (flag2 | flag3)
            break;
        }
        curyA = curyB;
      }
      if (flag4 && flag2 | flag3)
      {
        ((PXSelectBase) this.Document).Cache.SetValue<POReceipt.returnInventoryCostMode>((object) row, (object) oldRow.ReturnInventoryCostMode);
        sender.RaiseExceptionHandling<POReceipt.returnInventoryCostMode>((object) row, (object) oldRow.ReturnInventoryCostMode, (Exception) new PXSetPropertyException("The purchase return includes lines that cannot be processed with the receipt cost and lines that cannot be processed with the cost calculated according to the valuation method. To proceed, select the Manual Cost Input option."));
        return;
      }
      if (flag2)
      {
        ((PXSelectBase) this.Document).Cache.SetValue<POReceipt.returnInventoryCostMode>((object) row, (object) oldRow.ReturnInventoryCostMode);
        sender.RaiseExceptionHandling<POReceipt.returnInventoryCostMode>((object) row, (object) oldRow.ReturnInventoryCostMode, (Exception) new PXSetPropertyException("The original {0} purchase receipt or some of its lines cannot be found. The return cannot be processed with the original cost. Select another option."));
        return;
      }
      if (flag3)
      {
        ((PXSelectBase) this.Document).Cache.SetValue<POReceipt.returnInventoryCostMode>((object) row, (object) oldRow.ReturnInventoryCostMode);
        sender.RaiseExceptionHandling<POReceipt.returnInventoryCostMode>((object) row, (object) oldRow.ReturnInventoryCostMode, (Exception) new PXSetPropertyException("The Original Cost from Receipt option cannot be selected because this return contains lines from receipts with a different currency, currency rate, rate type, or effective date."));
        return;
      }
    }
    foreach (PXResult<POReceiptLine, POReceiptLine2, POReceipt, PX.Objects.CM.Extensions.CurrencyInfo> pxResult in pxResultset)
    {
      POReceiptLine returnLine = PXResult<POReceiptLine, POReceiptLine2, POReceipt, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
      POReceiptLine2 receiptLine = PXResult<POReceiptLine, POReceiptLine2, POReceipt, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
      PX.Objects.CM.Extensions.CurrencyInfo receipt_CuryInfo = PXResult<POReceiptLine, POReceiptLine2, POReceipt, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
      if (updateCurencyInfo && !flag1)
      {
        this.CopyReceiptCurrencyInfoToReturn(receipt_CuryInfo, this.GetCurrencyInfo(row.CuryInfoID));
        flag1 = true;
      }
      this.SetOriginalReceiptAmounts(returnLine, (POReceiptLine) receiptLine);
      ((PXSelectBase) this.transactions).Cache.Update((object) returnLine);
    }
  }

  public virtual void SetOriginalAmountsReturnLine(POReceiptLine row)
  {
    POReceiptLine2 receiptLine = PXResultset<POReceiptLine2>.op_Implicit(PXSelectBase<POReceiptLine2, PXViewOf<POReceiptLine2>.BasedOn<SelectFromBase<POReceiptLine2, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLine2.receiptType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<POReceiptLine2.receiptNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<POReceiptLine2.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) row.OrigReceiptType,
      (object) row.OrigReceiptNbr,
      (object) row.OrigReceiptLineNbr
    }));
    if (receiptLine == null)
      return;
    this.SetOriginalReceiptAmounts(row, (POReceiptLine) receiptLine);
  }

  public virtual void SetOriginalReceiptAmounts(POReceiptLine returnLine, POReceiptLine receiptLine)
  {
    PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = this.MultiCurrencyExt.GetDefaultCurrencyInfo();
    returnLine.AllowEditUnitCost = new bool?(false);
    returnLine.DiscPct = receiptLine.DiscPct;
    Decimal? nullable1;
    Decimal num1;
    if (receiptLine.InventoryID.HasValue)
    {
      num1 = INUnitAttribute.ConvertToBase(((PXSelectBase) this.transactions).Cache, receiptLine.InventoryID, receiptLine.UOM, receiptLine.ReceiptQty.Value, INPrecision.QUANTITY);
    }
    else
    {
      nullable1 = receiptLine.ReceiptQty;
      num1 = nullable1.Value;
    }
    Decimal num2 = num1;
    Decimal num3;
    if (returnLine.InventoryID.HasValue)
    {
      PXCache cache = ((PXSelectBase) this.transactions).Cache;
      int? inventoryId = receiptLine.InventoryID;
      string uom = returnLine.UOM;
      nullable1 = returnLine.ReceiptQty;
      Decimal num4 = nullable1.Value;
      num3 = INUnitAttribute.ConvertToBase(cache, inventoryId, uom, num4, INPrecision.QUANTITY);
    }
    else
    {
      nullable1 = returnLine.ReceiptQty;
      num3 = nullable1.Value;
    }
    Decimal num5 = num3;
    bool flag = num2 == num5;
    Decimal num6 = num5 / num2;
    POReceiptLine poReceiptLine1 = returnLine;
    Decimal? nullable2;
    if (!flag)
    {
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = defaultCurrencyInfo;
      nullable1 = receiptLine.CuryDiscAmt;
      Decimal num7 = num6;
      Decimal valueOrDefault = (nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num7) : new Decimal?()).GetValueOrDefault();
      nullable2 = new Decimal?(currencyInfo.RoundCury(valueOrDefault));
    }
    else
      nullable2 = receiptLine.CuryDiscAmt;
    poReceiptLine1.CuryDiscAmt = nullable2;
    POReceiptLine poReceiptLine2 = returnLine;
    PXCache cache1 = ((PXSelectBase) this.transactions).Cache;
    // ISSUE: variable of a boxed type
    __Boxed<int?> inventoryId1 = (ValueType) receiptLine.InventoryID;
    string uom1 = returnLine.UOM;
    string uom2 = receiptLine.UOM;
    nullable1 = receiptLine.CuryUnitCost;
    Decimal num8 = nullable1.Value;
    Decimal? nullable3 = new Decimal?(INUnitAttribute.ConvertFromTo<POReceiptLine.inventoryID>(cache1, (object) inventoryId1, uom1, uom2, num8, INPrecision.UNITCOST));
    poReceiptLine2.CuryUnitCost = nullable3;
    ((PXSelectBase) this.transactions).Cache.SetValueExt<POReceiptLine.curyUnitCost>((object) returnLine, (object) returnLine.CuryUnitCost);
    POReceiptLine poReceiptLine3 = returnLine;
    nullable1 = receiptLine.TranCostFinal;
    Decimal? nullable4 = receiptLine.ReceiptQty;
    Decimal? nullable5 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / nullable4.GetValueOrDefault()) : new Decimal?();
    poReceiptLine3.TranUnitCost = nullable5;
    nullable4 = returnLine.TranUnitCost;
    if (nullable4.HasValue)
    {
      POReceiptLine poReceiptLine4 = returnLine;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = defaultCurrencyInfo;
      nullable4 = returnLine.TranUnitCost;
      Decimal valueOrDefault = nullable4.GetValueOrDefault();
      Decimal? nullable6 = new Decimal?(Math.Round(currencyInfo.CuryConvCuryRaw(valueOrDefault), 6, MidpointRounding.AwayFromZero));
      poReceiptLine4.CuryTranUnitCost = nullable6;
    }
    POReceiptLine poReceiptLine5 = returnLine;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = defaultCurrencyInfo;
    nullable4 = receiptLine.CuryExtCost;
    Decimal num9 = num6;
    Decimal? nullable7;
    if (!nullable4.HasValue)
    {
      nullable1 = new Decimal?();
      nullable7 = nullable1;
    }
    else
      nullable7 = new Decimal?(nullable4.GetValueOrDefault() * num9);
    nullable1 = nullable7;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    Decimal? nullable8 = new Decimal?(currencyInfo1.RoundCury(valueOrDefault1));
    poReceiptLine5.CuryExtCost = nullable8;
    ((PXSelectBase) this.transactions).Cache.SetValueExt<POReceiptLine.curyExtCost>((object) returnLine, (object) returnLine.CuryExtCost);
    POReceiptLine poReceiptLine6 = returnLine;
    Decimal? nullable9;
    if (!flag)
    {
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = defaultCurrencyInfo;
      nullable1 = receiptLine.TranCostFinal;
      nullable4 = nullable1 ?? receiptLine.TranCost;
      Decimal num10 = num6;
      Decimal? nullable10;
      if (!nullable4.HasValue)
      {
        nullable1 = new Decimal?();
        nullable10 = nullable1;
      }
      else
        nullable10 = new Decimal?(nullable4.GetValueOrDefault() * num10);
      nullable1 = nullable10;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      nullable9 = new Decimal?(currencyInfo2.RoundCury(valueOrDefault2));
    }
    else
    {
      nullable1 = receiptLine.TranCostFinal;
      nullable9 = nullable1 ?? receiptLine.TranCost;
    }
    poReceiptLine6.TranCost = nullable9;
    nullable4 = returnLine.TranCost;
    if (!nullable4.HasValue)
      return;
    POReceiptLine poReceiptLine7 = returnLine;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = defaultCurrencyInfo;
    nullable4 = returnLine.TranCost;
    Decimal valueOrDefault3 = nullable4.GetValueOrDefault();
    Decimal? nullable11 = new Decimal?(currencyInfo3.CuryConvCury(valueOrDefault3));
    poReceiptLine7.CuryTranCost = nullable11;
  }

  public virtual void ResetAmountsAllReturnLines(PXCache sender, POReceipt row)
  {
    if (row.ReceiptType != "RN")
      return;
    foreach (PXResult<POReceiptLine> pxResult in ((PXSelectBase<POReceiptLine>) this.transactions).Select(Array.Empty<object>()))
    {
      POReceiptLine poReceiptLine = PXResult<POReceiptLine>.op_Implicit(pxResult);
      if (poReceiptLine.OrigReceiptNbr != null)
      {
        poReceiptLine.AllowEditUnitCost = new bool?(true);
        poReceiptLine.CuryTranCost = (Decimal?) PXFormulaAttribute.Evaluate<POReceiptLine.curyTranCost>(((PXSelectBase) this.transactions).Cache, (object) poReceiptLine) ?? poReceiptLine.CuryTranCost;
        ((PXSelectBase) this.transactions).Cache.Update((object) poReceiptLine);
      }
    }
  }

  protected virtual void POReceipt_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    POReceipt row = (POReceipt) e.Row;
    if (row == null || !row.Released.GetValueOrDefault())
      return;
    if (!string.IsNullOrEmpty(row.InvtDocType) && !string.IsNullOrEmpty(row.InvtRefNbr))
    {
      row.InventoryDocType = row.InvtDocType;
      row.InventoryRefNbr = row.InvtRefNbr;
    }
    else
    {
      INTran inventoryDoc = this.GetInventoryDoc(row);
      if (inventoryDoc == null)
        return;
      row.InventoryDocType = inventoryDoc.DocType;
      row.InventoryRefNbr = inventoryDoc.RefNbr;
    }
  }

  protected virtual void POReceipt_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    POReceipt row = (POReceipt) e.Row;
    if (row == null)
      return;
    bool? nullable1 = row.Released;
    bool valueOrDefault1 = nullable1.GetValueOrDefault();
    nullable1 = ((PXSelectBase<POSetup>) this.posetup).Current.RequireReceiptControlTotal;
    bool valueOrDefault2 = nullable1.GetValueOrDefault();
    bool flag1 = row.ReceiptType == "RN";
    bool flag2 = row.ReceiptType == "RX";
    bool flag3 = row.ReceiptType == "RT";
    int num1 = row.SiteID.HasValue ? 1 : 0;
    nullable1 = row.IsUnderCorrection;
    int num2;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = row.Canceled;
      num2 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 1;
    bool flag4 = num2 != 0;
    bool flag5 = row.OrigReceiptNbr != null;
    ProjectAttribute.IsPMVisible("PO");
    if (!this._skipUIUpdate)
    {
      PXUIFieldAttribute.SetVisible<POReceipt.curyID>(sender, (object) row, this.AllowNonBaseCurrency());
      PXUIFieldAttribute.SetVisible<POReceipt.projectID>(sender, (object) row, false);
    }
    ((PXSelectBase<POReceiptEntry.POOrderFilter>) this.filter).Current.VendorID = row.VendorID;
    ((PXSelectBase<POReceiptEntry.POOrderFilter>) this.filter).Current.VendorLocationID = row.VendorLocationID;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.poLinesSelection).Cache, (string) null, false);
    nullable1 = row.Released;
    if (nullable1.GetValueOrDefault())
    {
      PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
      sender.AllowDelete = false;
      sender.AllowUpdate = true;
      PXDefaultAttribute.SetPersistingCheck<POReceipt.invoiceNbr>(sender, (object) row, (PXPersistingCheck) 2);
      PXNoteAttribute.ForcePassThrow<POReceipt.noteID>(sender);
      PXNoteAttribute.ForcePassThrow<POReceiptLine.noteID>(((PXSelectBase) this.transactions).Cache);
    }
    else if (!this._skipUIUpdate)
    {
      PXUIFieldAttribute.SetEnabled<POReceipt.autoCreateInvoice>(sender, (object) row, !flag2);
      PXUIFieldAttribute.SetEnabled<POReceipt.projectID>(sender, (object) row, false);
      PXDefaultAttribute.SetPersistingCheck<POReceipt.projectID>(sender, (object) row, (PXPersistingCheck) 2);
      nullable1 = row.AutoCreateInvoice;
      bool valueOrDefault3 = nullable1.GetValueOrDefault();
      PXUIFieldAttribute.SetRequired<POReceipt.invoiceNbr>(sender, valueOrDefault3);
      PXUIFieldAttribute.SetEnabled<POReceipt.invoiceDate>(sender, (object) row, valueOrDefault3);
      PXDefaultAttribute.SetPersistingCheck<POReceipt.invoiceDate>(sender, (object) row, valueOrDefault3 ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
      PXUIFieldAttribute.SetRequired<POReceipt.invoiceDate>(sender, valueOrDefault3);
      int num3;
      if (valueOrDefault3)
      {
        nullable1 = ((PXSelectBase<APSetup>) this.apsetup).Current.RequireVendorRef;
        num3 = nullable1.Value ? 1 : 0;
      }
      else
        num3 = 0;
      bool flag6 = num3 != 0;
      PXDefaultAttribute.SetPersistingCheck<POReceipt.invoiceNbr>(sender, (object) row, flag6 ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
      PXUIFieldAttribute.SetRequired<POReceipt.invoiceNbr>(sender, flag6);
      PXUIFieldAttribute.SetVisible<POReceiptEntry.POOrderS.receivedQty>(((PXSelectBase) this.openOrders).Cache, (object) null, flag1);
      PXUIFieldAttribute.SetVisible<POReceiptEntry.POOrderS.leftToReceiveQty>(((PXSelectBase) this.openOrders).Cache, (object) null, !flag1);
      PXUIFieldAttribute.SetVisible<POReceiptEntry.POLineS.receivedQty>(((PXSelectBase) this.poLinesSelection).Cache, (object) null, flag1);
      PXUIFieldAttribute.SetVisible<POLine.leftToReceiveQty>(((PXSelectBase) this.poLinesSelection).Cache, (object) null, !flag1);
      bool flag7 = this.HasTransactions();
      PXUIFieldAttribute.SetEnabled<POReceipt.receiptType>(sender, (object) row, !flag7);
      PXUIFieldAttribute.SetEnabled<POReceipt.vendorID>(sender, (object) row, !flag7);
      PXUIFieldAttribute.SetEnabled<POReceipt.siteID>(sender, (object) row, !flag7);
      sender.AllowDelete = true;
      sender.AllowUpdate = true;
      PXUIFieldAttribute.SetEnabled<POReceiptEntry.POLineS.selected>(((PXSelectBase) this.poLinesSelection).Cache, (object) null, true);
      string theOnlyTaxCalcMode;
      if (valueOrDefault3 && (theOnlyTaxCalcMode = GraphHelper.RowCast<POOrderReceiptLink>((IEnumerable) ((PXSelectBase<POOrderReceiptLink>) this.ReceiptOrdersLink).SelectWindowed(0, 1, Array.Empty<object>())).FirstOrDefault<POOrderReceiptLink>()?.TaxCalcMode) != null && GraphHelper.RowCast<POOrderReceiptLink>((IEnumerable) ((PXSelectBase<POOrderReceiptLink>) this.ReceiptOrdersLink).Select(Array.Empty<object>())).Any<POOrderReceiptLink>((Func<POOrderReceiptLink, bool>) (_ => _.TaxCalcMode != theOnlyTaxCalcMode)))
        sender.RaiseExceptionHandling<POReceipt.autoCreateInvoice>((object) row, (object) valueOrDefault3, (Exception) new PXSetPropertyException("The purchase receipt contains purchase orders with different tax calculation modes. The bill will be created with the {0} tax calculation mode set by default for the vendor location.", (PXErrorLevel) 2, new object[1]
        {
          (object) PXStringListAttribute.GetLocalizedLabel<PX.Objects.CR.Location.vTaxCalcMode>(((PXSelectBase) this.location).Cache, (object) ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current)
        }));
      else
        sender.RaiseExceptionHandling<POReceipt.autoCreateInvoice>((object) row, (object) valueOrDefault3, (Exception) null);
    }
    int? nullable2 = row.VendorID;
    int num4;
    if (nullable2.HasValue)
    {
      nullable2 = row.VendorLocationID;
      num4 = nullable2.HasValue ? 1 : 0;
    }
    else
      num4 = 0;
    bool flag8 = num4 != 0;
    PXCache cache = ((PXSelectBase) this.transactions).Cache;
    int num5;
    if (((valueOrDefault1 ? 0 : (!flag2 ? 1 : 0)) & (flag8 ? 1 : 0)) != 0)
    {
      nullable1 = row.WMSSingleOrder;
      bool flag9 = false;
      num5 = nullable1.GetValueOrDefault() == flag9 & nullable1.HasValue ? 1 : 0;
    }
    else
      num5 = 0;
    cache.AllowInsert = num5 != 0;
    ((PXSelectBase) this.transactions).Cache.AllowUpdate = flag8;
    ((PXSelectBase) this.transactions).Cache.AllowDelete = !valueOrDefault1;
    if (!this._skipUIUpdate)
    {
      PXUIFieldAttribute.SetEnabled<POReceipt.receiptNbr>(sender, (object) row);
      PXUIFieldAttribute.SetEnabled<POReceipt.receiptType>(sender, (object) row);
      PXUIFieldAttribute.SetVisible<POReceipt.controlQty>(sender, e.Row, valueOrDefault2 | valueOrDefault1);
      int num6;
      if (((flag1 ? 0 : (!valueOrDefault1 ? 1 : 0)) & (flag8 ? 1 : 0)) != 0)
      {
        nullable1 = row.WMSSingleOrder;
        bool flag10 = false;
        if (nullable1.GetValueOrDefault() == flag10 & nullable1.HasValue)
        {
          nullable2 = row.ProjectID;
          num6 = nullable2.HasValue ? 1 : 0;
          goto label_26;
        }
      }
      num6 = 0;
label_26:
      bool flag11 = num6 != 0;
      bool flag12 = ((!flag1 ? 0 : (!valueOrDefault1 ? 1 : 0)) & (flag8 ? 1 : 0)) != 0;
      ((PXAction) this.addPOOrder).SetEnabled(flag11);
      ((PXAction) this.addPOOrderLine).SetEnabled(flag11);
      ((PXAction) this.addPOReceiptLine).SetEnabled(flag11);
      ((PXAction) this.addPOReceiptReturn).SetEnabled(flag12);
      ((PXAction) this.addPOReceiptLineReturn).SetEnabled(flag12);
      int num7;
      if (valueOrDefault1)
      {
        Decimal? unbilledQty = row.UnbilledQty;
        Decimal num8 = 0M;
        if (!(unbilledQty.GetValueOrDefault() == num8 & unbilledQty.HasValue))
        {
          num7 = !flag2 ? 1 : 0;
          goto label_30;
        }
      }
      num7 = 0;
label_30:
      ((PXAction) this.createAPDocument).SetEnabled(num7 != 0);
    }
    this.addLinePopupHandler = flag2 ? (POReceiptEntry.PopupHandler) new POReceiptEntry.AddTransferPopupHandler(this) : (POReceiptEntry.PopupHandler) new POReceiptEntry.AddReceiptPopupHandler(this);
    PXUIFieldAttribute.SetVisible<POReceipt.siteID>(sender, e.Row, flag2);
    PXUIFieldAttribute.SetVisible<POReceipt.vendorID>(sender, e.Row, !flag2);
    PXUIFieldAttribute.SetVisible<POReceipt.vendorLocationID>(sender, e.Row, !flag2);
    PXUIFieldAttribute.SetVisible<POReceipt.autoCreateInvoice>(sender, e.Row, !flag2);
    PXUIFieldAttribute.SetVisible<POReceipt.invoiceNbr>(sender, e.Row, !flag2);
    ((PXAction) this.addPOOrder).SetVisible(flag3);
    ((PXAction) this.addPOOrderLine).SetVisible(flag3);
    ((PXAction) this.addPOReceiptLine).SetVisible(!flag1);
    ((PXAction) this.addPOReceiptReturn).SetVisible(flag1);
    ((PXAction) this.addPOReceiptLineReturn).SetVisible(flag1);
    ((PXAction) this.viewPOOrder).SetVisible(!flag2);
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.ReceiptOrdersLink).Cache, (string) null, !flag2);
    PXUIFieldAttribute.SetVisible<POReceipt.invoiceDate>(sender, e.Row, !flag2);
    PXUIFieldAttribute.SetVisible<POReceipt.unbilledQty>(sender, e.Row, !flag2);
    ((PXSelectBase) this.landedCosts).AllowDelete = false;
    if (!((PXGraph) this).IsImport)
    {
      PXUIFieldAttribute.SetVisible<POReceiptLine.pOType>(((PXGraph) this).Caches[typeof (POReceiptLine)], (object) null, !flag2);
      PXUIFieldAttribute.SetVisible<POReceiptLine.pONbr>(((PXGraph) this).Caches[typeof (POReceiptLine)], (object) null, !flag2);
      PXUIFieldAttribute.SetVisible<POReceiptLine.pOLineNbr>(((PXGraph) this).Caches[typeof (POReceiptLine)], (object) null, !flag2);
      PXUIFieldAttribute.SetVisible<POReceiptLine.reasonCode>(((PXGraph) this).Caches[typeof (POReceiptLine)], (object) null, flag1 | flag5);
      PXUIFieldAttribute.SetVisible<POReceiptLine.allowComplete>(((PXGraph) this).Caches[typeof (POReceiptLine)], (object) null, flag3);
      int num9;
      if (flag3)
      {
        POReceivePutAwaySetup current = ((PXSelectBase<POReceivePutAwaySetup>) this.rpaSetup).Current;
        int num10;
        if (current == null)
        {
          num10 = 0;
        }
        else
        {
          nullable1 = current.VerifyReceiptsBeforeRelease;
          num10 = nullable1.GetValueOrDefault() ? 1 : 0;
        }
        if (num10 != 0)
        {
          num9 = 1;
          goto label_39;
        }
      }
      num9 = this is ReceivePutAway.Host ? 1 : 0;
label_39:
      bool flag13 = num9 != 0;
      PXUIFieldAttribute.SetVisible<POReceiptLine.receivedToDateQty>(((PXGraph) this).Caches[typeof (POReceiptLine)], (object) null, flag13);
      PXUIFieldAttribute.SetVisible<POReceiptLine.allowOpen>(((PXGraph) this).Caches[typeof (POReceiptLine)], (object) null, flag1);
      bool flag14 = flag2 && !(this is ReceivePutAway.Host);
      PXUIFieldAttribute.SetVisible<POReceiptLine.sOOrderType>(((PXGraph) this).Caches[typeof (POReceiptLine)], (object) null, flag2);
      PXUIFieldAttribute.SetVisible<POReceiptLine.sOOrderNbr>(((PXGraph) this).Caches[typeof (POReceiptLine)], (object) null, flag14);
      PXUIFieldAttribute.SetVisible<POReceiptLine.sOOrderLineNbr>(((PXGraph) this).Caches[typeof (POReceiptLine)], (object) null, flag2);
      PXUIFieldAttribute.SetVisible<POReceiptLine.sOShipmentNbr>(((PXGraph) this).Caches[typeof (POReceiptLine)], (object) null, flag14);
      PXUIFieldAttribute.SetVisible<POReceiptLine.selected>(((PXGraph) this).Caches[typeof (POReceiptLine)], (object) null, flag3 & valueOrDefault1 && !flag4);
      PXUIFieldAttribute.SetVisible<POReceiptLine.returnedQty>(((PXGraph) this).Caches[typeof (POReceiptLine)], (object) null, flag3 & valueOrDefault1);
      PXUIFieldAttribute.SetVisible<POReceiptLine.origReceiptNbr>(((PXGraph) this).Caches[typeof (POReceiptLine)], (object) null, flag1);
      PXUIFieldAttribute.SetVisible<POReceiptLineSplit.receivedQty>(((PXGraph) this).Caches[typeof (POReceiptLineSplit)], (object) null, flag13);
    }
    PXUIFieldAttribute.SetVisible<POReceipt.returnInventoryCostMode>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<POReceiptAPDoc.accruedQty>(((PXGraph) this).Caches[typeof (POReceiptAPDoc)], (object) null, valueOrDefault1);
    PXUIFieldAttribute.SetVisible<POReceiptAPDoc.accruedAmt>(((PXGraph) this).Caches[typeof (POReceiptAPDoc)], (object) null, valueOrDefault1);
    PXUIFieldAttribute.SetVisible<POReceiptAPDoc.totalPPVAmt>(((PXGraph) this).Caches[typeof (POReceiptAPDoc)], (object) null, valueOrDefault1);
    ((PXSelectBase) this.receiptHistory).AllowSelect = !flag2;
    ((PXSelectBase) this.apDocs).AllowSelect = !flag2;
  }

  protected virtual void POReceipt_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    POReceipt row = (POReceipt) e.Row;
    this.isDeleting = true;
  }

  protected virtual void POReceipt_VendorLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<POReceipt.branchID>(e.Row);
  }

  [PopupMessage]
  [PXMergeAttributes]
  protected virtual void POReceipt_VendorID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void POReceipt_VendorID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    ((PXSetup<PX.Objects.GL.Branch, Where<BqlOperand<PX.Objects.GL.Branch.bAccountID, IBqlInt>.IsEqual<BqlField<POReceipt.vendorID, IBqlInt>.AsOptional>>>) this.branch).RaiseFieldUpdated(sender, e.Row);
    ((PXSetup<PX.Objects.AP.Vendor, Where<BqlOperand<PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsEqual<BqlField<POReceipt.vendorID, IBqlInt>.AsOptional>>>) this.vendor).RaiseFieldUpdated(sender, e.Row);
    sender.SetDefaultExt<POReceipt.autoCreateInvoice>(e.Row);
    sender.SetDefaultExt<POReceipt.vendorLocationID>(e.Row);
  }

  protected virtual void POReceipt_InvoiceDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    POReceipt row = (POReceipt) e.Row;
    if (row == null)
      return;
    e.NewValue = (object) row.ReceiptDate;
  }

  protected virtual void POReceipt_ProjectID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is POReceipt row))
      return;
    foreach (PXResult<POReceiptLine> pxResult in ((PXSelectBase<POReceiptLine>) this.transactions).Select(Array.Empty<object>()))
    {
      POReceiptLine poReceiptLine = PXResult<POReceiptLine>.op_Implicit(pxResult);
      poReceiptLine.ProjectID = row.ProjectID;
      ((PXSelectBase<POReceiptLine>) this.transactions).Update(poReceiptLine);
    }
    ((PXSelectBase) this.poLinesSelection).Cache.Clear();
    ((PXSelectBase) this.openOrders).Cache.Clear();
  }

  private INTran GetInventoryDoc(POReceipt doc)
  {
    List<INTran> list = GraphHelper.RowCast<INTran>((IEnumerable) PXSelectBase<INTran, PXSelectGroupBy<INTran, Where<INTran.pOReceiptType, Equal<Required<INTran.pOReceiptType>>, And<INTran.pOReceiptNbr, Equal<Required<INTran.pOReceiptNbr>>, And<INTran.tranType, NotIn<Required<INTran.tranType>>>>>, Aggregate<GroupBy<INTran.docType, GroupBy<INTran.refNbr>>>, OrderBy<Asc<INTran.refNbr>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) doc.ReceiptType,
      (object) doc.ReceiptNbr,
      (object) new string[2]{ "CRM", "RET" }
    })).OrderByDescending<INTran, string>((Func<INTran, string>) (t => t.RefNbr)).ThenByDescending<INTran, DateTime?>((Func<INTran, DateTime?>) (t => t.LastModifiedDateTime)).ToList<INTran>();
    string expectedINDocType = doc.ReceiptType == "RN" ? (doc.ReturnInventoryCostMode == "O" ? "A" : "I") : "R";
    return list.FirstOrDefault<INTran>((Func<INTran, bool>) (t => t.DocType == expectedINDocType)) ?? list.FirstOrDefault<INTran>((Func<INTran, bool>) (t => t.DocType == "I"));
  }

  protected virtual void POReceiptLine_ReasonCode_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    POReceiptLine row = (POReceiptLine) e.Row;
    if (row == null || !(row.ReceiptType == "RN"))
      return;
    e.NewValue = (object) ((PXSelectBase<POSetup>) this.posetup).Current.RCReturnReasonCodeID;
  }

  protected object GetValue<Field>(object data) where Field : IBqlField
  {
    return data == null ? (object) null : ((PXGraph) this).Caches[BqlCommand.GetItemType(typeof (Field))].GetValue(data, typeof (Field).Name);
  }

  protected virtual void POReceiptLine_POAccrualAcctID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is POReceiptLine row))
      return;
    if (this.IsAccrualAccountRequired(row))
    {
      if (!this._forceAccrualAcctDefaulting)
      {
        int? nullable;
        if (row.ReceiptType == "RT" && !string.IsNullOrEmpty(row.PONbr) && !string.IsNullOrEmpty(row.POType))
        {
          nullable = row.POLineNbr;
          if (nullable.HasValue)
            goto label_8;
        }
        if (row.ReceiptType == "RN" && !string.IsNullOrEmpty(row.OrigReceiptType) && !string.IsNullOrEmpty(row.OrigReceiptNbr))
        {
          nullable = row.OrigReceiptLineNbr;
          if (!nullable.HasValue)
            goto label_9;
        }
        else
          goto label_9;
label_8:
        ((CancelEventArgs) e).Cancel = true;
        return;
      }
label_9:
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
      INPostClass postclass = INPostClass.PK.Find((PXGraph) this, inventoryItem?.PostClassID);
      if (postclass == null)
        return;
      PX.Objects.AP.Vendor current = ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current;
      PX.Objects.IN.INSite site = PX.Objects.IN.INSite.PK.Find((PXGraph) this, row.SiteID);
      e.NewValue = (object) INReleaseProcess.GetPOAccrualAcctID<INPostClass.pOAccrualAcctID>((PXGraph) this, postclass.POAccrualAcctDefault, inventoryItem, site, postclass, current);
      if (e.NewValue == null)
        return;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      e.NewValue = (object) null;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void POReceiptLine_POAccrualSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is POReceiptLine row))
      return;
    if (this.IsAccrualAccountRequired(row))
    {
      if (!this._forceAccrualAcctDefaulting)
      {
        int? nullable;
        if (row.ReceiptType == "RT" && !string.IsNullOrEmpty(row.PONbr) && !string.IsNullOrEmpty(row.POType))
        {
          nullable = row.POLineNbr;
          if (nullable.HasValue)
            goto label_8;
        }
        if (row.ReceiptType == "RN" && !string.IsNullOrEmpty(row.OrigReceiptType) && !string.IsNullOrEmpty(row.OrigReceiptNbr))
        {
          nullable = row.OrigReceiptLineNbr;
          if (!nullable.HasValue)
            goto label_9;
        }
        else
          goto label_9;
label_8:
        ((CancelEventArgs) e).Cancel = true;
        return;
      }
label_9:
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
      INPostClass postclass = INPostClass.PK.Find((PXGraph) this, inventoryItem?.PostClassID);
      if (postclass == null)
        return;
      try
      {
        PX.Objects.AP.Vendor current = ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current;
        PX.Objects.IN.INSite site = PX.Objects.IN.INSite.PK.Find((PXGraph) this, row.SiteID);
        e.NewValue = (object) INReleaseProcess.GetPOAccrualSubID<INPostClass.pOAccrualSubID>((PXGraph) this, postclass.POAccrualAcctDefault, postclass.POAccrualSubMask, inventoryItem, site, postclass, current);
      }
      catch (PXMaskArgumentException ex)
      {
      }
      if (e.NewValue == null)
        return;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      e.NewValue = (object) null;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  /// <summary>
  /// Sets Expense Account for items with Accrue Cost = true. See implementation in CostAccrual extension.
  /// </summary>
  public virtual void SetExpenseAccount(
    PXCache sender,
    PXFieldDefaultingEventArgs e,
    PX.Objects.IN.InventoryItem item)
  {
  }

  protected virtual void POReceiptLine_ExpenseAcctID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    POReceiptLine row = (POReceiptLine) e.Row;
    if (row == null || !EnumerableExtensions.IsIn<string>(row.ReceiptType, "RT", "RN"))
      return;
    int? nullable1;
    if (row.ReceiptType == "RT" && !string.IsNullOrEmpty(row.PONbr) && !string.IsNullOrEmpty(row.POType))
    {
      nullable1 = row.POLineNbr;
      if (nullable1.HasValue)
        return;
    }
    if (row.ReceiptType == "RN" && !string.IsNullOrEmpty(row.OrigReceiptType) && !string.IsNullOrEmpty(row.OrigReceiptNbr))
    {
      nullable1 = row.OrigReceiptLineNbr;
      if (nullable1.HasValue)
        return;
    }
    switch (row.LineType)
    {
      case "DN":
        ((CancelEventArgs) e).Cancel = true;
        break;
      case "FT":
        PX.Objects.CS.Carrier data = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current.VCarrierID);
        e.NewValue = this.GetValue<PX.Objects.CS.Carrier.freightExpenseAcctID>((object) data) ?? (object) ((PXSelectBase<POSetup>) this.posetup).Current.FreightExpenseAcctID;
        ((CancelEventArgs) e).Cancel = true;
        break;
      default:
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
        if (inventoryItem != null)
        {
          if (row != null)
          {
            bool? nullable2 = inventoryItem.StkItem;
            if (!nullable2.GetValueOrDefault())
            {
              nullable2 = row.AccrueCost;
              if (nullable2.GetValueOrDefault())
              {
                this.SetExpenseAccount(sender, e, inventoryItem);
                goto label_16;
              }
            }
          }
          e.NewValue = (object) this.GetNonStockExpenseAccount(row, inventoryItem);
        }
label_16:
        if (e.NewValue == null)
          break;
        ((CancelEventArgs) e).Cancel = true;
        break;
    }
  }

  public virtual int? GetNonStockExpenseAccount(POReceiptLine row, PX.Objects.IN.InventoryItem item)
  {
    INPostClass postclass;
    if (POLineType.IsNonStock(row.LineType) && !POLineType.IsService(row.LineType) && (postclass = INPostClass.PK.Find((PXGraph) this, item.PostClassID)) != null)
    {
      PX.Objects.IN.INSite site = PX.Objects.IN.INSite.PK.Find((PXGraph) this, row.SiteID);
      try
      {
        PX.Objects.PM.PMProject project;
        PX.Objects.PM.PMTask task;
        PMProjectHelper.TryToGetProjectAndTask((PXGraph) this, (int?) row?.ProjectID, (int?) row?.TaskID, out project, out task);
        return INReleaseProcess.GetAcctID<INPostClass.cOGSAcctID>((PXGraph) this, postclass.COGSAcctDefault, InventoryAccountServiceHelper.Params(item, site, postclass, (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task));
      }
      catch (PXMaskArgumentException ex)
      {
      }
    }
    else if (POLineType.IsNonStock(row.LineType))
      return item.COGSAcctID ?? ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current.VExpenseAcctID;
    return new int?();
  }

  /// <summary>
  /// Sets Expense Subaccount for items with Accrue Cost = true. See implementation in CostAccrual extension.
  /// </summary>
  public virtual object GetExpenseSub(
    PXCache sender,
    PXFieldDefaultingEventArgs e,
    PX.Objects.IN.InventoryItem item)
  {
    return (object) null;
  }

  protected virtual void POReceiptLine_ExpenseSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    POReceiptLine row = (POReceiptLine) e.Row;
    if (row == null || !EnumerableExtensions.IsIn<string>(row.ReceiptType, "RT", "RN"))
      return;
    int? nullable1;
    if (row.ReceiptType == "RT" && !string.IsNullOrEmpty(row.PONbr) && !string.IsNullOrEmpty(row.POType))
    {
      nullable1 = row.POLineNbr;
      if (nullable1.HasValue)
        goto label_6;
    }
    if (row.ReceiptType == "RN" && !string.IsNullOrEmpty(row.OrigReceiptType) && !string.IsNullOrEmpty(row.OrigReceiptNbr))
    {
      nullable1 = row.OrigReceiptLineNbr;
      if (nullable1.HasValue)
        goto label_6;
    }
    switch (row.LineType)
    {
      case "DN":
        return;
      case "FT":
        PX.Objects.CS.Carrier data1 = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current.VCarrierID);
        e.NewValue = this.GetValue<PX.Objects.CS.Carrier.freightExpenseSubID>((object) data1) ?? (object) ((PXSelectBase<POSetup>) this.posetup).Current.FreightExpenseSubID;
        ((CancelEventArgs) e).Cancel = true;
        return;
      default:
        PX.Objects.IN.InventoryItem data2 = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
        if (data2 != null)
        {
          bool? nullable2 = data2.StkItem;
          if (nullable2.GetValueOrDefault())
          {
            e.NewValue = (object) null;
            return;
          }
          INPostClass postclass;
          if (POLineType.IsNonStock(row.LineType) && !POLineType.IsService(row.LineType) && (postclass = INPostClass.PK.Find((PXGraph) this, data2.PostClassID)) != null)
          {
            PX.Objects.IN.INSite site = PX.Objects.IN.INSite.PK.Find((PXGraph) this, row.SiteID);
            try
            {
              int? projectID;
              if (row == null)
              {
                nullable1 = new int?();
                projectID = nullable1;
              }
              else
                projectID = row.ProjectID;
              int? taskID;
              if (row == null)
              {
                nullable1 = new int?();
                taskID = nullable1;
              }
              else
                taskID = row.TaskID;
              PX.Objects.PM.PMProject project;
              ref PX.Objects.PM.PMProject local1 = ref project;
              PX.Objects.PM.PMTask task;
              ref PX.Objects.PM.PMTask local2 = ref task;
              PMProjectHelper.TryToGetProjectAndTask((PXGraph) this, projectID, taskID, out local1, out local2);
              e.NewValue = (object) INReleaseProcess.GetSubID<INPostClass.cOGSSubID>((PXGraph) this, postclass.COGSAcctDefault, postclass.COGSSubMask, InventoryAccountServiceHelper.Params(data2, site, postclass, (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task));
            }
            catch (PXMaskArgumentException ex)
            {
            }
          }
          else
            e.NewValue = (object) null;
          if (POLineType.IsNonStock(row.LineType))
          {
            PX.Objects.EP.EPEmployee data3 = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.userID, Equal<Required<PX.Objects.EP.EPEmployee.userID>>>>.Config>.Select((PXGraph) this, new object[1]
            {
              (object) PXAccess.GetUserID()
            }));
            PX.Objects.CR.Standalone.Location data4 = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelectJoin<PX.Objects.CR.Standalone.Location, InnerJoin<BAccountR, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<PX.Objects.GL.Branch, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<POLine.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
            {
              (object) row.BranchID
            }));
            nullable1 = row.ProjectID;
            PX.Objects.PM.PMProject pmProject = PX.Objects.PM.PMProject.PK.Find((PXGraph) this, nullable1 ?? ProjectDefaultAttribute.NonProject());
            PX.Objects.PM.PMTask pmTask = GraphHelper.RowCast<PX.Objects.PM.PMTask>((IEnumerable) PXSelectBase<PX.Objects.PM.PMTask, PXSelect<PX.Objects.PM.PMTask, Where<PX.Objects.PM.PMTask.projectID, Equal<Required<PX.Objects.PM.PMTask.projectID>>, And<PX.Objects.PM.PMTask.taskID, Equal<Required<PX.Objects.PM.PMTask.taskID>>>>>.Config>.Select((PXGraph) this, new object[2]
            {
              (object) pmProject.ContractID,
              (object) row.TaskID
            })).FirstOrDefault<PX.Objects.PM.PMTask>();
            int? nullable3;
            if (pmTask == null)
            {
              nullable1 = new int?();
              nullable3 = nullable1;
            }
            else
              nullable3 = pmTask.DefaultExpenseSubID;
            int? nullable4 = nullable3;
            POReceipt current1 = ((PXSelectBase<POReceipt>) this.Document).Current;
            PX.Objects.CR.Location current2 = ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current;
            object obj;
            if (row != null)
            {
              nullable2 = data2.StkItem;
              if (!nullable2.GetValueOrDefault())
              {
                nullable2 = row.AccrueCost;
                if (nullable2.GetValueOrDefault())
                {
                  obj = this.GetExpenseSub(sender, e, data2);
                  goto label_32;
                }
              }
            }
            obj = (object) PX.Objects.AP.SubAccountMaskAttribute.MakeSub<APSetup.expenseSubMask>((PXGraph) this, ((PXSelectBase<APSetup>) this.apsetup).Current.ExpenseSubMask, new object[6]
            {
              this.GetValue<PX.Objects.CR.Location.vExpenseSubID>((object) current2),
              e.NewValue ?? this.GetValue<PX.Objects.IN.InventoryItem.cOGSSubID>((object) data2),
              this.GetValue<PX.Objects.EP.EPEmployee.expenseSubID>((object) data3),
              this.GetValue<PX.Objects.CR.Standalone.Location.cMPExpenseSubID>((object) data4),
              (object) pmProject.DefaultExpenseSubID,
              (object) nullable4
            }, new System.Type[6]
            {
              typeof (PX.Objects.CR.Location.vExpenseSubID),
              typeof (PX.Objects.IN.InventoryItem.cOGSSubID),
              typeof (PX.Objects.EP.EPEmployee.expenseSubID),
              typeof (PX.Objects.CR.Standalone.Location.cMPExpenseSubID),
              typeof (PX.Objects.PM.PMProject.defaultExpenseSubID),
              typeof (PX.Objects.PM.PMTask.defaultExpenseSubID)
            });
label_32:
            sender.RaiseFieldUpdating<POReceiptLine.expenseSubID>(e.Row, ref obj);
            e.NewValue = obj;
          }
          else
            e.NewValue = (object) null;
        }
        ((CancelEventArgs) e).Cancel = true;
        return;
    }
label_6:
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void POReceiptLine_InvtMult_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    POReceiptLine row = (POReceiptLine) e.Row;
    if (row == null)
      return;
    e.NewValue = (object) INTranType.InvtMult(row.TranType);
  }

  protected virtual void POReceiptLine_ReceiptQty_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    POReceiptLine row = (POReceiptLine) e.Row;
    if (row == null)
      return;
    e.NewValue = (object) (row.LineType == "FT" ? 1M : 0M);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<POReceiptLine, POReceiptLine.selected> e)
  {
    string inventoryCD;
    if (e.Row != null && ((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POReceiptLine, POReceiptLine.selected>, POReceiptLine, object>) e).NewValue).GetValueOrDefault() && !this.VerifyStockItem((IPOReturnLineSource) e.Row, out inventoryCD))
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POReceiptLine, POReceiptLine.selected>, POReceiptLine, object>) e).NewValue = (object) false;
      throw new PXSetPropertyException("The {0} item cannot be returned with a link to the purchase receipt that had been processed before the stock status of the item has changed. To return the item, add a new line with this item to the purchase return.", new object[1]
      {
        (object) inventoryCD
      });
    }
  }

  protected virtual bool VerifyStockItem(IPOReturnLineSource row, out string inventoryCD)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
    int num;
    if (inventoryItem != null && inventoryItem.IsConverted.GetValueOrDefault() && row.IsStockItem.HasValue)
    {
      bool? isStockItem = row.IsStockItem;
      bool? stkItem = inventoryItem.StkItem;
      num = !(isStockItem.GetValueOrDefault() == stkItem.GetValueOrDefault() & isStockItem.HasValue == stkItem.HasValue) ? 1 : 0;
    }
    else
      num = 0;
    inventoryCD = inventoryItem?.InventoryCD.Trim();
    return num == 0;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<POReceiptLineReturn, POReceiptLineReturn.selected> e)
  {
    string inventoryCD;
    if (e.Row != null && ((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POReceiptLineReturn, POReceiptLineReturn.selected>, POReceiptLineReturn, object>) e).NewValue).GetValueOrDefault() && !this.VerifyStockItem((IPOReturnLineSource) e.Row, out inventoryCD))
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POReceiptLineReturn, POReceiptLineReturn.selected>, POReceiptLineReturn, object>) e).NewValue = (object) false;
      throw new PXSetPropertyException("The {0} item cannot be returned with a link to the purchase receipt that had been processed before the stock status of the item has changed. To return the item, add a new line with this item to the purchase return.", new object[1]
      {
        (object) inventoryCD
      });
    }
  }

  protected virtual void POReceiptLine_ReceiptQty_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row is POReceiptLine row && !row.ManualPrice.GetValueOrDefault())
      sender.SetDefaultExt<POReceiptLine.curyUnitCost>(e.Row);
    if (!(row.ReceiptType == "RN") || row.OrigReceiptNbr == null || !(((PXSelectBase<POReceipt>) this.Document).Current?.ReturnInventoryCostMode == "O"))
      return;
    this.SetOriginalAmountsReturnLine(row);
  }

  [PopupMessage]
  [PXMergeAttributes]
  protected virtual void POReceiptLine_InventoryID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void POReceiptLine_InventoryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.inventoryIDChanging = true;
    sender.SetDefaultExt<POReceiptLine.accrueCost>(e.Row);
    sender.SetDefaultExt<POReceiptLine.unitVolume>(e.Row);
    sender.SetDefaultExt<POReceiptLine.unitWeight>(e.Row);
    sender.SetDefaultExt<POReceiptLine.uOM>(e.Row);
    sender.SetDefaultExt<POReceiptLine.tranDesc>(e.Row);
    sender.SetDefaultExt<POReceiptLine.siteID>(e.Row);
    sender.SetDefaultExt<POReceiptLine.expenseAcctID>(e.Row);
    sender.SetDefaultExt<POReceiptLine.expenseSubID>(e.Row);
    sender.SetDefaultExt<POReceiptLine.pOAccrualAcctID>(e.Row);
    sender.SetDefaultExt<POReceiptLine.pOAccrualSubID>(e.Row);
    sender.SetDefaultExt<POReceiptLine.curyUnitCost>(e.Row);
  }

  protected virtual void POReceiptLine_UOM_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    POReceiptLine row = (POReceiptLine) e.Row;
    if (e.Row == null || this.inventoryIDChanging)
      return;
    row.LastBaseReceivedQty = row.BaseReceiptQty;
    if (e.NewValue == null && (POLineType.IsStock(row.LineType) || POLineType.IsNonStock(row.LineType) && row.InventoryID.HasValue))
      throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[uOM]"
      });
  }

  protected virtual void POReceiptLine_UOM_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    POReceiptLine row1 = (POReceiptLine) e.Row;
    string oldValue = (string) e.OldValue;
    if (this.inventoryIDChanging)
      return;
    Decimal num1 = 0M;
    Decimal valueOrDefault1 = row1.CuryUnitCost.GetValueOrDefault();
    Decimal valueOrDefault2 = row1.CuryTranUnitCost.GetValueOrDefault();
    Decimal? nullable;
    if (row1 != null && !string.IsNullOrEmpty(oldValue) && !string.IsNullOrEmpty(row1.UOM) && row1.UOM != oldValue)
    {
      nullable = row1.LastBaseReceivedQty;
      if (nullable.HasValue)
      {
        nullable = row1.LastBaseReceivedQty;
        if (nullable.Value != 0M)
        {
          PXCache sender1 = sender;
          object row2 = e.Row;
          string uom = row1.UOM;
          nullable = row1.LastBaseReceivedQty;
          Decimal num2 = nullable.Value;
          num1 = INUnitAttribute.ConvertFromBase<POReceiptLine.inventoryID>(sender1, row2, uom, num2, INPrecision.QUANTITY);
        }
      }
      Decimal num3 = INUnitAttribute.ConvertFromBase<POReceiptLine.inventoryID>(sender, e.Row, oldValue, valueOrDefault1, INPrecision.UNITCOST);
      valueOrDefault1 = INUnitAttribute.ConvertToBase<POReceiptLine.inventoryID>(sender, e.Row, row1.UOM, num3, INPrecision.UNITCOST);
      nullable = row1.CuryTranUnitCost;
      if (nullable.HasValue)
      {
        Decimal num4 = INUnitAttribute.ConvertFromBase<POReceiptLine.inventoryID>(sender, e.Row, oldValue, valueOrDefault2, INPrecision.NOROUND);
        valueOrDefault2 = INUnitAttribute.ConvertToBase<POReceiptLine.inventoryID>(sender, e.Row, row1.UOM, num4, INPrecision.NOROUND);
      }
    }
    sender.SetValueExt<POReceiptLine.receiptQty>(e.Row, (object) num1);
    sender.SetValueExt<POReceiptLine.curyUnitCost>(e.Row, (object) valueOrDefault1);
    nullable = row1.CuryTranUnitCost;
    if (!nullable.HasValue)
      return;
    sender.SetValueExt<POReceiptLine.curyTranUnitCost>(e.Row, (object) valueOrDefault2);
    PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = this.MultiCurrencyExt.GetDefaultCurrencyInfo();
    PXCache pxCache = sender;
    object row3 = e.Row;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = defaultCurrencyInfo;
    nullable = row1.CuryTranUnitCost;
    Decimal valueOrDefault3 = nullable.GetValueOrDefault();
    // ISSUE: variable of a boxed type
    __Boxed<Decimal> local = (ValueType) currencyInfo.CuryConvBaseRaw(valueOrDefault3);
    pxCache.SetValueExt<POReceiptLine.tranUnitCost>(row3, (object) local);
  }

  protected virtual void POReceiptLine_AlternateID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if ((POReceiptLine) e.Row == null)
      return;
    sender.SetDefaultExt<POReceiptLine.curyUnitCost>(e.Row);
  }

  protected virtual void POReceiptLine_SiteID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    POReceiptLine row = (POReceiptLine) e.Row;
    int? nullable;
    int num;
    if (row == null)
    {
      num = 1;
    }
    else
    {
      nullable = row.InventoryID;
      num = !nullable.HasValue ? 1 : 0;
    }
    if (num != 0)
      return;
    if (row.ReceiptType == "RT" && !string.IsNullOrEmpty(row.PONbr) && !string.IsNullOrEmpty(row.POType))
    {
      nullable = row.POLineNbr;
      if (nullable.HasValue)
        return;
    }
    if (row.ReceiptType == "RN" && !string.IsNullOrEmpty(row.OrigReceiptType) && !string.IsNullOrEmpty(row.OrigReceiptNbr))
    {
      nullable = row.OrigReceiptLineNbr;
      if (nullable.HasValue)
        return;
    }
    sender.SetDefaultExt<POReceiptLine.expenseAcctID>(e.Row);
    sender.SetDefaultExt<POReceiptLine.expenseSubID>(e.Row);
    sender.SetDefaultExt<POReceiptLine.pOAccrualAcctID>(e.Row);
    sender.SetDefaultExt<POReceiptLine.pOAccrualSubID>(e.Row);
    sender.SetDefaultExt<POReceiptLine.curyUnitCost>(e.Row);
  }

  protected virtual void POReceiptLine_LineType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<POReceiptLine.receiptQty>(e.Row);
    POReceiptLine row = (POReceiptLine) e.Row;
    if (row == null || !e.ExternalCall)
      return;
    int num1 = row.ReceiptType == "RX" ? 1 : 0;
    int? nullable;
    int num2;
    if (!string.IsNullOrEmpty(row.POType) && !string.IsNullOrEmpty(row.PONbr))
    {
      nullable = row.POLineNbr;
      num2 = nullable.HasValue ? 1 : 0;
    }
    else
      num2 = 0;
    bool flag1 = num2 != 0;
    int num3;
    if (!string.IsNullOrEmpty(row.OrigReceiptType) && !string.IsNullOrEmpty(row.OrigReceiptNbr))
    {
      nullable = row.OrigReceiptLineNbr;
      num3 = nullable.HasValue ? 1 : 0;
    }
    else
      num3 = 0;
    bool flag2 = num3 != 0;
    int num4 = flag1 ? 1 : 0;
    if ((num1 | num4 | (flag2 ? 1 : 0)) != 0)
      return;
    nullable = row.ExpenseAcctID;
    if (nullable.HasValue)
    {
      nullable = row.ExpenseSubID;
      if (nullable.HasValue)
        goto label_14;
    }
    if (this.IsExpenseAccountRequired(row))
    {
      sender.SetDefaultExt<POReceiptLine.expenseAcctID>(e.Row);
      sender.SetDefaultExt<POReceiptLine.expenseSubID>(e.Row);
    }
label_14:
    nullable = row.POAccrualAcctID;
    if (nullable.HasValue)
    {
      nullable = row.POAccrualSubID;
      if (nullable.HasValue)
        return;
    }
    if (!this.IsAccrualAccountRequired(row))
      return;
    sender.SetDefaultExt<POReceiptLine.pOAccrualAcctID>(e.Row);
    sender.SetDefaultExt<POReceiptLine.pOAccrualSubID>(e.Row);
  }

  protected virtual void POReceiptLine_CuryUnitCost_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    POReceiptLine row = (POReceiptLine) e.Row;
    if (row == null)
      return;
    bool? nullable = row.AllowEditUnitCost;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.ManualPrice;
      if (!nullable.GetValueOrDefault() && (!(row.ReceiptType == "RN") || row.OrigReceiptNbr == null))
      {
        POLine poLine = (POLine) null;
        POOrder poOrder = (POOrder) null;
        if (row.PONbr != null && row.POType != null && row.POLineNbr.HasValue)
        {
          PXResult<POLine> pxResult = PXResultset<POLine>.op_Implicit(PXSelectBase<POLine, PXSelectReadonly2<POLine, InnerJoin<POOrder, On<POOrder.orderType, Equal<POLine.orderType>, And<POOrder.orderNbr, Equal<POLine.orderNbr>>>>, Where<POLine.orderType, Equal<Required<POLine.orderType>>, And<POLine.orderNbr, Equal<Required<POLine.orderNbr>>, And<POLine.lineNbr, Equal<Required<POLine.lineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
          {
            (object) row.POType,
            (object) row.PONbr,
            (object) row.POLineNbr
          }));
          poOrder = PXResult.Unwrap<POOrder>((object) pxResult);
          poLine = PXResult.Unwrap<POLine>((object) pxResult);
        }
        if (poLine != null && poLine.UOM == row.UOM)
        {
          if (poOrder?.CuryID == ((PXSelectBase<POReceipt>) this.Document).Current.CuryID)
          {
            e.NewValue = (object) poLine.CuryUnitCost.GetValueOrDefault();
            ((CancelEventArgs) e).Cancel = true;
            return;
          }
          if (this.AllowNonBaseCurrency())
          {
            PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = this.MultiCurrencyExt.GetDefaultCurrencyInfo();
            e.NewValue = (object) defaultCurrencyInfo.CuryConvCury(poLine.UnitCost.GetValueOrDefault(), new int?(CommonSetupDecPl.PrcCst));
            ((CancelEventArgs) e).Cancel = true;
            return;
          }
        }
        e.NewValue = (object) this.DefaultUnitCost(sender, row, !this.AllowNonBaseCurrency()).GetValueOrDefault();
        return;
      }
    }
    e.NewValue = (object) row.CuryUnitCost.GetValueOrDefault();
  }

  protected virtual void POReceiptLine_ManualPrice_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is POReceiptLine row))
      return;
    bool? manualPrice = row.ManualPrice;
    bool flag = false;
    if (!(manualPrice.GetValueOrDefault() == flag & manualPrice.HasValue) || !e.ExternalCall)
      return;
    sender.SetDefaultExt<POReceiptLine.curyUnitCost>(e.Row);
  }

  protected virtual void POReceiptLine_CuryUnitCost_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (this.IsNegativeCostStockItem((POReceiptLine) e.Row, (Decimal?) e.NewValue))
      throw new PXSetPropertyException("A value for the Unit Cost must not be negative for Stock Items");
  }

  protected virtual void POReceiptLine_CuryExtCost_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (this.IsNegativeCostStockItem((POReceiptLine) e.Row, (Decimal?) e.NewValue))
      throw new PXSetPropertyException("A value for the Ext. Cost must not be negative for Stock Items");
  }

  protected virtual bool IsNegativeCostStockItem(POReceiptLine row, Decimal? value)
  {
    if (value.HasValue)
    {
      Decimal? nullable = value;
      Decimal num = 0M;
      if (nullable.GetValueOrDefault() < num & nullable.HasValue)
        return EnumerableExtensions.IsNotIn<string>(row.LineType, "NS", "SV", "MC", "FT", "NP", new string[3]
        {
          "NO",
          "NF",
          "NM"
        });
    }
    return false;
  }

  protected virtual void POReceiptLine_AllowComplete_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    POReceiptLine row = (POReceiptLine) e.Row;
    if (string.IsNullOrEmpty(row.POType) || string.IsNullOrEmpty(row.PONbr) || !row.POLineNbr.HasValue)
      return;
    POLine referencedPoLine = this.GetReferencedPOLine(row.POType, row.PONbr, row.POLineNbr);
    if (referencedPoLine != null)
    {
      bool? allowComplete = referencedPoLine.AllowComplete;
      bool valueOrDefault = row.AllowComplete.GetValueOrDefault();
      if (!(allowComplete.GetValueOrDefault() == valueOrDefault & allowComplete.HasValue))
      {
        referencedPoLine.AllowComplete = new bool?(row.AllowComplete.GetValueOrDefault());
        ((PXSelectBase<POLine>) this.poline).Update(referencedPoLine);
      }
    }
    ((PXSelectBase) this.transactions).View.RequestRefresh();
  }

  protected virtual void POReceiptLine_AllowOpen_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    POReceiptLine row = (POReceiptLine) e.Row;
    if (string.IsNullOrEmpty(row.POType) || string.IsNullOrEmpty(row.PONbr) || !row.POLineNbr.HasValue)
      return;
    POLine referencedPoLine = this.GetReferencedPOLine(row.POType, row.PONbr, row.POLineNbr);
    if (referencedPoLine != null)
    {
      bool? allowComplete = referencedPoLine.AllowComplete;
      bool valueOrDefault = row.AllowOpen.GetValueOrDefault();
      if (!(allowComplete.GetValueOrDefault() == valueOrDefault & allowComplete.HasValue))
      {
        referencedPoLine.AllowComplete = new bool?(row.AllowOpen.GetValueOrDefault());
        ((PXSelectBase<POLine>) this.poline).Update(referencedPoLine);
      }
    }
    ((PXSelectBase) this.transactions).View.RequestRefresh();
  }

  protected virtual void POReceiptLine_ProjectID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    int? nullable;
    int num1;
    if (!(e.Row is POReceiptLine row))
    {
      num1 = 1;
    }
    else
    {
      nullable = row.ProjectID;
      num1 = !nullable.HasValue ? 1 : 0;
    }
    if (num1 != 0 || ((PXGraph) this).IsImport)
      return;
    int num2;
    if (row == null)
    {
      num2 = 1;
    }
    else
    {
      nullable = row.InventoryID;
      num2 = !nullable.HasValue ? 1 : 0;
    }
    if (num2 != 0)
      return;
    if (row.ReceiptType == "RT" && !string.IsNullOrEmpty(row.PONbr) && !string.IsNullOrEmpty(row.POType))
    {
      nullable = row.POLineNbr;
      if (nullable.HasValue)
        return;
    }
    if (row.ReceiptType == "RN" && !string.IsNullOrEmpty(row.OrigReceiptType) && !string.IsNullOrEmpty(row.OrigReceiptNbr))
    {
      nullable = row.OrigReceiptLineNbr;
      if (nullable.HasValue)
        return;
    }
    sender.SetDefaultExt<POReceiptLine.expenseAcctID>((object) row);
    sender.SetDefaultExt<POReceiptLine.expenseSubID>((object) row);
  }

  protected virtual void POReceiptLine_TaskID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    int? nullable;
    int num1;
    if (!(e.Row is POReceiptLine row))
    {
      num1 = 1;
    }
    else
    {
      nullable = row.ProjectID;
      num1 = !nullable.HasValue ? 1 : 0;
    }
    if (num1 != 0)
    {
      int num2;
      if (row == null)
      {
        num2 = 1;
      }
      else
      {
        nullable = row.TaskID;
        num2 = !nullable.HasValue ? 1 : 0;
      }
      if (num2 != 0)
        return;
    }
    if (((PXGraph) this).IsImport)
      return;
    int num3;
    if (row == null)
    {
      num3 = 1;
    }
    else
    {
      nullable = row.InventoryID;
      num3 = !nullable.HasValue ? 1 : 0;
    }
    if (num3 != 0)
      return;
    if (row.ReceiptType == "RT" && !string.IsNullOrEmpty(row.PONbr) && !string.IsNullOrEmpty(row.POType))
    {
      nullable = row.POLineNbr;
      if (nullable.HasValue)
        return;
    }
    if (row.ReceiptType == "RN" && !string.IsNullOrEmpty(row.OrigReceiptType) && !string.IsNullOrEmpty(row.OrigReceiptNbr))
    {
      nullable = row.OrigReceiptLineNbr;
      if (nullable.HasValue)
        return;
    }
    sender.SetDefaultExt<POReceiptLine.expenseAcctID>((object) row);
    sender.SetDefaultExt<POReceiptLine.expenseSubID>((object) row);
  }

  protected virtual void POReceiptLine_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is POReceiptLine row))
      return;
    bool flag1 = row.ReceiptType == "RT";
    POReceipt current1 = ((PXSelectBase<POReceipt>) this.Document).Current;
    bool flag2 = current1 != null && current1.WMSSingleOrder.GetValueOrDefault();
    bool flag3 = ((PXSelectBase<POReceipt>) this.Document).Current?.ReturnInventoryCostMode == "O";
    int? nullable1;
    int num1;
    if (!string.IsNullOrEmpty(row.POType) && !string.IsNullOrEmpty(row.PONbr))
    {
      nullable1 = row.POLineNbr;
      num1 = nullable1.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag4 = num1 != 0;
    POReceipt current2 = ((PXSelectBase<POReceipt>) this.Document).Current;
    int num2;
    if ((current2 != null ? (current2.IsUnderCorrection.GetValueOrDefault() ? 1 : 0) : 0) == 0)
    {
      POReceipt current3 = ((PXSelectBase<POReceipt>) this.Document).Current;
      num2 = current3 != null ? (current3.Canceled.GetValueOrDefault() ? 1 : 0) : 0;
    }
    else
      num2 = 1;
    bool flag5 = num2 != 0;
    if (!row.Released.GetValueOrDefault())
    {
      bool flag6 = POLineType.IsStock(row.LineType);
      bool flag7 = POLineType.IsNonStock(row.LineType);
      bool flag8 = POLineType.UsePOAccrual(row.LineType);
      bool flag9 = row.ReceiptType == "RN";
      bool flag10 = row.OrigReceiptNbr != null;
      bool flag11 = row.ReceiptType == "RX";
      bool flag12 = row.LineType == "FT";
      bool flag13 = row.LineType == "GP";
      bool? nullable2 = row.IsStockItem;
      bool flag14 = (nullable2.HasValue ? new bool?(!nullable2.GetValueOrDefault()) : new bool?()).GetValueOrDefault() && row.IsKit.GetValueOrDefault();
      bool flag15 = row.POAccrualType == "R";
      int num3;
      if (!string.IsNullOrEmpty(row.OrigReceiptType) && !string.IsNullOrEmpty(row.OrigReceiptNbr))
      {
        nullable1 = row.OrigReceiptLineNbr;
        num3 = nullable1.HasValue ? 1 : 0;
      }
      else
        num3 = 0;
      bool flag16 = num3 != 0;
      PXUIFieldAttribute.SetEnabled<POReceiptLine.branchID>(sender, (object) row, !flag11 && (flag15 || !flag8) && !flag2);
      PXUIFieldAttribute.SetEnabled<POReceiptLine.uOM>(sender, (object) row, !flag11 && (flag6 || row.LineType == "NS"));
      PXUIFieldAttribute.SetEnabled<POReceiptLine.inventoryID>(sender, (object) row, !flag11 && !flag4 && !flag12 && !flag16);
      PXUIFieldAttribute.SetEnabled<POReceiptLine.lineType>(sender, (object) row, !flag11 && !flag4 && !flag16);
      PXUIFieldAttribute.SetEnabled<POReceiptLine.siteID>(sender, (object) row, !flag11 && !flag2);
      PXUIFieldAttribute.SetEnabled<POReceiptLine.subItemID>(sender, (object) row, flag6 && !flag14 && !flag11);
      PXUIFieldAttribute.SetEnabled<POReceiptLine.receiptQty>(sender, (object) row, flag6 | flag7);
      PXUIFieldAttribute.SetEnabled<POReceiptLine.locationID>(sender, e.Row, flag6 && !flag13 || POLineType.IsNonStockNonServiceNonDropShip(row.LineType));
      PXUIFieldAttribute.SetEnabled<POReceiptLine.allowComplete>(sender, e.Row, flag4);
      PXUIFieldAttribute.SetEnabled<POReceiptLine.allowOpen>(sender, e.Row, flag4);
      if (flag13)
      {
        PXUIFieldAttribute.SetEnabled<POReceiptLine.lotSerialNbr>(sender, e.Row, false);
        PXUIFieldAttribute.SetEnabled<POReceiptLine.expireDate>(sender, e.Row, false);
      }
      if (flag9)
      {
        PXUIFieldAttribute.SetEnabled<POReceiptLine.expenseAcctID>(sender, e.Row, flag7);
        PXUIFieldAttribute.SetEnabled<POReceiptLine.expenseSubID>(sender, e.Row, flag7);
      }
      else
      {
        PXUIFieldAttribute.SetEnabled<POReceiptLine.expenseAcctID>(sender, e.Row, this.IsExpenseAccountRequired(row));
        PXUIFieldAttribute.SetEnabled<POReceiptLine.expenseSubID>(sender, e.Row, this.IsExpenseAccountRequired(row));
      }
      PXUIFieldAttribute.SetEnabled<POReceiptLine.pOType>(sender, e.Row, false);
      PXUIFieldAttribute.SetEnabled<POReceiptLine.pONbr>(sender, e.Row, false);
      PXUIFieldAttribute.SetEnabled<POReceiptLine.pOLineNbr>(sender, e.Row, false);
      PXUIFieldAttribute.SetEnabled<POReceiptLine.reasonCode>(sender, e.Row, flag9 | flag10);
      PXUIFieldAttribute.SetRequired<POReceiptLine.reasonCode>(sender, flag9);
      PXUIFieldAttribute.SetEnabled<POReceiptLine.pOAccrualAcctID>(sender, e.Row, flag15 && this.IsAccrualAccountRequired(row));
      PXUIFieldAttribute.SetEnabled<POReceiptLine.pOAccrualSubID>(sender, e.Row, flag15 && this.IsAccrualAccountRequired(row));
      PXUIFieldAttribute.SetEnabled<POReceiptLine.sOOrderType>(sender, e.Row, false);
      PXUIFieldAttribute.SetEnabled<POReceiptLine.sOOrderNbr>(sender, e.Row, false);
      PXUIFieldAttribute.SetEnabled<POReceiptLine.sOOrderLineNbr>(sender, e.Row, false);
      nullable2 = row.AllowEditUnitCost;
      bool flag17 = nullable2.GetValueOrDefault() && (flag1 || flag9 && (!flag16 || !flag3));
      PXUIFieldAttribute.SetEnabled<POReceiptLine.manualPrice>(sender, e.Row, flag17);
      PXUIFieldAttribute.SetEnabled<POReceiptLine.curyUnitCost>(sender, e.Row, flag17 && !flag12);
      PXUIFieldAttribute.SetEnabled<POReceiptLine.curyExtCost>(sender, e.Row, flag17);
      PXUIFieldAttribute.SetEnabled<POReceiptLine.selected>(sender, e.Row, false);
    }
    else
    {
      PXUIFieldAttribute.SetEnabled(sender, e.Row, false);
      if (flag1)
        PXUIFieldAttribute.SetEnabled<POReceiptLine.selected>(sender, e.Row, !flag5);
    }
    string inventoryCD;
    if (sender.GetStatus((object) row) != 2 || this.VerifyStockItem((IPOReturnLineSource) row, out inventoryCD))
      return;
    sender.RaiseExceptionHandling<POReceiptLine.inventoryID>((object) row, (object) inventoryCD, (Exception) new PXSetPropertyException("The {0} item cannot be returned with a link to the purchase receipt that had been processed before the stock status of the item has changed. To return the item, add a new line with this item to the purchase return.", new object[1]
    {
      (object) inventoryCD
    }));
  }

  [Obsolete("The method is obsolete and will be removed in the later Acumatica versions.")]
  protected virtual void POReceiptLineReturn_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelecting<POReceiptLine> e)
  {
    int num1;
    if (!string.IsNullOrEmpty(e.Row?.POType) && !string.IsNullOrEmpty(e.Row?.PONbr))
    {
      POReceiptLine row = e.Row;
      num1 = row != null ? (row.POLineNbr.HasValue ? 1 : 0) : 0;
    }
    else
      num1 = 0;
    if (num1 != 0)
    {
      POLine referencedPoLine = this.GetReferencedPOLine(e.Row.POType, e.Row.PONbr, e.Row.POLineNbr);
      POReceiptLine row1 = e.Row;
      POReceiptLine row2 = e.Row;
      bool? nullable1;
      ref bool? local = ref nullable1;
      bool? nullable2 = e.Row.Released;
      int num2;
      if (!nullable2.GetValueOrDefault())
      {
        bool? nullable3;
        if (referencedPoLine == null)
        {
          nullable2 = new bool?();
          nullable3 = nullable2;
        }
        else
          nullable3 = referencedPoLine.AllowComplete;
        nullable2 = nullable3;
        num2 = nullable2.GetValueOrDefault() ? 1 : 0;
      }
      else if (!(e.Row.ReceiptType != "RN"))
      {
        if (referencedPoLine == null)
        {
          num2 = 1;
        }
        else
        {
          nullable2 = referencedPoLine.Completed;
          num2 = !nullable2.GetValueOrDefault() ? 1 : 0;
        }
      }
      else if (referencedPoLine == null)
      {
        num2 = 0;
      }
      else
      {
        nullable2 = referencedPoLine.Completed;
        num2 = nullable2.GetValueOrDefault() ? 1 : 0;
      }
      local = new bool?(num2 != 0);
      bool? nullable4 = nullable1;
      row2.AllowOpen = nullable4;
      bool? nullable5 = nullable1;
      row1.AllowComplete = nullable5;
    }
    POReceiptLine row3 = e.Row;
    if ((row3 != null ? (row3.Released.GetValueOrDefault() ? 1 : 0) : 0) == 0)
    {
      POReceiptLine row4 = e.Row;
      if ((row4 != null ? (row4.IsCorrection.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        return;
    }
    this.PopulateReturnedQty((IPOReturnLineSource) e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<POReceiptLineReturn> e)
  {
    if (e.Row == null)
      return;
    this.PopulateReturnedQty((IPOReturnLineSource) e.Row);
  }

  public virtual void PopulateReturnedQty(IPOReturnLineSource row)
  {
    Decimal valueOrDefault = row.BaseReturnedQty.GetValueOrDefault();
    row.ReturnedQty = new Decimal?(valueOrDefault == 0M ? 0M : INUnitAttribute.ConvertFromBase<POReceiptLine.inventoryID, POReceiptLine.uOM>(((PXGraph) this).Caches[row.GetType()], (object) row, valueOrDefault, INPrecision.QUANTITY));
  }

  public virtual void VerifyTransferLine(PXCache sender, POReceiptLine row)
  {
    if (row.ReceiptType != "RX" || ((PXSelectBase<POReceipt>) this.Document).Current.Released.GetValueOrDefault())
      return;
    POReceiptLine poReceiptLine = PXResultset<POReceiptLine>.op_Implicit(PXSelectBase<POReceiptLine, PXSelectReadonly2<POReceiptLine, InnerJoin<POReceipt, On<POReceiptLine.FK.Receipt>>, Where<POReceiptLine.receiptType, Equal<Current<POReceiptLine.receiptType>>, And<POReceiptLine.receiptNbr, NotEqual<Current<POReceiptLine.receiptNbr>>, And<POReceipt.released, NotEqual<True>, And<POReceiptLine.origRefNbr, Equal<Current<POReceiptLine.origRefNbr>>, And<POReceiptLine.origLineNbr, Equal<Current<POReceiptLine.origLineNbr>>>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    if (poReceiptLine != null)
      throw new PXRowPersistingException(typeof (POReceiptLine.lineNbr).Name, (object) row.LineNbr, "The item in this transfer line is already received according to the '{1}' line of the '{0}' PO receipt.", new object[2]
      {
        (object) poReceiptLine.ReceiptNbr,
        (object) poReceiptLine.LineNbr
      });
    INTran inTran = PXResultset<INTran>.op_Implicit(PXSelectBase<INTran, PXSelectReadonly<INTran, Where<INTran.docType, Equal<INDocType.receipt>, And2<Where<INTran.pOReceiptType, NotEqual<Current<POReceiptLine.receiptType>>, Or<INTran.pOReceiptNbr, NotEqual<Current<POReceiptLine.receiptNbr>>, Or<INTran.pOReceiptLineNbr, NotEqual<Current<POReceiptLine.lineNbr>>>>>, And<INTran.origRefNbr, Equal<Current<POReceiptLine.origRefNbr>>, And<INTran.origLineNbr, Equal<Current<POReceiptLine.origLineNbr>>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    if (inTran != null)
      throw new PXRowPersistingException(typeof (POReceiptLine.lineNbr).Name, (object) row.LineNbr, "The item in this transfer line is already received according to the '{1}' line of the '{0}' IN transaction.", new object[2]
      {
        (object) inTran.RefNbr,
        (object) inTran.LineNbr
      });
  }

  protected virtual bool IsStockItem(POReceiptLine row)
  {
    if (row == null || row.LineType == null)
      return false;
    return row.LineType == "GI" || row.LineType == "GS" || row.LineType == "GF" || row.LineType == "GR" || row.LineType == "GM" || row.LineType == "GP";
  }

  public virtual bool AllowNonBaseCurrency()
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return false;
    return ((PXSelectBase<POReceipt>) this.Document).Current?.ReceiptType == "RT" || ((PXSelectBase<POReceipt>) this.Document).Current?.ReceiptType == "RN";
  }

  public virtual bool HasTransactions()
  {
    return ((PXSelectBase<POReceiptLine>) this.transactions).SelectWindowed(0, 1, Array.Empty<object>()).Count > 0;
  }

  protected virtual void POReceiptLine_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    POReceiptLine row1 = (POReceiptLine) e.Row;
    if (row1 == null || PXDBOperationExt.Command(e.Operation) == 3)
      return;
    bool flag1 = row1.ReceiptType == "RN";
    bool flag2 = this.IsStockItem(row1);
    PXDefaultAttribute.SetPersistingCheck<POReceiptLine.inventoryID>(sender, (object) row1, POLineType.IsStock(row1.LineType) || POLineType.IsNonStock(row1.LineType) && !POLineType.IsService(row1.LineType) ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<POReceiptLine.receiptQty>(sender, (object) row1, flag2 || row1.LineType == "NS" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<POReceiptLine.baseReceiptQty>(sender, (object) row1, flag2 || row1.LineType == "NS" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<POReceiptLine.expenseAcctID>(sender, e.Row, !this.IsExpenseAccountRequired(row1) || row1.LineType == "NS" & flag1 ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXDefaultAttribute.SetPersistingCheck<POReceiptLine.expenseSubID>(sender, e.Row, !this.IsExpenseAccountRequired(row1) || row1.LineType == "NS" & flag1 ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXDefaultAttribute.SetPersistingCheck<POReceiptLine.pOAccrualAcctID>(sender, e.Row, this.IsAccrualAccountRequired(row1) ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<POReceiptLine.pOAccrualSubID>(sender, e.Row, this.IsAccrualAccountRequired(row1) ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<POReceiptLine.siteID>(sender, (object) row1, row1.LineType == "DN" || row1.LineType == "FT" || POLineType.IsService(row1.LineType) ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXCache pxCache = sender;
    object row2 = e.Row;
    int? nullable;
    int num;
    if (!POLineType.IsStock(row1.LineType))
    {
      if (POLineType.IsNonStock(row1.LineType))
      {
        nullable = row1.InventoryID;
        if (nullable.HasValue)
          goto label_5;
      }
      num = 2;
      goto label_6;
    }
label_5:
    num = 1;
label_6:
    PXDefaultAttribute.SetPersistingCheck<POReceiptLine.uOM>(pxCache, row2, (PXPersistingCheck) num);
    PXDefaultAttribute.SetPersistingCheck<POReceiptLine.reasonCode>(sender, e.Row, flag1 ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
    this.ValidateUomConversionNotAdjusted(row1);
    this.ValidateReceiptQty(row1);
    if (!string.IsNullOrEmpty(row1.POType) && !string.IsNullOrEmpty(row1.PONbr))
    {
      nullable = row1.LineNbr;
      if (nullable.HasValue)
        this.CheckRctForPOQuantityRule(sender, row1, false);
    }
    this.CheckReturnQty(sender, row1);
  }

  protected virtual void ValidateReceiptQty(POReceiptLine line)
  {
    if (line == null)
      return;
    bool flag1 = line.IsIntercompany.GetValueOrDefault() && line.ReceiptType == "RT";
    bool flag2 = line.OrigReceiptType == "RT" && line.ReceiptType == "RT";
    (bool shouldVerify, PXErrorLevel errorLevel) = this.ShouldVerifyZeroQty(((PXSelectBase<POReceipt>) this.Document).Current);
    if (!shouldVerify)
      return;
    Decimal? receiptQty;
    if (!flag1 && !flag2)
    {
      receiptQty = line.ReceiptQty;
      Decimal num = 0M;
      if (receiptQty.GetValueOrDefault() <= num & receiptQty.HasValue)
        goto label_6;
    }
    receiptQty = line.ReceiptQty;
    Decimal num1 = 0M;
    if (!(receiptQty.GetValueOrDefault() < num1 & receiptQty.HasValue))
      return;
label_6:
    ((PXCache) GraphHelper.Caches<POReceiptLine>((PXGraph) this)).RaiseExceptionHandling<POReceiptLine.receiptQty>((object) line, (object) line.ReceiptQty, (Exception) new PXSetPropertyException((IBqlTable) line, "Quantity must be greater than 0", errorLevel));
  }

  protected virtual void ValidateReceiptQtyOnRelease()
  {
    foreach (PXResult<POReceiptLine> pxResult in ((PXSelectBase<POReceiptLine>) this.transactions).Select(Array.Empty<object>()))
      this.ValidateLineReceiptQtyOnRelease(PXResult<POReceiptLine>.op_Implicit(pxResult));
  }

  protected virtual (bool shouldVerify, PXErrorLevel errorLevel) ShouldVerifyZeroQty(
    POReceipt receipt)
  {
    if (receipt == null)
      return (false, (PXErrorLevel) 0);
    if (receipt.Hold.GetValueOrDefault())
      return (false, (PXErrorLevel) 0);
    if (receipt.Received.GetValueOrDefault() || AllowZeroReceiptQtyScope.IsActive(receipt))
    {
      POReceivePutAwaySetup current = ((PXSelectBase<POReceivePutAwaySetup>) this.rpaSetup).Current;
      if ((current != null ? (current.KeepZeroLinesOnReceiptConfirmation.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        return (true, (PXErrorLevel) 2);
    }
    return (true, (PXErrorLevel) 4);
  }

  protected virtual void CheckReturnQty(PXCache sender, POReceiptLine row)
  {
    if (!(row.ReceiptType == "RN") || string.IsNullOrEmpty(row.OrigReceiptType) || string.IsNullOrEmpty(row.OrigReceiptNbr) || !row.OrigReceiptLineNbr.HasValue)
      return;
    Decimal? baseReceiptQty = row.BaseReceiptQty;
    Decimal? baseOrigQty = row.BaseOrigQty;
    if (!(baseReceiptQty.GetValueOrDefault() > baseOrigQty.GetValueOrDefault() & baseReceiptQty.HasValue & baseOrigQty.HasValue))
      return;
    sender.RaiseExceptionHandling<POReceiptLine.receiptQty>((object) row, (object) row.ReceiptQty, (Exception) new PXSetPropertyException("The Returned Qty. exceeds the Received Qty. in the {0} line of the originating PO receipt.", (PXErrorLevel) 4, new object[1]
    {
      PXForeignSelectorAttribute.GetValueExt<POReceiptLine.inventoryID>(sender, (object) row)
    }));
  }

  protected virtual bool IsExpenseAccountRequired(POReceiptLine line)
  {
    if (!(line.LineType != "DN"))
      return false;
    return !POLineType.IsStock(line.LineType) || POLineType.IsProjectDropShip(line.LineType);
  }

  protected virtual bool IsAccrualAccountRequired(POReceiptLine line)
  {
    return line.ReceiptType != "RX" && POLineType.UsePOAccrual(line.LineType) && line.DropshipExpenseRecording != "B";
  }

  protected virtual void POReceiptLine_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    POReceiptLine row = (POReceiptLine) e.Row;
    POReceiptLine oldRow = (POReceiptLine) e.OldRow;
    if (((PXGraph) this).IsCopyPasteContext && row.LineType == "GP")
      row.LineType = "GI";
    this.ClearUnused(row);
    POLine aOriginLine = POLine.PK.Find((PXGraph) this, row.POType, row.PONbr, row.POLineNbr);
    Decimal? baseReceiptQty1 = row.BaseReceiptQty;
    Decimal? baseReceiptQty2 = oldRow.BaseReceiptQty;
    if (!(baseReceiptQty1.GetValueOrDefault() == baseReceiptQty2.GetValueOrDefault() & baseReceiptQty1.HasValue == baseReceiptQty2.HasValue))
      this.UpdatePOLineCompleteFlag(row, false, aOriginLine);
    if (!((PXGraph) this).IsImport)
    {
      PXSelectJoin<POReceipt, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<POReceipt.vendorID>>>, Where<POReceipt.receiptType, Equal<Optional<POReceipt.receiptType>>, And<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>> document = this.Document;
      if ((document != null ? (((bool?) ((PXSelectBase<POReceipt>) document).Current?.Received).GetValueOrDefault() ? 1 : 0) : 0) != 0)
        this.ValidateReceiptQty(row);
    }
    try
    {
      this.CheckRctForPOQuantityRule(sender, row, true, aOriginLine);
    }
    finally
    {
      this.inventoryIDChanging = false;
    }
    if (!e.ExternalCall && !sender.Graph.IsImport || !sender.ObjectsEqual<POReceiptLine.inventoryID>(e.Row, e.OldRow) || !sender.ObjectsEqual<POReceiptLine.subItemID>(e.Row, e.OldRow) || !sender.ObjectsEqual<POReceiptLine.alternateID>(e.Row, e.OldRow) || !sender.ObjectsEqual<POReceiptLine.uOM>(e.Row, e.OldRow) || !sender.ObjectsEqual<POReceiptLine.receiptQty>(e.Row, e.OldRow) || !sender.ObjectsEqual<POReceiptLine.branchID>(e.Row, e.OldRow) || !sender.ObjectsEqual<POReceiptLine.siteID>(e.Row, e.OldRow) || !sender.ObjectsEqual<POReceiptLine.manualPrice>(e.Row, e.OldRow) || sender.ObjectsEqual<POReceiptLine.unitCost>(e.Row, e.OldRow) && sender.ObjectsEqual<POReceiptLine.curyUnitCost>(e.Row, e.OldRow) && sender.ObjectsEqual<POReceiptLine.curyExtCost>(e.Row, e.OldRow))
      return;
    sender.SetValueExt<POReceiptLine.manualPrice>((object) row, (object) true);
  }

  protected virtual void POReceiptLine_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    this.ChangeCopyPasteLineType(e.Row as POReceiptLine);
  }

  protected virtual void POReceiptLine_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    this.ChangeCopyPasteLineType(e.NewRow as POReceiptLine);
  }

  protected virtual void ChangeCopyPasteLineType(POReceiptLine line)
  {
    if (!((PXGraph) this).IsCopyPasteContext)
      return;
    string lineType = line?.LineType;
    if (lineType == null || lineType.Length != 2)
      return;
    switch (lineType[1])
    {
      case 'F':
        switch (lineType)
        {
          case "GF":
            break;
          case "NF":
            goto label_21;
          default:
            return;
        }
        break;
      case 'G':
        if (!(lineType == "PG"))
          return;
        break;
      case 'H':
        return;
      case 'I':
        return;
      case 'J':
        return;
      case 'K':
        return;
      case 'L':
        return;
      case 'M':
        switch (lineType)
        {
          case "GM":
            break;
          case "NM":
            goto label_21;
          default:
            return;
        }
        break;
      case 'N':
        if (!(lineType == "PN"))
          return;
        goto label_21;
      case 'O':
        if (!(lineType == "NO"))
          return;
        goto label_21;
      case 'P':
        switch (lineType)
        {
          case "GP":
            break;
          case "NP":
            goto label_21;
          default:
            return;
        }
        break;
      case 'Q':
        return;
      case 'R':
        if (!(lineType == "GR"))
          return;
        break;
      case 'S':
        if (!(lineType == "GS"))
          return;
        break;
      default:
        return;
    }
    line.LineType = "GI";
    return;
label_21:
    line.LineType = "NS";
  }

  protected virtual void POReceiptLine_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    POReceiptLine row = (POReceiptLine) e.Row;
    this.ClearUnused(row);
    POLine aOriginLine = (POLine) null;
    if (row.PONbr != null && row.POType != null && row.POLineNbr.HasValue)
    {
      aOriginLine = POLine.PK.Find((PXGraph) this, row.POType, row.PONbr, row.POLineNbr);
      this.UpdatePOLineCompleteFlag(row, false, aOriginLine);
    }
    if (aOriginLine == null)
    {
      Decimal? unitCost = row.UnitCost;
      if (unitCost.HasValue)
      {
        unitCost = row.UnitCost;
        Decimal num = 0M;
        if (!(unitCost.GetValueOrDefault() == num & unitCost.HasValue))
          goto label_6;
      }
      row.CuryUnitCost = this.DefaultUnitCost(sender, row, !this.AllowNonBaseCurrency());
    }
label_6:
    try
    {
      this.CheckRctForPOQuantityRule(sender, row, true, aOriginLine);
    }
    finally
    {
      this.inventoryIDChanging = false;
    }
  }

  protected virtual void POReceiptLine_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    POReceiptLine row = (POReceiptLine) e.Row;
    if (row.PONbr == null || row.POType == null)
      return;
    if (row.POLineNbr.HasValue)
    {
      Decimal? baseReceiptQty = row.BaseReceiptQty;
      Decimal num = 0M;
      if (baseReceiptQty.GetValueOrDefault() >= num & baseReceiptQty.HasValue)
        this.UpdatePOLineCompleteFlag(row, true, (POLine) null);
    }
    this.DeleteUnusedReference(row, this.isDeleting);
  }

  protected bool IsRequired(string poLineType)
  {
    return poLineType == "NS" || poLineType == "FT" || poLineType == "SV";
  }

  protected virtual void POReceiptLine_Selected_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is POReceiptLine row) || !(bool) e.NewValue)
      return;
    Decimal? baseReturnedQty = row.BaseReturnedQty;
    Decimal? baseReceiptQty = row.BaseReceiptQty;
    if (baseReturnedQty.GetValueOrDefault() >= baseReceiptQty.GetValueOrDefault() & baseReturnedQty.HasValue & baseReceiptQty.HasValue)
    {
      ((PXSelectBase) this.transactions).View.RequestRefresh();
      throw new PXSetPropertyException("The line is already completely returned.", (PXErrorLevel) 2);
    }
  }

  [PXStringList(new string[] {"GI", "GS", "GF", "GR", "GP", "NP", "PG", "PN", "NO", "NF", "NS", "SV", "FT", "DN"}, new string[] {"Goods for IN", "Goods for SO", "Goods for FS", "Goods for RP", "Goods for Drop-Ship", "Non-Stock for Drop-Ship", "Goods for Project", "Non-Stock for Project", "Non-Stock for SO", "Non-Stock for FS", "Non-Stock", "Service", "Freight", "Description"})]
  [PXMergeAttributes]
  public void POLineS_LineType_CacheAttached(PXCache sender)
  {
  }

  protected virtual void POOrderFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    POReceiptEntry.POOrderFilter row = (POReceiptEntry.POOrderFilter) e.Row;
    POReceipt current = ((PXSelectBase<POReceipt>) this.Document).Current;
    if (row == null || current == null)
      return;
    if (!string.IsNullOrEmpty(current.POType))
      row.OrderType = current.POType;
    int? nullable = current.ShipToBAccountID;
    if (nullable.HasValue)
      row.ShipToBAccountID = current.ShipToBAccountID;
    nullable = current.ShipToLocationID;
    if (nullable.HasValue)
      row.ShipToLocationID = current.ShipToLocationID;
    PXUIFieldAttribute.SetEnabled<POReceiptEntry.POOrderFilter.orderType>(sender, (object) row, string.IsNullOrEmpty(current.POType));
    PXStringListAttribute.SetList<POReceiptEntry.POOrderFilter.orderType>(sender, (object) null, new string[3]
    {
      "RO",
      "DP",
      "PD"
    }, new string[3]
    {
      "Normal",
      "Drop-Ship",
      "Project Drop-Ship"
    });
    PXCache pxCache1 = sender;
    POReceiptEntry.POOrderFilter poOrderFilter1 = row;
    nullable = current.ShipToBAccountID;
    int num1 = !nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<POReceiptEntry.POOrderFilter.shipToBAccountID>(pxCache1, (object) poOrderFilter1, num1 != 0);
    PXCache pxCache2 = sender;
    POReceiptEntry.POOrderFilter poOrderFilter2 = row;
    nullable = current.ShipToLocationID;
    int num2 = !nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<POReceiptEntry.POOrderFilter.shipToLocationID>(pxCache2, (object) poOrderFilter2, num2 != 0);
    PXUIFieldAttribute.SetVisible<POReceiptEntry.POOrderFilter.shipToBAccountID>(sender, (object) row, row.OrderType == "DP" || string.IsNullOrEmpty(row.OrderType));
    PXUIFieldAttribute.SetVisible<POReceiptEntry.POOrderFilter.shipToLocationID>(sender, (object) row, row.OrderType == "DP" || string.IsNullOrEmpty(row.OrderType));
    nullable = row.VendorID;
    int num3;
    if (!nullable.HasValue)
    {
      nullable = ((PXSelectBase<POReceipt>) this.Document).Current.VendorID;
      num3 = !nullable.HasValue ? 1 : 0;
    }
    else
      num3 = 0;
    bool flag = num3 != 0;
    PXUIFieldAttribute.SetVisible<POReceiptEntry.POLineS.orderNbr>(((PXGraph) this).Caches[typeof (POReceiptEntry.POLineS)], (object) null, row.OrderNbr == null);
    PXUIFieldAttribute.SetVisible<POReceiptEntry.POLineS.vendorID>(((PXGraph) this).Caches[typeof (POReceiptEntry.POLineS)], (object) null, flag);
    PXUIFieldAttribute.SetVisible<POLine.vendorLocationID>(((PXGraph) this).Caches[typeof (POReceiptEntry.POLineS)], (object) null, flag);
    ((PXAction) this.addPOOrderLine2).SetEnabled(row.AllowAddLine.GetValueOrDefault());
  }

  protected virtual void POOrderFilter_OrderNbr_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    POReceiptEntry.POOrderFilter row = (POReceiptEntry.POOrderFilter) e.Row;
    this.ClearSelectionCache<POReceiptEntry.POLineS.selected>(((PXSelectBase) this.poLinesSelection).Cache);
  }

  protected virtual void POOrderFilter_OrderType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    POReceiptEntry.POOrderFilter row = (POReceiptEntry.POOrderFilter) e.Row;
    sender.SetDefaultExt<POReceiptEntry.POOrderFilter.shipToBAccountID>(e.Row);
    sender.SetValuePending<POReceiptEntry.POOrderFilter.orderNbr>(e.Row, (object) null);
    ((PXSelectBase) this.poLinesSelection).Cache.Clear();
    ((PXSelectBase) this.openOrders).Cache.Clear();
  }

  protected virtual void POOrderFilter_ShipToBAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    POReceiptEntry.POOrderFilter row = (POReceiptEntry.POOrderFilter) e.Row;
    sender.SetDefaultExt<POReceiptEntry.POOrderFilter.shipToLocationID>(e.Row);
    ((PXSelectBase) this.poLinesSelection).Cache.Clear();
    ((PXSelectBase) this.openOrders).Cache.Clear();
  }

  protected virtual void POReceiptLineS_BarCode_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!e.ExternalCall)
      return;
    PXResult<INItemXRef, PX.Objects.IN.InventoryItem, INSubItem> crossReference = this.GetCrossReference((POReceiptEntry.POReceiptLineS) e.Row);
    if (crossReference != null)
    {
      sender.SetValue<POReceiptEntry.POReceiptLineS.inventoryID>(e.Row, (object) null);
      sender.SetValuePending<POReceiptEntry.POReceiptLineS.inventoryID>(e.Row, (object) PXResult<INItemXRef, PX.Objects.IN.InventoryItem, INSubItem>.op_Implicit(crossReference).InventoryCD);
      sender.SetValuePending<POReceiptEntry.POReceiptLineS.subItemID>(e.Row, (object) PXResult<INItemXRef, PX.Objects.IN.InventoryItem, INSubItem>.op_Implicit(crossReference).SubItemCD);
      sender.SetValuePending<POReceiptEntry.POReceiptLineS.uOM>(e.Row, (object) PXResult<INItemXRef, PX.Objects.IN.InventoryItem, INSubItem>.op_Implicit(crossReference).UOM);
    }
    else
    {
      sender.SetValuePending<POReceiptEntry.POReceiptLineS.inventoryID>(e.Row, (object) null);
      sender.SetValuePending<POReceiptEntry.POReceiptLineS.subItemID>(e.Row, (object) null);
      sender.SetValuePending<POReceiptEntry.POReceiptLineS.uOM>(e.Row, (object) null);
    }
  }

  protected virtual void POReceiptLineS_InventoryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!e.ExternalCall)
      return;
    POReceiptEntry.POReceiptLineS row = e.Row as POReceiptEntry.POReceiptLineS;
    if (e.OldValue != null && row.InventoryID.HasValue)
      row.BarCode = (string) null;
    sender.SetDefaultExt<POReceiptEntry.POReceiptLineS.subItemID>((object) e);
  }

  protected virtual void POReceiptLineS_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    POReceiptEntry.POReceiptLineS newRow = e.NewRow as POReceiptEntry.POReceiptLineS;
    POReceiptEntry.POReceiptLineS row = e.Row as POReceiptEntry.POReceiptLineS;
    if (!e.ExternalCall || newRow == null || row == null)
      return;
    bool? byOne1 = newRow.ByOne;
    bool? byOne2 = row.ByOne;
    bool? nullable1;
    if (!(byOne1.GetValueOrDefault() == byOne2.GetValueOrDefault() & byOne1.HasValue == byOne2.HasValue))
    {
      nullable1 = newRow.ByOne;
      if (nullable1.GetValueOrDefault())
        newRow.Qty = new Decimal?(1M);
    }
    int? nullable2 = newRow.InventoryID;
    if (!nullable2.HasValue)
      return;
    nullable2 = newRow.InventoryID;
    int? inventoryId = row.InventoryID;
    if (nullable2.GetValueOrDefault() == inventoryId.GetValueOrDefault() & nullable2.HasValue == inventoryId.HasValue)
    {
      int? subItemId = newRow.SubItemID;
      nullable2 = row.SubItemID;
      if (subItemId.GetValueOrDefault() == nullable2.GetValueOrDefault() & subItemId.HasValue == nullable2.HasValue)
      {
        nullable2 = newRow.VendorLocationID;
        int? vendorLocationId = row.VendorLocationID;
        if (nullable2.GetValueOrDefault() == vendorLocationId.GetValueOrDefault() & nullable2.HasValue == vendorLocationId.HasValue)
        {
          int? shipFromSiteId = newRow.ShipFromSiteID;
          nullable2 = row.ShipFromSiteID;
          if (shipFromSiteId.GetValueOrDefault() == nullable2.GetValueOrDefault() & shipFromSiteId.HasValue == nullable2.HasValue)
          {
            nullable1 = newRow.FetchMode;
            if (!nullable1.GetValueOrDefault())
              return;
          }
        }
      }
    }
    if (this.addLinePopupHandler.View.Answer == null)
    {
      ((PXSelectBase) this.filter).Cache.Remove((object) ((PXSelectBase<POReceiptEntry.POOrderFilter>) this.filter).Current);
      ((PXSelectBase) this.filter).Cache.Insert((object) new POReceiptEntry.POOrderFilter()
      {
        VendorID = newRow.VendorID,
        VendorLocationID = newRow.VendorLocationID,
        ShipFromSiteID = newRow.ShipFromSiteID,
        OrderType = newRow.POType,
        OrderNbr = newRow.PONbr,
        ReceiptType = newRow.ReceiptType,
        InventoryID = newRow.InventoryID,
        SubItemID = newRow.SubItemID,
        ResetFilter = new bool?(true),
        AllowAddLine = new bool?(false)
      });
    }
    this.addLinePopupHandler.TryGetSourceItem(newRow);
  }

  protected virtual void POReceiptLineS_UOM_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    PXResult<INItemXRef, PX.Objects.IN.InventoryItem, INSubItem> crossReference = this.GetCrossReference((POReceiptEntry.POReceiptLineS) e.Row);
    if (crossReference == null)
      return;
    e.NewValue = (object) PXResult<INItemXRef, PX.Objects.IN.InventoryItem, INSubItem>.op_Implicit(crossReference).UOM;
  }

  private PXResult<INItemXRef, PX.Objects.IN.InventoryItem, INSubItem> GetCrossReference(
    POReceiptEntry.POReceiptLineS row)
  {
    return (PXResult<INItemXRef, PX.Objects.IN.InventoryItem, INSubItem>) PXResultset<INItemXRef>.op_Implicit(PXSelectBase<INItemXRef, PXSelectJoin<INItemXRef, InnerJoin<PX.Objects.IN.InventoryItem, On2<INItemXRef.FK.InventoryItem, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noPurchases>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>>>>>, InnerJoin<INSubItem, On<INItemXRef.FK.SubItem>>>, Where<INItemXRef.alternateID, Equal<Current<POReceiptEntry.POReceiptLineS.barCode>>, And<INItemXRef.alternateType, In3<INAlternateType.barcode, INAlternateType.gIN>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
  }

  public virtual POLine GetReferencedPOLine(string poType, string poNbr, int? poLineNbr)
  {
    if (string.IsNullOrEmpty(poType) || string.IsNullOrEmpty(poNbr) || !poLineNbr.HasValue)
      return (POLine) null;
    POLine referencedPoLine = ((PXSelectBase<POLine>) this.poline).Locate(new POLine()
    {
      OrderType = poType,
      OrderNbr = poNbr,
      LineNbr = poLineNbr
    });
    if (referencedPoLine == null)
    {
      referencedPoLine = POLine.PK.Find((PXGraph) this, poType, poNbr, poLineNbr, (PKFindOptions) 1);
      if (referencedPoLine != null)
        GraphHelper.Hold(((PXSelectBase) this.poline).Cache, (object) referencedPoLine);
    }
    return referencedPoLine;
  }

  protected virtual void POReceiptLineS_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    POReceiptEntry.POReceiptLineS row1 = e.Row as POReceiptEntry.POReceiptLineS;
    POReceiptEntry.POReceiptLineS oldRow = e.OldRow as POReceiptEntry.POReceiptLineS;
    if (!e.ExternalCall || row1 == null || oldRow == null)
      return;
    bool? byOne1 = row1.ByOne;
    bool? byOne2 = oldRow.ByOne;
    bool? nullable1;
    Decimal? nullable2;
    if (!(byOne1.GetValueOrDefault() == byOne2.GetValueOrDefault() & byOne1.HasValue == byOne2.HasValue))
    {
      nullable1 = row1.ByOne;
      if (nullable1.GetValueOrDefault())
      {
        nullable2 = row1.Qty;
        Decimal num = (Decimal) 1;
        if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
          sender.SetValueExt<POReceiptEntry.POReceiptLineS.receiptQty>((object) row1, (object) 1M);
      }
    }
    int? nullable3 = row1.InventoryID;
    if (nullable3.HasValue)
    {
      nullable3 = row1.InventoryID;
      int? inventoryId = oldRow.InventoryID;
      if (nullable3.GetValueOrDefault() == inventoryId.GetValueOrDefault() & nullable3.HasValue == inventoryId.HasValue)
      {
        int? subItemId = row1.SubItemID;
        nullable3 = oldRow.SubItemID;
        if (subItemId.GetValueOrDefault() == nullable3.GetValueOrDefault() & subItemId.HasValue == nullable3.HasValue)
        {
          nullable3 = row1.VendorLocationID;
          int? vendorLocationId = oldRow.VendorLocationID;
          if (nullable3.GetValueOrDefault() == vendorLocationId.GetValueOrDefault() & nullable3.HasValue == vendorLocationId.HasValue)
          {
            int? shipFromSiteId = row1.ShipFromSiteID;
            nullable3 = oldRow.ShipFromSiteID;
            if (shipFromSiteId.GetValueOrDefault() == nullable3.GetValueOrDefault() & shipFromSiteId.HasValue == nullable3.HasValue)
            {
              nullable1 = row1.FetchMode;
              if (!nullable1.GetValueOrDefault())
                goto label_15;
            }
          }
        }
      }
      object sourceItem = this.addLinePopupHandler.GetSourceItem();
      if (sourceItem != null)
      {
        this.addLinePopupHandler.SetFilterToSource(sender, row1, sourceItem);
        goto label_31;
      }
      this.addLinePopupHandler.SetFilterToError(sender, row1);
      goto label_31;
    }
label_15:
    int? nullable4;
    if (row1.UOM != oldRow.UOM)
    {
      nullable3 = row1.InventoryID;
      int? inventoryId = oldRow.InventoryID;
      if (nullable3.GetValueOrDefault() == inventoryId.GetValueOrDefault() & nullable3.HasValue == inventoryId.HasValue)
      {
        nullable4 = row1.InventoryID;
        if (nullable4.HasValue)
        {
          if (oldRow.UOM != null && row1.UOM != null)
          {
            Decimal? unitCost = row1.UnitCost;
            string uom1 = oldRow.UOM;
            nullable2 = row1.UnitCost;
            Decimal valueOrDefault = nullable2.GetValueOrDefault();
            nullable2 = oldRow.Qty;
            Decimal? nullable5 = row1.Qty;
            Decimal num1;
            if (!(nullable2.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable2.HasValue == nullable5.HasValue))
            {
              nullable5 = row1.Qty;
              num1 = nullable5.Value;
            }
            else
            {
              PXCache sender1 = sender;
              object row2 = e.Row;
              string uom2 = row1.UOM;
              nullable5 = oldRow.BaseReceiptQty;
              Decimal num2 = nullable5.Value;
              num1 = INUnitAttribute.ConvertFromBase<POReceiptEntry.POReceiptLineS.inventoryID>(sender1, row2, uom2, num2, INPrecision.QUANTITY);
            }
            Decimal num3 = num1;
            nullable4 = row1.VendorID;
            if (nullable4.HasValue)
            {
              nullable4 = row1.InventoryID;
              if (nullable4.HasValue)
              {
                nullable5 = row1.UnitCost;
                Decimal num4 = 0M;
                if (nullable5.GetValueOrDefault() == num4 & nullable5.HasValue)
                  sender.SetDefaultExt<POReceiptEntry.POReceiptLineS.unitCost>((object) row1);
              }
            }
            nullable5 = oldRow.UnitCost;
            nullable2 = row1.UnitCost;
            Decimal num5;
            if (nullable5.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable5.HasValue == nullable2.HasValue)
            {
              Decimal num6 = INUnitAttribute.ConvertFromBase<POReceiptEntry.POReceiptLineS.inventoryID>(sender, e.Row, uom1, valueOrDefault, INPrecision.UNITCOST);
              num5 = INUnitAttribute.ConvertToBase<POReceiptEntry.POReceiptLineS.inventoryID>(sender, e.Row, row1.UOM, num6, INPrecision.UNITCOST);
            }
            else
            {
              nullable2 = row1.UnitCost;
              num5 = nullable2.Value;
            }
            sender.SetValueExt<POReceiptEntry.POReceiptLineS.receiptQty>(e.Row, (object) num3);
            sender.SetValueExt<POReceiptEntry.POReceiptLineS.unitCost>(e.Row, (object) num5);
          }
          else
            sender.SetDefaultExt<POReceiptEntry.POReceiptLineS.unitCost>((object) row1);
        }
      }
    }
label_31:
    bool flag = false;
    nullable1 = row1.AutoAddLine;
    if (nullable1.GetValueOrDefault())
    {
      nullable2 = row1.Qty;
      Decimal num7 = 0M;
      if (nullable2.GetValueOrDefault() > num7 & nullable2.HasValue)
      {
        nullable4 = row1.VendorID;
        if (nullable4.HasValue)
        {
          nullable4 = row1.InventoryID;
          if (nullable4.HasValue && row1.BarCode != null)
          {
            nullable4 = row1.SubItemID;
            if (nullable4.HasValue)
            {
              nullable4 = row1.LocationID;
              if (nullable4.HasValue)
              {
                flag = row1.LotSerialNbr != null;
                if (!flag)
                {
                  INLotSerClass inLotSerClass = PXResultset<INLotSerClass>.op_Implicit(PXSelectBase<INLotSerClass, PXSelectJoin<INLotSerClass, InnerJoin<PX.Objects.IN.InventoryItem, On<INLotSerClass.lotSerClassID, Equal<PX.Objects.IN.InventoryItem.lotSerClassID>>>, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
                  {
                    (object) row1.InventoryID
                  }));
                  int num8;
                  if (!(inLotSerClass.LotSerTrack == "N"))
                  {
                    nullable1 = inLotSerClass.AutoNextNbr;
                    num8 = nullable1.GetValueOrDefault() ? 1 : 0;
                  }
                  else
                    num8 = 1;
                  flag = num8 != 0;
                }
                if (flag)
                {
                  this.AddReceiptLine();
                  this.ResetReceiptFilter(true);
                }
              }
            }
          }
        }
      }
    }
    if (flag)
      return;
    row1.Description = (string) null;
  }

  protected virtual void POReceiptLineS_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is POReceiptEntry.POReceiptLineS row))
      return;
    INLotSerClass inLotSerClass = INLotSerClass.PK.Find((PXGraph) this, PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID)?.LotSerClassID);
    bool flag1 = inLotSerClass != null && inLotSerClass.LotSerTrack != "N" && inLotSerClass.LotSerAssign == "R";
    bool flag2 = row.ReceiptType == "RX";
    PXUIFieldAttribute.SetEnabled<POReceiptEntry.POReceiptLineS.pOType>(sender, (object) row, row.PONbr == null);
    PXCache pxCache1 = sender;
    POReceiptEntry.POReceiptLineS poReceiptLineS1 = row;
    bool? nullable1;
    int num1;
    if (flag1)
    {
      nullable1 = inLotSerClass.AutoNextNbr;
      bool flag3 = false;
      num1 = nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    PXUIFieldAttribute.SetEnabled<POReceiptEntry.POReceiptLineS.lotSerialNbr>(pxCache1, (object) poReceiptLineS1, num1 != 0);
    PXCache pxCache2 = sender;
    POReceiptEntry.POReceiptLineS poReceiptLineS2 = row;
    int num2;
    if (flag1)
    {
      nullable1 = inLotSerClass.LotSerTrackExpiration;
      num2 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    PXUIFieldAttribute.SetEnabled<POReceiptEntry.POReceiptLineS.expireDate>(pxCache2, (object) poReceiptLineS2, num2 != 0);
    PXCache pxCache3 = sender;
    POReceiptEntry.POReceiptLineS poReceiptLineS3 = row;
    int? nullable2;
    int num3;
    if (row.PONbr == null)
    {
      nullable2 = row.ReceiptVendorID;
      num3 = !nullable2.HasValue ? 1 : 0;
    }
    else
      num3 = 0;
    PXUIFieldAttribute.SetEnabled<POReceiptEntry.POReceiptLineS.vendorID>(pxCache3, (object) poReceiptLineS3, num3 != 0);
    PXCache pxCache4 = sender;
    POReceiptEntry.POReceiptLineS poReceiptLineS4 = row;
    int num4;
    if (row.PONbr == null)
    {
      nullable2 = row.ReceiptVendorLocationID;
      num4 = !nullable2.HasValue ? 1 : 0;
    }
    else
      num4 = 0;
    PXUIFieldAttribute.SetEnabled<POReceiptEntry.POReceiptLineS.vendorLocationID>(pxCache4, (object) poReceiptLineS4, num4 != 0);
    PXUIFieldAttribute.SetEnabled<POReceiptEntry.POReceiptLineS.siteID>(sender, (object) row, row.PONbr == null && !flag2);
    PXCache pxCache5 = sender;
    POReceiptEntry.POReceiptLineS poReceiptLineS5 = row;
    int num5;
    if (!flag1 || !(inLotSerClass.LotSerTrack == "S"))
    {
      nullable1 = row.ByOne;
      if (!nullable1.GetValueOrDefault())
      {
        num5 = !flag2 ? 1 : 0;
        goto label_17;
      }
    }
    num5 = 0;
label_17:
    PXUIFieldAttribute.SetEnabled<POReceiptEntry.POReceiptLineS.uOM>(pxCache5, (object) poReceiptLineS5, num5 != 0);
    PXCache pxCache6 = sender;
    POReceiptEntry.POReceiptLineS poReceiptLineS6 = row;
    int num6;
    if (!flag1 || !(inLotSerClass.LotSerTrack == "S"))
    {
      nullable1 = row.ByOne;
      if (!nullable1.GetValueOrDefault())
      {
        num6 = !flag2 ? 1 : 0;
        goto label_21;
      }
    }
    num6 = 0;
label_21:
    PXUIFieldAttribute.SetEnabled<POReceiptEntry.POReceiptLineS.receiptQty>(pxCache6, (object) poReceiptLineS6, num6 != 0);
    PXCache pxCache7 = sender;
    POReceiptEntry.POReceiptLineS poReceiptLineS7 = row;
    int num7;
    if (!string.IsNullOrEmpty(row.BarCode))
    {
      nullable2 = row.InventoryID;
      num7 = !nullable2.HasValue ? 1 : 0;
    }
    else
      num7 = 1;
    PXUIFieldAttribute.SetEnabled<POReceiptEntry.POReceiptLineS.inventoryID>(pxCache7, (object) poReceiptLineS7, num7 != 0);
    PXUIFieldAttribute.SetEnabled<POReceiptEntry.POReceiptLineS.unitCost>(sender, (object) row, row.POAccrualType != "O");
    PXUIFieldAttribute.SetVisible<POReceiptEntry.POReceiptLineS.vendorID>(sender, (object) row, !flag2);
    PXUIFieldAttribute.SetVisible<POReceiptEntry.POReceiptLineS.vendorLocationID>(sender, (object) row, !flag2);
    PXUIFieldAttribute.SetVisible<POReceiptEntry.POReceiptLineS.pOType>(sender, (object) row, !flag2);
    PXUIFieldAttribute.SetVisible<POReceiptEntry.POReceiptLineS.pONbr>(sender, (object) row, !flag2);
    PXUIFieldAttribute.SetVisible<POReceiptEntry.POReceiptLineS.pOLineNbr>(sender, (object) row, !flag2);
    PXUIFieldAttribute.SetVisible<POReceiptEntry.POReceiptLineS.unitCost>(sender, (object) row, !flag2);
    PXUIFieldAttribute.SetVisible<POReceiptEntry.POReceiptLineS.shipFromSiteID>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetVisible<POReceiptEntry.POReceiptLineS.sOOrderType>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetVisible<POReceiptEntry.POReceiptLineS.sOOrderNbr>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetVisible<POReceiptEntry.POReceiptLineS.sOOrderLineNbr>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetVisible<POReceiptEntry.POReceiptLineS.sOShipmentNbr>(sender, (object) row, flag2);
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if (row.BarCode != null)
    {
      nullable2 = row.InventoryID;
      if (nullable2.HasValue)
      {
        nullable2 = row.SubItemID;
        if (nullable2.HasValue)
        {
          if (PXResultset<INItemXRef>.op_Implicit(PXSelectBase<INItemXRef, PXSelect<INItemXRef, Where<INItemXRef.inventoryID, Equal<Current<POReceiptEntry.POReceiptLineS.inventoryID>>, And<INItemXRef.alternateID, Equal<Current<POReceiptEntry.POReceiptLineS.barCode>>, And<INItemXRef.alternateType, In3<INAlternateType.barcode, INAlternateType.gIN>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
          {
            e.Row
          }, Array.Empty<object>())) == null)
            propertyException = new PXSetPropertyException("Barcode will be added to inventory item.", (PXErrorLevel) 2);
        }
      }
    }
    sender.RaiseExceptionHandling<POReceiptEntry.POReceiptLineS.barCode>(e.Row, (object) ((POReceiptEntry.POReceiptLineS) e.Row).BarCode, (Exception) propertyException);
  }

  protected virtual void INItemXRef_BAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<POReceiptLandedCostDetail, POReceiptLandedCostDetail.curyLineAmt> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = this.MultiCurrencyExt.GetCurrencyInfo(e.Row.CuryInfoID);
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<POReceiptLandedCostDetail, POReceiptLandedCostDetail.curyLineAmt>>) e).ReturnValue = (object) currencyInfo.CuryConvCury(e.Row.LineAmt ?? e.Row.POLandedCostDetailLineAmt.GetValueOrDefault());
  }

  public virtual IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    return viewName == "poLinesSelection" && (maximumRows == 0 || maximumRows > 201) ? ((PXGraph) this).ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, 201, ref totalRows) : ((PXGraph) this).ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }

  public virtual void Clear(PXClearOption option)
  {
    ((PXGraph) this).Clear(option);
    if (!((PXGraph) this).IsImport || ((PXGraph) this).IsMobile)
      return;
    ((PXSelectBase) this.filter).Cache.Clear();
    ((PXSelectBase) this.filter).Cache.Insert((object) new POReceiptEntry.POOrderFilter());
  }

  public virtual bool IsDirty
  {
    get
    {
      POReceipt current = ((PXSelectBase<POReceipt>) this.Document).Current;
      return ((current != null ? (!current.Released.GetValueOrDefault() ? 1 : 0) : 1) != 0 || ((PXGraph) this).IsContractBasedAPI) && ((PXGraph) this).IsDirty;
    }
  }

  public virtual void Persist()
  {
    if (((PXSelectBase<POReceipt>) this.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<POReceipt>) this.Document).Current.Hold;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        foreach (PXResult<POReceiptLine> pxResult in ((PXSelectBase<POReceiptLine>) this.transactions).Select(Array.Empty<object>()))
        {
          POReceiptLine poReceiptLine = PXResult<POReceiptLine>.op_Implicit(pxResult);
          Decimal? receiptQty = poReceiptLine.ReceiptQty;
          Decimal num = 0M;
          if (receiptQty.GetValueOrDefault() == num & receiptQty.HasValue)
          {
            if (((PXSelectBase<POReceipt>) this.Document).Current.ReceiptType == "RX")
            {
              nullable = ((PXSelectBase<POReceipt>) this.Document).Current.Received;
              if (!nullable.GetValueOrDefault() && !AllowZeroReceiptQtyScope.IsActive(((PXSelectBase<POReceipt>) this.Document).Current))
                ((PXSelectBase<POReceiptLine>) this.transactions).Delete(poReceiptLine);
            }
            if (!(((PXSelectBase<POReceipt>) this.Document).Current.ReceiptType == "RT"))
            {
              if (((PXSelectBase<POReceipt>) this.Document).Current.ReceiptType == "RX")
              {
                nullable = ((PXSelectBase<POReceipt>) this.Document).Current.Received;
                if (!nullable.GetValueOrDefault())
                  continue;
              }
              else
                continue;
            }
            GraphHelper.MarkUpdated(((PXSelectBase) this.transactions).Cache, (object) poReceiptLine);
          }
        }
        this.ValidateDuplicateSerialsOnDropship();
      }
    }
    this.SyncUnassigned();
    List<Action<PXGraph>> actionList = new List<Action<PXGraph>>();
    if (((PXSelectBase<POReceipt>) this.Document).Current?.ReceiptType == "RX")
    {
      foreach (IBqlTable binding in ((PXSelectBase) this.transactions).Cache.Inserted)
      {
        UniquenessChecker<SelectFromBase<POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLine.origTranType, Equal<BqlField<POReceiptLine.origTranType, IBqlString>.FromCurrent>>>>, And<BqlOperand<POReceiptLine.origRefNbr, IBqlString>.IsEqual<BqlField<POReceiptLine.origRefNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<POReceiptLine.origLineNbr, IBqlInt>.IsEqual<BqlField<POReceiptLine.origLineNbr, IBqlInt>.FromCurrent>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLine.released, Equal<False>>>>>.Or<BqlOperand<POReceiptLine.iNReleased, IBqlBool>.IsEqual<False>>>>> uniquenessChecker = new UniquenessChecker<SelectFromBase<POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLine.origTranType, Equal<BqlField<POReceiptLine.origTranType, IBqlString>.FromCurrent>>>>, And<BqlOperand<POReceiptLine.origRefNbr, IBqlString>.IsEqual<BqlField<POReceiptLine.origRefNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<POReceiptLine.origLineNbr, IBqlInt>.IsEqual<BqlField<POReceiptLine.origLineNbr, IBqlInt>.FromCurrent>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLine.released, Equal<False>>>>>.Or<BqlOperand<POReceiptLine.iNReleased, IBqlBool>.IsEqual<False>>>>>(binding);
        actionList.Add(new Action<PXGraph>(uniquenessChecker.OnBeforeCommitImpl));
        ((PXGraph) this).OnBeforeCommit += new Action<PXGraph>(uniquenessChecker.OnBeforeCommitImpl);
      }
    }
    try
    {
      ((PXGraph) this).Persist();
    }
    finally
    {
      if (((PXSelectBase<POReceipt>) this.Document).Current?.ReceiptType == "RX")
      {
        foreach (Action<PXGraph> action in actionList)
          ((PXGraph) this).OnBeforeCommit -= action;
      }
    }
    this.ClearSelectionCache<POReceiptEntry.POLineS.selected>(((PXSelectBase) this.poLinesSelection).Cache);
    ((PXSelectBase) this.openOrders).Cache.Clear();
  }

  protected virtual void ValidateDuplicateSerialsOnDropship()
  {
    if (((PXSelectBase<POReceipt>) this.Document).Current == null || ((PXSelectBase<POReceipt>) this.Document).Current.Hold.GetValueOrDefault())
      return;
    HashSet<string> stringSet = new HashSet<string>();
    bool flag = false;
    foreach (PXResult<POReceiptLineSplit> pxResult in ((PXSelectBase<POReceiptLineSplit>) this.splits).Select(Array.Empty<object>()))
    {
      POReceiptLineSplit split = PXResult<POReceiptLineSplit>.op_Implicit(pxResult);
      if (split.LineType == "GP" && this.LineSplittingExt.IsTrackSerial(split) && (string.IsNullOrEmpty(split.AssignedNbr) || !INLotSerialNbrAttribute.StringsEqual(split.AssignedNbr, split.LotSerialNbr)))
      {
        string str = $"{split.InventoryID}.{split.LotSerialNbr}";
        if (stringSet.Contains(str))
        {
          flag = true;
          POReceiptLine poReceiptLine = PXParentAttribute.SelectParent<POReceiptLine>(((PXSelectBase) this.splits).Cache, (object) split);
          if (poReceiptLine != null)
            ((PXSelectBase) this.transactions).Cache.RaiseExceptionHandling<POReceiptLine.inventoryID>((object) poReceiptLine, (object) null, (Exception) new PXSetPropertyException("Contains duplicate serial numbers.", (PXErrorLevel) 5));
        }
        else
          stringSet.Add(str);
      }
    }
    if (flag)
      throw new PXException("One or more records with duplicate serial numbers found in the document.");
  }

  public virtual POReceipt CreateEmptyReceiptFrom(POOrder order)
  {
    POReceipt poReceipt = ((PXSelectBase<POReceipt>) this.Document).Insert();
    poReceipt.ReceiptType = "RT";
    poReceipt.BranchID = order.BranchID;
    poReceipt.VendorID = order.VendorID;
    poReceipt.VendorLocationID = order.VendorLocationID;
    poReceipt.ProjectID = order.ProjectID;
    POReceipt emptyReceiptFrom = ((PXSelectBase<POReceipt>) this.Document).Update(poReceipt);
    if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
    {
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = this.MultiCurrencyExt.GetCurrencyInfo(order.CuryInfoID);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = this.MultiCurrencyExt.GetCurrencyInfo(emptyReceiptFrom.CuryInfoID);
      currencyInfo2.CuryID = currencyInfo1.CuryID;
      currencyInfo2.CuryRateTypeID = currencyInfo1.CuryRateTypeID;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.MultiCurrencyExt.currencyinfo).Update(currencyInfo2);
      emptyReceiptFrom.CuryID = currencyInfo3.CuryID;
      emptyReceiptFrom = ((PXSelectBase<POReceipt>) this.Document).Update(emptyReceiptFrom);
    }
    return emptyReceiptFrom;
  }

  public virtual POReceipt CreateReceiptFrom(POOrder order, bool redirect = false)
  {
    this.ValidatePOOrder(order);
    this.CreateEmptyReceiptFrom(order);
    this.AddPurchaseOrder(order);
    if (!((PXSelectBase) this.transactions).Cache.IsDirty)
      throw new PXException("There are no lines in this document that may be entered in PO Receipt Document");
    if (redirect)
      throw new PXRedirectRequiredException((PXGraph) this, "Switch to PO Receipt");
    return ((PXSelectBase<POReceipt>) this.Document).Current;
  }

  public int? GetReasonCodeSubID(
    PX.Objects.CS.ReasonCode reasoncode,
    PX.Objects.IN.InventoryItem item,
    PX.Objects.IN.INSite site,
    INPostClass postclass)
  {
    int? nullable1 = (int?) ((PXGraph) this).Caches[typeof (PX.Objects.CS.ReasonCode)].GetValue<PX.Objects.CS.ReasonCode.subID>((object) reasoncode);
    int? nullable2 = (int?) ((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)].GetValue<PX.Objects.IN.InventoryItem.reasonCodeSubID>((object) item);
    int? nullable3 = (int?) ((PXGraph) this).Caches[typeof (PX.Objects.IN.INSite)].GetValue<PX.Objects.IN.INSite.reasonCodeSubID>((object) site);
    int? nullable4 = (int?) ((PXGraph) this).Caches[typeof (INPostClass)].GetValue<INPostClass.reasonCodeSubID>((object) postclass);
    object reasonCodeSubId = (object) PX.Objects.IN.ReasonCodeSubAccountMaskAttribute.MakeSub<PX.Objects.CS.ReasonCode.subMask>((PXGraph) this, reasoncode.SubMask, new object[4]
    {
      (object) nullable1,
      (object) nullable2,
      (object) nullable3,
      (object) nullable4
    }, new System.Type[4]
    {
      typeof (PX.Objects.CS.ReasonCode.subID),
      typeof (PX.Objects.IN.InventoryItem.reasonCodeSubID),
      typeof (PX.Objects.IN.INSite.reasonCodeSubID),
      typeof (INPostClass.reasonCodeSubID)
    });
    ((PXGraph) this).Caches[typeof (PX.Objects.CS.ReasonCode)].RaiseFieldUpdating<PX.Objects.CS.ReasonCode.subID>((object) reasoncode, ref reasonCodeSubId);
    return (int?) reasonCodeSubId;
  }

  protected virtual Decimal? DefaultUnitCost(
    PXCache sender,
    POReceiptLine row,
    bool alwaysFromBaseCury)
  {
    POReceipt current = ((PXSelectBase<POReceipt>) this.Document).Current;
    int? nullable1;
    int num1;
    if (current == null)
    {
      num1 = 1;
    }
    else
    {
      nullable1 = current.VendorID;
      num1 = !nullable1.HasValue ? 1 : 0;
    }
    if (num1 == 0)
    {
      int num2;
      if (row == null)
      {
        num2 = 1;
      }
      else
      {
        nullable1 = row.InventoryID;
        num2 = !nullable1.HasValue ? 1 : 0;
      }
      if (num2 == 0 && !string.IsNullOrEmpty(row.UOM) || !(row?.LineType != "FT"))
      {
        if (row.LineType == "FT")
          return new Decimal?(0M);
        Decimal? nullable2;
        if (row.ManualPrice.GetValueOrDefault())
        {
          nullable2 = row.UnitCost;
          if (nullable2.HasValue)
            goto label_14;
        }
        if (row.PONbr == null)
        {
          PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = this.MultiCurrencyExt.GetDefaultCurrencyInfo();
          Decimal? newValue = APVendorPriceMaint.CalculateUnitCost(sender, row.VendorID, current.VendorLocationID, row.InventoryID, row.SiteID, defaultCurrencyInfo.GetCM(), row.UOM, row.ReceiptQty, current.ReceiptDate.Value, row.UnitCost, alwaysFromBaseCury);
          if (!newValue.HasValue)
          {
            nullable1 = row.SubItemID;
            if (nullable1.HasValue)
              newValue = POItemCostManager.Fetch<POReceiptLine.inventoryID, POReceiptLine.curyInfoID>(sender.Graph, (object) row, current.VendorID, current.VendorLocationID, current.ReceiptDate, current.CuryID, row.InventoryID, row.SubItemID, row.SiteID, row.UOM);
          }
          APVendorPriceMaint.CheckNewUnitCost<POReceiptLine, POReceiptLine.curyUnitCost>(sender, row, (object) newValue);
          return newValue;
        }
label_14:
        nullable2 = alwaysFromBaseCury ? row.UnitCost : row.CuryUnitCost;
        return new Decimal?(nullable2.GetValueOrDefault());
      }
    }
    return new Decimal?();
  }

  protected virtual void ParentFieldUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.ObjectsEqual<POReceipt.receiptDate, POReceipt.curyID>(e.Row, e.OldRow))
      return;
    foreach (PXResult<POReceiptLine> pxResult in ((PXSelectBase<POReceiptLine>) this.transactions).Select(Array.Empty<object>()))
      GraphHelper.MarkUpdated(((PXSelectBase) this.transactions).Cache, (object) PXResult<POReceiptLine>.op_Implicit(pxResult), true);
  }

  public virtual void AddPurchaseOrder(POOrder aOrder) => this.AddPurchaseOrder(aOrder, new int?());

  public virtual void AddPurchaseOrder(POOrder aOrder, int? inventoryID, int? subItemID = null)
  {
    POReceiptEntry.POOrderFilter poOrderFilter = new POReceiptEntry.POOrderFilter();
    poOrderFilter.OrderType = aOrder.OrderType;
    poOrderFilter.OrderNbr = aOrder.OrderNbr;
    if (inventoryID.HasValue)
      poOrderFilter.InventoryID = inventoryID;
    if (subItemID.HasValue)
      poOrderFilter.SubItemID = subItemID;
    bool flag1 = false;
    bool flag2 = false;
    Exception exception = (Exception) null;
    try
    {
      this.PrefetchDropShipLinks(aOrder);
      this._skipUIUpdate = true;
      PXView view = ((PXSelectBase) this.poLinesSelection).View;
      object[] objArray1 = (object[]) new POReceiptEntry.POOrderFilter[1]
      {
        poOrderFilter
      };
      object[] objArray2 = Array.Empty<object>();
      foreach (PXResult<POReceiptEntry.POLineS, POOrder> pxResult in view.SelectMultiBound(objArray1, objArray2))
      {
        try
        {
          this.AddPOLine((POLine) PXResult<POReceiptEntry.POLineS, POOrder>.op_Implicit(pxResult));
          flag2 = true;
        }
        catch (PXException ex)
        {
          if (ex is FailedToAddPOOrderException)
            exception = (Exception) ex;
          PXTrace.WriteError((Exception) ex);
          flag1 = true;
        }
      }
      if (flag2)
        this.AddPOOrderReceipt(aOrder.OrderType, aOrder.OrderNbr);
    }
    finally
    {
      this._skipUIUpdate = false;
    }
    if (flag1)
      throw exception != null ? (object) exception : (object) new PXException("One purchase order line or multiple purchase order lines cannot be added to the bill. See Trace Log for details.");
  }

  public virtual POReceiptLine AddPOLine(POLine aLine, bool isLSEntryBlocked = false)
  {
    POReceipt current = ((PXSelectBase<POReceipt>) this.Document).Current;
    if (current == null || !(current.ReceiptType != "RX"))
      return (POReceiptLine) null;
    this.ValidatePOLine(aLine, current);
    Decimal aBaseQtyAdj = 0M;
    Dictionary<int, POReceiptLine> dictionary = new Dictionary<int, POReceiptLine>();
    foreach (PXResult<POReceiptLine> pxResult in PXSelectBase<POReceiptLine, PXSelect<POReceiptLine, Where<POReceiptLine.receiptType, Equal<Required<POReceiptLine.receiptType>>, And<POReceiptLine.receiptNbr, Equal<Required<POReceiptLine.receiptNbr>>, And<POReceiptLine.pOType, Equal<Required<POReceiptLine.pOType>>, And<POReceiptLine.pONbr, Equal<Required<POReceiptLine.pONbr>>, And<POReceiptLine.pOLineNbr, Equal<Required<POReceiptLine.pOLineNbr>>>>>>>>.Config>.Select((PXGraph) this, new object[5]
    {
      (object) current.ReceiptType,
      (object) current.ReceiptNbr,
      (object) aLine.OrderType,
      (object) aLine.OrderNbr,
      (object) aLine.LineNbr
    }))
    {
      POReceiptLine poReceiptLine1 = PXResult<POReceiptLine>.op_Implicit(pxResult);
      if (EnumerableExtensions.IsIn<string>(aLine.LineType, "GS", "NO", "GF", "NF", "GM", new string[1]
      {
        "NM"
      }))
        return (POReceiptLine) null;
      POReceiptLine poReceiptLine2 = (POReceiptLine) PrimaryKeyOf<POReceiptLine>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<POReceiptLine.receiptType, POReceiptLine.receiptNbr, POReceiptLine.lineNbr>>.Find((PXGraph) this, (TypeArrayOf<IBqlField>.IFilledWith<POReceiptLine.receiptType, POReceiptLine.receiptNbr, POReceiptLine.lineNbr>) poReceiptLine1, (PKFindOptions) 0);
      Decimal num1 = aBaseQtyAdj;
      Decimal? baseReceiptQty = poReceiptLine1.BaseReceiptQty;
      Decimal valueOrDefault = baseReceiptQty.GetValueOrDefault();
      Decimal num2;
      if (poReceiptLine2 == null)
      {
        num2 = 0M;
      }
      else
      {
        baseReceiptQty = poReceiptLine2.BaseReceiptQty;
        num2 = baseReceiptQty.Value;
      }
      Decimal num3 = valueOrDefault - num2;
      aBaseQtyAdj = num1 + num3;
      dictionary[poReceiptLine1.LineNbr.Value] = poReceiptLine1;
    }
    foreach (PXResult<POReceiptLine> pxResult in PXSelectBase<POReceiptLine, PXSelectReadonly<POReceiptLine, Where<POReceiptLine.receiptType, Equal<Required<POReceiptLine.receiptType>>, And<POReceiptLine.receiptNbr, Equal<Required<POReceiptLine.receiptNbr>>, And<POReceiptLine.pOType, Equal<Required<POReceiptLine.pOType>>, And<POReceiptLine.pONbr, Equal<Required<POReceiptLine.pONbr>>, And<POReceiptLine.pOLineNbr, Equal<Required<POReceiptLine.pOLineNbr>>>>>>>>.Config>.Select((PXGraph) this, new object[5]
    {
      (object) current.ReceiptType,
      (object) current.ReceiptNbr,
      (object) aLine.OrderType,
      (object) aLine.OrderNbr,
      (object) aLine.LineNbr
    }))
    {
      POReceiptLine poReceiptLine = PXResult<POReceiptLine>.op_Implicit(pxResult);
      if (!dictionary.ContainsKey(poReceiptLine.LineNbr.Value))
        aBaseQtyAdj -= poReceiptLine.BaseReceiptQty.Value;
    }
    Decimal aQtyAdj = aBaseQtyAdj;
    POReceiptLine poReceiptLine3 = new POReceiptLine()
    {
      ReceiptType = current.ReceiptType,
      ReceiptNbr = current.ReceiptNbr,
      CuryInfoID = current.CuryInfoID
    };
    int? nullable1;
    if (aBaseQtyAdj != 0M)
    {
      nullable1 = aLine.InventoryID;
      if (nullable1.HasValue && !string.IsNullOrEmpty(aLine.UOM))
        aQtyAdj = INUnitAttribute.ConvertFromBase(((PXSelectBase) this.transactions).Cache, aLine.InventoryID, aLine.UOM, aBaseQtyAdj, INPrecision.QUANTITY);
    }
    this.Copy(poReceiptLine3, aLine, aQtyAdj, aBaseQtyAdj);
    int num4;
    if (POLineType.UsePOAccrual(aLine.LineType))
    {
      nullable1 = aLine.POAccrualAcctID;
      if (!nullable1.HasValue)
      {
        nullable1 = aLine.POAccrualSubID;
        num4 = !nullable1.HasValue ? 1 : 0;
        goto label_28;
      }
    }
    num4 = 0;
label_28:
    this._forceAccrualAcctDefaulting = num4 != 0;
    Decimal? nullable2 = poReceiptLine3.ReceiptQty;
    Decimal num5 = 0M;
    if (nullable2.GetValueOrDefault() >= num5 & nullable2.HasValue)
    {
      try
      {
        poReceiptLine3.IsLSEntryBlocked = new bool?(isLSEntryBlocked);
        poReceiptLine3 = this.InsertReceiptLine(poReceiptLine3, current, aLine);
      }
      finally
      {
        if (poReceiptLine3 != null)
          poReceiptLine3.IsLSEntryBlocked = new bool?(false);
      }
    }
    else
    {
      POReceiptLine poReceiptLine4 = poReceiptLine3;
      POReceiptLine poReceiptLine5 = poReceiptLine3;
      nullable2 = new Decimal?(0M);
      Decimal? nullable3 = nullable2;
      poReceiptLine5.BaseReceiptQty = nullable3;
      Decimal? nullable4 = nullable2;
      poReceiptLine4.ReceiptQty = nullable4;
      poReceiptLine3 = this.InsertReceiptLine(poReceiptLine3, current, aLine);
      ((PXSelectBase) this.transactions).Cache.RaiseExceptionHandling<POReceiptLine.receiptQty>((object) poReceiptLine3, (object) poReceiptLine3.ReceiptQty, (Exception) new PXSetPropertyException("Quantity received is already above the order's quantity for this line", (PXErrorLevel) 2));
    }
    this._forceAccrualAcctDefaulting = false;
    if (((PXSelectBase<POSetup>) this.posetup).Current.CopyLineNotesToReceipt.GetValueOrDefault() || ((PXSelectBase<POSetup>) this.posetup).Current.CopyLineFilesToReceipt.GetValueOrDefault())
      PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.poline).Cache, (object) aLine, ((PXSelectBase) this.transactions).Cache, (object) poReceiptLine3, ((PXSelectBase<POSetup>) this.posetup).Current.CopyLineNotesToReceipt, ((PXSelectBase<POSetup>) this.posetup).Current.CopyLineFilesToReceipt);
    return poReceiptLine3;
  }

  public virtual POReceiptLine AddReturnLine(IPOReturnLineSource origLine)
  {
    Decimal? receiptQty = origLine.ReceiptQty;
    Decimal? returnedQty = origLine.ReturnedQty;
    Decimal? nullable1 = receiptQty.HasValue & returnedQty.HasValue ? new Decimal?(receiptQty.GetValueOrDefault() - returnedQty.GetValueOrDefault()) : new Decimal?();
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() <= num1 & nullable1.HasValue)
      return (POReceiptLine) null;
    POReceiptLine poReceiptLine1 = PXResultset<POReceiptLine>.op_Implicit(PXSelectBase<POReceiptLine, PXSelect<POReceiptLine, Where<POReceiptLine.origReceiptType, Equal<Required<POReceiptLine.origReceiptType>>, And<POReceiptLine.origReceiptNbr, Equal<Required<POReceiptLine.origReceiptNbr>>, And<POReceiptLine.origReceiptLineNbr, Equal<Required<POReceiptLine.origReceiptLineNbr>>, And<POReceiptLine.released, Equal<False>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
    {
      (object) origLine.ReceiptType,
      (object) origLine.ReceiptNbr,
      (object) origLine.LineNbr
    }));
    if (poReceiptLine1 != null)
      throw new PXAlreadyCreatedException("There is at least one unreleased return {0} prepared for this purchase receipt. To create new purchase return, remove or release the unreleased purchase return.", new object[1]
      {
        (object) poReceiptLine1.ReceiptNbr
      });
    if (((PXSelectBase<POReceipt>) this.Document).Current?.CuryID != ((PXGraph) this).Accessinfo.BaseCuryID)
    {
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = this.GetCurrencyInfo((long?) ((PXSelectBase<POReceipt>) this.Document).Current?.CuryInfoID);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = this.GetCurrencyInfo(origLine.CuryInfoID);
      if (((PXSelectBase<POReceipt>) this.Document).Current?.ReturnInventoryCostMode == "O")
      {
        if (!((IQueryable<PXResult<POReceiptLine>>) ((PXSelectBase<POReceiptLine>) this.transactions).Select(Array.Empty<object>())).Any<PXResult<POReceiptLine>>())
          this.CopyReceiptCurrencyInfoToReturn(currencyInfo2, currencyInfo1);
        else if (!this.IsSameCurrencyInfo(currencyInfo1, currencyInfo2))
          throw new PXException("The Original Cost from Receipt option is selected. The {0} purchase receipt cannot be added to this return because it has a different currency, currency rate, rate type, or effective date.", new object[1]
          {
            (object) origLine.ReceiptNbr
          });
      }
      else if (currencyInfo1.CuryID != currencyInfo2.CuryID)
        throw new PXException("The {0} purchase receipt cannot be added to this return because it has a different currency.", new object[1]
        {
          (object) origLine.ReceiptNbr
        });
    }
    POReceiptLine destLine = ((PXSelectBase<POReceiptLine>) this.transactions).Insert();
    this.CopyFromOrigReceiptLine(destLine, origLine, POLineType.IsProjectDropShip(origLine.LineType), new bool?(((PXSelectBase<POReceipt>) this.Document).Current?.ReturnInventoryCostMode == "O"));
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, origLine.InventoryID);
    int num2;
    if (inventoryItem != null && inventoryItem.IsConverted.GetValueOrDefault() && origLine.IsStockItem.HasValue)
    {
      bool? isStockItem = origLine.IsStockItem;
      bool? stkItem = inventoryItem.StkItem;
      num2 = !(isStockItem.GetValueOrDefault() == stkItem.GetValueOrDefault() & isStockItem.HasValue == stkItem.HasValue) ? 1 : 0;
    }
    else
      num2 = 0;
    bool flag = num2 != 0;
    if (flag)
      ((PXSelectBase) this.transactions).Cache.SetDefaultExt<POReceiptLine.lineType>((object) destLine);
    if (!this.IsLotSerTracked(origLine))
    {
      destLine = ((PXSelectBase<POReceiptLine>) this.transactions).Update(destLine);
    }
    else
    {
      using (this.LineSplittingExt.SuppressedModeScope(true))
      {
        destLine.UnassignedQty = destLine.BaseReceiptQty;
        destLine = ((PXSelectBase<POReceiptLine>) this.transactions).Update(destLine);
      }
      if (!string.IsNullOrEmpty(origLine.LotSerialNbr))
      {
        destLine.LotSerialNbr = origLine.LotSerialNbr;
        destLine.ExpireDate = origLine.ExpireDate;
        try
        {
          destLine = ((PXSelectBase<POReceiptLine>) this.transactions).Update(destLine);
        }
        catch (Exception ex)
        {
          ((PXSelectBase) this.transactions).Cache.RaiseExceptionHandling<POReceiptLine.lotSerialNbr>((object) destLine, (object) null, (Exception) new PXSetPropertyException(ex.Message, (PXErrorLevel) 4));
        }
      }
    }
    if (flag)
    {
      POReceiptLine poReceiptLine2 = destLine;
      bool? stkItem = inventoryItem.StkItem;
      bool? nullable2 = stkItem.HasValue ? new bool?(!stkItem.GetValueOrDefault()) : new bool?();
      poReceiptLine2.IsStockItem = nullable2;
    }
    if (!string.IsNullOrEmpty(destLine.POType) && !string.IsNullOrEmpty(destLine.PONbr))
      this.AddPOOrderReceipt(destLine.POType, destLine.PONbr);
    return destLine;
  }

  public virtual void ValidateUomConversionNotAdjusted(POReceiptLine rctLine)
  {
    Decimal? nullable1;
    if (!(rctLine.ReceiptType != "RN"))
    {
      nullable1 = rctLine.ReceiptQty;
      Decimal num = 0M;
      if (!(nullable1.GetValueOrDefault() <= num & nullable1.HasValue))
        goto label_4;
    }
    if (!rctLine.IsCorrection.GetValueOrDefault() || rctLine.POType == "DP")
      return;
    nullable1 = rctLine.BaseOrigQty;
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
      return;
label_4:
    if (!rctLine.OrigReceiptLineNbr.HasValue || rctLine.Released.GetValueOrDefault())
      return;
    POReceiptLine parent = KeysRelation<CompositeKey<Field<POReceiptLine.origReceiptType>.IsRelatedTo<POReceiptLine.receiptType>, Field<POReceiptLine.origReceiptNbr>.IsRelatedTo<POReceiptLine.receiptNbr>, Field<POReceiptLine.origReceiptLineNbr>.IsRelatedTo<POReceiptLine.lineNbr>>.WithTablesOf<POReceiptLine, POReceiptLine>, POReceiptLine, POReceiptLine>.FindParent((PXGraph) this, rctLine, (PKFindOptions) 0);
    if ((parent != null ? (!parent.InventoryID.HasValue ? 1 : 0) : 1) != 0 || parent == null || parent.UOM == null)
      return;
    POReceiptLine data = new POReceiptLine()
    {
      InventoryID = parent.InventoryID,
      ReceiptQty = parent.ReceiptQty,
      UOM = parent.UOM
    };
    PXDBQuantityAttribute.CalcBaseQty<POReceiptLine.receiptQty>(((PXSelectBase) this.transactions).Cache, (object) data);
    Decimal? baseReceiptQty1 = data.BaseReceiptQty;
    nullable1 = new Decimal?();
    Decimal? nullable2 = nullable1;
    Decimal? baseReceiptQty2 = parent.BaseReceiptQty;
    if (!EnumerableExtensions.IsNotIn<Decimal?>(baseReceiptQty1, nullable2, baseReceiptQty2))
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, parent.InventoryID);
    if (inventoryItem == null || inventoryItem.BaseUnit == null || string.Equals(inventoryItem.BaseUnit, parent.UOM, StringComparison.OrdinalIgnoreCase))
      return;
    if (((PXSelectBase) this.transactions).Cache.RaiseExceptionHandling<POReceiptLine.baseReceiptQty>((object) rctLine, (object) rctLine.BaseReceiptQty, (Exception) new PXSetPropertyException((IBqlTable) rctLine, rctLine.ReceiptType == "RN" ? "The conversion rule from the {0} purchase unit to the {1} base unit has been modified for the {2} item. The base quantity of the item in the purchase return will not match the base quantity in the {3} purchase receipt. To process the item return using the current conversion rule, you can include the item in a purchase return that is not linked to the original purchase receipt." : "The conversion rule from the {0} purchase unit to the {1} base unit has been modified for the {2} item. The base quantity of the item in the correction receipt will not match the base quantity in the original purchase receipt ({3}). To correct the original purchase receipt, the conversion rule from the {0} purchase unit to the {1} base unit should be the same as in the original purchase receipt.", (PXErrorLevel) 4, new object[4]
    {
      (object) parent.UOM.TrimEnd(),
      (object) inventoryItem.BaseUnit.TrimEnd(),
      (object) inventoryItem.InventoryCD.TrimEnd(),
      (object) parent.ReceiptNbr.TrimEnd()
    })))
      throw new PXRowPersistingException("baseReceiptQty", (object) rctLine.BaseReceiptQty, rctLine.ReceiptType == "RN" ? "The conversion rule from the {0} purchase unit to the {1} base unit has been modified for the {2} item. The base quantity of the item in the purchase return will not match the base quantity in the {3} purchase receipt. To process the item return using the current conversion rule, you can include the item in a purchase return that is not linked to the original purchase receipt." : "The conversion rule from the {0} purchase unit to the {1} base unit has been modified for the {2} item. The base quantity of the item in the correction receipt will not match the base quantity in the original purchase receipt ({3}). To correct the original purchase receipt, the conversion rule from the {0} purchase unit to the {1} base unit should be the same as in the original purchase receipt.", new object[4]
      {
        (object) parent.UOM.TrimEnd(),
        (object) inventoryItem.BaseUnit.TrimEnd(),
        (object) inventoryItem.InventoryCD.TrimEnd(),
        (object) parent.ReceiptNbr.TrimEnd()
      });
  }

  public virtual bool IsLotSerTracked(IPOReturnLineSource line)
  {
    if (!string.IsNullOrEmpty(line.LotSerialNbr))
      return true;
    INLotSerClass inLotSerClass = INLotSerClass.PK.Find((PXGraph) this, PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, line.InventoryID)?.LotSerClassID);
    return (!(((PXSelectBase<POReceipt>) this.Document).Current?.ReceiptType == "RN") || !(inLotSerClass?.LotSerAssign == "U")) && !EnumerableExtensions.IsIn<string>(inLotSerClass?.LotSerTrack, (string) null, "N");
  }

  public virtual void CopyFromOrigReceiptLine(
    POReceiptLine destLine,
    IPOReturnLineSource srcLine,
    bool preserveLineType,
    bool? returnOrigCost)
  {
    destLine.BranchID = srcLine.BranchID;
    destLine.OrigReceiptType = srcLine.ReceiptType;
    destLine.OrigReceiptNbr = srcLine.ReceiptNbr;
    destLine.OrigReceiptLineNbr = srcLine.LineNbr;
    destLine.POType = srcLine.POType;
    destLine.PONbr = srcLine.PONbr;
    destLine.POLineNbr = srcLine.POLineNbr;
    destLine.InventoryID = srcLine.InventoryID;
    destLine.AccrueCost = srcLine.AccrueCost;
    destLine.DropshipExpenseRecording = srcLine.DropshipExpenseRecording;
    destLine.SubItemID = srcLine.SubItemID;
    destLine.SiteID = srcLine.SiteID;
    destLine.LocationID = srcLine.LocationID;
    destLine.LineType = !preserveLineType ? (POLineType.IsService(srcLine.LineType) ? "SV" : (POLineType.IsStock(srcLine.LineType) ? "GI" : (POLineType.IsNonStock(srcLine.LineType) ? "NS" : "FT"))) : srcLine.LineType;
    destLine.OrigReceiptLineType = srcLine.LineType;
    destLine.UOM = srcLine.UOM;
    POReceiptLine poReceiptLine1 = destLine;
    Decimal? nullable1 = srcLine.ReceiptQty;
    Decimal? returnedQty1 = srcLine.ReturnedQty;
    Decimal? nullable2 = nullable1.HasValue & returnedQty1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - returnedQty1.GetValueOrDefault()) : new Decimal?();
    poReceiptLine1.ReceiptQty = nullable2;
    POReceiptLine poReceiptLine2 = destLine;
    Decimal? baseReceiptQty = srcLine.BaseReceiptQty;
    nullable1 = srcLine.BaseReturnedQty;
    Decimal? nullable3 = baseReceiptQty.HasValue & nullable1.HasValue ? new Decimal?(baseReceiptQty.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    poReceiptLine2.BaseReceiptQty = nullable3;
    POReceiptLine poReceiptLine3 = destLine;
    nullable1 = srcLine.BaseReceiptQty;
    Decimal? baseReturnedQty = srcLine.BaseReturnedQty;
    Decimal? nullable4 = nullable1.HasValue & baseReturnedQty.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - baseReturnedQty.GetValueOrDefault()) : new Decimal?();
    poReceiptLine3.BaseOrigQty = nullable4;
    destLine.ExpenseAcctID = srcLine.ExpenseAcctID;
    destLine.ExpenseSubID = srcLine.ExpenseSubID;
    destLine.POAccrualAcctID = srcLine.POAccrualAcctID;
    destLine.POAccrualSubID = srcLine.POAccrualSubID;
    destLine.POAccrualType = "R";
    destLine.TranDesc = srcLine.TranDesc;
    destLine.AllowComplete = new bool?(false);
    destLine.AllowOpen = new bool?(false);
    destLine.CostCodeID = srcLine.CostCodeID;
    destLine.ProjectID = srcLine.ProjectID;
    destLine.TaskID = srcLine.TaskID;
    destLine.ManualPrice = srcLine.ManualPrice;
    destLine.AllowEditUnitCost = new bool?(!returnOrigCost.GetValueOrDefault());
    destLine.DiscPct = srcLine.DiscPct;
    destLine.IsSpecialOrder = srcLine.IsSpecialOrder;
    destLine.CostCenterID = srcLine.CostCenterID;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = this.MultiCurrencyExt.GetCurrencyInfo(destLine.CuryInfoID);
    Decimal? curyDiscAmt = srcLine.CuryDiscAmt;
    POReceiptLine poReceiptLine4 = destLine;
    Decimal? returnedQty2 = srcLine.ReturnedQty;
    Decimal num1 = 0M;
    Decimal? nullable5;
    Decimal? nullable6;
    Decimal? nullable7;
    if (!(returnedQty2.GetValueOrDefault() == num1 & returnedQty2.HasValue))
    {
      Decimal? receiptQty1 = srcLine.ReceiptQty;
      Decimal num2 = 0M;
      if (!(receiptQty1.GetValueOrDefault() == num2 & receiptQty1.HasValue))
      {
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = currencyInfo1;
        Decimal? nullable8 = curyDiscAmt;
        Decimal? receiptQty2 = srcLine.ReceiptQty;
        nullable5 = srcLine.ReturnedQty;
        nullable6 = receiptQty2.HasValue & nullable5.HasValue ? new Decimal?(receiptQty2.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable9;
        if (!(nullable8.HasValue & nullable6.HasValue))
        {
          nullable5 = new Decimal?();
          nullable9 = nullable5;
        }
        else
          nullable9 = new Decimal?(nullable8.GetValueOrDefault() * nullable6.GetValueOrDefault());
        Decimal? nullable10 = nullable9;
        nullable1 = srcLine.ReceiptQty;
        Decimal? nullable11;
        if (!(nullable10.HasValue & nullable1.HasValue))
        {
          nullable6 = new Decimal?();
          nullable11 = nullable6;
        }
        else
          nullable11 = new Decimal?(nullable10.GetValueOrDefault() / nullable1.GetValueOrDefault());
        nullable6 = nullable11;
        Decimal valueOrDefault = nullable6.GetValueOrDefault();
        nullable7 = new Decimal?(currencyInfo2.RoundCury(valueOrDefault));
        goto label_10;
      }
    }
    nullable7 = curyDiscAmt;
label_10:
    poReceiptLine4.CuryDiscAmt = nullable7;
    destLine.CuryUnitCost = srcLine.CuryUnitCost;
    POReceiptLine poReceiptLine5 = destLine;
    Decimal? nullable12 = srcLine.ReceiptQty;
    Decimal num3 = 0M;
    Decimal? nullable13;
    if (nullable12.GetValueOrDefault() == num3 & nullable12.HasValue)
    {
      nullable13 = srcLine.TranUnitCost;
    }
    else
    {
      nullable12 = srcLine.TranCostFinal;
      nullable1 = srcLine.ReceiptQty;
      if (!(nullable12.HasValue & nullable1.HasValue))
      {
        nullable6 = new Decimal?();
        nullable13 = nullable6;
      }
      else
        nullable13 = new Decimal?(nullable12.GetValueOrDefault() / nullable1.GetValueOrDefault());
    }
    poReceiptLine5.TranUnitCost = nullable13;
    nullable1 = destLine.TranUnitCost;
    if (nullable1.HasValue)
    {
      POReceiptLine poReceiptLine6 = destLine;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = currencyInfo1;
      nullable1 = destLine.TranUnitCost;
      Decimal valueOrDefault = nullable1.GetValueOrDefault();
      Decimal? nullable14 = new Decimal?(currencyInfo3.CuryConvCuryRaw(valueOrDefault));
      poReceiptLine6.CuryTranUnitCost = nullable14;
      nullable1 = destLine.CuryTranUnitCost;
      if (nullable1.HasValue)
      {
        POReceiptLine poReceiptLine7 = destLine;
        nullable1 = destLine.CuryTranUnitCost;
        Decimal? nullable15 = new Decimal?(Math.Round(nullable1.Value, 6, MidpointRounding.AwayFromZero));
        poReceiptLine7.CuryTranUnitCost = nullable15;
      }
    }
    Decimal? curyExtCost = srcLine.CuryExtCost;
    POReceiptLine poReceiptLine8 = destLine;
    nullable1 = srcLine.ReturnedQty;
    Decimal num4 = 0M;
    Decimal? nullable16;
    if (!(nullable1.GetValueOrDefault() == num4 & nullable1.HasValue))
    {
      nullable1 = srcLine.ReceiptQty;
      Decimal num5 = 0M;
      if (!(nullable1.GetValueOrDefault() == num5 & nullable1.HasValue))
      {
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo4 = currencyInfo1;
        nullable6 = curyExtCost;
        nullable5 = srcLine.ReceiptQty;
        Decimal? nullable17 = srcLine.ReturnedQty;
        Decimal? nullable18 = nullable5.HasValue & nullable17.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable17.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable19;
        if (!(nullable6.HasValue & nullable18.HasValue))
        {
          nullable17 = new Decimal?();
          nullable19 = nullable17;
        }
        else
          nullable19 = new Decimal?(nullable6.GetValueOrDefault() * nullable18.GetValueOrDefault());
        nullable1 = nullable19;
        nullable12 = srcLine.ReceiptQty;
        Decimal? nullable20;
        if (!(nullable1.HasValue & nullable12.HasValue))
        {
          nullable18 = new Decimal?();
          nullable20 = nullable18;
        }
        else
          nullable20 = new Decimal?(nullable1.GetValueOrDefault() / nullable12.GetValueOrDefault());
        nullable18 = nullable20;
        Decimal valueOrDefault = nullable18.GetValueOrDefault();
        nullable16 = new Decimal?(currencyInfo4.RoundCury(valueOrDefault));
        goto label_28;
      }
    }
    nullable16 = curyExtCost;
label_28:
    poReceiptLine8.CuryExtCost = nullable16;
  }

  public virtual void AddPOOrderReceipt(string aOrderType, string aOrderNbr)
  {
    POOrderReceipt poOrderReceipt1 = (POOrderReceipt) null;
    foreach (PXResult<POOrderReceipt> pxResult in ((PXSelectBase<POOrderReceipt>) this.ReceiptOrders).Select(Array.Empty<object>()))
    {
      POOrderReceipt poOrderReceipt2 = PXResult<POOrderReceipt>.op_Implicit(pxResult);
      if (poOrderReceipt2.POType == aOrderType && poOrderReceipt2.PONbr == aOrderNbr)
        poOrderReceipt1 = poOrderReceipt2;
    }
    if (poOrderReceipt1 != null)
      return;
    POOrder poOrder = PXResultset<POOrder>.op_Implicit(PXSelectBase<POOrder, PXSelectReadonly<POOrder, Where<POOrder.orderType, Equal<Required<POOrder.orderType>>, And<POOrder.orderNbr, Equal<Required<POOrder.orderNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) aOrderType,
      (object) aOrderNbr
    }));
    if (poOrder == null)
      return;
    ((PXSelectBase<POOrderReceipt>) this.ReceiptOrders).Insert(new POOrderReceipt()
    {
      POType = aOrderType,
      PONbr = aOrderNbr,
      OrderNoteID = poOrder.NoteID
    });
    POReceipt current = ((PXSelectBase<POReceipt>) this.Document).Current;
    current.POType = poOrder.OrderType;
    if (!(poOrder.OrderType == "DP"))
      return;
    current.ShipToBAccountID = poOrder.ShipToBAccountID;
    current.ShipToLocationID = poOrder.ShipToLocationID;
    ((PXSelectBase<POReceipt>) this.Document).Update(current);
  }

  protected virtual void CheckRctForPOQuantityRule(
    PXCache sender,
    POReceiptLine row,
    bool displayAsWarning)
  {
    this.CheckRctForPOQuantityRule(sender, row, displayAsWarning, (POLine) null);
  }

  protected virtual void CheckRctForPOQuantityRule(
    PXCache sender,
    POReceiptLine row,
    bool displayAsWarning,
    POLine aOriginLine)
  {
    if (row.Released.GetValueOrDefault())
      return;
    POLine poLine = aOriginLine;
    int? nullable1;
    if (poLine != null && !(poLine.OrderType != row.POType) && !(poLine.OrderNbr != row.PONbr))
    {
      int? lineNbr = poLine.LineNbr;
      nullable1 = row.POLineNbr;
      if (lineNbr.GetValueOrDefault() == nullable1.GetValueOrDefault() & lineNbr.HasValue == nullable1.HasValue)
        goto label_5;
    }
    poLine = POLine.PK.Find((PXGraph) this, row.POType, row.PONbr, row.POLineNbr);
label_5:
    if (poLine == null || !EnumerableExtensions.IsIn<string>(poLine.RcptQtyAction, "R", "W"))
      return;
    Decimal? nullable2 = poLine.OrderQty;
    Decimal num1 = 0M;
    if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
      return;
    nullable2 = row.ReceiptQty;
    Decimal num2 = nullable2.Value;
    nullable1 = row.InventoryID;
    if (nullable1.HasValue)
    {
      if (!string.IsNullOrEmpty(poLine.UOM) && !string.IsNullOrEmpty(row.UOM) && poLine.UOM != row.UOM)
        num2 = INUnitAttribute.ConvertFromTo<POReceiptLine.inventoryID>(sender, (object) row, row.UOM, poLine.UOM, num2, INPrecision.QUANTITY);
      foreach (PXResult<POReceiptLine> pxResult in PXSelectBase<POReceiptLine, PXSelect<POReceiptLine, Where<POReceiptLine.receiptType, Equal<Required<POReceiptLine.receiptType>>, And<POReceiptLine.receiptNbr, Equal<Required<POReceiptLine.receiptNbr>>, And<POReceiptLine.lineNbr, NotEqual<Required<POReceiptLine.lineNbr>>, And<POReceiptLine.pOType, Equal<Required<POReceiptLine.pOType>>, And<POReceiptLine.pONbr, Equal<Required<POReceiptLine.pONbr>>, And<POReceiptLine.pOLineNbr, Equal<Required<POReceiptLine.pOLineNbr>>>>>>>>>.Config>.Select((PXGraph) this, new object[6]
      {
        (object) row.ReceiptType,
        (object) row.ReceiptNbr,
        (object) row.LineNbr,
        (object) row.POType,
        (object) row.PONbr,
        (object) row.POLineNbr
      }))
      {
        POReceiptLine poReceiptLine = PXResult<POReceiptLine>.op_Implicit(pxResult);
        if (!string.IsNullOrEmpty(poLine.UOM) && !string.IsNullOrEmpty(poReceiptLine.UOM) && poLine.UOM != poReceiptLine.UOM)
        {
          nullable2 = poReceiptLine.Qty;
          if (nullable2.HasValue)
          {
            Decimal num3 = num2;
            PXCache sender1 = sender;
            POReceiptLine Row = poReceiptLine;
            string uom1 = poReceiptLine.UOM;
            string uom2 = poLine.UOM;
            nullable2 = poReceiptLine.Qty;
            Decimal num4 = nullable2.Value;
            Decimal num5 = INUnitAttribute.ConvertFromTo<POReceiptLine.inventoryID>(sender1, (object) Row, uom1, uom2, num4, INPrecision.QUANTITY);
            num2 = num3 + num5;
          }
        }
      }
    }
    nullable2 = poLine.RcptQtyMin;
    Decimal num6 = nullable2.Value / 100.0M;
    nullable2 = poLine.OpenQty;
    Decimal num7 = nullable2.Value;
    Decimal num8 = PXDBQuantityAttribute.Round(new Decimal?(num6 * num7));
    Decimal num9 = num2;
    nullable2 = poLine.OrderQty;
    Decimal num10 = nullable2.Value;
    Decimal num11 = num9 / num10 * 100.0M;
    POLine referencedPoLine = this.GetReferencedPOLine(row.POType, row.PONbr, row.POLineNbr);
    if (referencedPoLine != null)
    {
      nullable2 = referencedPoLine.ReceivedQty;
      Decimal num12 = nullable2.Value;
      nullable2 = poLine.OrderQty;
      Decimal num13 = nullable2.Value;
      num11 = num12 / num13 * 100.0M;
    }
    if (row.ReceiptType == "RT")
    {
      PXErrorLevel pxErrorLevel = displayAsWarning || !(poLine.RcptQtyAction == "R") ? (PXErrorLevel) 2 : (PXErrorLevel) 4;
      if (num2 < num8)
        sender.RaiseExceptionHandling<POReceiptLine.receiptQty>((object) row, (object) row.ReceiptQty, (Exception) new PXSetPropertyException("Receipt quantity is below then minimum quantity defined in PO Order for this item", pxErrorLevel));
      Decimal num14 = num11;
      nullable2 = poLine.RcptQtyMax;
      Decimal valueOrDefault = nullable2.GetValueOrDefault();
      if (!(num14 > valueOrDefault & nullable2.HasValue))
        return;
      sender.RaiseExceptionHandling<POReceiptLine.receiptQty>((object) row, (object) row.ReceiptQty, (Exception) new PXSetPropertyException("Receipted quantity is above the maximum quantity defined in PO Order for this item", pxErrorLevel));
    }
    else
    {
      if (referencedPoLine == null)
        return;
      nullable2 = referencedPoLine.ReceivedQty;
      Decimal num15 = 0M;
      if (!(nullable2.GetValueOrDefault() < num15 & nullable2.HasValue))
        return;
      sender.RaiseExceptionHandling<POReceiptLine.receiptQty>((object) row, (object) row.ReceiptQty, (Exception) new PXSetPropertyException("Receipt quantity will go negative source PO Order line", (PXErrorLevel) 4));
    }
  }

  public virtual void UpdatePOLineCompleteFlag(
    POReceiptLine row,
    bool isDeleted,
    POLine aOriginLine)
  {
    if (string.IsNullOrEmpty(row.PONbr) || string.IsNullOrEmpty(row.POType))
      return;
    int? nullable1 = row.POLineNbr;
    if (!nullable1.HasValue)
      return;
    POLine poLine1 = this.GetReferencedPOLine(row.POType, row.PONbr, row.POLineNbr);
    POLine poLine2 = aOriginLine;
    if (poLine2 != null && !(poLine2.OrderType != row.POType) && !(poLine2.OrderNbr != row.PONbr))
    {
      nullable1 = poLine2.LineNbr;
      int? poLineNbr = row.POLineNbr;
      if (nullable1.GetValueOrDefault() == poLineNbr.GetValueOrDefault() & nullable1.HasValue == poLineNbr.HasValue)
        goto label_6;
    }
    poLine2 = POLine.PK.Find((PXGraph) this, row.POType, row.PONbr, row.POLineNbr);
label_6:
    if (poLine1 == null || poLine2 == null || poLine2.CompletePOLine != "Q")
      return;
    if (row.ReceiptType == "RT")
    {
      Decimal? nullable2 = poLine2.OrderQty;
      Decimal num1 = nullable2.Value;
      nullable2 = poLine2.RcptQtyThreshold;
      Decimal num2 = nullable2.Value;
      Decimal num3 = num1 * num2 / 100.0M;
      nullable2 = poLine1.ReceivedQty;
      bool flag = nullable2.Value >= num3;
      int num4 = flag ? 1 : 0;
      bool? allowComplete = poLine1.AllowComplete;
      int num5 = allowComplete.GetValueOrDefault() ? 1 : 0;
      if (!(num4 == num5 & allowComplete.HasValue))
      {
        poLine1.AllowComplete = new bool?(flag);
        poLine1 = ((PXSelectBase<POLine>) this.poline).Update(poLine1);
        ((PXSelectBase) this.transactions).View.RequestRefresh();
      }
      POReceiptLine poReceiptLine = row;
      row.AllowOpen = allowComplete = poLine1.AllowComplete;
      bool? nullable3 = allowComplete;
      poReceiptLine.AllowComplete = nullable3;
    }
    else
    {
      bool? nullable4 = poLine1.Completed;
      bool valueOrDefault = nullable4.GetValueOrDefault();
      int num6 = valueOrDefault ? 1 : 0;
      nullable4 = poLine1.AllowComplete;
      int num7 = nullable4.GetValueOrDefault() ? 1 : 0;
      if (!(num6 == num7 & nullable4.HasValue))
      {
        poLine1.AllowComplete = new bool?(valueOrDefault);
        poLine1 = ((PXSelectBase<POLine>) this.poline).Update(poLine1);
        ((PXSelectBase) this.transactions).View.RequestRefresh();
      }
      POReceiptLine poReceiptLine = row;
      row.AllowOpen = nullable4 = poLine1.AllowComplete;
      bool? nullable5 = nullable4;
      poReceiptLine.AllowComplete = nullable5;
    }
  }

  protected virtual void DeleteUnusedReference(POReceiptLine aLine, bool skipReceiptUpdate)
  {
    if (string.IsNullOrEmpty(aLine.POType) || string.IsNullOrEmpty(aLine.PONbr))
      return;
    string poType = aLine.POType;
    string poNbr = aLine.PONbr;
    POReceipt current = ((PXSelectBase<POReceipt>) this.Document).Current;
    POOrderReceipt poOrderReceipt1 = (POOrderReceipt) null;
    foreach (PXResult<POOrderReceipt> pxResult in ((PXSelectBase<POOrderReceipt>) this.ReceiptOrders).Select(Array.Empty<object>()))
    {
      POOrderReceipt poOrderReceipt2 = PXResult<POOrderReceipt>.op_Implicit(pxResult);
      if (poOrderReceipt2.POType == poType && poOrderReceipt2.PONbr == poNbr)
        poOrderReceipt1 = poOrderReceipt2;
    }
    foreach (PXResult<POReceiptLine> pxResult in PXSelectBase<POReceiptLine, PXSelect<POReceiptLine, Where<POReceiptLine.receiptType, Equal<Required<POReceipt.receiptType>>, And<POReceiptLine.receiptNbr, Equal<Required<POReceipt.receiptNbr>>, And<POReceiptLine.pOType, Equal<Required<POReceiptLine.pOType>>, And<POReceiptLine.pONbr, Equal<Required<POReceiptLine.pONbr>>>>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) aLine.ReceiptType,
      (object) aLine.ReceiptNbr,
      (object) poType,
      (object) poNbr
    }))
    {
      int? lineNbr1 = PXResult<POReceiptLine>.op_Implicit(pxResult).LineNbr;
      int? lineNbr2 = aLine.LineNbr;
      if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue))
        return;
    }
    if (poOrderReceipt1 != null)
      ((PXSelectBase<POOrderReceipt>) this.ReceiptOrders).Delete(poOrderReceipt1);
    if (skipReceiptUpdate || !(current.POType == aLine.POType))
      return;
    POReceiptLine poReceiptLine1 = (POReceiptLine) null;
    foreach (PXResult<POReceiptLine, POOrder> pxResult in PXSelectBase<POReceiptLine, PXSelectJoin<POReceiptLine, InnerJoin<POOrder, On<POOrder.orderType, Equal<POReceiptLine.pOType>, And<POOrder.orderNbr, Equal<POReceiptLine.pONbr>>>>, Where<POReceiptLine.receiptType, Equal<Required<POReceipt.receiptType>>, And<POReceiptLine.receiptNbr, Equal<Required<POReceipt.receiptNbr>>, And<POReceiptLine.pOType, Equal<Required<POReceiptLine.pOType>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) aLine.ReceiptType,
      (object) aLine.ReceiptNbr,
      (object) poType
    }))
    {
      POReceiptLine poReceiptLine2 = PXResult<POReceiptLine, POOrder>.op_Implicit(pxResult);
      POOrder poOrder = PXResult<POReceiptLine, POOrder>.op_Implicit(pxResult);
      int? nullable1 = poReceiptLine2.LineNbr;
      int? nullable2 = aLine.LineNbr;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      {
        poReceiptLine1 = poReceiptLine2;
        nullable2 = current.ShipToBAccountID;
        if (!nullable2.HasValue)
          return;
        nullable2 = current.ShipToLocationID;
        if (!nullable2.HasValue)
          return;
        nullable2 = current.ShipToBAccountID;
        nullable1 = poOrder.ShipToBAccountID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          nullable1 = current.ShipToLocationID;
          nullable2 = poOrder.ShipToLocationID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
            return;
        }
      }
    }
    current.ShipToBAccountID = new int?();
    current.ShipToLocationID = new int?();
    if (poReceiptLine1 == null)
      current.POType = (string) null;
    ((PXSelectBase<POReceipt>) this.Document).Update(current);
  }

  public virtual void Copy(POReceiptLine aDest, POLine aSrc, Decimal aQtyAdj, Decimal aBaseQtyAdj)
  {
    aDest.BranchID = aSrc.BranchID;
    aDest.POType = aSrc.OrderType;
    aDest.PONbr = aSrc.OrderNbr;
    aDest.POLineNbr = aSrc.LineNbr;
    aDest.IsStockItem = aSrc.IsStockItem;
    aDest.InventoryID = aSrc.InventoryID;
    aDest.AccrueCost = aSrc.AccrueCost;
    aDest.AlternateID = aSrc.AlternateID;
    aDest.SubItemID = aSrc.SubItemID;
    aDest.SiteID = aSrc.SiteID;
    aDest.LineType = aSrc.LineType;
    aDest.DropshipExpenseRecording = aSrc.DropshipExpenseRecording;
    aDest.POAccrualType = aDest.ReceiptType == "RN" ? "R" : aSrc.POAccrualType;
    if (aDest.POAccrualType == "O")
    {
      aDest.POAccrualRefNoteID = aSrc.OrderNoteID;
      aDest.POAccrualLineNbr = aSrc.LineNbr;
    }
    aDest.POAccrualAcctID = aSrc.POAccrualAcctID;
    aDest.POAccrualSubID = aSrc.POAccrualSubID;
    if (((PXSelectBase<POReceipt>) this.Document).Current.ReceiptType == "RN")
      aDest.LineType = POLineType.IsStock(aSrc.LineType) ? "GI" : (POLineType.IsNonStock(aSrc.LineType) ? "NS" : "FT");
    if (aDest.LineType == "GI" || aDest.LineType == "GR")
      aDest.OrigPlanType = "70";
    else if (aDest.LineType == "GS")
      aDest.OrigPlanType = "76";
    else if (aDest.LineType == "GF")
      aDest.OrigPlanType = "F7";
    else if (aDest.LineType == "GP")
      aDest.OrigPlanType = "74";
    else if (aDest.LineType == "GM")
      aDest.OrigPlanType = "M4";
    aDest.TranDesc = aSrc.TranDesc;
    aDest.UnitVolume = new Decimal?();
    aDest.UnitWeight = new Decimal?();
    aDest.UOM = aSrc.UOM;
    aDest.VendorID = aSrc.VendorID;
    aDest.AlternateID = aSrc.AlternateID;
    bool? nullable1;
    if (((PXSelectBase<POSetup>) this.posetup).Current.DefaultReceiptQty == "Z" && aSrc.LineType != "FT")
    {
      aDest.BaseQty = new Decimal?(0M);
      aDest.Qty = new Decimal?(0M);
    }
    else
    {
      nullable1 = aSrc.OrderedQtyAltered;
      if (nullable1.GetValueOrDefault())
      {
        aDest.UOM = aSrc.OverridenUOM;
        aDest.BaseQty = aSrc.BaseOverridenQty;
        aDest.Qty = aSrc.OverridenQty;
      }
      else
      {
        POReceiptLine poReceiptLine1 = aDest;
        Decimal? nullable2;
        if (!(((PXSelectBase<POReceipt>) this.Document).Current.ReceiptType == "RN"))
        {
          Decimal? toReceiveBaseQty = aSrc.LeftToReceiveBaseQty;
          Decimal num = aBaseQtyAdj;
          nullable2 = toReceiveBaseQty.HasValue ? new Decimal?(toReceiveBaseQty.GetValueOrDefault() - num) : new Decimal?();
        }
        else
        {
          Decimal? baseReceivedQty = aSrc.BaseReceivedQty;
          Decimal num = aBaseQtyAdj;
          nullable2 = baseReceivedQty.HasValue ? new Decimal?(baseReceivedQty.GetValueOrDefault() - num) : new Decimal?();
        }
        poReceiptLine1.BaseQty = nullable2;
        POReceiptLine poReceiptLine2 = aDest;
        Decimal? nullable3;
        if (!(((PXSelectBase<POReceipt>) this.Document).Current.ReceiptType == "RN"))
        {
          Decimal? leftToReceiveQty = aSrc.LeftToReceiveQty;
          Decimal num = aQtyAdj;
          nullable3 = leftToReceiveQty.HasValue ? new Decimal?(leftToReceiveQty.GetValueOrDefault() - num) : new Decimal?();
        }
        else
        {
          Decimal? receivedQty = aSrc.ReceivedQty;
          Decimal num = aQtyAdj;
          nullable3 = receivedQty.HasValue ? new Decimal?(receivedQty.GetValueOrDefault() - num) : new Decimal?();
        }
        poReceiptLine2.Qty = nullable3;
      }
    }
    aDest.ExpenseAcctID = aSrc.ExpenseAcctID;
    aDest.ExpenseSubID = aSrc.ExpenseSubID;
    POReceiptLine poReceiptLine3 = aDest;
    POReceiptLine poReceiptLine4 = aDest;
    nullable1 = new bool?(aSrc.CompletePOLine == "Q");
    bool? nullable4 = nullable1;
    poReceiptLine4.AllowOpen = nullable4;
    bool? nullable5 = nullable1;
    poReceiptLine3.AllowComplete = nullable5;
    aDest.ManualPrice = new bool?(false);
    bool flag = POLineType.UsePOAccrual(aDest.LineType) && aDest.POAccrualType == "R" || !POLineType.UsePOAccrual(aDest.LineType) && !string.IsNullOrEmpty(aDest.POType) && !string.IsNullOrEmpty(aDest.PONbr) && aDest.POLineNbr.HasValue;
    aDest.AllowEditUnitCost = flag ? aSrc.AllowEditUnitCostInPR : new bool?(false);
    aDest.DiscPct = aSrc.DiscPct;
    this.CalculateAmountFromPOLine(aDest, aSrc);
    aDest.ProjectID = aSrc.ProjectID;
    aDest.TaskID = aSrc.TaskID;
    aDest.CostCodeID = aSrc.CostCodeID;
    aDest.IsSpecialOrder = aSrc.IsSpecialOrder;
    aDest.CostCenterID = aSrc.CostCenterID;
  }

  public virtual void CalculateAmountFromPOLine(POReceiptLine aDest, POLine aSrc)
  {
    ARReleaseProcess.Amount expensePostingAmount = APReleaseProcess.GetExpensePostingAmount((PXGraph) this, aSrc);
    Decimal valueOrDefault1 = aSrc.UnitCost.GetValueOrDefault();
    Decimal valueOrDefault2 = expensePostingAmount.Base.GetValueOrDefault();
    Decimal valueOrDefault3 = expensePostingAmount.Cury.GetValueOrDefault();
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = this.MultiCurrencyExt.GetCurrencyInfo(aSrc.CuryInfoID);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = this.MultiCurrencyExt.GetCurrencyInfo(aDest.CuryInfoID);
    if (this.AllowNonBaseCurrency() && currencyInfo2.CuryID == currencyInfo1.CuryID)
    {
      aDest.CuryUnitCost = new Decimal?(aSrc.CuryUnitCost.GetValueOrDefault());
      POReceiptLine poReceiptLine1 = aDest;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = currencyInfo2;
      Decimal? nullable1 = aSrc.OrderQty;
      Decimal num1 = 0M;
      Decimal? nullable2;
      Decimal? nullable3;
      if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
      {
        nullable1 = aSrc.CuryLineAmt;
        nullable3 = new Decimal?(nullable1.GetValueOrDefault());
      }
      else
      {
        Decimal valueOrDefault4 = aSrc.CuryLineAmt.GetValueOrDefault();
        Decimal? qty = aDest.Qty;
        nullable1 = qty.HasValue ? new Decimal?(valueOrDefault4 * qty.GetValueOrDefault()) : new Decimal?();
        nullable2 = aSrc.OrderQty;
        nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / nullable2.GetValueOrDefault()) : new Decimal?();
      }
      nullable2 = nullable3;
      Decimal valueOrDefault5 = nullable2.GetValueOrDefault();
      Decimal? nullable4 = new Decimal?(currencyInfo3.RoundCury(valueOrDefault5));
      poReceiptLine1.CuryExtCost = nullable4;
      POReceiptLine poReceiptLine2 = aDest;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo4 = currencyInfo2;
      nullable2 = aSrc.OrderQty;
      Decimal num2 = 0M;
      Decimal? nullable5;
      if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
      {
        nullable5 = new Decimal?(valueOrDefault3);
      }
      else
      {
        Decimal num3 = valueOrDefault3;
        Decimal? nullable6 = aDest.Qty;
        nullable2 = nullable6.HasValue ? new Decimal?(num3 * nullable6.GetValueOrDefault()) : new Decimal?();
        nullable1 = aSrc.OrderQty;
        if (!(nullable2.HasValue & nullable1.HasValue))
        {
          nullable6 = new Decimal?();
          nullable5 = nullable6;
        }
        else
          nullable5 = new Decimal?(nullable2.GetValueOrDefault() / nullable1.GetValueOrDefault());
      }
      nullable1 = nullable5;
      Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
      Decimal? nullable7 = new Decimal?(currencyInfo4.RoundCury(valueOrDefault6));
      poReceiptLine2.CuryTranCost = nullable7;
      POReceiptLine poReceiptLine3 = aDest;
      nullable1 = aSrc.OrderQty;
      Decimal num4 = 0M;
      Decimal? nullable8;
      if (nullable1.GetValueOrDefault() == num4 & nullable1.HasValue)
      {
        nullable8 = new Decimal?(valueOrDefault3);
      }
      else
      {
        Decimal num5 = valueOrDefault3;
        nullable1 = aSrc.OrderQty;
        if (!nullable1.HasValue)
        {
          nullable2 = new Decimal?();
          nullable8 = nullable2;
        }
        else
          nullable8 = new Decimal?(num5 / nullable1.GetValueOrDefault());
      }
      poReceiptLine3.CuryTranUnitCost = nullable8;
      nullable1 = aDest.CuryTranUnitCost;
      if (!nullable1.HasValue)
        return;
      POReceiptLine poReceiptLine4 = aDest;
      nullable1 = aDest.CuryTranUnitCost;
      Decimal? nullable9 = new Decimal?(Math.Round(nullable1.Value, 6, MidpointRounding.AwayFromZero));
      poReceiptLine4.CuryTranUnitCost = nullable9;
    }
    else if (currencyInfo2.CuryID == currencyInfo2.BaseCuryID)
    {
      aDest.CuryUnitCost = new Decimal?(valueOrDefault1);
      POReceiptLine poReceiptLine5 = aDest;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo5 = currencyInfo2;
      Decimal? orderQty = aSrc.OrderQty;
      Decimal num6 = 0M;
      Decimal? nullable10;
      Decimal? nullable11;
      Decimal? nullable12;
      Decimal? nullable13;
      Decimal? nullable14;
      if (orderQty.GetValueOrDefault() == num6 & orderQty.HasValue)
      {
        nullable14 = aSrc.LineAmt;
      }
      else
      {
        nullable10 = aSrc.LineAmt;
        nullable11 = aDest.Qty;
        nullable12 = nullable10.HasValue & nullable11.HasValue ? new Decimal?(nullable10.GetValueOrDefault() * nullable11.GetValueOrDefault()) : new Decimal?();
        nullable13 = aSrc.OrderQty;
        if (!(nullable12.HasValue & nullable13.HasValue))
        {
          nullable11 = new Decimal?();
          nullable14 = nullable11;
        }
        else
          nullable14 = new Decimal?(nullable12.GetValueOrDefault() / nullable13.GetValueOrDefault());
      }
      nullable13 = nullable14;
      Decimal valueOrDefault7 = nullable13.GetValueOrDefault();
      Decimal? nullable15 = new Decimal?(currencyInfo5.RoundCury(valueOrDefault7));
      poReceiptLine5.CuryExtCost = nullable15;
      POReceiptLine poReceiptLine6 = aDest;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo6 = currencyInfo2;
      nullable13 = aSrc.OrderQty;
      Decimal num7 = 0M;
      Decimal? nullable16;
      if (nullable13.GetValueOrDefault() == num7 & nullable13.HasValue)
      {
        nullable16 = new Decimal?(valueOrDefault2);
      }
      else
      {
        Decimal num8 = valueOrDefault2;
        nullable11 = aDest.Qty;
        Decimal? nullable17;
        if (!nullable11.HasValue)
        {
          nullable10 = new Decimal?();
          nullable17 = nullable10;
        }
        else
          nullable17 = new Decimal?(num8 * nullable11.GetValueOrDefault());
        nullable13 = nullable17;
        nullable12 = aSrc.OrderQty;
        if (!(nullable13.HasValue & nullable12.HasValue))
        {
          nullable11 = new Decimal?();
          nullable16 = nullable11;
        }
        else
          nullable16 = new Decimal?(nullable13.GetValueOrDefault() / nullable12.GetValueOrDefault());
      }
      nullable12 = nullable16;
      Decimal valueOrDefault8 = nullable12.GetValueOrDefault();
      Decimal? nullable18 = new Decimal?(currencyInfo6.RoundCury(valueOrDefault8));
      poReceiptLine6.CuryTranCost = nullable18;
      POReceiptLine poReceiptLine7 = aDest;
      nullable12 = aSrc.OrderQty;
      Decimal num9 = 0M;
      Decimal? nullable19;
      if (nullable12.GetValueOrDefault() == num9 & nullable12.HasValue)
      {
        nullable19 = new Decimal?(valueOrDefault2);
      }
      else
      {
        Decimal num10 = valueOrDefault2;
        nullable12 = aSrc.OrderQty;
        if (!nullable12.HasValue)
        {
          nullable13 = new Decimal?();
          nullable19 = nullable13;
        }
        else
          nullable19 = new Decimal?(num10 / nullable12.GetValueOrDefault());
      }
      poReceiptLine7.CuryTranUnitCost = nullable19;
      nullable12 = aDest.CuryTranUnitCost;
      if (!nullable12.HasValue)
        return;
      POReceiptLine poReceiptLine8 = aDest;
      nullable12 = aDest.CuryTranUnitCost;
      Decimal? nullable20 = new Decimal?(Math.Round(nullable12.Value, 6, MidpointRounding.AwayFromZero));
      poReceiptLine8.CuryTranUnitCost = nullable20;
    }
    else
    {
      aDest.UnitCost = new Decimal?(valueOrDefault1);
      POReceiptLine poReceiptLine9 = aDest;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo7 = currencyInfo2;
      Decimal? orderQty = aSrc.OrderQty;
      Decimal num11 = 0M;
      Decimal? nullable21;
      Decimal? nullable22;
      Decimal? nullable23;
      Decimal? nullable24;
      Decimal? nullable25;
      if (orderQty.GetValueOrDefault() == num11 & orderQty.HasValue)
      {
        nullable25 = aSrc.LineAmt;
      }
      else
      {
        nullable21 = aSrc.LineAmt;
        nullable22 = aDest.Qty;
        nullable23 = nullable21.HasValue & nullable22.HasValue ? new Decimal?(nullable21.GetValueOrDefault() * nullable22.GetValueOrDefault()) : new Decimal?();
        nullable24 = aSrc.OrderQty;
        if (!(nullable23.HasValue & nullable24.HasValue))
        {
          nullable22 = new Decimal?();
          nullable25 = nullable22;
        }
        else
          nullable25 = new Decimal?(nullable23.GetValueOrDefault() / nullable24.GetValueOrDefault());
      }
      nullable24 = nullable25;
      Decimal valueOrDefault9 = nullable24.GetValueOrDefault();
      Decimal? nullable26 = new Decimal?(currencyInfo7.RoundCury(valueOrDefault9));
      poReceiptLine9.ExtCost = nullable26;
      POReceiptLine poReceiptLine10 = aDest;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo8 = currencyInfo2;
      nullable24 = aSrc.OrderQty;
      Decimal num12 = 0M;
      Decimal? nullable27;
      if (nullable24.GetValueOrDefault() == num12 & nullable24.HasValue)
      {
        nullable27 = new Decimal?(valueOrDefault2);
      }
      else
      {
        Decimal num13 = valueOrDefault2;
        nullable22 = aDest.Qty;
        Decimal? nullable28;
        if (!nullable22.HasValue)
        {
          nullable21 = new Decimal?();
          nullable28 = nullable21;
        }
        else
          nullable28 = new Decimal?(num13 * nullable22.GetValueOrDefault());
        nullable24 = nullable28;
        nullable23 = aSrc.OrderQty;
        if (!(nullable24.HasValue & nullable23.HasValue))
        {
          nullable22 = new Decimal?();
          nullable27 = nullable22;
        }
        else
          nullable27 = new Decimal?(nullable24.GetValueOrDefault() / nullable23.GetValueOrDefault());
      }
      nullable23 = nullable27;
      Decimal valueOrDefault10 = nullable23.GetValueOrDefault();
      Decimal? nullable29 = new Decimal?(currencyInfo8.RoundCury(valueOrDefault10));
      poReceiptLine10.TranCost = nullable29;
      POReceiptLine poReceiptLine11 = aDest;
      nullable23 = aSrc.OrderQty;
      Decimal num14 = 0M;
      Decimal? nullable30;
      if (nullable23.GetValueOrDefault() == num14 & nullable23.HasValue)
      {
        nullable30 = new Decimal?(valueOrDefault2);
      }
      else
      {
        Decimal num15 = valueOrDefault2;
        nullable23 = aSrc.OrderQty;
        if (!nullable23.HasValue)
        {
          nullable24 = new Decimal?();
          nullable30 = nullable24;
        }
        else
          nullable30 = new Decimal?(num15 / nullable23.GetValueOrDefault());
      }
      poReceiptLine11.TranUnitCost = nullable30;
      POReceiptLine poReceiptLine12 = aDest;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo9 = currencyInfo2;
      nullable23 = aDest.UnitCost;
      Decimal valueOrDefault11 = nullable23.GetValueOrDefault();
      int? precision = new int?(CommonSetupDecPl.PrcCst);
      Decimal? nullable31 = new Decimal?(currencyInfo9.CuryConvCury(valueOrDefault11, precision));
      poReceiptLine12.CuryUnitCost = nullable31;
      POReceiptLine poReceiptLine13 = aDest;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo10 = currencyInfo2;
      nullable23 = aDest.ExtCost;
      Decimal valueOrDefault12 = nullable23.GetValueOrDefault();
      Decimal? nullable32 = new Decimal?(currencyInfo10.CuryConvCury(valueOrDefault12));
      poReceiptLine13.CuryExtCost = nullable32;
      POReceiptLine poReceiptLine14 = aDest;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo11 = currencyInfo2;
      nullable23 = aDest.TranCost;
      Decimal valueOrDefault13 = nullable23.GetValueOrDefault();
      Decimal? nullable33 = new Decimal?(currencyInfo11.CuryConvCury(valueOrDefault13));
      poReceiptLine14.CuryTranCost = nullable33;
      POReceiptLine poReceiptLine15 = aDest;
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo12 = currencyInfo2;
      nullable23 = aDest.TranUnitCost;
      Decimal valueOrDefault14 = nullable23.GetValueOrDefault();
      Decimal? nullable34 = new Decimal?(currencyInfo12.CuryConvCuryRaw(valueOrDefault14));
      poReceiptLine15.CuryTranUnitCost = nullable34;
      nullable23 = aDest.CuryTranUnitCost;
      if (!nullable23.HasValue)
        return;
      POReceiptLine poReceiptLine16 = aDest;
      nullable23 = aDest.CuryTranUnitCost;
      Decimal? nullable35 = new Decimal?(Math.Round(nullable23.Value, 6, MidpointRounding.AwayFromZero));
      poReceiptLine16.CuryTranUnitCost = nullable35;
    }
  }

  protected virtual void ClearUnused(POReceiptLine aLine)
  {
    if (EnumerableExtensions.IsIn<string>(aLine.LineType, "FT", "MC", "NP"))
    {
      aLine.SubItemID = new int?();
      if (aLine.LineType == "MC")
      {
        aLine.InventoryID = new int?();
        aLine.UOM = (string) null;
        aLine.CuryUnitCost = new Decimal?(0M);
      }
      aLine.LocationID = new int?();
    }
    if (!this.IsExpenseAccountRequired(aLine))
    {
      aLine.ExpenseAcctID = new int?();
      aLine.ExpenseSubID = new int?();
    }
    if (this.IsAccrualAccountRequired(aLine))
      return;
    aLine.POAccrualAcctID = new int?();
    aLine.POAccrualSubID = new int?();
  }

  protected virtual bool CanAddOrder(POOrder aOrder, out string message)
  {
    POReceipt current = ((PXSelectBase<POReceipt>) this.Document).Current;
    if (!string.IsNullOrEmpty(current.POType))
    {
      if (current.POType != aOrder.OrderType)
      {
        message = string.Format(PXMessages.LocalizeNoPrefix("Purchase Order {0} {1} cannot be added - it has a type different from other orders in this Receipt"), (object) PXMessages.LocalizeNoPrefix(aOrder.OrderType), (object) aOrder.OrderNbr);
        return false;
      }
      int? nullable1 = current.ShipToBAccountID;
      if (!nullable1.HasValue)
      {
        nullable1 = current.ShipToLocationID;
        if (!nullable1.HasValue)
          goto label_8;
      }
      nullable1 = current.ShipToBAccountID;
      int? nullable2 = aOrder.ShipToBAccountID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        nullable2 = current.ShipToLocationID;
        nullable1 = aOrder.ShipToLocationID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          goto label_8;
      }
      message = string.Format(PXMessages.LocalizeNoPrefix("Purchase Order {0} {1} cannot be added because it has a different shipping destination than other orders in this receipt."), (object) PXMessages.LocalizeNoPrefix(aOrder.OrderType), (object) aOrder.OrderNbr);
      return false;
    }
label_8:
    message = string.Empty;
    return true;
  }

  protected static bool IsPassingFilter(POReceiptEntry.POOrderFilter aFilter, POOrder aOrder)
  {
    if (!string.IsNullOrEmpty(aFilter.OrderType) && aOrder.OrderType != aFilter.OrderType)
      return false;
    int? nullable = aFilter.ShipToBAccountID;
    if (nullable.HasValue)
    {
      nullable = aOrder.ShipToBAccountID;
      int? shipToBaccountId = aFilter.ShipToBAccountID;
      if (!(nullable.GetValueOrDefault() == shipToBaccountId.GetValueOrDefault() & nullable.HasValue == shipToBaccountId.HasValue))
        return false;
    }
    int? shipToLocationId = aFilter.ShipToLocationID;
    if (shipToLocationId.HasValue)
    {
      shipToLocationId = aOrder.ShipToLocationID;
      nullable = aFilter.ShipToLocationID;
      if (!(shipToLocationId.GetValueOrDefault() == nullable.GetValueOrDefault() & shipToLocationId.HasValue == nullable.HasValue))
        return false;
    }
    return true;
  }

  protected static bool IsPassingFilter(POReceiptEntry.POOrderFilter aFilter, POLine aLine)
  {
    return !(aLine.OrderType != aFilter.OrderType) && !(aLine.OrderNbr != aFilter.OrderNbr);
  }

  protected virtual bool HasOrderWithRetainage(POReceipt row)
  {
    return PXResultset<POOrderReceipt>.op_Implicit(PXSelectBase<POOrderReceipt, PXSelectJoin<POOrderReceipt, InnerJoin<POOrder, On<POOrder.orderType, Equal<POOrderReceipt.pOType>, And<POOrder.orderNbr, Equal<POOrderReceipt.pONbr>>>>, Where2<KeysRelation<CompositeKey<Field<POOrderReceipt.receiptType>.IsRelatedTo<POReceipt.receiptType>, Field<POOrderReceipt.receiptNbr>.IsRelatedTo<POReceipt.receiptNbr>>.WithTablesOf<POReceipt, POOrderReceipt>, POReceipt, POOrderReceipt>.SameAsCurrent, And<POOrder.retainageApply, Equal<boolTrue>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) new POReceipt[1]
    {
      row
    }, Array.Empty<object>())) != null;
  }

  public virtual void ReleaseReceipt(
    POReceipt aDoc,
    DocumentList<PX.Objects.IN.INRegister> aINCreated,
    DocumentList<PX.Objects.AP.APInvoice> aAPCreated,
    bool aIsMassProcess)
  {
    Lazy<INRegisterEntryBase> inRegisterEntry = Lazy.By<INRegisterEntryBase>((Func<INRegisterEntryBase>) (() => this.ReleaseContextExt.GetCleanINRegisterEntryWithInsertedHeader(aDoc)));
    using (PX.Objects.PO.GraphExtensions.POReceiptEntryExt.SO2POSync so2PoSync = ((PXGraph) this).GetExtension<PX.Objects.PO.GraphExtensions.POReceiptEntryExt.SO2POSync>().InitReleasingContext())
    {
      ((PXGraph) this).Clear();
      aDoc = this.ActualizeAndValidatePOReceiptForReleasing(aDoc);
      bool? nullable1 = aDoc.Received;
      if (nullable1.GetValueOrDefault())
        this.ValidateReceiptQtyOnRelease();
      this.PreReleaseReceipt(aDoc, inRegisterEntry);
      POReceiptLine poReceiptLine1 = (POReceiptLine) null;
      INTran newline = (INTran) null;
      HashSet<((string, string), (string, string))> valueTupleSet = new HashSet<((string, string), (string, string))>();
      string str = (string) null;
      foreach (PXResult<POReceiptLine> row in this.GetLinesToReleaseQuery().Select(new object[2]
      {
        (object) aDoc.ReceiptType,
        (object) aDoc.ReceiptNbr
      }))
      {
        POReceiptLine poReceiptLine2 = PXResult<POReceiptLine>.op_Implicit(row);
        POReceiptLineSplit split = ((PXResult) row).GetItem<POReceiptLineSplit>();
        INItemPlan copy = PXCache<INItemPlan>.CreateCopy(((PXResult) row).GetItem<INItemPlan>());
        INPlanType inPlanType = INPlanType.PK.Find((PXGraph) this, copy.PlanType);
        if ((object) inPlanType == null)
          inPlanType = new INPlanType();
        INPlanType plantype = inPlanType;
        PX.Objects.IN.INSite inSite = ((PXResult) row).GetItem<PX.Objects.IN.INSite>();
        POLine poLine = this.GetReferencedPOLine(poReceiptLine2.POType, poReceiptLine2.PONbr, poReceiptLine2.POLineNbr);
        POOrder poOrder = ((PXResult) row).GetItem<POOrder>();
        POAddress poAddress = ((PXResult) row).GetItem<POAddress>();
        Decimal? nullable2 = poReceiptLine2.BaseReceiptQty;
        Decimal num1 = 0M;
        if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
        {
          nullable1 = poReceiptLine2.IsCorrection;
          if (!nullable1.GetValueOrDefault() && split.ReceiptNbr == null)
            continue;
        }
        this.ValidateReceiptLineOnRelease(row);
        int num2 = POLineType.IsStockNonDropShip(poReceiptLine2.LineType) ? 1 : 0;
        bool flag1 = POLineType.IsNonStockNonServiceNonDropShip(poReceiptLine2.LineType);
        int num3;
        if (num2 != 0 || flag1 && poReceiptLine2.POAccrualAcctID.HasValue || POLineType.IsProjectDropShip(poReceiptLine2.LineType) && poReceiptLine2.DropshipExpenseRecording == "R")
        {
          nullable1 = poReceiptLine2.IsCorrection;
          bool flag2 = false;
          if (!(nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue))
          {
            nullable1 = poReceiptLine2.IsAdjustedIN;
            if (!nullable1.GetValueOrDefault())
              goto label_15;
          }
          nullable2 = poReceiptLine2.ReceiptQty;
          Decimal num4 = 0M;
          if (nullable2.GetValueOrDefault() == num4 & nullable2.HasValue)
          {
            nullable2 = poReceiptLine2.BaseReceiptQty;
            Decimal num5 = 0M;
            num3 = !(nullable2.GetValueOrDefault() == num5 & nullable2.HasValue) ? 1 : 0;
            goto label_16;
          }
          num3 = 1;
          goto label_16;
        }
label_15:
        num3 = 0;
label_16:
        bool flag3 = num3 != 0;
        int num6;
        if (flag3 && !string.IsNullOrEmpty(plantype.PlanType))
        {
          nullable1 = plantype.DeleteOnEvent;
          num6 = nullable1.GetValueOrDefault() ? 1 : 0;
        }
        else
          num6 = 0;
        bool preserveExistingPlan = num6 != 0;
        this.ProcessPlanOnRelease(((PXSelectBase) this.splits).Cache, (IItemPlanPOReceiptSource) split, copy, plantype, preserveExistingPlan);
        int num7 = !((PXGraph) this).Caches[typeof (POReceiptLine)].ObjectsEqual((object) poReceiptLine1, (object) poReceiptLine2) ? 1 : 0;
        if (num7 != 0)
          this.ReleaseReceiptLine(poReceiptLine2, poLine, poOrder);
        int num8;
        if (split.InventoryID.HasValue)
        {
          int? inventoryId1 = poReceiptLine2.InventoryID;
          int? inventoryId2 = split.InventoryID;
          num8 = !(inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue) ? 1 : 0;
        }
        else
          num8 = 0;
        bool flag4 = num8 != 0;
        if ((num7 | (flag4 ? 1 : 0)) != 0)
        {
          if (flag3)
          {
            INTran inTran1 = new INTran()
            {
              BranchID = poReceiptLine2.OrigTranType == "TRX" ? inSite.BranchID : poReceiptLine2.BranchID,
              TranType = POReceiptType.GetINTranType(aDoc.ReceiptType),
              POReceiptType = poReceiptLine2.ReceiptType,
              POReceiptNbr = poReceiptLine2.ReceiptNbr,
              POReceiptLineNbr = poReceiptLine2.LineNbr,
              POLineType = poReceiptLine2.LineType
            };
            if (poReceiptLine2.OrigTranType == "TRX")
            {
              inTran1.OrigDocType = poReceiptLine2.OrigDocType;
              inTran1.OrigTranType = poReceiptLine2.OrigTranType;
              inTran1.OrigRefNbr = poReceiptLine2.OrigRefNbr;
              inTran1.OrigLineNbr = poReceiptLine2.OrigLineNbr;
              inTran1.AcctID = new int?();
              inTran1.SubID = new int?();
              inTran1.InvtAcctID = new int?();
              inTran1.InvtSubID = new int?();
              inTran1.OrigPlanType = poReceiptLine2.OrigPlanType;
              inTran1.OrigNoteID = poReceiptLine2.OrigNoteID;
              inTran1.OrigToLocationID = poReceiptLine2.OrigToLocationID;
              inTran1.OrigIsLotSerial = poReceiptLine2.OrigIsLotSerial;
              if (str == null)
                str = poReceiptLine2.OrigRefNbr;
            }
            else
            {
              INTran inTran2 = inTran1;
              nullable1 = poReceiptLine2.IsCorrection;
              string origReceiptNbr = nullable1.GetValueOrDefault() ? inRegisterEntry.Value.INRegisterDataMember.Current.OrigReceiptNbr : (string) null;
              inTran2.OrigRefNbr = origReceiptNbr;
              inTran1.AcctID = poReceiptLine2.POAccrualAcctID;
              inTran1.SubID = poReceiptLine2.POAccrualSubID;
              inTran1.ReclassificationProhibited = new bool?(true);
              if (POLineType.IsProjectDropShip(poReceiptLine2.LineType))
              {
                inTran1.COGSAcctID = poReceiptLine2.ExpenseAcctID;
                inTran1.COGSSubID = poReceiptLine2.ExpenseSubID;
              }
              else
              {
                inTran1.InvtAcctID = POLineType.IsStockNonDropShip(poReceiptLine2.LineType) ? new int?() : poReceiptLine2.ExpenseAcctID;
                inTran1.InvtSubID = POLineType.IsStockNonDropShip(poReceiptLine2.LineType) ? new int?() : poReceiptLine2.ExpenseSubID;
              }
              inTran1.OrigPlanType = (string) null;
            }
            INTran inTran3 = inTran1;
            bool? nullable3;
            if (!flag4 || !POLineType.IsStockNonDropShip(poReceiptLine2.LineType))
            {
              nullable3 = poReceiptLine2.IsStockItem;
            }
            else
            {
              nullable1 = new bool?();
              nullable3 = nullable1;
            }
            inTran3.IsStockItem = nullable3;
            inTran1.InventoryID = !flag4 || !POLineType.IsStockNonDropShip(poReceiptLine2.LineType) ? poReceiptLine2.InventoryID : split.InventoryID;
            inTran1.SiteID = poReceiptLine2.SiteID;
            if (flag4 && !POLineType.IsNonStockNonServiceNonDropShip(poReceiptLine2.LineType))
            {
              inTran1.SubItemID = split.SubItemID;
              inTran1.LocationID = split.LocationID;
              inTran1.UOM = split.UOM;
              inTran1.UnitPrice = new Decimal?(0M);
              inTran1.UnitCost = new Decimal?(0M);
              inTran1.TranDesc = (string) null;
            }
            else
            {
              inTran1.SubItemID = poReceiptLine2.SubItemID;
              inTran1.LocationID = poReceiptLine2.LocationID;
              inTran1.UOM = poReceiptLine2.UOM;
              inTran1.TranDesc = poReceiptLine2.TranDesc;
              inTran1.ReasonCode = poReceiptLine2.ReasonCode;
              inTran1.UnitCost = poReceiptLine2.UnitCost;
            }
            inTran1.InvtMult = POLineType.IsProjectDropShip(poReceiptLine2.LineType) ? new short?((short) 0) : poReceiptLine2.InvtMult;
            inTran1.Qty = POLineType.IsStockNonDropShip(poReceiptLine2.LineType) ? new Decimal?(0M) : poReceiptLine2.Qty;
            inTran1.ExpireDate = poReceiptLine2.ExpireDate;
            inTran1.AccrueCost = poReceiptLine2.AccrueCost;
            inTran1.ProjectID = poReceiptLine2.ProjectID;
            inTran1.TaskID = poReceiptLine2.TaskID;
            inTran1.CostCodeID = poReceiptLine2.CostCodeID;
            inTran1.IsIntercompany = poReceiptLine2.IsIntercompany;
            so2PoSync.UpdateSOOrderLink(inTran1, poLine, poReceiptLine2);
            inRegisterEntry.Value.CostCenterDispatcherExt?.SetInventorySource(inTran1);
            try
            {
              newline = inRegisterEntry.Value.LSSelectDataMember.Insert(inTran1);
            }
            catch (PXException ex)
            {
              throw new ErrorProcessingEntityException(((PXGraph) this).Caches[((object) poReceiptLine2).GetType()], (IBqlTable) poReceiptLine2, ex);
            }
          }
          else
            newline = (INTran) null;
          poLine = this.UpdateReceiptLineOnRelease(row, poLine);
          so2PoSync.ProcessDemandsOnRelease(poReceiptLine2, poLine, poAddress);
        }
        poReceiptLine1 = poReceiptLine2;
        if (newline != null && !string.IsNullOrEmpty(split.ReceiptNbr) && !flag1)
        {
          INTranSplit inTranSplit = INTranSplit.FromINTran(newline);
          inTranSplit.SplitLineNbr = new int?();
          inTranSplit.SubItemID = split.SubItemID;
          inTranSplit.LocationID = split.LocationID;
          inTranSplit.LotSerialNbr = split.LotSerialNbr;
          inTranSplit.InventoryID = split.InventoryID;
          inTranSplit.UOM = split.UOM;
          inTranSplit.Qty = split.Qty;
          inTranSplit.ExpireDate = split.ExpireDate;
          inTranSplit.POLineType = poLine != null ? poLine.LineType : poReceiptLine2.LineType;
          inTranSplit.OrigPlanType = poReceiptLine2.OrigTranType == "TRX" ? poReceiptLine2.OrigPlanType : (string) null;
          if (preserveExistingPlan)
          {
            inTranSplit.PlanID = copy.PlanID;
            copy.OrigPlanID = new long?();
            copy.OrigPlanType = (string) null;
            copy.OrigNoteID = new Guid?();
            copy.OrigPlanLevel = new int?();
            copy.KitInventoryID = new int?();
            copy.IgnoreOrigPlan = new bool?(false);
            copy.Reverse = new bool?(false);
            copy.RefNoteID = inRegisterEntry.Value.INRegisterDataMember.Current.NoteID;
            copy.RefEntityType = typeof (PX.Objects.IN.INRegister).FullName;
            ((PXGraph) inRegisterEntry.Value).Caches[typeof (INItemPlan)].Update((object) copy);
          }
          try
          {
            INTranSplit newSplit = inRegisterEntry.Value.INTranSplitDataMember.Insert(inTranSplit);
            so2PoSync.ReattachDemandPlansToIN(inRegisterEntry.Value, newSplit);
            ((PXSelectBase) inRegisterEntry.Value.INTranSplitDataMember).Cache.RaiseRowUpdated((object) newSplit, (object) newSplit);
          }
          catch (PXException ex)
          {
            throw new ErrorProcessingEntityException(((PXGraph) this).Caches[((object) poReceiptLine2).GetType()], (IBqlTable) poReceiptLine2, ex);
          }
        }
        else if (preserveExistingPlan)
          this.ThrowPlanNotReattachedError(poReceiptLine2, split);
        if (newline != null)
        {
          int? inventoryId3 = newline.InventoryID;
          int? inventoryId4 = poReceiptLine2.InventoryID;
          if (inventoryId3.GetValueOrDefault() == inventoryId4.GetValueOrDefault() & inventoryId3.HasValue == inventoryId4.HasValue)
            newline.TranCost = poReceiptLine2.TranCostFinal;
        }
        so2PoSync.TryFinalizeDemand(aDoc, poReceiptLine2, poLine, newline);
        if (aDoc.ReceiptType == "RX" && poReceiptLine2.SOShipmentNbr != null)
          valueTupleSet.Add(((poReceiptLine2.SOShipmentType, poReceiptLine2.SOShipmentNbr), (poReceiptLine2.SOOrderType, poReceiptLine2.SOOrderNbr)));
      }
      if (inRegisterEntry.IsValueCreated)
      {
        PX.Objects.IN.INRegister copy = PXCache<PX.Objects.IN.INRegister>.CreateCopy(inRegisterEntry.Value.INRegisterDataMember.Current);
        PXFormulaAttribute.CalcAggregate<INTran.qty>(((PXSelectBase) inRegisterEntry.Value.INTranDataMember).Cache, (object) copy);
        PXFormulaAttribute.CalcAggregate<INTran.tranCost>(((PXSelectBase) inRegisterEntry.Value.INTranDataMember).Cache, (object) copy);
        inRegisterEntry.Value.INRegisterDataMember.Update(copy);
        this.PopulateINReceiptAttributes((PXGraph) inRegisterEntry.Value, aDoc, copy);
        if (str != null)
          ((PXSelectBase) inRegisterEntry.Value.INRegisterDataMember).Cache.SetValue<PX.Objects.IN.INRegister.transferNbr>((object) inRegisterEntry.Value.INRegisterDataMember.Current, (object) str);
      }
      List<PX.Objects.IN.INRegister> forReleaseIN = new List<PX.Objects.IN.INRegister>();
      List<PX.Objects.AP.APRegister> list = new List<PX.Objects.AP.APRegister>();
      bool flag5 = false;
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        if (inRegisterEntry.IsValueCreated)
        {
          try
          {
            ((PXAction) inRegisterEntry.Value.Save).Press();
          }
          catch (PXOuterException ex)
          {
            if (ex.Row is INTran row)
            {
              POReceiptLine entity = new POReceiptLine()
              {
                ReceiptType = row.POReceiptType,
                ReceiptNbr = row.POReceiptNbr,
                LineNbr = row.POReceiptLineNbr
              };
              throw new ErrorProcessingEntityException(((PXGraph) inRegisterEntry.Value).Caches[((object) entity).GetType()], (IBqlTable) entity, ex);
            }
            throw;
          }
        }
        POReceipt poReceipt = this.UpdateReceiptReleased(inRegisterEntry.IsValueCreated ? inRegisterEntry.Value.INRegisterDataMember.Current : (PX.Objects.IN.INRegister) null);
        foreach (PXResult<POReceiptToShipmentLink> pxResult in ((PXSelectBase<POReceiptToShipmentLink>) this.RelatedShipments).Select(Array.Empty<object>()))
        {
          POReceiptToShipmentLink receiptToShipmentLink = PXResult<POReceiptToShipmentLink>.op_Implicit(pxResult);
          if (!valueTupleSet.Contains(((receiptToShipmentLink.SOShipmentType, receiptToShipmentLink.SOShipmentNbr), (receiptToShipmentLink.SOOrderType, receiptToShipmentLink.SOOrderNbr))))
            ((PXSelectBase<POReceiptToShipmentLink>) this.RelatedShipments).Delete(receiptToShipmentLink);
        }
        ((PXAction) this.Save).Press();
        bool? nullable4 = aDoc.AutoCreateInvoice;
        if (nullable4.GetValueOrDefault())
        {
          Decimal? unbilledQty = poReceipt.UnbilledQty;
          Decimal num = 0M;
          if (unbilledQty.GetValueOrDefault() > num & unbilledQty.HasValue)
          {
            nullable4 = ((PXSelectBase<APSetup>) this.apsetup).Current.RetainTaxes;
            bool flag6 = nullable4.GetValueOrDefault() && this.HasOrderWithRetainage(aDoc);
            APInvoiceEntry cleanApInvoiceEntry = this.ReleaseContextExt.GetCleanAPInvoiceEntry();
            cleanApInvoiceEntry.InvoicePOReceipt(aDoc, aAPCreated, keepOrderTaxes: !flag6);
            cleanApInvoiceEntry.AttachPrepayment();
            ((PXAction) cleanApInvoiceEntry.Save).Press();
            ((PXSelectBase<PX.Objects.AP.APInvoice>) cleanApInvoiceEntry.Document).Current.Passed = new bool?(true);
            aAPCreated?.Add(((PXSelectBase<PX.Objects.AP.APInvoice>) cleanApInvoiceEntry.Document).Current);
            nullable4 = ((PXSelectBase<POSetup>) this.posetup).Current.AutoReleaseAP;
            if (nullable4.GetValueOrDefault())
            {
              nullable4 = ((PXSelectBase<PX.Objects.AP.APInvoice>) cleanApInvoiceEntry.Document).Current.Hold;
              bool flag7 = false;
              if (nullable4.GetValueOrDefault() == flag7 & nullable4.HasValue)
                list.Add((PX.Objects.AP.APRegister) ((PXSelectBase<PX.Objects.AP.APInvoice>) cleanApInvoiceEntry.Document).Current);
            }
          }
          else
            flag5 = true;
        }
        this.OnBeforeReleaseReceiptCommit(inRegisterEntry.IsValueCreated ? inRegisterEntry.Value.INRegisterDataMember.Current : (PX.Objects.IN.INRegister) null, aINCreated, forReleaseIN);
        transactionScope.Complete();
      }
      if (inRegisterEntry.IsValueCreated && aINCreated.Find((object) inRegisterEntry.Value.INRegisterDataMember.Current) == null)
        aINCreated.Add(inRegisterEntry.Value.INRegisterDataMember.Current);
      this.ReleaseINDocuments(forReleaseIN, "IN Document has been created but failed to release with the following error: '{0}'. Please, validate a created IN Document");
      if (list.Count > 0)
      {
        try
        {
          APDocumentRelease.ReleaseDoc(list, aIsMassProcess);
        }
        catch (PXException ex)
        {
          throw new PXException("Release of AP document failed with the following error: '{0}'. Please validate the AP document", new object[1]
          {
            (object) ((Exception) ex).Message
          });
        }
      }
      if (flag5)
        throw new PXException("AP document was not created because all lines have already been billed.");
    }
  }

  public virtual void ReleaseINDocuments(List<PX.Objects.IN.INRegister> forReleaseIN, string defaultErrorMessage)
  {
    if (forReleaseIN == null)
      return;
    // ISSUE: explicit non-virtual call
    if (__nonvirtual (forReleaseIN.Count) <= 0)
      return;
    try
    {
      INDocumentRelease.ReleaseDoc(forReleaseIN, false);
    }
    catch (PXException ex)
    {
      this.HandleINReleaseException(forReleaseIN, defaultErrorMessage, ex);
    }
  }

  protected virtual void HandleINReleaseException(
    List<PX.Objects.IN.INRegister> forReleaseIN,
    string defaultErrorMessage,
    PXException ex)
  {
    if (((Exception) ex).InnerException is PXIntercompanyReceivedNotIssuedException)
      throw new PXIntercompanyReceivedNotIssuedException(((Exception) ex).InnerException, "The {0} inventory receipt has been created but it could not be released because at least one item with a serial number has not yet been issued from a warehouse of the selling company. Wait until the selling company issues the item, and then release the inventory receipt on the Receipts (IN301000) form.", forReleaseIN[0].RefNbr);
    throw new PXException(defaultErrorMessage, new object[1]
    {
      (object) ((Exception) ex).Message
    });
  }

  public virtual void PopulateINReceiptAttributes(
    PXGraph docgraph,
    POReceipt aDoc,
    PX.Objects.IN.INRegister copy)
  {
  }

  protected virtual void PreReleaseReceipt(
    POReceipt aDoc,
    Lazy<INRegisterEntryBase> inRegisterEntry)
  {
  }

  protected virtual void OnBeforeReleaseReceiptCommit(
    PX.Objects.IN.INRegister inRegister,
    DocumentList<PX.Objects.IN.INRegister> aINCreated,
    List<PX.Objects.IN.INRegister> forReleaseIN)
  {
    if (inRegister == null)
      return;
    bool? hold = inRegister.Hold;
    bool flag = false;
    if (!(hold.GetValueOrDefault() == flag & hold.HasValue) || !((PXSelectBase<POSetup>) this.posetup).Current.AutoReleaseIN.GetValueOrDefault())
      return;
    forReleaseIN.Add(inRegister);
  }

  public virtual void PrefetchDropShipLinks(POOrder order)
  {
  }

  public virtual void ValidatePOOrder(POOrder order)
  {
  }

  public virtual void ValidateProjectDropShipLineOnRelease(POReceiptLine line)
  {
    if (!(line.DropshipExpenseRecording == "R") || !PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      return;
    INSetup current = ((PXSelectBase<INSetup>) this.insetup).Current;
    if ((current != null ? (!current.UpdateGL.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      throw new PXException("The purchase receipt cannot be released because it contains the lines that require posting of GL transactions while the Update GL check box is cleared on the Inventory Preferences (IN101000) form. To release the purchase receipt, select the Update GL check box.");
  }

  public virtual PXSelectBase<POReceiptLine> GetLinesToReleaseQuery()
  {
    return (PXSelectBase<POReceiptLine>) new PXSelectJoin<POReceiptLine, LeftJoin<POReceiptLineSplit, On<POReceiptLineSplit.FK.ReceiptLine>, LeftJoin<INTran, On<INTran.FK.POReceiptLine>, LeftJoin<INItemPlan, On<INItemPlan.planID, Equal<POReceiptLineSplit.planID>>, LeftJoin<PX.Objects.IN.INSite, On<POReceiptLine.FK.Site>, LeftJoin<POOrder, On<POReceiptLine.FK.Order>, LeftJoin<POAddress, On<POOrder.shipAddressID, Equal<POAddress.addressID>>>>>>>>, Where<POReceiptLine.receiptType, Equal<Required<POReceipt.receiptType>>, And<POReceiptLine.receiptNbr, Equal<Required<POReceipt.receiptNbr>>, And<INTran.refNbr, IsNull>>>, OrderBy<Asc<POReceiptLine.receiptType, Asc<POReceiptLine.receiptNbr, Asc<POReceiptLine.lineNbr>>>>>((PXGraph) this);
  }

  public virtual void ValidatePOLine(POLine poline, POReceipt receipt)
  {
  }

  public virtual POReceiptLine InsertReceiptLine(
    POReceiptLine line,
    POReceipt receipt,
    POLine poline)
  {
    return ((PXSelectBase<POReceiptLine>) this.transactions).Insert(line);
  }

  protected virtual void ValidateLineOnRelease(PXResult<POReceiptLine> row)
  {
    POReceiptLine poReceiptLine = PXResult<POReceiptLine>.op_Implicit(row);
    this.ValidateLineReceiptQtyOnRelease(poReceiptLine);
    if (this.LineSplittingExt.IsLSEntryEnabled(poReceiptLine) && Math.Abs(poReceiptLine.UnassignedQty.GetValueOrDefault()) >= 0.0000005M)
      throw new PXException("One or more lines have unassigned Location and/or Lot/Serial Number");
    this.ValidateProjectDropShipLineOnRelease(PXResult<POReceiptLine>.op_Implicit(row));
  }

  protected virtual void ValidateLineReceiptQtyOnRelease(POReceiptLine receiptLine)
  {
    Decimal? receiptQty;
    if ((!receiptLine.IsIntercompany.GetValueOrDefault() ? 0 : (receiptLine.ReceiptType == "RT" ? 1 : 0)) == 0 && !receiptLine.IsCorrection.GetValueOrDefault())
    {
      receiptQty = receiptLine.ReceiptQty;
      Decimal num = 0M;
      if (receiptQty.GetValueOrDefault() <= num & receiptQty.HasValue)
        goto label_3;
    }
    receiptQty = receiptLine.ReceiptQty;
    Decimal num1 = 0M;
    if (!(receiptQty.GetValueOrDefault() < num1 & receiptQty.HasValue))
      return;
label_3:
    throw new PXException("You cannot release the receipt because the Receipt Qty. in a line must be greater than 0. Specify a nonzero quantity or delete the lines with a quantity of 0.");
  }

  public virtual void ValidateReceiptLineOnRelease(PXResult<POReceiptLine> row)
  {
    this.ValidateLineOnRelease(row);
  }

  public virtual void ValidateReturnLineOnRelease(PXResult<POReceiptLine> row)
  {
    this.ValidateLineOnRelease(row);
    if (PXResult<POReceiptLine>.op_Implicit(row).POAccrualType == "O")
      throw new InvalidOperationException("PO Return cannot be Order-based, it must create its own separate PO Accrual.");
  }

  protected virtual POLine UpdateReceiptLineOnRelease(PXResult<POReceiptLine> row, POLine poLine)
  {
    return poLine;
  }

  protected virtual void UpdateReturnLineOnRelease(PXResult<POReceiptLine> row, POLine poLine)
  {
  }

  protected virtual void ReleaseReceiptLine(POReceiptLine line, POLine poLine, POOrder poOrder)
  {
  }

  protected virtual void ReleaseReturnLine(
    POReceiptLine line,
    POLine poLine,
    POReceiptLine2 origLine)
  {
    this.UpdateReceiptReturn(line, origLine.BaseQty);
  }

  public virtual POReceiptLineReturnUpdate UpdateReceiptReturn(
    POReceiptLine line,
    Decimal? baseOrigQty)
  {
    if (string.IsNullOrEmpty(line.OrigReceiptType) || string.IsNullOrEmpty(line.OrigReceiptNbr) || !line.OrigReceiptLineNbr.HasValue)
      return (POReceiptLineReturnUpdate) null;
    POReceiptLineReturnUpdate lineReturnUpdate1 = ((PXSelectBase<POReceiptLineReturnUpdate>) this.poReceiptReturnUpdate).Insert(new POReceiptLineReturnUpdate()
    {
      ReceiptType = line.OrigReceiptType,
      ReceiptNbr = line.OrigReceiptNbr,
      LineNbr = line.OrigReceiptLineNbr
    });
    lineReturnUpdate1.BaseOrigQty = baseOrigQty;
    POReceiptLineReturnUpdate lineReturnUpdate2 = lineReturnUpdate1;
    Decimal? baseReturnedQty = lineReturnUpdate2.BaseReturnedQty;
    Decimal? baseReceiptQty = line.BaseReceiptQty;
    lineReturnUpdate2.BaseReturnedQty = baseReturnedQty.HasValue & baseReceiptQty.HasValue ? new Decimal?(baseReturnedQty.GetValueOrDefault() + baseReceiptQty.GetValueOrDefault()) : new Decimal?();
    return ((PXSelectBase<POReceiptLineReturnUpdate>) this.poReceiptReturnUpdate).Update(lineReturnUpdate1);
  }

  protected virtual void VerifyOrigINReceipt(
    POReceiptLine line,
    POReceiptLineSplit split,
    POReceiptLine origLine)
  {
    if (string.IsNullOrEmpty(line.OrigReceiptType) || string.IsNullOrEmpty(line.OrigReceiptNbr) || !line.OrigReceiptLineNbr.HasValue || POLineType.IsDropShip(line.OrigReceiptLineType))
      return;
    bool? nullable = origLine.INReleased;
    if (!nullable.GetValueOrDefault() && (!POLineType.IsProjectDropShip(line.LineType) || !(line.DropshipExpenseRecording == "B")))
    {
      INTran originalTran = this.GetOriginalTran(line);
      int num1;
      if (POLineType.IsStockNonDropShip(line.LineType))
      {
        nullable = split.IsStockItem;
        num1 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num1 = 0;
      bool flag1 = POLineType.IsNonStockNonServiceNonDropShip(line.LineType);
      bool flag2 = POLineType.IsProjectDropShip(line.LineType) && line.DropshipExpenseRecording == "R";
      int num2 = flag2 ? 1 : 0;
      if ((num1 | num2) != 0)
      {
        int num3;
        if (originalTran == null)
        {
          num3 = 1;
        }
        else
        {
          nullable = originalTran.Released;
          num3 = !nullable.GetValueOrDefault() ? 1 : 0;
        }
        if (num3 != 0)
          goto label_13;
      }
      if (!(flag1 | flag2) || originalTran == null)
        return;
      nullable = originalTran.Released;
      bool flag3 = false;
      if (!(nullable.GetValueOrDefault() == flag3 & nullable.HasValue))
        return;
label_13:
      throw new PXException("Original inventory receipt {0} must be released prior to return.", new object[1]
      {
        (object) originalTran?.RefNbr
      });
    }
  }

  public virtual void ReleaseReturn(
    POReceipt aDoc,
    DocumentList<PX.Objects.IN.INRegister> aCreated,
    DocumentList<PX.Objects.AP.APInvoice> aInvoiceCreated,
    bool aIsMassProcess)
  {
    Lazy<INRegisterEntryBase> lazy = Lazy.By<INRegisterEntryBase>((Func<INRegisterEntryBase>) (() => this.ReleaseContextExt.GetCleanINRegisterEntryWithInsertedHeader(aDoc)));
    ((PXGraph) this).Clear();
    aDoc = this.ActualizeAndValidatePOReceiptForReleasing(aDoc);
    PX.Objects.PO.GraphExtensions.POReceiptEntryExt.SO2POSync extension = ((PXGraph) this).GetExtension<PX.Objects.PO.GraphExtensions.POReceiptEntryExt.SO2POSync>();
    bool flag1 = EnumerableExtensions.IsIn<string>(aDoc.ReturnInventoryCostMode, "O", "M");
    POReceiptLine poReceiptLine1 = (POReceiptLine) null;
    INTran newline = (INTran) null;
    HashSet<Tuple<string, string>> tupleSet = new HashSet<Tuple<string, string>>();
    List<PXResult<POReceiptLine, POReceiptLineSplit, INTran, INItemPlan, POReceiptLine2>> list1 = ((IEnumerable<PXResult<POReceiptLine>>) PXSelectBase<POReceiptLine, PXSelectJoin<POReceiptLine, LeftJoin<POReceiptLineSplit, On<POReceiptLineSplit.FK.ReceiptLine>, LeftJoin<INTran, On<INTran.FK.POReceiptLine>, LeftJoin<INItemPlan, On<INItemPlan.planID, Equal<POReceiptLineSplit.planID>>, LeftJoin<POReceiptLine2, On<POReceiptLine2.receiptType, Equal<POReceiptLine.origReceiptType>, And<POReceiptLine2.receiptNbr, Equal<POReceiptLine.origReceiptNbr>, And<POReceiptLine2.lineNbr, Equal<POReceiptLine.origReceiptLineNbr>>>>>>>>, Where2<KeysRelation<CompositeKey<Field<POReceiptLine.receiptType>.IsRelatedTo<POReceipt.receiptType>, Field<POReceiptLine.receiptNbr>.IsRelatedTo<POReceipt.receiptNbr>>.WithTablesOf<POReceipt, POReceiptLine>, POReceipt, POReceiptLine>.SameAsCurrent, And<INTran.refNbr, IsNull>>, OrderBy<Asc<POReceiptLine.receiptType, Asc<POReceiptLine.receiptNbr, Asc<POReceiptLine.lineNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).AsEnumerable<PXResult<POReceiptLine>>().Cast<PXResult<POReceiptLine, POReceiptLineSplit, INTran, INItemPlan, POReceiptLine2>>().ToList<PXResult<POReceiptLine, POReceiptLineSplit, INTran, INItemPlan, POReceiptLine2>>();
    for (int index = 0; index < list1.Count; ++index)
    {
      PXResult<POReceiptLine, POReceiptLineSplit, INTran, INItemPlan, POReceiptLine2> row = list1[index];
      POReceiptLine poReceiptLine2 = PXResult<POReceiptLine, POReceiptLineSplit, INTran, INItemPlan, POReceiptLine2>.op_Implicit(row);
      POReceiptLine2 origLine = PXResult<POReceiptLine, POReceiptLineSplit, INTran, INItemPlan, POReceiptLine2>.op_Implicit(row);
      POReceiptLineSplit receiptLineSplit = ((PXResult) row).GetItem<POReceiptLineSplit>();
      INItemPlan copy = PXCache<INItemPlan>.CreateCopy(((PXResult) row).GetItem<INItemPlan>());
      INPlanType inPlanType = INPlanType.PK.Find((PXGraph) this, copy.PlanType);
      if ((object) inPlanType == null)
        inPlanType = new INPlanType();
      INPlanType plantype = inPlanType;
      POLine referencedPoLine = this.GetReferencedPOLine(poReceiptLine2.POType, poReceiptLine2.PONbr, poReceiptLine2.POLineNbr);
      this.ValidateReturnLineOnRelease((PXResult<POReceiptLine>) row);
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, poReceiptLine2.InventoryID);
      bool flag2 = POLineType.IsStockNonDropShip(poReceiptLine2.LineType) && receiptLineSplit.IsStockItem.GetValueOrDefault();
      bool flag3 = POLineType.IsNonStockNonServiceNonDropShip(poReceiptLine2.LineType);
      if (flag2 && (string.IsNullOrEmpty(poReceiptLine2.OrigReceiptType) || string.IsNullOrEmpty(poReceiptLine2.OrigReceiptNbr) || !poReceiptLine2.OrigReceiptLineNbr.HasValue))
      {
        if (aDoc.ReturnInventoryCostMode == "O")
          throw new PXException("Return by original receipt cost cannot be processed because there is at least one line not linked to original receipt (#{0}).", new object[1]
          {
            (object) poReceiptLine2.LineNbr
          });
        if (aDoc.ReturnInventoryCostMode == "M")
          throw new PXException("The return with the manual cost input cannot be processed because there is at least one line that is not linked to the original receipt (#{0}). Select another option or delete the lines that are not linked to the original receipt.", new object[1]
          {
            (object) poReceiptLine2.LineNbr
          });
      }
      bool flag4 = flag2 | flag3 || POLineType.IsProjectDropShip(poReceiptLine2.LineType) && poReceiptLine2.DropshipExpenseRecording == "R" && ((PXSelectBase<INSetup>) this.insetup).Current.UpdateGL.GetValueOrDefault();
      if (flag4)
        this.VerifyOrigINReceipt(poReceiptLine2, receiptLineSplit, (POReceiptLine) origLine);
      bool preserveExistingPlan = flag4 && !string.IsNullOrEmpty(plantype.PlanType) && plantype.DeleteOnEvent.GetValueOrDefault();
      this.ProcessPlanOnRelease(((PXSelectBase) this.splits).Cache, (IItemPlanPOReceiptSource) receiptLineSplit, copy, plantype, preserveExistingPlan);
      int num = !((PXGraph) this).Caches[typeof (POReceiptLine)].ObjectsEqual((object) poReceiptLine1, (object) poReceiptLine2) ? 1 : 0;
      if (num != 0)
      {
        this.ReleaseReturnLine(poReceiptLine2, referencedPoLine, origLine);
        this.UpdateReturnLineOnRelease((PXResult<POReceiptLine>) row, referencedPoLine);
      }
      bool flag5 = !EnumerableExtensions.IsIn<int?>(receiptLineSplit.InventoryID, new int?(), poReceiptLine2.InventoryID);
      if ((num | (flag5 ? 1 : 0)) != 0)
      {
        if (flag4)
        {
          INTran inTran = new INTran()
          {
            BranchID = poReceiptLine2.BranchID,
            TranType = POReceiptType.GetINTranType(aDoc.ReceiptType),
            POReceiptType = poReceiptLine2.ReceiptType,
            POReceiptNbr = poReceiptLine2.ReceiptNbr,
            POReceiptLineNbr = poReceiptLine2.LineNbr,
            POLineType = poReceiptLine2.LineType,
            AcctID = poReceiptLine2.POAccrualAcctID,
            SubID = poReceiptLine2.POAccrualSubID,
            ReclassificationProhibited = new bool?(true),
            InvtAcctID = flag3 ? poReceiptLine2.ExpenseAcctID : new int?(),
            InvtSubID = flag3 ? poReceiptLine2.ExpenseSubID : new int?(),
            ReasonCode = poReceiptLine2.ReasonCode,
            SiteID = poReceiptLine2.SiteID,
            InvtMult = poReceiptLine2.InvtMult,
            ExpireDate = poReceiptLine2.ExpireDate,
            ProjectID = poReceiptLine2.ProjectID,
            TaskID = poReceiptLine2.TaskID,
            CostCodeID = poReceiptLine2.CostCodeID,
            TranDesc = flag5 ? (string) null : poReceiptLine2.TranDesc,
            UOM = flag5 ? receiptLineSplit.UOM : poReceiptLine2.UOM,
            Qty = flag2 ? new Decimal?(0M) : poReceiptLine2.Qty,
            IsStockItem = flag5 ? new bool?() : poReceiptLine2.IsStockItem,
            InventoryID = flag5 ? receiptLineSplit.InventoryID : poReceiptLine2.InventoryID,
            SubItemID = flag5 ? receiptLineSplit.SubItemID : poReceiptLine2.SubItemID,
            LocationID = flag5 ? receiptLineSplit.LocationID : poReceiptLine2.LocationID,
            UnitPrice = flag5 ? new Decimal?(0M) : new Decimal?(),
            UnitCost = new Decimal?(flag5 ? 0M : poReceiptLine2.UnitCost.GetValueOrDefault()),
            IsIntercompany = poReceiptLine2.IsIntercompany,
            ExactCost = new bool?(flag1)
          };
          extension.UpdateSOOrderLink(inTran, referencedPoLine, poReceiptLine2);
          this.UpdateINTranForProjectDropShip(inTran, poReceiptLine2);
          lazy.Value.CostCenterDispatcherExt?.SetInventorySource(inTran);
          try
          {
            newline = lazy.Value.LSSelectDataMember.Insert(inTran);
          }
          catch (PXException ex)
          {
            throw new ErrorProcessingEntityException(((PXGraph) this).Caches[((object) poReceiptLine2).GetType()], (IBqlTable) poReceiptLine2, ex);
          }
        }
        else
          newline = (INTran) null;
      }
      poReceiptLine1 = poReceiptLine2;
      if (newline != null && !string.IsNullOrEmpty(receiptLineSplit.ReceiptNbr) && !flag3)
      {
        INTranSplit inTranSplit1 = INTranSplit.FromINTran(newline);
        inTranSplit1.SplitLineNbr = new int?();
        inTranSplit1.InventoryID = receiptLineSplit.InventoryID;
        inTranSplit1.SubItemID = receiptLineSplit.SubItemID;
        inTranSplit1.LocationID = receiptLineSplit.LocationID;
        inTranSplit1.LotSerialNbr = receiptLineSplit.LotSerialNbr;
        inTranSplit1.UOM = receiptLineSplit.UOM;
        inTranSplit1.Qty = receiptLineSplit.Qty;
        inTranSplit1.ExpireDate = receiptLineSplit.ExpireDate;
        inTranSplit1.POLineType = referencedPoLine != null ? referencedPoLine.LineType : poReceiptLine2.LineType;
        if (preserveExistingPlan)
        {
          inTranSplit1.PlanID = copy.PlanID;
          copy.OrigPlanID = new long?();
          copy.OrigPlanType = (string) null;
          copy.OrigNoteID = new Guid?();
          copy.OrigPlanLevel = new int?();
          copy.IgnoreOrigPlan = new bool?(false);
          copy.Reverse = new bool?(false);
          copy.RefNoteID = lazy.Value.INRegisterDataMember.Current.NoteID;
          copy.RefEntityType = typeof (PX.Objects.IN.INRegister).FullName;
          ((PXGraph) lazy.Value).Caches[typeof (INItemPlan)].Update((object) copy);
        }
        try
        {
          INTranSplit inTranSplit2 = lazy.Value.INTranSplitDataMember.Insert(inTranSplit1);
          ((PXSelectBase) lazy.Value.INTranSplitDataMember).Cache.RaiseRowUpdated((object) inTranSplit2, (object) inTranSplit2);
        }
        catch (PXException ex)
        {
          throw new ErrorProcessingEntityException(((PXGraph) this).Caches[((object) poReceiptLine2).GetType()], (IBqlTable) poReceiptLine2, ex);
        }
      }
      else if (preserveExistingPlan)
        this.ThrowPlanNotReattachedError(poReceiptLine2, receiptLineSplit);
      if (((index + 1 >= list1.Count ? 1 : (!((PXGraph) this).Caches[typeof (POReceiptLine)].ObjectsEqual((object) poReceiptLine2, (object) PXResult<POReceiptLine, POReceiptLineSplit, INTran, INItemPlan, POReceiptLine2>.op_Implicit(list1[index + 1])) ? 1 : 0)) | (flag5 ? 1 : 0)) != 0)
        this.SetReturnCostFinal(newline, poReceiptLine2, inventoryItem, aDoc.ReturnInventoryCostMode);
      extension.ReduceSOAllocationOnReleaseReturn((PXResult<POReceiptLine>) row, receiptLineSplit, referencedPoLine);
    }
    if (lazy.IsValueCreated)
    {
      PX.Objects.IN.INRegister copy = PXCache<PX.Objects.IN.INRegister>.CreateCopy(lazy.Value.INRegisterDataMember.Current);
      PXFormulaAttribute.CalcAggregate<INTran.qty>(((PXSelectBase) lazy.Value.INTranDataMember).Cache, (object) copy);
      PXFormulaAttribute.CalcAggregate<INTran.tranAmt>(((PXSelectBase) lazy.Value.INTranDataMember).Cache, (object) copy);
      PXFormulaAttribute.CalcAggregate<INTran.tranCost>(((PXSelectBase) lazy.Value.INTranDataMember).Cache, (object) copy);
      lazy.Value.INRegisterDataMember.Update(copy);
    }
    APInvoiceEntry apInvoiceEntry = (APInvoiceEntry) null;
    if (aDoc.AutoCreateInvoice.Value)
    {
      bool flag6 = ((PXSelectBase<APSetup>) this.apsetup).Current.RetainTaxes.GetValueOrDefault() && this.HasOrderWithRetainage(aDoc);
      apInvoiceEntry = this.ReleaseContextExt.GetCleanAPInvoiceEntry();
      apInvoiceEntry.InvoicePOReceipt(aDoc, aInvoiceCreated, keepOrderTaxes: !flag6);
    }
    List<PX.Objects.AP.APRegister> list2 = new List<PX.Objects.AP.APRegister>();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (lazy.IsValueCreated)
        ((PXAction) lazy.Value.Save).Press();
      this.UpdateReturnReleased(lazy.IsValueCreated ? lazy.Value.INRegisterDataMember.Current : (PX.Objects.IN.INRegister) null);
      ((PXAction) this.Save).Press();
      if (lazy.IsValueCreated)
        this.ReleaseINDocuments(new List<PX.Objects.IN.INRegister>()
        {
          lazy.Value.INRegisterDataMember.Current
        }, "IN Document failed to release with the following error: '{0}'.");
      bool? nullable = aDoc.AutoCreateInvoice;
      if (nullable.Value)
      {
        ((PXAction) apInvoiceEntry.Save).Press();
        nullable = ((PXSelectBase<POSetup>) this.posetup).Current.AutoReleaseAP;
        if (nullable.GetValueOrDefault())
        {
          nullable = ((PXSelectBase<PX.Objects.AP.APInvoice>) apInvoiceEntry.Document).Current.Hold;
          bool flag7 = false;
          if (nullable.GetValueOrDefault() == flag7 & nullable.HasValue)
            list2.Add((PX.Objects.AP.APRegister) ((PXSelectBase<PX.Objects.AP.APInvoice>) apInvoiceEntry.Document).Current);
        }
      }
      transactionScope.Complete();
    }
    if (lazy.IsValueCreated && aCreated.Find((object) lazy.Value.INRegisterDataMember.Current) == null)
      aCreated.Add(lazy.Value.INRegisterDataMember.Current);
    if (list2.Count <= 0)
      return;
    try
    {
      APDocumentRelease.ReleaseDoc(list2, aIsMassProcess);
    }
    catch (PXException ex)
    {
      throw new PXException("Release of AP document failed with the following error: '{0}'. Please validate the AP document", new object[1]
      {
        (object) ((Exception) ex).Message
      });
    }
  }

  public virtual void ProcessPlanOnRelease(
    PXCache splitCache,
    IItemPlanPOReceiptSource split,
    INItemPlan plan,
    INPlanType plantype,
    bool preserveExistingPlan)
  {
    if (string.IsNullOrEmpty(split.ReceiptType))
      return;
    if (!string.IsNullOrEmpty(plantype.PlanType) && plantype.DeleteOnEvent.Value)
    {
      if (!preserveExistingPlan)
        ((PXGraph) this).Caches[typeof (INItemPlan)].Delete((object) plan);
      GraphHelper.MarkUpdated(splitCache, (object) split, true);
      split = (IItemPlanPOReceiptSource) splitCache.Locate((object) split);
      if (split != null)
        split.PlanID = new long?();
      splitCache.IsDirty = true;
    }
    else
    {
      if (string.IsNullOrEmpty(plantype.PlanType) || string.IsNullOrEmpty(plantype.ReplanOnEvent))
        return;
      plan.PlanType = plantype.ReplanOnEvent;
      ((PXGraph) this).Caches[typeof (INItemPlan)].Update((object) plan);
      GraphHelper.MarkUpdated(splitCache, (object) split, true);
      splitCache.IsDirty = true;
    }
  }

  protected virtual POReceipt ActualizeAndValidatePOReceiptForReleasing(POReceipt aDoc)
  {
    ((PXSelectBase<POReceipt>) this.Document).Current = PXResultset<POReceipt>.op_Implicit(((PXSelectBase<POReceipt>) this.Document).Search<POReceipt.receiptNbr>((object) aDoc.ReceiptNbr, new object[1]
    {
      (object) aDoc.ReceiptType
    }));
    bool? nullable = WorkflowAction.HasWorkflowActionEnabled<POReceiptEntry, POReceipt>(this, (Expression<Func<POReceiptEntry, PXAction<POReceipt>>>) (g => g.release), ((PXSelectBase<POReceipt>) this.Document).Current);
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      throw new PXInvalidOperationException("The {0} action is not available in the {1} document at the moment. The document is being used by another process.", new object[2]
      {
        (object) ((PXAction) this.release).GetCaption(),
        (object) ((PXSelectBase) this.Document).Cache.GetRowDescription((object) ((PXSelectBase<POReceipt>) this.Document).Current)
      });
    return ((PXSelectBase<POReceipt>) this.Document).Current;
  }

  protected virtual POReceipt UpdateReceiptReleased(PX.Objects.IN.INRegister inRegister)
  {
    POReceipt current = ((PXSelectBase<POReceipt>) this.Document).Current;
    ((SelectedEntityEvent<POReceipt>) PXEntityEventBase<POReceipt>.Container<POReceipt.Events>.Select((Expression<Func<POReceipt.Events, PXEntityEvent<POReceipt.Events>>>) (ev => ev.InventoryReceiptCreated))).FireOn((PXGraph) this, current);
    current.InvtDocType = inRegister?.DocType;
    current.InvtRefNbr = inRegister?.RefNbr;
    Decimal? nullable1 = new Decimal?(0M);
    foreach (PXResult<POReceiptLine> pxResult in ((PXSelectBase<POReceiptLine>) this.transactions).Select(Array.Empty<object>()))
    {
      POReceiptLine poReceiptLine = PXResult<POReceiptLine>.op_Implicit(pxResult);
      Decimal? nullable2 = nullable1;
      Decimal? unbilledQty = poReceiptLine.UnbilledQty;
      nullable1 = nullable2.HasValue & unbilledQty.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + unbilledQty.GetValueOrDefault()) : new Decimal?();
      poReceiptLine.Released = new bool?(true);
      GraphHelper.MarkUpdated(((PXGraph) this).Caches[typeof (POReceiptLine)], (object) poReceiptLine, true);
    }
    current.UnbilledQty = nullable1;
    return ((PXSelectBase<POReceipt>) this.Document).Update(current);
  }

  protected virtual POReceipt UpdateReturnReleased(PX.Objects.IN.INRegister inRegister)
  {
    POReceipt current = ((PXSelectBase<POReceipt>) this.Document).Current;
    ((SelectedEntityEvent<POReceipt>) PXEntityEventBase<POReceipt>.Container<POReceipt.Events>.Select((Expression<Func<POReceipt.Events, PXEntityEvent<POReceipt.Events>>>) (ev => ev.InventoryIssueCreated))).FireOn((PXGraph) this, current);
    current.InvtDocType = inRegister?.DocType;
    current.InvtRefNbr = inRegister?.RefNbr;
    POReceipt poReceipt = ((PXSelectBase<POReceipt>) this.Document).Update(current);
    foreach (PXResult<POReceiptLine> pxResult in ((PXSelectBase<POReceiptLine>) this.transactions).Select(Array.Empty<object>()))
    {
      POReceiptLine poReceiptLine = PXResult<POReceiptLine>.op_Implicit(pxResult);
      poReceiptLine.Released = new bool?(true);
      GraphHelper.MarkUpdated(((PXGraph) this).Caches[typeof (POReceiptLine)], (object) poReceiptLine, true);
    }
    return poReceipt;
  }

  public virtual void SetReturnCostFinal(
    INTran newline,
    POReceiptLine rctLine,
    PX.Objects.IN.InventoryItem item,
    string returnCostMode)
  {
    if (newline == null || EnumerableExtensions.IsNotIn<string>(returnCostMode, "O", "M"))
    {
      if (!POLineType.IsNonStock(rctLine.LineType) && !POLineType.IsDropShip(rctLine.LineType) && !POLineType.IsProjectDropShip(rctLine.LineType))
        return;
      if (newline != null)
        newline.TranCost = rctLine.TranCost;
      rctLine.TranCostFinal = rctLine.TranCost;
      GraphHelper.MarkUpdated(((PXGraph) this).Caches[typeof (POReceiptLine)], (object) rctLine, true);
    }
    else
    {
      PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = this.MultiCurrencyExt.GetDefaultCurrencyInfo();
      Decimal valueOrDefault1 = rctLine.UnitCost.GetValueOrDefault();
      Decimal? nullable1 = rctLine.TranCost;
      Decimal? nullable2;
      Decimal? nullable3;
      if (item.ValMethod == "F" && !POLineType.IsProjectDropShip(rctLine.LineType))
      {
        if (rctLine.POType == "DP")
        {
          if (returnCostMode == "M")
            throw new PXException("A return by manual cost input cannot be processed for a purchase order with the Drop-Ship type if the return order includes lines with FIFO-valuated items.");
          throw new PXException("A return by original cost cannot be processed for a purchase order with the Drop-Ship type if the return order includes lines with FIFO-valuated items.");
        }
        INTran originalTran = this.GetOriginalTran(rctLine);
        PXResultset<INCostStatus> pxResultset = PXSelectBase<INCostStatus, PXViewOf<INCostStatus>.BasedOn<SelectFromBase<INCostStatus, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INTranCost>.On<BqlOperand<INTranCost.costID, IBqlLong>.IsEqual<INCostStatus.costID>>>>.Where<KeysRelation<CompositeKey<Field<INTranCost.docType>.IsRelatedTo<INTran.docType>, Field<INTranCost.refNbr>.IsRelatedTo<INTran.refNbr>, Field<INTranCost.lineNbr>.IsRelatedTo<INTran.lineNbr>>.WithTablesOf<INTran, INTranCost>, INTran, INTranCost>.SameAsCurrent>>.ReadOnly.Config>.SelectMultiBound((PXGraph) this, (object[]) new INTran[1]
        {
          originalTran
        }, Array.Empty<object>());
        if (originalTran == null || pxResultset == null || pxResultset.Count != 1)
          throw new PXException("Inventory Receipt for Purchase Receipt# '{0}' was not found.", new object[1]
          {
            (object) rctLine.OrigReceiptNbr
          });
        newline.OrigRefNbr = originalTran.OrigRefNbr ?? originalTran.RefNbr;
        if (returnCostMode != "M")
        {
          INCostStatus inCostStatus = PXResultset<INCostStatus>.op_Implicit(pxResultset);
          Decimal? qtyOnHand1 = inCostStatus.QtyOnHand;
          Decimal num = 0M;
          Decimal? nullable4;
          if (!(qtyOnHand1.GetValueOrDefault() <= num & qtyOnHand1.HasValue))
          {
            nullable2 = inCostStatus.TotalCost;
            Decimal? qtyOnHand2 = inCostStatus.QtyOnHand;
            nullable4 = nullable2.HasValue & qtyOnHand2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() / qtyOnHand2.GetValueOrDefault()) : new Decimal?();
          }
          else
            nullable4 = inCostStatus.TotalCost;
          Decimal? nullable5 = nullable4;
          valueOrDefault1 = INUnitAttribute.ConvertToBase<POReceiptLine.inventoryID>(((PXSelectBase) this.transactions).Cache, (object) rctLine, rctLine.UOM, nullable5.GetValueOrDefault(), INPrecision.UNITCOST);
          nullable3 = inCostStatus.QtyOnHand;
          nullable2 = rctLine.BaseReceiptQty;
          Decimal? nullable6;
          if (!(nullable3.GetValueOrDefault() <= nullable2.GetValueOrDefault() & nullable3.HasValue & nullable2.HasValue))
          {
            PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = defaultCurrencyInfo;
            Decimal? totalCost = inCostStatus.TotalCost;
            Decimal? nullable7 = rctLine.BaseReceiptQty;
            nullable2 = totalCost.HasValue & nullable7.HasValue ? new Decimal?(totalCost.GetValueOrDefault() * nullable7.GetValueOrDefault()) : new Decimal?();
            nullable3 = inCostStatus.QtyOnHand;
            Decimal? nullable8;
            if (!(nullable2.HasValue & nullable3.HasValue))
            {
              nullable7 = new Decimal?();
              nullable8 = nullable7;
            }
            else
              nullable8 = new Decimal?(nullable2.GetValueOrDefault() / nullable3.GetValueOrDefault());
            nullable7 = nullable8;
            Decimal valueOrDefault2 = nullable7.GetValueOrDefault();
            nullable6 = new Decimal?(currencyInfo.RoundBase(valueOrDefault2));
          }
          else
            nullable6 = inCostStatus.TotalCost;
          nullable1 = nullable6;
        }
      }
      newline.UnitCost = new Decimal?(valueOrDefault1);
      newline.TranCost = nullable1;
      POReceiptLine poReceiptLine = rctLine;
      nullable3 = rctLine.TranCostFinal;
      Decimal valueOrDefault3 = nullable3.GetValueOrDefault();
      nullable2 = nullable1;
      Decimal? nullable9;
      if (!nullable2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable9 = nullable3;
      }
      else
        nullable9 = new Decimal?(valueOrDefault3 + nullable2.GetValueOrDefault());
      poReceiptLine.TranCostFinal = nullable9;
      GraphHelper.MarkUpdated(((PXGraph) this).Caches[typeof (POReceiptLine)], (object) rctLine, true);
    }
  }

  public virtual INTran GetOriginalTran(POReceiptLine line)
  {
    return PXResultset<INTran>.op_Implicit(PXSelectBase<INTran, PXViewOf<INTran>.BasedOn<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.docType, In3<INDocType.receipt, INDocType.issue>>>>, And<BqlOperand<INTran.tranType, IBqlString>.IsEqual<INTranType.receipt>>>, And<BqlOperand<INTran.pOReceiptType, IBqlString>.IsEqual<BqlField<POReceiptLine.origReceiptType, IBqlString>.FromCurrent>>>, And<BqlOperand<INTran.pOReceiptNbr, IBqlString>.IsEqual<BqlField<POReceiptLine.origReceiptNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<INTran.pOReceiptLineNbr, IBqlInt>.IsEqual<BqlField<POReceiptLine.origReceiptLineNbr, IBqlInt>.FromCurrent>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) new POReceiptLine[1]
    {
      line
    }, Array.Empty<object>()));
  }

  protected virtual void ThrowPlanNotReattachedError(POReceiptLine line, POReceiptLineSplit split)
  {
    throw new PXInvalidOperationException();
  }

  public virtual POLine ReopenPOLineIfNeeded(POLine poLine)
  {
    if (poLine != null && poLine.Completed.GetValueOrDefault())
    {
      bool? allowComplete = poLine.AllowComplete;
      bool flag = false;
      if (allowComplete.GetValueOrDefault() == flag & allowComplete.HasValue)
      {
        POLine copy = PXCache<POLine>.CreateCopy(poLine);
        copy.Completed = new bool?(false);
        copy.Closed = new bool?(false);
        poLine = ((PXSelectBase<POLine>) this.poline).Update(copy);
      }
    }
    return poLine;
  }

  protected virtual void UpdateINTranForProjectDropShip(INTran newline, POReceiptLine line)
  {
    if (!POLineType.IsProjectDropShip(line.LineType))
      return;
    newline.InvtMult = new short?((short) 0);
    newline.AcctID = line.POAccrualAcctID;
    newline.SubID = line.POAccrualSubID;
    newline.COGSAcctID = line.ExpenseAcctID;
    newline.COGSSubID = line.ExpenseSubID;
    newline.InvtAcctID = new int?();
    newline.InvtSubID = new int?();
  }

  internal IDisposable GetSkipUIUpdateScope()
  {
    return (IDisposable) new SimpleScope((System.Action) (() => this._skipUIUpdate = true), (System.Action) (() => this._skipUIUpdate = false));
  }

  internal bool SkipUIUpdate => this._skipUIUpdate;

  protected virtual void SyncUnassigned()
  {
  }

  public virtual void PostReceipt(PostReceiptArgs args)
  {
    PX.Objects.SO.SOOrderShipment soOrderShipment = args.SOOrderShipment;
    PX.Objects.SO.SOOrder soOrder = args.SOOrder;
    POReceipt receipt = args.Receipt;
    INIssueEntry inIssueGraph = args.INIssueGraph;
    PX.Objects.AR.ARInvoice invoice = args.Invoice;
    ((PXGraph) this).Clear();
    ((PXGraph) inIssueGraph).Clear();
    ((PXSelectBase<INSetup>) inIssueGraph.insetup).Current.HoldEntry = new bool?(false);
    ((PXSelectBase<INSetup>) inIssueGraph.insetup).Current.RequireControlTotal = new bool?(false);
    PX.Objects.IN.INRegister inRegister1 = args.CreatedDocuments.Find<PX.Objects.IN.INRegister.srcDocType, PX.Objects.IN.INRegister.srcRefNbr>((object) soOrderShipment.ShipmentType, (object) soOrderShipment.ShipmentNbr) ?? new PX.Objects.IN.INRegister();
    if (inRegister1.RefNbr != null)
    {
      ((PXSelectBase<PX.Objects.IN.INRegister>) inIssueGraph.issue).Current = PXResultset<PX.Objects.IN.INRegister>.op_Implicit(((PXSelectBase<PX.Objects.IN.INRegister>) inIssueGraph.issue).Search<PX.Objects.IN.INRegister.docType, PX.Objects.IN.INRegister.refNbr>((object) inRegister1.DocType, (object) inRegister1.RefNbr, Array.Empty<object>()));
      if (((PXSelectBase<PX.Objects.IN.INRegister>) inIssueGraph.issue).Current != null && ((PXSelectBase<PX.Objects.IN.INRegister>) inIssueGraph.issue).Current.SrcRefNbr == null)
      {
        ((PXSelectBase<PX.Objects.IN.INRegister>) inIssueGraph.issue).Current.SrcDocType = soOrderShipment.ShipmentType;
        ((PXSelectBase<PX.Objects.IN.INRegister>) inIssueGraph.issue).Current.SrcRefNbr = soOrderShipment.ShipmentNbr;
      }
    }
    else
    {
      inRegister1.BranchID = soOrder.BranchID;
      inRegister1.DocType = "I";
      inRegister1.SiteID = soOrderShipment.SiteID;
      PX.Objects.IN.INRegister inRegister2 = inRegister1;
      DateTime? nullable;
      if (args.IsReversal)
      {
        DateTime? docDate = invoice.DocDate;
        DateTime? receiptDate = receipt.ReceiptDate;
        if ((docDate.HasValue & receiptDate.HasValue ? (docDate.GetValueOrDefault() < receiptDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          nullable = receipt.ReceiptDate;
          goto label_7;
        }
      }
      nullable = invoice.DocDate;
label_7:
      inRegister2.TranDate = nullable;
      inRegister1.OrigModule = "SO";
      inRegister1.SrcDocType = soOrderShipment.ShipmentType;
      inRegister1.SrcRefNbr = soOrderShipment.ShipmentNbr;
      inRegister1.FinPeriodID = !args.IsReversal || string.CompareOrdinal(invoice.FinPeriodID, receipt.FinPeriodID) >= 0 ? invoice.FinPeriodID : receipt.FinPeriodID;
      ((PXSelectBase<PX.Objects.IN.INRegister>) inIssueGraph.issue).Insert(inRegister1);
    }
    POReceiptLine poReceiptLine = (POReceiptLine) null;
    PXView pxView = new PXView((PXGraph) this, false, this.GetDropshipReceiptsSelectCommand(args));
    object[] objArray1 = new object[1]
    {
      (object) soOrderShipment
    };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<POReceiptLine> res in pxView.SelectMultiBound(objArray1, objArray2))
    {
      POReceiptLine line = PXResult<POReceiptLine>.op_Implicit(res);
      PX.Objects.SO.SOLine soLine = PXResult.Unwrap<PX.Objects.SO.SOLine>((object) res);
      PX.Objects.AR.ARTran arTran = PXResult.Unwrap<PX.Objects.AR.ARTran>((object) res);
      SOOrderTypeOperation orderTypeOperation = SOOrderTypeOperation.PK.Find((PXGraph) this, soLine.OrderType, soLine.Operation);
      INLocation inLocation = PXResult.Unwrap<INLocation>((object) res);
      PXResult.Unwrap<INLotSerClass>((object) res);
      INPostClass postclass = PXResult.Unwrap<INPostClass>((object) res);
      PX.Objects.IN.InventoryItem inventoryItem = PXResult.Unwrap<PX.Objects.IN.InventoryItem>((object) res);
      PX.Objects.IN.INSite site = PXResult.Unwrap<PX.Objects.IN.INSite>((object) res);
      if (!((PXGraph) this).Caches[typeof (POReceiptLine)].ObjectsEqual((object) poReceiptLine, (object) line))
      {
        if (line.LineType == "GP" && !inLocation.LocationID.HasValue)
          throw new PXException("Drop-Ship Location is not configured for warehouse {0}", new object[1]
          {
            ((PXGraph) this).Caches[typeof (POReceiptLine)].GetValueExt<POReceiptLine.siteID>((object) line)
          });
        PX.Objects.PM.Lite.PMProject project;
        PX.Objects.PM.Lite.PMTask task;
        this.TryToGetProjectAndTask(res, line, out project, out task);
        INTran tran = new INTran();
        tran.BranchID = soLine.BranchID;
        tran.TranType = args.IsReversal ? INTranType.GetOppositeTranType(orderTypeOperation.INDocType) : orderTypeOperation.INDocType;
        tran.POReceiptType = line.ReceiptType;
        tran.POReceiptNbr = line.ReceiptNbr;
        tran.POReceiptLineNbr = line.LineNbr;
        tran.POLineType = line.LineType;
        tran.SOShipmentNbr = line.ReceiptNbr;
        tran.SOShipmentType = "H";
        tran.SOShipmentLineNbr = line.LineNbr;
        tran.SOOrderType = soLine.OrderType;
        tran.SOOrderNbr = soLine.OrderNbr;
        tran.SOOrderLineNbr = soLine.LineNbr;
        if (!args.IsReversal)
        {
          tran.ARDocType = arTran.TranType;
          tran.ARRefNbr = arTran.RefNbr;
          tran.ARLineNbr = arTran.LineNbr;
        }
        tran.InventoryID = line.InventoryID;
        tran.SubItemID = line.SubItemID;
        tran.SiteID = line.SiteID;
        tran.LocationID = inLocation.LocationID;
        tran.BAccountID = soLine.CustomerID;
        tran.InvtMult = new short?((short) 0);
        tran.IsCostUnmanaged = new bool?(true);
        tran.UOM = line.UOM;
        tran.Qty = line.ReceiptQty;
        tran.UnitPrice = new Decimal?(arTran.UnitPrice.GetValueOrDefault());
        bool flag = !args.IsReversal && (arTran.DrCr == "C" && arTran.SOOrderLineOperation == "R" || arTran.DrCr == "D" && arTran.SOOrderLineOperation == "I");
        INTran inTran1 = tran;
        Decimal? nullable1;
        if (!flag)
        {
          nullable1 = arTran.TranAmt;
        }
        else
        {
          Decimal? tranAmt = arTran.TranAmt;
          nullable1 = tranAmt.HasValue ? new Decimal?(-tranAmt.GetValueOrDefault()) : new Decimal?();
        }
        Decimal? nullable2 = new Decimal?(nullable1.GetValueOrDefault());
        inTran1.TranAmt = nullable2;
        tran.UnitCost = line.UnitCost;
        tran.TranCost = line.TranCostFinal;
        tran.TranDesc = soLine.TranDesc;
        tran.ReasonCode = soLine.ReasonCode;
        tran.AcctID = line.POAccrualAcctID;
        tran.SubID = line.POAccrualSubID;
        tran.ReclassificationProhibited = new bool?(true);
        int? nullable3 = line.ExpenseAcctID;
        if (!nullable3.HasValue && postclass != null && postclass.COGSSubFromSales.GetValueOrDefault())
        {
          tran.COGSAcctID = INReleaseProcess.GetAccountDefaults<INPostClass.cOGSAcctID>((PXGraph) this, InventoryAccountServiceHelper.Params(inventoryItem, site, postclass, (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task));
          tran.COGSSubID = arTran.SubID;
        }
        else
        {
          tran.COGSAcctID = line.ExpenseAcctID;
          INTran inTran2 = tran;
          nullable3 = postclass == null || !postclass.COGSSubFromSales.GetValueOrDefault() ? new int?() : arTran.SubID;
          int? nullable4 = nullable3 ?? line.ExpenseSubID;
          inTran2.COGSSubID = nullable4;
        }
        tran.ProjectID = line.ProjectID;
        tran.TaskID = line.TaskID;
        tran.CostCodeID = line.CostCodeID;
        inIssueGraph.CostCenterDispatcherExt?.SetInventorySource(tran);
        INTran inTran3 = ((PXSelectBase<INTran>) inIssueGraph.transactions).Insert(tran);
        foreach (PXResult<POReceiptLineSplit> pxResult in ((PXSelectBase<POReceiptLineSplit>) new PXSelect<POReceiptLineSplit, Where<POReceiptLineSplit.receiptType, Equal<Required<POReceiptLineSplit.receiptType>>, And<POReceiptLineSplit.receiptNbr, Equal<Required<POReceiptLineSplit.receiptNbr>>, And<POReceiptLineSplit.lineNbr, Equal<Required<POReceiptLineSplit.lineNbr>>, And<POReceiptLineSplit.qty, NotEqual<decimal0>>>>>>((PXGraph) this)).Select(new object[3]
        {
          (object) line.ReceiptType,
          (object) line.ReceiptNbr,
          (object) line.LineNbr
        }))
        {
          POReceiptLineSplit receiptLineSplit = PXResult<POReceiptLineSplit>.op_Implicit(pxResult);
          INTranSplit inTranSplit1 = INTranSplit.FromINTran(inTran3);
          INTranSplit inTranSplit2 = inTranSplit1;
          nullable3 = new int?();
          int? nullable5 = nullable3;
          inTranSplit2.SplitLineNbr = nullable5;
          inTranSplit1.LotSerialNbr = receiptLineSplit.LotSerialNbr;
          inTranSplit1.ExpireDate = receiptLineSplit.ExpireDate;
          inTranSplit1.BaseQty = receiptLineSplit.BaseQty;
          inTranSplit1.Qty = receiptLineSplit.Qty;
          inTranSplit1.UOM = receiptLineSplit.UOM;
          inTranSplit1.InvtMult = new short?((short) 0);
          ((PXSelectBase<INTranSplit>) inIssueGraph.splits).Insert(inTranSplit1);
        }
        poReceiptLine = line;
      }
    }
    PX.Objects.IN.INRegister copy = PXCache<PX.Objects.IN.INRegister>.CreateCopy(((PXSelectBase<PX.Objects.IN.INRegister>) inIssueGraph.issue).Current);
    PXFormulaAttribute.CalcAggregate<INTran.qty>(((PXSelectBase) inIssueGraph.transactions).Cache, (object) copy);
    PXFormulaAttribute.CalcAggregate<INTran.tranAmt>(((PXSelectBase) inIssueGraph.transactions).Cache, (object) copy);
    PXFormulaAttribute.CalcAggregate<INTran.tranCost>(((PXSelectBase) inIssueGraph.transactions).Cache, (object) copy);
    ((PXSelectBase<PX.Objects.IN.INRegister>) inIssueGraph.issue).Update(copy);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (((PXSelectBase) inIssueGraph.transactions).Cache.IsDirty)
      {
        ((PXAction) inIssueGraph.Save).Press();
        if (!args.IsReversal)
        {
          soOrderShipment.InvtDocType = ((PXSelectBase<PX.Objects.IN.INRegister>) inIssueGraph.issue).Current.DocType;
          soOrderShipment.InvtRefNbr = ((PXSelectBase<PX.Objects.IN.INRegister>) inIssueGraph.issue).Current.RefNbr;
          soOrderShipment.InvtNoteID = ((PXSelectBase<PX.Objects.IN.INRegister>) inIssueGraph.issue).Current.NoteID;
          PXCache<PX.Objects.SO.SOOrderShipment> pxCache = GraphHelper.Caches<PX.Objects.SO.SOOrderShipment>((PXGraph) this, true);
          pxCache.Update(soOrderShipment);
          PXDBDefaultAttribute.SetDefaultForUpdate<PX.Objects.SO.SOOrderShipment.shipAddressID>((PXCache) pxCache, (object) null, false);
          PXDBDefaultAttribute.SetDefaultForUpdate<PX.Objects.SO.SOOrderShipment.shipContactID>((PXCache) pxCache, (object) null, false);
          PXDBDefaultAttribute.SetDefaultForUpdate<PX.Objects.SO.SOOrderShipment.shipmentNbr>((PXCache) pxCache, (object) null, false);
        }
        ((PXAction) this.Save).Press();
        args.CreatedDocuments.AddOrReplace(((PXSelectBase<PX.Objects.IN.INRegister>) inIssueGraph.issue).Current);
      }
      transactionScope.Complete();
    }
  }

  protected virtual BqlCommand GetDropshipReceiptsSelectCommand(PostReceiptArgs args)
  {
    BqlCommand bqlCommand = BqlCommand.CreateInstance(new System.Type[1]
    {
      typeof (Select2<POReceiptLine, InnerJoin<PX.Objects.IN.InventoryItem, On<POReceiptLine.FK.InventoryItem>, LeftJoin<INLotSerClass, On<PX.Objects.IN.InventoryItem.FK.LotSerialClass>, LeftJoin<INPostClass, On<PX.Objects.IN.InventoryItem.FK.PostClass>, InnerJoin<PX.Objects.IN.INSite, On<POReceiptLine.FK.Site>, LeftJoin<INLocation, On<INLocation.locationID, Equal<PX.Objects.IN.INSite.dropShipLocationID>>>>>>>, Where<POReceiptLine.receiptNbr, Equal<Current<PX.Objects.SO.SOOrderShipment.shipmentNbr>>, And<PX.Objects.SO.SOLine.orderType, Equal<Current<PX.Objects.SO.SOOrderShipment.orderType>>, And<PX.Objects.SO.SOLine.orderNbr, Equal<Current<PX.Objects.SO.SOOrderShipment.orderNbr>>, And<POReceiptLine.receiptQty, NotEqual<decimal0>, And<POReceiptLine.lineType, In3<POLineType.goodsForDropShip, POLineType.nonStockForDropShip>>>>>>>)
    });
    if (!args.IsReversal)
      bqlCommand = bqlCommand.WhereAnd<Where<INTran.refNbr, IsNull>>();
    return BqlCommand.AppendJoin<InnerJoin<PX.Objects.AR.ARTran, On<PX.Objects.AR.ARTran.sOShipmentNbr, Equal<POReceiptLine.receiptNbr>, And<PX.Objects.AR.ARTran.sOShipmentType, Equal<INDocType.dropShip>, And<PX.Objects.AR.ARTran.sOOrderType, Equal<PX.Objects.SO.SOLine.orderType>, And<PX.Objects.AR.ARTran.sOOrderNbr, Equal<PX.Objects.SO.SOLine.orderNbr>, And<PX.Objects.AR.ARTran.sOOrderLineNbr, Equal<PX.Objects.SO.SOLine.lineNbr>, And<PX.Objects.AR.ARTran.lineType, Equal<PX.Objects.SO.SOLine.lineType>>>>>>>, LeftJoin<INTran, On<INTran.sOShipmentNbr, Equal<POReceiptLine.receiptNbr>, And<INTran.sOShipmentType, Equal<INDocType.dropShip>, And<INTran.sOShipmentLineNbr, Equal<POReceiptLine.lineNbr>, And<INTran.sOOrderType, Equal<PX.Objects.SO.SOLine.orderType>, And<INTran.sOOrderNbr, Equal<PX.Objects.SO.SOLine.orderNbr>, And<INTran.sOOrderLineNbr, Equal<PX.Objects.SO.SOLine.lineNbr>>>>>>>>>>(!(args.SOOrderShipment.Operation == "R") ? BqlCommand.AppendJoin<InnerJoin<PX.Objects.SO.SOLineSplit, On<PX.Objects.SO.SOLineSplit.pOType, Equal<POReceiptLine.pOType>, And<PX.Objects.SO.SOLineSplit.pONbr, Equal<POReceiptLine.pONbr>, And<PX.Objects.SO.SOLineSplit.pOLineNbr, Equal<POReceiptLine.pOLineNbr>>>>, InnerJoin<PX.Objects.SO.SOLine, On<PX.Objects.SO.SOLineSplit.FK.OrderLine>>>>(bqlCommand.WhereAnd<Where<POReceiptLine.receiptType, Equal<POReceiptType.poreceipt>>>()) : BqlCommand.AppendJoin<InnerJoin<PX.Objects.SO.SOLine, On<POReceiptLine.FK.SOLine>>>(bqlCommand.WhereAnd<Where<POReceiptLine.receiptType, Equal<POReceiptType.poreturn>>>()));
  }

  protected virtual void TryToGetProjectAndTask(
    PXResult<POReceiptLine> res,
    POReceiptLine line,
    out PX.Objects.PM.Lite.PMProject project,
    out PX.Objects.PM.Lite.PMTask task)
  {
    project = (PX.Objects.PM.Lite.PMProject) null;
    task = (PX.Objects.PM.Lite.PMTask) null;
  }

  public class CostAccrual : NonStockAccrualGraph<POReceiptEntry, POReceipt>
  {
    [PXOverride]
    public virtual void SetExpenseAccount(
      PXCache sender,
      PXFieldDefaultingEventArgs e,
      PX.Objects.IN.InventoryItem item,
      Action<PXCache, PXFieldDefaultingEventArgs, PX.Objects.IN.InventoryItem> baseMethod)
    {
      POReceiptLine row = (POReceiptLine) e.Row;
      if (row == null || !row.AccrueCost.GetValueOrDefault())
        return;
      this.SetExpenseAccountSub(sender, e, item, row.SiteID, (NonStockAccrualGraph<POReceiptEntry, POReceipt>.GetAccountSubUsingPostingClassDelegate) ((inItem, inSite, inPostClass) => (object) INReleaseProcess.GetAcctID<INPostClass.invtAcctID>((PXGraph) this.Base, inPostClass.InvtAcctDefault, inItem, inSite, inPostClass)), (NonStockAccrualGraph<POReceiptEntry, POReceipt>.GetAccountSubFromItemDelegate) (inItem => (object) inItem.InvtAcctID));
    }

    [PXOverride]
    public virtual object GetExpenseSub(
      PXCache sender,
      PXFieldDefaultingEventArgs e,
      PX.Objects.IN.InventoryItem item,
      Func<PXCache, PXFieldDefaultingEventArgs, PX.Objects.IN.InventoryItem, object> baseMethod)
    {
      POReceiptLine row = (POReceiptLine) e.Row;
      object expenseSub = (object) null;
      if (row != null && row.AccrueCost.GetValueOrDefault())
        expenseSub = this.GetExpenseAccountSub(sender, e, item, row.SiteID, (NonStockAccrualGraph<POReceiptEntry, POReceipt>.GetAccountSubUsingPostingClassDelegate) ((inItem, inSite, inPostClass) => (object) INReleaseProcess.GetSubID<INPostClass.invtSubID>((PXGraph) this.Base, inPostClass.InvtAcctDefault, inPostClass.InvtSubMask, inItem, inSite, inPostClass)), (NonStockAccrualGraph<POReceiptEntry, POReceipt>.GetAccountSubFromItemDelegate) (inItem => (object) inItem.InvtSubID));
      return expenseSub;
    }
  }

  public class MultiCurrency : MultiCurrencyGraph<POReceiptEntry, POReceipt>
  {
    protected override string Module => "PO";

    protected override MultiCurrencyGraph<POReceiptEntry, POReceipt>.CurySourceMapping GetCurySourceMapping()
    {
      return new MultiCurrencyGraph<POReceiptEntry, POReceipt>.CurySourceMapping(typeof (PX.Objects.AP.Vendor));
    }

    protected override MultiCurrencyGraph<POReceiptEntry, POReceipt>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<POReceiptEntry, POReceipt>.DocumentMapping(typeof (POReceipt))
      {
        DocumentDate = typeof (POReceipt.receiptDate),
        BAccountID = typeof (POReceipt.vendorID)
      };
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[2]
      {
        (PXSelectBase) this.Base.Document,
        (PXSelectBase) this.Base.transactions
      };
    }

    protected override void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyID> e)
    {
      if (((PXSelectBase<POReceipt>) this.Base.Document).Current?.ReceiptType == "RX")
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyID>, PX.Objects.CM.Extensions.CurrencyInfo, object>) e).NewValue = (object) this.GetBaseCurency();
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyID>>) e).Cancel = true;
      }
      else
        base._(e);
    }

    protected override void _(
      PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.documentDate> e)
    {
      POReceipt main = (POReceipt) ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.documentDate>>) e).Cache.GetMain<PX.Objects.Extensions.MultiCurrency.Document>(e.Row);
      if (main == null || !(main.ReceiptType != "RN") && !(main.ReturnInventoryCostMode != "O"))
        return;
      this.DateFieldUpdated<PX.Objects.Extensions.MultiCurrency.Document.curyInfoID, PX.Objects.Extensions.MultiCurrency.Document.documentDate>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.documentDate>>) e).Cache, (IBqlTable) e.Row);
    }

    protected override void _(PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.branchID> e)
    {
      POReceipt main = (POReceipt) ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.branchID>>) e).Cache.GetMain<PX.Objects.Extensions.MultiCurrency.Document>(e.Row);
      bool resetCuryID = !((PXGraph) this.Base).IsCopyPasteContext && ((main != null ? (!main.VendorID.HasValue ? 1 : 0) : 1) != 0 && ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.branchID>>) e).ExternalCall || main?.ReceiptType == "RX");
      this.SourceFieldUpdated<PX.Objects.Extensions.MultiCurrency.Document.curyInfoID, PX.Objects.Extensions.MultiCurrency.Document.curyID, PX.Objects.Extensions.MultiCurrency.Document.documentDate>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.branchID>>) e).Cache, (IBqlTable) e.Row, resetCuryID);
    }

    protected override void _(
      PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.bAccountID> e)
    {
      if (!((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.bAccountID>>) e).ExternalCall && e.Row?.CuryID != null || !this.Base.AllowNonBaseCurrency() || ((PXGraph) this.Base).IsCopyPasteContext)
        return;
      this.SourceFieldUpdated<PX.Objects.Extensions.MultiCurrency.Document.curyInfoID, PX.Objects.Extensions.MultiCurrency.Document.curyID, PX.Objects.Extensions.MultiCurrency.Document.documentDate>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.bAccountID>>) e).Cache, (IBqlTable) e.Row);
    }

    protected override void _(PX.Data.Events.RowSelected<PX.Objects.Extensions.MultiCurrency.Document> e)
    {
      if (this.Base.SkipUIUpdate)
        return;
      PXUIFieldAttribute.SetVisible<POReceipt.curyID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.Extensions.MultiCurrency.Document>>) e).Cache, (object) e.Row, this.Base.AllowNonBaseCurrency());
      base._(e);
    }

    protected override bool AllowOverrideCury()
    {
      return !this.Base.HasTransactions() && this.Base.AllowNonBaseCurrency() && ((PXSelectBase<PX.Objects.AP.Vendor>) this.Base.vendor).Current != null && ((PXSelectBase<PX.Objects.AP.Vendor>) this.Base.vendor).Current.AllowOverrideCury.GetValueOrDefault();
    }

    protected override bool AllowOverrideRate(PXCache sender, PX.Objects.CM.Extensions.CurrencyInfo info, CurySource source)
    {
      POReceipt current1 = ((PXSelectBase<POReceipt>) this.Base.Document).Current;
      POSetup current2 = ((PXSelectBase<POSetup>) this.Base.posetup).Current;
      bool? nullable;
      int num1;
      if (current2 == null)
      {
        num1 = 0;
      }
      else
      {
        nullable = current2.ChangeCuryRateOnReceipt;
        num1 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      bool flag = num1 != 0;
      int num2;
      if (base.AllowOverrideRate(sender, info, source))
      {
        if (current1 == null)
        {
          num2 = 1;
        }
        else
        {
          nullable = current1.Released;
          num2 = !nullable.GetValueOrDefault() ? 1 : 0;
        }
      }
      else
        num2 = 0;
      int num3 = flag ? 1 : 0;
      if ((num2 & num3) == 0 || !PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
        return false;
      if (current1?.ReceiptType == "RT")
        return true;
      return current1?.ReceiptType == "RN" && current1?.ReturnInventoryCostMode != "O";
    }
  }

  public abstract class PopupHandler
  {
    protected POReceiptEntry _graph;

    public PopupHandler(POReceiptEntry graph) => this._graph = graph;

    public abstract PXView View { get; }

    public abstract object GetSourceItem();

    public abstract void TryGetSourceItem(POReceiptEntry.POReceiptLineS filter);

    public abstract void SetFilterToSource(
      PXCache sender,
      POReceiptEntry.POReceiptLineS filter,
      object _source);

    public abstract void SetFilterToError(PXCache sender, POReceiptEntry.POReceiptLineS filter);
  }

  public class AddReceiptPopupHandler(POReceiptEntry graph) : POReceiptEntry.PopupHandler(graph)
  {
    public override PXView View => ((PXSelectBase) this._graph.poLinesSelection).View;

    public override void TryGetSourceItem(POReceiptEntry.POReceiptLineS filter)
    {
      PXResultset<POReceiptEntry.POLineS> pxResultset = ((PXSelectBase<POReceiptEntry.POLineS>) this._graph.poLinesSelection).Select(Array.Empty<object>());
      POReceiptEntry.POLineS poLineS1 = (POReceiptEntry.POLineS) null;
      int num1 = 0;
      foreach (PXResult<POReceiptEntry.POLineS> pxResult in pxResultset)
      {
        POReceiptEntry.POLineS poLineS2 = PXResult<POReceiptEntry.POLineS>.op_Implicit(pxResult);
        POLine referencedPoLine = this._graph.GetReferencedPOLine(poLineS2.OrderType, poLineS2.OrderNbr, poLineS2.LineNbr);
        if (referencedPoLine != null)
        {
          Decimal? orderQty = poLineS2.OrderQty;
          Decimal? receivedQty = referencedPoLine.ReceivedQty;
          Decimal? nullable = orderQty.HasValue & receivedQty.HasValue ? new Decimal?(orderQty.GetValueOrDefault() - receivedQty.GetValueOrDefault()) : new Decimal?();
          Decimal num2 = 0M;
          if (!(nullable.GetValueOrDefault() <= num2 & nullable.HasValue))
          {
            if (poLineS2.Selected.GetValueOrDefault())
            {
              num1 = 1;
              break;
            }
            ++num1;
            if (poLineS1 == null)
            {
              poLineS1 = poLineS2;
              continue;
            }
            continue;
          }
        }
        poLineS2.Selected = new bool?(false);
      }
      if (num1 > 1)
      {
        filter.FetchMode = new bool?(true);
        POReceiptEntry.POLineS poLineS3 = (POReceiptEntry.POLineS) null;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        if (((PXSelectBase<POReceiptEntry.POLineS>) this._graph.poLinesSelection).AskExt(POReceiptEntry.AddReceiptPopupHandler.\u003C\u003Ec.\u003C\u003E9__3_0 ?? (POReceiptEntry.AddReceiptPopupHandler.\u003C\u003Ec.\u003C\u003E9__3_0 = new PXView.InitializePanel((object) POReceiptEntry.AddReceiptPopupHandler.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CTryGetSourceItem\u003Eb__3_0)))) == 1)
        {
          foreach (POReceiptEntry.POLineS poLineS4 in ((PXSelectBase) this._graph.poLinesSelection).Cache.Updated)
          {
            if (poLineS4.Selected.GetValueOrDefault())
              poLineS3 = poLineS4;
          }
        }
        if (poLineS3 == null && ((PXSelectBase) this._graph.poLinesSelection).View.Answer == 1)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ((PXSelectBase<POReceiptEntry.POLineS>) this._graph.poLinesSelection).AskExt(POReceiptEntry.AddReceiptPopupHandler.\u003C\u003Ec.\u003C\u003E9__3_1 ?? (POReceiptEntry.AddReceiptPopupHandler.\u003C\u003Ec.\u003C\u003E9__3_1 = new PXView.InitializePanel((object) POReceiptEntry.AddReceiptPopupHandler.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CTryGetSourceItem\u003Eb__3_1))));
        }
      }
      ((PXSelectBase) this._graph.poLinesSelection).View.SetAnswer((string) null, (WebDialogResult) 0);
      filter.FetchMode = new bool?(false);
    }

    public override object GetSourceItem()
    {
      PXResultset<POReceiptEntry.POLineS> pxResultset = ((PXSelectBase<POReceiptEntry.POLineS>) this._graph.poLinesSelection).Select(Array.Empty<object>());
      POReceiptEntry.POLineS sourceItem = (POReceiptEntry.POLineS) null;
      int num1 = 0;
      foreach (PXResult<POReceiptEntry.POLineS> pxResult in pxResultset)
      {
        POReceiptEntry.POLineS poLineS = PXResult<POReceiptEntry.POLineS>.op_Implicit(pxResult);
        POLine referencedPoLine = this._graph.GetReferencedPOLine(poLineS.OrderType, poLineS.OrderNbr, poLineS.LineNbr);
        if (referencedPoLine != null)
        {
          if (((PXSelectBase<POReceipt>) this._graph.Document).Current.ReceiptType == "RT")
          {
            Decimal? orderQty = poLineS.OrderQty;
            Decimal? receivedQty = referencedPoLine.ReceivedQty;
            Decimal? nullable = orderQty.HasValue & receivedQty.HasValue ? new Decimal?(orderQty.GetValueOrDefault() - receivedQty.GetValueOrDefault()) : new Decimal?();
            Decimal num2 = 0M;
            if (nullable.GetValueOrDefault() <= num2 & nullable.HasValue)
              goto label_7;
          }
          if (((PXSelectBase<POReceipt>) this._graph.Document).Current.ReceiptType == "RN")
          {
            Decimal? receivedQty = referencedPoLine.ReceivedQty;
            Decimal num3 = 0M;
            if (receivedQty.GetValueOrDefault() <= num3 & receivedQty.HasValue)
              goto label_7;
          }
          if (poLineS.Selected.GetValueOrDefault())
          {
            sourceItem = poLineS;
            num1 = 1;
            break;
          }
          ++num1;
          if (sourceItem == null)
          {
            sourceItem = poLineS;
            continue;
          }
          continue;
        }
label_7:
        poLineS.Selected = new bool?(false);
      }
      if (num1 > 1)
      {
        sourceItem = (POReceiptEntry.POLineS) null;
        if (((PXSelectBase) this._graph.poLinesSelection).View.Answer == 1)
        {
          foreach (POReceiptEntry.POLineS poLineS in ((PXSelectBase) this._graph.poLinesSelection).Cache.Updated)
          {
            if (poLineS.Selected.GetValueOrDefault())
              sourceItem = poLineS;
          }
        }
      }
      return (object) sourceItem;
    }

    public override void SetFilterToSource(
      PXCache sender,
      POReceiptEntry.POReceiptLineS filter,
      object _source)
    {
      POReceiptEntry.POLineS poLineS = _source as POReceiptEntry.POLineS;
      if (!filter.VendorID.HasValue)
      {
        POOrder poOrder = PXResultset<POOrder>.op_Implicit(PXSelectBase<POOrder, PXSelect<POOrder, Where<POOrder.orderType, Equal<Required<POOrder.orderType>>, And<POOrder.orderNbr, Equal<Required<POOrder.orderNbr>>>>>.Config>.SelectWindowed((PXGraph) this._graph, 0, 1, new object[2]
        {
          (object) poLineS.OrderType,
          (object) poLineS.OrderNbr
        }));
        filter.VendorID = poOrder.VendorID;
        filter.VendorLocationID = poOrder.VendorLocationID;
      }
      filter.InventoryID = poLineS.InventoryID;
      filter.SubItemID = poLineS.SubItemID;
      filter.UOM = poLineS.UOM;
      filter.SiteID = poLineS.SiteID;
      filter.POType = poLineS.OrderType;
      filter.PONbr = poLineS.OrderNbr;
      filter.POLineNbr = poLineS.LineNbr;
      filter.UnitCost = poLineS.UnitCost;
      filter.POAccrualType = poLineS.POAccrualType;
      sender.SetDefaultExt<POReceiptEntry.POReceiptLineS.locationID>((object) filter);
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this._graph, filter.InventoryID);
      INLotSerClass inLotSerClass = INLotSerClass.PK.Find((PXGraph) this._graph, inventoryItem?.LotSerClassID);
      Decimal? nullable1 = new Decimal?();
      Decimal? nullable2 = new Decimal?();
      Decimal? nullable3;
      Decimal? nullable4;
      if (poLineS != null)
      {
        POLine referencedPoLine = poLineS != null ? this._graph.GetReferencedPOLine(poLineS.OrderType, poLineS.OrderNbr, poLineS.LineNbr) : (POLine) null;
        if (referencedPoLine != null)
        {
          Decimal? nullable5;
          if (!(((PXSelectBase<POReceipt>) this._graph.Document).Current.ReceiptType == "RT"))
          {
            nullable5 = referencedPoLine.ReceivedQty;
          }
          else
          {
            Decimal? orderQty = poLineS.OrderQty;
            nullable3 = referencedPoLine.ReceivedQty;
            nullable5 = orderQty.HasValue & nullable3.HasValue ? new Decimal?(orderQty.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
          }
          nullable1 = nullable5;
          Decimal? nullable6;
          if (!(((PXSelectBase<POReceipt>) this._graph.Document).Current.ReceiptType == "RT"))
          {
            nullable6 = referencedPoLine.BaseReceivedQty;
          }
          else
          {
            nullable3 = poLineS.BaseOrderQty;
            nullable4 = referencedPoLine.BaseReceivedQty;
            nullable6 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
          }
          nullable2 = nullable6;
        }
        nullable4 = nullable1;
        Decimal num = 0M;
        if (nullable4.GetValueOrDefault() < num & nullable4.HasValue)
        {
          nullable1 = new Decimal?(0M);
          nullable2 = new Decimal?(0M);
        }
      }
      Decimal num1 = nullable1.GetValueOrDefault();
      if (inLotSerClass != null && inLotSerClass.LotSerTrack == "S" && inLotSerClass.LotSerAssign == "R")
      {
        num1 = 1M;
        filter.UOM = inventoryItem.BaseUnit;
      }
      else if (filter.ByOne.GetValueOrDefault() || poLineS == null)
        num1 = 1M;
      sender.SetValueExt<POReceiptEntry.POReceiptLineS.receiptQty>((object) filter, (object) num1);
      if (nullable2.HasValue)
      {
        nullable4 = filter.BaseReceiptQty;
        nullable3 = nullable2;
        if (nullable4.GetValueOrDefault() > nullable3.GetValueOrDefault() & nullable4.HasValue & nullable3.HasValue)
          sender.SetValueExt<POReceiptEntry.POReceiptLineS.receiptQty>((object) filter, (object) 0M);
      }
      if (poLineS == null)
        return;
      sender.SetValueExt<POReceiptEntry.POReceiptLineS.unitCost>((object) filter, (object) poLineS.UnitCost);
    }

    public override void SetFilterToError(PXCache sender, POReceiptEntry.POReceiptLineS filter)
    {
      filter.PONbr = (string) null;
      filter.POLineNbr = new int?();
      sender.SetDefaultExt<POReceiptEntry.POReceiptLineS.uOM>((object) filter);
      sender.SetDefaultExt<POReceiptEntry.POReceiptLineS.siteID>((object) filter);
      sender.SetValueExt<POReceiptEntry.POReceiptLineS.receiptQty>((object) filter, (object) 0M);
      if (filter.VendorID.HasValue)
        sender.SetDefaultExt<POReceiptEntry.POReceiptLineS.unitCost>((object) filter);
      sender.RaiseExceptionHandling<POReceiptEntry.POReceiptLineS.pONbr>((object) filter, (object) null, (Exception) new PXSetPropertyException("There are no lines in open purchase orders that match specified criteria.", (PXErrorLevel) 2));
    }
  }

  public class AddTransferPopupHandler(POReceiptEntry graph) : POReceiptEntry.PopupHandler(graph)
  {
    public override PXView View => ((PXSelectBase) this._graph.intranSelection).View;

    public override void TryGetSourceItem(POReceiptEntry.POReceiptLineS filter)
    {
      PXResultset<INTran> pxResultset = ((PXSelectBase<INTran>) this._graph.intranSelection).Select(Array.Empty<object>());
      INTran inTran1 = (INTran) null;
      int num = 0;
      foreach (PXResult<INTran> pxResult in pxResultset)
      {
        INTran inTran2 = PXResult<INTran>.op_Implicit(pxResult);
        if (inTran2.Selected.GetValueOrDefault())
        {
          num = 1;
          break;
        }
        ++num;
        if (inTran1 == null)
          inTran1 = inTran2;
      }
      if (num > 1)
      {
        filter.FetchMode = new bool?(true);
        INTran inTran3 = (INTran) null;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        if (((PXSelectBase<INTran>) this._graph.intranSelection).AskExt(POReceiptEntry.AddTransferPopupHandler.\u003C\u003Ec.\u003C\u003E9__3_0 ?? (POReceiptEntry.AddTransferPopupHandler.\u003C\u003Ec.\u003C\u003E9__3_0 = new PXView.InitializePanel((object) POReceiptEntry.AddTransferPopupHandler.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CTryGetSourceItem\u003Eb__3_0)))) == 1)
        {
          foreach (INTran inTran4 in ((PXSelectBase) this._graph.intranSelection).Cache.Updated)
          {
            if (inTran4.Selected.GetValueOrDefault())
              inTran3 = inTran4;
          }
        }
        if (inTran3 == null && ((PXSelectBase) this._graph.intranSelection).View.Answer == 1)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ((PXSelectBase<INTran>) this._graph.intranSelection).AskExt(POReceiptEntry.AddTransferPopupHandler.\u003C\u003Ec.\u003C\u003E9__3_1 ?? (POReceiptEntry.AddTransferPopupHandler.\u003C\u003Ec.\u003C\u003E9__3_1 = new PXView.InitializePanel((object) POReceiptEntry.AddTransferPopupHandler.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CTryGetSourceItem\u003Eb__3_1))));
        }
      }
      ((PXSelectBase) this._graph.intranSelection).View.SetAnswer((string) null, (WebDialogResult) 0);
      filter.FetchMode = new bool?(false);
    }

    public override object GetSourceItem()
    {
      PXResultset<INTran> pxResultset = ((PXSelectBase<INTran>) this._graph.intranSelection).Select(Array.Empty<object>());
      INTran sourceItem = (INTran) null;
      int num = 0;
      foreach (PXResult<INTran> pxResult in pxResultset)
      {
        INTran inTran = PXResult<INTran>.op_Implicit(pxResult);
        if (inTran.Selected.GetValueOrDefault())
        {
          sourceItem = inTran;
          num = 1;
          break;
        }
        ++num;
        if (sourceItem == null)
          sourceItem = inTran;
      }
      if (num > 1)
      {
        sourceItem = (INTran) null;
        if (((PXSelectBase) this._graph.intranSelection).View.Answer == 1)
        {
          foreach (INTran inTran in ((PXSelectBase) this._graph.intranSelection).Cache.Updated)
          {
            if (inTran.Selected.GetValueOrDefault())
              sourceItem = inTran;
          }
        }
      }
      return (object) sourceItem;
    }

    public override void SetFilterToSource(
      PXCache sender,
      POReceiptEntry.POReceiptLineS filter,
      object _source)
    {
      INTran inTran = _source as INTran;
      filter.InventoryID = inTran.InventoryID;
      filter.SubItemID = inTran.SubItemID;
      filter.UOM = inTran.UOM;
      filter.ShipFromSiteID = inTran.SiteID;
      filter.SiteID = inTran.ToSiteID;
      filter.SOOrderType = inTran.SOOrderType;
      filter.SOOrderNbr = inTran.SOOrderNbr;
      filter.SOOrderLineNbr = inTran.SOOrderLineNbr;
      filter.SOShipmentNbr = inTran.SOShipmentNbr;
      filter.OrigRefNbr = inTran.RefNbr;
      filter.OrigLineNbr = inTran.LineNbr;
      filter.UnitCost = inTran.UnitCost;
      sender.SetDefaultExt<POReceiptEntry.POReceiptLineS.locationID>((object) filter);
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this._graph, filter.InventoryID);
      INLotSerClass inLotSerClass = INLotSerClass.PK.Find((PXGraph) this._graph, inventoryItem?.LotSerClassID);
      Decimal? qty = inTran.Qty;
      Decimal? baseQty = inTran.BaseQty;
      Decimal num = qty.GetValueOrDefault();
      if (inLotSerClass != null && inLotSerClass.LotSerTrack == "S" && inLotSerClass.LotSerAssign == "R")
      {
        num = 1M;
        filter.UOM = inventoryItem.BaseUnit;
      }
      else if (filter.ByOne.GetValueOrDefault() || inTran == null)
        num = 1M;
      sender.SetValueExt<POReceiptEntry.POReceiptLineS.receiptQty>((object) filter, (object) num);
      if (baseQty.HasValue)
      {
        Decimal? baseReceiptQty = filter.BaseReceiptQty;
        Decimal? nullable = baseQty;
        if (baseReceiptQty.GetValueOrDefault() > nullable.GetValueOrDefault() & baseReceiptQty.HasValue & nullable.HasValue)
          sender.SetValueExt<POReceiptEntry.POReceiptLineS.receiptQty>((object) filter, (object) 0M);
      }
      if (inTran == null)
        return;
      sender.SetValueExt<POReceiptEntry.POReceiptLineS.unitCost>((object) filter, (object) inTran.UnitCost);
    }

    public override void SetFilterToError(PXCache sender, POReceiptEntry.POReceiptLineS filter)
    {
      filter.SOOrderType = (string) null;
      filter.SOOrderNbr = (string) null;
      filter.SOOrderLineNbr = new int?();
      filter.OrigRefNbr = (string) null;
      filter.OrigLineNbr = new int?();
      sender.SetDefaultExt<POReceiptEntry.POReceiptLineS.uOM>((object) filter);
      sender.SetDefaultExt<POReceiptEntry.POReceiptLineS.siteID>((object) filter);
      sender.SetValueExt<POReceiptEntry.POReceiptLineS.receiptQty>((object) filter, (object) 0M);
      if (filter.VendorID.HasValue)
        sender.SetDefaultExt<POReceiptEntry.POReceiptLineS.unitCost>((object) filter);
      sender.RaiseExceptionHandling<POReceiptEntry.POReceiptLineS.sOOrderNbr>((object) filter, (object) null, (Exception) new PXSetPropertyException("There are no unreceived lines in transfer orders that match specified criteria.", (PXErrorLevel) 4));
    }
  }

  [Serializable]
  public class POOrderFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _VendorID;
    protected int? _VendorLocationID;
    protected int? _InventoryID;
    protected int? _SubItemID;
    protected string _OrderType;
    protected string _OrderNbr;
    protected string _SOOrderNbr;
    protected int? _ShipToBAccountID;
    protected int? _ShipToLocationID;
    protected int? _ShipFromSiteID;
    protected bool? _ResetFilter;
    protected bool? _AllowAddLine;

    [Vendor(typeof (Search<BAccountR.bAccountID, Where<True, Equal<True>>>))]
    [VerndorNonEmployeeOrOrganizationRestrictor(typeof (POReceipt.receiptType))]
    [PXRestrictor(typeof (Where<PX.Objects.AP.Vendor.vStatus, IsNull, Or<PX.Objects.AP.Vendor.vStatus, In3<VendorStatus.active, VendorStatus.oneTime, VendorStatus.holdPayments>>>), "The vendor status is '{0}'.", new System.Type[] {typeof (PX.Objects.AP.Vendor.vStatus)})]
    [PXDefault(typeof (POReceipt.vendorID))]
    public virtual int? VendorID
    {
      get => this._VendorID;
      set => this._VendorID = value;
    }

    [PXDefault(typeof (POReceipt.vendorLocationID))]
    [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POReceiptEntry.POOrderFilter.vendorID>>>))]
    public virtual int? VendorLocationID
    {
      get => this._VendorLocationID;
      set => this._VendorLocationID = value;
    }

    [PXString(2, IsFixed = true)]
    public virtual string ReceiptType { get; set; }

    [POReceiptLineInventory(typeof (POReceiptEntry.POOrderFilter.receiptType))]
    [PXDefault]
    public virtual int? InventoryID
    {
      get => this._InventoryID;
      set => this._InventoryID = value;
    }

    [SubItem(typeof (POReceiptEntry.POOrderFilter.inventoryID))]
    [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.defaultSubItemID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current2<POReceiptEntry.POOrderFilter.inventoryID>>, And<PX.Objects.IN.InventoryItem.defaultSubItemOnEntry, Equal<boolTrue>>>>))]
    [PXFormula(typeof (Default<POReceiptEntry.POOrderFilter.inventoryID>))]
    public virtual int? SubItemID
    {
      get => this._SubItemID;
      set => this._SubItemID = value;
    }

    [PXDBString(2, IsFixed = true)]
    [PXDefault("RO")]
    [POOrderType.RegularDropShipList]
    [PXUIField(DisplayName = "Type")]
    public virtual string OrderType
    {
      get => this._OrderType;
      set => this._OrderType = value;
    }

    [PXDBString(15, IsUnicode = true, InputMask = "")]
    [PXDefault]
    [PXUIField]
    [PX.Objects.PO.PO.RefNbr(typeof (Search2<POReceiptEntry.POOrderS.orderNbr, LeftJoin<PX.Objects.AP.Vendor, On<POOrder.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>, Where<POReceiptEntry.POOrderS.orderType, Equal<Optional<POReceiptEntry.POOrderFilter.orderType>>, And<POReceiptEntry.POOrderS.hold, Equal<boolFalse>, And2<Where<POReceiptEntry.POOrderS.orderType, NotEqual<POOrderType.projectDropShip>, Or<POOrder.dropshipReceiptProcessing, Equal<DropshipReceiptProcessingOption.generateReceipt>>>, And2<Where<Current<POReceipt.vendorID>, IsNull, Or<POOrder.vendorID, Equal<Current<POReceipt.vendorID>>>>, And2<Where<Current<POReceipt.vendorLocationID>, IsNull, Or<POOrder.vendorLocationID, Equal<Current<POReceipt.vendorLocationID>>>>, And2<Where<POOrder.shipToBAccountID, Equal<Current<POReceiptEntry.POOrderFilter.shipToBAccountID>>, Or<Current<POReceiptEntry.POOrderFilter.shipToBAccountID>, IsNull>>, And2<Where<POOrder.shipToLocationID, Equal<Current<POReceiptEntry.POOrderFilter.shipToLocationID>>, Or<Current<POReceiptEntry.POOrderFilter.shipToLocationID>, IsNull>>, And<POReceiptEntry.POOrderS.cancelled, Equal<boolFalse>, And<POOrder.status, Equal<POOrderStatus.open>>>>>>>>>>>), Filterable = true)]
    public virtual string OrderNbr
    {
      get => this._OrderNbr;
      set => this._OrderNbr = value;
    }

    /// <summary>Transfer order order type</summary>
    [PXDBString(2, IsFixed = true)]
    [PXUIField(DisplayName = "Transfer Type")]
    [PXSelector(typeof (Search<PX.Objects.SO.SOOrderType.orderType, Where<PX.Objects.SO.SOOrderType.active, Equal<True>, And<PX.Objects.SO.SOOrderType.behavior, Equal<SOBehavior.tR>>>>))]
    public virtual string SOOrderType { get; set; }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Transfer Nbr.", Visible = false)]
    [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<POReceiptEntry.POOrderFilter.sOOrderType>>>>))]
    public virtual string SOOrderNbr
    {
      get => this._SOOrderNbr;
      set => this._SOOrderNbr = value;
    }

    [PXDBInt]
    [PXSelector(typeof (Search2<BAccount2.bAccountID, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<BAccount2.bAccountID>>>, Where<Optional<POReceiptEntry.POOrderFilter.orderType>, Equal<POOrderType.regularOrder>, Or<Where<PX.Objects.AR.Customer.bAccountID, IsNotNull, And<Optional<POReceiptEntry.POOrderFilter.orderType>, Equal<POOrderType.dropShip>>>>>>), new System.Type[] {typeof (PX.Objects.CR.BAccount.acctCD), typeof (PX.Objects.CR.BAccount.acctName), typeof (PX.Objects.CR.BAccount.type), typeof (PX.Objects.CR.BAccount.acctReferenceNbr), typeof (PX.Objects.CR.BAccount.parentBAccountID)}, SubstituteKey = typeof (PX.Objects.CR.BAccount.acctCD), DescriptionField = typeof (PX.Objects.CR.BAccount.acctName))]
    [PXDefault]
    [PXUIField(DisplayName = "Ship To")]
    public virtual int? ShipToBAccountID
    {
      get => this._ShipToBAccountID;
      set => this._ShipToBAccountID = value;
    }

    [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Optional<POReceiptEntry.POOrderFilter.shipToBAccountID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
    [PXDefault(typeof (Search<BAccount2.defLocationID, Where<BAccount2.bAccountID, Equal<Current<POReceiptEntry.POOrderFilter.shipToBAccountID>>>>))]
    [PXUIField(DisplayName = "Shipping Location")]
    public virtual int? ShipToLocationID
    {
      get => this._ShipToLocationID;
      set => this._ShipToLocationID = value;
    }

    [Site(DisplayName = "From Warehouse", DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
    public virtual int? ShipFromSiteID
    {
      get => this._ShipFromSiteID;
      set => this._ShipFromSiteID = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    public virtual bool? ResetFilter
    {
      get => this._ResetFilter;
      set => this._ResetFilter = value;
    }

    [PXDBBool]
    [PXDefault(true)]
    public virtual bool? AllowAddLine
    {
      get => this._AllowAddLine;
      set => this._AllowAddLine = value;
    }

    public abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POReceiptEntry.POOrderFilter.vendorID>
    {
    }

    public abstract class vendorLocationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POReceiptEntry.POOrderFilter.vendorLocationID>
    {
    }

    public abstract class receiptType : IBqlField, IBqlOperand
    {
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POReceiptEntry.POOrderFilter.inventoryID>
    {
    }

    public abstract class subItemID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POReceiptEntry.POOrderFilter.subItemID>
    {
    }

    public abstract class orderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POOrderFilter.orderType>
    {
    }

    public abstract class orderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POOrderFilter.orderNbr>
    {
    }

    public abstract class sOOrderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POOrderFilter.sOOrderType>
    {
    }

    public abstract class sOOrderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POOrderFilter.sOOrderNbr>
    {
    }

    public abstract class shipToBAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POReceiptEntry.POOrderFilter.shipToBAccountID>
    {
    }

    public abstract class shipToLocationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POReceiptEntry.POOrderFilter.shipToLocationID>
    {
    }

    public abstract class shipFromSiteID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POReceiptEntry.POOrderFilter.shipFromSiteID>
    {
    }

    public abstract class resetFilter : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POReceiptEntry.POOrderFilter.resetFilter>
    {
    }

    public abstract class allowAddLine : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POReceiptEntry.POOrderFilter.allowAddLine>
    {
    }
  }

  [POLineForReceivingProjection(Persistent = false)]
  [Serializable]
  public class POLineS : POLine
  {
    [PXDBString(2, IsKey = true, IsFixed = true)]
    [PXUIField]
    public override string OrderType
    {
      get => this._OrderType;
      set => this._OrderType = value;
    }

    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
    [PXUIField]
    public override string OrderNbr
    {
      get => this._OrderNbr;
      set => this._OrderNbr = value;
    }

    [PXDBInt(IsKey = true)]
    [PXUIField]
    public override int? LineNbr
    {
      get => this._LineNbr;
      set => this._LineNbr = value;
    }

    [Vendor(typeof (Search<BAccountR.bAccountID, Where<True, Equal<True>>>), DescriptionField = typeof (PX.Objects.CR.BAccount.acctName), CacheGlobal = true, Filterable = true)]
    [VerndorNonEmployeeOrOrganizationRestrictor]
    public override int? VendorID
    {
      get => this._VendorID;
      set => this._VendorID = value;
    }

    [PXDBLong]
    public override long? CuryInfoID
    {
      get => this._CuryInfoID;
      set => this._CuryInfoID = value;
    }

    [PXDBLong]
    public override long? PlanID
    {
      get => this._PlanID;
      set => this._PlanID = value;
    }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField]
    [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
    [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
    public override string TaxCategoryID
    {
      get => this._TaxCategoryID;
      set => this._TaxCategoryID = value;
    }

    [Inventory(Filterable = true, DisplayName = "Inventory ID")]
    public override int? InventoryID
    {
      get => this._InventoryID;
      set => this._InventoryID = value;
    }

    [PXDBDate]
    [PXUIField(DisplayName = "Promised Date")]
    public override DateTime? PromisedDate
    {
      get => this._PromisedDate;
      set => this._PromisedDate = value;
    }

    [PXDBGuid(false)]
    public override Guid? CommitmentID
    {
      get => this._CommitmentID;
      set => this._CommitmentID = value;
    }

    public new abstract class orderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POLineS.orderType>
    {
    }

    public new abstract class orderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POLineS.orderNbr>
    {
    }

    public new abstract class lineNbr : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptEntry.POLineS.lineNbr>
    {
    }

    public new abstract class vendorID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptEntry.POLineS.vendorID>
    {
    }

    public new abstract class receivedQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POReceiptEntry.POLineS.receivedQty>
    {
    }

    public new abstract class baseReceivedQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POReceiptEntry.POLineS.baseReceivedQty>
    {
    }

    public new abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      POReceiptEntry.POLineS.curyInfoID>
    {
    }

    public new abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POReceiptEntry.POLineS.selected>
    {
    }

    public new abstract class planID : BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    POReceiptEntry.POLineS.planID>
    {
    }

    public new abstract class lineType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POLineS.lineType>
    {
    }

    public new abstract class cancelled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POReceiptEntry.POLineS.cancelled>
    {
    }

    public new abstract class completed : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POReceiptEntry.POLineS.completed>
    {
    }

    public new abstract class orderQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POReceiptEntry.POLineS.orderQty>
    {
    }

    public new abstract class taxCategoryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POLineS.taxCategoryID>
    {
    }

    public new abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POReceiptEntry.POLineS.inventoryID>
    {
    }

    public new abstract class subItemID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POReceiptEntry.POLineS.subItemID>
    {
    }

    public new abstract class promisedDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      POReceiptEntry.POLineS.promisedDate>
    {
    }

    public new abstract class commitmentID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      POReceiptEntry.POLineS.commitmentID>
    {
    }
  }

  [PXProjection(typeof (Select5<POOrder, InnerJoin<POReceiptEntry.POLineS, On<POReceiptEntry.POLineS.orderType, Equal<POOrder.orderType>, And<POReceiptEntry.POLineS.orderNbr, Equal<POOrder.orderNbr>, And<POReceiptEntry.POLineS.lineType, NotEqual<POLineType.description>, And<POReceiptEntry.POLineS.completed, NotEqual<True>, And<POReceiptEntry.POLineS.cancelled, NotEqual<True>, And2<Where<CurrentValue<POReceiptEntry.POReceiptLineS.inventoryID>, IsNull, Or<POReceiptEntry.POLineS.inventoryID, Equal<CurrentValue<POReceiptEntry.POReceiptLineS.inventoryID>>>>, And<Where<CurrentValue<POReceiptEntry.POReceiptLineS.subItemID>, IsNull, Or<POReceiptEntry.POLineS.subItemID, Equal<CurrentValue<POReceiptEntry.POReceiptLineS.subItemID>>>>>>>>>>>>, Aggregate<GroupBy<POOrder.orderType, GroupBy<POOrder.orderNbr, GroupBy<POOrder.orderDate, GroupBy<POOrder.curyID, GroupBy<POOrder.curyOrderTotal, GroupBy<POOrder.hold, GroupBy<POOrder.status, GroupBy<POOrder.cancelled, GroupBy<POOrder.isTaxValid, GroupBy<POOrder.isUnbilledTaxValid, Sum<POReceiptEntry.POLineS.orderQty, Sum<POReceiptEntry.POLineS.receivedQty, Sum<POReceiptEntry.POLineS.baseReceivedQty>>>>>>>>>>>>>>>), Persistent = false)]
  [Serializable]
  public class POOrderS : POOrder
  {
    protected Decimal? _ReceivedQty;
    protected Decimal? _BaseReceivedQty;

    [PXDBQuantity(HandleEmptyKey = true, BqlField = typeof (POReceiptEntry.POLineS.receivedQty))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField]
    public virtual Decimal? ReceivedQty
    {
      get => this._ReceivedQty;
      set => this._ReceivedQty = value;
    }

    [PXDBDecimal(6, BqlField = typeof (POReceiptEntry.POLineS.baseReceivedQty))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField]
    public virtual Decimal? BaseReceivedQty
    {
      get => this._BaseReceivedQty;
      set => this._BaseReceivedQty = value;
    }

    [PXQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField]
    public virtual Decimal? LeftToReceiveQty
    {
      [PXDependsOnFields(new System.Type[] {typeof (POOrder.orderQty), typeof (POReceiptEntry.POOrderS.receivedQty)})] get
      {
        Decimal? orderQty = this.OrderQty;
        Decimal? receivedQty = this.ReceivedQty;
        return !(orderQty.HasValue & receivedQty.HasValue) ? new Decimal?() : new Decimal?(orderQty.GetValueOrDefault() - receivedQty.GetValueOrDefault());
      }
    }

    [PXDBLong]
    public override long? CuryInfoID
    {
      get => this._CuryInfoID;
      set => this._CuryInfoID = value;
    }

    public new abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POReceiptEntry.POOrderS.selected>
    {
    }

    public new abstract class orderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POOrderS.orderType>
    {
    }

    public new abstract class orderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POOrderS.orderNbr>
    {
    }

    public abstract class receivedQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POReceiptEntry.POOrderS.receivedQty>
    {
    }

    public abstract class baseReceivedQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POReceiptEntry.POOrderS.baseReceivedQty>
    {
    }

    public new abstract class hold : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceiptEntry.POOrderS.hold>
    {
    }

    public new abstract class cancelled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POReceiptEntry.POOrderS.cancelled>
    {
    }

    public new abstract class isTaxValid : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POReceiptEntry.POOrderS.isTaxValid>
    {
    }

    public new abstract class isUnbilledTaxValid : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POReceiptEntry.POOrderS.isUnbilledTaxValid>
    {
    }

    public abstract class leftToReceiveQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POReceiptEntry.POOrderS.leftToReceiveQty>
    {
    }

    public new abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      POReceiptEntry.POOrderS.curyInfoID>
    {
    }

    public new abstract class hasMultipleProjects : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POReceiptEntry.POOrderS.hasMultipleProjects>
    {
    }
  }

  [Serializable]
  public class POReceiptLineS : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _BarCode;
    protected int? _InventoryID;
    protected int? _VendorID;
    protected int? _VendorLocationID;
    protected int? _ShipFromSiteID;
    protected string _POType;
    protected string _PONbr;
    protected int? _POLineNbr;
    protected string _SOOrderType;
    protected string _SOOrderNbr;
    protected int? _SOOrderLineNbr;
    protected string _SOShipmentNbr;
    protected string _OrigRefNbr;
    protected int? _OrigLineNbr;
    protected short? _InvtMult;
    protected int? _SubItemID;
    protected string _UOM;
    protected int? _SiteID;
    protected int? _LocationID;
    protected string _LotSerialNbr;
    protected DateTime? _ExpireDate;
    protected Decimal? _ReceiptQty;
    protected Decimal? _BaseReceiptQty;
    protected long? _CuryInfoID;
    protected Decimal? _UnitCost;
    protected bool? _FetchMode;
    protected bool? _ByOne;
    protected bool? _AutoAddLine;
    protected string _Description;

    [PXDBString(255 /*0xFF*/, IsUnicode = true)]
    [PXUIField(DisplayName = "Barcode")]
    public virtual string BarCode
    {
      get => this._BarCode;
      set => this._BarCode = value;
    }

    [POReceiptLineInventory(typeof (POReceiptEntry.POReceiptLineS.receiptType))]
    [PXDefault]
    public virtual int? InventoryID
    {
      get => this._InventoryID;
      set => this._InventoryID = value;
    }

    [Vendor(typeof (Search<BAccountR.bAccountID, Where<True, Equal<True>>>), CacheGlobal = true, Filterable = true)]
    [VerndorNonEmployeeOrOrganizationRestrictor]
    [PXDBDefault(typeof (POReceipt.vendorID))]
    public virtual int? VendorID
    {
      get => this._VendorID;
      set => this._VendorID = value;
    }

    [PXDefault(typeof (Search<BAccountR.defLocationID, Where<BAccountR.bAccountID, Equal<Current<POReceiptEntry.POReceiptLineS.vendorID>>>>))]
    [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POReceiptEntry.POReceiptLineS.vendorID>>>))]
    [PXFormula(typeof (Default<POReceiptEntry.POReceiptLineS.vendorID>))]
    public virtual int? VendorLocationID
    {
      get => this._VendorLocationID;
      set => this._VendorLocationID = value;
    }

    [Site(DisplayName = "From Warehouse", DescriptionField = typeof (PX.Objects.IN.INSite.descr))]
    public virtual int? ShipFromSiteID
    {
      get => this._ShipFromSiteID;
      set => this._ShipFromSiteID = value;
    }

    [PXDBString(2, IsFixed = true)]
    [POOrderType.RegularDropShipList]
    [PXDefault("RO")]
    [PXUIField(DisplayName = "Order Type")]
    public virtual string POType
    {
      get => this._POType;
      set => this._POType = value;
    }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Order Nbr.")]
    [PX.Objects.PO.PO.RefNbr(typeof (Search2<POOrder.orderNbr, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<POOrder.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>, Where<POOrder.orderType, Equal<Optional<POReceiptEntry.POReceiptLineS.pOType>>, And<PX.Objects.AP.Vendor.bAccountID, IsNotNull>>, OrderBy<Desc<POOrder.orderNbr>>>), Filterable = true)]
    public virtual string PONbr
    {
      get => this._PONbr;
      set => this._PONbr = value;
    }

    [PXDBInt]
    [PXUIField(DisplayName = "Line Nbr.")]
    public virtual int? POLineNbr
    {
      get => this._POLineNbr;
      set => this._POLineNbr = value;
    }

    [PXDBString(1, IsFixed = true)]
    [POAccrualType.List]
    [PXUIField(DisplayName = "Billing Based On", Enabled = false)]
    public virtual string POAccrualType { get; set; }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Transfer Type", Enabled = false)]
    [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderType>))]
    public virtual string SOOrderType
    {
      get => this._SOOrderType;
      set => this._SOOrderType = value;
    }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Transfer Nbr.", Enabled = false)]
    [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr>))]
    public virtual string SOOrderNbr
    {
      get => this._SOOrderNbr;
      set => this._SOOrderNbr = value;
    }

    [PXDBInt]
    [PXUIField(DisplayName = "Line Nbr.", Enabled = false)]
    public virtual int? SOOrderLineNbr
    {
      get => this._SOOrderLineNbr;
      set => this._SOOrderLineNbr = value;
    }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Shipment Nbr.", Enabled = false)]
    [PXSelector(typeof (Search<PX.Objects.SO.SOShipment.shipmentNbr>))]
    public virtual string SOShipmentNbr
    {
      get => this._SOShipmentNbr;
      set => this._SOShipmentNbr = value;
    }

    [PXDBString(15, IsUnicode = true)]
    public virtual string OrigRefNbr
    {
      get => this._OrigRefNbr;
      set => this._OrigRefNbr = value;
    }

    [PXDBInt]
    public virtual int? OrigLineNbr
    {
      get => this._OrigLineNbr;
      set => this._OrigLineNbr = value;
    }

    public string TranType => POReceiptType.GetINTranType("RT");

    [PXDBShort]
    [PXDefault]
    public virtual short? InvtMult
    {
      get => this._InvtMult;
      set => this._InvtMult = value;
    }

    [SubItem(typeof (POReceiptEntry.POReceiptLineS.inventoryID))]
    public virtual int? SubItemID
    {
      get => this._SubItemID;
      set => this._SubItemID = value;
    }

    [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.purchaseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<POReceiptEntry.POReceiptLineS.inventoryID>>>>))]
    [INUnit(typeof (POReceiptEntry.POReceiptLineS.inventoryID))]
    public virtual string UOM
    {
      get => this._UOM;
      set => this._UOM = value;
    }

    [POSiteAvail(typeof (POReceiptEntry.POReceiptLineS.inventoryID), typeof (POReceiptEntry.POReceiptLineS.subItemID), typeof (CostCenter.freeStock))]
    [InterBranchRestrictor(typeof (Where<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<POReceipt.branchID>>>))]
    [PXDefault(typeof (Coalesce<Search2<LocationBranchSettings.vSiteID, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.siteID, Equal<LocationBranchSettings.vSiteID>>>, Where<LocationBranchSettings.locationID, Equal<Current2<POReceiptEntry.POReceiptLineS.vendorLocationID>>, And<LocationBranchSettings.bAccountID, Equal<Current2<POReceiptEntry.POReceiptLineS.vendorID>>, And<LocationBranchSettings.branchID, Equal<Current<AccessInfo.branchID>>, And<Where2<FeatureInstalled<FeaturesSet.interBranch>, Or<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<POReceipt.branchID>>>>>>>>>, Search2<PX.Objects.CR.Location.vSiteID, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.siteID, Equal<PX.Objects.CR.Location.vSiteID>>>, Where<PX.Objects.CR.Location.locationID, Equal<Current2<POReceiptEntry.POReceiptLineS.vendorLocationID>>, And<PX.Objects.CR.Location.bAccountID, Equal<Current2<POReceiptEntry.POReceiptLineS.vendorID>>, And<Where2<FeatureInstalled<FeaturesSet.interBranch>, Or<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<POReceipt.branchID>>>>>>>>, Search2<InventoryItemCurySettings.dfltSiteID, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.siteID, Equal<InventoryItemCurySettings.dfltSiteID>>>, Where<InventoryItemCurySettings.inventoryID, Equal<Current2<POReceiptEntry.POReceiptLineS.inventoryID>>, And<InventoryItemCurySettings.curyID, EqualBaseCuryID<Current2<POReceipt.branchID>>, And<Where2<FeatureInstalled<FeaturesSet.interBranch>, Or<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<POReceipt.branchID>>>>>>>>>))]
    [PXFormula(typeof (Default<POReceiptEntry.POReceiptLineS.vendorLocationID>))]
    [PXFormula(typeof (Default<POReceiptEntry.POReceiptLineS.inventoryID>))]
    public virtual int? SiteID
    {
      get => this._SiteID;
      set => this._SiteID = value;
    }

    [LocationAvail(typeof (POReceiptEntry.POReceiptLineS.inventoryID), typeof (POReceiptEntry.POReceiptLineS.subItemID), typeof (CostCenter.freeStock), typeof (POReceiptEntry.POReceiptLineS.siteID), typeof (POReceiptEntry.POReceiptLineS.tranType), typeof (POReceiptEntry.POReceiptLineS.invtMult), KeepEntry = false)]
    [PXFormula(typeof (Default<POReceiptEntry.POReceiptLineS.siteID>))]
    [PXFormula(typeof (Default<POReceiptEntry.POReceiptLineS.inventoryID>))]
    public virtual int? LocationID
    {
      get => this._LocationID;
      set => this._LocationID = value;
    }

    [LotSerialNbr]
    public virtual string LotSerialNbr
    {
      get => this._LotSerialNbr;
      set => this._LotSerialNbr = value;
    }

    [PXDBDate(InputMask = "d", DisplayMask = "d")]
    [PXUIField(DisplayName = "Expiration Date")]
    public virtual DateTime? ExpireDate
    {
      get => this._ExpireDate;
      set => this._ExpireDate = value;
    }

    [PXDBQuantity(typeof (POReceiptEntry.POReceiptLineS.uOM), typeof (POReceiptEntry.POReceiptLineS.baseReceiptQty), HandleEmptyKey = true, MinValue = 0.0)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField]
    public virtual Decimal? ReceiptQty
    {
      get => this._ReceiptQty;
      set => this._ReceiptQty = value;
    }

    public virtual Decimal? Qty
    {
      get => this._ReceiptQty;
      set => this._ReceiptQty = value;
    }

    [PXDBDecimal(6)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? BaseReceiptQty
    {
      get => this._BaseReceiptQty;
      set => this._BaseReceiptQty = value;
    }

    [PXDBLong]
    [CurrencyInfo(typeof (POReceipt.curyInfoID))]
    public virtual long? CuryInfoID
    {
      get => this._CuryInfoID;
      set => this._CuryInfoID = value;
    }

    [PXDBDecimal(typeof (Search<CommonSetup.decPlPrcCst>))]
    [PXUIField]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? UnitCost
    {
      get => this._UnitCost;
      set => this._UnitCost = value;
    }

    [PXDBBool]
    public virtual bool? FetchMode
    {
      get => this._FetchMode;
      set => this._FetchMode = value;
    }

    [PXDBBool]
    [PXUIField(DisplayName = "Add One Unit per Barcode")]
    [PXDefault(typeof (POSetup.receiptByOneBarcodeReceiptBarcode))]
    public virtual bool? ByOne
    {
      get => this._ByOne;
      set => this._ByOne = value;
    }

    [PXDBBool]
    [PXDefault(typeof (POSetup.autoAddLineReceiptBarcode))]
    [PXUIField(DisplayName = "Add Line Automatically")]
    public virtual bool? AutoAddLine
    {
      get => this._AutoAddLine;
      set => this._AutoAddLine = value;
    }

    [PXDBString(255 /*0xFF*/)]
    [PXUIField(DisplayName = "")]
    public virtual string Description
    {
      get => this._Description;
      set => this._Description = value;
    }

    [PXDBString(2, IsFixed = true)]
    public virtual string ReceiptType { get; set; }

    [PXDBInt]
    public virtual int? ReceiptVendorID { get; set; }

    [PXDBInt]
    public virtual int? ReceiptVendorLocationID { get; set; }

    public abstract class barCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.barCode>
    {
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.inventoryID>
    {
    }

    public abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.vendorID>
    {
    }

    public abstract class vendorLocationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.vendorLocationID>
    {
    }

    public abstract class shipFromSiteID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.shipFromSiteID>
    {
    }

    public abstract class pOType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.pOType>
    {
    }

    public abstract class pONbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.pONbr>
    {
    }

    public abstract class pOLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.pOLineNbr>
    {
    }

    public abstract class pOAccrualType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.pOAccrualType>
    {
    }

    public abstract class sOOrderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.sOOrderType>
    {
    }

    public abstract class sOOrderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.sOOrderNbr>
    {
    }

    public abstract class sOOrderLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.sOOrderLineNbr>
    {
    }

    public abstract class sOShipmentNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.sOShipmentNbr>
    {
    }

    public abstract class origRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.origRefNbr>
    {
    }

    public abstract class origLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.origLineNbr>
    {
    }

    public abstract class tranType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.tranType>
    {
    }

    public abstract class invtMult : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.invtMult>
    {
    }

    public abstract class subItemID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.subItemID>
    {
    }

    public abstract class uOM : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptEntry.POReceiptLineS.uOM>
    {
    }

    public abstract class siteID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptEntry.POReceiptLineS.siteID>
    {
    }

    public abstract class locationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.locationID>
    {
    }

    public abstract class lotSerialNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.lotSerialNbr>
    {
    }

    public abstract class expireDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.expireDate>
    {
    }

    public abstract class receiptQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.receiptQty>
    {
    }

    public abstract class baseReceiptQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.baseReceiptQty>
    {
    }

    public abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.curyInfoID>
    {
    }

    public abstract class unitCost : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.unitCost>
    {
    }

    public abstract class fetchMode : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.fetchMode>
    {
    }

    public abstract class byOne : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceiptEntry.POReceiptLineS.byOne>
    {
    }

    public abstract class autoAddLine : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.autoAddLine>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.description>
    {
    }

    public abstract class receiptType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.receiptType>
    {
    }

    public abstract class receiptVendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.receiptVendorID>
    {
    }

    public abstract class receiptVendorLocationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POReceiptEntry.POReceiptLineS.receiptVendorLocationID>
    {
    }
  }

  /// <exclude />
  public class ExtensionSort : 
    SortExtensionsBy<TypeArrayOf<PXGraphExtension<POReceiptEntry>>.FilledWith<POReceiptEntry.MultiCurrency, UpdatePOOnRelease, AffectedPOOrdersByPOReceipt>>
  {
  }
}
