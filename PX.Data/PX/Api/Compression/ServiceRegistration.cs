// Decompiled with JetBrains decompiler
// Type: PX.Api.Compression.ServiceRegistration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Microsoft.Extensions.Configuration;
using System;

#nullable disable
namespace PX.Api.Compression;

public class ServiceRegistration : Module
{
  private readonly IConfiguration _configuration;

  public ServiceRegistration(IConfiguration configuration) => this._configuration = configuration;

  protected virtual void Load(ContainerBuilder builder)
  {
    int threshold;
    if (ServerCompressionHandler.TryConfigure(this._configuration, out threshold))
      RegistrationExtensions.RegisterInstance<Func<ServerCompressionHandler>>(builder, (Func<ServerCompressionHandler>) (() => new ServerCompressionHandler(threshold, new ICompressor[2]
      {
        (ICompressor) new GZipCompressor(),
        (ICompressor) new DeflateCompressor()
      })));
    else
      RegistrationExtensions.RegisterInstance<Func<ServerCompressionHandler>>(builder, (Func<ServerCompressionHandler>) (() => (ServerCompressionHandler) null));
  }
}
