// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTransactionsProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA;

public class CABankTransactionsProcess : PXGraph<CABankTransactionsProcess>
{
  public PXAction<CashAccount> cancel;
  [PXFilterable(new Type[] {})]
  public PXProcessingJoin<CashAccount, LeftJoin<CABankTranByCashAccount, On<CashAccount.cashAccountID, Equal<CABankTranByCashAccount.cashAccountID>>>, Where2<Where<CashAccount.restrictVisibilityWithBranch, Equal<False>, Or<CashAccount.branchID, Equal<Current<AccessInfo.branchID>>>>, And<Where<CashAccount.baseCuryID, Equal<Current<AccessInfo.baseCuryID>>, Or<FeatureInstalled<FeaturesSet.multipleBaseCurrencies>>>>>, OrderBy<Asc<CashAccount.cashAccountCD>>> BankAccountSummary;
  public PXAction<CashAccount> ViewUnmatchedTrans;

  public CABankTransactionsProcess()
  {
    ((PXProcessing<CashAccount>) this.BankAccountSummary).SetProcessCaption("Auto-Match");
    ((PXProcessing<CashAccount>) this.BankAccountSummary).SetProcessAllCaption("Auto-Match All");
  }

  [PXUIField]
  [PXCancelButton]
  protected virtual IEnumerable Cancel(PXAdapter adapter)
  {
    ((PXSelectBase) this.BankAccountSummary).Cache.Clear();
    ((PXGraph) this).TimeStamp = (byte[]) null;
    PXLongOperation.ClearStatus(((PXGraph) this).UID);
    return adapter.Get();
  }

  public static void DoMatch(List<CashAccount> list, object processorID)
  {
    CABankMatchingProcess instance = PXGraph.CreateInstance<CABankMatchingProcess>();
    foreach (CashAccount cashAccount in list)
    {
      PXProcessing<CashAccount>.SetCurrentItem((object) cashAccount);
      IEnumerable<CABankTran> aRows = GraphHelper.RowCast<CABankTran>((IEnumerable) ((PXSelectBase<CABankTran>) instance.UnMatchedDetails).Select(new object[1]
      {
        (object) cashAccount.CashAccountID
      }));
      try
      {
        instance.DoMatch(aRows, cashAccount, (Guid?) processorID);
        ((PXGraph) instance).Persist();
        if (PXProcessing<CashAccount>.GetItemMessage() == null)
          PXProcessing<CashAccount>.SetProcessed();
      }
      catch (Exception ex)
      {
        PXProcessing<CashAccount>.SetError(ex);
      }
    }
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewUnmatchedTrans(PXAdapter adapter)
  {
    this.RedirectToMatchingOf((int?) ((PXSelectBase<CashAccount>) this.BankAccountSummary).Current?.CashAccountID);
    return adapter.Get();
  }

  private void RedirectToMatchingOf(int? cashAccountID)
  {
    if (cashAccountID.HasValue)
    {
      CABankTransactionsMaint instance = PXGraph.CreateInstance<CABankTransactionsMaint>();
      ((PXSelectBase) instance.TranMatch).Cache.Clear();
      ((PXSelectBase) instance.TranFilter).Cache.SetValueExt<CABankTransactionsMaint.Filter.cashAccountID>((object) ((PXSelectBase<CABankTransactionsMaint.Filter>) instance.TranFilter).Current, (object) cashAccountID);
      throw new PXRedirectRequiredException((PXGraph) instance, "Process Transactions");
    }
  }

  protected virtual void _(Events.RowSelected<CashAccount> e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<CashAccount>) this.BankAccountSummary).SetProcessDelegate(new PXProcessingBase<CashAccount>.ProcessListDelegate((object) new CABankTransactionsProcess.\u003C\u003Ec__DisplayClass8_0()
    {
      processorID = (Guid?) ((PXGraph) this).UID
    }, __methodptr(\u003C_\u003Eb__0)));
  }
}
