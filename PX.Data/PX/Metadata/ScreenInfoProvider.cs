// Decompiled with JetBrains decompiler
// Type: PX.Metadata.ScreenInfoProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Metadata;

[PXInternalUseOnly]
public class ScreenInfoProvider : IScreenInfoProvider
{
  private readonly IScreenInfoCollector<string> _screenInfoCollector;

  public ScreenInfoProvider(IScreenInfoCollector<string> screenInfoCollector)
  {
    this._screenInfoCollector = screenInfoCollector ?? throw new ArgumentNullException(nameof (screenInfoCollector));
  }

  public virtual PXSiteMap.ScreenInfo Get(string screenId, string locale)
  {
    return this._screenInfoCollector.Collect(screenId, locale);
  }
}
