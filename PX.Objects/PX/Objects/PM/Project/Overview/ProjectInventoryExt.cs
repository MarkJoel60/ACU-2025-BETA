// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.Project.Overview.ProjectInventoryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM.Project.Overview;

/// <summary>
/// The 'Project Inventory' section on the PM301500 screen.
/// </summary>
public class ProjectInventoryExt : ProjectOverviewExtension
{
  [PXFilterable(new Type[] {})]
  public FbqlSelect<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>, PX.Objects.IN.InventoryItem>.View ProjectInventory;
  [PXHidden]
  public FbqlSelect<SelectFromBase<INSite, TypeArrayOf<IFbqlJoin>.Empty>, INSite>.View InSites;
  [PXHidden]
  public FbqlSelect<SelectFromBase<INLocation, TypeArrayOf<IFbqlJoin>.Empty>, INLocation>.View InLocations;
  [PXHidden]
  public FbqlSelect<SelectFromBase<INLocationStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>, INLocationStatusByCostCenter>.View InLocationStatusByCostCenters;
  [PXHidden]
  public FbqlSelect<SelectFromBase<PMTask, TypeArrayOf<IFbqlJoin>.Empty>, PMTask>.View ProjectTasks;
  [PXHidden]
  public FbqlSelect<SelectFromBase<INCostCenter, TypeArrayOf<IFbqlJoin>.Empty>, INCostCenter>.View InCostCenters;
  [PXHidden]
  public FbqlSelect<SelectFromBase<INCostStatus, TypeArrayOf<IFbqlJoin>.Empty>, INCostStatus>.View InCostStatus;
  public PXAction<PMProject> openProjectInventory;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.materialManagement>();

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Total Qty. On Hand")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<INLocationStatusByCostCenter.qtyOnHand> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Location ID")]
  protected virtual void _(PX.Data.Events.CacheAttached<INLocation.locationCD> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Cost Layer Type")]
  protected virtual void _(PX.Data.Events.CacheAttached<INCostCenter.costLayerType> e)
  {
  }

  public virtual IEnumerable projectInventory()
  {
    ProjectInventoryExt projectInventoryExt = this;
    ProjectOverview projectOverview = projectInventoryExt.Base;
    DataViewHelper.DataViewFilter[] dataViewFilterArray = new DataViewHelper.DataViewFilter[1]
    {
      DataViewHelper.DataViewFilter.Create<PMProject, PMProject.contractCD>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) projectInventoryExt.Base.Project).Current.ContractCD)
    };
    foreach (IDictionary<string, object> genericInquiryData in projectOverview.GetGenericInquiryDataDictionary("PM301010", dataViewFilterArray))
      yield return (object) new PXResult<PX.Objects.IN.InventoryItem, INSite, INLocation, INLocationStatusByCostCenter, PMTask, INCostCenter, INCostStatus>(genericInquiryData.GetValue<PX.Objects.IN.InventoryItem>(), genericInquiryData.GetValue<INSite>(), genericInquiryData.GetValue<INLocation>(), genericInquiryData.GetValue<INLocationStatusByCostCenter>(), genericInquiryData.GetValue<PMTask>(), genericInquiryData.GetValue<INCostCenter>(), new INCostStatus()
      {
        TotalCost = new Decimal?(genericInquiryData.GetGenericResultValue<Decimal>("PMItemCostStatusByCostCenter"))
      });
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable OpenProjectInventory(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToGenericIquiry("PM301010", DataViewHelper.DataViewFilter.Create<PMProject, PMProject.contractCD>((PXCondition) 0, (object) ((PXSelectBase<PMProject>) this.Base.Project).Current.ContractCD));
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem> e)
  {
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem>>) e).Cache.AllowInsert = false;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem>>) e).Cache.AllowUpdate = false;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem>>) e).Cache.AllowDelete = false;
  }
}
