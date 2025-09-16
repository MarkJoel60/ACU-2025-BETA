// Decompiled with JetBrains decompiler
// Type: PX.Data.DependencyInjection.LifetimeScopeHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Core.Lifetime;
using CommonServiceLocator;
using PX.Common;
using PX.Common.Context;
using System;
using System.Reactive.Disposables;
using System.Web;

#nullable disable
namespace PX.Data.DependencyInjection;

[PXInternalUseOnly]
public static class LifetimeScopeHelper
{
  internal static object DefaultScopeTag => MatchingScopeLifetimeTags.RequestLifetimeScopeTag;

  [PXInternalUseOnly]
  public static IDisposable BeginLifetimeScope()
  {
    return (IDisposable) SlotStore.Instance.BeginLifetimeScope();
  }

  internal static ILifetimeScope BeginLifetimeScope(this ISlotStore slots)
  {
    return !ServiceLocator.IsLocationProviderSet ? (ILifetimeScope) null : slots.BeginLifetimeScope(ServiceLocator.Current.GetInstance<ILifetimeScope>());
  }

  internal static ILifetimeScope BeginLifetimeScope(this ISlotStore slots, ILifetimeScope parent)
  {
    ILifetimeScope scope = parent.BeginLifetimeScope(LifetimeScopeHelper.DefaultScopeTag);
    IDisposable disposable = slots.SetLifetimeScope(scope);
    scope.Disposer.AddInstanceForDisposal(disposable);
    return scope;
  }

  [PXInternalUseOnly]
  public static IDisposable SetLifetimeScope(this ISlotStore slots, ILifetimeScope scope)
  {
    TypeKeyedOperationExtensions.Set<ILifetimeScope>(slots, scope);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return Disposable.Create<ISlotStore>(slots, LifetimeScopeHelper.\u003C\u003EO.\u003C0\u003E__ClearLifetimeScope ?? (LifetimeScopeHelper.\u003C\u003EO.\u003C0\u003E__ClearLifetimeScope = new System.Action<ISlotStore>(LifetimeScopeHelper.ClearLifetimeScope)));
  }

  [PXInternalUseOnly]
  public static ILifetimeScope GetLifetimeScope(this ISlotStore slots)
  {
    return TypeKeyedOperationExtensions.Get<ILifetimeScope>(slots);
  }

  private static void ClearLifetimeScope(this ISlotStore slots)
  {
    TypeKeyedOperationExtensions.Remove<ILifetimeScope>(slots);
  }

  public static ILifetimeScope GetLifetimeScope() => SlotStore.Instance.GetLifetimeScope();

  public static ILifetimeScope GetLifetimeScope(this HttpContext httpContext)
  {
    return httpContext.Slots().GetLifetimeScope();
  }
}
