// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranRuleMaintBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CA;

public class CABankTranRuleMaintBase : PXGraph<CABankTranRuleMaintBase>
{
  public PXSelect<CABankTranRule> Rule;

  protected virtual void CABankTranRule_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    CABankTranRule row = e.Row as CABankTranRule;
    if (string.IsNullOrWhiteSpace(row.BankTranDescription) && !row.CuryTranAmt.HasValue && string.IsNullOrWhiteSpace(row.TranCode) && string.IsNullOrWhiteSpace(row.PayeeName))
      throw new PXException("Tran. Code, Description, Payee/Payer or Amount Criteria must be specified for the rule.");
    PXDefaultAttribute.SetPersistingCheck<CABankTranRule.documentModule>(cache, (object) row, row.Action == "C" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<CABankTranRule.documentEntryTypeID>(cache, (object) row, row.Action == "C" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<CABankTranRule.curyTranAmt>(cache, (object) row, row.AmountMatchingMode == "E" ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<CABankTranRule.curyMinTranAmt>(cache, (object) row, row.AmountMatchingMode == "B" ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<CABankTranRule.maxCuryTranAmt>(cache, (object) row, row.AmountMatchingMode == "B" ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
    if (!row.BankTranCashAccountID.HasValue || row.DocumentEntryTypeID == null)
      return;
    if (PXResultset<CashAccountETDetail>.op_Implicit(PXSelectBase<CashAccountETDetail, PXSelect<CashAccountETDetail, Where<CashAccountETDetail.cashAccountID, Equal<Required<CashAccountETDetail.cashAccountID>>, And<CashAccountETDetail.entryTypeID, Equal<Required<CashAccountETDetail.entryTypeID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.BankTranCashAccountID,
      (object) row.DocumentEntryTypeID
    })) != null)
      return;
    cache.RaiseExceptionHandling<CABankTranRule.documentEntryTypeID>((object) row, (object) row.DocumentEntryTypeID, (Exception) new PXSetPropertyException<CABankTranRule.documentEntryTypeID>("Entry Type does not suit the selected Cash Account."));
  }

  protected virtual void CABankTranRule_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    if (PXResultset<CABankTran>.op_Implicit(PXSelectBase<CABankTran, PXSelect<CABankTran, Where<CABankTran.ruleID, Equal<Required<CABankTran.ruleID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) (e.Row as CABankTranRule).RuleID
    })) != null)
      throw new PXException("Cannot delete the Rule. There are Transactions associated with this Rule.");
  }

  protected virtual void CABankTranRule_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is CABankTranRule row))
      return;
    bool flag1 = row.Action == "C";
    PXUIFieldAttribute.SetRequired<CABankTranRule.documentModule>(cache, flag1);
    PXUIFieldAttribute.SetRequired<CABankTranRule.documentEntryTypeID>(cache, flag1);
    PXUIFieldAttribute.SetVisible<CABankTranRule.documentModule>(cache, (object) row, flag1);
    PXUIFieldAttribute.SetVisible<CABankTranRule.documentEntryTypeID>(cache, (object) row, flag1);
    bool flag2 = row.AmountMatchingMode == "B";
    bool flag3 = row.AmountMatchingMode != "N" && !flag2;
    PXUIFieldAttribute.SetVisible<CABankTranRule.curyTranAmt>(cache, (object) row, flag3);
    PXUIFieldAttribute.SetVisible<CABankTranRule.curyMinTranAmt>(cache, (object) row, flag2);
    PXUIFieldAttribute.SetVisible<CABankTranRule.maxCuryTranAmt>(cache, (object) row, flag2);
    PXUIFieldAttribute.SetRequired<CABankTranRule.curyTranAmt>(cache, flag3);
    PXUIFieldAttribute.SetRequired<CABankTranRule.curyMinTranAmt>(cache, flag2);
    PXUIFieldAttribute.SetRequired<CABankTranRule.maxCuryTranAmt>(cache, flag2);
    bool flag4 = !row.BankTranCashAccountID.HasValue;
    PXUIFieldAttribute.SetEnabled<CABankTranRule.tranCuryID>(cache, (object) row, flag4);
  }

  protected virtual void CABankTranRule_BankTranCashAccountID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CABankTranRule row))
      return;
    if (row.BankTranCashAccountID.HasValue)
    {
      CashAccount cashAccount = (CashAccount) PXSelectorAttribute.Select<CABankTranRule.bankTranCashAccountID>(cache, (object) row);
      cache.SetValueExt<CABankTranRule.tranCuryID>((object) row, (object) cashAccount.CuryID);
    }
    else
      cache.SetDefaultExt<CABankTranRule.tranCuryID>((object) row);
  }

  protected virtual void CABankTranRule_AmountMatchingMode_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CABankTranRule row))
      return;
    switch (row.AmountMatchingMode)
    {
      case "N":
        cache.SetValueExt<CABankTranRule.curyTranAmt>((object) row, (object) null);
        cache.SetValueExt<CABankTranRule.maxCuryTranAmt>((object) row, (object) null);
        break;
      case "E":
        cache.SetValueExt<CABankTranRule.maxCuryTranAmt>((object) row, (object) null);
        break;
      case "B":
        if (row.MaxCuryTranAmt.HasValue)
          break;
        cache.SetValueExt<CABankTranRule.maxCuryTranAmt>((object) row, (object) row.CuryTranAmt);
        break;
    }
  }

  protected virtual void CABankTranRule_Action_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    CABankTranRule row = e.Row as CABankTranRule;
    if (row.Action == "C")
    {
      cache.SetDefaultExt<CABankTranRule.documentModule>((object) row);
      cache.SetDefaultExt<CABankTranRule.documentEntryTypeID>((object) row);
    }
    else
    {
      row.DocumentModule = (string) null;
      row.DocumentEntryTypeID = (string) null;
    }
  }
}
