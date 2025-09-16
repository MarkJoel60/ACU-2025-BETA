// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.TransferLineSplittingExtension`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class TransferLineSplittingExtension<TGraph, TSplittingExt, TPrimary, TLine, TSplit> : 
  PXGraphExtension<TSplittingExt, TGraph>
  where TGraph : PXGraph
  where TSplittingExt : LineSplittingExtension<TGraph, TPrimary, TLine, TSplit>
  where TPrimary : class, IBqlTable, new()
  where TLine : class, IBqlTable, ILSPrimary, ILSTransferPrimary, new()
  where TSplit : class, IBqlTable, ILSDetail, new()
{
  protected TSplittingExt LineSplittingExt => this.Base1;

  protected PXCache<TSplit> SplitCache => this.LineSplittingExt.SplitCache;

  protected PXCache<TLine> LineCache => this.LineSplittingExt.LineCache;

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.LineSplittingExtension`4.SubscribeForLineEvents" />
  /// </summary>
  [PXOverride]
  public virtual void SubscribeForLineEvents(Action baseMethod)
  {
    baseMethod();
    if (!(this.LineLotSerialNbrField != (Type) null))
      return;
    PXGraph.FieldVerifyingEvents fieldVerifying = ((PXGraphExtension<TGraph>) this).Base.FieldVerifying;
    Type type = typeof (TLine);
    string name = this.LineLotSerialNbrField.Name;
    TransferLineSplittingExtension<TGraph, TSplittingExt, TPrimary, TLine, TSplit> splittingExtension = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) splittingExtension, __vmethodptr(splittingExtension, LineEventHandlerLotSerialNbr));
    fieldVerifying.AddHandler(type, name, pxFieldVerifying);
  }

  [PXOverride]
  public virtual void EventHandlerQty(
    AbstractEvents.IFieldVerifying<TLine, IBqlField, Decimal?> e,
    Action<AbstractEvents.IFieldVerifying<TLine, IBqlField, Decimal?>> baseMethod)
  {
    baseMethod(e);
    if (!this.Base1.IsTransferReceipt((ILSMaster) e.Row))
      return;
    Decimal? newValue = e.NewValue;
    if (!newValue.HasValue || this.Base1.SuppressedMode)
      return;
    if (!string.IsNullOrEmpty(e.Row.LotSerialNbr))
    {
      TLine row = e.Row;
      newValue = e.NewValue;
      Decimal qty = newValue.Value;
      string lotSerialNbr = e.Row.LotSerialNbr;
      this.VerifyQtyInTransit(row, qty, lotSerialNbr);
    }
    else
    {
      TLine row = e.Row;
      newValue = e.NewValue;
      Decimal qty = newValue.Value;
      this.VerifyMaxLineQty(row, qty, false);
    }
  }

  protected virtual void LineEventHandlerLotSerialNbr(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    TLine row = (TLine) e.Row;
    string newValue = (string) e.NewValue;
    if (this.Base1.SuppressedMode || !this.Base1.IsTransferReceipt((ILSMaster) row) || string.IsNullOrEmpty(newValue))
      return;
    this.VerifyQtyInTransit(row, row.Qty.GetValueOrDefault(), newValue);
  }

  [PXOverride]
  public virtual void EventHandler(
    AbstractEvents.IRowPersisting<TLine> e,
    Action<AbstractEvents.IRowPersisting<TLine>> baseMethod)
  {
    if (this.Base1.IsTransferReceipt((ILSMaster) e.Row) && PXDBOperationExt.Command(e.Operation) != 3)
    {
      this.VerifyMaxLineQty(e.Row, e.Row.Qty.GetValueOrDefault(), true);
      this.VerifyUnassignedQty(e.Row);
    }
    baseMethod(e);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.LineSplittingExtension`4.SubscribeForSplitEvents" />
  /// </summary>
  [PXOverride]
  public virtual void SubscribeForSplitEvents(Action baseMethod)
  {
    baseMethod();
    if (!(this.SplitLotSerialNbrField != (Type) null))
      return;
    PXGraph.FieldVerifyingEvents fieldVerifying = ((PXGraphExtension<TGraph>) this).Base.FieldVerifying;
    Type type = typeof (TSplit);
    string name = this.SplitLotSerialNbrField.Name;
    TransferLineSplittingExtension<TGraph, TSplittingExt, TPrimary, TLine, TSplit> splittingExtension = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) splittingExtension, __vmethodptr(splittingExtension, SplitEventHandlerLotSerialNbr));
    fieldVerifying.AddHandler(type, name, pxFieldVerifying);
  }

  [PXOverride]
  public virtual void EventHandlerQty(
    AbstractEvents.IFieldVerifying<TSplit, IBqlField, Decimal?> e,
    Action<AbstractEvents.IFieldVerifying<TSplit, IBqlField, Decimal?>> baseMethod)
  {
    baseMethod(e);
    if (this.Base1.SuppressedMode || !this.Base1.IsTransferReceipt((ILSMaster) e.Row) || string.IsNullOrEmpty(e.Row.LotSerialNbr))
      return;
    Decimal? newValue = e.NewValue;
    if (!newValue.HasValue)
      return;
    TSplit row = e.Row;
    newValue = e.NewValue;
    Decimal qty = newValue.Value;
    string lotSerialNbr = e.Row.LotSerialNbr;
    this.VerifyQtyInTransit(row, qty, lotSerialNbr);
  }

  protected virtual void SplitEventHandlerLotSerialNbr(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    TSplit row = (TSplit) e.Row;
    string newValue = (string) e.NewValue;
    if (this.Base1.SuppressedMode || !this.Base1.IsTransferReceipt((ILSMaster) row) || string.IsNullOrEmpty(newValue))
      return;
    this.VerifyQtyInTransit(row, row.Qty.GetValueOrDefault(), newValue);
  }

  [PXOverride]
  public virtual (Decimal, TSplit) IssueLotSerials(
    TLine line,
    Decimal deltaBaseQty,
    PXCache statusCache,
    PXCache statusAccumCache,
    PXResult<InventoryItem, INLotSerClass> item,
    Func<TLine, Decimal, PXCache, PXCache, PXResult<InventoryItem, INLotSerClass>, (Decimal, TSplit)> baseMethod)
  {
    return this.Base1.IsTransferReceipt((ILSMaster) line) ? (deltaBaseQty, this.LineSplittingExt.LineToSplit(line)) : baseMethod(line, deltaBaseQty, statusCache, statusAccumCache, item);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.LineSplittingExtension`4.IsPrimaryFieldsUpdated(`2,`2)" />
  /// </summary>
  [PXOverride]
  public virtual bool IsPrimaryFieldsUpdated(
    TLine line,
    TLine oldLine,
    Func<TLine, TLine, bool> baseMethod)
  {
    if (!this.Base1.IsTransferReceipt((ILSMaster) line))
      return baseMethod(line, oldLine);
    int? subItemId = line.SubItemID;
    int? nullable = oldLine.SubItemID;
    if (!(subItemId.GetValueOrDefault() == nullable.GetValueOrDefault() & subItemId.HasValue == nullable.HasValue))
      return true;
    nullable = ((ILSMaster) line).SiteID;
    int? siteId = ((ILSMaster) oldLine).SiteID;
    return !(nullable.GetValueOrDefault() == siteId.GetValueOrDefault() & nullable.HasValue == siteId.HasValue);
  }

  protected virtual void VerifyUnassignedQty(TLine line)
  {
    Decimal? unassignedQty = line.UnassignedQty;
    Decimal num = 0M;
    if (unassignedQty.GetValueOrDefault() > num & unassignedQty.HasValue)
      throw new PXRowPersistingException(this.LineQtyField.Name, (object) line.Qty, "One or more lines have unassigned Location and/or Lot/Serial Number");
  }

  protected virtual void VerifyMaxLineQty(TLine line, Decimal qty, bool persisting)
  {
    Decimal num = INUnitAttribute.ConvertFromBase((PXCache) this.LineCache, ((ILSMaster) line).InventoryID, line.UOM, line.MaxTransferBaseQty.GetValueOrDefault(), INPrecision.QUANTITY);
    if (!(qty > num) || !line.MaxTransferBaseQty.HasValue)
      return;
    if (persisting)
      throw new PXRowPersistingException(this.LineQtyField.Name, (object) qty, "Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
      {
        (object) num
      });
    ((PXCache) this.LineCache).RaiseExceptionHandling(this.LineQtyField.Name, (object) line, (object) qty, (Exception) new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", (PXErrorLevel) 4, new object[1]
    {
      (object) num
    }));
  }

  protected virtual void VerifyQtyInTransit(TLine line, Decimal qty, string lotSerialNbr)
  {
    INTransitLineLotSerialStatus transitLotSerialStatus = this.GetTransitLotSerialStatus(line, line.SubItemID, lotSerialNbr);
    Decimal num1 = INUnitAttribute.ConvertToBase((PXCache) this.LineCache, ((ILSMaster) line).InventoryID, line.UOM, qty, INPrecision.QUANTITY);
    if (transitLotSerialStatus != null)
    {
      Decimal? qtyAvail = transitLotSerialStatus.QtyAvail;
      Decimal num2 = num1;
      if (!(qtyAvail.GetValueOrDefault() < num2 & qtyAvail.HasValue))
        return;
    }
    throw new PXSetPropertyException("The quantity in transit for the {0} item with the {1} lot/serial number will become negative.", new object[2]
    {
      (object) InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<TGraph>) this).Base, ((ILSMaster) line).InventoryID).InventoryCD,
      (object) lotSerialNbr
    });
  }

  protected virtual void VerifyQtyInTransit(TSplit split, Decimal qty, string lotSerialNbr)
  {
    INTransitLineLotSerialStatus transitLotSerialStatus = this.GetTransitLotSerialStatus(PXParentAttribute.SelectParent<TLine>((PXCache) this.SplitCache, (object) split), split.SubItemID, lotSerialNbr);
    Decimal num1 = PXParentAttribute.SelectSiblings((PXCache) this.SplitCache, (object) split, typeof (TLine)).Cast<TSplit>().Where<TSplit>((Func<TSplit, bool>) (s =>
    {
      int? splitLineNbr1 = s.SplitLineNbr;
      int? splitLineNbr2 = split.SplitLineNbr;
      return !(splitLineNbr1.GetValueOrDefault() == splitLineNbr2.GetValueOrDefault() & splitLineNbr1.HasValue == splitLineNbr2.HasValue) && string.Equals(s.LotSerialNbr, lotSerialNbr, StringComparison.OrdinalIgnoreCase);
    })).Sum<TSplit>((Func<TSplit, Decimal>) (s => s.Qty.GetValueOrDefault()));
    if (transitLotSerialStatus != null)
    {
      Decimal? qtyAvail = transitLotSerialStatus.QtyAvail;
      Decimal num2 = qty + num1;
      if (!(qtyAvail.GetValueOrDefault() < num2 & qtyAvail.HasValue))
        return;
    }
    throw new PXSetPropertyException("The quantity in transit for the {0} item with the {1} lot/serial number will become negative.", new object[2]
    {
      (object) InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<TGraph>) this).Base, ((ILSMaster) split).InventoryID).InventoryCD,
      (object) lotSerialNbr
    });
  }

  protected virtual INTransitLineLotSerialStatus GetTransitLotSerialStatus(
    TLine line,
    int? subItemID,
    string lotSerialNbr)
  {
    if ((object) line == null)
      return (INTransitLineLotSerialStatus) null;
    return PXResultset<INTransitLineLotSerialStatus>.op_Implicit(((PXSelectBase<INTransitLineLotSerialStatus>) new FbqlSelect<SelectFromBase<INTransitLineLotSerialStatus, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTransitLineLotSerialStatus.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<INTransitLineLotSerialStatus.subItemID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INTransitLineLotSerialStatus.transferNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<INTransitLineLotSerialStatus.transferLineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INTransitLineLotSerialStatus.lotSerialNbr, IBqlString>.IsEqual<P.AsString>>>, INTransitLineLotSerialStatus>.View((PXGraph) ((PXGraphExtension<TGraph>) this).Base)).SelectWindowed(0, 1, new object[5]
    {
      (object) ((ILSMaster) line).InventoryID,
      (object) subItemID,
      (object) line.OrigRefNbr,
      (object) line.OrigLineNbr,
      (object) lotSerialNbr
    }));
  }

  /// Uses <see cref="P:PX.Objects.IN.GraphExtensions.LineSplittingExtension`4.SplitLotSerialNbrField" />
  [PXProtectedAccess(null)]
  protected abstract Type SplitLotSerialNbrField { get; }

  /// Uses <see cref="P:PX.Objects.IN.GraphExtensions.LineSplittingExtension`4.LineLotSerialNbrField" />
  [PXProtectedAccess(null)]
  protected abstract Type LineLotSerialNbrField { get; }

  /// Uses <see cref="P:PX.Objects.IN.GraphExtensions.LineSplittingExtension`4.LineQtyField" />
  [PXProtectedAccess(null)]
  protected abstract Type LineQtyField { get; }
}
