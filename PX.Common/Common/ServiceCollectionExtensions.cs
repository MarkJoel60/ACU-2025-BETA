// Decompiled with JetBrains decompiler
// Type: PX.Common.ServiceCollectionExtensions
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using Microsoft.Extensions.DependencyInjection;
using PX.Security;
using System;

#nullable enable
namespace PX.Common;

[PXInternalUseOnly]
public static class ServiceCollectionExtensions
{
  [PXInternalUseOnly]
  public static 
  #nullable disable
  IServiceCollection AddSessionContextFactory(this IServiceCollection services)
  {
    return ServiceCollectionServiceExtensions.AddSingleton<ISessionContextFactory>(services, ServiceCollectionExtensions.\u0002.\u000E ?? (ServiceCollectionExtensions.\u0002.\u000E = new Func<IServiceProvider, ISessionContextFactory>(ServiceCollectionExtensions.\u0002.\u0002.\u0002)));
  }

  [Serializable]
  private sealed class \u0002
  {
    public static readonly ServiceCollectionExtensions.\u0002 \u0002 = new ServiceCollectionExtensions.\u0002();
    public static Func<
    #nullable enable
    IServiceProvider, ISessionContextFactory> \u000E;

    internal ISessionContextFactory \u0002(IServiceProvider _param1)
    {
      return (ISessionContextFactory) new PXSessionContextFactory(ServiceProviderServiceExtensions.GetRequiredService<IRoleManagementService>(_param1), ServiceProviderServiceExtensions.GetRequiredService<ISessionContextFactoryAdapter>(_param1));
    }
  }
}
