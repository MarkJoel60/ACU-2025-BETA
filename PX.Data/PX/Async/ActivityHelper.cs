// Decompiled with JetBrains decompiler
// Type: PX.Async.ActivityHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using PX.Diagnostics;
using System;
using System.Diagnostics;
using System.Reactive.Disposables;

#nullable enable
namespace PX.Async;

internal static class ActivityHelper
{
  internal static readonly ActivitySource ActivitySource = new ActivitySource("PX.Async", typeof (ActivityHelper).Assembly.GetName().Version.ToString());

  internal static ContainerBuilder EnableInstrumentation(this ContainerBuilder containerBuilder)
  {
    OptionsContainerBuilderExtensions.Configure<InstrumentationOptions>(containerBuilder, (Action<InstrumentationOptions>) (options => options.AddSource(ActivityHelper.ActivitySource.Name)));
    return containerBuilder;
  }

  internal static IDisposable ClearRootActivity(out Activity? previous)
  {
    previous = Activity.Current;
    if (previous == null)
      return Disposable.Empty;
    Activity.Current = (Activity) null;
    Activity localPrevious = previous;
    return Disposable.Create((Action) (() =>
    {
      if (localPrevious.IsStopped)
        return;
      Activity.Current = localPrevious;
    }));
  }
}
