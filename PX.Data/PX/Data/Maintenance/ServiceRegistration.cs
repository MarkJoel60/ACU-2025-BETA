// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.ServiceRegistration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Builder;

#nullable disable
namespace PX.Data.Maintenance;

internal class ServiceRegistration : Module
{
  protected virtual void Load(ContainerBuilder builder)
  {
    RegistrationExtensions.RegisterType<AppRestartExecutor>(builder).As<IAppRestartExecutor>().SingleInstance();
    RegistrationExtensions.RegisterType<WatchDogAppRestartNotificationTransport>(builder).As<IAppRestartNotificationTransport>().SingleInstance();
    ApplicationStartActivation.ActivateOnApplicationStart<AppRestartService, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<AppRestartService>(builder).As<IAppRestartService>().SingleInstance(), (System.Action<AppRestartService>) (observer => observer.Subscribe()));
  }
}
