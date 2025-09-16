// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSServiceOrderLineSplittingExtension
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

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
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class FSServiceOrderLineSplittingExtension : 
  LineSplittingExtension<ServiceOrderEntry, FSServiceOrder, FSSODet, FSSODetSplit>
{
  protected FSServiceOrder _LastSelected;

  public bool IsLocationEnabled
  {
    get
    {
      PX.Objects.SO.SOOrderType soOrderType = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSetup<PX.Objects.SO.SOOrderType>.Select((PXGraph) this.Base, Array.Empty<object>()));
      if (soOrderType == null)
        return true;
      bool? nullable = soOrderType.RequireShipping;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        nullable = soOrderType.RequireLocation;
        if (nullable.GetValueOrDefault())
          return soOrderType.INDocType != "UND";
      }
      return false;
    }
  }

  public bool IsLotSerialRequired
  {
    get
    {
      PX.Objects.SO.SOOrderType soOrderType = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSetup<PX.Objects.SO.SOOrderType>.Select((PXGraph) this.Base, Array.Empty<object>()));
      return soOrderType == null || soOrderType.RequireLotSerial.GetValueOrDefault();
    }
  }

  public bool IsLSEntryEnabled
  {
    get
    {
      PX.Objects.SO.SOOrderType soOrderType = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSetup<PX.Objects.SO.SOOrderType>.Select((PXGraph) this.Base, Array.Empty<object>()));
      return soOrderType == null || soOrderType.RequireLocation.GetValueOrDefault() || soOrderType.RequireLotSerial.GetValueOrDefault();
    }
  }

  protected override Type SplitsToDocumentCondition
  {
    get
    {
      return typeof (KeysRelation<CompositeKey<Field<FSSODetSplit.srvOrdType>.IsRelatedTo<FSServiceOrder.srvOrdType>, Field<FSSODetSplit.refNbr>.IsRelatedTo<FSServiceOrder.refNbr>>.WithTablesOf<FSServiceOrder, FSSODetSplit>, FSServiceOrder, FSSODetSplit>.SameAsCurrent);
    }
  }

  protected override Type LineQtyField => typeof (FSSODet.orderQty);

  public override FSSODetSplit LineToSplit(FSSODet line)
  {
    using (new LineSplittingExtension<ServiceOrderEntry, FSServiceOrder, FSSODet, FSSODetSplit>.InvtMultScope(line))
    {
      FSSODetSplit split = (FSSODetSplit) line;
      Decimal? baseQty = line.BaseQty;
      Decimal? unassignedQty = line.UnassignedQty;
      split.BaseQty = baseQty.HasValue & unassignedQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() - unassignedQty.GetValueOrDefault()) : new Decimal?();
      return split;
    }
  }

  public override void Initialize()
  {
    base.Initialize();
    ((RowSelectedEvents) ((PXGraph) this.Base).RowSelected).AddAbstractHandler<FSServiceOrder>(new Action<AbstractEvents.IRowSelected<FSServiceOrder>>(this.EventHandler));
    ((RowUpdatedEvents) ((PXGraph) this.Base).RowUpdated).AddAbstractHandler<FSServiceOrder>(new Action<AbstractEvents.IRowUpdated<FSServiceOrder>>(this.EventHandler));
    this.showSplits?.SetVisible(PXAccess.FeatureInstalled<FeaturesSet.inventory>() && ((PXSelectBase<FSSrvOrdType>) this.Base.ServiceOrderTypeSelected)?.Current?.Behavior != "QT");
  }

  public override IEnumerable ShowSplits(PXAdapter adapter)
  {
    if (this.LineCurrent == null)
      return adapter.Get();
    if (!this.LineCurrent.InventoryID.HasValue)
      throw new PXSetPropertyException("This action cannot be used for a line with the Instruction or Comment type.");
    if (EnumerableExtensions.IsIn<string>(this.LineCurrent.LineType, "SERVI", "NSTKI"))
    {
      bool? enablePo = this.LineCurrent.EnablePO;
      bool flag = false;
      if (enablePo.GetValueOrDefault() == flag & enablePo.HasValue)
        throw new PXSetPropertyException("Shipment Scheduling and Bin/Lot/Serial assignment are not possible for non-stock items.");
    }
    int num = this.IsLSEntryEnabled ? 1 : 0;
    return base.ShowSplits(adapter);
  }

  protected virtual void EventHandler(AbstractEvents.IRowSelected<FSServiceOrder> e)
  {
    if (this._LastSelected == null || this._LastSelected != e.Row)
    {
      PXUIFieldAttribute.SetRequired<FSSODet.locationID>((PXCache) this.LineCache, this.IsLocationEnabled);
      PXUIFieldAttribute.SetVisible<FSSODet.locationID>((PXCache) this.LineCache, (object) null, this.IsLocationEnabled);
      PXUIFieldAttribute.SetVisible<FSSODet.lotSerialNbr>((PXCache) this.LineCache, (object) null, this.IsLSEntryEnabled);
      PXUIFieldAttribute.SetVisible<FSSODet.expireDate>((PXCache) this.LineCache, (object) null, this.IsLSEntryEnabled);
      PXUIFieldAttribute.SetVisible<FSSODetSplit.inventoryID>((PXCache) this.SplitCache, (object) null, this.IsLSEntryEnabled);
      PXUIFieldAttribute.SetVisible<FSSODetSplit.expireDate>((PXCache) this.SplitCache, (object) null, this.IsLSEntryEnabled);
      PXView pxView;
      if (((Dictionary<string, PXView>) ((PXGraph) this.Base).Views).TryGetValue(this.TypePrefixed("LotSerOptions"), out pxView))
        pxView.AllowSelect = this.IsLSEntryEnabled;
      if (e.Row != null)
        this._LastSelected = e.Row;
    }
    this.showSplits.SetEnabled(false);
    if (!this.IsLSEntryEnabled)
      return;
    this.showSplits.SetEnabled(true);
  }

  protected virtual void EventHandler(AbstractEvents.IRowUpdated<FSServiceOrder> e)
  {
    if (!this.IsLSEntryEnabled)
      return;
    bool? hold1 = e.Row.Hold;
    bool? hold2 = e.OldRow.Hold;
    if (hold1.GetValueOrDefault() == hold2.GetValueOrDefault() & hold1.HasValue == hold2.HasValue)
      return;
    hold2 = e.Row.Hold;
    bool flag = false;
    if (!(hold2.GetValueOrDefault() == flag & hold2.HasValue))
      return;
    foreach (FSSODet selectSibling in PXParentAttribute.SelectSiblings((PXCache) this.LineCache, (object) null, typeof (FSServiceOrder)))
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
        ((PXCache) this.LineCache).RaiseExceptionHandling<FSSODet.orderQty>((object) selectSibling, (object) selectSibling.Qty, (Exception) new PXSetPropertyException("One or more lines have unassigned Location and/or Lot/Serial Number"));
        GraphHelper.MarkUpdated((PXCache) this.LineCache, (object) selectSibling);
      }
    }
  }

  protected override void EventHandler(AbstractEvents.IRowUpdated<FSSODet> e)
  {
    try
    {
      base.EventHandler(e);
    }
    catch (PXUnitConversionException ex)
    {
      if (PXUIFieldAttribute.GetErrors(((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache, (object) e.Row, new PXErrorLevel[1]
      {
        (PXErrorLevel) 4
      }).Keys.Any<string>(new Func<string, bool>(isUomField)))
        return;
      ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.RaiseExceptionHandling<FSSODet.uOM>((object) e.Row, (object) null, (Exception) ex);
    }

    static bool isUomField(string f)
    {
      return string.Equals(f, "uOM", StringComparison.InvariantCultureIgnoreCase);
    }
  }

  protected override void EventHandler(AbstractEvents.IRowPersisting<FSSODet> e)
  {
    if (EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
    {
      if (!(e.Row.SOLineType == "GI"))
      {
        if (e.Row.SOLineType == "GN")
        {
          short? invtMult = e.Row.InvtMult;
          if ((invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() != -1)
            goto label_8;
        }
        else
          goto label_8;
      }
      Decimal? nullable;
      Decimal num;
      if (e.Row.TranType != "UND")
      {
        nullable = e.Row.BaseQty;
        num = 0M;
        if (nullable.GetValueOrDefault() < num & nullable.HasValue)
        {
          if (!((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache.RaiseExceptionHandling<FSSODet.orderQty>((object) e.Row, (object) e.Row.Qty, (Exception) new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", (PXErrorLevel) 0)))
            return;
          throw new PXRowPersistingException("orderQty", (object) e.Row.Qty, "Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
          {
            (object) 0
          });
        }
      }
label_8:
      if (this.IsLSEntryEnabled)
      {
        bool? hold = (PXParentAttribute.SelectParent<FSServiceOrder>(((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache, (object) e.Row) ?? ((PXSelectBase<FSServiceOrder>) this.Base.ServiceOrderRecords).Current).Hold;
        bool flag = false;
        if (hold.GetValueOrDefault() == flag & hold.HasValue)
        {
          nullable = e.Row.BaseQty;
          if (Math.Abs(nullable.Value) >= 0.0000005M)
          {
            nullable = e.Row.UnassignedQty;
            num = 0.0000005M;
            if (!(nullable.GetValueOrDefault() >= num & nullable.HasValue))
            {
              nullable = e.Row.UnassignedQty;
              num = -0.0000005M;
              if (!(nullable.GetValueOrDefault() <= num & nullable.HasValue))
                goto label_15;
            }
            if (((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache.RaiseExceptionHandling<FSSODet.orderQty>((object) e.Row, (object) e.Row.Qty, (Exception) new PXSetPropertyException("One or more lines have unassigned Location and/or Lot/Serial Number")))
              throw new PXRowPersistingException("orderQty", (object) e.Row.Qty, "One or more lines have unassigned Location and/or Lot/Serial Number");
          }
        }
      }
    }
label_15:
    if (!this.IsLSEntryEnabled)
    {
      if (e.Row.TranType == "TRX" && this.LineCounters.ContainsKey(e.Row))
        this.LineCounters[e.Row].UnassignedNumber = 0;
      else
        this.LineCounters[e.Row] = new LSSelect.Counters()
        {
          UnassignedNumber = 0
        };
    }
    base.EventHandler(e);
  }

  protected override void SubscribeForSplitEvents()
  {
    base.SubscribeForSplitEvents();
    ((RowSelectedEvents) ((PXGraph) this.Base).RowSelected).AddAbstractHandler<FSSODetSplit>(new Action<AbstractEvents.IRowSelected<FSSODetSplit>>(this.EventHandler));
    ((RowDeletingEvents) ((PXGraph) this.Base).RowDeleting).AddAbstractHandler<FSSODetSplit>(new Action<AbstractEvents.IRowDeleting<FSSODetSplit>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<FSSODetSplit, FSSODetSplit.invtMult, short>(new Action<AbstractEvents.IFieldDefaulting<FSSODetSplit, FSSODetSplit.invtMult, short?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<FSSODetSplit, FSSODetSplit.subItemID, int>(new Action<AbstractEvents.IFieldDefaulting<FSSODetSplit, FSSODetSplit.subItemID, int?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<FSSODetSplit, FSSODetSplit.locationID, int>(new Action<AbstractEvents.IFieldDefaulting<FSSODetSplit, FSSODetSplit.locationID, int?>>(this.EventHandler));
  }

  protected virtual void EventHandler(AbstractEvents.IRowSelected<FSSODetSplit> e)
  {
    if (e.Row == null)
      return;
    bool flag1 = e.Row.LineType == "GI";
    object valueExt = ((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache.GetValueExt<FSSODetSplit.isAllocated>((object) e.Row);
    bool? nullable;
    int num1;
    if (!e.Row.IsAllocated.GetValueOrDefault())
    {
      nullable = (bool?) PXFieldState.UnwrapValue(valueExt);
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 1;
    bool flag2 = num1 != 0;
    nullable = e.Row.Completed;
    bool valueOrDefault = nullable.GetValueOrDefault();
    bool flag3 = e.Row.Operation == "I";
    int num2;
    if (e.Row.PONbr == null)
    {
      if (e.Row.SOOrderNbr != null)
      {
        nullable = e.Row.IsAllocated;
        num2 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num2 = 0;
    }
    else
      num2 = 1;
    bool flag4 = num2 != 0;
    FSSODet fssoDet = PXParentAttribute.SelectParent<FSSODet>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row);
    PXUIFieldAttribute.SetEnabled<FSSODetSplit.subItemID>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, flag1);
    PXUIFieldAttribute.SetEnabled<FSSODetSplit.completed>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<FSSODetSplit.shippedQty>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<FSSODetSplit.shipmentNbr>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, false);
    PXCache cache = ((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache;
    FSSODetSplit row = e.Row;
    int num3;
    if (flag1 & flag3 && !valueOrDefault)
    {
      if (fssoDet == null)
      {
        num3 = 1;
      }
      else
      {
        nullable = fssoDet.POCreate;
        num3 = !nullable.GetValueOrDefault() ? 1 : 0;
      }
    }
    else
      num3 = 0;
    PXUIFieldAttribute.SetEnabled<FSSODetSplit.isAllocated>(cache, (object) row, num3 != 0);
    PXUIFieldAttribute.SetEnabled<FSSODetSplit.siteID>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, flag1 & flag2 && !flag4);
    PXUIFieldAttribute.SetEnabled<FSSODetSplit.qty>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, !valueOrDefault && !flag4);
    PXUIFieldAttribute.SetEnabled<FSSODetSplit.shipDate>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, !valueOrDefault && fssoDet?.ShipComplete == "B");
    PXUIFieldAttribute.SetEnabled<FSSODetSplit.pONbr>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<FSSODetSplit.pOReceiptNbr>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, false);
    nullable = e.Row.Completed;
    if (!nullable.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetEnabled(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, false);
  }

  public virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<FSSODetSplit, FSSODetSplit.invtMult, short?> e)
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
    using (new LineSplittingExtension<ServiceOrderEntry, FSServiceOrder, FSSODet, FSSODetSplit>.InvtMultScope(this.LineCurrent))
    {
      e.NewValue = this.LineCurrent.InvtMult;
      ((ICancelEventArgs) e).Cancel = true;
    }
  }

  public virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<FSSODetSplit, FSSODetSplit.subItemID, int?> e)
  {
    if (this.LineCurrent == null)
      return;
    if (e.Row != null)
    {
      int? lineNbr1 = this.LineCurrent.LineNbr;
      int? lineNbr2 = e.Row.LineNbr;
      if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue) || !e.Row.IsStockItem.GetValueOrDefault())
        return;
    }
    e.NewValue = this.LineCurrent.SubItemID;
    ((ICancelEventArgs) e).Cancel = true;
  }

  public virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<FSSODetSplit, FSSODetSplit.locationID, int?> e)
  {
    if (this.LineCurrent == null || !this.LineCurrent.LocationID.HasValue)
      return;
    if (e.Row != null)
    {
      int? lineNbr1 = this.LineCurrent.LineNbr;
      int? lineNbr2 = e.Row.LineNbr;
      if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue) || !e.Row.IsStockItem.GetValueOrDefault())
        return;
    }
    e.NewValue = this.LineCurrent.LocationID;
    ((ICancelEventArgs) e).Cancel = this.SuppressedMode || e.NewValue.HasValue || !this.IsLocationEnabled;
  }

  protected override void EventHandler(AbstractEvents.IRowInserting<FSSODetSplit> e)
  {
    if (!this.IsLSEntryEnabled)
      return;
    if (e.ExternalCall && e.Row.LineType != "GI")
      throw new PXSetPropertyException("The record cannot be inserted.");
    base.EventHandler(e);
    if (e.Row == null || this.IsLocationEnabled || !e.Row.LocationID.HasValue)
      return;
    e.Row.LocationID = new int?();
  }

  protected virtual void EventHandler(AbstractEvents.IRowDeleting<FSSODetSplit> e)
  {
    if (e.Row != null && (e.Row.POCreate.GetValueOrDefault() && (!string.IsNullOrEmpty(e.Row.PONbr) || !string.IsNullOrEmpty(e.Row.POType)) || !string.IsNullOrEmpty(e.Row.POReceiptNbr) || !string.IsNullOrEmpty(e.Row.POReceiptType)))
      throw new PXException("The line cannot be deleted because the purchase order has already been created for this line.");
  }

  public override void EventHandler(AbstractEvents.IRowPersisting<FSSODetSplit> e)
  {
    base.EventHandler(e);
    if (e.Row == null || !EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    int num1;
    if (e.Row.RequireLocation.GetValueOrDefault() && e.Row.IsStockItem.GetValueOrDefault())
    {
      Decimal? baseQty = e.Row.BaseQty;
      Decimal num2 = 0M;
      num1 = !(baseQty.GetValueOrDefault() == num2 & baseQty.HasValue) ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag = num1 != 0;
    PXDefaultAttribute.SetPersistingCheck<FSSODetSplit.subItemID>(((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache, (object) e.Row, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<FSSODetSplit.locationID>(((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache, (object) e.Row, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  public override void EventHandlerUOM(
    AbstractEvents.IFieldDefaulting<FSSODetSplit, IBqlField, string> e)
  {
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(e.Row.InventoryID);
    if (pxResult != null && PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerTrack == "S")
    {
      e.NewValue = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).BaseUnit;
      ((ICancelEventArgs) e).Cancel = true;
    }
    else
      base.EventHandlerUOM(e);
  }

  internal FSSODetSplit[] GetSplits(FSSODet line) => this.SelectSplits((FSSODetSplit) line, true);

  protected override FSSODetSplit[] SelectSplits(FSSODetSplit split)
  {
    return this.SelectSplits(split, true);
  }

  protected virtual FSSODetSplit[] SelectSplits(FSSODetSplit split, bool excludeCompleted = true)
  {
    return this.Availability.IsOptimizationEnabled ? ((IEnumerable<FSSODetSplit>) this.SelectAllSplits(split)).Where<FSSODetSplit>(new Func<FSSODetSplit, bool>(NotCompleted)).ToArray<FSSODetSplit>() : ((IEnumerable<FSSODetSplit>) base.SelectSplits(split)).Where<FSSODetSplit>(new Func<FSSODetSplit, bool>(NotCompleted)).ToArray<FSSODetSplit>();

    bool NotCompleted(FSSODetSplit a)
    {
      bool? completed = a.Completed;
      bool flag = false;
      if (completed.GetValueOrDefault() == flag & completed.HasValue)
        return true;
      return !excludeCompleted && a.PONbr == null && a.SOOrderNbr == null;
    }
  }

  private FSSODetSplit[] SelectAllSplits(FSSODetSplit split)
  {
    return PXParentAttribute.SelectSiblings((PXCache) this.SplitCache, (object) split, typeof (FSServiceOrder)).Cast<FSSODetSplit>().Where<FSSODetSplit>((Func<FSSODetSplit, bool>) (a =>
    {
      if (!this.SameInventoryItem((ILSMaster) a, (ILSMaster) split))
        return false;
      int? lineNbr1 = a.LineNbr;
      int? lineNbr2 = split.LineNbr;
      return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
    })).ToArray<FSSODetSplit>();
  }

  protected override FSSODetSplit[] SelectSplitsOrdered(FSSODetSplit split)
  {
    return this.SelectSplitsOrdered(split, true);
  }

  protected virtual FSSODetSplit[] SelectSplitsOrdered(FSSODetSplit split, bool excludeCompleted = true)
  {
    return ((IEnumerable<FSSODetSplit>) this.SelectSplits(split, excludeCompleted)).OrderBy<FSSODetSplit, int>((Func<FSSODetSplit, int>) (s =>
    {
      if (s.Completed.GetValueOrDefault())
        return 0;
      return !s.IsAllocated.GetValueOrDefault() ? 2 : 1;
    })).ThenBy<FSSODetSplit, int?>((Func<FSSODetSplit, int?>) (s => s.SplitLineNbr)).ToArray<FSSODetSplit>();
  }

  protected override FSSODetSplit[] SelectSplitsReversed(FSSODetSplit split)
  {
    return this.SelectSplitsReversed(split, true);
  }

  protected virtual FSSODetSplit[] SelectSplitsReversed(FSSODetSplit split, bool excludeCompleted = true)
  {
    return ((IEnumerable<FSSODetSplit>) this.SelectSplits(split, excludeCompleted)).OrderByDescending<FSSODetSplit, int>((Func<FSSODetSplit, int>) (s =>
    {
      if (s.Completed.GetValueOrDefault())
        return 0;
      return !s.IsAllocated.GetValueOrDefault() ? 2 : 1;
    })).ThenByDescending<FSSODetSplit, int?>((Func<FSSODetSplit, int?>) (s => s.SplitLineNbr)).ToArray<FSSODetSplit>();
  }

  public override PXSelectBase<INLotSerialStatusByCostCenter> GetSerialStatusCmd(
    FSSODet line,
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> item)
  {
    PXSelectBase<INLotSerialStatusByCostCenter> serialStatusCmd = (PXSelectBase<INLotSerialStatusByCostCenter>) new FbqlSelect<SelectFromBase<INLotSerialStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INLocation>.On<INLotSerialStatusByCostCenter.FK.Location>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLotSerialStatusByCostCenter.inventoryID, Equal<BqlField<INLotSerialStatusByCostCenter.inventoryID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<INLotSerialStatusByCostCenter.siteID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.siteID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INLotSerialStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.costCenterID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<INLotSerialStatusByCostCenter.qtyOnHand, IBqlDecimal>.IsGreater<decimal0>>>, INLotSerialStatusByCostCenter>.View((PXGraph) this.Base);
    if (!this.IsLocationEnabled && this.IsLotSerialRequired)
    {
      serialStatusCmd.Join<InnerJoin<INSiteLotSerial, On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteLotSerial.inventoryID, Equal<INLotSerialStatusByCostCenter.inventoryID>>>>, And<BqlOperand<INSiteLotSerial.siteID, IBqlInt>.IsEqual<INLotSerialStatusByCostCenter.siteID>>>>.And<BqlOperand<INSiteLotSerial.lotSerialNbr, IBqlString>.IsEqual<INLotSerialStatusByCostCenter.lotSerialNbr>>>>>();
      serialStatusCmd.WhereAnd<Where<BqlOperand<INSiteLotSerial.qtyHardAvail, IBqlDecimal>.IsGreater<decimal0>>>();
    }
    int? nullable = line.SubItemID;
    if (nullable.HasValue)
      serialStatusCmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.subItemID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.subItemID, IBqlInt>.FromCurrent>>>();
    nullable = line.LocationID;
    if (nullable.HasValue)
      serialStatusCmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.locationID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.locationID, IBqlInt>.FromCurrent>>>();
    else if (line.TranType == "TRX")
      serialStatusCmd.WhereAnd<Where<BqlOperand<INLocation.transfersValid, IBqlBool>.IsEqual<True>>>();
    else
      serialStatusCmd.WhereAnd<Where<BqlOperand<INLocation.salesValid, IBqlBool>.IsEqual<True>>>();
    switch (((PXResult) item).GetItem<INLotSerClass>().LotSerIssueMethod)
    {
      case "F":
        serialStatusCmd.OrderByNew<OrderBy<Asc<INLocation.pickPriority, Asc<INLotSerialStatusByCostCenter.receiptDate, Asc<INLotSerialStatusByCostCenter.lotSerialNbr>>>>>();
        break;
      case "L":
        serialStatusCmd.OrderByNew<OrderBy<Asc<INLocation.pickPriority, Desc<INLotSerialStatusByCostCenter.receiptDate, Asc<INLotSerialStatusByCostCenter.lotSerialNbr>>>>>();
        break;
      case "E":
        serialStatusCmd.OrderByNew<OrderBy<Asc<INLocation.pickPriority, Asc<INLotSerialStatusByCostCenter.expireDate, Asc<INLotSerialStatusByCostCenter.lotSerialNbr>>>>>();
        break;
      case "S":
        serialStatusCmd.OrderByNew<OrderBy<Asc<INLocation.pickPriority, Asc<INLotSerialStatusByCostCenter.lotSerialNbr>>>>();
        break;
      case "U":
        serialStatusCmd.WhereAnd<Where<BqlOperand<True, IBqlBool>.IsEqual<False>>>();
        break;
      default:
        throw new PXException();
    }
    return serialStatusCmd;
  }

  protected override void UpdateCounters(LSSelect.Counters counters, FSSODetSplit split)
  {
    base.UpdateCounters(counters, split);
    if (!split.POCreate.GetValueOrDefault())
      return;
    LSSelect.Counters counters1 = counters;
    Decimal baseQty = counters1.BaseQty;
    Decimal? nullable = split.BaseReceivedQty;
    Decimal num1 = nullable.Value;
    nullable = split.BaseShippedQty;
    Decimal num2 = nullable.Value;
    Decimal num3 = num1 + num2;
    counters1.BaseQty = baseQty - num3;
  }

  public override FSSODetSplit EnsureSplit(ILSMaster row)
  {
    FSSODetSplit fssoDetSplit = base.EnsureSplit(row);
    if (fssoDetSplit != null)
    {
      int? nullable = fssoDetSplit.InventoryID;
      if (nullable.HasValue)
      {
        nullable = fssoDetSplit.SubItemID;
        if (!nullable.HasValue)
          fssoDetSplit.SubItemID = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, fssoDetSplit.InventoryID).DefaultSubItemID;
      }
    }
    return fssoDetSplit;
  }
}
