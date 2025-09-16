// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.TransactionEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.CS;
using PX.Objects.FA.Overrides.AssetProcess;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.FA;

[TableAndChartDashboardType]
public class TransactionEntry : PXGraph<
#nullable disable
TransactionEntry, FARegister>
{
  public PXSelect<FARegister> Document;
  public PXSelect<FAAccrualTran> Additions;
  public PXSelect<FATran, Where<FATran.refNbr, Equal<Current<FARegister.refNbr>>>> Trans;
  public PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Optional<FATran.assetID>>>> Asset;
  public PXSelect<FADetails, Where<FADetails.assetID, Equal<Optional<FATran.assetID>>>> assetdetails;
  public PXSelect<FABookBalance, Where<FABookBalance.assetID, Equal<Required<FABookBalance.assetID>>, And<FABookBalance.bookID, Equal<Required<FABookBalance.bookID>>>>> bookbalances;
  public PXSelect<FABookHist> bookhistory;
  public PXSelect<FALocationHistory, Where<FALocationHistory.assetID, Equal<Optional<FATran.assetID>>>, OrderBy<Desc<FALocationHistory.revisionID>>> locationHistory;
  public PXSetup<FASetup> fasetup;
  public FbqlSelect<SelectFromBase<FATran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  FATran.tranType, 
  #nullable disable
  Equal<FATran.tranType.transferPurchasing>>>>>.And<BqlOperand<
  #nullable enable
  FATran.refNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  FARegister.refNbr, IBqlString>.FromCurrent>>>, 
  #nullable disable
  FATran>.View TransferPurchasingTrans;
  private readonly DocumentList<FARegister> _created;
  private readonly Dictionary<string, bool> _debit_enable = new Dictionary<string, bool>()
  {
    {
      "P+",
      false
    },
    {
      "P-",
      true
    },
    {
      "D+",
      true
    },
    {
      "D-",
      false
    },
    {
      "C+",
      true
    },
    {
      "C-",
      false
    },
    {
      "S+",
      true
    },
    {
      "S-",
      false
    }
  };
  protected bool IsDefaulting;
  public PXInitializeState<FARegister> initializeState;
  public PXWorkflowEventHandler<FARegister> OnUpdateStatus;
  public PXWorkflowEventHandler<FARegister> OnReleaseDocument;
  public PXAction<FARegister> putOnHold;
  public PXAction<FARegister> releaseFromHold;
  public PXAction<FARegister> release;
  public PXAction<FARegister> viewBatch;
  public PXAction<FARegister> viewAsset;
  public PXAction<FARegister> viewBook;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  [InjectDependency]
  public IFABookPeriodRepository FABookPeriodRepository { get; set; }

  [PXMergeAttributes]
  [PXDefault("P+")]
  protected virtual void _(PX.Data.Events.CacheAttached<FATran.tranType> e)
  {
  }

  [SubAccount(typeof (FATran.creditAccountID), typeof (FATran.branchID), false)]
  protected virtual void FATran_CreditSubID_CacheAttached(PXCache sender)
  {
  }

  [SubAccount(typeof (FATran.debitAccountID), typeof (FATran.branchID), false)]
  protected virtual void FATran_DebitSubID_CacheAttached(PXCache sender)
  {
  }

  [PXDBScalar(typeof (Search<FixedAsset.depreciable, Where<FixedAsset.assetID, Equal<FATran.assetID>>>))]
  protected virtual void FATran_Depreciable_CacheAttached(PXCache sender)
  {
  }

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
  [PXRestrictor(typeof (Where<FixedAsset.status, NotEqual<FixedAssetStatus.disposed>, And<FixedAsset.status, NotEqual<FixedAssetStatus.reversed>>>), "Disposed and reversed fixed assets cannot be used on this form.", new Type[] {})]
  protected virtual void _(PX.Data.Events.CacheAttached<FATran.assetID> e)
  {
  }

  public DocumentList<FARegister> created => this._created;

  public bool UpdateGL
  {
    get => ((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault();
  }

  public TransactionEntry()
  {
    FASetup current = ((PXSelectBase<FASetup>) this.fasetup).Current;
    this._created = (DocumentList<FARegister>) new BatchDocumentList<FARegister, FARegister.lineCntr>((PXGraph) this);
  }

  public static void SegregateRegister(
    PXGraph graph,
    int BranchID,
    string Origin,
    string PeriodID,
    DateTime? DocDate,
    string descr,
    DocumentList<FARegister> created)
  {
    PXCache cach = graph.Caches[typeof (FARegister)];
    if (graph.Caches[typeof (FATran)].IsInsertedUpdatedDeleted)
    {
      graph.Actions.PressSave();
      if (cach.Current != null && created.Find(cach.Current) == null)
        created.Add((FARegister) cach.Current);
      graph.Clear();
    }
    FARegister faRegister1 = created.Find<FARegister.branchID, FARegister.origin, FARegister.finPeriodID>((object) BranchID, (object) Origin, (object) PeriodID) ?? new FARegister();
    if (faRegister1.RefNbr != null)
    {
      if (!WebConfig.ParallelProcessingDisabled)
      {
        int? lineCntr = faRegister1.LineCntr;
        int num = 500;
        if (!(lineCntr.GetValueOrDefault() <= num & lineCntr.HasValue))
          goto label_10;
      }
      FARegister faRegister2 = faRegister1;
      faRegister2.TranAmt = faRegister1.TranAmt;
      PXCache<FARegister>.StoreOriginal(graph, faRegister2);
      if (faRegister2.DocDesc != descr)
      {
        faRegister2.DocDesc = string.Empty;
        cach.Update((object) faRegister2);
      }
      cach.Current = (object) faRegister2;
      return;
    }
label_10:
    if (graph.IsDirty)
    {
      graph.Actions.PressSave();
      graph.Clear();
    }
    FARegister faRegister3 = new FARegister()
    {
      Hold = new bool?(false),
      BranchID = new int?(BranchID),
      Origin = Origin,
      FinPeriodID = PeriodID,
      DocDate = DocDate,
      DocDesc = descr
    };
    cach.Insert((object) faRegister3);
  }

  public virtual void Persist()
  {
    ((PXGraph) this).Persist();
    if (((PXSelectBase<FARegister>) this.Document).Current == null)
      return;
    FARegister faRegister = this.created.Find((object) ((PXSelectBase<FARegister>) this.Document).Current);
    if (faRegister == null)
      this.created.Add(((PXSelectBase<FARegister>) this.Document).Current);
    else
      ((PXSelectBase) this.Document).Cache.RestoreCopy((object) faRegister, (object) ((PXSelectBase<FARegister>) this.Document).Current);
  }

  protected virtual void DefaultingAccSub(
    PXFieldDefaultingEventArgs e,
    Dictionary<string, int?> accs)
  {
    FATran row = (FATran) e.Row;
    if (row == null || !row.AssetID.HasValue)
      return;
    if (row.TranType == null)
      return;
    try
    {
      e.NewValue = (object) accs[row.TranType];
      ((CancelEventArgs) e).Cancel = true;
    }
    catch (KeyNotFoundException ex)
    {
    }
  }

  protected virtual void DefaultingAllAccounts(PXCache sender, FATran trn)
  {
    sender.SetDefaultExt<FATran.debitAccountID>((object) trn);
    sender.SetDefaultExt<FATran.debitSubID>((object) trn);
    sender.SetDefaultExt<FATran.creditAccountID>((object) trn);
    sender.SetDefaultExt<FATran.creditSubID>((object) trn);
  }

  protected virtual void SetCurrentAsset(FATran trn)
  {
    int? assetId1;
    if (((PXSelectBase<FixedAsset>) this.Asset).Current != null)
    {
      assetId1 = ((PXSelectBase<FixedAsset>) this.Asset).Current.AssetID;
      int? assetId2 = trn.AssetID;
      if (assetId1.GetValueOrDefault() == assetId2.GetValueOrDefault() & assetId1.HasValue == assetId2.HasValue)
        goto label_3;
    }
    ((PXSelectBase<FixedAsset>) this.Asset).Current = ((PXSelectBase<FixedAsset>) this.Asset).SelectSingle(new object[1]
    {
      (object) trn.AssetID
    });
label_3:
    if (((PXSelectBase<FADetails>) this.assetdetails).Current != null)
    {
      int? assetId3 = ((PXSelectBase<FADetails>) this.assetdetails).Current.AssetID;
      assetId1 = trn.AssetID;
      if (assetId3.GetValueOrDefault() == assetId1.GetValueOrDefault() & assetId3.HasValue == assetId1.HasValue)
        return;
    }
    ((PXSelectBase<FADetails>) this.assetdetails).Current = ((PXSelectBase<FADetails>) this.assetdetails).SelectSingle(new object[1]
    {
      (object) trn.AssetID
    });
  }

  protected virtual void ToggleAccounts(PXCache sender, FATran trn)
  {
    if (trn == null || trn.TranType == null)
      return;
    bool flag;
    if (this._debit_enable.TryGetValue(trn.TranType, out flag))
    {
      PXUIFieldAttribute.SetEnabled<FATran.creditAccountID>(sender, (object) trn, !flag);
      PXUIFieldAttribute.SetEnabled<FATran.creditSubID>(sender, (object) trn, !flag);
      PXUIFieldAttribute.SetEnabled<FATran.debitAccountID>(sender, (object) trn, flag);
      PXUIFieldAttribute.SetEnabled<FATran.debitSubID>(sender, (object) trn, flag);
    }
    if (!(trn.TranType == "P+"))
      return;
    PXUIFieldAttribute.SetEnabled<FATran.creditAccountID>(sender, (object) trn, false);
    PXUIFieldAttribute.SetEnabled<FATran.creditSubID>(sender, (object) trn, false);
    PXUIFieldAttribute.SetEnabled<FATran.debitAccountID>(sender, (object) trn, false);
    PXUIFieldAttribute.SetEnabled<FATran.debitSubID>(sender, (object) trn, false);
  }

  public virtual void CheckIfAssetCanBeDisposed(
    FixedAsset asset,
    FADetails det,
    FASetup fasetup,
    DateTime disposalDate,
    string disposalPeriodID,
    bool deprBeforeDisposal)
  {
    ((PXGraph) this).GetExtension<TransactionEntry.TransactionEntryFixedAssetChecksExtension>().CheckIfAssetCanBeDisposed(asset, det, fasetup, disposalDate, disposalPeriodID, deprBeforeDisposal);
  }

  protected virtual void FATran_RefNbr_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void FATran_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    FATran row = (FATran) e.Row;
    PXRowInsertingEventArgs insertingEventArgs = e;
    int num1 = ((CancelEventArgs) insertingEventArgs).Cancel ? 1 : 0;
    Decimal? tranAmt = row.TranAmt;
    Decimal num2 = 0M;
    int num3 = !(tranAmt.GetValueOrDefault() == num2 & tranAmt.HasValue) ? 0 : (!string.IsNullOrEmpty(row.MethodDesc) ? 1 : (!(row.TranType != "C+") || !(row.TranType != "P+") || !(row.TranType != "P-") || !(row.TranType != "S+") || !(row.TranType != "S-") ? 0 : (row.Origin != "A" ? 1 : 0)));
    ((CancelEventArgs) insertingEventArgs).Cancel = (num1 | num3) != 0;
    this.SetCurrentAsset(row);
    row.Depreciable = (bool?) ((PXSelectBase<FixedAsset>) this.Asset).Current?.Depreciable;
  }

  protected virtual void FATran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    FATran row = (FATran) e.Row;
    if (row == null)
      return;
    if (sender.AllowUpdate && row.Origin != "A")
    {
      PXUIFieldAttribute.SetEnabled(sender, e.Row, false);
      PXUIFieldAttribute.SetEnabled<FATran.tranDesc>(sender, e.Row, true);
    }
    this.ToggleAccounts(sender, row);
    FARegister current = ((PXSelectBase<FARegister>) this.Document).Current;
    if (current == null)
      return;
    bool? depreciable = row.Depreciable;
    if (!depreciable.HasValue)
      return;
    if (current.Origin == "A")
    {
      depreciable = row.Depreciable;
      if (depreciable.GetValueOrDefault())
        PXStringListAttribute.SetList<FATran.tranType>(((PXSelectBase) this.Trans).Cache, (object) row, new FATran.tranType.AdjustmentListAttribute().AllowedValues, new FATran.tranType.AdjustmentListAttribute().AllowedLabels);
      else
        PXStringListAttribute.SetList<FATran.tranType>(((PXSelectBase) this.Trans).Cache, (object) row, new FATran.tranType.NonDepreciableListAttribute().AllowedValues, new FATran.tranType.NonDepreciableListAttribute().AllowedLabels);
    }
    ((PXSelectBase) this.Trans).Cache.AllowInsert = current.Origin == "A";
  }

  protected virtual void _(PX.Data.Events.RowDeleted<FARegister> e)
  {
    FARegister row = e.Row;
    if (row == null || !(row.Origin == "T") || row.Released.GetValueOrDefault())
      return;
    foreach (PXResult<FATran> pxResult in ((PXSelectBase<FATran>) this.TransferPurchasingTrans).Select(Array.Empty<object>()))
      this.RestorePreviousLocation(PXResult<FATran>.op_Implicit(pxResult));
  }

  private void RestorePreviousLocation(FATran tran)
  {
    this.SetCurrentAsset(tran);
    FixedAsset copy1 = PXCache<FixedAsset>.CreateCopy(((PXSelectBase<FixedAsset>) this.Asset).Current);
    FADetails copy2 = PXCache<FADetails>.CreateCopy(((PXSelectBase<FADetails>) this.assetdetails).Current);
    ((PXSelectBase<FALocationHistory>) this.locationHistory).Delete(((PXSelectBase<FALocationHistory>) this.locationHistory).SelectSingle(new object[1]
    {
      (object) tran.AssetID
    }));
    FALocationHistory faLocationHistory = ((PXSelectBase<FALocationHistory>) this.locationHistory).SelectSingle(new object[1]
    {
      (object) tran.AssetID
    });
    FAClass faClass = faLocationHistory != null ? PXResultset<FAClass>.op_Implicit(PXSelectBase<FAClass, PXViewOf<FAClass>.BasedOn<SelectFromBase<FAClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FAClass.assetID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) faLocationHistory.ClassID
    })) : throw new PXException("Previous location of Fixed Asset '{0}' is not found for restoring.", new object[1]
    {
      (object) copy1.AssetCD
    });
    copy2.LocationRevID = faLocationHistory.RevisionID;
    ((PXSelectBase<FADetails>) this.assetdetails).Update(copy2);
    FixedAsset fixedAsset1 = copy1;
    int? nullable1 = faLocationHistory.ClassID;
    int? nullable2 = nullable1 ?? copy1.ClassID;
    fixedAsset1.ClassID = nullable2;
    copy1.UnderConstruction = faClass.UnderConstruction ?? copy1.UnderConstruction;
    FixedAsset fixedAsset2 = copy1;
    nullable1 = faLocationHistory.BranchID;
    int? nullable3 = nullable1 ?? copy1.BranchID;
    fixedAsset2.BranchID = nullable3;
    FixedAsset fixedAsset3 = copy1;
    nullable1 = faLocationHistory.FAAccountID;
    int? nullable4 = nullable1 ?? copy1.FAAccountID;
    fixedAsset3.FAAccountID = nullable4;
    FixedAsset fixedAsset4 = copy1;
    nullable1 = faLocationHistory.FASubID;
    int? nullable5 = nullable1 ?? copy1.FASubID;
    fixedAsset4.FASubID = nullable5;
    FixedAsset fixedAsset5 = copy1;
    nullable1 = faLocationHistory.AccumulatedDepreciationAccountID;
    int? nullable6 = nullable1 ?? copy1.AccumulatedDepreciationAccountID;
    fixedAsset5.AccumulatedDepreciationAccountID = nullable6;
    FixedAsset fixedAsset6 = copy1;
    nullable1 = faLocationHistory.AccumulatedDepreciationSubID;
    int? nullable7 = nullable1 ?? copy1.AccumulatedDepreciationSubID;
    fixedAsset6.AccumulatedDepreciationSubID = nullable7;
    FixedAsset fixedAsset7 = copy1;
    nullable1 = faLocationHistory.DepreciatedExpenseAccountID;
    int? nullable8 = nullable1 ?? copy1.DepreciatedExpenseAccountID;
    fixedAsset7.DepreciatedExpenseAccountID = nullable8;
    FixedAsset fixedAsset8 = copy1;
    nullable1 = faLocationHistory.DepreciatedExpenseSubID;
    int? nullable9 = nullable1 ?? copy1.DepreciatedExpenseSubID;
    fixedAsset8.DepreciatedExpenseSubID = nullable9;
    copy1.DisposalAccountID = faLocationHistory.DisposalAccountID;
    copy1.DisposalSubID = faLocationHistory.DisposalSubID;
    FixedAsset fixedAsset9 = copy1;
    nullable1 = faLocationHistory.GainAcctID;
    int? nullable10 = nullable1 ?? copy1.GainAcctID;
    fixedAsset9.GainAcctID = nullable10;
    FixedAsset fixedAsset10 = copy1;
    nullable1 = faLocationHistory.GainSubID;
    int? nullable11 = nullable1 ?? copy1.GainSubID;
    fixedAsset10.GainSubID = nullable11;
    FixedAsset fixedAsset11 = copy1;
    nullable1 = faLocationHistory.LossAcctID;
    int? nullable12 = nullable1 ?? copy1.LossAcctID;
    fixedAsset11.LossAcctID = nullable12;
    FixedAsset fixedAsset12 = copy1;
    nullable1 = faLocationHistory.LossSubID;
    int? nullable13 = nullable1 ?? copy1.LossSubID;
    fixedAsset12.LossSubID = nullable13;
    ((PXSelectBase<FixedAsset>) this.Asset).Update(copy1);
  }

  protected virtual void FATran_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    FATran row = (FATran) e.Row;
    if (row == null || !(row.TranType == "P+") || row.Released.GetValueOrDefault())
      return;
    FALocationHistory faLocationHistory = ((PXSelectBase<FALocationHistory>) this.locationHistory).SelectSingle(new object[1]
    {
      (object) row.AssetID
    });
    if (faLocationHistory == null || faLocationHistory.RefNbr == null)
      return;
    faLocationHistory.RefNbr = (string) null;
    ((PXSelectBase<FALocationHistory>) this.locationHistory).Update(faLocationHistory);
  }

  protected virtual void FATran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    FATran row = (FATran) e.Row;
    if (row == null || !(row.Origin != "V") || !(row.Origin != "L") || !(row.TranType == "D-") && !(row.TranType == "D+"))
      return;
    AccountAttribute.VerifyAccountIsNotControl<FATran.debitAccountID>(sender, (EventArgs) e);
  }

  protected virtual void FATran_AssetID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    FATran row = (FATran) e.Row;
    if (row == null)
      return;
    this.SetCurrentAsset(row);
    sender.SetDefaultExt<FATran.branchID>((object) row);
    this.DefaultingAllAccounts(sender, row);
    row.Depreciable = (bool?) ((PXSelectBase<FixedAsset>) this.Asset).Current?.Depreciable;
  }

  protected virtual void FATran_BookID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<FATran.finPeriodID>(e.Row);
  }

  protected virtual void FATran_TranType_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.DefaultingAllAccounts(sender, (FATran) e.Row);
    object finPeriodId = (object) ((FATran) e.Row).FinPeriodID;
    try
    {
      sender.RaiseFieldVerifying<FATran.finPeriodID>(e.Row, ref finPeriodId);
    }
    catch (PXSetPropertyException ex)
    {
      sender.SetValue<FATran.finPeriodID>(e.Row, (object) null);
      sender.RaiseExceptionHandling<FATran.finPeriodID>(e.Row, finPeriodId, (Exception) ex);
    }
  }

  protected virtual void FATran_FinPeriodID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    this.IsDefaulting = true;
  }

  protected virtual void FATran_FinPeriodID_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    try
    {
      if (e.Row == null || !this.IsDefaulting)
        return;
      object obj = (object) FinPeriodIDFormattingAttribute.FormatForStoring((string) e.NewValue);
      sender.RaiseFieldVerifying<FATran.finPeriodID>(e.Row, ref obj);
    }
    finally
    {
      this.IsDefaulting = false;
    }
  }

  protected virtual void FATran_FinPeriodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    FATran row = (FATran) e.Row;
    int? nullable;
    int num;
    if (row == null)
    {
      num = 1;
    }
    else
    {
      nullable = row.AssetID;
      num = !nullable.HasValue ? 1 : 0;
    }
    if (num != 0)
      return;
    nullable = row.BookID;
    if (!nullable.HasValue || !row.TranDate.HasValue || ((CancelEventArgs) e).Cancel)
      return;
    if (e.NewValue == null)
      return;
    try
    {
      if (PXResultset<FABookPeriod>.op_Implicit(PXSelectBase<FABookPeriod, PXSelect<FABookPeriod, Where<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>, And<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.finPeriodID, Equal<Required<FABookPeriod.finPeriodID>>>>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) row.BookID,
        (object) this.FABookPeriodRepository.GetFABookPeriodOrganizationID(row.BookID, row.AssetID),
        (object) (string) e.NewValue
      })) == null)
        throw new PXSetPropertyException("Book Period cannot be found in the system.");
      FABookBalance faBookBalance = ((PXSelectBase<FABookBalance>) this.bookbalances).SelectSingle(new object[2]
      {
        (object) row.AssetID,
        (object) row.BookID
      });
      if (!(row.TranType == "D+") && !(row.TranType == "D-") || !(row.Origin == "A"))
        return;
      if (!string.IsNullOrEmpty(faBookBalance.CurrDeprPeriod) && string.CompareOrdinal((string) e.NewValue, faBookBalance.CurrDeprPeriod) >= 0)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than {0}.", new object[1]
        {
          (object) FinPeriodIDFormattingAttribute.FormatForError(faBookBalance.CurrDeprPeriod)
        });
      if (!string.IsNullOrEmpty(faBookBalance.LastDeprPeriod) && string.CompareOrdinal((string) e.NewValue, faBookBalance.LastDeprPeriod) > 0)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
        {
          (object) FinPeriodIDFormattingAttribute.FormatForError(faBookBalance.LastDeprPeriod)
        });
      if (!string.IsNullOrEmpty(faBookBalance.DeprFromPeriod) && string.CompareOrdinal((string) e.NewValue, faBookBalance.DeprFromPeriod) < 0)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
        {
          (object) FinPeriodIDFormattingAttribute.FormatForError(faBookBalance.DeprFromPeriod)
        });
    }
    catch (PXSetPropertyException ex)
    {
      e.NewValue = (object) PeriodIDAttribute.FormatForDisplay((string) e.NewValue);
      throw;
    }
  }

  protected virtual void FATran_DebitAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<FixedAsset>) this.Asset).Current == null)
      return;
    Dictionary<string, int?> accs = new Dictionary<string, int?>()
    {
      {
        "P+",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.FAAccountID
      },
      {
        "P-",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.FAAccrualAcctID
      },
      {
        "D+",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.DepreciatedExpenseAccountID
      },
      {
        "D-",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.AccumulatedDepreciationAccountID
      },
      {
        "C+",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.DepreciatedExpenseAccountID
      },
      {
        "C-",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.AccumulatedDepreciationAccountID
      },
      {
        "A+",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.DepreciatedExpenseAccountID
      },
      {
        "A-",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.AccumulatedDepreciationAccountID
      },
      {
        "R+",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.FAAccrualAcctID
      },
      {
        "R-",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.FAAccrualAcctID
      },
      {
        "PR",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.FAAccountID
      }
    };
    this.DefaultingAccSub(e, accs);
  }

  protected virtual void FATran_DebitAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    FATran row = (FATran) e.Row;
    if (row == null || !e.ExternalCall || !(row.TranType == "D-") && !(row.TranType == "D+"))
      return;
    AccountAttribute.VerifyAccountIsNotControl<FATran.debitAccountID>(sender, (EventArgs) e);
  }

  protected virtual void FATran_DebitSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<FixedAsset>) this.Asset).Current == null)
      return;
    Dictionary<string, int?> accs = new Dictionary<string, int?>()
    {
      {
        "P+",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.FASubID
      },
      {
        "P-",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.FAAccrualSubID
      },
      {
        "D+",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.DepreciatedExpenseSubID
      },
      {
        "D-",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.AccumulatedDepreciationSubID
      },
      {
        "C+",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.DepreciatedExpenseSubID
      },
      {
        "C-",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.AccumulatedDepreciationSubID
      },
      {
        "A+",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.DepreciatedExpenseSubID
      },
      {
        "A-",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.AccumulatedDepreciationSubID
      },
      {
        "R+",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.FAAccrualSubID
      },
      {
        "R-",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.FAAccrualSubID
      },
      {
        "PR",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.FASubID
      }
    };
    this.DefaultingAccSub(e, accs);
  }

  protected virtual void FATran_CreditAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<FixedAsset>) this.Asset).Current == null)
      return;
    Dictionary<string, int?> accs = new Dictionary<string, int?>()
    {
      {
        "P+",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.FAAccrualAcctID
      },
      {
        "P-",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.FAAccountID
      },
      {
        "D+",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.AccumulatedDepreciationAccountID
      },
      {
        "D-",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.DepreciatedExpenseAccountID
      },
      {
        "C+",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.AccumulatedDepreciationAccountID
      },
      {
        "C-",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.DepreciatedExpenseAccountID
      },
      {
        "A+",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.AccumulatedDepreciationAccountID
      },
      {
        "A-",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.DepreciatedExpenseAccountID
      },
      {
        "R+",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.FAAccrualAcctID
      },
      {
        "R-",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.FAAccrualAcctID
      },
      {
        "PR",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.FAAccrualAcctID
      }
    };
    this.DefaultingAccSub(e, accs);
  }

  protected virtual void FATran_CreditSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<FixedAsset>) this.Asset).Current == null)
      return;
    Dictionary<string, int?> accs = new Dictionary<string, int?>()
    {
      {
        "P+",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.FAAccrualSubID
      },
      {
        "P-",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.FASubID
      },
      {
        "D+",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.AccumulatedDepreciationSubID
      },
      {
        "D-",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.DepreciatedExpenseSubID
      },
      {
        "C+",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.AccumulatedDepreciationSubID
      },
      {
        "C-",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.DepreciatedExpenseSubID
      },
      {
        "A+",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.AccumulatedDepreciationSubID
      },
      {
        "A-",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.DepreciatedExpenseSubID
      },
      {
        "R+",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.FAAccrualSubID
      },
      {
        "R-",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.FAAccrualSubID
      },
      {
        "PR",
        ((PXSelectBase<FixedAsset>) this.Asset).Current.FAAccrualSubID
      }
    };
    this.DefaultingAccSub(e, accs);
  }

  protected virtual void FARegister_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    FARegister row = (FARegister) e.Row;
    if (row == null)
      return;
    if (row.Released.GetValueOrDefault())
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
      cache.AllowDelete = false;
      cache.AllowUpdate = false;
      ((PXSelectBase) this.Trans).Cache.AllowDelete = false;
      ((PXSelectBase) this.Trans).Cache.AllowUpdate = false;
      ((PXSelectBase) this.Trans).Cache.AllowInsert = false;
    }
    else
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) row, true);
      PXUIFieldAttribute.SetEnabled<FARegister.status>(cache, (object) row, false);
      cache.AllowDelete = true;
      cache.AllowUpdate = true;
      ((PXSelectBase) this.Trans).Cache.AllowDelete = true;
      ((PXSelectBase) this.Trans).Cache.AllowUpdate = true;
      ((PXSelectBase) this.Trans).Cache.AllowInsert = true;
    }
    PXUIFieldAttribute.SetEnabled<FARegister.origin>(cache, (object) row, false);
    PXUIFieldAttribute.SetVisible<FARegister.reason>(cache, (object) row, row.Origin == "S" || row.Origin == "T");
    PXUIFieldAttribute.SetVisibility<FATran.srcBranchID>(((PXSelectBase) this.Trans).Cache, (object) null, row.Origin == "T" ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisible<FATran.srcBranchID>(((PXSelectBase) this.Trans).Cache, (object) null, row.Origin == "T");
    PXUIFieldAttribute.SetEnabled<FATran.srcBranchID>(((PXSelectBase) this.Trans).Cache, (object) null, row.Origin == "T");
  }

  protected virtual void FARegister_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row == null || PXLongOperation.GetCurrentItem() != null)
      return;
    using (new PXConnectionScope())
      PXFormulaAttribute.CalcAggregate<FATran.tranAmt>(((PXSelectBase) this.Trans).Cache, e.Row, true);
  }

  protected virtual void FARegister_DocDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is FARegister row) || row.Origin != "A")
      return;
    PXCache pxCache = (PXCache) GraphHelper.Caches<FATran>(sender.Graph);
    foreach (FATran faTran in PXParentAttribute.SelectSiblings(pxCache, (object) null, typeof (FARegister)).OfType<FATran>().Select<FATran, FATran>(new Func<FATran, FATran>(PXCache<FATran>.CreateCopy)))
    {
      faTran.TranDate = row.DocDate;
      pxCache.Update((object) faTran);
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    TransactionEntry.\u003C\u003Ec__DisplayClass85_0 cDisplayClass850 = new TransactionEntry.\u003C\u003Ec__DisplayClass85_0();
    PXCache cache = ((PXSelectBase) this.Document).Cache;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass850.list = new List<FARegister>();
    foreach (FARegister faRegister in adapter.Get<FARegister>().Where<FARegister>((Func<FARegister, bool>) (fadoc => !fadoc.Hold.GetValueOrDefault() && !fadoc.Released.GetValueOrDefault())))
    {
      cache.Update((object) faRegister);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass850.list.Add(faRegister);
    }
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass850.list.Count == 0)
      throw new PXException("Document Status is invalid for processing.");
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass850, __methodptr(\u003CRelease\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass850.list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewBatch(PXAdapter adapter)
  {
    if (((PXSelectBase<FATran>) this.Trans).Current != null)
    {
      JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
      ((PXSelectBase<Batch>) instance.BatchModule).Current = PXResultset<Batch>.op_Implicit(((PXSelectBase<Batch>) instance.BatchModule).Search<Batch.batchNbr>((object) ((PXSelectBase<FATran>) this.Trans).Current.BatchNbr, new object[1]
      {
        (object) "FA"
      }));
      if (((PXSelectBase<Batch>) instance.BatchModule).Current != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, nameof (ViewBatch));
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewAsset(PXAdapter adapter)
  {
    if (((PXSelectBase<FATran>) this.Trans).Current != null)
    {
      AssetMaint instance = PXGraph.CreateInstance<AssetMaint>();
      ((PXSelectBase<FixedAsset>) instance.CurrentAsset).Current = PXResultset<FixedAsset>.op_Implicit(PXSelectBase<FixedAsset, PXSelect<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FATran.assetID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (((PXSelectBase<FixedAsset>) instance.CurrentAsset).Current != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, nameof (ViewAsset));
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewBook(PXAdapter adapter)
  {
    if (((PXSelectBase<FATran>) this.Trans).Current != null)
    {
      BookMaint instance = PXGraph.CreateInstance<BookMaint>();
      ((PXSelectBase<FABook>) instance.Book).Current = PXResultset<FABook>.op_Implicit(PXSelectBase<FABook, PXSelect<FABook, Where<FABook.bookID, Equal<Current<FATran.bookID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (((PXSelectBase<FABook>) instance.Book).Current != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, nameof (ViewBook));
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  public class TransactionEntryFixedAssetChecksExtension : 
    FixedAssetChecksExtensionBase<TransactionEntry>
  {
  }
}
