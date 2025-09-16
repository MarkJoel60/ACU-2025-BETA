// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CashTransferEntryMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CA;

public class CashTransferEntryMultipleBaseCurrencies : PXGraphExtension<CashTransferEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  protected virtual void _(
    Events.FieldVerifying<CATransfer, CATransfer.outAccountID> e)
  {
    if (((Events.FieldVerifyingBase<Events.FieldVerifying<CATransfer, CATransfer.outAccountID>, CATransfer, object>) e).NewValue == null)
      return;
    CashAccount cashAccount1 = PXSelectorAttribute.Select<CATransfer.inAccountID>(((Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<CATransfer, CATransfer.outAccountID>>) e).Cache, (object) e.Row) as CashAccount;
    CashAccount cashAccount2 = PXSelectorAttribute.Select<CATransfer.outAccountID>(((Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<CATransfer, CATransfer.outAccountID>>) e).Cache, (object) e.Row, ((Events.FieldVerifyingBase<Events.FieldVerifying<CATransfer, CATransfer.outAccountID>, CATransfer, object>) e).NewValue) as CashAccount;
    if (e.Row.InAccountID.HasValue && cashAccount1.BaseCuryID != cashAccount2.BaseCuryID)
    {
      ((Events.FieldVerifyingBase<Events.FieldVerifying<CATransfer, CATransfer.outAccountID>, CATransfer, object>) e).NewValue = (object) ((CashAccount) PXSelectorAttribute.Select<CATransfer.outAccountID>(((Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<CATransfer, CATransfer.outAccountID>>) e).Cache, (object) e.Row, ((Events.FieldVerifyingBase<Events.FieldVerifying<CATransfer, CATransfer.outAccountID>, CATransfer, object>) e).NewValue)).CashAccountCD;
      throw new PXSetPropertyException("The base currency of the {0} branch associated with the {1} cash account differs from the base currency of the {2} branch associated with the {3} cash account.", new object[4]
      {
        (object) PXAccess.GetBranchCD(cashAccount1.BranchID),
        (object) cashAccount1.CashAccountCD,
        (object) PXAccess.GetBranchCD(cashAccount2.BranchID),
        (object) cashAccount2.CashAccountCD
      });
    }
  }

  protected virtual void _(
    Events.FieldVerifying<CATransfer, CATransfer.inAccountID> e)
  {
    if (((Events.FieldVerifyingBase<Events.FieldVerifying<CATransfer, CATransfer.inAccountID>, CATransfer, object>) e).NewValue == null)
      return;
    CashAccount cashAccount1 = PXSelectorAttribute.Select<CATransfer.inAccountID>(((Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<CATransfer, CATransfer.inAccountID>>) e).Cache, (object) e.Row, ((Events.FieldVerifyingBase<Events.FieldVerifying<CATransfer, CATransfer.inAccountID>, CATransfer, object>) e).NewValue) as CashAccount;
    CashAccount cashAccount2 = PXSelectorAttribute.Select<CATransfer.outAccountID>(((Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<CATransfer, CATransfer.inAccountID>>) e).Cache, (object) e.Row) as CashAccount;
    if (e.Row.OutAccountID.HasValue && cashAccount1.BaseCuryID != cashAccount2.BaseCuryID)
    {
      ((Events.FieldVerifyingBase<Events.FieldVerifying<CATransfer, CATransfer.inAccountID>, CATransfer, object>) e).NewValue = (object) ((CashAccount) PXSelectorAttribute.Select<CATransfer.outAccountID>(((Events.Event<PXFieldVerifyingEventArgs, Events.FieldVerifying<CATransfer, CATransfer.inAccountID>>) e).Cache, (object) e.Row, ((Events.FieldVerifyingBase<Events.FieldVerifying<CATransfer, CATransfer.inAccountID>, CATransfer, object>) e).NewValue)).CashAccountCD;
      throw new PXSetPropertyException("The base currency of the {0} branch associated with the {1} cash account differs from the base currency of the {2} branch associated with the {3} cash account.", new object[4]
      {
        (object) PXAccess.GetBranchCD(cashAccount1.BranchID),
        (object) cashAccount1.CashAccountCD,
        (object) PXAccess.GetBranchCD(cashAccount2.BranchID),
        (object) cashAccount2.CashAccountCD
      });
    }
  }

  protected virtual void VerifyBaseCuryWithExpenseAccounts()
  {
    foreach (PXResult<CAExpense> pxResult in ((PXSelectBase<CAExpense>) this.Base.Expenses).Select(Array.Empty<object>()))
    {
      CAExpense caExpense = PXResult<CAExpense>.op_Implicit(pxResult);
      CashAccount cashAccount1 = PXSelectorAttribute.Select<CAExpense.cashAccountID>(((PXSelectBase) this.Base.Expenses).Cache, (object) caExpense) as CashAccount;
      CashAccount cashAccount2 = PXSelectorAttribute.Select<CATransfer.outAccountID>(((PXSelectBase) this.Base.Transfer).Cache, (object) ((PXSelectBase<CATransfer>) this.Base.Transfer).Current) as CashAccount;
      if (string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<CAExpense.cashAccountID>(((PXSelectBase) this.Base.Expenses).Cache, (object) caExpense)))
      {
        if (cashAccount1 != null && cashAccount2 != null && cashAccount1.BaseCuryID != cashAccount2.BaseCuryID)
          ((PXSelectBase) this.Base.Expenses).Cache.RaiseExceptionHandling<CAExpense.cashAccountID>((object) caExpense, (object) cashAccount1.CashAccountCD, (Exception) new PXSetPropertyException("The base currency of the {0} branch associated with the {1} cash account differs from the base currency of the {2} branch associated with the {3} cash account.", (PXErrorLevel) 4, new object[4]
          {
            (object) PXAccess.GetBranchCD(cashAccount1.BranchID),
            (object) cashAccount1.CashAccountCD,
            (object) PXAccess.GetBranchCD(cashAccount2.BranchID),
            (object) cashAccount2.CashAccountCD
          }));
        else
          ((PXSelectBase) this.Base.Expenses).Cache.RaiseExceptionHandling<CAExpense.cashAccountID>((object) caExpense, (object) cashAccount1.CashAccountCD, (Exception) null);
      }
    }
  }

  protected virtual void _(Events.RowPersisting<CATransfer> e)
  {
    this.VerifyBaseCuryWithExpenseAccounts();
  }

  protected virtual void CATransfer_OutAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.VerifyBaseCuryWithExpenseAccounts();
  }
}
