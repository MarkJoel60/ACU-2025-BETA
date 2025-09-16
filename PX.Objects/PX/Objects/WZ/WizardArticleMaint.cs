// Decompiled with JetBrains decompiler
// Type: PX.Objects.WZ.WizardArticleMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Specialized;

#nullable disable
namespace PX.Objects.WZ;

public class WizardArticleMaint : PXGraph<WizardArticleMaint>
{
  public PXSelect<WZTask> Tasks;
  public PXFilter<WZTaskAssign> CurrentTask;
  public PXSelect<WZSubTask, Where<WZSubTask.parentTaskID, Equal<Current<WZTask.taskID>>>> SubTasks;
  public PXSelectJoin<WZTaskPredecessorRelation, InnerJoin<WZTask, On<WZTask.taskID, Equal<WZTaskPredecessorRelation.predecessorID>>>, Where<WZTaskPredecessorRelation.taskID, Equal<Current<WZTask.taskID>>>> Predecessors;
  public PXSelectJoin<WZTaskSuccessorRelation, InnerJoin<WZTask, On<WZTask.taskID, Equal<WZTaskSuccessorRelation.taskID>>>, Where<WZTaskSuccessorRelation.predecessorID, Equal<Current<WZTask.taskID>>>> Successors;
  public PXCancel<WZTask> cancel;
  public PXAction<WZTask> startTask;
  public PXAction<WZTask> skip;
  public PXAction<WZTask> skipSubtask;
  public PXAction<WZTask> markAsComplete;
  public PXAction<WZTask> goToScreen;
  public PXAction<WZTask> assign;
  public PXAction<WZTask> viewPredecessorTask;
  public PXAction<WZTask> viewBlockedTask;
  public PXAction<WZTask> viewSubTask;

  protected virtual void WZTask_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is WZTask row))
      return;
    if (((OrderedDictionary) ((PXGraph) this).Actions).Contains((object) "CancelClose"))
      ((PXGraph) this).Actions["CancelClose"].SetTooltip("Back to Scenario");
    PXUIFieldAttribute.SetEnabled<WZTask.details>(((PXSelectBase) this.Tasks).Cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<WZTask.assignedTo>(((PXSelectBase) this.Tasks).Cache, (object) row, false);
    bool flag = row.Status == "AC" || row.Status == "OP";
    int num = row.Status == "CP" ? 1 : 0;
    ((PXAction) this.markAsComplete).SetEnabled(flag);
    ((PXAction) this.skip).SetVisible(false);
    if (row.IsOptional.GetValueOrDefault())
    {
      ((PXAction) this.skip).SetVisible(true);
      ((PXAction) this.skip).SetEnabled(!(row.Status == "SK") && !(row.Status == "CP"));
    }
    ((PXAction) this.startTask).SetEnabled(row.Status == "OP");
    ((PXSelectBase) this.SubTasks).AllowUpdate = false;
    ((PXSelectBase) this.Predecessors).AllowUpdate = false;
    ((PXSelectBase) this.Successors).AllowUpdate = false;
    PXResultset<WZTaskPredecessorRelation> pxResultset1 = ((PXSelectBase<WZTaskPredecessorRelation>) this.Predecessors).Select(Array.Empty<object>());
    PXResultset<WZTaskSuccessorRelation> pxResultset2 = ((PXSelectBase<WZTaskSuccessorRelation>) this.Successors).Select(Array.Empty<object>());
    PXResultset<WZSubTask> pxResultset3 = ((PXSelectBase<WZSubTask>) this.SubTasks).Select(Array.Empty<object>());
    if (pxResultset1.Count == 0)
      ((PXSelectBase) this.Predecessors).Cache.AllowSelect = false;
    if (pxResultset2.Count == 0)
      ((PXSelectBase) this.Successors).Cache.AllowSelect = false;
    if (pxResultset3.Count == 0)
      ((PXSelectBase) this.SubTasks).Cache.AllowSelect = false;
    ((PXAction) this.goToScreen).SetVisible(row.ScreenID != null);
  }

  [PXSuppressActionValidation]
  [PXDBGuid(false, IsKey = true)]
  [PXUIField]
  [PXSelector(typeof (Search<WZTask.taskID, Where<WZTask.scenarioID, Equal<Current<WZTaskPredecessorRelation.scenarioID>>, And<WZTask.taskID, NotEqual<Current<WZTask.taskID>>>>>), SubstituteKey = typeof (WZTask.name))]
  protected virtual void WZTaskPredecessorRelation_PredecessorID_CacheAttached(PXAdapter adapter)
  {
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable GoToScreen(PXAdapter adapter)
  {
    WZTask wzTask = PXResultset<WZTask>.op_Implicit(PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.taskID, Equal<Current<WZTask.taskID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (wzTask == null || wzTask.ScreenID == null)
      return adapter.Get();
    PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(wzTask.ScreenID);
    if (wzTask.ImportScenarioID.HasValue)
    {
      SYMappingActive syMappingActive = PXResultset<SYMappingActive>.op_Implicit(PXSelectBase<SYMappingActive, PXSelect<SYMappingActive, Where<SYMapping.mappingID, Equal<Required<SYMapping.mappingID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) wzTask.ImportScenarioID
      }));
      if (syMappingActive != null)
      {
        SYImportProcessSingle instance = PXGraph.CreateInstance<SYImportProcessSingle>();
        ((PXSelectBase<SYMappingActive>) instance.MappingsSingle).Current = syMappingActive;
        throw new PXRedirectRequiredException((PXGraph) instance, true, wzTask.Name);
      }
      return adapter.Get();
    }
    if (mapNodeByScreenId.GraphType != null && GraphHelper.GetType(mapNodeByScreenId.GraphType) == typeof (FeaturesMaint))
    {
      FeaturesMaint instance = PXGraph.CreateInstance<FeaturesMaint>();
      ((PXSelectBase<FeaturesSet>) instance.Features).Current = PXResultset<FeaturesSet>.op_Implicit(((PXSelectBase<FeaturesSet>) instance.Features).Select(Array.Empty<object>()));
      ((PXSelectBase) instance.ActivationBehaviour).Cache.SetValueExt<AfterActivation.refresh>((object) ((PXSelectBase<AfterActivation>) instance.ActivationBehaviour).Current, (object) false);
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 4);
    }
    throw new PXRedirectToUrlException(mapNodeByScreenId.Url, (PXBaseRedirectException.WindowMode) 3, wzTask.Name);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable MarkAsComplete(PXAdapter adapter)
  {
    WZTaskEntry instance = PXGraph.CreateInstance<WZTaskEntry>();
    WZTask current = ((PXSelectBase<WZTask>) this.Tasks).Current;
    if (current != null && current.Status != "PN" && current.Status != "DS")
    {
      if (!instance.CanTaskBeCompleted(current))
        throw new PXException("Task cannot be completed because there are still Open/In Progress subtasks.");
      current.Status = "CP";
      ((PXSelectBase<WZTask>) instance.TaskInfo).Update(current);
      ((PXAction) instance.Save).Press();
    }
    ((PXSelectBase) this.Tasks).Cache.ClearQueryCacheObsolete();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Skip")]
  [PXButton]
  public virtual IEnumerable Skip(PXAdapter adapter)
  {
    WZTask current = ((PXSelectBase<WZTask>) this.Tasks).Current;
    WZTaskEntry instance = PXGraph.CreateInstance<WZTaskEntry>();
    if (current != null && (current.Status == "OP" || current.Status == "PN" || current.Status == "AC") && current.IsOptional.GetValueOrDefault())
    {
      current.Status = "SK";
      ((PXSelectBase<WZTask>) instance.TaskInfo).Update(current);
      ((PXGraph) instance).Actions.PressSave();
      ((PXSelectBase) this.Tasks).Cache.Clear();
      ((PXSelectBase) this.Tasks).Cache.ClearQueryCacheObsolete();
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Skip")]
  [PXButton]
  public virtual IEnumerable SkipSubtask(PXAdapter adapter)
  {
    WZTask current = (WZTask) ((PXSelectBase<WZSubTask>) this.SubTasks).Current;
    WZTaskEntry instance = PXGraph.CreateInstance<WZTaskEntry>();
    if (current != null && (current.Status == "OP" || current.Status == "PN" || current.Status == "AC") && current.IsOptional.GetValueOrDefault())
    {
      current.Status = "SK";
      ((PXSelectBase<WZTask>) instance.TaskInfo).Update(current);
      ((PXGraph) instance).Actions.PressSave();
      ((PXSelectBase) this.Tasks).Cache.Clear();
      ((PXSelectBase) this.Tasks).Cache.ClearQueryCacheObsolete();
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Start Task")]
  [PXButton]
  public virtual IEnumerable StartTask(PXAdapter adapter)
  {
    WZTask current = ((PXSelectBase<WZTask>) this.Tasks).Current;
    WZTaskEntry instance = PXGraph.CreateInstance<WZTaskEntry>();
    if (current != null && current.Status == "OP")
    {
      current.Status = "AC";
      ((PXSelectBase<WZTask>) instance.TaskInfo).Update(current);
      ((PXAction) instance.Save).Press();
      ((PXSelectBase) this.Tasks).Cache.Clear();
      ((PXSelectBase) this.Tasks).Cache.ClearQueryCacheObsolete();
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Assign")]
  [PXButton]
  public virtual IEnumerable Assign(PXAdapter adapter)
  {
    if (((PXSelectBase<WZTaskAssign>) this.CurrentTask).AskExt(true) == 1)
    {
      if (((PXSelectBase<WZTaskAssign>) this.CurrentTask).Current.AssignedTo.HasValue)
      {
        WZTaskEntry instance = PXGraph.CreateInstance<WZTaskEntry>();
        WZTask current = ((PXSelectBase<WZTask>) this.Tasks).Current;
        if (current != null)
        {
          current.AssignedTo = ((PXSelectBase<WZTaskAssign>) this.CurrentTask).Current.AssignedTo;
          ((PXSelectBase<WZTask>) instance.TaskInfo).Update(current);
          ((PXGraph) instance).Actions.PressSave();
        }
      }
      ((PXSelectBase) this.Tasks).Cache.ClearQueryCacheObsolete();
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Blocked Task")]
  [PXButton]
  public virtual IEnumerable ViewBlockedTask(PXAdapter adapter)
  {
    WZTaskSuccessorRelation current = ((PXSelectBase<WZTaskSuccessorRelation>) this.Successors).Current;
    if (current == null)
      return adapter.Get();
    WZTask wzTask = PXResultset<WZTask>.op_Implicit(PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.taskID, Equal<Required<WZTask.taskID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) current.TaskID
    }));
    if (wzTask == null)
      return adapter.Get();
    throw new PXRedirectToUrlException($"{PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, typeof (WizardArticleMaint)).Url}?TaskID={wzTask.TaskID.ToString()}", (PXBaseRedirectException.WindowMode) 4, wzTask.Name);
  }

  [PXUIField(DisplayName = "View Predecessor Task")]
  [PXButton]
  public virtual IEnumerable ViewPredecessorTask(PXAdapter adapter)
  {
    WZTaskPredecessorRelation current = ((PXSelectBase<WZTaskPredecessorRelation>) this.Predecessors).Current;
    if (current == null)
      return adapter.Get();
    WZTask wzTask = PXResultset<WZTask>.op_Implicit(PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.taskID, Equal<Required<WZTask.taskID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) current.PredecessorID
    }));
    if (wzTask == null)
      return adapter.Get();
    throw new PXRedirectToUrlException($"{PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, typeof (WizardArticleMaint)).Url}?TaskID={wzTask.TaskID.ToString()}", (PXBaseRedirectException.WindowMode) 4, wzTask.Name);
  }

  [PXUIField(DisplayName = "View Subtask")]
  [PXButton]
  public virtual IEnumerable ViewSubTask(PXAdapter adapter)
  {
    WZTask current = (WZTask) ((PXSelectBase<WZSubTask>) this.SubTasks).Current;
    if (current == null)
      return adapter.Get();
    throw new PXRedirectToUrlException($"{PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, typeof (WizardArticleMaint)).Url}?TaskID={current.TaskID.ToString()}", (PXBaseRedirectException.WindowMode) 4, current.Name);
  }
}
