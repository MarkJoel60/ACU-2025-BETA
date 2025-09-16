// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.PM.Descriptor.Attributes.CommitmentRefNoteAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.Common;
using PX.Objects.PM;
using PX.Objects.PO;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CN.Subcontracts.PM.Descriptor.Attributes;

public class CommitmentRefNoteAttribute : PXRefNoteBaseAttribute
{
  public virtual void FieldSelecting(PXCache cache, PXFieldSelectingEventArgs args)
  {
    using (new PXReadBranchRestrictedScope())
    {
      Guid? noteId = (Guid?) cache.GetValue(args.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
      args.ReturnValue = (object) this.GetDescription(cache.Graph, noteId);
    }
  }

  public virtual void CacheAttached(PXCache cache)
  {
    base.CacheAttached(cache);
    string str = $"{cache.GetItemType().Name}${((PXEventSubscriberAttribute) this)._FieldName}$Link";
    // ISSUE: method pointer
    cache.Graph.Actions[str] = (PXAction) Activator.CreateInstance(typeof (PXNamedAction<>).MakeGenericType(typeof (CommitmentInquiry.ProjectBalanceFilter)), (object) cache.Graph, (object) str, (object) new PXButtonDelegate((object) this, __methodptr(RedirectToRelatedScreen)), (object) CommitmentRefNoteAttribute.GetEventSubscriberAttributes());
  }

  private IEnumerable RedirectToRelatedScreen(PXAdapter adapter)
  {
    PXCache cach = adapter.View.Graph.Caches[typeof (PMCommitment)];
    if (cach.Current != null)
    {
      string fieldName = ((PXEventSubscriberAttribute) this)._FieldName;
      if (cach.GetValueExt(cach.Current, fieldName) is PXRefNoteBaseAttribute.PXLinkState valueExt)
      {
        this.helper.NavigateToRow(valueExt.target.FullName, valueExt.keys, (PXRedirectHelper.WindowMode) 1);
      }
      else
      {
        Guid? noteId = (Guid?) cach.GetValue(cach.Current, fieldName);
        Note note = this.helper.SelectNote(noteId);
        if (CommitmentRefNoteAttribute.IsSubcontract(adapter.View.Graph, note))
          this.RedirectToSubcontractScreen(noteId);
        this.helper.NavigateToRow(noteId, (PXRedirectHelper.WindowMode) 1);
      }
    }
    return adapter.Get();
  }

  private void RedirectToSubcontractScreen(Guid? noteId)
  {
    SubcontractEntry instance = PXGraph.CreateInstance<SubcontractEntry>();
    ((PXSelectBase<POOrder>) instance.Document).Current = (POOrder) this.helper.GetEntityRow(noteId);
    throw CommitmentRefNoteAttribute.GetRedirectException((PXGraph) instance);
  }

  private string GetDescription(PXGraph graph, Guid? noteId)
  {
    return this.helper.SelectNote(noteId) == null ? string.Empty : this.helper.GetEntityRowID(noteId, (string) null);
  }

  private static PXRedirectRequiredException GetRedirectException(PXGraph graph)
  {
    PXRedirectRequiredException redirectException = new PXRedirectRequiredException(graph, string.Empty);
    ((PXBaseRedirectException) redirectException).Mode = (PXBaseRedirectException.WindowMode) 3;
    return redirectException;
  }

  private static bool IsSubcontract(PXGraph graph, Note note)
  {
    return CommitmentRefNoteAttribute.GetPurchaseOrder(graph, note).OrderType == "RS";
  }

  private static PXEventSubscriberAttribute[] GetEventSubscriberAttributes()
  {
    return new PXEventSubscriberAttribute[1]
    {
      (PXEventSubscriberAttribute) new PXUIFieldAttribute()
      {
        MapEnableRights = (PXCacheRights) 1
      }
    };
  }

  private static POOrder GetPurchaseOrder(PXGraph graph, Note note)
  {
    PXSelect<POOrder, Where<POOrder.noteID, Equal<Required<Note.noteID>>>> pxSelect = new PXSelect<POOrder, Where<POOrder.noteID, Equal<Required<Note.noteID>>>>(graph);
    using (new PXReadBranchRestrictedScope())
      return ((PXSelectBase<POOrder>) pxSelect).SelectSingle(new object[1]
      {
        (object) note.NoteID
      });
  }
}
