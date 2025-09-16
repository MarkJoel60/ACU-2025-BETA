// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryTranDetEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

[TableAndChartDashboardType]
public class InventoryTranDetEnq : PXGraph<InventoryTranDetEnq>
{
  public PXFilter<InventoryTranDetEnqFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXSelectJoin<InventoryTranDetEnqResult, CrossJoin<INTran>, Where<True, Equal<True>>> ResultRecords;
  public PXSelectJoin<InventoryTranDetEnqResult, CrossJoin<INTran>, Where<True, Equal<True>>> InternalResultRecords;
  public PXCancel<InventoryTranDetEnqFilter> Cancel;
  public PXAction<InventoryTranDetEnqFilter> PreviousPeriod;
  public PXAction<InventoryTranDetEnqFilter> NextPeriod;
  public PXSelect<INTran> Tran;
  protected string _SOOrderNbr;
  public PXAction<InventoryTranDetEnqFilter> viewSummary;
  public PXAction<InventoryTranDetEnqFilter> viewAllocDet;

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
  [PXSelector(typeof (Search<PX.Objects.PO.POReceipt.receiptNbr, Where<BqlOperand<PX.Objects.PO.POReceipt.receiptType, IBqlString>.IsEqual<BqlField<InventoryTranDetEnqResult.pOReceiptType, IBqlString>.FromCurrent>>>))]
  protected virtual void INTran_POReceiptNbr_CacheAttached(PXCache sender)
  {
  }

  public InventoryTranDetEnq()
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
    InventoryTranDetEnqFilter current = ((PXSelectBase<InventoryTranDetEnqFilter>) this.Filter).Current;
    bool valueOrDefault1 = current.SummaryByDay.GetValueOrDefault();
    bool valueOrDefault2 = current.IncludeUnreleased.GetValueOrDefault();
    bool valueOrDefault3 = current.ByFinancialPeriod.GetValueOrDefault();
    PXCache<INTran> pxCache = GraphHelper.Caches<INTran>((PXGraph) this);
    PXUIFieldAttribute.SetVisible<INTran.inventoryID>((PXCache) pxCache, (object) null, false);
    PXUIFieldAttribute.SetVisible<InventoryTranDetEnqResult.begQty>(((PXSelectBase) this.ResultRecords).Cache, (object) null, valueOrDefault3);
    PXUIFieldAttribute.SetVisible<InventoryTranDetEnqResult.endQty>(((PXSelectBase) this.ResultRecords).Cache, (object) null, valueOrDefault3);
    PXUIFieldAttribute.SetVisible<InventoryTranDetEnqResult.begBalance>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault2 & valueOrDefault3);
    PXUIFieldAttribute.SetVisible<InventoryTranDetEnqResult.endBalance>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault2 & valueOrDefault3);
    PXUIFieldAttribute.SetVisible<InventoryTranDetEnqResult.extCostIn>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault2);
    PXUIFieldAttribute.SetVisible<InventoryTranDetEnqResult.extCostOut>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault2);
    PXUIFieldAttribute.SetVisible<InventoryTranDetEnqResult.unitCost>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault2 & !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<INTran.finPeriodID>((PXCache) pxCache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<INTran.tranPeriodID>((PXCache) pxCache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<InventoryTranDetEnqResult.tranType>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<InventoryTranDetEnqResult.refNbr>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<InventoryTranDetEnqResult.subItemID>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<INTran.siteID>((PXCache) pxCache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<InventoryTranDetEnqResult.locationID>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<InventoryTranDetEnqResult.lotSerialNbr>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<InventoryTranDetEnqResult.released>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<INTran.releasedDateTime>((PXCache) pxCache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Tran).Cache, (string) null, !valueOrDefault1);
    if (PXView.MaximumRows == 1 && PXView.Searches != null && PXView.Searches.Length == 1)
    {
      InventoryTranDetEnqResult tranDetEnqResult = (InventoryTranDetEnqResult) ((PXSelectBase) this.ResultRecords).Cache.Locate((object) new InventoryTranDetEnqResult()
      {
        GridLineNbr = (int?) PXView.Searches[0]
      });
      if (tranDetEnqResult != null && tranDetEnqResult.TranDate.HasValue)
      {
        PXDelegateResult pxDelegateResult = new PXDelegateResult();
        ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) new List<InventoryTranDetEnqResult>()
        {
          tranDetEnqResult
        });
        pxDelegateResult.IsResultFiltered = true;
        pxDelegateResult.IsResultSorted = true;
        pxDelegateResult.IsResultTruncated = true;
        return (IEnumerable) pxDelegateResult;
      }
    }
    if (!current.InventoryID.HasValue)
      return (IEnumerable) new List<PXResult<InventoryTranDetEnqResult, INTran>>();
    if (current.FinPeriodID == null)
      return (IEnumerable) new List<PXResult<InventoryTranDetEnqResult, INTran>>();
    int startRow = PXView.StartRow;
    int num = 0;
    List<object> collection = ((PXSelectBase) this.InternalResultRecords).View.Select(PXView.Currents, PXView.Parameters, new object[PXView.SortColumns.Length], PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num);
    PXView.StartRow = 0;
    ((PXSelectBase) this.ResultRecords).Cache.Clear();
    foreach (PXResult<InventoryTranDetEnqResult, INTran> pxResult in collection)
      ((PXSelectBase) this.ResultRecords).Cache.SetStatus((object) PXResult<InventoryTranDetEnqResult, INTran>.op_Implicit(pxResult), (PXEntryStatus) 5);
    PXDelegateResult pxDelegateResult1 = new PXDelegateResult();
    ((List<object>) pxDelegateResult1).AddRange((IEnumerable<object>) collection);
    pxDelegateResult1.IsResultFiltered = true;
    pxDelegateResult1.IsResultSorted = true;
    pxDelegateResult1.IsResultTruncated = num > ((List<object>) pxDelegateResult1).Count;
    return (IEnumerable) pxDelegateResult1;
  }

  protected virtual IEnumerable internalResultRecords()
  {
    InventoryTranDetEnqFilter current = ((PXSelectBase<InventoryTranDetEnqFilter>) this.Filter).Current;
    bool valueOrDefault1 = current.SummaryByDay.GetValueOrDefault();
    bool valueOrDefault2 = current.IncludeUnreleased.GetValueOrDefault();
    bool valueOrDefault3 = current.ByFinancialPeriod.GetValueOrDefault();
    PXSelectBase<INTran> pxSelectBase1 = (PXSelectBase<INTran>) new PXSelectJoinGroupBy<INTran, LeftJoin<INTranSplit, On<INTranSplit.FK.Tran>, LeftJoin<INSubItem, On<INSubItem.subItemID, Equal<INTran.subItemID>, Or<INSubItem.subItemID, Equal<INTranSplit.subItemID>>>, InnerJoin<INSite, On<INTran.FK.Site>>>>, Where<INTran.inventoryID, Equal<Current<InventoryTranDetEnqFilter.inventoryID>>, And2<Match<INSite, Current<AccessInfo.userName>>, And<Where<INTran.isStockItem, IsNull, Or<INTran.isStockItem, Equal<True>>>>>>, Aggregate<GroupBy<INTran.inventoryID, GroupBy<INTran.invtMult, GroupBy<INTranSplit.invtMult, Sum<INTranSplit.baseQty, Sum<INTran.tranCost, Sum<INTranSplit.totalQty, Sum<INTranSplit.estCost, Sum<INTranSplit.totalCost>>>>>>>>>>((PXGraph) this);
    PXSelectBase<INTran> pxSelectBase2 = (PXSelectBase<INTran>) new PXSelectReadonly2<INTran, LeftJoin<INTranSplit, On<INTranSplit.FK.Tran>, LeftJoin<INSubItem, On<INSubItem.subItemID, Equal<INTran.subItemID>, Or<INSubItem.subItemID, Equal<INTranSplit.subItemID>>>, InnerJoin<INSite, On<INTran.FK.Site>>>>, Where<INTran.inventoryID, Equal<Current<InventoryTranDetEnqFilter.inventoryID>>, And2<Match<INSite, Current<AccessInfo.userName>>, And<Where<INTran.isStockItem, IsNull, Or<INTran.isStockItem, Equal<True>>>>>>>((PXGraph) this);
    string[] newSorts;
    bool[] newDescs;
    bool sortsChanged;
    bool filtersChanged;
    this.AlterSortsAndFilters(out newSorts, out newDescs, out sortsChanged, out PXFilterRow[] _, out filtersChanged);
    DateTime? nullable1;
    if (!valueOrDefault3)
    {
      pxSelectBase2.WhereAnd<Where<INTran.tranPeriodID, Equal<Current<InventoryTranDetEnqFilter.finPeriodID>>>>();
      if (current.StartDate.HasValue)
      {
        pxSelectBase2.WhereAnd<Where<INTran.tranDate, GreaterEqual<Current<InventoryTranDetEnqFilter.startDate>>>>();
        pxSelectBase1.WhereAnd<Where<INTran.tranPeriodID, Less<Current<InventoryTranDetEnqFilter.finPeriodID>>, Or<Where<INTran.tranPeriodID, Equal<Current<InventoryTranDetEnqFilter.finPeriodID>>, And<INTran.tranDate, Less<Current<InventoryTranDetEnqFilter.startDate>>>>>>>();
      }
      else
        pxSelectBase1.WhereAnd<Where<INTran.tranPeriodID, Less<Current<InventoryTranDetEnqFilter.finPeriodID>>>>();
      nullable1 = current.EndDate;
      if (nullable1.HasValue)
        pxSelectBase2.WhereAnd<Where<INTran.tranDate, LessEqual<Current<InventoryTranDetEnqFilter.endDate>>>>();
    }
    else
    {
      pxSelectBase2.WhereAnd<Where<INTran.finPeriodID, Equal<Current<InventoryTranDetEnqFilter.finPeriodID>>>>();
      pxSelectBase1.WhereAnd<Where<INTran.finPeriodID, Less<Current<InventoryTranDetEnqFilter.finPeriodID>>>>();
    }
    int? nullable2 = current.SiteID;
    if (nullable2.HasValue)
    {
      pxSelectBase1.WhereAnd<Where<INTran.siteID, Equal<Current<InventoryTranDetEnqFilter.siteID>>>>();
      pxSelectBase2.WhereAnd<Where<INTran.siteID, Equal<Current<InventoryTranDetEnqFilter.siteID>>>>();
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
    {
      pxSelectBase2.WhereAnd<Where<INSite.branchID, InsideBranchesOf<Current<InventoryTranDetEnqFilter.orgBAccountID>>>>();
      pxSelectBase1.WhereAnd<Where<INSite.branchID, InsideBranchesOf<Current<InventoryTranDetEnqFilter.orgBAccountID>>>>();
    }
    if (!SubCDUtils.IsSubCDEmpty(current.SubItemCD))
    {
      pxSelectBase1.WhereAnd<Where<INSubItem.subItemCD, Like<Current<InventoryTranDetEnqFilter.subItemCDWildcard>>>>();
      pxSelectBase2.WhereAnd<Where<INSubItem.subItemCD, Like<Current<InventoryTranDetEnqFilter.subItemCDWildcard>>>>();
    }
    nullable2 = current.LocationID;
    if ((nullable2 ?? -1) != -1)
    {
      pxSelectBase1.WhereAnd<Where<INTran.locationID, Equal<Current<InventoryTranDetEnqFilter.locationID>>, Or<INTranSplit.locationID, Equal<Current<InventoryTranDetEnqFilter.locationID>>>>>();
      pxSelectBase2.WhereAnd<Where<INTran.locationID, Equal<Current<InventoryTranDetEnqFilter.locationID>>, Or<INTranSplit.locationID, Equal<Current<InventoryTranDetEnqFilter.locationID>>>>>();
    }
    if ((current.LotSerialNbr ?? "") != "")
    {
      pxSelectBase1.WhereAnd<Where<INTran.lotSerialNbr, Like<Current<InventoryTranDetEnqFilter.lotSerialNbrWildcard>>, Or<INTranSplit.lotSerialNbr, Like<Current<InventoryTranDetEnqFilter.lotSerialNbrWildcard>>>>>();
      pxSelectBase2.WhereAnd<Where<INTran.lotSerialNbr, Like<Current<InventoryTranDetEnqFilter.lotSerialNbrWildcard>>, Or<INTranSplit.lotSerialNbr, Like<Current<InventoryTranDetEnqFilter.lotSerialNbrWildcard>>>>>();
    }
    if (!valueOrDefault2)
    {
      pxSelectBase1.WhereAnd<Where<INTran.released, Equal<boolTrue>>>();
      pxSelectBase2.WhereAnd<Where<INTran.released, Equal<boolTrue>>>();
    }
    int num1 = 0;
    int num2 = 0;
    Decimal num3 = 0M;
    Decimal num4 = 0M;
    if (valueOrDefault3)
    {
      foreach (PXResult<INTran, INTranSplit, INSubItem> pxResult in ((PXSelectBase) pxSelectBase1).View.Select(PXView.Currents, PXView.Parameters, new object[newSorts.Length], newSorts, newDescs, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref num1, 0, ref num2))
      {
        INTranSplit inTranSplit = PXResult<INTran, INTranSplit, INSubItem>.op_Implicit(pxResult);
        INTran inTran = PXResult<INTran, INTranSplit, INSubItem>.op_Implicit(pxResult);
        Decimal num5 = num3;
        short? invtMult = inTranSplit.InvtMult;
        Decimal? nullable3 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable4 = inTranSplit.BaseQty;
        Decimal valueOrDefault4 = (nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * nullable4.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
        num3 = num5 + valueOrDefault4;
        invtMult = inTranSplit.InvtMult;
        if (!invtMult.HasValue)
        {
          Decimal num6 = num4;
          invtMult = inTran.InvtMult;
          nullable3 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
          nullable4 = inTran.TranCost;
          Decimal valueOrDefault5 = (nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * nullable4.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
          num4 = num6 + valueOrDefault5;
        }
        else
        {
          nullable3 = inTranSplit.TotalQty;
          Decimal num7 = 0M;
          if (!(nullable3.GetValueOrDefault() == num7 & nullable3.HasValue))
          {
            Decimal num8 = num4;
            invtMult = inTranSplit.InvtMult;
            nullable3 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
            nullable4 = inTranSplit.EstCost;
            Decimal valueOrDefault6 = (nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * nullable4.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
            num4 = num8 + valueOrDefault6;
          }
          else
          {
            Decimal num9 = num4;
            invtMult = inTranSplit.InvtMult;
            nullable3 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
            nullable4 = inTranSplit.TotalCost;
            Decimal valueOrDefault7 = (nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * nullable4.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
            num4 = num9 + valueOrDefault7;
          }
        }
      }
    }
    int num10 = (valueOrDefault1 || sortsChanged || filtersChanged ? 0 : (!PXView.ReverseOrder ? 1 : 0)) != 0 ? PXView.StartRow + PXView.MaximumRows : 0;
    int num11 = 0;
    List<object> objectList = ((PXSelectBase) pxSelectBase2).View.Select(PXView.Currents, PXView.Parameters, new object[newSorts.Length], newSorts, newDescs, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref num1, num10, ref num11);
    List<PXResult<InventoryTranDetEnqResult, INTran>> collection = new List<PXResult<InventoryTranDetEnqResult, INTran>>();
    int num12 = 0;
    foreach (PXResult<INTran, INTranSplit, INSubItem> pxResult in objectList)
    {
      INTranSplit inTranSplit = PXResult<INTran, INTranSplit, INSubItem>.op_Implicit(pxResult);
      INTran inTran = PXResult<INTran, INTranSplit, INSubItem>.op_Implicit(pxResult);
      short? invtMult = inTranSplit.InvtMult;
      Decimal? nullable5 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable6 = inTranSplit.BaseQty;
      Decimal valueOrDefault8 = (nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() * nullable6.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
      invtMult = inTranSplit.InvtMult;
      Decimal valueOrDefault9;
      if (!invtMult.HasValue)
      {
        invtMult = inTran.InvtMult;
        nullable5 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
        nullable6 = inTran.TranCost;
        valueOrDefault9 = (nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() * nullable6.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
      }
      else
      {
        nullable5 = inTranSplit.TotalQty;
        Decimal num13 = 0M;
        if (!(nullable5.GetValueOrDefault() == num13 & nullable5.HasValue))
        {
          invtMult = inTranSplit.InvtMult;
          nullable5 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
          nullable6 = inTranSplit.EstCost;
          valueOrDefault9 = (nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() * nullable6.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
        }
        else
        {
          invtMult = inTranSplit.InvtMult;
          nullable5 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
          nullable6 = inTranSplit.TotalCost;
          valueOrDefault9 = (nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() * nullable6.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
        }
      }
      if (!(valueOrDefault3 & inTran.FinPeriodID.CompareTo(current.FinPeriodID) < 0) && !(!valueOrDefault3 & inTran.TranPeriodID.CompareTo(current.FinPeriodID) < 0))
      {
        nullable1 = current.StartDate;
        DateTime? nullable7;
        if (nullable1.HasValue)
        {
          nullable1 = inTran.TranDate;
          nullable7 = current.StartDate;
          if ((nullable1.HasValue & nullable7.HasValue ? (nullable1.GetValueOrDefault() < nullable7.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            continue;
        }
        if (valueOrDefault1)
        {
          if (collection.Count > 0)
          {
            nullable7 = PXResult<InventoryTranDetEnqResult, INTran>.op_Implicit(collection[collection.Count - 1]).TranDate;
            nullable1 = inTran.TranDate;
            if ((nullable7.HasValue == nullable1.HasValue ? (nullable7.HasValue ? (nullable7.GetValueOrDefault() == nullable1.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
            {
              InventoryTranDetEnqResult tranDetEnqResult1 = PXResult<InventoryTranDetEnqResult, INTran>.op_Implicit(collection[collection.Count - 1]);
              if (valueOrDefault8 >= 0M)
              {
                InventoryTranDetEnqResult tranDetEnqResult2 = tranDetEnqResult1;
                nullable5 = tranDetEnqResult2.QtyIn;
                Decimal num14 = valueOrDefault8;
                Decimal? nullable8;
                if (!nullable5.HasValue)
                {
                  nullable6 = new Decimal?();
                  nullable8 = nullable6;
                }
                else
                  nullable8 = new Decimal?(nullable5.GetValueOrDefault() + num14);
                tranDetEnqResult2.QtyIn = nullable8;
              }
              else
              {
                InventoryTranDetEnqResult tranDetEnqResult3 = tranDetEnqResult1;
                nullable5 = tranDetEnqResult3.QtyOut;
                Decimal num15 = valueOrDefault8;
                Decimal? nullable9;
                if (!nullable5.HasValue)
                {
                  nullable6 = new Decimal?();
                  nullable9 = nullable6;
                }
                else
                  nullable9 = new Decimal?(nullable5.GetValueOrDefault() - num15);
                tranDetEnqResult3.QtyOut = nullable9;
              }
              InventoryTranDetEnqResult tranDetEnqResult4 = tranDetEnqResult1;
              nullable5 = tranDetEnqResult4.EndQty;
              Decimal num16 = valueOrDefault8;
              Decimal? nullable10;
              if (!nullable5.HasValue)
              {
                nullable6 = new Decimal?();
                nullable10 = nullable6;
              }
              else
                nullable10 = new Decimal?(nullable5.GetValueOrDefault() + num16);
              tranDetEnqResult4.EndQty = nullable10;
              if (!valueOrDefault2)
              {
                if (valueOrDefault9 >= 0M)
                {
                  InventoryTranDetEnqResult tranDetEnqResult5 = tranDetEnqResult1;
                  nullable5 = tranDetEnqResult5.ExtCostIn;
                  Decimal num17 = valueOrDefault9;
                  Decimal? nullable11;
                  if (!nullable5.HasValue)
                  {
                    nullable6 = new Decimal?();
                    nullable11 = nullable6;
                  }
                  else
                    nullable11 = new Decimal?(nullable5.GetValueOrDefault() + num17);
                  tranDetEnqResult5.ExtCostIn = nullable11;
                }
                else
                {
                  InventoryTranDetEnqResult tranDetEnqResult6 = tranDetEnqResult1;
                  nullable5 = tranDetEnqResult6.ExtCostOut;
                  Decimal num18 = valueOrDefault9;
                  Decimal? nullable12;
                  if (!nullable5.HasValue)
                  {
                    nullable6 = new Decimal?();
                    nullable12 = nullable6;
                  }
                  else
                    nullable12 = new Decimal?(nullable5.GetValueOrDefault() - num18);
                  tranDetEnqResult6.ExtCostOut = nullable12;
                }
                InventoryTranDetEnqResult tranDetEnqResult7 = tranDetEnqResult1;
                nullable5 = tranDetEnqResult7.EndBalance;
                Decimal num19 = valueOrDefault9;
                Decimal? nullable13;
                if (!nullable5.HasValue)
                {
                  nullable6 = new Decimal?();
                  nullable13 = nullable6;
                }
                else
                  nullable13 = new Decimal?(nullable5.GetValueOrDefault() + num19);
                tranDetEnqResult7.EndBalance = nullable13;
              }
              num3 += valueOrDefault8;
              num4 += valueOrDefault9;
              continue;
            }
          }
          InventoryTranDetEnqResult tranDetEnqResult8 = new InventoryTranDetEnqResult();
          tranDetEnqResult8.TranDate = inTran.TranDate;
          if (valueOrDefault8 >= 0M)
          {
            tranDetEnqResult8.QtyIn = new Decimal?(valueOrDefault8);
            tranDetEnqResult8.QtyOut = new Decimal?(0M);
          }
          else
          {
            tranDetEnqResult8.QtyIn = new Decimal?(0M);
            tranDetEnqResult8.QtyOut = new Decimal?(-valueOrDefault8);
          }
          if (!valueOrDefault2)
          {
            if (valueOrDefault9 >= 0M)
            {
              tranDetEnqResult8.ExtCostIn = new Decimal?(valueOrDefault9);
              tranDetEnqResult8.ExtCostOut = new Decimal?(0M);
            }
            else
            {
              tranDetEnqResult8.ExtCostIn = new Decimal?(0M);
              tranDetEnqResult8.ExtCostOut = new Decimal?(-valueOrDefault9);
            }
          }
          if (!valueOrDefault3)
          {
            InventoryTranDetEnqResult tranDetEnqResult9 = tranDetEnqResult8;
            nullable5 = tranDetEnqResult8.BegQty;
            Decimal num20 = valueOrDefault8;
            Decimal? nullable14;
            if (!nullable5.HasValue)
            {
              nullable6 = new Decimal?();
              nullable14 = nullable6;
            }
            else
              nullable14 = new Decimal?(nullable5.GetValueOrDefault() + num20);
            tranDetEnqResult9.EndQty = nullable14;
            if (!valueOrDefault2)
            {
              InventoryTranDetEnqResult tranDetEnqResult10 = tranDetEnqResult8;
              nullable5 = tranDetEnqResult8.BegBalance;
              Decimal num21 = valueOrDefault9;
              Decimal? nullable15;
              if (!nullable5.HasValue)
              {
                nullable6 = new Decimal?();
                nullable15 = nullable6;
              }
              else
                nullable15 = new Decimal?(nullable5.GetValueOrDefault() + num21);
              tranDetEnqResult10.EndBalance = nullable15;
            }
          }
          tranDetEnqResult8.GridLineNbr = new int?(++num12);
          tranDetEnqResult8.BegQty = new Decimal?(num3);
          tranDetEnqResult8.BegBalance = new Decimal?(num4);
          Decimal num22 = num3;
          nullable5 = tranDetEnqResult8.QtyIn;
          Decimal valueOrDefault10 = nullable5.GetValueOrDefault();
          nullable5 = tranDetEnqResult8.QtyOut;
          Decimal valueOrDefault11 = nullable5.GetValueOrDefault();
          Decimal num23 = valueOrDefault10 - valueOrDefault11;
          num3 = num22 + num23;
          Decimal num24 = num4;
          nullable5 = tranDetEnqResult8.ExtCostIn;
          Decimal valueOrDefault12 = nullable5.GetValueOrDefault();
          nullable5 = tranDetEnqResult8.ExtCostOut;
          Decimal valueOrDefault13 = nullable5.GetValueOrDefault();
          Decimal num25 = valueOrDefault12 - valueOrDefault13;
          num4 = num24 + num25;
          tranDetEnqResult8.EndQty = new Decimal?(num3);
          tranDetEnqResult8.EndBalance = new Decimal?(num4);
          collection.Add(new PXResult<InventoryTranDetEnqResult, INTran>(tranDetEnqResult8, (INTran) null));
        }
        else
        {
          InventoryTranDetEnqResult tranDetEnqResult11 = new InventoryTranDetEnqResult();
          tranDetEnqResult11.TranDate = inTran.TranDate;
          if (valueOrDefault8 >= 0M)
          {
            tranDetEnqResult11.QtyIn = new Decimal?(valueOrDefault8);
            tranDetEnqResult11.QtyOut = new Decimal?(0M);
          }
          else
          {
            tranDetEnqResult11.QtyIn = new Decimal?(0M);
            tranDetEnqResult11.QtyOut = new Decimal?(-valueOrDefault8);
          }
          if (!valueOrDefault2)
          {
            if (valueOrDefault9 >= 0M)
            {
              tranDetEnqResult11.ExtCostIn = new Decimal?(valueOrDefault9);
              tranDetEnqResult11.ExtCostOut = new Decimal?(0M);
            }
            else
            {
              tranDetEnqResult11.ExtCostIn = new Decimal?(0M);
              tranDetEnqResult11.ExtCostOut = new Decimal?(-valueOrDefault9);
            }
          }
          if (valueOrDefault8 != 0M)
            tranDetEnqResult11.UnitCost = new Decimal?(valueOrDefault9 / valueOrDefault8);
          if (!valueOrDefault3)
          {
            InventoryTranDetEnqResult tranDetEnqResult12 = tranDetEnqResult11;
            nullable5 = tranDetEnqResult11.BegQty;
            Decimal num26 = valueOrDefault8;
            Decimal? nullable16;
            if (!nullable5.HasValue)
            {
              nullable6 = new Decimal?();
              nullable16 = nullable6;
            }
            else
              nullable16 = new Decimal?(nullable5.GetValueOrDefault() + num26);
            tranDetEnqResult12.EndQty = nullable16;
            if (!valueOrDefault2)
            {
              InventoryTranDetEnqResult tranDetEnqResult13 = tranDetEnqResult11;
              nullable5 = tranDetEnqResult11.BegBalance;
              Decimal num27 = valueOrDefault9;
              Decimal? nullable17;
              if (!nullable5.HasValue)
              {
                nullable6 = new Decimal?();
                nullable17 = nullable6;
              }
              else
                nullable17 = new Decimal?(nullable5.GetValueOrDefault() + num27);
              tranDetEnqResult13.EndBalance = nullable17;
            }
          }
          tranDetEnqResult11.TranType = inTran.TranType;
          tranDetEnqResult11.DocType = inTran.DocType;
          tranDetEnqResult11.POReceiptType = inTran.POReceiptType;
          tranDetEnqResult11.RefNbr = inTran.RefNbr;
          tranDetEnqResult11.LineNbr = inTran.LineNbr;
          InventoryTranDetEnqResult tranDetEnqResult14 = tranDetEnqResult11;
          nullable2 = inTranSplit.SubItemID;
          int? nullable18 = nullable2 ?? inTran.SubItemID;
          tranDetEnqResult14.SubItemID = nullable18;
          InventoryTranDetEnqResult tranDetEnqResult15 = tranDetEnqResult11;
          nullable2 = inTranSplit.SiteID;
          int? nullable19 = nullable2 ?? inTran.SiteID;
          tranDetEnqResult15.SiteID = nullable19;
          InventoryTranDetEnqResult tranDetEnqResult16 = tranDetEnqResult11;
          nullable2 = inTranSplit.LocationID;
          int? nullable20 = nullable2 ?? inTran.LocationID;
          tranDetEnqResult16.LocationID = nullable20;
          tranDetEnqResult11.LotSerialNbr = inTranSplit.LotSerialNbr ?? inTran.LotSerialNbr;
          tranDetEnqResult11.Released = inTran.Released;
          tranDetEnqResult11.GridLineNbr = new int?(++num12);
          tranDetEnqResult11.BegQty = new Decimal?(num3);
          tranDetEnqResult11.BegBalance = new Decimal?(num4);
          Decimal num28 = num3;
          nullable5 = tranDetEnqResult11.QtyIn;
          Decimal valueOrDefault14 = nullable5.GetValueOrDefault();
          nullable5 = tranDetEnqResult11.QtyOut;
          Decimal valueOrDefault15 = nullable5.GetValueOrDefault();
          Decimal num29 = valueOrDefault14 - valueOrDefault15;
          num3 = num28 + num29;
          Decimal num30 = num4;
          nullable5 = tranDetEnqResult11.ExtCostIn;
          Decimal valueOrDefault16 = nullable5.GetValueOrDefault();
          nullable5 = tranDetEnqResult11.ExtCostOut;
          Decimal valueOrDefault17 = nullable5.GetValueOrDefault();
          Decimal num31 = valueOrDefault16 - valueOrDefault17;
          num4 = num30 + num31;
          tranDetEnqResult11.EndQty = new Decimal?(num3);
          tranDetEnqResult11.EndBalance = new Decimal?(num4);
          collection.Add(new PXResult<InventoryTranDetEnqResult, INTran>(tranDetEnqResult11, inTran));
        }
      }
    }
    if (valueOrDefault1)
      return (IEnumerable) collection;
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    pxDelegateResult.IsResultFiltered = !filtersChanged;
    pxDelegateResult.IsResultSorted = !sortsChanged;
    pxDelegateResult.IsResultTruncated = num11 > collection.Count;
    if (!PXView.ReverseOrder)
    {
      ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) collection);
    }
    else
    {
      IEnumerable source = PXView.Sort((IEnumerable) collection);
      ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) source.Cast<PXResult<InventoryTranDetEnqResult, INTran>>());
      pxDelegateResult.IsResultSorted = true;
    }
    return (IEnumerable) pxDelegateResult;
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
    bool includeUnreleased = ((bool?) ((PXSelectBase<InventoryTranDetEnqFilter>) this.Filter).Current?.IncludeUnreleased).GetValueOrDefault();
    bool summaryByDay = ((bool?) ((PXSelectBase<InventoryTranDetEnqFilter>) this.Filter).Current?.SummaryByDay).GetValueOrDefault();
    List<string> newSortColumns = new List<string>();
    List<bool> newDescendings = new List<bool>();
    List<PXFilterRow> filters = new List<PXFilterRow>();
    string intranPrefix = "INTran__";
    string intranSplitPrefix = "INTranSplit__";
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
      AddSort("ReleasedDateTime", desc);
      AddSort("DocType", desc);
      AddSort("RefNbr", desc);
      AddSort(intranSplitPrefix + "LotSerialNbr", desc);
      AddSort(intranPrefix + "LotSerialNbr", desc);
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
      AddSort(intranSplitPrefix + "LotSerialNbr", desc);
      AddSort(intranPrefix + "LotSerialNbr", desc);
      AddSort("LineNbr", desc);
      byTranDate = true;
    }

    bool ProcessField(PXFilterRow filter, string sortField = null, bool desc = false)
    {
      bool flag = false;
      string str = (filter != null || sortField != null) && (filter == null || sortField == null) ? sortField : throw new PXArgumentException();
      if (filter != null)
        str = filter.DataField;
      if (str.StartsWith(intranPrefix))
        str = str.Substring(intranPrefix.Length);
      string strA1 = str;
      if (string.Compare(strA1, "GridLineNbr", true) == 0)
      {
        if (sortField != null)
        {
          if (!includeUnreleased && !summaryByDay)
            AddSortByReleasedDate(desc);
          else
            AddSortByTranDate(desc);
        }
        if (filter != null)
          flag = true;
      }
      else if (string.Compare(strA1, "ReleasedDateTime", true) == 0)
      {
        if (sortField != null)
          AddSortByReleasedDate(desc);
        if (filter != null)
          AddFilter(filter, str);
      }
      else if (string.Compare(strA1, "TranDate", true) == 0)
      {
        if (sortField != null)
          AddSortByTranDate(desc);
        if (filter != null)
          AddFilter(filter, str);
      }
      else
      {
        string strA2 = strA1;
        if (string.Compare(strA2, "LotSerialNbr", true) == 0 || string.Compare(strA2, "SubItemID", true) == 0 || string.Compare(strA2, "LocationID", true) == 0)
        {
          if (sortField != null)
          {
            AddSort(intranSplitPrefix + str, desc);
            AddSort(str, desc);
          }
          if (filter != null)
            AddFilter(filter, intranSplitPrefix + str);
        }
        else
        {
          string strA3 = strA1;
          if (string.Compare(strA3, "DocType", true) == 0 || string.Compare(strA3, "SiteID", true) == 0 || string.Compare(strA3, "FinPeriodID", true) == 0 || string.Compare(strA3, "TranPeriodID", true) == 0 || string.Compare(strA3, "Released", true) == 0 || string.Compare(strA3, "ReleasedDateTime", true) == 0 || string.Compare(strA3, "SOOrderType", true) == 0 || string.Compare(strA3, "SOOrderNbr", true) == 0)
          {
            if (sortField != null)
              AddSort(str, desc);
            if (filter != null)
              AddFilter(filter, str);
          }
          else
            flag = true;
        }
      }
      return flag;
    }
  }

  public virtual bool IsDirty => false;

  protected virtual void InventoryTranDetEnqFilter_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    InventoryTranDetEnqFilter row = (InventoryTranDetEnqFilter) e.Row;
    DateTime? nullable = row.PeriodStartDate;
    if (nullable.HasValue)
      return;
    nullable = row.PeriodEndDate;
    if (nullable.HasValue)
      return;
    this.ResetFilterDates(row);
  }

  protected virtual void ResetFilterDates(InventoryTranDetEnqFilter aRow)
  {
    FinPeriod byId = this.FinPeriodRepository.FindByID(new int?(0), aRow.FinPeriodID);
    if (byId == null)
      return;
    aRow.PeriodStartDate = byId.StartDate;
    aRow.PeriodEndDate = new DateTime?(byId.EndDate.Value);
    aRow.EndDate = new DateTime?();
    aRow.StartDate = new DateTime?();
  }

  protected virtual void InventoryTranDetEnqFilter_ByFinancialPeriod_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    InventoryTranDetEnqFilter row = (InventoryTranDetEnqFilter) e.Row;
    bool valueOrDefault = row.ByFinancialPeriod.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<InventoryTranDetEnqFilter.startDate>(cache, (object) null, !valueOrDefault);
    PXUIFieldAttribute.SetEnabled<InventoryTranDetEnqFilter.endDate>(cache, (object) null, !valueOrDefault);
    if (!valueOrDefault)
      return;
    cache.SetValueExt<InventoryTranDetEnqFilter.startDate>((object) row, (object) null);
    cache.SetValueExt<InventoryTranDetEnqFilter.endDate>((object) row, (object) null);
  }

  protected virtual void InventoryTranDetEnqFilter_FinPeriodID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    this.ResetFilterDates((InventoryTranDetEnqFilter) e.Row);
  }

  protected virtual void InventoryTranDetEnqFilter_StartDate_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    InventoryTranDetEnqFilter row = (InventoryTranDetEnqFilter) e.Row;
    DateTime? newValue = (DateTime?) e.NewValue;
    if (newValue.HasValue)
    {
      DateTime? nullable = row.EndDate;
      if (nullable.HasValue)
      {
        nullable = newValue;
        DateTime dateTime = row.EndDate.Value;
        if ((nullable.HasValue ? (nullable.GetValueOrDefault() > dateTime ? 1 : 0) : 0) != 0)
          throw new PXSetPropertyException("Start date must be less or equal to the end date.");
      }
    }
    InventoryTranDetEnqFilter current = ((PXSelectBase<InventoryTranDetEnqFilter>) this.Filter).Current;
  }

  protected virtual void InventoryTranDetEnqFilter_EndDate_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    InventoryTranDetEnqFilter row = (InventoryTranDetEnqFilter) e.Row;
    DateTime? newValue = (DateTime?) e.NewValue;
    if (!newValue.HasValue)
      return;
    DateTime? nullable = row.StartDate;
    if (!nullable.HasValue)
      return;
    nullable = newValue;
    DateTime dateTime = row.StartDate.Value;
    if ((nullable.HasValue ? (nullable.GetValueOrDefault() < dateTime ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException("Start date must be less or equal to the end date.");
  }

  [PXUIField]
  [PXPreviousButton]
  public virtual IEnumerable previousperiod(PXAdapter adapter)
  {
    InventoryTranDetEnqFilter current = ((PXSelectBase<InventoryTranDetEnqFilter>) this.Filter).Current;
    FinPeriod prevPeriod = this.FinPeriodRepository.FindPrevPeriod(new int?(0), current.FinPeriodID, true);
    current.FinPeriodID = prevPeriod?.FinPeriodID;
    this.ResetFilterDates(current);
    return adapter.Get();
  }

  [PXUIField]
  [PXNextButton]
  public virtual IEnumerable nextperiod(PXAdapter adapter)
  {
    InventoryTranDetEnqFilter current = ((PXSelectBase<InventoryTranDetEnqFilter>) this.Filter).Current;
    FinPeriod nextPeriod = this.FinPeriodRepository.FindNextPeriod(new int?(0), current.FinPeriodID, true);
    current.FinPeriodID = nextPeriod.FinPeriodID;
    this.ResetFilterDates(current);
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Inventory Summary")]
  protected virtual IEnumerable ViewSummary(PXAdapter a)
  {
    if (((PXSelectBase<InventoryTranDetEnqResult>) this.ResultRecords).Current != null)
    {
      InventoryTranDetEnqResult current = ((PXSelectBase<InventoryTranDetEnqResult>) this.ResultRecords).Current;
      INTran inTran = INTran.PK.Find((PXGraph) this, current.DocType, current.RefNbr, current.LineNbr);
      if (inTran != null)
      {
        PXSegmentedState valueExt = ((PXSelectBase) this.ResultRecords).Cache.GetValueExt<InventoryTranDetEnqResult.subItemID>((object) current) as PXSegmentedState;
        InventorySummaryEnq.Redirect(inTran.InventoryID, valueExt != null ? (string) ((PXFieldState) valueExt).Value : (string) null, inTran.SiteID, ((PXSelectBase<InventoryTranDetEnqResult>) this.ResultRecords).Current.LocationID, false);
      }
    }
    return a.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Allocation Details")]
  protected virtual IEnumerable ViewAllocDet(PXAdapter a)
  {
    if (((PXSelectBase<InventoryTranDetEnqResult>) this.ResultRecords).Current != null)
    {
      InventoryTranDetEnqResult current = ((PXSelectBase<InventoryTranDetEnqResult>) this.ResultRecords).Current;
      INTran inTran = INTran.PK.Find((PXGraph) this, current.DocType, current.RefNbr, current.LineNbr);
      if (inTran != null)
      {
        PXSegmentedState valueExt = ((PXSelectBase) this.ResultRecords).Cache.GetValueExt<InventoryTranDetEnqResult.subItemID>((object) current) as PXSegmentedState;
        InventoryAllocDetEnq.Redirect(inTran.InventoryID, valueExt != null ? (string) ((PXFieldState) valueExt).Value : (string) null, (string) null, inTran.SiteID, current.LocationID);
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
    InventoryTranDetEnq instance = PXGraph.CreateInstance<InventoryTranDetEnq>();
    if (!string.IsNullOrEmpty(finPeriodID))
      ((PXSelectBase<InventoryTranDetEnqFilter>) instance.Filter).Current.FinPeriodID = finPeriodID;
    ((PXSelectBase<InventoryTranDetEnqFilter>) instance.Filter).Current.InventoryID = inventoryID;
    ((PXSelectBase<InventoryTranDetEnqFilter>) instance.Filter).Current.SubItemCD = subItemCD;
    ((PXSelectBase<InventoryTranDetEnqFilter>) instance.Filter).Current.SiteID = siteID;
    ((PXSelectBase<InventoryTranDetEnqFilter>) instance.Filter).Current.LocationID = locationID;
    ((PXSelectBase<InventoryTranDetEnqFilter>) instance.Filter).Current.LotSerialNbr = lotSerNum;
    throw new PXRedirectRequiredException((PXGraph) instance, "Transaction Details");
  }
}
