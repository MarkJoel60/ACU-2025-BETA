// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ExpenseClaimDetailEntryExt`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.EP.DAC;
using System;

#nullable disable
namespace PX.Objects.EP;

/// <summary>Event Handlers</summary>
/// <typeparam name="TGraph"></typeparam>
public abstract class ExpenseClaimDetailEntryExt<TGraph> : ExpenseClaimDetailGraphExtBase<TGraph> where TGraph : PXGraph
{
  [PXCopyPasteHiddenView]
  public PXSetup<PX.Objects.EP.EPSetup> epsetup;

  public virtual bool UseClaimStatus => false;

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.inventoryID> e)
  {
    Decimal? costByExpenseItem = this.GetUnitCostByExpenseItem(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.inventoryID>>) e).Cache, e.Row);
    if (!costByExpenseItem.HasValue)
      return;
    Decimal? nullable = costByExpenseItem;
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.inventoryID>>) e).Cache.SetValueExt<EPExpenseClaimDetails.curyUnitCost>((object) e.Row, (object) costByExpenseItem);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<EPExpenseClaimDetails, EPExpenseClaimDetails.curyUnitCost> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<EPExpenseClaimDetails, EPExpenseClaimDetails.curyUnitCost>, EPExpenseClaimDetails, object>) e).NewValue = (object) this.GetUnitCostByExpenseItem(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<EPExpenseClaimDetails, EPExpenseClaimDetails.curyUnitCost>>) e).Cache, e.Row);
  }

  public virtual void _(PX.Data.Events.RowSelected<EPExpenseClaimDetails> e)
  {
    EPExpenseClaimDetails row1 = e.Row;
    if (row1 == null)
      return;
    EPExpenseClaim parentClaim = this.GetParentClaim(row1.RefNbr);
    bool flag1 = PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() && ((PXSelectBase<PX.Objects.EP.EPSetup>) this.epsetup).Current.ClaimDetailsAssignmentMapID.HasValue;
    bool? nullable = row1.LegacyReceipt;
    bool flag2 = nullable.GetValueOrDefault() && !string.IsNullOrEmpty(row1.RefNbr);
    nullable = row1.Hold;
    int num1;
    if (!nullable.GetValueOrDefault() && flag1)
    {
      if (this.UseClaimStatus && parentClaim != null)
      {
        nullable = parentClaim.Hold;
        if (nullable.GetValueOrDefault())
          goto label_4;
      }
      num1 = 0;
      goto label_6;
    }
label_4:
    num1 = !flag2 ? 1 : 0;
label_6:
    bool enabledEditReceipt = num1 != 0;
    if (parentClaim != null)
    {
      nullable = parentClaim.Hold;
      bool valueOrDefault = nullable.GetValueOrDefault();
      enabledEditReceipt &= valueOrDefault;
    }
    DateTime? bankTranDate = row1.BankTranDate;
    bool notMatchedToBankTran = (!bankTranDate.HasValue ? 1 : 0) != 0;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.Adjust<PXUIFieldAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) null).For<EPExpenseClaimDetails.expenseDate>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = notMatchedToBankTran & enabledEditReceipt));
    chained = chained.SameFor<EPExpenseClaimDetails.qty>();
    chained = chained.SameFor<EPExpenseClaimDetails.uOM>();
    chained = chained.SameFor<EPExpenseClaimDetails.curyUnitCost>();
    chained = chained.SameFor<EPExpenseClaimDetails.curyEmployeePart>();
    chained = chained.SameFor<EPExpenseClaimDetails.curyExtCost>();
    chained.SameFor<EPExpenseClaimDetails.paidWith>();
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.curyTipAmt>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) row1, notMatchedToBankTran & enabledEditReceipt && row1.PaidWith != "CardPers");
    int num2;
    if (row1.PaidWith != "PersAcc" & notMatchedToBankTran)
    {
      nullable = row1.Released;
      num2 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    bool flag3 = num2 != 0;
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.corpCardID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) row1, flag3);
    int num3;
    if (row1.PaidWith != "CardPers")
    {
      nullable = row1.Released;
      num3 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 0;
    bool flag4 = num3 != 0;
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.contractID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) row1, flag4);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.taskID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) row1, flag4);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.costCodeID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache, (object) row1, flag4);
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache;
    EPExpenseClaimDetails row2 = row1;
    bankTranDate = row1.BankTranDate;
    int num4 = bankTranDate.HasValue ? 1 : 0;
    object[] objArray = new object[1];
    bankTranDate = row1.BankTranDate;
    ref DateTime? local = ref bankTranDate;
    objArray[0] = (object) (local.HasValue ? local.GetValueOrDefault().ToShortDateString() : (string) null);
    UIState.RaiseOrHideError<EPExpenseClaimDetails.claimDetailCD>(cache, (object) row2, num4 != 0, "Some boxes and actions on the form are unavailable because the expense receipt is matched to a bank statement record with the date of {0}.", (PXErrorLevel) 2, objArray);
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<EPExpenseClaimDetails>>) e).Cache.VerifyFieldAndRaiseException<EPExpenseClaimDetails.curyEmployeePart>((object) row1);
  }

  public virtual void _(PX.Data.Events.RowPersisting<EPExpenseClaimDetails> e)
  {
    if (e.Operation == 3)
      return;
    if (e.Row.PaidWith != "PersAcc" && !e.Row.CorpCardID.HasValue)
    {
      string name = typeof (EPExpenseClaimDetails.corpCardID).Name;
      throw new PXRowPersistingException(name, (object) e.Row.CorpCardID, "'{0}' cannot be empty.", new object[1]
      {
        (object) $"[{name}]"
      });
    }
    EPExpenseClaim parentClaim = this.GetParentClaim(e.Row.RefNbr);
    this.VerifyEmployeePartIsZeroForCorpCardReceipt(e.Row.PaidWith, e.Row.CuryEmployeePart);
    this.VerifyEmployeePartNotExceedTotal(e.Row.CuryExtCost, e.Row.CuryEmployeePart);
    this.VerifyEmployeePartSign(e.Row.CuryExtCost, e.Row.CuryEmployeePart);
    this.VerifyEmployeeAndClaimCurrenciesForCash(e.Row, e.Row.PaidWith, parentClaim);
    this.VerifyClaimAndCorpCardCurrencies(e.Row.CorpCardID, parentClaim);
    this.VerifyExpenseRefNbrIsNotEmpty(e.Row);
  }

  public virtual void _(PX.Data.Events.RowInserted<EPExpenseClaimDetails> e)
  {
    EPExpenseClaimDetails row = e.Row;
    if (row == null)
      return;
    this.DefaultCardCurrencyInfo(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<EPExpenseClaimDetails>>) e).Cache, row);
    this.SetClaimCuryWhenNotInClaim(row, row.RefNbr, row.CorpCardID);
    this.SetCardCurrencyData(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<EPExpenseClaimDetails>>) e).Cache, row, row.CorpCardID);
    this.ClearFieldsIfNeeded(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<EPExpenseClaimDetails>>) e).Cache, row);
  }

  public virtual void _(PX.Data.Events.RowUpdated<EPExpenseClaimDetails> e)
  {
    this.AmtFieldUpdated(e.OldRow, e.Row);
  }

  public virtual void _(
    PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.paidWith> e)
  {
    EPExpenseClaimDetails row = e.Row;
    if (row == null)
      return;
    this.ClearFieldsIfNeeded(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.paidWith>>) e).Cache, row);
    PXUIFieldAttribute.SetError<EPExpenseClaimDetails.curyEmployeePart>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.paidWith>>) e).Cache, (object) row, (string) null);
    object obj = PXFormulaAttribute.Evaluate<EPExpenseClaimDetails.curyEmployeePart>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.paidWith>>) e).Cache, (object) row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.paidWith>>) e).Cache.SetValueExt<EPExpenseClaimDetails.curyEmployeePart>((object) row, obj ?? (object) row.CuryEmployeePart);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.paidWith>>) e).Cache.SetValuePending<EPExpenseClaimDetails.curyEmployeePart>((object) row, obj ?? (object) row.CuryEmployeePart);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.paidWith>>) e).Cache.VerifyFieldAndRaiseException<EPExpenseClaimDetails.curyEmployeePart>((object) row);
  }

  public virtual void _(
    PX.Data.Events.FieldDefaulting<EPExpenseClaimDetails.paidWith> e)
  {
    if (!(e.Row is EPExpenseClaimDetails row))
      return;
    int? employeeId = row.EmployeeID;
    if (!employeeId.HasValue)
      return;
    employeeId = row.EmployeeID;
    bool flag = this.GetFirstCreditCardForEmployeeAlphabeticallySorted(employeeId.Value) != null;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<EPExpenseClaimDetails.paidWith>, object, object>) e).NewValue = flag ? (object) "CardComp" : (object) "PersAcc";
  }

  public virtual void _(
    PX.Data.Events.FieldDefaulting<EPExpenseClaimDetails.corpCardID> e)
  {
    if (!(e.Row is EPExpenseClaimDetails row))
      return;
    int? employeeId = row.EmployeeID;
    if (!employeeId.HasValue)
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<EPExpenseClaimDetails.corpCardID>, object, object>) e).NewValue = (object) null;
    else if (row.PaidWith == "PersAcc")
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<EPExpenseClaimDetails.corpCardID>, object, object>) e).NewValue = (object) null;
    }
    else
    {
      employeeId = row.EmployeeID;
      EPEmployeeCorpCardLink alphabeticallySorted = this.GetFirstCreditCardForEmployeeAlphabeticallySorted(employeeId.Value);
      if (alphabeticallySorted == null)
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<EPExpenseClaimDetails.corpCardID>, object, object>) e).NewValue = (object) null;
      }
      else
      {
        employeeId = row.EmployeeID;
        EPExpenseClaimDetails creditCardForEmployee = this.GetLastUsedCreditCardForEmployee(employeeId.Value);
        int? corpCardID = creditCardForEmployee != null ? creditCardForEmployee.CorpCardID : alphabeticallySorted.CorpCardID;
        if (row.RefNbr != null)
        {
          EPExpenseClaim epExpenseClaim = PXParentAttribute.SelectParent<EPExpenseClaim>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<EPExpenseClaimDetails.corpCardID>>) e).Cache, (object) row);
          if (CACorpCardsMaint.GetCardCashAccount((PXGraph) this.Base, corpCardID).CuryID != epExpenseClaim.CuryID)
            corpCardID = new int?();
        }
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<EPExpenseClaimDetails.corpCardID>, object, object>) e).NewValue = (object) corpCardID;
        ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<EPExpenseClaimDetails.corpCardID>>) e).Cache.SetValueExt<EPExpenseClaimDetails.corpCardID>((object) row, (object) corpCardID);
      }
    }
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.curyExtCost> e)
  {
    EPExpenseClaimDetails row = (EPExpenseClaimDetails) e.Row;
    if (row == null || this.Base.IsCopyPasteContext)
      return;
    Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.curyExtCost>, object, object>) e).NewValue;
    if (!newValue.HasValue)
      return;
    this.VerifyIsPositiveForCorpCardReceipt((string) BqlHelper.GetValuePendingOrRow<EPExpenseClaimDetails.paidWith>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.curyExtCost>>) e).Cache, (object) row), newValue);
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.curyEmployeePart> e)
  {
    EPExpenseClaimDetails row = (EPExpenseClaimDetails) e.Row;
    if (row == null || this.Base.IsCopyPasteContext)
      return;
    Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.curyEmployeePart>, object, object>) e).NewValue;
    if (!newValue.HasValue)
      return;
    this.VerifyEmployeePartIsZeroForCorpCardReceipt((string) BqlHelper.GetValuePendingOrRow<EPExpenseClaimDetails.paidWith>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.curyEmployeePart>>) e).Cache, (object) row), newValue);
    Decimal? valuePendingOrRow = (Decimal?) BqlHelper.GetValuePendingOrRow<EPExpenseClaimDetails.curyExtCost>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.curyEmployeePart>>) e).Cache, (object) row);
    this.VerifyEmployeePartNotExceedTotal(valuePendingOrRow, newValue);
    this.VerifyEmployeePartSign(valuePendingOrRow, newValue);
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.refNbr> e)
  {
    EPExpenseClaimDetails row = (EPExpenseClaimDetails) e.Row;
    string newValue = (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.refNbr>, object, object>) e).NewValue;
    object valuePending = ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.refNbr>>) e).Cache.GetValuePending<EPExpenseClaimDetails.corpCardID>((object) row);
    int? nullable = new int?();
    int? corpCardID = valuePending != PXCache.NotSetValue ? (!(valuePending is string corpCardCD) ? (int?) valuePending : (int?) CACorpCard.UK.Find(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.refNbr>>) e).Cache.Graph, corpCardCD)?.CorpCardID) : row.CorpCardID;
    EPExpenseClaim parentClaim = this.GetParentClaim(newValue);
    this.VerifyClaimAndCorpCardCurrencies(corpCardID, parentClaim);
    this.VerifyEmployeeAndClaimCurrenciesForCash(row, row.PaidWith, parentClaim);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPExpenseClaimDetails.corpCardID> e)
  {
    EPExpenseClaimDetails row = (EPExpenseClaimDetails) e.Row;
    int? newValue = (int?) e.NewValue;
    this.SetCardCurrencyData(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails.corpCardID>>) e).Cache, row, newValue);
    this.SetClaimCuryWhenNotInClaim(row, row.RefNbr, newValue);
    this.RecalcAmountInClaimCury(row);
  }

  protected virtual void EPTaxTran_CuryTaxableAmt_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    this.RecalcAmountInClaimCury(this.Receipts.Current);
  }

  protected virtual void EPTaxTran_CuryTaxAmt_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    this.RecalcAmountInClaimCury(this.Receipts.Current);
  }

  protected virtual void EPTaxTran_ClaimCuryTaxAmt_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    this.RecalcAmountInClaimCury(this.Receipts.Current);
  }

  protected virtual void EPTaxTran_CuryExpenseAmt_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    this.RecalcAmountInClaimCury(this.Receipts.Current);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.CM.CurrencyInfo> e)
  {
    EPExpenseClaimDetails current = this.Receipts.Current;
    if (current == null)
      return;
    this.RecalcAmountInClaimCury(current);
  }
}
