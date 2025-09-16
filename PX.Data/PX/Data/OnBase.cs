// Decompiled with JetBrains decompiler
// Type: PX.Data.OnBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.PushNotifications;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class OnBase : IBqlOn, IBqlCreator, IBqlVerifier
{
  public abstract void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value);

  public bool AppendJoiner(
    Joiner join,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    SQLExpression exp = SQLExpression.None();
    int num = this.AppendExpression(ref exp, graph, info, selection) ? 1 : 0;
    join.On(exp);
    return num != 0;
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    int num = this.AppendJoinExpression(ref exp, graph, info, selection) ? 1 : 0;
    if (graph == null)
      return num != 0;
    TableChangingScope.AppendRestrictionsOnIsNew(ref exp, graph, info.Tables, selection);
    return num != 0;
  }

  public abstract bool AppendJoinExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection);

  public abstract IBqlUnary GetMatchingWhere();
}
