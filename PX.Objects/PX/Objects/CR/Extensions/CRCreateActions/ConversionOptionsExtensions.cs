// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateActions.ConversionOptionsExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Linq;
using System.Reactive.Disposables;

#nullable disable
namespace PX.Objects.CR.Extensions.CRCreateActions;

public static class ConversionOptionsExtensions
{
  public static IDisposable PreserveCachedRecords<TTargetGraph, TTarget>(
    this ConversionOptions<TTargetGraph, TTarget> options)
    where TTargetGraph : PXGraph, new()
    where TTarget : class, IBqlTable, INotable, new()
  {
    return options == null || options.PreserveCachedRecordsFilters == null || options.PreserveCachedRecordsFilters.Count == 0 ? Disposable.Empty : (IDisposable) new CompositeDisposable(options.PreserveCachedRecordsFilters.Select<ICRPreserveCachedRecordsFilter, IDisposable>((Func<ICRPreserveCachedRecordsFilter, IDisposable>) (f => f.PreserveCachedRecords())));
  }
}
