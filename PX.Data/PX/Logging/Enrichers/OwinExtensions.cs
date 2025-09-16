// Decompiled with JetBrains decompiler
// Type: PX.Logging.Enrichers.OwinExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Owin;
using Owin;
using PX.Common;
using System;
using System.Threading.Tasks;

#nullable disable
namespace PX.Logging.Enrichers;

[PXInternalUseOnly]
public static class OwinExtensions
{
  public static IAppBuilder DisableAspNetCallbackEnricher(this IAppBuilder app)
  {
    return AppBuilderUseExtensions.Use(app, (Func<IOwinContext, Func<Task>, Task>) (async (ctx, next) =>
    {
      using (AspNetCallbackEnricher.Disable())
        await next();
    }));
  }
}
