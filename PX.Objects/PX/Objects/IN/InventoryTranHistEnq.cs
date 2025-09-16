// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryTranHistEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

[TableAndChartDashboardType]
public class InventoryTranHistEnq : PXGraph<InventoryTranHistEnq>
{
  private readonly string INTranPrefix = "INTran__";
  private readonly string INTranSplitPrefix = "INTranSplit__";
  public PXFilter<InventoryTranHistEnqFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXSelectJoin<InventoryTranHistEnqResult, CrossJoin<INTran, CrossJoin<INTranSplit>>, Where<True, Equal<True>>> ResultRecords;
  public PXSelectJoin<InventoryTranHistEnqResult, CrossJoin<INTran, CrossJoin<INTranSplit>>, Where<True, Equal<True>>> InternalResultRecords;
  public PXCancel<InventoryTranHistEnqFilter> Cancel;
  public PXAction<InventoryTranHistEnqFilter> viewSummary;
  public PXAction<InventoryTranHistEnqFilter> viewAllocDet;
  public PXSelect<INTran> Tran;
  protected string _SOOrderNbr;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrderType.orderType>))]
  protected virtual void INTran_SOOrderType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr>))]
  protected virtual void INTran_SOOrderNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.PO.POReceipt.receiptNbr, Where<BqlOperand<PX.Objects.PO.POReceipt.receiptType, IBqlString>.IsEqual<BqlField<InventoryTranHistEnqResult.pOReceiptType, IBqlString>.FromCurrent>>>))]
  protected virtual void INTran_POReceiptNbr_CacheAttached(PXCache sender)
  {
  }

  protected virtual void InventoryTranHistEnqFilter_StartDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    DateTime dateTime = ((PXGraph) this).Accessinfo.BusinessDate.Value;
    e.NewValue = (object) new DateTime(dateTime.Year, dateTime.Month, 1);
    ((CancelEventArgs) e).Cancel = true;
  }

  public InventoryTranHistEnq()
  {
    ((PXSelectBase) this.ResultRecords).Cache.AllowInsert = false;
    ((PXSelectBase) this.ResultRecords).Cache.AllowDelete = false;
    ((PXSelectBase) this.ResultRecords).Cache.AllowUpdate = false;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXUIField]
  public void _(PX.Data.Events.CacheAttached<INTran.finPeriodID> args)
  {
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXUIField]
  public void _(PX.Data.Events.CacheAttached<INTran.tranPeriodID> args)
  {
  }

  protected virtual IEnumerable resultRecords()
  {
    InventoryTranHistEnqFilter current = ((PXSelectBase<InventoryTranHistEnqFilter>) this.Filter).Current;
    bool valueOrDefault = current.SummaryByDay.GetValueOrDefault();
    current.IncludeUnreleased.GetValueOrDefault();
    PXCache pxCache1 = (PXCache) GraphHelper.Caches<INTran>((PXGraph) this);
    PXCache pxCache2 = (PXCache) GraphHelper.Caches<INTranSplit>((PXGraph) this);
    PXUIFieldAttribute.SetVisible<INTran.inventoryID>(pxCache1, (object) null, false);
    PXUIFieldAttribute.SetVisible<INTran.finPeriodID>(pxCache1, (object) null, false);
    PXUIFieldAttribute.SetVisible<INTran.tranPeriodID>(pxCache1, (object) null, false);
    PXUIFieldAttribute.SetVisible<INTran.tranType>(pxCache1, (object) null, !valueOrDefault);
    PXUIFieldAttribute.SetVisible<InventoryTranHistEnqResult.refNbr>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault);
    PXUIFieldAttribute.SetVisible<INTranSplit.subItemID>(pxCache2, (object) null, !valueOrDefault);
    PXUIFieldAttribute.SetVisible<INTran.siteID>(pxCache1, (object) null, !valueOrDefault);
    PXUIFieldAttribute.SetVisible<INTranSplit.locationID>(pxCache2, (object) null, !valueOrDefault);
    PXUIFieldAttribute.SetVisible<INTranSplit.lotSerialNbr>(pxCache2, (object) null, !valueOrDefault);
    PXUIFieldAttribute.SetVisible<InventoryTranHistEnqResult.unitCost>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault);
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Tran).Cache, (string) null, !valueOrDefault);
    int startRow = PXView.StartRow;
    int num = 0;
    Decimal? beginQty = new Decimal?();
    if (PXView.MaximumRows == 1 && PXView.Searches != null && PXView.Searches.Length == 1)
    {
      InventoryTranHistEnqResult tranHistEnqResult = (InventoryTranHistEnqResult) ((PXSelectBase) this.ResultRecords).Cache.Locate((object) new InventoryTranHistEnqResult()
      {
        GridLineNbr = (int?) PXView.Searches[0]
      });
      if (tranHistEnqResult != null && tranHistEnqResult.TranDate.HasValue)
      {
        PXDelegateResult pxDelegateResult = new PXDelegateResult();
        ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) new List<InventoryTranHistEnqResult>()
        {
          tranHistEnqResult
        });
        pxDelegateResult.IsResultFiltered = true;
        pxDelegateResult.IsResultSorted = true;
        pxDelegateResult.IsResultTruncated = true;
        return (IEnumerable) pxDelegateResult;
      }
    }
    ((PXSelectBase) this.ResultRecords).Cache.Clear();
    List<object> collection = ((PXSelectBase) this.InternalResultRecords).View.Select(PXView.Currents, PXView.Parameters, new object[PXView.SortColumns.Length], PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num);
    PXView.StartRow = 0;
    if (this.IsSortedByDateDescending())
    {
      for (int index = collection.Count - 1; index >= 0; --index)
        CalculateBegAndEndQty((PXResult<InventoryTranHistEnqResult>) collection[index]);
    }
    else
    {
      foreach (PXResult<InventoryTranHistEnqResult> pxResult in collection)
        CalculateBegAndEndQty(pxResult);
    }
    PXDelegateResult pxDelegateResult1 = new PXDelegateResult();
    ((List<object>) pxDelegateResult1).AddRange((IEnumerable<object>) collection);
    pxDelegateResult1.IsResultFiltered = true;
    pxDelegateResult1.IsResultSorted = true;
    pxDelegateResult1.IsResultTruncated = num > ((List<object>) pxDelegateResult1).Count;
    return (IEnumerable) pxDelegateResult1;

    void CalculateBegAndEndQty(PXResult<InventoryTranHistEnqResult> item)
    {
      InventoryTranHistEnqResult tranHistEnqResult = PXResult<InventoryTranHistEnqResult>.op_Implicit(item);
      tranHistEnqResult.BegQty = beginQty = beginQty ?? tranHistEnqResult.BegQty;
      Decimal? qtyIn = tranHistEnqResult.QtyIn;
      Decimal? qtyOut = tranHistEnqResult.QtyOut;
      ref InventoryTranHistEnq.\u003C\u003Ec__DisplayClass21_0 local = ref obj1;
      Decimal? beginQty = beginQty;
      Decimal num = qtyIn.GetValueOrDefault() - qtyOut.GetValueOrDefault();
      Decimal? nullable = beginQty.HasValue ? new Decimal?(beginQty.GetValueOrDefault() + num) : new Decimal?();
      // ISSUE: reference to a compiler-generated field
      local.beginQty = nullable;
      tranHistEnqResult.EndQty = beginQty;
      ((PXSelectBase) this.ResultRecords).Cache.SetStatus((object) tranHistEnqResult, (PXEntryStatus) 5);
    }
  }

  protected virtual void AlterSortsAndFilters(
    out string[] newSorts,
    out bool[] newDescs,
    out bool sortsChanged,
    out PXFilterRow[] newFilters,
    out bool filtersChanged)
  {
    bool byTranDate = false;
    bool byReleasedDate = false;
    bool summaryByDay = ((bool?) ((PXSelectBase<InventoryTranHistEnqFilter>) this.Filter).Current?.SummaryByDay).GetValueOrDefault();
    List<string> newSortColumns = new List<string>();
    List<bool> newDescendings = new List<bool>();
    List<PXFilterRow> filters = new List<PXFilterRow>();
    sortsChanged = false;
    filtersChanged = false;
    foreach (PXFilterRow filter in PXView.Filters)
      filtersChanged |= ProcessField(filter);
    for (int index = 0; index < PXView.SortColumns.Length; ++index)
      sortsChanged |= ProcessField((PXFilterRow) null, PXView.SortColumns[index], PXView.Descendings[index]);
    newSorts = newSortColumns.ToArray();
    newDescs = newDescendings.ToArray();
    newFilters = filters.ToArray();

    void AddFilter(PXFilterRow oldFilter, string newField)
    {
      filters.Add(new PXFilterRow(oldFilter)
      {
        DataField = newField
      });
    }

    void AddSort(string field, bool desc)
    {
      newSortColumns.Add(field);
      newDescendings.Add(desc);
    }

    void AddSortByReleasedDate(bool desc)
    {
      if (byReleasedDate)
        return;
      AddSort("Released", !desc);
      AddSort("ReleasedDateTime", desc);
      AddSort("DocType", desc);
      AddSort("RefNbr", desc);
      AddSort(this.INTranSplitPrefix + "LotSerialNbr", desc);
      AddSort(this.INTranPrefix + "LotSerialNbr", desc);
      AddSort("LineNbr", desc);
      byReleasedDate = true;
    }

    void AddSortByTranDate(bool desc)
    {
      if (byTranDate)
        return;
      AddSort("TranDate", desc);
      AddSort("Released", !desc);
      AddSort("ReleasedDateTime", desc);
      AddSort("DocType", desc);
      AddSort("RefNbr", desc);
      AddSort(this.INTranSplitPrefix + "LotSerialNbr", desc);
      AddSort(this.INTranPrefix + "LotSerialNbr", desc);
      AddSort("LineNbr", desc);
      byTranDate = true;
    }

    bool ProcessField(PXFilterRow filter, string sortField = null, bool desc = false)
    {
      bool flag = false;
      string str = (filter != null || sortField != null) && (filter == null || sortField == null) ? sortField : throw new PXArgumentException();
      if (filter != null)
        str = filter.DataField;
      if (str.StartsWith(this.INTranPrefix))
        str = str.Substring(this.INTranPrefix.Length);
      string strA1 = str;
      if (string.Compare(strA1, "GridLineNbr", true) == 0)
      {
        if (sortField != null)
        {
          if (!summaryByDay)
          {
            AddSortByReleasedDate(desc);
            flag |= desc;
          }
          else
          {
            AddSortByTranDate(desc);
            flag |= desc;
          }
        }
        if (filter != null)
          flag = true;
      }
      else if (string.Compare(strA1, "ReleasedDateTime", true) == 0)
      {
        if (sortField != null)
        {
          AddSortByReleasedDate(desc);
          flag |= desc;
        }
        if (filter != null)
          AddFilter(filter, str);
      }
      else if (string.Compare(strA1, "TranDate", true) == 0)
      {
        if (sortField != null)
        {
          AddSortByTranDate(desc);
          flag |= desc;
        }
        if (filter != null)
          AddFilter(filter, str);
      }
      else
      {
        string strA2 = strA1;
        if (string.Compare(strA2, this.INTranSplitPrefix + "LotSerialNbr", true) == 0 || string.Compare(strA2, this.INTranSplitPrefix + "SubItemID", true) == 0 || string.Compare(strA2, this.INTranSplitPrefix + "LocationID", true) == 0)
        {
          if (sortField != null)
            AddSort(str, desc);
          if (filter != null)
            AddFilter(filter, str);
        }
        else
        {
          string strA3 = strA1;
          if (string.Compare(strA3, "DocType", true) == 0 || string.Compare(strA3, "SiteID", true) == 0 || string.Compare(strA3, "Released", true) == 0 || string.Compare(strA3, "ReleasedDateTime", true) == 0)
          {
            if (sortField != null)
              AddSort(str, desc);
            if (filter != null)
              AddFilter(filter, str);
          }
          else
          {
            string strA4 = strA1;
            if (string.Compare(strA4, "FinPeriodID", true) == 0 || string.Compare(strA4, "TranPeriodID", true) == 0 || string.Compare(strA4, "SOOrderType", true) == 0 || string.Compare(strA4, "SOOrderNbr", true) == 0)
            {
              if (sortField != null)
                AddSort(this.INTranPrefix + str, desc);
              if (filter != null)
                AddFilter(filter, this.INTranPrefix + str);
            }
            else
            {
              string strA5 = strA1;
              if (string.Compare(strA5, "QtyIn", true) == 0 || string.Compare(strA5, "QtyOut", true) == 0)
              {
                if (sortField != null)
                  flag = true;
                if (filter != null)
                {
                  if (!summaryByDay)
                    AddFilter(filter, this.INTranSplitPrefix + str);
                  else
                    flag = true;
                }
              }
              else
                flag = true;
            }
          }
        }
      }
      return flag;
    }
  }

  protected virtual IEnumerable internalResultRecords()
  {
    InventoryTranHistEnqFilter current = ((PXSelectBase<InventoryTranHistEnqFilter>) this.Filter).Current;
    bool valueOrDefault1 = current.SummaryByDay.GetValueOrDefault();
    bool valueOrDefault2 = current.IncludeUnreleased.GetValueOrDefault();
    bool flag = !string.IsNullOrEmpty(current.LotSerialNbr) && PXAccess.FeatureInstalled<FeaturesSet.lotSerialTracking>();
    List<PXResult<InventoryTranHistEnqResult, INTran, INTranSplit>> pxResultList = new List<PXResult<InventoryTranHistEnqResult, INTran, INTranSplit>>();
    int? nullable1 = current.InventoryID;
    if (!nullable1.HasValue)
      return (IEnumerable) new PXDelegateResult()
      {
        IsResultFiltered = true,
        IsResultSorted = true,
        IsResultTruncated = true
      };
    PXSelectBase<INTranSplit> pxSelectBase1 = (PXSelectBase<INTranSplit>) new PXSelectReadonly2<INTranSplit, InnerJoin<INTran, On<INTranSplit.FK.Tran>, InnerJoin<INSubItem, On<INTranSplit.FK.SubItem>, InnerJoin<INSite, On<INTran.FK.Site>>>>, Where<INTranSplit.inventoryID, Equal<Current<InventoryTranHistEnqFilter.inventoryID>>, And<Match<INSite, Current<AccessInfo.userName>>>>, OrderBy<Asc<INTranSplit.docType, Asc<INTranSplit.refNbr, Asc<INTranSplit.lineNbr, Asc<INTranSplit.splitLineNbr>>>>>>((PXGraph) this);
    PXSelectBase<INTranSplit> pxSelectBase2 = (PXSelectBase<INTranSplit>) new PXSelectJoinGroupBy<INTranSplit, InnerJoin<INTran, On<INTranSplit.FK.Tran>, InnerJoin<INSubItem, On<INTranSplit.FK.SubItem>, InnerJoin<INSite, On<INTran.FK.Site>>>>, Where<INTranSplit.inventoryID, Equal<Current<InventoryTranHistEnqFilter.inventoryID>>, And<INTranSplit.tranDate, Less<Required<INTranSplit.tranDate>>, And<Match<INSite, Current<AccessInfo.userName>>>>>, Aggregate<GroupBy<INTranSplit.invtMult, Sum<INTranSplit.baseQty>>>>((PXGraph) this);
    PXSelectBase<INItemSiteHistByLatestSDate> pxSelectBase3 = (PXSelectBase<INItemSiteHistByLatestSDate>) new PXSelectReadonly2<INItemSiteHistByLatestSDate, InnerJoin<INItemSiteHistDay, On<INItemSiteHistDay.inventoryID, Equal<INItemSiteHistByLatestSDate.inventoryID>, And<INItemSiteHistDay.siteID, Equal<INItemSiteHistByLatestSDate.siteID>, And<INItemSiteHistDay.subItemID, Equal<INItemSiteHistByLatestSDate.subItemID>, And<INItemSiteHistDay.locationID, Equal<INItemSiteHistByLatestSDate.locationID>, And<INItemSiteHistDay.sDate, Equal<INItemSiteHistByLatestSDate.lastActivityDate>>>>>>, InnerJoin<INSubItem, On<INSubItem.subItemID, Equal<INItemSiteHistByLatestSDate.subItemID>>, InnerJoin<INSite, On<INSite.siteID, Equal<INItemSiteHistByLatestSDate.siteID>>>>>, Where<INItemSiteHistByLatestSDate.inventoryID, Equal<Current<InventoryTranHistEnqFilter.inventoryID>>, And<Match<INSite, Current<AccessInfo.userName>>>>>((PXGraph) this);
    if (!SubCDUtils.IsSubCDEmpty(current.SubItemCD) && PXAccess.FeatureInstalled<FeaturesSet.subItem>())
    {
      pxSelectBase1.WhereAnd<Where<INSubItem.subItemCD, Like<Current<InventoryTranHistEnqFilter.subItemCDWildcard>>>>();
      pxSelectBase3.WhereAnd<Where<INSubItem.subItemCD, Like<Current<InventoryTranHistEnqFilter.subItemCDWildcard>>>>();
      pxSelectBase2.WhereAnd<Where<INSubItem.subItemCD, Like<Current<InventoryTranHistEnqFilter.subItemCDWildcard>>>>();
    }
    nullable1 = current.SiteID;
    if (nullable1.HasValue && PXAccess.FeatureInstalled<FeaturesSet.warehouse>())
    {
      pxSelectBase1.WhereAnd<Where<INTranSplit.siteID, Equal<Current<InventoryTranHistEnqFilter.siteID>>>>();
      pxSelectBase3.WhereAnd<Where<INItemSiteHistByLatestSDate.siteID, Equal<Current<InventoryTranHistEnqFilter.siteID>>>>();
      pxSelectBase2.WhereAnd<Where<INTranSplit.siteID, Equal<Current<InventoryTranHistEnqFilter.siteID>>>>();
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
    {
      pxSelectBase1.WhereAnd<Where<INSite.branchID, InsideBranchesOf<Current<InventoryTranHistEnqFilter.orgBAccountID>>>>();
      pxSelectBase3.WhereAnd<Where<INSite.branchID, InsideBranchesOf<Current<InventoryTranHistEnqFilter.orgBAccountID>>>>();
      pxSelectBase2.WhereAnd<Where<INSite.branchID, InsideBranchesOf<Current<InventoryTranHistEnqFilter.orgBAccountID>>>>();
    }
    nullable1 = current.LocationID;
    if ((nullable1 ?? -1) != -1 && PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>())
    {
      pxSelectBase1.WhereAnd<Where<INTranSplit.locationID, Equal<Current<InventoryTranHistEnqFilter.locationID>>>>();
      pxSelectBase3.WhereAnd<Where<INItemSiteHistByLatestSDate.locationID, Equal<Current<InventoryTranHistEnqFilter.locationID>>>>();
      pxSelectBase2.WhereAnd<Where<INTranSplit.locationID, Equal<Current<InventoryTranHistEnqFilter.locationID>>>>();
    }
    if (flag)
    {
      pxSelectBase1.WhereAnd<Where<INTranSplit.lotSerialNbr, Like<Current<InventoryTranHistEnqFilter.lotSerialNbrWildcard>>>>();
      pxSelectBase2.WhereAnd<Where<INTranSplit.lotSerialNbr, Like<Current<InventoryTranHistEnqFilter.lotSerialNbrWildcard>>>>();
    }
    if (!valueOrDefault2)
    {
      pxSelectBase1.WhereAnd<Where<INTranSplit.released, Equal<True>>>();
      pxSelectBase2.WhereAnd<Where<INTranSplit.released, Equal<True>>>();
    }
    string[] newSorts;
    bool[] newDescs;
    bool sortsChanged;
    PXFilterRow[] newFilters;
    bool filtersChanged;
    this.AlterSortsAndFilters(out newSorts, out newDescs, out sortsChanged, out newFilters, out filtersChanged);
    Decimal cumulativeQty = 0M;
    DateTime? nullable2 = current.StartDate;
    if (nullable2.HasValue)
    {
      int num1 = 0;
      int num2 = 0;
      if (flag)
      {
        PXView view = ((PXSelectBase) pxSelectBase2).View;
        object[] objArray1 = new object[0];
        object[] objArray2 = new object[1]
        {
          (object) current.StartDate
        };
        object[] objArray3 = new object[0];
        string[] strArray = new string[0];
        bool[] flagArray = new bool[0];
        PXFilterRow[] pxFilterRowArray = newFilters;
        ref int local1 = ref num1;
        ref int local2 = ref num2;
        foreach (PXResult<INTranSplit> pxResult in view.Select(objArray1, objArray2, objArray3, strArray, flagArray, pxFilterRowArray, ref local1, 0, ref local2))
        {
          INTranSplit inTranSplit = PXResult<INTranSplit>.op_Implicit(pxResult);
          Decimal num3 = cumulativeQty;
          short? invtMult = inTranSplit.InvtMult;
          Decimal? nullable3 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
          Decimal? baseQty = inTranSplit.BaseQty;
          Decimal valueOrDefault3 = (nullable3.HasValue & baseQty.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * baseQty.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
          cumulativeQty = num3 + valueOrDefault3;
        }
      }
      else
      {
        int num4 = 0;
        int num5 = 0;
        PXFilterRow[] beginningBalanceFilters = this.GetBeginningBalanceFilters();
        List<Type> typeList = new List<Type>((IEnumerable<Type>) new Type[3]
        {
          typeof (INItemSiteHistDay.begQty),
          typeof (INItemSiteHistDay.endQty),
          typeof (INItemSiteHistByLatestSDate.lastActivityDate)
        });
        using (new PXFieldScope(((PXSelectBase) pxSelectBase3).View, typeList.ToArray()))
        {
          foreach (PXResult<INItemSiteHistByLatestSDate, INItemSiteHistDay> pxResult in ((PXSelectBase) pxSelectBase3).View.Select(new object[0], new object[0], new object[0], new string[0], new bool[0], beginningBalanceFilters, ref num4, 0, ref num5))
          {
            INItemSiteHistByLatestSDate histByLatestSdate = PXResult<INItemSiteHistByLatestSDate, INItemSiteHistDay>.op_Implicit(pxResult);
            INItemSiteHistDay inItemSiteHistDay = PXResult<INItemSiteHistByLatestSDate, INItemSiteHistDay>.op_Implicit(pxResult);
            Decimal num6 = cumulativeQty;
            nullable2 = histByLatestSdate.LastActivityDate;
            Decimal? nullable4;
            if (nullable2.HasValue)
            {
              nullable2 = current.StartDate;
              DateTime dateTime = nullable2.Value;
              DateTime date1 = dateTime.Date;
              nullable2 = histByLatestSdate.LastActivityDate;
              dateTime = nullable2.Value;
              DateTime date2 = dateTime.Date;
              if (date1 == date2)
              {
                nullable4 = inItemSiteHistDay.BegQty;
                goto label_28;
              }
            }
            nullable4 = inItemSiteHistDay.EndQty;
label_28:
            Decimal valueOrDefault4 = nullable4.GetValueOrDefault();
            cumulativeQty = num6 + valueOrDefault4;
          }
        }
      }
      if (valueOrDefault2)
      {
        int valueOrDefault5 = PXAccess.GetParentOrganizationID((int?) INSite.PK.Find((PXGraph) this, current.SiteID)?.BranchID).GetValueOrDefault();
        string finPeriodID;
        DateTime? nullable5;
        try
        {
          finPeriodID = this.FinPeriodRepository.GetPeriodIDFromDate(current.StartDate, new int?(valueOrDefault5));
          nullable5 = new DateTime?(this.FinPeriodRepository.PeriodStartDate(finPeriodID, new int?(valueOrDefault5)));
        }
        catch (PXFinPeriodException ex)
        {
          finPeriodID = (string) null;
          nullable5 = current.StartDate;
        }
        PXSelectBase<OrganizationFinPeriod> pxSelectBase4 = (PXSelectBase<OrganizationFinPeriod>) new PXSelectGroupBy<OrganizationFinPeriod, Where<OrganizationFinPeriod.finPeriodID, LessEqual<Required<OrganizationFinPeriod.finPeriodID>>, And<OrganizationFinPeriod.iNClosed, Equal<False>, Or<OrganizationFinPeriod.finPeriodID, Equal<Required<OrganizationFinPeriod.finPeriodID>>>>>, Aggregate<GroupBy<OrganizationFinPeriod.finPeriodID>>, OrderBy<Asc<OrganizationFinPeriod.finPeriodID>>>((PXGraph) this);
        List<object> objectList = new List<object>()
        {
          (object) finPeriodID,
          (object) finPeriodID
        };
        if (valueOrDefault5 != 0)
        {
          pxSelectBase4.WhereAnd<Where<OrganizationFinPeriod.organizationID, Equal<Required<OrganizationFinPeriod.organizationID>>>>();
          objectList.Add((object) valueOrDefault5);
        }
        OrganizationFinPeriod organizationFinPeriod = PXResultset<OrganizationFinPeriod>.op_Implicit(pxSelectBase4.SelectWindowed(0, 1, objectList.ToArray()));
        if (organizationFinPeriod != null)
        {
          string finPeriodId = organizationFinPeriod.FinPeriodID;
          nullable5 = new DateTime?(this.FinPeriodRepository.PeriodStartDate(organizationFinPeriod.FinPeriodID, new int?(valueOrDefault5)));
          PXView pxView = new PXView((PXGraph) this, true, ((PXSelectBase) pxSelectBase1).View.BqlSelect.WhereAnd<Where<INTranSplit.tranDate, GreaterEqual<Required<INTranSplit.tranDate>>>>().WhereAnd<Where<INTranSplit.tranDate, Less<Required<INTranSplit.tranDate>>>>().WhereAnd<Where<INTranSplit.released, Equal<False>>>().AggregateNew<Aggregate<GroupBy<INTranSplit.inventoryID, GroupBy<INTranSplit.invtMult, Sum<INTranSplit.baseQty>>>>>());
          int num7 = 0;
          int num8 = 0;
          object[] objArray4 = new object[0];
          object[] objArray5 = new object[2]
          {
            (object) nullable5,
            (object) current.StartDate.Value
          };
          object[] objArray6 = new object[0];
          string[] strArray = new string[0];
          bool[] flagArray = new bool[0];
          PXFilterRow[] pxFilterRowArray = newFilters;
          ref int local3 = ref num7;
          ref int local4 = ref num8;
          foreach (PXResult<INTranSplit> pxResult in pxView.Select(objArray4, objArray5, objArray6, strArray, flagArray, pxFilterRowArray, ref local3, 0, ref local4))
          {
            INTranSplit inTranSplit = PXResult<INTranSplit>.op_Implicit(pxResult);
            Decimal num9 = cumulativeQty;
            short? invtMult = inTranSplit.InvtMult;
            Decimal? nullable6 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
            Decimal? baseQty = inTranSplit.BaseQty;
            Decimal valueOrDefault6 = (nullable6.HasValue & baseQty.HasValue ? new Decimal?(nullable6.GetValueOrDefault() * baseQty.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
            cumulativeQty = num9 + valueOrDefault6;
          }
        }
      }
    }
    nullable2 = current.StartDate;
    if (nullable2.HasValue)
      pxSelectBase1.WhereAnd<Where<INTranSplit.tranDate, GreaterEqual<Current<InventoryTranHistEnqFilter.startDate>>>>();
    nullable2 = current.EndDate;
    if (nullable2.HasValue)
      pxSelectBase1.WhereAnd<Where<INTranSplit.tranDate, LessEqual<Current<InventoryTranHistEnqFilter.endDate>>>>();
    int num10 = 0;
    int num11 = (sortsChanged || filtersChanged ? 0 : (!PXView.ReverseOrder ? 1 : 0)) != 0 ? PXView.StartRow + PXView.MaximumRows : 0;
    int num12 = 0;
    List<object> objectList1 = (!valueOrDefault1 ? ((PXSelectBase) pxSelectBase1).View : new PXView((PXGraph) this, true, ((PXSelectBase) pxSelectBase1).View.BqlSelect.AggregateNew<Aggregate<GroupBy<INTranSplit.tranDate, Sum<INTranSplit.qtyIn, Sum<INTranSplit.qtyOut>>>>>())).Select(PXView.Currents, new object[1]
    {
      (object) current.StartDate
    }, (object[]) new string[newSorts.Length], newSorts, newDescs, newFilters, ref num10, num11, ref num12);
    int num13 = 0;
    foreach (PXResult<INTranSplit, INTran, INSubItem> pxResult in objectList1)
    {
      INTranSplit inTranSplit = PXResult<INTranSplit, INTran, INSubItem>.op_Implicit(pxResult);
      INTran inTran = PXResult<INTranSplit, INTran, INSubItem>.op_Implicit(pxResult);
      if (valueOrDefault1)
      {
        pxResultList.Add(new PXResult<InventoryTranHistEnqResult, INTran, INTranSplit>(new InventoryTranHistEnqResult()
        {
          TranDate = inTranSplit.TranDate,
          QtyIn = inTranSplit.QtyIn,
          QtyOut = inTranSplit.QtyOut,
          GridLineNbr = new int?(++num13)
        }, (INTran) null, (INTranSplit) null));
      }
      else
      {
        InventoryTranHistEnqResult tranHistEnqResult = new InventoryTranHistEnqResult();
        tranHistEnqResult.TranDate = inTranSplit.TranDate;
        tranHistEnqResult.QtyIn = inTranSplit.QtyIn;
        tranHistEnqResult.QtyOut = inTranSplit.QtyOut;
        tranHistEnqResult.DocType = inTranSplit.DocType;
        tranHistEnqResult.POReceiptType = inTran.POReceiptType;
        tranHistEnqResult.RefNbr = inTranSplit.RefNbr;
        tranHistEnqResult.LineNbr = inTranSplit.LineNbr;
        tranHistEnqResult.SplitLineNbr = inTranSplit.SplitLineNbr;
        tranHistEnqResult.GridLineNbr = new int?(++num13);
        Decimal? nullable7;
        Decimal? nullable8;
        Decimal? nullable9;
        if (current.ShowAdjUnitCost.GetValueOrDefault())
        {
          nullable7 = inTranSplit.TotalQty;
          Decimal? nullable10;
          if (nullable7.HasValue)
          {
            nullable7 = inTranSplit.TotalQty;
            Decimal num14 = 0M;
            if (!(nullable7.GetValueOrDefault() == num14 & nullable7.HasValue))
            {
              Decimal? totalCost = inTranSplit.TotalCost;
              Decimal? additionalCost = inTranSplit.AdditionalCost;
              nullable7 = totalCost.HasValue & additionalCost.HasValue ? new Decimal?(totalCost.GetValueOrDefault() + additionalCost.GetValueOrDefault()) : new Decimal?();
              nullable8 = inTranSplit.TotalQty;
              nullable10 = nullable7.HasValue & nullable8.HasValue ? new Decimal?(nullable7.GetValueOrDefault() / nullable8.GetValueOrDefault()) : new Decimal?();
              goto label_59;
            }
          }
          nullable10 = new Decimal?(0M);
label_59:
          nullable9 = nullable10;
        }
        else
        {
          nullable8 = inTranSplit.TotalQty;
          Decimal? nullable11;
          if (nullable8.HasValue)
          {
            nullable8 = inTranSplit.TotalQty;
            Decimal num15 = 0M;
            if (!(nullable8.GetValueOrDefault() == num15 & nullable8.HasValue))
            {
              nullable8 = inTranSplit.TotalCost;
              nullable7 = inTranSplit.TotalQty;
              nullable11 = nullable8.HasValue & nullable7.HasValue ? new Decimal?(nullable8.GetValueOrDefault() / nullable7.GetValueOrDefault()) : new Decimal?();
              goto label_64;
            }
          }
          nullable11 = new Decimal?(0M);
label_64:
          nullable9 = nullable11;
        }
        tranHistEnqResult.UnitCost = nullable9;
        pxResultList.Add(new PXResult<InventoryTranHistEnqResult, INTran, INTranSplit>(tranHistEnqResult, inTran, inTranSplit));
      }
    }
    this.RecalculateTotalColumns(valueOrDefault1, cumulativeQty, pxResultList);
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    pxDelegateResult.IsResultFiltered = !filtersChanged;
    pxDelegateResult.IsResultSorted = !sortsChanged;
    pxDelegateResult.IsResultTruncated = num12 > pxResultList.Count;
    if (!PXView.ReverseOrder)
    {
      ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) pxResultList);
    }
    else
    {
      IEnumerable source = PXView.Sort((IEnumerable) pxResultList);
      ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) source.Cast<PXResult<InventoryTranHistEnqResult, INTran, INTranSplit>>());
      pxDelegateResult.IsResultSorted = true;
    }
    return (IEnumerable) pxDelegateResult;
  }

  private void RecalculateTotalColumns(
    bool summaryByDay,
    Decimal cumulativeQty,
    List<PXResult<InventoryTranHistEnqResult, INTran, INTranSplit>> resultList)
  {
    if (this.IsSortedByDateDescending())
    {
      for (int index = resultList.Count - 1; index >= 0; --index)
        CalculateTotal(resultList[index]);
    }
    else
    {
      foreach (PXResult<InventoryTranHistEnqResult, INTran, INTranSplit> result in resultList)
        CalculateTotal(result);
    }

    void CalculateTotal(
      PXResult<InventoryTranHistEnqResult, INTran, INTranSplit> item)
    {
      InventoryTranHistEnqResult tranHistEnqResult1 = PXResult<InventoryTranHistEnqResult, INTran, INTranSplit>.op_Implicit(item);
      tranHistEnqResult1.BegQty = new Decimal?(cumulativeQty);
      Decimal? nullable1;
      if (summaryByDay)
      {
        ref InventoryTranHistEnq.\u003C\u003Ec__DisplayClass24_0 local = ref obj1;
        Decimal cumulativeQty = cumulativeQty;
        Decimal? qtyIn = tranHistEnqResult1.QtyIn;
        nullable1 = tranHistEnqResult1.QtyOut;
        Decimal valueOrDefault = (qtyIn.HasValue & nullable1.HasValue ? new Decimal?(qtyIn.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
        Decimal num = cumulativeQty + valueOrDefault;
        // ISSUE: reference to a compiler-generated field
        local.cumulativeQty = num;
      }
      else
      {
        INTranSplit inTranSplit = PXResult<InventoryTranHistEnqResult, INTran, INTranSplit>.op_Implicit(item);
        ref InventoryTranHistEnq.\u003C\u003Ec__DisplayClass24_0 local = ref obj1;
        Decimal cumulativeQty = cumulativeQty;
        short? invtMult = inTranSplit.InvtMult;
        Decimal? nullable2 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
        nullable1 = inTranSplit.BaseQty;
        Decimal valueOrDefault = (nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable1.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
        Decimal num = cumulativeQty + valueOrDefault;
        // ISSUE: reference to a compiler-generated field
        local.cumulativeQty = num;
      }
      InventoryTranHistEnqResult tranHistEnqResult2 = tranHistEnqResult1;
      Decimal? begQty = tranHistEnqResult1.BegQty;
      Decimal? nullable3 = tranHistEnqResult1.QtyIn;
      Decimal? nullable4 = begQty.HasValue & nullable3.HasValue ? new Decimal?(begQty.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
      nullable1 = tranHistEnqResult1.QtyOut;
      Decimal? nullable5;
      if (!(nullable4.HasValue & nullable1.HasValue))
      {
        nullable3 = new Decimal?();
        nullable5 = nullable3;
      }
      else
        nullable5 = new Decimal?(nullable4.GetValueOrDefault() - nullable1.GetValueOrDefault());
      tranHistEnqResult2.EndQty = nullable5;
    }
  }

  private bool IsSortedByDateDescending()
  {
    int index = Array.FindIndex<string>(PXView.SortColumns, (Predicate<string>) (f => f.Equals(this.INTranPrefix + "ReleasedDateTime", StringComparison.OrdinalIgnoreCase)));
    if (index == -1)
      index = Array.FindIndex<string>(PXView.SortColumns, (Predicate<string>) (f => f.Equals("TranDate", StringComparison.OrdinalIgnoreCase)));
    return index != -1 && PXView.Descendings.Length > index && PXView.Descendings[index];
  }

  private PXFilterRow[] GetBeginningBalanceFilters()
  {
    if (!NonGenericIEnumerableExtensions.Any_((IEnumerable) PXView.Filters))
      return new PXFilterRow[0];
    List<PXFilterRow> pxFilterRowList = new List<PXFilterRow>();
    string[] source = new string[5]
    {
      "SiteID",
      "LocationID",
      "SubItemID",
      "QtyIn",
      "QtyOut"
    };
    foreach (PXFilterRow filter in PXView.Filters)
    {
      string str = filter.DataField;
      if (str.StartsWith(this.INTranPrefix))
        str = str.Substring(this.INTranPrefix.Length);
      else if (str.StartsWith(this.INTranSplitPrefix))
        str = str.Substring(this.INTranSplitPrefix.Length);
      if (((IEnumerable<string>) source).Contains<string>(str))
        pxFilterRowList.Add(new PXFilterRow(filter)
        {
          DataField = "INItemSiteHistDay__" + str
        });
      else if (str == "TranDate")
        pxFilterRowList.Add(new PXFilterRow(filter)
        {
          DataField = "INItemSiteHistDay__SDate"
        });
    }
    return pxFilterRowList.ToArray();
  }

  public virtual bool IsDirty => false;

  [PXButton]
  [PXUIField(DisplayName = "Inventory Summary")]
  protected virtual IEnumerable ViewSummary(PXAdapter a)
  {
    if (((PXSelectBase<InventoryTranHistEnqResult>) this.ResultRecords).Current != null)
    {
      InventoryTranHistEnqResult current = ((PXSelectBase<InventoryTranHistEnqResult>) this.ResultRecords).Current;
      INTran inTran = INTran.PK.Find((PXGraph) this, current.DocType, current.RefNbr, current.LineNbr);
      INTranSplit inTranSplit = INTranSplit.PK.Find((PXGraph) this, current.DocType, current.RefNbr, current.LineNbr, current.SplitLineNbr);
      if (inTran != null)
      {
        PXSegmentedState valueExt = ((PXSelectBase) this.ResultRecords).Cache.GetValueExt<INTranSplit.subItemID>((object) inTranSplit) as PXSegmentedState;
        InventorySummaryEnq.Redirect(inTran.InventoryID, valueExt != null ? (string) ((PXFieldState) valueExt).Value : (string) null, inTran.SiteID, inTranSplit.LocationID, false);
      }
    }
    return a.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Allocation Details")]
  protected virtual IEnumerable ViewAllocDet(PXAdapter a)
  {
    if (((PXSelectBase<InventoryTranHistEnqResult>) this.ResultRecords).Current != null)
    {
      InventoryTranHistEnqResult current = ((PXSelectBase<InventoryTranHistEnqResult>) this.ResultRecords).Current;
      INTran inTran = INTran.PK.Find((PXGraph) this, current.DocType, current.RefNbr, current.LineNbr);
      INTranSplit inTranSplit = INTranSplit.PK.Find((PXGraph) this, current.DocType, current.RefNbr, current.LineNbr, current.SplitLineNbr);
      if (inTran != null)
      {
        PXSegmentedState valueExt = ((PXSelectBase) this.ResultRecords).Cache.GetValueExt<INTranSplit.subItemID>((object) inTranSplit) as PXSegmentedState;
        InventoryAllocDetEnq.Redirect(inTran.InventoryID, valueExt != null ? (string) ((PXFieldState) valueExt).Value : (string) null, (string) null, inTran.SiteID, inTranSplit.LocationID);
      }
    }
    return a.Get();
  }

  public static void Redirect(
    string finPeriodID,
    int? inventoryID,
    string subItemCD,
    string lotSerNum,
    int? siteID,
    int? locationID)
  {
    InventoryTranHistEnq instance = PXGraph.CreateInstance<InventoryTranHistEnq>();
    ((PXSelectBase<InventoryTranHistEnqFilter>) instance.Filter).Current.InventoryID = inventoryID;
    ((PXSelectBase<InventoryTranHistEnqFilter>) instance.Filter).Current.SubItemCD = subItemCD;
    ((PXSelectBase<InventoryTranHistEnqFilter>) instance.Filter).Current.SiteID = siteID;
    ((PXSelectBase<InventoryTranHistEnqFilter>) instance.Filter).Current.LocationID = locationID;
    ((PXSelectBase<InventoryTranHistEnqFilter>) instance.Filter).Current.LotSerialNbr = lotSerNum;
    throw new PXRedirectRequiredException((PXGraph) instance, "Transaction History");
  }
}
