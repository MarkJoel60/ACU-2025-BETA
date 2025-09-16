// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.BlanketLineSplitting
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO.DAC.Projections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

[PXProtectedAccess(typeof (SOOrderLineSplittingExtension))]
public abstract class BlanketLineSplitting : 
  PXGraphExtension<SOOrderLineSplittingAllocatedExtension, SOOrderLineSplittingExtension, SOOrderEntry>
{
  private bool _internalCall;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  [PXProtectedAccess(null)]
  protected abstract PXCache<PX.Objects.SO.SOLine> LineCache { get; }

  [PXProtectedAccess(null)]
  protected abstract PXCache<PX.Objects.SO.SOLineSplit> SplitCache { get; }

  [PXProtectedAccess(null)]
  protected abstract PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> ReadInventoryItem(
    int? inventoryID);

  [PXProtectedAccess(null)]
  protected abstract void SetSplitQtyWithLine(PX.Objects.SO.SOLineSplit split, PX.Objects.SO.SOLine line);

  [PXProtectedAccess(null)]
  protected abstract void SetLineQtyFromBase(PX.Objects.SO.SOLine line);

  [PXProtectedAccess(null)]
  protected abstract Dictionary<PX.Objects.SO.SOLine, LSSelect.Counters> LineCounters { get; }

  [PXProtectedAccess(null)]
  protected abstract PX.Objects.SO.SOLineSplit[] SelectSplitsReversed(PX.Objects.SO.SOLineSplit split);

  [PXProtectedAccess(typeof (SOOrderLineSplittingAllocatedExtension))]
  protected abstract void RefreshViewOf(PXCache cache);

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.schedOrderDate> e)
  {
    this.ForbidChangeIfMultipleSplits(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.schedShipDate> e)
  {
    this.ForbidChangeIfMultipleSplits(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOCreateDate> e)
  {
    this.ForbidChangeIfMultipleSplits(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.customerOrderNbr> e)
  {
    this.ForbidChangeIfMultipleSplits(e.Row);
  }

  protected virtual void ForbidChangeIfMultipleSplits(PX.Objects.SO.SOLine line)
  {
    if (!(line.Behavior == "BL"))
      return;
    if (((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).View.SelectMultiBound((object[]) new PX.Objects.SO.SOLine[1]
    {
      line
    }, Array.Empty<object>()).Count > 1)
      throw new PXSetPropertyException("The value in this column cannot be specified for a line with multiple splits in the Line Details dialog box.");
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOLine> e)
  {
    if (e.Row.Behavior != "BL" || this._internalCall)
      return;
    bool flag1 = !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOLine>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOLine.schedOrderDate>((object) e.OldRow, (object) e.Row);
    bool flag2 = !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOLine>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOLine.schedShipDate>((object) e.OldRow, (object) e.Row);
    bool flag3 = !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOLine>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOLine.pOCreateDate>((object) e.OldRow, (object) e.Row);
    bool flag4 = !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOLine>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOLine.customerOrderNbr>((object) e.OldRow, (object) e.Row);
    if (!(flag1 | flag2 | flag3 | flag4))
      return;
    this._internalCall = true;
    try
    {
      foreach (PX.Objects.SO.SOLineSplit soLineSplit in ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).View.SelectMultiBound((object[]) new PX.Objects.SO.SOLine[1]
      {
        e.Row
      }, Array.Empty<object>()))
      {
        if (flag1)
          soLineSplit.SchedOrderDate = e.Row.SchedOrderDate;
        if (flag2)
          soLineSplit.SchedShipDate = e.Row.SchedShipDate;
        if (flag3)
          soLineSplit.POCreateDate = e.Row.POCreateDate;
        if (flag4)
          soLineSplit.CustomerOrderNbr = e.Row.CustomerOrderNbr;
        ((PXSelectBase<PX.Objects.SO.SOLineSplit>) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Update(soLineSplit);
      }
    }
    finally
    {
      this._internalCall = false;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.customerOrderNbr> e)
  {
    if (!(e.Row.Behavior == "BL") || string.IsNullOrEmpty(((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current?.CustomerOrderNbr))
      return;
    bool flag = GraphHelper.RowCast<PX.Objects.SO.SOLineSplit>((IEnumerable) ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).View.SelectMultiBound((object[]) new PX.Objects.SO.SOLine[1]
    {
      ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current
    }, Array.Empty<object>())).Any<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s => s != e.Row));
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.customerOrderNbr>, PX.Objects.SO.SOLineSplit, object>) e).NewValue = flag ? (object) (string) null : (object) ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current.CustomerOrderNbr;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.qty> e)
  {
    if (e.Row.Behavior != "BL" || ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current == null || ((PXGraphExtension<SOOrderLineSplittingExtension, SOOrderEntry>) this).Base1.SuppressedMode)
      return;
    Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.qty>, PX.Objects.SO.SOLineSplit, object>) e).NewValue;
    Decimal? nullable1 = e.Row.Qty;
    nullable1 = newValue.HasValue & nullable1.HasValue ? new Decimal?(newValue.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable2 = ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current.UnassignedQty;
    if (!(nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue))
      return;
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.qty> fieldVerifying = e;
    nullable2 = e.Row.Qty;
    nullable1 = ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current.UnassignedQty;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) (nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?());
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.qty>, PX.Objects.SO.SOLineSplit, object>) fieldVerifying).NewValue = (object) local;
    ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLineSplit.qty>((object) e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.qty>, PX.Objects.SO.SOLineSplit, object>) e).NewValue, (Exception) new PXSetPropertyException("The total quantity of the item in the Line Details dialog box cannot differ from the quantity of the item in the sales order line."));
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.SO.SOLineSplit> e)
  {
    this.UpdateDatesFromSplitsToLine((PX.Objects.SO.SOLineSplit) null, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOLineSplit> e)
  {
    this.UpdateDatesFromSplitsToLine(e.OldRow, e.Row);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.SO.SOLineSplit> e)
  {
    this.UpdateDatesFromSplitsToLine(e.Row, (PX.Objects.SO.SOLineSplit) null);
  }

  protected virtual void UpdateDatesFromSplitsToLine(PX.Objects.SO.SOLineSplit oldRow, PX.Objects.SO.SOLineSplit newRow)
  {
    PX.Objects.SO.SOLineSplit soLineSplit = newRow ?? oldRow;
    if (soLineSplit.Behavior != "BL" || this._internalCall)
      return;
    PXCache cache = ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).Cache;
    bool flag1 = newRow == null || oldRow == null;
    bool flag2 = !cache.ObjectsEqual<PX.Objects.SO.SOLineSplit.schedOrderDate>((object) oldRow, (object) newRow);
    bool flag3 = !cache.ObjectsEqual<PX.Objects.SO.SOLine.schedShipDate>((object) oldRow, (object) newRow);
    bool flag4 = !cache.ObjectsEqual<PX.Objects.SO.SOLineSplit.pOCreateDate>((object) oldRow, (object) newRow);
    if (!(flag1 | flag2 | flag3 | flag4))
      return;
    PX.Objects.SO.SOLine soLine = PXParentAttribute.SelectParent<PX.Objects.SO.SOLine>(cache, (object) soLineSplit);
    if (soLine == null)
      return;
    List<object> objectList = ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.splits).View.SelectMultiBound((object[]) new PX.Objects.SO.SOLine[1]
    {
      soLine
    }, Array.Empty<object>());
    int count = objectList.Count;
    IEnumerable<PX.Objects.SO.SOLineSplit> source = GraphHelper.RowCast<PX.Objects.SO.SOLineSplit>((IEnumerable) objectList);
    PX.Objects.SO.SOLineSplit firstSplit = source.FirstOrDefault<PX.Objects.SO.SOLineSplit>();
    if (firstSplit != null & flag1)
    {
      string customerOrderNbr = count > 1 ? (string) null : firstSplit.CustomerOrderNbr;
      if (soLine.CustomerOrderNbr != customerOrderNbr)
      {
        ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache.SetValue<PX.Objects.SO.SOLine.customerOrderNbr>((object) soLine, (object) customerOrderNbr);
        GraphHelper.MarkUpdated(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache, (object) soLine, true);
      }
    }
    DateTime? nullable1;
    if (firstSplit != null && flag1 | flag2)
    {
      DateTime? nullable2 = source.All<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s =>
      {
        DateTime? schedOrderDate1 = s.SchedOrderDate;
        DateTime? schedOrderDate2 = firstSplit.SchedOrderDate;
        if (schedOrderDate1.HasValue != schedOrderDate2.HasValue)
          return false;
        return !schedOrderDate1.HasValue || schedOrderDate1.GetValueOrDefault() == schedOrderDate2.GetValueOrDefault();
      })) ? firstSplit.SchedOrderDate : new DateTime?();
      nullable1 = soLine.SchedOrderDate;
      DateTime? nullable3 = nullable2;
      if ((nullable1.HasValue == nullable3.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable3.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache.SetValue<PX.Objects.SO.SOLine.schedOrderDate>((object) soLine, (object) nullable2);
        GraphHelper.MarkUpdated(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache, (object) soLine, true);
      }
    }
    DateTime? nullable4;
    if (firstSplit != null && flag1 | flag3)
    {
      DateTime? nullable5 = source.All<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s =>
      {
        DateTime? schedShipDate1 = s.SchedShipDate;
        DateTime? schedShipDate2 = firstSplit.SchedShipDate;
        if (schedShipDate1.HasValue != schedShipDate2.HasValue)
          return false;
        return !schedShipDate1.HasValue || schedShipDate1.GetValueOrDefault() == schedShipDate2.GetValueOrDefault();
      })) ? firstSplit.SchedShipDate : new DateTime?();
      nullable4 = soLine.SchedShipDate;
      nullable1 = nullable5;
      if ((nullable4.HasValue == nullable1.HasValue ? (nullable4.HasValue ? (nullable4.GetValueOrDefault() != nullable1.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache.SetValue<PX.Objects.SO.SOLine.schedShipDate>((object) soLine, (object) nullable5);
        GraphHelper.MarkUpdated(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache, (object) soLine, true);
      }
    }
    if (firstSplit == null || !(flag1 | flag4))
      return;
    DateTime? nullable6;
    if (!source.All<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s =>
    {
      DateTime? poCreateDate1 = s.POCreateDate;
      DateTime? poCreateDate2 = firstSplit.POCreateDate;
      if (poCreateDate1.HasValue != poCreateDate2.HasValue)
        return false;
      return !poCreateDate1.HasValue || poCreateDate1.GetValueOrDefault() == poCreateDate2.GetValueOrDefault();
    })))
    {
      nullable1 = new DateTime?();
      nullable6 = nullable1;
    }
    else
      nullable6 = firstSplit.POCreateDate;
    DateTime? nullable7 = nullable6;
    nullable1 = soLine.POCreateDate;
    nullable4 = nullable7;
    if ((nullable1.HasValue == nullable4.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable4.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
      return;
    ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache.SetValue<PX.Objects.SO.SOLine.pOCreateDate>((object) soLine, (object) nullable7);
    GraphHelper.MarkUpdated(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache, (object) soLine, true);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOOrderLineSplittingAllocatedExtension.TruncateSchedules(PX.Objects.SO.SOLine,System.Decimal)" />
  /// </summary>
  [PXOverride]
  public virtual void TruncateSchedules(
    PX.Objects.SO.SOLine line,
    Decimal baseQty,
    HashSet<int?> lastUnshippedSchedules,
    Action<PX.Objects.SO.SOLine, Decimal, HashSet<int?>> base_TruncateSchedules)
  {
    if (line.Behavior != "BL")
    {
      base_TruncateSchedules(line, baseQty, lastUnshippedSchedules);
    }
    else
    {
      this.LineCounters.Remove(line);
      Decimal? nullable1 = line.UnassignedQty;
      Decimal num1 = 0M;
      if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue)
      {
        nullable1 = line.UnassignedQty;
        Decimal num2 = baseQty;
        if (nullable1.GetValueOrDefault() >= num2 & nullable1.HasValue)
        {
          PX.Objects.SO.SOLine soLine = line;
          nullable1 = soLine.UnassignedQty;
          Decimal num3 = baseQty;
          soLine.UnassignedQty = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num3) : new Decimal?();
          baseQty = 0M;
        }
        else
        {
          Decimal num4 = baseQty;
          nullable1 = line.UnassignedQty;
          Decimal num5 = nullable1.Value;
          baseQty = num4 - num5;
          line.UnassignedQty = new Decimal?(0M);
        }
      }
      foreach (PX.Objects.SO.SOLineSplit soLineSplit1 in (IEnumerable<PX.Objects.SO.SOLineSplit>) ((IEnumerable<PX.Objects.SO.SOLineSplit>) this.SelectSplitsReversed(PX.Objects.SO.SOLineSplit.FromSOLine(line))).OrderBy<PX.Objects.SO.SOLineSplit, bool>((Func<PX.Objects.SO.SOLineSplit, bool>) (split =>
      {
        int? childLineCntr = split.ChildLineCntr;
        int num9 = 0;
        return childLineCntr.GetValueOrDefault() > num9 & childLineCntr.HasValue;
      })))
      {
        nullable1 = soLineSplit1.BaseQty;
        Decimal? nullable2 = soLineSplit1.BaseQtyOnOrders;
        Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
        Decimal num6 = baseQty;
        nullable2 = nullable3;
        Decimal valueOrDefault = nullable2.GetValueOrDefault();
        if (num6 >= valueOrDefault & nullable2.HasValue)
        {
          int? childLineCntr = soLineSplit1.ChildLineCntr;
          int num7 = 0;
          if (childLineCntr.GetValueOrDefault() == num7 & childLineCntr.HasValue)
          {
            this.SplitCache.Delete(soLineSplit1);
            goto label_15;
          }
        }
        PX.Objects.SO.SOLineSplit copy = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(soLineSplit1);
        PX.Objects.SO.SOLineSplit soLineSplit2 = copy;
        nullable2 = soLineSplit2.BaseQty;
        Decimal num8 = Math.Min(nullable3.Value, baseQty);
        Decimal? nullable4;
        if (!nullable2.HasValue)
        {
          nullable1 = new Decimal?();
          nullable4 = nullable1;
        }
        else
          nullable4 = new Decimal?(nullable2.GetValueOrDefault() - num8);
        soLineSplit2.BaseQty = nullable4;
        this.SetSplitQtyWithLine(copy, line);
        this.SplitCache.Update(copy);
label_15:
        baseQty -= nullable3.Value;
        if (baseQty <= 0M)
          break;
      }
    }
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.LineSplittingExtension`4.SetUnassignedQty(`2,System.Decimal,System.Boolean)" />
  /// </summary>
  [PXOverride]
  public virtual void SetUnassignedQty(
    PX.Objects.SO.SOLine line,
    Decimal detailsBaseQty,
    bool allowNegative,
    Action<PX.Objects.SO.SOLine, Decimal, bool> base_SetUnassignedQty)
  {
    if (line.Behavior != "BL")
      base_SetUnassignedQty(line, detailsBaseQty, allowNegative);
    else
      line.UnassignedQty = new Decimal?(PXDBQuantityAttribute.Round(new Decimal?(line.BaseOpenQty.Value - detailsBaseQty)));
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOOrderLineSplittingAllocatedExtension.SchedulesEqual(PX.Objects.SO.SOLineSplit,PX.Objects.SO.SOLineSplit,PX.Data.PXDBOperation)" />
  /// </summary>
  [PXOverride]
  public virtual bool SchedulesEqual(
    PX.Objects.SO.SOLineSplit a,
    PX.Objects.SO.SOLineSplit b,
    PXDBOperation operation,
    Func<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit, PXDBOperation, bool> base_SchedulesEqual)
  {
    if (!(b.Behavior == "BL"))
      return base_SchedulesEqual(a, b, operation);
    if (operation != 2 || !base_SchedulesEqual(a, b, operation))
      return false;
    DateTime? nullable1 = a.SchedOrderDate;
    DateTime? nullable2 = b.SchedOrderDate;
    if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
    {
      nullable2 = a.SchedShipDate;
      nullable1 = b.SchedShipDate;
      if ((nullable2.HasValue == nullable1.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      {
        nullable1 = a.POCreateDate;
        nullable2 = b.POCreateDate;
        if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
          return a.CustomerOrderNbr == b.CustomerOrderNbr;
      }
    }
    return false;
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOOrderLineSplittingAllocatedExtension.AssignNewSplitFields(PX.Objects.SO.SOLineSplit,PX.Objects.SO.SOLine)" />
  /// </summary>
  [PXOverride]
  public virtual void AssignNewSplitFields(
    PX.Objects.SO.SOLineSplit split,
    PX.Objects.SO.SOLine line,
    Action<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine> base_AssignNewSplitFields)
  {
    base_AssignNewSplitFields(split, line);
    if (string.IsNullOrEmpty(line.BlanketNbr))
      return;
    BlanketSOLineSplit blanketSoLineSplit = PXParentAttribute.SelectParent<BlanketSOLineSplit>((PXCache) this.LineCache, (object) line);
    if (blanketSoLineSplit == null)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<BlanketSOLineSplit>((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base), new object[4]
      {
        (object) line.BlanketType,
        (object) line.BlanketNbr,
        (object) line.BlanketLineNbr,
        (object) line.BlanketSplitLineNbr
      });
    if (blanketSoLineSplit.IsAllocated.GetValueOrDefault())
      split.IsAllocated = new bool?(true);
    split.POType = blanketSoLineSplit.POType;
    split.PONbr = blanketSoLineSplit.PONbr;
    split.POLineNbr = blanketSoLineSplit.POLineNbr;
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOOrderLineSplittingExtension.UpdateCounters(PX.Objects.IN.LSSelect.Counters,PX.Objects.SO.SOLineSplit)" />
  /// </summary>
  [PXOverride]
  public virtual void UpdateCounters(
    LSSelect.Counters counters,
    PX.Objects.SO.SOLineSplit split,
    Action<LSSelect.Counters, PX.Objects.SO.SOLineSplit> base_UpdateCounters)
  {
    base_UpdateCounters(counters, split);
    if (!(split.Behavior == "BL"))
      return;
    bool? nullable = split.POCreate;
    if (nullable.GetValueOrDefault())
      return;
    nullable = split.AMProdCreate;
    if (nullable.GetValueOrDefault())
      return;
    counters.BaseQty -= split.BaseShippedQty.Value;
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOOrderLineSplittingAllocatedExtension.ShouldUncompleteSchedule(PX.Objects.SO.SOLine,PX.Objects.SO.SOLineSplit)" />
  /// </summary>
  [PXOverride]
  public virtual bool ShouldUncompleteSchedule(
    PX.Objects.SO.SOLine line,
    PX.Objects.SO.SOLineSplit split,
    Func<PX.Objects.SO.SOLine, PX.Objects.SO.SOLineSplit, bool> base_ShouldUncompleteSchedule)
  {
    if (!(split.Behavior == "BL"))
      return base_ShouldUncompleteSchedule(line, split);
    if (split.LineType == "MI")
      return true;
    Decimal? shippedQty = split.ShippedQty;
    Decimal? qty = split.Qty;
    return shippedQty.GetValueOrDefault() < qty.GetValueOrDefault() & shippedQty.HasValue & qty.HasValue;
  }

  [PXOverride]
  public virtual void EventHandler(
    AbstractEvents.IRowUpdated<PX.Objects.SO.SOLineSplit> e,
    Action<AbstractEvents.IRowUpdated<PX.Objects.SO.SOLineSplit>> base_EventHandler)
  {
    base_EventHandler(e);
    if (!(e.Row.Behavior == "BL") || ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current == null || ((PXGraphExtension<SOOrderLineSplittingExtension, SOOrderEntry>) this).Base1.SuppressedMode || !e.ExternalCall)
      return;
    Decimal? qty = e.Row.Qty;
    Decimal? nullable1 = e.OldRow.Qty;
    Decimal? nullable2 = qty.HasValue & nullable1.HasValue ? new Decimal?(qty.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    nullable1 = nullable2;
    Decimal num1 = 0M;
    if (!(nullable1.GetValueOrDefault() < num1 & nullable1.HasValue))
      return;
    nullable1 = ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current.UnassignedQty;
    Decimal num2 = 0M;
    if (!(nullable1.GetValueOrDefault() > num2 & nullable1.HasValue))
      return;
    PX.Objects.SO.SOLineSplit split = (PX.Objects.SO.SOLineSplit) ((PXCache) this.SplitCache).Insert();
    bool? isAllocated = e.OldRow.IsAllocated;
    if (isAllocated.GetValueOrDefault())
    {
      isAllocated = e.Row.IsAllocated;
      if (isAllocated.GetValueOrDefault())
        split.IsAllocated = new bool?(true);
    }
    split.BaseQty = ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current.UnassignedQty;
    this.SetSplitQtyWithLine(split, ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current);
    nullable1 = split.Qty;
    Decimal num3 = -nullable2.Value;
    if (nullable1.GetValueOrDefault() > num3 & nullable1.HasValue)
      split.Qty = new Decimal?(-nullable2.Value);
    this.SplitCache.Update(split);
    this.RefreshViewOf((PXCache) this.SplitCache);
  }
}
