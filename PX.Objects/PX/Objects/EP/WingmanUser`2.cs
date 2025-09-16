// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.WingmanUser`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.EP;

public class WingmanUser<UserID, DelegationOf> : IBqlComparison, IBqlCreator, IBqlVerifier
  where UserID : 
  #nullable disable
  IBqlOperand, new()
  where DelegationOf : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DelegationOf>, new()
{
  private IBqlCreator _operand;
  private const string EMPLOYEERALIAS = "EMPLOYEERALIAS";

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    result = new bool?();
    value = (object) null;
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag1 = true;
    SQLExpression sqlExpression = (SQLExpression) null;
    bool flag2 = flag1 & BqlCommand.AppendExpression<UserID>(ref sqlExpression, graph, info, selection, ref this._operand);
    if (graph == null || !info.BuildExpression)
      return flag2;
    SimpleTable simpleTable = (SimpleTable) new SimpleTable<EPEmployee>("EMPLOYEERALIAS");
    DelegationOf delegationOf = new DelegationOf();
    exp = exp.In(new Query().Select<EPWingman.employeeID>().From<EPWingman>().InnerJoin((Table) simpleTable).On(SQLExpressionExt.EQ((SQLExpression) new Column<EPEmployee.bAccountID>((Table) simpleTable), (SQLExpression) new Column(typeof (EPWingman.wingmanID), (Table) null)).And(SQLExpressionExt.EQ((SQLExpression) new Column<EPEmployee.userID>((Table) simpleTable), sqlExpression))).Where(SQLExpressionExt.EQ((SQLExpression) new Column<EPWingman.isActive>((Table) null), (SQLExpression) new SQLConst((object) 1)).And(SQLExpressionExt.EQ((SQLExpression) new Column<EPWingman.delegationOf>((Table) null), (SQLExpression) new SQLConst((object) ((BqlConstant<DelegationOf, IBqlString, string>) (object) delegationOf).Value)))));
    return flag2;
  }
}
