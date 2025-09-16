// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POOrderEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Export;
using PX.Api.Models;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.DependencyInjection;
using PX.Data.Description;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.LicensePolicy;
using PX.Objects.AP;
using PX.Objects.AP.MigrationMode;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Scopes;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.EP;
using PX.Objects.Extensions.CostAccrual;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.IN.InventoryRelease;
using PX.Objects.IN.Services;
using PX.Objects.PM;
using PX.Objects.PO.DAC.Projections;
using PX.Objects.RQ;
using PX.Objects.SO.GraphExtensions.SO2PO;
using PX.Objects.TX;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

#nullable enable
namespace PX.Objects.PO;

[Serializable]
public class POOrderEntry : 
  PXGraph<
  #nullable disable
  POOrderEntry, POOrder>,
  PXImportAttribute.IPXPrepareItems,
  IGraphWithInitialization
{
  public PXSetup<PX.Objects.GL.Company> company;
  [PXViewName("Purchase Order")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (POOrder.sOOrderType), typeof (POOrder.sOOrderNbr), typeof (POOrder.isLegacyDropShip)})]
  public PXSelectJoin<POOrder, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<POOrder.vendorID>>>, Where<POOrder.orderType, Equal<Optional<POOrder.orderType>>, And<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>> Document;
  public PXSelect<POOrder, Where<POOrder.orderType, Equal<Current<POOrder.orderType>>, And<POOrder.orderNbr, Equal<Current<POOrder.orderNbr>>>>> CurrentDocument;
  [PXViewName("Purchase Order Line")]
  [PXImport(typeof (POOrder))]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (POLine.closed), typeof (POLine.cancelled), typeof (POLine.completed)})]
  public PXOrderedSelect<POOrder, POLine, Where<POLine.orderType, Equal<Current<POOrder.orderType>>, And<POLine.orderNbr, Equal<Optional<POOrder.orderNbr>>>>, OrderBy<Asc<POLine.orderType, Asc<POLine.orderNbr, Asc<POLine.sortOrder, Asc<POLine.lineNbr>>>>>> Transactions;
  public PXSelect<POTax, Where<POTax.orderType, Equal<Current<POOrder.orderType>>, And<POTax.orderNbr, Equal<Current<POOrder.orderNbr>>>>, OrderBy<Asc<POTax.orderType, Asc<POTax.orderNbr, Asc<POTax.taxID>>>>> Tax_Rows;
  public PXSelectJoin<POTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<POTaxTran.taxID>>>, Where<POTaxTran.orderType, Equal<Current<POOrder.orderType>>, And<POTaxTran.orderNbr, Equal<Current<POOrder.orderNbr>>>>> Taxes;
  public PXSelect<POOrderDiscountDetail, Where<POOrderDiscountDetail.orderType, Equal<Current<POOrder.orderType>>, And<POOrderDiscountDetail.orderNbr, Equal<Current<POOrder.orderNbr>>>>, OrderBy<Asc<POOrderDiscountDetail.lineNbr>>> DiscountDetails;
  [PXViewName("Remit Address")]
  public PXSelect<PORemitAddress, Where<PORemitAddress.addressID, Equal<Current<POOrder.remitAddressID>>>> Remit_Address;
  [PXViewName("Remit Contact")]
  public PXSelect<PORemitContact, Where<PORemitContact.contactID, Equal<Current<POOrder.remitContactID>>>> Remit_Contact;
  [PXViewName("Ship Address")]
  public PXSelect<POShipAddress, Where<POShipAddress.addressID, Equal<Current<POOrder.shipAddressID>>>> Shipping_Address;
  [PXViewName("Ship Contact")]
  public PXSelect<POShipContact, Where<POShipContact.contactID, Equal<Current<POOrder.shipContactID>>>> Shipping_Contact;
  public PXSelect<POSetupApproval, Where<POSetupApproval.orderType, Equal<Optional<POOrder.orderType>>>> SetupApproval;
  [PXViewName("Approval")]
  public EPApprovalAutomation<POOrder, POOrder.approved, POOrder.rejected, POOrder.hold, POSetupApproval> Approval;
  [PXCopyPasteHiddenView]
  public PXSelect<INReplenishmentOrder> Replenihment;
  [PXCopyPasteHiddenView]
  public PXSelect<INReplenishmentLine, Where<INReplenishmentLine.pOType, Equal<Current<POLine.orderType>>, And<INReplenishmentLine.pONbr, Equal<Current<POLine.orderNbr>>, And<INReplenishmentLine.pOLineNbr, Equal<Current<POLine.lineNbr>>>>>> ReplenishmentLines;
  public PXSelect<POItemCostManager.POVendorInventoryPriceUpdate> priceStatus;
  public PXSetup<PX.Objects.PO.POSetup> POSetup;
  public APSetupNoMigrationMode apsetup;
  public PXSetup<PX.Objects.IN.INSetup> INSetup;
  public PXSetup<PX.Objects.GL.Branch>.Where<BqlOperand<
  #nullable enable
  PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AccessInfo.branchID, IBqlInt>.FromCurrent>> Company;
  public 
  #nullable disable
  PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<POOrder.curyInfoID>>>> Current_currencyinfo;
  public PXSelect<RQRequisitionOrder> rqrequisitionorder;
  [PXFilterable(new System.Type[] {})]
  [PXCopyPasteHiddenView]
  public PXSelect<POOrderPOReceipt, Where<POOrderPOReceipt.pOType, Equal<Current<POOrder.orderType>>, And<POOrderPOReceipt.pONbr, Equal<Current<POOrder.orderNbr>>>>> Receipts;
  [PXCopyPasteHiddenView]
  public PXSelect<POOrderAPDoc, Where<POOrderAPDoc.pOOrderType, Equal<Current<POOrder.orderType>>, And<POOrderAPDoc.pONbr, Equal<Current<POOrder.orderNbr>>>>> APDocs;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<POBlanketOrderPOOrder, LeftJoin<POBlanketOrderPOReceipt, On<POBlanketOrderPOOrder.pOType, Equal<POBlanketOrderPOReceipt.pOType>, And<POBlanketOrderPOOrder.pONbr, Equal<POBlanketOrderPOReceipt.pONbr>, And<POBlanketOrderPOOrder.orderType, Equal<POBlanketOrderPOReceipt.orderType>, And<POBlanketOrderPOOrder.orderNbr, Equal<POBlanketOrderPOReceipt.orderNbr>>>>>>, Where<POBlanketOrderPOOrder.pOType, Equal<Current<POOrder.orderType>>, And<POBlanketOrderPOOrder.pONbr, Equal<Current<POOrder.orderNbr>>>>, OrderBy<Asc<POBlanketOrderPOOrder.orderNbr, Asc<POBlanketOrderPOReceipt.receiptNbr>>>> ChildOrdersReceipts;
  [PXCopyPasteHiddenView]
  public PXSelect<POBlanketOrderAPDoc, Where<POBlanketOrderAPDoc.pOType, Equal<Current<POOrder.orderType>>, And<POBlanketOrderAPDoc.pONbr, Equal<Current<POOrder.orderNbr>>>>> ChildOrdersAPDocs;
  [PXViewName("Main Contact")]
  public PXSelect<PX.Objects.CR.Contact> DefaultCompanyContact;
  [PXViewName("Vendor")]
  public PXSetup<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Optional<POOrder.vendorID>>>> vendor;
  public PXSetup<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<PX.Objects.AP.Vendor.vendorClassID>>>> vendorclass;
  [PXViewName("Employee")]
  public PXSetup<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.defContactID, Equal<Current<POOrder.ownerID>>>> Employee;
  public PXSetup<PX.Objects.TX.TaxZone, Where<PX.Objects.TX.TaxZone.taxZoneID, Equal<Current<POOrder.taxZoneID>>>> taxzone;
  public PXSetup<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POOrder.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Optional<POOrder.vendorLocationID>>>>> location;
  public PXFilter<POOrderEntry.POOrderFilter> filter;
  public PXSelect<POOrderEntry.POLineS, Where<POOrderEntry.POLineS.orderType, Equal<Current<POOrderEntry.POOrderFilter.orderType>>, And<POOrderEntry.POLineS.orderNbr, Equal<Current<POOrderEntry.POOrderFilter.orderNbr>>, And<POOrderEntry.POLineS.lineType, NotEqual<POLineType.description>, And<POOrderEntry.POLineS.completed, Equal<boolFalse>, And<Where<POOrderEntry.POLineS.orderQty, Equal<decimal0>, Or<POOrderEntry.POLineS.openQty, Greater<decimal0>>>>>>>>, OrderBy<Asc<POOrderEntry.POLineS.sortOrder>>> poLinesSelection;
  public PXSelectJoin<POOrderEntry.POOrderS, CrossJoin<APSetup>, Where<POOrderEntry.POOrderS.vendorID, Equal<Current<POOrder.vendorID>>, And<POOrderEntry.POOrderS.vendorLocationID, Equal<Current<POOrder.vendorLocationID>>, And<POOrderEntry.POOrderS.curyID, Equal<Current<POOrder.curyID>>, And<POOrderEntry.POOrderS.hold, Equal<boolFalse>, And<POOrderEntry.POOrderS.cancelled, Equal<boolFalse>, And<POOrderEntry.POOrderS.approved, Equal<boolTrue>, And<Where<POOrderEntry.POOrderS.payToVendorID, Equal<Current<POOrder.payToVendorID>>, Or<Not<FeatureInstalled<FeaturesSet.vendorRelations>>>>>>>>>>>, OrderBy<Asc<POOrderEntry.POOrderS.orderNbr>>> openOrders;
  public PXSelect<POLineR> poLiner;
  public PXSelect<POOrderEntry.POOrderR, Where<POOrderEntry.POOrderR.orderType, Equal<Required<POOrderEntry.POOrderR.orderType>>, And<POOrderEntry.POOrderR.orderNbr, Equal<Required<POOrderEntry.POOrderR.orderNbr>>, And<POOrderEntry.POOrderR.status, Equal<Required<POOrderEntry.POOrderR.status>>>>>> poOrder;
  [PXCopyPasteHiddenView]
  public PXSelect<POOrderEntry.SOLineSplit3, Where<POOrderEntry.SOLineSplit3.pOType, Equal<Optional<POLine.orderType>>, And<POOrderEntry.SOLineSplit3.pONbr, Equal<Optional<POLine.orderNbr>>, And<POOrderEntry.SOLineSplit3.pOLineNbr, Equal<Optional<POLine.lineNbr>>>>>> FixedDemand;
  [PXCopyPasteHiddenView]
  public PXSelectReadonly<PX.Objects.SO.SOLineSplit, Where<PX.Objects.SO.SOLineSplit.pOType, Equal<Optional<POLine.orderType>>, And<PX.Objects.SO.SOLineSplit.pONbr, Equal<Optional<POLine.orderNbr>>, And<PX.Objects.SO.SOLineSplit.pOLineNbr, Equal<Optional<POLine.lineNbr>>>>>> RelatedSOLineSplit;
  [PXCopyPasteHiddenView]
  public PXSelect<POOrderEntry.SOLine5, Where<POOrderEntry.SOLine5.orderType, Equal<Optional<POOrderEntry.SOLineSplit3.orderType>>, And<POOrderEntry.SOLine5.orderNbr, Equal<Optional<POOrderEntry.SOLineSplit3.orderNbr>>, And<POOrderEntry.SOLine5.lineNbr, Equal<Optional<POOrderEntry.SOLineSplit3.lineNbr>>>>>> FixedDemandOrigSOLine;
  public PXFilter<RecalcDiscountsParamFilter> recalcdiscountsfilter;
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.SO.SOLineSplit> solinesplit;
  protected readonly string viewInventoryID;
  public PXInitializeState<POOrder> initializeState;
  public PXAction<POOrder> hold;
  public PXAction<POOrder> putOnHold;
  public PXAction<POOrder> releaseFromHold;
  public PXAction<POOrder> cancelOrder;
  public PXAction<POOrder> reopenOrder;
  public PXAction<POOrder> markAsDontEmail;
  public PXAction<POOrder> markAsDontPrint;
  public PXAction<POOrder> action;
  public PXAction<POOrder> complete;
  public PXAction<POOrder> notification;
  public PXAction<POOrder> emailPurchaseOrder;
  public PXAction<POOrder> report;
  public PXAction<POOrder> vendorDetails;
  public PXAction<POOrder> printPurchaseOrder;
  public PXAction<POOrder> viewPurchaseOrderReceipt;
  public PXAction<POOrder> addPOOrder;
  public PXAction<POOrder> addPOOrderLine;
  public PXAction<POOrder> createPOReceipt;
  public PXAction<POOrder> createAPInvoice;
  public PXAction<POOrder> validateAddresses;
  public PXAction<POOrder> recalculateDiscountsAction;
  public PXAction<POOrder> recalcOk;
  public PXAction<POOrder> viewBlanketOrder;
  public PXAction<POOrder> viewDemand;
  protected bool _ExceptionHandling;
  private bool _blockUIUpdate;
  internal bool skipCostDefaulting;
  public PXWorkflowEventHandler<POOrder> OnLinesCompleted;
  public PXWorkflowEventHandler<POOrder> OnLinesClosed;
  public PXWorkflowEventHandler<POOrder> OnLinesReopened;
  public PXWorkflowEventHandler<POOrder> OnLinesLinked;
  public PXWorkflowEventHandler<POOrder> OnLinesUnlinked;
  public PXWorkflowEventHandler<POOrder> OnPrinted;
  public PXWorkflowEventHandler<POOrder> OnDoNotPrintChecked;
  public PXWorkflowEventHandler<POOrder> OnDoNotEmailChecked;
  public PXWorkflowEventHandler<POOrder> OnReleaseChangeOrder;

  private DiscountEngine<POLine, POOrderDiscountDetail> _discountEngine
  {
    get => DiscountEngineProvider.GetEngineFor<POLine, POOrderDiscountDetail>();
  }

  [InjectDependency]
  public IInventoryAccountService InventoryAccountService { get; set; }

  protected virtual void _(PX.Data.Events.FieldSelecting<POOrderAPDoc.statusText> e)
  {
    if (((PXSelectBase<POOrder>) this.Document).Current == null || e.Row == null)
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<POOrderAPDoc.statusText>>) e).ReturnValue = (object) this.GetPOOrderAPDocStatusText();
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<POBlanketOrderAPDoc.statusText> e)
  {
    if (((PXSelectBase<POOrder>) this.Document).Current == null || e.Row == null)
      return;
    APTranSigned apTranTotal = ((PXSelectBase<APTranSigned>) new PXSelectJoinGroupBy<APTranSigned, InnerJoin<POLine, On<APTranSigned.pOOrderType, Equal<POLine.orderType>, And<APTranSigned.pONbr, Equal<POLine.orderNbr>, And<APTranSigned.pOLineNbr, Equal<POLine.lineNbr>>>>>, Where<POLine.pOType, Equal<Current<POOrder.orderType>>, And<POLine.pONbr, Equal<Current<POOrder.orderNbr>>, And<APTranSigned.tranType, NotEqual<APDocType.prepayment>>>>, Aggregate<Sum<APTranSigned.signedBaseQty, Sum<APTranSigned.signedCuryTranAmt, Sum<APTranSigned.signedCuryRetainageAmt, Sum<APTranSigned.pOPPVAmt>>>>>>((PXGraph) this)).SelectSingle(Array.Empty<object>());
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<POBlanketOrderAPDoc.statusText>>) e).ReturnValue = (object) this.GetAPDocStatusText(apTranTotal);
  }

  protected virtual string GetPOOrderAPDocStatusText()
  {
    return this.GetAPDocStatusText(this.GetPOOrderAPDocTranSigned(false));
  }

  protected virtual APTranSigned GetPOOrderAPDocTranSigned(bool releasedOnly)
  {
    PXSelectGroupBy<APTranSigned, Where<APTranSigned.pOOrderType, Equal<Current<POOrder.orderType>>, And<APTranSigned.pONbr, Equal<Current<POOrder.orderNbr>>, And<APTranSigned.tranType, NotEqual<APDocType.prepayment>>>>, Aggregate<Sum<APTranSigned.signedBaseQty, Sum<APTranSigned.signedCuryTranAmt, Sum<APTranSigned.signedCuryRetainageAmt, Sum<APTranSigned.pOPPVAmt>>>>>> pxSelectGroupBy = new PXSelectGroupBy<APTranSigned, Where<APTranSigned.pOOrderType, Equal<Current<POOrder.orderType>>, And<APTranSigned.pONbr, Equal<Current<POOrder.orderNbr>>, And<APTranSigned.tranType, NotEqual<APDocType.prepayment>>>>, Aggregate<Sum<APTranSigned.signedBaseQty, Sum<APTranSigned.signedCuryTranAmt, Sum<APTranSigned.signedCuryRetainageAmt, Sum<APTranSigned.pOPPVAmt>>>>>>((PXGraph) this);
    if (releasedOnly)
      ((PXSelectBase<APTranSigned>) pxSelectGroupBy).WhereAnd<Where<BqlOperand<APTranSigned.released, IBqlBool>.IsEqual<True>>>();
    return ((PXSelectBase<APTranSigned>) pxSelectGroupBy).SelectSingle(Array.Empty<object>());
  }

  protected virtual int GetFieldPrecision<TTable, TField>(TTable row)
    where TTable : IBqlTable
    where TField : IBqlField
  {
    object obj = (object) null;
    ((PXGraph) this).Caches[typeof (TTable)].RaiseFieldSelecting<TField>((object) row, ref obj, true);
    return !(obj is PXDecimalState pxDecimalState) ? 0 : ((PXFieldState) pxDecimalState).Precision;
  }

  protected virtual (Decimal TotalBilledQty, Decimal TotalBilledAmt, Decimal TotalPPVAmt) GetAPDocStatusValues(
    APTranSigned apTranTotal)
  {
    Decimal? nullable1 = (Decimal?) apTranTotal?.SignedBaseQty;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    Decimal? nullable2;
    if (apTranTotal == null)
    {
      nullable1 = new Decimal?();
      nullable2 = nullable1;
    }
    else
      nullable2 = apTranTotal.SignedCuryTranAmt;
    nullable1 = nullable2;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal? nullable3;
    if (apTranTotal == null)
    {
      nullable1 = new Decimal?();
      nullable3 = nullable1;
    }
    else
      nullable3 = apTranTotal.SignedCuryRetainageAmt;
    nullable1 = nullable3;
    Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
    Decimal num = valueOrDefault2 + valueOrDefault3;
    Decimal? nullable4;
    if (apTranTotal == null)
    {
      nullable1 = new Decimal?();
      nullable4 = nullable1;
    }
    else
      nullable4 = apTranTotal.POPPVAmt;
    nullable1 = nullable4;
    Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
    return (valueOrDefault1, num, valueOrDefault4);
  }

  protected virtual string GetAPDocStatusText(APTranSigned apTranTotal)
  {
    int fieldPrecision1 = this.GetFieldPrecision<APTranSigned, APTranSigned.signedCuryTranAmt>(apTranTotal);
    int fieldPrecision2 = this.GetFieldPrecision<APTranSigned, APTranSigned.pOPPVAmt>(apTranTotal);
    (Decimal TotalBilledQty, Decimal TotalBilledAmt, Decimal TotalPPVAmt) apDocStatusValues = this.GetAPDocStatusValues(apTranTotal);
    return PXAccess.FeatureInstalled<FeaturesSet.inventory>() ? PXMessages.LocalizeFormatNoPrefix("Total Billed Qty. {0}, Total Billed Amt. {1}, Total PPV Amt. {2}", new object[3]
    {
      (object) this.FormatQty(new Decimal?(apDocStatusValues.TotalBilledQty)),
      (object) this.FormatAmt(new Decimal?(apDocStatusValues.TotalBilledAmt), fieldPrecision1),
      (object) this.FormatAmt(new Decimal?(apDocStatusValues.TotalPPVAmt), fieldPrecision2)
    }) : PXMessages.LocalizeFormatNoPrefix("Total Billed Qty. {0}, Total Billed Amt. {1}", new object[2]
    {
      (object) this.FormatQty(new Decimal?(apDocStatusValues.TotalBilledQty)),
      (object) this.FormatAmt(new Decimal?(apDocStatusValues.TotalBilledAmt), fieldPrecision1)
    });
  }

  public virtual string FormatQty(Decimal? value)
  {
    return value.HasValue ? value.Value.ToString("N" + CommonSetupDecPl.Qty.ToString(), (IFormatProvider) NumberFormatInfo.CurrentInfo) : string.Empty;
  }

  public virtual string FormatAmt(Decimal? value, int precision)
  {
    return value.HasValue ? value.Value.ToString("N" + precision.ToString(), (IFormatProvider) NumberFormatInfo.CurrentInfo) : string.Empty;
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<POOrderPOReceipt.statusText> e)
  {
    if (((PXSelectBase<POOrder>) this.Document).Current == null || e.Row == null)
      return;
    POReceiptLineSigned receiptLineSigned = PXResultset<POReceiptLineSigned>.op_Implicit(PXSelectBase<POReceiptLineSigned, PXViewOf<POReceiptLineSigned>.BasedOn<SelectFromBase<POReceiptLineSigned, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<POReceipt>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceipt.receiptType, Equal<POReceiptLineSigned.receiptType>>>>, And<BqlOperand<POReceipt.receiptNbr, IBqlString>.IsEqual<POReceiptLineSigned.receiptNbr>>>, And<BqlOperand<POReceipt.isUnderCorrection, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<POReceipt.canceled, IBqlBool>.IsEqual<False>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLineSigned.pOType, Equal<BqlField<POOrder.orderType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<POReceiptLineSigned.pONbr, IBqlString>.IsEqual<BqlField<POOrder.orderNbr, IBqlString>.FromCurrent>>>.Aggregate<To<Sum<POReceiptLineSigned.signedBaseReceiptQty>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) new POOrder[1]
    {
      ((PXSelectBase<POOrder>) this.Document).Current
    }, Array.Empty<object>()));
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<POOrderPOReceipt.statusText>>) e).ReturnValue = (object) PXMessages.LocalizeFormatNoPrefix("Total Received Qty. {0}", new object[1]
    {
      (object) this.FormatQty(new Decimal?(((Decimal?) receiptLineSigned?.SignedBaseReceiptQty).GetValueOrDefault()))
    });
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<POBlanketOrderPOOrder.statusText> e)
  {
    if (((PXSelectBase<POOrder>) this.Document).Current == null || e.Row == null)
      return;
    POLine poLine = ((PXSelectBase<POLine>) new PXSelectGroupBy<POLine, Where<POLine.pOType, Equal<Current<POOrder.orderType>>, And<POLine.pONbr, Equal<Current<POOrder.orderNbr>>>>, Aggregate<Sum<POLine.baseOrderQty>>>((PXGraph) this)).SelectSingle(Array.Empty<object>());
    POReceiptLineSigned receiptLineSigned = ((PXSelectBase<POReceiptLineSigned>) new PXSelectJoinGroupBy<POReceiptLineSigned, InnerJoin<POLine, On<POReceiptLineSigned.pOType, Equal<POLine.orderType>, And<POReceiptLineSigned.pONbr, Equal<POLine.orderNbr>, And<POReceiptLineSigned.pOLineNbr, Equal<POLine.lineNbr>>>>>, Where<POLine.pOType, Equal<Current<POOrder.orderType>>, And<POLine.pONbr, Equal<Current<POOrder.orderNbr>>>>, Aggregate<Sum<POReceiptLineSigned.signedBaseReceiptQty>>>((PXGraph) this)).SelectSingle(Array.Empty<object>());
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<POBlanketOrderPOOrder.statusText>>) e).ReturnValue = (object) PXMessages.LocalizeFormatNoPrefix("Total Ordered Qty. {0}, Total Received Qty. {1}", new object[2]
    {
      (object) this.FormatQty(new Decimal?(((Decimal?) poLine?.BaseOrderQty).GetValueOrDefault())),
      (object) this.FormatQty(new Decimal?(((Decimal?) receiptLineSigned?.SignedBaseReceiptQty).GetValueOrDefault()))
    });
  }

  protected virtual IEnumerable defaultCompanyContact()
  {
    return (IEnumerable) OrganizationMaint.GetDefaultContactForCurrentOrganization((PXGraph) this);
  }

  [PXOptimizationBehavior(IgnoreBqlDelegate = true)]
  protected virtual IEnumerable transactions()
  {
    this.PrefetchWithDetails();
    return (IEnumerable) null;
  }

  public virtual void PrefetchWithDetails()
  {
  }

  protected virtual bool IsMigrationModeAllowed => false;

  public POOrderEntry()
  {
    this.apsetup.DisableMigrationCheck = this.IsMigrationModeAllowed;
    PX.Objects.PO.POSetup current1 = ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current;
    APSetup current2 = ((PXSelectBase<APSetup>) this.apsetup).Current;
    if (PXAccess.FeatureInstalled<FeaturesSet.inventory>())
    {
      PX.Objects.IN.INSetup current3 = ((PXSelectBase<PX.Objects.IN.INSetup>) this.INSetup).Current;
    }
    PXGraph.RowUpdatedEvents rowUpdated = ((PXGraph) this).RowUpdated;
    POOrderEntry poOrderEntry = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) poOrderEntry, __vmethodptr(poOrderEntry, ParentFieldUpdated));
    rowUpdated.AddHandler<POOrder>(pxRowUpdated);
    POOrderEntry.POOrderFilter current4 = ((PXSelectBase<POOrderEntry.POOrderFilter>) this.filter).Current;
    ((PXSelectBase) this.poLiner).Cache.AllowInsert = false;
    ((PXSelectBase) this.poLiner).Cache.AllowDelete = false;
    ((PXSelectBase) this.poLinesSelection).Cache.AllowInsert = false;
    ((PXSelectBase) this.poLinesSelection).Cache.AllowDelete = false;
    ((PXSelectBase) this.poLinesSelection).Cache.AllowUpdate = true;
    ((PXSelectBase) this.openOrders).Cache.AllowInsert = false;
    ((PXSelectBase) this.openOrders).Cache.AllowDelete = false;
    ((PXSelectBase) this.openOrders).Cache.AllowUpdate = true;
    ((PXSelectBase) this.FixedDemand).AllowDelete = false;
    ((PXSelectBase) this.FixedDemand).AllowInsert = false;
    this.viewInventoryID = ((PXFieldState) ((PXSelectBase) this.Transactions).Cache.GetStateExt<POLine.inventoryID>((object) null))?.ViewName;
    bool flag = ProjectAttribute.IsPMVisible("PO");
    PXUIFieldAttribute.SetVisible<POLine.projectID>(((PXSelectBase) this.Transactions).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<POLine.taskID>(((PXSelectBase) this.Transactions).Cache, (object) null, flag);
    TaxBaseAttribute.SetTaxCalc<POLine.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, TaxCalc.ManualLineCalc);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(POOrderEntry.\u003C\u003Ec.\u003C\u003E9__67_0 ?? (POOrderEntry.\u003C\u003Ec.\u003C\u003E9__67_0 = new PXFieldDefaulting((object) POOrderEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__67_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<PX.Objects.IN.InventoryItem.stkItem>(POOrderEntry.\u003C\u003Ec.\u003C\u003E9__67_1 ?? (POOrderEntry.\u003C\u003Ec.\u003C\u003E9__67_1 = new PXFieldDefaulting((object) POOrderEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__67_1))));
  }

  [InjectDependency]
  protected Func<string, ReportNotificationGenerator> ReportNotificationGeneratorFactory { get; private set; }

  [InjectDependency]
  protected ILicenseLimitsService _licenseLimits { get; set; }

  void IGraphWithInitialization.Initialize()
  {
    if (this._licenseLimits == null)
      return;
    ((PXGraph) this).OnBeforeCommit += this._licenseLimits.GetCheckerDelegate<POOrder>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (POLine), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[2]
      {
        (PXDataFieldValue) new PXDataFieldValue<POLine.orderType>((PXDbType) 3, (object) ((PXSelectBase<POOrder>) ((POOrderEntry) graph).Document).Current?.OrderType),
        (PXDataFieldValue) new PXDataFieldValue<POLine.orderNbr>((object) ((PXSelectBase<POOrder>) ((POOrderEntry) graph).Document).Current?.OrderNbr)
      }))
    });
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Hold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold")]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter)
  {
    return (IEnumerable) adapter.Get<POOrder>();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Remove Hold")]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter)
  {
    this.ValidateBeforeOpen();
    return (IEnumerable) adapter.Get<POOrder>();
  }

  protected virtual void ValidateBeforeOpen()
  {
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable CancelOrder(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable ReopenOrder(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable MarkAsDontEmail(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable MarkAsDontPrint(PXAdapter adapter) => adapter.Get();

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    if (viewName.ToLower() == "document" && values != null && (((PXGraph) this).IsImport || ((PXGraph) this).IsExport || ((PXGraph) this).IsMobile || ((PXGraph) this).IsContractBasedAPI))
    {
      ((PXSelectBase) this.Document).Cache.Locate(keys);
      if (values.Contains((object) "Hold") && values[(object) "Hold"] != PXCache.NotSetValue && values[(object) "Hold"] != null)
      {
        bool valueOrDefault = ((PXSelectBase<POOrder>) this.Document).Current.Hold.GetValueOrDefault();
        if (Convert.ToBoolean(values[(object) "Hold"]) != valueOrDefault)
        {
          ((PXAction<POOrder>) ((PXGraph) this).Actions["hold"]).PressImpl(false, false);
          values[(object) "Hold"] = PXCache.NotSetValue;
        }
      }
    }
    return ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Action(
    PXAdapter adapter,
    [PXInt, PXIntList(new int[] {1, 2}, new string[] {"Persist", "Update"})] int? actionID,
    [PXBool] bool refresh,
    [PXString] string actionName)
  {
    List<POOrder> poOrderList = new List<POOrder>();
    if (actionName != null)
    {
      PXAction action = ((PXGraph) this).Actions[actionName];
      if (action != null)
      {
        foreach (PXResult<POOrder> pxResult in action.Press(adapter))
          poOrderList.Add(PXResult<POOrder>.op_Implicit(pxResult));
      }
    }
    else
    {
      foreach (POOrder poOrder in adapter.Get<POOrder>())
        poOrderList.Add(poOrder);
    }
    if (refresh)
    {
      foreach (POOrder poOrder in poOrderList)
        ((PXSelectBase<POOrder>) this.Document).Search<POOrder.orderNbr>((object) poOrder.OrderNbr, new object[1]
        {
          (object) poOrder.OrderType
        });
    }
    if (actionID.HasValue)
    {
      switch (actionID.GetValueOrDefault())
      {
        case 1:
          ((PXAction) this.Save).Press();
          break;
      }
    }
    return (IEnumerable) poOrderList;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Complete Order")]
  protected virtual IEnumerable Complete(PXAdapter adapter)
  {
    POOrderEntry graph = this;
    foreach (object obj in adapter.Get())
    {
      POOrder poOrder = obj is PXResult pxResult1 ? (POOrder) pxResult1[0] : (POOrder) obj;
      ((PXSelectBase<POOrder>) graph.Document).Current = poOrder;
      if (PXResultset<POReceipt>.op_Implicit(PXSelectBase<POReceipt, PXSelectJoin<POReceipt, InnerJoin<POOrderReceipt, On<POOrderReceipt.FK.Receipt>>, Where<POOrderReceipt.pOType, Equal<Required<POOrder.orderType>>, And<POOrderReceipt.pONbr, Equal<Required<POOrder.orderNbr>>, And<POReceipt.released, Equal<False>>>>>.Config>.Select((PXGraph) graph, new object[2]
      {
        (object) poOrder.OrderType,
        (object) poOrder.OrderNbr
      })) != null)
        throw new PXException("The {0} purchase order cannot be completed because one or multiple unreleased purchase receipts have been generated for this order. To proceed, delete or release purchase receipts first.", new object[1]
        {
          (object) poOrder.OrderNbr
        });
      foreach (PXResult<POLine> pxResult2 in ((PXSelectBase<POLine>) graph.Transactions).Select(Array.Empty<object>()))
      {
        POLine poLine = PXResult<POLine>.op_Implicit(pxResult2);
        if (!poLine.Completed.GetValueOrDefault())
        {
          POLine copy = (POLine) ((PXSelectBase) graph.Transactions).Cache.CreateCopy((object) poLine);
          copy.Completed = new bool?(true);
          using (new POOrderEntry.SuppressOrderEventsScope((PXGraph) graph))
            ((PXSelectBase<POLine>) graph.Transactions).Update(copy);
        }
      }
      yield return obj;
    }
  }

  [PXUIField(DisplayName = "Notifications", Visible = false)]
  [PXButton(ImageKey = "DataEntryF")]
  protected virtual IEnumerable Notification(PXAdapter adapter, [PXString] string notificationCD)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    POOrderEntry.\u003C\u003Ec__DisplayClass100_0 displayClass1000 = new POOrderEntry.\u003C\u003Ec__DisplayClass100_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1000.notificationCD = notificationCD;
    // ISSUE: reference to a compiler-generated field
    displayClass1000.massProcess = adapter.MassProcess;
    // ISSUE: reference to a compiler-generated field
    displayClass1000.orders = adapter.Get<POOrder>().ToArray<POOrder>();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1000, __methodptr(\u003CNotification\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) displayClass1000.orders;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable EmailPurchaseOrder(PXAdapter adapter, [PXString] string notificationCD = null)
  {
    return this.Notification(adapter, notificationCD ?? "PURCHASE ORDER");
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Report(
    PXAdapter adapter,
    [PXInt, PXIntList(new int[] {1, 2}, new string[] {"Vendor Details", "Activities"})] int? inquiryID,
    [PXString(8, InputMask = "CC.CC.CC.CC")] string reportID,
    [PXBool] bool sendByEmail,
    [PXBool] bool refresh)
  {
    List<POOrder> list = adapter.Get<POOrder>().ToList<POOrder>();
    if (!string.IsNullOrEmpty(reportID))
    {
      int num = 0;
      string str1 = (string) null;
      string str2 = (string) null;
      Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
      Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
      PXReportRequiredException requiredException = (PXReportRequiredException) null;
      Dictionary<PrintSettings, PXReportRequiredException> reportsToPrint = new Dictionary<PrintSettings, PXReportRequiredException>();
      foreach (POOrder poOrder in list)
      {
        Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
        dictionary3["POOrder.OrderType"] = poOrder.OrderType;
        dictionary3["POOrder.OrderNbr"] = poOrder.OrderNbr;
        str1 = new NotificationUtility((PXGraph) this).SearchVendorReport(reportID, poOrder.VendorID, poOrder.BranchID);
        requiredException = PXReportRequiredException.CombineReport(requiredException, str1, dictionary3, OrganizationLocalizationHelper.GetCurrentLocalization((PXGraph) this));
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
        reportsToPrint = SMPrintJobMaint.AssignPrintJobToPrinter(reportsToPrint, dictionary3, adapter, new Func<string, string, int?, Guid?>(new NotificationUtility((PXGraph) this).SearchPrinter), "Vendor", reportID, str1, poOrder.BranchID, OrganizationLocalizationHelper.GetCurrentLocalization((PXGraph) this));
        PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) poOrder.VendorID,
          (object) poOrder.VendorLocationID
        }));
        dictionary2["POOrder.OrderType" + num.ToString()] = poOrder.OrderType;
        dictionary2["POOrder.OrderNbr" + num.ToString()] = poOrder.OrderNbr;
        ++num;
        if (str2 == null)
          str2 = new NotificationUtility((PXGraph) this).SearchVendorReport(reportID, poOrder.VendorID, poOrder.BranchID);
        if (refresh)
        {
          ((PXSelectBase<POOrder>) this.Document).Search<POOrder.orderNbr>((object) poOrder.OrderNbr, new object[1]
          {
            (object) poOrder.OrderType
          });
          ((SelectedEntityEvent<POOrder>) PXEntityEventBase<POOrder>.Container<POOrder.Events>.Select((Expression<Func<POOrder.Events, PXEntityEvent<POOrder.Events>>>) (ev => ev.Printed))).FireOn((PXGraph) this, ((PXSelectBase<POOrder>) this.Document).Current);
        }
      }
      if (requiredException != null)
      {
        if (sendByEmail)
        {
          try
          {
            ReportNotificationGenerator notificationGenerator = this.ReportNotificationGeneratorFactory(str1);
            notificationGenerator.Parameters = (IDictionary<string, string>) dictionary2;
            if (!notificationGenerator.Send().Any<CRSMEmail>())
              throw new PXException("The mail send has failed.");
            ((PXGraph) this).SelectTimeStamp();
            ((PXAction) this.Save).Press();
          }
          finally
          {
            ((PXGraph) this).Clear();
          }
        }
        else
        {
          ((PXAction) this.Save).Press();
          ((PXGraph) this).LongOperationManager.StartAsyncOperation((Func<CancellationToken, System.Threading.Tasks.Task>) (ct => (System.Threading.Tasks.Task) SMPrintJobMaint.CreatePrintJobGroups(reportsToPrint, ct)));
          throw requiredException;
        }
      }
    }
    else if (inquiryID.HasValue && inquiryID.HasValue && inquiryID.GetValueOrDefault() == 1 && ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current != null)
    {
      APDocumentEnq instance = PXGraph.CreateInstance<APDocumentEnq>();
      ((PXSelectBase<APDocumentEnq.APDocumentFilter>) instance.Filter).Current.VendorID = ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current.BAccountID;
      ((PXSelectBase<APDocumentEnq.APDocumentFilter>) instance.Filter).Select(Array.Empty<object>());
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, "Vendor Details");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
      throw requiredException;
    }
    return (IEnumerable) list;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable VendorDetails(PXAdapter adapter)
  {
    return this.Report(adapter.Apply<PXAdapter>((System.Action<PXAdapter>) (a => a.Menu = "Vendor Details")), new int?(1), (string) null, false, false);
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable PrintPurchaseOrder(PXAdapter adapter)
  {
    return this.Report(adapter.Apply<PXAdapter>((System.Action<PXAdapter>) (a => a.Menu = "Print Purchase Order")), new int?(), "PO641000", false, true);
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable ViewPurchaseOrderReceipt(PXAdapter adapter)
  {
    return this.Report(adapter.Apply<PXAdapter>((System.Action<PXAdapter>) (a => a.Menu = "Purchase Order Receipt and Billing History")), new int?(), "PO643000", false, false);
  }

  [PXUIField]
  [PXLookupButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable AddPOOrder(PXAdapter adapter)
  {
    if (((PXSelectBase<POOrder>) this.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<POOrder>) this.Document).Current.Hold;
      if (nullable.GetValueOrDefault() && POOrderType.IsUseBlanket(((PXSelectBase<POOrder>) this.Document).Current.OrderType))
      {
        if (((PXSelectBase<POOrderEntry.POOrderS>) this.openOrders).AskExt() == 1)
        {
          foreach (POOrderEntry.POOrderS aOrder in ((PXSelectBase) this.openOrders).Cache.Updated)
          {
            nullable = aOrder.Selected;
            if (nullable.Value)
              this.AddPurchaseOrder(aOrder);
            aOrder.Selected = new bool?(false);
          }
        }
        else
        {
          foreach (POOrderEntry.POOrderS poOrderS in ((PXSelectBase) this.openOrders).Cache.Updated)
            poOrderS.Selected = new bool?(false);
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable AddPOOrderLine(PXAdapter adapter)
  {
    if (((PXSelectBase<POOrder>) this.Document).Current != null)
    {
      bool? nullable = ((PXSelectBase<POOrder>) this.Document).Current.Hold;
      if (nullable.GetValueOrDefault() && POOrderType.IsUseBlanket(((PXSelectBase<POOrder>) this.Document).Current.OrderType))
      {
        bool flag = false;
        if (((PXSelectBase<POOrderEntry.POLineS>) this.poLinesSelection).AskExt() == 1)
        {
          foreach (POOrderEntry.POLineS aLine in ((PXSelectBase) this.poLinesSelection).Cache.Updated)
          {
            nullable = aLine.Selected;
            if (nullable.Value)
            {
              this.AddPOLine(aLine, ((PXSelectBase<POOrderEntry.POOrderFilter>) this.filter).Current.OrderType == "BL");
              int num1 = flag ? 1 : 0;
              Decimal? retainagePct = aLine.RetainagePct;
              Decimal num2 = 0M;
              int num3 = !(retainagePct.GetValueOrDefault() == num2 & retainagePct.HasValue) ? 1 : 0;
              flag = (num1 | num3) != 0;
            }
            aLine.Selected = new bool?(false);
          }
        }
        else
        {
          foreach (POOrderEntry.POLineS poLineS in ((PXSelectBase) this.poLinesSelection).Cache.Updated)
            poLineS.Selected = new bool?(false);
        }
        if (flag && this.EnableRetainage())
          ((PXSelectBase<POOrder>) this.Document).Update(((PXSelectBase<POOrder>) this.Document).Current);
        ((PXSelectBase) this.filter).Cache.Clear();
        ((PXSelectBase) this.poLinesSelection).Cache.Clear();
        ((PXSelectBase) this.poLinesSelection).Cache.ClearQueryCache();
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable CreatePOReceipt(PXAdapter adapter)
  {
    if (((PXSelectBase<POOrder>) this.Document).Current != null && EnumerableExtensions.IsIn<string>(((PXSelectBase<POOrder>) this.Document).Current.OrderType, "RO", "DP", "PD"))
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      POOrderEntry.\u003C\u003Ec__DisplayClass116_0 displayClass1160 = new POOrderEntry.\u003C\u003Ec__DisplayClass116_0();
      // ISSUE: reference to a compiler-generated field
      displayClass1160.order = ((PXSelectBase<POOrder>) this.Document).Current;
      // ISSUE: reference to a compiler-generated field
      if (displayClass1160.order.Status == "N")
      {
        this.ValidateLines();
        bool flag = false;
        foreach (PXResult<POLine> pxResult in ((PXSelectBase<POLine>) this.Transactions).Select(Array.Empty<object>()))
        {
          if (this.NeedsPOReceipt(PXResult<POLine>.op_Implicit(pxResult), ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current))
          {
            flag = true;
            break;
          }
        }
        if (flag)
        {
          ((PXAction) this.Save).Press();
          // ISSUE: method pointer
          PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1160, __methodptr(\u003CCreatePOReceipt\u003Eb__0)));
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          throw new PXException("The purchase order {0} does not contain any items to be received.", new object[1]
          {
            (object) displayClass1160.order.OrderNbr
          });
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable CreateAPInvoice(PXAdapter adapter)
  {
    if (((PXSelectBase<POOrder>) this.Document).Current != null && (POOrderType.IsNormalType(((PXSelectBase<POOrder>) this.Document).Current.OrderType) || EnumerableExtensions.IsIn<string>(((PXSelectBase<POOrder>) this.Document).Current.OrderType, "DP", "PD")))
    {
      POOrder current = ((PXSelectBase<POOrder>) this.Document).Current;
      if (this.HasReceiptUnderCorrection(current))
        throw new PXException("The bill cannot be created because at least one linked purchase receipt is under correction.");
      if (EnumerableExtensions.IsIn<string>(current.Status, "A", "N", "M"))
      {
        this.ValidateLines();
        if (this.NeedsAPInvoice())
        {
          ((PXAction) this.Save).Press();
          APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
          bool? nullable = ((PXSelectBase<APSetup>) this.apsetup).Current.RetainTaxes;
          int num;
          if (nullable.GetValueOrDefault())
          {
            nullable = current.RetainageApply;
            num = nullable.GetValueOrDefault() ? 1 : 0;
          }
          else
            num = 0;
          bool flag = num != 0;
          instance.InvoicePOOrder(current, true, !flag);
          instance.AttachPrepayment();
          throw new PXRedirectRequiredException((PXGraph) instance, "Switch to PO Receipt");
        }
        throw new PXException("There are no lines in this document that may be entered in AP Bill Document directly");
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
  {
    POOrderEntry poOrderEntry = this;
    foreach (POOrder poOrder in adapter.Get<POOrder>())
    {
      if (poOrder != null)
        ((PXGraph) poOrderEntry).FindAllImplementations<IAddressValidationHelper>().ValidateAddresses();
      yield return (object) poOrder;
    }
  }

  protected virtual void AddPurchaseOrder(POOrderEntry.POOrderS aOrder)
  {
    PXSelect<POOrderEntry.POLineS, Where<POOrderEntry.POLineS.orderType, Equal<Required<POLine.orderType>>, And<POOrderEntry.POLineS.orderNbr, Equal<Required<POLine.orderNbr>>, And<POOrderEntry.POLineS.cancelled, NotEqual<boolTrue>, And<POOrderEntry.POLineS.completed, NotEqual<boolTrue>, And<Where<POOrderEntry.POLineS.orderQty, Equal<decimal0>, Or<POOrderEntry.POLineS.openQty, Greater<decimal0>>>>>>>>> pxSelect = new PXSelect<POOrderEntry.POLineS, Where<POOrderEntry.POLineS.orderType, Equal<Required<POLine.orderType>>, And<POOrderEntry.POLineS.orderNbr, Equal<Required<POLine.orderNbr>>, And<POOrderEntry.POLineS.cancelled, NotEqual<boolTrue>, And<POOrderEntry.POLineS.completed, NotEqual<boolTrue>, And<Where<POOrderEntry.POLineS.orderQty, Equal<decimal0>, Or<POOrderEntry.POLineS.openQty, Greater<decimal0>>>>>>>>>((PXGraph) this);
    bool flag1 = false;
    object[] objArray = new object[2]
    {
      (object) aOrder.OrderType,
      (object) aOrder.OrderNbr
    };
    foreach (PXResult<POOrderEntry.POLineS> pxResult in ((PXSelectBase<POOrderEntry.POLineS>) pxSelect).Select(objArray))
    {
      POOrderEntry.POLineS aLine = PXResult<POOrderEntry.POLineS>.op_Implicit(pxResult);
      this.AddPOLine(aLine, aOrder.OrderType == "BL");
      int num1 = flag1 ? 1 : 0;
      Decimal? retainagePct = aLine.RetainagePct;
      Decimal num2 = 0M;
      int num3 = !(retainagePct.GetValueOrDefault() == num2 & retainagePct.HasValue) ? 1 : 0;
      flag1 = (num1 | num3) != 0;
    }
    bool flag2 = false;
    POOrder current = ((PXSelectBase<POOrder>) this.Document).Current;
    if (string.IsNullOrEmpty(current.VendorRefNbr))
    {
      current.VendorRefNbr = aOrder.VendorRefNbr;
      flag2 = true;
    }
    if (flag1)
      flag2 |= this.EnableRetainage();
    if (!flag2)
      return;
    ((PXSelectBase<POOrder>) this.Document).Update(current);
  }

  protected virtual bool EnableRetainage()
  {
    if (((PXSelectBase<POOrder>) this.Document).Current.RetainageApply.GetValueOrDefault() || POOrderType.IsNormalType(((PXSelectBase<POOrder>) this.Document).Current.OrderType))
      return false;
    ((PXSelectBase<POOrder>) this.Document).Current.RetainageApply = new bool?(true);
    ((PXSelectBase) this.Document).Cache.SetDefaultExt<POOrder.defRetainagePct>((object) ((PXSelectBase<POOrder>) this.Document).Current);
    ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<POOrder.retainageApply>((object) ((PXSelectBase<POOrder>) this.Document).Current, (object) true, (Exception) new PXSetPropertyException("The Apply Retainage check box is selected automatically because you have added one or more lines with a retainage from the purchase order.", (PXErrorLevel) 2));
    return true;
  }

  protected virtual void AddPOLine(POOrderEntry.POLineS aLine, bool blanked)
  {
    POLine poLine1 = (POLine) null;
    if (blanked)
    {
      foreach (PXResult<POLine> pxResult in ((PXSelectBase<POLine>) this.Transactions).Select(Array.Empty<object>()))
      {
        POLine poLine2 = PXResult<POLine>.op_Implicit(pxResult);
        if (poLine2.POType == aLine.OrderType & poLine2.PONbr == aLine.OrderNbr)
        {
          int? poLineNbr = poLine2.POLineNbr;
          int? lineNbr = aLine.LineNbr;
          if (poLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & poLineNbr.HasValue == lineNbr.HasValue)
          {
            poLine1 = poLine2;
            break;
          }
        }
      }
    }
    if (poLine1 != null)
      return;
    POLine poLine3 = new POLine();
    poLine3.BranchID = aLine.BranchID;
    poLine3.InventoryID = aLine.InventoryID;
    poLine3.SubItemID = aLine.SubItemID;
    poLine3.SiteID = aLine.SiteID;
    poLine3.TaxCategoryID = aLine.TaxCategoryID;
    poLine3.TranDesc = aLine.TranDesc;
    poLine3.UnitCost = aLine.UnitCost;
    poLine3.UnitVolume = aLine.UnitVolume;
    poLine3.UnitWeight = aLine.UnitWeight;
    poLine3.UOM = aLine.UOM;
    poLine3.AlternateID = aLine.AlternateID;
    poLine3.CuryUnitCost = aLine.CuryUnitCost;
    poLine3.ManualPrice = aLine.ManualPrice;
    poLine3.ExpenseAcctID = aLine.ExpenseAcctID;
    poLine3.ExpenseSubID = aLine.ExpenseSubID;
    poLine3.RcptQtyMin = aLine.RcptQtyMin;
    poLine3.RcptQtyMax = aLine.RcptQtyMax;
    poLine3.RcptQtyThreshold = aLine.RcptQtyThreshold;
    poLine3.RcptQtyAction = aLine.RcptQtyAction;
    poLine3.POType = aLine.OrderType;
    poLine3.PONbr = aLine.OrderNbr;
    poLine3.POLineNbr = aLine.LineNbr;
    poLine3.ProjectID = aLine.ProjectID;
    poLine3.TaskID = aLine.TaskID;
    poLine3.CostCodeID = aLine.CostCodeID;
    poLine3.OrderQty = !blanked ? aLine.OrderQty : aLine.OpenQty;
    Decimal? nullable;
    if (!(aLine.LineType == "FT"))
    {
      nullable = aLine.OrderQty;
      Decimal num = 0M;
      if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
        goto label_15;
    }
    poLine3.CuryLineAmt = aLine.CuryLineAmt;
label_15:
    poLine3.OpenQty = poLine3.OrderQty;
    poLine3.RetainagePct = aLine.RetainagePct;
    nullable = aLine.OpenQty;
    Decimal? orderQty = aLine.OrderQty;
    bool flag = !(nullable.GetValueOrDefault() == orderQty.GetValueOrDefault() & nullable.HasValue == orderQty.HasValue);
    if (blanked && !flag)
    {
      poLine3.CuryRetainageAmt = aLine.CuryRetainageAmt;
      poLine3.RetainageAmt = aLine.RetainageAmt;
    }
    POLine poLine4 = ((PXSelectBase<POLine>) this.Transactions).Insert(poLine3);
    if (!(poLine4.LineType != aLine.LineType) || !POLineType.IsDefault(poLine4.LineType))
      return;
    POLine copy = PXCache<POLine>.CreateCopy(poLine4);
    poLine4.LineType = aLine.LineType;
    ((PXSelectBase) this.Transactions).Cache.RaiseRowUpdated((object) poLine4, (object) copy);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable RecalculateDiscountsAction(PXAdapter adapter)
  {
    if (adapter.MassProcess)
    {
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CRecalculateDiscountsAction\u003Eb__125_0)));
    }
    else if (!adapter.ExternalCall || ((PXGraph) this).IsImport)
      this.RecalculateDiscountsProc(true);
    else if (((PXSelectBase<RecalcDiscountsParamFilter>) this.recalcdiscountsfilter).AskExt() == 1)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) new POOrderEntry.\u003C\u003Ec__DisplayClass125_0()
      {
        clone = GraphHelper.Clone<POOrderEntry>(this)
      }, __methodptr(\u003CRecalculateDiscountsAction\u003Eb__1)));
    }
    return adapter.Get();
  }

  protected virtual void RecalculateDiscountsProc(bool redirect)
  {
    this._discountEngine.RecalculatePricesAndDiscounts(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<POLine>) this.Transactions, ((PXSelectBase<POLine>) this.Transactions).Current, (PXSelectBase<POOrderDiscountDetail>) this.DiscountDetails, ((PXSelectBase<POOrder>) this.Document).Current.VendorLocationID, ((PXSelectBase<POOrder>) this.Document).Current.OrderDate, ((PXSelectBase<RecalcDiscountsParamFilter>) this.recalcdiscountsfilter).Current, DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation | DiscountEngine.DiscountCalculationOptions.DisableARDiscountsCalculation);
    if (redirect)
      PXLongOperation.SetCustomInfo((object) this);
    else
      ((PXAction) this.Save).Press();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable RecalcOk(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewBlanketOrder(PXAdapter adapter)
  {
    PXRedirectHelper.TryRedirect(((PXSelectBase) this.Document).Cache, (object) KeysRelation<CompositeKey<Field<POLine.pOType>.IsRelatedTo<POOrder.orderType>, Field<POLine.pONbr>.IsRelatedTo<POOrder.orderNbr>>.WithTablesOf<POOrder, POLine>, POOrder, POLine>.FindParent((PXGraph) this, ((PXSelectBase<POLine>) this.Transactions).Current, (PKFindOptions) 0), "View Parent Order", (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton(VisibleOnDataSource = false)]
  public virtual IEnumerable ViewDemand(PXAdapter adapter)
  {
    ((PXSelectBase<POOrderEntry.SOLineSplit3>) this.FixedDemand).AskExt();
    return adapter.Get();
  }

  protected virtual void ParentFieldUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.ObjectsEqual<POOrder.orderDate, POOrder.curyID>(e.Row, e.OldRow) || !(e.Row is POOrder row) || row.LinesStatusUpdated.GetValueOrDefault())
      return;
    foreach (PXResult<POLine> pxResult in ((PXSelectBase<POLine>) this.Transactions).Select(Array.Empty<object>()))
      GraphHelper.MarkUpdated(((PXSelectBase) this.Transactions).Cache, (object) PXResult<POLine>.op_Implicit(pxResult), true);
    row.LinesStatusUpdated = new bool?(true);
  }

  private object GetAcctSub<Field>(PXCache cache, object data) where Field : IBqlField
  {
    object valueExt = cache.GetValueExt<Field>(data);
    return valueExt is PXFieldState ? ((PXFieldState) valueExt).Value : valueExt;
  }

  public static bool NeedsPOReceipt(POLine aLine, bool skipCompleted, PX.Objects.PO.POSetup poSetup)
  {
    if (skipCompleted && (aLine.Completed.GetValueOrDefault() || aLine.Cancelled.GetValueOrDefault()) || aLine.OrderType == "PD" && aLine.DropshipReceiptProcessing == "S")
      return false;
    return EnumerableExtensions.IsIn<string>(aLine.LineType, "GP", "NP", "NO", "NF", "NM", new string[9]
    {
      "GI",
      "GS",
      "GF",
      "GM",
      "GR",
      "NS",
      "FT",
      "PG",
      "PN"
    }) || aLine.LineType == "SV" && (aLine.POAccrualType == "R" || poSetup != null && (aLine.OrderType == "DP" && poSetup.AddServicesFromDSPOtoPR.GetValueOrDefault() || aLine.OrderType == "RO" && (poSetup.AddServicesFromNormalPOtoPR.GetValueOrDefault() || aLine.ProcessNonStockAsServiceViaPR.GetValueOrDefault())));
  }

  public virtual bool NeedsPOReceipt(POLine aLine, PX.Objects.PO.POSetup poSetup)
  {
    return POOrderEntry.NeedsPOReceipt(aLine, true, poSetup);
  }

  public virtual bool NeedsAPInvoice()
  {
    foreach (POLine aLine in ((PXSelectBase) this.Transactions).Cache.Inserted)
    {
      if (this.NeedsAPInvoice(aLine, true, ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current))
        return true;
    }
    foreach (PXResult<POLine> pxResult in PXSelectBase<POLine, PXSelectReadonly2<POLine, LeftJoin<PX.Objects.AP.APTran, On<PX.Objects.AP.APTran.pOAccrualRefNoteID, Equal<POLine.orderNoteID>, And<PX.Objects.AP.APTran.pOAccrualLineNbr, Equal<POLine.lineNbr>, And<PX.Objects.AP.APTran.released, Equal<False>>>>>, Where<POLine.orderType, Equal<Current<POOrder.orderType>>, And<POLine.orderNbr, Equal<Current<POOrder.orderNbr>>, And<POLine.pOAccrualType, Equal<POAccrualType.order>, And<PX.Objects.AP.APTran.refNbr, IsNull>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      POLine aLine = PXResult<POLine>.op_Implicit(pxResult);
      if (this.NeedsAPInvoice(aLine, true, ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current) && ((PXSelectBase) this.Transactions).Cache.GetStatus((object) aLine) != 3)
        return true;
    }
    return false;
  }

  public virtual bool NeedsAPInvoice(POLine aLine, bool skipCompleted, PX.Objects.PO.POSetup poSetup)
  {
    if (skipCompleted)
    {
      if (aLine.Closed.GetValueOrDefault() || aLine.Cancelled.GetValueOrDefault())
        return false;
      Decimal? completedQty = aLine.CompletedQty;
      Decimal? nullable1 = aLine.BilledQty;
      if (completedQty.GetValueOrDefault() <= nullable1.GetValueOrDefault() & completedQty.HasValue & nullable1.HasValue)
      {
        int num1;
        if (!(aLine.CompletePOLine == "Q"))
        {
          Decimal? curyExtCost = aLine.CuryExtCost;
          Decimal? curyRetainageAmt = aLine.CuryRetainageAmt;
          Decimal? nullable2 = curyExtCost.HasValue & curyRetainageAmt.HasValue ? new Decimal?(curyExtCost.GetValueOrDefault() + curyRetainageAmt.GetValueOrDefault()) : new Decimal?();
          Decimal? rcptQtyThreshold = aLine.RcptQtyThreshold;
          nullable1 = nullable2.HasValue & rcptQtyThreshold.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * rcptQtyThreshold.GetValueOrDefault() / 100M) : new Decimal?();
          Decimal? curyBilledAmt = aLine.CuryBilledAmt;
          num1 = nullable1.GetValueOrDefault() <= curyBilledAmt.GetValueOrDefault() & nullable1.HasValue & curyBilledAmt.HasValue ? 1 : 0;
        }
        else
        {
          Decimal? orderQty1 = aLine.OrderQty;
          Decimal num2 = 0M;
          if (orderQty1.GetValueOrDefault() > num2 & orderQty1.HasValue)
          {
            Decimal? orderQty2 = aLine.OrderQty;
            Decimal? rcptQtyThreshold = aLine.RcptQtyThreshold;
            Decimal? nullable3 = orderQty2.HasValue & rcptQtyThreshold.HasValue ? new Decimal?(orderQty2.GetValueOrDefault() * rcptQtyThreshold.GetValueOrDefault() / 100M) : new Decimal?();
            nullable1 = aLine.BilledQty;
            num1 = nullable3.GetValueOrDefault() <= nullable1.GetValueOrDefault() & nullable3.HasValue & nullable1.HasValue ? 1 : 0;
          }
          else
            num1 = 0;
        }
        if (num1 != 0)
          return false;
      }
    }
    return aLine.POAccrualType == "O" || aLine.LineType == "MC";
  }

  public virtual void ValidateLines()
  {
    bool flag = false;
    foreach (PXResult<POLine> pxResult in ((PXSelectBase<POLine>) this.Transactions).Select(Array.Empty<object>()))
    {
      POLine poLine = PXResult<POLine>.op_Implicit(pxResult);
      if (poLine.TaskID.HasValue)
      {
        PMProject pmProject = PXSelectorAttribute.Select<POLine.projectID>(((PXSelectBase) this.Transactions).Cache, (object) poLine) as PMProject;
        if (!pmProject.IsActive.GetValueOrDefault())
        {
          PXUIFieldAttribute.SetError<POLine.projectID>(((PXSelectBase) this.Transactions).Cache, (object) poLine, "Project is not Active.", pmProject.ContractCD);
          flag = true;
        }
        else
        {
          PMTask pmTask = PXSelectorAttribute.Select<POLine.taskID>(((PXSelectBase) this.Transactions).Cache, (object) poLine) as PMTask;
          if (!pmTask.IsActive.GetValueOrDefault())
          {
            PXUIFieldAttribute.SetError<POLine.taskID>(((PXSelectBase) this.Transactions).Cache, (object) poLine, "Project Task is not Active.", pmTask.TaskCD);
            flag = true;
          }
        }
      }
    }
    if (flag)
      throw new PXException("One or more records in the document is invalid.");
  }

  internal void UpdateSOLine(POOrderEntry.SOLineSplit3 split, int? vendorID, bool poCreated)
  {
    int? vendorId1 = split.VendorID;
    int? nullable1 = vendorID;
    bool flag1 = !(vendorId1.GetValueOrDefault() == nullable1.GetValueOrDefault() & vendorId1.HasValue == nullable1.HasValue);
    bool? poCreated1 = split.POCreated;
    bool flag2 = poCreated;
    bool flag3 = !(poCreated1.GetValueOrDefault() == flag2 & poCreated1.HasValue);
    if (!(flag1 | flag3))
      return;
    POOrderEntry.SOLine5 soLine5 = PXResultset<POOrderEntry.SOLine5>.op_Implicit(((PXSelectBase<POOrderEntry.SOLine5>) this.FixedDemandOrigSOLine).Select(new object[3]
    {
      (object) split.OrderType,
      (object) split.OrderNbr,
      (object) split.LineNbr
    }));
    bool flag4 = false;
    if (flag1)
    {
      split.VendorID = vendorID;
      if (soLine5 != null)
      {
        int? vendorId2 = soLine5.VendorID;
        int? nullable2 = vendorID;
        if (!(vendorId2.GetValueOrDefault() == nullable2.GetValueOrDefault() & vendorId2.HasValue == nullable2.HasValue))
        {
          soLine5.VendorID = vendorID;
          flag4 = true;
        }
      }
    }
    if (flag3)
    {
      split.POCreated = new bool?(poCreated);
      if (soLine5 != null)
      {
        bool? poCreated2 = soLine5.POCreated;
        bool flag5 = poCreated;
        if (!(poCreated2.GetValueOrDefault() == flag5 & poCreated2.HasValue))
        {
          soLine5.POCreated = new bool?(poCreated);
          flag4 = true;
        }
      }
    }
    if (!flag4)
      return;
    GraphHelper.MarkUpdated(((PXSelectBase) this.FixedDemandOrigSOLine).Cache, (object) soLine5, true);
  }

  public virtual bool GetRequireControlTotal(string aOrderType)
  {
    bool requireControlTotal = false;
    switch (aOrderType)
    {
      case "BL":
      case "SB":
        requireControlTotal = ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current.RequireBlanketControlTotal.GetValueOrDefault();
        break;
      case "RO":
        requireControlTotal = ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current.RequireOrderControlTotal.GetValueOrDefault();
        break;
      case "DP":
        requireControlTotal = ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current.RequireDropShipControlTotal.GetValueOrDefault();
        break;
      case "PD":
        requireControlTotal = ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current.RequireProjectDropShipControlTotal.GetValueOrDefault();
        break;
    }
    return requireControlTotal;
  }

  private bool IsZeroQuantityValid(POLine row)
  {
    if (POLineType.IsStock(row.LineType))
      return false;
    if (!row.InventoryID.HasValue)
      return true;
    return !(row.CompletePOLine == "Q") && row.POAccrualType != "R";
  }

  private bool IsZeroUnitCostValid(POLine row)
  {
    if (!POLineType.IsStock(row.LineType))
      return true;
    Decimal? curyLineAmt = row.CuryLineAmt;
    Decimal num = 0M;
    return !(curyLineAmt.GetValueOrDefault() == num & curyLineAmt.HasValue);
  }

  protected virtual List<KeyValuePair<string, List<FieldInfo>>> AdjustApiScript(
    List<KeyValuePair<string, List<FieldInfo>>> fieldsByView)
  {
    List<KeyValuePair<string, List<FieldInfo>>> source1 = ((PXGraph) this).AdjustApiScript(fieldsByView);
    List<FieldInfo> source2 = source1.FirstOrDefault<KeyValuePair<string, List<FieldInfo>>>((Func<KeyValuePair<string, List<FieldInfo>>, bool>) (x => x.Key == "Document")).Value;
    if (source2 == null)
      return source1;
    FieldInfo fieldInfo = source2.FirstOrDefault<FieldInfo>((Func<FieldInfo, bool>) (x => x.FieldName == "ExpectedDate"));
    if (fieldInfo == null)
      return source1;
    source2.Remove(fieldInfo);
    source2.Add(fieldInfo);
    return source1;
  }

  public virtual void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers)
  {
    ((PXGraph) this).CopyPasteGetScript(isImportSimple, script, containers);
    int index1 = script.FindIndex((Predicate<Command>) (x => x.FieldName == "ExpectedDate"));
    if (index1 == -1)
      return;
    Command cmd = script[index1];
    Command command = script.LastOrDefault<Command>((Func<Command, bool>) (x => string.Equals(x.ObjectName, cmd.ObjectName)));
    int index2 = script.IndexOf(command);
    Container container = containers[index1];
    script.RemoveAt(index1);
    containers.RemoveAt(index1);
    script.Insert(index2, cmd);
    containers.Insert(index2, container);
  }

  public bool BlockUIUpdate => this._blockUIUpdate;

  [InterBranchRestrictor(typeof (Where2<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<POOrder.branchID>>, Or<Current<POOrder.orderType>, Equal<POOrderType.standardBlanket>>>))]
  [PXMergeAttributes]
  protected virtual void POOrder_SiteID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void POOrder_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is POOrder row))
      return;
    row.RequestApproval = ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current.OrderRequestApproval;
    bool valueOrDefault = row.RetainageApply.GetValueOrDefault();
    bool flag1 = row.OrderType == "PD";
    bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.vendorDiscounts>();
    PXUIFieldAttribute.SetVisible<POOrder.hold>(cache, (object) null, true);
    PXUIFieldAttribute.SetRequired<POOrder.termsID>(cache, true);
    bool flag3 = row.OrderType == "BL";
    PXUIFieldAttribute.SetVisible<POOrder.expirationDate>(cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisible<POOrder.expectedDate>(cache, (object) null, !flag3);
    PXUIFieldAttribute.SetVisible<POOrder.curyID>(cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
    if (flag1)
    {
      ((PXAction) this.createAPInvoice).SetDisplayOnMainToolbar(row.DropshipReceiptProcessing == "S");
      PXStringListAttribute.SetList<POOrder.shipDestType>(((PXSelectBase) this.Document).Cache, (object) null, new string[1]
      {
        "P"
      }, new string[1]{ "Project Site" });
    }
    else
      PXStringListAttribute.SetList<POOrder.shipDestType>(((PXSelectBase) this.Document).Cache, (object) null, new string[4]
      {
        "L",
        "C",
        "V",
        "S"
      }, new string[4]
      {
        "Branch",
        "Customer",
        "Vendor",
        "Warehouse"
      });
    PXUIFieldAttribute.SetEnabled<POOrder.defRetainagePct>(cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<POOrder.curyRetainageTotal>(cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetEnabled<POOrder.curyRetainageTotal>(cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<POLine.retainagePct>(((PXSelectBase) this.Transactions).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<POLine.curyRetainageAmt>(((PXSelectBase) this.Transactions).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<POTaxTran.curyRetainedTaxableAmt>(((PXSelectBase) this.Taxes).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetEnabled<POTaxTran.curyRetainedTaxableAmt>(((PXSelectBase) this.Taxes).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<POTaxTran.curyRetainedTaxAmt>(((PXSelectBase) this.Taxes).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetEnabled<POTaxTran.curyRetainedTaxAmt>(((PXSelectBase) this.Taxes).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<POOrderDiscountDetail.curyRetainedDiscountAmt>(((PXSelectBase) this.DiscountDetails).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetEnabled<POOrderDiscountDetail.curyRetainedDiscountAmt>(((PXSelectBase) this.DiscountDetails).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<POOrder.sOOrderType>(cache, (object) null, !flag1);
    PXUIFieldAttribute.SetVisible<POOrder.sOOrderNbr>(cache, (object) null, !flag1);
    PXUIFieldAttribute.SetVisible<POOrder.rQReqNbr>(cache, (object) row, !flag1);
    PXUIFieldAttribute.SetVisible<POLine.siteID>(((PXSelectBase) this.Transactions).Cache, (object) null, !flag1);
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.poLinesSelection).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.openOrders).Cache, (string) null, false);
    bool flag4 = PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();
    PXUIFieldAttribute.SetVisible<POOrder.projectID>(cache, (object) null, flag4);
    PXDefaultAttribute.SetPersistingCheck<POOrder.projectID>(cache, (object) null, flag4 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    if (!row.Cancelled.GetValueOrDefault() && EnumerableExtensions.IsNotIn<string>(row.Status, "M", "C") && !this._blockUIUpdate)
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) row, true);
      PXUIFieldAttribute.SetEnabled<POOrder.status>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<POOrder.curyOrderTotal>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<POOrder.curyDiscTot>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<POOrder.curyDetailExtCostTotal>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<POOrder.curyLineTotal>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<POOrder.curyTaxTotal>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<POOrder.openOrderQty>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<POOrder.curyUnbilledLineTotal>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<POOrder.curyUnbilledOrderTotal>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<POOrder.unbilledOrderQty>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<POOrder.rQReqNbr>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<POOrder.curyUnbilledTaxTotal>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<POOrder.shipToBAccountID>(cache, (object) row, row.ShipDestType != "L" && this.IsShipToBAccountRequired(row));
      PXUIFieldAttribute.SetEnabled<POOrder.shipToLocationID>(cache, (object) row, this.IsShipToBAccountRequired(row));
      PXUIFieldAttribute.SetEnabled<POOrder.siteID>(cache, (object) row, row.ShipDestType == "S");
      PXUIFieldAttribute.SetEnabled<POOrder.curyVatExemptTotal>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<POOrder.curyVatTaxableTotal>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<POOrder.orderBasedAPBill>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<POOrder.pOAccrualType>(cache, (object) row, false);
      PXUIFieldAttribute.SetRequired<POOrder.siteID>(cache, row.OrderType != "RS");
      PXUIFieldAttribute.SetRequired<POOrder.shipToBAccountID>(cache, this.IsShipToBAccountRequired(row));
      PXUIFieldAttribute.SetRequired<POOrder.shipToLocationID>(cache, this.IsShipToBAccountRequired(row));
      PXUIFieldAttribute.SetEnabled<POOrder.hold>(cache, (object) row, true);
      PXUIFieldAttribute.SetEnabled<POOrder.termsID>(cache, (object) row, true);
      PXUIFieldAttribute.SetEnabled<POOrder.expectedDate>(cache, (object) row, true);
      PXUIFieldAttribute.SetEnabled<POOrder.cancelled>(cache, (object) row, row.Status == "N");
      PXUIFieldAttribute.SetEnabled<POOrder.retainageApply>(cache, (object) row, row.OrderType != "DP");
      PXUIFieldAttribute.SetEnabled<POOrder.projectID>(cache, (object) null, flag4);
      PXUIFieldAttribute.SetEnabled<POOrderEntry.POOrderS.selected>(((PXSelectBase) this.openOrders).Cache, (object) null, true);
      PXUIFieldAttribute.SetEnabled<POOrderEntry.POLineS.selected>(((PXSelectBase) this.poLinesSelection).Cache, (object) null, true);
      PXUIFieldAttribute.SetEnabled<POOrder.curyLineDiscTotal>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<POOrder.curyDiscTot>(cache, (object) row, !flag2);
      PXUIFieldAttribute.SetEnabled<POOrder.shipDestType>(((PXSelectBase) this.Document).Cache, (object) null, !flag1);
      PXUIFieldAttribute.SetEnabled<POOrder.defRetainagePct>(cache, (object) null, valueOrDefault);
    }
    bool? nullable1 = ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current.OrderRequestApproval;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = row.Approved;
      if (nullable1.GetValueOrDefault())
      {
        PXUIFieldAttribute.SetVisible<POOrder.approved>(cache, (object) row, false);
        goto label_15;
      }
    }
    PXUIFieldAttribute.SetVisible<POOrder.approved>(cache, (object) row, true);
    PXCache pxCache1 = cache;
    POOrder poOrder1 = row;
    nullable1 = ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current.OrderRequestApproval;
    int num1;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = row.Approved;
      if (nullable1.GetValueOrDefault())
      {
        num1 = 2;
        goto label_14;
      }
    }
    num1 = 1;
label_14:
    PXDefaultAttribute.SetPersistingCheck<POOrder.ownerID>(pxCache1, (object) poOrder1, (PXPersistingCheck) num1);
label_15:
    PXUIFieldAttribute.SetEnabled<POOrder.orderType>(cache, (object) row);
    PXUIFieldAttribute.SetEnabled<POOrder.orderNbr>(cache, (object) row);
    int? nullable2 = row.VendorID;
    int num2;
    if (nullable2.HasValue)
    {
      nullable2 = row.VendorLocationID;
      num2 = nullable2.HasValue ? 1 : 0;
    }
    else
      num2 = 0;
    bool flag5 = num2 != 0;
    ((PXSelectBase) this.Transactions).Cache.AllowDelete = ((PXSelectBase) this.Transactions).Cache.AllowUpdate = ((PXSelectBase) this.Transactions).Cache.AllowInsert = flag5;
    ((PXAction) this.addPOOrder).SetEnabled(flag5);
    ((PXAction) this.addPOOrderLine).SetEnabled(flag5);
    ((PXSelectBase) this.Taxes).Cache.AllowDelete = ((PXSelectBase) this.DiscountDetails).Cache.AllowDelete = ((PXSelectBase) this.Transactions).Cache.AllowDelete;
    ((PXSelectBase) this.Taxes).Cache.AllowUpdate = ((PXSelectBase) this.DiscountDetails).Cache.AllowUpdate = ((PXSelectBase) this.Transactions).Cache.AllowUpdate;
    ((PXSelectBase) this.Taxes).Cache.AllowInsert = ((PXSelectBase) this.DiscountDetails).Cache.AllowInsert = ((PXSelectBase) this.Transactions).Cache.AllowInsert;
    if (row != null && ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current != null)
    {
      nullable1 = ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current.TaxAgency;
      if (nullable1.Value)
      {
        PXUIFieldAttribute.SetEnabled<POOrder.taxZoneID>(cache, (object) row, false);
        PXUIFieldAttribute.SetEnabled<POLine.taxCategoryID>(((PXSelectBase) this.Transactions).Cache, (object) null, false);
      }
    }
    nullable2 = row.VendorID;
    if (nullable2.HasValue && !this._blockUIUpdate)
    {
      nullable1 = row.HasUsedLine;
      if (!nullable1.HasValue)
      {
        POLine poLine = PXResultset<POLine>.op_Implicit(PXSelectBase<POLine, PXSelect<POLine, Where<POLine.orderType, Equal<Required<POLine.orderType>>, And<POLine.orderNbr, Equal<Required<POLine.orderNbr>>, And<Where<POLine.receivedQty, Greater<decimal0>, Or<POLine.completed, Equal<boolTrue>, Or<POLine.cancelled, Equal<boolTrue>>>>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) row.OrderType,
          (object) row.OrderNbr
        }));
        row.HasUsedLine = new bool?(poLine != null);
      }
      PXCache pxCache2 = cache;
      POOrder poOrder2 = row;
      nullable1 = row.HasUsedLine;
      int num3 = !nullable1.GetValueOrDefault() ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<POOrder.orderDate>(pxCache2, (object) poOrder2, num3 != 0);
    }
    if (!this._blockUIUpdate)
    {
      bool requireControlTotal = this.GetRequireControlTotal(row.OrderType);
      PXUIFieldAttribute.SetVisible<POOrder.curyControlTotal>(cache, e.Row, requireControlTotal);
      PXUIFieldAttribute.SetEnabled<POOrder.curyDiscTot>(cache, (object) row, !flag2);
      PXUIFieldAttribute.SetVisible<POOrder.shipToBAccountID>(cache, (object) row, this.IsShipToBAccountRequired(row));
      PXUIFieldAttribute.SetVisible<POOrder.shipToLocationID>(cache, (object) row, this.IsShipToBAccountRequired(row));
      PXUIFieldAttribute.SetVisible<POOrder.siteID>(cache, (object) row, row.ShipDestType == "S");
      PXAction<POOrder> validateAddresses = this.validateAddresses;
      nullable1 = row.Cancelled;
      bool flag6 = false;
      int num4 = !(nullable1.GetValueOrDefault() == flag6 & nullable1.HasValue) ? 0 : (((PXGraph) this).FindAllImplementations<IAddressValidationHelper>().RequiresValidation() ? 1 : 0);
      ((PXAction) validateAddresses).SetEnabled(num4 != 0);
    }
    if (row != null && row.ShipDestType == "S" && PXUIFieldAttribute.GetError<POOrder.siteID>(cache, e.Row) == null)
    {
      string siteIdErrorMessage = row.SiteIdErrorMessage;
      if (!string.IsNullOrWhiteSpace(siteIdErrorMessage))
        cache.RaiseExceptionHandling<POOrder.siteID>(e.Row, cache.GetValueExt<POOrder.siteID>(e.Row), (Exception) new PXSetPropertyException(siteIdErrorMessage, (PXErrorLevel) 4));
    }
    PXUIFieldAttribute.SetEnabled<POOrder.payToVendorID>(cache, (object) row, EnumerableExtensions.IsNotIn<string>(row.Status, "L", "M", "C"));
    if (row == null)
      return;
    bool flag7 = row.OrderType == "BL";
    ((PXSelectBase) this.Receipts).AllowSelect = ((PXSelectBase) this.APDocs).AllowSelect = !flag7;
    ((PXSelectBase) this.ChildOrdersReceipts).AllowSelect = ((PXSelectBase) this.ChildOrdersAPDocs).AllowSelect = flag7;
  }

  public virtual bool IsShipToBAccountRequired(POOrder doc)
  {
    return EnumerableExtensions.IsNotIn<string>(doc.ShipDestType, "S", "P");
  }

  protected virtual void POOrder_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    POOrder row = (POOrder) e.Row;
    POOrder oldRow = (POOrder) e.OldRow;
    if (row == null)
      return;
    if (!sender.ObjectsEqual<POOrder.hold>((object) oldRow, (object) row))
      ((PXSelectBase) this.Transactions).Cache.ClearItemAttributes();
    if (!sender.ObjectsEqual<POOrder.projectID>(e.OldRow, e.Row) && row.OrderType == "PD")
    {
      string str1 = (string) null;
      string str2 = (string) null;
      if (row.ProjectID.HasValue && row.OrderType == "PD")
      {
        PMProject pmProject = PMProject.PK.Find((PXGraph) this, row.ProjectID);
        if (pmProject != null)
        {
          if (!PXAccess.FeatureInstalled<FeaturesSet.inventory>() && !PXAccess.FeatureInstalled<FeaturesSet.pOReceiptsWithoutInventory>())
          {
            str1 = "S";
            str2 = "B";
          }
          if (PXAccess.FeatureInstalled<FeaturesSet.inventory>())
          {
            str1 = pmProject.DropshipReceiptProcessing;
            str2 = pmProject.DropshipExpenseRecording;
          }
          if (PXAccess.FeatureInstalled<FeaturesSet.pOReceiptsWithoutInventory>())
          {
            str1 = "R";
            str2 = "B";
          }
        }
        if (str2 == "R")
        {
          PX.Objects.IN.INSetup current = ((PXSelectBase<PX.Objects.IN.INSetup>) this.INSetup).Current;
          if ((current != null ? (!current.UpdateGL.GetValueOrDefault() ? 1 : 0) : 1) != 0)
          {
            str2 = "B";
            sender.RaiseExceptionHandling<POOrder.projectID>(e.Row, (object) null, (Exception) new PXSetPropertyException("The selected project requires posting of drop-ship expenses on release of the purchase receipt. To be able to process the purchase order, either select the Update GL check box on the Inventory Preferences (IN101000) form or select On Bill Release in the Record Drop-Ship Expenses box for the {0} project.", new object[1]
            {
              sender.GetValueExt<POOrder.projectID>(e.Row)
            }));
            sender.SetValueExt<POOrder.projectID>(e.Row, (object) null);
          }
        }
        row.DropshipReceiptProcessing = str1;
        row.DropshipExpenseRecording = str2;
        try
        {
          SharedRecordAttribute.DefaultRecord<POOrder.shipAddressID>(sender, e.Row);
        }
        catch (SharedRecordMissingException ex)
        {
          sender.RaiseExceptionHandling<POOrder.siteID>(e.Row, sender.GetValueExt<POOrder.siteID>(e.Row), (Exception) new PXSetPropertyException("The document cannot be saved, shipping address is not specified  for the warehouse selected  on the Shipping Instructions tab.", (PXErrorLevel) 4));
        }
        try
        {
          SharedRecordAttribute.DefaultRecord<POOrder.shipContactID>(sender, e.Row);
        }
        catch (SharedRecordMissingException ex)
        {
          sender.RaiseExceptionHandling<POOrder.siteID>(e.Row, sender.GetValueExt<POOrder.siteID>(e.Row), (Exception) new PXSetPropertyException("The document cannot be saved, shipping contact is not specified  for the warehouse selected  on the Shipping Instructions tab.", (PXErrorLevel) 4));
        }
      }
      foreach (PXResult<POLine> pxResult in ((PXSelectBase<POLine>) this.Transactions).Select(Array.Empty<object>()))
      {
        POLine poLine = PXResult<POLine>.op_Implicit(pxResult);
        if (row.OrderType == "PD")
        {
          poLine.DropshipReceiptProcessing = str1;
          poLine.DropshipExpenseRecording = str2;
          poLine.POAccrualType = (string) PXFormulaAttribute.Evaluate<POLine.pOAccrualType>(((PXSelectBase) this.Transactions).Cache, (object) poLine);
        }
        ((PXSelectBase<POLine>) this.Transactions).Update(poLine);
      }
      if (((PXSelectBase<POOrderEntry.POOrderFilter>) this.filter).Current?.OrderNbr != null)
        ((PXSelectBase<POOrderEntry.POOrderFilter>) this.filter).Current.OrderNbr = (string) null;
    }
    if (e.ExternalCall && (!sender.ObjectsEqual<POOrder.orderDate>(e.OldRow, e.Row) || !sender.ObjectsEqual<POOrder.vendorLocationID>(e.OldRow, e.Row)))
      this._discountEngine.AutoRecalculatePricesAndDiscounts(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<POLine>) this.Transactions, (POLine) null, (PXSelectBase<POOrderDiscountDetail>) this.DiscountDetails, row.VendorLocationID, row.OrderDate, DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation | DiscountEngine.DiscountCalculationOptions.DisableARDiscountsCalculation);
    if (!this._discountEngine.IsInternalDiscountEngineCall && e.ExternalCall && sender.GetStatus((object) row) != 3 && !sender.ObjectsEqual<POOrder.curyDiscTot>(e.OldRow, e.Row))
    {
      this._discountEngine.SetTotalDocDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<POLine>) this.Transactions, (PXSelectBase<POOrderDiscountDetail>) this.DiscountDetails, ((PXSelectBase<POOrder>) this.Document).Current.CuryDiscTot, DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation | DiscountEngine.DiscountCalculationOptions.DisableARDiscountsCalculation);
      this.RecalculateTotalDiscount();
    }
    bool? nullable1 = row.Cancelled;
    Decimal? nullable2;
    if (nullable1.HasValue)
    {
      nullable1 = row.Cancelled;
      if (!nullable1.Value && !this.GetRequireControlTotal(row.OrderType))
      {
        Decimal? curyOrderTotal = row.CuryOrderTotal;
        Decimal? curyControlTotal = row.CuryControlTotal;
        if (!(curyOrderTotal.GetValueOrDefault() == curyControlTotal.GetValueOrDefault() & curyOrderTotal.HasValue == curyControlTotal.HasValue))
        {
          nullable2 = row.CuryOrderTotal;
          if (nullable2.HasValue)
          {
            nullable2 = row.CuryOrderTotal;
            Decimal num = 0M;
            if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
            {
              sender.SetValueExt<POOrder.curyControlTotal>(e.Row, (object) row.CuryOrderTotal);
              goto label_44;
            }
          }
          sender.SetValueExt<POOrder.curyControlTotal>(e.Row, (object) 0M);
        }
      }
    }
label_44:
    nullable1 = row.Hold;
    Decimal? nullable3;
    if (!nullable1.Value)
    {
      nullable1 = row.Cancelled;
      bool flag1 = false;
      if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue)
      {
        nullable2 = row.CuryControlTotal;
        Decimal? curyOrderTotal = row.CuryOrderTotal;
        if (!(nullable2.GetValueOrDefault() == curyOrderTotal.GetValueOrDefault() & nullable2.HasValue == curyOrderTotal.HasValue))
          sender.RaiseExceptionHandling<POOrder.curyControlTotal>(e.Row, (object) row.CuryControlTotal, (Exception) new PXSetPropertyException("Document is out of balance."));
        else
          sender.RaiseExceptionHandling<POOrder.curyControlTotal>(e.Row, (object) row.CuryControlTotal, (Exception) null);
        nullable3 = row.CuryLineTotal;
        Decimal num = 0M;
        if (nullable3.GetValueOrDefault() < num & nullable3.HasValue)
        {
          nullable1 = row.Hold;
          bool flag2 = false;
          if (nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue)
          {
            if (sender.RaiseExceptionHandling<POOrder.curyLineTotal>(e.Row, (object) row.CuryLineTotal, (Exception) new PXSetPropertyException("'{0}' may not be negative", new object[1]
            {
              (object) "[curyLineTotal]"
            })))
              throw new PXRowPersistingException("curyLineTotal", (object) null, "'{0}' may not be negative", new object[1]
              {
                (object) "curyLineTotal"
              });
            goto label_55;
          }
        }
        sender.RaiseExceptionHandling<POOrder.curyLineTotal>(e.Row, (object) null, (Exception) null);
        goto label_55;
      }
    }
    sender.RaiseExceptionHandling<POOrder.curyLineTotal>(e.Row, (object) null, (Exception) null);
label_55:
    if (row != null && this.IsExternalTax(row.TaxZoneID) && !sender.ObjectsEqual<POOrder.curyDiscTot>(e.Row, e.OldRow))
      row.IsTaxValid = new bool?(false);
    int? vendorId;
    if (((PXSelectBase) this.Document).View.Answer != 6 && !((PXGraph) this).IsContractBasedAPI)
    {
      vendorId = oldRow.VendorID;
      if (!vendorId.HasValue || sender.ObjectsEqual<POOrder.vendorID>(e.Row, e.OldRow))
        goto label_71;
    }
    if (!sender.ObjectsEqual<POOrder.orderDate>(e.Row, e.OldRow))
    {
      foreach (PXResult<POLine> pxResult in ((PXSelectBase<POLine>) this.Transactions).Select(Array.Empty<object>()))
      {
        POLine poLine = PXResult<POLine>.op_Implicit(pxResult);
        nullable1 = poLine.Completed;
        if (!nullable1.Value)
        {
          nullable1 = poLine.Cancelled;
          if (!nullable1.Value)
          {
            nullable3 = poLine.ReceivedQty;
            Decimal num = 0M;
            if (!(nullable3.GetValueOrDefault() > num & nullable3.HasValue))
              ((PXSelectBase) this.Transactions).Cache.SetDefaultExt<POLine.requestedDate>((object) poLine);
          }
        }
      }
    }
label_71:
    if (((PXSelectBase) this.Document).View.Answer != 6 && !((PXGraph) this).IsContractBasedAPI)
    {
      vendorId = oldRow.VendorID;
      if (!vendorId.HasValue || sender.ObjectsEqual<POOrder.vendorID>(e.Row, e.OldRow))
        goto label_85;
    }
    if (!sender.ObjectsEqual<POOrder.expectedDate>(e.Row, e.OldRow))
    {
      foreach (PXResult<POLine> pxResult in ((PXSelectBase<POLine>) this.Transactions).Select(Array.Empty<object>()))
      {
        POLine poLine = PXResult<POLine>.op_Implicit(pxResult);
        nullable1 = poLine.Completed;
        if (!nullable1.Value)
        {
          nullable1 = poLine.Cancelled;
          if (!nullable1.Value)
          {
            nullable3 = poLine.ReceivedQty;
            Decimal num = 0M;
            if (!(nullable3.GetValueOrDefault() > num & nullable3.HasValue))
            {
              POLine copy = PXCache<POLine>.CreateCopy(poLine);
              ((PXSelectBase) this.Transactions).Cache.SetDefaultExt<POLine.promisedDate>((object) copy);
              ((PXSelectBase) this.Transactions).Cache.Update((object) copy);
            }
          }
        }
      }
    }
label_85:
    if (!sender.ObjectsEqual<POOrder.dontPrint>(e.OldRow, e.Row))
    {
      nullable1 = row.DontPrint;
      if (nullable1.GetValueOrDefault())
        ((SelectedEntityEvent<POOrder>) PXEntityEventBase<POOrder>.Container<POOrder.Events>.Select((Expression<Func<POOrder.Events, PXEntityEvent<POOrder.Events>>>) (ev => ev.DoNotPrintChecked))).FireOn((PXGraph) this, (POOrder) e.Row);
    }
    if (sender.ObjectsEqual<POOrder.dontEmail>(e.OldRow, e.Row))
      return;
    nullable1 = row.DontEmail;
    if (!nullable1.GetValueOrDefault())
      return;
    ((SelectedEntityEvent<POOrder>) PXEntityEventBase<POOrder>.Container<POOrder.Events>.Select((Expression<Func<POOrder.Events, PXEntityEvent<POOrder.Events>>>) (ev => ev.DoNotEmailChecked))).FireOn((PXGraph) this, (POOrder) e.Row);
  }

  protected virtual void POOrder_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row == null)
      return;
    using (new ReadOnlyScope(new PXCache[2]
    {
      ((PXSelectBase) this.Shipping_Address).Cache,
      ((PXSelectBase) this.Shipping_Contact).Cache
    }))
    {
      try
      {
        SharedRecordAttribute.DefaultRecord<POOrder.shipAddressID>(sender, e.Row);
      }
      catch (SharedRecordMissingException ex)
      {
        sender.RaiseExceptionHandling<POOrder.siteID>(e.Row, sender.GetValueExt<POOrder.siteID>(e.Row), (Exception) new PXSetPropertyException("The document cannot be saved, shipping address is not specified  for the warehouse selected  on the Shipping Instructions tab.", (PXErrorLevel) 4));
      }
      try
      {
        SharedRecordAttribute.DefaultRecord<POOrder.shipContactID>(sender, e.Row);
      }
      catch (SharedRecordMissingException ex)
      {
        sender.RaiseExceptionHandling<POOrder.siteID>(e.Row, sender.GetValueExt<POOrder.siteID>(e.Row), (Exception) new PXSetPropertyException("The document cannot be saved, shipping contact is not specified  for the warehouse selected  on the Shipping Instructions tab.", (PXErrorLevel) 4));
      }
    }
  }

  protected virtual void POOrder_ShipDestType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    POOrder row = (POOrder) e.Row;
    if (row == null)
      return;
    if (row.ShipDestType == "S")
    {
      sender.SetDefaultExt<POOrder.siteID>(e.Row);
      sender.SetValueExt<POOrder.shipToBAccountID>(e.Row, (object) null);
      sender.SetValueExt<POOrder.shipToLocationID>(e.Row, (object) null);
    }
    else if (row.ShipDestType == "P")
    {
      sender.SetValueExt<POOrder.siteID>(e.Row, (object) null);
      sender.SetValueExt<POOrder.shipToBAccountID>(e.Row, (object) null);
      sender.SetValueExt<POOrder.shipToLocationID>(e.Row, (object) null);
    }
    else
    {
      sender.SetValueExt<POOrder.siteID>(e.Row, (object) null);
      sender.SetDefaultExt<POOrder.shipToBAccountID>(e.Row);
      sender.SetDefaultExt<POOrder.shipToLocationID>(e.Row);
    }
  }

  protected virtual void POOrder_SiteID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    POOrder row = (POOrder) e.Row;
    if (row == null || !(row.ShipDestType == "S"))
      return;
    string str = string.Empty;
    try
    {
      SharedRecordAttribute.DefaultRecord<POOrder.shipAddressID>(sender, e.Row);
    }
    catch (SharedRecordMissingException ex)
    {
      sender.RaiseExceptionHandling<POOrder.siteID>(e.Row, sender.GetValueExt<POOrder.siteID>(e.Row), (Exception) new PXSetPropertyException("The document cannot be saved, shipping address is not specified  for the warehouse selected  on the Shipping Instructions tab.", (PXErrorLevel) 4));
      sender.SetValueExt<POOrder.shipAddressID>(e.Row, (object) null);
      str = "The document cannot be saved, shipping address is not specified  for the warehouse selected  on the Shipping Instructions tab.";
    }
    try
    {
      SharedRecordAttribute.DefaultRecord<POOrder.shipContactID>(sender, e.Row);
    }
    catch (SharedRecordMissingException ex)
    {
      sender.RaiseExceptionHandling<POOrder.siteID>(e.Row, sender.GetValueExt<POOrder.siteID>(e.Row), (Exception) new PXSetPropertyException("The document cannot be saved, shipping contact is not specified  for the warehouse selected  on the Shipping Instructions tab.", (PXErrorLevel) 4));
      sender.SetValueExt<POOrder.shipContactID>(e.Row, (object) null);
      str = "The document cannot be saved, shipping contact is not specified  for the warehouse selected  on the Shipping Instructions tab.";
    }
    sender.SetValueExt<POOrder.siteIdErrorMessage>(e.Row, (object) str);
    if (!string.IsNullOrWhiteSpace(str))
      return;
    PXUIFieldAttribute.SetError<POOrder.siteID>(sender, e.Row, (string) null);
  }

  protected virtual void POOrder_ShipToBAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if ((POOrder) e.Row == null)
      return;
    sender.SetDefaultExt<POOrder.shipToLocationID>(e.Row);
  }

  protected virtual void POOrder_ShipToLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if ((POOrder) e.Row == null)
      return;
    try
    {
      SharedRecordAttribute.DefaultRecord<POOrder.shipAddressID>(sender, e.Row);
    }
    catch (SharedRecordMissingException ex)
    {
      sender.RaiseExceptionHandling<POOrder.siteID>(e.Row, sender.GetValueExt<POOrder.siteID>(e.Row), (Exception) new PXSetPropertyException("The document cannot be saved, shipping address is not specified  for the warehouse selected  on the Shipping Instructions tab.", (PXErrorLevel) 4));
    }
    try
    {
      SharedRecordAttribute.DefaultRecord<POOrder.shipContactID>(sender, e.Row);
    }
    catch (SharedRecordMissingException ex)
    {
      sender.RaiseExceptionHandling<POOrder.siteID>(e.Row, sender.GetValueExt<POOrder.siteID>(e.Row), (Exception) new PXSetPropertyException("The document cannot be saved, shipping contact is not specified  for the warehouse selected  on the Shipping Instructions tab.", (PXErrorLevel) 4));
    }
  }

  protected virtual void POOrder_ShipToLocationID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    POOrder row = (POOrder) e.Row;
    if (row == null || this.IsShipToBAccountRequired(row))
      return;
    ((CancelEventArgs) e).Cancel = true;
    e.NewValue = (object) null;
  }

  protected virtual void POOrder_ShipToBAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    POOrder row = (POOrder) e.Row;
    if (row == null)
      return;
    if (!this.IsShipToBAccountRequired(row))
    {
      ((CancelEventArgs) e).Cancel = true;
      e.NewValue = (object) null;
    }
    else
    {
      if (!(row.OrderType == "DP"))
        return;
      if (PXResultset<PX.Objects.SO.SOLineSplit>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLineSplit, PXSelectJoin<PX.Objects.SO.SOLineSplit, InnerJoin<PX.Objects.SO.SOOrder, On<PX.Objects.SO.SOOrder.orderType, Equal<PX.Objects.SO.SOLineSplit.orderType>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<PX.Objects.SO.SOLineSplit.orderNbr>>>>, Where<PX.Objects.SO.SOLineSplit.pOType, Equal<Current<POOrder.orderType>>, And<PX.Objects.SO.SOLineSplit.pONbr, Equal<Current<POOrder.orderNbr>>, And<PX.Objects.SO.SOOrder.customerID, NotEqual<Required<PX.Objects.SO.SOOrder.customerID>>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        e.NewValue
      })) != null)
      {
        PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          e.NewValue
        }));
        throw new PXSetPropertyException("A customer to whom the drop-ship order is shipped must match the customer specified in the linked sales order.")
        {
          ErrorValue = (object) customer?.AcctCD
        };
      }
    }
  }

  protected virtual void POOrder_OwnerID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if ((POOrder) e.Row == null || !((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current.UpdateSubOnOwnerChange.GetValueOrDefault())
      return;
    foreach (PXResult<POLine> pxResult in ((PXSelectBase<POLine>) this.Transactions).Select(Array.Empty<object>()))
    {
      POLine poLine = PXResult<POLine>.op_Implicit(pxResult);
      if (poLine.LineType == "NS" || poLine.LineType == "SV")
        ((PXSelectBase) this.Transactions).Cache.SetDefaultExt<POLine.expenseSubID>((object) poLine);
    }
  }

  protected virtual void POOrder_Cancelled_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    POOrder row = (POOrder) e.Row;
    if (row == null || !(bool) e.NewValue)
      return;
    if (row.OrderType == "BL")
    {
      if (PXResultset<POLine>.op_Implicit(PXSelectBase<POLine, PXSelect<POLine, Where<POLine.receivedQty, Greater<decimal0>, And<POLine.orderType, Equal<Required<POOrder.orderType>>, And<POLine.orderNbr, Equal<Required<POOrder.orderNbr>>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.OrderType,
        (object) row.OrderNbr
      })) == null)
        return;
      this.ThrowErrorWhenPurchaseReceiptExists((POReceipt) null, row);
    }
    else
    {
      POReceipt receipt = PXResultset<POReceipt>.op_Implicit(PXSelectBase<POReceipt, PXSelectJoin<POReceipt, InnerJoin<POOrderReceipt, On<POOrderReceipt.FK.Receipt>>, Where<POOrderReceipt.pOType, Equal<Required<POReceiptLine.pOType>>, And<POOrderReceipt.pONbr, Equal<Required<POReceiptLine.pONbr>>>>, OrderBy<Asc<POReceipt.released>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.OrderType,
        (object) row.OrderNbr
      }));
      if (receipt == null)
        return;
      this.ThrowErrorWhenPurchaseReceiptExists(receipt, row);
    }
  }

  protected virtual void ThrowErrorWhenPurchaseReceiptExists(POReceipt receipt, POOrder order)
  {
    if (receipt != null)
    {
      bool? released = receipt.Released;
      bool flag = false;
      if (released.GetValueOrDefault() == flag & released.HasValue)
        throw new PXSetPropertyException("This Order can not be cancelled - there is one or more unreleased PO Receipts referencing it");
    }
    if (order.OrderType == "BL" || receipt != null && receipt.Released.GetValueOrDefault())
      throw new PXSetPropertyException("The {0} purchase order cannot be canceled because some of the ordered items have been received.", new object[1]
      {
        (object) order.OrderNbr
      });
  }

  protected virtual void ThrowErrorWhenPurchaseReceiptExists(POReceipt receipt, POLine line)
  {
    if (receipt != null)
    {
      bool? released = receipt.Released;
      bool flag = false;
      if (released.GetValueOrDefault() == flag & released.HasValue)
        throw new PXSetPropertyException("This line can not be completed or cancelled - there is one or more unreleased PO Receipts referencing it");
    }
    if (line.OrderType == "BL" || receipt != null && receipt.Released.GetValueOrDefault())
      throw new PXSetPropertyException("The {0} purchase order line cannot be canceled because a part of the line items has been received.", new object[1]
      {
        (object) line.OrderNbr
      });
  }

  public virtual void POOrder_Cancelled_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    POOrder row = (POOrder) e.Row;
    bool? nullable1 = e.OldValue as bool?;
    int num1 = nullable1.GetValueOrDefault() ? 1 : 0;
    nullable1 = row.Cancelled;
    bool valueOrDefault = nullable1.GetValueOrDefault();
    int num2 = valueOrDefault ? 1 : 0;
    if (num1 == num2)
      return;
    foreach (PXResult<POLine> pxResult in ((PXSelectBase<POLine>) this.Transactions).Select(Array.Empty<object>()))
    {
      POLine line = PXResult<POLine>.op_Implicit(pxResult);
      bool? nullable2 = new bool?();
      bool? nullable3;
      if (valueOrDefault)
      {
        nullable3 = line.Completed;
        if (!nullable3.GetValueOrDefault())
          nullable2 = new bool?(true);
      }
      if (!valueOrDefault)
      {
        nullable3 = line.Cancelled;
        if (nullable3.GetValueOrDefault())
        {
          bool? nullable4;
          if (!this.CanReopenPOLine(line))
          {
            nullable3 = new bool?();
            nullable4 = nullable3;
          }
          else
            nullable4 = new bool?(false);
          nullable2 = nullable4;
        }
      }
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, line.InventoryID);
      if (!nullable2.GetValueOrDefault() && inventoryItem != null)
      {
        nullable3 = inventoryItem.IsConverted;
        if (nullable3.GetValueOrDefault())
        {
          nullable3 = line.IsStockItem;
          if (nullable3.HasValue)
          {
            nullable3 = line.IsStockItem;
            bool? stkItem = inventoryItem.StkItem;
            if (!(nullable3.GetValueOrDefault() == stkItem.GetValueOrDefault() & nullable3.HasValue == stkItem.HasValue))
              continue;
          }
        }
      }
      if (nullable2.HasValue)
      {
        try
        {
          POLine copy = (POLine) ((PXSelectBase) this.Transactions).Cache.CreateCopy((object) line);
          copy.Cancelled = nullable2;
          copy.Completed = nullable2;
          using (new POOrderEntry.SuppressOrderEventsScope((PXGraph) this))
            ((PXSelectBase<POLine>) this.Transactions).Update(copy);
        }
        catch (PXFieldValueProcessingException ex)
        {
          throw new PXException("{0} '{1}' record raised at least one error. Please review the errors.", new object[2]
          {
            (object) "Updating",
            (object) ((PXSelectBase) this.Transactions).Cache.GetItemType().Name
          });
        }
      }
    }
    if (valueOrDefault)
      return;
    this.RaiseOrderEvents(row);
  }

  public virtual bool CanReopenPOLine(POLine line)
  {
    POOrder current = ((PXSelectBase<POOrder>) this.Document).Current;
    bool? nullable;
    if (!line.Completed.GetValueOrDefault())
    {
      nullable = line.Cancelled;
      if (!nullable.GetValueOrDefault())
        return true;
    }
    if (!this.IsLinkedToSO(line))
      return true;
    int num;
    if (current == null)
    {
      num = 0;
    }
    else
    {
      nullable = current.IsLegacyDropShip;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    return num == 0;
  }

  private bool IsLineWithDropShipLocation(POLine poLine)
  {
    PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this, (int?) poLine?.SiteID);
    return inSite == null || inSite.DropShipLocationID.HasValue;
  }

  protected virtual void POOrder_Hold_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    bool? newValue = (bool?) e.NewValue;
    bool flag = false;
    if (newValue.GetValueOrDefault() == flag & newValue.HasValue && ((PXSelectBase<POOrder>) this.Document).Current?.OrderType == "DP")
    {
      foreach (PXResult<POLine> pxResult in ((PXSelectBase<POLine>) this.Transactions).Select(Array.Empty<object>()))
      {
        POLine poLine = PXResult<POLine>.op_Implicit(pxResult);
        if (!this.IsLineWithDropShipLocation(poLine))
          GraphHelper.MarkUpdated(((PXSelectBase) this.Transactions).Cache, (object) poLine, true);
      }
    }
    else
    {
      if (!((bool?) e.NewValue).GetValueOrDefault())
        return;
      POOrderReceipt poOrderReceipt = PXResultset<POOrderReceipt>.op_Implicit(PXSelectBase<POOrderReceipt, PXSelectJoin<POOrderReceipt, InnerJoin<POReceipt, On2<POOrderReceipt.FK.Receipt, And<POReceipt.released, Equal<boolFalse>>>>, Where<POOrderReceipt.pOType, Equal<Current<POOrder.orderType>>, And<POOrderReceipt.pONbr, Equal<Current<POOrder.orderNbr>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        e.Row
      }, Array.Empty<object>()));
      if (poOrderReceipt != null)
        throw new PXException("The purchase order {0} {1} cannot be put on hold. It has unreleased receipt {2}.", new object[3]
        {
          (object) poOrderReceipt.POType,
          (object) poOrderReceipt.PONbr,
          (object) poOrderReceipt.ReceiptNbr
        });
    }
  }

  protected virtual void POOrder_OrderType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) "RO";
  }

  protected virtual void POOrder_Approved_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) (bool) (((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current == null ? 1 : (!((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current.OrderRequestApproval.GetValueOrDefault() ? 1 : 0));
  }

  protected virtual void POOrder_DontPrint_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if ((POOrder) e.Row == null)
      return;
    e.NewValue = (object) (bool) (((PXSelectBase<PX.Objects.CR.Location>) this.location).Current == null ? 1 : (!((PXSelectBase<PX.Objects.CR.Location>) this.location).Current.VPrintOrder.GetValueOrDefault() ? 1 : 0));
  }

  protected virtual void POOrder_DontEmail_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if ((POOrder) e.Row == null)
      return;
    e.NewValue = (object) (bool) (((PXSelectBase<PX.Objects.CR.Location>) this.location).Current == null ? 1 : (!((PXSelectBase<PX.Objects.CR.Location>) this.location).Current.VEmailOrder.GetValueOrDefault() ? 1 : 0));
  }

  protected virtual void POOrder_ExpectedDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    POOrder row = (POOrder) e.Row;
    PX.Objects.CR.Location current = ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current;
    if (row == null)
      return;
    DateTime? orderDate = row.OrderDate;
    if (!orderDate.HasValue)
      return;
    int valueOrDefault = current != null ? (int) current.VLeadTime.GetValueOrDefault() : 0;
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    orderDate = row.OrderDate;
    // ISSUE: variable of a boxed type
    __Boxed<DateTime> local = (ValueType) orderDate.Value.AddDays((double) valueOrDefault);
    defaultingEventArgs.NewValue = (object) local;
  }

  protected virtual void POOrder_TaxZoneID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    POOrder row = (POOrder) e.Row;
    if (row == null)
      return;
    e.NewValue = (object) this.GetDefaultTaxZone(row);
  }

  public virtual string GetDefaultTaxZone(POOrder row)
  {
    string defaultTaxZone = (string) null;
    if (row != null)
    {
      PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.VendorID,
        (object) row.VendorLocationID
      }));
      if (location != null && !string.IsNullOrEmpty(location.VTaxZoneID))
        defaultTaxZone = location.VTaxZoneID;
    }
    return defaultTaxZone;
  }

  protected virtual void POOrder_VendorLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PX.Objects.CR.Location current = ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current;
    POOrder row = (POOrder) e.Row;
    using (new POOrderEntry.VendorLocationUpdatedContext())
    {
      if (current != null)
      {
        int? nullable = current.BAccountID;
        int? vendorId = row.VendorID;
        if (nullable.GetValueOrDefault() == vendorId.GetValueOrDefault() & nullable.HasValue == vendorId.HasValue)
        {
          int? locationId = current.LocationID;
          nullable = row.VendorLocationID;
          if (locationId.GetValueOrDefault() == nullable.GetValueOrDefault() & locationId.HasValue == nullable.HasValue)
            goto label_5;
        }
      }
      ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current = PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) this.location).Select(Array.Empty<object>()));
label_5:
      sender.SetDefaultExt<POOrder.branchID>(e.Row);
      sender.SetDefaultExt<POOrder.taxCalcMode>(e.Row);
      sender.SetDefaultExt<POOrder.shipVia>(e.Row);
      sender.SetDefaultExt<POOrder.fOBPoint>(e.Row);
      sender.SetDefaultExt<POOrder.expectedDate>(e.Row);
      sender.SetDefaultExt<POOrder.siteID>(e.Row);
      sender.SetDefaultExt<POOrder.approved>(e.Row);
      sender.SetDefaultExt<POOrder.dontPrint>(e.Row);
      sender.SetDefaultExt<POOrder.dontEmail>(e.Row);
      sender.SetDefaultExt<POOrder.printed>(e.Row);
      sender.SetDefaultExt<POOrder.emailed>(e.Row);
      if (row.OrderType != "DP")
      {
        sender.SetDefaultExt<POOrder.shipDestType>(e.Row);
        sender.SetDefaultExt<POOrder.shipToLocationID>(e.Row);
      }
      SharedRecordAttribute.DefaultRecord<POOrder.remitAddressID>(sender, e.Row);
      SharedRecordAttribute.DefaultRecord<POOrder.remitContactID>(sender, e.Row);
      if (e.OldValue == null)
        return;
      foreach (PXResult<POLine, INItemPlan> pxResult in PXSelectBase<POLine, PXViewOf<POLine>.BasedOn<SelectFromBase<POLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INItemPlan>.On<BqlOperand<POLine.planID, IBqlLong>.IsEqual<INItemPlan.supplyPlanID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLine.orderType, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<POLine.orderNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.OrderType,
        (object) row.OrderNbr
      }))
      {
        INItemPlan inItemPlan = PXResult<POLine, INItemPlan>.op_Implicit(pxResult);
        inItemPlan.VendorLocationID = ((PXSelectBase<POOrder>) this.Document).Current.VendorLocationID;
        GraphHelper.Caches<INItemPlan>((PXGraph) this).Update(inItemPlan);
      }
    }
  }

  protected virtual void POOrder_PayToVendorID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is POOrder row))
      return;
    PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelectReadonly<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<POOrder.payToVendorID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    }));
    if (vendor != null && vendor.CuryID != null && !vendor.AllowOverrideCury.GetValueOrDefault() && row.CuryID != vendor.CuryID)
    {
      e.NewValue = (object) vendor.AcctCD;
      throw new PXSetPropertyException("The currency '{1}' of the pay-to vendor '{0}' differs from currency '{2}' of the document.", new object[3]
      {
        (object) vendor.AcctCD,
        (object) vendor.CuryID,
        (object) row.CuryID
      });
    }
  }

  [PopupMessage]
  [PXMergeAttributes]
  protected virtual void POOrder_VendorID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void POOrder_VendorID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is POOrder row))
      return;
    using (new POOrderEntry.VendorUpdatedContext())
    {
      sender.SetDefaultExt<POOrder.vendorLocationID>((object) row);
      int? nullable1 = row.VendorID;
      if (nullable1.HasValue)
      {
        int? nullable2;
        if (PXAccess.FeatureInstalled<FeaturesSet.vendorRelations>())
        {
          nullable1 = (int?) PX.Objects.AP.Vendor.PK.Find((PXGraph) this, row.VendorID)?.PayToVendorID;
          nullable2 = nullable1 ?? row.VendorID;
        }
        else
          nullable2 = row.VendorID;
        row.PayToVendorID = nullable2;
      }
      else
      {
        POOrder poOrder = row;
        nullable1 = new int?();
        int? nullable3 = nullable1;
        poOrder.PayToVendorID = nullable3;
      }
      sender.SetDefaultExt<POOrder.termsID>((object) row);
      if (e.OldValue != null)
      {
        nullable1 = ((POOrder) e.Row).VendorID;
        int? oldValue = (int?) e.OldValue;
        if (!(nullable1.GetValueOrDefault() == oldValue.GetValueOrDefault() & nullable1.HasValue == oldValue.HasValue))
        {
          sender.SetDefaultExt<POOrder.orderDate>(e.Row);
          sender.SetValue<POOrder.vendorRefNbr>(e.Row, (object) null);
          sender.SetDefaultExt<POOrder.ownerID>(e.Row);
          foreach (PXResult<POLine> pxResult in ((PXSelectBase<POLine>) this.Transactions).Select(Array.Empty<object>()))
          {
            POLine poLine = PXResult<POLine>.op_Implicit(pxResult);
            poLine.VendorID = ((PXSelectBase<POOrder>) this.Document).Current.VendorID;
            poLine.POAccrualType = (string) PXFormulaAttribute.Evaluate<POLine.pOAccrualType>(((PXSelectBase) this.Transactions).Cache, (object) poLine);
            ((PXSelectBase<POLine>) this.Transactions).Update(poLine);
          }
          foreach (PXResult<POLine, INItemPlan, POOrderEntry.SOLineSplit3> pxResult in PXSelectBase<POLine, PXViewOf<POLine>.BasedOn<SelectFromBase<POLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<INItemPlan>.On<BqlOperand<POLine.planID, IBqlLong>.IsEqual<INItemPlan.supplyPlanID>>>, FbqlJoins.Left<POOrderEntry.SOLineSplit3>.On<BqlOperand<POOrderEntry.SOLineSplit3.planID, IBqlLong>.IsEqual<INItemPlan.planID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLine.orderType, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<POLine.orderNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) row.OrderType,
            (object) row.OrderNbr
          }))
          {
            POOrderEntry.SOLineSplit3 split = PXResult<POLine, INItemPlan, POOrderEntry.SOLineSplit3>.op_Implicit(pxResult);
            INItemPlan inItemPlan = PXResult<POLine, INItemPlan, POOrderEntry.SOLineSplit3>.op_Implicit(pxResult);
            if (split.OrderNbr != null)
            {
              this.UpdateSOLine(split, ((PXSelectBase<POOrder>) this.Document).Current.VendorID, true);
              GraphHelper.MarkUpdated(((PXSelectBase) this.FixedDemand).Cache, (object) split, true);
            }
            if (inItemPlan.PlanID.HasValue)
            {
              inItemPlan.VendorID = ((PXSelectBase<POOrder>) this.Document).Current.VendorID;
              inItemPlan.VendorLocationID = ((PXSelectBase<POOrder>) this.Document).Current.VendorLocationID;
              GraphHelper.Caches<INItemPlan>((PXGraph) this).Update(inItemPlan);
            }
          }
        }
      }
      Validate.VerifyField<POOrder.payToVendorID>(sender, (object) row);
      Validate.VerifyField<POOrder.vendorRefNbr>(sender, (object) row);
    }
  }

  protected virtual void POOrder_VendorID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is POOrder row) || ((PXSelectBase<POLine>) this.Transactions).Select(Array.Empty<object>()).Count == 0)
      return;
    PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXViewOf<PX.Objects.AP.Vendor>.BasedOn<SelectFromBase<PX.Objects.AP.Vendor, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    }));
    if (vendor != null)
    {
      bool? nullable = vendor.AllowOverrideCury;
      if (!nullable.GetValueOrDefault() && vendor.CuryID != row.CuryID && !string.IsNullOrEmpty(vendor.CuryID))
        this.RaiseVendorIDException(sender, row, e.NewValue, $"The selected vendor does not work with the {row.CuryID} currency specified in the purchase order. Select another vendor.");
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ((PXGraph) this).FindImplementation<IPXCurrencyHelper>().GetCurrencyInfo(row.CuryInfoID);
      if (currencyInfo != null && currencyInfo.CuryID != currencyInfo.BaseCuryID)
      {
        nullable = vendor.AllowOverrideRate;
        if (!nullable.GetValueOrDefault() && vendor.CuryRateTypeID != currencyInfo.CuryRateTypeID)
          this.RaiseVendorIDException(sender, row, e.NewValue, $"The selected vendor does not work with the {currencyInfo.CuryRateTypeID} currency rate type specified in the purchase order. Select another vendor.");
      }
    }
    if (((PXSelectBase<POOrderPOReceipt>) this.Receipts).Select(Array.Empty<object>()).Count > 0)
      this.RaiseVendorIDException(sender, row, e.NewValue, "You cannot change a vendor for this purchase order because it has purchase receipts linked.");
    if (((PXSelectBase<POOrderAPDoc>) this.APDocs).Select(Array.Empty<object>()).Count > 0)
      this.RaiseVendorIDException(sender, row, e.NewValue, "You cannot change a vendor for this purchase order because it has AP documents linked.");
    if (PXResult<POLine>.op_Implicit(((IEnumerable<PXResult<POLine>>) ((PXSelectBase<POLine>) this.Transactions).Select(Array.Empty<object>())).AsEnumerable<PXResult<POLine>>().FirstOrDefault<PXResult<POLine>>((Func<PXResult<POLine>, bool>) (res => !string.IsNullOrEmpty(PXResult<POLine>.op_Implicit(res).PONbr)))) != null)
      this.RaiseVendorIDException(sender, row, e.NewValue, "You cannot change a vendor for this purchase order because one or more lines were created from a blanked purchase order.");
    foreach (PXResult<PX.Objects.SO.SOLineSplit> pxResult in PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.pOType, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<PX.Objects.SO.SOLineSplit.pONbr, IBqlString>.IsEqual<P.AsString>>>.Aggregate<To<GroupBy<PX.Objects.SO.SOLineSplit.orderType>, GroupBy<PX.Objects.SO.SOLineSplit.orderNbr>, GroupBy<PX.Objects.SO.SOLineSplit.lineNbr>, GroupBy<PX.Objects.SO.SOLineSplit.pOType>, GroupBy<PX.Objects.SO.SOLineSplit.pONbr>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.OrderType,
      (object) row.OrderNbr
    }))
    {
      PXResult<PX.Objects.SO.SOLineSplit>.op_Implicit(pxResult);
      if (PXSelectBase<PX.Objects.SO.SOLineSplit, PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.orderType, Equal<P.AsString.ASCII>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.orderNbr, Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.lineNbr, Equal<P.AsInt>>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.pOType, NotEqual<P.AsString.ASCII>>>>>.Or<BqlOperand<PX.Objects.SO.SOLineSplit.pONbr, IBqlString>.IsNotEqual<P.AsString>>>>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.OrderType,
        (object) row.OrderNbr
      }).Count != 0)
        this.RaiseVendorIDException(sender, row, e.NewValue, "You cannot change a vendor for this purchase order because it is linked with a sales order line that is split between multiple purchase orders.");
    }
  }

  public virtual void RaiseVendorIDException(
    PXCache sender,
    POOrder order,
    object newVendorID,
    string error)
  {
    BAccountR baccountR = (BAccountR) PXSelectorAttribute.Select<POOrder.vendorID>(sender, (object) order, newVendorID);
    throw new PXSetPropertyException(error)
    {
      ErrorValue = (object) baccountR?.AcctCD
    };
  }

  protected virtual void POOrder_PayToVendorID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is POOrder row))
      return;
    sender.SetDefaultExt<POOrder.termsID>((object) row);
    foreach (POLine line in GraphHelper.RowCast<POLine>((IEnumerable) ((PXSelectBase<POLine>) this.Transactions).Select(Array.Empty<object>())).Where<POLine>((Func<POLine, bool>) (line => !line.Completed.GetValueOrDefault() && !line.Cancelled.GetValueOrDefault() && !line.Closed.GetValueOrDefault())))
    {
      if (this.IsAccrualAccountEnabled(line))
      {
        try
        {
          ((PXSelectBase) this.Transactions).Cache.SetDefaultExt<POLine.pOAccrualAcctID>((object) line);
        }
        catch (PXSetPropertyException<POLine.pOAccrualAcctID> ex)
        {
          ((PXSelectBase) this.Transactions).Cache.RaiseExceptionHandling<POLine.pOAccrualAcctID>((object) line, (object) line.POAccrualAcctID, (Exception) ex);
        }
        try
        {
          ((PXSelectBase) this.Transactions).Cache.SetDefaultExt<POLine.pOAccrualSubID>((object) line);
        }
        catch (PXSetPropertyException<POLine.pOAccrualSubID> ex)
        {
          ((PXSelectBase) this.Transactions).Cache.RaiseExceptionHandling<POLine.pOAccrualSubID>((object) line, (object) line.POAccrualSubID, (Exception) ex);
        }
      }
      if (line.LineType == "NS" || line.LineType == "SV")
        ((PXSelectBase) this.Transactions).Cache.SetDefaultExt<POLine.expenseSubID>((object) line);
    }
  }

  protected virtual void POOrder_BranchID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is POOrder))
      return;
    sender.SetDefaultExt<POOrder.externalTaxExemptionNumber>(e.Row);
    sender.SetDefaultExt<POOrder.entityUsageType>(e.Row);
  }

  protected virtual void POOrder_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    POOrder row = (POOrder) e.Row;
    int? rowCount1 = PXSelectBase<POOrderReceipt, PXSelectGroupBy<POOrderReceipt, Where<POOrderReceipt.pONbr, Equal<Required<POOrder.orderNbr>>, And<POOrderReceipt.pOType, Equal<Required<POOrder.orderType>>>>, Aggregate<Count>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.OrderNbr,
      (object) row.OrderType
    }).RowCount;
    int num1 = 0;
    if (rowCount1.GetValueOrDefault() > num1 & rowCount1.HasValue)
      throw new PXException("The order cannot be deleted because some quantity of items for this purchase order have been received.");
    int? rowCount2 = PXSelectBase<PX.Objects.AP.APTran, PXSelectGroupBy<PX.Objects.AP.APTran, Where<PX.Objects.AP.APTran.pONbr, Equal<Required<POOrder.orderNbr>>, And<PX.Objects.AP.APTran.pOOrderType, Equal<Required<POOrder.orderType>>, And<PX.Objects.AP.APTran.released, Equal<True>>>>, Aggregate<Count>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.OrderNbr,
      (object) row.OrderType
    }).RowCount;
    int num2 = 0;
    if (rowCount2.GetValueOrDefault() > num2 & rowCount2.HasValue)
      throw new PXException("The order cannot be deleted because there is at least one AP bill has been released for this order. For the list of AP bills, refer to Reports > View Purchase Order Receipts History.");
    if (PXResultset<POOrderPrepayment>.op_Implicit(PXSelectBase<POOrderPrepayment, PXSelect<POOrderPrepayment, Where<POOrderPrepayment.orderType, Equal<Current<POOrder.orderType>>, And<POOrderPrepayment.orderNbr, Equal<Current<POOrder.orderNbr>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>())) != null)
      throw new PXException("The purchase order cannot be deleted because one or multiple prepayments have been applied to this order. To proceed, delete all prepayments first.");
    rowCount2 = PXSelectBase<PX.Objects.AP.APTran, PXSelectGroupBy<PX.Objects.AP.APTran, Where<PX.Objects.AP.APTran.pONbr, Equal<Required<POOrder.orderNbr>>, And<PX.Objects.AP.APTran.pOOrderType, Equal<Required<POOrder.orderType>>>>, Aggregate<Count>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.OrderNbr,
      (object) row.OrderType
    }).RowCount;
    int num3 = 0;
    if (rowCount2.GetValueOrDefault() > num3 & rowCount2.HasValue)
      throw new PXException("The order cannot be deleted because one or multiple AP bills have been generated for this order. To proceed, delete AP bills first. For the list of AP bills, refer to Reports > View Purchase Order Receipts History.");
    if (!(row.OrderType == "BL"))
      return;
    if (PXResultset<POLine>.op_Implicit(PXSelectBase<POLine, PXViewOf<POLine>.BasedOn<SelectFromBase<POLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLine.pONbr, Equal<BqlField<POOrder.orderNbr, IBqlString>.FromCurrent>>>>>.And<BqlOperand<POLine.pOType, IBqlString>.IsEqual<BqlField<POOrder.orderType, IBqlString>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>())) != null)
      throw new PXException("The {0} blanket purchase order cannot be deleted because at least one normal or drop-ship purchase order is linked to this order.", new object[1]
      {
        (object) row.OrderNbr
      });
  }

  protected virtual void POOrder_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    POOrder row = (POOrder) e.Row;
    if ((e.Operation & 3) == 3)
      return;
    PXDefaultAttribute.SetPersistingCheck<POOrder.siteID>(sender, (object) row, !(row.ShipDestType == "S") || !(row.OrderType != "RS") ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXDefaultAttribute.SetPersistingCheck<POOrder.shipToLocationID>(sender, (object) row, this.IsShipToBAccountRequired(row) ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<POOrder.shipToBAccountID>(sender, (object) row, this.IsShipToBAccountRequired(row) ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    if (row != null && !string.IsNullOrEmpty(row.ShipVia))
    {
      PX.Objects.CS.Carrier carrier = (PX.Objects.CS.Carrier) PXSelectorAttribute.Select<POOrder.shipVia>(sender, (object) row);
      int num;
      if (carrier == null)
      {
        num = 0;
      }
      else
      {
        bool? isActive = carrier.IsActive;
        bool flag = false;
        num = isActive.GetValueOrDefault() == flag & isActive.HasValue ? 1 : 0;
      }
      if (num != 0)
        sender.RaiseExceptionHandling<POOrder.shipVia>((object) row, (object) row.ShipVia, (Exception) new PXSetPropertyException((IBqlTable) row, "The Ship Via code is not active.", (PXErrorLevel) 2));
    }
    if (row.OrderType == "PD")
    {
      if (row.ShipDestType != "P" && sender.RaiseExceptionHandling<POOrder.shipDestType>(e.Row, (object) row.ShipDestType, (Exception) new PXSetPropertyException("Project Site is the only valid Shipping Destination Type for the Project Drop-Ship order.", (PXErrorLevel) 4)))
        throw new PXRowPersistingException(typeof (POOrder.shipDestType).Name, (object) null, "Project Site is the only valid Shipping Destination Type for the Project Drop-Ship order.");
      if (!row.ProjectID.HasValue)
      {
        if (sender.RaiseExceptionHandling<POOrder.projectID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[projectID]"
        })))
          throw new PXRowPersistingException(typeof (POOrder.projectID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
          {
            (object) "projectID"
          });
      }
    }
    if (string.IsNullOrEmpty(row.TermsID))
    {
      if (sender.RaiseExceptionHandling<POOrder.termsID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[termsID]"
      })))
        throw new PXRowPersistingException(typeof (POOrder.termsID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) "termsID"
        });
    }
    Decimal? nullable = row.CuryOrderTotal;
    Decimal num1 = 0M;
    if (nullable.GetValueOrDefault() < num1 & nullable.HasValue)
    {
      bool? hold = row.Hold;
      bool flag = false;
      if (hold.GetValueOrDefault() == flag & hold.HasValue)
      {
        if (sender.RaiseExceptionHandling<POOrder.curyLineTotal>(e.Row, (object) row.CuryLineTotal, (Exception) new PXSetPropertyException("'{0}' may not be negative", new object[1]
        {
          (object) "[curyLineTotal]"
        })))
          throw new PXRowPersistingException(typeof (POOrder.curyLineTotal).Name, (object) null, "'{0}' may not be negative", new object[1]
          {
            (object) "curyLineTotal"
          });
      }
    }
    nullable = row.CuryDiscTot;
    Decimal num2 = Math.Abs(row.CuryLineTotal.GetValueOrDefault());
    if (nullable.GetValueOrDefault() > num2 & nullable.HasValue && sender.RaiseExceptionHandling<POOrder.curyDiscTot>(e.Row, (object) row.CuryDiscTot, (Exception) new PXSetPropertyException("The total amount of line and document discounts cannot exceed the Detail Total amount.", (PXErrorLevel) 4)))
      throw new PXRowPersistingException(typeof (POOrder.curyDiscTot).Name, (object) null, "The total amount of line and document discounts cannot exceed the Detail Total amount.");
  }

  protected virtual void POOrder_OrderDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    POOrder row = (POOrder) e.Row;
    if (this.IsVendorOrLocationChanged(sender, row) || ((PXSelectBase<POLine>) this.Transactions).Select(Array.Empty<object>()).Count <= 0 || ((PXGraph) this).IsMobile || ((PXGraph) this).IsContractBasedAPI)
      return;
    ((PXSelectBase<POOrder>) this.Document).Ask("Warning", "Changing of the purchase order date will reset the 'Requested' and 'Promised' dates for all order lines to new values. Do you want to continue?", (MessageButtons) 4, (MessageIcon) 2);
  }

  protected virtual void POOrder_OrderDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<POOrder.expectedDate>(e.Row);
  }

  protected virtual void POOrder_RetainageApply_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    POOrder row = (POOrder) e.Row;
    if (row == null)
      return;
    bool? newValue = (bool?) e.NewValue;
    bool? nullable = row.RetainageApply;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = newValue;
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue) || PXResultset<POLine>.op_Implicit(PXSelectBase<POLine, PXSelect<POLine, Where<POLine.orderType, Equal<Current<POOrder.orderType>>, And<POLine.orderNbr, Equal<Current<POOrder.orderNbr>>, And<POLine.retainagePct, NotEqual<decimal0>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>())) == null)
      return;
    if (6 == ((PXSelectBase<POOrder>) this.Document).Ask("Warning", "If you clear the Apply Retainage check box, the retainage amount and retainage percent will be set to zero. Do you want to proceed?", (MessageButtons) 4, (MessageIcon) 3))
    {
      foreach (PXResult<POLine> pxResult in ((PXSelectBase<POLine>) this.Transactions).Select(Array.Empty<object>()))
      {
        POLine poLine = PXResult<POLine>.op_Implicit(pxResult);
        Decimal? retainagePct = poLine.RetainagePct;
        Decimal num = 0M;
        if (!(retainagePct.GetValueOrDefault() == num & retainagePct.HasValue))
        {
          poLine.CuryRetainageAmt = new Decimal?(0M);
          poLine.RetainagePct = new Decimal?(0M);
          ((PXSelectBase<POLine>) this.Transactions).Update(poLine);
        }
      }
    }
    else
    {
      e.NewValue = (object) true;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void POOrder_ExpectedDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    POOrder row = (POOrder) e.Row;
    if (this.IsVendorOrLocationChanged(sender, row))
    {
      DateTime result;
      if (e.NewValue == null || !DateTime.TryParse(e.NewValue.ToString(), out result))
        return;
      DateTime? expectedDate1 = row.ExpectedDate;
      DateTime dateTime1 = result;
      if ((expectedDate1.HasValue ? (expectedDate1.GetValueOrDefault() != dateTime1 ? 1 : 0) : 1) == 0)
        return;
      PXCache pxCache = sender;
      POOrder poOrder = row;
      // ISSUE: variable of a boxed type
      __Boxed<DateTime?> expectedDate2 = (ValueType) row.ExpectedDate;
      object[] objArray = new object[2];
      DateTime dateTime2 = row.ExpectedDate.Value;
      objArray[0] = (object) dateTime2.Date;
      dateTime2 = result;
      objArray[1] = (object) dateTime2.Date;
      PXSetPropertyException propertyException = new PXSetPropertyException("The promised date has been changed from {0} to {1}.", (PXErrorLevel) 2, objArray);
      pxCache.RaiseExceptionHandling<POOrder.expectedDate>((object) poOrder, (object) expectedDate2, (Exception) propertyException);
    }
    else
    {
      if (((PXSelectBase<POLine>) this.Transactions).Select(Array.Empty<object>()).Count <= 0 || ((PXGraph) this).IsMobile || ((PXGraph) this).IsContractBasedAPI)
        return;
      ((PXSelectBase<POOrder>) this.Document).Ask("Warning", "Changing of the purchase order 'Promised on' date will reset 'Promised' dates for all it's details to their default values. Continue?", (MessageButtons) 4, (MessageIcon) 2);
    }
  }

  protected virtual void POOrder_ProjectID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is POOrder row) || !(row.OrderType == "PD"))
      return;
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowPersisting<POShipAddress> e)
  {
    if (e.Row == null)
      return;
    PXDefaultAttribute.SetPersistingCheck<POShipAddress.bAccountID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<POShipAddress>>) e).Cache, (object) e.Row, ((PXSelectBase<POOrder>) this.Document).Current?.OrderType == "PD" ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<POShipContact> e)
  {
    if (e.Row == null)
      return;
    PXDefaultAttribute.SetPersistingCheck<POShipContact.bAccountID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<POShipContact>>) e).Cache, (object) e.Row, ((PXSelectBase<POOrder>) this.Document).Current?.OrderType == "PD" ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDBDefault(typeof (POOrder.taxZoneID))]
  [PXUIField(DisplayName = "Vendor Tax Zone", Enabled = false)]
  protected virtual void POTaxTran_TaxZoneID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXParent(typeof (Select<POOrder, Where<POOrder.orderType, Equal<Current<RQRequisitionOrder.orderType>>, And<POOrder.orderNbr, Equal<Current<RQRequisitionOrder.orderNbr>>>>>))]
  protected virtual void RQRequisitionOrder_OrderNbr_CacheAttached(PXCache sender)
  {
  }

  protected virtual void POTaxTran_TaxZoneID_ExceptionHandling(
    PXCache sender,
    PXExceptionHandlingEventArgs e)
  {
    Exception exception = (Exception) (e.Exception as PXSetPropertyException);
    if (exception == null)
      return;
    ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<POOrder.taxZoneID>((object) ((PXSelectBase<POOrder>) this.Document).Current, (object) null, exception);
  }

  protected virtual void POTaxTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is POTaxTran))
      return;
    PXUIFieldAttribute.SetEnabled<POTaxTran.taxID>(sender, e.Row, sender.GetStatus(e.Row) == 2);
    PXUIFieldAttribute.SetEnabled<POTaxTran.curyExpenseAmt>(sender, e.Row, false);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<POOrder, POOrder.expirationDate> eventArgs)
  {
    if (!(eventArgs.Row?.OrderType == "BL"))
      return;
    DateTime? newValue = (DateTime?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POOrder, POOrder.expirationDate>, POOrder, object>) eventArgs).NewValue;
    DateTime? orderDate = eventArgs.Row.OrderDate;
    if ((newValue.HasValue & orderDate.HasValue ? (newValue.GetValueOrDefault() < orderDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<POOrder, POOrder.expirationDate>>) eventArgs).Cache.RaiseExceptionHandling<POOrder.expirationDate>((object) eventArgs.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POOrder, POOrder.expirationDate>, POOrder, object>) eventArgs).NewValue, (Exception) new PXSetPropertyException<POOrder.expirationDate>("The Expires On date is set to a date in the past.", (PXErrorLevel) 3));
  }

  public virtual void UpdateDocumentState(POOrder order)
  {
    ((PXSelectBase) this.Document).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase<POOrder>) this.Document).Search<POOrder.orderNbr>((object) order.OrderNbr, new object[1]
    {
      (object) order.OrderType
    });
    if (((PXSelectBase<POOrder>) this.Document).Current.Hold.GetValueOrDefault() || this.RaiseOrderEvents(((PXSelectBase<POOrder>) this.Document).Current, false))
      return;
    ((SelectedEntityEvent<POOrder>) PXEntityEventBase<POOrder>.Container<POOrder.Events>.Select((Expression<Func<POOrder.Events, PXEntityEvent<POOrder.Events>>>) (ev => ev.LinesReopened))).FireOn((PXGraph) this, ((PXSelectBase<POOrder>) this.Document).Current);
  }

  protected virtual bool RaiseOrderEvents(POOrder document)
  {
    return this.RaiseOrderEvents(document, true);
  }

  private bool RaiseOrderEvents(POOrder order, bool validateState)
  {
    if (CounterScope<POOrderEntry.SuppressOrderEventsScope>.Suppressed((PXGraph) this) || validateState && (order.Status != "N" || order.Hold.GetValueOrDefault()))
      return false;
    int? linesToCloseCntr = order.LinesToCloseCntr;
    int num1 = 0;
    if (linesToCloseCntr.GetValueOrDefault() == num1 & linesToCloseCntr.HasValue)
    {
      ((SelectedEntityEvent<POOrder>) PXEntityEventBase<POOrder>.Container<POOrder.Events>.Select((Expression<Func<POOrder.Events, PXEntityEvent<POOrder.Events>>>) (ev => ev.LinesClosed))).FireOn((PXGraph) this, order);
      return true;
    }
    int? linesToCompleteCntr = order.LinesToCompleteCntr;
    int num2 = 0;
    if (!(linesToCompleteCntr.GetValueOrDefault() == num2 & linesToCompleteCntr.HasValue))
      return false;
    ((SelectedEntityEvent<POOrder>) PXEntityEventBase<POOrder>.Container<POOrder.Events>.Select((Expression<Func<POOrder.Events, PXEntityEvent<POOrder.Events>>>) (ev => ev.LinesCompleted))).FireOn((PXGraph) this, order);
    return true;
  }

  [POCommitment]
  [PXDBGuid(false)]
  protected virtual void POLine_CommitmentID_CacheAttached(PXCache sender)
  {
  }

  [PXFormula(typeof (Selector<POLine.inventoryID, PX.Objects.IN.InventoryItem.kitItem>))]
  [PXMergeAttributes]
  public void POLine_IsKit_CacheAttached(PXCache sender)
  {
  }

  [PXFormula(typeof (Selector<POLine.inventoryID, PX.Objects.IN.InventoryItem.stkItem>))]
  [PXMergeAttributes]
  public void POLine_IsStockItem_CacheAttached(PXCache sender)
  {
  }

  [InterBranchRestrictor(typeof (Where2<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<POOrder.branchID>>, Or<Current<POOrder.orderType>, Equal<POOrderType.standardBlanket>>>))]
  [PXMergeAttributes]
  protected virtual void POLine_SiteID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXParent(typeof (Select<POLineR, Where<POLineR.orderType, Equal<Current<POLine.pOType>>, And<POLineR.orderType, Equal<POOrderType.blanket>, And<POLineR.orderNbr, Equal<Current<POLine.pONbr>>, And<POLineR.lineNbr, Equal<Current<POLine.pOLineNbr>>>>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<POLine.pOLineNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(null, typeof (SumCalc<POLineR.curyBLOrderedCost>))]
  protected virtual void _(PX.Data.Events.CacheAttached<POLine.curyExtCost> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDBDefault(typeof (POOrder.orderType))]
  protected virtual void _(PX.Data.Events.CacheAttached<POLine.orderType> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDBDefault(typeof (POOrder.orderNbr))]
  protected virtual void _(PX.Data.Events.CacheAttached<POLine.orderNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDBDefault(typeof (POOrder.vendorID))]
  protected virtual void _(PX.Data.Events.CacheAttached<POLine.vendorID> e)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXDBDefault(typeof (POOrder.orderDate))]
  protected virtual void _(PX.Data.Events.CacheAttached<POLine.orderDate> e)
  {
  }

  [PXMergeAttributes]
  [POTax(typeof (POOrder), typeof (POTax), typeof (POTaxTran), Inventory = typeof (POLine.inventoryID), UOM = typeof (POLine.uOM), LineQty = typeof (POLine.orderQty))]
  [POUnbilledTax(typeof (POOrder), typeof (POTax), typeof (POTaxTran), Inventory = typeof (POLine.inventoryID), UOM = typeof (POLine.uOM), LineQty = typeof (POLine.unbilledQty))]
  [PORetainedTax(typeof (POOrder), typeof (POTax), typeof (POTaxTran))]
  protected virtual void _(PX.Data.Events.CacheAttached<POLine.taxCategoryID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXDBQuantityAttribute), "DecimalVerifyUnits", InventoryUnitType.PurchaseUnit)]
  [PXCustomizeBaseAttribute(typeof (PXDBQuantityAttribute), "ConvertToDecimalVerifyUnits", false)]
  protected virtual void _(PX.Data.Events.CacheAttached<POLine.orderQty> e)
  {
  }

  protected virtual void LessThanZeroVerifying<TField>(Decimal value) where TField : IBqlField
  {
    if (value < 0M)
      throw new PXSetPropertyException<TField>("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
      {
        (object) 0.ToString()
      });
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<POLine, POLine.orderQty> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLine, POLine.orderQty>, POLine, object>) e).NewValue == null)
      return;
    this.LessThanZeroVerifying<POLine.orderQty>((Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLine, POLine.orderQty>, POLine, object>) e).NewValue);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<POLine, POLine.orderedQty> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLine, POLine.orderedQty>, POLine, object>) e).NewValue == null)
      return;
    this.LessThanZeroVerifying<POLine.orderedQty>((Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLine, POLine.orderedQty>, POLine, object>) e).NewValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<POLine, POLine.completedQty> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLine, POLine.completedQty>, POLine, object>) e).NewValue == null)
      return;
    this.LessThanZeroVerifying<POLine.completedQty>((Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLine, POLine.completedQty>, POLine, object>) e).NewValue);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<POLine, POLine.billedQty> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLine, POLine.billedQty>, POLine, object>) e).NewValue == null)
      return;
    this.LessThanZeroVerifying<POLine.billedQty>((Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLine, POLine.billedQty>, POLine, object>) e).NewValue);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<POLine, POLine.reqPrepaidQty> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLine, POLine.reqPrepaidQty>, POLine, object>) e).NewValue == null)
      return;
    this.LessThanZeroVerifying<POLine.reqPrepaidQty>((Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLine, POLine.reqPrepaidQty>, POLine, object>) e).NewValue);
  }

  protected virtual void POLine_OrderQty_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    POLine row = (POLine) e.Row;
    if (row == null)
      return;
    e.NewValue = (object) (row.LineType == "FT" ? 1M : 0M);
  }

  protected object GetValue<Field>(object data) where Field : IBqlField
  {
    return data == null ? (object) null : ((PXGraph) this).Caches[BqlCommand.GetItemType(typeof (Field))].GetValue(data, typeof (Field).Name);
  }

  protected virtual void POLine_OrderQty_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is POLine row))
      return;
    Decimal? orderQty = row.OrderQty;
    Decimal num = 0M;
    if (orderQty.GetValueOrDefault() == num & orderQty.HasValue)
    {
      sender.SetValueExt<POLine.curyDiscAmt>((object) row, (object) 0M);
      sender.SetValueExt<POLine.discPct>((object) row, (object) 0M);
    }
    else
      sender.SetDefaultExt<POLine.curyUnitCost>(e.Row);
  }

  protected virtual void POLine_POAccrualAcctID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    POLine row = (POLine) e.Row;
    if (row == null)
      return;
    if (this.IsAccrualAccountEnabled(row))
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
      if (inventoryItem == null)
        return;
      bool? nullable = inventoryItem.StkItem;
      if (!nullable.GetValueOrDefault())
      {
        nullable = inventoryItem.NonStockReceipt;
        if (!nullable.GetValueOrDefault())
          return;
      }
      INPostClass postclass = INPostClass.PK.Find((PXGraph) this, inventoryItem.PostClassID);
      if (postclass == null)
        return;
      PX.Objects.IN.INSite site = PX.Objects.IN.INSite.PK.Find((PXGraph) this, row.SiteID);
      PX.Objects.AP.Vendor vendor1;
      if (PXAccess.FeatureInstalled<FeaturesSet.vendorRelations>())
      {
        int? vendorId = ((PXSelectBase<POOrder>) this.Document).Current.VendorID;
        int? payToVendorId = ((PXSelectBase<POOrder>) this.Document).Current.PayToVendorID;
        if (!(vendorId.GetValueOrDefault() == payToVendorId.GetValueOrDefault() & vendorId.HasValue == payToVendorId.HasValue))
        {
          vendor1 = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<POOrder.payToVendorID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[0], Array.Empty<object>()));
          goto label_11;
        }
      }
      vendor1 = ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current;
label_11:
      PX.Objects.AP.Vendor vendor2 = vendor1;
      e.NewValue = (object) INReleaseProcess.GetPOAccrualAcctID<INPostClass.pOAccrualAcctID>((PXGraph) this, postclass.POAccrualAcctDefault, inventoryItem, site, postclass, vendor2);
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

  protected virtual void POLine_POAccrualSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    POLine row = (POLine) e.Row;
    if (row == null)
      return;
    if (this.IsAccrualAccountEnabled(row))
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
      if (inventoryItem == null)
        return;
      bool? nullable = inventoryItem.StkItem;
      if (!nullable.GetValueOrDefault())
      {
        nullable = inventoryItem.NonStockReceipt;
        if (!nullable.GetValueOrDefault())
          return;
      }
      INPostClass postclass = INPostClass.PK.Find((PXGraph) this, inventoryItem.PostClassID);
      if (postclass == null)
        return;
      PX.Objects.IN.INSite site = PX.Objects.IN.INSite.PK.Find((PXGraph) this, row.SiteID);
      try
      {
        PX.Objects.AP.Vendor vendor1;
        if (PXAccess.FeatureInstalled<FeaturesSet.vendorRelations>())
        {
          int? vendorId = ((PXSelectBase<POOrder>) this.Document).Current.VendorID;
          int? payToVendorId = ((PXSelectBase<POOrder>) this.Document).Current.PayToVendorID;
          if (!(vendorId.GetValueOrDefault() == payToVendorId.GetValueOrDefault() & vendorId.HasValue == payToVendorId.HasValue))
          {
            vendor1 = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<POOrder.payToVendorID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[0], Array.Empty<object>()));
            goto label_12;
          }
        }
        vendor1 = ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current;
label_12:
        PX.Objects.AP.Vendor vendor2 = vendor1;
        e.NewValue = (object) INReleaseProcess.GetPOAccrualSubID<INPostClass.pOAccrualSubID>((PXGraph) this, postclass.POAccrualAcctDefault, postclass.POAccrualSubMask, inventoryItem, site, postclass, vendor2);
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

  protected virtual void POLine_POAccrualAcctID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    POLine row = (POLine) e.Row;
    if (row == null || this.IsAccrualAccountEnabled(row) || !sender.Graph.IsImport)
      return;
    row.POAccrualAcctID = new int?();
  }

  protected virtual void POLine_POAccrualSubID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    POLine row = (POLine) e.Row;
    if (row == null || this.IsAccrualAccountEnabled(row) || !sender.Graph.IsImport)
      return;
    row.POAccrualSubID = new int?();
  }

  protected virtual bool IsExpenseAccountEnabled(POLine line)
  {
    if (!(line.LineType != "DN"))
      return false;
    return !POLineType.IsStock(line.LineType) || line.OrderType == "PD";
  }

  protected virtual bool IsAccrualAccountEnabled(POLine line)
  {
    if (!POLineType.UsePOAccrual(line.LineType))
      return false;
    return line.OrderType != "PD" || line.DropshipExpenseRecording != "B";
  }

  protected virtual bool IsAccrualAccountRequired(POLine line)
  {
    return line.POAccrualType == "O" && this.IsAccrualAccountEnabled(line);
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

  protected virtual void POLine_ExpenseAcctID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    POLine row = (POLine) e.Row;
    if (row == null)
      return;
    switch (row.LineType)
    {
      case "DN":
        ((CancelEventArgs) e).Cancel = true;
        return;
      case "FT":
        if (((PXSelectBase<POOrder>) this.Document).Current != null && ((PXSelectBase<POOrder>) this.Document).Current.OrderType == "PD")
        {
          PMProject pmProject = PMProject.PK.Find((PXGraph) this, row.ProjectID);
          if (pmProject != null && EnumerableExtensions.IsIn<string>(pmProject.DropshipExpenseAccountSource, "T", "P"))
            break;
        }
        PX.Objects.CS.Carrier data1 = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current.VCarrierID);
        e.NewValue = this.GetValue<PX.Objects.CS.Carrier.freightExpenseAcctID>((object) data1) ?? (object) ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current.FreightExpenseAcctID;
        ((CancelEventArgs) e).Cancel = true;
        return;
    }
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
    if (((PXSelectBase<POOrder>) this.Document).Current != null && ((PXSelectBase<POOrder>) this.Document).Current.OrderType == "PD")
    {
      PMProject data2 = PMProject.PK.Find((PXGraph) this, row.ProjectID);
      if (data2 != null)
      {
        switch (data2.DropshipExpenseAccountSource)
        {
          case "T":
            PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, row.ProjectID, row.TaskID);
            if (dirty != null)
            {
              e.NewValue = this.GetValue<PMTask.defaultExpenseAccountID>((object) dirty);
              if (e.NewValue != null)
              {
                ((CancelEventArgs) e).Cancel = true;
                break;
              }
              goto case "P";
            }
            break;
          case "P":
            e.NewValue = this.GetValue<PMProject.defaultExpenseAccountID>((object) data2);
            if (e.NewValue != null)
            {
              ((CancelEventArgs) e).Cancel = true;
              break;
            }
            goto case "O";
          case "O":
            if (inventoryItem != null)
            {
              this.SetExpenseAccountUsingDefaultRules(e, row, inventoryItem, true);
              break;
            }
            e.NewValue = (object) ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current.VExpenseAcctID;
            break;
        }
      }
    }
    else if (inventoryItem != null)
    {
      if (row != null)
      {
        bool? nullable = inventoryItem.StkItem;
        if (!nullable.GetValueOrDefault())
        {
          nullable = row.AccrueCost;
          if (nullable.GetValueOrDefault())
          {
            this.SetExpenseAccount(sender, e, inventoryItem);
            goto label_26;
          }
        }
      }
      this.SetExpenseAccountUsingDefaultRules(e, row, inventoryItem, false);
    }
    else
      e.NewValue = (object) (int?) ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current?.VExpenseAcctID;
label_26:
    if (e.NewValue == null)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  private void SetExpenseAccountUsingDefaultRules(
    PXFieldDefaultingEventArgs e,
    POLine row,
    PX.Objects.IN.InventoryItem item,
    bool setExpenseAccountForStockItems)
  {
    INPostClass postclass = INPostClass.PK.Find((PXGraph) this, item.PostClassID);
    APSetup current1 = ((PXSelectBase<APSetup>) this.apsetup).Current;
    bool flag = setExpenseAccountForStockItems ? POLineType.IsNonStock(row.LineType) || POLineType.IsStock(row.LineType) : POLineType.IsNonStock(row.LineType);
    if (flag && !POLineType.IsService(row.LineType) && postclass != null)
    {
      PX.Objects.AP.Vendor current2 = ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current;
      if ((current2 != null ? (!current2.IsBranch.GetValueOrDefault() ? 1 : 0) : 1) != 0 || current1?.IntercompanyExpenseAccountDefault == "I" || POLineType.IsDropShip(row.LineType))
      {
        PX.Objects.IN.INSite site = PX.Objects.IN.INSite.PK.Find((PXGraph) this, row.SiteID);
        try
        {
          PMProject project;
          PMTask task;
          PMProjectHelper.TryToGetProjectAndTask((PXGraph) this, (int?) row?.ProjectID, (int?) row?.TaskID, out project, out task);
          e.NewValue = (object) INReleaseProcess.GetAcctID<INPostClass.cOGSAcctID>((PXGraph) this, postclass.COGSAcctDefault, InventoryAccountServiceHelper.Params(item, site, postclass, (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task));
          return;
        }
        catch (PXMaskArgumentException ex)
        {
          return;
        }
      }
    }
    if (!flag)
      return;
    PX.Objects.AP.Vendor current3 = ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current;
    if ((current3 != null ? (current3.IsBranch.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      switch (current1?.IntercompanyExpenseAccountDefault)
      {
        case "L":
          e.NewValue = (object) (int?) ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current?.VExpenseAcctID;
          break;
        case "I":
          e.NewValue = (object) item.COGSAcctID;
          break;
      }
    }
    else
      e.NewValue = (object) (item.COGSAcctID ?? ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current.VExpenseAcctID);
  }

  protected virtual void POLine_ExpenseAcctID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    POLine row = (POLine) e.Row;
    if (row.ProjectID.HasValue || !(row.OrderType != "PD"))
      return;
    sender.SetDefaultExt<POLine.projectID>(e.Row);
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

  protected virtual void POLine_ExpenseSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    POLine row = (POLine) e.Row;
    if (row == null)
      return;
    switch (row.LineType)
    {
      case "DN":
        break;
      case "FT":
        PX.Objects.CS.Carrier data1 = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current.VCarrierID);
        e.NewValue = this.GetValue<PX.Objects.CS.Carrier.freightExpenseSubID>((object) data1) ?? (object) ((PXSelectBase<PX.Objects.PO.POSetup>) this.POSetup).Current.FreightExpenseSubID;
        ((CancelEventArgs) e).Cancel = true;
        break;
      default:
        PX.Objects.IN.InventoryItem data2 = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
        if (data2 != null)
        {
          if (((PXSelectBase<POOrder>) this.Document).Current != null && ((PXSelectBase<POOrder>) this.Document).Current.OrderType == "PD")
          {
            PMProject data3 = PMProject.PK.Find((PXGraph) this, row.ProjectID);
            PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, row.ProjectID, row.TaskID);
            INPostClass data4 = INPostClass.PK.Find((PXGraph) this, data2.PostClassID);
            if (data2 != null && data3 != null)
            {
              if (dirty == null)
              {
                string dropshipExpenseSubMask = data3.DropshipExpenseSubMask;
                if ((dropshipExpenseSubMask != null ? (!dropshipExpenseSubMask.Contains("T") ? 1 : 0) : 0) == 0)
                  goto label_35;
              }
              object obj1 = this.GetValue<PX.Objects.IN.InventoryItem.cOGSSubID>((object) data2);
              object obj2 = this.GetValue<INPostClass.cOGSSubID>((object) data4);
              object obj3 = this.GetValue<PMProject.defaultExpenseSubID>((object) data3);
              object obj4 = this.GetValue<PMTask.defaultExpenseSubID>((object) dirty);
              object obj5 = (object) DropshipExpenseSubAccountMaskAttribute.MakeSub<PMProject.dropshipExpenseSubMask>((PXGraph) this, data3.DropshipExpenseSubMask, new object[4]
              {
                obj1,
                obj2,
                obj3,
                obj4
              }, new System.Type[4]
              {
                typeof (PX.Objects.IN.InventoryItem.cOGSSubID),
                typeof (INPostClass.cOGSSubID),
                typeof (PMProject.defaultExpenseSubID),
                typeof (PMTask.defaultExpenseSubID)
              });
              sender.RaiseFieldUpdating<POReceiptLine.expenseSubID>(e.Row, ref obj5);
              e.NewValue = obj5;
            }
          }
          else
          {
            INPostClass postclass;
            if (POLineType.IsNonStock(row.LineType) && !POLineType.IsService(row.LineType) && (postclass = INPostClass.PK.Find((PXGraph) this, data2.PostClassID)) != null)
            {
              PX.Objects.IN.INSite site = PX.Objects.IN.INSite.PK.Find((PXGraph) this, row.SiteID);
              try
              {
                PMProject project;
                PMTask task;
                PMProjectHelper.TryToGetProjectAndTask((PXGraph) this, (int?) row?.ProjectID, (int?) row?.TaskID, out project, out task);
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
              POOrder current = ((PXSelectBase<POOrder>) this.Document).Current;
              PX.Objects.EP.EPEmployee data5 = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.defContactID, Equal<Required<PX.Objects.EP.EPEmployee.defContactID>>>>.Config>.Select((PXGraph) this, new object[1]
              {
                (object) current.OwnerID
              }));
              PX.Objects.CR.Standalone.Location data6 = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelectJoin<PX.Objects.CR.Standalone.Location, InnerJoin<BAccountR, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<PX.Objects.GL.Branch, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<POLine.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
              {
                (object) row.BranchID
              }));
              PMProject pmProject = PMProject.PK.Find((PXGraph) this, row.ProjectID ?? ProjectDefaultAttribute.NonProject());
              int? defaultExpenseSubId = (int?) PMTask.PK.FindDirty((PXGraph) this, row.ProjectID, row.TaskID)?.DefaultExpenseSubID;
              PX.Objects.CR.Location data7 = ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current;
              if (PXAccess.FeatureInstalled<FeaturesSet.vendorRelations>())
              {
                int? nullable = current.PayToVendorID;
                if (nullable.HasValue)
                {
                  nullable = current.VendorID;
                  int? payToVendorId = current.PayToVendorID;
                  if (!(nullable.GetValueOrDefault() == payToVendorId.GetValueOrDefault() & nullable.HasValue == payToVendorId.HasValue))
                    data7 = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.Location.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>, And<PX.Objects.CR.Location.locationID, Equal<PX.Objects.CR.BAccount.defLocationID>>>>, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<POOrder.payToVendorID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
                    {
                      (object) current
                    }, Array.Empty<object>()));
                }
              }
              object obj;
              if (row != null)
              {
                bool? nullable = data2.StkItem;
                if (!nullable.GetValueOrDefault())
                {
                  nullable = row.AccrueCost;
                  if (nullable.GetValueOrDefault())
                  {
                    obj = this.GetExpenseSub(sender, e, data2);
                    goto label_25;
                  }
                }
              }
              obj = (object) PX.Objects.AP.SubAccountMaskAttribute.MakeSub<APSetup.expenseSubMask>((PXGraph) this, ((PXSelectBase<APSetup>) this.apsetup).Current.ExpenseSubMask, new object[6]
              {
                this.GetValue<PX.Objects.CR.Location.vExpenseSubID>((object) data7),
                e.NewValue ?? this.GetValue<PX.Objects.IN.InventoryItem.cOGSSubID>((object) data2),
                this.GetValue<PX.Objects.EP.EPEmployee.expenseSubID>((object) data5),
                this.GetValue<PX.Objects.CR.Standalone.Location.cMPExpenseSubID>((object) data6),
                (object) pmProject.DefaultExpenseSubID,
                (object) defaultExpenseSubId
              }, new System.Type[6]
              {
                typeof (PX.Objects.CR.Location.vExpenseSubID),
                typeof (PX.Objects.IN.InventoryItem.cOGSSubID),
                typeof (PX.Objects.EP.EPEmployee.expenseSubID),
                typeof (PX.Objects.CR.Standalone.Location.cMPExpenseSubID),
                typeof (PMProject.defaultExpenseSubID),
                typeof (PMTask.defaultExpenseSubID)
              });
label_25:
              sender.RaiseFieldUpdating<POReceiptLine.expenseSubID>(e.Row, ref obj);
              e.NewValue = obj;
            }
            else
              e.NewValue = (object) null;
          }
        }
        else
        {
          string mask = ((PXSelectBase<APSetup>) this.apsetup).Current.ExpenseSubMask.Replace("I", "L");
          POOrder current = ((PXSelectBase<POOrder>) this.Document).Current;
          PX.Objects.EP.EPEmployee data8 = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.defContactID, Equal<Required<PX.Objects.EP.EPEmployee.defContactID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) current.OwnerID
          }));
          PX.Objects.CR.Standalone.Location data9 = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelectJoin<PX.Objects.CR.Standalone.Location, InnerJoin<BAccountR, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<PX.Objects.GL.Branch, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<POLine.branchID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) row.BranchID
          }));
          int? nullable1 = row.ProjectID;
          PMProject pmProject = PMProject.PK.Find((PXGraph) this, nullable1 ?? ProjectDefaultAttribute.NonProject());
          PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, row.ProjectID, row.TaskID);
          int? nullable2;
          if (dirty == null)
          {
            nullable1 = new int?();
            nullable2 = nullable1;
          }
          else
            nullable2 = dirty.DefaultExpenseSubID;
          int? nullable3 = nullable2;
          PX.Objects.CR.Location data10 = ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current;
          if (PXAccess.FeatureInstalled<FeaturesSet.vendorRelations>())
          {
            nullable1 = current.PayToVendorID;
            if (nullable1.HasValue)
            {
              nullable1 = current.VendorID;
              int? payToVendorId = current.PayToVendorID;
              if (!(nullable1.GetValueOrDefault() == payToVendorId.GetValueOrDefault() & nullable1.HasValue == payToVendorId.HasValue))
                data10 = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.Location.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>, And<PX.Objects.CR.Location.locationID, Equal<PX.Objects.CR.BAccount.defLocationID>>>>, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<POOrder.payToVendorID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
                {
                  (object) current
                }, Array.Empty<object>()));
            }
          }
          object obj = (object) PX.Objects.AP.SubAccountMaskAttribute.MakeSub<APSetup.expenseSubMask>((PXGraph) this, mask, new object[6]
          {
            this.GetValue<PX.Objects.CR.Location.vExpenseSubID>((object) data10),
            this.GetValue<PX.Objects.CR.Location.vExpenseSubID>((object) data10),
            this.GetValue<PX.Objects.EP.EPEmployee.expenseSubID>((object) data8),
            this.GetValue<PX.Objects.CR.Standalone.Location.cMPExpenseSubID>((object) data9),
            (object) pmProject.DefaultExpenseSubID,
            (object) nullable3
          }, new System.Type[6]
          {
            typeof (PX.Objects.CR.Location.vExpenseSubID),
            typeof (PX.Objects.CR.Location.vExpenseSubID),
            typeof (PX.Objects.EP.EPEmployee.expenseSubID),
            typeof (PX.Objects.CR.Standalone.Location.cMPExpenseSubID),
            typeof (PMProject.defaultExpenseSubID),
            typeof (PMTask.defaultExpenseSubID)
          });
          sender.RaiseFieldUpdating<POReceiptLine.expenseSubID>(e.Row, ref obj);
          e.NewValue = obj;
        }
label_35:
        ((CancelEventArgs) e).Cancel = true;
        break;
    }
  }

  protected virtual void POLine_TaxCategoryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (TaxBaseAttribute.GetTaxCalc<POLine.taxCategoryID>(sender, e.Row) == TaxCalc.Calc && ((PXSelectBase<PX.Objects.TX.TaxZone>) this.taxzone).Current != null && !string.IsNullOrEmpty(((PXSelectBase<PX.Objects.TX.TaxZone>) this.taxzone).Current.DfltTaxCategoryID) && !((POLine) e.Row).InventoryID.HasValue)
      e.NewValue = (object) ((PXSelectBase<PX.Objects.TX.TaxZone>) this.taxzone).Current.DfltTaxCategoryID;
    if (this.vendor == null || ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current == null || !((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current.TaxAgency.Value)
      return;
    ((POLine) e.Row).TaxCategoryID = string.Empty;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void POLine_UnitCost_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((POLine) e.Row).InventoryID.HasValue)
      return;
    e.NewValue = (object) 0M;
  }

  protected virtual void POLine_CuryUnitCost_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.skipCostDefaulting)
      return;
    POLine row = e.Row as POLine;
    POOrder current = ((PXSelectBase<POOrder>) this.Document).Current;
    if (row != null && row.ManualPrice.GetValueOrDefault())
    {
      e.NewValue = (object) row.CuryUnitCost.GetValueOrDefault();
    }
    else
    {
      int? nullable1;
      int num1;
      if (row == null)
      {
        num1 = 1;
      }
      else
      {
        nullable1 = row.InventoryID;
        num1 = !nullable1.HasValue ? 1 : 0;
      }
      if (num1 != 0)
        return;
      int num2;
      if (current == null)
      {
        num2 = 1;
      }
      else
      {
        nullable1 = current.VendorID;
        num2 = !nullable1.HasValue ? 1 : 0;
      }
      if (num2 != 0)
        return;
      Decimal? nullable2 = new Decimal?();
      if (row.UOM != null)
      {
        DateTime date = ((PXSelectBase<POOrder>) this.Document).Current.OrderDate.Value;
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ((PXGraph) this).FindImplementation<IPXCurrencyHelper>().GetCurrencyInfo(current.CuryInfoID);
        nullable2 = APVendorPriceMaint.CalculateUnitCost(sender, row.VendorID, current.VendorLocationID, row.InventoryID, row.SiteID, currencyInfo.GetCM(), row.UOM, row.OrderQty, date, row.CuryUnitCost);
        e.NewValue = (object) nullable2;
      }
      if (!nullable2.HasValue)
        e.NewValue = (object) POItemCostManager.Fetch<POLine.inventoryID, POLine.curyInfoID>(sender.Graph, (object) row, current.VendorID, current.VendorLocationID, current.OrderDate, current.CuryID, row.InventoryID, row.SubItemID, row.SiteID, row.UOM);
      APVendorPriceMaint.CheckNewUnitCost<POLine, POLine.curyUnitCost>(sender, row, e.NewValue);
    }
  }

  protected virtual void POLine_ManualPrice_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is POLine row))
      return;
    sender.SetDefaultExt<POLine.curyUnitCost>((object) row);
  }

  protected virtual void POLine_CuryUnitCost_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (this.IsNegativeCostStockItem((POLine) e.Row, (Decimal?) e.NewValue))
      throw new PXSetPropertyException("A value for the Unit Cost must not be negative for Stock Items");
  }

  protected virtual bool IsNegativeCostStockItem(POLine row, Decimal? value)
  {
    Decimal? nullable = value;
    Decimal num = 0M;
    if (!(nullable.GetValueOrDefault() < num & nullable.HasValue) || row.LineType == null)
      return false;
    return EnumerableExtensions.IsNotIn<string>(row.LineType, "NS", "SV", "MC", "FT", "NP", new string[4]
    {
      "PN",
      "NO",
      "NF",
      "NM"
    });
  }

  protected virtual void POLine_PromisedDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is POLine row) || !row.InventoryID.HasValue)
      return;
    PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
    POOrder current = ((PXSelectBase<POOrder>) this.Document).Current;
    PXResult<PX.Objects.CR.Standalone.Location, POVendorInventory> pxResult = (PXResult<PX.Objects.CR.Standalone.Location, POVendorInventory>) PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelectJoin<PX.Objects.CR.Standalone.Location, LeftJoin<POVendorInventory, On<POVendorInventory.inventoryID, Equal<Required<POLine.inventoryID>>, And<POVendorInventory.subItemID, Equal<Required<POLine.subItemID>>, And<POVendorInventory.vendorID, Equal<PX.Objects.CR.Standalone.Location.bAccountID>, And<Where<POVendorInventory.vendorLocationID, Equal<PX.Objects.CR.Standalone.Location.locationID>, Or<POVendorInventory.vendorLocationID, IsNull>>>>>>>, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Required<POLine.vendorID>>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Required<POLine.vendorLocationID>>>>, OrderBy<Desc<POVendorInventory.vendorLocationID, Asc<PX.Objects.CR.Standalone.Location.locationID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[4]
    {
      (object) row.InventoryID,
      (object) row.SubItemID,
      (object) current.VendorID,
      (object) current.VendorLocationID
    }));
    if (pxResult == null)
      return;
    PX.Objects.CR.Standalone.Location location = PXResult<PX.Objects.CR.Standalone.Location, POVendorInventory>.op_Implicit(pxResult);
    POVendorInventory poVendorInventory = PXResult<PX.Objects.CR.Standalone.Location, POVendorInventory>.op_Implicit(pxResult);
    if (!current.ExpectedDate.HasValue)
    {
      PXFieldDefaultingEventArgs defaultingEventArgs = e;
      DateTime dateTime = current.OrderDate.Value;
      ref DateTime local1 = ref dateTime;
      short? nullable = location.VLeadTime;
      int valueOrDefault1 = (int) nullable.GetValueOrDefault();
      nullable = poVendorInventory.AddLeadTimeDays;
      int valueOrDefault2 = (int) nullable.GetValueOrDefault();
      double num = (double) (valueOrDefault1 + valueOrDefault2);
      // ISSUE: variable of a boxed type
      __Boxed<DateTime> local2 = (ValueType) local1.AddDays(num);
      defaultingEventArgs.NewValue = (object) local2;
    }
    else
      e.NewValue = (object) current.ExpectedDate.Value.AddDays((double) poVendorInventory.AddLeadTimeDays.GetValueOrDefault());
  }

  protected virtual void POLine_LineType_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<POLine.orderQty>(e.Row);
    sender.SetDefaultExt<POLine.expenseAcctID>(e.Row);
    sender.SetDefaultExt<POLine.expenseSubID>(e.Row);
    sender.SetDefaultExt<POLine.pOAccrualAcctID>(e.Row);
    sender.SetDefaultExt<POLine.pOAccrualSubID>(e.Row);
  }

  protected virtual void POLine_UOM_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<POLine.unitCost>(e.Row);
    sender.SetDefaultExt<POLine.curyUnitCost>(e.Row);
    sender.SetDefaultExt<POLine.promisedDate>(e.Row);
    sender.SetValue<POLine.unitCost>(e.Row, (object) null);
  }

  /// <summary>
  /// Gets a value indicating whether to skip the <see cref="P:PX.Objects.PO.POLine.CuryUnitCost" />'s field defaulting event. Used by PriceUnits customization for Lexware
  /// </summary>
  public bool SkipCostDefaulting => this.skipCostDefaulting;

  [PopupMessage]
  [PXMergeAttributes]
  protected virtual void POLine_InventoryID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void POLine_VendorID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is POLine))
      return;
    sender.SetDefaultExt<POLine.rcptQtyMin>(e.Row);
    sender.SetDefaultExt<POLine.rcptQtyMax>(e.Row);
    sender.SetDefaultExt<POLine.rcptQtyThreshold>(e.Row);
    sender.SetDefaultExt<POLine.rcptQtyAction>(e.Row);
    sender.SetDefaultExt<POLine.expenseAcctID>(e.Row);
    try
    {
      sender.SetDefaultExt<POLine.expenseSubID>(e.Row);
    }
    catch (PXSetPropertyException ex)
    {
      sender.SetValue<POLine.expenseSubID>(e.Row, (object) null);
    }
    sender.SetDefaultExt<POLine.pOAccrualAcctID>(e.Row);
    try
    {
      sender.SetDefaultExt<POLine.pOAccrualSubID>(e.Row);
    }
    catch (PXSetPropertyException ex)
    {
      sender.SetValue<POLine.pOAccrualSubID>(e.Row, (object) null);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<POLine, POLine.inventoryID> e)
  {
    if (e.Row != null && this.IsItemReceivedOrAPDocCreated(e.Row))
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLine, POLine.inventoryID>>) e).Cancel = true;
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLine, POLine.inventoryID>, POLine, object>) e).NewValue = (object) PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, (int?) e.OldValue)?.InventoryCD;
      throw new PXSetPropertyException("The {0} item cannot be changed because it has been added to at least one related prepayment request, AP bill, or purchase receipt.", (PXErrorLevel) 4, new object[1]
      {
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLine, POLine.inventoryID>, POLine, object>) e).NewValue
      });
    }
    POLine relatedPOLine;
    if (e.Row != null && e.Row.OrderType == "BL" && e.OldValue != ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLine, POLine.inventoryID>, POLine, object>) e).NewValue && this.BlanketPOLineIsReferencedInPOOrder(e.Row, out relatedPOLine))
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLine, POLine.inventoryID>>) e).Cancel = true;
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLine, POLine.inventoryID>, POLine, object>) e).NewValue = (object) PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, e.Row.InventoryID)?.InventoryCD;
      throw new PXSetPropertyException("The {0} item cannot be changed because it is specified in a line of the {1} purchase order associated with this blanket order.", (PXErrorLevel) 4, new object[2]
      {
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLine, POLine.inventoryID>, POLine, object>) e).NewValue,
        (object) relatedPOLine.OrderNbr
      });
    }
  }

  protected virtual void POLine_InventoryID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.skipCostDefaulting = true;
    sender.SetDefaultExt<POLine.accrueCost>(e.Row);
    sender.SetDefaultExt<POLine.processNonStockAsServiceViaPR>(e.Row);
    sender.SetDefaultExt<POLine.vendorID>(e.Row);
    sender.SetDefaultExt<POLine.subItemID>(e.Row);
    sender.SetDefaultExt<POLine.siteID>(e.Row);
    sender.SetDefaultExt<POLine.expenseAcctID>(e.Row);
    sender.SetDefaultExt<POLine.expenseSubID>(e.Row);
    sender.SetDefaultExt<POLine.pOAccrualAcctID>(e.Row);
    sender.SetDefaultExt<POLine.pOAccrualSubID>(e.Row);
    sender.SetDefaultExt<POLine.taxCategoryID>(e.Row);
    sender.SetDefaultExt<POLine.uOM>(e.Row);
    sender.SetDefaultExt<POLine.unitCost>(e.Row);
    this.skipCostDefaulting = false;
    sender.SetDefaultExt<POLine.curyUnitCost>(e.Row);
    sender.SetDefaultExt<POLine.promisedDate>(e.Row);
    sender.SetValue<POLine.unitCost>(e.Row, (object) null);
    sender.SetDefaultExt<POLine.siteID>(e.Row);
    sender.SetDefaultExt<POLine.unitWeight>(e.Row);
    sender.SetDefaultExt<POLine.unitVolume>(e.Row);
    sender.SetDefaultExt<POLine.costCodeID>(e.Row);
    if (!(e.Row is POLine row))
      return;
    row.POAccrualType = (string) PXFormulaAttribute.Evaluate<POLine.pOAccrualType>(((PXSelectBase) this.Transactions).Cache, (object) row);
    PX.Objects.IN.InventoryItem inventoryItem;
    if ((inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID)) != null)
      row.TranDesc = PXDBLocalizableStringAttribute.GetTranslation(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItem, "Descr", ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current?.LocaleName);
    if (!(row.POType == "BL") || row.PONbr == null || e.OldValue == null)
      return;
    int? inventoryId = row.InventoryID;
    int? oldValue = (int?) e.OldValue;
    if (inventoryId.GetValueOrDefault() == oldValue.GetValueOrDefault() & inventoryId.HasValue == oldValue.HasValue)
      return;
    string poNbr = row.PONbr;
    sender.SetValueExt<POLine.pOLineNbr>((object) row, (object) null);
    sender.SetValueExt<POLine.pONbr>((object) row, (object) null);
    sender.SetValueExt<POLine.pOType>((object) row, (object) null);
    sender.RaiseExceptionHandling<POLine.inventoryID>((object) row, (object) row.InventoryID, (Exception) new PXSetPropertyException("This line is no longer linked to the {0} blanket PO because the inventory ID was changed.", (PXErrorLevel) 2, new object[1]
    {
      (object) poNbr
    }));
  }

  protected virtual void POLine_ProjectID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if ((e.Row is POLine row ? (!row.ProjectID.HasValue ? 1 : 0) : 1) != 0 || ((PXGraph) this).IsImport)
      return;
    sender.SetDefaultExt<POLine.expenseAcctID>((object) row);
    sender.SetDefaultExt<POLine.expenseSubID>((object) row);
  }

  protected virtual void POLine_TaskID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    int? nullable;
    int num1;
    if (!(e.Row is POLine row))
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
    sender.SetDefaultExt<POLine.expenseAcctID>((object) row);
    sender.SetDefaultExt<POLine.expenseSubID>((object) row);
    sender.SetDefaultExt<POLine.costCodeID>(e.Row);
  }

  protected virtual void POLine_TaskID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is POLine row) || !(e.NewValue is int))
      return;
    PMProject pmProject = PMProject.PK.Find((PXGraph) this, row.ProjectID);
    if (e.NewValue == null || !POLineType.IsStock(row.LineType))
      return;
    int? nullable1 = row.SiteID;
    if (!nullable1.HasValue || !(row.OrderType != "PD") || !(pmProject?.AccountingMode == "L"))
      return;
    HashSet<int> intSet1 = new HashSet<int>();
    HashSet<int> source = new HashSet<int>();
    foreach (PXResult<INLocation> pxResult in PXSelectBase<INLocation, PXSelectReadonly<INLocation, Where<INLocation.projectID, Equal<Required<INLocation.projectID>>, And2<Where<INLocation.taskID, IsNull>, Or<INLocation.taskID, Equal<Required<INLocation.taskID>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.ProjectID,
      e.NewValue
    }))
    {
      INLocation inLocation = PXResult<INLocation>.op_Implicit(pxResult);
      HashSet<int> intSet2 = intSet1;
      nullable1 = inLocation.SiteID;
      int num1 = nullable1.Value;
      intSet2.Add(num1);
      nullable1 = inLocation.TaskID;
      int? nullable2 = (int?) e.NewValue;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        HashSet<int> intSet3 = source;
        nullable2 = inLocation.SiteID;
        int num2 = nullable2.Value;
        intSet3.Add(num2);
      }
    }
    if ((source.Count <= 0 || source.Contains(row.SiteID.Value)) && intSet1.Contains(row.SiteID.Value))
      return;
    string str = (string) null;
    PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, row.ProjectID, row.TaskID);
    if (dirty != null)
      str = dirty.TaskCD;
    if (source.Count > 0)
    {
      PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this, new int?(source.First<int>()));
      sender.RaiseExceptionHandling<POLine.taskID>((object) row, (object) str, (Exception) new PXSetPropertyException("Given Project Task is associated with another Warehouse - '{0}'. Either change the warehouse or select another Project Task.", (PXErrorLevel) 2, new object[1]
      {
        (object) inSite.SiteCD
      }));
    }
    else
      sender.RaiseExceptionHandling<POLine.taskID>((object) row, (object) str, (Exception) new PXSetPropertyException("Given Project Task is not associated with any Warehouse Location and cannot be used with a Stock-Item. Either select another Project Task or map it to the warehouse location.", (PXErrorLevel) 2));
  }

  protected virtual void POLine_SiteID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    POLine row = (POLine) e.Row;
    if (row == null || !POLineType.IsProjectDropShip(row.LineType))
      return;
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void POLine_SiteID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    POLine row = (POLine) e.Row;
    if (row == null)
      return;
    if ((row.LineType == "GI" || row.LineType == "GS" || row.LineType == "GF" || row.LineType == "NS" || row.LineType == "SV" || row.LineType == "GR" || row.LineType == "GP") && row.SiteID.HasValue)
    {
      sender.SetDefaultExt<POLine.expenseAcctID>(e.Row);
      sender.SetDefaultExt<POLine.expenseSubID>(e.Row);
      PX.Objects.AP.APTran apTran = PXResultset<PX.Objects.AP.APTran>.op_Implicit(PXSelectBase<PX.Objects.AP.APTran, PXViewOf<PX.Objects.AP.APTran>.BasedOn<SelectFromBase<PX.Objects.AP.APTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APTran.pOOrderType, Equal<P.AsString.ASCII>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APTran.pONbr, Equal<P.AsString>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APTran.pOLineNbr, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.AP.APTran.tranType, IBqlString>.IsNotEqual<APDocType.prepayment>>>>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) row.OrderType,
        (object) row.OrderNbr,
        (object) row.LineNbr
      }));
      POReceiptLine poReceiptLine = PXResultset<POReceiptLine>.op_Implicit(PXSelectBase<POReceiptLine, PXViewOf<POReceiptLine>.BasedOn<SelectFromBase<POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLine.pOType, Equal<P.AsString.ASCII>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLine.pONbr, Equal<P.AsString>>>>>.And<BqlOperand<POReceiptLine.pOLineNbr, IBqlInt>.IsEqual<P.AsInt>>>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) row.OrderType,
        (object) row.OrderNbr,
        (object) row.LineNbr
      }));
      if (apTran == null && poReceiptLine == null)
      {
        sender.SetDefaultExt<POLine.pOAccrualAcctID>(e.Row);
        sender.SetDefaultExt<POLine.pOAccrualSubID>(e.Row);
      }
    }
    sender.SetDefaultExt<POLine.curyUnitCost>(e.Row);
  }

  protected virtual void POLine_SubItemID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<POLine.uOM>(e.Row);
    sender.SetDefaultExt<POLine.unitCost>(e.Row);
    sender.SetDefaultExt<POLine.curyUnitCost>(e.Row);
    sender.SetDefaultExt<POLine.promisedDate>(e.Row);
  }

  protected virtual void POLine_DiscountID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is POLine row) || !e.ExternalCall)
      return;
    this._discountEngine.UpdateManualLineDiscount(sender, (PXSelectBase<POLine>) this.Transactions, row, (PXSelectBase<POOrderDiscountDetail>) this.DiscountDetails, ((PXSelectBase<POOrder>) this.Document).Current.BranchID, ((PXSelectBase<POOrder>) this.Document).Current.VendorLocationID, ((PXSelectBase<POOrder>) this.Document).Current.OrderDate, DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation | DiscountEngine.DiscountCalculationOptions.DisableARDiscountsCalculation);
  }

  protected virtual void POLine_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    POLine row1 = (POLine) e.Row;
    POOrder current = ((PXSelectBase<POOrder>) this.Document).Current;
    if (current == null || row1 == null || ((PXGraph) this).IsExport && !((PXGraph) this).IsContractBasedAPI)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row1.InventoryID);
    bool? nullable1;
    int num1;
    if (inventoryItem != null)
    {
      nullable1 = inventoryItem.IsConverted;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = row1.IsStockItem;
        if (nullable1.HasValue)
        {
          nullable1 = row1.IsStockItem;
          bool? stkItem = inventoryItem.StkItem;
          num1 = !(nullable1.GetValueOrDefault() == stkItem.GetValueOrDefault() & nullable1.HasValue == stkItem.HasValue) ? 1 : 0;
          goto label_6;
        }
      }
    }
    num1 = 0;
label_6:
    bool? nullable2;
    int num2;
    if (current != null)
    {
      nullable2 = current.IsLegacyDropShip;
      if (nullable2.GetValueOrDefault())
      {
        nullable2 = row1.Completed;
        if (nullable2.GetValueOrDefault())
        {
          num2 = this.IsLinkedToSO(row1) ? 1 : 0;
          goto label_11;
        }
      }
    }
    num2 = 0;
label_11:
    bool flag1 = num2 != 0;
    if (num1 != 0)
    {
      PXUIFieldAttribute.SetEnabled(sender, e.Row, false);
      nullable2 = row1.Closed;
      if (!nullable2.GetValueOrDefault())
        PXUIFieldAttribute.SetEnabled<POLine.closed>(sender, e.Row, true);
    }
    else
    {
      nullable2 = ((PXSelectBase<POOrder>) this.Document).Current.Hold;
      if (!nullable2.GetValueOrDefault() | flag1)
      {
        PXUIFieldAttribute.SetEnabled(sender, e.Row, false);
        PXUIFieldAttribute.SetEnabled<POLine.closed>(sender, e.Row, EnumerableExtensions.IsIn<string>(current.Status, "H", "B", "N"));
        if (EnumerableExtensions.IsIn<string>(current.Status, "B", "N") && !flag1)
        {
          PXUIFieldAttribute.SetEnabled<POLine.promisedDate>(sender, e.Row, true);
          PXCache pxCache1 = sender;
          object row2 = e.Row;
          Decimal? receivedQty1 = row1.ReceivedQty;
          Decimal num3 = 0M;
          Decimal? nullable3;
          Decimal? nullable4;
          Decimal? nullable5;
          Decimal? nullable6;
          int num4;
          if (!(receivedQty1.GetValueOrDefault() == num3 & receivedQty1.HasValue))
          {
            Decimal? receivedQty2 = row1.ReceivedQty;
            nullable3 = row1.OrderQty;
            nullable4 = row1.RcptQtyThreshold;
            nullable5 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * nullable4.GetValueOrDefault()) : new Decimal?();
            Decimal num5 = (Decimal) 100;
            Decimal? nullable7;
            if (!nullable5.HasValue)
            {
              nullable4 = new Decimal?();
              nullable7 = nullable4;
            }
            else
              nullable7 = new Decimal?(nullable5.GetValueOrDefault() / num5);
            nullable6 = nullable7;
            num4 = receivedQty2.GetValueOrDefault() < nullable6.GetValueOrDefault() & receivedQty2.HasValue & nullable6.HasValue ? 1 : 0;
          }
          else
            num4 = 1;
          PXUIFieldAttribute.SetEnabled<POLine.cancelled>(pxCache1, row2, num4 != 0);
          PXCache pxCache2 = sender;
          object row3 = e.Row;
          nullable6 = row1.ReceivedQty;
          Decimal num6 = 0M;
          int num7;
          if (!(nullable6.GetValueOrDefault() == num6 & nullable6.HasValue))
          {
            nullable6 = row1.ReceivedQty;
            nullable4 = row1.OrderQty;
            nullable3 = row1.RcptQtyThreshold;
            nullable5 = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * nullable3.GetValueOrDefault()) : new Decimal?();
            Decimal num8 = (Decimal) 100;
            Decimal? nullable8;
            if (!nullable5.HasValue)
            {
              nullable3 = new Decimal?();
              nullable8 = nullable3;
            }
            else
              nullable8 = new Decimal?(nullable5.GetValueOrDefault() / num8);
            Decimal? nullable9 = nullable8;
            num7 = nullable6.GetValueOrDefault() >= nullable9.GetValueOrDefault() & nullable6.HasValue & nullable9.HasValue ? 1 : 0;
          }
          else
            num7 = 1;
          PXUIFieldAttribute.SetEnabled<POLine.completed>(pxCache2, row3, num7 != 0);
        }
      }
      else
      {
        if (!this._blockUIUpdate)
        {
          nullable2 = row1.IsStockItem;
          bool? nullable10;
          if (!nullable2.HasValue)
          {
            nullable1 = new bool?();
            nullable10 = nullable1;
          }
          else
            nullable10 = new bool?(!nullable2.GetValueOrDefault());
          nullable1 = nullable10;
          int num9;
          if (nullable1.GetValueOrDefault())
          {
            nullable1 = row1.IsKit;
            num9 = nullable1.GetValueOrDefault() ? 1 : 0;
          }
          else
            num9 = 0;
          bool flag2 = num9 != 0;
          int num10 = ProjectAttribute.IsPMVisible("PO") ? 1 : 0;
          int num11;
          if (current == null)
          {
            num11 = 0;
          }
          else
          {
            nullable2 = current.RetainageApply;
            num11 = nullable2.GetValueOrDefault() ? 1 : 0;
          }
          bool flag3 = num11 != 0;
          bool flag4 = num10 != 0 && current?.OrderType != "PD";
          PXUIFieldAttribute.SetEnabled<POLine.completePOLine>(sender, e.Row, false);
          Decimal? receivedQty3;
          switch (row1.LineType)
          {
            case "DN":
              PXUIFieldAttribute.SetEnabled(sender, e.Row, false);
              PXUIFieldAttribute.SetEnabled<POLine.branchID>(sender, e.Row, true);
              PXUIFieldAttribute.SetEnabled<POLine.inventoryID>(sender, e.Row, true);
              PXUIFieldAttribute.SetEnabled<POLine.tranDesc>(sender, e.Row, true);
              break;
            case "FT":
            case "MC":
              PXUIFieldAttribute.SetEnabled(sender, e.Row, false);
              PXUIFieldAttribute.SetEnabled<POLine.branchID>(sender, e.Row, true);
              PXUIFieldAttribute.SetEnabled<POLine.inventoryID>(sender, e.Row, true);
              PXUIFieldAttribute.SetEnabled<POLine.tranDesc>(sender, e.Row, true);
              PXUIFieldAttribute.SetEnabled<POLine.taxCategoryID>(sender, e.Row, true);
              PXUIFieldAttribute.SetEnabled<POLine.curyLineAmt>(sender, e.Row, true);
              PXUIFieldAttribute.SetEnabled<POLine.cancelled>(sender, e.Row, true);
              PXUIFieldAttribute.SetEnabled<POLine.projectID>(sender, e.Row, flag4);
              PXUIFieldAttribute.SetEnabled<POLine.taskID>(sender, e.Row, true);
              PXUIFieldAttribute.SetEnabled<POLine.retainagePct>(sender, e.Row, flag3);
              PXUIFieldAttribute.SetEnabled<POLine.curyRetainageAmt>(sender, e.Row, flag3);
              break;
            case "NS":
            case "SV":
              PXUIFieldAttribute.SetEnabled(sender, e.Row, true);
              PXUIFieldAttribute.SetEnabled<POLine.siteID>(sender, e.Row, true);
              PXUIFieldAttribute.SetEnabled<POLine.subItemID>(sender, e.Row, false);
              PXCache pxCache3 = sender;
              object row4 = e.Row;
              receivedQty3 = row1.ReceivedQty;
              Decimal num12 = 0M;
              int num13 = receivedQty3.GetValueOrDefault() == num12 & receivedQty3.HasValue ? 1 : 0;
              PXUIFieldAttribute.SetEnabled<POLine.inventoryID>(pxCache3, row4, num13 != 0);
              PXUIFieldAttribute.SetEnabled<POLine.cancelled>(sender, e.Row, true);
              PXUIFieldAttribute.SetEnabled<POLine.curyExtCost>(sender, e.Row, false);
              PXCache pxCache4 = sender;
              object row5 = e.Row;
              receivedQty3 = row1.ReceivedQty;
              Decimal num14 = 0M;
              int num15 = receivedQty3.GetValueOrDefault() == num14 & receivedQty3.HasValue ? 1 : 0;
              PXUIFieldAttribute.SetEnabled<POLine.uOM>(pxCache4, row5, num15 != 0);
              PXUIFieldAttribute.SetEnabled<POLine.completePOLine>(sender, e.Row, false);
              PXUIFieldAttribute.SetEnabled<POLine.projectID>(sender, e.Row, flag4);
              PXUIFieldAttribute.SetEnabled<POLine.retainagePct>(sender, e.Row, flag3);
              PXUIFieldAttribute.SetEnabled<POLine.curyRetainageAmt>(sender, e.Row, flag3);
              break;
            default:
              PXUIFieldAttribute.SetEnabled(sender, e.Row, true);
              PXCache pxCache5 = sender;
              object row6 = e.Row;
              Decimal? receivedQty4 = row1.ReceivedQty;
              Decimal num16 = 0M;
              int num17 = receivedQty4.GetValueOrDefault() == num16 & receivedQty4.HasValue ? 1 : 0;
              PXUIFieldAttribute.SetEnabled<POLine.inventoryID>(pxCache5, row6, num17 != 0);
              PXCache pxCache6 = sender;
              object row7 = e.Row;
              int num18;
              if (!flag2)
              {
                receivedQty3 = row1.ReceivedQty;
                Decimal num19 = 0M;
                num18 = receivedQty3.GetValueOrDefault() == num19 & receivedQty3.HasValue ? 1 : 0;
              }
              else
                num18 = 0;
              PXUIFieldAttribute.SetEnabled<POLine.subItemID>(pxCache6, row7, num18 != 0);
              PXCache pxCache7 = sender;
              object row8 = e.Row;
              receivedQty3 = row1.ReceivedQty;
              Decimal num20 = 0M;
              int num21 = receivedQty3.GetValueOrDefault() == num20 & receivedQty3.HasValue ? 1 : 0;
              PXUIFieldAttribute.SetEnabled<POLine.uOM>(pxCache7, row8, num21 != 0);
              PXUIFieldAttribute.SetEnabled<POLine.discountSequenceID>(sender, e.Row, false);
              PXUIFieldAttribute.SetEnabled<POLine.completePOLine>(sender, e.Row, false);
              PXUIFieldAttribute.SetEnabled<POLine.projectID>(sender, e.Row, flag4);
              PXUIFieldAttribute.SetEnabled<POLine.retainagePct>(sender, e.Row, flag3);
              PXUIFieldAttribute.SetEnabled<POLine.curyRetainageAmt>(sender, e.Row, flag3);
              PXUIFieldAttribute.SetEnabled<POLine.isSpecialOrder>(sender, e.Row, false);
              break;
          }
          PXCache pxCache8 = sender;
          object row9 = e.Row;
          int num22;
          if (POLineType.IsDefault(row1.LineType))
          {
            receivedQty3 = row1.ReceivedQty;
            Decimal num23 = 0M;
            num22 = receivedQty3.GetValueOrDefault() == num23 & receivedQty3.HasValue ? 1 : 0;
          }
          else
            num22 = 0;
          PXUIFieldAttribute.SetEnabled<POLine.lineType>(pxCache8, row9, num22 != 0);
          PXUIFieldAttribute.SetEnabled<POLine.receivedQty>(sender, e.Row, false);
          PXUIFieldAttribute.SetEnabled<POLine.curyExtCost>(sender, e.Row, false);
          PXUIFieldAttribute.SetEnabled<POLine.baseOrderQty>(sender, e.Row, false);
          PXUIFieldAttribute.SetEnabled<POLine.pOAccrualType>(sender, e.Row, false);
          PXUIFieldAttribute.SetEnabled<POLine.completedQty>(sender, e.Row, false);
          PXUIFieldAttribute.SetEnabled<POLine.billedQty>(sender, e.Row, false);
          PXUIFieldAttribute.SetEnabled<POLine.curyBilledAmt>(sender, e.Row, false);
          PXUIFieldAttribute.SetEnabled<POLine.openQty>(sender, e.Row, false);
          PXUIFieldAttribute.SetEnabled<POLine.unbilledQty>(sender, e.Row, false);
          PXUIFieldAttribute.SetEnabled<POLine.curyUnbilledAmt>(sender, e.Row, false);
          PXUIFieldAttribute.SetEnabled<POLine.displayReqPrepaidQty>(sender, e.Row, false);
          PXUIFieldAttribute.SetEnabled<POLine.curyReqPrepaidAmt>(sender, e.Row, false);
          PXUIFieldAttribute.SetEnabled<POLine.allowEditUnitCostInPR>(sender, e.Row, false);
          PXUIFieldAttribute.SetEnabled<POLine.expenseAcctID>(sender, e.Row, this.IsExpenseAccountEnabled(row1));
          PXUIFieldAttribute.SetEnabled<POLine.expenseSubID>(sender, e.Row, this.IsExpenseAccountEnabled(row1));
          PXUIFieldAttribute.SetEnabled<POLine.pOAccrualAcctID>(sender, e.Row, this.IsAccrualAccountEnabled(row1));
          PXUIFieldAttribute.SetEnabled<POLine.pOAccrualSubID>(sender, e.Row, this.IsAccrualAccountEnabled(row1));
        }
        nullable2 = current.Cancelled;
        bool flag5 = false;
        if (nullable2.GetValueOrDefault() == flag5 & nullable2.HasValue && EnumerableExtensions.IsNotIn<string>(current.Status, "M", "C") && row1.POType == "BL" && !string.IsNullOrEmpty(row1.PONbr))
        {
          POOrder poOrder = PXResultset<POOrder>.op_Implicit(PXSelectBase<POOrder, PXSelectReadonly<POOrder, Where<POOrder.orderType, Equal<Required<POOrder.orderType>>, And<POOrder.orderNbr, Equal<Required<POOrder.orderNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) row1.POType,
            (object) row1.PONbr
          }));
          if (poOrder != null)
          {
            DateTime? expirationDate = poOrder.ExpirationDate;
            if (expirationDate.HasValue)
            {
              expirationDate = poOrder.ExpirationDate;
              DateTime? orderDate = current.OrderDate;
              if ((expirationDate.HasValue & orderDate.HasValue ? (expirationDate.GetValueOrDefault() < orderDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                ((PXSelectBase) this.Transactions).Cache.RaiseExceptionHandling<POLine.lineType>((object) row1, (object) row1.LineType, (Exception) new PXSetPropertyException("Originating Blanket Order# {1} expires before the date of current Document - on {0:d}", (PXErrorLevel) 3, new object[2]
                {
                  (object) poOrder.ExpirationDate.Value,
                  (object) poOrder.OrderNbr
                }));
            }
          }
        }
        if (current.ShipDestType == "S")
        {
          int? siteId1 = current.SiteID;
          if (siteId1.HasValue)
          {
            siteId1 = row1.SiteID;
            if (siteId1.HasValue)
            {
              siteId1 = current.SiteID;
              int? siteId2 = row1.SiteID;
              if (!(siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue))
              {
                PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this, row1.SiteID);
                sender.RaiseExceptionHandling<POLine.siteID>(e.Row, (object) inSite?.SiteCD, (Exception) new PXSetPropertyException("The warehouse in the line differs from the warehouse on the Shipping Instructions tab. The items will be delivered to the {0} warehouse.", (PXErrorLevel) 2, new object[1]
                {
                  (object) inSite?.SiteCD
                }));
                goto label_64;
              }
            }
          }
        }
        if (PXUIFieldAttribute.GetWarning<POLine.siteID>(sender, (object) row1) != null)
        {
          PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this, row1.SiteID);
          sender.RaiseExceptionHandling<POLine.siteID>(e.Row, (object) inSite?.SiteCD, (Exception) null);
        }
      }
    }
label_64:
    PXUIFieldAttribute.SetVisible<POLine.pONbr>(sender, e.Row, current.OrderType != "PD");
    PXUIFieldAttribute.SetVisible<POLine.pOType>(sender, e.Row, current.OrderType != "PD");
    PXUIFieldAttribute.SetEnabled<POLine.pONbr>(sender, e.Row, false);
    PXUIFieldAttribute.SetEnabled<POLine.pOType>(sender, e.Row, false);
    PXCache pxCache9 = sender;
    object row10 = e.Row;
    int num24;
    if (row1 == null)
    {
      num24 = 0;
    }
    else
    {
      nullable2 = row1.ItemRequiresTerms;
      num24 = nullable2.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<POLine.dRTermStartDate>(pxCache9, row10, num24 != 0);
    PXCache pxCache10 = sender;
    object row11 = e.Row;
    int num25;
    if (row1 == null)
    {
      num25 = 0;
    }
    else
    {
      nullable2 = row1.ItemRequiresTerms;
      num25 = nullable2.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<POLine.dRTermEndDate>(pxCache10, row11, num25 != 0);
  }

  protected virtual void POLine_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    POLine row = (POLine) e.Row;
    if (row == null || PXDBOperationExt.Command(e.Operation) == 3)
      return;
    bool? nullable1;
    if (((PXSelectBase<POOrder>) this.Document).Current?.OrderType == "DP")
    {
      nullable1 = ((PXSelectBase<POOrder>) this.Document).Current.Hold;
      bool flag = false;
      if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue && !this.IsLineWithDropShipLocation(row))
      {
        PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this, row.SiteID);
        if (sender.RaiseExceptionHandling<POLine.siteID>((object) row, (object) inSite.SiteCD, (Exception) new PXSetPropertyException("The selected warehouse has no drop-ship location. Please select another warehouse or define the drop-ship location for the currently selected warehouse.")))
          throw new PXRowPersistingException(typeof (POLine.siteID).Name, (object) inSite.SiteCD, "The selected warehouse has no drop-ship location. Please select another warehouse or define the drop-ship location for the currently selected warehouse.");
      }
    }
    bool flag1 = POLineType.IsStock(row.LineType);
    bool flag2 = POLineType.IsNonStock(row.LineType);
    nullable1 = row.IsStockItem;
    bool flag3 = (nullable1.HasValue ? new bool?(!nullable1.GetValueOrDefault()) : new bool?()).GetValueOrDefault() && row.IsKit.GetValueOrDefault();
    PXDefaultAttribute.SetPersistingCheck<POLine.inventoryID>(sender, e.Row, POLineType.IsStock(row.LineType) || POLineType.IsNonStock(row.LineType) && !POLineType.IsService(row.LineType) ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<POLine.subItemID>(sender, e.Row, !flag1 || flag3 ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXDefaultAttribute.SetPersistingCheck<POLine.uOM>(sender, e.Row, flag1 | flag2 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<POLine.orderQty>(sender, e.Row, flag1 | flag2 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<POLine.baseOrderQty>(sender, e.Row, flag1 | flag2 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<POLine.curyUnitCost>(sender, e.Row, flag1 | flag2 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<POLine.unitCost>(sender, e.Row, flag1 | flag2 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<POLine.expenseAcctID>(sender, e.Row, this.IsExpenseAccountEnabled(row) ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<POLine.expenseSubID>(sender, e.Row, this.IsExpenseAccountEnabled(row) ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<POLine.pOAccrualAcctID>(sender, e.Row, this.IsAccrualAccountRequired(row) ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<POLine.pOAccrualSubID>(sender, e.Row, this.IsAccrualAccountRequired(row) ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<POLine.curyExtCost>(sender, e.Row, row.LineType == "DN" ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXDefaultAttribute.SetPersistingCheck<POLine.siteID>(sender, e.Row, POLineType.IsProjectDropShip(row.LineType) || !flag1 && (!flag2 || POLineType.IsService(row.LineType)) ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    Decimal? nullable2 = row.OrderQty;
    Decimal num1 = 0M;
    if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue && !this.IsZeroQuantityValid(row))
    {
      string str = flag1 ? "Quantity must be greater than 0" : "The quantity must be greater than 0 because the non-stock item in the line either requires receipt or is closed by quantity. To be able to process the line with the zero quantity, clear the Require Receipt check box and select By Amount in the Close PO Line box for the item on the General tab of the Non-Stock Items (IN202000) form.";
      sender.RaiseExceptionHandling<POLine.orderQty>((object) row, (object) row.OrderQty, (Exception) new PXSetPropertyException((IBqlTable) row, str, (PXErrorLevel) 4));
    }
    nullable2 = row.CuryUnitCost;
    Decimal num2 = 0M;
    if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue && !this.IsZeroUnitCostValid(row))
      sender.RaiseExceptionHandling<POLine.curyUnitCost>((object) row, (object) row.CuryUnitCost, (Exception) new PXSetPropertyException((IBqlTable) row, "Unit Cost should not be 0 for the stock items", (PXErrorLevel) 2));
    this.CheckProjectAccountRule(sender, row);
  }

  protected virtual void POLine_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    POLine row = (POLine) e.Row;
    this.ClearUnused(row);
    if (row.OrderType == "PD")
    {
      int? nullable = row.ProjectID;
      if (nullable.HasValue)
      {
        nullable = row.TaskID;
        if (nullable.HasValue)
        {
          nullable = row.ExpenseAcctID;
          if (!nullable.HasValue)
            sender.SetDefaultExt<POLine.expenseAcctID>((object) row);
        }
      }
    }
    this.RecalculateDiscounts(sender, (POLine) e.Row);
    TaxBaseAttribute.Calculate<POLine.taxCategoryID>(sender, e);
  }

  protected virtual void POLine_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    POLine row = (POLine) e.Row;
    int? inventoryId1 = row.InventoryID;
    int? inventoryId2 = ((POLine) e.OldRow).InventoryID;
    int num = inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue ? 1 : 0;
    if (row.LineType != ((POLine) e.OldRow).LineType)
      this.ClearUnused(row);
    if (row.OrderType == "PD" && !sender.ObjectsEqual<POLine.dropshipExpenseRecording>(e.Row, e.OldRow))
    {
      if (row.DropshipExpenseRecording == "R")
      {
        sender.SetDefaultExt<POLine.pOAccrualAcctID>((object) row);
        sender.SetDefaultExt<POLine.pOAccrualSubID>((object) row);
      }
      else
      {
        sender.SetValueExt<POLine.pOAccrualAcctID>((object) row, (object) null);
        sender.SetValueExt<POLine.pOAccrualSubID>((object) row, (object) null);
      }
    }
    if (!sender.ObjectsEqual<POLine.vendorID>(e.Row, e.OldRow) || !sender.ObjectsEqual<POLine.branchID>(e.Row, e.OldRow) || !sender.ObjectsEqual<POLine.inventoryID>(e.Row, e.OldRow) || !sender.ObjectsEqual<POLine.baseOrderQty>(e.Row, e.OldRow) || !sender.ObjectsEqual<POLine.curyUnitCost>(e.Row, e.OldRow) || !sender.ObjectsEqual<POLine.curyExtCost>(e.Row, e.OldRow) || !sender.ObjectsEqual<POLine.curyLineAmt>(e.Row, e.OldRow) || !sender.ObjectsEqual<POLine.curyDiscAmt>(e.Row, e.OldRow) || !sender.ObjectsEqual<POLine.discPct>(e.Row, e.OldRow) || !sender.ObjectsEqual<POLine.manualDisc>(e.Row, e.OldRow) || !sender.ObjectsEqual<POLine.discountID>(e.Row, e.OldRow) || !sender.ObjectsEqual<POLine.siteID>(e.Row, e.OldRow))
      this.RecalculateDiscounts(sender, row);
    if ((e.ExternalCall || sender.Graph.IsImport) && sender.ObjectsEqual<POLine.vendorID>(e.Row, e.OldRow) && sender.ObjectsEqual<POLine.inventoryID>(e.Row, e.OldRow) && sender.ObjectsEqual<POLine.uOM>(e.Row, e.OldRow) && sender.ObjectsEqual<POLine.orderQty>(e.Row, e.OldRow) && sender.ObjectsEqual<POLine.branchID>(e.Row, e.OldRow) && sender.ObjectsEqual<POLine.siteID>(e.Row, e.OldRow) && sender.ObjectsEqual<POLine.manualPrice>(e.Row, e.OldRow) && (!sender.ObjectsEqual<POLine.curyUnitCost>(e.Row, e.OldRow) || !sender.ObjectsEqual<POLine.curyLineAmt>(e.Row, e.OldRow)))
      row.ManualPrice = new bool?(true);
    TaxBaseAttribute.Calculate<POLine.taxCategoryID>(sender, e);
    bool? nullable;
    if (!sender.ObjectsEqual<POLine.completed>(e.Row, e.OldRow))
    {
      nullable = row.Completed;
      if (nullable.GetValueOrDefault())
        goto label_14;
    }
    if (sender.ObjectsEqual<POLine.closed>(e.Row, e.OldRow))
      return;
    nullable = row.Closed;
    if (!nullable.GetValueOrDefault())
      return;
label_14:
    if (!this.RaiseOrderEvents(((PXSelectBase<POOrder>) this.Document).Current))
      return;
    ((PXSelectBase) this.Document).View.RequestRefresh();
    ((PXSelectBase) this.Transactions).View.RequestRefresh();
  }

  protected virtual void POLine_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    POLine row = (POLine) e.Row;
    Decimal? receivedQty = row.ReceivedQty;
    Decimal num1 = 0M;
    if (receivedQty.GetValueOrDefault() > num1 & receivedQty.HasValue)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXException("The line cannot be deleted because some quantity of an item in this line have been received.");
    }
    int? rowCount1 = PXSelectBase<POReceiptLine, PXSelectGroupBy<POReceiptLine, Where<POReceiptLine.pOType, Equal<Current<POLine.orderType>>, And<POReceiptLine.pONbr, Equal<Current<POLine.orderNbr>>, And<POReceiptLine.pOLineNbr, Equal<Current<POLine.lineNbr>>>>>, Aggregate<Count>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()).RowCount;
    int num2 = 0;
    if (rowCount1.GetValueOrDefault() > num2 & rowCount1.HasValue)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXException("The line cannot be deleted because some quantity of an item in this line have been received.");
    }
    int? rowCount2 = PXSelectBase<PX.Objects.AP.APTran, PXSelectGroupBy<PX.Objects.AP.APTran, Where<PX.Objects.AP.APTran.pOOrderType, Equal<Current<POLine.orderType>>, And<PX.Objects.AP.APTran.pONbr, Equal<Current<POLine.orderNbr>>, And<PX.Objects.AP.APTran.pOLineNbr, Equal<Current<POLine.lineNbr>>, And<PX.Objects.AP.APTran.released, Equal<True>>>>>, Aggregate<Count>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()).RowCount;
    int num3 = 0;
    if (rowCount2.GetValueOrDefault() > num3 & rowCount2.HasValue)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXException("The order line cannot be deleted because there is at least one AP bill has been released for this order line. For the list of AP bills, refer to Reports > View Purchase Order Receipts History.");
    }
    PXResult<PX.Objects.AP.APTran> pxResult = PXResultset<PX.Objects.AP.APTran>.op_Implicit(PXSelectBase<PX.Objects.AP.APTran, PXSelectJoin<PX.Objects.AP.APTran, LeftJoin<POOrderPrepayment, On<POOrderPrepayment.aPDocType, Equal<PX.Objects.AP.APTran.tranType>, And<POOrderPrepayment.aPRefNbr, Equal<PX.Objects.AP.APTran.refNbr>, And<POOrderPrepayment.orderType, Equal<Current<POLine.orderType>>, And<POOrderPrepayment.orderNbr, Equal<Current<POLine.orderNbr>>>>>>>, Where<PX.Objects.AP.APTran.pOOrderType, Equal<Current<POLine.orderType>>, And<PX.Objects.AP.APTran.pONbr, Equal<Current<POLine.orderNbr>>, And<PX.Objects.AP.APTran.pOLineNbr, Equal<Current<POLine.lineNbr>>>>>, OrderBy<Desc<POOrderPrepayment.aPRefNbr>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    if (pxResult != null && ((PXResult) pxResult).GetItem<POOrderPrepayment>().APRefNbr != null)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXException("The line cannot be deleted because one or multiple prepayment requests have been generated for this order. To proceed, delete prepayment requests first.");
    }
    if (pxResult != null)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXException("The order line cannot be deleted because one or multiple AP bills have been generated for this order line. To proceed, delete AP bills first. For the list of AP bills, refer to Reports > View Purchase Order Receipts History.");
    }
    POLine relatedPOLine;
    if (row.OrderType == "BL" && this.BlanketPOLineIsReferencedInPOOrder(row, out relatedPOLine))
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXException("The line cannot be deleted because it is associated with a line in the {0} purchase order.", new object[1]
      {
        (object) relatedPOLine.OrderNbr
      });
    }
  }

  protected virtual void POLine_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (((PXSelectBase<POOrder>) this.Document).Current == null || ((PXSelectBase) this.Document).Cache.GetStatus((object) ((PXSelectBase<POOrder>) this.Document).Current) == 3 || ((PXSelectBase) this.Document).Cache.GetStatus((object) ((PXSelectBase<POOrder>) this.Document).Current) == 4)
      return;
    this._discountEngine.RecalculateGroupAndDocumentDiscounts(sender, (PXSelectBase<POLine>) this.Transactions, (POLine) null, (PXSelectBase<POOrderDiscountDetail>) this.DiscountDetails, ((PXSelectBase<POOrder>) this.Document).Current.BranchID, ((PXSelectBase<POOrder>) this.Document).Current.VendorLocationID, ((PXSelectBase<POOrder>) this.Document).Current.OrderDate, DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation | DiscountEngine.DiscountCalculationOptions.DisableARDiscountsCalculation);
    this.RecalculateTotalDiscount();
  }

  protected virtual void _(PX.Data.Events.RowDeleted<POOrder> e)
  {
    if (e.Row == null)
      return;
    ((SelectedEntityEvent<POOrder>) PXEntityEventBase<POOrder>.Container<POOrder.Events>.Select((Expression<Func<POOrder.Events, PXEntityEvent<POOrder.Events>>>) (ev => ev.OrderDeleted))).FireOn((PXGraph) this, e.Row);
  }

  protected virtual void POLine_Closed_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    POLine row = (POLine) e.Row;
    if (!row.Closed.GetValueOrDefault())
      return;
    sender.SetValueExt<POLine.completed>((object) row, (object) true);
  }

  protected virtual void POLine_Completed_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    POLine row = (POLine) e.Row;
    bool? completed = row.Completed;
    bool flag = false;
    if (!(completed.GetValueOrDefault() == flag & completed.HasValue))
      return;
    sender.SetValueExt<POLine.closed>((object) row, (object) false);
  }

  protected virtual void POLine_Completed_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    POLine row = (POLine) e.Row;
    if (row == null || !(bool) e.NewValue)
      return;
    POReceipt receipt = PXResultset<POReceipt>.op_Implicit(PXSelectBase<POReceipt, PXSelectJoin<POReceipt, InnerJoin<POReceiptLine, On<POReceiptLine.FK.Receipt>>, Where<POReceiptLine.pOType, Equal<Required<POReceiptLine.pOType>>, And<POReceiptLine.pONbr, Equal<Required<POReceiptLine.pONbr>>, And<POReceiptLine.pOLineNbr, Equal<Required<POReceiptLine.pOLineNbr>>, And<POReceiptLine.released, Equal<False>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) row?.OrderType,
      (object) row?.OrderNbr,
      (object) (int?) row?.LineNbr
    }));
    if (receipt == null)
      return;
    this.ThrowErrorWhenPurchaseReceiptExists(receipt, row);
  }

  protected virtual void POLine_Cancelled_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    POLine row = (POLine) e.Row;
    if (row.Cancelled.GetValueOrDefault())
    {
      sender.SetValueExt<POLine.completed>((object) row, (object) true);
    }
    else
    {
      Decimal? openQty = row.OpenQty;
      Decimal? orderQty = row.OrderQty;
      Decimal? rcptQtyThreshold = row.RcptQtyThreshold;
      Decimal? nullable = orderQty.HasValue & rcptQtyThreshold.HasValue ? new Decimal?(orderQty.GetValueOrDefault() * rcptQtyThreshold.GetValueOrDefault() / 100M) : new Decimal?();
      if (openQty.GetValueOrDefault() < nullable.GetValueOrDefault() & openQty.HasValue & nullable.HasValue)
        sender.SetValueExt<POLine.completed>((object) row, (object) false);
    }
    sender.RaiseFieldUpdated<POLine.taxCategoryID>((object) row, (object) row.TaxCategoryID);
  }

  protected virtual void POLine_Cancelled_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    POLine row = (POLine) e.Row;
    if (!((bool?) e.NewValue).GetValueOrDefault())
      return;
    if (row.OrderType == "BL")
    {
      Decimal? receivedQty = row.ReceivedQty;
      Decimal num = 0M;
      if (receivedQty.GetValueOrDefault() > num & receivedQty.HasValue)
      {
        this.ThrowErrorWhenPurchaseReceiptExists((POReceipt) null, row);
        return;
      }
    }
    POReceipt receipt = PXResultset<POReceipt>.op_Implicit(PXSelectBase<POReceipt, PXSelectJoin<POReceipt, InnerJoin<POReceiptLine, On<POReceiptLine.FK.Receipt>>, Where<POReceiptLine.pOType, Equal<Required<POReceiptLine.pOType>>, And<POReceiptLine.pONbr, Equal<Required<POReceiptLine.pONbr>>, And<POReceiptLine.pOLineNbr, Equal<Required<POReceiptLine.pOLineNbr>>>>>, OrderBy<Asc<POReceipt.released>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) row?.OrderType,
      (object) row?.OrderNbr,
      (object) (int?) row?.LineNbr
    }));
    if (receipt == null)
      return;
    this.ThrowErrorWhenPurchaseReceiptExists(receipt, row);
  }

  protected bool IsRequired(string poLineType)
  {
    return poLineType == "NS" || poLineType == "FT" || poLineType == "SV";
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<POLine, POLine.projectID> e)
  {
    POOrder current = ((PXSelectBase<POOrder>) this.Document).Current;
    POLine row = e.Row;
    if (current == null || row == null || !(current.OrderType == "PD"))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POLine, POLine.projectID>, POLine, object>) e).NewValue = (object) current.ProjectID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POLine, POLine.projectID>>) e).Cancel = true;
  }

  [PXBool]
  [DRTerms.Dates(typeof (POLine.dRTermStartDate), typeof (POLine.dRTermEndDate), typeof (POLine.inventoryID), VerifyDatesPresent = false)]
  protected virtual void POLine_ItemRequiresTerms_CacheAttached(PXCache sender)
  {
  }

  protected virtual void SOLineSplit3_POUOM_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    POOrderEntry.SOLineSplit3 row = (POOrderEntry.SOLineSplit3) e.Row;
    if (row == null)
      return;
    POLine poLine = PXResultset<POLine>.op_Implicit(PXSelectBase<POLine, PXSelect<POLine, Where<POLine.orderType, Equal<Current<POOrderEntry.SOLineSplit3.pOType>>, And<POLine.orderNbr, Equal<Current<POOrderEntry.SOLineSplit3.pONbr>>, And<POLine.lineNbr, Equal<Current<POOrderEntry.SOLineSplit3.pOLineNbr>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    e.ReturnValue = poLine == null || poLine.UOM == null ? (object) row.UOM : (object) poLine.UOM;
  }

  protected virtual void SOLineSplit3_POUOMOrderQty_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    POOrderEntry.SOLineSplit3 row = (POOrderEntry.SOLineSplit3) e.Row;
    if (row == null)
      return;
    POLine poLine = PXResultset<POLine>.op_Implicit(PXSelectBase<POLine, PXSelect<POLine, Where<POLine.orderType, Equal<Current<POOrderEntry.SOLineSplit3.pOType>>, And<POLine.orderNbr, Equal<Current<POOrderEntry.SOLineSplit3.pONbr>>, And<POLine.lineNbr, Equal<Current<POOrderEntry.SOLineSplit3.pOLineNbr>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    if (poLine == null)
      return;
    string str = poLine.UOM ?? row.UOM;
    if (!string.Equals(row.UOM, str))
    {
      Decimal num = INUnitAttribute.ConvertToBase<POOrderEntry.SOLineSplit3.inventoryID>(sender, (object) row, row.UOM, row.OrderQty.Value, INPrecision.QUANTITY);
      e.ReturnValue = (object) INUnitAttribute.ConvertFromBase<POOrderEntry.SOLineSplit3.inventoryID>(sender, (object) row, str, num, INPrecision.QUANTITY);
    }
    else
      e.ReturnValue = (object) row.OrderQty;
  }

  protected virtual void Vendor_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void POOrderDiscountDetail_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (this.Document == null)
      return;
    POOrder current = ((PXSelectBase<POOrder>) this.Document).Current;
  }

  protected virtual void POOrderDiscountDetail_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    POOrderDiscountDetail row = (POOrderDiscountDetail) e.Row;
    if (this._discountEngine.IsInternalDiscountEngineCall || row == null)
      return;
    if (row.DiscountID != null)
    {
      this._discountEngine.InsertManualDocGroupDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<POLine>) this.Transactions, (PXSelectBase<POOrderDiscountDetail>) this.DiscountDetails, row, row.DiscountID, (string) null, ((PXSelectBase<POOrder>) this.Document).Current.BranchID, ((PXSelectBase<POOrder>) this.Document).Current.VendorLocationID, ((PXSelectBase<POOrder>) this.Document).Current.OrderDate, DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation | DiscountEngine.DiscountCalculationOptions.DisableARDiscountsCalculation);
      this.RecalculateTotalDiscount();
    }
    if (!this._discountEngine.SetExternalManualDocDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<POLine>) this.Transactions, (PXSelectBase<POOrderDiscountDetail>) this.DiscountDetails, row, (POOrderDiscountDetail) null, DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation | DiscountEngine.DiscountCalculationOptions.DisableARDiscountsCalculation))
      return;
    this.RecalculateTotalDiscount();
  }

  protected virtual void POOrderDiscountDetail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    POOrderDiscountDetail row = (POOrderDiscountDetail) e.Row;
    POOrderDiscountDetail oldRow = (POOrderDiscountDetail) e.OldRow;
    if (this._discountEngine.IsInternalDiscountEngineCall || row == null)
      return;
    if (!sender.ObjectsEqual<POOrderDiscountDetail.skipDiscount>(e.Row, e.OldRow))
    {
      this._discountEngine.UpdateDocumentDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<POLine>) this.Transactions, (PXSelectBase<POOrderDiscountDetail>) this.DiscountDetails, ((PXSelectBase<POOrder>) this.Document).Current.BranchID, ((PXSelectBase<POOrder>) this.Document).Current.VendorLocationID, ((PXSelectBase<POOrder>) this.Document).Current.OrderDate, row.Type != "D", DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation | DiscountEngine.DiscountCalculationOptions.DisableARDiscountsCalculation);
      this.RecalculateTotalDiscount();
    }
    if (!sender.ObjectsEqual<POOrderDiscountDetail.discountID>(e.Row, e.OldRow) || !sender.ObjectsEqual<POOrderDiscountDetail.discountSequenceID>(e.Row, e.OldRow))
    {
      this._discountEngine.UpdateManualDocGroupDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<POLine>) this.Transactions, (PXSelectBase<POOrderDiscountDetail>) this.DiscountDetails, row, row.DiscountID, sender.ObjectsEqual<POOrderDiscountDetail.discountID>(e.Row, e.OldRow) ? row.DiscountSequenceID : (string) null, ((PXSelectBase<POOrder>) this.Document).Current.BranchID, ((PXSelectBase<POOrder>) this.Document).Current.VendorLocationID, ((PXSelectBase<POOrder>) this.Document).Current.OrderDate, DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation | DiscountEngine.DiscountCalculationOptions.DisableARDiscountsCalculation);
      this.RecalculateTotalDiscount();
    }
    if (!this._discountEngine.SetExternalManualDocDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<POLine>) this.Transactions, (PXSelectBase<POOrderDiscountDetail>) this.DiscountDetails, row, oldRow, DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation | DiscountEngine.DiscountCalculationOptions.DisableARDiscountsCalculation))
      return;
    this.RecalculateTotalDiscount();
  }

  protected virtual void POOrderDiscountDetail_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    POOrderDiscountDetail row = (POOrderDiscountDetail) e.Row;
    if (!this._discountEngine.IsInternalDiscountEngineCall && row != null)
      this._discountEngine.UpdateDocumentDiscount(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<POLine>) this.Transactions, (PXSelectBase<POOrderDiscountDetail>) this.DiscountDetails, ((PXSelectBase<POOrder>) this.Document).Current.BranchID, ((PXSelectBase<POOrder>) this.Document).Current.VendorLocationID, ((PXSelectBase<POOrder>) this.Document).Current.OrderDate, row.Type != null && row.Type != "D" && row.Type != "B", DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation | DiscountEngine.DiscountCalculationOptions.DisableARDiscountsCalculation);
    this.RecalculateTotalDiscount();
  }

  protected virtual void POOrderDiscountDetail_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    POOrderDiscountDetail row = (POOrderDiscountDetail) e.Row;
    bool flag = row.Type == "B";
    PXDefaultAttribute.SetPersistingCheck<POOrderDiscountDetail.discountID>(sender, (object) row, flag ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXDefaultAttribute.SetPersistingCheck<POOrderDiscountDetail.discountSequenceID>(sender, (object) row, flag ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
  }

  protected virtual void ClearUnused(POLine poLine)
  {
    if (poLine.LineType == "DN" || poLine.LineType == "FT" || poLine.LineType == "MC")
    {
      poLine.InventoryID = new int?();
      poLine.SubItemID = new int?();
      poLine.UOM = (string) null;
      poLine.ReceivedQty = new Decimal?(0M);
      poLine.BaseReceivedQty = new Decimal?(0M);
      poLine.OpenQty = poLine.OrderQty;
      poLine.BaseOpenQty = poLine.BaseOrderQty;
      poLine.CuryUnitCost = new Decimal?(0M);
      poLine.UnitCost = new Decimal?(0M);
      poLine.UnitVolume = new Decimal?(0M);
      poLine.UnitWeight = new Decimal?(0M);
      poLine.RcptQtyAction = "A";
      poLine.RcptQtyMax = new Decimal?((Decimal) 100);
      poLine.RcptQtyMin = new Decimal?(0M);
    }
    if (poLine.LineType == "DN")
    {
      poLine.SiteID = new int?();
      poLine.ExpenseAcctID = new int?();
      poLine.ExpenseSubID = new int?();
    }
    if (!this.IsExpenseAccountEnabled(poLine))
    {
      poLine.ExpenseAcctID = new int?();
      poLine.ExpenseSubID = new int?();
    }
    if (this.IsAccrualAccountEnabled(poLine))
      return;
    poLine.POAccrualAcctID = new int?();
    poLine.POAccrualSubID = new int?();
  }

  public bool IsLinkedToSO(POLine row)
  {
    return row.LineType == "GS" || row.LineType == "GP" || row.LineType == "NO" || row.LineType == "NP";
  }

  protected virtual void _(PX.Data.Events.FieldUpdating<POOrder, POOrder.approved> e)
  {
    bool result;
    if (e.Row == null || true.Equals(e.OldValue) || (!(((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<POOrder, POOrder.approved>>) e).NewValue is bool newValue) || !newValue) && (((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<POOrder, POOrder.approved>>) e).NewValue == null || !bool.TryParse(((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<POOrder, POOrder.approved>>) e).NewValue.ToString(), out result) || !result) || !this.GetRequireControlTotal(e.Row.OrderType))
      return;
    Decimal? curyOrderTotal = e.Row.CuryOrderTotal;
    Decimal? curyControlTotal = e.Row.CuryControlTotal;
    if (curyOrderTotal.GetValueOrDefault() == curyControlTotal.GetValueOrDefault() & curyOrderTotal.HasValue == curyControlTotal.HasValue)
      return;
    ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<POOrder, POOrder.approved>>) e).Cancel = true;
    ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<POOrder, POOrder.approved>>) e).NewValue = (object) false;
  }

  protected virtual void _(PX.Data.Events.FieldUpdating<POOrder, POOrder.rejected> e)
  {
    bool result;
    if (e.Row == null || true.Equals(e.OldValue) || (!(((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<POOrder, POOrder.rejected>>) e).NewValue is bool newValue) || !newValue) && (((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<POOrder, POOrder.rejected>>) e).NewValue == null || !bool.TryParse(((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<POOrder, POOrder.rejected>>) e).NewValue.ToString(), out result) || !result) || !this.GetRequireControlTotal(e.Row.OrderType))
      return;
    Decimal? curyOrderTotal = e.Row.CuryOrderTotal;
    Decimal? curyControlTotal = e.Row.CuryControlTotal;
    if (curyOrderTotal.GetValueOrDefault() == curyControlTotal.GetValueOrDefault() & curyOrderTotal.HasValue == curyControlTotal.HasValue)
      return;
    ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<POOrder, POOrder.rejected>>) e).Cancel = true;
    ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<POOrder, POOrder.rejected>>) e).NewValue = (object) false;
  }

  private bool BlanketPOLineIsReferencedInPOOrder(POLine currentLine, out POLine relatedPOLine)
  {
    relatedPOLine = PXResultset<POLine>.op_Implicit(PXSelectBase<POLine, PXViewOf<POLine>.BasedOn<SelectFromBase<POLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLine.pONbr, Equal<BqlField<POLine.orderNbr, IBqlString>.FromCurrent>>>>, And<BqlOperand<POLine.pOType, IBqlString>.IsEqual<BqlField<POLine.orderType, IBqlString>.FromCurrent>>>>.And<BqlOperand<POLine.pOLineNbr, IBqlInt>.IsEqual<BqlField<POLine.lineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) currentLine
    }, Array.Empty<object>()));
    return relatedPOLine != null;
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (string.Compare(viewName, "Transactions", true) == 0)
    {
      if (values.Contains((object) "orderType"))
        values[(object) "orderType"] = (object) ((PXSelectBase<POOrder>) this.Document).Current.OrderType;
      else
        values.Add((object) "orderType", (object) ((PXSelectBase<POOrder>) this.Document).Current.OrderType);
      if (values.Contains((object) "orderNbr"))
        values[(object) "orderNbr"] = (object) ((PXSelectBase<POOrder>) this.Document).Current.OrderNbr;
      else
        values.Add((object) "orderNbr", (object) ((PXSelectBase<POOrder>) this.Document).Current.OrderNbr);
      this._blockUIUpdate = true;
    }
    return true;
  }

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  [PXDefault(typeof (POOrder.orderDate))]
  [PXMergeAttributes]
  protected virtual void EPApproval_DocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (POOrder.vendorID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_BAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (Search<CREmployee.defContactID, Where<BqlOperand<CREmployee.defContactID, IBqlInt>.IsEqual<BqlField<POOrder.ownerID, IBqlInt>.FromCurrent>>>))]
  [PXMergeAttributes]
  protected virtual void EPApproval_DocumentOwnerID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (POOrder.orderDesc))]
  [PXMergeAttributes]
  protected virtual void EPApproval_Descr_CacheAttached(PXCache sender)
  {
  }

  [CurrencyInfo(typeof (POOrder.curyInfoID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (POOrder.curyOrderTotal))]
  [PXMergeAttributes]
  protected virtual void EPApproval_CuryTotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (POOrder.orderTotal))]
  [PXMergeAttributes]
  protected virtual void EPApproval_TotalAmount_CacheAttached(PXCache sender)
  {
  }

  public virtual bool IsExternalTax(string taxZoneID) => false;

  public virtual POOrder CalculateExternalTax(POOrder order) => order;

  protected virtual void InsertImportedTaxes()
  {
  }

  protected virtual void RecalculateDiscounts(PXCache sender, POLine line)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.vendorDiscounts>() && line.InventoryID.HasValue && line.OrderQty.HasValue && line.CuryLineAmt.HasValue)
    {
      DiscountEngine.DiscountCalculationOptions discountCalculationOptions = DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation | DiscountEngine.DiscountCalculationOptions.DisableARDiscountsCalculation;
      if (line.CalculateDiscountsOnImport.GetValueOrDefault())
        discountCalculationOptions |= DiscountEngine.DiscountCalculationOptions.CalculateDiscountsFromImport;
      this._discountEngine.SetDiscounts(sender, (PXSelectBase<POLine>) this.Transactions, line, (PXSelectBase<POOrderDiscountDetail>) this.DiscountDetails, ((PXSelectBase<POOrder>) this.Document).Current.BranchID, ((PXSelectBase<POOrder>) this.Document).Current.VendorLocationID, ((PXSelectBase<POOrder>) this.Document).Current.CuryID, ((PXSelectBase<POOrder>) this.Document).Current.OrderDate, ((PXSelectBase<RecalcDiscountsParamFilter>) this.recalcdiscountsfilter).Current, discountCalculationOptions);
      this.RecalculateTotalDiscount();
    }
    else
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.vendorDiscounts>() || ((PXSelectBase<POOrder>) this.Document).Current == null)
        return;
      this._discountEngine.CalculateDocumentDiscountRate(((PXSelectBase) this.Transactions).Cache, (PXSelectBase<POLine>) this.Transactions, line, (PXSelectBase<POOrderDiscountDetail>) this.DiscountDetails, DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation | DiscountEngine.DiscountCalculationOptions.DisableARDiscountsCalculation);
    }
  }

  private void RecalculateTotalDiscount()
  {
    if (((PXSelectBase<POOrder>) this.Document).Current == null)
      return;
    POOrder copy = PXCache<POOrder>.CreateCopy(((PXSelectBase<POOrder>) this.Document).Current);
    (Decimal groupDiscountTotal, Decimal documentDiscountTotal, Decimal discountTotal) discountTotals = this._discountEngine.GetDiscountTotals<POOrderDiscountDetail>((PXSelectBase<POOrderDiscountDetail>) this.DiscountDetails);
    ((PXSelectBase) this.Document).Cache.SetValueExt<PX.Objects.SO.SOOrder.curyGroupDiscTotal>((object) ((PXSelectBase<POOrder>) this.Document).Current, (object) discountTotals.groupDiscountTotal);
    ((PXSelectBase) this.Document).Cache.SetValueExt<PX.Objects.SO.SOOrder.curyDocumentDiscTotal>((object) ((PXSelectBase<POOrder>) this.Document).Current, (object) discountTotals.documentDiscountTotal);
    ((PXSelectBase) this.Document).Cache.SetValueExt<PX.Objects.SO.SOOrder.curyDiscTot>((object) ((PXSelectBase<POOrder>) this.Document).Current, (object) discountTotals.discountTotal);
    ((PXSelectBase) this.Document).Cache.RaiseRowUpdated((object) ((PXSelectBase<POOrder>) this.Document).Current, (object) copy);
  }

  public virtual bool IsVendorOrLocationChanged(PXCache sender, POOrder order)
  {
    return POOrderEntry.VendorUpdatedContext.IsScoped() || POOrderEntry.VendorLocationUpdatedContext.IsScoped();
  }

  public virtual bool HasDetailRecords()
  {
    if (((PXSelectBase<POLine>) this.Transactions).Current != null)
      return true;
    return ((PXSelectBase) this.Document).Cache.GetStatus((object) ((PXSelectBase<POOrder>) this.Document).Current) == 2 ? ((PXSelectBase) this.Transactions).Cache.IsDirty : ((PXSelectBase<POLine>) this.Transactions).Select(Array.Empty<object>()).Count > 0;
  }

  public virtual void CheckProjectAccountRule(PXCache sender, POLine row)
  {
    if (row.LineType == "DN" || POLineType.IsStock(row.LineType))
      return;
    int? projectId = row.ProjectID;
    int? nullable1 = ProjectDefaultAttribute.NonProject();
    if (projectId.GetValueOrDefault() == nullable1.GetValueOrDefault() & projectId.HasValue == nullable1.HasValue)
      return;
    PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>> pxSelect = new PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>((PXGraph) this);
    PX.Objects.GL.Account account = (PX.Objects.GL.Account) null;
    int? nullable2 = row.ExpenseAcctID;
    if (nullable2.HasValue)
      account = PXResultset<PX.Objects.GL.Account>.op_Implicit(((PXSelectBase<PX.Objects.GL.Account>) pxSelect).Select(new object[1]
      {
        (object) row.ExpenseAcctID
      }));
    if (account == null)
      return;
    nullable2 = account.AccountGroupID;
    if (nullable2.HasValue)
      return;
    sender.RaiseExceptionHandling<POLine.expenseAcctID>((object) row, (object) account.AccountCD, (Exception) new PXSetPropertyException("Account {0} is not mapped to any project account group. Either map the account or select a non-project code.", (PXErrorLevel) 4, new object[1]
    {
      (object) account.AccountCD
    }));
  }

  public virtual void Persist()
  {
    this._discountEngine.ValidateDiscountDetails((PXSelectBase<POOrderDiscountDetail>) this.DiscountDetails);
    bool? nullable1;
    if (((PXSelectBase<APSetup>) this.apsetup).Current.VendorPriceUpdate == "P" && ((PXSelectBase<POOrder>) this.Document).Current != null)
    {
      nullable1 = ((PXSelectBase<POOrder>) this.Document).Current.UpdateVendorCost;
      bool flag1 = false;
      if (!(nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue))
      {
        List<POLine> poLineList = new List<POLine>();
        foreach (POLine poLine in ((PXSelectBase) this.Transactions).Cache.Cached)
        {
          bool flag2 = EnumerableExtensions.IsIn<string>(poLine.OrderType, "RO", "DP", "PD");
          if (((!EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.Transactions).Cache.GetStatus((object) poLine), (PXEntryStatus) 2, (PXEntryStatus) 1) ? 0 : (poLine.InventoryID.HasValue ? 1 : 0)) & (flag2 ? 1 : 0)) != 0)
          {
            Decimal? curyUnitCost = poLine.CuryUnitCost;
            Decimal num = 0M;
            if (curyUnitCost.GetValueOrDefault() > num & curyUnitCost.HasValue)
              poLineList.Add(poLine);
          }
        }
        poLineList.Sort((Comparison<POLine>) ((x, y) => x.LineNbr.Value.CompareTo(y.LineNbr.Value)));
        foreach (POLine poLine in poLineList)
          POItemCostManager.Update((PXGraph) this, ((PXSelectBase<POOrder>) this.Document).Current.VendorID, ((PXSelectBase<POOrder>) this.Document).Current.VendorLocationID, ((PXSelectBase<POOrder>) this.Document).Current.CuryID, poLine.InventoryID, poLine.SubItemID, poLine.UOM, poLine.CuryUnitCost.Value);
      }
    }
    ((PXGraph) this).GetExtension<POOrderEntry.SO2POSync>().Process((((PXSelectBase<POOrder>) this.Document).Current?.OrderType, ((PXSelectBase<POOrder>) this.Document).Current?.OrderNbr));
    this.ClearPOLinePlanIDIfPlanIsDeleted();
    foreach (POLine poLine1 in ((PXSelectBase) this.Transactions).Cache.Cached)
    {
      if (((PXSelectBase) this.Transactions).Cache.GetStatus((object) poLine1) == 2 || ((PXSelectBase) this.Transactions).Cache.GetStatus((object) poLine1) == 1)
      {
        POLine poLine2 = poLine1;
        nullable1 = poLine1.HasInclusiveTaxes;
        int num1;
        if (!nullable1.GetValueOrDefault())
        {
          Decimal? curyRetainageAmt = poLine1.CuryRetainageAmt;
          Decimal num2 = 0M;
          num1 = curyRetainageAmt.GetValueOrDefault() == num2 & curyRetainageAmt.HasValue ? 1 : 0;
        }
        else
          num1 = 0;
        bool? nullable2 = new bool?(num1 != 0);
        poLine2.AllowEditUnitCostInPR = nullable2;
      }
    }
    this.InsertImportedTaxes();
    ((PXGraph) this).Persist();
  }

  protected virtual void ClearPOLinePlanIDIfPlanIsDeleted()
  {
    PXCache<INItemPlan> planCache = GraphHelper.Caches<INItemPlan>((PXGraph) this);
    HashSet<long> hashSet = GraphHelper.RowCast<INItemPlan>(((PXCache) planCache).Cached).Where<INItemPlan>((Func<INItemPlan, bool>) (p => EnumerableExtensions.IsIn<PXEntryStatus>(planCache.GetStatus(p), (PXEntryStatus) 3, (PXEntryStatus) 4) && p.PlanID.HasValue)).Select<INItemPlan, long>((Func<INItemPlan, long>) (p => p.PlanID.Value)).ToHashSet<long>();
    if (!hashSet.Any<long>())
      return;
    foreach (PXResult<POLine> pxResult in ((PXSelectBase<POLine>) this.Transactions).Select(Array.Empty<object>()))
    {
      POLine poLine1 = PXResult<POLine>.op_Implicit(pxResult);
      long? nullable1 = poLine1.PlanID;
      if (nullable1.HasValue)
      {
        HashSet<long> longSet = hashSet;
        nullable1 = poLine1.PlanID;
        long num = nullable1.Value;
        if (longSet.Contains(num))
        {
          POLine poLine2 = poLine1;
          nullable1 = new long?();
          long? nullable2 = nullable1;
          poLine2.PlanID = nullable2;
          GraphHelper.MarkUpdated(((PXSelectBase) this.Transactions).Cache, (object) poLine1, true);
        }
      }
    }
  }

  public virtual bool IsDropShipOrSingleProject(POOrder order)
  {
    if (order?.OrderType == "PD")
      return true;
    if (order == null)
      return false;
    bool? multipleProjects = order.HasMultipleProjects;
    bool flag = false;
    return multipleProjects.GetValueOrDefault() == flag & multipleProjects.HasValue;
  }

  private bool IsItemReceivedOrAPDocCreated(POLine row)
  {
    Decimal? receivedQty = row.ReceivedQty;
    Decimal num = 0M;
    if (receivedQty.GetValueOrDefault() > num & receivedQty.HasValue)
      return true;
    if (PXResultset<POReceiptLine>.op_Implicit(PXSelectBase<POReceiptLine, PXViewOf<POReceiptLine>.BasedOn<SelectFromBase<POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POReceiptLine.pOType, Equal<BqlField<POLine.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<POReceiptLine.pONbr, IBqlString>.IsEqual<BqlField<POLine.orderNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<POReceiptLine.pOLineNbr, IBqlInt>.IsEqual<BqlField<POLine.lineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>())) != null)
      return true;
    return PXResultset<PX.Objects.AP.APTran>.op_Implicit(PXSelectBase<PX.Objects.AP.APTran, PXViewOf<PX.Objects.AP.APTran>.BasedOn<SelectFromBase<PX.Objects.AP.APTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APTran.pOOrderType, Equal<BqlField<POLine.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.AP.APTran.pONbr, IBqlString>.IsEqual<BqlField<POLine.orderNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.AP.APTran.pOLineNbr, IBqlInt>.IsEqual<BqlField<POLine.lineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>())) != null;
  }

  private bool HasReceiptUnderCorrection(POOrder order)
  {
    return PXResultset<POReceipt>.op_Implicit(PXSelectBase<POReceipt, PXViewOf<POReceipt>.BasedOn<SelectFromBase<POReceipt, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<POOrderReceipt>.On<POOrderReceipt.FK.Receipt>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POOrderReceipt.pOType, Equal<BqlField<POOrder.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<POOrderReceipt.pONbr, IBqlString>.IsEqual<BqlField<POOrder.orderNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<POReceipt.isUnderCorrection, IBqlBool>.IsEqual<True>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) new POOrder[1]
    {
      order
    }, Array.Empty<object>())) != null;
  }

  public class CostAccrual : NonStockAccrualGraph<POOrderEntry, POOrder>
  {
    [PXOverride]
    public virtual void SetExpenseAccount(
      PXCache sender,
      PXFieldDefaultingEventArgs e,
      PX.Objects.IN.InventoryItem item,
      System.Action<PXCache, PXFieldDefaultingEventArgs, PX.Objects.IN.InventoryItem> baseMethod)
    {
      POLine row = (POLine) e.Row;
      if (row == null || !row.AccrueCost.GetValueOrDefault())
        return;
      this.SetExpenseAccountSub(sender, e, item, row.SiteID, (NonStockAccrualGraph<POOrderEntry, POOrder>.GetAccountSubUsingPostingClassDelegate) ((inItem, inSite, inPostClass) => (object) INReleaseProcess.GetAcctID<INPostClass.invtAcctID>((PXGraph) this.Base, inPostClass.InvtAcctDefault, inItem, inSite, inPostClass)), (NonStockAccrualGraph<POOrderEntry, POOrder>.GetAccountSubFromItemDelegate) (inItem => (object) inItem.InvtAcctID));
    }

    [PXOverride]
    public virtual object GetExpenseSub(
      PXCache sender,
      PXFieldDefaultingEventArgs e,
      PX.Objects.IN.InventoryItem item,
      Func<PXCache, PXFieldDefaultingEventArgs, PX.Objects.IN.InventoryItem, object> baseMethod)
    {
      POLine row = (POLine) e.Row;
      object expenseSub = (object) null;
      if (row != null && row.AccrueCost.GetValueOrDefault())
        expenseSub = this.GetExpenseAccountSub(sender, e, item, row.SiteID, (NonStockAccrualGraph<POOrderEntry, POOrder>.GetAccountSubUsingPostingClassDelegate) ((inItem, inSite, inPostClass) => (object) INReleaseProcess.GetSubID<INPostClass.invtSubID>((PXGraph) this.Base, inPostClass.InvtAcctDefault, inPostClass.InvtSubMask, inItem, inSite, inPostClass)), (NonStockAccrualGraph<POOrderEntry, POOrder>.GetAccountSubFromItemDelegate) (inItem => (object) inItem.InvtSubID));
      return expenseSub;
    }
  }

  public class MultiCurrency : MultiCurrencyGraph<POOrderEntry, POOrder>
  {
    protected override string Module => "PO";

    protected override MultiCurrencyGraph<POOrderEntry, POOrder>.CurySourceMapping GetCurySourceMapping()
    {
      return new MultiCurrencyGraph<POOrderEntry, POOrder>.CurySourceMapping(typeof (PX.Objects.AP.Vendor));
    }

    protected override MultiCurrencyGraph<POOrderEntry, POOrder>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<POOrderEntry, POOrder>.DocumentMapping(typeof (POOrder))
      {
        DocumentDate = typeof (POOrder.orderDate),
        BAccountID = typeof (POOrder.vendorID)
      };
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[5]
      {
        (PXSelectBase) this.Base.Document,
        (PXSelectBase) this.Base.Transactions,
        (PXSelectBase) this.Base.Tax_Rows,
        (PXSelectBase) this.Base.Taxes,
        (PXSelectBase) this.Base.DiscountDetails
      };
    }

    protected override PXSelectBase[] GetTrackedExceptChildren()
    {
      return new PXSelectBase[1]
      {
        (PXSelectBase) this.Base.poLiner
      };
    }

    protected override bool AllowOverrideRate(PXCache sender, PX.Objects.CM.Extensions.CurrencyInfo info, CurySource source)
    {
      if (!base.AllowOverrideRate(sender, info, source))
        return false;
      POOrder current = ((PXSelectBase<POOrder>) this.Base.Document).Current;
      return current != null && current.Hold.GetValueOrDefault();
    }

    protected override bool AllowOverrideCury()
    {
      return base.AllowOverrideCury() || ((PXGraph) this.Base).IsCopyPasteContext || PXUIFieldAttribute.GetErrorWithLevel<POOrder.curyID>(((PXSelectBase) this.Base.Document).Cache, (object) ((PXSelectBase<POOrder>) this.Base.Document).Current).Item2 == 4;
    }

    [PXOverride]
    public virtual void POOrder_VendorID_FieldUpdated(
      PXCache sender,
      PXFieldUpdatedEventArgs e,
      PXFieldUpdated baseMethod)
    {
      POOrder row = (POOrder) e.Row;
      if (e.ExternalCall || row == null || row.CuryID == null || row.OverrideCurrency.GetValueOrDefault())
        this.SourceFieldUpdated<POOrder.curyInfoID, POOrder.curyID, POOrder.orderDate>(sender, (IBqlTable) row);
      baseMethod.Invoke(sender, e);
    }

    protected override void _(
      PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.bAccountID> e)
    {
    }

    protected override void _(PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.branchID> e)
    {
      PX.Objects.Extensions.MultiCurrency.Document row = e.Row;
      bool resetCuryID = (row != null ? (!row.BAccountID.HasValue ? 1 : 0) : 1) != 0 && !((PXGraph) this.Base).IsCopyPasteContext && (((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.branchID>>) e).ExternalCall || e.Row?.CuryID == null);
      this.SourceFieldUpdated<PX.Objects.Extensions.MultiCurrency.Document.curyInfoID, PX.Objects.Extensions.MultiCurrency.Document.curyID, PX.Objects.Extensions.MultiCurrency.Document.documentDate>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.branchID>>) e).Cache, (IBqlTable) e.Row, resetCuryID);
    }

    protected override void _(PX.Data.Events.FieldVerifying<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.curyID> e)
    {
      string newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.curyID>, PX.Objects.Extensions.MultiCurrency.Document, object>) e).NewValue as string;
      if (((PXSelectBase<PX.Objects.AP.Vendor>) this.Base.vendor).Current != null && !((PXSelectBase<PX.Objects.AP.Vendor>) this.Base.vendor).Current.AllowOverrideCury.Value && newValue != ((PXSelectBase<PX.Objects.AP.Vendor>) this.Base.vendor).Current.CuryID)
        throw new PXSetPropertyException("The currency '{1}' of the vendor '{0}' differs from currency '{2}' of the document.", new object[3]
        {
          (object) ((PXSelectBase<PX.Objects.AP.Vendor>) this.Base.vendor).Current.AcctCD,
          (object) ((PXSelectBase<PX.Objects.AP.Vendor>) this.Base.vendor).Current.CuryID,
          (object) newValue
        });
      base._(e);
    }

    protected override void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyID> e)
    {
      base._(e);
      if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
        return;
      if (e.Row != null && this.Base.HasDetailRecords())
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyID>, PX.Objects.CM.Extensions.CurrencyInfo, object>) e).NewValue = (object) (e.Row.CuryID ?? ((PXSelectBase<PX.Objects.AP.Vendor>) this.Base.vendor)?.Current?.CuryID);
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyID>>) e).Cancel = true;
      }
      else
      {
        if (((PXSelectBase<PX.Objects.AP.Vendor>) this.Base.vendor).Current == null || string.IsNullOrEmpty(((PXSelectBase<PX.Objects.AP.Vendor>) this.Base.vendor).Current.CuryID))
          return;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyID>, PX.Objects.CM.Extensions.CurrencyInfo, object>) e).NewValue = (object) ((PXSelectBase<PX.Objects.AP.Vendor>) this.Base.vendor).Current.CuryID;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyID>>) e).Cancel = true;
      }
    }

    protected override void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID> e)
    {
      base._(e);
      if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
        return;
      CMSetup cmSetup = PXResultset<CMSetup>.op_Implicit(PXSelectBase<CMSetup, PXSelect<CMSetup>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
      if (e.Row != null && this.Base.HasDetailRecords())
      {
        PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID> fieldDefaulting = e;
        object obj = (object) e.Row.CuryRateTypeID;
        if (obj == null)
        {
          string curyRateTypeId = ((PXSelectBase<PX.Objects.AP.Vendor>) this.Base.vendor)?.Current?.CuryRateTypeID;
          if (curyRateTypeId == null)
          {
            string apRateTypeDflt = cmSetup?.APRateTypeDflt;
            obj = apRateTypeDflt != null ? (object) apRateTypeDflt : ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>, PX.Objects.CM.Extensions.CurrencyInfo, object>) e).NewValue;
          }
          else
            obj = (object) curyRateTypeId;
        }
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>, PX.Objects.CM.Extensions.CurrencyInfo, object>) fieldDefaulting).NewValue = obj;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>>) e).Cancel = true;
      }
      else if (((PXSelectBase<PX.Objects.AP.Vendor>) this.Base.vendor).Current != null && !string.IsNullOrEmpty(((PXSelectBase<PX.Objects.AP.Vendor>) this.Base.vendor).Current.CuryRateTypeID))
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>, PX.Objects.CM.Extensions.CurrencyInfo, object>) e).NewValue = (object) ((PXSelectBase<PX.Objects.AP.Vendor>) this.Base.vendor).Current.CuryRateTypeID;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>>) e).Cancel = true;
      }
      else
      {
        if (cmSetup == null)
          return;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>, PX.Objects.CM.Extensions.CurrencyInfo, object>) e).NewValue = (object) cmSetup.APRateTypeDflt;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyRateTypeID>>) e).Cancel = true;
      }
    }

    protected override void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate> e)
    {
      base._(e);
      if (((PXSelectBase<POOrder>) this.Base.Document).Current == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>, PX.Objects.CM.Extensions.CurrencyInfo, object>) e).NewValue = (object) ((PXSelectBase<POOrder>) this.Base.Document).Current.OrderDate;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CM.Extensions.CurrencyInfo.curyEffDate>>) e).Cancel = true;
    }
  }

  public class SO2POSync : SO2POSyncFromPOOrderExtension<POOrderEntry>
  {
  }

  public class VendorUpdatedContext : IDisposable
  {
    private const string isVendorUpdatedSlotName = "IsVendorUpdatedContext";

    public VendorUpdatedContext() => PXContext.SetSlot<bool>("IsVendorUpdatedContext", true);

    public void Dispose() => PXContext.SetSlot<bool>("IsVendorUpdatedContext", false);

    public static bool IsScoped() => PXContext.GetSlot<bool>("IsVendorUpdatedContext");
  }

  public class VendorLocationUpdatedContext : IDisposable
  {
    private const string isVendorLocationUpdatedSlotName = "IsVendorLocationUpdatedContext";

    public VendorLocationUpdatedContext()
    {
      PXContext.SetSlot<bool>("IsVendorLocationUpdatedContext", true);
    }

    public void Dispose() => PXContext.SetSlot<bool>("IsVendorLocationUpdatedContext", false);

    public static bool IsScoped() => PXContext.GetSlot<bool>("IsVendorLocationUpdatedContext");
  }

  public class SuppressOrderEventsScope(PXGraph graph) : 
    CounterScope<POOrderEntry.SuppressOrderEventsScope>(graph)
  {
  }

  [Serializable]
  public class POOrderFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _VendorID;
    protected string _OrderType;
    protected string _OrderNbr;

    [VendorActive]
    [PXDefault(typeof (POOrder.vendorID))]
    public virtual int? VendorID
    {
      get => this._VendorID;
      set => this._VendorID = value;
    }

    [PXDBString(2, IsFixed = true)]
    [PXDefault("BL")]
    [POOrderType.BlanketList]
    [PXUIField]
    public virtual string OrderType
    {
      get => this._OrderType;
      set => this._OrderType = value;
    }

    [PXDBString(15, IsUnicode = true, InputMask = "")]
    [PXDefault]
    [PXUIField]
    [PX.Objects.PO.PO.RefNbr(typeof (Search2<POOrderEntry.POOrderS.orderNbr, CrossJoin<APSetup, InnerJoin<PX.Objects.AP.Vendor, On<POOrderEntry.POOrderS.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>>, Where<POOrderEntry.POOrderS.orderType, Equal<Current<POOrderEntry.POOrderFilter.orderType>>, And<POOrderEntry.POOrderS.vendorID, Equal<Current<POOrder.vendorID>>, And<POOrderEntry.POOrderS.vendorLocationID, Equal<Current<POOrder.vendorLocationID>>, And<POOrderEntry.POOrderS.curyID, Equal<Current<POOrder.curyID>>, And<POOrderEntry.POOrderS.hold, Equal<boolFalse>, And<POOrderEntry.POOrderS.cancelled, Equal<boolFalse>, And<POOrderEntry.POOrderS.approved, Equal<boolTrue>, And2<Where<POOrderEntry.POOrderS.orderQty, Equal<decimal0>, Or<POOrderEntry.POOrderS.openOrderQty, Greater<decimal0>>>, And2<Where<POOrderEntry.POOrderS.orderType, Equal<POOrderType.blanket>, Or<POOrderEntry.POOrderS.orderType, Equal<POOrderType.standardBlanket>>>, And<Where<POOrderEntry.POOrderS.payToVendorID, Equal<Current<POOrder.payToVendorID>>, Or<Not<FeatureInstalled<FeaturesSet.vendorRelations>>>>>>>>>>>>>>>), Filterable = true)]
    public virtual string OrderNbr
    {
      get => this._OrderNbr;
      set => this._OrderNbr = value;
    }

    public abstract class vendorID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrderEntry.POOrderFilter.vendorID>
    {
    }

    public abstract class orderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.POOrderFilter.orderType>
    {
    }

    public abstract class orderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.POOrderFilter.orderNbr>
    {
    }
  }

  [PXProjection(typeof (Select<POLine>), Persistent = false)]
  [PXCacheName("PO Line to Add")]
  [Serializable]
  public class POLineS : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISortOrder
  {
    protected bool? _Selected = new bool?(false);
    protected int? _BranchID;
    protected string _OrderType;
    protected string _OrderNbr;
    protected int? _LineNbr;
    protected int? _SortOrder;
    protected string _LineType;
    protected int? _InventoryID;
    protected int? _SubItemID;
    protected int? _SiteID;
    protected string _UOM;
    protected Decimal? _OrderQty;
    protected Decimal? _BaseOrderQty;
    protected Decimal? _ReceivedQty;
    protected Decimal? _BaseReceivedQty;
    protected long? _CuryInfoID;
    protected Decimal? _CuryUnitCost;
    protected Decimal? _UnitCost;
    protected bool? _ManualPrice;
    protected string _TaxCategoryID;
    protected int? _ExpenseAcctID;
    protected int? _ExpenseSubID;
    protected string _AlternateID;
    protected string _TranDesc;
    protected Decimal? _UnitWeight;
    protected Decimal? _UnitVolume;
    protected int? _ProjectID;
    protected int? _TaskID;
    protected int? _CostCodeID;
    protected Decimal? _RcptQtyMin;
    protected Decimal? _RcptQtyMax;
    protected Decimal? _RcptQtyThreshold;
    protected string _RcptQtyAction;
    protected bool? _Cancelled;
    protected bool? _Completed;

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Selected")]
    public virtual bool? Selected
    {
      get => this._Selected;
      set => this._Selected = value;
    }

    [Branch(null, null, true, true, true, BqlField = typeof (POLine.branchID))]
    public virtual int? BranchID
    {
      get => this._BranchID;
      set => this._BranchID = value;
    }

    [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (POLine.orderType))]
    [PXDBDefault(typeof (POOrder.orderType))]
    [PXUIField]
    public virtual string OrderType
    {
      get => this._OrderType;
      set => this._OrderType = value;
    }

    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (POLine.orderNbr))]
    [PXDBDefault(typeof (POOrder.orderNbr))]
    [PXUIField]
    public virtual string OrderNbr
    {
      get => this._OrderNbr;
      set => this._OrderNbr = value;
    }

    [PXDBInt(IsKey = true, BqlField = typeof (POLine.lineNbr))]
    [PXUIField]
    [PXLineNbr(typeof (POOrder.lineCntr))]
    public virtual int? LineNbr
    {
      get => this._LineNbr;
      set => this._LineNbr = value;
    }

    [PXDBInt(BqlField = typeof (POLine.sortOrder))]
    public virtual int? SortOrder
    {
      get => this._SortOrder;
      set => this._SortOrder = value;
    }

    [PXDBString(2, IsFixed = true, BqlField = typeof (POLine.lineType))]
    [PXDefault("GI")]
    [POLineType.List]
    [PXUIField(DisplayName = "Line Type")]
    public virtual string LineType
    {
      get => this._LineType;
      set => this._LineType = value;
    }

    [POLineInventoryItem(Filterable = true, BqlField = typeof (POLine.inventoryID))]
    public virtual int? InventoryID
    {
      get => this._InventoryID;
      set => this._InventoryID = value;
    }

    [SubItem(typeof (POOrderEntry.POLineS.inventoryID), BqlField = typeof (POLine.subItemID))]
    public virtual int? SubItemID
    {
      get => this._SubItemID;
      set => this._SubItemID = value;
    }

    [SiteAvail(typeof (POOrderEntry.POLineS.inventoryID), typeof (POOrderEntry.POLineS.subItemID), typeof (POOrderEntry.POLineS.costCenterID), BqlField = typeof (POLine.siteID))]
    public virtual int? SiteID
    {
      get => this._SiteID;
      set => this._SiteID = value;
    }

    [INUnit(typeof (POOrderEntry.POLineS.inventoryID), DisplayName = "UOM", BqlField = typeof (POLine.uOM))]
    public virtual string UOM
    {
      get => this._UOM;
      set => this._UOM = value;
    }

    [PXDBQuantity(typeof (POOrderEntry.POLineS.uOM), typeof (POOrderEntry.POLineS.baseOrderQty), HandleEmptyKey = true, BqlField = typeof (POLine.orderQty))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXFormula(null, typeof (SumCalc<POOrder.orderQty>))]
    [PXUIField]
    public virtual Decimal? OrderQty
    {
      get => this._OrderQty;
      set => this._OrderQty = value;
    }

    [PXDBDecimal(6, BqlField = typeof (POLine.baseOrderQty))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? BaseOrderQty
    {
      get => this._BaseOrderQty;
      set => this._BaseOrderQty = value;
    }

    [PXDBQuantity(typeof (POOrderEntry.POLineS.uOM), typeof (POOrderEntry.POLineS.baseReceivedQty), HandleEmptyKey = true, BqlField = typeof (POLine.receivedQty))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField]
    public virtual Decimal? ReceivedQty
    {
      get => this._ReceivedQty;
      set => this._ReceivedQty = value;
    }

    [PXDBDecimal(6, BqlField = typeof (POLine.baseReceivedQty))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField]
    public virtual Decimal? BaseReceivedQty
    {
      get => this._BaseReceivedQty;
      set => this._BaseReceivedQty = value;
    }

    [PXDBQuantity(BqlField = typeof (POLine.openQty))]
    [PXUIField(DisplayName = "Open Qty.", Enabled = false)]
    public virtual Decimal? OpenQty { get; set; }

    [PXDBLong(BqlField = typeof (POLine.curyInfoID))]
    public virtual long? CuryInfoID
    {
      get => this._CuryInfoID;
      set => this._CuryInfoID = value;
    }

    [PXDBCurrency(typeof (POOrderEntry.POLineS.curyInfoID), typeof (POOrderEntry.POLineS.unitCost), BqlField = typeof (POLine.curyUnitCost))]
    [PXUIField]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? CuryUnitCost
    {
      get => this._CuryUnitCost;
      set => this._CuryUnitCost = value;
    }

    [PXDBPriceCost(BqlField = typeof (POLine.unitCost))]
    public virtual Decimal? UnitCost
    {
      get => this._UnitCost;
      set => this._UnitCost = value;
    }

    [PXDBBool(BqlField = typeof (POLine.manualPrice))]
    [PXDefault(false)]
    [PXUIField]
    public virtual bool? ManualPrice
    {
      get => this._ManualPrice;
      set => this._ManualPrice = value;
    }

    [PXDBCurrency(typeof (POLine.curyInfoID), typeof (POLine.lineAmt), BqlField = typeof (POLine.curyLineAmt))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Ext. Cost")]
    public virtual Decimal? CuryLineAmt { get; set; }

    [PXDBDecimal(4, BqlField = typeof (POLine.lineAmt))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? LineAmt { get; set; }

    [PXDBString(15, IsUnicode = true, BqlField = typeof (POLine.taxCategoryID))]
    [PXUIField]
    [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
    [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
    public virtual string TaxCategoryID
    {
      get => this._TaxCategoryID;
      set => this._TaxCategoryID = value;
    }

    [Account]
    public virtual int? ExpenseAcctID
    {
      get => this._ExpenseAcctID;
      set => this._ExpenseAcctID = value;
    }

    [SubAccount(typeof (POOrderEntry.POLineS.expenseAcctID))]
    public virtual int? ExpenseSubID
    {
      get => this._ExpenseSubID;
      set => this._ExpenseSubID = value;
    }

    [PXDBString(50, IsUnicode = true, BqlField = typeof (POLine.alternateID), InputMask = "")]
    [PXUIField(DisplayName = "Alternate ID")]
    public virtual string AlternateID
    {
      get => this._AlternateID;
      set => this._AlternateID = value;
    }

    [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (POLine.tranDesc))]
    [PXUIField]
    public virtual string TranDesc
    {
      get => this._TranDesc;
      set => this._TranDesc = value;
    }

    [PXDBDecimal(6, BqlField = typeof (POLine.unitWeight))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Unit Weight")]
    public virtual Decimal? UnitWeight
    {
      get => this._UnitWeight;
      set => this._UnitWeight = value;
    }

    [PXDBDecimal(6, BqlField = typeof (POLine.unitVolume))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Unit Volume")]
    public virtual Decimal? UnitVolume
    {
      get => this._UnitVolume;
      set => this._UnitVolume = value;
    }

    [POProjectDefault(typeof (POLine.lineType))]
    [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PMProject.contractCD)})]
    [PXRestrictor(typeof (Where<PMProject.visibleInPO, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
    [ProjectBase(BqlField = typeof (POLine.projectID))]
    public virtual int? ProjectID
    {
      get => this._ProjectID;
      set => this._ProjectID = value;
    }

    [ActiveProjectTask(typeof (POLine.projectID), "PO", DisplayName = "Project Task", BqlField = typeof (POLine.taskID))]
    public virtual int? TaskID
    {
      get => this._TaskID;
      set => this._TaskID = value;
    }

    [CostCode(typeof (POOrderEntry.POLineS.expenseAcctID), typeof (POOrderEntry.POLineS.taskID), DisplayName = "Cost Code", BqlField = typeof (POLine.costCodeID))]
    public virtual int? CostCodeID { get; set; }

    [PXDBDecimal(2, MinValue = 0.0, MaxValue = 999.0, BqlField = typeof (POLine.rcptQtyMin))]
    [PXDefault(typeof (Search<PX.Objects.CR.Location.vRcptQtyMin, Where<PX.Objects.CR.Location.locationID, Equal<Current<POOrder.vendorLocationID>>, And<PX.Objects.CR.Location.bAccountID, Equal<Current<POOrder.vendorID>>>>>))]
    [PXUIField(DisplayName = "Min. Receipt (%)")]
    public virtual Decimal? RcptQtyMin
    {
      get => this._RcptQtyMin;
      set => this._RcptQtyMin = value;
    }

    [PXDBDecimal(2, MinValue = 0.0, MaxValue = 999.0, BqlField = typeof (POLine.rcptQtyMax))]
    [PXDefault(typeof (Search<PX.Objects.CR.Location.vRcptQtyMax, Where<PX.Objects.CR.Location.locationID, Equal<Current<POOrder.vendorLocationID>>, And<PX.Objects.CR.Location.bAccountID, Equal<Current<POOrder.vendorID>>>>>))]
    [PXUIField(DisplayName = "Max. Receipt (%)")]
    public virtual Decimal? RcptQtyMax
    {
      get => this._RcptQtyMax;
      set => this._RcptQtyMax = value;
    }

    [PXDBDecimal(2, MinValue = 0.0, MaxValue = 999.0, BqlField = typeof (POLine.rcptQtyThreshold))]
    [PXDefault(typeof (Search<PX.Objects.CR.Location.vRcptQtyThreshold, Where<PX.Objects.CR.Location.locationID, Equal<Current<POOrder.vendorLocationID>>, And<PX.Objects.CR.Location.bAccountID, Equal<Current<POOrder.vendorID>>>>>))]
    [PXUIField(DisplayName = "Complete On (%)")]
    public virtual Decimal? RcptQtyThreshold
    {
      get => this._RcptQtyThreshold;
      set => this._RcptQtyThreshold = value;
    }

    [PXDBString(1, IsFixed = true, BqlField = typeof (POLine.rcptQtyAction))]
    [POReceiptQtyAction.List]
    [PXDefault(typeof (Search<PX.Objects.CR.Location.vRcptQtyAction, Where<PX.Objects.CR.Location.locationID, Equal<Current<POOrder.vendorLocationID>>, And<PX.Objects.CR.Location.bAccountID, Equal<Current<POOrder.vendorID>>>>>))]
    [PXUIField(DisplayName = "Receipt Action")]
    public virtual string RcptQtyAction
    {
      get => this._RcptQtyAction;
      set => this._RcptQtyAction = value;
    }

    [PXDBBool(BqlField = typeof (POLine.cancelled))]
    [PXUIField]
    [PXDefault(false)]
    public virtual bool? Cancelled
    {
      get => this._Cancelled;
      set => this._Cancelled = value;
    }

    [PXDBBool(BqlField = typeof (POLine.completed))]
    [PXUIField]
    [PXDefault(false)]
    public virtual bool? Completed
    {
      get => this._Completed;
      set => this._Completed = value;
    }

    [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0, BqlField = typeof (POLine.retainagePct))]
    [PXUIField(DisplayName = "Retainage Percent", FieldClass = "Retainage")]
    public virtual Decimal? RetainagePct { get; set; }

    [PXDBCurrency(typeof (POOrderEntry.POLineS.curyInfoID), typeof (POOrderEntry.POLineS.retainageAmt), BqlField = typeof (POLine.curyRetainageAmt))]
    [PXUIField(DisplayName = "Retainage Amount", FieldClass = "Retainage")]
    public virtual Decimal? CuryRetainageAmt { get; set; }

    [PXDBBaseCury(BqlField = typeof (POLine.retainageAmt))]
    public virtual Decimal? RetainageAmt { get; set; }

    [PXDBInt(BqlField = typeof (POLine.costCenterID))]
    public virtual int? CostCenterID { get; set; }

    public abstract class selected : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrderEntry.POLineS.selected>
    {
    }

    public abstract class branchID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrderEntry.POLineS.branchID>
    {
    }

    public abstract class orderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.POLineS.orderType>
    {
    }

    public abstract class orderNbr : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderEntry.POLineS.orderNbr>
    {
    }

    public abstract class lineNbr : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrderEntry.POLineS.lineNbr>
    {
    }

    public abstract class sortOrder : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrderEntry.POLineS.sortOrder>
    {
    }

    public abstract class lineType : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderEntry.POLineS.lineType>
    {
    }

    public abstract class inventoryID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrderEntry.POLineS.inventoryID>
    {
    }

    public abstract class subItemID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrderEntry.POLineS.subItemID>
    {
    }

    public abstract class siteID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrderEntry.POLineS.siteID>
    {
    }

    public abstract class uOM : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderEntry.POLineS.uOM>
    {
    }

    public abstract class orderQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POLineS.orderQty>
    {
    }

    public abstract class baseOrderQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POLineS.baseOrderQty>
    {
    }

    public abstract class receivedQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POLineS.receivedQty>
    {
    }

    public abstract class baseReceivedQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POLineS.baseReceivedQty>
    {
    }

    public abstract class openQty : BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrderEntry.POLineS.openQty>
    {
    }

    public abstract class curyInfoID : BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    POOrderEntry.POLineS.curyInfoID>
    {
    }

    public abstract class curyUnitCost : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POLineS.curyUnitCost>
    {
    }

    public abstract class unitCost : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POLineS.unitCost>
    {
    }

    public abstract class manualPrice : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POOrderEntry.POLineS.manualPrice>
    {
    }

    public abstract class curyLineAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POLineS.curyLineAmt>
    {
    }

    public abstract class lineAmt : BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrderEntry.POLineS.lineAmt>
    {
    }

    public abstract class taxCategoryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.POLineS.taxCategoryID>
    {
    }

    public abstract class expenseAcctID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POOrderEntry.POLineS.expenseAcctID>
    {
    }

    public abstract class expenseSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POOrderEntry.POLineS.expenseSubID>
    {
    }

    public abstract class alternateID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.POLineS.alternateID>
    {
    }

    public abstract class tranDesc : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderEntry.POLineS.tranDesc>
    {
    }

    public abstract class unitWeight : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POLineS.unitWeight>
    {
    }

    public abstract class unitVolume : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POLineS.unitVolume>
    {
    }

    public abstract class projectID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrderEntry.POLineS.projectID>
    {
    }

    public abstract class taskID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrderEntry.POLineS.taskID>
    {
    }

    public abstract class costCodeID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrderEntry.POLineS.costCodeID>
    {
    }

    public abstract class rcptQtyMin : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POLineS.rcptQtyMin>
    {
    }

    public abstract class rcptQtyMax : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POLineS.rcptQtyMax>
    {
    }

    public abstract class rcptQtyThreshold : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POLineS.rcptQtyThreshold>
    {
    }

    public abstract class rcptQtyAction : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.POLineS.rcptQtyAction>
    {
    }

    public abstract class cancelled : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrderEntry.POLineS.cancelled>
    {
    }

    public abstract class completed : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrderEntry.POLineS.completed>
    {
    }

    public abstract class retainagePct : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POLineS.retainagePct>
    {
    }

    public abstract class curyRetainageAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POLineS.curyRetainageAmt>
    {
    }

    public abstract class retainageAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POLineS.retainageAmt>
    {
    }

    public abstract class costCenterID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POOrderEntry.POLineS.costCenterID>
    {
    }
  }

  [PXProjection(typeof (Select5<POOrder, InnerJoin<POLine, On<POLine.orderType, Equal<POOrder.orderType>, And<POLine.orderNbr, Equal<POOrder.orderNbr>, And<POLine.cancelled, NotEqual<boolTrue>, And<POLine.completed, NotEqual<boolTrue>, And2<Where<POLine.orderQty, Equal<decimal0>, Or<POLine.openQty, Greater<decimal0>>>, And<Where<POOrder.orderType, Equal<POOrderType.standardBlanket>, Or<POLine.lineType, NotEqual<POLineType.description>>>>>>>>>>, Where<POOrder.orderType, Equal<POOrderType.blanket>, Or<POOrder.orderType, Equal<POOrderType.standardBlanket>>>, Aggregate<GroupBy<POOrder.orderType, GroupBy<POOrder.orderNbr, GroupBy<POOrder.orderDate, GroupBy<POOrder.curyID, GroupBy<POOrder.curyOrderTotal, GroupBy<POOrder.hold, GroupBy<POOrder.cancelled, GroupBy<POOrder.approved, GroupBy<POOrder.isTaxValid, GroupBy<POOrder.isUnbilledTaxValid, Max<POOrder.openOrderQty, Sum<POLine.orderQty, Sum<POLine.baseOrderQty, Sum<POLine.receivedQty, Sum<POLine.baseReceivedQty, Sum<POLine.curyExtCost, Sum<POLine.extCost, Sum<POLine.curyBLOrderedCost, Sum<POLine.bLOrderedCost>>>>>>>>>>>>>>>>>>>>>), Persistent = false)]
  [Serializable]
  public class POOrderS : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected bool? _Selected = new bool?(false);
    protected Decimal? _ReceivedQty;
    protected Decimal? _BaseReceivedQty;
    protected string _OrderType;
    protected string _OrderNbr;
    protected int? _VendorID;
    protected int? _VendorLocationID;
    protected DateTime? _OrderDate;
    protected DateTime? _ExpectedDate;
    protected DateTime? _ExpirationDate;
    protected string _Status;
    protected bool? _Hold;
    protected bool? _Approved;
    protected bool? _Cancelled;
    protected string _CuryID;
    protected long? _CuryInfoID;
    protected string _VendorRefNbr;
    protected Decimal? _CuryOrderTotal;
    protected Decimal? _OrderTotal;
    protected Decimal? _OrderQty;
    protected string _TermsID;
    protected string _OrderDesc;
    protected Decimal? _CuryLineTotal;
    protected Decimal? _LineTotal;

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Selected")]
    public virtual bool? Selected
    {
      get => this._Selected;
      set => this._Selected = value;
    }

    [PXDBQuantity(HandleEmptyKey = true, BqlField = typeof (POLine.receivedQty))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField]
    public virtual Decimal? ReceivedQty
    {
      get => this._ReceivedQty;
      set => this._ReceivedQty = value;
    }

    [PXDBDecimal(6, BqlField = typeof (POLine.baseReceivedQty))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField]
    public virtual Decimal? BaseReceivedQty
    {
      get => this._BaseReceivedQty;
      set => this._BaseReceivedQty = value;
    }

    [PXDBQuantity(HandleEmptyKey = true, BqlField = typeof (POLine.orderQty))]
    public virtual Decimal? LineTotalQty { get; set; }

    [PXDBDecimal(6, BqlField = typeof (POLine.baseOrderQty))]
    public virtual Decimal? BaseLineTotalQty { get; set; }

    [PXDBDecimal(6, BqlField = typeof (POLine.curyExtCost))]
    public virtual Decimal? CuryLineTotalCost { get; set; }

    [PXDBDecimal(6, BqlField = typeof (POLine.extCost))]
    public virtual Decimal? LineTotalCost { get; set; }

    [PXDBDecimal(6, BqlField = typeof (POLine.curyBLOrderedCost))]
    public virtual Decimal? CuryBLOrderedCost { get; set; }

    [PXDBDecimal(6, BqlField = typeof (POLine.bLOrderedCost))]
    public virtual Decimal? BLOrderedCost { get; set; }

    [PXCurrency(typeof (POOrderEntry.POOrderS.curyInfoID), typeof (POOrderEntry.POOrderS.leftToReceiveCost))]
    [PXUIField]
    public virtual Decimal? CuryLeftToReceiveCost
    {
      [PXDependsOnFields(new System.Type[] {typeof (POOrderEntry.POOrderS.curyLineTotalCost), typeof (POOrderEntry.POOrderS.curyBLOrderedCost)})] get
      {
        Decimal? curyLineTotalCost = this.CuryLineTotalCost;
        Decimal? curyBlOrderedCost = this.CuryBLOrderedCost;
        return !(curyLineTotalCost.HasValue & curyBlOrderedCost.HasValue) ? new Decimal?() : new Decimal?(curyLineTotalCost.GetValueOrDefault() - curyBlOrderedCost.GetValueOrDefault());
      }
    }

    [PXBaseCury]
    public virtual Decimal? LeftToReceiveCost
    {
      [PXDependsOnFields(new System.Type[] {typeof (POOrderEntry.POOrderS.lineTotalCost), typeof (POOrderEntry.POOrderS.bLOrderedCost)})] get
      {
        Decimal? lineTotalCost = this.LineTotalCost;
        Decimal? blOrderedCost = this.BLOrderedCost;
        return !(lineTotalCost.HasValue & blOrderedCost.HasValue) ? new Decimal?() : new Decimal?(lineTotalCost.GetValueOrDefault() - blOrderedCost.GetValueOrDefault());
      }
    }

    [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (POOrder.orderType))]
    [POOrderType.List]
    [PXUIField]
    public virtual string OrderType
    {
      get => this._OrderType;
      set => this._OrderType = value;
    }

    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (POOrder.orderNbr))]
    [PXUIField]
    [PX.Objects.PO.PO.Numbering]
    [PX.Objects.PO.PO.RefNbr(typeof (Search2<POOrderEntry.POOrderS.orderNbr, InnerJoinSingleTable<PX.Objects.AP.Vendor, On<POOrderEntry.POOrderS.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>, Where<POOrderEntry.POOrderS.orderType, Equal<Optional<POOrderEntry.POOrderS.orderType>>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>), Filterable = true)]
    public virtual string OrderNbr
    {
      get => this._OrderNbr;
      set => this._OrderNbr = value;
    }

    [VendorActive]
    [PXDefault]
    public virtual int? VendorID
    {
      get => this._VendorID;
      set => this._VendorID = value;
    }

    [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POOrderEntry.POOrderS.vendorID>>>))]
    public virtual int? VendorLocationID
    {
      get => this._VendorLocationID;
      set => this._VendorLocationID = value;
    }

    [PXDBDate(BqlField = typeof (POOrder.orderDate))]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? OrderDate
    {
      get => this._OrderDate;
      set => this._OrderDate = value;
    }

    [PXDBDate(BqlField = typeof (POOrder.expectedDate))]
    [PXDefault(typeof (POOrderEntry.POOrderS.orderDate))]
    [PXUIField(DisplayName = "Promised On")]
    public virtual DateTime? ExpectedDate
    {
      get => this._ExpectedDate;
      set => this._ExpectedDate = value;
    }

    [PXDBDate(BqlField = typeof (POOrder.expirationDate))]
    [PXUIField(DisplayName = "Expired On")]
    public virtual DateTime? ExpirationDate
    {
      get => this._ExpirationDate;
      set => this._ExpirationDate = value;
    }

    [PXDBString(1, IsFixed = true, BqlField = typeof (POOrder.status))]
    [PXUIField]
    [POOrderStatus.List]
    public virtual string Status
    {
      get => this._Status;
      set => this._Status = value;
    }

    [PXDBBool(BqlField = typeof (POOrder.hold))]
    [PXUIField]
    [PXDefault(true)]
    public virtual bool? Hold
    {
      get => this._Hold;
      set => this._Hold = value;
    }

    [PXDBBool(BqlField = typeof (POOrder.approved))]
    [PXUIField]
    [PXDefault(true)]
    public virtual bool? Approved
    {
      get => this._Approved;
      set => this._Approved = value;
    }

    [PXDBBool(BqlField = typeof (POOrder.cancelled))]
    [PXUIField]
    [PXDefault(false)]
    public virtual bool? Cancelled
    {
      get => this._Cancelled;
      set => this._Cancelled = value;
    }

    [PXDBBool(BqlField = typeof (POOrder.isTaxValid))]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Tax Is Up to Date", Enabled = false)]
    public virtual bool? IsTaxValid { get; set; }

    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (POOrder.curyID))]
    [PXUIField]
    [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
    public virtual string CuryID
    {
      get => this._CuryID;
      set => this._CuryID = value;
    }

    [PXDBLong(BqlField = typeof (POOrder.curyInfoID))]
    public virtual long? CuryInfoID
    {
      get => this._CuryInfoID;
      set => this._CuryInfoID = value;
    }

    [PXDBString(40, IsUnicode = true, BqlField = typeof (POOrder.vendorRefNbr))]
    [PXUIField]
    public virtual string VendorRefNbr
    {
      get => this._VendorRefNbr;
      set => this._VendorRefNbr = value;
    }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (POOrderEntry.POOrderS.curyInfoID), typeof (POOrderEntry.POOrderS.orderTotal), BqlField = typeof (POOrder.curyOrderTotal))]
    [PXUIField]
    public virtual Decimal? CuryOrderTotal
    {
      get => this._CuryOrderTotal;
      set => this._CuryOrderTotal = value;
    }

    [PXDBBaseCury(BqlField = typeof (POOrder.orderTotal))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? OrderTotal
    {
      get => this._OrderTotal;
      set => this._OrderTotal = value;
    }

    [PXDBQuantity(BqlField = typeof (POOrder.openOrderQty))]
    [PXUIField(DisplayName = "Open Qty.", Enabled = false)]
    public virtual Decimal? OpenOrderQty { get; set; }

    [PXDBQuantity(BqlField = typeof (POOrder.orderQty))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? OrderQty
    {
      get => this._OrderQty;
      set => this._OrderQty = value;
    }

    [PXDBString(10, IsUnicode = true, IsFixed = true, BqlField = typeof (POOrder.termsID))]
    [PXUIField]
    [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.vendor>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), Filterable = true)]
    public virtual string TermsID
    {
      get => this._TermsID;
      set => this._TermsID = value;
    }

    [PXDBString(60, IsUnicode = true, BqlField = typeof (POOrder.orderDesc))]
    [PXUIField]
    public virtual string OrderDesc
    {
      get => this._OrderDesc;
      set => this._OrderDesc = value;
    }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (POOrderEntry.POOrderS.curyInfoID), typeof (POOrderEntry.POOrderS.lineTotal), BqlField = typeof (POOrder.curyLineTotal))]
    [PXUIField]
    public virtual Decimal? CuryLineTotal
    {
      get => this._CuryLineTotal;
      set => this._CuryLineTotal = value;
    }

    [PXDBBaseCury(BqlField = typeof (POOrder.lineTotal))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? LineTotal
    {
      get => this._LineTotal;
      set => this._LineTotal = value;
    }

    /// <summary>
    /// A reference to the <see cref="T:PX.Objects.AP.Vendor" />.
    /// </summary>
    /// <value>
    /// An integer identifier of the vendor, whom the AP bill will belong to.
    /// </value>
    [POOrderPayToVendor(CacheGlobal = true, Filterable = true, BqlField = typeof (POOrder.payToVendorID))]
    public virtual int? PayToVendorID { get; set; }

    [ProjectBase(BqlField = typeof (POOrder.projectID))]
    public virtual int? ProjectID { get; set; }

    public abstract class selected : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrderEntry.POOrderS.selected>
    {
    }

    public abstract class receivedQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POOrderS.receivedQty>
    {
    }

    public abstract class baseReceivedQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POOrderS.baseReceivedQty>
    {
    }

    public abstract class lineTotalQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POOrderS.lineTotalQty>
    {
    }

    public abstract class baseLineTotalQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POOrderS.baseLineTotalQty>
    {
    }

    public abstract class curyLineTotalCost : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POOrderS.curyLineTotalCost>
    {
    }

    public abstract class lineTotalCost : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POOrderS.lineTotalCost>
    {
    }

    public abstract class curyBLOrderedCost : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POOrderS.curyBLOrderedCost>
    {
    }

    public abstract class bLOrderedCost : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POOrderS.bLOrderedCost>
    {
    }

    public abstract class curyLeftToReceiveCost : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POOrderS.curyLeftToReceiveCost>
    {
    }

    public abstract class leftToReceiveCost : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POOrderS.leftToReceiveCost>
    {
    }

    public abstract class orderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.POOrderS.orderType>
    {
    }

    public abstract class orderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.POOrderS.orderNbr>
    {
    }

    public abstract class vendorID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrderEntry.POOrderS.vendorID>
    {
    }

    public abstract class vendorLocationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POOrderEntry.POOrderS.vendorLocationID>
    {
    }

    public abstract class orderDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      POOrderEntry.POOrderS.orderDate>
    {
    }

    public abstract class expectedDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      POOrderEntry.POOrderS.expectedDate>
    {
    }

    public abstract class expirationDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      POOrderEntry.POOrderS.expirationDate>
    {
    }

    public abstract class status : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderEntry.POOrderS.status>
    {
    }

    public abstract class hold : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrderEntry.POOrderS.hold>
    {
    }

    public abstract class approved : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrderEntry.POOrderS.approved>
    {
    }

    public abstract class cancelled : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrderEntry.POOrderS.cancelled>
    {
    }

    public abstract class isTaxValid : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POOrderEntry.POOrderS.isTaxValid>
    {
    }

    public abstract class curyID : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderEntry.POOrderS.curyID>
    {
    }

    public abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      POOrderEntry.POOrderS.curyInfoID>
    {
    }

    public abstract class vendorRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.POOrderS.vendorRefNbr>
    {
    }

    public abstract class curyOrderTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POOrderS.curyOrderTotal>
    {
    }

    public abstract class orderTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POOrderS.orderTotal>
    {
    }

    public abstract class openOrderQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POOrderS.openOrderQty>
    {
    }

    public abstract class orderQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POOrderS.orderQty>
    {
    }

    public abstract class termsID : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderEntry.POOrderS.termsID>
    {
    }

    public abstract class orderDesc : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.POOrderS.orderDesc>
    {
    }

    public abstract class curyLineTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POOrderS.curyLineTotal>
    {
    }

    public abstract class lineTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POOrderS.lineTotal>
    {
    }

    public abstract class payToVendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POOrderEntry.POOrderS.payToVendorID>
    {
    }

    public abstract class projectID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrderEntry.POOrderS.projectID>
    {
    }
  }

  [PXProjection(typeof (Select<POOrder>), Persistent = true)]
  [Serializable]
  public class POOrderR : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _OrderType;
    protected string _OrderNbr;
    protected string _Status;
    protected bool? _Hold;

    [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (POOrder.orderType))]
    public virtual string OrderType
    {
      get => this._OrderType;
      set => this._OrderType = value;
    }

    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (POOrder.orderNbr))]
    [PXDefault]
    public virtual string OrderNbr
    {
      get => this._OrderNbr;
      set => this._OrderNbr = value;
    }

    [PXDBString(1, IsFixed = true, BqlField = typeof (POOrder.status))]
    public virtual string Status
    {
      get => this._Status;
      set => this._Status = value;
    }

    [PXDBBool(BqlField = typeof (POOrder.hold))]
    public virtual bool? Hold
    {
      get => this._Hold;
      set => this._Hold = value;
    }

    [PXDBInt(BqlField = typeof (POOrder.linesToCompleteCntr))]
    public virtual int? LinesToCompleteCntr { get; set; }

    [PXDBInt(BqlField = typeof (POOrder.linesToCloseCntr))]
    public virtual int? LinesToCloseCntr { get; set; }

    [PXDBQuantity(BqlField = typeof (POOrder.openOrderQty))]
    public virtual Decimal? OpenOrderQty { get; set; }

    public class PK : 
      PrimaryKeyOf<POOrderEntry.POOrderR>.By<POOrderEntry.POOrderR.orderType, POOrderEntry.POOrderR.orderNbr>
    {
      public static POOrderEntry.POOrderR Find(
        PXGraph graph,
        string orderType,
        string orderNbr,
        PKFindOptions options = 0)
      {
        return (POOrderEntry.POOrderR) PrimaryKeyOf<POOrderEntry.POOrderR>.By<POOrderEntry.POOrderR.orderType, POOrderEntry.POOrderR.orderNbr>.FindBy(graph, (object) orderType, (object) orderNbr, options);
      }
    }

    public abstract class orderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.POOrderR.orderType>
    {
    }

    public abstract class orderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.POOrderR.orderNbr>
    {
    }

    public abstract class status : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderEntry.POOrderR.status>
    {
    }

    public abstract class hold : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrderEntry.POOrderR.hold>
    {
    }

    public abstract class linesToCompleteCntr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POOrderEntry.POOrderR.linesToCompleteCntr>
    {
    }

    public abstract class linesToCloseCntr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POOrderEntry.POOrderR.linesToCloseCntr>
    {
    }

    public abstract class openOrderQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.POOrderR.openOrderQty>
    {
    }
  }

  /// <exclude />
  [PXCacheName("Sales Order Line Split")]
  [PXProjection(typeof (Select2<PX.Objects.SO.SOLineSplit, InnerJoin<PX.Objects.SO.SOLine, On<PX.Objects.SO.SOLine.orderType, Equal<PX.Objects.SO.SOLineSplit.orderType>, And<PX.Objects.SO.SOLine.orderNbr, Equal<PX.Objects.SO.SOLineSplit.orderNbr>, And<PX.Objects.SO.SOLine.lineNbr, Equal<PX.Objects.SO.SOLineSplit.lineNbr>>>>>>), new System.Type[] {typeof (PX.Objects.SO.SOLineSplit)})]
  [Serializable]
  public class SOLineSplit3 : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISortOrder
  {
    protected string _OrderType;
    protected string _OrderNbr;
    protected int? _LineNbr;
    protected int? _SortOrder;
    protected int? _SplitLineNbr;
    protected string _Operation;
    protected string _LineType;
    protected int? _VendorID;
    protected bool? _POCreate;
    protected bool? _POCreated;
    protected string _POType;
    protected string _PONbr;
    protected int? _POLineNbr;
    protected Guid? _RefNoteID;
    protected DateTime? _RequestDate;
    protected int? _CustomerID;
    protected int? _SiteID;
    protected string _UOM;
    protected Decimal? _OrderQty;
    protected bool? _LinePOCreate;
    protected string _POUOM;
    protected Decimal? _POUOMOrderQty;
    protected long? _PlanID;
    protected int? _InventoryID;
    protected DateTime? _ShipDate;
    protected string _TranDesc;
    protected int? _ProjectID;
    protected int? _TaskID;
    protected Guid? _NoteID;

    [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.orderType))]
    [PXDefault]
    [PXUIField(DisplayName = "Order Type", Enabled = false)]
    public virtual string OrderType
    {
      get => this._OrderType;
      set => this._OrderType = value;
    }

    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (PX.Objects.SO.SOLineSplit.orderNbr))]
    [PXDefault]
    [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
    [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<POOrderEntry.SOLineSplit3.orderType>>>>))]
    public virtual string OrderNbr
    {
      get => this._OrderNbr;
      set => this._OrderNbr = value;
    }

    [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.lineNbr))]
    [PXUIField(DisplayName = "Line Nbr.", Enabled = false)]
    public virtual int? LineNbr
    {
      get => this._LineNbr;
      set => this._LineNbr = value;
    }

    [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.sortOrder))]
    public virtual int? SortOrder
    {
      get => this._SortOrder;
      set => this._SortOrder = value;
    }

    [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.splitLineNbr))]
    public virtual int? SplitLineNbr
    {
      get => this._SplitLineNbr;
      set => this._SplitLineNbr = value;
    }

    [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLineSplit.parentSplitLineNbr))]
    [PXUIField(DisplayName = "Parent Allocation ID", Visible = false, IsReadOnly = true, Enabled = false)]
    public virtual int? ParentSplitLineNbr { get; set; }

    [PXDBString(2, IsFixed = true, InputMask = ">aa", BqlField = typeof (PX.Objects.SO.SOLineSplit.behavior))]
    public virtual string Behavior { get; set; }

    [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.operation))]
    public virtual string Operation
    {
      get => this._Operation;
      set => this._Operation = value;
    }

    [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLine.lineType))]
    [PXUIField(DisplayName = "Line Type", Enabled = false)]
    public virtual string LineType
    {
      get => this._LineType;
      set => this._LineType = value;
    }

    [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLineSplit.vendorID))]
    public virtual int? VendorID
    {
      get => this._VendorID;
      set => this._VendorID = value;
    }

    [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLineSplit.pOCreate))]
    public virtual bool? POCreate
    {
      get => this._POCreate;
      set => this._POCreate = value;
    }

    [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLine.pOCreated))]
    public virtual bool? POCreated
    {
      get => this._POCreated;
      set => this._POCreated = value;
    }

    [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLineSplit.pOCompleted))]
    public virtual bool? POCompleted { get; set; }

    [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLineSplit.pOCancelled))]
    public virtual bool? POCancelled { get; set; }

    [PXDBString(BqlField = typeof (PX.Objects.SO.SOLineSplit.pOSource))]
    public virtual string POSource { get; set; }

    [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.pOType))]
    [PXUIField(DisplayName = "PO Type", Enabled = false)]
    public virtual string POType
    {
      get => this._POType;
      set => this._POType = value;
    }

    [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.pONbr))]
    [PXDBDefault(typeof (POOrder.orderNbr))]
    [PXUIField(DisplayName = "PO Nbr.", Enabled = false)]
    public virtual string PONbr
    {
      get => this._PONbr;
      set => this._PONbr = value;
    }

    [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLineSplit.pOLineNbr))]
    [PXUIField(DisplayName = "PO Line Nbr.", Enabled = false)]
    public virtual int? POLineNbr
    {
      get => this._POLineNbr;
      set => this._POLineNbr = value;
    }

    [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.pOReceiptType))]
    [PXUIField(DisplayName = "PO Receipt Type", Enabled = false)]
    public virtual string POReceiptType { get; set; }

    [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.pOReceiptNbr))]
    [PXUIField(DisplayName = "PO Receipt Nbr.", Enabled = false)]
    public virtual string POReceiptNbr { get; set; }

    [PXDBGuid(false, BqlField = typeof (PX.Objects.SO.SOLineSplit.refNoteID))]
    public virtual Guid? RefNoteID
    {
      get => this._RefNoteID;
      set => this._RefNoteID = value;
    }

    [PXDBDate(BqlField = typeof (PX.Objects.SO.SOLine.requestDate))]
    [PXDefault]
    [PXUIField(DisplayName = "Requested", Enabled = false)]
    public virtual DateTime? RequestDate
    {
      get => this._RequestDate;
      set => this._RequestDate = value;
    }

    [Customer(BqlField = typeof (PX.Objects.SO.SOLine.customerID), Enabled = false)]
    [PXDefault]
    public virtual int? CustomerID
    {
      get => this._CustomerID;
      set => this._CustomerID = value;
    }

    [SiteAvail(typeof (PX.Objects.SO.SOLineSplit.inventoryID), typeof (PX.Objects.SO.SOLineSplit.subItemID), typeof (CostCenter.freeStock), BqlField = typeof (PX.Objects.SO.SOLineSplit.siteID), DisplayName = "Warehouse")]
    public virtual int? SiteID
    {
      get => this._SiteID;
      set => this._SiteID = value;
    }

    [INUnit(typeof (PX.Objects.SO.SOLineSplit.inventoryID), DisplayName = "Orig. UOM", BqlField = typeof (PX.Objects.SO.SOLineSplit.uOM), Enabled = false)]
    [PXDefault]
    public virtual string UOM
    {
      get => this._UOM;
      set => this._UOM = value;
    }

    [PXDBQuantity(BqlField = typeof (PX.Objects.SO.SOLineSplit.qty))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Orig. Quantity", Enabled = false)]
    public virtual Decimal? OrderQty
    {
      get => this._OrderQty;
      set => this._OrderQty = value;
    }

    [PXDBDecimal(6, BqlField = typeof (PX.Objects.SO.SOLineSplit.baseQty))]
    public virtual Decimal? BaseOrderQty { get; set; }

    /// <exclude />
    [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLine.pOCreate))]
    public virtual bool? LinePOCreate
    {
      get => this._LinePOCreate;
      set => this._LinePOCreate = value;
    }

    [PXBool]
    [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.SO.SOLine.baseOpenQty, Equal<PX.Objects.SO.SOLine.baseOrderQty>, And<BqlOperand<PX.Objects.SO.SOLine.baseOrderQty, IBqlDecimal>.Multiply<PX.Objects.SO.SOLine.lineSign>, Equal<PX.Objects.SO.SOLineSplit.baseQty>, And<PX.Objects.SO.SOLineSplit.isAllocated, NotEqual<True>>>>, True>, False>), typeof (bool))]
    public virtual bool? IsValidForDropShip { get; set; }

    [PXString(6, IsUnicode = true, InputMask = ">aaaaaa")]
    [PXUIField(DisplayName = "UOM", Enabled = false)]
    public virtual string POUOM
    {
      get => this._POUOM;
      set => this._POUOM = value;
    }

    [PXQuantity]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Quantity", Enabled = false)]
    public virtual Decimal? POUOMOrderQty
    {
      get => this._POUOMOrderQty;
      set => this._POUOMOrderQty = value;
    }

    [PXDBLong(BqlField = typeof (PX.Objects.SO.SOLineSplit.planID))]
    public virtual long? PlanID
    {
      get => this._PlanID;
      set => this._PlanID = value;
    }

    [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLineSplit.isStockItem))]
    public virtual bool? IsStockItem { get; set; }

    [CrossItem(INPrimaryAlternateType.CPN, Filterable = true, BqlField = typeof (PX.Objects.SO.SOLineSplit.inventoryID))]
    public virtual int? InventoryID
    {
      get => this._InventoryID;
      set => this._InventoryID = value;
    }

    [PXDBDate(BqlField = typeof (PX.Objects.SO.SOLineSplit.shipDate))]
    [PXUIField]
    public virtual DateTime? ShipDate
    {
      get => this._ShipDate;
      set => this._ShipDate = value;
    }

    [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOLine.tranDesc))]
    [PXUIField(DisplayName = "Line Description")]
    public virtual string TranDesc
    {
      get => this._TranDesc;
      set => this._TranDesc = value;
    }

    [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.salesAcctID))]
    public virtual int? SalesAcctID { get; set; }

    [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.salesSubID))]
    public virtual int? SalesSubID { get; set; }

    [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.projectID))]
    public virtual int? ProjectID
    {
      get => this._ProjectID;
      set => this._ProjectID = value;
    }

    [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.taskID))]
    public virtual int? TaskID
    {
      get => this._TaskID;
      set => this._TaskID = value;
    }

    [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.costCodeID))]
    public virtual int? CostCodeID { get; set; }

    [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLine.isSpecialOrder))]
    public virtual bool? IsSpecialOrder { get; set; }

    [PXDBDecimal(6, BqlField = typeof (PX.Objects.SO.SOLine.curyUnitCost))]
    public virtual Decimal? CuryUnitCost { get; set; }

    [PXBool]
    [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<PX.Objects.SO.SOLineSplit.completed, IBqlBool>.IsEqual<True>>, False>, True>), typeof (bool?))]
    [PXUIField(DisplayName = "Active", Enabled = false)]
    public virtual bool? Active { get; set; }

    [PXNote(BqlField = typeof (PX.Objects.SO.SOLine.noteID))]
    public virtual Guid? NoteID
    {
      get => this._NoteID;
      set => this._NoteID = value;
    }

    [PXDBTimestamp]
    public virtual byte[] tstamp { get; set; }

    public class PK : 
      PrimaryKeyOf<POOrderEntry.SOLineSplit3>.By<POOrderEntry.SOLineSplit3.orderType, POOrderEntry.SOLineSplit3.orderNbr, POOrderEntry.SOLineSplit3.lineNbr, POOrderEntry.SOLineSplit3.splitLineNbr>
    {
      public static POOrderEntry.SOLineSplit3 Find(
        PXGraph graph,
        string orderType,
        string orderNbr,
        int? lineNbr,
        int? splitLineNbr,
        PKFindOptions options = 0)
      {
        return (POOrderEntry.SOLineSplit3) PrimaryKeyOf<POOrderEntry.SOLineSplit3>.By<POOrderEntry.SOLineSplit3.orderType, POOrderEntry.SOLineSplit3.orderNbr, POOrderEntry.SOLineSplit3.lineNbr, POOrderEntry.SOLineSplit3.splitLineNbr>.FindBy(graph, (object) orderType, (object) orderNbr, (object) lineNbr, (object) splitLineNbr, options);
      }
    }

    public static class FK
    {
      public class OrderLine : 
        PrimaryKeyOf<POOrderEntry.SOLine5>.By<POOrderEntry.SOLine5.orderType, POOrderEntry.SOLine5.orderNbr, POOrderEntry.SOLine5.lineNbr>.ForeignKeyOf<POOrderEntry.SOLineSplit3>.By<POOrderEntry.SOLineSplit3.orderType, POOrderEntry.SOLineSplit3.orderNbr, POOrderEntry.SOLineSplit3.lineNbr>
      {
      }

      public class POLine : 
        PrimaryKeyOf<POLine>.By<POLine.orderType, POLine.orderNbr, POLine.lineNbr>.ForeignKeyOf<POOrderEntry.SOLineSplit3>.By<POOrderEntry.SOLineSplit3.pOType, POOrderEntry.SOLineSplit3.pONbr, POOrderEntry.SOLineSplit3.pOLineNbr>
      {
      }
    }

    public abstract class orderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.orderType>
    {
    }

    public abstract class orderNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.orderNbr>
    {
    }

    public abstract class lineNbr : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrderEntry.SOLineSplit3.lineNbr>
    {
    }

    public abstract class sortOrder : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.sortOrder>
    {
    }

    public abstract class splitLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.splitLineNbr>
    {
    }

    public abstract class parentSplitLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.parentSplitLineNbr>
    {
    }

    public abstract class behavior : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.behavior>
    {
    }

    public abstract class operation : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.operation>
    {
    }

    public abstract class lineType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.lineType>
    {
    }

    public abstract class vendorID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrderEntry.SOLineSplit3.vendorID>
    {
    }

    public abstract class pOCreate : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.pOCreate>
    {
    }

    public abstract class pOCreated : IBqlField, IBqlOperand
    {
    }

    public abstract class pOCompleted : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.pOCompleted>
    {
    }

    public abstract class pOCancelled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.pOCancelled>
    {
    }

    public abstract class pOSource : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.pOSource>
    {
    }

    public abstract class pOType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.pOType>
    {
    }

    public abstract class pONbr : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderEntry.SOLineSplit3.pONbr>
    {
    }

    public abstract class pOLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.pOLineNbr>
    {
    }

    public abstract class pOReceiptType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.pOReceiptType>
    {
    }

    public abstract class pOReceiptNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.pOReceiptNbr>
    {
    }

    public abstract class refNoteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.refNoteID>
    {
    }

    public abstract class requestDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.requestDate>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.customerID>
    {
    }

    public abstract class siteID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrderEntry.SOLineSplit3.siteID>
    {
    }

    public abstract class uOM : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderEntry.SOLineSplit3.uOM>
    {
    }

    public abstract class orderQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.orderQty>
    {
    }

    public abstract class baseOrderQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.baseOrderQty>
    {
    }

    /// <exclude />
    public abstract class linePOCreate : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.linePOCreate>
    {
    }

    public abstract class isValidForDropShip : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.isValidForDropShip>
    {
    }

    public abstract class pOUOM : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderEntry.SOLineSplit3.pOUOM>
    {
    }

    public abstract class pOUOMOrderQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.pOUOMOrderQty>
    {
    }

    public abstract class planID : BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    POOrderEntry.SOLineSplit3.planID>
    {
    }

    public abstract class isStockItem : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.isStockItem>
    {
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.inventoryID>
    {
    }

    public abstract class shipDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.shipDate>
    {
    }

    public abstract class tranDesc : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.tranDesc>
    {
    }

    public abstract class salesAcctID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.salesAcctID>
    {
    }

    public abstract class salesSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.salesSubID>
    {
    }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.projectID>
    {
    }

    public abstract class taskID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrderEntry.SOLineSplit3.taskID>
    {
    }

    public abstract class costCodeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.costCodeID>
    {
    }

    public abstract class isSpecialOrder : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.isSpecialOrder>
    {
    }

    public abstract class curyUnitCost : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.curyUnitCost>
    {
    }

    public abstract class active : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrderEntry.SOLineSplit3.active>
    {
    }

    public abstract class noteID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POOrderEntry.SOLineSplit3.noteID>
    {
    }

    public abstract class Tstamp : 
      BqlType<
      #nullable enable
      IBqlByteArray, byte[]>.Field<
      #nullable disable
      POOrderEntry.SOLineSplit3.Tstamp>
    {
    }
  }

  [PXProjection(typeof (Select<PX.Objects.SO.SOLine>), Persistent = true)]
  [PXHidden]
  [Serializable]
  public class SOLine5 : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _OrderType;
    protected string _OrderNbr;
    protected int? _LineNbr;
    protected int? _VendorID;
    protected bool? _POCreated;

    [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLine.orderType))]
    [PXDefault]
    public virtual string OrderType
    {
      get => this._OrderType;
      set => this._OrderType = value;
    }

    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (PX.Objects.SO.SOLine.orderNbr))]
    [PXDefault]
    public virtual string OrderNbr
    {
      get => this._OrderNbr;
      set => this._OrderNbr = value;
    }

    [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.SO.SOLine.lineNbr))]
    public virtual int? LineNbr
    {
      get => this._LineNbr;
      set => this._LineNbr = value;
    }

    [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.vendorID))]
    public virtual int? VendorID
    {
      get => this._VendorID;
      set => this._VendorID = value;
    }

    [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLine.pOCreated))]
    public virtual bool? POCreated
    {
      get => this._POCreated;
      set => this._POCreated = value;
    }

    [PXDBString(BqlField = typeof (PX.Objects.SO.SOLine.pOSource))]
    public virtual string POSource { get; set; }

    [PXDBDecimal(6, BqlField = typeof (PX.Objects.SO.SOLine.curyUnitCost))]
    public virtual Decimal? CuryUnitCost { get; set; }

    [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLine.isCostUpdatedOnPO))]
    public virtual bool? IsCostUpdatedOnPO { get; set; }

    [PXDecimal(6)]
    public virtual Decimal? CuryUnitCostUpdated { get; set; }

    [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLine.completed))]
    public virtual bool? Completed { get; set; }

    [PXDBTimestamp]
    public virtual byte[] tstamp { get; set; }

    public class PK : 
      PrimaryKeyOf<POOrderEntry.SOLine5>.By<POOrderEntry.SOLine5.orderType, POOrderEntry.SOLine5.orderNbr, POOrderEntry.SOLine5.lineNbr>
    {
      public static POOrderEntry.SOLine5 Find(
        PXGraph graph,
        string orderType,
        string orderNbr,
        int? lineNbr,
        PKFindOptions options = 0)
      {
        return (POOrderEntry.SOLine5) PrimaryKeyOf<POOrderEntry.SOLine5>.By<POOrderEntry.SOLine5.orderType, POOrderEntry.SOLine5.orderNbr, POOrderEntry.SOLine5.lineNbr>.FindBy(graph, (object) orderType, (object) orderNbr, (object) lineNbr, options);
      }
    }

    public abstract class orderType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      POOrderEntry.SOLine5.orderType>
    {
    }

    public abstract class orderNbr : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderEntry.SOLine5.orderNbr>
    {
    }

    public abstract class lineNbr : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrderEntry.SOLine5.lineNbr>
    {
    }

    public abstract class vendorID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POOrderEntry.SOLine5.vendorID>
    {
    }

    public abstract class pOCreated : IBqlField, IBqlOperand
    {
    }

    public abstract class pOSource : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POOrderEntry.SOLine5.pOSource>
    {
    }

    public abstract class curyUnitCost : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.SOLine5.curyUnitCost>
    {
    }

    public abstract class isCostUpdatedOnPO : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      POOrderEntry.SOLine5.isCostUpdatedOnPO>
    {
    }

    public abstract class curyUnitCostUpdated : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      POOrderEntry.SOLine5.curyUnitCostUpdated>
    {
    }

    public abstract class completed : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrderEntry.SOLine5.completed>
    {
    }

    public abstract class Tstamp : BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    POOrderEntry.SOLine5.Tstamp>
    {
    }
  }

  /// <exclude />
  public class POOrderEntryAddressLookupExtension : 
    AddressLookupExtension<POOrderEntry, POOrder, POShipAddress>
  {
    protected override string AddressView => "Shipping_Address";
  }

  /// <exclude />
  public class POOrderEntryRemitAddressLookupExtension : 
    AddressLookupExtension<POOrderEntry, POOrder, PORemitAddress>
  {
    protected override string AddressView => "Remit_Address";
  }

  public class POOrderEntryShippingAddressCachingHelper : 
    AddressValidationExtension<POOrderEntry, POShipAddress>
  {
    protected override IEnumerable<PXSelectBase<POShipAddress>> AddressSelects()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      POOrderEntry.POOrderEntryShippingAddressCachingHelper addressCachingHelper = this;
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
      this.\u003C\u003E2__current = (PXSelectBase<POShipAddress>) addressCachingHelper.Base.Shipping_Address;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }
  }

  public class POOrderEntryRemitAddressCachingHelper : 
    AddressValidationExtension<POOrderEntry, PORemitAddress>
  {
    protected override IEnumerable<PXSelectBase<PORemitAddress>> AddressSelects()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      POOrderEntry.POOrderEntryRemitAddressCachingHelper addressCachingHelper = this;
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
      this.\u003C\u003E2__current = (PXSelectBase<PORemitAddress>) addressCachingHelper.Base.Remit_Address;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }
  }
}
