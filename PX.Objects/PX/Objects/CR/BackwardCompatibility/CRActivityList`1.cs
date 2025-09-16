// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BackwardCompatibility.CRActivityList`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CR.BackwardCompatibility;

/// <exclude />
[Obsolete]
public class CRActivityList<TPrimaryView> : CRActivityListBase<TPrimaryView, CRPMTimeActivity> where TPrimaryView : class, IBqlTable, new()
{
  public CRActivityList(PXGraph graph)
    : base(graph)
  {
  }

  public CRActivityList(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public IEnumerable NewActivity(PXAdapter adapter, string type)
  {
    return this.NewActivityByType(adapter, type);
  }
}
