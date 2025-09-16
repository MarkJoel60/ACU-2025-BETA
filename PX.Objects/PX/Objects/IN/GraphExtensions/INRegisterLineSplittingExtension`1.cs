// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INRegisterLineSplittingExtension`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.PO;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class INRegisterLineSplittingExtension<TRegisterGraph> : 
  LineSplittingExtension<TRegisterGraph, PX.Objects.IN.INRegister, INTran, INTranSplit>
  where TRegisterGraph : INRegisterEntryBase
{
  protected override Type SplitsToDocumentCondition
  {
    get
    {
      return typeof (KeysRelation<CompositeKey<Field<INTranSplit.docType>.IsRelatedTo<PX.Objects.IN.INRegister.docType>, Field<INTranSplit.refNbr>.IsRelatedTo<PX.Objects.IN.INRegister.refNbr>>.WithTablesOf<PX.Objects.IN.INRegister, INTranSplit>, PX.Objects.IN.INRegister, INTranSplit>.SameAsCurrent);
    }
  }

  protected override Type LineQtyField => typeof (INTran.qty);

  public override INTranSplit LineToSplit(INTran line)
  {
    using (this.InvtMultModeScope(line))
    {
      INTranSplit split = INTranSplit.FromINTran(line);
      Decimal? baseQty = line.BaseQty;
      Decimal? unassignedQty = line.UnassignedQty;
      split.BaseQty = baseQty.HasValue & unassignedQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() - unassignedQty.GetValueOrDefault()) : new Decimal?();
      return split;
    }
  }

  public override void Initialize()
  {
    base.Initialize();
    ((RowSelectedEvents) ((PXGraph) (object) this.Base).RowSelected).AddAbstractHandler<PX.Objects.IN.INRegister>(new Action<AbstractEvents.IRowSelected<PX.Objects.IN.INRegister>>(this.EventHandler));
    ((RowUpdatedEvents) ((PXGraph) (object) this.Base).RowUpdated).AddAbstractHandler<PX.Objects.IN.INRegister>(new Action<AbstractEvents.IRowUpdated<PX.Objects.IN.INRegister>>(this.EventHandler));
  }

  protected virtual void EventHandler(AbstractEvents.IRowSelected<PX.Objects.IN.INRegister> e)
  {
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained1 = PXCacheEx.AdjustUI((PXCache) this.LineCache, (object) null).For<INTran.locationID>((Action<PXUIFieldAttribute>) (a => a.Enabled = this.IsCorrectionMode || this.IsFullMode));
    chained1 = chained1.SameFor<INTran.lotSerialNbr>();
    chained1 = chained1.SameFor<INTran.expireDate>();
    chained1 = chained1.SameFor<INTran.reasonCode>();
    chained1 = chained1.SameFor<INTran.projectID>();
    chained1 = chained1.SameFor<INTran.taskID>();
    chained1 = chained1.SameFor<INTran.costCodeID>();
    chained1 = chained1.For<INTran.tranType>((Action<PXUIFieldAttribute>) (a => a.Enabled = this.IsFullMode));
    chained1 = chained1.SameFor<INTran.branchID>();
    chained1 = chained1.SameFor<INTran.inventoryID>();
    chained1 = chained1.SameFor<INTran.subItemID>();
    chained1 = chained1.SameFor<INTran.siteID>();
    chained1 = chained1.SameFor<INTran.qty>();
    chained1 = chained1.SameFor<INTran.uOM>();
    chained1 = chained1.SameFor<INTran.unitPrice>();
    chained1 = chained1.SameFor<INTran.tranAmt>();
    chained1 = chained1.SameFor<INTran.unitCost>();
    chained1 = chained1.SameFor<INTran.tranCost>();
    chained1.SameFor<INTran.tranDesc>();
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster;
    if (this.IsCorrectionMode && e.Row?.DocType == "R" && e.Row.TransferNbr != null && e.Row.OrigModule != "PO")
    {
      attributeAdjuster = PXCacheEx.AdjustUI((PXCache) this.LineCache, (object) null);
      attributeAdjuster.For<INTran.qty>((Action<PXUIFieldAttribute>) (a => a.Enabled = true));
    }
    attributeAdjuster = PXCacheEx.AdjustUI((PXCache) this.SplitCache, (object) null);
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained2 = attributeAdjuster.For<INTranSplit.subItemID>((Action<PXUIFieldAttribute>) (a => a.Enabled = this.IsCorrectionMode || this.IsFullMode));
    chained2 = chained2.SameFor<INTranSplit.qty>();
    chained2 = chained2.SameFor<INTranSplit.locationID>();
    chained2 = chained2.SameFor<INTranSplit.lotSerialNbr>();
    chained2.SameFor<INTranSplit.expireDate>();
    if (!(e.Row?.DocType == "T"))
      return;
    PXCacheEx.Adjust<INExpireDateAttribute>((PXCache) this.SplitCache, (object) null).For<INTranSplit.expireDate>((Action<INExpireDateAttribute>) (a => a.ForceDisable = true));
    attributeAdjuster = PXCacheEx.AdjustUI((PXCache) this.SplitCache, (object) null);
    attributeAdjuster.For<INTranSplit.expireDate>((Action<PXUIFieldAttribute>) (a => a.Visible = false));
  }

  protected virtual void EventHandler(AbstractEvents.IRowUpdated<PX.Objects.IN.INRegister> e)
  {
    bool? hold1 = e.Row.Hold;
    bool? hold2 = e.OldRow.Hold;
    if (hold1.GetValueOrDefault() == hold2.GetValueOrDefault() & hold1.HasValue == hold2.HasValue)
      return;
    bool? hold3 = e.Row.Hold;
    bool flag = false;
    if (!(hold3.GetValueOrDefault() == flag & hold3.HasValue))
      return;
    foreach (INTran selectSibling in PXParentAttribute.SelectSiblings((PXCache) this.LineCache, (object) null, typeof (PX.Objects.IN.INRegister)))
    {
      Decimal? nullable = selectSibling.BaseQty;
      if (Math.Abs(nullable.Value) >= 0.0000005M)
      {
        nullable = selectSibling.UnassignedQty;
        Decimal num = 0.0000005M;
        if (!(nullable.GetValueOrDefault() >= num & nullable.HasValue))
        {
          nullable = selectSibling.UnassignedQty;
          num = -0.0000005M;
          if (!(nullable.GetValueOrDefault() <= num & nullable.HasValue))
            continue;
        }
        ((PXCache) this.LineCache).RaiseExceptionHandling<INTran.qty>((object) selectSibling, (object) selectSibling.Qty, (Exception) new PXSetPropertyException("One or more lines have unassigned Location and/or Lot/Serial Number"));
        GraphHelper.MarkUpdated((PXCache) this.LineCache, (object) selectSibling, true);
      }
    }
  }

  protected override void EventHandler(AbstractEvents.IRowSelected<INTran> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(((PXCache) this.LineCache).Graph, e.Row.InventoryID);
    PXCache<INTranSplit> splitCache = this.SplitCache;
    int num;
    if (inventoryItem != null)
    {
      bool? stkItem = inventoryItem.StkItem;
      bool flag = false;
      num = !(stkItem.GetValueOrDefault() == flag & stkItem.HasValue) ? 1 : (!inventoryItem.KitItem.GetValueOrDefault() ? 1 : 0);
    }
    else
      num = 1;
    PXUIFieldAttribute.SetReadOnly<INTranSplit.inventoryID>((PXCache) splitCache, (object) null, num != 0);
  }

  protected override void EventHandler(AbstractEvents.IRowInserted<INTran> e)
  {
    short? invtMult = e.Row.InvtMult;
    int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num = 0;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue) || e.Row.TranType == "RCA")
    {
      base.EventHandler(e);
    }
    else
    {
      ((IGenericEventWith<PXRowInsertedEventArgs>) e).Cache.SetValue<INTran.lotSerialNbr>((object) e.Row, (object) null);
      ((IGenericEventWith<PXRowInsertedEventArgs>) e).Cache.SetValue<INTran.expireDate>((object) e.Row, (object) null);
    }
  }

  protected override void EventHandler(AbstractEvents.IRowUpdated<INTran> e)
  {
    short? invtMult = e.Row.InvtMult;
    int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num = 0;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
    {
      if (!object.Equals((object) e.Row.TranType, (object) e.OldRow.TranType))
      {
        ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.SetDefaultExt<INTran.invtMult>((object) e.Row);
        foreach (INTranSplit selectSibling in PXParentAttribute.SelectSiblings((PXCache) this.SplitCache, (object) INTranSplit.FromINTran(e.Row), typeof (INTran)))
        {
          INTranSplit copy = PXCache<INTranSplit>.CreateCopy(selectSibling);
          selectSibling.TranType = e.Row.TranType;
          GraphHelper.MarkUpdated((PXCache) this.SplitCache, (object) selectSibling, true);
          this.SplitCache.RaiseRowUpdated(selectSibling, copy);
        }
      }
      base.EventHandler(e);
    }
    else
    {
      if ((!(e.Row.TranType == "TRX") ? 0 : (POLineType.IsNonStockNonServiceNonDropShip(e.Row.POLineType) ? 1 : 0)) == 0 || !string.IsNullOrEmpty(e.Row.LotSerialNbr))
        ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.SetValue<INTran.lotSerialNbr>((object) e.Row, (object) null);
      ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.SetValue<INTran.expireDate>((object) e.Row, (object) null);
    }
  }

  protected override void EventHandler(AbstractEvents.IRowDeleted<INTran> e)
  {
    short? invtMult = e.Row.InvtMult;
    int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    int num = 0;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return;
    base.EventHandler(e);
  }

  protected override void EventHandler(AbstractEvents.IRowPersisting<INTran> e)
  {
    if (EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
    {
      bool? hold = (PXParentAttribute.SelectParent<PX.Objects.IN.INRegister>(((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache, (object) e.Row) ?? this.DocumentCurrent).Hold;
      bool flag = false;
      if (hold.GetValueOrDefault() == flag & hold.HasValue && Math.Abs(e.Row.BaseQty.Value) >= 0.0000005M)
      {
        Decimal? unassignedQty1 = e.Row.UnassignedQty;
        Decimal num = 0.0000005M;
        if (!(unassignedQty1.GetValueOrDefault() >= num & unassignedQty1.HasValue))
        {
          Decimal? unassignedQty2 = e.Row.UnassignedQty;
          num = -0.0000005M;
          if (!(unassignedQty2.GetValueOrDefault() <= num & unassignedQty2.HasValue))
            goto label_6;
        }
        if (((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache.RaiseExceptionHandling<INTran.qty>((object) e.Row, (object) e.Row.Qty, (Exception) new PXSetPropertyException("One or more lines have unassigned Location and/or Lot/Serial Number")))
          throw new PXRowPersistingException(typeof (INTran.qty).Name, (object) e.Row.Qty, "One or more lines have unassigned Location and/or Lot/Serial Number");
      }
    }
label_6:
    base.EventHandler(e);
  }

  public override void EventHandlerQty(
    AbstractEvents.IFieldVerifying<INTran, IBqlField, Decimal?> e)
  {
    Decimal? newValue = e.NewValue;
    Decimal num = 0M;
    if (newValue.GetValueOrDefault() < num & newValue.HasValue)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", (PXErrorLevel) 4, new object[1]
      {
        (object) 0
      });
  }

  protected override void SubscribeForSplitEvents()
  {
    base.SubscribeForSplitEvents();
    ((FieldDefaultingEvents) ((PXGraph) (object) this.Base).FieldDefaulting).AddAbstractHandler<INTranSplit, INTranSplit.invtMult, short>(new Action<AbstractEvents.IFieldDefaulting<INTranSplit, INTranSplit.invtMult, short?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) (object) this.Base).FieldDefaulting).AddAbstractHandler<INTranSplit, INTranSplit.subItemID, int>(new Action<AbstractEvents.IFieldDefaulting<INTranSplit, INTranSplit.subItemID, int?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) (object) this.Base).FieldDefaulting).AddAbstractHandler<INTranSplit, INTranSplit.locationID, int>(new Action<AbstractEvents.IFieldDefaulting<INTranSplit, INTranSplit.locationID, int?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) (object) this.Base).FieldDefaulting).AddAbstractHandler<INTranSplit, INTranSplit.lotSerialNbr, string>(new Action<AbstractEvents.IFieldDefaulting<INTranSplit, INTranSplit.lotSerialNbr, string>>(this.EventHandler));
  }

  public override void EventHandler(AbstractEvents.IRowPersisting<INTranSplit> e)
  {
    base.EventHandler(e);
    if (e.Row == null || !EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    Decimal? baseQty = e.Row.BaseQty;
    Decimal num = 0M;
    if (baseQty.GetValueOrDefault() == num & baseQty.HasValue || e.Row.LocationID.HasValue)
      return;
    this.ThrowFieldIsEmpty<INTranSplit.locationID>(((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache, (object) e.Row);
  }

  protected virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<INTranSplit, INTranSplit.invtMult, short?> e)
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

  protected virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<INTranSplit, INTranSplit.subItemID, int?> e)
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

  protected virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<INTranSplit, INTranSplit.locationID, int?> e)
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

  protected virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<INTranSplit, INTranSplit.lotSerialNbr, string> e)
  {
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(e.Row.InventoryID);
    if (pxResult == null)
      return;
    object obj;
    if (!e.Row.InvtMult.HasValue)
      ((IGenericEventWith<PXFieldDefaultingEventArgs>) e).Cache.RaiseFieldDefaulting<INTranSplit.invtMult>((object) e.Row, ref obj);
    if (e.Row.TranType == null)
      ((IGenericEventWith<PXFieldDefaultingEventArgs>) e).Cache.RaiseFieldDefaulting<INTranSplit.tranType>((object) e.Row, ref obj);
    INLotSerTrack.Mode tranTrackMode = this.GetTranTrackMode((ILSMaster) e.Row, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult));
    if (tranTrackMode != INLotSerTrack.Mode.None && (tranTrackMode & INLotSerTrack.Mode.Create) <= INLotSerTrack.Mode.None)
      return;
    ILotSerNumVal lotSerNum = this.ReadLotSerNumVal(pxResult);
    foreach (INTranSplit number in INLotSerialNbrAttribute.CreateNumbers<INTranSplit>(((IGenericEventWith<PXFieldDefaultingEventArgs>) e).Cache, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult), lotSerNum, tranTrackMode, 1M))
    {
      e.NewValue = number.LotSerialNbr;
      ((ICancelEventArgs) e).Cancel = true;
    }
  }

  public override void DefaultLotSerialNbr(INTranSplit split)
  {
    if (split.DocType == "R" && split.TranType == "TRX" || !string.IsNullOrEmpty(split.OrigModule) && split.OrigModule != "IN")
      split.AssignedNbr = (string) null;
    else
      base.DefaultLotSerialNbr(split);
  }

  protected override void SetLineQtyFromBase(INTran line)
  {
    if (line.UOM == line.OrigUOM)
    {
      Decimal? baseQty = line.BaseQty;
      Decimal? baseOrigFullQty = line.BaseOrigFullQty;
      if (baseQty.GetValueOrDefault() == baseOrigFullQty.GetValueOrDefault() & baseQty.HasValue == baseOrigFullQty.HasValue)
      {
        line.Qty = line.OrigFullQty;
        return;
      }
    }
    base.SetLineQtyFromBase(line);
  }

  protected override void AppendSerialStatusCmdWhere(
    PXSelectBase<INLotSerialStatusByCostCenter> cmd,
    INTran Row,
    INLotSerClass lotSerClass)
  {
    if (Row.SubItemID.HasValue)
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.subItemID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.subItemID, IBqlInt>.FromCurrent>>>();
    if (Row.LocationID.HasValue)
    {
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.locationID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.locationID, IBqlInt>.FromCurrent>>>();
    }
    else
    {
      switch (Row.TranType)
      {
        case "III":
          cmd.WhereAnd<Where<BqlOperand<INLocation.receiptsValid, IBqlBool>.IsEqual<True>>>();
          break;
        case "TRX":
          cmd.WhereAnd<Where<BqlOperand<INLocation.transfersValid, IBqlBool>.IsEqual<True>>>();
          break;
        default:
          cmd.WhereAnd<Where<BqlOperand<INLocation.salesValid, IBqlBool>.IsEqual<True>>>();
          break;
      }
    }
    if (!lotSerClass.IsManualAssignRequired.GetValueOrDefault())
      return;
    if (string.IsNullOrEmpty(Row.LotSerialNbr))
      cmd.WhereAnd<Where<BqlOperand<True, IBqlBool>.IsEqual<False>>>();
    else
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.lotSerialNbr, IBqlString>.IsEqual<BqlField<INLotSerialStatusByCostCenter.lotSerialNbr, IBqlString>.FromCurrent>>>();
  }

  public override bool IsTrackSerial(INTranSplit split)
  {
    return (!(split.TranType == "III") || !(split.OrigModule == "PO") || !(readLotSerialClass()?.LotSerAssign == "U")) && base.IsTrackSerial(split);

    INLotSerClass readLotSerialClass()
    {
      return ((PXResult) this.ReadInventoryItem(split.InventoryID))?.GetItem<INLotSerClass>();
    }
  }

  protected override INLotSerTrack.Mode GetTranTrackMode(ILSMaster row, INLotSerClass lotSerClass)
  {
    return row is INTranSplit inTranSplit && inTranSplit.TranType == "III" && lotSerClass.LotSerAssign == "U" && inTranSplit.OrigModule == "PO" ? INLotSerTrack.Mode.None : base.GetTranTrackMode(row, lotSerClass);
  }
}
