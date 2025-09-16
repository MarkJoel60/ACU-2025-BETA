// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventorySummaryEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN.DAC.Projections;
using PX.Objects.IN.DAC.Unbound;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Objects.IN;

[TableAndChartDashboardType]
public class InventorySummaryEnq : PXGraph<InventorySummaryEnq>
{
  public PXCancel<InventorySummaryEnqFilter> Cancel;
  public PXAction<InventorySummaryEnqFilter> viewAllocDet;
  public PXFilter<InventorySummaryEnqFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXSelect<InventorySummaryEnquiryResult> ISERecords;
  public PXSetupOptional<CommonSetup> commonsetup;
  private Lazy<string> _locationDisplayName;
  public PXAction<InventorySummaryEnqFilter> viewItem;
  private bool timestampSelected;

  public InventorySummaryEnq.ItemPlanHelper ItemPlanHelperExt
  {
    get => ((PXGraph) this).FindImplementation<InventorySummaryEnq.ItemPlanHelper>();
  }

  protected virtual IEnumerable iSERecords()
  {
    string controlTimeStamp = this.ControlTimeStamp;
    if (PXView.MaximumRows == 1 && PXView.Searches != null && PXView.Searches.Length == 1)
    {
      InventorySummaryEnquiryResult summaryEnquiryResult = (InventorySummaryEnquiryResult) ((PXSelectBase) this.ISERecords).Cache.Locate((object) new InventorySummaryEnquiryResult()
      {
        GridLineNbr = (int?) PXView.Searches[0]
      });
      if (summaryEnquiryResult != null && summaryEnquiryResult.ControlTimeStamp == controlTimeStamp)
      {
        PXDelegateResult pxDelegateResult = new PXDelegateResult();
        ((List<object>) pxDelegateResult).Add((object) summaryEnquiryResult);
        pxDelegateResult.IsResultFiltered = true;
        pxDelegateResult.IsResultSorted = true;
        pxDelegateResult.IsResultTruncated = true;
        return (IEnumerable) pxDelegateResult;
      }
    }
    int num1 = 0;
    if (!NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.ISERecords).Cache.Cached) || GraphHelper.RowCast<InventorySummaryEnquiryResult>(((PXSelectBase) this.ISERecords).Cache.Cached).First<InventorySummaryEnquiryResult>().ControlTimeStamp != controlTimeStamp)
    {
      ((PXSelectBase) this.ISERecords).Cache.Clear();
      foreach (InventorySummaryEnquiryResult summaryEnquiryResult in this.iSERecordsFetch())
      {
        summaryEnquiryResult.GridLineNbr = new int?(++num1);
        GraphHelper.Hold(((PXSelectBase) this.ISERecords).Cache, (object) summaryEnquiryResult);
      }
    }
    else
      num1 = (int) ((PXSelectBase) this.ISERecords).Cache.Cached.Count();
    if (((PXGraph) this).IsImport || ((PXGraph) this).IsExport || ((PXGraph) this).IsContractBasedAPI)
      return ((PXSelectBase) this.ISERecords).Cache.Cached;
    IEnumerable<InventorySummaryEnquiryResult> resultset = GraphHelper.RowCast<InventorySummaryEnquiryResult>(((PXSelectBase) this.ISERecords).Cache.Cached);
    InventorySummaryEnquiryResult summaryTotal = this.CalculateSummaryTotal(resultset);
    int num2;
    summaryTotal.GridLineNbr = new int?(num2 = num1 + 1);
    return this.SortSummaryResult(resultset, summaryTotal);
  }

  public InventorySummaryEnq()
  {
    ((PXSelectBase) this.ISERecords).Cache.AllowInsert = false;
    ((PXSelectBase) this.ISERecords).Cache.AllowDelete = false;
    ((PXSelectBase) this.ISERecords).Cache.AllowUpdate = false;
    CommonSetup current = ((PXSelectBase<CommonSetup>) this.commonsetup).Current;
    this._locationDisplayName = new Lazy<string>(new Func<string>(this.GetLocationDisplayName));
    PXUIFieldAttribute.SetVisible<InventorySummaryEnqFilter.expandByCostLayerType>(((PXSelectBase) this.Filter).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.specialOrders>() || PXAccess.FeatureInstalled<FeaturesSet.materialManagement>());
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<InventorySummaryEnqFilter.locationID> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<InventorySummaryEnqFilter.locationID>, object, object>) e).NewValue = (object) null;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<InventorySummaryEnqFilter.locationID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<InventorySummaryEnquiryResult.locationID> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<InventorySummaryEnquiryResult.locationID>, object, object>) e).NewValue = (object) null;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<InventorySummaryEnquiryResult.locationID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowSelected<InventorySummaryEnqFilter> e)
  {
    if (e.Row == null)
      return;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.AdjustUI(((PXSelectBase) this.ISERecords).Cache, (object) null).For<InventorySummaryEnquiryResult.lotSerialNbr>((Action<PXUIFieldAttribute>) (a => a.Visible = e.Row.ExpandByLotSerialNbr.GetValueOrDefault()));
    chained = chained.SameFor<InventorySummaryEnquiryResult.expireDate>();
    chained.For<InventorySummaryEnquiryResult.costLayerType>((Action<PXUIFieldAttribute>) (a => a.Visible = e.Row.ExpandByCostLayerType.GetValueOrDefault()));
  }

  protected virtual void _(PX.Data.Events.RowInserted<InventorySummaryEnqFilter> e)
  {
    ((PXSelectBase) this.ISERecords).Cache.Clear();
  }

  protected virtual void _(PX.Data.Events.RowUpdated<InventorySummaryEnqFilter> e)
  {
    ((PXSelectBase) this.ISERecords).Cache.Clear();
  }

  protected virtual void _(PX.Data.Events.RowDeleted<InventorySummaryEnqFilter> e)
  {
    ((PXSelectBase) this.ISERecords).Cache.Clear();
  }

  protected virtual void AppendCostLocationLayerJoin(PXSelectBase<INLocationStatusByCostCenter> cmd)
  {
  }

  protected virtual void AppendCostLotSerialLayerJoin(
    PXSelectBase<INLotSerialStatusByCostCenter> cmd)
  {
  }

  protected virtual void AppendFilters<T>(PXSelectBase<T> cmd, InventorySummaryEnqFilter filter) where T : class, IBqlTable, new()
  {
    if (filter.InventoryID.HasValue)
      cmd.WhereAnd<Where<InventoryItem.inventoryID, Equal<Current<InventorySummaryEnqFilter.inventoryID>>>>();
    if (!SubCDUtils.IsSubCDEmpty(filter.SubItemCD))
      cmd.WhereAnd<Where<INSubItem.subItemCD, Like<Current<InventorySummaryEnqFilter.subItemCDWildcard>>>>();
    if (filter.SiteID.HasValue)
      cmd.WhereAnd<Where<INSite.siteID, Equal<Current<InventorySummaryEnqFilter.siteID>>>>();
    if (PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
      cmd.WhereAnd<Where<INSite.branchID, InsideBranchesOf<Current<InventorySummaryEnqFilter.orgBAccountID>>>>();
    if (!EnumerableExtensions.IsNotIn<Type>(typeof (T), typeof (INSiteStatusByCostCenter), typeof (INItemPlan)) || !filter.LocationID.HasValue)
      return;
    cmd.WhereAnd<Where<INLocation.locationID, Equal<Current<InventorySummaryEnqFilter.locationID>>>>();
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<InventorySummaryEnquiryResult.locationID> e)
  {
    string str = (string) null;
    object returnValue = ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<InventorySummaryEnquiryResult.locationID>>) e).ReturnValue;
    if (returnValue != null)
    {
      if (returnValue is -1)
        str = "Total:";
    }
    else
      str = "<UNASSIGNED>";
    if (str == null)
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<InventorySummaryEnquiryResult.locationID>>) e).ReturnState = (object) PXFieldState.CreateInstance((object) PXMessages.LocalizeNoPrefix(str), typeof (string), new bool?(false), new bool?(), new int?(), new int?(), new int?(), (object) null, "locationID", (string) null, this._locationDisplayName.Value, (string) null, (PXErrorLevel) 0, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<InventorySummaryEnquiryResult.locationID>>) e).Cancel = true;
  }

  private string GetLocationDisplayName()
  {
    string locationDisplayName = PXUIFieldAttribute.GetDisplayName<InventorySummaryEnquiryResult.locationID>(((PXSelectBase) this.ISERecords).Cache);
    if (locationDisplayName != null)
      locationDisplayName = PXMessages.LocalizeNoPrefix(locationDisplayName);
    return locationDisplayName;
  }

  protected virtual List<Type> GetCostTables()
  {
    return new List<Type>((IEnumerable<Type>) new Type[1]
    {
      typeof (INLocationCostStatus)
    });
  }

  protected virtual IEnumerable<InventorySummaryEnquiryResult> iSERecordsFetch()
  {
    try
    {
      return this.iSERecordsFetchImpl();
    }
    finally
    {
      ((PXCache) GraphHelper.Caches<SiteStatusAggregate>((PXGraph) this)).Clear();
      ((PXCache) GraphHelper.Caches<LocationStatusAggregate>((PXGraph) this)).Clear();
      ((PXCache) GraphHelper.Caches<LotSerialStatusAggregate>((PXGraph) this)).Clear();
    }
  }

  private IEnumerable<InventorySummaryEnquiryResult> iSERecordsFetchImpl()
  {
    InventorySummaryEnqFilter filter = ((PXSelectBase<InventorySummaryEnqFilter>) this.Filter).Current;
    string controlTimeStamp = this.ControlTimeStamp;
    PXSelectBase<INLotSerialStatusByCostCenter> cmd1 = (PXSelectBase<INLotSerialStatusByCostCenter>) new PXSelectReadonly2<INLotSerialStatusByCostCenter, InnerJoin<INLocation, On<INLotSerialStatusByCostCenter.FK.Location>, InnerJoin<InventoryItem, On<InventoryItem.inventoryID, Equal<INLotSerialStatusByCostCenter.inventoryID>, And<Match<InventoryItem, Current<AccessInfo.userName>>>>, InnerJoin<INSite, On2<INLotSerialStatusByCostCenter.FK.Site, And<INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>, And<Match<INSite, Current<AccessInfo.userName>>>>>, InnerJoin<INSubItem, On<INLotSerialStatusByCostCenter.FK.SubItem>, LeftJoin<INCostCenter, On<INLotSerialStatusByCostCenter.FK.CostCenter>, LeftJoin<INLotSerClass, On<InventoryItem.FK.LotSerialClass>>>>>>>, Where<INLotSerialStatusByCostCenter.inventoryID, Equal<Current<InventorySummaryEnqFilter.inventoryID>>, And<Where<Current<InventorySummaryEnqFilter.expandByLotSerialNbr>, Equal<True>, And<INLotSerClass.lotSerAssign, Equal<INLotSerAssign.whenReceived>, And<INLotSerClass.lotSerTrack, NotEqual<INLotSerTrack.notNumbered>, Or<InventoryItem.valMethod, Equal<INValMethod.specific>, Or<INLotSerClass.lotSerTrackExpiration, Equal<True>, And<INLotSerClass.lotSerTrack, NotEqual<INLotSerTrack.notNumbered>>>>>>>>>>((PXGraph) this);
    this.AppendCostLotSerialLayerJoin(cmd1);
    this.AppendFilters<INLotSerialStatusByCostCenter>(cmd1, filter);
    PXSelectReadonly<INLotSerialCostStatusByCostLayerType, Where<INLotSerialCostStatusByCostLayerType.inventoryID, Equal<Current<InventorySummaryEnqFilter.inventoryID>>, And<INLotSerialCostStatusByCostLayerType.lotSerialNbr, IsNotNull>>> pxSelectReadonly1 = new PXSelectReadonly<INLotSerialCostStatusByCostLayerType, Where<INLotSerialCostStatusByCostLayerType.inventoryID, Equal<Current<InventorySummaryEnqFilter.inventoryID>>, And<INLotSerialCostStatusByCostLayerType.lotSerialNbr, IsNotNull>>>((PXGraph) this);
    PXSelectReadonly<INSiteCostStatusByCostLayerType, Where<INSiteCostStatusByCostLayerType.inventoryID, Equal<Current<InventorySummaryEnqFilter.inventoryID>>>> pxSelectReadonly2 = new PXSelectReadonly<INSiteCostStatusByCostLayerType, Where<INSiteCostStatusByCostLayerType.inventoryID, Equal<Current<InventorySummaryEnqFilter.inventoryID>>>>((PXGraph) this);
    PXSelectReadonly<INLocationCostStatus, Where<INLocationCostStatus.inventoryID, Equal<Current<InventorySummaryEnqFilter.inventoryID>>>> pxSelectReadonly3 = new PXSelectReadonly<INLocationCostStatus, Where<INLocationCostStatus.inventoryID, Equal<Current<InventorySummaryEnqFilter.inventoryID>>>>((PXGraph) this);
    if (filter.SiteID.HasValue)
    {
      ((PXSelectBase<INLotSerialCostStatusByCostLayerType>) pxSelectReadonly1).WhereAnd<Where<INLotSerialCostStatusByCostLayerType.siteID, Equal<Current<InventorySummaryEnqFilter.siteID>>>>();
      ((PXSelectBase<INSiteCostStatusByCostLayerType>) pxSelectReadonly2).WhereAnd<Where<INSiteCostStatusByCostLayerType.siteID, Equal<Current<InventorySummaryEnqFilter.siteID>>>>();
      ((PXSelectBase<INLocationCostStatus>) pxSelectReadonly3).WhereAnd<Where<INLocationCostStatus.siteID, Equal<Current<InventorySummaryEnqFilter.siteID>>>>();
    }
    int? nullable1 = filter.LocationID;
    if (nullable1.HasValue)
      ((PXSelectBase<INLocationCostStatus>) pxSelectReadonly3).WhereAnd<Where<INLocationCostStatus.locationID, Equal<Current<InventorySummaryEnqFilter.locationID>>>>();
    nullable1 = filter.InventoryID;
    if (nullable1.HasValue)
    {
      foreach (PXResult<INLotSerialCostStatusByCostLayerType> pxResult in ((PXSelectBase<INLotSerialCostStatusByCostLayerType>) pxSelectReadonly1).Select(Array.Empty<object>()))
      {
        INLotSerialCostStatusByCostLayerType statusByCostLayerType = PXResult<INLotSerialCostStatusByCostLayerType>.op_Implicit(pxResult);
        ((PXSelectBase) pxSelectReadonly1).Cache.SetStatus((object) statusByCostLayerType, (PXEntryStatus) 0);
      }
      foreach (PXResult<INSiteCostStatusByCostLayerType> pxResult in ((PXSelectBase<INSiteCostStatusByCostLayerType>) pxSelectReadonly2).Select(Array.Empty<object>()))
      {
        INSiteCostStatusByCostLayerType statusByCostLayerType = PXResult<INSiteCostStatusByCostLayerType>.op_Implicit(pxResult);
        ((PXSelectBase) pxSelectReadonly2).Cache.SetStatus((object) statusByCostLayerType, (PXEntryStatus) 0);
      }
      foreach (PXResult<INLocationCostStatus> pxResult in ((PXSelectBase<INLocationCostStatus>) pxSelectReadonly3).Select(Array.Empty<object>()))
      {
        INLocationCostStatus locationCostStatus = PXResult<INLocationCostStatus>.op_Implicit(pxResult);
        ((PXSelectBase) pxSelectReadonly3).Cache.SetStatus((object) locationCostStatus, (PXEntryStatus) 0);
      }
    }
    List<Type> typeList = new List<Type>((IEnumerable<Type>) new Type[7]
    {
      typeof (INLotSerialStatusByCostCenter),
      typeof (INSite.siteCD),
      typeof (INSubItem.subItemCD),
      typeof (INCostCenter.costLayerType),
      typeof (INLocation.locationCD),
      typeof (INLocation.inclQtyAvail),
      typeof (InventoryItem.baseUnit)
    });
    typeList.AddRange((IEnumerable<Type>) this.GetCostTables());
    bool inserted1;
    using (new PXFieldScope(((PXSelectBase) cmd1).View, typeList.ToArray()))
    {
      foreach (PXResult<INLotSerialStatusByCostCenter, INLocation, InventoryItem, INSite, INSubItem, INCostCenter> pxResult in cmd1.Select(Array.Empty<object>()))
      {
        INLotSerialStatusByCostCenter statusByCostCenter = PXResult<INLotSerialStatusByCostCenter, INLocation, InventoryItem, INSite, INSubItem, INCostCenter>.op_Implicit(pxResult);
        INLocation inLocation = PXResult<INLotSerialStatusByCostCenter, INLocation, InventoryItem, INSite, INSubItem, INCostCenter>.op_Implicit(pxResult);
        InventoryItem inventoryItem = PXResult<INLotSerialStatusByCostCenter, INLocation, InventoryItem, INSite, INSubItem, INCostCenter>.op_Implicit(pxResult);
        INCostCenter inCostCenter = PXResult<INLotSerialStatusByCostCenter, INLocation, InventoryItem, INSite, INSubItem, INCostCenter>.op_Implicit(pxResult);
        bool inserted2;
        LotSerialStatusAggregate serialStatusAggregate1 = this.LocateOrInsertLotSerialAggregate(statusByCostCenter, out inserted2);
        serialStatusAggregate1.Add<LotSerialStatusAggregate>((IStatus) statusByCostCenter);
        if (inserted2)
        {
          serialStatusAggregate1.CostLayerType = inCostCenter.CostLayerType ?? "N";
          serialStatusAggregate1.SubItemCD = PXResult<INLotSerialStatusByCostCenter, INLocation, InventoryItem, INSite, INSubItem, INCostCenter>.op_Implicit(pxResult).SubItemCD;
          serialStatusAggregate1.SiteCD = PXResult<INLotSerialStatusByCostCenter, INLocation, InventoryItem, INSite, INSubItem, INCostCenter>.op_Implicit(pxResult).SiteCD;
          serialStatusAggregate1.LocationCD = PXResult<INLotSerialStatusByCostCenter, INLocation, InventoryItem, INSite, INSubItem, INCostCenter>.op_Implicit(pxResult).LocationCD;
          serialStatusAggregate1.ExpireDate = statusByCostCenter.ExpireDate;
          serialStatusAggregate1.BaseUnit = inventoryItem.BaseUnit;
        }
        ICostStatus st = (ICostStatus) ((PXSelectBase<INLotSerialCostStatusByCostLayerType>) pxSelectReadonly1).Locate(new INLotSerialCostStatusByCostLayerType()
        {
          InventoryID = statusByCostCenter.InventoryID,
          SubItemID = statusByCostCenter.SubItemID,
          SiteID = statusByCostCenter.SiteID,
          LotSerialNbr = statusByCostCenter.LotSerialNbr,
          CostLayerType = inCostCenter.CostLayerType ?? "N"
        });
        Decimal? nullable2;
        int num1;
        if (st == null)
        {
          num1 = 1;
        }
        else
        {
          nullable2 = st.TotalCost;
          num1 = !nullable2.HasValue ? 1 : 0;
        }
        bool? nullable3;
        if (num1 != 0)
        {
          nullable1 = statusByCostCenter.CostCenterID;
          int num2 = 0;
          if (nullable1.GetValueOrDefault() == num2 & nullable1.HasValue)
          {
            if (!(inventoryItem.ValMethod != "S"))
            {
              nullable3 = filter.ExpandByLotSerialNbr;
              if (!nullable3.GetValueOrDefault())
                goto label_39;
            }
            st = (ICostStatus) ((PXSelectBase<INLocationCostStatus>) pxSelectReadonly3).Locate(new INLocationCostStatus()
            {
              InventoryID = statusByCostCenter.InventoryID,
              SubItemID = statusByCostCenter.SubItemID,
              LocationID = statusByCostCenter.LocationID,
              SiteID = statusByCostCenter.SiteID
            });
          }
        }
label_39:
        int num3;
        if (st == null)
        {
          num3 = 1;
        }
        else
        {
          nullable2 = st.TotalCost;
          num3 = !nullable2.HasValue ? 1 : 0;
        }
        if (num3 != 0)
          st = (ICostStatus) ((PXSelectBase<INSiteCostStatusByCostLayerType>) pxSelectReadonly2).Locate(new INSiteCostStatusByCostLayerType()
          {
            InventoryID = statusByCostCenter.InventoryID,
            SubItemID = statusByCostCenter.SubItemID,
            SiteID = statusByCostCenter.SiteID,
            CostLayerType = inCostCenter.CostLayerType ?? "N"
          });
        LotSerialStatusAggregate serialStatusAggregate2 = serialStatusAggregate1;
        nullable2 = serialStatusAggregate2.TotalCost;
        Decimal unitCost = this.CalculateUnitCost(st, false);
        Decimal? nullable4 = statusByCostCenter.QtyOnHand;
        Decimal? nullable5 = nullable4.HasValue ? new Decimal?(unitCost * nullable4.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable6;
        if (!(nullable2.HasValue & nullable5.HasValue))
        {
          nullable4 = new Decimal?();
          nullable6 = nullable4;
        }
        else
          nullable6 = new Decimal?(nullable2.GetValueOrDefault() + nullable5.GetValueOrDefault());
        serialStatusAggregate2.TotalCost = nullable6;
        nullable3 = inLocation.InclQtyAvail;
        inserted1 = false;
        if (nullable3.GetValueOrDefault() == inserted1 & nullable3.HasValue)
        {
          serialStatusAggregate1.QtyNotAvail = serialStatusAggregate1.QtyAvail;
          serialStatusAggregate1.QtyAvail = new Decimal?(0M);
          serialStatusAggregate1.QtyHardAvail = new Decimal?(0M);
          serialStatusAggregate1.QtyActual = new Decimal?(0M);
        }
        else
          serialStatusAggregate1.QtyNotAvail = new Decimal?(0M);
        DateTime? nullable7 = statusByCostCenter.ExpireDate;
        if (nullable7.HasValue)
        {
          nullable7 = ((PXGraph) this).Accessinfo.BusinessDate;
          DateTime t1 = nullable7.Value;
          nullable7 = statusByCostCenter.ExpireDate;
          DateTime t2 = nullable7.Value;
          if (DateTime.Compare(t1, t2) > 0)
            serialStatusAggregate1.QtyExpired = statusByCostCenter.QtyOnHand;
        }
        if (PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>())
          this.LocateOrInsertLocationAggregate((LocationStatusAggregate.ITable) serialStatusAggregate1, true, out inserted1).Add<LocationStatusAggregate>((IStatus) serialStatusAggregate1);
        else
          this.LocateOrInsertSiteAggregate((SiteStatusAggregate.ITable) serialStatusAggregate1, true, out inserted1).Add<SiteStatusAggregate>((IStatus) serialStatusAggregate1);
        LotSerialStatusAggregate serialStatusAggregate3 = serialStatusAggregate1;
        Decimal? qtyAvail = serialStatusAggregate3.QtyAvail;
        nullable2 = serialStatusAggregate1.QtyExpired;
        Decimal? nullable8;
        if (!(qtyAvail.HasValue & nullable2.HasValue))
        {
          nullable4 = new Decimal?();
          nullable8 = nullable4;
        }
        else
          nullable8 = new Decimal?(qtyAvail.GetValueOrDefault() - nullable2.GetValueOrDefault());
        serialStatusAggregate3.QtyAvail = nullable8;
        nullable3 = inLocation.InclQtyAvail;
        inserted1 = false;
        if (nullable3.GetValueOrDefault() == inserted1 & nullable3.HasValue)
        {
          nullable2 = serialStatusAggregate1.QtyAvail;
          Decimal num4 = 0M;
          if (nullable2.GetValueOrDefault() < num4 & nullable2.HasValue)
            serialStatusAggregate1.QtyAvail = new Decimal?(0M);
        }
      }
    }
    PXSelectBase<INLocationStatusByCostCenter> cmd2 = (PXSelectBase<INLocationStatusByCostCenter>) new PXSelectReadonly2<INLocationStatusByCostCenter, InnerJoin<INLocation, On<INLocationStatusByCostCenter.FK.Location>, InnerJoin<InventoryItem, On<InventoryItem.inventoryID, Equal<INLocationStatusByCostCenter.inventoryID>, And<Match<InventoryItem, Current<AccessInfo.userName>>>>, InnerJoin<INSite, On2<INLocationStatusByCostCenter.FK.Site, And<INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>, And<Match<INSite, Current<AccessInfo.userName>>>>>, InnerJoin<INSubItem, On<INLocationStatusByCostCenter.FK.SubItem>, LeftJoin<INCostCenter, On<INLocationStatusByCostCenter.FK.CostCenter>>>>>>, Where<INLocationStatusByCostCenter.inventoryID, Equal<Current<InventorySummaryEnqFilter.inventoryID>>>>((PXGraph) this);
    this.AppendCostLocationLayerJoin(cmd2);
    this.AppendFilters<INLocationStatusByCostCenter>(cmd2, filter);
    foreach (PXResult<INLocationStatusByCostCenter, INLocation, InventoryItem, INSite, INSubItem, INCostCenter> pxResult in cmd2.Select(Array.Empty<object>()))
    {
      INLocationStatusByCostCenter other = PXResult<INLocationStatusByCostCenter, INLocation, InventoryItem, INSite, INSubItem, INCostCenter>.op_Implicit(pxResult);
      INLocation inLocation = PXResult<INLocationStatusByCostCenter, INLocation, InventoryItem, INSite, INSubItem, INCostCenter>.op_Implicit(pxResult);
      InventoryItem inventoryItem = PXResult<INLocationStatusByCostCenter, INLocation, InventoryItem, INSite, INSubItem, INCostCenter>.op_Implicit(pxResult);
      INCostCenter inCostCenter = PXResult<INLocationStatusByCostCenter, INLocation, InventoryItem, INSite, INSubItem, INCostCenter>.op_Implicit(pxResult);
      LocationStatusAggregate ret = new LocationStatusAggregate()
      {
        InventoryID = other.InventoryID,
        SubItemID = other.SubItemID,
        SiteID = other.SiteID,
        LocationID = other.LocationID,
        CostCenterID = other.CostCenterID
      };
      bool inserted3;
      ret = this.LocateOrInsertLocationAggregate((LocationStatusAggregate.ITable) ret, false, out inserted3);
      ret.Add<LocationStatusAggregate>((IStatus) other);
      if (inserted3)
      {
        ret.CostLayerType = inCostCenter.CostLayerType ?? "N";
        ret.SubItemCD = PXResult<INLocationStatusByCostCenter, INLocation, InventoryItem, INSite, INSubItem, INCostCenter>.op_Implicit(pxResult).SubItemCD;
        ret.SiteCD = PXResult<INLocationStatusByCostCenter, INLocation, InventoryItem, INSite, INSubItem, INCostCenter>.op_Implicit(pxResult).SiteCD;
        ret.LocationCD = PXResult<INLocationStatusByCostCenter, INLocation, InventoryItem, INSite, INSubItem, INCostCenter>.op_Implicit(pxResult).LocationCD;
        ret.BaseUnit = inventoryItem.BaseUnit;
      }
      int? costCenterId = other.CostCenterID;
      int num5 = 0;
      INLocationCostStatus locationCostStatus;
      if (!(costCenterId.GetValueOrDefault() == num5 & costCenterId.HasValue))
        locationCostStatus = (INLocationCostStatus) null;
      else
        locationCostStatus = ((PXSelectBase<INLocationCostStatus>) pxSelectReadonly3).Locate(new INLocationCostStatus()
        {
          InventoryID = other.InventoryID,
          SubItemID = other.SubItemID,
          LocationID = other.LocationID,
          SiteID = other.SiteID
        });
      ICostStatus st = (ICostStatus) locationCostStatus;
      Decimal? nullable9;
      int num6;
      if (st == null)
      {
        num6 = 1;
      }
      else
      {
        nullable9 = st.TotalCost;
        num6 = !nullable9.HasValue ? 1 : 0;
      }
      if (num6 != 0)
        st = (ICostStatus) ((PXSelectBase<INSiteCostStatusByCostLayerType>) pxSelectReadonly2).Locate(new INSiteCostStatusByCostLayerType()
        {
          InventoryID = other.InventoryID,
          SubItemID = other.SubItemID,
          SiteID = other.SiteID,
          CostLayerType = inCostCenter.CostLayerType ?? "N"
        });
      LocationStatusAggregate locationStatusAggregate1 = ret;
      nullable9 = locationStatusAggregate1.TotalCost;
      Decimal unitCost = this.CalculateUnitCost(st, false);
      Decimal? nullable10 = other.QtyOnHand;
      Decimal? nullable11 = nullable10.HasValue ? new Decimal?(unitCost * nullable10.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable12;
      if (!(nullable9.HasValue & nullable11.HasValue))
      {
        nullable10 = new Decimal?();
        nullable12 = nullable10;
      }
      else
        nullable12 = new Decimal?(nullable9.GetValueOrDefault() + nullable11.GetValueOrDefault());
      locationStatusAggregate1.TotalCost = nullable12;
      bool? nullable13 = inLocation.InclQtyAvail;
      inserted1 = false;
      if (nullable13.GetValueOrDefault() == inserted1 & nullable13.HasValue)
      {
        ret.QtyNotAvail = ret.QtyAvail;
        ret.QtyAvail = new Decimal?(0M);
        ret.QtyHardAvail = new Decimal?(0M);
        ret.QtyActual = new Decimal?(0M);
      }
      else
        ret.QtyNotAvail = new Decimal?(0M);
      Lazy<SiteStatusAggregate> lazy = new Lazy<SiteStatusAggregate>((Func<SiteStatusAggregate>) (() => this.LocateOrInsertSiteAggregate((SiteStatusAggregate.ITable) ret, true, out bool _)));
      if (PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>())
      {
        lazy.Value.Add<SiteStatusAggregate>((IStatus) ret);
      }
      else
      {
        bool flag = inventoryItem.ValMethod == "S";
        if (!flag)
        {
          INLotSerClass inLotSerClass = INLotSerClass.PK.Find((PXGraph) this, inventoryItem.LotSerClassID);
          int num7;
          if (inLotSerClass?.LotSerTrack != "N")
          {
            if (inLotSerClass != null)
            {
              nullable13 = inLotSerClass.LotSerTrackExpiration;
              if (nullable13.GetValueOrDefault())
              {
                num7 = 1;
                goto label_96;
              }
            }
            nullable13 = filter.ExpandByLotSerialNbr;
            num7 = !nullable13.GetValueOrDefault() ? 0 : (inLotSerClass?.LotSerAssign == "R" ? 1 : 0);
          }
          else
            num7 = 0;
label_96:
          flag = num7 != 0;
        }
        if (!flag)
        {
          SiteStatusAggregate siteStatusAggregate1 = lazy.Value;
          nullable11 = siteStatusAggregate1.QtyOnHand;
          nullable9 = ret.QtyOnHand;
          Decimal? nullable14;
          if (!(nullable11.HasValue & nullable9.HasValue))
          {
            nullable10 = new Decimal?();
            nullable14 = nullable10;
          }
          else
            nullable14 = new Decimal?(nullable11.GetValueOrDefault() + nullable9.GetValueOrDefault());
          siteStatusAggregate1.QtyOnHand = nullable14;
          SiteStatusAggregate siteStatusAggregate2 = lazy.Value;
          nullable9 = siteStatusAggregate2.TotalCost;
          nullable11 = ret.TotalCost;
          Decimal? nullable15;
          if (!(nullable9.HasValue & nullable11.HasValue))
          {
            nullable10 = new Decimal?();
            nullable15 = nullable10;
          }
          else
            nullable15 = new Decimal?(nullable9.GetValueOrDefault() + nullable11.GetValueOrDefault());
          siteStatusAggregate2.TotalCost = nullable15;
        }
      }
      LocationStatusAggregate locationStatusAggregate2 = this.LocateOrInsertLocationAggregate((LocationStatusAggregate.ITable) ret, true, out inserted1);
      nullable13 = filter.ExpandByLotSerialNbr;
      if (nullable13.GetValueOrDefault())
      {
        ret = ret.Subtract<LocationStatusAggregate>((IStatus) locationStatusAggregate2);
      }
      else
      {
        nullable11 = locationStatusAggregate2.TotalCost;
        Decimal num8 = 0M;
        if (!(nullable11.GetValueOrDefault() == num8 & nullable11.HasValue))
        {
          ret.TotalCost = locationStatusAggregate2.TotalCost;
          ret.UnitCost = new Decimal?(this.CalculateUnitCost((ICostStatus) locationStatusAggregate2, true));
        }
        LocationStatusAggregate locationStatusAggregate3 = ret;
        nullable11 = locationStatusAggregate3.QtyExpired;
        nullable9 = locationStatusAggregate2.QtyExpired;
        Decimal? nullable16;
        if (!(nullable11.HasValue & nullable9.HasValue))
        {
          nullable10 = new Decimal?();
          nullable16 = nullable10;
        }
        else
          nullable16 = new Decimal?(nullable11.GetValueOrDefault() + nullable9.GetValueOrDefault());
        locationStatusAggregate3.QtyExpired = nullable16;
        LocationStatusAggregate locationStatusAggregate4 = ret;
        nullable9 = locationStatusAggregate4.QtyAvail;
        nullable11 = locationStatusAggregate2.QtyExpired;
        Decimal? nullable17;
        if (!(nullable9.HasValue & nullable11.HasValue))
        {
          nullable10 = new Decimal?();
          nullable17 = nullable10;
        }
        else
          nullable17 = new Decimal?(nullable9.GetValueOrDefault() - nullable11.GetValueOrDefault());
        locationStatusAggregate4.QtyAvail = nullable17;
        nullable13 = inLocation.InclQtyAvail;
        inserted1 = false;
        if (nullable13.GetValueOrDefault() == inserted1 & nullable13.HasValue)
        {
          nullable11 = ret.QtyAvail;
          Decimal num9 = 0M;
          if (nullable11.GetValueOrDefault() < num9 & nullable11.HasValue)
            ret.QtyAvail = new Decimal?(0M);
        }
      }
    }
    if (filter.ExpandByLotSerialNbr.GetValueOrDefault())
    {
      IEnumerable<PXResult<INPlanType>> source = ((IEnumerable<PXResult<INPlanType>>) PXSelectBase<INPlanType, PXSelectReadonly<INPlanType, Where<INPlanType.inclQtySOShipping, Equal<True>>>.Config>.SelectMultiBound((PXGraph) this, (object[]) null, Array.Empty<object>())).AsEnumerable<PXResult<INPlanType>>();
      PXSelectBase<INItemPlan> cmd3 = (PXSelectBase<INItemPlan>) new PXSelectReadonly2<INItemPlan, InnerJoin<InventoryItem, On2<INItemPlan.FK.InventoryItem, And<Match<InventoryItem, Current<AccessInfo.userName>>>>, InnerJoin<INSite, On2<INItemPlan.FK.Site, And<Match<INSite, Current<AccessInfo.userName>>>>, InnerJoin<INSubItem, On<INItemPlan.FK.SubItem>, InnerJoin<INPlanType, On<INItemPlan.FK.PlanType>>>>>, Where<INPlanType.inclQtySOShipping, Equal<decimal1>, And<INItemPlan.inventoryID, Equal<Current<InventorySummaryEnqFilter.inventoryID>>>>>((PXGraph) this);
      this.AppendFilters<INItemPlan>(cmd3, filter);
      List<PXResult<INItemPlan, InventoryItem, INSite, INSubItem>> pxResultList = new List<PXResult<INItemPlan, InventoryItem, INSite, INSubItem>>();
      foreach (PXResult<INItemPlan, InventoryItem, INSite, INSubItem> pxResult in cmd3.Select(Array.Empty<object>()))
        pxResultList.Add(pxResult);
      bool? nullable18;
      int? nullable19;
      for (int index1 = 0; index1 < pxResultList.Count; ++index1)
      {
        INItemPlan plan_rec = PXResult<INItemPlan, InventoryItem, INSite, INSubItem>.op_Implicit(pxResultList[index1]);
        if (source.Any<PXResult<INPlanType>>((Func<PXResult<INPlanType>, bool>) (x => PXResult<INPlanType>.op_Implicit(x).PlanType == plan_rec.OrigPlanType)))
        {
          for (int index2 = 0; index2 < pxResultList.Count; ++index2)
          {
            INItemPlan inItemPlan1 = PXResult<INItemPlan, InventoryItem, INSite, INSubItem>.op_Implicit(pxResultList[index2]);
            long? planId = inItemPlan1.PlanID;
            long? origPlanId = plan_rec.OrigPlanID;
            if (!(planId.GetValueOrDefault() == origPlanId.GetValueOrDefault() & planId.HasValue == origPlanId.HasValue))
            {
              Guid? refNoteId = inItemPlan1.RefNoteID;
              Guid? origNoteId = plan_rec.OrigNoteID;
              if ((refNoteId.HasValue == origNoteId.HasValue ? (refNoteId.HasValue ? (refNoteId.GetValueOrDefault() == origNoteId.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && inItemPlan1.PlanType == plan_rec.OrigPlanType)
              {
                bool? reverse = inItemPlan1.Reverse;
                nullable18 = plan_rec.Reverse;
                if (reverse.GetValueOrDefault() == nullable18.GetValueOrDefault() & reverse.HasValue == nullable18.HasValue)
                {
                  nullable19 = inItemPlan1.SubItemID;
                  int? nullable20 = plan_rec.SubItemID;
                  if (nullable19.GetValueOrDefault() == nullable20.GetValueOrDefault() & nullable19.HasValue == nullable20.HasValue)
                  {
                    nullable20 = inItemPlan1.SiteID;
                    nullable19 = plan_rec.SiteID;
                    if (nullable20.GetValueOrDefault() == nullable19.GetValueOrDefault() & nullable20.HasValue == nullable19.HasValue)
                    {
                      nullable19 = inItemPlan1.LocationID;
                      if (nullable19.HasValue || !string.Equals(inItemPlan1.LotSerialNbr, plan_rec.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
                        continue;
                    }
                    else
                      continue;
                  }
                  else
                    continue;
                }
                else
                  continue;
              }
              else
                continue;
            }
            INItemPlan inItemPlan2 = inItemPlan1;
            Decimal? planQty1 = inItemPlan2.PlanQty;
            Decimal? planQty2 = plan_rec.PlanQty;
            inItemPlan2.PlanQty = planQty1.HasValue & planQty2.HasValue ? new Decimal?(planQty1.GetValueOrDefault() - planQty2.GetValueOrDefault()) : new Decimal?();
            plan_rec.PlanQty = new Decimal?(0M);
            break;
          }
        }
      }
      foreach (PXResult<INItemPlan, InventoryItem, INSite, INSubItem> pxResult in pxResultList)
      {
        INItemPlan plan_rec = PXResult<INItemPlan, InventoryItem, INSite, INSubItem>.op_Implicit(pxResult);
        PXResult<INItemPlan, InventoryItem, INSite, INSubItem>.op_Implicit(pxResult);
        Decimal? planQty = plan_rec.PlanQty;
        Decimal num = 0M;
        if (!(planQty.GetValueOrDefault() == num & planQty.HasValue) && !string.IsNullOrEmpty(plan_rec.LotSerialNbr))
        {
          nullable19 = plan_rec.LocationID;
          if (!nullable19.HasValue)
          {
            InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, plan_rec.InventoryID);
            INSubItem inSubItem = INSubItem.PK.Find((PXGraph) this, plan_rec.SubItemID);
            INSite inSite = INSite.PK.Find((PXGraph) this, plan_rec.SiteID);
            INItemLotSerial inItemLotSerial = INItemLotSerial.PK.Find((PXGraph) this, plan_rec.InventoryID, plan_rec.LotSerialNbr);
            INCostCenter inCostCenter = INCostCenter.PK.Find((PXGraph) this, plan_rec.CostCenterID);
            PXCache<SiteStatusByCostCenter> pxCache1 = GraphHelper.Caches<SiteStatusByCostCenter>((PXGraph) this);
            SiteStatusByCostCenter statusByCostCenter = new SiteStatusByCostCenter();
            statusByCostCenter.SiteID = plan_rec.SiteID;
            statusByCostCenter.InventoryID = plan_rec.InventoryID;
            statusByCostCenter.SubItemID = plan_rec.SubItemID;
            statusByCostCenter.CostCenterID = plan_rec.CostCenterID;
            SiteStatusByCostCenter other = pxCache1.Insert(statusByCostCenter);
            InventorySummaryEnq.ItemPlanHelper itemPlanHelperExt = this.ItemPlanHelperExt;
            SiteStatusByCostCenter target = other;
            INItemPlan plan = plan_rec;
            INPlanType plantype = PXResult<INPlanType>.op_Implicit(source.First<PXResult<INPlanType>>((Func<PXResult<INPlanType>, bool>) (x => PXResult<INPlanType>.op_Implicit(x).PlanType == plan_rec.PlanType)));
            nullable18 = other.InclQtyAvail;
            bool? InclQtyAvail = new bool?(nullable18.GetValueOrDefault());
            bool? hold = plan_rec.Hold;
            string refEntityType = plan_rec.RefEntityType;
            itemPlanHelperExt.UpdateAllocatedQuantitiesBase<SiteStatusByCostCenter>(target, (IQtyPlanned) plan, plantype, InclQtyAvail, hold, refEntityType);
            PXCache<LotSerialStatusAggregate> pxCache2 = GraphHelper.Caches<LotSerialStatusAggregate>((PXGraph) this);
            LotSerialStatusAggregate serialStatusAggregate = new LotSerialStatusAggregate()
            {
              SiteID = plan_rec.SiteID,
              SiteCD = inSite?.SiteCD,
              InventoryID = plan_rec.InventoryID,
              BaseUnit = inventoryItem?.BaseUnit,
              SubItemID = plan_rec.SubItemID,
              SubItemCD = inSubItem?.SubItemCD,
              LocationID = new int?(-3),
              LotSerialNbr = plan_rec.LotSerialNbr,
              ExpireDate = (DateTime?) inItemLotSerial?.ExpireDate,
              CostCenterID = plan_rec.CostCenterID,
              CostLayerType = inCostCenter?.CostLayerType ?? "N"
            };
            (pxCache2.Locate(serialStatusAggregate) ?? pxCache2.Insert(serialStatusAggregate)).Add<LotSerialStatusAggregate>((IStatus) other);
            GraphHelper.Caches<SiteStatusAggregate>((PXGraph) this);
            this.LocateOrInsertSiteAggregate((SiteStatusAggregate.ITable) new SiteStatusAggregate()
            {
              SiteID = plan_rec.SiteID,
              SiteCD = inSite?.SiteCD,
              InventoryID = plan_rec.InventoryID,
              BaseUnit = inventoryItem?.BaseUnit,
              SubItemID = plan_rec.SubItemID,
              SubItemCD = inSubItem?.SubItemCD,
              CostCenterID = plan_rec.CostCenterID,
              CostLayerType = (inCostCenter?.CostLayerType ?? "N")
            }, false, out inserted1).Subtract<SiteStatusAggregate>((IStatus) other);
            ((PXCache) pxCache1).Clear();
          }
        }
      }
    }
    PXSelectBase<INSiteStatusByCostCenter> cmd4 = (PXSelectBase<INSiteStatusByCostCenter>) new PXSelectReadonly2<INSiteStatusByCostCenter, InnerJoin<InventoryItem, On<InventoryItem.inventoryID, Equal<INSiteStatusByCostCenter.inventoryID>, And<Match<InventoryItem, Current<AccessInfo.userName>>>>, InnerJoin<INSite, On2<INSiteStatusByCostCenter.FK.Site, And<INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>, And<Match<INSite, Current<AccessInfo.userName>>>>>, InnerJoin<INSubItem, On<INSiteStatusByCostCenter.FK.SubItem>, LeftJoin<INCostCenter, On<INSiteStatusByCostCenter.FK.CostCenter>>>>>, Where<INSiteStatusByCostCenter.inventoryID, Equal<Current<InventorySummaryEnqFilter.inventoryID>>>>((PXGraph) this);
    this.AppendFilters<INSiteStatusByCostCenter>(cmd4, filter);
    foreach (PXResult<INSiteStatusByCostCenter, InventoryItem, INSite, INSubItem, INCostCenter> pxResult in cmd4.Select(Array.Empty<object>()))
    {
      INSiteStatusByCostCenter other = PXResult<INSiteStatusByCostCenter, InventoryItem, INSite, INSubItem, INCostCenter>.op_Implicit(pxResult);
      InventoryItem inventoryItem = PXResult<INSiteStatusByCostCenter, InventoryItem, INSite, INSubItem, INCostCenter>.op_Implicit(pxResult);
      INCostCenter inCostCenter = PXResult<INSiteStatusByCostCenter, InventoryItem, INSite, INSubItem, INCostCenter>.op_Implicit(pxResult);
      SiteStatusAggregate siteStatusAggregate3 = this.LocateOrInsertSiteAggregate((SiteStatusAggregate.ITable) new SiteStatusAggregate()
      {
        InventoryID = other.InventoryID,
        SubItemID = other.SubItemID,
        SiteID = other.SiteID,
        CostCenterID = other.CostCenterID
      }, false, out inserted1);
      siteStatusAggregate3.Add<SiteStatusAggregate>((IStatus) other);
      siteStatusAggregate3.CostLayerType = inCostCenter.CostLayerType ?? "N";
      siteStatusAggregate3.SubItemCD = PXResult<INSiteStatusByCostCenter, InventoryItem, INSite, INSubItem, INCostCenter>.op_Implicit(pxResult).SubItemCD;
      siteStatusAggregate3.SiteCD = PXResult<INSiteStatusByCostCenter, InventoryItem, INSite, INSubItem, INCostCenter>.op_Implicit(pxResult).SiteCD;
      siteStatusAggregate3.BaseUnit = inventoryItem.BaseUnit;
      siteStatusAggregate3.TotalCost = new Decimal?(0M);
      siteStatusAggregate3.QtyExpired = new Decimal?(0M);
      SiteStatusAggregate siteStatusAggregate4 = this.LocateOrInsertSiteAggregate((SiteStatusAggregate.ITable) siteStatusAggregate3, true, out inserted1);
      if (PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>() || filter.ExpandByLotSerialNbr.GetValueOrDefault())
      {
        siteStatusAggregate3.Subtract<SiteStatusAggregate>((IStatus) siteStatusAggregate4).TotalCost = new Decimal?(0M);
      }
      else
      {
        Decimal? nullable21 = siteStatusAggregate4.TotalCost;
        Decimal num = 0M;
        if (!(nullable21.GetValueOrDefault() == num & nullable21.HasValue))
        {
          siteStatusAggregate3.TotalCost = siteStatusAggregate4.TotalCost;
          siteStatusAggregate3.UnitCost = new Decimal?(this.CalculateUnitCost((ICostStatus) siteStatusAggregate4, true));
        }
        SiteStatusAggregate siteStatusAggregate5 = siteStatusAggregate3;
        nullable21 = siteStatusAggregate5.QtyExpired;
        Decimal? nullable22 = siteStatusAggregate4.QtyExpired;
        siteStatusAggregate5.QtyExpired = nullable21.HasValue & nullable22.HasValue ? new Decimal?(nullable21.GetValueOrDefault() + nullable22.GetValueOrDefault()) : new Decimal?();
        SiteStatusAggregate siteStatusAggregate6 = siteStatusAggregate3;
        nullable22 = siteStatusAggregate6.QtyAvail;
        nullable21 = siteStatusAggregate4.QtyExpired;
        siteStatusAggregate6.QtyAvail = nullable22.HasValue & nullable21.HasValue ? new Decimal?(nullable22.GetValueOrDefault() - nullable21.GetValueOrDefault()) : new Decimal?();
      }
    }
    return ((IEnumerable<SiteStatusAggregate.ITable>) Array<SiteStatusAggregate.ITable>.Empty).Concat<SiteStatusAggregate.ITable>((IEnumerable<SiteStatusAggregate.ITable>) this.AggregateRecords(((PXCache) GraphHelper.Caches<LotSerialStatusAggregate>((PXGraph) this)).Inserted.Cast<LotSerialStatusAggregate>().Where<LotSerialStatusAggregate>((Func<LotSerialStatusAggregate, bool>) (s => filter.ExpandByLotSerialNbr.GetValueOrDefault() && !s.IsZero())), r => new
    {
      InventoryID = r.InventoryID,
      SubItemID = r.SubItemID,
      SiteID = r.SiteID,
      LocationID = r.LocationID,
      LotSerialNbr = r.LotSerialNbr,
      CostLayerType = this.GetCostLayerTypeForAggregate((SiteStatusAggregate.ITable) r)
    })).Concat<SiteStatusAggregate.ITable>((IEnumerable<SiteStatusAggregate.ITable>) this.AggregateRecords(((PXCache) GraphHelper.Caches<LocationStatusAggregate>((PXGraph) this)).Inserted.Cast<LocationStatusAggregate>().Where<LocationStatusAggregate>((Func<LocationStatusAggregate, bool>) (s => PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>() && !s.ExcludeRecord.GetValueOrDefault() && !s.IsZero())), r => new
    {
      InventoryID = r.InventoryID,
      SubItemID = r.SubItemID,
      SiteID = r.SiteID,
      LocationID = r.LocationID,
      CostLayerType = this.GetCostLayerTypeForAggregate((SiteStatusAggregate.ITable) r)
    })).Concat<SiteStatusAggregate.ITable>((IEnumerable<SiteStatusAggregate.ITable>) this.AggregateRecords(((PXCache) GraphHelper.Caches<SiteStatusAggregate>((PXGraph) this)).Inserted.Cast<SiteStatusAggregate>().Where<SiteStatusAggregate>((Func<SiteStatusAggregate, bool>) (s => (!PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>() || !filter.LocationID.HasValue) && !s.ExcludeRecord.GetValueOrDefault() && !s.IsZero())), r => new
    {
      InventoryID = r.InventoryID,
      SubItemID = r.SubItemID,
      SiteID = r.SiteID,
      CostLayerType = this.GetCostLayerTypeForAggregate((SiteStatusAggregate.ITable) r)
    })).OrderBy<SiteStatusAggregate.ITable, string>((Func<SiteStatusAggregate.ITable, string>) (s => s.CostLayerType)).ThenBy<SiteStatusAggregate.ITable, string>((Func<SiteStatusAggregate.ITable, string>) (s => s.SubItemCD)).ThenBy<SiteStatusAggregate.ITable, string>((Func<SiteStatusAggregate.ITable, string>) (s => s.SiteCD)).ThenBy<SiteStatusAggregate.ITable, string>((Func<SiteStatusAggregate.ITable, string>) (s => !(s is LocationStatusAggregate.ITable table) ? (string) null : table.LocationCD)).ThenBy<SiteStatusAggregate.ITable, string>((Func<SiteStatusAggregate.ITable, string>) (s => !(s is LotSerialStatusAggregate serialStatusAggregate4) ? (string) null : serialStatusAggregate4.LotSerialNbr)).Select<SiteStatusAggregate.ITable, InventorySummaryEnquiryResult>((Func<SiteStatusAggregate.ITable, InventorySummaryEnquiryResult>) (s => this.ConvertToResult(s, controlTimeStamp)));
  }

  private string GetCostLayerTypeForAggregate(SiteStatusAggregate.ITable t)
  {
    return !((PXSelectBase<InventorySummaryEnqFilter>) this.Filter).Current.ExpandByCostLayerType.GetValueOrDefault() ? "N" : t.CostLayerType;
  }

  private IEnumerable<T> AggregateRecords<T, TKey>(
    IEnumerable<T> enumerable,
    Func<T, TKey> groupSel)
    where T : class, SiteStatusAggregate.ITable, new()
  {
    InventorySummaryEnq graph = this;
    foreach (IGrouping<TKey, T> grouping in enumerable.GroupBy<T, TKey>(groupSel))
    {
      T obj = default (T);
      foreach (T other in (IEnumerable<T>) grouping)
      {
        if ((object) obj == null)
          obj = (T) ((PXGraph) graph).Caches[typeof (T)].CreateCopy((object) other);
        else
          obj.Add<T>((IStatus) other);
      }
      obj.UnitCost = new Decimal?(graph.CalculateUnitCost((ICostStatus) obj, true));
      obj.TotalCost = new Decimal?(PXDBCurrencyAttribute.BaseRound((PXGraph) graph, obj.TotalCost.GetValueOrDefault()));
      yield return obj;
    }
  }

  private InventorySummaryEnquiryResult ConvertToResult(
    SiteStatusAggregate.ITable s,
    string controlTimeStamp)
  {
    InventorySummaryEnquiryResult it = new InventorySummaryEnquiryResult()
    {
      InventoryID = s.InventoryID,
      SubItemID = s.SubItemID,
      SiteID = s.SiteID,
      CostLayerType = s.CostLayerType,
      BaseUnit = s.BaseUnit,
      UnitCost = s.UnitCost,
      TotalCost = s.TotalCost,
      ControlTimeStamp = controlTimeStamp
    };
    it.OverrideBy<InventorySummaryEnquiryResult>((IStatus) s);
    switch (s)
    {
      case LotSerialStatusAggregate serialStatusAggregate:
        InventorySummaryEnquiryResult summaryEnquiryResult = it;
        int? nullable1 = serialStatusAggregate.LocationID;
        int? nullable2;
        if (nullable1.GetValueOrDefault() != -3)
        {
          nullable2 = serialStatusAggregate.LocationID;
        }
        else
        {
          nullable1 = new int?();
          nullable2 = nullable1;
        }
        summaryEnquiryResult.LocationID = nullable2;
        it.LotSerialNbr = serialStatusAggregate.LotSerialNbr;
        it.ExpireDate = serialStatusAggregate.ExpireDate;
        break;
      case LocationStatusAggregate locationStatusAggregate:
        it.LocationID = locationStatusAggregate.LocationID;
        break;
    }
    return it;
  }

  private Decimal CalculateUnitCost(ICostStatus st, bool round)
  {
    Decimal? nullable1 = (Decimal?) st?.QtyOnHand;
    Decimal? nullable2;
    Decimal? nullable3;
    if (!(nullable1.GetValueOrDefault() != 0M))
    {
      nullable3 = new Decimal?(0M);
    }
    else
    {
      nullable1 = st.TotalCost;
      nullable2 = st.QtyOnHand;
      nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / nullable2.GetValueOrDefault()) : new Decimal?();
    }
    nullable2 = nullable3;
    Decimal valueOrDefault = nullable2.GetValueOrDefault();
    return !round ? valueOrDefault : Math.Round(valueOrDefault, (int) ((PXSelectBase<CommonSetup>) this.commonsetup).Current.DecPlPrcCst.Value, MidpointRounding.AwayFromZero);
  }

  protected virtual IEnumerable SortSummaryResult(
    IEnumerable<InventorySummaryEnquiryResult> resultset,
    InventorySummaryEnquiryResult total)
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultSorted = true
    };
    IEnumerable<InventorySummaryEnquiryResult> collection = GraphHelper.RowCast<InventorySummaryEnquiryResult>(PXView.Sort((IEnumerable) resultset));
    if (resultset.Any<InventorySummaryEnquiryResult>())
    {
      if (!PXView.ReverseOrder)
      {
        ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) collection);
        ((List<object>) pxDelegateResult).Add((object) total);
      }
      else
      {
        ((List<object>) pxDelegateResult).Add((object) total);
        ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) collection);
      }
    }
    return (IEnumerable) pxDelegateResult;
  }

  protected virtual InventorySummaryEnquiryResult CalculateSummaryTotal(
    IEnumerable<InventorySummaryEnquiryResult> resultset)
  {
    InventorySummaryEnquiryResult sumTotal = resultset.CalculateSumTotal<InventorySummaryEnquiryResult>(((PXSelectBase) this.ISERecords).Cache);
    sumTotal.IsTotal = new bool?(true);
    sumTotal.LocationID = new int?(-1);
    sumTotal.SiteID = new int?();
    sumTotal.UnitCost = new Decimal?();
    return sumTotal;
  }

  private SiteStatusAggregate LocateOrInsertSiteAggregate(
    SiteStatusAggregate.ITable ret,
    bool exclude,
    out bool inserted)
  {
    SiteStatusAggregate siteStatusAggregate1 = new SiteStatusAggregate()
    {
      InventoryID = ret.InventoryID,
      SubItemID = ret.SubItemID,
      SiteID = ret.SiteID,
      CostCenterID = ret.CostCenterID,
      ExcludeRecord = new bool?(exclude)
    };
    PXCache pxCache = (PXCache) GraphHelper.Caches<SiteStatusAggregate>((PXGraph) this);
    SiteStatusAggregate siteStatusAggregate2 = (SiteStatusAggregate) pxCache.Locate((object) siteStatusAggregate1);
    if (siteStatusAggregate2 != null)
    {
      inserted = false;
      return siteStatusAggregate2;
    }
    inserted = true;
    return (SiteStatusAggregate) pxCache.Insert((object) siteStatusAggregate1);
  }

  private LocationStatusAggregate LocateOrInsertLocationAggregate(
    LocationStatusAggregate.ITable ret,
    bool exclude,
    out bool inserted)
  {
    LocationStatusAggregate locationStatusAggregate1 = new LocationStatusAggregate()
    {
      InventoryID = ret.InventoryID,
      SubItemID = ret.SubItemID,
      SiteID = ret.SiteID,
      LocationID = ret.LocationID,
      CostCenterID = ret.CostCenterID,
      ExcludeRecord = new bool?(exclude)
    };
    PXCache pxCache = (PXCache) GraphHelper.Caches<LocationStatusAggregate>((PXGraph) this);
    LocationStatusAggregate locationStatusAggregate2 = (LocationStatusAggregate) pxCache.Locate((object) locationStatusAggregate1);
    if (locationStatusAggregate2 != null)
    {
      inserted = false;
      return locationStatusAggregate2;
    }
    inserted = true;
    return (LocationStatusAggregate) pxCache.Insert((object) locationStatusAggregate1);
  }

  private LotSerialStatusAggregate LocateOrInsertLotSerialAggregate(
    INLotSerialStatusByCostCenter st,
    out bool inserted)
  {
    LotSerialStatusAggregate serialStatusAggregate1 = new LotSerialStatusAggregate()
    {
      InventoryID = st.InventoryID,
      SubItemID = st.SubItemID,
      SiteID = st.SiteID,
      LocationID = st.LocationID,
      LotSerialNbr = st.LotSerialNbr,
      CostCenterID = st.CostCenterID
    };
    PXCache pxCache = (PXCache) GraphHelper.Caches<LotSerialStatusAggregate>((PXGraph) this);
    LotSerialStatusAggregate serialStatusAggregate2 = (LotSerialStatusAggregate) pxCache.Locate((object) serialStatusAggregate1);
    if (serialStatusAggregate2 != null)
    {
      inserted = false;
      return serialStatusAggregate2;
    }
    inserted = true;
    return (LotSerialStatusAggregate) pxCache.Insert((object) serialStatusAggregate1);
  }

  public virtual bool IsDirty => false;

  [PXUIField(DisplayName = "")]
  [PXEditDetailButton]
  protected virtual IEnumerable ViewAllocDet(PXAdapter a)
  {
    if (((PXSelectBase<InventorySummaryEnquiryResult>) this.ISERecords).Current != null)
    {
      object valueExt = ((PXSelectBase) this.ISERecords).Cache.GetValueExt<InventorySummaryEnquiryResult.subItemID>((object) ((PXSelectBase<InventorySummaryEnquiryResult>) this.ISERecords).Current);
      if (valueExt is PXSegmentedState)
        valueExt = ((PXFieldState) valueExt).Value;
      InventoryAllocDetEnq.Redirect(((PXSelectBase<InventorySummaryEnquiryResult>) this.ISERecords).Current.InventoryID, valueExt != null ? (string) valueExt : (string) null, ((PXSelectBase<InventorySummaryEnqFilter>) this.Filter).Current.ExpandByLotSerialNbr.GetValueOrDefault() ? ((PXSelectBase<InventorySummaryEnquiryResult>) this.ISERecords).Current.LotSerialNbr : (string) null, ((PXSelectBase<InventorySummaryEnquiryResult>) this.ISERecords).Current.SiteID, ((PXSelectBase<InventorySummaryEnquiryResult>) this.ISERecords).Current.LocationID);
    }
    return a.Get();
  }

  [PXButton(DisplayOnMainToolbar = false)]
  [PXUIField]
  protected virtual IEnumerable ViewItem(PXAdapter a)
  {
    if (((PXSelectBase<InventorySummaryEnquiryResult>) this.ISERecords).Current != null)
      InventoryItemMaint.Redirect(((PXSelectBase<InventorySummaryEnquiryResult>) this.ISERecords).Current.InventoryID, true);
    return a.Get();
  }

  public static void Redirect(int? inventoryID, string subItemCD, int? siteID, int? locationID)
  {
    InventorySummaryEnq.Redirect(inventoryID, subItemCD, siteID, locationID, true);
  }

  public static void Redirect(
    int? inventoryID,
    string subItemCD,
    int? siteID,
    int? locationID,
    bool newWindow)
  {
    InventorySummaryEnq instance = PXGraph.CreateInstance<InventorySummaryEnq>();
    InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) instance, inventoryID);
    if (inventoryItem != null && inventoryItem.IsConverted.GetValueOrDefault() && !inventoryItem.StkItem.GetValueOrDefault())
      throw new PXException("The {0} item has been converted to a non-stock item.", new object[1]
      {
        (object) inventoryItem.InventoryCD.Trim()
      });
    ((PXSelectBase<InventorySummaryEnqFilter>) instance.Filter).Current.InventoryID = inventoryID;
    ((PXSelectBase<InventorySummaryEnqFilter>) instance.Filter).Current.SubItemCD = subItemCD;
    ((PXSelectBase<InventorySummaryEnqFilter>) instance.Filter).Current.SiteID = siteID;
    ((PXSelectBase<InventorySummaryEnqFilter>) instance.Filter).Current.LocationID = locationID;
    if (newWindow)
    {
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Inventory Summary");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    throw new PXRedirectRequiredException((PXGraph) instance, "Inventory Summary");
  }

  private string ControlTimeStamp
  {
    get
    {
      if (!this.timestampSelected)
      {
        PXDatabase.SelectTimeStamp();
        this.timestampSelected = true;
      }
      InventorySummaryEnq.Definition slot = PXContext.GetSlot<InventorySummaryEnq.Definition>();
      if (slot == null)
      {
        Type[] typeArray = new Type[13]
        {
          typeof (InventoryItem),
          typeof (INSubItem),
          typeof (INItemPlan),
          typeof (INSite),
          typeof (INSiteStatusByCostCenter),
          typeof (CommonSetup),
          typeof (INLocation),
          typeof (INLocationStatusByCostCenter),
          typeof (INLotSerClass),
          typeof (INLotSerialStatusByCostCenter),
          typeof (INItemLotSerial),
          typeof (INCostStatus),
          typeof (INCostSubItemXRef)
        };
        PXContext.SetSlot<InventorySummaryEnq.Definition>(slot = PXDatabase.GetSlot<InventorySummaryEnq.Definition>("InventorySummaryEnq$ControlTimeStampDefinition", typeArray));
      }
      return slot.TimeStamp;
    }
  }

  public class ItemPlanHelper : PX.Objects.IN.GraphExtensions.ItemPlanHelper<InventorySummaryEnq>
  {
  }

  public class Definition : IPrefetchable, IPXCompanyDependent
  {
    public string TimeStamp { get; private set; }

    public void Prefetch()
    {
      this.TimeStamp = Encoding.Default.GetString(PXDatabase.Provider.SelectTimeStamp());
    }
  }
}
