// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APDocumentEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP.Standalone;
using PX.Objects.CM;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.MigrationMode;
using PX.Objects.Common.Utility;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AP;

[TableAndChartDashboardType]
public class APDocumentEnq : PXGraph<
#nullable disable
APDocumentEnq>
{
  public PXSetup<PX.Objects.AP.APSetup> APSetup;
  public PXSetup<PX.Objects.GL.Company> Company;
  public PXFilter<APDocumentEnq.APDocumentFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoinOrderBy<APDocumentEnq.APDocumentResult, LeftJoin<APInvoice, On<APDocumentEnq.APDocumentResult.docType, Equal<APInvoice.docType>, And<APDocumentEnq.APDocumentResult.refNbr, Equal<APInvoice.refNbr>>>>, PX.Data.OrderBy<Desc<APDocumentEnq.APDocumentResult.docDate>>> Documents;
  public PXAction<APDocumentEnq.APDocumentFilter> refresh;
  public PXCancel<APDocumentEnq.APDocumentFilter> Cancel;
  [Obsolete("Will be removed in Acumatica 2019R1")]
  public PXAction<APDocumentEnq.APDocumentFilter> viewDocument;
  public PXAction<APDocumentEnq.APDocumentFilter> previousPeriod;
  public PXAction<APDocumentEnq.APDocumentFilter> nextPeriod;
  public PXAction<APDocumentEnq.APDocumentFilter> actionsFolder;
  public PXAction<APDocumentEnq.APDocumentFilter> createInvoice;
  public PXAction<APDocumentEnq.APDocumentFilter> createPayment;
  public PXAction<APDocumentEnq.APDocumentFilter> payDocument;
  public PXAction<APDocumentEnq.APDocumentFilter> reportsFolder;
  public PXAction<APDocumentEnq.APDocumentFilter> aPBalanceByVendorReport;
  public PXAction<APDocumentEnq.APDocumentFilter> vendorHistoryReport;
  public PXAction<APDocumentEnq.APDocumentFilter> aPAgedPastDueReport;
  public PXAction<APDocumentEnq.APDocumentFilter> aPAgedOutstandingReport;
  public PXAction<APDocumentEnq.APDocumentFilter> aPRegisterReport;
  public PXAction<APDocumentEnq.APDocumentFilter> viewOriginalDocument;

  protected virtual IEnumerable documents()
  {
    PXDelegateResult pxDelegateResult = this.SelectDetails();
    this.viewDocument.SetEnabled(pxDelegateResult.Count > 0);
    return (IEnumerable) pxDelegateResult;
  }

  protected virtual PXDelegateResult SelectDetails(bool summarize = false)
  {
    APDocumentEnq.APDocumentFilter current = this.Filter.Current;
    PXResultset<APDocumentEnq.APDocumentResult, APInvoice> pxResultset = new PXResultset<APDocumentEnq.APDocumentResult, APInvoice>();
    Dictionary<Tuple<string, string>, Decimal?> dictionary = (Dictionary<Tuple<string, string>, Decimal?>) null;
    List<System.Type> typeList;
    if (!summarize)
    {
      typeList = new List<System.Type>()
      {
        typeof (APDocumentEnq.APDocumentResult)
      };
    }
    else
    {
      typeList = new List<System.Type>();
      typeList.Add(typeof (APDocumentEnq.APDocumentResult.released));
      typeList.Add(typeof (APDocumentEnq.APDocumentResult.curyOrigDocAmt));
      typeList.Add(typeof (APDocumentEnq.APDocumentResult.origDocAmt));
      typeList.Add(typeof (APDocumentEnq.APDocumentResult.curyDocBal));
      typeList.Add(typeof (APDocumentEnq.APDocumentResult.docBal));
      typeList.Add(typeof (APDocumentEnq.APDocumentResult.curyRetainageUnreleasedAmt));
      typeList.Add(typeof (APDocumentEnq.APDocumentResult.retainageUnreleasedAmt));
    }
    List<System.Type> fieldsAndTables = typeList;
    FinPeriodIDAttribute.SetMasterPeriodID<Batch.finPeriodID>(this.Filter.Cache, (object) current);
    BqlCommand bqlCommand1 = new PXSelectReadonly<APDocumentEnq.APDocumentResult, Where<APDocumentEnq.APDocumentResult.vendorID, Equal<Current<APDocumentEnq.APDocumentFilter.vendorID>>>, PX.Data.OrderBy<Asc<APDocumentEnq.APDocumentResult.docType, Asc<APDocumentEnq.APDocumentResult.refNbr>>>>((PXGraph) this).View.BqlSelect;
    int? nullable1 = current.OrgBAccountID;
    if (nullable1.HasValue)
      bqlCommand1 = bqlCommand1.WhereAnd<Where<APDocumentEnq.APDocumentResult.branchID, Inside<Current<APDocumentEnq.APDocumentFilter.orgBAccountID>>>>();
    nullable1 = current.AccountID;
    if (nullable1.HasValue)
      bqlCommand1 = !PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vATRecognitionOnPrepaymentsAP>() ? bqlCommand1.WhereAnd<Where<APDocumentEnq.APDocumentResult.aPAccountID, Equal<Current<APDocumentEnq.APDocumentFilter.accountID>>>>() : bqlCommand1.WhereAnd<Where<APDocumentEnq.APDocumentResult.accountID, Equal<Current<APDocumentEnq.APDocumentFilter.accountID>>>>();
    bool? nullable2 = current.IncludeUnreleased;
    BqlCommand bqlCommand2 = nullable2.GetValueOrDefault() ? bqlCommand1.WhereAnd<Where<APDocumentEnq.APDocumentResult.scheduled, Equal<False>, PX.Data.And<Where<APDocumentEnq.APDocumentResult.voided, Equal<False>, Or<APDocumentEnq.APDocumentResult.released, Equal<True>>>>>>() : bqlCommand1.WhereAnd<PX.Data.Where<Where<APDocumentEnq.APDocumentResult.released, Equal<True>, Or<APDocumentEnq.APDocumentResult.prebooked, Equal<True>>>>>();
    nullable2 = current.ShowAllDocs;
    if (!nullable2.GetValueOrDefault())
      bqlCommand2 = bqlCommand2.WhereAnd<Where<APDocumentEnq.APDocumentResult.installmentCntr, PX.Data.IsNull>>();
    if (!SubCDUtils.IsSubCDEmpty(current.SubCD))
      bqlCommand2 = BqlCommand.AppendJoin<InnerJoin<PX.Objects.GL.Sub, On<PX.Objects.GL.Sub.subID, Equal<APDocumentEnq.APDocumentResult.aPSubID>>>>(bqlCommand2).WhereAnd<Where<PX.Objects.GL.Sub.subCD, Like<Current<APDocumentEnq.APDocumentFilter.subCDWildcard>>>>();
    if (current.DocType != null)
      bqlCommand2 = bqlCommand2.WhereAnd<Where<APDocumentEnq.APDocumentResult.docType, Equal<Current<APDocumentEnq.APDocumentFilter.docType>>>>();
    if (current.CuryID != null)
      bqlCommand2 = bqlCommand2.WhereAnd<Where<APDocumentEnq.APDocumentResult.curyID, Equal<Current<APDocumentEnq.APDocumentFilter.curyID>>>>();
    System.Type queryType = typeof (APDocumentEnq.APDocumentResult);
    bool flag1 = current.FinPeriodID != null;
    if (flag1)
    {
      nullable2 = current.UseMasterCalendar;
      BqlCommand bqlCommand3 = !nullable2.GetValueOrDefault() ? bqlCommand2.WhereAnd<Where<APDocumentEnq.APDocumentResult.finPeriodID, LessEqual<Current<APDocumentEnq.APDocumentFilter.finPeriodID>>>>().WhereAnd<Where<APDocumentEnq.APDocumentResult.finPostPeriodID, PX.Data.IsNull, Or<APDocumentEnq.APDocumentResult.finPostPeriodID, LessEqual<Current<APDocumentEnq.APDocumentFilter.finPeriodID>>>>>() : bqlCommand2.WhereAnd<Where<APDocumentEnq.APDocumentResult.tranPeriodID, LessEqual<Current<APDocumentEnq.APDocumentFilter.finPeriodID>>>>().WhereAnd<Where<APDocumentEnq.APDocumentResult.tranPostPeriodID, PX.Data.IsNull, Or<APDocumentEnq.APDocumentResult.tranPostPeriodID, LessEqual<Current<APDocumentEnq.APDocumentFilter.finPeriodID>>>>>();
      queryType = typeof (APDocumentEnq.APDocumentPeriodResult);
      System.Type aggregate = summarize ? typeof (PX.Data.Aggregate<GroupBy<APDocumentEnq.APDocumentPeriodResult.released, Sum<APDocumentEnq.APDocumentPeriodResult.curyDocBal, Sum<APDocumentEnq.APDocumentPeriodResult.docBal, Sum<APDocumentEnq.APDocumentPeriodResult.curyRetainageUnreleasedAmt, Sum<APDocumentEnq.APDocumentPeriodResult.retainageUnreleasedAmt>>>>>>) : typeof (PX.Data.Aggregate<GroupBy<APDocumentEnq.APDocumentPeriodResult.docType, GroupBy<APDocumentEnq.APDocumentPeriodResult.refNbr, GroupBy<APDocumentEnq.APDocumentPeriodResult.accountID, Sum<APDocumentEnq.APDocumentPeriodResult.curyBegBalance, Sum<APDocumentEnq.APDocumentPeriodResult.begBalance, Sum<APDocumentEnq.APDocumentPeriodResult.curyDocBal, Sum<APDocumentEnq.APDocumentPeriodResult.docBal, Sum<APDocumentEnq.APDocumentPeriodResult.curyRetainageUnreleasedAmt, Sum<APDocumentEnq.APDocumentPeriodResult.retainageUnreleasedAmt, Sum<APDocumentEnq.APDocumentPeriodResult.curyDiscActTaken, Sum<APDocumentEnq.APDocumentPeriodResult.discActTaken, Sum<APDocumentEnq.APDocumentPeriodResult.curyTaxWheld, Sum<APDocumentEnq.APDocumentPeriodResult.taxWheld, Sum<APDocumentEnq.APDocumentPeriodResult.rGOLAmt, Sum<APDocumentEnq.APDocumentPeriodResult.aPTurnover>>>>>>>>>>>>>>>, PX.Data.Having<BqlChainableConditionHavingBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<FunctionWrapper<Sum<APDocumentEnq.APDocumentPeriodResult.begBalance>>, NotEqual<FunctionWrapper<Zero>>>>>, PX.Data.Or<HavingConditionWrapper<BqlAggregatedOperand<Sum<APDocumentEnq.APDocumentPeriodResult.docBal>, IBqlDecimal>.IsNotEqual<Zero>>>>, PX.Data.Or<HavingConditionWrapper<BqlAggregatedOperand<Sum<APDocumentEnq.APDocumentPeriodResult.retainageUnreleasedAmt>, IBqlDecimal>.IsNotEqual<Zero>>>>, PX.Data.Or<HavingConditionWrapper<BqlAggregatedOperand<Sum<APDocumentEnq.APDocumentPeriodResult.turn>, IBqlDecimal>.IsNotEqual<Zero>>>>, PX.Data.Or<HavingConditionWrapper<BqlAggregatedOperand<Sum<APDocumentEnq.APDocumentPeriodResult.retainageUnreleasedAmt>, IBqlDecimal>.IsNotEqual<Zero>>>>>.Or<BqlAggregatedOperand<Max<APDocumentEnq.APDocumentPeriodResult.released>, IBqlBool>.IsEqual<False>>>>);
      nullable2 = current.IncludeGLTurnover;
      if (nullable2.GetValueOrDefault() && !summarize)
      {
        dictionary = this.SelectGLTurn();
        queryType = typeof (APDocumentEnq.GLDocumentPeriodResult);
        aggregate = typeof (PX.Data.Aggregate<GroupBy<APDocumentEnq.GLDocumentPeriodResult.docType, GroupBy<APDocumentEnq.GLDocumentPeriodResult.refNbr, Sum<APDocumentEnq.GLDocumentPeriodResult.aPTurnover>>>>);
      }
      fieldsAndTables = new List<System.Type>() { queryType };
      List<System.Type> types = new List<System.Type>((IEnumerable<System.Type>) BqlCommand.Decompose(bqlCommand3.GetSelectType()));
      this.ChangeQueryType(types, queryType);
      this.AddAggregate(types, aggregate);
      this.AdjustSelectAddAggregate(types);
      if (summarize)
      {
        this.RemoveOrderBy(types);
        this.AdjustSelectRemoveOrderby(types);
      }
      bqlCommand2 = BqlCommand.CreateInstance(BqlCommand.Compose(types.ToArray()));
    }
    else if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vATRecognitionOnPrepaymentsAP>())
    {
      queryType = typeof (APDocumentEnq.APDocumentPPIPeriodResult);
      nullable2 = current.ShowAllDocs;
      bool flag2 = false;
      if (nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue)
        bqlCommand2 = bqlCommand2.WhereAnd<Where<APDocumentEnq.APDocumentResult.openDoc, Equal<True>>>();
      System.Type aggregate = summarize ? typeof (PX.Data.Aggregate<GroupBy<APDocumentEnq.APDocumentPPIPeriodResult.released, Sum<APDocumentEnq.APDocumentPPIPeriodResult.curyDocBal, Sum<APDocumentEnq.APDocumentPPIPeriodResult.docBal, Sum<APDocumentEnq.APDocumentPPIPeriodResult.curyRetainageUnreleasedAmt, Sum<APDocumentEnq.APDocumentPPIPeriodResult.retainageUnreleasedAmt>>>>>>) : typeof (PX.Data.Aggregate<GroupBy<APDocumentEnq.APDocumentPPIPeriodResult.docType, GroupBy<APDocumentEnq.APDocumentPPIPeriodResult.refNbr, GroupBy<APDocumentEnq.APDocumentPPIPeriodResult.accountID, Sum<APDocumentEnq.APDocumentPPIPeriodResult.curyBegBalance, Sum<APDocumentEnq.APDocumentPPIPeriodResult.begBalance, Sum<APDocumentEnq.APDocumentPPIPeriodResult.curyDocBal, Sum<APDocumentEnq.APDocumentPPIPeriodResult.docBal, Sum<APDocumentEnq.APDocumentPPIPeriodResult.curyRetainageUnreleasedAmt, Sum<APDocumentEnq.APDocumentPPIPeriodResult.retainageUnreleasedAmt, Sum<APDocumentEnq.APDocumentPPIPeriodResult.curyDiscActTaken, Sum<APDocumentEnq.APDocumentPPIPeriodResult.discActTaken, Sum<APDocumentEnq.APDocumentPPIPeriodResult.curyTaxWheld, Sum<APDocumentEnq.APDocumentPPIPeriodResult.taxWheld, Sum<APDocumentEnq.APDocumentPPIPeriodResult.rGOLAmt, Sum<APDocumentEnq.APDocumentPPIPeriodResult.aPTurnover>>>>>>>>>>>>>>>>);
      fieldsAndTables = new List<System.Type>() { queryType };
      List<System.Type> types = new List<System.Type>((IEnumerable<System.Type>) BqlCommand.Decompose(bqlCommand2.GetSelectType()));
      this.ChangeQueryType(types, queryType);
      this.AddAggregate(types, aggregate);
      this.AdjustSelectAddAggregate(types);
      if (summarize)
      {
        this.RemoveOrderBy(types);
        this.AdjustSelectRemoveOrderby(types);
      }
      bqlCommand2 = BqlCommand.CreateInstance(BqlCommand.Compose(types.ToArray()));
    }
    else
    {
      nullable2 = current.ShowAllDocs;
      bool flag3 = false;
      if (nullable2.GetValueOrDefault() == flag3 & nullable2.HasValue)
        bqlCommand2 = bqlCommand2.WhereAnd<Where<APDocumentEnq.APDocumentResult.openDoc, Equal<True>>>();
      if (summarize)
      {
        List<System.Type> types = new List<System.Type>((IEnumerable<System.Type>) BqlCommand.Decompose(bqlCommand2.GetSelectType()));
        System.Type aggregate = typeof (PX.Data.Aggregate<GroupBy<APDocumentEnq.APDocumentResult.released, Sum<APDocumentEnq.APDocumentResult.curyOrigDocAmt, Sum<APDocumentEnq.APDocumentResult.origDocAmt, Sum<APDocumentEnq.APDocumentResult.curyDocBal, Sum<APDocumentEnq.APDocumentResult.docBal, Sum<APDocumentEnq.APDocumentResult.curyRetainageUnreleasedAmt, Sum<APDocumentEnq.APDocumentResult.retainageUnreleasedAmt>>>>>>>>);
        this.RemoveOrderBy(types);
        this.AddAggregate(types, aggregate);
        this.AdjustSelectRemoveOrderbyAddAggregate(types);
        bqlCommand2 = BqlCommand.CreateInstance(BqlCommand.Compose(types.ToArray()));
      }
    }
    PXResultMapper pxResultMapper = new PXResultMapper((PXGraph) this, new Dictionary<System.Type, System.Type>()
    {
      [typeof (APDocumentEnq.APDocumentResult)] = queryType
    }, new System.Type[1]
    {
      typeof (APDocumentEnq.APDocumentResult)
    });
    if (flag1)
      pxResultMapper.ExtFilters.Add<System.Type>((IEnumerable<System.Type>) new System.Type[11]
      {
        typeof (APDocumentEnq.APDocumentResult.curyBegBalance),
        typeof (APDocumentEnq.APDocumentResult.begBalance),
        typeof (APDocumentEnq.APDocumentResult.curyDocBal),
        typeof (APDocumentEnq.APDocumentResult.docBal),
        typeof (APDocumentEnq.APDocumentResult.curyRetainageUnreleasedAmt),
        typeof (APDocumentEnq.APDocumentResult.retainageUnreleasedAmt),
        typeof (APDocumentEnq.APDocumentResult.curyDiscActTaken),
        typeof (APDocumentEnq.APDocumentResult.discActTaken),
        typeof (APDocumentEnq.APDocumentResult.curyTaxWheld),
        typeof (APDocumentEnq.APDocumentResult.taxWheld),
        typeof (APDocumentEnq.APDocumentResult.rGOLAmt)
      });
    int startRow = PXView.StartRow;
    int totalRows = 0;
    PXDelegateResult delegateResult = pxResultMapper.CreateDelegateResult(!summarize && dictionary == null);
    PXView view = new PXView((PXGraph) this, true, bqlCommand2);
    using (new PXFieldScope(view, (IEnumerable<System.Type>) fieldsAndTables))
    {
      foreach (object obj in summarize || dictionary != null ? view.SelectMulti() : view.Select((object[]) null, (object[]) null, PXView.Searches, pxResultMapper.SortColumns, PXView.Descendings, (PXFilterRow[]) pxResultMapper.Filters, ref startRow, PXView.MaximumRows, ref totalRows))
      {
        object data = obj is PXResult pxResult ? pxResult[0] : obj;
        if (data is APDocumentEnq.APDocumentResult)
        {
          APDocumentEnq.APDocumentResult apDocumentResult = (APDocumentEnq.APDocumentResult) data;
          if (apDocumentResult != null)
          {
            nullable1 = apDocumentResult.AccountID;
            if (nullable1.HasValue)
            {
              nullable1 = apDocumentResult.SubID;
              if (nullable1.HasValue)
              {
                apDocumentResult.APAccountID = apDocumentResult.AccountID;
                apDocumentResult.APSubID = apDocumentResult.SubID;
              }
            }
          }
          delegateResult.Add(data);
        }
        else
        {
          APDocumentEnq.APDocumentResult instance = (APDocumentEnq.APDocumentResult) this.Documents.Cache.CreateInstance();
          foreach (string field in (List<string>) this.Documents.Cache.Fields)
            this.Documents.Cache.SetValue((object) instance, field, view.Cache.GetValue(data, field));
          nullable1 = instance.AccountID;
          if (nullable1.HasValue)
          {
            nullable1 = instance.SubID;
            if (nullable1.HasValue)
            {
              instance.APAccountID = instance.AccountID;
              instance.APSubID = instance.SubID;
            }
          }
          instance.GLTurnover = new Decimal?(0M);
          Decimal? nullable3;
          if (dictionary != null && dictionary.TryGetValue(new Tuple<string, string>(instance.DocType, instance.RefNbr), out nullable3))
            instance.GLTurnover = new Decimal?(nullable3.GetValueOrDefault());
          delegateResult.Add((object) instance);
        }
      }
    }
    return delegateResult;
  }

  protected virtual Dictionary<Tuple<string, string>, Decimal?> SelectGLTurn()
  {
    BqlCommand bqlSelect1 = new PXSelectGroupBy<APTranPost, Where<APTranPost.accountID, PX.Data.IsNotNull>, PX.Data.Aggregate<GroupBy<APTranPost.accountID>>>((PXGraph) this).View.BqlSelect;
    BqlCommand bqlSelect2 = new PXSelect<APDocumentEnq.GLTran, PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.GLTran.module, In3<BatchModule.moduleGL, BatchModule.moduleAP>>>>, PX.Data.And<BqlOperand<APDocumentEnq.GLTran.branchID, IBqlInt>.IsEqual<BqlField<APDocumentEnq.APDocumentFilter.branchID, IBqlInt>.FromCurrent>>>, PX.Data.And<BqlOperand<APDocumentEnq.GLTran.referenceID, IBqlInt>.IsEqual<BqlField<APDocumentEnq.APDocumentFilter.vendorID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<APDocumentEnq.GLTran.posted, IBqlBool>.IsEqual<True>>>>((PXGraph) this).View.BqlSelect;
    BqlCommand bqlCommand = !this.Filter.Current.UseMasterCalendar.GetValueOrDefault() ? bqlSelect2.WhereAnd<Where<APDocumentEnq.GLTran.finPeriodID, Equal<Current<APDocumentEnq.APDocumentFilter.finPeriodID>>>>() : bqlSelect2.WhereAnd<Where<APDocumentEnq.GLTran.tranPeriodID, Equal<Current<APDocumentEnq.APDocumentFilter.finPeriodID>>>>();
    BqlCommand select = (this.Filter.Current.AccountID.HasValue ? bqlCommand.WhereAnd<Where<APDocumentEnq.GLTran.accountID, Equal<Current<APDocumentEnq.APDocumentFilter.accountID>>>>() : bqlCommand.WhereAnd<PX.Data.Where<BqlOperand<APDocumentEnq.GLTran.accountID, IBqlInt>.IsInSubselect<FbqlSelect<SelectFromBase<APTranPost, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APTranPost.tranType, Equal<APDocumentEnq.GLTran.tranType>>>>>.And<BqlOperand<APTranPost.tranRefNbr, IBqlString>.IsEqual<APDocumentEnq.GLTran.refNbr>>>, APTranPost>.SearchFor<APTranPost.accountID>>>>()).AggregateNew<PX.Data.Aggregate<GroupBy<APDocumentEnq.GLTran.tranType, GroupBy<APDocumentEnq.GLTran.refNbr, Sum<APDocumentEnq.GLTran.gLTurnover>>>>>();
    List<System.Type> fieldsAndTables = new List<System.Type>()
    {
      typeof (APDocumentEnq.GLTran.tranType),
      typeof (APDocumentEnq.GLTran.refNbr),
      typeof (APDocumentEnq.GLTran.gLTurnover)
    };
    PXView view = new PXView((PXGraph) this, true, select);
    using (new PXFieldScope(view, (IEnumerable<System.Type>) fieldsAndTables))
      return view.SelectMulti().AsEnumerable<object>().RowCast<APDocumentEnq.GLTran>().ToDictionary<APDocumentEnq.GLTran, Tuple<string, string>, Decimal?>((Func<APDocumentEnq.GLTran, Tuple<string, string>>) (t => new Tuple<string, string>(t.TranType, t.RefNbr)), (Func<APDocumentEnq.GLTran, Decimal?>) (t => t.GLTurnover));
  }

  protected virtual IEnumerable filter()
  {
    APDocumentEnq apDocumentEnq = this;
    PXCache cache = apDocumentEnq.Caches[typeof (APDocumentEnq.APDocumentFilter)];
    if (cache.Current is APDocumentEnq.APDocumentFilter current1 && current1.RefreshTotals.GetValueOrDefault())
    {
      current1.ClearSummary();
      foreach (APDocumentEnq.APDocumentResult selectDetail in (List<object>) apDocumentEnq.SelectDetails(true))
        apDocumentEnq.Aggregate(current1, selectDetail);
      if (current1.VendorID.HasValue)
      {
        APVendorBalanceEnq instance = PXGraph.CreateInstance<APVendorBalanceEnq>();
        APVendorBalanceEnq.APHistoryFilter current = instance.Filter.Current;
        APVendorBalanceEnq.Copy(current, current1);
        if (current.FinPeriodID == null)
          current.FinPeriodID = instance.GetLastActivityPeriod(current1.VendorID);
        instance.Filter.Update(current);
        APVendorBalanceEnq.APHistorySummary aSrc = (APVendorBalanceEnq.APHistorySummary) instance.Summary.Select();
        apDocumentEnq.SetSummary(current1, aSrc);
      }
      current1.RefreshTotals = new bool?(false);
    }
    yield return cache.Current;
    cache.IsDirty = false;
  }

  protected virtual void AdjustSelectRemoveOrderbyAddAggregate(List<System.Type> types)
  {
    if (types[0] == typeof (PX.Data.Select<,,>))
      types[0] = typeof (Select4<,,>);
    if (!(types[0] == typeof (Select2<,,,>)))
      return;
    types[0] = typeof (Select5<,,,>);
  }

  protected virtual void AdjustSelectRemoveOrderby(List<System.Type> types)
  {
    if (types[0] == typeof (Select4<,,,>))
      types[0] = typeof (Select4<,,>);
    if (!(types[0] == typeof (Select5<,,,,>)))
      return;
    types[0] = typeof (Select5<,,,>);
  }

  protected virtual void AdjustSelectAddAggregate(List<System.Type> types)
  {
    if (types[0] == typeof (PX.Data.Select<,,>))
      types[0] = typeof (Select4<,,,>);
    if (!(types[0] == typeof (Select2<,,,>)))
      return;
    types[0] = typeof (Select5<,,,,>);
  }

  protected virtual void AddAggregate(List<System.Type> types, System.Type aggregate)
  {
    int index = types.IndexOf(typeof (PX.Data.OrderBy<>));
    if (index != -1)
      types.Insert(index, aggregate);
    else
      types.Add(aggregate);
  }

  protected virtual void RemoveOrderBy(List<System.Type> types)
  {
    int index = types.IndexOf(typeof (PX.Data.OrderBy<>));
    types.RemoveRange(index, types.Count - index);
  }

  protected virtual void ChangeQueryType(List<System.Type> types, System.Type queryType)
  {
    for (int index = 0; index < types.Count; ++index)
    {
      System.Type type = types[index];
      if (type == typeof (APDocumentEnq.APDocumentResult))
        types[index] = queryType;
      else if (type.DeclaringType == typeof (APDocumentEnq.APDocumentResult))
      {
        System.Type nestedType = queryType.GetNestedType(type.Name);
        types[index] = nestedType;
      }
    }
  }

  public APDocumentEnq()
  {
    PX.Objects.AP.APSetup current1 = this.APSetup.Current;
    PX.Objects.GL.Company current2 = this.Company.Current;
    this.Documents.Cache.AllowDelete = false;
    this.Documents.Cache.AllowInsert = false;
    this.Documents.Cache.AllowUpdate = false;
    PXUIFieldAttribute.SetVisibility<APDocumentEnq.APRegister.finPeriodID>(this.Documents.Cache, (object) null, PXUIVisibility.Visible);
    PXUIFieldAttribute.SetVisibility<APDocumentEnq.APRegister.vendorID>(this.Documents.Cache, (object) null, PXUIVisibility.Visible);
    PXUIFieldAttribute.SetVisibility<APDocumentEnq.APRegister.curyDiscBal>(this.Documents.Cache, (object) null, PXUIVisibility.Visible);
    PXUIFieldAttribute.SetVisibility<APDocumentEnq.APRegister.curyOrigDocAmt>(this.Documents.Cache, (object) null, PXUIVisibility.Visible);
    PXUIFieldAttribute.SetVisibility<APDocumentEnq.APRegister.curyDiscTaken>(this.Documents.Cache, (object) null, PXUIVisibility.Visible);
    this.actionsFolder.MenuAutoOpen = true;
    this.actionsFolder.AddMenuAction((PXAction) this.createInvoice);
    this.actionsFolder.AddMenuAction((PXAction) this.createPayment);
    this.actionsFolder.AddMenuAction((PXAction) this.payDocument);
    this.reportsFolder.MenuAutoOpen = true;
    this.reportsFolder.AddMenuAction((PXAction) this.aPBalanceByVendorReport);
    this.reportsFolder.AddMenuAction((PXAction) this.vendorHistoryReport);
    this.reportsFolder.AddMenuAction((PXAction) this.aPAgedPastDueReport);
    this.reportsFolder.AddMenuAction((PXAction) this.aPAgedOutstandingReport);
    this.reportsFolder.AddMenuAction((PXAction) this.aPRegisterReport);
  }

  public override bool IsDirty => false;

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Original Document")]
  protected virtual void APDocumentResult_OrigRefNbr_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = true)]
  [PXButton(ImageKey = "Refresh", IsLockedOnToolbar = true)]
  public IEnumerable Refresh(PXAdapter adapter)
  {
    this.Filter.Current.RefreshTotals = new bool?(true);
    return adapter.Get();
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = false)]
  [PXEditDetailButton(ImageKey = "DataEntry")]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (this.Documents.Current != null)
      PXRedirectHelper.TryRedirect(this.Documents.Cache, (object) this.Documents.Current, "Document", PXRedirectHelper.WindowMode.NewWindow);
    return (IEnumerable) this.Filter.Select();
  }

  [PXButton]
  public virtual IEnumerable ViewOriginalDocument(PXAdapter adapter)
  {
    if (this.Documents.Current != null)
    {
      APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
      PX.Objects.AP.APRegister row = (PX.Objects.AP.APRegister) PXSelectBase<PX.Objects.AP.APRegister, PXSelect<PX.Objects.AP.APRegister, Where<PX.Objects.AP.APRegister.refNbr, Equal<Required<PX.Objects.AP.APRegister.origRefNbr>>, And<PX.Objects.AP.APRegister.docType, Equal<Required<PX.Objects.AP.APRegister.origDocType>>>>>.Config>.SelectSingleBound((PXGraph) instance, (object[]) null, (object) this.Documents.Current.OrigRefNbr, (object) this.Documents.Current.OrigDocType);
      if (row != null)
        PXRedirectHelper.TryRedirect(instance.Document.Cache, (object) row, "Document", PXRedirectHelper.WindowMode.NewWindow);
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXPreviousButton]
  public virtual IEnumerable PreviousPeriod(PXAdapter adapter)
  {
    APDocumentEnq.APDocumentFilter current = this.Filter.Current;
    FinPeriod prevPeriod = this.FinPeriodRepository.FindPrevPeriod(this.FinPeriodRepository.GetCalendarOrganizationID(current.OrganizationID, current.BranchID, current.UseMasterCalendar), current.FinPeriodID, true);
    current.FinPeriodID = prevPeriod?.FinPeriodID;
    current.RefreshTotals = new bool?(true);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXNextButton]
  public virtual IEnumerable NextPeriod(PXAdapter adapter)
  {
    APDocumentEnq.APDocumentFilter current = this.Filter.Current;
    FinPeriod nextPeriod = this.FinPeriodRepository.FindNextPeriod(this.FinPeriodRepository.GetCalendarOrganizationID(current.OrganizationID, current.BranchID, current.UseMasterCalendar), current.FinPeriodID, true);
    current.FinPeriodID = nextPeriod?.FinPeriodID;
    current.RefreshTotals = new bool?(true);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Actions", MapEnableRights = PXCacheRights.Select)]
  [PXButton(SpecialType = PXSpecialButtonType.ActionsFolder)]
  protected virtual IEnumerable Actionsfolder(PXAdapter adapter) => adapter.Get();

  [PXUIField(DisplayName = "Create Bill", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton(Category = "Document Processing")]
  public virtual IEnumerable CreateInvoice(PXAdapter adapter)
  {
    APDocumentEnq.APDocumentFilter current = this.Filter.Current;
    if ((current != null ? (current.VendorID.HasValue ? 1 : 0) : 0) != 0)
    {
      VendorMaint instance = PXGraph.CreateInstance<VendorMaint>();
      instance.BAccount.Current = VendorR.PK.Find((PXGraph) instance, this.Filter.Current.VendorID);
      instance.newBillAdjustment.Press();
    }
    return (IEnumerable) this.Filter.Select();
  }

  [PXUIField(DisplayName = "Create Payment", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton(Category = "Document Processing")]
  public virtual IEnumerable CreatePayment(PXAdapter adapter)
  {
    APDocumentEnq.APDocumentFilter current = this.Filter.Current;
    if ((current != null ? (current.VendorID.HasValue ? 1 : 0) : 0) != 0)
    {
      VendorMaint instance = PXGraph.CreateInstance<VendorMaint>();
      instance.BAccount.Current = VendorR.PK.Find((PXGraph) instance, this.Filter.Current.VendorID);
      instance.newManualCheck.Press();
    }
    return (IEnumerable) this.Filter.Select();
  }

  [PXUIField(DisplayName = "Pay/Apply Document", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton(Category = "Document Processing")]
  public virtual IEnumerable PayDocument(PXAdapter adapter)
  {
    if (this.Documents.Current != null)
    {
      if (this.Documents.Current.Status != "N" && this.Documents.Current.Status != "U")
        throw new PXException("Only open documents can be selected for payment.");
      APInvoice invoice = this.FindInvoice(this.Documents.Current);
      if (invoice != null)
      {
        if (APDocType.Payable(this.Documents.Current.DocType).GetValueOrDefault())
        {
          APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
          instance.Clear();
          try
          {
            instance.CreatePayment(invoice);
          }
          catch (AdjustedNotFoundException ex)
          {
            APAdjust apAdjust = (APAdjust) PXSelectBase<APAdjust, PXSelect<APAdjust, Where<APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>, And<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>, And<APAdjust.released, Equal<False>>>>>.Config>.Select((PXGraph) instance, (object) invoice.DocType, (object) invoice.RefNbr);
            if (apAdjust != null)
            {
              instance.Clear();
              instance.Document.Current = (APPayment) instance.Document.Search<APPayment.refNbr>((object) apAdjust.AdjgRefNbr, (object) apAdjust.AdjgDocType);
              throw new PXRedirectRequiredException((PXGraph) instance, "PayInvoice");
            }
          }
          throw new PXRedirectRequiredException((PXGraph) instance, "PayInvoice");
        }
        APPaymentEntry instance1 = PXGraph.CreateInstance<APPaymentEntry>();
        APPayment apPayment = (APPayment) instance1.Document.Search<APPayment.refNbr>((object) this.Documents.Current.RefNbr, (object) this.Documents.Current.DocType);
        if (apPayment != null)
        {
          instance1.Document.Current = apPayment;
          throw new PXRedirectRequiredException((PXGraph) instance1, "View Document");
        }
      }
    }
    return (IEnumerable) this.Filter.Select();
  }

  [PXUIField(DisplayName = "Reports", MapEnableRights = PXCacheRights.Select)]
  [PXButton(SpecialType = PXSpecialButtonType.ReportsFolder)]
  protected virtual IEnumerable Reportsfolder(PXAdapter adapter) => adapter.Get();

  [PXUIField(DisplayName = "AP Balance by Vendor", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  public virtual IEnumerable APBalanceByVendorReport(PXAdapter adapter)
  {
    APDocumentEnq.APDocumentFilter current = this.Filter.Current;
    if (current != null)
    {
      Dictionary<string, string> reportParameters = this.GetBasicReportParameters(current);
      if (!string.IsNullOrEmpty(current.FinPeriodID))
        reportParameters["PeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(current.FinPeriodID);
      reportParameters["UseMasterCalendar"] = current.UseMasterCalendar.GetValueOrDefault() ? true.ToString() : false.ToString();
      throw new PXReportRequiredException(reportParameters, "AP632500", "AP Balance by Vendor");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Vendor History", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  public virtual IEnumerable VendorHistoryReport(PXAdapter adapter)
  {
    APDocumentEnq.APDocumentFilter current = this.Filter.Current;
    if (current != null)
    {
      Dictionary<string, string> reportParameters = this.GetBasicReportParameters(current);
      if (!string.IsNullOrEmpty(current.FinPeriodID))
      {
        reportParameters["FromPeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(current.FinPeriodID);
        reportParameters["ToPeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(current.FinPeriodID);
      }
      throw new PXReportRequiredException(reportParameters, "AP652000", "Vendor History");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "AP Aging", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  public virtual IEnumerable APAgedPastDueReport(PXAdapter adapter)
  {
    APDocumentEnq.APDocumentFilter current = this.Filter.Current;
    if (current != null)
      throw new PXReportRequiredException(this.GetBasicReportParameters(current), "AP631000", "AP Aging");
    return adapter.Get();
  }

  [PXUIField(DisplayName = "AP Coming Due", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  public virtual IEnumerable APAgedOutstandingReport(PXAdapter adapter)
  {
    APDocumentEnq.APDocumentFilter current = this.Filter.Current;
    if (current != null)
      throw new PXReportRequiredException(this.GetBasicReportParameters(current), "AP631500", "AP Coming Due");
    return adapter.Get();
  }

  [PXUIField(DisplayName = "AP Register", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  public virtual IEnumerable APRegisterReport(PXAdapter adapter)
  {
    APDocumentEnq.APDocumentFilter current = this.Filter.Current;
    if (current != null)
    {
      Dictionary<string, string> reportParameters = this.GetBasicReportParameters(current);
      if (!string.IsNullOrEmpty(current.FinPeriodID))
      {
        reportParameters["StartPeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(current.FinPeriodID);
        reportParameters["EndPeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(current.FinPeriodID);
      }
      throw new PXReportRequiredException(reportParameters, "AP621500", "AP Register");
    }
    return adapter.Get();
  }

  private Dictionary<string, string> GetBasicReportParameters(APDocumentEnq.APDocumentFilter filter)
  {
    BAccountR baccountR = (BAccountR) PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, (object) filter.OrgBAccountID);
    return new Dictionary<string, string>()
    {
      {
        "VendorID",
        VendorMaint.FindByID((PXGraph) this, filter.VendorID)?.AcctCD
      },
      {
        "OrgBAccountID",
        baccountR?.AcctCD
      }
    };
  }

  public virtual void APDocumentFilter_AccountID_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    if (!(e.Row is APDocumentEnq.APDocumentFilter row))
      return;
    e.Cancel = true;
    int? nullable = new int?();
    row.AccountID = nullable;
  }

  public virtual void APDocumentFilter_CuryID_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    if (!(e.Row is APDocumentEnq.APDocumentFilter row))
      return;
    e.Cancel = true;
    row.CuryID = (string) null;
  }

  public virtual void APDocumentFilter_SubID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  public virtual void APDocumentFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (cache.ObjectsEqual<APDocumentEnq.APDocumentFilter.organizationID>(e.Row, e.OldRow) && cache.ObjectsEqual<APDocumentEnq.APDocumentFilter.orgBAccountID>(e.Row, e.OldRow) && cache.ObjectsEqual<APDocumentEnq.APDocumentFilter.branchID>(e.Row, e.OldRow) && cache.ObjectsEqual<APDocumentEnq.APDocumentFilter.vendorID>(e.Row, e.OldRow) && cache.ObjectsEqual<APDocumentEnq.APDocumentFilter.finPeriodID>(e.Row, e.OldRow) && cache.ObjectsEqual<APDocumentEnq.APDocumentFilter.masterFinPeriodID>(e.Row, e.OldRow) && cache.ObjectsEqual<APDocumentEnq.APDocumentFilter.showAllDocs>(e.Row, e.OldRow) && cache.ObjectsEqual<APDocumentEnq.APDocumentFilter.includeUnreleased>(e.Row, e.OldRow) && cache.ObjectsEqual<APDocumentEnq.APDocumentFilter.accountID>(e.Row, e.OldRow) && cache.ObjectsEqual<APDocumentEnq.APDocumentFilter.subCD>(e.Row, e.OldRow) && cache.ObjectsEqual<APDocumentEnq.APDocumentFilter.subCDWildcard>(e.Row, e.OldRow) && cache.ObjectsEqual<APDocumentEnq.APDocumentFilter.docType>(e.Row, e.OldRow) && cache.ObjectsEqual<APDocumentEnq.APDocumentFilter.curyID>(e.Row, e.OldRow))
      return;
    (e.Row as APDocumentEnq.APDocumentFilter).RefreshTotals = new bool?(true);
  }

  public virtual void APDocumentFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    APDocumentEnq.APDocumentFilter row = (APDocumentEnq.APDocumentFilter) e.Row;
    bool isVisible1 = row.FinPeriodID != null;
    PXCache cache1 = this.Documents.Cache;
    bool isVisible2 = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>();
    bool flag1 = !string.IsNullOrEmpty(row.CuryID) && row.CuryID != this.Company.Current.BaseCuryID;
    bool flag2 = !string.IsNullOrEmpty(row.CuryID) && row.CuryID == this.Company.Current.BaseCuryID;
    PXUIFieldAttribute.SetVisible<APDocumentEnq.APDocumentFilter.showAllDocs>(cache, (object) row, !isVisible1);
    PXUIFieldAttribute.SetVisible<APDocumentEnq.APDocumentFilter.curyID>(cache, (object) row, isVisible2);
    PXUIFieldAttribute.SetVisible<APDocumentEnq.APDocumentFilter.curyBalanceSummary>(cache, (object) row, isVisible2 & flag1);
    PXUIFieldAttribute.SetVisible<APDocumentEnq.APDocumentFilter.curyDifference>(cache, (object) row, isVisible2 & flag1);
    PXUIFieldAttribute.SetVisible<APDocumentEnq.APDocumentFilter.curyVendorBalance>(cache, (object) row, isVisible2 & flag1);
    PXUIFieldAttribute.SetVisible<APDocumentEnq.APDocumentFilter.curyVendorRetainedBalance>(cache, (object) row, isVisible2 & flag1);
    PXUIFieldAttribute.SetVisible<APDocumentEnq.APDocumentFilter.curyVendorDepositsBalance>(cache, (object) row, isVisible2 & flag1);
    PXUIFieldAttribute.SetVisible<APDocumentEnq.APDocumentResult.curyID>(cache1, (object) null, isVisible2);
    PXUIFieldAttribute.SetVisible<APDocumentEnq.APDocumentResult.rGOLAmt>(cache1, (object) null, isVisible2 && !flag2);
    PXUIFieldAttribute.SetVisible<APDocumentEnq.APDocumentResult.curyBegBalance>(cache1, (object) null, isVisible1 & isVisible2 && !flag2);
    PXUIFieldAttribute.SetVisible<APDocumentEnq.APDocumentResult.begBalance>(cache1, (object) null, isVisible1);
    PXUIFieldAttribute.SetVisible<APDocumentEnq.APDocumentResult.curyOrigDocAmt>(cache1, (object) null, isVisible2 && !flag2);
    PXUIFieldAttribute.SetVisible<APDocumentEnq.APDocumentResult.curyDocBal>(cache1, (object) null, isVisible2 && !flag2);
    PXUIFieldAttribute.SetVisible<APDocumentEnq.APDocumentResult.curyDiscActTaken>(cache1, (object) null, isVisible2 && !flag2);
    PXUIFieldAttribute.SetVisible<APDocumentEnq.APDocumentResult.curyTaxWheld>(cache1, (object) null, isVisible2 && !flag2);
    PXUIFieldAttribute.SetVisible<APDocumentEnq.APDocumentResult.curyRetainageTotal>(cache1, (object) null, isVisible2 && !flag2);
    PXUIFieldAttribute.SetVisible<APDocumentEnq.APDocumentResult.curyOrigDocAmtWithRetainageTotal>(cache1, (object) null, isVisible2 && !flag2);
    PXUIFieldAttribute.SetVisible<APDocumentEnq.APDocumentResult.curyRetainageUnreleasedAmt>(cache1, (object) null, isVisible2 && !flag2);
    bool required = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleBaseCurrencies>();
    PXUIFieldAttribute.SetRequired<APDocumentEnq.APDocumentFilter.orgBAccountID>(cache, required);
    int num;
    if (!required)
    {
      num = row.VendorID.HasValue ? 1 : 0;
    }
    else
    {
      int? nullable = row.VendorID;
      if (nullable.HasValue)
      {
        nullable = row.OrgBAccountID;
        num = nullable.HasValue ? 1 : 0;
      }
      else
        num = 0;
    }
    bool isEnabled = num != 0;
    this.aPBalanceByVendorReport.SetEnabled(isEnabled);
    this.vendorHistoryReport.SetEnabled(isEnabled);
    this.aPAgedPastDueReport.SetEnabled(isEnabled);
    this.aPAgedOutstandingReport.SetEnabled(isEnabled);
    this.aPRegisterReport.SetEnabled(isEnabled);
  }

  protected virtual void SetSummary(
    APDocumentEnq.APDocumentFilter aDest,
    APVendorBalanceEnq.APHistorySummary aSrc)
  {
    aDest.VendorBalance = aSrc.BalanceSummary;
    aDest.VendorDepositsBalance = aSrc.DepositsSummary;
    aDest.CuryVendorBalance = aSrc.CuryBalanceSummary;
    aDest.CuryVendorDepositsBalance = aSrc.CuryDepositsSummary;
  }

  protected virtual void Aggregate(
    APDocumentEnq.APDocumentFilter aDest,
    APDocumentEnq.APDocumentResult aSrc)
  {
    APDocumentEnq.APDocumentFilter apDocumentFilter1 = aDest;
    Decimal? nullable = apDocumentFilter1.BalanceSummary;
    Decimal valueOrDefault1 = aSrc.DocBal.GetValueOrDefault();
    apDocumentFilter1.BalanceSummary = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
    APDocumentEnq.APDocumentFilter apDocumentFilter2 = aDest;
    nullable = apDocumentFilter2.CuryBalanceSummary;
    Decimal valueOrDefault2 = aSrc.CuryDocBal.GetValueOrDefault();
    apDocumentFilter2.CuryBalanceSummary = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
    APDocumentEnq.APDocumentFilter apDocumentFilter3 = aDest;
    nullable = apDocumentFilter3.VendorRetainedBalance;
    Decimal valueOrDefault3 = aSrc.RetainageUnreleasedAmt.GetValueOrDefault();
    apDocumentFilter3.VendorRetainedBalance = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault3) : new Decimal?();
    APDocumentEnq.APDocumentFilter apDocumentFilter4 = aDest;
    nullable = apDocumentFilter4.CuryVendorRetainedBalance;
    Decimal valueOrDefault4 = aSrc.CuryRetainageUnreleasedAmt.GetValueOrDefault();
    apDocumentFilter4.CuryVendorRetainedBalance = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault4) : new Decimal?();
  }

  protected virtual APInvoice FindInvoice(APDocumentEnq.APDocumentResult aRes)
  {
    return (APInvoice) PXSelectBase<APInvoice, PXSelect<APInvoice, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>.Config>.Select((PXGraph) this, (object) aRes.DocType, (object) aRes.RefNbr);
  }

  protected virtual APPayment FindPayment(APDocumentEnq.APDocumentResult aRes)
  {
    return (APPayment) PXSelectBase<APPayment, PXSelect<APPayment, Where<APPayment.docType, Equal<Required<APPayment.docType>>, And<APPayment.refNbr, Equal<Required<APPayment.refNbr>>>>>.Config>.Select((PXGraph) this, (object) aRes.DocType, (object) aRes.RefNbr);
  }

  [Serializable]
  public class APDocumentFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [Organization(false, Required = false)]
    public int? OrganizationID { get; set; }

    [BranchOfOrganization(typeof (APDocumentEnq.APDocumentFilter.organizationID), false, null, null)]
    public int? BranchID { get; set; }

    [OrganizationTree(typeof (APDocumentEnq.APDocumentFilter.organizationID), typeof (APDocumentEnq.APDocumentFilter.branchID), null, false)]
    public int? OrgBAccountID { get; set; }

    [Vendor(DescriptionField = typeof (Vendor.acctName))]
    [PXDefault]
    public virtual int? VendorID { get; set; }

    [Account(null, typeof (Search5<PX.Objects.GL.Account.accountID, InnerJoin<APHistory, On<PX.Objects.GL.Account.accountID, Equal<APHistory.accountID>>>, PX.Data.Where<Match<Current<AccessInfo.userName>>>, PX.Data.Aggregate<GroupBy<PX.Objects.GL.Account.accountID>>>), DisplayName = "AP Account", DescriptionField = typeof (PX.Objects.GL.Account.description))]
    public virtual int? AccountID { get; set; }

    [PXDBString(30, IsUnicode = true)]
    [PXUIField(DisplayName = "AP Subaccount", Visibility = PXUIVisibility.Invisible, FieldClass = "SUBACCOUNT")]
    [PXDimension("SUBACCOUNT", ValidComboRequired = false)]
    public virtual string SubCD { get; set; }

    [PXBool]
    [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
    [PXUIField(DisplayName = "Use Master Calendar")]
    [PXUIVisible(typeof (FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleCalendarsSupport>))]
    public bool? UseMasterCalendar { get; set; }

    [AnyPeriodFilterable(null, null, typeof (APDocumentEnq.APDocumentFilter.branchID), null, typeof (APDocumentEnq.APDocumentFilter.organizationID), typeof (APDocumentEnq.APDocumentFilter.useMasterCalendar), null, false, null, null)]
    [PXUIField(DisplayName = "Period", Visibility = PXUIVisibility.Visible, Required = false)]
    public virtual string FinPeriodID { get; set; }

    [Obsolete("This is an absolete field. It will be removed in 2019R2")]
    [PeriodID(null, null, null, true)]
    public virtual string MasterFinPeriodID { get; set; }

    [PXDBString(3, IsFixed = true)]
    [PXDefault]
    [APDocType.List]
    [PXUIField(DisplayName = "Type")]
    public virtual string DocType { get; set; }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Show All Documents")]
    public virtual bool? ShowAllDocs { get; set; }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Include Unreleased Documents")]
    public virtual bool? IncludeUnreleased { get; set; }

    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID), CacheGlobal = true)]
    [PXUIField(DisplayName = "Currency")]
    public virtual string CuryID { get; set; }

    [PXDBString(30, IsUnicode = true)]
    public virtual string SubCDWildcard => SubCDUtils.CreateSubCDWildcard(this.SubCD, "SUBACCOUNT");

    [PXDBBool]
    [PXDefault(true)]
    public bool? RefreshTotals { get; set; }

    [CurySymbol(null, null, typeof (APDocumentEnq.APDocumentFilter.curyID), null, null, null, "Balance by Documents", true, false)]
    [PXCury(typeof (APDocumentEnq.APDocumentFilter.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Balance by Documents (Currency)", Enabled = false)]
    public virtual Decimal? CuryBalanceSummary { get; set; }

    [CurySymbol(null, null, null, null, null, typeof (APDocumentEnq.APDocumentFilter.vendorID), null, false, false)]
    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Balance by Documents", Enabled = false)]
    public virtual Decimal? BalanceSummary { get; set; }

    [CurySymbol(null, null, typeof (APDocumentEnq.APDocumentFilter.curyID), null, null, null, "Current Balance", true, false)]
    [PXCury(typeof (APDocumentEnq.APDocumentFilter.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Current Balance (Currency)", Enabled = false)]
    public virtual Decimal? CuryVendorBalance { get; set; }

    [CurySymbol(null, null, null, null, null, typeof (APDocumentEnq.APDocumentFilter.vendorID), null, false, false)]
    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Current Balance", Enabled = false)]
    public virtual Decimal? VendorBalance { get; set; }

    [CurySymbol(null, null, typeof (APDocumentEnq.APDocumentFilter.curyID), null, null, null, "Retained Balance", true, false)]
    [PXCury(typeof (APDocumentEnq.APDocumentFilter.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Retained Balance (Currency)", Enabled = false, FieldClass = "Retainage")]
    public virtual Decimal? CuryVendorRetainedBalance { get; set; }

    [CurySymbol(null, null, null, null, null, typeof (APDocumentEnq.APDocumentFilter.vendorID), null, false, false)]
    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Retained Balance", Enabled = false, FieldClass = "Retainage")]
    public virtual Decimal? VendorRetainedBalance { get; set; }

    [CurySymbol(null, null, typeof (APDocumentEnq.APDocumentFilter.curyID), null, null, null, "Prepayment Balance", true, false)]
    [PXCury(typeof (APDocumentEnq.APDocumentFilter.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Prepayments Balance (Currency)", Enabled = false)]
    public virtual Decimal? CuryVendorDepositsBalance { get; set; }

    [CurySymbol(null, null, null, null, null, typeof (APDocumentEnq.APDocumentFilter.vendorID), null, false, false)]
    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Prepayment Balance", Enabled = false)]
    public virtual Decimal? VendorDepositsBalance { get; set; }

    [CurySymbol(null, null, typeof (APDocumentEnq.APDocumentFilter.curyID), null, null, null, "Balance Discrepancy", true, false)]
    [PXCury(typeof (APDocumentEnq.APDocumentFilter.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Balance Discrepancy (Currency)", Enabled = false)]
    public virtual Decimal? CuryDifference
    {
      get
      {
        Decimal? curyBalanceSummary = this.CuryBalanceSummary;
        Decimal? curyVendorBalance = this.CuryVendorBalance;
        Decimal? curyDifference = this.CuryVendorDepositsBalance;
        Decimal? nullable = curyVendorBalance.HasValue & curyDifference.HasValue ? new Decimal?(curyVendorBalance.GetValueOrDefault() + curyDifference.GetValueOrDefault()) : new Decimal?();
        if (curyBalanceSummary.HasValue & nullable.HasValue)
          return new Decimal?(curyBalanceSummary.GetValueOrDefault() - nullable.GetValueOrDefault());
        curyDifference = new Decimal?();
        return curyDifference;
      }
    }

    [CurySymbol(null, null, null, null, null, typeof (APDocumentEnq.APDocumentFilter.vendorID), null, false, false)]
    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Balance Discrepancy", Enabled = false)]
    public virtual Decimal? Difference
    {
      get
      {
        Decimal? balanceSummary = this.BalanceSummary;
        Decimal? vendorBalance = this.VendorBalance;
        Decimal? difference = this.VendorDepositsBalance;
        Decimal? nullable = vendorBalance.HasValue & difference.HasValue ? new Decimal?(vendorBalance.GetValueOrDefault() + difference.GetValueOrDefault()) : new Decimal?();
        if (balanceSummary.HasValue & nullable.HasValue)
          return new Decimal?(balanceSummary.GetValueOrDefault() - nullable.GetValueOrDefault());
        difference = new Decimal?();
        return difference;
      }
    }

    [PXDBBool]
    [PXDefault(false)]
    public virtual bool? IncludeGLTurnover { get; set; }

    public virtual void ClearSummary()
    {
      this.VendorBalance = new Decimal?(0M);
      this.VendorDepositsBalance = new Decimal?(0M);
      this.BalanceSummary = new Decimal?(0M);
      this.CuryVendorBalance = new Decimal?(0M);
      this.CuryVendorDepositsBalance = new Decimal?(0M);
      this.CuryBalanceSummary = new Decimal?(0M);
      this.CuryVendorRetainedBalance = new Decimal?(0M);
      this.VendorRetainedBalance = new Decimal?(0M);
    }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.organizationID>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.branchID>
    {
    }

    public abstract class orgBAccountID : IBqlField, IBqlOperand
    {
    }

    public abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.vendorID>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.accountID>
    {
    }

    public abstract class subCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.subCD>
    {
    }

    public abstract class useMasterCalendar : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.useMasterCalendar>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.finPeriodID>
    {
    }

    public abstract class masterFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.masterFinPeriodID>
    {
    }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.docType>
    {
    }

    public abstract class showAllDocs : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.showAllDocs>
    {
    }

    public abstract class includeUnreleased : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.includeUnreleased>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.curyID>
    {
    }

    public abstract class subCDWildcard : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.subCDWildcard>
    {
    }

    public abstract class refreshTotals : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.refreshTotals>
    {
    }

    public abstract class curyBalanceSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.curyBalanceSummary>
    {
    }

    public abstract class balanceSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.balanceSummary>
    {
    }

    public abstract class curyVendorBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.curyVendorBalance>
    {
    }

    public abstract class vendorBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.vendorBalance>
    {
    }

    public abstract class curyVendorRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.curyVendorRetainedBalance>
    {
    }

    public abstract class vendorRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.vendorRetainedBalance>
    {
    }

    public abstract class curyVendorDepositsBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.curyVendorDepositsBalance>
    {
    }

    public abstract class vendorDepositsBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.vendorDepositsBalance>
    {
    }

    public abstract class curyDifference : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.curyDifference>
    {
    }

    public abstract class difference : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.difference>
    {
    }

    public abstract class includeGLTurnover : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentFilter.includeGLTurnover>
    {
    }
  }

  [PXPrimaryGraph(typeof (APDocumentEnq), Filter = typeof (APDocumentEnq.APDocumentFilter))]
  [PXCacheName("AP History for Report")]
  [Serializable]
  public class APHistoryForReport : APHistory
  {
  }

  [PXHidden]
  public class APRegister : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected Decimal? _CuryDocBal;
    protected Decimal? _CuryWhTaxBal;
    protected Decimal? _WhTaxBal;
    protected string _DocDesc;

    [PXDBString(3, IsKey = true, IsFixed = true)]
    [PXDefault]
    [APDocType.List]
    [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = true, TabOrder = 0)]
    public virtual string DocType { get; set; }

    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
    [PXDefault]
    [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 1)]
    [PXSelector(typeof (Search<APDocumentEnq.APRegister.refNbr>), Filterable = true)]
    public virtual string RefNbr { get; set; }

    /// <summary>
    /// Code of the <see cref="T:PX.Objects.CM.Currency">Currency</see> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">company's base currency</see>.
    /// </value>
    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
    [PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible)]
    [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string CuryID { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.GL.Branch">Branch</see>, to which the document belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID">Branch.BranchID</see> field.
    /// </value>
    [Branch(null, null, true, true, true)]
    public virtual int? BranchID { get; set; }

    /// <summary>Date of the document.</summary>
    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual System.DateTime? DocDate { get; set; }

    /// <summary>
    /// The <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">financial period</see> of the document.
    /// </summary>
    /// <value>
    /// Determined by the <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.DocDate">date of the document</see>. Unlike <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.FinPeriodID" />
    /// the value of this field can't be overridden by user.
    /// </value>
    [PeriodID(null, null, null, true)]
    [PXUIField(DisplayName = "Master Period")]
    public virtual string TranPeriodID { get; set; }

    /// <summary>
    /// The <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">financial period</see> of the document.
    /// </summary>
    /// <value>
    /// The value defaults to the period to which the <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.DocDate" /> belongs,
    /// but can be overridden by a user.
    /// </value>
    [APOpenPeriod(typeof (APDocumentEnq.APRegister.docDate), typeof (APDocumentEnq.APRegister.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (APDocumentEnq.APRegister.tranPeriodID), IsHeader = true)]
    [PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string FinPeriodID { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.AP.Vendor" />, whom the document belongs to.
    /// </summary>
    [VendorActive(Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof (Vendor.acctName), CacheGlobal = true, Filterable = true)]
    [PXDefault]
    public virtual int? VendorID { get; set; }

    /// <summary>
    /// Identifier of the AP account, to which the document belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
    /// </value>
    [PXDefault]
    [Account(typeof (APDocumentEnq.APRegister.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, PX.Data.And<Where<Current<GLSetup.ytdNetIncAccountID>, PX.Data.IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DisplayName = "AP Account", ControlAccountForModule = "AP")]
    public virtual int? APAccountID { get; set; }

    /// <summary>
    /// Identifier of the AP subaccount, to which the document belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
    /// </value>
    [PXDefault]
    [SubAccount(typeof (APDocumentEnq.APRegister.aPAccountID), typeof (APDocumentEnq.APRegister.branchID), true, DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "AP Subaccount", Visibility = PXUIVisibility.Visible)]
    public virtual int? APSubID { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the document.
    /// </summary>
    /// <value>
    /// Generated automatically. Corresponds to the <see cref="!:PX.Objects.CM.CurrencyInfo.CurrencyInfoID" /> field.
    /// </value>
    [PXDBLong]
    [CurrencyInfo(ModuleCode = "AP")]
    public virtual long? CuryInfoID { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document was released.
    /// </summary>
    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Released", Visible = false)]
    public virtual bool? Released { get; set; }

    /// <summary>
    /// When set, on persist checks, that the document has the corresponded <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.Released" /> original value.
    /// When not set, on persist checks, that <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.Released" /> value is not changed.
    /// Throws an error otherwise.
    /// </summary>
    [PXDBRestrictionBool(typeof (APDocumentEnq.APRegister.released))]
    public virtual bool? ReleasedToVerify { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document is open.
    /// </summary>
    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Open", Visible = false)]
    public virtual bool? OpenDoc { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document is on hold and thus cannot be released.
    /// </summary>
    [PXDBBool]
    [PXUIField(DisplayName = "Hold", Visibility = PXUIVisibility.Visible)]
    [PXDefault(true, typeof (PX.Objects.AP.APSetup.holdEntry))]
    public virtual bool? Hold { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document is part of a <c>Schedule</c> and serves as a template for generating other documents according to it.
    /// </summary>
    [PXDBBool]
    [PXDefault(false)]
    public virtual bool? Scheduled { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document was voided. In this case <see cref="!:VoidBatchNbr" /> field will hold the number of the voiding <see cref="T:PX.Objects.GL.Batch" />.
    /// </summary>
    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Void", Visible = false)]
    public virtual bool? Voided { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document was printed.
    /// </summary>
    [PXDBBool]
    [PXDefault(false)]
    public virtual bool? Printed { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document was prebooked.
    /// </summary>
    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Prebooked")]
    public virtual bool? Prebooked { get; set; }

    [PXDBBool]
    [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual bool? Approved { get; set; }

    [PXDBBool]
    [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
    public bool? Rejected { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
    /// </value>
    [PXNote(DescriptionField = typeof (APDocumentEnq.APRegister.refNbr))]
    public virtual Guid? NoteID { get; set; }

    /// <summary>!REV!</summary>
    [PXDBGuid(false)]
    public virtual Guid? RefNoteID { get; set; }

    /// <summary>The date of the last application.</summary>
    [PXDBDate]
    [PXUIField(DisplayName = "Closed Date", Visibility = PXUIVisibility.Invisible)]
    public virtual System.DateTime? ClosedDate { get; set; }

    /// <summary>
    /// The <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">financial period</see>
    /// in which the document was closed.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.FinPeriodID" /> field.
    /// </value>
    [FinPeriodID(null, typeof (APDocumentEnq.APRegister.branchID), null, null, null, null, true, false, null, typeof (APDocumentEnq.APRegister.closedTranPeriodID), null, true, true)]
    [PXUIField(DisplayName = "Closed Period", Visibility = PXUIVisibility.Invisible)]
    public virtual string ClosedFinPeriodID { get; set; }

    /// <summary>
    /// The <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">financial period</see> in which the document was closed.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.TranPeriodID" /> field.
    /// </value>
    [PeriodID(null, null, null, true)]
    [PXUIField(DisplayName = "Closed Master Period", Visibility = PXUIVisibility.Invisible)]
    public virtual string ClosedTranPeriodID { get; set; }

    [PXDBBool]
    [PXUIField(DisplayName = "Retainage Bill", Enabled = false, FieldClass = "Retainage")]
    [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual bool? IsRetainageDocument { get; set; }

    /// <summary>Type of the original (source) document.</summary>
    [PXDBString(3, IsFixed = true)]
    [APDocType.List]
    [PXUIField(DisplayName = "Orig. Doc. Type")]
    public virtual string OrigDocType { get; set; }

    /// <summary>Reference number of the original (source) document.</summary>
    [PXDBString(15, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Orig. Ref. Nbr.")]
    public virtual string OrigRefNbr { get; set; }

    /// <summary>
    /// The amount to be paid for the document in the currency of the document. (See <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.CuryID" />)
    /// </summary>
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (APDocumentEnq.APRegister.curyInfoID), typeof (APDocumentEnq.APRegister.origDocAmt))]
    [PXUIField(DisplayName = "Amount", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual Decimal? CuryOrigDocAmt { get; set; }

    /// <summary>
    /// The amount to be paid for the document in the base currency of the company. (See <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
    /// </summary>
    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Amount")]
    public virtual Decimal? OrigDocAmt { get; set; }

    /// <summary>
    /// The balance of the Accounts Payable document after tax (if inclusive) and the discount in the currency of the document. (See <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.CuryID" />)
    /// </summary>
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (APDocumentEnq.APRegister.curyInfoID), typeof (APDocumentEnq.APRegister.docBal), BaseCalc = false)]
    [PXUIField(DisplayName = "Balance", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
    public virtual Decimal? CuryDocBal { get; set; }

    /// <summary>
    /// The balance of the Accounts Payable document after tax (if inclusive) and the discount in the base currency of the company. (See <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
    /// </summary>
    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? DocBal { get; set; }

    /// <summary>
    /// Total discount associated with the document in the base currency of the company. (See <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
    /// </summary>
    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? DiscTot { get; set; }

    /// <summary>
    /// Total discount associated with the document in the currency of the document. (See <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.CuryID" />)
    /// </summary>
    [PXDBCurrency(typeof (APDocumentEnq.APRegister.curyInfoID), typeof (APDocumentEnq.APRegister.discTot))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Discount Total", Enabled = true)]
    public virtual Decimal? CuryDiscTot { get; set; }

    /// <summary>
    /// !REV! The amount of the cash discount taken for the original document.
    /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.CuryID" />)
    /// </summary>
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (APDocumentEnq.APRegister.curyInfoID), typeof (APDocumentEnq.APRegister.origDiscAmt))]
    [PXUIField(DisplayName = "Cash Discount", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual Decimal? CuryOrigDiscAmt { get; set; }

    /// <summary>
    /// The amount of the cash discount taken for the original document.
    /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
    /// </summary>
    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? OrigDiscAmt { get; set; }

    /// <summary>
    /// !REV! The amount of the cash discount taken.
    /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.CuryID" />)
    /// </summary>
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (APDocumentEnq.APRegister.curyInfoID), typeof (APDocumentEnq.APRegister.discTaken))]
    public virtual Decimal? CuryDiscTaken { get; set; }

    /// <summary>
    /// The amount of the cash discount taken.
    /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
    /// </summary>
    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? DiscTaken { get; set; }

    /// <summary>
    /// The difference between the cash discount that was available and the actual amount of cash discount taken.
    /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.CuryID" />)
    /// </summary>
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (APDocumentEnq.APRegister.curyInfoID), typeof (APDocumentEnq.APRegister.discBal), BaseCalc = false)]
    [PXUIField(DisplayName = "Cash Discount Balance", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
    public virtual Decimal? CuryDiscBal { get; set; }

    /// <summary>
    /// The difference between the cash discount that was available and the actual amount of cash discount taken.
    /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
    /// </summary>
    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? DiscBal { get; set; }

    /// <summary>
    /// The amount of withholding tax calculated for the document, if applicable, in the currency of the document. (See <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.CuryID" />)
    /// </summary>
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (APDocumentEnq.APRegister.curyInfoID), typeof (APDocumentEnq.APRegister.origWhTaxAmt))]
    [PXUIField(DisplayName = "With. Tax", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
    public virtual Decimal? CuryOrigWhTaxAmt { get; set; }

    /// <summary>
    /// The amount of withholding tax calculated for the document, if applicable, in the base currency of the company. (See <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
    /// </summary>
    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? OrigWhTaxAmt { get; set; }

    /// <summary>
    /// !REV! The difference between the original amount of withholding tax to be payed and the amount that was actually paid.
    /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.CuryID" />)
    /// </summary>
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (APDocumentEnq.APRegister.curyInfoID), typeof (APDocumentEnq.APRegister.whTaxBal), BaseCalc = false)]
    public virtual Decimal? CuryWhTaxBal { get; set; }

    /// <summary>
    /// The difference between the original amount of withholding tax to be payed and the amount that was actually paid.
    /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
    /// </summary>
    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? WhTaxBal { get; set; }

    /// <summary>
    /// !REV! The amount of tax withheld from the payments to the document.
    /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.CuryID" />)
    /// </summary>
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (APDocumentEnq.APRegister.curyInfoID), typeof (APDocumentEnq.APRegister.taxWheld))]
    public virtual Decimal? CuryTaxWheld { get; set; }

    /// <summary>
    /// The amount of tax withheld from the payments to the document.
    /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
    /// </summary>
    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? TaxWheld { get; set; }

    [PXDBCurrency(typeof (APDocumentEnq.APRegister.curyInfoID), typeof (APDocumentEnq.APRegister.retainageTotal))]
    [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual Decimal? CuryRetainageTotal { get; set; }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
    public virtual Decimal? RetainageTotal { get; set; }

    [PXDBCurrency(typeof (APDocumentEnq.APRegister.curyInfoID), typeof (APDocumentEnq.APRegister.retainageUnreleasedAmt))]
    [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual Decimal? CuryRetainageUnreleasedAmt { get; set; }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
    public virtual Decimal? RetainageUnreleasedAmt { get; set; }

    /// <summary>Description of the document.</summary>
    [PXDBString(512 /*0x0200*/, IsUnicode = true)]
    [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string DocDesc { get; set; }

    [PXDBString(1, IsFixed = true)]
    [PXDefault("H")]
    [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
    [APDocStatus.List]
    public virtual string Status { get; set; }

    /// <summary>
    /// Realized Gain and Loss amount associated with the document.
    /// </summary>
    [PXDBDecimal(4)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? RGOLAmt { get; set; }

    [PXDecimal]
    [PXDependsOnFields(new System.Type[] {typeof (APDocumentEnq.APRegister.docType)})]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlOperand<APDocumentEnq.APRegister.docType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj, APDocType.quickCheck, APDocType.prepaymentInvoice>>, decimal1>, decimal_1>), typeof (Decimal))]
    public virtual Decimal? SignBalance { get; set; }

    /// <summary>
    /// Specifies (if set to <c>true</c>) that the record has been created
    /// in migration mode without affecting GL module.
    /// </summary>
    [MigratedRecord(typeof (PX.Objects.AP.APSetup.migrationMode))]
    public virtual bool? IsMigratedRecord { get; set; }

    /// <summary>
    /// Number of the <see cref="T:PX.Objects.GL.Batch" />, generated for the document on release.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Batch.BatchNbr">Batch.BatchNbr</see> field.
    /// </value>
    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Batch Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
    [PXSelector(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAP>>>))]
    [BatchNbr(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAP>>>), IsMigratedRecordField = typeof (APDocumentEnq.APRegister.isMigratedRecord))]
    public virtual string BatchNbr { get; set; }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APRegister.docType>
    {
    }

    public abstract class refNbr : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APDocumentEnq.APRegister.refNbr>
    {
    }

    public abstract class curyID : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APDocumentEnq.APRegister.curyID>
    {
    }

    public abstract class branchID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APDocumentEnq.APRegister.branchID>
    {
    }

    public abstract class docDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APDocumentEnq.APRegister.docDate>
    {
    }

    public abstract class tranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APRegister.tranPeriodID>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APRegister.finPeriodID>
    {
    }

    public abstract class vendorID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APDocumentEnq.APRegister.vendorID>
    {
    }

    public abstract class aPAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APRegister.aPAccountID>
    {
    }

    public abstract class aPSubID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APDocumentEnq.APRegister.aPSubID>
    {
    }

    public abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      APDocumentEnq.APRegister.curyInfoID>
    {
    }

    public abstract class released : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APDocumentEnq.APRegister.released>
    {
    }

    /// <exclude />
    public abstract class releasedToVerify : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APRegister.releasedToVerify>
    {
    }

    public abstract class openDoc : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APDocumentEnq.APRegister.openDoc>
    {
    }

    public abstract class hold : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APDocumentEnq.APRegister.hold>
    {
    }

    public abstract class scheduled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APRegister.scheduled>
    {
    }

    public abstract class voided : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APDocumentEnq.APRegister.voided>
    {
    }

    public abstract class printed : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APDocumentEnq.APRegister.printed>
    {
    }

    public abstract class prebooked : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APRegister.prebooked>
    {
    }

    public abstract class approved : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APDocumentEnq.APRegister.approved>
    {
    }

    public abstract class rejected : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APDocumentEnq.APRegister.rejected>
    {
    }

    public abstract class noteID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    APDocumentEnq.APRegister.noteID>
    {
    }

    public abstract class refNoteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      APDocumentEnq.APRegister.refNoteID>
    {
    }

    public abstract class closedDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APDocumentEnq.APRegister.closedDate>
    {
    }

    public abstract class closedFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APRegister.closedFinPeriodID>
    {
    }

    public abstract class closedTranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APRegister.closedTranPeriodID>
    {
    }

    public abstract class isRetainageDocument : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APRegister.isRetainageDocument>
    {
    }

    public abstract class origDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APRegister.origDocType>
    {
    }

    public abstract class origRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APRegister.origRefNbr>
    {
    }

    public abstract class curyOrigDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.curyOrigDocAmt>
    {
    }

    public abstract class origDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.origDocAmt>
    {
    }

    public abstract class curyDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.curyDocBal>
    {
    }

    public abstract class docBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.docBal>
    {
    }

    public abstract class discTot : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.discTot>
    {
    }

    public abstract class curyDiscTot : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.curyDiscTot>
    {
    }

    public abstract class curyOrigDiscAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.curyOrigDiscAmt>
    {
    }

    public abstract class origDiscAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.origDiscAmt>
    {
    }

    public abstract class curyDiscTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.curyDiscTaken>
    {
    }

    public abstract class discTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.discTaken>
    {
    }

    public abstract class curyDiscBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.curyDiscBal>
    {
    }

    public abstract class discBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.discBal>
    {
    }

    public abstract class curyOrigWhTaxAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.curyOrigWhTaxAmt>
    {
    }

    public abstract class origWhTaxAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.origWhTaxAmt>
    {
    }

    public abstract class curyWhTaxBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.curyWhTaxBal>
    {
    }

    public abstract class whTaxBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.whTaxBal>
    {
    }

    public abstract class curyTaxWheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.curyTaxWheld>
    {
    }

    public abstract class taxWheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.taxWheld>
    {
    }

    public abstract class curyRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.curyRetainageTotal>
    {
    }

    public abstract class retainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.retainageTotal>
    {
    }

    public abstract class curyRetainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.curyRetainageUnreleasedAmt>
    {
    }

    public abstract class retainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.retainageUnreleasedAmt>
    {
    }

    public abstract class docDesc : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APRegister.docDesc>
    {
    }

    public abstract class status : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APDocumentEnq.APRegister.status>
    {
    }

    public abstract class rGOLAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.rGOLAmt>
    {
    }

    public abstract class signBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APRegister.signBalance>
    {
    }

    public abstract class isMigratedRecord : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APRegister.isMigratedRecord>
    {
    }

    public abstract class batchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APRegister.batchNbr>
    {
    }
  }

  [PXProjection(typeof (Select2<APDocumentEnq.APRegister, LeftJoin<APDocumentEnq.Ref.APInvoice, On<APDocumentEnq.Ref.APInvoice.docType, Equal<APDocumentEnq.APRegister.docType>, And<APDocumentEnq.Ref.APInvoice.refNbr, Equal<APDocumentEnq.APRegister.refNbr>>>, LeftJoin<APDocumentEnq.Ref.APPayment, On<APDocumentEnq.Ref.APPayment.docType, Equal<APDocumentEnq.APRegister.docType>, And<APDocumentEnq.Ref.APPayment.refNbr, Equal<APDocumentEnq.APRegister.refNbr>>>>>, PX.Data.Where<Not<Where<APDocumentEnq.Ref.APInvoice.docType, Equal<APDocType.prepayment>, And<APDocumentEnq.Ref.APPayment.refNbr, PX.Data.IsNull>>>>>))]
  [PXPrimaryGraph(new System.Type[] {typeof (APQuickCheckEntry), typeof (TXInvoiceEntry), typeof (APInvoiceEntry), typeof (APPaymentEntry)}, new System.Type[] {typeof (PX.Data.Select<APQuickCheck, Where<APQuickCheck.docType, Equal<Current<APDocumentEnq.APDocumentResult.docType>>, And<APQuickCheck.refNbr, Equal<Current<APDocumentEnq.APDocumentResult.refNbr>>>>>), typeof (PX.Data.Select<APInvoice, Where<APInvoice.docType, Equal<Current<APDocumentEnq.APDocumentResult.docType>>, And<APInvoice.refNbr, Equal<Current<APDocumentEnq.APDocumentResult.refNbr>>, PX.Data.And<Where<APInvoice.released, Equal<False>, And<PX.Objects.AP.APRegister.origModule, Equal<BatchModule.moduleTX>>>>>>>), typeof (PX.Data.Select<APInvoice, Where<APInvoice.docType, Equal<Current<APDocumentEnq.APDocumentResult.docType>>, And<APInvoice.refNbr, Equal<Current<APDocumentEnq.APDocumentResult.refNbr>>>>>), typeof (PX.Data.Select<APPayment, Where<APPayment.docType, Equal<Current<APDocumentEnq.APDocumentResult.docType>>, And<APPayment.refNbr, Equal<Current<APDocumentEnq.APDocumentResult.refNbr>>>>>)})]
  [PXCacheName("Vendor Details")]
  [Serializable]
  public class APDocumentResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected Decimal? _CuryWhTaxBal;
    protected Decimal? _WhTaxBal;

    [PXDBString(3, IsKey = true, IsFixed = true, BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXDefault]
    [APDocType.List]
    [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = true, TabOrder = 0)]
    public virtual string DocType { get; set; }

    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXDefault]
    [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 1)]
    [PXSelector(typeof (Search<APDocumentEnq.APRegister.refNbr>), Filterable = true)]
    public virtual string RefNbr { get; set; }

    /// <summary>
    /// The counter of <see cref="!:TermsInstallment">installments</see> associated with the document.
    /// </summary>
    [PXDBShort(BqlTable = typeof (APDocumentEnq.Ref.APInvoice))]
    public virtual short? InstallmentCntr { get; set; }

    /// <summary>
    /// A reference to the <see cref="T:PX.Objects.AP.Vendor" />.
    /// </summary>
    /// <value>
    /// An integer identifier of the vendor that supplied the goods.
    /// </value>
    [Vendor(DisplayName = "Supplied-By Vendor", DescriptionField = typeof (Vendor.acctName), FieldClass = "VendorRelations", CacheGlobal = true, Filterable = true, BqlTable = typeof (APDocumentEnq.Ref.APInvoice))]
    public virtual int? SuppliedByVendorID { get; set; }

    /// <summary>
    /// Code of the <see cref="T:PX.Objects.CM.Currency">Currency</see> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">company's base currency</see>.
    /// </value>
    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible)]
    [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string CuryID { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.GL.Branch">Branch</see>, to which the document belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID">Branch.BranchID</see> field.
    /// </value>
    [Branch(null, null, true, true, true, BqlTable = typeof (APDocumentEnq.APRegister))]
    public virtual int? BranchID { get; set; }

    /// <summary>Date of the document.</summary>
    [PXDBDate(BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual System.DateTime? DocDate { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Determined by the <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.DocDate">date of the document</see>. Unlike <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.FinPeriodID" />
    /// the value of this field can't be overriden by user.
    /// </value>
    [PeriodID(null, null, null, true, BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXUIField(DisplayName = "Master Period")]
    public virtual string TranPeriodID { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the period, to which the <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.DocDate" /> belongs, but can be overriden by user.
    /// </value>
    [APOpenPeriod(typeof (APDocumentEnq.APRegister.docDate), typeof (APDocumentEnq.APRegister.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (APDocumentEnq.APRegister.tranPeriodID), IsHeader = true, BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string FinPeriodID { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.AP.Vendor" />, whom the document belongs to.
    /// </summary>
    [VendorActive(Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof (Vendor.acctName), CacheGlobal = true, Filterable = true, BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXDefault]
    public virtual int? VendorID { get; set; }

    /// <summary>
    /// Identifier of the AP account, to which the document belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
    /// </value>
    [PXDefault]
    [Account(typeof (APDocumentEnq.APRegister.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, PX.Data.And<Where<Current<GLSetup.ytdNetIncAccountID>, PX.Data.IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DisplayName = "AP Account", ControlAccountForModule = "AP", BqlTable = typeof (APDocumentEnq.APRegister))]
    public virtual int? APAccountID { get; set; }

    /// <summary>
    /// Identifier of the AP subaccount, to which the document belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
    /// </value>
    [PXDefault]
    [SubAccount(typeof (APDocumentEnq.APRegister.aPAccountID), typeof (APDocumentEnq.APRegister.branchID), true, DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "AP Subaccount", Visibility = PXUIVisibility.Visible, BqlTable = typeof (APDocumentEnq.APRegister))]
    public virtual int? APSubID { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the document.
    /// </summary>
    /// <value>
    /// Generated automatically. Corresponds to the <see cref="!:PX.Objects.CM.CurrencyInfo.CurrencyInfoID" /> field.
    /// </value>
    [PXDBLong(BqlTable = typeof (APDocumentEnq.APRegister))]
    [CurrencyInfo(ModuleCode = "AP")]
    public virtual long? CuryInfoID { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document was released.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Released", Visible = false)]
    public virtual bool? Released { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document was prebooked.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Prebooked")]
    public virtual bool? Prebooked { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document is open.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Open", Visible = false)]
    public virtual bool? OpenDoc { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document is on hold and thus cannot be released.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXUIField(DisplayName = "Hold", Visibility = PXUIVisibility.Visible)]
    [PXDefault(true, typeof (PX.Objects.AP.APSetup.holdEntry))]
    public virtual bool? Hold { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document is part of a <c>Schedule</c> and serves as a template for generating other documents according to it.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXDefault(false)]
    public virtual bool? Scheduled { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document was voided. In this case <see cref="!:VoidBatchNbr" /> field will hold the number of the voiding <see cref="T:PX.Objects.GL.Batch" />.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Void", Visible = false)]
    public virtual bool? Voided { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
    /// </value>
    [APDocumentEnq.APDocumentResult.noteID.Note]
    public virtual Guid? NoteID { get; set; }

    /// <summary>The date of the last application.</summary>
    [PXDBDate(BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXUIField(DisplayName = "Closed Date", Visibility = PXUIVisibility.Invisible)]
    public virtual System.DateTime? ClosedDate { get; set; }

    /// <summary>
    /// The <see cref="!:PX.Objects.GL.FinancialPeriod">Financial Period</see>, in which the document was closed.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AP.APDocumentEnq.APDocumentResult.FinPeriodID" /> field.
    /// </value>
    [FinPeriodID(null, typeof (APDocumentEnq.APRegister.branchID), null, null, null, null, true, false, null, typeof (APDocumentEnq.APRegister.closedTranPeriodID), null, true, true, BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXUIField(DisplayName = "Closed Period", Visibility = PXUIVisibility.Invisible)]
    public virtual string ClosedFinPeriodID { get; set; }

    /// <summary>
    /// The <see cref="!:PX.Objects.GL.FinancialPeriod">Financial Period</see>, in which the document was closed.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AP.APDocumentEnq.APDocumentResult.TranPeriodID" /> field.
    /// </value>
    [PeriodID(null, null, null, true, BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXUIField(DisplayName = "Closed Master Period", Visibility = PXUIVisibility.Invisible)]
    public virtual string ClosedTranPeriodID { get; set; }

    [PXDBBool(BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXUIField(DisplayName = "Retainage Bill", Enabled = false, FieldClass = "Retainage")]
    [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual bool? IsRetainageDocument { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (APDocumentEnq.APRegister.curyInfoID), typeof (APDocumentEnq.APRegister.origDocAmt))]
    [PXUIField(DisplayName = "Currency Origin. Amount")]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APRegister.docType, Equal<APDocType.prepayment>>>>>.And<BqlOperand<APInvoice.refNbr, IBqlString>.IsNotNull>>, decimal0>, Mult<APDocumentEnq.APRegister.signBalance, APDocumentEnq.APRegister.curyOrigDocAmt>>), typeof (Decimal))]
    public virtual Decimal? CuryOrigDocAmt { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "Origin. Amount")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APRegister.docType, Equal<APDocType.prepayment>>>>>.And<BqlOperand<APInvoice.refNbr, IBqlString>.IsNotNull>>, decimal0>, Mult<APDocumentEnq.APRegister.signBalance, APDocumentEnq.APRegister.origDocAmt>>), typeof (Decimal))]
    public virtual Decimal? OrigDocAmt { get; set; }

    [PXCurrency(typeof (APDocumentEnq.APDocumentResult.curyInfoID), typeof (APDocumentEnq.APDocumentResult.retainageTotal))]
    [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXDBCalced(typeof (Mult<APDocumentEnq.APRegister.signBalance, APDocumentEnq.APRegister.curyRetainageTotal>), typeof (Decimal))]
    public virtual Decimal? CuryRetainageTotal { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
    [PXDBCalced(typeof (Mult<APDocumentEnq.APRegister.signBalance, APDocumentEnq.APRegister.retainageTotal>), typeof (Decimal))]
    public virtual Decimal? RetainageTotal { get; set; }

    [PXCury(typeof (APDocumentEnq.APDocumentResult.curyID))]
    [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXDBCalced(typeof (BqlFunction<Add<APDocumentEnq.APRegister.curyOrigDocAmt, APDocumentEnq.APRegister.curyRetainageTotal>, IBqlDecimal>.Multiply<APDocumentEnq.APRegister.signBalance>), typeof (Decimal))]
    public virtual Decimal? CuryOrigDocAmtWithRetainageTotal { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
    [PXDBCalced(typeof (BqlFunction<Add<APDocumentEnq.APRegister.origDocAmt, APDocumentEnq.APRegister.retainageTotal>, IBqlDecimal>.Multiply<APDocumentEnq.APRegister.signBalance>), typeof (Decimal))]
    public virtual Decimal? OrigDocAmtWithRetainageTotal { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "RGOL Amount")]
    [PXDBCalced(typeof (Mult<decimal_1, APDocumentEnq.APRegister.rGOLAmt>), typeof (Decimal))]
    public virtual Decimal? RGOLAmt { get; set; }

    /// <summary>Type of the original (source) document.</summary>
    [PXDBString(3, IsFixed = true, BqlTable = typeof (APDocumentEnq.APRegister))]
    [APDocType.List]
    [PXUIField(DisplayName = "Orig. Doc. Type")]
    public virtual string OrigDocType { get; set; }

    /// <summary>Reference number of the original (source) document.</summary>
    [PXDBString(15, IsUnicode = true, InputMask = "", BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXUIField(DisplayName = "Orig. Ref. Nbr.")]
    public virtual string OrigRefNbr { get; set; }

    [PXString(30, IsUnicode = true)]
    [PXUIField(DisplayName = "Vendor Invoice Nbr./Payment Nbr.")]
    [PXDBCalced(typeof (IsNull<APDocumentEnq.Ref.APInvoice.invoiceNbr, APDocumentEnq.Ref.APPayment.extRefNbr>), typeof (string))]
    public virtual string ExtRefNbr { get; set; }

    [PXDBString(10, IsUnicode = true, BqlField = typeof (APDocumentEnq.Ref.APPayment.paymentMethodID))]
    [PXUIField(DisplayName = "Payment Method")]
    public virtual string PaymentMethodID { get; set; }

    [PXCurrency(typeof (APDocumentEnq.APRegister.curyInfoID), typeof (APDocumentEnq.APDocumentResult.begBalance))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Currency Period Beg. Balance")]
    public virtual Decimal? CuryBegBalance { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Period Beg. Balance")]
    public virtual Decimal? BegBalance { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (APDocumentEnq.APRegister.curyInfoID), typeof (APDocumentEnq.APDocumentResult.discActTaken))]
    [PXUIField(DisplayName = "Currency Cash Discount Taken")]
    [PXDBCalced(typeof (Mult<APDocumentEnq.APRegister.signBalance, APDocumentEnq.APRegister.curyDiscTaken>), typeof (Decimal))]
    public virtual Decimal? CuryDiscActTaken { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Cash Discount Taken")]
    [PXDBCalced(typeof (Mult<APDocumentEnq.APRegister.signBalance, APDocumentEnq.APRegister.discTaken>), typeof (Decimal))]
    public virtual Decimal? DiscActTaken { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (APDocumentEnq.APRegister.curyInfoID), typeof (APDocumentEnq.APDocumentResult.taxWheld))]
    [PXUIField(DisplayName = "Currency Tax Withheld")]
    [PXDBCalced(typeof (Mult<APDocumentEnq.APRegister.signBalance, APDocumentEnq.APRegister.curyTaxWheld>), typeof (Decimal))]
    public virtual Decimal? CuryTaxWheld { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Tax Withheld")]
    [PXDBCalced(typeof (Mult<APDocumentEnq.APRegister.signBalance, APDocumentEnq.APRegister.taxWheld>), typeof (Decimal))]
    public virtual Decimal? TaxWheld { get; set; }

    /// <summary>
    /// Expected GL turnover for the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "AP Turnover")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? APTurnover { get; set; }

    /// <summary>
    /// Expected GL turnover for the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXBaseCury]
    [PXUIField(DisplayName = "GL Turnover")]
    public virtual Decimal? GLTurnover { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (APDocumentEnq.APRegister.curyInfoID), typeof (APDocumentEnq.APRegister.docBal), BaseCalc = false)]
    [PXUIField(DisplayName = "Currency Balance")]
    [PXDBCalced(typeof (Switch<Case<Where<APDocumentEnq.APRegister.voided, NotEqual<True>>, Mult<APDocumentEnq.APRegister.signBalance, APDocumentEnq.APRegister.curyDocBal>>, decimal0>), typeof (Decimal))]
    public virtual Decimal? CuryDocBal { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Balance")]
    [PXDBCalced(typeof (Switch<Case<Where<APDocumentEnq.APRegister.voided, NotEqual<True>>, Mult<APDocumentEnq.APRegister.signBalance, APDocumentEnq.APRegister.docBal>>, decimal0>), typeof (Decimal))]
    public virtual Decimal? DocBal { get; set; }

    /// <summary>
    /// !REV! The difference between the original amount of withholding tax to be payed and the amount that was actually paid.
    /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AP.APDocumentEnq.APDocumentResult.CuryID" />)
    /// </summary>
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (APDocumentEnq.APRegister.curyInfoID), typeof (APDocumentEnq.APRegister.whTaxBal), BaseCalc = false)]
    [PXDBCalced(typeof (Mult<APDocumentEnq.APRegister.signBalance, APDocumentEnq.APRegister.curyWhTaxBal>), typeof (Decimal))]
    public virtual Decimal? CuryWhTaxBal { get; set; }

    /// <summary>
    /// The difference between the original amount of withholding tax to be payed and the amount that was actually paid.
    /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
    /// </summary>
    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCalced(typeof (Mult<APDocumentEnq.APRegister.signBalance, APDocumentEnq.APRegister.whTaxBal>), typeof (Decimal))]
    public virtual Decimal? WhTaxBal { get; set; }

    [PXCurrency(typeof (APDocumentEnq.APRegister.curyInfoID), typeof (APDocumentEnq.APRegister.retainageUnreleasedAmt))]
    [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXDBCalced(typeof (Mult<APDocumentEnq.APRegister.signBalance, APDocumentEnq.APRegister.curyRetainageUnreleasedAmt>), typeof (Decimal))]
    public virtual Decimal? CuryRetainageUnreleasedAmt { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
    [PXDBCalced(typeof (Mult<APDocumentEnq.APRegister.signBalance, APDocumentEnq.APRegister.retainageUnreleasedAmt>), typeof (Decimal))]
    public virtual Decimal? RetainageUnreleasedAmt { get; set; }

    [PXDBString(1, IsFixed = true, BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXDefault("H")]
    [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
    [APDocStatus.List]
    public virtual string Status { get; set; }

    /// <summary>Description of the document.</summary>
    [PXDBString(512 /*0x0200*/, IsUnicode = true, BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string DocDesc { get; set; }

    [PXString]
    [PXDBCalced(typeof (APDocumentEnq.APRegister.tranPeriodID), typeof (string))]
    public virtual string TranPostPeriodID { get; set; }

    [PXString]
    [PXDBCalced(typeof (APDocumentEnq.APRegister.finPeriodID), typeof (string))]
    public virtual string FinPostPeriodID { get; set; }

    /// <summary>
    /// Specifies (if set to <c>true</c>) that the record has been created
    /// in migration mode without affecting GL module.
    /// </summary>
    [MigratedRecord(typeof (PX.Objects.AP.APSetup.migrationMode), BqlTable = typeof (APDocumentEnq.APRegister))]
    public virtual bool? IsMigratedRecord { get; set; }

    /// <summary>
    /// Number of the <see cref="T:PX.Objects.GL.Batch" />, generated for the document on release.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Batch.BatchNbr">Batch.BatchNbr</see> field.
    /// </value>
    [PXDBString(15, IsUnicode = true, BqlTable = typeof (APDocumentEnq.APRegister))]
    [PXUIField(DisplayName = "Batch Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
    [PXSelector(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAP>>>))]
    [BatchNbr(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAP>>>), IsMigratedRecordField = typeof (APDocumentEnq.APDocumentResult.isMigratedRecord))]
    public virtual string BatchNbr { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APDocumentEnq.APRegister.APAccountID" />
    [Account(typeof (APDocumentEnq.APDocumentResult.branchID), DisplayName = "Account", BqlField = typeof (APDocumentEnq.APRegister.aPAccountID))]
    public virtual int? AccountID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AP.APDocumentEnq.APRegister.APSubID" />
    /// .
    [SubAccount(typeof (APDocumentEnq.APDocumentResult.accountID), DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "Subaccount", Visibility = PXUIVisibility.Visible, BqlField = typeof (APDocumentEnq.APRegister.aPSubID))]
    public virtual int? SubID { get; set; }

    /// <inheritdoc cref="!:APRegister.PendingPayment" />
    /// .
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APRegister))]
    public virtual bool? PendingPayment { get; set; }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.docType>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.refNbr>
    {
    }

    public abstract class installmentCntr : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.installmentCntr>
    {
    }

    public abstract class suppliedByVendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.suppliedByVendorID>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.curyID>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.branchID>
    {
    }

    public abstract class docDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.docDate>
    {
    }

    public abstract class tranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.tranPeriodID>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.finPeriodID>
    {
    }

    public abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.vendorID>
    {
    }

    public abstract class aPAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.aPAccountID>
    {
    }

    public abstract class aPSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.aPSubID>
    {
    }

    public abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.curyInfoID>
    {
    }

    public abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.released>
    {
    }

    public abstract class prebooked : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.prebooked>
    {
    }

    public abstract class openDoc : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.openDoc>
    {
    }

    public abstract class hold : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APDocumentEnq.APDocumentResult.hold>
    {
    }

    public abstract class scheduled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.scheduled>
    {
    }

    public abstract class voided : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.voided>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.noteID>
    {
      public class NoteAttribute : PXNoteAttribute
      {
        public NoteAttribute() => this.BqlTable = typeof (APDocumentEnq.APRegister);

        protected override bool IsVirtualTable(System.Type table) => false;

        protected override string GetEntityType(PXCache cache, Guid? noteId)
        {
          object current = cache?.Current;
          return current != null && APDocType.DocClass(cache.GetValue<APDocumentEnq.APDocumentResult.docType>(current) as string) != "N" ? typeof (APPayment).FullName : typeof (APInvoice).FullName;
        }
      }
    }

    public abstract class closedDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.closedDate>
    {
    }

    public abstract class closedFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.closedFinPeriodID>
    {
    }

    public abstract class closedTranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.closedTranPeriodID>
    {
    }

    public abstract class isRetainageDocument : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.isRetainageDocument>
    {
    }

    public abstract class curyOrigDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.curyOrigDocAmt>
    {
    }

    public abstract class origDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.origDocAmt>
    {
    }

    public abstract class curyRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.curyRetainageTotal>
    {
    }

    public abstract class retainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.retainageTotal>
    {
    }

    public abstract class curyOrigDocAmtWithRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.curyOrigDocAmtWithRetainageTotal>
    {
    }

    public abstract class origDocAmtWithRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.origDocAmtWithRetainageTotal>
    {
    }

    public abstract class rGOLAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.rGOLAmt>
    {
    }

    public abstract class origDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.origDocType>
    {
    }

    public abstract class origRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.origRefNbr>
    {
    }

    public abstract class extRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.extRefNbr>
    {
    }

    public abstract class paymentMethodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.paymentMethodID>
    {
    }

    public abstract class curyBegBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.curyBegBalance>
    {
    }

    public abstract class begBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.begBalance>
    {
    }

    public abstract class curyDiscActTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.curyDiscActTaken>
    {
    }

    public abstract class discActTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.discActTaken>
    {
    }

    public abstract class curyTaxWheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.curyTaxWheld>
    {
    }

    public abstract class taxWheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.taxWheld>
    {
    }

    public abstract class aPTurnover : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.aPTurnover>
    {
    }

    public abstract class gLTurnover : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.gLTurnover>
    {
    }

    public abstract class curyDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.curyDocBal>
    {
    }

    public abstract class docBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.docBal>
    {
    }

    public abstract class curyWhTaxBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.curyWhTaxBal>
    {
    }

    public abstract class whTaxBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.whTaxBal>
    {
    }

    public abstract class curyRetainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.curyRetainageUnreleasedAmt>
    {
    }

    public abstract class retainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.retainageUnreleasedAmt>
    {
    }

    public abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.status>
    {
    }

    public abstract class docDesc : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.docDesc>
    {
    }

    public abstract class tranPostPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.tranPostPeriodID>
    {
    }

    public abstract class finPostPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.finPostPeriodID>
    {
    }

    public abstract class isMigratedRecord : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.isMigratedRecord>
    {
    }

    public abstract class batchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.batchNbr>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.accountID>
    {
    }

    public abstract class subID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APDocumentEnq.APDocumentResult.subID>
    {
    }

    public abstract class pendingPayment : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentResult.pendingPayment>
    {
    }
  }

  [PXProjection(typeof (Select2<APDocumentEnq.APDocumentResult, LeftJoin<APTranPostGL, On<APTranPostGL.docType, Equal<APDocumentEnq.APDocumentResult.docType>, And<APTranPostGL.refNbr, Equal<APDocumentEnq.APDocumentResult.refNbr>>>>>))]
  [PXHidden]
  public class APDocumentPeriodResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(3, IsKey = true, IsFixed = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault]
    [APDocType.List]
    [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = true, TabOrder = 0)]
    public virtual string DocType { get; set; }

    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault]
    [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 1)]
    [PXSelector(typeof (Search<APDocumentEnq.APDocumentResult.refNbr>), Filterable = true)]
    public virtual string RefNbr { get; set; }

    /// <summary>
    /// A reference to the <see cref="T:PX.Objects.AP.Vendor" />.
    /// </summary>
    /// <value>
    /// An integer identifier of the vendor that supplied the goods.
    /// </value>
    [Vendor(DisplayName = "Supplied-By Vendor", DescriptionField = typeof (Vendor.acctName), FieldClass = "VendorRelations", CacheGlobal = true, Filterable = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    public virtual int? SuppliedByVendorID { get; set; }

    /// <summary>
    /// Code of the <see cref="T:PX.Objects.CM.Currency">Currency</see> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">company's base currency</see>.
    /// </value>
    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible)]
    [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string CuryID { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.GL.Branch">Branch</see>, to which the document belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID">Branch.BranchID</see> field.
    /// </value>
    [Branch(null, null, true, true, true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    public virtual int? BranchID { get; set; }

    /// <summary>Date of the document.</summary>
    [PXDBDate(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual System.DateTime? DocDate { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Determined by the <see cref="P:PX.Objects.AP.APDocumentEnq.APDocumentResult.DocDate">date of the document</see>. Unlike <see cref="P:PX.Objects.AP.APDocumentEnq.APDocumentResult.FinPeriodID" />
    /// the value of this field can't be overriden by user.
    /// </value>
    [PeriodID(null, null, null, true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Master Period")]
    public virtual string TranPeriodID { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the period, to which the <see cref="P:PX.Objects.AP.APDocumentEnq.APDocumentResult.DocDate" /> belongs, but can be overriden by user.
    /// </value>
    [APOpenPeriod(typeof (APDocumentEnq.APDocumentResult.docDate), typeof (APDocumentEnq.APDocumentResult.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (APDocumentEnq.APDocumentResult.tranPeriodID), IsHeader = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string FinPeriodID { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.AP.Vendor" />, whom the document belongs to.
    /// </summary>
    [VendorActive(Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof (Vendor.acctName), CacheGlobal = true, Filterable = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault]
    public virtual int? VendorID { get; set; }

    /// <summary>
    /// Identifier of the AP account, to which the document belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
    /// </value>
    [PXDefault]
    [Account(typeof (APDocumentEnq.APDocumentResult.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, PX.Data.And<Where<Current<GLSetup.ytdNetIncAccountID>, PX.Data.IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DisplayName = "AP Account", ControlAccountForModule = "AP", BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    public virtual int? APAccountID { get; set; }

    /// <summary>
    /// Identifier of the AP subaccount, to which the document belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
    /// </value>
    [PXDefault]
    [SubAccount(typeof (APDocumentEnq.APDocumentResult.aPAccountID), typeof (APDocumentEnq.APDocumentResult.branchID), true, DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "AP Subaccount", Visibility = PXUIVisibility.Visible, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    public virtual int? APSubID { get; set; }

    /// <summary>
    /// Specifies (if set to <c>true</c>) that the record has been created
    /// in migration mode without affecting GL module.
    /// </summary>
    [MigratedRecord(typeof (PX.Objects.AP.APSetup.migrationMode), BqlTable = typeof (APDocumentEnq.APRegister))]
    public virtual bool? IsMigratedRecord { get; set; }

    /// <summary>
    /// The counter of <see cref="!:TermsInstallment">installments</see> associated with the document.
    /// </summary>
    [PXDBShort(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    public virtual short? InstallmentCntr { get; set; }

    /// <summary>
    /// Number of the <see cref="T:PX.Objects.GL.Batch" />, generated for the document on release.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Batch.BatchNbr">Batch.BatchNbr</see> field.
    /// </value>
    [PXDBString(15, IsUnicode = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Batch Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
    [PXSelector(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAP>>>))]
    [BatchNbr(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAP>>>), IsMigratedRecordField = typeof (APDocumentEnq.APDocumentPeriodResult.isMigratedRecord))]
    public virtual string BatchNbr { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the document.
    /// </summary>
    /// <value>
    /// Generated automatically. Corresponds to the <see cref="!:PX.Objects.CM.CurrencyInfo.CurrencyInfoID" /> field.
    /// </value>
    [PXDBLong(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [CurrencyInfo(ModuleCode = "AP")]
    public virtual long? CuryInfoID { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document was released.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Released", Visible = false)]
    public virtual bool? Released { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document was prebooked.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Prebooked")]
    public virtual bool? Prebooked { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document is open.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Open", Visible = false)]
    public virtual bool? OpenDoc { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document is on hold and thus cannot be released.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Hold", Visibility = PXUIVisibility.Visible)]
    [PXDefault(true, typeof (PX.Objects.AP.APSetup.holdEntry))]
    public virtual bool? Hold { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document is part of a <c>Schedule</c> and serves as a template for generating other documents according to it.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(false)]
    public virtual bool? Scheduled { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document was voided. In this case <see cref="!:VoidBatchNbr" /> field will hold the number of the voiding <see cref="T:PX.Objects.GL.Batch" />.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Void", Visible = false)]
    public virtual bool? Voided { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
    /// </value>
    [PXNote(DescriptionField = typeof (APDocumentEnq.APDocumentPeriodResult.refNbr), BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    public virtual Guid? NoteID { get; set; }

    /// <summary>The date of the last application.</summary>
    [PXDBDate(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Closed Date", Visibility = PXUIVisibility.Invisible)]
    public virtual System.DateTime? ClosedDate { get; set; }

    /// <summary>
    /// The <see cref="!:PX.Objects.GL.FinancialPeriod">Financial Period</see>, in which the document was closed.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AP.APDocumentEnq.APDocumentPeriodResult.FinPeriodID" /> field.
    /// </value>
    [FinPeriodID(null, typeof (APDocumentEnq.APDocumentResult.branchID), null, null, null, null, true, false, null, typeof (APDocumentEnq.APDocumentResult.closedTranPeriodID), null, true, true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Closed Period", Visibility = PXUIVisibility.Invisible)]
    public virtual string ClosedFinPeriodID { get; set; }

    /// <summary>
    /// The <see cref="!:PX.Objects.GL.FinancialPeriod">Financial Period</see>, in which the document was closed.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AP.APDocumentEnq.APDocumentPeriodResult.TranPeriodID" /> field.
    /// </value>
    [PeriodID(null, null, null, true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Closed Master Period", Visibility = PXUIVisibility.Invisible)]
    public virtual string ClosedTranPeriodID { get; set; }

    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Retainage Bill", Enabled = false, FieldClass = "Retainage")]
    [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual bool? IsRetainageDocument { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (APDocumentEnq.APDocumentResult.curyInfoID), typeof (APDocumentEnq.APDocumentResult.origDocAmt), BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APTranPostGL.type, IBqlString>.IsEqual<APTranPost.type.origin>>>, BqlOperand<APTranPostGL.glSign, IBqlShort>.Multiply<APDocumentEnq.APDocumentResult.curyOrigDocAmt>, Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APDocumentEnq.APDocumentResult.released, IBqlBool>.IsEqual<True>>>, Null>>, APDocumentEnq.APDocumentResult.curyOrigDocAmt>), typeof (Decimal))]
    [PXUIField(DisplayName = "Currency Origin. Amount")]
    public virtual Decimal? CuryOrigDocAmt { get; set; }

    [PXBaseCury(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APTranPostGL.type, IBqlString>.IsEqual<APTranPost.type.origin>>>, BqlOperand<APTranPostGL.glSign, IBqlShort>.Multiply<APDocumentEnq.APDocumentResult.origDocAmt>, Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APDocumentEnq.APDocumentResult.released, IBqlBool>.IsEqual<True>>>, Null>>, APDocumentEnq.APDocumentResult.origDocAmt>), typeof (Decimal))]
    [PXUIField(DisplayName = "Origin. Amount")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? OrigDocAmt { get; set; }

    [PXDBCurrency(typeof (APDocumentEnq.APDocumentPeriodResult.curyInfoID), typeof (APDocumentEnq.APDocumentPeriodResult.retainageTotal), BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual Decimal? CuryRetainageTotal { get; set; }

    [PXDBBaseCury(null, null, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
    public virtual Decimal? RetainageTotal { get; set; }

    [PXCury(typeof (APDocumentEnq.APDocumentPeriodResult.curyID), BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APTranPostGL.type, IBqlString>.IsEqual<APTranPost.type.origin>>>, BqlOperand<APTranPostGL.glSign, IBqlShort>.Multiply<APDocumentEnq.APDocumentResult.curyOrigDocAmtWithRetainageTotal>, Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APDocumentEnq.APDocumentResult.released, IBqlBool>.IsEqual<True>>>, Null>>, APDocumentEnq.APDocumentResult.curyOrigDocAmtWithRetainageTotal>), typeof (Decimal))]
    public virtual Decimal? CuryOrigDocAmtWithRetainageTotal { get; set; }

    [PXBaseCury(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APTranPostGL.type, IBqlString>.IsEqual<APTranPost.type.origin>>>, BqlOperand<APTranPostGL.glSign, IBqlShort>.Multiply<APDocumentEnq.APDocumentResult.origDocAmtWithRetainageTotal>, Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APDocumentEnq.APDocumentResult.released, IBqlBool>.IsEqual<True>>>, Null>>, APDocumentEnq.APDocumentResult.origDocAmtWithRetainageTotal>), typeof (Decimal))]
    public virtual Decimal? OrigDocAmtWithRetainageTotal { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "RGOL Amount")]
    [PXDBCalced(typeof (Mult<decimal_1, APTranPostGL.turnRGOLAmt>), typeof (Decimal))]
    public virtual Decimal? RGOLAmt { get; set; }

    /// <summary>Type of the original (source) document.</summary>
    [PXDBString(3, IsFixed = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [APDocType.List]
    [PXUIField(DisplayName = "Orig. Doc. Type")]
    public virtual string OrigDocType { get; set; }

    /// <summary>Reference number of the original (source) document.</summary>
    [PXDBString(15, IsUnicode = true, InputMask = "", BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Orig. Ref. Nbr.")]
    public virtual string OrigRefNbr { get; set; }

    [PXDBString(30, IsUnicode = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Vendor Invoice Nbr./Payment Nbr.")]
    public virtual string ExtRefNbr { get; set; }

    [PXDBString(10, IsUnicode = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Payment Method")]
    public virtual string PaymentMethodID { get; set; }

    [PXCurrency(typeof (APDocumentEnq.APDocumentResult.curyInfoID), typeof (APDocumentEnq.APDocumentResult.begBalance))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Currency Period Beg. Balance")]
    [PXDBCalced(typeof (Switch<Case<Where<APDocumentEnq.APDocumentResult.released, Equal<False>, And<APDocumentEnq.APDocumentResult.prebooked, Equal<False>, And<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<False>, And<APDocumentEnq.APDocumentResult.finPeriodID, Less<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>>>, APDocumentEnq.APDocumentResult.curyOrigDocAmt, Case<Where<APDocumentEnq.APDocumentResult.released, Equal<False>, And<APDocumentEnq.APDocumentResult.prebooked, Equal<False>, And<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<True>, And<APDocumentEnq.APDocumentResult.tranPeriodID, Less<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>>>, APDocumentEnq.APDocumentResult.curyOrigDocAmt, Case<Where<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<True>, And<APTranPostGL.tranPeriodID, Less<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>, APTranPostGL.curyBalanceAmt, Case<Where<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<False>, And<APTranPostGL.finPeriodID, Less<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>, APTranPostGL.curyBalanceAmt>>>>, decimal0>), typeof (Decimal))]
    public virtual Decimal? CuryBegBalance { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Period Beg. Balance")]
    [PXDBCalced(typeof (Switch<Case<Where<APDocumentEnq.APDocumentResult.released, Equal<False>, And<APDocumentEnq.APDocumentResult.prebooked, Equal<False>, And<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<False>, And<APDocumentEnq.APDocumentResult.finPeriodID, Less<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>>>, APDocumentEnq.APDocumentResult.origDocAmt, Case<Where<APDocumentEnq.APDocumentResult.released, Equal<False>, And<APDocumentEnq.APDocumentResult.prebooked, Equal<False>, And<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<True>, And<APDocumentEnq.APDocumentResult.tranPeriodID, Less<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>>>, APDocumentEnq.APDocumentResult.origDocAmt, Case<Where<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<True>, And<APTranPostGL.tranPeriodID, Less<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>, APTranPostGL.balanceAmt, Case<Where<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<False>, And<APTranPostGL.finPeriodID, Less<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>, APTranPostGL.balanceAmt>>>>, decimal0>), typeof (Decimal))]
    public virtual Decimal? BegBalance { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (APDocumentEnq.APDocumentResult.curyInfoID), typeof (APDocumentEnq.APDocumentResult.discActTaken))]
    [PXUIField(DisplayName = "Currency Cash Discount Taken")]
    [PXDBCalced(typeof (IsNull<APTranPostGL.curyTurnDiscAmt, decimal0>), typeof (Decimal))]
    public virtual Decimal? CuryDiscActTaken { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Cash Discount Taken")]
    [PXDBCalced(typeof (IsNull<APTranPostGL.turnDiscAmt, decimal0>), typeof (Decimal))]
    public virtual Decimal? DiscActTaken { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (APDocumentEnq.APDocumentResult.curyInfoID), typeof (APDocumentEnq.APDocumentResult.taxWheld))]
    [PXUIField(DisplayName = "Currency Tax Withheld")]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlOperand<APTranPostGL.type, IBqlString>.IsNotEqual<APTranPost.type.application>>, decimal0>, IsNull<APTranPostGL.curyTurnWhTaxAmt, decimal0>>), typeof (Decimal))]
    public virtual Decimal? CuryTaxWheld { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Tax Withheld")]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlOperand<APTranPostGL.type, IBqlString>.IsNotEqual<APTranPost.type.application>>, decimal0>, IsNull<APTranPostGL.turnWhTaxAmt, decimal0>>), typeof (Decimal))]
    public virtual Decimal? TaxWheld { get; set; }

    /// <summary>
    /// Expected GL turnover for the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "AP Turnover")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? APTurnover { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<True>, And<APTranPostGL.tranPeriodID, Equal<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>, decimal1, Case<Where<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<False>, And<APTranPostGL.finPeriodID, Equal<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>, decimal1>>, decimal0>), typeof (Decimal))]
    public virtual Decimal? Turn { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (APDocumentEnq.APDocumentResult.curyInfoID), typeof (APDocumentEnq.APDocumentResult.docBal), BaseCalc = false)]
    [PXUIField(DisplayName = "Currency Balance")]
    [PXDBCalced(typeof (Switch<Case<Where<APDocumentEnq.APDocumentResult.released, Equal<False>, And<APDocumentEnq.APDocumentResult.prebooked, Equal<False>>>, APDocumentEnq.APDocumentResult.curyOrigDocAmt>, APTranPostGL.curyBalanceAmt>), typeof (Decimal))]
    public virtual Decimal? CuryDocBal { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Balance")]
    [PXDBCalced(typeof (Switch<Case<Where<APDocumentEnq.APDocumentResult.released, Equal<False>, And<APDocumentEnq.APDocumentResult.prebooked, Equal<False>>>, APDocumentEnq.APDocumentResult.origDocAmt>, APTranPostGL.balanceAmt>), typeof (Decimal))]
    public virtual Decimal? DocBal { get; set; }

    [PXCurrency(typeof (APDocumentEnq.APDocumentResult.curyInfoID), typeof (APDocumentEnq.APDocumentResult.retainageUnreleasedAmt))]
    [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXDBCalced(typeof (IsNull<APTranPostGL.curyTurnRetainageAmt, decimal0>), typeof (Decimal))]
    public virtual Decimal? CuryRetainageUnreleasedAmt { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
    [PXDBCalced(typeof (IsNull<APTranPostGL.turnRetainageAmt, decimal0>), typeof (Decimal))]
    public virtual Decimal? RetainageUnreleasedAmt { get; set; }

    [PXDBString(1, IsFixed = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault("H")]
    [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
    [APDocStatus.List]
    public virtual string Status { get; set; }

    /// <summary>Description of the document.</summary>
    [PXDBString(512 /*0x0200*/, IsUnicode = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string DocDesc { get; set; }

    [PeriodID(null, null, null, true, BqlField = typeof (APTranPostGL.tranPeriodID))]
    public virtual string TranPostPeriodID { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the period, to which the <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.DocDate" /> belongs, but can be virtualn by user.
    /// </value>
    [APOpenPeriod(typeof (APDocumentEnq.APDocumentPeriodResult.docDate), typeof (APDocumentEnq.APDocumentPeriodResult.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (APDocumentEnq.APDocumentPeriodResult.tranPostPeriodID), IsHeader = true, BqlField = typeof (APTranPostGL.finPeriodID))]
    public virtual string FinPostPeriodID { get; set; }

    [Account(typeof (APDocumentEnq.APDocumentPeriodResult.branchID), IsDBField = false, DisplayName = "Account", BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlOperand<APDocumentEnq.APDocumentResult.docType, IBqlString>.IsEqual<APDocType.prepaymentInvoice>>, APTranPostGL.accountID>, APDocumentEnq.APDocumentResult.aPAccountID>), typeof (int?))]
    public virtual int? AccountID { get; set; }

    [SubAccount(typeof (APDocumentEnq.APDocumentPeriodResult.accountID), IsDBField = false, DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "Subaccount", Visibility = PXUIVisibility.Visible, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlOperand<APDocumentEnq.APDocumentResult.docType, IBqlString>.IsEqual<APDocType.prepaymentInvoice>>, APTranPostGL.subID>, APDocumentEnq.APDocumentResult.aPSubID>), typeof (int?))]
    public virtual int? SubID { get; set; }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.docType>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.refNbr>
    {
    }

    public abstract class suppliedByVendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.suppliedByVendorID>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.curyID>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.branchID>
    {
    }

    public abstract class docDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.docDate>
    {
    }

    public abstract class tranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.tranPeriodID>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.finPeriodID>
    {
    }

    public abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.vendorID>
    {
    }

    public abstract class aPAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.aPAccountID>
    {
    }

    public abstract class aPSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.aPSubID>
    {
    }

    public abstract class isMigratedRecord : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.isMigratedRecord>
    {
    }

    public abstract class installmentCntr : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.installmentCntr>
    {
    }

    public abstract class batchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.batchNbr>
    {
    }

    public abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.curyInfoID>
    {
    }

    public abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.released>
    {
    }

    public abstract class prebooked : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.prebooked>
    {
    }

    public abstract class openDoc : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.openDoc>
    {
    }

    public abstract class hold : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.hold>
    {
    }

    public abstract class scheduled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.scheduled>
    {
    }

    public abstract class voided : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.voided>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.noteID>
    {
    }

    public abstract class closedDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.closedDate>
    {
    }

    public abstract class closedFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.closedFinPeriodID>
    {
    }

    public abstract class closedTranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.closedTranPeriodID>
    {
    }

    public abstract class isRetainageDocument : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.isRetainageDocument>
    {
    }

    public abstract class curyOrigDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.curyOrigDocAmt>
    {
    }

    public abstract class origDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.origDocAmt>
    {
    }

    public abstract class curyRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.curyRetainageTotal>
    {
    }

    public abstract class retainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.retainageTotal>
    {
    }

    public abstract class curyOrigDocAmtWithRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.curyOrigDocAmtWithRetainageTotal>
    {
    }

    public abstract class origDocAmtWithRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.origDocAmtWithRetainageTotal>
    {
    }

    public abstract class rGOLAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.rGOLAmt>
    {
    }

    public abstract class origDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.origDocType>
    {
    }

    public abstract class origRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.origRefNbr>
    {
    }

    public abstract class extRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.extRefNbr>
    {
    }

    public abstract class paymentMethodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.paymentMethodID>
    {
    }

    public abstract class curyBegBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.curyBegBalance>
    {
    }

    public abstract class begBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.begBalance>
    {
    }

    public abstract class curyDiscActTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.curyDiscActTaken>
    {
    }

    public abstract class discActTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.discActTaken>
    {
    }

    public abstract class curyTaxWheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.curyTaxWheld>
    {
    }

    public abstract class taxWheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.taxWheld>
    {
    }

    public abstract class aPTurnover : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.aPTurnover>
    {
    }

    public abstract class turn : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.turn>
    {
    }

    public abstract class curyDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.curyDocBal>
    {
    }

    public abstract class docBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.docBal>
    {
    }

    public abstract class curyRetainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.curyRetainageUnreleasedAmt>
    {
    }

    public abstract class retainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.retainageUnreleasedAmt>
    {
    }

    public abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.status>
    {
    }

    public abstract class docDesc : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.docDesc>
    {
    }

    public abstract class tranPostPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.tranPostPeriodID>
    {
    }

    public abstract class finPostPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.finPostPeriodID>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.accountID>
    {
    }

    public abstract class subID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPeriodResult.subID>
    {
    }
  }

  [PXProjection(typeof (Select2<APDocumentEnq.APDocumentResult, LeftJoin<APTranPostGL, On<APTranPostGL.docType, Equal<APDocType.prepaymentInvoice>, And<APTranPostGL.docType, Equal<APDocumentEnq.APDocumentResult.docType>, And<APTranPostGL.refNbr, Equal<APDocumentEnq.APDocumentResult.refNbr>>>>>>))]
  [PXHidden]
  public class APDocumentPPIPeriodResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(3, IsKey = true, IsFixed = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault]
    [APDocType.List]
    [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = true, TabOrder = 0)]
    public virtual string DocType { get; set; }

    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault]
    [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 1)]
    [PXSelector(typeof (Search<APDocumentEnq.APDocumentResult.refNbr>), Filterable = true)]
    public virtual string RefNbr { get; set; }

    /// <summary>
    /// A reference to the <see cref="T:PX.Objects.AP.Vendor" />.
    /// </summary>
    /// <value>
    /// An integer identifier of the vendor that supplied the goods.
    /// </value>
    [Vendor(DisplayName = "Supplied-by Vendor", DescriptionField = typeof (Vendor.acctName), FieldClass = "VendorRelations", CacheGlobal = true, Filterable = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    public virtual int? SuppliedByVendorID { get; set; }

    /// <summary>
    /// Code of the <see cref="T:PX.Objects.CM.Currency">Currency</see> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">company's base currency</see>.
    /// </value>
    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible)]
    [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string CuryID { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.GL.Branch">Branch</see>, to which the document belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID">Branch.BranchID</see> field.
    /// </value>
    [Branch(null, null, true, true, true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    public virtual int? BranchID { get; set; }

    /// <summary>Date of the document.</summary>
    [PXDBDate(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual System.DateTime? DocDate { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Determined by the <see cref="P:PX.Objects.AP.APDocumentEnq.APDocumentResult.DocDate">date of the document</see>. Unlike <see cref="P:PX.Objects.AP.APDocumentEnq.APDocumentResult.FinPeriodID" />
    /// the value of this field can't be overriden by user.
    /// </value>
    [PeriodID(null, null, null, true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Master Period")]
    public virtual string TranPeriodID { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the period, to which the <see cref="P:PX.Objects.AP.APDocumentEnq.APDocumentResult.DocDate" /> belongs, but can be overriden by user.
    /// </value>
    [APOpenPeriod(typeof (APDocumentEnq.APDocumentResult.docDate), typeof (APDocumentEnq.APDocumentResult.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (APDocumentEnq.APDocumentResult.tranPeriodID), IsHeader = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string FinPeriodID { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.AP.Vendor" />, whom the document belongs to.
    /// </summary>
    [VendorActive(Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof (Vendor.acctName), CacheGlobal = true, Filterable = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault]
    public virtual int? VendorID { get; set; }

    /// <summary>
    /// Identifier of the AP account, to which the document belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
    /// </value>
    [PXDefault]
    [Account(typeof (APDocumentEnq.APDocumentResult.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, PX.Data.And<Where<Current<GLSetup.ytdNetIncAccountID>, PX.Data.IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DisplayName = "AP Account", ControlAccountForModule = "AP", BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    public virtual int? APAccountID { get; set; }

    /// <summary>
    /// Identifier of the AP subaccount, to which the document belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
    /// </value>
    [PXDefault]
    [SubAccount(typeof (APDocumentEnq.APDocumentResult.aPAccountID), typeof (APDocumentEnq.APDocumentResult.branchID), true, DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "AP Subaccount", Visibility = PXUIVisibility.Visible, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    public virtual int? APSubID { get; set; }

    /// <summary>
    /// Specifies (if set to <c>true</c>) that the record has been created
    /// in migration mode without affecting GL module.
    /// </summary>
    [MigratedRecord(typeof (PX.Objects.AP.APSetup.migrationMode), BqlTable = typeof (APDocumentEnq.APRegister))]
    public virtual bool? IsMigratedRecord { get; set; }

    [PXDBShort(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    public virtual short? InstallmentCntr { get; set; }

    /// <summary>
    /// Number of the <see cref="T:PX.Objects.GL.Batch" />, generated for the document on release.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Batch.BatchNbr">Batch.BatchNbr</see> field.
    /// </value>
    [PXDBString(15, IsUnicode = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Batch Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
    [PXSelector(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAP>>>))]
    [BatchNbr(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAP>>>), IsMigratedRecordField = typeof (APDocumentEnq.APDocumentPPIPeriodResult.isMigratedRecord))]
    public virtual string BatchNbr { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the document.
    /// </summary>
    /// <value>
    /// Generated automatically. Corresponds to the <see cref="!:PX.Objects.CM.CurrencyInfo.CurrencyInfoID" /> field.
    /// </value>
    [PXDBLong(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [CurrencyInfo(ModuleCode = "AP")]
    public virtual long? CuryInfoID { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document was released.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Released", Visible = false)]
    public virtual bool? Released { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document was prebooked.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Prebooked")]
    public virtual bool? Prebooked { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document is open.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Open", Visible = false)]
    public virtual bool? OpenDoc { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document is on hold and thus cannot be released.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Hold", Visibility = PXUIVisibility.Visible)]
    [PXDefault(true, typeof (PX.Objects.AP.APSetup.holdEntry))]
    public virtual bool? Hold { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document is part of a <c>Schedule</c> and serves as a template for generating other documents according to it.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(false)]
    public virtual bool? Scheduled { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document was voided. In this case <see cref="!:VoidBatchNbr" /> field will hold the number of the voiding <see cref="T:PX.Objects.GL.Batch" />.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Void", Visible = false)]
    public virtual bool? Voided { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
    /// </value>
    [PXNote(DescriptionField = typeof (APDocumentEnq.APDocumentPPIPeriodResult.refNbr), BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    public virtual Guid? NoteID { get; set; }

    /// <summary>The date of the last application.</summary>
    [PXDBDate(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Closed Date", Visibility = PXUIVisibility.Invisible)]
    public virtual System.DateTime? ClosedDate { get; set; }

    /// <summary>
    /// The <see cref="!:PX.Objects.GL.FinancialPeriod">Financial Period</see>, in which the document was closed.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AP.APDocumentEnq.APDocumentPPIPeriodResult.FinPeriodID" /> field.
    /// </value>
    [FinPeriodID(null, typeof (APDocumentEnq.APDocumentResult.branchID), null, null, null, null, true, false, null, typeof (APDocumentEnq.APDocumentResult.closedTranPeriodID), null, true, true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Closed Period", Visibility = PXUIVisibility.Invisible)]
    public virtual string ClosedFinPeriodID { get; set; }

    /// <summary>
    /// The <see cref="!:PX.Objects.GL.FinancialPeriod">Financial Period</see>, in which the document was closed.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AP.APDocumentEnq.APDocumentPPIPeriodResult.TranPeriodID" /> field.
    /// </value>
    [PeriodID(null, null, null, true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Closed Master Period", Visibility = PXUIVisibility.Invisible)]
    public virtual string ClosedTranPeriodID { get; set; }

    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Retainage Bill", Enabled = false, FieldClass = "Retainage")]
    [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual bool? IsRetainageDocument { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (APDocumentEnq.APDocumentResult.curyInfoID), typeof (APDocumentEnq.APDocumentResult.origDocAmt), BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APTranPostGL.type, IBqlString>.IsEqual<APTranPost.type.origin>>>, APTranPostGL.curyTurnAmt, Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APDocumentEnq.APDocumentResult.released, IBqlBool>.IsEqual<True>>>, Null>>, APDocumentEnq.APDocumentResult.curyOrigDocAmt>), typeof (Decimal))]
    [PXUIField(DisplayName = "Currency Origin. Amount")]
    public virtual Decimal? CuryOrigDocAmt { get; set; }

    [PXBaseCury(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APTranPostGL.type, IBqlString>.IsEqual<APTranPost.type.origin>>>, APTranPostGL.turnAmt, Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APDocumentEnq.APDocumentResult.released, IBqlBool>.IsEqual<True>>>, Null>>, APDocumentEnq.APDocumentResult.origDocAmt>), typeof (Decimal))]
    [PXUIField(DisplayName = "Origin. Amount")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? OrigDocAmt { get; set; }

    [PXDBCurrency(typeof (APDocumentEnq.APDocumentPPIPeriodResult.curyInfoID), typeof (APDocumentEnq.APDocumentPPIPeriodResult.retainageTotal), BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual Decimal? CuryRetainageTotal { get; set; }

    [PXDBBaseCury(null, null, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
    public virtual Decimal? RetainageTotal { get; set; }

    [PXCury(typeof (APDocumentEnq.APDocumentPPIPeriodResult.curyID), BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APTranPostGL.type, IBqlString>.IsEqual<APTranPost.type.origin>>>, APTranPostGL.curyTurnAmt, Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APDocumentEnq.APDocumentResult.released, IBqlBool>.IsEqual<True>>>, Null>>, APDocumentEnq.APDocumentResult.curyOrigDocAmtWithRetainageTotal>), typeof (Decimal))]
    public virtual Decimal? CuryOrigDocAmtWithRetainageTotal { get; set; }

    [PXBaseCury(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APTranPostGL.type, IBqlString>.IsEqual<APTranPost.type.origin>>>, APTranPostGL.turnAmt, Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APDocumentEnq.APDocumentResult.released, IBqlBool>.IsEqual<True>>>, Null>>, APDocumentEnq.APDocumentResult.origDocAmtWithRetainageTotal>), typeof (Decimal))]
    public virtual Decimal? OrigDocAmtWithRetainageTotal { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "RGOL Amount")]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APTranPostGL.type, IBqlString>.IsEqual<APTranPost.type.application>>>, PX.Data.Minus<APTranPostGL.rGOLAmt>>, IsNull<APDocumentEnq.APDocumentResult.rGOLAmt, decimal0>>), typeof (Decimal))]
    public virtual Decimal? RGOLAmt { get; set; }

    /// <summary>Type of the original (source) document.</summary>
    [PXDBString(3, IsFixed = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [APDocType.List]
    [PXUIField(DisplayName = "Orig. Doc. Type")]
    public virtual string OrigDocType { get; set; }

    /// <summary>Reference number of the original (source) document.</summary>
    [PXDBString(15, IsUnicode = true, InputMask = "", BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Orig. Ref. Nbr.")]
    public virtual string OrigRefNbr { get; set; }

    [PXDBString(30, IsUnicode = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Vendor Invoice Nbr./Payment Nbr.")]
    public virtual string ExtRefNbr { get; set; }

    [PXDBString(10, IsUnicode = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Payment Method")]
    public virtual string PaymentMethodID { get; set; }

    [PXCurrency(typeof (APDocumentEnq.APDocumentResult.curyInfoID), typeof (APDocumentEnq.APDocumentResult.begBalance))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Currency Period Beg. Balance")]
    [PXDBCalced(typeof (Switch<Case<Where<APDocumentEnq.APDocumentResult.released, Equal<False>, And<APDocumentEnq.APDocumentResult.prebooked, Equal<False>, And<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<False>, And<APDocumentEnq.APDocumentResult.finPeriodID, Less<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>>>, APDocumentEnq.APDocumentResult.curyOrigDocAmt, Case<Where<APDocumentEnq.APDocumentResult.released, Equal<False>, And<APDocumentEnq.APDocumentResult.prebooked, Equal<False>, And<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<True>, And<APDocumentEnq.APDocumentResult.tranPeriodID, Less<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>>>, APDocumentEnq.APDocumentResult.curyOrigDocAmt, Case<Where<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<True>, And<APTranPostGL.tranPeriodID, Less<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>, APTranPostGL.curyBalanceAmt, Case<Where<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<False>, And<APTranPostGL.finPeriodID, Less<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>, APTranPostGL.curyBalanceAmt>>>>, decimal0>), typeof (Decimal))]
    public virtual Decimal? CuryBegBalance { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Period Beg. Balance")]
    [PXDBCalced(typeof (Switch<Case<Where<APDocumentEnq.APDocumentResult.released, Equal<False>, And<APDocumentEnq.APDocumentResult.prebooked, Equal<False>, And<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<False>, And<APDocumentEnq.APDocumentResult.finPeriodID, Less<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>>>, APDocumentEnq.APDocumentResult.origDocAmt, Case<Where<APDocumentEnq.APDocumentResult.released, Equal<False>, And<APDocumentEnq.APDocumentResult.prebooked, Equal<False>, And<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<True>, And<APDocumentEnq.APDocumentResult.tranPeriodID, Less<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>>>, APDocumentEnq.APDocumentResult.origDocAmt, Case<Where<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<True>, And<APTranPostGL.tranPeriodID, Less<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>, APTranPostGL.balanceAmt, Case<Where<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<False>, And<APTranPostGL.finPeriodID, Less<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>, APTranPostGL.balanceAmt>>>>, decimal0>), typeof (Decimal))]
    public virtual Decimal? BegBalance { get; set; }

    [PXCurrency(typeof (APDocumentEnq.APDocumentResult.curyInfoID), typeof (APDocumentEnq.APDocumentResult.discActTaken))]
    [PXUIField(DisplayName = "Currency Cash Discount Taken")]
    [PXDBCalced(typeof (BqlFunction<Mult<APTranPostGL.curyTurnDiscAmt, APTranPostGL.glSign>, IBqlDecimal>.IfNullThen<APDocumentEnq.APDocumentResult.curyDiscActTaken>), typeof (Decimal))]
    public virtual Decimal? CuryDiscActTaken { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "Cash Discount Taken")]
    [PXDBCalced(typeof (BqlFunction<Mult<APTranPostGL.turnDiscAmt, APTranPostGL.glSign>, IBqlDecimal>.IfNullThen<APDocumentEnq.APDocumentResult.discActTaken>), typeof (Decimal))]
    public virtual Decimal? DiscActTaken { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (APDocumentEnq.APDocumentResult.curyInfoID), typeof (APDocumentEnq.APDocumentResult.taxWheld))]
    [PXUIField(DisplayName = "Currency Tax Withheld")]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlOperand<APTranPostGL.type, IBqlString>.IsNotEqual<APTranPost.type.application>>, decimal0>, IsNull<APTranPostGL.curyTurnWhTaxAmt, decimal0>>), typeof (Decimal))]
    public virtual Decimal? CuryTaxWheld { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Tax Withheld")]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlOperand<APTranPostGL.type, IBqlString>.IsNotEqual<APTranPost.type.application>>, decimal0>, IsNull<APTranPostGL.turnWhTaxAmt, decimal0>>), typeof (Decimal))]
    public virtual Decimal? TaxWheld { get; set; }

    /// <summary>
    /// Expected GL turnover for the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "AP Turnover")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? APTurnover { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<True>, And<APTranPostGL.tranPeriodID, Equal<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>, decimal1, Case<Where<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<False>, And<APTranPostGL.finPeriodID, Equal<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>, decimal1>>, decimal0>), typeof (Decimal))]
    public virtual Decimal? Turn { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (APDocumentEnq.APDocumentResult.curyInfoID), typeof (APDocumentEnq.APDocumentResult.docBal), BaseCalc = false)]
    [PXUIField(DisplayName = "Currency Balance")]
    [PXDBCalced(typeof (Switch<Case<Where<APDocumentEnq.APDocumentResult.released, Equal<False>, And<APDocumentEnq.APDocumentResult.prebooked, Equal<False>>>, APDocumentEnq.APDocumentResult.curyOrigDocAmt, Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APDocumentEnq.APDocumentResult.released, IBqlBool>.IsEqual<True>>>, APTranPostGL.curyBalanceAmt>>, APDocumentEnq.APDocumentResult.curyDocBal>), typeof (Decimal))]
    public virtual Decimal? CuryDocBal { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Balance")]
    [PXDBCalced(typeof (Switch<Case<Where<APDocumentEnq.APDocumentResult.released, Equal<False>, And<APDocumentEnq.APDocumentResult.prebooked, Equal<False>>>, APDocumentEnq.APDocumentResult.origDocAmt, Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APDocumentEnq.APDocumentResult.released, IBqlBool>.IsEqual<True>>>, APTranPostGL.balanceAmt>>, APDocumentEnq.APDocumentResult.docBal>), typeof (Decimal))]
    public virtual Decimal? DocBal { get; set; }

    [PXCurrency(typeof (APDocumentEnq.APDocumentResult.curyInfoID), typeof (APDocumentEnq.APDocumentResult.retainageUnreleasedAmt))]
    [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APDocumentEnq.APDocumentResult.released, IBqlBool>.IsEqual<True>>>, BqlOperand<APTranPostGL.curyRetainageUnreleasedAmt, IBqlDecimal>.Multiply<APTranPostGL.balanceSign>>, APDocumentEnq.APDocumentResult.curyRetainageUnreleasedAmt>), typeof (Decimal))]
    public virtual Decimal? CuryRetainageUnreleasedAmt { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APDocumentEnq.APDocumentResult.released, IBqlBool>.IsEqual<True>>>, BqlOperand<APTranPostGL.retainageUnreleasedAmt, IBqlDecimal>.Multiply<APTranPostGL.balanceSign>>, APDocumentEnq.APDocumentResult.retainageUnreleasedAmt>), typeof (Decimal))]
    public virtual Decimal? RetainageUnreleasedAmt { get; set; }

    [PXDBString(1, IsFixed = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault("H")]
    [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
    [APDocStatus.List]
    public virtual string Status { get; set; }

    /// <summary>Description of the document.</summary>
    [PXDBString(512 /*0x0200*/, IsUnicode = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string DocDesc { get; set; }

    [PeriodID(null, null, null, true, BqlField = typeof (APTranPostGL.tranPeriodID))]
    public virtual string TranPostPeriodID { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the period, to which the <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.DocDate" /> belongs, but can be virtualn by user.
    /// </value>
    [APOpenPeriod(typeof (APDocumentEnq.APDocumentPPIPeriodResult.docDate), typeof (APDocumentEnq.APDocumentPPIPeriodResult.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (APDocumentEnq.APDocumentPPIPeriodResult.tranPostPeriodID), IsHeader = true, BqlField = typeof (APTranPostGL.finPeriodID))]
    public virtual string FinPostPeriodID { get; set; }

    [Account(typeof (APDocumentEnq.APDocumentPPIPeriodResult.branchID), IsDBField = false, DisplayName = "Account", BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APDocumentEnq.APDocumentResult.released, IBqlBool>.IsEqual<True>>>, APTranPostGL.accountID>, APDocumentEnq.APDocumentResult.aPAccountID>), typeof (int?))]
    public virtual int? AccountID { get; set; }

    [SubAccount(typeof (APDocumentEnq.APDocumentPPIPeriodResult.accountID), IsDBField = false, DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "Subaccount", Visibility = PXUIVisibility.Visible, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APDocumentEnq.APDocumentResult.docType, Equal<APDocType.prepaymentInvoice>>>>>.And<BqlOperand<APDocumentEnq.APDocumentResult.released, IBqlBool>.IsEqual<True>>>, APTranPostGL.subID>, APDocumentEnq.APDocumentResult.aPSubID>), typeof (int?))]
    public virtual int? SubID { get; set; }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.docType>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.refNbr>
    {
    }

    public abstract class suppliedByVendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.suppliedByVendorID>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.curyID>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.branchID>
    {
    }

    public abstract class docDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.docDate>
    {
    }

    public abstract class tranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.tranPeriodID>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.finPeriodID>
    {
    }

    public abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.vendorID>
    {
    }

    public abstract class aPAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.aPAccountID>
    {
    }

    public abstract class aPSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.aPSubID>
    {
    }

    public abstract class isMigratedRecord : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.isMigratedRecord>
    {
    }

    public abstract class installmentCntr : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.installmentCntr>
    {
    }

    public abstract class batchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.batchNbr>
    {
    }

    public abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.curyInfoID>
    {
    }

    public abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.released>
    {
    }

    public abstract class prebooked : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.prebooked>
    {
    }

    public abstract class openDoc : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.openDoc>
    {
    }

    public abstract class hold : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.hold>
    {
    }

    public abstract class scheduled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.scheduled>
    {
    }

    public abstract class voided : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.voided>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.noteID>
    {
    }

    public abstract class closedDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.closedDate>
    {
    }

    public abstract class closedFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.closedFinPeriodID>
    {
    }

    public abstract class closedTranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.closedTranPeriodID>
    {
    }

    public abstract class isRetainageDocument : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.isRetainageDocument>
    {
    }

    public abstract class curyOrigDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.curyOrigDocAmt>
    {
    }

    public abstract class origDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.origDocAmt>
    {
    }

    public abstract class curyRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.curyRetainageTotal>
    {
    }

    public abstract class retainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.retainageTotal>
    {
    }

    public abstract class curyOrigDocAmtWithRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.curyOrigDocAmtWithRetainageTotal>
    {
    }

    public abstract class origDocAmtWithRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.origDocAmtWithRetainageTotal>
    {
    }

    public abstract class rGOLAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.rGOLAmt>
    {
    }

    public abstract class origDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.origDocType>
    {
    }

    public abstract class origRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.origRefNbr>
    {
    }

    public abstract class extRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.extRefNbr>
    {
    }

    public abstract class paymentMethodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.paymentMethodID>
    {
    }

    public abstract class curyBegBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.curyBegBalance>
    {
    }

    public abstract class begBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.begBalance>
    {
    }

    public abstract class curyDiscActTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.curyDiscActTaken>
    {
    }

    public abstract class discActTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.discActTaken>
    {
    }

    public abstract class curyTaxWheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.curyTaxWheld>
    {
    }

    public abstract class taxWheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.taxWheld>
    {
    }

    public abstract class aPTurnover : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.aPTurnover>
    {
    }

    public abstract class turn : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.turn>
    {
    }

    public abstract class curyDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.curyDocBal>
    {
    }

    public abstract class docBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.docBal>
    {
    }

    public abstract class curyRetainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.curyRetainageUnreleasedAmt>
    {
    }

    public abstract class retainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.retainageUnreleasedAmt>
    {
    }

    public abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.status>
    {
    }

    public abstract class docDesc : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.docDesc>
    {
    }

    public abstract class tranPostPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.tranPostPeriodID>
    {
    }

    public abstract class finPostPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.finPostPeriodID>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.accountID>
    {
    }

    public abstract class subID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.APDocumentPPIPeriodResult.subID>
    {
    }
  }

  [PXProjection(typeof (Select2<APDocumentEnq.APDocumentResult, LeftJoin<APTranPostGL, On<APTranPostGL.tranType, Equal<APDocumentEnq.APDocumentResult.docType>, And<APTranPostGL.tranRefNbr, Equal<APDocumentEnq.APDocumentResult.refNbr>>>>>))]
  [PXHidden]
  public class GLDocumentPeriodResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(3, IsKey = true, IsFixed = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault]
    [APDocType.List]
    [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = true, TabOrder = 0)]
    public virtual string DocType { get; set; }

    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault]
    [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 1)]
    [PXSelector(typeof (Search<APDocumentEnq.APDocumentResult.refNbr>), Filterable = true)]
    public virtual string RefNbr { get; set; }

    /// <summary>
    /// Code of the <see cref="T:PX.Objects.CM.Currency">Currency</see> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">company's base currency</see>.
    /// </value>
    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible)]
    [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string CuryID { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.GL.Branch">Branch</see>, to which the document belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID">Branch.BranchID</see> field.
    /// </value>
    [Branch(null, null, true, true, true, BqlTable = typeof (APTranPostGL))]
    public virtual int? BranchID { get; set; }

    /// <summary>Date of the document.</summary>
    [PXDBDate(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Date", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual System.DateTime? DocDate { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Determined by the <see cref="P:PX.Objects.AP.APDocumentEnq.APDocumentResult.DocDate">date of the document</see>. Unlike <see cref="P:PX.Objects.AP.APDocumentEnq.APDocumentResult.FinPeriodID" />
    /// the value of this field can't be overriden by user.
    /// </value>
    [PeriodID(null, null, null, true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Master Period")]
    public virtual string TranPeriodID { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the period, to which the <see cref="P:PX.Objects.AP.APDocumentEnq.APDocumentResult.DocDate" /> belongs, but can be overriden by user.
    /// </value>
    [APOpenPeriod(typeof (APDocumentEnq.APDocumentResult.docDate), typeof (APDocumentEnq.APDocumentResult.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (APDocumentEnq.APDocumentResult.tranPeriodID), IsHeader = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Post Period", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string FinPeriodID { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.AP.Vendor" />, whom the document belongs to.
    /// </summary>
    [VendorActive(Visibility = PXUIVisibility.SelectorVisible, DescriptionField = typeof (Vendor.acctName), CacheGlobal = true, Filterable = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault]
    public virtual int? VendorID { get; set; }

    /// <summary>
    /// Identifier of the AP account, to which the document belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
    /// </value>
    [PXDefault]
    [Account(typeof (APDocumentEnq.APDocumentResult.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, PX.Data.And<Where<Current<GLSetup.ytdNetIncAccountID>, PX.Data.IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DisplayName = "AP Account", ControlAccountForModule = "AP", BqlField = typeof (APTranPostGL.accountID))]
    public virtual int? APAccountID { get; set; }

    /// <summary>
    /// Identifier of the AP subaccount, to which the document belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
    /// </value>
    [PXDefault]
    [SubAccount(typeof (APDocumentEnq.APDocumentResult.aPAccountID), typeof (APDocumentEnq.APDocumentResult.branchID), true, DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "AP Subaccount", Visibility = PXUIVisibility.Visible, BqlField = typeof (APTranPostGL.subID))]
    public virtual int? APSubID { get; set; }

    /// <summary>
    /// Specifies (if set to <c>true</c>) that the record has been created
    /// in migration mode without affecting GL module.
    /// </summary>
    [MigratedRecord(typeof (PX.Objects.AP.APSetup.migrationMode), BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    public virtual bool? IsMigratedRecord { get; set; }

    /// <summary>
    /// The counter of <see cref="!:TermsInstallment">installments</see> associated with the document.
    /// </summary>
    [PXDBShort(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    public virtual short? InstallmentCntr { get; set; }

    /// <summary>
    /// Number of the <see cref="T:PX.Objects.GL.Batch" />, generated for the document on release.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Batch.BatchNbr">Batch.BatchNbr</see> field.
    /// </value>
    [PXDBString(15, IsUnicode = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Batch Nbr.", Visibility = PXUIVisibility.Visible, Enabled = false)]
    [PXSelector(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAP>>>))]
    [BatchNbr(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleAP>>>), IsMigratedRecordField = typeof (APDocumentEnq.GLDocumentPeriodResult.isMigratedRecord))]
    public virtual string BatchNbr { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the document.
    /// </summary>
    /// <value>
    /// Generated automatically. Corresponds to the <see cref="!:PX.Objects.CM.CurrencyInfo.CurrencyInfoID" /> field.
    /// </value>
    [PXDBLong(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [CurrencyInfo(ModuleCode = "AP")]
    public virtual long? CuryInfoID { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document was released.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Released", Visible = false)]
    public virtual bool? Released { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document was prebooked.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Prebooked")]
    public virtual bool? Prebooked { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document is open.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Open", Visible = false)]
    public virtual bool? OpenDoc { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document is on hold and thus cannot be released.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Hold", Visibility = PXUIVisibility.Visible)]
    [PXDefault(true, typeof (PX.Objects.AP.APSetup.holdEntry))]
    public virtual bool? Hold { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document is part of a <c>Schedule</c> and serves as a template for generating other documents according to it.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(false)]
    public virtual bool? Scheduled { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document was voided. In this case <see cref="!:VoidBatchNbr" /> field will hold the number of the voiding <see cref="T:PX.Objects.GL.Batch" />.
    /// </summary>
    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Void", Visible = false)]
    public virtual bool? Voided { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
    /// </value>
    [PXNote(DescriptionField = typeof (APDocumentEnq.GLDocumentPeriodResult.refNbr), BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    public virtual Guid? NoteID { get; set; }

    /// <summary>The date of the last application.</summary>
    [PXDBDate(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Closed Date", Visibility = PXUIVisibility.Invisible)]
    public virtual System.DateTime? ClosedDate { get; set; }

    /// <summary>
    /// The <see cref="!:PX.Objects.GL.FinancialPeriod">Financial Period</see>, in which the document was closed.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AP.APDocumentEnq.GLDocumentPeriodResult.FinPeriodID" /> field.
    /// </value>
    [FinPeriodID(null, typeof (APDocumentEnq.APDocumentResult.branchID), null, null, null, null, true, false, null, typeof (APDocumentEnq.APDocumentResult.closedTranPeriodID), null, true, true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Closed Period", Visibility = PXUIVisibility.Invisible)]
    public virtual string ClosedFinPeriodID { get; set; }

    /// <summary>
    /// The <see cref="!:PX.Objects.GL.FinancialPeriod">Financial Period</see>, in which the document was closed.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AP.APDocumentEnq.GLDocumentPeriodResult.TranPeriodID" /> field.
    /// </value>
    [PeriodID(null, null, null, true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Closed Master Period", Visibility = PXUIVisibility.Invisible)]
    public virtual string ClosedTranPeriodID { get; set; }

    [PXDBBool(BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Retainage Bill", Enabled = false, FieldClass = "Retainage")]
    [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual bool? IsRetainageDocument { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (APDocumentEnq.APDocumentResult.curyInfoID), typeof (APDocumentEnq.APDocumentResult.origDocAmt), BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Currency Origin. Amount")]
    public virtual Decimal? CuryOrigDocAmt { get; set; }

    [PXDBBaseCury(null, null, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Origin. Amount")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? OrigDocAmt { get; set; }

    [PXDBCurrency(typeof (APDocumentEnq.GLDocumentPeriodResult.curyInfoID), typeof (APDocumentEnq.GLDocumentPeriodResult.retainageTotal), BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual Decimal? CuryRetainageTotal { get; set; }

    [PXDBBaseCury(null, null, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
    public virtual Decimal? RetainageTotal { get; set; }

    [PXDBCury(typeof (APDocumentEnq.GLDocumentPeriodResult.curyID), BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    public virtual Decimal? CuryOrigDocAmtWithRetainageTotal { get; set; }

    [PXDBBaseCury(null, null, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
    public virtual Decimal? OrigDocAmtWithRetainageTotal { get; set; }

    [PXDBBaseCury(null, null, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "RGOL Amount")]
    public virtual Decimal? RGOLAmt { get; set; }

    /// <summary>Type of the original (source) document.</summary>
    [PXDBString(3, IsFixed = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [APDocType.List]
    [PXUIField(DisplayName = "Orig. Doc. Type")]
    public virtual string OrigDocType { get; set; }

    /// <summary>Reference number of the original (source) document.</summary>
    [PXDBString(15, IsUnicode = true, InputMask = "", BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Orig. Ref. Nbr.")]
    public virtual string OrigRefNbr { get; set; }

    [PXDBString(30, IsUnicode = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Vendor Invoice Nbr./Payment Nbr.")]
    public virtual string ExtRefNbr { get; set; }

    [PXDBString(10, IsUnicode = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Payment Method")]
    public virtual string PaymentMethodID { get; set; }

    [PXCurrency(typeof (APDocumentEnq.APDocumentResult.curyInfoID), typeof (APDocumentEnq.APDocumentResult.begBalance))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Currency Period Beg. Balance")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? CuryBegBalance { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Period Beg. Balance")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? BegBalance { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (APDocumentEnq.APDocumentResult.curyInfoID), typeof (APDocumentEnq.APDocumentResult.discActTaken))]
    [PXUIField(DisplayName = "Currency Cash Discount Taken")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? CuryDiscActTaken { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "Cash Discount Taken")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? DiscActTaken { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (APDocumentEnq.APDocumentResult.curyInfoID), typeof (APDocumentEnq.APDocumentResult.taxWheld))]
    [PXUIField(DisplayName = "Currency Tax Withheld")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? CuryTaxWheld { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Tax Withheld")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? TaxWheld { get; set; }

    /// <summary>
    /// Expected GL turnover for the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "AP Turnover")]
    [PXDBCalced(typeof (Switch<Case<Where<CurrentValue<APDocumentEnq.APDocumentFilter.branchID>, NotEqual<APTranPostGL.branchID>>, decimal0, Case<Where<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<True>, And<APTranPostGL.tranPeriodID, Equal<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>, Sub<APTranPostGL.creditAPAmt, APTranPostGL.debitAPAmt>, Case<Where<CurrentValue<APDocumentEnq.APDocumentFilter.useMasterCalendar>, Equal<False>, And<APTranPostGL.finPeriodID, Equal<CurrentValue<APDocumentEnq.APDocumentFilter.finPeriodID>>>>, Sub<APTranPostGL.creditAPAmt, APTranPostGL.debitAPAmt>>>>, decimal0>), typeof (Decimal))]
    public virtual Decimal? APTurnover { get; set; }

    /// <summary>
    /// Expected GL turnover for the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXBaseCury]
    [PXUIField(DisplayName = "GL Turnover")]
    public virtual Decimal? GLTurnover { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (APDocumentEnq.APDocumentResult.curyInfoID), typeof (APDocumentEnq.APDocumentResult.docBal), BaseCalc = false)]
    [PXUIField(DisplayName = "Currency Balance")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? CuryDocBal { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Balance")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? DocBal { get; set; }

    [PXCurrency(typeof (APDocumentEnq.APDocumentResult.curyInfoID), typeof (APDocumentEnq.APDocumentResult.retainageUnreleasedAmt))]
    [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? CuryRetainageUnreleasedAmt { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? RetainageUnreleasedAmt { get; set; }

    [PXDBString(1, IsFixed = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDefault("H")]
    [PXUIField(DisplayName = "Status", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
    [APDocStatus.List]
    public virtual string Status { get; set; }

    /// <summary>Description of the document.</summary>
    [PXDBString(512 /*0x0200*/, IsUnicode = true, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string DocDesc { get; set; }

    [PeriodID(null, null, null, true, BqlField = typeof (APTranPostGL.tranPeriodID))]
    public virtual string TranPostPeriodID { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the period, to which the <see cref="P:PX.Objects.AP.APDocumentEnq.APRegister.DocDate" /> belongs, but can be virtualn by user.
    /// </value>
    [APOpenPeriod(typeof (APDocumentEnq.GLDocumentPeriodResult.docDate), typeof (APDocumentEnq.GLDocumentPeriodResult.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (APDocumentEnq.GLDocumentPeriodResult.tranPostPeriodID), IsHeader = true, BqlField = typeof (APTranPostGL.finPeriodID))]
    public virtual string FinPostPeriodID { get; set; }

    [Account(typeof (APDocumentEnq.GLDocumentPeriodResult.branchID), IsDBField = false, DisplayName = "Account", BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlOperand<APDocumentEnq.APDocumentResult.docType, IBqlString>.IsEqual<APDocType.prepaymentInvoice>>, APTranPostGL.accountID>, APDocumentEnq.APDocumentResult.aPAccountID>), typeof (int?))]
    public virtual int? AccountID { get; set; }

    [SubAccount(typeof (APDocumentEnq.GLDocumentPeriodResult.accountID), IsDBField = false, DescriptionField = typeof (PX.Objects.GL.Sub.description), DisplayName = "Subaccount", Visibility = PXUIVisibility.Visible, BqlTable = typeof (APDocumentEnq.APDocumentResult))]
    [PXDBCalced(typeof (Switch<Case<PX.Data.Where<BqlOperand<APDocumentEnq.APDocumentResult.docType, IBqlString>.IsEqual<APDocType.prepaymentInvoice>>, APTranPostGL.subID>, APDocumentEnq.APDocumentResult.aPSubID>), typeof (int?))]
    public virtual int? SubID { get; set; }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.docType>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.refNbr>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.curyID>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.branchID>
    {
    }

    public abstract class docDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.docDate>
    {
    }

    public abstract class tranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.tranPeriodID>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.finPeriodID>
    {
    }

    public abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.vendorID>
    {
    }

    public abstract class aPAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.aPAccountID>
    {
    }

    public abstract class aPSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.aPSubID>
    {
    }

    public abstract class isMigratedRecord : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.isMigratedRecord>
    {
    }

    public abstract class installmentCntr : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.installmentCntr>
    {
    }

    public abstract class batchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.batchNbr>
    {
    }

    public abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.curyInfoID>
    {
    }

    public abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.released>
    {
    }

    public abstract class prebooked : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.prebooked>
    {
    }

    public abstract class openDoc : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.openDoc>
    {
    }

    public abstract class hold : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.hold>
    {
    }

    public abstract class scheduled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.scheduled>
    {
    }

    public abstract class voided : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.voided>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.noteID>
    {
    }

    public abstract class closedDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.closedDate>
    {
    }

    public abstract class closedFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.closedFinPeriodID>
    {
    }

    public abstract class closedTranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.closedTranPeriodID>
    {
    }

    public abstract class isRetainageDocument : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.isRetainageDocument>
    {
    }

    public abstract class curyOrigDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.curyOrigDocAmt>
    {
    }

    public abstract class origDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.origDocAmt>
    {
    }

    public abstract class curyRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.curyRetainageTotal>
    {
    }

    public abstract class retainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.retainageTotal>
    {
    }

    public abstract class curyOrigDocAmtWithRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.curyOrigDocAmtWithRetainageTotal>
    {
    }

    public abstract class origDocAmtWithRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.origDocAmtWithRetainageTotal>
    {
    }

    public abstract class rGOLAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.rGOLAmt>
    {
    }

    public abstract class origDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.origDocType>
    {
    }

    public abstract class origRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.origRefNbr>
    {
    }

    public abstract class extRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.extRefNbr>
    {
    }

    public abstract class paymentMethodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.paymentMethodID>
    {
    }

    public abstract class curyBegBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.curyBegBalance>
    {
    }

    public abstract class begBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.begBalance>
    {
    }

    public abstract class curyDiscActTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.curyDiscActTaken>
    {
    }

    public abstract class discActTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.discActTaken>
    {
    }

    public abstract class curyTaxWheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.curyTaxWheld>
    {
    }

    public abstract class taxWheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.taxWheld>
    {
    }

    public abstract class aPTurnover : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.aPTurnover>
    {
    }

    public abstract class gLTurnover : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.gLTurnover>
    {
    }

    public abstract class curyDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.curyDocBal>
    {
    }

    public abstract class docBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.docBal>
    {
    }

    public abstract class curyRetainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.curyRetainageUnreleasedAmt>
    {
    }

    public abstract class retainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.retainageUnreleasedAmt>
    {
    }

    public abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.status>
    {
    }

    public abstract class docDesc : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.docDesc>
    {
    }

    public abstract class tranPostPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.tranPostPeriodID>
    {
    }

    public abstract class finPostPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.finPostPeriodID>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.accountID>
    {
    }

    public abstract class subID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.GLDocumentPeriodResult.subID>
    {
    }
  }

  public class Ref
  {
    [PXHidden]
    public class APInvoice : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
    {
      /// <summary>
      /// The type of the document.
      /// This field is a part of the compound key of the document.
      /// </summary>
      /// <value>
      /// The field can have one of the values described in <see cref="T:PX.Objects.AP.APDocType.ListAttribute" />.
      /// </value>
      [PXDBString(3, IsKey = true, IsFixed = true)]
      [PXDefault]
      [APDocType.List]
      [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = true, TabOrder = 0)]
      public virtual string DocType { get; set; }

      /// <summary>
      /// The reference number of the document.
      /// This field is a part of the compound key of the document.
      /// </summary>
      [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
      [PXDefault]
      [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 1)]
      [PXSelector(typeof (Search<APDocumentEnq.APRegister.refNbr, Where<APDocumentEnq.APRegister.docType, Equal<Optional<APDocumentEnq.APRegister.docType>>>>), Filterable = true)]
      public virtual string RefNbr { get; set; }

      /// <summary>
      /// The original reference number or ID assigned by the customer to the customer document.
      /// </summary>
      [PXDBString(40, IsUnicode = true)]
      [PXUIField(DisplayName = "Customer Order", Visibility = PXUIVisibility.SelectorVisible, Required = false)]
      public virtual string InvoiceNbr { get; set; }

      /// <summary>
      /// The counter of <see cref="!:TermsInstallment">installments</see> associated with the document.
      /// </summary>
      [PXDBShort]
      public virtual short? InstallmentCntr { get; set; }

      /// <summary>
      /// A reference to the <see cref="T:PX.Objects.AP.Vendor" />.
      /// </summary>
      /// <value>
      /// An integer identifier of the vendor that supplied the goods.
      /// </value>
      [Vendor(DisplayName = "Supplied-By Vendor", DescriptionField = typeof (Vendor.acctName), FieldClass = "VendorRelations", CacheGlobal = true, Filterable = true, Required = true)]
      public virtual int? SuppliedByVendorID { get; set; }

      public abstract class docType : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        APDocumentEnq.Ref.APInvoice.docType>
      {
        public const int Length = 3;
      }

      public abstract class refNbr : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        APDocumentEnq.Ref.APInvoice.refNbr>
      {
      }

      public abstract class invoiceNbr : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        APDocumentEnq.Ref.APInvoice.invoiceNbr>
      {
      }

      public abstract class installmentCntr : 
        BqlType<
        #nullable enable
        IBqlShort, short>.Field<
        #nullable disable
        APDocumentEnq.Ref.APInvoice.installmentCntr>
      {
      }

      public abstract class suppliedByVendorID : 
        BqlType<
        #nullable enable
        IBqlInt, int>.Field<
        #nullable disable
        APDocumentEnq.Ref.APInvoice.suppliedByVendorID>
      {
      }
    }

    [PXHidden]
    public class APPayment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
    {
      /// <summary>
      /// The type of the document.
      /// This field is a part of the compound key of the document.
      /// </summary>
      /// <value>
      /// The field can have one of the values described in <see cref="T:PX.Objects.AP.APDocType.ListAttribute" />.
      /// </value>
      [PXDBString(3, IsKey = true, IsFixed = true)]
      [PXDefault]
      [APDocType.List]
      [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, Enabled = true, TabOrder = 0)]
      public virtual string DocType { get; set; }

      /// <summary>
      /// The reference number of the document.
      /// This field is a part of the compound key of the document.
      /// </summary>
      [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
      [PXDefault]
      [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.SelectorVisible, TabOrder = 1)]
      [PXSelector(typeof (Search<APDocumentEnq.APRegister.refNbr, Where<APDocumentEnq.APRegister.docType, Equal<Optional<APDocumentEnq.APRegister.docType>>>>), Filterable = true)]
      public virtual string RefNbr { get; set; }

      [PXDBString(10, IsUnicode = true)]
      [PXUIField(DisplayName = "Payment Method", Enabled = false)]
      public virtual string PaymentMethodID { get; set; }

      [PXDBString(40, IsUnicode = true)]
      [PXUIField(DisplayName = "Payment Ref.", Visibility = PXUIVisibility.SelectorVisible)]
      public virtual string ExtRefNbr { get; set; }

      public abstract class docType : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        APDocumentEnq.Ref.APPayment.docType>
      {
        public const int Length = 3;
      }

      public abstract class refNbr : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        APDocumentEnq.Ref.APPayment.refNbr>
      {
      }

      public abstract class paymentMethodID : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        APDocumentEnq.Ref.APPayment.paymentMethodID>
      {
      }

      public abstract class extRefNbr : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        APDocumentEnq.Ref.APPayment.extRefNbr>
      {
      }
    }
  }

  [PXHidden]
  [Serializable]
  public class GLTran : PX.Objects.GL.GLTran
  {
    [PXBaseCury]
    [PXDBCalced(typeof (Sub<APDocumentEnq.GLTran.creditAmt, APDocumentEnq.GLTran.debitAmt>), typeof (Decimal))]
    public virtual Decimal? GLTurnover { get; set; }

    public new abstract class branchID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APDocumentEnq.GLTran.branchID>
    {
    }

    public new abstract class module : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APDocumentEnq.GLTran.module>
    {
    }

    public new abstract class batchNbr : IBqlField, IBqlOperand
    {
    }

    public new abstract class lineNbr : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APDocumentEnq.GLTran.lineNbr>
    {
    }

    public new abstract class ledgerID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APDocumentEnq.GLTran.ledgerID>
    {
    }

    public new abstract class accountID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APDocumentEnq.GLTran.accountID>
    {
    }

    public new abstract class subID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APDocumentEnq.GLTran.subID>
    {
    }

    public new abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.GLTran.finPeriodID>
    {
    }

    public new abstract class tranPeriodID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.GLTran.tranPeriodID>
    {
    }

    public new abstract class creditAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLTran.creditAmt>
    {
    }

    public new abstract class debitAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLTran.debitAmt>
    {
    }

    public new abstract class posted : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    APDocumentEnq.GLTran.posted>
    {
    }

    public new abstract class tranType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APDocumentEnq.GLTran.tranType>
    {
    }

    public new abstract class refNbr : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APDocumentEnq.GLTran.refNbr>
    {
    }

    public new abstract class referenceID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APDocumentEnq.GLTran.referenceID>
    {
    }

    public abstract class gLTurnover : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APDocumentEnq.GLTran.gLTurnover>
    {
    }
  }
}
