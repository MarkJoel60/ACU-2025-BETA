// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Reclassification.Processing.ReclassifyTransactionsProcessor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.Common.Extensions;
using PX.Objects.GL.Reclassification.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL.Reclassification.Processing;

public class ReclassifyTransactionsProcessor : 
  ReclassifyTransactionsBase<ReclassifyTransactionsProcessor>
{
  public PXSelect<GLTranForReclassification> GLTranForReclass;
  protected JournalEntry JournalEntryInstance;

  public void ProcessTransForReclassification(
    List<GLTranForReclassification> origTransForReclass,
    ReclassGraphState state)
  {
    this.State = state;
    ReclassifyTransactionsProcessor.TransGroupKey? nullable = new ReclassifyTransactionsProcessor.TransGroupKey?();
    this.JournalEntryInstance = this.CreateJournalEntry();
    bool flag = false;
    Batch batchForEdit = (Batch) null;
    IReadOnlyCollection<ReclassifyTransactionsProcessor.ReclassificationItem> reclassItems = (IReadOnlyCollection<ReclassifyTransactionsProcessor.ReclassificationItem>) null;
    Dictionary<GLTranForReclassification, GLTranForReclassification> mappedTrans = new Dictionary<GLTranForReclassification, GLTranForReclassification>();
    Dictionary<GLTranKey, ReclassifyTransactionsProcessor.ReclassificationItem> allReclassItemsByHeaderKey = this.BuildReclassificationItems((ICollection<GLTranForReclassification>) origTransForReclass, out mappedTrans);
    List<GLTranForReclassification> list = mappedTrans.Keys.ToList<GLTranForReclassification>();
    if (this.State.ReclassScreenMode == ReclassScreenMode.Editing)
      nullable = new ReclassifyTransactionsProcessor.TransGroupKey?(this.DefineTransGroupKeyToPutToExistingBatch((IReadOnlyCollection<GLTranForReclassification>) list));
    this.PrepareJournalEntryGraph();
    foreach (IGrouping<ReclassifyTransactionsProcessor.TransGroupKey, ReclassifyTransactionsProcessor.ReclassificationItem> source in allReclassItemsByHeaderKey.Values.GroupBy<ReclassifyTransactionsProcessor.ReclassificationItem, ReclassifyTransactionsProcessor.TransGroupKey>((Func<ReclassifyTransactionsProcessor.ReclassificationItem, ReclassifyTransactionsProcessor.TransGroupKey>) (tranWithCury => new ReclassifyTransactionsProcessor.TransGroupKey()
    {
      MasterPeriodID = tranWithCury.HeadTranForReclass.TranPeriodID,
      CuryID = tranWithCury.CuryInfo.CuryID
    })))
    {
      ReclassifyTransactionsProcessor.ReclassificationItem[] array = source.ToArray<ReclassifyTransactionsProcessor.ReclassificationItem>();
      if (this.State.ReclassScreenMode == ReclassScreenMode.Editing && source.Key.Equals((object) nullable.Value))
      {
        batchForEdit = JournalEntry.FindBatch((PXGraph) this.JournalEntryInstance, this.State.EditingBatchModule, this.State.EditingBatchNbr);
        reclassItems = (IReadOnlyCollection<ReclassifyTransactionsProcessor.ReclassificationItem>) array;
      }
      else
        flag |= !this.ProcessTranForReclassGroup((IReadOnlyCollection<ReclassifyTransactionsProcessor.ReclassificationItem>) array, mappedTrans, origTransForReclass, allReclassItemsByHeaderKey);
    }
    if (this.State.ReclassScreenMode == ReclassScreenMode.Editing)
      flag |= !this.ProcessTranForReclassGroup(reclassItems, mappedTrans, origTransForReclass, allReclassItemsByHeaderKey, batchForEdit);
    if (flag)
      throw new PXException("At least one item has not been processed.");
  }

  protected virtual JournalEntry CreateJournalEntry()
  {
    JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
    instance.Mode |= JournalEntry.Modes.Reclassification;
    PXCacheEx.Adjust<FinPeriodIDAttribute>(((PXSelectBase) instance.GLTranModuleBatNbr).Cache, (object) null).For<GLTran.finPeriodID>((Action<FinPeriodIDAttribute>) (attr => attr.RedefaultOnDateChanged = false));
    PXCacheEx.Adjust<OpenPeriodAttribute>(((PXSelectBase) instance.BatchModule).Cache, (object) null).For<Batch.finPeriodID>((Action<OpenPeriodAttribute>) (attr => attr.RedefaultOnDateChanged = false));
    return instance;
  }

  private void PrepareJournalEntryGraph()
  {
    if (this.State.ReclassScreenMode == ReclassScreenMode.Editing)
    {
      foreach (object obj in this.GetTransReclassTypeSorted((PXGraph) this.JournalEntryInstance, this.State.EditingBatchModule, this.State.EditingBatchNbr))
        ((PXSelectBase) this.JournalEntryInstance.GLTranModuleBatNbr).Cache.SetStatus(obj, (PXEntryStatus) 5);
    }
    ((PXSelectBase<PX.Objects.GL.GLSetup>) this.JournalEntryInstance.glsetup).Current.RequireControlTotal = new bool?(false);
  }

  private bool ProcessTranForReclassGroup(
    IReadOnlyCollection<ReclassifyTransactionsProcessor.ReclassificationItem> reclassItems,
    Dictionary<GLTranForReclassification, GLTranForReclassification> workOrigTranMap,
    List<GLTranForReclassification> origTransForReclass,
    Dictionary<GLTranKey, ReclassifyTransactionsProcessor.ReclassificationItem> allReclassItemsByHeaderKey,
    Batch batchForEdit = null)
  {
    List<GLTran> transMovedFromExistingBatch = new List<GLTran>();
    Batch reclassBatch;
    try
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        reclassBatch = this.BuildReclassificationBatch(reclassItems, transMovedFromExistingBatch, batchForEdit);
        ((PXGraph) this.JournalEntryInstance).Actions.PressSave();
        foreach (GLTran glTran in transMovedFromExistingBatch)
          ((PXSelectBase<GLTran>) this.JournalEntryInstance.GLTranModuleBatNbr).Delete(glTran);
        ((PXGraph) this.JournalEntryInstance).Actions.PressSave();
        foreach (IGrouping<\u003C\u003Ef__AnonymousType60<string, string>, ReclassifyTransactionsProcessor.ReclassificationItem> source in reclassItems.GroupBy(item => new
        {
          Module = item.HeadTranForReclass.Module,
          BatchNbr = item.HeadTranForReclass.BatchNbr
        }))
          this.UpdateOrigRecordsByBatches(source.Key.Module, source.Key.BatchNbr, (IReadOnlyCollection<ReclassifyTransactionsProcessor.ReclassificationItem>) source.ToList<ReclassifyTransactionsProcessor.ReclassificationItem>().AsReadOnly(), reclassBatch, allReclassItemsByHeaderKey);
        transactionScope.Complete();
      }
      foreach (ReclassifyTransactionsProcessor.ReclassificationItem reclassItem in (IEnumerable<ReclassifyTransactionsProcessor.ReclassificationItem>) reclassItems)
      {
        GLTranForReclassification workOrigTran = workOrigTranMap[reclassItem.HeadTranForReclass];
        workOrigTran.ReclassBatchModule = reclassBatch.Module;
        workOrigTran.ReclassBatchNbr = reclassBatch.BatchNbr;
      }
      if (batchForEdit != null)
        this.State.GLTranForReclassToDelete.Clear();
    }
    catch (Exception ex)
    {
      string exceptionMessage = this.GetExceptionMessage(ex);
      string str = PXMessages.LocalizeNoPrefix("Reclassification batch has not been created for the transaction.") + Environment.NewLine + exceptionMessage;
      foreach (ReclassifyTransactionsProcessor.ReclassificationItem reclassItem in (IEnumerable<ReclassifyTransactionsProcessor.ReclassificationItem>) reclassItems)
      {
        ((PXSelectBase) this.GLTranForReclass).Cache.RestoreCopy((object) reclassItem.HeadTranForReclass, (object) workOrigTranMap[reclassItem.HeadTranForReclass]);
        PXProcessing<GLTranForReclassification>.SetError(origTransForReclass.IndexOf(workOrigTranMap[reclassItem.HeadTranForReclass]), str);
      }
      ((PXGraph) this.JournalEntryInstance).Clear();
      this.PrepareJournalEntryGraph();
      return false;
    }
    if (((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current.AutoReleaseReclassBatch.GetValueOrDefault())
    {
      if (batchForEdit == null)
      {
        try
        {
          JournalEntry.ReleaseBatch((IList<Batch>) new Batch[1]
          {
            reclassBatch
          }, (IList<Batch>) null, true);
        }
        catch (Exception ex)
        {
          string exceptionMessage = this.GetExceptionMessage(ex);
          string str = PXMessages.LocalizeNoPrefix("Reclassification batch generated for this transaction has not been released or posted.") + Environment.NewLine + exceptionMessage;
          foreach (ReclassifyTransactionsProcessor.ReclassificationItem reclassItem in (IEnumerable<ReclassifyTransactionsProcessor.ReclassificationItem>) reclassItems)
            PXProcessing<GLTranForReclassification>.SetError(origTransForReclass.IndexOf(workOrigTranMap[reclassItem.HeadTranForReclass]), str);
          return false;
        }
      }
    }
    foreach (ReclassifyTransactionsProcessor.ReclassificationItem reclassItem in (IEnumerable<ReclassifyTransactionsProcessor.ReclassificationItem>) reclassItems)
    {
      PXProcessing<GLTranForReclassification>.SetInfo(origTransForReclass.IndexOf(workOrigTranMap[reclassItem.HeadTranForReclass]), "The record has been processed successfully.");
      foreach (GLTranForReclassification key in reclassItem.SplittingTransForReclass)
        PXProcessing<GLTranForReclassification>.SetInfo(origTransForReclass.IndexOf(workOrigTranMap[key]), "The record has been processed successfully.");
    }
    return true;
  }

  protected virtual void UpdateOrigRecordsByBatches(
    string origModule,
    string origBatchNbr,
    IReadOnlyCollection<ReclassifyTransactionsProcessor.ReclassificationItem> reclassificationItems,
    Batch reclassBatch,
    Dictionary<GLTranKey, ReclassifyTransactionsProcessor.ReclassificationItem> allReclassItemsByHeaderKey)
  {
    ((PXGraph) this.JournalEntryInstance).Clear((PXClearOption) 3);
    ((PXGraph) this.JournalEntryInstance).SelectTimeStamp();
    ((PXSelectBase<Batch>) this.JournalEntryInstance.BatchModule).Current = JournalEntry.FindBatch((PXGraph) this.JournalEntryInstance, origModule, origBatchNbr);
    List<int?> list = reclassificationItems.Select<ReclassifyTransactionsProcessor.ReclassificationItem, int?>((Func<ReclassifyTransactionsProcessor.ReclassificationItem, int?>) (m => m.HeadTranForReclass.LineNbr)).ToList<int?>();
    Decimal d = (Decimal) reclassificationItems.Count / 2M;
    int num = (int) Math.Ceiling(d);
    for (int index = 0; index < num; ++index)
    {
      int count1 = (Decimal) index <= d - 1M ? 2 : reclassificationItems.Count % 2;
      int?[] array = list.GetRange(index * 2, count1).ToArray();
      foreach (GLTran tran in JournalEntry.GetTrans((PXGraph) this.JournalEntryInstance, origModule, origBatchNbr, array))
      {
        tran.ReclassBatchNbr = reclassBatch.BatchNbr;
        tran.ReclassBatchModule = reclassBatch.Module;
        ReclassifyTransactionsProcessor.ReclassificationItem reclassificationItem = allReclassItemsByHeaderKey[new GLTranKey(tran)];
        if (!tran.ReclassTotalCount.HasValue)
          tran.ReclassTotalCount = new int?(0);
        GLTran glTran = tran;
        int? reclassTotalCount = glTran.ReclassTotalCount;
        int count2 = reclassificationItem.NewReclassifyingTrans.Count;
        glTran.ReclassTotalCount = reclassTotalCount.HasValue ? new int?(reclassTotalCount.GetValueOrDefault() + count2) : new int?();
        ((PXSelectBase<GLTran>) this.JournalEntryInstance.GLTranModuleBatNbr).Update(tran);
      }
    }
    ((PXAction) this.JournalEntryInstance.Save).Press();
  }

  private ReclassifyTransactionsProcessor.TransGroupKey DefineTransGroupKeyToPutToExistingBatch(
    IReadOnlyCollection<GLTranForReclassification> transForReclass)
  {
    string minMasterPeriodIDForEditing = (string) null;
    string minMasterPeriodIDForBatchCury = (string) null;
    string strB1 = (string) null;
    string strB2 = (string) null;
    foreach (GLTranForReclassification reclassification in (IEnumerable<GLTranForReclassification>) transForReclass)
    {
      if (reclassification.ReclassRowType == ReclassRowTypes.Editing && (minMasterPeriodIDForEditing == null || string.CompareOrdinal(reclassification.TranPeriodID, minMasterPeriodIDForEditing) < 0))
        minMasterPeriodIDForEditing = reclassification.TranPeriodID;
      if (reclassification.CuryID == this.State.EditingBatchCuryID && (minMasterPeriodIDForBatchCury == null || string.CompareOrdinal(reclassification.TranPeriodID, minMasterPeriodIDForBatchCury) < 0))
        minMasterPeriodIDForBatchCury = reclassification.TranPeriodID;
      int num = string.CompareOrdinal(reclassification.TranPeriodID, strB1);
      if (strB1 == null || num < 0)
      {
        strB1 = reclassification.TranPeriodID;
        strB2 = reclassification.CuryID;
      }
      if (num == 0 && string.CompareOrdinal(reclassification.CuryID, strB2) < 0)
        strB2 = reclassification.CuryID;
    }
    ReclassifyTransactionsProcessor.TransGroupKeyDiscriminatorPair[] discriminatorPairArray = new ReclassifyTransactionsProcessor.TransGroupKeyDiscriminatorPair[3]
    {
      new ReclassifyTransactionsProcessor.TransGroupKeyDiscriminatorPair((Func<GLTranForReclassification, bool>) (tranForReclass => tranForReclass.ReclassRowType == ReclassRowTypes.Editing && tranForReclass.TranPeriodID == this.State.EditingBatchMasterPeriodID)),
      new ReclassifyTransactionsProcessor.TransGroupKeyDiscriminatorPair((Func<GLTranForReclassification, bool>) (tranForReclass => tranForReclass.ReclassRowType == ReclassRowTypes.Editing && tranForReclass.TranPeriodID == minMasterPeriodIDForEditing)),
      new ReclassifyTransactionsProcessor.TransGroupKeyDiscriminatorPair((Func<GLTranForReclassification, bool>) (tranForReclass => tranForReclass.CuryID == this.State.EditingBatchCuryID && tranForReclass.TranPeriodID == minMasterPeriodIDForBatchCury))
    };
    foreach (GLTranForReclassification reclassification in (IEnumerable<GLTranForReclassification>) transForReclass)
    {
      for (int index = 0; index < discriminatorPairArray.Length; ++index)
      {
        if (discriminatorPairArray[index].Discriminator(reclassification))
        {
          if (index == 0)
            return new ReclassifyTransactionsProcessor.TransGroupKey()
            {
              MasterPeriodID = reclassification.TranPeriodID,
              CuryID = reclassification.CuryID
            };
          ReclassifyTransactionsProcessor.TransGroupKey transGroupKey = discriminatorPairArray[index].TransGroupKey with
          {
            CuryID = reclassification.CuryID,
            MasterPeriodID = reclassification.TranPeriodID
          };
        }
      }
    }
    for (int index = 1; index < discriminatorPairArray.Length; ++index)
    {
      if (discriminatorPairArray[index].TransGroupKey.CuryID != null)
        return discriminatorPairArray[index].TransGroupKey;
    }
    return new ReclassifyTransactionsProcessor.TransGroupKey()
    {
      MasterPeriodID = strB1,
      CuryID = strB2
    };
  }

  private Dictionary<GLTranKey, ReclassifyTransactionsProcessor.ReclassificationItem> BuildReclassificationItems(
    ICollection<GLTranForReclassification> transForReclassOrig,
    out Dictionary<GLTranForReclassification, GLTranForReclassification> mappedTrans)
  {
    mappedTrans = transForReclassOrig.ToDictionary<GLTranForReclassification, GLTranForReclassification, GLTranForReclassification>((Func<GLTranForReclassification, GLTranForReclassification>) (origTran => PXCache<GLTranForReclassification>.CreateCopy(origTran)), (Func<GLTranForReclassification, GLTranForReclassification>) (origTran => origTran));
    Dictionary<GLTranKey, ReclassifyTransactionsProcessor.ReclassificationItem> dictionary = new Dictionary<GLTranKey, ReclassifyTransactionsProcessor.ReclassificationItem>();
    foreach (GLTranForReclassification key1 in mappedTrans.Keys)
    {
      if (!key1.IsSplitting)
      {
        GLTranKey key2 = new GLTranKey((GLTran) key1);
        if (dictionary.ContainsKey(key2))
        {
          dictionary[key2].HeadTranForReclass = key1;
        }
        else
        {
          dictionary.Add(key2, new ReclassifyTransactionsProcessor.ReclassificationItem());
          dictionary[key2].HeadTranForReclass = key1;
        }
      }
      else if (dictionary.ContainsKey(key1.ParentKey))
      {
        dictionary[key1.ParentKey].SplittingTransForReclass.Add(key1);
      }
      else
      {
        dictionary.Add(key1.ParentKey, new ReclassifyTransactionsProcessor.ReclassificationItem());
        dictionary[key1.ParentKey].SplittingTransForReclass.Add(key1);
      }
    }
    foreach (IGrouping<long?, ReclassifyTransactionsProcessor.ReclassificationItem> grouping in dictionary.Values.GroupBy<ReclassifyTransactionsProcessor.ReclassificationItem, long?>((Func<ReclassifyTransactionsProcessor.ReclassificationItem, long?>) (m => m.HeadTranForReclass.CuryInfoID)))
    {
      PXResultset<PX.Objects.CM.CurrencyInfo> pxResultset = PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) grouping.Key
      });
      foreach (ReclassifyTransactionsProcessor.ReclassificationItem reclassificationItem in (IEnumerable<ReclassifyTransactionsProcessor.ReclassificationItem>) grouping)
        reclassificationItem.CuryInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResultset);
    }
    return dictionary;
  }

  private Batch BuildReclassificationBatch(
    IReadOnlyCollection<ReclassifyTransactionsProcessor.ReclassificationItem> transForReclassItems,
    List<GLTran> transMovedFromExistingBatch,
    Batch batchForEditing = null)
  {
    DateTime maxValue = DateTime.MaxValue;
    GLTranForReclassification headTranForReclass = transForReclassItems.First<ReclassifyTransactionsProcessor.ReclassificationItem>().HeadTranForReclass;
    ((PXSelectBase<Batch>) this.JournalEntryInstance.BatchModule).Current = (Batch) null;
    Batch row;
    if (batchForEditing == null)
    {
      ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.JournalEntryInstance.currencyinfo).Current = (PX.Objects.CM.CurrencyInfo) null;
      row = ((PXSelectBase<Batch>) this.JournalEntryInstance.BatchModule).Insert(new Batch()
      {
        BranchID = headTranForReclass.NewBranchID,
        BatchType = "RCL",
        FinPeriodID = headTranForReclass.NewFinPeriodID
      });
    }
    else
    {
      row = batchForEditing;
      ((PXSelectBase<Batch>) this.JournalEntryInstance.BatchModule).Current = row;
    }
    foreach (ReclassifyTransactionsProcessor.ReclassificationItem transForReclassItem in (IEnumerable<ReclassifyTransactionsProcessor.ReclassificationItem>) transForReclassItems)
    {
      foreach (GLTranForReclassification tranForReclass in (ReclassifyTransactionsBase<ReclassifyTransactionsProcessor>.IsReclassAttrChanged(transForReclassItem.HeadTranForReclass) ? ((IEnumerable<GLTranForReclassification>) transForReclassItem.HeadTranForReclass.SingleToArray<GLTranForReclassification>()).Union<GLTranForReclassification>((IEnumerable<GLTranForReclassification>) transForReclassItem.SplittingTransForReclass) : (IEnumerable<GLTranForReclassification>) new GLTranForReclassification[0]).Union<GLTranForReclassification>((IEnumerable<GLTranForReclassification>) transForReclassItem.SplittingTransForReclass))
      {
        GLTran editReclassTranPair = this.CreateOrEditReclassTranPair(tranForReclass, transForReclassItem, transMovedFromExistingBatch);
        DateTime? tranDate = editReclassTranPair.TranDate;
        DateTime dateTime = maxValue;
        if ((tranDate.HasValue ? (tranDate.GetValueOrDefault() < dateTime ? 1 : 0) : 0) != 0)
        {
          tranDate = editReclassTranPair.TranDate;
          maxValue = tranDate.Value;
        }
      }
    }
    if (batchForEditing != null)
    {
      foreach (GLTranForReclassification tranForReclass in this.State.GLTranForReclassToDelete)
      {
        GLTran glTran = this.LocateReverseTran(((PXSelectBase) this.JournalEntryInstance.GLTranModuleBatNbr).Cache, tranForReclass);
        ((PXSelectBase<GLTran>) this.JournalEntryInstance.GLTranModuleBatNbr).Delete(this.LocateReclassifyingTran(((PXSelectBase) this.JournalEntryInstance.GLTranModuleBatNbr).Cache, tranForReclass));
        ((PXSelectBase<GLTran>) this.JournalEntryInstance.GLTranModuleBatNbr).Delete(glTran);
      }
    }
    if (batchForEditing == null)
    {
      PX.Objects.CM.CurrencyInfo copy = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(transForReclassItems.First<ReclassifyTransactionsProcessor.ReclassificationItem>().CuryInfo);
      copy.CuryInfoID = new long?();
      PX.Objects.CM.CurrencyInfo currencyInfo = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.JournalEntryInstance.currencyinfo).Insert(copy);
      PX.Objects.GL.Branch branch = PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXViewOf<PX.Objects.GL.Branch>.BasedOn<SelectFromBase<PX.Objects.GL.Branch, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
      {
        (object) headTranForReclass.NewBranchID
      }));
      row.BranchID = headTranForReclass.NewBranchID;
      FinPeriodIDAttribute.SetPeriodsByMaster<Batch.finPeriodID>(((PXSelectBase) this.JournalEntryInstance.BatchModule).Cache, (object) row, headTranForReclass.TranPeriodID);
      row.LedgerID = branch.LedgerID;
      row.Module = "GL";
      row.CuryInfoID = currencyInfo.CuryInfoID;
    }
    else
    {
      PX.Objects.CM.CurrencyInfo currencyInfo = (PX.Objects.CM.CurrencyInfo) null;
      int? nullable1 = new int?(int.MaxValue);
      foreach (ReclassifyTransactionsProcessor.ReclassificationItem transForReclassItem in (IEnumerable<ReclassifyTransactionsProcessor.ReclassificationItem>) transForReclassItems)
      {
        GLTran itemWithMin = transForReclassItem.ReclassifyingTrans.GetItemWithMin<GLTran, int>((Func<GLTran, int>) (m => m.LineNbr.Value));
        int? nullable2 = nullable1;
        int? lineNbr = itemWithMin.LineNbr;
        if (nullable2.GetValueOrDefault() > lineNbr.GetValueOrDefault() & nullable2.HasValue & lineNbr.HasValue)
        {
          nullable1 = itemWithMin.LineNbr;
          currencyInfo = transForReclassItem.CuryInfo;
        }
      }
      PX.Objects.CM.CurrencyInfo copy = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(currencyInfo);
      copy.CuryInfoID = this.JournalEntryInstance.currencyInfo.CuryInfoID;
      copy.tstamp = this.JournalEntryInstance.currencyInfo.tstamp;
      ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.JournalEntryInstance.currencyinfo).Update(copy);
    }
    row.DateEntered = new DateTime?(maxValue);
    row.CuryID = headTranForReclass.CuryID;
    if (this.State.ReclassScreenMode == ReclassScreenMode.Reversing)
    {
      row.OrigModule = this.State.OrigBatchModuleToReverse;
      row.OrigBatchNbr = this.State.OrigBatchNbrToReverse;
      row.AutoReverseCopy = new bool?(true);
    }
    if (!((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current.HoldEntry.GetValueOrDefault())
      ((PXAction) this.JournalEntryInstance.releaseFromHold).Press();
    return ((PXSelectBase<Batch>) this.JournalEntryInstance.BatchModule).Update(row);
  }

  private GLTran CreateOrEditReclassTranPair(
    GLTranForReclassification tranForReclass,
    ReclassifyTransactionsProcessor.ReclassificationItem reclassItem,
    List<GLTran> transMovedFromExistingBatch)
  {
    GLTran editReclassTranPair;
    if (tranForReclass.ReclassRowType == ReclassRowTypes.Editing)
    {
      this.EditReverseTran(tranForReclass, transMovedFromExistingBatch);
      editReclassTranPair = this.EditReclassifyingTran(tranForReclass, transMovedFromExistingBatch);
    }
    else
    {
      PX.Objects.CM.CurrencyInfo currencyInfo = this.CreateCurrencyInfo(reclassItem.CuryInfo);
      this.BuildReverseTran(tranForReclass, reclassItem, currencyInfo);
      editReclassTranPair = this.BuildReclassifyingTran(tranForReclass, reclassItem, currencyInfo);
      reclassItem.NewReclassifyingTrans.Add(editReclassTranPair);
    }
    reclassItem.ReclassifyingTrans.Add(editReclassTranPair);
    return editReclassTranPair;
  }

  private GLTran BuildReclassifyingTran(
    GLTranForReclassification tranForReclass,
    ReclassifyTransactionsProcessor.ReclassificationItem reclassItem,
    PX.Objects.CM.CurrencyInfo newTranCuryInfo)
  {
    GLTran glTran = JournalEntry.BuildReleasableTransaction((PXGraph) this.JournalEntryInstance, (GLTran) tranForReclass, JournalEntry.TranBuildingModes.SetLinkToOriginal, newTranCuryInfo);
    this.SetOrigLineNumber(glTran, reclassItem);
    this.SetReclassifyingTranBusinessAttrs(glTran, tranForReclass);
    this.SetReclassificationLinkingAttrs(glTran, tranForReclass, reclassItem);
    this.SetDependingOnReclassTypeAttrs(glTran, tranForReclass);
    this.SetOtherAttrs(glTran, reclassItem);
    bool isBaseCalc = glTran.TranClass != "R";
    PXDBCurrencyAttribute.SetBaseCalc<GLTran.curyCreditAmt>(((PXSelectBase) this.JournalEntryInstance.GLTranModuleBatNbr).Cache, (object) null, isBaseCalc);
    PXDBCurrencyAttribute.SetBaseCalc<GLTran.curyDebitAmt>(((PXSelectBase) this.JournalEntryInstance.GLTranModuleBatNbr).Cache, (object) null, isBaseCalc);
    return ((PXSelectBase<GLTran>) this.JournalEntryInstance.GLTranModuleBatNbr).Insert(glTran);
  }

  private GLTran EditReclassifyingTran(
    GLTranForReclassification tranForReclass,
    List<GLTran> transMovedFromExistingBatch)
  {
    GLTran glTran = this.LocateReclassifyingTran(((PXSelectBase) this.JournalEntryInstance.GLTranModuleBatNbr).Cache, tranForReclass);
    int? branchId = glTran.BranchID;
    int? newBranchId = tranForReclass.NewBranchID;
    int num;
    if (branchId.GetValueOrDefault() == newBranchId.GetValueOrDefault() & branchId.HasValue == newBranchId.HasValue)
    {
      int? accountId = glTran.AccountID;
      int? nullable1 = tranForReclass.NewAccountID;
      if (accountId.GetValueOrDefault() == nullable1.GetValueOrDefault() & accountId.HasValue == nullable1.HasValue)
      {
        nullable1 = glTran.SubID;
        int? newSubId = tranForReclass.NewSubID;
        if (nullable1.GetValueOrDefault() == newSubId.GetValueOrDefault() & nullable1.HasValue == newSubId.HasValue)
        {
          DateTime? tranDate = glTran.TranDate;
          DateTime? newTranDate = tranForReclass.NewTranDate;
          if ((tranDate.HasValue == newTranDate.HasValue ? (tranDate.HasValue ? (tranDate.GetValueOrDefault() != newTranDate.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0 && !(glTran.TranDesc != tranForReclass.NewTranDesc) && !this.AnyChangesInProjectAttributes(glTran, tranForReclass))
          {
            Decimal? nullable2 = tranForReclass.CuryNewAmt;
            if (nullable2.HasValue)
            {
              Decimal? curyDebitAmt = glTran.CuryDebitAmt;
              Decimal? curyCreditAmt = glTran.CuryCreditAmt;
              nullable2 = curyDebitAmt.HasValue & curyCreditAmt.HasValue ? new Decimal?(curyDebitAmt.GetValueOrDefault() + curyCreditAmt.GetValueOrDefault()) : new Decimal?();
              Decimal? curyNewAmt = tranForReclass.CuryNewAmt;
              num = !(nullable2.GetValueOrDefault() == curyNewAmt.GetValueOrDefault() & nullable2.HasValue == curyNewAmt.HasValue) ? 1 : 0;
              goto label_8;
            }
            num = 0;
            goto label_8;
          }
        }
      }
    }
    num = 1;
label_8:
    if (num == 0)
      return glTran;
    GLTran reclassifyingTran = this.CopyTranForMovingIfNeed(glTran, transMovedFromExistingBatch);
    if (reclassifyingTran != null)
    {
      this.SetReclassifyingTranBusinessAttrs(reclassifyingTran, tranForReclass);
      return ((PXSelectBase<GLTran>) this.JournalEntryInstance.GLTranModuleBatNbr).Insert(reclassifyingTran);
    }
    this.SetReclassifyingTranBusinessAttrs(glTran, tranForReclass);
    return ((PXSelectBase<GLTran>) this.JournalEntryInstance.GLTranModuleBatNbr).Update(glTran);
  }

  private bool AnyChangesInProjectAttributes(
    GLTran reclassifyingTran,
    GLTranForReclassification tranForReclass)
  {
    int? projectId = reclassifyingTran.ProjectID;
    int? newProjectId = tranForReclass.NewProjectID;
    if (projectId.GetValueOrDefault() == newProjectId.GetValueOrDefault() & projectId.HasValue == newProjectId.HasValue)
    {
      int? taskId = reclassifyingTran.TaskID;
      int? newTaskId = tranForReclass.NewTaskID;
      if (taskId.GetValueOrDefault() == newTaskId.GetValueOrDefault() & taskId.HasValue == newTaskId.HasValue)
      {
        int? costCodeId = reclassifyingTran.CostCodeID;
        int? newCostCodeId = tranForReclass.NewCostCodeID;
        return !(costCodeId.GetValueOrDefault() == newCostCodeId.GetValueOrDefault() & costCodeId.HasValue == newCostCodeId.HasValue);
      }
    }
    return true;
  }

  private GLTran CopyTranForMovingIfNeed(GLTran tran, List<GLTran> transMovedFromExistingBatch)
  {
    Batch current = ((PXSelectBase<Batch>) this.JournalEntryInstance.BatchModule).Current;
    if (!(current.Module != tran.Module) && !(current.BatchNbr != tran.BatchNbr))
      return (GLTran) null;
    GLTran glTran = tran;
    tran = PXCache<GLTran>.CreateCopy(glTran);
    tran.Module = (string) null;
    tran.BatchNbr = (string) null;
    tran.LineNbr = new int?();
    transMovedFromExistingBatch.Add(glTran);
    return tran;
  }

  private void EditReverseTran(
    GLTranForReclassification tranForReclass,
    List<GLTran> transMovedFromExistingBatch)
  {
    GLTran tran1 = this.LocateReverseTran(((PXSelectBase) this.JournalEntryInstance.GLTranModuleBatNbr).Cache, tranForReclass);
    bool flag;
    if (tran1 == null && !tranForReclass.EditingPairReclassifyingLineNbr.HasValue)
    {
      flag = false;
    }
    else
    {
      DateTime? tranDate = tran1.TranDate;
      DateTime? newTranDate = tranForReclass.NewTranDate;
      int num;
      if ((tranDate.HasValue == newTranDate.HasValue ? (tranDate.HasValue ? (tranDate.GetValueOrDefault() != newTranDate.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0 && !(tran1.TranDesc != tranForReclass.NewTranDesc))
      {
        Decimal? nullable = tranForReclass.CuryNewAmt;
        if (nullable.HasValue)
        {
          Decimal? curyDebitAmt = tran1.CuryDebitAmt;
          Decimal? curyCreditAmt = tran1.CuryCreditAmt;
          nullable = curyDebitAmt.HasValue & curyCreditAmt.HasValue ? new Decimal?(curyDebitAmt.GetValueOrDefault() + curyCreditAmt.GetValueOrDefault()) : new Decimal?();
          Decimal? curyNewAmt = tranForReclass.CuryNewAmt;
          num = !(nullable.GetValueOrDefault() == curyNewAmt.GetValueOrDefault() & nullable.HasValue == curyNewAmt.HasValue) ? 1 : 0;
        }
        else
          num = 0;
      }
      else
        num = 1;
      flag = num != 0;
    }
    if (!flag)
      return;
    GLTran tran2 = this.CopyTranForMovingIfNeed(tran1, transMovedFromExistingBatch);
    if (tran2 != null)
    {
      this.SetCommonBusinessAttrs(tran2, tranForReclass);
      this.SetReclassAmount(tran2, tranForReclass);
      ((PXSelectBase<GLTran>) this.JournalEntryInstance.GLTranModuleBatNbr).Insert(tran2);
    }
    else
    {
      this.SetCommonBusinessAttrs(tran1, tranForReclass);
      this.SetReclassAmount(tran1, tranForReclass);
      ((PXSelectBase<GLTran>) this.JournalEntryInstance.GLTranModuleBatNbr).Update(tran1);
    }
  }

  private void BuildReverseTran(
    GLTranForReclassification tranForReclass,
    ReclassifyTransactionsProcessor.ReclassificationItem reclassItem,
    PX.Objects.CM.CurrencyInfo newTranCuryInfo)
  {
    GLTran tran = JournalEntry.BuildReverseTran((PXGraph) this.JournalEntryInstance, (GLTran) tranForReclass, JournalEntry.TranBuildingModes.SetLinkToOriginal, newTranCuryInfo);
    tran.IsReclassReverse = new bool?(true);
    this.SetOrigLineNumber(tran, reclassItem);
    this.SetCommonBusinessAttrs(tran, tranForReclass);
    this.SetReclassificationLinkingAttrs(tran, tranForReclass, reclassItem);
    this.SetDependingOnReclassTypeAttrs(tran, tranForReclass);
    this.SetOtherAttrs(tran, reclassItem);
    bool isBaseCalc = tranForReclass.TranClass != "R";
    PXDBCurrencyAttribute.SetBaseCalc<GLTran.curyCreditAmt>(((PXSelectBase) this.JournalEntryInstance.GLTranModuleBatNbr).Cache, (object) null, isBaseCalc);
    PXDBCurrencyAttribute.SetBaseCalc<GLTran.curyDebitAmt>(((PXSelectBase) this.JournalEntryInstance.GLTranModuleBatNbr).Cache, (object) null, isBaseCalc);
    ((PXSelectBase<GLTran>) this.JournalEntryInstance.GLTranModuleBatNbr).Insert(tran);
  }

  protected virtual void SetOtherAttrs(
    GLTran tran,
    ReclassifyTransactionsProcessor.ReclassificationItem reclassItem)
  {
  }

  protected virtual void SetOrigLineNumber(
    GLTran tran,
    ReclassifyTransactionsProcessor.ReclassificationItem reclassItem)
  {
    tran.OrigLineNbr = reclassItem.HeadTranForReclass.LineNbr;
  }

  private void SetCommonBusinessAttrs(
    GLTran tran,
    GLTranForReclassification tranForReclassification)
  {
    tran.TranDate = tranForReclassification.NewTranDate;
    tran.TranDesc = tranForReclassification.NewTranDesc;
  }

  private void SetReclassifyingTranBusinessAttrs(
    GLTran reclassifyingTran,
    GLTranForReclassification tranForReclass)
  {
    reclassifyingTran.BranchID = tranForReclass.NewBranchID;
    reclassifyingTran.AccountID = tranForReclass.NewAccountID;
    if (tranForReclass.NewSubCD != null)
      ((PXSelectBase<GLTran>) this.JournalEntryInstance.GLTranModuleBatNbr).SetValueExt<GLTran.subID>(reclassifyingTran, (object) tranForReclass.NewSubCD);
    else
      reclassifyingTran.SubID = tranForReclass.NewSubID;
    this.SetCommonBusinessAttrs(reclassifyingTran, tranForReclass);
    this.SetReclassAmount(reclassifyingTran, tranForReclass);
    this.SetProjectAttrs(reclassifyingTran, tranForReclass);
  }

  private void SetProjectAttrs(GLTran reclassifyingTran, GLTranForReclassification tranForReclass)
  {
    reclassifyingTran.ProjectID = tranForReclass.NewProjectID;
    reclassifyingTran.TaskID = tranForReclass.NewTaskID;
    reclassifyingTran.CostCodeID = tranForReclass.NewCostCodeID;
  }

  private void SetReclassificationLinkingAttrs(
    GLTran tran,
    GLTranForReclassification tranForReclassification,
    ReclassifyTransactionsProcessor.ReclassificationItem reclassItem)
  {
    if (JournalEntry.IsReclassifacationTran((GLTran) tranForReclassification))
    {
      tran.ReclassSourceTranModule = tranForReclassification.ReclassSourceTranModule;
      tran.ReclassSourceTranBatchNbr = tranForReclassification.ReclassSourceTranBatchNbr;
      tran.ReclassSourceTranLineNbr = tranForReclassification.ReclassSourceTranLineNbr;
      GLTran glTran = tran;
      int? reclassSeqNbr = tranForReclassification.ReclassSeqNbr;
      int? nullable = reclassSeqNbr.HasValue ? new int?(reclassSeqNbr.GetValueOrDefault() + 1) : new int?();
      glTran.ReclassSeqNbr = nullable;
    }
    else
    {
      tran.ReclassSourceTranModule = tranForReclassification.Module;
      tran.ReclassSourceTranBatchNbr = tranForReclassification.BatchNbr;
      tran.ReclassSourceTranLineNbr = reclassItem.HeadTranForReclass.LineNbr;
      tran.ReclassSeqNbr = new int?(1);
    }
    tran.ReclassOrigTranDate = tranForReclassification.TranDate;
  }

  private void SetDependingOnReclassTypeAttrs(
    GLTran tran,
    GLTranForReclassification tranForReclassification)
  {
    tran.ReclassType = tranForReclassification.IsSplitting ? "S" : "C";
    this.SetReclassAmount(tran, tranForReclassification);
  }

  private void SetReclassAmount(GLTran tran, GLTranForReclassification rtran)
  {
    if (!rtran.IsSplitting && !rtran.IsSplitted)
      return;
    if (rtran.IsSplitting)
    {
      if (tran.IsReclassReverse.GetValueOrDefault())
      {
        GLTran glTran1 = tran;
        Decimal? nullable1 = rtran.SourceCuryCreditAmt;
        Decimal num1 = 0M;
        Decimal? nullable2 = !(nullable1.GetValueOrDefault() == num1 & nullable1.HasValue) ? rtran.CuryNewAmt : new Decimal?(0M);
        glTran1.CuryDebitAmt = nullable2;
        GLTran glTran2 = tran;
        nullable1 = rtran.SourceCuryDebitAmt;
        Decimal num2 = 0M;
        Decimal? nullable3 = !(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue) ? rtran.CuryNewAmt : new Decimal?(0M);
        glTran2.CuryCreditAmt = nullable3;
      }
      else
      {
        GLTran glTran3 = tran;
        Decimal? nullable4 = rtran.SourceCuryDebitAmt;
        Decimal num3 = 0M;
        Decimal? nullable5 = !(nullable4.GetValueOrDefault() == num3 & nullable4.HasValue) ? rtran.CuryNewAmt : new Decimal?(0M);
        glTran3.CuryDebitAmt = nullable5;
        GLTran glTran4 = tran;
        nullable4 = rtran.SourceCuryCreditAmt;
        Decimal num4 = 0M;
        Decimal? nullable6 = !(nullable4.GetValueOrDefault() == num4 & nullable4.HasValue) ? rtran.CuryNewAmt : new Decimal?(0M);
        glTran4.CuryCreditAmt = nullable6;
      }
    }
    else
    {
      GLTran glTran5 = tran;
      Decimal? nullable7 = tran.CuryDebitAmt;
      Decimal num5 = 0M;
      Decimal? nullable8 = !(nullable7.GetValueOrDefault() == num5 & nullable7.HasValue) ? rtran.CuryNewAmt : new Decimal?(0M);
      glTran5.CuryDebitAmt = nullable8;
      GLTran glTran6 = tran;
      nullable7 = tran.CuryCreditAmt;
      Decimal num6 = 0M;
      Decimal? nullable9 = !(nullable7.GetValueOrDefault() == num6 & nullable7.HasValue) ? rtran.CuryNewAmt : new Decimal?(0M);
      glTran6.CuryCreditAmt = nullable9;
    }
  }

  private GLTran LocateReclassifyingTran(PXCache cache, GLTranForReclassification tranForReclass)
  {
    GLTran glTran = new GLTran()
    {
      Module = this.State.EditingBatchModule,
      BatchNbr = this.State.EditingBatchNbr,
      LineNbr = tranForReclass.EditingPairReclassifyingLineNbr
    };
    return (GLTran) cache.Locate((object) glTran);
  }

  private GLTran LocateReverseTran(PXCache cache, GLTranForReclassification tranForReclass)
  {
    GLTran glTran1 = new GLTran();
    glTran1.Module = this.State.EditingBatchModule;
    glTran1.BatchNbr = this.State.EditingBatchNbr;
    int? reclassifyingLineNbr = tranForReclass.EditingPairReclassifyingLineNbr;
    glTran1.LineNbr = reclassifyingLineNbr.HasValue ? new int?(reclassifyingLineNbr.GetValueOrDefault() - 1) : new int?();
    GLTran glTran2 = glTran1;
    return (GLTran) cache.Locate((object) glTran2);
  }

  private PX.Objects.CM.CurrencyInfo CreateCurrencyInfo(PX.Objects.CM.CurrencyInfo curyInfo)
  {
    PX.Objects.CM.CurrencyInfo copy = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(curyInfo);
    copy.CuryInfoID = new long?();
    copy.IsReadOnly = new bool?(true);
    copy.BaseCalc = new bool?(true);
    return ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.JournalEntryInstance.currencyinfo).Insert(copy);
  }

  private string GetExceptionMessage(Exception ex)
  {
    return !(ex is PXOuterException pxOuterException) ? ex.Message : $"{((Exception) pxOuterException).Message};{string.Join(";", pxOuterException.InnerMessages)}";
  }

  public struct TransGroupKey
  {
    public string MasterPeriodID { get; set; }

    public string CuryID { get; set; }

    public override bool Equals(object obj)
    {
      if (obj == null || this.GetType() != obj.GetType())
        return false;
      ReclassifyTransactionsProcessor.TransGroupKey transGroupKey = (ReclassifyTransactionsProcessor.TransGroupKey) obj;
      return this.MasterPeriodID == transGroupKey.MasterPeriodID && this.CuryID == transGroupKey.CuryID;
    }

    public override int GetHashCode()
    {
      return (17 * 23 + this.MasterPeriodID.GetHashCode()) * 23 + this.CuryID.GetHashCode();
    }
  }

  public class TransGroupKeyDiscriminatorPair
  {
    public ReclassifyTransactionsProcessor.TransGroupKey TransGroupKey { get; set; }

    public Func<GLTranForReclassification, bool> Discriminator { get; set; }

    public TransGroupKeyDiscriminatorPair(
      Func<GLTranForReclassification, bool> discriminator)
    {
      this.TransGroupKey = new ReclassifyTransactionsProcessor.TransGroupKey();
      this.Discriminator = discriminator;
    }
  }

  public class ReclassificationItem
  {
    public GLTranForReclassification HeadTranForReclass;
    public List<GLTranForReclassification> SplittingTransForReclass;
    public PX.Objects.CM.CurrencyInfo CuryInfo;
    public List<GLTran> ReclassifyingTrans;
    public List<GLTran> NewReclassifyingTrans;

    public ReclassificationItem(
      GLTranForReclassification tran,
      List<GLTranForReclassification> splittingTransForReclass,
      PX.Objects.CM.CurrencyInfo curyInfo)
    {
      this.HeadTranForReclass = tran;
      this.SplittingTransForReclass = splittingTransForReclass;
      this.CuryInfo = curyInfo;
      this.ReclassifyingTrans = new List<GLTran>();
      this.NewReclassifyingTrans = new List<GLTran>();
    }

    public ReclassificationItem()
    {
      this.SplittingTransForReclass = new List<GLTranForReclassification>();
      this.ReclassifyingTrans = new List<GLTran>();
      this.NewReclassifyingTrans = new List<GLTran>();
    }
  }
}
