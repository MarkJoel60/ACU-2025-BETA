// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ExpenseClaimDetailEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.EP.DAC;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.TX;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.EP;

public class ExpenseClaimDetailEntry : PXGraph<
#nullable disable
ExpenseClaimDetailEntry, EPExpenseClaimDetails>
{
  [PXViewName("Expense Receipt")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (EPExpenseClaimDetails.refNbr)})]
  public PXSelectJoin<EPExpenseClaimDetails, LeftJoin<EPExpenseClaim, On<EPExpenseClaim.refNbr, Equal<EPExpenseClaimDetails.refNbr>>, LeftJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<EPExpenseClaimDetails.employeeID>>>>, Where<EPEmployee.defContactID, Equal<Current<AccessInfo.contactID>>, Or<EPExpenseClaimDetails.createdByID, Equal<Current<AccessInfo.userID>>, Or<EPExpenseClaimDetails.employeeID, WingmanUser<Current<AccessInfo.userID>, EPDelegationOf.expenses>, Or<EPEmployee.defContactID, IsSubordinateOfContact<Current<AccessInfo.contactID>>, Or<EPExpenseClaimDetails.noteID, Approver<Current<AccessInfo.contactID>>, Or<EPExpenseClaim.noteID, Approver<Current<AccessInfo.contactID>>>>>>>>, OrderBy<Desc<EPExpenseClaimDetails.claimDetailID>>> ClaimDetails;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (EPExpenseClaimDetails.refNbr)})]
  public PXSelect<EPExpenseClaimDetails, Where<EPExpenseClaimDetails.claimDetailID, Equal<Current<EPExpenseClaimDetails.claimDetailID>>>> CurrentClaimDetails;
  [PXCopyPasteHiddenView]
  public PXSelect<EPExpenseClaim, Where<EPExpenseClaim.refNbr, Equal<Optional2<EPExpenseClaimDetails.refNbr>>>> CurrentClaim;
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.CM.CurrencyInfo> currencyinfo;
  [PXCopyPasteHiddenView]
  public PXSetup<EPSetup> epsetup;
  [PXCopyPasteHiddenView]
  public PXSetup<GLSetup> glsetup;
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.CM.Currency> currency;
  [PXCopyPasteHiddenView]
  public PXSelect<CurrencyList, Where<CurrencyList.isActive, Equal<True>>> currencyList;
  [PXCopyPasteHiddenView]
  public PXSetup<Company> comapny;
  [PXViewName("Employee")]
  [PXCopyPasteHiddenView]
  public PXSelect<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<EPExpenseClaimDetails.employeeID>>>> Employee;
  [PXCopyPasteHiddenView]
  [PXViewName("Approval")]
  public EPApprovalAutomation<EPExpenseClaimDetails, EPExpenseClaimDetails.approved, EPExpenseClaimDetails.rejected, EPExpenseClaimDetails.hold, EPSetupExpenseClaimDetailApproval> Approval;
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.CT.Contract, Where<PX.Objects.CT.Contract.contractID, Equal<Current<EPExpenseClaimDetails.contractID>>>> CurrentContract;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<EPTax, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<EPTax.taxID>>>, Where<EPTax.claimDetailID, Equal<Current<EPExpenseClaimDetails.claimDetailID>>>> TaxRows;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<EPTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<EPTaxTran.taxID>>>, Where<EPTaxTran.claimDetailID, Equal<Current<EPExpenseClaimDetails.claimDetailID>>>> Taxes;
  [PXCopyPasteHiddenView]
  public PXSelectReadonly2<EPTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<EPTaxTran.taxID>>>, Where<EPTaxTran.claimDetailID, Equal<Current<EPExpenseClaimDetails.claimDetailID>>, And<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.use>>>> UseTaxes;
  [PXCopyPasteHiddenView]
  public PXSelect<EPTaxAggregate, Where<EPTaxAggregate.refNbr, Equal<Current<EPExpenseClaimDetails.refNbr>>>> TaxAggregate;
  public PXFilter<ExpenseClaimDetailEntry.TaxZoneUpdateAsk> TaxZoneUpdateAskView;
  [PXCopyPasteHiddenView]
  public PXSelect<ExpenseClaimDetailEntry.ApprovalSetup> ApprovalSetupView;
  public PXAction<EPExpenseClaimDetails> Submit;
  public PXAction<EPExpenseClaimDetails> Claim;
  public PXAction<EPExpenseClaimDetails> hold;
  public PXInitializeState<EPExpenseClaimDetails> initializeState;
  public PXAction<EPExpenseClaimDetails> SaveTaxZone;
  public ToggleCurrency<EPExpenseClaimDetails> CurrencyView;

  public IEnumerable approvalSetupView()
  {
    yield return (object) new ExpenseClaimDetailEntry.ApprovalSetup()
    {
      ApprovalEnabled = new bool?(PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() && ((PXSelectBase<EPSetup>) this.epsetup).Current.ClaimDetailsAssignmentMapID.HasValue)
    };
  }

  public ExpenseClaimDetailEntry.ExpenseClaimDetailEntryExt ReceiptEntryExt
  {
    get
    {
      return ((PXGraph) this).FindImplementation<ExpenseClaimDetailEntry.ExpenseClaimDetailEntryExt>();
    }
  }

  [PXUIField]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable submit(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable claim(PXAdapter adapter)
  {
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003Cclaim\u003Eb__26_0)));
    return adapter.Get();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold")]
  protected virtual IEnumerable Hold(PXAdapter adapter) => adapter.Get();

  [PXUIField(DisplayName = "Yes")]
  [PXButton]
  protected virtual IEnumerable saveTaxZone(PXAdapter adapter)
  {
    ((PXSelectBase) this.Employee).Cache.SetValue<EPEmployee.receiptAndClaimTaxZoneID>((object) ((PXSelectBase<EPEmployee>) this.Employee).Current, (object) ((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Current.TaxZoneID);
    ((PXSelectBase<EPEmployee>) this.Employee).Update(((PXSelectBase<EPEmployee>) this.Employee).Current);
    return adapter.Get();
  }

  public ExpenseClaimDetailEntry()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<PX.Objects.IN.InventoryItem.stkItem>(ExpenseClaimDetailEntry.\u003C\u003Ec.\u003C\u003E9__33_0 ?? (ExpenseClaimDetailEntry.\u003C\u003Ec.\u003C\u003E9__33_0 = new PXFieldDefaulting((object) ExpenseClaimDetailEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__33_0))));
    PXUIFieldAttribute.SetVisible<EPExpenseClaimDetails.contractID>(((PXSelectBase) this.ClaimDetails).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.contractManagement>() || PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>());
  }

  public virtual void InitCacheMapping(Dictionary<System.Type, System.Type> map)
  {
    ((PXGraph) this).InitCacheMapping(map);
    ((PXGraph) this).Caches.AddCacheMappingsWithInheritance((PXGraph) this, typeof (PX.Objects.CT.Contract));
  }

  public virtual void Clear(PXClearOption option)
  {
    ((PXSelectBase) this.CurrentClaim).Cache.ClearQueryCache();
    ((PXGraph) this).Clear(option);
  }

  public virtual void _(PX.Data.Events.RowDeleting<EPExpenseClaimDetails> e)
  {
    if (e.Row.BankTranDate.HasValue)
      throw new PXException("The expense receipt cannot be deleted because it is matched to a bank statement record with the date of {0}.", new object[1]
      {
        (object) e.Row.BankTranDate
      });
  }

  public virtual void _(PX.Data.Events.RowPersisted<EPExpenseClaimDetails> e)
  {
    if (e.Operation != 2 || e.TranStatus != null)
      return;
    ((PXGraph) this).FindImplementation<ExpenseClaimDetailEntry.ExpenseClaimDetailEntryExt>().SetClaimCuryWhenNotInClaim(e.Row, e.Row.RefNbr, e.Row.CorpCardID);
    long? claimCuryInfoId = e.Row.ClaimCuryInfoID;
    long? cardCuryInfoId = e.Row.CardCuryInfoID;
    if (!(claimCuryInfoId.GetValueOrDefault() == cardCuryInfoId.GetValueOrDefault() & claimCuryInfoId.HasValue == cardCuryInfoId.HasValue))
      return;
    PXUpdate<Set<EPExpenseClaimDetails.claimCuryInfoID, Required<EPExpenseClaimDetails.claimCuryInfoID>>, EPExpenseClaimDetails, Where<EPExpenseClaimDetails.claimDetailCD, Equal<Required<EPExpenseClaimDetails.claimDetailCD>>>>.Update((PXGraph) this, new object[2]
    {
      (object) e.Row.ClaimCuryInfoID,
      (object) e.Row.ClaimDetailCD
    });
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.corpCardID> e)
  {
    EPExpenseClaimDetails receipt = (EPExpenseClaimDetails) e.Row;
    this.ReceiptEntryExt.VerifyClaimAndCorpCardCurrencies((int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.corpCardID>, object, object>) e).NewValue, this.ReceiptEntryExt.GetParentClaim((string) BqlHelper.GetValuePendingOrRow<EPExpenseClaimDetails.refNbr>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.corpCardID>>) e).Cache, (object) receipt)), (System.Action) (() => ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.corpCardID>, object, object>) e).NewValue = VerifyingHelper.GetNewValueByIncoming(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.corpCardID>>) e).Cache, (object) receipt, typeof (EPExpenseClaimDetails.corpCardID).Name, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.corpCardID>>) e).ExternalCall)));
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.paidWith> e)
  {
    EPExpenseClaimDetails row = (EPExpenseClaimDetails) e.Row;
    string newValue = (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.paidWith>, object, object>) e).NewValue;
    Decimal? valuePendingOrRow1 = (Decimal?) BqlHelper.GetValuePendingOrRow<EPExpenseClaimDetails.curyExtCost>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.paidWith>>) e).Cache, (object) row);
    this.ReceiptEntryExt.VerifyIsPositiveForCorpCardReceipt(newValue, valuePendingOrRow1);
    Decimal? valuePendingOrRow2 = (Decimal?) BqlHelper.GetValuePendingOrRow<EPExpenseClaimDetails.curyEmployeePart>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.paidWith>>) e).Cache, (object) row);
    this.ReceiptEntryExt.VerifyEmployeePartIsZeroForCorpCardReceipt(newValue, valuePendingOrRow2);
    string valuePendingOrRow3 = (string) BqlHelper.GetValuePendingOrRow<EPExpenseClaimDetails.refNbr>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.paidWith>>) e).Cache, (object) row);
    this.ReceiptEntryExt.VerifyEmployeeAndClaimCurrenciesForCash(row, newValue, this.ReceiptEntryExt.GetParentClaim(valuePendingOrRow3));
  }

  protected void EPExpenseClaimDetails_TaxZoneID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    EPExpenseClaimDetails row = e.Row as EPExpenseClaimDetails;
    e.NewValue = (object) this.GetDefaultTaxZone(row);
  }

  protected void EPExpenseClaimDetails_TaxCalcMode_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is EPExpenseClaimDetails row) || row.RefNbr == null)
      return;
    PXSetup<EPSetup> epsetup = this.epsetup;
    int num;
    if (epsetup == null)
    {
      num = 0;
    }
    else
    {
      bool? taxSettingInClaims = (bool?) ((PXSelectBase<EPSetup>) epsetup).Current?.AllowMixedTaxSettingInClaims;
      bool flag = false;
      num = taxSettingInClaims.GetValueOrDefault() == flag & taxSettingInClaims.HasValue ? 1 : 0;
    }
    if (num == 0)
      return;
    e.NewValue = (object) ((PXSelectBase<EPExpenseClaim>) this.CurrentClaim).SelectSingle(new object[1]
    {
      (object) row.RefNbr
    }).TaxCalcMode;
  }

  protected void EPExpenseClaimDetails_ExpenseDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if ((EPExpenseClaimDetails) e.Row == null || e.NewValue != null)
      return;
    e.NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate;
  }

  protected virtual void EPExpenseClaimDetails_ExpenseAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is EPExpenseClaimDetails row) || row.ContractID.HasValue)
      return;
    sender.SetDefaultExt<EPExpenseClaimDetails.contractID>((object) row);
  }

  protected virtual void EPExpenseClaimDetails_Hold_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) (bool) (!PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>() ? 0 : (((PXSelectBase<EPSetup>) this.epsetup).Current.ClaimDetailsAssignmentMapID.HasValue ? 1 : 0));
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void EPExpenseClaimDetails_ExpenseSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ExpenseClaimDetailGraphExtBase<ExpenseClaimDetailEntry>.ExpenseSubID_FieldDefaulting(sender, e, ((PXSelectBase<EPSetup>) this.epsetup).Current.ExpenseSubMask, ((PXSelectBase<EPSetup>) this.epsetup).Current.ExpenseSubMaskNB);
  }

  protected virtual void EPExpenseClaimDetails_SalesSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ExpenseClaimDetailGraphExtBase<ExpenseClaimDetailEntry>.SalesSubID_FieldDefaulting(sender, e, ((PXSelectBase<EPSetup>) this.epsetup).Current.SalesSubMask);
  }

  protected virtual void EPExpenseClaimDetails_ExpenseDate_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    CurrencyInfoAttribute.SetEffectiveDate<EPExpenseClaimDetails.expenseDate, EPExpenseClaimDetails.curyInfoID>(cache, e);
    CurrencyInfoAttribute.SetEffectiveDate<EPExpenseClaimDetails.expenseDate, EPExpenseClaimDetails.cardCuryInfoID>(cache, e);
  }

  protected virtual void EPExpenseClaimDetails_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    EPExpenseClaimDetails row = (EPExpenseClaimDetails) e.Row;
    PX.Objects.CM.CurrencyInfo currencyInfo = CurrencyInfoAttribute.SetDefaults<EPExpenseClaimDetails.curyInfoID>(cache, e.Row);
    if (currencyInfo == null)
      return;
    if (row.CorpCardID.HasValue)
    {
      PX.Objects.CA.CashAccount cardCashAccount = CACorpCardsMaint.GetCardCashAccount((PXGraph) this, row.CorpCardID);
      cache.SetValueExt<EPExpenseClaimDetails.curyID>((object) row, (object) cardCashAccount.CuryID);
    }
    else
      row.CuryID = currencyInfo.CuryID;
  }

  protected virtual void EPExpenseClaimDetails_RefNbr_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    EPExpenseClaimDetails row = (EPExpenseClaimDetails) e.Row;
    if (row == null)
      return;
    ((PXGraph) this).FindImplementation<ExpenseClaimDetailEntry.ExpenseClaimDetailEntryExt>().RefNbrUpdated(cache, PXResultset<EPExpenseClaim>.op_Implicit(((PXSelectBase<EPExpenseClaim>) this.CurrentClaim).Select(new object[1]
    {
      (object) row.RefNbr
    })), row, (string) e.OldValue);
  }

  protected virtual void EPExpenseClaimDetails_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    EPExpenseClaimDetails row1 = (EPExpenseClaimDetails) e.Row;
    if (row1 == null)
      return;
    EPExpenseClaim epExpenseClaim = PXResultset<EPExpenseClaim>.op_Implicit(PXSelectBase<EPExpenseClaim, PXSelect<EPExpenseClaim, Where<EPExpenseClaim.refNbr, Equal<Required<EPExpenseClaimDetails.refNbr>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1], new object[1]
    {
      (object) row1.RefNbr
    }));
    int? nullable1;
    int num1;
    if (PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>())
    {
      nullable1 = ((PXSelectBase<EPSetup>) this.epsetup).Current.ClaimDetailsAssignmentMapID;
      num1 = nullable1.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag1 = num1 != 0;
    bool flag2 = row1.LegacyReceipt.GetValueOrDefault() && !string.IsNullOrEmpty(row1.RefNbr);
    bool flag3 = (row1.Hold.GetValueOrDefault() || !flag1) && !flag2;
    bool flag4 = true;
    bool flag5 = flag3 && (!cache.AllowUpdate || string.IsNullOrEmpty(row1.RefNbr));
    bool? nullable2;
    int num2;
    if (!row1.Rejected.GetValueOrDefault())
    {
      nullable2 = row1.Released;
      num2 = !nullable2.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    bool flag6 = num2 != 0;
    bool flag7 = (((PXSelectBase<PX.Objects.CT.Contract>) this.CurrentContract).SelectSingle(Array.Empty<object>())?.ContractCD ?? "X").Trim() == "X";
    bool flag8 = false;
    bool flag9 = false;
    if (epExpenseClaim != null)
    {
      flag8 = true;
      nullable2 = row1.HoldClaim;
      bool valueOrDefault = nullable2.GetValueOrDefault();
      flag3 &= valueOrDefault;
      flag4 = valueOrDefault;
      flag5 = false;
      flag6 &= valueOrDefault;
      nullable2 = epExpenseClaim.Released;
      flag9 = nullable2.GetValueOrDefault();
    }
    int num3;
    if (flag4)
    {
      nullable2 = row1.LegacyReceipt;
      bool flag10 = false;
      num3 = nullable2.GetValueOrDefault() == flag10 & nullable2.HasValue ? 1 : 0;
    }
    else
      num3 = 0;
    bool flag11 = num3 != 0;
    ((PXSelectBase) this.Approval).AllowSelect = flag1;
    ((PXAction) this.Delete).SetEnabled(flag3 && epExpenseClaim == null);
    PXUIFieldAttribute.SetEnabled(cache, (object) row1, flag3);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.claimDetailID>(cache, (object) row1, true);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.refNbr>(cache, (object) row1, flag11);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.employeeID>(cache, (object) row1, flag5);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.branchID>(cache, (object) row1, flag5);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.expenseAccountID>(cache, (object) row1, flag6);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.expenseSubID>(cache, (object) row1, flag6);
    PXCache pxCache1 = cache;
    EPExpenseClaimDetails expenseClaimDetails1 = row1;
    int num4;
    if (flag6)
    {
      nullable2 = row1.Billable;
      num4 = nullable2.GetValueOrDefault() ? 1 : 0;
    }
    else
      num4 = 0;
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.salesAccountID>(pxCache1, (object) expenseClaimDetails1, num4 != 0);
    PXCache pxCache2 = cache;
    EPExpenseClaimDetails expenseClaimDetails2 = row1;
    int num5;
    if (flag6)
    {
      nullable2 = row1.Billable;
      num5 = nullable2.GetValueOrDefault() ? 1 : 0;
    }
    else
      num5 = 0;
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.salesSubID>(pxCache2, (object) expenseClaimDetails2, num5 != 0);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.customerID>(cache, (object) row1, flag7 && !flag9);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.customerLocationID>(cache, (object) row1, flag7 && !flag9);
    DateTime? nullable3;
    int num6;
    if (row1.PaidWith != "CardPers")
    {
      nullable2 = row1.Released;
      if (!nullable2.GetValueOrDefault())
      {
        nullable3 = row1.BankTranDate;
        num6 = !nullable3.HasValue ? 1 : 0;
        goto label_22;
      }
    }
    num6 = 0;
label_22:
    bool flag12 = num6 != 0;
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.taxCategoryID>(cache, (object) row1, flag6 & flag12);
    PXCache pxCache3 = cache;
    EPExpenseClaimDetails expenseClaimDetails3 = row1;
    int num7;
    if (flag3)
    {
      nullable3 = row1.BankTranDate;
      num7 = !nullable3.HasValue ? 1 : 0;
    }
    else
      num7 = 0;
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.curyID>(pxCache3, (object) expenseClaimDetails3, num7 != 0);
    PXAction<EPExpenseClaimDetails> submit = this.Submit;
    int num8;
    if (cache.GetStatus((object) row1) != 2)
    {
      nullable2 = row1.Hold;
      num8 = nullable2.GetValueOrDefault() ? 1 : 0;
    }
    else
      num8 = 0;
    ((PXAction) submit).SetEnabled(num8 != 0);
    PXAction<EPExpenseClaimDetails> claim = this.Claim;
    int num9;
    if (cache.GetStatus((object) row1) != 2)
    {
      nullable2 = row1.Approved;
      if (nullable2.GetValueOrDefault())
      {
        num9 = !flag8 ? 1 : 0;
        goto label_32;
      }
    }
    num9 = 0;
label_32:
    ((PXAction) claim).SetEnabled(num9 != 0);
    nullable1 = row1.ContractID;
    if (nullable1.HasValue)
    {
      nullable2 = row1.Billable;
      if (nullable2.Value)
      {
        nullable1 = row1.TaskID;
        if (nullable1.HasValue)
        {
          PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, row1.ContractID, row1.TaskID);
          if (dirty != null)
          {
            nullable2 = dirty.VisibleInAP;
            if (!nullable2.Value)
              cache.RaiseExceptionHandling<EPExpenseClaimDetails.taskID>(e.Row, (object) dirty.TaskCD, (Exception) new PXSetPropertyException("Project Task '{0}' is invisible in {1} module.", new object[2]
              {
                (object) dirty.TaskCD,
                (object) "AP"
              }));
          }
        }
      }
    }
    PX.Objects.CM.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<EPExpenseClaimDetails.curyInfoID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row1
    }, Array.Empty<object>()));
    DateTime? nullable4;
    if (currencyInfo != null && currencyInfo.CuryRateTypeID != null)
    {
      nullable3 = currencyInfo.CuryEffDate;
      if (nullable3.HasValue)
      {
        nullable3 = row1.ExpenseDate;
        if (nullable3.HasValue)
        {
          nullable3 = currencyInfo.CuryEffDate;
          nullable4 = row1.ExpenseDate;
          if ((nullable3.HasValue & nullable4.HasValue ? (nullable3.GetValueOrDefault() < nullable4.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            PX.Objects.CM.CurrencyRateType currencyRateType = (PX.Objects.CM.CurrencyRateType) PXSelectorAttribute.Select<PX.Objects.CM.CurrencyInfo.curyRateTypeID>(((PXSelectBase) this.currencyinfo).Cache, (object) currencyInfo);
            if (currencyRateType != null)
            {
              short? rateEffDays = currencyRateType.RateEffDays;
              nullable1 = rateEffDays.HasValue ? new int?((int) rateEffDays.GetValueOrDefault()) : new int?();
              int num10 = 0;
              if (nullable1.GetValueOrDefault() > num10 & nullable1.HasValue)
              {
                nullable4 = row1.ExpenseDate;
                nullable3 = currencyInfo.CuryEffDate;
                int days = (nullable4.HasValue & nullable3.HasValue ? new TimeSpan?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new TimeSpan?()).Value.Days;
                rateEffDays = currencyRateType.RateEffDays;
                nullable1 = rateEffDays.HasValue ? new int?((int) rateEffDays.GetValueOrDefault()) : new int?();
                int valueOrDefault = nullable1.GetValueOrDefault();
                if (days > valueOrDefault & nullable1.HasValue)
                {
                  string curyRateTypeId = currencyInfo.CuryRateTypeID;
                  string baseCuryId = currencyInfo.BaseCuryID;
                  string curyId = currencyInfo.CuryID;
                  nullable4 = row1.ExpenseDate;
                  DateTime CuryEffDate = nullable4.Value;
                  PXRateIsNotDefinedForThisDateException thisDateException = new PXRateIsNotDefinedForThisDateException(curyRateTypeId, baseCuryId, curyId, CuryEffDate);
                  cache.RaiseExceptionHandling<EPExpenseClaimDetails.expenseDate>(e.Row, (object) ((EPExpenseClaimDetails) e.Row).ExpenseDate, (Exception) thisDateException);
                }
              }
            }
          }
        }
      }
    }
    string str = PXUIFieldAttribute.GetError<PX.Objects.CM.CurrencyInfo.curyID>(((PXSelectBase) this.currencyinfo).Cache, (object) currencyInfo);
    Decimal? nullable5;
    if (string.IsNullOrEmpty(str) && currencyInfo != null)
    {
      nullable5 = currencyInfo.CuryRate;
      if (!nullable5.HasValue)
        str = "Currency Rate is not defined.";
    }
    if (string.IsNullOrEmpty(str))
      cache.RaiseExceptionHandling<EPExpenseClaimDetails.curyID>(e.Row, (object) null, (Exception) null);
    else
      cache.RaiseExceptionHandling<EPExpenseClaimDetails.curyID>(e.Row, (object) null, (Exception) new PXSetPropertyException(str, (PXErrorLevel) 2));
    Guid userId1 = ((PXGraph) this).Accessinfo.UserID;
    Guid? nullable6 = row1.CreatedByID;
    bool flag13 = nullable6.HasValue && userId1 == nullable6.GetValueOrDefault();
    EPEmployee employee = PXResultset<EPEmployee>.op_Implicit(((PXSelectBase<EPEmployee>) this.Employee).Select(Array.Empty<object>()));
    if (employee != null)
    {
      if (!flag13)
      {
        Guid userId2 = ((PXGraph) this).Accessinfo.UserID;
        nullable6 = employee.UserID;
        if ((nullable6.HasValue ? (userId2 == nullable6.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          flag13 = true;
      }
      if (!flag13)
      {
        if (PXResultset<EPWingmanForExpenses>.op_Implicit(PXSelectBase<EPWingmanForExpenses, PXSelectJoin<EPWingmanForExpenses, InnerJoin<EPEmployee, On<EPWingman.wingmanID, Equal<EPEmployee.bAccountID>>>, Where<EPWingman.employeeID, Equal<Required<EPWingman.employeeID>>, And<EPEmployee.userID, Equal<Required<EPEmployee.userID>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) row1.EmployeeID,
          (object) ((PXGraph) this).Accessinfo.UserID
        })) != null)
          flag13 = true;
      }
    }
    if (!flag13)
      ((PXAction) this.hold).SetEnabled(false);
    this.ValidateProjectAndProjectTask(row1);
    int num11;
    if (flag3)
    {
      nullable2 = ((PXSelectBase<EPSetup>) this.epsetup).Current.AllowMixedTaxSettingInClaims;
      if ((nullable2.GetValueOrDefault() || ((PXSelectBase<EPExpenseClaimDetails>) this.CurrentClaimDetails).Current.RefNbr == null) && row1.PaidWith != "CardPers")
      {
        nullable2 = row1.Released;
        if (!nullable2.GetValueOrDefault())
        {
          nullable4 = row1.BankTranDate;
          num11 = !nullable4.HasValue ? 1 : 0;
          goto label_66;
        }
      }
    }
    num11 = 0;
label_66:
    bool flag14 = num11 != 0;
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.taxZoneID>(cache, (object) row1, flag14 & flag12);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.taxCalcMode>(cache, (object) row1, flag14);
    PXCache cache1 = ((PXSelectBase) this.ClaimDetails).Cache;
    nullable1 = ((PXSelectBase<EPSetup>) this.epsetup).Current.NonTaxableTipItem;
    int num12;
    if (!nullable1.HasValue)
    {
      nullable5 = row1.CuryTipAmt;
      num12 = nullable5.GetValueOrDefault() != 0M ? 1 : 0;
    }
    else
      num12 = 1;
    PXUIFieldAttribute.SetVisible<EPExpenseClaimDetails.curyTipAmt>(cache1, (object) null, num12 != 0);
    PXCache cache2 = ((PXSelectBase) this.Taxes).Cache;
    int num13;
    if (flag3)
    {
      nullable4 = row1.BankTranDate;
      num13 = !nullable4.HasValue ? 1 : 0;
    }
    else
      num13 = 0;
    cache2.SetAllEditPermissions(num13 != 0);
    nullable2 = row1.LegacyReceipt;
    if (nullable2.GetValueOrDefault())
    {
      PXCache cache3 = cache;
      EPExpenseClaimDetails row2 = row1;
      int num14;
      if (flag2)
      {
        nullable2 = row1.Released;
        bool flag15 = false;
        num14 = nullable2.GetValueOrDefault() == flag15 & nullable2.HasValue ? 1 : 0;
      }
      else
        num14 = 0;
      object[] objArray1 = new object[1]
      {
        (object) row1.RefNbr
      };
      UIState.RaiseOrHideError<EPExpenseClaimDetails.refNbr>(cache3, (object) row2, num14 != 0, "This expense receipt, which is a legacy expense receipt created in an earlier version of Acumatica ERP, cannot be edited when included in an expense claim. To be able to modify data in this expense receipt, delete the expense claim {0}.", (PXErrorLevel) 2, objArray1);
      PXCache cache4 = cache;
      EPExpenseClaimDetails row3 = row1;
      nullable2 = row1.LegacyReceipt;
      int num15;
      if (nullable2.GetValueOrDefault())
      {
        nullable2 = row1.Released;
        bool flag16 = false;
        if (nullable2.GetValueOrDefault() == flag16 & nullable2.HasValue)
        {
          num15 = !string.IsNullOrEmpty(row1.TaxZoneID) ? 1 : 0;
          goto label_80;
        }
      }
      num15 = 0;
label_80:
      object[] objArray2 = Array.Empty<object>();
      UIState.RaiseOrHideError<EPExpenseClaimDetails.claimDetailID>(cache4, (object) row3, num15 != 0, "This receipt was created in an old version of Acumatica ERP. Taxes for this receipt will be calculated upon an update of the Tax Zone, Tax Category, Tax Calculation Mode, or Amount field.", (PXErrorLevel) 2, objArray2);
    }
    string taxZoneId = employee == null ? (string) null : ExpenseClaimDetailGraphExtBase<ExpenseClaimDetailEntry>.GetTaxZoneID((PXGraph) this, employee);
    bool flag17 = string.IsNullOrEmpty(row1.TaxZoneID) && !string.IsNullOrEmpty(taxZoneId);
    PXCache cache5 = cache;
    EPExpenseClaimDetails row4 = row1;
    int num16;
    if (flag17)
    {
      nullable2 = row1.Released;
      bool flag18 = false;
      if (nullable2.GetValueOrDefault() == flag18 & nullable2.HasValue)
      {
        num16 = row1.PaidWith != "CardPers" ? 1 : 0;
        goto label_85;
      }
    }
    num16 = 0;
label_85:
    object[] objArray = Array.Empty<object>();
    UIState.RaiseOrHideError<EPExpenseClaimDetails.taxZoneID>(cache5, (object) row4, num16 != 0, "Because the employee who is claiming the expenses has a tax zone specified, you may need to also specify a tax zone for the receipt.", (PXErrorLevel) 2, objArray);
    if (((PXSelectBase<EPTaxTran>) this.UseTaxes).Select(Array.Empty<object>()).Count != 0)
      cache.RaiseExceptionHandling<EPExpenseClaimDetails.curyTaxTotal>((object) row1, (object) row1.CuryTaxTotal, (Exception) new PXSetPropertyException("Use Tax is excluded from Tax Total.", (PXErrorLevel) 2));
    else
      cache.RaiseExceptionHandling<EPExpenseClaimDetails.curyTaxTotal>((object) row1, (object) row1.CuryTaxTotal, (Exception) null);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.claimDetailCD>(cache, (object) row1, true);
    bool flag19 = PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();
    PXUIFieldAttribute.SetVisible<EPExpenseClaimDetails.claimCuryTranAmtWithTaxes>(cache, (object) row1, row1.IsPaidWithCard & flag19);
    PXCache pxCache4 = cache;
    EPExpenseClaimDetails expenseClaimDetails4 = row1;
    nullable1 = row1.CorpCardID;
    int num17 = nullable1.HasValue & flag19 ? 1 : 0;
    PXUIFieldAttribute.SetVisible<EPExpenseClaimDetails.cardCuryID>(pxCache4, (object) expenseClaimDetails4, num17 != 0);
  }

  protected virtual void EPExpenseClaimDetails_RowPersisting(
    PXCache cache,
    PXRowPersistingEventArgs e)
  {
    EPExpenseClaimDetails row = (EPExpenseClaimDetails) e.Row;
    bool? nullable1;
    if (!string.IsNullOrEmpty(row.RefNbr) && e.Operation != 3)
    {
      EPExpenseClaim epExpenseClaim = ((PXSelectBase<EPExpenseClaim>) this.CurrentClaim).SelectSingle(new object[1]
      {
        (object) row.RefNbr
      });
      if (epExpenseClaim != null)
      {
        nullable1 = ((PXSelectBase<EPSetup>) this.epsetup).Current.AllowMixedTaxSettingInClaims;
        bool flag = false;
        if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
        {
          if (epExpenseClaim.TaxZoneID != ((PXSelectBase<EPExpenseClaimDetails>) this.CurrentClaimDetails).Current.TaxZoneID)
            cache.RaiseExceptionHandling<EPExpenseClaimDetails.taxZoneID>((object) row, (object) row.TaxZoneID, (Exception) new PXSetPropertyException("Cannot add the expense receipt {0} to the expense claim {1} because they have different tax zones specified.", new object[2]
            {
              (object) row.ClaimDetailID,
              (object) epExpenseClaim.RefNbr
            }));
          if (epExpenseClaim.TaxCalcMode != row.TaxCalcMode)
            cache.RaiseExceptionHandling<EPExpenseClaimDetails.taxCalcMode>((object) row, (object) row.TaxCalcMode, (Exception) new PXSetPropertyException("Cannot add the expense receipt {0} to the expense claim {1} because they have different tax calculation modes specified.", new object[2]
            {
              (object) row.ClaimDetailID,
              (object) epExpenseClaim.RefNbr
            }));
        }
      }
    }
    if (row == null || e.Operation == 3)
      return;
    nullable1 = row.Hold;
    bool flag1 = false;
    if (!(nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue))
      return;
    Decimal? nullable2;
    if (!((PXSelectBase<EPSetup>) this.epsetup).Current.NonTaxableTipItem.HasValue)
    {
      nullable2 = row.CuryTipAmt;
      if (nullable2.GetValueOrDefault() != 0M)
        cache.RaiseExceptionHandling<EPExpenseClaimDetails.curyTipAmt>((object) row, (object) row.CuryTipAmt, (Exception) new PXSetPropertyException("To be able to specify a nonzero tip amount in the expense receipt, specify an appropriate tip item in the Non-Taxable Tip Item box on the Time & Expenses Preferences (EP101000) form."));
    }
    PX.Objects.CM.CurrencyInfo currencyInfo = CurrencyInfoAttribute.GetCurrencyInfo<EPExpenseClaimDetails.curyInfoID>(cache, (object) row);
    nullable2 = CurrencyCollection.GetCurrency(currencyInfo?.BaseCuryID).RoundingLimit;
    Decimal num = Math.Abs(row.TaxRoundDiff.GetValueOrDefault());
    if (nullable2.GetValueOrDefault() < num & nullable2.HasValue)
      throw new PXException("The amount to be posted to the rounding account ({1} {0}) exceeds the limit ({2} {0}) specified on the Currencies (CM202000) form.", new object[3]
      {
        (object) ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo)?.Current?.BaseCuryID,
        (object) row.TaxRoundDiff,
        (object) PXDBQuantityAttribute.Round(CurrencyCollection.GetCurrency(currencyInfo.BaseCuryID).RoundingLimit)
      });
  }

  protected virtual void EPExpenseClaimDetails_EmployeeID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    EPExpenseClaimDetails row = e.Row as EPExpenseClaimDetails;
    cache.SetDefaultExt<EPExpenseClaimDetails.taxZoneID>((object) row);
    cache.SetDefaultExt<EPExpenseClaimDetails.branchID>((object) row);
    if (!((PXGraph) this).IsCopyPasteContext)
    {
      PX.Objects.CM.CurrencyInfo currencyInfo = CurrencyInfoAttribute.SetDefaults<EPExpenseClaim.curyInfoID>(cache, (object) row);
      if (currencyInfo != null)
        ((EPExpenseClaimDetails) e.Row).CuryID = currencyInfo.CuryID;
    }
    cache.SetDefaultExt<EPExpenseClaimDetails.corpCardID>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.inventoryID> e)
  {
    PXUIFieldAttribute.SetError<EPExpenseClaimDetails.uOM>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.inventoryID>>) e).Cache, (object) e.Row, (string) null);
    object obj = PXFormulaAttribute.Evaluate<EPExpenseClaimDetails.uOM>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.inventoryID>>) e).Cache, (object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.inventoryID>>) e).Cache.SetValueExt<EPExpenseClaimDetails.uOM>((object) e.Row, obj);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.inventoryID>>) e).Cache.SetDefaultExt<EPExpenseClaimDetails.costCodeID>((object) e.Row);
    if (!(PXSelectorAttribute.Select<PX.Objects.IN.InventoryItem.inventoryID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.inventoryID>>) e).Cache, (object) e.Row) is PX.Objects.IN.InventoryItem inventoryItem) || !((PXSelectBase<EPSetup>) this.epsetup).Current.AllowMixedTaxSettingInClaims.GetValueOrDefault() && ((PXSelectBase<EPExpenseClaimDetails>) this.CurrentClaimDetails).Current.RefNbr != null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.inventoryID>>) e).Cache.SetValueExt<EPExpenseClaimDetails.taxCalcMode>((object) e.Row, (object) inventoryItem.TaxCalcMode);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.taskID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.taskID>>) e).Cache.SetDefaultExt<EPExpenseClaimDetails.costCodeID>((object) e.Row);
  }

  protected virtual void EPExpenseClaimDetails_CustomerID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if ((e.Row is EPExpenseClaimDetails row ? (!row.CustomerID.HasValue ? 1 : 0) : 1) == 0)
      return;
    cache.SetValueExt<EPExpenseClaimDetails.customerLocationID>((object) row, (object) null);
  }

  protected virtual void EPExpenseClaimDetails_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is EPExpenseClaimDetails row))
      return;
    ExpenseClaimDetailGraphExtBase<ExpenseClaimDetailEntry>.DeleteLegacyTaxRows((PXGraph) this, row.RefNbr);
  }

  protected virtual void EPExpenseClaimDetails_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    EPExpenseClaimDetails row = e.Row as EPExpenseClaimDetails;
    EPExpenseClaimDetails oldRow = e.OldRow as EPExpenseClaimDetails;
    if (row == null || oldRow == null)
      return;
    if (row.RefNbr != oldRow.RefNbr || row.TaxCategoryID != oldRow.TaxCategoryID || row.TaxCalcMode != oldRow.TaxCalcMode || row.TaxZoneID != oldRow.TaxZoneID)
      ExpenseClaimDetailGraphExtBase<ExpenseClaimDetailEntry>.DeleteLegacyTaxRows((PXGraph) this, row.RefNbr);
    if (!e.ExternalCall || ((PXGraph) this).IsMobile || !(row.TaxZoneID != oldRow.TaxZoneID) || string.IsNullOrEmpty(row.TaxZoneID))
      return;
    EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(((PXSelectBase<EPEmployee>) this.Employee).Select(Array.Empty<object>()));
    string str = epEmployee.ReceiptAndClaimTaxZoneID;
    if (string.IsNullOrEmpty(str))
      str = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.locationID, Equal<Required<EPEmployee.defLocationID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) epEmployee.DefLocationID
      }))?.VTaxZoneID;
    if (!(row.TaxZoneID != str))
      return;
    ((PXSelectBase<EPEmployee>) this.Employee).Current = epEmployee;
    this.ReceiptEntryExt.AmtFieldUpdated(oldRow, row);
    if (((PXGraph) this).IsCopyPasteContext)
      return;
    ((PXSelectBase) this.TaxZoneUpdateAskView).View.AskExt();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.uOM> e)
  {
    if (e.Row == null)
      return;
    string oldValue = (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.uOM>, EPExpenseClaimDetails, object>) e).OldValue;
    Decimal valueOrDefault = e.Row.CuryUnitCost.GetValueOrDefault();
    if (string.IsNullOrEmpty(oldValue) || string.IsNullOrEmpty(e.Row.UOM) || !(oldValue != e.Row.UOM) || !(valueOrDefault != 0M))
      return;
    Decimal num1 = INUnitAttribute.ConvertFromBase<EPExpenseClaimDetails.inventoryID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.uOM>>) e).Cache, (object) e.Row, oldValue, valueOrDefault, INPrecision.NOROUND);
    Decimal num2 = INUnitAttribute.ConvertToBase<EPExpenseClaimDetails.inventoryID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.uOM>>) e).Cache, (object) e.Row, e.Row.UOM, num1, INPrecision.UNITCOST);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.uOM>>) e).Cache.SetValueExt<EPExpenseClaimDetails.curyUnitCost>((object) e.Row, (object) num2);
  }

  protected virtual void EPTaxTran_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    EPTaxTran row = (EPTaxTran) e.Row;
    EPExpenseClaimDetails current = ((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Current;
    if (row == null || current == null || e.Operation == 3)
      return;
    Decimal? nullable = row.CuryTaxAmt;
    Decimal num1 = 0M;
    if (nullable.GetValueOrDefault() > num1 & nullable.HasValue)
    {
      nullable = current.CuryExtCost;
      Decimal num2 = 0M;
      if (nullable.GetValueOrDefault() < num2 & nullable.HasValue)
        goto label_5;
    }
    nullable = row.CuryTaxAmt;
    Decimal num3 = 0M;
    if (nullable.GetValueOrDefault() < num3 & nullable.HasValue)
    {
      nullable = current.CuryExtCost;
      Decimal num4 = 0M;
      if (!(nullable.GetValueOrDefault() > num4 & nullable.HasValue))
        goto label_6;
    }
    else
      goto label_6;
label_5:
    cache.RaiseExceptionHandling<EPTaxTran.curyTaxAmt>((object) row, (object) row.CuryTaxAmt, (Exception) new PXSetPropertyException("The tax amount must have the same sign as the total amount."));
label_6:
    nullable = row.CuryTaxableAmt;
    Decimal num5 = 0M;
    if (nullable.GetValueOrDefault() > num5 & nullable.HasValue)
    {
      nullable = current.CuryExtCost;
      Decimal num6 = 0M;
      if (nullable.GetValueOrDefault() < num6 & nullable.HasValue)
        goto label_10;
    }
    nullable = row.CuryTaxableAmt;
    Decimal num7 = 0M;
    if (!(nullable.GetValueOrDefault() < num7 & nullable.HasValue))
      return;
    nullable = current.CuryExtCost;
    Decimal num8 = 0M;
    if (!(nullable.GetValueOrDefault() > num8 & nullable.HasValue))
      return;
label_10:
    cache.RaiseExceptionHandling<EPTaxTran.curyTaxableAmt>((object) row, (object) row.CuryTaxableAmt, (Exception) new PXSetPropertyException("The taxable amount must have the same sign as the total amount."));
  }

  protected virtual void CurrencyInfo_CuryID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee>.Config>.Search<EPEmployee.bAccountID>((PXGraph) this, (object) (((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Current != null ? ((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Current.EmployeeID : new int?()), Array.Empty<object>()));
    if (epEmployee != null && epEmployee.CuryID != null)
    {
      e.NewValue = (object) epEmployee.CuryID;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      if (((PXSelectBase<Company>) this.comapny).Current == null)
        return;
      e.NewValue = (object) ((PXSelectBase<Company>) this.comapny).Current.BaseCuryID;
    }
  }

  protected virtual void CurrencyInfo_CuryRateTypeID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Current == null)
      return;
    int? nullable1 = ((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Current.EmployeeID;
    if (!nullable1.HasValue)
      return;
    int? nullable2;
    if (((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Current == null)
    {
      nullable1 = new int?();
      nullable2 = nullable1;
    }
    else
      nullable2 = ((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Current.EmployeeID;
    EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee>.Config>.Search<EPEmployee.bAccountID>((PXGraph) this, (object) nullable2, Array.Empty<object>()));
    if (epEmployee == null || epEmployee.CuryRateTypeID == null)
      return;
    e.NewValue = (object) epEmployee.CuryRateTypeID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_CuryEffDate_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Current == null)
      return;
    e.NewValue = (object) ((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Current.ExpenseDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CM.CurrencyInfo row) || ((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Current == null)
      return;
    bool flag = row.AllowUpdate(((PXSelectBase) this.ClaimDetails).Cache) && ((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Current.EmployeeID.HasValue;
    if (flag)
    {
      CurrencyList currencyList = (CurrencyList) PXSelectorAttribute.Select<PX.Objects.CM.CurrencyInfo.curyID>(cache, (object) row);
      if (currencyList != null)
      {
        bool? nullable = currencyList.IsFinancial;
        if (nullable.GetValueOrDefault())
        {
          EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<EPExpenseClaimDetails.employeeID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
          {
            (object) ((PXSelectBase<EPExpenseClaimDetails>) this.ClaimDetails).Current
          }, Array.Empty<object>()));
          int num;
          if (epEmployee != null)
          {
            nullable = epEmployee.AllowOverrideRate;
            num = nullable.GetValueOrDefault() ? 1 : 0;
          }
          else
            num = 0;
          flag = num != 0;
        }
      }
    }
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyRateTypeID>(cache, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyEffDate>(cache, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.sampleCuryRate>(cache, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.sampleRecipRate>(cache, (object) row, flag);
  }

  public virtual void ValidateProjectAndProjectTask(EPExpenseClaimDetails info)
  {
    if (info == null)
      return;
    string error1 = PXUIFieldAttribute.GetError<EPExpenseClaimDetails.contractID>(((PXSelectBase) this.ClaimDetails).Cache, (object) info);
    if (!string.IsNullOrEmpty(error1) && error1.Equals(PXLocalizer.Localize("The project is expired.")))
      PXUIFieldAttribute.SetError<EPExpenseClaimDetails.contractID>(((PXSelectBase) this.ClaimDetails).Cache, (object) info, (string) null);
    int? nullable1 = info.ContractID;
    DateTime? nullable2;
    DateTime? nullable3;
    if (nullable1.HasValue)
    {
      PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Required<EPExpenseClaimDetails.contractID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) info.ContractID
      }));
      if (pmProject != null)
      {
        nullable2 = pmProject.ExpireDate;
        if (nullable2.HasValue)
        {
          nullable2 = info.ExpenseDate;
          if (nullable2.HasValue)
          {
            nullable2 = info.ExpenseDate;
            nullable3 = pmProject.ExpireDate;
            if ((nullable2.HasValue & nullable3.HasValue ? (nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              ((PXSelectBase) this.ClaimDetails).Cache.RaiseExceptionHandling<EPExpenseClaimDetails.contractID>((object) info, (object) info.ContractID, (Exception) new PXSetPropertyException("The project is expired.", (PXErrorLevel) 2));
          }
        }
      }
    }
    string error2 = PXUIFieldAttribute.GetError<EPExpenseClaimDetails.taskID>(((PXSelectBase) this.ClaimDetails).Cache, (object) info);
    if (!string.IsNullOrEmpty(error2) && (error2.Equals(PXLocalizer.Localize("The project task is expired.")) || error2.Equals(PXLocalizer.Localize("The project task is completed."))))
      PXUIFieldAttribute.SetError<EPExpenseClaimDetails.taskID>(((PXSelectBase) this.ClaimDetails).Cache, (object) info, (string) null);
    nullable1 = info.TaskID;
    if (!nullable1.HasValue)
      return;
    PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, info.ContractID, info.TaskID);
    if (dirty == null)
      return;
    nullable3 = dirty.EndDate;
    if (!nullable3.HasValue)
      return;
    nullable3 = info.ExpenseDate;
    if (!nullable3.HasValue)
      return;
    nullable3 = info.ExpenseDate;
    nullable2 = dirty.EndDate;
    if ((nullable3.HasValue & nullable2.HasValue ? (nullable3.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0 && dirty.Status != "F")
    {
      ((PXSelectBase) this.ClaimDetails).Cache.RaiseExceptionHandling<EPExpenseClaimDetails.taskID>((object) info, (object) info.TaskID, (Exception) new PXSetPropertyException("The project task is expired.", (PXErrorLevel) 2));
    }
    else
    {
      if (!(dirty.Status == "F"))
        return;
      ((PXSelectBase) this.ClaimDetails).Cache.RaiseExceptionHandling<EPExpenseClaimDetails.taskID>((object) info, (object) info.TaskID, (Exception) new PXSetPropertyException("The project task is completed.", (PXErrorLevel) 2));
    }
  }

  [PXMergeAttributes]
  [PXDBTimestamp]
  protected virtual void _(PX.Data.Events.CacheAttached<EPExpenseClaim.Tstamp> e)
  {
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (CurrencyList.curyID))]
  [PX.Objects.CM.CurrencyInfo.CuryID]
  protected virtual void CurrencyInfo_CuryId_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes]
  [EPTax]
  protected virtual void EPExpenseClaimDetails_TaxCategoryID_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes]
  [Branch(typeof (Search2<PX.Objects.GL.Branch.branchID, InnerJoin<EPEmployee, On<PX.Objects.GL.Branch.bAccountID, Equal<EPEmployee.parentBAccountID>>>, Where<EPEmployee.bAccountID, Equal<Current<EPExpenseClaimDetails.employeeID>>>>), null, true, true, true)]
  protected virtual void EPExpenseClaimDetails_BranchID_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Amount in Card Currency", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<EPExpenseClaimDetails.claimCuryTranAmtWithTaxes> e)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Corporate Card")]
  [PXRestrictor(typeof (Where<CACorpCard.isActive, Equal<True>>), "The corporate card is inactive.", new System.Type[] {})]
  [PXSelector(typeof (Search2<CACorpCard.corpCardID, InnerJoin<EPEmployeeCorpCardLink, On<EPEmployeeCorpCardLink.employeeID, Equal<Current2<EPExpenseClaimDetails.employeeID>>, And<EPEmployeeCorpCardLink.corpCardID, Equal<CACorpCard.corpCardID>>>, InnerJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<CACorpCard.cashAccountID>>, InnerJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<PX.Objects.CA.CashAccount.accountID>>>>>>), new System.Type[] {typeof (CACorpCard.corpCardCD), typeof (CACorpCard.name), typeof (CACorpCard.cardNumber), typeof (PX.Objects.GL.Account.curyID)}, SubstituteKey = typeof (CACorpCard.corpCardCD), DescriptionField = typeof (CACorpCard.name))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<EPExpenseClaimDetails.corpCardID> e)
  {
  }

  [PXDBInt]
  [PXDefault(typeof (Search<EPEmployee.bAccountID, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>))]
  [PXSubordinateAndWingmenSelector(typeof (EPDelegationOf.expenses))]
  [PXUIField]
  [PXFormula(typeof (Switch<Case<Where<Selector<EPExpenseClaimDetails.refNbr, EPExpenseClaim.employeeID>, IsNotNull, And<Current2<EPExpenseClaimDetails.employeeID>, IsNull>>, Selector<EPExpenseClaimDetails.refNbr, EPExpenseClaim.employeeID>>, EPExpenseClaimDetails.employeeID>))]
  [PXUIEnabled(typeof (Where<EPExpenseClaimDetails.refNbr, IsNull>))]
  protected virtual void EPExpenseClaimDetails_employeeID_CacheAttached(PXCache cache)
  {
  }

  [CurrencyInfo(ModuleCode = "EP", CuryIDField = "curyID", CuryDisplayName = "Currency")]
  [PXDBLong]
  protected virtual void EPExpenseClaimDetails_CuryInfoID_CacheAttached(PXCache cache)
  {
  }

  protected virtual void EPExpenseClaimDetails_CuryID_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    EPExpenseClaim epExpenseClaim = ((PXSelectBase<EPExpenseClaim>) this.CurrentClaim).SelectSingle(Array.Empty<object>());
    EPExpenseClaimDetails row = e.Row as EPExpenseClaimDetails;
    if (epExpenseClaim == null || row == null)
      return;
    long? curyInfoId = epExpenseClaim.CuryInfoID;
    long? nullable1 = row.CuryInfoID;
    if (!(curyInfoId.GetValueOrDefault() == nullable1.GetValueOrDefault() & curyInfoId.HasValue == nullable1.HasValue))
      return;
    EPExpenseClaimDetails expenseClaimDetails = row;
    nullable1 = new long?();
    long? nullable2 = nullable1;
    expenseClaimDetails.CuryInfoID = nullable2;
    ((PXSelectBase) this.ClaimDetails).Cache.Update((object) row);
  }

  [PXDBLong]
  protected virtual void EPExpenseClaimDetails_ClaimCuryInfoID_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  [PXSelector(typeof (Search2<EPExpenseClaim.refNbr, LeftJoin<EPTaxAggregate, On<EPTaxAggregate.refNbr, Equal<EPExpenseClaim.refNbr>>>, Where2<Where<Current<EPExpenseClaimDetails.holdClaim>, Equal<False>, Or<EPExpenseClaim.hold, Equal<True>, And2<Where<EPExpenseClaim.employeeID, Equal<Current2<EPExpenseClaimDetails.employeeID>>, Or<Current2<EPExpenseClaimDetails.employeeID>, IsNull>>, And<Where<Current<EPExpenseClaimDetails.rejected>, Equal<False>>>>>>, And2<Where2<Where<EPExpenseClaim.taxZoneID, Equal<Current2<EPExpenseClaimDetails.taxZoneID>>, Or<Where<EPExpenseClaim.taxZoneID, IsNull, And<Current2<EPExpenseClaimDetails.taxZoneID>, IsNull>>>>, And<EPExpenseClaim.taxCalcMode, Equal<Current2<EPExpenseClaimDetails.taxCalcMode>>, Or<Current<EPSetup.allowMixedTaxSettingInClaims>, Equal<True>>>>, And<EPTaxAggregate.refNbr, IsNull>>>>), new System.Type[] {typeof (EPExpenseClaim.refNbr), typeof (EPExpenseClaim.employeeID), typeof (EPExpenseClaim.locationID), typeof (EPExpenseClaim.docDate), typeof (EPExpenseClaim.docDesc), typeof (EPExpenseClaim.curyID), typeof (EPExpenseClaim.curyDocBal)}, DescriptionField = typeof (EPExpenseClaim.docDesc))]
  protected virtual void EPExpenseClaimDetails_RefNbr_CacheAttached(PXCache cache)
  {
  }

  protected virtual void EPExpenseClaimDetails_CuryTipAmt_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    EPExpenseClaimDetails row = e.Row as EPExpenseClaimDetails;
    Decimal? newValue = e.NewValue as Decimal?;
    if (row == null || !newValue.HasValue)
      return;
    Decimal? nullable = newValue;
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return;
    if (PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<EPSetup>) this.epsetup).Current.NonTaxableTipItem
    })) == null)
      throw new PXSetPropertyException("To be able to specify a nonzero tip amount in the expense receipt, specify an appropriate tip item in the Non-Taxable Tip Item box on the Time & Expenses Preferences (EP101000) form.");
  }

  protected virtual void EPExpenseClaimDetails_CuryTipAmt_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is EPExpenseClaimDetails row))
      return;
    Decimal? curyTipAmt = row.CuryTipAmt;
    Decimal num = 0M;
    if (!(curyTipAmt.GetValueOrDefault() == num & curyTipAmt.HasValue))
      ((PXSelectBase<EPExpenseClaimDetails>) this.CurrentClaimDetails).SetValueExt<EPExpenseClaimDetails.taxTipCategoryID>(row, (object) (PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<EPSetup>) this.epsetup).Current.NonTaxableTipItem
      })) ?? throw new PXSetPropertyException<EPExpenseClaimDetails.curyTipAmt>("To be able to specify a nonzero tip amount in the expense receipt, specify an appropriate tip item in the Non-Taxable Tip Item box on the Time & Expenses Preferences (EP101000) form.")).TaxCategoryID);
    else
      ((PXSelectBase<EPExpenseClaimDetails>) this.CurrentClaimDetails).SetValueExt<EPExpenseClaimDetails.taxTipCategoryID>(row, (object) null);
  }

  protected virtual void EPExpenseClaimDetails_TaxCategoryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is EPExpenseClaimDetails row))
      return;
    row.CuryTaxableAmtFromTax = new Decimal?(0M);
    row.TaxableAmtFromTax = new Decimal?(0M);
    row.CuryTaxAmt = new Decimal?(0M);
    row.TaxAmt = new Decimal?(0M);
  }

  [PXDefault(typeof (EPExpenseClaimDetails.expenseDate))]
  [PXMergeAttributes]
  protected virtual void EPApproval_DocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (EPExpenseClaimDetails.employeeID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_BAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (FbqlSelect<SelectFromBase<PX.Objects.CR.Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<EPEmployee>.On<EPEmployee.FK.ContactInfo>>>.Where<BqlOperand<EPEmployee.bAccountID, IBqlInt>.IsEqual<BqlField<EPExpenseClaimDetails.employeeID, IBqlInt>.FromCurrent>>, PX.Objects.CR.Contact>.SearchFor<PX.Objects.CR.Contact.contactID>))]
  [PXMergeAttributes]
  protected virtual void EPApproval_DocumentOwnerID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (EPExpenseClaimDetails.tranDesc))]
  [PXMergeAttributes]
  protected virtual void EPApproval_Descr_CacheAttached(PXCache sender)
  {
  }

  [CurrencyInfo(typeof (EPExpenseClaimDetails.curyInfoID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (EPExpenseClaimDetails.curyTranAmt))]
  [PXMergeAttributes]
  protected virtual void EPApproval_CuryTotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (EPExpenseClaimDetails.tranAmt))]
  [PXMergeAttributes]
  protected virtual void EPApproval_TotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXInt]
  [PXDBScalar(typeof (Search<EPSetup.claimDetailsAssignmentMapID>))]
  protected virtual void EPSetup_AssignmentMapID_CacheAttached(PXCache cache)
  {
  }

  [PXInt]
  [PXDBScalar(typeof (Search<EPSetup.claimDetailsAssignmentNotificationID>))]
  protected virtual void EPSetup_AssignmentNotificationID_CacheAttached(PXCache cache)
  {
  }

  [PXBool]
  [PXDefault(true)]
  [PXFormula(typeof (IIf<FeatureInstalled<FeaturesSet.approvalWorkflow>, True, False>))]
  protected virtual void EPSetup_IsActive_CacheAttached(PXCache cache)
  {
  }

  public virtual string GetDefaultTaxZone(EPExpenseClaimDetails row)
  {
    string defaultTaxZone = (string) null;
    if (row == null || row == null || row.RefNbr == null)
      return defaultTaxZone;
    PXSetup<EPSetup> epsetup = this.epsetup;
    int num;
    if (epsetup == null)
    {
      num = 0;
    }
    else
    {
      bool? taxSettingInClaims = (bool?) ((PXSelectBase<EPSetup>) epsetup).Current?.AllowMixedTaxSettingInClaims;
      bool flag = false;
      num = taxSettingInClaims.GetValueOrDefault() == flag & taxSettingInClaims.HasValue ? 1 : 0;
    }
    if (num != 0)
      defaultTaxZone = ((PXSelectBase<EPExpenseClaim>) this.CurrentClaim).SelectSingle(new object[1]
      {
        (object) row.RefNbr
      }).TaxZoneID;
    return defaultTaxZone;
  }

  public class ExpenseClaimDetailEntryExt : PX.Objects.EP.ExpenseClaimDetailEntryExt<ExpenseClaimDetailEntry>
  {
    public override PXSelectBase<EPExpenseClaimDetails> Receipts
    {
      get => (PXSelectBase<EPExpenseClaimDetails>) this.Base.ClaimDetails;
    }

    public override PXSelectBase<EPExpenseClaim> Claim
    {
      get => (PXSelectBase<EPExpenseClaim>) this.Base.CurrentClaim;
    }

    public override PXSelectBase<PX.Objects.CM.CurrencyInfo> CurrencyInfo
    {
      get => (PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.Base.currencyinfo;
    }
  }

  public class ExpenseClaimDetailEntry_ActivityDetailsExt : 
    ActivityDetailsExt<ExpenseClaimDetailEntry, EPExpenseClaimDetails, EPExpenseClaimDetails.noteID>
  {
    public override System.Type GetBAccountIDCommand()
    {
      return typeof (Select<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<EPExpenseClaimDetails.employeeID>>>>);
    }

    public override System.Type GetEmailMessageTarget()
    {
      return typeof (Select<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Current<EPEmployee.defContactID>>>>);
    }
  }

  [PXHidden]
  [Serializable]
  public class TaxZoneUpdateAsk : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }

  [PXHidden]
  [Serializable]
  public class ApprovalSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXBool]
    public virtual bool? ApprovalEnabled { get; set; }

    public abstract class approvalEnabled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ExpenseClaimDetailEntry.ApprovalSetup.approvalEnabled>
    {
    }
  }
}
