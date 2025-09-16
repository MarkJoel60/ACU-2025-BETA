// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.TransferCashAccountAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Extensions;
using PX.Objects.GL;
using System;

#nullable disable
namespace PX.Objects.CA;

public class TransferCashAccountAttribute : CashAccountAttribute, IPXFieldUpdatedSubscriber
{
  public Type PairCashAccount { get; set; }

  public void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    int? cashAccountID = (int?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
    int? nullable1 = (int?) sender.GetValue(e.Row, this.PairCashAccount.Name);
    if (nullable1.HasValue)
    {
      int? nullable2 = nullable1;
      int? nullable3 = cashAccountID;
      if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
      {
        string cashAccountCd = TransferCashAccountAttribute.GetCashAccount(sender.Graph, cashAccountID).CashAccountCD;
        PXSetPropertyException propertyException = new PXSetPropertyException("The destination cash account must be different from the source cash account.");
        sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, (object) cashAccountCd, (Exception) propertyException);
      }
    }
    this.VerifyAccount(sender, e);
    this.VerifySubaccount(sender, e);
  }

  private void VerifyAccount(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    int? cashAccountID = (int?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
    if (!cashAccountID.HasValue)
      return;
    CashAccount cashAccount = TransferCashAccountAttribute.GetCashAccount(sender.Graph, cashAccountID);
    PX.Objects.GL.Account account = TransferCashAccountAttribute.GetAccount(sender.Graph, cashAccount.AccountID);
    if (account == null)
      return;
    bool? active = account.Active;
    bool flag = false;
    if (!(active.GetValueOrDefault() == flag & active.HasValue))
      return;
    PXSetPropertyException propertyException = new PXSetPropertyException("The document cannot be released because the {0} cash account uses the inactive {1} GL account. To release the document with this cash account, activate the GL account on the Chart of Accounts (GL202500) form.", (PXErrorLevel) 4, new object[2]
    {
      (object) cashAccount.CashAccountCD,
      (object) account.AccountCD
    });
    sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, (object) cashAccount.CashAccountCD, (Exception) propertyException);
  }

  private void VerifySubaccount(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    int? cashAccountID = (int?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName);
    if (!cashAccountID.HasValue)
      return;
    CashAccount cashAccount = TransferCashAccountAttribute.GetCashAccount(sender.Graph, cashAccountID);
    Sub sub = TransferCashAccountAttribute.GetSub(sender.Graph, cashAccount.SubID);
    if (sub == null)
      return;
    bool? active = sub.Active;
    bool flag = false;
    if (!(active.GetValueOrDefault() == flag & active.HasValue))
      return;
    PXSetPropertyException propertyException = new PXSetPropertyException("The {0} subaccount used with this cash account is inactive. To use this cash account, activate the subaccount on the Subaccounts (GL203000) form.", (PXErrorLevel) 4, new object[1]
    {
      (object) sender.Graph.Caches[typeof (Sub)].GetFormatedMaskField<Sub.subCD>((IBqlTable) sub)
    });
    sender.RaiseExceptionHandling(((PXEventSubscriberAttribute) this)._FieldName, e.Row, (object) cashAccount.CashAccountCD, (Exception) propertyException);
  }

  private static CashAccount GetCashAccount(PXGraph graph, int? cashAccountID)
  {
    return PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXViewOf<CashAccount>.BasedOn<SelectFromBase<CashAccount, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CashAccount.cashAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(graph, new object[1]
    {
      (object) cashAccountID
    }));
  }

  private static PX.Objects.GL.Account GetAccount(PXGraph graph, int? accountID)
  {
    return PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXViewOf<PX.Objects.GL.Account>.BasedOn<SelectFromBase<PX.Objects.GL.Account, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.GL.Account.accountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(graph, new object[1]
    {
      (object) accountID
    }));
  }

  private static Sub GetSub(PXGraph graph, int? subID)
  {
    return PXResultset<Sub>.op_Implicit(PXSelectBase<Sub, PXViewOf<Sub>.BasedOn<SelectFromBase<Sub, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Sub.subID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(graph, new object[1]
    {
      (object) subID
    }));
  }
}
