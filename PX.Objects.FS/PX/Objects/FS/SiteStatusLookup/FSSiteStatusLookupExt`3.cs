// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SiteStatusLookup.FSSiteStatusLookupExt`3
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CM;
using PX.Objects.Extensions.AddItemLookup;
using PX.Objects.IN;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.FS.SiteStatusLookup;

public abstract class FSSiteStatusLookupExt<TGraph, TDocument, TLine> : 
  SiteStatusLookupExt<TGraph, TDocument, TLine, FSSiteStatusSelected, FSSiteStatusFilter>
  where TGraph : PXGraph, ISiteStatusLookupCompatible
  where TDocument : class, IBqlTable, new()
  where TLine : class, IBqlTable, new()
{
  protected override bool LineShallBeAdded(FSSiteStatusSelected line)
  {
    if (!line.Selected.GetValueOrDefault())
      return false;
    Decimal? qtySelected = line.QtySelected;
    Decimal num1 = 0M;
    if (qtySelected.GetValueOrDefault() > num1 & qtySelected.HasValue)
      return true;
    int? durationSelected = line.DurationSelected;
    int num2 = 0;
    return durationSelected.GetValueOrDefault() > num2 & durationSelected.HasValue;
  }

  protected override IEnumerable AddSelectedItemsHandler(PXAdapter adapter)
  {
    ((PXCache) GraphHelper.Caches<TLine>((PXGraph) this.Base)).ForceExceptionHandling = true;
    foreach (FSSiteStatusSelected line in ((PXSelectBase) this.ItemInfo).Cache.Cached)
    {
      if (this.LineShallBeAdded(line))
        this.CreateNewLine(line);
    }
    ((PXSelectBase) this.ItemInfo).Cache.Clear();
    return adapter.Get();
  }

  protected override void OnSelectedUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!this.Base.IsMobile)
      return;
    base.OnSelectedUpdated(sender, e);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<FSSiteStatusSelected> e)
  {
    if (((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<FSSiteStatusSelected>>) e).Cache.Fields.Contains(typeof (FSSiteStatusSelected.curyID).Name) && ((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<FSSiteStatusSelected>>) e).Cache.GetValue<FSSiteStatusSelected.curyID>((object) e.Row) == null)
    {
      PXCache cach = ((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<FSSiteStatusSelected>>) e).Cache.Graph.Caches[typeof (FSServiceOrder)];
      ((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<FSSiteStatusSelected>>) e).Cache.SetValue<FSSiteStatusSelected.curyID>((object) e.Row, cach.GetValue<FSServiceOrder.curyID>(cach.Current));
      ((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<FSSiteStatusSelected>>) e).Cache.SetValue<FSSiteStatusSelected.curyInfoID>((object) e.Row, cach.GetValue<FSServiceOrder.curyInfoID>(cach.Current));
    }
    if (!(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<FSSiteStatusSelected>>) e).Cache.Graph is RouteServiceContractScheduleEntry) && !(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<FSSiteStatusSelected>>) e).Cache.Graph is ServiceContractScheduleEntry))
      return;
    PX.Objects.CM.Currency baseCurrency = CurrencyCollection.GetBaseCurrency();
    if (baseCurrency == null)
      return;
    ((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<FSSiteStatusSelected>>) e).Cache.SetValue<FSSiteStatusSelected.curyID>((object) e.Row, (object) baseCurrency.CuryID);
    ((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<FSSiteStatusSelected>>) e).Cache.SetValue<FSSiteStatusSelected.curyInfoID>((object) e.Row, (object) baseCurrency.CuryInfoID);
  }

  protected override void _(PX.Data.Events.RowSelected<FSSiteStatusSelected> e)
  {
    base._(e);
    PXUIFieldAttribute.SetEnabled<FSSiteStatusSelected.durationSelected>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSiteStatusSelected>>) e).Cache, (object) e.Row, true);
  }

  protected virtual void _(PX.Data.Events.RowInserted<FSSiteStatusFilter> e)
  {
    if (e.Row == null)
      return;
    bool flag = this.Base.InventoryItemsAreIncluded();
    e.Row.IncludeIN = new bool?(flag);
    if (!flag)
      e.Row.OnlyAvailable = new bool?(false);
    DateTime? nullable1 = e.Row.HistoryDate;
    if (nullable1.HasValue)
      return;
    FSSiteStatusFilter row = e.Row;
    nullable1 = ((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<FSSiteStatusFilter>>) e).Cache.Graph.Accessinfo.BusinessDate;
    DateTime? nullable2 = new DateTime?(nullable1.GetValueOrDefault().AddMonths(-3));
    row.HistoryDate = nullable2;
  }

  protected override void _(PX.Data.Events.RowSelected<FSSiteStatusFilter> e)
  {
    base._(e);
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible<FSSiteStatusFilter.historyDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSiteStatusFilter>>) e).Cache, (object) null, e.Row.Mode.GetValueOrDefault() == 1);
    PXCache cach = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSiteStatusFilter>>) e).Cache.Graph.Caches[typeof (FSSiteStatusSelected)];
    int? mode = e.Row.Mode;
    PXUIFieldAttribute.SetVisible<FSSiteStatusSelected.qtyLastSale>(cach, (object) null, (mode.GetValueOrDefault() == 1 ? 1 : 0) != 0);
    mode = e.Row.Mode;
    PXUIFieldAttribute.SetVisible<FSSiteStatusSelected.curyID>(cach, (object) null, (mode.GetValueOrDefault() == 1 ? 1 : 0) != 0);
    mode = e.Row.Mode;
    PXUIFieldAttribute.SetVisible<FSSiteStatusSelected.curyUnitPrice>(cach, (object) null, (mode.GetValueOrDefault() == 1 ? 1 : 0) != 0);
    mode = e.Row.Mode;
    PXUIFieldAttribute.SetVisible<FSSiteStatusSelected.lastSalesDate>(cach, (object) null, (mode.GetValueOrDefault() == 1 ? 1 : 0) != 0);
    bool includeIN = this.Base.InventoryItemsAreIncluded();
    bool flag = e.Row.LineType == "NSTKI" || e.Row.LineType == "SERVI";
    PXUIFieldAttribute.SetVisible<INSiteStatusFilter.inventory_Wildcard>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSiteStatusFilter>>) e).Cache, (object) e.Row, includeIN);
    PXUIFieldAttribute.SetVisible<FSSiteStatusFilter.mode>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSiteStatusFilter>>) e).Cache, (object) e.Row, includeIN);
    PXUIFieldAttribute.SetVisible<INSiteStatusFilter.barCode>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSiteStatusFilter>>) e).Cache, (object) e.Row, includeIN);
    PXUIFieldAttribute.SetVisible<INSiteStatusFilter.barCodeWildcard>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSiteStatusFilter>>) e).Cache, (object) e.Row, includeIN);
    PXUIFieldAttribute.SetVisible<FSSiteStatusFilter.siteID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSiteStatusFilter>>) e).Cache, (object) e.Row, includeIN);
    PXUIFieldAttribute.SetVisible<FSSiteStatusFilter.onlyAvailable>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSiteStatusFilter>>) e).Cache, (object) e.Row, includeIN && !flag);
    FSLineType.SetLineTypeList<FSSiteStatusFilter.lineType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<FSSiteStatusFilter>>) e).Cache, (object) e.Row, includeIN, false, false, false, true);
  }
}
