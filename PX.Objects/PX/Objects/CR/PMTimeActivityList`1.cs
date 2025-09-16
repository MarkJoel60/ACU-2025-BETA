// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PMTimeActivityList`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.EP;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CR;

public class PMTimeActivityList<TMasterActivity> : PXSelectBase<PMTimeActivity> where TMasterActivity : CRActivity, new()
{
  private static readonly EPSetup EmptyEpSetup = new EPSetup();

  public PMTimeActivityList(PXGraph graph)
  {
    ((PXSelectBase) this)._Graph = graph;
    PXGraph.RowSelectedEvents rowSelected = graph.RowSelected;
    System.Type type1 = typeof (PMTimeActivity);
    PMTimeActivityList<TMasterActivity> timeActivityList1 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) timeActivityList1, __vmethodptr(timeActivityList1, PMTimeActivity_RowSelected));
    rowSelected.AddHandler(type1, pxRowSelected);
    PXGraph.RowDeletingEvents rowDeleting = graph.RowDeleting;
    System.Type type2 = typeof (PMTimeActivity);
    PMTimeActivityList<TMasterActivity> timeActivityList2 = this;
    // ISSUE: virtual method pointer
    PXRowDeleting pxRowDeleting = new PXRowDeleting((object) timeActivityList2, __vmethodptr(timeActivityList2, PMTimeActivity_RowDeleting));
    rowDeleting.AddHandler(type2, pxRowDeleting);
    PXGraph.RowInsertedEvents rowInserted1 = graph.RowInserted;
    System.Type type3 = typeof (PMTimeActivity);
    PMTimeActivityList<TMasterActivity> timeActivityList3 = this;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted1 = new PXRowInserted((object) timeActivityList3, __vmethodptr(timeActivityList3, PMTimeActivity_RowInserted));
    rowInserted1.AddHandler(type3, pxRowInserted1);
    PXGraph.RowInsertingEvents rowInserting = graph.RowInserting;
    System.Type type4 = typeof (PMTimeActivity);
    PMTimeActivityList<TMasterActivity> timeActivityList4 = this;
    // ISSUE: virtual method pointer
    PXRowInserting pxRowInserting = new PXRowInserting((object) timeActivityList4, __vmethodptr(timeActivityList4, PMTimeActivity_RowInserting));
    rowInserting.AddHandler(type4, pxRowInserting);
    PXGraph.RowPersistingEvents rowPersisting1 = graph.RowPersisting;
    System.Type type5 = typeof (PMTimeActivity);
    PMTimeActivityList<TMasterActivity> timeActivityList5 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting1 = new PXRowPersisting((object) timeActivityList5, __vmethodptr(timeActivityList5, PMTimeActivity_RowPersisting));
    rowPersisting1.AddHandler(type5, pxRowPersisting1);
    PXGraph.RowUpdatedEvents rowUpdated = graph.RowUpdated;
    System.Type type6 = typeof (PMTimeActivity);
    PMTimeActivityList<TMasterActivity> timeActivityList6 = this;
    // ISSUE: virtual method pointer
    PXRowUpdated pxRowUpdated = new PXRowUpdated((object) timeActivityList6, __vmethodptr(timeActivityList6, PMTimeActivity_RowUpdated));
    rowUpdated.AddHandler(type6, pxRowUpdated);
    PXGraph.RowPersistedEvents rowPersisted = graph.RowPersisted;
    System.Type type7 = typeof (PMTimeActivity);
    PMTimeActivityList<TMasterActivity> timeActivityList7 = this;
    // ISSUE: virtual method pointer
    PXRowPersisted pxRowPersisted = new PXRowPersisted((object) timeActivityList7, __vmethodptr(timeActivityList7, PMTimeActivity_RowPersisted));
    rowPersisted.AddHandler(type7, pxRowPersisted);
    PXGraph.FieldUpdatedEvents fieldUpdated1 = graph.FieldUpdated;
    System.Type type8 = typeof (PMTimeActivity);
    string name1 = typeof (PMTimeActivity.timeSpent).Name;
    PMTimeActivityList<TMasterActivity> timeActivityList8 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) timeActivityList8, __vmethodptr(timeActivityList8, PMTimeActivity_TimeSpent_FieldUpdated));
    fieldUpdated1.AddHandler(type8, name1, pxFieldUpdated1);
    PXGraph.FieldUpdatedEvents fieldUpdated2 = graph.FieldUpdated;
    System.Type type9 = typeof (PMTimeActivity);
    string name2 = typeof (PMTimeActivity.trackTime).Name;
    PMTimeActivityList<TMasterActivity> timeActivityList9 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) timeActivityList9, __vmethodptr(timeActivityList9, PMTimeActivity_TrackTime_FieldUpdated));
    fieldUpdated2.AddHandler(type9, name2, pxFieldUpdated2);
    PXGraph.FieldUpdatedEvents fieldUpdated3 = graph.FieldUpdated;
    System.Type type10 = typeof (PMTimeActivity);
    string name3 = typeof (PMTimeActivity.approvalStatus).Name;
    PMTimeActivityList<TMasterActivity> timeActivityList10 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated3 = new PXFieldUpdated((object) timeActivityList10, __vmethodptr(timeActivityList10, PMTimeActivity_ApprovalStatus_FieldUpdated));
    fieldUpdated3.AddHandler(type10, name3, pxFieldUpdated3);
    PXGraph.FieldUpdatedEvents fieldUpdated4 = graph.FieldUpdated;
    System.Type type11 = typeof (PMTimeActivity);
    string name4 = typeof (PMTimeActivity.projectTaskID).Name;
    PMTimeActivityList<TMasterActivity> timeActivityList11 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated4 = new PXFieldUpdated((object) timeActivityList11, __vmethodptr(timeActivityList11, PMTimeActivity_projectTaskID_FieldUpdated));
    fieldUpdated4.AddHandler(type11, name4, pxFieldUpdated4);
    PXGraph.FieldUpdatedEvents fieldUpdated5 = graph.FieldUpdated;
    System.Type type12 = typeof (PMTimeActivity);
    string name5 = typeof (PMTimeActivity.labourItemID).Name;
    PMTimeActivityList<TMasterActivity> timeActivityList12 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated5 = new PXFieldUpdated((object) timeActivityList12, __vmethodptr(timeActivityList12, PMTimeActivity_labourItemID_FieldUpdated));
    fieldUpdated5.AddHandler(type12, name5, pxFieldUpdated5);
    PXGraph.RowInsertedEvents rowInserted2 = graph.RowInserted;
    PMTimeActivityList<TMasterActivity> timeActivityList13 = this;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted2 = new PXRowInserted((object) timeActivityList13, __vmethodptr(timeActivityList13, Master_RowInserted));
    rowInserted2.AddHandler<TMasterActivity>(pxRowInserted2);
    PXGraph.RowPersistingEvents rowPersisting2 = graph.RowPersisting;
    PMTimeActivityList<TMasterActivity> timeActivityList14 = this;
    // ISSUE: virtual method pointer
    PXRowPersisting pxRowPersisting2 = new PXRowPersisting((object) timeActivityList14, __vmethodptr(timeActivityList14, Master_RowPersisting));
    rowPersisting2.AddHandler<TMasterActivity>(pxRowPersisting2);
    PXGraph.FieldUpdatedEvents fieldUpdated6 = graph.FieldUpdated;
    System.Type type13 = typeof (TMasterActivity);
    string name6 = typeof (CRActivity.type).Name;
    PMTimeActivityList<TMasterActivity> timeActivityList15 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated6 = new PXFieldUpdated((object) timeActivityList15, __vmethodptr(timeActivityList15, Master_Type_FieldUpdated));
    fieldUpdated6.AddHandler(type13, name6, pxFieldUpdated6);
    PXGraph.FieldUpdatedEvents fieldUpdated7 = graph.FieldUpdated;
    System.Type type14 = typeof (TMasterActivity);
    string name7 = typeof (CRActivity.ownerID).Name;
    PMTimeActivityList<TMasterActivity> timeActivityList16 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated7 = new PXFieldUpdated((object) timeActivityList16, __vmethodptr(timeActivityList16, Master_OwnerID_FieldUpdated));
    fieldUpdated7.AddHandler(type14, name7, pxFieldUpdated7);
    PXGraph.FieldUpdatedEvents fieldUpdated8 = graph.FieldUpdated;
    System.Type type15 = typeof (TMasterActivity);
    string name8 = typeof (CRActivity.startDate).Name;
    PMTimeActivityList<TMasterActivity> timeActivityList17 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated8 = new PXFieldUpdated((object) timeActivityList17, __vmethodptr(timeActivityList17, Master_StartDate_FieldUpdated));
    fieldUpdated8.AddHandler(type15, name8, pxFieldUpdated8);
    PXGraph.FieldUpdatedEvents fieldUpdated9 = graph.FieldUpdated;
    System.Type type16 = typeof (TMasterActivity);
    string name9 = typeof (CRActivity.subject).Name;
    PMTimeActivityList<TMasterActivity> timeActivityList18 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated9 = new PXFieldUpdated((object) timeActivityList18, __vmethodptr(timeActivityList18, Master_Subject_FieldUpdated));
    fieldUpdated9.AddHandler(type16, name9, pxFieldUpdated9);
    PXGraph.FieldUpdatedEvents fieldUpdated10 = graph.FieldUpdated;
    System.Type type17 = typeof (TMasterActivity);
    string name10 = typeof (CRActivity.parentNoteID).Name;
    PMTimeActivityList<TMasterActivity> timeActivityList19 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated10 = new PXFieldUpdated((object) timeActivityList19, __vmethodptr(timeActivityList19, Master_ParentNoteID_FieldUpdated));
    fieldUpdated10.AddHandler(type17, name10, pxFieldUpdated10);
    ((PXSelectBase) this).View = new PXView(graph, false, PMTimeActivityList<TMasterActivity>.GenerateOriginalCommand());
    ActivityStatusListAttribute.SetRestictedMode<PMTimeActivity.approvalStatus>(((PXSelectBase) this).View.Cache, true);
  }

  public static BqlCommand GenerateOriginalCommand()
  {
    System.Type nestedType = typeof (PMTimeActivity).GetNestedType(typeof (PMTimeActivity.createdDateTime).Name);
    return BqlCommand.CreateInstance(new System.Type[11]
    {
      typeof (Select<,,>),
      typeof (PMTimeActivity),
      typeof (Where<,,>),
      typeof (PMTimeActivity.refNoteID),
      typeof (Equal<>),
      typeof (PX.Data.Current<>),
      typeof (TMasterActivity).GetNestedType(typeof (CRActivity.noteID).Name),
      typeof (And<PMTimeActivity.isCorrected, Equal<False>>),
      typeof (OrderBy<>),
      typeof (Desc<>),
      nestedType
    });
  }

  public virtual PMTimeActivity SelectSingle(params object[] parameters)
  {
    using (new PXReadInsertedDeletedScope())
      return (((PXSelectBase) this).View.Cache.Current = ((PXSelectBase) this).View.SelectSingle(parameters)) as PMTimeActivity;
  }

  protected virtual void Master_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    using (new ReadOnlyScope(new PXCache[1]
    {
      this.MainCache
    }))
    {
      this.Current = (PMTimeActivity) this.MainCache.Insert();
      this.Current.ApprovalStatus = "OP";
    }
  }

  protected virtual void Master_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    TMasterActivity row = e.Row as TMasterActivity;
    PMTimeActivity current = this.Current;
    if ((object) row == null || current == null || current.TrackTime.GetValueOrDefault() || e.Operation == 3)
      return;
    int? classId = row.ClassID;
    if (classId.GetValueOrDefault() == 4)
      return;
    classId = row.ClassID;
    string str1;
    if (classId.GetValueOrDefault() != 1)
    {
      classId = row.ClassID;
      int num = 0;
      if (!(classId.GetValueOrDefault() == num & classId.HasValue))
      {
        str1 = "CD";
        goto label_6;
      }
    }
    str1 = row.UIStatus;
label_6:
    string str2 = str1;
    string valueOriginal = cache.GetValueOriginal<CRActivity.uistatus>((object) row) as string;
    if (!(str2 != valueOriginal))
      return;
    cache.SetValueExt<CRActivity.uistatus>((object) row, (object) str2);
    classId = row.ClassID;
    if (classId.GetValueOrDefault() == 2)
      cache.SetValue<CRActivity.completedDate>((object) row, (object) row.StartDate);
    cache.RaiseRowUpdated((object) row, (object) row);
  }

  protected virtual void Master_Type_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    TMasterActivity row = e.Row as TMasterActivity;
    PMTimeActivity pmTimeActivity = this.Current ?? (PMTimeActivity) this.MainCache.Insert();
    if ((object) row == null || pmTimeActivity == null)
      return;
    bool valueOrDefault = ((bool?) PXFormulaAttribute.Evaluate<PMTimeActivity.trackTime>(this.MainCache, (object) pmTimeActivity)).GetValueOrDefault();
    if (valueOrDefault.Equals((object) pmTimeActivity.TrackTime))
      return;
    pmTimeActivity.TrackTime = new bool?(valueOrDefault);
    this.MainCache.Update((object) pmTimeActivity);
  }

  protected virtual void Master_OwnerID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    TMasterActivity row = e.Row as TMasterActivity;
    PMTimeActivity current = this.Current;
    if ((object) row == null || current == null)
      return;
    this.MainCache.SetValue<PMTimeActivity.ownerID>((object) current, (object) row.OwnerID);
    GraphHelper.MarkUpdated(this.MainCache, (object) current);
  }

  protected virtual void Master_StartDate_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    TMasterActivity row = e.Row as TMasterActivity;
    PMTimeActivity current = this.Current;
    if ((object) row == null || current == null)
      return;
    current.Date = (DateTime?) PXFormulaAttribute.Evaluate<PMTimeActivity.date>(this.MainCache, (object) current);
    this.MainCache.SetDefaultExt<PMTimeActivity.weekID>((object) current);
    this.MainCache.Update((object) current);
  }

  protected virtual void Master_Subject_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    PMTimeActivity current = this.Current;
    if (current == null)
      return;
    GraphHelper.MarkUpdated(this.MainCache, (object) current);
  }

  protected virtual void Master_ParentNoteID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    TMasterActivity row = e.Row as TMasterActivity;
    PMTimeActivity current = this.Current;
    if ((object) row == null || current == null)
      return;
    PXResult<CRActivity, PMTimeActivity> pxResult = (PXResult<CRActivity, PMTimeActivity>) PXResultset<CRActivity>.op_Implicit(PXSelectBase<CRActivity, PXSelectJoin<CRActivity, InnerJoin<PMTimeActivity, On<PMTimeActivity.isCorrected, Equal<False>, And<CRActivity.noteID, Equal<PMTimeActivity.refNoteID>>>>, Where<CRActivity.noteID, Equal<Required<CRActivity.noteID>>>>.Config>.Select(((PXSelectBase) this)._Graph, new object[1]
    {
      (object) row.ParentNoteID
    }));
    CRActivity crActivity = PXResult<CRActivity, PMTimeActivity>.op_Implicit(pxResult);
    PMTimeActivity pmTimeActivity1 = PXResult<CRActivity, PMTimeActivity>.op_Implicit(pxResult);
    if (pmTimeActivity1 != null)
    {
      current.ProjectID = pmTimeActivity1.ProjectID;
      current.ProjectTaskID = pmTimeActivity1.ProjectTaskID;
    }
    PMTimeActivity pmTimeActivity2 = current;
    Guid? nullable;
    if (crActivity != null)
    {
      int? classId = crActivity.ClassID;
      int num = 0;
      if (classId.GetValueOrDefault() == num & classId.HasValue)
      {
        nullable = crActivity.NoteID;
        goto label_7;
      }
    }
    nullable = new Guid?();
label_7:
    pmTimeActivity2.ParentTaskNoteID = nullable;
    this.MainCache.Update((object) current);
  }

  protected virtual void PMTimeActivity_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    PMTimeActivity row = (PMTimeActivity) e.Row;
    TMasterActivity current = (TMasterActivity) this.MasterCache.Current;
    PXUIFieldAttribute.SetDisplayName<PMTimeActivity.approvalStatus>(cache, "Status");
    if (row == null || (object) current == null)
      return;
    bool? nullable;
    int num1;
    if (string.IsNullOrEmpty(row.TimeCardCD))
    {
      nullable = row.Billed;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 1;
    bool flag = num1 != 0;
    int? classId = current.ClassID;
    int num2 = 0;
    string str;
    if (!(classId.GetValueOrDefault() == num2 & classId.HasValue))
    {
      classId = current.ClassID;
      if (classId.GetValueOrDefault() != 1)
      {
        str = (string) cache.GetValueOriginal<PMTimeActivity.approvalStatus>((object) row) ?? "OP";
        goto label_9;
      }
    }
    str = (string) this.MasterCache.GetValueOriginal<CRActivity.uistatus>((object) current) ?? "OP";
label_9:
    if (str == "OP")
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) row, true);
      PXCache pxCache1 = cache;
      PMTimeActivity pmTimeActivity1 = row;
      int num3;
      if (!flag)
      {
        if (row == null)
        {
          num3 = 0;
        }
        else
        {
          nullable = row.IsBillable;
          num3 = nullable.GetValueOrDefault() ? 1 : 0;
        }
      }
      else
        num3 = 0;
      PXUIFieldAttribute.SetEnabled<PMTimeActivity.timeBillable>(pxCache1, (object) pmTimeActivity1, num3 != 0);
      PXCache pxCache2 = cache;
      PMTimeActivity pmTimeActivity2 = row;
      int num4;
      if (!flag)
      {
        if (row == null)
        {
          num4 = 0;
        }
        else
        {
          nullable = row.IsBillable;
          num4 = nullable.GetValueOrDefault() ? 1 : 0;
        }
      }
      else
        num4 = 0;
      PXUIFieldAttribute.SetEnabled<PMTimeActivity.overtimeBillable>(pxCache2, (object) pmTimeActivity2, num4 != 0);
      PXUIFieldAttribute.SetEnabled<PMTimeActivity.projectID>(cache, (object) row, !flag);
      PXUIFieldAttribute.SetEnabled<PMTimeActivity.projectTaskID>(cache, (object) row, !flag);
      PXUIFieldAttribute.SetEnabled<PMTimeActivity.trackTime>(cache, (object) row, !flag);
    }
    else
      PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
    PXCache pxCache = cache;
    PMTimeActivity pmTimeActivity = row;
    nullable = row.TrackTime;
    int num5;
    if (nullable.GetValueOrDefault() && !flag)
    {
      nullable = row.Released;
      num5 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num5 = 0;
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.approvalStatus>(pxCache, (object) pmTimeActivity, num5 != 0);
    PXUIFieldAttribute.SetEnabled<PMTimeActivity.released>(cache, (object) row, false);
    nullable = row.Released;
    if (!nullable.GetValueOrDefault() || row.ARRefNbr != null)
      return;
    CRCase crCase = PXResultset<CRCase>.op_Implicit(PXSelectBase<CRCase, PXSelect<CRCase, Where<CRCase.noteID, Equal<Required<CRActivity.refNoteID>>>>.Config>.Select(((PXSelectBase) this)._Graph, new object[1]
    {
      (object) current.RefNoteID
    }));
    if (crCase != null && crCase.ARRefNbr != null)
    {
      ARInvoice arInvoice = (ARInvoice) PXSelectorAttribute.Select<CRCase.aRRefNbr>((PXCache) GraphHelper.Caches<CRCase>(((PXSelectBase) this)._Graph), (object) crCase);
      row.ARRefNbr = arInvoice.RefNbr;
      row.ARDocType = arInvoice.DocType;
    }
    if (row.ARRefNbr != null)
      return;
    PMTran pmTran = PXResultset<PMTran>.op_Implicit(PXSelectBase<PMTran, PXSelect<PMTran, Where<PMTran.origRefID, Equal<Required<CRActivity.noteID>>>>.Config>.Select(((PXSelectBase) this)._Graph, new object[1]
    {
      (object) current.NoteID
    }));
    if (pmTran == null)
      return;
    row.ARDocType = pmTran.ARTranType;
    row.ARRefNbr = pmTran.ARRefNbr;
  }

  protected virtual void PMTimeActivity_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    PMTimeActivity row = (PMTimeActivity) e.Row;
    if (row != null && (!string.IsNullOrEmpty(row.TimeCardCD) || row.Billed.GetValueOrDefault()))
      throw new PXException("Activity cannot be deleted because it has already been billed");
  }

  protected virtual void PMTimeActivity_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    PMTimeActivity row = e.Row as PMTimeActivity;
    TMasterActivity current = (TMasterActivity) this.MasterCache.Current;
    if (row == null || (object) current == null)
      return;
    row.RefNoteID = current.NoteID;
    row.OwnerID = current.OwnerID;
    cache.RaiseFieldUpdated<PMTimeActivity.approvalStatus>((object) row, (object) null);
    cache.RaiseFieldUpdated<PMTimeActivity.ownerID>((object) row, (object) null);
    if (!current.ParentNoteID.HasValue)
      return;
    PXResult<CRActivity, PMTimeActivity> pxResult = (PXResult<CRActivity, PMTimeActivity>) PXResultset<CRActivity>.op_Implicit(PXSelectBase<CRActivity, PXSelectJoin<CRActivity, InnerJoin<PMTimeActivity, On<PMTimeActivity.isCorrected, Equal<False>, And<CRActivity.noteID, Equal<PMTimeActivity.refNoteID>>>>, Where<CRActivity.noteID, Equal<Required<CRActivity.noteID>>>>.Config>.Select(((PXSelectBase) this)._Graph, new object[1]
    {
      (object) current.ParentNoteID
    }));
    CRActivity crActivity = PXResult<CRActivity, PMTimeActivity>.op_Implicit(pxResult);
    PMTimeActivity pmTimeActivity1 = PXResult<CRActivity, PMTimeActivity>.op_Implicit(pxResult);
    Guid? nullable1;
    int? nullable2;
    if (pmTimeActivity1 != null)
    {
      nullable1 = pmTimeActivity1.RefNoteID;
      if (nullable1.HasValue)
      {
        nullable2 = pmTimeActivity1.ProjectID;
        if (!nullable2.HasValue)
        {
          nullable2 = pmTimeActivity1.ProjectTaskID;
          if (!nullable2.HasValue)
            goto label_9;
        }
        nullable2 = row.ProjectID;
        if (!nullable2.HasValue || ProjectDefaultAttribute.IsNonProject(row.ProjectID))
        {
          row.ProjectID = pmTimeActivity1.ProjectID;
          row.ProjectTaskID = pmTimeActivity1.ProjectTaskID;
          row.CostCodeID = pmTimeActivity1.CostCodeID;
          cache.RaiseFieldUpdated<PMTimeActivity.projectTaskID>((object) row, (object) null);
        }
      }
    }
label_9:
    PMTimeActivity pmTimeActivity2 = row;
    Guid? nullable3;
    if (crActivity != null)
    {
      nullable2 = crActivity.ClassID;
      int num = 0;
      if (nullable2.GetValueOrDefault() == num & nullable2.HasValue)
      {
        nullable3 = crActivity.NoteID;
        goto label_13;
      }
    }
    nullable1 = new Guid?();
    nullable3 = nullable1;
label_13:
    pmTimeActivity2.ParentTaskNoteID = nullable3;
  }

  protected virtual void PMTimeActivity_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is PMTimeActivity row))
      return;
    IEnumerator enumerator1 = cache.Inserted.GetEnumerator();
    if (enumerator1 != null && enumerator1.MoveNext())
    {
      cache.SetStatus(enumerator1.Current, (PXEntryStatus) 5);
    }
    else
    {
      IEnumerator enumerator2 = cache.Updated.GetEnumerator();
      if (enumerator2 != null && enumerator2.MoveNext())
      {
        cache.SetStatus(enumerator2.Current, (PXEntryStatus) 5);
      }
      else
      {
        IEnumerator enumerator3 = cache.Deleted.GetEnumerator();
        if (enumerator3 == null || !enumerator3.MoveNext())
          return;
        row.NoteID = ((PMTimeActivity) enumerator3.Current).NoteID;
      }
    }
  }

  protected virtual void PMTimeActivity_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    PMTimeActivity row = e.Row as PMTimeActivity;
    TMasterActivity current = (TMasterActivity) this.MasterCache.Current;
    if (row == null || (object) current == null)
      return;
    cache.SetValue<PMTimeActivity.summary>((object) row, (object) current.Subject);
    Guid? noteId = row.NoteID;
    Guid? refNoteId = row.RefNoteID;
    if ((noteId.HasValue == refNoteId.HasValue ? (noteId.HasValue ? (noteId.GetValueOrDefault() == refNoteId.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
    {
      cache.SetValue<PMTimeActivity.noteID>((object) row, (object) SequentialGuid.Generate());
      cache.Normalize();
    }
    int? nullable = current.ClassID;
    int num = 0;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
    {
      nullable = current.ClassID;
      if (nullable.GetValueOrDefault() != 1)
        goto label_7;
    }
    cache.SetValue<PMTimeActivity.trackTime>((object) row, (object) false);
label_7:
    row.NeedToBeDeleted = (bool?) PXFormulaAttribute.Evaluate<PMTimeActivity.needToBeDeleted>(cache, (object) row);
    if (row.NeedToBeDeleted.GetValueOrDefault() && e.Operation != 3)
      ((CancelEventArgs) e).Cancel = true;
    else if (!row.TrackTime.GetValueOrDefault())
    {
      if (current.UIStatus == "CD")
        row.ApprovalStatus = "CD";
      else if (current.UIStatus == "CL")
        row.ApprovalStatus = "CL";
      else
        row.ApprovalStatus = "OP";
    }
    else
    {
      if (!(row.ApprovalStatus == "CD"))
        return;
      nullable = row.ApproverID;
      if (!nullable.HasValue)
        return;
      row.ApprovalStatus = "PA";
    }
  }

  protected virtual void PMTimeActivity_RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
  {
    PMTimeActivity row = (PMTimeActivity) e.Row;
    if (row == null || e.TranStatus != null || e.Operation != 3)
      return;
    PXDatabase.Update<PMTimeActivity>(new PXDataFieldParam[3]
    {
      (PXDataFieldParam) new PXDataFieldAssign<PMTimeActivity.projectID>((PXDbType) 8, (object) ProjectDefaultAttribute.NonProject()),
      (PXDataFieldParam) new PXDataFieldAssign<PMTimeActivity.projectTaskID>((PXDbType) 8, (object) null),
      (PXDataFieldParam) new PXDataFieldRestrict<PMTimeActivity.noteID>((PXDbType) 14, (object) row.NoteID)
    });
  }

  protected virtual void PMTimeActivity_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is PMTimeActivity row) || ((PXSelectBase) this)._Graph.IsContractBasedAPI)
      return;
    bool flag = cache.GetValueOriginal<PMTimeActivity.noteID>((object) row) != null;
    row.NeedToBeDeleted = (bool?) PXFormulaAttribute.Evaluate<PMTimeActivity.needToBeDeleted>(cache, (object) row);
    if (row.NeedToBeDeleted.GetValueOrDefault())
    {
      if (!flag)
        cache.SetStatus((object) row, (PXEntryStatus) 5);
      else
        cache.SetStatus((object) row, (PXEntryStatus) 3);
      this.StoreCached(new PXCommandKey(new object[1]
      {
        (object) row.RefNoteID
      }, (object[]) null, (string[]) null, (bool[]) null, new int?(0), new int?(1), (PXFilterRow[]) null, false, (string[]) null), new List<object>()
      {
        (object) row
      });
    }
    else
    {
      if (cache.GetStatus((object) row) != 1 || flag)
        return;
      cache.SetStatus((object) row, (PXEntryStatus) 2);
    }
  }

  protected virtual void PMTimeActivity_ApprovalStatus_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    PMTimeActivity row = e.Row as PMTimeActivity;
    TMasterActivity current = (TMasterActivity) this.MasterCache.Current;
    if (row == null || (object) current == null)
      return;
    bool? nullable = row.TrackTime;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = current.IsLocked;
    if (nullable.GetValueOrDefault() || !(current.UIStatus != "DR"))
      return;
    TMasterActivity copy = (TMasterActivity) this.MasterCache.CreateCopy((object) current);
    switch (row.ApprovalStatus)
    {
      case "OP":
        copy.UIStatus = "OP";
        break;
      case "CL":
        copy.UIStatus = "CL";
        break;
      default:
        copy.UIStatus = "CD";
        break;
    }
    if (!(current.UIStatus != copy.UIStatus))
      return;
    this.MasterCache.Update((object) copy);
  }

  protected virtual void PMTimeActivity_TimeSpent_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    PMTimeActivity row = e.Row as PMTimeActivity;
    TMasterActivity current = (TMasterActivity) this.MasterCache.Current;
    if (row == null || (object) current == null)
      return;
    DateTime? nullable1 = current.StartDate;
    if (!nullable1.HasValue)
      return;
    PXCache masterCache = this.MasterCache;
    // ISSUE: variable of a boxed type
    __Boxed<TMasterActivity> local1 = (object) current;
    string name = typeof (CRActivity.endDate).Name;
    int? timeSpent = row.TimeSpent;
    DateTime? nullable2;
    if (!timeSpent.HasValue)
    {
      nullable1 = new DateTime?();
      nullable2 = nullable1;
    }
    else
    {
      nullable1 = current.StartDate;
      DateTime dateTime = nullable1.Value;
      ref DateTime local2 = ref dateTime;
      timeSpent = row.TimeSpent;
      double num = (double) timeSpent.Value;
      nullable2 = new DateTime?(local2.AddMinutes(num));
    }
    // ISSUE: variable of a boxed type
    __Boxed<DateTime?> local3 = (ValueType) nullable2;
    masterCache.SetValue((object) local1, name, (object) local3);
  }

  protected virtual void PMTimeActivity_TrackTime_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    PMTimeActivity row = e.Row as PMTimeActivity;
    TMasterActivity current = (TMasterActivity) this.MasterCache.Current;
    if (row == null || (object) current == null || row.TrackTime.GetValueOrDefault())
      return;
    cache.SetValue<PMTimeActivity.timeSpent>((object) row, (object) 0);
    cache.SetValue<PMTimeActivity.timeBillable>((object) row, (object) 0);
    cache.SetValue<PMTimeActivity.overtimeSpent>((object) row, (object) 0);
    cache.SetValue<PMTimeActivity.overtimeBillable>((object) row, (object) 0);
    cache.SetValue<PMTimeActivity.approvalStatus>((object) row, (object) "CD");
    this.MasterCache.SetValue<CRActivity.uistatus>((object) current, (object) "CD");
    if (current.ClassID.GetValueOrDefault() != 2)
      return;
    this.MasterCache.SetValue<CRActivity.completedDate>((object) current, (object) current.StartDate);
  }

  protected virtual void PMTimeActivity_projectTaskID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    cache.SetDefaultExt<PMTimeActivity.costCodeID>(e.Row);
  }

  protected virtual void PMTimeActivity_labourItemID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    cache.SetDefaultExt<PMTimeActivity.costCodeID>(e.Row);
  }

  private PXCache MainCache => ((PXSelectBase) this)._Graph.Caches[typeof (PMTimeActivity)];

  private PXCache MasterCache => ((PXSelectBase) this)._Graph.Caches[typeof (TMasterActivity)];

  public PMTimeActivity Current
  {
    get => (PMTimeActivity) ((PXSelectBase) this).View.Cache.Current;
    set => ((PXSelectBase) this).View.Cache.Current = (object) value;
  }
}
