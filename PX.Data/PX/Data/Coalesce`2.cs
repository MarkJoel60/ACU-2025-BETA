// Decompiled with JetBrains decompiler
// Type: PX.Data.Coalesce`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Retrieves a value using <tt>Search1</tt> or, if it returns null, <tt>Search2</tt>.
/// </summary>
/// <typeparam name="Search1">The main <tt>Search</tt> expression.</typeparam>
/// <typeparam name="Search2">The additional <tt>Search</tt> expression.</typeparam>
public class Coalesce<Search1, Search2> : BqlCommand, IBqlSearch, IBqlCoalesce
  where Search1 : IBqlSearch, new()
  where Search2 : IBqlSearch, new()
{
  public System.Type GetField() => new Search1().GetField();

  public string GetFieldName(PXGraph graph)
  {
    System.Type field = this.GetField();
    return BqlCommand.GetFieldName(field, graph.Caches[field.DeclaringType], PXDBOperation.Select);
  }

  public SQLExpression GetFieldExpression(PXGraph graph) => new Search1().GetFieldExpression(graph);

  public SQLExpression GetWhereExpression(PXGraph graph) => new Search1().GetWhereExpression(graph);

  public override Query GetQueryInternal(
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return (Query) null;
  }

  /// <exclude />
  public override void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
  }

  public override BqlCommand WhereNew<newWhere>()
  {
    return this.TransformSearches((Func<BqlCommand, BqlCommand>) (c => c.WhereNew<newWhere>()));
  }

  public override BqlCommand WhereNew(System.Type newWhere)
  {
    return this.TransformSearches((Func<BqlCommand, BqlCommand>) (c => c.WhereNew(newWhere)));
  }

  public override BqlCommand WhereAnd<where>()
  {
    return this.TransformSearches((Func<BqlCommand, BqlCommand>) (c => c.WhereAnd<where>()));
  }

  public override BqlCommand WhereAnd(System.Type where)
  {
    return this.TransformSearches((Func<BqlCommand, BqlCommand>) (c => c.WhereAnd(where)));
  }

  public override BqlCommand WhereOr<where>()
  {
    return this.TransformSearches((Func<BqlCommand, BqlCommand>) (c => c.WhereOr<where>()));
  }

  public override BqlCommand WhereOr(System.Type where)
  {
    return this.TransformSearches((Func<BqlCommand, BqlCommand>) (c => c.WhereOr(where)));
  }

  public override BqlCommand WhereNot()
  {
    return this.TransformSearches((Func<BqlCommand, BqlCommand>) (c => c.WhereNot()));
  }

  public override BqlCommand OrderByNew<newOrderBy>()
  {
    return this.TransformSearches((Func<BqlCommand, BqlCommand>) (c => c.OrderByNew<newOrderBy>()));
  }

  public override BqlCommand OrderByNew(System.Type newOrderBy)
  {
    return this.TransformSearches((Func<BqlCommand, BqlCommand>) (c => c.OrderByNew(newOrderBy)));
  }

  private BqlCommand TransformSearches(Func<BqlCommand, BqlCommand> commandTransformer)
  {
    return (BqlCommand) Activator.CreateInstance(typeof (Coalesce<,>).MakeGenericType(commandTransformer((object) new Search1() as BqlCommand).GetType(), commandTransformer((object) new Search2() as BqlCommand).GetType()));
  }

  public void GetCommands(List<BqlCommand> cmds)
  {
    object obj1 = (object) new Search1();
    object obj2 = (object) new Search2();
    if (obj1 is IBqlCoalesce)
      ((IBqlCoalesce) obj1).GetCommands(cmds);
    else
      cmds.Add((BqlCommand) obj1);
    if (obj2 is IBqlCoalesce)
      ((IBqlCoalesce) obj2).GetCommands(cmds);
    else
      cmds.Add((BqlCommand) obj2);
  }

  public BqlCommand[] GetCommands()
  {
    List<BqlCommand> cmds = new List<BqlCommand>();
    this.GetCommands(cmds);
    return cmds.ToArray();
  }
}
