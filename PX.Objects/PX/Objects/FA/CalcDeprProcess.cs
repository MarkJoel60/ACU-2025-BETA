// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.CalcDeprProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FA;

public class CalcDeprProcess : PXGraph<CalcDeprProcess>
{
  public PXCancel<BalanceFilter> Cancel;
  public PXFilter<BalanceFilter> Filter;
  [Obsolete("Will be removed in Acumatica 2019R1")]
  public PXAction<BalanceFilter> ViewAsset;
  public PXAction<BalanceFilter> ViewBook;
  public PXAction<BalanceFilter> ViewClass;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingJoinOrderBy<FABookBalance, BalanceFilter, InnerJoin<FixedAsset, On<FixedAsset.assetID, Equal<FABookBalance.assetID>>, InnerJoin<FADetails, On<FADetails.assetID, Equal<FABookBalance.assetID>>, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<FixedAsset.fAAccountID>>>>>, OrderBy<Asc<FABookBalance.assetID, Asc<FABookBalance.bookID>>>> Balances;
  public PXSetup<Company> company;
  public PXSetup<FASetup> fasetup;

  public CalcDeprProcess()
  {
    FASetup current = ((PXSelectBase<FASetup>) this.fasetup).Current;
  }

  [PXDBInt(IsKey = true)]
  [PXSelector(typeof (Search<FixedAsset.assetID>), SubstituteKey = typeof (FixedAsset.assetCD), CacheGlobal = true, DescriptionField = typeof (FixedAsset.description))]
  [PXUIField(DisplayName = "Fixed Asset", Enabled = false)]
  public virtual void FABookBalance_AssetID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (FABook.bookID), SubstituteKey = typeof (FABook.bookCode), DescriptionField = typeof (FABook.description))]
  protected virtual void FABookBalance_BookID_CacheAttached(PXCache sender)
  {
  }

  protected virtual IEnumerable balances()
  {
    BalanceFilter current = ((PXSelectBase<BalanceFilter>) this.Filter).Current;
    PXView view = ((BqlCommand) new Select2<FABookBalance, InnerJoin<FixedAsset, On<FixedAsset.assetID, Equal<FABookBalance.assetID>>, InnerJoin<FADetails, On<FADetails.assetID, Equal<FABookBalance.assetID>>, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<FixedAsset.fAAccountID>>>>>, Where<FABookBalance.depreciate, Equal<True>, And<FABookBalance.status, Equal<FixedAssetStatus.active>, And<FADetails.status, Equal<FixedAssetStatus.active>, And<FixedAsset.underConstruction, NotEqual<True>>>>>, OrderBy<Asc<FABookBalance.assetID, Asc<FABookBalance.bookID>>>>()).CreateView((PXGraph) this, mergeCache: PXView.MaximumRows > 1);
    if (current.BookID.HasValue)
      view.WhereAnd<Where<FABookBalance.bookID, Equal<Current<ProcessAssetFilter.bookID>>>>();
    int? nullable = current.ClassID;
    if (nullable.HasValue)
      view.WhereAnd<Where<FixedAsset.classID, Equal<Current<ProcessAssetFilter.classID>>>>();
    if (!PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>())
    {
      nullable = current.OrgBAccountID;
      if (!nullable.HasValue)
        goto label_7;
    }
    view.WhereAnd<Where<FixedAsset.branchID, Inside<Current<BalanceFilter.orgBAccountID>>>>();
label_7:
    if (!string.IsNullOrEmpty(current.PeriodID))
      view.WhereAnd<Where<FABookBalance.currDeprPeriod, LessEqual<Current<BalanceFilter.periodID>>>>();
    nullable = current.ParentAssetID;
    if (nullable.HasValue)
      view.WhereAnd<Where<FixedAsset.parentAssetID, Equal<Current<ProcessAssetFilter.parentAssetID>>>>();
    int startRow = PXView.StartRow;
    int num = 0;
    List<PXFilterRow> pxFilterRowList = new List<PXFilterRow>();
    foreach (PXFilterRow filter in PXView.Filters)
    {
      if (filter.DataField.ToLower() == "status")
        filter.DataField = "FADetails__Status";
      pxFilterRowList.Add(filter);
    }
    List<object> objectList = view.Select(PXView.Currents, (object[]) null, PXView.Searches, PXView.SortColumns, PXView.Descendings, pxFilterRowList.ToArray(), ref startRow, PXView.MaximumRows, ref num);
    PXView.StartRow = 0;
    return (IEnumerable) objectList;
  }

  protected virtual void FABookBalance_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    FABookBalance row = (FABookBalance) e.Row;
    if (row == null || PXLongOperation.Exists(((PXGraph) this).UID))
      return;
    bool flag = false;
    try
    {
      this.CheckAcceleratedDepreciationStraightLine((PXGraph) this, row);
    }
    catch (PXException ex)
    {
      flag = true;
      if (((PXSelectBase<BalanceFilter>) this.Filter).Current.Action == "D")
      {
        PXUIFieldAttribute.SetEnabled<FABookBalance.selected>(sender, (object) row, false);
        sender.RaiseExceptionHandling<FABookBalance.selected>((object) row, (object) null, (Exception) new PXSetPropertyException(ex.MessageNoNumber, (PXErrorLevel) 5));
      }
      else
        sender.RaiseExceptionHandling<FABookBalance.selected>((object) row, (object) null, (Exception) new PXSetPropertyException(ex.MessageNoNumber, (PXErrorLevel) 3));
    }
    if (flag)
      return;
    try
    {
      CalcDeprProcess.CalcDeprProcessFixedAssetChecksExtension extension = ((PXGraph) this).GetExtension<CalcDeprProcess.CalcDeprProcessFixedAssetChecksExtension>();
      extension.CheckIfAssetIsUnderConstruction(row.AssetID);
      extension.CheckIfAssetIsNotAcquired(row.AssetID, row.BookID);
      extension.CheckUnreleasedTransactions(row.AssetID);
    }
    catch (PXException ex)
    {
      PXUIFieldAttribute.SetEnabled<FABookBalance.selected>(sender, (object) row, false);
      sender.RaiseExceptionHandling<FABookBalance.selected>((object) row, (object) null, (Exception) new PXSetPropertyException(ex.MessageNoNumber, (PXErrorLevel) 3));
    }
  }

  private IEnumerable<FABookBalance> GetProcessableRecords(IEnumerable<FABookBalance> list)
  {
    CalcDeprProcess.CalcDeprProcessFixedAssetChecksExtension calcDeprProcessExtension = ((PXGraph) this).GetExtension<CalcDeprProcess.CalcDeprProcessFixedAssetChecksExtension>();
    return list.Where<FABookBalance>((Func<FABookBalance, bool>) (balance => !calcDeprProcessExtension.UnreleasedTransactionsExistsForAsset(balance.AssetID)));
  }

  private void SetProcessDelegate()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CalcDeprProcess.\u003C\u003Ec__DisplayClass15_0 cDisplayClass150 = new CalcDeprProcess.\u003C\u003Ec__DisplayClass15_0()
    {
      filter = ((PXSelectBase<BalanceFilter>) this.Filter).Current
    };
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass150.depreciate = cDisplayClass150.filter.Action == "D";
    ((PXProcessingBase<FABookBalance>) this.Balances).ParallelProcessingOptions = (Action<PXParallelProcessingOptions>) (settings =>
    {
      settings.IsEnabled = true;
      settings.AutoBatchSize = true;
      settings.BatchSize = 500;
    });
    // ISSUE: reference to a compiler-generated field
    cDisplayClass150.graph = GraphHelper.Clone<CalcDeprProcess>(this);
    // ISSUE: method pointer
    ((PXProcessingBase<FABookBalance>) this.Balances).SetProcessDelegate(new PXProcessingBase<FABookBalance>.ProcessListDelegate((object) cDisplayClass150, __methodptr(\u003CSetProcessDelegate\u003Eb__1)));
    // ISSUE: reference to a compiler-generated field
    bool flag = !string.IsNullOrEmpty(cDisplayClass150.filter.PeriodID) && ((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault();
    // ISSUE: reference to a compiler-generated field
    PXUIFieldAttribute.SetEnabled<BalanceFilter.action>(((PXSelectBase) this.Filter).Cache, (object) cDisplayClass150.filter, flag);
    if (flag)
      return;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass150.filter.Action = "C";
  }

  protected virtual void BalanceFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    this.SetProcessDelegate();
  }

  [PXSuppressActionValidation]
  [PXUIField]
  [PXButton]
  protected virtual IEnumerable actionsFolder(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable viewAsset(PXAdapter adapter)
  {
    if (((PXSelectBase<FABookBalance>) this.Balances).Current != null)
    {
      AssetMaint instance = PXGraph.CreateInstance<AssetMaint>();
      ((PXSelectBase<FixedAsset>) instance.CurrentAsset).Current = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FABookBalance.assetID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (((PXSelectBase<FixedAsset>) instance.CurrentAsset).Current != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "ViewAsset");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewBook(PXAdapter adapter)
  {
    if (((PXSelectBase<FABookBalance>) this.Balances).Current != null)
    {
      BookMaint instance = PXGraph.CreateInstance<BookMaint>();
      ((PXSelectBase<FABook>) instance.Book).Current = PXResultset<FABook>.op_Implicit(PXSelectBase<FABook, PXSelect<FABook, Where<FABook.bookID, Equal<Current<FABookBalance.bookID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (((PXSelectBase<FABook>) instance.Book).Current != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "ViewBook");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewClass(PXAdapter adapter)
  {
    if (((PXSelectBase<FABookBalance>) this.Balances).Current != null)
    {
      AssetClassMaint instance = PXGraph.CreateInstance<AssetClassMaint>();
      ((PXSelectBase<FixedAsset>) instance.CurrentAssetClass).Current = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FABookBalance.classID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (((PXSelectBase<FixedAsset>) instance.CurrentAssetClass).Current != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "ViewClass");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  protected virtual void CheckAcceleratedDepreciationStraightLine(
    PXGraph graph,
    FABookBalance bookBalance)
  {
    PXResult<FABookBalance, FixedAsset, FAClass, FADepreciationMethod> pxResult = (PXResult<FABookBalance, FixedAsset, FAClass, FADepreciationMethod>) PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXViewOf<FABookBalance>.BasedOn<SelectFromBase<FABookBalance, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<FixedAsset>.On<BqlOperand<FABookBalance.assetID, IBqlInt>.IsEqual<FixedAsset.assetID>>>, FbqlJoins.Left<FAClass>.On<BqlOperand<FABookBalance.classID, IBqlInt>.IsEqual<FAClass.assetID>>>, FbqlJoins.Inner<FADepreciationMethod>.On<BqlOperand<FABookBalance.depreciationMethodID, IBqlInt>.IsEqual<FADepreciationMethod.methodID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookBalance.assetID, Equal<BqlField<FABookBalance.assetID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<FABookBalance.bookID, IBqlInt>.IsEqual<BqlField<FABookBalance.bookID, IBqlInt>.FromCurrent>>>>.Config>.SelectSingleBound(graph, new object[1]
    {
      (object) bookBalance
    }, Array.Empty<object>()));
    FixedAsset fixedAsset = PXResult<FABookBalance, FixedAsset, FAClass, FADepreciationMethod>.op_Implicit(pxResult);
    FAClass faClass = PXResult<FABookBalance, FixedAsset, FAClass, FADepreciationMethod>.op_Implicit(pxResult);
    if (!(PXResult<FABookBalance, FixedAsset, FAClass, FADepreciationMethod>.op_Implicit(pxResult).DepreciationMethod == "SL") || !(bookBalance.AveragingConvention == "FD") && !(bookBalance.AveragingConvention == "FP") || !faClass.AcceleratedDepreciation.GetValueOrDefault())
      return;
    if (((IQueryable<PXResult<FABookHistory>>) PXSelectBase<FABookHistory, PXViewOf<FABookHistory>.BasedOn<SelectFromBase<FABookHistory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookHistory.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FABookHistory.bookID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<FABookHistory.ptdDeprBase, IBqlDecimal>.IsNotEqual<decimal0>>>>.And<BqlOperand<FABookHistory.finPeriodID, IBqlString>.IsNotEqual<P.AsString>>>.Order<PX.Data.BQL.Fluent.By<BqlField<FABookHistory.finPeriodID, IBqlString>.Asc>>>.Config>.Select(graph, new object[3]
    {
      (object) bookBalance.AssetID,
      (object) bookBalance.BookID,
      (object) bookBalance.DeprFromPeriod
    })).Any<PXResult<FABookHistory>>())
      throw new PXException("The Accelerated Depreciation for SL Method check box selected for the {0} asset class is not supported for the Full Day and Full Period averaging conventions. With these settings, the accumulated depreciation of the {1} asset will not be calculated correctly. Change the depreciation method to a method based on the Remaining Value calculation method on the Balance tab of the Fixed Assets (FA202500) form.", new object[2]
      {
        (object) faClass.AssetCD,
        (object) fixedAsset.AssetCD
      });
  }

  public class CalcDeprProcessFixedAssetChecksExtension : 
    FixedAssetChecksExtensionBase<CalcDeprProcess>
  {
  }
}
