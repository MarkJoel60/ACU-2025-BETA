// Decompiled with JetBrains decompiler
// Type: PX.Metadata.IScreenInfoProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.Metadata;

public interface IScreenInfoProvider
{
  /// <summary>
  /// Returns <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> for the specific screen.
  /// </summary>
  /// <param name="screenId">Screen ID for which <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> is collected.</param>
  /// <param name="locale">Locale that is used to collect <see cref="T:PX.Data.PXSiteMap.ScreenInfo" />.</param>
  /// <returns>Screen metadata for the specific screen;
  /// <see langword="null" />, if the screen does not exist.</returns>
  PXSiteMap.ScreenInfo Get(string screenId, string locale);
}
