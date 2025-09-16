// Decompiled with JetBrains decompiler
// Type: PX.Licensing.ServiceRegistration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;

#nullable disable
namespace PX.Licensing;

internal class ServiceRegistration : Module
{
  protected virtual void Load(ContainerBuilder builder)
  {
    OptionsContainerBuilderExtensions.BindFromConfiguration<LicenseObserverServiceOptions>(builder);
    RegistrationExtensions.RegisterType<LicensingUiService>(builder).As<ILicensingUiService>().SingleInstance();
  }
}
