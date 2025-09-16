// Decompiled with JetBrains decompiler
// Type: PX.Data.SetBase`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class SetBase<Field, Operand, NextSet> : IBqlSet
  where Field : IBqlField
  where Operand : IBqlOperand
  where NextSet : IBqlSet
{
  private IBqlCreator _operand;
  private IBqlSet _nextSet;

  public bool AppendExpression(
    PXGraph graph,
    BqlCommandInfo info,
    List<KeyValuePair<SQLExpression, SQLExpression>> assignments)
  {
    bool flag = true;
    SQLExpression key = SQLExpression.None();
    if (assignments != null)
      key = BqlCommand.GetSingleExpression(typeof (Field), graph, info.Tables, (BqlCommand.Selection) null, BqlCommand.FieldPlace.Select);
    if (typeof (IBqlField).IsAssignableFrom(typeof (Operand)))
    {
      assignments?.Add(new KeyValuePair<SQLExpression, SQLExpression>(key, BqlCommand.GetSingleExpression(typeof (Operand), graph, info.Tables, (BqlCommand.Selection) null, BqlCommand.FieldPlace.Select)));
    }
    else
    {
      if (this._operand == null)
        this._operand = this._operand.createOperand<Operand>();
      SQLExpression exp = (SQLExpression) null;
      flag &= this._operand.AppendExpression(ref exp, graph, info, (BqlCommand.Selection) null);
      assignments?.Add(new KeyValuePair<SQLExpression, SQLExpression>(key, exp));
    }
    if (typeof (NextSet) != typeof (BqlNone))
    {
      if (this._nextSet == null)
        this._nextSet = (IBqlSet) Activator.CreateInstance<NextSet>();
      flag &= this._nextSet.AppendExpression(graph, info, assignments);
    }
    return flag;
  }

  public System.Type GetFieldType() => typeof (Field).DeclaringType;
}
