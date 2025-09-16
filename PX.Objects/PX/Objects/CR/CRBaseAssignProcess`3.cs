// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRBaseAssignProcess`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.EP;
using PX.Data.MassProcess;
using PX.Objects.EP;
using System;

#nullable disable
namespace PX.Objects.CR;

public abstract class CRBaseAssignProcess<TGraph, TPrimary, TAssignmentMapField> : 
  CRBaseMassProcess<TGraph, TPrimary>,
  IMassProcess<TPrimary>
  where TGraph : PXGraph, IMassProcess<TPrimary>, new()
  where TPrimary : class, IBqlTable, IAssign, new()
  where TAssignmentMapField : IBqlField
{
  protected EPAssignmentProcessor<TPrimary> processor;

  protected CRBaseAssignProcess()
  {
    this.processor = PXGraph.CreateInstance<EPAssignmentProcessor<TPrimary>>();
  }

  public override void ProccessItem(PXGraph graph, TPrimary item)
  {
    int? assignmentMapId = CRBaseAssignProcess<TGraph, TPrimary, TAssignmentMapField>.GetAssignmentMapId(graph);
    if (!assignmentMapId.HasValue)
      throw new PXException("Assignment Map ID is not specified.");
    PXCache cach = graph.Caches[typeof (TPrimary)];
    if (!cach.GetItemType().IsAssignableFrom(typeof (TPrimary)))
    {
      PXCache cache = graph.Views[graph.PrimaryView].Cache;
      PXCache instance = (PXCache) Activator.CreateInstance(typeof (PXCache<TPrimary>), (object) cache.Graph);
      object[] objArray = new object[cache.Keys.Count];
      string[] strArray = new string[cache.Keys.Count];
      for (int index = 0; index < cache.Keys.Count; ++index)
      {
        objArray[index] = instance.GetValue((object) item, cache.Keys[index]);
        strArray[index] = cache.Keys[index];
      }
      int num1 = 0;
      int num2 = 0;
      graph.Views[graph.PrimaryView].Select((object[]) null, (object[]) null, objArray, strArray, (bool[]) null, (PXFilterRow[]) null, ref num1, 1, ref num2);
      item = (TPrimary) graph.Views[graph.PrimaryView].Cache.Current;
    }
    TPrimary copy = (TPrimary) cach.CreateCopy((object) item);
    if (!this.processor.Assign(copy, assignmentMapId))
      throw new PXException("Unable to find route for assignment process.");
    cach.Update((object) copy);
  }

  private static int? GetAssignmentMapId(PXGraph graph)
  {
    BqlCommand search = (BqlCommand) Activator.CreateInstance(BqlCommand.Compose(new System.Type[2]
    {
      typeof (Search<>),
      typeof (TAssignmentMapField)
    }));
    PXView view = new PXView(graph, true, BqlCommand.CreateInstance(new System.Type[1]
    {
      search.GetSelectType()
    }));
    return view.SelectSingle(Array.Empty<object>()).With<object, int?>((Func<object, int?>) (_ => (int?) view.Cache.GetValue(_, ((IBqlSearch) search).GetField().Name)));
  }
}
