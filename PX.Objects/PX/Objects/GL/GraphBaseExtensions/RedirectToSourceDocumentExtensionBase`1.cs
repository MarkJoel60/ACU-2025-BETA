// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GraphBaseExtensions.RedirectToSourceDocumentExtensionBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.GL.GraphBaseExtensions;

public class RedirectToSourceDocumentExtensionBase<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  public virtual IDocGraphCreator GetGraphCreator(string tranModule, string batchType)
  {
    if (tranModule != null && tranModule.Length == 2)
    {
      switch (tranModule[0])
      {
        case 'A':
          switch (tranModule)
          {
            case "AP":
              return (IDocGraphCreator) new APDocGraphCreator();
            case "AR":
              return (IDocGraphCreator) new ARDocGraphCreator();
          }
          break;
        case 'C':
          if (tranModule == "CA")
            return (IDocGraphCreator) new CADocGraphCreator();
          break;
        case 'D':
          if (tranModule == "DR")
            return (IDocGraphCreator) new DRDocGraphCreator();
          break;
        case 'F':
          if (tranModule == "FA")
            return (IDocGraphCreator) new FADocGraphCreator();
          break;
        case 'G':
          if (tranModule == "GL")
            return batchType == "T" ? (IDocGraphCreator) new JournalEntryImportGraphCreator() : (IDocGraphCreator) null;
          break;
        case 'I':
          if (tranModule == "IN")
            return (IDocGraphCreator) new INDocGraphCreator();
          break;
        case 'P':
          if (tranModule == "PM")
            return (IDocGraphCreator) new PMDocGraphCreator();
          break;
      }
    }
    return (IDocGraphCreator) null;
  }

  public virtual void RedirectToSourceDocument(GLTran tran, Batch batch)
  {
    if (tran.TranType == null)
      throw new PXException("The selected reference number is assigned to a journal entry. Click the link in the Batch Number column to open the batch that includes the entry.");
    PXGraph pxGraph = (this.GetGraphCreator(tran.Module, batch.BatchType) ?? throw new PXException("The selected reference number is assigned to a journal entry. Click the link in the Batch Number column to open the batch that includes the entry.")).Create(tran);
    if (pxGraph != null)
    {
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException(pxGraph, true, "View Source Document");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }
}
