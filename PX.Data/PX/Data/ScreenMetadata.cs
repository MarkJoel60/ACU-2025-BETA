// Decompiled with JetBrains decompiler
// Type: PX.Data.ScreenMetadata
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Description;

#nullable disable
namespace PX.Data;

/// <exclude />
internal class ScreenMetadata
{
  internal ScreenType ScreenType { get; }

  internal PXViewDescription PrimaryView { get; }

  internal PXViewDescription DataView { get; }

  internal PXSiteMap.ScreenInfo ScreenInfo { get; }

  internal ScreenMetadata(
    ScreenType screenType,
    PXSiteMap.ScreenInfo screenInfo,
    PXViewDescription primaryView,
    PXViewDescription dataView)
  {
    this.ScreenType = screenType;
    this.PrimaryView = primaryView;
    this.DataView = dataView;
    this.ScreenInfo = screenInfo;
  }

  internal ScreenMetadata(
    ScreenType screenType,
    PXSiteMap.ScreenInfo screenInfo,
    PXViewDescription primaryView)
    : this(screenType, screenInfo, primaryView, primaryView)
  {
  }

  internal ScreenMetadata(PXSiteMap.ScreenInfo screenInfo)
    : this(ScreenType.Unknown, screenInfo, (PXViewDescription) null, (PXViewDescription) null)
  {
  }
}
