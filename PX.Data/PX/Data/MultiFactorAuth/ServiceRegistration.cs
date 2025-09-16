// Decompiled with JetBrains decompiler
// Type: PX.Data.MultiFactorAuth.ServiceRegistration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Features.Decorators;
using Microsoft.Extensions.Configuration;
using System;

#nullable enable
namespace PX.Data.MultiFactorAuth;

internal class ServiceRegistration : Module
{
  protected virtual void Load(
  #nullable disable
  ContainerBuilder builder)
  {
    OptionsContainerBuilderExtensions.Configure<OtpConfiguration>(builder, (Action<OtpConfiguration, IConfiguration>) ((options, configuration) => ConfigurationBinder.Bind((IConfiguration) configuration.GetSection("multifactor:otp"), (object) options, (System.Action<BinderOptions>) (opts => opts.BindNonPublicProperties = true))));
    RegistrationExtensions.RegisterDecorator<MultiFactorServiceHttpRequestConverter, IMultiFactorService>(builder, (Func<IDecoratorContext, bool>) null);
  }
}
