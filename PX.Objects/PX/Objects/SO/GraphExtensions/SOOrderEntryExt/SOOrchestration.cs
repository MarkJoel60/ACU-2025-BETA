// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOOrchestration
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO.Models.OrderOrchestration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class SOOrchestration : PXGraphExtension<SOOrderEntry>
{
  [PXCopyPasteHiddenView]
  public PXFilter<SOOrderOrchestrationSettings> OrderOrchestrationSettingsView;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<SOOrderOrchestrationSummaryLine, TypeArrayOf<IFbqlJoin>.Empty>, SOOrderOrchestrationSummaryLine>.View OrderOrchestrationSummariesView;
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<SOOrderOrchestrationLine, TypeArrayOf<IFbqlJoin>.Empty>, SOOrderOrchestrationLine>.View OrderOrchestrationDetailLineView;
  public PXAction<PX.Objects.SO.SOOrder> OrchestrateOrder;

  protected virtual int LimitNumberOfWarehousesInAlgorithm => 15;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.orderOrchestration>();

  public virtual IEnumerable orderOrchestrationDetailLineView()
  {
    IEnumerable<SOOrderOrchestrationLine> source = GraphHelper.Caches<SOOrderOrchestrationLine>((PXGraph) this.Base).Rows.Inserted;
    SOOrderOrchestrationSummaryLine currentSummaryLine = ((PXSelectBase) this.OrderOrchestrationSummariesView).Cache.Current as SOOrderOrchestrationSummaryLine;
    if (currentSummaryLine != null)
      source = source.Where<SOOrderOrchestrationLine>((Func<SOOrderOrchestrationLine, bool>) (i =>
      {
        int? inventoryId1 = i.InventoryID;
        int? inventoryId2 = currentSummaryLine.InventoryID;
        if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
        {
          int? orderLineNbr1 = i.OrderLineNbr;
          int? orderLineNbr2 = currentSummaryLine.OrderLineNbr;
          if (orderLineNbr1.GetValueOrDefault() == orderLineNbr2.GetValueOrDefault() & orderLineNbr1.HasValue == orderLineNbr2.HasValue)
          {
            bool? nullable = i.IsSplitLine;
            if (nullable.GetValueOrDefault())
              return true;
            nullable = i.IsAllocated;
            return nullable.GetValueOrDefault();
          }
        }
        return false;
      }));
    return (IEnumerable) source;
  }

  public virtual IEnumerable orderOrchestrationSummariesView()
  {
    IEnumerable<SOOrderOrchestrationSummaryLine> inserted = GraphHelper.Caches<SOOrderOrchestrationSummaryLine>((PXGraph) this.Base).Rows.Inserted;
    if (inserted == null || !inserted.Any<SOOrderOrchestrationSummaryLine>())
    {
      this.ComputeOrchestrationLines(true);
      this.ComputeOrchestrationSummaryLines();
      inserted = GraphHelper.Caches<SOOrderOrchestrationSummaryLine>((PXGraph) this.Base).Rows.Inserted;
    }
    return (IEnumerable) inserted.Where<SOOrderOrchestrationSummaryLine>((Func<SOOrderOrchestrationSummaryLine, bool>) (line => line.LineNbr.HasValue && line.OrderLineNbr.HasValue));
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable orchestrateOrder(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SOOrchestration.\u003C\u003Ec__DisplayClass10_0 cDisplayClass100 = new SOOrchestration.\u003C\u003Ec__DisplayClass10_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass100.list = adapter.Get<PX.Objects.SO.SOOrder>().ToList<PX.Objects.SO.SOOrder>();
    if (((PXSelectBase) this.OrderOrchestrationSettingsView).View.Answer != 1)
    {
      ((PXCache) GraphHelper.Caches<SOOrderOrchestrationLine>((PXGraph) this.Base)).Clear();
      ((PXCache) GraphHelper.Caches<SOOrderOrchestrationSummaryLine>((PXGraph) this.Base)).Clear();
      if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current != null)
      {
        ((PXSelectBase<SOOrderOrchestrationSettings>) this.OrderOrchestrationSettingsView).Current.OrchestrationStrategy = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrchestrationStrategy;
        ((PXSelectBase<SOOrderOrchestrationSettings>) this.OrderOrchestrationSettingsView).Current.LimitWarehouse = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.LimitWarehouse;
        ((PXSelectBase<SOOrderOrchestrationSettings>) this.OrderOrchestrationSettingsView).Current.NumberOfWarehouses = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.NumberOfWarehouses;
        ((PXSelectBase<SOOrderOrchestrationSettings>) this.OrderOrchestrationSettingsView).Current.ShippingZoneID = this.FindAltWarehouseZoneID();
      }
    }
    ((PXAction) this.Base.Save).Press();
    if (!adapter.MassProcess)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      SOOrchestration.\u003C\u003Ec__DisplayClass10_1 cDisplayClass101 = new SOOrchestration.\u003C\u003Ec__DisplayClass10_1();
      if (GraphHelper.RowCast<PX.Objects.SO.SOLine>((IEnumerable) ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>())).AsEnumerable<PX.Objects.SO.SOLine>().Where<PX.Objects.SO.SOLine>((Func<PX.Objects.SO.SOLine, bool>) (l => l.IsOrchestratedLine.GetValueOrDefault())).Any<PX.Objects.SO.SOLine>() && ((PXSelectBase) ((PXGraph) this.Base).GetExtension<SOOrchestration>().OrderOrchestrationSettingsView).View.Answer != 1 && ((PXSelectBase) this.Base.Transactions).View.Ask((object) "OrchestrateOrder", "Warning", "Some lines have already been orchestrated. Re-orchestration will be applied only to lines that are not marked as orchestrated.", (MessageButtons) 1, (MessageIcon) 3) != 1)
        return adapter.Get();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass101.orderEntryGraph = GraphHelper.Clone<SOOrderEntry>(this.Base);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass101.orderEntryGraphExt = ((PXGraph) cDisplayClass101.orderEntryGraph).GetExtension<SOOrchestration>();
      // ISSUE: reference to a compiler-generated field
      if (((PXSelectBase) cDisplayClass101.orderEntryGraphExt.OrderOrchestrationSettingsView).View.Answer == 1)
      {
        // ISSUE: method pointer
        PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass101, __methodptr(\u003CorchestrateOrder\u003Eb__2)));
      }
      else
      {
        // ISSUE: method pointer
        PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass101, __methodptr(\u003CorchestrateOrder\u003Eb__3)));
      }
    }
    else
    {
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass100, __methodptr(\u003CorchestrateOrder\u003Eb__0)));
    }
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass100.list;
  }

  public void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.isOrchestrationAllowed> e)
  {
    if (e.Row?.OrderType == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOOrder, PX.Objects.SO.SOOrder.isOrchestrationAllowed>, PX.Objects.SO.SOOrder, object>) e).NewValue = (object) (bool) ((int) e.Row.IsOrchestrationAllowed ?? (this.GetOrchestrationAllowedStatus(e.Row.OrderType) ? 1 : 0));
  }

  protected virtual bool GetOrchestrationAllowedStatus(string orderType)
  {
    return ((IQueryable<PXResult<SOOrderTypeOperation>>) PXSelectBase<SOOrderTypeOperation, PXViewOf<SOOrderTypeOperation>.BasedOn<SelectFromBase<SOOrderTypeOperation, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOOrderType>.On<SOOrderTypeOperation.FK.OrderType>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrderTypeOperation.orderType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<SOOrderTypeOperation.operation, IBqlString>.IsEqual<SOOperation.issue>>>, And<BqlOperand<SOOrderTypeOperation.active, IBqlBool>.IsEqual<True>>>, And<BqlOperand<SOOrderTypeOperation.iNDocType, IBqlString>.IsIn<INTranType.issue, INTranType.invoice>>>, And<BqlOperand<PX.Objects.SO.SOOrderType.requireShipping, IBqlBool>.IsEqual<True>>>, And<BqlOperand<PX.Objects.SO.SOOrderType.behavior, IBqlString>.IsIn<SOBehavior.rM, SOBehavior.sO>>>>.And<BqlOperand<PX.Objects.SO.SOOrderType.defaultOperation, IBqlString>.IsEqual<SOOperation.issue>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) orderType
    })).Any<PXResult<SOOrderTypeOperation>>();
  }

  public void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder> e)
  {
    if (e.Row == null)
      return;
    bool isOrchestrationAllowed = e.Row.IsOrchestrationAllowed.GetValueOrDefault();
    int? openSiteCntr = e.Row.OpenSiteCntr;
    int num = 0;
    ((PXAction) this.OrchestrateOrder).SetEnabled(openSiteCntr.GetValueOrDefault() > num & openSiteCntr.HasValue & ((e.Row.OrchestrationStrategy == null || !(e.Row.OrchestrationStrategy != "NA") || !(e.Row.OrchestrationStatus != "PO") ? 0 : (((PXSelectBase) this.Base.Document).Cache.GetStatus((object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current) != 2 ? 1 : 0)) & (isOrchestrationAllowed ? 1 : 0)) != 0);
    bool isOrderOrchestrationAllowed = isOrchestrationAllowed && !e.Row.WillCall.GetValueOrDefault();
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) e).Cache, (object) e.Row);
    attributeAdjuster.For<PX.Objects.SO.SOOrder.orchestrationStatus>((Action<PXUIFieldAttribute>) (a => a.Visible = isOrchestrationAllowed));
    attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) e).Cache, (object) e.Row);
    attributeAdjuster.For<PX.Objects.SO.SOOrder.orchestrationStrategy>((Action<PXUIFieldAttribute>) (a =>
    {
      a.Enabled = isOrderOrchestrationAllowed;
      a.Visible = isOrchestrationAllowed;
    }));
    bool isOrderOrchestrationEnabled = isOrderOrchestrationAllowed && e.Row.OrchestrationStrategy != "NA";
    attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) e).Cache, (object) e.Row);
    attributeAdjuster.For<PX.Objects.SO.SOOrder.limitWarehouse>((Action<PXUIFieldAttribute>) (a =>
    {
      a.Enabled = isOrderOrchestrationEnabled;
      a.Visible = isOrchestrationAllowed;
    }));
    attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder>>) e).Cache, (object) e.Row);
    attributeAdjuster.For<PX.Objects.SO.SOOrder.numberOfWarehouses>((Action<PXUIFieldAttribute>) (a =>
    {
      a.Enabled = isOrderOrchestrationEnabled && e.Row.LimitWarehouse.GetValueOrDefault();
      a.Visible = isOrchestrationAllowed;
    }));
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventoryID> e)
  {
    this.ResetLineOrchestration(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventoryID>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.uOM> e)
  {
    this.ResetLineOrchestration(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.uOM>>) e).Cache, e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.orderQty> e)
  {
    this.ResetLineOrchestration(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.orderQty>>) e).Cache, e.Row);
  }

  protected virtual void ResetLineOrchestration(PXCache cache, PX.Objects.SO.SOLine line)
  {
    if (!line.IsOrchestratedLine.GetValueOrDefault())
      return;
    cache.SetValueExt<PX.Objects.SO.SOLine.isOrchestratedLine>((object) line, (object) false);
    cache.SetValueExt<PX.Objects.SO.SOLine.orchestrationPlanID>((object) line, (object) null);
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXGraph) this.Base).OnBeforePersist += new Action<PXGraph>(this.BeforePersist);
  }

  protected virtual void BeforePersist(PXGraph obj)
  {
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    if (current == null)
      return;
    if (!current.IsOrchestrationAllowed.GetValueOrDefault() && current.OrchestrationStatus != "NA")
    {
      current.OrchestrationStatus = "NA";
      ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Update(current);
    }
    else
    {
      if (current.OrchestrationStatus == "NW")
        return;
      Lazy<ICollection<PX.Objects.SO.SOLine>> lazy = new Lazy<ICollection<PX.Objects.SO.SOLine>>((Func<ICollection<PX.Objects.SO.SOLine>>) (() => this.GetLinesToExclude()));
      bool flag = GraphHelper.Caches<PX.Objects.SO.SOLine>((PXGraph) this.Base).Rows.Inserted.Union<PX.Objects.SO.SOLine>(GraphHelper.Caches<PX.Objects.SO.SOLine>((PXGraph) this.Base).Rows.Updated).Where<PX.Objects.SO.SOLine>((Func<PX.Objects.SO.SOLine, bool>) (line => !this.IsRestrictedForOrchestration(line))).Except<PX.Objects.SO.SOLine>((IEnumerable<PX.Objects.SO.SOLine>) lazy.Value, GraphHelper.Caches<PX.Objects.SO.SOLine>((PXGraph) this.Base).GetKeyComparer<PX.Objects.SO.SOLine>()).Any<PX.Objects.SO.SOLine>();
      if (!(current.IsOrchestrationAllowed.GetValueOrDefault() & flag))
        return;
      current.OrchestrationStatus = "NW";
      ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Update(current);
    }
  }

  protected virtual void ValidateOrderForOrchestration()
  {
    if (!((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.IsOrchestrationAllowed.GetValueOrDefault())
      throw new PXException("Order orchestration is available only for order types with the SO or RMA automation behavior, the Process Shipments check box selected, and the active Issue operation on the Order Types (SO201000) form.");
  }

  protected virtual int ComputeOrchestrationLines(bool silent = false)
  {
    int orchestrationLines1 = 0;
    ((PXCache) GraphHelper.Caches<SOOrderOrchestrationLine>((PXGraph) this.Base)).Clear();
    ((PXCache) GraphHelper.Caches<SOOrderOrchestrationSummaryLine>((PXGraph) this.Base)).Clear();
    List<SOOrderOrchestrationLine> orchestrationLines2 = this.GetOrchestrationLines(silent);
    if (orchestrationLines2.Count == 0)
      return orchestrationLines1;
    foreach (SOOrderOrchestrationLine orchestrationLine in orchestrationLines2)
    {
      orchestrationLine.RecordID = new int?(++orchestrationLines1);
      ((PXSelectBase<SOOrderOrchestrationLine>) this.OrderOrchestrationDetailLineView).Insert(orchestrationLine);
    }
    ((PXSelectBase) this.OrderOrchestrationDetailLineView).Cache.IsDirty = false;
    return orchestrationLines1;
  }

  protected virtual void ComputeOrchestrationSummaryLines()
  {
    List<SOOrderOrchestrationLine> list1 = GraphHelper.Caches<SOOrderOrchestrationLine>((PXGraph) this.Base).Rows.Inserted.ToList<SOOrderOrchestrationLine>();
    if (list1.Count == 0)
      return;
    int num1 = 0;
    foreach (SOOrderOrchestrationLine orchestrationLine in EnumerableExtensions.Distinct<SOOrderOrchestrationLine, int?>(list1.Where<SOOrderOrchestrationLine>((Func<SOOrderOrchestrationLine, bool>) (r =>
    {
      int? sitePriority = r.SitePriority;
      int num2 = 0;
      if (!(sitePriority.GetValueOrDefault() == num2 & sitePriority.HasValue))
        return false;
      bool? isSplitLine = r.IsSplitLine;
      bool flag = false;
      return isSplitLine.GetValueOrDefault() == flag & isSplitLine.HasValue;
    })), (Func<SOOrderOrchestrationLine, int?>) (w => w.OrderLineNbr)))
    {
      SOOrderOrchestrationLine result = orchestrationLine;
      List<SOOrderOrchestrationLine> list2 = list1.Where<SOOrderOrchestrationLine>((Func<SOOrderOrchestrationLine, bool>) (r =>
      {
        int? orderLineNbr1 = r.OrderLineNbr;
        int? orderLineNbr2 = result.OrderLineNbr;
        if (!(orderLineNbr1.GetValueOrDefault() == orderLineNbr2.GetValueOrDefault() & orderLineNbr1.HasValue == orderLineNbr2.HasValue))
          return false;
        bool? nullable = r.IsSplitLine;
        if (nullable.GetValueOrDefault())
          return true;
        nullable = r.IsAllocated;
        return nullable.GetValueOrDefault();
      })).ToList<SOOrderOrchestrationLine>();
      ((PXSelectBase<SOOrderOrchestrationSummaryLine>) this.OrderOrchestrationSummariesView).Insert(new SOOrderOrchestrationSummaryLine()
      {
        LineNbr = new int?(++num1),
        OrderLineNbr = result.OrderLineNbr,
        InventoryID = result.InventoryID,
        InventoryDescr = result.InventoryDescr,
        SiteCD = result.AltSiteCD,
        SiteDescr = result.AltSiteDescr,
        BaseUOM = result.BaseUOM,
        SalesUOM = result.SalesUOM,
        OrderQty = result.OrderQty,
        SplitQty = list2.Where<SOOrderOrchestrationLine>((Func<SOOrderOrchestrationLine, bool>) (bl =>
        {
          bool? isAllocated = bl.IsAllocated;
          bool flag = false;
          return isAllocated.GetValueOrDefault() == flag & isAllocated.HasValue;
        })).Sum<SOOrderOrchestrationLine>((Func<SOOrderOrchestrationLine, Decimal?>) (bl => bl.LineQty)),
        Splits = new int?(list2.Count),
        SplitDetails = string.Concat(list2.Select<SOOrderOrchestrationLine, string>((Func<SOOrderOrchestrationLine, string>) (bl => $"{bl.AltSiteCD} : {bl.LineQty.ToFormattedString()}, "))).TrimEnd(',', ' ')
      });
    }
    ((PXSelectBase) this.OrderOrchestrationSummariesView).Cache.IsDirty = false;
  }

  protected virtual List<SOOrderOrchestrationLine> GetOrchestrationLines(bool silent)
  {
    List<OrderInventoryInfo> orderInventoryInfo = this.GetOrderInventoryInfo();
    if (!orderInventoryInfo.Where<OrderInventoryInfo>((Func<OrderInventoryInfo, bool>) (i => !i.IsAllocated)).Any<OrderInventoryInfo>())
    {
      PXTrace.WriteInformation("No order lines with open unallocated quantities have been found for the orchestration.");
      return new List<SOOrderOrchestrationLine>();
    }
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current;
    List<WarehouseInfo> warehouseInfoList = !current.OrchestrationStrategy.Equals("DP") ? this.GetWarehouseInfo(orderInventoryInfo, silent) : this.GetWarehouseInfoByZone(orderInventoryInfo, silent);
    if (warehouseInfoList.Any<WarehouseInfo>() && orderInventoryInfo.Any<OrderInventoryInfo>())
    {
      if (current.LimitWarehouse.GetValueOrDefault())
      {
        int? numberOfWarehouses = current.NumberOfWarehouses;
        int num = 0;
        if (numberOfWarehouses.GetValueOrDefault() == num & numberOfWarehouses.HasValue)
          goto label_5;
      }
      List<List<int>> warehouseCombinations = this.GetWarehouseCombinations(warehouseInfoList);
      List<WarehouseInfo> singleWarehouse = this.FindSingleWarehouse(warehouseCombinations, warehouseInfoList, orderInventoryInfo);
      if (singleWarehouse.Any<WarehouseInfo>())
        return this.AssignAltWarehouses(singleWarehouse);
      List<WarehouseInfo> warehouses = this.FindWarehouses(warehouseCombinations, warehouseInfoList, orderInventoryInfo, current.NumberOfWarehouses);
      if (warehouses.Count != 0)
        return this.AssignAltWarehouses(warehouses);
      if (!silent)
        throw new PXException("The order cannot be orchestrated due to insufficient stock across all available warehouses.");
      PXTrace.WriteInformation("The order cannot be orchestrated due to insufficient stock across all available warehouses.");
      return new List<SOOrderOrchestrationLine>();
    }
label_5:
    return new List<SOOrderOrchestrationLine>();
  }

  protected virtual List<SOOrderOrchestrationLine> AssignAltWarehouses(
    List<WarehouseInfo> warehouseInfo)
  {
    List<SOOrderOrchestrationLine> source1 = new List<SOOrderOrchestrationLine>();
    if (!warehouseInfo.Any<WarehouseInfo>())
      return source1;
    foreach (PX.Objects.SO.SOLine availableSoLine in this.GetAvailableSOLines())
    {
      PX.Objects.SO.SOLine soLine = availableSoLine;
      IEnumerable<PX.Objects.SO.SOLineSplit> source2 = this.SelectSplits(soLine);
      Decimal? nullable1 = source2.Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (i =>
      {
        bool? isAllocated = i.IsAllocated;
        bool flag = false;
        return isAllocated.GetValueOrDefault() == flag & isAllocated.HasValue;
      })).Sum<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, Decimal?>) (i => i.BaseQty));
      if (nullable1.HasValue)
      {
        Decimal? nullable2 = nullable1;
        Decimal num1 = 0M;
        if (!(nullable2.GetValueOrDefault() <= num1 & nullable2.HasValue))
        {
          INUnit unit = INUnit.UK.ByInventory.Find((PXGraph) this.Base, soLine.InventoryID, soLine.UOM);
          foreach (PX.Objects.SO.SOLineSplit soLineSplit in source2.Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (l => l.IsAllocated.GetValueOrDefault())))
            source1.Add(new SOOrderOrchestrationLine()
            {
              OrderLineNbr = soLine.LineNbr,
              InventoryID = soLine.InventoryID,
              InventoryDescr = soLine.TranDesc,
              OrderQty = soLine.OpenQty,
              LineQty = soLineSplit.OpenQty,
              AltSiteID = soLineSplit.SiteID,
              SitePriority = new int?(0),
              IsSplitLine = new bool?(false),
              SalesUOM = soLine.UOM,
              IsAllocated = new bool?(true)
            });
          Decimal? nullable3 = nullable1;
          foreach (WarehouseInfo warehouseInfo1 in warehouseInfo)
          {
            Decimal? nullable4 = nullable3;
            Decimal num2 = 0M;
            if (!(nullable4.GetValueOrDefault() <= num2 & nullable4.HasValue))
            {
              WarehouseInventoryDetails inventoryDetails1 = warehouseInfo1.InventoryDetails.Where<WarehouseInventoryDetails>((Func<WarehouseInventoryDetails, bool>) (x =>
              {
                int? inventoryId1 = x.InventoryID;
                int? inventoryId2 = soLine.InventoryID;
                return inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue;
              })).FirstOrDefault<WarehouseInventoryDetails>();
              if (inventoryDetails1 != null)
              {
                nullable4 = inventoryDetails1.QtyHardAvail;
                Decimal num3 = 0M;
                if (!(nullable4.GetValueOrDefault() <= num3 & nullable4.HasValue))
                {
                  Decimal? nullable5;
                  if (inventoryDetails1.MaintainSafetyStock.GetValueOrDefault())
                  {
                    nullable4 = inventoryDetails1.SafetyStock;
                    nullable5 = inventoryDetails1.QtyHardAvail;
                    if (nullable4.GetValueOrDefault() >= nullable5.GetValueOrDefault() & nullable4.HasValue & nullable5.HasValue)
                      continue;
                  }
                  bool? maintainSafetyStock = inventoryDetails1.MaintainSafetyStock;
                  bool flag = false;
                  Decimal? nullable6;
                  if (!(maintainSafetyStock.GetValueOrDefault() == flag & maintainSafetyStock.HasValue))
                  {
                    nullable5 = inventoryDetails1.QtyHardAvail;
                    nullable4 = inventoryDetails1.SafetyStock;
                    nullable6 = nullable5.HasValue & nullable4.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
                  }
                  else
                    nullable6 = inventoryDetails1.QtyHardAvail;
                  Decimal? nullable7 = nullable6;
                  nullable4 = nullable7;
                  nullable5 = nullable3;
                  Decimal? nullable8;
                  if (!(nullable4.GetValueOrDefault() >= nullable5.GetValueOrDefault() & nullable4.HasValue & nullable5.HasValue))
                  {
                    nullable5 = nullable7;
                    Decimal num4 = 0M;
                    nullable8 = nullable5.GetValueOrDefault() <= num4 & nullable5.HasValue ? new Decimal?(0M) : nullable7;
                  }
                  else
                    nullable8 = nullable3;
                  Decimal? newQty = nullable8;
                  nullable5 = newQty;
                  Decimal num5 = 0M;
                  if (!(nullable5.GetValueOrDefault() <= num5 & nullable5.HasValue))
                  {
                    source1.Add(new SOOrderOrchestrationLine()
                    {
                      OrderLineNbr = soLine.LineNbr,
                      InventoryID = soLine.InventoryID,
                      InventoryDescr = soLine.TranDesc,
                      OrderQty = soLine.OpenQty,
                      LineQty = new Decimal?(INUnitAttribute.ConvertValue(newQty.Value, unit, INPrecision.QUANTITY, true)),
                      AltSiteID = warehouseInfo1.SiteID,
                      SitePriority = warehouseInfo1.SitePriority,
                      IsSplitLine = new bool?(true),
                      SalesUOM = soLine.UOM,
                      IsAllocated = new bool?(false),
                      PlanID = warehouseInfo1.PlanID
                    });
                    nullable5 = nullable3;
                    nullable4 = newQty;
                    nullable3 = nullable5.HasValue & nullable4.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
                    EnumerableExtensions.ForEach<WarehouseInventoryDetails>(warehouseInfo1.InventoryDetails.Where<WarehouseInventoryDetails>((Func<WarehouseInventoryDetails, bool>) (i =>
                    {
                      int? inventoryId3 = i.InventoryID;
                      int? inventoryId4 = soLine.InventoryID;
                      return inventoryId3.GetValueOrDefault() == inventoryId4.GetValueOrDefault() & inventoryId3.HasValue == inventoryId4.HasValue;
                    })), (Action<WarehouseInventoryDetails>) (i =>
                    {
                      WarehouseInventoryDetails inventoryDetails2 = i;
                      Decimal? qtyHardAvail = inventoryDetails2.QtyHardAvail;
                      Decimal? nullable9 = newQty;
                      inventoryDetails2.QtyHardAvail = qtyHardAvail.HasValue & nullable9.HasValue ? new Decimal?(qtyHardAvail.GetValueOrDefault() - nullable9.GetValueOrDefault()) : new Decimal?();
                    }));
                    nullable4 = nullable3;
                    Decimal num6 = 0M;
                    if (nullable4.GetValueOrDefault() <= num6 & nullable4.HasValue)
                      break;
                  }
                }
              }
            }
            else
              break;
          }
          List<SOOrderOrchestrationLine> orchestrationLineList = source1;
          SOOrderOrchestrationLine orchestrationLine = new SOOrderOrchestrationLine();
          orchestrationLine.OrderLineNbr = soLine.LineNbr;
          orchestrationLine.InventoryID = soLine.InventoryID;
          orchestrationLine.InventoryDescr = soLine.TranDesc;
          orchestrationLine.OrderQty = soLine.OpenQty;
          Decimal? baseQty = soLine.BaseQty;
          Decimal? nullable10 = nullable1;
          Decimal? nullable11 = baseQty.HasValue & nullable10.HasValue ? new Decimal?(baseQty.GetValueOrDefault() - nullable10.GetValueOrDefault()) : new Decimal?();
          Decimal? nullable12 = nullable3;
          orchestrationLine.LineQty = new Decimal?(INUnitAttribute.ConvertValue((nullable11.HasValue & nullable12.HasValue ? new Decimal?(nullable11.GetValueOrDefault() + nullable12.GetValueOrDefault()) : new Decimal?()).Value, unit, INPrecision.QUANTITY, true));
          orchestrationLine.AltSiteID = soLine.SiteID;
          orchestrationLine.SitePriority = new int?(0);
          orchestrationLine.IsSplitLine = new bool?(false);
          orchestrationLine.SalesUOM = soLine.UOM;
          orchestrationLine.IsAllocated = new bool?(false);
          orchestrationLineList.Insert(0, orchestrationLine);
        }
      }
    }
    return source1.OrderBy<SOOrderOrchestrationLine, int?>((Func<SOOrderOrchestrationLine, int?>) (line => line.OrderLineNbr)).ToList<SOOrderOrchestrationLine>();
  }

  protected virtual List<OrderInventoryInfo> GetOrderInventoryInfo()
  {
    IEnumerable<PX.Objects.SO.SOLine> availableSoLines = this.GetAvailableSOLines();
    List<OrderInventoryInfo> source = new List<OrderInventoryInfo>();
    if (availableSoLines == null || !availableSoLines.Any<PX.Objects.SO.SOLine>())
      return source;
    foreach (PX.Objects.SO.SOLine soLine1 in availableSoLines)
    {
      PX.Objects.SO.SOLine soLine = soLine1;
      string baseUnit = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, soLine.InventoryID)?.BaseUnit;
      Decimal? nullable1 = this.SelectSplits(soLine).Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (i => i.IsAllocated.GetValueOrDefault())).Sum<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, Decimal?>) (i => i.BaseQty));
      Decimal? nullable2 = nullable1;
      Decimal num1 = 0M;
      if (nullable2.GetValueOrDefault() > num1 & nullable2.HasValue)
        source.Add(new OrderInventoryInfo()
        {
          InventoryID = soLine.InventoryID,
          LineQty = nullable1,
          BaseUOM = baseUnit,
          IsAllocated = true
        });
      nullable2 = soLine.BaseQty;
      Decimal? nullable3 = nullable1;
      Decimal? unAllocatedQty = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
      if (unAllocatedQty.HasValue)
      {
        nullable3 = unAllocatedQty;
        Decimal num2 = 0M;
        if (!(nullable3.GetValueOrDefault() <= num2 & nullable3.HasValue))
        {
          if (source.Exists((Predicate<OrderInventoryInfo>) (i =>
          {
            int? inventoryId3 = i.InventoryID;
            int? inventoryId4 = soLine.InventoryID;
            return inventoryId3.GetValueOrDefault() == inventoryId4.GetValueOrDefault() & inventoryId3.HasValue == inventoryId4.HasValue && !i.IsAllocated;
          })))
            EnumerableExtensions.ForEach<OrderInventoryInfo>(source.Where<OrderInventoryInfo>((Func<OrderInventoryInfo, bool>) (i =>
            {
              int? inventoryId1 = i.InventoryID;
              int? inventoryId2 = soLine.InventoryID;
              return inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue;
            })), (Action<OrderInventoryInfo>) (i =>
            {
              OrderInventoryInfo orderInventoryInfo = i;
              Decimal? lineQty = orderInventoryInfo.LineQty;
              Decimal? nullable4 = unAllocatedQty;
              orderInventoryInfo.LineQty = lineQty.HasValue & nullable4.HasValue ? new Decimal?(lineQty.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            }));
          else
            source.Add(new OrderInventoryInfo()
            {
              InventoryID = soLine.InventoryID,
              LineQty = unAllocatedQty,
              BaseUOM = baseUnit,
              IsAllocated = false
            });
        }
      }
    }
    return source;
  }

  protected virtual void Orchestrate()
  {
    Decimal num1 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderTotal.Value;
    foreach (SOOrderOrchestrationSummaryLine orchestrationSummaryLine in (IEnumerable<SOOrderOrchestrationSummaryLine>) GraphHelper.RowCast<SOOrderOrchestrationSummaryLine>((IEnumerable) ((IEnumerable<PXResult<SOOrderOrchestrationSummaryLine>>) ((PXSelectBase<SOOrderOrchestrationSummaryLine>) this.OrderOrchestrationSummariesView).Select(Array.Empty<object>())).AsEnumerable<PXResult<SOOrderOrchestrationSummaryLine>>()).OrderBy<SOOrderOrchestrationSummaryLine, int?>((Func<SOOrderOrchestrationSummaryLine, int?>) (o => o.LineNbr)))
    {
      SOOrderOrchestrationSummaryLine summaryLine = orchestrationSummaryLine;
      this.OrchestrateOrderLine(EnumerableEx.Select<PX.Objects.SO.SOLine>((IEnumerable) this.GetAvailableSOLines()).Where<PX.Objects.SO.SOLine>((Func<PX.Objects.SO.SOLine, bool>) (l =>
      {
        int? lineNbr = l.LineNbr;
        int? orderLineNbr = summaryLine.OrderLineNbr;
        return lineNbr.GetValueOrDefault() == orderLineNbr.GetValueOrDefault() & lineNbr.HasValue == orderLineNbr.HasValue;
      })).FirstOrDefault<PX.Objects.SO.SOLine>());
    }
    Decimal num2 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderTotal.Value;
    if (num1 != num2)
    {
      PXTrace.WriteInformation("The order total after the {0} orchestration does not match the {1} order total before the orchestration.", new object[2]
      {
        (object) num2,
        (object) num1
      });
      throw new PXException("The orchestration is canceled because the calculated order total differs from the order total before the orchestration. Possible reasons include rounding differences in line taxes or discounts, manually changed discounts or prices, or quantity tiers defined in the price lists for the items.");
    }
    ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrchestrationStatus = "PO";
    ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).UpdateCurrent();
    ((PXAction) this.Base.Save).Press();
  }

  protected virtual void OrchestrateFromTransactions()
  {
    if (this.ComputeOrchestrationLines() == 0)
      throw new PXException("No lines have been found for order orchestration. Check Trace for more details.", new object[2]
      {
        (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderType,
        (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderNbr
      });
    IEnumerable<PX.Objects.SO.SOLine> availableSoLines = this.GetAvailableSOLines();
    Decimal num1 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderTotal.Value;
    foreach (PX.Objects.SO.SOLine soLine in availableSoLines)
      this.OrchestrateOrderLine(soLine);
    Decimal num2 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderTotal.Value;
    if (num1 != num2)
      throw new PXException("The orchestration is canceled because the calculated order total differs from the order total before the orchestration. Possible reasons include rounding differences in line taxes or discounts, manually changed discounts or prices, or quantity tiers defined in the price lists for the items.");
    ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrchestrationStatus = "PO";
    ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).UpdateCurrent();
    ((PXAction) this.Base.Save).Press();
  }

  protected virtual void OrchestrateOrderLine(PX.Objects.SO.SOLine soLine)
  {
    List<SOOrderOrchestrationLine> list = GraphHelper.Caches<SOOrderOrchestrationLine>((PXGraph) this.Base).Rows.Inserted.Where<SOOrderOrchestrationLine>((Func<SOOrderOrchestrationLine, bool>) (l =>
    {
      int? orderLineNbr = l.OrderLineNbr;
      int? lineNbr = soLine.LineNbr;
      return orderLineNbr.GetValueOrDefault() == lineNbr.GetValueOrDefault() & orderLineNbr.HasValue == lineNbr.HasValue;
    })).OrderBy<SOOrderOrchestrationLine, int?>((Func<SOOrderOrchestrationLine, int?>) (l => l.RecordID)).ToList<SOOrderOrchestrationLine>();
    if (!list.Any<SOOrderOrchestrationLine>())
      return;
    Decimal? nullable1 = this.SelectSplits(soLine).Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (i =>
    {
      bool? isAllocated = i.IsAllocated;
      bool flag = false;
      return isAllocated.GetValueOrDefault() == flag & isAllocated.HasValue;
    })).Sum<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, Decimal?>) (i => i.Qty));
    Decimal? nullable2 = nullable1;
    Decimal num1 = 0M;
    if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
      return;
    Decimal? nullable3 = nullable1;
    Decimal? openQty = soLine.OpenQty;
    foreach (SOOrderOrchestrationLine orchestrationLine in list)
    {
      bool? isSplitLine = orchestrationLine.IsSplitLine;
      bool flag = false;
      if (!(isSplitLine.GetValueOrDefault() == flag & isSplitLine.HasValue))
      {
        PX.Objects.SO.SOLine newLine = PXCache<PX.Objects.SO.SOLine>.CreateCopy(soLine);
        newLine.OrderType = (string) null;
        newLine.OrderNbr = (string) null;
        newLine.LineNbr = new int?();
        newLine.Qty = new Decimal?(0M);
        newLine.BaseQty = new Decimal?(0M);
        newLine.OpenQty = new Decimal?(0M);
        newLine.ShippedQty = new Decimal?(0M);
        newLine.ClosedQty = new Decimal?(0M);
        newLine.BlanketType = (string) null;
        newLine.BlanketNbr = (string) null;
        newLine.BlanketLineNbr = new int?();
        newLine.BlanketSplitLineNbr = new int?();
        newLine.QtyOnOrders = new Decimal?(0M);
        newLine.BaseQtyOnOrders = new Decimal?(0M);
        newLine.ChildLineCntr = new int?(0);
        newLine.OpenChildLineCntr = new int?(0);
        newLine.SiteID = orchestrationLine.AltSiteID;
        newLine = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Insert(newLine);
        newLine.OrderQty = orchestrationLine.LineQty;
        newLine = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(newLine);
        newLine.IsOrchestratedLine = new bool?(true);
        newLine.OrchestrationOriginalLineNbr = soLine.LineNbr;
        newLine.OrchestrationOriginalSiteID = soLine.OrchestrationOriginalSiteID ?? soLine.SiteID;
        newLine.OrchestrationPlanID = orchestrationLine.PlanID;
        newLine = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(newLine);
        EnumerableExtensions.ForEach<PX.Objects.SO.SOLineSplit>(GraphHelper.Caches<PX.Objects.SO.SOLineSplit>((PXGraph) this.Base).Rows.Inserted.Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s =>
        {
          int? lineNbr1 = s.LineNbr;
          int? lineNbr2 = newLine.LineNbr;
          return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
        })), (Action<PX.Objects.SO.SOLineSplit>) (split =>
        {
          split.IsAllocated = new bool?(true);
          split.IsOrchestratedLine = new bool?(true);
          ((PXSelectBase<PX.Objects.SO.SOLineSplit>) this.Base.splits).Update(split);
        }));
        nullable2 = nullable3;
        Decimal? lineQty = orchestrationLine.LineQty;
        nullable3 = nullable2.HasValue & lineQty.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - lineQty.GetValueOrDefault()) : new Decimal?();
      }
    }
    Decimal? nullable4 = nullable3;
    Decimal num2 = 0M;
    Decimal? nullable5;
    if (nullable4.GetValueOrDefault() == num2 & nullable4.HasValue)
    {
      nullable4 = openQty;
      nullable5 = nullable1;
      if (nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue)
      {
        ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Delete(soLine);
        return;
      }
    }
    nullable5 = openQty;
    nullable4 = nullable1;
    Decimal? nullable6 = nullable5.HasValue & nullable4.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
    PX.Objects.SO.SOLine soLine1 = soLine;
    nullable4 = nullable3;
    Decimal num3 = 0M;
    Decimal? nullable7;
    if (!(nullable4.GetValueOrDefault() == num3 & nullable4.HasValue))
    {
      nullable4 = nullable6;
      nullable5 = nullable3;
      nullable7 = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
    }
    else
      nullable7 = nullable6;
    soLine1.OrderQty = nullable7;
    nullable5 = soLine.OrderQty;
    nullable4 = nullable6;
    if (nullable5.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable5.HasValue == nullable4.HasValue)
      soLine.IsOrchestratedLine = new bool?(true);
    ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soLine);
    nullable4 = nullable3;
    Decimal num4 = 0M;
    if (nullable4.GetValueOrDefault() > num4 & nullable4.HasValue)
      throw new PXException("The order cannot be orchestrated completely.");
  }

  protected virtual List<WarehouseInfo> GetWarehouseInfoByZone(
    List<OrderInventoryInfo> orderInventoryInfo,
    bool silent)
  {
    List<SOOrchestrationPlanLine> list = GraphHelper.RowCast<SOOrchestrationPlanLine>((IEnumerable) PXSelectBase<SOOrchestrationPlanLine, PXViewOf<SOOrchestrationPlanLine>.BasedOn<SelectFromBase<SOOrchestrationPlanLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOOrchestrationPlan>.On<BqlOperand<SOOrchestrationPlan.planID, IBqlString>.IsEqual<SOOrchestrationPlanLine.planID>>>, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<BqlOperand<PX.Objects.IN.INSite.siteID, IBqlInt>.IsEqual<SOOrchestrationPlanLine.targetSiteID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrchestrationPlan.isActive, Equal<True>>>>, And<BqlOperand<SOOrchestrationPlan.shippingZoneID, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<PX.Objects.IN.INSite.active, IBqlBool>.IsEqual<True>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) this.FindAltWarehouseZoneID()
    })).OrderBy<SOOrchestrationPlanLine, int?>((Func<SOOrchestrationPlanLine, int?>) (i => i.Priority)).ToList<SOOrchestrationPlanLine>();
    if (list.Any<SOOrchestrationPlanLine>())
      return this.AllocateWarehousesFromPlan(orderInventoryInfo, list);
    if (!silent)
      throw new PXException("No valid orchestration plan has been found for this order.");
    PXTrace.WriteInformation("No valid orchestration plan has been found for this order.");
    return new List<WarehouseInfo>();
  }

  protected virtual List<WarehouseInfo> GetWarehouseInfo(
    List<OrderInventoryInfo> orderInventoryInfo,
    bool silent)
  {
    List<WarehouseInfo> source1 = new List<WarehouseInfo>();
    Dictionary<int, SOOrchestrationPlanLine> source2 = new Dictionary<int, SOOrchestrationPlanLine>();
    foreach (PX.Objects.SO.SOLine soLine in EnumerableExtensions.Distinct<PX.Objects.SO.SOLine, int?>(this.GetAvailableSOLines(), (Func<PX.Objects.SO.SOLine, int?>) (l => l.SiteID)))
    {
      foreach (SOOrchestrationPlanLine orchestrationPlanLine1 in GraphHelper.RowCast<SOOrchestrationPlanLine>((IEnumerable) PXSelectBase<SOOrchestrationPlanLine, PXViewOf<SOOrchestrationPlanLine>.BasedOn<SelectFromBase<SOOrchestrationPlanLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOOrchestrationPlan>.On<BqlOperand<SOOrchestrationPlan.planID, IBqlString>.IsEqual<SOOrchestrationPlanLine.planID>>>, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<BqlOperand<PX.Objects.IN.INSite.siteID, IBqlInt>.IsEqual<SOOrchestrationPlanLine.targetSiteID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrchestrationPlan.isActive, Equal<True>>>>, And<BqlOperand<SOOrchestrationPlan.sourceSiteID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<PX.Objects.IN.INSite.active, IBqlBool>.IsEqual<True>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) soLine.SiteID
      })).ToList<SOOrchestrationPlanLine>())
      {
        SOOrchestrationPlanLine orchestrationPlanLine2;
        if (source2.TryGetValue(orchestrationPlanLine1.TargetSiteID.Value, out orchestrationPlanLine2))
        {
          SOOrchestrationPlanLine orchestrationPlanLine3 = orchestrationPlanLine2;
          int? priority = orchestrationPlanLine2.Priority;
          int val1 = priority.Value;
          priority = orchestrationPlanLine1.Priority;
          int val2 = priority.Value;
          int? nullable1 = new int?(Math.Min(val1, val2));
          orchestrationPlanLine3.Priority = nullable1;
          SOOrchestrationPlanLine orchestrationPlanLine4 = orchestrationPlanLine2;
          bool? maintainSaftyStock = orchestrationPlanLine2.MaintainSaftyStock;
          int num;
          if (!maintainSaftyStock.Value)
          {
            maintainSaftyStock = orchestrationPlanLine1.MaintainSaftyStock;
            num = maintainSaftyStock.Value ? 1 : 0;
          }
          else
            num = 1;
          bool? nullable2 = new bool?(num != 0);
          orchestrationPlanLine4.MaintainSaftyStock = nullable2;
        }
        else
          source2.Add(orchestrationPlanLine1.TargetSiteID.Value, orchestrationPlanLine1);
      }
    }
    if (!source2.Any<KeyValuePair<int, SOOrchestrationPlanLine>>())
    {
      if (!silent)
        throw new PXException("No valid orchestration plan has been found for this order.");
      PXTrace.WriteInformation("No valid orchestration plan has been found for this order.");
      return new List<WarehouseInfo>();
    }
    if (source2.Count > 0)
      source1.AddRange((IEnumerable<WarehouseInfo>) this.AllocateWarehousesFromPlan(orderInventoryInfo, source2.Values.OrderBy<SOOrchestrationPlanLine, int?>((Func<SOOrchestrationPlanLine, int?>) (i => i.Priority)).ToList<SOOrchestrationPlanLine>()));
    return source1.OrderBy<WarehouseInfo, int?>((Func<WarehouseInfo, int?>) (w => w.SitePriority)).ToList<WarehouseInfo>();
  }

  protected virtual List<WarehouseInfo> AllocateWarehousesFromPlan(
    List<OrderInventoryInfo> orderInventoryInfo,
    List<SOOrchestrationPlanLine> planDetails)
  {
    HashSet<string> values = new HashSet<string>();
    List<WarehouseInfo> source = new List<WarehouseInfo>();
    foreach (SOOrchestrationPlanLine planDetail in planDetails)
    {
      SOOrchestrationPlanLine siteDetail = planDetail;
      WarehouseInfo warehouseInfo = new WarehouseInfo()
      {
        SiteID = siteDetail.TargetSiteID,
        SitePriority = siteDetail.Priority,
        InventoryDetails = new List<WarehouseInventoryDetails>(),
        PlanID = siteDetail.PlanID
      };
      foreach (OrderInventoryInfo orderInventoryInfo1 in orderInventoryInfo.Where<OrderInventoryInfo>((Func<OrderInventoryInfo, bool>) (i => !i.IsAllocated)))
      {
        OrderInventoryInfo lineInfo = orderInventoryInfo1;
        if (!source.Exists((Predicate<WarehouseInfo>) (i =>
        {
          int? siteId = i.SiteID;
          int? targetSiteId = siteDetail.TargetSiteID;
          return siteId.GetValueOrDefault() == targetSiteId.GetValueOrDefault() & siteId.HasValue == targetSiteId.HasValue && i.InventoryDetails.Exists((Predicate<WarehouseInventoryDetails>) (l =>
          {
            int? inventoryId3 = l.InventoryID;
            int? inventoryId4 = lineInfo.InventoryID;
            return inventoryId3.GetValueOrDefault() == inventoryId4.GetValueOrDefault() & inventoryId3.HasValue == inventoryId4.HasValue;
          }));
        })))
        {
          Decimal? nullable1 = (Decimal?) GraphHelper.RowCast<INSiteStatusByCostCenter>((IEnumerable) PXSelectBase<INSiteStatusByCostCenter, PXViewOf<INSiteStatusByCostCenter>.BasedOn<SelectFromBase<INSiteStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteStatusByCostCenter.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<INSiteStatusByCostCenter.siteID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INSiteStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<CostCenter.freeStock>>>>.And<BqlOperand<INSiteStatusByCostCenter.qtyHardAvail, IBqlDecimal>.IsGreater<decimal0>>>>.Config>.Select((PXGraph) this.Base, new object[2]
          {
            (object) lineInfo.InventoryID,
            (object) siteDetail.TargetSiteID
          })).FirstOrDefault<INSiteStatusByCostCenter>()?.QtyHardAvail;
          Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
          if (!(valueOrDefault1 <= 0M))
          {
            INItemSite inItemSite = GraphHelper.RowCast<INItemSite>((IEnumerable) PXSelectBase<INItemSite, PXViewOf<INItemSite>.BasedOn<SelectFromBase<INItemSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSite.inventoryID, Equal<P.AsInt>>>>>.And<BqlOperand<INItemSite.siteID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this.Base, new object[2]
            {
              (object) lineInfo.InventoryID,
              (object) siteDetail.TargetSiteID
            })).FirstOrDefault<INItemSite>();
            Decimal? nullable2;
            if (inItemSite == null)
            {
              nullable1 = new Decimal?();
              nullable2 = nullable1;
            }
            else
              nullable2 = inItemSite.SafetyStock;
            nullable1 = nullable2;
            Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
            if (!siteDetail.MaintainSaftyStock.GetValueOrDefault() || !(valueOrDefault1 - valueOrDefault2 <= 0M))
              warehouseInfo.InventoryDetails.Add(new WarehouseInventoryDetails()
              {
                SiteID = siteDetail.TargetSiteID,
                BaseUOM = lineInfo.BaseUOM,
                InventoryID = lineInfo.InventoryID,
                QtyHardAvail = new Decimal?(valueOrDefault1),
                SafetyStock = new Decimal?(valueOrDefault2),
                MaintainSafetyStock = siteDetail.MaintainSaftyStock
              });
          }
        }
      }
      if (warehouseInfo.InventoryDetails.Count > 0)
        source.Add(warehouseInfo);
      values.Add(siteDetail.PlanID);
    }
    PXTrace.WriteInformation("The following orchestration plans were selected: {0}.", new object[1]
    {
      (object) string.Join(", ", (IEnumerable<string>) values)
    });
    return source.OrderBy<WarehouseInfo, int?>((Func<WarehouseInfo, int?>) (w => w.SitePriority)).ToList<WarehouseInfo>();
  }

  protected virtual string FindAltWarehouseZoneID()
  {
    string zoneId = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.CurrentDocument).Current.ShipZoneID?.ToString();
    if (string.IsNullOrEmpty(zoneId))
    {
      SOShippingAddress soShippingAddress = PXResultset<SOShippingAddress>.op_Implicit(((PXSelectBase<SOShippingAddress>) this.Base.Shipping_Address).Select(Array.Empty<object>()));
      string countryId = soShippingAddress?.CountryID;
      string state = soShippingAddress?.State;
      if (countryId == null && state == null)
      {
        PXTrace.WriteInformation("The order’s shipping zone or destination address is missing or incomplete.");
        return (string) null;
      }
      if (state != null)
      {
        IEnumerable<ShippingZoneLine> source = GraphHelper.RowCast<ShippingZoneLine>((IEnumerable) PXSelectBase<ShippingZoneLine, PXViewOf<ShippingZoneLine>.BasedOn<SelectFromBase<ShippingZoneLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ShippingZoneLine.countryID, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<ShippingZoneLine.stateID, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this.Base, new object[2]
        {
          (object) countryId,
          (object) state
        }));
        zoneId = source != null ? source.FirstOrDefault<ShippingZoneLine>()?.ZoneID : (string) null;
      }
      if (zoneId == null && countryId != null)
      {
        IEnumerable<ShippingZoneLine> source = GraphHelper.RowCast<ShippingZoneLine>((IEnumerable) PXSelectBase<ShippingZoneLine, PXViewOf<ShippingZoneLine>.BasedOn<SelectFromBase<ShippingZoneLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ShippingZoneLine.countryID, Equal<P.AsString.ASCII>>>>>.And<BqlOperand<ShippingZoneLine.stateID, IBqlString>.IsNull>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) countryId
        }));
        zoneId = source != null ? source.FirstOrDefault<ShippingZoneLine>()?.ZoneID : (string) null;
      }
      if (zoneId == null && countryId != null)
      {
        if (state != null)
          PXTrace.WriteInformation("The shipping zone has not been found for the {0} country or {1} state.", new object[2]
          {
            (object) countryId,
            (object) state
          });
        else
          PXTrace.WriteInformation("The shipping zone has not been found for the {0} country.", new object[1]
          {
            (object) countryId
          });
      }
    }
    return zoneId;
  }

  protected virtual IEnumerable<PX.Objects.SO.SOLine> GetAvailableSOLines()
  {
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current == null)
      return Enumerable.Empty<PX.Objects.SO.SOLine>();
    bool anyRestricted = false;
    IEnumerable<PX.Objects.SO.SOLine> first = GraphHelper.RowCast<PX.Objects.SO.SOLine>((IEnumerable) ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Select(Array.Empty<object>())).AsEnumerable<PX.Objects.SO.SOLine>().Where<PX.Objects.SO.SOLine>((Func<PX.Objects.SO.SOLine, bool>) (line =>
    {
      bool flag = this.IsRestrictedForOrchestration(line);
      if (flag && !anyRestricted)
        PXTrace.WriteInformation("Lines marked for purchase, production, or linked to a blanket sales order were skipped from the orchestration.");
      anyRestricted |= flag;
      return !flag;
    }));
    ICollection<PX.Objects.SO.SOLine> linesToExclude = this.GetLinesToExclude();
    if (linesToExclude.Any<PX.Objects.SO.SOLine>())
      PXTrace.WriteInformation("Lines linked to a shipment, purchase order, or another document were skipped from the orchestration.");
    ICollection<PX.Objects.SO.SOLine> second = linesToExclude;
    IEqualityComparer<PX.Objects.SO.SOLine> keyComparer = GraphHelper.Caches<PX.Objects.SO.SOLine>((PXGraph) this.Base).GetKeyComparer<PX.Objects.SO.SOLine>();
    return first.Except<PX.Objects.SO.SOLine>((IEnumerable<PX.Objects.SO.SOLine>) second, keyComparer);
  }

  protected virtual bool IsRestrictedForOrchestration(PX.Objects.SO.SOLine line)
  {
    Decimal? baseQty = line.BaseQty;
    Decimal num1 = 0M;
    if (!(baseQty.GetValueOrDefault() == num1 & baseQty.HasValue))
    {
      Decimal? openQty = line.OpenQty;
      Decimal num2 = 0M;
      if (!(openQty.GetValueOrDefault() == num2 & openQty.HasValue) && !(line.Operation != "I") && line.IsStockItem.GetValueOrDefault() && !line.IsOrchestratedLine.GetValueOrDefault() && line.BlanketNbr == null && !line.POCreate.GetValueOrDefault())
        return line.IsSpecialOrder.GetValueOrDefault();
    }
    return true;
  }

  protected virtual ICollection<PX.Objects.SO.SOLine> GetLinesToExclude()
  {
    return (ICollection<PX.Objects.SO.SOLine>) ((IEnumerable<PX.Objects.SO.SOLineSplit>) ((PXSelectBase<PX.Objects.SO.SOLineSplit>) new FbqlSelect<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<PX.Objects.SO.SOLineSplit.orderType>.IsRelatedTo<PX.Objects.SO.SOOrder.orderType>, Field<PX.Objects.SO.SOLineSplit.orderNbr>.IsRelatedTo<PX.Objects.SO.SOOrder.orderNbr>>.WithTablesOf<PX.Objects.SO.SOOrder, PX.Objects.SO.SOLineSplit>, PX.Objects.SO.SOOrder, PX.Objects.SO.SOLineSplit>.SameAsCurrent.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.shipmentNbr, IsNotNull>>>, Or<BqlOperand<PX.Objects.SO.SOLineSplit.pONbr, IBqlString>.IsNotNull>>>.Or<BqlOperand<PX.Objects.SO.SOLineSplit.pOReceiptNbr, IBqlString>.IsNotNull>>>, PX.Objects.SO.SOLineSplit>.View((PXGraph) this.Base)).Select<PX.Objects.SO.SOLineSplit>(Array.Empty<object>())).AsEnumerable<PX.Objects.SO.SOLineSplit>().GroupBy<PX.Objects.SO.SOLineSplit, (string, string, int?)>((Func<PX.Objects.SO.SOLineSplit, (string, string, int?)>) (s => (s.OrderType, s.OrderNbr, s.LineNbr))).Select<IGrouping<(string, string, int?), PX.Objects.SO.SOLineSplit>, PX.Objects.SO.SOLine>((Func<IGrouping<(string, string, int?), PX.Objects.SO.SOLineSplit>, PX.Objects.SO.SOLine>) (s => new PX.Objects.SO.SOLine()
    {
      OrderType = s.Key.OrderType,
      OrderNbr = s.Key.OrderNbr,
      LineNbr = s.Key.LineNbr
    })).ToList<PX.Objects.SO.SOLine>();
  }

  protected virtual IEnumerable<PX.Objects.SO.SOLineSplit> SelectSplits(PX.Objects.SO.SOLine line)
  {
    return GraphHelper.RowCast<PX.Objects.SO.SOLineSplit>((IEnumerable) ((PXSelectBase) this.Base.splits).View.SelectMultiBound((object[]) new PX.Objects.SO.SOLine[1]
    {
      line
    }, Array.Empty<object>())).AsEnumerable<PX.Objects.SO.SOLineSplit>().Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (i => i.PlanID.HasValue));
  }

  protected virtual List<WarehouseInfo> FindSingleWarehouse(
    List<List<int>> combos,
    List<WarehouseInfo> warehouseInfo,
    List<OrderInventoryInfo> orderInventoryInfo)
  {
    List<WarehouseInfo> singleWarehouse = new List<WarehouseInfo>();
    foreach (int num1 in combos.Where<List<int>>((Func<List<int>, bool>) (c => c.Count == 1)).Select<List<int>, int>((Func<List<int>, int>) (w => w.FirstOrDefault<int>())))
    {
      int combo = num1;
      WarehouseInfo warehouseInfo1 = warehouseInfo.Where<WarehouseInfo>((Func<WarehouseInfo, bool>) (w =>
      {
        int? siteId = w.SiteID;
        int num2 = combo;
        return siteId.GetValueOrDefault() == num2 & siteId.HasValue;
      })).FirstOrDefault<WarehouseInfo>();
      int num3 = 0;
      List<OrderInventoryInfo> list = orderInventoryInfo.Where<OrderInventoryInfo>((Func<OrderInventoryInfo, bool>) (l => !l.IsAllocated)).ToList<OrderInventoryInfo>();
      foreach (OrderInventoryInfo orderInventoryInfo1 in list)
      {
        OrderInventoryInfo orderInventoryitem = orderInventoryInfo1;
        WarehouseInventoryDetails inventoryDetails = warehouseInfo1.InventoryDetails.Where<WarehouseInventoryDetails>((Func<WarehouseInventoryDetails, bool>) (i =>
        {
          int? inventoryId1 = i.InventoryID;
          int? inventoryId2 = orderInventoryitem.InventoryID;
          return inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue;
        })).FirstOrDefault<WarehouseInventoryDetails>();
        if (inventoryDetails != null)
        {
          Decimal? nullable1 = inventoryDetails.QtyHardAvail;
          if (inventoryDetails.MaintainSafetyStock.GetValueOrDefault())
          {
            Decimal? nullable2 = nullable1;
            Decimal? safetyStock = inventoryDetails.SafetyStock;
            nullable1 = nullable2.HasValue & safetyStock.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - safetyStock.GetValueOrDefault()) : new Decimal?();
          }
          Decimal? nullable3 = nullable1;
          Decimal? lineQty = orderInventoryitem.LineQty;
          if (nullable3.GetValueOrDefault() >= lineQty.GetValueOrDefault() & nullable3.HasValue & lineQty.HasValue)
            ++num3;
        }
        else
          break;
      }
      if (num3 == list.Count)
      {
        singleWarehouse.Add(warehouseInfo1);
        break;
      }
    }
    return singleWarehouse;
  }

  protected virtual List<WarehouseInfo> FindWarehouses(
    List<List<int>> combos,
    List<WarehouseInfo> warehouseInfo,
    List<OrderInventoryInfo> orderInventoryInfo,
    int? warehouseLimit)
  {
    List<WarehouseInfo> source = new List<WarehouseInfo>();
    List<OrderInventoryInfo> unAllocOrderInventoryInfo = orderInventoryInfo.Where<OrderInventoryInfo>((Func<OrderInventoryInfo, bool>) (x => !x.IsAllocated)).ToList<OrderInventoryInfo>();
    if (combos.Count == 0 || warehouseInfo.Count == 0 || unAllocOrderInventoryInfo.Count == 0)
      return source;
    if (!warehouseLimit.HasValue || (warehouseLimit ?? 0) == 0)
      warehouseLimit = new int?(9999);
    foreach (List<int> intList in combos.Where<List<int>>((Func<List<int>, bool>) (c =>
    {
      if (c.Count <= 1)
        return false;
      int count = c.Count;
      int? nullable = warehouseLimit;
      int valueOrDefault = nullable.GetValueOrDefault();
      return count <= valueOrDefault & nullable.HasValue;
    })))
    {
      List<int> combo = intList;
      int num1 = 0;
      for (int inventoryIndex = 0; inventoryIndex < unAllocOrderInventoryInfo.Count; inventoryIndex++)
      {
        Decimal? nullable1 = unAllocOrderInventoryInfo[inventoryIndex].LineQty;
        for (int wComboIndex = 0; wComboIndex < combo.Count; wComboIndex++)
        {
          WarehouseInfo warehouseInfo1 = warehouseInfo.FirstOrDefault<WarehouseInfo>((Func<WarehouseInfo, bool>) (w =>
          {
            int? siteId = w.SiteID;
            int num2 = combo[wComboIndex];
            return siteId.GetValueOrDefault() == num2 & siteId.HasValue;
          }));
          WarehouseInventoryDetails inventoryDetails = warehouseInfo1 != null ? warehouseInfo1.InventoryDetails.Where<WarehouseInventoryDetails>((Func<WarehouseInventoryDetails, bool>) (i =>
          {
            int? inventoryId1 = i.InventoryID;
            int? inventoryId2 = unAllocOrderInventoryInfo[inventoryIndex].InventoryID;
            return inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue;
          })).FirstOrDefault<WarehouseInventoryDetails>() : (WarehouseInventoryDetails) null;
          if (inventoryDetails != null)
          {
            Decimal? nullable2 = inventoryDetails.QtyHardAvail;
            Decimal num3 = 0M;
            if (!(nullable2.GetValueOrDefault() == num3 & nullable2.HasValue))
            {
              Decimal? nullable3 = inventoryDetails.QtyHardAvail;
              Decimal? nullable4;
              if (inventoryDetails.MaintainSafetyStock.GetValueOrDefault())
              {
                nullable2 = nullable3;
                nullable4 = inventoryDetails.SafetyStock;
                nullable3 = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
              }
              nullable4 = nullable3;
              nullable2 = nullable1;
              Decimal? nullable5 = nullable4.GetValueOrDefault() <= nullable2.GetValueOrDefault() & nullable4.HasValue & nullable2.HasValue ? nullable3 : nullable1;
              nullable2 = nullable1;
              nullable4 = nullable5;
              nullable1 = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
              nullable4 = nullable1;
              Decimal num4 = 0M;
              if (nullable4.GetValueOrDefault() <= num4 & nullable4.HasValue)
              {
                ++num1;
                break;
              }
            }
          }
        }
        if (unAllocOrderInventoryInfo.Count == num1)
        {
          source = warehouseInfo.Where<WarehouseInfo>((Func<WarehouseInfo, bool>) (w => combo.Contains(w.SiteID.Value))).ToList<WarehouseInfo>();
          break;
        }
      }
      if (source.Any<WarehouseInfo>())
        break;
    }
    return source;
  }

  protected virtual List<List<int>> GetWarehouseCombinations(List<WarehouseInfo> warehouseInfo)
  {
    if (warehouseInfo.Count > this.LimitNumberOfWarehousesInAlgorithm)
      throw new PXException("The total number of warehouses used in the orchestration cannot exceed {0}. Review the selected orchestration plans and reduce this number.", new object[1]
      {
        (object) this.LimitNumberOfWarehousesInAlgorithm
      });
    warehouseInfo = warehouseInfo.OrderByDescending<WarehouseInfo, int?>((Func<WarehouseInfo, int?>) (i => i.SitePriority)).ToList<WarehouseInfo>();
    List<List<int>> source = new List<List<int>>();
    double num = Math.Pow(2.0, (double) warehouseInfo.Count);
    for (int index1 = 1; (double) index1 <= num - 1.0; ++index1)
    {
      string str = Convert.ToString(index1, 2).PadLeft(warehouseInfo.Count, '0');
      List<int> intList = new List<int>();
      for (int index2 = 0; index2 < str.Length; ++index2)
      {
        if (str[index2] == '1')
          intList.Add(warehouseInfo[index2].SiteID.Value);
      }
      source.Add(intList);
    }
    return source.OrderBy<List<int>, int>((Func<List<int>, int>) (x => x.Count)).ToList<List<int>>();
  }

  protected class ShowOrchestrationDialog : IPXCustomInfo
  {
    protected int _recordsCount;

    public ShowOrchestrationDialog(int recordsCount) => this._recordsCount = recordsCount;

    public void Complete(PXLongRunStatus status, PXGraph graph)
    {
      graph.LongOperationManager.ClearStatus();
      if (this._recordsCount == 0)
        return;
      ((PXSelectBase<SOOrderOrchestrationSettings>) graph.GetExtension<SOOrchestration>().OrderOrchestrationSettingsView).AskExt(true);
    }
  }
}
