// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.AssetMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.DependencyInjection;
using PX.Data.WorkflowAPI;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.Common.Scopes;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.FA.Overrides.AssetProcess;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.GL.GraphBaseExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.FA;

public class AssetMaint : 
  PXGraph<
  #nullable disable
  AssetMaint, FixedAsset>,
  IGraphWithInitialization,
  IAssetTransferInformationCheckable
{
  public PXSelect<FixedAsset, Where<FixedAsset.recordType, Equal<FARecordType.assetType>>> Asset;
  public PXSelect<FAClass> Class;
  public PXSelect<PX.Objects.CR.BAccount> Baccount;
  public PXSelect<PX.Objects.AP.Vendor> Vendor;
  public PXSelect<EPEmployee> Employee;
  public PXSelect<FixedAsset, Where<FixedAsset.assetCD, Equal<Current<FixedAsset.assetCD>>>> CurrentAsset;
  public PXSelect<FADetails, Where<FADetails.assetID, Equal<Optional<FixedAsset.assetID>>>> AssetDetails;
  public PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>>, OrderBy<Asc<FABookBalance.assetID, Asc<FABookBalance.bookID>>>> AssetBalance;
  public PXSelect<FABookSettings, Where<FABookSettings.assetID, Equal<Current<FixedAsset.classID>>>> DepreciationSettings;
  public PXSelect<FABookHistory, Where<FABookHistory.assetID, Equal<Current<FixedAsset.assetID>>, And<FABookHistory.bookID, Equal<Current<FABookSettings.bookID>>>>> BookHistory;
  public PXSelect<FABookHist> BookHist;
  public PXSelectJoin<FAComponent, InnerJoin<FADetails, On<FixedAsset.assetID, Equal<FADetails.assetID>>>, Where<FixedAsset.parentAssetID, Equal<Current<FixedAsset.assetID>>>> AssetElements;
  public PXSelect<FARegister> Register;
  public FbqlSelect<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  FATran.assetID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  FixedAsset.assetID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  FATran.bookID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  TranBookFilter.bookID, IBqlInt>.FromCurrent>>>>>.Or<
  #nullable disable
  BqlOperand<Current<
  #nullable enable
  TranBookFilter.bookID>, IBqlInt>.IsNull>>>.Order<
  #nullable disable
  By<Asc<FATran.finPeriodID>, Asc<FATran.bookID>>>, FATran>.View FATransactions;
  public PXSelect<FALocationHistory, Where<FALocationHistory.assetID, Equal<Current<FixedAsset.assetID>>>, OrderBy<Desc<FALocationHistory.revisionID>>> LocationHistory;
  public PXSelect<FAUsage, Where<FAUsage.assetID, Equal<Current<FixedAsset.assetID>>>> AssetUsage;
  public PXSelect<FAServiceSchedule, Where<FAServiceSchedule.scheduleID, Equal<Current<FixedAsset.serviceScheduleID>>>> ServiceSchedule;
  public PXSelect<FAService, Where<FAService.assetID, Equal<Current<FixedAsset.assetID>>>> AssetService;
  public PXSelect<FALocationHistory, Where<FALocationHistory.assetID, Equal<Optional<FADetails.assetID>>, And<FALocationHistory.revisionID, Equal<Optional<FADetails.locationRevID>>>>> AssetLocation;
  [PXCopyPasteHiddenView]
  public PXSelectOrderBy<FAHistory, OrderBy<Asc<FAHistory.finPeriodID>>> AssetHistory;
  [PXCopyPasteHiddenView]
  public PXSelectOrderBy<FASheetHistory, OrderBy<Asc<FASheetHistory.periodNbr>>> BookSheetHistory;
  public PXSetup<FASetup> fasetup;
  [PXCopyPasteHiddenView]
  public PXFilter<DeprBookFilter> deprbookfilter;
  [PXCopyPasteHiddenView]
  public PXFilter<TranBookFilter> bookfilter;
  public PXFilter<DisposeParams> DispParams;
  public PXFilter<PX.Objects.FA.SuspendParams> SuspendParams;
  public PXFilter<ReverseDisposalInfo> RevDispInfo;
  public PXFilter<GLTranFilter> GLTrnFilter;
  public FbqlSelect<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  FATran.tranType, 
  #nullable disable
  Equal<FATran.tranType.transferPurchasing>>>>>.And<BqlOperand<
  #nullable enable
  FATran.assetID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  FixedAsset.assetID, IBqlInt>.FromCurrent>>>, 
  #nullable disable
  FATran>.View transferTransactions;
  [PXFilterable(new System.Type[] {})]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<DsplFAATran, LeftJoin<FAAccrualTran, On<DsplFAATran.tranID, Equal<FAAccrualTran.tranID>>>> DsplAdditions;
  public PXSelectJoin<Numbering, InnerJoin<FASetup, On<FASetup.assetNumberingID, Equal<Numbering.numberingID>>>> assetNumbering;
  public PXSetup<GLSetup> glsetup;
  public PXSetup<Company> company;
  public Dictionary<int?, int> _Books = new Dictionary<int?, int>();
  public List<string> _FieldNames = new List<string>();
  public List<string> _SheetFieldNames = new List<string>();
  private FixedAsset _PersistedAsset;
  public PXAction<FixedAsset> viewDocument;
  public PXAction<FixedAsset> viewReconcileDocument;
  public PXAction<FixedAsset> viewBatch;
  public PXWorkflowEventHandler<FixedAsset> OnUpdateStatus;
  public PXWorkflowEventHandler<FixedAsset> OnActivateAsset;
  public PXWorkflowEventHandler<FixedAsset> OnDisposeAsset;
  public PXWorkflowEventHandler<FixedAsset> OnSuspendAsset;
  public PXWorkflowEventHandler<FixedAsset> OnFullyDepreciateAsset;
  public PXWorkflowEventHandler<FixedAsset> OnNotFullyDepreciateAsset;
  public PXWorkflowEventHandler<FixedAsset> OnReverseAsset;
  public PXInitializeState<FixedAsset> initializeState;
  public PXAction<FixedAsset> putOnHold;
  public PXAction<FixedAsset> releaseFromHold;
  public PXAction<FixedAsset> runReversal;
  public PXAction<FixedAsset> runDispReversal;
  public PXAction<FixedAsset> disposalOK;
  public PXAction<FixedAsset> runDisposal;
  public PXAction<FixedAsset> runSplit;
  public PXAction<FixedAsset> action;
  public PXAction<FixedAsset> CalculateDepreciation;
  public PXAction<FixedAsset> Suspend;
  public PXAction<FixedAsset> ReduceUnreconCost;
  protected Dictionary<System.Type, System.Type> AccountSubPairs = new Dictionary<System.Type, System.Type>()
  {
    [typeof (FixedAsset.fAAccountID)] = typeof (FixedAsset.fASubID),
    [typeof (FixedAsset.accumulatedDepreciationAccountID)] = typeof (FixedAsset.accumulatedDepreciationSubID),
    [typeof (FixedAsset.depreciatedExpenseAccountID)] = typeof (FixedAsset.depreciatedExpenseSubID),
    [typeof (FixedAsset.fAAccrualAcctID)] = typeof (FixedAsset.fAAccrualSubID),
    [typeof (FixedAsset.disposalAccountID)] = typeof (FixedAsset.disposalSubID),
    [typeof (FixedAsset.gainAcctID)] = typeof (FixedAsset.gainSubID),
    [typeof (FixedAsset.lossAcctID)] = typeof (FixedAsset.lossSubID),
    [typeof (FixedAsset.constructionAccountID)] = typeof (FixedAsset.constructionSubID)
  };

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FixedAsset.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FixedAsset.accumulatedDepreciationAccountID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FixedAsset.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FixedAsset.accumulatedDepreciationSubID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FixedAsset.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FixedAsset.depreciatedExpenseAccountID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FixedAsset.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FixedAsset.depreciatedExpenseSubID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FADetails.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FADetails.depreciateFromDate> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<PX.Objects.FA.Standalone.FADetails.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PX.Objects.FA.Standalone.FADetails.depreciateFromDate> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FALocationHistory.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FALocationHistory.accumulatedDepreciationAccountID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FALocationHistory.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FALocationHistory.accumulatedDepreciationSubID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FALocationHistory.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FALocationHistory.depreciatedExpenseAccountID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FALocationHistory.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FALocationHistory.depreciatedExpenseSubID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FABookSettings.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FABookSettings.depreciationMethodID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FABookSettings.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FABookSettings.averagingConvention> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FABookBalance.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FABookBalance.depreciationMethodID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FABookBalance.assetID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<FABookBalance.averagingConvention> e)
  {
  }

  [PXMergeAttributes]
  [PXUIRequired(typeof (FixedAssetCanBeDepreciated<FABookBalance.assetID>))]
  protected virtual void _(PX.Data.Events.CacheAttached<FABookBalance.deprFromDate> e)
  {
  }

  public static FixedAsset FindByID(PXGraph graph, int? assetID)
  {
    return PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select(graph, new object[1]
    {
      (object) assetID
    }));
  }

  [InjectDependency]
  public IFABookPeriodRepository FABookPeriodRepository { get; set; }

  [InjectDependency]
  public IFABookPeriodUtils FABookPeriodUtils { get; set; }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  public AssetMaint()
  {
    PXCache cach1 = ((PXGraph) this).Caches[typeof (FABookPeriod)];
    PXCache cach2 = ((PXGraph) this).Caches[typeof (PX.Objects.GL.GLTran)];
    PXCache cache1 = ((PXSelectBase) this.AssetDetails).Cache;
    PXCache cache2 = ((PXSelectBase) this.Asset).Cache;
    FASetup current1 = ((PXSelectBase<FASetup>) this.fasetup).Current;
    GLSetup current2 = ((PXSelectBase<GLSetup>) this.glsetup).Current;
    PXUIFieldAttribute.SetRequired<FADetails.propertyType>(cache1, true);
    PXUIFieldAttribute.SetRequired<FixedAsset.assetTypeID>(cache2, true);
    PXUIFieldAttribute.SetRequired<FixedAsset.classID>(cache2, true);
    PXUIFieldAttribute.SetVisible<PX.Objects.GL.GLTran.module>(cach2, (object) null, false);
    PXUIFieldAttribute.SetVisible<PX.Objects.GL.GLTran.batchNbr>(cach2, (object) null, false);
    PXUIFieldAttribute.SetVisible<PX.Objects.GL.GLTran.refNbr>(cach2, (object) null, false);
    int num1 = 0;
    foreach (PXResult<FABook> pxResult in PXSelectBase<FABook, PXSelect<FABook>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      FABook faBook = PXResult<FABook>.op_Implicit(pxResult);
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      AssetMaint.\u003C\u003Ec__DisplayClass73_0 cDisplayClass730 = new AssetMaint.\u003C\u003Ec__DisplayClass73_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass730.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass730.j = num1++;
      // ISSUE: reference to a compiler-generated field
      this._Books.Add(faBook.BookID, cDisplayClass730.j);
      string str1 = $"{faBook.BookCode} {PXMessages.LocalizeNoPrefix("Calculated")}";
      this._FieldNames.Add(str1);
      ((PXSelectBase) this.AssetHistory).Cache.Fields.Add(str1);
      // ISSUE: method pointer
      ((PXGraph) this).FieldSelecting.AddHandler(typeof (FAHistory), str1, new PXFieldSelecting((object) cDisplayClass730, __methodptr(\u003C\u002Ector\u003Eb__0)));
      string str2 = $"{faBook.BookCode} {PXMessages.LocalizeNoPrefix("Depreciated")}";
      this._FieldNames.Add(str2);
      ((PXSelectBase) this.AssetHistory).Cache.Fields.Add(str2);
      // ISSUE: method pointer
      ((PXGraph) this).FieldSelecting.AddHandler(typeof (FAHistory), str2, new PXFieldSelecting((object) cDisplayClass730, __methodptr(\u003C\u002Ector\u003Eb__1)));
    }
    int num2 = 0;
    foreach (PXResult<FABookBalance2> pxResult in PXSelectBase<FABookBalance2, PXSelectGroupBy<FABookBalance2, Aggregate<Max<FABookBalance2.exactLife>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
      num2 = PXResult<FABookBalance2>.op_Implicit(pxResult).ExactLife.GetValueOrDefault();
    for (int index = 0; index < num2; ++index)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      AssetMaint.\u003C\u003Ec__DisplayClass73_1 cDisplayClass731 = new AssetMaint.\u003C\u003Ec__DisplayClass73_1();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass731.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass731.j = index;
      // ISSUE: reference to a compiler-generated field
      string str3 = $"Year_{cDisplayClass731.j}";
      this._SheetFieldNames.Add(str3);
      ((PXSelectBase) this.BookSheetHistory).Cache.Fields.Add(str3);
      // ISSUE: method pointer
      ((PXGraph) this).FieldSelecting.AddHandler(typeof (FASheetHistory), str3, new PXFieldSelecting((object) cDisplayClass731, __methodptr(\u003C\u002Ector\u003Eb__2)));
      // ISSUE: reference to a compiler-generated field
      string str4 = $"Calc_{cDisplayClass731.j}";
      this._SheetFieldNames.Add(str4);
      ((PXSelectBase) this.BookSheetHistory).Cache.Fields.Add(str4);
      // ISSUE: method pointer
      ((PXGraph) this).FieldSelecting.AddHandler(typeof (FASheetHistory), str4, new PXFieldSelecting((object) cDisplayClass731, __methodptr(\u003C\u002Ector\u003Eb__3)));
      // ISSUE: reference to a compiler-generated field
      string str5 = $"Depr_{cDisplayClass731.j}";
      this._SheetFieldNames.Add(str5);
      ((PXSelectBase) this.BookSheetHistory).Cache.Fields.Add(str5);
      // ISSUE: method pointer
      ((PXGraph) this).FieldSelecting.AddHandler(typeof (FASheetHistory), str5, new PXFieldSelecting((object) cDisplayClass731, __methodptr(\u003C\u002Ector\u003Eb__4)));
    }
    ((PXSelectBase) this.FATransactions).Cache.AllowInsert = false;
    ((PXSelectBase) this.FATransactions).Cache.AllowUpdate = false;
    ((PXSelectBase) this.FATransactions).Cache.AllowDelete = true;
    PXUIFieldAttribute.SetEnabled<FABookBalance.lastDeprPeriod>(((PXSelectBase) this.AssetBalance).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<FABookBalance.ytdDepreciated>(((PXSelectBase) this.AssetBalance).Cache, (object) null, true);
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<FixedAsset.fASubID>(new PXFieldDefaulting((object) null, __methodptr(FASubIDFieldDefaulting<FixedAsset.fASubMask, FixedAsset.fASubID>)));
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<FixedAsset.accumulatedDepreciationSubID>(new PXFieldDefaulting((object) null, __methodptr(FASubIDFieldDefaulting<FixedAsset.accumDeprSubMask, FixedAsset.accumulatedDepreciationSubID>)));
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<FixedAsset.depreciatedExpenseSubID>(new PXFieldDefaulting((object) null, __methodptr(FASubIDFieldDefaulting<FixedAsset.deprExpenceSubMask, FixedAsset.depreciatedExpenseSubID>)));
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<FixedAsset.disposalSubID>(new PXFieldDefaulting((object) null, __methodptr(FASubIDFieldDefaulting<FixedAsset.proceedsSubMask, FixedAsset.disposalSubID>)));
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<FixedAsset.gainSubID>(new PXFieldDefaulting((object) null, __methodptr(FASubIDFieldDefaulting<FixedAsset.gainLossSubMask, FixedAsset.gainSubID>)));
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<FixedAsset.lossSubID>(new PXFieldDefaulting((object) null, __methodptr(FASubIDFieldDefaulting<FixedAsset.gainLossSubMask, FixedAsset.lossSubID>)));
    PXGraph.FieldUpdatedEvents fieldUpdated1 = ((PXGraph) this).FieldUpdated;
    AssetMaint assetMaint1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) assetMaint1, __vmethodptr(assetMaint1, SetFABookBalanceUpdated));
    fieldUpdated1.AddHandler<FixedAsset.fAAccountID>(pxFieldUpdated1);
    PXGraph.FieldUpdatedEvents fieldUpdated2 = ((PXGraph) this).FieldUpdated;
    AssetMaint assetMaint2 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) assetMaint2, __vmethodptr(assetMaint2, SetFABookBalanceUpdated));
    fieldUpdated2.AddHandler<FixedAsset.fASubID>(pxFieldUpdated2);
    PXGraph.FieldUpdatedEvents fieldUpdated3 = ((PXGraph) this).FieldUpdated;
    AssetMaint assetMaint3 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated3 = new PXFieldUpdated((object) assetMaint3, __vmethodptr(assetMaint3, SetFABookBalanceUpdated));
    fieldUpdated3.AddHandler<FixedAsset.accumulatedDepreciationAccountID>(pxFieldUpdated3);
    PXGraph.FieldUpdatedEvents fieldUpdated4 = ((PXGraph) this).FieldUpdated;
    AssetMaint assetMaint4 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated4 = new PXFieldUpdated((object) assetMaint4, __vmethodptr(assetMaint4, SetFABookBalanceUpdated));
    fieldUpdated4.AddHandler<FixedAsset.accumulatedDepreciationSubID>(pxFieldUpdated4);
    PXGraph.FieldUpdatedEvents fieldUpdated5 = ((PXGraph) this).FieldUpdated;
    AssetMaint assetMaint5 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated5 = new PXFieldUpdated((object) assetMaint5, __vmethodptr(assetMaint5, SetFABookBalanceUpdated));
    fieldUpdated5.AddHandler<FixedAsset.fAAccrualAcctID>(pxFieldUpdated5);
    PXGraph.FieldUpdatedEvents fieldUpdated6 = ((PXGraph) this).FieldUpdated;
    AssetMaint assetMaint6 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated6 = new PXFieldUpdated((object) assetMaint6, __vmethodptr(assetMaint6, SetFABookBalanceUpdated));
    fieldUpdated6.AddHandler<FixedAsset.fAAccrualSubID>(pxFieldUpdated6);
    PXGraph.FieldUpdatedEvents fieldUpdated7 = ((PXGraph) this).FieldUpdated;
    AssetMaint assetMaint7 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated7 = new PXFieldUpdated((object) assetMaint7, __vmethodptr(assetMaint7, SetFABookBalanceUpdated));
    fieldUpdated7.AddHandler<FixedAsset.depreciatedExpenseAccountID>(pxFieldUpdated7);
    PXGraph.FieldUpdatedEvents fieldUpdated8 = ((PXGraph) this).FieldUpdated;
    AssetMaint assetMaint8 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated8 = new PXFieldUpdated((object) assetMaint8, __vmethodptr(assetMaint8, SetFABookBalanceUpdated));
    fieldUpdated8.AddHandler<FixedAsset.depreciatedExpenseSubID>(pxFieldUpdated8);
  }

  void IGraphWithInitialization.Initialize()
  {
    ((PXSelectBase) this.GLTrnFilter).Cache.SetDefaultExt<GLTranFilter.currentCost>((object) ((PXSelectBase<GLTranFilter>) this.GLTrnFilter).Current);
    ((PXSelectBase) this.GLTrnFilter).Cache.SetDefaultExt<GLTranFilter.accrualBalance>((object) ((PXSelectBase<GLTranFilter>) this.GLTrnFilter).Current);
    ((PXSelectBase) this.GLTrnFilter).Cache.SetDefaultExt<GLTranFilter.unreconciledAmt>((object) ((PXSelectBase<GLTranFilter>) this.GLTrnFilter).Current);
    ((PXSelectBase) this.GLTrnFilter).Cache.SetDefaultExt<GLTranFilter.selectionAmt>((object) ((PXSelectBase<GLTranFilter>) this.GLTrnFilter).Current);
  }

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    if (((PXGraph) this).IsImport && viewName == "AssetLocation")
    {
      if (((PXSelectBase<FADetails>) this.AssetDetails).Current == null)
        ((PXSelectBase<FADetails>) this.AssetDetails).Current = PXResultset<FADetails>.op_Implicit(((PXSelectBase<FADetails>) this.AssetDetails).Select(Array.Empty<object>()));
      if (keys.Count == 0)
      {
        keys.Add((object) typeof (FALocationHistory.assetID).Name, (object) ((PXSelectBase<FADetails>) this.AssetDetails).Current.AssetID);
        keys.Add((object) typeof (FALocationHistory.revisionID).Name, (object) ((PXSelectBase<FADetails>) this.AssetDetails).Current.LocationRevID);
      }
    }
    return ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
  }

  protected virtual void SetFABookBalanceUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault())
      return;
    EnumerableExtensions.ForEach<FABookBalance>(GraphHelper.RowCast<FABookBalance>((IEnumerable) ((PXSelectBase<FABookBalance>) this.AssetBalance).Select(Array.Empty<object>())), (System.Action<FABookBalance>) (balance => GraphHelper.MarkUpdated((PXCache) GraphHelper.Caches<FABookBalance>(sender.Graph), (object) balance)));
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    TransactionEntry instance = PXGraph.CreateInstance<TransactionEntry>();
    ((PXSelectBase<FARegister>) instance.Document).Current = PXResultset<FARegister>.op_Implicit(((PXSelectBase<FARegister>) instance.Document).Search<FARegister.refNbr>((object) ((PXSelectBase<FATran>) this.FATransactions).Current.RefNbr, Array.Empty<object>()));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, "FATransactions");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXUIField]
  [PXButton]
  public virtual void ViewReconcileDocument()
  {
    PXResult<FAAccrualTran, PX.Objects.GL.GLTran, Batch> pxResult = (PXResult<FAAccrualTran, PX.Objects.GL.GLTran, Batch>) PXResultset<FAAccrualTran>.op_Implicit(PXSelectBase<FAAccrualTran, PXViewOf<FAAccrualTran>.BasedOn<SelectFromBase<FAAccrualTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.GL.GLTran>.On<BqlOperand<PX.Objects.GL.GLTran.tranID, IBqlInt>.IsEqual<FAAccrualTran.tranID>>>, FbqlJoins.Left<Batch>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Batch.module, Equal<PX.Objects.GL.GLTran.module>>>>>.And<BqlOperand<Batch.batchNbr, IBqlString>.IsEqual<PX.Objects.GL.GLTran.batchNbr>>>>>.Where<BqlOperand<FAAccrualTran.tranID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<DsplFAATran>) this.DsplAdditions).Current.TranID
    }));
    PX.Objects.GL.GLTran tran = PXResult<FAAccrualTran, PX.Objects.GL.GLTran, Batch>.op_Implicit(pxResult);
    Batch batch = PXResult<FAAccrualTran, PX.Objects.GL.GLTran, Batch>.op_Implicit(pxResult);
    ((PXGraph) this).GetExtension<AssetMaint.RedirectToSourceDocumentFromAssetMaintExtension>().RedirectToSourceDocument(tran, batch);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewBatch(PXAdapter adapter)
  {
    JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
    ((PXSelectBase<Batch>) instance.BatchModule).Current = PXResultset<Batch>.op_Implicit(((PXSelectBase<Batch>) instance.BatchModule).Search<Batch.batchNbr>((object) ((PXSelectBase<FATran>) this.FATransactions).Current.BatchNbr, new object[1]
    {
      (object) "FA"
    }));
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, "Batch");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  protected virtual void GLTranFilter_UnreconciledAmt_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    GLTranFilter row = (GLTranFilter) e.Row;
    Decimal? nullable;
    int num;
    if (row == null)
    {
      num = 1;
    }
    else
    {
      nullable = row.CurrentCost;
      num = !nullable.HasValue ? 1 : 0;
    }
    if (num != 0)
      return;
    nullable = row.AccrualBalance;
    if (!nullable.HasValue)
      return;
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    nullable = row.CurrentCost;
    Decimal? accrualBalance = row.AccrualBalance;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) (nullable.HasValue & accrualBalance.HasValue ? new Decimal?(nullable.GetValueOrDefault() - accrualBalance.GetValueOrDefault()) : new Decimal?());
    defaultingEventArgs.NewValue = (object) local;
  }

  protected virtual void GLTranFilter_TranDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    FABookPeriod faBookPeriod = PXResultset<FABookPeriod>.op_Implicit(PXSelectBase<FABookPeriod, PXViewOf<FABookPeriod>.BasedOn<SelectFromBase<FABookPeriod, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FABookBalance>.On<BqlOperand<FABookPeriod.bookID, IBqlInt>.IsEqual<FABookBalance.bookID>>>, FbqlJoins.Inner<FADetails>.On<BqlOperand<FABookBalance.assetID, IBqlInt>.IsEqual<FADetails.assetID>>>, FbqlJoins.Inner<FinPeriod>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.organizationID, Equal<FinPeriod.organizationID>>>>>.And<BqlOperand<FABookPeriod.finPeriodID, IBqlString>.IsEqual<FinPeriod.finPeriodID>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookBalance.assetID, Equal<BqlField<FixedAsset.assetID, IBqlInt>.FromCurrent.NoDefault>>>>, And<BqlOperand<FABookBalance.updateGL, IBqlBool>.IsEqual<True>>>, And<BqlOperand<FABookPeriod.endDate, IBqlDateTime>.IsNotEqual<FABookPeriod.startDate>>>, And<BqlOperand<FABookPeriod.organizationID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<FADetails.receiptDate, IBqlDateTime>.IsGreaterEqual<FABookPeriod.startDate>>>>.And<BqlOperand<FADetails.receiptDate, IBqlDateTime>.IsLess<FABookPeriod.endDate>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) PXAccess.GetParentOrganizationID((int?) ((PXSelectBase<FixedAsset>) this.CurrentAsset).Current?.BranchID)
    }));
    if (faBookPeriod == null)
      return;
    DateTime? nullable1 = ((PXGraph) this).Accessinfo.BusinessDate;
    DateTime? nullable2 = faBookPeriod.StartDate;
    if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() >= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      nullable2 = ((PXGraph) this).Accessinfo.BusinessDate;
      nullable1 = faBookPeriod.EndDate;
      if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        e.NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate;
        return;
      }
    }
    e.NewValue = (object) faBookPeriod.StartDate;
  }

  public virtual PX.Objects.FA.Standalone.FADetails FetchDetailsLite(FixedAsset asset)
  {
    return AssetMaint.FetchDetailsLite((PXGraph) this, asset);
  }

  /// <summary>
  /// Performance improvement. Reduce selecting heavy projection FADetails and use lite Standalone.FADetails
  /// </summary>
  public static PX.Objects.FA.Standalone.FADetails FetchDetailsLite(PXGraph graph, FixedAsset asset)
  {
    FADetails source = GraphHelper.Caches<FADetails>(graph).Locate(new FADetails()
    {
      AssetID = (int?) asset?.AssetID
    });
    PX.Objects.FA.Standalone.FADetails faDetails;
    if (source != null)
      faDetails = Utilities.Clone<FADetails, PX.Objects.FA.Standalone.FADetails>(graph, source);
    else
      faDetails = PXResultset<PX.Objects.FA.Standalone.FADetails>.op_Implicit(PXSelectBase<PX.Objects.FA.Standalone.FADetails, PXSelect<PX.Objects.FA.Standalone.FADetails, Where<PX.Objects.FA.Standalone.FADetails.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select(graph, new object[1]
      {
        (object) (int?) asset?.AssetID
      }));
    return faDetails;
  }

  protected virtual void _(PX.Data.Events.RowUpdating<FABookBalance> e)
  {
    FABookBalance newRow = e.NewRow;
    Decimal? acquisitionCost1 = newRow.AcquisitionCost;
    Decimal num = 0M;
    if (!(acquisitionCost1.GetValueOrDefault() > num & acquisitionCost1.HasValue))
      return;
    Decimal? acquisitionCost2 = newRow.AcquisitionCost;
    Decimal? tax179Amount = newRow.Tax179Amount;
    Decimal? bonusAmount = newRow.BonusAmount;
    Decimal? nullable = tax179Amount.HasValue & bonusAmount.HasValue ? new Decimal?(tax179Amount.GetValueOrDefault() + bonusAmount.GetValueOrDefault()) : new Decimal?();
    if (!(acquisitionCost2.GetValueOrDefault() < nullable.GetValueOrDefault() & acquisitionCost2.HasValue & nullable.HasValue))
      return;
    ((PXSelectBase) this.AssetBalance).Cache.RaiseExceptionHandling<FABookBalance.acquisitionCost>((object) newRow, (object) newRow.AcquisitionCost, (Exception) new PXSetPropertyException("The total of the Tax 179 amount and the bonus amount must not exceed the acquisition cost of the asset.", (PXErrorLevel) 5));
    e.Cancel = true;
  }

  public virtual void Persist()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AssetMaint.\u003C\u003Ec__DisplayClass88_0 cDisplayClass880 = new AssetMaint.\u003C\u003Ec__DisplayClass88_0();
    FixedAsset current = ((PXSelectBase<FixedAsset>) this.CurrentAsset).Current;
    PX.Objects.FA.Standalone.FADetails details = this.FetchDetailsLite(current);
    FALocationHistory currentLocation = ((PXGraph) this).GetCurrentLocation(details);
    PXSelectBase<FATran> pxSelectBase = (PXSelectBase<FATran>) new PXSelect<FATran, Where<FATran.assetID, Equal<Current<FABookBalance.assetID>>, And<FATran.bookID, Equal<Current<FABookBalance.bookID>>>>, OrderBy<Asc<Switch<Case<Where<FATran.origin, Equal<FARegister.origin.purchasing>>, int4, Case<Where<FATran.origin, Equal<FARegister.origin.reconcilliation>>, int1>>, int0>, Asc<Switch<Case<Where<FATran.tranType, Equal<FATran.tranType.purchasingPlus>>, int0>, int1>, Desc<FATran.batchNbr, Asc<FATran.refNbr, Asc<FATran.lineNbr>>>>>>>((PXGraph) this);
    Dictionary<FABookBalance, List<OperableTran>> booktrn = new Dictionary<FABookBalance, List<OperableTran>>();
    FARegister register1 = PXResultset<FARegister>.op_Implicit(PXSelectBase<FARegister, PXViewOf<FARegister>.BasedOn<SelectFromBase<FARegister, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FATran>.On<BqlOperand<FARegister.refNbr, IBqlString>.IsEqual<FATran.refNbr>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FARegister.origin, Equal<FARegister.origin.purchasing>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FARegister.released, Equal<False>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<FASetup.updateGL>, NotEqual<True>>>>>.And<BqlOperand<FATran.batchNbr, IBqlString>.IsNull>>>>, And<BqlOperand<FATran.tranType, IBqlString>.IsEqual<FATran.tranType.purchasingPlus>>>>.And<BqlOperand<FATran.assetID, IBqlInt>.IsEqual<BqlField<FixedAsset.assetID, IBqlInt>.FromCurrent>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    foreach (FABookBalance key in ((PXSelectBase) this.AssetBalance).Cache.Deleted)
    {
      foreach (FATran faTran in ((PXSelectBase) pxSelectBase).View.SelectMultiBound(new object[1]
      {
        (object) key
      }, Array.Empty<object>()))
      {
        if (faTran != null && (faTran.Origin == "P" || faTran.Origin == "R"))
        {
          bool? released = faTran.Released;
          bool flag = false;
          if (released.GetValueOrDefault() == flag & released.HasValue)
          {
            if (!booktrn.ContainsKey(key))
              booktrn.Add(key, new List<OperableTran>());
            booktrn[key].Add(new OperableTran(PXCache<FATran>.CreateCopy(faTran), (PXDBOperation) 3));
          }
        }
      }
    }
    bool flag1 = false;
    if (current != null)
    {
      flag1 = PXResultset<FATran>.op_Implicit(PXSelectBase<FATran, PXSelect<FATran, Where<FATran.assetID, Equal<Required<FATran.assetID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
      {
        (object) current.AssetID
      })) != null;
      int? nullable1 = (int?) ((PXSelectBase) this.Asset).Cache.GetValue<FixedAsset.fAAccountID>((object) PX.Objects.FA.AssetProcess.GetSourceForNewAccounts((PXGraph) this, current));
      int? nullable2 = AssetMaint.MakeSubID<FixedAsset.fASubMask, FixedAsset.fASubID>(((PXSelectBase) this.Asset).Cache, current);
      int? faAccountId = current.FAAccountID;
      int? nullable3 = nullable1;
      int? nullable4;
      if (faAccountId.GetValueOrDefault() == nullable3.GetValueOrDefault() & faAccountId.HasValue == nullable3.HasValue)
      {
        nullable4 = current.FASubID;
        int? nullable5 = nullable2;
        if (nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue)
        {
          int? branchId = currentLocation.BranchID;
          nullable4 = current.BranchID;
          if (branchId.GetValueOrDefault() == nullable4.GetValueOrDefault() & branchId.HasValue == nullable4.HasValue)
            goto label_20;
        }
      }
      if (register1 != null)
        goto label_21;
label_20:
      if (flag1)
        goto label_28;
label_21:
      foreach (PXResult<FABookBalance> pxResult in ((PXSelectBase<FABookBalance>) this.AssetBalance).Select(Array.Empty<object>()))
        GraphHelper.MarkUpdated(((PXSelectBase) this.AssetBalance).Cache, (object) PXResult<FABookBalance>.op_Implicit(pxResult));
label_28:
      if (NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.AssetBalance).Cache.Updated) && current.IsConvertedFromAP((PXGraph) this))
      {
        nullable4 = current.SplittedFrom;
        if (!nullable4.HasValue && ((PXSelectBase<FixedAsset>) this.Asset).Ask("The selected fixed asset has been created by the process on the Convert Purchases to Assets (FA504500) form. The changes made to the asset will cause the system to delete the reconciliation transactions created by the process, and if you need to reconcile the asset, you have to do so manually. Do you want to proceed?", (MessageButtons) 4) != 6)
          return;
      }
    }
    foreach (FABookBalance faBookBalance in ((PXSelectBase) this.AssetBalance).Cache.Updated)
    {
      AssetMaint.CheckBookBalanceParameters((PXGraph) this, ((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL, currentLocation.BranchID, faBookBalance);
      FATran faTran = (FATran) ((PXSelectBase) pxSelectBase).View.SelectSingleBound(new object[1]
      {
        (object) faBookBalance
      }, Array.Empty<object>());
      if (faTran != null && (faTran.Origin == "P" || faTran.Origin == "R"))
      {
        bool? nullable = faTran.Released;
        bool flag2 = false;
        if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
        {
          nullable = ((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL;
          if (nullable.GetValueOrDefault() || !string.IsNullOrEmpty(faTran.BatchNbr))
            goto label_37;
        }
        booktrn.Add(faBookBalance, new List<OperableTran>());
        booktrn[faBookBalance].Add(new OperableTran(PXCache<FATran>.CreateCopy(faTran), (PXDBOperation) 1));
      }
label_37:
      if (faTran == null)
      {
        booktrn.Add(faBookBalance, new List<OperableTran>());
        booktrn[faBookBalance].Add(new OperableTran((FATran) null, (PXDBOperation) 2));
      }
    }
    foreach (FABookBalance faBookBalance in ((PXSelectBase) this.AssetBalance).Cache.Inserted)
    {
      AssetMaint.CheckBookBalanceParameters((PXGraph) this, ((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL, currentLocation.BranchID, faBookBalance);
      booktrn.Add(faBookBalance, new List<OperableTran>());
      booktrn[faBookBalance].Add(new OperableTran((FATran) null, (PXDBOperation) 2));
    }
    FARegister register2 = (FARegister) null;
    if (current != null && details.ReceiptDate.HasValue)
    {
      FALocationHistory prevLocation = ((PXGraph) this).GetPrevLocation(currentLocation);
      FABookBalanceTransfer bookBalanceTransfer = PXResultset<FABookBalanceTransfer>.op_Implicit(PXSelectBase<FABookBalanceTransfer, PXSelect<FABookBalanceTransfer, Where<FABookBalanceTransfer.assetID, Equal<Current<FixedAsset.assetID>>>, OrderBy<Desc<FABookBalanceTransfer.updateGL>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) current
      }, Array.Empty<object>()));
      FABookBalance faBookBalance = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>>, OrderBy<Desc<FABookBalance.updateGL>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) current
      }, Array.Empty<object>()));
      if (faBookBalance == null || prevLocation == null && string.IsNullOrEmpty(currentLocation.PeriodID) || bookBalanceTransfer == null || !((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault())
      {
        if (faBookBalance != null && faBookBalance.BookID.HasValue)
          currentLocation.PeriodID = this.FABookPeriodRepository.GetFABookPeriodIDOfDate(details.ReceiptDate, faBookBalance.BookID, current.AssetID);
        currentLocation.TransactionDate = details.ReceiptDate;
      }
      currentLocation.ClassID = current.ClassID;
      PX.Objects.FA.AssetProcess.TransferAsset((PXGraph) this, current, currentLocation, ref register2);
      if (register1 != null || !flag1)
      {
        int? branchId1 = currentLocation.BranchID;
        int? branchId2 = current.BranchID;
        if (!(branchId1.GetValueOrDefault() == branchId2.GetValueOrDefault() & branchId1.HasValue == branchId2.HasValue))
        {
          FixedAsset copy = PXCache<FixedAsset>.CreateCopy(current);
          copy.BranchID = currentLocation.BranchID;
          ((PXSelectBase<FixedAsset>) this.Asset).Update(copy);
        }
      }
    }
    FARegister faRegister1 = (FARegister) null;
    List<FARegister> created;
    using (new PXTimeStampScope(((PXGraph) this).TimeStamp))
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        AssetMaint.\u003C\u003Ec__DisplayClass88_1 cDisplayClass881 = new AssetMaint.\u003C\u003Ec__DisplayClass88_1();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass881.r = register1;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated method
        foreach (FATran faTran in ((PXSelectBase) this.FATransactions).Cache.Inserted.Cast<FATran>().Where<FATran>(cDisplayClass881.\u003C\u003E9__1 ?? (cDisplayClass881.\u003C\u003E9__1 = new Func<FATran, bool>(cDisplayClass881.\u003CPersist\u003Eb__1))))
        {
          faTran.RefNbr = register1.RefNbr;
          faTran.LineNbr = (int?) PXLineNbrAttribute.NewLineNbr<FATran.lineNbr>(((PXSelectBase) this.FATransactions).Cache, (object) register1);
          ((PXSelectBase) this.FATransactions).Cache.Normalize();
          PXParentAttribute.SetLeaveChildren<FATran.refNbr>(((PXSelectBase) this.FATransactions).Cache, (object) null, true);
          foreach (object obj in ((PXSelectBase) this.Register).Cache.Inserted.Cast<FARegister>().Where<FARegister>((Func<FARegister, bool>) (doc => doc.Origin == "P")))
            ((PXSelectBase) this.Register).Cache.Delete(obj);
          ((PXSelectBase) this.Register).Cache.Current = (object) register1;
        }
        foreach (FARegister faRegister2 in ((PXSelectBase) this.Register).Cache.Inserted)
        {
          if (faRegister2.Origin == "P")
            register1 = faRegister2;
          if (faRegister2.Origin == "R")
            faRegister1 = faRegister2;
        }
        ((PXGraph) this).Persist();
        TransactionEntry instance = PXGraph.CreateInstance<TransactionEntry>();
        ((PXGraph) instance).Clear();
        if (current != null)
          PX.Objects.FA.AssetProcess.AcquireAsset(instance, current.BranchID.Value, (IDictionary<FABookBalance, List<OperableTran>>) booktrn, register1);
        created = (List<FARegister>) instance.created;
        ((PXGraph) this).SelectTimeStamp();
        transactionScope.Complete();
      }
    }
    ((PXSelectBase) this.FATransactions).Cache.Clear();
    ((PXSelectBase) this.AssetBalance).Cache.Clear();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass880.toRelease = new PXHashSet<FARegister>((PXGraph) this);
    if (((PXSelectBase<FASetup>) this.fasetup).Current.AutoReleaseAsset.GetValueOrDefault() && created != null && created.Count > 0)
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass880.toRelease.Add(created[0]);
    }
    bool? nullable6 = ((PXSelectBase<FASetup>) this.fasetup).Current.AutoReleaseAsset;
    if (nullable6.GetValueOrDefault() && register1 != null)
    {
      nullable6 = register1.Released;
      if (!nullable6.GetValueOrDefault())
      {
        // ISSUE: reference to a compiler-generated field
        cDisplayClass880.toRelease.Add(register1);
      }
    }
    nullable6 = ((PXSelectBase<FASetup>) this.fasetup).Current.AutoReleaseAsset;
    if (nullable6.GetValueOrDefault() && faRegister1 != null)
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass880.toRelease.Add(faRegister1);
    }
    nullable6 = ((PXSelectBase<FASetup>) this.fasetup).Current.AutoReleaseTransfer;
    if (nullable6.GetValueOrDefault() && register2 != null)
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass880.toRelease.Add(register2);
    }
    // ISSUE: reference to a compiler-generated field
    if (current != null && current.Status != "H" && cDisplayClass880.toRelease.Count > 0)
    {
      ((PXGraph) this).SelectTimeStamp();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass880, __methodptr(\u003CPersist\u003Eb__0)));
    }
    if (current == null)
      return;
    ((PXSelectBase) this.Asset).Cache.RestoreCopy((object) ((PXSelectBase<FixedAsset>) this.Asset).Current, (object) PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXViewOf<FixedAsset>.BasedOn<SelectFromBase<FixedAsset, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FixedAsset.assetID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) current.AssetID.Value
    })));
  }

  protected int? AssetBookID(int idx)
  {
    return GraphHelper.RowCast<FABookBalance>((IEnumerable) ((PXSelectBase<FABookBalance>) this.AssetBalance).Select(Array.Empty<object>())).FirstOrDefault<FABookBalance>((Func<FABookBalance, bool>) (book => book != null && book.BookID.HasValue && this._Books[book.BookID] == idx && book.Depreciate.GetValueOrDefault())).With<FABookBalance, int?>((Func<FABookBalance, int?>) (bal => bal.BookID));
  }

  protected virtual void PtdCalculatedFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    int fieldNbr,
    bool calc)
  {
    e.ReturnState = (object) PXDecimalState.CreateInstance(e.ReturnState, new int?(2), this._FieldNames[2 * fieldNbr + (!calc ? 1 : 0)], new bool?(), new int?(), new Decimal?(), new Decimal?());
    ((PXFieldState) e.ReturnState).Visibility = (PXUIVisibility) 3;
    int? bookID = this.AssetBookID(fieldNbr);
    ((PXFieldState) e.ReturnState).Visible = bookID.HasValue;
    FAHistory row = (FAHistory) e.Row;
    if ((row != null ? (!row.AssetID.HasValue ? 1 : 0) : 1) != 0 || !((PXFieldState) e.ReturnState).Visible)
      return;
    if (PXResultset<FABookPeriod>.op_Implicit(PXSelectBase<FABookPeriod, PXSelect<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.finPeriodID, Equal<Required<FABookPeriod.finPeriodID>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) bookID,
      (object) this.FABookPeriodRepository.GetFABookPeriodOrganizationID(bookID, row.AssetID),
      (object) row.FinPeriodID
    })) == null)
      return;
    e.ReturnValue = (object) ((FAHistory) e.Row).PtdDepreciated[2 * fieldNbr + (!calc ? 1 : 0)].GetValueOrDefault();
  }

  protected virtual void BookSheetPeriodDepreciatedFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    int fieldNbr)
  {
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(7), new bool?(true), this._SheetFieldNames[3 * fieldNbr], new bool?(false), new int?(), "##-####", (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null);
    ((PXFieldState) e.ReturnState).Visibility = (PXUIVisibility) 3;
    FABookBalance faBookBalance = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>, And<FABookBalance.bookID, Equal<Current<DeprBookFilter.bookID>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (faBookBalance == null)
    {
      ((PXFieldState) e.ReturnState).Visible = false;
    }
    else
    {
      int int32 = Convert.ToInt32(faBookBalance.DeprFromYear);
      int num = Math.Max(Convert.ToInt32(faBookBalance.DeprToYear), Convert.ToInt32((faBookBalance.LastDeprPeriod ?? "1900").Substring(0, 4)));
      ((PXFieldState) e.ReturnState).Visible = int32 + fieldNbr <= num;
    }
    ((PXFieldState) e.ReturnState).DisplayName = PXMessages.LocalizeNoPrefix("Period");
    FASheetHistory row = (FASheetHistory) e.Row;
    if (row?.PtdValues?[fieldNbr] == null)
      return;
    e.ReturnValue = (object) PeriodIDAttribute.FormatPeriod(row.PtdValues[fieldNbr].PeriodID);
  }

  protected virtual void BookSheetPtdCalcFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    int fieldNbr,
    bool calc)
  {
    e.ReturnState = (object) PXDecimalState.CreateInstance(e.ReturnState, new int?(2), this._SheetFieldNames[3 * fieldNbr + (calc ? 1 : 2)], new bool?(), new int?(), new Decimal?(), new Decimal?());
    ((PXFieldState) e.ReturnState).Visibility = (PXUIVisibility) 3;
    FABookBalance faBookBalance = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>, And<FABookBalance.bookID, Equal<Current<DeprBookFilter.bookID>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (faBookBalance == null)
    {
      ((PXFieldState) e.ReturnState).Visible = false;
    }
    else
    {
      int int32 = Convert.ToInt32(faBookBalance.DeprFromYear);
      int num = Math.Max(Convert.ToInt32(faBookBalance.DeprToYear), Convert.ToInt32((faBookBalance.LastDeprPeriod ?? "1900").Substring(0, 4)));
      ((PXFieldState) e.ReturnState).Visible = int32 + fieldNbr <= num;
    }
    ((PXFieldState) e.ReturnState).DisplayName = calc ? PXMessages.LocalizeNoPrefix("Calculated") : PXMessages.LocalizeNoPrefix("Depreciated");
    FASheetHistory row = (FASheetHistory) e.Row;
    if (row?.PtdValues?[fieldNbr] == null)
      return;
    e.ReturnValue = (object) (calc ? row.PtdValues[fieldNbr].PtdCalculated : row.PtdValues[fieldNbr].PtdDepreciated);
  }

  public virtual IEnumerable booksheethistory()
  {
    AssetMaint assetMaint1 = this;
    FixedAsset current1 = ((PXSelectBase<FixedAsset>) assetMaint1.CurrentAsset).Current;
    DeprBookFilter current2 = ((PXSelectBase<DeprBookFilter>) assetMaint1.deprbookfilter).Current;
    if ((current2 != null ? (!current2.BookID.HasValue ? 1 : 0) : 1) == 0 && (current1 != null ? (!current1.AssetID.HasValue ? 1 : 0) : 1) == 0)
    {
      FASheetHistory dyn = (FASheetHistory) null;
      FABookBalance bal = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>, And<FABookBalance.bookID, Equal<Current<DeprBookFilter.bookID>>>>>.Config>.Select((PXGraph) assetMaint1, Array.Empty<object>()));
      if (bal != null)
      {
        string strB = bal.DeprToYear;
        string strA = string.IsNullOrEmpty(bal.LastDeprPeriod) ? strB : bal.LastDeprPeriod.Substring(0, 4);
        if (string.CompareOrdinal(strA, strB) > 0)
          strB = strA;
        int fromYearInt;
        int.TryParse(bal.DeprFromYear, out fromYearInt);
        AssetMaint assetMaint2 = assetMaint1;
        object[] objArray = new object[3]
        {
          (object) assetMaint1.FABookPeriodRepository.GetFABookPeriodOrganizationID(current2.BookID, current1.AssetID),
          (object) bal.DeprFromYear,
          (object) strB
        };
        foreach (PXResult<FABookPeriod, FABookHistory> pxResult in PXSelectBase<FABookPeriod, PXSelectJoin<FABookPeriod, LeftJoin<FABookHistory, On<FABookPeriod.finPeriodID, Equal<FABookHistory.finPeriodID>, And<FABookPeriod.bookID, Equal<FABookHistory.bookID>, And<FABookHistory.assetID, Equal<Current<FixedAsset.assetID>>>>>>, Where<FABookPeriod.bookID, Equal<Current<DeprBookFilter.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.finYear, GreaterEqual<Required<FABookPeriod.finYear>>, And<FABookPeriod.finYear, LessEqual<Required<FABookPeriod.finYear>>, And<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>>>>>>, OrderBy<Asc<FABookPeriod.periodNbr, Asc<FABookPeriod.finYear>>>>.Config>.Select((PXGraph) assetMaint2, objArray))
        {
          FABookHistory hist = PXResult<FABookPeriod, FABookHistory>.op_Implicit(pxResult);
          FABookPeriod period = PXResult<FABookPeriod, FABookHistory>.op_Implicit(pxResult);
          if (dyn != null && dyn.PeriodNbr != period.PeriodNbr)
            yield return (object) dyn;
          if (dyn == null || dyn.PeriodNbr != period.PeriodNbr)
            dyn = new FASheetHistory()
            {
              AssetID = bal.AssetID,
              PeriodNbr = period.PeriodNbr,
              PtdValues = new FASheetHistory.PeriodDeprPair[assetMaint1._SheetFieldNames.Count / 3]
            };
          Decimal? nullable1;
          Decimal? nullable2;
          Decimal? nullable3;
          Decimal? nullable4;
          Decimal? nullable5;
          if (((PXSelectBase<FASetup>) assetMaint1.fasetup).Current.AccurateDepreciation.GetValueOrDefault())
          {
            if (bal.CurrDeprPeriod == null || string.CompareOrdinal(hist.FinPeriodID, bal.CurrDeprPeriod) < 0)
            {
              nullable1 = hist.PtdDepreciated;
              nullable2 = hist.PtdAdjusted;
              nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
              nullable4 = hist.PtdDeprDisposed;
              Decimal? nullable6;
              if (!(nullable3.HasValue & nullable4.HasValue))
              {
                nullable2 = new Decimal?();
                nullable6 = nullable2;
              }
              else
                nullable6 = new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault());
              nullable5 = nullable6;
            }
            else if (string.CompareOrdinal(hist.FinPeriodID, bal.CurrDeprPeriod) > 0)
            {
              nullable5 = hist.PtdCalculated;
            }
            else
            {
              nullable4 = hist.YtdCalculated;
              nullable3 = hist.YtdDepreciated;
              Decimal? nullable7;
              if (!(nullable4.HasValue & nullable3.HasValue))
              {
                nullable2 = new Decimal?();
                nullable7 = nullable2;
              }
              else
                nullable7 = new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault());
              nullable5 = nullable7;
            }
          }
          else
            nullable5 = hist.PtdCalculated;
          int result;
          int.TryParse(period.FinYear, out result);
          int num = result - fromYearInt;
          if (dyn.PtdValues.Length > num)
          {
            FASheetHistory.PeriodDeprPair[] ptdValues = dyn.PtdValues;
            int index = num;
            string finPeriodId = period.FinPeriodID;
            nullable2 = hist.PtdDepreciated;
            nullable1 = hist.PtdAdjusted;
            nullable3 = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
            nullable4 = hist.PtdDeprDisposed;
            Decimal? depr;
            if (!(nullable3.HasValue & nullable4.HasValue))
            {
              nullable1 = new Decimal?();
              depr = nullable1;
            }
            else
              depr = new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault());
            Decimal? calc = nullable5;
            FASheetHistory.PeriodDeprPair periodDeprPair = new FASheetHistory.PeriodDeprPair(finPeriodId, depr, calc);
            ptdValues[index] = periodDeprPair;
          }
          hist = (FABookHistory) null;
          period = (FABookPeriod) null;
        }
        yield return (object) dyn;
      }
    }
  }

  public virtual IEnumerable assethistory()
  {
    AssetMaint assetMaint1 = this;
    string str1 = "207699";
    string strB = "190001";
    ((PXSelectBase<FADetails>) assetMaint1.AssetDetails).Current = PXResultset<FADetails>.op_Implicit(((PXSelectBase<FADetails>) assetMaint1.AssetDetails).Select(Array.Empty<object>()));
    if (((PXSelectBase<FADetails>) assetMaint1.AssetDetails).Current != null)
    {
      int? assetId1 = ((PXSelectBase<FADetails>) assetMaint1.AssetDetails).Current.AssetID;
      int num = 0;
      if (!(assetId1.GetValueOrDefault() < num & assetId1.HasValue))
      {
        Dictionary<int?, FABookBalance> balances = new Dictionary<int?, FABookBalance>();
        foreach (PXResult<FABookBalance> pxResult in ((PXSelectBase<FABookBalance>) assetMaint1.AssetBalance).Select(Array.Empty<object>()))
        {
          FABookBalance faBookBalance = PXResult<FABookBalance>.op_Implicit(pxResult);
          if (faBookBalance.Depreciate.GetValueOrDefault())
          {
            DateTime? nullable = faBookBalance.DeprToDate;
            if (!nullable.HasValue)
            {
              nullable = ((PXSelectBase<FADetails>) assetMaint1.AssetDetails).Current.DepreciateFromDate;
              if (!nullable.HasValue)
                goto label_19;
            }
            string str2;
            if (string.IsNullOrEmpty(faBookBalance.DeprFromPeriod))
            {
              IFABookPeriodRepository periodRepository = assetMaint1.FABookPeriodRepository;
              nullable = faBookBalance.DeprToDate;
              DateTime? date = nullable ?? ((PXSelectBase<FADetails>) assetMaint1.AssetDetails).Current.DepreciateFromDate;
              int? bookId = faBookBalance.BookID;
              int? assetId2 = faBookBalance.AssetID;
              str2 = periodRepository.GetFABookPeriodIDOfDate(date, bookId, assetId2);
            }
            else
              str2 = faBookBalance.DeprFromPeriod;
            string strA1 = str2;
            string str3 = !string.IsNullOrEmpty(faBookBalance.DeprToPeriod) ? faBookBalance.DeprToPeriod : strA1;
            string strA2 = !string.IsNullOrEmpty(faBookBalance.LastDeprPeriod) ? faBookBalance.LastDeprPeriod : str3;
            if (string.CompareOrdinal(strA1, str1) < 0)
              str1 = strA1;
            if (string.CompareOrdinal(strA2, str3) > 0)
              str3 = strA2;
            if (string.CompareOrdinal(str3, strB) > 0)
              strB = str3;
          }
label_19:
          balances[faBookBalance.BookID] = faBookBalance;
        }
        if (string.CompareOrdinal(str1, strB) <= 0)
        {
          FAHistory dyn = (FAHistory) null;
          AssetMaint assetMaint2 = assetMaint1;
          object[] objArray = new object[2]
          {
            (object) str1,
            (object) strB
          };
          // ISSUE: reference to a compiler-generated method
          foreach (PXResult<FABookPeriod, FABookHistory> pxResult in ((IEnumerable<PXResult<FABookPeriod>>) PXSelectBase<FABookPeriod, PXSelectReadonly2<FABookPeriod, LeftJoin<FABookHistory, On<FABookHistory.assetID, Equal<Current<FixedAsset.assetID>>, And<FABookHistory.bookID, Equal<FABookPeriod.bookID>, And<FABookHistory.finPeriodID, Equal<FABookPeriod.finPeriodID>>>>>, Where<FABookPeriod.finPeriodID, GreaterEqual<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.finPeriodID, LessEqual<Required<FABookPeriod.finPeriodID>>, And<FABookPeriod.startDate, NotEqual<FABookPeriod.endDate>>>>, OrderBy<Asc<FABookPeriod.finPeriodID>>>.Config>.Select((PXGraph) assetMaint2, objArray)).AsEnumerable<PXResult<FABookPeriod>>().Where<PXResult<FABookPeriod>>(new Func<PXResult<FABookPeriod>, bool>(assetMaint1.\u003Cassethistory\u003Eb__94_0)))
          {
            FABookPeriod period = PXResult<FABookPeriod, FABookHistory>.op_Implicit(pxResult);
            FABookHistory hist = PXResult<FABookPeriod, FABookHistory>.op_Implicit(pxResult);
            FABookBalance balance;
            if (balances.TryGetValue(period.BookID, out balance))
            {
              bool? nullable1 = balance.Depreciate;
              if (nullable1.GetValueOrDefault())
              {
                if (dyn != null && dyn.FinPeriodID != period.FinPeriodID)
                  yield return (object) dyn;
                if (dyn == null || dyn.FinPeriodID != period.FinPeriodID)
                  dyn = new FAHistory()
                  {
                    AssetID = ((PXSelectBase<FixedAsset>) assetMaint1.Asset).Current.AssetID,
                    FinPeriodID = period.FinPeriodID,
                    PtdDepreciated = new Decimal?[assetMaint1._Books.Count * 2]
                  };
                nullable1 = ((PXSelectBase<FASetup>) assetMaint1.fasetup).Current.AccurateDepreciation;
                Decimal? nullable2;
                Decimal? nullable3;
                Decimal? nullable4;
                Decimal? nullable5;
                if (nullable1.GetValueOrDefault())
                {
                  if (balance.CurrDeprPeriod == null || string.CompareOrdinal(hist.FinPeriodID, balance.CurrDeprPeriod) < 0)
                  {
                    Decimal?[] ptdDepreciated = dyn.PtdDepreciated;
                    int index = assetMaint1._Books[period.BookID] * 2;
                    nullable2 = hist.PtdDepreciated;
                    nullable3 = hist.PtdAdjusted;
                    nullable4 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
                    nullable5 = hist.PtdDeprDisposed;
                    Decimal? nullable6;
                    if (!(nullable4.HasValue & nullable5.HasValue))
                    {
                      nullable3 = new Decimal?();
                      nullable6 = nullable3;
                    }
                    else
                      nullable6 = new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault());
                    ptdDepreciated[index] = nullable6;
                  }
                  else if (string.CompareOrdinal(hist.FinPeriodID, balance.CurrDeprPeriod) > 0)
                  {
                    dyn.PtdDepreciated[assetMaint1._Books[period.BookID] * 2] = hist.PtdCalculated;
                  }
                  else
                  {
                    Decimal?[] ptdDepreciated = dyn.PtdDepreciated;
                    int index = assetMaint1._Books[period.BookID] * 2;
                    nullable5 = hist.YtdCalculated;
                    nullable4 = hist.YtdDepreciated;
                    Decimal? nullable7;
                    if (!(nullable5.HasValue & nullable4.HasValue))
                    {
                      nullable3 = new Decimal?();
                      nullable7 = nullable3;
                    }
                    else
                      nullable7 = new Decimal?(nullable5.GetValueOrDefault() - nullable4.GetValueOrDefault());
                    ptdDepreciated[index] = nullable7;
                  }
                }
                else
                  dyn.PtdDepreciated[assetMaint1._Books[period.BookID] * 2] = hist.PtdCalculated;
                Decimal?[] ptdDepreciated1 = dyn.PtdDepreciated;
                int index1 = assetMaint1._Books[period.BookID] * 2 + 1;
                nullable3 = hist.PtdDepreciated;
                nullable2 = hist.PtdAdjusted;
                nullable4 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
                nullable5 = hist.PtdDeprDisposed;
                Decimal? nullable8;
                if (!(nullable4.HasValue & nullable5.HasValue))
                {
                  nullable2 = new Decimal?();
                  nullable8 = nullable2;
                }
                else
                  nullable8 = new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault());
                ptdDepreciated1[index1] = nullable8;
                period = (FABookPeriod) null;
                hist = (FABookHistory) null;
                balance = (FABookBalance) null;
              }
            }
          }
          yield return (object) dyn;
        }
      }
    }
  }

  public virtual IEnumerable<PXDataRecord> ProviderSelect(
    BqlCommand command,
    int topCount,
    params PXDataValue[] pars)
  {
    System.Type[] tables = command.GetTables();
    if (tables != null && tables.Length > 1 && tables[0] == typeof (FAAccrualTran))
      command = command.OrderByNew<OrderBy<Asc<FAAccrualTran.gLTranID>>>();
    return ((PXGraph) this).ProviderSelect(command, topCount, pars);
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter)
  {
    AssetMaint graph = this;
    foreach (FixedAsset fixedAsset in adapter.Get())
    {
      ((PXSelectBase<FixedAsset>) graph.Asset).Current = fixedAsset;
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      FABookBalance faBookBalance = GraphHelper.RowCast<FABookBalance>((IEnumerable) ((PXSelectBase<FABookBalance>) graph.AssetBalance).Select(Array.Empty<object>())).Where<FABookBalance>(new Func<FABookBalance, bool>(graph.\u003CReleaseFromHold\u003Eb__107_0)).Select(new Func<FABookBalance, \u003C\u003Ef__AnonymousType54<FABookBalance, Decimal?>>(graph.\u003CReleaseFromHold\u003Eb__107_1)).Where(t =>
      {
        Decimal? acquisitionCost = t.bookbal.AcquisitionCost;
        Decimal? acquiredCost = t.AcquiredCost;
        Decimal? nullable = acquisitionCost.HasValue & acquiredCost.HasValue ? new Decimal?(acquisitionCost.GetValueOrDefault() - acquiredCost.GetValueOrDefault()) : new Decimal?();
        Decimal num = 0.00005M;
        return nullable.GetValueOrDefault() >= num & nullable.HasValue;
      }).Select(t => t.bookbal).FirstOrDefault<FABookBalance>();
      if (faBookBalance != null)
        throw new PXSetPropertyException("Asset is out of balance and cannot be removed from 'Hold'. Add Purchasing+ transactions with total amount {0}.", new object[1]
        {
          (object) PXCurrencyAttribute.BaseRound((PXGraph) graph, faBookBalance.AcquisitionCost)
        });
      yield return (object) fixedAsset;
    }
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable RunReversal(
    PXAdapter adapter,
    [PXDate] DateTime? disposalDate,
    [PXString] string disposalPeriodID,
    [PXBaseCury] Decimal? disposalCost,
    [PXInt] int? disposalMethID,
    [PXInt] int? disposalAcctID,
    [PXInt] int? disposalSubID,
    [PXString] string dispAmtMode,
    [PXBool] bool? deprBeforeDisposal,
    [PXString] string reason,
    [PXInt] int? assetID)
  {
    if (adapter.MassProcess)
      throw new NotImplementedException();
    ((PXAction) this.Save).Press();
    ((PXGraph) this).GetExtension<AssetMaint.AssetMaintFixedAssetChecksExtension>().CheckUnreleasedTransactions((FixedAsset) ((PXGraph) this).Caches[typeof (FixedAsset)].Current);
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CRunReversal\u003Eb__109_0)));
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable RunDispReversal(
    PXAdapter adapter,
    [PXDate] DateTime? disposalDate,
    [PXString] string disposalPeriodID,
    [PXBaseCury] Decimal? disposalCost,
    [PXInt] int? disposalMethID,
    [PXInt] int? disposalAcctID,
    [PXInt] int? disposalSubID,
    [PXString] string dispAmtMode,
    [PXBool] bool? deprBeforeDisposal,
    [PXString] string reason,
    [PXInt] int? assetID)
  {
    if (adapter.MassProcess)
      throw new NotImplementedException();
    if (((PXSelectBase) this.RevDispInfo).View.Answer == null || ((PXGraph) this).IsImport)
    {
      ((PXSelectBase<FADetails>) this.AssetDetails).Current = PXResultset<FADetails>.op_Implicit(((PXSelectBase<FADetails>) this.AssetDetails).Select(Array.Empty<object>()));
      ((PXSelectBase) this.RevDispInfo).Cache.Remove((object) ((PXSelectBase<ReverseDisposalInfo>) this.RevDispInfo).Current);
      ((PXSelectBase<ReverseDisposalInfo>) this.RevDispInfo).Insert();
      int? parentOrganizationId = PXAccess.GetParentOrganizationID(((PXSelectBase<FixedAsset>) this.CurrentAsset).Current.BranchID);
      if (((PXSelectBase<FADetails>) this.AssetDetails).Current.DisposalDate.HasValue && !this.FinPeriodRepository.FindFinPeriodByDate(((PXSelectBase<FADetails>) this.AssetDetails).Current.DisposalDate, parentOrganizationId).FAClosed.GetValueOrDefault())
        ((PXSelectBase<ReverseDisposalInfo>) this.RevDispInfo).Current.ReverseDisposalDate = ((PXSelectBase<FADetails>) this.AssetDetails).Current.DisposalDate;
      string finPeriodId = this.FinPeriodRepository.FindFinPeriodByDate(((PXSelectBase<ReverseDisposalInfo>) this.RevDispInfo).Current.ReverseDisposalDate, parentOrganizationId)?.FinPeriodID;
      if (PXSelectorAttribute.Select<ReverseDisposalInfo.reverseDisposalPeriodID>(((PXSelectBase) this.RevDispInfo).Cache, ((PXSelectBase) this.RevDispInfo).Cache.Current, (object) FinPeriodIDFormattingAttribute.FormatForDisplay(finPeriodId)) == null)
      {
        IEnumerable<FinPeriod> source = GraphHelper.RowCast<FinPeriod>((IEnumerable) PXSelectorAttribute.SelectAll<ReverseDisposalInfo.reverseDisposalPeriodID>(((PXSelectBase) this.RevDispInfo).Cache, ((PXSelectBase) this.RevDispInfo).Cache.Current)).Where<FinPeriod>((Func<FinPeriod, bool>) (p =>
        {
          DateTime? startDate = p.StartDate;
          DateTime? nullable = ((PXSelectBase<ReverseDisposalInfo>) this.RevDispInfo).Current.DisposalDate;
          if ((startDate.HasValue & nullable.HasValue ? (startDate.GetValueOrDefault() >= nullable.GetValueOrDefault() ? 1 : 0) : 0) == 0)
            return false;
          nullable = p.EndDate;
          DateTime? disposalDate1 = ((PXSelectBase<ReverseDisposalInfo>) this.RevDispInfo).Current.DisposalDate;
          return nullable.HasValue & disposalDate1.HasValue && nullable.GetValueOrDefault() <= disposalDate1.GetValueOrDefault();
        }));
        FinPeriod finPeriod = source != null ? source.FirstOrDefault<FinPeriod>() : (FinPeriod) null;
        if (finPeriod != null)
          finPeriodId = finPeriod.FinPeriodID;
      }
      ((PXSelectBase<ReverseDisposalInfo>) this.RevDispInfo).Current.ReverseDisposalPeriodID = finPeriodId;
    }
    if (((PXSelectBase) this.RevDispInfo).View.Answer == null)
      ((PXSelectBase<ReverseDisposalInfo>) this.RevDispInfo).AskExt();
    else if (((PXSelectBase) this.RevDispInfo).View.Answer != 1 || !this.RevDispInfo.VerifyRequired())
      return adapter.Get();
    ((PXAction) this.Save).Press();
    ((PXGraph) this).GetExtension<AssetMaint.AssetMaintFixedAssetChecksExtension>().CheckUnreleasedTransactions(((PXSelectBase<FixedAsset>) this.CurrentAsset).Current);
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CRunDispReversal\u003Eb__111_0)));
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable DisposalOK(PXAdapter adapter)
  {
    this.DispParams.VerifyRequired();
    DisposeParams current1 = (DisposeParams) ((PXSelectBase) this.DispParams).Cache.Current;
    if (((IEnumerable<string>) ((PXSelectBase) this.DispParams).Cache.Fields).Select<string, string>((Func<string, string>) (field => PXUIFieldAttribute.GetErrorOnly(((PXSelectBase) this.DispParams).Cache, ((PXSelectBase) this.DispParams).Cache.Current, field))).Any<string>((Func<string, bool>) (err => !string.IsNullOrEmpty(err))))
      return adapter.Get();
    FixedAsset current2 = ((PXSelectBase<FixedAsset>) this.CurrentAsset).Current;
    FADetails faDetails = PXResultset<FADetails>.op_Implicit(PXSelectBase<FADetails, PXSelect<FADetails, Where<FADetails.assetID, Equal<Current<FixedAsset.assetID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) current2
    }, Array.Empty<object>()));
    PXResultset<FixedAsset, FADetails> list = new PXResultset<FixedAsset, FADetails>();
    ((PXResultset<FixedAsset>) list).Add((PXResult<FixedAsset>) new PXResult<FixedAsset, FADetails>(current2, faDetails));
    this.Dispose(list, faDetails.CurrentCost.GetValueOrDefault(), current1.DisposalDate, current1.DisposalPeriodID, current1.DisposalAmt, current1.DisposalMethodID, current1.DisposalAccountID, current1.DisposalSubID, (string) null, new bool?(current1.ActionBeforeDisposal == "D"), current1.Reason, false);
    return adapter.Get();
  }

  protected virtual void Dispose(
    PXResultset<FixedAsset, FADetails> list,
    Decimal cost,
    DateTime? disposalDate,
    string disposalPeriodID,
    Decimal? disposalCost,
    int? disposalMethID,
    int? disposalAcctID,
    int? disposalSubID,
    string dispAmtMode,
    bool? deprBeforeDisposal,
    string reason,
    bool isMassProcess)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AssetMaint.\u003C\u003Ec__DisplayClass114_0 displayClass1140 = new AssetMaint.\u003C\u003Ec__DisplayClass114_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1140.list = list;
    // ISSUE: reference to a compiler-generated field
    displayClass1140.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    displayClass1140.deprBeforeDisposal = deprBeforeDisposal;
    // ISSUE: reference to a compiler-generated field
    displayClass1140.reason = reason;
    // ISSUE: reference to a compiler-generated field
    displayClass1140.isMassProcess = isMassProcess;
    Decimal num1 = 0M;
    FADetails faDetails1 = (FADetails) null;
    // ISSUE: reference to a compiler-generated field
    foreach (PXResult<FixedAsset, FADetails> pxResult in (PXResultset<FixedAsset>) displayClass1140.list)
    {
      FixedAsset fixedAsset = PXResult<FixedAsset, FADetails>.op_Implicit(pxResult);
      FADetails faDetails2 = PXResult<FixedAsset, FADetails>.op_Implicit(pxResult);
      Decimal? nullable;
      Decimal num2;
      if (!(cost == 0M))
      {
        Decimal valueOrDefault1 = disposalCost.GetValueOrDefault();
        nullable = faDetails2.CurrentCost;
        Decimal valueOrDefault2 = nullable.GetValueOrDefault();
        num2 = valueOrDefault1 * valueOrDefault2 / cost;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        num2 = disposalCost.GetValueOrDefault() / (Decimal) ((PXResultset<FixedAsset>) displayClass1140.list).Count;
      }
      Decimal num3 = num2;
      if (dispAmtMode == "M")
      {
        nullable = fixedAsset.DisposalAmt;
        nullable = nullable.HasValue ? fixedAsset.DisposalAmt : throw new PXException("Can not dispose Fixed Assets. Disposal Amount is empty.");
        num3 = nullable.Value;
      }
      fixedAsset.DisposalAccountID = disposalAcctID;
      fixedAsset.DisposalSubID = disposalSubID;
      faDetails2.DisposalDate = disposalDate;
      faDetails2.DisposalMethodID = disposalMethID;
      faDetails2.SaleAmount = new Decimal?(PXCurrencyAttribute.BaseRound((PXGraph) this, num3));
      faDetails2.DisposalPeriodID = disposalPeriodID;
      ((PXSelectBase<FixedAsset>) this.Asset).Update(fixedAsset);
      FADetails faDetails3 = ((PXSelectBase<FADetails>) this.AssetDetails).Update(faDetails2);
      ((PXAction) this.Save).Press();
      Decimal num4 = num1;
      nullable = faDetails3.SaleAmount;
      Decimal num5 = nullable.Value;
      num1 = num4 + num5;
      if (faDetails1 != null)
      {
        nullable = faDetails1.SaleAmount;
        Decimal? saleAmount = faDetails3.SaleAmount;
        if (!(nullable.GetValueOrDefault() < saleAmount.GetValueOrDefault() & nullable.HasValue & saleAmount.HasValue))
          continue;
      }
      faDetails1 = faDetails3;
    }
    Decimal num6 = disposalCost.GetValueOrDefault() - num1;
    if (dispAmtMode == "A" && num6 != 0M)
    {
      FADetails faDetails4 = faDetails1;
      Decimal? saleAmount = faDetails1.SaleAmount;
      Decimal num7 = num6;
      Decimal? nullable = saleAmount.HasValue ? new Decimal?(saleAmount.GetValueOrDefault() + num7) : new Decimal?();
      faDetails4.SaleAmount = nullable;
      ((PXSelectBase<FADetails>) this.AssetDetails).Update(faDetails1);
      ((PXAction) this.Save).Press();
    }
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1140, __methodptr(\u003CDispose\u003Eb__0)));
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable RunDisposal(
    PXAdapter adapter,
    [PXDate] DateTime? disposalDate,
    [PXString] string disposalPeriodID,
    [PXBaseCury] Decimal? disposalCost,
    [PXInt] int? disposalMethID,
    [PXInt] int? disposalAcctID,
    [PXInt] int? disposalSubID,
    [PXString] string dispAmtMode,
    [PXBool] bool? deprBeforeDisposal,
    [PXString] string reason,
    [PXInt] int? assetID)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AssetMaint.\u003C\u003Ec__DisplayClass116_0 displayClass1160 = new AssetMaint.\u003C\u003Ec__DisplayClass116_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1160.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    displayClass1160.list = new PXResultset<FixedAsset, FADetails>();
    Decimal cost = 0M;
    foreach (FixedAsset fixedAsset in adapter.Get())
    {
      FADetails faDetails = !(fixedAsset.Status == "D") ? PXResultset<FADetails>.op_Implicit(PXSelectBase<FADetails, PXSelect<FADetails, Where<FADetails.assetID, Equal<Current<FixedAsset.assetID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) fixedAsset
      }, Array.Empty<object>())) : throw new PXException("The operation cannot be completed because the fixed asset has already been disposed.");
      // ISSUE: reference to a compiler-generated field
      ((PXResultset<FixedAsset>) displayClass1160.list).Add((PXResult<FixedAsset>) new PXResult<FixedAsset, FADetails>(fixedAsset, faDetails));
      cost += faDetails.CurrentCost.GetValueOrDefault();
    }
    // ISSUE: reference to a compiler-generated field
    if (((PXResultset<FixedAsset>) displayClass1160.list).Count == 0)
    {
      // ISSUE: reference to a compiler-generated field
      return (IEnumerable) displayClass1160.list;
    }
    if (!adapter.MassProcess)
    {
      // ISSUE: method pointer
      ((PXSelectBase<DisposeParams>) this.DispParams).AskExt(new PXView.InitializePanel((object) displayClass1160, __methodptr(\u003CRunDisposal\u003Eb__0)));
      // ISSUE: reference to a compiler-generated field
      return (IEnumerable) displayClass1160.list;
    }
    // ISSUE: reference to a compiler-generated field
    this.Dispose(displayClass1160.list, cost, disposalDate, disposalPeriodID, disposalCost, disposalMethID, disposalAcctID, disposalSubID, dispAmtMode, deprBeforeDisposal, reason, adapter.MassProcess);
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) displayClass1160.list;
  }

  public void GLTranFilterPeriodIDFieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable RunSplit(
    PXAdapter adapter,
    [PXDate] DateTime? disposalDate,
    [PXString] string disposalPeriodID,
    [PXBaseCury] Decimal? disposalCost,
    [PXInt] int? disposalMethID,
    [PXInt] int? disposalAcctID,
    [PXInt] int? disposalSubID,
    [PXString] string dispAmtMode,
    [PXBool] bool? deprBeforeDisposal,
    [PXString] string reason,
    [PXInt] int? assetID)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AssetMaint.\u003C\u003Ec__DisplayClass119_0 displayClass1190 = new AssetMaint.\u003C\u003Ec__DisplayClass119_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1190.disposalDate = disposalDate;
    // ISSUE: reference to a compiler-generated field
    displayClass1190.disposalPeriodID = disposalPeriodID;
    // ISSUE: reference to a compiler-generated field
    displayClass1190.deprBeforeDisposal = deprBeforeDisposal;
    // ISSUE: reference to a compiler-generated field
    displayClass1190.\u003C\u003E4__this = this;
    List<SplitParams> splitParamsList = new List<SplitParams>();
    if (adapter.MassProcess)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      AssetMaint.\u003C\u003Ec__DisplayClass119_1 displayClass1191 = new AssetMaint.\u003C\u003Ec__DisplayClass119_1();
      // ISSUE: reference to a compiler-generated field
      displayClass1191.CS\u0024\u003C\u003E8__locals1 = displayClass1190;
      using (new AssetMaint.SplitOperationScope())
      {
        Numbering numbering = PXResultset<Numbering>.op_Implicit(((PXSelectBase<Numbering>) this.assetNumbering).Select(Array.Empty<object>()));
        // ISSUE: reference to a compiler-generated field
        displayClass1191.asset = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) assetID
        }));
        // ISSUE: reference to a compiler-generated field
        ((PXGraph) this).GetExtension<AssetMaint.AssetMaintFixedAssetChecksExtension>().CheckUnreleasedTransactions(displayClass1191.asset);
        FADetails faDetails1 = PXResultset<FADetails>.op_Implicit(PXSelectBase<FADetails, PXSelect<FADetails, Where<FADetails.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) assetID
        }));
        FALocationHistory faLocationHistory = PXResultset<FALocationHistory>.op_Implicit(PXSelectBase<FALocationHistory, PXSelect<FALocationHistory, Where<FALocationHistory.assetID, Equal<Current<FADetails.assetID>>, And<FALocationHistory.revisionID, Equal<Current<FADetails.locationRevID>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
        {
          (object) faDetails1
        }, Array.Empty<object>()));
        FABookBalance faBookBalance1 = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) assetID
        }));
        Decimal num1 = 0M;
        Decimal num2 = 0M;
        foreach (SplitParams splitParams in adapter.Get())
        {
          PXProcessing<SplitParams>.SetCurrentItem((object) splitParams);
          if (string.IsNullOrEmpty(splitParams.SplittedAssetCD) && numbering.UserNumbering.GetValueOrDefault())
            throw new PXSetPropertyException("Cannot create the fixed asset. To create a fixed asset, specify the Asset ID or clear the Manual Numbering check box on the Numbering Sequences (CS201010) form for the fixed asset sequence.");
          Decimal num3 = num1;
          Decimal? nullable = splitParams.Cost;
          Decimal valueOrDefault1 = nullable.GetValueOrDefault();
          num1 = num3 + valueOrDefault1;
          Decimal num4 = Math.Abs(num1);
          nullable = faBookBalance1.YtdDeprBase;
          Decimal num5 = Math.Abs(nullable.GetValueOrDefault());
          if (num4 > num5)
            throw new PXSetPropertyException("Total cost of splitted assets greater than cost of origin asset.");
          Decimal num6 = num2;
          nullable = splitParams.SplittedQty;
          Decimal valueOrDefault2 = nullable.GetValueOrDefault();
          num2 = num6 + valueOrDefault2;
          splitParamsList.Add(splitParams);
        }
        // ISSUE: reference to a compiler-generated field
        string formatTemplate = $"{PXMessages.LocalizeNoPrefix("split from")} {displayClass1191.asset.AssetCD}";
        // ISSUE: reference to a compiler-generated field
        if (!string.IsNullOrEmpty(displayClass1191.asset.Description))
          formatTemplate = $"{"{0}"} - {formatTemplate}";
        Dictionary<int, (Decimal, Decimal)> dictionary1 = new Dictionary<int, (Decimal, Decimal)>();
        Decimal num7 = 0M;
        // ISSUE: reference to a compiler-generated field
        displayClass1191.ratio = new Dictionary<FixedAsset, Decimal>();
        foreach (SplitParams splitParams in splitParamsList)
        {
          // ISSUE: reference to a compiler-generated field
          FixedAsset copy1 = (FixedAsset) ((PXSelectBase) this.Asset).Cache.CreateCopy((object) displayClass1191.asset);
          copy1.NoteID = new Guid?();
          copy1.AssetID = new int?();
          copy1.AssetCD = splitParams.SplittedAssetCD;
          copy1.ClassID = new int?();
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          copy1.Description = string.IsNullOrEmpty(displayClass1191.asset.Description) ? formatTemplate : ((PXGraph) this).MakeDescription<FixedAsset.description>(formatTemplate, displayClass1191.asset.Description);
          copy1.Qty = splitParams.SplittedQty;
          string tempKey = AssetGLTransactions.GetTempKey<FixedAsset.assetCD>(((PXSelectBase) this.Asset).Cache);
          FixedAsset fixedAsset = ((PXSelectBase<FixedAsset>) this.Asset).Insert(copy1);
          fixedAsset.AssetCD = splitParams.SplittedAssetCD ?? tempKey;
          ((PXSelectBase) this.Asset).Cache.Normalize();
          // ISSUE: reference to a compiler-generated field
          fixedAsset.ClassID = displayClass1191.asset.ClassID;
          // ISSUE: reference to a compiler-generated field
          fixedAsset.FAAccountID = displayClass1191.asset.FAAccountID;
          // ISSUE: reference to a compiler-generated field
          fixedAsset.FASubID = displayClass1191.asset.FASubID;
          // ISSUE: reference to a compiler-generated field
          fixedAsset.AccumulatedDepreciationAccountID = displayClass1191.asset.AccumulatedDepreciationAccountID;
          // ISSUE: reference to a compiler-generated field
          fixedAsset.AccumulatedDepreciationSubID = displayClass1191.asset.AccumulatedDepreciationSubID;
          // ISSUE: reference to a compiler-generated field
          fixedAsset.DepreciatedExpenseAccountID = displayClass1191.asset.DepreciatedExpenseAccountID;
          // ISSUE: reference to a compiler-generated field
          fixedAsset.DepreciatedExpenseSubID = displayClass1191.asset.DepreciatedExpenseSubID;
          // ISSUE: reference to a compiler-generated field
          fixedAsset.FAAccrualAcctID = displayClass1191.asset.FAAccrualAcctID;
          // ISSUE: reference to a compiler-generated field
          fixedAsset.FAAccrualSubID = displayClass1191.asset.FAAccrualSubID;
          // ISSUE: reference to a compiler-generated field
          fixedAsset.SplittedFrom = displayClass1191.asset.AssetID;
          FALocationHistory copy2 = (FALocationHistory) ((PXSelectBase) this.AssetLocation).Cache.CreateCopy((object) faLocationHistory);
          copy2.AssetID = fixedAsset.AssetID;
          copy2.RevisionID = ((PXSelectBase<FADetails>) this.AssetDetails).Current.LocationRevID;
          copy2.Department = faLocationHistory.Department;
          copy2.PeriodID = faLocationHistory.PeriodID;
          ((PXSelectBase<FALocationHistory>) this.AssetLocation).Update(copy2);
          // ISSUE: method pointer
          ((PXGraph) this).FieldVerifying.AddHandler<GLTranFilter.periodID>(new PXFieldVerifying((object) this, __methodptr(GLTranFilterPeriodIDFieldVerifying)));
          FADetails copy3 = (FADetails) ((PXSelectBase) this.AssetDetails).Cache.CreateCopy((object) faDetails1);
          copy3.AssetID = fixedAsset.AssetID;
          copy3.AcquisitionCost = splitParams.Cost;
          FADetails faDetails2 = copy3;
          Decimal? nullable1 = faDetails1.SalvageAmount;
          Decimal? ratio1 = splitParams.Ratio;
          Decimal? nullable2 = nullable1.HasValue & ratio1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * ratio1.GetValueOrDefault() / 100M) : new Decimal?();
          // ISSUE: reference to a compiler-generated field
          Decimal? nullable3 = new Decimal?(PXRounder.RoundCury(nullable2.GetValueOrDefault(), displayClass1191.asset.BaseCuryID));
          faDetails2.SalvageAmount = nullable3;
          copy3.LocationRevID = ((PXSelectBase<FADetails>) this.AssetDetails).Current.LocationRevID;
          FADetails faDetails3 = ((PXSelectBase<FADetails>) this.AssetDetails).Update(copy3);
          Decimal num8 = num7;
          nullable1 = faDetails3.SalvageAmount;
          Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
          num7 = num8 + valueOrDefault3;
          // ISSUE: method pointer
          ((PXGraph) this).FieldVerifying.RemoveHandler<GLTranFilter.periodID>(new PXFieldVerifying((object) this, __methodptr(GLTranFilterPeriodIDFieldVerifying)));
          // ISSUE: reference to a compiler-generated field
          foreach (PXResult<FABookBalance> pxResult in PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
          {
            (object) displayClass1191.asset
          }, Array.Empty<object>()))
          {
            FABookBalance faBookBalance2 = PXResult<FABookBalance>.op_Implicit(pxResult);
            FABookBalance copy4 = (FABookBalance) ((PXSelectBase) this.AssetBalance).Cache.CreateCopy((object) faBookBalance2);
            copy4.NoteID = new Guid?();
            copy4.AssetID = fixedAsset.AssetID;
            copy4.MaxHistoryPeriodID = (string) null;
            FABookBalance faBookBalance3 = copy4;
            nullable1 = new Decimal?();
            Decimal? nullable4 = nullable1;
            faBookBalance3.UsefulLife = nullable4;
            FABookBalance faBookBalance4 = ((PXSelectBase<FABookBalance>) this.AssetBalance).Insert(copy4);
            faBookBalance4.UsefulLife = faBookBalance2.UsefulLife;
            FABookBalance faBookBalance5 = faBookBalance4;
            Decimal? ytdAcquired = faBookBalance2.YtdAcquired;
            nullable2 = splitParams.Ratio;
            nullable1 = ytdAcquired.HasValue & nullable2.HasValue ? new Decimal?(ytdAcquired.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
            Decimal num9 = (Decimal) 100;
            Decimal? nullable5;
            if (!nullable1.HasValue)
            {
              nullable2 = new Decimal?();
              nullable5 = nullable2;
            }
            else
              nullable5 = new Decimal?(nullable1.GetValueOrDefault() / num9);
            nullable2 = nullable5;
            // ISSUE: reference to a compiler-generated field
            Decimal? nullable6 = new Decimal?(PXRounder.RoundCury(nullable2.GetValueOrDefault(), displayClass1191.asset.BaseCuryID));
            faBookBalance5.AcquisitionCost = nullable6;
            faBookBalance4.SalvageAmount = faDetails3.SalvageAmount;
            faBookBalance4.BonusRate = faBookBalance2.BonusRate;
            Dictionary<int, (Decimal, Decimal)> dictionary2 = dictionary1;
            int? bookId = faBookBalance2.BookID;
            int key1 = bookId.Value;
            (Decimal, Decimal) valueTuple1;
            ref (Decimal, Decimal) local = ref valueTuple1;
            if (!dictionary2.TryGetValue(key1, out local))
            {
              Dictionary<int, (Decimal, Decimal)> dictionary3 = dictionary1;
              bookId = faBookBalance2.BookID;
              int key2 = bookId.Value;
              nullable1 = faBookBalance2.BonusAmount;
              Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
              nullable1 = faBookBalance2.Tax179Amount;
              Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
              (Decimal, Decimal) valueTuple2;
              valueTuple1 = valueTuple2 = (valueOrDefault4, valueOrDefault5);
              dictionary3[key2] = valueTuple2;
            }
            FABookBalance faBookBalance6 = faBookBalance4;
            Decimal num10 = valueTuple1.Item1;
            nullable1 = splitParams.Ratio;
            Decimal? nullable7;
            if (!nullable1.HasValue)
            {
              nullable2 = new Decimal?();
              nullable7 = nullable2;
            }
            else
              nullable7 = new Decimal?(num10 * nullable1.GetValueOrDefault() / 100M);
            nullable2 = nullable7;
            // ISSUE: reference to a compiler-generated field
            Decimal? nullable8 = new Decimal?(PXRounder.RoundCury(nullable2.GetValueOrDefault(), displayClass1191.asset.BaseCuryID));
            faBookBalance6.BonusAmount = nullable8;
            FABookBalance faBookBalance7 = faBookBalance4;
            Decimal num11 = valueTuple1.Item2;
            nullable1 = splitParams.Ratio;
            Decimal? nullable9;
            if (!nullable1.HasValue)
            {
              nullable2 = new Decimal?();
              nullable9 = nullable2;
            }
            else
              nullable9 = new Decimal?(num11 * nullable1.GetValueOrDefault() / 100M);
            nullable2 = nullable9;
            // ISSUE: reference to a compiler-generated field
            Decimal? nullable10 = new Decimal?(PXRounder.RoundCury(nullable2.GetValueOrDefault(), displayClass1191.asset.BaseCuryID));
            faBookBalance7.Tax179Amount = nullable10;
            nullable1 = faBookBalance2.BonusAmount;
            nullable2 = faBookBalance4.BonusAmount;
            Decimal? nullable11 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
            FABookBalance faBookBalance8 = faBookBalance2;
            nullable2 = faBookBalance8.Tax179Amount;
            nullable1 = faBookBalance4.Tax179Amount;
            faBookBalance8.Tax179Amount = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
            ((PXSelectBase<FABookBalance>) this.AssetBalance).Update(faBookBalance2);
            ((PXSelectBase<FABookBalance>) this.AssetBalance).SetValueExt<FABookBalance.bonusAmount>(faBookBalance2, (object) nullable11);
          }
          ((PXAction) this.Save).Press();
          splitParams.SplittedAssetCD = fixedAsset.AssetCD;
          // ISSUE: reference to a compiler-generated field
          Dictionary<FixedAsset, Decimal> ratio2 = displayClass1191.ratio;
          FixedAsset key = fixedAsset;
          nullable1 = splitParams.Ratio;
          Decimal num12 = nullable1.Value;
          ratio2.Add(key, num12);
        }
        // ISSUE: reference to a compiler-generated field
        FixedAsset asset1 = displayClass1191.asset;
        Decimal? nullable12 = asset1.Qty;
        Decimal num13 = num2;
        asset1.Qty = nullable12.HasValue ? new Decimal?(nullable12.GetValueOrDefault() - num13) : new Decimal?();
        // ISSUE: reference to a compiler-generated field
        nullable12 = displayClass1191.asset.Qty;
        Decimal num14 = 0M;
        if (nullable12.GetValueOrDefault() <= num14 & nullable12.HasValue)
        {
          // ISSUE: reference to a compiler-generated field
          displayClass1191.asset.Qty = new Decimal?(1M);
        }
        // ISSUE: reference to a compiler-generated field
        ((PXSelectBase<FixedAsset>) this.Asset).Update(displayClass1191.asset);
        ((PXAction) this.Save).Press();
        // ISSUE: reference to a compiler-generated field
        FixedAsset asset2 = displayClass1191.asset;
        nullable12 = faDetails1.SalvageAmount;
        Decimal num15 = num7;
        Decimal? nullable13 = nullable12.HasValue ? new Decimal?(nullable12.GetValueOrDefault() - num15) : new Decimal?();
        asset2.SalvageAmtAfterSplit = nullable13;
      }
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1191, __methodptr(\u003CRunSplit\u003Eb__1)));
      return (IEnumerable) splitParamsList;
    }
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1190, __methodptr(\u003CRunSplit\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Action(
    PXAdapter adapter,
    [PXDate] DateTime? disposalDate,
    [PXString] string disposalPeriodID,
    [PXBaseCury] Decimal? disposalCost,
    [PXInt] int? disposalMethID,
    [PXInt] int? disposalAcctID,
    [PXInt] int? disposalSubID,
    [PXString] string dispAmtMode,
    [PXBool] bool? deprBeforeDisposal,
    [PXString] string reason,
    [PXInt] int? assetID)
  {
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable calculateDepreciation(PXAdapter adapter)
  {
    FixedAsset current = ((PXSelectBase<FixedAsset>) this.Asset).Current;
    FADetails faDetails = PXResultset<FADetails>.op_Implicit(((PXSelectBase<FADetails>) this.AssetDetails).Select(Array.Empty<object>()));
    if (current != null && current.ClassID.HasValue && faDetails != null && faDetails.DepreciateFromDate.HasValue)
    {
      ((PXAction) this.Save).Press();
      this.DepreciationCalculation(current);
      ((PXAction) this.Save).Press();
    }
    ((PXGraph) this).Caches[typeof (FABookPeriod)].ClearQueryCache();
    ((PXGraph) this).Caches[typeof (FABookHistory)].ClearQueryCache();
    return !((PXGraph) this).IsContractBasedAPI && !((PXGraph) this).IsImport ? ((PXAction) this.Cancel).Press(adapter) : adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable suspend(PXAdapter adapter, [PXString(6)] string CurrentPeriodID)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    AssetMaint.\u003C\u003Ec__DisplayClass125_0 displayClass1250 = new AssetMaint.\u003C\u003Ec__DisplayClass125_0();
    // ISSUE: reference to a compiler-generated field
    displayClass1250.CurrentPeriodID = CurrentPeriodID;
    // ISSUE: reference to a compiler-generated field
    displayClass1250.list = adapter.Get().Cast<FixedAsset>().ToList<FixedAsset>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (!adapter.MassProcess && displayClass1250.list.Count > 0 && displayClass1250.list[0].Suspended.GetValueOrDefault())
    {
      if (((PXSelectBase) this.SuspendParams).View.Answer == null)
        ((PXSelectBase) this.SuspendParams).Cache.SetDefaultExt<PX.Objects.FA.SuspendParams.currentPeriodID>((object) ((PXSelectBase<PX.Objects.FA.SuspendParams>) this.SuspendParams).Current);
      if (((PXSelectBase<PX.Objects.FA.SuspendParams>) this.SuspendParams).AskExt() != 1)
        return adapter.Get();
      // ISSUE: reference to a compiler-generated field
      displayClass1250.CurrentPeriodID = ((PXSelectBase<PX.Objects.FA.SuspendParams>) this.SuspendParams).Current.CurrentPeriodID;
    }
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) displayClass1250, __methodptr(\u003Csuspend\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable reduceUnreconCost(PXAdapter adapter)
  {
    FixedAsset current1 = ((PXSelectBase<FixedAsset>) this.CurrentAsset).Current;
    GLTranFilter current2 = ((PXSelectBase<GLTranFilter>) this.GLTrnFilter).Current;
    Decimal? currentCost = current2.CurrentCost;
    Decimal? expectedCost = current2.ExpectedCost;
    if (!(currentCost.GetValueOrDefault() == expectedCost.GetValueOrDefault() & currentCost.HasValue == expectedCost.HasValue))
    {
      PX.Objects.FA.AssetProcess.RestrictAdditonDeductionForCalcMethod((PXGraph) this, current1.AssetID, "PC");
      PX.Objects.FA.AssetProcess.RestrictAdditonDeductionForCalcMethod((PXGraph) this, current1.AssetID, "ZL");
      PX.Objects.FA.AssetProcess.RestrictAdditonDeductionForCalcMethod((PXGraph) this, current1.AssetID, "LE");
    }
    if (((PXSelectBase<FARegister>) this.Register).Current == null || ((PXSelectBase<FARegister>) this.Register).Current.Released.GetValueOrDefault())
      AssetGLTransactions.SetCurrentRegister(this.Register, current1.BranchID.Value);
    foreach (PXResult<FABookBalance> pxResult in PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      FABookBalance faBookBalance = PXResult<FABookBalance>.op_Implicit(pxResult);
      ((PXSelectBase<FATran>) this.FATransactions).Insert(new FATran()
      {
        AssetID = current1.AssetID,
        BookID = faBookBalance.BookID,
        TranAmt = current2.UnreconciledAmt,
        TranDate = current2.TranDate,
        TranType = "P-",
        CreditAccountID = current1.FAAccountID,
        CreditSubID = current1.FASubID,
        DebitAccountID = current1.FAAccrualAcctID,
        DebitSubID = current1.FAAccrualSubID,
        TranDesc = PXMessages.LocalizeFormatNoPrefix("Deduction Unreconciled Cost for Asset {0}", new object[1]
        {
          (object) current1.AssetCD
        })
      });
    }
    return adapter.Get();
  }

  private void DepreciationCalculation(FixedAsset fixedAsset)
  {
    if (!fixedAsset.Depreciable.GetValueOrDefault() || fixedAsset.UnderConstruction.GetValueOrDefault())
      return;
    PX.Objects.FA.DepreciationCalculation instance = PXGraph.CreateInstance<PX.Objects.FA.DepreciationCalculation>();
    foreach (PXResult<FABookBalance> pxResult in PXSelectBase<FABookBalance, PXSelectReadonly<FABookBalance, Where<FABookBalance.assetID, Equal<Required<FABookBalance.assetID>>, And<FABookBalance.depreciationMethodID, IsNotNull>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) fixedAsset.AssetID
    }))
    {
      FABookBalance assetBalance = PXResult<FABookBalance>.op_Implicit(pxResult);
      ((PXGraph) instance).Clear();
      ((PXCache) GraphHelper.Caches<FABookBalance>((PXGraph) instance)).SetStatus((object) assetBalance, (PXEntryStatus) 0);
      instance.Calculate(assetBalance, uiGraph: (PXGraph) this);
    }
  }

  private static void CopyLocation(FALocationHistory from, FALocationHistory to)
  {
    to.ClassID = from.ClassID;
    to.LocationID = from.LocationID;
    to.BuildingID = from.BuildingID;
    to.Floor = from.Floor;
    to.Room = from.Room;
    to.EmployeeID = from.EmployeeID;
    to.Department = from.Department;
    to.Reason = from.Reason;
    to.FAAccountID = from.FAAccountID;
    to.FASubID = from.FASubID;
    to.AccumulatedDepreciationAccountID = from.AccumulatedDepreciationAccountID;
    to.AccumulatedDepreciationSubID = from.AccumulatedDepreciationSubID;
    to.DepreciatedExpenseAccountID = from.DepreciatedExpenseAccountID;
    to.DepreciatedExpenseSubID = from.DepreciatedExpenseSubID;
    to.DisposalAccountID = from.DisposalAccountID;
    to.DisposalSubID = from.DisposalSubID;
    to.GainAcctID = from.GainAcctID;
    to.GainSubID = from.GainSubID;
    to.LossAcctID = from.LossAcctID;
    to.LossSubID = from.LossSubID;
  }

  public static (Decimal MinValue, Decimal MaxValue) GetSignedRange(Decimal? value)
  {
    return (Math.Min(0M, value.GetValueOrDefault()), Math.Max(0M, value.GetValueOrDefault()));
  }

  public static bool IsValueInSignedRange(
    Decimal? value,
    Decimal? boundValue,
    out (Decimal MinValue, Decimal MaxValue) range)
  {
    range = AssetMaint.GetSignedRange(boundValue);
    Decimal? nullable1 = value;
    Decimal num1 = range.Item1;
    if (!(nullable1.GetValueOrDefault() >= num1 & nullable1.HasValue))
      return false;
    Decimal? nullable2 = value;
    Decimal num2 = range.Item2;
    return nullable2.GetValueOrDefault() <= num2 & nullable2.HasValue;
  }

  protected bool HasAnyGLTran(int? assetID)
  {
    return ((IQueryable<PXResult<FATran>>) PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.assetID, Equal<P.AsInt>>>>>.And<BqlOperand<FATran.batchNbr, IBqlString>.IsNotNull>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) assetID
    })).Any<PXResult<FATran>>();
  }

  public static void CheckBookBalanceParameters(
    PXGraph graph,
    bool? UpdateGL,
    int? branchID,
    FABookBalance bookBalance)
  {
    if (FlaggedModeScopeBase<AssetMaint.SplitOperationScope>.IsActive)
      return;
    PXAccess.Organization parentOrganization = PXAccess.GetParentOrganization(branchID);
    PXCache cach = graph.Caches[typeof (FABookBalance)];
    if (UpdateGL.GetValueOrDefault())
    {
      if (AssetMaint.IsPostingBookBalanceParametersValidInNonMigrationMode(graph, (int?) parentOrganization?.OrganizationID, bookBalance))
        return;
      cach.RaiseExceptionHandling<FABookBalance.deprToPeriod>((object) bookBalance, (object) bookBalance.DeprToPeriod, (Exception) new PXSetPropertyException("The fixed asset cannot be created because the asset's Depr. to Period is closed in the posting book for the {0} company.", new object[1]
      {
        (object) parentOrganization?.OrganizationCD
      }));
    }
    else
    {
      if (!AssetMaint.IsPostingBookBalanceParametersValidInMigrationMode(graph, (int?) parentOrganization?.OrganizationID, bookBalance))
        cach.RaiseExceptionHandling<FABookBalance.deprToPeriod>((object) bookBalance, (object) bookBalance.DeprToPeriod, (Exception) new PXSetPropertyException("The fixed asset cannot be created because it has nonzero net book value and the asset's Depr. to Period is closed in the posting book for the {0} company.", new object[1]
        {
          (object) parentOrganization?.OrganizationCD
        }));
      if (bookBalance.LastDeprPeriod != null)
        return;
      Decimal? ytdDepreciated = bookBalance.YtdDepreciated;
      Decimal num = 0M;
      if (ytdDepreciated.GetValueOrDefault() == num & ytdDepreciated.HasValue)
        return;
      cach.RaiseExceptionHandling<FABookBalance.lastDeprPeriod>((object) bookBalance, (object) bookBalance.LastDeprPeriod, (Exception) new PXSetPropertyException("Last Depr. Period cannot be empty for a fixed asset in the book with nonzero accumulated depreciation."));
    }
  }

  public static bool IsPostingBookBalanceParametersValidInNonMigrationMode(
    PXGraph graph,
    int? organizationID,
    FABookBalance bookBalance)
  {
    bool flag = true;
    if (bookBalance.UpdateGL.GetValueOrDefault() && bookBalance.DeprToPeriod != null && !AssetMaint.IsFinPeriodValid(graph, organizationID, bookBalance.DeprToPeriod))
    {
      Decimal? acquisitionCost = bookBalance.AcquisitionCost;
      Decimal num = 0M;
      if (!(acquisitionCost.GetValueOrDefault() == num & acquisitionCost.HasValue))
        flag = false;
    }
    return flag;
  }

  public static bool IsPostingBookBalanceParametersValidInMigrationMode(
    PXGraph graph,
    int? organizationID,
    FABookBalance bookBalance)
  {
    bool flag = true;
    if (bookBalance.UpdateGL.GetValueOrDefault() && bookBalance.DeprToPeriod != null && !AssetMaint.IsFinPeriodValid(graph, organizationID, bookBalance.DeprToPeriod) && bookBalance.LastDeprPeriod == null)
    {
      Decimal? acquisitionCost = bookBalance.AcquisitionCost;
      Decimal num = 0M;
      if (!(acquisitionCost.GetValueOrDefault() == num & acquisitionCost.HasValue))
        flag = false;
    }
    return flag;
  }

  public static bool IsFinPeriodValid(
    PXGraph graph,
    int? calendarOrganizationID,
    string finPeriodID)
  {
    FinPeriod byId = graph.GetService<IFinPeriodRepository>().FindByID(calendarOrganizationID, finPeriodID);
    return byId == null || !byId.FAClosed.GetValueOrDefault();
  }

  protected virtual void FixedAsset_RecordType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) "A";
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void FAClass_RecordType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) "C";
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void FADetails_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    FADetails row = (FADetails) e.Row;
    if (row == null)
      return;
    FATran tran = PXResultset<FATran>.op_Implicit(PXSelectBase<FATran, PXSelect<FATran, Where<FATran.assetID, Equal<Required<FADetails.assetID>>>, OrderBy<Desc<Switch<Case<Where<FATran.origin, Equal<FARegister.origin.depreciation>>, intMax, Case<Where<FATran.origin, Equal<FARegister.origin.purchasing>>, int2, Case<Where<FATran.origin, Equal<FARegister.origin.reconcilliation>>, int1>>>, int0>, Desc<Switch<Case<Where<FATran.tranType, Equal<FATran.tranType.depreciationPlus>>, intMax, Case<Where<FATran.tranType, Equal<FATran.tranType.depreciationMinus>>, intMax, Case<Where<FATran.tranType, Equal<FATran.tranType.calculatedPlus>>, intMax, Case<Where<FATran.tranType, Equal<FATran.tranType.calculatedMinus>>, intMax>>>>, int0>, Desc<FATran.released, Asc<FATran.refNbr, Asc<FATran.lineNbr>>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) row.AssetID
    }));
    bool flag1 = AssetMaint.IsDepreciated(tran);
    bool flag2 = flag1 || AssetMaint.IsPurchased(tran);
    bool? nullable = ((PXSelectBase<FixedAsset>) this.Asset).Current.UnderConstruction;
    bool valueOrDefault = nullable.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<FADetails.receiptDate>(sender, (object) row, !flag2);
    PXUIFieldAttribute.SetEnabled<FADetails.depreciateFromDate>(sender, (object) row, !flag1);
    PXUIFieldAttribute.SetRequired<FADetails.depreciateFromDate>(sender, !valueOrDefault);
    PXCache pxCache = sender;
    FADetails faDetails = row;
    int num;
    if (((PXSelectBase<FASetup>) this.fasetup).Current.TagNumberingID == null)
    {
      nullable = ((PXSelectBase<FASetup>) this.fasetup).Current.CopyTagFromAssetID;
      num = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 0;
    PXUIFieldAttribute.SetEnabled<FADetails.tagNbr>(pxCache, (object) faDetails, num != 0);
    if (!string.IsNullOrEmpty(PXUIFieldAttribute.GetError<FADetails.depreciateFromDate>(sender, (object) row)))
      return;
    PXSetPropertyException propertyException = !valueOrDefault || !row.DepreciateFromDate.HasValue ? (PXSetPropertyException) null : (PXSetPropertyException) new PXSetPropertyException<FADetails.depreciateFromDate>("The fixed asset is under construction and cannot be depreciated.", (PXErrorLevel) 2);
    sender.RaiseExceptionHandling<FADetails.depreciateFromDate>(e.Row, (object) row.DepreciateFromDate, (Exception) propertyException);
  }

  protected virtual void FADetails_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    FADetails row = (FADetails) e.Row;
    if (sender.ObjectsEqual<FADetails.receiptDate>(e.Row, e.OldRow))
      return;
    PXResultset<FALocationHistory> pxResultset = ((PXSelectBase<FALocationHistory>) this.LocationHistory).SelectWindowed(0, 2, Array.Empty<object>());
    if (pxResultset.Count != 1)
      return;
    ((PXSelectBase<FALocationHistory>) this.AssetLocation).Current = PXResult<FALocationHistory>.op_Implicit(pxResultset[0]);
    ((PXSelectBase<FALocationHistory>) this.AssetLocation).Current.TransactionDate = row.ReceiptDate;
    ((PXSelectBase<FALocationHistory>) this.AssetLocation).Current.PeriodID = this.FinPeriodRepository.FindFinPeriodByDate(row.DepreciateFromDate, PXAccess.GetParentOrganizationID(((PXSelectBase<FixedAsset>) this.CurrentAsset).Current.BranchID))?.FinPeriodID;
    ((PXSelectBase<FALocationHistory>) this.AssetLocation).Update(((PXSelectBase<FALocationHistory>) this.AssetLocation).Current);
  }

  protected virtual void FADetails_Hold_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    FixedAsset current = ((PXSelectBase<FixedAsset>) this.Asset).Current;
    if (current == null)
      return;
    PXBoolAttribute.ConvertValue(e);
    current.HoldEntry = new bool?(((bool?) e.NewValue).GetValueOrDefault());
    PXEntityEventBase<FixedAsset>.Container<FixedAsset.Events>.FireOnPropertyChanged<FixedAsset.classID>((PXGraph) this, (FixedAsset.Events) current);
  }

  protected virtual bool IsSplittedFixedAsset(FixedAsset fixedAsset)
  {
    if (fixedAsset.SplittedFrom.HasValue)
      return true;
    return ((IQueryable<PXResult<FixedAsset>>) PXSelectBase<FixedAsset, PXViewOf<FixedAsset>.BasedOn<SelectFromBase<FixedAsset, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FixedAsset.splittedFrom, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fixedAsset.AssetID
    })).Any<PXResult<FixedAsset>>();
  }

  protected virtual bool IsFixedAssetAccountSubUsed(FixedAsset fixedAsset)
  {
    return ((IQueryable<PXResult<FATran>>) PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FATran.released, IBqlBool>.IsEqual<True>>>, And<BqlOperand<FATran.batchNbr, IBqlString>.IsNotNull>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.tranType, Equal<FATran.tranType.purchasingPlus>>>>, Or<BqlOperand<FATran.tranType, IBqlString>.IsEqual<FATran.tranType.purchasingMinus>>>>.Or<BqlOperand<FATran.tranType, IBqlString>.IsEqual<FATran.tranType.transferPurchasing>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fixedAsset.AssetID
    })).Any<PXResult<FATran>>();
  }

  protected virtual bool IsAccumulatedDepreciationAccountSubUsed(FixedAsset fixedAsset)
  {
    return ((IQueryable<PXResult<FATran>>) PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FATran.released, IBqlBool>.IsEqual<True>>>, And<BqlOperand<FATran.batchNbr, IBqlString>.IsNotNull>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.tranType, Equal<FATran.tranType.depreciationPlus>>>>, Or<BqlOperand<FATran.tranType, IBqlString>.IsEqual<FATran.tranType.depreciationMinus>>>, Or<BqlOperand<FATran.tranType, IBqlString>.IsEqual<FATran.tranType.adjustingDeprPlus>>>, Or<BqlOperand<FATran.tranType, IBqlString>.IsEqual<FATran.tranType.adjustingDeprMinus>>>>.Or<BqlOperand<FATran.tranType, IBqlString>.IsEqual<FATran.tranType.transferDepreciation>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fixedAsset.AssetID
    })).Any<PXResult<FATran>>();
  }

  protected virtual bool HasSplittedDepreciation(FixedAsset fixedAsset)
  {
    return ((IQueryable<PXResult<FATran>>) PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FABook>.On<BqlOperand<FATran.bookID, IBqlInt>.IsEqual<FABook.bookID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.assetID, Equal<P.AsInt>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.tranType, Equal<FATran.tranType.adjustingDeprPlus>>>>>.Or<BqlOperand<FATran.tranType, IBqlString>.IsEqual<FATran.tranType.adjustingDeprMinus>>>>>.And<BqlOperand<FABook.updateGL, IBqlBool>.IsEqual<True>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) fixedAsset.AssetID
    })).Any<PXResult<FATran>>();
  }

  protected virtual Dictionary<System.Type, System.Type> GetFAAccountSubPairs()
  {
    return this.AccountSubPairs;
  }

  protected virtual bool IsAccountChangeable<AccountSubField>(FixedAsset fixedAsset) where AccountSubField : IBqlField
  {
    return this.IsAccountChangeable(fixedAsset, typeof (AccountSubField));
  }

  protected virtual bool IsAccountChangeable(FixedAsset fixedAsset, System.Type accountSubField)
  {
    if (((IEnumerable<System.Type>) new System.Type[4]
    {
      typeof (FixedAsset.fAAccountID),
      typeof (FixedAsset.fASubID),
      typeof (FixedAsset.fAAccrualAcctID),
      typeof (FixedAsset.fAAccrualSubID)
    }).Contains<System.Type>(accountSubField))
      return !this.IsFixedAssetAccountSubUsed(fixedAsset) && !this.IsSplittedFixedAsset(fixedAsset);
    if (((IEnumerable<System.Type>) new System.Type[2]
    {
      typeof (FixedAsset.accumulatedDepreciationAccountID),
      typeof (FixedAsset.accumulatedDepreciationSubID)
    }).Contains<System.Type>(accountSubField))
    {
      if (this.IsAccumulatedDepreciationAccountSubUsed(fixedAsset))
        return false;
      return !this.IsSplittedFixedAsset(fixedAsset) || !this.HasSplittedDepreciation(fixedAsset);
    }
    Dictionary<System.Type, System.Type> faAccountSubPairs = this.GetFAAccountSubPairs();
    if (faAccountSubPairs.Keys.Contains<System.Type>(accountSubField) || faAccountSubPairs.Values.Contains<System.Type>(accountSubField))
      return true;
    throw new ArgumentOutOfRangeException(accountSubField.FullName);
  }

  protected virtual void FixedAsset_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    FixedAsset row = (FixedAsset) e.Row;
    if (row == null)
      return;
    PXAction<FixedAsset> suspend1 = this.Suspend;
    bool? nullable = row.Suspended;
    string str = nullable.GetValueOrDefault() ? "Unsuspend" : "Suspend";
    ((PXAction) suspend1).SetCaption(str);
    PXAction<FixedAsset> suspend2 = this.Suspend;
    nullable = row.Depreciable;
    int num1 = nullable.GetValueOrDefault() ? 1 : 0;
    ((PXAction) suspend2).SetEnabled(num1 != 0);
    bool flag1 = row.Status != "D" && row.Status != "R";
    sender.AllowUpdate = flag1;
    sender.AllowDelete = flag1;
    ((PXSelectBase) this.AssetBalance).Cache.AllowInsert = flag1;
    ((PXSelectBase) this.AssetBalance).Cache.AllowUpdate = flag1;
    ((PXSelectBase) this.AssetBalance).Cache.AllowDelete = flag1;
    ((PXSelectBase) this.AssetUsage).Cache.AllowInsert = row.UsageScheduleID.HasValue & flag1;
    ((PXSelectBase) this.AssetUsage).Cache.AllowUpdate = flag1;
    ((PXSelectBase) this.AssetUsage).Cache.AllowDelete = flag1;
    ((PXSelectBase) this.FATransactions).Cache.AllowDelete = flag1;
    bool flag2 = sender.GetStatus((object) row) == 2;
    ((PXSelectBase) this.AssetHistory).Cache.AllowInsert = !flag2;
    ((PXSelectBase) this.BookSheetHistory).Cache.AllowInsert = !flag2;
    PXAction<FixedAsset> calculateDepreciation = this.CalculateDepreciation;
    int num2;
    if (row.Status == "A")
    {
      nullable = row.Depreciable;
      if (nullable.GetValueOrDefault())
      {
        nullable = row.UnderConstruction;
        bool flag3 = false;
        if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
        {
          FATran faTran = ((PXSelectBase<FATran>) this.transferTransactions).SelectSingle(Array.Empty<object>());
          if (faTran == null)
          {
            num2 = 1;
            goto label_8;
          }
          nullable = faTran.Released;
          bool flag4 = false;
          num2 = !(nullable.GetValueOrDefault() == flag4 & nullable.HasValue) ? 1 : 0;
          goto label_8;
        }
      }
    }
    num2 = 0;
label_8:
    ((PXAction) calculateDepreciation).SetEnabled(num2 != 0);
    bool flag5 = this.IsAccountChangeable<FixedAsset.fAAccountID>(row);
    bool flag6 = this.IsAccountChangeable<FixedAsset.accumulatedDepreciationAccountID>(row);
    PXUIFieldAttribute.SetEnabled<FixedAsset.fAAccountID>(sender, (object) row, flag5);
    PXUIFieldAttribute.SetEnabled<FixedAsset.fASubID>(sender, (object) row, flag5);
    PXUIFieldAttribute.SetEnabled<FixedAsset.fAAccrualAcctID>(sender, (object) row, flag5);
    PXUIFieldAttribute.SetEnabled<FixedAsset.fAAccrualSubID>(sender, (object) row, flag5);
    PXUIFieldAttribute.SetEnabled<FixedAsset.accumulatedDepreciationAccountID>(sender, (object) row, flag6);
    PXUIFieldAttribute.SetEnabled<FixedAsset.accumulatedDepreciationSubID>(sender, (object) row, flag6);
    FADepreciationMethod depreciationMethod = PXResultset<FADepreciationMethod>.op_Implicit(PXSelectBase<FADepreciationMethod, PXSelectJoin<FADepreciationMethod, InnerJoin<FABookBalance, On<FADepreciationMethod.methodID, Equal<FABookBalance.depreciationMethodID>>>, Where<FADepreciationMethod.usefulLife, IsNotNull, And<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    bool flag7 = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>, And<FABookBalance.status, NotEqual<FixedAssetStatus.active>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>())) != null;
    PXUIFieldAttribute.SetEnabled<FixedAsset.usefulLife>(sender, (object) row, depreciationMethod == null && !flag7);
    nullable = row.Depreciable;
    bool valueOrDefault = nullable.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<DisposeParams.actionBeforeDisposal>(((PXSelectBase) this.DispParams).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetDisplayName<FABookBalance.deprFromDate>(((PXSelectBase) this.AssetBalance).Cache, valueOrDefault ? "Depr. From" : "Placed-in-Service Date");
    PXUIFieldAttribute.SetDisplayName<FABookBalance.deprFromPeriod>(((PXSelectBase) this.AssetBalance).Cache, valueOrDefault ? "Depr. From Period" : "Placed-in-Service Period");
    PXUIFieldAttribute.SetVisible<FABookBalance.deprToDate>(((PXSelectBase) this.AssetBalance).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<FABookBalance.deprToPeriod>(((PXSelectBase) this.AssetBalance).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<FABookBalance.lastDeprPeriod>(((PXSelectBase) this.AssetBalance).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<FABookBalance.salvageAmount>(((PXSelectBase) this.AssetBalance).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<FABookBalance.ytdDepreciated>(((PXSelectBase) this.AssetBalance).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<FABookBalance.averagingConvention>(((PXSelectBase) this.AssetBalance).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<FABookBalance.tax179Amount>(((PXSelectBase) this.AssetBalance).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<FABookBalance.ytdTax179Recap>(((PXSelectBase) this.AssetBalance).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<FABookBalance.bonusID>(((PXSelectBase) this.AssetBalance).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<FABookBalance.bonusRate>(((PXSelectBase) this.AssetBalance).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<FABookBalance.bonusAmount>(((PXSelectBase) this.AssetBalance).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<FABookBalance.ytdBonusRecap>(((PXSelectBase) this.AssetBalance).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<FABookBalance.depreciationMethodID>(((PXSelectBase) this.AssetBalance).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<FABookBalance.midMonthType>(((PXSelectBase) this.AssetBalance).Cache, (object) null, valueOrDefault);
    PXUIFieldAttribute.SetVisible<FABookBalance.midMonthDay>(((PXSelectBase) this.AssetBalance).Cache, (object) null, valueOrDefault);
    PXFilter<DeprBookFilter> deprbookfilter1 = this.deprbookfilter;
    nullable = row.UnderConstruction;
    int num3 = !nullable.GetValueOrDefault() ? 1 : 0;
    ((PXSelectBase) deprbookfilter1).AllowSelect = num3 != 0;
    PXSelectOrderBy<FAHistory, OrderBy<Asc<FAHistory.finPeriodID>>> assetHistory = this.AssetHistory;
    nullable = row.Depreciable;
    int num4;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.UnderConstruction;
      if (!nullable.GetValueOrDefault())
      {
        num4 = ((PXSelectBase<FASetup>) this.fasetup).Current.DeprHistoryView == "S" ? 1 : 0;
        goto label_12;
      }
    }
    num4 = 0;
label_12:
    ((PXSelectBase) assetHistory).AllowSelect = num4 != 0;
    PXSelectOrderBy<FASheetHistory, OrderBy<Asc<FASheetHistory.periodNbr>>> bookSheetHistory = this.BookSheetHistory;
    PXFilter<DeprBookFilter> deprbookfilter2 = this.deprbookfilter;
    nullable = row.Depreciable;
    int num5;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.UnderConstruction;
      if (!nullable.GetValueOrDefault())
      {
        num5 = ((PXSelectBase<FASetup>) this.fasetup).Current.DeprHistoryView == "B" ? 1 : 0;
        goto label_16;
      }
    }
    num5 = 0;
label_16:
    bool flag8 = num5 != 0;
    ((PXSelectBase) deprbookfilter2).AllowSelect = num5 != 0;
    int num6 = flag8 ? 1 : 0;
    ((PXSelectBase) bookSheetHistory).AllowSelect = num6 != 0;
    bool flag9 = row.Status != "R" && row.Status != "D";
    ((PXSelectBase) this.CurrentAsset).Cache.AllowUpdate = flag9;
    ((PXSelectBase) this.AssetDetails).Cache.AllowUpdate = flag9;
    ((PXSelectBase) this.AssetLocation).Cache.AllowUpdate = flag9;
    ((PXSelectBase) this.GLTrnFilter).Cache.AllowSelect = flag9;
    ((PXSelectBase) this.DsplAdditions).Cache.AllowSelect = flag9;
  }

  protected virtual void FixedAsset_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    ((PXSelectBase) this.AssetDetails).Cache.Insert();
    ((PXSelectBase) this.AssetLocation).Cache.Insert();
    ((PXSelectBase) this.AssetLocation).Cache.IsDirty = false;
    ((PXSelectBase) this.AssetDetails).Cache.IsDirty = false;
  }

  protected virtual DateTime? GetTransferDate(int? assetID, string transferPeriodID)
  {
    FADetails faDetails = PXResultset<FADetails>.op_Implicit(PXSelectBase<FADetails, PXViewOf<FADetails>.BasedOn<SelectFromBase<FADetails, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FADetails.assetID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) assetID
    }));
    FABookBalance faBookBalance = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXViewOf<FABookBalance>.BasedOn<SelectFromBase<FABookBalance, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FABookBalance.assetID, IBqlInt>.IsEqual<P.AsInt>>.Order<PX.Data.BQL.Fluent.By<Desc<FABookBalance.updateGL>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) assetID
    }));
    DateTime? transferDate = ((PXGraph) this).Accessinfo.BusinessDate;
    FABookPeriod faBookPeriodById = this.FABookPeriodRepository.FindOrganizationFABookPeriodByID(transferPeriodID, faBookBalance.BookID, assetID);
    DateTime? nullable1 = faBookPeriodById.StartDate;
    DateTime? nullable2 = transferDate;
    if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
    {
      nullable2 = faBookPeriodById.EndDate;
      nullable1 = transferDate;
      if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
        goto label_3;
    }
    transferDate = faBookPeriodById.StartDate;
label_3:
    nullable1 = transferDate;
    nullable2 = faDetails.ReceiptDate;
    if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      transferDate = faDetails.ReceiptDate;
    return transferDate;
  }

  protected virtual void FixedAsset_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    FixedAsset newRow = e.NewRow as FixedAsset;
    FixedAsset row = e.Row as FixedAsset;
    if (newRow == null || !((PXGraph) this).WillAccountsBeChanged(newRow, row, out int? _, out int? _, out int? _, out int? _) || !FAInnerStateDescriptor.IsAcquired(newRow.AssetID, (PXGraph) this) || FAInnerStateDescriptor.WillBeTransferred(newRow.AssetID, (PXGraph) this))
      return;
    if (((PXSelectBase<FADetails>) this.AssetDetails).Current == null)
      ((PXSelectBase<FADetails>) this.AssetDetails).Current = PXResultset<FADetails>.op_Implicit(((PXSelectBase<FADetails>) this.AssetDetails).Select(Array.Empty<object>()));
    if (((PXSelectBase<FALocationHistory>) this.AssetLocation).Current == null)
      ((PXSelectBase<FALocationHistory>) this.AssetLocation).Current = PXResultset<FALocationHistory>.op_Implicit(((PXSelectBase<FALocationHistory>) this.AssetLocation).Select(Array.Empty<object>()));
    FALocationHistory copy = PXCache<FALocationHistory>.CreateCopy(((PXSelectBase<FALocationHistory>) this.AssetLocation).Current);
    if (!((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault() && !this.HasAnyGLTran(newRow.AssetID))
    {
      ((PXSelectBase) this.AssetLocation).Cache.Update((object) copy);
    }
    else
    {
      copy.RefNbr = (string) null;
      copy.PeriodID = this.GetTransferPeriod(newRow.AssetID);
      if (copy.PeriodID == null && ((PXGraph) this).IsImport)
      {
        string finPeriodId = this.FinPeriodRepository.FindFinPeriodByDate(((PXGraph) this).Accessinfo.BusinessDate, PXAccess.GetParentOrganizationID(((PXSelectBase<FixedAsset>) this.CurrentAsset).Current.BranchID))?.FinPeriodID;
        copy.PeriodID = finPeriodId;
      }
      copy.ClassID = ((PXSelectBase<FixedAsset>) this.CurrentAsset).Current.ClassID;
      copy.RevisionID = ((PXSelectBase<FADetails>) this.AssetDetails).Current.LocationRevID;
      copy.TransactionDate = this.GetTransferDate(newRow.AssetID, copy.PeriodID);
      FALocationHistory faLocationHistory = (FALocationHistory) ((PXSelectBase) this.AssetLocation).Cache.Insert((object) copy);
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FixedAsset> e)
  {
    if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FixedAsset>>) e).Cache.ObjectsEqual<FixedAsset.status>((object) e.Row, (object) e.OldRow))
    {
      FixedAsset row = e.Row;
      FADetails faDetails = PXResultset<FADetails>.op_Implicit(PXSelectBase<FADetails, PXSelect<FADetails, Where<FADetails.assetID, Equal<Required<FADetails.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) (int?) row?.AssetID
      }));
      if (((PXSelectBase) this.Asset).Cache.GetStatus((object) row) != 2 && row.Status != "F" && PX.Objects.FA.AssetProcess.IsFullyDepreciatedAsset((PXGraph) this, row.AssetID))
      {
        ((SelectedEntityEvent<FixedAsset>) PXEntityEventBase<FixedAsset>.Container<FixedAsset.Events>.Select((Expression<Func<FixedAsset.Events, PXEntityEvent<FixedAsset.Events>>>) (ev => ev.FullyDepreciateAsset))).FireOn((PXGraph) this, row);
        return;
      }
      ((PXSelectBase) this.AssetDetails).Cache.SetValue<FADetails.status>((object) faDetails, (object) row.Status);
      GraphHelper.MarkUpdated(((PXSelectBase) this.AssetDetails).Cache, (object) faDetails);
    }
    FixedAsset row1 = e.Row;
    int? classId1;
    int num;
    if (row1 == null)
    {
      num = 0;
    }
    else
    {
      classId1 = row1.ClassID;
      num = classId1.HasValue ? 1 : 0;
    }
    if (num == 0)
      return;
    classId1 = (int?) e.OldRow?.ClassID;
    int? classId2 = (int?) e.Row?.ClassID;
    if (classId1.GetValueOrDefault() == classId2.GetValueOrDefault() & classId1.HasValue == classId2.HasValue)
      return;
    FixedAsset fixedAsset1 = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXViewOf<FixedAsset>.BasedOn<SelectFromBase<FixedAsset, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FixedAsset.assetID, IBqlInt>.IsEqual<BqlField<FixedAsset.classID, IBqlInt>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) e.Row
    }, Array.Empty<object>()));
    FixedAsset copy = (FixedAsset) ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FixedAsset>>) e).Cache.CreateCopy((object) e.Row);
    copy.UnderConstruction = fixedAsset1.UnderConstruction;
    FixedAsset fixedAsset2 = (FixedAsset) ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<FixedAsset>>) e).Cache.Update((object) copy);
  }

  protected virtual void FixedAsset_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    FixedAsset row = (FixedAsset) e.Row;
    if (!row.ClassID.HasValue)
      sender.RaiseExceptionHandling<FixedAsset.classID>((object) row, (object) row.ClassID, (Exception) new PXSetPropertyException("Value can not be empty."));
    this._PersistedAsset = row;
  }

  protected virtual void FixedAsset_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    FixedAsset row = (FixedAsset) e.Row;
    if (row == null)
      return;
    if (PXResultset<FATran>.op_Implicit(PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.assetID, Equal<BqlField<FixedAsset.assetID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<FATran.released, IBqlBool>.IsEqual<True>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.batchNbr, IsNotNull>>>>.Or<BqlOperand<Current<FixedAsset.splittedFrom>, IBqlInt>.IsNotNull>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>())) != null)
      throw new PXSetPropertyException("Record cannot be deleted.");
    GraphHelper.EnsureCachePersistence((PXGraph) this, typeof (FARegister));
    GraphHelper.EnsureCachePersistence((PXGraph) this, typeof (FABookHistory));
    foreach (PXResult<FARegister> pxResult in PXSelectBase<FARegister, PXSelectJoinGroupBy<FARegister, LeftJoin<FATran, On<FARegister.refNbr, Equal<FATran.refNbr>>>, Where<FATran.assetID, Equal<Required<FixedAsset.assetID>>>, Aggregate<GroupBy<FARegister.refNbr>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.AssetID
    }))
    {
      FARegister faRegister = PXResult<FARegister>.op_Implicit(pxResult);
      GraphHelper.Caches<FARegister>((PXGraph) this).Delete(faRegister);
    }
    foreach (PXResult<FABookHistory> pxResult in PXSelectBase<FABookHistory, PXSelect<FABookHistory, Where<FABookHistory.assetID, Equal<Required<FixedAsset.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.AssetID
    }))
    {
      FABookHistory faBookHistory = PXResult<FABookHistory>.op_Implicit(pxResult);
      GraphHelper.Caches<FABookHistory>((PXGraph) this).Delete(faBookHistory);
    }
  }

  protected virtual void CheckNewUsefulLife(PXCache sender, FABookBalance bal, Decimal usefulLife)
  {
    this.CheckLastDeprPeriodBetweenDeprFromAndDeprTo<FABookBalance.usefulLife>(sender, bal, (object) usefulLife);
  }

  /// <summary>
  /// The method to validate that <see cref="P:PX.Objects.FA.FABookBalance.LastDeprPeriod" /> greater than
  /// <see cref="P:PX.Objects.FA.FABookBalance.DeprFromPeriod" /> and less than <see cref="P:PX.Objects.FA.FABookBalance.DeprToPeriod" /> for the book.
  /// Throws an exception with an appropriate error message in case of negative validation.
  /// </summary>
  protected virtual void CheckLastDeprPeriodBetweenDeprFromAndDeprTo<T>(
    PXCache sender,
    FABookBalance bal,
    object newValue)
    where T : IBqlField
  {
    FABookBalance copy = (FABookBalance) sender.CreateCopy((object) bal);
    sender.SetValue<T>((object) copy, newValue);
    using (new SuppressHistoryUpdateScope())
    {
      sender.SetDefaultExt<FABookBalance.recoveryPeriod>((object) copy);
      sender.SetDefaultExt<FABookBalance.deprFromPeriod>((object) copy);
      sender.SetDefaultExt<FABookBalance.deprToPeriod>((object) copy);
    }
    if (copy.LastDeprPeriod == null || copy.DeprFromPeriod == null || copy.DeprToPeriod == null)
      return;
    if (string.CompareOrdinal(copy.LastDeprPeriod, copy.DeprFromPeriod) < 0)
      throw new PXSetPropertyException("The new Depr. From period '{0}' is later than the most recent depreciation period '{1}'.", new object[2]
      {
        (object) FinPeriodIDFormattingAttribute.FormatForError(copy.DeprFromPeriod),
        (object) FinPeriodIDFormattingAttribute.FormatForError(copy.LastDeprPeriod)
      });
    if (string.CompareOrdinal(copy.LastDeprPeriod, copy.DeprToPeriod) > 0)
      throw new PXSetPropertyException("The new Depr. To period '{0}' is earlier than the most recent depreciation period '{1}'.", new object[2]
      {
        (object) FinPeriodIDFormattingAttribute.FormatForError(copy.DeprToPeriod),
        (object) FinPeriodIDFormattingAttribute.FormatForError(copy.LastDeprPeriod)
      });
  }

  [Obsolete("Obsoilete. Will be removed in Acumatica ERP 2020R2")]
  protected virtual void CheckDeprToPeriodGreaterLastPeriod<T>(
    PXCache sender,
    FABookBalance bal,
    object newValue)
    where T : IBqlField
  {
    FABookBalance copy = (FABookBalance) sender.CreateCopy((object) bal);
    sender.SetValue<T>((object) copy, newValue);
    sender.SetDefaultExt<FABookBalance.recoveryPeriod>((object) copy);
    sender.SetDefaultExt<FABookBalance.deprToPeriod>((object) copy);
    if (copy.LastDeprPeriod != null && copy.DeprToPeriod != null && string.CompareOrdinal(copy.LastDeprPeriod, copy.DeprToPeriod) >= 0)
      throw new PXSetPropertyException("New Depr. To Period '{0}' less than Last Depreciation Period '{1}'.", new object[2]
      {
        (object) FinPeriodIDFormattingAttribute.FormatForError(copy.DeprToPeriod),
        (object) FinPeriodIDFormattingAttribute.FormatForError(copy.LastDeprPeriod)
      });
  }

  protected virtual void FixedAsset_UsefulLife_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    FixedAsset row = (FixedAsset) e.Row;
    if (row.Depreciable.GetValueOrDefault() && e.NewValue == null)
      throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[usefulLife]"
      });
    if (row.Depreciable.GetValueOrDefault() && (Decimal) e.NewValue == 0M)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
      {
        (object) "0"
      });
    foreach (PXResult<FABookBalance> pxResult in PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>>, OrderBy<Asc<FABookBalance.assetID, Asc<FABookBalance.bookID>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()))
      this.CheckNewUsefulLife(((PXSelectBase) this.AssetBalance).Cache, PXResult<FABookBalance>.op_Implicit(pxResult), (Decimal) e.NewValue);
  }

  protected virtual void FABookBalance_UsefulLife_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is FABookBalance row))
      return;
    if (row.Depreciate.GetValueOrDefault())
    {
      if (e.NewValue == null)
        throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[usefulLife]"
        });
      if ((Decimal) e.NewValue == 0M)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
        {
          (object) "0"
        });
    }
    if (e.NewValue == null)
      return;
    this.CheckNewUsefulLife(sender, (FABookBalance) e.Row, (Decimal) e.NewValue);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<FABookBalance.bonusAmount> e)
  {
    if (!(e.Row is FABookBalance row) || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FABookBalance.bonusAmount>, object, object>) e).NewValue == null)
      return;
    DateTime? deprFromDate = row.DeprFromDate;
    int? bonusId = row.BonusID;
    if (!bonusId.HasValue || !deprFromDate.HasValue)
      return;
    FABonusDetails faBonusDetails = PXResultset<FABonusDetails>.op_Implicit(PXSelectBase<FABonusDetails, PXSelect<FABonusDetails, Where<FABonusDetails.bonusID, Equal<Required<FABonus.bonusID>>, And<FABonusDetails.startDate, LessEqual<Required<FABookBalance.deprFromDate>>, And<FABonusDetails.endDate, GreaterEqual<Required<FABookBalance.deprFromDate>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) bonusId,
      (object) deprFromDate,
      (object) deprFromDate
    }));
    if (faBonusDetails == null)
      return;
    Decimal? bonusMax = faBonusDetails.BonusMax;
    Decimal? nullable1 = bonusMax;
    Decimal num = 0M;
    if (!(nullable1.GetValueOrDefault() > num & nullable1.HasValue))
      return;
    Decimal newValue = (Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FABookBalance.bonusAmount>, object, object>) e).NewValue;
    Decimal? nullable2 = bonusMax;
    Decimal valueOrDefault = nullable2.GetValueOrDefault();
    if (!(newValue > valueOrDefault & nullable2.HasValue))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FABookBalance.bonusAmount>, object, object>) e).NewValue = (object) bonusMax;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FABookBalance.averagingConvention> e)
  {
    if (!(e.Row is FABookBalance row) || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FABookBalance.averagingConvention>, object, object>) e).NewValue == null)
      return;
    this.CheckDeprToPeriodGreaterLastPeriod<FABookBalance.averagingConvention>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FABookBalance.averagingConvention>>) e).Cache, row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FABookBalance.averagingConvention>, object, object>) e).NewValue);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<FixedAsset.classID> e)
  {
    FixedAsset row = e.Row as FixedAsset;
    FAClass faClass = PXResultset<FAClass>.op_Implicit(PXSelectBase<FAClass, PXViewOf<FAClass>.BasedOn<SelectFromBase<FAClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FAClass.assetID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FixedAsset.classID>, object, object>) e).NewValue
    }));
    foreach (PXResult<FABookSettings, FABook> pxResult in PXSelectBase<FABookSettings, PXViewOf<FABookSettings>.BasedOn<SelectFromBase<FABookSettings, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<FABook>.On<BqlOperand<FABookSettings.bookID, IBqlInt>.IsEqual<FABook.bookID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookSettings.assetID, Equal<P.AsInt>>>>>.And<BqlOperand<FABook.updateGL, IBqlBool>.IsNotEqual<True>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FixedAsset.classID>, object, object>) e).NewValue
    }))
    {
      FABook book = PXResult<FABookSettings, FABook>.op_Implicit(pxResult);
      IYearSetup faBookYearSetup = this.FABookPeriodRepository.FindFABookYearSetup(book);
      if (faBookYearSetup != null)
      {
        if (!faBookYearSetup.IsFixedLengthPeriod && book != null)
        {
          if (((IQueryable<PXResult<FABookPeriodSetup>>) PXSelectBase<FABookPeriodSetup, PXViewOf<FABookPeriodSetup>.BasedOn<SelectFromBase<FABookPeriodSetup, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FABookPeriodSetup.bookID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) book.BookID
          })).FirstOrDefault<PXResult<FABookPeriodSetup>>() != null)
            continue;
        }
        else
          continue;
      }
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FixedAsset.classID>, object, object>) e).NewValue = (object) faClass.AssetCD;
      throw new PXSetPropertyException("The book calendar does not exist for the {0} book. Use the Book Calendar Setup (FA206000) form to set up calendars for all needed books.", new object[1]
      {
        (object) book.BookCode
      });
    }
    bool? underConstruction = row.UnderConstruction;
    if (underConstruction.GetValueOrDefault())
    {
      underConstruction = faClass.UnderConstruction;
      if (!underConstruction.GetValueOrDefault() && !((PXSelectBase<FADetails>) this.AssetDetails).Current.DepreciateFromDate.HasValue)
      {
        ((PXSelectBase) this.AssetDetails).Cache.RaiseExceptionHandling<FADetails.depreciateFromDate>((object) ((PXSelectBase<FADetails>) this.AssetDetails).Current, (object) ((PXSelectBase<FADetails>) this.AssetDetails).Current.DepreciateFromDate, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<FADetails.depreciateFromDate>(((PXSelectBase) this.AssetDetails).Cache)
        }));
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FixedAsset.classID>, object, object>) e).NewValue = (object) faClass.AssetCD;
        throw new PXSetPropertyException("The asset under construction with an empty Placed-in-Service date cannot be transferred to another class.");
      }
    }
    if (faClass == null)
      return;
    underConstruction = faClass.UnderConstruction;
    if (!underConstruction.GetValueOrDefault())
      return;
    bool flag = false;
    foreach (PXResult<FABookBalance> pxResult in ((PXSelectBase<FABookBalance>) this.AssetBalance).Select(Array.Empty<object>()))
    {
      Decimal? ytdDepreciated = PXResult<FABookBalance>.op_Implicit(pxResult).YtdDepreciated;
      Decimal num = 0M;
      if (!(ytdDepreciated.GetValueOrDefault() == num & ytdDepreciated.HasValue))
      {
        flag = true;
        break;
      }
    }
    if (row.UnderConstruction.GetValueOrDefault() & flag)
      throw new PXSetPropertyException("The asset under construction with an empty Placed-in-Service date cannot be transferred to another class.");
  }

  public virtual void _(PX.Data.Events.FieldUpdated<FABookBalance.usefulLife> e)
  {
    FABookBalance row = (FABookBalance) e.Row;
    if (row == null)
      return;
    FADepreciationMethod depreciationMethod = PXResultset<FADepreciationMethod>.op_Implicit(PXSelectBase<FADepreciationMethod, PXSelect<FADepreciationMethod, Where<FADepreciationMethod.methodID, Equal<Current<FABookBalance.depreciationMethodID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    if (depreciationMethod != null)
    {
      Decimal? usefulLife1 = depreciationMethod.UsefulLife;
      if (usefulLife1.HasValue)
      {
        usefulLife1 = row.UsefulLife;
        Decimal? usefulLife2 = depreciationMethod.UsefulLife;
        if (!(usefulLife1.GetValueOrDefault() == usefulLife2.GetValueOrDefault() & usefulLife1.HasValue == usefulLife2.HasValue))
          row.DepreciationMethodID = new int?();
      }
    }
    if (row.DeprToPeriod == null || row.LastDeprPeriod == null || string.CompareOrdinal(row.DeprToPeriod, row.LastDeprPeriod) != 0)
      return;
    row.CurrDeprPeriod = (string) null;
    row.Status = "F";
    if (!GraphHelper.RowCast<FABookBalance>((IEnumerable) ((PXSelectBase<FABookBalance>) this.AssetBalance).Select(Array.Empty<object>())).All<FABookBalance>((Func<FABookBalance, bool>) (_ => _.Status == "F")))
      return;
    ((SelectedEntityEvent<FixedAsset>) PXEntityEventBase<FixedAsset>.Container<FixedAsset.Events>.Select((Expression<Func<FixedAsset.Events, PXEntityEvent<FixedAsset.Events>>>) (ev => ev.FullyDepreciateAsset))).FireOn((PXGraph) this, ((PXSelectBase<FixedAsset>) this.Asset).Current);
  }

  protected virtual void FixedAsset_UsefulLife_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    FixedAsset row = e.Row as FixedAsset;
    if (((PXSelectBase<FADetails>) this.AssetDetails).Current == null || !((PXSelectBase<FADetails>) this.AssetDetails).Current.ReceiptDate.HasValue)
      return;
    bool flag = true;
    foreach (PXResult<FABookBalance> pxResult in ((PXSelectBase<FABookBalance>) this.AssetBalance).Select(Array.Empty<object>()))
    {
      FABookBalance copy = (FABookBalance) ((PXSelectBase) this.AssetBalance).Cache.CreateCopy((object) PXResult<FABookBalance>.op_Implicit(pxResult));
      copy.UsefulLife = new Decimal?((Decimal) e.NewValue);
      object obj = (object) null;
      ((PXSelectBase) this.AssetBalance).Cache.RaiseFieldDefaulting<FABookBalance.deprToPeriod>((object) copy, ref obj);
      if (string.CompareOrdinal(obj?.ToString(), copy.LastDeprPeriod) > 0)
      {
        flag = false;
        break;
      }
    }
    if (!flag || ((PXSelectBase<FixedAsset>) this.Asset).Ask("The fixed asset with the reduced useful life will get the Fully Depreciated status and its useful life will be disabled for editing. Click OK to proceed with the changes.", (MessageButtons) 4) != 7)
      return;
    ((CancelEventArgs) e).Cancel = true;
    e.NewValue = (object) row.UsefulLife;
  }

  protected virtual void FixedAsset_UsefulLife_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    AssetMaint.UpdateBalances<FABookBalance.usefulLife, FixedAsset.usefulLife>(sender, e);
    FixedAsset row = e.Row as FixedAsset;
    if (((PXSelectBase) this.Asset).Cache.GetStatus((object) row) == 2 || !(row.Status != "F") || !PX.Objects.FA.AssetProcess.IsFullyDepreciatedAsset((PXGraph) this, row.AssetID))
      return;
    row.Status = "F";
    FADetails faDetails1 = ((PXSelectBase<FADetails>) this.AssetDetails).Current;
    if (faDetails1 == null)
      faDetails1 = PXResultset<FADetails>.op_Implicit(PXSelectBase<FADetails, PXSelect<FADetails, Where<FADetails.assetID, Equal<Required<FADetails.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) (int?) row?.AssetID
      }));
    FADetails faDetails2 = faDetails1;
    if (faDetails2 == null)
      return;
    faDetails2.Status = "F";
  }

  protected virtual void FixedAsset_Depreciable_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    AssetMaint.UpdateBalances<FABookBalance.depreciate, FixedAsset.depreciable>(sender, e);
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<FixedAsset.depreciable> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FixedAsset.depreciable>, object, object>) e).NewValue == null || (bool) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FixedAsset.depreciable>, object, object>) e).NewValue)
      return;
    foreach (PXResult<FABookBalance> pxResult in ((PXSelectBase<FABookBalance>) this.AssetBalance).Select(Array.Empty<object>()))
    {
      FABookBalance faBookBalance = PXResult<FABookBalance>.op_Implicit(pxResult);
      if (string.IsNullOrWhiteSpace(faBookBalance.LastDeprPeriod))
      {
        Decimal? ytdDepreciated = faBookBalance.YtdDepreciated;
        if (ytdDepreciated.HasValue)
        {
          ytdDepreciated = faBookBalance.YtdDepreciated;
          Decimal num = 0.0M;
          if (ytdDepreciated.GetValueOrDefault() == num & ytdDepreciated.HasValue)
            continue;
        }
        else
          continue;
      }
      throw new PXSetPropertyException((IBqlTable) (e.Row as FABookBalance), "The asset is non-depreciable and cannot have Last Depr. Period and a non-zero Accum. Depr. specified for each book on the Balance tab. Correct these settings and try again.");
    }
  }

  protected virtual void FixedAsset_ParentAssetID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    FixedAsset row = (FixedAsset) e.Row;
    if (row == null)
      return;
    PXSelectBase<FixedAsset> pxSelectBase = (PXSelectBase<FixedAsset>) new PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Required<FixedAsset.parentAssetID>>>>((PXGraph) this);
    int? nullable1 = (int?) e.NewValue;
    string str1 = (string) null;
    FixedAsset fixedAsset;
    for (; nullable1.HasValue; nullable1 = fixedAsset.ParentAssetID)
    {
      fixedAsset = PXResultset<FixedAsset>.op_Implicit(pxSelectBase.Select(new object[1]
      {
        (object) nullable1
      }));
      str1 = str1 ?? fixedAsset.AssetCD;
      int? parentAssetId = fixedAsset.ParentAssetID;
      if (parentAssetId.HasValue)
      {
        parentAssetId = fixedAsset.ParentAssetID;
        int? nullable2 = row.AssetID;
        if (parentAssetId.GetValueOrDefault() == nullable2.GetValueOrDefault() & parentAssetId.HasValue == nullable2.HasValue)
        {
          PXFieldVerifyingEventArgs verifyingEventArgs = e;
          nullable2 = row.ParentAssetID;
          string str2;
          if (!nullable2.HasValue)
            str2 = (string) null;
          else
            str2 = PXResultset<FixedAsset>.op_Implicit(pxSelectBase.Select(new object[1]
            {
              (object) row.ParentAssetID
            })).AssetCD;
          verifyingEventArgs.NewValue = (object) str2;
          throw new PXSetPropertyException("You cannot reference the child asset '{0}' as the parent asset.", new object[1]
          {
            (object) str1
          });
        }
      }
    }
  }

  protected virtual void FixedAsset_FASubID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is FixedAsset row))
      return;
    int? newValue = (int?) e.NewValue;
    if (!newValue.HasValue)
      return;
    string str = this.VerifySubIDByFAClass<FAClass.fASubMask, FixedAsset.fASubID>(sender, row, newValue);
    if (!string.IsNullOrEmpty(str))
    {
      e.NewValue = (object) str;
      throw new PXSetPropertyException("The entered value does not correspond to the mask specified for the subaccount on the Fixed Asset Classes (FA201000) form.");
    }
  }

  protected virtual void FixedAsset_AccumulatedDepreciationSubID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is FixedAsset row))
      return;
    int? newValue = (int?) e.NewValue;
    if (!newValue.HasValue)
      return;
    string str = this.VerifySubIDByFAClass<FAClass.accumDeprSubMask, FixedAsset.accumulatedDepreciationSubID>(sender, row, newValue);
    if (!string.IsNullOrEmpty(str))
    {
      e.NewValue = (object) str;
      throw new PXSetPropertyException("The entered value does not correspond to the mask specified for the subaccount on the Fixed Asset Classes (FA201000) form.");
    }
  }

  protected void UpdateFATranAccountSub<UpdatingField>(
    int? assetID,
    int? newValue,
    params string[] transactionTypes)
    where UpdatingField : IBqlField
  {
    PXCache cach = ((PXGraph) this).Caches[BqlCommand.GetItemType<UpdatingField>()];
    foreach (PXResult<FATran> pxResult in PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FATran.batchNbr, IBqlString>.IsNull>>>.And<BqlOperand<FATran.tranType, IBqlString>.IsIn<P.AsString>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) assetID,
      (object) transactionTypes
    }))
    {
      FATran faTran = PXResult<FATran>.op_Implicit(pxResult);
      cach.SetValue<UpdatingField>((object) faTran, (object) newValue);
      cach.Update((object) faTran);
    }
  }

  protected void _(
    PX.Data.Events.FieldUpdated<FixedAsset, FixedAsset.fAAccountID> e)
  {
    if (e.Row == null || ((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault() && e.Row.IsAcquired.GetValueOrDefault())
      return;
    this.UpdateFATranAccountSub<FATran.debitAccountID>(e.Row.AssetID, (int?) e.NewValue, "P+");
    this.UpdateFATranAccountSub<FATran.creditAccountID>(e.Row.AssetID, (int?) e.NewValue, "P-");
  }

  protected void _(
    PX.Data.Events.FieldUpdated<FixedAsset, FixedAsset.fASubID> e)
  {
    if (e.Row == null || ((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault() && e.Row.IsAcquired.GetValueOrDefault())
      return;
    this.UpdateFATranAccountSub<FATran.debitSubID>(e.Row.AssetID, (int?) e.NewValue, "P+");
    this.UpdateFATranAccountSub<FATran.creditSubID>(e.Row.AssetID, (int?) e.NewValue, "P-");
  }

  protected void _(
    PX.Data.Events.FieldUpdated<FixedAsset, FixedAsset.fAAccrualAcctID> e)
  {
    if (e.Row == null || ((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault() && e.Row.IsAcquired.GetValueOrDefault())
      return;
    this.UpdateFATranAccountSub<FATran.debitAccountID>(e.Row.AssetID, (int?) e.NewValue, "P-", "R+");
    this.UpdateFATranAccountSub<FATran.creditAccountID>(e.Row.AssetID, (int?) e.NewValue, "P+", "R-");
  }

  protected void _(
    PX.Data.Events.FieldUpdated<FixedAsset, FixedAsset.fAAccrualSubID> e)
  {
    if (e.Row == null || ((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault() && e.Row.IsAcquired.GetValueOrDefault())
      return;
    this.UpdateFATranAccountSub<FATran.debitSubID>(e.Row.AssetID, (int?) e.NewValue, "P-", "R+");
    this.UpdateFATranAccountSub<FATran.creditSubID>(e.Row.AssetID, (int?) e.NewValue, "P+", "R-");
  }

  protected void _(
    PX.Data.Events.FieldUpdated<FixedAsset, FixedAsset.accumulatedDepreciationAccountID> e)
  {
    if (e.Row == null || ((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault() && e.Row.IsAcquired.GetValueOrDefault())
      return;
    this.UpdateFATranAccountSub<FATran.debitAccountID>(e.Row.AssetID, (int?) e.NewValue, "D-", "C-", "A-");
    this.UpdateFATranAccountSub<FATran.creditAccountID>(e.Row.AssetID, (int?) e.NewValue, "D+", "C+", "A+");
    PXCache pxCache = (PXCache) GraphHelper.Caches<FATran>((PXGraph) this);
    foreach (PXResult<FATran> pxResult in PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FATran.batchNbr, IBqlString>.IsNull>>, And<BqlOperand<FATran.released, IBqlBool>.IsEqual<True>>>, And<BqlOperand<FATran.creditAccountID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<FATran.tranType, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) e.Row.AssetID,
      (object) (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FixedAsset, FixedAsset.accumulatedDepreciationAccountID>, FixedAsset, object>) e).OldValue,
      (object) "TD"
    }))
    {
      FATran faTran = PXResult<FATran>.op_Implicit(pxResult);
      faTran.CreditAccountID = (int?) e.NewValue;
      pxCache.Update((object) faTran);
    }
  }

  protected void _(
    PX.Data.Events.FieldUpdated<FixedAsset, FixedAsset.accumulatedDepreciationSubID> e)
  {
    if (e.Row == null || ((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault() && e.Row.IsAcquired.GetValueOrDefault())
      return;
    this.UpdateFATranAccountSub<FATran.debitSubID>(e.Row.AssetID, (int?) e.NewValue, "D-", "C-", "A-");
    this.UpdateFATranAccountSub<FATran.creditSubID>(e.Row.AssetID, (int?) e.NewValue, "D+", "C+", "A+");
    PXCache pxCache = (PXCache) GraphHelper.Caches<FATran>((PXGraph) this);
    foreach (PXResult<FATran> pxResult in PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FATran.batchNbr, IBqlString>.IsNull>>, And<BqlOperand<FATran.released, IBqlBool>.IsEqual<True>>>, And<BqlOperand<FATran.creditSubID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<FATran.tranType, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) e.Row.AssetID,
      (object) (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FixedAsset, FixedAsset.accumulatedDepreciationSubID>, FixedAsset, object>) e).OldValue,
      (object) "TD"
    }))
    {
      FATran faTran = PXResult<FATran>.op_Implicit(pxResult);
      faTran.CreditSubID = (int?) e.NewValue;
      pxCache.Update((object) faTran);
    }
  }

  protected virtual string VerifySubIDByFAClass<MaskField, SubIDField>(
    PXCache sender,
    FixedAsset asset,
    int? subID)
    where MaskField : IBqlField
    where SubIDField : IBqlField
  {
    string str1 = string.Empty;
    string subCd1 = PXResultset<PX.Objects.GL.Sub>.op_Implicit(PXSelectBase<PX.Objects.GL.Sub, PXSelect<PX.Objects.GL.Sub, Where<PX.Objects.GL.Sub.subID, Equal<Required<FixedAsset.fASubID>>>>.Config>.SelectSingleBound(sender.Graph, new object[0], new object[1]
    {
      (object) subID
    })).SubCD;
    FAClass faClass = PXResultset<FAClass>.op_Implicit(PXSelectBase<FAClass, PXSelect<FAClass, Where<FAClass.assetID, Equal<Current<FixedAsset.classID>>>>.Config>.SelectSingleBound(sender.Graph, new object[1]
    {
      (object) asset
    }, Array.Empty<object>()));
    if (faClass == null || faClass.UnderConstruction.GetValueOrDefault() && typeof (SubIDField) == typeof (FixedAsset.accumulatedDepreciationSubID))
      return string.Empty;
    int? nullable = AssetMaint.MakeSubID<MaskField, SubIDField>(sender.Graph.Caches[typeof (FixedAsset)], asset);
    string subCd2 = PXResultset<PX.Objects.GL.Sub>.op_Implicit(PXSelectBase<PX.Objects.GL.Sub, PXSelect<PX.Objects.GL.Sub, Where<PX.Objects.GL.Sub.subID, Equal<Required<FixedAsset.fASubID>>>>.Config>.SelectSingleBound(sender.Graph, new object[0], new object[1]
    {
      (object) nullable
    })).SubCD;
    string str2 = (string) sender.Graph.Caches[typeof (FAClass)].GetValue<MaskField>((object) faClass);
    for (int index = 0; index < subCd1.Count<char>(); ++index)
    {
      if (str2[index] != 'A' && (int) subCd1[index] != (int) subCd2[index])
      {
        str1 = subCd1;
        break;
      }
    }
    return str1;
  }

  public static int? MakeSubID<MaskField, SubIDField>(PXCache sender, FixedAsset asset)
    where MaskField : IBqlField
    where SubIDField : IBqlField
  {
    FAClass faClass = PXResultset<FAClass>.op_Implicit(PXSelectBase<FAClass, PXSelect<FAClass, Where<FAClass.assetID, Equal<Current<FixedAsset.classID>>>>.Config>.SelectSingleBound(sender.Graph, new object[1]
    {
      (object) asset
    }, Array.Empty<object>()));
    if (faClass == null)
      return new int?();
    FADetails faDetails = PXResultset<FADetails>.op_Implicit(PXSelectBase<FADetails, PXSelect<FADetails, Where<FADetails.assetID, Equal<Current<FixedAsset.assetID>>>>.Config>.SelectSingleBound(sender.Graph, new object[1]
    {
      (object) asset
    }, Array.Empty<object>()));
    FALocationHistory faLocationHistory = PXResultset<FALocationHistory>.op_Implicit(PXSelectBase<FALocationHistory, PXSelect<FALocationHistory, Where<FALocationHistory.assetID, Equal<Current<FADetails.assetID>>, And<FALocationHistory.revisionID, Equal<Current<FADetails.locationRevID>>>>>.Config>.SelectSingleBound(sender.Graph, new object[2]
    {
      (object) asset,
      (object) faDetails
    }, Array.Empty<object>()));
    PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.Location.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>, And<PX.Objects.CR.Location.locationID, Equal<PX.Objects.CR.BAccount.defLocationID>>>, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Current<FALocationHistory.locationID>>>>.Config>.SelectSingleBound(sender.Graph, new object[1]
    {
      (object) faLocationHistory
    }, Array.Empty<object>()));
    EPDepartment epDepartment = PXResultset<EPDepartment>.op_Implicit(PXSelectBase<EPDepartment, PXSelect<EPDepartment, Where<EPDepartment.departmentID, Equal<Current<FALocationHistory.department>>>>.Config>.SelectSingleBound(sender.Graph, new object[1]
    {
      (object) faLocationHistory
    }, Array.Empty<object>()));
    string mask = (string) sender.Graph.Caches[typeof (FAClass)].GetValue<MaskField>((object) faClass);
    int? nullable1 = (int?) sender.Graph.Caches[typeof (FixedAsset)].GetValue<SubIDField>((object) asset);
    int? nullable2 = (int?) sender.Graph.Caches[typeof (PX.Objects.CR.Location)].GetValue<PX.Objects.CR.Location.cMPExpenseSubID>((object) location);
    int? nullable3 = (int?) sender.Graph.Caches[typeof (EPDepartment)].GetValue<EPDepartment.expenseSubID>((object) epDepartment);
    int? nullable4 = (int?) sender.Graph.Caches[typeof (FixedAsset)].GetValue<SubIDField>((object) faClass);
    object obj = (object) SubAccountMaskAttribute.MakeSub<MaskField>(sender.Graph, mask, new object[4]
    {
      (object) nullable1,
      (object) nullable2,
      (object) nullable3,
      (object) nullable4
    }, new System.Type[4]
    {
      typeof (SubIDField),
      typeof (PX.Objects.CR.Location.cMPExpenseSubID),
      typeof (EPDepartment.expenseSubID),
      typeof (SubIDField)
    });
    sender.RaiseFieldUpdating<SubIDField>((object) asset, ref obj);
    return (int?) obj;
  }

  protected static void FASubIDFieldDefaulting<MaskField, SubIDField>(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
    where MaskField : IBqlField
    where SubIDField : IBqlField
  {
    FixedAsset row = (FixedAsset) e.Row;
    e.NewValue = (object) AssetMaint.MakeSubID<MaskField, SubIDField>(sender, row);
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FixedAsset, FixedAsset.classID> e)
  {
    if ((e.Row != null || !string.IsNullOrWhiteSpace(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FixedAsset, FixedAsset.classID>, FixedAsset, object>) e).NewValue as string)) && !((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault() && this.HasAnyGLTran(e.Row.AssetID))
    {
      FAClass faClass = PXSelectorAttribute.Select<FixedAsset.classID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FixedAsset, FixedAsset.classID>>) e).Cache, (object) e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FixedAsset, FixedAsset.classID>, FixedAsset, object>) e).NewValue) as FAClass;
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FixedAsset, FixedAsset.classID>, FixedAsset, object>) e).NewValue = (object) faClass?.AssetCD;
      throw new PXSetPropertyException("This operation cannot be completed in migration mode. To exit migration mode, select the Update GL check box on the Fixed Assets Preferences (FA101000) form.");
    }
  }

  protected virtual void FixedAsset_ClassID_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    FixedAsset row = (FixedAsset) e.Row;
    if ((row != null ? (!row.ClassID.HasValue ? 1 : 0) : 1) != 0)
      return;
    FixedAsset fixedAsset = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FixedAsset.classID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    if (fixedAsset == null || string.CompareOrdinal((string) e.NewValue, fixedAsset.AssetCD) == 0 || ((PXSelectBase<FixedAsset>) this.CurrentAsset).Ask(row, "Important", "Warning - Only GL Accounts will be changed, all other parameters remain unchanged. Do you want to continue?", (MessageButtons) 4, (MessageIcon) 2) != 7)
      return;
    e.NewValue = (object) fixedAsset.AssetCD;
  }

  protected virtual void FixedAsset_ClassID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    FixedAsset currAsset = (FixedAsset) e.Row;
    if (currAsset == null)
      return;
    foreach (KeyValuePair<System.Type, System.Type> faAccountSubPair in this.GetFAAccountSubPairs())
    {
      if (this.IsAccountChangeable(currAsset, faAccountSubPair.Key))
      {
        sender.SetDefaultExt((object) currAsset, faAccountSubPair.Key.Name, (object) null);
        sender.RaiseExceptionHandling(faAccountSubPair.Value.Name, (object) currAsset, (object) null, (Exception) null);
        sender.SetDefaultExt((object) currAsset, faAccountSubPair.Value.Name, (object) null);
      }
    }
    if (e.OldValue != null)
    {
      Dictionary<int, FABookSettings> dictionary1 = GraphHelper.RowCast<FABookSettings>((IEnumerable) PXSelectBase<FABookSettings, PXViewOf<FABookSettings>.BasedOn<SelectFromBase<FABookSettings, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FABookSettings.assetID, IBqlInt>.IsEqual<BqlField<FixedAsset.classID, IBqlInt>.FromCurrent>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
      {
        e.Row
      }, Array.Empty<object>())).ToDictionary<FABookSettings, int>((Func<FABookSettings, int>) (s => s.BookID.Value));
      foreach (PXResult<FABookBalance> pxResult in ((PXSelectBase<FABookBalance>) this.AssetBalance).Select(Array.Empty<object>()))
      {
        FABookBalance balance = PXResult<FABookBalance>.op_Implicit(pxResult);
        int? nullable = balance.DepreciationMethodID;
        if (!nullable.HasValue && balance.AveragingConvention == null)
        {
          Dictionary<int, FABookSettings> dictionary2 = dictionary1;
          nullable = balance.BookID;
          int key = nullable.Value;
          FABookSettings faBookSettings;
          ref FABookSettings local = ref faBookSettings;
          if (dictionary2.TryGetValue(key, out local))
          {
            balance.DepreciationMethodID = this.GetDeprMethodOfAssetType(faBookSettings.DepreciationMethodID, balance);
            balance.AveragingConvention = faBookSettings.AveragingConvention;
            ((PXSelectBase<FABookBalance>) this.AssetBalance).Update(balance);
          }
        }
      }
    }
    else
    {
      sender.SetDefaultExt<FixedAsset.isTangible>(e.Row);
      sender.SetDefaultExt<FixedAsset.assetTypeID>(e.Row);
      sender.SetDefaultExt<FixedAsset.usefulLife>(e.Row);
      sender.SetDefaultExt<FixedAsset.serviceScheduleID>(e.Row);
      sender.SetDefaultExt<FixedAsset.usageScheduleID>(e.Row);
      EnumerableExtensions.ForEach<FABookSettings>(GraphHelper.RowCast<FABookSettings>((IEnumerable) PXSelectBase<FABookSettings, PXViewOf<FABookSettings>.BasedOn<SelectFromBase<FABookSettings, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FABookSettings.assetID, IBqlInt>.IsEqual<BqlField<FixedAsset.classID, IBqlInt>.FromCurrent>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
      {
        e.Row
      }, Array.Empty<object>())), (System.Action<FABookSettings>) (sett => ((PXSelectBase<FABookBalance>) this.AssetBalance).Insert(new FABookBalance()
      {
        AssetID = currAsset.AssetID,
        ClassID = currAsset.ClassID,
        BookID = sett.BookID,
        AveragingConvention = sett.AveragingConvention
      })));
      ((PXSelectBase) this.AssetBalance).Cache.IsDirty = false;
      FAClass faClass = PXResultset<FAClass>.op_Implicit(PXSelectBase<FAClass, PXSelect<FAClass, Where<FAClass.assetID, Equal<Required<FixedAsset.classID>>>>.Config>.SelectSingleBound(sender.Graph, (object[]) null, new object[1]
      {
        (object) currAsset.ClassID
      }));
      currAsset.HoldEntry = faClass.HoldEntry;
    }
  }

  protected virtual void FixedAsset_Status_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    FixedAsset row = (FixedAsset) e.Row;
    if (row == null || !(row.Status == "H") || !((string) e.NewValue != "H"))
      return;
    FABookBalance faBookBalance = GraphHelper.RowCast<FABookBalance>((IEnumerable) ((PXSelectBase<FABookBalance>) this.AssetBalance).Select(Array.Empty<object>())).Where<FABookBalance>((Func<FABookBalance, bool>) (bookbal => ((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault() && bookbal.UpdateGL.GetValueOrDefault() && string.IsNullOrEmpty(bookbal.InitPeriod))).Select(bookbal => new
    {
      bookbal = bookbal,
      AcquiredCost = GraphHelper.RowCast<FATran>((IEnumerable) PXSelectBase<FATran, PXSelect<FATran, Where<FATran.assetID, Equal<Current<FABookBalance.assetID>>, And<FATran.bookID, Equal<Current<FABookBalance.bookID>>, And<FATran.tranType, Equal<FATran.tranType.purchasingPlus>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
      {
        (object) bookbal
      }, Array.Empty<object>())).Aggregate<FATran, Decimal?>(new Decimal?(0M), (Func<Decimal?, FATran, Decimal?>) ((current, tran) =>
      {
        Decimal? nullable = current;
        Decimal? tranAmt = tran.TranAmt;
        return !(nullable.HasValue & tranAmt.HasValue) ? new Decimal?() : new Decimal?(nullable.GetValueOrDefault() + tranAmt.GetValueOrDefault());
      }))
    }).Where(t =>
    {
      Decimal? acquisitionCost = t.bookbal.AcquisitionCost;
      Decimal? acquiredCost = t.AcquiredCost;
      Decimal? nullable = acquisitionCost.HasValue & acquiredCost.HasValue ? new Decimal?(acquisitionCost.GetValueOrDefault() - acquiredCost.GetValueOrDefault()) : new Decimal?();
      Decimal num = 0.00005M;
      return nullable.GetValueOrDefault() >= num & nullable.HasValue;
    }).Select(t => t.bookbal).FirstOrDefault<FABookBalance>();
    if (faBookBalance != null)
      throw new PXSetPropertyException("Asset is out of balance and cannot be removed from 'Hold'. Add Purchasing+ transactions with total amount {0}.", new object[1]
      {
        (object) PXCurrencyAttribute.BaseRound((PXGraph) this, faBookBalance.AcquisitionCost)
      });
  }

  public static void UpdateBalances<Field, ParentField>(
    PXCache sourceCache,
    PXFieldUpdatedEventArgs e)
    where Field : IBqlField
    where ParentField : IBqlField
  {
    System.Type itemType = BqlCommand.GetItemType<Field>();
    PXCache cach = sourceCache.Graph.Caches[itemType];
    BqlCommand bqlCommand = BqlTemplate.OfCommand<Select<BqlPlaceholder.A, Where<BqlPlaceholder.B, Equal<Required<BqlPlaceholder.C>>>>>.Replace<BqlPlaceholder.A>(itemType).Replace<BqlPlaceholder.B>(GraphHelper.GetBqlField<FixedAsset.assetID>(cach)).Replace<BqlPlaceholder.C>(GraphHelper.GetBqlField<FixedAsset.assetID>(sourceCache)).ToCommand();
    System.Type type = (System.Type) null;
    for (int index = cach.BqlKeys.Count - 1; index >= 0; --index)
    {
      System.Type bqlKey = cach.BqlKeys[index];
      if (type == (System.Type) null)
        type = typeof (Asc<>).MakeGenericType(bqlKey);
      else
        type = typeof (Asc<,>).MakeGenericType(bqlKey, type);
    }
    if (type != (System.Type) null)
      bqlCommand = bqlCommand.OrderByNew(typeof (OrderBy<>).MakeGenericType(type));
    foreach (object obj in new PXView(sourceCache.Graph, false, bqlCommand).SelectMulti(new object[1]
    {
      sourceCache.GetValue(e.Row, "assetID")
    }))
    {
      cach.RaiseRowSelected(obj);
      PXFieldState stateExt;
      if ((stateExt = (PXFieldState) cach.GetStateExt<Field>(obj)) != null && stateExt.Enabled)
      {
        object copy = cach.CreateCopy(obj);
        cach.SetValue<Field>(copy, sourceCache.GetValue<ParentField>(e.Row));
        try
        {
          if (copy is FABookBalance faBookBalance)
            faBookBalance.NoteID = new Guid?(Guid.NewGuid());
          if (faBookBalance != null && typeof (ParentField) == typeof (FADetails.depreciateFromDate))
          {
            DateTime? nullable = faBookBalance.DeprFromDate;
            if (nullable.HasValue)
            {
              DeprCalcParameters deprCalcParameters = (sourceCache.Graph is PX.Objects.FA.DepreciationCalculation graph1 ? graph1.Params : (DeprCalcParameters) null) ?? new DeprCalcParameters();
              PXGraph graph2 = sourceCache.Graph;
              FABookBalance balance = faBookBalance;
              nullable = new DateTime?();
              DateTime? recoveryEndDate = nullable;
              deprCalcParameters.Fill(graph2, balance, recoveryEndDate: recoveryEndDate);
            }
          }
          cach.Update(copy);
        }
        catch (PXSetPropertyException ex)
        {
          if (((Exception) ex).InnerException is TranDateOutOfRangeException)
            throw new PXSetPropertyException("Calendar is not setup for date in Book '{0}'.", new object[1]
            {
              cach.GetValueExt<FABookBalance.bookID>(obj)
            });
          throw;
        }
      }
    }
  }

  protected virtual void FADetails_DepreciateFromDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row is FADetails row)
    {
      DateTime? receiptDate = row.ReceiptDate;
      if (receiptDate.HasValue && e.NewValue != null)
      {
        receiptDate = row.ReceiptDate;
        if (receiptDate.Value.CompareTo(e.NewValue) > 0)
          throw new PXSetPropertyException<FADetails.depreciateFromDate>("The {0} must be equal to or later than the {1}.", new object[2]
          {
            (object) PXUIFieldAttribute.GetDisplayName<FADetails.depreciateFromDate>(sender),
            (object) PXUIFieldAttribute.GetDisplayName<FADetails.receiptDate>(sender)
          });
      }
    }
    if (!((PXSelectBase<FixedAsset>) this.CurrentAsset).Current.UnderConstruction.GetValueOrDefault())
      return;
    AssetMaint.AssetMaintFixedAssetChecksExtension extension = ((PXGraph) this).GetExtension<AssetMaint.AssetMaintFixedAssetChecksExtension>();
    try
    {
      extension.CheckDepreciationPeriodNotEarlierThanTheLastTransactionPeriod(((PXSelectBase<FixedAsset>) this.CurrentAsset).Current, e.NewValue as DateTime?);
    }
    catch (PXException ex)
    {
      throw new PXSetPropertyException<FADetails.depreciateFromDate>(((Exception) ex).Message);
    }
  }

  protected virtual void FADetails_DepreciateFromDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    try
    {
      AssetMaint.UpdateBalances<FABookBalance.deprFromDate, FADetails.depreciateFromDate>(sender, e);
      sender.RaiseExceptionHandling<FADetails.depreciateFromDate>(e.Row, (object) ((FADetails) e.Row).DepreciateFromDate, (Exception) null);
    }
    catch (PXException ex)
    {
      sender.SetValue<FADetails.depreciateFromDate>(e.Row, e.OldValue);
      sender.RaiseExceptionHandling<FADetails.depreciateFromDate>(e.Row, e.OldValue, (Exception) ex);
    }
    ((PXSelectBase) this.GLTrnFilter).Cache.SetDefaultExt<GLTranFilter.tranDate>((object) ((PXSelectBase<GLTranFilter>) this.GLTrnFilter).Current);
  }

  protected virtual void FADetails_AcquisitionCost_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    AssetMaint.UpdateBalances<FABookBalance.acquisitionCost, FADetails.acquisitionCost>(sender, e);
    ((PXSelectBase) this.GLTrnFilter).Cache.SetDefaultExt<GLTranFilter.acquisitionCost>((object) ((PXSelectBase<GLTranFilter>) this.GLTrnFilter).Current);
  }

  protected virtual void FADetails_SalvageAmount_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    AssetMaint.UpdateBalances<FABookBalance.salvageAmount, FADetails.salvageAmount>(sender, e);
  }

  protected virtual void FADetails_SalvageAmount_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    FADetails row = (FADetails) e.Row;
    Decimal? newValue = (Decimal?) e.NewValue;
    Decimal? acquisitionCost = row.AcquisitionCost;
    if (newValue.GetValueOrDefault() > acquisitionCost.GetValueOrDefault() & newValue.HasValue & acquisitionCost.HasValue)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
      {
        (object) row.AcquisitionCost
      });
  }

  protected virtual void FADetails_TagNbr_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    FixedAsset fixedAsset = PXParentAttribute.SelectParent<FixedAsset>(sender, e.Row);
    if (fixedAsset == null || fixedAsset.AssetCD == null || !((PXSelectBase<FASetup>) this.fasetup).Current.CopyTagFromAssetID.GetValueOrDefault())
      return;
    e.NewValue = (object) fixedAsset.AssetCD;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void FADetails_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    FADetails row1 = (FADetails) e.Row;
    bool? nullable;
    if (row1 != null)
    {
      nullable = ((PXSelectBase<FASetup>) this.fasetup).Current.CopyTagFromAssetID;
      if (nullable.GetValueOrDefault() && (e.Operation & 3) == 2)
        row1.TagNbr = this._PersistedAsset.AssetCD;
    }
    PXCache pxCache = sender;
    object row2 = e.Row;
    FixedAsset current = ((PXSelectBase<FixedAsset>) this.Asset).Current;
    int num1;
    if (current == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = current.UnderConstruction;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    int num2 = num1 != 0 ? 2 : 1;
    PXDefaultAttribute.SetPersistingCheck<FADetails.depreciateFromDate>(pxCache, row2, (PXPersistingCheck) num2);
  }

  protected virtual void FASetup_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    FASetup row = (FASetup) e.Row;
    if (!row.CopyTagFromAssetID.GetValueOrDefault())
      return;
    row.TagNumberingID = (string) null;
  }

  protected virtual void FABookBalance_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!string.IsNullOrEmpty(((FABookBalance) e.Row).InitPeriod) && e.ExternalCall)
      throw new PXSetPropertyException("Record cannot be deleted.");
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FABookBalance> e)
  {
    FABookBalance row = e.Row;
    if (((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault() || !row.UpdateGL.GetValueOrDefault() || ((PXSelectBase<FixedAsset>) this.Asset).Current == null)
      return;
    int num = PXAccess.GetParentOrganizationID(((PXSelectBase<FixedAsset>) this.Asset).Current.BranchID).Value;
    List<string> stringList = new List<string>();
    if (row.DeprFromPeriod != null)
      stringList.Add(row.DeprFromPeriod);
    if (row.LastDeprPeriod != null)
      stringList.Add(row.LastDeprPeriod);
    foreach (string period in stringList)
    {
      PXResultset<FABookPeriod, FinPeriod> pxResultset = PXSelectBase<FABookPeriod, PXViewOf<FABookPeriod>.BasedOn<SelectFromBase<FABookPeriod, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<FinPeriod>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.organizationID, Equal<FinPeriod.organizationID>>>>>.And<BqlOperand<FABookPeriod.finPeriodID, IBqlString>.IsEqual<FinPeriod.finPeriodID>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookPeriod.finPeriodID, Equal<P.AsString>>>>, And<BqlOperand<FABookPeriod.organizationID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<FABookPeriod.bookID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select<PXResultset<FABookPeriod, FinPeriod>>((PXGraph) this, new object[3]
      {
        (object) period,
        (object) num,
        (object) row.BookID
      });
      FABookPeriod faBookPeriod = PXResultset<FABookPeriod, FinPeriod>.op_Implicit(pxResultset);
      FinPeriod finPeriod = PXResultset<FABookPeriod, FinPeriod>.op_Implicit(pxResultset);
      PXException pxException = new PXException("The document cannot be released, because the {0} period in the posting book does not match the financial period in the general ledger for the {1} company. To amend the periods in the posting book based on the periods in the general ledger, on the Book Calendars (FA304000) form, click Synchronize FA Calendar with GL.", new object[2]
      {
        (object) PeriodIDAttribute.FormatForError(period),
        (object) PXAccess.GetOrganizationCD(new int?(num))
      });
      if (faBookPeriod != null)
      {
        DateTime? nullable1;
        DateTime? nullable2;
        if (faBookPeriod != null && finPeriod != null && finPeriod.FinPeriodID != null)
        {
          nullable1 = faBookPeriod.StartDate;
          nullable2 = finPeriod.StartDate;
          if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
          {
            nullable2 = faBookPeriod.EndDate;
            nullable1 = finPeriod.EndDate;
            if ((nullable2.HasValue == nullable1.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() != nullable1.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
              goto label_11;
          }
          else
            goto label_11;
        }
        if (finPeriod == null || finPeriod.FinPeriodID == null)
        {
          FinPeriod firstPeriod = this.FinPeriodRepository.FindFirstPeriod(new int?(num));
          if (firstPeriod != null && firstPeriod.FinPeriodID != null)
          {
            nullable1 = firstPeriod.StartDate;
            nullable2 = faBookPeriod.StartDate;
            if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
              continue;
          }
          throw pxException;
        }
        continue;
      }
label_11:
      throw pxException;
    }
  }

  protected virtual void _(PX.Data.Events.RowInserted<FABookBalance> e)
  {
    PX.Objects.FA.AssetProcess.AdjustFixedAssetStatus((PXGraph) this, e.Row.AssetID);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FABookBalance> e)
  {
    PX.Objects.FA.AssetProcess.AdjustFixedAssetStatus((PXGraph) this, e.Row.AssetID);
  }

  protected virtual void FABookBalance_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    FABookBalance row = (FABookBalance) e.Row;
    if (row == null)
      return;
    bool flag1 = false;
    bool flag2 = !string.IsNullOrEmpty(row.InitPeriod);
    bool flag3 = !((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault();
    bool flag4 = sender.GetStatus(e.Row) == 2;
    bool valueOrDefault = ((PXSelectBase<FixedAsset>) this.Asset).Current.UnderConstruction.GetValueOrDefault();
    if (!flag4)
    {
      FATran tran = PXResultset<FATran>.op_Implicit(PXSelectBase<FATran, PXSelectReadonly<FATran, Where<FATran.assetID, Equal<Required<FABookBalance.assetID>>, And<FATran.bookID, Equal<Required<FABookBalance.bookID>>>>, OrderBy<Desc<Switch<Case<Where<FATran.origin, Equal<FARegister.origin.depreciation>>, intMax, Case<Where<FATran.origin, Equal<FARegister.origin.purchasing>>, int2, Case<Where<FATran.origin, Equal<FARegister.origin.reconcilliation>>, int1>>>, int0>, Desc<Switch<Case<Where<FATran.tranType, Equal<FATran.tranType.depreciationPlus>>, intMax, Case<Where<FATran.tranType, Equal<FATran.tranType.depreciationMinus>>, intMax, Case<Where<FATran.tranType, Equal<FATran.tranType.calculatedPlus>>, intMax, Case<Where<FATran.tranType, Equal<FATran.tranType.calculatedMinus>>, intMax>>>>, int0>, Desc<FATran.released, Asc<FATran.refNbr, Asc<FATran.lineNbr>>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
      {
        (object) row.AssetID,
        (object) row.BookID
      }));
      flag1 = AssetMaint.IsDepreciated(tran);
      flag4 = !flag1 && !AssetMaint.IsPurchased(tran);
    }
    bool flag5 = flag4 || this.IsPureMigrated(row);
    bool? nullable;
    int num1;
    if (row == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = row.Depreciate;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    bool flag6 = num1 != 0;
    PXUIFieldAttribute.SetEnabled<FABookBalance.lastDeprPeriod>(sender, e.Row, flag5 & flag3 & flag6 && !valueOrDefault);
    PXUIFieldAttribute.SetEnabled<FABookBalance.ytdDepreciated>(sender, e.Row, flag5 & flag3 & flag6 && !valueOrDefault);
    PXUIFieldAttribute.SetEnabled<FABookBalance.deprFromDate>(sender, e.Row, !flag1);
    PXUIFieldAttribute.SetEnabled<FABookBalance.acquisitionCost>(sender, e.Row, !flag2 || flag4 & flag3);
    PXUIFieldAttribute.SetEnabled<FABookBalance.tax179Amount>(sender, e.Row, !flag2 & flag6);
    PXUIFieldAttribute.SetEnabled<FABookBalance.bonusAmount>(sender, e.Row, !flag2 & flag6);
    PXUIFieldAttribute.SetEnabled<FABookBalance.bonusRate>(sender, e.Row, !flag2 & flag6);
    PXUIFieldAttribute.SetEnabled<FABookBalance.bonusID>(sender, e.Row, !flag2 & flag6);
    if (row.Status == "D")
      PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
    IYearSetup faBookYearSetup = this.FABookPeriodRepository.FindFABookYearSetup(row.BookID);
    FADepreciationMethod depreciationMethod = PXResultset<FADepreciationMethod>.op_Implicit(PXSelectBase<FADepreciationMethod, PXSelect<FADepreciationMethod, Where<FADepreciationMethod.methodID, Equal<Current<FABookBalance.depreciationMethodID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    PXUIFieldAttribute.SetEnabled<FABookBalance.usefulLife>(sender, (object) row, row.Status == "A");
    PXUIFieldAttribute.SetEnabled<FABookBalance.averagingConvention>(sender, (object) row, depreciationMethod != null && !depreciationMethod.IsYearlyAccountancyTableMethod);
    List<KeyValuePair<object, Dictionary<object, string[]>>> keyValuePairList1 = new List<KeyValuePair<object, Dictionary<object, string[]>>>();
    if (depreciationMethod != null)
    {
      List<KeyValuePair<object, Dictionary<object, string[]>>> keyValuePairList2 = keyValuePairList1;
      nullable = depreciationMethod.IsTableMethod;
      KeyValuePair<object, Dictionary<object, string[]>> keyValuePair = nullable.GetValueOrDefault() ? new KeyValuePair<object, Dictionary<object, string[]>>((object) depreciationMethod.RecordType, FAAveragingConvention.RecordTypeDisabledValues) : new KeyValuePair<object, Dictionary<object, string[]>>((object) depreciationMethod.DepreciationMethod, FAAveragingConvention.DeprMethodDisabledValues);
      keyValuePairList2.Add(keyValuePair);
    }
    if (faBookYearSetup != null)
      keyValuePairList1.Add(new KeyValuePair<object, Dictionary<object, string[]>>((object) faBookYearSetup.IsFixedLengthPeriod, FAAveragingConvention.FixedLengthPeriodDisabledValues));
    FAAveragingConvention.SetAveragingConventionsList<FADepreciationMethod.averagingConvention>(sender, (object) row, keyValuePairList1.ToArray());
    PXCache pxCache1 = sender;
    nullable = row.Depreciate;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetRequired<FABookBalance.deprFromDate>(pxCache1, num2 != 0);
    FABookSettings faBookSettings = PXResultset<FABookSettings>.op_Implicit(PXSelectBase<FABookSettings, PXSelect<FABookSettings, Where<FABookSettings.assetID, Equal<Current<FixedAsset.classID>>, And<FABookSettings.bookID, Equal<Required<FABookBalance.bookID>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.BookID
    }));
    PXCache pxCache2 = sender;
    FABookBalance faBookBalance1 = row;
    int num3;
    if (faBookSettings == null)
    {
      num3 = 0;
    }
    else
    {
      nullable = faBookSettings.Sect179;
      num3 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<FABookBalance.tax179Amount>(pxCache2, (object) faBookBalance1, num3 != 0);
    PXCache pxCache3 = sender;
    FABookBalance faBookBalance2 = row;
    int num4;
    if (faBookSettings == null)
    {
      num4 = 0;
    }
    else
    {
      nullable = faBookSettings.Sect179;
      num4 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetEnabled<FABookBalance.ytdTax179Recap>(pxCache3, (object) faBookBalance2, num4 != 0);
    row.AllowChangeDeprFromPeriod = new bool?(!flag1);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FABookBalance.deprFromDate> e)
  {
    if (!(e.Row is FABookBalance row) || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FABookBalance.deprFromDate>, object, object>) e).NewValue == null)
      return;
    FADetails faDetails = ((PXSelectBase<FADetails>) this.AssetDetails).Current ?? PXResultset<FADetails>.op_Implicit(((PXSelectBase<FADetails>) this.AssetDetails).Select(Array.Empty<object>()));
    if (faDetails.ReceiptDate.HasValue)
    {
      DateTime? receiptDate = faDetails.ReceiptDate;
      if (receiptDate.Value.CompareTo(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FABookBalance.deprFromDate>, object, object>) e).NewValue) > 0)
      {
        object[] objArray = new object[1];
        receiptDate = ((PXSelectBase<FADetails>) this.AssetDetails).Current.ReceiptDate;
        objArray[0] = (object) receiptDate.Value.ToShortDateString();
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", objArray);
      }
    }
    this.CheckLastDeprPeriodBetweenDeprFromAndDeprTo<FABookBalance.deprFromDate>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FABookBalance.deprFromDate>>) e).Cache, row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FABookBalance.deprFromDate>, object, object>) e).NewValue);
  }

  protected virtual void FABookBalance_DeprFromPeriod_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    FABookBalance row = e.Row as FABookBalance;
    string strB1 = (string) e.OldValue;
    FixedAsset current = ((PXSelectBase<FixedAsset>) this.Asset).Current;
    bool flag1 = current != null && current.UnderConstruction.GetValueOrDefault();
    if (row == null || string.IsNullOrEmpty(row.DeprFromPeriod) || string.IsNullOrEmpty(strB1) && !flag1 || string.CompareOrdinal(row.DeprFromPeriod, strB1) == 0)
      return;
    if (string.CompareOrdinal(row.DeprFromPeriod, row.CurrDeprPeriod) != 0 && row.LastDeprPeriod == null)
    {
      row.CurrDeprPeriod = row.DeprFromPeriod;
      if (row.InitPeriod != null)
        row.InitPeriod = row.DeprFromPeriod;
    }
    if (!((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault() || FlaggedModeScopeBase<SuppressHistoryUpdateScope>.IsActive)
      return;
    string str1 = (string) null;
    if (flag1)
    {
      str1 = PXResultset<FABookHistory>.op_Implicit(PXSelectBase<FABookHistory, PXViewOf<FABookHistory>.BasedOn<SelectFromBase<FABookHistory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookHistory.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FABookHistory.bookID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<FABookHistory.ptdDeprBase, IBqlDecimal>.IsNotEqual<decimal0>>>.Order<PX.Data.BQL.Fluent.By<BqlField<FABookHistory.finPeriodID, IBqlString>.Asc>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.AssetID,
        (object) row.BookID
      }))?.FinPeriodID ?? row.DeprFromPeriod;
      if (string.IsNullOrEmpty(strB1))
        strB1 = str1;
    }
    bool flag2 = string.CompareOrdinal(row.DeprFromPeriod, strB1) > 0;
    string strB2 = flag2 ? strB1 : row.DeprFromPeriod;
    string strB3 = flag2 ? row.DeprFromPeriod : strB1;
    Decimal? nullable1 = new Decimal?(0M);
    Decimal num = flag2 ? 1M : -1M;
    foreach (PXResult<FABookPeriod, FABookHist> pxResult in PXSelectBase<FABookPeriod, PXSelectJoin<FABookPeriod, LeftJoin<FABookHist, On<FABookHist.assetID, Equal<Required<FABookBalance.assetID>>, And<FABookHist.bookID, Equal<FABookPeriod.bookID>, And<FABookHist.finPeriodID, Equal<FABookPeriod.finPeriodID>>>>>, Where<FABookPeriod.bookID, Equal<Required<FABookBalance.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.finPeriodID, GreaterEqual<Required<FABookBalance.deprFromPeriod>>, And<FABookPeriod.finPeriodID, LessEqual<Required<FABookBalance.deprToPeriod>>>>>>, OrderBy<Asc<FABookPeriod.finPeriodID>>>.Config>.Select((PXGraph) this, new object[5]
    {
      (object) row.AssetID,
      (object) row.BookID,
      (object) this.FABookPeriodRepository.GetFABookPeriodOrganizationID(row),
      (object) strB2,
      (object) strB3
    }))
    {
      FABookPeriod faBookPeriod = PXResult<FABookPeriod, FABookHist>.op_Implicit(pxResult);
      FABookHist faBookHist1 = PXResult<FABookPeriod, FABookHist>.op_Implicit(pxResult);
      FABookHist keyedHistory = new FABookHist();
      keyedHistory.AssetID = row.AssetID;
      keyedHistory.BookID = row.BookID;
      keyedHistory.FinPeriodID = faBookPeriod.FinPeriodID;
      FABookHist faBookHist2 = FAHelper.InsertFABookHist((PXGraph) this, keyedHistory, ref row);
      string str2 = (string) sender.GetValueOriginal<FABookBalance.deprFromPeriod>((object) row);
      if (string.IsNullOrEmpty(str2) & flag1)
        str2 = str1;
      if (str2 != null & flag2 && str2 != strB2 && faBookPeriod.FinPeriodID == strB2)
        faBookHist1 = faBookHist2;
      Decimal? nullable2;
      Decimal? nullable3;
      if (!flag2)
      {
        if (string.CompareOrdinal(faBookHist1.FinPeriodID ?? faBookPeriod.FinPeriodID, strB2) != 0)
        {
          nullable3 = faBookHist1.PtdAcquired;
        }
        else
        {
          nullable2 = faBookHist1.YtdAcquired;
          nullable3 = nullable2 ?? row.YtdAcquired;
        }
      }
      else
        nullable3 = faBookHist1.PtdDeprBase;
      Decimal? nullable4 = nullable3;
      nullable4 = new Decimal?(nullable4.GetValueOrDefault() * num);
      FABookHist faBookHist3 = faBookHist2;
      nullable2 = faBookHist3.PtdDeprBase;
      Decimal? nullable5 = nullable4;
      faBookHist3.PtdDeprBase = nullable2.HasValue & nullable5.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
      FABookHist faBookHist4 = faBookHist2;
      nullable5 = faBookHist4.YtdDeprBase;
      nullable2 = nullable4;
      faBookHist4.YtdDeprBase = nullable5.HasValue & nullable2.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      nullable2 = nullable1;
      nullable5 = nullable4;
      nullable1 = nullable2.HasValue & nullable5.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
      if (string.CompareOrdinal(faBookPeriod.FinPeriodID, strB3) == 0)
      {
        FABookHist faBookHist5 = faBookHist2;
        nullable5 = faBookHist5.PtdDeprBase;
        nullable2 = nullable1;
        faBookHist5.PtdDeprBase = nullable5.HasValue & nullable2.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        FABookHist faBookHist6 = faBookHist2;
        nullable2 = faBookHist6.YtdDeprBase;
        nullable5 = nullable1;
        faBookHist6.YtdDeprBase = nullable2.HasValue & nullable5.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
      }
    }
  }

  protected virtual void FABookBalance_DeprFromPeriod_CommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
    if (!(e.Row is FABookBalance row) || (e.Operation & 3) != 1 || row.AllowChangeDeprFromPeriod.GetValueOrDefault())
      return;
    e.IsRestriction = true;
  }

  protected virtual void FABookBalance_BookID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (PXResultset<FABookSettings>.op_Implicit(PXSelectBase<FABookSettings, PXSelect<FABookSettings, Where<FABookSettings.assetID, Equal<Current<FixedAsset.classID>>, And<FABookSettings.bookID, Equal<Required<FABookSettings.bookID>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    })) == null)
      throw new PXSetPropertyException("'{0}' cannot be found in the system.", new object[1]
      {
        (object) "[bookID]"
      });
  }

  protected virtual void FABookBalance_DepreciationMethodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    FABookBalance row = (FABookBalance) e.Row;
    if (row == null || !row.BookID.HasValue)
      return;
    IYearSetup faBookYearSetup = this.FABookPeriodRepository.FindFABookYearSetup(row.BookID);
    FADepreciationMethod depreciationMethod = PXSelectorAttribute.Select<FABookSettings.depreciationMethodID>(((PXSelectBase) this.DepreciationSettings).Cache, (object) row, e.NewValue) as FADepreciationMethod;
    if ((faBookYearSetup.PeriodType == "WK" || faBookYearSetup.PeriodType == "BW" || faBookYearSetup.PeriodType == "FW") && depreciationMethod != null && depreciationMethod.IsNewZealandMethod)
    {
      e.NewValue = (object) depreciationMethod?.MethodCD;
      throw new PXSetPropertyException(PXMessages.LocalizeFormat("The {0} calculation method cannot be selected for a book that uses a week-based calendar.", new object[1]
      {
        (object) PXStringListAttribute.GetLocalizedLabel<FADepreciationMethod.depreciationMethod>(((PXGraph) this).Caches[typeof (FADepreciationMethod)], (object) depreciationMethod)
      }));
    }
  }

  protected virtual void FABookBalance_DepreciationMethodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    FABookBalance row = (FABookBalance) e.Row;
    if ((row != null ? (!row.DepreciationMethodID.HasValue ? 1 : 0) : 1) != 0)
      return;
    if (e.ExternalCall)
    {
      FADepreciationMethod depreciationMethod = PXResultset<FADepreciationMethod>.op_Implicit(PXSelectBase<FADepreciationMethod, PXSelect<FADepreciationMethod, Where<FADepreciationMethod.methodID, Equal<Current<FABookBalance.depreciationMethodID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) row
      }, Array.Empty<object>()));
      sender.SetValueExt<FABookBalance.averagingConvention>((object) row, (object) (depreciationMethod?.AveragingConvention ?? row.AveragingConvention));
    }
    if (!((PXGraph) this).IsImport)
      return;
    bool flag = false;
    object depreciationMethodId = (object) row.DepreciationMethodID;
    try
    {
      sender.RaiseFieldVerifying<FABookBalance.depreciationMethodID>((object) row, ref depreciationMethodId);
    }
    catch (PXException ex)
    {
      flag = true;
    }
    if (flag)
      return;
    sender.RaiseExceptionHandling<FABookBalance.depreciationMethodID>((object) row, depreciationMethodId, (Exception) null);
  }

  protected virtual void FABookBalance_DeprToPeriod_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    FABookBalance row = (FABookBalance) e.Row;
    if (row == null || e.OldValue == null)
      return;
    row.LastPeriod = row.DeprToPeriod;
  }

  protected virtual void FABookBalance_SalvageAmount_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    FABookBalance row = (FABookBalance) e.Row;
    (Decimal MinValue, Decimal MaxValue) range;
    if (!AssetMaint.IsValueInSignedRange((Decimal?) e.NewValue, row.AcquisitionCost, out range))
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0} and less than or equal to {1}.", new object[2]
      {
        (object) range.MinValue,
        (object) range.MaxValue
      });
  }

  protected virtual void FABookBalance_LastDeprPeriod_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    FABookBalance row = (FABookBalance) e.Row;
    if (e.NewValue == null)
      return;
    if (string.CompareOrdinal((string) e.NewValue, row.DeprFromPeriod) < 0)
    {
      e.NewValue = (object) PeriodIDAttribute.FormatForDisplay((string) e.NewValue);
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
      {
        (object) PeriodIDAttribute.FormatForError(row.DeprFromPeriod)
      });
    }
    if (string.CompareOrdinal((string) e.NewValue, row.DeprToPeriod) > 0)
    {
      e.NewValue = (object) PeriodIDAttribute.FormatForDisplay((string) e.NewValue);
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
      {
        (object) PeriodIDAttribute.FormatForError(row.DeprToPeriod)
      });
    }
  }

  protected virtual void FABookBalance_DeprToDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    FABookBalance row = (FABookBalance) e.Row;
    DateTime? newValue = (DateTime?) e.NewValue;
    DateTime? deprFromDate = row.DeprFromDate;
    if ((newValue.HasValue & deprFromDate.HasValue ? (newValue.GetValueOrDefault() < deprFromDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
      {
        (object) row.DeprFromDate
      });
  }

  protected virtual void FAUsage_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    FAUsage row = (FAUsage) e.Row;
    if (row != null && row.Depreciated.GetValueOrDefault())
      throw new PXSetPropertyException("Record cannot be deleted.");
  }

  protected virtual void FAUsage_Value_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    FAUsage row = (FAUsage) e.Row;
    if (row == null)
      return;
    if ((Decimal) e.NewValue <= 0M)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", (PXErrorLevel) 0);
    FAUsage faUsage = PXResultset<FAUsage>.op_Implicit(PXSelectBase<FAUsage, PXSelect<FAUsage, Where<FAUsage.assetID, Equal<Current<FADetails.assetID>>, And<FAUsage.number, Less<Current<FAUsage.number>>>>, OrderBy<Desc<FAUsage.number>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    Decimal? totalExpectedUsage;
    if (faUsage != null)
    {
      Decimal newValue = (Decimal) e.NewValue;
      totalExpectedUsage = faUsage.Value;
      Decimal valueOrDefault = totalExpectedUsage.GetValueOrDefault();
      if (newValue <= valueOrDefault & totalExpectedUsage.HasValue)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
        {
          (object) faUsage.Value
        });
    }
    FADetails faDetails = PXResultset<FADetails>.op_Implicit(PXSelectBase<FADetails, PXSelect<FADetails, Where<FADetails.assetID, Equal<Required<FADetails.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.AssetID
    }));
    if (faDetails == null)
      return;
    Decimal newValue1 = (Decimal) e.NewValue;
    totalExpectedUsage = faDetails.TotalExpectedUsage;
    Decimal valueOrDefault1 = totalExpectedUsage.GetValueOrDefault();
    if (newValue1 > valueOrDefault1 & totalExpectedUsage.HasValue)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
      {
        (object) faDetails.TotalExpectedUsage
      });
  }

  protected virtual void FAUsage_MeasurementDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    FAUsage row = (FAUsage) e.Row;
    e.NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate;
    if (row == null || e.NewValue == null)
      return;
    FAUsage faUsage = PXResultset<FAUsage>.op_Implicit(PXSelectBase<FAUsage, PXSelect<FAUsage, Where<FAUsage.assetID, Equal<Current<FADetails.assetID>>, And<FAUsage.number, Less<Current<FAUsage.number>>>>, OrderBy<Desc<FAUsage.number>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    if (faUsage == null)
      return;
    DateTime newValue = (DateTime) e.NewValue;
    DateTime? measurementDate = faUsage.MeasurementDate;
    if ((measurementDate.HasValue ? (newValue <= measurementDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    sender.RaiseExceptionHandling<FAUsage.measurementDate>((object) row, e.NewValue, (Exception) new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
    {
      (object) faUsage.MeasurementDate
    }));
  }

  protected virtual void FAUsage_MeasurementDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    FAUsage row = (FAUsage) e.Row;
    if (row == null)
      return;
    FAUsage faUsage = PXResultset<FAUsage>.op_Implicit(PXSelectBase<FAUsage, PXSelect<FAUsage, Where<FAUsage.assetID, Equal<Current<FADetails.assetID>>, And<FAUsage.number, Less<Current<FAUsage.number>>>>, OrderBy<Desc<FAUsage.number>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    if (faUsage != null)
    {
      DateTime newValue = (DateTime) e.NewValue;
      DateTime? measurementDate = faUsage.MeasurementDate;
      if ((measurementDate.HasValue ? (newValue <= measurementDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
        {
          (object) faUsage.MeasurementDate
        });
    }
    sender.RaiseExceptionHandling<FAUsage.measurementDate>((object) row, e.NewValue, (Exception) null);
  }

  protected virtual void FALocationHistory_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (!(e.NewRow is FALocationHistory newRow) || !FAInnerStateDescriptor.IsAcquired(newRow.AssetID, (PXGraph) this) || FAInnerStateDescriptor.WillBeTransferred(newRow.AssetID, (PXGraph) this) || !((PXGraph) this).IsLocationChanged(e.Row as FALocationHistory, newRow))
      return;
    FALocationHistory faLocationHistory = PXCache<FALocationHistory>.CreateCopy(newRow);
    try
    {
      if (sender.GetStatus(e.Row) == 1)
        sender.SetStatus(e.Row, (PXEntryStatus) 0);
      faLocationHistory.RefNbr = (string) null;
      faLocationHistory.PeriodID = this.GetTransferPeriod(newRow.AssetID);
      faLocationHistory.ClassID = ((PXSelectBase<FixedAsset>) this.CurrentAsset).Current.ClassID;
      faLocationHistory.RevisionID = ((PXSelectBase<FADetails>) this.AssetDetails).Current.LocationRevID;
      faLocationHistory.TransactionDate = this.GetTransferDate(newRow.AssetID, faLocationHistory.PeriodID);
      faLocationHistory = (FALocationHistory) sender.Insert((object) faLocationHistory);
    }
    finally
    {
      ((CancelEventArgs) e).Cancel = faLocationHistory != null;
      if (!((CancelEventArgs) e).Cancel)
        GraphHelper.MarkUpdated(sender, (object) newRow);
    }
  }

  public static void LiveUpdateMaskedSubs(
    PXGraph graph,
    PXCache facache,
    FALocationHistory lochist)
  {
    if (lochist == null)
      return;
    FixedAsset asset = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FALocationHistory.assetID>>>>.Config>.SelectSingleBound(graph, new object[1]
    {
      (object) lochist
    }, Array.Empty<object>()));
    FABookBalance faBookBalance = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>>, OrderBy<Desc<FABookBalance.updateGL>>>.Config>.SelectSingleBound(graph, new object[1]
    {
      (object) asset
    }, Array.Empty<object>()));
    if (faBookBalance != null)
    {
      Decimal? ytdDeprBase = faBookBalance.YtdDeprBase;
      Decimal num = 0M;
      if (!(ytdDeprBase.GetValueOrDefault() == num & ytdDeprBase.HasValue))
        return;
    }
    asset.FASubID = AssetMaint.MakeSubID<FixedAsset.fASubMask, FixedAsset.fASubID>(facache, asset);
    asset.AccumulatedDepreciationSubID = AssetMaint.MakeSubID<FixedAsset.accumDeprSubMask, FixedAsset.accumulatedDepreciationSubID>(facache, asset);
    asset.DepreciatedExpenseSubID = AssetMaint.MakeSubID<FixedAsset.deprExpenceSubMask, FixedAsset.depreciatedExpenseSubID>(facache, asset);
    asset.DisposalSubID = AssetMaint.MakeSubID<FixedAsset.proceedsSubMask, FixedAsset.disposalSubID>(facache, asset);
    asset.GainSubID = AssetMaint.MakeSubID<FixedAsset.gainLossSubMask, FixedAsset.gainSubID>(facache, asset);
    asset.LossSubID = AssetMaint.MakeSubID<FixedAsset.gainLossSubMask, FixedAsset.lossSubID>(facache, asset);
    GraphHelper.MarkUpdated(facache, (object) asset);
  }

  protected virtual void FALocationHistory_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    AssetMaint.LiveUpdateMaskedSubs((PXGraph) this, ((PXSelectBase) this.Asset).Cache, (FALocationHistory) e.Row);
  }

  protected virtual void FALocationHistory_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    AssetMaint.LiveUpdateMaskedSubs((PXGraph) this, ((PXSelectBase) this.Asset).Cache, (FALocationHistory) e.Row);
  }

  protected virtual void FALocationHistory_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    FALocationHistory row = (FALocationHistory) e.Row;
    if (row == null)
      return;
    foreach (FALocationHistory to in sender.Cached)
    {
      PXEntryStatus status = sender.GetStatus((object) to);
      if (status == 2 || status == 1)
      {
        AssetMaint.CopyLocation(row, to);
        ((CancelEventArgs) e).Cancel = true;
        break;
      }
    }
    if (((CancelEventArgs) e).Cancel)
      return;
    if (((PXSelectBase<FADetails>) this.AssetDetails).Current == null)
      ((PXSelectBase<FADetails>) this.AssetDetails).Current = PXResultset<FADetails>.op_Implicit(((PXSelectBase<FADetails>) this.AssetDetails).Select(Array.Empty<object>()));
    FADetails current = ((PXSelectBase<FADetails>) this.AssetDetails).Current;
    FALocationHistory faLocationHistory = row;
    int? revisionId = faLocationHistory.RevisionID;
    int? nullable1 = revisionId.HasValue ? new int?(revisionId.GetValueOrDefault() + 1) : new int?();
    faLocationHistory.RevisionID = nullable1;
    int? nullable2 = nullable1;
    current.LocationRevID = nullable2;
    ((PXSelectBase) this.AssetDetails).Cache.Update((object) ((PXSelectBase<FADetails>) this.AssetDetails).Current);
  }

  protected virtual void FALocationHistory_EmployeeID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    FALocationHistory row = (FALocationHistory) e.Row;
    sender.SetDefaultExt<FALocationHistory.locationID>((object) row);
    sender.SetDefaultExt<FALocationHistory.department>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FALocationHistory.locationID> e)
  {
    FixedAsset current = ((PXSelectBase<FixedAsset>) this.CurrentAsset).Current;
    if (current == null)
      return;
    bool? nullable1 = ((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL;
    if (!nullable1.GetValueOrDefault() && this.HasAnyGLTran(current.AssetID))
    {
      PX.Objects.GL.Branch branch = PXSelectorAttribute.Select<FALocationHistory.locationID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FALocationHistory.locationID>>) e).Cache, e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FALocationHistory.locationID>, object, object>) e).NewValue) as PX.Objects.GL.Branch;
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FALocationHistory.locationID>, object, object>) e).NewValue = (object) branch?.BranchCD;
      throw new PXSetPropertyException("This operation cannot be completed in migration mode. To exit migration mode, select the Update GL check box on the Fixed Assets Preferences (FA101000) form.");
    }
    if (!PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>())
      return;
    nullable1 = current.IsAcquired;
    if (!nullable1.GetValueOrDefault())
      return;
    int? parentOrganizationId1 = PXAccess.GetParentOrganizationID(current.BranchID);
    int? parentOrganizationId2 = PXAccess.GetParentOrganizationID((int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FALocationHistory.locationID>, object, object>) e).NewValue);
    int? nullable2 = parentOrganizationId1;
    int? nullable3 = parentOrganizationId2;
    if (!(nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue))
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FALocationHistory.locationID>, object, object>) e).NewValue = (object) PXAccess.GetBranchCD((int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FALocationHistory.locationID>, object, object>) e).NewValue);
      throw new PXSetPropertyException("The fixed asset cannot be transferred between different companies. Select a branch from the company of the fixed asset.");
    }
  }

  protected virtual void FALocationHistory_LocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.ExternalCall)
    {
      sender.SetDefaultExt<FALocationHistory.buildingID>(e.Row);
      sender.SetValuePending<FALocationHistory.buildingID>(e.Row, (object) null);
    }
    FixedAsset current = ((PXSelectBase<FixedAsset>) this.CurrentAsset).Current;
    FADetails faDetails = PXResultset<FADetails>.op_Implicit(((PXSelectBase<FADetails>) this.AssetDetails).Select(Array.Empty<object>()));
    FALocationHistory row = e.Row as FALocationHistory;
    if (!PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>() || current.IsAcquired.GetValueOrDefault() || faDetails == null || row == null)
      return;
    current.BranchID = row.BranchID;
    GraphHelper.MarkUpdated(((PXSelectBase) this.CurrentAsset).Cache, (object) current);
    foreach (PXResult<FABookBalance> pxResult in ((PXSelectBase<FABookBalance>) this.AssetBalance).Select(Array.Empty<object>()))
    {
      FABookBalance faBookBalance = PXResult<FABookBalance>.op_Implicit(pxResult);
      FABookBalance copy = PXCache<FABookBalance>.CreateCopy(faBookBalance);
      copy.NoteID = new Guid?(Guid.NewGuid());
      copy.DeprFromPeriod = (string) PXFormulaAttribute.Evaluate<FABookBalance.deprFromPeriod>(((PXSelectBase) this.AssetBalance).Cache, (object) faBookBalance);
      copy.DeprToPeriod = (string) PXFormulaAttribute.Evaluate<FABookBalance.deprToPeriod>(((PXSelectBase) this.AssetBalance).Cache, (object) faBookBalance);
      ((PXSelectBase<FABookBalance>) this.AssetBalance).Update(copy);
    }
  }

  protected virtual void FALocationHistory_BuildingID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.ExternalCall)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FALocationHistory.department> e)
  {
    FixedAsset current = ((PXSelectBase<FixedAsset>) this.CurrentAsset).Current;
    if (current != null && !((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault() && this.HasAnyGLTran(current.AssetID))
      throw new PXSetPropertyException("This operation cannot be completed in migration mode. To exit migration mode, select the Update GL check box on the Fixed Assets Preferences (FA101000) form.");
  }

  protected virtual void FAComponent_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    FixedAsset current = ((PXSelectBase<FixedAsset>) this.Asset).Current;
    FAComponent row = (FAComponent) e.Row;
    if (current == null || row == null || e.Operation != 3)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void FAComponent_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    FAComponent row = (FAComponent) e.Row;
    FixedAsset current = ((PXSelectBase<FixedAsset>) this.Asset).Current;
    if (row == null || current == null || row.AssetCD == null)
      return;
    FAComponent faComponent = PXResultset<FAComponent>.op_Implicit(PXSelectBase<FAComponent, PXSelectReadonly<FAComponent, Where<FAComponent.assetCD, Equal<Required<FAComponent.assetCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.AssetCD
    }));
    PXCache<FAComponent>.RestoreCopy((FAComponent) e.Row, faComponent);
    sender.SetStatus(e.Row, (PXEntryStatus) 1);
    sender.SetValue<FixedAsset.parentAssetID>(e.Row, (object) current.AssetID);
  }

  protected virtual void FAComponent_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    FAComponent row = (FAComponent) e.Row;
    sender.SetValue<FixedAsset.parentAssetID>((object) row, (object) null);
    sender.SetStatus((object) row, (PXEntryStatus) 1);
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void ReverseDisposalInfo_ReverseDisposalPeriodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if ((ReverseDisposalInfo) e.Row == null)
      return;
    OrganizationFinPeriod organizationFinPeriod = PXResultset<OrganizationFinPeriod>.op_Implicit(PXSelectBase<OrganizationFinPeriod, PXSelectJoin<OrganizationFinPeriod, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.organizationID, Equal<OrganizationFinPeriod.organizationID>>>, Where<OrganizationFinPeriod.finPeriodID, Equal<Required<OrganizationFinPeriod.finPeriodID>>, And<PX.Objects.GL.Branch.branchID, Equal<Current<FixedAsset.branchID>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) (string) e.NewValue
    }));
    if (organizationFinPeriod == null)
      return;
    if (organizationFinPeriod.Status == "Inactive")
      sender.RaiseExceptionHandling<ReverseDisposalInfo.reverseDisposalPeriodID>(e.Row, (object) null, (Exception) new FiscalPeriodInactiveException(organizationFinPeriod.FinPeriodID, PXAccess.GetOrganizationCD(organizationFinPeriod.OrganizationID), (PXErrorLevel) 2));
    if (!(organizationFinPeriod.Status == "Locked"))
      return;
    sender.RaiseExceptionHandling<ReverseDisposalInfo.reverseDisposalPeriodID>(e.Row, (object) null, (Exception) new FiscalPeriodLockedException(organizationFinPeriod.FinPeriodID, PXAccess.GetOrganizationCD(organizationFinPeriod.OrganizationID), (PXErrorLevel) 2));
  }

  protected virtual void FATran_AssetID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void FATran_BookID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void FATran_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    FATran row = (FATran) e.Row;
    if (row.Released.GetValueOrDefault() && e.ExternalCall || row.Released.GetValueOrDefault() && !string.IsNullOrEmpty(row.BatchNbr))
      throw new PXSetPropertyException("The record cannot be deleted.");
  }

  protected virtual void FATran_RefNbr_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    FATran row = (FATran) e.Row;
    if (row == null || !AssetGLTransactions.IsTempKey(row.RefNbr))
      return;
    e.ReturnValue = (object) null;
  }

  protected virtual void GLTranFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (((PXGraph) this).UnattendedMode && !((PXGraph) this).IsContractBasedAPI && !((PXGraph) this).IsImport && !((PXGraph) this).IsExport)
      return;
    GLTranFilter row = (GLTranFilter) e.Row;
    FixedAsset current = ((PXSelectBase<FixedAsset>) this.CurrentAsset).Current;
    FADetails faDetails = ((PXSelectBase<FADetails>) this.AssetDetails).Current ?? PXResultset<FADetails>.op_Implicit(((PXSelectBase<FADetails>) this.AssetDetails).Select(Array.Empty<object>()));
    if (row == null || current == null || faDetails == null)
      return;
    FATran faTran1 = PXResultset<FATran>.op_Implicit(PXSelectBase<FATran, PXSelect<FATran, Where<FATran.assetID, Equal<Current<FixedAsset.assetID>>, And<FATran.released, Equal<False>, And<Where<FATran.tranType, Equal<FATran.tranType.purchasingPlus>, Or<FATran.tranType, Equal<FATran.tranType.purchasingMinus>>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    FATran faTran2 = PXResultset<FATran>.op_Implicit(PXSelectBase<FATran, PXSelect<FATran, Where<FATran.assetID, Equal<Current<FixedAsset.assetID>>, And<FATran.released, Equal<False>, And<Where<FATran.tranType, Equal<FATran.tranType.reconcilliationPlus>, Or<FATran.tranType, Equal<FATran.tranType.reconcilliationMinus>>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    FinPeriod finPeriod = (FinPeriod) null;
    if (row.TranDate.HasValue && row.BranchID.HasValue)
      finPeriod = this.FinPeriodRepository.FindFinPeriodByDate(row.TranDate, PXAccess.GetParentOrganizationID(row.BranchID));
    PXUIFieldAttribute.SetWarning<GLTranFilter.acquisitionCost>(sender, (object) row, ((PXSelectBase) this.CurrentAsset).Cache.GetStatus((object) current) == 2 ? "The fixed asset is not saved." : (string) null);
    PXUIFieldAttribute.SetWarning<GLTranFilter.currentCost>(sender, (object) row, faTran1 != null ? "The fixed asset has unreleased purchasing transactions." : (string) null);
    PXUIFieldAttribute.SetWarning<GLTranFilter.accrualBalance>(sender, (object) row, faTran2 != null ? "The fixed asset has unreleased reconciliation transactions." : (string) null);
    PXCache pxCache1 = sender;
    GLTranFilter glTranFilter1 = row;
    int num1 = Math.Sign(row.UnreconciledAmt.GetValueOrDefault());
    Decimal? nullable1 = row.CurrentCost;
    int num2 = Math.Sign(nullable1.GetValueOrDefault());
    string str1 = num1 * num2 < 0 ? string.Format(PXMessages.LocalizeNoPrefix("The current cost of the {0} fixed asset and its unreconciled amount must have the same sign."), (object) current?.AssetCD) : (string) null;
    PXUIFieldAttribute.SetError<GLTranFilter.unreconciledAmt>(pxCache1, (object) glTranFilter1, str1);
    PXCache pxCache2 = sender;
    GLTranFilter glTranFilter2 = row;
    bool? nullable2;
    string str2;
    if (finPeriod != null && finPeriod.FAClosed.GetValueOrDefault())
    {
      nullable2 = ((PXSelectBase<GLSetup>) this.glsetup).Current.PostClosedPeriods;
      bool flag = false;
      if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
      {
        str2 = "The selected period will be automatically changed to the nearest open period.";
        goto label_9;
      }
    }
    str2 = (string) null;
label_9:
    PXUIFieldAttribute.SetWarning<GLTranFilter.periodID>(pxCache2, (object) glTranFilter2, str2);
    PXCache pxCache3 = sender;
    GLTranFilter glTranFilter3 = row;
    nullable2 = faDetails.IsReconciled;
    int num3 = nullable2.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<GLTranFilter.reconType>(pxCache3, (object) glTranFilter3, num3 != 0);
    PXAction<FixedAsset> reduceUnreconCost = this.ReduceUnreconCost;
    int num4;
    if (row.ReconType == "+")
    {
      nullable1 = row.UnreconciledAmt;
      Decimal num5 = 0M;
      if (nullable1.GetValueOrDefault() > num5 & nullable1.HasValue)
      {
        nullable1 = row.AccrualBalance;
        Decimal num6 = 0M;
        if (nullable1.GetValueOrDefault() > num6 & nullable1.HasValue && faTran1 == null)
        {
          num4 = faTran2 == null ? 1 : 0;
          goto label_14;
        }
      }
    }
    num4 = 0;
label_14:
    ((PXAction) reduceUnreconCost).SetEnabled(num4 != 0);
  }

  protected virtual void DisposeParams_DisposalMethodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<DisposeParams.disposalAccountID>(e.Row);
    sender.SetDefaultExt<DisposeParams.disposalSubID>(e.Row);
  }

  protected virtual string GetMostRecentTransactionPeriodID(int? AssetID)
  {
    return ((PXSelectBase<FATran>) ((PXGraph) this).GetExtension<AssetMaint.AssetMaintFixedAssetChecksExtension>().recentTransactions).SelectSingle(new object[1]
    {
      (object) AssetID
    })?.TranPeriodID;
  }

  /// <summary>
  /// find the most recent transaction in non-posting book. Return transaction from posting book if there is no non-posting book for the asset
  /// </summary>
  /// <returns></returns>
  protected virtual FATran GetMostRecentTransaction(int? AssetID)
  {
    return PXResultset<FATran>.op_Implicit(PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<FABook>.On<BqlOperand<FABook.bookID, IBqlInt>.IsEqual<FATran.bookID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.assetID, Equal<P.AsInt>>>>>.And<BqlOperand<FATran.released, IBqlBool>.IsEqual<True>>>.Order<By<Asc<FABook.updateGL>, Desc<FATran.tranDate>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) AssetID
    }));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<DisposeParams.disposalPeriodID> e)
  {
    DisposeParams row = e.Row as DisposeParams;
    string newValue = (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<DisposeParams.disposalPeriodID>, object, object>) e).NewValue;
    (bool checkPassed, bool cannotFindPeriodByDate, string str1, string str2) = ((PXGraph) this).GetExtension<AssetMaint.AssetMaintFixedAssetChecksExtension>().CheckGivenPeriodOrDateNotEarlierThanTheLastTransactionPeriod(((PXSelectBase<FixedAsset>) this.CurrentAsset).Current, row.DisposalDate, newValue);
    if (cannotFindPeriodByDate)
      throw new PXSetPropertyException<DisposeParams.disposalPeriodID>("Book Period cannot be found in the system.");
    if (!checkPassed)
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<DisposeParams.disposalPeriodID>, object, object>) e).NewValue = (object) PeriodIDAttribute.FormatForDisplay(str1);
      throw new PXSetPropertyException<DisposeParams.disposalPeriodID>("The disposal period ({0}) cannot be earlier than the period of the most recent transaction ({1}).", new object[2]
      {
        (object) FinPeriodIDFormattingAttribute.FormatForError(str1),
        (object) FinPeriodIDFormattingAttribute.FormatForError(str2)
      });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<DisposeParams.disposalDate> e)
  {
    if (!(e.Row is DisposeParams row))
      return;
    if (row.DisposalPeriodID == null)
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<DisposeParams.disposalDate>, object, object>) e).NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate;
    }
    else
    {
      FATran recentTransaction = this.GetMostRecentTransaction((int?) ((PXSelectBase<FixedAsset>) this.Asset).Current?.AssetID);
      PX.Data.Events.FieldDefaulting<DisposeParams.disposalDate> fieldDefaulting = e;
      DateTime? businessDate = ((PXGraph) this).Accessinfo.BusinessDate;
      DateTime? nullable1 = (DateTime?) recentTransaction?.TranDate;
      DateTime? nullable2;
      if ((businessDate.HasValue & nullable1.HasValue ? (businessDate.GetValueOrDefault() > nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      {
        if (recentTransaction == null)
        {
          nullable1 = new DateTime?();
          nullable2 = nullable1;
        }
        else
          nullable2 = recentTransaction.TranDate;
      }
      else
        nullable2 = ((PXGraph) this).Accessinfo.BusinessDate;
      // ISSUE: variable of a boxed type
      __Boxed<DateTime?> local = (ValueType) nullable2;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<DisposeParams.disposalDate>, object, object>) fieldDefaulting).NewValue = (object) local;
    }
  }

  protected virtual void DisposeParams_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    DisposeParams row = (DisposeParams) e.Row;
    if (row == null)
      return;
    row.DisposalAccountID = new int?();
    row.DisposalSubID = new int?();
  }

  protected virtual void _(PX.Data.Events.RowSelected<DisposeParams> e)
  {
    if (e.Row == null)
      return;
    FABookBalance faBookBalance = PXResultset<FABookBalance>.op_Implicit(PXSelectBase<FABookBalance, PXViewOf<FABookBalance>.BasedOn<SelectFromBase<FABookBalance, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookBalance.assetID, Equal<BqlField<FixedAsset.assetID, IBqlInt>.FromCurrent.NoDefault>>>>>.And<BqlOperand<FABookBalance.updateGL, IBqlBool>.IsEqual<True>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (faBookBalance != null && string.CompareOrdinal(faBookBalance.CurrDeprPeriod, e.Row.DisposalPeriodID) < 0 && e.Row.ActionBeforeDisposal == "S")
      PXUIFieldAttribute.SetWarning<DisposeParams.actionBeforeDisposal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DisposeParams>>) e).Cache, (object) e.Row, PXMessages.LocalizeFormatNoPrefix(faBookBalance.LastDeprPeriod == null ? "The fixed asset has not been depreciated. The depreciation will be suspended from {0} to {1}." : "The fixed asset has been depreciated only until {0}. The depreciation will be suspended from {0} to {1}.", new object[2]
      {
        (object) FinPeriodIDFormattingAttribute.FormatForError(faBookBalance.LastDeprPeriod ?? faBookBalance.DeprFromPeriod),
        (object) FinPeriodIDFormattingAttribute.FormatForError(e.Row.DisposalPeriodID)
      }));
    else
      PXUIFieldAttribute.SetWarning<DisposeParams.actionBeforeDisposal>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DisposeParams>>) e).Cache, (object) e.Row, (string) null);
    FATran recentTransaction = this.GetMostRecentTransaction((int?) ((PXSelectBase<FixedAsset>) this.Asset).Current?.AssetID);
    Dictionary<int, FABook> books = PXDatabase.GetSlot<FABookCollection>("FABookCollection", new System.Type[1]
    {
      typeof (FABook)
    }).Books;
    if (recentTransaction != null)
    {
      DateTime? disposalDate1 = e.Row.DisposalDate;
      if (disposalDate1.HasValue && !books[recentTransaction.BookID.Value].UpdateGL.GetValueOrDefault())
      {
        disposalDate1 = e.Row.DisposalDate;
        DateTime? tranDate = recentTransaction.TranDate;
        if ((disposalDate1.HasValue & tranDate.HasValue ? (disposalDate1.GetValueOrDefault() < tranDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DisposeParams>>) e).Cache;
          DisposeParams row = e.Row;
          // ISSUE: variable of a boxed type
          __Boxed<DateTime?> disposalDate2 = (ValueType) e.Row.DisposalDate;
          object[] objArray = new object[1];
          tranDate = recentTransaction.TranDate;
          objArray[0] = (object) tranDate.Value.ToShortDateString();
          PXSetPropertyException propertyException = new PXSetPropertyException("The disposal date cannot be earlier than the date of the most recent transaction ({0}).", objArray);
          cache.RaiseExceptionHandling<DisposeParams.disposalDate>((object) row, (object) disposalDate2, (Exception) propertyException);
          return;
        }
      }
    }
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DisposeParams>>) e).Cache.RaiseExceptionHandling<DisposeParams.disposalDate>((object) e.Row, (object) e.Row.DisposalDate, (Exception) null);
  }

  protected virtual bool WasSuspended(int? assetID)
  {
    return PXResultset<FABookHistory>.op_Implicit(PXSelectBase<FABookHistory, PXViewOf<FABookHistory>.BasedOn<SelectFromBase<FABookHistory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FABookHistory.closed, Equal<True>>>>, And<BqlOperand<FABookHistory.suspended, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<FABookHistory.assetID, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) assetID
    })) != null;
  }

  private static bool IsDepreciated(FATran tran)
  {
    if (tran == null)
      return false;
    return tran.Origin == "D" || tran.TranType == "D+" || tran.TranType == "D-" || tran.TranType == "C+" || tran.TranType == "C-";
  }

  private static bool IsPurchased(FATran tran)
  {
    return tran != null && (tran.Origin == "P" || tran.Origin == "R") && tran.Released.GetValueOrDefault();
  }

  protected virtual bool IsPureMigrated(FABookBalance bookBalance)
  {
    bool flag = false;
    if (bookBalance.UpdateGL.GetValueOrDefault())
    {
      List<FATran> list = GraphHelper.RowCast<FATran>((IEnumerable) PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FATran.bookID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<FATran.released, IBqlBool>.IsEqual<True>>>, And<BqlOperand<FATran.batchNbr, IBqlString>.IsNull>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.tranType, Equal<FATran.tranType.purchasingPlus>>>>>.Or<BqlOperand<FATran.tranType, IBqlString>.IsEqual<FATran.tranType.reconcilliationPlus>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) bookBalance.AssetID,
        (object) bookBalance.BookID
      })).ToList<FATran>();
      if ((list.Count != 2 ? 0 : (new HashSet<(string, Decimal?)>(list.Select<FATran, (string, Decimal?)>((Func<FATran, (string, Decimal?)>) (transaction => (transaction.RefNbr, transaction.TranAmt)))).Count == 1 ? 1 : 0)) != 0)
        flag = PXResultset<FATran>.op_Implicit(PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FATran.bookID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<FATran.released, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<FATran.batchNbr, IBqlString>.IsNotNull>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
        {
          (object) bookBalance.AssetID,
          (object) bookBalance.BookID
        })) == null;
    }
    else
    {
      List<FATran> list1 = GraphHelper.RowCast<FATran>((IEnumerable) PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FATran.bookID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<FATran.released, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<FATran.batchNbr, IBqlString>.IsNull>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) bookBalance.AssetID,
        (object) bookBalance.BookID
      })).ToList<FATran>();
      Decimal? nullable;
      if (list1.Count == 2)
      {
        nullable = bookBalance.YtdDepreciated;
        Decimal num = 0M;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
          goto label_7;
      }
      if (list1.Count == 3)
      {
        nullable = bookBalance.YtdDepreciated;
        Decimal num = 0M;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
          goto label_17;
      }
      else
        goto label_17;
label_7:
      if (new HashSet<string>(list1.Select<FATran, string>((Func<FATran, string>) (transaction => transaction.RefNbr))).Count == 1)
      {
        List<FATran> list2 = GraphHelper.RowCast<FATran>((IEnumerable) list1.Where<FATran>((Func<FATran, bool>) (transaction => transaction.TranType == "P+"))).ToList<FATran>();
        List<FATran> list3 = GraphHelper.RowCast<FATran>((IEnumerable) list1.Where<FATran>((Func<FATran, bool>) (transaction => transaction.TranType == "R+"))).ToList<FATran>();
        List<FATran> list4 = GraphHelper.RowCast<FATran>((IEnumerable) list1.Where<FATran>((Func<FATran, bool>) (transaction => transaction.TranType == "D+"))).ToList<FATran>();
        FATran faTran1 = list2.FirstOrDefault<FATran>();
        FATran faTran2 = list3.FirstOrDefault<FATran>();
        FATran faTran3 = list4.FirstOrDefault<FATran>();
        int num;
        if (list2.Count == 1 && list3.Count == 1)
        {
          nullable = faTran1.TranAmt;
          Decimal? tranAmt = faTran2.TranAmt;
          if (nullable.GetValueOrDefault() == tranAmt.GetValueOrDefault() & nullable.HasValue == tranAmt.HasValue)
          {
            if (!list4.IsEmpty<FATran>())
            {
              if (list4.Count == 1)
              {
                tranAmt = faTran3.TranAmt;
                nullable = bookBalance.YtdDepreciated;
                num = tranAmt.GetValueOrDefault() == nullable.GetValueOrDefault() & tranAmt.HasValue == nullable.HasValue ? 1 : 0;
                goto label_16;
              }
              num = 0;
              goto label_16;
            }
            num = 1;
            goto label_16;
          }
        }
        num = 0;
label_16:
        flag = num != 0;
      }
    }
label_17:
    if (flag)
      flag = PXResultset<FATran>.op_Implicit(PXSelectBase<FATran, PXViewOf<FATran>.BasedOn<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FATran.assetID, Equal<P.AsInt>>>>, And<BqlOperand<FATran.bookID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<FATran.released, IBqlBool>.IsNotEqual<True>>>>.Config>.SelectSingleBound((PXGraph) this, new object[0], new object[2]
      {
        (object) bookBalance.AssetID,
        (object) bookBalance.BookID
      })) == null;
    return flag;
  }

  private string GetTransferPeriod(int? assetID)
  {
    return PXResultset<FABookBalanceTransfer>.op_Implicit(PXSelectBase<FABookBalanceTransfer, PXSelect<FABookBalanceTransfer, Where<FABookBalanceTransfer.assetID, Equal<Required<FixedAsset.assetID>>>, OrderBy<Desc<FABookBalanceTransfer.updateGL>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) assetID
    }))?.TransferPeriodID;
  }

  protected virtual int? GetDeprMethodOfAssetType(int? depreciationMethodID, FABookBalance balance)
  {
    return AssetMaint.GetDeprMethodOfAssetType((PXGraph) this, depreciationMethodID, balance);
  }

  public static int? GetDeprMethodOfAssetType(
    PXGraph graph,
    int? depreciationMethodID,
    FABookBalance balance)
  {
    if (!depreciationMethodID.HasValue)
      return new int?();
    FADepreciationMethod classMethod = PXResultset<FADepreciationMethod>.op_Implicit(PXSelectBase<FADepreciationMethod, PXViewOf<FADepreciationMethod>.BasedOn<SelectFromBase<FADepreciationMethod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FADepreciationMethod.methodID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(graph, new object[1]
    {
      (object) depreciationMethodID
    }));
    return classMethod.RecordType != "C" ? depreciationMethodID : AssetMaint.GetSuitableDepreciationMethod(graph, classMethod, balance.DeprFromDate, balance.BookID, balance.AssetID).MethodID;
  }

  public static FADepreciationMethod GetSuitableDepreciationMethod(
    PXGraph graph,
    FADepreciationMethod classMethod,
    DateTime? deprDate,
    int? bookID,
    int? assetID)
  {
    int? nullable = new int?();
    if (classMethod.AveragingConvention == "HQ")
      nullable = new int?((int) graph.GetService<IFABookPeriodRepository>().GetQuarterNumberOfDate(deprDate, bookID, assetID));
    if (classMethod.AveragingConvention == "HP")
      nullable = new int?((int) graph.GetService<IFABookPeriodRepository>().GetPeriodNumberOfDate(deprDate, bookID, assetID));
    return PXResultset<FADepreciationMethod>.op_Implicit(PXSelectBase<FADepreciationMethod, PXViewOf<FADepreciationMethod>.BasedOn<SelectFromBase<FADepreciationMethod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FADepreciationMethod.parentMethodID, Equal<P.AsInt>>>>>.And<BqlOperand<FADepreciationMethod.averagingConvPeriod, IBqlShort>.IsEqual<P.AsInt>>>>.Config>.Select(graph, new object[2]
    {
      (object) classMethod.MethodID,
      (object) nullable
    }));
  }

  public virtual void CheckAssetTransferInformation(
    FixedAsset asset,
    FADetails det,
    string transferPeriodId = null)
  {
    ((PXGraph) this).GetExtension<AssetMaint.AssetMaintFixedAssetChecksExtension>().CheckAssetTransferInformation(asset, det, transferPeriodId);
  }

  public class RedirectToSourceDocumentFromAssetMaintExtension : 
    RedirectToSourceDocumentExtensionBase<AssetMaint>
  {
  }

  public class AdditionsViewExtension : AdditionsViewExtensionBase<AssetMaint>
  {
    public PXSelect<FAAccrualTran> Additions;
    [PXFilterable(new System.Type[] {})]
    [PXCopyPasteHiddenView]
    public PXSelectJoin<DsplFAATran, LeftJoin<FAAccrualTran, On<DsplFAATran.tranID, Equal<FAAccrualTran.gLTranID>>>> DsplAdditions;
    public PXAction<FixedAsset> ProcessAdditions;

    public virtual void Initialize()
    {
      ((PXGraphExtension) this).Initialize();
      ((PXSelectBase) this.DsplAdditions).Cache.AllowInsert = false;
    }

    public virtual IEnumerable additions()
    {
      FixedAsset current1 = ((PXSelectBase<FixedAsset>) this.Base.Asset).Current;
      int? assetId;
      int num1;
      if (current1 == null)
      {
        num1 = 1;
      }
      else
      {
        assetId = current1.AssetID;
        num1 = !assetId.HasValue ? 1 : 0;
      }
      if (num1 == 0)
      {
        FixedAsset current2 = ((PXSelectBase<FixedAsset>) this.Base.Asset).Current;
        int num2;
        if (current2 == null)
        {
          num2 = 0;
        }
        else
        {
          assetId = current2.AssetID;
          int num3 = 0;
          num2 = assetId.GetValueOrDefault() < num3 & assetId.HasValue ? 1 : 0;
        }
        if (num2 == 0)
          return this.GetFAAccrualTransactions(((PXSelectBase<GLTranFilter>) this.Base.GLTrnFilter).Current, ((PXSelectBase) this.Additions).Cache);
      }
      return (IEnumerable) new List<FAAccrualTran>();
    }

    public virtual IEnumerable dspladditions()
    {
      ((PXSelectBase) this.Additions).View.Clear();
      int startRow = PXView.StartRow;
      int num = 0;
      foreach (FAAccrualTran faAccrualTran in ((PXSelectBase) this.Additions).View.Select(PXView.Currents, (object[]) null, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num))
      {
        DsplFAATran dsplFaaTran = new DsplFAATran()
        {
          TranID = faAccrualTran.TranID,
          GLTranQty = faAccrualTran.GLTranQty,
          GLTranAmt = faAccrualTran.GLTranAmt,
          ClosedQty = faAccrualTran.ClosedQty,
          ClosedAmt = faAccrualTran.ClosedAmt
        };
        ((PXSelectBase) this.DsplAdditions).Cache.IsDirty = (((PXSelectBase) this.DsplAdditions).Cache.GetStatus((object) dsplFaaTran) != null || ((PXSelectBase<DsplFAATran>) this.DsplAdditions).Insert(dsplFaaTran) == null) && ((PXSelectBase) this.DsplAdditions).Cache.IsDirty;
        yield return (object) new PXResult<DsplFAATran, FAAccrualTran>(((PXSelectBase) this.DsplAdditions).Cache.Locate((object) dsplFaaTran) as DsplFAATran, faAccrualTran);
      }
      PXView.StartRow = 0;
    }

    [PXUIField]
    [PXProcessButton]
    public virtual IEnumerable processAdditions(PXAdapter adapter)
    {
      FixedAsset current1 = ((PXSelectBase<FixedAsset>) this.Base.CurrentAsset).Current;
      FADetails faDetails = ((PXSelectBase<FADetails>) this.Base.AssetDetails).Current ?? PXResultset<FADetails>.op_Implicit(((PXSelectBase<FADetails>) this.Base.AssetDetails).Select(Array.Empty<object>()));
      GLTranFilter current2 = ((PXSelectBase<GLTranFilter>) this.Base.GLTrnFilter).Current;
      Numbering numbering = PXResultset<Numbering>.op_Implicit(((PXSelectBase<Numbering>) this.Base.assetNumbering).Select(Array.Empty<object>()));
      Decimal? currentCost = current2.CurrentCost;
      Decimal? expectedCost = current2.ExpectedCost;
      if (!(currentCost.GetValueOrDefault() == expectedCost.GetValueOrDefault() & currentCost.HasValue == expectedCost.HasValue))
      {
        PX.Objects.FA.AssetProcess.RestrictAdditonDeductionForCalcMethod((PXGraph) this.Base, current1.AssetID, "PC");
        PX.Objects.FA.AssetProcess.RestrictAdditonDeductionForCalcMethod((PXGraph) this.Base, current1.AssetID, "ZL");
        PX.Objects.FA.AssetProcess.RestrictAdditonDeductionForCalcMethod((PXGraph) this.Base, current1.AssetID, "LE");
      }
      Decimal num1 = current2.AccrualBalance.GetValueOrDefault();
      Decimal num2 = current2.CurrentCost.GetValueOrDefault();
      bool flag1 = PXAccess.FeatureInstalled<FeaturesSet.interBranch>();
      foreach (DsplFAATran dsplFaaTran in ((PXSelectBase) this.DsplAdditions).Cache.Inserted)
      {
        bool? nullable1 = dsplFaaTran.Selected;
        if (nullable1.GetValueOrDefault())
        {
          AssetGLTransactions.GLTran gltran = PXResultset<AssetGLTransactions.GLTran>.op_Implicit(PXSelectBase<AssetGLTransactions.GLTran, PXSelect<AssetGLTransactions.GLTran, Where<PX.Objects.GL.GLTran.tranID, Equal<Current<DsplFAATran.tranID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new DsplFAATran[1]
          {
            dsplFaaTran
          }, Array.Empty<object>()));
          bool flag2 = PXAccess.IsSameParentOrganization(gltran.BranchID, current1.BranchID);
          PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID((PXGraph) this.Base, PXAccess.GetParentOrganizationID(gltran.BranchID));
          int? nullable2;
          if (!flag1)
          {
            int? branchId = gltran.BranchID;
            nullable2 = current1.BranchID;
            if (!(branchId.GetValueOrDefault() == nullable2.GetValueOrDefault() & branchId.HasValue == nullable2.HasValue) && (!flag2 || flag2 && organizationById.OrganizationType != "NotBalancing"))
              throw new PXException("Inter-Branch Transactions feature is disabled.");
          }
          nullable1 = dsplFaaTran.Component;
          Decimal? nullable3;
          Decimal? nullable4;
          if (nullable1.GetValueOrDefault())
          {
            nullable1 = numbering.UserNumbering;
            if (nullable1.GetValueOrDefault())
              throw new PXException("Components cannot be created for the asset because manual numbering is activated for the {0} numbering sequence. Use the Convert Purchases to Assets (FA504500) form to create manually numbered components.", new object[1]
              {
                (object) numbering.NumberingID
              });
            FALocationHistory loc = PXResultset<FALocationHistory>.op_Implicit(((PXSelectBase<FALocationHistory>) this.Base.AssetLocation).Select(new object[2]
            {
              (object) current1.AssetID,
              (object) faDetails.LocationRevID
            }));
            if (((PXSelectBase) this.Base.AssetLocation).Cache.GetStatus((object) loc) != null)
              throw new PXException("Fixed asset must be saved");
            FixedAsset cls = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Current<DsplFAATran.classID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
            {
              (object) dsplFaaTran
            }, Array.Empty<object>()));
            if (cls == null)
              throw new PXException("'{0}' cannot be empty.", new object[1]
              {
                (object) typeof (DsplFAATran.classID).Name
              });
            AdditionsFATran instance = PXGraph.CreateInstance<AdditionsFATran>();
            ((PXGraph) instance).SelectTimeStamp();
            AssetMaint graph = this.Base;
            nullable3 = dsplFaaTran.SelectedAmt;
            nullable4 = dsplFaaTran.SelectedQty;
            Decimal valueOrDefault1 = (nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() / nullable4.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
            Decimal num3 = PXDBCurrencyAttribute.BaseRound((PXGraph) graph, valueOrDefault1);
            int num4 = 0;
            while (true)
            {
              Decimal num5 = (Decimal) num4;
              nullable3 = dsplFaaTran.SelectedQty;
              Decimal valueOrDefault2 = nullable3.GetValueOrDefault();
              if (num5 < valueOrDefault2 & nullable3.HasValue)
              {
                instance.InsertNewComponent(current1, cls, current2.TranDate, new Decimal?(num3), new Decimal?(1M), (IFALocation) loc, gltran);
                ++num4;
              }
              else
                break;
            }
            ((PXGraph) instance).Actions.PressSave();
          }
          else
          {
            if (((PXSelectBase<FARegister>) this.Base.Register).Current != null)
            {
              nullable1 = ((PXSelectBase<FARegister>) this.Base.Register).Current.Released;
              if (!nullable1.GetValueOrDefault())
                goto label_22;
            }
            PXSelect<FARegister> register = this.Base.Register;
            nullable2 = current1.BranchID;
            int BranchID = nullable2.Value;
            AssetGLTransactions.SetCurrentRegister(register, BranchID);
label_22:
            FATran faTran1 = new FATran();
            foreach (PXResult<FABookBalance> pxResult in PXSelectBase<FABookBalance, PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Current<FixedAsset.assetID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
            {
              FABookBalance bal = PXResult<FABookBalance>.op_Implicit(pxResult);
              FADepreciationMethod depreciationMethod = PXResultset<FADepreciationMethod>.op_Implicit(PXSelectBase<FADepreciationMethod, PXSelect<FADepreciationMethod, Where<FADepreciationMethod.methodID, Equal<Required<FADepreciationMethod.methodID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
              {
                (object) bal.DepreciationMethodID
              }));
              nullable1 = bal.Depreciate;
              if (nullable1.GetValueOrDefault())
              {
                nullable1 = ((PXSelectBase<FixedAsset>) this.Base.Asset).Current.UnderConstruction;
                bool flag3 = false;
                if (nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue && depreciationMethod == null)
                  throw new PXException("Depreciation method does not exist.");
              }
              if (!current2.TranDate.HasValue)
                throw new PXException("'{0}' cannot be empty.", new object[1]
                {
                  (object) PXUIFieldAttribute.GetDisplayName<GLTranFilter.tranDate>(((PXSelectBase) this.Base.GLTrnFilter).Cache)
                });
              nullable1 = bal.UpdateGL;
              if (nullable1.GetValueOrDefault() && string.IsNullOrEmpty(current2.PeriodID))
                throw new PXException("'{0}' cannot be empty.", new object[1]
                {
                  (object) PXUIFieldAttribute.GetDisplayName<GLTranFilter.periodID>(((PXSelectBase) this.Base.GLTrnFilter).Cache)
                });
              OrganizationFinPeriod periodInSubledger1 = this.Base.FinPeriodUtils.GetNearestOpenOrganizationFinPeriodInSubledger<OrganizationFinPeriod.fAClosed>(gltran.FinPeriodID, current1.BranchID, (Func<bool>) (() => bal.UpdateGL.GetValueOrDefault()));
              if (current2.ReconType == "+")
              {
                OrganizationFinPeriod periodInSubledger2 = this.Base.FABookPeriodUtils.GetNearestOpenOrganizationMappedFABookPeriodInSubledger<OrganizationFinPeriod.fAClosed>(bal.BookID, gltran.BranchID, gltran.FinPeriodID, current1.BranchID);
                FATran faTran2 = new FATran();
                faTran2.AssetID = current1.AssetID;
                faTran2.BookID = bal.BookID;
                faTran2.TranAmt = dsplFaaTran.SelectedAmt;
                faTran2.Qty = dsplFaaTran.SelectedQty;
                faTran2.TranDate = gltran.TranDate;
                faTran2.FinPeriodID = periodInSubledger2?.FinPeriodID;
                nullable1 = bal.UpdateGL;
                int? nullable5;
                if (!nullable1.GetValueOrDefault())
                {
                  nullable2 = new int?();
                  nullable5 = nullable2;
                }
                else
                  nullable5 = gltran.TranID;
                faTran2.GLTranID = nullable5;
                faTran2.TranType = "R+";
                faTran2.CreditAccountID = gltran.AccountID;
                faTran2.CreditSubID = gltran.SubID;
                faTran2.DebitAccountID = current1.FAAccrualAcctID;
                faTran2.DebitSubID = current1.FAAccrualSubID;
                faTran2.TranDesc = gltran.TranDesc;
                faTran1 = faTran2;
                faTran1 = ((PXSelectBase<FATran>) this.Base.FATransactions).Insert(faTran1);
                nullable4 = faTran1.TranAmt;
                Decimal num6 = num1;
                nullable3 = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + num6) : new Decimal?();
                Decimal num7 = num2;
                if (nullable3.GetValueOrDefault() > num7 & nullable3.HasValue)
                {
                  FATran faTran3 = new FATran();
                  faTran3.AssetID = current1.AssetID;
                  faTran3.BookID = bal.BookID;
                  nullable4 = faTran1.TranAmt;
                  Decimal num8 = num1;
                  nullable3 = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + num8) : new Decimal?();
                  Decimal num9 = num2;
                  Decimal? nullable6;
                  if (!nullable3.HasValue)
                  {
                    nullable4 = new Decimal?();
                    nullable6 = nullable4;
                  }
                  else
                    nullable6 = new Decimal?(nullable3.GetValueOrDefault() - num9);
                  faTran3.TranAmt = nullable6;
                  faTran3.TranDate = current2.TranDate;
                  nullable1 = bal.UpdateGL;
                  faTran3.FinPeriodID = nullable1.GetValueOrDefault() ? current2.PeriodID : (string) null;
                  faTran3.TranType = "P+";
                  faTran3.CreditAccountID = current1.FAAccrualAcctID;
                  faTran3.CreditSubID = current1.FAAccrualSubID;
                  faTran3.DebitAccountID = current1.FAAccountID;
                  faTran3.DebitSubID = current1.FASubID;
                  faTran3.TranDesc = gltran.TranDesc;
                  faTran1 = faTran3;
                  faTran1 = ((PXSelectBase<FATran>) this.Base.FATransactions).Insert(faTran1);
                  if (bal.Status == "F")
                  {
                    nullable1 = bal.Depreciate;
                    if (nullable1.GetValueOrDefault())
                    {
                      nullable1 = ((PXSelectBase<FixedAsset>) this.Base.Asset).Current.UnderConstruction;
                      if (!nullable1.GetValueOrDefault() && depreciationMethod.IsPureStraightLine)
                      {
                        faTran1 = new FATran()
                        {
                          AssetID = current1.AssetID,
                          BookID = bal.BookID,
                          TranAmt = faTran1.TranAmt,
                          TranDate = current2.TranDate,
                          FinPeriodID = current2.PeriodID,
                          TranType = "C+",
                          CreditAccountID = current1.AccumulatedDepreciationAccountID,
                          CreditSubID = current1.AccumulatedDepreciationSubID,
                          DebitAccountID = current1.DepreciatedExpenseAccountID,
                          DebitSubID = current1.DepreciatedExpenseSubID,
                          TranDesc = gltran.TranDesc
                        };
                        faTran1 = ((PXSelectBase<FATran>) this.Base.FATransactions).Insert(faTran1);
                      }
                    }
                  }
                }
              }
              else
              {
                FATran faTran4 = new FATran();
                faTran4.AssetID = current1.AssetID;
                faTran4.BookID = bal.BookID;
                faTran4.TranAmt = dsplFaaTran.SelectedAmt;
                faTran4.Qty = dsplFaaTran.SelectedQty;
                faTran4.TranDate = gltran.TranDate;
                faTran4.FinPeriodID = periodInSubledger1 != null ? periodInSubledger1.FinPeriodID : gltran.FinPeriodID;
                nullable1 = bal.UpdateGL;
                int? nullable7;
                if (!nullable1.GetValueOrDefault())
                {
                  nullable2 = new int?();
                  nullable7 = nullable2;
                }
                else
                  nullable7 = gltran.TranID;
                faTran4.GLTranID = nullable7;
                faTran4.TranType = "R-";
                faTran4.DebitAccountID = gltran.AccountID;
                faTran4.DebitSubID = gltran.SubID;
                faTran4.CreditAccountID = current1.FAAccrualAcctID;
                faTran4.CreditSubID = current1.FAAccrualSubID;
                faTran4.TranDesc = gltran.TranDesc;
                faTran1 = faTran4;
                ((PXSelectBase<FATran>) this.Base.FATransactions).Insert(faTran1);
                FATran faTran5 = new FATran();
                faTran5.AssetID = current1.AssetID;
                faTran5.BookID = bal.BookID;
                faTran5.TranAmt = dsplFaaTran.SelectedAmt;
                faTran5.TranDate = current2.TranDate;
                nullable1 = bal.UpdateGL;
                faTran5.FinPeriodID = nullable1.GetValueOrDefault() ? current2.PeriodID : (string) null;
                faTran5.TranType = "P-";
                faTran5.DebitAccountID = current1.FAAccrualAcctID;
                faTran5.DebitSubID = current1.FAAccrualSubID;
                faTran5.CreditAccountID = current1.FAAccountID;
                faTran5.CreditSubID = current1.FASubID;
                faTran5.TranDesc = gltran.TranDesc;
                faTran1 = faTran5;
                faTran1 = ((PXSelectBase<FATran>) this.Base.FATransactions).Insert(faTran1);
                if (bal.Status == "F")
                {
                  nullable1 = bal.Depreciate;
                  if (nullable1.GetValueOrDefault())
                  {
                    nullable1 = ((PXSelectBase<FixedAsset>) this.Base.Asset).Current.UnderConstruction;
                    if (!nullable1.GetValueOrDefault() && depreciationMethod.IsPureStraightLine)
                    {
                      faTran1 = new FATran()
                      {
                        AssetID = current1.AssetID,
                        BookID = bal.BookID,
                        TranAmt = faTran1.TranAmt,
                        TranDate = current2.TranDate,
                        FinPeriodID = current2.PeriodID,
                        TranType = "C-",
                        CreditAccountID = current1.DepreciatedExpenseAccountID,
                        CreditSubID = current1.DepreciatedExpenseSubID,
                        DebitAccountID = current1.AccumulatedDepreciationAccountID,
                        DebitSubID = current1.AccumulatedDepreciationSubID,
                        TranDesc = gltran.TranDesc
                      };
                      faTran1 = ((PXSelectBase<FATran>) this.Base.FATransactions).Insert(faTran1);
                    }
                  }
                }
              }
            }
            Decimal num10 = num1;
            nullable3 = dsplFaaTran.SelectedAmt;
            Decimal valueOrDefault = nullable3.GetValueOrDefault();
            num1 = num10 + valueOrDefault;
            Decimal num11 = num2;
            Decimal num12;
            if (!(faTran1.TranType == "P+"))
            {
              num12 = 0M;
            }
            else
            {
              nullable3 = faTran1.TranAmt;
              num12 = nullable3.GetValueOrDefault();
            }
            num2 = num11 + num12;
          }
        }
      }
      ((PXSelectBase) this.DsplAdditions).Cache.Clear();
      return adapter.Get();
    }

    protected virtual void FATran_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
    {
      ((PXSelectBase) this.DsplAdditions).Cache.Clear();
    }

    protected virtual void GLTranFilter_AccountID_FieldUpdated(
      PXCache sender,
      PXFieldUpdatedEventArgs e)
    {
      ((PXSelectBase) this.DsplAdditions).Cache.Clear();
    }

    protected virtual void GLTranFilter_SubID_FieldUpdated(
      PXCache sender,
      PXFieldUpdatedEventArgs e)
    {
      ((PXSelectBase) this.DsplAdditions).Cache.Clear();
    }

    protected virtual void GLTranFilter_ReconType_FieldUpdated(
      PXCache sender,
      PXFieldUpdatedEventArgs e)
    {
      ((PXSelectBase) this.DsplAdditions).Cache.Clear();
    }

    protected virtual void GLTranFilter_ShowReconciled_FieldUpdated(
      PXCache sender,
      PXFieldUpdatedEventArgs e)
    {
      ((PXSelectBase) this.DsplAdditions).Cache.Clear();
    }

    protected virtual void GLTranFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
    {
      GLTranFilter row = (GLTranFilter) e.Row;
      FixedAsset current = ((PXSelectBase<FixedAsset>) this.Base.CurrentAsset).Current;
      FATran faTran1 = PXResultset<FATran>.op_Implicit(PXSelectBase<FATran, PXSelect<FATran, Where<FATran.assetID, Equal<Current<FixedAsset.assetID>>, And<FATran.released, Equal<False>, And<Where<FATran.tranType, Equal<FATran.tranType.purchasingPlus>, Or<FATran.tranType, Equal<FATran.tranType.purchasingMinus>>>>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
      FATran faTran2 = PXResultset<FATran>.op_Implicit(PXSelectBase<FATran, PXSelect<FATran, Where<FATran.assetID, Equal<Current<FixedAsset.assetID>>, And<FATran.released, Equal<False>, And<Where<FATran.tranType, Equal<FATran.tranType.reconcilliationPlus>, Or<FATran.tranType, Equal<FATran.tranType.reconcilliationMinus>>>>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
      bool flag = true;
      try
      {
        AccountAttribute.VerifyAccountIsNotControl((PX.Objects.GL.Account) PXSelectorAttribute.Select<GLTranFilter.accountID>(sender, e.Row));
      }
      catch (PXSetPropertyException ex)
      {
        flag = ex.ErrorLevel < 4;
      }
      PXAction<FixedAsset> processAdditions = this.ProcessAdditions;
      int num1;
      if (((PXSelectBase) this.Base.CurrentAsset).Cache.GetStatus((object) current) != 2 && faTran1 == null && faTran2 == null)
      {
        Decimal? unreconciledAmt = row.UnreconciledAmt;
        Decimal num2 = 0M;
        num1 = unreconciledAmt.GetValueOrDefault() >= num2 & unreconciledAmt.HasValue ? 1 : 0;
      }
      else
        num1 = 0;
      int num3 = flag ? 1 : 0;
      int num4 = num1 & num3;
      ((PXAction) processAdditions).SetEnabled(num4 != 0);
    }

    protected virtual void DsplFAATran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
    {
      DsplFAATran row = (DsplFAATran) e.Row;
      if (row == null)
        return;
      PXCache pxCache1 = sender;
      DsplFAATran dsplFaaTran1 = row;
      bool? nullable = row.Selected;
      int num1 = !nullable.GetValueOrDefault() ? 0 : (((PXSelectBase<GLTranFilter>) this.Base.GLTrnFilter).Current.ReconType == "+" ? 1 : 0);
      PXUIFieldAttribute.SetEnabled<DsplFAATran.component>(pxCache1, (object) dsplFaaTran1, num1 != 0);
      PXCache pxCache2 = sender;
      DsplFAATran dsplFaaTran2 = row;
      nullable = row.Component;
      int num2 = nullable.GetValueOrDefault() ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<DsplFAATran.classID>(pxCache2, (object) dsplFaaTran2, num2 != 0);
      PXUIFieldAttribute.SetVisible<PX.Objects.GL.GLTran.inventoryID>(((PXGraph) this.Base).Caches[typeof (PX.Objects.GL.GLTran)], (object) null, true);
    }

    protected virtual void DsplFAATran_ClassID_FieldVerifying(
      PXCache sender,
      PXFieldVerifyingEventArgs e)
    {
      DsplFAATran row = (DsplFAATran) e.Row;
      if (row != null && row.Component.GetValueOrDefault() && e.NewValue == null)
        throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) "[classID]"
        });
    }

    protected virtual void DsplFAATran_SelectedAmt_FieldUpdated(
      PXCache sender,
      PXFieldUpdatedEventArgs e)
    {
      DsplFAATran row = (DsplFAATran) e.Row;
      Decimal? selectedAmt = row.SelectedAmt;
      Decimal num = 0M;
      if (!(selectedAmt.GetValueOrDefault() > num & selectedAmt.HasValue))
        return;
      row.Selected = new bool?(true);
    }

    protected virtual void DsplFAATran_SelectedQty_FieldUpdated(
      PXCache sender,
      PXFieldUpdatedEventArgs e)
    {
      DsplFAATran row = (DsplFAATran) e.Row;
      Decimal? selectedQty = row.SelectedQty;
      Decimal num = 0M;
      if (!(selectedQty.GetValueOrDefault() > num & selectedQty.HasValue))
        return;
      row.Selected = new bool?(true);
    }

    protected virtual void DsplFAATran_Reconciled_FieldUpdated(
      PXCache sender,
      PXFieldUpdatedEventArgs e)
    {
      DsplFAATran row = (DsplFAATran) e.Row;
      if (row == null)
        return;
      FAAccrualTran faAccrualTran = PXResultset<FAAccrualTran>.op_Implicit(PXSelectBase<FAAccrualTran, PXSelect<FAAccrualTran, Where<FAAccrualTran.tranID, Equal<Current<DsplFAATran.tranID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
      {
        (object) row
      }, Array.Empty<object>()));
      faAccrualTran.Reconciled = row.Reconciled;
      if (((PXSelectBase) this.Additions).Cache.GetStatus((object) faAccrualTran) != null)
        return;
      ((PXSelectBase) this.Additions).Cache.SetStatus((object) faAccrualTran, (PXEntryStatus) 1);
      ((PXSelectBase) this.Additions).Cache.IsDirty = true;
    }

    protected virtual void DsplFAATran_SelectedAmt_FieldVerifying(
      PXCache sender,
      PXFieldVerifyingEventArgs e)
    {
      DsplFAATran row = (DsplFAATran) e.Row;
      if (row == null || !row.Selected.GetValueOrDefault())
        return;
      Decimal? nullable1 = (Decimal?) e.NewValue;
      Decimal num = 0M;
      if (nullable1.GetValueOrDefault() <= num & nullable1.HasValue)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", (PXErrorLevel) 0);
      nullable1 = (Decimal?) e.NewValue;
      Decimal? glTranAmt = row.GLTranAmt;
      Decimal? nullable2 = row.ClosedAmt;
      Decimal? nullable3 = glTranAmt.HasValue & nullable2.HasValue ? new Decimal?(glTranAmt.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      if (nullable1.GetValueOrDefault() > nullable3.GetValueOrDefault() & nullable1.HasValue & nullable3.HasValue)
      {
        object[] objArray = new object[1];
        nullable3 = row.GLTranAmt;
        nullable1 = row.ClosedAmt;
        Decimal? nullable4;
        if (!(nullable3.HasValue & nullable1.HasValue))
        {
          nullable2 = new Decimal?();
          nullable4 = nullable2;
        }
        else
          nullable4 = new Decimal?(nullable3.GetValueOrDefault() - nullable1.GetValueOrDefault());
        objArray[0] = (object) nullable4;
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", objArray);
      }
    }

    protected virtual void DsplFAATran_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
    {
      DsplFAATran row = (DsplFAATran) e.Row;
      if (row == null)
        return;
      GLTranFilter current = ((PXSelectBase<GLTranFilter>) this.Base.GLTrnFilter).Current;
      Decimal num1 = 0M;
      Decimal num2 = current.CurrentCost.GetValueOrDefault();
      Decimal? nullable = current.AccrualBalance;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      foreach (DsplFAATran dsplFaaTran in ((PXSelectBase) this.DsplAdditions).Cache.Inserted)
      {
        Decimal num3 = num1;
        nullable = dsplFaaTran.SelectedAmt;
        Decimal valueOrDefault2 = nullable.GetValueOrDefault();
        num1 = num3 + valueOrDefault2;
      }
      Decimal num4;
      if (current.ReconType == "+")
      {
        num4 = valueOrDefault1 + num1;
        Decimal num5 = num4;
        Decimal? currentCost = current.CurrentCost;
        Decimal valueOrDefault3 = currentCost.GetValueOrDefault();
        if (num5 > valueOrDefault3 & currentCost.HasValue)
          num2 = num4;
      }
      else
      {
        num4 = valueOrDefault1 - num1;
        num2 = num4;
      }
      current.SelectionAmt = new Decimal?(num1);
      current.ExpectedAccrualBal = new Decimal?(num4);
      current.ExpectedCost = new Decimal?(num2);
      try
      {
        object classId = (object) row.ClassID;
        sender.RaiseFieldVerifying<DsplFAATran.classID>((object) row, ref classId);
      }
      catch (PXSetPropertyException ex)
      {
        sender.RaiseExceptionHandling<DsplFAATran.classID>((object) row, (object) row.ClassID, (Exception) ex);
      }
      try
      {
        object selectedAmt = (object) row.SelectedAmt;
        sender.RaiseFieldVerifying<DsplFAATran.selectedAmt>((object) row, ref selectedAmt);
      }
      catch (PXSetPropertyException ex)
      {
        sender.RaiseExceptionHandling<DsplFAATran.selectedAmt>((object) row, (object) row.SelectedAmt, (Exception) ex);
      }
    }

    protected virtual void DsplFAATran_Component_FieldUpdated(
      PXCache sender,
      PXFieldUpdatedEventArgs e)
    {
      DsplFAATran row = (DsplFAATran) e.Row;
      if (row == null)
        return;
      bool? nullable1 = row.Component;
      if (nullable1.GetValueOrDefault())
        return;
      row.ClassID = new int?();
      object selectedQty1 = (object) row.SelectedQty;
      DsplFAATran dsplFaaTran = row;
      nullable1 = row.Selected;
      Decimal? nullable2;
      Decimal num;
      if (!nullable1.GetValueOrDefault())
      {
        num = 0M;
      }
      else
      {
        nullable2 = ((PXSelectBase<FixedAsset>) this.Base.Asset).Current.Qty;
        Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
        nullable2 = row.OpenQty;
        Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
        num = Math.Min(valueOrDefault1, valueOrDefault2);
      }
      Decimal? nullable3 = new Decimal?(num);
      dsplFaaTran.SelectedQty = nullable3;
      nullable2 = (Decimal?) selectedQty1;
      Decimal? selectedQty2 = row.SelectedQty;
      if (nullable2.GetValueOrDefault() == selectedQty2.GetValueOrDefault() & nullable2.HasValue == selectedQty2.HasValue)
        return;
      sender.RaiseFieldUpdated<DsplFAATran.selectedQty>((object) row, selectedQty1);
    }

    protected virtual void DsplFAATran_Selected_FieldUpdated(
      PXCache sender,
      PXFieldUpdatedEventArgs e)
    {
      DsplFAATran row = (DsplFAATran) e.Row;
      if (row == null)
        return;
      GLTranFilter current = ((PXSelectBase<GLTranFilter>) this.Base.GLTrnFilter).Current;
      object selectedQty = (object) row.SelectedQty;
      DsplFAATran dsplFaaTran1 = row;
      Decimal? nullable1;
      Decimal num1;
      if (!row.Selected.GetValueOrDefault())
      {
        num1 = 0M;
      }
      else
      {
        nullable1 = ((PXSelectBase<FixedAsset>) this.Base.Asset).Current.Qty;
        Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
        nullable1 = row.OpenQty;
        Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
        num1 = Math.Min(valueOrDefault1, valueOrDefault2);
      }
      Decimal? nullable2 = new Decimal?(num1);
      dsplFaaTran1.SelectedQty = nullable2;
      nullable1 = (Decimal?) selectedQty;
      Decimal? nullable3 = row.SelectedQty;
      if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
        sender.RaiseFieldUpdated<DsplFAATran.selectedQty>((object) row, selectedQty);
      object selectedAmt = (object) row.SelectedAmt;
      DsplFAATran dsplFaaTran2 = row;
      bool? selected = row.Selected;
      Decimal num2;
      if (!selected.GetValueOrDefault())
      {
        num2 = 0M;
      }
      else
      {
        nullable3 = current.ExpectedCost;
        Decimal valueOrDefault3 = nullable3.GetValueOrDefault();
        nullable3 = current.ExpectedAccrualBal;
        Decimal valueOrDefault4 = nullable3.GetValueOrDefault();
        Decimal val1 = valueOrDefault3 - valueOrDefault4;
        nullable3 = row.OpenAmt;
        Decimal valueOrDefault5 = nullable3.GetValueOrDefault();
        num2 = Math.Min(val1, valueOrDefault5);
      }
      Decimal? nullable4 = new Decimal?(num2);
      dsplFaaTran2.SelectedAmt = nullable4;
      selected = row.Selected;
      if (selected.GetValueOrDefault())
      {
        nullable3 = current.UnreconciledAmt;
        Decimal num3 = 0M;
        if (nullable3.GetValueOrDefault() == num3 & nullable3.HasValue)
          row.SelectedAmt = row.OpenAmt;
      }
      nullable3 = (Decimal?) selectedAmt;
      nullable1 = row.SelectedAmt;
      if (nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue)
        return;
      sender.RaiseFieldUpdated<DsplFAATran.selectedAmt>((object) row, selectedAmt);
    }

    protected virtual void DsplFAATran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
    {
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  public class AssetMaintFixedAssetChecksExtension : FixedAssetChecksExtensionBase<AssetMaint>
  {
  }

  public class SplitOperationScope : FlaggedModeScopeBase<AssetMaint.SplitOperationScope>
  {
  }
}
