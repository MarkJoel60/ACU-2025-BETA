// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.KitAssemblyEntryExt.INComponentLineSplittingExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.KitAssemblyEntryExt;

public class INComponentLineSplittingExtension : 
  LineSplittingExtension<KitAssemblyEntry, INKitRegister, INComponentTran, INComponentTranSplit>
{
  protected bool Initialized;

  protected override Type SplitsToDocumentCondition
  {
    get
    {
      return typeof (KeysRelation<CompositeKey<Field<INComponentTranSplit.docType>.IsRelatedTo<INKitRegister.docType>, Field<INComponentTranSplit.refNbr>.IsRelatedTo<INKitRegister.refNbr>>.WithTablesOf<INKitRegister, INComponentTranSplit>, INKitRegister, INComponentTranSplit>.SameAsCurrent);
    }
  }

  public override INComponentTranSplit LineToSplit(INComponentTran line)
  {
    using (this.InvtMultModeScope(line))
    {
      INComponentTranSplit split = INComponentTranSplit.FromINComponentTran(line);
      Decimal? baseQty = line.BaseQty;
      Decimal? unassignedQty = line.UnassignedQty;
      split.BaseQty = baseQty.HasValue & unassignedQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() - unassignedQty.GetValueOrDefault()) : new Decimal?();
      return split;
    }
  }

  public override void Initialize()
  {
    base.Initialize();
    ((RowUpdatedEvents) ((PXGraph) this.Base).RowUpdated).AddAbstractHandler<INKitRegister>(new Action<AbstractEvents.IRowUpdated<INKitRegister>>(this.EventHandler));
  }

  protected virtual void EventHandler(AbstractEvents.IRowUpdated<INKitRegister> e)
  {
    bool? hold1 = e.Row.Hold;
    bool? hold2 = e.OldRow.Hold;
    if (hold1.GetValueOrDefault() == hold2.GetValueOrDefault() & hold1.HasValue == hold2.HasValue)
      return;
    bool? hold3 = e.Row.Hold;
    bool flag = false;
    if (!(hold3.GetValueOrDefault() == flag & hold3.HasValue))
      return;
    foreach (INComponentTran selectSibling in PXParentAttribute.SelectSiblings((PXCache) this.LineCache, (object) null, typeof (INKitRegister)))
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
        ((PXCache) this.LineCache).RaiseExceptionHandling<INComponentTran.qty>((object) selectSibling, (object) selectSibling.Qty, (Exception) new PXSetPropertyException("One or more lines have unassigned Location and/or Lot/Serial Number"));
        GraphHelper.MarkUpdated((PXCache) this.LineCache, (object) selectSibling, true);
      }
    }
  }

  public override void EventHandlerQty(
    AbstractEvents.IFieldVerifying<INComponentTran, IBqlField, Decimal?> e)
  {
    base.EventHandlerQty(e);
    Decimal? newValue = e.NewValue;
    Decimal num = 0M;
    if (newValue.GetValueOrDefault() < num & newValue.HasValue)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", (PXErrorLevel) 4, new object[1]
      {
        (object) 0
      });
  }

  protected override void EventHandler(AbstractEvents.IRowPersisting<INComponentTran> e)
  {
    if (EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
    {
      bool? hold = (PXParentAttribute.SelectParent<INKitRegister>(((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache, (object) e.Row) ?? ((PXSelectBase<INKitRegister>) this.Base.Document).Current).Hold;
      bool flag = false;
      if (hold.GetValueOrDefault() == flag & hold.HasValue && Math.Abs(e.Row.BaseQty.Value) >= 0.0000005M)
      {
        Decimal? unassignedQty = e.Row.UnassignedQty;
        Decimal num = 0.0000005M;
        if (!(unassignedQty.GetValueOrDefault() >= num & unassignedQty.HasValue))
        {
          unassignedQty = e.Row.UnassignedQty;
          num = -0.0000005M;
          if (!(unassignedQty.GetValueOrDefault() <= num & unassignedQty.HasValue))
            goto label_6;
        }
        if (((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache.RaiseExceptionHandling<INComponentTran.qty>((object) e.Row, (object) e.Row.Qty, (Exception) new PXSetPropertyException("One or more lines have unassigned Location and/or Lot/Serial Number")))
          throw new PXRowPersistingException(typeof (INComponentTran.qty).Name, (object) e.Row.Qty, "One or more lines have unassigned Location and/or Lot/Serial Number");
      }
    }
label_6:
    base.EventHandler(e);
  }

  protected override void SubscribeForSplitEvents()
  {
    base.SubscribeForSplitEvents();
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<INComponentTranSplit, INComponentTranSplit.invtMult, short>(new Action<AbstractEvents.IFieldDefaulting<INComponentTranSplit, INComponentTranSplit.invtMult, short?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<INComponentTranSplit, INComponentTranSplit.subItemID, int>(new Action<AbstractEvents.IFieldDefaulting<INComponentTranSplit, INComponentTranSplit.subItemID, int?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<INComponentTranSplit, INComponentTranSplit.locationID, int>(new Action<AbstractEvents.IFieldDefaulting<INComponentTranSplit, INComponentTranSplit.locationID, int?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<INComponentTranSplit, INComponentTranSplit.lotSerialNbr, string>(new Action<AbstractEvents.IFieldDefaulting<INComponentTranSplit, INComponentTranSplit.lotSerialNbr, string>>(this.EventHandler));
    ((FieldVerifyingEvents) ((PXGraph) this.Base).FieldVerifying).AddAbstractHandler<INComponentTranSplit, INComponentTranSplit.qty, Decimal>(new Action<AbstractEvents.IFieldVerifying<INComponentTranSplit, INComponentTranSplit.qty, Decimal?>>(this.EventHandler));
  }

  public virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<INComponentTranSplit, INComponentTranSplit.invtMult, short?> e)
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
    AbstractEvents.IFieldDefaulting<INComponentTranSplit, INComponentTranSplit.subItemID, int?> e)
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
    AbstractEvents.IFieldDefaulting<INComponentTranSplit, INComponentTranSplit.locationID, int?> e)
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
    AbstractEvents.IFieldDefaulting<INComponentTranSplit, INComponentTranSplit.lotSerialNbr, string> e)
  {
    PXResult<InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(e.Row.InventoryID);
    if (pxResult == null)
      return;
    object obj;
    if (!e.Row.InvtMult.HasValue)
      ((IGenericEventWith<PXFieldDefaultingEventArgs>) e).Cache.RaiseFieldDefaulting<INComponentTranSplit.invtMult>((object) e.Row, ref obj);
    if (e.Row.TranType == null)
      ((IGenericEventWith<PXFieldDefaultingEventArgs>) e).Cache.RaiseFieldDefaulting<INComponentTranSplit.tranType>((object) e.Row, ref obj);
    INLotSerClass lotSerClass = PXResult<InventoryItem, INLotSerClass>.op_Implicit(pxResult);
    string tranType = e.Row.TranType;
    short? invtMult = e.Row.InvtMult;
    int? invMult = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    INLotSerTrack.Mode mode = INLotSerialNbrAttribute.TranTrackMode(lotSerClass, tranType, invMult);
    if (mode != INLotSerTrack.Mode.None && (mode & INLotSerTrack.Mode.Create) <= INLotSerTrack.Mode.None)
      return;
    ILotSerNumVal lotSerNum = this.ReadLotSerNumVal(pxResult);
    foreach (INComponentTranSplit number in INLotSerialNbrAttribute.CreateNumbers<INComponentTranSplit>(((IGenericEventWith<PXFieldDefaultingEventArgs>) e).Cache, PXResult<InventoryItem, INLotSerClass>.op_Implicit(pxResult), lotSerNum, mode, 1M))
    {
      e.NewValue = number.LotSerialNbr;
      ((ICancelEventArgs) e).Cancel = true;
    }
  }

  public virtual void EventHandler(
    AbstractEvents.IFieldVerifying<INComponentTranSplit, INComponentTranSplit.qty, Decimal?> e)
  {
    if (!e.Row.InventoryID.HasValue)
      return;
    InventoryItem inventoryItem;
    INLotSerClass inLotSerClass;
    this.ReadInventoryItem(e.Row.InventoryID).Deconstruct(ref inventoryItem, ref inLotSerClass);
    INLotSerClass lotSerClass = inLotSerClass;
    string tranType = e.Row.TranType;
    short? invtMult = e.Row.InvtMult;
    int? invMult = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    if (!INLotSerialNbrAttribute.IsTrackSerial(lotSerClass, tranType, invMult) || !EnumerableExtensions.IsNotIn<Decimal?>(e.NewValue, new Decimal?(), new Decimal?(0M), new Decimal?(1M)))
      return;
    e.NewValue = new Decimal?(1M);
  }

  public virtual void EventHandlerINComponentTranSplit(
    AbstractEvents.IRowPersisting<INComponentTranSplit> e)
  {
    if (e.Row == null || !EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    Decimal? baseQty = e.Row.BaseQty;
    Decimal num = 0M;
    if (baseQty.GetValueOrDefault() == num & baseQty.HasValue || e.Row.LocationID.HasValue)
      return;
    this.ThrowFieldIsEmpty<INComponentTranSplit.locationID>(((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache, (object) e.Row);
  }

  protected override DateTime? ExpireDateByLot(ILSMaster item, ILSMaster master)
  {
    if (master != null)
    {
      short? invtMult = master.InvtMult;
      int? nullable = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      int num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      {
        item.ExpireDate = new DateTime?();
        return base.ExpireDateByLot(item, (ILSMaster) null);
      }
    }
    return base.ExpireDateByLot(item, master);
  }
}
