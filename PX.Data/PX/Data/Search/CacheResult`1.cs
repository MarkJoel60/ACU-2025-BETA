// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.CacheResult`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Search;

/// <summary>
/// Represents a single record that is cached in user session.
/// </summary>
/// <typeparam name="T">Type of identifiers which will be saved in user session.</typeparam>
public class CacheResult<T> : CacheResult
{
  protected List<T> results;

  /// <summary>A list of records identifying database records.</summary>
  public List<T> Results => this.results;
}
