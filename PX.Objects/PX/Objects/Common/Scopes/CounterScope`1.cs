// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Scopes.CounterScope`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.Common.Scopes;

public abstract class CounterScope<TSelf> : CounterScope where TSelf : CounterScope<TSelf>
{
  public CounterScope(PXGraph graph)
    : base(graph, typeof (TSelf).FullName)
  {
  }

  public static bool IsEmpty(PXGraph graph) => CounterScope.IsEmpty(graph, typeof (TSelf).FullName);

  public static bool Suppressed(PXGraph graph) => !CounterScope<TSelf>.IsEmpty(graph);
}
