// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CashAccountMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.CA;

[Serializable]
public class CashAccountMaint : PXGraph<
#nullable disable
CashAccountMaint, PX.Objects.CA.CashAccount>
{
  public PXAction<PX.Objects.CA.CashAccount> action;
  public CashAccountMaint.CashAccountChangeID ChangeID;
  public PXAction<PX.Objects.CA.CashAccount> redirectToCashAccountDetails;
  public PXAction<PX.Objects.CA.CashAccount> redirectToCashFlowForecast;
  public PXAction<PX.Objects.CA.CashAccount> redirectToReconciliationStatementHistory;
  public PXAction<PX.Objects.CA.CashAccount> redirectToBankTransactionsHistory;
  public PXSelectJoin<PX.Objects.CA.CashAccount, LeftJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<PX.Objects.CA.CashAccount.accountID>>, LeftJoin<PX.Objects.GL.Sub, On<PX.Objects.GL.Sub.subID, Equal<PX.Objects.CA.CashAccount.subID>>>>, Where2<Match<Current<AccessInfo.userName>>, And2<Where2<Match<PX.Objects.GL.Account, Current<AccessInfo.userName>>, Or<PX.Objects.GL.Account.accountID, IsNull>>, And<Where2<Match<PX.Objects.GL.Sub, Current<AccessInfo.userName>>, Or<PX.Objects.GL.Sub.subID, IsNull>>>>>> CashAccount;
  public PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Current<PX.Objects.CA.CashAccount.cashAccountID>>>> CurrentCashAccount;
  public PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Optional<PX.Objects.CA.CashAccount.accountID>>, And<Match<Current<AccessInfo.userName>>>>> Account_AccountID;
  public PXSelectReadonly<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Optional<PX.Objects.CA.CashAccount.accountID>>, And<Match<Current<AccessInfo.userName>>>>> Account_AccountID_Readonly;
  public PXSelect<CashAccountCheck, Where<CashAccountCheck.cashAccountID, Equal<Optional<PX.Objects.CA.CashAccount.cashAccountID>>>> CashAccountChecks;
  public PXSelectJoin<PaymentMethodAccount, InnerJoin<PaymentMethod, On<PaymentMethod.paymentMethodID, Equal<PaymentMethodAccount.paymentMethodID>>>, Where<PaymentMethodAccount.cashAccountID, Equal<Current<PX.Objects.CA.CashAccount.cashAccountID>>>> Details;
  [PXImport(typeof (PX.Objects.CA.CashAccount))]
  public PXSelectJoin<CashAccountETDetail, InnerJoin<CAEntryType, On<CAEntryType.entryTypeId, Equal<CashAccountETDetail.entryTypeID>>>, Where<CashAccountETDetail.cashAccountID, Equal<Current<PX.Objects.CA.CashAccount.cashAccountID>>>> ETDetails;
  public PXSelect<CAEntryType> EntryTypes;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (CashAccountDeposit.cashAccountID)})]
  public PXSelectJoin<CashAccountDeposit, InnerJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<CashAccountDeposit.depositAcctID>>>, Where<CashAccountDeposit.cashAccountID, Equal<Current<PX.Objects.CA.CashAccount.cashAccountID>>>> Deposits;
  public PXSelect<CashAccountMaint.PaymentMethodAccount2, Where<CashAccountMaint.PaymentMethodAccount2.cashAccountID, Equal<Current2<PX.Objects.CA.CashAccount.cashAccountID>>>> PaymentMethodForRemittance;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<CashAccountPaymentMethodDetail, InnerJoin<PaymentMethodDetail, On<PaymentMethodDetail.paymentMethodID, Equal<CashAccountPaymentMethodDetail.paymentMethodID>, And<PaymentMethodDetail.detailID, Equal<CashAccountPaymentMethodDetail.detailID>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForCashAccount>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>>, Where<CashAccountPaymentMethodDetail.cashAccountID, Equal<Optional2<PX.Objects.CA.CashAccount.cashAccountID>>, And<CashAccountPaymentMethodDetail.paymentMethodID, Equal<Optional2<CashAccountMaint.PaymentMethodAccount2.paymentMethodID>>>>, OrderBy<Asc<PaymentMethodDetail.orderIndex>>> PaymentDetails;
  public PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Optional2<PaymentMethodAccount.paymentMethodID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForCashAccount>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>> PaymentTypeDetails;
  public PXSelect<CADailySummary, Where<CADailySummary.cashAccountID, Equal<Required<PX.Objects.CA.CashAccount.cashAccountID>>>> CADailySummaryDetails;
  public PXSelect<VendorClass, Where<VendorClass.cashAcctID, Equal<Required<PX.Objects.CA.CashAccount.cashAccountID>>>> VendorClasses;
  public PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.vCashAccountID, Equal<Required<PX.Objects.CA.CashAccount.cashAccountID>>>> Locations;
  public PXSetup<PX.Objects.GL.Company> Company;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  public PXSetup<CASetup> casetup;
  private bool isPaymentMergedFlag;
  private Dictionary<string, string> remittancePMErrors = new Dictionary<string, string>();

  public CashAccountMaint()
  {
    PX.Objects.GL.GLSetup current1 = ((PXSelectBase<PX.Objects.GL.GLSetup>) this.GLSetup).Current;
    CASetup current2 = ((PXSelectBase<CASetup>) this.casetup).Current;
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(new PXFieldDefaulting((object) this, __methodptr(SetDefaultBaccountType)));
    ((PXSelectBase) this.PaymentMethodForRemittance).Cache.AllowInsert = false;
    ((PXSelectBase) this.PaymentMethodForRemittance).Cache.AllowDelete = false;
    ((PXSelectBase) this.PaymentDetails).Cache.AllowInsert = false;
    ((PXSelectBase) this.PaymentDetails).Cache.AllowDelete = false;
  }

  /// <summary>
  /// Sets default baccount type. Method is used as a workaround for the redirection problem with the edit button of the empty Bank ID field.
  /// </summary>
  private void SetDefaultBaccountType(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null)
      return;
    e.NewValue = (object) "VE";
  }

  [PXDBDefault(typeof (PX.Objects.CA.CashAccount.cashAccountID))]
  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Current<PaymentMethodAccount.cashAccountID>>>>))]
  protected virtual void PaymentMethodAccount_CashAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Action(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton(Category = "Inquiries")]
  public virtual IEnumerable RedirectToCashAccountDetails(PXAdapter adapter)
  {
    PX.Objects.CA.CashAccount current = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.CashAccount).Current;
    if (current != null)
    {
      CATranEnq instance = PXGraph.CreateInstance<CATranEnq>();
      ((PXSelectBase<CAEnqFilter>) instance.Filter).Current.CashAccountID = current.CashAccountID;
      ((PXSelectBase) instance.Filter).Cache.SetDefaultExt<CAEnqFilter.curyID>((object) ((PXSelectBase<CAEnqFilter>) instance.Filter).Current);
      throw new PXRedirectRequiredException((PXGraph) instance, true, "Cash Account Details");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Inquiries")]
  public virtual IEnumerable RedirectToCashFlowForecast(PXAdapter adapter)
  {
    PX.Objects.CA.CashAccount current = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.CashAccount).Current;
    if (current != null)
    {
      CashFlowEnq instance = PXGraph.CreateInstance<CashFlowEnq>();
      ((PXSelectBase<CashFlowEnq.CashFlowFilter>) instance.Filter).Current.CashAccountID = current.CashAccountID;
      throw new PXRedirectRequiredException((PXGraph) instance, true, "Cash Flow Forecast Enquiry");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Inquiries")]
  public virtual IEnumerable RedirectToReconciliationStatementHistory(PXAdapter adapter)
  {
    PX.Objects.CA.CashAccount current = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.CashAccount).Current;
    if (current != null)
    {
      CAReconEnq instance = PXGraph.CreateInstance<CAReconEnq>();
      ((PXSelectBase<CAEnqFilter>) instance.Filter).Current.CashAccountID = current.CashAccountID;
      throw new PXRedirectRequiredException((PXGraph) instance, true, "Reconciliation Statement History");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(Category = "Inquiries")]
  public virtual IEnumerable RedirectToBankTransactionsHistory(PXAdapter adapter)
  {
    PX.Objects.CA.CashAccount current = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.CashAccount).Current;
    if (current != null)
    {
      CABankTransactionsEnq instance = PXGraph.CreateInstance<CABankTransactionsEnq>();
      ((PXSelectBase<CABankTransactionsEnq.Filter>) instance.TranFilter).Current.CashAccountID = current.CashAccountID;
      throw new PXRedirectRequiredException((PXGraph) instance, true, "Bank Transactions History");
    }
    return adapter.Get();
  }

  [PXCancelButton]
  [PXUIField]
  protected IEnumerable Cancel(PXAdapter a)
  {
    CashAccountMaint cashAccountMaint = this;
    foreach (PXResult<PX.Objects.CA.CashAccount> pxResult in ((PXAction) new PXCancel<PX.Objects.CA.CashAccount>((PXGraph) cashAccountMaint, nameof (Cancel))).Press(a))
    {
      PX.Objects.CA.CashAccount cashAccount = PXResult<PX.Objects.CA.CashAccount>.op_Implicit(pxResult);
      if (cashAccount != null && ((PXSelectBase) cashAccountMaint.CashAccount).Cache.GetStatus((object) cashAccount) == 2)
      {
        using (new PXReadBranchRestrictedScope())
        {
          if (PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelectReadonly<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountCD, Equal<Required<PX.Objects.CA.CashAccount.cashAccountCD>>, And<PX.Objects.CA.CashAccount.cashAccountID, NotEqual<Required<PX.Objects.CA.CashAccount.cashAccountID>>>>>.Config>.Select((PXGraph) cashAccountMaint, new object[2]
          {
            (object) cashAccount.CashAccountCD,
            (object) cashAccount.CashAccountID
          })) != null)
            ((PXSelectBase) cashAccountMaint.CashAccount).Cache.RaiseExceptionHandling<PX.Objects.CA.CashAccount.cashAccountCD>((object) cashAccount, (object) cashAccount.CashAccountCD, (Exception) new PXSetPropertyException("This ID is already used for another Cash Account record."));
        }
      }
      yield return (object) cashAccount;
    }
  }

  public IEnumerable paymentMethodForRemittance()
  {
    CashAccountMaint cashAccountMaint = this;
    PXCache cache = ((PXGraph) cashAccountMaint).Caches[typeof (CashAccountMaint.PaymentMethodAccount2)];
    cache.AllowDelete = true;
    cache.AllowInsert = true;
    cache.Clear();
    foreach (PXResult<PaymentMethodAccount> paymentMethodAccount1 in cashAccountMaint.GetCurrentUsedPaymentMethodAccounts())
    {
      PaymentMethodAccount paymentMethodAccount2 = PXResult<PaymentMethodAccount>.op_Implicit(paymentMethodAccount1);
      PaymentMethod paymentMethod = PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) cashAccountMaint, new object[1]
      {
        (object) paymentMethodAccount2.PaymentMethodID
      }));
      if (paymentMethod != null && paymentMethod.UseForCA.GetValueOrDefault())
      {
        if (!string.IsNullOrEmpty(PXResultset<PaymentMethodDetail>.op_Implicit(PXSelectBase<PaymentMethodDetail, PXSelectReadonly<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Required<PaymentMethodDetail.paymentMethodID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForCashAccount>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>.Config>.Select((PXGraph) cashAccountMaint, new object[1]
        {
          (object) paymentMethodAccount2.PaymentMethodID
        }))?.PaymentMethodID))
        {
          CashAccountMaint.PaymentMethodAccount2 paymentMethodAccount2_1 = new CashAccountMaint.PaymentMethodAccount2()
          {
            PaymentMethodID = paymentMethodAccount2.PaymentMethodID,
            CashAccountID = paymentMethodAccount2.CashAccountID
          };
          cache.Insert((object) paymentMethodAccount2_1);
          string str;
          if (cashAccountMaint.remittancePMErrors.TryGetValue(paymentMethodAccount2_1.PaymentMethodID, out str))
          {
            cache.RaiseExceptionHandling<CashAccountMaint.PaymentMethodAccount2.paymentMethodID>((object) paymentMethodAccount2_1, (object) paymentMethodAccount2_1.PaymentMethodID, (Exception) new PXSetPropertyException(str));
            cashAccountMaint.remittancePMErrors.Remove(paymentMethodAccount2_1.PaymentMethodID);
          }
          yield return (object) paymentMethodAccount2_1;
        }
      }
    }
    cache.AllowDelete = false;
    cache.AllowInsert = false;
    cache.AllowUpdate = false;
    ((PXGraph) cashAccountMaint).Caches[typeof (CashAccountMaint.PaymentMethodAccount2)].IsDirty = false;
  }

  protected virtual void CashAccount_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CA.CashAccount row))
      return;
    if (this.CheckCashAccountForTransactions(row))
      throw new PXException("This Cash Account cannot be deleted as one or more transaction already exists.");
    this.CheckCashAccountInUse(sender, row);
  }

  protected virtual void CashAccount_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CA.CashAccount row))
      return;
    foreach (PXResult<CashAccountCheck> pxResult in ((PXSelectBase<CashAccountCheck>) this.CashAccountChecks).Select(new object[1]
    {
      (object) row.CashAccountID
    }))
      ((PXSelectBase<CashAccountCheck>) this.CashAccountChecks).Delete(PXResult<CashAccountCheck>.op_Implicit(pxResult));
    using (new PXReadBranchRestrictedScope())
    {
      PX.Objects.CA.CashAccount cashAccount = PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.accountID, Equal<Required<PX.Objects.CA.CashAccount.accountID>>, And<PX.Objects.CA.CashAccount.cashAccountID, NotEqual<Required<PX.Objects.CA.CashAccount.cashAccountID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.AccountID,
        (object) row.CashAccountID
      }));
      PX.Objects.GL.Account account = ((PXSelectBase<PX.Objects.GL.Account>) this.Account_AccountID).SelectSingle(new object[1]
      {
        (object) row.AccountID
      });
      if (account != null)
      {
        if (account.IsCashAccount.GetValueOrDefault())
        {
          if (cashAccount == null)
          {
            account.IsCashAccount = new bool?(false);
            ((PXSelectBase<PX.Objects.GL.Account>) this.Account_AccountID).Update(account);
          }
        }
      }
    }
    this.DeleteCashAccountInUse(row);
  }

  protected virtual void CashAccount_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.CashAccount.curyID>(((PXGraph) this).Caches[typeof (PX.Objects.CA.CashAccount)], (object) null, false);
    PXUIFieldAttribute.SetVisible<PX.Objects.CA.CashAccount.curyRateTypeID>(sender, (object) null, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
    PX.Objects.CA.CashAccount row = (PX.Objects.CA.CashAccount) e.Row;
    if (row == null)
      return;
    this.RecalcOptions(row);
    bool flag1 = false;
    bool flag2 = row.ClearingAccount.Value;
    if (flag2)
      flag1 = PXResultset<CashAccountDeposit>.op_Implicit(PXSelectBase<CashAccountDeposit, PXSelect<CashAccountDeposit, Where<CashAccountDeposit.depositAcctID, Equal<Required<CashAccountDeposit.depositAcctID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.CashAccountID
      })) != null;
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.CashAccount.clearingAccount>(sender, (object) row, !flag1 || !flag2);
    bool valueOrDefault = ((PXSelectBase<CASetup>) this.casetup).Current.ImportToSingleAccount.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<PX.Objects.CA.CashAccount.statementImportTypeName>(sender, (object) row, valueOrDefault);
    ((PXSelectBase) this.Deposits).Cache.AllowInsert = !row.ClearingAccount.Value;
    ((PXSelectBase) this.Deposits).Cache.AllowUpdate = !row.ClearingAccount.Value;
    bool flag3 = this.CheckCashAccountForTransactions(row);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.CashAccount.accountID>(sender, (object) row, !flag3);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.CashAccount.subID>(sender, (object) row, !flag3);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CA.CashAccount.branchID>(sender, (object) row, !flag3);
    int num1;
    if (sender.GetStatus((object) row) == 2)
    {
      int? cashAccountId = row.CashAccountID;
      int num2 = 0;
      num1 = !(cashAccountId.GetValueOrDefault() < num2 & cashAccountId.HasValue) ? 1 : 0;
    }
    else
      num1 = 1;
    bool flag4 = num1 != 0;
    ((PXAction) this.redirectToCashAccountDetails).SetEnabled(flag4);
    ((PXAction) this.redirectToCashFlowForecast).SetEnabled(flag4);
    ((PXAction) this.redirectToReconciliationStatementHistory).SetEnabled(flag4);
    ((PXAction) this.redirectToBankTransactionsHistory).SetEnabled(flag4);
  }

  protected virtual void CashAccount_AccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    PX.Objects.CA.CashAccount row = (PX.Objects.CA.CashAccount) e.Row;
    if (e.NewValue == null)
      return;
    PX.Objects.GL.Account acct = ((PXSelectBase<PX.Objects.GL.Account>) this.Account_AccountID).SelectSingle(new object[1]
    {
      (object) (int) e.NewValue
    });
    if (string.IsNullOrEmpty(acct.CuryID))
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      {
        e.NewValue = (object) acct.AccountCD;
        throw new PXSetPropertyException("Only a denominated GL account can be linked to a cash account. For the account, specify a currency of denomination in the Currency column on the Chart of Accounts (GL202500) form.", (PXErrorLevel) 4);
      }
      row.CuryID = ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID;
      row.BranchID = ((PXGraph) this).Accessinfo.BranchID;
      acct = PXCache<PX.Objects.GL.Account>.CreateCopy(acct);
      acct.CuryID = ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID;
      ((PXSelectBase<PX.Objects.GL.Account>) this.Account_AccountID).Update(acct);
    }
    else
    {
      row.CuryID = acct.CuryID;
      row.BranchID = ((PXGraph) this).Accessinfo.BranchID;
    }
    List<string> accountUsage = this.GetAccountUsage(acct);
    if (accountUsage.Any<string>())
    {
      e.NewValue = (object) acct.AccountCD;
      throw new PXSetPropertyException("The {0} account cannot be used as a cash account because there are open documents associated with the account in the following subledgers: {1}.", new object[2]
      {
        (object) acct.AccountCD,
        (object) string.Join(", ", (IEnumerable<string>) accountUsage)
      });
    }
    row.Descr = acct.Description;
    if (!PXDBLocalizableStringAttribute.IsEnabled || !(((PXSelectBase) this.Account_AccountID).Cache.GetValueExt((object) acct, "DescriptionTranslations") is string[] valueExt))
      return;
    sender.SetValueExt((object) row, "DescrTranslations", (object) valueExt);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.CA.CashAccount.accountID> e)
  {
    if (((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.CA.CashAccount.accountID>, object, object>) e).OldValue == null || e.NewValue == ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.CA.CashAccount.accountID>, object, object>) e).OldValue)
      return;
    ((PXSelectBase) this.Account_AccountID).Cache.Remove((object) (PX.Objects.GL.Account) ((PXSelectBase) this.Account_AccountID).Cache.Locate((object) ((PXSelectBase<PX.Objects.GL.Account>) this.Account_AccountID).SelectSingle(new object[1]
    {
      (object) (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.CA.CashAccount.accountID>, object, object>) e).OldValue
    })));
  }

  private List<string> GetAccountUsage(PX.Objects.GL.Account acct)
  {
    List<string> accountUsage = new List<string>();
    if (((IQueryable<PXResult<PX.Objects.AP.APInvoice>>) PXSelectBase<PX.Objects.AP.APInvoice, PXViewOf<PX.Objects.AP.APInvoice>.BasedOn<SelectFromBase<PX.Objects.AP.APInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APInvoice.status, Equal<APDocStatus.open>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlOperand<PX.Objects.AP.APInvoice.aPAccountID, IBqlInt>.IsEqual<P.AsInt>>>>.Or<BqlOperand<PX.Objects.AP.APRegister.retainageAcctID, IBqlInt>.IsEqual<P.AsInt>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) acct.AccountID,
      (object) acct.AccountID
    })).Any<PXResult<PX.Objects.AP.APInvoice>>())
      accountUsage.Add("AP");
    if (((IQueryable<PXResult<PX.Objects.AR.ARInvoice>>) PXSelectBase<PX.Objects.AR.ARInvoice, PXViewOf<PX.Objects.AR.ARInvoice>.BasedOn<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.status, In3<ARDocStatus.open, ARDocStatus.unapplied>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlOperand<PX.Objects.AR.ARInvoice.aRAccountID, IBqlInt>.IsEqual<P.AsInt>>>>.Or<BqlOperand<ARRegister.retainageAcctID, IBqlInt>.IsEqual<P.AsInt>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) acct.AccountID,
      (object) acct.AccountID
    })).Any<PXResult<PX.Objects.AR.ARInvoice>>())
      accountUsage.Add("AR");
    return accountUsage;
  }

  protected virtual void CashAccount_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    PX.Objects.CA.CashAccount row = e.Row as PX.Objects.CA.CashAccount;
    if (row.ReconNumberingID != null || !row.Reconcile.GetValueOrDefault())
      return;
    sender.RaiseExceptionHandling<PX.Objects.CA.CashAccount.reconNumberingID>((object) row, (object) row.ReconNumberingID, (Exception) new PXSetPropertyException("Requires Reconciliation Numbering"));
  }

  protected virtual void CashAccount_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is PX.Objects.CA.CashAccount row))
      return;
    if (((PXSelectBase) this.CashAccount).Cache.GetStatus((object) row) == 2)
    {
      using (new PXReadBranchRestrictedScope())
      {
        if (PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelectReadonly<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountCD, Equal<Required<PX.Objects.CA.CashAccount.cashAccountCD>>, And<PX.Objects.CA.CashAccount.cashAccountID, NotEqual<Required<PX.Objects.CA.CashAccount.cashAccountID>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) row.CashAccountCD,
          (object) row.CashAccountID
        })) != null)
        {
          ((PXSelectBase) this.CashAccount).Cache.RaiseExceptionHandling<PX.Objects.CA.CashAccount.cashAccountCD>((object) row, (object) row.CashAccountCD, (Exception) new PXSetPropertyException("This ID is already used for another Cash Account record."));
          throw new PXSetPropertyException("This ID is already used for another Cash Account record.");
        }
      }
    }
    row.BaseCuryID = PXAccess.GetBranch(row.BranchID)?.BaseCuryID;
    bool? nullable1;
    if (row.ReconNumberingID == null)
    {
      nullable1 = row.Reconcile;
      if (nullable1.GetValueOrDefault())
        sender.RaiseExceptionHandling<PX.Objects.CA.CashAccount.reconNumberingID>((object) row, (object) row.ReconNumberingID, (Exception) new PXSetPropertyException("Requires Reconciliation Numbering"));
    }
    if (PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.accountID, Equal<Current<PX.Objects.CA.CashAccount.accountID>>, And<PX.Objects.CA.CashAccount.subID, Equal<Current<PX.Objects.CA.CashAccount.subID>>, And<PX.Objects.CA.CashAccount.branchID, Equal<Current<PX.Objects.CA.CashAccount.branchID>>, And<PX.Objects.CA.CashAccount.cashAccountID, NotEqual<Current<PX.Objects.CA.CashAccount.cashAccountID>>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())) != null)
      throw new PXException("Cash account for this account, sub account and branch already exist");
    int? nullable2 = row.CashAccountID;
    if (nullable2.HasValue)
    {
      nullable2 = row.CashAccountID;
      if (nullable2.GetValueOrDefault() != -1)
      {
        PXEntryStatus status = ((PXSelectBase) this.CashAccount).Cache.GetStatus(e.Row);
        if (status != 4 && status != 3 && status == 2 && this.HasGLTrans(row.AccountID, row.SubID, row.BranchID))
          sender.RaiseExceptionHandling<PX.Objects.CA.CashAccount.cashAccountCD>(e.Row, (object) row.CashAccountCD, (Exception) new PXSetPropertyException("One or more transactions recorded on the selected General Ledger account are not tracked in the Cash Management module. To synchronize the balances of the specified accounts, validate the balance of the cash account on the Validate Account Balances (CA.50.30.00) form.", (PXErrorLevel) 2));
      }
    }
    if (((PXSelectBase<CASetup>) this.casetup).Current != null)
    {
      nullable2 = ((PXSelectBase<CASetup>) this.casetup).Current.TransitAcctId;
      int? accountId = row.AccountID;
      if (nullable2.GetValueOrDefault() == accountId.GetValueOrDefault() & nullable2.HasValue == accountId.HasValue)
      {
        PXUIFieldAttribute.SetError<PX.Objects.CA.CashAccount.accountID>(sender, (object) row, "Cash-in-Transit account cannot be Cash Account.");
        throw new PXSetPropertyException<PX.Objects.CA.CashAccount.accountID>("Cash-in-Transit account cannot be Cash Account.");
      }
    }
    nullable1 = row.UseForCorpCard;
    if (nullable1.GetValueOrDefault())
    {
      nullable1 = row.ClearingAccount;
      if (nullable1.GetValueOrDefault())
        throw new PXRowPersistingException(typeof (PX.Objects.CA.CashAccount.clearingAccount).Name, (object) row.ClearingAccount, "The cash account configured for corporate cards cannot be set up as a clearing account.");
      PXResultset<PaymentMethodAccount> pxResultset = ((PXSelectBase<PaymentMethodAccount>) this.Details).Select(Array.Empty<object>());
      PaymentMethodAccount paymentMethodAccount = (pxResultset.Count == 1 ? PXResultset<PaymentMethodAccount>.op_Implicit(pxResultset) : throw new PXRowPersistingException(sender.GetItemType().Name, (object) row, "The cash account configured for corporate cards should have a single associated payment method.")) ?? throw new PXException("The payment method for the cash account cannot be found in the system.");
      PaymentMethod paymentMethod = PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) paymentMethodAccount.PaymentMethodID
      })) ?? throw new PXException("The {0} payment method cannot be found in the system.", new object[1]
      {
        (object) paymentMethodAccount.PaymentMethodID
      });
      nullable1 = paymentMethod.APRequirePaymentRef;
      if (nullable1.GetValueOrDefault() || paymentMethod.APAdditionalProcessing != "N")
        throw new PXRowPersistingException(typeof (PX.Objects.CA.CashAccount.clearingAccount).Name, (object) row.ClearingAccount, "For a cash account configured for corporate cards, you can select only a payment method with the following settings specified on the Settings for Use in AP tab of the Payment Methods (CA204000) form: the Require Unique Payment Ref. check box is cleared and the Not Required option is selected in the Additional Processing section.");
    }
    nullable1 = row.ClearingAccount;
    if (nullable1.GetValueOrDefault())
    {
      CAEntryType caEntryType = PXResultset<CAEntryType>.op_Implicit(PXSelectBase<CAEntryType, PXSelect<CAEntryType, Where<CAEntryType.useToReclassifyPayments, Equal<True>, And<CAEntryType.branchID, Equal<Required<CAEntryType.branchID>>, And<CAEntryType.accountID, Equal<Required<CAEntryType.accountID>>, And<CAEntryType.subID, Equal<Required<CAEntryType.subID>>>>>>>.Config>.Select((PXGraph) this, new object[3]
      {
        (object) row.BranchID,
        (object) row.AccountID,
        (object) row.SubID
      }));
      if (caEntryType != null)
        throw new PXRowPersistingException(typeof (PX.Objects.CA.CashAccount.clearingAccount).Name, (object) row.ClearingAccount, "The {0} cash account cannot be configured as a clearing account because it is specified as a reclassification account in the {1} entry type. Clear the Clearing Account check box or select another reclassification account for the {1} entry type.", new object[2]
        {
          (object) row.CashAccountCD,
          (object) caEntryType.EntryTypeId
        });
    }
    if (((PXSelectBase) this.CashAccount).Cache.GetStatus((object) row) != 2 && ((PXSelectBase) this.CashAccount).Cache.GetStatus((object) row) != 1)
      return;
    nullable1 = ((PXSelectBase<PX.Objects.GL.Account>) this.Account_AccountID_Readonly).SelectSingle(new object[1]
    {
      (object) row.AccountID
    }).IsCashAccount;
    if (nullable1.GetValueOrDefault())
      return;
    this.MarkGLAccoutAsCashAccount(row.AccountID);
  }

  protected virtual void CashAccount_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    PX.Objects.CA.CashAccount row = e.Row as PX.Objects.CA.CashAccount;
    PX.Objects.CA.CashAccount oldRow = e.OldRow as PX.Objects.CA.CashAccount;
    if (sender.ObjectsEqual<PX.Objects.CA.CashAccount.accountID>((object) row, (object) oldRow))
      return;
    PX.Objects.CA.CashAccount[] cashAccountArray = new PX.Objects.CA.CashAccount[2]
    {
      row,
      oldRow
    };
    foreach (PX.Objects.CA.CashAccount cashAccount in cashAccountArray)
    {
      if (cashAccount.AccountID.HasValue)
        this.MarkGLAccoutAsCashAccount(cashAccount.AccountID);
    }
  }

  private void MarkGLAccoutAsCashAccount(int? accountID)
  {
    PX.Objects.GL.Account account = ((PXSelectBase<PX.Objects.GL.Account>) this.Account_AccountID).SelectSingle(new object[1]
    {
      (object) accountID
    });
    using (new PXReadBranchRestrictedScope())
    {
      bool flag1 = PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.accountID, Equal<Required<PX.Objects.CA.CashAccount.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) accountID
      }).Count > 0;
      if (account == null)
        return;
      bool? isCashAccount = account.IsCashAccount;
      bool flag2 = flag1;
      if (isCashAccount.GetValueOrDefault() == flag2 & isCashAccount.HasValue)
        return;
      PX.Objects.GL.Account copy = PXCache<PX.Objects.GL.Account>.CreateCopy(account);
      copy.IsCashAccount = new bool?(flag1);
      if (flag1 && copy.PostOption != "D")
        copy.PostOption = "D";
      ((PXSelectBase<PX.Objects.GL.Account>) this.Account_AccountID).Update(copy);
    }
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.CA.CashAccount> e)
  {
    if (!((PXGraph) this).IsImport)
      return;
    PX.Objects.GL.Account account = ((PXSelectBase<PX.Objects.GL.Account>) this.Account_AccountID).SelectSingle(new object[1]
    {
      (object) e.Row.AccountID
    });
    if (account == null)
      return;
    PX.Objects.GL.Account copy = PXCache<PX.Objects.GL.Account>.CreateCopy(account);
    copy.IsCashAccount = new bool?(true);
    copy.PostOption = "D";
    ((PXSelectBase<PX.Objects.GL.Account>) this.Account_AccountID).Update(copy);
  }

  protected virtual void CashAccount_ClearingAccount_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!((PX.Objects.CA.CashAccount) e.Row).ClearingAccount.GetValueOrDefault())
      return;
    foreach (PXResult<CashAccountDeposit> pxResult in ((PXSelectBase<CashAccountDeposit>) this.Deposits).Select(Array.Empty<object>()))
      ((PXSelectBase<CashAccountDeposit>) this.Deposits).Delete(PXResult<CashAccountDeposit>.op_Implicit(pxResult));
  }

  protected virtual void CashAccount_Reconcile_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PX.Objects.CA.CashAccount row = (PX.Objects.CA.CashAccount) e.Row;
    bool? reconcile = row.Reconcile;
    bool flag = false;
    if (!(reconcile.GetValueOrDefault() == flag & reconcile.HasValue))
      return;
    row.ReconNumberingID = (string) null;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.CA.CashAccount, PX.Objects.CA.CashAccount.branchID> e)
  {
    PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(e.Row.BranchID);
    e.Row.BaseCuryID = branch.BaseCuryID;
    if (!this.ValidateClearingAccount(e.Row.BranchID))
      return;
    BranchBaseAttribute.VerifyFieldInPXCache<CashAccountDeposit, CashAccountDeposit.depositAcctID>((PXGraph) this, ((PXSelectBase<CashAccountDeposit>) this.Deposits).Select(Array.Empty<object>()));
  }

  protected virtual void CashAccount_ClearingAccount_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    PX.Objects.CA.CashAccount row = (PX.Objects.CA.CashAccount) e.Row;
    if ((bool) e.NewValue && ((PXSelectBase<CashAccountDeposit>) this.Deposits).Any<CashAccountDeposit>())
    {
      e.NewValue = (object) false;
      throw new PXSetPropertyException("A cash account that has one or more clearing accounts cannot be defined as a clearing account.", (PXErrorLevel) 4);
    }
  }

  protected virtual void PaymentMethodAccount_UseForAP_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    if (row == null)
      return;
    bool? nullable = PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(((PXSelectBase<PX.Objects.CA.CashAccount>) this.CurrentCashAccount).Select(Array.Empty<object>())).ClearingAccount;
    if (nullable ?? false)
    {
      e.NewValue = (object) false;
    }
    else
    {
      if (string.IsNullOrEmpty(row.PaymentMethodID))
        return;
      PaymentMethod paymentMethod = PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.PaymentMethodID
      }));
      PXFieldDefaultingEventArgs defaultingEventArgs = e;
      int num;
      if (paymentMethod != null)
      {
        nullable = paymentMethod.UseForAP;
        num = nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num = 0;
      // ISSUE: variable of a boxed type
      __Boxed<bool> local = (ValueType) (bool) num;
      defaultingEventArgs.NewValue = (object) local;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void PaymentMethodAccount_UseForAR_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    if (string.IsNullOrEmpty(row?.PaymentMethodID))
      return;
    PaymentMethod paymentMethod = PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.PaymentMethodID
    }));
    e.NewValue = (object) (bool) (paymentMethod == null ? 0 : (paymentMethod.UseForAR.GetValueOrDefault() ? 1 : 0));
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PaymentMethodAccount_APQuickBatchGeneration_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    PaymentMethod paymentMethod = PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.PaymentMethodID
    }));
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    int num;
    if (row != null)
    {
      bool? nullable = row.UseForAP;
      if (nullable.GetValueOrDefault())
      {
        if (paymentMethod == null)
        {
          num = 0;
          goto label_6;
        }
        nullable = paymentMethod.APCreateBatchPayment;
        num = nullable.GetValueOrDefault() ? 1 : 0;
        goto label_6;
      }
    }
    num = 0;
label_6:
    // ISSUE: variable of a boxed type
    __Boxed<bool> local = (ValueType) (bool) num;
    defaultingEventArgs.NewValue = (object) local;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PaymentMethodAccount_UseForAP_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    if (string.IsNullOrEmpty(row?.PaymentMethodID))
      return;
    PaymentMethod paymentMethod = PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.PaymentMethodID
    }));
    if ((paymentMethod != null ? (!paymentMethod.UseForAP.GetValueOrDefault() ? 1 : 0) : 1) != 0 && (bool) e.NewValue)
    {
      e.NewValue = (object) false;
      throw new PXSetPropertyException("The {0} payment method cannot be used in AP. Use the Payment Methods (CA204000) form to modify the payment method settings.", new object[1]
      {
        (object) row.PaymentMethodID
      });
    }
  }

  protected virtual void PaymentMethodAccount_UseForAR_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    if (string.IsNullOrEmpty(row?.PaymentMethodID))
      return;
    PaymentMethod paymentMethod = PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.PaymentMethodID
    }));
    if ((paymentMethod != null ? (!paymentMethod.UseForAR.GetValueOrDefault() ? 1 : 0) : 1) != 0 && (bool) e.NewValue)
    {
      e.NewValue = (object) false;
      throw new PXSetPropertyException("The {0} payment method cannot be used in AR. Use the Payment Methods (CA204000) form to modify the payment method settings.", new object[1]
      {
        (object) row.PaymentMethodID
      });
    }
  }

  protected virtual void PaymentMethodAccount_APQuickBatchGeneration_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    PaymentMethodAccountHelper.APQuickBatchGenerationFieldVerifying(cache, e);
  }

  protected virtual void PaymentMethodAccount_BatchLastRefNbr_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    if (row == null || string.IsNullOrEmpty(row.PaymentMethodID))
      return;
    if (PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.PaymentMethodID
    })).APCreateBatchPayment.GetValueOrDefault())
    {
      e.NewValue = (object) "00000000";
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      e.NewValue = (object) null;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void PaymentMethodAccount_PaymentMethodID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (string.IsNullOrEmpty(((PaymentMethodAccount) e.Row)?.PaymentMethodID))
      return;
    cache.SetDefaultExt<PaymentMethodAccount.aPBatchLastRefNbr>(e.Row);
    cache.SetDefaultExt<PaymentMethodAccount.useForAR>(e.Row);
    cache.SetDefaultExt<PaymentMethodAccount.useForAP>(e.Row);
    cache.SetDefaultExt<PaymentMethodAccount.aPQuickBatchGeneration>(e.Row);
  }

  protected virtual void PaymentMethodAccount_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    PX.Objects.CA.CashAccount current = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.CashAccount).Current;
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    if (current != null)
      GraphHelper.MarkUpdated(((PXSelectBase) this.CashAccount).Cache, (object) current);
    ((PXSelectBase) this.PaymentDetails).View.RequestRefresh();
  }

  protected virtual void PaymentMethodAccount_RowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    PaymentMethodAccount newRow = (PaymentMethodAccount) e.NewRow;
    if (newRow == null || !(row.PaymentMethodID != newRow.PaymentMethodID))
      return;
    foreach (PXResult<PaymentMethodAccount> pxResult in ((PXSelectBase<PaymentMethodAccount>) this.Details).Select(Array.Empty<object>()))
    {
      PaymentMethodAccount paymentMethodAccount = PXResult<PaymentMethodAccount>.op_Implicit(pxResult);
      if (row != paymentMethodAccount && newRow != paymentMethodAccount && paymentMethodAccount.PaymentMethodID == newRow.PaymentMethodID)
        throw new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefixNLA("Payment method '{0}' is already added to this Cash Account", new object[1]
        {
          (object) newRow.PaymentMethodID
        }));
    }
  }

  protected virtual void PaymentMethodAccount_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    PXException error = (PXException) null;
    if (row.APQuickBatchGeneration.GetValueOrDefault() && !PaymentMethodAccountHelper.TryToVerifyAPLastReferenceNumber<PaymentMethodAccount.aPQuickBatchGeneration>(row.APAutoNextNbr, row.APLastRefNbr, (string) null, out error))
      cache.RaiseExceptionHandling<PaymentMethodAccount.aPQuickBatchGeneration>((object) row, (object) row.APQuickBatchGeneration, (Exception) error);
    else
      cache.RaiseExceptionHandling<PaymentMethodAccount.aPQuickBatchGeneration>((object) row, (object) row.APQuickBatchGeneration, (Exception) null);
  }

  protected virtual void PaymentMethodAccount_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    if (row == null)
      return;
    if (row.PaymentMethodID != null)
    {
      foreach (PXResult<PaymentMethodAccount> pxResult in ((PXSelectBase<PaymentMethodAccount>) this.Details).Select(Array.Empty<object>()))
      {
        PaymentMethodAccount paymentMethodAccount = PXResult<PaymentMethodAccount>.op_Implicit(pxResult);
        if (row != paymentMethodAccount && paymentMethodAccount.PaymentMethodID == row.PaymentMethodID)
          throw new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefixNLA("Payment method '{0}' is already added to this Cash Account", new object[1]
          {
            (object) row.PaymentMethodID
          }));
      }
    }
    if (row.APIsDefault.GetValueOrDefault() && string.IsNullOrEmpty(row.PaymentMethodID))
      throw new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<PaymentMethodAccount.paymentMethodID>(cache)
      });
  }

  protected virtual void PaymentMethodAccount_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    PX.Objects.CA.CashAccount current = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.CashAccount).Current;
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    bool? nullable;
    int num1;
    if (row == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = row.UseForAP;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    bool flag1 = num1 != 0;
    int num2;
    if (row == null)
    {
      num2 = 0;
    }
    else
    {
      nullable = row.UseForAR;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    bool flag2 = num2 != 0;
    if (row != null && !string.IsNullOrEmpty(row.PaymentMethodID))
    {
      PaymentMethod paymentMethod = PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.PaymentMethodID
      }));
      int num3;
      if (paymentMethod == null)
      {
        num3 = 0;
      }
      else
      {
        nullable = paymentMethod.APCreateBatchPayment;
        num3 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      bool flag3 = num3 != 0;
      PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.aPBatchLastRefNbr>(cache, (object) row, flag1 & flag3);
      PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.aPQuickBatchGeneration>(cache, e.Row, flag1 & flag3);
      PaymentMethodAccountHelper.VerifyAPAutoNextNbr(cache, row, paymentMethod);
      PXException error = (PXException) null;
      nullable = row.APQuickBatchGeneration;
      if (nullable.GetValueOrDefault())
        PaymentMethodAccountHelper.TryToVerifyAPLastReferenceNumber<PaymentMethodAccount.aPQuickBatchGeneration>(row.APAutoNextNbr, row.APLastRefNbr, (string) null, out error);
      cache.RaiseExceptionHandling<PaymentMethodAccount.aPQuickBatchGeneration>((object) row, (object) row.APQuickBatchGeneration, (Exception) error);
    }
    PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.aPIsDefault>(cache, e.Row, false);
    PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.aPAutoNextNbr>(cache, e.Row, flag1);
    PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.aPLastRefNbr>(cache, e.Row, flag1);
    PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.aRIsDefault>(cache, e.Row, false);
    PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.aRIsDefaultForRefund>(cache, e.Row, false);
    PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.aRAutoNextNbr>(cache, e.Row, flag2);
    PXUIFieldAttribute.SetEnabled<PaymentMethodAccount.aRLastRefNbr>(cache, e.Row, flag2);
    if (row == null)
      return;
    PXEntryStatus status = cache.GetStatus(e.Row);
    if (status == 3 || status == 4)
      return;
    this.isPaymentMergedFlag = false;
    this.FillPaymentDetails(row.PaymentMethodID);
  }

  protected virtual void PaymentMethodAccount_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    PaymentMethodAccount row = (PaymentMethodAccount) e.Row;
    PXDefaultAttribute.SetPersistingCheck<PaymentMethodAccount.aPLastRefNbr>(sender, e.Row, row.APAutoNextNbr.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    bool? nullable = row.APAutoNextNbr;
    if (nullable.GetValueOrDefault() && row.APLastRefNbr == null)
      sender.RaiseExceptionHandling<PaymentMethodAccount.aPAutoNextNbr>((object) row, (object) row.APAutoNextNbr, (Exception) new PXSetPropertyException("To use the {0} - Suggest Next Number option you must specify the {0} Last Reference Number.", new object[1]
      {
        (object) "AP"
      }));
    nullable = row.ARAutoNextNbr;
    if (nullable.GetValueOrDefault() && row.ARLastRefNbr == null)
      sender.RaiseExceptionHandling<PaymentMethodAccount.aRAutoNextNbr>((object) row, (object) row.ARAutoNextNbr, (Exception) new PXSetPropertyException("To use the {0} - Suggest Next Number option you must specify the {0} Last Reference Number.", new object[1]
      {
        (object) "AR"
      }));
    PaymentMethodAccountHelper.VerifyQuickBatchGenerationOnRowPersisting(sender, row);
  }

  protected virtual void PaymentMethodAccount2_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CashAccountETDetail_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    if (PXResultset<CAEntryType>.op_Implicit(PXSelectBase<CAEntryType, PXSelect<CAEntryType, Where<CAEntryType.entryTypeId, Equal<Required<CAEntryType.entryTypeId>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) ((CashAccountETDetail) e.Row).EntryTypeID
    })) != null)
      return;
    cache.RaiseExceptionHandling<CashAccountETDetail.entryTypeID>(e.Row, (object) ((CashAccountETDetail) e.Row).EntryTypeID, (Exception) new PXException("This Entry Type ID does not exist"));
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CashAccountETDetail_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    CashAccountETDetail newRow = e.NewRow as CashAccountETDetail;
    if (!newRow.OffsetAccountID.HasValue)
      return;
    if (!PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) newRow.OffsetAccountID
    })).IsCashAccount.GetValueOrDefault())
      return;
    if (PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.accountID, Equal<Required<PX.Objects.CA.CashAccount.accountID>>, And<PX.Objects.CA.CashAccount.subID, Equal<Required<PX.Objects.CA.CashAccount.subID>>, And<PX.Objects.CA.CashAccount.branchID, Equal<Required<PX.Objects.CA.CashAccount.branchID>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) newRow.OffsetAccountID,
      (object) newRow.OffsetSubID,
      (object) newRow.OffsetBranchID
    })) != null)
      return;
    string field1 = (string) PXSelectorAttribute.GetField(sender, (object) newRow, typeof (CashAccountETDetail.offsetBranchID).Name, (object) newRow.OffsetBranchID, typeof (PX.Objects.GL.Branch.branchCD).Name);
    sender.RaiseExceptionHandling<CashAccountETDetail.offsetBranchID>((object) newRow, (object) field1, (Exception) new PXSetPropertyException("There is no Cash Account matching these Branch and Subaccount", (PXErrorLevel) 4));
    string field2 = (string) PXSelectorAttribute.GetField(sender, (object) newRow, typeof (CashAccountETDetail.offsetSubID).Name, (object) newRow.OffsetSubID, typeof (PX.Objects.GL.Sub.subCD).Name);
    sender.RaiseExceptionHandling<CashAccountETDetail.offsetSubID>((object) newRow, (object) field2, (Exception) new PXSetPropertyException("There is no Cash Account matching these Branch and Subaccount", (PXErrorLevel) 4));
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CashAccountETDetail_EntryTypeID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    CashAccountETDetail row = (CashAccountETDetail) e.Row;
    cache.SetDefaultExt<CashAccountETDetail.offsetAccountID>(e.Row);
    cache.SetDefaultExt<CashAccountETDetail.offsetSubID>(e.Row);
  }

  protected virtual void CashAccountETDetail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    this.IsCorrectCashAccount(sender, e.Row);
    this.SetupDefaultEntryType(sender, e);
  }

  private void SetupDefaultEntryType(PXCache sender, PXRowUpdatedEventArgs e)
  {
    CashAccountETDetail row = (CashAccountETDetail) e.Row;
    CashAccountETDetail oldRow = (CashAccountETDetail) e.OldRow;
    bool? isDefault;
    int num1;
    if (row == null)
    {
      num1 = 1;
    }
    else
    {
      isDefault = row.IsDefault;
      num1 = !isDefault.GetValueOrDefault() ? 1 : 0;
    }
    if (num1 != 0)
    {
      int num2;
      if (oldRow == null)
      {
        num2 = 1;
      }
      else
      {
        isDefault = oldRow.IsDefault;
        bool flag = false;
        num2 = !(isDefault.GetValueOrDefault() == flag & isDefault.HasValue) ? 1 : 0;
      }
      if (num2 != 0)
        return;
    }
    CashAccountETDetail cashAccountEtDetail = PXResultset<CashAccountETDetail>.op_Implicit(PXSelectBase<CashAccountETDetail, PXSelect<CashAccountETDetail, Where<CashAccountETDetail.isDefault, Equal<True>, And<CashAccountETDetail.cashAccountID, Equal<Required<CashAccountETDetail.cashAccountID>>, And<CashAccountETDetail.entryTypeID, NotEqual<Required<CashAccountETDetail.entryTypeID>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.CashAccountID,
      (object) row.EntryTypeID
    }));
    if (cashAccountEtDetail == null)
      return;
    isDefault = cashAccountEtDetail.IsDefault;
    if (!isDefault.GetValueOrDefault())
      return;
    cashAccountEtDetail.IsDefault = new bool?(false);
    ((PXSelectBase<CashAccountETDetail>) this.ETDetails).Update(cashAccountEtDetail);
    ((PXSelectBase) this.ETDetails).View.RequestRefresh();
  }

  protected virtual void CashAccountETDetail_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    this.IsCorrectCashAccount(sender, e.Row);
  }

  private bool IsCorrectCashAccount(PXCache sender, object Row)
  {
    CashAccountETDetail cashAccountEtDetail = (CashAccountETDetail) Row;
    if (cashAccountEtDetail != null)
    {
      CAEntryType caEntryType = (CAEntryType) PXSelectorAttribute.Select<CashAccountETDetail.entryTypeID>(sender, Row);
      if (caEntryType != null && !cashAccountEtDetail.OffsetCashAccountID.HasValue && caEntryType.UseToReclassifyPayments.GetValueOrDefault() && !cashAccountEtDetail.OffsetAccountID.HasValue)
      {
        PX.Objects.CA.CashAccount cashAccount = PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelectReadonly<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.accountID, Equal<Required<PX.Objects.CA.CashAccount.accountID>>, And<PX.Objects.CA.CashAccount.subID, Equal<Required<PX.Objects.CA.CashAccount.subID>>, And<PX.Objects.CA.CashAccount.branchID, Equal<Required<PX.Objects.CA.CashAccount.branchID>>>>>>.Config>.Select((PXGraph) this, new object[3]
        {
          (object) caEntryType.AccountID,
          (object) caEntryType.SubID,
          (object) caEntryType.BranchID
        }));
        PX.Objects.CA.CashAccount current = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.CashAccount).Current;
        if (cashAccount != null)
        {
          int? cashAccountId1 = current.CashAccountID;
          int? cashAccountId2 = cashAccount.CashAccountID;
          if (cashAccountId1.GetValueOrDefault() == cashAccountId2.GetValueOrDefault() & cashAccountId1.HasValue == cashAccountId2.HasValue)
          {
            sender.RaiseExceptionHandling<CashAccountETDetail.offsetCashAccountID>((object) cashAccountEtDetail, (object) null, (Exception) new PXSetPropertyException("Default offset account can not be the same as current Cash Account. You must override Reclassification account.", (PXErrorLevel) 4));
            return false;
          }
          if (cashAccount.CuryID != current.CuryID)
          {
            sender.RaiseExceptionHandling<CashAccountETDetail.offsetCashAccountID>((object) cashAccountEtDetail, (object) null, (Exception) new PXSetPropertyException("Default offset account currency is different from the currency of the current Cash Account. You must override Reclassification account.", (PXErrorLevel) 4));
            return false;
          }
        }
      }
    }
    return true;
  }

  protected virtual void CashAccountETDetail_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CashAccountETDetail row1 = (CashAccountETDetail) e.Row;
    if (row1 == null)
      return;
    CAEntryType caEntryType = (CAEntryType) PXSelectorAttribute.Select<CashAccountETDetail.entryTypeID>(sender, e.Row);
    bool? reclassifyPayments;
    if (caEntryType != null)
    {
      int? nullable = row1.OffsetAccountID;
      if (nullable.HasValue)
      {
        nullable = row1.OffsetSubID;
        if (nullable.HasValue)
        {
          PX.Objects.CA.CashAccount cashAccount = PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelectReadonly<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.accountID, Equal<Required<PX.Objects.CA.CashAccount.accountID>>, And<PX.Objects.CA.CashAccount.subID, Equal<Required<PX.Objects.CA.CashAccount.subID>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) row1.OffsetAccountID,
            (object) row1.OffsetSubID
          }));
          PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelectReadonly<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) row1.OffsetAccountID
          }));
          bool flag = cashAccount != null;
          reclassifyPayments = caEntryType.UseToReclassifyPayments;
          if (reclassifyPayments.GetValueOrDefault())
          {
            if (!flag)
            {
              sender.RaiseExceptionHandling<CashAccountETDetail.offsetAccountID>(e.Row, (object) account.AccountCD, (Exception) new PXSetPropertyException("Entry Type which is used for Payment Reclassification must have a Cash Account as Offset Account", (PXErrorLevel) 4));
              goto label_16;
            }
            PX.Objects.CA.CashAccount current = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.CashAccount).Current;
            nullable = current.CashAccountID;
            int? cashAccountId = cashAccount.CashAccountID;
            if (nullable.GetValueOrDefault() == cashAccountId.GetValueOrDefault() & nullable.HasValue == cashAccountId.HasValue)
            {
              sender.RaiseExceptionHandling<CashAccountETDetail.offsetAccountID>(e.Row, (object) account.AccountCD, (Exception) new PXSetPropertyException("Offset account may not be the same as current Cash Account", (PXErrorLevel) 4));
              goto label_16;
            }
            if (cashAccount.CuryID != current.CuryID)
            {
              sender.RaiseExceptionHandling<CashAccountETDetail.offsetAccountID>(e.Row, (object) account.AccountCD, (Exception) new PXSetPropertyException("Offset account must be a Cash Account in the same currency as current Cash Account", (PXErrorLevel) 4));
              goto label_16;
            }
            PaymentMethodAccount paymentMethodAccount = PXResultset<PaymentMethodAccount>.op_Implicit(PXSelectBase<PaymentMethodAccount, PXSelectReadonly2<PaymentMethodAccount, InnerJoin<PaymentMethod, On<PaymentMethod.paymentMethodID, Equal<PaymentMethodAccount.paymentMethodID>, And<PaymentMethod.isActive, Equal<True>>>>, Where<PaymentMethodAccount.cashAccountID, Equal<Required<PaymentMethodAccount.cashAccountID>>, And<Where<PaymentMethodAccount.useForAP, Equal<True>, Or<PaymentMethodAccount.useForAR, Equal<True>>>>>>.Config>.Select((PXGraph) this, new object[1]
            {
              (object) cashAccount.CashAccountID
            }));
            if (paymentMethodAccount == null || !paymentMethodAccount.CashAccountID.HasValue)
            {
              sender.RaiseExceptionHandling<CashAccountETDetail.offsetAccountID>(e.Row, (object) account.AccountCD, (Exception) new PXSetPropertyException("This Cash Account is not configured for usage with any Payment Method. Please, check the configuration of the Payment Methods before using the Payments Reclassifications", (PXErrorLevel) 2));
              goto label_16;
            }
            sender.RaiseExceptionHandling<CashAccountETDetail.offsetAccountID>(e.Row, (object) null, (Exception) null);
            goto label_16;
          }
          row1.OffsetCashAccountID = new int?();
          PXUIFieldAttribute.SetEnabled<CashAccountETDetail.offsetBranchID>(sender, e.Row, flag);
          goto label_16;
        }
      }
      sender.RaiseExceptionHandling<CashAccountETDetail.offsetAccountID>(e.Row, (object) null, (Exception) null);
    }
label_16:
    if ((sender.GetStateExt<CashAccountETDetail.offsetAccountID>(e.Row) is PXFieldState stateExt ? (stateExt.ErrorLevel < 4 ? 1 : 0) : 0) != 0)
      AccountAttribute.VerifyAccountIsNotControl<CashAccountETDetail.offsetAccountID>(sender, (EventArgs) e);
    PXUIFieldAttribute.SetEnabled<CashAccountETDetail.taxZoneID>(sender, e.Row, caEntryType != null && caEntryType.Module == "CA");
    PXCache pxCache1 = sender;
    object row2 = e.Row;
    int num1;
    if (caEntryType != null)
    {
      reclassifyPayments = caEntryType.UseToReclassifyPayments;
      num1 = reclassifyPayments.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    PXUIFieldAttribute.SetEnabled<CashAccountETDetail.offsetCashAccountID>(pxCache1, row2, num1 != 0);
    PXCache pxCache2 = sender;
    object row3 = e.Row;
    int num2;
    if (caEntryType != null)
    {
      reclassifyPayments = caEntryType.UseToReclassifyPayments;
      num2 = !reclassifyPayments.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    PXUIFieldAttribute.SetEnabled<CashAccountETDetail.offsetAccountID>(pxCache2, row3, num2 != 0);
    PXCache pxCache3 = sender;
    object row4 = e.Row;
    int num3;
    if (caEntryType != null)
    {
      reclassifyPayments = caEntryType.UseToReclassifyPayments;
      num3 = !reclassifyPayments.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 0;
    PXUIFieldAttribute.SetEnabled<CashAccountETDetail.offsetSubID>(pxCache3, row4, num3 != 0);
  }

  protected virtual void CashAccountETDetail_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    CashAccountETDetail row = (CashAccountETDetail) e.Row;
    CAEntryType caEntryType = (CAEntryType) PXSelectorAttribute.Select<CashAccountETDetail.entryTypeID>(sender, e.Row);
    if (caEntryType == null || !caEntryType.UseToReclassifyPayments.GetValueOrDefault())
      return;
    int? nullable = row.OffsetAccountID;
    if (!nullable.HasValue)
      return;
    nullable = row.OffsetSubID;
    if (!nullable.HasValue)
      return;
    PX.Objects.CA.CashAccount cashAccount = PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelectReadonly<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.accountID, Equal<Required<PX.Objects.CA.CashAccount.accountID>>, And<PX.Objects.CA.CashAccount.subID, Equal<Required<PX.Objects.CA.CashAccount.subID>>, And<PX.Objects.CA.CashAccount.branchID, Equal<Required<PX.Objects.CA.CashAccount.branchID>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) row.OffsetAccountID,
      (object) row.OffsetSubID,
      (object) row.OffsetBranchID
    }));
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelectReadonly<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.OffsetAccountID
    }));
    if (cashAccount == null)
    {
      if (sender.RaiseExceptionHandling<CashAccountETDetail.offsetAccountID>(e.Row, (object) account.AccountCD, (Exception) new PXSetPropertyException("Entry Type which is used for Payment Reclassification must have a Cash Account as Offset Account", (PXErrorLevel) 4)))
        throw new PXRowPersistingException(typeof (CashAccountETDetail.offsetAccountID).Name, (object) account.AccountCD, "Entry Type which is used for Payment Reclassification must have a Cash Account as Offset Account");
    }
    else
    {
      PX.Objects.CA.CashAccount current = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.CashAccount).Current;
      nullable = current.CashAccountID;
      int? cashAccountId = cashAccount.CashAccountID;
      if (nullable.GetValueOrDefault() == cashAccountId.GetValueOrDefault() & nullable.HasValue == cashAccountId.HasValue && sender.RaiseExceptionHandling<CashAccountETDetail.offsetAccountID>(e.Row, (object) account.AccountCD, (Exception) new PXSetPropertyException("Offset account may not be the same as current Cash Account", (PXErrorLevel) 4)))
        throw new PXRowPersistingException(typeof (CashAccountETDetail.offsetAccountID).Name, (object) account.AccountCD, "Offset account may not be the same as current Cash Account");
      if (cashAccount.CuryID != current.CuryID && sender.RaiseExceptionHandling<CashAccountETDetail.offsetAccountID>(e.Row, (object) account.AccountCD, (Exception) new PXSetPropertyException("Offset account must be a Cash Account in the same currency as current Cash Account", (PXErrorLevel) 4)))
        throw new PXRowPersistingException(typeof (CashAccountETDetail.offsetAccountID).Name, (object) account.AccountCD, "Offset account must be a Cash Account in the same currency as current Cash Account");
    }
  }

  protected virtual void CashAccountETDetail_OffsetCashAccountID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    CashAccountETDetail row = (CashAccountETDetail) e.Row;
    cache.SetDefaultExt<CashAccountETDetail.offsetAccountID>(e.Row);
    cache.SetDefaultExt<CashAccountETDetail.offsetSubID>(e.Row);
    cache.SetDefaultExt<CashAccountETDetail.offsetBranchID>(e.Row);
  }

  protected virtual void CashAccountETDetail_OffsetAccountID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CashAccountETDetail row))
      return;
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.OffsetAccountID
    }));
    if (account != null && account.IsCashAccount.GetValueOrDefault())
    {
      PX.Objects.CA.CashAccount cashAccount = PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.accountID, Equal<Required<PX.Objects.CA.CashAccount.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) account.AccountID
      }));
      cache.SetValueExt<CashAccountETDetail.offsetBranchID>((object) row, (object) cashAccount.BranchID);
      cache.SetValueExt<CashAccountETDetail.offsetSubID>((object) row, (object) cashAccount.SubID);
    }
    else
      cache.SetValue<CashAccountETDetail.offsetBranchID>((object) row, (object) null);
  }

  protected virtual void CashAccountETDetail_OffsetAccountID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    CashAccountETDetail row = (CashAccountETDetail) e.Row;
    if (row == null || !row.OffsetCashAccountID.HasValue)
      return;
    PX.Objects.CA.CashAccount cashAccount = PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Required<PX.Objects.CA.CashAccount.cashAccountID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.OffsetCashAccountID
    }));
    if (cashAccount == null)
      return;
    e.NewValue = (object) cashAccount.AccountID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CashAccountETDetail_OffsetSubID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    CashAccountETDetail row = (CashAccountETDetail) e.Row;
    if (row == null || !row.OffsetCashAccountID.HasValue)
      return;
    PX.Objects.CA.CashAccount cashAccount = PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Required<PX.Objects.CA.CashAccount.cashAccountID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.OffsetCashAccountID
    }));
    if (cashAccount == null)
      return;
    e.NewValue = (object) cashAccount.SubID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CashAccountETDetail_OffsetBranchID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    CashAccountETDetail row = (CashAccountETDetail) e.Row;
    if (row == null)
      return;
    if (row.OffsetCashAccountID.HasValue)
    {
      PX.Objects.CA.CashAccount cashAccount = PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Required<PX.Objects.CA.CashAccount.cashAccountID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) row.OffsetCashAccountID
      }));
      if (cashAccount == null)
        return;
      e.NewValue = (object) cashAccount.BranchID;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      e.NewValue = (object) null;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void CashAccountDeposit_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    CashAccountDeposit row = (CashAccountDeposit) e.Row;
  }

  protected virtual void CashAccountDeposit_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    CashAccountDeposit row = (CashAccountDeposit) e.Row;
  }

  protected virtual void CashAccountDeposit_RowUpdating(PXCache cache, PXRowUpdatingEventArgs e)
  {
    CashAccountDeposit row = (CashAccountDeposit) e.Row;
  }

  protected virtual void CashAccountDeposit_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    CashAccountDeposit row = (CashAccountDeposit) e.Row;
  }

  protected virtual void CashAccountDeposit_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    CashAccountDeposit row = (CashAccountDeposit) e.Row;
    if (row == null)
      return;
    PXCache pxCache = cache;
    Decimal? chargeRate = row.ChargeRate;
    Decimal num1 = 0M;
    int num2 = !(chargeRate.GetValueOrDefault() == num1 & chargeRate.HasValue) ? 1 : 0;
    PXUIFieldAttribute.SetRequired<CashAccountDeposit.chargeEntryTypeID>(pxCache, num2 != 0);
  }

  protected virtual void CashAccountDeposit_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    CashAccountDeposit row = (CashAccountDeposit) e.Row;
    PXCache pxCache = cache;
    CashAccountDeposit cashAccountDeposit = row;
    Decimal? chargeRate = row.ChargeRate;
    Decimal num1 = 0M;
    int num2 = !(chargeRate.GetValueOrDefault() == num1 & chargeRate.HasValue) ? 1 : 2;
    PXDefaultAttribute.SetPersistingCheck<CashAccountDeposit.chargeEntryTypeID>(pxCache, (object) cashAccountDeposit, (PXPersistingCheck) num2);
  }

  protected virtual void CashAccountPaymentMethodDetail_RowPersisting(
    PXCache cache,
    PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) != 2 && (e.Operation & 3) != 1)
      return;
    CashAccountPaymentMethodDetail row = (CashAccountPaymentMethodDetail) e.Row;
    if (row == null)
      return;
    PaymentMethodAccount paymentMethodAccount = this.GetCurrentPaymentMethodAccount(row.PaymentMethodID);
    if ((paymentMethodAccount != null ? (!paymentMethodAccount.UseForAP.GetValueOrDefault() ? 1 : 0) : 1) != 0 && (paymentMethodAccount != null ? (!paymentMethodAccount.UseForAR.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      return;
    PaymentMethodDetail template = this.FindTemplate(row);
    bool flag = template != null && template.IsRequired.GetValueOrDefault();
    PXDefaultAttribute.SetPersistingCheck<CashAccountPaymentMethodDetail.detailValue>(cache, (object) row, flag ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  protected virtual void CashAccountPaymentMethodDetail_DetailValue_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    CashAccountPaymentMethodDetail row = (CashAccountPaymentMethodDetail) e.Row;
    string newValue = e.NewValue as string;
    PaymentMethodDetail template = this.FindTemplate(row);
    if ((template == null ? 0 : (template.IsRequired.GetValueOrDefault() ? 1 : 0)) == 0 || !string.IsNullOrEmpty(newValue))
      return;
    PXSetPropertyException propertyException = new PXSetPropertyException("This field is required.");
    cache.RaiseExceptionHandling<CashAccountPaymentMethodDetail.detailValue>((object) row, (object) newValue, (Exception) propertyException);
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<CashAccountPaymentMethodDetail, CashAccountPaymentMethodDetail.detailValue> e)
  {
    PaymentMethodDetailHelper.CashAccountDetailValueFieldSelecting((PXGraph) this, e);
  }

  public virtual void Clear()
  {
    ((PXGraph) this).Clear();
    this.isPaymentMergedFlag = false;
  }

  public virtual void Persist()
  {
    if (((PXSelectBase<PX.Objects.CA.CashAccount>) this.CashAccount).Current != null && ((PXSelectBase) this.CashAccount).Cache.GetStatus((object) ((PXSelectBase<PX.Objects.CA.CashAccount>) this.CashAccount).Current) != 3 && ((PXSelectBase) this.CashAccount).Cache.GetStatus((object) ((PXSelectBase<PX.Objects.CA.CashAccount>) this.CashAccount).Current) != 4 && !this.ValidateClearingAccount(((PXSelectBase<PX.Objects.CA.CashAccount>) this.CashAccount).Current.BranchID))
      return;
    BranchBaseAttribute.VerifyFieldInPXCache<CashAccountDeposit, CashAccountDeposit.depositAcctID>((PXGraph) this, ((PXSelectBase<CashAccountDeposit>) this.Deposits).Select(Array.Empty<object>()));
    this.ValidatePaymentMethodAccount();
    foreach (CashAccountPaymentMethodDetail aRow in ((PXSelectBase) this.PaymentDetails).Cache.Cached)
    {
      CashAccountPaymentMethodDetail paymentMethodDetail = aRow;
      if (!this.ValidateDetail(aRow))
        this.remittancePMErrors[paymentMethodDetail.PaymentMethodID] = "Some remittance settings for this payment method have invalid values. Update the settings on the Remittance Settings tab.";
    }
    if (this.remittancePMErrors.Count > 0)
      throw new PXException("Some remittance settings for this payment method have invalid values. Update the settings on the Remittance Settings tab.");
    bool flag = false;
    foreach (CashAccountETDetail Row in ((PXSelectBase) this.ETDetails).Cache.Inserted)
    {
      if (!this.IsCorrectCashAccount(((PXSelectBase) this.ETDetails).Cache, (object) Row))
        flag = true;
    }
    foreach (CashAccountETDetail Row in ((PXSelectBase) this.ETDetails).Cache.Updated)
    {
      if (!this.IsCorrectCashAccount(((PXSelectBase) this.ETDetails).Cache, (object) Row))
        flag = true;
    }
    if (flag)
      throw new PXException("{0} '{1}' record raised at least one error. Please review the errors.", new object[2]
      {
        (object) ErrorMessages.GetLocal("Inserting "),
        (object) typeof (CashAccountETDetail).Name
      });
    ((PXGraph) this).Persist();
  }

  protected virtual void ValidatePaymentMethodAccount()
  {
  }

  public virtual int Persist(System.Type cacheType, PXDBOperation operation)
  {
    try
    {
      return ((PXGraph) this).Persist(cacheType, operation);
    }
    catch (PXDatabaseException ex)
    {
      if (cacheType == typeof (PaymentMethodAccount) && (operation == 3 || operation == 3) && (ex.ErrorCode == null || ex.ErrorCode == 1))
      {
        string str = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.CurrentCashAccount).Current != null ? ((PXSelectBase<PX.Objects.CA.CashAccount>) this.CurrentCashAccount).Current.CashAccountCD.Trim() : ex.Keys[1].ToString();
        throw new PXException("The combination of Payment Method, Cash Account '{0}, {1}' cannot be deleted because it is already used in payments.", new object[2]
        {
          ex.Keys[0],
          (object) str
        });
      }
      throw;
    }
  }

  protected virtual void FillPaymentDetails(string aPaymentMethodID)
  {
    PX.Objects.CA.CashAccount current = ((PXSelectBase<PX.Objects.CA.CashAccount>) this.CurrentCashAccount).Current;
    if (current == null || this.isPaymentMergedFlag)
      return;
    int? cashAccountId = current.CashAccountID;
    if (!string.IsNullOrEmpty(aPaymentMethodID))
    {
      List<PaymentMethodDetail> paymentMethodDetailList = new List<PaymentMethodDetail>();
      foreach (PXResult<PaymentMethodDetail> pxResult1 in PXSelectBase<PaymentMethodDetail, PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Required<PaymentMethodDetail.paymentMethodID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForCashAccount>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) aPaymentMethodID
      }))
      {
        PaymentMethodDetail paymentMethodDetail1 = PXResult<PaymentMethodDetail>.op_Implicit(pxResult1);
        PaymentMethod paymentMethod = PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) paymentMethodDetail1.PaymentMethodID
        }));
        if (paymentMethod != null && paymentMethod.UseForCA.GetValueOrDefault())
        {
          CashAccountPaymentMethodDetail paymentMethodDetail2 = (CashAccountPaymentMethodDetail) null;
          foreach (PXResult<CashAccountPaymentMethodDetail> pxResult2 in ((PXSelectBase<CashAccountPaymentMethodDetail>) this.PaymentDetails).Select(new object[2]
          {
            (object) cashAccountId,
            (object) aPaymentMethodID
          }))
          {
            CashAccountPaymentMethodDetail paymentMethodDetail3 = PXResult<CashAccountPaymentMethodDetail>.op_Implicit(pxResult2);
            if (paymentMethodDetail3.DetailID == paymentMethodDetail1.DetailID)
            {
              paymentMethodDetail2 = paymentMethodDetail3;
              break;
            }
          }
          if (paymentMethodDetail2 == null)
            paymentMethodDetailList.Add(paymentMethodDetail1);
        }
      }
      using (new ReadOnlyScope(new PXCache[1]
      {
        ((PXSelectBase) this.PaymentDetails).Cache
      }))
      {
        foreach (PaymentMethodDetail paymentMethodDetail in paymentMethodDetailList)
          ((PXSelectBase<CashAccountPaymentMethodDetail>) this.PaymentDetails).Insert(new CashAccountPaymentMethodDetail()
          {
            CashAccountID = current.CashAccountID,
            PaymentMethodID = aPaymentMethodID,
            DetailID = paymentMethodDetail.DetailID
          });
        if (paymentMethodDetailList.Count > 0)
          ((PXSelectBase) this.PaymentDetails).View.RequestRefresh();
      }
    }
    this.isPaymentMergedFlag = true;
  }

  public virtual void CheckCashAccountInUse(PXCache sender, PX.Objects.CA.CashAccount aAcct)
  {
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, (object) aAcct, typeof (PX.Objects.AR.CustomerPaymentMethod.cashAccountID), (System.Type) null, (string) null);
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, (object) aAcct, typeof (CashAccountDeposit.depositAcctID), (System.Type) null, "The cash account cannot be deleted. This cash account is assigned to another cash account as clearing. Remove links to other cash account and then proceed with the cash account deletion.");
  }

  private void DeleteCashAccountInUse(PX.Objects.CA.CashAccount cashacct)
  {
    foreach (PXResult<CADailySummary> pxResult in ((PXSelectBase<CADailySummary>) this.CADailySummaryDetails).Select(new object[1]
    {
      (object) cashacct.CashAccountID
    }))
      ((PXSelectBase<CADailySummary>) this.CADailySummaryDetails).Delete(PXResult<CADailySummary>.op_Implicit(pxResult));
    foreach (PXResult<PX.Objects.CR.Location> pxResult in ((PXSelectBase<PX.Objects.CR.Location>) this.Locations).Select(new object[1]
    {
      (object) cashacct.CashAccountID
    }))
    {
      PX.Objects.CR.Location location = PXResult<PX.Objects.CR.Location>.op_Implicit(pxResult);
      location.VCashAccountID = new int?();
      ((PXSelectBase<PX.Objects.CR.Location>) this.Locations).Update(location);
    }
  }

  public virtual bool HasGLTrans(int? aAccountID, int? subID, int? branchID)
  {
    if (!aAccountID.HasValue || !subID.HasValue || !branchID.HasValue)
      return false;
    PX.Objects.GL.GLTran glTran = PXResultset<PX.Objects.GL.GLTran>.op_Implicit(PXSelectBase<PX.Objects.GL.GLTran, PXSelect<PX.Objects.GL.GLTran, Where<PX.Objects.GL.GLTran.accountID, Equal<Required<PX.Objects.GL.GLTran.accountID>>, And<PX.Objects.GL.GLTran.subID, Equal<Required<PX.Objects.GL.GLTran.subID>>, And<PX.Objects.GL.GLTran.branchID, Equal<Required<PX.Objects.GL.GLTran.branchID>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
    {
      (object) aAccountID,
      (object) subID,
      (object) branchID
    }));
    return glTran != null && glTran.BatchNbr != null;
  }

  public virtual bool CheckCashAccountForTransactions(PX.Objects.CA.CashAccount aAcct)
  {
    return PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.cashAccountID, Equal<Required<CATran.cashAccountID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) aAcct.CashAccountID
    })) != null;
  }

  public virtual bool CheckIfCashAccountUsedForBatchPayments(PX.Objects.CA.CashAccount aAcct)
  {
    return !string.IsNullOrEmpty(PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelectJoin<PaymentMethod, InnerJoin<PaymentMethodAccount, On<PaymentMethod.paymentMethodID, Equal<PaymentMethodAccount.paymentMethodID>>>, Where<PaymentMethod.aPCreateBatchPayment, Equal<True>, And<PaymentMethodAccount.cashAccountID, Equal<Required<PaymentMethodAccount.cashAccountID>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) aAcct.CashAccountID
    }))?.PaymentMethodID);
  }

  private void RecalcOptions(PX.Objects.CA.CashAccount aRow)
  {
    CashAccountMaint.CashAccountOptions cashAccountOptions = this.DetectOptions();
    bool isDirty = ((PXGraph) this).Caches[typeof (PX.Objects.CA.CashAccount)].IsDirty;
    aRow.PTInstancesAllowed = new bool?((cashAccountOptions & CashAccountMaint.CashAccountOptions.HasPTInstances) != 0);
    aRow.AcctSettingsAllowed = new bool?((cashAccountOptions & CashAccountMaint.CashAccountOptions.HasPTSettings) != 0);
    ((PXGraph) this).Caches[typeof (PX.Objects.CA.CashAccount)].IsDirty = isDirty;
  }

  private CashAccountMaint.CashAccountOptions DetectOptions()
  {
    bool flag = false;
    foreach (PXResult<PaymentMethodAccount, PaymentMethod> pxResult in ((PXSelectBase<PaymentMethodAccount>) this.Details).Select(Array.Empty<object>()))
    {
      PaymentMethod paymentMethod1 = PXResult<PaymentMethodAccount, PaymentMethod>.op_Implicit(pxResult);
      if (!flag)
      {
        PaymentMethodDetail paymentMethodDetail = PXResultset<PaymentMethodDetail>.op_Implicit(PXSelectBase<PaymentMethodDetail, PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Required<PaymentMethodDetail.paymentMethodID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForCashAccount>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) paymentMethod1.PaymentMethodID
        }));
        if (paymentMethodDetail != null)
        {
          PaymentMethod paymentMethod2 = PXResultset<PaymentMethod>.op_Implicit(PXSelectBase<PaymentMethod, PXSelect<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Required<PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) paymentMethodDetail.PaymentMethodID
          }));
          if (paymentMethod2 != null && paymentMethod2.UseForCA.GetValueOrDefault())
            flag = true;
        }
      }
    }
    CashAccountMaint.CashAccountOptions cashAccountOptions = CashAccountMaint.CashAccountOptions.None;
    if (flag)
      cashAccountOptions |= CashAccountMaint.CashAccountOptions.HasPTSettings;
    return cashAccountOptions;
  }

  protected virtual PaymentMethodDetail FindTemplate(CashAccountPaymentMethodDetail aDet)
  {
    return PXResultset<PaymentMethodDetail>.op_Implicit(PXSelectBase<PaymentMethodDetail, PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Required<PaymentMethodDetail.paymentMethodID>>, And<PaymentMethodDetail.detailID, Equal<Required<PaymentMethodDetail.detailID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForCashAccount>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) aDet.PaymentMethodID,
      (object) aDet.DetailID
    }));
  }

  protected virtual bool ValidateDetail(CashAccountPaymentMethodDetail aRow)
  {
    PaymentMethodAccount paymentMethodAccount = this.GetCurrentPaymentMethodAccount(aRow.PaymentMethodID);
    if ((paymentMethodAccount != null ? (!paymentMethodAccount.UseForAP.GetValueOrDefault() ? 1 : 0) : 1) != 0 && (paymentMethodAccount != null ? (!paymentMethodAccount.UseForAR.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      return true;
    PaymentMethodDetail template = this.FindTemplate(aRow);
    if ((template != null ? (template.IsRequired.GetValueOrDefault() ? 1 : 0) : 0) == 0 || !string.IsNullOrEmpty(aRow.DetailValue))
      return true;
    PXSetPropertyException propertyException = new PXSetPropertyException("This field is required.");
    ((PXSelectBase) this.PaymentDetails).Cache.RaiseExceptionHandling<CashAccountPaymentMethodDetail.detailValue>((object) aRow, (object) aRow.DetailValue, (Exception) propertyException);
    return false;
  }

  public virtual PXResultset<PaymentMethodAccount> GetCurrentUsedPaymentMethodAccounts()
  {
    return PXSelectBase<PaymentMethodAccount, PXSelect<PaymentMethodAccount, Where2<Where<PaymentMethodAccount.useForAP, Equal<True>, Or<PaymentMethodAccount.useForAR, Equal<True>>>, And<PaymentMethodAccount.cashAccountID, Equal<Current2<PX.Objects.CA.CashAccount.cashAccountID>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
  }

  private PaymentMethodAccount GetCurrentPaymentMethodAccount(string paymentMethodID)
  {
    return PXResultset<PaymentMethodAccount>.op_Implicit(PXSelectBase<PaymentMethodAccount, PXSelect<PaymentMethodAccount, Where<PaymentMethodAccount.paymentMethodID, Equal<Required<PaymentMethodAccount.paymentMethodID>>, And<PaymentMethodAccount.cashAccountID, Equal<Current2<PX.Objects.CA.CashAccount.cashAccountID>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) paymentMethodID
    }));
  }

  protected virtual bool ValidateClearingAccount(int? branchId)
  {
    bool flag = true;
    PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(branchId);
    using (new PXReadBranchRestrictedScope())
    {
      foreach (PXResult<CashAccountDeposit, PX.Objects.CA.CashAccount> pxResult in ((PXSelectBase<CashAccountDeposit>) this.Deposits).Select(Array.Empty<object>()))
      {
        CashAccountDeposit cashAccountDeposit = PXResult<CashAccountDeposit, PX.Objects.CA.CashAccount>.op_Implicit(pxResult);
        PX.Objects.CA.CashAccount cashAccount = PXResult<CashAccountDeposit, PX.Objects.CA.CashAccount>.op_Implicit(pxResult);
        if (cashAccount.BaseCuryID != branch.BaseCuryID)
        {
          ((PXSelectBase) this.Deposits).Cache.RaiseExceptionHandling<CashAccountDeposit.depositAcctID>((object) cashAccountDeposit, (object) cashAccount.CashAccountCD, (Exception) new PXSetPropertyException("The base currency of the {0} branch associated with the {1} clearing account differs from the base currency of the {2} branch.", (PXErrorLevel) 4, new object[3]
          {
            (object) PXAccess.GetBranchCD(cashAccount.BranchID),
            (object) cashAccount.CashAccountCD,
            (object) branch.BranchCD
          }));
          flag = false;
        }
      }
    }
    return flag;
  }

  [PXProjection(typeof (Select<PaymentMethodAccount>), Persistent = false)]
  [Serializable]
  public class PaymentMethodAccount2 : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(10, IsUnicode = true, IsKey = true, BqlField = typeof (PaymentMethodAccount.paymentMethodID))]
    [PXDefault(typeof (PaymentMethod.paymentMethodID))]
    [PXSelector(typeof (Search<PaymentMethod.paymentMethodID>))]
    [PXUIField(DisplayName = "Payment Method")]
    public virtual string PaymentMethodID { get; set; }

    [PXDBInt(BqlField = typeof (PaymentMethodAccount.cashAccountID), IsKey = true)]
    [PXDefault]
    public virtual int? CashAccountID { get; set; }

    public abstract class paymentMethodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CashAccountMaint.PaymentMethodAccount2.paymentMethodID>
    {
    }

    public abstract class cashAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CashAccountMaint.PaymentMethodAccount2.cashAccountID>
    {
    }
  }

  [Flags]
  private enum CashAccountOptions
  {
    None = 0,
    HasPTSettings = 1,
    HasPTInstances = 2,
  }

  public class CashAccountChangeID(PXGraph graph, string name) : 
    PXChangeID<PX.Objects.CA.CashAccount, PX.Objects.CA.CashAccount.cashAccountCD>(graph, name)
  {
    [PXButton(Category = "Other")]
    [PXUIField]
    protected virtual IEnumerable Handler(PXAdapter adapter) => base.Handler(adapter);
  }
}
