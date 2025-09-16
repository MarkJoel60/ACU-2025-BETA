// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.POReceiptLineSplittingExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class POReceiptLineSplittingExtension : 
  LineSplittingExtension<POReceiptEntry, PX.Objects.PO.POReceipt, PX.Objects.PO.POReceiptLine, POReceiptLineSplit>
{
  protected override Type SplitsToDocumentCondition
  {
    get
    {
      return typeof (KeysRelation<CompositeKey<Field<POReceiptLineSplit.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<POReceiptLineSplit.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, POReceiptLineSplit>, PX.Objects.PO.POReceipt, POReceiptLineSplit>.SameAsCurrent);
    }
  }

  protected override Type LineQtyField => typeof (PX.Objects.PO.POReceiptLine.receiptQty);

  public override POReceiptLineSplit LineToSplit(PX.Objects.PO.POReceiptLine item)
  {
    using (this.InvtMultModeScope(item))
    {
      POReceiptLineSplit split = POReceiptLineSplit.FromPOReceiptLine(item);
      Decimal? baseQty = item.BaseQty;
      Decimal? unassignedQty = item.UnassignedQty;
      split.BaseQty = baseQty.HasValue & unassignedQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() - unassignedQty.GetValueOrDefault()) : new Decimal?();
      return split;
    }
  }

  public override IEnumerable GenerateNumbers(PXAdapter adapter)
  {
    return this.LineCurrent == null || !this.IsLSEntryEnabled(this.LineCurrent) ? adapter.Get() : base.GenerateNumbers(adapter);
  }

  public override IEnumerable ShowSplits(PXAdapter adapter)
  {
    if (this.LineCurrent == null)
      return adapter.Get();
    if (!this.IsLSEntryEnabled(this.LineCurrent))
      throw new PXSetPropertyException("The Line Details dialog box cannot be opened because changing line details is not allowed for the selected item.");
    return base.ShowSplits(adapter);
  }

  protected override void SubscribeForLineEvents()
  {
    base.SubscribeForLineEvents();
    ((FieldUpdatedEvents) ((PXGraph) this.Base).FieldUpdated).AddAbstractHandler<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.receiptQty, Decimal>(new Action<AbstractEvents.IFieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.receiptQty, Decimal?>>(this.EventHandler));
    ((FieldSelectingEvents) ((PXGraph) this.Base).FieldSelecting).AddAbstractHandler<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.origOrderQty>(new Action<AbstractEvents.IFieldSelecting<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.origOrderQty>>(this.EventHandler));
    ((FieldSelectingEvents) ((PXGraph) this.Base).FieldSelecting).AddAbstractHandler<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.openOrderQty>(new Action<AbstractEvents.IFieldSelecting<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.openOrderQty>>(this.EventHandler));
  }

  protected virtual void EventHandler(
    AbstractEvents.IFieldUpdated<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.receiptQty, Decimal?> e)
  {
    if (e.Row == null)
      return;
    Decimal? receiptQty = e.Row.ReceiptQty;
    Decimal? oldValue = e.OldValue;
    if (receiptQty.GetValueOrDefault() == oldValue.GetValueOrDefault() & receiptQty.HasValue == oldValue.HasValue)
      return;
    ((IGenericEventWith<PXFieldUpdatedEventArgs>) e).Cache.RaiseFieldUpdated<PX.Objects.PO.POReceiptLine.baseReceiptQty>((object) e.Row, (object) e.Row.BaseReceiptQty);
  }

  protected virtual void EventHandler(
    AbstractEvents.IFieldSelecting<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.origOrderQty> e)
  {
    int? nullable1;
    if (e.Row?.PONbr != null)
    {
      PX.Objects.PO.POLine referencedPoLine = this.Base.GetReferencedPOLine(e.Row.POType, e.Row.PONbr, e.Row.POLineNbr);
      if (referencedPoLine != null)
      {
        int? inventoryId = e.Row.InventoryID;
        nullable1 = referencedPoLine.InventoryID;
        if (inventoryId.GetValueOrDefault() == nullable1.GetValueOrDefault() & inventoryId.HasValue == nullable1.HasValue)
        {
          if (!string.Equals(e.Row.UOM, referencedPoLine.UOM))
          {
            Decimal num = INUnitAttribute.ConvertToBase<PX.Objects.PO.POReceiptLine.inventoryID>(((IGenericEventWith<PXFieldSelectingEventArgs>) e).Cache, (object) e.Row, referencedPoLine.UOM, referencedPoLine.OrderQty.Value, INPrecision.QUANTITY);
            e.ReturnValue = (object) INUnitAttribute.ConvertFromBase<PX.Objects.PO.POReceiptLine.inventoryID, PX.Objects.PO.POReceiptLine.uOM>(((IGenericEventWith<PXFieldSelectingEventArgs>) e).Cache, (object) e.Row, num, INPrecision.QUANTITY);
          }
          else
            e.ReturnValue = (object) referencedPoLine.OrderQty;
        }
      }
    }
    if (e.Row?.OrigRefNbr != null)
    {
      INTran inTran = PXResultset<INTran>.op_Implicit(PXSelectBase<INTran, PXViewOf<INTran>.BasedOn<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.tranType, Equal<INTranType.transfer>>>>, And<BqlOperand<INTran.refNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.origRefNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<INTran.lineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.origLineNbr, IBqlInt>.FromCurrent>>>>.And<BqlOperand<INTran.docType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.origDocType, IBqlString>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
      {
        (object) e.Row
      }, Array.Empty<object>()));
      if (inTran != null)
        e.ReturnValue = (object) inTran.Qty;
    }
    object returnState = e.ReturnState;
    short? decPlQty = ((CommonSetup) ((PXCache) GraphHelper.Caches<CommonSetup>((PXGraph) this.Base)).Current).DecPlQty;
    int? nullable2;
    if (!decPlQty.HasValue)
    {
      nullable1 = new int?();
      nullable2 = nullable1;
    }
    else
      nullable2 = new int?((int) decPlQty.GetValueOrDefault());
    bool? nullable3 = new bool?(false);
    int? nullable4 = new int?(0);
    Decimal? nullable5 = new Decimal?(Decimal.MinValue);
    Decimal? nullable6 = new Decimal?(Decimal.MaxValue);
    PXFieldState instance = PXDecimalState.CreateInstance(returnState, nullable2, "OrigOrderQty", nullable3, nullable4, nullable5, nullable6);
    instance.DisplayName = PXMessages.LocalizeNoPrefix("Ordered Qty.");
    instance.Enabled = false;
    e.ReturnState = (object) instance;
  }

  protected virtual void EventHandler(
    AbstractEvents.IFieldSelecting<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.openOrderQty> e)
  {
    int? nullable1;
    Decimal? nullable2;
    if (e.Row?.PONbr != null)
    {
      PX.Objects.PO.POLine referencedPoLine = this.Base.GetReferencedPOLine(e.Row.POType, e.Row.PONbr, e.Row.POLineNbr);
      if (referencedPoLine != null)
      {
        int? inventoryId = e.Row.InventoryID;
        nullable1 = referencedPoLine.InventoryID;
        if (inventoryId.GetValueOrDefault() == nullable1.GetValueOrDefault() & inventoryId.HasValue == nullable1.HasValue)
        {
          Decimal? nullable3;
          if (!string.Equals(e.Row.UOM, referencedPoLine.UOM))
          {
            Decimal num = INUnitAttribute.ConvertToBase<PX.Objects.PO.POReceiptLine.inventoryID>(((IGenericEventWith<PXFieldSelectingEventArgs>) e).Cache, (object) e.Row, referencedPoLine.UOM, this.GetOpenQtyForReceiptWithPO(e.Row, referencedPoLine).Value, INPrecision.QUANTITY);
            nullable3 = new Decimal?(INUnitAttribute.ConvertFromBase<PX.Objects.PO.POReceiptLine.inventoryID>(((IGenericEventWith<PXFieldSelectingEventArgs>) e).Cache, (object) e.Row, e.Row.UOM, num, INPrecision.QUANTITY));
          }
          else
            nullable3 = this.GetOpenQtyForReceiptWithPO(e.Row, referencedPoLine);
          AbstractEvents.IFieldSelecting<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine.openOrderQty> ifieldSelecting = e;
          nullable2 = nullable3;
          Decimal num1 = 0M;
          // ISSUE: variable of a boxed type
          __Boxed<Decimal?> local = (ValueType) (nullable2.GetValueOrDefault() < num1 & nullable2.HasValue ? new Decimal?(0M) : nullable3);
          ifieldSelecting.ReturnValue = (object) local;
        }
      }
    }
    if (e.Row?.OrigRefNbr != null)
    {
      INTransitLineStatus transitLineStatus = PXResultset<INTransitLineStatus>.op_Implicit(PXSelectBase<INTransitLineStatus, PXViewOf<INTransitLineStatus>.BasedOn<SelectFromBase<INTransitLineStatus, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTransitLineStatus.transferNbr, Equal<BqlField<PX.Objects.PO.POReceiptLine.origRefNbr, IBqlString>.FromCurrent>>>>>.And<BqlOperand<INTransitLineStatus.transferLineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.origLineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
      {
        (object) e.Row
      }, Array.Empty<object>()));
      if (transitLineStatus != null)
      {
        nullable2 = transitLineStatus.QtyOnHand;
        Decimal num2 = nullable2.Value;
        Decimal num3;
        if (!e.Row.Released.GetValueOrDefault())
        {
          nullable2 = e.Row.BaseReceiptQty;
          num3 = nullable2.GetValueOrDefault();
        }
        else
          num3 = 0M;
        Decimal num4 = num2 - num3;
        e.ReturnValue = (object) INUnitAttribute.ConvertFromBase<PX.Objects.PO.POReceiptLine.inventoryID>(((IGenericEventWith<PXFieldSelectingEventArgs>) e).Cache, (object) e.Row, e.Row.UOM, num4, INPrecision.QUANTITY);
      }
    }
    object returnState = e.ReturnState;
    short? decPlQty = ((CommonSetup) ((PXCache) GraphHelper.Caches<CommonSetup>((PXGraph) this.Base)).Current).DecPlQty;
    int? nullable4;
    if (!decPlQty.HasValue)
    {
      nullable1 = new int?();
      nullable4 = nullable1;
    }
    else
      nullable4 = new int?((int) decPlQty.GetValueOrDefault());
    bool? nullable5 = new bool?(false);
    int? nullable6 = new int?(0);
    Decimal? nullable7 = new Decimal?(Decimal.MinValue);
    Decimal? nullable8 = new Decimal?(Decimal.MaxValue);
    PXFieldState instance = PXDecimalState.CreateInstance(returnState, nullable4, "OpenOrderQty", nullable5, nullable6, nullable7, nullable8);
    instance.DisplayName = PXMessages.LocalizeNoPrefix("Open Qty.");
    instance.Enabled = false;
    e.ReturnState = (object) instance;
  }

  protected override void EventHandler(AbstractEvents.IRowSelected<PX.Objects.PO.POReceiptLine> e)
  {
    if (e.Row == null)
      return;
    bool lsEntryEnabled = this.IsLSEntryEnabled(e.Row) && !e.Row.Released.GetValueOrDefault() && !this.IsDropshipReturn() && !POLineType.IsNonStockNonServiceNonDropShip(e.Row.LineType);
    ((PXCache) this.SplitCache).AllowInsert = lsEntryEnabled && this.AllowInsertAndDelete(e.Row);
    ((PXCache) this.SplitCache).AllowUpdate = lsEntryEnabled;
    ((PXCache) this.SplitCache).AllowDelete = lsEntryEnabled && this.AllowInsertAndDelete(e.Row);
    PXUIFieldAttribute.SetEnabled<POReceiptLineSplit.locationID>((PXCache) this.SplitCache, (object) null, e.Row.LineType != "GP" & lsEntryEnabled);
    PXCacheEx.Adjust<POLotSerialNbrAttribute>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row).For<PX.Objects.PO.POReceiptLine.lotSerialNbr>((Action<POLotSerialNbrAttribute>) (a => a.ForceDisable = !lsEntryEnabled));
    if (!lsEntryEnabled || !(Math.Abs(e.Row.UnassignedQty.GetValueOrDefault()) >= 0.0000005M))
      return;
    this.RaiseUnassignedExceptionHandling(e.Row);
  }

  protected virtual bool AllowInsertAndDelete(PX.Objects.PO.POReceiptLine line) => true;

  protected override void EventHandler(AbstractEvents.IRowInserted<PX.Objects.PO.POReceiptLine> e)
  {
    if (this.IsLSEntryEnabled(e.Row))
    {
      base.EventHandler(e);
    }
    else
    {
      ((IGenericEventWith<PXRowInsertedEventArgs>) e).Cache.SetValue<PX.Objects.PO.POReceiptLine.locationID>((object) e.Row, (object) null);
      ((IGenericEventWith<PXRowInsertedEventArgs>) e).Cache.SetValue<PX.Objects.PO.POReceiptLine.lotSerialNbr>((object) e.Row, (object) null);
      ((IGenericEventWith<PXRowInsertedEventArgs>) e).Cache.SetValue<PX.Objects.PO.POReceiptLine.expireDate>((object) e.Row, (object) null);
    }
  }

  protected override void EventHandler(AbstractEvents.IRowUpdated<PX.Objects.PO.POReceiptLine> e)
  {
    if (this.IsLSEntryEnabled(e.Row) && (e.Row.LineType != "PG" || e.Row.ReceiptType != "RN"))
    {
      using (this.ResolveNotDecimalUnitErrorRedirectorScope<POReceiptLineSplit.qty>((object) e.Row))
        base.EventHandler(e);
    }
    else
    {
      ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.SetValue<PX.Objects.PO.POReceiptLine.locationID>((object) e.Row, (object) null);
      ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.SetValue<PX.Objects.PO.POReceiptLine.lotSerialNbr>((object) e.Row, (object) null);
      ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.SetValue<PX.Objects.PO.POReceiptLine.expireDate>((object) e.Row, (object) null);
      if (e.Row == null || e.OldRow == null)
        return;
      int? inventoryId1 = e.Row.InventoryID;
      int? inventoryId2 = e.OldRow.InventoryID;
      if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
        return;
      this.RaiseRowDeleted(e.OldRow);
    }
  }

  protected override void EventHandler(AbstractEvents.IRowDeleted<PX.Objects.PO.POReceiptLine> e)
  {
    if (!this.IsLSEntryEnabled(e.Row))
      return;
    base.EventHandler(e);
  }

  protected virtual Decimal? GetOpenQtyForReceiptWithPO(PX.Objects.PO.POReceiptLine receiptLine, PX.Objects.PO.POLine origLine)
  {
    Decimal? orderQty = origLine.OrderQty;
    Decimal? receivedQty = origLine.ReceivedQty;
    return !(orderQty.HasValue & receivedQty.HasValue) ? new Decimal?() : new Decimal?(orderQty.GetValueOrDefault() - receivedQty.GetValueOrDefault());
  }

  protected override void SubscribeForSplitEvents()
  {
    base.SubscribeForSplitEvents();
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<POReceiptLineSplit, POReceiptLineSplit.invtMult, short>(new Action<AbstractEvents.IFieldDefaulting<POReceiptLineSplit, POReceiptLineSplit.invtMult, short?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<POReceiptLineSplit, POReceiptLineSplit.subItemID, int>(new Action<AbstractEvents.IFieldDefaulting<POReceiptLineSplit, POReceiptLineSplit.subItemID, int?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<POReceiptLineSplit, POReceiptLineSplit.locationID, int>(new Action<AbstractEvents.IFieldDefaulting<POReceiptLineSplit, POReceiptLineSplit.locationID, int?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<POReceiptLineSplit, POReceiptLineSplit.lotSerialNbr, string>(new Action<AbstractEvents.IFieldDefaulting<POReceiptLineSplit, POReceiptLineSplit.lotSerialNbr, string>>(this.EventHandler));
  }

  public virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<POReceiptLineSplit, POReceiptLineSplit.invtMult, short?> e)
  {
    if (this.LineCurrent == null)
      return;
    if (e.Row != null)
    {
      int? lineNbr1 = this.LineCurrent.LineNbr;
      int? lineNbr2 = e.Row.LineNbr;
      if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue))
        return;
    }
    using (this.InvtMultModeScope(this.LineCurrent))
    {
      e.NewValue = this.LineCurrent.InvtMult;
      ((ICancelEventArgs) e).Cancel = true;
    }
  }

  public virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<POReceiptLineSplit, POReceiptLineSplit.subItemID, int?> e)
  {
    if (this.LineCurrent == null)
      return;
    if (e.Row != null)
    {
      int? lineNbr1 = this.LineCurrent.LineNbr;
      int? lineNbr2 = e.Row.LineNbr;
      if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue))
        return;
    }
    e.NewValue = this.LineCurrent.SubItemID;
    ((ICancelEventArgs) e).Cancel = true;
  }

  public virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<POReceiptLineSplit, POReceiptLineSplit.locationID, int?> e)
  {
    if (this.LineCurrent == null)
      return;
    if (e.Row != null)
    {
      int? lineNbr1 = this.LineCurrent.LineNbr;
      int? lineNbr2 = e.Row.LineNbr;
      if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue))
        return;
    }
    e.NewValue = this.LineCurrent.LocationID;
    ((ICancelEventArgs) e).Cancel = true;
  }

  public virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<POReceiptLineSplit, POReceiptLineSplit.lotSerialNbr, string> e)
  {
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(e.Row.InventoryID);
    if (pxResult == null)
      return;
    if (!e.Row.InvtMult.HasValue)
    {
      object obj;
      ((IGenericEventWith<PXFieldDefaultingEventArgs>) e).Cache.RaiseFieldDefaulting<POReceiptLineSplit.invtMult>((object) e.Row, ref obj);
    }
    INLotSerTrack.Mode tranTrackMode = this.GetTranTrackMode((ILSMaster) e.Row, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult));
    if (tranTrackMode != INLotSerTrack.Mode.None && (tranTrackMode & INLotSerTrack.Mode.Create) <= INLotSerTrack.Mode.None)
      return;
    ILotSerNumVal lotSerNum = this.ReadLotSerNumVal(pxResult);
    foreach (POReceiptLineSplit number in INLotSerialNbrAttribute.CreateNumbers<POReceiptLineSplit>(((IGenericEventWith<PXFieldDefaultingEventArgs>) e).Cache, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult), lotSerNum, tranTrackMode, 1M))
    {
      e.NewValue = number.LotSerialNbr;
      ((ICancelEventArgs) e).Cancel = true;
    }
  }

  public override void EventHandlerQty(
    AbstractEvents.IFieldVerifying<POReceiptLineSplit, IBqlField, Decimal?> e)
  {
    if (this.IsTrackSerial(e.Row))
      base.EventHandlerQty(e);
    else
      e.NewValue = this.VerifySNQuantity(((IGenericEventWith<PXFieldVerifyingEventArgs>) e).Cache, (ILSMaster) e.Row, e.NewValue, "qty");
  }

  protected virtual void EventHandler(PX.Data.Events.RowPersisting<POReceiptLineSplit> e)
  {
    bool flag1 = e.Row.LineType == "GP";
    bool flag2 = POLineType.IsNonStockNonServiceNonDropShip(e.Row.LineType);
    PXDefaultAttribute.SetPersistingCheck<POReceiptLineSplit.locationID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<POReceiptLineSplit>>) e).Cache, (object) e.Row, flag2 | flag1 ? (PXPersistingCheck) 2 : (PXPersistingCheck) 0);
    PXDefaultAttribute.SetPersistingCheck<POReceiptLineSplit.subItemID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<POReceiptLineSplit>>) e).Cache, (object) e.Row, flag2 ? (PXPersistingCheck) 2 : (PXPersistingCheck) 0);
  }

  public override PX.Objects.PO.POReceiptLine Clone(PX.Objects.PO.POReceiptLine item)
  {
    PX.Objects.PO.POReceiptLine poReceiptLine = base.Clone(item);
    poReceiptLine.POType = (string) null;
    poReceiptLine.PONbr = (string) null;
    poReceiptLine.POLineNbr = new int?();
    return poReceiptLine;
  }

  public override bool IsTrackSerial(POReceiptLineSplit split)
  {
    if (split.LineType == "GP")
      return readLotSerialClass()?.LotSerTrack == "S";
    return (!(((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current?.ReceiptType == "RN") || !(readLotSerialClass()?.LotSerAssign == "U")) && base.IsTrackSerial(split);

    INLotSerClass readLotSerialClass()
    {
      return ((PXResult) this.ReadInventoryItem(split.InventoryID))?.GetItem<INLotSerClass>();
    }
  }

  protected override bool IsLotSerOptionsEnabled(LSSelect.LotSerOptions opt)
  {
    if (base.IsLotSerOptionsEnabled(opt))
    {
      PX.Objects.PO.POReceipt current = ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current;
      if ((current != null ? (!current.Released.GetValueOrDefault() ? 1 : 0) : 1) != 0)
        return !this.IsDropshipReturn();
    }
    return false;
  }

  public override void DefaultLotSerialNbr(POReceiptLineSplit row)
  {
    if (row.ReceiptType == "RX")
      row.AssignedNbr = (string) null;
    else
      base.DefaultLotSerialNbr(row);
  }

  protected override INLotSerTrack.Mode GetTranTrackMode(ILSMaster row, INLotSerClass lotSerClass)
  {
    if (((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current?.ReceiptType == "RN" && lotSerClass?.LotSerAssign == "U")
      return INLotSerTrack.Mode.None;
    return row is PX.Objects.PO.POReceiptLine poReceiptLine && poReceiptLine.LineType == "GP" && lotSerClass != null && lotSerClass.LotSerTrack != null && lotSerClass.LotSerTrack != "N" ? INLotSerTrack.Mode.Create : base.GetTranTrackMode(row, lotSerClass);
  }

  public virtual bool IsLSEntryEnabled(PX.Objects.PO.POReceiptLine line)
  {
    if (line == null)
      return true;
    bool? nullable = line.IsLSEntryBlocked;
    if (nullable.GetValueOrDefault())
      return false;
    if (POLineType.IsStockNonDropShip(line.LineType) || POLineType.IsNonStockNonServiceNonDropShip(line.LineType))
      return true;
    if (!EnumerableExtensions.IsIn<string>(line.LineType, "GP", "PG"))
      return false;
    nullable = line.LotSerialNbrRequiredForDropship;
    return nullable.GetValueOrDefault();
  }

  protected virtual bool IsDropshipReturn()
  {
    return !string.IsNullOrEmpty(((PXSelectBase<PX.Objects.PO.POReceipt>) this.Base.Document).Current?.SOOrderNbr);
  }

  protected override void AppendSerialStatusCmdWhere(
    PXSelectBase<INLotSerialStatusByCostCenter> cmd,
    PX.Objects.PO.POReceiptLine Row,
    INLotSerClass lotSerClass)
  {
    if (Row.SubItemID.HasValue)
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.subItemID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.subItemID, IBqlInt>.FromCurrent>>>();
    if (Row.LocationID.HasValue)
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.locationID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.locationID, IBqlInt>.FromCurrent>>>();
    else
      cmd.WhereAnd<Where<BqlOperand<INLocation.receiptsValid, IBqlBool>.IsEqual<True>>>();
    if (!lotSerClass.IsManualAssignRequired.GetValueOrDefault())
      return;
    if (string.IsNullOrEmpty(Row.LotSerialNbr))
      cmd.WhereAnd<Where<BqlOperand<True, IBqlBool>.IsEqual<False>>>();
    else
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.lotSerialNbr, IBqlString>.IsEqual<BqlField<INLotSerialStatusByCostCenter.lotSerialNbr, IBqlString>.FromCurrent>>>();
  }

  protected override void RaiseUnassignedExceptionHandling(PX.Objects.PO.POReceiptLine line)
  {
    ((PXCache) this.LineCache).RaiseExceptionHandling<PX.Objects.PO.POReceiptLine.unassignedQty>((object) line, (object) line.UnassignedQty, (Exception) new PXSetPropertyException("One or more lines have unassigned Location and/or Lot/Serial Number", (PXErrorLevel) 2));
  }

  protected override void EventHandlerInternal(AbstractEvents.IRowUpdated<PX.Objects.PO.POReceiptLine> e)
  {
    if (e.Row.Released.GetValueOrDefault() && ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.ObjectsEqual<PX.Objects.PO.POReceiptLine.released>((object) e.Row, (object) e.OldRow))
      return;
    base.EventHandlerInternal(e);
  }
}
