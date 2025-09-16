// Decompiled with JetBrains decompiler
// Type: PX.Objects.WZ.WZScenarioEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Metadata;
using PX.Objects.GL;
using PX.SM;
using System;
using System.Collections;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.WZ;

[TableAndChartDashboardType]
[Serializable]
public class WZScenarioEntry : PXGraph<WZScenarioEntry, WZScenario>, ICanAlterSiteMap
{
  public PXSelectJoinOrderBy<WZScenario, LeftJoin<PX.SM.SiteMap, On<WZScenario.nodeID, Equal<PX.SM.SiteMap.nodeID>>>, OrderBy<Asc<WZScenario.scenarioOrder>>> Scenarios;
  public PXSelectSiteMapTree<False, False, False, False, False> SiteMap;
  public PXSelect<WZTask> Task;
  public PXSelect<WZTaskRelation> TaskRelation;
  public PXSelect<WZTaskFeature> TaskFeature;
  public PXAction<WZScenario> activateScenario;
  public PXAction<WZScenario> suspendScenario;
  public PXAction<WZScenario> completeScenario;
  public PXAction<WZScenario> viewScenarioDetails;

  [InjectDependency]
  protected IScreenInfoCacheControl ScreenInfoCacheControl { get; set; }

  public bool IsSiteMapAltered { get; internal set; }

  public virtual bool CanClipboardCopyPaste() => false;

  protected virtual IEnumerable scenarios()
  {
    WZScenarioEntry wzScenarioEntry = this;
    foreach (PXResult<WZScenario, PX.SM.SiteMap> pxResult in PXSelectBase<WZScenario, PXSelectJoinOrderBy<WZScenario, LeftJoin<PX.SM.SiteMap, On<WZScenario.nodeID, Equal<PX.SM.SiteMap.nodeID>>>, OrderBy<Asc<WZScenario.scenarioOrder>>>.Config>.Select((PXGraph) wzScenarioEntry, Array.Empty<object>()))
    {
      WZScenario wzScenario1 = PXResult<WZScenario, PX.SM.SiteMap>.op_Implicit(pxResult);
      PX.SM.SiteMap siteMap = PXResult<WZScenario, PX.SM.SiteMap>.op_Implicit(pxResult);
      Guid? nodeId = wzScenario1.NodeID;
      if (nodeId.HasValue)
      {
        nodeId = siteMap.NodeID;
        if (!nodeId.HasValue)
        {
          siteMap.NodeID = wzScenario1.NodeID;
          WZScenario wzScenario2 = PXResultset<WZScenario>.op_Implicit(PXSelectBase<WZScenario, PXSelect<WZScenario, Where<WZScenario.scenarioID, Equal<Required<WZScenario.scenarioID>>>>.Config>.Select((PXGraph) wzScenarioEntry, new object[1]
          {
            (object) wzScenario1.NodeID
          }));
          if (wzScenario2 != null)
            siteMap.Title = wzScenario2.Name;
        }
      }
      yield return (object) new PXResult<WZScenario, PX.SM.SiteMap>(wzScenario1, siteMap);
    }
  }

  protected virtual void WZScenario_ScenarioID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is WZScenario))
      return;
    e.NewValue = (object) Guid.NewGuid();
  }

  protected virtual void WZScenario_NodeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is WZScenario))
      return;
    e.NewValue = (object) Guid.NewGuid();
  }

  protected virtual void WZScenario_NodeID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is WZScenario))
      return;
    PXSiteMap.Provider.Clear();
  }

  protected virtual void WZScenario_NodeID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is WZScenario row) || e.NewValue == null)
      return;
    WZScenario wzScenario = PXResultset<WZScenario>.op_Implicit(PXSelectBase<WZScenario, PXSelect<WZScenario, Where<WZScenario.nodeID, Equal<Required<WZScenario.nodeID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) Guid.Empty
    }));
    if (wzScenario == null)
      return;
    Guid? scenarioId1 = wzScenario.ScenarioID;
    Guid? scenarioId2 = row.ScenarioID;
    if ((scenarioId1.HasValue == scenarioId2.HasValue ? (scenarioId1.HasValue ? (scenarioId1.GetValueOrDefault() != scenarioId2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0 && (Guid) e.NewValue == Guid.Empty)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXSetPropertyException("Only one Scenario can be located at the Sitemap Root.", (PXErrorLevel) 4);
    }
  }

  protected virtual void WZScenario_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase) this.Scenarios).View.RequestRefresh();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ActivateScenario(PXAdapter adapter)
  {
    WZTaskEntry instance = PXGraph.CreateInstance<WZTaskEntry>();
    WZScenario current = ((PXSelectBase<WZScenario>) this.Scenarios).Current;
    if (current != null)
    {
      ((PXSelectBase<WZScenario>) instance.Scenario).Current = current;
      ((PXAction) instance.activateScenario).Press();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable SuspendScenario(PXAdapter adapter)
  {
    WZTaskEntry instance = PXGraph.CreateInstance<WZTaskEntry>();
    WZScenario current = ((PXSelectBase<WZScenario>) this.Scenarios).Current;
    if (current != null)
    {
      ((PXSelectBase<WZScenario>) instance.Scenario).Current = current;
      ((PXAction) instance.suspendScenario).Press();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CompleteScenario(PXAdapter adapter)
  {
    WZTaskEntry instance = PXGraph.CreateInstance<WZTaskEntry>();
    WZScenario current = ((PXSelectBase<WZScenario>) this.Scenarios).Current;
    if (current != null)
    {
      ((PXSelectBase<WZScenario>) instance.Scenario).Current = current;
      ((PXAction) instance.completeScenario).Press();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewScenarioDetails(PXAdapter adapter)
  {
    WZTaskEntry instance = PXGraph.CreateInstance<WZTaskEntry>();
    WZScenario current = ((PXSelectBase<WZScenario>) this.Scenarios).Current;
    if (current != null)
    {
      ((PXSelectBase<WZScenario>) instance.Scenario).Current = current;
      throw new PXRedirectRequiredException((PXGraph) instance, "Scenario Tasks");
    }
    return adapter.Get();
  }
}
