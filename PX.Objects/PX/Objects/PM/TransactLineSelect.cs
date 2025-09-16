// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TransactLineSelect
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

[PXDynamicButton(new string[] {"TransactPasteLine", "TransactResetOrder"}, new string[] {"Paste Line", "Reset Order"}, TranslationKeyType = typeof (PX.Objects.Common.Messages))]
public class TransactLineSelect : 
  PXOrderedSelect<PMProforma, PMProformaTransactLine, Where<PMProformaTransactLine.refNbr, Equal<Current<PMProforma.refNbr>>, And<PMProformaTransactLine.revisionID, Equal<Current<PMProforma.revisionID>>, And<PMProformaTransactLine.type, Equal<PMProformaLineType.transaction>>>>, OrderBy<Asc<PMProformaLine.sortOrder, Asc<PMProformaTransactLine.lineNbr>>>>
{
  public const string TransactPasteLineCommand = "TransactPasteLine";
  public const string TransactResetOrderCommand = "TransactResetOrder";

  public TransactLineSelect(PXGraph graph)
    : base(graph)
  {
  }

  public TransactLineSelect(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  protected virtual void AddActions(PXGraph graph)
  {
    PXGraph pxGraph1 = graph;
    TransactLineSelect transactLineSelect1 = this;
    // ISSUE: virtual method pointer
    PXButtonDelegate pxButtonDelegate1 = new PXButtonDelegate((object) transactLineSelect1, __vmethodptr(transactLineSelect1, PasteLine));
    ((PXOrderedSelectBase<PMProforma, PMProformaTransactLine>) this).AddAction(pxGraph1, "TransactPasteLine", "Paste Line", pxButtonDelegate1, (List<PXEventSubscriberAttribute>) null);
    PXGraph pxGraph2 = graph;
    TransactLineSelect transactLineSelect2 = this;
    // ISSUE: virtual method pointer
    PXButtonDelegate pxButtonDelegate2 = new PXButtonDelegate((object) transactLineSelect2, __vmethodptr(transactLineSelect2, ResetOrder));
    ((PXOrderedSelectBase<PMProforma, PMProformaTransactLine>) this).AddAction(pxGraph2, "TransactResetOrder", "Reset Order", pxButtonDelegate2, (List<PXEventSubscriberAttribute>) null);
  }
}
