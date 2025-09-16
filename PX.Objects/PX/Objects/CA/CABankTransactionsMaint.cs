// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTransactionsMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.AP.MigrationMode;
using PX.Objects.AR;
using PX.Objects.AR.MigrationMode;
using PX.Objects.CA.BankStatementHelpers;
using PX.Objects.CA.BankStatementProtoHelpers;
using PX.Objects.CA.Descriptor;
using PX.Objects.CA.MultiCurrency;
using PX.Objects.CA.Repositories;
using PX.Objects.CA.Utility;
using PX.Objects.CN.Common.Extensions;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.TX;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.CA;

public class CABankTransactionsMaint : 
  PXGraph<
  #nullable disable
  CABankTransactionsMaint>,
  ICABankTransactionsDataProvider,
  IAddARTransaction,
  IAddAPTransaction
{
  public PXFilter<CABankTransactionsMaint.Filter> TranFilter;
  public PXSelect<PX.Objects.AR.Standalone.ARRegister> dummyARRegister;
  public PXSelect<PX.Objects.AP.Standalone.APRegister> dummyAPRegister;
  [PXFilterable(new System.Type[] {})]
  public PXSelect<CABankTran, Where<CABankTran.processed, Equal<False>, And<CABankTran.cashAccountID, Equal<Current<CABankTransactionsMaint.Filter.cashAccountID>>, And<CABankTran.tranType, Equal<Current<CABankTransactionsMaint.Filter.tranType>>>>>, OrderBy<Asc<CABankTran.tranID>>> Details;
  public PXSelect<CABankTran, Where<CABankTran.tranID, Equal<Current<CABankTran.tranID>>>> CurrentCABankTran;
  public FbqlSelect<SelectFromBase<CABankTax, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  CABankTax.bankTranID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  CABankTran.tranID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  CABankTax>.View Taxes;
  public FbqlSelect<SelectFromBase<CABankTaxTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.TX.Tax>.On<BqlOperand<
  #nullable enable
  PX.Objects.TX.Tax.taxID, IBqlString>.IsEqual<
  #nullable disable
  CABankTaxTran.taxID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CABankTaxTran.bankTranID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CABankTran.tranID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  CABankTaxTran.bankTranType, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  CABankTran.tranType, IBqlString>.FromCurrent>>>, 
  #nullable disable
  CABankTaxTran>.View TaxTrans;
  public FbqlSelect<SelectFromBase<PX.Objects.TX.TaxZone, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PX.Objects.TX.TaxZone.taxZoneID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  CABankTran.taxZoneID, IBqlString>.FromCurrent>>, 
  #nullable disable
  PX.Objects.TX.TaxZone>.View Taxzone;
  public PXSelect<CABankTran, Where<CABankTran.processed, Equal<False>, And<CABankTran.cashAccountID, Equal<Current<CABankTransactionsMaint.Filter.cashAccountID>>, And<CABankTran.tranType, Equal<Current<CABankTransactionsMaint.Filter.tranType>>, And<CABankTran.documentMatched, Equal<False>>>>>> UnMatchedDetails;
  public PXSelect<CABankTran, Where<CABankTran.tranID, Equal<Current<CABankTran.tranID>>>> DetailsForPaymentCreation;
  public PXSelect<CABankTran, Where<CABankTran.tranID, Equal<Current<CABankTran.tranID>>>> DetailsForInvoiceApplication;
  public PXSelect<CABankTran, Where<CABankTran.tranID, Equal<Current<CABankTran.tranID>>>> DetailsForPaymentMatching;
  public PXSetup<PX.Objects.CA.CASetup> CASetup;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;
  public PXSetup<ARSetup> arsetup;
  public PXSelect<CATran, Where<CATran.tranID, IsNull>> caTran;
  public PXSelect<PX.Objects.AP.APPayment, Where<PX.Objects.AP.APPayment.docType, IsNull>> apPayment;
  public PXSelect<PX.Objects.AR.ARPayment, Where<PX.Objects.AR.ARPayment.docType, IsNull>> arPayment;
  public PXSelect<CADeposit, Where<CADeposit.tranType, IsNull>> caDeposit;
  public PXSelect<CAAdj, Where<CAAdj.adjRefNbr, IsNull>> caAdjustment;
  public PXSelect<CATransfer, Where<CATransfer.transferNbr, IsNull>> caTransfer;
  public PXSelectJoin<PX.Objects.CA.BankStatementHelpers.CATranExt, LeftJoin<PX.Objects.CA.Light.BAccount, On<PX.Objects.CA.Light.BAccount.bAccountID, Equal<PX.Objects.CA.BankStatementHelpers.CATranExt.referenceID>>>, Where<PX.Objects.CA.BankStatementHelpers.CATranExt.matchRelevance, IsNotNull>, OrderBy<Desc<PX.Objects.CA.BankStatementHelpers.CATranExt.matchRelevance>>> DetailMatchesCA;
  public PXSelect<CABankTranMatch, Where<CABankTranMatch.matchType, Equal<CABankTranMatch.matchType.match>, And<CABankTranMatch.tranID, Equal<Required<CABankTran.tranID>>>>> TranMatch;
  public PXSelect<CABankTranMatch, Where<CABankTranMatch.tranID, Equal<Required<CABankTran.tranID>>, And<CABankTranMatch.tranType, Equal<Required<CABankTranMatch.tranType>>, And<CABankTranMatch.docModule, Equal<Required<CABankTranMatch.docModule>>, And<CABankTranMatch.docType, Equal<Required<CABankTranMatch.docType>>, And<CABankTranMatch.docRefNbr, Equal<Required<CABankTranMatch.docRefNbr>>>>>>>> TranMatchInvoices;
  public PXSelect<CABankTranMatch, Where<CABankTranMatch.matchType, Equal<CABankTranMatch.matchType.charge>, And<CABankTranMatch.tranID, Equal<Required<CABankTran.tranID>>>>> TranMatchCharge;
  public PXSelectJoin<CABankTranAdjustment, LeftJoin<PX.Objects.AR.ARInvoice, On<CABankTranAdjustment.adjdModule, Equal<BatchModule.moduleAR>, And<CABankTranAdjustment.adjdRefNbr, Equal<PX.Objects.AR.ARInvoice.refNbr>, And<CABankTranAdjustment.adjdDocType, Equal<PX.Objects.AR.ARInvoice.docType>>>>>, Where<CABankTranAdjustment.tranID, Equal<Optional<CABankTran.tranID>>>> Adjustments;
  public PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Current<CABankTransactionsMaint.Filter.cashAccountID>>>> cashAccount;
  public PXSelect<GeneralInvoice> invoices;
  public FbqlSelect<SelectFromBase<CABankChargeTax, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  CABankChargeTax.bankTranID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  CABankTran.tranID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  CABankChargeTax>.View ChargeTaxes;
  public FbqlSelect<SelectFromBase<CABankTaxTranMatch, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.TX.Tax>.On<BqlOperand<
  #nullable enable
  PX.Objects.TX.Tax.taxID, IBqlString>.IsEqual<
  #nullable disable
  CABankTaxTranMatch.taxID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CABankTaxTranMatch.bankTranID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  CABankTran.tranID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  CABankTaxTranMatch.bankTranType, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  CABankTran.tranType, IBqlString>.FromCurrent>>>, 
  #nullable disable
  CABankTaxTranMatch>.View ChargeTaxTrans;
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>> CurrencyInfo_CuryInfoID;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Optional<CABankTran.curyInfoID>>>> currencyinfo;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Optional<CABankTranAdjustment.adjgCuryInfoID>>>> currencyinfo_adjustment;
  public PXSelectReadonly<CashAccount, Where<CashAccount.extRefNbr, Equal<Optional<CashAccount.extRefNbr>>>> cashAccountByExtRef;
  public PXSelect<CABankTranInvoiceMatch, Where<True, Equal<False>>, OrderBy<Desc<CABankTranDocumentMatch.matchRelevance>>> DetailMatchingInvoices;
  public PXSelect<CABankTranExpenseDetailMatch, Where<True, Equal<True>>, OrderBy<Desc<CABankTranDocumentMatch.matchRelevance, Asc<CABankTranExpenseDetailMatch.curyDocAmtDiff>>>> ExpenseReceiptDetailMatches;
  [PXCopyPasteHiddenView]
  public PXSelect<CAExpense> cAExpense;
  [PXHidden]
  public PXSelect<CABankTranRule, Where<CABankTranRule.isActive, Equal<True>>> Rules;
  public PXSelect<CABankTranDetail, Where<CABankTranDetail.bankTranID, Equal<Optional<CABankTran.tranID>>, And<CABankTranDetail.bankTranType, Equal<Optional<CABankTran.tranType>>>>> TranSplit;
  public PXSelect<BAccountR> BaccountCache;
  public PXSelect<PX.Objects.CA.Light.BAccount> LightBAccountCache;
  public PXSelect<EPExpenseClaimDetails> ExpenseReceipts;
  protected Dictionary<object, List<CABankTranInvoiceMatch>> matchingInvoices;
  protected Dictionary<object, List<PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>>> matchingTrans;
  protected Dictionary<object, List<CABankTranExpenseDetailMatch>> matchingExpenseReceiptDetails;
  public PXSave<CABankTransactionsMaint.Filter> Save;
  public PXAction<CABankTransactionsMaint.Filter> cancel;
  public PXFilter<CABankTransactionsMaint.MatchingLoadOptions> loadOpts;
  public PXAction<CABankTransactionsMaint.Filter> loadInvoices;
  public PXAction<CABankTransactionsMaint.Filter> autoMatch;
  public PXAction<CABankTransactionsMaint.Filter> processMatched;
  public PXAction<CABankTransactionsMaint.Filter> matchSettingsPanel;
  public PXSelect<CABankTransactionsMaint.Filter> NewRevisionPanel;
  public PXAction<CABankTransactionsMaint.Filter> uploadFile;
  public PXAction<CABankTransactionsMaint.Filter> clearMatch;
  public PXAction<CABankTransactionsMaint.Filter> clearAllMatches;
  public PXAction<CABankTransactionsMaint.Filter> hide;
  public PXFilter<CABankTransactionsMaint.CreateRuleSettings> RuleCreation;
  public PXAction<CABankTransactionsMaint.Filter> createRule;
  public PXAction<CABankTransactionsMaint.Filter> unapplyRule;
  public PXAction<CABankTransactionsMaint.Filter> viewPayment;
  public PXAction<CABankTransactionsMaint.Filter> viewBAccount;
  public PXAction<CABankTransactionsMaint.Filter> viewBAccountInvoices;
  public PXAction<CABankTransactionsMaint.Filter> viewInvoice;
  public PXAction<CABankTransactionsMaint.Filter> refreshAfterRuleCreate;
  public PXAction<CABankTransactionsMaint.Filter> viewDocumentToApply;
  public PXAction<CABankTransactionsMaint.Filter> ViewExpenseReceipt;
  public PXAction<CABankTransactionsMaint.Filter> ResetMatchSettingsToDefault;
  private Func<PX.Objects.AR.Customer, CABankTranAdjustment, bool> canBeWrittenOff = (Func<PX.Objects.AR.Customer, CABankTranAdjustment, bool>) ((customer, adj) =>
  {
    if (customer != null && adj.AdjdRefNbr != null && customer.SmallBalanceAllow.GetValueOrDefault())
    {
      Decimal? smallBalanceLimit = customer.SmallBalanceLimit;
      Decimal num = 0M;
      if (smallBalanceLimit.GetValueOrDefault() > num & smallBalanceLimit.HasValue && adj.AdjdDocType != "CRM")
        return adj.AdjdModule == "AR";
    }
    return false;
  });
  public PXAction<CABankTran> ViewTaxDetails;

  public CABankTransactionsMaint()
  {
    ((PXSelectBase) this.Details).Cache.AllowInsert = false;
    ((PXSelectBase) this.Details).Cache.AllowDelete = false;
    ((PXSelectBase) this.Details).Cache.AllowUpdate = false;
    ((PXSelectBase) this.DetailMatchesCA).Cache.AllowInsert = false;
    ((PXSelectBase) this.DetailMatchesCA).Cache.AllowDelete = false;
    ((PXSelectBase) this.Adjustments).Cache.AllowInsert = false;
    ((PXSelectBase) this.Adjustments).Cache.AllowDelete = false;
    ((PXSelectBase) this.DetailMatchingInvoices).Cache.AllowInsert = false;
    ((PXSelectBase) this.DetailMatchingInvoices).Cache.AllowDelete = false;
    ((PXSelectBase) this.Details).AllowUpdate = false;
    ((PXSelectBase) this.TranSplit).AllowInsert = false;
    APSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) this);
    ARSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) this);
    // ISSUE: method pointer
    ((PXAction) this.matchSettingsPanel).StateSelectingEvents += new PXFieldSelecting((object) this, __methodptr(StateSelectingEventsHandler));
    // ISSUE: method pointer
    ((PXAction) this.processMatched).StateSelectingEvents += new PXFieldSelecting((object) this, __methodptr(StateSelectingEventsHandler));
    // ISSUE: method pointer
    ((PXAction) this.uploadFile).StateSelectingEvents += new PXFieldSelecting((object) this, __methodptr(StateSelectingEventsHandler));
    // ISSUE: method pointer
    ((PXAction) this.autoMatch).StateSelectingEvents += new PXFieldSelecting((object) this, __methodptr(StateSelectingEventsHandler));
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(new PXFieldDefaulting((object) this, __methodptr(SetDefaultBaccountType)));
    PXUIFieldAttribute.SetVisible<CABankTranDetail.projectID>(((PXSelectBase) this.TranSplit).Cache, (object) null, false);
    this.EnableCreateTab(((PXSelectBase) this.Details).Cache, (CABankTran) null, false);
  }

  private void StateSelectingEventsHandler(PXCache sender, PXFieldSelectingEventArgs e)
  {
    TimeSpan timeSpan;
    Exception exception;
    if (PXLongOperation.GetStatus(((PXGraph) this).UID, ref timeSpan, ref exception) == null)
      return;
    PXButtonState instance = PXButtonState.CreateInstance(e.ReturnState, (string) null, (string) null, (string) null, (string) null, (string) null, new bool?(false), (PXConfirmationType) 2, (string) null, new char?(), new bool?(), new bool?(), (ButtonMenu[]) null, new bool?(), new bool?(), new bool?(), (string) null, (string) null, (PXShortCutAttribute) null, typeof (CABankTransactionsMaint.Filter));
    ((PXFieldState) instance).Enabled = false;
    e.ReturnState = (object) instance;
  }

  /// <summary>
  /// Sets default baccount type. Method is used as a workaround for the redirection problem with the edit button of the empty Business Account field.
  /// </summary>
  private void SetDefaultBaccountType(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    CABankTran current = ((PXSelectBase<CABankTran>) this.DetailsForPaymentCreation).Current;
    if (e.Row == null || current == null)
      return;
    if (current.OrigModule == "AP")
    {
      e.NewValue = (object) "VE";
    }
    else
    {
      if (!(current.OrigModule == "AR"))
        return;
      e.NewValue = (object) "CU";
    }
  }

  internal bool MatchInvoiceProcess { get; set; }

  [InjectDependency]
  public ICABankTransactionsRepository CABankTransactionsRepository { get; set; }

  [InjectDependency]
  public IMatchingService MatchingService { get; set; }

  public virtual IMatchSettings CurrentMatchSesstings
  {
    get
    {
      return (IMatchSettings) ((PXSelectBase<CashAccount>) this.cashAccount).Current ?? (IMatchSettings) PXResultset<CashAccount>.op_Implicit(((PXSelectBase<CashAccount>) this.cashAccount).Select(Array.Empty<object>()));
    }
  }

  protected virtual IEnumerable detailMatchingInvoices()
  {
    CABankTransactionsMaint graph = this;
    CABankTran current = ((PXSelectBase<CABankTran>) graph.Details).Current;
    if (current != null)
    {
      PXCache cache = ((PXSelectBase) graph.DetailMatchingInvoices).Cache;
      cache.Clear();
      current.CountInvoiceMatches = new int?(0);
      IEnumerable matchingInvoicesProc = (IEnumerable) CABankMatchingProcess.FindDetailMatchingInvoicesProc((PXGraph) graph, current, ((PXSelectBase<CashAccount>) graph.cashAccount).Current ?? PXResultset<CashAccount>.op_Implicit(((PXSelectBase<CashAccount>) graph.cashAccount).Select(Array.Empty<object>())), 0M, (CABankTranInvoiceMatch[]) null);
      if (NonGenericIEnumerableExtensions.Any_(matchingInvoicesProc))
      {
        List<CABankTranMatch> existingMatches = new List<CABankTranMatch>();
        foreach (PXResult<CABankTranMatch> pxResult in ((PXSelectBase<CABankTranMatch>) graph.TranMatch).Select(new object[1]
        {
          (object) current.TranID
        }))
        {
          CABankTranMatch caBankTranMatch = PXResult<CABankTranMatch>.op_Implicit(pxResult);
          if (caBankTranMatch.DocModule != null && caBankTranMatch.DocType != null && caBankTranMatch.DocRefNbr != null)
            existingMatches.Add(caBankTranMatch);
        }
        foreach (CABankTranInvoiceMatch tranInvoiceMatch in matchingInvoicesProc)
        {
          CABankTranInvoiceMatch invMatch = cache.Insert((object) tranInvoiceMatch) as CABankTranInvoiceMatch;
          if (invMatch != null)
          {
            bool flag = false;
            if (existingMatches.Any<CABankTranMatch>((Func<CABankTranMatch, bool>) (existingMatch => existingMatch.DocModule == invMatch.OrigModule && existingMatch.DocType == invMatch.OrigTranType && existingMatch.DocRefNbr == invMatch.OrigRefNbr)))
              flag = true;
            cache.SetValue<CABankTranInvoiceMatch.isMatched>((object) invMatch, (object) flag);
            yield return (object) invMatch;
          }
        }
        existingMatches = (List<CABankTranMatch>) null;
      }
      cache.IsDirty = false;
    }
  }

  protected virtual IEnumerable expenseReceiptDetailMatches()
  {
    CABankTransactionsMaint graph = this;
    CABankTran current = ((PXSelectBase<CABankTran>) graph.Details).Current;
    if (current != null)
    {
      PXCache matchesCache = ((PXSelectBase) graph.ExpenseReceiptDetailMatches).Cache;
      matchesCache.Clear();
      current.CountExpenseReceiptDetailMatches = new int?(0);
      IList<CABankTranExpenseDetailMatch> receiptDetailMatches = CABankMatchingProcess.FindExpenseReceiptDetailMatches((PXGraph) graph, current, ((PXSelectBase<CashAccount>) graph.cashAccount).Current ?? PXResultset<CashAccount>.op_Implicit(((PXSelectBase<CashAccount>) graph.cashAccount).Select(Array.Empty<object>())), 0M, (CABankTranExpenseDetailMatch[]) null);
      if (receiptDetailMatches.Any<CABankTranExpenseDetailMatch>())
      {
        CABankTranMatch existingMatch = (CABankTranMatch) null;
        foreach (PXResult<CABankTranMatch> pxResult in ((PXSelectBase<CABankTranMatch>) graph.TranMatch).Select(new object[1]
        {
          (object) current.TranID
        }))
        {
          CABankTranMatch caBankTranMatch = PXResult<CABankTranMatch>.op_Implicit(pxResult);
          if (caBankTranMatch.DocModule != null && caBankTranMatch.DocType != null && caBankTranMatch.DocRefNbr != null)
          {
            existingMatch = caBankTranMatch;
            break;
          }
        }
        foreach (object obj in (IEnumerable<CABankTranExpenseDetailMatch>) receiptDetailMatches)
        {
          CABankTranExpenseDetailMatch expenseDetailMatch = (CABankTranExpenseDetailMatch) matchesCache.Insert(obj);
          if (expenseDetailMatch != null)
          {
            bool flag = false;
            if (existingMatch != null && existingMatch.DocModule == "EP" && existingMatch.DocType == "ECD" && existingMatch.DocRefNbr == expenseDetailMatch.RefNbr)
            {
              flag = true;
              existingMatch = (CABankTranMatch) null;
            }
            matchesCache.SetValue<CABankTranExpenseDetailMatch.isMatched>((object) expenseDetailMatch, (object) flag);
            yield return (object) expenseDetailMatch;
          }
        }
        existingMatch = (CABankTranMatch) null;
      }
      matchesCache.IsDirty = false;
    }
  }

  protected virtual IEnumerable details()
  {
    CABankTransactionsMaint transactionsMaint = this;
    CABankTransactionsMaint.Filter current = ((PXSelectBase<CABankTransactionsMaint.Filter>) transactionsMaint.TranFilter).Current;
    if (current != null && current.CashAccountID.HasValue)
    {
      TimeSpan timeSpan;
      Exception exception;
      PXLongRunStatus status = PXLongOperation.GetStatus(((PXGraph) transactionsMaint).UID, ref timeSpan, ref exception);
      IEnumerable<CABankTran> caBankTrans = (IEnumerable<CABankTran>) null;
      if (status != null)
      {
        object[] source;
        PXLongOperation.GetCustomInfo(((PXGraph) transactionsMaint).UID, ref source);
        if (source != null)
          caBankTrans = Enumerable.Cast<CABankTran>(source);
      }
      foreach (object obj in caBankTrans ?? transactionsMaint.GetUnprocessedTransactions())
        yield return obj;
    }
  }

  protected virtual IEnumerable<CABankTran> GetUnprocessedTransactions()
  {
    CABankTransactionsMaint transactionsMaint = this;
    CABankTransactionsMaint.Filter current = ((PXSelectBase<CABankTransactionsMaint.Filter>) transactionsMaint.TranFilter).Current;
    if (current != null && current.CashAccountID.HasValue)
    {
      PXSelect<CABankTran, Where<CABankTran.processed, Equal<False>, And<CABankTran.cashAccountID, Equal<Required<CABankTran.cashAccountID>>, And<CABankTran.tranType, Equal<Required<CABankTran.tranType>>>>>, OrderBy<Asc<CABankTran.tranID>>> pxSelect = new PXSelect<CABankTran, Where<CABankTran.processed, Equal<False>, And<CABankTran.cashAccountID, Equal<Required<CABankTran.cashAccountID>>, And<CABankTran.tranType, Equal<Required<CABankTran.tranType>>>>>, OrderBy<Asc<CABankTran.tranID>>>((PXGraph) transactionsMaint);
      object[] objArray = new object[2]
      {
        (object) current.CashAccountID,
        (object) current.TranType
      };
      foreach (PXResult<CABankTran> pxResult in ((PXSelectBase<CABankTran>) pxSelect).SelectWithViewContext(objArray))
        yield return PXResult<CABankTran>.op_Implicit(pxResult);
    }
  }

  protected virtual IEnumerable detailMatchesCA()
  {
    CABankTransactionsMaint graph = this;
    CABankTran current = ((PXSelectBase<CABankTran>) graph.Details).Current;
    if (current != null)
    {
      PXCache cache = ((PXSelectBase) graph.DetailMatchesCA).Cache;
      PX.Objects.CA.BankStatementHelpers.CATranExt[] items = cache.Cached.ToArray<PX.Objects.CA.BankStatementHelpers.CATranExt>();
      current.CountMatches = new int?(0);
      cache.Clear();
      foreach (PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount> detailMatch in graph.MatchingService.FindDetailMatches<CABankTransactionsMaint>(graph, current, graph.CurrentMatchSesstings, (PX.Objects.CA.BankStatementHelpers.CATranExt[]) null, 0M))
      {
        PX.Objects.CA.BankStatementHelpers.CATranExt caTranExt1 = PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>.op_Implicit(detailMatch);
        PX.Objects.CA.Light.BAccount baccount = PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>.op_Implicit(detailMatch);
        PX.Objects.CA.BankStatementHelpers.CATranExt caTranExt2 = (PX.Objects.CA.BankStatementHelpers.CATranExt) null;
        bool? nullable1 = caTranExt1.IsMatched;
        bool valueOrDefault = nullable1.GetValueOrDefault();
        PX.Objects.CA.BankStatementHelpers.CATranExt caTranExt3 = caTranExt1;
        nullable1 = new bool?();
        bool? nullable2 = nullable1;
        caTranExt3.IsMatched = nullable2;
        if (caTranExt1.OrigModule == "AP" && caTranExt1.OrigTranType == "CBT")
        {
          foreach (PX.Objects.CA.BankStatementHelpers.CATranExt caTranExt4 in items)
          {
            if (caTranExt4.OrigModule == "AP" && caTranExt4.OrigTranType == "CBT" && caTranExt4.OrigRefNbr == caTranExt1.OrigRefNbr)
            {
              caTranExt1.TranID = caTranExt4.TranID;
              caTranExt2 = ((PXSelectBase<PX.Objects.CA.BankStatementHelpers.CATranExt>) graph.DetailMatchesCA).Update(caTranExt1);
              break;
            }
          }
        }
        if (caTranExt2 == null)
          caTranExt2 = ((PXSelectBase<PX.Objects.CA.BankStatementHelpers.CATranExt>) graph.DetailMatchesCA).Insert(caTranExt1);
        caTranExt2.IsMatched = new bool?(valueOrDefault);
        yield return (object) new PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt, PX.Objects.CA.Light.BAccount>(caTranExt2, baccount);
      }
      cache.IsDirty = false;
    }
  }

  protected virtual bool IsMatchedOnCreateTab(string refNbr, string docType, string module)
  {
    return PXResultset<CABankTranAdjustment>.op_Implicit(PXSelectBase<CABankTranAdjustment, PXSelect<CABankTranAdjustment, Where<CABankTranAdjustment.adjdRefNbr, Equal<Required<CABankTranAdjustment.adjdRefNbr>>, And<CABankTranAdjustment.adjdModule, Equal<Required<CABankTranAdjustment.adjdModule>>, And<CABankTranAdjustment.adjdDocType, Equal<Required<CABankTranAdjustment.adjdDocType>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) refNbr,
      (object) module,
      (object) docType
    })) != null;
  }

  protected virtual bool IsInvoiceMatched(string refNbr, string docType, string module)
  {
    return PXResultset<CABankTranMatch>.op_Implicit(PXSelectBase<CABankTranMatch, PXSelect<CABankTranMatch, Where<CABankTranMatch.docRefNbr, Equal<Required<CABankTranMatch.docRefNbr>>, And<CABankTranMatch.docModule, Equal<Required<CABankTranMatch.docModule>>, And<CABankTranMatch.docType, Equal<Required<CABankTranMatch.docType>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) refNbr,
      (object) module,
      (object) docType
    })) != null;
  }

  protected virtual void PopulateAdjustmentFieldsAR(CABankTranAdjustment adj)
  {
    ((PXGraph) this).GetExtension<StatementApplicationBalancesProto>().PopulateAdjustmentFieldsAR(((PXSelectBase<CABankTran>) this.DetailsForPaymentCreation).Current, adj);
  }

  protected virtual void PopulateAdjustmentFieldsAP(CABankTranAdjustment adj)
  {
    ((PXGraph) this).GetExtension<StatementApplicationBalancesProto>().PopulateAdjustmentFieldsAP(((PXSelectBase<CABankTran>) this.DetailsForPaymentCreation).Current, adj);
  }

  public virtual void UpdateBalance(CABankTranAdjustment adj, bool isCalcRGOL)
  {
    if (adj.AdjdDocType == null || adj.AdjdRefNbr == null)
      return;
    ((PXGraph) this).GetExtension<StatementApplicationBalancesProto>().UpdateBalance(((PXSelectBase<CABankTran>) this.DetailsForPaymentCreation).Current, adj, isCalcRGOL);
  }

  [PXCancelButton]
  [PXUIField]
  protected virtual IEnumerable Cancel(PXAdapter adapter)
  {
    int? cashAccountId = ((PXSelectBase<CABankTransactionsMaint.Filter>) this.TranFilter).Current.CashAccountID;
    ((PXGraph) this).Clear();
    ((PXSelectBase) this.TranFilter).Cache.SetValueExt<CABankTransactionsMaint.Filter.cashAccountID>((object) ((PXSelectBase<CABankTransactionsMaint.Filter>) this.TranFilter).Current, (object) cashAccountId);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "Refresh")]
  public virtual IEnumerable LoadInvoices(PXAdapter adapter)
  {
    CABankTran current1 = ((PXSelectBase<CABankTran>) this.Details).Current;
    bool flag = current1.DrCr == "D";
    if (current1.OrigModule == "AR")
    {
      WebDialogResult webDialogResult = ((PXSelectBase<CABankTransactionsMaint.MatchingLoadOptions>) this.loadOpts).AskExt();
      if (webDialogResult == 1 || webDialogResult == 6)
      {
        if (webDialogResult == 6)
        {
          foreach (PXResult<CABankTranAdjustment> pxResult in ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Select(Array.Empty<object>()))
            ((PXSelectBase) this.Adjustments).Cache.Delete((object) PXResult<CABankTranAdjustment>.op_Implicit(pxResult));
        }
        ((PXSelectBase<CABankTransactionsMaint.MatchingLoadOptions>) this.loadOpts).Current.LoadChildDocuments = "INCRM";
        CABankTransactionsMaint.CABankTransactionsMaintCustomerDocsExtension extension = ((PXGraph) this).GetExtension<CABankTransactionsMaint.CABankTransactionsMaintCustomerDocsExtension>();
        CABankTransactionsMaint.MatchingLoadOptions current2 = ((PXSelectBase<CABankTransactionsMaint.MatchingLoadOptions>) this.loadOpts).Current;
        PX.Objects.AR.ARPayment currentARPayment = new PX.Objects.AR.ARPayment();
        currentARPayment.Released = new bool?(false);
        currentARPayment.OpenDoc = new bool?(true);
        currentARPayment.Hold = new bool?(false);
        currentARPayment.Status = "B";
        currentARPayment.CustomerID = ((PXSelectBase<CABankTran>) this.Details).Current.PayeeBAccountID;
        currentARPayment.CashAccountID = ((PXSelectBase<CABankTran>) this.Details).Current.CashAccountID;
        currentARPayment.PaymentMethodID = ((PXSelectBase<CABankTran>) this.Details).Current.PaymentMethodID;
        currentARPayment.CuryOrigDocAmt = ((PXSelectBase<CABankTran>) this.Details).Current.CuryOrigDocAmt;
        currentARPayment.DocType = "PMT";
        currentARPayment.CuryID = ((PXSelectBase<CABankTran>) this.Details).Current.CuryID;
        currentARPayment.AdjDate = current1.TranDate;
        currentARPayment.AdjTranPeriodID = current1.MatchingFinPeriodID;
        ARSetup current3 = ((PXSelectBase<ARSetup>) this.arsetup).Current;
        PXResultset<PX.Objects.AR.ARInvoice> custDocs = extension.GetCustDocs((ARPaymentEntry.LoadOptions) current2, currentARPayment, current3);
        List<string> stringList = new List<string>();
        foreach (PXResult<CABankTranAdjustment> pxResult in ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Select(Array.Empty<object>()))
        {
          CABankTranAdjustment bankTranAdjustment = PXResult<CABankTranAdjustment>.op_Implicit(pxResult);
          stringList.Add($"{bankTranAdjustment.AdjdDocType}_{bankTranAdjustment.AdjdRefNbr}");
        }
        foreach (PXResult<PX.Objects.AR.ARInvoice> pxResult in ((IEnumerable<PXResult<PX.Objects.AR.ARInvoice>>) custDocs).AsEnumerable<PXResult<PX.Objects.AR.ARInvoice>>().Where<PXResult<PX.Objects.AR.ARInvoice>>((Func<PXResult<PX.Objects.AR.ARInvoice>, bool>) (row => !PXResult<PX.Objects.AR.ARInvoice>.op_Implicit(row).PaymentsByLinesAllowed.GetValueOrDefault())))
        {
          PX.Objects.AR.ARInvoice invoice = PXResult<PX.Objects.AR.ARInvoice>.op_Implicit(pxResult);
          bool? pendingPayment;
          if (!flag)
          {
            if (!(invoice.DocType == "INV"))
            {
              if (invoice.DocType == "PPI")
              {
                pendingPayment = invoice.PendingPayment;
                if (pendingPayment.GetValueOrDefault())
                  continue;
              }
              if (invoice.DocType == "DRM" || invoice.DocType == "FCH")
                continue;
            }
            else
              continue;
          }
          if (flag)
          {
            if (!(invoice.DocType == "PPM"))
            {
              if (invoice.DocType == "PPI")
              {
                pendingPayment = invoice.PendingPayment;
                if (!pendingPayment.GetValueOrDefault())
                  continue;
              }
              if (invoice.DocType == "PMT")
                continue;
            }
            else
              continue;
          }
          string str = $"{invoice.DocType}_{invoice.RefNbr}";
          if (!stringList.Contains(str) && !this.IsMatchedOnCreateTab(invoice.RefNbr, invoice.DocType, "AR") && !this.IsInvoiceMatched(invoice.RefNbr, invoice.DocType, "AR") && PXInvoiceSelectorAttribute.GetRecordsAR(invoice.DocType, current1.TranID, new int?(), current1, ((PXSelectBase) this.Adjustments).Cache, (PXGraph) this).Where<PX.Objects.AR.ARAdjust.ARInvoice>((Func<PX.Objects.AR.ARAdjust.ARInvoice, bool>) (inv => inv.DocType == invoice.DocType && inv.RefNbr == invoice.RefNbr)).Any<PX.Objects.AR.ARAdjust.ARInvoice>())
          {
            Decimal? nullable = current1.CuryUnappliedBal;
            Decimal num1 = 0M;
            if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
            {
              nullable = current1.CuryOrigDocAmt;
              Decimal num2 = 0M;
              if (nullable.GetValueOrDefault() > num2 & nullable.HasValue)
                break;
            }
            CABankTranAdjustment bankTranAdjustment = ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Insert(new CABankTranAdjustment());
            bankTranAdjustment.AdjdDocType = invoice.DocType;
            bankTranAdjustment.AdjdRefNbr = invoice.RefNbr;
            ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Update(bankTranAdjustment);
          }
        }
        Decimal? curyApplAmt1 = current1.CuryApplAmt;
        Decimal num = 0M;
        if (curyApplAmt1.GetValueOrDefault() < num & curyApplAmt1.HasValue)
        {
          List<CABankTranAdjustment> bankTranAdjustmentList = new List<CABankTranAdjustment>();
          foreach (PXResult<CABankTranAdjustment> pxResult in ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Select(Array.Empty<object>()))
          {
            CABankTranAdjustment bankTranAdjustment = PXResult<CABankTranAdjustment>.op_Implicit(pxResult);
            if (bankTranAdjustment.AdjdDocType == "CRM")
              bankTranAdjustmentList.Add(bankTranAdjustment);
          }
          bankTranAdjustmentList.Sort((Comparison<CABankTranAdjustment>) ((a, b) => ((IComparable) a.CuryAdjgAmt).CompareTo((object) b.CuryAdjgAmt)));
          foreach (CABankTranAdjustment bankTranAdjustment1 in bankTranAdjustmentList)
          {
            Decimal? curyAdjgAmt = bankTranAdjustment1.CuryAdjgAmt;
            Decimal? nullable1 = current1.CuryApplAmt;
            Decimal? nullable2 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
            if (curyAdjgAmt.GetValueOrDefault() <= nullable2.GetValueOrDefault() & curyAdjgAmt.HasValue & nullable2.HasValue)
            {
              ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Delete(bankTranAdjustment1);
            }
            else
            {
              CABankTranAdjustment copy = PXCache<CABankTranAdjustment>.CreateCopy(bankTranAdjustment1);
              CABankTranAdjustment bankTranAdjustment2 = copy;
              nullable2 = bankTranAdjustment2.CuryAdjgAmt;
              Decimal? curyApplAmt2 = current1.CuryApplAmt;
              Decimal? nullable3;
              if (!(nullable2.HasValue & curyApplAmt2.HasValue))
              {
                nullable1 = new Decimal?();
                nullable3 = nullable1;
              }
              else
                nullable3 = new Decimal?(nullable2.GetValueOrDefault() + curyApplAmt2.GetValueOrDefault());
              bankTranAdjustment2.CuryAdjgAmt = nullable3;
              ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Update(copy);
            }
          }
        }
      }
    }
    else if (current1.OrigModule == "AP")
    {
      PX.Objects.AP.APPayment currentAPPayment = new PX.Objects.AP.APPayment();
      currentAPPayment.Released = new bool?(false);
      currentAPPayment.OpenDoc = new bool?(true);
      currentAPPayment.Hold = new bool?(false);
      currentAPPayment.Status = "B";
      currentAPPayment.VendorID = current1.PayeeBAccountID;
      currentAPPayment.CashAccountID = current1.CashAccountID;
      currentAPPayment.PaymentMethodID = current1.PaymentMethodID;
      currentAPPayment.CuryOrigDocAmt = current1.CuryOrigDocAmt;
      currentAPPayment.DocType = "CHK";
      currentAPPayment.CuryID = current1.CuryID;
      currentAPPayment.AdjDate = current1.TranDate;
      currentAPPayment.AdjTranPeriodID = current1.MatchingFinPeriodID;
      currentAPPayment.BranchID = ((PXGraph) this).Accessinfo.BranchID;
      PXResultset<PX.Objects.AP.APInvoice> vendDocs = ((PXGraph) this).GetExtension<CABankTransactionsMaint.CABankTransactionsMaintVendorDocsExtension>().GetVendDocs(currentAPPayment, ((PXSelectBase<PX.Objects.AP.APSetup>) this.APSetup).Current);
      List<string> stringList = new List<string>();
      foreach (PXResult<CABankTranAdjustment> pxResult in ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Select(Array.Empty<object>()))
      {
        CABankTranAdjustment bankTranAdjustment = PXResult<CABankTranAdjustment>.op_Implicit(pxResult);
        stringList.Add($"{bankTranAdjustment.AdjdDocType}_{bankTranAdjustment.AdjdRefNbr}");
      }
      foreach (PXResult<PX.Objects.AP.APInvoice> pxResult in ((IEnumerable<PXResult<PX.Objects.AP.APInvoice>>) vendDocs).AsEnumerable<PXResult<PX.Objects.AP.APInvoice>>().Where<PXResult<PX.Objects.AP.APInvoice>>((Func<PXResult<PX.Objects.AP.APInvoice>, bool>) (row => !PXResult<PX.Objects.AP.APInvoice>.op_Implicit(row).PaymentsByLinesAllowed.GetValueOrDefault())))
      {
        PX.Objects.AP.APInvoice invoice = PXResult<PX.Objects.AP.APInvoice>.op_Implicit(pxResult);
        if (!flag || !(invoice.DocType == "ACR") && !(invoice.DocType == "INV"))
        {
          string str = $"{invoice.DocType}_{invoice.RefNbr}";
          if (!stringList.Contains(str) && !this.IsInvoiceMatched(invoice.RefNbr, invoice.DocType, "AP") && !this.IsMatchedOnCreateTab(invoice.RefNbr, invoice.DocType, "AP") && PXInvoiceSelectorAttribute.GetRecordsAP(invoice.DocType, current1.TranID, new int?(), current1, ((PXSelectBase) this.Adjustments).Cache, (PXGraph) this).Where<PX.Objects.AP.APAdjust.APInvoice>((Func<PX.Objects.AP.APAdjust.APInvoice, bool>) (inv => inv.DocType == invoice.DocType && inv.RefNbr == invoice.RefNbr)).Any<PX.Objects.AP.APAdjust.APInvoice>())
          {
            Decimal? nullable = current1.CuryUnappliedBal;
            Decimal num3 = 0M;
            if (nullable.GetValueOrDefault() == num3 & nullable.HasValue)
            {
              nullable = current1.CuryOrigDocAmt;
              Decimal num4 = 0M;
              if (nullable.GetValueOrDefault() > num4 & nullable.HasValue)
                break;
            }
            CABankTranAdjustment bankTranAdjustment = ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Insert(new CABankTranAdjustment());
            bankTranAdjustment.AdjdDocType = invoice.DocType;
            bankTranAdjustment.AdjdRefNbr = invoice.RefNbr;
            ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Update(bankTranAdjustment);
          }
        }
      }
      Decimal? curyApplAmt3 = current1.CuryApplAmt;
      Decimal num = 0M;
      if (curyApplAmt3.GetValueOrDefault() < num & curyApplAmt3.HasValue)
      {
        List<CABankTranAdjustment> bankTranAdjustmentList = new List<CABankTranAdjustment>();
        foreach (PXResult<CABankTranAdjustment> pxResult in ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Select(Array.Empty<object>()))
        {
          CABankTranAdjustment bankTranAdjustment = PXResult<CABankTranAdjustment>.op_Implicit(pxResult);
          if (bankTranAdjustment.AdjdDocType == "ADR")
            bankTranAdjustmentList.Add(bankTranAdjustment);
        }
        bankTranAdjustmentList.Sort((Comparison<CABankTranAdjustment>) ((a, b) => ((IComparable) a.CuryAdjgAmt).CompareTo((object) b.CuryAdjgAmt)));
        foreach (CABankTranAdjustment bankTranAdjustment3 in bankTranAdjustmentList)
        {
          Decimal? curyAdjgAmt = bankTranAdjustment3.CuryAdjgAmt;
          Decimal? nullable4 = current1.CuryApplAmt;
          Decimal? nullable5 = nullable4.HasValue ? new Decimal?(-nullable4.GetValueOrDefault()) : new Decimal?();
          if (curyAdjgAmt.GetValueOrDefault() <= nullable5.GetValueOrDefault() & curyAdjgAmt.HasValue & nullable5.HasValue)
          {
            ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Delete(bankTranAdjustment3);
          }
          else
          {
            CABankTranAdjustment copy = PXCache<CABankTranAdjustment>.CreateCopy(bankTranAdjustment3);
            CABankTranAdjustment bankTranAdjustment4 = copy;
            nullable5 = bankTranAdjustment4.CuryAdjgAmt;
            Decimal? curyApplAmt4 = current1.CuryApplAmt;
            Decimal? nullable6;
            if (!(nullable5.HasValue & curyApplAmt4.HasValue))
            {
              nullable4 = new Decimal?();
              nullable6 = nullable4;
            }
            else
              nullable6 = new Decimal?(nullable5.GetValueOrDefault() + curyApplAmt4.GetValueOrDefault());
            bankTranAdjustment4.CuryAdjgAmt = nullable6;
            ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Update(copy);
          }
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable AutoMatch(PXAdapter adapter)
  {
    ((PXAction) this.Save).Press();
    PXGraph.CreateInstance<CABankMatchingProcess>().DoMatch(this, (((PXSelectBase<CashAccount>) this.cashAccount).Current ?? PXResultset<CashAccount>.op_Implicit(((PXSelectBase<CashAccount>) this.cashAccount).Select(Array.Empty<object>()))).CashAccountID);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ProcessMatched(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CABankTransactionsMaint.\u003C\u003Ec__DisplayClass84_0 cDisplayClass840 = new CABankTransactionsMaint.\u003C\u003Ec__DisplayClass84_0();
    ((PXAction) this.Save).Press();
    PXResultset<CABankTran> pxResultset = ((PXSelectBase<CABankTran>) this.Details).Select(Array.Empty<object>());
    // ISSUE: reference to a compiler-generated field
    cDisplayClass840.toProcess = GraphHelper.RowCast<CABankTran>((IEnumerable) pxResultset).Where<CABankTran>((Func<CABankTran, bool>) (t => t.DocumentMatched.GetValueOrDefault() && !t.Processed.GetValueOrDefault())).ToList<CABankTran>();
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass840.toProcess.Count < 1)
      return adapter.Get();
    PXLongOperation.ClearStatus(((PXGraph) this).UID);
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass840, __methodptr(\u003CProcessMatched\u003Eb__1)));
    ((PXGraph) this).Caches[typeof (PX.Objects.CA.Light.ARInvoice)].ClearQueryCache();
    ((PXGraph) this).Caches[typeof (PX.Objects.CA.Light.APInvoice)].ClearQueryCache();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable MatchSettingsPanel(PXAdapter adapter)
  {
    ((PXSelectBase<CashAccount>) this.cashAccount).AskExt();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable UploadFile(PXAdapter adapter)
  {
    ((PXAction) this.Save).Press();
    bool flag = true;
    CABankTransactionsMaint.Filter current = ((PXSelectBase<CABankTransactionsMaint.Filter>) this.TranFilter).Current;
    if (((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.ImportToSingleAccount.GetValueOrDefault())
    {
      if (current == null || !current.CashAccountID.HasValue)
        throw new PXException("You need to select a Cash Account, for which a statement will be imported");
      CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) current.CashAccountID
      }));
      if (cashAccount != null && string.IsNullOrEmpty(cashAccount.StatementImportTypeName))
        throw new PXException("The statement import service has not been specified for the selected cash account. Update the account settings on the Cash Accounts (CA202000) form.");
    }
    else if (string.IsNullOrEmpty(((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.StatementImportTypeName))
      throw new PXException("You have to configure Statement Import Service. Please, check 'Bank Statement Settings' section in the 'Cash Management Preferences'");
    bool? importToSingleAccount;
    if (((PXSelectBase<CABankTran>) this.Details).Current != null && ((PXGraph) this).IsDirty)
    {
      importToSingleAccount = ((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.ImportToSingleAccount;
      if (!importToSingleAccount.GetValueOrDefault())
      {
        if (((PXSelectBase<CABankTran>) this.Details).Ask("Confirmation", "Unsaved data in this screen will be lost. Continue?", (MessageButtons) 4) != 6)
          flag = false;
      }
      else
        flag = true;
    }
    if (flag && ((PXSelectBase<CABankTransactionsMaint.Filter>) this.NewRevisionPanel).AskExt() == 1)
    {
      CABankTransactionsMaint.Filter copy = (CABankTransactionsMaint.Filter) ((PXSelectBase) this.TranFilter).Cache.CreateCopy((object) ((PXSelectBase<CABankTransactionsMaint.Filter>) this.TranFilter).Current);
      FileInfo aFileInfo = PXContext.SessionTyped<PXSessionStatePXData>().FileInfo.Pop<FileInfo>("ImportStatementProtoFile");
      CABankTransactionsImport instance = PXGraph.CreateInstance<CABankTransactionsImport>();
      CABankTranHeader caBankTranHeader = new CABankTranHeader()
      {
        CashAccountID = current.CashAccountID
      };
      importToSingleAccount = ((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.ImportToSingleAccount;
      if (importToSingleAccount.GetValueOrDefault())
        caBankTranHeader = ((PXSelectBase<CABankTranHeader>) instance.Header).Insert(caBankTranHeader);
      ((PXSelectBase<CABankTranHeader>) instance.Header).Current = caBankTranHeader;
      instance.ImportStatement(aFileInfo, false);
      ((PXAction) instance.Save).Press();
      ((PXGraph) this).Clear();
      ((PXGraph) this).Caches[typeof (CABankTran)].ClearQueryCacheObsolete();
      ((PXSelectBase<CABankTransactionsMaint.Filter>) this.TranFilter).Current = copy;
      if (!copy.CashAccountID.HasValue)
      {
        copy.CashAccountID = ((PXSelectBase<CABankTranHeader>) instance.Header).Current.CashAccountID;
        return (IEnumerable) new List<CABankTransactionsMaint.Filter>()
        {
          copy
        };
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ClearMatch(PXAdapter adapter)
  {
    this.ClearMatchProc(((PXSelectBase<CABankTran>) this.Details).Current);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ClearAllMatches(PXAdapter adapter)
  {
    foreach (PXResult<CABankTran> pxResult in ((PXSelectBase<CABankTran>) this.Details).Select(Array.Empty<object>()))
      this.ClearMatchProc(PXResult<CABankTran>.op_Implicit(pxResult));
    return adapter.Get();
  }

  protected virtual void ClearMatchProc(CABankTran detail)
  {
    bool? processed = detail.Processed;
    bool flag = false;
    if (!(processed.GetValueOrDefault() == flag & processed.HasValue) || !detail.DocumentMatched.GetValueOrDefault() && !detail.CreateDocument.GetValueOrDefault())
      return;
    CABankTran copy = (CABankTran) ((PXSelectBase) this.Details).Cache.CreateCopy((object) detail);
    foreach (PXResult<CABankTranMatch> pxResult in ((PXSelectBase<CABankTranMatch>) this.TranMatch).Select(new object[1]
    {
      (object) detail.TranID
    }))
    {
      CABankTranMatch match = PXResult<CABankTranMatch>.op_Implicit(pxResult);
      if (this.IsMatchedToExpenseReceipt(match))
      {
        EPExpenseClaimDetails expenseClaimDetails = PXResultset<EPExpenseClaimDetails>.op_Implicit(PXSelectBase<EPExpenseClaimDetails, PXSelect<EPExpenseClaimDetails, Where<EPExpenseClaimDetails.claimDetailCD, Equal<Required<EPExpenseClaimDetails.claimDetailCD>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) match.DocRefNbr
        }));
        expenseClaimDetails.BankTranDate = new DateTime?();
        ((PXSelectBase<EPExpenseClaimDetails>) this.ExpenseReceipts).Update(expenseClaimDetails);
      }
      ((PXSelectBase<CABankTranMatch>) this.TranMatch).Delete(match);
    }
    foreach (PXResult<CABankTranAdjustment> pxResult in ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Select(new object[1]
    {
      (object) copy.TranID
    }))
      ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Delete(PXResult<CABankTranAdjustment>.op_Implicit(pxResult));
    foreach (PXResult<CABankTranDetail> pxResult in ((PXSelectBase<CABankTranDetail>) this.TranSplit).Select(new object[1]
    {
      (object) copy.TranID
    }))
      ((PXSelectBase<CABankTranDetail>) this.TranSplit).Delete(PXResult<CABankTranDetail>.op_Implicit(pxResult));
    CABankTransactionsMaint.ClearFields(copy);
    CABankTransactionsMaint.ClearChargeFields(copy);
    ((PXSelectBase<CABankTran>) this.Details).Update(copy);
  }

  public static void ClearFields(CABankTran detail)
  {
    detail.CreateDocument = new bool?(false);
    detail.DocumentMatched = new bool?(false);
    detail.MultipleMatching = new bool?(false);
    detail.HistMatchedToInvoice = new bool?(false);
    detail.MatchedToInvoice = new bool?(false);
    detail.BAccountID = new int?();
    detail.OrigModule = (string) null;
    detail.PaymentMethodID = (string) null;
    detail.PMInstanceID = new int?();
    detail.LocationID = new int?();
    detail.EntryTypeID = (string) null;
    detail.UserDesc = (string) null;
    detail.InvoiceNotFound = new bool?();
    detail.RuleID = new int?();
    detail.CountAdjustments = new int?();
  }

  public static void ClearChargeFields(CABankTran detail)
  {
    detail.ChargeTypeID = (string) null;
    detail.CuryChargeAmt = new Decimal?();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Hide(PXAdapter adapter)
  {
    CABankTran current = ((PXSelectBase<CABankTran>) this.Details).Current;
    bool? processed = current.Processed;
    bool flag = false;
    if (processed.GetValueOrDefault() == flag & processed.HasValue && ((PXSelectBase<CABankTran>) this.Details).Ask("Hide Transaction", "This will undo all changes to this transaction, hide it from feed and mark it as processed. Proceed?", (MessageButtons) 4) == 6)
    {
      this.ClearMatchProc(current);
      current.Hidden = new bool?(true);
      current.Processed = new bool?(true);
      ((PXSelectBase<CABankTran>) this.Details).Update(current);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual void CreateRule()
  {
    CABankTran current = ((PXSelectBase<CABankTran>) this.DetailsForPaymentCreation).Current;
    if (current == null || !current.CreateDocument.GetValueOrDefault())
      return;
    if (current.OrigModule != "CA")
      throw new PXException("Only Cash Management documents can be created according to rules.");
    CABankTranRuleMaintPopup instance = PXGraph.CreateInstance<CABankTranRuleMaintPopup>();
    CABankTranRulePopup bankTranRulePopup = new CABankTranRulePopup();
    bankTranRulePopup.BankDrCr = current.DrCr;
    bankTranRulePopup.BankTranCashAccountID = current.CashAccountID;
    bankTranRulePopup.TranCode = current.TranCode;
    bankTranRulePopup.BankTranDescription = current.TranDesc;
    bankTranRulePopup.AmountMatchingMode = "E";
    bankTranRulePopup.CuryTranAmt = new Decimal?(Math.Abs(current.CuryTranAmt ?? 0.0M));
    bankTranRulePopup.DocumentModule = current.OrigModule;
    bankTranRulePopup.DocumentEntryTypeID = current.EntryTypeID;
    bankTranRulePopup.TranCuryID = current.CuryID;
    bankTranRulePopup.PayeeName = current.PayeeName;
    ((PXSelectBase) instance.Rule).Cache.Insert((object) bankTranRulePopup);
    PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 2);
  }

  [PXUIField]
  [PXButton]
  public virtual void UnapplyRule()
  {
    CABankTran current = ((PXSelectBase<CABankTran>) this.DetailsForPaymentCreation).Current;
    if (current == null || !current.CreateDocument.GetValueOrDefault())
      return;
    int? nullable1 = current.RuleID;
    if (!nullable1.HasValue)
      return;
    CABankTran copy = ((PXSelectBase) this.DetailsForPaymentCreation).Cache.CreateCopy((object) current) as CABankTran;
    CABankTran caBankTran = copy;
    nullable1 = new int?();
    int? nullable2 = nullable1;
    caBankTran.RuleID = nullable2;
    ((PXSelectBase) this.DetailsForPaymentCreation).Cache.SetDefaultExt<CABankTran.origModule>((object) copy);
    ((PXSelectBase) this.DetailsForPaymentCreation).Cache.SetDefaultExt<CABankTran.curyTotalAmt>((object) copy);
    ((PXSelectBase) this.DetailsForPaymentCreation).Cache.Update((object) copy);
  }

  [PXUIField]
  [PXButton]
  public virtual void ViewPayment()
  {
    PX.Objects.CA.BankStatementHelpers.CATranExt current = ((PXSelectBase<PX.Objects.CA.BankStatementHelpers.CATranExt>) this.DetailMatchesCA).Current;
    if (current == null)
      return;
    PXRedirectHelper.TryRedirect(((PXSelectBase) this.DetailMatchesCA).Cache, (object) current, "Document", (PXRedirectHelper.WindowMode) 3);
  }

  [PXUIField]
  [PXButton]
  public virtual void ViewBAccount()
  {
    PX.Objects.CA.BankStatementHelpers.CATranExt current = ((PXSelectBase<PX.Objects.CA.BankStatementHelpers.CATranExt>) this.DetailMatchesCA).Current;
    if ((current != null ? (!current.ReferenceID.HasValue ? 1 : 0) : 1) != 0)
      return;
    PXRedirectHelper.TryRedirect(((PXGraph) this).Caches[typeof (PX.Objects.CR.BAccount)], (object) PX.Objects.CR.BAccount.PK.Find((PXGraph) this, current.ReferenceID), "Business Account", (PXRedirectHelper.WindowMode) 3);
  }

  [PXUIField]
  [PXButton]
  public virtual void ViewBAccountInvoices()
  {
    CABankTranInvoiceMatch current = ((PXSelectBase<CABankTranInvoiceMatch>) this.DetailMatchingInvoices).Current;
    if ((current != null ? (!current.ReferenceID.HasValue ? 1 : 0) : 1) != 0)
      return;
    PXRedirectHelper.TryRedirect(((PXGraph) this).Caches[typeof (PX.Objects.CR.BAccount)], (object) PX.Objects.CR.BAccount.PK.Find((PXGraph) this, current.ReferenceID), "Business Account", (PXRedirectHelper.WindowMode) 3);
  }

  [PXUIField]
  [PXButton]
  public virtual void ViewInvoice()
  {
    CABankTranInvoiceMatch current = ((PXSelectBase<CABankTranInvoiceMatch>) this.DetailMatchingInvoices).Current;
    if (current == null)
      return;
    PXCache pxCache = (PXCache) null;
    object obj = (object) null;
    switch (current.OrigModule)
    {
      case "AP":
        obj = (object) PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(PXSelectBase<PX.Objects.AP.APInvoice, PXSelect<PX.Objects.AP.APInvoice, Where<PX.Objects.AP.APInvoice.docType, Equal<Required<PX.Objects.AP.APInvoice.docType>>, And<PX.Objects.AP.APInvoice.refNbr, Equal<Required<PX.Objects.AP.APInvoice.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) current.OrigTranType,
          (object) current.OrigRefNbr
        }));
        if (obj != null)
        {
          pxCache = ((PXGraph) this).Caches[typeof (PX.Objects.AP.APInvoice)];
          break;
        }
        break;
      case "AR":
        obj = (object) PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) current.OrigTranType,
          (object) current.OrigRefNbr
        }));
        if (obj != null)
        {
          pxCache = ((PXGraph) this).Caches[typeof (PX.Objects.AR.ARInvoice)];
          break;
        }
        break;
    }
    if (pxCache == null || obj == null)
      return;
    PXRedirectHelper.TryRedirect(pxCache, obj, "Document", (PXRedirectHelper.WindowMode) 3);
  }

  [PXButton]
  public virtual void RefreshAfterRuleCreate()
  {
    CABankTran current = ((PXSelectBase<CABankTran>) this.Details).Current;
    ((PXAction) this.cancel).Press();
    ((PXSelectBase<CABankTran>) this.Details).Select(Array.Empty<object>());
    ((PXSelectBase<CABankTran>) this.Details).Current = (CABankTran) ((PXSelectBase) this.Details).Cache.Locate((object) current);
    if (!this.AttemptApplyRules(((PXSelectBase<CABankTran>) this.Details).Current, false))
      return;
    ((PXSelectBase) this.Details).Cache.Update((object) ((PXSelectBase<CABankTran>) this.Details).Current);
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewDocumentToApply(PXAdapter adapter)
  {
    CABankTran current1 = ((PXSelectBase<CABankTran>) this.DetailsForPaymentCreation).Current;
    CABankTranAdjustment current2 = ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Current;
    if (current1?.OrigModule == "AP")
      PXRedirectHelper.TryRedirect(((PXSelectBase) this.dummyAPRegister).Cache, (object) PXResultset<PX.Objects.AP.APRegister>.op_Implicit(PXSelectBase<PX.Objects.AP.APRegister, PXSelect<PX.Objects.AP.APRegister, Where<PX.Objects.AP.APRegister.docType, Equal<Required<PX.Objects.AP.APRegister.docType>>, And<PX.Objects.AP.APRegister.refNbr, Equal<Required<PX.Objects.AP.APRegister.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) current2.AdjdDocType,
        (object) current2.AdjdRefNbr
      })), "View Document", (PXRedirectHelper.WindowMode) 3);
    if (current1.OrigModule == "AR")
      PXRedirectHelper.TryRedirect(((PXSelectBase) this.dummyARRegister).Cache, (object) PXResultset<PX.Objects.AR.ARRegister>.op_Implicit(PXSelectBase<PX.Objects.AR.ARRegister, PXSelect<PX.Objects.AR.ARRegister, Where<PX.Objects.AR.ARRegister.docType, Equal<Required<PX.Objects.AR.ARRegister.docType>>, And<PX.Objects.AR.ARRegister.refNbr, Equal<Required<PX.Objects.AR.ARRegister.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) current2.AdjdDocType,
        (object) current2.AdjdRefNbr
      })), "View Document", (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  protected virtual IEnumerable viewExpenseReceipt(PXAdapter adapter)
  {
    RedirectionToOrigDoc.TryRedirect("ECD", ((PXSelectBase<CABankTranExpenseDetailMatch>) this.ExpenseReceiptDetailMatches).Current.RefNbr, "EP");
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable resetMatchSettingsToDefault(PXAdapter adapter)
  {
    PXCache cache = ((PXSelectBase) this.cashAccount).Cache;
    CashAccount current = ((PXSelectBase<CashAccount>) this.cashAccount).Current;
    cache.SetDefaultExt<CashAccount.receiptTranDaysBefore>((object) current);
    cache.SetDefaultExt<CashAccount.receiptTranDaysAfter>((object) current);
    cache.SetDefaultExt<CashAccount.disbursementTranDaysBefore>((object) current);
    cache.SetDefaultExt<CashAccount.disbursementTranDaysAfter>((object) current);
    cache.SetDefaultExt<CashAccount.allowMatchingCreditMemo>((object) current);
    cache.SetDefaultExt<CashAccount.allowMatchingDebitAdjustment>((object) current);
    cache.SetDefaultExt<CashAccount.refNbrCompareWeight>((object) current);
    cache.SetDefaultExt<CashAccount.dateCompareWeight>((object) current);
    cache.SetDefaultExt<CashAccount.payeeCompareWeight>((object) current);
    cache.SetDefaultExt<CashAccount.dateMeanOffset>((object) current);
    cache.SetDefaultExt<CashAccount.dateSigma>((object) current);
    cache.SetDefaultExt<CashAccount.curyDiffThreshold>((object) current);
    cache.SetDefaultExt<CashAccount.amountWeight>((object) current);
    cache.SetDefaultExt<CashAccount.emptyRefNbrMatching>((object) current);
    cache.SetDefaultExt<CashAccount.skipVoided>((object) current);
    cache.SetDefaultExt<CashAccount.matchThreshold>((object) current);
    cache.SetDefaultExt<CashAccount.relativeMatchThreshold>((object) current);
    cache.SetDefaultExt<CashAccount.invoiceFilterByCashAccount>((object) current);
    cache.SetDefaultExt<CashAccount.invoiceFilterByDate>((object) current);
    cache.SetDefaultExt<CashAccount.daysBeforeInvoiceDiscountDate>((object) current);
    cache.SetDefaultExt<CashAccount.daysBeforeInvoiceDueDate>((object) current);
    cache.SetDefaultExt<CashAccount.daysAfterInvoiceDueDate>((object) current);
    cache.SetDefaultExt<CashAccount.invoiceRefNbrCompareWeight>((object) current);
    cache.SetDefaultExt<CashAccount.invoiceDateCompareWeight>((object) current);
    cache.SetDefaultExt<CashAccount.invoicePayeeCompareWeight>((object) current);
    cache.SetDefaultExt<CashAccount.averagePaymentDelay>((object) current);
    cache.SetDefaultExt<CashAccount.invoiceDateSigma>((object) current);
    ((CashAccount) cache.Update((object) current)).MatchSettingsPerAccount = new bool?(false);
    return adapter.Get();
  }

  protected virtual void CABankTranInvoiceMatch_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    if (!(e.Row is CABankTranInvoiceMatch row))
      return;
    PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CABankTranInvoiceMatch.isMatched>(sender, (object) row, true);
  }

  protected virtual void CABankTranInvoiceMatch_IsMatched_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    object row = e.Row;
    if (!((bool?) e.NewValue).GetValueOrDefault())
      return;
    if (this.CreatePaymentIsChosen(((PXSelectBase<CABankTran>) this.Details).Current))
      throw new PXSetPropertyException("The Create check box is selected for the bank transaction on the Create Payment tab. To match the bank transaction to another document, go to the Create Payment tab and clear the Create check box.", (PXErrorLevel) 3);
    if (!((PXSelectBase<CABankTran>) this.Details).Current.DocumentMatched.GetValueOrDefault())
      return;
    if (((PXSelectBase<CABankTran>) this.Details).Current.MatchedToExpenseReceipt.GetValueOrDefault())
      throw new PXSetPropertyException("The bank transaction is already matched to an expense receipt. To match the bank transaction to another document, open the Match to Expense Receipts tab and unmatch the transaction.", (PXErrorLevel) 3);
    if (!((PXSelectBase<CABankTran>) this.Details).Current.MultipleMatching.GetValueOrDefault() && ((PXSelectBase<CABankTran>) this.Details).Current.MatchedToInvoice.GetValueOrDefault())
      throw new PXSetPropertyException("Another option is already chosen", (PXErrorLevel) 3);
    if (!((PXSelectBase<CABankTran>) this.Details).Current.MatchedToInvoice.GetValueOrDefault())
      throw new PXSetPropertyException("The bank transaction is already matched to a payment. To match the bank transaction to another document, go to the Match to Payments tab and unmatch the transaction.", (PXErrorLevel) 3);
  }

  protected virtual bool CreatePaymentIsChosen(CABankTran bankTran)
  {
    if (!bankTran.CreateDocument.GetValueOrDefault())
      return false;
    return bankTran.OrigModule != "CA" || !string.IsNullOrEmpty(bankTran.EntryTypeID);
  }

  protected virtual void CABankTranInvoiceMatch_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    CABankTran current = ((PXSelectBase<CABankTran>) this.Details).Current;
    CABankTranInvoiceMatch row = e.Row as CABankTranInvoiceMatch;
    CABankTranInvoiceMatch oldRow = e.OldRow as CABankTranInvoiceMatch;
    if (!sender.ObjectsEqual<CABankTranInvoiceMatch.isMatched>((object) row, (object) oldRow))
    {
      if (row.IsMatched.GetValueOrDefault())
      {
        if (current.CreateDocument.GetValueOrDefault())
          ((PXSelectBase) this.Details).Cache.SetValueExt<CABankTran.createDocument>((object) current, (object) false);
        bool flag = row.CuryDiscAmt.HasValue && current.TranDate.HasValue && row.DiscDate.HasValue && current.TranDate.Value <= row.DiscDate.Value;
        CABankTranMatch caBankTranMatch = new CABankTranMatch();
        caBankTranMatch.TranID = current.TranID;
        caBankTranMatch.TranType = current.TranType;
        caBankTranMatch.DocModule = row.OrigModule;
        caBankTranMatch.DocType = row.OrigTranType;
        caBankTranMatch.DocRefNbr = row.OrigRefNbr;
        caBankTranMatch.ReferenceID = row.ReferenceID;
        Decimal? curyTranAmt = row.CuryTranAmt;
        Decimal? nullable = flag ? row.CuryDiscAmt : new Decimal?(0M);
        caBankTranMatch.CuryApplAmt = curyTranAmt.HasValue & nullable.HasValue ? new Decimal?(curyTranAmt.GetValueOrDefault() - nullable.GetValueOrDefault()) : new Decimal?();
        ((PXSelectBase<CABankTranMatch>) this.TranMatch).Insert(caBankTranMatch);
      }
      else
      {
        foreach (CABankTranMatch caBankTranMatch in GraphHelper.RowCast<CABankTranMatch>((IEnumerable) ((PXSelectBase<CABankTranMatch>) this.TranMatch).Select(new object[1]
        {
          (object) current.TranID
        })).Where<CABankTranMatch>((Func<CABankTranMatch, bool>) (item => item.DocModule == row.OrigModule && item.DocType == row.OrigTranType && item.DocRefNbr == row.OrigRefNbr)))
          ((PXSelectBase<CABankTranMatch>) this.TranMatch).Delete(caBankTranMatch);
      }
      bool flag1 = NonGenericIEnumerableExtensions.Any_((IEnumerable) ((PXSelectBase<CABankTranMatch>) this.TranMatch).Select(new object[1]
      {
        (object) current.TranID
      }));
      current.DocumentMatched = new bool?(flag1);
      if (!flag1)
        ((PXSelectBase) this.Details).Cache.SetValueExt<CABankTran.origModule>((object) current, (object) null);
      ((PXSelectBase) this.Details).Cache.Update((object) current);
    }
    sender.IsDirty = false;
  }

  protected virtual void CABankTranInvoiceMatch_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CABankTranInvoiceMatch_OrigTranType_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowSelected<CABankTranExpenseDetailMatch> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CABankTranExpenseDetailMatch>>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<CABankTranExpenseDetailMatch.isMatched>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CABankTranExpenseDetailMatch>>) e).Cache, (object) e.Row, true);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CABankTranExpenseDetailMatch> e)
  {
    CABankTran current = ((PXSelectBase<CABankTran>) this.Details).Current;
    if (!((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<CABankTranExpenseDetailMatch>>) e).Cache.ObjectsEqual<CABankTranExpenseDetailMatch.isMatched>((object) e.Row, (object) e.OldRow))
    {
      bool? nullable = current.CreateDocument;
      if (nullable.GetValueOrDefault())
        ((PXSelectBase) this.Details).Cache.SetValueExt<CABankTran.createDocument>((object) current, (object) false);
      EPExpenseClaimDetails expenseClaimDetails = PXResultset<EPExpenseClaimDetails>.op_Implicit(PXSelectBase<EPExpenseClaimDetails, PXSelect<EPExpenseClaimDetails, Where<EPExpenseClaimDetails.claimDetailCD, Equal<Required<EPExpenseClaimDetails.claimDetailCD>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) e.Row.RefNbr
      }));
      nullable = e.Row.IsMatched;
      if (nullable.GetValueOrDefault())
      {
        ((PXSelectBase<CABankTranMatch>) this.TranMatch).Insert(new CABankTranMatch()
        {
          TranID = current.TranID,
          TranType = current.TranType,
          DocModule = "EP",
          DocRefNbr = e.Row.RefNbr,
          DocType = "ECD",
          ReferenceID = e.Row.ReferenceID,
          CuryApplAmt = expenseClaimDetails.ClaimCuryTranAmtWithTaxes
        });
        expenseClaimDetails.BankTranDate = current.TranDate;
        ((PXSelectBase<EPExpenseClaimDetails>) this.ExpenseReceipts).Update(expenseClaimDetails);
      }
      else
      {
        foreach (PXResult<CABankTranMatch> pxResult in ((PXSelectBase<CABankTranMatch>) this.TranMatch).Select(new object[1]
        {
          (object) current.TranID
        }))
          ((PXSelectBase<CABankTranMatch>) this.TranMatch).Delete(PXResult<CABankTranMatch>.op_Implicit(pxResult));
        expenseClaimDetails.BankTranDate = new DateTime?();
        ((PXSelectBase<EPExpenseClaimDetails>) this.ExpenseReceipts).Update(expenseClaimDetails);
        ((PXSelectBase) this.Details).Cache.SetValueExt<CABankTran.origModule>((object) current, (object) null);
      }
      current.DocumentMatched = e.Row.IsMatched;
      ((PXSelectBase) this.Details).Cache.Update((object) current);
    }
    ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<CABankTranExpenseDetailMatch>>) e).Cache.IsDirty = false;
  }

  protected virtual void _(
    PX.Data.Events.RowPersisting<CABankTranExpenseDetailMatch> e)
  {
    e.Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<CABankTranExpenseDetailMatch.isMatched> e)
  {
    object row = e.Row;
    if (!((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CABankTranExpenseDetailMatch.isMatched>, object, object>) e).NewValue).GetValueOrDefault())
      return;
    if (this.CreatePaymentIsChosen(((PXSelectBase<CABankTran>) this.Details).Current))
      throw new PXSetPropertyException("The Create check box is selected for the bank transaction on the Create Payment tab. To match the bank transaction to another document, go to the Create Payment tab and clear the Create check box.", (PXErrorLevel) 3);
    if (!((PXSelectBase<CABankTran>) this.Details).Current.DocumentMatched.GetValueOrDefault())
      return;
    if (((PXSelectBase<CABankTran>) this.Details).Current.MatchedToInvoice.GetValueOrDefault())
      throw new PXSetPropertyException("The bank transaction is already matched to an invoice. To match the bank transaction to another document, go to the Match to Invoices tab and unmatch the transaction.", (PXErrorLevel) 3);
    if (((PXSelectBase<CABankTran>) this.Details).Current.MatchedToExpenseReceipt.GetValueOrDefault())
      throw new PXSetPropertyException("Another option is already chosen", (PXErrorLevel) 3);
    throw new PXSetPropertyException("The bank transaction is already matched to a payment. To match the bank transaction to another document, go to the Match to Payments tab and unmatch the transaction.", (PXErrorLevel) 3);
  }

  protected virtual void CABankTranAdjustment_AdjdRefNbr_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CABankTranAdjustment row = (CABankTranAdjustment) e.Row;
    row.AdjgDocDate = ((PXSelectBase<CABankTran>) this.DetailsForPaymentCreation).Current.TranDate;
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
    if (((PXSelectBase<CABankTran>) this.DetailsForPaymentCreation).Current.OrigModule == "AP")
    {
      this.PopulateAdjustmentFieldsAP(row);
      sender.SetDefaultExt<CABankTranAdjustment.adjdTranPeriodID>(e.Row);
    }
    else if (((PXSelectBase<CABankTran>) this.DetailsForPaymentCreation).Current.OrigModule == "AR")
    {
      this.PopulateAdjustmentFieldsAR(row);
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

  protected virtual void CABankTranAdjustment_AdjdRefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    CABankTranAdjustment row = (CABankTranAdjustment) e.Row;
    if (row == null)
      return;
    foreach (PXResult<CABankTranAdjustment> pxResult in ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Select(Array.Empty<object>()))
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
    CABankTran current = ((PXSelectBase<CABankTran>) this.Details).Current;
    sender.SetValue<CABankTranAdjustment.adjdModule>((object) row, (object) current.OrigModule);
  }

  protected virtual void CABankTranAdjustment_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    CABankTranAdjustment row = (CABankTranAdjustment) e.Row;
    if (row == null || row.AdjdRefNbr == null)
      ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<CABankTran.createDocument>((object) ((PXSelectBase<CABankTran>) this.Details).Current, (object) ((PXSelectBase<CABankTran>) this.Details).Current.CreateDocument, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 5, new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CABankTranAdjustment.adjdRefNbr>(sender)
      }));
    if (row == null)
      return;
    Decimal? curyDocBal = row.CuryDocBal;
    Decimal num = 0M;
    if (!(curyDocBal.GetValueOrDefault() < num & curyDocBal.HasValue))
      return;
    sender.RaiseExceptionHandling<CABankTranAdjustment.curyDocBal>((object) row, (object) row.CuryDocBal, (Exception) new PXSetPropertyException("The document is out of the balance."));
  }

  protected virtual void CABankTranAdjustment_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CABankTranAdjustment row = (CABankTranAdjustment) e.Row;
    if (((PXSelectBase<CABankTran>) this.Details).Current != null && row != null)
    {
      bool flag1 = row.AdjdRefNbr != null && ((PXSelectBase<CABankTran>) this.DetailsForPaymentCreation).Current?.OrigModule == "AP";
      PXUIFieldAttribute.SetEnabled<CABankTranAdjustment.adjdRefNbr>(sender, (object) row, row.AdjdRefNbr == null);
      PXUIFieldAttribute.SetEnabled<CABankTranAdjustment.adjdDocType>(sender, (object) row, row.AdjdRefNbr == null);
      PXUIFieldAttribute.SetEnabled<CABankTranAdjustment.curyAdjgAmt>(sender, (object) row, row.AdjdRefNbr != null);
      PXUIFieldAttribute.SetEnabled<CABankTranAdjustment.curyAdjgDiscAmt>(sender, (object) row, row.AdjdRefNbr != null);
      PXUIFieldAttribute.SetEnabled<CABankTranAdjustment.curyAdjgWhTaxAmt>(sender, (object) row, flag1);
      PX.Objects.AR.Customer customer = PX.Objects.AR.Customer.PK.Find((PXGraph) this, ((PXSelectBase<CABankTran>) this.Details).Current.PayeeBAccountID);
      PXCache pxCache = sender;
      CABankTranAdjustment bankTranAdjustment = row;
      int num1;
      if (this.canBeWrittenOff(customer, row))
      {
        bool? nullable = row.Released;
        if (!nullable.GetValueOrDefault())
        {
          nullable = row.Voided;
          bool flag2 = false;
          num1 = nullable.GetValueOrDefault() == flag2 & nullable.HasValue ? 1 : 0;
          goto label_5;
        }
      }
      num1 = 0;
label_5:
      PXUIFieldAttribute.SetEnabled<CABankTranAdjustment.curyAdjgWOAmt>(pxCache, (object) bankTranAdjustment, num1 != 0);
      PXUIFieldAttribute.SetEnabled<CABankTranAdjustment.writeOffReasonCode>(sender, (object) row, this.canBeWrittenOff(customer, row));
      if (((PXSelectBase<CABankTran>) this.Details).Current.InvoiceInfo != null && row.AdjdRefNbr == ((PXSelectBase<CABankTran>) this.Details).Current.InvoiceInfo)
      {
        Decimal? curyAdjgAmt = row.CuryAdjgAmt;
        Decimal? curyTotalAmt = ((PXSelectBase<CABankTran>) this.Details).Current.CuryTotalAmt;
        if (curyAdjgAmt.GetValueOrDefault() == curyTotalAmt.GetValueOrDefault() & curyAdjgAmt.HasValue == curyTotalAmt.HasValue)
        {
          Decimal? curyDocBal = row.CuryDocBal;
          Decimal num2 = 0M;
          if (curyDocBal.GetValueOrDefault() == num2 & curyDocBal.HasValue)
            goto label_9;
        }
        sender.RaiseExceptionHandling<CABankTranAdjustment.curyAdjgAmt>((object) row, (object) row.CuryAdjgAmt, (Exception) new PXSetPropertyException("Please note that the application amount is different from the invoice total.", (PXErrorLevel) 2));
      }
    }
label_9:
    PXUIFieldAttribute.SetVisible<PX.Objects.AR.ARInvoice.customerID>(((PXGraph) this).Caches[typeof (PX.Objects.AR.ARInvoice)], (object) null, PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>() && ((PXSelectBase<CABankTran>) this.DetailsForPaymentCreation).Current?.OrigModule == "AR");
  }

  protected virtual void CABankTranAdjustment_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    CABankTranAdjustment row = (CABankTranAdjustment) e.Row;
    if (row == null)
      return;
    CABankTran caBankTran = PXResultset<CABankTran>.op_Implicit(PXSelectBase<CABankTran, PXSelect<CABankTran, Where<CABankTran.tranID, Equal<Required<CABankTranAdjustment.tranID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.TranID
    }));
    if (row.AdjdRefNbr == null)
      ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<CABankTran.createDocument>((object) caBankTran, (object) caBankTran.CreateDocument, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 5, new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<CABankTranAdjustment.adjdRefNbr>(sender)
      }));
    Decimal? nullable = row.CuryWhTaxBal;
    Decimal num1 = 0M;
    if (nullable.GetValueOrDefault() < num1 & nullable.HasValue)
      sender.RaiseExceptionHandling<CABankTran.createDocument>(e.Row, (object) row.CuryAdjgWhTaxAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
    if (caBankTran.OrigModule == "AR")
    {
      nullable = row.CuryAdjgWhTaxAmt;
      Decimal num2 = 0M;
      if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue) && string.IsNullOrEmpty(row.WriteOffReasonCode))
        ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<CABankTran.createDocument>((object) caBankTran, (object) caBankTran.CreateDocument, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[2]
        {
          (object) PXUIFieldAttribute.GetDisplayName<CABankTranAdjustment.writeOffReasonCode>(sender),
          (object) (PXErrorLevel) 5
        }));
    }
    if (!string.IsNullOrEmpty(row.WriteOffReasonCode))
    {
      PX.Objects.CS.ReasonCode reasonCode = PX.Objects.CS.ReasonCode.PK.Find((PXGraph) this, row.WriteOffReasonCode);
      if (reasonCode?.Usage == "B")
      {
        nullable = row.CuryAdjgWOAmt;
        if (nullable.Value < 0M)
          sender.RaiseExceptionHandling<CABankTranAdjustment.writeOffReasonCode>((object) row, (object) row.WriteOffReasonCode, (Exception) new PXSetPropertyException("For a negative write-off amount, specify the Credit write-off reason code."));
      }
      if (reasonCode?.Usage == "C")
      {
        nullable = row.CuryAdjgWOAmt;
        if (nullable.Value > 0M)
          sender.RaiseExceptionHandling<CABankTranAdjustment.writeOffReasonCode>((object) row, (object) row.WriteOffReasonCode, (Exception) new PXSetPropertyException("For a positive write-off amount, specify the Balance write-off reason code."));
      }
    }
    nullable = row.CuryDocBal;
    Decimal num3 = 0M;
    if (!(nullable.GetValueOrDefault() < num3 & nullable.HasValue))
      return;
    sender.RaiseExceptionHandling<CABankTranAdjustment.curyDocBal>((object) row, (object) row.CuryDocBal, (Exception) new PXSetPropertyException("The document is out of the balance."));
  }

  protected virtual void CABankTran_CreateDocument_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row as CABankTran).DocumentMatched.GetValueOrDefault())
      return;
    bool? nullable = (bool?) e.NewValue;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = ((PXSelectBase<CABankTran>) this.Details).Current.MatchedToExpenseReceipt;
    if (nullable.GetValueOrDefault())
      throw new PXSetPropertyException("The bank transaction is already matched to an expense receipt. To match the bank transaction to another document, open the Match to Expense Receipts tab and unmatch the transaction.", (PXErrorLevel) 3);
    nullable = ((PXSelectBase<CABankTran>) this.Details).Current.MatchedToInvoice;
    if (nullable.GetValueOrDefault())
      throw new PXSetPropertyException("The bank transaction is already matched to an invoice. To match the bank transaction to another document, go to the Match to Invoices tab and unmatch the transaction.", (PXErrorLevel) 3);
    throw new PXSetPropertyException("The bank transaction is already matched to a payment. To match the bank transaction to another document, go to the Match to Payments tab and unmatch the transaction.", (PXErrorLevel) 3);
  }

  protected virtual bool ApplyInvoiceInfo(PXCache sender, CABankTran row)
  {
    if (row.CreateDocument.GetValueOrDefault() && row.InvoiceInfo != null)
    {
      string Module;
      object invoiceByInvoiceInfo = ((PXGraph) this).GetService<ICABankTransactionsRepository>().FindInvoiceByInvoiceInfo<CABankTransactionsMaint>(this, row, out Module);
      if (invoiceByInvoiceInfo != null)
      {
        int? nullable1 = new int?();
        int? nullable2;
        string str;
        string refNbr;
        switch (Module)
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
        sender.SetValueExt<CABankTran.origModule>((object) row, (object) Module);
        sender.SetValue<CABankTran.payeeBAccountID>((object) row, (object) nullable2);
        object payeeBaccountId = (object) row.PayeeBAccountID;
        sender.RaiseFieldUpdating<CABankTran.payeeBAccountID>((object) row, ref payeeBaccountId);
        sender.RaiseFieldUpdated<CABankTran.payeeBAccountID>((object) row, (object) null);
        if (str != null)
        {
          try
          {
            sender.SetValueExt<CABankTran.paymentMethodID>((object) row, (object) str);
            sender.SetValueExt<CABankTran.pMInstanceID>((object) row, (object) nullable1);
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
      sender.SetValue<CABankTran.invoiceNotFound>((object) row, (object) true);
    }
    return false;
  }

  protected virtual void CABankTran_CreateDocument_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CABankTran row))
      return;
    bool? createDocument = row.CreateDocument;
    bool flag1 = false;
    if (createDocument.GetValueOrDefault() == flag1 & createDocument.HasValue)
      ((PXSelectBase) this.Details).Cache.SetValueExt<CABankTran.documentMatched>((object) row, (object) row.CreateDocument);
    createDocument = row.CreateDocument;
    if (createDocument.GetValueOrDefault() && !row.CuryInfoID.HasValue)
      this.SetDefaultCurrencyInfoID(row);
    this.ResetTranFields(sender, row);
    this.ClearTaxInfo(sender, row);
    createDocument = row.CreateDocument;
    if (createDocument.GetValueOrDefault())
    {
      sender.SetDefaultExt<CABankTran.curyApplAmt>((object) row);
      try
      {
        ((PXSelectBase) this.Details).Cache.SetDefaultExt<CABankTran.matchingPaymentDate>((object) row);
        ((PXSelectBase) this.Details).Cache.SetDefaultExt<CABankTran.matchingfinPeriodID>((object) row);
      }
      catch (PXSetPropertyException ex)
      {
      }
      bool flag2 = false;
      try
      {
        if (!this.MatchInvoiceProcess)
          flag2 = this.ApplyInvoiceInfo(sender, row);
      }
      catch (PXSetPropertyException ex)
      {
        sender.RaiseExceptionHandling<CABankTran.invoiceInfo>((object) row, (object) row.CreateDocument, (Exception) new PXSetPropertyException(((Exception) ex).Message, (PXErrorLevel) 2));
        foreach (PXResult<CABankTranAdjustment> pxResult in ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Select(Array.Empty<object>()))
          ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Delete(PXResult<CABankTranAdjustment>.op_Implicit(pxResult));
      }
      CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(((PXSelectBase<CashAccount>) this.cashAccount).Select(Array.Empty<object>()));
      bool flag3 = cashAccount != null && cashAccount.ClearingAccount.GetValueOrDefault();
      row.UserDesc = row.TranDesc;
      if (row.CreateDocument.GetValueOrDefault() && !flag2 && !flag3 && !this.MatchInvoiceProcess)
        this.AttemptApplyRules(row, !e.ExternalCall);
      row.UserDesc = row.TranDesc;
      ((PXSelectBase) this.Details).Cache.SetValueExt<CABankTran.documentMatched>((object) row, (object) (bool) (!row.CreateDocument.GetValueOrDefault() ? 0 : (CABankTransactionsMaint.ValidateTranFields(this, sender, row, (PXSelectBase<CABankTranAdjustment>) this.Adjustments) ? 1 : 0)));
    }
    else
      sender.SetValue<CABankTran.invoiceNotFound>((object) row, (object) false);
  }

  private void ClearTaxInfo(PXCache sender, CABankTran row)
  {
    sender.SetDefaultExt<CABankTran.curyTaxTotal>((object) row);
    foreach (PXResult<CABankTax> pxResult in ((PXSelectBase<CABankTax>) this.Taxes).Select(Array.Empty<object>()))
      ((PXSelectBase<CABankTax>) this.Taxes).Delete(PXResult<CABankTax>.op_Implicit(pxResult));
    foreach (PXResult<CABankTaxTran> pxResult in ((PXSelectBase<CABankTaxTran>) this.TaxTrans).Select(Array.Empty<object>()))
      ((PXSelectBase<CABankTaxTran>) this.TaxTrans).Delete(PXResult<CABankTaxTran>.op_Implicit(pxResult));
  }

  protected virtual void ResetTranFields(PXCache cache, CABankTran transaction)
  {
    cache.SetDefaultExt<CABankTran.ruleID>((object) transaction);
    cache.SetDefaultExt<CABankTran.origModule>((object) transaction);
    cache.SetDefaultExt<CABankTran.curyTotalAmt>((object) transaction);
  }

  protected virtual void ClearRule(PXCache cache, CABankTran transaction)
  {
    this.ResetTranFields(cache, transaction);
  }

  [PXMergeAttributes]
  [PXDefault("M")]
  protected virtual void _(PX.Data.Events.CacheAttached<CABankTran.matchReason> e)
  {
  }

  protected virtual void CABankTran_MultipleMatching_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CABankTran row))
      return;
    bool? nullable = row.MultipleMatching;
    if (!nullable.GetValueOrDefault())
    {
      nullable = row.DocumentMatched;
      if (nullable.GetValueOrDefault())
      {
        nullable = row.MatchedToInvoice;
        if (nullable.GetValueOrDefault())
        {
          ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.documentMatched>((object) row, (object) false);
          ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.matchedToExisting>((object) row, (object) null);
          ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.matchedToInvoice>((object) row, (object) null);
          ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.matchedToExpenseReceipt>((object) row, (object) null);
          ((PXSelectBase) this.Details).Cache.SetValueExt<CABankTran.origModule>((object) row, (object) null);
          ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.chargeTypeID>((object) row, (object) null);
          ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.curyChargeAmt>((object) row, (object) null);
          ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.curyChargeTaxAmt>((object) row, (object) null);
        }
      }
    }
    nullable = (bool?) e.OldValue;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = row.MultipleMatching;
    if (nullable.GetValueOrDefault())
      return;
    ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.chargeTypeID>((object) row, (object) null);
    ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.curyChargeAmt>((object) row, (object) null);
    ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.curyChargeTaxAmt>((object) row, (object) null);
  }

  protected virtual void CABankTran_MultipleMatchingToPayments_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CABankTran row))
      return;
    bool? nullable = row.MultipleMatchingToPayments;
    if (nullable.GetValueOrDefault())
      return;
    nullable = row.MatchedToInvoice;
    bool flag1 = false;
    if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue))
      return;
    nullable = row.MatchedToExpenseReceipt;
    bool flag2 = false;
    if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
      return;
    ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.documentMatched>((object) row, (object) false);
    ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.matchReceiptsAndDisbursements>((object) row, (object) false);
  }

  protected virtual void CABankTran_MatchReceiptsAndDisbursements_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CABankTran row) || row.MultipleMatchingToPayments.GetValueOrDefault())
      return;
    ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.documentMatched>((object) row, (object) false);
  }

  protected virtual void CABankTran_OrigModule_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (string.IsNullOrEmpty((string) e.NewValue))
      return;
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(((PXSelectBase<CashAccount>) this.cashAccount).Select(Array.Empty<object>()));
    int num = cashAccount != null ? (cashAccount.ClearingAccount.GetValueOrDefault() ? 1 : 0) : 0;
    string newValue = (string) e.NewValue;
    if (num == 0)
      return;
    switch (newValue)
    {
      case "CA":
        throw new PXSetPropertyException<CABankTran.origModule>("A document of this type cannot be recorded to this account. The account is selected as a clearing account on the Cash Accounts (CA202000) form.");
      case "AP":
        if (!(((CABankTran) e.Row).DrCr == "C"))
          break;
        goto case "CA";
    }
  }

  protected virtual void CABankTran_OrigModule_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    CABankTran row = (CABankTran) e.Row;
    if (row != null)
    {
      bool? nullable = row.CreateDocument;
      if (nullable.GetValueOrDefault())
      {
        CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(((PXSelectBase<CashAccount>) this.cashAccount).Select(Array.Empty<object>()));
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

  protected virtual void CABankTran_OrigModule_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if ((CABankTran) e.Row == null)
      return;
    sender.SetDefaultExt<CABankTran.payeeBAccountID>(e.Row);
    sender.SetDefaultExt<CABankTran.entryTypeID>(e.Row);
  }

  protected virtual void CABankTran_ChargeTypeID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CABankTran row = (CABankTran) e.Row;
    if (row == null)
      return;
    sender.SetDefaultExt<CABankTran.chargeDrCr>(e.Row);
    if (row.ChargeTypeID != null)
      return;
    sender.SetDefaultExt<CABankTran.curyChargeAmt>(e.Row);
  }

  protected virtual void CABankTran_PayeeBAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if ((CABankTran) e.Row == null)
      return;
    sender.SetDefaultExt<CABankTran.payeeLocationID>(e.Row);
    sender.SetDefaultExt<CABankTran.paymentMethodID>(e.Row);
    sender.SetDefaultExt<CABankTran.pMInstanceID>(e.Row);
    ((PXSelectBase) this.DetailMatchingInvoices).View.RequestRefresh();
  }

  protected virtual void CABankTran_PayeeBAccountIDCopy_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CABankTran row = (CABankTran) e.Row;
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
      ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.documentMatched>((object) row, (object) false);
      ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.matchedToExisting>((object) row, (object) null);
      ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.matchedToInvoice>((object) row, (object) null);
      ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.matchedToExpenseReceipt>((object) row, (object) null);
      ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.origModule>((object) row, (object) null);
    }
    sender.SetDefaultExt<CABankTran.payeeLocationID>(e.Row);
    sender.SetDefaultExt<CABankTran.paymentMethodID>(e.Row);
    sender.SetDefaultExt<CABankTran.pMInstanceID>(e.Row);
    sender.SetDefaultExt<CABankTran.entryTypeID>(e.Row);
    ((PXSelectBase) this.DetailMatchingInvoices).View.RequestRefresh();
  }

  protected virtual void CABankTran_DocumentMatched_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CABankTran row = (CABankTran) e.Row;
    if (row == null || !row.DocumentMatched.GetValueOrDefault())
      return;
    CABankTranMatch match = ((PXSelectBase<CABankTranMatch>) this.TranMatch).SelectSingle(new object[1]
    {
      (object) row.TranID
    });
    if (match != null && !string.IsNullOrEmpty(match.DocRefNbr) && !this.IsMatchedToExpenseReceipt(match))
    {
      ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.origModule>((object) row, (object) match.DocModule);
      if (!row.PayeeBAccountIDCopy.HasValue)
      {
        ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.payeeBAccountIDCopy>((object) row, (object) match.ReferenceID);
        object payeeBaccountIdCopy = (object) row.PayeeBAccountIDCopy;
        ((PXSelectBase) this.Details).Cache.RaiseFieldUpdating<CABankTran.payeeBAccountIDCopy>((object) row, ref payeeBaccountIdCopy);
        ((PXSelectBase) this.Details).Cache.RaiseFieldUpdated<CABankTran.payeeBAccountIDCopy>((object) row, (object) null);
      }
      else
      {
        ((PXSelectBase) this.Details).Cache.SetDefaultExt<CABankTran.payeeLocationID>((object) row);
        ((PXSelectBase) this.Details).Cache.SetDefaultExt<CABankTran.paymentMethodID>((object) row);
        ((PXSelectBase) this.Details).Cache.SetDefaultExt<CABankTran.pMInstanceID>((object) row);
      }
    }
    ((PXSelectBase) this.Details).Cache.SetDefaultExt<CABankTran.matchReason>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CABankTran, CABankTran.taxZoneID> e)
  {
    CABankTran row = e.Row;
    if (!(row.OrigModule == "CA") || !(row.TaxZoneID != (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CABankTran, CABankTran.taxZoneID>, CABankTran, object>) e).OldValue) || (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CABankTran, CABankTran.taxZoneID>, CABankTran, object>) e).OldValue == null)
      return;
    foreach (PXResult<CABankTranDetail> pxResult in ((PXSelectBase<CABankTranDetail>) this.TranSplit).Select(Array.Empty<object>()))
      ((PXSelectBase<CABankTranDetail>) this.TranSplit).Delete(PXResult<CABankTranDetail>.op_Implicit(pxResult));
    ((PXSelectBase<CABankTranDetail>) this.TranSplit).Insert(new CABankTranDetail());
  }

  protected virtual void CABankTran_MatchingPaymentDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    CABankTran row = (CABankTran) e.Row;
    DateTime newValue = (DateTime) e.NewValue;
    DateTime dateTime = row.TranDate.Value;
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(((PXSelectBase<CashAccount>) this.cashAccount).Select(Array.Empty<object>()));
    if (cashAccount != null)
    {
      int? disbursementTranDaysBefore = cashAccount.DisbursementTranDaysBefore;
    }
    if (newValue > dateTime && sender.RaiseExceptionHandling<CABankTran.matchingPaymentDate>((object) row, (object) newValue, (Exception) new PXSetPropertyException("The payment date must be equal to or earlier than the bank transaction date.")))
      throw new PXSetPropertyException("The payment date must be equal to or earlier than the bank transaction date.");
  }

  protected virtual void CABankTran_EntryTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    CABankTran row = (CABankTran) e.Row;
    if (row == null)
      return;
    if (row.OrigModule == "CA")
    {
      string str = this.GetUnrecognizedReceiptDefaultEntryType(row);
      if (string.IsNullOrEmpty(str))
        str = this.GetDefaultCashAccountEntryType(row);
      e.NewValue = (object) str;
    }
    else
      e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual string GetUnrecognizedReceiptDefaultEntryType(CABankTran row)
  {
    string paymentEntryTypeId = ((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.UnknownPaymentEntryTypeID;
    return PXResultset<CAEntryType>.op_Implicit(PXSelectBase<CAEntryType, PXSelectJoin<CAEntryType, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>, Where<CashAccountETDetail.cashAccountID, Equal<Required<CABankTran.cashAccountID>>, And<CAEntryType.module, Equal<BatchModule.moduleCA>, And<CAEntryType.drCr, Equal<Required<CABankTran.drCr>>, And<CAEntryType.entryTypeId, Equal<Required<CAEntryType.entryTypeId>>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) row.CashAccountID,
      (object) row.DrCr,
      (object) paymentEntryTypeId
    })) == null ? (string) null : paymentEntryTypeId;
  }

  protected virtual string GetDefaultCashAccountEntryType(CABankTran row)
  {
    return PXResultset<CAEntryType>.op_Implicit(PXSelectBase<CAEntryType, PXSelectJoin<CAEntryType, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>, Where<CashAccountETDetail.cashAccountID, Equal<Required<CABankTran.cashAccountID>>, And<CashAccountETDetail.isDefault, Equal<True>, And<CAEntryType.module, Equal<BatchModule.moduleCA>, And<CAEntryType.drCr, Equal<Required<CABankTran.drCr>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.CashAccountID,
      (object) row.DrCr
    }))?.EntryTypeId;
  }

  protected virtual void CABankTran_PaymentMethodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if ((CABankTran) e.Row == null)
      return;
    sender.SetDefaultExt<CABankTran.pMInstanceID>(e.Row);
  }

  protected virtual void CABankTran_PaymentMethodIDCopy_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if ((CABankTran) e.Row == null)
      return;
    sender.SetDefaultExt<CABankTran.pMInstanceID>(e.Row);
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2025 R1.")]
  protected virtual void CABankTran_EntryTypeId_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
  }

  protected virtual void CABankTran_EntryTypeID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CABankTran row))
      return;
    CAEntryType caEntryType = PXResultset<CAEntryType>.op_Implicit(PXSelectBase<CAEntryType, PXSelect<CAEntryType, Where<CAEntryType.entryTypeId, Equal<Required<CAEntryType.entryTypeId>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.EntryTypeID
    }));
    bool? nullable;
    if (caEntryType != null)
    {
      row.DrCr = caEntryType.DrCr;
      nullable = caEntryType.UseToReclassifyPayments;
      if (nullable.GetValueOrDefault() && row.CashAccountID.HasValue)
      {
        if (PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, NotEqual<Required<CashAccount.cashAccountID>>, And<CashAccount.curyID, Equal<Required<CashAccount.curyID>>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, new object[2]
        {
          (object) row.CashAccountID,
          (object) row.CuryID
        })) == null)
          sender.RaiseExceptionHandling<CABankTran.entryTypeID>((object) row, (object) null, (Exception) new PXSetPropertyException("This Entry Type requires to set a Cash Account with currency {0} as an Offset Account. Currently, there is no such a Cash Account defined in the system", (PXErrorLevel) 2, new object[1]
          {
            (object) row.CuryID
          }));
      }
    }
    nullable = row.CreateDocument;
    if (!nullable.GetValueOrDefault())
      return;
    sender.SetDefaultExt<CAAdj.taxCalcMode>((object) row);
    sender.SetDefaultExt<CAAdj.taxZoneID>((object) row);
  }

  protected virtual void CABankTran_MatchStatsInfo_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    CABankTran row = (CABankTran) e.Row;
    if (row == null)
      return;
    bool? nullable = row.DocumentMatched;
    string str;
    if (!nullable.GetValueOrDefault())
    {
      nullable = row.CreateDocument;
      if (!nullable.GetValueOrDefault())
      {
        str = PXMessages.LocalizeFormatNoPrefix("Transaction is not matched.", Array.Empty<object>());
        goto label_10;
      }
    }
    nullable = row.MatchedToExisting;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.CreateDocument;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        nullable = row.MatchedToInvoice;
        if (nullable.GetValueOrDefault())
        {
          str = PXMessages.LocalizeFormatNoPrefix("A new payment will be created for this transaction based on the invoice details.", Array.Empty<object>());
          goto label_10;
        }
        nullable = row.MatchedToExpenseReceipt;
        str = !nullable.GetValueOrDefault() ? PXMessages.LocalizeFormatNoPrefix("Transaction is matched to an existing document.", Array.Empty<object>()) : PXMessages.LocalizeFormatNoPrefix("The transaction is already matched to an expense receipt.", Array.Empty<object>());
        goto label_10;
      }
    }
    str = !row.RuleID.HasValue ? PXMessages.LocalizeFormatNoPrefix("New payment will be created for this transaction.", Array.Empty<object>()) : PXMessages.LocalizeFormatNoPrefix("New payment will be created for this transaction basing on the defined rule.", Array.Empty<object>());
label_10:
    e.ReturnValue = (object) str;
  }

  protected virtual void CABankTran_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    this.CABankTranRowUpdated(this, sender, e, (PXSelectBase<CABankTranDetail>) this.TranSplit, (PXSelectBase<CABankTranMatch>) this.TranMatch, (PXSelectBase<CABankTranAdjustment>) this.Adjustments, (PXSelectBase<CABankTranMatch>) this.TranMatchCharge, this.MatchInvoiceProcess);
  }

  public void CABankTranRowUpdated(
    CABankTransactionsMaint graph,
    PXCache sender,
    PXRowUpdatedEventArgs e,
    PXSelectBase<CABankTranDetail> tranSplit,
    PXSelectBase<CABankTranMatch> tranMatch,
    PXSelectBase<CABankTranAdjustment> adjustments,
    PXSelectBase<CABankTranMatch> tranMatchCharge,
    bool matchInvoiceProcess)
  {
    CABankTran row = (CABankTran) e.Row;
    CABankTran oldRow = (CABankTran) e.OldRow;
    if (row.CreateDocument.GetValueOrDefault())
    {
      if (oldRow.TaxCalcMode != row.TaxCalcMode || row.OrigModule == "CA" && row.EntryTypeID != oldRow.EntryTypeID)
      {
        foreach (PXResult<CABankTranDetail> pxResult in tranSplit.Select(Array.Empty<object>()))
        {
          CABankTranDetail caBankTranDetail = PXResult<CABankTranDetail>.op_Implicit(pxResult);
          tranSplit.Delete(caBankTranDetail);
        }
        if (!string.IsNullOrEmpty(row.EntryTypeID))
          tranSplit.Insert(new CABankTranDetail());
      }
      sender.SetValueExt<CABankTran.documentMatched>((object) row, (object) CABankTransactionsMaint.ValidateTranFields(graph, sender, row, adjustments));
    }
    bool? createDocument1 = row.CreateDocument;
    bool flag1 = false;
    if (createDocument1.GetValueOrDefault() == flag1 & createDocument1.HasValue)
    {
      bool? createDocument2 = oldRow.CreateDocument;
      bool? createDocument3 = row.CreateDocument;
      if (!(createDocument2.GetValueOrDefault() == createDocument3.GetValueOrDefault() & createDocument2.HasValue == createDocument3.HasValue))
        goto label_15;
    }
    if (!(row.OrigModule != "CA") || !(row.OrigModule != "EP") || !(oldRow.OrigModule == "CA") && !(oldRow.OrigModule == "EP"))
      goto label_22;
label_15:
    foreach (PXResult<CABankTranDetail> pxResult in tranSplit.Select(Array.Empty<object>()))
    {
      CABankTranDetail caBankTranDetail = PXResult<CABankTranDetail>.op_Implicit(pxResult);
      tranSplit.Delete(caBankTranDetail);
    }
label_22:
    bool? nullable1;
    if (oldRow != null)
    {
      nullable1 = oldRow.CreateDocument;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = row.CreateDocument;
        bool flag2 = false;
        if (nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue || row.OrigModule != "AR" && row.OrigModule != "AP" && row.OrigModule != "EP")
        {
          foreach (PXResult<CABankTranAdjustment> pxResult in adjustments.Select(Array.Empty<object>()))
          {
            CABankTranAdjustment bankTranAdjustment = PXResult<CABankTranAdjustment>.op_Implicit(pxResult);
            adjustments.Delete(bankTranAdjustment);
          }
        }
      }
    }
    if (oldRow != null)
    {
      nullable1 = oldRow.MultipleMatching;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = row.MultipleMatching;
        bool flag3 = false;
        if (nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue)
        {
          foreach (PXResult<CABankTranMatch> pxResult in tranMatch.Select(new object[1]
          {
            (object) row.TranID
          }))
          {
            CABankTranMatch match = PXResult<CABankTranMatch>.op_Implicit(pxResult);
            if (!string.IsNullOrEmpty(match.DocRefNbr) && !this.IsMatchedToExpenseReceipt(match))
              tranMatch.Delete(match);
          }
        }
      }
    }
    if (oldRow != null)
    {
      nullable1 = row.MatchedToInvoice;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = oldRow.MultipleMatchingToPayments;
        if (nullable1.GetValueOrDefault())
        {
          nullable1 = row.MultipleMatchingToPayments;
          bool flag4 = false;
          if (nullable1.GetValueOrDefault() == flag4 & nullable1.HasValue)
          {
            foreach (PXResult<CABankTranMatch> pxResult in tranMatch.Select(new object[1]
            {
              (object) row.TranID
            }))
            {
              CABankTranMatch caBankTranMatch = PXResult<CABankTranMatch>.op_Implicit(pxResult);
              if (caBankTranMatch.CATranID.HasValue)
                tranMatch.Delete(caBankTranMatch);
            }
          }
        }
      }
    }
    if (oldRow != null)
    {
      nullable1 = oldRow.MatchedToInvoice;
      if (nullable1.GetValueOrDefault())
      {
        int? payeeBaccountId = row.PayeeBAccountID;
        int? nullable2 = oldRow.PayeeBAccountID;
        if (!(payeeBaccountId.GetValueOrDefault() == nullable2.GetValueOrDefault() & payeeBaccountId.HasValue == nullable2.HasValue))
        {
          nullable2 = row.PayeeBAccountID;
          if (!nullable2.HasValue)
          {
            foreach (PXResult<CABankTranMatch> pxResult in tranMatch.Select(new object[1]
            {
              (object) row.TranID
            }))
            {
              CABankTranMatch match = PXResult<CABankTranMatch>.op_Implicit(pxResult);
              if (!string.IsNullOrEmpty(match.DocRefNbr) && !this.IsMatchedToExpenseReceipt(match))
                tranMatch.Delete(match);
            }
          }
          else
          {
            nullable2 = (int?) PX.Objects.CR.BAccount.PK.Find((PXGraph) graph, oldRow.PayeeBAccountID)?.ParentBAccountID;
            int? payeeBaccountIdCopy = row.PayeeBAccountIDCopy;
            if (!(nullable2.GetValueOrDefault() == payeeBaccountIdCopy.GetValueOrDefault() & nullable2.HasValue == payeeBaccountIdCopy.HasValue))
            {
              foreach (PXResult<CABankTranMatch> pxResult in tranMatch.Select(new object[1]
              {
                (object) row.TranID
              }))
              {
                CABankTranMatch caBankTranMatch = PXResult<CABankTranMatch>.op_Implicit(pxResult);
                int? referenceId = caBankTranMatch.ReferenceID;
                nullable2 = row.PayeeBAccountIDCopy;
                if (!(referenceId.GetValueOrDefault() == nullable2.GetValueOrDefault() & referenceId.HasValue == nullable2.HasValue))
                  tranMatch.Delete(caBankTranMatch);
              }
            }
          }
        }
      }
    }
    CABankTransactionsMaint.ProcessChargeOnRowUpdated(row, oldRow, tranMatchCharge, matchInvoiceProcess);
  }

  private static void ProcessChargeOnRowUpdated(
    CABankTran row,
    CABankTran oldRow,
    PXSelectBase<CABankTranMatch> tranMatchCharge,
    bool matchInvoiceProcess)
  {
    if (matchInvoiceProcess)
      return;
    if (!(row.ChargeTypeID != oldRow.ChargeTypeID))
    {
      Decimal? curyChargeAmt = oldRow.CuryChargeAmt;
      Decimal valueOrDefault1 = curyChargeAmt.GetValueOrDefault();
      curyChargeAmt = row.CuryChargeAmt;
      Decimal valueOrDefault2 = curyChargeAmt.GetValueOrDefault();
      if (!(valueOrDefault1 != valueOrDefault2))
        return;
    }
    foreach (PXResult<CABankTranMatch> pxResult in tranMatchCharge.Select(new object[1]
    {
      (object) row.TranID
    }))
    {
      CABankTranMatch caBankTranMatch = PXResult<CABankTranMatch>.op_Implicit(pxResult);
      tranMatchCharge.Delete(caBankTranMatch);
    }
    Decimal num = (Decimal) (row.ChargeDrCr == row.DrCr ? 1 : -1) * row.CuryChargeAmt.GetValueOrDefault();
    if (!(num != 0M))
      return;
    CABankTranMatch caBankTranMatch1 = new CABankTranMatch()
    {
      TranID = row.TranID,
      MatchType = "C",
      TranType = row.TranType,
      CuryApplAmt = new Decimal?(num),
      CuryApplTaxableAmt = new Decimal?(num),
      IsCharge = new bool?(true)
    };
    tranMatchCharge.Insert(caBankTranMatch1);
  }

  protected virtual void EnableTranFields(PXCache sender, CABankTran row)
  {
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    List<CABankTranMatch> list = GraphHelper.RowCast<CABankTranMatch>((IEnumerable) ((PXSelectBase<CABankTranMatch>) this.TranMatch).Select(new object[1]
    {
      (object) row.TranID
    })).ToList<CABankTranMatch>();
    if (list.Count != 0)
    {
      flag1 = list.Any<CABankTranMatch>((Func<CABankTranMatch, bool>) (match => match.CATranID.HasValue && !match.IsCharge.GetValueOrDefault() || match.DocType == "CBT"));
      flag3 = list.Any<CABankTranMatch>((Func<CABankTranMatch, bool>) (match => this.IsMatchedToExpenseReceipt(match)));
      flag2 = list.Any<CABankTranMatch>((Func<CABankTranMatch, bool>) (match => !string.IsNullOrEmpty(match.DocRefNbr) && !this.IsMatchedToExpenseReceipt(match) && match.DocType != "CBT"));
      int num = 0;
      if (flag1)
        ++num;
      if (flag3)
        ++num;
      if (flag2)
        ++num;
      if (num > 1)
        throw new PXException("The {0} bank transaction is matched to multiple documents with different types and cannot be processed. Please contact your Acumatica Support provider for assistance.", new object[1]
        {
          (object) row.TranID
        });
    }
    bool needsPMInstance = false;
    bool? nullable;
    if (row.OrigModule == "AR")
    {
      PaymentMethod paymentMethod = (PaymentMethod) PXSelectorAttribute.Select<CABankTran.paymentMethodID>(sender, (object) row);
      int num;
      if (paymentMethod != null)
      {
        nullable = paymentMethod.ARIsOnePerCustomer;
        bool flag4 = false;
        num = nullable.GetValueOrDefault() == flag4 & nullable.HasValue ? 1 : 0;
      }
      else
        num = 0;
      needsPMInstance = num != 0;
    }
    nullable = row.MultipleMatching;
    bool valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = row.MultipleMatchingToPayments;
    bool valueOrDefault2 = nullable.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CABankTran.multipleMatching>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<CABankTran.multipleMatchingToPayments>(sender, (object) row, true);
    PXUIFieldAttribute.SetEnabled<CABankTran.matchReceiptsAndDisbursements>(sender, (object) row, valueOrDefault2);
    PXUIFieldAttribute.SetEnabled<CABankTran.payeeBAccountIDCopy>(sender, (object) row, !flag1 && !flag3);
    PXUIFieldAttribute.SetVisible<CABankTran.payeeLocationIDCopy>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetEnabled<CABankTran.payeeLocationIDCopy>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetVisible<CABankTran.paymentMethodIDCopy>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetEnabled<CABankTran.paymentMethodIDCopy>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetVisible<CABankTran.pMInstanceIDCopy>(sender, (object) row, flag2 & needsPMInstance);
    PXUIFieldAttribute.SetEnabled<CABankTran.pMInstanceIDCopy>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetVisible<CABankTran.curyTotalAmtCopy>(sender, (object) row, flag2 | valueOrDefault1);
    PXUIFieldAttribute.SetVisible<CABankTran.curyTotalAmtDisplay>(sender, (object) row, flag1 | valueOrDefault2);
    PXUIFieldAttribute.SetVisible<CABankTran.curyApplAmtMatchToInvoice>(sender, (object) row, flag2 | valueOrDefault1);
    PXUIFieldAttribute.SetVisible<CABankTran.curyApplAmtMatchToPayment>(sender, (object) row, flag1 | valueOrDefault2);
    PXUIFieldAttribute.SetVisible<CABankTran.curyUnappliedBalMatchToInvoice>(sender, (object) row, flag2 | valueOrDefault1);
    PXUIFieldAttribute.SetVisible<CABankTran.curyUnappliedBalMatchToPayment>(sender, (object) row, flag1 | valueOrDefault2);
    PXCache pxCache1 = sender;
    CABankTran caBankTran1 = row;
    nullable = row.MultipleMatching;
    int num1 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<CABankTran.chargeTypeID>(pxCache1, (object) caBankTran1, num1 != 0);
    PXCache pxCache2 = sender;
    CABankTran caBankTran2 = row;
    nullable = row.MultipleMatching;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CABankTran.chargeTypeID>(pxCache2, (object) caBankTran2, num2 != 0);
    PXCache pxCache3 = sender;
    CABankTran caBankTran3 = row;
    nullable = row.MultipleMatching;
    int num3 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<CABankTran.curyChargeAmt>(pxCache3, (object) caBankTran3, num3 != 0);
    PXCache pxCache4 = sender;
    CABankTran caBankTran4 = row;
    nullable = row.MultipleMatching;
    int num4 = !nullable.GetValueOrDefault() ? 0 : (row.ChargeTypeID != null ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<CABankTran.curyChargeAmt>(pxCache4, (object) caBankTran4, num4 != 0);
    PXCache pxCache5 = sender;
    CABankTran caBankTran5 = row;
    nullable = row.MultipleMatching;
    int num5 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<CABankTran.curyChargeTaxAmt>(pxCache5, (object) caBankTran5, num5 != 0);
    int num6 = flag1 || flag2 ? 0 : (!flag3 ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<CABankTran.createDocument>(sender, (object) row, true);
    CAEntryType caEntryType = PXResultset<CAEntryType>.op_Implicit(PXSelectBase<CAEntryType, PXSelect<CAEntryType, Where<CAEntryType.entryTypeId, Equal<Required<CAEntryType.entryTypeId>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.EntryTypeID
    }));
    if (caEntryType != null)
    {
      nullable = caEntryType.UseToReclassifyPayments;
      bool valueOrDefault3 = nullable.GetValueOrDefault();
      PXUIFieldAttribute.SetEnabled<CABankTranDetail.accountID>(((PXSelectBase) this.TranSplit).Cache, (object) null, !valueOrDefault3);
      PXUIFieldAttribute.SetEnabled<CABankTranDetail.subID>(((PXSelectBase) this.TranSplit).Cache, (object) null, !valueOrDefault3);
      PXUIFieldAttribute.SetEnabled<CABankTranDetail.branchID>(((PXSelectBase) this.TranSplit).Cache, (object) null, !valueOrDefault3);
      PXUIFieldAttribute.SetEnabled<CABankTranDetail.cashAccountID>(((PXSelectBase) this.TranSplit).Cache, (object) null, valueOrDefault3);
      PXUIFieldAttribute.SetVisible<CABankTranDetail.cashAccountID>(((PXSelectBase) this.TranSplit).Cache, (object) null, valueOrDefault3);
      ((PXSelectBase) this.TranSplit).AllowInsert = true;
    }
    else
      ((PXSelectBase) this.TranSplit).AllowInsert = false;
    PXUIFieldAttribute.SetEnabled<CABankTranAdjustment.adjdCuryRate>(((PXSelectBase) this.Adjustments).Cache, (object) null, row.OrigModule == "AR" || row.OrigModule == "AP");
    this.EnableCreateTab(sender, row, needsPMInstance);
  }

  private void VerifyMatchingPaymentDate(PXCache sender, CABankTran row)
  {
    if (!row.MatchingPaymentDate.HasValue)
    {
      sender.RaiseExceptionHandling<CABankTran.matchingPaymentDate>((object) row, (object) null, (Exception) null);
    }
    else
    {
      DateTime dateTime1 = row.MatchingPaymentDate.Value;
      DateTime dateTime2 = row.TranDate.Value;
      int? nullable1 = new int?(((int?) PXResultset<CashAccount>.op_Implicit(((PXSelectBase<CashAccount>) this.cashAccount).Select(Array.Empty<object>()))?.DisbursementTranDaysBefore).GetValueOrDefault());
      DateTime dateTime3 = dateTime1;
      ref DateTime local = ref dateTime2;
      int? nullable2 = nullable1;
      double num = (double) (nullable2.HasValue ? new int?(-nullable2.GetValueOrDefault()) : new int?()).Value;
      DateTime dateTime4 = local.AddDays(num);
      if (dateTime3 < dateTime4)
      {
        sender.RaiseExceptionHandling<CABankTran.matchingPaymentDate>((object) row, (object) dateTime1, (Exception) new PXSetPropertyException("The payment date is more than {0} days earlier than the bank transaction date.", (PXErrorLevel) 2, new object[1]
        {
          (object) nullable1
        }));
      }
      else
      {
        if (!(dateTime1 == dateTime2))
          return;
        sender.RaiseExceptionHandling<CABankTran.matchingPaymentDate>((object) row, (object) dateTime1, (Exception) null);
      }
    }
  }

  private void EnableCreateTab(PXCache sender, CABankTran row, bool needsPMInstance)
  {
    bool flag1 = row != null && row.CreateDocument.GetValueOrDefault();
    bool flag2 = row != null && row.RuleApplied.GetValueOrDefault();
    bool flag3 = ((row == null ? 0 : (row.OrigModule == "AR" ? 1 : 0)) & (flag1 ? 1 : 0)) != 0;
    bool flag4 = ((row == null ? 0 : (row.OrigModule == "AP" ? 1 : 0)) & (flag1 ? 1 : 0)) != 0;
    bool flag5 = ((row == null ? 0 : (row.OrigModule == "CA" ? 1 : 0)) & (flag1 ? 1 : 0)) != 0;
    bool flag6 = flag3 | flag4;
    int? nullable = (int?) row?.CountAdjustments;
    bool flag7 = nullable.GetValueOrDefault() == 0;
    bool flag8 = row != null && row.DrCr == "D";
    PXUIFieldAttribute.SetVisible<CABankTran.ruleID>(sender, (object) row, flag1 & flag2);
    PXUIFieldAttribute.SetVisible<CABankTran.origModule>(sender, (object) row, flag1);
    PXCache pxCache = sender;
    CABankTran caBankTran = row;
    int num1;
    if (flag1 & flag7)
    {
      nullable = row.RuleID;
      num1 = !nullable.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    PXUIFieldAttribute.SetEnabled<CABankTran.origModule>(pxCache, (object) caBankTran, num1 != 0);
    PXUIFieldAttribute.SetVisible<CABankTran.entryTypeID>(sender, (object) row, flag5);
    PXUIFieldAttribute.SetEnabled<CABankTran.entryTypeID>(sender, (object) row, flag5 && !flag2);
    PXUIFieldAttribute.SetEnabled<CABankTran.taxZoneID>(sender, (object) row, flag5);
    PXUIFieldAttribute.SetEnabled<CABankTran.taxCalcMode>(sender, (object) row, flag5);
    PXUIFieldAttribute.SetVisible<CABankTran.payeeBAccountID>(sender, (object) row, flag6);
    PXUIFieldAttribute.SetEnabled<CABankTran.payeeBAccountID>(sender, (object) row, flag6 & flag7);
    PXUIFieldAttribute.SetVisible<CABankTran.payeeLocationID>(sender, (object) row, flag6);
    PXUIFieldAttribute.SetEnabled<CABankTran.payeeLocationID>(sender, (object) row, flag6);
    PXUIFieldAttribute.SetVisible<CABankTran.paymentMethodID>(sender, (object) row, flag6);
    PXUIFieldAttribute.SetEnabled<CABankTran.paymentMethodID>(sender, (object) row, flag6);
    PXUIFieldAttribute.SetVisible<CABankTran.pMInstanceID>(sender, (object) row, needsPMInstance);
    PXUIFieldAttribute.SetEnabled<CABankTran.pMInstanceID>(sender, (object) row, flag3);
    PXUIFieldAttribute.SetVisible<CABankTran.invoiceInfo>(sender, (object) row, flag6);
    PXUIFieldAttribute.SetEnabled<CABankTran.invoiceInfo>(sender, (object) row, false);
    PXUIFieldAttribute.SetVisible<CABankTran.curyTotalAmt>(sender, (object) row, flag6);
    PXUIFieldAttribute.SetVisible<CABankTran.curyDetailsWithTaxesTotal>(sender, (object) row, flag5);
    PXUIFieldAttribute.SetVisible<CABankTran.curyApplAmt>(sender, (object) row, flag6);
    PXUIFieldAttribute.SetVisible<CABankTran.curyUnappliedBal>(sender, (object) row, flag6);
    PXUIFieldAttribute.SetVisible<CABankTran.curyWOAmt>(sender, (object) row, flag3 & flag8);
    PXUIFieldAttribute.SetVisible<CABankTranAdjustment.curyAdjgWOAmt>(((PXSelectBase) this.Adjustments).Cache, (object) null, flag3 & flag8);
    PXUIFieldAttribute.SetVisible<CABankTranAdjustment.writeOffReasonCode>(((PXSelectBase) this.Adjustments).Cache, (object) null, flag3 & flag8);
    PXUIFieldAttribute.SetVisible<CABankTranAdjustment.apExtRefNbr>(((PXSelectBase) this.Adjustments).Cache, (object) null, flag4);
    PXUIFieldAttribute.SetVisible<CABankTran.curyApplAmtCA>(sender, (object) row, flag5);
    PXUIFieldAttribute.SetVisible<CABankTran.curyUnappliedBalCA>(sender, (object) row, flag5);
    PXUIFieldAttribute.SetVisible<CABankTran.curyTaxTotal>(sender, (object) row, flag5);
    PXUIFieldAttribute.SetVisible<CABankTran.userDesc>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<CABankTran.userDesc>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<GeneralInvoice.apExtRefNbr>(((PXSelectBase) this.invoices).Cache, (object) null, flag4);
    PXUIFieldAttribute.SetVisible<GeneralInvoice.arExtRefNbr>(((PXSelectBase) this.invoices).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisible<GeneralInvoice.vendorBAccountID>(((PXSelectBase) this.invoices).Cache, (object) null, flag4);
    PXUIFieldAttribute.SetVisible<GeneralInvoice.customerBAccountID>(((PXSelectBase) this.invoices).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetEnabled<CABankTran.matchingPaymentDate>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<CABankTran.matchingPaymentDate>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<CABankTran.matchingfinPeriodID>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<CABankTran.matchingfinPeriodID>(sender, (object) row, flag1);
    if (flag3 & flag8)
      PXUIFieldAttribute.SetVisible<CABankTranAdjustment.curyAdjgWhTaxAmt>(((PXSelectBase) this.Adjustments).Cache, (object) null, false);
    ((PXSelectBase) this.TranSplit).View.AllowSelect = flag5;
    ((PXSelectBase) this.Adjustments).View.AllowSelect = flag6;
    PXView view = ((PXSelectBase) this.Adjustments).View;
    int num2;
    if (row == null)
    {
      num2 = 0;
    }
    else
    {
      nullable = row.PayeeBAccountID;
      num2 = nullable.HasValue ? 1 : 0;
    }
    view.AllowInsert = num2 != 0;
  }

  public static bool ValidateTranFields(
    CABankTransactionsMaint graph,
    PXCache sender,
    CABankTran row,
    PXSelectBase<CABankTranAdjustment> adjustments)
  {
    bool valueOrDefault1 = row.CreateDocument.GetValueOrDefault();
    bool flag1 = valueOrDefault1 && row.OrigModule == "CA";
    bool flag2 = valueOrDefault1 && row.OrigModule == "AR";
    int num1 = !valueOrDefault1 ? 0 : (row.OrigModule == "AP" ? 1 : 0);
    bool valueOrDefault2 = row.MatchedToInvoice.GetValueOrDefault();
    bool? nullable1 = row.MatchedToExpenseReceipt;
    bool valueOrDefault3 = nullable1.GetValueOrDefault();
    nullable1 = row.MatchedToExisting;
    bool valueOrDefault4 = nullable1.GetValueOrDefault();
    bool isIncorrect1 = (num1 | (flag2 ? 1 : 0)) != 0 && !row.BAccountID.HasValue;
    bool isIncorrect2 = (num1 | (flag2 ? 1 : 0)) != 0 && !row.LocationID.HasValue;
    bool isIncorrect3 = (num1 | (flag2 ? 1 : 0)) != 0 && string.IsNullOrEmpty(row.PaymentMethodID);
    nullable1 = row.MatchedToInvoice;
    bool isIncorrect4 = nullable1.GetValueOrDefault() && string.IsNullOrEmpty(row.PaymentMethodIDCopy);
    bool isIncorrect5 = flag1 && string.IsNullOrEmpty(row.EntryTypeID);
    Decimal? nullable2;
    int num2;
    if (num1 != 0 && row.DrCr == "D" && row.CuryUnappliedBal.HasValue)
    {
      nullable2 = row.CuryUnappliedBal;
      Decimal num3 = 0M;
      num2 = nullable2.GetValueOrDefault() > num3 & nullable2.HasValue ? 1 : 0;
    }
    else
      num2 = 0;
    bool isIncorrect6 = num2 != 0;
    int num4;
    if (flag1)
    {
      nullable2 = row.CuryUnappliedBalCA;
      if (nullable2.HasValue)
      {
        nullable2 = row.CuryUnappliedBalCA;
        Decimal num5 = 0M;
        num4 = !(nullable2.GetValueOrDefault() == num5 & nullable2.HasValue) ? 1 : 0;
        goto label_7;
      }
    }
    num4 = 0;
label_7:
    bool flag3 = num4 != 0;
    int num6;
    if (valueOrDefault2)
    {
      nullable2 = row.CuryUnappliedBalMatch;
      if (nullable2.HasValue)
      {
        nullable2 = row.CuryUnappliedBalMatch;
        Decimal num7 = 0M;
        num6 = !(nullable2.GetValueOrDefault() == num7 & nullable2.HasValue) ? 1 : 0;
        goto label_11;
      }
    }
    num6 = 0;
label_11:
    bool flag4 = num6 != 0;
    int num8;
    if (valueOrDefault4 && !valueOrDefault2 && !valueOrDefault3)
    {
      nullable2 = row.CuryUnappliedBalMatch;
      if (nullable2.HasValue)
      {
        nullable2 = row.CuryUnappliedBalMatch;
        Decimal num9 = 0M;
        num8 = !(nullable2.GetValueOrDefault() == num9 & nullable2.HasValue) ? 1 : 0;
        goto label_15;
      }
    }
    num8 = 0;
label_15:
    bool flag5 = num8 != 0;
    bool isIncorrect7 = false;
    if (!row.PMInstanceID.HasValue & flag2)
    {
      PaymentMethod paymentMethod = PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) graph, new object[1]
      {
        (object) row.PaymentMethodID
      }));
      int num10;
      if (paymentMethod != null)
      {
        nullable1 = paymentMethod.IsAccountNumberRequired;
        num10 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
      else
        num10 = 0;
      isIncorrect7 = num10 != 0;
    }
    CABankTranAdjustment bankTranAdjustment = (CABankTranAdjustment) null;
    string str = (string) null;
    if (valueOrDefault1 && row.InvoiceInfo != null)
    {
      nullable1 = row.HasAdjustments;
      if (nullable1.GetValueOrDefault())
      {
        bankTranAdjustment = PXResultset<CABankTranAdjustment>.op_Implicit(adjustments.Search<CABankTranAdjustment.adjdRefNbr, CABankTranAdjustment.adjdModule>((object) row.InvoiceInfo, (object) row.OrigModule, Array.Empty<object>()));
      }
      else
      {
        try
        {
          ((PXGraph) graph).GetService<ICABankTransactionsRepository>().FindInvoiceByInvoiceInfo<CABankTransactionsMaint>(graph, row, out string _);
        }
        catch (Exception ex)
        {
          str = ex.Message;
        }
      }
    }
    int num11;
    if (valueOrDefault1)
    {
      nullable1 = row.InvoiceNotFound;
      num11 = nullable1.GetValueOrDefault() ? 1 : (bankTranAdjustment != null ? 0 : (row.InvoiceInfo != null ? 1 : 0));
    }
    else
      num11 = 0;
    bool flag6 = num11 != 0;
    int num12;
    if (bankTranAdjustment != null)
    {
      nullable2 = bankTranAdjustment.CuryAdjgAmt;
      Decimal? curyTotalAmt = (sender.Current as CABankTran).CuryTotalAmt;
      if (nullable2.GetValueOrDefault() == curyTotalAmt.GetValueOrDefault() & nullable2.HasValue == curyTotalAmt.HasValue)
      {
        Decimal? curyDocBal = bankTranAdjustment.CuryDocBal;
        Decimal num13 = 0M;
        num12 = !(curyDocBal.GetValueOrDefault() == num13 & curyDocBal.HasValue) ? 1 : 0;
      }
      else
        num12 = 1;
    }
    else
      num12 = 0;
    bool isIncorrect8 = num12 != 0;
    PXCache cache1 = sender;
    CABankTran row1 = row;
    int? detailErrorCount = row.DetailErrorCount;
    int num14 = 0;
    int num15 = detailErrorCount.GetValueOrDefault() > num14 & detailErrorCount.HasValue ? 1 : 0;
    object[] objArray1 = Array.Empty<object>();
    UIState.RaiseOrHideError<CABankTran.createDocument>(cache1, (object) row1, num15 != 0, "There are warnings for this transaction. Verify the transaction settings.", (PXErrorLevel) 3, objArray1);
    CABankTransactionsMaint.CheckExternalTaxProviders(sender, row);
    UIState.RaiseOrHideError<CABankTran.entryTypeID>(sender, (object) row, isIncorrect5, "Filling the Entry Type box is mandatory for creating a payment.", (PXErrorLevel) 3);
    UIState.RaiseOrHideError<CABankTran.curyUnappliedBalCA>(sender, (object) row, (flag3 ? 1 : 0) != 0, "The payment detail amount ({0:F2}) differs from the bank transaction amount ({1:F2}). To create the payment, you should add details whose total amount is equal to the bank transaction amount.", (PXErrorLevel) 3, (object) row.CuryDetailsWithTaxesTotal, (object) row.CuryTotalAmt);
    UIState.RaiseOrHideError<CABankTran.curyUnappliedBalMatchToInvoice>(sender, (object) row, (flag4 ? 1 : 0) != 0, "The total amount of the selected invoices ({0}) is not equal to the bank transaction amount ({1}). Select invoices with the total amount equal to the bank transaction amount.", (PXErrorLevel) 3, (object) row.CuryApplAmtMatch, (object) row.CuryTotalAmt);
    UIState.RaiseOrHideError<CABankTran.curyUnappliedBalMatchToPayment>(sender, (object) row, (flag5 ? 1 : 0) != 0, "The total amount of the selected invoices ({0}) is not equal to the bank transaction amount ({1}). Select invoices with the total amount equal to the bank transaction amount.", (PXErrorLevel) 3, (object) row.CuryApplAmtMatch, (object) row.CuryTotalAmt);
    UIState.RaiseOrHideError<CABankTran.payeeBAccountID>(sender, (object) row, isIncorrect1, "Filling the Business Account box is mandatory for creating a payment.", (PXErrorLevel) 3);
    UIState.RaiseOrHideError<CABankTran.payeeLocationID>(sender, (object) row, isIncorrect2, "Filling the Location box is mandatory for creating a payment.", (PXErrorLevel) 3);
    UIState.RaiseOrHideError<CABankTran.curyUnappliedBal>(sender, (object) row, isIncorrect6, "To be able to create the payment, you need to apply documents whose total amount must be equal to the payment amount.", (PXErrorLevel) 3);
    UIState.RaiseOrHideError<CABankTran.paymentMethodID>(sender, (object) row, isIncorrect3, "Filling the Payment Method box is mandatory for creating a payment.", (PXErrorLevel) 3);
    UIState.RaiseOrHideError<CABankTran.paymentMethodIDCopy>(sender, (object) row, isIncorrect4, "Filling the Payment Method box is mandatory for creating a payment.", (PXErrorLevel) 3);
    UIState.RaiseOrHideError<CABankTran.pMInstanceID>(sender, (object) row, isIncorrect7, "Filling the Payment Method box is mandatory for creating a payment.", (PXErrorLevel) 3);
    UIState.RaiseOrHideError<CABankTran.curyApplAmt>(sender, (object) row, isIncorrect8, "Please note that the application amount is different from the invoice total.", (PXErrorLevel) 2);
    PXCache cache2 = sender;
    CABankTran row2 = row;
    int num16 = flag6 ? 1 : 0;
    nullable1 = row.InvoiceNotFound;
    string message = nullable1.GetValueOrDefault() ? "Invoice No. '{0}' has not been found." : str ?? "Application of '{0}' invoice was removed by user.";
    object[] objArray2 = new object[1]
    {
      (object) row.InvoiceInfo
    };
    UIState.RaiseOrHideError<CABankTran.invoiceInfo>(cache2, (object) row2, num16 != 0, message, (PXErrorLevel) 2, objArray2);
    bool flag7 = true;
    sender.RaiseExceptionHandling<CABankTran.documentMatched>((object) row, (object) row.DocumentMatched, (Exception) null);
    Dictionary<string, string> errors1 = PXUIFieldAttribute.GetErrors(sender, (object) row, new PXErrorLevel[2]
    {
      (PXErrorLevel) 4,
      (PXErrorLevel) 5
    });
    if (errors1.Count != 0)
    {
      flag7 = false;
      sender.RaiseExceptionHandling<CABankTran.documentMatched>((object) row, (object) row.DocumentMatched, (Exception) new PXSetPropertyException(errors1.Values.First<string>(), (PXErrorLevel) 5));
    }
    else
    {
      Dictionary<string, string> errors2 = PXUIFieldAttribute.GetErrors(sender, (object) row, new PXErrorLevel[1]
      {
        (PXErrorLevel) 3
      });
      if (errors2.Count != 0)
      {
        flag7 = false;
        sender.RaiseExceptionHandling<CABankTran.documentMatched>((object) row, (object) row.DocumentMatched, (Exception) new PXSetPropertyException(errors2.Values.First<string>(), (PXErrorLevel) 3));
      }
      else
      {
        Dictionary<string, string> errors3 = PXUIFieldAttribute.GetErrors(sender, (object) row, new PXErrorLevel[1]
        {
          (PXErrorLevel) 2
        });
        if (errors3.Count != 0)
          sender.RaiseExceptionHandling<CABankTran.documentMatched>((object) row, (object) row.DocumentMatched, (Exception) new PXSetPropertyException(errors3.Values.First<string>(), (PXErrorLevel) 3));
      }
    }
    return flag7;
  }

  protected virtual void EnableManualMatchingOperations(PXCache sender, CABankTran row)
  {
    bool flag1 = this.PaymentMatchingAllowed(row);
    bool flag2 = this.InvoiceMatchingAllowed(row);
    bool flag3 = this.ExpenseReceiptMatchingAllowed(row);
    bool flag4 = this.DocumentCreationAllowed(row);
    ((PXSelectBase) this.DetailMatchesCA).AllowUpdate = flag1;
    ((PXSelectBase) this.DetailMatchingInvoices).AllowUpdate = flag2;
    ((PXSelectBase) this.ExpenseReceiptDetailMatches).AllowUpdate = flag3;
    PXUIFieldAttribute.SetEnabled<CABankTran.createDocument>(sender, (object) row, flag4);
    PXUIFieldAttribute.SetEnabled<CABankTran.multipleMatching>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetEnabled<CABankTran.multipleMatchingToPayments>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<CABankTran.payeeBAccountIDCopy>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetEnabled<CABankTran.payeeBAccountID>(sender, (object) row, flag2);
  }

  protected virtual bool PaymentMatchingAllowed(CABankTran transaction)
  {
    return CABankTranOperations.Verify.PaymentMatchingAllowed(transaction, CABankTranOperations.MatchingType.Manual);
  }

  protected virtual bool InvoiceMatchingAllowed(CABankTran transaction)
  {
    return CABankTranOperations.Verify.InvoiceMatchingAllowed(transaction, CABankTranOperations.MatchingType.Manual);
  }

  protected virtual bool ExpenseReceiptMatchingAllowed(CABankTran transaction)
  {
    return CABankTranOperations.Verify.ExpenseReceiptMatchingAllowed(transaction, CABankTranOperations.MatchingType.Manual);
  }

  protected virtual bool DocumentCreationAllowed(CABankTran transaction)
  {
    return CABankTranOperations.Verify.DocumentCreationAllowed(transaction, CABankTranOperations.MatchingType.Manual);
  }

  public static void CheckExternalTaxProviders(PXCache sender, CABankTran row)
  {
    PX.Objects.TX.TaxZone taxZone1 = PXResultset<PX.Objects.TX.TaxZone>.op_Implicit(PXSelectBase<PX.Objects.TX.TaxZone, PXViewOf<PX.Objects.TX.TaxZone>.BasedOn<SelectFromBase<PX.Objects.TX.TaxZone, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.TX.TaxZone.taxZoneID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select(sender.Graph, new object[1]
    {
      (object) row.TaxZoneID
    }));
    bool? isExternal;
    int num1;
    if (taxZone1 == null)
    {
      num1 = 0;
    }
    else
    {
      isExternal = taxZone1.IsExternal;
      num1 = isExternal.GetValueOrDefault() ? 1 : 0;
    }
    bool isIncorrect1 = num1 != 0;
    bool isIncorrect2 = false;
    if (row.ChargeTypeID != null)
    {
      string taxZoneId = PXResultset<CashAccountETDetail>.op_Implicit(PXSelectBase<CashAccountETDetail, PXViewOf<CashAccountETDetail>.BasedOn<SelectFromBase<CashAccountETDetail, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CashAccountETDetail.cashAccountID, Equal<P.AsInt>>>>>.And<BqlOperand<CashAccountETDetail.entryTypeID, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select(sender.Graph, new object[2]
      {
        (object) row.CashAccountID,
        (object) row.ChargeTypeID
      }))?.TaxZoneID;
      PX.Objects.TX.TaxZone taxZone2 = PXResultset<PX.Objects.TX.TaxZone>.op_Implicit(PXSelectBase<PX.Objects.TX.TaxZone, PXViewOf<PX.Objects.TX.TaxZone>.BasedOn<SelectFromBase<PX.Objects.TX.TaxZone, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.TX.TaxZone.taxZoneID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select(sender.Graph, new object[1]
      {
        (object) taxZoneId
      }));
      int num2;
      if (taxZone2 == null)
      {
        num2 = 0;
      }
      else
      {
        isExternal = taxZone2.IsExternal;
        num2 = isExternal.GetValueOrDefault() ? 1 : 0;
      }
      isIncorrect2 = num2 != 0;
    }
    UIState.RaiseOrHideError<CABankTran.taxZoneID>(sender, (object) row, isIncorrect1, "Taxes for an external tax provider cannot be calculated on the current form. To calculate taxes correctly, create a cash transaction on the Transactions (CA304000) form.", (PXErrorLevel) 5);
    UIState.RaiseOrHideError<CABankTran.curyTaxTotal>(sender, (object) row, isIncorrect1, "Taxes for an external tax provider cannot be calculated on the current form. To calculate taxes correctly, create a cash transaction on the Transactions (CA304000) form.", (PXErrorLevel) 5);
    UIState.RaiseOrHideError<CABankTran.chargeTypeID>(sender, (object) row, isIncorrect2, "Taxes for an external tax provider cannot be calculated on the current form. To calculate taxes correctly, create a cash transaction on the Transactions (CA304000) form.", (PXErrorLevel) 5);
  }

  protected virtual void CABankTran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    CABankTran row = (CABankTran) e.Row;
    if (!row.CashAccountID.HasValue)
      sender.RaiseExceptionHandling<CABankTran.cashAccountID>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[cashAccountID]"
      }));
    CashAccount cashAccount = ((PXSelectBase<CashAccount>) this.cashAccount).SelectSingle(Array.Empty<object>());
    CAMatchProcess caMatchProcess = CAMatchProcess.PK.Find((PXGraph) this, (int?) cashAccount?.CashAccountID);
    if ((caMatchProcess != null ? (caMatchProcess.CashAccountID.HasValue ? 1 : 0) : 0) != 0)
      throw new PXRowPersistedException(typeof (CABankTransactionsMaint.Filter.cashAccountID).Name, (object) (int?) cashAccount?.CashAccountID, "The {0} cash account is under the matching process. Your changes will be lost.", new object[1]
      {
        (object) cashAccount.CashAccountCD
      });
  }

  protected virtual void CABankTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is CABankTran row))
      return;
    CABankTransactionsMaint.Filter current = ((PXSelectBase<CABankTransactionsMaint.Filter>) this.TranFilter).Current;
    if ((current != null ? (!current.CashAccountID.HasValue ? 1 : 0) : 1) != 0)
      return;
    this.ValidateRunningMatchingProcesses(sender, row);
    CABankTransactionsMaint.ValidateTranFields(this, sender, row, (PXSelectBase<CABankTranAdjustment>) this.Adjustments);
    this.EnableTranFields(sender, row);
    this.VerifyMatchingPaymentDate(sender, row);
    this.EnableManualMatchingOperations(sender, row);
    bool? matchedToExisting = row.MatchedToExisting;
    if (!matchedToExisting.HasValue)
    {
      row.MatchedToExisting = new bool?(((PXSelectBase<CABankTranMatch>) this.TranMatch).Select(new object[1]
      {
        (object) row.TranID
      }).Count != 0);
      matchedToExisting = row.MatchedToExisting;
      if (matchedToExisting.GetValueOrDefault())
      {
        CABankTranMatch match = ((PXSelectBase<CABankTranMatch>) this.TranMatch).SelectSingle(new object[1]
        {
          (object) row.TranID
        });
        row.MatchedToExpenseReceipt = new bool?(this.IsMatchedToExpenseReceipt(match));
        row.MatchedToInvoice = new bool?(this.IsMatchedToInvoice(row, match));
        PXFormulaAttribute.CalcAggregate<CABankTranMatch.curyApplAmt>(((PXSelectBase) this.TranMatch).Cache, e.Row);
      }
    }
    StatementsMatchingProto.SetDocTypeList(((PXSelectBase) this.Adjustments).Cache, row);
    Dictionary<int, PXSetPropertyException> customInfo = PXLongOperation.GetCustomInfo(((PXGraph) this).UID) as Dictionary<int, PXSetPropertyException>;
    TimeSpan timeSpan;
    Exception exception;
    PXLongRunStatus status = PXLongOperation.GetStatus(((PXGraph) this).UID, ref timeSpan, ref exception);
    if (status != 3 && status != 2 || customInfo == null)
      return;
    int key = row.TranID.Value;
    if (!customInfo.ContainsKey(key))
      return;
    sender.RaiseExceptionHandling<CABankTran.documentMatched>((object) row, (object) row.DocumentMatched, (Exception) customInfo[key]);
  }

  private void ValidateRunningMatchingProcesses(PXCache sender, CABankTran row)
  {
    CAMatchProcess caMatchProcess = CAMatchProcess.PK.Find((PXGraph) this, row.CashAccountID);
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if ((caMatchProcess != null ? (caMatchProcess.CashAccountID.HasValue ? 1 : 0) : 0) != 0)
      propertyException = new PXSetPropertyException("The {0} cash account is under the matching process. Your changes may be lost.", (PXErrorLevel) 2, new object[1]
      {
        (object) CashAccount.PK.Find((PXGraph) this, row.CashAccountID).CashAccountCD
      });
    sender.RaiseExceptionHandling<CABankTransactionsMaint.Filter.cashAccountID>((object) row, (object) row.CashAccountID, (Exception) propertyException);
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXVendorCustomerSelectorAttribute))]
  [PXVendorCustomerSelector(typeof (CABankTran.origModule))]
  [PXRestrictor(typeof (Where<BAccountR.type, In3<BAccountType.customerType, BAccountType.combinedType, BAccountType.empCombinedType>, And<BAccountR.status, In3<CustomerStatus.active, CustomerStatus.oneTime, CustomerStatus.creditHold>, Or<BAccountR.type, In3<BAccountType.vendorType, BAccountType.employeeType, BAccountType.combinedType, BAccountType.empCombinedType>, And<BAccountR.vStatus, In3<VendorStatus.active, VendorStatus.oneTime, VendorStatus.holdPayments>>>>>), "Business account with the {0} status is not allowed.", new System.Type[] {typeof (CABankTran.payeeBAccountID)})]
  protected virtual void CABankTran_PayeeBAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXInt]
  [PXDBCalced(typeof (IsNull<CABankTran.tranID, int0>), typeof (int))]
  protected virtual void _(PX.Data.Events.CacheAttached<CABankTran.sortOrder> e)
  {
  }

  private void FieldsDisableOnProcessing(PXLongRunStatus status)
  {
    bool flag = status == 0;
    ((PXSelectBase) this.Details).Cache.AllowUpdate = flag;
    ((PXSelectBase) this.DetailMatchesCA).Cache.AllowUpdate = flag;
    ((PXSelectBase) this.DetailsForPaymentCreation).Cache.AllowUpdate = flag;
    ((PXSelectBase) this.DetailMatchingInvoices).Cache.AllowUpdate = flag;
    ((PXSelectBase) this.ExpenseReceiptDetailMatches).Cache.AllowUpdate = flag;
    ((PXSelectBase) this.Adjustments).Cache.AllowInsert = flag;
    ((PXSelectBase) this.Adjustments).Cache.AllowUpdate = flag;
    ((PXSelectBase) this.Adjustments).Cache.AllowDelete = flag;
    ((PXSelectBase) this.TranSplit).Cache.AllowInsert = flag;
    ((PXSelectBase) this.TranSplit).Cache.AllowUpdate = flag;
    ((PXSelectBase) this.TranSplit).Cache.AllowDelete = flag;
    ((PXAction) this.autoMatch).SetEnabled(flag);
    ((PXAction) this.processMatched).SetEnabled(flag);
    ((PXAction) this.matchSettingsPanel).SetEnabled(flag);
    ((PXAction) this.uploadFile).SetEnabled(flag);
    ((PXAction) this.clearMatch).SetEnabled(flag);
    ((PXAction) this.clearAllMatches).SetEnabled(flag);
    ((PXAction) this.hide).SetEnabled(flag);
  }

  protected virtual void Filter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CABankTransactionsMaint.Filter row = e.Row as CABankTransactionsMaint.Filter;
    TimeSpan timeSpan;
    Exception exception;
    PXLongRunStatus status = PXLongOperation.GetStatus(((PXGraph) this).UID, ref timeSpan, ref exception);
    PXUIFieldAttribute.SetEnabled(sender, (string) null, status == 0);
    int? cashAccountId;
    int num1;
    if (status == null && row != null)
    {
      cashAccountId = row.CashAccountID;
      num1 = cashAccountId.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    if (num1 == 0)
      PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.Details).Cache, (string) null, false);
    this.FieldsDisableOnProcessing(status);
    PXAction<CABankTransactionsMaint.Filter> autoMatch = this.autoMatch;
    cashAccountId = row.CashAccountID;
    int num2 = cashAccountId.HasValue ? 1 : 0;
    ((PXAction) autoMatch).SetEnabled(num2 != 0);
    PXAction<CABankTransactionsMaint.Filter> processMatched = this.processMatched;
    cashAccountId = row.CashAccountID;
    int num3 = cashAccountId.HasValue ? 1 : 0;
    ((PXAction) processMatched).SetEnabled(num3 != 0);
    PXAction<CABankTransactionsMaint.Filter> matchSettingsPanel = this.matchSettingsPanel;
    cashAccountId = row.CashAccountID;
    int num4 = cashAccountId.HasValue ? 1 : 0;
    ((PXAction) matchSettingsPanel).SetEnabled(num4 != 0);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<CABankTransactionsMaint.Filter.cashAccountID> e)
  {
    if (e == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CABankTransactionsMaint.Filter.cashAccountID>, object, object>) e).NewValue == null)
      return;
    CashAccount cashAccount = CashAccount.PK.Find((PXGraph) this, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CABankTransactionsMaint.Filter.cashAccountID>, object, object>) e).NewValue);
    if ((cashAccount != null ? (!cashAccount.Active.GetValueOrDefault() ? 1 : 0) : 1) != 0)
    {
      ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<CABankTransactionsMaint.Filter.cashAccountID>>) e).Cache.RaiseExceptionHandling<CABankTransactionsMaint.Filter.cashAccountID>(e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CABankTransactionsMaint.Filter.cashAccountID>, object, object>) e).NewValue, (Exception) new PXSetPropertyException("The cash account {0} is deactivated on the Cash Accounts (CA202000) form.", (PXErrorLevel) 5, new object[1]
      {
        (object) cashAccount.CashAccountCD
      }));
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CABankTransactionsMaint.Filter.cashAccountID>>) e).Cancel = true;
    }
    using (new PXReadBranchRestrictedScope())
    {
      PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this, (int?) cashAccount?.AccountID);
      if ((account != null ? (!account.IsCashAccount.GetValueOrDefault() ? 1 : 0) : 1) == 0)
        return;
      PXTrace.WriteError((Exception) new PXException("This cash account is mapped to the {0} GL account for which the Cash Account check box is cleared on the Chart of Accounts (GL202500) form.", new object[1]
      {
        (object) account.AccountCD
      }));
      ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<CABankTransactionsMaint.Filter.cashAccountID>>) e).Cache.RaiseExceptionHandling<CABankTransactionsMaint.Filter.cashAccountID>(e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CABankTransactionsMaint.Filter.cashAccountID>, object, object>) e).NewValue, (Exception) new PXSetPropertyException("This cash account is mapped to the {0} GL account for which the Cash Account check box is cleared on the Chart of Accounts (GL202500) form.", (PXErrorLevel) 5, new object[1]
      {
        (object) account.AccountCD
      }));
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CABankTransactionsMaint.Filter.cashAccountID>>) e).Cancel = true;
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<CashAccount> e)
  {
    if (e.Row == null)
      return;
    bool valueOrDefault1 = ((PXSelectBase<CABankTransactionsMaint.Filter>) this.TranFilter).Current.IsCorpCardCashAccount.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<CashAccount.curyDiffThreshold>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CashAccount>>) e).Cache, (object) null, valueOrDefault1);
    PXUIFieldAttribute.SetVisible<CashAccount.amountWeight>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CashAccount>>) e).Cache, (object) null, valueOrDefault1);
    PXUIFieldAttribute.SetVisible<CashAccount.ratioInRelevanceCalculationLabel>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CashAccount>>) e).Cache, (object) null, valueOrDefault1);
    bool valueOrDefault2 = e.Row.InvoiceFilterByDate.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<CashAccount.daysBeforeInvoiceDiscountDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CashAccount>>) e).Cache, (object) null, valueOrDefault2);
    PXUIFieldAttribute.SetEnabled<CashAccount.daysBeforeInvoiceDueDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CashAccount>>) e).Cache, (object) null, valueOrDefault2);
    PXUIFieldAttribute.SetEnabled<CashAccount.daysAfterInvoiceDueDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CashAccount>>) e).Cache, (object) null, valueOrDefault2);
    PXUIFieldAttribute.SetRequired<CashAccount.daysBeforeInvoiceDiscountDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CashAccount>>) e).Cache, valueOrDefault2);
    PXUIFieldAttribute.SetRequired<CashAccount.daysBeforeInvoiceDueDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CashAccount>>) e).Cache, valueOrDefault2);
    PXUIFieldAttribute.SetRequired<CashAccount.daysAfterInvoiceDueDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CashAccount>>) e).Cache, valueOrDefault2);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CashAccount> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<CashAccount>>) e).Cache.ObjectsEqual<CashAccount.receiptTranDaysBefore, CashAccount.receiptTranDaysAfter, CashAccount.disbursementTranDaysBefore, CashAccount.disbursementTranDaysAfter, CashAccount.allowMatchingCreditMemo, CashAccount.refNbrCompareWeight, CashAccount.matchThreshold, CashAccount.relativeMatchThreshold>((object) e.OldRow, (object) e.Row) && ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<CashAccount>>) e).Cache.ObjectsEqual<CashAccount.dateCompareWeight, CashAccount.payeeCompareWeight, CashAccount.dateMeanOffset, CashAccount.dateSigma, CashAccount.skipVoided, CashAccount.curyDiffThreshold, CashAccount.amountWeight, CashAccount.emptyRefNbrMatching>((object) e.OldRow, (object) e.Row) && ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<CashAccount>>) e).Cache.ObjectsEqual<CashAccount.invoiceFilterByCashAccount, CashAccount.invoiceFilterByDate, CashAccount.daysAfterInvoiceDueDate, CashAccount.daysBeforeInvoiceDiscountDate, CashAccount.daysBeforeInvoiceDueDate, CashAccount.invoiceRefNbrCompareWeight, CashAccount.invoiceDateCompareWeight, CashAccount.invoicePayeeCompareWeight>((object) e.OldRow, (object) e.Row) && ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<CashAccount>>) e).Cache.ObjectsEqual<CashAccount.averagePaymentDelay, CashAccount.invoiceDateSigma, CashAccount.allowMatchingDebitAdjustment>((object) e.OldRow, (object) e.Row))
      return;
    e.Row.MatchSettingsPerAccount = new bool?(true);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CashAccount.invoiceFilterByDate> e)
  {
    CashAccount row = (CashAccount) e.Row;
    if (row == null || row.InvoiceFilterByDate.GetValueOrDefault())
      return;
    row.DaysBeforeInvoiceDiscountDate = new int?(0);
    row.DaysBeforeInvoiceDueDate = new int?(0);
    row.DaysAfterInvoiceDueDate = new int?(0);
  }

  protected virtual void CATranExt_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CA.BankStatementHelpers.CATranExt row))
      return;
    PXUIFieldAttribute.SetVisible<CATran.finPeriodID>(sender, (object) null, false);
    PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.BankStatementHelpers.CATranExt.isMatched>(sender, (object) row, true);
  }

  protected virtual void CATranExt_IsMatched_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    object row = e.Row;
    if (!((bool?) e.NewValue).GetValueOrDefault())
      return;
    if (this.CreatePaymentIsChosen(((PXSelectBase<CABankTran>) this.Details).Current))
      throw new PXSetPropertyException("The Create check box is selected for the bank transaction on the Create Payment tab. To match the bank transaction to another document, go to the Create Payment tab and clear the Create check box.", (PXErrorLevel) 3);
    if (!((PXSelectBase<CABankTran>) this.Details).Current.DocumentMatched.GetValueOrDefault())
      return;
    if (((PXSelectBase<CABankTran>) this.Details).Current.MatchedToExpenseReceipt.GetValueOrDefault())
      throw new PXSetPropertyException("The bank transaction is already matched to an expense receipt. To match the bank transaction to another document, open the Match to Expense Receipts tab and unmatch the transaction.", (PXErrorLevel) 3);
    if (((PXSelectBase<CABankTran>) this.Details).Current.MatchedToInvoice.GetValueOrDefault())
      throw new PXSetPropertyException("The bank transaction is already matched to an invoice. To match the bank transaction to another document, go to the Match to Invoices tab and unmatch the transaction.", (PXErrorLevel) 3);
    if (!((PXSelectBase<CABankTran>) this.Details).Current.MultipleMatchingToPayments.GetValueOrDefault())
      throw new PXSetPropertyException("Another option is already chosen", (PXErrorLevel) 3);
  }

  protected virtual void CATranExt_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    PX.Objects.CA.BankStatementHelpers.CATranExt row = e.Row as PX.Objects.CA.BankStatementHelpers.CATranExt;
    CABankTran current = ((PXSelectBase<CABankTran>) this.Details).Current;
    if (!sender.ObjectsEqual<PX.Objects.CA.BankStatementHelpers.CATranExt.isMatched>(e.Row, e.OldRow))
    {
      bool? nullable = row.IsMatched;
      if (nullable.GetValueOrDefault())
      {
        nullable = current.CreateDocument;
        if (nullable.GetValueOrDefault())
          ((PXSelectBase) this.Details).Cache.SetValueExt<CABankTran.createDocument>((object) current, (object) false);
        CABankTranMatch caBankTranMatch1;
        if (row.OrigTranType == "CBT")
        {
          CABankTranMatch caBankTranMatch2 = new CABankTranMatch();
          caBankTranMatch2.TranID = current.TranID;
          caBankTranMatch2.TranType = current.TranType;
          caBankTranMatch2.DocModule = row.OrigModule;
          caBankTranMatch2.DocType = "CBT";
          caBankTranMatch2.DocRefNbr = row.OrigRefNbr;
          caBankTranMatch2.ReferenceID = row.ReferenceID;
          Decimal num = (Decimal) (current.DrCr == "D" ? 1 : -1);
          Decimal? curyTranAmt = row.CuryTranAmt;
          caBankTranMatch2.CuryApplAmt = curyTranAmt.HasValue ? new Decimal?(num * curyTranAmt.GetValueOrDefault()) : new Decimal?();
          caBankTranMatch1 = caBankTranMatch2;
        }
        else
        {
          CABankTranMatch caBankTranMatch3 = new CABankTranMatch();
          caBankTranMatch3.TranID = current.TranID;
          caBankTranMatch3.TranType = current.TranType;
          caBankTranMatch3.CATranID = row.TranID;
          caBankTranMatch3.ReferenceID = row.ReferenceID;
          Decimal num = (Decimal) (current.DrCr == "D" ? 1 : -1);
          Decimal? curyTranAmt = row.CuryTranAmt;
          caBankTranMatch3.CuryApplAmt = curyTranAmt.HasValue ? new Decimal?(num * curyTranAmt.GetValueOrDefault()) : new Decimal?();
          caBankTranMatch1 = caBankTranMatch3;
        }
        ((PXSelectBase<CABankTranMatch>) this.TranMatch).Insert(caBankTranMatch1);
      }
      else
      {
        foreach (CABankTranMatch caBankTranMatch in GraphHelper.RowCast<CABankTranMatch>((IEnumerable) ((PXSelectBase<CABankTranMatch>) this.TranMatch).Select(new object[1]
        {
          (object) current.TranID
        })).Where<CABankTranMatch>((Func<CABankTranMatch, bool>) (item =>
        {
          long? caTranId = item.CATranID;
          long? tranId = row.TranID;
          return caTranId.GetValueOrDefault() == tranId.GetValueOrDefault() & caTranId.HasValue == tranId.HasValue || this.IsCABatch(row, item);
        })))
          ((PXSelectBase<CABankTranMatch>) this.TranMatch).Delete(caBankTranMatch);
      }
      bool flag = NonGenericIEnumerableExtensions.Any_((IEnumerable) ((PXSelectBase<CABankTranMatch>) this.TranMatch).Select(new object[1]
      {
        (object) current.TranID
      }));
      ((PXSelectBase) this.Details).Cache.SetValueExt<CABankTran.documentMatched>((object) current, (object) flag);
      ((PXSelectBase) this.Details).Cache.SetStatus((object) ((PXSelectBase<CABankTran>) this.Details).Current, (PXEntryStatus) 1);
    }
    sender.IsDirty = false;
  }

  protected virtual void CATranExt_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CABankTranDetail_AccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    CABankTranDetail row = e.Row as CABankTranDetail;
    CABankTran current = ((PXSelectBase<CABankTran>) this.Details).Current;
    if (current == null || current.EntryTypeID == null || row == null)
      return;
    e.NewValue = (object) this.GetDefaultAccountValues((PXGraph) this, current.CashAccountID, current.EntryTypeID).AccountID;
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
    CABankTran current = ((PXSelectBase<CABankTran>) this.Details).Current;
    CABankTranDetail row = e.Row as CABankTranDetail;
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
    CABankTran current = ((PXSelectBase<CABankTran>) this.Details).Current;
    if (row == null || !row.InventoryID.HasValue)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<CABankTranDetail.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.InventoryID
    }));
    if (inventoryItem != null && current != null)
    {
      if (current.DrCr == "D")
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
    PX.Objects.TX.TaxZone taxZone = PXResultset<PX.Objects.TX.TaxZone>.op_Implicit(((PXSelectBase<PX.Objects.TX.TaxZone>) this.Taxzone).Select(Array.Empty<object>()));
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
    CABankTran current = ((PXSelectBase<CABankTran>) this.Details).Current;
    if (current == null || current.EntryTypeID == null || row == null)
      return;
    e.NewValue = (object) this.GetDefaultAccountValues((PXGraph) this, current.CashAccountID, current.EntryTypeID).SubID;
    ((CancelEventArgs) e).Cancel = e.NewValue != null;
  }

  protected virtual void CABankTranDetail_TranDesc_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    object row = e.Row;
    CABankTran current = ((PXSelectBase<CABankTran>) this.Details).Current;
    if (current == null || current.EntryTypeID == null)
      return;
    CAEntryType caEntryType = PXResultset<CAEntryType>.op_Implicit(PXSelectBase<CAEntryType, PXSelect<CAEntryType, Where<CAEntryType.entryTypeId, Equal<Required<CAEntryType.entryTypeId>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) current.EntryTypeID
    }));
    if (caEntryType == null)
      return;
    e.NewValue = (object) caEntryType.Descr;
  }

  [PXMergeAttributes]
  [CABankTranTax(typeof (CABankTran), typeof (CABankTax), typeof (CABankTaxTran), typeof (CABankTran.taxCalcMode), null, CuryOrigDocAmt = typeof (CABankTran.curyTranAmt), CuryLineTotal = typeof (CABankTran.curyApplAmtCA))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CABankTranDetail.taxCategoryID> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CABankTaxTran, CABankTaxTran.taxType> e)
  {
    if (e.Row == null || ((PXSelectBase<CABankTran>) this.Details).Current == null)
      return;
    if (((PXSelectBase<CABankTran>) this.Details).Current.DrCr == "C")
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
    if (e.Row == null || ((PXSelectBase<CABankTran>) this.Details).Current == null)
      return;
    if (((PXSelectBase<CABankTran>) this.Details).Current.DrCr == "C")
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
    if (e.Row == null || ((PXSelectBase<CABankTran>) this.Details).Current == null)
      return;
    if (((PXSelectBase<CABankTran>) this.Details).Current.DrCr == "C")
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
    CABankTran caBankTran = (CABankTran) PXParentAttribute.SelectParent(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CABankTaxTran>>) e).Cache, (object) e.Row);
    if (caBankTran != null && (e.Operation == 2 || e.Operation == 1))
    {
      e.Row.TaxZoneID = caBankTran.TaxZoneID;
      PXDefaultAttribute.SetPersistingCheck<CABankTaxTran.taxZoneID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CABankTaxTran>>) e).Cache, (object) e.Row, (PXPersistingCheck) 1);
    }
    else
      PXDefaultAttribute.SetPersistingCheck<CABankTaxTran.taxZoneID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CABankTaxTran>>) e).Cache, (object) e.Row, (PXPersistingCheck) 2);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CABankTran, CABankTran.chargeTaxCalcMode> e)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>())
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTran, CABankTran.chargeTaxCalcMode>, CABankTran, object>) e).NewValue = (object) "G";
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CABankTran, CABankTran.chargeTaxCalcMode>, CABankTran, object>) e).NewValue = (object) "T";
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CABankTaxTranMatch, CABankTaxTranMatch.taxType> e)
  {
    if (e.Row == null || ((PXSelectBase<CABankTran>) this.Details).Current == null)
      return;
    if (((PXSelectBase<CABankTran>) this.Details).Current.DrCr == "C")
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
    if (e.Row == null || ((PXSelectBase<CABankTran>) this.Details).Current == null)
      return;
    if (((PXSelectBase<CABankTran>) this.Details).Current.DrCr == "C")
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
    if (e.Row == null || ((PXSelectBase<CABankTran>) this.Details).Current == null)
      return;
    if (((PXSelectBase<CABankTran>) this.Details).Current.DrCr == "C")
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
    CABankTran current = ((PXSelectBase<CABankTran>) this.Details).Current;
    CABankTranDetail row = e.Row as CABankTranDetail;
    row.Qty = new Decimal?(1.0M);
    Decimal num1 = current.DrCr == "D" ? 1M : -1M;
    Decimal? nullable1 = current.CuryTranAmt;
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(num1 * nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable3 = new Decimal?(0M);
    foreach (PXResult<CABankTaxTran, PX.Objects.TX.Tax> pxResult in ((PXSelectBase<CABankTaxTran>) this.TaxTrans).Select(Array.Empty<object>()))
    {
      CABankTaxTran caBankTaxTran = PXResult<CABankTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult);
      PX.Objects.TX.Tax tax = PXResult<CABankTaxTran, PX.Objects.TX.Tax>.op_Implicit(pxResult);
      if ((PXAccess.FeatureInstalled<FeaturesSet.netGrossEntryMode>() && !(current.TaxCalcMode == "T") || !(tax.TaxCalcLevel != "0") ? (current.TaxCalcMode == "N" ? 1 : 0) : 1) != 0)
      {
        nullable1 = nullable3;
        Decimal? curyTaxAmt = caBankTaxTran.CuryTaxAmt;
        Decimal? nullable4 = caBankTaxTran.CuryExpenseAmt;
        Decimal? nullable5 = curyTaxAmt.HasValue & nullable4.HasValue ? new Decimal?(curyTaxAmt.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable6;
        if (!(nullable1.HasValue & nullable5.HasValue))
        {
          nullable4 = new Decimal?();
          nullable6 = nullable4;
        }
        else
          nullable6 = new Decimal?(nullable1.GetValueOrDefault() + nullable5.GetValueOrDefault());
        nullable3 = nullable6;
      }
    }
    Decimal? nullable7 = nullable2;
    Decimal? nullable8 = current.CuryApplAmtCA;
    Decimal? nullable9 = nullable7.HasValue & nullable8.HasValue ? new Decimal?(nullable7.GetValueOrDefault() - nullable8.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable10 = nullable3;
    Decimal? nullable11;
    if (!(nullable9.HasValue & nullable10.HasValue))
    {
      nullable8 = new Decimal?();
      nullable11 = nullable8;
    }
    else
      nullable11 = new Decimal?(nullable9.GetValueOrDefault() - nullable10.GetValueOrDefault());
    Decimal? nullable12 = nullable11;
    CABankTranDetail caBankTranDetail1 = row;
    Decimal? nullable13 = nullable12;
    Decimal num2 = 0M;
    Decimal? nullable14 = nullable13.GetValueOrDefault() > num2 & nullable13.HasValue ? nullable12 : new Decimal?(0M);
    caBankTranDetail1.CuryUnitPrice = nullable14;
    PXCache pxCache = sender;
    CABankTranDetail caBankTranDetail2 = row;
    nullable13 = row.Qty;
    nullable9 = row.CuryUnitPrice;
    Decimal? nullable15;
    if (!(nullable13.HasValue & nullable9.HasValue))
    {
      nullable8 = new Decimal?();
      nullable15 = nullable8;
    }
    else
      nullable15 = new Decimal?(nullable13.GetValueOrDefault() * nullable9.GetValueOrDefault());
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) nullable15;
    pxCache.SetValueExt<CABankTranDetail.curyTranAmt>((object) caBankTranDetail2, (object) local);
    row.TranDesc = current.UserDesc;
    CATranDetailHelper.VerifyOffsetCashAccount(sender, (ICATranDetail) row, (int?) ((PXSelectBase<CABankTranDetail>) this.TranSplit).Current?.CashAccountID);
  }

  protected virtual void CABankTranDetail_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    CATranDetailHelper.OnCATranDetailRowUpdatingEvent(sender, e);
    if (!CATranDetailHelper.VerifyOffsetCashAccount(sender, (ICATranDetail) (e.NewRow as CABankTranDetail), (int?) ((PXSelectBase<CABankTransactionsMaint.Filter>) this.TranFilter).Current?.CashAccountID))
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  private static PXSetPropertyException GetTranDetailError(
    PXCache sender,
    CABankTranDetail tranDetail)
  {
    sender.GetStateExt<CABankTranDetail.accountID>((object) tranDetail);
    PXSetPropertyException tranDetailError = (PXSetPropertyException) null;
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    bool flag = true;
    try
    {
      AccountAttribute.VerifyAccountIsNotControl((PX.Objects.GL.Account) PXSelectorAttribute.Select<CABankTranDetail.accountID>(sender, (object) tranDetail));
    }
    catch (PXSetPropertyException ex)
    {
      flag = false;
      propertyException = ex;
    }
    PXEntryStatus status = sender.GetStatus((object) tranDetail);
    if (status != 4 && status != 3)
    {
      int? nullable = tranDetail.AccountID;
      if (!nullable.HasValue)
      {
        tranDetailError = new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 3, new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<CABankTranDetail.accountID>(sender)
        });
      }
      else
      {
        nullable = tranDetail.SubID;
        if (!nullable.HasValue)
          tranDetailError = new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 3, new object[1]
          {
            (object) PXUIFieldAttribute.GetDisplayName<CABankTranDetail.subID>(sender)
          });
        else if (!flag)
          tranDetailError = propertyException;
      }
    }
    return tranDetailError;
  }

  protected virtual void _(PX.Data.Events.RowInserted<CABankTranDetail> e)
  {
    this.RaiseOrHideParentWarning(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CABankTranDetail> e)
  {
    this.RaiseOrHideParentWarning(e.Row);
  }

  private void RaiseOrHideParentWarning(CABankTranDetail detail)
  {
    CABankTran row = PXResultset<CABankTran>.op_Implicit(PXSelectBase<CABankTran, PXViewOf<CABankTran>.BasedOn<SelectFromBase<CABankTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CABankTran.tranID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) detail.BankTranID
    }));
    PXSetPropertyException detailsError = CABankTransactionsMaint.GetDetailsError(this, row);
    row.DetailErrorCount = new int?(detailsError != null ? 1 : 0);
    ((PXSelectBase) this.Details).Cache.Update((object) row);
  }

  protected virtual void _(PX.Data.Events.RowDeleted<CABankTranDetail> e)
  {
    this.RaiseOrHideParentWarning(e.Row);
  }

  private static PXSetPropertyException GetDetailsError(
    CABankTransactionsMaint graph,
    CABankTran row)
  {
    List<object> objectList = ((PXSelectBase) graph.TranSplit).View.SelectMultiBound(new object[1]
    {
      (object) row
    }, Array.Empty<object>());
    foreach (CABankTranDetail tranDetail in objectList)
    {
      PXSetPropertyException tranDetailError = CABankTransactionsMaint.GetTranDetailError(((PXSelectBase) graph.TranSplit).Cache, tranDetail);
      if (tranDetailError != null)
        return tranDetailError;
    }
    return (PXSetPropertyException) null;
  }

  protected virtual void CABankTranDetail_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CABankTranDetail row = (CABankTranDetail) e.Row;
    if (row == null)
      return;
    PXSetPropertyException tranDetailError = CABankTransactionsMaint.GetTranDetailError(sender, row);
    sender.RaiseExceptionHandling<CABankTranDetail.accountID>((object) row, (object) row.AccountID, (Exception) tranDetailError);
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
  [CABankTranMatchTax(typeof (CABankTran), typeof (CABankChargeTax), typeof (CABankTaxTranMatch), typeof (CABankTran.chargeTaxCalcMode), null, CuryOrigDocAmt = typeof (CABankTran.curyChargeAmt), CuryLineTotal = typeof (CABankTran.curyChargeAmt), TaxZoneID = typeof (CABankTran.chargeTaxZoneID))]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  [PXDefault(typeof (Search<PX.Objects.TX.TaxZone.dfltTaxCategoryID, Where<PX.Objects.TX.TaxZone.taxZoneID, Equal<Current<CABankTran.chargeTaxZoneID>>, And<Current<CABankTranMatch.matchType>, Equal<CABankTranMatch.matchType.charge>, And<Current<CABankTranMatch.cATranID>, IsNull>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CABankTranMatch.taxCategoryID> e)
  {
  }

  protected virtual void CABankTranMatch_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    CABankTran caBankTran = PXResultset<CABankTran>.op_Implicit(PXSelectBase<CABankTran, PXSelect<CABankTran, Where<CABankTran.tranID, Equal<Required<CABankTran.tranID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) (e.Row as CABankTranMatch).TranID
    }));
    ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.matchedToExisting>((object) caBankTran, (object) null);
    ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.matchedToInvoice>((object) caBankTran, (object) null);
    ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.matchedToExpenseReceipt>((object) caBankTran, (object) null);
  }

  protected virtual void CABankTranMatch_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    CABankTran caBankTran = PXResultset<CABankTran>.op_Implicit(PXSelectBase<CABankTran, PXSelect<CABankTran, Where<CABankTran.tranID, Equal<Required<CABankTran.tranID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) (e.Row as CABankTranMatch).TranID
    }));
    ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.matchedToExisting>((object) caBankTran, (object) null);
    ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.matchedToInvoice>((object) caBankTran, (object) null);
    ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.matchedToExpenseReceipt>((object) caBankTran, (object) null);
  }

  protected virtual void CABankTranMatch_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Operation != 2)
      return;
    CABankTranMatch row = (CABankTranMatch) e.Row;
    if (!((IEnumerable<PXResult<CABankTranMatch>>) PXSelectBase<CABankTranMatch, PXSelectReadonly2<CABankTranMatch, InnerJoin<CABankTran, On<CABankTran.tranID, Equal<CABankTranMatch.tranID>>>, Where<CABankTranMatch.cATranID, Equal<Required<CABankTranMatch.cATranID>>, And<CABankTran.tranType, Equal<Current<CABankTran.tranType>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.CATranID
    })).AsEnumerable<PXResult<CABankTranMatch>>().Any<PXResult<CABankTranMatch>>((Func<PXResult<CABankTranMatch>, bool>) (bankTran => ((PXGraph) this).Caches["CABankTranMatch"].GetStatus((object) PXResult<CABankTranMatch>.op_Implicit(bankTran)) != 3)))
      return;
    CABankTran caBankTran = PXResultset<CABankTran>.op_Implicit(PXSelectBase<CABankTran, PXSelect<CABankTran, Where<CABankTran.tranID, Equal<Required<CABankTranMatch.tranID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.TranID
    }));
    ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<CABankTran.extRefNbr>((object) caBankTran, (object) caBankTran.ExtRefNbr, (Exception) new PXSetPropertyException("This document has been already matched. Refresh the page to update the data.", (PXErrorLevel) 5));
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CA.Light.BAccount.acctCD> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CA.Light.BAccount.acctName> e)
  {
  }

  public static void DoProcessing(
    IEnumerable<CABankTran> list,
    Dictionary<int, PXSetPropertyException> listMessages)
  {
    CABankTransactionsMaint instance1 = PXGraph.CreateInstance<CABankTransactionsMaint>();
    bool flag1 = false;
    List<PX.Objects.GL.Batch> externalPostList = new List<PX.Objects.GL.Batch>();
    foreach (CABankTran caBankTran1 in list)
    {
      PXSetPropertyException error = (PXSetPropertyException) null;
      ((PXGraph) instance1).Clear();
      bool? nullable = caBankTran1.DocumentMatched;
      bool flag2 = false;
      if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
      {
        CABankTran caBankTran2 = (CABankTran) ((PXSelectBase) instance1.Details).Cache.CreateCopy((object) caBankTran1);
        ((PXSelectBase<CABankTran>) instance1.Details).Current = caBankTran2;
        ((PXSelectBase<CABankTran>) instance1.DetailsForPaymentCreation).Current = caBankTran2;
        CABankTransactionsMaint.Filter current = ((PXSelectBase<CABankTransactionsMaint.Filter>) instance1.TranFilter).Current;
        ((PXSelectBase<CABankTransactionsMaint.Filter>) instance1.TranFilter).SetValueExt<CABankTransactionsMaint.Filter.cashAccountID>(current, (object) caBankTran2.CashAccountID);
        ((PXSelectBase<CABankTransactionsMaint.Filter>) instance1.TranFilter).SetValueExt<CABankTransactionsMaint.Filter.tranType>(current, (object) caBankTran2.TranType);
        try
        {
          CABankTranMatch match = ((PXSelectBase<CABankTranMatch>) instance1.TranMatch).SelectSingle(new object[1]
          {
            (object) caBankTran2.TranID
          });
          using (PXTransactionScope transactionScope = new PXTransactionScope())
          {
            instance1.ValidateBeforeProcessing(caBankTran1);
            if (match != null && instance1.IsMatchedToExpenseReceipt(match))
            {
              instance1.MatchExpenseReceipt(caBankTran1, match);
            }
            else
            {
              if (match != null && !string.IsNullOrEmpty(match.DocRefNbr) && match.DocType != "CBT")
              {
                caBankTran2 = instance1.MatchInvoices(caBankTran2);
                if (caBankTran2.ChargeTypeID != null)
                  caBankTran2 = instance1.CreateChargesProc(caBankTran2);
              }
              else
              {
                caBankTran2.ChargeTypeID = (string) null;
                caBankTran2 = ((PXSelectBase<CABankTran>) instance1.Details).Update(caBankTran2);
              }
              nullable = caBankTran2.CreateDocument;
              if (nullable.GetValueOrDefault())
                instance1.CreateDocumentProc(caBankTran2, false);
              instance1.MatchCATran(caBankTran2, externalPostList, out error);
            }
            caBankTran2 = (CABankTran) ((PXSelectBase) instance1.Details).Cache.CreateCopy((object) caBankTran2);
            caBankTran2.Processed = new bool?(error == null);
            caBankTran2.DocumentMatched = new bool?(true);
            caBankTran2.RuleID = caBankTran1.RuleID;
            ((PXSelectBase<CABankTran>) instance1.Details).Update(caBankTran2);
            ((PXAction) instance1.Save).Press();
            transactionScope.Complete((PXGraph) instance1);
          }
          listMessages[caBankTran2.TranID.Value] = error ?? new PXSetPropertyException("Bank transaction was processed", (PXErrorLevel) 1);
          flag1 = flag1 || error != null && error.ErrorLevel == 5;
        }
        catch (PXOuterException ex)
        {
          listMessages[caBankTran2.TranID.Value] = new PXSetPropertyException((Exception) ex, (PXErrorLevel) 5, $"{((Exception) ex).Message} {ex.InnerMessages[0]}", Array.Empty<object>());
          flag1 = true;
        }
        catch (Exception ex)
        {
          listMessages[caBankTran2.TranID.Value] = new PXSetPropertyException(ex, (PXErrorLevel) 5, ex.Message, Array.Empty<object>());
          flag1 = true;
        }
      }
    }
    List<PX.Objects.GL.Batch> batchList = new List<PX.Objects.GL.Batch>();
    if (externalPostList.Count > 0)
    {
      PostGraph instance2 = PXGraph.CreateInstance<PostGraph>();
      foreach (PX.Objects.GL.Batch b in externalPostList)
      {
        try
        {
          ((PXGraph) instance2).Clear();
          instance2.PostBatchProc(b);
        }
        catch (Exception ex)
        {
          batchList.Add(b);
        }
      }
    }
    if (batchList.Count > 0)
      throw new PXException("Documents were successfully created, but {0} of {1} were not posted", new object[2]
      {
        (object) batchList.Count,
        (object) externalPostList.Count
      });
    if (flag1)
      throw new PXException("Not all records have been processed, please review");
  }

  public static void DoProcessing(IEnumerable<CABankTran> list)
  {
    Dictionary<int, PXSetPropertyException> listMessages = new Dictionary<int, PXSetPropertyException>();
    PXLongOperation.SetCustomInfo((object) listMessages, Enumerable.Cast<object>(list).ToArray<object>());
    CABankTransactionsMaint.DoProcessing(list, listMessages);
  }

  protected virtual void VerifyBeforeMatchInvoices(CABankTran det)
  {
    if (det.CuryUnappliedBalMatch.GetValueOrDefault() != 0M)
      throw new PXSetPropertyException("The total amount of the selected invoices ({0}) is not equal to the bank transaction amount ({1}). Select invoices with the total amount equal to the bank transaction amount.", new object[2]
      {
        (object) det.CuryApplAmtMatch,
        (object) det.CuryTotalAmt
      });
  }

  protected virtual void VerifyBeforeMatchExpenseReceipt(
    CABankTran det,
    EPExpenseClaimDetails receipt)
  {
    if ((receipt != null ? (!receipt.Approved.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      throw new PXException("The document has the Pending Approval status and cannot be released. The document must be approved by a responsible person before it can be released.");
  }

  protected virtual void VerifyBeforeMatchCATran(CABankTran det)
  {
    if (det.CuryUnappliedBalMatch.GetValueOrDefault() != 0M && det.DocumentMatched.GetValueOrDefault())
      throw new PXSetPropertyException("The total amount of the selected payments ({0}) is not equal to the bank transaction amount ({1}). Select payments with the total amount equal to the bank transaction amount.", new object[2]
      {
        (object) det.CuryApplAmtMatch,
        (object) det.CuryTotalAmt
      });
  }

  protected virtual void ValidateBeforeProcessing(CABankTran det)
  {
  }

  protected virtual CABankTran MatchInvoices(CABankTran det)
  {
    this.VerifyBeforeMatchInvoices(det);
    List<CABankTranMatch> list = GraphHelper.RowCast<CABankTranMatch>((IEnumerable) ((PXSelectBase<CABankTranMatch>) this.TranMatch).Select(new object[1]
    {
      (object) det.TranID
    })).ToList<CABankTranMatch>();
    int? baccountId = det.BAccountID;
    int? locationId = det.LocationID;
    string origModule = det.OrigModule;
    string paymentMethodId = det.PaymentMethodID;
    string chargeTypeId = det.ChargeTypeID;
    Decimal? curyChargeAmt = det.CuryChargeAmt;
    this.MatchInvoicePreContext(det);
    using (new CABankTransactionsMaint.MatchInvoiceContext(this))
    {
      this.MatchInvoiceContextStart(det);
      this.ClearMatchProc(det);
      ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.createDocument>((object) det, (object) true);
      this.ClearRule(((PXSelectBase) this.Details).Cache, det);
      foreach (PXResult<CABankTranAdjustment> pxResult in ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Select(new object[1]
      {
        (object) det.TranID
      }))
        ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Delete(PXResult<CABankTranAdjustment>.op_Implicit(pxResult));
      ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.origModule>((object) det, (object) origModule);
      ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.payeeBAccountID>((object) det, (object) baccountId);
      ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.payeeLocationID>((object) det, (object) locationId);
      ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.paymentMethodID>((object) det, (object) paymentMethodId);
      ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.chargeTypeID>((object) det, (object) chargeTypeId);
      ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.curyChargeAmt>((object) det, (object) curyChargeAmt);
      ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.histMatchedToInvoice>((object) det, (object) true);
      this.MatchInvoiceContextEnd(det);
      det = ((PXSelectBase<CABankTran>) this.Details).Update(det);
    }
    foreach (CABankTranMatch caBankTranMatch in list)
    {
      try
      {
        CABankTranAdjustment bankTranAdjustment = ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Insert(new CABankTranAdjustment()
        {
          TranID = det.TranID
        });
        bankTranAdjustment.AdjdDocType = caBankTranMatch.DocType;
        bankTranAdjustment.AdjdRefNbr = caBankTranMatch.DocRefNbr;
        bankTranAdjustment.AdjdModule = caBankTranMatch.DocModule;
        bankTranAdjustment.CuryAdjgAmt = new Decimal?(Math.Abs(caBankTranMatch.CuryApplAmt.GetValueOrDefault()));
        ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Update(bankTranAdjustment);
      }
      catch
      {
        throw new PXSetPropertyException("Could not add application of '{0}' invoice. Possibly it is already used in another application", new object[1]
        {
          (object) caBankTranMatch.DocRefNbr
        });
      }
    }
    return det;
  }

  protected virtual void MatchInvoicePreContext(CABankTran caBankTran)
  {
  }

  protected virtual void MatchInvoiceContextStart(CABankTran caBankTran)
  {
  }

  protected virtual void MatchInvoiceContextEnd(CABankTran caBankTran)
  {
  }

  protected virtual void MatchCATran(CABankTran det, List<PX.Objects.GL.Batch> externalPostList)
  {
    this.MatchCATran(det, externalPostList, out PXSetPropertyException _);
  }

  protected virtual void MatchCATran(
    CABankTran det,
    List<PX.Objects.GL.Batch> externalPostList,
    out PXSetPropertyException error)
  {
    error = (PXSetPropertyException) null;
    this.VerifyBeforeMatchCATran(det);
    bool flag = false;
    foreach (PXResult<CABankTranMatch> pxResult in ((PXSelectBase<CABankTranMatch>) this.TranMatch).Select(new object[1]
    {
      (object) det.TranID
    }))
    {
      CABankTranMatch match = PXResult<CABankTranMatch>.op_Implicit(pxResult);
      flag = this.ProcessCABankTranMatches(det, externalPostList, match, out error);
    }
    foreach (PXResult<CABankTranMatch> pxResult in ((PXSelectBase<CABankTranMatch>) this.TranMatchCharge).Select(new object[1]
    {
      (object) det.TranID
    }))
    {
      CABankTranMatch match = PXResult<CABankTranMatch>.op_Implicit(pxResult);
      flag = this.ProcessCABankTranMatches(det, externalPostList, match, out error);
    }
    if (!flag)
      throw new PXException("Match for transaction '{0}' was not found", new object[1]
      {
        (object) det.TranID
      });
  }

  private bool ProcessCABankTranMatches(
    CABankTran det,
    List<PX.Objects.GL.Batch> externalPostList,
    CABankTranMatch match,
    out PXSetPropertyException error)
  {
    error = (PXSetPropertyException) null;
    if (match == null)
      throw new PXException("Match for transaction '{0}' was not found", new object[1]
      {
        (object) det.TranID
      });
    bool flag1 = true;
    if (match.DocModule == "AP" && match.DocType == "CBT")
    {
      bool flag2 = true;
      foreach (PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt> pxResult in PXSelectBase<PX.Objects.CA.BankStatementHelpers.CATranExt, PXSelectJoin<PX.Objects.CA.BankStatementHelpers.CATranExt, InnerJoin<CABatchDetail, On<CATran.origTranType, Equal<CABatchDetail.origDocType>, And<CATran.origRefNbr, Equal<CABatchDetail.origRefNbr>, And<CATran.origModule, Equal<CABatchDetail.origModule>>>>>, Where<CABatchDetail.batchNbr, Equal<Required<CABatchDetail.batchNbr>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) match.DocRefNbr
      }))
      {
        PX.Objects.CA.BankStatementHelpers.CATranExt caTranExt = PXResult<PX.Objects.CA.BankStatementHelpers.CATranExt>.op_Implicit(pxResult);
        if (!this.ProcessCATran(det, externalPostList, caTranExt.TranID, out error, false))
          flag2 = false;
      }
      if (flag2)
        PXDatabase.Update<CABatch>(new PXDataFieldParam[3]
        {
          (PXDataFieldParam) new PXDataFieldAssign<CABatch.cleared>((object) true),
          (PXDataFieldParam) new PXDataFieldAssign<CABatch.clearDate>((object) det.TranDate),
          (PXDataFieldParam) new PXDataFieldRestrict<CABatch.batchNbr>((PXDbType) 22, new int?(15), (object) match.DocRefNbr, (PXComp) 0)
        });
      CABatch batch = PXResultset<CABatch>.op_Implicit(PXSelectBase<CABatch, PXSelectReadonly<CABatch, Where<CABatch.batchNbr, Equal<Required<CABatch.batchNbr>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) match.DocRefNbr
      }));
      if (!batch.Released.GetValueOrDefault())
      {
        CABatchEntry instance = PXGraph.CreateInstance<CABatchEntry>();
        ((PXSelectBase<CABatch>) instance.Document).Current = batch;
        this.ReleaseCABatch(instance, batch, det);
      }
    }
    else
      this.ProcessCATran(det, externalPostList, match.CATranID, out error);
    return flag1;
  }

  protected virtual void ReleaseCABatch(
    CABatchEntry batchEntryGraph,
    CABatch batch,
    CABankTran detail)
  {
    if (!batch.SkipExport.GetValueOrDefault() && !batch.Exported.GetValueOrDefault())
      throw new PXException("The {0} AP batch has an incorrect status and cannot be processed. It should be exported before release.", new object[1]
      {
        (object) batch.BatchNbr
      });
    ((PXGraph) batchEntryGraph).SelectTimeStamp();
    ((PXAction) batchEntryGraph.release).Press();
  }

  protected virtual bool ProcessCATran(CABankTran det, List<PX.Objects.GL.Batch> externalPostList, long? tranID)
  {
    return this.ProcessCATran(det, externalPostList, tranID, out PXSetPropertyException _);
  }

  protected virtual bool ProcessCATran(
    CABankTran det,
    List<PX.Objects.GL.Batch> externalPostList,
    long? tranID,
    out PXSetPropertyException error)
  {
    return this.ProcessCATran(det, externalPostList, tranID, out error, true);
  }

  protected virtual bool ProcessCATran(
    CABankTran det,
    List<PX.Objects.GL.Batch> externalPostList,
    long? tranID,
    out PXSetPropertyException error,
    bool checkAmt = true)
  {
    error = (PXSetPropertyException) null;
    Func<PX.Objects.CA.BankStatementHelpers.CATranExt> func = (Func<PX.Objects.CA.BankStatementHelpers.CATranExt>) (() => PXResultset<PX.Objects.CA.BankStatementHelpers.CATranExt>.op_Implicit(PXSelectBase<PX.Objects.CA.BankStatementHelpers.CATranExt, PXSelectReadonly<PX.Objects.CA.BankStatementHelpers.CATranExt, Where<PX.Objects.CA.BankStatementHelpers.CATranExt.tranID, Equal<Required<PX.Objects.CA.BankStatementHelpers.CATranExt.tranID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) tranID
    })));
    PX.Objects.CA.BankStatementHelpers.CATranExt caTranExt = func();
    Decimal? nullable = caTranExt != null ? caTranExt.CuryTranAmt : throw new PXException("The transaction cannot be processed due to absence of the matched document. Clear the transaction match by clicking the Unmatch button and repeat the matching process.");
    Decimal? curyTranAmt = det.CuryTranAmt;
    if (((nullable.GetValueOrDefault() == curyTranAmt.GetValueOrDefault() & nullable.HasValue == curyTranAmt.HasValue ? 0 : (!det.MultipleMatchingToPayments.GetValueOrDefault() ? 1 : (det.CuryUnappliedBalMatch.GetValueOrDefault() != 0M ? 1 : 0))) & (checkAmt ? 1 : 0)) != 0)
      throw new PXException("The payment detail amount ({0:F2}) differs from the bank transaction amount ({1:F2}). To create the payment, you should add details whose total amount is equal to the bank transaction amount.", new object[2]
      {
        (object) caTranExt.CuryTranAmt,
        (object) det.CuryTranAmt
      });
    this.TryToSetPaymentDateToBankDate((PXGraph) this, det, caTranExt);
    if (!caTranExt.Released.GetValueOrDefault())
    {
      PXGraph aGraph = (PXGraph) null;
      switch (caTranExt.OrigModule)
      {
        case "AP":
          if (((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.ReleaseAP.GetValueOrDefault())
          {
            CATrxRelease.ReleaseCATran((CATran) caTranExt, ref aGraph, externalPostList, out error);
            this.TryToReleaseBatchPayment(caTranExt, tranID);
            break;
          }
          break;
        case "AR":
          if (((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.ReleaseAR.GetValueOrDefault())
          {
            CATrxRelease.ReleaseCATran((CATran) caTranExt, ref aGraph, externalPostList, out error);
            break;
          }
          break;
        case "CA":
          CATrxRelease.ReleaseCATran((CATran) caTranExt, ref aGraph, externalPostList, out error);
          break;
        default:
          throw new Exception("The document of this type cannot be released in the Cash Management module. Release the document in the module from which the document has originated.");
      }
    }
    ((PXGraph) this).Caches[typeof (PX.Objects.CA.BankStatementHelpers.CATranExt)].ClearQueryCache();
    PX.Objects.CA.BankStatementHelpers.CATranExt aTran = func();
    if (aTran == null)
      throw new PXException("The process cannot be completed. Please contact support service.");
    if (aTran.Released.GetValueOrDefault())
    {
      bool? cleared = aTran.Cleared;
      bool flag = false;
      if (cleared.GetValueOrDefault() == flag & cleared.HasValue)
      {
        ((PXGraph) this).SelectTimeStamp();
        StatementsMatchingProto.UpdateSourceDoc((PXGraph) this, (CATran) aTran, det.TranDate);
      }
    }
    return aTran.Cleared.GetValueOrDefault();
  }

  protected virtual void TryToReleaseBatchPayment(PX.Objects.CA.BankStatementHelpers.CATranExt tran, long? tranID)
  {
    PXResult<CABatch, CABatchDetail> pxResult = (PXResult<CABatch, CABatchDetail>) PXResultset<CABatch>.op_Implicit(PXSelectBase<CABatch, PXSelectReadonly2<CABatch, InnerJoin<CABatchDetail, On<CABatch.batchNbr, Equal<CABatchDetail.batchNbr>>>, Where<CABatchDetail.origModule, Equal<Required<CABatchDetail.origModule>>, And<CABatchDetail.origDocType, Equal<Required<CABatchDetail.origDocType>>, And<CABatchDetail.origRefNbr, Equal<Required<CABatchDetail.origRefNbr>>, And<CABatchDetail.origLineNbr, Equal<CABatchDetail.origLineNbr.defaultValue>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) "AP",
      (object) tran.OrigTranType,
      (object) tran.OrigRefNbr
    }));
    CABatch batch = PXResult<CABatch, CABatchDetail>.op_Implicit(pxResult);
    CABatchDetail detail = PXResult<CABatch, CABatchDetail>.op_Implicit(pxResult);
    if (batch == null || (batch != null ? (!batch.Released.GetValueOrDefault() ? 1 : 0) : 1) == 0 || this.CheckIfBatchHasUnreleasedPayments(detail, tranID) || this.CheckIfBatchHasUnreleasedPaymentsInCache(detail, tranID))
      return;
    CABatchEntry instance = PXGraph.CreateInstance<CABatchEntry>();
    ((PXSelectBase<CABatch>) instance.Document).Current = batch;
    try
    {
      this.ReleaseCABatch(instance, batch, (CABankTran) null);
    }
    catch
    {
    }
  }

  private bool CheckIfBatchHasUnreleasedPayments(CABatchDetail detail, long? tranID)
  {
    return ((IQueryable<PXResult<CABatchDetail>>) PXSelectBase<CABatchDetail, PXSelectJoin<CABatchDetail, InnerJoin<PX.Objects.AP.APPayment, On<CABatchDetail.origModule, Equal<BatchModule.moduleAP>, And<CABatchDetail.origDocType, Equal<PX.Objects.AP.APPayment.docType>, And<CABatchDetail.origRefNbr, Equal<PX.Objects.AP.APPayment.refNbr>, And<CABatchDetail.origLineNbr, Equal<CABatchDetail.origLineNbr.defaultValue>>>>>>, Where<CABatchDetail.batchNbr, Equal<Required<CABatch.batchNbr>>, And<PX.Objects.AP.APPayment.released, NotEqual<True>, And<PX.Objects.AP.APPayment.cATranID, NotEqual<Required<PX.Objects.AP.APPayment.cATranID>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) detail.BatchNbr,
      (object) tranID
    })).Any<PXResult<CABatchDetail>>();
  }

  private bool CheckIfBatchHasUnreleasedPaymentsInCache(CABatchDetail detail, long? tranID)
  {
    foreach (PXResult<CABankTran> pxResult in PXSelectBase<CABankTran, PXSelectJoin<CABankTran, InnerJoin<CABankTranMatch, On<CABankTran.tranID, Equal<CABankTranMatch.tranID>>, InnerJoin<PX.Objects.AP.APPayment, On<PX.Objects.AP.APPayment.docType, Equal<CABankTranMatch.docType>, And<PX.Objects.AP.APPayment.refNbr, Equal<CABankTranMatch.docRefNbr>, And<CABankTranMatch.docModule, Equal<BatchModule.moduleAP>>>>, InnerJoin<CABatchDetail, On<CABatchDetail.origModule, Equal<BatchModule.moduleAP>, And<CABatchDetail.origDocType, Equal<PX.Objects.AP.APPayment.docType>, And<CABatchDetail.origRefNbr, Equal<PX.Objects.AP.APPayment.refNbr>, And<CABatchDetail.origLineNbr, Equal<CABatchDetail.origLineNbr.defaultValue>>>>>>>>, Where<CABatchDetail.batchNbr, Equal<Required<CABatch.batchNbr>>, And<CABankTran.processed, NotEqual<True>, And<PX.Objects.AP.APPayment.cATranID, NotEqual<Required<PX.Objects.AP.APPayment.cATranID>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) detail.BatchNbr,
      (object) tranID
    }))
    {
      if (!((PXSelectBase<CABankTran>) this.Details).Locate(PXResult<CABankTran>.op_Implicit(pxResult)).Processed.GetValueOrDefault())
        return true;
    }
    return false;
  }

  protected virtual bool TryToSetPaymentDateToBankDate(
    PXGraph graph,
    CABankTran bankTransaction,
    PX.Objects.CA.BankStatementHelpers.CATranExt caTransaction)
  {
    bool? released = caTransaction.Released;
    bool flag = false;
    if (released.GetValueOrDefault() == flag & released.HasValue && bankTransaction.TranDate.HasValue)
    {
      DateTime? tranDate1 = bankTransaction.TranDate;
      DateTime? tranDate2 = caTransaction.TranDate;
      if ((tranDate1.HasValue == tranDate2.HasValue ? (tranDate1.HasValue ? (tranDate1.GetValueOrDefault() == tranDate2.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
      {
        switch (caTransaction.OrigModule)
        {
          case "AP":
            if (caTransaction.OrigTranType != "GLE")
            {
              PX.Objects.AP.APPayment apPayment1 = PX.Objects.AP.APPayment.PK.Find(graph, caTransaction.OrigTranType, caTransaction.OrigRefNbr);
              PaymentMethod paymentMethod = PaymentMethod.PK.Find(graph, apPayment1.PaymentMethodID);
              if ((paymentMethod != null ? (!paymentMethod.PaymentDateToBankDate.GetValueOrDefault() ? 1 : 0) : 1) != 0)
                return false;
              APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
              PX.Objects.AP.APPayment apPayment2 = PXResultset<PX.Objects.AP.APPayment>.op_Implicit(((PXSelectBase<PX.Objects.AP.APPayment>) instance.Document).Search<PX.Objects.AP.APPayment.refNbr>((object) caTransaction.OrigRefNbr, new object[1]
              {
                (object) caTransaction.OrigTranType
              }));
              ((PXSelectBase<PX.Objects.AP.APPayment>) instance.Document).Current = apPayment2;
              apPayment2.AdjDate = bankTransaction.TranDate;
              apPayment2.DocDate = bankTransaction.TranDate;
              ((PXSelectBase<PX.Objects.AP.APPayment>) instance.Document).Update(apPayment2);
              foreach (PXResult<PX.Objects.AP.APAdjust> pxResult in ((PXSelectBase<PX.Objects.AP.APAdjust>) instance.Adjustments_Raw).Select(Array.Empty<object>()))
              {
                PX.Objects.AP.APAdjust apAdjust = PXResult<PX.Objects.AP.APAdjust>.op_Implicit(pxResult);
                apAdjust.AdjgDocDate = bankTransaction.TranDate;
                ((PXSelectBase<PX.Objects.AP.APAdjust>) instance.Adjustments_Raw).Update(apAdjust);
              }
              ((PXGraph) instance).Persist();
              return true;
            }
            break;
          case "AR":
            if (caTransaction.OrigTranType != "GLE")
            {
              PX.Objects.AR.ARPayment arPayment1 = PX.Objects.AR.ARPayment.PK.Find(graph, caTransaction.OrigTranType, caTransaction.OrigRefNbr);
              PaymentMethod paymentMethod = PaymentMethod.PK.Find(graph, arPayment1.PaymentMethodID);
              if ((paymentMethod != null ? (!paymentMethod.PaymentDateToBankDate.GetValueOrDefault() ? 1 : 0) : 1) != 0)
                return false;
              ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
              PX.Objects.AR.ARPayment arPayment2 = PXResultset<PX.Objects.AR.ARPayment>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Search<PX.Objects.AR.ARPayment.refNbr>((object) caTransaction.OrigRefNbr, new object[1]
              {
                (object) caTransaction.OrigTranType
              }));
              ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Current = arPayment2;
              arPayment2.AdjDate = bankTransaction.TranDate;
              arPayment2.DocDate = bankTransaction.TranDate;
              ((PXSelectBase<PX.Objects.AR.ARPayment>) instance.Document).Update(arPayment2);
              foreach (PXResult<PX.Objects.AR.ARAdjust> pxResult in ((PXSelectBase<PX.Objects.AR.ARAdjust>) instance.Adjustments_Raw).Select(Array.Empty<object>()))
              {
                PX.Objects.AR.ARAdjust arAdjust = PXResult<PX.Objects.AR.ARAdjust>.op_Implicit(pxResult);
                arAdjust.AdjgDocDate = bankTransaction.TranDate;
                ((PXSelectBase<PX.Objects.AR.ARAdjust>) instance.Adjustments_Raw).Update(arAdjust);
              }
              ((PXGraph) instance).Persist();
              return true;
            }
            break;
        }
        return false;
      }
    }
    return false;
  }

  public void MatchExpenseReceipt(CABankTran bankTran, CABankTranMatch match)
  {
    EPExpenseClaimDetails receipt = PXResultset<EPExpenseClaimDetails>.op_Implicit(PXSelectBase<EPExpenseClaimDetails, PXSelect<EPExpenseClaimDetails, Where<EPExpenseClaimDetails.claimDetailCD, Equal<Required<EPExpenseClaimDetails.claimDetailCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) match.DocRefNbr
    }));
    this.VerifyBeforeMatchExpenseReceipt(bankTran, receipt);
    ((PXGraph) this).SelectTimeStamp();
    ((PXSelectBase<EPExpenseClaimDetails>) this.ExpenseReceipts).Update(receipt);
    if (!receipt.Released.GetValueOrDefault())
      return;
    CATran aTran;
    if (receipt.PaidWith == "CardComp")
    {
      aTran = PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.origModule, Equal<BatchModule.moduleAP>, And<CATran.origTranType, Equal<Required<CATran.origTranType>>, And<CATran.origRefNbr, Equal<Required<CATran.origRefNbr>>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) receipt.APDocType,
        (object) receipt.APRefNbr
      }));
    }
    else
    {
      if (!(receipt.PaidWith == "CardPers"))
        throw new InvalidOperationException();
      aTran = PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelectJoin<CATran, InnerJoin<PX.Objects.GL.GLTran, On<CATran.tranID, Equal<PX.Objects.GL.GLTran.cATranID>>>, Where<PX.Objects.GL.GLTran.module, Equal<BatchModule.moduleAP>, And<PX.Objects.GL.GLTran.tranType, Equal<Required<PX.Objects.GL.GLTran.tranType>>, And<PX.Objects.GL.GLTran.refNbr, Equal<Required<PX.Objects.GL.GLTran.refNbr>>, And<PX.Objects.GL.GLTran.tranLineNbr, Equal<Required<PX.Objects.GL.GLTran.tranLineNbr>>>>>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) receipt.APDocType,
        (object) receipt.APRefNbr,
        (object) receipt.APLineNbr
      }));
    }
    if (aTran == null)
      return;
    bool? nullable = aTran.Released;
    if (nullable.GetValueOrDefault())
    {
      nullable = aTran.Cleared;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        if (receipt.PaidWith == "CardComp")
        {
          StatementsMatchingProto.UpdateSourceDoc((PXGraph) this, aTran, bankTran.TranDate);
        }
        else
        {
          aTran.Cleared = new bool?(true);
          aTran.ClearDate = bankTran.TranDate ?? aTran.TranDate;
          ((PXGraph) this).Caches[typeof (CATran)].Update((object) aTran);
        }
      }
    }
    match.CATranID = aTran.TranID;
    ((PXSelectBase<CABankTranMatch>) this.TranMatch).Update(match);
  }

  protected virtual void ValidateDataForDocumentCreation(CABankTran aRow)
  {
    if (((IQueryable<PXResult<CABankTranMatch>>) ((PXSelectBase<CABankTranMatch>) this.TranMatch).Select(new object[1]
    {
      (object) aRow.TranID
    })).Where<PXResult<CABankTranMatch>>((Expression<Func<PXResult<CABankTranMatch>, bool>>) (t => ((CABankTranMatch) t).IsCharge != (bool?) true)).Count<PXResult<CABankTranMatch>>() != 0)
      throw new PXSetPropertyException("Document is already created");
    int? nullable1 = aRow.BAccountID;
    if (!nullable1.HasValue && aRow.OrigModule != "CA")
      throw new PXSetPropertyException("Filling the Business Account box is mandatory for creating a payment.");
    bool? nullable2;
    if (aRow.OrigModule == "CA")
    {
      if (aRow.EntryTypeID == null)
        throw new PXRowPersistingException(typeof (CABankTranDetail).Name, (object) null, "Filling the Entry Type box is mandatory for creating a payment.");
      nullable2 = ((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.RequireExtRefNbr;
      if (nullable2.GetValueOrDefault() && string.IsNullOrWhiteSpace(aRow.ExtRefNbr))
        throw new PXSetPropertyException("The document could not be processed because the external reference number of the bank transaction is empty. Fill in the Ext. Ref. Nbr. box on the Import Bank Transactions (CA306500) form for the bank transaction.");
    }
    nullable1 = aRow.LocationID;
    if (!nullable1.HasValue && aRow.OrigModule != "CA")
      throw new PXSetPropertyException("Filling the Location box is mandatory for creating a payment.");
    if (aRow.OrigModule == "AR")
    {
      nullable1 = !string.IsNullOrEmpty(aRow.PaymentMethodID) ? aRow.PMInstanceID : throw new PXSetPropertyException("Filling the Payment Method box is mandatory for creating a payment.");
      if (!nullable1.HasValue)
      {
        PaymentMethod paymentMethod = PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) aRow.PaymentMethodID
        }));
        if (paymentMethod != null)
        {
          nullable2 = paymentMethod.IsAccountNumberRequired;
          if (nullable2.GetValueOrDefault())
            throw new PXSetPropertyException("Filling the Payment Method box is mandatory for creating a payment.");
        }
      }
    }
    if (!(aRow.OrigModule == "AP"))
      return;
    if (string.IsNullOrEmpty(aRow.PaymentMethodID))
      throw new PXSetPropertyException("Filling the Payment Method box is mandatory for creating a payment.");
    if (!string.IsNullOrWhiteSpace(aRow.ExtRefNbr))
      return;
    if (PX.Objects.AP.PaymentRefAttribute.PaymentRefMustBeUnique(PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) aRow.PaymentMethodID
    }))))
      throw new PXSetPropertyException("The document could not be processed because the external reference number of the bank transaction is empty. Fill in the Ext. Ref. Nbr. box on the Import Bank Transactions (CA306500) form for the bank transaction.");
    if (aRow.ExtRefNbr != null && aRow.ExtRefNbr.StartsWith(" "))
      throw new PXSetPropertyException("The document could not be processed because the external reference number of the bank transaction cannot start with a leading space. Correct the Ext. Ref. Nbr. box on the Import Bank Transactions (CA306500) form for the bank transaction.");
  }

  protected virtual void ValidateDataForChargeCreation(CABankTran aRow)
  {
    if (aRow.ChargeTypeID == null)
      throw new PXSetPropertyException("To create a charge, specify a charge type.");
    if (!PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>())
      return;
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelectJoin<PX.Objects.GL.Account, InnerJoin<CAEntryType, On<CAEntryType.accountID, Equal<PX.Objects.GL.Account.accountID>>>, Where<CAEntryType.entryTypeId, Equal<Required<CAEntryType.entryTypeId>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) aRow.ChargeTypeID
    }));
    if ((account != null ? (account.AccountGroupID.HasValue ? 1 : 0) : 0) != 0)
    {
      if (aRow.DrCr == "D")
        throw new PXSetPropertyException("The charge cannot be created automatically from this tab, because the {0} charge type requires a specific project. Do the following:~1. On the Payments and Applications(AR302000) form, create a payment for the invoice.~2. On the Transactions (CA304000) form, create a cash transaction for the entry type of the charge and specify the project on the Transaction Details tab.~3. On the Match to Payments tab of the current form, select the Match to Multiple Payments check box and match the invoice and charge to the bank transaction.", new object[1]
        {
          (object) aRow.ChargeTypeID
        });
      throw new PXSetPropertyException("The charge cannot be created automatically from this tab, because the {0} charge type requires a specific project. Do the following:~1. On the Checks and Payments(AP302000) form, create an AP payment for the bill.~2. On the Transactions (CA304000) form, create a cash transaction for the entry type of the charge and specify the project on the Transaction Details tab.~3. On the Match to Payments tab of the current form, select the Match to Multiple Payments check box and match the bill and charge to the bank transaction.", new object[1]
      {
        (object) aRow.ChargeTypeID
      });
    }
    if (((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.RequireExtRefNbr.GetValueOrDefault() && string.IsNullOrWhiteSpace(aRow.ExtRefNbr))
      throw new PXSetPropertyException("The document could not be processed because the external reference number of the bank transaction is empty. Fill in the Ext. Ref. Nbr. box on the Import Bank Transactions (CA306500) form for the bank transaction.");
  }

  protected virtual void CreateDocumentProc(CABankTran aRow, bool doPersist)
  {
    CATran caTran = (CATran) null;
    PXCache cache = ((PXSelectBase) this.Details).Cache;
    this.ValidateDataForDocumentCreation(aRow);
    PX.Objects.CM.Extensions.CurrencyInfo aCuryInfo = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) aRow.CuryInfoID
    }));
    Decimal? curyTaxRoundDiff = aRow.CuryTaxRoundDiff;
    Decimal num1 = 0M;
    if (!(curyTaxRoundDiff.GetValueOrDefault() == num1 & curyTaxRoundDiff.HasValue))
      throw new PXException("The tax amount cannot be edited, because the system cannot process an inclusive tax with the tax amount different from the calculated tax amount. Use the Transactions (CA304000) form to process the cash entry.");
    if (aRow.OrigModule == "AR")
    {
      List<PX.Objects.AR.ARAdjust> aAdjustments = new List<PX.Objects.AR.ARAdjust>();
      foreach (PXResult<CABankTranAdjustment> pxResult1 in ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Select(new object[1]
      {
        (object) aRow.TranID
      }))
      {
        CABankTranAdjustment bankTranAdjustment = PXResult<CABankTranAdjustment>.op_Implicit(pxResult1);
        if (bankTranAdjustment.PaymentsByLinesAllowed.GetValueOrDefault())
        {
          foreach (PXResult<PX.Objects.AR.ARTran> pxResult2 in PXSelectBase<PX.Objects.AR.ARTran, PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Current<CABankTranAdjustment.adjdDocType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Current<CABankTranAdjustment.adjdRefNbr>>, And<PX.Objects.AR.ARTran.curyTranBal, NotEqual<Zero>>>>, OrderBy<Desc<PX.Objects.AR.ARTran.curyTranBal>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
          {
            (object) bankTranAdjustment
          }, Array.Empty<object>()))
          {
            PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran>.op_Implicit(pxResult2);
            aAdjustments.Add(new PX.Objects.AR.ARAdjust()
            {
              AdjdRefNbr = bankTranAdjustment.AdjdRefNbr,
              AdjdDocType = bankTranAdjustment.AdjdDocType,
              AdjdLineNbr = arTran.LineNbr
            });
          }
        }
        else
          aAdjustments.Add(new PX.Objects.AR.ARAdjust()
          {
            AdjdRefNbr = bankTranAdjustment.AdjdRefNbr,
            AdjdDocType = bankTranAdjustment.AdjdDocType,
            CuryAdjgAmt = bankTranAdjustment.CuryAdjgAmt,
            CuryAdjgDiscAmt = bankTranAdjustment.CuryAdjgDiscAmt,
            AdjdCuryRate = bankTranAdjustment.AdjdCuryRate,
            WOBal = bankTranAdjustment.WhTaxBal,
            AdjWOAmt = bankTranAdjustment.AdjWhTaxAmt,
            CuryAdjdWOAmt = bankTranAdjustment.CuryAdjdWhTaxAmt,
            CuryAdjgWOAmt = bankTranAdjustment.CuryAdjgWhTaxAmt,
            CuryWOBal = bankTranAdjustment.CuryWhTaxBal,
            WriteOffReasonCode = bankTranAdjustment.WriteOffReasonCode
          });
      }
      bool? releaseAr = ((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.ReleaseAR;
      bool flag = false;
      bool aOnHold = releaseAr.GetValueOrDefault() == flag & releaseAr.HasValue;
      caTran = this.AddARTransaction((ICADocSource) aRow, aCuryInfo, (IEnumerable<PX.Objects.AR.ARAdjust>) aAdjustments, aOnHold);
    }
    if (aRow.OrigModule == "AP")
    {
      List<ICADocAdjust> aAdjustments = new List<ICADocAdjust>();
      foreach (PXResult<CABankTranAdjustment> pxResult in ((PXSelectBase<CABankTranAdjustment>) this.Adjustments).Select(new object[1]
      {
        (object) aRow.TranID
      }))
      {
        CABankTranAdjustment bankTranAdjustment = PXResult<CABankTranAdjustment>.op_Implicit(pxResult);
        aAdjustments.Add((ICADocAdjust) bankTranAdjustment);
      }
      bool? releaseAp = ((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.ReleaseAP;
      bool flag = false;
      bool aOnHold = releaseAp.GetValueOrDefault() == flag & releaseAp.HasValue;
      caTran = this.AddAPTransaction((ICADocSource) aRow, aCuryInfo, (IList<ICADocAdjust>) aAdjustments, aOnHold);
    }
    if (aRow.OrigModule == "CA")
    {
      List<CASplit> splits = new List<CASplit>();
      foreach (PXResult<CABankTranDetail> pxResult in ((PXSelectBase<CABankTranDetail>) this.TranSplit).Select(new object[2]
      {
        (object) aRow.TranID,
        (object) aRow.TranType
      }))
      {
        CABankTranDetail caBankTranDetail = PXResult<CABankTranDetail>.op_Implicit(pxResult);
        splits.Add(new CASplit()
        {
          LineNbr = caBankTranDetail.LineNbr,
          AccountID = caBankTranDetail.AccountID,
          BranchID = caBankTranDetail.BranchID,
          CashAccountID = caBankTranDetail.CashAccountID,
          CuryTranAmt = caBankTranDetail.CuryTranAmt,
          CuryUnitPrice = caBankTranDetail.CuryUnitPrice,
          InventoryID = caBankTranDetail.InventoryID,
          NonBillable = caBankTranDetail.NonBillable,
          NoteID = caBankTranDetail.NoteID,
          ProjectID = caBankTranDetail.ProjectID,
          Qty = caBankTranDetail.Qty,
          SubID = caBankTranDetail.SubID,
          TaskID = caBankTranDetail.TaskID,
          CostCodeID = caBankTranDetail.CostCodeID,
          TranDesc = caBankTranDetail.TranDesc,
          TaxCategoryID = caBankTranDetail.TaxCategoryID
        });
      }
      List<CATaxTran> taxTrans = new List<CATaxTran>();
      foreach (PXResult<CABankTaxTran> pxResult in ((PXSelectBase<CABankTaxTran>) this.TaxTrans).Select(Array.Empty<object>()))
      {
        CABankTaxTran caBankTaxTran = PXResult<CABankTaxTran>.op_Implicit(pxResult);
        CATaxTran caTaxTran = new CATaxTran();
        caTaxTran.BranchID = caBankTaxTran.BranchID;
        caTaxTran.VendorID = caBankTaxTran.VendorID;
        caTaxTran.AccountID = caBankTaxTran.AccountID;
        caTaxTran.TaxPeriodID = caBankTaxTran.TaxPeriodID;
        caTaxTran.FinPeriodID = caBankTaxTran.FinPeriodID;
        caTaxTran.FinDate = caBankTaxTran.FinDate;
        caTaxTran.Module = caBankTaxTran.Module;
        caTaxTran.TranType = caBankTaxTran.Module;
        caTaxTran.Released = caBankTaxTran.Released;
        caTaxTran.Voided = caBankTaxTran.Voided;
        caTaxTran.TaxID = caBankTaxTran.TaxID;
        caTaxTran.TaxRate = caBankTaxTran.TaxRate;
        caTaxTran.CuryOrigTaxableAmt = caBankTaxTran.CuryOrigTaxableAmt;
        caTaxTran.OrigTaxableAmt = caBankTaxTran.OrigTaxableAmt;
        caTaxTran.CuryTaxableAmt = caBankTaxTran.CuryTaxableAmt;
        caTaxTran.TaxableAmt = caBankTaxTran.TaxableAmt;
        caTaxTran.CuryExemptedAmt = caBankTaxTran.CuryExemptedAmt;
        caTaxTran.ExemptedAmt = caBankTaxTran.ExemptedAmt;
        caTaxTran.CuryTaxAmt = caBankTaxTran.CuryTaxAmt;
        caTaxTran.TaxAmt = caBankTaxTran.TaxAmt;
        caTaxTran.BAccountID = caBankTaxTran.BAccountID;
        caTaxTran.CuryTaxAmtSumm = caBankTaxTran.CuryTaxAmtSumm;
        caTaxTran.TaxAmtSumm = caBankTaxTran.TaxAmtSumm;
        caTaxTran.NonDeductibleTaxRate = caBankTaxTran.NonDeductibleTaxRate;
        caTaxTran.CuryExpenseAmt = caBankTaxTran.CuryExpenseAmt;
        caTaxTran.ExpenseAmt = caBankTaxTran.ExpenseAmt;
        caTaxTran.CuryID = caBankTaxTran.CuryID;
        caTaxTran.TaxUOM = caBankTaxTran.TaxUOM;
        caTaxTran.TaxableQty = caBankTaxTran.TaxableQty;
        caTaxTran.TaxBucketID = caBankTaxTran.TaxBucketID;
        caTaxTran.TaxType = caBankTaxTran.TaxType;
        caTaxTran.TaxZoneID = caBankTaxTran.TaxZoneID;
        taxTrans.Add(caTaxTran);
      }
      if (splits.Count <= 0)
        throw new PXRowPersistingException(typeof (CABankTranDetail).Name, (object) null, "This document has no details and can not be processed.");
      caTran = this.AddCATransaction((ICADocWithTaxesSource) aRow, aCuryInfo, (IEnumerable<CASplit>) splits, (IEnumerable<CATaxTran>) taxTrans, false);
    }
    if (caTran != null)
    {
      CABankTranMatch caBankTranMatch = new CABankTranMatch();
      caBankTranMatch.TranID = aRow.TranID;
      caBankTranMatch.TranType = aRow.TranType;
      caBankTranMatch.CATranID = caTran.TranID;
      Decimal num2 = (Decimal) (aRow.DrCr == "D" ? 1 : -1);
      Decimal? curyTranAmt = caTran.CuryTranAmt;
      caBankTranMatch.CuryApplAmt = curyTranAmt.HasValue ? new Decimal?(num2 * curyTranAmt.GetValueOrDefault()) : new Decimal?();
      ((PXSelectBase<CABankTranMatch>) this.TranMatch).Insert(caBankTranMatch);
      aRow.CreateDocument = new bool?(false);
      cache.Update((object) aRow);
    }
    if (!doPersist)
      return;
    ((PXAction) this.Save).Press();
  }

  protected virtual CABankTran CreateChargesProc(CABankTran aRow)
  {
    PXCache cache = ((PXSelectBase) this.Details).Cache;
    this.ValidateDataForChargeCreation(aRow);
    Decimal? amount = new Decimal?();
    string taxCategotyID = (string) null;
    foreach (PXResult<CABankTranMatch> pxResult in ((PXSelectBase<CABankTranMatch>) this.TranMatchCharge).Select(new object[1]
    {
      (object) aRow.TranID
    }))
    {
      CABankTranMatch caBankTranMatch = PXResult<CABankTranMatch>.op_Implicit(pxResult);
      amount = caBankTranMatch.CuryApplAmt;
      taxCategotyID = caBankTranMatch.TaxCategoryID;
      ((PXSelectBase<CABankTranMatch>) this.TranMatch).Delete(caBankTranMatch);
    }
    PX.Objects.CM.Extensions.CurrencyInfo aCuryInfo = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) aRow.CuryInfoID
    }));
    CATran caTran = this.AddChargeTransaction(aRow, aCuryInfo, amount, taxCategotyID);
    if (caTran != null)
    {
      CABankTranMatch caBankTranMatch = new CABankTranMatch();
      caBankTranMatch.TranID = aRow.TranID;
      caBankTranMatch.MatchType = "C";
      caBankTranMatch.TranType = aRow.TranType;
      caBankTranMatch.CATranID = caTran.TranID;
      Decimal num = (Decimal) (aRow.ChargeDrCr == aRow.DrCr ? 1 : -1);
      Decimal? curyChargeAmt = aRow.CuryChargeAmt;
      caBankTranMatch.CuryApplAmt = curyChargeAmt.HasValue ? new Decimal?(num * curyChargeAmt.GetValueOrDefault()) : new Decimal?();
      ((PXSelectBase<CABankTranMatch>) this.TranMatch).Insert(caBankTranMatch);
      aRow.MultipleMatchingToPayments = new bool?(true);
      aRow = (CABankTran) cache.Update((object) aRow);
    }
    return aRow;
  }

  protected virtual CATran AddARTransaction(
    ICADocSource parameters,
    PX.Objects.CM.Extensions.CurrencyInfo aCuryInfo,
    IEnumerable<ICADocAdjust> aAdjustments,
    bool aOnHold)
  {
    PaymentReclassifyProcess.CheckARTransaction(parameters);
    PX.Objects.CM.CurrencyInfo cm = aCuryInfo.GetCM();
    return PaymentReclassifyProcess.AddARTransaction((IAddARTransaction) this, parameters, cm, aAdjustments, aOnHold);
  }

  protected virtual CATran AddARTransaction(
    ICADocSource parameters,
    PX.Objects.CM.Extensions.CurrencyInfo aCuryInfo,
    IEnumerable<PX.Objects.AR.ARAdjust> aAdjustments,
    bool aOnHold)
  {
    PaymentReclassifyProcess.CheckARTransaction(parameters);
    PX.Objects.CM.CurrencyInfo cm = aCuryInfo.GetCM();
    return PaymentReclassifyProcess.AddARTransaction((IAddARTransaction) this, parameters, cm, aAdjustments, aOnHold);
  }

  public virtual PX.Objects.AR.ARPayment InitializeARPayment(
    ARPaymentEntry graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    bool aOnHold)
  {
    return AddARTransactionHelper.InitializeARPayment(graph, parameters, aCuryInfo, aOnHold, BranchSource.CustomerVendorLocation);
  }

  public virtual void InitializeCurrencyInfo(
    ARPaymentEntry graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    PX.Objects.AR.ARPayment doc)
  {
    AddARTransactionHelper.InitializeCurrencyInfo(graph, parameters, aCuryInfo, doc);
  }

  public virtual Decimal InitializeARAdjustment(
    ARPaymentEntry graph,
    PX.Objects.AR.ARAdjust adjustment,
    Decimal curyAppliedAmt)
  {
    return AddARTransactionHelper.InitializeARAdjustment(graph, adjustment, curyAppliedAmt);
  }

  protected virtual CATran AddAPTransaction(
    ICADocSource parameters,
    PX.Objects.CM.Extensions.CurrencyInfo aCuryInfo,
    IList<ICADocAdjust> aAdjustments,
    bool aOnHold)
  {
    PaymentReclassifyProcess.CheckAPTransaction(parameters);
    PX.Objects.CM.CurrencyInfo cm = aCuryInfo.GetCM();
    return PaymentReclassifyProcess.AddAPTransaction((IAddAPTransaction) this, parameters, cm, aAdjustments, aOnHold);
  }

  public virtual PX.Objects.AP.APPayment InitializeAPPayment(
    APPaymentEntry graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    IList<ICADocAdjust> aAdjustments,
    bool aOnHold)
  {
    return AddAPTransactionHelper.InitializeAPPayment(graph, parameters, aCuryInfo, aAdjustments, aOnHold, BranchSource.CustomerVendorLocation);
  }

  public virtual void InitializeCurrencyInfo(
    APPaymentEntry graph,
    ICADocSource parameters,
    PX.Objects.CM.CurrencyInfo aCuryInfo,
    PX.Objects.AP.APPayment doc)
  {
    AddAPTransactionHelper.InitializeCurrencyInfo(graph, parameters, aCuryInfo, doc);
  }

  public virtual PX.Objects.AP.APAdjust InitializeAPAdjustment(
    APPaymentEntry graph,
    ICADocAdjust adjustment)
  {
    return AddAPTransactionHelper.InitializeAPAdjustment(graph, adjustment);
  }

  protected virtual CATran AddCATransaction(
    ICADocWithTaxesSource parameters,
    PX.Objects.CM.Extensions.CurrencyInfo aCuryInfo,
    IEnumerable<CASplit> splits,
    IEnumerable<CATaxTran> taxTrans,
    bool IsTransferExpense)
  {
    CABankTransactionsMaint.CheckCATransaction((ICADocSource) parameters, ((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current);
    return CABankTransactionsMaint.AddCATransaction((PXGraph) this, parameters, aCuryInfo, splits, taxTrans, IsTransferExpense);
  }

  protected virtual CATran AddChargeTransaction(
    CABankTran bankTran,
    PX.Objects.CM.Extensions.CurrencyInfo aCuryInfo,
    Decimal? amount,
    string taxCategotyID)
  {
    CABankTransactionsMaint.CheckChargeTransaction(bankTran, ((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current);
    return CABankTransactionsMaint.AddChargeTransaction((PXGraph) this, (ICADocSource) bankTran, aCuryInfo, amount, taxCategotyID);
  }

  protected virtual bool IsARInvoiceSearchNeeded(CABankTran aRow)
  {
    return aRow.OrigModule == "AR" && !string.IsNullOrEmpty(aRow.InvoiceInfo);
  }

  protected virtual bool IsAPInvoiceSearchNeeded(CABankTran aRow)
  {
    return aRow.OrigModule == "AP" && !string.IsNullOrEmpty(aRow.InvoiceInfo);
  }

  protected virtual bool AttemptApplyRules(CABankTran transaction, bool applyHiding)
  {
    if (transaction == null || transaction.RuleID.HasValue)
      return false;
    foreach (PXResult<CABankTranRule> pxResult in ((PXSelectBase<CABankTranRule>) this.Rules).Select(Array.Empty<object>()))
    {
      CABankTranRule rule = PXResult<CABankTranRule>.op_Implicit(pxResult);
      if (applyHiding || rule.Action != "H")
      {
        if (this.CheckRuleMatches(transaction, rule))
        {
          try
          {
            this.ApplyRule(transaction, rule);
            ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<CABankTran.entryTypeID>((object) transaction, (object) transaction.EntryTypeID, (Exception) null);
            PXUIFieldAttribute.SetError<CABankTran.ruleID>(((PXSelectBase) this.Details).Cache, (object) transaction, (string) null);
            return true;
          }
          catch (PXException ex)
          {
            ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<CABankTran.entryTypeID>((object) transaction, (object) transaction.EntryTypeID, (Exception) null);
            PXUIFieldAttribute.SetWarning<CABankTran.ruleID>(((PXSelectBase) this.Details).Cache, (object) transaction, "Failed to apply a rule due to data validation.");
            this.ResetTranFields(((PXSelectBase) this.Details).Cache, transaction);
          }
        }
      }
    }
    return false;
  }

  protected virtual bool CheckRuleMatches(CABankTran transaction, CABankTranRule rule)
  {
    return this.MatchingService.CheckRuleMatches(transaction, rule);
  }

  protected virtual void ApplyRule(CABankTran transaction, CABankTranRule rule)
  {
    if (rule.Action == "C")
    {
      if (!transaction.CuryInfoID.HasValue)
        this.SetDefaultCurrencyInfoID(transaction);
      ((PXSelectBase) this.Details).Cache.SetValueExt<CABankTran.origModule>((object) transaction, (object) rule.DocumentModule);
      ((PXSelectBase) this.Details).Cache.SetValueExt<CABankTran.entryTypeID>((object) transaction, (object) rule.DocumentEntryTypeID);
    }
    else if (rule.Action == "H")
    {
      transaction.CreateDocument = new bool?(false);
      transaction.DocumentMatched = new bool?(false);
      transaction.Hidden = new bool?(true);
      transaction.Processed = new bool?(true);
    }
    ((PXSelectBase) this.Details).Cache.SetValue<CABankTran.ruleID>((object) transaction, (object) rule.RuleID);
  }

  protected virtual void SetDefaultCurrencyInfoID(CABankTran transaction)
  {
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ((PXGraph) this).GetExtension<CABankTransactionsMaintMultiCurrency>().CreateCurrencyInfo();
    ((PXSelectBase) this.Details).Cache.SetValueExt<CABankTran.curyInfoID>((object) transaction, (object) currencyInfo.CuryInfoID);
  }

  public virtual void ApplyRule(CABankTranRule rule)
  {
    this.ApplyRule(((PXSelectBase) this.Details).Cache, GraphHelper.RowCast<CABankTran>((IEnumerable) ((PXSelectBase<CABankTran>) this.UnMatchedDetails).Select(Array.Empty<object>())), rule);
  }

  public virtual void ApplyRule(
    PXCache aUpdateCache,
    IEnumerable<CABankTran> aRows,
    CABankTranRule rule)
  {
    foreach (CABankTran aRow in aRows)
    {
      bool? createDocument = aRow.CreateDocument;
      if ((!createDocument.GetValueOrDefault() || !(aRow.OrigModule != "CA") && aRow.EntryTypeID == null) && this.CheckRuleMatches(aRow, rule))
      {
        aUpdateCache.Current = (object) aRow;
        CABankTran caBankTran1 = aUpdateCache.CreateCopy((object) aRow) as CABankTran;
        try
        {
          createDocument = caBankTran1.CreateDocument;
          if (createDocument.GetValueOrDefault())
          {
            caBankTran1.CreateDocument = new bool?(false);
            caBankTran1 = aUpdateCache.Update((object) caBankTran1) as CABankTran;
          }
          caBankTran1.CreateDocument = new bool?(true);
          this.ApplyRule(caBankTran1, rule);
          PXCache cache = ((PXSelectBase) this.Details).Cache;
          CABankTran caBankTran2 = caBankTran1;
          createDocument = caBankTran1.CreateDocument;
          // ISSUE: variable of a boxed type
          __Boxed<bool> local = (ValueType) (bool) (!createDocument.GetValueOrDefault() ? 0 : (CABankTransactionsMaint.ValidateTranFields(this, aUpdateCache, caBankTran1, (PXSelectBase<CABankTranAdjustment>) this.Adjustments) ? 1 : 0));
          cache.SetValueExt<CABankTran.documentMatched>((object) caBankTran2, (object) local);
          aUpdateCache.Update((object) caBankTran1);
          ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<CABankTran.entryTypeID>((object) caBankTran1, (object) caBankTran1.EntryTypeID, (Exception) null);
          PXUIFieldAttribute.SetError<CABankTran.ruleID>(aUpdateCache, (object) caBankTran1, (string) null);
        }
        catch (PXException ex)
        {
          ((PXSelectBase) this.Details).Cache.RaiseExceptionHandling<CABankTran.entryTypeID>((object) caBankTran1, (object) caBankTran1.EntryTypeID, (Exception) null);
          PXUIFieldAttribute.SetWarning<CABankTran.ruleID>(aUpdateCache, (object) caBankTran1, "Failed to apply a rule due to data validation.");
          this.ResetTranFields(aUpdateCache, caBankTran1);
        }
      }
    }
  }

  protected virtual void CATranExt_OrigTranType_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CATranExt_CuryTranAmtCalc_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    CABankTran current = ((PXSelectBase<CABankTran>) this.Details).Current;
    PX.Objects.CA.BankStatementHelpers.CATranExt row = (PX.Objects.CA.BankStatementHelpers.CATranExt) e.Row;
    if (this.Details == null || row == null)
      return;
    Decimal? curyTranAmt = current.CuryTranAmt;
    Decimal num = 0M;
    bool flag = curyTranAmt.GetValueOrDefault() <= num & curyTranAmt.HasValue;
    e.ReturnValue = (object) (flag ? -1M * row.CuryTranAmt.Value : row.CuryTranAmt.Value);
  }

  [PXUIField]
  [PXLookupButton]
  protected virtual IEnumerable viewTaxDetails(PXAdapter adapter)
  {
    ((PXSelectBase<CABankTaxTran>) this.TaxTrans).AskExt(true);
    return adapter.Get();
  }

  [PXCustomizeBaseAttribute]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CashAccount.acctSettingsAllowed> e)
  {
  }

  [PXCustomizeBaseAttribute]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CashAccount.pTInstancesAllowed> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Business Account Name")]
  protected virtual void BAccountR_AcctName_CacheAttached(PXCache sender)
  {
  }

  [Branch(null, typeof (Search2<PX.Objects.GL.Branch.branchID, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>>, Where2<MatchWithBranch<PX.Objects.GL.Branch.branchID>, And<BqlOperand<PX.Objects.GL.Branch.baseCuryID, IBqlString>.IsEqual<BqlField<CABankTransactionsMaint.Filter.baseCuryID, IBqlString>.FromCurrent>>>>), true, true, true)]
  protected virtual void _(PX.Data.Events.CacheAttached<CABankTranDetail.branchID> e)
  {
  }

  public static void CheckCATransaction(ICADocSource parameters, PX.Objects.CA.CASetup setup)
  {
    if (!(parameters.OrigModule == "CA"))
      return;
    if (!parameters.CashAccountID.HasValue)
      throw new PXRowPersistingException(typeof (AddTrxFilter.cashAccountID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) typeof (AddTrxFilter.cashAccountID).Name
      });
    if (string.IsNullOrEmpty(parameters.EntryTypeID))
      throw new PXRowPersistingException(typeof (AddTrxFilter.entryTypeID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) typeof (AddTrxFilter.entryTypeID).Name
      });
    if (string.IsNullOrEmpty(parameters.ExtRefNbr) && setup.RequireExtRefNbr.GetValueOrDefault())
      throw new PXRowPersistingException(typeof (AddTrxFilter.extRefNbr).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) typeof (AddTrxFilter.extRefNbr).Name
      });
  }

  public static void CheckChargeTransaction(CABankTran bankTran, PX.Objects.CA.CASetup setup)
  {
    if (!(bankTran.OrigModule == "CA"))
      return;
    if (!bankTran.CashAccountID.HasValue)
      throw new PXRowPersistingException(typeof (CABankTran.cashAccountID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) typeof (CABankTran.cashAccountID).Name
      });
    if (string.IsNullOrEmpty(bankTran.ChargeTypeID))
      throw new PXRowPersistingException(typeof (CABankTran.entryTypeID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) typeof (CABankTran.entryTypeID).Name
      });
    if (string.IsNullOrEmpty(bankTran.ExtRefNbr) && setup.RequireExtRefNbr.GetValueOrDefault())
      throw new PXRowPersistingException(typeof (CABankTran.extRefNbr).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) typeof (CABankTran.extRefNbr).Name
      });
  }

  public static CATran AddCATransaction(
    PXGraph graph,
    ICADocWithTaxesSource parameters,
    PX.Objects.CM.Extensions.CurrencyInfo aCuryInfo,
    IEnumerable<CASplit> splits,
    IEnumerable<CATaxTran> taxTrans,
    bool IsTransferExpense)
  {
    if (!(parameters.OrigModule == "CA"))
      return (CATran) null;
    CATranEntry instance = PXGraph.CreateInstance<CATranEntry>();
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) parameters.CashAccountID
    }));
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = aCuryInfo;
    if (currencyInfo1 == null)
      currencyInfo1 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelectReadonly<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) instance, new object[1]
      {
        (object) parameters.CuryInfoID
      }));
    if (currencyInfo1 != null)
    {
      foreach (PXResult<PX.Objects.CM.Extensions.CurrencyInfo> pxResult in PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<CAAdj.curyInfoID>>>>.Config>.Select((PXGraph) instance, Array.Empty<object>()))
      {
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = PXResult<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
        PX.Objects.CM.Extensions.CurrencyInfo copy = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(currencyInfo1);
        copy.CuryInfoID = currencyInfo2.CuryInfoID;
        ((PXSelectBase) instance.currencyinfo).Cache.Update((object) copy);
      }
    }
    else if (cashAccount != null && cashAccount.CuryRateTypeID != null)
      currencyInfo1 = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) instance.currencyinfo).Insert(new PX.Objects.CM.Extensions.CurrencyInfo()
      {
        CuryID = cashAccount.CuryID,
        CuryRateTypeID = cashAccount.CuryRateTypeID
      });
    CAAdj caAdj1 = new CAAdj();
    caAdj1.AdjTranType = IsTransferExpense ? "CTE" : "CAE";
    if (IsTransferExpense)
      caAdj1.TransferNbr = ((PXSelectBase<CATransfer>) (graph as CashTransferEntry).Transfer).Current.TransferNbr;
    caAdj1.CashAccountID = parameters.CashAccountID;
    caAdj1.CuryID = parameters.CuryID;
    caAdj1.CuryInfoID = currencyInfo1.CuryInfoID;
    caAdj1.DrCr = parameters.DrCr;
    caAdj1.ExtRefNbr = parameters.ExtRefNbr?.Trim();
    caAdj1.Released = new bool?(false);
    caAdj1.Cleared = parameters.Cleared;
    caAdj1.TranDate = parameters.MatchingPaymentDate;
    caAdj1.FinPeriodID = parameters.FinPeriodID;
    caAdj1.TranDesc = parameters.TranDesc;
    caAdj1.EntryTypeID = parameters.EntryTypeID;
    caAdj1.CuryControlAmt = parameters.CuryOrigDocAmt;
    caAdj1.CABankTranRefNoteID = parameters.NoteID;
    caAdj1.NoteID = new Guid?();
    caAdj1.Hold = new bool?(true);
    caAdj1.TaxZoneID = parameters.TaxZoneID;
    caAdj1.TaxCalcMode = parameters.TaxCalcMode;
    CAAdj caAdj2 = ((PXSelectBase<CAAdj>) instance.CAAdjRecords).Insert(caAdj1);
    PXNoteAttribute.CopyNoteAndFiles(((PXGraph) instance).Caches[parameters.GetType()], (object) parameters, ((PXSelectBase) instance.CAAdjRecords).Cache, (object) caAdj2, (PXNoteAttribute.IPXCopySettings) null);
    if (splits == null)
    {
      ((PXSelectBase<CASplit>) instance.CASplitRecords).Insert(new CASplit()
      {
        AdjTranType = caAdj2.AdjTranType,
        CuryInfoID = currencyInfo1.CuryInfoID,
        Qty = new Decimal?(1M),
        CuryUnitPrice = parameters.CuryOrigDocAmt,
        CuryTranAmt = parameters.CuryOrigDocAmt,
        TranDesc = parameters.TranDesc
      });
    }
    else
    {
      foreach (CASplit split in splits)
      {
        split.AdjTranType = caAdj2.AdjTranType;
        split.AdjRefNbr = caAdj2.RefNbr;
        ((PXSelectBase<CASplit>) instance.CASplitRecords).Insert(split);
      }
    }
    Dictionary<string, CATaxTran> dictionary = taxTrans.ToDictionary<CATaxTran, string>((Func<CATaxTran, string>) (x => x.TaxID));
    foreach (PXResult<CATaxTran> pxResult in ((PXSelectBase<CATaxTran>) instance.Taxes).Select(Array.Empty<object>()))
    {
      CATaxTran caTaxTran = PXResult<CATaxTran>.op_Implicit(pxResult);
      if (dictionary.ContainsKey(caTaxTran.TaxID))
      {
        if (!object.Equals((object) caTaxTran.CuryTaxAmt, (object) dictionary[caTaxTran.TaxID].CuryTaxAmt))
        {
          caTaxTran.CuryTaxAmt = dictionary[caTaxTran.TaxID].CuryTaxAmt;
          ((PXSelectBase<CATaxTran>) instance.Taxes).Update(caTaxTran);
        }
      }
      else
        ((PXSelectBase<CATaxTran>) instance.Taxes).Delete(caTaxTran);
    }
    Decimal num = parameters.DrCr == "D" ? 1M : -1M;
    Decimal? curyTranAmt = parameters.CuryTranAmt;
    Decimal? nullable = curyTranAmt.HasValue ? new Decimal?(num * curyTranAmt.GetValueOrDefault()) : new Decimal?();
    caAdj2.CuryTaxAmt = parameters.CuryTaxTotal;
    caAdj2.CuryTaxTotal = parameters.CuryTaxTotal;
    caAdj2.CuryOrigDocAmt = nullable;
    caAdj2.CuryTranAmt = nullable;
    caAdj2.Hold = new bool?(false);
    ((PXSelectBase<CAAdj>) instance.CAAdjRecords).Update(caAdj2);
    ((PXAction) instance.releaseFromHold).Press();
    ((PXAction) instance.Save).Press();
    CAAdj current = (CAAdj) ((PXGraph) instance).Caches[typeof (CAAdj)].Current;
    return PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.tranID, Equal<Required<CAAdj.tranID>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) current.TranID
    }));
  }

  public static CATran AddChargeTransaction(
    PXGraph graph,
    ICADocSource bankTran,
    PX.Objects.CM.Extensions.CurrencyInfo aCuryInfo,
    Decimal? amount,
    string taxCategotyID)
  {
    CATranEntry instance = PXGraph.CreateInstance<CATranEntry>();
    CATranEntryMultiCurrency extension = ((PXGraph) instance).GetExtension<CATranEntryMultiCurrency>();
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select(graph, new object[1]
    {
      (object) bankTran.CashAccountID
    }));
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = aCuryInfo;
    PX.Objects.CM.Extensions.CurrencyInfo copy1 = (PX.Objects.CM.Extensions.CurrencyInfo) ((PXSelectBase) extension.currencyinfo).Cache.CreateCopy((object) currencyInfo1);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) extension.currencyinfo).Insert(copy1);
    if (currencyInfo2 == null)
      currencyInfo2 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelectReadonly<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) instance, new object[1]
      {
        (object) bankTran.CuryInfoID
      }));
    if (currencyInfo2 != null)
    {
      foreach (PXResult<PX.Objects.CM.Extensions.CurrencyInfo> pxResult in PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<CAAdj.curyInfoID>>>>.Config>.Select((PXGraph) instance, Array.Empty<object>()))
      {
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo3 = PXResult<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(pxResult);
        PX.Objects.CM.Extensions.CurrencyInfo copy2 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(currencyInfo2);
        copy2.CuryInfoID = currencyInfo3.CuryInfoID;
        ((PXSelectBase) extension.currencyinfo).Cache.Update((object) copy2);
      }
    }
    else if (cashAccount != null && cashAccount.CuryRateTypeID != null)
      currencyInfo2 = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) extension.currencyinfo).Insert(new PX.Objects.CM.Extensions.CurrencyInfo()
      {
        CuryID = cashAccount.CuryID,
        CuryRateTypeID = cashAccount.CuryRateTypeID
      });
    CAAdj adj = ((PXSelectBase<CAAdj>) instance.CAAdjRecords).Insert(new CAAdj()
    {
      AdjTranType = "CAE",
      CashAccountID = bankTran.CashAccountID,
      CuryID = bankTran.CuryID,
      CuryInfoID = currencyInfo2.CuryInfoID,
      DrCr = bankTran.DrCr,
      ExtRefNbr = bankTran.ExtRefNbr?.Trim(),
      Released = new bool?(false),
      Cleared = bankTran.Cleared,
      TranDate = bankTran.MatchingPaymentDate,
      FinPeriodID = bankTran.FinPeriodID,
      TranDesc = bankTran.TranDesc,
      EntryTypeID = bankTran.ChargeTypeID,
      CuryControlAmt = bankTran.CuryChargeAmt,
      CABankTranRefNoteID = bankTran.NoteID,
      Hold = new bool?(true),
      TaxZoneID = bankTran.ChargeTaxZoneID,
      TaxCalcMode = bankTran.ChargeTaxCalcMode
    });
    CASplit caSplit1 = new CASplit();
    caSplit1.AdjTranType = adj.AdjTranType;
    caSplit1.CuryInfoID = currencyInfo2.CuryInfoID;
    CASplit caSplit2 = caSplit1;
    Decimal num1 = (Decimal) (bankTran.ChargeDrCr == bankTran.DrCr ? 1 : -1);
    Decimal? nullable1 = amount;
    Decimal? nullable2 = nullable1.HasValue ? new Decimal?(num1 * nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable3 = nullable2 ?? bankTran.CuryChargeAmt;
    caSplit2.CuryTranAmt = nullable3;
    caSplit1.TranDesc = bankTran.TranDesc;
    caSplit1.TaxCategoryID = taxCategotyID;
    ((PXSelectBase<CASplit>) instance.CASplitRecords).Insert(caSplit1);
    nullable2 = ((PXSelectBase<CAAdj>) instance.CAAdjRecords).Current.CuryTranAmt;
    nullable1 = bankTran.CuryChargeAmt;
    Decimal? discrepancy = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    nullable1 = discrepancy;
    Decimal num2 = 0M;
    if (!(nullable1.GetValueOrDefault() == num2 & nullable1.HasValue))
      CABankTransactionsMaint.ProcessDiscrepancy(instance, adj, discrepancy);
    adj.Hold = new bool?(false);
    ((PXSelectBase<CAAdj>) instance.CAAdjRecords).Update(adj);
    ((PXAction) instance.releaseFromHold).Press();
    ((PXAction) instance.Save).Press();
    CAAdj current = (CAAdj) ((PXGraph) instance).Caches[typeof (CAAdj)].Current;
    return PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.tranID, Equal<Required<CAAdj.tranID>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) current.TranID
    }));
  }

  private static void ProcessDiscrepancy(CATranEntry graph, CAAdj adj, Decimal? discrepancy)
  {
    CAAdj caAdj1 = adj;
    Decimal? nullable1 = caAdj1.CuryTranAmt;
    Decimal? nullable2 = discrepancy;
    caAdj1.CuryTranAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    CAAdj caAdj2 = adj;
    Decimal? nullable3 = caAdj2.CuryTaxTotal;
    nullable1 = discrepancy;
    caAdj2.CuryTaxTotal = nullable3.HasValue & nullable1.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    using (IEnumerator<PXResult<CATaxTran>> enumerator = ((PXSelectBase<CATaxTran>) graph.Taxes).Select(Array.Empty<object>()).GetEnumerator())
    {
      if (!enumerator.MoveNext())
        return;
      CATaxTran caTaxTran1 = PXResult<CATaxTran>.op_Implicit(enumerator.Current);
      CATaxTran caTaxTran2 = caTaxTran1;
      nullable1 = caTaxTran2.CuryTaxAmt;
      nullable3 = discrepancy;
      caTaxTran2.CuryTaxAmt = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
      ((PXSelectBase<CATaxTran>) graph.Taxes).Update(caTaxTran1);
    }
  }

  public static void RematchFromExpenseReceipt(
    PXGraph graph,
    CABankTranMatch bankTranMatch,
    long? catranID,
    int? referenceID,
    EPExpenseClaimDetails receipt)
  {
    bankTranMatch.CATranID = catranID;
    bankTranMatch.ReferenceID = referenceID;
    bankTranMatch.DocModule = (string) null;
    bankTranMatch.DocType = (string) null;
    bankTranMatch.DocRefNbr = (string) null;
    graph.Caches[typeof (CABankTranMatch)].Update((object) bankTranMatch);
    CABankTran caBankTran = PXResultset<CABankTran>.op_Implicit(PXSelectBase<CABankTran, PXSelect<CABankTran, Where<CABankTran.tranID, Equal<Required<CABankTran.tranID>>>>.Config>.Select(graph, new object[1]
    {
      (object) bankTranMatch.TranID
    }));
    graph.Caches[typeof (CABankTran)].Update((object) caBankTran);
    receipt.BankTranDate = new DateTime?();
  }

  protected virtual bool IsMatchedToExpenseReceipt(CABankTranMatch match)
  {
    return CABankTransactionsHelper.IsMatchedToExpenseReceipt(match);
  }

  protected virtual bool IsMatchedToInvoice(CABankTran tran, CABankTranMatch match)
  {
    return CABankTransactionsHelper.IsMatchedToInvoice(tran, match);
  }

  protected virtual bool IsCABatch(PX.Objects.CA.BankStatementHelpers.CATranExt tran, CABankTranMatch match)
  {
    return tran.OrigTranType == "CBT" && match.DocType == "CBT" && match.DocModule == "AP" && match.DocRefNbr == tran.OrigRefNbr;
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
    CABankTran aDetail,
    IMatchSettings aSettings,
    Pair<DateTime, DateTime> tranDateRange,
    string curyID,
    bool bestMatchOnly = false)
  {
    return this.CABankTransactionsRepository.SearchForMatchingTransactions((PXGraph) this, aDetail, aSettings, tranDateRange, curyID, bestMatchOnly);
  }

  public virtual PXResultset<CABatch> SearchForMatchingCABatches(
    CABankTran aDetail,
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
    CABankTran aRow)
  {
    return this.CABankTransactionsRepository.FindARInvoiceByInvoiceInfo((PXGraph) this, aRow);
  }

  public virtual PXResult<PX.Objects.AP.APInvoice, PX.Objects.AP.APAdjust, PX.Objects.AP.APPayment> FindAPInvoiceByInvoiceInfo(
    CABankTran aRow)
  {
    return this.CABankTransactionsRepository.FindAPInvoiceByInvoiceInfo((PXGraph) this, aRow);
  }

  public virtual string GetStatus(CATran tran) => StatementsMatchingProto.GetStatus(tran);

  public class MatchInvoiceContext : IDisposable
  {
    private CABankTransactionsMaint Graph { get; set; }

    public MatchInvoiceContext(CABankTransactionsMaint graph)
    {
      graph.MatchInvoiceProcess = true;
      this.Graph = graph;
    }

    public void Dispose() => this.Graph.MatchInvoiceProcess = false;
  }

  [Serializable]
  public class Filter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _CashAccountID;
    protected string _TranType;

    [CashAccount(typeof (AccessInfo.branchID), typeof (Search<CashAccount.cashAccountID, Where<Match<Current<AccessInfo.userName>>>>))]
    public virtual int? CashAccountID
    {
      get => this._CashAccountID;
      set => this._CashAccountID = value;
    }

    [PXDBString(1, IsFixed = true)]
    [PXDefault(typeof (CABankTranType.statement))]
    [CABankTranType.List]
    public virtual string TranType
    {
      get => this._TranType;
      set => this._TranType = value;
    }

    [PXUIField(DisplayName = "IsCorpCardCashAccount", Visible = false)]
    [PXBool]
    [PXFormula(typeof (Selector<CABankTransactionsMaint.Filter.cashAccountID, CashAccount.useForCorpCard>))]
    public bool? IsCorpCardCashAccount { get; set; }

    [PXDBString(5, IsUnicode = true)]
    [PXFormula(typeof (Selector<CABankTransactionsMaint.Filter.cashAccountID, CashAccount.baseCuryID>))]
    public virtual string BaseCuryID { get; set; }

    public abstract class cashAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CABankTransactionsMaint.Filter.cashAccountID>
    {
    }

    public abstract class tranType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsMaint.Filter.tranType>
    {
    }

    public abstract class isCorpCardCashAccount : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CABankTransactionsMaint.Filter.isCorpCardCashAccount>
    {
    }

    public abstract class baseCuryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsMaint.Filter.baseCuryID>
    {
    }
  }

  [PXHidden]
  public class MatchingLoadOptions : ARPaymentEntry.LoadOptions
  {
    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "From Ref. Nbr.")]
    [PXSelector(typeof (Search2<PX.Objects.AR.ARAdjust.ARInvoice.refNbr, LeftJoin<PX.Objects.AR.ARAdjust, On<PX.Objects.AR.ARAdjust.adjdDocType, Equal<PX.Objects.AR.ARAdjust.ARInvoice.docType>, And<PX.Objects.AR.ARAdjust.adjdRefNbr, Equal<PX.Objects.AR.ARAdjust.ARInvoice.refNbr>, And<PX.Objects.AR.ARAdjust.released, Equal<boolFalse>, And<PX.Objects.AR.ARAdjust.voided, Equal<boolFalse>, And<Where<PX.Objects.AR.ARAdjust.adjgDocType, NotEqual<Current<CABankTranAdjustment.adjdDocType>>>>>>>>, LeftJoin<CABankTranAdjustment, On<CABankTranAdjustment.adjdModule, Equal<BatchModule.moduleAR>, And<CABankTranAdjustment.adjdDocType, Equal<PX.Objects.AR.ARAdjust.ARInvoice.docType>, And<CABankTranAdjustment.adjdRefNbr, Equal<PX.Objects.AR.ARAdjust.ARInvoice.refNbr>, And<CABankTranAdjustment.released, Equal<boolFalse>, And<Where<CABankTranAdjustment.tranID, NotEqual<Current<CABankTranAdjustment.tranID>>, Or<Current<CABankTranAdjustment.adjNbr>, IsNull, Or<CABankTranAdjustment.adjNbr, NotEqual<Current<CABankTranAdjustment.adjNbr>>>>>>>>>>, LeftJoin<CABankTran, On<CABankTran.tranID, Equal<CABankTranAdjustment.tranID>>>>>, Where<PX.Objects.AR.ARAdjust.ARInvoice.customerID, In2<Search<PX.Objects.AR.Override.BAccount.bAccountID, Where<PX.Objects.AR.Override.BAccount.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>, Or<PX.Objects.AR.Override.BAccount.consolidatingBAccountID, Equal<Current<CABankTran.payeeBAccountID>>>>>>, And<PX.Objects.AR.ARAdjust.ARInvoice.released, Equal<boolTrue>, And<PX.Objects.AR.ARAdjust.ARInvoice.openDoc, Equal<boolTrue>, And<PX.Objects.AR.ARAdjust.adjgRefNbr, IsNull, And<PX.Objects.AR.ARAdjust.ARInvoice.pendingPPD, NotEqual<True>, And<Where<CABankTranAdjustment.adjdRefNbr, IsNull, Or<CABankTran.origModule, NotEqual<BatchModule.moduleAR>>>>>>>>>>))]
    public override string StartRefNbr { get; set; }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "To Ref. Nbr.")]
    [PXSelector(typeof (Search2<PX.Objects.AR.ARAdjust.ARInvoice.refNbr, LeftJoin<PX.Objects.AR.ARAdjust, On<PX.Objects.AR.ARAdjust.adjdDocType, Equal<PX.Objects.AR.ARAdjust.ARInvoice.docType>, And<PX.Objects.AR.ARAdjust.adjdRefNbr, Equal<PX.Objects.AR.ARAdjust.ARInvoice.refNbr>, And<PX.Objects.AR.ARAdjust.released, Equal<boolFalse>, And<PX.Objects.AR.ARAdjust.voided, Equal<boolFalse>, And<Where<PX.Objects.AR.ARAdjust.adjgDocType, NotEqual<Current<CABankTranAdjustment.adjdDocType>>>>>>>>, LeftJoin<CABankTranAdjustment, On<CABankTranAdjustment.adjdModule, Equal<BatchModule.moduleAR>, And<CABankTranAdjustment.adjdDocType, Equal<PX.Objects.AR.ARAdjust.ARInvoice.docType>, And<CABankTranAdjustment.adjdRefNbr, Equal<PX.Objects.AR.ARAdjust.ARInvoice.refNbr>, And<CABankTranAdjustment.released, Equal<boolFalse>, And<Where<CABankTranAdjustment.tranID, NotEqual<Current<CABankTranAdjustment.tranID>>, Or<Current<CABankTranAdjustment.adjNbr>, IsNull, Or<CABankTranAdjustment.adjNbr, NotEqual<Current<CABankTranAdjustment.adjNbr>>>>>>>>>>, LeftJoin<CABankTran, On<CABankTran.tranID, Equal<CABankTranAdjustment.tranID>>>>>, Where<PX.Objects.AR.ARAdjust.ARInvoice.customerID, In2<Search<PX.Objects.AR.Override.BAccount.bAccountID, Where<PX.Objects.AR.Override.BAccount.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>, Or<PX.Objects.AR.Override.BAccount.consolidatingBAccountID, Equal<Current<CABankTran.payeeBAccountID>>>>>>, And<PX.Objects.AR.ARAdjust.ARInvoice.released, Equal<boolTrue>, And<PX.Objects.AR.ARAdjust.ARInvoice.openDoc, Equal<boolTrue>, And<PX.Objects.AR.ARAdjust.adjgRefNbr, IsNull, And<PX.Objects.AR.ARAdjust.ARInvoice.pendingPPD, NotEqual<True>, And<Where<CABankTranAdjustment.adjdRefNbr, IsNull, Or<CABankTran.origModule, NotEqual<BatchModule.moduleAR>>>>>>>>>>))]
    public override string EndRefNbr { get; set; }

    public new abstract class startRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsMaint.MatchingLoadOptions.startRefNbr>
    {
    }

    public new abstract class endRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsMaint.MatchingLoadOptions.endRefNbr>
    {
    }
  }

  [Serializable]
  public class CreateRuleSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(30, IsUnicode = true, InputMask = ">AAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")]
    [PXUIField(DisplayName = "Rule ID", Required = true)]
    public virtual string RuleName { get; set; }

    public abstract class ruleName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CABankTransactionsMaint.CreateRuleSettings.ruleName>
    {
    }
  }

  public class GLCATranToExpenseReceiptMatchingGraphExtension<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
  {
    protected void _(PX.Data.Events.RowInserted<CATran> e)
    {
      if (!(e.Row.OrigTranType == "GLE") || !(e.Row.OrigModule == "AP"))
        return;
      CABankTranMatch caBankTranMatch = PXResult<EPExpenseClaimDetails, CABankTranMatch>.op_Implicit(this.GetExpenseReceiptWithBankTranMatching(e.Row));
      if (caBankTranMatch == null || !caBankTranMatch.TranID.HasValue)
        return;
      CABankTran caBankTran = CABankTran.PK.Find((PXGraph) this.Base, caBankTranMatch.TranID);
      e.Row.Cleared = new bool?(true);
      e.Row.ClearDate = caBankTran.TranDate;
    }

    protected void _(PX.Data.Events.RowPersisted<CATran> e)
    {
      if (e.Operation != 2 || e.TranStatus != null || !(e.Row.OrigTranType == "GLE") || !(e.Row.OrigModule == "AP"))
        return;
      PXResult<EPExpenseClaimDetails, CABankTranMatch> bankTranMatching = this.GetExpenseReceiptWithBankTranMatching(e.Row);
      if (bankTranMatching == null)
        return;
      CABankTranMatch bankTranMatch = PXResult<EPExpenseClaimDetails, CABankTranMatch>.op_Implicit(bankTranMatching);
      if (bankTranMatch == null || bankTranMatch.DocRefNbr == null)
        return;
      EPExpenseClaimDetails receipt = PXResult<EPExpenseClaimDetails, CABankTranMatch>.op_Implicit(bankTranMatching);
      CABankTransactionsMaint.RematchFromExpenseReceipt((PXGraph) this.Base, bankTranMatch, e.Row.TranID, e.Row.ReferenceID, receipt);
      this.Base.Caches[typeof (CABankTranMatch)].PersistUpdated((object) bankTranMatch);
      PXCache cach = this.Base.Caches[typeof (EPExpenseClaimDetails)];
      cach.GetAttributesOfType<PXDBTimestampAttribute>((object) null, "tstamp").First<PXDBTimestampAttribute>().VerifyTimestamp = (VerifyTimestampOptions) 1;
      cach.PersistUpdated((object) receipt);
    }

    private PXResult<EPExpenseClaimDetails, CABankTranMatch> GetExpenseReceiptWithBankTranMatching(
      CATran caTran)
    {
      PX.Objects.GL.GLTran glTran = PXResultset<PX.Objects.GL.GLTran>.op_Implicit(PXSelectBase<PX.Objects.GL.GLTran, PXSelect<PX.Objects.GL.GLTran, Where<PX.Objects.GL.GLTran.module, Equal<Required<PX.Objects.GL.GLTran.module>>, And<PX.Objects.GL.GLTran.batchNbr, Equal<Required<PX.Objects.GL.GLTran.batchNbr>>, And<PX.Objects.GL.GLTran.lineNbr, Equal<Required<PX.Objects.GL.GLTran.lineNbr>>, And<PX.Objects.GL.GLTran.tranType, Equal<APDocType.debitAdj>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
      {
        (object) caTran.OrigModule,
        (object) caTran.OrigRefNbr,
        (object) caTran.OrigLineNbr
      }));
      if (glTran == null)
        return (PXResult<EPExpenseClaimDetails, CABankTranMatch>) null;
      return ((IEnumerable<PXResult<EPExpenseClaimDetails>>) PXSelectBase<EPExpenseClaimDetails, PXSelectJoin<EPExpenseClaimDetails, LeftJoin<CABankTranMatch, On<EPExpenseClaimDetails.docType, Equal<CABankTranMatch.docType>, And<EPExpenseClaimDetails.claimDetailCD, Equal<CABankTranMatch.docRefNbr>, And<CABankTranMatch.docModule, Equal<BatchModule.moduleEP>>>>>, Where<EPExpenseClaimDetails.aPDocType, Equal<Required<EPExpenseClaimDetails.aPDocType>>, And<EPExpenseClaimDetails.aPRefNbr, Equal<Required<EPExpenseClaimDetails.aPRefNbr>>, And<EPExpenseClaimDetails.aPLineNbr, Equal<Required<EPExpenseClaimDetails.aPLineNbr>>>>>>.Config>.Select((PXGraph) this.Base, new object[3]
      {
        (object) glTran.TranType,
        (object) glTran.RefNbr,
        (object) glTran.TranLineNbr
      })).AsEnumerable<PXResult<EPExpenseClaimDetails>>().Cast<PXResult<EPExpenseClaimDetails, CABankTranMatch>>().SingleOrDefault<PXResult<EPExpenseClaimDetails, CABankTranMatch>>();
    }
  }

  public class CABankTransactionsMaintCustomerDocsExtension : 
    CustomerDocsExtensionBase<CABankTransactionsMaint>
  {
  }

  public class CABankTransactionsMaintVendorDocsExtension : 
    VendorDocsExtensionBase<CABankTransactionsMaint>
  {
  }
}
