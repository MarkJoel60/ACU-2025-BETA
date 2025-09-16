// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CACorpCardsMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.EP.DAC;

#nullable disable
namespace PX.Objects.CA;

public class CACorpCardsMaint : PXGraph<CACorpCardsMaint, CACorpCard>
{
  public PXSelect<CACorpCard> CreditCards;
  public PXSelect<EPEmployeeCorpCardLink, Where<EPEmployeeCorpCardLink.corpCardID, Equal<Current<CACorpCard.corpCardID>>>> EmployeeLinks;

  public static CashAccount GetCardCashAccount(PXGraph graph, int? corpCardID)
  {
    return PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelectJoin<CashAccount, InnerJoin<CACorpCard, On<CashAccount.cashAccountID, Equal<CACorpCard.cashAccountID>>>, Where<CACorpCard.corpCardID, Equal<Required<CACorpCard.corpCardID>>>>.Config>.Select(graph, new object[1]
    {
      (object) corpCardID
    }));
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (CACorpCard.corpCardID))]
  [PXCustomizeBaseAttribute(typeof (PXSelectorAttribute), "DirtyRead", true)]
  protected virtual void _(
    Events.CacheAttached<EPEmployeeCorpCardLink.corpCardID> e)
  {
  }

  [PXMergeAttributes]
  protected virtual void _(
    Events.FieldUpdated<CACorpCard, CACorpCard.branchID> e)
  {
    if (e.Row == null)
      return;
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<CACorpCard, CACorpCard.branchID>>) e).Cache.SetDefaultExt<CACorpCard.cashAccountID>((object) e.Row);
  }
}
