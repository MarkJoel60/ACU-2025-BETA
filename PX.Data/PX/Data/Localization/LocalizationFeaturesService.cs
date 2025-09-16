// Decompiled with JetBrains decompiler
// Type: PX.Data.Localization.LocalizationFeaturesService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Localization;

internal class LocalizationFeaturesService : ILocalizationFeaturesService
{
  private readonly PXAccessProvider _accessProvider;

  public LocalizationFeaturesService(PXAccessProvider accessProvider)
  {
    this._accessProvider = accessProvider;
  }

  public string GetEnabledLocalization()
  {
    CurrentLocalization slot = PXContext.GetSlot<CurrentLocalization>();
    if (string.IsNullOrEmpty(slot?.LocalizationCode) || string.Equals(slot?.LocalizationCode, "00", StringComparison.InvariantCulture) || !this.GetEnabledLocalizations().Contains<string>(slot?.LocalizationCode))
      return (string) null;
    return slot?.LocalizationCode;
  }

  public string GetLocalizationCodeForFeature(string feature)
  {
    return this._accessProvider.GetLocalizationCodeForFeature(feature);
  }

  public IEnumerable<string> GetEnabledLocalizations()
  {
    return this._accessProvider.GetEnabledLocalizations();
  }
}
