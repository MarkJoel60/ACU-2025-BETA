// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.IScheduleProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Process;

internal interface IScheduleProvider
{
  /// <summary>Returns active schedules only</summary>
  IEnumerable<AUSchedule> ActiveSchedules { get; }

  /// <summary>Returns all schedules despite its activity</summary>
  IEnumerable<AUSchedule> AllSchedules { get; }

  /// <summary>
  /// Subscribes an <param name="action">action</param> to schedule modifications.
  /// </summary>
  /// <returns>Object that must be disposed when subscription is no longer needed.</returns>
  IDisposable Subscribe(System.Action<object> action, object state);
}
