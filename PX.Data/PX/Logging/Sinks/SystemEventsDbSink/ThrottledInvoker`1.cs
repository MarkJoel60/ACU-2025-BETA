// Decompiled with JetBrains decompiler
// Type: PX.Logging.Sinks.SystemEventsDbSink.ThrottledInvoker`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Concurrent;

#nullable disable
namespace PX.Logging.Sinks.SystemEventsDbSink;

/// <summary>
/// Does not allow to invoke an action of a certain type more than specified times within a period of time.
/// </summary>
/// <typeparam name="T">Type whose values represent different kinds of throttled events</typeparam>
public static class ThrottledInvoker<T>
{
  private const int MaxInvocationsPerType = 5;
  /// <summary>
  /// Tuple's items meaning:
  /// Item1 - invocation counter (how many times the event type has been invoked so far);
  /// Item2 - DateTime of the last invocation;
  /// Item3 - if the event has already been throttled within the throttling period of time; to avoid resetting Item2.
  /// </summary>
  private static ConcurrentDictionary<T, Tuple<int, DateTime, bool>> InvocationCounter = new ConcurrentDictionary<T, Tuple<int, DateTime, bool>>();

  /// <summary>
  /// Invokes an action or throttles it if it has already been called <paramref name="maxInvocationCount" /> times within <paramref name="throttlingPeriod" />.
  /// </summary>
  /// <param name="eventType">Type of the event.</param>
  /// <param name="throttledAction">The throttled action.</param>
  /// <param name="actionWhenThrottlingOccurs">The action called when throttling occurs.</param>
  /// <param name="throttlingPeriod">Within this period only <paramref name="maxInvocationCount" /> of calls of throttled action is allowed.</param>
  /// <param name="maxInvocationCount">The maximum invocation count for the throttling period.</param>
  /// <returns><c>true</c>, if <paramref name="throttledAction" /> has been called,
  /// <c>false</c> otherwise (<paramref name="actionWhenThrottlingOccurs" /> has been called instead;
  /// or nothing happened in case <paramref name="actionWhenThrottlingOccurs" /> has already been called
  /// once within <paramref name="throttlingPeriod" />).</returns>
  public static bool Invoke(
    T eventType,
    Action throttledAction,
    Action actionWhenThrottlingOccurs,
    TimeSpan throttlingPeriod,
    int maxInvocationCount = 5)
  {
    Tuple<int, DateTime, bool> tuple = ThrottledInvoker<T>.InvocationCounter.AddOrUpdate(eventType, (Func<T, Tuple<int, DateTime, bool>>) (t => Tuple.Create<int, DateTime, bool>(1, DateTime.UtcNow, false)), (Func<T, Tuple<int, DateTime, bool>, Tuple<int, DateTime, bool>>) ((t, v) => Tuple.Create<int, DateTime, bool>(v.Item1 + 1, v.Item2, v.Item3)));
    if (tuple.Item1 > maxInvocationCount)
    {
      if (DateTime.UtcNow - tuple.Item2 > throttlingPeriod)
      {
        ThrottledInvoker<T>.InvocationCounter.AddOrUpdate(eventType, (Func<T, Tuple<int, DateTime, bool>>) (t => Tuple.Create<int, DateTime, bool>(1, DateTime.UtcNow, false)), (Func<T, Tuple<int, DateTime, bool>, Tuple<int, DateTime, bool>>) ((t, v) => Tuple.Create<int, DateTime, bool>(1, DateTime.UtcNow, false)));
        if (throttledAction != null)
          throttledAction();
        return true;
      }
      if (!tuple.Item3)
      {
        ThrottledInvoker<T>.InvocationCounter.AddOrUpdate(eventType, (Func<T, Tuple<int, DateTime, bool>>) (t => Tuple.Create<int, DateTime, bool>(1, DateTime.UtcNow, false)), (Func<T, Tuple<int, DateTime, bool>, Tuple<int, DateTime, bool>>) ((t, v) => Tuple.Create<int, DateTime, bool>(v.Item1, v.Item2, true)));
        if (actionWhenThrottlingOccurs != null)
          actionWhenThrottlingOccurs();
      }
      return false;
    }
    if (throttledAction != null)
      throttledAction();
    return true;
  }
}
