// Decompiled with JetBrains decompiler
// Type: PX.Data.CacheLogger
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

#nullable disable
namespace PX.Data;

public class CacheLogger
{
  public static bool IsEnabled;
  private Dictionary<System.Type, CacheLogger.CacheEntry> Items = new Dictionary<System.Type, CacheLogger.CacheEntry>();

  public void CacheRequested(System.Type t)
  {
    if (!CacheLogger.IsEnabled)
      return;
    StackTrace stackTrace = PXStackTrace.GetStackTrace(3);
    int hashCode = stackTrace.ToString().GetHashCode();
    CacheLogger.StackEntry stackEntry1 = new CacheLogger.StackEntry(stackTrace);
    if (stackEntry1.Class == "AddHandler" || stackEntry1.Method.StartsWith("PXGraph::HasException"))
      return;
    CacheLogger.CacheEntry cacheEntry;
    if (!this.Items.TryGetValue(t, out cacheEntry))
    {
      cacheEntry = new CacheLogger.CacheEntry() { T = t };
      this.Items.Add(t, cacheEntry);
    }
    CacheLogger.StackEntry stackEntry2;
    if (!cacheEntry.AccessStacks.TryGetValue(hashCode, out stackEntry2))
      cacheEntry.AccessStacks.Add(hashCode, stackEntry1);
    else
      ++stackEntry2.Cnt;
  }

  public void CacheCreated(System.Type t, double ms)
  {
    if (!CacheLogger.IsEnabled)
      return;
    CacheLogger.CacheEntry cacheEntry;
    if (!this.Items.TryGetValue(t, out cacheEntry))
    {
      cacheEntry = new CacheLogger.CacheEntry() { T = t };
      this.Items.Add(t, cacheEntry);
    }
    StackTrace stackTrace = PXStackTrace.GetStackTrace(3);
    cacheEntry.Created = stackTrace.ToString();
    MethodBase method1 = stackTrace.GetFrame(0).GetMethod();
    MethodBase method2 = stackTrace.GetFrame(1).GetMethod();
    cacheEntry.CreatedMethod = $"{method1.DeclaringType?.Name}::{method1.Name} << {method2.DeclaringType?.Name}::{method2.Name}";
    cacheEntry.CreateTime = (int) ms;
  }

  public void Report(PXGraph g)
  {
    if (!CacheLogger.IsEnabled)
      return;
    Dictionary<System.Type, CacheLogger.CacheEntry>.ValueCollection values = this.Items.Values;
    List<\u003C\u003Ef__AnonymousType35<System.Type, string, int>> list = values.OrderBy<CacheLogger.CacheEntry, int>((Func<CacheLogger.CacheEntry, int>) (cacheEntry => cacheEntry.DistinctMethods.Count<string>())).ThenByDescending<CacheLogger.CacheEntry, int>((Func<CacheLogger.CacheEntry, int>) (cacheEntry => cacheEntry.CreateTime)).SelectMany((Func<CacheLogger.CacheEntry, IEnumerable<string>>) (cacheEntry => cacheEntry.DistinctMethods), (cacheEntry, s) => new
    {
      T = cacheEntry.T,
      s = s,
      CreateTime = cacheEntry.CreateTime
    }).ToList();
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine($"{g.GetType().Name} empty:{values.Count<CacheLogger.CacheEntry>((Func<CacheLogger.CacheEntry, bool>) (_ => _.AccessStacks.Count > 0))} all:{values.Count} links:{values.Sum<CacheLogger.CacheEntry>((Func<CacheLogger.CacheEntry, int>) (_ => _.AccessStacks.Count))}");
    foreach (var data in list)
      ;
    foreach (IGrouping<string, \u003C\u003Ef__AnonymousType35<System.Type, string, int>> source in list.GroupBy(_ => StringExtensions.FirstSegment(_.s, '<')).OrderBy<IGrouping<string, \u003C\u003Ef__AnonymousType35<System.Type, string, int>>, int>(_ => _.Count()))
      stringBuilder.AppendLine($"{source.Key} {source.Count()}");
    PXTrace.WriteError(stringBuilder.ToString());
  }

  private class CacheEntry
  {
    public System.Type T;
    public readonly Dictionary<int, CacheLogger.StackEntry> AccessStacks = new Dictionary<int, CacheLogger.StackEntry>();
    public string Created;
    public string CreatedMethod;
    public int CreateTime;

    public IEnumerable<string> DistinctMethods
    {
      get
      {
        if (this.AccessStacks.Count > 0)
          return (IEnumerable<string>) this.AccessStacks.Values.Select<CacheLogger.StackEntry, string>((Func<CacheLogger.StackEntry, string>) (_ => _.Class)).Distinct<string>().ToList<string>();
        return (IEnumerable<string>) new string[1]
        {
          this.CreatedMethod
        };
      }
    }

    public override string ToString() => $"{this.T.Name}  {this.AccessStacks.Count}";
  }

  private class StackEntry
  {
    public int Cnt = 1;
    public string Stack;
    public string Class;
    public string Method;

    public StackEntry(StackTrace t)
    {
      this.Stack = t.ToString();
      int index = 0;
      MethodBase method1;
      for (method1 = t.GetFrame(index).GetMethod(); method1.Name == "get_Cache" || method1.Name == "get__Cache" || method1.Name == "RemoveHandler" || method1.Name == "CacheLazyLoad"; method1 = t.GetFrame(index).GetMethod())
        ++index;
      MethodBase method2 = t.GetFrame(index + 1).GetMethod();
      this.Method = $"{method1.DeclaringType?.Name}::{method1.Name} << {method2.DeclaringType?.Name}::{method2.Name}";
      this.Class = this.Method;
      try
      {
        if (!(t.GetFrame(0).GetMethod().Name == "AddHandler"))
          return;
        this.Class = "AddHandler";
      }
      catch
      {
      }
    }

    public override string ToString()
    {
      return this.Class != null ? $"{this.Cnt} {this.Class}" : $"{this.Cnt} {this.Stack}";
    }
  }
}
