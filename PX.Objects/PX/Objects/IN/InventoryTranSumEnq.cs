// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryTranSumEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

[TableAndChartDashboardType]
public class InventoryTranSumEnq : PXGraph<InventoryTranSumEnq>
{
  public PXFilter<InventoryTranSumEnqFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXSelectOrderBy<INItemSiteHist, OrderBy<Asc<INItemSiteHist.inventoryID, Asc<INItemSiteHist.subItemID, Asc<INItemSiteHist.siteID, Asc<INItemSiteHist.locationID>>>>>> ResultRecords;
  public PXCancel<InventoryTranSumEnqFilter> Cancel;
  public PXAction<InventoryTranSumEnqFilter> PreviousPeriod;
  public PXAction<InventoryTranSumEnqFilter> NextPeriod;
  public PXAction<InventoryTranSumEnqFilter> viewInventoryTranDet;
  public PXAction<InventoryTranSumEnqFilter> viewSummary;
  public PXAction<InventoryTranSumEnqFilter> viewAllocDet;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public InventoryTranSumEnq()
  {
    ((PXSelectBase) this.ResultRecords).Cache.AllowInsert = false;
    ((PXSelectBase) this.ResultRecords).Cache.AllowDelete = false;
    ((PXSelectBase) this.ResultRecords).Cache.AllowUpdate = false;
    PXGraph.FieldSelectingEvents fieldSelecting1 = ((PXGraph) this).FieldSelecting;
    InventoryTranSumEnq inventoryTranSumEnq1 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting1 = new PXFieldSelecting((object) inventoryTranSumEnq1, __vmethodptr(inventoryTranSumEnq1, QtyFieldSelecting<INItemSiteHist.tranPtdQtyAdjusted, INItemSiteHist.finPtdQtyAdjusted>));
    fieldSelecting1.AddHandler<INItemSiteHist.tranPtdQtyAdjusted>(pxFieldSelecting1);
    PXGraph.FieldSelectingEvents fieldSelecting2 = ((PXGraph) this).FieldSelecting;
    InventoryTranSumEnq inventoryTranSumEnq2 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting2 = new PXFieldSelecting((object) inventoryTranSumEnq2, __vmethodptr(inventoryTranSumEnq2, QtyFieldSelecting<INItemSiteHist.tranBegQty, INItemSiteHist.finBegQty>));
    fieldSelecting2.AddHandler<INItemSiteHist.tranBegQty>(pxFieldSelecting2);
    PXGraph.FieldSelectingEvents fieldSelecting3 = ((PXGraph) this).FieldSelecting;
    InventoryTranSumEnq inventoryTranSumEnq3 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting3 = new PXFieldSelecting((object) inventoryTranSumEnq3, __vmethodptr(inventoryTranSumEnq3, QtyFieldSelecting<INItemSiteHist.tranPtdQtyAssemblyIn, INItemSiteHist.finPtdQtyAssemblyIn>));
    fieldSelecting3.AddHandler<INItemSiteHist.tranPtdQtyAssemblyIn>(pxFieldSelecting3);
    PXGraph.FieldSelectingEvents fieldSelecting4 = ((PXGraph) this).FieldSelecting;
    InventoryTranSumEnq inventoryTranSumEnq4 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting4 = new PXFieldSelecting((object) inventoryTranSumEnq4, __vmethodptr(inventoryTranSumEnq4, QtyFieldSelecting<INItemSiteHist.tranPtdQtyAssemblyOut, INItemSiteHist.finPtdQtyAssemblyOut>));
    fieldSelecting4.AddHandler<INItemSiteHist.tranPtdQtyAssemblyOut>(pxFieldSelecting4);
    PXGraph.FieldSelectingEvents fieldSelecting5 = ((PXGraph) this).FieldSelecting;
    InventoryTranSumEnq inventoryTranSumEnq5 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting5 = new PXFieldSelecting((object) inventoryTranSumEnq5, __vmethodptr(inventoryTranSumEnq5, QtyFieldSelecting<INItemSiteHist.tranPtdQtyCreditMemos, INItemSiteHist.finPtdQtyCreditMemos>));
    fieldSelecting5.AddHandler<INItemSiteHist.tranPtdQtyCreditMemos>(pxFieldSelecting5);
    PXGraph.FieldSelectingEvents fieldSelecting6 = ((PXGraph) this).FieldSelecting;
    InventoryTranSumEnq inventoryTranSumEnq6 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting6 = new PXFieldSelecting((object) inventoryTranSumEnq6, __vmethodptr(inventoryTranSumEnq6, QtyFieldSelecting<INItemSiteHist.tranPtdQtyDropShipSales, INItemSiteHist.finPtdQtyDropShipSales>));
    fieldSelecting6.AddHandler<INItemSiteHist.tranPtdQtyDropShipSales>(pxFieldSelecting6);
    PXGraph.FieldSelectingEvents fieldSelecting7 = ((PXGraph) this).FieldSelecting;
    InventoryTranSumEnq inventoryTranSumEnq7 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting7 = new PXFieldSelecting((object) inventoryTranSumEnq7, __vmethodptr(inventoryTranSumEnq7, QtyFieldSelecting<INItemSiteHist.tranPtdQtyIssued, INItemSiteHist.finPtdQtyIssued>));
    fieldSelecting7.AddHandler<INItemSiteHist.tranPtdQtyIssued>(pxFieldSelecting7);
    PXGraph.FieldSelectingEvents fieldSelecting8 = ((PXGraph) this).FieldSelecting;
    InventoryTranSumEnq inventoryTranSumEnq8 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting8 = new PXFieldSelecting((object) inventoryTranSumEnq8, __vmethodptr(inventoryTranSumEnq8, QtyFieldSelecting<INItemSiteHist.tranPtdQtyReceived, INItemSiteHist.finPtdQtyReceived>));
    fieldSelecting8.AddHandler<INItemSiteHist.tranPtdQtyReceived>(pxFieldSelecting8);
    PXGraph.FieldSelectingEvents fieldSelecting9 = ((PXGraph) this).FieldSelecting;
    InventoryTranSumEnq inventoryTranSumEnq9 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting9 = new PXFieldSelecting((object) inventoryTranSumEnq9, __vmethodptr(inventoryTranSumEnq9, QtyFieldSelecting<INItemSiteHist.tranPtdQtySales, INItemSiteHist.finPtdQtySales>));
    fieldSelecting9.AddHandler<INItemSiteHist.tranPtdQtySales>(pxFieldSelecting9);
    PXGraph.FieldSelectingEvents fieldSelecting10 = ((PXGraph) this).FieldSelecting;
    InventoryTranSumEnq inventoryTranSumEnq10 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting10 = new PXFieldSelecting((object) inventoryTranSumEnq10, __vmethodptr(inventoryTranSumEnq10, QtyFieldSelecting<INItemSiteHist.tranPtdQtyTransferIn, INItemSiteHist.finPtdQtyTransferIn>));
    fieldSelecting10.AddHandler<INItemSiteHist.tranPtdQtyTransferIn>(pxFieldSelecting10);
    PXGraph.FieldSelectingEvents fieldSelecting11 = ((PXGraph) this).FieldSelecting;
    InventoryTranSumEnq inventoryTranSumEnq11 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting11 = new PXFieldSelecting((object) inventoryTranSumEnq11, __vmethodptr(inventoryTranSumEnq11, QtyFieldSelecting<INItemSiteHist.tranPtdQtyTransferOut, INItemSiteHist.finPtdQtyTransferOut>));
    fieldSelecting11.AddHandler<INItemSiteHist.tranPtdQtyTransferOut>(pxFieldSelecting11);
    PXGraph.FieldSelectingEvents fieldSelecting12 = ((PXGraph) this).FieldSelecting;
    InventoryTranSumEnq inventoryTranSumEnq12 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting12 = new PXFieldSelecting((object) inventoryTranSumEnq12, __vmethodptr(inventoryTranSumEnq12, QtyFieldSelecting<INItemSiteHist.tranYtdQty, INItemSiteHist.finYtdQty>));
    fieldSelecting12.AddHandler<INItemSiteHist.tranYtdQty>(pxFieldSelecting12);
  }

  protected virtual void InventoryTranSumEnqFilter_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible<INItemSiteHist.subItemID>(((PXSelectBase) this.ResultRecords).Cache, (object) null, ((PXSelectBase<InventoryTranSumEnqFilter>) this.Filter).Current.SubItemDetails.GetValueOrDefault());
    PXCache cache1 = ((PXSelectBase) this.ResultRecords).Cache;
    bool? nullable = ((PXSelectBase<InventoryTranSumEnqFilter>) this.Filter).Current.SiteDetails;
    int num1;
    if (!nullable.GetValueOrDefault())
    {
      nullable = ((PXSelectBase<InventoryTranSumEnqFilter>) this.Filter).Current.LocationDetails;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 1;
    PXUIFieldAttribute.SetVisible<INItemSiteHist.siteID>(cache1, (object) null, num1 != 0);
    PXCache cache2 = ((PXSelectBase) this.ResultRecords).Cache;
    nullable = ((PXSelectBase<InventoryTranSumEnqFilter>) this.Filter).Current.LocationDetails;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<INItemSiteHist.locationID>(cache2, (object) null, num2 != 0);
  }

  protected virtual void QtyFieldSelecting<TranQtyField, FinQtyField>(
    PXCache sender,
    PXFieldSelectingEventArgs e)
    where TranQtyField : IBqlField
    where FinQtyField : IBqlField
  {
    INItemSiteHist row = (INItemSiteHist) e.Row;
    if (row == null)
      return;
    e.ReturnValue = ((PXSelectBase<InventoryTranSumEnqFilter>) this.Filter).Current.ByFinancialPeriod.GetValueOrDefault() ? sender.GetValue<FinQtyField>((object) row) : sender.GetValue<TranQtyField>((object) row);
  }

  protected virtual IEnumerable resultRecords()
  {
    InventoryTranSumEnq inventoryTranSumEnq = this;
    if (((PXSelectBase<InventoryTranSumEnqFilter>) inventoryTranSumEnq.Filter).Current != null && !string.IsNullOrEmpty(((PXSelectBase<InventoryTranSumEnqFilter>) inventoryTranSumEnq.Filter).Current.FinPeriodID))
    {
      PXSelectBase<INItemSiteHistByPeriod> pxSelectBase = (PXSelectBase<INItemSiteHistByPeriod>) new PXSelectJoin<INItemSiteHistByPeriod, InnerJoin<INItemSiteHist, On<INItemSiteHist.inventoryID, Equal<INItemSiteHistByPeriod.inventoryID>, And<INItemSiteHist.siteID, Equal<INItemSiteHistByPeriod.siteID>, And<INItemSiteHist.subItemID, Equal<INItemSiteHistByPeriod.subItemID>, And<INItemSiteHist.locationID, Equal<INItemSiteHistByPeriod.locationID>, And<INItemSiteHist.finPeriodID, Equal<INItemSiteHistByPeriod.lastActivityPeriod>>>>>>, InnerJoin<INSubItem, On<INSubItem.subItemID, Equal<INItemSiteHistByPeriod.subItemID>>, InnerJoin<InventoryItem, On<InventoryItem.inventoryID, Equal<INItemSiteHistByPeriod.inventoryID>>, InnerJoin<INSite, On<INSite.siteID, Equal<INItemSiteHistByPeriod.siteID>>>>>>, Where<INItemSiteHistByPeriod.finPeriodID, Equal<Current<InventoryTranSumEnqFilter.finPeriodID>>, And2<Match<InventoryItem, Current<AccessInfo.userName>>, And<Match<INSite, Current<AccessInfo.userName>>>>>>((PXGraph) inventoryTranSumEnq);
      bool? nullable1 = ((PXSelectBase<InventoryTranSumEnqFilter>) inventoryTranSumEnq.Filter).Current.SiteDetails;
      int num1;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = ((PXSelectBase<InventoryTranSumEnqFilter>) inventoryTranSumEnq.Filter).Current.LocationDetails;
        num1 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
      else
        num1 = 1;
      bool SiteDetails = num1 != 0;
      nullable1 = ((PXSelectBase<InventoryTranSumEnqFilter>) inventoryTranSumEnq.Filter).Current.LocationDetails;
      bool LocationDetails = nullable1.GetValueOrDefault();
      nullable1 = ((PXSelectBase<InventoryTranSumEnqFilter>) inventoryTranSumEnq.Filter).Current.SubItemDetails;
      bool SubItemDetails = nullable1.GetValueOrDefault();
      if (((PXSelectBase<InventoryTranSumEnqFilter>) inventoryTranSumEnq.Filter).Current.InventoryID.HasValue)
        pxSelectBase.WhereAnd<Where<INItemSiteHistByPeriod.inventoryID, Equal<Current<InventoryTranSumEnqFilter.inventoryID>>>>();
      if (!SubCDUtils.IsSubCDEmpty(((PXSelectBase<InventoryTranSumEnqFilter>) inventoryTranSumEnq.Filter).Current.SubItemCD))
        pxSelectBase.WhereAnd<Where<INSubItem.subItemCD, Like<Current<InventoryTranSumEnqFilter.subItemCDWildcard>>>>();
      int? nullable2 = ((PXSelectBase<InventoryTranSumEnqFilter>) inventoryTranSumEnq.Filter).Current.SiteID;
      if (nullable2.HasValue)
        pxSelectBase.WhereAnd<Where<INItemSiteHistByPeriod.siteID, Equal<Current<InventoryTranSumEnqFilter.siteID>>>>();
      nullable2 = ((PXSelectBase<InventoryTranSumEnqFilter>) inventoryTranSumEnq.Filter).Current.LocationID;
      if ((nullable2 ?? -1) != -1)
        pxSelectBase.WhereAnd<Where<INItemSiteHistByPeriod.locationID, Equal<Current<InventoryTranSumEnqFilter.locationID>>>>();
      List<string> stringList = new List<string>();
      stringList.Add("inventoryID");
      if (SubItemDetails)
        stringList.Add("subItemID");
      if (SiteDetails)
        stringList.Add("siteID");
      if (LocationDetails)
        stringList.Add("locationID");
      int num2 = 0;
      int num3 = 0;
      List<PXResult<INItemSiteHistByPeriod, INItemSiteHist>> list = ((PXSelectBase) pxSelectBase).View.Select((object[]) null, (object[]) null, new object[stringList.Count], stringList.ToArray(), new bool[stringList.Count], PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref num2, 0, ref num3).Cast<PXResult<INItemSiteHistByPeriod, INItemSiteHist>>().ToList<PXResult<INItemSiteHistByPeriod, INItemSiteHist>>();
      INItemSiteHist inItemSiteHist1 = new INItemSiteHist();
      foreach (PXResult<INItemSiteHistByPeriod, INItemSiteHist> pxResult in list)
      {
        INItemSiteHistByPeriod byperiod = PXResult<INItemSiteHistByPeriod, INItemSiteHist>.op_Implicit(pxResult);
        INItemSiteHist hist = PXResult<INItemSiteHistByPeriod, INItemSiteHist>.op_Implicit(pxResult);
        if (!string.Equals(byperiod.FinPeriodID, byperiod.LastActivityPeriod))
        {
          hist.TranBegQty = hist.TranYtdQty;
          hist.TranPtdQtyAdjusted = new Decimal?(0M);
          hist.TranPtdQtyAssemblyIn = new Decimal?(0M);
          hist.TranPtdQtyAssemblyOut = new Decimal?(0M);
          hist.TranPtdQtyCreditMemos = new Decimal?(0M);
          hist.TranPtdQtyDropShipSales = new Decimal?(0M);
          hist.TranPtdQtyIssued = new Decimal?(0M);
          hist.TranPtdQtyReceived = new Decimal?(0M);
          hist.TranPtdQtySales = new Decimal?(0M);
          hist.TranPtdQtyTransferIn = new Decimal?(0M);
          hist.TranPtdQtyTransferOut = new Decimal?(0M);
          hist.FinBegQty = hist.FinYtdQty;
          hist.FinPtdQtyAdjusted = new Decimal?(0M);
          hist.FinPtdQtyAssemblyIn = new Decimal?(0M);
          hist.FinPtdQtyAssemblyOut = new Decimal?(0M);
          hist.FinPtdQtyCreditMemos = new Decimal?(0M);
          hist.FinPtdQtyDropShipSales = new Decimal?(0M);
          hist.FinPtdQtyIssued = new Decimal?(0M);
          hist.FinPtdQtyReceived = new Decimal?(0M);
          hist.FinPtdQtySales = new Decimal?(0M);
          hist.FinPtdQtyTransferIn = new Decimal?(0M);
          hist.FinPtdQtyTransferOut = new Decimal?(0M);
        }
        if (!object.Equals((object) inItemSiteHist1.InventoryID, (object) hist.InventoryID) || SubItemDetails && !object.Equals((object) inItemSiteHist1.SubItemID, (object) hist.SubItemID) || SiteDetails && !object.Equals((object) inItemSiteHist1.SiteID, (object) hist.SiteID) || LocationDetails && !object.Equals((object) inItemSiteHist1.LocationID, (object) hist.LocationID))
        {
          nullable2 = inItemSiteHist1.InventoryID;
          if (nullable2.HasValue)
          {
            nullable1 = ((PXSelectBase<InventoryTranSumEnqFilter>) inventoryTranSumEnq.Filter).Current.ShowItemsWithoutMovement;
            if (nullable1.GetValueOrDefault() || string.Equals(inItemSiteHist1.FinPeriodID, inItemSiteHist1.LastActivityPeriod))
              yield return (object) inItemSiteHist1;
          }
          inItemSiteHist1 = PXCache<INItemSiteHist>.CreateCopy(hist);
          inItemSiteHist1.FinPeriodID = byperiod.FinPeriodID;
          inItemSiteHist1.LastActivityPeriod = byperiod.LastActivityPeriod;
        }
        else
        {
          if (string.Compare(inItemSiteHist1.LastActivityPeriod, byperiod.LastActivityPeriod) < 0)
            inItemSiteHist1.LastActivityPeriod = byperiod.LastActivityPeriod;
          INItemSiteHist inItemSiteHist2 = inItemSiteHist1;
          Decimal? nullable3 = inItemSiteHist2.TranBegQty;
          Decimal? nullable4 = hist.TranBegQty;
          inItemSiteHist2.TranBegQty = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist3 = inItemSiteHist1;
          nullable4 = inItemSiteHist3.TranPtdQtyAdjusted;
          nullable3 = hist.TranPtdQtyAdjusted;
          inItemSiteHist3.TranPtdQtyAdjusted = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist4 = inItemSiteHist1;
          nullable3 = inItemSiteHist4.TranPtdQtyAssemblyIn;
          nullable4 = hist.TranPtdQtyAssemblyIn;
          inItemSiteHist4.TranPtdQtyAssemblyIn = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist5 = inItemSiteHist1;
          nullable4 = inItemSiteHist5.TranPtdQtyAssemblyOut;
          nullable3 = hist.TranPtdQtyAssemblyOut;
          inItemSiteHist5.TranPtdQtyAssemblyOut = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist6 = inItemSiteHist1;
          nullable3 = inItemSiteHist6.TranPtdQtyCreditMemos;
          nullable4 = hist.TranPtdQtyCreditMemos;
          inItemSiteHist6.TranPtdQtyCreditMemos = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist7 = inItemSiteHist1;
          nullable4 = inItemSiteHist7.TranPtdQtyDropShipSales;
          nullable3 = hist.TranPtdQtyDropShipSales;
          inItemSiteHist7.TranPtdQtyDropShipSales = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist8 = inItemSiteHist1;
          nullable3 = inItemSiteHist8.TranPtdQtyIssued;
          nullable4 = hist.TranPtdQtyIssued;
          inItemSiteHist8.TranPtdQtyIssued = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist9 = inItemSiteHist1;
          nullable4 = inItemSiteHist9.TranPtdQtyReceived;
          nullable3 = hist.TranPtdQtyReceived;
          inItemSiteHist9.TranPtdQtyReceived = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist10 = inItemSiteHist1;
          nullable3 = inItemSiteHist10.TranPtdQtySales;
          nullable4 = hist.TranPtdQtySales;
          inItemSiteHist10.TranPtdQtySales = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist11 = inItemSiteHist1;
          nullable4 = inItemSiteHist11.TranPtdQtyTransferIn;
          nullable3 = hist.TranPtdQtyTransferIn;
          inItemSiteHist11.TranPtdQtyTransferIn = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist12 = inItemSiteHist1;
          nullable3 = inItemSiteHist12.TranPtdQtyTransferOut;
          nullable4 = hist.TranPtdQtyTransferOut;
          inItemSiteHist12.TranPtdQtyTransferOut = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist13 = inItemSiteHist1;
          nullable4 = inItemSiteHist13.TranYtdQty;
          nullable3 = hist.TranYtdQty;
          inItemSiteHist13.TranYtdQty = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist14 = inItemSiteHist1;
          nullable3 = inItemSiteHist14.FinBegQty;
          nullable4 = hist.FinBegQty;
          inItemSiteHist14.FinBegQty = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist15 = inItemSiteHist1;
          nullable4 = inItemSiteHist15.FinPtdQtyAdjusted;
          nullable3 = hist.FinPtdQtyAdjusted;
          inItemSiteHist15.FinPtdQtyAdjusted = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist16 = inItemSiteHist1;
          nullable3 = inItemSiteHist16.FinPtdQtyAssemblyIn;
          nullable4 = hist.FinPtdQtyAssemblyIn;
          inItemSiteHist16.FinPtdQtyAssemblyIn = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist17 = inItemSiteHist1;
          nullable4 = inItemSiteHist17.FinPtdQtyAssemblyOut;
          nullable3 = hist.FinPtdQtyAssemblyOut;
          inItemSiteHist17.FinPtdQtyAssemblyOut = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist18 = inItemSiteHist1;
          nullable3 = inItemSiteHist18.FinPtdQtyCreditMemos;
          nullable4 = hist.FinPtdQtyCreditMemos;
          inItemSiteHist18.FinPtdQtyCreditMemos = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist19 = inItemSiteHist1;
          nullable4 = inItemSiteHist19.FinPtdQtyDropShipSales;
          nullable3 = hist.FinPtdQtyDropShipSales;
          inItemSiteHist19.FinPtdQtyDropShipSales = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist20 = inItemSiteHist1;
          nullable3 = inItemSiteHist20.FinPtdQtyIssued;
          nullable4 = hist.FinPtdQtyIssued;
          inItemSiteHist20.FinPtdQtyIssued = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist21 = inItemSiteHist1;
          nullable4 = inItemSiteHist21.FinPtdQtyReceived;
          nullable3 = hist.FinPtdQtyReceived;
          inItemSiteHist21.FinPtdQtyReceived = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist22 = inItemSiteHist1;
          nullable3 = inItemSiteHist22.FinPtdQtySales;
          nullable4 = hist.FinPtdQtySales;
          inItemSiteHist22.FinPtdQtySales = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist23 = inItemSiteHist1;
          nullable4 = inItemSiteHist23.FinPtdQtyTransferIn;
          nullable3 = hist.FinPtdQtyTransferIn;
          inItemSiteHist23.FinPtdQtyTransferIn = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist24 = inItemSiteHist1;
          nullable3 = inItemSiteHist24.FinPtdQtyTransferOut;
          nullable4 = hist.FinPtdQtyTransferOut;
          inItemSiteHist24.FinPtdQtyTransferOut = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
          INItemSiteHist inItemSiteHist25 = inItemSiteHist1;
          nullable4 = inItemSiteHist25.FinYtdQty;
          nullable3 = hist.FinYtdQty;
          inItemSiteHist25.FinYtdQty = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
        }
        byperiod = (INItemSiteHistByPeriod) null;
        hist = (INItemSiteHist) null;
      }
      nullable2 = inItemSiteHist1.InventoryID;
      if (nullable2.HasValue)
      {
        nullable1 = ((PXSelectBase<InventoryTranSumEnqFilter>) inventoryTranSumEnq.Filter).Current.ShowItemsWithoutMovement;
        if (nullable1.GetValueOrDefault() || string.Equals(inItemSiteHist1.FinPeriodID, inItemSiteHist1.LastActivityPeriod))
          yield return (object) inItemSiteHist1;
      }
    }
  }

  public virtual bool IsDirty => false;

  public virtual void InventoryTranSumEnqFilter_InventoryCD_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  [PXUIField]
  [PXPreviousButton]
  public virtual IEnumerable previousperiod(PXAdapter adapter)
  {
    InventoryTranSumEnqFilter current = ((PXSelectBase<InventoryTranSumEnqFilter>) this.Filter).Current;
    FinPeriod prevPeriod = this.FinPeriodRepository.FindPrevPeriod(new int?(0), current.FinPeriodID, true);
    current.FinPeriodID = prevPeriod?.FinPeriodID;
    return adapter.Get();
  }

  [PXUIField]
  [PXNextButton]
  public virtual IEnumerable nextperiod(PXAdapter adapter)
  {
    InventoryTranSumEnqFilter current = ((PXSelectBase<InventoryTranSumEnqFilter>) this.Filter).Current;
    FinPeriod nextPeriod = this.FinPeriodRepository.FindNextPeriod(new int?(0), current.FinPeriodID, true);
    current.FinPeriodID = nextPeriod?.FinPeriodID;
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Transaction Details")]
  [PXButton(IsLockedOnToolbar = true)]
  protected virtual IEnumerable ViewInventoryTranDet(PXAdapter adapter)
  {
    if (((PXSelectBase<INItemSiteHist>) this.ResultRecords).Current != null && ((PXSelectBase<InventoryTranSumEnqFilter>) this.Filter).Current != null)
    {
      INItemSiteHist current1 = ((PXSelectBase<INItemSiteHist>) this.ResultRecords).Current;
      InventoryTranSumEnqFilter current2 = ((PXSelectBase<InventoryTranSumEnqFilter>) this.Filter).Current;
      InventoryTranDetEnq instance = PXGraph.CreateInstance<InventoryTranDetEnq>();
      ((PXSelectBase<InventoryTranDetEnqFilter>) instance.Filter).Current.ByFinancialPeriod = current2.ByFinancialPeriod;
      ((PXSelectBase<InventoryTranDetEnqFilter>) instance.Filter).Current.FinPeriodID = current2.FinPeriodID;
      ((PXSelectBase<InventoryTranDetEnqFilter>) instance.Filter).Current.InventoryID = current1.InventoryID;
      if (current1.SubItemID.HasValue)
      {
        INSubItem inSubItem = INSubItem.PK.Find((PXGraph) instance, current1.SubItemID);
        if (inSubItem != null)
          ((PXSelectBase<InventoryTranDetEnqFilter>) instance.Filter).Current.SubItemCD = inSubItem.SubItemCD;
      }
      ((PXSelectBase<InventoryTranDetEnqFilter>) instance.Filter).Current.SiteID = current1.SiteID;
      ((PXSelectBase<InventoryTranDetEnqFilter>) instance.Filter).Current.LocationID = current1.LocationID;
      throw new PXRedirectRequiredException((PXGraph) instance, "Inventory Transaction Details");
    }
    return adapter.Get();
  }

  [PXButton(IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "Inventory Summary")]
  protected virtual IEnumerable ViewSummary(PXAdapter a)
  {
    if (((PXSelectBase<INItemSiteHist>) this.ResultRecords).Current != null)
      InventorySummaryEnq.Redirect(((PXSelectBase<INItemSiteHist>) this.ResultRecords).Current.InventoryID, ((PXSelectBase<InventoryTranSumEnqFilter>) this.Filter).Current.SubItemCD, ((PXSelectBase<InventoryTranSumEnqFilter>) this.Filter).Current.SiteID, ((PXSelectBase<InventoryTranSumEnqFilter>) this.Filter).Current.LocationID, false);
    return a.Get();
  }

  [PXButton(IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "Allocation Details")]
  protected virtual IEnumerable ViewAllocDet(PXAdapter a)
  {
    if (((PXSelectBase<INItemSiteHist>) this.ResultRecords).Current != null)
      InventoryAllocDetEnq.Redirect(((PXSelectBase<INItemSiteHist>) this.ResultRecords).Current.InventoryID, ((PXSelectBase<InventoryTranSumEnqFilter>) this.Filter).Current.SubItemCD, (string) null, ((PXSelectBase<InventoryTranSumEnqFilter>) this.Filter).Current.SiteID, ((PXSelectBase<InventoryTranSumEnqFilter>) this.Filter).Current.LocationID);
    return a.Get();
  }

  public static void Redirect(
    string finPeriodID,
    int? inventoryID,
    string subItemCD,
    int? siteID,
    int? locationID)
  {
    InventoryTranSumEnq instance = PXGraph.CreateInstance<InventoryTranSumEnq>();
    if (!string.IsNullOrEmpty(finPeriodID))
      ((PXSelectBase<InventoryTranSumEnqFilter>) instance.Filter).Current.FinPeriodID = finPeriodID;
    ((PXSelectBase<InventoryTranSumEnqFilter>) instance.Filter).Current.InventoryID = inventoryID;
    ((PXSelectBase<InventoryTranSumEnqFilter>) instance.Filter).Current.SubItemCD = subItemCD;
    ((PXSelectBase<InventoryTranSumEnqFilter>) instance.Filter).Current.SiteID = siteID;
    ((PXSelectBase<InventoryTranSumEnqFilter>) instance.Filter).Current.LocationID = locationID;
    throw new PXRedirectRequiredException((PXGraph) instance, "Transaction Summmary");
  }
}
