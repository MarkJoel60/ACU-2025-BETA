// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.LocalizationServiceExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CS;

public static class LocalizationServiceExtensions
{
  public static bool LocalizationEnabled<TFeature>(string localizationCode) where TFeature : IBqlField
  {
    return PXAccess.LocalizationEnabled<TFeature>(localizationCode);
  }

  public static bool LocalizationEnabled<TFeature>(PXView primaryView) where TFeature : IBqlField
  {
    return LocalizationServiceExtensions.LocalizationEnabled<TFeature>(OrganizationLocalizationHelper.GetCurrentLocalizationCode(primaryView));
  }

  public static bool LocalizationEnabled<TFeature>(PXGraph graph) where TFeature : IBqlField
  {
    return LocalizationServiceExtensions.LocalizationEnabled<TFeature>(OrganizationLocalizationHelper.GetCurrentLocalizationCode(graph));
  }
}
