// Decompiled with JetBrains decompiler
// Type: PX.Async.ParallelProcessing.ServiceRegistration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Microsoft.Extensions.Configuration;
using PX.Data;
using System;

#nullable disable
namespace PX.Async.ParallelProcessing;

internal class ServiceRegistration : Module
{
  protected virtual void Load(ContainerBuilder builder)
  {
    OptionsContainerBuilderExtensions.Configure<ParallelProcessingOptions>(builder, (Action<ParallelProcessingOptions, IConfiguration>) ((options, configuration) => options.Configure(PXLicenseHelper.License)));
  }
}
