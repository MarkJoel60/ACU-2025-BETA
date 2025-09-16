// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRCaseActivityHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CR;

public class CRCaseActivityHelper
{
  public static CRCaseActivityHelper Attach(PXGraph graph)
  {
    CRCaseActivityHelper caseActivityHelper1 = new CRCaseActivityHelper();
    PXGraph.RowInsertedEvents rowInserted = graph.RowInserted;
    CRCaseActivityHelper caseActivityHelper2 = caseActivityHelper1;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted = new PXRowInserted((object) caseActivityHelper2, __vmethodptr(caseActivityHelper2, ActivityRowInserted));
    rowInserted.AddHandler<PMTimeActivity>(pxRowInserted);
    PXGraph.RowSelectedEvents rowSelected = graph.RowSelected;
    CRCaseActivityHelper caseActivityHelper3 = caseActivityHelper1;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) caseActivityHelper3, __vmethodptr(caseActivityHelper3, ActivityRowSelected));
    rowSelected.AddHandler<PMTimeActivity>(pxRowSelected);
    PXGraph.RowUpdatedEvents rowUpdated = graph.RowUpdated;
    CRCaseActivityHelper caseActivityHelper4 = caseActivityHelper1;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) caseActivityHelper4, __vmethodptr(caseActivityHelper4, ActivityRowUpdated));
    rowUpdated.AddHandler<PMTimeActivity>(pxRowUpdated);
    return caseActivityHelper1;
  }

  protected virtual void ActivityRowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is PMTimeActivity row))
      return;
    CRCase crCase = this.GetCase(sender.Graph, (object) row.RefNoteID);
    if (crCase == null || !crCase.Released.GetValueOrDefault())
      return;
    row.IsBillable = new bool?(false);
  }

  protected virtual void ActivityRowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    PMTimeActivity row = e.Row as PMTimeActivity;
    PMTimeActivity oldRow = e.OldRow as PMTimeActivity;
    if (row == null || oldRow == null)
      return;
    CRCase crCase = this.GetCase(sender.Graph, (object) row.RefNoteID);
    if (crCase == null || !crCase.Released.GetValueOrDefault())
      return;
    row.IsBillable = new bool?(false);
  }

  protected virtual void ActivityRowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PMTimeActivity row) || !string.IsNullOrEmpty(row.TimeCardCD))
      return;
    CRCase crCase = this.GetCase(sender.Graph, (object) row.RefNoteID);
    if (crCase == null || !crCase.Released.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.isBillable>(sender, (object) row, false);
  }

  private CRCase GetCase(PXGraph graph, object refNoteID)
  {
    if (refNoteID == null)
      return (CRCase) null;
    return PXResultset<CRCase>.op_Implicit(PXSelectBase<CRCase, PXSelectJoin<CRCase, InnerJoin<CRActivityLink, On<CRActivityLink.refNoteID, Equal<CRCase.noteID>>>, Where<CRActivityLink.noteID, Equal<Required<PMTimeActivity.refNoteID>>>>.Config>.SelectWindowed(graph, 0, 1, new object[1]
    {
      refNoteID
    }));
  }
}
