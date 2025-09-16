// Decompiled with JetBrains decompiler
// Type: PX.Data.Match`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.SQLTree;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>Matches only the data records the specified user has access rights for. The condition is applied to the data records of the first table mentioned in a BQL
/// statement.</summary>
/// <typeparam name="Parameter">The user, which is typically specified through the <tt>Current</tt> parameter.</typeparam>
/// <example>
///   <code title="" description="" lang="CS">
/// PXSelect&lt;Table1,
///     Where&lt;Match&lt;Current&lt;AccessInfo.userName&gt;&gt;&gt;&gt; records;</code>
/// </example>
public sealed class Match<Parameter> : 
  BqlChainableConditionLite<Match<Parameter>>,
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where Parameter : IBqlParameter, new()
{
  private IBqlParameter _parameter;

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    result = new bool?(true);
    if (pars.Count <= 0)
      return;
    pars.RemoveAt(0);
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    List<System.Type> tables = info.Tables;
    if (this._parameter == null)
    {
      this._parameter = (IBqlParameter) new Parameter();
      this._parameter.NullAllowed = true;
      if (tables != null)
        this._parameter.MaskedType = tables[0];
    }
    else if (tables != null)
      this._parameter.MaskedType = tables[0];
    info.Parameters?.Add(this._parameter);
    if (graph == null || !info.BuildExpression)
      return true;
    if (GroupHelper.Count == 0 || !graph.Caches[this._parameter.MaskedType].Fields.Contains<string>("GroupMask", (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
    {
      exp = new SQLConst((object) 1).EQ((object) 1);
      info.Parameters?.RemoveAt(info.Parameters.Count - 1);
      return true;
    }
    System.Type referencedType = this._parameter.GetReferencedType();
    bool flag = referencedType != (System.Type) null && referencedType.IsNested && (BqlCommand.GetItemType(referencedType) == typeof (RelationGroup) || typeof (RelationGroup).IsAssignableFrom(BqlCommand.GetItemType(referencedType)));
    exp = SQLExpression.None();
    SQLExpression r1 = SQLExpression.None();
    for (uint index = 0; (long) index < (long) ((GroupHelper.Count + 31 /*0x1F*/) / 32 /*0x20*/); ++index)
    {
      if (index > 0U)
        info.Parameters?.Add(this._parameter);
      Column column = new Column("GroupMask", (Table) new SimpleTable(tables[0]));
      Literal r2 = Literal.NewParameter(selection.ParamCounter++);
      Literal r3 = Literal.NewParameter(selection.ParamCounter++);
      if (flag)
      {
        SQLExpression r4 = SQLExpressionExt.EQ(new SQLConst((object) 0), (SQLExpression) r2).Or(SQLExpressionExt.NE(new SQLConst((object) 0), column.ConvertBinToInt((uint) ((int) index * 4 + 1), 4U).BitAnd((SQLExpression) new SQLConst((object) -1))).And(SQLExpressionExt.NE(new SQLConst((object) 0), column.ConvertBinToInt((uint) ((int) index * 4 + 1), 4U).BitAnd((SQLExpression) r3))));
        exp = exp.And(r4);
      }
      else
      {
        SQLExpression r5 = SQLExpressionExt.EQ(new SQLConst((object) 0), column.ConvertBinToInt((uint) ((int) index * 4 + 1), 4U).BitAnd((SQLExpression) r2));
        SQLExpression r6 = SQLExpressionExt.NE(new SQLConst((object) 0), column.ConvertBinToInt((uint) ((int) index * 4 + 1), 4U).BitAnd((SQLExpression) r3));
        exp = exp.And(r5);
        r1 = r1.Or(r6);
      }
      info.Parameters?.Add(this._parameter);
    }
    if (r1.Oper() != SQLExpression.Operation.NONE)
      exp = exp.Or(r1).Embrace();
    return true;
  }
}
