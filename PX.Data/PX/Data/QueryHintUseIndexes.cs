// Decompiled with JetBrains decompiler
// Type: PX.Data.QueryHintUseIndexes
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
internal class QueryHintUseIndexes : IBqlQueryHintTableOptions
{
  private IEnumerable<string> _indexes;

  public QueryHintUseIndexes(IEnumerable<string> indexes) => this._indexes = indexes;

  public IEnumerable<string> ForcedIndexes => this._indexes;
}
