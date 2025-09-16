// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLandedCostDocEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.IN;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.Services;
using PX.Objects.PO.LandedCosts;
using PX.Objects.TX;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.PO;

[Serializable]
public class POLandedCostDocEntry : PXGraph<
#nullable disable
POLandedCostDocEntry, POLandedCostDoc>
{
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (POLandedCostDoc.hold)})]
  [PXViewName("Landed Costs Document")]
  public PXSelect<POLandedCostDoc> Document;
  public PXSetup<POSetup> posetup;
  public PXSetup<APSetup> apsetup;
  public PXSetup<PX.Objects.GL.Branch>.Where<BqlOperand<
  #nullable enable
  PX.Objects.GL.Branch.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  POLandedCostDoc.vendorID, IBqlInt>.AsOptional>> company;
  [PXViewName("Vendor")]
  public 
  #nullable disable
  PXSetup<PX.Objects.AP.Vendor>.Where<BqlOperand<
  #nullable enable
  PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  POLandedCostDoc.vendorID, IBqlInt>.AsOptional>> vendor;
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
  public 
  #nullable disable
  PXSetup<PX.Objects.TX.TaxZone>.Where<BqlOperand<
  #nullable enable
  PX.Objects.TX.TaxZone.taxZoneID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  POLandedCostDoc.taxZoneID, IBqlString>.FromCurrent>> taxzone;
  [PXViewName("Vendor Location")]
  public 
  #nullable disable
  PXSetup<PX.Objects.CR.Location>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.CR.Location.bAccountID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  POLandedCostDoc.vendorID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PX.Objects.CR.Location.locationID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  POLandedCostDoc.vendorLocationID, IBqlInt>.AsOptional>>> location;
  public 
  #nullable disable
  PXSelect<POLandedCostDoc, Where<POLandedCostDoc.docType, Equal<Current<POLandedCostDoc.docType>>, And<POLandedCostDoc.refNbr, Equal<Current<POLandedCostDoc.refNbr>>>>> CurrentDocument;
  public PXSelect<POLandedCostReceipt, Where<POLandedCostReceipt.lCDocType, Equal<Current<POLandedCostDoc.docType>>, And<POLandedCostReceipt.lCRefNbr, Equal<Current<POLandedCostDoc.refNbr>>>>> Receipts;
  [PXCopyPasteHiddenView]
  public PXSelect<POLandedCostReceiptLine, Where<POLandedCostReceiptLine.docType, Equal<Current<POLandedCostDoc.docType>>, And<POLandedCostReceiptLine.refNbr, Equal<Current<POLandedCostDoc.refNbr>>>>, OrderBy<Asc<POLandedCostReceiptLine.lineNbr>>> ReceiptLines;
  public PXSelect<POLandedCostDetail, Where<POLandedCostDetail.docType, Equal<Current<POLandedCostDoc.docType>>, And<POLandedCostDetail.refNbr, Equal<Current<POLandedCostDoc.refNbr>>>>, OrderBy<Asc<POLandedCostDetail.lineNbr>>> Details;
  [PXCopyPasteHiddenView]
  public PXSelect<POLandedCostSplit, Where<POLandedCostSplit.docType, Equal<Current<POLandedCostDoc.docType>>, And<POLandedCostSplit.refNbr, Equal<Current<POLandedCostDoc.refNbr>>>>> Splits;
  [PXCopyPasteHiddenView]
  public PXSelect<POLandedCostTax, Where<POLandedCostTax.docType, Equal<Current<POLandedCostDoc.docType>>, And<POLandedCostTax.refNbr, Equal<Current<POLandedCostDoc.refNbr>>>>, OrderBy<Asc<POLandedCostTax.refNbr, Asc<POLandedCostTax.taxID>>>> Tax_Rows;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<POLandedCostTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<POLandedCostTaxTran.taxID>>>, Where<POLandedCostTaxTran.docType, Equal<Current<POLandedCostDoc.docType>>, And<POLandedCostTaxTran.refNbr, Equal<Current<POLandedCostDoc.refNbr>>>>> Taxes;
  public PXFilter<POReceiptFilter> filter;
  [PXCopyPasteHiddenView]
  [PXReadOnlyView]
  public PXSelectOrderBy<POReceipt, OrderBy<Desc<POReceipt.receiptDate, Desc<POReceipt.lastModifiedDateTime>>>> poReceiptSelection;
  public PXSelectJoin<POReceipt, InnerJoin<POReceiptLinesCount, On<POReceiptLinesCount.FK.Receipt>>, Where<POReceipt.receiptType, Equal<Current<POReceiptFilter.receiptType>>, And2<Where<Current<POReceiptFilter.vendorID>, IsNull, Or<POReceipt.vendorID, Equal<Current<POReceiptFilter.vendorID>>>>, And2<Where<Current<POReceiptFilter.receiptNbr>, IsNull, Or<POReceipt.receiptNbr, Equal<Current<POReceiptFilter.receiptNbr>>>>, And<POReceipt.released, Equal<True>, And<POReceipt.isUnderCorrection, Equal<False>, And<POReceipt.canceled, Equal<False>>>>>>>, OrderBy<Desc<POReceipt.receiptDate, Desc<POReceipt.lastModifiedDateTime>>>> poReceiptSelectionView;
  [PXCopyPasteHiddenView]
  public PXSelect<POReceiptLineAdd, Where<POReceiptLineAdd.receiptType, Equal<Current<POReceiptFilter.receiptType>>, And<POReceiptLineAdd.lineType, NotEqual<POLineType.service>, And<POReceiptLineAdd.lineType, NotEqual<POLineType.freight>, And2<Where<Current<POReceiptFilter.receiptNbr>, IsNull, Or<POReceiptLineAdd.receiptNbr, Equal<Current<POReceiptFilter.receiptNbr>>>>, And2<Where<Current<POReceiptFilter.orderType>, IsNull, Or<POReceiptLineAdd.pOType, Equal<Current<POReceiptFilter.orderType>>>>, And2<Where<Current<POReceiptFilter.orderNbr>, IsNull, Or<POReceiptLineAdd.pONbr, Equal<Current<POReceiptFilter.orderNbr>>>>, And2<Where<Current<POReceiptFilter.inventoryID>, IsNull, Or<POReceiptLineAdd.inventoryID, Equal<Current<POReceiptFilter.inventoryID>>>>, And2<Where<Current<POReceiptFilter.vendorID>, IsNull, Or<POReceiptLineAdd.vendorID, Equal<Current<POReceiptFilter.vendorID>>>>, And<POReceiptLineAdd.released, Equal<True>, And<POReceiptLineAdd.isUnderCorrection, Equal<False>, And<POReceiptLineAdd.canceled, Equal<False>, And<POReceiptLineAdd.receiptQty, Greater<decimal0>>>>>>>>>>>>>, OrderBy<Desc<POReceiptLineAdd.receiptDate, Desc<POReceiptLineAdd.receiptLastModifiedDateTime, Desc<POReceiptLineAdd.receiptNbr, Desc<POReceiptLineAdd.lineNbr>>>>>> poReceiptLinesSelection;
  public PXSelectJoin<POReceiptLineAdd, LeftJoin<PX.Objects.AP.Vendor, On<POReceiptLineAdd.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>, Where<POReceiptLineAdd.receiptType, Equal<Current<POReceiptFilter.receiptType>>, And<POReceiptLineAdd.lineType, NotEqual<POLineType.service>, And<POReceiptLineAdd.lineType, NotEqual<POLineType.freight>, And2<Where<Current<POReceiptFilter.receiptNbr>, IsNull, Or<POReceiptLineAdd.receiptNbr, Equal<Current<POReceiptFilter.receiptNbr>>>>, And2<Where<Current<POReceiptFilter.orderType>, IsNull, Or<POReceiptLineAdd.pOType, Equal<Current<POReceiptFilter.orderType>>>>, And2<Where<Current<POReceiptFilter.orderNbr>, IsNull, Or<POReceiptLineAdd.pONbr, Equal<Current<POReceiptFilter.orderNbr>>>>, And2<Where<Current<POReceiptFilter.inventoryID>, IsNull, Or<POReceiptLineAdd.inventoryID, Equal<Current<POReceiptFilter.inventoryID>>>>, And2<Where<Current<POReceiptFilter.vendorID>, IsNull, Or<POReceiptLineAdd.vendorID, Equal<Current<POReceiptFilter.vendorID>>>>, And<POReceiptLineAdd.released, Equal<True>, And<POReceiptLineAdd.isUnderCorrection, Equal<False>, And<POReceiptLineAdd.canceled, Equal<False>, And<POReceiptLineAdd.receiptQty, Greater<decimal0>>>>>>>>>>>>>, OrderBy<Desc<POReceiptLineAdd.receiptDate, Desc<POReceiptLineAdd.receiptLastModifiedDateTime, Desc<POReceiptLineAdd.receiptNbr, Desc<POReceiptLineAdd.lineNbr>>>>>> poReceiptLinesSelectionView;
  public PXInitializeState<POLandedCostDoc> initializeState;
  public PXAction<POLandedCostDoc> putOnHold;
  public PXAction<POLandedCostDoc> releaseFromHold;
  public PXAction<POLandedCostDoc> createAPInvoice;
  public PXAction<POLandedCostDoc> addPOReceipt;
  public PXAction<POLandedCostDoc> addPOReceipt2;
  public PXAction<POLandedCostDoc> addPOReceiptLine;
  public PXAction<POLandedCostDoc> addPOReceiptLine2;
  public PXAction<POLandedCostDoc> addLC;
  public PXAction<POLandedCostDoc> release;
  public PXAction<POLandedCostDoc> action;
  public PXWorkflowEventHandler<POLandedCostDoc> OnInventoryAdjustmentCreated;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  [InjectDependency]
  public IInventoryAccountService InventoryAccountService { get; set; }

  public POLandedCostDocEntry()
  {
    ((PXSelectBase) this.poReceiptSelection).Cache.AllowInsert = false;
    ((PXSelectBase) this.poReceiptSelection).Cache.AllowDelete = false;
    ((PXSelectBase) this.poReceiptLinesSelection).Cache.AllowInsert = false;
    ((PXSelectBase) this.poReceiptLinesSelection).Cache.AllowDelete = false;
    bool flag = ((PXSelectBase<APSetup>) this.apsetup).Current.RequireVendorRef.Value;
    OpenPeriodAttribute.SetValidatePeriod<POLandedCostDoc.finPeriodID>(((PXSelectBase) this.Document).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    PXDefaultAttribute.SetPersistingCheck<POLandedCostDoc.vendorRefNbr>(((PXSelectBase) this.Document).Cache, (object) null, flag ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetRequired<POReceipt.invoiceNbr>(((PXSelectBase) this.Document).Cache, flag);
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.poReceiptSelection).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<POReceipt.selected>(((PXSelectBase) this.poReceiptSelection).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.poReceiptLinesSelection).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<POReceiptLineAdd.selected>(((PXSelectBase) this.poReceiptLinesSelection).Cache, (object) null, true);
    TaxBaseAttribute.SetTaxCalc<POLandedCostDetail.taxCategoryID>(((PXSelectBase) this.Details).Cache, (object) null, TaxCalc.ManualLineCalc);
    TaxBaseAttribute.IncludeDirectTaxLine<POLandedCostDetail.taxCategoryID>(((PXSelectBase) this.Details).Cache, (object) null, true);
  }

  public virtual LandedCostAPBillFactory GetApBillFactory()
  {
    return new LandedCostAPBillFactory((PXGraph) this);
  }

  public virtual LandedCostINAdjustmentFactory GetInAdjustmentFactory(PXGraph graph)
  {
    return new LandedCostINAdjustmentFactory(graph);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<BAccountR, BAccountR.type> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<BAccountR, BAccountR.type>, BAccountR, object>) e).NewValue = (object) "VE";
  }

  protected virtual void _(PX.Data.Events.RowSelected<POLandedCostDoc> e)
  {
    if (e.Row == null)
      return;
    bool valueOrDefault1 = e.Row.Released.GetValueOrDefault();
    int? nullable1 = e.Row.VendorID;
    int num1;
    if (nullable1.HasValue)
    {
      nullable1 = e.Row.VendorLocationID;
      num1 = nullable1.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag1 = num1 != 0;
    bool valueOrDefault2 = ((PXSelectBase<POSetup>) this.posetup).Current.RequireLandedCostsControlTotal.GetValueOrDefault();
    bool flag2 = ((PXSelectBase<POLandedCostDetail>) this.Details).SelectWindowed(0, 1, Array.Empty<object>()).Count > 0;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache.AllowDelete = !valueOrDefault1;
    PXUIFieldAttribute.SetEnabled<POLandedCostDoc.docType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, (object) e.Row, !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<POLandedCostDoc.docDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, (object) e.Row, !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<POLandedCostDoc.finPeriodID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, (object) e.Row, !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<POLandedCostDoc.vendorID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, (object) e.Row, !valueOrDefault1 && !flag2);
    PXUIFieldAttribute.SetEnabled<POLandedCostDoc.vendorLocationID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, (object) e.Row, !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<POLandedCostDoc.createBill>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, (object) e.Row, !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<POLandedCostDoc.vendorRefNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, (object) e.Row, !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<POLandedCostDoc.curyControlTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, (object) e.Row, !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<POLandedCostDoc.branchID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, (object) e.Row, !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<POLandedCostDoc.termsID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, (object) e.Row, !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<POLandedCostDoc.billDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, (object) e.Row, !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<POLandedCostDoc.dueDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, (object) e.Row, !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<POLandedCostDoc.discDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, (object) e.Row, !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<POLandedCostDoc.curyDiscAmt>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, (object) e.Row, !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<POLandedCostDoc.taxZoneID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, (object) e.Row, !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<POLandedCostDoc.workgroupID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, (object) e.Row, !valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<POLandedCostDoc.ownerID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, (object) e.Row, !valueOrDefault1);
    ((PXSelectBase) this.ReceiptLines).Cache.AllowDelete = !valueOrDefault1 & flag1;
    ((PXSelectBase) this.ReceiptLines).Cache.AllowUpdate = !valueOrDefault1 & flag1;
    ((PXSelectBase) this.ReceiptLines).Cache.AllowInsert = !valueOrDefault1 & flag1;
    ((PXAction) this.addPOReceipt).SetEnabled(!valueOrDefault1 & flag1);
    ((PXAction) this.addPOReceiptLine).SetEnabled(!valueOrDefault1 & flag1);
    ((PXSelectBase) this.Details).Cache.AllowDelete = !valueOrDefault1 & flag1;
    ((PXSelectBase) this.Details).Cache.AllowUpdate = !valueOrDefault1 & flag1;
    ((PXSelectBase) this.Details).Cache.AllowInsert = !valueOrDefault1 & flag1;
    ((PXSelectBase) this.Taxes).Cache.AllowDelete = !valueOrDefault1 & flag1;
    ((PXSelectBase) this.Taxes).Cache.AllowUpdate = !valueOrDefault1 & flag1;
    ((PXSelectBase) this.Taxes).Cache.AllowInsert = !valueOrDefault1 & flag1;
    PXUIFieldAttribute.SetVisible<POLandedCostDoc.curyControlTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, (object) null, valueOrDefault2);
    PXUIFieldAttribute.SetRequired<POLandedCostDoc.curyControlTotal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, valueOrDefault2);
    PXAction<POLandedCostDoc> release = this.release;
    bool? nullable2 = e.Row.Hold;
    int num2;
    if (!nullable2.GetValueOrDefault())
    {
      nullable2 = e.Row.Released;
      num2 = !nullable2.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    ((PXAction) release).SetEnabled(num2 != 0);
    nullable2 = e.Row.CreateBill;
    int num3;
    if (nullable2.Value)
    {
      nullable2 = ((PXSelectBase<APSetup>) this.apsetup).Current.RequireVendorRef;
      num3 = nullable2.Value ? 1 : 0;
    }
    else
      num3 = 0;
    bool flag3 = num3 != 0;
    PXDefaultAttribute.SetPersistingCheck<POLandedCostDoc.vendorRefNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, (object) e.Row, flag3 ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetRequired<POLandedCostDoc.vendorRefNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDoc>>) e).Cache, flag3);
    if (GraphHelper.RowCast<POLandedCostDetail>((IEnumerable) ((PXSelectBase<POLandedCostDetail>) this.Details).Select(Array.Empty<object>())).Where<POLandedCostDetail>((Func<POLandedCostDetail, bool>) (t => string.IsNullOrEmpty(t.APDocType) && string.IsNullOrEmpty(t.APRefNbr))).ToList<POLandedCostDetail>().Any<POLandedCostDetail>())
      return;
    ((PXAction) this.createAPInvoice).SetEnabled(false);
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDBDefault(typeof (POLandedCostDoc.taxZoneID))]
  [PXUIField(DisplayName = "Vendor Tax Zone", Enabled = false)]
  protected virtual void POLandedCostTaxTran_TaxZoneID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<POLandedCostReceiptLine.pOReceiptBaseCuryID> e)
  {
    PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = ((PXGraph) this).FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo();
    if (defaultCurrencyInfo == null)
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<POLandedCostReceiptLine.pOReceiptBaseCuryID>>) e).ReturnValue = (object) defaultCurrencyInfo.BaseCuryID;
  }

  protected virtual void _(PX.Data.Events.RowSelected<POLandedCostDetail> e)
  {
    if (e.Row == null || string.IsNullOrEmpty(e.Row.APRefNbr))
      return;
    PXUIFieldAttribute.SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostDetail>>) e).Cache, (object) e.Row, false);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<POLandedCostDetail> e)
  {
    if (e.Row != null && !string.IsNullOrEmpty(e.Row.APRefNbr))
    {
      e.Cancel = true;
      throw new PXSetPropertyException("Landed costs with a linked AP bill cannot be deleted.");
    }
  }

  protected virtual void _(PX.Data.Events.RowInserted<POLandedCostDetail> e)
  {
    TaxBaseAttribute.Calculate<POLandedCostDetail.taxCategoryID>(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<POLandedCostDetail>>) e).Cache, ((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<POLandedCostDetail>>) e).Args);
  }

  protected virtual void POLandedCostDetail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    TaxBaseAttribute.Calculate<POLandedCostDetail.taxCategoryID>(sender, e);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<POLandedCostDoc> e)
  {
    if (!((PXSelectBase<POSetup>) this.posetup).Current.RequireLandedCostsControlTotal.GetValueOrDefault())
    {
      ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<POLandedCostDoc>>) e).Cache.SetValue<POLandedCostDoc.curyControlTotal>((object) e.Row, (object) e.Row.CuryDocTotal);
    }
    else
    {
      if (e.Row.Hold.GetValueOrDefault())
        return;
      Decimal? curyControlTotal = e.Row.CuryControlTotal;
      Decimal? curyDocTotal = e.Row.CuryDocTotal;
      if (!(curyControlTotal.GetValueOrDefault() == curyDocTotal.GetValueOrDefault() & curyControlTotal.HasValue == curyDocTotal.HasValue))
        ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<POLandedCostDoc>>) e).Cache.RaiseExceptionHandling<POLandedCostDoc.curyControlTotal>((object) e.Row, (object) e.Row.CuryControlTotal, (Exception) new PXSetPropertyException("Document is out of balance."));
      else
        ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<POLandedCostDoc>>) e).Cache.RaiseExceptionHandling<POLandedCostDoc.curyControlTotal>((object) e.Row, (object) e.Row.CuryControlTotal, (Exception) null);
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<POLandedCostDoc.vendorID> e)
  {
    POLandedCostDoc row = (POLandedCostDoc) e.Row;
    ((PXSetup<PX.Objects.GL.Branch, Where<BqlOperand<PX.Objects.GL.Branch.bAccountID, IBqlInt>.IsEqual<BqlField<POLandedCostDoc.vendorID, IBqlInt>.AsOptional>>>) this.company).RaiseFieldUpdated(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<POLandedCostDoc.vendorID>>) e).Cache, e.Row);
    ((PXSetup<PX.Objects.AP.Vendor, Where<BqlOperand<PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsEqual<BqlField<POLandedCostDoc.vendorID, IBqlInt>.AsOptional>>>) this.vendor).RaiseFieldUpdated(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<POLandedCostDoc.vendorID>>) e).Cache, e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<POLandedCostDoc.vendorID>>) e).Cache.SetDefaultExt<POLandedCostDoc.createBill>((object) row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<POLandedCostDoc.vendorID>>) e).Cache.SetDefaultExt<POLandedCostDoc.vendorLocationID>(e.Row);
    if (row.VendorID.HasValue)
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.vendorRelations>())
      {
        PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<POLandedCostDoc.vendorID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
        {
          (object) row
        }, Array.Empty<object>()));
        row.PayToVendorID = (int?) vendor?.PayToVendorID ?? row.VendorID;
      }
      else
        row.PayToVendorID = row.VendorID;
    }
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<POLandedCostDoc.vendorID>>) e).Cache.SetDefaultExt<POLandedCostDoc.termsID>(e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<POLandedCostDoc.vendorID>>) e).Cache.SetDefaultExt<POLandedCostDoc.taxZoneID>(e.Row);
    Validate.VerifyField<POLandedCostDoc.payToVendorID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<POLandedCostDoc.vendorID>>) e).Cache, (object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<POLandedCostDoc.payToVendorID> e)
  {
    if (!(e.Row is POLandedCostDoc row))
      return;
    PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelectReadonly<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<POLandedCostDoc.payToVendorID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLandedCostDoc.payToVendorID>, object, object>) e).NewValue
    }));
    if (vendor != null && vendor.CuryID != null && !vendor.AllowOverrideCury.GetValueOrDefault() && row.CuryID != vendor.CuryID)
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLandedCostDoc.payToVendorID>, object, object>) e).NewValue = (object) vendor.AcctCD;
      throw new PXSetPropertyException("The currency '{1}' of the pay-to vendor '{0}' differs from currency '{2}' of the document.", new object[3]
      {
        (object) vendor.AcctCD,
        (object) vendor.CuryID,
        (object) row.CuryID
      });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<POLandedCostDoc.payToVendorID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<POLandedCostDoc.payToVendorID>>) e).Cache.SetDefaultExt<POLandedCostDoc.termsID>(e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<POLandedCostDoc.payToVendorID>>) e).Cache.SetDefaultExt<POLandedCostDoc.taxZoneID>(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<POLandedCostDetail.landedCostCodeID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<POLandedCostDetail.landedCostCodeID>>) e).Cache.SetDefaultExt<POLandedCostDetail.descr>(e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<POLandedCostDetail.landedCostCodeID>>) e).Cache.SetDefaultExt<POLandedCostDetail.taxCategoryID>(e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<POLandedCostDetail.landedCostCodeID>>) e).Cache.SetDefaultExt<POLandedCostDetail.lCAccrualAcct>(e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<POLandedCostDetail.landedCostCodeID>>) e).Cache.SetDefaultExt<POLandedCostDetail.lCAccrualSub>(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<POLandedCostDoc.vendorLocationID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<POLandedCostDoc.vendorLocationID>>) e).Cache.SetDefaultExt<POLandedCostDoc.branchID>(e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<POLandedCostDoc.vendorLocationID>>) e).Cache.SetDefaultExt<POLandedCostDoc.taxZoneID>(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<POLandedCostDetail.inventoryID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowPersisting<POLandedCostDoc> e)
  {
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo.curyID> e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    PX.Objects.CM.Extensions.CurrencyInfo row = (PX.Objects.CM.Extensions.CurrencyInfo) e.Row;
    POLandedCostDoc current = ((PXSelectBase<POLandedCostDoc>) this.Document).Current;
    if (row == null || current == null)
      return;
    long? curyInfoId1 = row.CuryInfoID;
    long? curyInfoId2 = current.CuryInfoID;
    if (!(curyInfoId1.GetValueOrDefault() == curyInfoId2.GetValueOrDefault() & curyInfoId1.HasValue == curyInfoId2.HasValue) || ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current == null || string.IsNullOrEmpty(((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current.CuryID))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo.curyID>, object, object>) e).NewValue = (object) ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current.CuryID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CM.Extensions.CurrencyInfo.curyID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowPersisted<POLandedCostDetail> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<POLandedCostReceiptLine> e)
  {
    if (e.Row == null || ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostReceiptLine>>) e).Cache.GetStatus((object) e.Row) != 2)
      return;
    bool? nullable1 = e.Row.IsStockItem;
    if (!nullable1.HasValue)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, e.Row.InventoryID);
    if (inventoryItem == null)
      return;
    nullable1 = inventoryItem.IsConverted;
    if (!nullable1.GetValueOrDefault())
      return;
    nullable1 = inventoryItem.StkItem;
    bool? nullable2 = e.Row.IsStockItem;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return;
    string str1 = inventoryItem.InventoryCD.Trim();
    nullable2 = inventoryItem.StkItem;
    string str2 = nullable2.GetValueOrDefault() ? "The landed cost amount cannot be allocated for the {0} item because the stock status of the item has changed. To add the landed cost amount to the inventory account of the item, create an inventory adjustment on the Adjustments (IN303000) form." : "The landed cost amount cannot be allocated for the {0} item because the stock status of the item has changed. To add the landed cost amount to the expense account of the item, create a transaction on the Journal Transactions (GL301000) form.";
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POLandedCostReceiptLine>>) e).Cache.RaiseExceptionHandling<POReceiptLine.inventoryID>((object) e.Row, (object) str1, (Exception) new PXSetPropertyException(str2, new object[1]
    {
      (object) str1
    }));
  }

  protected virtual void _(PX.Data.Events.RowPersisting<POLandedCostReceiptLine> e)
  {
    if (e.Operation == 3 || ((PXSelectBase<POLandedCostDoc>) this.Document).Current == null || ((PXSelectBase<POLandedCostDoc>) this.Document).Current.Released.GetValueOrDefault())
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, e.Row.InventoryID);
    bool? nullable;
    if (inventoryItem != null)
    {
      nullable = inventoryItem.KitItem;
      if (nullable.GetValueOrDefault())
      {
        nullable = inventoryItem.StkItem;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
          ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<POLandedCostReceiptLine>>) e).Cache.RaiseExceptionHandling<POLandedCostReceiptLine.inventoryID>((object) e.Row, (object) inventoryItem.InventoryCD, (Exception) new PXSetPropertyException("A landed cost document cannot be saved with a non-stock kit added."));
      }
    }
    if (e.Operation == 2)
    {
      POReceipt poReceipt = POReceipt.PK.Find((PXGraph) this, e.Row.POReceiptType, e.Row.POReceiptNbr);
      nullable = poReceipt.Canceled;
      if (!nullable.GetValueOrDefault())
      {
        nullable = poReceipt.IsUnderCorrection;
        if (!nullable.GetValueOrDefault())
          return;
      }
      throw new PXLockViolationException(typeof (POReceipt), (PXDBOperation) 2, (object[]) new string[2]
      {
        poReceipt.ReceiptType,
        poReceipt.ReceiptNbr
      });
    }
  }

  public virtual IEnumerable PoReceiptSelection()
  {
    \u003C\u003Ef__AnonymousType84<string, string, int>[] currentReceiptNbrs = GraphHelper.RowCast<POLandedCostReceiptLine>((IEnumerable) ((PXSelectBase<POLandedCostReceiptLine>) this.ReceiptLines).Select(Array.Empty<object>())).GroupBy(t => new
    {
      ReceiptType = t.POReceiptType,
      ReceiptNbr = t.POReceiptNbr
    }).Select(t => new
    {
      ReceiptType = t.Key.ReceiptType,
      ReceiptNbr = t.Key.ReceiptNbr,
      RowCount = t.Count<POLandedCostReceiptLine>()
    }).ToArray();
    int startRow = PXView.StartRow;
    int num = 0;
    int maximumRows = PXView.MaximumRows;
    if (currentReceiptNbrs.Any())
      maximumRows += currentReceiptNbrs.Length;
    List<object> source;
    using (new PXFieldScope(((PXSelectBase) this.poReceiptSelectionView).View, new System.Type[9]
    {
      typeof (POReceipt.receiptType),
      typeof (POReceipt.receiptNbr),
      typeof (POReceipt.invoiceNbr),
      typeof (POReceipt.vendorID),
      typeof (POReceipt.branchID),
      typeof (POReceipt.curyID),
      typeof (POReceipt.receiptDate),
      typeof (POReceipt.orderQty),
      typeof (POReceiptLinesCount)
    }))
      source = ((PXSelectBase) this.poReceiptSelectionView).View.Select(PXView.Currents, PXView.Parameters, new object[PXView.SortColumns.Length], PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, maximumRows, ref num);
    PXView.StartRow = 0;
    return (IEnumerable) GraphHelper.RowCast<POReceipt>((IEnumerable) source.Select<object, PXResult<POReceipt, POReceiptLinesCount>>((Func<object, PXResult<POReceipt, POReceiptLinesCount>>) (t => (PXResult<POReceipt, POReceiptLinesCount>) t)).Where<PXResult<POReceipt, POReceiptLinesCount>>((Func<PXResult<POReceipt, POReceiptLinesCount>, bool>) (t => !currentReceiptNbrs.Contains(new
    {
      ReceiptType = PXResult<POReceipt, POReceiptLinesCount>.op_Implicit(t).ReceiptType,
      ReceiptNbr = PXResult<POReceipt, POReceiptLinesCount>.op_Implicit(t).ReceiptNbr,
      RowCount = PXResult<POReceipt, POReceiptLinesCount>.op_Implicit(t).LinesCount.GetValueOrDefault()
    })))).ToList<POReceipt>();
  }

  public virtual IEnumerable PoReceiptLinesSelection()
  {
    \u003C\u003Ef__AnonymousType85<string, string, int?>[] currentReceiptLines = GraphHelper.RowCast<POLandedCostReceiptLine>((IEnumerable) ((PXSelectBase<POLandedCostReceiptLine>) this.ReceiptLines).Select(Array.Empty<object>())).Select(t => new
    {
      POReceiptType = t.POReceiptType,
      POReceiptNbr = t.POReceiptNbr,
      POReceiptLineNbr = t.POReceiptLineNbr
    }).ToArray();
    int startRow = PXView.StartRow;
    int num = 0;
    int maximumRows = PXView.MaximumRows;
    if (currentReceiptLines.Any() && maximumRows > 0)
      maximumRows += currentReceiptLines.Length;
    List<POReceiptLineAdd> list = GraphHelper.RowCast<POReceiptLineAdd>((IEnumerable) ((PXSelectBase) this.poReceiptLinesSelectionView).View.Select(PXView.Currents, PXView.Parameters, new object[PXView.SortColumns.Length], PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, maximumRows, ref num)).ToList<POReceiptLineAdd>();
    PXView.StartRow = 0;
    Func<POReceiptLineAdd, bool> predicate = (Func<POReceiptLineAdd, bool>) (t => !currentReceiptLines.Contains(new
    {
      POReceiptType = t.ReceiptType,
      POReceiptNbr = t.ReceiptNbr,
      POReceiptLineNbr = t.LineNbr
    }));
    return (IEnumerable) list.Where<POReceiptLineAdd>(predicate).ToList<POReceiptLineAdd>();
  }

  public virtual void Persist()
  {
    using (new PXReadBranchRestrictedScope())
    {
      this.ValidateDocument();
      this.AllocateLandedCosts();
    }
    ((PXGraph) this).Persist();
  }

  protected virtual void ValidateDocument()
  {
    if (((PXSelectBase<POLandedCostDoc>) this.Document).Current == null)
      return;
    POLandedCostDoc current = ((PXSelectBase<POLandedCostDoc>) this.Document).Current;
    POLandedCostDetail[] array1 = GraphHelper.RowCast<POLandedCostDetail>((IEnumerable) ((PXSelectBase<POLandedCostDetail>) this.Details).Select(Array.Empty<object>())).ToArray<POLandedCostDetail>();
    POLandedCostReceiptLine[] array2 = GraphHelper.RowCast<POLandedCostReceiptLine>((IEnumerable) ((PXSelectBase<POLandedCostReceiptLine>) this.ReceiptLines).Select(Array.Empty<object>())).ToArray<POLandedCostReceiptLine>();
    if (((IEnumerable<POLandedCostReceiptLine>) array2).Any<POLandedCostReceiptLine>())
    {
      bool flag = false;
      string str = "";
      foreach (POLandedCostDetail row in array1)
      {
        string message;
        if (!LandedCostAllocationService.Instance.ValidateLCTran((PXGraph) this, current, (IEnumerable<POLandedCostReceiptLine>) array2, row, out message))
        {
          if (((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<POLandedCostDetail.landedCostCodeID>((object) row, (object) row.LandedCostCodeID, (Exception) new PXSetPropertyException(message, (PXErrorLevel) 5)))
            throw new PXRowPersistingException(typeof (POLandedCostDetail.landedCostCodeID).Name, (object) row.LandedCostCodeID, message);
          flag = true;
          str = message;
        }
      }
      if (flag)
        throw new PXException(str);
    }
    else
    {
      bool? hold = current.Hold;
      bool flag = false;
      if (hold.GetValueOrDefault() == flag & hold.HasValue)
        throw new PXException("Landed Costs document details are empty.");
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold")]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter)
  {
    return (IEnumerable) adapter.Get<POLandedCostDoc>();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Remove Hold")]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter)
  {
    return (IEnumerable) adapter.Get<POLandedCostDoc>();
  }

  [PXUIField]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable CreateAPInvoice(PXAdapter adapter)
  {
    if (((PXSelectBase<POLandedCostDoc>) this.Document).Current != null)
    {
      POLandedCostDoc current = ((PXSelectBase<POLandedCostDoc>) this.Document).Current;
      if (current.Released.GetValueOrDefault())
      {
        ((PXSelectBase<POLandedCostDoc>) this.Document).Current = current;
        List<POLandedCostDetail> list1 = GraphHelper.RowCast<POLandedCostDetail>((IEnumerable) ((PXSelectBase<POLandedCostDetail>) this.Details).Select(Array.Empty<object>())).Where<POLandedCostDetail>((Func<POLandedCostDetail, bool>) (t => string.IsNullOrEmpty(t.APDocType) && string.IsNullOrEmpty(t.APRefNbr))).ToList<POLandedCostDetail>();
        List<POLandedCostTaxTran> list2 = ((IEnumerable<PXResult<POLandedCostTaxTran>>) ((PXSelectBase<POLandedCostTaxTran>) this.Taxes).Select(Array.Empty<object>())).AsEnumerable<PXResult<POLandedCostTaxTran>>().Select(r => new
        {
          TaxTran = PXResult.Unwrap<POLandedCostTaxTran>((object) r),
          Tax = PXResult.Unwrap<PX.Objects.TX.Tax>((object) r)
        }).OrderBy(r => r.Tax, (IComparer<PX.Objects.TX.Tax>) TaxByCalculationLevelComparer.Instance).Select(r => r.TaxTran).ToList<POLandedCostTaxTran>();
        if (!list1.Any<POLandedCostDetail>())
          return adapter.Get();
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = ((PXGraph) this).FindImplementation<IPXCurrencyHelper>().GetCurrencyInfo(current.CuryInfoID);
        APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = ((PXGraph) instance).FindImplementation<APInvoiceEntry.MultiCurrency>().CloneCurrencyInfo(currencyInfo1);
        LandedCostAPBillFactory apBillFactory = this.GetApBillFactory();
        PX.Objects.AP.APInvoice landedCostBillHeader = apBillFactory.CreateLandedCostBillHeader(current, (IEnumerable<POLandedCostDetail>) list1, new PX.Objects.AP.APInvoice());
        PX.Objects.AP.APInvoice apInvoice1 = new PX.Objects.AP.APInvoice();
        apInvoice1.CuryInfoID = currencyInfo2.CuryInfoID;
        apInvoice1.CuryID = currencyInfo2.CuryID;
        apInvoice1.BranchID = landedCostBillHeader.BranchID;
        apInvoice1.DocType = landedCostBillHeader.DocType;
        apInvoice1.DocDate = landedCostBillHeader.DocDate;
        PX.Objects.AP.APInvoice apInvoice2 = apInvoice1;
        PX.Objects.AP.APInvoice newdoc = ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Insert(apInvoice2);
        APInvoiceWrapper landedCostBill = apBillFactory.CreateLandedCostBill(current, (IEnumerable<POLandedCostDetail>) list1, (IEnumerable<POLandedCostTaxTran>) list2, newdoc);
        ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Update(landedCostBill.Document);
        TaxBaseAttribute.SetTaxCalc<PX.Objects.AP.APTran.taxCategoryID, APTaxAttribute>(((PXSelectBase) instance.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
        foreach (PX.Objects.AP.APTran transaction in (IEnumerable<PX.Objects.AP.APTran>) landedCostBill.Transactions)
        {
          PX.Objects.AP.APTran tran = ((PXSelectBase<PX.Objects.AP.APTran>) instance.Transactions).Insert(transaction);
          instance.LandedCostDetailSetLink(tran);
        }
        foreach (APTaxTran apTaxTran in GraphHelper.RowCast<APTaxTran>(((PXSelectBase) instance.Taxes).Cache.Cached))
          ((PXSelectBase<APTaxTran>) instance.Taxes).Delete(apTaxTran);
        foreach (APTaxTran tax in (IEnumerable<APTaxTran>) landedCostBill.Taxes)
          this.InsertAPTaxTran(instance, tax);
        current.APDocCreated = new bool?(true);
        GraphHelper.MarkUpdated((PXCache) GraphHelper.Caches<POLandedCostDoc>((PXGraph) instance), (object) current, true);
        throw new PXRedirectRequiredException((PXGraph) instance, "Enter AP Bill");
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddPOReceipt(PXAdapter adapter)
  {
    // ISSUE: method pointer
    return ((PXSelectBase<POLandedCostDoc>) this.Document).Current != null && !((PXSelectBase<POLandedCostDoc>) this.Document).Current.Released.GetValueOrDefault() && ((PXSelectBase<POReceipt>) this.poReceiptSelection).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CAddPOReceipt\u003Eb__68_0)), true) == 1 ? this.AddPOReceipt2(adapter) : adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowDeleted<POLandedCostTaxTran> e)
  {
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<POLandedCostDoc.billDate> e)
  {
    POLandedCostDoc row = (POLandedCostDoc) e.Row;
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLandedCostDoc.billDate>, object, object>) e).NewValue == null)
      return;
    int? parentOrganizationId = PXAccess.GetParentOrganizationID(row.BranchID);
    ProcessingResult period = this.FinPeriodUtils.CanPostToPeriod((IFinPeriod) (this.FinPeriodRepository.FindFinPeriodByDate((DateTime?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLandedCostDoc.billDate>, object, object>) e).NewValue, parentOrganizationId) ?? throw new PXSetPropertyException<POLandedCostDoc.billDate>("The financial period that corresponds to the {0} date does not exist in the {1} company.", new object[2]
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<POLandedCostDoc.billDate>, object, object>) e).NewValue,
      (object) PXAccess.GetOrganizationCD(parentOrganizationId)
    })), typeof (FinPeriod.aPClosed));
    if (!period.IsSuccess)
      throw new PXSetPropertyException<POLandedCostDoc.billDate>(period.GetGeneralMessage());
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<POReceiptFilter.receiptType> e)
  {
    string currentReceiptType = this.GetCurrentReceiptType();
    if (string.IsNullOrEmpty(currentReceiptType))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<POReceiptFilter.receiptType>, object, object>) e).NewValue = (object) currentReceiptType;
  }

  protected virtual void _(PX.Data.Events.RowSelected<POReceiptFilter> e)
  {
    string currentReceiptType = this.GetCurrentReceiptType();
    PXUIFieldAttribute.SetEnabled<POReceiptFilter.receiptType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<POReceiptFilter>>) e).Cache, (object) null, string.IsNullOrEmpty(currentReceiptType));
  }

  protected virtual string GetCurrentReceiptType()
  {
    return GraphHelper.RowCast<POLandedCostReceiptLine>((IEnumerable) ((PXSelectBase<POLandedCostReceiptLine>) this.ReceiptLines).SelectWindowed(0, 1, Array.Empty<object>())).FirstOrDefault<POLandedCostReceiptLine>()?.POReceiptType;
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddPOReceipt2(PXAdapter adapter)
  {
    if (((PXSelectBase<POLandedCostDoc>) this.Document).Current != null && !((PXSelectBase<POLandedCostDoc>) this.Document).Current.Released.GetValueOrDefault())
    {
      POReceipt[] array = GraphHelper.RowCast<POReceipt>(((PXSelectBase) this.poReceiptSelection).Cache.Updated).Where<POReceipt>((Func<POReceipt, bool>) (t => t.Selected.GetValueOrDefault() && t.ReceiptType == ((PXSelectBase<POReceiptFilter>) this.filter).Current.ReceiptType)).ToArray<POReceipt>();
      this.AddPurchaseReceipts((IEnumerable<POReceipt>) array);
      EnumerableExtensions.ForEach<POReceipt>((IEnumerable<POReceipt>) array, (System.Action<POReceipt>) (t => t.Selected = new bool?(false)));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddPOReceiptLine(PXAdapter adapter)
  {
    // ISSUE: method pointer
    return ((PXSelectBase<POLandedCostDoc>) this.Document).Current != null && !((PXSelectBase<POLandedCostDoc>) this.Document).Current.Released.GetValueOrDefault() && ((PXSelectBase<POReceiptLineAdd>) this.poReceiptLinesSelection).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CAddPOReceiptLine\u003Eb__77_0)), true) == 1 ? this.AddPOReceiptLine2(adapter) : adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddPOReceiptLine2(PXAdapter adapter)
  {
    if (((PXSelectBase<POLandedCostDoc>) this.Document).Current != null && !((PXSelectBase<POLandedCostDoc>) this.Document).Current.Released.GetValueOrDefault())
    {
      POReceiptLineAdd[] array = GraphHelper.RowCast<POReceiptLineAdd>(((PXSelectBase) this.poReceiptLinesSelection).Cache.Updated).Where<POReceiptLineAdd>((Func<POReceiptLineAdd, bool>) (t => t.Selected.GetValueOrDefault())).ToArray<POReceiptLineAdd>();
      this.AddPurchaseReceiptLines((IEnumerable<POReceiptLineAdd>) array);
      EnumerableExtensions.ForEach<POReceiptLineAdd>((IEnumerable<POReceiptLineAdd>) array, (System.Action<POReceiptLineAdd>) (t => t.Selected = new bool?(false)));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddLC(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable Release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    POLandedCostDocEntry.\u003C\u003Ec__DisplayClass83_0 cDisplayClass830 = new POLandedCostDocEntry.\u003C\u003Ec__DisplayClass83_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass830.list = new List<POLandedCostDoc>();
    foreach (POLandedCostDoc poLandedCostDoc in adapter.Get<POLandedCostDoc>())
    {
      bool? nullable = poLandedCostDoc.Hold;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = poLandedCostDoc.Released;
        bool flag2 = false;
        if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
        {
          // ISSUE: reference to a compiler-generated field
          cDisplayClass830.list.Add(((PXSelectBase<POLandedCostDoc>) this.Document).Update(poLandedCostDoc));
        }
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass830.list.Count == 0)
      throw new PXException("Document Status is invalid for processing.");
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass830, __methodptr(\u003CRelease\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass830.list;
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Action(PXAdapter adapter, [PXString] string ActionName)
  {
    if (!string.IsNullOrEmpty(ActionName))
    {
      PXAction action = ((PXGraph) this).Actions[ActionName];
      if (action != null)
      {
        ((PXAction) this.Save).Press();
        List<object> objectList = new List<object>();
        foreach (object obj in action.Press(adapter))
          objectList.Add(obj);
        return (IEnumerable) objectList;
      }
    }
    return adapter.Get();
  }

  public virtual void AddPurchaseReceipts(IEnumerable<POReceipt> receipts)
  {
    List<POReceiptLineAdd> lines = new List<POReceiptLineAdd>();
    POReceiptFilter current = ((PXSelectBase<POReceiptFilter>) this.filter).Current;
    foreach (POReceipt receipt in receipts)
    {
      ((PXSelectBase) this.filter).Cache.Remove((object) ((PXSelectBase<POReceiptFilter>) this.filter).Current);
      ((PXSelectBase) this.filter).Cache.Insert((object) new POReceiptFilter());
      ((PXSelectBase<POReceiptFilter>) this.filter).Current.ReceiptType = receipt.ReceiptType;
      ((PXSelectBase<POReceiptFilter>) this.filter).Current.ReceiptNbr = receipt.ReceiptNbr;
      POReceiptLineAdd[] array = GraphHelper.RowCast<POReceiptLineAdd>((IEnumerable) ((PXSelectBase<POReceiptLineAdd>) this.poReceiptLinesSelection).Select(Array.Empty<object>())).ToArray<POReceiptLineAdd>();
      lines.AddRange((IEnumerable<POReceiptLineAdd>) array);
    }
    ((PXSelectBase) this.filter).Cache.Remove((object) ((PXSelectBase<POReceiptFilter>) this.filter).Current);
    ((PXSelectBase) this.filter).Cache.Insert((object) current);
    this.AddPurchaseReceiptLines((IEnumerable<POReceiptLineAdd>) lines);
  }

  protected virtual POReceiptLineAdd[] GetLinesWithoutDuplicates(IEnumerable<POReceiptLineAdd> lines)
  {
    List<\u003C\u003Ef__AnonymousType87<string, string, int?>> asd = GraphHelper.RowCast<POLandedCostReceiptLine>((IEnumerable) ((PXSelectBase<POLandedCostReceiptLine>) this.ReceiptLines).Select(Array.Empty<object>())).Select(t => new
    {
      ReceiptType = t.POReceiptType,
      ReceiptNbr = t.POReceiptNbr,
      ReceiptLineNbr = t.POReceiptLineNbr
    }).ToList();
    return lines.Where<POReceiptLineAdd>((Func<POReceiptLineAdd, bool>) (t => !asd.Contains(new
    {
      ReceiptType = t.ReceiptType,
      ReceiptNbr = t.ReceiptNbr,
      ReceiptLineNbr = t.LineNbr
    }))).ToArray<POReceiptLineAdd>();
  }

  public virtual void AddPurchaseReceiptLines(IEnumerable<POReceiptLineAdd> lines)
  {
    if (((PXSelectBase<POLandedCostDoc>) this.Document).Current == null || lines == null)
      return;
    foreach (POReceiptLineAdd withoutDuplicate in this.GetLinesWithoutDuplicates(lines))
    {
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) withoutDuplicate.CuryInfoID
      }));
      ((PXSelectBase<POLandedCostReceiptLine>) this.ReceiptLines).Insert(new POLandedCostReceiptLine()
      {
        DocType = ((PXSelectBase<POLandedCostDoc>) this.Document).Current.DocType,
        RefNbr = ((PXSelectBase<POLandedCostDoc>) this.Document).Current.RefNbr,
        POReceiptType = withoutDuplicate.ReceiptType,
        POReceiptNbr = withoutDuplicate.ReceiptNbr,
        POReceiptLineNbr = withoutDuplicate.LineNbr,
        BranchID = withoutDuplicate.BranchID,
        SiteID = withoutDuplicate.SiteID,
        InventoryID = withoutDuplicate.InventoryID,
        SubItemID = withoutDuplicate.SubItemID,
        UOM = withoutDuplicate.UOM,
        BaseReceiptQty = withoutDuplicate.BaseReceiptQty,
        ReceiptQty = withoutDuplicate.ReceiptQty,
        POReceiptBaseCuryID = currencyInfo.BaseCuryID,
        LineAmt = withoutDuplicate.TranCostFinal,
        AllocatedLCAmt = new Decimal?(0M),
        CuryAllocatedLCAmt = new Decimal?(0M),
        UnitWeight = withoutDuplicate.UnitWeight,
        UnitVolume = withoutDuplicate.UnitVolume
      }).IsStockItem = withoutDuplicate.IsStockItem;
    }
  }

  public virtual void AllocateLandedCosts()
  {
    if (((PXSelectBase<POLandedCostDoc>) this.Document).Current == null)
      return;
    POLandedCostDoc current = ((PXSelectBase<POLandedCostDoc>) this.Document).Current;
    List<POLandedCostReceiptLine> list1 = ((IEnumerable<PXResult<POLandedCostReceiptLine>>) ((PXSelectBase<POLandedCostReceiptLine>) this.ReceiptLines).Select(Array.Empty<object>())).AsEnumerable<PXResult<POLandedCostReceiptLine>>().Select<PXResult<POLandedCostReceiptLine>, POLandedCostReceiptLine>((Func<PXResult<POLandedCostReceiptLine>, POLandedCostReceiptLine>) (t => PXResult<POLandedCostReceiptLine>.op_Implicit(t))).ToList<POLandedCostReceiptLine>();
    List<POLandedCostDetail> list2 = ((IEnumerable<PXResult<POLandedCostDetail>>) ((PXSelectBase<POLandedCostDetail>) this.Details).Select(Array.Empty<object>())).AsEnumerable<PXResult<POLandedCostDetail>>().Select<PXResult<POLandedCostDetail>, POLandedCostDetail>((Func<PXResult<POLandedCostDetail>, POLandedCostDetail>) (t => PXResult<POLandedCostDetail>.op_Implicit(t))).ToList<POLandedCostDetail>();
    List<PXResult<POLandedCostTax, PX.Objects.TX.Tax>> list3 = ((IEnumerable<PXResult<POLandedCostTax>>) ((PXSelectBase<POLandedCostTax>) new PXSelectJoin<POLandedCostTax, InnerJoin<PX.Objects.TX.Tax, On<POLandedCostTax.taxID, Equal<PX.Objects.TX.Tax.taxID>>>, Where<POLandedCostTax.docType, Equal<Required<POLandedCostTax.docType>>, And<POLandedCostTax.refNbr, Equal<Required<POLandedCostTax.refNbr>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) current.DocType,
      (object) current.RefNbr
    })).AsEnumerable<PXResult<POLandedCostTax>>().Select<PXResult<POLandedCostTax>, PXResult<POLandedCostTax, PX.Objects.TX.Tax>>((Func<PXResult<POLandedCostTax>, PXResult<POLandedCostTax, PX.Objects.TX.Tax>>) (t => (PXResult<POLandedCostTax, PX.Objects.TX.Tax>) t)).ToList<PXResult<POLandedCostTax, PX.Objects.TX.Tax>>();
    LandedCostAllocationService.POLandedCostReceiptLineAdjustment[] adjustments = LandedCostAllocationService.Instance.Allocate((PXGraph) this, current, (IEnumerable<POLandedCostReceiptLine>) list1, (IEnumerable<POLandedCostDetail>) list2, (IEnumerable<PXResult<POLandedCostTax, PX.Objects.TX.Tax>>) list3);
    POLandedCostSplit[] landedCostSplits = LandedCostAllocationService.Instance.GetLandedCostSplits(current, adjustments);
    this.TrackLandedCostSplits((IEnumerable<POLandedCostSplit>) landedCostSplits);
    PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = ((PXGraph) this).FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo();
    foreach (POLandedCostReceiptLine landedCostReceiptLine1 in list1)
    {
      POLandedCostReceiptLine landedCostReceiptLine = landedCostReceiptLine1;
      List<POLandedCostSplit> list4 = ((IEnumerable<POLandedCostSplit>) landedCostSplits).Where<POLandedCostSplit>((Func<POLandedCostSplit, bool>) (t =>
      {
        int? receiptLineNbr = t.ReceiptLineNbr;
        int? lineNbr = landedCostReceiptLine.LineNbr;
        return receiptLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & receiptLineNbr.HasValue == lineNbr.HasValue;
      })).ToList<POLandedCostSplit>();
      landedCostReceiptLine.AllocatedLCAmt = list4.Sum<POLandedCostSplit>((Func<POLandedCostSplit, Decimal?>) (t => t.LineAmt));
      landedCostReceiptLine.CuryAllocatedLCAmt = new Decimal?(defaultCurrencyInfo.CuryConvCury(landedCostReceiptLine.AllocatedLCAmt.GetValueOrDefault()));
      ((PXSelectBase) this.ReceiptLines).Cache.Update((object) landedCostReceiptLine);
    }
    current.AllocatedTotal = new Decimal?(list1.Sum<POLandedCostReceiptLine>((Func<POLandedCostReceiptLine, Decimal>) (t => t.AllocatedLCAmt.GetValueOrDefault())));
    current.CuryAllocatedTotal = new Decimal?(defaultCurrencyInfo.CuryConvCury(current.AllocatedTotal.Value));
    ((PXSelectBase) this.Document).Cache.Update((object) current);
  }

  public virtual void ReleaseDoc(List<POLandedCostDoc> list)
  {
    foreach (POLandedCostDoc doc in list)
      this.ReleaseDoc(doc);
  }

  public virtual void ReleaseDoc(POLandedCostDoc doc)
  {
    if (doc == null)
      return;
    ((PXGraph) this).Clear();
    PXSelect<POLandedCostDoc> document1 = this.Document;
    PXSelect<POLandedCostDoc> document2 = this.Document;
    string refNbr = doc.RefNbr;
    object[] objArray = new object[1]
    {
      (object) doc.DocType
    };
    POLandedCostDoc poLandedCostDoc1;
    POLandedCostDoc poLandedCostDoc2 = poLandedCostDoc1 = PXResultset<POLandedCostDoc>.op_Implicit(((PXSelectBase<POLandedCostDoc>) document2).Search<POLandedCostDoc.refNbr>((object) refNbr, objArray));
    ((PXSelectBase<POLandedCostDoc>) document1).Current = poLandedCostDoc1;
    doc = poLandedCostDoc2;
    bool? nullable = WorkflowAction.HasWorkflowActionEnabled<POLandedCostDocEntry, POLandedCostDoc>(this, (Expression<Func<POLandedCostDocEntry, PXAction<POLandedCostDoc>>>) (g => g.release), ((PXSelectBase<POLandedCostDoc>) this.Document).Current);
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      throw new PXInvalidOperationException("The {0} action is not available in the {1} document at the moment. The document is being used by another process.", new object[2]
      {
        (object) ((PXAction) this.release).GetCaption(),
        (object) ((PXSelectBase) this.Document).Cache.GetRowDescription((object) ((PXSelectBase<POLandedCostDoc>) this.Document).Current)
      });
    POSetup poSetup = PXResultset<POSetup>.op_Implicit(((PXSelectBase<POSetup>) this.posetup).Select(Array.Empty<object>()));
    string str = "";
    int num = 0;
    nullable = poSetup.AutoReleaseLCIN;
    bool valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = poSetup.AutoReleaseAP;
    bool valueOrDefault2 = nullable.GetValueOrDefault();
    List<POLandedCostReceiptLine> list1 = GraphHelper.RowCast<POLandedCostReceiptLine>((IEnumerable) ((PXSelectBase) this.ReceiptLines).View.SelectMultiBound(new object[1]
    {
      (object) doc
    }, Array.Empty<object>())).ToList<POLandedCostReceiptLine>();
    this.ValidateReceiptLines(list1);
    List<POLandedCostDetail> list2 = GraphHelper.RowCast<POLandedCostDetail>((IEnumerable) ((PXSelectBase) this.Details).View.SelectMultiBound(new object[1]
    {
      (object) doc
    }, Array.Empty<object>())).ToList<POLandedCostDetail>();
    List<PXResult<POLandedCostTax, PX.Objects.TX.Tax>> list3 = ((IQueryable<PXResult<POLandedCostTax>>) ((PXSelectBase<POLandedCostTax>) new PXSelectJoin<POLandedCostTax, InnerJoin<PX.Objects.TX.Tax, On<POLandedCostTax.taxID, Equal<PX.Objects.TX.Tax.taxID>>>, Where<POLandedCostTax.docType, Equal<Required<POLandedCostTax.docType>>, And<POLandedCostTax.refNbr, Equal<Required<POLandedCostTax.refNbr>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    })).Select<PXResult<POLandedCostTax>, PXResult<POLandedCostTax, PX.Objects.TX.Tax>>((Expression<Func<PXResult<POLandedCostTax>, PXResult<POLandedCostTax, PX.Objects.TX.Tax>>>) (t => (PXResult<POLandedCostTax, PX.Objects.TX.Tax>) t)).ToList<PXResult<POLandedCostTax, PX.Objects.TX.Tax>>();
    List<POLandedCostTaxTran> list4 = ((PXSelectBase) this.Taxes).View.SelectMultiBound(new object[1]
    {
      (object) doc
    }, Array.Empty<object>()).AsEnumerable<object>().Select(r => new
    {
      TaxTran = PXResult.Unwrap<POLandedCostTaxTran>(r),
      Tax = PXResult.Unwrap<PX.Objects.TX.Tax>(r)
    }).OrderBy(r => r.Tax, (IComparer<PX.Objects.TX.Tax>) TaxByCalculationLevelComparer.Instance).Select(r => r.TaxTran).ToList<POLandedCostTaxTran>();
    if (!list1.Any<POLandedCostReceiptLine>() || !list2.Any<POLandedCostDetail>())
      throw new PXException("Cannot release landed costs without document detail lines.");
    if (GraphHelper.RowCast<POReceipt>((IEnumerable) ((PXSelectBase<POLandedCostReceipt>) new PXSelectJoin<POLandedCostReceipt, InnerJoin<POReceipt, On<POLandedCostReceipt.FK.Receipt>>, Where<POReceipt.released, Equal<False>, And<POLandedCostReceipt.lCDocType, Equal<Required<POLandedCostReceipt.lCDocType>>, And<POLandedCostReceipt.lCRefNbr, Equal<Required<POLandedCostReceipt.lCRefNbr>>>>>>((PXGraph) this)).SelectWindowed(0, 1, new object[2]
    {
      (object) doc.DocType,
      (object) doc.RefNbr
    })).Any<POReceipt>())
      throw new PXException("Cannot release landed costs with unreleased receipts.");
    List<PX.Objects.IN.INRegister> inRegisterList = new List<PX.Objects.IN.INRegister>();
    List<PX.Objects.AP.APRegister> apRegisterList = new List<PX.Objects.AP.APRegister>();
    LandedCostAllocationService.POLandedCostReceiptLineAdjustment[] adjustments = LandedCostAllocationService.Instance.Allocate((PXGraph) this, doc, (IEnumerable<POLandedCostReceiptLine>) list1, (IEnumerable<POLandedCostDetail>) list2, (IEnumerable<PXResult<POLandedCostTax, PX.Objects.TX.Tax>>) list3);
    POLandedCostSplit[] landedCostSplits = LandedCostAllocationService.Instance.GetLandedCostSplits(doc, adjustments);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      try
      {
        inRegisterList = this.CreateLandedCostAdjustment(doc, (IEnumerable<LandedCostAllocationService.POLandedCostReceiptLineAdjustment>) adjustments);
        ((SelectedEntityEvent<POLandedCostDoc>) PXEntityEventBase<POLandedCostDoc>.Container<POLandedCostDoc.Events>.Select((Expression<Func<POLandedCostDoc.Events, PXEntityEvent<POLandedCostDoc.Events>>>) (ev => ev.InventoryAdjustmentCreated))).FireOn((PXGraph) this, doc);
        doc = ((PXSelectBase<POLandedCostDoc>) this.Document).Update(doc);
        ((PXAction) this.Save).Press();
        transactionScope.Complete();
      }
      catch (Exception ex)
      {
        throw new PXException(ex, "IN Document has not been created with the following error: {0}", new object[1]
        {
          (object) ex.Message
        });
      }
    }
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      try
      {
        if (doc.CreateBill.GetValueOrDefault() && list2.All<POLandedCostDetail>((Func<POLandedCostDetail, bool>) (t => string.IsNullOrEmpty(t.APRefNbr))))
          apRegisterList = this.CreateLandedCostBill(doc, (IEnumerable<POLandedCostReceiptLine>) list1, (IEnumerable<POLandedCostDetail>) list2, (IEnumerable<POLandedCostSplit>) landedCostSplits, (IEnumerable<POLandedCostTaxTran>) list4);
        doc = ((PXSelectBase<POLandedCostDoc>) this.Document).Update(doc);
        ((PXAction) this.Save).Press();
        transactionScope.Complete();
      }
      catch (Exception ex)
      {
        PXTrace.WriteError(ex);
        ++num;
        str = PXLocalizer.LocalizeFormat("AP Document has not been created with the following error: {0}", new object[1]
        {
          (object) ex.Message
        });
      }
    }
    if (valueOrDefault1)
    {
      try
      {
        if (inRegisterList.Any<PX.Objects.IN.INRegister>())
          INDocumentRelease.ReleaseDoc(inRegisterList, false);
      }
      catch (Exception ex)
      {
        PXTrace.WriteError(ex);
        ++num;
        str = PXLocalizer.LocalizeFormat("IN Document failed to release with the following error: {0}", new object[1]
        {
          (object) ex.Message
        });
      }
    }
    if (valueOrDefault2)
    {
      try
      {
        if (apRegisterList.Any<PX.Objects.AP.APRegister>())
          APDocumentRelease.ReleaseDoc(apRegisterList, true);
      }
      catch (Exception ex)
      {
        PXTrace.WriteError(ex);
        ++num;
        str = PXLocalizer.LocalizeFormat("AP Document failed to release with the following error: {0}", new object[1]
        {
          (object) ex.Message
        });
      }
    }
    if (num == 1)
      throw new PXException(str);
    if (num > 1)
      throw new PXException("Landed costs release has many errors. Check Trace for details.");
  }

  protected virtual void ValidateReceiptLines(List<POLandedCostReceiptLine> details)
  {
    foreach (object detail in details)
      ConvertedInventoryItemAttribute.ValidateRow(((PXSelectBase) this.ReceiptLines).Cache, detail);
  }

  protected virtual List<PX.Objects.IN.INRegister> CreateLandedCostAdjustment(
    POLandedCostDoc doc,
    IEnumerable<LandedCostAllocationService.POLandedCostReceiptLineAdjustment> adjustments)
  {
    POSetup poSetup = PXResultset<POSetup>.op_Implicit(((PXSelectBase<POSetup>) this.posetup).Select(Array.Empty<object>()));
    INAdjustmentEntry instance = PXGraph.CreateInstance<INAdjustmentEntry>();
    IDictionary<POLandedCostDetail, INAdjustmentWrapper> landedCostAdjustments = this.GetInAdjustmentFactory((PXGraph) instance).CreateLandedCostAdjustments(doc, adjustments);
    bool valueOrDefault = poSetup.AutoReleaseLCIN.GetValueOrDefault();
    List<PX.Objects.IN.INRegister> landedCostAdjustment = new List<PX.Objects.IN.INRegister>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<INTran.inventoryID>(POLandedCostDocEntry.\u003C\u003Ec.\u003C\u003E9__94_0 ?? (POLandedCostDocEntry.\u003C\u003Ec.\u003C\u003E9__94_0 = new PXFieldVerifying((object) POLandedCostDocEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCreateLandedCostAdjustment\u003Eb__94_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<INTran.origRefNbr>(POLandedCostDocEntry.\u003C\u003Ec.\u003C\u003E9__94_1 ?? (POLandedCostDocEntry.\u003C\u003Ec.\u003C\u003E9__94_1 = new PXFieldVerifying((object) POLandedCostDocEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCreateLandedCostAdjustment\u003Eb__94_1))));
    foreach (KeyValuePair<POLandedCostDetail, INAdjustmentWrapper> keyValuePair in (IEnumerable<KeyValuePair<POLandedCostDetail, INAdjustmentWrapper>>) landedCostAdjustments)
    {
      ((PXSelectBase<INSetup>) instance.insetup).Current.RequireControlTotal = new bool?(false);
      if (valueOrDefault)
        ((PXSelectBase<INSetup>) instance.insetup).Current.HoldEntry = new bool?(false);
      ((PXSelectBase<PX.Objects.IN.INRegister>) instance.adjustment).Insert(keyValuePair.Value.Document);
      foreach (INTran transaction in (IEnumerable<INTran>) keyValuePair.Value.Transactions)
      {
        instance.CostCenterDispatcherExt?.SetInventorySource(transaction);
        ((PXSelectBase<INTran>) instance.transactions).Insert(transaction);
      }
      ((PXAction) instance.Save).Press();
      PX.Objects.IN.INRegister current = ((PXSelectBase<PX.Objects.IN.INRegister>) instance.adjustment).Current;
      keyValuePair.Key.INDocType = current.DocType;
      keyValuePair.Key.INRefNbr = current.RefNbr;
      ((PXSelectBase) this.Details).Cache.Update((object) keyValuePair.Key);
      landedCostAdjustment.Add(current);
      ((PXGraph) instance).Clear();
    }
    doc.INDocCreated = new bool?(true);
    ((PXSelectBase) this.Document).Cache.Update((object) doc);
    return landedCostAdjustment;
  }

  protected virtual List<PX.Objects.AP.APRegister> CreateLandedCostBill(
    POLandedCostDoc doc,
    IEnumerable<POLandedCostReceiptLine> receiptLines,
    IEnumerable<POLandedCostDetail> details,
    IEnumerable<POLandedCostSplit> splits,
    IEnumerable<POLandedCostTaxTran> taxes)
  {
    int num = PXResultset<POSetup>.op_Implicit(((PXSelectBase<POSetup>) this.posetup).Select(Array.Empty<object>())).AutoReleaseAP.GetValueOrDefault() ? 1 : 0;
    APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
    ((PXSelectBase<APSetup>) instance.APSetup).Current.RequireControlTotal = new bool?(false);
    ((PXSelectBase<APSetup>) instance.APSetup).Current.RequireControlTaxTotal = new bool?(false);
    if (num != 0)
      ((PXSelectBase<APSetup>) instance.APSetup).Current.HoldEntry = new bool?(false);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = ((PXGraph) instance).FindImplementation<IPXCurrencyHelper>().GetCurrencyInfo(doc.CuryInfoID);
    currencyInfo1.CuryInfoID = new long?();
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) instance.currencyinfo).Insert(currencyInfo1);
    LandedCostAPBillFactory apBillFactory = this.GetApBillFactory();
    PX.Objects.AP.APInvoice landedCostBillHeader = apBillFactory.CreateLandedCostBillHeader(doc, details, new PX.Objects.AP.APInvoice());
    PX.Objects.AP.APInvoice apInvoice1 = new PX.Objects.AP.APInvoice();
    apInvoice1.CuryInfoID = currencyInfo2.CuryInfoID;
    apInvoice1.CuryID = currencyInfo2.CuryID;
    apInvoice1.BranchID = landedCostBillHeader.BranchID;
    apInvoice1.DocType = landedCostBillHeader.DocType;
    apInvoice1.DocDate = landedCostBillHeader.DocDate;
    PX.Objects.AP.APInvoice apInvoice2 = apInvoice1;
    PX.Objects.AP.APInvoice newdoc = ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Insert(apInvoice2);
    APInvoiceWrapper landedCostBill1 = apBillFactory.CreateLandedCostBill(doc, details, taxes, newdoc);
    List<PX.Objects.AP.APRegister> landedCostBill2 = new List<PX.Objects.AP.APRegister>();
    PX.Objects.AP.APInvoice apInvoice3 = ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Update(landedCostBill1.Document);
    TaxBaseAttribute.SetTaxCalc<PX.Objects.AP.APTran.taxCategoryID, APTaxAttribute>(((PXSelectBase) instance.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
    foreach (PX.Objects.AP.APTran transaction in (IEnumerable<PX.Objects.AP.APTran>) landedCostBill1.Transactions)
      ((PXSelectBase<PX.Objects.AP.APTran>) instance.Transactions).Insert(transaction);
    foreach (APTaxTran apTaxTran in GraphHelper.RowCast<APTaxTran>(((PXSelectBase) instance.Taxes).Cache.Cached))
      ((PXSelectBase<APTaxTran>) instance.Taxes).Delete(apTaxTran);
    foreach (APTaxTran tax in (IEnumerable<APTaxTran>) landedCostBill1.Taxes)
      this.InsertAPTaxTran(instance, tax);
    List<PX.Objects.EP.ApprovalMap> assignedMaps = instance.Approval.GetAssignedMaps(((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Current, ((PXSelectBase) instance.Document).Cache);
    if (assignedMaps.Any<PX.Objects.EP.ApprovalMap>())
      instance.Approval.Assign(((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Current, (IEnumerable<PX.Objects.EP.ApprovalMap>) assignedMaps);
    ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).SetValueExt<PX.Objects.AP.APInvoice.curyOrigDiscAmt>(apInvoice3, (object) landedCostBillHeader.CuryOrigDiscAmt);
    ((PXAction) instance.Save).Press();
    foreach (POLandedCostDetail detail in details)
    {
      detail.APDocType = apInvoice3.DocType;
      detail.APRefNbr = apInvoice3.RefNbr;
      ((PXSelectBase) this.Details).Cache.Update((object) detail);
    }
    doc.APDocCreated = new bool?(true);
    ((PXSelectBase) this.Document).Cache.Update((object) doc);
    landedCostBill2.Add((PX.Objects.AP.APRegister) apInvoice3);
    return landedCostBill2;
  }

  protected virtual APTaxTran InsertAPTaxTran(APInvoiceEntry apGraph, APTaxTran newTax)
  {
    APTaxTran apTaxTran = new APTaxTran();
    apTaxTran.Module = "AP";
    ((PXSelectBase) apGraph.Taxes).Cache.SetDefaultExt<APTaxTran.origTranType>((object) apTaxTran);
    ((PXSelectBase) apGraph.Taxes).Cache.SetDefaultExt<APTaxTran.origRefNbr>((object) apTaxTran);
    ((PXSelectBase) apGraph.Taxes).Cache.SetDefaultExt<TaxTran.lineRefNbr>((object) apTaxTran);
    apTaxTran.TranType = ((PXSelectBase<PX.Objects.AP.APInvoice>) apGraph.Document).Current.DocType;
    apTaxTran.RefNbr = ((PXSelectBase<PX.Objects.AP.APInvoice>) apGraph.Document).Current.RefNbr;
    apTaxTran.TaxID = newTax.TaxID;
    apTaxTran.TaxRate = newTax.TaxRate;
    apTaxTran.CuryTaxableAmt = newTax.CuryTaxableAmt;
    apTaxTran.CuryTaxAmt = newTax.CuryTaxAmt;
    apTaxTran.CuryTaxAmtSumm = newTax.CuryTaxAmt;
    apTaxTran.NonDeductibleTaxRate = newTax.NonDeductibleTaxRate;
    apTaxTran.CuryExpenseAmt = newTax.CuryExpenseAmt;
    apTaxTran.IsTaxInclusive = newTax.IsTaxInclusive;
    return ((PXSelectBase<APTaxTran>) apGraph.Taxes).Insert(apTaxTran);
  }

  protected virtual void TrackLandedCostSplits(IEnumerable<POLandedCostSplit> landedCostSplits)
  {
    IEqualityComparer<object> comparer = PXCacheEx.GetComparer(((PXSelectBase) this.Splits).Cache);
    Dictionary<POLandedCostSplit, POLandedCostSplit> dictionary = GraphHelper.RowCast<POLandedCostSplit>((IEnumerable) ((PXSelectBase<POLandedCostSplit>) this.Splits).Select(Array.Empty<object>())).ToDictionary<POLandedCostSplit, POLandedCostSplit>((Func<POLandedCostSplit, POLandedCostSplit>) (r => r), (IEqualityComparer<POLandedCostSplit>) comparer);
    foreach (POLandedCostSplit landedCostSplit in landedCostSplits)
    {
      POLandedCostSplit poLandedCostSplit;
      dictionary.TryGetValue(landedCostSplit, out poLandedCostSplit);
      Decimal num = ((PXGraph) this).FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo().CuryConvCury(landedCostSplit.LineAmt.GetValueOrDefault());
      if (poLandedCostSplit != null)
      {
        POLandedCostSplit copy = (POLandedCostSplit) ((PXSelectBase) this.Splits).Cache.CreateCopy((object) poLandedCostSplit);
        copy.LineAmt = landedCostSplit.LineAmt;
        copy.CuryLineAmt = new Decimal?(num);
        ((PXSelectBase<POLandedCostSplit>) this.Splits).Update(copy);
        dictionary.Remove(landedCostSplit);
      }
      else
      {
        landedCostSplit.CuryLineAmt = new Decimal?(num);
        ((PXSelectBase<POLandedCostSplit>) this.Splits).Insert(landedCostSplit);
      }
    }
    foreach (POLandedCostSplit key in dictionary.Keys)
      ((PXSelectBase<POLandedCostSplit>) this.Splits).Delete(key);
  }

  public class MultiCurrency : MultiCurrencyGraph<POLandedCostDocEntry, POLandedCostDoc>
  {
    protected override string Module => "PO";

    protected override MultiCurrencyGraph<POLandedCostDocEntry, POLandedCostDoc>.CurySourceMapping GetCurySourceMapping()
    {
      return new MultiCurrencyGraph<POLandedCostDocEntry, POLandedCostDoc>.CurySourceMapping(typeof (PX.Objects.AP.Vendor));
    }

    protected override MultiCurrencyGraph<POLandedCostDocEntry, POLandedCostDoc>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<POLandedCostDocEntry, POLandedCostDoc>.DocumentMapping(typeof (POLandedCostDoc))
      {
        DocumentDate = typeof (POLandedCostDoc.docDate),
        BAccountID = typeof (POLandedCostDoc.vendorID)
      };
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[6]
      {
        (PXSelectBase) this.Base.Document,
        (PXSelectBase) this.Base.ReceiptLines,
        (PXSelectBase) this.Base.Details,
        (PXSelectBase) this.Base.Splits,
        (PXSelectBase) this.Base.Tax_Rows,
        (PXSelectBase) this.Base.Taxes
      };
    }

    protected override bool AllowOverrideCury()
    {
      POLandedCostDoc current = ((PXSelectBase<POLandedCostDoc>) this.Base.Document).Current;
      return current != null && base.AllowOverrideCury() && !current.Released.GetValueOrDefault();
    }

    protected override bool AllowOverrideRate(PXCache sender, PX.Objects.CM.Extensions.CurrencyInfo info, CurySource source)
    {
      POLandedCostDoc current = ((PXSelectBase<POLandedCostDoc>) this.Base.Document).Current;
      if (current == null)
        return false;
      bool flag = current.VendorID.HasValue && current.VendorLocationID.HasValue;
      return base.AllowOverrideRate(sender, info, source) & flag && !current.Released.GetValueOrDefault();
    }

    protected override void _(PX.Data.Events.RowSelected<PX.Objects.Extensions.MultiCurrency.Document> e)
    {
      base._(e);
      bool flag = PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();
      PXUIFieldAttribute.SetVisible<PX.Objects.Extensions.MultiCurrency.Document.curyID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.Extensions.MultiCurrency.Document>>) e).Cache, (object) e.Row, flag);
    }

    protected override void _(
      PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.bAccountID> e)
    {
      if (!((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.bAccountID>>) e).ExternalCall && e.Row?.CuryID != null || ((PXGraph) this.Base).IsCopyPasteContext)
        return;
      this.SourceFieldUpdated<PX.Objects.Extensions.MultiCurrency.Document.curyInfoID, PX.Objects.Extensions.MultiCurrency.Document.curyID, PX.Objects.Extensions.MultiCurrency.Document.documentDate>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.bAccountID>>) e).Cache, (IBqlTable) e.Row);
    }

    protected override void _(PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.branchID> e)
    {
      PX.Objects.Extensions.MultiCurrency.Document row = e.Row;
      bool resetCuryID = (row != null ? (!row.BAccountID.HasValue ? 1 : 0) : 1) != 0 && !((PXGraph) this.Base).IsCopyPasteContext && (((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.branchID>>) e).ExternalCall || e.Row?.CuryID == null);
      this.SourceFieldUpdated<PX.Objects.Extensions.MultiCurrency.Document.curyInfoID, PX.Objects.Extensions.MultiCurrency.Document.curyID, PX.Objects.Extensions.MultiCurrency.Document.documentDate>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.branchID>>) e).Cache, (IBqlTable) e.Row, resetCuryID);
    }
  }
}
