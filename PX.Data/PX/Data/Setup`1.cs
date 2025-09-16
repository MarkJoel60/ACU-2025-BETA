// Decompiled with JetBrains decompiler
// Type: PX.Data.Setup`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#nullable disable
namespace PX.Data;

/// <summary>
/// Returns the value of the specified field from the setup data record.
/// The setup data record is obtained by the <see cref="M:PX.Data.PXSetup`1.Select(PX.Data.PXGraph,System.Object[])" /> method.
/// </summary>
/// <typeparam name="Field">The field of the setup data record.</typeparam>
public sealed class Setup<Field> : IBqlCreator, IBqlVerifier, IBqlOperand where Field : IBqlField
{
  bool IBqlCreator.AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return true;
  }

  void IBqlVerifier.Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    System.Type itemType = BqlCommand.GetItemType(typeof (Field));
    PXCache cach = cache.Graph.Caches[itemType];
    object data = (object) GenericCall.Of<IBqlTable>((Expression<Func<IBqlTable>>) (() => this.GetSetup<Setup<Field>.DummySetup>(cache.Graph))).ButWith(itemType, Array.Empty<System.Type>());
    value = cach.GetValue(data, typeof (Field).Name);
  }

  private IBqlTable GetSetup<TSetup>(PXGraph graph) where TSetup : class, IBqlTable, new()
  {
    return (IBqlTable) (TSetup) PXSetup<TSetup>.Select(graph, Array.Empty<object>());
  }

  [PXHidden]
  private class DummySetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }
}
