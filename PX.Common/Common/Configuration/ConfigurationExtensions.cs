// Decompiled with JetBrains decompiler
// Type: PX.Common.Configuration.ConfigurationExtensions
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using Microsoft.Extensions.Configuration;

#nullable disable
namespace PX.Common.Configuration;

[PXInternalUseOnly]
public static class ConfigurationExtensions
{
  [PXInternalUseOnly]
  public static bool IsClusterEnabled(this IConfiguration configuration)
  {
    return WebConfig.GetAndValidateIsClusterEnabled(ConfigurationBinder.GetValue<bool>(configuration, nameof (IsClusterEnabled), false), ConfigurationBinder.GetValue<bool>(configuration, "SharedSessionEnabled", false), configuration.IsMultiSiteMode());
  }

  [PXInternalUseOnly]
  public static bool IsMultiSiteMode(this IConfiguration configuration)
  {
    return ConfigurationBinder.GetValue<bool>(configuration, nameof (IsMultiSiteMode), false);
  }
}
