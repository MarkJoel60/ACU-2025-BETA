// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Extensions.GraphExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Common.Extensions;

public static class GraphExtensions
{
  public static void RedirectToEntity(
    this PXGraph graph,
    object row,
    PXRedirectHelper.WindowMode windowMode)
  {
    if (row == null)
      return;
    PXRedirectHelper.TryRedirect(graph, row, windowMode);
  }

  public static TService GetService<TService>(this PXGraph graph)
  {
    return ((Func<PXGraph, TService>) ((IServiceProvider) ServiceLocator.Current).GetService(typeof (Func<PXGraph, TService>)))(graph);
  }

  public static bool HasErrors(this PXGraph graph)
  {
    return graph.Views.Caches.Where<Type>((Func<Type, bool>) (key => ((Dictionary<Type, PXCache>) graph.Caches).ContainsKey(key))).SelectMany<Type, PXEventSubscriberAttribute>((Func<Type, IEnumerable<PXEventSubscriberAttribute>>) (key => (IEnumerable<PXEventSubscriberAttribute>) graph.Caches[key].GetAttributes((string) null))).Any<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (a => a is IPXInterfaceField ipxInterfaceField && EnumerableExtensions.IsIn<PXErrorLevel>(ipxInterfaceField.ErrorLevel, (PXErrorLevel) 4, (PXErrorLevel) 5)));
  }
}
