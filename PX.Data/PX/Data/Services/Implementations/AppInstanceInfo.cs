// Decompiled with JetBrains decompiler
// Type: PX.Data.Services.Implementations.AppInstanceInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Services.Interfaces;

#nullable disable
namespace PX.Data.Services.Implementations;

internal class AppInstanceInfo : IAppInstanceInfo
{
  private readonly WebAppType _webAppType;

  public AppInstanceInfo(
    string appVersion,
    string installationId,
    WebAppType webAppType,
    bool isCluster)
  {
    this.Version = appVersion;
    this.InstallationId = installationId;
    this._webAppType = webAppType;
    this.IsCluster = isCluster;
  }

  public string Version { get; }

  public string InstallationId { get; }

  public bool IsPortal => this._webAppType.IsPortal();

  public bool IsCluster { get; }
}
