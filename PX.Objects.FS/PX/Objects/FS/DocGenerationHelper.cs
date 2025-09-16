// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.DocGenerationHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.FS;

public static class DocGenerationHelper
{
  public static void ValidatePostBatchStatus<TDocument>(
    PXGraph graph,
    PXDBOperation dbOperation,
    string postTo,
    string createdDocType,
    string createdRefNbr)
    where TDocument : class, IBqlTable, new()
  {
    if (dbOperation != 1 || !(graph.Accessinfo.ScreenID != SharedFunctions.SetScreenIDToDotFormat("FS500100")) || !(graph.Accessinfo.ScreenID != SharedFunctions.SetScreenIDToDotFormat("FS500600")))
      return;
    if (PXResultset<FSCreatedDoc>.op_Implicit(PXSelectBase<FSCreatedDoc, PXSelectJoin<FSCreatedDoc, InnerJoin<FSPostBatch, On<FSCreatedDoc.batchID, Equal<FSPostBatch.batchID>>>, Where<FSPostBatch.status, Equal<FSPostBatch.status.temporary>, And<FSPostBatch.postTo, Equal<Required<FSPostBatch.postTo>>, And<FSCreatedDoc.createdDocType, Equal<Required<FSCreatedDoc.createdDocType>>, And<FSCreatedDoc.createdRefNbr, Equal<Required<FSCreatedDoc.createdRefNbr>>>>>>>.Config>.Select(graph, new object[3]
    {
      (object) postTo,
      (object) createdDocType,
      (object) createdRefNbr
    })) != null)
    {
      PXProcessing<TDocument>.SetError("The document cannot be updated because the batch has the Temporary status.");
      throw new PXException("The document cannot be updated because the batch has the Temporary status.");
    }
  }

  public static void ValidateAutoNumbering(PXGraph graph, string numberingID)
  {
    Numbering numbering = (Numbering) null;
    if (numberingID != null)
      numbering = PXResultset<Numbering>.op_Implicit(PXSelectBase<Numbering, PXSelect<Numbering, Where<Numbering.numberingID, Equal<Required<Numbering.numberingID>>>>.Config>.Select(graph, new object[1]
      {
        (object) numberingID
      }));
    if (numbering == null)
      throw new PXSetPropertyException("Numbering ID is null.");
    if (numbering.UserNumbering.GetValueOrDefault())
      throw new PXSetPropertyException("Cannot generate the next number. Manual Numbering is activated for '{0}'", new object[1]
      {
        (object) numbering.NumberingID
      });
  }
}
