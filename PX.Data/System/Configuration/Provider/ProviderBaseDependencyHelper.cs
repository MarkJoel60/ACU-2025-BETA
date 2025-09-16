// Decompiled with JetBrains decompiler
// Type: System.Configuration.Provider.ProviderBaseDependencyHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Builder;
using System.Collections.Generic;

#nullable enable
namespace System.Configuration.Provider;

internal static class ProviderBaseDependencyHelper
{
  private static readonly 
  #nullable disable
  Stack<IComponentContext> Contexts = new Stack<IComponentContext>();

  internal static void RegisterProvider<T>(this ContainerBuilder containerBuilder, Func<T> func) where T : ProviderBase
  {
    RegistrationExtensions.AutoActivate<T, SimpleActivatorData, SingleRegistrationStyle>(RegistrationExtensions.Register<T>(containerBuilder, (Func<IComponentContext, T>) (context =>
    {
      ProviderBaseDependencyHelper.Contexts.Push(context);
      try
      {
        return func();
      }
      finally
      {
        if (ProviderBaseDependencyHelper.Contexts.Pop() != context)
          throw new InvalidOperationException("Component context mismatch in ProviderBaseDependencyHelper");
      }
    })).As<T>()).SingleInstance();
  }

  internal static T Resolve<T>()
  {
    return ProviderBaseDependencyHelper.Contexts.Count != 0 ? ResolutionExtensions.Resolve<T>(ProviderBaseDependencyHelper.Contexts.Peek()) : throw new InvalidOperationException("Component context is not set, probably not running in a DI resolve context");
  }
}
