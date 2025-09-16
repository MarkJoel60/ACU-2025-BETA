// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRNotificationSourceList`3
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

public class CRNotificationSourceList<Source, SourceClass, NotificationType> : 
  EPDependNoteList<NotificationSource, NotificationSource.refNoteID, Source>
  where Source : class, IBqlTable
  where SourceClass : class, IBqlField
  where NotificationType : class, IBqlOperand
{
  protected readonly PXView _SourceView;
  protected readonly PXView _ClassView;

  public CRNotificationSourceList(PXGraph graph)
    : base(graph)
  {
    PXGraph pxGraph = graph;
    BqlCommand command = PXSelectBase<NotificationSource, PXViewOf<NotificationSource>.BasedOn<SelectFromBase<NotificationSource, TypeArrayOf<IFbqlJoin>.Empty>.Order<By<BqlField<NotificationSource.setupID_description, IBqlString>.Asc, BqlField<NotificationSource.nBranchID_description, IBqlString>.Asc>>>.Config>.GetCommand();
    CRNotificationSourceList<Source, SourceClass, NotificationType> notificationSourceList1 = this;
    // ISSUE: virtual method pointer
    PXSelectDelegate pxSelectDelegate = new PXSelectDelegate((object) notificationSourceList1, __vmethodptr(notificationSourceList1, NotificationSources));
    ((PXSelectBase) this).View = new PXView(pxGraph, false, command, (Delegate) pxSelectDelegate);
    this._SourceView = new PXView(graph, false, BqlCommand.CreateInstance(new System.Type[1]
    {
      BqlCommand.Compose(new System.Type[3]
      {
        typeof (Select<,>),
        typeof (NotificationSource),
        this.ComposeWhere
      })
    }));
    this._ClassView = new PXView(graph, true, BqlTemplate.OfCommand<SelectFromBase<NotificationSource, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<NotificationSetup>.On<BqlOperand<NotificationSetup.setupID, IBqlGuid>.IsEqual<NotificationSource.setupID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<NotificationSetup.sourceCD, Equal<BqlPlaceholder.Named<BqlPlaceholder.T>.AsField>>>>>.And<BqlOperand<NotificationSource.classID, IBqlString>.IsEqual<BqlField<BqlPlaceholder.C, BqlPlaceholder.IBqlAny>.AsOptional>>>>.Replace<BqlPlaceholder.T>(typeof (NotificationType)).Replace<BqlPlaceholder.C>(typeof (SourceClass)).ToCommand());
    PXUIFieldAttribute.SetVisible<NotificationSource.overrideSource>(((PXSelectBase) this).Cache, (object) null, true);
    PXUIFieldAttribute.SetVisibility<NotificationSource.overrideSource>(((PXSelectBase) this).Cache, (object) null, (PXUIVisibility) 3);
    PXGraph.RowPersistingEvents rowPersisting = graph.RowPersisting;
    CRNotificationSourceList<Source, SourceClass, NotificationType> notificationSourceList2 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting = new PXRowPersisting((object) notificationSourceList2, __vmethodptr(notificationSourceList2, OnRowPersisting));
    rowPersisting.AddHandler<NotificationSource>(pxRowPersisting);
    PXGraph.RowDeletingEvents rowDeleting = graph.RowDeleting;
    CRNotificationSourceList<Source, SourceClass, NotificationType> notificationSourceList3 = this;
    // ISSUE: virtual method pointer
    PXRowDeleting pxRowDeleting = new PXRowDeleting((object) notificationSourceList3, __vmethodptr(notificationSourceList3, OnRowDeleting));
    rowDeleting.AddHandler<NotificationSource>(pxRowDeleting);
    PXGraph.RowSelectedEvents rowSelected = graph.RowSelected;
    CRNotificationSourceList<Source, SourceClass, NotificationType> notificationSourceList4 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) notificationSourceList4, __vmethodptr(notificationSourceList4, OnRowSelected));
    rowSelected.AddHandler<NotificationSource>(pxRowSelected);
    PXGraph.FieldUpdatedEvents fieldUpdated = graph.FieldUpdated;
    CRNotificationSourceList<Source, SourceClass, NotificationType> notificationSourceList5 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) notificationSourceList5, __vmethodptr(notificationSourceList5, OnFieldUpdated_OverrideSource));
    fieldUpdated.AddHandler<NotificationSource.overrideSource>(pxFieldUpdated);
  }

  protected virtual IEnumerable NotificationSources()
  {
    List<NotificationSource> notificationSourceList = new List<NotificationSource>();
    foreach (NotificationSource notificationSource in this._SourceView.SelectMulti(Array.Empty<object>()))
      notificationSourceList.Add(notificationSource);
    if (((PXSelectBase) this)._Graph.IsCopyPasteContext)
      return (IEnumerable) notificationSourceList;
    foreach (NotificationSource classItem1 in this.GetClassItems())
    {
      NotificationSource classItem = classItem1;
      if (notificationSourceList.Find((Predicate<NotificationSource>) (i =>
      {
        Guid? setupId1 = i.SetupID;
        Guid? setupId2 = classItem.SetupID;
        if ((setupId1.HasValue == setupId2.HasValue ? (setupId1.HasValue ? (setupId1.GetValueOrDefault() == setupId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
          return false;
        int? nbranchId1 = i.NBranchID;
        int? nbranchId2 = classItem.NBranchID;
        return nbranchId1.GetValueOrDefault() == nbranchId2.GetValueOrDefault() & nbranchId1.HasValue == nbranchId2.HasValue;
      })) == null)
        notificationSourceList.Add(classItem);
    }
    return (IEnumerable) notificationSourceList;
  }

  protected virtual void OnRowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    NotificationSource row = (NotificationSource) e.Row;
    foreach (NotificationSource notificationSource in this.GetClassItems().Where<NotificationSource>((Func<NotificationSource, bool>) (classItem =>
    {
      Guid? setupId1 = classItem.SetupID;
      Guid? setupId2 = row.SetupID;
      if ((setupId1.HasValue == setupId2.HasValue ? (setupId1.HasValue ? (setupId1.GetValueOrDefault() == setupId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
        return false;
      int? nbranchId1 = classItem.NBranchID;
      int? nbranchId2 = row.NBranchID;
      return nbranchId1.GetValueOrDefault() == nbranchId2.GetValueOrDefault() & nbranchId1.HasValue == nbranchId2.HasValue;
    })))
    {
      if (!e.ExternalCall)
      {
        bool? overrideSource = row.OverrideSource;
        bool flag = false;
        if (!(overrideSource.GetValueOrDefault() == flag & overrideSource.HasValue))
          continue;
      }
      ((CancelEventArgs) e).Cancel = true;
      throw new PXRowPersistingException(typeof (NotificationSource).Name, (object) null, "The row cannot be deleted because it is inherited from the Mailing & Printing settings of the class.");
    }
    if (((CancelEventArgs) e).Cancel)
      return;
    ((PXSelectBase) this).View.RequestRefresh();
  }

  protected virtual void OnRowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    NotificationSource row = (NotificationSource) e.Row;
    if (row == null || row.ClassID == null)
      return;
    if (e.Operation == 3)
      ((CancelEventArgs) e).Cancel = true;
    if (e.Operation != 1)
      return;
    sender.SetStatus((object) row, (PXEntryStatus) 3);
    NotificationSource instance = (NotificationSource) sender.CreateInstance();
    instance.SetupID = row.SetupID;
    instance.NBranchID = row.NBranchID;
    NotificationSource notificationSource1 = GraphHelper.InitNewRow<NotificationSource>(sender, instance);
    NotificationSource copy1 = PXCache<NotificationSource>.CreateCopy(row);
    copy1.NBranchID = notificationSource1.NBranchID;
    copy1.SourceID = notificationSource1.SourceID;
    copy1.RefNoteID = notificationSource1.RefNoteID;
    copy1.ClassID = (string) null;
    NotificationSource notificationSource2 = (NotificationSource) sender.Update((object) copy1);
    if (notificationSource2 != null)
    {
      sender.PersistInserted((object) notificationSource2);
      sender.Normalize();
      sender.SetStatus((object) notificationSource2, (PXEntryStatus) 0);
      PXCache cach1 = sender.Graph.Caches[BqlCommand.GetItemType(this.SourceNoteID)];
      Guid? nullable = (Guid?) cach1.GetValue(cach1.Current, this.SourceNoteID.Name);
      if (nullable.HasValue)
      {
        PXCache cach2 = sender.Graph.Caches[typeof (NotificationRecipient)];
        foreach (PXResult<NotificationRecipient> pxResult in PXSelectBase<NotificationRecipient, PXSelect<NotificationRecipient, Where<NotificationRecipient.sourceID, Equal<Required<NotificationRecipient.sourceID>>, And<NotificationRecipient.refNoteID, Equal<Required<NotificationRecipient.refNoteID>>, And<NotificationRecipient.classID, IsNotNull>>>>.Config>.Select(sender.Graph, new object[2]
        {
          (object) row.SourceID,
          (object) nullable
        }))
        {
          NotificationRecipient notificationRecipient = PXResult<NotificationRecipient>.op_Implicit(pxResult);
          if (cach2.GetStatus((object) notificationRecipient) == 2)
          {
            NotificationRecipient copy2 = (NotificationRecipient) cach2.CreateCopy((object) notificationRecipient);
            copy2.SourceID = notificationSource2.SourceID;
            cach2.Update((object) copy2);
            cach2.PersistInserted((object) copy2);
          }
          if (cach2.GetStatus((object) notificationRecipient) != 1 && cach2.GetStatus((object) notificationRecipient) != 2)
          {
            NotificationRecipient copy3 = (NotificationRecipient) cach2.CreateCopy((object) notificationRecipient);
            copy3.SourceID = notificationSource2.SourceID;
            copy3.ClassID = (string) null;
            cach2.Update((object) copy3);
          }
        }
        cach2.Clear();
      }
    }
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void OnRowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    NotificationSource row = (NotificationSource) e.Row;
    bool flag = this.GetClassItems().Any<NotificationSource>((Func<NotificationSource, bool>) (cs =>
    {
      Guid? setupId1 = cs.SetupID;
      Guid? setupId2 = row.SetupID;
      if (setupId1.HasValue != setupId2.HasValue)
        return false;
      return !setupId1.HasValue || setupId1.GetValueOrDefault() == setupId2.GetValueOrDefault();
    }));
    row.OverrideSource = new bool?(flag && row.RefNoteID.HasValue);
  }

  public virtual void OnFieldUpdated_OverrideSource(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    NotificationSource row = (NotificationSource) e.Row;
    if (row == null || row.OverrideSource.GetValueOrDefault())
      return;
    row.OverrideSource = new bool?(true);
    cache.Delete((object) row);
  }

  private IEnumerable<NotificationSource> GetClassItems()
  {
    foreach (object obj in this._ClassView.SelectMulti(Array.Empty<object>()))
    {
      NotificationSource classItem = PXResult.Unwrap<NotificationSource>(obj);
      if (classItem != null)
        yield return classItem;
    }
  }
}
