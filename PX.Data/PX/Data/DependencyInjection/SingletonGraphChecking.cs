// Decompiled with JetBrains decompiler
// Type: PX.Data.DependencyInjection.SingletonGraphChecking
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Core;
using Autofac.Core.Lifetime;
using Autofac.Core.Resolving.Pipeline;
using System;

#nullable enable
namespace PX.Data.DependencyInjection;

internal static class SingletonGraphChecking
{
  internal static void DisallowSingleInstanceGraphs(this 
  #nullable disable
  ContainerBuilder containerBuilder)
  {
    containerBuilder.ComponentRegistryBuilder.Registered += (EventHandler<ComponentRegisteredEventArgs>) ((_, args) => SingletonGraphChecking.OnRegistered(args.ComponentRegistration));
  }

  private static void OnRegistered(IComponentRegistration registration)
  {
    if (registration.Sharing != 1)
      return;
    System.Type limitType = registration.Activator.LimitType;
    if (!TypeExtensions.IsAssignableTo<PXGraph>(limitType))
      return;
    if (registration.Lifetime is RootScopeLifetime)
      throw new InvalidOperationException($"Graph {limitType.FullName} is registered as singleton which is not allowed");
    ComponentRegistrationExtensions.ConfigurePipeline(registration, (System.Action<IResolvePipelineBuilder>) (builder => PipelineBuilderExtensions.Use(builder, (PipelinePhase) 200, (MiddlewareInsertionMode) 1, (Action<ResolveRequestContext, System.Action<ResolveRequestContext>>) ((context, next) =>
    {
      if (context.ActivationScope == context.ActivationScope.RootLifetimeScope)
        throw new InvalidOperationException($"Graph {limitType.FullName} is being activated in the root scope which is not allowed");
      next(context);
    }))));
  }
}
