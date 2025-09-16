// Decompiled with JetBrains decompiler
// Type: PX.Data.MassProcess.PXPrimaryGraphCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.MassProcess;

/// <exclude />
public class PXPrimaryGraphCollection
{
  private readonly PXGraph Graph;
  private readonly Dictionary<System.Type, PXGraph> Graphs = new Dictionary<System.Type, PXGraph>();

  public PXPrimaryGraphCollection(PXGraph graph) => this.Graph = graph;

  public PXGraph this[IBqlTable row]
  {
    get
    {
      PXCache cach = this.Graph.Caches[row.GetType()];
      object copy = cach.CreateCopy((object) row);
      System.Type graphType;
      PXPrimaryGraphAttribute.FindPrimaryGraph(cach, ref copy, out graphType);
      return this[graphType];
    }
  }

  public PXGraph this[System.Type graphType]
  {
    get
    {
      PXGraph pxGraph = (PXGraph) null;
      if (graphType != (System.Type) null && !this.Graphs.TryGetValue(graphType, out pxGraph))
      {
        pxGraph = PXGraph.CreateInstance(graphType);
        PXDBAttributeAttribute.Activate(pxGraph.Views[pxGraph.PrimaryView].Cache);
        this.Graphs[graphType] = pxGraph;
      }
      pxGraph?.Clear();
      return pxGraph;
    }
  }
}
