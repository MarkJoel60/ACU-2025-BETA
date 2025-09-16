// Decompiled with JetBrains decompiler
// Type: PX.Objects.WZ.WZTaskEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.DependencyInjection;
using PX.Licensing;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.WZ;

public class WZTaskEntry : PXGraph<WZTaskEntry>, IGraphWithInitialization
{
  public PXSelect<WZScenario> Scenario;
  public PXSelect<WZTaskTreeItem, Where<WZTaskTreeItem.taskID, IsNotNull, And<WZTaskTreeItem.parentTaskID, Equal<Argument<Guid?>>>>, OrderBy<Asc<WZTaskTreeItem.position>>> TasksTreeItems;
  [PXCopyPasteHiddenFields(new Type[] {typeof (WZTask.details)})]
  public PXSelect<WZTask, Where<WZTask.taskID, Equal<Current<WZTaskTreeItem.taskID>>>> TaskInfo;
  public PXSelectJoin<WZTaskPredecessorRelation, InnerJoin<WZTask, On<WZTask.taskID, Equal<WZTaskPredecessorRelation.predecessorID>>>, Where<WZTaskPredecessorRelation.taskID, Equal<Current<WZTask.taskID>>, And<WZTaskPredecessorRelation.scenarioID, Equal<Current<WZScenario.scenarioID>>>>> Predecessors;
  public PXSelectJoin<WZTaskSuccessorRelation, InnerJoin<WZTask, On<WZTask.taskID, Equal<WZTaskSuccessorRelation.taskID>>>, Where<WZTaskSuccessorRelation.predecessorID, Equal<Current<WZTask.taskID>>, And<WZTaskSuccessorRelation.scenarioID, Equal<Current<WZScenario.scenarioID>>>>> Successors;
  public PXSelect<WZTask> Childs;
  public PXSelectOrderBy<WZTaskFeature, OrderBy<Asc<WZTaskFeature.order>>> Features;
  public PXSelectSiteMapTree<False, False, False, False, False> SiteMap;
  private Dictionary<string, string> FeaturesNames = new Dictionary<string, string>();
  private Dictionary<string, string> FeatureDependsOn = new Dictionary<string, string>();
  private Dictionary<string, int> FeatureOffset = new Dictionary<string, int>();
  private Dictionary<string, int> FeatureOrder = new Dictionary<string, int>();
  public PXCancel<WZScenario> Cancel;
  public PXSave<WZScenario> Save;
  public PXAction<WZScenario> activateScenario;
  public PXAction<WZScenario> activateScenarioWithoutRefresh;
  public PXAction<WZScenario> prepareTasksForActivation;
  public PXAction<WZScenario> suspendScenario;
  public PXAction<WZScenario> completeScenario;
  public PXAction<WZScenario> completeScenarioWithoutRefresh;
  public PXAction<WZScenario> deleteTask;
  public PXAction<WZScenario> addTask;
  public PXAction<WZScenario> up;
  public PXAction<WZScenario> down;
  public PXAction<WZScenario> left;
  public PXAction<WZScenario> right;

  [InjectDependency]
  private ILicensing _licensing { get; set; }

  void IGraphWithInitialization.Initialize()
  {
    Type type = ((object) new FeaturesSet()).GetType();
    PXLicense license = this._licensing.License;
    List<string> stringList = new List<string>();
    Dictionary<string, List<string>> subfeatures = new Dictionary<string, List<string>>();
    PXCache cache = (PXCache) new PXCache<FeaturesSet>((PXGraph) this);
    foreach (var data in ((IEnumerable<string>) cache.Fields).SelectMany((Func<string, IEnumerable<PXEventSubscriberAttribute>>) (f => cache.GetAttributes(cache.Current, f).Where<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (atr => atr is FeatureAttribute))), (f, atr) => new
    {
      f = f,
      atr = atr
    }))
    {
      PXFieldState stateExt1 = cache.GetStateExt((object) null, data.f) as PXFieldState;
      FeatureAttribute atr = data.atr as FeatureAttribute;
      if (stateExt1 != null && stateExt1.Visible)
      {
        if (atr != null && atr.Top)
          stringList.Add(stateExt1.Name);
        if (atr != null && atr.Parent != (Type) null)
        {
          Type parent = atr.Parent;
          string field = ((PXGraph) this).Caches[type].GetField(parent);
          PXFieldState stateExt2 = ((PXGraph) this).Caches[type].GetStateExt((object) null, field) as PXFieldState;
          if (!atr.Top)
          {
            if (!subfeatures.ContainsKey(stateExt2.Name))
              subfeatures.Add(stateExt2.Name, new List<string>());
            subfeatures[stateExt2.Name].Add(stateExt1.Name);
          }
          if (license.Licensed && !PXAccess.BypassLicense)
          {
            if (((PXLicenseDefinition) license).Features.Contains($"{type?.ToString()}+{stateExt2.Name}"))
              this.FeatureDependsOn.Add(stateExt1.Name, stateExt2.Name);
          }
          else
            this.FeatureDependsOn.Add(stateExt1.Name, stateExt2.Name);
        }
        if (license.Licensed && !PXAccess.BypassLicense)
        {
          if (((PXLicenseDefinition) license).Features.Contains($"{type?.ToString()}+{stateExt1.Name}"))
            this.FeaturesNames.Add(stateExt1.Name, stateExt1.DisplayName);
        }
        else
          this.FeaturesNames.Add(stateExt1.Name, stateExt1.DisplayName);
      }
    }
    List<string> features = new List<string>();
    foreach (string key in this.FeaturesNames.Keys)
    {
      if (!this.FeatureDependsOn.ContainsKey(key))
        features.Add(key);
    }
    foreach (string str in stringList)
    {
      if (!features.Contains(str))
        features.Add(str);
    }
    this.FillFeatureHierarchy(0, 0, (IEnumerable<string>) features, subfeatures);
    ((PXSelectBase) this.TaskInfo).Cache.AllowInsert = false;
    ((PXAction) this.deleteTask).SetEnabled(false);
    ((PXAction) this.addTask).SetEnabled(false);
    ((PXAction) this.up).SetEnabled(false);
    ((PXAction) this.down).SetEnabled(false);
    ((PXAction) this.left).SetEnabled(false);
    ((PXAction) this.right).SetEnabled(false);
  }

  [PXDBGuid(false, IsKey = true)]
  [PXSelector(typeof (WZScenario.scenarioID), DescriptionField = typeof (WZScenario.name))]
  [PXUIField]
  protected virtual void WZScenario_ScenarioID_CacheAttached(PXCache sender)
  {
  }

  protected virtual IEnumerable tasksTreeItems([PXDBGuid(false)] Guid? taskID)
  {
    List<WZTaskTreeItem> wzTaskTreeItemList = new List<WZTaskTreeItem>();
    if (!taskID.HasValue)
      taskID = new Guid?(Guid.Empty);
    foreach (PXResult<WZTask> pxResult in (IEnumerable) PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.scenarioID, Equal<Current<WZScenario.scenarioID>>, And<WZTask.parentTaskID, Equal<Required<WZTask.taskID>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) taskID
    }))
    {
      WZTask wzTask = PXResult<WZTask>.op_Implicit(pxResult);
      WZTaskTreeItem wzTaskTreeItem = new WZTaskTreeItem()
      {
        ScenarioID = wzTask.ScenarioID,
        TaskID = wzTask.TaskID,
        ParentTaskID = wzTask.ParentTaskID,
        Position = wzTask.Position,
        Name = wzTask.Name
      };
      if (PXResultset<WZTask>.op_Implicit(PXSelectBase<WZTask, PXSelectReadonly<WZTask, Where<WZTask.scenarioID, Equal<Current<WZScenario.scenarioID>>, And<WZTask.taskID, Equal<Required<WZTask.taskID>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) wzTask.TaskID
      })) == null)
        ((PXSelectBase) this.TasksTreeItems).Cache.ActiveRow = (IBqlTable) wzTaskTreeItem;
      wzTaskTreeItemList.Add(wzTaskTreeItem);
    }
    ((PXSelectBase) this.Childs).View.RequestRefresh();
    return (IEnumerable) wzTaskTreeItemList;
  }

  protected virtual IEnumerable childs()
  {
    List<WZTask> wzTaskList = new List<WZTask>();
    if (((PXSelectBase<WZTask>) this.TaskInfo).Current != null)
      wzTaskList = this.GetChildTasks(((PXSelectBase<WZTask>) this.TaskInfo).Current.TaskID);
    return (IEnumerable) wzTaskList;
  }

  protected virtual IEnumerable features()
  {
    List<WZTaskFeature> wzTaskFeatureList = new List<WZTaskFeature>();
    if (((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current != null)
    {
      Guid? taskId = ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.TaskID;
      Guid empty = Guid.Empty;
      if ((taskId.HasValue ? (taskId.GetValueOrDefault() == empty ? 1 : 0) : 0) == 0)
      {
        taskId = ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.TaskID;
        if (taskId.HasValue)
        {
          bool flag = false;
          foreach (string key in this.FeaturesNames.Keys)
          {
            WZTaskFeature wzTaskFeature1 = PXResultset<WZTaskFeature>.op_Implicit(PXSelectBase<WZTaskFeature, PXSelect<WZTaskFeature, Where<WZTaskFeature.taskID, Equal<Required<WZTaskFeature.taskID>>, And<WZTaskFeature.feature, Equal<Required<WZTaskFeature.feature>>>>>.Config>.Select((PXGraph) this, new object[2]
            {
              (object) ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.TaskID,
              (object) key
            }));
            if (wzTaskFeature1 == null)
            {
              WZTaskFeature wzTaskFeature2 = new WZTaskFeature()
              {
                ScenarioID = ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.ScenarioID,
                TaskID = ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.TaskID,
                Feature = key,
                DisplayName = this.FeaturesNames[key],
                Required = new bool?(false),
                Offset = new int?(this.FeatureOffset[key]),
                Order = new int?(this.FeatureOrder[key])
              };
              ((PXSelectBase<WZTaskFeature>) this.Features).Insert(wzTaskFeature2);
              if (((PXSelectBase) this.Features).Cache.GetStatus((object) wzTaskFeature2) == 1)
                flag = true;
              wzTaskFeatureList.Add(wzTaskFeature2);
            }
            else
            {
              wzTaskFeature1.Offset = new int?(this.FeatureOffset[key]);
              wzTaskFeature1.Order = new int?(this.FeatureOrder[key]);
              if (((PXSelectBase) this.Features).Cache.GetStatus((object) wzTaskFeature1) != 2 && ((PXSelectBase) this.Features).Cache.GetStatus((object) wzTaskFeature1) != 1 && ((PXSelectBase) this.Features).Cache.GetStatus((object) wzTaskFeature1) != null)
              {
                wzTaskFeature1.Required = new bool?(true);
                wzTaskFeature1.DisplayName = this.FeaturesNames[key];
              }
              if (((PXSelectBase) this.Features).Cache.GetStatus((object) wzTaskFeature1) == 1)
              {
                wzTaskFeature1.DisplayName = this.FeaturesNames[key];
                flag = true;
              }
              if (((PXSelectBase) this.Features).Cache.GetStatus((object) wzTaskFeature1) == 2 && wzTaskFeature1.Required.GetValueOrDefault())
              {
                wzTaskFeature1.DisplayName = this.FeaturesNames[key];
                flag = true;
              }
              if (((PXSelectBase) this.Features).Cache.GetStatus((object) wzTaskFeature1) == null)
              {
                if (PXResultset<WZTaskFeature>.op_Implicit(PXSelectBase<WZTaskFeature, PXSelectReadonly<WZTaskFeature, Where<WZTaskFeature.taskID, Equal<Required<WZTaskFeature.taskID>>, And<WZTaskFeature.feature, Equal<Required<WZTaskFeature.feature>>>>>.Config>.Select((PXGraph) this, new object[2]
                {
                  (object) wzTaskFeature1.TaskID,
                  (object) wzTaskFeature1.Feature
                })) != null)
                {
                  wzTaskFeature1.Required = new bool?(true);
                  wzTaskFeature1.DisplayName = this.FeaturesNames[key];
                }
              }
              wzTaskFeatureList.Add(wzTaskFeature1);
            }
          }
          ((PXSelectBase) this.Features).Cache.IsDirty = flag;
          return (IEnumerable) wzTaskFeatureList;
        }
      }
    }
    return (IEnumerable) wzTaskFeatureList;
  }

  protected virtual void WZScenario_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    ((PXAction) this.activateScenario).SetEnabled(false);
    ((PXAction) this.completeScenario).SetEnabled(false);
    ((PXAction) this.suspendScenario).SetEnabled(false);
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void WZScenario_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is WZScenario row))
      return;
    ((PXAction) this.deleteTask).SetEnabled(true);
    ((PXAction) this.addTask).SetEnabled(true);
    ((PXAction) this.up).SetEnabled(true);
    ((PXAction) this.down).SetEnabled(true);
    ((PXAction) this.left).SetEnabled(true);
    ((PXAction) this.right).SetEnabled(true);
    bool flag1 = row.Status != "AC";
    bool flag2 = row.Status == "AC";
    bool flag3 = row.Status != "SU";
    bool flag4 = row.Status != "AC" && row.Status != "SU";
    ((PXSelectBase) this.Successors).Cache.AllowUpdate = ((PXSelectBase) this.Successors).Cache.AllowDelete = ((PXSelectBase) this.Successors).Cache.AllowInsert = false;
    ((PXSelectBase) this.Predecessors).Cache.AllowUpdate = ((PXSelectBase) this.Features).Cache.AllowUpdate = flag4;
    ((PXSelectBase) this.Predecessors).Cache.AllowDelete = ((PXSelectBase) this.Features).Cache.AllowDelete = flag4;
    ((PXSelectBase) this.Predecessors).Cache.AllowInsert = ((PXSelectBase) this.Features).Cache.AllowInsert = flag4;
    ((PXAction) this.activateScenario).SetEnabled(flag1);
    ((PXAction) this.completeScenario).SetEnabled(flag2);
    ((PXAction) this.suspendScenario).SetEnabled(flag3);
    PXResultset<WZTask> pxResultset = PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.scenarioID, Equal<Required<WZScenario.scenarioID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ScenarioID
    });
    if (pxResultset == null)
      return;
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.TaskInfo).Cache, (string) null, pxResultset.Count != 0);
  }

  protected virtual void WZTask_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    WZTask row = e.Row as WZTask;
    WZScenario current = ((PXSelectBase<WZScenario>) this.Scenario).Current;
    if (row == null || current == null)
      return;
    int num = 0;
    row.Position = new int?(num);
    if (((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current == null)
      return;
    foreach (WZTaskTreeItem tasksTreeItem in this.tasksTreeItems(((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.TaskID))
    {
      int? position = tasksTreeItem.Position;
      if (position.Value > num)
      {
        position = tasksTreeItem.Position;
        num = position.Value;
      }
      row.Position = new int?(num + 1);
    }
  }

  protected virtual void WZTask_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    WZTask row = (WZTask) e.Row;
  }

  protected virtual void WZTask_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    WZTask row = (WZTask) e.Row;
  }

  protected virtual void WZTask_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    WZTask row = (WZTask) e.Row;
  }

  protected virtual void WZTask_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row is WZTask row && ((PXSelectBase<WZScenario>) this.Scenario).Current != null)
    {
      ((PXSelectBase) this.Scenario).Cache.AllowUpdate = true;
      ((PXSelectBase<WZTask>) this.TaskInfo).Current.TaskID = row.TaskID;
      Guid? parentTaskId = row.ParentTaskID;
      Guid empty = Guid.Empty;
      if ((parentTaskId.HasValue ? (parentTaskId.GetValueOrDefault() == empty ? 1 : 0) : 0) != 0)
        row.Type = "AR";
    }
    ((PXSelectBase) this.TasksTreeItems).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.TasksTreeItems).View.RequestRefresh();
  }

  protected virtual void WZTask_TaskID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is WZTask row))
      return;
    row.ScenarioID = ((PXSelectBase<WZScenario>) this.Scenario).Current.ScenarioID;
    row.TaskID = new Guid?(Guid.NewGuid());
    row.ParentTaskID = new Guid?(Guid.Empty);
    if (((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current == null)
      return;
    Guid? taskId = ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.TaskID;
    Guid empty = Guid.Empty;
    if ((taskId.HasValue ? (taskId.GetValueOrDefault() != empty ? 1 : 0) : 1) == 0)
      return;
    row.ParentTaskID = ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.TaskID;
  }

  protected virtual void WZTask_Name_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is WZTask row))
      return;
    row.Name = "New Task";
  }

  protected virtual void WZTask_Status_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is WZTask row))
      return;
    if (row.Status == "CP")
    {
      row.CompletedBy = ((PXGraph) this).Accessinfo.ContactID;
      row.CompletedDate = new DateTime?(PXTimeZoneInfo.Now);
    }
    if (row.Status == "AC")
    {
      row.StartedDate = new DateTime?(PXTimeZoneInfo.Now);
      row.CompletedDate = new DateTime?();
    }
    if (row.Status == "SK")
    {
      row.CompletedBy = ((PXGraph) this).Accessinfo.ContactID;
      row.CompletedDate = new DateTime?(PXTimeZoneInfo.Now);
    }
    Dictionary<Guid, WZTask> tasksToOpen = this.GetTasksToOpen(row);
    Dictionary<Guid, WZTask> tasksToSkip = this.GetTasksToSkip(row);
    Dictionary<Guid, WZTask> tasksToDisable = this.GetTasksToDisable(row);
    Dictionary<Guid, WZTask> tasksToEnable = this.GetTasksToEnable(row);
    foreach (WZTask wzTask in tasksToOpen.Values)
      ((PXSelectBase<WZTask>) this.TaskInfo).Update(wzTask);
    foreach (WZTask wzTask in tasksToSkip.Values)
      ((PXSelectBase<WZTask>) this.TaskInfo).Update(wzTask);
    foreach (WZTask wzTask in tasksToDisable.Values)
      ((PXSelectBase<WZTask>) this.TaskInfo).Update(wzTask);
    foreach (WZTask wzTask in tasksToEnable.Values)
      ((PXSelectBase<WZTask>) this.TaskInfo).Update(wzTask);
    if (!this.ScenarioTasksCompleted(row.ScenarioID))
      return;
    if (((PXSelectBase<WZScenario>) this.Scenario).Current == null)
      ((PXSelectBase<WZScenario>) this.Scenario).Current = PXResultset<WZScenario>.op_Implicit(PXSelectBase<WZScenario, PXSelect<WZScenario, Where<WZScenario.scenarioID, Equal<Required<WZScenario.scenarioID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.ScenarioID
      }));
    ((PXAction) this.completeScenarioWithoutRefresh).Press();
  }

  public bool ScenarioTasksCompleted(Guid? scenarioID)
  {
    PXResultset<WZTask> pxResultset = PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.scenarioID, Equal<Required<WZTask.scenarioID>>, And<Where<WZTask.status, Equal<WizardTaskStatusesAttribute.Open>, Or<WZTask.status, Equal<WizardTaskStatusesAttribute.Pending>, Or<WZTask.status, Equal<WizardTaskStatusesAttribute.Active>>>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) scenarioID
    });
    return pxResultset == null || pxResultset.Count <= 0;
  }

  public bool CanTaskBeCompleted(WZTask selectedTask)
  {
    foreach (PXResult<WZTask> pxResult in PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.scenarioID, Equal<Required<WZTask.scenarioID>>, And<WZTask.parentTaskID, Equal<Required<WZTask.taskID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) selectedTask.ScenarioID,
      (object) selectedTask.TaskID
    }))
    {
      WZTask selectedTask1 = PXResult<WZTask>.op_Implicit(pxResult);
      if (selectedTask1.Status == "OP" || selectedTask1.Status == "AC" || !this.CanTaskBeCompleted(selectedTask1))
        return false;
    }
    return true;
  }

  private Dictionary<Guid, WZTask> GetTasksToOpen(WZTask selectedTask)
  {
    Dictionary<Guid, WZTask> tasksToOpen = new Dictionary<Guid, WZTask>();
    if (selectedTask.Status == "OP")
    {
      foreach (PXResult<WZTask> pxResult1 in PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.scenarioID, Equal<Required<WZTask.scenarioID>>, And<WZTask.parentTaskID, Equal<Required<WZTask.taskID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) selectedTask.ScenarioID,
        (object) selectedTask.TaskID
      }))
      {
        WZTask wzTask1 = PXResult<WZTask>.op_Implicit(pxResult1);
        bool flag = true;
        foreach (PXResult<WZTaskPredecessorRelation, WZTask> pxResult2 in PXSelectBase<WZTaskPredecessorRelation, PXSelectJoin<WZTaskPredecessorRelation, InnerJoin<WZTask, On<WZTask.taskID, Equal<WZTaskPredecessorRelation.predecessorID>>>, Where<WZTaskPredecessorRelation.taskID, Equal<Required<WZTask.taskID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) wzTask1.TaskID
        }))
        {
          WZTask wzTask2 = PXResult<WZTaskPredecessorRelation, WZTask>.op_Implicit(pxResult2);
          if (wzTask2 != null && wzTask2.Status != "CP" && wzTask2.Status != "SK")
          {
            flag = false;
            break;
          }
        }
        if (flag && wzTask1.Status != "DS" && !tasksToOpen.ContainsKey(wzTask1.TaskID.Value))
        {
          WZTask copy = (WZTask) ((PXSelectBase) this.TaskInfo).Cache.CreateCopy((object) wzTask1);
          copy.Status = "OP";
          tasksToOpen.Add(copy.TaskID.Value, copy);
        }
      }
    }
    WZScenario wzScenario = PXResultset<WZScenario>.op_Implicit(PXSelectBase<WZScenario, PXSelect<WZScenario, Where<WZScenario.scenarioID, Equal<Required<WZScenario.scenarioID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) selectedTask.ScenarioID
    }));
    if (wzScenario != null && wzScenario.Status != "CP" && (selectedTask.Status == "CP" || selectedTask.Status == "SK"))
    {
      foreach (PXResult<WZTask, WZTaskSuccessorRelation> pxResult3 in PXSelectBase<WZTask, PXSelectJoin<WZTask, LeftJoin<WZTaskSuccessorRelation, On<WZTaskSuccessorRelation.taskID, Equal<WZTask.taskID>>>, Where<WZTask.scenarioID, Equal<Required<WZTask.scenarioID>>, And<WZTaskSuccessorRelation.predecessorID, Equal<Required<WZTask.taskID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) selectedTask.ScenarioID,
        (object) selectedTask.TaskID
      }))
      {
        WZTask wzTask3 = PXResult<WZTask, WZTaskSuccessorRelation>.op_Implicit(pxResult3);
        WZTaskSuccessorRelation successorRelation = PXResult<WZTask, WZTaskSuccessorRelation>.op_Implicit(pxResult3);
        if (successorRelation != null && successorRelation.TaskID.HasValue)
        {
          bool flag = false;
          foreach (PXResult<WZTask, WZTaskPredecessorRelation> pxResult4 in PXSelectBase<WZTask, PXSelectJoin<WZTask, LeftJoin<WZTaskPredecessorRelation, On<WZTaskPredecessorRelation.predecessorID, Equal<WZTask.taskID>>>, Where<WZTask.scenarioID, Equal<Required<WZTask.scenarioID>>, And<WZTaskPredecessorRelation.taskID, Equal<Required<WZTask.taskID>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) selectedTask.ScenarioID,
            (object) wzTask3.TaskID
          }))
          {
            WZTask wzTask4 = PXResult<WZTask, WZTaskPredecessorRelation>.op_Implicit(pxResult4);
            if (wzTask4 != null)
            {
              if (wzTask4.Status == "CP" || wzTask4.Status == "SK" || wzTask4.Status == "DS")
              {
                flag = true;
              }
              else
              {
                flag = false;
                break;
              }
            }
          }
          if (flag && wzTask3.Status == "PN")
          {
            WZTask copy = (WZTask) ((PXSelectBase) this.TaskInfo).Cache.CreateCopy((object) wzTask3);
            copy.Status = "OP";
            if (!tasksToOpen.ContainsKey(copy.TaskID.Value))
              tasksToOpen.Add(copy.TaskID.Value, copy);
          }
        }
      }
    }
    return tasksToOpen;
  }

  private Dictionary<Guid, WZTask> GetTasksToDisable(WZTask selectedTask)
  {
    Dictionary<Guid, WZTask> tasksToDisable = new Dictionary<Guid, WZTask>();
    if (selectedTask.Status == "DS")
    {
      foreach (PXResult<WZTask> pxResult in PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.scenarioID, Equal<Required<WZTask.scenarioID>>, And<WZTask.parentTaskID, Equal<Required<WZTask.taskID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) selectedTask.ScenarioID,
        (object) selectedTask.TaskID
      }))
      {
        WZTask wzTask1 = PXResult<WZTask>.op_Implicit(pxResult);
        if (wzTask1 != null)
        {
          Guid? taskId = wzTask1.TaskID;
          if (taskId.HasValue)
          {
            WZTask copy = (WZTask) ((PXSelectBase) this.TaskInfo).Cache.CreateCopy((object) wzTask1);
            copy.Status = "DS";
            Dictionary<Guid, WZTask> dictionary1 = tasksToDisable;
            taskId = copy.TaskID;
            Guid key1 = taskId.Value;
            if (!dictionary1.ContainsKey(key1))
            {
              Dictionary<Guid, WZTask> dictionary2 = tasksToDisable;
              taskId = copy.TaskID;
              Guid key2 = taskId.Value;
              WZTask wzTask2 = copy;
              dictionary2.Add(key2, wzTask2);
            }
          }
        }
      }
    }
    return tasksToDisable;
  }

  private Dictionary<Guid, WZTask> GetTasksToEnable(WZTask selectedTask)
  {
    Dictionary<Guid, WZTask> tasksToEnable = new Dictionary<Guid, WZTask>();
    if (selectedTask.Status == "PN")
    {
      foreach (PXResult<WZTask> pxResult1 in PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.scenarioID, Equal<Required<WZTask.scenarioID>>, And<WZTask.parentTaskID, Equal<Required<WZTask.taskID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) selectedTask.ScenarioID,
        (object) selectedTask.TaskID
      }))
      {
        WZTask wzTask = PXResult<WZTask>.op_Implicit(pxResult1);
        if (wzTask != null && wzTask.TaskID.HasValue && wzTask.Status == "DS")
        {
          PXResultset<WZTaskFeature> pxResultset = PXSelectBase<WZTaskFeature, PXSelectReadonly<WZTaskFeature, Where<WZTaskFeature.scenarioID, Equal<Required<WZTask.scenarioID>>, And<WZTaskFeature.taskID, Equal<Required<WZTask.taskID>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) wzTask.ScenarioID,
            (object) wzTask.TaskID
          });
          bool flag;
          if (pxResultset.Count == 0)
          {
            flag = true;
          }
          else
          {
            flag = true;
            foreach (PXResult<WZTaskFeature> pxResult2 in pxResultset)
            {
              if (!PXAccess.FeatureInstalled($"{typeof (FeaturesSet).FullName}+{PXResult<WZTaskFeature>.op_Implicit(pxResult2).Feature}"))
              {
                flag = false;
                break;
              }
            }
          }
          if (flag)
          {
            WZTask copy = (WZTask) ((PXSelectBase) this.TaskInfo).Cache.CreateCopy((object) wzTask);
            copy.Status = "PN";
            if (!tasksToEnable.ContainsKey(copy.TaskID.Value))
              tasksToEnable.Add(copy.TaskID.Value, copy);
          }
        }
      }
    }
    return tasksToEnable;
  }

  private Dictionary<Guid, WZTask> GetTasksToSkip(WZTask selectedTask)
  {
    Dictionary<Guid, WZTask> tasksToSkip = new Dictionary<Guid, WZTask>();
    if (selectedTask.Status == "SK")
    {
      foreach (PXResult<WZTask> pxResult in PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.scenarioID, Equal<Required<WZTask.scenarioID>>, And<WZTask.parentTaskID, Equal<Required<WZTask.taskID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) selectedTask.ScenarioID,
        (object) selectedTask.TaskID
      }))
      {
        WZTask wzTask1 = PXResult<WZTask>.op_Implicit(pxResult);
        if (wzTask1.Status != "DS")
        {
          Dictionary<Guid, WZTask> dictionary1 = tasksToSkip;
          Guid? taskId = wzTask1.TaskID;
          Guid key1 = taskId.Value;
          if (!dictionary1.ContainsKey(key1))
          {
            WZTask copy = (WZTask) ((PXSelectBase) this.TaskInfo).Cache.CreateCopy((object) wzTask1);
            if (!copy.IsOptional.Value)
            {
              copy.Status = "SK";
              Dictionary<Guid, WZTask> dictionary2 = tasksToSkip;
              taskId = copy.TaskID;
              Guid key2 = taskId.Value;
              WZTask wzTask2 = copy;
              dictionary2.Add(key2, wzTask2);
            }
            else if (copy.Status != "CP")
            {
              copy.Status = "SK";
              Dictionary<Guid, WZTask> dictionary3 = tasksToSkip;
              taskId = copy.TaskID;
              Guid key3 = taskId.Value;
              WZTask wzTask3 = copy;
              dictionary3.Add(key3, wzTask3);
            }
          }
        }
      }
    }
    return tasksToSkip;
  }

  protected virtual void WZTask_Type_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is WZTask row) || !(row.Type == "AR"))
      return;
    row.ScreenID = (string) null;
    row.ImportScenarioID = new Guid?();
  }

  protected virtual void WZTaskFeature_DisplayName_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is WZTaskFeature row) || row.Feature == null)
      return;
    row.DisplayName = this.FeaturesNames[row.Feature];
  }

  protected virtual void WZTaskFeature_Required_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is WZTaskFeature row) || row.Feature == null)
      return;
    List<string> featureDependency = this.GetFeatureDependency(row.Feature);
    List<string> featuresDependsOn = this.GetFeaturesDependsOn(row.Feature);
    if (row.Required.Value)
    {
      WZTask wzTask = PXResultset<WZTask>.op_Implicit(PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.scenarioID, Equal<Required<WZTask.scenarioID>>, And<WZTask.taskID, Equal<Required<WZTask.taskID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.ScenarioID,
        (object) ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.TaskID
      }));
      if (!PXAccess.FeatureInstalled($"{typeof (FeaturesSet).FullName}+{row.Feature}") && wzTask != null)
      {
        wzTask.Status = "DS";
        ((PXSelectBase<WZTask>) this.TaskInfo).Update(wzTask);
      }
      foreach (string str in featureDependency)
      {
        WZTaskFeature wzTaskFeature = PXResultset<WZTaskFeature>.op_Implicit(PXSelectBase<WZTaskFeature, PXSelect<WZTaskFeature, Where<WZTaskFeature.scenarioID, Equal<Required<WZTask.scenarioID>>, And<WZTaskFeature.taskID, Equal<Required<WZTask.taskID>>, And<WZTaskFeature.feature, Equal<Required<WZTaskFeature.feature>>>>>>.Config>.Select((PXGraph) this, new object[3]
        {
          (object) ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.ScenarioID,
          (object) ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.TaskID,
          (object) str
        }));
        if (wzTaskFeature != null)
        {
          wzTaskFeature.Required = new bool?(true);
          if (!PXAccess.FeatureInstalled($"{typeof (FeaturesSet).FullName}+{str}") && wzTask != null)
          {
            wzTask.Status = "DS";
            ((PXSelectBase<WZTask>) this.TaskInfo).Update(wzTask);
          }
          ((PXSelectBase<WZTaskFeature>) this.Features).Update(wzTaskFeature);
        }
      }
    }
    else
    {
      WZTask wzTask = PXResultset<WZTask>.op_Implicit(PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.scenarioID, Equal<Required<WZTask.scenarioID>>, And<WZTask.taskID, Equal<Required<WZTask.taskID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.ScenarioID,
        (object) ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.TaskID
      }));
      if (!PXAccess.FeatureInstalled($"{typeof (FeaturesSet).FullName}+{row.Feature}") && wzTask != null)
      {
        wzTask.Status = "PN";
        ((PXSelectBase<WZTask>) this.TaskInfo).Update(wzTask);
      }
      foreach (string str in featuresDependsOn)
      {
        WZTaskFeature wzTaskFeature = PXResultset<WZTaskFeature>.op_Implicit(PXSelectBase<WZTaskFeature, PXSelect<WZTaskFeature, Where<WZTaskFeature.scenarioID, Equal<Required<WZTask.scenarioID>>, And<WZTaskFeature.taskID, Equal<Required<WZTask.taskID>>, And<WZTaskFeature.feature, Equal<Required<WZTaskFeature.feature>>>>>>.Config>.Select((PXGraph) this, new object[3]
        {
          (object) ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.ScenarioID,
          (object) ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.TaskID,
          (object) str
        }));
        if (wzTaskFeature != null)
        {
          wzTaskFeature.Required = new bool?(false);
          if (!PXAccess.FeatureInstalled(str) && wzTask != null && wzTask.Status == "DS")
          {
            wzTask.Status = "PN";
            ((PXSelectBase<WZTask>) this.TaskInfo).Update(wzTask);
          }
          ((PXSelectBase<WZTaskFeature>) this.Features).Update(wzTaskFeature);
        }
      }
    }
    ((PXSelectBase) this.Features).View.RequestRefresh();
  }

  protected virtual void WZTaskFeature_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is WZTaskFeature row) || row.Required.GetValueOrDefault())
      return;
    ((PXSelectBase<WZTaskFeature>) this.Features).Delete(row);
    ((PXSelectBase) this.Features).View.RequestRefresh();
  }

  protected virtual void WZTaskSuccessorRelation_TaskID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is WZTaskSuccessorRelation row))
      return;
    row.PredecessorID = ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.TaskID;
  }

  protected virtual void WZTaskSuccessorRelation_RowInserting(
    PXCache sender,
    PXRowInsertingEventArgs e)
  {
    if (e.Row is WZTaskSuccessorRelation row)
    {
      Guid? nullable = row.TaskID;
      if (nullable.HasValue)
      {
        nullable = row.PredecessorID;
        if (nullable.HasValue)
          return;
      }
    }
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void WZTaskSuccessorRelation_TaskID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is WZTaskSuccessorRelation) || e.NewValue == null)
      return;
    if (PXResultset<WZTaskRelation>.op_Implicit(PXSelectBase<WZTaskRelation, PXSelect<WZTaskRelation, Where<WZTaskRelation.scenarioID, Equal<Current<WZTask.scenarioID>>, And<WZTaskRelation.predecessorID, Equal<Current<WZTask.taskID>>, And<WZTaskRelation.taskID, Equal<Required<WZTaskRelation.taskID>>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    })) != null)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXSetPropertyException("This task already in successor list", (PXErrorLevel) 4);
    }
    foreach (WZTask requiredTask in this.GetRequiredTasks(((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.ScenarioID, ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.TaskID))
    {
      Guid? taskId = requiredTask.TaskID;
      Guid newValue = (Guid) e.NewValue;
      if ((taskId.HasValue ? (taskId.GetValueOrDefault() == newValue ? 1 : 0) : 0) != 0)
      {
        ((CancelEventArgs) e).Cancel = true;
        throw new PXSetPropertyException("This task cannot be successor for current task because of cycle reference", (PXErrorLevel) 4);
      }
    }
  }

  protected virtual void WZTaskPredecessorRelation_PredecessorID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is WZTaskPredecessorRelation row) || e.NewValue == null)
      return;
    Guid? nullable;
    if (PXResultset<WZTaskRelation>.op_Implicit(PXSelectBase<WZTaskRelation, PXSelect<WZTaskRelation, Where<WZTaskRelation.scenarioID, Equal<Current<WZTask.scenarioID>>, And<WZTaskRelation.taskID, Equal<Current<WZTask.taskID>>, And<WZTaskRelation.predecessorID, Equal<Required<WZTaskRelation.predecessorID>>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    })) != null)
    {
      nullable = row.PredecessorID;
      if (nullable.HasValue)
      {
        e.NewValue = (object) row.PredecessorID;
        sender.RaiseExceptionHandling<WZTaskPredecessorRelation.predecessorID>(e.Row, (object) null, (Exception) new PXSetPropertyException("This task already in predecessor list", (PXErrorLevel) 4));
      }
    }
    foreach (WZTask nextTask in this.GetNextTasks(((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.ScenarioID, ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.TaskID))
    {
      nullable = nextTask.TaskID;
      Guid newValue = (Guid) e.NewValue;
      if ((nullable.HasValue ? (nullable.GetValueOrDefault() == newValue ? 1 : 0) : 0) != 0)
      {
        ((CancelEventArgs) e).Cancel = true;
        throw new PXSetPropertyException("This task cannot be predecessor for current task because of cycle reference", (PXErrorLevel) 4);
      }
    }
  }

  protected virtual void WZTaskPredecessorRelation_TaskID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is WZTaskPredecessorRelation row))
      return;
    row.TaskID = ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.TaskID;
  }

  protected virtual void WZTaskPredecessorRelation_RowInserting(
    PXCache sender,
    PXRowInsertingEventArgs e)
  {
    if (e.Row is WZTaskPredecessorRelation row)
    {
      Guid? nullable = row.TaskID;
      if (nullable.HasValue)
      {
        nullable = row.PredecessorID;
        if (nullable.HasValue)
          return;
      }
    }
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void Persist()
  {
    foreach (WZTaskFeature wzTaskFeature in ((PXSelectBase) this.Features).Cache.Inserted)
    {
      bool? required = wzTaskFeature.Required;
      if (required.HasValue)
      {
        required = wzTaskFeature.Required;
        bool flag = false;
        if (!(required.GetValueOrDefault() == flag & required.HasValue))
          continue;
      }
      ((PXSelectBase) this.Features).Cache.SetStatus((object) wzTaskFeature, (PXEntryStatus) 4);
    }
    foreach (WZTaskFeature wzTaskFeature in ((PXSelectBase) this.Features).Cache.Updated)
    {
      bool? required = wzTaskFeature.Required;
      if (required.HasValue)
      {
        required = wzTaskFeature.Required;
        bool flag = false;
        if (!(required.GetValueOrDefault() == flag & required.HasValue))
          continue;
      }
      ((PXSelectBase) this.Features).Cache.SetStatus((object) wzTaskFeature, (PXEntryStatus) 3);
    }
    ((PXGraph) this).Persist();
    PXSiteMap.Provider.Clear();
  }

  public virtual IEnumerable PrepareTasksForActivation(PXAdapter adapter)
  {
    WZScenario current = ((PXSelectBase<WZScenario>) this.Scenario).Current;
    if (current != null && current.Status == "CP")
    {
      foreach (PXResult<WZTask> pxResult in PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.scenarioID, Equal<Current<WZScenario.scenarioID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
      {
        WZTask copy = (WZTask) ((PXSelectBase) this.TaskInfo).Cache.CreateCopy((object) PXResult<WZTask>.op_Implicit(pxResult));
        if (copy.Status != "DS")
          copy.Status = "PN";
        copy.StartedDate = new DateTime?();
        copy.CompletedDate = new DateTime?();
        copy.CompletedBy = new int?();
        ((PXSelectBase<WZTask>) this.TaskInfo).Update(copy);
      }
      ((PXGraph) this).Actions.PressSave();
    }
    return adapter.Get();
  }

  public virtual IEnumerable ActivateScenarioWithoutRefresh(PXAdapter adapter)
  {
    WZScenario current = ((PXSelectBase<WZScenario>) this.Scenario).Current;
    if (current != null && current.Status != "AC")
    {
      if (current.Status == "SU")
      {
        current.Status = "AC";
        ((PXSelectBase<WZScenario>) this.Scenario).Update(current);
        ((PXGraph) this).Actions.PressSave();
        return adapter.Get();
      }
      current.Status = "AC";
      if (!current.Scheduled.Value)
        current.ExecutionDate = ((PXGraph) this).Accessinfo.BusinessDate;
      ((PXSelectBase<WZScenario>) this.Scenario).Update(current);
      foreach (PXResult<WZTask, WZTaskRelation> pxResult1 in PXSelectBase<WZTask, PXSelectJoin<WZTask, LeftJoin<WZTaskRelation, On<WZTaskRelation.taskID, Equal<WZTask.taskID>>>, Where<WZTask.scenarioID, Equal<Required<WZScenario.scenarioID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) current.ScenarioID
      }))
      {
        WZTask wzTask1 = PXResult<WZTask, WZTaskRelation>.op_Implicit(pxResult1);
        bool flag1 = true;
        foreach (PXResult<WZTaskFeature> pxResult2 in PXSelectBase<WZTaskFeature, PXSelectReadonly<WZTaskFeature, Where<WZTaskFeature.taskID, Equal<Required<WZTask.taskID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) wzTask1.TaskID
        }))
        {
          if (!PXAccess.FeatureInstalled($"{typeof (FeaturesSet).FullName}+{PXResult<WZTaskFeature>.op_Implicit(pxResult2).Feature}"))
          {
            flag1 = false;
            break;
          }
        }
        WZTask copy = (WZTask) ((PXSelectBase) this.TaskInfo).Cache.CreateCopy((object) wzTask1);
        if (flag1)
        {
          bool flag2 = false;
          Guid? nullable = wzTask1.ParentTaskID;
          Guid empty1 = Guid.Empty;
          if ((nullable.HasValue ? (nullable.GetValueOrDefault() != empty1 ? 1 : 0) : 1) != 0)
          {
            foreach (PXResult<WZTask> pxResult3 in PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.scenarioID, Equal<Required<WZScenario.scenarioID>>, And<WZTask.taskID, Equal<Required<WZTask.parentTaskID>>>>>.Config>.Select((PXGraph) this, new object[2]
            {
              (object) current.ScenarioID,
              (object) wzTask1.ParentTaskID
            }))
            {
              WZTask wzTask2 = PXResult<WZTask>.op_Implicit(pxResult3);
              if (wzTask2.Status != "AC" && wzTask2.Status != "OP")
              {
                flag2 = true;
                break;
              }
              foreach (PXResult<WZTask> pxResult4 in PXSelectBase<WZTask, PXSelectJoin<WZTask, LeftJoin<WZTaskRelation, On<WZTaskRelation.predecessorID, Equal<WZTask.taskID>>>, Where<WZTask.scenarioID, Equal<Required<WZTask.scenarioID>>, And<WZTaskRelation.taskID, Equal<Required<WZTask.taskID>>>>>.Config>.Select((PXGraph) this, new object[2]
              {
                (object) wzTask2.ScenarioID,
                (object) wzTask2.TaskID
              }))
              {
                if (PXResult<WZTask>.op_Implicit(pxResult4).Status != "CP")
                {
                  flag2 = true;
                  break;
                }
              }
            }
          }
          WZTaskPredecessorRelation predecessorRelation = (WZTaskPredecessorRelation) PXResult<WZTask, WZTaskRelation>.op_Implicit(pxResult1);
          if (predecessorRelation != null)
          {
            nullable = predecessorRelation.TaskID;
            if (!nullable.HasValue)
            {
              if (!flag2)
                copy.Status = "OP";
            }
            else
            {
              bool flag3 = false;
              foreach (PXResult<WZTask, WZTaskRelation> pxResult5 in PXSelectBase<WZTask, PXSelectJoin<WZTask, LeftJoin<WZTaskRelation, On<WZTaskRelation.predecessorID, Equal<WZTask.taskID>>>, Where<WZTask.scenarioID, Equal<Required<WZTask.scenarioID>>, And<WZTaskRelation.taskID, Equal<Required<WZTask.taskID>>>>>.Config>.Select((PXGraph) this, new object[2]
              {
                (object) wzTask1.ScenarioID,
                (object) wzTask1.TaskID
              }))
              {
                WZTaskRelation wzTaskRelation = PXResult<WZTask, WZTaskRelation>.op_Implicit(pxResult5);
                if (wzTaskRelation != null)
                {
                  nullable = wzTaskRelation.PredecessorID;
                  if (nullable.HasValue)
                  {
                    WZTask wzTask3 = PXResultset<WZTask>.op_Implicit(PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.taskID, Equal<Required<WZTask.taskID>>>>.Config>.Select((PXGraph) this, new object[1]
                    {
                      (object) wzTaskRelation.PredecessorID
                    }));
                    if (wzTask3 != null && wzTask3.Status != "CP" && wzTask3.Status != "SK" && wzTask3.Status != "DS")
                    {
                      flag3 = true;
                      break;
                    }
                  }
                }
              }
              if (!flag3)
                copy.Status = "OP";
            }
          }
          if (predecessorRelation == null)
          {
            nullable = wzTask1.ParentTaskID;
            Guid empty2 = Guid.Empty;
            if ((nullable.HasValue ? (nullable.GetValueOrDefault() == empty2 ? 1 : 0) : 0) != 0)
              copy.Status = "OP";
          }
          if (wzTask1.Status == "DS")
          {
            if (PXResultset<WZTask>.op_Implicit(PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.scenarioID, Equal<Required<WZScenario.scenarioID>>, And<WZTask.taskID, Equal<Required<WZTask.parentTaskID>>>>>.Config>.Select((PXGraph) this, new object[2]
            {
              (object) current.ScenarioID,
              (object) wzTask1.ParentTaskID
            })).Status != "DS")
              copy.Status = "PN";
          }
        }
        else
          copy.Status = "DS";
        ((PXSelectBase<WZTask>) this.TaskInfo).Update(copy);
      }
      ((PXGraph) this).Actions.PressSave();
    }
    return adapter.Get();
  }

  public virtual IEnumerable CompleteScenarioWithoutRefresh(PXAdapter adapter)
  {
    WZScenario current = ((PXSelectBase<WZScenario>) this.Scenario).Current;
    if (current != null && current.Status == "AC")
    {
      current.Status = "CP";
      ((PXSelectBase<WZScenario>) this.Scenario).Update(current);
      foreach (PXResult<WZTask> pxResult in PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.scenarioID, Equal<Required<WZScenario.scenarioID>>, And<Where<WZTask.status, Equal<WizardTaskStatusesAttribute.Active>, Or<WZTask.status, Equal<WizardTaskStatusesAttribute.Open>>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) current.ScenarioID
      }))
      {
        WZTask copy = (WZTask) ((PXSelectBase) this.TaskInfo).Cache.CreateCopy((object) PXResult<WZTask>.op_Implicit(pxResult));
        copy.Status = "SK";
        ((PXSelectBase<WZTask>) this.TaskInfo).Update(copy);
      }
      ((PXGraph) this).Actions.PressSave();
      PXSiteMap.Provider.Clear();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable SuspendScenario(PXAdapter adapter)
  {
    WZScenario current = ((PXSelectBase<WZScenario>) this.Scenario).Current;
    if (current != null && current.Status != "SU")
    {
      current.Status = "SU";
      ((PXSelectBase<WZScenario>) this.Scenario).Update(current);
      ((PXGraph) this).Actions.PressSave();
      PXSiteMap.Provider.Clear();
      throw new PXRefreshException();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "Remove")]
  public virtual IEnumerable DeleteTask(PXAdapter adapter)
  {
    if (((PXSelectBase<WZScenario>) this.Scenario).Current != null && (((PXSelectBase<WZScenario>) this.Scenario).Current.Status == "AC" || ((PXSelectBase<WZScenario>) this.Scenario).Current.Status == "SU"))
      throw new PXException("A scenario in the '{0}' status cannot be edited", new object[1]
      {
        (object) new WizardScenarioStatusesAttribute().ValueLabelDic[((PXSelectBase<WZScenario>) this.Scenario).Current.Status]
      });
    if (((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current != null)
    {
      Guid? taskId = ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.TaskID;
      Guid empty = Guid.Empty;
      if ((taskId.HasValue ? (taskId.GetValueOrDefault() != empty ? 1 : 0) : 1) != 0 && ((PXSelectBase<WZTask>) this.TaskInfo).Ask("Delete task", "All child tasks will be deleted. Are you sure you want to delete this task?", (MessageButtons) 4) == 6)
      {
        WZTask wzTask1 = PXResultset<WZTask>.op_Implicit(PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.taskID, Equal<Required<WZTask.taskID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) ((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.TaskID
        }));
        this.deleteTasks(((PXSelectBase<WZTaskTreeItem>) this.TasksTreeItems).Current.TaskID);
        int? position = wzTask1.Position;
        int num = position.Value;
        if (num > 0)
        {
          foreach (PXResult<WZTask> pxResult in PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.parentTaskID, Equal<Required<WZTask.parentTaskID>>, And<WZTask.position, Greater<Required<WZTask.position>>, And<WZTask.scenarioID, Equal<Required<WZTask.scenarioID>>>>>>.Config>.Select((PXGraph) this, new object[3]
          {
            (object) wzTask1.ParentTaskID,
            (object) num,
            (object) wzTask1.ScenarioID
          }))
          {
            WZTask copy = (WZTask) ((PXSelectBase) this.TaskInfo).Cache.CreateCopy((object) PXResult<WZTask>.op_Implicit(pxResult));
            WZTask wzTask2 = copy;
            position = wzTask2.Position;
            wzTask2.Position = position.HasValue ? new int?(position.GetValueOrDefault() - 1) : new int?();
            ((PXSelectBase<WZTask>) this.TaskInfo).Update(copy);
          }
        }
        ((PXSelectBase) this.TaskInfo).Cache.Delete((object) wzTask1);
      }
    }
    ((PXSelectBase) this.TaskInfo).View.RequestRefresh();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "AddNew")]
  public virtual IEnumerable AddTask(PXAdapter adapter)
  {
    WZScenario current = ((PXSelectBase<WZScenario>) this.Scenario).Current;
    if (current != null)
    {
      if (current.Status == "AC" || current.Status == "SU")
        throw new PXException("A scenario in the '{0}' status cannot be edited", new object[1]
        {
          (object) new WizardScenarioStatusesAttribute().ValueLabelDic[current.Status]
        });
      if (((PXSelectBase<WZTask>) this.TaskInfo).Current != null)
      {
        WZTask wzTask = PXResultset<WZTask>.op_Implicit(PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.scenarioID, Equal<Current<WZScenario.scenarioID>>, And<WZTask.taskID, Equal<Required<WZTask.parentTaskID>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) ((PXSelectBase<WZTask>) this.TaskInfo).Current.ParentTaskID
        }));
        if (wzTask != null)
        {
          Guid? parentTaskId = wzTask.ParentTaskID;
          Guid empty = Guid.Empty;
          if ((parentTaskId.HasValue ? (parentTaskId.GetValueOrDefault() == empty ? 1 : 0) : 0) != 0 && ((PXSelectBase<WZTask>) this.TaskInfo).Current.Type == "SC")
            throw new PXException("Task of Screen type cannot have childs");
        }
      }
      ((PXSelectBase<WZTask>) this.TaskInfo).Insert((WZTask) ((PXSelectBase) this.TaskInfo).Cache.CreateInstance());
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "ArrowUp")]
  public virtual IEnumerable Up(PXAdapter adapter)
  {
    if (((PXSelectBase<WZScenario>) this.Scenario).Current != null && (((PXSelectBase<WZScenario>) this.Scenario).Current.Status == "AC" || ((PXSelectBase<WZScenario>) this.Scenario).Current.Status == "SU"))
      throw new PXException("A scenario in the '{0}' status cannot be edited", new object[1]
      {
        (object) new WizardScenarioStatusesAttribute().ValueLabelDic[((PXSelectBase<WZScenario>) this.Scenario).Current.Status]
      });
    if (((PXSelectBase<WZTask>) this.TaskInfo).Current != null)
    {
      WZTask wzTask = PXResultset<WZTask>.op_Implicit(PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.parentTaskID, Equal<Required<WZTask.parentTaskID>>, And<WZTask.position, Less<Required<WZTask.position>>, And<WZTask.scenarioID, Equal<Required<WZTask.scenarioID>>>>>, OrderBy<Desc<WZTask.position>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
      {
        (object) ((PXSelectBase<WZTask>) this.TaskInfo).Current.ParentTaskID,
        (object) ((PXSelectBase<WZTask>) this.TaskInfo).Current.Position,
        (object) ((PXSelectBase<WZTask>) this.TaskInfo).Current.ScenarioID
      }));
      if (wzTask != null)
      {
        int num = ((PXSelectBase<WZTask>) this.TaskInfo).Current.Position.Value;
        ((PXSelectBase<WZTask>) this.TaskInfo).Current.Position = wzTask.Position;
        ((PXSelectBase<WZTask>) this.TaskInfo).Update(((PXSelectBase<WZTask>) this.TaskInfo).Current);
        wzTask.Position = new int?(num);
        ((PXSelectBase<WZTask>) this.TaskInfo).Update(wzTask);
      }
    }
    ((PXSelectBase) this.TaskInfo).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.TasksTreeItems).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.TasksTreeItems).View.RequestRefresh();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "ArrowDown")]
  public virtual IEnumerable Down(PXAdapter adapter)
  {
    if (((PXSelectBase<WZScenario>) this.Scenario).Current != null && (((PXSelectBase<WZScenario>) this.Scenario).Current.Status == "AC" || ((PXSelectBase<WZScenario>) this.Scenario).Current.Status == "SU"))
      throw new PXException("A scenario in the '{0}' status cannot be edited", new object[1]
      {
        (object) new WizardScenarioStatusesAttribute().ValueLabelDic[((PXSelectBase<WZScenario>) this.Scenario).Current.Status]
      });
    if (((PXSelectBase<WZTask>) this.TaskInfo).Current != null)
    {
      WZTask wzTask = PXResultset<WZTask>.op_Implicit(PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.parentTaskID, Equal<Required<WZTask.parentTaskID>>, And<WZTask.position, Greater<Required<WZTask.position>>, And<WZTask.scenarioID, Equal<Required<WZTask.scenarioID>>>>>, OrderBy<Asc<WZTask.position>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
      {
        (object) ((PXSelectBase<WZTask>) this.TaskInfo).Current.ParentTaskID,
        (object) ((PXSelectBase<WZTask>) this.TaskInfo).Current.Position,
        (object) ((PXSelectBase<WZTask>) this.TaskInfo).Current.ScenarioID
      }));
      if (wzTask != null)
      {
        int num = ((PXSelectBase<WZTask>) this.TaskInfo).Current.Position.Value;
        ((PXSelectBase<WZTask>) this.TaskInfo).Current.Position = wzTask.Position;
        ((PXSelectBase<WZTask>) this.TaskInfo).Update(((PXSelectBase<WZTask>) this.TaskInfo).Current);
        wzTask.Position = new int?(num);
        ((PXSelectBase<WZTask>) this.TaskInfo).Update(wzTask);
      }
    }
    ((PXSelectBase) this.TaskInfo).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.TasksTreeItems).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.TasksTreeItems).View.RequestRefresh();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "ArrowLeft")]
  public virtual IEnumerable Left(PXAdapter adapter)
  {
    if (((PXSelectBase<WZScenario>) this.Scenario).Current != null && (((PXSelectBase<WZScenario>) this.Scenario).Current.Status == "AC" || ((PXSelectBase<WZScenario>) this.Scenario).Current.Status == "SU"))
      throw new PXException("A scenario in the '{0}' status cannot be edited", new object[1]
      {
        (object) new WizardScenarioStatusesAttribute().ValueLabelDic[((PXSelectBase<WZScenario>) this.Scenario).Current.Status]
      });
    if (((PXSelectBase<WZTask>) this.TaskInfo).Current != null)
    {
      Guid? parentTaskId = ((PXSelectBase<WZTask>) this.TaskInfo).Current.ParentTaskID;
      Guid empty1 = Guid.Empty;
      if ((parentTaskId.HasValue ? (parentTaskId.GetValueOrDefault() != empty1 ? 1 : 0) : 1) != 0)
      {
        WZTask wzTask1 = PXResultset<WZTask>.op_Implicit(PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.taskID, Equal<Required<WZTask.parentTaskID>>>, OrderBy<Asc<WZTask.position>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
        {
          (object) ((PXSelectBase<WZTask>) this.TaskInfo).Current.ParentTaskID
        }));
        if (wzTask1 != null)
        {
          parentTaskId = wzTask1.ParentTaskID;
          Guid empty2 = Guid.Empty;
          if ((parentTaskId.HasValue ? (parentTaskId.GetValueOrDefault() == empty2 ? 1 : 0) : 0) != 0 && ((PXSelectBase<WZTask>) this.TaskInfo).Current.Type == "SC")
            return adapter.Get();
          parentTaskId = wzTask1.ParentTaskID;
          Guid empty3 = Guid.Empty;
          WZTask wzTask2;
          if ((parentTaskId.HasValue ? (parentTaskId.GetValueOrDefault() == empty3 ? 1 : 0) : 0) != 0)
            wzTask2 = PXResultset<WZTask>.op_Implicit(PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.parentTaskID, Equal<Required<WZTask.taskID>>>, OrderBy<Desc<WZTask.position>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
            {
              (object) Guid.Empty
            }));
          else
            wzTask2 = PXResultset<WZTask>.op_Implicit(PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.parentTaskID, Equal<Required<WZTask.parentTaskID>>>, OrderBy<Desc<WZTask.position>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
            {
              (object) wzTask1.ParentTaskID
            }));
          if (wzTask2 != null)
          {
            ((PXSelectBase<WZTask>) this.TaskInfo).Current.ParentTaskID = wzTask2.ParentTaskID;
            WZTask current = ((PXSelectBase<WZTask>) this.TaskInfo).Current;
            int? position = wzTask2.Position;
            int? nullable = position.HasValue ? new int?(position.GetValueOrDefault() + 1) : new int?();
            current.Position = nullable;
            ((PXSelectBase<WZTask>) this.TaskInfo).Update(((PXSelectBase<WZTask>) this.TaskInfo).Current);
          }
        }
      }
    }
    ((PXSelectBase) this.TaskInfo).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.TasksTreeItems).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.TasksTreeItems).View.RequestRefresh();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "ArrowRight")]
  public virtual IEnumerable Right(PXAdapter adapter)
  {
    if (((PXSelectBase<WZScenario>) this.Scenario).Current != null && (((PXSelectBase<WZScenario>) this.Scenario).Current.Status == "AC" || ((PXSelectBase<WZScenario>) this.Scenario).Current.Status == "SU"))
      throw new PXException("A scenario in the '{0}' status cannot be edited", new object[1]
      {
        (object) new WizardScenarioStatusesAttribute().ValueLabelDic[((PXSelectBase<WZScenario>) this.Scenario).Current.Status]
      });
    if (((PXSelectBase<WZTask>) this.TaskInfo).Current != null)
    {
      WZTask wzTask1 = PXResultset<WZTask>.op_Implicit(PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.parentTaskID, Equal<Required<WZTask.parentTaskID>>, And<WZTask.scenarioID, Equal<Required<WZTask.scenarioID>>, And<WZTask.position, Less<Required<WZTask.position>>>>>, OrderBy<Desc<WZTask.position>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
      {
        (object) ((PXSelectBase<WZTask>) this.TaskInfo).Current.ParentTaskID,
        (object) ((PXSelectBase<WZTask>) this.TaskInfo).Current.ScenarioID,
        (object) ((PXSelectBase<WZTask>) this.TaskInfo).Current.Position
      }));
      if (wzTask1 != null)
      {
        if (wzTask1.Type == "SC")
          return adapter.Get();
        WZTask wzTask2 = PXResultset<WZTask>.op_Implicit(PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.parentTaskID, Equal<Required<WZTask.parentTaskID>>, And<WZTask.scenarioID, Equal<Required<WZTask.scenarioID>>>>, OrderBy<Desc<WZTask.position>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
        {
          (object) wzTask1.TaskID,
          (object) wzTask1.ScenarioID
        }));
        if (wzTask2 != null)
        {
          ((PXSelectBase<WZTask>) this.TaskInfo).Current.ParentTaskID = wzTask2.ParentTaskID;
          WZTask current = ((PXSelectBase<WZTask>) this.TaskInfo).Current;
          int? position = wzTask2.Position;
          int? nullable = position.HasValue ? new int?(position.GetValueOrDefault() + 1) : new int?();
          current.Position = nullable;
          ((PXSelectBase<WZTask>) this.TaskInfo).Update(((PXSelectBase<WZTask>) this.TaskInfo).Current);
        }
        else
        {
          ((PXSelectBase<WZTask>) this.TaskInfo).Current.ParentTaskID = wzTask1.TaskID;
          ((PXSelectBase<WZTask>) this.TaskInfo).Current.Position = new int?(1);
          ((PXSelectBase<WZTask>) this.TaskInfo).Update(((PXSelectBase<WZTask>) this.TaskInfo).Current);
        }
      }
    }
    ((PXSelectBase) this.TaskInfo).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.TasksTreeItems).Cache.ClearQueryCacheObsolete();
    ((PXSelectBase) this.TasksTreeItems).View.RequestRefresh();
    return adapter.Get();
  }

  private void deleteTasks(Guid? taskID)
  {
    if (!taskID.HasValue)
      return;
    Guid? nullable = taskID;
    Guid empty = Guid.Empty;
    if ((nullable.HasValue ? (nullable.GetValueOrDefault() == empty ? 1 : 0) : 0) != 0)
      return;
    foreach (PXResult<WZTask> pxResult in PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.parentTaskID, Equal<Required<WZTask.parentTaskID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) taskID
    }))
    {
      WZTask wzTask = PXResult<WZTask>.op_Implicit(pxResult);
      nullable = wzTask.TaskID;
      Guid? parentTaskId = wzTask.ParentTaskID;
      if ((nullable.HasValue == parentTaskId.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != parentTaskId.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        this.deleteTasks(wzTask.TaskID);
      ((PXSelectBase) this.TasksTreeItems).Cache.Delete((object) wzTask);
    }
  }

  private List<string> GetFeatureDependency(string featureID)
  {
    List<string> featureDependency = new List<string>();
    if (this.FeatureDependsOn.ContainsKey(featureID))
      featureDependency.Add(this.FeatureDependsOn[featureID]);
    return featureDependency;
  }

  private List<string> GetFeaturesDependsOn(string featureID)
  {
    List<string> featuresDependsOn = new List<string>();
    foreach (KeyValuePair<string, string> keyValuePair in this.FeatureDependsOn)
    {
      if (keyValuePair.Value.Equals(featureID))
        featuresDependsOn.Add(keyValuePair.Key);
    }
    return featuresDependsOn;
  }

  private List<WZTask> GetRequiredTasks(Guid? scenarioID, Guid? taskID)
  {
    List<WZTask> requiredTasks = new List<WZTask>();
    foreach (PXResult<WZTaskRelation, WZTask> pxResult in PXSelectBase<WZTaskRelation, PXSelectJoin<WZTaskRelation, InnerJoin<WZTask, On<WZTaskRelation.predecessorID, Equal<WZTask.taskID>>>, Where<WZTaskRelation.scenarioID, Equal<Required<WZTask.scenarioID>>, And<WZTaskRelation.taskID, Equal<Required<WZTask.taskID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) scenarioID,
      (object) taskID
    }))
    {
      WZTask wzTask = PXResult<WZTaskRelation, WZTask>.op_Implicit(pxResult);
      WZTaskRelation wzTaskRelation = PXResult<WZTaskRelation, WZTask>.op_Implicit(pxResult);
      requiredTasks.Add(wzTask);
      foreach (WZTask requiredTask in this.GetRequiredTasks(wzTask.ScenarioID, wzTaskRelation.PredecessorID))
        requiredTasks.Add(requiredTask);
    }
    return requiredTasks;
  }

  private List<WZTask> GetNextTasks(Guid? scenarioID, Guid? taskID)
  {
    List<WZTask> nextTasks = new List<WZTask>();
    foreach (PXResult<WZTaskRelation, WZTask> pxResult in PXSelectBase<WZTaskRelation, PXSelectJoin<WZTaskRelation, InnerJoin<WZTask, On<WZTaskRelation.taskID, Equal<WZTask.taskID>>>, Where<WZTaskRelation.scenarioID, Equal<Required<WZTask.scenarioID>>, And<WZTaskRelation.predecessorID, Equal<Required<WZTask.taskID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) scenarioID,
      (object) taskID
    }))
    {
      WZTask wzTask = PXResult<WZTaskRelation, WZTask>.op_Implicit(pxResult);
      WZTaskRelation wzTaskRelation = PXResult<WZTaskRelation, WZTask>.op_Implicit(pxResult);
      nextTasks.Add(wzTask);
      foreach (WZTask nextTask in this.GetNextTasks(wzTask.ScenarioID, wzTaskRelation.TaskID))
        nextTasks.Add(nextTask);
    }
    return nextTasks;
  }

  private List<WZTask> GetChildTasks(Guid? taskID)
  {
    List<WZTask> childTasks = new List<WZTask>();
    foreach (PXResult<WZTask> pxResult in PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.scenarioID, Equal<Current<WZScenario.scenarioID>>, And<WZTask.parentTaskID, Equal<Required<WZTask.parentTaskID>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) taskID
    }))
    {
      WZTask wzTask = PXResult<WZTask>.op_Implicit(pxResult);
      childTasks.Add(wzTask);
      foreach (WZTask childTask in this.GetChildTasks(wzTask.TaskID))
        childTasks.Add(childTask);
    }
    return childTasks;
  }

  private int FillFeatureHierarchy(
    int level,
    int order,
    IEnumerable<string> features,
    Dictionary<string, List<string>> subfeatures)
  {
    foreach (string feature in features)
    {
      ++order;
      if (!this.FeatureOrder.ContainsKey(feature))
        this.FeatureOrder.Add(feature, order);
      if (!this.FeatureOffset.ContainsKey(feature))
        this.FeatureOffset.Add(feature, level);
      if (subfeatures.ContainsKey(feature))
        order = this.FillFeatureHierarchy(level + 1, order, (IEnumerable<string>) subfeatures[feature], subfeatures);
    }
    return order;
  }
}
