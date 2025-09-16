// Decompiled with JetBrains decompiler
// Type: PX.Data.Config.ModernUiAggregatedScreenWhitelist
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Options;
using PX.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Config;

[PXInternalUseOnly]
public class ModernUiAggregatedScreenWhitelist
{
  public ModernUiAggregatedScreenWhitelist(
    IEnumerable<IModernUiScreenWhitelist> screenWhitelists,
    IOptions<ModernUiSwitchOptions> options)
  {
    HashSet<string> stringSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (IModernUiScreenWhitelist screenWhitelist in screenWhitelists)
      EnumerableExtensions.AddRange<string>((ISet<string>) stringSet, (IEnumerable<string>) screenWhitelist.AllowedScreens);
    this.AllowedScreens = stringSet;
    this.UseNewUIWhitelist = options.Value.UseNewUIWhitelist;
    this.DisableWhitelistWarning = options.Value.DisableWhitelistWarning;
  }

  public HashSet<string> AllowedScreens { get; }

  public bool UseNewUIWhitelist { get; }

  public bool DisableWhitelistWarning { get; }
}
