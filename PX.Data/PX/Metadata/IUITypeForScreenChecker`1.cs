// Decompiled with JetBrains decompiler
// Type: PX.Metadata.IUITypeForScreenChecker`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Metadata;

[PXInternalUseOnly]
public interface IUITypeForScreenChecker<in T>
{
  /// <summary>
  ///     Returns true if the data for collecting <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> for the specific data id is available.
  /// </summary>
  bool HasDataFor(T id);
}
