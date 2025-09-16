// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TaskInquiry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.EP;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[DashboardType(new int[] {2})]
public class TaskInquiry : PXGraph<
#nullable disable
TaskInquiry>
{
  [PXViewName("Selection")]
  public PXFilter<TaskInquiry.TaskFilter> Filter;
  [PXViewName("Tasks")]
  [PXFilterable(new Type[] {})]
  public PXSelectJoin<PMTask, LeftJoin<PMTaskTotal, On<PMTaskTotal.projectID, Equal<PMTask.projectID>, And<PMTaskTotal.taskID, Equal<PMTask.taskID>>>, LeftJoin<PMProject, On<PMTask.projectID, Equal<PMProject.contractID>>>>, Where<PMTask.approverID, Equal<Current<TaskInquiry.TaskFilter.approverID>>, And<PMProject.status, NotEqual<ProjectStatus.closed>>>> FilteredItems;
  public PXCancel<TaskInquiry.TaskFilter> Cancel;
  public PXAction<TaskInquiry.TaskFilter> viewProject;
  public PXAction<TaskInquiry.TaskFilter> viewTask;

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewProject(PXAdapter adapter)
  {
    if (((PXSelectBase<PMTask>) this.FilteredItems).Current != null)
      ProjectAccountingService.NavigateToProjectScreen(((PXSelectBase<PMTask>) this.FilteredItems).Current.ProjectID);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewTask(PXAdapter adapter)
  {
    ProjectTaskEntry instance = PXGraph.CreateInstance<ProjectTaskEntry>();
    ((PXSelectBase<PMTask>) instance.Task).Current = PXResultset<PMTask>.op_Implicit(((PXSelectBase<PMTask>) instance.Task).Search<PMTask.taskCD>((object) ((PXSelectBase<PMTask>) this.FilteredItems).Current.TaskCD, new object[1]
    {
      (object) ((PXSelectBase<PMTask>) this.FilteredItems).Current.ProjectID
    }));
    ProjectAccountingService.NavigateToScreen((PXGraph) instance, "View Task");
    return adapter.Get();
  }

  public TaskInquiry()
  {
    ((PXSelectBase) this.FilteredItems).Cache.AllowInsert = false;
    ((PXSelectBase) this.FilteredItems).Cache.AllowUpdate = false;
    ((PXSelectBase) this.FilteredItems).Cache.AllowDelete = false;
    PXUIFieldAttribute.SetVisible<PMTask.rateTableID>(((PXSelectBase) this.FilteredItems).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMTask.startDate>(((PXSelectBase) this.FilteredItems).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMTask.locationID>(((PXSelectBase) this.FilteredItems).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMTask.endDate>(((PXSelectBase) this.FilteredItems).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMTask.defaultSalesSubID>(((PXSelectBase) this.FilteredItems).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMTask.defaultExpenseSubID>(((PXSelectBase) this.FilteredItems).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMTask.visibleInAP>(((PXSelectBase) this.FilteredItems).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMTask.visibleInAR>(((PXSelectBase) this.FilteredItems).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMTask.visibleInGL>(((PXSelectBase) this.FilteredItems).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMTask.visibleInIN>(((PXSelectBase) this.FilteredItems).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMTask.visibleInCA>(((PXSelectBase) this.FilteredItems).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMTask.visibleInPO>(((PXSelectBase) this.FilteredItems).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMTask.visibleInSO>(((PXSelectBase) this.FilteredItems).Cache, (object) null, false);
    PXUIFieldAttribute.SetDisplayName<PMProject.description>(((PXGraph) this).Caches[typeof (PMProject)], "Project Description");
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class TaskFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _ApproverID;

    [PXDBInt]
    [PXSubordinateSelector]
    [PXUIField]
    public virtual int? ApproverID
    {
      get => this._ApproverID;
      set => this._ApproverID = value;
    }

    public abstract class approverID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaskInquiry.TaskFilter.approverID>
    {
    }
  }
}
