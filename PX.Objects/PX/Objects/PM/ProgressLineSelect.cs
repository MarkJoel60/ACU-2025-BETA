// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProgressLineSelect
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

[PXDynamicButton(new string[] {"ProgressPasteLine", "ProgressResetOrder"}, new string[] {"Paste Line", "Reset Order"}, TranslationKeyType = typeof (PX.Objects.Common.Messages))]
public class ProgressLineSelect : 
  PXOrderedSelect<PMProforma, PMProformaProgressLine, LeftJoin<PMRevenueBudget, On<PMProformaLine.projectID, Equal<PMRevenueBudget.projectID>, And<PMProformaProgressLine.taskID, Equal<PMRevenueBudget.projectTaskID>, And<PMProformaProgressLine.accountGroupID, Equal<PMRevenueBudget.accountGroupID>, And<PMProformaProgressLine.inventoryID, Equal<PMRevenueBudget.inventoryID>, And<PMProformaLine.costCodeID, Equal<PMRevenueBudget.costCodeID>>>>>>>, Where<PMProformaProgressLine.refNbr, Equal<Current<PMProforma.refNbr>>, And<PMProformaProgressLine.revisionID, Equal<Current<PMProforma.revisionID>>, And<PMProformaProgressLine.type, Equal<PMProformaLineType.progressive>>>>, OrderBy<Asc<PMProformaLine.sortOrder, Asc<PMProformaProgressLine.lineNbr>>>>
{
  public const string ProgressivePasteLineCommand = "ProgressPasteLine";
  public const string ProgressiveResetOrderCommand = "ProgressResetOrder";

  public ProgressLineSelect(PXGraph graph)
    : base(graph)
  {
  }

  public ProgressLineSelect(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  protected virtual void AddActions(PXGraph graph)
  {
    PXGraph pxGraph1 = graph;
    ProgressLineSelect progressLineSelect1 = this;
    // ISSUE: virtual method pointer
    PXButtonDelegate pxButtonDelegate1 = new PXButtonDelegate((object) progressLineSelect1, __vmethodptr(progressLineSelect1, PasteLine));
    ((PXOrderedSelectBase<PMProforma, PMProformaProgressLine>) this).AddAction(pxGraph1, "ProgressPasteLine", "Paste Line", pxButtonDelegate1, (List<PXEventSubscriberAttribute>) null);
    PXGraph pxGraph2 = graph;
    ProgressLineSelect progressLineSelect2 = this;
    // ISSUE: virtual method pointer
    PXButtonDelegate pxButtonDelegate2 = new PXButtonDelegate((object) progressLineSelect2, __vmethodptr(progressLineSelect2, ResetOrder));
    ((PXOrderedSelectBase<PMProforma, PMProformaProgressLine>) this).AddAction(pxGraph2, "ProgressResetOrder", "Reset Order", pxButtonDelegate2, (List<PXEventSubscriberAttribute>) null);
  }
}
