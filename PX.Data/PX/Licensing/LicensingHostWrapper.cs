// Decompiled with JetBrains decompiler
// Type: PX.Licensing.LicensingHostWrapper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PX.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Licensing;

internal class LicensingHostWrapper : IHost, IDisposable
{
  private readonly IHost _host;
  private readonly ILicensingManager _manager;

  public LicensingHostWrapper(IHost host)
  {
    this._host = host;
    this._manager = LicensingManager.GetVerifiedManager(host.Services);
    this._manager.InitializePXLogin(ServiceProviderServiceExtensions.GetRequiredService<PXLogin>(host.Services));
  }

  async Task IHost.StartAsync(CancellationToken cancellationToken)
  {
    await this._host.StartAsync(cancellationToken);
    await this._manager.StartAsync(cancellationToken);
  }

  Task IHost.StopAsync(CancellationToken cancellationToken)
  {
    return this._host.StopAsync(cancellationToken);
  }

  void IDisposable.Dispose() => ((IDisposable) this._host).Dispose();

  IServiceProvider IHost.Services => this._host.Services;
}
