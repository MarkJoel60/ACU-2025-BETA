// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.EntryTypeMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CA;

public class EntryTypeMaint : PXGraph<EntryTypeMaint>
{
  [PXFilterable(new Type[] {})]
  public PXSelect<CAEntryType> EntryType;
  public PXSetup<PX.Objects.CA.CASetup> CASetup;
  public PXSave<CAEntryType> Save;
  public PXCancel<CAEntryType> Cancel;
  private bool moduleChanged;

  public EntryTypeMaint()
  {
    PX.Objects.CA.CASetup current = ((PXSelectBase<PX.Objects.CA.CASetup>) this.CASetup).Current;
  }

  protected virtual void CAEntryType_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    CAEntryType row = (CAEntryType) e.Row;
    if (string.IsNullOrEmpty(row.EntryTypeId))
    {
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      CAEntryType caEntryType = PXResultset<CAEntryType>.op_Implicit(PXSelectBase<CAEntryType, PXSelectReadonly<CAEntryType, Where<CAEntryType.entryTypeId, Equal<Optional<CAEntryType.entryTypeId>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) row.EntryTypeId
      }));
      if (caEntryType == null || caEntryType == row)
        return;
      cache.RaiseExceptionHandling<CAEntryType.entryTypeId>(e.Row, (object) row.EntryTypeId, (Exception) new PXException("Record with this ID already exists."));
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void CAEntryType_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    CAEntryType row = (CAEntryType) e.Row;
    if (row == null)
      return;
    if (row.Module == "CA")
    {
      PXUIFieldAttribute.SetEnabled<CAEntryType.referenceID>(((PXSelectBase) this.EntryType).Cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<CAEntryType.accountID>(sender, (object) row, true);
      PXUIFieldAttribute.SetEnabled<CAEntryType.subID>(sender, (object) row, true);
    }
    else
    {
      PXUIFieldAttribute.SetEnabled<CAEntryType.referenceID>(((PXSelectBase) this.EntryType).Cache, (object) row, true);
      PXUIFieldAttribute.SetEnabled<CAEntryType.accountID>(sender, (object) row, false);
      PXUIFieldAttribute.SetEnabled<CAEntryType.subID>(sender, (object) row, false);
    }
    PXUIFieldAttribute.SetEnabled<CAEntryType.useToReclassifyPayments>(sender, (object) row, row.Module == "CA");
    bool flag1 = sender.GetStatus(e.Row) == 2;
    PXUIFieldAttribute.SetEnabled<CAEntryType.entryTypeId>(sender, (object) row, flag1);
    bool flag2 = false;
    PX.Objects.GL.Account account = (PX.Objects.GL.Account) null;
    CashAccount cashAccount = (CashAccount) null;
    int? nullable1 = row.AccountID;
    if (nullable1.HasValue)
    {
      nullable1 = row.SubID;
      if (nullable1.HasValue)
      {
        cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelectReadonly<CashAccount, Where<CashAccount.accountID, Equal<Required<CashAccount.accountID>>, And<CashAccount.subID, Equal<Required<CashAccount.subID>>>>>.Config>.Select((PXGraph) this, new object[2]
        {
          (object) row.AccountID,
          (object) row.SubID
        }));
        account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelectReadonly<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.AccountID
        }));
        flag2 = cashAccount != null;
      }
    }
    PXCache pxCache1 = sender;
    CAEntryType caEntryType1 = row;
    bool? reclassifyPayments = row.UseToReclassifyPayments;
    int num1 = !reclassifyPayments.GetValueOrDefault() & flag2 ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CAEntryType.branchID>(pxCache1, (object) caEntryType1, num1 != 0);
    PXCache pxCache2 = sender;
    CAEntryType caEntryType2 = row;
    reclassifyPayments = row.UseToReclassifyPayments;
    int num2 = reclassifyPayments.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CAEntryType.cashAccountID>(pxCache2, (object) caEntryType2, num2 != 0);
    reclassifyPayments = row.UseToReclassifyPayments;
    if (reclassifyPayments.GetValueOrDefault() && account != null)
    {
      nullable1 = account.AccountID;
      if (nullable1.HasValue)
      {
        if (!flag2)
        {
          sender.RaiseExceptionHandling<CAEntryType.accountID>(e.Row, (object) account.AccountCD, (Exception) new PXSetPropertyException("Entry Type which is used for Payment Reclassification must have a Cash Account as Offset Account", (PXErrorLevel) 4));
          return;
        }
        PaymentMethodAccount paymentMethodAccount = PXResultset<PaymentMethodAccount>.op_Implicit(PXSelectBase<PaymentMethodAccount, PXSelectReadonly2<PaymentMethodAccount, InnerJoin<PaymentMethod, On<PaymentMethod.paymentMethodID, Equal<PaymentMethodAccount.paymentMethodID>, And<PaymentMethod.isActive, Equal<True>>>>, Where<PaymentMethodAccount.cashAccountID, Equal<Required<PaymentMethodAccount.cashAccountID>>, And<Where<PaymentMethodAccount.useForAP, Equal<True>, Or<PaymentMethodAccount.useForAR, Equal<True>>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.CashAccountID
        }));
        if (paymentMethodAccount != null)
        {
          nullable1 = paymentMethodAccount.CashAccountID;
          if (nullable1.HasValue)
            return;
        }
        sender.RaiseExceptionHandling<CAEntryType.cashAccountID>(e.Row, (object) cashAccount.CashAccountCD, (Exception) new PXSetPropertyException("This Cash Account is not configured for usage with any Payment Method. Please, check the configuration of the Payment Methods before using the Payments Reclassifications", (PXErrorLevel) 2));
        return;
      }
    }
    CAEntryType caEntryType3 = row;
    nullable1 = new int?();
    int? nullable2 = nullable1;
    caEntryType3.CashAccountID = nullable2;
  }

  protected virtual void CAEntryType_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    CAEntryType newRow = e.NewRow as CAEntryType;
    if (!newRow.AccountID.HasValue)
      return;
    if (!PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) newRow.AccountID
    })).IsCashAccount.GetValueOrDefault())
      return;
    if (PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.accountID, Equal<Required<CashAccount.accountID>>, And<CashAccount.subID, Equal<Required<CashAccount.subID>>, And<CashAccount.branchID, Equal<Required<CashAccount.branchID>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) newRow.AccountID,
      (object) newRow.SubID,
      (object) newRow.BranchID
    })) != null)
      return;
    string field1 = (string) PXSelectorAttribute.GetField(sender, (object) newRow, typeof (CAEntryType.branchID).Name, (object) newRow.BranchID, typeof (PX.Objects.GL.Branch.branchCD).Name);
    sender.RaiseExceptionHandling<CAEntryType.branchID>((object) newRow, (object) field1, (Exception) new PXSetPropertyException("There is no Cash Account matching these Branch and Subaccount", (PXErrorLevel) 4));
    string field2 = (string) PXSelectorAttribute.GetField(sender, (object) newRow, typeof (CAEntryType.subID).Name, (object) newRow.SubID, typeof (Sub.subCD).Name);
    sender.RaiseExceptionHandling<CAEntryType.subID>((object) newRow, (object) field2, (Exception) new PXSetPropertyException("There is no Cash Account matching these Branch and Subaccount", (PXErrorLevel) 4));
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CAEntryType_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    CAEntryType row = e.Row as CAEntryType;
    PXDefaultAttribute.SetPersistingCheck<CAEntryType.referenceID>(sender, e.Row, (PXPersistingCheck) 2);
    if ((e.Operation & 3) == 1)
    {
      if (PXResultset<CAAdj>.op_Implicit(PXSelectBase<CAAdj, PXSelectReadonly<CAAdj, Where<CAAdj.entryTypeID, Equal<Required<CAAdj.entryTypeID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.EntryTypeId
      })) != null)
      {
        CAEntryType caEntryType = PXResultset<CAEntryType>.op_Implicit(PXSelectBase<CAEntryType, PXSelectReadonly<CAEntryType, Where<CAEntryType.entryTypeId, Equal<Required<CAEntryType.entryTypeId>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.EntryTypeId
        }));
        if (caEntryType != null)
        {
          if (row.DrCr != caEntryType.DrCr)
          {
            if (sender.RaiseExceptionHandling<CAEntryType.drCr>(e.Row, (object) row.DrCr, (Exception) new PXSetPropertyException("You cannot change the Type for the Entry Type if one or more transactions was entered already.", (PXErrorLevel) 5, new object[1]
            {
              (object) "[drCr]"
            })))
              throw new PXRowPersistingException(typeof (CAEntryType.drCr).Name, (object) row.DrCr, "You cannot change the Type for the Entry Type if one or more transactions was entered already.", new object[1]
              {
                (object) "drCr"
              });
          }
          if (row.Module != caEntryType.Module)
          {
            if (sender.RaiseExceptionHandling<CAEntryType.module>(e.Row, (object) row.Module, (Exception) new PXSetPropertyException("You cannot change the Module for the Entry Type if one or more transactions was entered already.", (PXErrorLevel) 5, new object[1]
            {
              (object) "[module]"
            })))
              throw new PXRowPersistingException(typeof (CAEntryType.module).Name, (object) row.Module, "You cannot change the Module for the Entry Type if one or more transactions was entered already.", new object[1]
              {
                (object) "module"
              });
          }
        }
      }
    }
    if ((e.Operation & 3) == 3)
      return;
    bool? nullable = row.UseToReclassifyPayments;
    if (!nullable.GetValueOrDefault())
      return;
    int? accountId = row.AccountID;
    if (!accountId.HasValue)
      return;
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelectReadonly<CashAccount, Where<CashAccount.accountID, Equal<Required<CashAccount.accountID>>, And<CashAccount.subID, Equal<Required<CashAccount.subID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.AccountID,
      (object) row.SubID
    }));
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelectReadonly<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.AccountID
    }));
    if (cashAccount != null)
    {
      accountId = cashAccount.AccountID;
      if (accountId.HasValue)
      {
        if (cashAccount == null)
          return;
        nullable = cashAccount.ClearingAccount;
        if (!nullable.GetValueOrDefault())
          return;
        if (!sender.RaiseExceptionHandling<CAEntryType.cashAccountID>(e.Row, (object) cashAccount.CashAccountCD, (Exception) new PXSetPropertyException("The {0} cash account cannot be used as a reclassification account because it is a clearing account. Clear the Clearing Account check box on the Cash Accounts (CA202000) form for the {0} account or select another cash account.", (PXErrorLevel) 4, new object[1]
        {
          (object) cashAccount.CashAccountCD
        })))
          return;
        throw new PXRowPersistingException(typeof (CAEntryType.cashAccountID).Name, (object) cashAccount.CashAccountCD, "The {0} cash account cannot be used as a reclassification account because it is a clearing account. Clear the Clearing Account check box on the Cash Accounts (CA202000) form for the {0} account or select another cash account.", new object[1]
        {
          (object) cashAccount.CashAccountCD
        });
      }
    }
    if (sender.RaiseExceptionHandling<CAEntryType.accountID>(e.Row, (object) account.AccountCD, (Exception) new PXSetPropertyException("Entry Type which is used for Payment Reclassification must have a Cash Account as Offset Account", (PXErrorLevel) 4)))
      throw new PXRowPersistingException(typeof (CAEntryType.accountID).Name, (object) account.AccountCD, "Entry Type which is used for Payment Reclassification must have a Cash Account as Offset Account");
  }

  protected virtual void CAEntryType_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    try
    {
      PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (CABankTranRule.documentEntryTypeID), (Type) null, "The entry type cannot be deleted because it is used on the Bank Transaction Rules (CA204500) form.");
    }
    catch (PXException ex)
    {
      CAEntryType row = e.Row as CAEntryType;
      sender.RaiseExceptionHandling<CAEntryType.entryTypeId>(e.Row, (object) row.EntryTypeId, (Exception) new PXSetPropertyException(ex.MessageNoPrefix, (PXErrorLevel) 4));
      throw;
    }
  }

  protected virtual void CAEntryType_Module_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    this.moduleChanged = true;
    CAEntryType row = (CAEntryType) e.Row;
    if (row.Module == "CA")
    {
      row.ReferenceID = new int?();
    }
    else
    {
      row.AccountID = new int?();
      row.SubID = new int?();
      row.ReferenceID = new int?();
    }
    cache.SetDefaultExt<CAEntryType.useToReclassifyPayments>(e.Row);
  }

  protected virtual void CAEntryType_ReferenceID_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    if (!(((CAEntryType) e.Row).Module == "CA") && !this.moduleChanged)
      return;
    e.NewValue = (object) null;
  }

  protected virtual void CAEntryType_AccountID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CAEntryType row))
      return;
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.AccountID
    }));
    if (account != null && account.IsCashAccount.GetValueOrDefault())
    {
      CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.accountID, Equal<Required<CashAccount.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) account.AccountID
      }));
      cache.SetValueExt<CAEntryType.branchID>((object) row, (object) cashAccount.BranchID);
      cache.SetValueExt<CAEntryType.subID>((object) row, (object) cashAccount.SubID);
    }
    else
      cache.SetValueExt<CAEntryType.branchID>((object) row, (object) null);
  }

  protected virtual void CAEntryType_AccountID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    cache.RaiseExceptionHandling<CAEntryType.accountID>(e.Row, (object) null, (Exception) null);
  }

  protected virtual void CAEntryType_CashAccountID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CAEntryType row) || !row.CashAccountID.HasValue)
      return;
    cache.SetValuePending<CAEntryType.accountID>(e.Row, PXCache.NotSetValue);
    cache.SetDefaultExt<CAEntryType.accountID>(e.Row);
    cache.SetDefaultExt<CAEntryType.subID>(e.Row);
    cache.SetDefaultExt<CAEntryType.branchID>(e.Row);
  }

  protected virtual void CAEntryType_AccountID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    CAEntryType row = (CAEntryType) e.Row;
    if (row == null || !row.CashAccountID.HasValue)
      return;
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.CashAccountID
    }));
    if (cashAccount == null)
      return;
    e.NewValue = (object) cashAccount.AccountID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CAEntryType_SubID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    CAEntryType row = (CAEntryType) e.Row;
    if (row == null || !row.CashAccountID.HasValue)
      return;
    CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.CashAccountID
    }));
    if (cashAccount == null)
      return;
    e.NewValue = (object) cashAccount.SubID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CAEntryType_BranchID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    CAEntryType row = (CAEntryType) e.Row;
    if (row == null)
      return;
    if (row.CashAccountID.HasValue)
    {
      CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) row.CashAccountID
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

  protected virtual void CAEntryType_UseToReclassifyPayments_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    CAEntryType row = e.Row as CAEntryType;
    bool? reclassifyPayments = row.UseToReclassifyPayments;
    bool flag = false;
    if (reclassifyPayments.GetValueOrDefault() == flag & reclassifyPayments.HasValue)
    {
      cache.SetValueExt<CAEntryType.cashAccountID>(e.Row, (object) null);
    }
    else
    {
      CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.accountID, Equal<Required<CashAccount.accountID>>, And<CashAccount.subID, Equal<Required<CashAccount.subID>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
      {
        (object) row.AccountID,
        (object) row.SubID
      }));
      if (cashAccount == null)
        return;
      cache.SetValueExt<CAEntryType.cashAccountID>(e.Row, (object) cashAccount.CashAccountID);
    }
  }
}
