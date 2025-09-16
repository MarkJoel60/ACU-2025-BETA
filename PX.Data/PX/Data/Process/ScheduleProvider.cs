// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.ScheduleProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Disposables;

#nullable disable
namespace PX.Data.Process;

internal class ScheduleProvider : IScheduleProvider
{
  private readonly Func<PXDatabaseProvider> _databaseProviderFactory;
  private const string DB_SLOT_KEY = "ScheduleProvider";

  public ScheduleProvider(Func<PXDatabaseProvider> databaseProviderFactory)
  {
    this._databaseProviderFactory = databaseProviderFactory ?? throw new ArgumentNullException(nameof (databaseProviderFactory));
  }

  public IEnumerable<AUSchedule> ActiveSchedules
  {
    get => this.Data?.ActiveSchedules ?? Enumerable.Empty<AUSchedule>();
  }

  public IEnumerable<AUSchedule> AllSchedules
  {
    get => this.Data?.AllSchedules ?? Enumerable.Empty<AUSchedule>();
  }

  public IDisposable Subscribe(System.Action<object> action, object state)
  {
    PXDatabaseProvider provider = this._databaseProviderFactory();
    provider.Subscribe(typeof (AUSchedule), new PXDatabaseTableChanged(handler));
    return Disposable.Create((System.Action) (() => provider.UnSubscribe(typeof (AUSchedule), new PXDatabaseTableChanged(handler))));

    void handler() => action(state);
  }

  private ScheduleProvider.Definition Data
  {
    get
    {
      return this._databaseProviderFactory()?.GetSlot<ScheduleProvider.Definition>(nameof (ScheduleProvider), (PrefetchDelegate<ScheduleProvider.Definition>) (() => new ScheduleProvider.Definition()), typeof (AUSchedule));
    }
  }

  internal class Definition
  {
    public IEnumerable<AUSchedule> ActiveSchedules { get; }

    public IEnumerable<AUSchedule> AllSchedules { get; }

    public Definition()
      : this((Func<IEnumerable<AUSchedule>>) (() =>
      {
        AUScheduleMaint instance = PXGraph.CreateInstance<AUScheduleMaint>();
        AUSchedule[] array = PXSelectBase<AUSchedule, PXSelect<AUSchedule>.Config>.Select((PXGraph) instance).Select<PXResult<AUSchedule>, AUSchedule>((Expression<Func<PXResult<AUSchedule>, AUSchedule>>) (item => (AUSchedule) item)).Where<AUSchedule>((Expression<Func<AUSchedule, bool>>) (item => item.ScheduleID > (int?) 0)).ToArray<AUSchedule>();
        instance.Clear(PXClearOption.ClearQueriesOnly);
        return (IEnumerable<AUSchedule>) array;
      }))
    {
    }

    internal Definition(Func<IEnumerable<AUSchedule>> getSchedules)
    {
      this.AllSchedules = getSchedules();
      IEnumerable<AUSchedule> allSchedules = this.AllSchedules;
      this.ActiveSchedules = allSchedules != null ? (IEnumerable<AUSchedule>) allSchedules.Where<AUSchedule>((Func<AUSchedule, bool>) (s =>
      {
        bool? isActive = s.IsActive;
        bool flag = true;
        return isActive.GetValueOrDefault() == flag & isActive.HasValue;
      })).OrderBy<AUSchedule, System.DateTime?>((Func<AUSchedule, System.DateTime?>) (s => s.NextRunDateTime)).ToArray<AUSchedule>() : (IEnumerable<AUSchedule>) (AUSchedule[]) null;
    }
  }
}
