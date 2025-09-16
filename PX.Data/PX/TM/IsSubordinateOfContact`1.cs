// Decompiled with JetBrains decompiler
// Type: PX.TM.IsSubordinateOfContact`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.TM;

/// <exclude />
public class IsSubordinateOfContact<Operand> : IBqlComparison, IBqlCreator, IBqlVerifier where Operand : IBqlOperand, new()
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

  public virtual bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag = true;
    if (info.Fields is BqlCommand.EqualityList fields)
      fields.NonStrict = true;
    SQLExpression exp1 = (SQLExpression) null;
    if (!typeof (IBqlCreator).IsAssignableFrom(typeof (Operand)))
    {
      if (info.BuildExpression)
        exp1 = BqlCommand.GetSingleExpression(typeof (Operand), graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
      info.Fields?.Add(typeof (Operand));
    }
    else
    {
      if (this._operand == null)
        this._operand = this._operand.createOperand<Operand>();
      flag &= this._operand.AppendExpression(ref exp1, graph, info, selection);
    }
    if (!info.BuildExpression)
      return flag;
    Query q1 = new Query();
    q1[typeof (EPCompanyTreeH.workGroupID)].From(typeof (EPCompanyTreeH)).Join(typeof (EPCompanyTreeMember)).On(SQLExpression.EQ(typeof (EPCompanyTreeH.parentWGID), typeof (EPCompanyTreeMember.workGroupID)).And(SQLExpressionExt.NE(Column.SQLColumn(typeof (EPCompanyTreeH.parentWGID)), (SQLExpression) Column.SQLColumn(typeof (EPCompanyTreeH.workGroupID))).And(Column.SQLColumn(typeof (EPCompanyTreeMember.active)).EQ((object) 1)).And(SQLExpressionExt.EQ(Column.SQLColumn(typeof (EPCompanyTreeMember.contactID)), exp1)))).Where(new SQLConst((object) 1).EQ((object) 1));
    Query q2 = new Query();
    q2[typeof (EPCompanyTreeMember.contactID)].From(typeof (EPCompanyTreeMember)).Where(Column.SQLColumn(typeof (EPCompanyTreeMember.workGroupID)).In(q1));
    exp = exp.In(q2);
    return flag;
  }
}
