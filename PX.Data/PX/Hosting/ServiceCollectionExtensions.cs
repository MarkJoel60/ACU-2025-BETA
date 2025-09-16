// Decompiled with JetBrains decompiler
// Type: PX.Hosting.ServiceCollectionExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.DependencyInjection;
using PX.Common;
using System;
using System.Threading.Tasks;

#nullable enable
namespace PX.Hosting;

[PXInternalUseOnly]
public static class ServiceCollectionExtensions
{
  [PXInternalUseOnly]
  public static 
  #nullable disable
  IServiceCollection UseAsyncLocalInBackground(this IServiceCollection services)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return OptionsServiceCollectionExtensions.Configure<BackgroundExecutionOptions>(services, (Action<BackgroundExecutionOptions>) (options => options.AsyncLocalContextWrapper = ServiceCollectionExtensions.\u003C\u003EO.\u003C0\u003E__AsyncLocal ?? (ServiceCollectionExtensions.\u003C\u003EO.\u003C0\u003E__AsyncLocal = new Func<Func<Task>, Task>(PXContext.AsyncLocal))));
  }
}
