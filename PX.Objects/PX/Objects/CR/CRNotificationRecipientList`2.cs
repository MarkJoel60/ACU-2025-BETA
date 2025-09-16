// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRNotificationRecipientList`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.EP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.CR;

public class CRNotificationRecipientList<Source, SourceClassID> : 
  EPDependNoteList<NotificationRecipient, NotificationRecipient.refNoteID, Source>
  where Source : class, IBqlTable
  where SourceClassID : class, IBqlField
{
  protected readonly PXView _SourceView;
  protected readonly PXView _ClassView;
  private bool internalDelete;

  public CRNotificationRecipientList(PXGraph graph)
    : base(graph)
  {
    PXGraph pxGraph = graph;
    BqlCommand command = PXSelectBase<NotificationRecipient, PXViewOf<NotificationRecipient>.BasedOn<SelectFromBase<NotificationRecipient, TypeArrayOf<IFbqlJoin>.Empty>.Order<By<BqlField<NotificationRecipient.orderID, IBqlInt>.Asc>>>.Config>.GetCommand();
    CRNotificationRecipientList<Source, SourceClassID> notificationRecipientList1 = this;
    // ISSUE: virtual method pointer
    PXSelectDelegate pxSelectDelegate = new PXSelectDelegate((object) notificationRecipientList1, __vmethodptr(notificationRecipientList1, NotificationRecipients));
    ((PXSelectBase) this).View = new PXView(pxGraph, false, command, (Delegate) pxSelectDelegate);
    this._SourceView = new PXView(graph, false, BqlTemplate.OfCommand<SelectFromBase<NotificationRecipient, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<NotificationRecipient.sourceID, Equal<BqlField<NotificationSource.sourceID, IBqlInt>.AsOptional>>>>>.And<BqlOperand<NotificationRecipient.refNoteID, IBqlGuid>.IsEqual<BqlField<BqlPlaceholder.N, BqlPlaceholder.IBqlAny>.FromCurrent>>>>.Replace<BqlPlaceholder.N>(this.SourceNoteID).ToCommand());
    this._ClassView = new PXView(graph, true, BqlTemplate.OfCommand<SelectFromBase<NotificationRecipient, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<NotificationRecipient.classID, Equal<BqlField<BqlPlaceholder.A, BqlPlaceholder.IBqlAny>.FromCurrent>>>>, And<BqlOperand<NotificationRecipient.setupID, IBqlGuid>.IsEqual<BqlField<NotificationSource.setupID, IBqlGuid>.FromCurrent>>>>.And<BqlOperand<NotificationRecipient.refNoteID, IBqlGuid>.IsNull>>>.Replace<BqlPlaceholder.A>(typeof (SourceClassID)).ToCommand());
    PXGraph.RowPersistingEvents rowPersisting = graph.RowPersisting;
    CRNotificationRecipientList<Source, SourceClassID> notificationRecipientList2 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting = new PXRowPersisting((object) notificationRecipientList2, __vmethodptr(notificationRecipientList2, OnRowPersisting));
    rowPersisting.AddHandler<NotificationRecipient>(pxRowPersisting);
    PXGraph.RowSelectedEvents rowSelected = graph.RowSelected;
    CRNotificationRecipientList<Source, SourceClassID> notificationRecipientList3 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) notificationRecipientList3, __vmethodptr(notificationRecipientList3, OnRowSeleted));
    rowSelected.AddHandler<NotificationRecipient>(pxRowSelected);
    PXGraph.RowDeletingEvents rowDeleting = graph.RowDeleting;
    CRNotificationRecipientList<Source, SourceClassID> notificationRecipientList4 = this;
    // ISSUE: virtual method pointer
    PXRowDeleting pxRowDeleting = new PXRowDeleting((object) notificationRecipientList4, __vmethodptr(notificationRecipientList4, OnRowDeleting));
    rowDeleting.AddHandler<NotificationRecipient>(pxRowDeleting);
    PXGraph.RowInsertingEvents rowInserting = graph.RowInserting;
    CRNotificationRecipientList<Source, SourceClassID> notificationRecipientList5 = this;
    // ISSUE: virtual method pointer
    PXRowInserting pxRowInserting = new PXRowInserting((object) notificationRecipientList5, __vmethodptr(notificationRecipientList5, OnRowInserting));
    rowInserting.AddHandler<NotificationRecipient>(pxRowInserting);
    PXGraph.FieldUpdatedEvents fieldUpdated = graph.FieldUpdated;
    CRNotificationRecipientList<Source, SourceClassID> notificationRecipientList6 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) notificationRecipientList6, __vmethodptr(notificationRecipientList6, Source_OnFieldUpdated_OverrideSource));
    fieldUpdated.AddHandler<NotificationSource.overrideSource>(pxFieldUpdated);
  }

  protected virtual IEnumerable NotificationRecipients()
  {
    List<NotificationRecipient> notificationRecipientList = new List<NotificationRecipient>();
    foreach (NotificationRecipient notificationRecipient in this._SourceView.SelectMulti(Array.Empty<object>()))
    {
      notificationRecipient.OrderID = notificationRecipient.NotificationID;
      notificationRecipientList.Add(notificationRecipient);
    }
    foreach (NotificationRecipient classItem1 in this.GetClassItems())
    {
      NotificationRecipient classItem = classItem1;
      NotificationRecipient notificationRecipient1 = notificationRecipientList.Find((Predicate<NotificationRecipient>) (i =>
      {
        if (!(i.ContactType == classItem.ContactType))
          return false;
        int? contactId1 = i.ContactID;
        int? contactId2 = classItem.ContactID;
        return contactId1.GetValueOrDefault() == contactId2.GetValueOrDefault() & contactId1.HasValue == contactId2.HasValue;
      }));
      if (notificationRecipient1 == null)
      {
        notificationRecipient1 = classItem;
        notificationRecipientList.Add(notificationRecipient1);
      }
      NotificationRecipient notificationRecipient2 = notificationRecipient1;
      int? notificationId = classItem.NotificationID;
      int? nullable = notificationId.HasValue ? new int?(int.MinValue + notificationId.GetValueOrDefault()) : new int?();
      notificationRecipient2.OrderID = nullable;
    }
    return (IEnumerable) notificationRecipientList;
  }

  protected override void Source_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    sender.Current = e.Row;
    this.internalDelete = true;
    try
    {
      foreach (NotificationRecipient notificationRecipient in this._SourceView.SelectMulti(Array.Empty<object>()))
        this._SourceView.Cache.Delete((object) notificationRecipient);
    }
    finally
    {
      this.internalDelete = false;
    }
  }

  protected virtual void OnRowSeleted(PXCache sender, PXRowSelectedEventArgs e)
  {
    NotificationRecipient row = (NotificationRecipient) e.Row;
    if (row == null)
      return;
    bool flag1 = row.ContactType == "C" || row.ContactType == "E";
    bool flag2 = !this.GetClassItems().Any<NotificationRecipient>((Func<NotificationRecipient, bool>) (classItem =>
    {
      if (!(row.ContactType == classItem.ContactType))
        return false;
      int? contactId1 = row.ContactID;
      int? contactId2 = classItem.ContactID;
      return contactId1.GetValueOrDefault() == contactId2.GetValueOrDefault() & contactId1.HasValue == contactId2.HasValue;
    }));
    PXUIFieldAttribute.SetEnabled(sender, (object) row, typeof (NotificationRecipient.contactID).Name, flag2 & flag1);
    PXUIFieldAttribute.SetEnabled(sender, (object) row, typeof (NotificationRecipient.contactType).Name, flag2);
  }

  protected virtual void OnRowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    NotificationRecipient row = (NotificationRecipient) e.Row;
    if (e.Operation == 2)
    {
      NotificationSource notificationSource = PXResultset<NotificationSource>.op_Implicit(PXSelectBase<NotificationSource, PXSelectReadonly<NotificationSource, Where<NotificationSource.setupID, Equal<Required<NotificationSource.setupID>>, And<NotificationSource.refNoteID, Equal<Required<NotificationSource.refNoteID>>>>>.Config>.Select(sender.Graph, new object[2]
      {
        (object) row.SetupID,
        (object) row.RefNoteID
      }));
      if (notificationSource != null)
      {
        if (!((PXSelectBase) this)._Graph.IsImport && sender.Graph.Caches[typeof (NotificationSource)].GetStatus((object) notificationSource) == 1)
          ((CancelEventArgs) e).Cancel = true;
      }
      else
        ((CancelEventArgs) e).Cancel = true;
    }
    if (!row.RefNoteID.HasValue)
    {
      if (e.Operation != 1)
        return;
      sender.Remove((object) row);
      NotificationRecipient notificationRecipient1 = (NotificationRecipient) sender.Insert();
      NotificationRecipient copy = PXCache<NotificationRecipient>.CreateCopy(row);
      copy.NotificationID = notificationRecipient1.NotificationID;
      copy.RefNoteID = notificationRecipient1.RefNoteID;
      copy.ClassID = (string) null;
      NotificationSource notificationSource = PXResultset<NotificationSource>.op_Implicit(PXSelectBase<NotificationSource, PXSelectReadonly<NotificationSource, Where<NotificationSource.setupID, Equal<Required<NotificationSource.setupID>>, And<NotificationSource.refNoteID, Equal<Required<NotificationSource.refNoteID>>>>>.Config>.Select(sender.Graph, new object[2]
      {
        (object) row.SetupID,
        (object) row.RefNoteID
      }));
      if (notificationSource != null)
        copy.SourceID = notificationSource.SourceID;
      NotificationRecipient notificationRecipient2 = (NotificationRecipient) sender.Update((object) copy);
      if (notificationRecipient2 != null)
      {
        sender.PersistInserted((object) notificationRecipient2);
        sender.Normalize();
        sender.SetStatus((object) notificationRecipient2, (PXEntryStatus) 0);
      }
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      PXCache<NotificationSource> pxCache = GraphHelper.Caches<NotificationSource>(sender.Graph);
      NotificationSource notificationSource = PXResultset<NotificationSource>.op_Implicit(PXSelectBase<NotificationSource, PXViewOf<NotificationSource>.BasedOn<SelectFromBase<NotificationSource, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<NotificationSource.sourceID, Equal<P.AsInt>>>>>.And<BqlOperand<NotificationSource.refNoteID, IBqlGuid>.IsNull>>>.Config>.SelectSingleBound(sender.Graph, (object[]) null, new object[1]
      {
        (object) row.SourceID
      }));
      if (!EnumerableExtensions.IsIn<PXEntryStatus>(pxCache.GetStatus(notificationSource), (PXEntryStatus) 0, (PXEntryStatus) 5))
        return;
      ((PXCache) pxCache).SetStatus((object) notificationSource, (PXEntryStatus) 1);
    }
  }

  protected virtual void OnRowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (this.internalDelete)
      return;
    NotificationRecipient row = (NotificationRecipient) e.Row;
    foreach (NotificationRecipient classItem in this.GetClassItems())
    {
      Guid? setupId = classItem.SetupID;
      Guid? nullable = row.SetupID;
      if ((setupId.HasValue == nullable.HasValue ? (setupId.HasValue ? (setupId.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && classItem.ContactType == row.ContactType)
      {
        int? contactId1 = classItem.ContactID;
        int? contactId2 = row.ContactID;
        if (contactId1.GetValueOrDefault() == contactId2.GetValueOrDefault() & contactId1.HasValue == contactId2.HasValue)
        {
          nullable = row.RefNoteID;
          if (!nullable.HasValue)
          {
            ((CancelEventArgs) e).Cancel = true;
            throw new PXRowPersistingException(typeof (NotificationRecipient).Name, (object) null, "The row cannot be deleted because it is inherited from the Mailing & Printing settings of the class.");
          }
        }
      }
    }
    if (((CancelEventArgs) e).Cancel)
      return;
    ((PXSelectBase) this).View.RequestRefresh();
  }

  protected virtual void OnRowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (e.Row == null)
      return;
    ((NotificationRecipient) e.Row).ClassID = ((NotificationSource) sender.Graph.Caches[typeof (NotificationSource)].Current)?.ClassID;
  }

  public virtual void Source_OnFieldUpdated_OverrideSource(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    NotificationSource row = (NotificationSource) e.Row;
    if (row == null || row.OverrideSource.GetValueOrDefault())
      return;
    foreach (object obj in this._SourceView.SelectMulti(Array.Empty<object>()))
      ((PXSelectBase) this).Cache.Delete(obj);
  }

  private IEnumerable<NotificationRecipient> GetClassItems()
  {
    foreach (object obj in this._ClassView.SelectMulti(Array.Empty<object>()))
    {
      NotificationRecipient classItem = PXResult.Unwrap<NotificationRecipient>(obj);
      if (classItem != null)
        yield return classItem;
    }
  }
}
