// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.LocalizationFeatureScope
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

public class LocalizationFeatureScope : IDisposable
{
  private readonly string _previousLocalizationCode;

  public LocalizationFeatureScope(PXGraph graph)
    : this(OrganizationLocalizationHelper.GetCurrentLocalizationCode(graph))
  {
  }

  public LocalizationFeatureScope(PXView primaryView)
    : this(OrganizationLocalizationHelper.GetCurrentLocalizationCode(primaryView))
  {
  }

  public LocalizationFeatureScope(string localizationCode)
  {
    CurrentLocalization slot = PXContext.GetSlot<CurrentLocalization>();
    if (!string.IsNullOrEmpty(slot?.LocalizationCode))
      this._previousLocalizationCode = slot.LocalizationCode;
    PXContext.SetSlot<CurrentLocalization>(new CurrentLocalization(localizationCode));
  }

  public void Dispose()
  {
    if (!string.IsNullOrEmpty(this._previousLocalizationCode))
      PXContext.SetSlot<CurrentLocalization>(new CurrentLocalization(this._previousLocalizationCode));
    else
      PXContext.ClearSlot<CurrentLocalization>();
  }
}
