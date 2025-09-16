// Decompiled with JetBrains decompiler
// Type: PX.Data.NoSort
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public class NoSort : IBqlSortColumn, IBqlCreator, IBqlVerifier
{
  public System.Type GetReferencedType() => (System.Type) null;

  public bool IsDescending => false;

  public void AppendQuery(
    Query query,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    info.SortColumns?.Add((IBqlSortColumn) this);
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    info.SortColumns?.Add((IBqlSortColumn) this);
    return true;
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
  }
}
