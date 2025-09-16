// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.IsTableEmpty`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common;

public sealed class IsTableEmpty<Table> : IBqlOperand, IBqlCreator, IBqlVerifier where Table : class, IBqlTable, new()
{
  public void Verify(
    PXCache cache,
    object item,
    List<object> parameters,
    ref bool? result,
    ref object value)
  {
    if (cache.Graph?.Caches[typeof (Table)]?.Current != null)
      value = (object) false;
    else
      value = (object) ((object) PXResultset<Table>.op_Implicit(PXSelectBase<Table, PXSelect<Table>.Config>.SelectWindowed(cache.Graph, 0, 1, Array.Empty<object>())) == null);
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return true;
  }
}
