// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRReminderList`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CR;

public class CRReminderList<TMasterActivity> : PXSelectBase where TMasterActivity : CRActivity, new()
{
  public CRReminderList(PXGraph graph)
  {
    this._Graph = graph;
    this.View = new PXView(graph, false, CRReminderList<TMasterActivity>.GenerateOriginalCommand());
    PXGraph.RowInsertedEvents rowInserted1 = graph.RowInserted;
    CRReminderList<TMasterActivity> crReminderList1 = this;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted1 = new PXRowInserted((object) crReminderList1, __vmethodptr(crReminderList1, Master_RowInserted));
    rowInserted1.AddHandler<TMasterActivity>(pxRowInserted1);
    PXGraph.FieldUpdatedEvents fieldUpdated1 = graph.FieldUpdated;
    System.Type type = typeof (TMasterActivity);
    string name = typeof (CRActivity.ownerID).Name;
    CRReminderList<TMasterActivity> crReminderList2 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) crReminderList2, __vmethodptr(crReminderList2, Master_OwnerID_FieldUpdated));
    fieldUpdated1.AddHandler(type, name, pxFieldUpdated1);
    PXGraph.RowSelectedEvents rowSelected = graph.RowSelected;
    CRReminderList<TMasterActivity> crReminderList3 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) crReminderList3, __vmethodptr(crReminderList3, CRReminder_RowSelected));
    rowSelected.AddHandler<CRReminder>(pxRowSelected);
    PXGraph.RowInsertingEvents rowInserting = graph.RowInserting;
    CRReminderList<TMasterActivity> crReminderList4 = this;
    // ISSUE: virtual method pointer
    PXRowInserting pxRowInserting = new PXRowInserting((object) crReminderList4, __vmethodptr(crReminderList4, CRReminder_RowInserting));
    rowInserting.AddHandler<CRReminder>(pxRowInserting);
    PXGraph.RowInsertedEvents rowInserted2 = graph.RowInserted;
    CRReminderList<TMasterActivity> crReminderList5 = this;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted2 = new PXRowInserted((object) crReminderList5, __vmethodptr(crReminderList5, CRReminder_RowInserted));
    rowInserted2.AddHandler<CRReminder>(pxRowInserted2);
    PXGraph.RowPersistingEvents rowPersisting = graph.RowPersisting;
    CRReminderList<TMasterActivity> crReminderList6 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting = new PXRowPersisting((object) crReminderList6, __vmethodptr(crReminderList6, CRReminder_RowPersisting));
    rowPersisting.AddHandler<CRReminder>(pxRowPersisting);
    PXGraph.RowUpdatedEvents rowUpdated = graph.RowUpdated;
    CRReminderList<TMasterActivity> crReminderList7 = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) crReminderList7, __vmethodptr(crReminderList7, CRReminder_RowUpdated));
    rowUpdated.AddHandler<CRReminder>(pxRowUpdated);
    PXGraph.FieldUpdatedEvents fieldUpdated2 = graph.FieldUpdated;
    CRReminderList<TMasterActivity> crReminderList8 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) crReminderList8, __vmethodptr(crReminderList8, CRReminder_IsReminderOn_FieldUpdated));
    fieldUpdated2.AddHandler<CRReminder.isReminderOn>(pxFieldUpdated2);
    PXUIFieldAttribute.SetEnabled<CRReminder.reminderDate>(this.MainCache, (object) null, false);
    GraphHelper.EnsureCachePersistence(graph, typeof (CRReminder));
  }

  public virtual object SelectSingle(params object[] parameters)
  {
    return this.View.SelectSingle(parameters);
  }

  protected virtual void Master_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    using (new ReadOnlyScope(new PXCache[1]
    {
      this.MainCache
    }))
      this.Current = (CRReminder) this.MainCache.Insert();
  }

  protected virtual void Master_OwnerID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    TMasterActivity row = e.Row as TMasterActivity;
    CRReminder current = this.Current;
    if ((object) row == null || current == null)
      return;
    current.Owner = row.OwnerID;
    this.MainCache.Update((object) current);
    if (cache.Graph.UnattendedMode || this.MainCache.GetStatus((object) current) == 2)
      return;
    Guid? createdById = row.CreatedByID;
    Guid? userId = PXAccess.GetUserID(row.OwnerID);
    bool flag = createdById.HasValue != userId.HasValue || createdById.HasValue && createdById.GetValueOrDefault() != userId.GetValueOrDefault();
    this.MainCache.SetValueExt<CRReminder.isReminderOn>((object) current, (object) flag);
  }

  protected virtual void CRReminder_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    CRReminder row = e.Row as CRReminder;
    TMasterActivity current = (TMasterActivity) this.MasterCache.Current;
    if (row == null || (object) current == null)
      return;
    if ((string) this.MasterCache.GetValueOriginal((object) current, typeof (CRActivity.uistatus).Name) != "CD")
    {
      PXUIFieldAttribute.SetEnabled<CRReminder.reminderDate>(cache, (object) row, row.IsReminderOn.GetValueOrDefault());
      PXUIFieldAttribute.SetVisible<CRReminder.remindAt>(cache, (object) row, row.IsReminderOn.GetValueOrDefault());
    }
    else
      PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
  }

  protected virtual void CRReminder_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    CRReminder row = e.Row as CRReminder;
    TMasterActivity current = (TMasterActivity) this.MasterCache.Current;
    if (row == null || (object) current == null)
      return;
    IEnumerator enumerator = cache.Deleted.GetEnumerator();
    if (!enumerator.MoveNext())
      return;
    row.NoteID = ((CRReminder) enumerator.Current).NoteID;
  }

  protected virtual void CRReminder_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    CRReminder row = e.Row as CRReminder;
    TMasterActivity current = (TMasterActivity) this.MasterCache.Current;
    if (row == null || (object) current == null)
      return;
    row.RefNoteID = current.NoteID;
    row.Owner = current.OwnerID;
  }

  protected virtual void CRReminder_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is CRReminder row))
      return;
    bool? isReminderOn = row.IsReminderOn;
    if (!isReminderOn.GetValueOrDefault() && e.Operation != 3)
      ((CancelEventArgs) e).Cancel = true;
    isReminderOn = row.IsReminderOn;
    if (!isReminderOn.GetValueOrDefault() || row.ReminderDate.HasValue)
      return;
    string displayName = PXUIFieldAttribute.GetDisplayName<CRReminder.reminderDate>(this.MainCache);
    PXSetPropertyException propertyException = new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) displayName
    });
    if (this.MainCache.RaiseExceptionHandling<CRReminder.reminderDate>((object) row, (object) null, (Exception) propertyException))
      throw new PXRowPersistingException(typeof (CRReminder.reminderDate).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) displayName
      });
  }

  protected virtual void CRReminder_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is CRReminder row))
      return;
    bool flag = cache.GetValueOriginal<CRReminder.noteID>((object) row) != null;
    if (!row.IsReminderOn.GetValueOrDefault())
    {
      if (!flag)
        cache.SetStatus((object) row, (PXEntryStatus) 4);
      else
        cache.SetStatus((object) row, (PXEntryStatus) 3);
    }
    else
    {
      if (cache.GetStatus((object) row) != 1 || flag)
        return;
      cache.SetStatus((object) row, (PXEntryStatus) 2);
    }
  }

  protected virtual void CRReminder_IsReminderOn_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    CRReminder row = e.Row as CRReminder;
    TMasterActivity current = (TMasterActivity) this.MasterCache.Current;
    if (row == null || (object) current == null)
      return;
    int? classId = current.ClassID;
    int num = 0;
    bool? isReminderOn;
    DateTime? reminderDate;
    DateTime? startDate;
    DateTime? modifiedDateTime;
    DateTime dateTime1;
    if (classId.GetValueOrDefault() == num & classId.HasValue)
    {
      isReminderOn = row.IsReminderOn;
      if (isReminderOn.GetValueOrDefault())
      {
        PXCache pxCache = cache;
        CRReminder crReminder = row;
        reminderDate = row.ReminderDate;
        DateTime dateTime2;
        if (!reminderDate.HasValue)
        {
          startDate = current.StartDate;
          ref DateTime? local1 = ref startDate;
          if (!local1.HasValue)
          {
            modifiedDateTime = row.LastModifiedDateTime;
            ref DateTime? local2 = ref modifiedDateTime;
            if (!local2.HasValue)
            {
              dateTime1 = PXTimeZoneInfo.Now;
              dateTime2 = dateTime1.AddMinutes(15.0);
            }
            else
              dateTime2 = local2.GetValueOrDefault().AddMinutes(15.0);
          }
          else
            dateTime2 = local1.GetValueOrDefault().AddMinutes(15.0);
        }
        else
          dateTime2 = reminderDate.GetValueOrDefault();
        // ISSUE: variable of a boxed type
        __Boxed<DateTime> local = (ValueType) dateTime2;
        pxCache.SetValue<CRReminder.reminderDate>((object) crReminder, (object) local);
      }
    }
    classId = current.ClassID;
    if (classId.GetValueOrDefault() != 1)
      return;
    isReminderOn = row.IsReminderOn;
    if (!isReminderOn.GetValueOrDefault())
      return;
    PXCache pxCache1 = cache;
    CRReminder crReminder1 = row;
    reminderDate = row.ReminderDate;
    DateTime? nullable;
    if (!reminderDate.HasValue)
    {
      startDate = current.StartDate;
      ref DateTime? local3 = ref startDate;
      if (!local3.HasValue)
      {
        modifiedDateTime = row.LastModifiedDateTime;
        ref DateTime? local4 = ref modifiedDateTime;
        if (!local4.HasValue)
        {
          nullable = new DateTime?();
        }
        else
        {
          dateTime1 = local4.GetValueOrDefault();
          nullable = new DateTime?(dateTime1.AddMinutes(15.0));
        }
      }
      else
      {
        dateTime1 = local3.GetValueOrDefault();
        nullable = new DateTime?(dateTime1.AddMinutes(-15.0));
      }
    }
    else
      nullable = reminderDate;
    // ISSUE: variable of a boxed type
    __Boxed<DateTime?> local5 = (ValueType) nullable;
    pxCache1.SetValue<CRReminder.reminderDate>((object) crReminder1, (object) local5);
  }

  public static BqlCommand GenerateOriginalCommand()
  {
    System.Type nestedType = typeof (CRReminder).GetNestedType(typeof (CRReminder.createdDateTime).Name);
    return BqlCommand.CreateInstance(new System.Type[10]
    {
      typeof (Select<,,>),
      typeof (CRReminder),
      typeof (Where<,>),
      typeof (CRReminder.refNoteID),
      typeof (Equal<>),
      typeof (PX.Data.Current<>),
      typeof (TMasterActivity).GetNestedType(typeof (CRActivity.noteID).Name),
      typeof (OrderBy<>),
      typeof (Desc<>),
      nestedType
    });
  }

  private PXCache MainCache => this._Graph.Caches[typeof (CRReminder)];

  private PXCache MasterCache => this._Graph.Caches[typeof (TMasterActivity)];

  public CRReminder Current
  {
    get => (CRReminder) this.View.Cache.Current;
    set => this.View.Cache.Current = (object) value;
  }
}
