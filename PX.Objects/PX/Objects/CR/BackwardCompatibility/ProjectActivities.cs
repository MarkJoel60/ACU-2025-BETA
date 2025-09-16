// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BackwardCompatibility.ProjectActivities
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.EP;
using PX.Objects.PM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR.BackwardCompatibility;

/// <exclude />
[Obsolete]
public sealed class ProjectActivities : PMActivityList<PMProject>
{
  public ProjectActivities(PXGraph graph)
    : base(graph)
  {
    // ISSUE: method pointer
    ((PXSelectBase) this)._Graph.RowSelected.AddHandler<PMProject>(new PXRowSelected((object) this, __methodptr(PMProject_RowSelected)));
    // ISSUE: method pointer
    ((PXSelectBase) this)._Graph.FieldDefaulting.AddHandler<PMTimeActivity.projectID>(new PXFieldDefaulting((object) this, __methodptr(ProjectID_FieldDefaulting)));
    // ISSUE: method pointer
    ((PXSelectBase) this)._Graph.FieldDefaulting.AddHandler<PMTimeActivity.trackTime>(new PXFieldDefaulting((object) this, __methodptr(TrackTime_FieldDefaulting)));
  }

  private void ProjectID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    PXCache cach = sender.Graph.Caches[typeof (PMProject)];
    if (cach.Current == null)
      return;
    e.NewValue = (object) ((PX.Objects.CT.Contract) cach.Current).ContractID;
  }

  private void TrackTime_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) true;
  }

  private void PMProject_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PMProject row = (PMProject) e.Row;
    PXCache pxCache;
    if (row == null || ((PXSelectBase) this).View == null || ((Dictionary<System.Type, PXCache>) ((PXSelectBase) this).View.Graph.Caches).TryGetValue(((PXSelectBase) this).View.GetItemType(), out pxCache) && pxCache == null)
      return;
    bool userCanAddActivity = row.Status != "F";
    if (row.RestrictToEmployeeList.GetValueOrDefault() && !sender.Graph.IsExport)
    {
      EPEmployeeContract employeeContract = ((PXSelectBase<EPEmployeeContract>) new PXSelectJoin<EPEmployeeContract, InnerJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<EPEmployeeContract.employeeID>>>, Where<EPEmployeeContract.contractID, Equal<Current<PMProject.contractID>>, And<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>>(((PXSelectBase) this)._Graph)).SelectSingle(Array.Empty<object>());
      userCanAddActivity = userCanAddActivity && employeeContract != null;
    }
    ((PXSelectBase) this).View.Graph.Caches.SubscribeCacheCreated(((PXSelectBase) this).View.GetItemType(), (Action) (() => ((PXSelectBase) this).View.Cache.AllowInsert = userCanAddActivity));
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
    current.TrackTime = new bool?(flag);
    current.ProjectID = (int?) ((PX.Objects.CT.Contract) ((PXSelectBase) this)._Graph.Caches[typeof (PMProject)].Current)?.ContractID;
    cach.Update((object) current);
  }

  public new static BqlCommand GenerateOriginalCommand()
  {
    return BqlCommand.CreateInstance(new System.Type[1]
    {
      typeof (Select2<PMCRActivity, LeftJoin<CRReminder, On<CRReminder.refNoteID, Equal<PMCRActivity.noteID>>>, Where<CRPMTimeActivity.projectID, Equal<Current<PMProject.contractID>>>, OrderBy<Desc<CRPMTimeActivity.timeActivityCreatedDateTime>>>)
    });
  }

  protected override void SetCommandCondition(Delegate handler = null)
  {
    BqlCommand originalCommand = ProjectActivities.GenerateOriginalCommand();
    if ((object) handler == null)
      ((PXSelectBase) this).View = new PXView(((PXSelectBase) this).View.Graph, ((PXSelectBase) this).View.IsReadOnly, originalCommand);
    else
      ((PXSelectBase) this).View = new PXView(((PXSelectBase) this).View.Graph, ((PXSelectBase) this).View.IsReadOnly, originalCommand, handler);
  }
}
