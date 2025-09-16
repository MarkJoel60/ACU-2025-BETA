// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.KitAssemblyEntryExt.INKitLineSplittingExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.KitAssemblyEntryExt;

public class INKitLineSplittingExtension : 
  LineSplittingExtension<KitAssemblyEntry, INKitRegister, INKitRegister, INKitTranSplit>
{
  protected override Type SplitsToDocumentCondition
  {
    get
    {
      return typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INKitTranSplit.docType, Equal<BqlField<INKitRegister.docType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<INKitTranSplit.refNbr, IBqlString>.IsEqual<BqlField<INKitRegister.refNbr, IBqlString>.FromCurrent>>>);
    }
  }

  public override INKitTranSplit LineToSplit(INKitRegister line)
  {
    using (this.InvtMultModeScope(line))
    {
      INKitTranSplit split = INKitTranSplit.FromINKitRegister(line);
      Decimal? baseQty = line.BaseQty;
      Decimal? unassignedQty = line.UnassignedQty;
      split.BaseQty = baseQty.HasValue & unassignedQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() - unassignedQty.GetValueOrDefault()) : new Decimal?();
      return split;
    }
  }

  public override void Initialize()
  {
    base.Initialize();
    ((RowUpdatedEvents) ((PXGraph) this.Base).RowUpdated).AddAbstractHandler<INKitRegister>(new Action<AbstractEvents.IRowUpdated<INKitRegister>>(this.EventHandlerHeader));
  }

  protected override void AddLotSerOptionsView()
  {
    base.AddLotSerOptionsView();
    ((PXGraph) this.Base).Views[this.TypePrefixed("LotSerOptions")].AllowSelect = PXAccess.FeatureInstalled<FeaturesSet.subItem>() || PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>() || PXAccess.FeatureInstalled<FeaturesSet.lotSerialTracking>() || PXAccess.FeatureInstalled<FeaturesSet.replenishment>() || PXAccess.FeatureInstalled<FeaturesSet.sOToPOLink>();
  }

  protected virtual void EventHandlerHeader(AbstractEvents.IRowUpdated<INKitRegister> e)
  {
    bool? hold1 = e.Row.Hold;
    bool? hold2 = e.OldRow.Hold;
    if (hold1.GetValueOrDefault() == hold2.GetValueOrDefault() & hold1.HasValue == hold2.HasValue)
      return;
    bool? hold3 = e.Row.Hold;
    bool flag = false;
    if (!(hold3.GetValueOrDefault() == flag & hold3.HasValue))
      return;
    Decimal? unassignedQty = e.Row.UnassignedQty;
    Decimal num = 0M;
    if (unassignedQty.GetValueOrDefault() == num & unassignedQty.HasValue)
      return;
    ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.RaiseExceptionHandling<INKitRegister.qty>((object) e.Row, (object) e.Row.Qty, (Exception) new PXSetPropertyException("One or more lines have unassigned Location and/or Lot/Serial Number"));
  }

  protected override void EventHandler(AbstractEvents.IRowPersisting<INKitRegister> e)
  {
    if (EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
    {
      INKitRegister row = e.Row;
      if (row != null)
      {
        bool? hold = row.Hold;
        bool flag = false;
        if (hold.GetValueOrDefault() == flag & hold.HasValue && Math.Abs(e.Row.BaseQty.Value) >= 0.0000005M)
        {
          Decimal? unassignedQty = row.UnassignedQty;
          Decimal num = 0.0000005M;
          if (!(unassignedQty.GetValueOrDefault() >= num & unassignedQty.HasValue))
          {
            unassignedQty = row.UnassignedQty;
            num = -0.0000005M;
            if (!(unassignedQty.GetValueOrDefault() <= num & unassignedQty.HasValue))
              goto label_7;
          }
          if (((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache.RaiseExceptionHandling<INKitRegister.qty>((object) e.Row, (object) e.Row.Qty, (Exception) new PXSetPropertyException("One or more lines have unassigned Location and/or Lot/Serial Number")))
            throw new PXRowPersistingException(typeof (INKitRegister.qty).Name, (object) e.Row.Qty, "One or more lines have unassigned Location and/or Lot/Serial Number");
        }
      }
    }
label_7:
    base.EventHandler(e);
  }

  public override void EventHandlerQty(
    AbstractEvents.IFieldVerifying<INKitRegister, IBqlField, Decimal?> e)
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

  protected override void SubscribeForSplitEvents()
  {
    base.SubscribeForSplitEvents();
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<INKitTranSplit, INKitTranSplit.invtMult, short>(new Action<AbstractEvents.IFieldDefaulting<INKitTranSplit, INKitTranSplit.invtMult, short?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<INKitTranSplit, INKitTranSplit.subItemID, int>(new Action<AbstractEvents.IFieldDefaulting<INKitTranSplit, INKitTranSplit.subItemID, int?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<INKitTranSplit, INKitTranSplit.locationID, int>(new Action<AbstractEvents.IFieldDefaulting<INKitTranSplit, INKitTranSplit.locationID, int?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<INKitTranSplit, INKitTranSplit.lotSerialNbr, string>(new Action<AbstractEvents.IFieldDefaulting<INKitTranSplit, INKitTranSplit.lotSerialNbr, string>>(this.EventHandler));
    ((FieldVerifyingEvents) ((PXGraph) this.Base).FieldVerifying).AddAbstractHandler<INKitTranSplit, INKitTranSplit.qty, Decimal>(new Action<AbstractEvents.IFieldVerifying<INKitTranSplit, INKitTranSplit.qty, Decimal?>>(this.EventHandler));
  }

  public virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<INKitTranSplit, INKitTranSplit.invtMult, short?> e)
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
    AbstractEvents.IFieldDefaulting<INKitTranSplit, INKitTranSplit.subItemID, int?> e)
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
    AbstractEvents.IFieldDefaulting<INKitTranSplit, INKitTranSplit.locationID, int?> e)
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
    AbstractEvents.IFieldDefaulting<INKitTranSplit, INKitTranSplit.lotSerialNbr, string> e)
  {
    PXResult<InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(e.Row.InventoryID);
    if (pxResult == null)
      return;
    object obj;
    if (!e.Row.InvtMult.HasValue)
      ((IGenericEventWith<PXFieldDefaultingEventArgs>) e).Cache.RaiseFieldDefaulting<INKitTranSplit.invtMult>((object) e.Row, ref obj);
    if (e.Row.TranType == null)
      ((IGenericEventWith<PXFieldDefaultingEventArgs>) e).Cache.RaiseFieldDefaulting<INKitTranSplit.tranType>((object) e.Row, ref obj);
    INLotSerClass lotSerClass = PXResult<InventoryItem, INLotSerClass>.op_Implicit(pxResult);
    string tranType = e.Row.TranType;
    short? invtMult = e.Row.InvtMult;
    int? invMult = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    INLotSerTrack.Mode mode = INLotSerialNbrAttribute.TranTrackMode(lotSerClass, tranType, invMult);
    if (mode != INLotSerTrack.Mode.None && (mode & INLotSerTrack.Mode.Create) <= INLotSerTrack.Mode.None)
      return;
    ILotSerNumVal lotSerNum = this.ReadLotSerNumVal(pxResult);
    foreach (INKitTranSplit number in INLotSerialNbrAttribute.CreateNumbers<INKitTranSplit>(((IGenericEventWith<PXFieldDefaultingEventArgs>) e).Cache, PXResult<InventoryItem, INLotSerClass>.op_Implicit(pxResult), lotSerNum, mode, 1M))
    {
      e.NewValue = number.LotSerialNbr;
      ((ICancelEventArgs) e).Cancel = true;
    }
  }

  public virtual void EventHandler(
    AbstractEvents.IFieldVerifying<INKitTranSplit, INKitTranSplit.qty, Decimal?> e)
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

  public virtual void EventHandlerINKitTranSplit(AbstractEvents.IRowPersisting<INKitTranSplit> e)
  {
    if (e.Row == null || !EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    Decimal? baseQty = e.Row.BaseQty;
    Decimal num = 0M;
    if (baseQty.GetValueOrDefault() == num & baseQty.HasValue || e.Row.LocationID.HasValue)
      return;
    this.ThrowFieldIsEmpty<INKitTranSplit.locationID>(((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache, (object) e.Row);
  }

  protected override INKitTranSplit[] SelectSplits(INKitRegister row, bool compareInventoryID = true)
  {
    return GraphHelper.RowCast<INKitTranSplit>((IEnumerable) ((IEnumerable<PXResult<INKitTranSplit>>) PXSelectBase<INKitTranSplit, PXViewOf<INKitTranSplit>.BasedOn<SelectFromBase<INKitTranSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INKitTranSplit.docType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<INKitTranSplit.refNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<INKitTranSplit.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) row.DocType,
      (object) row.RefNbr,
      (object) row.KitLineNbr
    })).AsEnumerable<PXResult<INKitTranSplit>>()).ToArray<INKitTranSplit>();
  }

  protected override INKitTranSplit[] SelectSplits(INKitTranSplit row)
  {
    return this.SelectSplits(PXParentAttribute.SelectParent<INKitRegister>((PXCache) this.SplitCache, (object) row), true);
  }
}
