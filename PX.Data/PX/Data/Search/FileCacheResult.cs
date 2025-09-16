// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.FileCacheResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Search;

/// <exclude />
[Serializable]
public class FileCacheResult : CacheResult<Guid>
{
  public FileCacheResult(string query, List<Guid> results)
  {
    this.query = query;
    this.results = results;
    this.searchType = typeof (PXScreenSearch);
  }
}
