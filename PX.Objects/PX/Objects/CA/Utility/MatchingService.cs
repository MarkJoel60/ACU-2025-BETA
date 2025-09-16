// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Utility.MatchingService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CA.BankStatementHelpers;
using PX.Objects.CA.BankStatementProtoHelpers;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Objects.CA.Utility;

public class MatchingService : IMatchingService
{
  public virtual IEnumerable<PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>> FindDetailMatches<T>(
    T graph,
    CABankTran aDetail,
    IMatchSettings aSettings,
    PX.Objects.CA.BankStatementHelpers.CATranExt[] aBestMatches,
    Decimal aRelevanceTreshold)
    where T : PXGraph, ICABankTransactionsDataProvider
  {
    List<PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>> detailMatches = new List<PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>>();
    int num1 = aDetail.PayeeBAccountID.HasValue ? 1 : 0;
    int num2 = aDetail.PayeeLocationID.HasValue ? 1 : 0;
    if (!aDetail.TranEntryDate.HasValue && !aDetail.TranDate.HasValue)
      return (IEnumerable<PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>>) detailMatches;
    Pair<DateTime, DateTime> dateRangeForMatch = this.GetDateRangeForMatch(aDetail, aSettings);
    PX.Objects.CA.CashAccount cashAccount = graph.GetCashAccount(aDetail.CashAccountID);
    CASetup caSetup = PXResultset<CASetup>.op_Implicit(PXSelectBase<CASetup, PXSelect<CASetup>.Config>.Select((PXGraph) graph, Array.Empty<object>()));
    string curyId = cashAccount.CuryID;
    PX.Objects.CA.BankStatementHelpers.CATranExt caTranExt1 = (PX.Objects.CA.BankStatementHelpers.CATranExt) null;
    int length = aBestMatches != null ? aBestMatches.Length : 0;
    bool? nullable = cashAccount.MatchToBatch;
    if (nullable.GetValueOrDefault())
    {
      nullable = cashAccount.ClearingAccount;
      if (!nullable.GetValueOrDefault())
      {
        T graph1 = graph;
        CABankTran aDetail1 = aDetail;
        IMatchSettings aSettings1 = aSettings;
        Decimal aRelevanceTreshold1 = aRelevanceTreshold;
        PX.Objects.CA.BankStatementHelpers.CATranExt[] aBestMatches1 = aBestMatches;
        List<PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>> matchList = detailMatches;
        Pair<DateTime, DateTime> tranDateRange = dateRangeForMatch;
        string curyID = curyId;
        PX.Objects.CA.BankStatementHelpers.CATranExt bestMatch = caTranExt1;
        int bestMatchesNumber = length;
        nullable = caSetup.AllowMatchingToUnreleasedBatch;
        int num3 = nullable.GetValueOrDefault() ? 1 : 0;
        caTranExt1 = this.MatchToBatch<T>(graph1, aDetail1, aSettings1, aRelevanceTreshold1, aBestMatches1, matchList, tranDateRange, curyID, bestMatch, bestMatchesNumber, num3 != 0);
      }
    }
    bool bestMatchOnly = aBestMatches != null;
    foreach (PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount, CATran2, CABankTranMatch2, CABatchDetailOrigDocAggregate> matchingTransaction in graph.SearchForMatchingTransactions(aDetail, aSettings, dateRangeForMatch, curyId, bestMatchOnly))
    {
      PX.Objects.CA.BankStatementHelpers.CATranExt caTranExt2 = PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount, CATran2, CABankTranMatch2, CABatchDetailOrigDocAggregate>.op_Implicit(matchingTransaction);
      CABatchDetail detail = (CABatchDetail) PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount, CATran2, CABankTranMatch2, CABatchDetailOrigDocAggregate>.op_Implicit(matchingTransaction);
      nullable = cashAccount.MatchToBatch;
      if (nullable.GetValueOrDefault() && detail != null && detail.BatchNbr != null && !graph.SkipSearchForMatchesInCABatch((CATran) caTranExt2, detail.BatchNbr))
      {
        PXResultset<CABatchDetailOrigDocAggregate> pxResultset = graph.SearchForMatchesInCABatches(aDetail.TranType, detail.BatchNbr);
        if (pxResultset == null || pxResultset.Count < 1)
          continue;
      }
      PX.Objects.CA.Light.BAccount baccount = PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount, CATran2, CABankTranMatch2, CABatchDetailOrigDocAggregate>.op_Implicit(matchingTransaction);
      caTranExt2.ReferenceName = baccount.AcctName;
      bool flag1 = false;
      CABankTranMatch match1 = (CABankTranMatch) PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount, CATran2, CABankTranMatch2, CABatchDetailOrigDocAggregate>.op_Implicit(matchingTransaction);
      CABankTranMatch caBankTranMatch = this.CheckMatchInCache(graph.Caches[typeof (CABankTranMatch)], caTranExt2, match1);
      if (caBankTranMatch == null || !caBankTranMatch.TranID.HasValue)
      {
        CABankTranMatch match2 = (CABankTranMatch) PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount, CATran2, CABankTranMatch2, CABatchDetailOrigDocAggregate>.op_Implicit(matchingTransaction);
        caBankTranMatch = this.CheckBatchMatchInCache(graph.Caches[typeof (CABankTranMatch)], detail, match2);
      }
      if (caBankTranMatch != null && caBankTranMatch.TranID.HasValue)
      {
        int? tranId1 = caBankTranMatch.TranID;
        int? tranId2 = aDetail.TranID;
        if (tranId1.GetValueOrDefault() == tranId2.GetValueOrDefault() & tranId1.HasValue == tranId2.HasValue)
          flag1 = true;
        else
          continue;
      }
      caTranExt2.MatchRelevance = new Decimal?(this.EvaluateMatching(aDetail, (CATran) caTranExt2, aSettings));
      caTranExt2.IsMatched = new bool?(flag1);
      nullable = caTranExt2.IsMatched;
      bool flag2 = false;
      Decimal? matchRelevance1;
      if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
      {
        matchRelevance1 = caTranExt2.MatchRelevance;
        Decimal num4 = aRelevanceTreshold;
        if (matchRelevance1.GetValueOrDefault() < num4 & matchRelevance1.HasValue)
          continue;
      }
      Decimal? matchRelevance2;
      if (length > 0)
      {
        for (int index1 = 0; index1 < length; ++index1)
        {
          if (aBestMatches[index1] != null)
          {
            matchRelevance1 = aBestMatches[index1].MatchRelevance;
            matchRelevance2 = caTranExt2.MatchRelevance;
            if (!(matchRelevance1.GetValueOrDefault() < matchRelevance2.GetValueOrDefault() & matchRelevance1.HasValue & matchRelevance2.HasValue))
              continue;
          }
          for (int index2 = length - 1; index2 > index1; --index2)
            aBestMatches[index2] = aBestMatches[index2 - 1];
          aBestMatches[index1] = caTranExt2;
          break;
        }
      }
      else
      {
        if (caTranExt1 != null)
        {
          matchRelevance2 = caTranExt1.MatchRelevance;
          matchRelevance1 = caTranExt2.MatchRelevance;
          if (!(matchRelevance2.GetValueOrDefault() < matchRelevance1.GetValueOrDefault() & matchRelevance2.HasValue & matchRelevance1.HasValue))
            goto label_29;
        }
        caTranExt1 = caTranExt2;
      }
label_29:
      caTranExt2.IsBestMatch = new bool?(false);
      caTranExt2.Status = graph.GetStatus((CATran) caTranExt2);
      detailMatches.Add(new PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>(caTranExt2, baccount));
    }
    if (aDetail.DocumentMatched.GetValueOrDefault())
    {
      foreach (PXResult<CABankTranMatch, PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount> pxResult in PXSelectBase<CABankTranMatch, PXSelectJoin<CABankTranMatch, LeftJoin<PX.Objects.CA.BankStatementHelpers.CATranExt, On<CABankTranMatch.cATranID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.tranID>>, LeftJoin<PX.Objects.CA.Light.BAccount, On<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.referenceID>>>>, Where<CABankTranMatch.tranID, Equal<Required<CABankTranMatch.tranID>>>>.Config>.Select((PXGraph) graph, new object[1]
      {
        (object) aDetail.TranID
      }))
      {
        PX.Objects.CA.BankStatementHelpers.CATranExt matchedTran = PXResult<CABankTranMatch, PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>.op_Implicit(pxResult);
        CABankTranMatch caBankTranMatch = PXResult<CABankTranMatch, PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>.op_Implicit(pxResult);
        PX.Objects.CA.Light.BAccount baccount = PXResult<CABankTranMatch, PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>.op_Implicit(pxResult);
        if (matchedTran != null && matchedTran.TranID.HasValue)
        {
          if (detailMatches.Find((Predicate<PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>>) (result =>
          {
            long? tranId3 = PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>.op_Implicit(result).TranID;
            long? tranId4 = matchedTran.TranID;
            return tranId3.GetValueOrDefault() == tranId4.GetValueOrDefault() & tranId3.HasValue == tranId4.HasValue;
          })) == null)
          {
            matchedTran.MatchRelevance = new Decimal?(this.EvaluateMatching(aDetail, (CATran) matchedTran, aSettings));
            matchedTran.IsMatched = new bool?(true);
            matchedTran.Status = graph.GetStatus((CATran) matchedTran);
            detailMatches.Add(new PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>(matchedTran, baccount));
          }
        }
        else if (caBankTranMatch.DocModule == "AP" && caBankTranMatch.DocType == "CBT")
        {
          CABatch batch = PXResultset<CABatch>.op_Implicit(PXSelectBase<CABatch, PXSelect<CABatch, Where<CABatch.batchNbr, Equal<Required<CABatch.batchNbr>>>>.Config>.Select((PXGraph) graph, new object[1]
          {
            (object) caBankTranMatch.DocRefNbr
          }));
          if (batch != null && batch.BatchNbr != null && detailMatches.Find((Predicate<PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>>) (result =>
          {
            PX.Objects.CA.BankStatementHelpers.CATranExt caTranExt3 = PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>.op_Implicit(result);
            return caTranExt3.OrigModule == "AP" && caTranExt3.OrigRefNbr == batch.BatchNbr && caTranExt3.OrigTranType == "CBT";
          })) == null)
          {
            matchedTran = new PX.Objects.CA.BankStatementHelpers.CATranExt();
            batch.CopyTo((CATran) matchedTran);
            matchedTran.MatchRelevance = new Decimal?(this.EvaluateMatching(aDetail, (CATran) matchedTran, aSettings));
            matchedTran.IsMatched = new bool?(true);
            matchedTran.Status = graph.GetStatus((CATran) matchedTran);
            detailMatches.Add(new PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>(matchedTran, baccount));
          }
        }
      }
    }
    if (length > 0)
      caTranExt1 = aBestMatches[0];
    if (caTranExt1 != null)
      caTranExt1.IsBestMatch = new bool?(true);
    aDetail.CountMatches = new int?(detailMatches.Count);
    return (IEnumerable<PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>>) detailMatches;
  }

  public virtual PX.Objects.CA.BankStatementHelpers.CATranExt MatchToBatch<T>(
    T graph,
    CABankTran aDetail,
    IMatchSettings aSettings,
    Decimal aRelevanceTreshold,
    PX.Objects.CA.BankStatementHelpers.CATranExt[] aBestMatches,
    List<PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>> matchList,
    Pair<DateTime, DateTime> tranDateRange,
    string curyID,
    PX.Objects.CA.BankStatementHelpers.CATranExt bestMatch,
    int bestMatchesNumber,
    bool allowUnreleased)
    where T : PXGraph, ICABankTransactionsDataProvider
  {
    List<CABatchWithBaccount> batchWithBaccountList = new List<CABatchWithBaccount>();
    bool flag1 = false;
    bool flag2 = false;
    foreach (PXResult<CABatch, CABatchDetail, PX.Objects.CA.Light.APPayment, PX.Objects.CA.Light.BAccount, CABankTranMatch> forMatchingCaBatch in graph.SearchForMatchingCABatches(aDetail, aSettings, tranDateRange, curyID, allowUnreleased))
    {
      CABankTranMatch caBankTranMatch = PXResult<CABatch, CABatchDetail, PX.Objects.CA.Light.APPayment, PX.Objects.CA.Light.BAccount, CABankTranMatch>.op_Implicit(forMatchingCaBatch);
      CABatch caBatch = PXResult<CABatch, CABatchDetail, PX.Objects.CA.Light.APPayment, PX.Objects.CA.Light.BAccount, CABankTranMatch>.op_Implicit(forMatchingCaBatch);
      PX.Objects.CA.Light.APPayment apPayment = PXResult<CABatch, CABatchDetail, PX.Objects.CA.Light.APPayment, PX.Objects.CA.Light.BAccount, CABankTranMatch>.op_Implicit(forMatchingCaBatch);
      PX.Objects.CA.Light.BAccount baccount = PXResult<CABatch, CABatchDetail, PX.Objects.CA.Light.APPayment, PX.Objects.CA.Light.BAccount, CABankTranMatch>.op_Implicit(forMatchingCaBatch);
      int? nullable1;
      if (batchWithBaccountList.Count == 0 || batchWithBaccountList[batchWithBaccountList.Count - 1].Batch.BatchNbr != caBatch.BatchNbr)
      {
        if (batchWithBaccountList.Count > 0)
        {
          if (flag2)
          {
            CABatchWithBaccount batchWithBaccount = batchWithBaccountList[batchWithBaccountList.Count - 1];
            nullable1 = new int?();
            int? nullable2 = nullable1;
            batchWithBaccount.BaccountID = nullable2;
          }
          if (flag1)
            batchWithBaccountList.RemoveAt(batchWithBaccountList.Count - 1);
        }
        flag1 = false;
        flag2 = false;
        batchWithBaccountList.Add(new CABatchWithBaccount()
        {
          Batch = caBatch,
          BaccountID = apPayment.VendorID,
          BAccount = baccount
        });
      }
      if (caBankTranMatch != null)
      {
        nullable1 = caBankTranMatch.TranID;
        if (nullable1.HasValue)
          flag1 = true;
      }
      if (batchWithBaccountList.Count != 0)
      {
        nullable1 = batchWithBaccountList[batchWithBaccountList.Count - 1].BaccountID;
        int? vendorId = apPayment.VendorID;
        if (nullable1.GetValueOrDefault() == vendorId.GetValueOrDefault() & nullable1.HasValue == vendorId.HasValue)
          continue;
      }
      flag2 = true;
    }
    if (batchWithBaccountList.Count > 0)
    {
      if (flag2)
        batchWithBaccountList[batchWithBaccountList.Count - 1].BaccountID = new int?();
      if (flag1)
        batchWithBaccountList.RemoveAt(batchWithBaccountList.Count - 1);
    }
    foreach (CABatchWithBaccount batchWithBaccount in batchWithBaccountList)
    {
      PX.Objects.CA.BankStatementHelpers.CATranExt caTranExt = new PX.Objects.CA.BankStatementHelpers.CATranExt();
      batchWithBaccount.Batch.CopyTo((CATran) caTranExt);
      caTranExt.ReferenceID = batchWithBaccount.BaccountID;
      if (!graph.SkipSearchForMatchesInCABatch((CATran) caTranExt, batchWithBaccount.Batch.BatchNbr))
      {
        bool flag3 = false;
        PXResultset<CABankTranMatch> pxResultset = graph.SearchForTranMatchForCABatch(batchWithBaccount.Batch.BatchNbr);
        if (pxResultset.Count != 0)
        {
          int? tranId1 = PXResultset<CABankTranMatch>.op_Implicit(pxResultset).TranID;
          int? tranId2 = aDetail.TranID;
          if (tranId1.GetValueOrDefault() == tranId2.GetValueOrDefault() & tranId1.HasValue == tranId2.HasValue)
            flag3 = true;
          else
            continue;
        }
        caTranExt.MatchRelevance = new Decimal?(this.EvaluateMatching(aDetail, (CATran) caTranExt, aSettings));
        caTranExt.IsMatched = new bool?(flag3);
        bool? isMatched = caTranExt.IsMatched;
        bool flag4 = false;
        Decimal? matchRelevance1;
        if (isMatched.GetValueOrDefault() == flag4 & isMatched.HasValue)
        {
          matchRelevance1 = caTranExt.MatchRelevance;
          Decimal num = aRelevanceTreshold;
          if (matchRelevance1.GetValueOrDefault() < num & matchRelevance1.HasValue)
            continue;
        }
        Decimal? matchRelevance2;
        if (bestMatchesNumber > 0)
        {
          for (int index1 = 0; index1 < bestMatchesNumber; ++index1)
          {
            if (aBestMatches[index1] != null)
            {
              matchRelevance1 = aBestMatches[index1].MatchRelevance;
              matchRelevance2 = caTranExt.MatchRelevance;
              if (!(matchRelevance1.GetValueOrDefault() < matchRelevance2.GetValueOrDefault() & matchRelevance1.HasValue & matchRelevance2.HasValue))
                continue;
            }
            for (int index2 = bestMatchesNumber - 1; index2 > index1; --index2)
              aBestMatches[index2] = aBestMatches[index2 - 1];
            aBestMatches[index1] = caTranExt;
            break;
          }
        }
        else
        {
          if (bestMatch != null)
          {
            matchRelevance2 = bestMatch.MatchRelevance;
            matchRelevance1 = caTranExt.MatchRelevance;
            if (!(matchRelevance2.GetValueOrDefault() < matchRelevance1.GetValueOrDefault() & matchRelevance2.HasValue & matchRelevance1.HasValue))
              goto label_45;
          }
          bestMatch = caTranExt;
        }
label_45:
        caTranExt.IsBestMatch = new bool?(false);
        matchList.Add(new PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>(caTranExt, batchWithBaccount.BAccount));
      }
    }
    return bestMatch;
  }

  private CABankTranMatch CheckMatchInCache(PXCache cache, PX.Objects.CA.BankStatementHelpers.CATranExt catran, CABankTranMatch match)
  {
    long? nullable1;
    long? nullable2;
    if (match != null && match.TranID.HasValue)
    {
      PXEntryStatus status = cache.GetStatus((object) match);
      if (status != 3)
      {
        if (status == 1)
        {
          nullable1 = ((CABankTranMatch) cache.Locate((object) match)).CATranID;
          nullable2 = catran.TranID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
            goto label_5;
        }
        else
          goto label_5;
      }
      match = (CABankTranMatch) null;
    }
label_5:
    if (match == null || !match.TranID.HasValue)
    {
      foreach (CABankTranMatch caBankTranMatch in cache.Inserted)
      {
        nullable2 = caBankTranMatch.CATranID;
        nullable1 = catran.TranID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          return caBankTranMatch;
      }
      foreach (CABankTranMatch caBankTranMatch in cache.Updated)
      {
        long? caTranId = caBankTranMatch.CATranID;
        long? tranId = catran.TranID;
        if (caTranId.GetValueOrDefault() == tranId.GetValueOrDefault() & caTranId.HasValue == tranId.HasValue)
          return caBankTranMatch;
      }
    }
    return match;
  }

  private CABankTranMatch CheckBatchMatchInCache(
    PXCache cache,
    CABatchDetail detail,
    CABankTranMatch match)
  {
    if (match != null && match.TranID.HasValue)
    {
      PXEntryStatus status = cache.GetStatus((object) match);
      if (status == 3)
        match = (CABankTranMatch) null;
      else if (status == 1)
      {
        CABankTranMatch caBankTranMatch = (CABankTranMatch) cache.Locate((object) match);
        if (caBankTranMatch.DocRefNbr != match.DocRefNbr || caBankTranMatch.DocType != match.DocType || caBankTranMatch.DocModule != match.DocModule)
          match = (CABankTranMatch) null;
      }
    }
    if ((match == null || !match.TranID.HasValue) && detail != null && detail.BatchNbr != null)
    {
      foreach (CABankTranMatch caBankTranMatch in cache.Inserted)
      {
        if (caBankTranMatch.DocRefNbr == detail.BatchNbr && caBankTranMatch.DocType == "CBT" && caBankTranMatch.DocModule == "AP")
          return caBankTranMatch;
      }
      foreach (CABankTranMatch caBankTranMatch in cache.Updated)
      {
        if (caBankTranMatch.DocRefNbr == detail.BatchNbr && caBankTranMatch.DocType == "CBT" && caBankTranMatch.DocModule == "AP")
          return caBankTranMatch;
      }
    }
    return match;
  }

  public virtual bool CheckRuleMatches(CABankTran transaction, CABankTranRule rule)
  {
    if (rule.BankDrCr != transaction.DrCr)
      return false;
    bool flag = true;
    Decimal? nullable = transaction.CuryTranAmt;
    Decimal num1 = Math.Abs(nullable ?? 0.0M);
    switch (rule.AmountMatchingMode)
    {
      case "E":
        int num2 = flag ? 1 : 0;
        Decimal num3 = num1;
        nullable = rule.CuryTranAmt;
        Decimal valueOrDefault1 = nullable.GetValueOrDefault();
        int num4 = num3 == valueOrDefault1 & nullable.HasValue ? 1 : 0;
        flag = (num2 & num4) != 0;
        break;
      case "B":
        int num5 = flag ? 1 : 0;
        nullable = rule.CuryTranAmt;
        Decimal num6 = num1;
        int num7;
        if (nullable.GetValueOrDefault() <= num6 & nullable.HasValue)
        {
          Decimal num8 = num1;
          nullable = rule.MaxCuryTranAmt;
          Decimal valueOrDefault2 = nullable.GetValueOrDefault();
          num7 = num8 <= valueOrDefault2 & nullable.HasValue ? 1 : 0;
        }
        else
          num7 = 0;
        flag = (num5 & num7) != 0;
        break;
    }
    if (rule.BankTranCashAccountID.HasValue & flag)
    {
      int num9 = flag ? 1 : 0;
      int? cashAccountId = transaction.CashAccountID;
      int? tranCashAccountId = rule.BankTranCashAccountID;
      int num10 = cashAccountId.GetValueOrDefault() == tranCashAccountId.GetValueOrDefault() & cashAccountId.HasValue == tranCashAccountId.HasValue ? 1 : 0;
      flag = (num9 & num10) != 0;
    }
    if (rule.TranCuryID != null & flag)
      flag &= (transaction.CuryID ?? "").Trim() == rule.TranCuryID.Trim();
    if (!string.IsNullOrWhiteSpace(rule.TranCode) & flag)
      flag &= string.Equals(transaction.TranCode ?? "", rule.TranCode, StringComparison.CurrentCultureIgnoreCase);
    if (!string.IsNullOrEmpty(rule.BankTranDescription) & flag)
      flag &= rule.Regex.IsMatch(transaction.TranDesc ?? "");
    if (!string.IsNullOrEmpty(rule.PayeeName) & flag)
      flag &= this.GetRegexPayeeName(rule).IsMatch(transaction.PayeeName ?? "");
    return flag;
  }

  private Regex GetRegexPayeeName(CABankTranRule rule)
  {
    string str = Regex.Escape(rule.PayeeName ?? "");
    return new Regex(rule.UsePayeeNameWildcards.GetValueOrDefault() ? $"^{str.Replace("\\*", ".*").Replace("\\?", ".")}$" : str, rule.MatchDescriptionCase.GetValueOrDefault() ? RegexOptions.None : RegexOptions.IgnoreCase);
  }

  public virtual Decimal CompareDate(
    CABankTran aDetail,
    DateTime? tranDate,
    double meanValue,
    double sigma)
  {
    if (!tranDate.HasValue)
      return 0M;
    TimeSpan timeSpan1 = aDetail.TranDate.Value - tranDate.Value;
    TimeSpan timeSpan2 = aDetail.TranEntryDate.HasValue ? aDetail.TranEntryDate.Value - tranDate.Value : timeSpan1;
    TimeSpan timeSpan3 = timeSpan1.Duration() < timeSpan2.Duration() ? timeSpan1 : timeSpan2;
    double num1 = sigma * sigma;
    if (num1 < 1.0)
      num1 = 0.25;
    Decimal num2 = (Decimal) Math.Exp(-(Math.Pow(timeSpan3.TotalDays - meanValue, 2.0) / (2.0 * num1)));
    return !(num2 > 0M) ? 0.0M : num2;
  }

  public virtual Decimal CompareDate(
    CABankTran aDetail,
    CATran aTran,
    double meanValue,
    double sigma)
  {
    return this.CompareDate(aDetail, aTran.TranDate, meanValue, sigma);
  }

  public virtual Decimal CompareDate(
    CABankTran aDetail,
    CABankTranInvoiceMatch aTran,
    double meanValue,
    double sigma)
  {
    DateTime? discDate = (DateTime?) aTran?.DiscDate;
    DateTime dateTime = aDetail.TranDate.Value;
    DateTime? tranDate = (discDate.HasValue ? (discDate.GetValueOrDefault() >= dateTime ? 1 : 0) : 0) != 0 ? (DateTime?) aTran?.DiscDate : (DateTime?) aTran?.DueDate;
    return this.CompareDate(aDetail, tranDate, meanValue, sigma);
  }

  public virtual Decimal CompareDate(
    CABankTran aDetail,
    CABankTranExpenseDetailMatch aTran,
    double meanValue,
    double sigma)
  {
    return this.CompareDate(aDetail, aTran.DocDate, meanValue, sigma);
  }

  public virtual Decimal CompareRefNbr(
    CABankTran aDetail,
    string extRefNbr,
    bool looseCompare,
    IMatchSettings matchSettings)
  {
    return looseCompare ? this.EvaluateMatching(aDetail.ExtRefNbr, extRefNbr, false, matchSettings.EmptyRefNbrMatching ?? true) : this.EvaluateTideMatching(aDetail.ExtRefNbr, extRefNbr, false, matchSettings.EmptyRefNbrMatching ?? true);
  }

  public virtual Decimal CompareRefNbr(
    CABankTran aDetail,
    CATran aTran,
    bool looseCompare,
    IMatchSettings matchSettings)
  {
    return this.CompareRefNbr(aDetail, aTran.ExtRefNbr, looseCompare, matchSettings);
  }

  public virtual Decimal CompareRefNbr(
    CABankTran aDetail,
    CABankTranInvoiceMatch aTran,
    bool looseCompare)
  {
    Decimal num1;
    Decimal num2;
    if (looseCompare)
    {
      num1 = !string.IsNullOrEmpty(aDetail.InvoiceInfo) || !string.IsNullOrEmpty(aTran.ExtRefNbr) ? this.EvaluateMatching(aDetail.InvoiceInfo, aTran.ExtRefNbr, false, true) : 0M;
      num2 = !string.IsNullOrEmpty(aDetail.InvoiceInfo) || !string.IsNullOrEmpty(aTran.OrigRefNbr) ? this.EvaluateMatching(aDetail.InvoiceInfo, aTran.OrigRefNbr, false, true) : 0M;
    }
    else
    {
      num1 = !string.IsNullOrEmpty(aDetail.InvoiceInfo) || !string.IsNullOrEmpty(aTran.ExtRefNbr) ? this.EvaluateTideMatching(aDetail.InvoiceInfo, aTran.ExtRefNbr, false, true) : 0M;
      num2 = !string.IsNullOrEmpty(aDetail.InvoiceInfo) || !string.IsNullOrEmpty(aTran.OrigRefNbr) ? this.EvaluateTideMatching(aDetail.InvoiceInfo, aTran.OrigRefNbr, false, true) : 0M;
    }
    return !(num1 > num2) ? num2 : num1;
  }

  public virtual Decimal CompareRefNbr(
    CABankTran aDetail,
    CABankTranExpenseDetailMatch aTran,
    bool looseCompare,
    IMatchSettings matchSettings)
  {
    return this.CompareRefNbr(aDetail, aTran.ExtRefNbr, looseCompare, matchSettings);
  }

  public virtual Decimal ComparePayee(CABankTran aDetail, CATran aTran)
  {
    return this.EvaluateMatching(aDetail.PayeeName, aTran.ReferenceName, false, true);
  }

  public virtual Decimal ComparePayee(CABankTran aDetail, CABankTranInvoiceMatch aTran)
  {
    return this.EvaluateMatching(aDetail.PayeeName, aTran.ReferenceName, false, true);
  }

  public virtual Decimal CompareExpenseReceiptAmount(
    CABankTran aDetail,
    Decimal expenseAmount,
    Decimal diffTreshold)
  {
    return this.CompareExpenseReceiptAmount(-aDetail.CuryTranAmt.GetValueOrDefault(), expenseAmount, diffTreshold);
  }

  public virtual Decimal CompareExpenseReceiptAmount(
    CABankTran bankTran,
    CABankTranExpenseDetailMatch receipt,
    IMatchSettings settings)
  {
    CABankTran aDetail = bankTran;
    Decimal? nullable = receipt.CuryDocAmt;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = settings.CuryDiffThreshold;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    return this.CompareExpenseReceiptAmount(aDetail, valueOrDefault1, valueOrDefault2);
  }

  public virtual Decimal CompareExpenseReceiptAmount(
    Decimal tranAmount,
    Decimal expenseAmount,
    Decimal diffTreshold)
  {
    double num1 = Convert.ToDouble(tranAmount - expenseAmount);
    double num2 = Convert.ToDouble(diffTreshold * tranAmount) / 100.0;
    if (num2 == 0.0)
      return 0M;
    Decimal num3 = (Decimal) Math.Exp(-(num1 * num1 / (2.0 * num2 * num2)));
    return !(num3 > 0M) ? 0.0M : num3;
  }

  public virtual Decimal EvaluateMatching(
    string aStr1,
    string aStr2,
    bool aCaseSensitive,
    bool matchEmpty = true)
  {
    Decimal matching = 0M;
    if (string.IsNullOrEmpty(aStr1) || string.IsNullOrEmpty(aStr2))
      return this.MatchEmptyStrings(aStr1, aStr2, matchEmpty);
    string str1 = aStr1.Trim();
    string str2 = aStr2.Trim();
    int num1 = str1.Length > str2.Length ? str1.Length : str2.Length;
    if (num1 == 0)
      return 1M;
    Decimal num2 = 1M / (Decimal) num1;
    Decimal num3 = 0M;
    for (int index = 0; index < num1; ++index)
    {
      if (index < str1.Length && index < str2.Length)
      {
        char lower;
        int num4;
        if (!aCaseSensitive)
        {
          lower = char.ToLower(str2[index]);
          num4 = lower.CompareTo(char.ToLower(str1[index])) == 0 ? 1 : 0;
        }
        else
        {
          lower = str2[index];
          num4 = lower.CompareTo(str1[index]) == 0 ? 1 : 0;
        }
        if (num4 != 0)
          matching += num2;
      }
      num3 += num2;
    }
    if (matching > 0M && num3 != 1M)
      matching += 1M - num3;
    return matching;
  }

  public virtual Decimal EvaluateMatching(
    CABankTran aDetail,
    CATran aTran,
    IMatchSettings aSettings)
  {
    Decimal num1 = 0M;
    Decimal[] numArray1 = new Decimal[3]{ 0.1M, 0.7M, 0.2M };
    double sigma = 50.0;
    double meanValue = -7.0;
    if (aSettings != null)
    {
      Decimal? nullable = aSettings.DateCompareWeight;
      if (nullable.HasValue)
      {
        nullable = aSettings.RefNbrCompareWeight;
        if (nullable.HasValue)
        {
          nullable = aSettings.PayeeCompareWeight;
          if (nullable.HasValue)
          {
            nullable = aSettings.DateCompareWeight;
            Decimal num2 = nullable.Value;
            nullable = aSettings.RefNbrCompareWeight;
            Decimal num3 = nullable.Value;
            Decimal num4 = num2 + num3;
            nullable = aSettings.PayeeCompareWeight;
            Decimal num5 = nullable.Value;
            Decimal num6 = num4 + num5;
            if (num6 != 0M)
            {
              Decimal[] numArray2 = numArray1;
              nullable = aSettings.DateCompareWeight;
              Decimal num7 = nullable.Value / num6;
              numArray2[0] = num7;
              Decimal[] numArray3 = numArray1;
              nullable = aSettings.RefNbrCompareWeight;
              Decimal num8 = nullable.Value / num6;
              numArray3[1] = num8;
              Decimal[] numArray4 = numArray1;
              nullable = aSettings.PayeeCompareWeight;
              Decimal num9 = nullable.Value / num6;
              numArray4[2] = num9;
            }
          }
        }
      }
      nullable = aSettings.DateMeanOffset;
      if (nullable.HasValue)
      {
        nullable = aSettings.DateMeanOffset;
        meanValue = (double) nullable.Value;
      }
      nullable = aSettings.DateSigma;
      if (nullable.HasValue)
      {
        nullable = aSettings.DateSigma;
        sigma = (double) nullable.Value;
      }
    }
    bool looseCompare = false;
    return num1 + this.CompareDate(aDetail, aTran, meanValue, sigma) * numArray1[0] + this.CompareRefNbr(aDetail, aTran, looseCompare, aSettings) * numArray1[1] + this.ComparePayee(aDetail, aTran) * numArray1[2];
  }

  public virtual Decimal EvaluateMatching(
    CABankTran aDetail,
    CABankTranInvoiceMatch aTran,
    IMatchSettings aSettings)
  {
    Decimal num1 = 0M;
    Decimal[] numArray1 = new Decimal[3]{ 0.1M, 0.7M, 0.2M };
    double sigma = 50.0;
    double meanValue = -7.0;
    if (aSettings != null)
    {
      Decimal? nullable = aSettings.InvoiceDateCompareWeight;
      if (nullable.HasValue)
      {
        nullable = aSettings.InvoiceRefNbrCompareWeight;
        if (nullable.HasValue)
        {
          nullable = aSettings.InvoicePayeeCompareWeight;
          if (nullable.HasValue)
          {
            nullable = aSettings.InvoiceDateCompareWeight;
            Decimal num2 = nullable.Value;
            nullable = aSettings.InvoiceRefNbrCompareWeight;
            Decimal num3 = nullable.Value;
            Decimal num4 = num2 + num3;
            nullable = aSettings.InvoicePayeeCompareWeight;
            Decimal num5 = nullable.Value;
            Decimal num6 = num4 + num5;
            if (num6 != 0M)
            {
              Decimal[] numArray2 = numArray1;
              nullable = aSettings.InvoiceDateCompareWeight;
              Decimal num7 = nullable.Value / num6;
              numArray2[0] = num7;
              Decimal[] numArray3 = numArray1;
              nullable = aSettings.InvoiceRefNbrCompareWeight;
              Decimal num8 = nullable.Value / num6;
              numArray3[1] = num8;
              Decimal[] numArray4 = numArray1;
              nullable = aSettings.InvoicePayeeCompareWeight;
              Decimal num9 = nullable.Value / num6;
              numArray4[2] = num9;
            }
          }
        }
      }
      nullable = aSettings.AveragePaymentDelay;
      if (nullable.HasValue)
      {
        nullable = aSettings.AveragePaymentDelay;
        meanValue = (double) nullable.Value;
      }
      nullable = aSettings.InvoiceDateSigma;
      if (nullable.HasValue)
      {
        nullable = aSettings.InvoiceDateSigma;
        sigma = (double) nullable.Value;
      }
    }
    bool looseCompare = false;
    return num1 + this.CompareDate(aDetail, aTran, meanValue, sigma) * numArray1[0] + this.CompareRefNbr(aDetail, aTran, looseCompare) * numArray1[1] + this.ComparePayee(aDetail, aTran) * numArray1[2];
  }

  public virtual Decimal EvaluateMatching(
    CABankTran aDetail,
    CABankTranExpenseDetailMatch expenseMath,
    IMatchSettings aSettings)
  {
    Decimal num1 = 0M;
    Decimal[] numArray1 = new Decimal[3]{ 0.2M, 0.7M, 0.1M };
    double sigma = 50.0;
    double meanValue = -7.0;
    if (aSettings != null)
    {
      Decimal? nullable = aSettings.DateCompareWeight;
      if (nullable.HasValue)
      {
        nullable = aSettings.RefNbrCompareWeight;
        if (nullable.HasValue)
        {
          nullable = aSettings.AmountWeight;
          if (nullable.HasValue)
          {
            nullable = aSettings.DateCompareWeight;
            Decimal num2 = nullable.Value;
            nullable = aSettings.RefNbrCompareWeight;
            Decimal num3 = nullable.Value;
            Decimal num4 = num2 + num3;
            nullable = aSettings.AmountWeight;
            Decimal num5 = nullable.Value;
            Decimal num6 = num4 + num5;
            if (num6 != 0M)
            {
              Decimal[] numArray2 = numArray1;
              nullable = aSettings.DateCompareWeight;
              Decimal num7 = nullable.Value / num6;
              numArray2[0] = num7;
              Decimal[] numArray3 = numArray1;
              nullable = aSettings.RefNbrCompareWeight;
              Decimal num8 = nullable.Value / num6;
              numArray3[1] = num8;
              Decimal[] numArray4 = numArray1;
              nullable = aSettings.AmountWeight;
              Decimal num9 = nullable.Value / num6;
              numArray4[2] = num9;
            }
          }
        }
      }
      nullable = aSettings.DateMeanOffset;
      if (nullable.HasValue)
      {
        nullable = aSettings.DateMeanOffset;
        meanValue = (double) nullable.Value;
      }
      nullable = aSettings.DateSigma;
      if (nullable.HasValue)
      {
        nullable = aSettings.DateSigma;
        sigma = (double) nullable.Value;
      }
    }
    bool looseCompare = false;
    return num1 + this.CompareDate(aDetail, expenseMath, meanValue, sigma) * numArray1[0] + this.CompareRefNbr(aDetail, expenseMath, looseCompare, aSettings) * numArray1[1] + this.CompareExpenseReceiptAmount(aDetail, expenseMath, aSettings) * numArray1[2];
  }

  public virtual Decimal EvaluateTideMatching(
    string aStr1,
    string aStr2,
    bool aCaseSensitive,
    bool matchEmpty = true)
  {
    Decimal[] numArray = new Decimal[4]
    {
      1M,
      0.5M,
      0.25M,
      0.05M
    };
    if (string.IsNullOrEmpty(aStr1) || string.IsNullOrEmpty(aStr2))
      return this.MatchEmptyStrings(aStr1, aStr2, matchEmpty);
    string s1 = aStr1.Trim();
    string s2 = aStr2.Trim();
    long result1;
    long result2;
    if (long.TryParse(s1, out result1) && long.TryParse(s2, out result2))
      return result1 != result2 ? 0M : 1M;
    int num1 = Math.Max(s1.Length, s2.Length);
    if (num1 == 0)
      return 1M;
    if (Math.Abs(s1.Length - s2.Length) > 3)
      return 0M;
    int index1 = 0;
    for (int index2 = 0; index2 < num1; ++index2)
    {
      if (index2 < s1.Length && index2 < s2.Length)
      {
        char lower;
        int num2;
        if (!aCaseSensitive)
        {
          lower = char.ToLower(s2[index2]);
          num2 = lower.CompareTo(char.ToLower(s1[index2])) == 0 ? 1 : 0;
        }
        else
        {
          lower = s2[index2];
          num2 = lower.CompareTo(s1[index2]) == 0 ? 1 : 0;
        }
        if (num2 == 0)
          ++index1;
      }
      else
        ++index1;
      if (index1 > 3)
        return 0M;
    }
    return numArray[index1];
  }

  public virtual Decimal MatchEmptyStrings(string aStr1, string aStr2, bool matchEmpty = true)
  {
    return ((!string.IsNullOrEmpty(aStr1) ? 0 : (string.IsNullOrEmpty(aStr2) ? 1 : 0)) & (matchEmpty ? 1 : 0)) == 0 ? 0M : 1M;
  }

  public virtual Pair<DateTime, DateTime> GetDateRangeForMatch(
    CABankTran aDetail,
    IMatchSettings aSettings)
  {
    DateTime? tranEntryDate = aDetail.TranEntryDate;
    DateTime dateTime1 = tranEntryDate ?? aDetail.TranDate.Value;
    tranEntryDate = aDetail.TranEntryDate;
    DateTime dateTime2 = tranEntryDate ?? aDetail.TranDate.Value;
    bool flag = aDetail.DrCr == "D";
    ref DateTime local1 = ref dateTime1;
    int? nullable;
    int valueOrDefault1;
    if (!flag)
    {
      valueOrDefault1 = aSettings.DisbursementTranDaysBefore.GetValueOrDefault();
    }
    else
    {
      nullable = aSettings.ReceiptTranDaysBefore;
      valueOrDefault1 = nullable.GetValueOrDefault();
    }
    double num1 = (double) -valueOrDefault1;
    DateTime aFirst = local1.AddDays(num1);
    ref DateTime local2 = ref dateTime2;
    int valueOrDefault2;
    if (!flag)
    {
      nullable = aSettings.DisbursementTranDaysAfter;
      valueOrDefault2 = nullable.GetValueOrDefault();
    }
    else
    {
      nullable = aSettings.ReceiptTranDaysAfter;
      valueOrDefault2 = nullable.GetValueOrDefault();
    }
    double num2 = (double) valueOrDefault2;
    DateTime aSecond = local2.AddDays(num2);
    if (aSecond < aFirst)
    {
      DateTime dateTime3 = aFirst;
      aFirst = aSecond;
      aSecond = dateTime3;
    }
    return new Pair<DateTime, DateTime>(aFirst, aSecond);
  }

  /// <summary>
  /// Return dates for invoice matching:
  ///  1. first = Start of Due Date;
  ///  2. second = End of Due Date;
  ///  3. third = Last Discout Date;
  /// </summary>
  /// <param name="aDetail"></param>
  /// <param name="aSettings"></param>
  /// <returns></returns>
  public virtual Triplet<DateTime, DateTime, DateTime> GetInvoiceDateRangeForMatch(
    CABankTran aDetail,
    IMatchSettings aSettings)
  {
    DateTime? tranEntryDate = aDetail.TranEntryDate;
    DateTime dateTime1 = tranEntryDate ?? aDetail.TranDate.Value;
    tranEntryDate = aDetail.TranEntryDate;
    DateTime dateTime2 = tranEntryDate ?? aDetail.TranDate.Value;
    tranEntryDate = aDetail.TranEntryDate;
    DateTime dateTime3 = tranEntryDate ?? aDetail.TranDate.Value;
    ref DateTime local1 = ref dateTime1;
    int? nullable = aSettings.DaysAfterInvoiceDueDate;
    double valueOrDefault1 = (double) (nullable.HasValue ? new int?(-nullable.GetValueOrDefault()) : new int?()).GetValueOrDefault();
    DateTime aArg1 = local1.AddDays(valueOrDefault1);
    ref DateTime local2 = ref dateTime2;
    nullable = aSettings.DaysBeforeInvoiceDueDate;
    double valueOrDefault2 = (double) nullable.GetValueOrDefault();
    DateTime aArg2 = local2.AddDays(valueOrDefault2);
    ref DateTime local3 = ref dateTime3;
    nullable = aSettings.DaysBeforeInvoiceDiscountDate;
    double valueOrDefault3 = (double) nullable.GetValueOrDefault();
    DateTime aArg3 = local3.AddDays(valueOrDefault3);
    if (aArg2 < aArg1)
    {
      DateTime dateTime4 = aArg1;
      aArg1 = aArg2;
      aArg2 = dateTime4;
    }
    return new Triplet<DateTime, DateTime, DateTime>(aArg1, aArg2, aArg3);
  }
}
