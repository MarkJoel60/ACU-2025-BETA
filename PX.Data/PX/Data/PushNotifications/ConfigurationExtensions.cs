// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.ConfigurationExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Configuration;

#nullable disable
namespace PX.Data.PushNotifications;

public static class ConfigurationExtensions
{
  public static bool PushNotificationsEnabled(this IConfiguration configuration)
  {
    return ConfigurationBinder.GetValue<bool>(configuration, "EnablePushNotifications", true) && ConfigurationBinder.GetValue<QueueType?>(configuration, "QueueType").HasValue && ConfigurationBinder.GetValue<StorageType?>(configuration, "StorageType").HasValue;
  }
}
