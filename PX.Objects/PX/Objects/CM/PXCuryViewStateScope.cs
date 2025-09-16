// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.PXCuryViewStateScope
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CM;

public sealed class PXCuryViewStateScope : IDisposable
{
  private readonly bool saveState;
  private readonly PXGraph graph;

  public PXCuryViewStateScope(PXGraph graph)
    : this(graph, false)
  {
  }

  public PXCuryViewStateScope(PXGraph graph, bool curyState)
  {
    this.graph = graph;
    this.saveState = graph.Accessinfo.CuryViewState;
    graph.Accessinfo.CuryViewState = curyState;
  }

  public void Dispose() => this.graph.Accessinfo.CuryViewState = this.saveState;
}
