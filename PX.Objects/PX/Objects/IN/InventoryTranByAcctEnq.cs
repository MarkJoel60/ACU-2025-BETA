// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryTranByAcctEnq
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
using System.ComponentModel;

#nullable enable
namespace PX.Objects.IN;

[TableAndChartDashboardType]
public class InventoryTranByAcctEnq : PXGraph<
#nullable disable
InventoryTranByAcctEnq>
{
  public PXFilter<InventoryTranByAcctEnqFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXSelectJoin<InventoryTranByAcctEnqResult, CrossJoin<INTran>, Where<True, Equal<True>>, OrderBy<Asc<InventoryTranByAcctEnqResult.gridLineNbr>>> ResultRecords;
  public PXSelectJoin<InventoryTranByAcctEnqResult, CrossJoin<INTran>, Where<True, Equal<True>>, OrderBy<Asc<InventoryTranByAcctEnqResult.gridLineNbr>>> InternalResultRecords;
  public PXCancel<InventoryTranByAcctEnqFilter> Cancel;
  public PXAction<InventoryTranByAcctEnqFilter> PreviousPeriod;
  public PXAction<InventoryTranByAcctEnqFilter> NextPeriod;
  public PXSelect<INTran> Tran;
  protected string _SOOrderNbr;
  public PXAction<InventoryTranByAcctEnqFilter> viewItem;
  public PXAction<InventoryTranByAcctEnqFilter> viewSummary;
  public PXAction<InventoryTranByAcctEnqFilter> viewAllocDet;

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
  [PXSelector(typeof (Search<PX.Objects.PO.POReceipt.receiptNbr, Where<BqlOperand<PX.Objects.PO.POReceipt.receiptType, IBqlString>.IsEqual<BqlField<INTran.pOReceiptType, IBqlString>.FromCurrent>>>))]
  protected virtual void INTran_POReceiptNbr_CacheAttached(PXCache sender)
  {
  }

  public InventoryTranByAcctEnq()
  {
    ((PXSelectBase) this.ResultRecords).Cache.AllowInsert = false;
    ((PXSelectBase) this.ResultRecords).Cache.AllowDelete = false;
    ((PXSelectBase) this.ResultRecords).Cache.AllowUpdate = false;
  }

  protected virtual IEnumerable resultRecords()
  {
    InventoryTranByAcctEnqFilter current = ((PXSelectBase<InventoryTranByAcctEnqFilter>) this.Filter).Current;
    int num1 = 0;
    int num2 = 0;
    PXResultset<InventoryTranByAcctEnqResult> pxResultset = ((PXSelectBase<InventoryTranByAcctEnqResult>) this.InternalResultRecords).Select(Array.Empty<object>());
    if (pxResultset.Count == 0)
      return (IEnumerable) pxResultset;
    Decimal valueOrDefault = PXResult<InventoryTranByAcctEnqResult>.op_Implicit(pxResultset[0]).BegBalance.GetValueOrDefault();
    List<object> objectList = ((PXSelectBase) this.InternalResultRecords).View.Select(PXView.Currents, PXView.Parameters, new object[PXView.SortColumns.Length], PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref num1, 0, ref num2);
    foreach (PXResult<InventoryTranByAcctEnqResult> pxResult in objectList)
    {
      InventoryTranByAcctEnqResult tranByAcctEnqResult = PXResult<InventoryTranByAcctEnqResult>.op_Implicit(pxResult);
      tranByAcctEnqResult.BegBalance = new Decimal?(valueOrDefault);
      Decimal? debit = tranByAcctEnqResult.Debit;
      Decimal? credit = tranByAcctEnqResult.Credit;
      valueOrDefault += debit.GetValueOrDefault() - credit.GetValueOrDefault();
      tranByAcctEnqResult.EndBalance = new Decimal?(valueOrDefault);
    }
    return (IEnumerable) objectList;
  }

  protected virtual IEnumerable internalResultRecords()
  {
    InventoryTranByAcctEnqFilter current = ((PXSelectBase<InventoryTranByAcctEnqFilter>) this.Filter).Current;
    bool valueOrDefault1 = current.SummaryByDay.GetValueOrDefault();
    bool valueOrDefault2 = current.ByFinancialPeriod.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<InventoryTranByAcctEnqResult.tranType>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<InventoryTranByAcctEnqResult.docRefNbr>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<InventoryTranByAcctEnqResult.subItemCD>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<InventoryTranByAcctEnqResult.siteID>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<InventoryTranByAcctEnqResult.locationID>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<InventoryTranByAcctEnqResult.accountID>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<InventoryTranByAcctEnqResult.subID>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<InventoryTranByAcctEnqResult.inventoryID>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<InventoryTranByAcctEnqResult.costAdj>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<InventoryTranByAcctEnqResult.finPerNbr>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<InventoryTranByAcctEnqResult.tranPerNbr>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<InventoryTranByAcctEnqResult.qty>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<InventoryTranByAcctEnqResult.unitCost>(((PXSelectBase) this.ResultRecords).Cache, (object) null, !valueOrDefault1);
    PXUIFieldAttribute.SetVisible<InventoryTranByAcctEnqResult.begBalance>(((PXSelectBase) this.ResultRecords).Cache, (object) null, valueOrDefault2);
    PXUIFieldAttribute.SetVisible<InventoryTranByAcctEnqResult.endBalance>(((PXSelectBase) this.ResultRecords).Cache, (object) null, valueOrDefault2);
    PXUIFieldAttribute.SetVisible(((PXSelectBase) this.Tran).Cache, (string) null, !valueOrDefault1);
    List<PXResult<InventoryTranByAcctEnqResult, INTran>> pxResultList = new List<PXResult<InventoryTranByAcctEnqResult, INTran>>();
    Decimal num1 = 0M;
    if (!current.AccountID.HasValue)
      return (IEnumerable) pxResultList;
    if (current.FinPeriodID == null)
      return (IEnumerable) pxResultList;
    Decimal? nullable1;
    if (valueOrDefault2)
    {
      PXSelectBase<INItemCostHist> pxSelectBase = (PXSelectBase<INItemCostHist>) new PXSelectJoinGroupBy<INItemCostHist, InnerJoin<PX.Objects.GL.Sub, On<INItemCostHist.FK.Subaccount>>, Where<INItemCostHist.finPeriodID, Less<Current<InventoryTranByAcctEnqFilter.finPeriodID>>>, Aggregate<Sum<INItemCostHist.tranYtdCost, Sum<INItemCostHist.tranBegCost, Sum<INItemCostHist.finYtdCost, Sum<INItemCostHist.finBegCost>>>>>>((PXGraph) this);
      pxSelectBase.WhereAnd<Where<INItemCostHist.accountID, Equal<Current<InventoryTranByAcctEnqFilter.accountID>>>>();
      if (!SubCDUtils.IsSubCDEmpty(current.SubCD))
        pxSelectBase.WhereAnd<Where<PX.Objects.GL.Sub.subCD, Like<Current<InventoryTranByAcctEnqFilter.subCDWildcard>>>>();
      PXResultset<INItemCostHist> pxResultset = pxSelectBase.Select(Array.Empty<object>());
      if (pxResultset.Count == 1)
      {
        Decimal num2 = num1;
        Decimal valueOrDefault3 = PXResult<INItemCostHist>.op_Implicit(pxResultset[0]).FinYtdCost.GetValueOrDefault();
        nullable1 = PXResult<INItemCostHist>.op_Implicit(pxResultset[0]).FinBegCost;
        Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
        Decimal num3 = valueOrDefault3 - valueOrDefault4;
        num1 = num2 + num3;
      }
    }
    PXSelectBase<INTranCost> pxSelectBase1 = (PXSelectBase<INTranCost>) new PXSelectReadonly2<INTranCost, InnerJoin<INTran, On<INTranCost.FK.Tran>, InnerJoin<InventoryItem, On2<INTranCost.FK.InventoryItem, And<Match<InventoryItem, Current<AccessInfo.userName>>>>, InnerJoin<PX.Objects.GL.Sub, On<INTranCost.FK.InventorySubaccount>, InnerJoin<INSubItem, On<INTranCost.FK.CostSubItem>, LeftJoin<INSite, On<INTranCost.FK.CostSite>, LeftJoin<INLocation, On<INLocation.locationID, Equal<INTranCost.costSiteID>>, LeftJoin<INCostStatus, On<INTranCost.costID, Equal<INCostStatus.costID>>>>>>>>>, Where<INSite.siteID, IsNull, Or<Match<INSite, Current<AccessInfo.userName>>>>, OrderBy<Asc<INTranCost.tranDate, Asc<INTranCost.createdDateTime>>>>((PXGraph) this);
    DateTime? nullable2;
    if (valueOrDefault2)
    {
      pxSelectBase1.WhereAnd<Where<INTranCost.finPeriodID, Equal<Current<InventoryTranByAcctEnqFilter.finPeriodID>>>>();
    }
    else
    {
      pxSelectBase1.WhereAnd<Where<INTranCost.tranDate, GreaterEqual<Current<InventoryTranByAcctEnqFilter.periodStartDate>>>>();
      pxSelectBase1.WhereAnd<Where<INTranCost.tranDate, Less<Current<InventoryTranByAcctEnqFilter.periodEndDate>>>>();
      if (current.StartDate.HasValue)
        pxSelectBase1.WhereAnd<Where<INTranCost.tranDate, GreaterEqual<Current<InventoryTranByAcctEnqFilter.startDate>>>>();
      nullable2 = current.EndDate;
      if (nullable2.HasValue)
        pxSelectBase1.WhereAnd<Where<INTranCost.tranDate, LessEqual<Current<InventoryTranByAcctEnqFilter.endDate>>>>();
    }
    pxSelectBase1.WhereAnd<Where<INTranCost.invtAcctID, Equal<Current<InventoryTranByAcctEnqFilter.accountID>>>>();
    if (!SubCDUtils.IsSubCDEmpty(current.SubCD))
      pxSelectBase1.WhereAnd<Where<PX.Objects.GL.Sub.subCD, Like<Current<InventoryTranByAcctEnqFilter.subCDWildcard>>>>();
    int? nullable3 = current.InventoryID;
    if (nullable3.HasValue)
      pxSelectBase1.WhereAnd<Where<INTranCost.inventoryID, Equal<Current<InventoryTranByAcctEnqFilter.inventoryID>>>>();
    nullable3 = current.SiteID;
    if (nullable3.HasValue)
      pxSelectBase1.WhereAnd<Where<INTranCost.costSiteID, Equal<Current<InventoryTranByAcctEnqFilter.siteID>>>>();
    if (PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
    {
      pxSelectBase1.Join<InnerJoin<InventoryTranByAcctEnq.INSite2, On<InventoryTranByAcctEnq.INSite2.siteID, In3<INSite.siteID, INLocation.siteID>>>>();
      pxSelectBase1.WhereAnd<Where<InventoryTranByAcctEnq.INSite2.branchID, InsideBranchesOf<Current<InventoryTranByAcctEnqFilter.orgBAccountID>>>>();
    }
    int num4 = 0;
    foreach (PXResult<INTranCost, INTran, InventoryItem, PX.Objects.GL.Sub, INSubItem, INSite, INLocation, INCostStatus> pxResult in pxSelectBase1.Select(Array.Empty<object>()))
    {
      INTranCost inTranCost = PXResult<INTranCost, INTran, InventoryItem, PX.Objects.GL.Sub, INSubItem, INSite, INLocation, INCostStatus>.op_Implicit(pxResult);
      INTran inTran = PXResult<INTranCost, INTran, InventoryItem, PX.Objects.GL.Sub, INSubItem, INSite, INLocation, INCostStatus>.op_Implicit(pxResult);
      INSite inSite = PXResult<INTranCost, INTran, InventoryItem, PX.Objects.GL.Sub, INSubItem, INSite, INLocation, INCostStatus>.op_Implicit(pxResult);
      INLocation inLocation = PXResult<INTranCost, INTran, InventoryItem, PX.Objects.GL.Sub, INSubItem, INSite, INLocation, INCostStatus>.op_Implicit(pxResult);
      INSubItem inSubItem = PXResult<INTranCost, INTran, InventoryItem, PX.Objects.GL.Sub, INSubItem, INSite, INLocation, INCostStatus>.op_Implicit(pxResult);
      INCostStatus inCostStatus = PXResult<INTranCost, INTran, InventoryItem, PX.Objects.GL.Sub, INSubItem, INSite, INLocation, INCostStatus>.op_Implicit(pxResult);
      short? invtMult = inTranCost.InvtMult;
      nullable1 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable4 = inTranCost.TranCost;
      Decimal valueOrDefault5 = (nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable4.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
      nullable2 = inTranCost.TranDate;
      DateTime? nullable5 = current.StartDate;
      if ((nullable2.HasValue & nullable5.HasValue ? (nullable2.GetValueOrDefault() < nullable5.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        num1 += valueOrDefault5;
      else if (valueOrDefault1)
      {
        if (pxResultList.Count > 0)
        {
          nullable5 = PXResult<InventoryTranByAcctEnqResult, INTran>.op_Implicit(pxResultList[pxResultList.Count - 1]).TranDate;
          nullable2 = inTranCost.TranDate;
          if ((nullable5.HasValue == nullable2.HasValue ? (nullable5.HasValue ? (nullable5.GetValueOrDefault() == nullable2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
          {
            InventoryTranByAcctEnqResult tranByAcctEnqResult1 = PXResult<InventoryTranByAcctEnqResult, INTran>.op_Implicit(pxResultList[pxResultList.Count - 1]);
            if (valueOrDefault5 >= 0M)
            {
              InventoryTranByAcctEnqResult tranByAcctEnqResult2 = tranByAcctEnqResult1;
              nullable1 = tranByAcctEnqResult2.Debit;
              Decimal num5 = valueOrDefault5;
              Decimal? nullable6;
              if (!nullable1.HasValue)
              {
                nullable4 = new Decimal?();
                nullable6 = nullable4;
              }
              else
                nullable6 = new Decimal?(nullable1.GetValueOrDefault() + num5);
              tranByAcctEnqResult2.Debit = nullable6;
            }
            else
            {
              InventoryTranByAcctEnqResult tranByAcctEnqResult3 = tranByAcctEnqResult1;
              nullable1 = tranByAcctEnqResult3.Credit;
              Decimal num6 = valueOrDefault5;
              Decimal? nullable7;
              if (!nullable1.HasValue)
              {
                nullable4 = new Decimal?();
                nullable7 = nullable4;
              }
              else
                nullable7 = new Decimal?(nullable1.GetValueOrDefault() - num6);
              tranByAcctEnqResult3.Credit = nullable7;
            }
            InventoryTranByAcctEnqResult tranByAcctEnqResult4 = tranByAcctEnqResult1;
            nullable1 = tranByAcctEnqResult4.EndBalance;
            Decimal num7 = valueOrDefault5;
            Decimal? nullable8;
            if (!nullable1.HasValue)
            {
              nullable4 = new Decimal?();
              nullable8 = nullable4;
            }
            else
              nullable8 = new Decimal?(nullable1.GetValueOrDefault() + num7);
            tranByAcctEnqResult4.EndBalance = nullable8;
            pxResultList[pxResultList.Count - 1] = new PXResult<InventoryTranByAcctEnqResult, INTran>(tranByAcctEnqResult1, (INTran) null);
            goto label_50;
          }
        }
        InventoryTranByAcctEnqResult tranByAcctEnqResult5 = new InventoryTranByAcctEnqResult();
        tranByAcctEnqResult5.BegBalance = new Decimal?(num1);
        tranByAcctEnqResult5.TranDate = inTranCost.TranDate;
        if (valueOrDefault5 >= 0M)
        {
          tranByAcctEnqResult5.Debit = new Decimal?(valueOrDefault5);
          tranByAcctEnqResult5.Credit = new Decimal?(0M);
        }
        else
        {
          tranByAcctEnqResult5.Debit = new Decimal?(0M);
          tranByAcctEnqResult5.Credit = new Decimal?(-valueOrDefault5);
        }
        InventoryTranByAcctEnqResult tranByAcctEnqResult6 = tranByAcctEnqResult5;
        nullable1 = tranByAcctEnqResult5.BegBalance;
        Decimal num8 = valueOrDefault5;
        Decimal? nullable9;
        if (!nullable1.HasValue)
        {
          nullable4 = new Decimal?();
          nullable9 = nullable4;
        }
        else
          nullable9 = new Decimal?(nullable1.GetValueOrDefault() + num8);
        tranByAcctEnqResult6.EndBalance = nullable9;
        tranByAcctEnqResult5.GridLineNbr = new int?(++num4);
        tranByAcctEnqResult5.CreatedDateTime = inTranCost.CreatedDateTime;
        pxResultList.Add(new PXResult<InventoryTranByAcctEnqResult, INTran>(tranByAcctEnqResult5, (INTran) null));
label_50:
        num1 += valueOrDefault5;
      }
      else
      {
        InventoryTranByAcctEnqResult tranByAcctEnqResult7 = new InventoryTranByAcctEnqResult();
        tranByAcctEnqResult7.BegBalance = new Decimal?(num1);
        tranByAcctEnqResult7.TranDate = inTranCost.TranDate;
        if (valueOrDefault5 >= 0M)
        {
          tranByAcctEnqResult7.Debit = new Decimal?(valueOrDefault5);
          tranByAcctEnqResult7.Credit = new Decimal?(0M);
        }
        else
        {
          tranByAcctEnqResult7.Debit = new Decimal?(0M);
          tranByAcctEnqResult7.Credit = new Decimal?(-valueOrDefault5);
        }
        InventoryTranByAcctEnqResult tranByAcctEnqResult8 = tranByAcctEnqResult7;
        nullable1 = tranByAcctEnqResult7.BegBalance;
        Decimal num9 = valueOrDefault5;
        Decimal? nullable10;
        if (!nullable1.HasValue)
        {
          nullable4 = new Decimal?();
          nullable10 = nullable4;
        }
        else
          nullable10 = new Decimal?(nullable1.GetValueOrDefault() + num9);
        tranByAcctEnqResult8.EndBalance = nullable10;
        tranByAcctEnqResult7.AccountID = inTranCost.InvtAcctID;
        tranByAcctEnqResult7.SubID = inTranCost.InvtSubID;
        tranByAcctEnqResult7.TranType = inTranCost.TranType;
        tranByAcctEnqResult7.DocType = inTranCost.DocType;
        tranByAcctEnqResult7.DocRefNbr = inTranCost.RefNbr;
        tranByAcctEnqResult7.ReceiptNbr = inCostStatus.ReceiptNbr;
        tranByAcctEnqResult7.InventoryID = inTranCost.InventoryID;
        tranByAcctEnqResult7.SubItemCD = inSubItem.SubItemCD;
        nullable3 = inSite.SiteID;
        if (nullable3.HasValue)
        {
          tranByAcctEnqResult7.SiteID = inSite.SiteID;
          InventoryTranByAcctEnqResult tranByAcctEnqResult9 = tranByAcctEnqResult7;
          nullable3 = new int?();
          int? nullable11 = nullable3;
          tranByAcctEnqResult9.LocationID = nullable11;
        }
        else
        {
          nullable3 = inLocation.LocationID;
          if (nullable3.HasValue)
          {
            tranByAcctEnqResult7.SiteID = inLocation.SiteID;
            tranByAcctEnqResult7.LocationID = inLocation.LocationID;
          }
        }
        tranByAcctEnqResult7.TranDate = inTranCost.TranDate;
        tranByAcctEnqResult7.FinPerNbr = inTranCost.FinPeriodID;
        tranByAcctEnqResult7.TranPerNbr = inTranCost.TranPeriodID;
        InventoryTranByAcctEnqResult tranByAcctEnqResult10 = tranByAcctEnqResult7;
        nullable1 = inTranCost.Qty;
        invtMult = inTranCost.InvtMult;
        nullable4 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable12 = nullable1.HasValue & nullable4.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable4.GetValueOrDefault()) : new Decimal?();
        tranByAcctEnqResult10.Qty = nullable12;
        InventoryTranByAcctEnqResult tranByAcctEnqResult11 = tranByAcctEnqResult7;
        nullable4 = inTranCost.Qty;
        Decimal? nullable13;
        if (!(nullable4.GetValueOrDefault() == 0M))
        {
          nullable1 = inTranCost.TranCost;
          Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
          nullable1 = inTranCost.VarCost;
          Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
          Decimal num10 = valueOrDefault6 + valueOrDefault7;
          nullable4 = inTranCost.Qty;
          if (!nullable4.HasValue)
          {
            nullable1 = new Decimal?();
            nullable13 = nullable1;
          }
          else
            nullable13 = new Decimal?(num10 / nullable4.GetValueOrDefault());
        }
        else
        {
          nullable4 = new Decimal?();
          nullable13 = nullable4;
        }
        tranByAcctEnqResult11.UnitCost = nullable13;
        tranByAcctEnqResult7.CostAdj = new bool?(inTranCost.CostRefNbr != inTranCost.RefNbr);
        tranByAcctEnqResult7.GridLineNbr = new int?(++num4);
        tranByAcctEnqResult7.CreatedDateTime = inTranCost.CreatedDateTime;
        pxResultList.Add(new PXResult<InventoryTranByAcctEnqResult, INTran>(tranByAcctEnqResult7, inTran));
        num1 += valueOrDefault5;
      }
    }
    return (IEnumerable) pxResultList;
  }

  public virtual bool IsDirty => false;

  protected virtual void InventoryTranByAcctEnqFilter_PeriodStartDate_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is InventoryTranByAcctEnqFilter row))
      return;
    FinPeriod byId = this.FinPeriodRepository.FindByID(new int?(0), row.FinPeriodID);
    e.NewValue = (object) (DateTime?) byId?.StartDate;
  }

  protected virtual void InventoryTranByAcctEnqFilter_PeriodEndDate_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is InventoryTranByAcctEnqFilter row))
      return;
    FinPeriod byId = this.FinPeriodRepository.FindByID(new int?(0), row.FinPeriodID);
    e.NewValue = (object) (DateTime?) byId?.EndDate;
  }

  protected virtual void ResetFilterDates(InventoryTranByAcctEnqFilter aRow)
  {
    ((PXSelectBase) this.Filter).Cache.SetDefaultExt<InventoryTranByAcctEnqFilter.periodStartDate>((object) aRow);
    ((PXSelectBase) this.Filter).Cache.SetDefaultExt<InventoryTranByAcctEnqFilter.periodEndDate>((object) aRow);
    ((PXSelectBase) this.Filter).Cache.SetDefaultExt<InventoryTranByAcctEnqFilter.startDate>((object) aRow);
    ((PXSelectBase) this.Filter).Cache.SetDefaultExt<InventoryTranByAcctEnqFilter.endDate>((object) aRow);
  }

  protected virtual void InventoryTranByAcctEnqFilter_FinPeriodID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    this.ResetFilterDates((InventoryTranByAcctEnqFilter) e.Row);
  }

  protected virtual void InventoryTranByAcctEnqFilter_ByFinancialPeriod_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    InventoryTranByAcctEnqFilter row = (InventoryTranByAcctEnqFilter) e.Row;
    if (!row.ByFinancialPeriod.GetValueOrDefault())
      return;
    ((PXSelectBase) this.Filter).Cache.SetValueExt<InventoryTranByAcctEnqFilter.startDate>((object) row, (object) null);
    ((PXSelectBase) this.Filter).Cache.SetValueExt<InventoryTranByAcctEnqFilter.endDate>((object) row, (object) null);
  }

  protected virtual void InventoryTranByAcctEnqFilter_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs e)
  {
    bool valueOrDefault = ((InventoryTranByAcctEnqFilter) e.Row).ByFinancialPeriod.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<InventoryTranByAcctEnqFilter.startDate>(((PXSelectBase) this.Filter).Cache, (object) null, !valueOrDefault);
    PXUIFieldAttribute.SetEnabled<InventoryTranByAcctEnqFilter.endDate>(((PXSelectBase) this.Filter).Cache, (object) null, !valueOrDefault);
  }

  protected virtual void InventoryTranByAcctEnqFilter_SubCD_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void InventoryTranByAcctEnqFilter_RowUpdated(
    PXCache cache,
    PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase<InventoryTranByAcctEnqResult>) this.ResultRecords).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXPreviousButton]
  public virtual IEnumerable previousperiod(PXAdapter adapter)
  {
    InventoryTranByAcctEnqFilter current = ((PXSelectBase<InventoryTranByAcctEnqFilter>) this.Filter).Current;
    FinPeriod prevPeriod = this.FinPeriodRepository.FindPrevPeriod(new int?(0), current.FinPeriodID, true);
    current.FinPeriodID = prevPeriod?.FinPeriodID;
    this.ResetFilterDates(current);
    return adapter.Get();
  }

  [PXUIField]
  [PXNextButton]
  public virtual IEnumerable nextperiod(PXAdapter adapter)
  {
    InventoryTranByAcctEnqFilter current = ((PXSelectBase<InventoryTranByAcctEnqFilter>) this.Filter).Current;
    FinPeriod nextPeriod = this.FinPeriodRepository.FindNextPeriod(new int?(0), current.FinPeriodID, true);
    current.FinPeriodID = nextPeriod?.FinPeriodID;
    this.ResetFilterDates(current);
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "")]
  protected virtual IEnumerable ViewItem(PXAdapter a)
  {
    if (((PXSelectBase<InventoryTranByAcctEnqResult>) this.ResultRecords).Current != null)
      InventoryItemMaint.Redirect(((PXSelectBase<InventoryTranByAcctEnqResult>) this.ResultRecords).Current.InventoryID, true);
    return a.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Inventory Summary")]
  protected virtual IEnumerable ViewSummary(PXAdapter a)
  {
    if (((PXSelectBase<InventoryTranByAcctEnqResult>) this.ResultRecords).Current != null)
      InventorySummaryEnq.Redirect(((PXSelectBase<InventoryTranByAcctEnqResult>) this.ResultRecords).Current.InventoryID, ((PXSelectBase<InventoryTranByAcctEnqResult>) this.ResultRecords).Current.SubItemCD, ((PXSelectBase<InventoryTranByAcctEnqResult>) this.ResultRecords).Current.SiteID, ((PXSelectBase<InventoryTranByAcctEnqResult>) this.ResultRecords).Current.LocationID, false);
    return a.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Allocation Details")]
  protected virtual IEnumerable ViewAllocDet(PXAdapter a)
  {
    if (((PXSelectBase<InventoryTranByAcctEnqResult>) this.ResultRecords).Current != null)
      InventoryAllocDetEnq.Redirect(((PXSelectBase<InventoryTranByAcctEnqResult>) this.ResultRecords).Current.InventoryID, ((PXSelectBase<InventoryTranByAcctEnqResult>) this.ResultRecords).Current.SubItemCD, (string) null, ((PXSelectBase<InventoryTranByAcctEnqResult>) this.ResultRecords).Current.SiteID, ((PXSelectBase<InventoryTranByAcctEnqResult>) this.ResultRecords).Current.LocationID);
    return a.Get();
  }

  [PXHidden]
  public class INSite2 : INSite
  {
    public new abstract class siteID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryTranByAcctEnq.INSite2.siteID>
    {
    }

    public new abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      InventoryTranByAcctEnq.INSite2.branchID>
    {
    }
  }
}
