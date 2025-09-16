// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.SummaryPostingController
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// Realizes aggregating amounts of transactions with enabled summary post flag.
/// </summary>
public class SummaryPostingController
{
  private readonly JournalEntry _journalEntry;
  private readonly CASetup _caSetup;
  private bool _shouldBeNormalized;
  private bool _isSummaryTransactionsLoaded;
  private SummaryPostingController.GLTranGroupComparer _groupComparer;
  private IEqualityComparer<GLTran> _glTranCustomComparer;
  private Dictionary<GLTran, Dictionary<GLTran, GLTran>> _summaryTransactionsGroups;

  public SummaryPostingController(JournalEntry journalEntry, CASetup caSetup)
  {
    this._journalEntry = journalEntry;
    this._caSetup = caSetup;
    this._glTranCustomComparer = ((PXCache<GLTran>) ((PXSelectBase) journalEntry.GLTranModuleBatNbr).Cache).GetKeyComparer<GLTran>();
    this._groupComparer = new SummaryPostingController.GLTranGroupComparer(this._caSetup);
    this._summaryTransactionsGroups = new Dictionary<GLTran, Dictionary<GLTran, GLTran>>((IEqualityComparer<GLTran>) this._groupComparer);
  }

  /// <summary>
  ///  true - suitable summary transaction has been found and amounts have been aggregated.
  ///  false - nothing has been done.
  /// </summary>
  public bool TryAggregateToSummaryTransaction(GLTran tran)
  {
    if (!this.IsSummaryPostingAllowed(tran))
      return false;
    if (((PXSelectBase) this._journalEntry.BatchModule).Cache.GetStatus((object) ((PXSelectBase<Batch>) this._journalEntry.BatchModule).Current) != 2 && !this._isSummaryTransactionsLoaded)
      this.RecreateGroupsBySummaryTransFromDB();
    else
      this.NormalizeIfNeeded();
    Dictionary<GLTran, GLTran> dictionary;
    return this._summaryTransactionsGroups.TryGetValue(tran, out dictionary) && SummaryPostingController.TryAggregateToFirstSuitableTran(((PXSelectBase) this._journalEntry.GLTranModuleBatNbr).Cache, tran, (IEnumerable<GLTran>) dictionary.Values);
  }

  private bool IsSummaryPostingAllowed(GLTran tran)
  {
    if (!tran.SummPost.GetValueOrDefault() || tran.TaskID.HasValue)
      return false;
    bool? zeroPost = tran.ZeroPost;
    bool flag = false;
    if (!(zeroPost.GetValueOrDefault() == flag & zeroPost.HasValue) && !tran.CATranID.HasValue)
    {
      Account account = Account.PK.Find((PXGraph) this._journalEntry, tran.AccountID);
      if (account != null && account.PostOption != "S")
        return false;
    }
    return true;
  }

  private void RecreateGroupsBySummaryTransFromDB()
  {
    IEnumerable<GLTran> glTrans = GraphHelper.RowCast<GLTran>((IEnumerable) ((PXSelectBase<GLTran>) this._journalEntry.GLTranModuleBatNbr).SearchAll<Asc<GLTran.summPost>>(new object[1]
    {
      (object) true
    }, Array.Empty<object>()));
    this._summaryTransactionsGroups = new Dictionary<GLTran, Dictionary<GLTran, GLTran>>((IEqualityComparer<GLTran>) this._groupComparer);
    foreach (GLTran tran in glTrans)
      this.AddSummaryTransaction(tran);
    this._isSummaryTransactionsLoaded = true;
  }

  public void AddSummaryTransaction(GLTran tran)
  {
    this.NormalizeIfNeeded();
    Dictionary<GLTran, GLTran> dictionary1;
    if (this._summaryTransactionsGroups.TryGetValue(tran, out dictionary1))
    {
      dictionary1.Add(tran, tran);
    }
    else
    {
      Dictionary<GLTran, GLTran> dictionary2 = new Dictionary<GLTran, GLTran>(this._glTranCustomComparer)
      {
        {
          tran,
          tran
        }
      };
      this._summaryTransactionsGroups.Add(tran, dictionary2);
    }
  }

  public void RemoveIfNeeded(GLTran tran)
  {
    this.NormalizeIfNeeded();
    Dictionary<GLTran, GLTran> dictionary;
    if (!this._summaryTransactionsGroups.TryGetValue(tran, out dictionary) || !dictionary.ContainsKey(tran))
      return;
    if (dictionary.Count == 1)
      this._summaryTransactionsGroups.Remove(tran);
    else
      dictionary.Remove(tran);
  }

  public void ShouldBeNormalized() => this._shouldBeNormalized = true;

  private void NormalizeIfNeeded()
  {
    if (!this._shouldBeNormalized)
      return;
    Dictionary<GLTran, Dictionary<GLTran, GLTran>> dictionary = new Dictionary<GLTran, Dictionary<GLTran, GLTran>>((IEqualityComparer<GLTran>) this._groupComparer);
    foreach (KeyValuePair<GLTran, Dictionary<GLTran, GLTran>> transactionsGroup in this._summaryTransactionsGroups)
    {
      Dictionary<GLTran, GLTran> collection;
      if (dictionary.TryGetValue(transactionsGroup.Key, out collection))
        collection.AddRange<KeyValuePair<GLTran, GLTran>>((IEnumerable<KeyValuePair<GLTran, GLTran>>) transactionsGroup.Value);
      else
        dictionary.Add(transactionsGroup.Key, new Dictionary<GLTran, GLTran>((IDictionary<GLTran, GLTran>) transactionsGroup.Value, this._glTranCustomComparer));
    }
    this._summaryTransactionsGroups = dictionary;
    this._shouldBeNormalized = false;
  }

  public void ResetState()
  {
    this._summaryTransactionsGroups.Clear();
    this._shouldBeNormalized = false;
    this._isSummaryTransactionsLoaded = false;
  }

  public static bool TryAggregateToFirstSuitableTran(
    PXCache sender,
    GLTran tran,
    IEnumerable<GLTran> summaryTrans)
  {
    foreach (GLTran summaryTran in summaryTrans)
    {
      if (SummaryPostingController.TryAggregateToTran(sender, tran, summaryTran))
        return true;
    }
    return false;
  }

  public static bool TryAggregateToTran(PXCache sender, GLTran tran, GLTran summ_tran)
  {
    PXParentAttribute.SetParent(sender, (object) summ_tran, typeof (Batch), sender.Graph.Caches[typeof (Batch)].Current);
    PXCache<GLTran>.StoreOriginal(sender.Graph, summ_tran);
    GLTran copy = PXCache<GLTran>.CreateCopy(summ_tran);
    GLTran glTran1 = copy;
    Decimal? nullable1 = glTran1.CuryCreditAmt;
    Decimal? nullable2 = tran.CuryCreditAmt;
    glTran1.CuryCreditAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    GLTran glTran2 = copy;
    nullable2 = glTran2.CreditAmt;
    nullable1 = tran.CreditAmt;
    glTran2.CreditAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    GLTran glTran3 = copy;
    nullable1 = glTran3.CuryDebitAmt;
    nullable2 = tran.CuryDebitAmt;
    glTran3.CuryDebitAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    GLTran glTran4 = copy;
    nullable2 = glTran4.DebitAmt;
    nullable1 = tran.DebitAmt;
    glTran4.DebitAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    bool? zeroPost = tran.ZeroPost;
    bool flag = false;
    if (zeroPost.GetValueOrDefault() == flag & zeroPost.HasValue)
      copy.ZeroPost = new bool?(false);
    nullable2 = copy.CuryDebitAmt;
    Decimal? nullable3 = copy.CuryCreditAmt;
    nullable1 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue)
    {
      nullable3 = copy.DebitAmt;
      nullable2 = copy.CreditAmt;
      nullable1 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      Decimal num2 = 0M;
      if (nullable1.GetValueOrDefault() < num2 & nullable1.HasValue)
        goto label_6;
    }
    nullable2 = copy.CuryDebitAmt;
    nullable3 = copy.CuryCreditAmt;
    nullable1 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
    Decimal num3 = 0M;
    if (nullable1.GetValueOrDefault() < num3 & nullable1.HasValue)
    {
      nullable3 = copy.DebitAmt;
      nullable2 = copy.CreditAmt;
      nullable1 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      Decimal num4 = 0M;
      if (nullable1.GetValueOrDefault() > num4 & nullable1.HasValue)
        goto label_6;
    }
    PostGraph.NormalizeAmounts(copy);
    nullable1 = copy.CuryDebitAmt;
    Decimal num5 = 0M;
    if (nullable1.GetValueOrDefault() == num5 & nullable1.HasValue)
    {
      nullable1 = copy.CuryCreditAmt;
      Decimal num6 = 0M;
      if (nullable1.GetValueOrDefault() == num6 & nullable1.HasValue)
      {
        nullable1 = copy.DebitAmt;
        Decimal num7 = 0M;
        if (nullable1.GetValueOrDefault() == num7 & nullable1.HasValue)
        {
          nullable1 = copy.CreditAmt;
          Decimal num8 = 0M;
          if (nullable1.GetValueOrDefault() == num8 & nullable1.HasValue && !copy.ZeroPost.GetValueOrDefault())
          {
            sender.Delete((object) copy);
            goto label_15;
          }
        }
      }
    }
    if (!object.Equals((object) copy.TranDesc, (object) tran.TranDesc))
      copy.TranDesc = "";
    copy.Qty = new Decimal?(0M);
    copy.UOM = (string) null;
    copy.InventoryID = new int?();
    copy.TranLineNbr = new int?();
    sender.Update((object) copy);
label_15:
    return true;
label_6:
    return false;
  }

  private class GLTranGroupComparer : IEqualityComparer<GLTran>
  {
    private readonly CASetup _caSetup;
    private readonly bool _isSubFeatureOn;

    public GLTranGroupComparer(CASetup caSetup)
    {
      this._caSetup = caSetup;
      this._isSubFeatureOn = PXAccess.FeatureInstalled<FeaturesSet.subAccount>();
    }

    public bool Equals(GLTran a, GLTran b)
    {
      bool? summPost1 = a.SummPost;
      bool? summPost2 = b.SummPost;
      int num;
      if (summPost1.GetValueOrDefault() == summPost2.GetValueOrDefault() & summPost1.HasValue == summPost2.HasValue && a.Module == b.Module && a.BatchNbr == b.BatchNbr && a.RefNbr == b.RefNbr)
      {
        long? curyInfoId1 = a.CuryInfoID;
        long? curyInfoId2 = b.CuryInfoID;
        if (curyInfoId1.GetValueOrDefault() == curyInfoId2.GetValueOrDefault() & curyInfoId1.HasValue == curyInfoId2.HasValue)
        {
          int? branchId1 = a.BranchID;
          int? branchId2 = b.BranchID;
          if (branchId1.GetValueOrDefault() == branchId2.GetValueOrDefault() & branchId1.HasValue == branchId2.HasValue)
          {
            int? accountId1 = a.AccountID;
            int? accountId2 = b.AccountID;
            if (accountId1.GetValueOrDefault() == accountId2.GetValueOrDefault() & accountId1.HasValue == accountId2.HasValue)
            {
              int? subId1 = a.SubID;
              int? subId2 = b.SubID;
              if (subId1.GetValueOrDefault() == subId2.GetValueOrDefault() & subId1.HasValue == subId2.HasValue)
              {
                DateTime? tranDate1 = a.TranDate;
                DateTime? tranDate2 = b.TranDate;
                if ((tranDate1.HasValue == tranDate2.HasValue ? (tranDate1.HasValue ? (tranDate1.GetValueOrDefault() == tranDate2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
                {
                  bool? reclassificationProhibited1 = a.ReclassificationProhibited;
                  bool? reclassificationProhibited2 = b.ReclassificationProhibited;
                  if (reclassificationProhibited1.GetValueOrDefault() == reclassificationProhibited2.GetValueOrDefault() & reclassificationProhibited1.HasValue == reclassificationProhibited2.HasValue)
                  {
                    long? caTranId1 = a.CATranID;
                    long? caTranId2 = b.CATranID;
                    if (caTranId1.GetValueOrDefault() == caTranId2.GetValueOrDefault() & caTranId1.HasValue == caTranId2.HasValue || !a.CATranID.HasValue && !b.CATranID.HasValue)
                    {
                      int? projectId1 = a.ProjectID;
                      int? projectId2 = b.ProjectID;
                      if (projectId1.GetValueOrDefault() == projectId2.GetValueOrDefault() & projectId1.HasValue == projectId2.HasValue || !a.ProjectID.HasValue && !b.ProjectID.HasValue)
                      {
                        int? taskId1 = a.TaskID;
                        int? taskId2 = b.TaskID;
                        if (taskId1.GetValueOrDefault() == taskId2.GetValueOrDefault() & taskId1.HasValue == taskId2.HasValue || !a.TaskID.HasValue && !b.TaskID.HasValue)
                        {
                          int? costCodeId1 = a.CostCodeID;
                          int? costCodeId2 = b.CostCodeID;
                          num = costCodeId1.GetValueOrDefault() == costCodeId2.GetValueOrDefault() & costCodeId1.HasValue == costCodeId2.HasValue ? 1 : (a.CostCodeID.HasValue ? 0 : (!b.CostCodeID.HasValue ? 1 : 0));
                          goto label_12;
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
      num = 0;
label_12:
      bool flag = num != 0;
      if (this.IsCATransferOnTransitAccount(a) && CATranType.IsTransfer(b.TranType) || a.Module == "IN")
        return flag;
      return flag && a.TranType == b.TranType;
    }

    public int GetHashCode(GLTran obj)
    {
      int num = (((((((((17 * 23 + obj.SummPost.GetHashCode()) * 23 + obj.Module.GetHashCode()) * 23 + obj.BatchNbr.GetHashCode()) * 23 + obj.RefNbr.GetHashCode()) * 23 + obj.CuryInfoID.GetHashCode()) * 23 + obj.BranchID.GetHashCode()) * 23 + obj.AccountID.GetHashCode()) * 23 + obj.SubID.GetHashCode()) * 23 + obj.TranDate.GetHashCode()) * 23 + obj.ReclassificationProhibited.GetHashCode();
      return this.IsCATransferOnTransitAccount(obj) || obj.Module == "IN" ? num : num * 23 + obj.TranType.GetHashCode();
    }

    private bool IsCATransferOnTransitAccount(GLTran a)
    {
      if (a.Module == "CA")
      {
        int? accountId = a.AccountID;
        int? transitAcctId = (int?) this._caSetup?.TransitAcctId;
        if (accountId.GetValueOrDefault() == transitAcctId.GetValueOrDefault() & accountId.HasValue == transitAcctId.HasValue)
        {
          int? subId = a.SubID;
          int? transitSubId = (int?) this._caSetup?.TransitSubID;
          if (subId.GetValueOrDefault() == transitSubId.GetValueOrDefault() & subId.HasValue == transitSubId.HasValue || !this._isSubFeatureOn)
            return CATranType.IsTransfer(a.TranType);
        }
      }
      return false;
    }
  }
}
