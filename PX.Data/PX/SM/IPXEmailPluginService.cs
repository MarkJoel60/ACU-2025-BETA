// Decompiled with JetBrains decompiler
// Type: PX.SM.IPXEmailPluginService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.SM;

[PXInternalUseOnly]
public interface IPXEmailPluginService
{
  IEmailPluginSettings GetSettings(EMailAccount emailAccount, bool insertSettingsIfNotFound = false);

  TSettings GetSettings<TSettings>(EMailAccount emailAccount, bool insertSettingsIfNotFound = false) where TSettings : IEmailPluginSettings;

  IEmailConnectedService GetService(EMailAccount emailAccount, bool insertSettingsIfNotFound = false);

  TService GetService<TService>(EMailAccount emailAccount, bool insertSettingsIfNotFound = false) where TService : IEmailConnectedService;
}
