// Decompiled with JetBrains decompiler
// Type: PX.Metadata.IScreenInfoCollector`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;

#nullable disable
namespace PX.Metadata;

[PXInternalUseOnly]
public interface IScreenInfoCollector<in T> : IUITypeForScreenChecker<T>
{
  /// <summary>
  ///     Collects <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> for the specific data id.
  /// </summary>
  /// <param name="id">A data ID (screenId, inquiryGenericId etc) for which <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> is collected.</param>
  /// <param name="locale">Locale that is used to collect <see cref="T:PX.Data.PXSiteMap.ScreenInfo" />.</param>
  /// <returns>Screen metadata for the specific data id</returns>
  /// <remarks>This method is executed under the admin user.</remarks>
  PXSiteMap.ScreenInfo Collect(T id, string locale);
}
