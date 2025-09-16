// Decompiled with JetBrains decompiler
// Type: PX.Data.PXGraphScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class PXGraphScope : IDisposable
{
  private const string _SLOT_KEY = "__GRAPH_SLOT__";
  private readonly PXGraph _graph;
  private readonly PXGraphScope _previous;
  private bool _disposed;

  private PXGraphScope(PXGraph graph, PXGraphScope prev)
  {
    this._graph = graph;
    this._previous = prev;
  }

  public static PXGraphScope Instantiate(PXGraph graph)
  {
    PXGraphScope pxGraphScope = graph != null ? new PXGraphScope(graph, PXGraphScope.CurrentScope) : throw new ArgumentNullException(nameof (graph));
    PXContext.SetSlot<PXGraphScope>("__GRAPH_SLOT__", pxGraphScope);
    return pxGraphScope;
  }

  public static PXGraph Graph
  {
    get
    {
      return PXGraphScope.CurrentScope.With<PXGraphScope, PXGraph>((Func<PXGraphScope, PXGraph>) (_ => _._graph));
    }
  }

  private static PXGraphScope CurrentScope => PXContext.GetSlot<PXGraphScope>("__GRAPH_SLOT__");

  public void Dispose()
  {
    if (this._disposed)
      return;
    PXContext.SetSlot<PXGraphScope>("__GRAPH_SLOT__", this._previous);
    this._disposed = true;
  }
}
