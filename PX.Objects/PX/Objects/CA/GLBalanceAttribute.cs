// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.GLBalanceAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.CA;

/// <summary>
/// This attribute allows to display a  CashAccount balance from GLHistory for <br />
/// the defined Fin. Period. If the fin date is provided, the period, containing <br />
/// the date will be selected (Fin. Period parameter will be ignored in this case)<br />
/// Balance corresponds to the CuryFinYtdBalance for the period <br />
/// Read-only. Should be placed on the field having type Decimal?<br />
/// <example>
/// [GLBalance(typeof(CATransfer.outAccountID), null, typeof(CATransfer.outDate))]
/// or
/// [GLBalance(typeof(PrintChecksFilter.payAccountID), typeof(PrintChecksFilter.payFinPeriodID))]
/// </example>
/// </summary>
public class GLBalanceAttribute : PXEventSubscriberAttribute, IPXFieldSelectingSubscriber
{
  protected string _CashAccount;
  protected string _FinDate;
  protected string _FinPeriodID;

  /// <summary>Ctor</summary>
  /// <param name="cashAccountType">Must be IBqlField type. Refers CashAccountID field of the row.</param>
  /// <param name="finPeriodID">Must be IBqlField type. Refers FinPeriodID field of the row.</param>
  public GLBalanceAttribute(Type cashAccountType, Type finPeriodID)
  {
    this._CashAccount = cashAccountType.Name;
    this._FinPeriodID = finPeriodID.Name;
  }

  /// <summary>Ctor</summary>
  /// <param name="cashAccountType">Must be IBqlField type. Refers CashAccountID field of the row.</param>
  /// <param name="finPeriodID">Not used.Value is ignored</param>
  /// <param name="finDateType">Must be IBqlField type. Refers FinDate field of the row.</param>
  public GLBalanceAttribute(Type cashAccountType, Type finPeriodID, Type finDateType)
  {
    this._CashAccount = cashAccountType.Name;
    this._FinDate = finDateType.Name;
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    object obj1 = sender.GetValue(e.Row, this._CashAccount);
    object obj2 = (object) null;
    if (string.IsNullOrEmpty(this._FinPeriodID))
    {
      CashAccount cashAccount = PXResultset<CashAccount>.op_Implicit(PXSelectBase<CashAccount, PXSelect<CashAccount, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>>>.Config>.Select(sender.Graph, new object[1]
      {
        obj1
      }));
      if (cashAccount != null)
        obj2 = (object) sender.Graph.GetService<IFinPeriodRepository>().FindFinPeriodByDate((DateTime?) sender.GetValue(e.Row, this._FinDate), PXAccess.GetParentOrganizationID(cashAccount.BranchID))?.FinPeriodID;
    }
    else
      obj2 = sender.GetValue(e.Row, this._FinPeriodID);
    if (obj1 != null && obj2 != null)
    {
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      e.ReturnValue = (object) ((IQueryable<PXResult<GLHistory>>) PXSelectBase<GLHistory, PXSelectJoin<GLHistory, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<GLHistory.branchID>, And<PX.Objects.GL.Branch.ledgerID, Equal<GLHistory.ledgerID>>>, LeftJoin<CashAccount, On<GLHistory.branchID, Equal<CashAccount.branchID>, And<GLHistory.accountID, Equal<CashAccount.accountID>, And<GLHistory.subID, Equal<CashAccount.subID>>>>, LeftJoin<PX.Objects.GL.Account, On<GLHistory.accountID, Equal<PX.Objects.GL.Account.accountID>, And<Match<PX.Objects.GL.Account, Current<AccessInfo.userName>>>>, LeftJoin<Sub, On<GLHistory.subID, Equal<Sub.subID>, And<Match<Sub, Current<AccessInfo.userName>>>>>>>>, Where<CashAccount.cashAccountID, Equal<Required<CashAccount.cashAccountID>>, And<GLHistory.finPeriodID, LessEqual<Required<GLHistory.finPeriodID>>>>>.Config>.Select(sender.Graph, new object[2]
      {
        obj1,
        obj2
      })).Select<PXResult<GLHistory>, GLHistory>(Expression.Lambda<Func<PXResult<GLHistory>, GLHistory>>((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression)).OrderByDescending<GLHistory, string>((Expression<Func<GLHistory, string>>) (_ => _.FinPeriodID)).Select<GLHistory, Decimal?>((Expression<Func<GLHistory, Decimal?>>) (_ => _.CuryTranYtdBalance)).FirstOrDefault<Decimal?>().GetValueOrDefault();
    }
    else
      e.ReturnValue = (object) 0M;
    ((CancelEventArgs) e).Cancel = true;
  }
}
