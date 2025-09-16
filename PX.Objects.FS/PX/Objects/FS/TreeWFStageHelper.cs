// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.TreeWFStageHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.FS;

public static class TreeWFStageHelper
{
  public static IEnumerable treeWFStages(PXGraph graph, string srvOrdType, int? wFStageID)
  {
    if (!wFStageID.HasValue)
      wFStageID = new int?(0);
    PXGraph pxGraph = graph;
    object[] objArray = new object[2]
    {
      (object) srvOrdType,
      (object) wFStageID
    };
    foreach (PXResult<FSWFStage> pxResult in PXSelectBase<FSWFStage, PXSelectJoin<FSWFStage, InnerJoin<FSSrvOrdType, On<FSSrvOrdType.srvOrdTypeID, Equal<FSWFStage.wFID>>>, Where<FSSrvOrdType.srvOrdType, Equal<Required<FSSrvOrdType.srvOrdType>>, And<FSWFStage.parentWFStageID, Equal<Required<FSWFStage.parentWFStageID>>>>, OrderBy<Asc<FSWFStage.sortOrder>>>.Config>.Select(pxGraph, objArray))
      yield return (object) PXResult<FSWFStage>.op_Implicit(pxResult);
  }

  public class TreeWFStageView : PXSelectOrderBy<FSWFStage, OrderBy<Asc<FSWFStage.sortOrder>>>
  {
    public TreeWFStageView(PXGraph graph)
      : base(graph)
    {
    }

    public TreeWFStageView(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
    }
  }
}
