// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankMatchingProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CA.BankStatementHelpers;
using PX.Objects.CA.BankStatementProtoHelpers;
using PX.Objects.CA.Repositories;
using PX.Objects.CA.Utility;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.CA;

public class CABankMatchingProcess : PXGraph<
#nullable disable
CABankMatchingProcess>, ICABankTransactionsDataProvider
{
  public PXSelect<PX.Objects.CA.CABankTran> CABankTran;
  public PXSelect<CABankTranMatch, Where<CABankTranMatch.matchType, Equal<CABankTranMatch.matchType.match>, And<CABankTranMatch.tranID, Equal<Required<PX.Objects.CA.CABankTran.tranID>>>>> TranMatch;
  public PXSelect<CABankTranMatch, Where<CABankTranMatch.matchType, Equal<CABankTranMatch.matchType.charge>, And<CABankTranMatch.tranID, Equal<Required<PX.Objects.CA.CABankTran.tranID>>>>> TranMatchCharge;
  public PXSelect<EPExpenseClaimDetails> ExpenseReceipts;
  public PXSelectJoin<CABankTranAdjustment, LeftJoin<PX.Objects.AR.ARInvoice, On<CABankTranAdjustment.adjdModule, Equal<BatchModule.moduleAR>, And<CABankTranAdjustment.adjdRefNbr, Equal<PX.Objects.AR.ARInvoice.refNbr>, And<CABankTranAdjustment.adjdDocType, Equal<PX.Objects.AR.ARInvoice.docType>>>>>, Where<CABankTranAdjustment.tranID, Equal<Required<PX.Objects.CA.CABankTran.tranID>>>> Adjustments;
  public PXSelect<PX.Objects.CA.CABankTran, Where<PX.Objects.CA.CABankTran.processed, Equal<False>, And<PX.Objects.CA.CABankTran.cashAccountID, Equal<Required<CashAccount.cashAccountID>>, And<PX.Objects.CA.CABankTran.documentMatched, Equal<False>>>>, OrderBy<Asc<PX.Objects.CA.CABankTran.curyTranAmt>>> UnMatchedDetails;
  public PXSelect<PX.Objects.CA.CABankTran, Where<PX.Objects.CA.CABankTran.tranID, Equal<Required<PX.Objects.CA.CABankTran.tranID>>>> CABankTranSelection;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CA.CABankTran.curyInfoID>>>> currencyinfo;
  public PXSelect<CABankTranDetail, Where<CABankTranDetail.bankTranID, Equal<Required<PX.Objects.CA.CABankTran.tranID>>>> TranSplit;
  public PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>> cashAccount;
  public FbqlSelect<SelectFromBase<PX.Objects.TX.TaxZone, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PX.Objects.TX.TaxZone.taxZoneID, IBqlString>.IsEqual<
  #nullable disable
  P.AsString>>, PX.Objects.TX.TaxZone>.View Taxzone;
  public FbqlSelect<SelectFromBase<CABankTaxTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.TX.Tax>.On<BqlOperand<
  #nullable enable
  PX.Objects.TX.Tax.taxID, IBqlString>.IsEqual<
  #nullable disable
  CABankTaxTran.taxID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CABankTaxTran.bankTranID, 
  #nullable disable
  Equal<P.AsInt>>>>>.And<BqlOperand<
  #nullable enable
  CABankTaxTran.bankTranType, IBqlString>.IsEqual<
  #nullable disable
  P.AsString>>>, CABankTaxTran>.View TaxTrans;
  [PXHidden]
  public PXSelect<CABankTranRule, Where<CABankTranRule.isActive, Equal<True>>> Rules;
  public PXSetup<PX.Objects.CA.CASetup> CASetup;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;
  [PXHidden]
  public PXSelect<CAMatchProcess, Where<CAMatchProcess.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>> MatchProcess;
  [PXHidden]
  public PXSelectReadonly<CAMatchProcess, Where<CAMatchProcess.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>> MatchProcessSelect;
  protected Dictionary<object, List<CABankTranInvoiceMatch>> matchingInvoices;
  protected Dictionary<object, List<PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>>> matchingTrans;
  protected Dictionary<object, List<CABankTranExpenseDetailMatch>> matchingExpenseReceiptDetails;
  public const Decimal RelevanceTreshold = 0.2M;

  [InjectDependency]
  public ICABankTransactionsRepository CABankTransactionsRepository { get; set; }

  [InjectDependency]
  public IMatchingService MatchingService { get; set; }

  public virtual void DoMatch(CABankTransactionsMaint graph, int? cashAccountID)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABankMatchingProcess.\u003C\u003Ec__DisplayClass25_0 cDisplayClass250 = new CABankMatchingProcess.\u003C\u003Ec__DisplayClass25_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass250.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass250.graph = graph;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass250.tranList = GraphHelper.RowCast<PX.Objects.CA.CABankTran>((IEnumerable) ((PXSelectBase<PX.Objects.CA.CABankTran>) this.UnMatchedDetails).Select(new object[1]
    {
      (object) cashAccountID
    }));
    // ISSUE: reference to a compiler-generated field
    cDisplayClass250.cashAcc = ((PXSelectBase<CashAccount>) this.cashAccount).SelectSingle(new object[1]
    {
      (object) cashAccountID
    });
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    PXLongOperation.StartOperation(((PXGraph) cDisplayClass250.graph).UID, new PXToggleAsyncDelegate((object) cDisplayClass250, __methodptr(\u003CDoMatch\u003Eb__0)));
  }

  public virtual void DoMatch(
    IEnumerable<PX.Objects.CA.CABankTran> aRows,
    CashAccount cashAccount,
    Guid? processorID)
  {
    using (new CAMatchProcessContext(this, cashAccount.CashAccountID, processorID))
    {
      this.ClearCachedMatches();
      foreach (IEnumerable<PX.Objects.CA.CABankTran> aRows1 in (IEnumerable<IGrouping<Decimal?, PX.Objects.CA.CABankTran>>) aRows.GroupBy<PX.Objects.CA.CABankTran, Decimal?>((Func<PX.Objects.CA.CABankTran, Decimal?>) (d => d.CuryTranAmt)).OrderBy<IGrouping<Decimal?, PX.Objects.CA.CABankTran>, int>((Func<IGrouping<Decimal?, PX.Objects.CA.CABankTran>, int>) (s => s.Count<PX.Objects.CA.CABankTran>())))
      {
        this.DoMatchingOperations(aRows1, cashAccount);
        ((PXGraph) this).Persist();
        this.ClearCachedMatches();
      }
    }
  }

  protected virtual void DoMatchingOperations(
    IEnumerable<PX.Objects.CA.CABankTran> aRows,
    CashAccount cashAccount)
  {
    this.DoCAMatch(aRows, cashAccount);
    this.DoInvMatch(aRows, cashAccount);
    this.DoExpenseReceiptMatch(aRows, cashAccount);
    this.DoDocumentCreation(aRows, cashAccount);
  }

  protected virtual List<PX.Objects.CA.CABankTran> DoCAMatchInner(
    IEnumerable<PX.Objects.CA.CABankTran> aRows,
    CashAccount cashAccount)
  {
    Dictionary<string, List<CABankTranDocRef>> cross = new Dictionary<string, List<CABankTranDocRef>>();
    Dictionary<int, PX.Objects.CA.CABankTran> rows = new Dictionary<int, PX.Objects.CA.CABankTran>();
    int num1 = 0;
    Decimal num2 = cashAccount.MatchThreshold.GetValueOrDefault() / 100M;
    Decimal num3 = cashAccount.RelativeMatchThreshold.GetValueOrDefault() / 100M;
    foreach (PX.Objects.CA.CABankTran aRow in aRows)
    {
      ++num1;
      PX.Objects.CA.CABankTran caBankTran = aRow as PX.Objects.CA.CABankTran;
      if (this.PaymentMatchingAllowed(caBankTran))
      {
        bool? nullable1 = caBankTran.DocumentMatched;
        if (!nullable1.GetValueOrDefault())
        {
          nullable1 = caBankTran.CreateDocument;
          if (nullable1.GetValueOrDefault())
          {
            if (!(caBankTran.OrigModule != "CA"))
            {
              DateTime? tranDate = caBankTran.TranDate;
              DateTime? matchingPaymentDate = caBankTran.MatchingPaymentDate;
              if ((tranDate.HasValue == matchingPaymentDate.HasValue ? (tranDate.HasValue ? (tranDate.GetValueOrDefault() != matchingPaymentDate.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0 || caBankTran.EntryTypeID != null)
                continue;
            }
            else
              continue;
          }
          if (!rows.ContainsKey(caBankTran.TranID.Value))
            rows.Add(caBankTran.TranID.Value, caBankTran);
          PX.Objects.CA.BankStatementHelpers.CATranExt[] aBestMatches = new PX.Objects.CA.BankStatementHelpers.CATranExt[2];
          List<PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>> detailMatches = (List<PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>>) this.FindDetailMatches(this, caBankTran, cashAccount, aBestMatches);
          if (aBestMatches[0] != null)
          {
            Decimal? nullable2;
            if (aBestMatches[1] == null)
            {
              nullable2 = aBestMatches[0].MatchRelevance;
              Decimal num4 = num3;
              if (nullable2.GetValueOrDefault() >= num4 & nullable2.HasValue)
                goto label_15;
            }
            if (aBestMatches[1] != null)
            {
              Decimal? matchRelevance1 = aBestMatches[0].MatchRelevance;
              Decimal? matchRelevance2 = aBestMatches[1].MatchRelevance;
              nullable2 = matchRelevance1.HasValue & matchRelevance2.HasValue ? new Decimal?(matchRelevance1.GetValueOrDefault() - matchRelevance2.GetValueOrDefault()) : new Decimal?();
              Decimal num5 = num3;
              if (nullable2.GetValueOrDefault() > num5 & nullable2.HasValue)
                goto label_15;
            }
            nullable2 = aBestMatches[0].MatchRelevance;
            Decimal num6 = num2;
            if (!(nullable2.GetValueOrDefault() > num6 & nullable2.HasValue))
              continue;
label_15:
            PX.Objects.CA.BankStatementHelpers.CATranExt aSrc = aBestMatches[0];
            CABankTranDocRef caBankTranDocRef = new CABankTranDocRef();
            caBankTranDocRef.Copy(caBankTran);
            caBankTranDocRef.Copy((CATran) aSrc);
            long? nullable3 = caBankTranDocRef.CATranID;
            if (!nullable3.HasValue)
            {
              caBankTranDocRef.DocModule = aSrc.OrigModule;
              caBankTranDocRef.DocRefNbr = aSrc.OrigRefNbr;
              caBankTranDocRef.DocType = aSrc.OrigTranType;
            }
            caBankTranDocRef.MatchRelevance = aSrc.MatchRelevance;
            nullable3 = aSrc.TranID;
            string key;
            if (nullable3.HasValue)
            {
              nullable3 = aSrc.TranID;
              key = nullable3.Value.ToString();
            }
            else
              key = aSrc.OrigModule + aSrc.OrigTranType + aSrc.OrigRefNbr;
            if (!cross.ContainsKey(key))
              cross.Add(key, new List<CABankTranDocRef>());
            cross[key].Add(caBankTranDocRef);
          }
        }
      }
    }
    return this.DoMatch(aRows, cross, rows);
  }

  public virtual void DoCAMatch(IEnumerable<PX.Objects.CA.CABankTran> aRows, CashAccount cashAccount)
  {
    List<PX.Objects.CA.CABankTran> aRows1 = new List<PX.Objects.CA.CABankTran>();
    aRows1.AddRange(aRows);
    while (aRows1.Count > 0)
      aRows1 = this.DoCAMatchInner((IEnumerable<PX.Objects.CA.CABankTran>) aRows1, cashAccount);
    foreach (PX.Objects.CA.CABankTran aRow in aRows)
    {
      if (this.PaymentMatchingAllowed(aRow))
      {
        bool? nullable = aRow.DocumentMatched;
        bool flag1 = false;
        if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
        {
          nullable = aRow.CreateDocument;
          bool flag2 = false;
          if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
          {
            if (aRow.OrigModule == "CA")
            {
              DateTime? tranDate = aRow.TranDate;
              DateTime? matchingPaymentDate = aRow.MatchingPaymentDate;
              if ((tranDate.HasValue == matchingPaymentDate.HasValue ? (tranDate.HasValue ? (tranDate.GetValueOrDefault() == matchingPaymentDate.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0 || aRow.EntryTypeID != null)
                continue;
            }
            else
              continue;
          }
          int? countMatches = aRow.CountMatches;
          int num = 0;
          if (countMatches.GetValueOrDefault() > num & countMatches.HasValue)
          {
            PX.Objects.CA.BankStatementHelpers.CATranExt[] aBestMatches = new PX.Objects.CA.BankStatementHelpers.CATranExt[2];
            this.FindDetailMatches(this, aRow, cashAccount, aBestMatches);
          }
        }
      }
    }
  }

  protected virtual bool PaymentMatchingAllowed(PX.Objects.CA.CABankTran transaction)
  {
    return CABankTranOperations.Verify.PaymentMatchingAllowed(transaction, CABankTranOperations.MatchingType.AutoMatch);
  }

  protected virtual bool InvoiceMatchingAllowed(PX.Objects.CA.CABankTran transaction)
  {
    return CABankTranOperations.Verify.InvoiceMatchingAllowed(transaction, CABankTranOperations.MatchingType.AutoMatch);
  }

  protected virtual bool ExpenseReceiptMatchingAllowed(PX.Objects.CA.CABankTran transaction)
  {
    return CABankTranOperations.Verify.ExpenseReceiptMatchingAllowed(transaction, CABankTranOperations.MatchingType.AutoMatch);
  }

  protected virtual bool DocumentCreationAllowed(PX.Objects.CA.CABankTran transaction)
  {
    return CABankTranOperations.Verify.DocumentCreationAllowed(transaction, CABankTranOperations.MatchingType.AutoMatch);
  }

  protected virtual List<PX.Objects.CA.CABankTran> DoDocumentMatchInner<TMatchRow>(
    IEnumerable<PX.Objects.CA.CABankTran> aRows,
    CashAccount cashAccount,
    Func<CABankMatchingProcess, PX.Objects.CA.CABankTran, CashAccount, Decimal, TMatchRow[], IEnumerable> findDetailMatching)
    where TMatchRow : CABankTranDocumentMatch
  {
    CashAccount cashAccount1 = cashAccount;
    Dictionary<string, List<CABankTranDocRef>> cross = new Dictionary<string, List<CABankTranDocRef>>();
    Dictionary<int, PX.Objects.CA.CABankTran> rows = new Dictionary<int, PX.Objects.CA.CABankTran>();
    Decimal num1 = cashAccount1.MatchThreshold.GetValueOrDefault() / 100M;
    Decimal num2 = cashAccount1.RelativeMatchThreshold.GetValueOrDefault() / 100M;
    foreach (PX.Objects.CA.CABankTran aRow in aRows)
    {
      PX.Objects.CA.CABankTran caBankTran1 = aRow as PX.Objects.CA.CABankTran;
      if ((!(typeof (TMatchRow) == typeof (CABankTranInvoiceMatch)) || this.InvoiceMatchingAllowed(caBankTran1)) && (!(typeof (TMatchRow) == typeof (CABankTranExpenseDetailMatch)) || this.ExpenseReceiptMatchingAllowed(caBankTran1)) && !caBankTran1.DocumentMatched.GetValueOrDefault())
      {
        if (caBankTran1.CreateDocument.GetValueOrDefault())
        {
          if (!(caBankTran1.OrigModule != "CA"))
          {
            DateTime? tranDate = caBankTran1.TranDate;
            DateTime? matchingPaymentDate = caBankTran1.MatchingPaymentDate;
            if ((tranDate.HasValue == matchingPaymentDate.HasValue ? (tranDate.HasValue ? (tranDate.GetValueOrDefault() != matchingPaymentDate.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0 || caBankTran1.EntryTypeID != null)
              continue;
          }
          else
            continue;
        }
        Dictionary<int, PX.Objects.CA.CABankTran> dictionary1 = rows;
        int? tranId = caBankTran1.TranID;
        int key1 = tranId.Value;
        if (!dictionary1.ContainsKey(key1))
        {
          Dictionary<int, PX.Objects.CA.CABankTran> dictionary2 = rows;
          tranId = caBankTran1.TranID;
          int key2 = tranId.Value;
          PX.Objects.CA.CABankTran caBankTran2 = caBankTran1;
          dictionary2.Add(key2, caBankTran2);
        }
        TMatchRow[] matchRowArray = new TMatchRow[2];
        IEnumerable enumerable = findDetailMatching(this, caBankTran1, cashAccount, 0M, matchRowArray);
        if ((object) matchRowArray[0] != null)
        {
          Decimal? nullable;
          if ((object) matchRowArray[1] == null)
          {
            nullable = matchRowArray[0].MatchRelevance;
            Decimal num3 = num2;
            if (nullable.GetValueOrDefault() >= num3 & nullable.HasValue)
              goto label_14;
          }
          if ((object) matchRowArray[1] != null)
          {
            Decimal? matchRelevance1 = matchRowArray[0].MatchRelevance;
            Decimal? matchRelevance2 = matchRowArray[1].MatchRelevance;
            nullable = matchRelevance1.HasValue & matchRelevance2.HasValue ? new Decimal?(matchRelevance1.GetValueOrDefault() - matchRelevance2.GetValueOrDefault()) : new Decimal?();
            Decimal num4 = num2;
            if (nullable.GetValueOrDefault() > num4 & nullable.HasValue)
              goto label_14;
          }
          nullable = matchRowArray[0].MatchRelevance;
          Decimal num5 = num1;
          if (!(nullable.GetValueOrDefault() > num5 & nullable.HasValue))
            continue;
label_14:
          TMatchRow matchRow = matchRowArray[0];
          CABankTranDocRef docRef = new CABankTranDocRef();
          docRef.Copy(caBankTran1);
          matchRow.BuildDocRef(docRef);
          docRef.MatchRelevance = matchRow.MatchRelevance;
          string documentKey = matchRow.GetDocumentKey();
          if (!cross.ContainsKey(documentKey))
            cross.Add(documentKey, new List<CABankTranDocRef>());
          cross[documentKey].Add(docRef);
        }
      }
    }
    return this.DoMatch(aRows, cross, rows);
  }

  public virtual void DoInvMatch(IEnumerable<PX.Objects.CA.CABankTran> aRows, CashAccount cashAccount)
  {
    List<PX.Objects.CA.CABankTran> aRows1 = new List<PX.Objects.CA.CABankTran>();
    aRows1.AddRange(aRows);
    while (aRows1.Count > 0)
      aRows1 = this.DoDocumentMatchInner<CABankTranInvoiceMatch>((IEnumerable<PX.Objects.CA.CABankTran>) aRows1, cashAccount, new Func<CABankMatchingProcess, PX.Objects.CA.CABankTran, CashAccount, Decimal, CABankTranInvoiceMatch[], IEnumerable>(this.FindDetailMatchingInvoices));
    foreach (PX.Objects.CA.CABankTran aRow in aRows)
    {
      if (this.InvoiceMatchingAllowed(aRow))
      {
        bool? nullable = aRow.DocumentMatched;
        bool flag1 = false;
        if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
        {
          nullable = aRow.CreateDocument;
          bool flag2 = false;
          if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
          {
            if (aRow.OrigModule == "CA")
            {
              DateTime? tranDate = aRow.TranDate;
              DateTime? matchingPaymentDate = aRow.MatchingPaymentDate;
              if ((tranDate.HasValue == matchingPaymentDate.HasValue ? (tranDate.HasValue ? (tranDate.GetValueOrDefault() == matchingPaymentDate.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0 || aRow.EntryTypeID != null)
                continue;
            }
            else
              continue;
          }
          int? countInvoiceMatches = aRow.CountInvoiceMatches;
          int num = 0;
          if (countInvoiceMatches.GetValueOrDefault() > num & countInvoiceMatches.HasValue)
          {
            this.matchingInvoices.Remove((object) aRow);
            this.FindDetailMatchingInvoices(this, aRow, cashAccount);
          }
        }
      }
    }
  }

  public virtual void DoExpenseReceiptMatch(IEnumerable<PX.Objects.CA.CABankTran> aRows, CashAccount cashAccount)
  {
    List<PX.Objects.CA.CABankTran> aRows1 = new List<PX.Objects.CA.CABankTran>();
    aRows1.AddRange(aRows);
    while (aRows1.Count > 0)
      aRows1 = this.DoDocumentMatchInner<CABankTranExpenseDetailMatch>((IEnumerable<PX.Objects.CA.CABankTran>) aRows1, cashAccount, (Func<CABankMatchingProcess, PX.Objects.CA.CABankTran, CashAccount, Decimal, CABankTranExpenseDetailMatch[], IEnumerable>) ((aGraph, bankTran, settings, relevanceTreshold, bestMatches) => (IEnumerable) this.GetExpenseReceiptDetailMatches(aGraph, bankTran, settings, relevanceTreshold, bestMatches)));
    foreach (PX.Objects.CA.CABankTran aRow in aRows)
    {
      if (this.ExpenseReceiptMatchingAllowed(aRow))
      {
        bool? nullable = aRow.DocumentMatched;
        bool flag1 = false;
        if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
        {
          nullable = aRow.CreateDocument;
          bool flag2 = false;
          if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
          {
            if (aRow.OrigModule == "CA")
            {
              DateTime? tranDate = aRow.TranDate;
              DateTime? matchingPaymentDate = aRow.MatchingPaymentDate;
              if ((tranDate.HasValue == matchingPaymentDate.HasValue ? (tranDate.HasValue ? (tranDate.GetValueOrDefault() == matchingPaymentDate.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0 || aRow.EntryTypeID != null)
                continue;
            }
            else
              continue;
          }
          int? receiptDetailMatches = aRow.CountExpenseReceiptDetailMatches;
          int num = 0;
          if (receiptDetailMatches.GetValueOrDefault() > num & receiptDetailMatches.HasValue)
          {
            this.matchingExpenseReceiptDetails.Remove((object) aRow);
            this.FindExpenseReceiptDetailMatches(this, aRow, cashAccount);
          }
        }
      }
    }
  }

  public virtual void DoDocumentCreation(IEnumerable<PX.Objects.CA.CABankTran> aRows, CashAccount cashAccount)
  {
    foreach (PX.Objects.CA.CABankTran aRow in aRows)
    {
      if (this.DocumentCreationAllowed(aRow))
      {
        bool? nullable1 = aRow.DocumentMatched;
        if (!nullable1.GetValueOrDefault())
        {
          nullable1 = aRow.CreateDocument;
          if (nullable1.GetValueOrDefault())
          {
            if (aRow.OrigModule == "CA")
            {
              DateTime? tranDate = aRow.TranDate;
              DateTime? matchingPaymentDate = aRow.MatchingPaymentDate;
              if ((tranDate.HasValue == matchingPaymentDate.HasValue ? (tranDate.HasValue ? (tranDate.GetValueOrDefault() == matchingPaymentDate.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0 || aRow.EntryTypeID != null)
                continue;
            }
            else
              continue;
          }
          int? nullable2 = aRow.CountMatches;
          if (nullable2.HasValue)
          {
            nullable2 = aRow.CountMatches;
            int num1 = 0;
            if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
            {
              nullable2 = aRow.CountInvoiceMatches;
              if (nullable2.HasValue)
              {
                nullable2 = aRow.CountInvoiceMatches;
                int num2 = 0;
                if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
                {
                  nullable2 = aRow.CountExpenseReceiptDetailMatches;
                  if (nullable2.HasValue)
                  {
                    nullable2 = aRow.CountExpenseReceiptDetailMatches;
                    int num3 = 0;
                    if (!(nullable2.GetValueOrDefault() == num3 & nullable2.HasValue))
                      continue;
                  }
                  nullable1 = aRow.CreateDocument;
                  if (nullable1.GetValueOrDefault())
                  {
                    aRow.CreateDocument = new bool?(false);
                    ((PXSelectBase) this.CABankTran).Cache.Update((object) aRow);
                  }
                  aRow.CreateDocument = new bool?(true);
                  ((PXSelectBase<CashAccount>) this.cashAccount).Current = cashAccount;
                  ((PXSelectBase) this.CABankTran).Cache.Update((object) aRow);
                  this.CreateDocument(aRow, cashAccount);
                }
              }
            }
          }
        }
      }
    }
  }

  public virtual List<PX.Objects.CA.CABankTran> DoMatch(
    IEnumerable<PX.Objects.CA.CABankTran> aRows,
    Dictionary<string, List<CABankTranDocRef>> cross,
    Dictionary<int, PX.Objects.CA.CABankTran> rows)
  {
    Dictionary<int, PX.Objects.CA.CABankTran> dictionary1 = new Dictionary<int, PX.Objects.CA.CABankTran>();
    foreach (KeyValuePair<string, List<CABankTranDocRef>> keyValuePair in cross)
    {
      CABankTranDocRef docRef = (CABankTranDocRef) null;
      foreach (CABankTranDocRef caBankTranDocRef in keyValuePair.Value)
      {
        if (docRef != null)
        {
          Decimal? matchRelevance1 = docRef.MatchRelevance;
          Decimal? matchRelevance2 = caBankTranDocRef.MatchRelevance;
          if (!(matchRelevance1.GetValueOrDefault() < matchRelevance2.GetValueOrDefault() & matchRelevance1.HasValue & matchRelevance2.HasValue))
            continue;
        }
        docRef = caBankTranDocRef;
      }
      if (docRef != null && docRef.TranID.HasValue)
      {
        Dictionary<int, PX.Objects.CA.CABankTran> dictionary2 = rows;
        int? tranId = docRef.TranID;
        int key1 = tranId.Value;
        PX.Objects.CA.CABankTran caBankTran1;
        ref PX.Objects.CA.CABankTran local = ref caBankTran1;
        if (!dictionary2.TryGetValue(key1, out local))
        {
          caBankTran1 = PXResultset<PX.Objects.CA.CABankTran>.op_Implicit(((PXSelectBase<PX.Objects.CA.CABankTran>) this.CABankTran).SearchAll<Asc<PX.Objects.CA.CABankTran.tranID, Asc<PX.Objects.CA.CABankTran.tranID>>>(new object[1]
          {
            (object) docRef.TranID
          }, Array.Empty<object>()));
          Dictionary<int, PX.Objects.CA.CABankTran> dictionary3 = rows;
          tranId = caBankTran1.TranID;
          int key2 = tranId.Value;
          PX.Objects.CA.CABankTran caBankTran2 = caBankTran1;
          dictionary3.Add(key2, caBankTran2);
        }
        if (caBankTran1 != null)
        {
          if (((PXSelectBase<CABankTranMatch>) this.TranMatch).Select(new object[1]
          {
            (object) caBankTran1.TranID
          }).Count == 0)
          {
            CABankTranMatch caBankTranMatch1 = new CABankTranMatch()
            {
              TranID = caBankTran1.TranID,
              TranType = caBankTran1.TranType
            };
            caBankTranMatch1.Copy(docRef);
            if (caBankTran1.DrCr == "C" && (caBankTranMatch1.CATranID.HasValue || caBankTranMatch1.DocType == "CBT"))
            {
              CABankTranMatch caBankTranMatch2 = caBankTranMatch1;
              Decimal num = (Decimal) -1;
              Decimal? curyApplAmt = caBankTranMatch1.CuryApplAmt;
              Decimal? nullable = curyApplAmt.HasValue ? new Decimal?(num * curyApplAmt.GetValueOrDefault()) : new Decimal?();
              caBankTranMatch2.CuryApplAmt = nullable;
            }
            caBankTranMatch1.MatchType = "M";
            ((PXSelectBase<CABankTranMatch>) this.TranMatch).Insert(caBankTranMatch1);
            caBankTran1.CreateDocument = new bool?(false);
            caBankTran1 = ((PXSelectBase) this.CABankTran).Cache.Update((object) caBankTran1) as PX.Objects.CA.CABankTran;
            caBankTran1.DocumentMatched = new bool?(true);
            ((PXSelectBase) this.CABankTran).Cache.Update((object) caBankTran1);
            if (caBankTranMatch1.DocModule == "EP" && caBankTranMatch1.DocType == "ECD")
            {
              EPExpenseClaimDetails expenseClaimDetails = PXResultset<EPExpenseClaimDetails>.op_Implicit(PXSelectBase<EPExpenseClaimDetails, PXSelect<EPExpenseClaimDetails, Where<EPExpenseClaimDetails.claimDetailCD, Equal<Required<EPExpenseClaimDetails.claimDetailCD>>>>.Config>.Select((PXGraph) this, new object[1]
              {
                (object) caBankTranMatch1.DocRefNbr
              }));
              expenseClaimDetails.BankTranDate = caBankTran1.TranDate;
              ((PXSelectBase<EPExpenseClaimDetails>) this.ExpenseReceipts).Update(expenseClaimDetails);
            }
          }
        }
        Dictionary<int, PX.Objects.CA.CABankTran> dictionary4 = dictionary1;
        tranId = docRef.TranID;
        int key3 = tranId.Value;
        dictionary4.Remove(key3);
        foreach (CABankTranDocRef caBankTranDocRef in keyValuePair.Value)
        {
          if (caBankTranDocRef != docRef)
          {
            Dictionary<int, PX.Objects.CA.CABankTran> dictionary5 = dictionary1;
            tranId = caBankTranDocRef.TranID;
            int key4 = tranId.Value;
            dictionary5[key4] = (PX.Objects.CA.CABankTran) null;
          }
        }
      }
    }
    cross.Clear();
    List<PX.Objects.CA.CABankTran> caBankTranList = new List<PX.Objects.CA.CABankTran>(dictionary1.Keys.Count);
    foreach (KeyValuePair<int, PX.Objects.CA.CABankTran> keyValuePair in dictionary1)
    {
      PX.Objects.CA.CABankTran caBankTran;
      if (!rows.TryGetValue(keyValuePair.Key, out caBankTran))
      {
        caBankTran = PXResultset<PX.Objects.CA.CABankTran>.op_Implicit(((PXSelectBase<PX.Objects.CA.CABankTran>) this.CABankTran).SearchAll<Asc<PX.Objects.CA.CABankTran.tranID, Asc<PX.Objects.CA.CABankTran.tranID>>>(new object[1]
        {
          (object) keyValuePair.Key
        }, Array.Empty<object>()));
        rows.Add(caBankTran.TranID.Value, caBankTran);
      }
      if (caBankTran != null)
        caBankTranList.Add(caBankTran);
    }
    return caBankTranList;
  }

  public virtual IEnumerable FindDetailMatches(
    CABankMatchingProcess graph,
    PX.Objects.CA.CABankTran aDetail,
    CashAccount cashAccount,
    PX.Objects.CA.BankStatementHelpers.CATranExt[] aBestMatches)
  {
    List<PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>> detailMatches = (List<PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>>) null;
    if (this.matchingTrans == null)
      this.matchingTrans = new Dictionary<object, List<PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>>>();
    if (aBestMatches == null && this.matchingTrans.TryGetValue((object) aDetail, out detailMatches))
    {
      aDetail.CountMatches = new int?(detailMatches.Count);
      if (aDetail.MatchedToExisting.GetValueOrDefault() && detailMatches.Find((Predicate<PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>>) (result => PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>.op_Implicit(result).IsMatched.GetValueOrDefault())) == null)
        detailMatches.ForEach((Action<PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>>) (result =>
        {
          PX.Objects.CA.BankStatementHelpers.CATranExt caTranExt1 = PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>.op_Implicit(result);
          PX.Objects.CA.BankStatementHelpers.CATranExt caTranExt2 = caTranExt1;
          int num;
          if (PXSelectBase<CABankTranMatch, PXSelect<CABankTranMatch, Where<CABankTranMatch.tranID, Equal<Required<CABankTranMatch.tranID>>, And<CABankTranMatch.cATranID, Equal<Required<CABankTranMatch.cATranID>>>>>.Config>.Select((PXGraph) graph, new object[2]
          {
            (object) aDetail.TranID,
            (object) caTranExt1.TranID
          }).Count <= 0)
          {
            if (caTranExt1.OrigModule == "AP" && caTranExt1.OrigTranType == "CBT")
              num = PXSelectBase<CABankTranMatch, PXSelect<CABankTranMatch, Where<CABankTranMatch.tranID, Equal<Required<CABankTranMatch.tranID>>, And<CABankTranMatch.docRefNbr, Equal<Required<CABankTranMatch.docRefNbr>>, And<CABankTranMatch.docModule, Equal<Required<CABankTranMatch.docModule>>, And<CABankTranMatch.docType, Equal<Required<CABankTranMatch.docType>>>>>>>.Config>.Select((PXGraph) graph, new object[4]
              {
                (object) aDetail.TranID,
                (object) caTranExt1.OrigRefNbr,
                (object) caTranExt1.OrigModule,
                (object) caTranExt1.OrigTranType
              }).Count > 0 ? 1 : 0;
            else
              num = 0;
          }
          else
            num = 1;
          bool? nullable = new bool?(num != 0);
          caTranExt2.IsMatched = nullable;
        }));
      return (IEnumerable) detailMatches;
    }
    Decimal aRelevanceTreshold = 0M;
    detailMatches = (List<PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>>) this.MatchingService.FindDetailMatches<CABankMatchingProcess>(graph, aDetail, (IMatchSettings) cashAccount, aBestMatches, aRelevanceTreshold);
    this.matchingTrans[(object) aDetail] = detailMatches;
    return (IEnumerable) detailMatches;
  }

  public virtual IEnumerable FindDetailMatchingInvoices(
    CABankMatchingProcess graph,
    PX.Objects.CA.CABankTran aDetail,
    CashAccount cashAccount)
  {
    Decimal aRelevanceTreshold = 0M;
    return (IEnumerable) CABankMatchingProcess.FindDetailMatchingInvoicesProc((PXGraph) graph, aDetail, cashAccount, aRelevanceTreshold, (CABankTranInvoiceMatch[]) null);
  }

  public virtual IList<CABankTranExpenseDetailMatch> FindExpenseReceiptDetailMatches(
    CABankMatchingProcess graph,
    PX.Objects.CA.CABankTran detail,
    CashAccount cashAccount)
  {
    Decimal relevanceTreshold = 0M;
    return (IList<CABankTranExpenseDetailMatch>) this.GetExpenseReceiptDetailMatches(graph, detail, cashAccount, relevanceTreshold, (CABankTranExpenseDetailMatch[]) null);
  }

  public virtual IEnumerable FindDetailMatchingInvoices(
    CABankMatchingProcess graph,
    PX.Objects.CA.CABankTran aDetail,
    CashAccount cashAccount,
    Decimal aRelevanceTreshold,
    CABankTranInvoiceMatch[] aBestMatches)
  {
    List<CABankTranInvoiceMatch> matchingInvoices = (List<CABankTranInvoiceMatch>) null;
    if (this.matchingInvoices == null)
      this.matchingInvoices = new Dictionary<object, List<CABankTranInvoiceMatch>>(1);
    if (aBestMatches == null && this.matchingInvoices.TryGetValue((object) aDetail, out matchingInvoices))
    {
      aDetail.CountInvoiceMatches = new int?(matchingInvoices.Count);
      return (IEnumerable) matchingInvoices;
    }
    List<CABankTranInvoiceMatch> matchingInvoicesProc = CABankMatchingProcess.FindDetailMatchingInvoicesProc((PXGraph) graph, aDetail, cashAccount, aRelevanceTreshold, aBestMatches);
    this.matchingInvoices[(object) aDetail] = matchingInvoicesProc;
    aDetail.CountInvoiceMatches = new int?(matchingInvoicesProc.Count);
    return (IEnumerable) matchingInvoicesProc;
  }

  public static CABankTranInvoiceMatch CreateCABankTranInvoiceMatch(
    PX.Objects.CA.Light.ARInvoice doc,
    PX.Objects.CA.Light.BAccount bAccount,
    PX.Objects.CA.CABankTran aDetail)
  {
    CABankTranInvoiceMatch tranInvoiceMatch1 = new CABankTranInvoiceMatch();
    tranInvoiceMatch1.Copy(doc);
    tranInvoiceMatch1.ReferenceCD = bAccount.AcctCD;
    tranInvoiceMatch1.ReferenceName = bAccount.AcctName;
    if (tranInvoiceMatch1.DrCr != aDetail.DrCr)
    {
      CABankTranInvoiceMatch tranInvoiceMatch2 = tranInvoiceMatch1;
      Decimal num1 = (Decimal) -1;
      Decimal? nullable1 = tranInvoiceMatch1.CuryTranAmt;
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?(num1 * nullable1.GetValueOrDefault()) : new Decimal?();
      tranInvoiceMatch2.CuryTranAmt = nullable2;
      CABankTranInvoiceMatch tranInvoiceMatch3 = tranInvoiceMatch1;
      Decimal num2 = (Decimal) -1;
      nullable1 = tranInvoiceMatch1.TranAmt;
      Decimal? nullable3 = nullable1.HasValue ? new Decimal?(num2 * nullable1.GetValueOrDefault()) : new Decimal?();
      tranInvoiceMatch3.TranAmt = nullable3;
      CABankTranInvoiceMatch tranInvoiceMatch4 = tranInvoiceMatch1;
      Decimal num3 = (Decimal) -1;
      nullable1 = tranInvoiceMatch1.CuryDiscAmt;
      Decimal? nullable4 = nullable1.HasValue ? new Decimal?(num3 * nullable1.GetValueOrDefault()) : new Decimal?();
      tranInvoiceMatch4.CuryDiscAmt = nullable4;
      CABankTranInvoiceMatch tranInvoiceMatch5 = tranInvoiceMatch1;
      Decimal num4 = (Decimal) -1;
      nullable1 = tranInvoiceMatch1.DiscAmt;
      Decimal? nullable5 = nullable1.HasValue ? new Decimal?(num4 * nullable1.GetValueOrDefault()) : new Decimal?();
      tranInvoiceMatch5.DiscAmt = nullable5;
    }
    return tranInvoiceMatch1;
  }

  public static CABankTranInvoiceMatch CreateCABankTranInvoiceMatch(
    PX.Objects.CA.Light.APInvoice doc,
    PX.Objects.CA.Light.BAccount bAccount,
    PX.Objects.CA.CABankTran aDetail)
  {
    CABankTranInvoiceMatch tranInvoiceMatch1 = new CABankTranInvoiceMatch();
    tranInvoiceMatch1.Copy(doc);
    tranInvoiceMatch1.ReferenceCD = bAccount.AcctCD;
    tranInvoiceMatch1.ReferenceName = bAccount.AcctName;
    if (tranInvoiceMatch1.DrCr != aDetail.DrCr)
    {
      CABankTranInvoiceMatch tranInvoiceMatch2 = tranInvoiceMatch1;
      Decimal num1 = (Decimal) -1;
      Decimal? nullable1 = tranInvoiceMatch1.CuryTranAmt;
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?(num1 * nullable1.GetValueOrDefault()) : new Decimal?();
      tranInvoiceMatch2.CuryTranAmt = nullable2;
      CABankTranInvoiceMatch tranInvoiceMatch3 = tranInvoiceMatch1;
      Decimal num2 = (Decimal) -1;
      nullable1 = tranInvoiceMatch1.TranAmt;
      Decimal? nullable3 = nullable1.HasValue ? new Decimal?(num2 * nullable1.GetValueOrDefault()) : new Decimal?();
      tranInvoiceMatch3.TranAmt = nullable3;
      CABankTranInvoiceMatch tranInvoiceMatch4 = tranInvoiceMatch1;
      Decimal num3 = (Decimal) -1;
      nullable1 = tranInvoiceMatch1.CuryDiscAmt;
      Decimal? nullable4 = nullable1.HasValue ? new Decimal?(num3 * nullable1.GetValueOrDefault()) : new Decimal?();
      tranInvoiceMatch4.CuryDiscAmt = nullable4;
      CABankTranInvoiceMatch tranInvoiceMatch5 = tranInvoiceMatch1;
      Decimal num4 = (Decimal) -1;
      nullable1 = tranInvoiceMatch1.DiscAmt;
      Decimal? nullable5 = nullable1.HasValue ? new Decimal?(num4 * nullable1.GetValueOrDefault()) : new Decimal?();
      tranInvoiceMatch5.DiscAmt = nullable5;
    }
    return tranInvoiceMatch1;
  }

  public static List<CABankTranInvoiceMatch> FindDetailMatchingInvoicesProc(
    PXGraph graph,
    PX.Objects.CA.CABankTran aDetail,
    CashAccount cashAccount,
    Decimal aRelevanceTreshold,
    CABankTranInvoiceMatch[] aBestMatches)
  {
    List<CABankTranInvoiceMatch> source = new List<CABankTranInvoiceMatch>();
    IMatchSettings aSettings = (IMatchSettings) cashAccount;
    Decimal? curyTranAmt = aDetail.CuryTranAmt;
    Decimal num1 = 0M;
    Decimal? nullable1;
    Decimal? nullable2;
    if (!(curyTranAmt.GetValueOrDefault() > num1 & curyTranAmt.HasValue))
    {
      nullable1 = aDetail.CuryTranAmt;
      nullable2 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
    }
    else
      nullable2 = aDetail.CuryTranAmt;
    Decimal? tranAmount = nullable2;
    CABankTranInvoiceMatch tranInvoiceMatch1 = (CABankTranInvoiceMatch) null;
    int length = aBestMatches != null ? aBestMatches.Length : 0;
    bool valueOrDefault = cashAccount.ClearingAccount.GetValueOrDefault();
    bool? nullable3;
    if (!(aDetail.DrCr == "D"))
    {
      if (aDetail.DrCr == "C")
      {
        nullable3 = aSettings.AllowMatchingCreditMemo;
        if (!nullable3.GetValueOrDefault())
          goto label_51;
      }
      else
        goto label_51;
    }
    nullable1 = tranAmount;
    Decimal num2 = 0M;
    if (nullable1.GetValueOrDefault() > num2 & nullable1.HasValue)
    {
      List<object> bqlParams;
      foreach (PXResult<PX.Objects.CA.Light.ARInvoice, PX.Objects.CA.Light.BAccount, CABankTranMatch, PX.Objects.CA.Light.ARAdjust, PX.Objects.CA.Light.CABankTranAdjustment> pxResult in graph.GetService<ICABankTransactionsRepository>().CreateARInvoiceQuery(graph, aDetail, cashAccount, tranAmount, out bqlParams).Select(bqlParams.ToArray()))
      {
        PX.Objects.CA.Light.ARInvoice doc = PXResult<PX.Objects.CA.Light.ARInvoice, PX.Objects.CA.Light.BAccount, CABankTranMatch, PX.Objects.CA.Light.ARAdjust, PX.Objects.CA.Light.CABankTranAdjustment>.op_Implicit(pxResult);
        PX.Objects.CA.Light.BAccount bAccount = PXResult<PX.Objects.CA.Light.ARInvoice, PX.Objects.CA.Light.BAccount, CABankTranMatch, PX.Objects.CA.Light.ARAdjust, PX.Objects.CA.Light.CABankTranAdjustment>.op_Implicit(pxResult);
        CABankTranMatch aTranMatch = PXResult<PX.Objects.CA.Light.ARInvoice, PX.Objects.CA.Light.BAccount, CABankTranMatch, PX.Objects.CA.Light.ARAdjust, PX.Objects.CA.Light.CABankTranAdjustment>.op_Implicit(pxResult);
        PX.Objects.CA.Light.CABankTranAdjustment aAdjust = PXResult<PX.Objects.CA.Light.ARInvoice, PX.Objects.CA.Light.BAccount, CABankTranMatch, PX.Objects.CA.Light.ARAdjust, PX.Objects.CA.Light.CABankTranAdjustment>.op_Implicit(pxResult);
        CABankTranInvoiceMatch tranInvoiceMatch2 = CABankMatchingProcess.CreateCABankTranInvoiceMatch(doc, bAccount, aDetail);
        if (!CABankMatchingProcess.IsAlreadyMatched(graph, tranInvoiceMatch2, aDetail, aTranMatch) && !CABankMatchingProcess.IsAlreadyInAdjustment(graph, aAdjust, tranInvoiceMatch2.OrigTranType, tranInvoiceMatch2.OrigRefNbr, tranInvoiceMatch2.OrigModule))
        {
          tranInvoiceMatch2.MatchRelevance = new Decimal?(graph.GetService<IMatchingService>().EvaluateMatching(aDetail, tranInvoiceMatch2, aSettings));
          if ((aTranMatch == null ? 0 : (aTranMatch.TranID.HasValue ? 1 : 0)) == 0)
          {
            nullable1 = tranInvoiceMatch2.MatchRelevance;
            Decimal num3 = aRelevanceTreshold;
            if (nullable1.GetValueOrDefault() < num3 & nullable1.HasValue)
              continue;
          }
          Decimal? matchRelevance;
          if (length > 0)
          {
            for (int index1 = 0; index1 < length; ++index1)
            {
              if (aBestMatches[index1] != null)
              {
                nullable1 = aBestMatches[index1].MatchRelevance;
                matchRelevance = tranInvoiceMatch2.MatchRelevance;
                if (!(nullable1.GetValueOrDefault() < matchRelevance.GetValueOrDefault() & nullable1.HasValue & matchRelevance.HasValue))
                  continue;
              }
              for (int index2 = length - 1; index2 > index1; --index2)
                aBestMatches[index2] = aBestMatches[index2 - 1];
              aBestMatches[index1] = tranInvoiceMatch2;
              break;
            }
          }
          else
          {
            if (tranInvoiceMatch1 != null)
            {
              matchRelevance = tranInvoiceMatch1.MatchRelevance;
              nullable1 = tranInvoiceMatch2.MatchRelevance;
              if (!(matchRelevance.GetValueOrDefault() < nullable1.GetValueOrDefault() & matchRelevance.HasValue & nullable1.HasValue))
                goto label_25;
            }
            tranInvoiceMatch1 = tranInvoiceMatch2;
          }
label_25:
          tranInvoiceMatch2.IsBestMatch = new bool?(false);
          source.Add(tranInvoiceMatch2);
        }
      }
      foreach (PXResult<CABankTranMatch, PX.Objects.CA.Light.ARInvoice, PX.Objects.CA.Light.BAccount> pxResult in ((PXSelectBase<CABankTranMatch>) new PXSelectJoin<CABankTranMatch, LeftJoin<PX.Objects.CA.Light.ARInvoice, On<CABankTranMatch.docModule, Equal<BatchModule.moduleAR>, And<CABankTranMatch.docType, Equal<PX.Objects.CA.Light.ARInvoice.docType>, And<CABankTranMatch.docRefNbr, Equal<PX.Objects.CA.Light.ARInvoice.refNbr>>>>, LeftJoin<PX.Objects.CA.Light.BAccount, On<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.CA.Light.ARInvoice.customerID>>>>, Where<CABankTranMatch.tranID, Equal<Required<CABankTranMatch.tranID>>>>(graph)).Select(new object[1]
      {
        (object) aDetail.TranID
      }))
      {
        PX.Objects.CA.Light.ARInvoice iDoc = PXResult<CABankTranMatch, PX.Objects.CA.Light.ARInvoice, PX.Objects.CA.Light.BAccount>.op_Implicit(pxResult);
        if (!string.IsNullOrEmpty(iDoc?.RefNbr) && !source.Any<CABankTranInvoiceMatch>((Func<CABankTranInvoiceMatch, bool>) (r => r.OrigModule.Equals("AR") && r.OrigTranType.Equals(iDoc.DocType) && r.OrigRefNbr.Equals(iDoc.RefNbr))))
        {
          PX.Objects.CA.Light.BAccount bAccount = PXResult<CABankTranMatch, PX.Objects.CA.Light.ARInvoice, PX.Objects.CA.Light.BAccount>.op_Implicit(pxResult);
          PXResult<CABankTranMatch, PX.Objects.CA.Light.ARInvoice, PX.Objects.CA.Light.BAccount>.op_Implicit(pxResult);
          CABankTranInvoiceMatch tranInvoiceMatch3 = CABankMatchingProcess.CreateCABankTranInvoiceMatch(iDoc, bAccount, aDetail);
          tranInvoiceMatch3.MatchRelevance = new Decimal?(graph.GetService<IMatchingService>().EvaluateMatching(aDetail, tranInvoiceMatch3, aSettings));
          Decimal? matchRelevance1;
          Decimal? matchRelevance2;
          if (length > 0)
          {
            for (int index3 = 0; index3 < length; ++index3)
            {
              if (aBestMatches[index3] != null)
              {
                matchRelevance1 = aBestMatches[index3].MatchRelevance;
                matchRelevance2 = tranInvoiceMatch3.MatchRelevance;
                if (!(matchRelevance1.GetValueOrDefault() < matchRelevance2.GetValueOrDefault() & matchRelevance1.HasValue & matchRelevance2.HasValue))
                  continue;
              }
              for (int index4 = length - 1; index4 > index3; --index4)
                aBestMatches[index4] = aBestMatches[index4 - 1];
              aBestMatches[index3] = tranInvoiceMatch3;
              break;
            }
          }
          else
          {
            if (tranInvoiceMatch1 != null)
            {
              matchRelevance2 = tranInvoiceMatch1.MatchRelevance;
              matchRelevance1 = tranInvoiceMatch3.MatchRelevance;
              if (!(matchRelevance2.GetValueOrDefault() < matchRelevance1.GetValueOrDefault() & matchRelevance2.HasValue & matchRelevance1.HasValue))
                goto label_46;
            }
            tranInvoiceMatch1 = tranInvoiceMatch3;
          }
label_46:
          tranInvoiceMatch3.IsBestMatch = new bool?(false);
          source.Add(tranInvoiceMatch3);
        }
      }
    }
label_51:
    if (!(aDetail.DrCr == "C"))
    {
      if (aDetail.DrCr == "D")
      {
        nullable3 = aSettings.AllowMatchingDebitAdjustment;
        if (!nullable3.GetValueOrDefault())
          goto label_99;
      }
      else
        goto label_99;
    }
    if (!valueOrDefault)
    {
      List<object> bqlParams;
      foreach (PXResult<PX.Objects.CA.Light.APInvoice, PX.Objects.CA.Light.BAccount, CABankTranMatch, PX.Objects.CA.Light.APAdjust, PX.Objects.CA.Light.APPayment, PX.Objects.CA.Light.CABankTranAdjustment> pxResult in graph.GetService<ICABankTransactionsRepository>().CreateAPInvoiceQuery(graph, aDetail, cashAccount, tranAmount, out bqlParams).Select(bqlParams.ToArray()))
      {
        PX.Objects.CA.Light.APInvoice doc = PXResult<PX.Objects.CA.Light.APInvoice, PX.Objects.CA.Light.BAccount, CABankTranMatch, PX.Objects.CA.Light.APAdjust, PX.Objects.CA.Light.APPayment, PX.Objects.CA.Light.CABankTranAdjustment>.op_Implicit(pxResult);
        PX.Objects.CA.Light.BAccount bAccount = PXResult<PX.Objects.CA.Light.APInvoice, PX.Objects.CA.Light.BAccount, CABankTranMatch, PX.Objects.CA.Light.APAdjust, PX.Objects.CA.Light.APPayment, PX.Objects.CA.Light.CABankTranAdjustment>.op_Implicit(pxResult);
        CABankTranMatch aTranMatch = PXResult<PX.Objects.CA.Light.APInvoice, PX.Objects.CA.Light.BAccount, CABankTranMatch, PX.Objects.CA.Light.APAdjust, PX.Objects.CA.Light.APPayment, PX.Objects.CA.Light.CABankTranAdjustment>.op_Implicit(pxResult);
        PX.Objects.CA.Light.CABankTranAdjustment aAdjust = PXResult<PX.Objects.CA.Light.APInvoice, PX.Objects.CA.Light.BAccount, CABankTranMatch, PX.Objects.CA.Light.APAdjust, PX.Objects.CA.Light.APPayment, PX.Objects.CA.Light.CABankTranAdjustment>.op_Implicit(pxResult);
        CABankTranInvoiceMatch tranInvoiceMatch4 = CABankMatchingProcess.CreateCABankTranInvoiceMatch(doc, bAccount, aDetail);
        if (!CABankMatchingProcess.IsAlreadyMatched(graph, tranInvoiceMatch4, aDetail, aTranMatch) && !CABankMatchingProcess.IsAlreadyInAdjustment(graph, aAdjust, tranInvoiceMatch4.OrigTranType, tranInvoiceMatch4.OrigRefNbr, tranInvoiceMatch4.OrigModule))
        {
          tranInvoiceMatch4.MatchRelevance = new Decimal?(graph.GetService<IMatchingService>().EvaluateMatching(aDetail, tranInvoiceMatch4, aSettings));
          if ((aTranMatch == null ? 0 : (aTranMatch.TranID.HasValue ? 1 : 0)) == 0)
          {
            nullable1 = tranInvoiceMatch4.MatchRelevance;
            Decimal num4 = aRelevanceTreshold;
            if (nullable1.GetValueOrDefault() < num4 & nullable1.HasValue)
              continue;
          }
          Decimal? matchRelevance;
          if (length > 0)
          {
            for (int index5 = 0; index5 < length; ++index5)
            {
              if (aBestMatches[index5] != null)
              {
                nullable1 = aBestMatches[index5].MatchRelevance;
                matchRelevance = tranInvoiceMatch4.MatchRelevance;
                if (!(nullable1.GetValueOrDefault() < matchRelevance.GetValueOrDefault() & nullable1.HasValue & matchRelevance.HasValue))
                  continue;
              }
              for (int index6 = length - 1; index6 > index5; --index6)
                aBestMatches[index6] = aBestMatches[index6 - 1];
              aBestMatches[index5] = tranInvoiceMatch4;
              break;
            }
          }
          else
          {
            if (tranInvoiceMatch1 != null)
            {
              matchRelevance = tranInvoiceMatch1.MatchRelevance;
              nullable1 = tranInvoiceMatch4.MatchRelevance;
              if (!(matchRelevance.GetValueOrDefault() < nullable1.GetValueOrDefault() & matchRelevance.HasValue & nullable1.HasValue))
                goto label_73;
            }
            tranInvoiceMatch1 = tranInvoiceMatch4;
          }
label_73:
          tranInvoiceMatch4.IsBestMatch = new bool?(false);
          source.Add(tranInvoiceMatch4);
        }
      }
      foreach (PXResult<CABankTranMatch, PX.Objects.CA.Light.APInvoice, PX.Objects.CA.Light.BAccount> pxResult in ((PXSelectBase<CABankTranMatch>) new PXSelectJoin<CABankTranMatch, LeftJoin<PX.Objects.CA.Light.APInvoice, On<CABankTranMatch.docModule, Equal<BatchModule.moduleAP>, And<CABankTranMatch.docType, Equal<PX.Objects.CA.Light.APInvoice.docType>, And<CABankTranMatch.docRefNbr, Equal<PX.Objects.CA.Light.APInvoice.refNbr>>>>, LeftJoin<PX.Objects.CA.Light.BAccount, On<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.CA.Light.APInvoice.vendorID>>>>, Where<CABankTranMatch.tranID, Equal<Required<CABankTranMatch.tranID>>>>(graph)).Select(new object[1]
      {
        (object) aDetail.TranID
      }))
      {
        PX.Objects.CA.Light.APInvoice iDoc = PXResult<CABankTranMatch, PX.Objects.CA.Light.APInvoice, PX.Objects.CA.Light.BAccount>.op_Implicit(pxResult);
        if (!string.IsNullOrEmpty(iDoc?.RefNbr) && !source.Any<CABankTranInvoiceMatch>((Func<CABankTranInvoiceMatch, bool>) (r => r.OrigModule.Equals("AP") && r.OrigTranType.Equals(iDoc.DocType) && r.OrigRefNbr.Equals(iDoc.RefNbr))))
        {
          PX.Objects.CA.Light.BAccount bAccount = PXResult<CABankTranMatch, PX.Objects.CA.Light.APInvoice, PX.Objects.CA.Light.BAccount>.op_Implicit(pxResult);
          PXResult<CABankTranMatch, PX.Objects.CA.Light.APInvoice, PX.Objects.CA.Light.BAccount>.op_Implicit(pxResult);
          CABankTranInvoiceMatch tranInvoiceMatch5 = CABankMatchingProcess.CreateCABankTranInvoiceMatch(iDoc, bAccount, aDetail);
          tranInvoiceMatch5.MatchRelevance = new Decimal?(graph.GetService<IMatchingService>().EvaluateMatching(aDetail, tranInvoiceMatch5, aSettings));
          Decimal? matchRelevance3;
          Decimal? matchRelevance4;
          if (length > 0)
          {
            for (int index7 = 0; index7 < length; ++index7)
            {
              if (aBestMatches[index7] != null)
              {
                matchRelevance3 = aBestMatches[index7].MatchRelevance;
                matchRelevance4 = tranInvoiceMatch5.MatchRelevance;
                if (!(matchRelevance3.GetValueOrDefault() < matchRelevance4.GetValueOrDefault() & matchRelevance3.HasValue & matchRelevance4.HasValue))
                  continue;
              }
              for (int index8 = length - 1; index8 > index7; --index8)
                aBestMatches[index8] = aBestMatches[index8 - 1];
              aBestMatches[index7] = tranInvoiceMatch5;
              break;
            }
          }
          else
          {
            if (tranInvoiceMatch1 != null)
            {
              matchRelevance4 = tranInvoiceMatch1.MatchRelevance;
              matchRelevance3 = tranInvoiceMatch5.MatchRelevance;
              if (!(matchRelevance4.GetValueOrDefault() < matchRelevance3.GetValueOrDefault() & matchRelevance4.HasValue & matchRelevance3.HasValue))
                goto label_94;
            }
            tranInvoiceMatch1 = tranInvoiceMatch5;
          }
label_94:
          tranInvoiceMatch5.IsBestMatch = new bool?(false);
          source.Add(tranInvoiceMatch5);
        }
      }
    }
label_99:
    if (length > 0)
      tranInvoiceMatch1 = aBestMatches[0];
    if (tranInvoiceMatch1 != null)
      tranInvoiceMatch1.IsBestMatch = new bool?(true);
    return source;
  }

  public virtual List<CABankTranExpenseDetailMatch> GetExpenseReceiptDetailMatches(
    CABankMatchingProcess graph,
    PX.Objects.CA.CABankTran bankTran,
    CashAccount cashAccount,
    Decimal relevanceTreshold,
    CABankTranExpenseDetailMatch[] bestMatches)
  {
    if (!cashAccount.UseForCorpCard.GetValueOrDefault())
      return new List<CABankTranExpenseDetailMatch>();
    List<CABankTranExpenseDetailMatch> receiptDetailMatches = (List<CABankTranExpenseDetailMatch>) null;
    if (this.matchingExpenseReceiptDetails == null)
      this.matchingExpenseReceiptDetails = new Dictionary<object, List<CABankTranExpenseDetailMatch>>(1);
    if (bestMatches == null && this.matchingExpenseReceiptDetails.TryGetValue((object) bankTran, out receiptDetailMatches))
    {
      bankTran.CountExpenseReceiptDetailMatches = new int?(receiptDetailMatches.Count);
      return receiptDetailMatches;
    }
    if (receiptDetailMatches == null)
      receiptDetailMatches = new List<CABankTranExpenseDetailMatch>();
    receiptDetailMatches.AddRange((IEnumerable<CABankTranExpenseDetailMatch>) CABankMatchingProcess.FindExpenseReceiptDetailMatches((PXGraph) graph, bankTran, cashAccount, relevanceTreshold, bestMatches));
    this.matchingExpenseReceiptDetails[(object) bankTran] = receiptDetailMatches;
    bankTran.CountExpenseReceiptDetailMatches = new int?(receiptDetailMatches.Count);
    return receiptDetailMatches;
  }

  public static IList<CABankTranExpenseDetailMatch> FindExpenseReceiptDetailMatches(
    PXGraph graph,
    PX.Objects.CA.CABankTran bankTran,
    CashAccount cashAccount,
    Decimal relevanceTreshold,
    CABankTranExpenseDetailMatch[] bestMatches)
  {
    List<CABankTranExpenseDetailMatch> receiptDetailMatches = new List<CABankTranExpenseDetailMatch>();
    IMatchSettings aSettings = (IMatchSettings) cashAccount;
    Pair<DateTime, DateTime> dateRangeForMatch = graph.GetService<IMatchingService>().GetDateRangeForMatch(bankTran, aSettings);
    Decimal num1 = -bankTran.CuryTranAmt.Value;
    Decimal num2 = 0M;
    if (aSettings.CuryDiffThreshold.HasValue)
      num2 = Math.Abs(num1) / 100M * aSettings.CuryDiffThreshold.Value;
    Decimal num3 = num1 - num2;
    Decimal num4 = num1 + num2;
    int length = bestMatches != null ? bestMatches.Length : 0;
    CABankTranMatch caBankTranMatch = PXResultset<CABankTranMatch>.op_Implicit(PXSelectBase<CABankTranMatch, PXSelect<CABankTranMatch, Where<CABankTranMatch.tranID, Equal<Required<CABankTranMatch.tranID>>, And<CABankTranMatch.docModule, Equal<BatchModule.moduleEP>, And<CABankTranMatch.docType, Equal<EPExpenseClaimDetails.docType>>>>>.Config>.Select(graph, new object[1]
    {
      (object) bankTran.TranID
    }));
    foreach (PXResult<EPExpenseClaimDetails, PX.Objects.CA.Light.BAccount, CACorpCard, CABankTranMatch, PX.Objects.CM.Extensions.CurrencyInfo> pxResult in PXSelectBase<EPExpenseClaimDetails, PXSelectJoin<EPExpenseClaimDetails, InnerJoin<PX.Objects.CA.Light.BAccount, On<EPExpenseClaimDetails.employeeID, Equal<PX.Objects.CA.Light.BAccount.bAccountID>>, InnerJoin<CACorpCard, On<CACorpCard.corpCardID, Equal<EPExpenseClaimDetails.corpCardID>>, LeftJoin<CABankTranMatch, On<CABankTranMatch.docModule, Equal<BatchModule.moduleEP>, And<CABankTranMatch.docType, Equal<EPExpenseClaimDetails.docType>, And<CABankTranMatch.docRefNbr, Equal<EPExpenseClaimDetails.claimDetailCD>>>>, LeftJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<EPExpenseClaimDetails.claimCuryInfoID, Equal<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>, LeftJoin<CATran, On<CATran.origModule, Equal<BatchModule.moduleAP>, And<EPExpenseClaimDetails.aPDocType, Equal<CATran.origTranType>, And<EPExpenseClaimDetails.aPRefNbr, Equal<CATran.origRefNbr>>>>, LeftJoin<PX.Objects.GL.GLTran, On<PX.Objects.GL.GLTran.module, Equal<BatchModule.moduleAP>, And<EPExpenseClaimDetails.aPDocType, Equal<PX.Objects.GL.GLTran.tranType>, And<EPExpenseClaimDetails.aPRefNbr, Equal<PX.Objects.GL.GLTran.refNbr>, And<EPExpenseClaimDetails.aPLineNbr, Equal<PX.Objects.GL.GLTran.tranLineNbr>>>>>, LeftJoin<CATran2, On<CATran2.origModule, Equal<BatchModule.moduleAP>, And<CATran2.origTranType, Equal<GLTranType.gLEntry>, And<CATran2.origRefNbr, Equal<PX.Objects.GL.GLTran.batchNbr>, And<CATran2.origLineNbr, Equal<PX.Objects.GL.GLTran.lineNbr>>>>>>>>>>>>, Where2<Where<EPExpenseClaimDetails.claimCuryTranAmtWithTaxes, Between<Required<EPExpenseClaimDetails.claimCuryTranAmtWithTaxes>, Required<EPExpenseClaimDetails.claimCuryTranAmtWithTaxes>>, And<EPExpenseClaimDetails.curyID, NotEqual<EPExpenseClaimDetails.cardCuryID>, Or<EPExpenseClaimDetails.claimCuryTranAmtWithTaxes, Equal<Required<EPExpenseClaimDetails.claimCuryTranAmtWithTaxes>>>>>, And<EPExpenseClaimDetails.hold, NotEqual<True>, And<EPExpenseClaimDetails.rejected, NotEqual<True>, And<EPExpenseClaimDetails.paidWith, NotEqual<EPExpenseClaimDetails.paidWith.cash>, And<CACorpCard.cashAccountID, Equal<Required<CACorpCard.cashAccountID>>, And<EPExpenseClaimDetails.expenseDate, GreaterEqual<Required<EPExpenseClaimDetails.expenseDate>>, And<EPExpenseClaimDetails.expenseDate, LessEqual<Required<EPExpenseClaimDetails.expenseDate>>, And<CATran.tranID, IsNull, And<CATran2.tranID, IsNull, Or<EPExpenseClaimDetails.claimDetailCD, Equal<Required<EPExpenseClaimDetails.claimDetailCD>>>>>>>>>>>>>.Config>.Select(graph, new object[7]
    {
      (object) num3,
      (object) num4,
      (object) num1,
      (object) cashAccount.CashAccountID,
      (object) dateRangeForMatch.first,
      (object) dateRangeForMatch.second,
      (object) caBankTranMatch?.DocRefNbr
    }))
    {
      EPExpenseClaimDetails expenseClaimDetails = PXResult<EPExpenseClaimDetails, PX.Objects.CA.Light.BAccount, CACorpCard, CABankTranMatch, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
      PX.Objects.CA.Light.BAccount baccount = PXResult<EPExpenseClaimDetails, PX.Objects.CA.Light.BAccount, CACorpCard, CABankTranMatch, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
      CACorpCard caCorpCard = PXResult<EPExpenseClaimDetails, PX.Objects.CA.Light.BAccount, CACorpCard, CABankTranMatch, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
      CABankTranMatch aTranMatch = PXResult<EPExpenseClaimDetails, PX.Objects.CA.Light.BAccount, CACorpCard, CABankTranMatch, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
      PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResult<EPExpenseClaimDetails, PX.Objects.CA.Light.BAccount, CACorpCard, CABankTranMatch, PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
      CABankTranExpenseDetailMatch expenseDetailMatch = new CABankTranExpenseDetailMatch();
      expenseDetailMatch.RefNbr = expenseClaimDetails.ClaimDetailCD;
      expenseDetailMatch.ExtRefNbr = expenseClaimDetails.ExpenseRefNbr;
      expenseDetailMatch.CuryDocAmt = expenseClaimDetails.ClaimCuryTranAmtWithTaxes;
      expenseDetailMatch.ClaimCuryID = currencyInfo?.CuryID ?? cashAccount.CuryID;
      Decimal num5 = num1;
      Decimal? nullable = expenseClaimDetails.ClaimCuryTranAmtWithTaxes;
      Decimal num6 = nullable.Value;
      expenseDetailMatch.CuryDocAmtDiff = new Decimal?(Math.Abs(num5 - num6));
      expenseDetailMatch.PaidWith = expenseClaimDetails.PaidWith;
      expenseDetailMatch.ReferenceID = expenseClaimDetails.EmployeeID;
      expenseDetailMatch.ReferenceName = baccount.AcctName;
      expenseDetailMatch.DocDate = expenseClaimDetails.ExpenseDate;
      expenseDetailMatch.CardNumber = caCorpCard.CardNumber;
      expenseDetailMatch.TranDesc = expenseClaimDetails.TranDesc;
      CABankTranExpenseDetailMatch expenseMath = expenseDetailMatch;
      if (!CABankMatchingProcess.IsAlreadyMatched(graph, "EP", "ECD", expenseMath.RefNbr, bankTran, aTranMatch) && CABankMatchingProcess.IsCardNumberMatch(bankTran.CardNumber, expenseMath.CardNumber))
      {
        expenseMath.MatchRelevance = new Decimal?(graph.GetService<IMatchingService>().EvaluateMatching(bankTran, expenseMath, aSettings));
        if ((aTranMatch == null ? 0 : (aTranMatch.TranID.HasValue ? 1 : 0)) == 0)
        {
          nullable = expenseMath.MatchRelevance;
          Decimal num7 = relevanceTreshold;
          if (nullable.GetValueOrDefault() < num7 & nullable.HasValue)
            continue;
        }
        if (length > 0)
        {
          for (int index1 = 0; index1 < length; ++index1)
          {
            if (bestMatches[index1] != null)
            {
              nullable = bestMatches[index1].MatchRelevance;
              Decimal? matchRelevance = expenseMath.MatchRelevance;
              if (!(nullable.GetValueOrDefault() < matchRelevance.GetValueOrDefault() & nullable.HasValue & matchRelevance.HasValue))
                continue;
            }
            for (int index2 = length - 1; index2 > index1; --index2)
              bestMatches[index2] = bestMatches[index2 - 1];
            bestMatches[index1] = expenseMath;
            break;
          }
        }
        receiptDetailMatches.Add(expenseMath);
      }
    }
    return (IList<CABankTranExpenseDetailMatch>) receiptDetailMatches;
  }

  public static string ExtractCardNumber(string cardNumberRaw)
  {
    if (string.IsNullOrEmpty(cardNumberRaw))
      return cardNumberRaw;
    int startIndex = 0;
    int num = -1;
    for (int index = cardNumberRaw.Length - 1; index >= 0; --index)
    {
      if (num == -1 && char.IsDigit(cardNumberRaw[index]))
        num = index;
      else if (num != -1 && !char.IsDigit(cardNumberRaw[index]))
      {
        startIndex = index + 1;
        break;
      }
    }
    return num != -1 ? cardNumberRaw.Substring(startIndex, num - startIndex + 1) : string.Empty;
  }

  public static bool IsCardNumberMatch(string bankTranCardNumberRaw, string receiptCardNumberRaw)
  {
    string cardNumber1 = CABankMatchingProcess.ExtractCardNumber(bankTranCardNumberRaw);
    string cardNumber2 = CABankMatchingProcess.ExtractCardNumber(receiptCardNumberRaw);
    if (string.IsNullOrEmpty(cardNumber1))
      return true;
    if (string.IsNullOrEmpty(cardNumber2))
      return false;
    return cardNumber1.Length <= cardNumber2.Length ? cardNumber2.Contains(cardNumber1) : cardNumber1.Contains(cardNumber2);
  }

  protected static bool IsAlreadyInAdjustment(
    PXGraph graph,
    PX.Objects.CA.Light.CABankTranAdjustment aAdjust,
    string aTranDocType,
    string aTranRefNbr,
    string aTranModule)
  {
    return aAdjust != null ? CABankMatchingProcess.IsAlreadyInAdjustment(graph, aAdjust.TranID, aAdjust.AdjdRefNbr, aAdjust.AdjdDocType, aAdjust.AdjdModule, aTranDocType, aTranRefNbr, aTranModule) : CABankMatchingProcess.IsAlreadyInAdjustment(graph, new int?(), (string) null, (string) null, (string) null, aTranDocType, aTranRefNbr, aTranModule);
  }

  protected static bool IsAlreadyInAdjustment(
    PXGraph graph,
    int? tranID,
    string adjdRefNbr,
    string adjdDocType,
    string adjdModule,
    string aTranDocType,
    string aTranRefNbr,
    string aTranModule)
  {
    PXCache cach = graph.Caches[typeof (CABankTranAdjustment)];
    bool flag = adjdRefNbr != null;
    if (!flag)
    {
      foreach (CABankTranAdjustment bankTranAdjustment in cach.Inserted)
      {
        if (bankTranAdjustment.AdjdDocType == aTranDocType && bankTranAdjustment.AdjdRefNbr == aTranRefNbr && bankTranAdjustment.AdjdModule == aTranModule)
        {
          flag = true;
          break;
        }
      }
    }
    else
    {
      foreach (CABankTranAdjustment bankTranAdjustment in cach.Deleted)
      {
        int? tranId = bankTranAdjustment.TranID;
        int? nullable = tranID;
        if (tranId.GetValueOrDefault() == nullable.GetValueOrDefault() & tranId.HasValue == nullable.HasValue && bankTranAdjustment.AdjdDocType == adjdDocType && bankTranAdjustment.AdjdRefNbr == adjdRefNbr && bankTranAdjustment.AdjdModule == adjdModule)
        {
          flag = false;
          break;
        }
      }
    }
    return flag;
  }

  public static bool IsAlreadyMatched(
    PXGraph graph,
    CABankTranInvoiceMatch aMatch,
    PX.Objects.CA.CABankTran aTran,
    CABankTranMatch aTranMatch)
  {
    return CABankMatchingProcess.IsAlreadyMatched(graph, aMatch.OrigModule, aMatch.OrigTranType, aMatch.OrigRefNbr, aTran, aTranMatch);
  }

  public static bool IsAlreadyMatched(
    PXGraph graph,
    string module,
    string docType,
    string refNbr,
    PX.Objects.CA.CABankTran aTran,
    CABankTranMatch aTranMatch)
  {
    PXCache cach = graph.Caches[typeof (CABankTranMatch)];
    bool flag1 = aTranMatch.TranID.HasValue;
    int? tranId1 = aTranMatch.TranID;
    int? tranId2 = aTran.TranID;
    bool flag2 = tranId1.GetValueOrDefault() == tranId2.GetValueOrDefault() & tranId1.HasValue == tranId2.HasValue;
    if (flag1)
    {
      if (cach.GetStatus((object) aTranMatch) - 3 <= 1)
      {
        flag1 = false;
        flag2 = false;
      }
      else
      {
        CABankTranMatch caBankTranMatch = (CABankTranMatch) cach.Locate((object) aTranMatch);
        if (caBankTranMatch != null && !cach.ObjectsEqual<CABankTranMatch.tranType, CABankTranMatch.docModule, CABankTranMatch.docType, CABankTranMatch.docRefNbr>((object) caBankTranMatch, (object) aTranMatch))
        {
          flag1 = false;
          flag2 = false;
        }
      }
    }
    if (!flag1)
    {
      foreach (CABankTranMatch caBankTranMatch in cach.Inserted)
      {
        if (caBankTranMatch.TranType == aTran.TranType && caBankTranMatch.DocModule == module && caBankTranMatch.DocType == docType && caBankTranMatch.DocRefNbr == refNbr)
        {
          int? tranId3 = caBankTranMatch.TranID;
          int? tranId4 = aTran.TranID;
          if (!(tranId3.GetValueOrDefault() == tranId4.GetValueOrDefault() & tranId3.HasValue == tranId4.HasValue))
          {
            flag1 = true;
            tranId4 = caBankTranMatch.TranID;
            tranId3 = aTran.TranID;
            flag2 = tranId4.GetValueOrDefault() == tranId3.GetValueOrDefault() & tranId4.HasValue == tranId3.HasValue;
            break;
          }
        }
      }
    }
    if (!flag1)
    {
      foreach (CABankTranMatch caBankTranMatch in cach.Updated)
      {
        if (caBankTranMatch.TranType == aTran.TranType && caBankTranMatch.DocModule == module && caBankTranMatch.DocType == docType && caBankTranMatch.DocRefNbr == refNbr)
        {
          int? tranId5 = caBankTranMatch.TranID;
          int? tranId6 = aTran.TranID;
          if (!(tranId5.GetValueOrDefault() == tranId6.GetValueOrDefault() & tranId5.HasValue == tranId6.HasValue))
          {
            flag1 = true;
            tranId6 = caBankTranMatch.TranID;
            tranId5 = aTran.TranID;
            flag2 = tranId6.GetValueOrDefault() == tranId5.GetValueOrDefault() & tranId6.HasValue == tranId5.HasValue;
            break;
          }
        }
      }
    }
    return flag1 && !flag2;
  }

  protected virtual bool IsMatchedToExpenseReceipt(CABankTranMatch match)
  {
    return CABankTransactionsHelper.IsMatchedToExpenseReceipt(match);
  }

  protected virtual void ClearCachedMatches()
  {
    if (this.matchingInvoices != null)
      this.matchingInvoices.Clear();
    if (this.matchingTrans != null)
      this.matchingTrans.Clear();
    if (this.matchingExpenseReceiptDetails == null)
      return;
    this.matchingExpenseReceiptDetails.Clear();
  }

  [PXMergeAttributes]
  [PXDefault("A")]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CA.CABankTran.matchReason> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CA.CABankTran.lastAutoMatchDate> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CA.CABankTran.lastAutoMatchDate>, object, object>) e).NewValue = (object) PXTimeZoneInfo.Now;
  }

  protected virtual void CABankTran_DocumentMatched_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PX.Objects.CA.CABankTran row = (PX.Objects.CA.CABankTran) e.Row;
    if (row == null || !row.DocumentMatched.GetValueOrDefault())
      return;
    CABankTranMatch match = ((PXSelectBase<CABankTranMatch>) this.TranMatch).SelectSingle(new object[1]
    {
      (object) row.TranID
    });
    if (match != null && !string.IsNullOrEmpty(match.DocRefNbr) && !this.IsMatchedToExpenseReceipt(match))
    {
      ((PXSelectBase) this.CABankTran).Cache.SetValue<PX.Objects.CA.CABankTran.origModule>((object) row, (object) match.DocModule);
      if (!row.PayeeBAccountIDCopy.HasValue)
      {
        ((PXSelectBase) this.CABankTran).Cache.SetValue<PX.Objects.CA.CABankTran.payeeBAccountIDCopy>((object) row, (object) match.ReferenceID);
        object payeeBaccountIdCopy = (object) row.PayeeBAccountIDCopy;
        ((PXSelectBase) this.CABankTran).Cache.RaiseFieldUpdating<PX.Objects.CA.CABankTran.payeeBAccountIDCopy>((object) row, ref payeeBaccountIdCopy);
        ((PXSelectBase) this.CABankTran).Cache.RaiseFieldUpdated<PX.Objects.CA.CABankTran.payeeBAccountIDCopy>((object) row, (object) null);
      }
      else
      {
        ((PXSelectBase) this.CABankTran).Cache.SetDefaultExt<PX.Objects.CA.CABankTran.payeeLocationID>((object) row);
        ((PXSelectBase) this.CABankTran).Cache.SetDefaultExt<PX.Objects.CA.CABankTran.paymentMethodID>((object) row);
        ((PXSelectBase) this.CABankTran).Cache.SetDefaultExt<PX.Objects.CA.CABankTran.pMInstanceID>((object) row);
      }
    }
    ((PXSelectBase) this.CABankTran).Cache.SetDefaultExt<PX.Objects.CA.CABankTran.matchReason>((object) row);
    ((PXSelectBase) this.CABankTran).Cache.SetDefaultExt<PX.Objects.CA.CABankTran.lastAutoMatchDate>((object) row);
  }

  protected virtual void CABankTran_PayeeBAccountIDCopy_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PX.Objects.CA.CABankTran row = (PX.Objects.CA.CABankTran) e.Row;
    if (row == null)
      return;
    int? nullable1 = row.PayeeBAccountIDCopy;
    int num;
    if (!nullable1.HasValue)
    {
      bool? nullable2 = row.DocumentMatched;
      if (nullable2.GetValueOrDefault())
      {
        nullable2 = row.MatchedToInvoice;
        num = nullable2.GetValueOrDefault() ? 1 : 0;
        goto label_5;
      }
    }
    num = 0;
label_5:
    bool flag = num != 0;
    if (!flag)
    {
      nullable1 = row.PayeeBAccountIDCopy;
      if (nullable1.HasValue && e.OldValue != null)
      {
        nullable1 = (int?) PX.Objects.CR.BAccount.PK.Find((PXGraph) this, (int?) e.OldValue)?.ParentBAccountID;
        int? nullable3 = row.PayeeBAccountIDCopy;
        if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
        {
          flag = true;
          foreach (PXResult<CABankTranMatch> pxResult in ((PXSelectBase<CABankTranMatch>) this.TranMatch).Select(new object[1]
          {
            (object) row.TranID
          }))
          {
            nullable3 = PXResult<CABankTranMatch>.op_Implicit(pxResult).ReferenceID;
            nullable1 = row.PayeeBAccountIDCopy;
            if (nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue)
            {
              flag = false;
              break;
            }
          }
        }
      }
    }
    if (flag)
    {
      ((PXSelectBase) this.CABankTran).Cache.SetValue<PX.Objects.CA.CABankTran.documentMatched>((object) row, (object) false);
      ((PXSelectBase) this.CABankTran).Cache.SetValue<PX.Objects.CA.CABankTran.matchedToExisting>((object) row, (object) null);
      ((PXSelectBase) this.CABankTran).Cache.SetValue<PX.Objects.CA.CABankTran.matchedToInvoice>((object) row, (object) null);
      ((PXSelectBase) this.CABankTran).Cache.SetValue<PX.Objects.CA.CABankTran.matchedToExpenseReceipt>((object) row, (object) null);
      ((PXSelectBase) this.CABankTran).Cache.SetValue<PX.Objects.CA.CABankTran.origModule>((object) row, (object) null);
    }
    sender.SetDefaultExt<PX.Objects.CA.CABankTran.payeeLocationID>(e.Row);
    sender.SetDefaultExt<PX.Objects.CA.CABankTran.paymentMethodID>(e.Row);
    sender.SetDefaultExt<PX.Objects.CA.CABankTran.pMInstanceID>(e.Row);
    sender.SetDefaultExt<PX.Objects.CA.CABankTran.entryTypeID>(e.Row);
  }

  protected virtual string GetUnrecognizedReceiptDefaultEntryType(PX.Objects.CA.CABankTran row)
  {
    string paymentEntryTypeId = ((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.UnknownPaymentEntryTypeID;
    return PXResultset<CAEntryType>.op_Implicit(PXSelectBase<CAEntryType, PXSelectJoin<CAEntryType, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>, Where<CashAccountETDetail.cashAccountID, Equal<Required<PX.Objects.CA.CABankTran.cashAccountID>>, And<CAEntryType.module, Equal<BatchModule.moduleCA>, And<CAEntryType.drCr, Equal<Required<PX.Objects.CA.CABankTran.drCr>>, And<CAEntryType.entryTypeId, Equal<Required<CAEntryType.entryTypeId>>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) row.CashAccountID,
      (object) row.DrCr,
      (object) paymentEntryTypeId
    })) == null ? (string) null : paymentEntryTypeId;
  }

  protected virtual string GetDefaultCashAccountEntryType(PX.Objects.CA.CABankTran row)
  {
    return PXResultset<CAEntryType>.op_Implicit(PXSelectBase<CAEntryType, PXSelectJoin<CAEntryType, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>, Where<CashAccountETDetail.cashAccountID, Equal<Required<PX.Objects.CA.CABankTran.cashAccountID>>, And<CashAccountETDetail.isDefault, Equal<True>, And<CAEntryType.module, Equal<BatchModule.moduleCA>, And<CAEntryType.drCr, Equal<Required<PX.Objects.CA.CABankTran.drCr>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.CashAccountID,
      (object) row.DrCr
    }))?.EntryTypeId;
  }

  protected virtual void CreateDocument(PX.Objects.CA.CABankTran row, CashAccount cashAccount)
  {
    if (!row.CreateDocument.GetValueOrDefault())
      return;
    bool flag1 = false;
    try
    {
      flag1 = this.ApplyInvoiceInfo(((PXSelectBase) this.CABankTran).Cache, row);
    }
    catch
    {
      foreach (PXResult<CABankTranAdjustment> pxResult in ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Select(new object[1]
      {
        (object) row.TranID
      }))
        ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Delete(PXResult<CABankTranAdjustment>.op_Implicit(pxResult));
    }
    bool? nullable1;
    int num1;
    if (cashAccount == null)
    {
      num1 = 0;
    }
    else
    {
      nullable1 = cashAccount.ClearingAccount;
      num1 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    bool flag2 = num1 != 0;
    CABankTranDetail caBankTranDetail = (CABankTranDetail) null;
    nullable1 = row.CreateDocument;
    if (nullable1.GetValueOrDefault() && !flag1 && !flag2)
    {
      if (!this.AttemptApplyRules(row))
      {
        ((PXSelectBase<PX.Objects.CA.CABankTran>) this.CABankTran).Current = row;
        row.OrigModule = this.GetDefaultOrigModule(row, cashAccount);
        row.EntryTypeID = this.GetDefaultEntryTypeId(row);
      }
      PX.Objects.CA.CABankTran caBankTran = row;
      string tranDesc = row.TranDesc;
      string str = (tranDesc != null ? (tranDesc.Length > 256 /*0x0100*/ ? 1 : 0) : 0) != 0 ? row.TranDesc.Substring(0, (int) byte.MaxValue) : row.TranDesc;
      caBankTran.UserDesc = str;
      this.TryToSetDefaultTaxInfo(row, cashAccount);
      row = ((PXSelectBase<PX.Objects.CA.CABankTran>) this.CABankTran).Update(row);
      if (!string.IsNullOrEmpty(row.EntryTypeID))
      {
        Decimal num2 = row.DrCr == "D" ? 1M : -1M;
        Decimal? curyTranAmt = row.CuryTranAmt;
        Decimal? nullable2 = curyTranAmt.HasValue ? new Decimal?(num2 * curyTranAmt.GetValueOrDefault()) : new Decimal?();
        caBankTranDetail = ((PXSelectBase<CABankTranDetail>) this.TranSplit).Insert(new CABankTranDetail()
        {
          Qty = new Decimal?(1.0M),
          CuryUnitPrice = nullable2,
          CuryTranAmt = nullable2,
          TranDesc = row.UserDesc
        });
      }
    }
    bool flag3 = !(row.OrigModule == "CA") || caBankTranDetail != null && caBankTranDetail.AccountID.HasValue && caBankTranDetail != null && caBankTranDetail.SubID.HasValue;
    nullable1 = row.CreateDocument;
    int num3;
    if (nullable1.GetValueOrDefault() && !string.IsNullOrEmpty(row.EntryTypeID))
    {
      Decimal? curyUnappliedBalCa = row.CuryUnappliedBalCA;
      Decimal num4 = 0M;
      num3 = curyUnappliedBalCa.GetValueOrDefault() == num4 & curyUnappliedBalCa.HasValue ? 1 : 0;
    }
    else
      num3 = 0;
    int num5 = flag3 ? 1 : 0;
    bool flag4 = (num3 & num5) != 0;
    row.DocumentMatched = new bool?(flag4);
    ((PXSelectBase) this.CABankTran).Cache.Update((object) row);
    ((PXSelectBase) this.CABankTran).Cache.SetDefaultExt<PX.Objects.CA.CABankTran.matchingPaymentDate>((object) row);
    ((PXSelectBase) this.CABankTran).Cache.SetDefaultExt<PX.Objects.CA.CABankTran.matchingfinPeriodID>((object) row);
  }

  private void TryToSetDefaultTaxInfo(PX.Objects.CA.CABankTran row, CashAccount cashAccount)
  {
    if (string.IsNullOrEmpty(row.EntryTypeID))
      return;
    CashAccountETDetail cashAccountEtDetail = PXResultset<CashAccountETDetail>.op_Implicit(PXSelectBase<CashAccountETDetail, PXSelect<CashAccountETDetail, Where<CashAccountETDetail.cashAccountID, Equal<Required<PX.Objects.CA.CABankTran.cashAccountID>>, And<CashAccountETDetail.entryTypeID, Equal<Required<PX.Objects.CA.CABankTran.entryTypeID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) cashAccount.CashAccountID,
      (object) row.EntryTypeID
    }));
    if (cashAccountEtDetail == null || cashAccountEtDetail.EntryTypeID == null)
      return;
    row.TaxZoneID = cashAccountEtDetail.TaxZoneID;
    row.TaxCalcMode = cashAccountEtDetail.TaxCalcMode;
  }

  private string GetDefaultEntryTypeId(PX.Objects.CA.CABankTran row)
  {
    string defaultEntryTypeId = (string) null;
    if (row.OrigModule == "CA")
    {
      string str = this.GetUnrecognizedReceiptDefaultEntryType(row);
      if (string.IsNullOrEmpty(str))
        str = this.GetDefaultCashAccountEntryType(row);
      defaultEntryTypeId = str;
    }
    return defaultEntryTypeId;
  }

  private string GetDefaultOrigModule(PX.Objects.CA.CABankTran row, CashAccount cashAccount)
  {
    if (cashAccount != null && cashAccount.ClearingAccount.GetValueOrDefault())
      return "AR";
    if (string.IsNullOrEmpty(row.InvoiceInfo))
      return "CA";
    if (row.DrCr == "C")
      return "AP";
    if (row.DrCr == "D")
      return "AR";
    throw new NotImplementedException();
  }

  protected virtual object FindInvoiceByInvoiceInfo(PX.Objects.CA.CABankTran row, out string module)
  {
    return ((PXGraph) this).GetService<ICABankTransactionsRepository>().FindInvoiceByInvoiceInfo<CABankMatchingProcess>(this, row, out module);
  }

  protected virtual bool ApplyInvoiceInfo(PXCache sender, PX.Objects.CA.CABankTran row)
  {
    if (row.CreateDocument.GetValueOrDefault() && row.InvoiceInfo != null)
    {
      string module;
      object invoiceByInvoiceInfo = this.FindInvoiceByInvoiceInfo(row, out module);
      if (invoiceByInvoiceInfo != null)
      {
        int? nullable1 = new int?();
        int? nullable2;
        string str;
        string refNbr;
        switch (module)
        {
          case "AP":
            nullable2 = invoiceByInvoiceInfo is PX.Objects.AP.APInvoice apInvoice ? apInvoice.VendorID : throw new PXSetPropertyException("Wrong Invoice Type!");
            str = apInvoice.PayTypeID;
            refNbr = apInvoice.RefNbr;
            break;
          case "AR":
            nullable2 = invoiceByInvoiceInfo is PX.Objects.AR.ARInvoice arInvoice ? arInvoice.CustomerID : throw new PXSetPropertyException("Wrong Invoice Type!");
            str = arInvoice.PaymentMethodID;
            nullable1 = arInvoice.PMInstanceID;
            refNbr = arInvoice.RefNbr;
            break;
          default:
            throw new PXSetPropertyException("Unknown module!");
        }
        sender.SetValueExt<PX.Objects.CA.CABankTran.origModule>((object) row, (object) module);
        sender.SetValue<PX.Objects.CA.CABankTran.payeeBAccountID>((object) row, (object) nullable2);
        object payeeBaccountId = (object) row.PayeeBAccountID;
        sender.RaiseFieldUpdating<PX.Objects.CA.CABankTran.payeeBAccountID>((object) row, ref payeeBaccountId);
        sender.RaiseFieldUpdated<PX.Objects.CA.CABankTran.payeeBAccountID>((object) row, (object) null);
        if (str != null)
        {
          try
          {
            sender.SetValueExt<PX.Objects.CA.CABankTran.paymentMethodID>((object) row, (object) str);
            sender.SetValueExt<PX.Objects.CA.CABankTran.pMInstanceID>((object) row, (object) nullable1);
          }
          catch (PXSetPropertyException ex)
          {
          }
        }
        try
        {
          CABankTranAdjustment bankTranAdjustment = ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Insert(new CABankTranAdjustment()
          {
            TranID = row.TranID
          });
          bankTranAdjustment.AdjdDocType = "INV";
          bankTranAdjustment.AdjdRefNbr = refNbr;
          ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Update(bankTranAdjustment);
        }
        catch
        {
          throw new PXSetPropertyException("Could not add application of '{0}' invoice. Possibly it is already used in another application", new object[1]
          {
            (object) row.InvoiceInfo
          });
        }
        return true;
      }
      sender.SetValue<PX.Objects.CA.CABankTran.invoiceNotFound>((object) row, (object) true);
    }
    return false;
  }

  protected virtual bool AttemptApplyRules(PX.Objects.CA.CABankTran transaction)
  {
    if (transaction == null || transaction.RuleID.HasValue)
      return false;
    foreach (PXResult<CABankTranRule> pxResult in ((PXSelectBase<CABankTranRule>) this.Rules).Select(Array.Empty<object>()))
    {
      CABankTranRule rule = PXResult<CABankTranRule>.op_Implicit(pxResult);
      if (this.CheckRuleMatches(transaction, rule))
      {
        try
        {
          this.ApplyRule(transaction, rule);
          ((PXSelectBase) this.CABankTran).Cache.RaiseExceptionHandling<PX.Objects.CA.CABankTran.entryTypeID>((object) transaction, (object) transaction.EntryTypeID, (Exception) null);
          PXUIFieldAttribute.SetError<PX.Objects.CA.CABankTran.ruleID>(((PXSelectBase) this.CABankTran).Cache, (object) transaction, (string) null);
          return true;
        }
        catch (PXException ex)
        {
          ((PXSelectBase) this.CABankTran).Cache.RaiseExceptionHandling<PX.Objects.CA.CABankTran.entryTypeID>((object) transaction, (object) transaction.EntryTypeID, (Exception) null);
          PXUIFieldAttribute.SetWarning<PX.Objects.CA.CABankTran.ruleID>(((PXSelectBase) this.CABankTran).Cache, (object) transaction, "Failed to apply a rule due to data validation.");
          ((PXSelectBase) this.CABankTran).Cache.SetDefaultExt<PX.Objects.CA.CABankTran.ruleID>((object) transaction);
          ((PXSelectBase) this.CABankTran).Cache.SetDefaultExt<PX.Objects.CA.CABankTran.origModule>((object) transaction);
          ((PXSelectBase) this.CABankTran).Cache.SetDefaultExt<PX.Objects.CA.CABankTran.curyTotalAmt>((object) transaction);
        }
      }
    }
    return false;
  }

  protected virtual bool CheckRuleMatches(PX.Objects.CA.CABankTran transaction, CABankTranRule rule)
  {
    return this.MatchingService.CheckRuleMatches(transaction, rule);
  }

  protected virtual void ApplyRule(PX.Objects.CA.CABankTran transaction, CABankTranRule rule)
  {
    if (rule.Action == "C")
    {
      transaction.OrigModule = rule.DocumentModule;
      transaction.EntryTypeID = rule.DocumentEntryTypeID;
    }
    else if (rule.Action == "H")
    {
      transaction.CreateDocument = new bool?(false);
      transaction.DocumentMatched = new bool?(false);
      transaction.Hidden = new bool?(true);
      transaction.Processed = new bool?(true);
    }
    transaction.RuleID = rule.RuleID;
  }

  protected virtual void CABankTran_OrigModule_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    PX.Objects.CA.CABankTran row = (PX.Objects.CA.CABankTran) e.Row;
    if (row != null)
    {
      bool? nullable = row.CreateDocument;
      if (nullable.GetValueOrDefault())
      {
        CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(((PXSelectBase<CashAccount>) this.cashAccount).Select(new object[1]
        {
          (object) row.CashAccountID
        }));
        int num;
        if (cashAccount == null)
        {
          num = 0;
        }
        else
        {
          nullable = cashAccount.ClearingAccount;
          num = nullable.GetValueOrDefault() ? 1 : 0;
        }
        if (num != 0)
        {
          e.NewValue = (object) "AR";
          goto label_14;
        }
        if (!string.IsNullOrEmpty(row.InvoiceInfo))
        {
          if (row.DrCr == "C")
          {
            e.NewValue = (object) "AP";
            goto label_14;
          }
          if (row.DrCr == "D")
          {
            e.NewValue = (object) "AR";
            goto label_14;
          }
          goto label_14;
        }
        e.NewValue = (object) "CA";
        goto label_14;
      }
    }
    e.NewValue = (object) null;
label_14:
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CABankTranAdjustment_AdjdRefNbr_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CABankTranAdjustment row = (CABankTranAdjustment) e.Row;
    PX.Objects.CA.CABankTran tran = ((PXSelectBase<PX.Objects.CA.CABankTran>) this.CABankTranSelection).SelectSingle(new object[1]
    {
      (object) row.TranID
    });
    row.AdjgDocDate = (DateTime?) tran?.TranDate;
    row.Released = new bool?(false);
    row.CuryDocBal = new Decimal?();
    row.CuryDiscBal = new Decimal?();
    row.CuryWhTaxBal = new Decimal?();
    if (string.IsNullOrEmpty(row.AdjdRefNbr))
    {
      sender.SetValueExt<CABankTranAdjustment.curyAdjgAmt>((object) row, (object) null);
      sender.SetValueExt<CABankTranAdjustment.curyAdjgDiscAmt>((object) row, (object) null);
      sender.SetValueExt<CABankTranAdjustment.curyAdjgWhTaxAmt>((object) row, (object) null);
      sender.SetValueExt<CABankTranAdjustment.curyAdjgWOAmt>((object) row, (object) null);
    }
    Decimal? nullable1 = row.CuryAdjgAmt;
    Decimal num;
    if (nullable1.HasValue)
    {
      nullable1 = row.CuryAdjgAmt;
      num = 0.0M;
      if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
      {
        sender.SetValueExt<CABankTranAdjustment.curyAdjgAmt>((object) row, (object) row.CuryAdjgAmt);
        goto label_9;
      }
    }
    CABankTranAdjustment bankTranAdjustment = row;
    nullable1 = new Decimal?();
    Decimal? nullable2 = nullable1;
    bankTranAdjustment.CuryAdjgAmt = nullable2;
    if (tran.OrigModule == "AP")
    {
      this.PopulateAdjustmentFieldsAP(tran, row);
      sender.SetDefaultExt<CABankTranAdjustment.adjdTranPeriodID>(e.Row);
    }
    else if (tran.OrigModule == "AR")
    {
      this.PopulateAdjustmentFieldsAR(tran, row);
      sender.SetDefaultExt<CABankTranAdjustment.adjdTranPeriodID>(e.Row);
    }
label_9:
    nullable1 = row.CuryAdjgDiscAmt;
    if (nullable1.HasValue)
    {
      nullable1 = row.CuryAdjgDiscAmt;
      num = 0.0M;
      if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
        sender.SetValueExt<CABankTranAdjustment.curyAdjgDiscAmt>((object) row, (object) row.CuryAdjgDiscAmt);
    }
    nullable1 = row.CuryAdjgWhTaxAmt;
    if (!nullable1.HasValue)
      return;
    nullable1 = row.CuryAdjgWhTaxAmt;
    num = 0.0M;
    if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
      return;
    sender.SetValueExt<CABankTranAdjustment.curyAdjgWhTaxAmt>((object) row, (object) row.CuryAdjgWhTaxAmt);
  }

  protected virtual void PopulateAdjustmentFieldsAR(PX.Objects.CA.CABankTran tran, CABankTranAdjustment adj)
  {
    ((PXGraph) this).GetExtension<StatementApplicationBalancesProto>().PopulateAdjustmentFieldsAR(tran, adj);
  }

  protected virtual void PopulateAdjustmentFieldsAP(PX.Objects.CA.CABankTran tran, CABankTranAdjustment adj)
  {
    ((PXGraph) this).GetExtension<StatementApplicationBalancesProto>().PopulateAdjustmentFieldsAP(tran, adj);
  }

  protected virtual void CABankTranAdjustment_AdjdRefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    CABankTranAdjustment row = (CABankTranAdjustment) e.Row;
    if (row == null)
      return;
    foreach (PXResult<CABankTranAdjustment> pxResult in ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Select(new object[1]
    {
      (object) row.TranID
    }))
    {
      CABankTranAdjustment bankTranAdjustment = PXResult<CABankTranAdjustment>.op_Implicit(pxResult);
      if (row != bankTranAdjustment && bankTranAdjustment.AdjdDocType == row.AdjdDocType && bankTranAdjustment.AdjdRefNbr == (string) e.NewValue && bankTranAdjustment.AdjdModule == row.AdjdModule)
        throw new PXSetPropertyException<CABankTranAdjustment.adjdRefNbr>("The payment is already applied to this document.");
    }
  }

  protected virtual void CABankTranAdjustment_AdjdCuryRate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if ((Decimal) e.NewValue <= 0M)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
      {
        (object) 0.ToString()
      });
  }

  protected virtual void CABankTranAdjustment_CuryAdjgAmt_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    CABankTranAdjustment row = (CABankTranAdjustment) e.Row;
    if (row != null && row.AdjdRefNbr != null)
    {
      Decimal? nullable = row.CuryDocBal;
      if (nullable.HasValue)
      {
        nullable = row.CuryDiscBal;
        if (nullable.HasValue)
        {
          nullable = row.CuryWhTaxBal;
          if (nullable.HasValue)
            goto label_5;
        }
      }
      this.UpdateBalance((CABankTranAdjustment) e.Row, false);
label_5:
      int? voidAdjNbr = row.VoidAdjNbr;
      if (!voidAdjNbr.HasValue && (Decimal) e.NewValue < 0M)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
        {
          (object) 0.ToString()
        });
      voidAdjNbr = row.VoidAdjNbr;
      if (voidAdjNbr.HasValue && (Decimal) e.NewValue > 0M)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
        {
          (object) 0.ToString()
        });
    }
    else
      e.NewValue = (object) 0M;
  }

  protected virtual void CABankTranAdjustment_CuryAdjgAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.OldValue != null)
    {
      Decimal? nullable = ((CABankTranAdjustment) e.Row).CuryDocBal;
      Decimal num = 0M;
      if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      {
        nullable = ((CABankTranAdjustment) e.Row).CuryAdjgAmt;
        Decimal oldValue = (Decimal) e.OldValue;
        if (nullable.GetValueOrDefault() < oldValue & nullable.HasValue)
          ((CABankTranAdjustment) e.Row).CuryAdjgDiscAmt = new Decimal?(0M);
      }
    }
    this.UpdateBalance((CABankTranAdjustment) e.Row, true);
  }

  protected virtual void CABankTranAdjustment_CuryAdjgDiscAmt_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    CABankTranAdjustment row = (CABankTranAdjustment) e.Row;
    if (row != null && row.AdjdRefNbr != null)
    {
      Decimal? nullable1 = row.CuryDocBal;
      if (nullable1.HasValue)
      {
        nullable1 = row.CuryDiscBal;
        if (nullable1.HasValue)
        {
          nullable1 = row.CuryWhTaxBal;
          if (nullable1.HasValue)
            goto label_5;
        }
      }
      this.UpdateBalance((CABankTranAdjustment) e.Row, false);
label_5:
      int? voidAdjNbr = row.VoidAdjNbr;
      if (!voidAdjNbr.HasValue && (Decimal) e.NewValue < 0M)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
        {
          (object) 0.ToString()
        });
      voidAdjNbr = row.VoidAdjNbr;
      if (voidAdjNbr.HasValue && (Decimal) e.NewValue > 0M)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
        {
          (object) 0.ToString()
        });
      nullable1 = row.CuryDiscBal;
      Decimal num1 = nullable1.Value;
      nullable1 = row.CuryAdjgDiscAmt;
      Decimal num2 = nullable1.Value;
      if (num1 + num2 - (Decimal) e.NewValue < 0M)
      {
        object[] objArray = new object[1];
        nullable1 = row.CuryDiscBal;
        Decimal num3 = nullable1.Value;
        nullable1 = row.CuryAdjgDiscAmt;
        Decimal num4 = nullable1.Value;
        objArray[0] = (object) (num3 + num4).ToString();
        throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray);
      }
      nullable1 = row.CuryAdjgAmt;
      if (!nullable1.HasValue)
        return;
      Decimal? nullable2;
      if (sender.GetValuePending<CABankTranAdjustment.curyAdjgAmt>(e.Row) != PXCache.NotSetValue)
      {
        nullable1 = (Decimal?) sender.GetValuePending<CABankTranAdjustment.curyAdjgAmt>(e.Row);
        nullable2 = row.CuryAdjgAmt;
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
          return;
      }
      nullable2 = row.CuryDocBal;
      Decimal num5 = nullable2.Value;
      nullable2 = row.CuryAdjgDiscAmt;
      Decimal num6 = nullable2.Value;
      if (num5 + num6 - (Decimal) e.NewValue < 0M)
      {
        object[] objArray = new object[1];
        nullable2 = row.CuryDocBal;
        Decimal num7 = nullable2.Value;
        nullable2 = row.CuryAdjgDiscAmt;
        Decimal num8 = nullable2.Value;
        objArray[0] = (object) (num7 + num8).ToString();
        throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray);
      }
    }
    else
      e.NewValue = (object) 0M;
  }

  protected virtual void CABankTranAdjustment_CuryAdjgDiscAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.UpdateBalance((CABankTranAdjustment) e.Row, true);
  }

  protected virtual void CABankTranAdjustment_CuryAdjgWhTaxAmt_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    CABankTranAdjustment row = (CABankTranAdjustment) e.Row;
    if (row != null && row.AdjdRefNbr != null)
    {
      Decimal? nullable1 = row.CuryDocBal;
      if (nullable1.HasValue)
      {
        nullable1 = row.CuryDiscBal;
        if (nullable1.HasValue)
        {
          nullable1 = row.CuryWhTaxBal;
          if (nullable1.HasValue)
            goto label_5;
        }
      }
      this.UpdateBalance((CABankTranAdjustment) e.Row, false);
label_5:
      int? voidAdjNbr = row.VoidAdjNbr;
      if (!voidAdjNbr.HasValue && (Decimal) e.NewValue < 0M)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
        {
          (object) 0.ToString()
        });
      voidAdjNbr = row.VoidAdjNbr;
      if (voidAdjNbr.HasValue && (Decimal) e.NewValue > 0M)
        throw new PXSetPropertyException("Incorrect value. The value to be entered must be less than or equal to {0}.", new object[1]
        {
          (object) 0.ToString()
        });
      nullable1 = row.CuryWhTaxBal;
      Decimal num1 = nullable1.Value;
      nullable1 = row.CuryAdjgWhTaxAmt;
      Decimal num2 = nullable1.Value;
      if (num1 + num2 - (Decimal) e.NewValue < 0M)
      {
        object[] objArray = new object[1];
        nullable1 = row.CuryWhTaxBal;
        Decimal num3 = nullable1.Value;
        nullable1 = row.CuryAdjgWhTaxAmt;
        Decimal num4 = nullable1.Value;
        objArray[0] = (object) (num3 + num4).ToString();
        throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray);
      }
      nullable1 = row.CuryAdjgAmt;
      if (!nullable1.HasValue)
        return;
      Decimal? nullable2;
      if (sender.GetValuePending<CABankTranAdjustment.curyAdjgAmt>(e.Row) != PXCache.NotSetValue)
      {
        nullable1 = (Decimal?) sender.GetValuePending<CABankTranAdjustment.curyAdjgAmt>(e.Row);
        nullable2 = row.CuryAdjgAmt;
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
          return;
      }
      nullable2 = row.CuryDocBal;
      Decimal num5 = nullable2.Value;
      nullable2 = row.CuryAdjgWhTaxAmt;
      Decimal num6 = nullable2.Value;
      if (num5 + num6 - (Decimal) e.NewValue < 0M)
      {
        object[] objArray = new object[1];
        nullable2 = row.CuryDocBal;
        Decimal num7 = nullable2.Value;
        nullable2 = row.CuryAdjgWhTaxAmt;
        Decimal num8 = nullable2.Value;
        objArray[0] = (object) (num7 + num8).ToString();
        throw new PXSetPropertyException("The amount must be less than or equal to {0}.", objArray);
      }
    }
    else
      e.NewValue = (object) 0M;
  }

  protected virtual void CABankTranAdjustment_WriteOffReasonCode_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    CABankTranAdjustment row = (CABankTranAdjustment) e.Row;
    if (row == null || row.AdjdRefNbr == null)
      return;
    PX.Objects.CS.ReasonCode reasonCode = PX.Objects.CS.ReasonCode.PK.Find((PXGraph) this, (string) e.NewValue);
    Decimal? curyAdjgWoAmt;
    if (reasonCode?.Usage == "B")
    {
      curyAdjgWoAmt = row.CuryAdjgWOAmt;
      if (curyAdjgWoAmt.Value < 0M)
        throw new PXSetPropertyException("For a negative write-off amount, specify the Credit write-off reason code.");
    }
    if (!(reasonCode?.Usage == "C"))
      return;
    curyAdjgWoAmt = row.CuryAdjgWOAmt;
    if (curyAdjgWoAmt.Value > 0M)
      throw new PXSetPropertyException("For a positive write-off amount, specify the Balance write-off reason code.");
  }

  protected virtual void CABankTranAdjustment_CuryAdjgWOAmt_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    CABankTranAdjustment row = (CABankTranAdjustment) e.Row;
    if (row != null && row.AdjdRefNbr != null)
    {
      Decimal? nullable = row.CuryDocBal;
      if (nullable.HasValue)
      {
        nullable = row.CuryDiscBal;
        if (nullable.HasValue)
        {
          nullable = row.CuryWhTaxBal;
          if (nullable.HasValue)
            goto label_5;
        }
      }
      this.UpdateBalance(row, false);
label_5:
      nullable = row.CuryWhTaxBal;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = row.CuryAdjgWOAmt;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      if (valueOrDefault1 + valueOrDefault2 - (Decimal) e.NewValue < 0M)
      {
        object[] objArray = new object[1];
        nullable = row.CuryWhTaxBal;
        Decimal valueOrDefault3 = nullable.GetValueOrDefault();
        nullable = row.CuryAdjgWOAmt;
        Decimal valueOrDefault4 = nullable.GetValueOrDefault();
        objArray[0] = (object) (valueOrDefault3 + valueOrDefault4).ToString();
        throw new PXSetPropertyException("The customer's write-off limit {0} has been exceeded.", objArray);
      }
    }
    else
      e.NewValue = (object) 0M;
  }

  protected virtual void CABankTranAdjustment_CuryAdjgWOAmt_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.UpdateBalance((CABankTranAdjustment) e.Row, false);
    string name = typeof (CABankTranAdjustment.writeOffReasonCode).Name;
    object obj = sender.GetValue(sender.Current, name);
    try
    {
      sender.RaiseFieldVerifying(name, sender.Current, ref obj);
    }
    catch (PXSetPropertyException ex)
    {
      sender.RaiseExceptionHandling(name, sender.Current, obj, (Exception) ex);
    }
  }

  protected virtual void CABankTranAdjustment_CuryDocBal_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row != null && ((CABankTranAdjustment) e.Row).AdjdCuryInfoID.HasValue && !((CABankTranAdjustment) e.Row).CuryDocBal.HasValue && sender.GetStatus(e.Row) != 3)
      this.UpdateBalance((CABankTranAdjustment) e.Row, false);
    if (e.Row != null)
      e.NewValue = (object) ((CABankTranAdjustment) e.Row).CuryDocBal;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CABankTranAdjustment_CuryDiscBal_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row != null && ((CABankTranAdjustment) e.Row).AdjdCuryInfoID.HasValue && !((CABankTranAdjustment) e.Row).CuryDiscBal.HasValue && sender.GetStatus(e.Row) != 3)
      this.UpdateBalance((CABankTranAdjustment) e.Row, false);
    if (e.Row != null)
      e.NewValue = (object) ((CABankTranAdjustment) e.Row).CuryDiscBal;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CABankTranAdjustment_CuryWhTaxBal_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (e.Row != null && ((CABankTranAdjustment) e.Row).AdjdCuryInfoID.HasValue && !((CABankTranAdjustment) e.Row).CuryWhTaxBal.HasValue && sender.GetStatus(e.Row) != 3)
      this.UpdateBalance((CABankTranAdjustment) e.Row, false);
    if (e.Row != null)
      e.NewValue = (object) ((CABankTranAdjustment) e.Row).CuryWhTaxBal;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CABankTranAdjustment_AdjdDocType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CABankTranAdjustment row) || !(row.AdjdDocType != (string) e.OldValue))
      return;
    sender.SetValueExt<CABankTranAdjustment.adjdRefNbr>((object) row, (object) null);
    sender.SetValueExt<CABankTranAdjustment.curyAdjgAmt>((object) row, (object) 0M);
  }

  protected virtual void CABankTranAdjustment_RowInserting(
    PXCache sender,
    PXRowInsertingEventArgs e)
  {
    CABankTranAdjustment row = (CABankTranAdjustment) e.Row;
    if (row == null)
      return;
    foreach (PXResult<CABankTranAdjustment> pxResult in ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Select(Array.Empty<object>()))
    {
      CABankTranAdjustment bankTranAdjustment = PXResult<CABankTranAdjustment>.op_Implicit(pxResult);
      if (row.AdjdRefNbr != null && bankTranAdjustment.AdjdRefNbr == row.AdjdRefNbr && bankTranAdjustment.AdjdDocType == row.AdjdDocType)
      {
        PXEntryStatus status = ((PXSelectBase) this.Adjustments).Cache.GetStatus((object) bankTranAdjustment);
        if (status != 4 && status != 3)
        {
          sender.RaiseExceptionHandling<CABankTranAdjustment.adjdRefNbr>(e.Row, (object) null, (Exception) new PXException("Record with this ID already exists."));
          ((CancelEventArgs) e).Cancel = true;
          break;
        }
      }
    }
  }

  protected virtual void CABankTranAdjustment_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    CABankTranAdjustment row = (CABankTranAdjustment) e.Row;
    if (row == null)
      return;
    PX.Objects.CA.CABankTran caBankTran = ((PXSelectBase<PX.Objects.CA.CABankTran>) this.CABankTranSelection).SelectSingle(new object[1]
    {
      (object) row.TranID
    });
    sender.SetValue<CABankTranAdjustment.adjdModule>((object) row, (object) caBankTran.OrigModule);
  }

  public virtual void UpdateBalance(CABankTranAdjustment adj, bool isCalcRGOL)
  {
    if (adj.AdjdDocType == null || adj.AdjdRefNbr == null)
      return;
    PX.Objects.CA.CABankTran currentDetail = ((PXSelectBase<PX.Objects.CA.CABankTran>) this.CABankTranSelection).SelectSingle(new object[1]
    {
      (object) adj.TranID
    });
    ((PXGraph) this).GetExtension<StatementApplicationBalancesProto>().UpdateBalance(currentDetail, adj, isCalcRGOL);
  }

  protected virtual void CABankTranDetail_AccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if ((e.Row is CABankTranDetail row ? (!row.BankTranID.HasValue ? 1 : 0) : 1) != 0)
      return;
    PX.Objects.CA.CABankTran caBankTran = PXResultset<PX.Objects.CA.CABankTran>.op_Implicit(PXSelectBase<PX.Objects.CA.CABankTran, PXSelect<PX.Objects.CA.CABankTran, Where<PX.Objects.CA.CABankTran.tranID, Equal<Required<CABankTranDetail.bankTranID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.BankTranID
    }));
    if (caBankTran == null || caBankTran.EntryTypeID == null || row == null)
      return;
    e.NewValue = (object) this.GetDefaultAccountValues((PXGraph) this, caBankTran.CashAccountID, caBankTran.EntryTypeID).AccountID;
    ((CancelEventArgs) e).Cancel = e.NewValue != null;
  }

  protected virtual void CABankTranDetail_AccountID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    CATranDetailHelper.OnAccountIdFieldUpdatedEvent(cache, e);
    if (((CABankTranDetail) e.Row).InventoryID.HasValue)
      return;
    cache.SetDefaultExt<CABankTranDetail.taxCategoryID>(e.Row);
  }

  protected virtual void CABankTranDetail_BranchID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    CABankTranDetail row = e.Row as CABankTranDetail;
    PX.Objects.CA.CABankTran current = ((PXSelectBase<PX.Objects.CA.CABankTran>) this.CABankTran).Current;
    if (current == null || current.EntryTypeID == null || row == null)
      return;
    e.NewValue = (object) this.GetDefaultAccountValues((PXGraph) this, current.CashAccountID, current.EntryTypeID).BranchID;
    ((CancelEventArgs) e).Cancel = e.NewValue != null;
  }

  protected virtual void CABankTranDetail_CashAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    CATranDetailHelper.OnCashAccountIdFieldDefaultingEvent(sender, e);
  }

  protected virtual void CABankTranDetail_CashAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CATranDetailHelper.OnCashAccountIdFieldUpdatedEvent(sender, e);
  }

  protected virtual void CABankTranDetail_InventoryId_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CABankTranDetail row = e.Row as CABankTranDetail;
    PX.Objects.CA.CABankTran caBankTran = PXResultset<PX.Objects.CA.CABankTran>.op_Implicit(PXSelectBase<PX.Objects.CA.CABankTran, PXSelect<PX.Objects.CA.CABankTran, Where<PX.Objects.CA.CABankTran.tranID, Equal<Required<CABankTranDetail.bankTranID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.BankTranID
    }));
    if (row == null || !row.InventoryID.HasValue)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<CABankTranDetail.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.InventoryID
    }));
    if (inventoryItem != null && caBankTran != null)
    {
      if (caBankTran.DrCr == "D")
      {
        row.AccountID = inventoryItem.SalesAcctID;
        row.SubID = inventoryItem.SalesSubID;
      }
      else
      {
        row.AccountID = inventoryItem.COGSAcctID;
        row.SubID = inventoryItem.COGSSubID;
      }
    }
    sender.SetDefaultExt<CABankTranDetail.taxCategoryID>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CABankTranDetail, CABankTranDetail.taxCategoryID> e)
  {
    CABankTranDetail row = e.Row;
    if (row == null || TaxBaseAttribute.GetTaxCalc<CABankTranDetail.taxCategoryID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CABankTranDetail, CABankTranDetail.taxCategoryID>>) e).Cache, (object) row) != TaxCalc.Calc || row.InventoryID.HasValue)
      return;
    PX.Objects.GL.Account account = (PX.Objects.GL.Account) null;
    if (row.AccountID.HasValue)
      account = PX.Objects.GL.Account.PK.Find((PXGraph) this, row.AccountID);
    PX.Objects.TX.TaxZone taxZone = PXResultset<PX.Objects.TX.TaxZone>.op_Implicit(((PXSelectBase<PX.Objects.TX.TaxZone>) this.Taxzone).Select(new object[1]
    {
      (object) ((PXSelectBase<PX.Objects.CA.CABankTran>) this.CABankTran)?.Current?.TaxZoneID
    }));
    if (account != null && account.TaxCategoryID != null)
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTranDetail, CABankTranDetail.taxCategoryID>, CABankTranDetail, object>) e).NewValue = (object) account.TaxCategoryID;
    else if (taxZone != null && !string.IsNullOrEmpty(taxZone.DfltTaxCategoryID))
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTranDetail, CABankTranDetail.taxCategoryID>, CABankTranDetail, object>) e).NewValue = (object) taxZone.DfltTaxCategoryID;
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTranDetail, CABankTranDetail.taxCategoryID>, CABankTranDetail, object>) e).NewValue = (object) row.TaxCategoryID;
  }

  protected virtual void CABankTranDetail_Qty_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    object row = e.Row;
    e.NewValue = (object) 1.0M;
  }

  protected virtual void CABankTranDetail_SubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    CABankTranDetail row = e.Row as CABankTranDetail;
    PX.Objects.CA.CABankTran caBankTran = PXResultset<PX.Objects.CA.CABankTran>.op_Implicit(PXSelectBase<PX.Objects.CA.CABankTran, PXSelect<PX.Objects.CA.CABankTran, Where<PX.Objects.CA.CABankTran.tranID, Equal<Required<CABankTranDetail.bankTranID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.BankTranID
    }));
    if (caBankTran == null || caBankTran.EntryTypeID == null || row == null)
      return;
    e.NewValue = (object) this.GetDefaultAccountValues((PXGraph) this, caBankTran.CashAccountID, caBankTran.EntryTypeID).SubID;
    ((CancelEventArgs) e).Cancel = e.NewValue != null;
  }

  protected virtual void CABankTranDetail_TranDesc_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    PX.Objects.CA.CABankTran caBankTran = PXResultset<PX.Objects.CA.CABankTran>.op_Implicit(PXSelectBase<PX.Objects.CA.CABankTran, PXSelect<PX.Objects.CA.CABankTran, Where<PX.Objects.CA.CABankTran.tranID, Equal<Required<CABankTranDetail.bankTranID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) (e.Row as CABankTranDetail).BankTranID
    }));
    if (caBankTran == null || caBankTran.EntryTypeID == null)
      return;
    CAEntryType caEntryType = PXResultset<CAEntryType>.op_Implicit(PXSelectBase<CAEntryType, PXSelect<CAEntryType, Where<CAEntryType.entryTypeId, Equal<Required<CAEntryType.entryTypeId>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) caBankTran.EntryTypeID
    }));
    if (caEntryType == null)
      return;
    e.NewValue = (object) caEntryType.Descr;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.taxType> e)
  {
    PX.Objects.CA.CABankTran caBankTran = PXResultset<PX.Objects.CA.CABankTran>.op_Implicit(PXSelectBase<PX.Objects.CA.CABankTran, PXSelect<PX.Objects.CA.CABankTran, Where<PX.Objects.CA.CABankTran.tranID, Equal<Required<CABankTranDetail.bankTranID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.BankTranID
    }));
    if (e.Row == null || caBankTran == null)
      return;
    if (caBankTran.DrCr == "C")
    {
      PurchaseTax purchaseTax = PXResultset<PurchaseTax>.op_Implicit(PXSelectBase<PurchaseTax, PXSelect<PurchaseTax, Where<PurchaseTax.taxID, Equal<Required<PurchaseTax.taxID>>>>.Config>.Select(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.taxType>>) e).Cache.Graph, new object[1]
      {
        (object) e.Row.TaxID
      }));
      if (purchaseTax == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.taxType>, CABankTaxTran, object>) e).NewValue = (object) purchaseTax.TranTaxType;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.taxType>>) e).Cancel = true;
    }
    else
    {
      PX.Objects.AR.SalesTax salesTax = PXResultset<PX.Objects.AR.SalesTax>.op_Implicit(PXSelectBase<PX.Objects.AR.SalesTax, PXSelect<PX.Objects.AR.SalesTax, Where<PX.Objects.AR.SalesTax.taxID, Equal<Required<PX.Objects.AR.SalesTax.taxID>>>>.Config>.Select(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.taxType>>) e).Cache.Graph, new object[1]
      {
        (object) e.Row.TaxID
      }));
      if (salesTax == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.taxType>, CABankTaxTran, object>) e).NewValue = (object) salesTax.TranTaxType;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.taxType>>) e).Cancel = true;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.accountID> e)
  {
    PX.Objects.CA.CABankTran caBankTran = PXResultset<PX.Objects.CA.CABankTran>.op_Implicit(PXSelectBase<PX.Objects.CA.CABankTran, PXSelect<PX.Objects.CA.CABankTran, Where<PX.Objects.CA.CABankTran.tranID, Equal<Required<CABankTranDetail.bankTranID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.BankTranID
    }));
    if (e.Row == null || caBankTran == null)
      return;
    if (caBankTran.DrCr == "C")
    {
      PurchaseTax purchaseTax = PXResultset<PurchaseTax>.op_Implicit(PXSelectBase<PurchaseTax, PXSelect<PurchaseTax, Where<PurchaseTax.taxID, Equal<Required<PurchaseTax.taxID>>>>.Config>.Select(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.accountID>>) e).Cache.Graph, new object[1]
      {
        (object) e.Row.TaxID
      }));
      if (purchaseTax == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.accountID>, CABankTaxTran, object>) e).NewValue = (object) purchaseTax.HistTaxAcctID;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.accountID>>) e).Cancel = true;
    }
    else
    {
      PX.Objects.AR.SalesTax salesTax = PXResultset<PX.Objects.AR.SalesTax>.op_Implicit(PXSelectBase<PX.Objects.AR.SalesTax, PXSelect<PX.Objects.AR.SalesTax, Where<PX.Objects.AR.SalesTax.taxID, Equal<Required<PX.Objects.AR.SalesTax.taxID>>>>.Config>.Select(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.accountID>>) e).Cache.Graph, new object[1]
      {
        (object) e.Row.TaxID
      }));
      if (salesTax == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.accountID>, CABankTaxTran, object>) e).NewValue = (object) salesTax.HistTaxAcctID;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.accountID>>) e).Cancel = true;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.subID> e)
  {
    PX.Objects.CA.CABankTran caBankTran = PXResultset<PX.Objects.CA.CABankTran>.op_Implicit(PXSelectBase<PX.Objects.CA.CABankTran, PXSelect<PX.Objects.CA.CABankTran, Where<PX.Objects.CA.CABankTran.tranID, Equal<Required<CABankTranDetail.bankTranID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.BankTranID
    }));
    if (e.Row == null || caBankTran == null)
      return;
    if (caBankTran.DrCr == "C")
    {
      PurchaseTax purchaseTax = PXResultset<PurchaseTax>.op_Implicit(PXSelectBase<PurchaseTax, PXSelect<PurchaseTax, Where<PurchaseTax.taxID, Equal<Required<PurchaseTax.taxID>>>>.Config>.Select(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.subID>>) e).Cache.Graph, new object[1]
      {
        (object) e.Row.TaxID
      }));
      if (purchaseTax == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.subID>, CABankTaxTran, object>) e).NewValue = (object) purchaseTax.HistTaxSubID;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.subID>>) e).Cancel = true;
    }
    else
    {
      PX.Objects.AR.SalesTax salesTax = PXResultset<PX.Objects.AR.SalesTax>.op_Implicit(PXSelectBase<PX.Objects.AR.SalesTax, PXSelect<PX.Objects.AR.SalesTax, Where<PX.Objects.AR.SalesTax.taxID, Equal<Required<PX.Objects.AR.SalesTax.taxID>>>>.Config>.Select(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.subID>>) e).Cache.Graph, new object[1]
      {
        (object) e.Row.TaxID
      }));
      if (salesTax == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.subID>, CABankTaxTran, object>) e).NewValue = (object) salesTax.HistTaxSubID;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.subID>>) e).Cancel = true;
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CABankTaxTran> e)
  {
    PX.Objects.CA.CABankTran caBankTran = (PX.Objects.CA.CABankTran) PXParentAttribute.SelectParent(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CABankTaxTran>>) e).Cache, (object) e.Row);
    if (caBankTran != null && (e.Operation == 2 || e.Operation == 1))
    {
      e.Row.TaxZoneID = caBankTran.TaxZoneID;
      PXDefaultAttribute.SetPersistingCheck<CABankTaxTran.taxZoneID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CABankTaxTran>>) e).Cache, (object) e.Row, (PXPersistingCheck) 1);
    }
    else
      PXDefaultAttribute.SetPersistingCheck<CABankTaxTran.taxZoneID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CABankTaxTran>>) e).Cache, (object) e.Row, (PXPersistingCheck) 2);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.CA.CABankTran, PX.Objects.CA.CABankTran.chargeTaxCalcMode> e)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>())
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CA.CABankTran, PX.Objects.CA.CABankTran.chargeTaxCalcMode>, PX.Objects.CA.CABankTran, object>) e).NewValue = (object) "G";
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CA.CABankTran, PX.Objects.CA.CABankTran.chargeTaxCalcMode>, PX.Objects.CA.CABankTran, object>) e).NewValue = (object) "T";
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.taxType> e)
  {
    PX.Objects.CA.CABankTran caBankTran = PXResultset<PX.Objects.CA.CABankTran>.op_Implicit(PXSelectBase<PX.Objects.CA.CABankTran, PXSelect<PX.Objects.CA.CABankTran, Where<PX.Objects.CA.CABankTran.tranID, Equal<Required<CABankTranDetail.bankTranID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.BankTranID
    }));
    if (e.Row == null || caBankTran == null)
      return;
    if (caBankTran.DrCr == "C")
    {
      PurchaseTax purchaseTax = PXResultset<PurchaseTax>.op_Implicit(PXSelectBase<PurchaseTax, PXSelect<PurchaseTax, Where<PurchaseTax.taxID, Equal<Required<PurchaseTax.taxID>>>>.Config>.Select(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.taxType>>) e).Cache.Graph, new object[1]
      {
        (object) e.Row.TaxID
      }));
      if (purchaseTax == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.taxType>, CABankTaxTranMatch, object>) e).NewValue = (object) purchaseTax.TranTaxType;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.taxType>>) e).Cancel = true;
    }
    else
    {
      PX.Objects.AR.SalesTax salesTax = PXResultset<PX.Objects.AR.SalesTax>.op_Implicit(PXSelectBase<PX.Objects.AR.SalesTax, PXSelect<PX.Objects.AR.SalesTax, Where<PX.Objects.AR.SalesTax.taxID, Equal<Required<PX.Objects.AR.SalesTax.taxID>>>>.Config>.Select(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.taxType>>) e).Cache.Graph, new object[1]
      {
        (object) e.Row.TaxID
      }));
      if (salesTax == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.taxType>, CABankTaxTranMatch, object>) e).NewValue = (object) salesTax.TranTaxType;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.taxType>>) e).Cancel = true;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.accountID> e)
  {
    PX.Objects.CA.CABankTran caBankTran = PXResultset<PX.Objects.CA.CABankTran>.op_Implicit(PXSelectBase<PX.Objects.CA.CABankTran, PXSelect<PX.Objects.CA.CABankTran, Where<PX.Objects.CA.CABankTran.tranID, Equal<Required<CABankTranDetail.bankTranID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.BankTranID
    }));
    if (e.Row == null || caBankTran == null)
      return;
    if (caBankTran.DrCr == "C")
    {
      PurchaseTax purchaseTax = PXResultset<PurchaseTax>.op_Implicit(PXSelectBase<PurchaseTax, PXSelect<PurchaseTax, Where<PurchaseTax.taxID, Equal<Required<PurchaseTax.taxID>>>>.Config>.Select(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.accountID>>) e).Cache.Graph, new object[1]
      {
        (object) e.Row.TaxID
      }));
      if (purchaseTax == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.accountID>, CABankTaxTranMatch, object>) e).NewValue = (object) purchaseTax.HistTaxAcctID;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.accountID>>) e).Cancel = true;
    }
    else
    {
      PX.Objects.AR.SalesTax salesTax = PXResultset<PX.Objects.AR.SalesTax>.op_Implicit(PXSelectBase<PX.Objects.AR.SalesTax, PXSelect<PX.Objects.AR.SalesTax, Where<PX.Objects.AR.SalesTax.taxID, Equal<Required<PX.Objects.AR.SalesTax.taxID>>>>.Config>.Select(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.accountID>>) e).Cache.Graph, new object[1]
      {
        (object) e.Row.TaxID
      }));
      if (salesTax == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.accountID>, CABankTaxTranMatch, object>) e).NewValue = (object) salesTax.HistTaxAcctID;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.accountID>>) e).Cancel = true;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.subID> e)
  {
    PX.Objects.CA.CABankTran caBankTran = PXResultset<PX.Objects.CA.CABankTran>.op_Implicit(PXSelectBase<PX.Objects.CA.CABankTran, PXSelect<PX.Objects.CA.CABankTran, Where<PX.Objects.CA.CABankTran.tranID, Equal<Required<CABankTranDetail.bankTranID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.BankTranID
    }));
    if (e.Row == null || caBankTran == null)
      return;
    if (caBankTran.DrCr == "C")
    {
      PurchaseTax purchaseTax = PXResultset<PurchaseTax>.op_Implicit(PXSelectBase<PurchaseTax, PXSelect<PurchaseTax, Where<PurchaseTax.taxID, Equal<Required<PurchaseTax.taxID>>>>.Config>.Select(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.subID>>) e).Cache.Graph, new object[1]
      {
        (object) e.Row.TaxID
      }));
      if (purchaseTax == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.subID>, CABankTaxTranMatch, object>) e).NewValue = (object) purchaseTax.HistTaxSubID;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.subID>>) e).Cancel = true;
    }
    else
    {
      PX.Objects.AR.SalesTax salesTax = PXResultset<PX.Objects.AR.SalesTax>.op_Implicit(PXSelectBase<PX.Objects.AR.SalesTax, PXSelect<PX.Objects.AR.SalesTax, Where<PX.Objects.AR.SalesTax.taxID, Equal<Required<PX.Objects.AR.SalesTax.taxID>>>>.Config>.Select(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.subID>>) e).Cache.Graph, new object[1]
      {
        (object) e.Row.TaxID
      }));
      if (salesTax == null)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.subID>, CABankTaxTranMatch, object>) e).NewValue = (object) salesTax.HistTaxSubID;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.subID>>) e).Cancel = true;
    }
  }

  protected virtual void CABankTranDetail_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    CABankTranDetail row = e.Row as CABankTranDetail;
    CATranDetailHelper.VerifyOffsetCashAccount(sender, (ICATranDetail) row, (int?) ((PXSelectBase<CABankTranDetail>) this.TranSplit).Current?.CashAccountID);
  }

  protected virtual void CABankTranDetail_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    CATranDetailHelper.OnCATranDetailRowUpdatingEvent(sender, e);
    PX.Objects.CA.CABankTran caBankTran = PXResultset<PX.Objects.CA.CABankTran>.op_Implicit(PXSelectBase<PX.Objects.CA.CABankTran, PXSelect<PX.Objects.CA.CABankTran, Where<PX.Objects.CA.CABankTran.tranID, Equal<Required<CABankTranDetail.bankTranID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) (e.NewRow as CABankTranDetail).BankTranID
    }));
    CATranDetailHelper.UpdateNewTranDetailCuryTranAmtOrCuryUnitPrice(sender, e.Row as ICATranDetail, e.NewRow as ICATranDetail);
    if (!CATranDetailHelper.VerifyOffsetCashAccount(sender, (ICATranDetail) (e.NewRow as CABankTranDetail), (int?) caBankTran?.CashAccountID))
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CABankTranDetail_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CABankTranDetail row = (CABankTranDetail) e.Row;
    if (row == null)
      return;
    PX.Objects.CA.CABankTran caBankTran = PXResultset<PX.Objects.CA.CABankTran>.op_Implicit(PXSelectBase<PX.Objects.CA.CABankTran, PXSelect<PX.Objects.CA.CABankTran, Where<PX.Objects.CA.CABankTran.tranID, Equal<Required<CABankTranAdjustment.tranID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.BankTranID
    }));
    sender.GetStateExt<CABankTranDetail.accountID>((object) row);
    PXSetPropertyException propertyException1 = (PXSetPropertyException) null;
    PXSetPropertyException propertyException2 = (PXSetPropertyException) null;
    bool flag = true;
    try
    {
      AccountAttribute.VerifyAccountIsNotControl((PX.Objects.GL.Account) PXSelectorAttribute.Select<CABankTranDetail.accountID>(sender, e.Row));
    }
    catch (PXSetPropertyException ex)
    {
      flag = false;
      propertyException2 = ex;
    }
    int? nullable = row.AccountID;
    if (!nullable.HasValue)
    {
      propertyException1 = new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 3, new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CABankTranDetail.accountID>(sender)
      });
    }
    else
    {
      nullable = row.SubID;
      if (!nullable.HasValue)
        propertyException1 = new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 3, new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<CABankTranDetail.subID>(sender)
        });
      else if (!flag)
        propertyException1 = propertyException2;
    }
    ((PXSelectBase) this.CABankTran).Cache.RaiseExceptionHandling<PX.Objects.CA.CABankTran.createDocument>((object) caBankTran, (object) caBankTran.CreateDocument, (Exception) propertyException1);
    sender.RaiseExceptionHandling<CABankTranDetail.accountID>((object) row, (object) row.AccountID, (Exception) propertyException1);
  }

  private CABankTranDetail GetDefaultAccountValues(
    PXGraph graph,
    int? cashAccountID,
    string entryTypeID)
  {
    return CATranDetailHelper.CreateCATransactionDetailWithDefaultAccountValues<CABankTranDetail>(graph, cashAccountID, entryTypeID);
  }

  public virtual void updateAmountPrice(CABankTranDetail oldSplit, CABankTranDetail newSplit)
  {
    CATranDetailHelper.UpdateNewTranDetailCuryTranAmtOrCuryUnitPrice(((PXSelectBase) this.TranSplit).Cache, (ICATranDetail) oldSplit, (ICATranDetail) newSplit);
  }

  [PXMergeAttributes]
  [CABankTranTaxMatch(typeof (PX.Objects.CA.CABankTran), typeof (CABankTax), typeof (CABankTaxTran), typeof (PX.Objects.CA.CABankTran.taxCalcMode), null, CuryOrigDocAmt = typeof (PX.Objects.CA.CABankTran.curyTranAmt), CuryLineTotal = typeof (PX.Objects.CA.CABankTran.curyApplAmtCA))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CABankTranDetail.taxCategoryID> e)
  {
  }

  public virtual CashAccount GetCashAccount(int? cashAccountID)
  {
    CashAccount current = ((PXSelectBase<CashAccount>) this.cashAccount).Current;
    int num;
    if (current == null)
    {
      num = !cashAccountID.HasValue ? 1 : 0;
    }
    else
    {
      int? cashAccountId = current.CashAccountID;
      int? nullable = cashAccountID;
      num = cashAccountId.GetValueOrDefault() == nullable.GetValueOrDefault() & cashAccountId.HasValue == nullable.HasValue ? 1 : 0;
    }
    if (num != 0)
      return ((PXSelectBase<CashAccount>) this.cashAccount).Current;
    return PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) cashAccountID
    }));
  }

  public virtual PXResultset<PX.Objects.CA.BankStatementHelpers.CATranExt> SearchForMatchingTransactions(
    PX.Objects.CA.CABankTran aDetail,
    IMatchSettings aSettings,
    Pair<DateTime, DateTime> tranDateRange,
    string curyID,
    bool bestMatchOnly = false)
  {
    return this.CABankTransactionsRepository.SearchForMatchingTransactions((PXGraph) this, aDetail, aSettings, tranDateRange, curyID, bestMatchOnly);
  }

  public virtual PXResultset<CABatch> SearchForMatchingCABatches(
    PX.Objects.CA.CABankTran aDetail,
    IMatchSettings aSettings,
    Pair<DateTime, DateTime> tranDateRange,
    string curyID,
    bool allowUnreleased)
  {
    return this.CABankTransactionsRepository.SearchForMatchingCABatches((PXGraph) this, aDetail, aSettings, tranDateRange, curyID, allowUnreleased);
  }

  public virtual PXResultset<CABatchDetailOrigDocAggregate> SearchForMatchesInCABatches(
    string tranType,
    string batchNbr)
  {
    return this.CABankTransactionsRepository.SearchForMatchesInCABatches((PXGraph) this, tranType, batchNbr);
  }

  public virtual bool SkipSearchForMatchesInCABatch(CATran caTran, string batchNbr) => false;

  public virtual PXResultset<CABankTranMatch> SearchForTranMatchForCABatch(string batchNbr)
  {
    return this.CABankTransactionsRepository.SearchForTranMatchForCABatch((PXGraph) this, batchNbr);
  }

  public virtual PXResult<PX.Objects.AR.ARInvoice, PX.Objects.AR.ARAdjust> FindARInvoiceByInvoiceInfo(
    PX.Objects.CA.CABankTran aRow)
  {
    return this.CABankTransactionsRepository.FindARInvoiceByInvoiceInfo((PXGraph) this, aRow);
  }

  public virtual PXResult<PX.Objects.AP.APInvoice, PX.Objects.AP.APAdjust, PX.Objects.AP.APPayment> FindAPInvoiceByInvoiceInfo(
    PX.Objects.CA.CABankTran aRow)
  {
    return this.CABankTransactionsRepository.FindAPInvoiceByInvoiceInfo((PXGraph) this, aRow);
  }

  public virtual string GetStatus(CATran tran) => StatementsMatchingProto.GetStatus(tran);
}
