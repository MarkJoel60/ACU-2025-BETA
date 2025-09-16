// Decompiled with JetBrains decompiler
// Type: PX.Data.Not`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Adds logical "not" to a unary operator. In the resulting SQL the group is
/// preceded with NOT and surrounded by brackets.
/// </summary>
/// <typeparam name="Operator">The unary operator, <tt>Where</tt>, <tt>Where2</tt>,
/// or <tt>Match</tt> class.</typeparam>
/// <example>
/// The code below shows the definition of a data view in a graph.
/// The <tt>Not&lt;&gt;</tt> class is used in joining condition to add
/// negation to the <see cref="T:PX.Data.Match`2">Match&lt;,&gt;</see> clause.
/// <code>
/// public PXSelectJoin&lt;Schedule,
///     LeftJoin&lt;APRegisterAccess, On&lt;APRegisterAccess.scheduleID, Equal&lt;Schedule.scheduleID&gt;,
///         And&lt;APRegisterAccess.scheduled, Equal&lt;boolTrue&gt;,
///         And&lt;Not&lt;Match&lt;APRegisterAccess, Current&lt;AccessInfo.userName&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;
///         Schedule_Header;
/// </code>
/// </example>
public sealed class Not<Operator> : 
  BqlChainableConditionLite<Not<Operator>>,
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where Operator : IBqlUnary, new()
{
  private IBqlUnary _operator;

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (this._operator == null)
      this._operator = (IBqlUnary) new Operator();
    this._operator.Verify(cache, item, pars, ref result, ref value);
    bool? nullable = result;
    bool flag1 = false;
    if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
    {
      result = new bool?(true);
    }
    else
    {
      nullable = result;
      bool flag2 = true;
      if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
        return;
      result = new bool?(false);
    }
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (this._operator == null)
      this._operator = (IBqlUnary) new Operator();
    SQLExpression exp1 = (SQLExpression) null;
    int num = 1 & (this._operator.AppendExpression(ref exp1, graph, info, selection) ? 1 : 0);
    if (!info.BuildExpression)
      return num != 0;
    exp = SQLExpression.Not(exp1);
    return num != 0;
  }
}
