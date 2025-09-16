// Decompiled with JetBrains decompiler
// Type: PX.Objects.WZ.WizardScenarioMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.WZ;

public class WizardScenarioMaint : PXGraph<WizardScenarioMaint>
{
  public PXSelect<WZScenario> Scenario;
  [PXFilterable(new Type[] {})]
  public PXSelect<WZTask, Where<WZTask.scenarioID, Equal<Current<WZScenario.scenarioID>>, And<WZTask.status, NotEqual<WizardTaskStatusesAttribute.Disabled>>>, OrderBy<Asc<WZTask.order>>> Tasks;
  public PXFilter<WZTaskAssign> CurrentTask;
  public PXSelectJoin<WZTaskPredecessorRelation, InnerJoin<WZTask, On<WZTask.taskID, Equal<WZTaskPredecessorRelation.predecessorID>>>, Where<WZTaskPredecessorRelation.taskID, Equal<Current<WZTask.taskID>>>> Predecessors;
  public PXSave<WZScenario> save;
  public PXCancel<WZScenario> cancel;
  public PXAction<WZScenario> startTask;
  public PXAction<WZScenario> viewTask;
  public PXAction<WZScenario> markAsCompleted;
  public PXAction<WZScenario> assign;
  public PXMenuAction<WZScenario> Action;
  public PXAction<WZScenario> reopen;
  public PXAction<WZScenario> skip;
  public PXAction<WZScenario> completeScenario;

  protected virtual IEnumerable tasks()
  {
    WizardScenarioMaint wizardScenarioMaint1 = this;
    int order = 0;
    int level = 0;
    WizardScenarioMaint wizardScenarioMaint2 = wizardScenarioMaint1;
    object[] objArray = new object[1]{ (object) Guid.Empty };
    foreach (PXResult<WZTask> pxResult in PXSelectBase<WZTask, PXSelectReadonly<WZTask, Where<WZTask.scenarioID, Equal<Current<WZScenario.scenarioID>>, And<WZTask.parentTaskID, Equal<Required<WZTask.parentTaskID>>, And<WZTask.status, NotEqual<WizardTaskStatusesAttribute.Disabled>>>>, OrderBy<Asc<WZTask.position>>>.Config>.Select((PXGraph) wizardScenarioMaint2, objArray))
    {
      WZTask task = PXResult<WZTask>.op_Implicit(pxResult);
      task.Order = new int?(order);
      task.Offset = new int?(level);
      yield return (object) task;
      ++order;
      foreach (WZTask childTask in wizardScenarioMaint1.GetChildTasks(task.TaskID, level + 1))
      {
        childTask.Order = new int?(order);
        yield return (object) childTask;
        ++order;
      }
      task = (WZTask) null;
    }
  }

  public IEnumerable GetChildTasks(Guid? taskID, int level)
  {
    WizardScenarioMaint wizardScenarioMaint1 = this;
    WizardScenarioMaint wizardScenarioMaint2 = wizardScenarioMaint1;
    object[] objArray = new object[1]{ (object) taskID };
    foreach (PXResult<WZTask> pxResult in PXSelectBase<WZTask, PXSelectReadonly<WZTask, Where<WZTask.parentTaskID, Equal<Required<WZTask.taskID>>, And<WZTask.status, NotEqual<WizardTaskStatusesAttribute.Disabled>>>, OrderBy<Asc<WZTask.position>>>.Config>.Select((PXGraph) wizardScenarioMaint2, objArray))
    {
      WZTask task = PXResult<WZTask>.op_Implicit(pxResult);
      task.Offset = new int?(level);
      yield return (object) task;
      foreach (WZTask childTask in wizardScenarioMaint1.GetChildTasks(task.TaskID, level + 1))
        yield return (object) childTask;
      task = (WZTask) null;
    }
  }

  [PXUIField(DisplayName = "Start Task")]
  [PXButton]
  public virtual IEnumerable StartTask(PXAdapter adapter)
  {
    WZTaskEntry instance = PXGraph.CreateInstance<WZTaskEntry>();
    WZTask current = ((PXSelectBase<WZTask>) this.Tasks).Current;
    if (current != null && current.Status == "AC")
    {
      ((PXAction) this.viewTask).Press();
      return adapter.Get();
    }
    if (current != null && current.Status == "OP")
    {
      current.Status = "AC";
      ((PXSelectBase<WZTask>) instance.TaskInfo).Update(current);
      ((PXAction) instance.Save).Press();
      ((PXAction) this.viewTask).Press();
    }
    ((PXSelectBase) this.Tasks).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.Scenario).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.Scenario).Cache.Clear();
    ((PXSelectBase) this.Scenario).View.RequestRefresh();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Reopen")]
  [PXButton]
  public virtual IEnumerable Reopen(PXAdapter adapter)
  {
    WZTaskEntry instance = PXGraph.CreateInstance<WZTaskEntry>();
    WZTask current = ((PXSelectBase<WZTask>) this.Tasks).Current;
    if (current != null && (current.Status == "CP" || current.Status == "SK"))
    {
      current.Status = "OP";
      ((PXSelectBase<WZTask>) instance.TaskInfo).Update(current);
      ((PXAction) instance.Save).Press();
    }
    ((PXSelectBase) this.Tasks).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.Scenario).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.Scenario).Cache.Clear();
    ((PXSelectBase) this.Scenario).View.RequestRefresh();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Skip")]
  [PXButton]
  public virtual IEnumerable Skip(PXAdapter adapter)
  {
    WZTaskEntry instance = PXGraph.CreateInstance<WZTaskEntry>();
    WZTask current = ((PXSelectBase<WZTask>) this.Tasks).Current;
    if (current != null && current.IsOptional.GetValueOrDefault())
    {
      current.Status = "SK";
      ((PXSelectBase<WZTask>) instance.TaskInfo).Update(current);
      ((PXAction) instance.Save).Press();
    }
    ((PXSelectBase) this.Tasks).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.Scenario).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.Scenario).Cache.Clear();
    ((PXSelectBase) this.Scenario).View.RequestRefresh();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Assign")]
  [PXButton]
  public virtual IEnumerable Assign(PXAdapter adapter)
  {
    if (this.CurrentTask.AskExtRequired((WebDialogResult) 1, true))
    {
      if (((PXSelectBase<WZTaskAssign>) this.CurrentTask).Current.AssignedTo.HasValue)
      {
        WZTaskEntry instance = PXGraph.CreateInstance<WZTaskEntry>();
        WZTask wzTask1 = PXResultset<WZTask>.op_Implicit(PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.taskID, Equal<Required<WZTask.taskID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) ((PXSelectBase<WZTaskAssign>) this.CurrentTask).Current.TaskID
        }));
        if (wzTask1 != null)
        {
          WZTask copy1 = (WZTask) ((PXSelectBase) instance.TaskInfo).Cache.CreateCopy((object) wzTask1);
          copy1.AssignedTo = ((PXSelectBase<WZTaskAssign>) this.CurrentTask).Current.AssignedTo;
          ((PXSelectBase<WZTask>) instance.TaskInfo).Update(copy1);
          ((PXSelectBase<WZTask>) instance.TaskInfo).Current = wzTask1;
          ((PXSelectBase<WZScenario>) instance.Scenario).Current = ((PXSelectBase<WZScenario>) this.Scenario).Current;
          if (((PXSelectBase<WZTaskAssign>) this.CurrentTask).Current.OverrideAssignee.GetValueOrDefault())
          {
            foreach (PXResult<WZTask> pxResult in ((PXSelectBase<WZTask>) instance.Childs).Select(Array.Empty<object>()))
            {
              WZTask wzTask2 = PXResult<WZTask>.op_Implicit(pxResult);
              WZTask copy2 = (WZTask) ((PXSelectBase) instance.TaskInfo).Cache.CreateCopy((object) wzTask2);
              copy2.AssignedTo = ((PXSelectBase<WZTaskAssign>) this.CurrentTask).Current.AssignedTo;
              ((PXSelectBase<WZTask>) instance.TaskInfo).Update(copy2);
            }
          }
          ((PXAction) instance.Save).Press();
          ((PXSelectBase) this.CurrentTask).Cache.IsDirty = false;
        }
      }
      ((PXSelectBase) this.Tasks).Cache.ClearQueryCacheObsolete();
      ((PXSelectBase) this.CurrentTask).Cache.SetDefaultExt<WZTaskAssign.overrideAssignee>((object) ((PXSelectBase<WZTaskAssign>) this.CurrentTask).Current);
      ((PXSelectBase) this.CurrentTask).Cache.ClearQueryCacheObsolete();
      ((PXSelectBase) this.CurrentTask).View.RequestRefresh();
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Task")]
  [PXButton]
  public virtual IEnumerable ViewTask(PXAdapter adapter)
  {
    WZTask wzTask = PXResultset<WZTask>.op_Implicit(PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.taskID, Equal<Current<WZTask.taskID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (wzTask == null)
      return adapter.Get();
    PXSiteMapNode siteMapNode = PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, typeof (WizardArticleMaint));
    if (siteMapNode == null)
      throw new PXException("You have no access rights to the Wizard Article Screen WZ.20.15.10");
    throw new PXRedirectToUrlException($"{siteMapNode.Url}?TaskID={wzTask.TaskID.ToString()}", (PXBaseRedirectException.WindowMode) 4, wzTask.Name);
  }

  [PXDBGuid(false, IsKey = true)]
  [PXUIField]
  [PXDefault]
  [PXParent(typeof (Select<WZTask, Where<WZTask.taskID, Equal<Current<WZTaskPredecessorRelation.predecessorID>>>>))]
  [PXSelector(typeof (Search<WZTask.taskID, Where<WZTask.scenarioID, Equal<Current<WZTaskPredecessorRelation.scenarioID>>, And<WZTask.taskID, NotEqual<Current<WZTask.taskID>>>>>), SubstituteKey = typeof (WZTask.name))]
  protected virtual void WZTaskPredecessorRelation_PredecessorID_CacheAttached(PXCache sender)
  {
  }

  public WizardScenarioMaint()
  {
    ((PXAction) this.Action).AddMenuAction((PXAction) this.assign);
    ((PXAction) this.Action).AddMenuAction((PXAction) this.reopen);
    ((PXAction) this.Action).AddMenuAction((PXAction) this.completeScenario);
  }
}
