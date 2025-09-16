// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Export.MappingFieldNameTree.MappingFieldTreeNodeFactory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.CS;
using PX.Data.Description;
using PX.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Api.Export.MappingFieldNameTree;

internal class MappingFieldTreeNodeFactory
{
  private readonly PXSiteMap.ScreenInfo _screenInfo;
  private readonly PXGraph _graph;
  private readonly string _screenId;
  private readonly bool _isImport;
  private readonly bool _viewsAndFieldsMode;

  internal MappingFieldTreeNodeFactory(
    PXSiteMap.ScreenInfo screenInfo,
    PXGraph graph,
    string screenId,
    bool isImport = false,
    bool viewsAndFieldsMode = false)
  {
    this._screenInfo = screenInfo ?? throw new PXArgumentException(nameof (screenInfo));
    this._graph = graph ?? throw new PXArgumentException(nameof (graph));
    this._screenId = screenId ?? throw new PXArgumentException(nameof (screenId));
    this._isImport = isImport;
    this._viewsAndFieldsMode = viewsAndFieldsMode;
  }

  private string ActionIconUrl => Sprite.Ac.GetFullUrl("play_arrow");

  private string FieldIconUrl => Sprite.Tree.GetFullUrl("Field");

  internal IEnumerable<MappingFieldTreeNode> CreateChildNodes(string parentNodeKey)
  {
    (RootNodeType? Type, string str) = MappingFieldNodeKeyParser.GetRootNodeInfo(parentNodeKey);
    IEnumerable<MappingFieldTreeNode> childNodes;
    if (Type.HasValue)
    {
      switch (Type.GetValueOrDefault())
      {
        case RootNodeType.Root:
          childNodes = this.CreateRootNodes();
          goto label_6;
        case RootNodeType.Actions:
          childNodes = this.CreateActionsNodes();
          goto label_6;
        case RootNodeType.View:
          childNodes = this.CreateViewChildNodes(str);
          goto label_6;
      }
    }
    childNodes = (IEnumerable<MappingFieldTreeNode>) Array.Empty<MappingFieldTreeNode>();
label_6:
    return childNodes;
  }

  private IEnumerable<MappingFieldTreeNode> CreateRootNodes()
  {
    int nextOrderNumber1 = 0;
    if (this._viewsAndFieldsMode)
      return this.CreateViewNodes(nextOrderNumber1);
    MappingFieldTreeNode mappingFieldTreeNode1 = new MappingFieldTreeNode();
    mappingFieldTreeNode1.Key = "<ActionsNode>";
    mappingFieldTreeNode1.Text = MappingFieldNodeTextGenerator.ActionsNodeText;
    int num = nextOrderNumber1;
    int nextOrderNumber2 = num + 1;
    mappingFieldTreeNode1.OrderNumber = new int?(num);
    MappingFieldTreeNode mappingFieldTreeNode2 = mappingFieldTreeNode1;
    IEnumerable<MappingFieldTreeNode> viewNodes = this.CreateViewNodes(nextOrderNumber2);
    List<MappingFieldTreeNode> items = new List<MappingFieldTreeNode>();
    items.Add(mappingFieldTreeNode2);
    foreach (MappingFieldTreeNode mappingFieldTreeNode3 in viewNodes)
      items.Add(mappingFieldTreeNode3);
    return (IEnumerable<MappingFieldTreeNode>) new \u003C\u003Ez__ReadOnlyList<MappingFieldTreeNode>(items);
  }

  internal MappingFieldTreeNode CreateViewNode(
    string name,
    string displayName,
    ref int nextOrderNumber)
  {
    return new MappingFieldTreeNode()
    {
      Key = MappingFieldNodeTextGenerator.GetViewNodeKey(name),
      Text = MappingFieldNodeTextGenerator.GetViewNodeText(ScreenUtils.NormalizeViewName(name), displayName),
      OrderNumber = new int?(nextOrderNumber++)
    };
  }

  private IEnumerable<MappingFieldTreeNode> CreateViewNodes(int nextOrderNumber)
  {
    return this._screenInfo.Containers.Where<KeyValuePair<string, PXViewDescription>>((Func<KeyValuePair<string, PXViewDescription>, bool>) (c => this._graph.Views.ContainsKey(ScreenUtils.NormalizeViewName(c.Key)))).Select(c => new
    {
      Name = c.Key,
      DisplayName = c.Value.DisplayName
    }).OrderBy(v => v.DisplayName).Select(v => this.CreateViewNode(v.Name, v.DisplayName, ref nextOrderNumber));
  }

  private IEnumerable<MappingFieldTreeNode> CreateActionsNodes()
  {
    if (this._screenInfo.Actions != null)
    {
      PXSiteMap.ScreenInfo.Action[] actionArray = this._screenInfo.Actions;
      if ((!this._screenInfo.HasWorkflow ? 0 : (this._isImport ? 1 : 0)) != 0)
      {
        PXSiteMap.ScreenInfo.Action action = new PXSiteMap.ScreenInfo.Action("WorkflowTransition", PXLocalizer.Localize("Transition"), true, PXSpecialButtonType.ActionsFolder, false, true);
        actionArray = ((IEnumerable<PXSiteMap.ScreenInfo.Action>) actionArray).Union<PXSiteMap.ScreenInfo.Action>((IEnumerable<PXSiteMap.ScreenInfo.Action>) new \u003C\u003Ez__ReadOnlyArray<PXSiteMap.ScreenInfo.Action>(new PXSiteMap.ScreenInfo.Action[1]
        {
          action
        })).ToArray<PXSiteMap.ScreenInfo.Action>();
      }
      IOrderedEnumerable<\u003C\u003Ef__AnonymousType4<string, string>> orderedEnumerable = ((IEnumerable<PXSiteMap.ScreenInfo.Action>) actionArray).Select(a => new
      {
        Name = a.Name,
        DisplayName = string.IsNullOrEmpty(a.DisplayName) ? a.Name : a.DisplayName
      }).OrderBy(a => a.DisplayName);
      string actionView = this._screenInfo.PrimaryView;
      int nextOrderNumber = 0;
      foreach (var data in orderedEnumerable)
      {
        string actionNodeKey = MappingFieldNodeTextGenerator.GetActionNodeKey(actionView, data.Name);
        yield return new MappingFieldTreeNode()
        {
          Key = actionNodeKey,
          Value = actionNodeKey,
          Text = MappingFieldNodeTextGenerator.GetActionNodeText(data.Name, data.DisplayName),
          OrderNumber = new int?(nextOrderNumber++),
          Icon = this.ActionIconUrl
        };
      }
      string setBranchNodeKey = MappingFieldNodeTextGenerator.GetSetBranchNodeKey(actionView);
      yield return new MappingFieldTreeNode()
      {
        Key = setBranchNodeKey,
        Value = setBranchNodeKey,
        Text = MappingFieldNodeTextGenerator.SetBranchNodeText,
        OrderNumber = new int?(nextOrderNumber),
        Icon = this.ActionIconUrl
      };
    }
  }

  private IEnumerable<MappingFieldTreeNode> CreateViewChildNodes(string viewName)
  {
    if (this._screenInfo.Containers.ContainsKey(viewName))
    {
      int nextOrderNumber = 0;
      if (!this._viewsAndFieldsMode)
      {
        string fieldNodeKey = MappingFieldNodeTextGenerator.GetFieldNodeKey(viewName, "<Add All Fields>");
        yield return new MappingFieldTreeNode()
        {
          Key = fieldNodeKey,
          Value = fieldNodeKey,
          Text = MappingFieldNodeTextGenerator.AddAllFieldsNodeText,
          OrderNumber = new int?(nextOrderNumber++),
          Icon = this.ActionIconUrl
        };
      }
      foreach (MappingFieldTreeNode fieldNode in this.CreateFieldNodes(viewName, ref nextOrderNumber))
        yield return fieldNode;
      if (!this._viewsAndFieldsMode)
      {
        foreach (MappingFieldTreeNode parameterNode in this.CreateParameterNodes(viewName, ref nextOrderNumber))
          yield return parameterNode;
        if (this._screenInfo.Containers[viewName].HasNoteID)
        {
          string externalKeyNodeKey = MappingFieldNodeTextGenerator.GetExternalKeyNodeKey(viewName);
          yield return new MappingFieldTreeNode()
          {
            Key = externalKeyNodeKey,
            Value = externalKeyNodeKey,
            Text = MappingFieldNodeTextGenerator.GetExternalKeyNodeText(viewName),
            OrderNumber = new int?(nextOrderNumber++),
            Icon = this.FieldIconUrl
          };
        }
        if (this._screenInfo.Containers[viewName].HasLineNumber)
        {
          string lineNumberNodeKey = MappingFieldNodeTextGenerator.GetLineNumberNodeKey(viewName);
          yield return new MappingFieldTreeNode()
          {
            Key = lineNumberNodeKey,
            Value = lineNumberNodeKey,
            Text = MappingFieldNodeTextGenerator.GetLineNumberNodeText(),
            OrderNumber = new int?(nextOrderNumber++),
            Icon = this.FieldIconUrl
          };
        }
        string dialogAnswerNodeKey = MappingFieldNodeTextGenerator.GetDialogAnswerNodeKey(viewName);
        yield return new MappingFieldTreeNode()
        {
          Key = dialogAnswerNodeKey,
          Value = dialogAnswerNodeKey,
          Text = MappingFieldNodeTextGenerator.GetDialogAnswerNodeText(),
          OrderNumber = new int?(nextOrderNumber),
          Icon = this.FieldIconUrl
        };
      }
    }
  }

  private IEnumerable<MappingFieldTreeNode> CreateParameterNodes(
    string viewName,
    ref int nextOrderNumber)
  {
    ParsInfo[] parameters = this._screenInfo.Containers[viewName].Parameters;
    if (parameters == null)
      return (IEnumerable<MappingFieldTreeNode>) Array.Empty<MappingFieldTreeNode>();
    List<MappingFieldTreeNode> parameterNodes = new List<MappingFieldTreeNode>();
    foreach (ParsInfo parsInfo in parameters)
    {
      if (parsInfo.Type != ParType.Filters)
      {
        MappingFieldTreeNode mappingFieldTreeNode = new MappingFieldTreeNode()
        {
          OrderNumber = new int?(nextOrderNumber++),
          Icon = this.FieldIconUrl
        };
        if (parsInfo.Type == ParType.Parameters)
        {
          string parameterNodeKey = MappingFieldNodeTextGenerator.GetParameterNodeKey(viewName, parsInfo.Name);
          mappingFieldTreeNode.Key = parameterNodeKey;
          mappingFieldTreeNode.Value = parameterNodeKey;
          mappingFieldTreeNode.Text = MappingFieldNodeTextGenerator.GetParameterNodeText(viewName, parsInfo.Name);
        }
        else
        {
          string searchNodeKey = MappingFieldNodeTextGenerator.GetSearchNodeKey(viewName, parsInfo.Field, parsInfo.Name);
          mappingFieldTreeNode.Key = searchNodeKey;
          mappingFieldTreeNode.Value = searchNodeKey;
          mappingFieldTreeNode.Text = MappingFieldNodeTextGenerator.GetSearchNodeText(viewName, parsInfo.Name);
        }
        parameterNodes.Add(mappingFieldTreeNode);
      }
    }
    return (IEnumerable<MappingFieldTreeNode>) parameterNodes;
  }

  internal MappingFieldTreeNode CreateFieldNode(
    string viewName,
    string name,
    string displayName,
    ref int nextOrderNumber)
  {
    string fieldNodeKey1 = MappingFieldNodeTextGenerator.GetFieldNodeKey(viewName, name);
    string fieldNodeKey2 = MappingFieldNodeTextGenerator.GetFieldNodeKey(ScreenUtils.NormalizeViewName(viewName), name);
    return new MappingFieldTreeNode()
    {
      Key = fieldNodeKey1,
      Value = fieldNodeKey1,
      Text = MappingFieldNodeTextGenerator.GetFieldNodeText(fieldNodeKey2, displayName),
      OrderNumber = new int?(nextOrderNumber++),
      Icon = this.FieldIconUrl
    };
  }

  private IEnumerable<MappingFieldTreeNode> CreateFieldNodes(
    string viewName,
    ref int nextOrderNumber)
  {
    List<MappingFieldTreeNode> fieldNodes = new List<MappingFieldTreeNode>();
    List<PX.Data.Description.FieldInfo> source = new List<PX.Data.Description.FieldInfo>((IEnumerable<PX.Data.Description.FieldInfo>) this._screenInfo.Containers[viewName].Fields);
    string str = ScreenUtils.NormalizeViewName(viewName);
    if (str.Equals(this._screenInfo.PrimaryView, StringComparison.OrdinalIgnoreCase))
    {
      Tuple<PXFieldState, short, short, string>[] attributeFields = KeyValueHelper.GetAttributeFields(this._screenId);
      if (attributeFields.Length != 0)
      {
        IEnumerable<PX.Data.Description.FieldInfo> collection = ((IEnumerable<Tuple<PXFieldState, short, short, string>>) attributeFields).Select<Tuple<PXFieldState, short, short, string>, PX.Data.Description.FieldInfo>((Func<Tuple<PXFieldState, short, short, string>, PX.Data.Description.FieldInfo>) (_ => new PX.Data.Description.FieldInfo(_.Item1.Name, _.Item1.DisplayName, (CallbackDescr) null, false, _.Item1.DataType, true, false, true, false, (string) null, (string) null, (string) null, (object) null, (object) null, -1, -1, (string[]) null, false)));
        source.AddRange(collection);
      }
    }
    foreach (PX.Data.Description.FieldInfo fieldInfo in (IEnumerable<PX.Data.Description.FieldInfo>) source.OrderBy<PX.Data.Description.FieldInfo, string>((Func<PX.Data.Description.FieldInfo, string>) (f => f.DisplayName)))
    {
      MappingFieldNodeTextGenerator.GetFieldNodeKey(viewName, fieldInfo.FieldName);
      MappingFieldNodeTextGenerator.GetFieldNodeKey(str, fieldInfo.FieldName);
      MappingFieldTreeNode fieldNode = this.CreateFieldNode(viewName, fieldInfo.FieldName, fieldInfo.DisplayName, ref nextOrderNumber);
      fieldNodes.Add(fieldNode);
      foreach ((string FieldName, string DisplayName) selectorField in this.GetSelectorFields(this._graph.Views[str].Cache, fieldInfo.FieldName))
      {
        string selectorFieldNodeKey1 = MappingFieldNodeTextGenerator.GetSelectorFieldNodeKey(viewName, fieldInfo.FieldName, selectorField.FieldName);
        string selectorFieldNodeKey2 = MappingFieldNodeTextGenerator.GetSelectorFieldNodeKey(str, fieldInfo.FieldName, selectorField.FieldName);
        MappingFieldTreeNode mappingFieldTreeNode = new MappingFieldTreeNode()
        {
          Key = selectorFieldNodeKey1,
          Value = selectorFieldNodeKey1,
          Text = MappingFieldNodeTextGenerator.GetSelectorFieldNodeText(fieldInfo.DisplayName, selectorFieldNodeKey2, selectorField.DisplayName),
          OrderNumber = new int?(nextOrderNumber++),
          Icon = this.FieldIconUrl
        };
        fieldNodes.Add(mappingFieldTreeNode);
      }
    }
    return (IEnumerable<MappingFieldTreeNode>) fieldNodes;
  }

  private IEnumerable<(string FieldName, string DisplayName)> GetSelectorFields(
    PXCache cache,
    string fieldName)
  {
    PXFieldState fieldState = cache.GetStateExt((object) null, fieldName) as PXFieldState;
    if (fieldState != null && fieldState.FieldList != null && fieldState.HeaderList != null)
    {
      for (int i = 0; i < fieldState.FieldList.Length; ++i)
        yield return (fieldState.FieldList[i], fieldState.HeaderList[i]);
    }
  }
}
