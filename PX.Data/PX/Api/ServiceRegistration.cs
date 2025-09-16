// Decompiled with JetBrains decompiler
// Type: PX.Api.ServiceRegistration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Builder;
using PX.Api.Services;
using PX.Data.Api.Services;

#nullable disable
namespace PX.Api;

public class ServiceRegistration : Module
{
  protected virtual void Load(ContainerBuilder builder)
  {
    RegistrationExtensions.RegisterType<CompanyService>(builder).As<ICompanyService>().SingleInstance();
    RegistrationExtensions.RegisterType<LoginService>(builder).WithInternalConstructor<LoginService, ConcreteReflectionActivatorData, SingleRegistrationStyle>().As<ILoginService>().SingleInstance();
    RegistrationExtensions.RegisterType<ScreenService>(builder).As<IScreenService>();
    RegistrationExtensions.RegisterType<MiscService>(builder).As<IMiscService>().SingleInstance();
    RegistrationExtensions.RegisterType<ImportService>(builder).As<IImportService>().SingleInstance();
    RegistrationExtensions.RegisterType<AttributesService>(builder).As<IAttributesService>().SingleInstance();
    RegistrationExtensions.RegisterType<DialogService>(builder).As<IDialogService>().SingleInstance();
    RegistrationExtensions.RegisterType<CustomFilterSerializer>(builder).As<ICustomFilterSerializer>().SingleInstance();
    RegistrationExtensions.RegisterType<VersionedStateProvider>(builder).As<IVersionedStateProvider>().SingleInstance();
    RegistrationExtensions.RegisterType<SignatureService>(builder).As<ISignatureService>().SingleInstance();
  }
}
