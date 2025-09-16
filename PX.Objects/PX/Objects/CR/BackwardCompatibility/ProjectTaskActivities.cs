// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BackwardCompatibility.ProjectTaskActivities
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.EP;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.CR.BackwardCompatibility;

/// <exclude />
[Obsolete]
public sealed class ProjectTaskActivities : PMActivityList<PMTask>
{
  public ProjectTaskActivities(PXGraph graph)
    : base(graph)
  {
    // ISSUE: method pointer
    ((PXSelectBase) this)._Graph.RowSelected.AddHandler<PMTask>(new PXRowSelected((object) this, __methodptr(RowSelected)));
    // ISSUE: method pointer
    ((PXSelectBase) this)._Graph.RowInserting.AddHandler<CRPMTimeActivity>(new PXRowInserting((object) this, __methodptr(RowInserting)));
  }

  private void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if ((PMTask) e.Row == null || ((PXSelectBase) this).View == null || ((PXSelectBase) this).View.Cache == null)
      return;
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>.Config>.Select(((PXSelectBase) this)._Graph, Array.Empty<object>()));
    bool flag = true;
    if (pmProject != null && pmProject.RestrictToEmployeeList.GetValueOrDefault())
      flag = ((PXSelectBase<EPEmployeeContract>) new PXSelectJoin<EPEmployeeContract, InnerJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<EPEmployeeContract.employeeID>>>, Where<EPEmployeeContract.contractID, Equal<Current<PMTask.projectID>>, And<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>>(((PXSelectBase) this)._Graph)).SelectSingle(Array.Empty<object>()) != null;
    ((PXSelectBase) this).View.Cache.AllowInsert = flag;
  }

  private void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    CRPMTimeActivity row = (CRPMTimeActivity) e.Row;
    if (row == null)
      return;
    row.ProjectID = ((PMTask) sender.Graph.Caches[typeof (PMTask)].Current).ProjectID;
    row.ProjectTaskID = ((PMTask) sender.Graph.Caches[typeof (PMTask)].Current).TaskID;
  }

  protected override void CreateTimeActivity(PXCache cache, int classId)
  {
    PXCache cach = cache.Graph.Caches[typeof (PMTimeActivity)];
    if (cach == null)
      return;
    PMTimeActivity current = (PMTimeActivity) cach.Current;
    if (current == null)
      return;
    bool flag = classId != 0 && classId != 1;
    cach.SetValue<PMTimeActivity.trackTime>((object) current, (object) flag);
    cach.SetValueExt<PMTimeActivity.projectID>((object) current, (object) (int?) ((PMTask) ((PXSelectBase) this)._Graph.Caches[typeof (PMTask)].Current)?.ProjectID);
    cach.SetValueExt<PMTimeActivity.projectTaskID>((object) current, (object) (int?) ((PMTask) ((PXSelectBase) this)._Graph.Caches[typeof (PMTask)].Current)?.TaskID);
  }

  public new static BqlCommand GenerateOriginalCommand()
  {
    return BqlCommand.CreateInstance(new System.Type[1]
    {
      typeof (Select2<PMCRActivity, LeftJoin<CRReminder, On<CRReminder.refNoteID, Equal<PMCRActivity.noteID>>>, Where<CRPMTimeActivity.projectTaskID, Equal<Current<PMTask.taskID>>>, OrderBy<Desc<CRPMTimeActivity.timeActivityCreatedDateTime>>>)
    });
  }

  protected override void SetCommandCondition(Delegate handler = null)
  {
    BqlCommand originalCommand = ProjectTaskActivities.GenerateOriginalCommand();
    if ((object) handler == null)
      ((PXSelectBase) this).View = new PXView(((PXSelectBase) this).View.Graph, ((PXSelectBase) this).View.IsReadOnly, originalCommand);
    else
      ((PXSelectBase) this).View = new PXView(((PXSelectBase) this).View.Graph, ((PXSelectBase) this).View.IsReadOnly, originalCommand, handler);
  }
}
