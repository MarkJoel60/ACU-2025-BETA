// Decompiled with JetBrains decompiler
// Type: PX.Data.InBase3`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class InBase3<Constants> : IBqlComparison, IBqlCreator, IBqlVerifier where Constants : IBqlConstants, new()
{
  protected Constants _constants { get; }

  /// <exclude />
  public InBase3() => this._constants = new Constants();

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    IEnumerable<object> values = this._constants.GetValues(graph);
    SQLExpression sequence = (SQLExpression) null;
    if (!values.Any<object>())
    {
      sequence = SQLExpression.Null();
    }
    else
    {
      foreach (object v in values)
        sequence = sequence == null ? (SQLExpression) new SQLConst(v) : sequence.Seq((SQLExpression) new SQLConst(v));
    }
    if (info.BuildExpression)
      this.AppendExpression(ref exp, sequence);
    return true;
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    result = new bool?(this.Verify(value, this._constants.GetValues(cache.Graph)));
  }

  protected abstract void AppendExpression(ref SQLExpression exp, SQLExpression sequence);

  protected abstract bool Verify(object value, IEnumerable<object> sequence);
}
