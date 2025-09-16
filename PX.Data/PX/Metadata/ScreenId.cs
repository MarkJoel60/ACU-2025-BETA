// Decompiled with JetBrains decompiler
// Type: PX.Metadata.ScreenId
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Metadata;

/// <summary>
/// Represents the screen identifier in legacy UI.
/// Uses in combination with <see cref="T:PX.Metadata.IScreenInfoCollector`1" /> to get the screen info from aspx.
/// After the transition to TypeScript-based UI, it will be removed.
/// </summary>
/// <summary>
/// Represents the screen identifier in legacy UI.
/// Uses in combination with <see cref="T:PX.Metadata.IScreenInfoCollector`1" /> to get the screen info from aspx.
/// After the transition to TypeScript-based UI, it will be removed.
/// </summary>
internal sealed class ScreenId(string screenId, string customPath = null) : 
  IScreenInfoDataId,
  IScreenDataCustomPath
{
  private readonly string _cacheKeyPrefix;

  private ScreenId(string screenId, string cacheKeyPrefix, string customPath = null)
    : this(screenId, customPath)
  {
    this._cacheKeyPrefix = cacheKeyPrefix;
  }

  public string Id { get; } = screenId;

  string IScreenDataCustomPath.CustomPath => customPath;

  string IScreenInfoDataId.GetCacheKey()
  {
    string upperInvariant = this.Id.ToUpperInvariant();
    return !Str.IsNullOrEmpty(this._cacheKeyPrefix) ? this._cacheKeyPrefix + upperInvariant : upperInvariant;
  }

  /// <summary>
  ///     Creates a ScreenId with a custom path to its file
  ///     and a prefix for its cache key, which is used to store the screen info in the cache.
  ///     This allows us to cache the data in different cache slots.
  /// </summary>
  internal static ScreenId CreateWithCustomPath(
    string screenId,
    string customPath,
    string cacheKeyPrefix)
  {
    return new ScreenId(screenId, cacheKeyPrefix, customPath);
  }
}
