// Decompiled with JetBrains decompiler
// Type: PX.SM.EMailSourceHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Data;
using PX.Data.Automation;
using PX.Data.Description;
using PX.Metadata;
using PX.Web.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.SM;

/// <exclude />
public static class EMailSourceHelper
{
  internal const char FieldSeparator = '.';

  public static IEnumerable<EntityItemSource> TemplateScreens(
    PXGraph graph,
    string parent,
    string cacheType)
  {
    return EMailSourceHelper.TemplateScreensByCondition(graph, parent, cacheType, (EMailSourceHelper.MakeSiteMapCondition) (type => type.IsDefined(typeof (PXEMailSourceAttribute))));
  }

  public static IEnumerable<EntityItemSource> TemplateScreensByCondition(
    PXGraph graph,
    string parent,
    string cacheType,
    EMailSourceHelper.MakeSiteMapCondition condition)
  {
    foreach (PXSiteMapNode node in PXSiteMap.Provider.GetNodes())
    {
      EntityItemSource entityItemSource = EMailSourceHelper.MakeSiteMapBy(graph, node, cacheType, condition);
      if (entityItemSource != null)
        yield return entityItemSource;
    }
  }

  private static EntityItemSource MakeSiteMapBy(
    PXGraph graph,
    PXSiteMapNode node,
    string filterCacheType,
    EMailSourceHelper.MakeSiteMapCondition condition)
  {
    EntityItemSource entityItemSource = (EntityItemSource) null;
    EntityHelper entityHelper = new EntityHelper(graph);
    if (node.GraphType != null)
    {
      string[] dataMembers = PXPageIndexingService.GetDataMembers(node.GraphType);
      PXViewInfo graphView = GraphHelper.GetGraphView(node.GraphType, dataMembers == null || dataMembers.Length == 0 ? (string) null : dataMembers[0]);
      if (graphView != null)
      {
        System.Type type = GraphHelper.GetType(graphView.Cache.Name);
        if (type != (System.Type) null && condition(type))
        {
          if (filterCacheType != null)
          {
            if (graphView.Cache.Name == filterCacheType)
              entityItemSource = new EntityItemSource(node, node.GraphType);
          }
          else
            entityItemSource = new EntityItemSource(node, node.GraphType);
        }
      }
    }
    return entityItemSource;
  }

  public static IEnumerable TemplateEntity(
    PXGraph graph,
    string parent,
    string entityType,
    string graphType)
  {
    return EMailSourceHelper.TemplateEntity(graph, parent, entityType, graphType, true);
  }

  public static IEnumerable TemplateEntity(
    PXGraph graph,
    string parent,
    string entityType,
    string graphType,
    bool onlyVisible)
  {
    return EMailSourceHelper.TemplateEntity(graph, parent, entityType, graphType, onlyVisible, false);
  }

  public static IEnumerable TemplateEntity(
    PXGraph graph,
    string parent,
    string entityType,
    string graphType,
    bool onlyVisible,
    bool addGeneralInfo,
    bool addScreenViews = false,
    IWorkflowService workflowService = null)
  {
    if (entityType != null || graphType != null)
    {
      List<PXViewInfo> graphViews = GraphHelper.GetGraphViews(graphType, true, true);
      PXSiteMapNode siteMapNode = addScreenViews ? PXSiteMap.Provider.FindSiteMapNode(GraphHelper.GetType(graphType)) : (PXSiteMapNode) null;
      PXSiteMap.ScreenInfo screenInfo = siteMapNode != null ? ScreenUtils.ScreenInfo.TryGet(siteMapNode.ScreenID) : (PXSiteMap.ScreenInfo) null;
      List<PXViewInfo> graphSingleViews = addScreenViews ? EMailSourceHelper.GetGraphViews(siteMapNode?.ScreenID, graphType, graphViews) : new List<PXViewInfo>();
      if (graphViews.Count > 0 && entityType == null)
        entityType = graphViews[0].Cache.CacheType.FullName;
      int i;
      if (parent == null)
      {
        i = 0;
        if (graphType != null)
        {
          foreach (PXViewInfo pxViewInfo in graphViews)
            yield return (object) new CacheEntityItem()
            {
              Key = pxViewInfo.Name,
              SubKey = pxViewInfo.Cache.Name,
              Path = (string) null,
              Name = PXMessages.LocalizeNoPrefix(pxViewInfo.DisplayName),
              Number = new int?(i++),
              Icon = Sprite.Tree.GetFullUrl("Folder")
            };
          if (i == 0)
          {
            foreach (PXViewInfo graphView in GraphHelper.GetGraphViews(graphType, false))
            {
              if (graphView.Cache.Name == entityType)
              {
                yield return (object) new CacheEntityItem()
                {
                  Key = graphView.Name,
                  SubKey = graphView.Cache.Name,
                  Path = (string) null,
                  Name = PXMessages.LocalizeNoPrefix(graphView.Cache.DisplayName),
                  Number = new int?(i++),
                  Icon = Sprite.Main.GetFullUrl("Folder")
                };
                break;
              }
            }
          }
          if (addGeneralInfo)
            yield return (object) new CacheEntityItem()
            {
              Key = "GeneralInfo",
              SubKey = typeof (Access.AccessInfoNotification).Name,
              Path = (string) null,
              Name = "General Info",
              Number = new int?(i++),
              Icon = Sprite.Tree.GetFullUrl("Folder")
            };
          if (!addScreenViews)
            yield break;
          foreach (PXViewInfo pxViewInfo in graphSingleViews)
            yield return (object) new CacheEntityItem()
            {
              Key = pxViewInfo.Name,
              SubKey = pxViewInfo.Cache.Name,
              Path = (string) null,
              Name = PXMessages.LocalizeNoPrefix(pxViewInfo.DisplayName),
              Number = new int?(i++),
              Icon = Sprite.Tree.GetFullUrl("Folder")
            };
          if (siteMapNode == null)
            yield break;
          foreach (IGrouping<\u003C\u003Ef__AnonymousType108<string, string>, AUScreenActionBaseState> source in ((IEnumerable<AUScreenActionBaseState>) AUWorkflowActionsEngine.Slot.LocallyCachedSlot.Get(siteMapNode.ScreenID).IndexByScreenActionDefinitions).Where<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (actionDefinition => actionDefinition.Form != null)).GroupBy(act => new
          {
            Form = act.Form,
            DataMember = act.DataMember
          }))
          {
            CacheEntityItem cacheEntityItem = new CacheEntityItem();
            cacheEntityItem.Key = source.Key.Form;
            cacheEntityItem.SubKey = source.Key.DataMember;
            cacheEntityItem.Path = (string) null;
            cacheEntityItem.Name = PXMessages.LocalizeNoPrefix(string.Join(" / ", source.Where<AUScreenActionBaseState>((Func<AUScreenActionBaseState, bool>) (act => act.DisplayName != null)).Select<AUScreenActionBaseState, string>((Func<AUScreenActionBaseState, string>) (act => act.DisplayName))));
            if (string.IsNullOrEmpty(cacheEntityItem.Name))
              cacheEntityItem.Name = PXMessages.LocalizeNoPrefix(string.Join(" / ", source.Select<AUScreenActionBaseState, string>((Func<AUScreenActionBaseState, string>) (act => act.ActionName))));
            cacheEntityItem.Number = new int?(i++);
            cacheEntityItem.Icon = Sprite.Tree.GetFullUrl("Folder");
            yield return (object) cacheEntityItem;
          }
          yield break;
        }
      }
      PXCacheInfo pxCacheInfo = new PXCacheInfo(GraphHelper.GetType(entityType));
      int length = parent != null ? parent.IndexOf('.') : -1;
      string viewName = parent == null || length < 0 ? parent : parent.Substring(0, length);
      bool flag = addScreenViews && !string.IsNullOrEmpty(parent) && siteMapNode != null && workflowService != null;
      if (viewName == "GeneralInfo")
      {
        pxCacheInfo = new PXCacheInfo(typeof (Access.AccessInfoNotification));
        flag = false;
      }
      PXViewInfo pxViewInfo1 = graphViews.FirstOrDefault<PXViewInfo>((Func<PXViewInfo, bool>) (v => v.Name == viewName)) ?? graphSingleViews.FirstOrDefault<PXViewInfo>((Func<PXViewInfo, bool>) (v => v.Name == viewName));
      if (pxViewInfo1 != null)
      {
        pxCacheInfo = pxViewInfo1.Cache;
        flag = false;
      }
      if (flag)
      {
        string[] fields = parent.Split('.');
        if (fields.Length == 2)
        {
          AUWorkflowFormField workflowFormField = ((IEnumerable<AUWorkflowFormField>) workflowService.GetWorkflowFormFields(siteMapNode?.ScreenID, fields[0])).FirstOrDefault<AUWorkflowFormField>((Func<AUWorkflowFormField, bool>) (field => field.FieldName.OrdinalEquals(fields[1])));
          if (workflowFormField != null)
          {
            System.Type dacType;
            string name;
            FormFieldHelper.TryGetFieldFromFormFieldName(graph, workflowFormField.SchemaField, out dacType, out name);
            pxCacheInfo = new PXCacheInfo(dacType);
            parent = string.Join('.'.ToString(), fields[0], name);
          }
        }
      }
      PXFieldState[] pxFieldStateArray;
      int index;
      if (pxCacheInfo != null)
      {
        if (length < 0)
        {
          if ("GeneralInfo".Equals(viewName, StringComparison.Ordinal))
          {
            foreach (object generalInfoField in EMailSourceHelper.GetGeneralInfoFields(graph, parent))
              yield return generalInfoField;
          }
          else
          {
            i = 0;
            System.Type type = GraphHelper.GetType(pxCacheInfo.Name);
            PXDBAttributeAttribute.Activate(graph.Caches[type]);
            using (new PXCache.IndirectMappingScope(GraphHelper.GetType(graphType)))
            {
              PXFieldState[] pxFieldStateArray1;
              PXViewDescription parentContainer;
              if (!string.IsNullOrEmpty(parent) && graphSingleViews.Any<PXViewInfo>((Func<PXViewInfo, bool>) (view => view.Name == parent)) && screenInfo != null && screenInfo.Containers.TryGetValue(parent, out parentContainer))
              {
                pxFieldStateArray1 = ((IEnumerable<PXFieldState>) PXFieldState.GetFields(graph, new System.Type[1]
                {
                  type
                }, false)).Where<PXFieldState>((Func<PXFieldState, bool>) (field => ((IEnumerable<PX.Data.Description.FieldInfo>) parentContainer.Fields).Any<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (fieldInfo => fieldInfo.FieldName == field.Name)))).ToArray<PXFieldState>();
              }
              else
              {
                PXFieldState[] pxFieldStateArray2;
                if (!flag)
                  pxFieldStateArray2 = PXFieldState.GetFields(graph, new System.Type[1]
                  {
                    type
                  }, false);
                else
                  pxFieldStateArray2 = workflowService.GetFormFields(siteMapNode.ScreenID, GraphHelper.CreateGraph(graphType), parent);
                pxFieldStateArray1 = pxFieldStateArray2;
              }
              pxFieldStateArray = pxFieldStateArray1;
              for (index = 0; index < pxFieldStateArray.Length; ++index)
              {
                PXFieldState pxFieldState = pxFieldStateArray[index];
                if (pxFieldState.ViewName != null)
                {
                  pxFieldState.Visible = true;
                  pxFieldState.Visibility = PXUIVisibility.Dynamic;
                }
                if (((pxFieldState.Name.EndsWith("_Attributes") ? 0 : (!pxFieldState.Visible ? 1 : 0)) & (onlyVisible ? 1 : 0)) == 0 && (pxFieldState.Name.EndsWith("_Attributes") || !pxFieldState.Name.Contains("_")) && ((pxFieldState.Visibility & PXUIVisibility.Visible) == PXUIVisibility.Visible || (pxFieldState.Visibility & PXUIVisibility.Dynamic) == PXUIVisibility.Dynamic))
                  yield return (object) new CacheEntityItem()
                  {
                    Key = $"{parent ?? "null"}.{pxFieldState.Name}",
                    SubKey = pxFieldState.Name,
                    Path = $"(({(string.IsNullOrEmpty(viewName) ? pxFieldState.Name : $"{viewName}.{pxFieldState.Name}")}))",
                    Name = pxFieldState.DisplayName,
                    Number = new int?(i++),
                    Icon = Sprite.Tree.GetFullUrl("Field")
                  };
              }
              pxFieldStateArray = (PXFieldState[]) null;
            }
          }
        }
        else if (parent != null)
        {
          string[] strArray = parent.Split('>');
          System.Type key = GraphHelper.GetType(pxCacheInfo.Name);
          strArray[0] = strArray[0].Substring(length + 1);
          for (int index1 = 0; index1 < strArray.Length && key != (System.Type) null; ++index1)
          {
            if (!(graph.Caches[key].GetStateExt((object) null, strArray[index1]) is PXFieldState stateExt) || stateExt.ViewName == null || !graph.Views.ContainsKey(stateExt.ViewName))
              yield break;
            BqlCommand bqlSelect = graph.Views[stateExt.ViewName].BqlSelect;
            if (bqlSelect == null)
              yield break;
            System.Type type = (System.Type) null;
            System.Type[] tables = bqlSelect.GetTables();
            if (tables != null && tables.Length != 0)
              type = tables[0];
            key = type == (System.Type) null || type == key ? (System.Type) null : type;
          }
          if (key != (System.Type) null)
          {
            i = 0;
            pxFieldStateArray = PXFieldState.GetFields(graph, new System.Type[1]
            {
              key
            }, false);
            for (index = 0; index < pxFieldStateArray.Length; ++index)
            {
              PXFieldState pxFieldState = pxFieldStateArray[index];
              if (pxFieldState.ViewName != null)
              {
                pxFieldState.Visible = true;
                pxFieldState.Visibility = PXUIVisibility.Dynamic;
              }
              if (((pxFieldState.Name.EndsWith("_Attributes") ? 0 : (!pxFieldState.Visible ? 1 : 0)) & (onlyVisible ? 1 : 0)) == 0 && !(string.IsNullOrEmpty(pxFieldState.DisplayName) & onlyVisible) && (pxFieldState.Name.EndsWith("_Attributes") || !pxFieldState.Name.Contains("_")) && ((pxFieldState.Visibility & PXUIVisibility.Visible) == PXUIVisibility.Visible || (pxFieldState.Visibility & PXUIVisibility.Dynamic) == PXUIVisibility.Dynamic))
                yield return (object) new CacheEntityItem()
                {
                  Key = $"{parent}>{pxFieldState.Name}",
                  SubKey = pxFieldState.Name,
                  Path = $"(({parent.Replace("null.", string.Empty).Replace('>', '.')}.{pxFieldState.Name}))",
                  Name = pxFieldState.DisplayName,
                  Number = new int?(i++),
                  Icon = Sprite.Tree.GetFullUrl("Field")
                };
            }
            pxFieldStateArray = (PXFieldState[]) null;
          }
        }
      }
    }
  }

  internal static IEnumerable<CacheEntityItem> GetGeneralInfoFields(PXGraph graph, string parent)
  {
    int f = 0;
    System.Type type = GraphHelper.GetType(new PXCacheInfo(typeof (Access.AccessInfoNotification)).Name);
    PXDBAttributeAttribute.Activate(graph.Caches[type]);
    PXFieldState[] pxFieldStateArray = PXFieldState.GetFields(graph, new System.Type[1]
    {
      type
    }, false);
    for (int index = 0; index < pxFieldStateArray.Length; ++index)
    {
      PXFieldState pxFieldState = pxFieldStateArray[index];
      if (pxFieldState.ViewName != null)
      {
        pxFieldState.Visible = true;
        pxFieldState.Visibility = PXUIVisibility.Dynamic;
      }
      yield return new CacheEntityItem()
      {
        Key = $"{parent ?? "null"}.{pxFieldState.Name}",
        SubKey = pxFieldState.Name,
        Path = $"((GeneralInfo.{pxFieldState.Name}))",
        Name = pxFieldState.DisplayName,
        Number = new int?(f++),
        Icon = Sprite.Tree.GetFullUrl("Field")
      };
    }
    pxFieldStateArray = (PXFieldState[]) null;
  }

  private static List<PXViewInfo> GetGraphViews(
    string screenId,
    string graphType,
    List<PXViewInfo> graphViews)
  {
    List<PXViewInfo> graphViews1 = new List<PXViewInfo>();
    foreach ((string Name, string DisplayName) singleEntitySection in ScreenUtils.GetGraphScreenSingleEntitySections(GraphHelper.CreateGraph(graphType), screenId))
    {
      string viewName = ScreenUtils.NormalizeViewName(singleEntitySection.Name);
      if (!graphViews.Any<PXViewInfo>((Func<PXViewInfo, bool>) (view => view.Name.OrdinalEquals(viewName))))
      {
        PXViewInfo graphView = GraphHelper.GetGraphView(graphType, viewName, true);
        if (graphView != null)
          graphViews1.Add(new PXViewInfo(singleEntitySection.Name, singleEntitySection.DisplayName, graphView.Cache));
      }
    }
    return graphViews1;
  }

  /// <exclude />
  public delegate bool MakeSiteMapCondition(System.Type x);
}
