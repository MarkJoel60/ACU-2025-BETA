// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.AddInvoiceExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.SO.DAC.Projections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class AddInvoiceExt : PXGraphExtension<
#nullable disable
SOOrderEntry>
{
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<InvoiceSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  InvoiceSplit.customerID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.SO.SOOrder.customerID, IBqlInt>.FromCurrent.NoDefault>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  InvoiceSplit.aRlineType, IBqlString>.IsEqual<
  #nullable disable
  InvoiceSplit.sOlineType>>>, And<BqlOperand<
  #nullable enable
  InvoiceSplit.sOOrderNbr, IBqlString>.IsNotNull>>, 
  #nullable disable
  And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current2<
  #nullable enable
  PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.aRRefNbr>, 
  #nullable disable
  IsNotNull>>>, Or<BqlOperand<Current2<
  #nullable enable
  PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.orderNbr>, IBqlString>.IsNotNull>>>.Or<
  #nullable disable
  BqlOperand<Current2<
  #nullable enable
  PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.inventoryID>, IBqlInt>.IsNotNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  InvoiceSplit.aRDocType, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.aRDocType, IBqlString>.FromCurrent.NoDefault>>>>>.Or<
  #nullable disable
  BqlOperand<Current2<
  #nullable enable
  PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.aRDocType>, IBqlString>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  InvoiceSplit.aRRefNbr, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.aRRefNbr, IBqlString>.FromCurrent.NoDefault>>>>>.Or<
  #nullable disable
  BqlOperand<Current2<
  #nullable enable
  PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.aRRefNbr>, IBqlString>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  InvoiceSplit.sOOrderType, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.orderType, IBqlString>.FromCurrent.NoDefault>>>>>.Or<
  #nullable disable
  BqlOperand<Current2<
  #nullable enable
  PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.orderType>, IBqlString>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  InvoiceSplit.sOOrderNbr, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.orderNbr, IBqlString>.FromCurrent.NoDefault>>>>>.Or<
  #nullable disable
  BqlOperand<Current2<
  #nullable enable
  PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.orderNbr>, IBqlString>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  InvoiceSplit.inventoryID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.inventoryID, IBqlInt>.FromCurrent.NoDefault>>>>>.Or<
  #nullable disable
  BqlOperand<Current2<
  #nullable enable
  PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.inventoryID>, IBqlInt>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  InvoiceSplit.lotSerialNbr, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.lotSerialNbr, IBqlString>.FromCurrent.NoDefault>>>>>.Or<
  #nullable disable
  BqlOperand<Current2<
  #nullable enable
  PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.lotSerialNbr>, IBqlString>.IsNull>>>, 
  #nullable disable
  And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  InvoiceSplit.aRTranDate, 
  #nullable disable
  GreaterEqual<BqlField<
  #nullable enable
  PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.startDate, IBqlDateTime>.FromCurrent.NoDefault>>>>>.Or<
  #nullable disable
  BqlOperand<Current2<
  #nullable enable
  PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.startDate>, IBqlDateTime>.IsNull>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  InvoiceSplit.aRTranDate, 
  #nullable disable
  LessEqual<BqlField<
  #nullable enable
  PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.endDate, IBqlDateTime>.FromCurrent.NoDefault>>>>>.Or<
  #nullable disable
  BqlOperand<Current2<
  #nullable enable
  PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.endDate>, IBqlDateTime>.IsNull>>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  InvoiceSplit.aRTranDate, IBqlDateTime>.Desc, 
  #nullable disable
  BqlField<
  #nullable enable
  InvoiceSplit.inventoryID, IBqlInt>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  InvoiceSplit.subItemID, IBqlInt>.Asc>>, 
  #nullable disable
  InvoiceSplit>.View invoiceSplits;
  public PXFilter<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter> AddInvoiceFilter;
  public PXAction<PX.Objects.SO.SOOrder> addInvoiceOK;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster = PXCacheEx.AdjustUI(((PXSelectBase) this.invoiceSplits).Cache, (object) null);
    attributeAdjuster.ForAllFields((Action<PXUIFieldAttribute>) (a => a.Enabled = false));
    attributeAdjuster = PXCacheEx.AdjustUI(((PXSelectBase) this.invoiceSplits).Cache, (object) null);
    attributeAdjuster.For<InvoiceSplit.selected>((Action<PXUIFieldAttribute>) (a => a.Enabled = true)).SameFor<InvoiceSplit.qtyToReturn>();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable AddInvoice(PXAdapter adapter)
  {
    try
    {
      this.AddInvoiceProc();
    }
    finally
    {
      ((PXSelectBase) this.invoiceSplits).Cache.Clear();
      ((PXSelectBase) this.invoiceSplits).View.Clear();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable AddInvoiceOK(PXAdapter adapter)
  {
    ((PXSelectBase) this.invoiceSplits).View.Answer = (WebDialogResult) 1;
    return this.AddInvoice(adapter);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter, PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.startDate> e)
  {
    PX.Data.Events.FieldDefaulting<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter, PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.startDate> fieldDefaulting = e;
    DateTime? businessDate = ((PXGraph) this.Base).Accessinfo.BusinessDate;
    ref DateTime? local1 = ref businessDate;
    // ISSUE: variable of a boxed type
    __Boxed<DateTime?> local2 = (ValueType) (local1.HasValue ? new DateTime?(local1.GetValueOrDefault().AddDays(-90.0)) : new DateTime?());
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter, PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.startDate>, PX.Objects.SO.DAC.Unbound.AddInvoiceFilter, object>) fieldDefaulting).NewValue = (object) local2;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter, PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.inventoryID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter, PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.inventoryID>>) e).Cache.SetValueExt<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.lotSerialNbr>((object) e.Row, (object) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter, PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.aRRefNbr> e)
  {
    if (e.Row.ARDocType == null || e.Row.ARRefNbr == null)
      return;
    PX.Objects.AR.ARRegister arRegister = PX.Objects.AR.ARRegister.PK.Find((PXGraph) this.Base, e.Row.ARDocType, e.Row.ARRefNbr);
    DateTime? docDate = (DateTime?) arRegister?.DocDate;
    DateTime? nullable = e.Row.StartDate;
    if ((docDate.HasValue & nullable.HasValue ? (docDate.GetValueOrDefault() < nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter, PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.aRRefNbr>>) e).Cache.SetValueExt<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.startDate>((object) e.Row, (object) null);
    nullable = (DateTime?) arRegister?.DocDate;
    DateTime? endDate = e.Row.EndDate;
    if ((nullable.HasValue & endDate.HasValue ? (nullable.GetValueOrDefault() > endDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter, PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.aRRefNbr>>) e).Cache.SetValueExt<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.endDate>((object) e.Row, (object) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter, PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.orderNbr> e)
  {
    if (e.Row.OrderType == null || e.Row.OrderNbr == null)
      return;
    DateTime? nullable = e.Row.StartDate;
    if (nullable.HasValue && PXResultset<PX.Objects.AR.ARTran>.op_Implicit(PXSelectBase<PX.Objects.AR.ARTran, PXViewOf<PX.Objects.AR.ARTran>.BasedOn<SelectFromBase<PX.Objects.AR.ARTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AR.ARRegister>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.tranType, Equal<PX.Objects.AR.ARRegister.docType>>>>>.And<BqlOperand<PX.Objects.AR.ARTran.refNbr, IBqlString>.IsEqual<PX.Objects.AR.ARRegister.refNbr>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.sOOrderType, Equal<BqlField<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.AR.ARTran.sOOrderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.orderNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.AR.ARRegister.docDate, IBqlDateTime>.IsLess<BqlField<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.startDate, IBqlDateTime>.FromCurrent>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, Array.Empty<object>())) != null)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter, PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.orderNbr>>) e).Cache.SetValueExt<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.startDate>((object) e.Row, (object) null);
    nullable = e.Row.EndDate;
    if (!nullable.HasValue || PXResultset<PX.Objects.AR.ARTran>.op_Implicit(PXSelectBase<PX.Objects.AR.ARTran, PXViewOf<PX.Objects.AR.ARTran>.BasedOn<SelectFromBase<PX.Objects.AR.ARTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AR.ARRegister>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.tranType, Equal<PX.Objects.AR.ARRegister.docType>>>>>.And<BqlOperand<PX.Objects.AR.ARTran.refNbr, IBqlString>.IsEqual<PX.Objects.AR.ARRegister.refNbr>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.sOOrderType, Equal<BqlField<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.AR.ARTran.sOOrderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.orderNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.AR.ARRegister.docDate, IBqlDateTime>.IsGreater<BqlField<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.endDate, IBqlDateTime>.FromCurrent>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, Array.Empty<object>())) == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter, PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.orderNbr>>) e).Cache.SetValueExt<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.endDate>((object) e.Row, (object) null);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter> e)
  {
    ((PXSelectBase) this.invoiceSplits).Cache.Clear();
    ((PXSelectBase) this.invoiceSplits).Cache.ClearQueryCache();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter> e)
  {
    if (e.Row == null)
      return;
    string lotSerClassId = !e.Row.InventoryID.HasValue ? (string) null : PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, e.Row.InventoryID)?.LotSerClassID;
    bool lotEnabled = lotSerClassId != null && INLotSerClass.PK.Find((PXGraph) this.Base, lotSerClassId)?.LotSerTrack != "N";
    PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter>>) e).Cache, (object) null).For<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter.lotSerialNbr>((Action<PXUIFieldAttribute>) (a => a.Enabled = lotEnabled));
    PXCacheEx.AdjustUI(((PXSelectBase) this.invoiceSplits).Cache, (object) null).For<InvoiceSplit.componentID>((Action<PXUIFieldAttribute>) (a => a.Visible = e.Row.Expand.GetValueOrDefault())).SameFor<InvoiceSplit.componentDesc>();
  }

  protected virtual void _(PX.Data.Events.RowSelecting<InvoiceSplit> e)
  {
    if (e.Row?.ARRefNbr == null)
      return;
    this.CalculateQtyAvail(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<InvoiceSplit, InvoiceSplit.qtyToReturn> e)
  {
    Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<InvoiceSplit, InvoiceSplit.qtyToReturn>, InvoiceSplit, object>) e).NewValue;
    Decimal? qtyAvailForReturn = e.Row.QtyAvailForReturn;
    if (newValue.GetValueOrDefault() > qtyAvailForReturn.GetValueOrDefault() & newValue.HasValue & qtyAvailForReturn.HasValue)
      throw new PXSetPropertyException("The quantity to return cannot exceed the quantity that is available for return.");
  }

  protected void _(
    PX.Data.Events.FieldUpdated<InvoiceSplit, InvoiceSplit.selected> e)
  {
    Decimal? nullable;
    if (e.Row.Selected.GetValueOrDefault())
    {
      nullable = e.Row.QtyToReturn;
      Decimal num1 = 0M;
      if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
      {
        nullable = e.Row.QtyAvailForReturn;
        Decimal num2 = 0M;
        if (nullable.GetValueOrDefault() > num2 & nullable.HasValue)
        {
          PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InvoiceSplit, InvoiceSplit.selected>>) e).Cache;
          InvoiceSplit row = e.Row;
          nullable = e.Row.QtyAvailForReturn;
          // ISSUE: variable of a boxed type
          __Boxed<Decimal> valueOrDefault = (ValueType) nullable.GetValueOrDefault();
          cache.SetValueExt<InvoiceSplit.qtyToReturn>((object) row, (object) valueOrDefault);
        }
      }
    }
    if (e.Row.Selected.GetValueOrDefault())
      return;
    nullable = e.Row.QtyToReturn;
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InvoiceSplit, InvoiceSplit.selected>>) e).Cache.SetValueExt<InvoiceSplit.qtyToReturn>((object) e.Row, (object) 0M);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<InvoiceSplit, InvoiceSplit.qtyToReturn> e)
  {
    Decimal? qtyToReturn1 = e.Row.QtyToReturn;
    Decimal num1 = 0M;
    if (qtyToReturn1.GetValueOrDefault() > num1 & qtyToReturn1.HasValue && !e.Row.Selected.GetValueOrDefault())
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InvoiceSplit, InvoiceSplit.qtyToReturn>>) e).Cache.SetValueExt<InvoiceSplit.selected>((object) e.Row, (object) true);
    Decimal? qtyToReturn2 = e.Row.QtyToReturn;
    Decimal num2 = 0M;
    if (!(qtyToReturn2.GetValueOrDefault() == num2 & qtyToReturn2.HasValue) || !e.Row.Selected.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<InvoiceSplit, InvoiceSplit.qtyToReturn>>) e).Cache.SetValueExt<InvoiceSplit.selected>((object) e.Row, (object) false);
  }

  protected virtual void _(PX.Data.Events.RowSelected<InvoiceSplit> e)
  {
    if (e.Row == null)
      return;
    PXSetPropertyException propertyException1 = (PXSetPropertyException) null;
    PXSetPropertyException propertyException2 = (PXSetPropertyException) null;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, e.Row.ComponentID ?? e.Row.InventoryID);
    Decimal? qtyAvailForReturn = e.Row.QtyAvailForReturn;
    Decimal num = 0M;
    if (qtyAvailForReturn.GetValueOrDefault() == num & qtyAvailForReturn.HasValue)
    {
      string str = inventoryItem?.InventoryCD?.TrimEnd();
      bool? nullable = e.Row.SerialIsOnHand;
      if (nullable.GetValueOrDefault())
      {
        propertyException2 = new PXSetPropertyException("The {0} item with the {1} serial number cannot be added because it has already been received.", (PXErrorLevel) 2, new object[2]
        {
          (object) str,
          (object) e.Row.LotSerialNbr
        });
      }
      else
      {
        nullable = e.Row.SerialIsAlreadyReceived;
        if (nullable.GetValueOrDefault())
          propertyException2 = new PXSetPropertyException(e.Row.SerialIsAlreadyReceivedRef == null ? "Serial Number '{1}' for item '{0}' is already received." : "Serial Number '{1}' for item '{0}' is already received in '{2}'.", (PXErrorLevel) 2, new object[3]
          {
            (object) str,
            (object) e.Row.LotSerialNbr,
            (object) e.Row.SerialIsAlreadyReceivedRef
          });
      }
      nullable = e.Row.Selected;
      if (nullable.GetValueOrDefault())
      {
        PXSetPropertyException propertyException3 = propertyException2;
        if (propertyException3 == null)
          propertyException3 = new PXSetPropertyException("The {0} item has been fully returned.", (PXErrorLevel) 2, new object[1]
          {
            (object) str
          });
        propertyException1 = propertyException3;
      }
    }
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InvoiceSplit>>) e).Cache.RaiseExceptionHandling<InvoiceSplit.selected>((object) e.Row, (object) true, (Exception) propertyException1);
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InvoiceSplit>>) e).Cache.RaiseExceptionHandling<InvoiceSplit.qtyAvailForReturn>((object) e.Row, (object) 0, (Exception) propertyException2);
    if (inventoryItem == null || !EnumerableExtensions.IsIn<string>(inventoryItem.ItemStatus, "IN", "DE"))
      return;
    PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<InvoiceSplit>>) e).Cache, (object) e.Row).For<InvoiceSplit.selected>((Action<PXUIFieldAttribute>) (a => a.Enabled = false)).SameFor<InvoiceSplit.qtyToReturn>();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<InvoiceSplit, InvoiceSplit.inventoryID> e)
  {
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<InvoiceSplit, InvoiceSplit.inventoryID>>) e).Cancel = true;
  }

  protected virtual void AddInvoiceProc()
  {
    ((PXSelectBase) this.Base.Transactions).Cache.ForceExceptionHandling = true;
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    if ((current != null && current.IsCreditMemoOrder.GetValueOrDefault() || current != null && current.IsRMAOrder.GetValueOrDefault() || current != null && current.IsMixedOrder.GetValueOrDefault()) && ((PXSelectBase) this.Base.Transactions).Cache.AllowInsert && ((PXSelectBase<InvoiceSplit>) this.invoiceSplits).AskExt() == 1)
    {
      foreach (InvoiceSplit split in GraphHelper.RowCast<InvoiceSplit>(((PXSelectBase) this.invoiceSplits).Cache.Cached).Where<InvoiceSplit>((Func<InvoiceSplit, bool>) (s => s.Selected.GetValueOrDefault())))
        this.AddInvoiceProc(split);
    }
    this.ClearInvoiceFilter();
  }

  protected virtual void AddInvoiceProc(InvoiceSplit split)
  {
    PX.Objects.SO.SOLine origLine = PX.Objects.SO.SOLine.PK.Find((PXGraph) this.Base, split.SOOrderType, split.SOOrderNbr, split.SOLineNbr);
    PX.Objects.AR.ARTran artran = PX.Objects.AR.ARTran.PK.Find((PXGraph) this.Base, split.ARDocType, split.ARRefNbr, split.ARLineNbr);
    PX.Objects.AR.ARRegister invoice = PX.Objects.AR.ARRegister.PK.Find((PXGraph) this.Base, split.ARDocType, split.ARRefNbr);
    PX.Objects.SO.SOLine soLine1 = this.FindExistingSOLine(split);
    if (soLine1 != null)
    {
      ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Current = soLine1;
    }
    else
    {
      INTran tran = split.INDocType == "~" ? (INTran) null : INTran.PK.Find((PXGraph) this.Base, split.INDocType, split.INRefNbr, split.INLineNbr);
      PX.Objects.SO.SOLine instance = (PX.Objects.SO.SOLine) ((PXSelectBase) this.Base.Transactions).Cache.CreateInstance();
      this.FillSOLine(instance, split);
      this.FillSOLine(instance, tran, origLine, artran, invoice);
      PX.Objects.SO.SOLine soLine2 = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Insert(instance);
      soLine2.Operation = "R";
      PX.Objects.SO.SOLine newline;
      soLine1 = newline = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soLine2);
      this.UpdateSOSalesPerTran(split);
      this.ClearSplits(newline);
    }
    PX.Objects.SO.SOLine copy1 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(soLine1);
    PX.Objects.SO.SOLine copy2 = this.IncreaseQty(split, artran, invoice, copy1);
    this.CopyDiscount(artran, invoice, copy2);
  }

  protected virtual void ClearInvoiceFilter()
  {
    if (((PXSelectBase<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter>) this.AddInvoiceFilter).Current == null)
      return;
    if (((PXGraph) this.Base).IsImport)
    {
      ((PXSelectBase<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter>) this.AddInvoiceFilter).Current.ARRefNbr = (string) null;
      ((PXSelectBase<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter>) this.AddInvoiceFilter).Current.InventoryID = new int?();
      ((PXSelectBase<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter>) this.AddInvoiceFilter).Current.LotSerialNbr = (string) null;
      ((PXSelectBase<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter>) this.AddInvoiceFilter).Current.OrderNbr = (string) null;
    }
    else
      ((PXSelectBase<PX.Objects.SO.DAC.Unbound.AddInvoiceFilter>) this.AddInvoiceFilter).Current = (PX.Objects.SO.DAC.Unbound.AddInvoiceFilter) null;
  }

  protected virtual PX.Objects.SO.SOLine FindExistingSOLine(InvoiceSplit split)
  {
    return PXResultset<PX.Objects.SO.SOLine>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLine, PXSelect<PX.Objects.SO.SOLine, Where<PX.Objects.SO.SOLine.orderType, Equal<Current<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOLine.orderNbr, Equal<Current<PX.Objects.SO.SOOrder.orderNbr>>, And<PX.Objects.SO.SOLine.origOrderType, Equal<Required<PX.Objects.SO.SOLine.origOrderType>>, And<PX.Objects.SO.SOLine.origOrderNbr, Equal<Required<PX.Objects.SO.SOLine.origOrderNbr>>, And<PX.Objects.SO.SOLine.origLineNbr, Equal<Required<PX.Objects.SO.SOLine.origLineNbr>>, And<PX.Objects.SO.SOLine.inventoryID, Equal<Required<PX.Objects.SO.SOLine.inventoryID>>, And<PX.Objects.SO.SOLine.invoiceType, Equal<Required<PX.Objects.SO.SOLine.invoiceType>>, And<PX.Objects.SO.SOLine.invoiceNbr, Equal<Required<PX.Objects.SO.SOLine.invoiceNbr>>, And<PX.Objects.SO.SOLine.invoiceLineNbr, Equal<Required<PX.Objects.SO.SOLine.invoiceLineNbr>>>>>>>>>>>>.Config>.Select((PXGraph) this.Base, new object[7]
    {
      (object) split.SOOrderType,
      (object) split.SOOrderNbr,
      (object) split.SOLineNbr,
      (object) (split.ComponentID ?? split.InventoryID),
      (object) split.ARDocType,
      (object) split.ARRefNbr,
      (object) split.ARLineNbr
    }));
  }

  protected virtual void FillSOLine(PX.Objects.SO.SOLine newline, InvoiceSplit split)
  {
    newline.InvoiceType = split.ARDocType;
    newline.InvoiceNbr = split.ARRefNbr;
    newline.InvoiceLineNbr = split.ARLineNbr;
    newline.OrigOrderType = split.SOOrderType;
    newline.OrigOrderNbr = split.SOOrderNbr;
    newline.OrigLineNbr = split.SOLineNbr;
    newline.SalesPersonID = split.SalesPersonID;
    newline.InventoryID = split.ComponentID ?? split.InventoryID;
    newline.TranDesc = split.TranDesc;
    newline.SubItemID = split.SubItemID;
    newline.SiteID = split.SiteID;
    newline.LotSerialNbr = split.LotSerialNbr;
    newline.UOM = split.UOM;
    if (!split.AutoCreateIssueLine.HasValue)
      return;
    newline.AutoCreateIssueLine = split.AutoCreateIssueLine;
  }

  protected virtual void FillSOLine(
    PX.Objects.SO.SOLine newline,
    INTran tran,
    PX.Objects.SO.SOLine origLine,
    PX.Objects.AR.ARTran artran,
    PX.Objects.AR.ARRegister invoice)
  {
    newline.BranchID = origLine.BranchID;
    newline.InvoiceDate = artran.TranDate;
    newline.OrigShipmentType = artran.SOShipmentType;
    newline.SalesAcctID = new int?();
    newline.SalesSubID = new int?();
    newline.TaxCategoryID = origLine.TaxCategoryID;
    newline.Commissionable = artran.Commissionable;
    newline.IsSpecialOrder = origLine.IsSpecialOrder;
    newline.CostCenterID = origLine.CostCenterID;
    newline.ManualPrice = new bool?(true);
    newline.ManualDisc = new bool?(true);
    PX.Objects.SO.SOLine soLine = newline;
    int? inventoryId1 = newline.InventoryID;
    int? inventoryId2 = artran.InventoryID;
    string str = inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue ? artran.UOM : newline.UOM;
    soLine.InvoiceUOM = str;
    newline.CuryInfoID = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.CuryInfoID;
    if (artran != null && artran.AvalaraCustomerUsageType != null)
      newline.AvalaraCustomerUsageType = artran.AvalaraCustomerUsageType;
    int? inventoryId3;
    int? inventoryId4;
    if (origLine.LineType == "MI" || origLine.LineType == "GN" || tran == null)
    {
      inventoryId3 = newline.InventoryID;
      int? inventoryId5 = artran.InventoryID;
      if (inventoryId3.GetValueOrDefault() == inventoryId5.GetValueOrDefault() & inventoryId3.HasValue == inventoryId5.HasValue)
      {
        Decimal? qty1 = artran.Qty;
        Decimal num = 0M;
        Decimal? nullable;
        if (!(qty1.GetValueOrDefault() > num & qty1.HasValue))
        {
          nullable = artran.TranCost;
        }
        else
        {
          Decimal? tranCost = artran.TranCost;
          Decimal? qty2 = artran.Qty;
          nullable = tranCost.HasValue & qty2.HasValue ? new Decimal?(tranCost.GetValueOrDefault() / qty2.GetValueOrDefault()) : new Decimal?();
        }
        Decimal valueOrDefault = nullable.GetValueOrDefault();
        newline.UnitCost = new Decimal?(PXDBPriceCostAttribute.Round(valueOrDefault));
      }
    }
    else
    {
      inventoryId4 = newline.InventoryID;
      int? inventoryId6 = tran.InventoryID;
      if (inventoryId4.GetValueOrDefault() == inventoryId6.GetValueOrDefault() & inventoryId4.HasValue == inventoryId6.HasValue)
      {
        Decimal? qty3 = tran.Qty;
        Decimal num = 0M;
        Decimal? nullable;
        if (!(qty3.GetValueOrDefault() > num & qty3.HasValue))
        {
          nullable = tran.TranCost;
        }
        else
        {
          Decimal? tranCost = tran.TranCost;
          Decimal? qty4 = tran.Qty;
          nullable = tranCost.HasValue & qty4.HasValue ? new Decimal?(tranCost.GetValueOrDefault() / qty4.GetValueOrDefault()) : new Decimal?();
        }
        Decimal valueOrDefault = nullable.GetValueOrDefault();
        newline.UnitCost = new Decimal?(string.Equals(tran.UOM, newline.UOM, StringComparison.OrdinalIgnoreCase) ? PXDBPriceCostAttribute.Round(valueOrDefault) : INUnitAttribute.ConvertFromTo<PX.Objects.SO.SOLine.inventoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) newline, newline.UOM, tran.UOM, valueOrDefault, INPrecision.UNITCOST));
      }
    }
    newline.DiscPctDR = artran.DiscPctDR;
    Decimal? nullable1 = artran.CuryUnitPriceDR;
    if (nullable1.HasValue && invoice != null)
    {
      if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.CuryID == invoice.CuryID)
      {
        newline.CuryUnitPriceDR = artran.CuryUnitPriceDR;
      }
      else
      {
        Decimal baseval = 0M;
        PXCache cach = ((PXGraph) this.Base).Caches[typeof (PX.Objects.AR.ARTran)];
        PX.Objects.AR.ARTran row = artran;
        nullable1 = artran.CuryUnitPriceDR;
        Decimal valueOrDefault = nullable1.GetValueOrDefault();
        ref Decimal local = ref baseval;
        PXDBCurrencyAttribute.CuryConvBase(cach, (object) row, valueOrDefault, out local, true);
        Decimal curyval = 0M;
        PXDBCurrencyAttribute.CuryConvCury(((PXSelectBase) this.Base.Transactions).Cache, (object) newline, baseval, out curyval, CommonSetupDecPl.PrcCst);
        newline.CuryUnitPriceDR = new Decimal?(curyval);
      }
    }
    newline.DRTermStartDate = artran.DRTermStartDate;
    newline.DRTermEndDate = artran.DRTermEndDate;
    newline.ReasonCode = origLine.ReasonCode;
    newline.TaskID = artran.TaskID;
    newline.CostCodeID = artran.CostCodeID;
    if (!string.IsNullOrEmpty(artran.DeferredCode))
    {
      PXResultset<DRSetup>.op_Implicit(((PXSelectBase<DRSetup>) new PXSelect<DRSetup>((PXGraph) this.Base)).Select(Array.Empty<object>()));
      DRSchedule drSchedule;
      if (PXAccess.FeatureInstalled<FeaturesSet.aSC606>())
        drSchedule = PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelectReadonly<DRSchedule, Where<DRSchedule.module, Equal<BatchModule.moduleAR>, And<DRSchedule.docType, Equal<Required<PX.Objects.AR.ARTran.tranType>>, And<DRSchedule.refNbr, Equal<Required<PX.Objects.AR.ARTran.refNbr>>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
        {
          (object) artran.TranType,
          (object) artran.RefNbr
        }));
      else
        drSchedule = PXResultset<DRSchedule>.op_Implicit(PXSelectBase<DRSchedule, PXSelectReadonly<DRSchedule, Where<DRSchedule.module, Equal<BatchModule.moduleAR>, And<DRSchedule.docType, Equal<Required<PX.Objects.AR.ARTran.tranType>>, And<DRSchedule.refNbr, Equal<Required<PX.Objects.AR.ARTran.refNbr>>, And<DRSchedule.lineNbr, Equal<Required<PX.Objects.AR.ARTran.lineNbr>>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
        {
          (object) artran.TranType,
          (object) artran.RefNbr,
          (object) artran.LineNbr
        }));
      if (drSchedule != null)
        newline.DefScheduleID = drSchedule.ScheduleID;
    }
    PXCache cache1 = ((PXSelectBase) this.Base.Transactions).Cache;
    PX.Objects.SO.SOLine row1 = newline;
    nullable1 = newline.UnitCost;
    Decimal baseval1 = nullable1.Value;
    Decimal num1;
    ref Decimal local1 = ref num1;
    int prcCst = CommonSetupDecPl.PrcCst;
    PXDBCurrencyAttribute.CuryConvCury(cache1, (object) row1, baseval1, out local1, prcCst);
    newline.CuryUnitCost = new Decimal?(num1);
    if (invoice != null)
    {
      inventoryId3 = newline.InventoryID;
      inventoryId4 = artran.InventoryID;
      if (inventoryId3.GetValueOrDefault() == inventoryId4.GetValueOrDefault() & inventoryId3.HasValue == inventoryId4.HasValue)
      {
        if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.CuryID == invoice.CuryID)
        {
          Decimal num2;
          if (!string.Equals(artran.UOM, newline.UOM, StringComparison.OrdinalIgnoreCase))
          {
            PXCache cache2 = ((PXSelectBase) this.Base.Transactions).Cache;
            PX.Objects.SO.SOLine Row = newline;
            string uom1 = newline.UOM;
            string uom2 = artran.UOM;
            nullable1 = artran.CuryUnitPrice;
            Decimal num3 = nullable1.Value;
            num2 = INUnitAttribute.ConvertFromTo<PX.Objects.SO.SOLine.inventoryID>(cache2, (object) Row, uom1, uom2, num3, INPrecision.UNITCOST);
          }
          else
          {
            nullable1 = artran.CuryUnitPrice;
            num2 = PXDBPriceCostAttribute.Round(nullable1.Value);
          }
          Decimal baseval2 = num2;
          Decimal curyval;
          PXDBCurrencyAttribute.CuryConvBase(((PXSelectBase) this.Base.Transactions).Cache, (object) newline, baseval2, out curyval, CommonSetupDecPl.PrcCst);
          newline.CuryUnitPrice = new Decimal?(baseval2);
          newline.UnitPrice = new Decimal?(curyval);
        }
        else
        {
          Decimal num4;
          if (!string.Equals(artran.UOM, newline.UOM, StringComparison.OrdinalIgnoreCase))
          {
            PXCache cache3 = ((PXSelectBase) this.Base.Transactions).Cache;
            PX.Objects.SO.SOLine Row = newline;
            string uom3 = newline.UOM;
            string uom4 = artran.UOM;
            nullable1 = artran.UnitPrice;
            Decimal num5 = nullable1.Value;
            num4 = INUnitAttribute.ConvertFromTo<PX.Objects.SO.SOLine.inventoryID>(cache3, (object) Row, uom3, uom4, num5, INPrecision.UNITCOST);
          }
          else
          {
            nullable1 = artran.UnitPrice;
            num4 = PXDBPriceCostAttribute.Round(nullable1.Value);
          }
          Decimal baseval3 = num4;
          Decimal curyval;
          PXDBCurrencyAttribute.CuryConvCury(((PXSelectBase) this.Base.Transactions).Cache, (object) newline, baseval3, out curyval, CommonSetupDecPl.PrcCst);
          newline.CuryUnitPrice = new Decimal?(curyval);
          newline.UnitPrice = new Decimal?(baseval3);
        }
      }
    }
    newline.SkipLineDiscounts = artran.SkipLineDiscounts;
  }

  protected virtual void ClearSplits(PX.Objects.SO.SOLine newline)
  {
    this.Base.LineSplittingExt.RaiseRowDeleted(newline);
  }

  protected virtual void UpdateSOSalesPerTran(InvoiceSplit split)
  {
    SOSalesPerTran soSalesPerTran = SOSalesPerTran.PK.Find((PXGraph) this.Base, split.SOOrderType, split.SOOrderNbr, split.SalesPersonID);
    if (((PXSelectBase<SOSalesPerTran>) this.Base.SalesPerTran).Current == null || !((PXSelectBase) this.Base.SalesPerTran).Cache.ObjectsEqual<SOSalesPerTran.salespersonID>((object) soSalesPerTran, (object) ((PXSelectBase<SOSalesPerTran>) this.Base.SalesPerTran).Current))
      return;
    SOSalesPerTran copy = PXCache<SOSalesPerTran>.CreateCopy(((PXSelectBase<SOSalesPerTran>) this.Base.SalesPerTran).Current);
    ((PXSelectBase) this.Base.SalesPerTran).Cache.SetValueExt<SOSalesPerTran.commnPct>((object) ((PXSelectBase<SOSalesPerTran>) this.Base.SalesPerTran).Current, (object) soSalesPerTran.CommnPct);
    ((PXSelectBase) this.Base.SalesPerTran).Cache.RaiseRowUpdated((object) ((PXSelectBase<SOSalesPerTran>) this.Base.SalesPerTran).Current, (object) copy);
  }

  protected virtual PX.Objects.SO.SOLine IncreaseQty(
    InvoiceSplit split,
    PX.Objects.AR.ARTran artran,
    PX.Objects.AR.ARRegister invoice,
    PX.Objects.SO.SOLine soline)
  {
    INTranSplit inTranSplit = split.INDocType == "~" ? (INTranSplit) null : INTranSplit.PK.Find((PXGraph) this.Base, split.INDocType, split.INRefNbr, split.INLineNbr, split.INSplitLineNbr);
    bool flag = inTranSplit != null && (this.Base.LineSplittingExt.IsLSEntryEnabled || this.Base.LineSplittingAllocatedExt.IsAllocationEntryEnabled) && (!string.IsNullOrEmpty(split.LotSerialNbr) || this.Base.LineSplittingExt.IsLocationEnabled);
    Decimal? nullable1;
    if (flag)
    {
      nullable1 = split.QtyToReturn;
      Decimal num = 0M;
      if (nullable1.GetValueOrDefault() == num & nullable1.HasValue && INLotSerClass.PK.Find((PXGraph) this.Base, PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, soline.InventoryID)?.LotSerClassID)?.LotSerTrack == "S")
        flag = false;
    }
    if (!flag)
    {
      if (split.UOM == soline.UOM)
      {
        PX.Objects.SO.SOLine soLine = soline;
        nullable1 = soLine.Qty;
        short? lineSign = soline.LineSign;
        Decimal? nullable2 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable3 = split.QtyToReturn;
        Decimal? nullable4 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable3.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable5;
        if (!(nullable1.HasValue & nullable4.HasValue))
        {
          nullable3 = new Decimal?();
          nullable5 = nullable3;
        }
        else
          nullable5 = new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault());
        soLine.Qty = nullable5;
      }
      else
      {
        Decimal num1 = INUnitAttribute.ConvertToBase(((PXSelectBase) this.invoiceSplits).Cache, soline.InventoryID, split.UOM, split.QtyToReturn.GetValueOrDefault(), INPrecision.NOROUND);
        PX.Objects.SO.SOLine soLine = soline;
        Decimal? qty = soLine.Qty;
        short? lineSign = soline.LineSign;
        Decimal? nullable6 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
        Decimal num2 = INUnitAttribute.ConvertFromBase<PX.Objects.SO.SOLine.inventoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) soline, soline.UOM, num1, INPrecision.QUANTITY);
        nullable1 = nullable6.HasValue ? new Decimal?(nullable6.GetValueOrDefault() * num2) : new Decimal?();
        Decimal? nullable7;
        if (!(qty.HasValue & nullable1.HasValue))
        {
          nullable6 = new Decimal?();
          nullable7 = nullable6;
        }
        else
          nullable7 = new Decimal?(qty.GetValueOrDefault() + nullable1.GetValueOrDefault());
        soLine.Qty = nullable7;
      }
    }
    else if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.CuryID == invoice.CuryID)
    {
      PXCache cache = ((PXSelectBase) this.Base.Transactions).Cache;
      PX.Objects.SO.SOLine row = soline;
      nullable1 = artran.CuryTranAmt;
      Decimal curyval = nullable1.Value;
      Decimal num;
      ref Decimal local = ref num;
      PXDBCurrencyAttribute.CuryConvBase<PX.Objects.SO.SOLine.curyInfoID>(cache, (object) row, curyval, out local);
      soline.CuryLineAmt = artran.CuryTranAmt;
      soline.LineAmt = new Decimal?(num);
    }
    else
    {
      PXCache cache = ((PXSelectBase) this.Base.Transactions).Cache;
      PX.Objects.SO.SOLine row = soline;
      nullable1 = artran.TranAmt;
      Decimal baseval = nullable1.Value;
      Decimal num;
      ref Decimal local = ref num;
      PXDBCurrencyAttribute.CuryConvCury<PX.Objects.SO.SOLine.curyInfoID>(cache, (object) row, baseval, out local);
      soline.CuryLineAmt = new Decimal?(num);
      soline.LineAmt = artran.TranAmt;
    }
    try
    {
      soline = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soline);
    }
    catch (PXSetPropertyException ex)
    {
    }
    if (flag)
    {
      PX.Objects.SO.SOLineSplit soLineSplit1 = new PX.Objects.SO.SOLineSplit();
      soLineSplit1.SubItemID = inTranSplit.SubItemID;
      if (this.Base.LineSplittingExt.IsLocationEnabled)
        soLineSplit1.LocationID = inTranSplit.LocationID;
      soLineSplit1.LotSerialNbr = inTranSplit.LotSerialNbr;
      soLineSplit1.ExpireDate = inTranSplit.ExpireDate;
      soLineSplit1.UOM = inTranSplit.UOM;
      PX.Objects.SO.SOLineSplit soLineSplit2 = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) this.Base.splits).Insert(soLineSplit1);
      soLineSplit2.Qty = split.QtyToReturn;
      PX.Objects.SO.SOLineSplit soLineSplit3 = ((PXSelectBase<PX.Objects.SO.SOLineSplit>) this.Base.splits).Update(soLineSplit2);
      if (!string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PX.Objects.SO.SOLineSplit.qty>(((PXSelectBase) this.Base.splits).Cache, (object) soLineSplit3)))
      {
        soLineSplit3.Qty = new Decimal?(0M);
        ((PXSelectBase<PX.Objects.SO.SOLineSplit>) this.Base.splits).Update(soLineSplit3);
      }
    }
    return soline;
  }

  protected virtual PX.Objects.SO.SOLine CopyDiscount(
    PX.Objects.AR.ARTran artran,
    PX.Objects.AR.ARRegister invoice,
    PX.Objects.SO.SOLine copy)
  {
    Decimal? nullable1;
    Decimal num1;
    Decimal num2;
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.CuryID == invoice.CuryID)
    {
      PXCache cache = ((PXSelectBase) this.Base.Transactions).Cache;
      PX.Objects.SO.SOLine row = copy;
      nullable1 = artran.CuryDiscAmt;
      Decimal curyval = nullable1.Value;
      ref Decimal local = ref num1;
      PXDBCurrencyAttribute.CuryConvBase<PX.Objects.SO.SOLine.curyInfoID>(cache, (object) row, curyval, out local);
      nullable1 = artran.CuryDiscAmt;
      num2 = nullable1.Value;
    }
    else
    {
      PXCache cache = ((PXSelectBase) this.Base.Transactions).Cache;
      PX.Objects.SO.SOLine row = copy;
      nullable1 = artran.DiscAmt;
      Decimal baseval = nullable1.Value;
      ref Decimal local = ref num2;
      PXDBCurrencyAttribute.CuryConvCury<PX.Objects.SO.SOLine.curyInfoID>(cache, (object) row, baseval, out local);
      nullable1 = artran.DiscAmt;
      num1 = nullable1.Value;
    }
    nullable1 = artran.Qty;
    Decimal? nullable2 = copy.OrderQty;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) || artran.UOM != copy.UOM)
    {
      nullable2 = artran.Qty;
      Decimal num3 = 0M;
      if (!(nullable2.GetValueOrDefault() == num3 & nullable2.HasValue))
      {
        nullable2 = artran.BaseQty;
        Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
        nullable2 = copy.BaseOrderQty;
        Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
        if (valueOrDefault1 == 0M)
        {
          PXCache<PX.Objects.AR.ARTran> sender1 = GraphHelper.Caches<PX.Objects.AR.ARTran>((PXGraph) this.Base);
          int? inventoryId1 = artran.InventoryID;
          string uom1 = artran.UOM;
          nullable2 = artran.Qty;
          Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
          valueOrDefault1 = INUnitAttribute.ConvertToBase((PXCache) sender1, inventoryId1, uom1, valueOrDefault3, INPrecision.NOROUND);
          PXCache<PX.Objects.SO.SOLine> sender2 = GraphHelper.Caches<PX.Objects.SO.SOLine>((PXGraph) this.Base);
          int? inventoryId2 = copy.InventoryID;
          string uom2 = copy.UOM;
          nullable2 = copy.OrderQty;
          Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
          valueOrDefault2 = INUnitAttribute.ConvertToBase((PXCache) sender2, inventoryId2, uom2, valueOrDefault4, INPrecision.NOROUND);
        }
        copy.CuryDiscAmt = new Decimal?(num2 / valueOrDefault1 * valueOrDefault2);
        copy.DiscAmt = new Decimal?(PXDBCurrencyAttribute.BaseRound((PXGraph) this.Base, num1 / valueOrDefault1 * valueOrDefault2));
        goto label_9;
      }
    }
    copy.CuryDiscAmt = new Decimal?(num2);
    copy.DiscAmt = new Decimal?(num1);
label_9:
    copy.DiscPct = artran.DiscPct;
    copy.FreezeManualDisc = new bool?(true);
    try
    {
      copy = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(copy);
    }
    catch (PXSetPropertyException ex)
    {
    }
    ushort[] discountsAppliedToLine = artran.DiscountsAppliedToLine;
    if ((discountsAppliedToLine != null ? (((IEnumerable<ushort>) discountsAppliedToLine).Any<ushort>() ? 1 : 0) : 0) != 0)
      ((PXSelectBase) this.Base.Transactions).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLine.invoiceNbr>((object) copy, (object) copy.InvoiceNbr, (Exception) new PXSetPropertyException("The group or document discounts from the originating {0} invoice are not inherited by the return order. Change the discount in the return order manually if needed.", (PXErrorLevel) 3, new object[1]
      {
        (object) copy.InvoiceNbr
      }));
    return copy;
  }

  protected virtual void CalculateQtyAvail(InvoiceSplit split)
  {
    if (split.QtyAvailForReturn.HasValue && split.QtyReturned.HasValue)
      return;
    int? inventoryID = split.ComponentID ?? split.InventoryID;
    SOBaseItemAvailabilityExtension<SOOrderEntry, PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit>.ReturnedQtyResult returnedQtyResult = this.Base.ItemAvailabilityExt.MemoCheckQty(inventoryID, split.ARDocType, split.ARRefNbr, split.ARLineNbr, split.SOOrderType, split.SOOrderNbr, split.SOLineNbr, split);
    Decimal valueOrDefault = (returnedQtyResult.Success ? returnedQtyResult.QtyAvailForReturn : new Decimal?(0M)).GetValueOrDefault();
    if (valueOrDefault > 0M)
    {
      this.CalculateQtyAvail(split, ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current, inventoryID, valueOrDefault);
    }
    else
    {
      split.QtyAvailForReturn = new Decimal?(0M);
      split.QtyReturned = split.Qty;
    }
  }

  protected virtual void CalculateQtyAvail(
    InvoiceSplit split,
    PX.Objects.SO.SOOrder order,
    int? inventoryID,
    Decimal qtyAvailForReturn)
  {
    if (!string.IsNullOrEmpty(split.LotSerialNbr) && order != null)
    {
      PX.Objects.SO.SOLine soLine = new PX.Objects.SO.SOLine()
      {
        OrderType = order.OrderType,
        OrderNbr = order.OrderNbr,
        LineNbr = new int?(-1)
      };
      this.FillSOLine(soLine, split);
      qtyAvailForReturn = Math.Min(this.Base.ItemAvailabilityExt.MemoCheck(soLine, PX.Objects.SO.SOLineSplit.FromSOLine(soLine), false, false).qtyAvailForReturn, qtyAvailForReturn);
    }
    Decimal val1 = INUnitAttribute.ConvertFromBase(((PXSelectBase) this.Base.Transactions).Cache, inventoryID, split.UOM, qtyAvailForReturn, INPrecision.QUANTITY);
    split.QtyAvailForReturn = new Decimal?(Math.Min(val1, split.Qty.Value));
    InvoiceSplit invoiceSplit = split;
    Decimal? qty = split.Qty;
    Decimal? qtyAvailForReturn1 = split.QtyAvailForReturn;
    Decimal? nullable1 = qty.HasValue & qtyAvailForReturn1.HasValue ? new Decimal?(qty.GetValueOrDefault() - qtyAvailForReturn1.GetValueOrDefault()) : new Decimal?();
    invoiceSplit.QtyReturned = nullable1;
    if (string.IsNullOrEmpty(split.LotSerialNbr))
      return;
    INLotSerClass inLotSerClass = INLotSerClass.PK.Find((PXGraph) this.Base, PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, inventoryID)?.LotSerClassID);
    Decimal? nullable2;
    if (inLotSerClass.LotSerTrack == "S")
    {
      nullable2 = split.QtyAvailForReturn;
      Decimal num = (Decimal) 1;
      if (nullable2.GetValueOrDefault() < num & nullable2.HasValue)
        split.QtyAvailForReturn = new Decimal?(0M);
    }
    if (!(inLotSerClass.LotSerTrack == "S"))
      return;
    nullable2 = split.QtyAvailForReturn;
    Decimal num1 = (Decimal) 1;
    if (!(nullable2.GetValueOrDefault() == num1 & nullable2.HasValue))
      return;
    INItemLotSerial inItemLotSerial1 = INItemLotSerial.PK.Find((PXGraph) this.Base, inventoryID, split.LotSerialNbr);
    PXCache<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial> pxCache = GraphHelper.Caches<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial>((PXGraph) this.Base);
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial itemLotSerial1 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial();
    itemLotSerial1.InventoryID = inventoryID;
    itemLotSerial1.LotSerialNbr = split.LotSerialNbr;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial itemLotSerial2 = pxCache.Locate(itemLotSerial1);
    INItemLotSerial inItemLotSerial2 = inItemLotSerial1;
    nullable2 = inItemLotSerial2.QtyAvail;
    Decimal valueOrDefault1 = ((Decimal?) itemLotSerial2?.QtyAvail).GetValueOrDefault();
    inItemLotSerial2.QtyAvail = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
    INItemLotSerial inItemLotSerial3 = inItemLotSerial1;
    nullable2 = inItemLotSerial3.QtyHardAvail;
    Decimal valueOrDefault2 = ((Decimal?) itemLotSerial2?.QtyHardAvail).GetValueOrDefault();
    inItemLotSerial3.QtyHardAvail = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
    INItemLotSerial inItemLotSerial4 = inItemLotSerial1;
    nullable2 = inItemLotSerial4.QtyOnReceipt;
    Decimal valueOrDefault3 = ((Decimal?) itemLotSerial2?.QtyOnReceipt).GetValueOrDefault();
    inItemLotSerial4.QtyOnReceipt = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault3) : new Decimal?();
    int num2;
    if (inLotSerClass.LotSerAssign == "R")
    {
      nullable2 = inItemLotSerial1.QtyOnHand;
      Decimal num3 = 0M;
      num2 = nullable2.GetValueOrDefault() > num3 & nullable2.HasValue ? 1 : 0;
    }
    else
      num2 = 0;
    bool flag1 = num2 != 0;
    nullable2 = inItemLotSerial1.QtyAvail;
    Decimal num4 = 0M;
    int num5;
    if (!(nullable2.GetValueOrDefault() > num4 & nullable2.HasValue))
    {
      nullable2 = inItemLotSerial1.QtyHardAvail;
      Decimal num6 = 0M;
      if (!(nullable2.GetValueOrDefault() > num6 & nullable2.HasValue))
      {
        nullable2 = inItemLotSerial1.QtyOnReceipt;
        Decimal num7 = 0M;
        num5 = nullable2.GetValueOrDefault() > num7 & nullable2.HasValue ? 1 : 0;
        goto label_15;
      }
    }
    num5 = 1;
label_15:
    bool flag2 = num5 != 0;
    if (flag1 | flag2)
      split.QtyAvailForReturn = new Decimal?(0M);
    if (!flag1 & flag2)
    {
      INItemPlan inItemPlan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemPlan.inventoryID, Equal<P.AsInt>>>>>.And<BqlOperand<INItemPlan.lotSerialNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) inventoryID,
        (object) split.LotSerialNbr
      }));
      if (inItemPlan != null)
        split.SerialIsAlreadyReceivedRef = new EntityHelper((PXGraph) this.Base).GetEntityRowID(inItemPlan.RefNoteID, ", ");
    }
    split.SerialIsOnHand = new bool?(flag1);
    split.SerialIsAlreadyReceived = new bool?(flag2);
  }
}
