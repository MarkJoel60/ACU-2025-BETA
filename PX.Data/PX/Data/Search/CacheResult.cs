// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.CacheResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Search;

/// <summary>
/// Represents a single record that is cached in user session. This is an abstract class,
/// when creating such records use <see cref="T:PX.Data.Search.CacheResult`1" />.
/// </summary>
public abstract class CacheResult
{
  protected string query;
  protected System.Type searchType;

  /// <summary>Search query.</summary>
  public string Query => this.query;

  /// <summary>Type of search.</summary>
  public System.Type SearchType => this.searchType;
}
