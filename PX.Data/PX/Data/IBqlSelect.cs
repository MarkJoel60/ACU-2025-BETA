// Decompiled with JetBrains decompiler
// Type: PX.Data.IBqlSelect
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;

#nullable disable
namespace PX.Data;

/// <summary>A BQL select command.</summary>
public interface IBqlSelect
{
  BqlCommand AddNewJoin(System.Type newJoin);

  IBqlOrderBy GetOrderBy();

  /// <summary>Returns the BQL select command in the internal SQL tree format.</summary>
  /// <param name="graph">A <see cref="T:PX.Data.PXGraph">PXGraph</see> instance.</param>
  /// <param name="info">The information about the BQL select command.</param>
  /// <returns>The query in internal SQL tree format.</returns>
  Query GetQueryInternal(PXGraph graph, BqlCommandInfo info, BqlCommand.Selection selection);
}
