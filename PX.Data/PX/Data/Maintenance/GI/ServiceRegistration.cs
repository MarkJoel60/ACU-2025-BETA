// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.ServiceRegistration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Builder;
using Microsoft.Extensions.Hosting;
using PX.Data.GenericInquiry;
using PX.Data.GenericInquiry.Services;
using Serilog;
using System;

#nullable enable
namespace PX.Data.Maintenance.GI;

public class ServiceRegistration : Module
{
  private readonly 
  #nullable disable
  IHostEnvironment _hostEnvironment;

  public ServiceRegistration(IHostEnvironment hostEnvironment)
  {
    this._hostEnvironment = hostEnvironment;
  }

  protected virtual void Load(ContainerBuilder builder)
  {
    RegistrationExtensions.RegisterType<SystemNamespacesTablesRestrictor>(builder).As<IPXSchemaTableRestrictor>().SingleInstance();
    RegistrationExtensions.Register<XMLTablesRestrictor>(builder, (Func<IComponentContext, XMLTablesRestrictor>) (ctx => new XMLTablesRestrictor(this._hostEnvironment.ContentRootFileProvider, ResolutionExtensions.Resolve<ILogger>(ctx).ForContext<XMLTablesRestrictor>(), "App_Data/GITables.xml"))).As<IPXSchemaTableRestrictor>().SingleInstance();
    RegistrationExtensions.AsSelf<ObsoleteTablesRestrictor, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<ObsoleteTablesRestrictor>(builder)).SingleInstance();
    RegistrationExtensions.RegisterType<GenericInquiryReferenceInfoProvider>(builder).As<IGenericInquiryReferenceInfoProvider>().SingleInstance();
    RegistrationExtensions.RegisterType<GenericInquiryDescriptionProvider>(builder).As<IGenericInquiryDescriptionProvider>().SingleInstance();
    RegistrationExtensions.AsSelf<GIResultViewProcessor, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<GIResultViewProcessor>(builder)).As<IGIResultViewProcessor>();
    OptionsContainerBuilderExtensions.BindFromConfiguration<GIOptions>(builder, new string[1]
    {
      "genericInquiry"
    });
    builder.RegisterGIValidationServices();
  }
}
