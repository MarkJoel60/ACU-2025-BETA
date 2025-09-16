// Decompiled with JetBrains decompiler
// Type: PX.TM.Owned`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.TM;

/// <exclude />
[Obsolete]
public class Owned<Operand> : IBqlComparison, IBqlCreator, IBqlVerifier where Operand : IBqlOperand, new()
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
    Query q = new Query();
    q.Select<EPCompanyTreeH.workGroupID>().From<EPCompanyTreeH>().Join<EPCompanyTreeMember>().On(SQLExpressionExt.EQ(typeof (EPCompanyTreeH.parentWGID), typeof (EPCompanyTreeMember.workGroupID)).And(typeof (EPCompanyTreeMember.active).EQ((object) 1))).Join(typeof (PXAccess.BAccount)).On(SQLExpressionExt.EQ(typeof (PXAccess.BAccount.defContactID), typeof (EPCompanyTreeMember.contactID))).Join(typeof (PXAccess.EPEmployee)).On(SQLExpressionExt.EQ(typeof (PXAccess.EPEmployee.bAccountID), typeof (PXAccess.BAccount.bAccountID))).Where(SQLExpressionExt.EQ(Column.SQLColumn(typeof (PXAccess.EPEmployee.userID)), exp1));
    exp = exp.In(q);
    return flag;
  }
}
