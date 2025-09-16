// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAReleaseProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common.Exceptions;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.DAC.Abstract;
using PX.Objects.GL.FinPeriods;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.CA;

[PXHidden]
public class CAReleaseProcess : PXGraph<CAReleaseProcess>
{
  public PXSetup<CASetup> casetup;
  public PXSelectJoin<CATran, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<CATran.cashAccountID>>, InnerJoin<PX.Objects.CM.Currency, On<PX.Objects.CM.Currency.curyID, Equal<CashAccount.curyID>>, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<CATran.curyInfoID>>>>>, Where<CATran.origModule, Equal<BatchModule.moduleCA>, And<CATran.origTranType, In3<CATranType.cATransfer, CATranType.cATransferOut, CATranType.cATransferIn>, And<CATran.origRefNbr, Equal<Required<CATran.origRefNbr>>, And<CATran.released, Equal<boolFalse>>>>>, OrderBy<Asc<CATran.origRefNbr, Desc<CATran.origTranType>>>> CATranFundsTransfers_Ordered;
  public PXSelectJoin<CATran, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<CATran.cashAccountID>>, InnerJoin<PX.Objects.CM.Currency, On<PX.Objects.CM.Currency.curyID, Equal<CashAccount.curyID>>, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<CATran.curyInfoID>>>>>, Where<CATran.origModule, Equal<BatchModule.moduleCA>, And<CATran.origTranType, Equal<CATranType.cAAdjustment>, And<CATran.origRefNbr, Equal<Required<CATran.origRefNbr>>, And<CATran.released, Equal<boolFalse>>>>>, OrderBy<Asc<CATran.tranID>>> CATranCashTrans_Ordered;
  public PXSelect<CASplit, Where<CASplit.adjTranType, Equal<Required<CASplit.adjTranType>>, And<CASplit.adjTranType, NotEqual<CATranType.cATransferExp>, And<CASplit.adjRefNbr, Equal<Required<CASplit.adjRefNbr>>>>>> CASplits;
  public PXSelectJoin<CATaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<CATaxTran.taxID>>>, Where<CATaxTran.module, Equal<BatchModule.moduleCA>, And<CATaxTran.tranType, Equal<Required<CATaxTran.tranType>>, And<CATaxTran.refNbr, Equal<Required<CATaxTran.refNbr>>>>>, OrderBy<Asc<PX.Objects.TX.Tax.taxCalcLevel>>> CATaxTran_TranType_RefNbr;
  public PXSelectJoin<CAExpenseTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<CAExpenseTaxTran.taxID>>>, Where<CAExpenseTaxTran.module, Equal<BatchModule.moduleCA>, And<CAExpenseTaxTran.tranType, Equal<CATranType.cATransferExp>, And<CAExpenseTaxTran.refNbr, Equal<Required<CAExpenseTaxTran.refNbr>>, And<CAExpenseTaxTran.lineNbr, Equal<Required<CAExpenseTaxTran.lineNbr>>>>>>, OrderBy<Asc<PX.Objects.TX.Tax.taxCalcLevel>>> CAExpenseTaxTran_TranType_RefNbr;
  public PXSelect<PX.Objects.TX.SVATConversionHist> SVATConversionHistory;
  public PXSelect<CADepositEntry.ARPaymentUpdate> arDocs;
  public PXSelect<CADepositEntry.APPaymentUpdate> apDocs;
  public PXSelect<CADepositEntry.CAAdjUpdate> caDocs;
  public PXSelect<CADepositDetail> depositDetails;
  public PXSelect<CADeposit> deposit;
  public PXSelect<CAExpense, Where<CAExpense.refNbr, Equal<Required<CAExpense.refNbr>>>> CAExpense_RefNbr;
  public PXSelect<CATran, Where<CATran.tranID, Equal<Required<CAExpense.cashTranID>>>> CATran_CAExpense_CashTranID;
  public PXSetup<GLSetup> glsetup;
  public PXSelect<CAAdj> CAAdjastments;
  public PXSelect<CATransfer> CAtransfers;
  public PXSelect<CATaxTran> TaxTrans;
  public PXSelect<CAExpense> TransferExpenses;
  public PXSelect<PX.Objects.TX.SVATConversionHist> SVATConversionHist;
  public PXSelect<CADailySummary> DailySummary;
  public PXSelect<CADeposit> CADeposits;
  public PXSelect<CADepositDetail> CADepositDetails;
  public PXSelect<CADepositEntry.ARPaymentUpdate> ARPaymentUpdateRows;
  public PXSelect<CADepositEntry.APPaymentUpdate> APPaymentUpdateRows;
  public PXSelect<CADepositEntry.CAAdjUpdate> CAAdjUpdateRows;
  public PXSelect<CCBatch> ccBatch;
  public PXSelect<PX.Objects.GL.Sub, Where<PX.Objects.GL.Sub.subID, Equal<Required<CashAccount.subID>>>> subAccounts;
  public PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<CashAccount.accountID>>>> glAccounts;

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public bool? RequireControlTaxTotal
  {
    get
    {
      return new bool?(((PXSelectBase<CASetup>) this.casetup).Current.RequireControlTaxTotal.GetValueOrDefault() && PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>());
    }
  }

  public bool AutoPost => ((PXSelectBase<CASetup>) this.casetup).Current.AutoPostOption.Value;

  public CAReleaseProcess()
  {
    bool valueOrDefault = ((PXSelectBase<CASetup>) this.casetup).Current.RequireExtRefNbr.GetValueOrDefault();
    PXUIFieldAttribute.SetRequired<CAAdj.extRefNbr>(((PXGraph) this).Caches[typeof (CAAdj)], valueOrDefault);
    PXDefaultAttribute.SetPersistingCheck<CAAdj.extRefNbr>(((PXGraph) this).Caches[typeof (CAAdj)], (object) null, valueOrDefault ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetRequired<CADeposit.extRefNbr>(((PXGraph) this).Caches[typeof (CADeposit)], valueOrDefault);
    PXDefaultAttribute.SetPersistingCheck<CAAdj.extRefNbr>(((PXGraph) this).Caches[typeof (CADeposit)], (object) null, valueOrDefault ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  [PXRemoveBaseAttribute(typeof (CAAPARTranType.ListByModuleAttribute))]
  protected virtual void _(PX.Data.Events.CacheAttached<CATran.origTranType> e)
  {
  }

  public virtual void SegregateBatch(
    JournalEntry je,
    List<PX.Objects.GL.Batch> batchlist,
    int? branchID,
    string curyID,
    DateTime? docDate,
    string finPeriodID,
    string description,
    PX.Objects.CM.CurrencyInfo curyInfo)
  {
    PX.Objects.GL.Batch current = ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current;
    if (current != null)
    {
      ((PXAction) je.Save).Press();
      if (!batchlist.Contains(current))
        batchlist.Add(current);
    }
    JournalEntry.SegregateBatch(je, "CA", branchID, curyID, docDate, finPeriodID, description, curyInfo, (PX.Objects.GL.Batch) null);
  }

  private void PostGeneralTax(
    JournalEntry je,
    CATran catran,
    CAAdj caadj,
    CATaxTran x,
    PX.Objects.TX.Tax salestax,
    int? accountID = null,
    int? subID = null)
  {
    bool flag = caadj.DrCr == "C";
    PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
    glTran1.SummPost = new bool?(false);
    glTran1.CuryInfoID = catran.CuryInfoID;
    glTran1.TranType = caadj.AdjTranType;
    glTran1.TranClass = "T";
    glTran1.RefNbr = caadj.AdjRefNbr;
    glTran1.TranDate = caadj.TranDate;
    glTran1.AccountID = accountID ?? x.AccountID;
    PX.Objects.GL.GLTran glTran2 = glTran1;
    int? nullable1 = subID;
    int? nullable2 = nullable1 ?? x.SubID;
    glTran2.SubID = nullable2;
    glTran1.TranDesc = salestax.TaxID;
    glTran1.CuryDebitAmt = flag ? x.CuryTaxAmt : new Decimal?(0M);
    glTran1.DebitAmt = flag ? x.TaxAmt : new Decimal?(0M);
    glTran1.CuryCreditAmt = flag ? new Decimal?(0M) : x.CuryTaxAmt;
    glTran1.CreditAmt = flag ? new Decimal?(0M) : x.TaxAmt;
    glTran1.Released = new bool?(true);
    PX.Objects.GL.GLTran glTran3 = glTran1;
    nullable1 = new int?();
    int? nullable3 = nullable1;
    glTran3.ReferenceID = nullable3;
    glTran1.BranchID = caadj.BranchID;
    glTran1.ProjectID = ProjectDefaultAttribute.NonProject();
    ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(glTran1);
  }

  private void PostReverseTax(
    JournalEntry je,
    CATran catran,
    CAAdj caadj,
    CATaxTran x,
    PX.Objects.TX.Tax salestax)
  {
    bool flag = caadj.DrCr == "C";
    ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(new PX.Objects.GL.GLTran()
    {
      SummPost = new bool?(false),
      CuryInfoID = catran.CuryInfoID,
      TranType = caadj.AdjTranType,
      TranClass = "T",
      RefNbr = caadj.AdjRefNbr,
      TranDate = caadj.TranDate,
      AccountID = x.AccountID,
      SubID = x.SubID,
      TranDesc = salestax.TaxID,
      CuryDebitAmt = flag ? new Decimal?(0M) : x.CuryTaxAmt,
      DebitAmt = flag ? new Decimal?(0M) : x.TaxAmt,
      CuryCreditAmt = flag ? x.CuryTaxAmt : new Decimal?(0M),
      CreditAmt = flag ? x.TaxAmt : new Decimal?(0M),
      Released = new bool?(true),
      ReferenceID = new int?(),
      BranchID = caadj.BranchID,
      ProjectID = ProjectDefaultAttribute.NonProject()
    });
  }

  private void PostTaxAmountByProjectKey(
    JournalEntry je,
    CATran catran,
    CAAdj caadj,
    CATaxTran caTaxTran,
    PX.Objects.TX.Tax tax)
  {
    bool flag = caadj.DrCr == "C";
    PXResultset<CATax> deductibleLines = this.GetDeductibleLines(tax, caTaxTran);
    new APTaxAttribute(typeof (CARegister), typeof (CATax), typeof (CATaxTran)).DistributeTaxDiscrepancy<CATax, CATax.curyTaxAmt, CATax.taxAmt>((PXGraph) this, deductibleLines.FirstTableItems, caTaxTran.CuryTaxAmt.Value);
    Dictionary<ProjectKey, PX.Objects.GL.GLTran> dictionary1 = new Dictionary<ProjectKey, PX.Objects.GL.GLTran>();
    Dictionary<int?, CASplit> dictionary2 = new Dictionary<int?, CASplit>();
    foreach (PXResult<CATax, CASplit> pxResult in deductibleLines)
    {
      CATax caTax = PXResult<CATax, CASplit>.op_Implicit(pxResult);
      CASplit caSplit = PXResult<CATax, CASplit>.op_Implicit(pxResult);
      bool? expenseToSingleAccount = tax.ReportExpenseToSingleAccount;
      int? accountID = expenseToSingleAccount.GetValueOrDefault() ? tax.ExpenseAccountID : caSplit.AccountID;
      expenseToSingleAccount = tax.ReportExpenseToSingleAccount;
      int? subID = expenseToSingleAccount.GetValueOrDefault() ? tax.ExpenseSubID : caSplit.SubID;
      ProjectKey key = new ProjectKey(caSplit.BranchID, accountID, subID, caSplit.ProjectID, caSplit.TaskID, caSplit.CostCodeID, caSplit.InventoryID, caSplit.NonBillable);
      PX.Objects.GL.GLTran glTran1;
      if (dictionary1.TryGetValue(key, out glTran1))
      {
        glTran1.TranLineNbr = new int?();
        PX.Objects.GL.GLTran glTran2 = glTran1;
        Decimal? nullable1 = glTran1.Qty;
        Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
        nullable1 = caSplit.Qty;
        Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
        Decimal? nullable2 = new Decimal?(valueOrDefault1 + valueOrDefault2);
        glTran2.Qty = nullable2;
        PX.Objects.GL.GLTran glTran3 = glTran1;
        nullable1 = glTran1.CuryDebitAmt;
        Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
        Decimal num1;
        if (!flag)
        {
          num1 = 0M;
        }
        else
        {
          nullable1 = caTax.CuryTaxAmt;
          num1 = nullable1.GetValueOrDefault();
        }
        Decimal? nullable3 = new Decimal?(valueOrDefault3 + num1);
        glTran3.CuryDebitAmt = nullable3;
        PX.Objects.GL.GLTran glTran4 = glTran1;
        nullable1 = glTran1.DebitAmt;
        Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
        Decimal num2;
        if (!flag)
        {
          num2 = 0M;
        }
        else
        {
          nullable1 = caTax.TaxAmt;
          num2 = nullable1.GetValueOrDefault();
        }
        Decimal? nullable4 = new Decimal?(valueOrDefault4 + num2);
        glTran4.DebitAmt = nullable4;
        PX.Objects.GL.GLTran glTran5 = glTran1;
        nullable1 = glTran1.CuryCreditAmt;
        Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
        Decimal num3;
        if (!flag)
        {
          nullable1 = caTax.CuryTaxAmt;
          num3 = nullable1.GetValueOrDefault();
        }
        else
          num3 = 0M;
        Decimal? nullable5 = new Decimal?(valueOrDefault5 + num3);
        glTran5.CuryCreditAmt = nullable5;
        PX.Objects.GL.GLTran glTran6 = glTran1;
        nullable1 = glTran1.CreditAmt;
        Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
        Decimal num4;
        if (!flag)
        {
          nullable1 = caTax.TaxAmt;
          num4 = nullable1.GetValueOrDefault();
        }
        else
          num4 = 0M;
        Decimal? nullable6 = new Decimal?(valueOrDefault6 + num4);
        glTran6.CreditAmt = nullable6;
      }
      else
      {
        glTran1 = new PX.Objects.GL.GLTran();
        glTran1.SummPost = new bool?(false);
        glTran1.BranchID = caSplit.BranchID;
        glTran1.CuryInfoID = catran.CuryInfoID;
        glTran1.TranType = caadj.AdjTranType;
        glTran1.TranClass = "T";
        glTran1.RefNbr = caadj.AdjRefNbr;
        glTran1.TranDate = caadj.TranDate;
        glTran1.AccountID = accountID;
        glTran1.SubID = subID;
        glTran1.TranDesc = tax.TaxID;
        glTran1.TranLineNbr = caSplit.LineNbr;
        glTran1.CuryDebitAmt = flag ? caTax.CuryTaxAmt : new Decimal?(0M);
        glTran1.DebitAmt = flag ? caTax.TaxAmt : new Decimal?(0M);
        glTran1.CuryCreditAmt = flag ? new Decimal?(0M) : caTax.CuryTaxAmt;
        glTran1.CreditAmt = flag ? new Decimal?(0M) : caTax.TaxAmt;
        glTran1.Released = new bool?(true);
        glTran1.ReferenceID = new int?();
        glTran1.ProjectID = caSplit.ProjectID;
        glTran1.TaskID = caSplit.TaskID;
        glTran1.CostCodeID = caSplit.CostCodeID;
        glTran1.NonBillable = caSplit.NonBillable;
        glTran1.InventoryID = caSplit.InventoryID;
        glTran1.Qty = caSplit.Qty;
        glTran1.UOM = caSplit.UOM;
        dictionary1.Add(key, glTran1);
        dictionary2.Add(caSplit.LineNbr, caSplit);
      }
    }
    foreach (PX.Objects.GL.GLTran glTran in dictionary1.Values)
      ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(glTran);
  }

  private bool IsPostUseAndSalesTaxesByProjectKey(PXGraph graph, PX.Objects.TX.Tax tax)
  {
    bool flag = true;
    if (tax.ReportExpenseToSingleAccount.GetValueOrDefault())
    {
      PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
      {
        (object) tax.ExpenseAccountID
      }));
      flag = account != null && account.AccountGroupID.HasValue;
    }
    return flag;
  }

  protected virtual PXResultset<CATax> GetDeductibleLines(PX.Objects.TX.Tax salestax, CATaxTran x)
  {
    return PXSelectBase<CATax, PXSelectJoin<CATax, InnerJoin<CASplit, On<CATax.adjTranType, Equal<CASplit.adjTranType>, And<CATax.adjRefNbr, Equal<CASplit.adjRefNbr>, And<CATax.lineNbr, Equal<CASplit.lineNbr>>>>>, Where<CATax.taxID, Equal<Required<CATax.taxID>>, And<CASplit.adjTranType, Equal<Required<CASplit.adjTranType>>, And<CASplit.adjRefNbr, Equal<Required<CASplit.adjRefNbr>>>>>, OrderBy<Desc<CATax.curyTaxAmt>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) salestax.TaxID,
      (object) x.TranType,
      (object) x.RefNbr
    });
  }

  protected virtual PXResultset<CAExpenseTax> GetDeductibleLines(PX.Objects.TX.Tax salestax, CAExpenseTaxTran x)
  {
    return PXSelectBase<CAExpenseTax, PXSelect<CAExpenseTax, Where<CAExpenseTax.taxID, Equal<Required<CAExpenseTaxTran.taxID>>, And<CAExpenseTax.tranType, Equal<Required<CAExpenseTaxTran.tranType>>, And<CAExpenseTax.refNbr, Equal<Required<CAExpenseTaxTran.refNbr>>, And<CAExpenseTax.lineNbr, Equal<Required<CAExpenseTaxTran.lineNbr>>>>>>, OrderBy<Desc<CAExpenseTaxTran.curyTaxAmt>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) salestax.TaxID,
      (object) x.TranType,
      (object) x.RefNbr,
      (object) x.LineNbr
    });
  }

  public virtual void OnBeforeRelease(CAAdj doc)
  {
  }

  public virtual void ReleaseDocProc<TCADocument>(
    JournalEntry je,
    ref List<PX.Objects.GL.Batch> batchlist,
    TCADocument doc,
    SelectedEntityEvent<TCADocument> releaseEvent)
    where TCADocument : class, ICADocument, IBqlTable, new()
  {
    this.PerformBasicReleaseChecks((ICADocument) doc);
    if (doc.Status == "P" || doc.Status == "J")
      throw new ReleaseException(FailedWith.DocumentNotApproved, "The document has the Pending Approval status and cannot be released. The document must be approved by a responsible person before it can be released.", Array.Empty<object>());
    if (doc is CAAdj doc1)
      this.OnBeforeRelease(doc1);
    int? transitAcctID;
    int? transitSubID;
    this.TryToGetCashInTransitAccountAndSub((ICADocument) doc, out transitAcctID, out transitSubID);
    HashSet<long> caTranIDs = new HashSet<long>();
    HashSet<int> accountIDs = new HashSet<int>();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
      glTran1.DebitAmt = new Decimal?(0M);
      glTran1.CreditAmt = new Decimal?(0M);
      PX.Objects.CM.Currency currency = (PX.Objects.CM.Currency) null;
      PX.Objects.CM.CurrencyInfo rgol_info = (PX.Objects.CM.CurrencyInfo) null;
      PX.Objects.CM.CurrencyInfo currencyInfo1 = (PX.Objects.CM.CurrencyInfo) null;
      CATran objA = (CATran) null;
      if (!transitAcctID.HasValue || !transitSubID.HasValue)
        throw new PXException("The documents cannot be released due to absence of a cash-in-transit account. Specify the account in the Cash-In-Transit box on the Cash Management Preferences (CA101000) form.");
      PX.Objects.GL.Batch batch = (PX.Objects.GL.Batch) null;
      PX.Objects.GL.Batch batch1 = (PX.Objects.GL.Batch) null;
      PX.Objects.GL.Batch transferBatchOut = (PX.Objects.GL.Batch) null;
      PX.Objects.GL.Batch transferBatchIn = (PX.Objects.GL.Batch) null;
      CATran tranIn = (CATran) null;
      CATran transferTranOut = (CATran) null;
      CATran transferTranIn = (CATran) null;
      int? nullable1 = new int?();
      PXResultset<CATran> resultsetByTranType = this.GetResultsetByTranType((ICADocument) doc);
      this.FinPeriodUtils.ValidateFinPeriod((IEnumerable<IAccountable>) ((IEnumerable<PXResult<CATran>>) resultsetByTranType).AsEnumerable<PXResult<CATran>>().Select<PXResult<CATran>, CATran>((Func<PXResult<CATran>, CATran>) (result => PXResult<CATran>.op_Implicit(result))), typeof (OrganizationFinPeriod.cAClosed));
      foreach (PXResult<CATran, CashAccount, PX.Objects.CM.Currency, PX.Objects.CM.CurrencyInfo> pxResult1 in resultsetByTranType)
      {
        CATran caTran = PXResult<CATran, CashAccount, PX.Objects.CM.Currency, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult1);
        CashAccount cashacct = PXResult<CATran, CashAccount, PX.Objects.CM.Currency, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult1);
        PX.Objects.CM.Currency cury = PXResult<CATran, CashAccount, PX.Objects.CM.Currency, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult1);
        PX.Objects.CM.CurrencyInfo currencyInfo2 = PXResult<CATran, CashAccount, PX.Objects.CM.Currency, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult1);
        CAAdj caAdj = new CAAdj();
        if (doc.DocType == "CAE")
          caAdj = CAAdj.PK.Find((PXGraph) this, caTran.OrigTranType, caTran.OrigRefNbr);
        this.VerifyCAAdjNotOnHold(caAdj);
        if (!nullable1.HasValue && caTran.OrigTranType != "CTI")
          nullable1 = cashacct.BranchID;
        this.VerifyActivityOfAccountAndSubaccount(cashacct);
        this.VerifyCashAccountInGL(cashacct);
        int? nullable2 = nullable1;
        int? nullable3 = nullable2 ?? cashacct.BranchID;
        bool flag1 = true;
        bool isTransfer = CATranType.IsTransfer(caTran.OrigTranType);
        int? nullable4;
        if (isTransfer)
        {
          nullable2 = nullable3;
          nullable4 = (int?) batch1?.BranchID;
          if (nullable2.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable2.HasValue == nullable4.HasValue && this.ShouldNotSegregate(caTran, tranIn))
          {
            ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current = batch1;
            flag1 = false;
          }
        }
        if (flag1)
        {
          string byMasterPeriodId = this.FinPeriodRepository.FindFinPeriodIDByMasterPeriodID(PXAccess.GetParentOrganizationID(nullable3), caTran.TranPeriodID);
          this.SegregateBatch(je, batchlist, nullable3, caTran.CuryID, caTran.TranDate, byMasterPeriodId, caTran.TranDesc, currencyInfo2);
        }
        batch = ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current;
        List<CASplit> list = GraphHelper.RowCast<CASplit>((IEnumerable) ((PXSelectBase<CASplit>) this.CASplits).Select(new object[2]
        {
          (object) caAdj.AdjTranType,
          (object) caAdj.AdjRefNbr
        })).ToList<CASplit>();
        Decimal? nullable5;
        Decimal? nullable6;
        long? nullable7;
        if (!list.Any<CASplit>())
        {
          CASplit caSplit1 = new CASplit();
          caSplit1.AdjTranType = caTran.OrigTranType;
          caSplit1.CuryInfoID = caTran.CuryInfoID;
          CASplit caSplit2 = caSplit1;
          Decimal? nullable8;
          if (!(caTran.DrCr == "D"))
          {
            Decimal num = -1M;
            nullable5 = caTran.CuryTranAmt;
            if (!nullable5.HasValue)
            {
              nullable6 = new Decimal?();
              nullable8 = nullable6;
            }
            else
              nullable8 = new Decimal?(num * nullable5.GetValueOrDefault());
          }
          else
            nullable8 = caTran.CuryTranAmt;
          caSplit2.CuryTranAmt = nullable8;
          CASplit caSplit3 = caSplit1;
          Decimal? nullable9;
          if (!(caTran.DrCr == "D"))
          {
            Decimal num = -1M;
            nullable5 = caTran.TranAmt;
            if (!nullable5.HasValue)
            {
              nullable6 = new Decimal?();
              nullable9 = nullable6;
            }
            else
              nullable9 = new Decimal?(num * nullable5.GetValueOrDefault());
          }
          else
            nullable9 = caTran.TranAmt;
          caSplit3.TranAmt = nullable9;
          caSplit1.TranDesc = PXMessages.LocalizeNoPrefix("Offset");
          caSplit1.AccountID = transitAcctID;
          caSplit1.SubID = transitSubID;
          caSplit1.ReclassificationProhibited = new bool?(true);
          caSplit1.BranchID = nullable3;
          switch (caSplit1.AdjTranType)
          {
            case "CTO":
              PX.Objects.CM.CurrencyInfo copy = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(currencyInfo2);
              PX.Objects.CM.CurrencyInfo currencyInfo3 = copy;
              nullable7 = new long?();
              long? nullable10 = nullable7;
              currencyInfo3.CuryInfoID = nullable10;
              currencyInfo1 = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) je.currencyinfo).Insert(copy);
              currencyInfo1.BaseCalc = new bool?(false);
              caSplit1.CuryInfoID = currencyInfo1.CuryInfoID;
              break;
            case "CTI":
              currency = cury;
              rgol_info = currencyInfo2;
              glTran1.TranDate = caTran.TranDate;
              glTran1.BranchID = cashacct.BranchID;
              glTran1.TranDesc = caTran.TranDesc;
              FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran1, caTran.TranPeriodID);
              if (string.Equals(currencyInfo2.CuryID, currencyInfo1.CuryID))
              {
                caSplit1.CuryInfoID = currencyInfo1.CuryInfoID;
                break;
              }
              break;
          }
          PX.Objects.GL.GLTran glTran2 = glTran1;
          nullable5 = glTran2.DebitAmt;
          nullable6 = caTran.DrCr == "D" ? new Decimal?(0M) : caSplit1.TranAmt;
          glTran2.DebitAmt = nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
          PX.Objects.GL.GLTran glTran3 = glTran1;
          nullable6 = glTran3.CreditAmt;
          nullable5 = caTran.DrCr == "D" ? caSplit1.TranAmt : new Decimal?(0M);
          glTran3.CreditAmt = nullable6.HasValue & nullable5.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
          list.Add(caSplit1);
        }
        if (!object.Equals((object) objA, (object) caTran))
        {
          PX.Objects.GL.GLTran documentTran = this.CreateDocumentTran(caTran, cashacct);
          FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) documentTran, caTran.TranPeriodID);
          this.InsertDocumentTransaction(je, documentTran, new CAReleaseProcess.GLTranInsertionContext()
          {
            CATranRecord = caTran
          });
          Decimal docInclTaxDiscrepancy = 0.0M;
          foreach (PXResult<CATaxTran, PX.Objects.TX.Tax> pxResult2 in ((PXSelectBase<CATaxTran>) this.CATaxTran_TranType_RefNbr).Select(new object[2]
          {
            (object) caAdj.AdjTranType,
            (object) caAdj.AdjRefNbr
          }))
          {
            CATaxTran caTaxTran = PXResult<CATaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult2);
            PX.Objects.TX.Tax tax = PXResult<CATaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult2);
            if (caAdj.TaxCalcMode == "G" || tax.TaxCalcLevel == "0" && caAdj.TaxCalcMode != "N" || caTaxTran.IsTaxInclusive.GetValueOrDefault())
            {
              Decimal num1 = docInclTaxDiscrepancy;
              nullable5 = caTaxTran.CuryTaxAmt;
              Decimal num2 = nullable5 ?? 0.0M;
              nullable5 = caTaxTran.CuryTaxAmtSumm;
              Decimal num3 = nullable5 ?? 0.0M;
              Decimal num4 = (num2 - num3) * (tax.ReverseTax.GetValueOrDefault() ? -1.0M : 1.0M);
              docInclTaxDiscrepancy = num1 + num4;
            }
            if (!(tax.TaxType == "W"))
            {
              if (caAdj.DrCr == "C" && (tax.TaxType == "P" || tax.TaxType == "S"))
              {
                if (this.IsPostUseAndSalesTaxesByProjectKey((PXGraph) this, tax))
                {
                  bool? isExternal = tax.IsExternal;
                  bool flag2 = false;
                  if (isExternal.GetValueOrDefault() == flag2 & isExternal.HasValue)
                  {
                    this.PostTaxAmountByProjectKey(je, caTran, caAdj, caTaxTran, tax);
                    goto label_45;
                  }
                }
                this.PostGeneralTax(je, caTran, caAdj, caTaxTran, tax, tax.ExpenseAccountID, tax.ExpenseSubID);
label_45:
                if (tax.TaxType == "P")
                  this.PostReverseTax(je, caTran, caAdj, caTaxTran, tax);
              }
              else
              {
                if (!tax.ReverseTax.GetValueOrDefault())
                {
                  JournalEntry je1 = je;
                  CATran catran = caTran;
                  CAAdj caadj = caAdj;
                  CATaxTran x = caTaxTran;
                  PX.Objects.TX.Tax salestax = tax;
                  nullable4 = new int?();
                  int? accountID = nullable4;
                  nullable4 = new int?();
                  int? subID = nullable4;
                  this.PostGeneralTax(je1, catran, caadj, x, salestax, accountID, subID);
                }
                else
                  this.PostReverseTax(je, caTran, caAdj, caTaxTran, tax);
                if (tax.DeductibleVAT.GetValueOrDefault())
                {
                  if (tax.ReportExpenseToSingleAccount.GetValueOrDefault())
                  {
                    PX.Objects.GL.GLTran taxExpenseTran = this.CreateTaxExpenseTran(caTran, caAdj, caTaxTran, tax);
                    this.InsertTaxExpenseTran(je, taxExpenseTran, new CAReleaseProcess.GLTranInsertionContext()
                    {
                      CATranRecord = caTran,
                      CAAdjRecord = caAdj,
                      CATaxTranRecord = caTaxTran
                    });
                  }
                  else if (tax.TaxCalcType == "I")
                  {
                    PXResultset<CATax> deductibleLines = this.GetDeductibleLines(tax, caTaxTran);
                    APTaxAttribute apTaxAttribute = new APTaxAttribute(typeof (CARegister), typeof (CATax), typeof (CATaxTran));
                    IEnumerable<CATax> firstTableItems = deductibleLines.FirstTableItems;
                    nullable5 = caTaxTran.CuryExpenseAmt;
                    Decimal CuryTaxAmt = nullable5.Value;
                    apTaxAttribute.DistributeTaxDiscrepancy<CATax, CATax.curyExpenseAmt, CATax.expenseAmt>((PXGraph) this, firstTableItems, CuryTaxAmt);
                    foreach (PXResult<CATax, CASplit> pxResult3 in deductibleLines)
                    {
                      CATax taxLine = PXResult<CATax, CASplit>.op_Implicit(pxResult3);
                      CASplit split = PXResult<CATax, CASplit>.op_Implicit(pxResult3);
                      PX.Objects.GL.GLTran deductibleTaxLineTran = this.CreateDeductibleTaxLineTran(caTran, caAdj, tax, taxLine, split);
                      this.InsertDeductibleTaxTran(je, deductibleTaxLineTran, new CAReleaseProcess.GLTranInsertionContext()
                      {
                        CATranRecord = caTran,
                        CAAdjRecord = caAdj,
                        CASplitRecord = split
                      });
                    }
                  }
                }
              }
              FinPeriodIDAttribute.SetPeriodsByMaster<CATaxTran.finPeriodID>(((PXSelectBase) this.CATaxTran_TranType_RefNbr).Cache, (object) caTaxTran, caAdj.TranPeriodID);
              caTaxTran.Released = new bool?(true);
              ((PXSelectBase<CATaxTran>) this.CATaxTran_TranType_RefNbr).Update(caTaxTran);
              if (PXAccess.FeatureInstalled<FeaturesSet.vATReporting>() && (caTaxTran.TaxType == "B" || caTaxTran.TaxType == "A"))
              {
                Decimal multByTranType = ReportTaxProcess.GetMultByTranType("CA", caTaxTran.TranType);
                PX.Objects.TX.SVATConversionHist svatConversionHist = this.CreateSVATConversionHist(caAdj, caTaxTran, multByTranType);
                FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.TX.SVATConversionHist.adjdFinPeriodID>(((PXSelectBase) this.SVATConversionHistory).Cache, (object) svatConversionHist, caAdj.TranPeriodID);
                svatConversionHist.FillBaseAmounts(((PXSelectBase) this.SVATConversionHistory).Cache);
                ((PXSelectBase) this.SVATConversionHistory).Cache.Insert((object) svatConversionHist);
              }
            }
          }
          if (docInclTaxDiscrepancy != 0M)
            this.ProcessTaxDiscrepancy(je, batch, caAdj, currencyInfo2, docInclTaxDiscrepancy);
        }
        foreach (CASplit casplit in list)
        {
          PX.Objects.TX.Tax firstTax = this.GetFirstTax(caAdj, casplit);
          PX.Objects.GL.GLTran splitTran = this.CreateSplitTran(caTran, casplit, firstTax, isTransfer);
          FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) splitTran, caTran.TranPeriodID);
          this.InsertSplitTransaction(je, splitTran, new CAReleaseProcess.GLTranInsertionContext()
          {
            CASplitRecord = casplit,
            CATranRecord = caTran
          });
        }
        objA = caTran;
        if (this.ShouldCreateRGOLTran(glTran1, currency, rgol_info))
        {
          batch = ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current;
          PX.Objects.CM.CurrencyInfo copy = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(rgol_info);
          copy.CuryInfoID = new long?();
          PX.Objects.CM.CurrencyInfo rgolInfo = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) je.currencyinfo).Insert(copy);
          PX.Objects.GL.GLTran rgolTran = this.CreateRGOLTran(je, (ICADocument) doc, glTran1, currency, cashacct, rgolInfo, nullable3, transitAcctID, transitSubID);
          FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) rgolTran, glTran1.TranPeriodID);
          this.InsertIncomingRGOLTransaction(je, rgolTran);
          rgolTran.AccountID = transitAcctID;
          rgolTran.SubID = transitSubID;
          rgolTran.BranchID = nullable1 ?? cashacct.BranchID;
          Decimal? curyDebitAmt = rgolTran.CuryDebitAmt;
          Decimal? debitAmt = rgolTran.DebitAmt;
          rgolTran.CuryDebitAmt = rgolTran.CuryCreditAmt;
          rgolTran.DebitAmt = rgolTran.CreditAmt;
          rgolTran.CuryCreditAmt = curyDebitAmt;
          rgolTran.CreditAmt = debitAmt;
          this.InsertOutgoingRGOLTransaction(je, rgolTran);
          glTran1 = new PX.Objects.GL.GLTran();
          glTran1.DebitAmt = new Decimal?(0M);
          glTran1.CreditAmt = new Decimal?(0M);
          currency = (PX.Objects.CM.Currency) null;
          rgol_info = (PX.Objects.CM.CurrencyInfo) null;
        }
        if (doc is CAAdj doc2)
          this.CommitExternalTax(doc2);
        if (this.ShouldCreateRoundingTran(batch, caAdj))
        {
          PX.Objects.CM.CurrencyInfo copy = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(currencyInfo2);
          copy.CuryInfoID = new long?();
          PX.Objects.CM.CurrencyInfo info = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) je.currencyinfo).Insert(copy) ?? new PX.Objects.CM.CurrencyInfo();
          PX.Objects.GL.GLTran roundingTran = this.CreateRoundingTran(je, (ICADocument) doc, batch, info, caAdj);
          FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) roundingTran, batch.TranPeriodID);
          this.InsertRoundingTransaction(je, roundingTran, new CAReleaseProcess.GLTranInsertionContext()
          {
            CAAdjRecord = (object) doc as CAAdj,
            CATransferRecord = (object) doc as CATransfer,
            CATranRecord = caTran
          });
        }
        if (batch != null)
        {
          nullable5 = batch.CuryCreditTotal;
          nullable6 = batch.CuryDebitTotal;
          if (nullable5.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable5.HasValue == nullable6.HasValue)
          {
            this.AddRoundingTran(je, (ICADocument) doc, batch, cury, currencyInfo2.CuryInfoID);
            ((PXAction) je.Save).Press();
            if (isTransfer)
            {
              tranIn = caTran;
              batch1 = ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current;
            }
            if (caTran.OrigTranType == "CTO")
            {
              transferTranOut = caTran;
              transferBatchOut = ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current;
            }
            if (caTran.OrigTranType == "CTI")
            {
              transferTranIn = caTran;
              transferBatchIn = ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current;
            }
            if (caTran.OrigTranType == "CTI")
              this.CreateGLTransFromCAExpencesForCATransfer<TCADocument>(je, batchlist, doc, transferBatchOut, transferTranOut, transferBatchIn, transferTranIn);
            if (batchlist.Find((Predicate<PX.Objects.GL.Batch>) (_ => ((PXSelectBase) je.BatchModule).Cache.ObjectsEqual((object) _, (object) batch))) == null)
              batchlist.Add(batch);
            doc.Released = new bool?(true);
            releaseEvent.FireOn((PXGraph) this, doc);
            if (caTran.TranID.HasValue)
            {
              nullable6 = caTran.CuryTranAmt;
              Decimal num5 = 0M;
              if (!(nullable6.GetValueOrDefault() == num5 & nullable6.HasValue))
              {
                nullable6 = caTran.TranAmt;
                Decimal num6 = 0M;
                if (!(nullable6.GetValueOrDefault() == num6 & nullable6.HasValue))
                {
                  HashSet<long> longSet = caTranIDs;
                  nullable7 = caTran.TranID;
                  long num7 = nullable7.Value;
                  longSet.Add(num7);
                  nullable4 = cashacct.AccountID;
                  if (nullable4.HasValue)
                  {
                    HashSet<int> intSet = accountIDs;
                    nullable4 = cashacct.AccountID;
                    int num8 = nullable4.Value;
                    intSet.Add(num8);
                  }
                }
              }
            }
            ((PXGraph) this).Caches[typeof (TCADocument)].Update((object) doc);
            if (!((PXGraph) this).Caches[typeof (TCADocument)].ObjectsEqual((object) doc, (object) caAdj) && caAdj.AdjRefNbr != null)
            {
              caAdj.Released = new bool?(true);
              ((PXGraph) this).Caches[typeof (CAAdj)].Update((object) caAdj);
              continue;
            }
            continue;
          }
        }
        throw new PXException("The document is out of the balance.");
      }
      doc.Released = new bool?(true);
      ((PXGraph) this).Caches[typeof (TCADocument)].Update((object) doc);
      ((PXGraph) this).Actions.PressSave();
      this.CheckBoundGLAccountIsCashAccount(accountIDs);
      this.CheckMultipleGLPosting(caTranIDs);
      this.OnReleaseComplete((ICADocument) doc);
      transactionScope.Complete((PXGraph) this);
    }
  }

  private PX.Objects.GL.GLTran CreateDeductibleTaxLineTran(
    CATran catran,
    CAAdj caadj,
    PX.Objects.TX.Tax salestax,
    CATax taxLine,
    CASplit split)
  {
    PX.Objects.GL.GLTran deductibleTaxLineTran = new PX.Objects.GL.GLTran();
    deductibleTaxLineTran.SummPost = new bool?(false);
    deductibleTaxLineTran.BranchID = split.BranchID;
    deductibleTaxLineTran.CuryInfoID = catran.CuryInfoID;
    deductibleTaxLineTran.TranType = caadj.AdjTranType;
    deductibleTaxLineTran.TranClass = "T";
    deductibleTaxLineTran.RefNbr = caadj.AdjRefNbr;
    deductibleTaxLineTran.TranDate = caadj.TranDate;
    deductibleTaxLineTran.AccountID = split.AccountID;
    deductibleTaxLineTran.SubID = split.SubID;
    deductibleTaxLineTran.TranDesc = salestax.TaxID;
    deductibleTaxLineTran.TranLineNbr = split.LineNbr;
    bool flag = caadj.DrCr == "C";
    deductibleTaxLineTran.CuryDebitAmt = flag ? taxLine.CuryExpenseAmt : new Decimal?(0M);
    deductibleTaxLineTran.DebitAmt = flag ? taxLine.ExpenseAmt : new Decimal?(0M);
    deductibleTaxLineTran.CuryCreditAmt = flag ? new Decimal?(0M) : taxLine.CuryExpenseAmt;
    deductibleTaxLineTran.CreditAmt = flag ? new Decimal?(0M) : taxLine.ExpenseAmt;
    deductibleTaxLineTran.Released = new bool?(true);
    deductibleTaxLineTran.ReferenceID = new int?();
    deductibleTaxLineTran.ProjectID = split.ProjectID;
    deductibleTaxLineTran.TaskID = split.TaskID;
    deductibleTaxLineTran.CostCodeID = split.CostCodeID;
    return deductibleTaxLineTran;
  }

  private PX.Objects.TX.SVATConversionHist CreateSVATConversionHist(
    CAAdj caadj,
    CATaxTran x,
    Decimal mult)
  {
    PX.Objects.TX.SVATConversionHist svatConversionHist = new PX.Objects.TX.SVATConversionHist();
    svatConversionHist.Module = "CA";
    svatConversionHist.AdjdBranchID = x.BranchID;
    svatConversionHist.AdjdDocType = x.TranType;
    svatConversionHist.AdjdRefNbr = x.RefNbr;
    svatConversionHist.AdjgDocType = x.TranType;
    svatConversionHist.AdjgRefNbr = x.RefNbr;
    svatConversionHist.AdjdDocDate = caadj.TranDate;
    svatConversionHist.AdjdFinPeriodID = caadj.FinPeriodID;
    svatConversionHist.TaxID = x.TaxID;
    svatConversionHist.TaxType = x.TaxType;
    svatConversionHist.TaxRate = x.TaxRate;
    svatConversionHist.VendorID = x.VendorID;
    svatConversionHist.ReversalMethod = "D";
    svatConversionHist.CuryInfoID = x.CuryInfoID;
    Decimal? nullable = x.CuryTaxableAmt;
    Decimal num1 = mult;
    svatConversionHist.CuryTaxableAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() * num1) : new Decimal?();
    nullable = x.CuryTaxAmt;
    Decimal num2 = mult;
    svatConversionHist.CuryTaxAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() * num2) : new Decimal?();
    nullable = x.CuryTaxAmt;
    Decimal num3 = mult;
    svatConversionHist.CuryUnrecognizedTaxAmt = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() * num3) : new Decimal?();
    return svatConversionHist;
  }

  private PX.Objects.GL.GLTran CreateTaxExpenseTran(
    CATran catran,
    CAAdj caadj,
    CATaxTran x,
    PX.Objects.TX.Tax salestax)
  {
    PX.Objects.GL.GLTran taxExpenseTran = new PX.Objects.GL.GLTran();
    taxExpenseTran.SummPost = new bool?(false);
    taxExpenseTran.BranchID = caadj.BranchID;
    taxExpenseTran.CuryInfoID = catran.CuryInfoID;
    taxExpenseTran.TranType = caadj.AdjTranType;
    taxExpenseTran.TranClass = "T";
    taxExpenseTran.RefNbr = caadj.AdjRefNbr;
    taxExpenseTran.TranDate = caadj.TranDate;
    taxExpenseTran.AccountID = salestax.ExpenseAccountID;
    taxExpenseTran.SubID = salestax.ExpenseSubID;
    taxExpenseTran.TranDesc = salestax.TaxID;
    bool flag = caadj.DrCr == "C";
    taxExpenseTran.CuryDebitAmt = flag ? x.CuryExpenseAmt : new Decimal?(0M);
    taxExpenseTran.DebitAmt = flag ? x.ExpenseAmt : new Decimal?(0M);
    taxExpenseTran.CuryCreditAmt = flag ? new Decimal?(0M) : x.CuryExpenseAmt;
    taxExpenseTran.CreditAmt = flag ? new Decimal?(0M) : x.ExpenseAmt;
    taxExpenseTran.Released = new bool?(true);
    taxExpenseTran.ReferenceID = new int?();
    taxExpenseTran.ProjectID = ProjectDefaultAttribute.NonProject();
    return taxExpenseTran;
  }

  private bool ShouldCreateRoundingTran(PX.Objects.GL.Batch batch, CAAdj caadj)
  {
    if (!(caadj.AdjTranType == "CAE"))
      return false;
    Decimal? curyDebitTotal = batch.CuryDebitTotal;
    Decimal? curyCreditTotal = batch.CuryCreditTotal;
    return Math.Abs(Math.Round((curyDebitTotal.HasValue & curyCreditTotal.HasValue ? new Decimal?(curyDebitTotal.GetValueOrDefault() - curyCreditTotal.GetValueOrDefault()) : new Decimal?()).Value, 4)) >= 0.00005M;
  }

  private PX.Objects.GL.GLTran CreateRoundingTran(
    JournalEntry je,
    ICADocument doc,
    PX.Objects.GL.Batch batch,
    PX.Objects.CM.CurrencyInfo info,
    CAAdj caadj)
  {
    if (!this.RequireControlTaxTotal.GetValueOrDefault())
      throw new PXException("The document is out of the balance.");
    Decimal? debitTotal = batch.DebitTotal;
    Decimal? nullable1 = batch.CreditTotal;
    Decimal num1 = Math.Abs(Math.Round((debitTotal.HasValue & nullable1.HasValue ? new Decimal?(debitTotal.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?()).Value, 4));
    Decimal num2 = num1;
    Decimal? nullable2 = CurrencyCollection.GetCurrency(je.currencyInfo.BaseCuryID).RoundingLimit;
    Decimal valueOrDefault = nullable2.GetValueOrDefault();
    if (num2 > valueOrDefault & nullable2.HasValue)
      throw new PXException("The amount to be posted to the rounding account ({1} {0}) exceeds the limit ({2} {0}) specified on the Currencies (CM202000) form.", new object[3]
      {
        (object) ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) je.currencyinfo).Current.BaseCuryID,
        (object) num1,
        (object) PXDBQuantityAttribute.Round(CurrencyCollection.GetCurrency(je.currencyInfo.BaseCuryID).RoundingLimit)
      });
    PX.Objects.GL.GLTran roundingTran = new PX.Objects.GL.GLTran();
    roundingTran.SummPost = new bool?(true);
    roundingTran.BranchID = caadj.BranchID;
    PX.Objects.CM.Currency currency = PXResultset<PX.Objects.CM.Currency>.op_Implicit(PXSelectBase<PX.Objects.CM.Currency, PXSelect<PX.Objects.CM.Currency, Where<PX.Objects.CM.Currency.curyID, Equal<Required<PX.Objects.CM.Currency.curyID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) caadj.CuryID
    }));
    int? nullable3 = currency.RoundingGainAcctID;
    if (nullable3.HasValue)
    {
      nullable3 = currency.RoundingGainSubID;
      if (nullable3.HasValue)
      {
        nullable2 = batch.CuryDebitTotal;
        nullable1 = batch.CuryCreditTotal;
        if (Math.Sign((nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?()).Value) == 1)
        {
          roundingTran.AccountID = currency.RoundingGainAcctID;
          roundingTran.SubID = GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Currency.roundingGainSubID>((PXGraph) je, roundingTran.BranchID, currency);
          PX.Objects.GL.GLTran glTran = roundingTran;
          nullable2 = batch.CuryDebitTotal;
          nullable1 = batch.CuryCreditTotal;
          Decimal? nullable4 = new Decimal?(Math.Round((nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?()).Value, 4));
          glTran.CuryCreditAmt = nullable4;
          roundingTran.CuryDebitAmt = new Decimal?(0M);
        }
        else
        {
          roundingTran.AccountID = currency.RoundingLossAcctID;
          roundingTran.SubID = GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Currency.roundingLossSubID>((PXGraph) je, roundingTran.BranchID, currency);
          roundingTran.CuryCreditAmt = new Decimal?(0M);
          PX.Objects.GL.GLTran glTran = roundingTran;
          nullable2 = batch.CuryCreditTotal;
          nullable1 = batch.CuryDebitTotal;
          Decimal? nullable5 = new Decimal?(Math.Round((nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?()).Value, 4));
          glTran.CuryDebitAmt = nullable5;
        }
        roundingTran.CreditAmt = new Decimal?(0M);
        roundingTran.DebitAmt = new Decimal?(0M);
        roundingTran.TranType = doc.DocType;
        roundingTran.RefNbr = doc.RefNbr;
        roundingTran.TranClass = "N";
        roundingTran.TranDesc = "Rounding difference";
        roundingTran.LedgerID = batch.LedgerID;
        roundingTran.TranDate = batch.DateEntered;
        roundingTran.Released = new bool?(true);
        roundingTran.ProjectID = ProjectDefaultAttribute.NonProject();
        roundingTran.CuryInfoID = info.CuryInfoID;
        return roundingTran;
      }
    }
    throw new PXException("Rounding gain or loss account or subaccount is not specified for {0} currency.", new object[1]
    {
      (object) currency.CuryID
    });
  }

  private PX.Objects.TX.Tax GetFirstTax(CAAdj caadj, CASplit casplit)
  {
    PXResultset<CATax> pxResultset = PXSelectBase<CATax, PXSelectJoin<CATax, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<CATax.taxID>>>, Where<CATax.adjTranType, Equal<Required<CATax.adjTranType>>, And<CATax.adjRefNbr, Equal<Required<CATax.adjRefNbr>>, And<CATax.lineNbr, Equal<Required<CATax.lineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) casplit.AdjTranType,
      (object) casplit.AdjRefNbr,
      (object) casplit.LineNbr
    });
    IComparer<PX.Objects.TX.Tax> taxComparer = this.GetTaxComparer();
    ExceptionExtensions.ThrowOnNull<IComparer<PX.Objects.TX.Tax>>(taxComparer, "taxComparer", (string) null);
    pxResultset.Sort((Comparison<PXResult<CATax>>) ((x, y) => taxComparer.Compare(((PXResult) x).GetItem<PX.Objects.TX.Tax>(), ((PXResult) y).GetItem<PX.Objects.TX.Tax>())));
    PX.Objects.TX.Tax taxCorrespondingToLine = pxResultset.Count != 0 ? ((PXResult) pxResultset[0]).GetItem<PX.Objects.TX.Tax>() : (PX.Objects.TX.Tax) null;
    if (taxCorrespondingToLine != null && taxCorrespondingToLine.TaxID != null)
    {
      if (caadj.TaxCalcMode == "T")
      {
        if (((IEnumerable<PXResult<CATaxTran>>) ((IEnumerable<PXResult<CATaxTran>>) PXSelectBase<CATaxTran, PXViewOf<CATaxTran>.BasedOn<SelectFromBase<CATaxTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CATaxTran.module, Equal<BatchModule.moduleCA>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CATaxTran.tranType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<CATaxTran.refNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<CATaxTran.taxID, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<TaxTran.isTaxInclusive, IBqlBool>.IsEqual<True>>>>>.Config>.Select((PXGraph) this, new object[3]
        {
          (object) casplit.AdjTranType,
          (object) casplit.AdjRefNbr,
          (object) taxCorrespondingToLine.TaxID
        })).ToArray<PXResult<CATaxTran>>()).Any<PXResult<CATaxTran>>())
          taxCorrespondingToLine.TaxCalcLevel = "0";
      }
      this.AdjustTaxCalculationLevelForNetGrossEntryModeFeature(caadj, ref taxCorrespondingToLine);
    }
    return taxCorrespondingToLine;
  }

  private PX.Objects.GL.GLTran CreateDocumentTran(CATran catran, CashAccount cashacct)
  {
    PX.Objects.GL.GLTran documentTran = new PX.Objects.GL.GLTran();
    documentTran.SummPost = new bool?(false);
    documentTran.CuryInfoID = catran.CuryInfoID;
    documentTran.TranType = catran.OrigTranType;
    documentTran.RefNbr = catran.OrigRefNbr;
    documentTran.ReferenceID = catran.ReferenceID;
    documentTran.AccountID = cashacct.AccountID;
    documentTran.SubID = cashacct.SubID;
    documentTran.CATranID = catran.TranID;
    documentTran.TranDate = catran.TranDate;
    documentTran.BranchID = cashacct.BranchID;
    documentTran.CuryDebitAmt = catran.DrCr == "D" ? catran.CuryTranAmt : new Decimal?(0M);
    documentTran.DebitAmt = catran.DrCr == "D" ? catran.TranAmt : new Decimal?(0M);
    Decimal? nullable1;
    Decimal? nullable2;
    if (!(catran.DrCr == "D"))
    {
      Decimal num = -1M;
      nullable1 = catran.CuryTranAmt;
      nullable2 = nullable1.HasValue ? new Decimal?(num * nullable1.GetValueOrDefault()) : new Decimal?();
    }
    else
      nullable2 = new Decimal?(0M);
    documentTran.CuryCreditAmt = nullable2;
    Decimal? nullable3;
    if (!(catran.DrCr == "D"))
    {
      Decimal num = -1M;
      nullable1 = catran.TranAmt;
      nullable3 = nullable1.HasValue ? new Decimal?(num * nullable1.GetValueOrDefault()) : new Decimal?();
    }
    else
      nullable3 = new Decimal?(0M);
    documentTran.CreditAmt = nullable3;
    documentTran.TranDesc = catran.TranDesc;
    documentTran.Released = new bool?(true);
    documentTran.ProjectID = ProjectDefaultAttribute.NonProject();
    return documentTran;
  }

  private bool ShouldCreateRGOLTran(
    PX.Objects.GL.GLTran rgolTranData,
    PX.Objects.CM.Currency rgol_cury,
    PX.Objects.CM.CurrencyInfo rgol_info)
  {
    if (rgol_cury == null || rgol_info == null)
      return false;
    Decimal? debitAmt = rgolTranData.DebitAmt;
    Decimal? creditAmt = rgolTranData.CreditAmt;
    return Math.Abs(Math.Round((debitAmt.HasValue & creditAmt.HasValue ? new Decimal?(debitAmt.GetValueOrDefault() - creditAmt.GetValueOrDefault()) : new Decimal?()).Value, 4)) >= 0.00005M;
  }

  private PX.Objects.GL.GLTran CreateRGOLTran(
    JournalEntry je,
    ICADocument doc,
    PX.Objects.GL.GLTran rgolTranData,
    PX.Objects.CM.Currency rgolCury,
    CashAccount cashacct,
    PX.Objects.CM.CurrencyInfo rgolInfo,
    int? SourceBranchID,
    int? transitAcctID,
    int? transitSubID)
  {
    PX.Objects.GL.GLTran rgolTran = new PX.Objects.GL.GLTran();
    rgolTran.SummPost = new bool?(false);
    Decimal? debitAmt1 = rgolTranData.DebitAmt;
    Decimal? creditAmt1 = rgolTranData.CreditAmt;
    Decimal? nullable1 = debitAmt1.HasValue & creditAmt1.HasValue ? new Decimal?(debitAmt1.GetValueOrDefault() - creditAmt1.GetValueOrDefault()) : new Decimal?();
    if (Math.Sign(nullable1.Value) == 1)
    {
      rgolTran.AccountID = rgolCury.RealLossAcctID;
      rgolTran.SubID = GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Currency.realLossSubID>((PXGraph) je, rgolTranData.BranchID, rgolCury);
      PX.Objects.GL.GLTran glTran = rgolTran;
      Decimal? debitAmt2 = rgolTranData.DebitAmt;
      Decimal? creditAmt2 = rgolTranData.CreditAmt;
      Decimal? nullable2;
      if (!(debitAmt2.HasValue & creditAmt2.HasValue))
      {
        nullable1 = new Decimal?();
        nullable2 = nullable1;
      }
      else
        nullable2 = new Decimal?(debitAmt2.GetValueOrDefault() - creditAmt2.GetValueOrDefault());
      nullable1 = nullable2;
      Decimal? nullable3 = new Decimal?(Math.Round(nullable1.Value, 4));
      glTran.DebitAmt = nullable3;
      rgolTran.CuryDebitAmt = object.Equals((object) rgolInfo.CuryID, (object) rgolInfo.BaseCuryID) ? rgolTran.DebitAmt : new Decimal?(0M);
      rgolTran.CreditAmt = new Decimal?(0M);
      rgolTran.CuryCreditAmt = new Decimal?(0M);
    }
    else
    {
      rgolTran.AccountID = rgolCury.RealGainAcctID;
      rgolTran.SubID = GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Currency.realGainSubID>((PXGraph) je, rgolTranData.BranchID, rgolCury);
      rgolTran.DebitAmt = new Decimal?(0M);
      rgolTran.CuryDebitAmt = new Decimal?(0M);
      PX.Objects.GL.GLTran glTran = rgolTran;
      Decimal? creditAmt3 = rgolTranData.CreditAmt;
      Decimal? debitAmt3 = rgolTranData.DebitAmt;
      Decimal? nullable4;
      if (!(creditAmt3.HasValue & debitAmt3.HasValue))
      {
        nullable1 = new Decimal?();
        nullable4 = nullable1;
      }
      else
        nullable4 = new Decimal?(creditAmt3.GetValueOrDefault() - debitAmt3.GetValueOrDefault());
      nullable1 = nullable4;
      Decimal? nullable5 = new Decimal?(Math.Round(nullable1.Value, 4));
      glTran.CreditAmt = nullable5;
      rgolTran.CuryCreditAmt = object.Equals((object) rgolInfo.CuryID, (object) rgolInfo.BaseCuryID) ? rgolTran.CreditAmt : new Decimal?(0M);
    }
    rgolTran.TranType = "CTG";
    rgolTran.RefNbr = doc.RefNbr;
    rgolTran.TranDesc = "RGOL";
    rgolTran.TranDate = rgolTranData.TranDate;
    rgolTran.BranchID = rgolTranData.BranchID;
    rgolTran.Released = new bool?(true);
    rgolTran.CuryInfoID = rgolInfo.CuryInfoID;
    rgolTran.ProjectID = ProjectDefaultAttribute.NonProject();
    return rgolTran;
  }

  private PX.Objects.GL.GLTran CreateSplitTran(
    CATran catran,
    CASplit casplit,
    PX.Objects.TX.Tax tax,
    bool isTransfer)
  {
    PX.Objects.GL.GLTran splitTran = new PX.Objects.GL.GLTran();
    splitTran.SummPost = new bool?(isTransfer);
    splitTran.ZeroPost = isTransfer ? new bool?(false) : new bool?();
    splitTran.CuryInfoID = casplit.CuryInfoID;
    splitTran.TranType = catran.OrigTranType;
    splitTran.RefNbr = catran.OrigRefNbr;
    splitTran.InventoryID = casplit.InventoryID;
    splitTran.UOM = casplit.UOM;
    splitTran.Qty = casplit.Qty;
    splitTran.AccountID = casplit.AccountID;
    splitTran.SubID = casplit.SubID;
    splitTran.ReclassificationProhibited = new bool?(casplit.ReclassificationProhibited.GetValueOrDefault());
    splitTran.CATranID = new long?();
    splitTran.TranDate = catran.TranDate;
    splitTran.BranchID = casplit.BranchID;
    splitTran.ProjectID = ProjectDefaultAttribute.NonProject();
    if (tax != null && tax.TaxCalcLevel == "0" && tax.TaxType != "W")
    {
      splitTran.CuryDebitAmt = catran.DrCr == "D" ? new Decimal?(0M) : casplit.CuryTaxableAmt;
      splitTran.DebitAmt = catran.DrCr == "D" ? new Decimal?(0M) : casplit.TaxableAmt;
      splitTran.CuryCreditAmt = catran.DrCr == "D" ? casplit.CuryTaxableAmt : new Decimal?(0M);
      splitTran.CreditAmt = catran.DrCr == "D" ? casplit.TaxableAmt : new Decimal?(0M);
    }
    else
    {
      splitTran.CuryDebitAmt = catran.DrCr == "D" ? new Decimal?(0M) : casplit.CuryTranAmt;
      splitTran.DebitAmt = catran.DrCr == "D" ? new Decimal?(0M) : casplit.TranAmt;
      splitTran.CuryCreditAmt = catran.DrCr == "D" ? casplit.CuryTranAmt : new Decimal?(0M);
      splitTran.CreditAmt = catran.DrCr == "D" ? casplit.TranAmt : new Decimal?(0M);
    }
    splitTran.TranDesc = casplit.TranDesc;
    splitTran.ProjectID = casplit.ProjectID;
    splitTran.TaskID = casplit.TaskID;
    splitTran.CostCodeID = casplit.CostCodeID;
    splitTran.TranLineNbr = splitTran.SummPost.GetValueOrDefault() ? new int?() : casplit.LineNbr;
    splitTran.NonBillable = casplit.NonBillable;
    splitTran.Released = new bool?(true);
    return splitTran;
  }

  private PXResultset<CATran> GetResultsetByTranType(ICADocument doc)
  {
    switch (doc.DocType)
    {
      case "CT%":
      case "CTI":
      case "CTO":
        return ((PXSelectBase<CATran>) this.CATranFundsTransfers_Ordered).Select(new object[1]
        {
          (object) doc.RefNbr
        });
      case "CAE":
        return ((PXSelectBase<CATran>) this.CATranCashTrans_Ordered).Select(new object[1]
        {
          (object) doc.RefNbr
        });
      default:
        throw new NotImplementedException();
    }
  }

  protected virtual void PerformBasicReleaseChecks(ICADocument document)
  {
    if (document.Hold.Value)
      throw new PXException("Document on hold cannot be released");
  }

  private void TryToGetCashInTransitAccountAndSub(
    ICADocument doc,
    out int? transitAcctID,
    out int? transitSubID)
  {
    transitAcctID = new int?();
    transitSubID = new int?();
    if (doc.DocType == "CT%")
    {
      CATransfer caTransfer = (CATransfer) doc;
      transitAcctID = caTransfer.TransitAcctID;
      transitSubID = caTransfer.TransitSubID;
    }
    else
    {
      transitAcctID = ((PXSelectBase<CASetup>) this.casetup).Current.TransitAcctId;
      transitSubID = ((PXSelectBase<CASetup>) this.casetup).Current.TransitSubID;
    }
    if (!transitAcctID.HasValue || !transitSubID.HasValue)
      return;
    this.VerifyActivityOfCashInTransitAccountAndSubaccount(transitAcctID, transitSubID);
  }

  private void VerifyCashAccountInGL(CashAccount cashacct)
  {
    PX.Objects.GL.Account account = AccountAttribute.GetAccount((PXGraph) this, cashacct.AccountID);
    if (!account.IsCashAccount.GetValueOrDefault())
      throw new PXException("This cash account is mapped to the {0} GL account for which the Cash Account check box is cleared on the Chart of Accounts (GL202500) form.", new object[1]
      {
        (object) account.AccountCD
      });
  }

  private void VerifyCAAdjNotOnHold(CAAdj caadj)
  {
    if (caadj != null && caadj.TranID.HasValue && caadj.Hold.GetValueOrDefault())
      throw new PXException("Document on hold cannot be released");
  }

  private void VerifyActivityOfAccountAndSubaccount(CashAccount cashacct)
  {
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(((PXSelectBase<PX.Objects.GL.Account>) this.glAccounts).Select(new object[1]
    {
      (object) cashacct.AccountID
    }));
    if (account != null)
    {
      bool? active = account.Active;
      bool flag = false;
      if (active.GetValueOrDefault() == flag & active.HasValue)
        throw new PXException("The document cannot be released because the {0} cash account uses the inactive {1} GL account. To release the document with this cash account, activate the GL account on the Chart of Accounts (GL202500) form.", new object[2]
        {
          (object) cashacct.CashAccountCD,
          (object) account.AccountCD
        });
    }
    PX.Objects.GL.Sub dac = PXResultset<PX.Objects.GL.Sub>.op_Implicit(((PXSelectBase<PX.Objects.GL.Sub>) this.subAccounts).Select(new object[1]
    {
      (object) cashacct.SubID
    }));
    if (dac == null)
      return;
    bool? active1 = dac.Active;
    bool flag1 = false;
    if (active1.GetValueOrDefault() == flag1 & active1.HasValue)
    {
      string formatedMaskField = ((PXSelectBase) this.subAccounts).Cache.GetFormatedMaskField<PX.Objects.GL.Sub.subCD>((IBqlTable) dac);
      throw new PXException("The document cannot be released because the {0} cash account uses the inactive {1} subaccount. To release the document with this cash account, activate the subaccount on the Subaccounts (GL203000) form.", new object[2]
      {
        (object) cashacct.CashAccountCD,
        (object) formatedMaskField
      });
    }
  }

  private void VerifyActivityOfCashInTransitAccountAndSubaccount(
    int? transitAcctID,
    int? transitSubID)
  {
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(((PXSelectBase<PX.Objects.GL.Account>) this.glAccounts).Select(new object[1]
    {
      (object) transitAcctID
    }));
    if (account != null)
    {
      bool? active = account.Active;
      bool flag = false;
      if (active.GetValueOrDefault() == flag & active.HasValue)
        throw new PXException("The document cannot be released because the {0} cash-in-transit account is inactive. Activate the account on the Chart of Accounts (GL202500) form or change the account in the Cash-in-Transit Account box of the Cash Management Preferences (CA101000) form.", new object[1]
        {
          (object) account.AccountCD
        });
    }
    PX.Objects.GL.Sub dac = PXResultset<PX.Objects.GL.Sub>.op_Implicit(((PXSelectBase<PX.Objects.GL.Sub>) this.subAccounts).Select(new object[1]
    {
      (object) transitSubID
    }));
    if (dac == null)
      return;
    bool? active1 = dac.Active;
    bool flag1 = false;
    if (active1.GetValueOrDefault() == flag1 & active1.HasValue)
      throw new PXException("The document cannot be released because the {0} cash-in-transit subaccount is inactive. Activate the subaccount on the Subaccounts (GL203000) form or change the subaccount in the Cash-in-Transit Subaccount box of the Cash Management Preferences (CA101000) form.", new object[1]
      {
        (object) ((PXSelectBase) this.subAccounts).Cache.GetFormatedMaskField<PX.Objects.GL.Sub.subCD>((IBqlTable) dac)
      });
  }

  protected virtual void ProcessTaxDiscrepancy(
    JournalEntry je,
    PX.Objects.GL.Batch arbatch,
    CAAdj doc,
    PX.Objects.CM.CurrencyInfo currencyInfo,
    Decimal docInclTaxDiscrepancy)
  {
    if (docInclTaxDiscrepancy != 0M)
    {
      TXSetup txSetup = PXResultset<TXSetup>.op_Implicit(PXSetup<TXSetup>.Select((PXGraph) this, Array.Empty<object>()));
      int? nullable1;
      int num1;
      if (txSetup == null)
      {
        num1 = 1;
      }
      else
      {
        nullable1 = txSetup.TaxRoundingGainAcctID;
        num1 = !nullable1.HasValue ? 1 : 0;
      }
      if (num1 == 0)
      {
        int num2;
        if (txSetup == null)
        {
          num2 = 1;
        }
        else
        {
          nullable1 = txSetup.TaxRoundingLossAcctID;
          num2 = !nullable1.HasValue ? 1 : 0;
        }
        if (num2 == 0)
        {
          bool flag = doc.DrCr == "D";
          int? nullable2 = flag == docInclTaxDiscrepancy > 0M ? txSetup.TaxRoundingLossAcctID : txSetup.TaxRoundingGainAcctID;
          int? nullable3 = flag == docInclTaxDiscrepancy > 0M ? txSetup.TaxRoundingLossSubID : txSetup.TaxRoundingGainSubID;
          PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
          glTran1.SummPost = new bool?(false);
          glTran1.BranchID = doc.BranchID;
          glTran1.CuryInfoID = currencyInfo.CuryInfoID;
          glTran1.TranType = doc.DocType;
          glTran1.TranClass = "R";
          glTran1.RefNbr = doc.RefNbr;
          glTran1.TranDate = doc.DocDate;
          glTran1.AccountID = nullable2;
          glTran1.SubID = nullable3;
          glTran1.TranDesc = "Tax rounding difference";
          glTran1.CuryDebitAmt = new Decimal?(flag ? docInclTaxDiscrepancy : 0M);
          glTran1.DebitAmt = new Decimal?(flag ? docInclTaxDiscrepancy : 0M);
          glTran1.CuryCreditAmt = new Decimal?(flag ? 0M : docInclTaxDiscrepancy);
          glTran1.CreditAmt = new Decimal?(flag ? 0M : docInclTaxDiscrepancy);
          glTran1.Released = new bool?(true);
          PX.Objects.GL.GLTran glTran2 = glTran1;
          nullable1 = new int?();
          int? nullable4 = nullable1;
          glTran2.ReferenceID = nullable4;
          ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(glTran1);
          return;
        }
      }
      throw new PXException("Tax rounding gain and loss accounts cannot be empty. Specify these accounts on the Tax Preferences (TX103000) form.");
    }
  }

  protected virtual void AdjustTaxCalculationLevelForNetGrossEntryModeFeature(
    CAAdj document,
    ref PX.Objects.TX.Tax taxCorrespondingToLine)
  {
    CAReleaseProcess.AdjustTaxCalculationLevelForNetGrossEntryMode(document, ref taxCorrespondingToLine);
  }

  public static void AdjustTaxCalculationLevelForNetGrossEntryMode(
    CAAdj document,
    ref PX.Objects.TX.Tax taxCorrespondingToLine)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>())
      return;
    string taxCalcMode = document.TaxCalcMode;
    switch (taxCalcMode)
    {
      case "G":
        taxCorrespondingToLine.TaxCalcLevel = "0";
        break;
      case "N":
        taxCorrespondingToLine.TaxCalcLevel = "1";
        break;
      default:
        int num = taxCalcMode == "T" ? 1 : 0;
        break;
    }
  }

  protected virtual void OnReleaseComplete(ICADocument doc)
  {
  }

  private PX.Objects.GL.Batch CreateGLTransFromCAExpencesForCATransfer<TCADocument>(
    JournalEntry je,
    List<PX.Objects.GL.Batch> batchlist,
    TCADocument doc,
    PX.Objects.GL.Batch transferBatchOut,
    CATran transferTranOut,
    PX.Objects.GL.Batch transferBatchIn,
    CATran transferTranIn)
    where TCADocument : class, ICADocument, new()
  {
    PX.Objects.GL.Batch current = ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current;
    PX.Objects.GL.Batch expencesForCaTransfer = (PX.Objects.GL.Batch) null;
    bool flag1 = false;
    foreach (PXResult<CAExpense> pxResult in ((PXSelectBase<CAExpense>) this.CAExpense_RefNbr).Select(new object[1]
    {
      (object) doc.RefNbr
    }))
    {
      CAExpense caExpense = PXResult<CAExpense>.op_Implicit(pxResult);
      bool flag2 = true;
      bool flag3 = true;
      int? cashAccountId1 = caExpense.CashAccountID;
      int? cashAccountId2 = transferTranOut.CashAccountID;
      int num;
      if (!(cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue))
      {
        cashAccountId2 = caExpense.CashAccountID;
        cashAccountId1 = transferTranIn.CashAccountID;
        num = cashAccountId2.GetValueOrDefault() == cashAccountId1.GetValueOrDefault() & cashAccountId2.HasValue == cashAccountId1.HasValue ? 1 : 0;
      }
      else
        num = 1;
      bool flag4 = num != 0;
      if (this.ShouldNotSegregate(transferBatchOut.BranchID, transferTranOut, caExpense))
      {
        ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current = transferBatchOut;
        flag2 = false;
      }
      if (this.ShouldNotSegregate(transferBatchIn.BranchID, transferTranIn, caExpense))
      {
        ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current = transferBatchIn;
        flag3 = false;
      }
      if (flag2 & flag3 || !flag4)
      {
        PX.Objects.CM.CurrencyInfo curyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<CAExpense.curyInfoID>>>>.Config>.Select((PXGraph) je, new object[1]
        {
          (object) caExpense.CuryInfoID
        }));
        this.SegregateBatch(je, batchlist, caExpense.BranchID, caExpense.CuryID, caExpense.TranDate, caExpense.FinPeriodID, caExpense.TranDesc, curyInfo);
      }
      expencesForCaTransfer = ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current;
      this.GenerateGLTranPairForExpense(je, caExpense);
      this.GenerateGLTranForExpenseTaxes(je, caExpense);
      caExpense.Released = new bool?(true);
      this.SetAdjReleased(caExpense);
      ((PXSelectBase<CAExpense>) this.CAExpense_RefNbr).Update(caExpense);
      flag1 = true;
    }
    if (flag1)
      ((PXAction) je.Save).Press();
    ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current = current;
    return expencesForCaTransfer;
  }

  protected virtual void GenerateGLTranForExpenseTaxes(JournalEntry je, CAExpense expense)
  {
    foreach (PXResult<CAExpenseTaxTran, PX.Objects.TX.Tax> pxResult1 in ((PXSelectBase<CAExpenseTaxTran>) this.CAExpenseTaxTran_TranType_RefNbr).Select(new object[2]
    {
      (object) expense.RefNbr,
      (object) expense.LineNbr
    }))
    {
      CAExpenseTaxTran x = PXResult<CAExpenseTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult1);
      PX.Objects.TX.Tax salestax = PXResult<CAExpenseTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult1);
      if (!(salestax.TaxType == "W"))
      {
        if (expense.DrCr == "C" && (salestax.TaxType == "P" || salestax.TaxType == "S"))
        {
          this.PostGeneralTax(je, expense, x, salestax, salestax.ExpenseAccountID, salestax.ExpenseSubID);
          if (salestax.TaxType == "P")
            this.PostReverseTax(je, expense, x, salestax);
        }
        else
        {
          bool? nullable = salestax.ReverseTax;
          if (!nullable.GetValueOrDefault())
            this.PostGeneralTax(je, expense, x, salestax);
          else
            this.PostReverseTax(je, expense, x, salestax);
          nullable = salestax.DeductibleVAT;
          if (nullable.GetValueOrDefault())
          {
            nullable = salestax.ReportExpenseToSingleAccount;
            if (nullable.GetValueOrDefault())
            {
              PX.Objects.GL.GLTran glTran = new PX.Objects.GL.GLTran();
              glTran.SummPost = new bool?(false);
              glTran.BranchID = expense.BranchID;
              glTran.CuryInfoID = expense.CuryInfoID;
              glTran.TranType = "CTE";
              glTran.TranClass = "T";
              glTran.RefNbr = expense.RefNbr;
              glTran.TranDate = expense.TranDate;
              glTran.AccountID = salestax.ExpenseAccountID;
              glTran.SubID = salestax.ExpenseSubID;
              glTran.TranDesc = salestax.TaxID;
              bool flag = expense.DrCr == "C";
              glTran.CuryDebitAmt = flag ? x.CuryExpenseAmt : new Decimal?(0M);
              glTran.DebitAmt = flag ? x.ExpenseAmt : new Decimal?(0M);
              glTran.CuryCreditAmt = flag ? new Decimal?(0M) : x.CuryExpenseAmt;
              glTran.CreditAmt = flag ? new Decimal?(0M) : x.ExpenseAmt;
              glTran.Released = new bool?(true);
              glTran.ReferenceID = new int?();
              glTran.ProjectID = ProjectDefaultAttribute.NonProject();
              ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(glTran);
            }
            else if (salestax.TaxCalcType == "I")
            {
              PXResultset<CAExpenseTax> deductibleLines = this.GetDeductibleLines(salestax, x);
              new APTaxAttribute(typeof (CARegister), typeof (CAExpenseTax), typeof (CAExpenseTaxTran)).DistributeTaxDiscrepancy<CAExpenseTax, CAExpenseTax.curyExpenseAmt, CAExpenseTax.expenseAmt>((PXGraph) this, deductibleLines.FirstTableItems, x.CuryExpenseAmt.Value);
              foreach (PXResult<CAExpenseTax, CASplit> pxResult2 in deductibleLines)
              {
                CAExpenseTax caExpenseTax = PXResult<CAExpenseTax, CASplit>.op_Implicit(pxResult2);
                CASplit caSplit = PXResult<CAExpenseTax, CASplit>.op_Implicit(pxResult2);
                PX.Objects.GL.GLTran glTran = new PX.Objects.GL.GLTran();
                glTran.SummPost = new bool?(false);
                glTran.BranchID = caSplit.BranchID;
                glTran.CuryInfoID = expense.CuryInfoID;
                glTran.TranType = "CTE";
                glTran.TranClass = "T";
                glTran.RefNbr = expense.RefNbr;
                glTran.TranDate = expense.TranDate;
                glTran.AccountID = caSplit.AccountID;
                glTran.SubID = caSplit.SubID;
                glTran.TranDesc = salestax.TaxID;
                glTran.TranLineNbr = caSplit.LineNbr;
                bool flag = expense.DrCr == "C";
                glTran.CuryDebitAmt = flag ? caExpenseTax.CuryExpenseAmt : new Decimal?(0M);
                glTran.DebitAmt = flag ? caExpenseTax.ExpenseAmt : new Decimal?(0M);
                glTran.CuryCreditAmt = flag ? new Decimal?(0M) : caExpenseTax.CuryExpenseAmt;
                glTran.CreditAmt = flag ? new Decimal?(0M) : caExpenseTax.ExpenseAmt;
                glTran.Released = new bool?(true);
                glTran.ReferenceID = new int?();
                glTran.ProjectID = caSplit.ProjectID;
                glTran.TaskID = caSplit.TaskID;
                glTran.CostCodeID = caSplit.CostCodeID;
                ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(glTran);
              }
            }
          }
        }
      }
    }
  }

  protected virtual void PostGeneralTax(
    JournalEntry je,
    CAExpense expense,
    CAExpenseTaxTran x,
    PX.Objects.TX.Tax salestax,
    int? accountID = null,
    int? subID = null)
  {
    bool isCredit = expense.DrCr == "C";
    PX.Objects.GL.GLTran glTran = CAReleaseProcess.GenerateGLTran(expense, x, salestax, accountID, subID);
    CAReleaseProcess.SetAmounts(x, glTran, isCredit, true);
    ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(glTran);
  }

  protected virtual void PostReverseTax(
    JournalEntry je,
    CAExpense expense,
    CAExpenseTaxTran x,
    PX.Objects.TX.Tax salestax)
  {
    bool isCredit = expense.DrCr == "C";
    PX.Objects.GL.GLTran glTran = CAReleaseProcess.GenerateGLTran(expense, x, salestax);
    CAReleaseProcess.SetAmounts(x, glTran, isCredit, false);
    ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(glTran);
  }

  private static PX.Objects.GL.GLTran GenerateGLTran(
    CAExpense expense,
    CAExpenseTaxTran x,
    PX.Objects.TX.Tax salestax,
    int? accountID = null,
    int? subID = null)
  {
    PX.Objects.GL.GLTran glTran = new PX.Objects.GL.GLTran();
    glTran.SummPost = new bool?(false);
    glTran.CuryInfoID = expense.CuryInfoID;
    glTran.TranType = "CTE";
    glTran.TranClass = "T";
    glTran.RefNbr = expense.RefNbr;
    glTran.TranDate = expense.TranDate;
    glTran.AccountID = accountID ?? x.AccountID;
    int? nullable = subID;
    glTran.SubID = nullable ?? x.SubID;
    glTran.TranDesc = salestax.TaxID;
    glTran.Released = new bool?(true);
    nullable = new int?();
    glTran.ReferenceID = nullable;
    glTran.BranchID = expense.BranchID;
    glTran.ProjectID = ProjectDefaultAttribute.NonProject();
    return glTran;
  }

  private static void SetAmounts(
    CAExpenseTaxTran x,
    PX.Objects.GL.GLTran taxTran,
    bool isCredit,
    bool isDirectPosting)
  {
    if (isDirectPosting)
    {
      taxTran.CuryDebitAmt = isCredit ? x.CuryTaxAmt : new Decimal?(0M);
      taxTran.DebitAmt = isCredit ? x.TaxAmt : new Decimal?(0M);
      taxTran.CuryCreditAmt = isCredit ? new Decimal?(0M) : x.CuryTaxAmt;
      taxTran.CreditAmt = isCredit ? new Decimal?(0M) : x.TaxAmt;
    }
    else
    {
      taxTran.CuryDebitAmt = isCredit ? new Decimal?(0M) : x.CuryTaxAmt;
      taxTran.DebitAmt = isCredit ? new Decimal?(0M) : x.TaxAmt;
      taxTran.CuryCreditAmt = isCredit ? x.CuryTaxAmt : new Decimal?(0M);
      taxTran.CreditAmt = isCredit ? x.TaxAmt : new Decimal?(0M);
    }
  }

  protected virtual bool ShouldNotSegregate(CATran tranOut, CATran tranIn)
  {
    if (!(tranOut?.CuryID == tranIn?.CuryID) || !(tranOut?.TranPeriodID == tranIn?.TranPeriodID))
      return false;
    DateTime? tranDate1 = (DateTime?) tranOut?.TranDate;
    DateTime? tranDate2 = (DateTime?) tranIn?.TranDate;
    if (tranDate1.HasValue != tranDate2.HasValue)
      return false;
    return !tranDate1.HasValue || tranDate1.GetValueOrDefault() == tranDate2.GetValueOrDefault();
  }

  protected virtual bool ShouldNotSegregate(int? BranchID, CATran transferTran, CAExpense charge)
  {
    int? branchId = charge.BranchID;
    int? nullable = BranchID;
    if (!(branchId.GetValueOrDefault() == nullable.GetValueOrDefault() & branchId.HasValue == nullable.HasValue) || !(charge.CuryID == transferTran?.CuryID) || !(charge.TranPeriodID == transferTran?.TranPeriodID))
      return false;
    DateTime? tranDate1 = charge.TranDate;
    DateTime? tranDate2 = (DateTime?) transferTran?.TranDate;
    if (tranDate1.HasValue != tranDate2.HasValue)
      return false;
    return !tranDate1.HasValue || tranDate1.GetValueOrDefault() == tranDate2.GetValueOrDefault();
  }

  [Obsolete("Will be removed in Acumatica 2019R2")]
  private void SetAdjReleased(CAExpense charge)
  {
    if (string.IsNullOrEmpty(charge.AdjRefNbr))
      return;
    CAAdj caAdj = PXResultset<CAAdj>.op_Implicit(PXSelectBase<CAAdj, PXSelect<CAAdj, Where<CAAdj.adjRefNbr, Equal<Required<CAExpense.adjRefNbr>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) charge.AdjRefNbr
    }));
    caAdj.Released = new bool?(true);
    ((PXGraph) this).Caches[typeof (CAAdj)].Update((object) caAdj);
  }

  protected virtual void GenerateGLTranPairForExpense(JournalEntry je, CAExpense charge)
  {
    bool flag = charge.DrCr == "D";
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) charge.CashAccountID
    }));
    this.VerifyActivityOfExpenseAccountAndSub(charge, cashAccount);
    this.VerifyCashAccountInGL(cashAccount);
    PX.Objects.GL.GLTran row1 = new PX.Objects.GL.GLTran();
    row1.SummPost = new bool?(true);
    row1.BranchID = charge.BranchID;
    row1.AccountID = cashAccount.AccountID;
    row1.SubID = cashAccount.SubID;
    row1.CuryDebitAmt = flag ? charge.CuryTranAmt : new Decimal?(0M);
    row1.DebitAmt = flag ? charge.TranAmt : new Decimal?(0M);
    row1.CuryCreditAmt = flag ? new Decimal?(0M) : charge.CuryTranAmt;
    row1.CreditAmt = flag ? new Decimal?(0M) : charge.TranAmt;
    row1.TranType = charge.DocType;
    row1.RefNbr = charge.RefNbr;
    row1.TranLineNbr = charge.LineNbr;
    row1.TranDesc = charge.TranDesc;
    row1.TranDate = charge.TranDate;
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) row1, charge.TranPeriodID);
    row1.CuryInfoID = charge.CuryInfoID;
    row1.Released = new bool?(true);
    row1.CATranID = charge.CashTranID;
    ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(row1);
    PX.Objects.GL.GLTran row2 = new PX.Objects.GL.GLTran();
    row2.SummPost = new bool?(true);
    row2.ZeroPost = new bool?(false);
    row2.BranchID = charge.BranchID;
    row2.AccountID = charge.AccountID;
    row2.SubID = charge.SubID;
    CAExpenseTaxTran caExpenseTaxTran1 = new CAExpenseTaxTran();
    caExpenseTaxTran1.CuryTaxAmt = new Decimal?(0M);
    caExpenseTaxTran1.TaxAmt = new Decimal?(0M);
    CAExpenseTaxTran caExpenseTaxTran2 = caExpenseTaxTran1;
    foreach (PXResult<CAExpenseTaxTran, PX.Objects.TX.Tax> pxResult in ((PXSelectBase<CAExpenseTaxTran>) this.CAExpenseTaxTran_TranType_RefNbr).Select(new object[2]
    {
      (object) charge.RefNbr,
      (object) charge.LineNbr
    }))
    {
      CAExpenseTaxTran caExpenseTaxTran3 = PXResult<CAExpenseTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult);
      PX.Objects.TX.Tax tax = PXResult<CAExpenseTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult);
      CAExpenseTaxTran caExpenseTaxTran4 = caExpenseTaxTran2;
      Decimal? curyTaxAmt1 = caExpenseTaxTran4.CuryTaxAmt;
      Decimal? curyTaxAmt2 = caExpenseTaxTran3.CuryTaxAmt;
      Decimal? nullable1 = tax.DeductibleVAT.GetValueOrDefault() ? caExpenseTaxTran3.CuryExpenseAmt : new Decimal?(0M);
      Decimal? nullable2 = curyTaxAmt2.HasValue & nullable1.HasValue ? new Decimal?(curyTaxAmt2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
      caExpenseTaxTran4.CuryTaxAmt = curyTaxAmt1.HasValue & nullable2.HasValue ? new Decimal?(curyTaxAmt1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      CAExpenseTaxTran caExpenseTaxTran5 = caExpenseTaxTran2;
      Decimal? taxAmt1 = caExpenseTaxTran5.TaxAmt;
      Decimal? taxAmt2 = caExpenseTaxTran3.TaxAmt;
      Decimal? nullable3 = tax.DeductibleVAT.GetValueOrDefault() ? caExpenseTaxTran3.ExpenseAmt : new Decimal?(0M);
      Decimal? nullable4 = taxAmt2.HasValue & nullable3.HasValue ? new Decimal?(taxAmt2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
      caExpenseTaxTran5.TaxAmt = taxAmt1.HasValue & nullable4.HasValue ? new Decimal?(taxAmt1.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
      caExpenseTaxTran2.CuryTaxableAmt = caExpenseTaxTran3.CuryTaxableAmt;
    }
    if (charge.TaxCategoryID != null && charge.TaxZoneID != null && caExpenseTaxTran2 != null && caExpenseTaxTran2.CuryTaxableAmt.HasValue && Math.Abs(caExpenseTaxTran2.CuryTaxableAmt.Value) < Math.Abs(charge.CuryTranAmt.Value))
    {
      Decimal? nullable5 = charge.CuryTranAmt;
      Decimal? nullable6 = caExpenseTaxTran2.CuryTaxAmt;
      Decimal? nullable7 = nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
      nullable6 = charge.TranAmt;
      nullable5 = caExpenseTaxTran2.TaxAmt;
      Decimal? nullable8 = nullable6.HasValue & nullable5.HasValue ? new Decimal?(nullable6.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new Decimal?();
      row2.CuryDebitAmt = flag ? new Decimal?(0M) : nullable7;
      row2.DebitAmt = flag ? new Decimal?(0M) : nullable8;
      row2.CuryCreditAmt = flag ? nullable7 : new Decimal?(0M);
      row2.CreditAmt = flag ? nullable8 : new Decimal?(0M);
    }
    else
    {
      row2.CuryDebitAmt = flag ? new Decimal?(0M) : charge.CuryTranAmt;
      row2.DebitAmt = flag ? new Decimal?(0M) : charge.TranAmt;
      row2.CuryCreditAmt = flag ? charge.CuryTranAmt : new Decimal?(0M);
      row2.CreditAmt = flag ? charge.TranAmt : new Decimal?(0M);
    }
    row2.TranType = charge.DocType;
    row2.RefNbr = charge.RefNbr;
    row2.TranDesc = charge.TranDesc;
    row2.TranDate = charge.TranDate;
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) row2, charge.TranPeriodID);
    row2.CuryInfoID = charge.CuryInfoID;
    row2.Released = new bool?(true);
    ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(row2);
  }

  protected virtual void VerifyActivityOfExpenseAccountAndSub(
    CAExpense charge,
    CashAccount expCashcacount)
  {
    PX.Objects.GL.Account account1 = PXResultset<PX.Objects.GL.Account>.op_Implicit(((PXSelectBase<PX.Objects.GL.Account>) this.glAccounts).Select(new object[1]
    {
      (object) expCashcacount.AccountID
    }));
    if (account1 != null)
    {
      bool? active = account1.Active;
      bool flag = false;
      if (active.GetValueOrDefault() == flag & active.HasValue)
        throw new PXException("The document cannot be released because the {0} cash account uses the inactive {1} GL account. To release the document with this cash account, activate the GL account on the Chart of Accounts (GL202500) form.", new object[2]
        {
          (object) expCashcacount.CashAccountCD,
          (object) account1.AccountCD
        });
    }
    PX.Objects.GL.Account account2 = PXResultset<PX.Objects.GL.Account>.op_Implicit(((PXSelectBase<PX.Objects.GL.Account>) this.glAccounts).Select(new object[1]
    {
      (object) charge.AccountID
    }));
    if (account2 != null)
    {
      bool? active = account2.Active;
      bool flag = false;
      if (active.GetValueOrDefault() == flag & active.HasValue)
        throw new PXException("The document cannot be released because an expense uses the inactive {0} GL Account. To release the document with this GL Account, activate the account on the Chart of Accounts (GL202500) form.", new object[1]
        {
          (object) account2.AccountCD
        });
    }
    PX.Objects.GL.Sub dac1 = PXResultset<PX.Objects.GL.Sub>.op_Implicit(((PXSelectBase<PX.Objects.GL.Sub>) this.subAccounts).Select(new object[1]
    {
      (object) expCashcacount.SubID
    }));
    if (dac1 != null)
    {
      bool? active = dac1.Active;
      bool flag = false;
      if (active.GetValueOrDefault() == flag & active.HasValue)
      {
        string formatedMaskField = ((PXSelectBase) this.subAccounts).Cache.GetFormatedMaskField<PX.Objects.GL.Sub.subCD>((IBqlTable) dac1);
        throw new PXException("The document cannot be released because the {0} cash account uses the inactive {1} subaccount. To release the document with this cash account, activate the subaccount on the Subaccounts (GL203000) form.", new object[2]
        {
          (object) expCashcacount.CashAccountCD,
          (object) formatedMaskField
        });
      }
    }
    PX.Objects.GL.Sub dac2 = PXResultset<PX.Objects.GL.Sub>.op_Implicit(((PXSelectBase<PX.Objects.GL.Sub>) this.subAccounts).Select(new object[1]
    {
      (object) charge.SubID
    }));
    if (dac2 == null)
      return;
    bool? active1 = dac2.Active;
    bool flag1 = false;
    if (active1.GetValueOrDefault() == flag1 & active1.HasValue)
      throw new PXException("The document cannot be released because an expense uses the inactive {0} Offset Subaccount. To release the document with this subaccount, activate the subaccount on the Subaccounts (GL203000) form.", new object[1]
      {
        (object) ((PXSelectBase) this.subAccounts).Cache.GetFormatedMaskField<PX.Objects.GL.Sub.subCD>((IBqlTable) dac2)
      });
  }

  protected virtual void AddRoundingTran(
    JournalEntry je,
    ICADocument doc,
    PX.Objects.GL.Batch batch,
    PX.Objects.CM.Currency cury,
    long? curyInfoId)
  {
    Decimal? debitTotal = batch.DebitTotal;
    Decimal? nullable1 = batch.CreditTotal;
    if (!(Math.Abs(Math.Round((debitTotal.HasValue & nullable1.HasValue ? new Decimal?(debitTotal.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?()).Value, 4)) >= 0.00005M))
      return;
    PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
    glTran1.SummPost = new bool?(true);
    glTran1.ZeroPost = new bool?(false);
    Decimal? nullable2 = batch.DebitTotal;
    nullable1 = batch.CreditTotal;
    if (Math.Sign((nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?()).Value) == 1)
    {
      glTran1.AccountID = cury.RoundingGainAcctID;
      glTran1.SubID = GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Currency.roundingGainSubID>((PXGraph) je, batch.BranchID, cury);
      PX.Objects.GL.GLTran glTran2 = glTran1;
      nullable2 = batch.DebitTotal;
      nullable1 = batch.CreditTotal;
      Decimal? nullable3 = new Decimal?(Math.Round((nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?()).Value, 4));
      glTran2.CreditAmt = nullable3;
      glTran1.DebitAmt = new Decimal?(0M);
    }
    else
    {
      glTran1.AccountID = cury.RoundingLossAcctID;
      glTran1.SubID = GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Currency.roundingLossSubID>((PXGraph) je, batch.BranchID, cury);
      glTran1.CreditAmt = new Decimal?(0M);
      PX.Objects.GL.GLTran glTran3 = glTran1;
      nullable2 = batch.CreditTotal;
      nullable1 = batch.DebitTotal;
      Decimal? nullable4 = new Decimal?(Math.Round((nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?()).Value, 4));
      glTran3.DebitAmt = nullable4;
    }
    glTran1.CuryCreditAmt = new Decimal?(0M);
    glTran1.CuryDebitAmt = new Decimal?(0M);
    glTran1.TranType = doc.DocType;
    glTran1.RefNbr = doc.RefNbr;
    glTran1.TranClass = "N";
    glTran1.TranDesc = "Rounding difference";
    glTran1.LedgerID = batch.LedgerID;
    glTran1.BranchID = batch.BranchID;
    FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran1, batch.TranPeriodID);
    glTran1.TranDate = batch.DateEntered;
    glTran1.Released = new bool?(true);
    glTran1.ProjectID = ProjectDefaultAttribute.NonProject();
    PX.Objects.CM.CurrencyInfo copy = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(GraphHelper.Caches<PX.Objects.CM.CurrencyInfo>((PXGraph) je).Locate(new PX.Objects.CM.CurrencyInfo()
    {
      CuryInfoID = curyInfoId
    }) ?? new PX.Objects.CM.CurrencyInfo());
    copy.CuryInfoID = new long?();
    PX.Objects.CM.CurrencyInfo currencyInfo = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) je.currencyinfo).Insert(copy) ?? new PX.Objects.CM.CurrencyInfo();
    glTran1.CuryInfoID = currencyInfo.CuryInfoID;
    this.InsertRoundingTransaction(je, glTran1, new CAReleaseProcess.GLTranInsertionContext()
    {
      CAAdjRecord = doc as CAAdj,
      CATransferRecord = doc as CATransfer
    });
  }

  protected void CheckIfUnreleasedVoidedEntryExist(CADepositDetail detail)
  {
    bool flag = false;
    switch (detail.OrigModule)
    {
      case "AR":
        string str1 = ARPaymentType.AllVoidingTypes.Contains(detail.OrigDocType) ? detail.OrigDocType : ARPaymentType.GetVoidingARDocType(detail.OrigDocType);
        if (!string.IsNullOrEmpty(str1))
        {
          flag = ((IQueryable<PXResult<PX.Objects.AR.ARRegister>>) PXSelectBase<PX.Objects.AR.ARRegister, PXSelect<PX.Objects.AR.ARRegister, Where<PX.Objects.AR.ARRegister.released, NotEqual<True>, And<PX.Objects.AR.ARRegister.docType, Equal<Required<PX.Objects.AR.ARRegister.docType>>, And<PX.Objects.AR.ARRegister.refNbr, Equal<Required<PX.Objects.AR.ARRegister.refNbr>>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) str1,
            (object) detail.OrigRefNbr
          })).Any<PXResult<PX.Objects.AR.ARRegister>>();
          break;
        }
        break;
      case "AP":
        string str2 = APPaymentType.AllVoidingTypes.Contains(detail.OrigDocType) ? detail.OrigDocType : APPaymentType.GetVoidingAPDocType(detail.OrigDocType);
        if (!string.IsNullOrEmpty(str2))
        {
          flag = ((IQueryable<PXResult<PX.Objects.AP.APRegister>>) PXSelectBase<PX.Objects.AP.APRegister, PXSelect<PX.Objects.AP.APRegister, Where<PX.Objects.AP.APRegister.released, NotEqual<True>, And<PX.Objects.AP.APRegister.docType, Equal<Required<PX.Objects.AP.APRegister.docType>>, And<PX.Objects.AP.APRegister.refNbr, Equal<Required<PX.Objects.AP.APRegister.refNbr>>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) str2,
            (object) detail.OrigRefNbr
          })).Any<PXResult<PX.Objects.AP.APRegister>>();
          break;
        }
        break;
    }
    if (flag)
      throw new PXException("The voided deposit cannot be released because at least one included payment has an unreleased voiding entry. Delete the unreleased voided payments before releasing the voided deposit.");
  }

  public bool IsVoidedEntryWithoutPair(CADepositDetail detail)
  {
    if (PXSelectBase<PX.Objects.AR.ARRegister, PXSelect<PX.Objects.AR.ARRegister, Where<PX.Objects.AR.ARRegister.voided, Equal<True>, And<PX.Objects.AR.ARRegister.docType, Equal<Required<PX.Objects.AR.ARRegister.docType>>, And<PX.Objects.AR.ARRegister.refNbr, Equal<Required<PX.Objects.AR.ARRegister.refNbr>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) detail.OrigDocType,
      (object) detail.OrigRefNbr
    }).Count == 0)
      return false;
    string voidingArDocType = ARPaymentType.GetVoidingARDocType(detail.OrigDocType);
    return PXSelectBase<CATran, PXSelectJoin<CATran, InnerJoin<CADepositDetail, On<CADepositDetail.tranType, Equal<CATran.origTranType>, And<CADepositDetail.refNbr, Equal<CATran.origRefNbr>, And<CADepositDetail.tranID, Equal<CATran.tranID>>>>>, Where<CATran.origModule, Equal<BatchModule.moduleCA>, And<CATran.origTranType, Equal<Required<CATran.origTranType>>, And<CATran.origRefNbr, Equal<Required<CATran.origRefNbr>>, And<CADepositDetail.origModule, Equal<BatchModule.moduleAR>, And<CADepositDetail.origDocType, Equal<Required<CADepositDetail.origDocType>>, And<CADepositDetail.origRefNbr, Equal<Required<CADepositDetail.origRefNbr>>>>>>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) detail.TranType,
      (object) detail.RefNbr,
      (object) voidingArDocType,
      (object) detail.OrigRefNbr
    }).Count != 1;
  }

  public virtual void ReleaseDeposit(JournalEntry je, ref List<PX.Objects.GL.Batch> batchlist, CADeposit doc)
  {
    ((PXGraph) je).Clear();
    PX.Objects.CM.Currency currency = (PX.Objects.CM.Currency) null;
    PX.Objects.CM.Currency cury = (PX.Objects.CM.Currency) null;
    Dictionary<int, PX.Objects.GL.GLTran> dictionary1 = new Dictionary<int, PX.Objects.GL.GLTran>();
    CATran objA = (CATran) null;
    PX.Objects.GL.Batch batch = CAReleaseProcess.CreateGLBatch(je, doc);
    HashSet<long> caTranIDs = new HashSet<long>();
    HashSet<int> accountIDs = new HashSet<int>();
    string str = doc.TranType == "CVD" ? "CVX" : "CDX";
    PXResultset<CATran> source = ((PXSelectBase<CATran>) new PXSelectJoin<CATran, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<CATran.cashAccountID>>, InnerJoin<PX.Objects.CM.Currency, On<PX.Objects.CM.Currency.curyID, Equal<CashAccount.curyID>>, InnerJoin<PX.Objects.CM.CurrencyInfo, On<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<CATran.curyInfoID>>, LeftJoin<CADepositDetail, On<CADepositDetail.tranType, Equal<CATran.origTranType>, And<CADepositDetail.refNbr, Equal<CATran.origRefNbr>, And<CADepositDetail.tranID, Equal<CATran.tranID>>>>, LeftJoin<CADepositEntry.ARPaymentUpdate, On<CADepositEntry.ARPaymentUpdate.docType, Equal<CADepositDetail.origDocType>, And<CADepositEntry.ARPaymentUpdate.refNbr, Equal<CADepositDetail.origRefNbr>, And<CADepositDetail.origModule, Equal<BatchModule.moduleAR>>>>, LeftJoin<CADepositEntry.APPaymentUpdate, On<CADepositEntry.APPaymentUpdate.docType, Equal<CADepositDetail.origDocType>, And<CADepositEntry.APPaymentUpdate.refNbr, Equal<CADepositDetail.origRefNbr>, And<CADepositDetail.origModule, Equal<BatchModule.moduleAP>>>>, LeftJoin<CADepositEntry.CAAdjUpdate, On<CADepositEntry.CAAdjUpdate.adjTranType, Equal<CADepositDetail.origDocType>, And<CADepositEntry.CAAdjUpdate.adjRefNbr, Equal<CADepositDetail.origRefNbr>, And<CADepositDetail.origModule, Equal<BatchModule.moduleCA>>>>, LeftJoin<CCBatch, On<CCBatch.depositType, Equal<CADepositDetail.tranType>, And<CCBatch.depositNbr, Equal<CADepositDetail.refNbr>>>>>>>>>>>, Where<CATran.origModule, Equal<BatchModule.moduleCA>, And2<Where<CATran.origTranType, Equal<Required<CATran.origTranType>>, Or<CATran.origTranType, Equal<Required<CATran.origTranType>>>>, And<CATran.origRefNbr, Equal<Required<CATran.origRefNbr>>>>>, OrderBy<Asc<CATran.tranID>>>((PXGraph) this)).Select(new object[3]
    {
      (object) doc.DocType,
      (object) str,
      (object) doc.RefNbr
    });
    this.FinPeriodUtils.ValidateFinPeriod((IEnumerable<IAccountable>) ((IEnumerable<PXResult<CATran>>) source).AsEnumerable<PXResult<CATran>>().Select<PXResult<CATran>, CATran>((Func<PXResult<CATran>, CATran>) (result => PXResult<CATran>.op_Implicit(result))), typeof (OrganizationFinPeriod.cAClosed));
    foreach (PXResult<CATran, CashAccount, PX.Objects.CM.Currency, PX.Objects.CM.CurrencyInfo, CADepositDetail, CADepositEntry.ARPaymentUpdate, CADepositEntry.APPaymentUpdate, CADepositEntry.CAAdjUpdate, CCBatch> pxResult in source)
    {
      CATran objB = PXResult<CATran, CashAccount, PX.Objects.CM.Currency, PX.Objects.CM.CurrencyInfo, CADepositDetail, CADepositEntry.ARPaymentUpdate, CADepositEntry.APPaymentUpdate, CADepositEntry.CAAdjUpdate, CCBatch>.op_Implicit(pxResult);
      CashAccount cashacct = PXResult<CATran, CashAccount, PX.Objects.CM.Currency, PX.Objects.CM.CurrencyInfo, CADepositDetail, CADepositEntry.ARPaymentUpdate, CADepositEntry.APPaymentUpdate, CADepositEntry.CAAdjUpdate, CCBatch>.op_Implicit(pxResult);
      cury = PXResult<CATran, CashAccount, PX.Objects.CM.Currency, PX.Objects.CM.CurrencyInfo, CADepositDetail, CADepositEntry.ARPaymentUpdate, CADepositEntry.APPaymentUpdate, CADepositEntry.CAAdjUpdate, CCBatch>.op_Implicit(pxResult);
      PXResult<CATran, CashAccount, PX.Objects.CM.Currency, PX.Objects.CM.CurrencyInfo, CADepositDetail, CADepositEntry.ARPaymentUpdate, CADepositEntry.APPaymentUpdate, CADepositEntry.CAAdjUpdate, CCBatch>.op_Implicit(pxResult);
      CADepositDetail detail = PXResult<CATran, CashAccount, PX.Objects.CM.Currency, PX.Objects.CM.CurrencyInfo, CADepositDetail, CADepositEntry.ARPaymentUpdate, CADepositEntry.APPaymentUpdate, CADepositEntry.CAAdjUpdate, CCBatch>.op_Implicit(pxResult);
      CADepositEntry.ARPaymentUpdate arPaymentUpdate = PXResult<CATran, CashAccount, PX.Objects.CM.Currency, PX.Objects.CM.CurrencyInfo, CADepositDetail, CADepositEntry.ARPaymentUpdate, CADepositEntry.APPaymentUpdate, CADepositEntry.CAAdjUpdate, CCBatch>.op_Implicit(pxResult);
      CADepositEntry.APPaymentUpdate apPaymentUpdate = PXResult<CATran, CashAccount, PX.Objects.CM.Currency, PX.Objects.CM.CurrencyInfo, CADepositDetail, CADepositEntry.ARPaymentUpdate, CADepositEntry.APPaymentUpdate, CADepositEntry.CAAdjUpdate, CCBatch>.op_Implicit(pxResult);
      CADepositEntry.CAAdjUpdate caAdjUpdate = PXResult<CATran, CashAccount, PX.Objects.CM.Currency, PX.Objects.CM.CurrencyInfo, CADepositDetail, CADepositEntry.ARPaymentUpdate, CADepositEntry.APPaymentUpdate, CADepositEntry.CAAdjUpdate, CCBatch>.op_Implicit(pxResult);
      CCBatch ccBatch = PXResult<CATran, CashAccount, PX.Objects.CM.Currency, PX.Objects.CM.CurrencyInfo, CADepositDetail, CADepositEntry.ARPaymentUpdate, CADepositEntry.APPaymentUpdate, CADepositEntry.CAAdjUpdate, CCBatch>.op_Implicit(pxResult);
      if (objB.CuryID != doc.CuryID)
        throw new PXException("A deposit of multiple currencies is not supported yet.");
      this.CheckIfUnreleasedVoidedEntryExist(detail);
      if (!string.IsNullOrEmpty(detail.OrigRefNbr) && detail.OrigModule == "AR" && detail.DetailType == "VCD" && this.IsVoidedEntryWithoutPair(detail))
        throw new PXException("The voided deposit cannot be released due to an invalid status of at least one included payment.");
      this.VerifyCashAccountInGL(cashacct);
      batch = ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current;
      int? nullable1;
      if (!object.Equals((object) objA, (object) objB))
      {
        PX.Objects.GL.GLTran glTran1 = new PX.Objects.GL.GLTran();
        glTran1.SummPost = new bool?(false);
        glTran1.CuryInfoID = batch.CuryInfoID;
        glTran1.TranType = objB.OrigTranType;
        glTran1.RefNbr = objB.OrigRefNbr;
        PX.Objects.GL.GLTran glTran2 = glTran1;
        int? nullable2;
        if (detail == null)
        {
          nullable1 = new int?();
          nullable2 = nullable1;
        }
        else
          nullable2 = detail.LineNbr;
        glTran2.TranLineNbr = nullable2;
        glTran1.ReferenceID = objB.ReferenceID;
        glTran1.AccountID = cashacct.AccountID;
        glTran1.SubID = cashacct.SubID;
        glTran1.BranchID = cashacct.BranchID;
        glTran1.CATranID = objB.TranID;
        glTran1.TranDate = objB.TranDate;
        FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran1, objB.TranPeriodID);
        glTran1.CuryDebitAmt = objB.DrCr == "D" ? objB.CuryTranAmt : new Decimal?(0M);
        glTran1.DebitAmt = objB.DrCr == "D" ? objB.TranAmt : new Decimal?(0M);
        PX.Objects.GL.GLTran glTran3 = glTran1;
        Decimal? nullable3;
        Decimal? nullable4;
        if (!(objB.DrCr == "D"))
        {
          Decimal num = -1M;
          nullable3 = objB.CuryTranAmt;
          nullable4 = nullable3.HasValue ? new Decimal?(num * nullable3.GetValueOrDefault()) : new Decimal?();
        }
        else
          nullable4 = new Decimal?(0M);
        glTran3.CuryCreditAmt = nullable4;
        PX.Objects.GL.GLTran glTran4 = glTran1;
        Decimal? nullable5;
        if (!(objB.DrCr == "D"))
        {
          Decimal num = -1M;
          nullable3 = objB.TranAmt;
          nullable5 = nullable3.HasValue ? new Decimal?(num * nullable3.GetValueOrDefault()) : new Decimal?();
        }
        else
          nullable5 = new Decimal?(0M);
        glTran4.CreditAmt = nullable5;
        glTran1.TranDesc = objB.TranDesc;
        glTran1.Released = new bool?(true);
        if (objB.OrigTranType == "CDT" || objB.OrigTranType == "CVD")
          glTran1.ZeroPost = new bool?(true);
        this.InsertGLTransactionForDeposit(je, glTran1, new CAReleaseProcess.GLTranInsertionContext()
        {
          CADepositRecord = doc,
          CADepositDetailRecord = detail,
          CATranRecord = objB
        });
        if (!string.IsNullOrEmpty(arPaymentUpdate.RefNbr))
        {
          if (doc.TranType == "CDT")
          {
            arPaymentUpdate.Deposited = new bool?(true);
          }
          else
          {
            arPaymentUpdate.Deposited = new bool?(false);
            arPaymentUpdate.DepositType = (string) null;
            arPaymentUpdate.DepositNbr = (string) null;
          }
          ((PXGraph) this).Caches[typeof (CADepositEntry.ARPaymentUpdate)].Update((object) arPaymentUpdate);
        }
        if (!string.IsNullOrEmpty(apPaymentUpdate.RefNbr))
        {
          if (doc.TranType == "CDT")
          {
            apPaymentUpdate.Deposited = new bool?(true);
          }
          else
          {
            apPaymentUpdate.Deposited = new bool?(false);
            apPaymentUpdate.DepositType = (string) null;
            apPaymentUpdate.DepositNbr = (string) null;
          }
          ((PXGraph) this).Caches[typeof (CADepositEntry.APPaymentUpdate)].Update((object) apPaymentUpdate);
        }
        if (!string.IsNullOrEmpty(caAdjUpdate.AdjRefNbr))
        {
          if (doc.TranType == "CDT")
          {
            caAdjUpdate.Deposited = new bool?(true);
          }
          else
          {
            caAdjUpdate.Deposited = new bool?(false);
            caAdjUpdate.DepositType = (string) null;
            caAdjUpdate.DepositNbr = (string) null;
          }
          ((PXGraph) this).Caches[typeof (CADepositEntry.CAAdjUpdate)].Update((object) caAdjUpdate);
        }
        if (!string.IsNullOrEmpty(detail.OrigRefNbr))
        {
          nullable3 = detail.OrigAmtSigned;
          Decimal num1 = nullable3.Value;
          nullable3 = detail.TranAmt;
          Decimal num2 = nullable3.Value;
          Decimal num3 = Math.Round(num1 - num2, 3);
          if (num3 != 0M)
          {
            Dictionary<int, PX.Objects.GL.GLTran> dictionary2 = dictionary1;
            nullable1 = detail.CashAccountID;
            int key1 = nullable1.Value;
            PX.Objects.GL.GLTran row;
            if (!dictionary2.ContainsKey(key1))
            {
              row = new PX.Objects.GL.GLTran();
              row.DebitAmt = new Decimal?(0M);
              row.CreditAmt = new Decimal?(0M);
              row.AccountID = cashacct.AccountID;
              row.SubID = cashacct.SubID;
              row.BranchID = cashacct.BranchID;
              row.TranDate = objB.TranDate;
              FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) row, objB.TranPeriodID);
              row.TranType = "CTG";
              row.RefNbr = doc.RefNbr;
              row.TranDesc = "RGOL";
              row.Released = new bool?(true);
              row.CuryInfoID = batch.CuryInfoID;
              Dictionary<int, PX.Objects.GL.GLTran> dictionary3 = dictionary1;
              nullable1 = detail.CashAccountID;
              int key2 = nullable1.Value;
              PX.Objects.GL.GLTran glTran5 = row;
              dictionary3[key2] = glTran5;
            }
            else
            {
              Dictionary<int, PX.Objects.GL.GLTran> dictionary4 = dictionary1;
              nullable1 = detail.CashAccountID;
              int key3 = nullable1.Value;
              row = dictionary4[key3];
            }
            PX.Objects.GL.GLTran glTran6 = row;
            nullable3 = glTran6.DebitAmt;
            Decimal num4 = objB.DrCr == "C" == num3 > 0M ? 0M : Math.Abs(num3);
            glTran6.DebitAmt = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + num4) : new Decimal?();
            PX.Objects.GL.GLTran glTran7 = row;
            nullable3 = glTran7.CreditAmt;
            Decimal num5 = objB.DrCr == "C" == num3 > 0M ? Math.Abs(num3) : 0M;
            glTran7.CreditAmt = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + num5) : new Decimal?();
            currency = cury;
          }
        }
        nullable1 = ccBatch.BatchID;
        if (nullable1.HasValue)
        {
          ccBatch.Status = "DPD";
          ((PXGraph) this).Caches[typeof (CCBatch)].Update((object) ccBatch);
        }
      }
      objA = objB;
      long? tranId = objB.TranID;
      if (tranId.HasValue)
      {
        HashSet<long> longSet = caTranIDs;
        tranId = objB.TranID;
        long num6 = tranId.Value;
        longSet.Add(num6);
        nullable1 = cashacct.AccountID;
        if (nullable1.HasValue)
        {
          HashSet<int> intSet = accountIDs;
          nullable1 = cashacct.AccountID;
          int num7 = nullable1.Value;
          intSet.Add(num7);
        }
      }
    }
    if (batch != null)
    {
      foreach (PXResult<CADepositCharge> pxResult in PXSelectBase<CADepositCharge, PXSelect<CADepositCharge, Where<CADepositCharge.tranType, Equal<Required<CADepositCharge.tranType>>, And<CADepositCharge.refNbr, Equal<Required<CADepositCharge.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) doc.TranType,
        (object) doc.RefNbr
      }))
      {
        CADepositCharge caDepositCharge = PXResult<CADepositCharge>.op_Implicit(pxResult);
        if (caDepositCharge != null)
        {
          Decimal? curyChargeAmt = caDepositCharge.CuryChargeAmt;
          Decimal num = 0M;
          if (!(curyChargeAmt.GetValueOrDefault() == num & curyChargeAmt.HasValue))
          {
            PX.Objects.GL.GLTran glTran = new PX.Objects.GL.GLTran();
            glTran.SummPost = new bool?(false);
            glTran.CuryInfoID = batch.CuryInfoID;
            glTran.TranType = caDepositCharge.TranType;
            glTran.RefNbr = caDepositCharge.RefNbr;
            glTran.AccountID = caDepositCharge.AccountID;
            glTran.SubID = caDepositCharge.SubID;
            glTran.TranDate = doc.TranDate;
            glTran.BranchID = doc.BranchID;
            FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.GL.GLTran.finPeriodID>(((PXSelectBase) je.GLTranModuleBatNbr).Cache, (object) glTran, doc.TranPeriodID);
            glTran.CuryDebitAmt = caDepositCharge.DrCr == "D" ? new Decimal?(0M) : caDepositCharge.CuryChargeAmt;
            glTran.DebitAmt = caDepositCharge.DrCr == "D" ? new Decimal?(0M) : caDepositCharge.ChargeAmt;
            glTran.CuryCreditAmt = caDepositCharge.DrCr == "D" ? caDepositCharge.CuryChargeAmt : new Decimal?(0M);
            glTran.CreditAmt = caDepositCharge.DrCr == "D" ? caDepositCharge.ChargeAmt : new Decimal?(0M);
            glTran.Released = new bool?(true);
            this.InsertDepositChargeTransaction(je, glTran, new CAReleaseProcess.GLTranInsertionContext()
            {
              CADepositChargeRecord = caDepositCharge
            });
          }
        }
      }
      foreach (KeyValuePair<int, PX.Objects.GL.GLTran> keyValuePair in dictionary1)
      {
        PX.Objects.GL.GLTran tran = keyValuePair.Value;
        Decimal? debitAmt = tran.DebitAmt;
        Decimal? creditAmt = tran.CreditAmt;
        Decimal? nullable6;
        Decimal? nullable7;
        if (!(debitAmt.HasValue & creditAmt.HasValue))
        {
          nullable6 = new Decimal?();
          nullable7 = nullable6;
        }
        else
          nullable7 = new Decimal?(debitAmt.GetValueOrDefault() - creditAmt.GetValueOrDefault());
        nullable6 = nullable7;
        Decimal num8 = nullable6.Value;
        int num9 = Math.Sign(num8);
        Decimal num10 = Math.Abs(num8);
        if (num10 != 0M)
        {
          PX.Objects.GL.GLTran copy = (PX.Objects.GL.GLTran) ((PXGraph) je).Caches[typeof (PX.Objects.GL.GLTran)].CreateCopy((object) tran);
          copy.CuryDebitAmt = new Decimal?(0M);
          copy.CuryCreditAmt = new Decimal?(0M);
          if (doc.DocType == "CDT")
          {
            copy.AccountID = num9 < 0 ? currency.RealLossAcctID : currency.RealGainAcctID;
            copy.SubID = num9 < 0 ? GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Currency.realLossSubID>((PXGraph) je, tran.BranchID, currency) : GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Currency.realGainSubID>((PXGraph) je, tran.BranchID, currency);
          }
          else
          {
            copy.AccountID = num9 < 0 ? currency.RealGainAcctID : currency.RealLossAcctID;
            copy.SubID = num9 < 0 ? GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Currency.realGainSubID>((PXGraph) je, tran.BranchID, currency) : GainLossSubAccountMaskAttribute.GetSubID<PX.Objects.CM.Currency.realLossSubID>((PXGraph) je, tran.BranchID, currency);
          }
          copy.DebitAmt = new Decimal?(num9 < 0 ? num10 : 0M);
          copy.CreditAmt = new Decimal?(num9 < 0 ? 0M : num10);
          copy.TranType = "CTG";
          copy.RefNbr = doc.RefNbr;
          copy.TranDesc = "RGOL";
          copy.TranDate = tran.TranDate;
          copy.FinPeriodID = tran.FinPeriodID;
          copy.TranPeriodID = tran.TranPeriodID;
          copy.Released = new bool?(true);
          copy.CuryInfoID = batch.CuryInfoID;
          this.InsertIncomingingRGOLGLTransactionForDeposit(je, copy, new CAReleaseProcess.GLTranInsertionContext()
          {
            CADepositRecord = doc
          });
          tran.CuryDebitAmt = new Decimal?(0M);
          tran.DebitAmt = new Decimal?(num9 > 0 ? num10 : 0M);
          tran.CreditAmt = new Decimal?(num9 > 0 ? 0M : num10);
          this.InsertOurgoingRGOLGLTransactionForDeposit(je, tran, new CAReleaseProcess.GLTranInsertionContext()
          {
            CADepositRecord = doc
          });
        }
      }
    }
    Decimal? nullable;
    if (batch != null)
    {
      Decimal? curyCreditTotal = batch.CuryCreditTotal;
      nullable = batch.CuryDebitTotal;
      if (curyCreditTotal.GetValueOrDefault() == nullable.GetValueOrDefault() & curyCreditTotal.HasValue == nullable.HasValue)
        this.AddRoundingTran(je, (ICADocument) doc, batch, cury, batch.CuryInfoID);
    }
    if (batch != null)
    {
      nullable = batch.CuryCreditTotal;
      Decimal? curyDebitTotal = batch.CuryDebitTotal;
      if (!(nullable.GetValueOrDefault() == curyDebitTotal.GetValueOrDefault() & nullable.HasValue == curyDebitTotal.HasValue))
        throw new PXException("The batch is not balanced. Review the debit and credit amounts.");
    }
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (batch != null)
      {
        ((PXAction) je.Save).Press();
        if (!batchlist.Contains(batch))
          batchlist.Add(batch);
        doc.Released = new bool?(true);
        SelectedEntityEvent<CADeposit> selectedEntityEvent = (SelectedEntityEvent<CADeposit>) PXEntityEventBase<CADeposit>.Container<CADeposit.Events>.Select((Expression<Func<CADeposit.Events, PXEntityEvent<CADeposit.Events>>>) (ev => ev.ReleaseDocument));
        selectedEntityEvent.FireOn((PXGraph) this, doc);
        if (doc.TranType == "CVD")
        {
          CADeposit caDeposit = PXResultset<CADeposit>.op_Implicit(PXSelectBase<CADeposit, PXSelect<CADeposit, Where<CADeposit.tranType, Equal<CATranType.cADeposit>, And<CADeposit.refNbr, Equal<Required<CADeposit.refNbr>>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) doc.RefNbr
          }));
          caDeposit.Voided = new bool?(true);
          selectedEntityEvent = (SelectedEntityEvent<CADeposit>) PXEntityEventBase<CADeposit>.Container<CADeposit.Events>.Select((Expression<Func<CADeposit.Events, PXEntityEvent<CADeposit.Events>>>) (ev => ev.VoidDocument));
          selectedEntityEvent.FireOn((PXGraph) this, caDeposit);
          CCBatch ccBatch = PXResultset<CCBatch>.op_Implicit(PXSelectBase<CCBatch, PXSelect<CCBatch, Where<CCBatch.depositType, Equal<Required<CCBatch.depositType>>, And<CCBatch.depositNbr, Equal<Required<CCBatch.depositNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) caDeposit.TranType,
            (object) caDeposit.RefNbr
          }));
          if (ccBatch != null && ccBatch.BatchID.HasValue)
          {
            ccBatch.Status = "PRD";
            ((PXGraph) this).Caches[typeof (CCBatch)].Update((object) ccBatch);
          }
        }
      }
      ((PXGraph) this).Actions.PressSave();
      this.CheckBoundGLAccountIsCashAccount(accountIDs);
      this.CheckMultipleGLPosting(caTranIDs);
      transactionScope.Complete();
    }
  }

  private static PX.Objects.GL.Batch CreateGLBatch(JournalEntry je, CADeposit doc)
  {
    PX.Objects.CM.CurrencyInfo currencyInfo1 = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelectReadonly<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) je, new object[1]
    {
      (object) doc.CuryInfoID
    }));
    PX.Objects.CM.CurrencyInfo copy = (PX.Objects.CM.CurrencyInfo) ((PXSelectBase) je.currencyinfo).Cache.CreateCopy((object) currencyInfo1);
    copy.CuryInfoID = new long?();
    PX.Objects.CM.CurrencyInfo currencyInfo2 = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) je.currencyinfo).Insert(copy);
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) je, new object[1]
    {
      (object) doc.CashAccountID
    }));
    PX.Objects.GL.Batch glBatch = ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Insert(new PX.Objects.GL.Batch()
    {
      Module = "CA",
      Status = "U",
      Released = new bool?(true),
      Hold = new bool?(false),
      DateEntered = doc.TranDate,
      FinPeriodID = doc.FinPeriodID,
      TranPeriodID = doc.TranPeriodID,
      CuryID = doc.CuryID,
      CuryInfoID = currencyInfo2.CuryInfoID,
      DebitTotal = new Decimal?(0M),
      CreditTotal = new Decimal?(0M),
      BranchID = cashAccount.BranchID,
      Description = doc.TranDesc
    });
    PX.Objects.CM.CurrencyInfo currencyInfo3 = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) je.currencyinfo).Select(Array.Empty<object>()));
    if (currencyInfo3 != null)
    {
      currencyInfo3.CuryID = currencyInfo1.CuryID;
      ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) je.currencyinfo).SetValueExt<PX.Objects.CM.CurrencyInfo.curyEffDate>(currencyInfo3, (object) currencyInfo1.CuryEffDate);
      currencyInfo3.SampleCuryRate = currencyInfo1.SampleCuryRate ?? currencyInfo3.SampleCuryRate;
      currencyInfo3.CuryRateTypeID = currencyInfo1.CuryRateTypeID ?? currencyInfo3.CuryRateTypeID;
      ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) je.currencyinfo).Update(currencyInfo3);
    }
    ((PXSelectBase<PX.Objects.GL.Batch>) je.BatchModule).Current = glBatch;
    return glBatch;
  }

  public virtual CAAdj CommitExternalTax(CAAdj doc) => doc;

  protected void CheckBoundGLAccountIsCashAccount(HashSet<int> accountIDs)
  {
    foreach (PXResult<PX.Objects.GL.Account> pxResult in PXSelectBase<PX.Objects.GL.Account, PXSelectReadonly<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, In<Required<PX.Objects.GL.Account.accountID>>, And<PX.Objects.GL.Account.isCashAccount, Equal<False>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) accountIDs.ToArray<int>()
    }))
      PXTrace.WriteError($"This cash account is mapped to the {PXResult<PX.Objects.GL.Account>.op_Implicit(pxResult).AccountCD} GL account for which the Cash Account check box is cleared on the Chart of Accounts (GL202500) form.");
  }

  protected void CheckMultipleGLPosting(HashSet<long> caTranIDs)
  {
    foreach (long caTranId in caTranIDs)
    {
      int count = PXSelectBase<CATran, PXSelectReadonly2<CATran, InnerJoin<PX.Objects.GL.GLTran, On<PX.Objects.GL.GLTran.cATranID, Equal<CATran.tranID>>>, Where<CATran.released, Equal<True>, And<PX.Objects.GL.GLTran.released, Equal<True>, And<CATran.tranID, Equal<Required<CATran.tranID>>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) caTranId
      }).Count;
      this.ValidateIfMappedGLAccountIsCashAccount(caTranId, count);
      if (count != 1)
      {
        PXTrace.WriteError($"Error message: {"Cannot release documents. Please contact support."}; Date: {DateTime.Now}; Screen: {((PXGraph) this).Accessinfo.ScreenID}; Count: {count}; CATranID: {caTranId};");
        if (((PXSelectBase<CASetup>) this.casetup).Current.ValidateDataConsistencyOnRelease.GetValueOrDefault())
          throw new PXException("Cannot release documents. Please contact support.");
      }
    }
  }

  private void ValidateIfMappedGLAccountIsCashAccount(long id, int count)
  {
    if (count != 0)
      return;
    using (new PXReadBranchRestrictedScope())
    {
      PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelectReadonly2<PX.Objects.GL.Account, InnerJoin<CashAccount, On<PX.Objects.GL.Account.accountCD, Equal<CashAccount.accountID>>, InnerJoin<CATran, On<CashAccount.cashAccountCD, Equal<CATran.cashAccountID>>>>, Where<CATran.tranID, Equal<Required<CATran.tranID>>, And<PX.Objects.GL.Account.isCashAccount, Equal<boolFalse>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) id
      }));
      if ((bool?) account?.IsCashAccount ?? true)
        return;
      PXTrace.WriteError((Exception) new PXException("This cash account is mapped to the {0} GL account for which the Cash Account check box is cleared on the Chart of Accounts (GL202500) form.", new object[1]
      {
        (object) account.AccountCD
      }));
    }
  }

  protected virtual IComparer<PX.Objects.TX.Tax> GetTaxComparer()
  {
    return (IComparer<PX.Objects.TX.Tax>) TaxByCalculationLevelAndTypeComparer.Instance;
  }

  /// <summary>
  /// The method to insert document GL transactions
  /// for the <see cref="T:PX.Objects.CA.CAAdj" /> or <see cref="T:PX.Objects.CA.CATransfer" /> entity.
  /// <see cref="T:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext.CATranRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertDocumentTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    CAReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert document detail GL transactions
  /// for the <see cref="T:PX.Objects.CA.CAAdj" /> or <see cref="T:PX.Objects.CA.CATransfer" /> entity.
  /// <see cref="T:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext.CASplitRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertSplitTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    CAReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert document rounding GL transactions
  /// for the <see cref="T:PX.Objects.CA.CAAdj" /> or <see cref="T:PX.Objects.CA.CATransfer" /> entity.
  /// <see cref="T:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext.CATranRecord" />.
  /// <see cref="P:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext.CAAdjRecord" />.
  /// <see cref="P:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext.CATransferRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertRoundingTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    CAReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert deposit charges GL transactions
  /// for the <see cref="T:PX.Objects.CA.CADeposit" /> entity.
  /// <see cref="T:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext.CADepositChargeRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertDepositChargeTransaction(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    CAReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert incoming real gain/loss GL transactions
  /// for the <see cref="T:PX.Objects.CA.CATransfer" /> entity.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertIncomingRGOLTransaction(JournalEntry je, PX.Objects.GL.GLTran tran)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert outgoing real gain/loss GL transactions
  /// for the <see cref="T:PX.Objects.CA.CATransfer" /> entity.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertOutgoingRGOLTransaction(JournalEntry je, PX.Objects.GL.GLTran tran)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert deductible GL tax transactions
  /// for the <see cref="T:PX.Objects.CA.CASplit" /> entity.
  /// <see cref="T:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext.CATranRecord" />.
  /// <see cref="P:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext.CAAdjRecord" />.
  /// <see cref="P:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext.CASplitRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertDeductibleTaxTran(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    CAReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert tax expense GL transactions
  /// for the <see cref="T:PX.Objects.CA.CATransfer" /> entity.
  /// <see cref="T:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext.CATranRecord" />.
  /// <see cref="P:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext.CAAdjRecord" />.
  /// <see cref="P:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext.CATaxTranRecord" />.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertTaxExpenseTran(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    CAReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert deposit GL transactions
  /// for the <see cref="T:PX.Objects.CA.CADeposit" /> entity.
  /// <see cref="T:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext" /> class content:
  /// <see cref="P:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext.CADepositRecord" />.
  /// <see cref="P:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext.CADepositDetailRecord" />.
  /// <see cref="P:PX.Objects.CA.CAReleaseProcess.GLTranInsertionContext.CATranRecord" />.
  /// </summary>
  protected virtual void InsertGLTransactionForDeposit(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    CAReleaseProcess.GLTranInsertionContext context)
  {
    ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert outgoing real gain/loss GL transactions
  /// for the <see cref="T:PX.Objects.CA.CADeposit" /> entity.
  /// </summary>
  protected virtual void InsertOurgoingRGOLGLTransactionForDeposit(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    CAReleaseProcess.GLTranInsertionContext context)
  {
    ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  /// <summary>
  /// The method to insert incoming real gain/loss GL transactions
  /// for the <see cref="T:PX.Objects.CA.CADeposit" /> entity.
  /// </summary>
  public virtual PX.Objects.GL.GLTran InsertIncomingingRGOLGLTransactionForDeposit(
    JournalEntry je,
    PX.Objects.GL.GLTran tran,
    CAReleaseProcess.GLTranInsertionContext context)
  {
    return ((PXSelectBase<PX.Objects.GL.GLTran>) je.GLTranModuleBatNbr).Insert(tran);
  }

  public class GLTranInsertionContext
  {
    public virtual CAAdj CAAdjRecord { get; set; }

    public virtual CASplit CASplitRecord { get; set; }

    public virtual CATaxTran CATaxTranRecord { get; set; }

    public virtual CATransfer CATransferRecord { get; set; }

    public virtual CAExpense CAExpenseRecord { get; set; }

    public virtual CADeposit CADepositRecord { get; set; }

    public virtual CADepositDetail CADepositDetailRecord { get; set; }

    public virtual CADepositCharge CADepositChargeRecord { get; set; }

    public virtual CATran CATranRecord { get; set; }
  }
}
