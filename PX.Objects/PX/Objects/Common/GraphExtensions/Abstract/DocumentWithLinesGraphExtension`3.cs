// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.DocumentWithLinesGraphExtension`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract;

public abstract class DocumentWithLinesGraphExtension<TGraph, TDocument, TDocumentMapping> : 
  PXGraphExtension<TGraph>,
  IDocumentWithFinDetailsGraphExtension
  where TGraph : PXGraph
  where TDocument : Document, new()
  where TDocumentMapping : IBqlMapping
{
  /// <summary>A mapping-based view of the <see cref="T:PX.Objects.Common.GraphExtensions.Abstract.DAC.Document" /> data.</summary>
  public PXSelectExtension<TDocument> Documents;
  /// <summary>A mapping-based view of the <see cref="T:PX.Objects.Common.GraphExtensions.Abstract.DAC.DocumentLine" /> data.</summary>
  public PXSelectExtension<DocumentLine> Lines;

  protected abstract TDocumentMapping GetDocumentMapping();

  protected abstract DocumentLineMapping GetDocumentLineMapping();

  public List<int?> GetOrganizationIDsInDetails()
  {
    return ((IEnumerable<PXResult<DocumentLine>>) ((PXSelectBase<DocumentLine>) this.Lines).Select(Array.Empty<object>())).AsEnumerable<PXResult<DocumentLine>>().Select<PXResult<DocumentLine>, int?>((Func<PXResult<DocumentLine>, int?>) (row => PXAccess.GetParentOrganizationID(PXResult<DocumentLine>.op_Implicit(row).BranchID))).Distinct<int?>().ToList<int?>();
  }

  protected virtual void _(Events.RowUpdated<TDocument> e)
  {
    if (!this.ShouldUpdateLinesOnDocumentUpdated(e))
      return;
    foreach (PXResult<DocumentLine> pxResult in ((PXSelectBase<DocumentLine>) this.Lines).Select(Array.Empty<object>()))
    {
      DocumentLine line = PXResult<DocumentLine>.op_Implicit(pxResult);
      this.ProcessLineOnDocumentUpdated(e, line);
      if (((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TDocument>>) e).Cache is PXModelExtension<DocumentLine> cache)
        cache.UpdateExtensionMapping((object) line);
      GraphHelper.MarkUpdated(((PXSelectBase) this.Lines).Cache, (object) line);
    }
  }

  protected virtual bool ShouldUpdatePeriodOnDocumentUpdated(Events.RowUpdated<TDocument> e)
  {
    return !((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<TDocument>>) e).Cache.ObjectsEqual<Document.headerTranPeriodID>((object) e.Row, (object) e.OldRow);
  }

  protected virtual bool ShouldUpdateDetailsOnDocumentUpdated(Events.RowUpdated<TDocument> e)
  {
    return this.ShouldUpdatePeriodOnDocumentUpdated(e);
  }

  protected virtual bool ShouldUpdateLinesOnDocumentUpdated(Events.RowUpdated<TDocument> e)
  {
    return this.ShouldUpdateDetailsOnDocumentUpdated(e);
  }

  protected virtual void ProcessLineOnDocumentUpdated(
    Events.RowUpdated<TDocument> e,
    DocumentLine line)
  {
    if (!this.ShouldUpdatePeriodOnDocumentUpdated(e))
      return;
    FinPeriodIDAttribute.DefaultPeriods<DocumentLine.finPeriodID>(((PXSelectBase) this.Lines).Cache, (object) line);
  }
}
