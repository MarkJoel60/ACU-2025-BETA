// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.Approver`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using PX.TM;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.EP;

public class Approver<ContactID> : IBqlComparison, IBqlCreator, IBqlVerifier where ContactID : IBqlOperand, new()
{
  private IBqlCreator _operand;

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
    SQLExpression sqlExpression1 = (SQLExpression) null;
    bool flag2 = flag1 & BqlCommand.AppendExpression<ContactID>(ref sqlExpression1, graph, info, selection, ref this._operand);
    SQLExpression sqlExpression2 = (SQLExpression) null;
    bool flag3 = flag2 & BqlCommand.AppendExpression<ContactID>(ref sqlExpression2, graph, info, selection, ref this._operand);
    if (graph == null || !info.BuildExpression)
      return flag3;
    exp = exp.In(new Query().Select<EPApproval.refNoteID>().From<EPApproval>().Where(SQLExpressionExt.EQ(typeof (EPApproval.ownerID), sqlExpression1).Or(SQLExpressionExt.In(typeof (EPApproval.workgroupID), new Query().Select<EPCompanyTreeH.workGroupID>().From<EPCompanyTreeH>().Join<EPCompanyTreeMember>().On(SQLExpressionExt.EQ(typeof (EPCompanyTreeH.parentWGID), typeof (EPCompanyTreeMember.workGroupID)).And(SQLExpressionExt.NE(typeof (EPCompanyTreeH.parentWGID), typeof (EPCompanyTreeH.workGroupID))).And(SQLExpressionExt.EQ(typeof (EPCompanyTreeMember.active), (object) true)).And(SQLExpressionExt.EQ(typeof (EPCompanyTreeMember.contactID), sqlExpression2))).Where(SQLExpressionExt.EQ((SQLExpression) new SQLConst((object) 1), (SQLExpression) new SQLConst((object) 1)))))));
    return flag3;
  }
}
