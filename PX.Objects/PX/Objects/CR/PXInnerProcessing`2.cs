// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PXInnerProcessing`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CR;

/// <exclude />
[PXInternalUseOnly]
public class PXInnerProcessing<TPrimary, TDetail> : PXProcessing<TPrimary>
  where TPrimary : class, IBqlTable, new()
  where TDetail : class, IBqlTable, new()
{
  public PXInnerProcessing(PXGraph graph)
    : base(graph)
  {
  }

  public PXInnerProcessing(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  protected virtual void _PrepareGraph<Table>() where Table : class, IBqlTable, new()
  {
    this.AttachActions<Table>();
    ((PXProcessingBase<TPrimary>) this).AttachBaseActions<Table>();
    this.SetProcessAllVisible(false);
    this.SetProcessVisible(false);
    this._ScheduleButton.SetVisible(false);
    ((PXProcessingBase<TPrimary>) this).SuppressMerge = true;
    ((PXProcessingBase<TPrimary>) this).SuppressUpdate = true;
  }

  [PXUIField(DisplayName = "Close")]
  [PXButton(DisplayOnMainToolbar = false)]
  protected virtual IEnumerable actionCloseProcessing(PXAdapter adapter)
  {
    PXInnerProcessing<TPrimary, TDetail> pxInnerProcessing = this;
    foreach (object obj in ((PXSelectBase) pxInnerProcessing)._Graph.Actions["Cancel"].Press(adapter))
    {
      PXRedirectHelper.TryRedirect(((PXSelectBase) pxInnerProcessing)._Graph, (PXRedirectHelper.WindowMode) 0);
      yield return obj;
    }
  }
}
