// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CashTransferEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Extensions;
using PX.Objects.Extensions.PerUnitTax;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class CashTransferEntry : PXGraph<CashTransferEntry, CATransfer>
{
  [PXCopyPasteHiddenFields(new Type[] {typeof (CATransfer.clearDateIn), typeof (CATransfer.clearDateOut), typeof (CATransfer.clearedIn), typeof (CATransfer.clearedOut)})]
  public PXSelect<CATransfer> Transfer;
  [PXCopyPasteHiddenFields(new Type[] {typeof (CAExpense.refNbr), typeof (CAExpense.extRefNbr), typeof (CAExpense.adjRefNbr), typeof (CAExpense.curyInfoID)})]
  public CAChargeSelect<CATransfer, CAExpense.tranDate, CAExpense.finPeriodID, CAExpense, CAExpense.entryTypeID, CAExpense.refNbr, Where<CAExpense.refNbr, Equal<Current<CATransfer.transferNbr>>>> Expenses;
  [PXCopyPasteHiddenView]
  public PXSelect<CAExpenseTax, Where<CAExpenseTax.tranType, Equal<CATranType.cATransferExp>, And<CAExpenseTax.refNbr, Equal<Current<CAExpense.refNbr>>, And<CAExpenseTax.lineNbr, Equal<Current<CAExpense.lineNbr>>>>>, OrderBy<Asc<CAExpenseTax.tranType, Asc<CAExpenseTax.refNbr, Asc<CATax.taxID>>>>> ExpenseTaxes;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<CAExpenseTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<CAExpenseTaxTran.taxID>>>, Where<CAExpenseTaxTran.refNbr, Equal<Current<CAExpense.refNbr>>, And<CAExpenseTaxTran.lineNbr, Equal<Current<CAExpense.lineNbr>>>>> ExpenseTaxTrans;
  [PXCopyPasteHiddenFields(new Type[] {typeof (CATransfer.clearDateIn), typeof (CATransfer.clearDateOut), typeof (CATransfer.clearedIn), typeof (CATransfer.clearedOut)})]
  public PXSelect<CATran> caTran;
  public PXSetup<PX.Objects.CA.CASetup> CASetup;
  public PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Required<PX.Objects.CA.CashAccount.cashAccountID>>>> CashAccount;
  public PXSelectReadonly<OrganizationFinPeriod, Where<OrganizationFinPeriod.finPeriodID, Equal<Current<CATransfer.inPeriodID>>, And<EqualToOrganizationOfBranch<OrganizationFinPeriod.organizationID, Current<CATransfer.inBranchID>>>>> inFinPeriod;
  public PXSelectReadonly<OrganizationFinPeriod, Where<OrganizationFinPeriod.finPeriodID, Equal<Current<CATransfer.outPeriodID>>, And<EqualToOrganizationOfBranch<OrganizationFinPeriod.organizationID, Current<CATransfer.outBranchID>>>>> outFinPeriod;
  public PXSelect<Sub, Where<Sub.subID, Equal<Required<PX.Objects.CA.CashAccount.subID>>>> subaccount;
  public PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.CA.CashAccount.accountID>>>> account;
  public PXInitializeState<CATransfer> initializeState;
  public PXAction<CATransfer> putOnHold;
  public PXAction<CATransfer> releaseFromHold;
  public PXWorkflowEventHandler<CATransfer> OnReleaseDocument;
  public PXWorkflowEventHandler<CATransfer> OnUpdateStatus;
  public PXAction<CATransfer> ViewDoc;
  public PXAction<CATransfer> Release;
  public PXAction<CATransfer> Reverse;
  public PXAction<CATransfer> viewExpenseBatch;
  public PXAction<CATransfer> caReversingTransfers;

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  public CashTransferEntry()
  {
    OpenPeriodAttribute.SetValidatePeriod<CATransfer.inPeriodID>(((PXSelectBase) this.Transfer).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    OpenPeriodAttribute.SetValidatePeriod<CATransfer.outPeriodID>(((PXSelectBase) this.Transfer).Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    PXUIFieldAttribute.SetRequired<CATransfer.inExtRefNbr>(((PXSelectBase) this.Transfer).Cache, ((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.RequireExtRefNbr.GetValueOrDefault());
    PXUIFieldAttribute.SetRequired<CATransfer.outExtRefNbr>(((PXSelectBase) this.Transfer).Cache, ((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.RequireExtRefNbr.GetValueOrDefault());
  }

  protected virtual void CATransfer_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CATransfer row1 = (CATransfer) e.Row;
    if (row1 == null)
      return;
    bool? nullable = row1.Released;
    bool allowEdit = !nullable.GetValueOrDefault();
    nullable = row1.Released;
    bool valueOrDefault = nullable.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<CATransfer.inGLBalance>(sender, (object) row1, allowEdit);
    PXUIFieldAttribute.SetVisible<CATransfer.outGLBalance>(sender, (object) row1, allowEdit);
    PXUIFieldAttribute.SetVisible<CATransfer.cashBalanceIn>(sender, (object) row1, allowEdit);
    PXUIFieldAttribute.SetVisible<CATransfer.cashBalanceOut>(sender, (object) row1, allowEdit);
    PXUIFieldAttribute.SetVisible<CATransfer.tranIDIn_CATran_batchNbr>(sender, (object) row1, valueOrDefault);
    PXUIFieldAttribute.SetVisible<CATransfer.tranIDOut_CATran_batchNbr>(sender, (object) row1, valueOrDefault);
    PXUIFieldAttribute.SetEnabled(sender, (object) row1, false);
    sender.AllowDelete = allowEdit;
    sender.AllowUpdate = allowEdit;
    ((PXSelectBase) this.Expenses).Cache.SetAllEditPermissions(allowEdit);
    PX.Objects.CA.CashAccount cashAccount1 = (PX.Objects.CA.CashAccount) PXSelectorAttribute.Select<CATransfer.outAccountID>(sender, e.Row);
    PX.Objects.CA.CashAccount cashAccount2 = (PX.Objects.CA.CashAccount) PXSelectorAttribute.Select<CATransfer.inAccountID>(sender, e.Row);
    int num1;
    if (allowEdit && cashAccount2 != null)
    {
      nullable = cashAccount2.Reconcile;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag1 = num1 != 0;
    int num2;
    if (allowEdit && cashAccount1 != null)
    {
      nullable = cashAccount1.Reconcile;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    bool flag2 = num2 != 0;
    PXUIFieldAttribute.SetEnabled<CATransfer.hold>(sender, (object) row1, allowEdit);
    PXUIFieldAttribute.SetEnabled<CATransfer.transferNbr>(sender, (object) row1, true);
    PXUIFieldAttribute.SetEnabled<CATransfer.descr>(sender, (object) row1, allowEdit);
    PXUIFieldAttribute.SetEnabled<CATransfer.curyTranIn>(sender, (object) row1, allowEdit);
    PXUIFieldAttribute.SetEnabled<CATransfer.inAccountID>(sender, (object) row1, allowEdit);
    PXUIFieldAttribute.SetEnabled<CATransfer.inDate>(sender, (object) row1, allowEdit);
    PXUIFieldAttribute.SetEnabled<CATransfer.inExtRefNbr>(sender, (object) row1, allowEdit);
    PXUIFieldAttribute.SetEnabled<CATransfer.curyTranOut>(sender, (object) row1, allowEdit);
    PXUIFieldAttribute.SetEnabled<CATransfer.outAccountID>(sender, (object) row1, allowEdit);
    PXUIFieldAttribute.SetEnabled<CATransfer.outDate>(sender, (object) row1, allowEdit);
    PXUIFieldAttribute.SetEnabled<CATransfer.outExtRefNbr>(sender, (object) row1, allowEdit);
    PXUIFieldAttribute.SetEnabled<CATransfer.clearedOut>(sender, (object) row1, flag2);
    PXCache pxCache1 = sender;
    CATransfer caTransfer1 = row1;
    int num3;
    if (flag2)
    {
      nullable = row1.ClearedOut;
      num3 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 0;
    PXUIFieldAttribute.SetEnabled<CATransfer.clearDateOut>(pxCache1, (object) caTransfer1, num3 != 0);
    PXUIFieldAttribute.SetEnabled<CATransfer.clearedIn>(sender, (object) row1, flag1);
    PXCache pxCache2 = sender;
    CATransfer caTransfer2 = row1;
    int num4;
    if (flag1)
    {
      nullable = row1.ClearedIn;
      num4 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num4 = 0;
    PXUIFieldAttribute.SetEnabled<CATransfer.clearDateIn>(pxCache2, (object) caTransfer2, num4 != 0);
    ((PXSelectBase) this.ExpenseTaxTrans).AllowInsert = allowEdit;
    ((PXSelectBase) this.ExpenseTaxTrans).AllowUpdate = allowEdit;
    ((PXSelectBase) this.ExpenseTaxTrans).AllowDelete = allowEdit;
    PXCache cache = sender;
    CATransfer row2 = row1;
    nullable = row1.Released;
    int num5;
    if (!nullable.GetValueOrDefault())
    {
      DateTime? outDate = row1.OutDate;
      DateTime? inDate = row1.InDate;
      num5 = outDate.HasValue & inDate.HasValue ? (outDate.GetValueOrDefault() > inDate.GetValueOrDefault() ? 1 : 0) : 0;
    }
    else
      num5 = 0;
    object[] objArray = Array.Empty<object>();
    UIState.RaiseOrHideErrorByErrorLevelPriority<CATransfer.inDate>(cache, (object) row2, num5 != 0, "The date in Receipt Date is earlier than in Transfer Date.", (PXErrorLevel) 2, objArray);
    this.SetAdjRefNbrVisibility();
    PXCache pxCache3 = sender;
    CATransfer caTransfer3 = row1;
    int? reverseCount = row1.ReverseCount;
    int num6 = 0;
    int num7 = reverseCount.GetValueOrDefault() > num6 & reverseCount.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<CATransfer.reverseCount>(pxCache3, (object) caTransfer3, num7 != 0);
    PXUIFieldAttribute.SetVisible<CATransfer.origTransferNbr>(sender, (object) row1, row1.OrigTransferNbr != null);
  }

  protected virtual void CATransfer_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (!(e.Row is CATransfer row))
      return;
    if (row.Released.GetValueOrDefault() && !row.ReverseCount.HasValue && !((PXGraph) this).UnattendedMode)
    {
      using (new PXConnectionScope())
        row.ReverseCount = new int?(CashTransferEntry.GetReversingTransfer((PXGraph) this, row.TransferNbr).Count<CATransfer>());
    }
    using (new PXConnectionScope())
      this.RecalcTotalExpense(((PXSelectBase) this.Expenses).Cache, row);
  }

  private void RecalcTotalExpense(PXCache sender, CATransfer doc)
  {
    PXFormulaAttribute.CalcAggregate<CAExpense.tranAmt>(((PXSelectBase) this.Expenses).Cache, (object) doc, false);
    sender.RaiseFieldUpdated<CATransfer.totalExpenses>((object) doc, (object) null);
  }

  protected void CAExpense_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CAExpense row = (CAExpense) e.Row;
    if (row == null)
      return;
    PX.Objects.CA.CashAccount cashAccount = PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Required<CAExpense.cashAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.CashAccountID
    }));
    bool? nullable;
    int num;
    if (!row.Released.GetValueOrDefault())
    {
      if (cashAccount == null)
      {
        num = 0;
      }
      else
      {
        nullable = cashAccount.Reconcile;
        num = nullable.GetValueOrDefault() ? 1 : 0;
      }
    }
    else
      num = 0;
    bool flag = num != 0;
    PXUIFieldAttribute.SetEnabled<CAExpense.cleared>(sender, (object) row, flag);
    if (((PXSelectBase<CATransfer>) this.Transfer).Current == null || !(sender.GetStateExt<CAExpense.tranDate>((object) row) is PXFieldState stateExt) || stateExt.Value == null || stateExt.ErrorLevel == 4)
      return;
    nullable = ((PXSelectBase<CATransfer>) this.Transfer).Current.Released;
    if (!nullable.GetValueOrDefault())
    {
      DateTime? outDate = ((PXSelectBase<CATransfer>) this.Transfer).Current.OutDate;
      DateTime? tranDate = row.TranDate;
      if ((outDate.HasValue & tranDate.HasValue ? (outDate.GetValueOrDefault() > tranDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        sender.RaiseExceptionHandling<CAExpense.tranDate>((object) row, (object) row.TranDate, (Exception) new PXSetPropertyException("The date of the expense is earlier than the transfer date.", (PXErrorLevel) 2));
        return;
      }
    }
    sender.RaiseExceptionHandling<CAExpense.tranDate>((object) row, (object) row.TranDate, (Exception) null);
  }

  protected void CAExpense_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    CAExpense row = (CAExpense) e.Row;
    if (!row.CashTranID.HasValue)
      row.BatchNbr = (string) null;
    this.RecalcTotalExpense(sender, ((PXSelectBase<CATransfer>) this.Transfer).Current);
  }

  protected void CAExpense_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    CAExpense row = (CAExpense) e.Row;
    PXDefaultAttribute.SetPersistingCheck<CAExpense.taxCategoryID>(sender, (object) row, string.IsNullOrEmpty(row.TaxZoneID) ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PX.Objects.CA.CashAccount cashAccount = PXResultset<PX.Objects.CA.CashAccount>.op_Implicit(((PXSelectBase<PX.Objects.CA.CashAccount>) this.CashAccount).Select(new object[1]
    {
      (object) row.CashAccountID
    }));
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(((PXSelectBase<PX.Objects.GL.Account>) this.account).Select(new object[1]
    {
      (object) (int?) cashAccount?.AccountID
    }));
    if (account == null)
      return;
    bool? active = account.Active;
    bool flag = false;
    if (!(active.GetValueOrDefault() == flag & active.HasValue))
      return;
    sender.RaiseExceptionHandling<CAExpense.cashAccountID>(e.Row, (object) cashAccount.CashAccountCD, (Exception) new PXSetPropertyException<CAExpense.cashAccountID>("The {0} GL account used with this cash account is inactive. To use this GL account, activate the account on the Chart of Accounts (GL202500) form.", (PXErrorLevel) 4, new object[2]
    {
      (object) cashAccount.CashAccountCD,
      (object) account.AccountCD
    }));
  }

  protected void CAExpense_RowDeleted(PXCache sender, PXRowDeleted e)
  {
    this.RecalcTotalExpense(sender, ((PXSelectBase<CATransfer>) this.Transfer).Current);
  }

  protected void _(PX.Data.Events.FieldUpdated<CAExpense.curyTranAmt> e)
  {
    this.RecalcTotalExpense(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CAExpense.curyTranAmt>>) e).Cache, ((PXSelectBase<CATransfer>) this.Transfer).Current);
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Tax Amount")]
  protected virtual void CAExpense_CuryTaxTotal_Cacheattached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Enabled", false)]
  protected virtual void CAExpense_ExpenseNbr_Cacheattached(PXCache sender)
  {
  }

  [Obsolete("Will be removed in Acumatica 2019R2")]
  private void SetAdjRefNbrVisibility()
  {
    PXUIFieldAttribute.SetVisible<CAExpense.adjRefNbr>(((PXSelectBase) this.Expenses).Cache, (object) null, PXResultset<CAAdj>.op_Implicit(PXSelectBase<CAAdj, PXSelect<CAAdj, Where<CAAdj.transferNbr, Equal<Current<CATransfer.transferNbr>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())) != null);
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (CashAccountAttribute), "DisplayName", "Account")]
  protected virtual void CATransfer_OutAccountID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void CATransfer_OutAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CATransfer row = e.Row as CATransfer;
    if (!row.OutAccountID.HasValue)
      return;
    PX.Objects.CA.CashAccount cashAccount = (PX.Objects.CA.CashAccount) PXSelectorAttribute.Select<CATransfer.outAccountID>(sender, e.Row);
    if ((cashAccount != null ? (!cashAccount.Reconcile.GetValueOrDefault() ? 1 : 0) : 1) != 0)
    {
      row.ClearedOut = new bool?(true);
      row.ClearDateOut = row.OutDate;
    }
    else
    {
      row.ClearedOut = new bool?(false);
      row.ClearDateOut = new DateTime?();
    }
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (CashAccountAttribute), "DisplayName", "Account")]
  protected virtual void CATransfer_InAccountID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void CATransfer_InAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CATransfer row = e.Row as CATransfer;
    if (!row.InAccountID.HasValue)
      return;
    PX.Objects.CA.CashAccount cashAccount = (PX.Objects.CA.CashAccount) PXSelectorAttribute.Select<CATransfer.inAccountID>(sender, (object) row);
    if ((cashAccount != null ? (!cashAccount.Reconcile.GetValueOrDefault() ? 1 : 0) : 1) != 0)
    {
      row.ClearedIn = new bool?(true);
      row.ClearDateIn = row.InDate;
    }
    else
    {
      row.ClearedIn = new bool?(false);
      row.ClearDateIn = new DateTime?();
    }
  }

  protected virtual void CAExpense_CashAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CAExpense row = e.Row as CAExpense;
    PX.Objects.CA.CashAccount cashAccount = (PX.Objects.CA.CashAccount) PXSelectorAttribute.Select<CAExpense.cashAccountID>(sender, e.Row);
    if ((cashAccount != null ? (!cashAccount.Reconcile.GetValueOrDefault() ? 1 : 0) : 1) != 0)
    {
      row.Cleared = new bool?(true);
      row.ClearDate = row.TranDate;
    }
    else
    {
      row.Cleared = new bool?(false);
      row.ClearDate = new DateTime?();
    }
  }

  protected virtual void CATransfer_InDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    CATransfer row = e.Row as CATransfer;
    if (!row.ClearedIn.GetValueOrDefault())
      return;
    PX.Objects.CA.CashAccount cashAccount = (PX.Objects.CA.CashAccount) PXSelectorAttribute.Select<CATransfer.inAccountID>(sender, e.Row);
    if (cashAccount == null || cashAccount.Reconcile.GetValueOrDefault())
      return;
    row.ClearDateIn = row.InDate;
  }

  protected virtual void CATransfer_OutDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    CATransfer row = e.Row as CATransfer;
    if (row.ClearedOut.GetValueOrDefault())
    {
      PX.Objects.CA.CashAccount cashAccount = (PX.Objects.CA.CashAccount) PXSelectorAttribute.Select<CATransfer.outAccountID>(sender, e.Row);
      if (cashAccount != null && !cashAccount.Reconcile.GetValueOrDefault())
        row.ClearDateOut = row.OutDate;
    }
    sender.SetValueExt<CATransfer.inDate>((object) row, (object) row.OutDate);
  }

  protected virtual void CATransfer_OutExtRefNbr_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CATransfer row = (CATransfer) e.Row;
    if (!string.IsNullOrEmpty(row.InExtRefNbr))
      return;
    row.InExtRefNbr = row.OutExtRefNbr;
  }

  protected virtual void CATransfer_Descr_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    CATransfer row = (CATransfer) e.Row;
    int num;
    if (row == null)
    {
      num = 1;
    }
    else
    {
      bool? released = row.Released;
      bool flag = false;
      num = !(released.GetValueOrDefault() == flag & released.HasValue) ? 1 : 0;
    }
    if (num != 0)
      return;
    foreach (PXResult<CAExpenseTaxTran> pxResult in ((PXSelectBase<CAExpenseTaxTran>) this.ExpenseTaxTrans).Select(Array.Empty<object>()))
    {
      CAExpenseTaxTran caExpenseTaxTran = PXResult<CAExpenseTaxTran>.op_Implicit(pxResult);
      caExpenseTaxTran.Description = row.Descr;
      ((PXSelectBase) this.ExpenseTaxTrans).Cache.Update((object) caExpenseTaxTran);
    }
  }

  protected virtual void CATransfer_Hold_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    CATransfer row = (CATransfer) e.Row;
    if ((bool) e.NewValue)
      return;
    Decimal? tranOut = row.TranOut;
    Decimal num1 = 0M;
    if (tranOut.GetValueOrDefault() == num1 & tranOut.HasValue)
      cache.RaiseExceptionHandling<CATransfer.curyTranOut>((object) row, (object) row.CuryTranOut, (Exception) new PXSetPropertyException("A fund transfer with {0} amount cannot be processed. To proceed, enter an amount greater than zero.", new object[1]
      {
        cache.GetValueExt<CATransfer.curyTranOut>((object) row)
      }));
    Decimal? tranIn = row.TranIn;
    Decimal num2 = 0M;
    if (!(tranIn.GetValueOrDefault() == num2 & tranIn.HasValue))
      return;
    cache.RaiseExceptionHandling<CATransfer.curyTranIn>((object) row, (object) row.CuryTranIn, (Exception) new PXSetPropertyException("A fund transfer with {0} amount cannot be processed. To proceed, enter an amount greater than zero.", new object[1]
    {
      cache.GetValueExt<CATransfer.curyTranIn>((object) row)
    }));
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Amount")]
  protected virtual void CATRansfer_CuryTranOut_CacheAttached(PXCache sender)
  {
  }

  protected virtual void CATransfer_CuryTranOut_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    this.VerifyTransferNonNegative(e.Row as CATransfer, e.NewValue as Decimal?);
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Amount")]
  protected virtual void CATRansfer_CuryTranIn_CacheAttached(PXCache sender)
  {
  }

  protected virtual void CATransfer_CuryTranIn_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    this.VerifyTransferNonNegative(e.Row as CATransfer, e.NewValue as Decimal?);
  }

  protected virtual void VerifyTransferNonNegative(CATransfer transfer, Decimal? amount)
  {
    if (transfer == null || transfer.Released.GetValueOrDefault())
      return;
    Decimal? nullable = amount;
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() < num & nullable.HasValue)
      throw new PXSetPropertyException("Cannot transfer negative amount.");
  }

  protected virtual void CATransfer_ClearedIn_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CATransfer row = (CATransfer) e.Row;
    if (row.ClearedIn.GetValueOrDefault())
    {
      if (row.ClearDateIn.HasValue)
        return;
      row.ClearDateIn = row.InDate;
    }
    else
      row.ClearDateIn = new DateTime?();
  }

  protected virtual void CATransfer_ClearedOut_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    CATransfer row = (CATransfer) e.Row;
    if (row.ClearedOut.GetValueOrDefault())
    {
      if (row.ClearDateOut.HasValue)
        return;
      row.ClearDateOut = row.OutDate;
    }
    else
      row.ClearDateOut = new DateTime?();
  }

  protected virtual void CATransfer_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    CATransfer row = (CATransfer) e.Row;
    bool valueOrDefault = ((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current.RequireExtRefNbr.GetValueOrDefault();
    if (!row.OutAccountID.HasValue)
      sender.RaiseExceptionHandling<CATransfer.outAccountID>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[outAccountID]"
      }));
    PXDefaultAttribute.SetPersistingCheck<CATransfer.outExtRefNbr>(sender, (object) row, valueOrDefault ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    int? nullable1 = row.InAccountID;
    if (!nullable1.HasValue)
      sender.RaiseExceptionHandling<CATransfer.inAccountID>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[inAccountID]"
      }));
    PXDefaultAttribute.SetPersistingCheck<CATransfer.inExtRefNbr>(sender, (object) row, valueOrDefault ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    nullable1 = row.OutAccountID;
    int? inAccountId = row.InAccountID;
    if (nullable1.GetValueOrDefault() == inAccountId.GetValueOrDefault() & nullable1.HasValue == inAccountId.HasValue)
      sender.RaiseExceptionHandling<CATransfer.inAccountID>((object) row, (object) null, (Exception) new PXSetPropertyException("The destination cash account must be different from the source cash account."));
    bool? hold1 = row.Hold;
    bool flag1 = false;
    Decimal? nullable2;
    if (hold1.GetValueOrDefault() == flag1 & hold1.HasValue)
    {
      nullable2 = row.TranOut;
      if (nullable2.HasValue)
      {
        nullable2 = row.TranOut;
        Decimal num = 0M;
        if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
          goto label_10;
      }
      sender.RaiseExceptionHandling<CATransfer.curyTranOut>((object) row, (object) row.CuryTranOut, (Exception) new PXSetPropertyException("A fund transfer with {0} amount cannot be processed. To proceed, enter an amount greater than zero.", new object[1]
      {
        sender.GetValueExt<CATransfer.curyTranOut>((object) row)
      }));
    }
label_10:
    bool? hold2 = row.Hold;
    bool flag2 = false;
    if (!(hold2.GetValueOrDefault() == flag2 & hold2.HasValue))
      return;
    nullable2 = row.TranIn;
    if (nullable2.HasValue)
    {
      nullable2 = row.TranIn;
      Decimal num = 0M;
      if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
        return;
    }
    sender.RaiseExceptionHandling<CATransfer.curyTranIn>((object) row, (object) row.CuryTranIn, (Exception) new PXSetPropertyException("A fund transfer with {0} amount cannot be processed. To proceed, enter an amount greater than zero.", new object[1]
    {
      sender.GetValueExt<CATransfer.curyTranIn>((object) row)
    }));
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Batch Number", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.GL.Batch.batchNbr, Where<PX.Objects.GL.Batch.module, Equal<BatchModule.moduleCA>>>))]
  public virtual void CATran_BatchNbr_CacheAttached(PXCache sender)
  {
  }

  [PXDBCurrency(typeof (CATran.curyInfoID), typeof (CATran.tranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  protected virtual void CATran_CuryTranAmt_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CAExpenseTaxTran, CAExpenseTaxTran.taxID> e)
  {
    CAExpenseTaxTran row = e.Row;
    if (row == null || ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CAExpenseTaxTran, CAExpenseTaxTran.taxID>, CAExpenseTaxTran, object>) e).OldValue == null || ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<CAExpenseTaxTran, CAExpenseTaxTran.taxID>, CAExpenseTaxTran, object>) e).OldValue == e.NewValue)
      return;
    ((PXSelectBase) this.ExpenseTaxTrans).Cache.SetDefaultExt<CAExpenseTaxTran.accountID>((object) row);
    ((PXSelectBase) this.ExpenseTaxTrans).Cache.SetDefaultExt<CAExpenseTaxTran.taxType>((object) row);
    ((PXSelectBase) this.ExpenseTaxTrans).Cache.SetDefaultExt<CAExpenseTaxTran.taxBucketID>((object) row);
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable viewDoc(PXAdapter adapter)
  {
    if (((PXSelectBase<CAExpense>) this.Expenses).Current.AdjRefNbr != null)
    {
      CATranEntry instance = PXGraph.CreateInstance<CATranEntry>();
      ((PXGraph) instance).Clear();
      ((PXSelectBase) this.Transfer).Cache.IsDirty = false;
      CAAdj caAdj = PXResultset<CAAdj>.op_Implicit(PXSelectBase<CAAdj, PXSelect<CAAdj, Where<CAAdj.adjRefNbr, Equal<Required<CAExpense.adjRefNbr>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<CAExpense>) this.Expenses).Current.AdjRefNbr
      }));
      ((PXSelectBase<CAAdj>) instance.CAAdjRecords).Current = caAdj;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Document");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CashTransferEntry.\u003C\u003Ec__DisplayClass58_0 cDisplayClass580 = new CashTransferEntry.\u003C\u003Ec__DisplayClass58_0();
    PXCache cach = ((PXGraph) this).Caches[typeof (CATransfer)];
    CATransfer current = ((PXSelectBase<CATransfer>) this.Transfer).Current;
    ((PXAction) this.Save).Press();
    this.CheckTransfer();
    this.CheckExpensesOnHold();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass580.list = new List<CARegister>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass580.list.Add(CATrxRelease.CARegister(current, PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.tranID, Equal<Required<CATransfer.tranIDIn>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) current.TranIDIn
    })) ?? throw new PXException("Cash Transaction Not Found")));
    if (PXResultset<CATran>.op_Implicit(PXSelectBase<CATran, PXSelect<CATran, Where<CATran.tranID, Equal<Required<CATransfer.tranIDOut>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) current.TranIDOut
    })) == null)
      throw new PXException("Cash Transaction Not Found");
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass580, __methodptr(\u003Crelease\u003Eb__0)));
    return (IEnumerable) new List<CATransfer>() { current };
  }

  private void CheckTransfer()
  {
    CATransfer current = ((PXSelectBase<CATransfer>) this.Transfer).Current;
    try
    {
      ((PXSelectBase) this.Transfer).Cache.VerifyFieldAndRaiseException<CATransfer.outAccountID>((object) current, true);
      ((PXSelectBase) this.Transfer).Cache.VerifyFieldAndRaiseException<CATransfer.inAccountID>((object) current, true);
    }
    catch
    {
      throw new PXException("Transfer {0} cannot be released. Please review the errors.", new object[1]
      {
        (object) current.TransferNbr
      });
    }
  }

  [Obsolete("Will be removed in Acumatica 2019R2")]
  private void CheckExpensesOnHold()
  {
    bool flag = false;
    List<string> stringList = new List<string>();
    foreach (PXResult<CAExpense> pxResult in ((PXSelectBase<CAExpense>) this.Expenses).Select(Array.Empty<object>()))
    {
      CAExpense caExpense = PXResult<CAExpense>.op_Implicit(pxResult);
      if (!string.IsNullOrEmpty(caExpense.AdjRefNbr))
        stringList.Add(caExpense.AdjRefNbr);
    }
    if (stringList.Count < 1)
      return;
    ((PXAction) this.Cancel).Press();
    foreach (PXResult<CAAdj> pxResult in PXSelectBase<CAAdj, PXSelectReadonly<CAAdj, Where<CAAdj.adjRefNbr, In<Required<CAExpense.adjRefNbr>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) stringList.ToArray()
    }))
    {
      CAAdj adj = PXResult<CAAdj>.op_Implicit(pxResult);
      if (adj.Hold.GetValueOrDefault())
      {
        CAExpense caExpense = PXResult<CAExpense>.op_Implicit(((IEnumerable<PXResult<CAExpense>>) ((IEnumerable<PXResult<CAExpense>>) ((PXSelectBase<CAExpense>) this.Expenses).Select(Array.Empty<object>())).ToArray<PXResult<CAExpense>>()).First<PXResult<CAExpense>>((Func<PXResult<CAExpense>, bool>) (m => PXResult<CAExpense>.op_Implicit(m)?.AdjRefNbr == adj.AdjRefNbr)));
        ((PXSelectBase) this.Expenses).Cache.RaiseExceptionHandling<CAExpense.adjRefNbr>((object) caExpense, (object) caExpense.AdjRefNbr, (Exception) new PXSetPropertyException("The expense is on hold and cannot be released. Review the expense and clear the On Hold check box.", (PXErrorLevel) 5));
        flag = true;
      }
    }
    if (!flag)
      return;
    PXGraph.ThrowWithoutRollback((Exception) new PXException("The transfer {0} cannot be released. At least one expense associated with the transfer is on hold or pending approval. First clear the On Hold check box for an expense document and then release the transfer.", new object[1]
    {
      (object) ((PXSelectBase<CATransfer>) this.Transfer).Current.TransferNbr
    }));
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable reverse(PXAdapter adapter)
  {
    return (IEnumerable) new List<CATransfer>()
    {
      this.ReverseTransfer()
    };
  }

  private CATransfer ReverseTransfer()
  {
    CATransfer current = ((PXSelectBase<CATransfer>) this.Transfer).Current;
    if (!this.AskUserApprovalToReverse(current))
      return current;
    IEnumerable<CAExpense> caExpenses = GraphHelper.RowCast<CAExpense>((IEnumerable) PXSelectBase<CAExpense, PXSelect<CAExpense, Where<CAExpense.refNbr, Equal<Required<CAExpense.refNbr>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) current.RefNbr
    }));
    List<CAExpenseTaxTran> listOfNewTaxTrans = this.CreateListOfNewTaxTrans(current.RefNbr);
    CATransfer copy = (CATransfer) ((PXSelectBase) this.Transfer).Cache.CreateCopy((object) current);
    ((PXSelectBase) this.inFinPeriod).Cache.Current = ((PXSelectBase) this.inFinPeriod).View.SelectSingleBound(new object[1]
    {
      (object) copy
    }, Array.Empty<object>());
    this.FinPeriodUtils.VerifyAndSetFirstOpenedFinPeriod<CATransfer.inPeriodID, CATransfer.inBranchID>(((PXSelectBase) this.Transfer).Cache, (object) copy, (PXSelectBase<OrganizationFinPeriod>) this.inFinPeriod);
    ((PXSelectBase) this.outFinPeriod).Cache.Current = ((PXSelectBase) this.outFinPeriod).View.SelectSingleBound(new object[1]
    {
      (object) copy
    }, Array.Empty<object>());
    this.FinPeriodUtils.VerifyAndSetFirstOpenedFinPeriod<CATransfer.outPeriodID, CATransfer.outBranchID>(((PXSelectBase) this.Transfer).Cache, (object) copy, (PXSelectBase<OrganizationFinPeriod>) this.outFinPeriod);
    ((PXSelectBase) this.Transfer).Cache.Clear();
    ((PXSelectBase) this.Expenses).Cache.Clear();
    this.SwapInOutFields(current, copy);
    this.SetOtherFields(copy);
    CATransfer caTransfer1 = ((PXSelectBase<CATransfer>) this.Transfer).Insert(copy);
    ((PXSelectBase) this.Expenses).Cache.SetDefaultExt<CATransfer.hold>((object) caTransfer1);
    caTransfer1.InDate = current.OutDate;
    CATransfer caTransfer2 = ((PXSelectBase<CATransfer>) this.Transfer).Update(caTransfer1);
    foreach (CAExpense expense in caExpenses)
      this.ReverseExpence(caTransfer2, expense, listOfNewTaxTrans);
    this.RemoveLinkOnAdj();
    this.FinPeriodUtils.CopyPeriods<CATransfer, CATransfer.inPeriodID, CATransfer.inTranPeriodID, CATransfer.outPeriodID, CATransfer.outTranPeriodID>(((PXSelectBase) this.Transfer).Cache, current, caTransfer2);
    this.FinPeriodUtils.CopyPeriods<CATransfer, CATransfer.outPeriodID, CATransfer.outTranPeriodID, CATransfer.inPeriodID, CATransfer.inTranPeriodID>(((PXSelectBase) this.Transfer).Cache, current, caTransfer2);
    return caTransfer2;
  }

  protected virtual List<CAExpenseTaxTran> CreateListOfNewTaxTrans(string refNbr)
  {
    List<CAExpenseTaxTran> listOfNewTaxTrans = new List<CAExpenseTaxTran>();
    foreach (PXResult<CAExpenseTaxTran> pxResult in PXSelectBase<CAExpenseTaxTran, PXSelectJoin<CAExpenseTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<CAExpenseTaxTran.taxID>>>, Where<CAExpenseTaxTran.refNbr, Equal<Required<CAExpense.refNbr>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) refNbr
    }))
    {
      CAExpenseTaxTran caExpenseTaxTran1 = PXResult<CAExpenseTaxTran>.op_Implicit(pxResult);
      CAExpenseTaxTran caExpenseTaxTran2 = new CAExpenseTaxTran();
      caExpenseTaxTran2.AccountID = caExpenseTaxTran1.AccountID;
      caExpenseTaxTran2.BranchID = caExpenseTaxTran1.BranchID;
      caExpenseTaxTran2.FinPeriodID = caExpenseTaxTran1.FinPeriodID;
      caExpenseTaxTran2.SubID = caExpenseTaxTran1.SubID;
      caExpenseTaxTran2.TaxBucketID = caExpenseTaxTran1.TaxBucketID;
      caExpenseTaxTran2.TaxID = caExpenseTaxTran1.TaxID;
      caExpenseTaxTran2.TaxType = caExpenseTaxTran1.TaxType;
      caExpenseTaxTran2.TaxZoneID = caExpenseTaxTran1.TaxZoneID;
      caExpenseTaxTran2.TranDate = caExpenseTaxTran1.TranDate;
      caExpenseTaxTran2.VendorID = caExpenseTaxTran1.VendorID;
      caExpenseTaxTran2.CuryID = caExpenseTaxTran1.CuryID;
      caExpenseTaxTran2.Description = caExpenseTaxTran1.Description;
      caExpenseTaxTran2.NonDeductibleTaxRate = caExpenseTaxTran1.NonDeductibleTaxRate;
      caExpenseTaxTran2.TaxRate = caExpenseTaxTran1.TaxRate;
      CAExpenseTaxTran caExpenseTaxTran3 = caExpenseTaxTran2;
      Decimal? nullable1 = caExpenseTaxTran1.CuryTaxableAmt;
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      caExpenseTaxTran3.CuryTaxableAmt = nullable2;
      CAExpenseTaxTran caExpenseTaxTran4 = caExpenseTaxTran2;
      nullable1 = caExpenseTaxTran1.CuryExemptedAmt;
      Decimal? nullable3 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      caExpenseTaxTran4.CuryExemptedAmt = nullable3;
      CAExpenseTaxTran caExpenseTaxTran5 = caExpenseTaxTran2;
      nullable1 = caExpenseTaxTran1.CuryTaxAmt;
      Decimal? nullable4 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      caExpenseTaxTran5.CuryTaxAmt = nullable4;
      CAExpenseTaxTran caExpenseTaxTran6 = caExpenseTaxTran2;
      nullable1 = caExpenseTaxTran1.CuryTaxAmtSumm;
      Decimal? nullable5 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      caExpenseTaxTran6.CuryTaxAmtSumm = nullable5;
      CAExpenseTaxTran caExpenseTaxTran7 = caExpenseTaxTran2;
      nullable1 = caExpenseTaxTran1.CuryExpenseAmt;
      Decimal? nullable6 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      caExpenseTaxTran7.CuryExpenseAmt = nullable6;
      CAExpenseTaxTran caExpenseTaxTran8 = caExpenseTaxTran2;
      nullable1 = caExpenseTaxTran1.TaxableAmt;
      Decimal? nullable7 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      caExpenseTaxTran8.TaxableAmt = nullable7;
      CAExpenseTaxTran caExpenseTaxTran9 = caExpenseTaxTran2;
      nullable1 = caExpenseTaxTran1.ExemptedAmt;
      Decimal? nullable8 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      caExpenseTaxTran9.ExemptedAmt = nullable8;
      CAExpenseTaxTran caExpenseTaxTran10 = caExpenseTaxTran2;
      nullable1 = caExpenseTaxTran1.TaxAmt;
      Decimal? nullable9 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      caExpenseTaxTran10.TaxAmt = nullable9;
      CAExpenseTaxTran caExpenseTaxTran11 = caExpenseTaxTran2;
      nullable1 = caExpenseTaxTran1.ExpenseAmt;
      Decimal? nullable10 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      caExpenseTaxTran11.ExpenseAmt = nullable10;
      caExpenseTaxTran2.LineNbr = caExpenseTaxTran1.LineNbr;
      listOfNewTaxTrans.Add(caExpenseTaxTran2);
    }
    return listOfNewTaxTrans;
  }

  protected virtual void ReverseExpence(
    CATransfer transfer,
    CAExpense expense,
    List<CAExpenseTaxTran> origTaxTrans)
  {
    this.ReverseCAExpenseEntry(transfer, expense);
    foreach (CAExpenseTaxTran caExpenseTaxTran in origTaxTrans.Where<CAExpenseTaxTran>((Func<CAExpenseTaxTran, bool>) (m =>
    {
      int? lineNbr1 = m.LineNbr;
      int? lineNbr2 = expense.LineNbr;
      return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
    })))
      ((PXSelectBase<CAExpenseTaxTran>) this.ExpenseTaxTrans).Insert(caExpenseTaxTran);
  }

  protected virtual void ReverseCAExpenseEntry(CATransfer transfer, CAExpense expense)
  {
    CAExpense dest;
    using (new CashTransferEntry.CancelCAExpenseTaxCalcModeFieldDefaulting(this, expense))
    {
      dest = this.Expenses.ReverseCharge(expense, true);
      CAExpense caExpense1 = dest;
      Decimal? nullable1 = expense.CuryTaxableAmt;
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      caExpense1.CuryTaxableAmt = nullable2;
      CAExpense caExpense2 = dest;
      nullable1 = expense.CuryTaxTotal;
      Decimal? nullable3 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      caExpense2.CuryTaxTotal = nullable3;
      dest.FinPeriodID = transfer.OutPeriodID;
      dest.TranPeriodID = transfer.OutTranPeriodID;
      dest.TaxCalcMode = expense.TaxCalcMode;
      dest.TaxZoneID = expense.TaxZoneID;
      dest.TaxCategoryID = expense.TaxCategoryID;
      dest = ((PXSelectBase<CAExpense>) this.Expenses).Insert(dest);
    }
    this.FinPeriodUtils.CopyPeriods<CAExpense, CAExpense.finPeriodID, CAExpense.tranPeriodID>(((PXSelectBase) this.Expenses).Cache, expense, dest);
  }

  public bool AskUserApprovalToReverse(CATransfer origDoc)
  {
    return CashTransferEntry.GetReversingTransfer((PXGraph) this, origDoc.TransferNbr).Count<CATransfer>() < 1 || ((PXSelectBase) this.Transfer).View.Ask(PXMessages.LocalizeNoPrefix("One or more reversing transactions already exist. Do you want to proceed with reversing the transaction?"), (MessageButtons) 4) == 6;
  }

  public static IEnumerable<CATransfer> GetReversingTransfer(PXGraph graph, string refNbr)
  {
    return GraphHelper.RowCast<CATransfer>((IEnumerable) PXSelectBase<CATransfer, PXSelectReadonly<CATransfer, Where<CATransfer.origTransferNbr, Equal<Required<CATransfer.origTransferNbr>>>>.Config>.Select(graph, new object[1]
    {
      (object) refNbr
    }));
  }

  private void RemoveLinkOnAdj()
  {
    foreach (PXResult<CAExpense> pxResult in ((PXSelectBase<CAExpense>) this.Expenses).Select(Array.Empty<object>()))
    {
      CAExpense caExpense = PXResult<CAExpense>.op_Implicit(pxResult);
      if (caExpense.AdjRefNbr != null)
        caExpense.AdjRefNbr = (string) null;
    }
  }

  protected virtual void SwapInOutFields(CATransfer currentTransfer, CATransfer reverseTransfer)
  {
    reverseTransfer.OutAccountID = currentTransfer.InAccountID;
    reverseTransfer.OutCuryID = currentTransfer.InCuryID;
    reverseTransfer.CuryTranOut = currentTransfer.CuryTranIn;
    reverseTransfer.TranOut = currentTransfer.TranIn;
    reverseTransfer.OutDate = currentTransfer.InDate;
    reverseTransfer.OutPeriodID = currentTransfer.InPeriodID;
    reverseTransfer.OutTranPeriodID = currentTransfer.InTranPeriodID;
    reverseTransfer.TranIDOut = new long?();
    reverseTransfer.OutExtRefNbr = currentTransfer.InExtRefNbr;
    reverseTransfer.ClearedOut = currentTransfer.ClearedIn;
    reverseTransfer.ClearDateOut = currentTransfer.ClearDateIn;
    reverseTransfer.InAccountID = currentTransfer.OutAccountID;
    reverseTransfer.InCuryID = currentTransfer.OutCuryID;
    reverseTransfer.CuryTranIn = currentTransfer.CuryTranOut;
    reverseTransfer.TranIn = currentTransfer.TranOut;
    reverseTransfer.InPeriodID = currentTransfer.OutPeriodID;
    reverseTransfer.InTranPeriodID = currentTransfer.OutTranPeriodID;
    reverseTransfer.TranIDIn = new long?();
    reverseTransfer.InExtRefNbr = currentTransfer.OutExtRefNbr;
    reverseTransfer.ClearedIn = currentTransfer.ClearedOut;
    reverseTransfer.ClearDateIn = currentTransfer.ClearDateOut;
    reverseTransfer.OrigTransferNbr = currentTransfer.TransferNbr;
  }

  private void SetOtherFields(CATransfer reverseTransfer)
  {
    reverseTransfer.Descr = $"The reversing transfer of the {reverseTransfer.TransferNbr} transfer";
    reverseTransfer.TransferNbr = (string) null;
    reverseTransfer.Released = new bool?(false);
    reverseTransfer.NoteID = new Guid?();
    reverseTransfer.ReverseCount = new int?();
    reverseTransfer.Hold = new bool?();
    reverseTransfer.Status = (string) null;
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewExpenseBatch(PXAdapter adapter)
  {
    if (((PXSelectBase<CAExpense>) this.Expenses).Current != null)
    {
      string valueExt = (string) ((PXSelectBase<CAExpense>) this.Expenses).GetValueExt<CAExpense.batchNbr>(((PXSelectBase<CAExpense>) this.Expenses).Current);
      if (valueExt != null)
        this.RedirectToBatch(valueExt);
    }
    return adapter.Get();
  }

  private void RedirectToBatch(string BatchNbr)
  {
    JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
    ((PXGraph) instance).Clear();
    ((PXSelectBase<PX.Objects.GL.Batch>) instance.BatchModule).Current = PXResultset<PX.Objects.GL.Batch>.op_Implicit(PXSelectBase<PX.Objects.GL.Batch, PXSelect<PX.Objects.GL.Batch, Where<PX.Objects.GL.Batch.module, Equal<BatchModule.moduleCA>, And<PX.Objects.GL.Batch.batchNbr, Equal<Required<PX.Objects.GL.Batch.batchNbr>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) BatchNbr
    }));
    throw new PXRedirectRequiredException((string) null, (PXGraph) instance, (PXBaseRedirectException.WindowMode) 3, "Batch Record");
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CAReversingTransfers(PXAdapter adapter)
  {
    if (((PXSelectBase<CATransfer>) this.Transfer).Current != null)
      throw new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["TransferNbr"] = ((PXSelectBase<CATransfer>) this.Transfer).Current.TransferNbr
      }, "CA659500", "CA Reversing Transfers", (CurrentLocalization) null);
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CATransfer> e)
  {
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CATransfer>>) e).Cache.VerifyFieldAndRaiseException<CATransfer.inAccountID>((object) e.Row);
  }

  /// <summary>
  /// The defailting of the taxCalcMode field is overiden because we do not use the CashTransferEntryTax extension for reversing transfers.
  /// After it is overriten, the (newValue != (string)e.OldValue) condition always returns falls in the TaxBaseGraph.Document_TaxCalcMode_FieldUpdated handler
  /// </summary>
  public class CancelCAExpenseTaxCalcModeFieldDefaulting : IDisposable
  {
    private PXFieldDefaulting Event;

    private CashTransferEntry Graph { get; set; }

    public CancelCAExpenseTaxCalcModeFieldDefaulting(CashTransferEntry graph, CAExpense expense)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CashTransferEntry.CancelCAExpenseTaxCalcModeFieldDefaulting.\u003C\u003Ec__DisplayClass5_0 cDisplayClass50 = new CashTransferEntry.CancelCAExpenseTaxCalcModeFieldDefaulting.\u003C\u003Ec__DisplayClass5_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass50.expense = expense;
      // ISSUE: explicit constructor call
      base.\u002Ector();
      this.Graph = graph;
      // ISSUE: method pointer
      this.Event = new PXFieldDefaulting((object) cDisplayClass50, __methodptr(\u003C\u002Ector\u003Eb__0));
      ((PXGraph) this.Graph).FieldDefaulting.AddHandler<CAExpense.taxCalcMode>(this.Event);
    }

    public void Dispose()
    {
      ((PXGraph) this.Graph).FieldDefaulting.RemoveHandler<CAExpense.taxCalcMode>(this.Event);
    }
  }

  /// <summary>
  /// A per-unit tax graph extension for which will forbid edit of per-unit taxes in UI.
  /// </summary>
  public class PerUnitTaxDisableExt : 
    PerUnitTaxDataEntryGraphExtension<CashTransferEntry, CAExpenseTax>
  {
    public static bool IsActive()
    {
      return PerUnitTaxDataEntryGraphExtension<CashTransferEntry, CAExpenseTax>.IsActiveBase();
    }
  }
}
