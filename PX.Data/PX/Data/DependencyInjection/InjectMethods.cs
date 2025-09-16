// Decompiled with JetBrains decompiler
// Type: PX.Data.DependencyInjection.InjectMethods
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.DependencyInjection;

internal static class InjectMethods
{
  internal static void InjectDependencies(PXGraph graph, System.Type graphType, string prefix)
  {
    if (!InjectMethods.RequiresInjection((object) graph) && (graph.Extensions == null || !((IEnumerable<PXGraphExtension>) graph.Extensions).Any<PXGraphExtension>((Func<PXGraphExtension, bool>) (e => InjectMethods.RequiresInjection((object) e)))))
      return;
    CompositionRoot.GetDependencyInjector()?.InjectDependencies(graph, graphType, prefix);
  }

  internal static void InjectDependencies(PXAction action)
  {
    if (!InjectMethods.RequiresInjection((object) action))
      return;
    CompositionRoot.GetDependencyInjector()?.InjectDependencies(action);
  }

  internal static void InjectDependencies(PXEventSubscriberAttribute attribute, PXCache cache)
  {
    if ((cache == null || !InjectMethods.RequiresInjection((object) attribute)) && (cache != null || !InjectMethods.RequiresInjectionTypeLevel((object) attribute)))
      return;
    CompositionRoot.GetDependencyInjector()?.InjectDependencies(attribute, cache);
  }

  internal static void InjectDependencies(PXSelectBase indexer)
  {
    if (!InjectMethods.RequiresInjection((object) indexer))
      return;
    CompositionRoot.GetDependencyInjector()?.InjectDependencies(indexer);
  }

  private static bool RequiresInjection(object instance)
  {
    return PropertyDependencyInjector.PropertyInjector<InjectDependencyAttribute>.GetInjectableProperties(instance) != null;
  }

  private static bool RequiresInjectionTypeLevel(object instance)
  {
    return PropertyDependencyInjector.PropertyInjector<InjectDependencyOnTypeLevelAttribute>.GetInjectableProperties(instance) != null;
  }
}
