// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Scopes.CounterScope
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.Common.Scopes;

public class CounterScope : BaseCounterScope
{
  private static string ScopeKey(string scopePosfix = null)
  {
    return !string.IsNullOrEmpty(scopePosfix) ? scopePosfix : nameof (CounterScope);
  }

  private static string ScopeKey(PXGraph graph, string scopePosfix = null)
  {
    return $"{CounterScope.ScopeKey(scopePosfix)}For{graph.UID?.ToString()}";
  }

  public new static bool IsEmpty(string scopePosfix = null)
  {
    return BaseCounterScope.IsEmpty(CounterScope.ScopeKey(scopePosfix));
  }

  public static bool IsEmpty(PXGraph graph, string scopePosfix = null)
  {
    return BaseCounterScope.IsEmpty(CounterScope.ScopeKey(graph, scopePosfix));
  }

  public CounterScope(string scopePosfix = null)
    : base(CounterScope.ScopeKey(scopePosfix))
  {
  }

  public CounterScope(PXGraph graph, string scopePosfix = null)
    : base(CounterScope.ScopeKey(graph, scopePosfix))
  {
  }
}
