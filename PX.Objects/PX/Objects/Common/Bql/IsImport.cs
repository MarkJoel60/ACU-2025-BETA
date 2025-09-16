// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Bql.IsImport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common.Bql;

public class IsImport : BqlOperand<IsImport, IBqlBool>, IBqlCreator, IBqlVerifier
{
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    value = (object) cache.Graph.IsImport;
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (graph != null && info.BuildExpression)
      exp = SQLExpression.IsTrue(graph.IsImport);
    return true;
  }
}
