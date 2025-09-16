// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranRuleMaintPopup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections;

#nullable disable
namespace PX.Objects.CA;

public class CABankTranRuleMaintPopup : CABankTranRuleMaintBase
{
  public PXAction<CABankTranRule> saveAndApply;
  public PXAction<CABankTranRule> saveClose;

  [PXUIField]
  [PXButton]
  public virtual IEnumerable SaveAndApply(PXAdapter adapter)
  {
    ((PXGraph) this).Actions.PressSave();
    this.DoMatch();
    ((PXGraph) this).Actions.PressSave();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable SaveClose(PXAdapter adapter)
  {
    ((PXGraph) this).Actions.PressSave();
    return adapter.Get();
  }

  protected virtual void DoMatch()
  {
    CABankTranRule current = ((PXSelectBase<CABankTranRule>) this.Rule).Current;
    CABankTransactionsMaint instance = PXGraph.CreateInstance<CABankTransactionsMaint>();
    CABankTransactionsMaint.Filter filter = new CABankTransactionsMaint.Filter()
    {
      CashAccountID = current.BankTranCashAccountID,
      TranType = "S"
    };
    ((PXSelectBase<CABankTransactionsMaint.Filter>) instance.TranFilter).Current = filter;
    object obj;
    ((PXSelectBase) instance.TranFilter).Cache.RaiseFieldDefaulting<CABankTransactionsMaint.Filter.tranType>((object) filter, ref obj);
    ((PXAction) instance.cancel).Press();
    if (!current.IsActive.GetValueOrDefault())
      return;
    instance.ApplyRule(current);
    ((PXGraph) instance).Actions.PressSave();
  }

  protected override void CABankTranRule_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    base.CABankTranRule_RowSelected(cache, e);
    if (!(e.Row is CABankTranRule row))
      return;
    ((PXAction) this.saveAndApply).SetEnabled(row.IsActive.GetValueOrDefault());
  }
}
