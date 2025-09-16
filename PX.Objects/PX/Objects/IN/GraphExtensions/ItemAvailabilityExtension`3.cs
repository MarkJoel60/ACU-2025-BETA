// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common;
using PX.Objects.Common.Exceptions;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction;
using System;
using System.Collections.Generic;
using System.Globalization;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class ItemAvailabilityExtension<TGraph, TLine, TSplit> : PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TLine : class, IBqlTable, ILSPrimary, new()
  where TSplit : class, IBqlTable, ILSDetail, new()
{
  private PXCache<TLine> _lineCache;
  private PXCache<TSplit> _splitCache;
  protected int _detailsRequested;

  public PXCache<TLine> LineCache
  {
    get => this._lineCache ?? (this._lineCache = GraphHelper.Caches<TLine>((PXGraph) this.Base));
  }

  public PXCache<TSplit> SplitCache
  {
    get => this._splitCache ?? (this._splitCache = GraphHelper.Caches<TSplit>((PXGraph) this.Base));
  }

  protected abstract TSplit EnsureSplit(ILSMaster row);

  protected abstract string GetStatus(TLine line);

  protected abstract Decimal GetUnitRate(TLine line);

  protected abstract void RaiseQtyExceptionHandling(
    TLine line,
    PXExceptionInfo ei,
    Decimal? newValue);

  protected abstract void RaiseQtyExceptionHandling(
    TSplit split,
    PXExceptionInfo ei,
    Decimal? newValue);

  public virtual void Initialize()
  {
    this.AddStatusField();
    ItemPlanHelper<TGraph>.AddStatusDACsToCacheMapping((PXGraph) this.Base);
  }

  public (string Name, string DisplayName) StatusField { get; protected set; } = ("Availability", "Availability");

  protected virtual void AddStatusField()
  {
    this.StatusField = ("Availability", PXMessages.LocalizeNoPrefix("Availability"));
    ((PXCache) this.LineCache).Fields.Add(this.StatusField.Name);
    ((FieldSelectingEvents) this.Base.FieldSelecting).AddAbstractHandler<TLine>(this.StatusField.Name, new Action<AbstractEvents.IFieldSelecting<TLine, IBqlField>>(this.EventHandlerStatusField));
  }

  protected virtual void EventHandlerStatusField(AbstractEvents.IFieldSelecting<TLine, IBqlField> e)
  {
    int? nullable1;
    if ((object) e.Row != null)
    {
      nullable1 = ((ILSMaster) e.Row).InventoryID;
      if (nullable1.HasValue)
      {
        nullable1 = ((ILSMaster) e.Row).SiteID;
        if (nullable1.HasValue && !PXLongOperation.Exists((PXGraph) this.Base))
        {
          e.ReturnValue = (object) this.GetStatus(e.Row);
          goto label_5;
        }
      }
    }
    e.ReturnValue = (object) string.Empty;
label_5:
    object returnState = e.ReturnState;
    int? nullable2 = new int?((int) byte.MaxValue);
    bool? nullable3 = new bool?();
    string name = this.StatusField.Name;
    bool? nullable4 = new bool?(false);
    nullable1 = new int?();
    int? nullable5 = nullable1;
    bool? nullable6 = new bool?();
    PXFieldState instance = PXStringState.CreateInstance(returnState, nullable2, nullable3, name, nullable4, nullable5, (string) null, (string[]) null, (string[]) null, nullable6, (string) null, (string[]) null);
    instance.Visible = false;
    instance.Visibility = (PXUIVisibility) 1;
    instance.DisplayName = this.StatusField.DisplayName;
    e.ReturnState = (object) instance;
  }

  public virtual void Check(ILSMaster row, int? costCenterID)
  {
    if (row == null)
      return;
    short? invtMult = row.InvtMult;
    if ((invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() != -1)
      return;
    Decimal? baseQty = row.BaseQty;
    Decimal num = 0M;
    if (!(baseQty.GetValueOrDefault() > num & baseQty.HasValue))
      return;
    IStatus availability = this.FetchWithBaseUOM(row, true, costCenterID);
    this.Check(row, availability);
  }

  protected virtual void Check(ILSMaster row, IStatus availability)
  {
    foreach (PXExceptionInfo checkError in this.GetCheckErrors(row, availability))
      this.RaiseQtyExceptionHandling(row, checkError, row.Qty);
  }

  protected virtual void RaiseQtyExceptionHandling(
    ILSMaster row,
    PXExceptionInfo ei,
    Decimal? newValue)
  {
    switch (row)
    {
      case TLine line:
        this.RaiseQtyExceptionHandling(line, ei, newValue);
        break;
      case TSplit split:
        this.RaiseQtyExceptionHandling(split, ei, newValue);
        break;
    }
  }

  public virtual IEnumerable<PXExceptionInfo> GetCheckErrors(ILSMaster row, int? costCenterID)
  {
    if (row != null)
    {
      short? invtMult = row.InvtMult;
      if ((invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() == -1)
      {
        Decimal? baseQty = row.BaseQty;
        Decimal num = 0M;
        if (baseQty.GetValueOrDefault() > num & baseQty.HasValue)
        {
          IStatus availability = this.FetchWithBaseUOM(row, true, costCenterID);
          return this.GetCheckErrors(row, availability);
        }
      }
    }
    return (IEnumerable<PXExceptionInfo>) Array.Empty<PXExceptionInfo>();
  }

  protected virtual IEnumerable<PXExceptionInfo> GetCheckErrors(ILSMaster row, IStatus availability)
  {
    if (!this.IsAvailableQty(row, availability))
    {
      string errorMessageQtyAvail = this.GetErrorMessageQtyAvail(this.GetStatusLevel(availability));
      if (errorMessageQtyAvail != null)
        yield return new PXExceptionInfo((PXErrorLevel) 2, errorMessageQtyAvail, Array.Empty<object>());
    }
  }

  protected virtual bool IsAvailableQty(ILSMaster row, IStatus availability)
  {
    short? invtMult = row.InvtMult;
    if ((invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() == -1)
    {
      Decimal? nullable = row.BaseQty;
      Decimal num1 = 0M;
      if (nullable.GetValueOrDefault() > num1 & nullable.HasValue && availability != null)
      {
        nullable = availability.QtyNotAvail;
        Decimal num2 = 0M;
        if (nullable.GetValueOrDefault() < num2 & nullable.HasValue)
        {
          Decimal? qtyAvail = availability.QtyAvail;
          Decimal? qtyNotAvail = availability.QtyNotAvail;
          nullable = qtyAvail.HasValue & qtyNotAvail.HasValue ? new Decimal?(qtyAvail.GetValueOrDefault() + qtyNotAvail.GetValueOrDefault()) : new Decimal?();
          Decimal num3 = 0M;
          if (nullable.GetValueOrDefault() < num3 & nullable.HasValue)
            return false;
        }
      }
    }
    return true;
  }

  protected virtual string GetErrorMessageQtyAvail(
    ItemAvailabilityExtension<TGraph, TLine, TSplit>.StatusLevel level)
  {
    switch (level)
    {
      case ItemAvailabilityExtension<TGraph, TLine, TSplit>.StatusLevel.Site:
        return "The available quantity of the {0} {1} item is not sufficient in the {2} warehouse.";
      case ItemAvailabilityExtension<TGraph, TLine, TSplit>.StatusLevel.Location:
        return "Updating data for item '{0} {1}' on warehouse '{2} {3}' will result in negative available quantity.";
      case ItemAvailabilityExtension<TGraph, TLine, TSplit>.StatusLevel.LotSerial:
        return "Updating item '{0} {1}' in warehouse '{2} {3}' lot/serial number '{4}' quantity available will go negative.";
      default:
        throw new ArgumentOutOfRangeException(nameof (level));
    }
  }

  protected virtual string GetErrorMessageQtyOnHand(
    ItemAvailabilityExtension<TGraph, TLine, TSplit>.StatusLevel level)
  {
    switch (level)
    {
      case ItemAvailabilityExtension<TGraph, TLine, TSplit>.StatusLevel.Site:
        return "Updating item '{0} {1}' in warehouse '{2}' quantity on hand will go negative.";
      case ItemAvailabilityExtension<TGraph, TLine, TSplit>.StatusLevel.Location:
        return "Updating item '{0} {1}' in warehouse '{2} {3}' quantity on hand will go negative.";
      case ItemAvailabilityExtension<TGraph, TLine, TSplit>.StatusLevel.LotSerial:
        return "Updating item '{0} {1}' in warehouse '{2} {3}' lot/serial number '{4}' quantity on hand will go negative.";
      default:
        throw new ArgumentOutOfRangeException(nameof (level));
    }
  }

  protected virtual ItemAvailabilityExtension<TGraph, TLine, TSplit>.StatusLevel GetStatusLevel(
    IStatus availability)
  {
    switch (availability)
    {
      case PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter _:
        return ItemAvailabilityExtension<TGraph, TLine, TSplit>.StatusLevel.LotSerial;
      case LocationStatusByCostCenter _:
        return ItemAvailabilityExtension<TGraph, TLine, TSplit>.StatusLevel.Location;
      case SiteStatusByCostCenter _:
        return ItemAvailabilityExtension<TGraph, TLine, TSplit>.StatusLevel.Site;
      default:
        throw new ArgumentOutOfRangeException(nameof (availability));
    }
  }

  public bool IsFetching { get; protected set; }

  public IStatus FetchWithLineUOM(TLine line, bool excludeCurrent, int? costCenterID)
  {
    IStatus it = this.FetchWithBaseUOM((ILSMaster) line, excludeCurrent, costCenterID);
    return it != null ? it.Multiply<IStatus>(this.GetUnitRate(line)) : (IStatus) null;
  }

  public virtual IStatus FetchWithBaseUOM(ILSMaster row, bool excludeCurrent, int? costCenterID)
  {
    if (row == null)
      return (IStatus) null;
    try
    {
      this.IsFetching = true;
      return this.Fetch((ILSDetail) this.EnsureSplit(row), excludeCurrent, costCenterID);
    }
    finally
    {
      this.IsFetching = false;
    }
  }

  protected virtual IStatus Fetch(ILSDetail split, bool excludeCurrent, int? costCenterID)
  {
    if (split == null || !((ILSMaster) split).InventoryID.HasValue || !split.SubItemID.HasValue || !((ILSMaster) split).SiteID.HasValue)
      return (IStatus) null;
    INLotSerClass inLotSerClass = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, ((ILSMaster) split).InventoryID).With<PX.Objects.IN.InventoryItem, INLotSerClass>((Func<PX.Objects.IN.InventoryItem, INLotSerClass>) (ii => !ii.StkItem.GetValueOrDefault() ? (INLotSerClass) null : INLotSerClass.PK.Find((PXGraph) this.Base, ii.LotSerClassID)));
    if (inLotSerClass == null || inLotSerClass.LotSerTrack == null)
      return (IStatus) null;
    if (this._detailsRequested++ == this.DetailsCountToEnableOptimization)
      this.Optimize();
    if (!split.LocationID.HasValue)
      return this.FetchSite(split, excludeCurrent, costCenterID);
    return !string.IsNullOrEmpty(split.LotSerialNbr) && (string.IsNullOrEmpty(split.AssignedNbr) || !INLotSerialNbrAttribute.StringsEqual(split.AssignedNbr, split.LotSerialNbr)) && inLotSerClass.LotSerAssign == "R" ? this.FetchLotSerial(split, excludeCurrent, costCenterID) : this.FetchLocation(split, excludeCurrent, costCenterID);
  }

  protected virtual IStatus FetchLotSerial(ILSDetail split, bool excludeCurrent, int? costCenterID)
  {
    using (new DisableSelectorValidationScope((PXCache) GraphHelper.Caches<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter>((PXGraph) this.Base), new Type[1]
    {
      typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter.siteID)
    }))
    {
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter row = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter();
      row.InventoryID = ((ILSMaster) split).InventoryID;
      row.SubItemID = split.SubItemID;
      row.SiteID = ((ILSMaster) split).SiteID;
      row.LocationID = split.LocationID;
      row.LotSerialNbr = split.LotSerialNbr;
      row.CostCenterID = costCenterID;
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter statusByCostCenter = this.InitializeRecord<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter>(row);
      INLotSerialStatusByCostCenter existing = INLotSerialStatusByCostCenter.PK.Find((PXGraph) this.Base, ((ILSMaster) split).InventoryID, split.SubItemID, ((ILSMaster) split).SiteID, split.LocationID, split.LotSerialNbr, costCenterID);
      return this.Fetch<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter>(split, (IStatus) PXCache<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter>.CreateCopy(statusByCostCenter), (IStatus) existing, excludeCurrent);
    }
  }

  protected virtual IStatus FetchLocation(ILSDetail split, bool excludeCurrent, int? costCenterID)
  {
    using (new DisableSelectorValidationScope((PXCache) GraphHelper.Caches<LocationStatusByCostCenter>((PXGraph) this.Base), new Type[1]
    {
      typeof (LocationStatusByCostCenter.siteID)
    }))
    {
      LocationStatusByCostCenter row = new LocationStatusByCostCenter();
      row.InventoryID = ((ILSMaster) split).InventoryID;
      row.SubItemID = split.SubItemID;
      row.SiteID = ((ILSMaster) split).SiteID;
      row.LocationID = split.LocationID;
      row.CostCenterID = costCenterID;
      LocationStatusByCostCenter statusByCostCenter = this.InitializeRecord<LocationStatusByCostCenter>(row);
      INLocationStatusByCostCenter existing = INLocationStatusByCostCenter.PK.Find((PXGraph) this.Base, ((ILSMaster) split).InventoryID, split.SubItemID, ((ILSMaster) split).SiteID, split.LocationID, costCenterID);
      return this.Fetch<LocationStatusByCostCenter>(split, (IStatus) PXCache<LocationStatusByCostCenter>.CreateCopy(statusByCostCenter), (IStatus) existing, excludeCurrent);
    }
  }

  protected virtual IStatus FetchSite(ILSDetail split, bool excludeCurrent, int? costCenterID)
  {
    using (new DisableSelectorValidationScope((PXCache) GraphHelper.Caches<SiteStatusByCostCenter>((PXGraph) this.Base), new Type[1]
    {
      typeof (SiteStatusByCostCenter.siteID)
    }))
    {
      SiteStatusByCostCenter row = new SiteStatusByCostCenter();
      row.InventoryID = ((ILSMaster) split).InventoryID;
      row.SubItemID = split.SubItemID;
      row.SiteID = ((ILSMaster) split).SiteID;
      row.CostCenterID = costCenterID;
      SiteStatusByCostCenter statusByCostCenter = this.InitializeRecord<SiteStatusByCostCenter>(row);
      INSiteStatusByCostCenter existing = INSiteStatusByCostCenter.PK.Find((PXGraph) this.Base, ((ILSMaster) split).InventoryID, split.SubItemID, ((ILSMaster) split).SiteID, costCenterID);
      return this.Fetch<SiteStatusByCostCenter>(split, (IStatus) PXCache<SiteStatusByCostCenter>.CreateCopy(statusByCostCenter), (IStatus) existing, excludeCurrent);
    }
  }

  protected virtual IStatus Fetch<TQtyAllocated>(
    ILSDetail split,
    IStatus allocated,
    IStatus existing,
    bool excludeCurrent)
    where TQtyAllocated : class, IQtyAllocated, IBqlTable, new()
  {
    this.Summarize(allocated, existing);
    if (excludeCurrent)
    {
      AvailabilitySigns availabilitySigns = this.GetAvailabilitySigns<TQtyAllocated>((TSplit) split);
      this.ExcludeCurrent(split, allocated, availabilitySigns);
    }
    return allocated;
  }

  public virtual AvailabilitySigns GetAvailabilitySigns<TStatus>(TSplit split) where TStatus : class, IQtyAllocatedBase, IBqlTable, new()
  {
    return this.Base.FindImplementation<IItemPlanHandler<TSplit>>()?.GetAvailabilitySigns<TStatus>(split) ?? new AvailabilitySigns();
  }

  protected virtual void Summarize(IStatus allocated, IStatus existing)
  {
    allocated.Add<IStatus>(existing);
  }

  protected virtual void ExcludeCurrent(
    ILSDetail currentSplit,
    IStatus allocated,
    AvailabilitySigns signs)
  {
    Decimal? nullable1;
    Decimal? nullable2;
    if (Sign.op_Inequality(signs.SignQtyAvail, Sign.Zero))
    {
      IStatus status1 = allocated;
      nullable1 = status1.QtyAvail;
      Sign signQtyAvail1 = signs.SignQtyAvail;
      nullable2 = currentSplit.BaseQty;
      Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
      Decimal num1 = Sign.op_Multiply(signQtyAvail1, valueOrDefault1);
      Decimal? nullable3;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable3 = nullable2;
      }
      else
        nullable3 = new Decimal?(nullable1.GetValueOrDefault() - num1);
      status1.QtyAvail = nullable3;
      IStatus status2 = allocated;
      nullable1 = status2.QtyNotAvail;
      Sign signQtyAvail2 = signs.SignQtyAvail;
      nullable2 = currentSplit.BaseQty;
      Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
      Decimal num2 = Sign.op_Multiply(signQtyAvail2, valueOrDefault2);
      Decimal? nullable4;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable4 = nullable2;
      }
      else
        nullable4 = new Decimal?(nullable1.GetValueOrDefault() + num2);
      status2.QtyNotAvail = nullable4;
    }
    if (Sign.op_Inequality(signs.SignQtyHardAvail, Sign.Zero))
    {
      IStatus status = allocated;
      nullable1 = status.QtyHardAvail;
      Sign signQtyHardAvail = signs.SignQtyHardAvail;
      nullable2 = currentSplit.BaseQty;
      Decimal valueOrDefault = nullable2.GetValueOrDefault();
      Decimal num = Sign.op_Multiply(signQtyHardAvail, valueOrDefault);
      Decimal? nullable5;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable5 = nullable2;
      }
      else
        nullable5 = new Decimal?(nullable1.GetValueOrDefault() - num);
      status.QtyHardAvail = nullable5;
    }
    if (!Sign.op_Inequality(signs.SignQtyActual, Sign.Zero))
      return;
    IStatus status3 = allocated;
    nullable1 = status3.QtyActual;
    Sign signQtyActual = signs.SignQtyActual;
    nullable2 = currentSplit.BaseQty;
    Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
    Decimal num3 = Sign.op_Multiply(signQtyActual, valueOrDefault3);
    Decimal? nullable6;
    if (!nullable1.HasValue)
    {
      nullable2 = new Decimal?();
      nullable6 = nullable2;
    }
    else
      nullable6 = new Decimal?(nullable1.GetValueOrDefault() - num3);
    status3.QtyActual = nullable6;
  }

  protected T InitializeRecord<T>(T row) where T : class, IBqlTable, new()
  {
    // ISSUE: method pointer
    this.Base.RowInserted.AddHandler<T>(new PXRowInserted((object) null, __methodptr(\u003CInitializeRecord\u003Eg__CleanUpOnInsert\u007C41_0<T>)));
    try
    {
      return PXCache<T>.Insert((PXGraph) this.Base, row);
    }
    finally
    {
      // ISSUE: method pointer
      this.Base.RowInserted.RemoveHandler<T>(new PXRowInserted((object) null, __methodptr(\u003CInitializeRecord\u003Eg__CleanUpOnInsert\u007C41_0<T>)));
    }
  }

  protected Decimal GetUnitRate<TInventoryID, TUOM>(TLine line)
    where TInventoryID : IBqlField
    where TUOM : IBqlField
  {
    return INUnitAttribute.ConvertFromBase<TInventoryID, TUOM>((PXCache) this.LineCache, (object) line, 1M, INPrecision.NOROUND);
  }

  protected virtual string FormatQty(Decimal? value)
  {
    return value?.ToString("N" + CommonSetupDecPl.Qty.ToString(), (IFormatProvider) NumberFormatInfo.CurrentInfo) ?? string.Empty;
  }

  protected virtual int DetailsCountToEnableOptimization => 5;

  public bool IsOptimizationEnabled
  {
    get => this._detailsRequested > this.DetailsCountToEnableOptimization;
  }

  protected virtual void Optimize()
  {
  }

  public enum StatusLevel
  {
    Site,
    Location,
    LotSerial,
  }
}
