// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCopyPasteData`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Api.Models;
using PX.Common;
using PX.Common.Extensions;
using PX.Data.Description;
using PX.Metadata;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;

#nullable disable
namespace PX.Data;

[Serializable]
public class PXCopyPasteData<TGraph> where TGraph : PXGraph
{
  internal const int MaxDetailsCount = 1000;
  private readonly List<RowClipboard> Rows = new List<RowClipboard>();
  internal string ScreenId;

  public PXCopyPasteData()
  {
    if (!(typeof (TGraph) != typeof (PXGraph)))
      return;
    this.ScreenId = PXCopyPasteData<TGraph>.GetScreenId(typeof (TGraph));
  }

  internal static string GetScreenId(System.Type graph)
  {
    PXSiteMapNode mapNodeByGraphType = PXSiteMap.Provider.FindSiteMapNodeByGraphType(CustomizedTypeManager.GetTypeNotCustomized(graph.FullName));
    try
    {
      if (string.Equals(HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath, mapNodeByGraphType.Url, StringComparison.InvariantCultureIgnoreCase))
      {
        if (PXContext.GetScreenID() != null)
          return PXContext.GetScreenID().ToUpperInvariant().Replace(".", "");
      }
    }
    catch
    {
    }
    return mapNodeByGraphType.ScreenID.ToUpperInvariant();
  }

  internal static bool IsCurrentUserClipboardAvailable => PXSharedUserSession.IsAvailable;

  public static PXCopyPasteData<PXGraph> CurrentUserClipboard
  {
    get
    {
      PXCopyPasteData<PXGraph> v = PXSharedUserSession.CurrentUser.GetValueByType<PXCopyPasteData<PXGraph>>();
      if (v == null)
      {
        v = new PXCopyPasteData<PXGraph>();
        PXSharedUserSession.CurrentUser.SetValueByType<PXCopyPasteData<PXGraph>>(v);
      }
      return v;
    }
  }

  public static void SaveClipboard(PXCopyPasteData<PXGraph> clipboard)
  {
    PXSharedUserSession.CurrentUser.SetValueByType<PXCopyPasteData<PXGraph>>(clipboard);
  }

  public bool IsEmpty() => !this.Rows.Any<RowClipboard>();

  public void LoadTemplateFromDb(int templateId)
  {
    this.Rows.Clear();
    this.ScreenId = ((IEnumerable<AUTemplate>) PXDatabase.GetSlot<AUTemplateCache>("AUTemplateCache", typeof (AUTemplate)).Items).Single<AUTemplate>((Func<AUTemplate, bool>) (t =>
    {
      int? templateId1 = t.TemplateID;
      int num = templateId;
      return templateId1.GetValueOrDefault() == num & templateId1.HasValue;
    })).ScreenID;
    List<Command> script;
    PXCopyPasteData<TGraph>.GetScript((TGraph) PXGraph.CreateInstance(PXBuildManager.GetType(PXSiteMap.Provider.FindSiteMapNodeByScreenID(this.ScreenId).GraphType, true)), this.ScreenId, false, out script, out List<Container> _);
    PXGraph graph = new PXGraph();
    object[] objArray = new object[1]{ (object) templateId };
    foreach (AUTemplateData auTemplateData in PXSelectBase<AUTemplateData, PXSelectReadonly<AUTemplateData, Where<Optional<AUTemplate.templateID>, Equal<AUTemplateData.templateId>>>.Config>.Select(graph, objArray).FirstTableItems.ToArray<AUTemplateData>())
    {
      AUTemplateData dataRow = auTemplateData;
      Command command = script.FirstOrDefault<Command>((Func<Command, bool>) (_ => _.Name == dataRow.FieldId));
      if (command != null)
        this.Rows.Add(new RowClipboard()
        {
          Active = dataRow.Active,
          CName = dataRow.Container,
          FName = dataRow.Field,
          ExternalName = command.Name,
          Line = dataRow.Line,
          OrderId = dataRow.OrderId,
          Value = dataRow.Value
        });
    }
  }

  public void PasteTo(TGraph graph)
  {
    graph.IsCopyPasteContext = true;
    if (this.ScreenId != graph.Accessinfo.ScreenID.ToUpperInvariant().Replace(".", ""))
      throw new PXException("Invalid screenId");
    PXSiteMap.ScreenInfo withInvariantLocale = ScreenUtils.ScreenInfo.GetWithInvariantLocale(this.ScreenId);
    List<Command> script;
    PXCopyPasteData<TGraph>.GetScript(graph, this.ScreenId, false, out script, out List<Container> _);
    Dictionary<string, Command> dictionary = EnumerableExtensions.Distinct<Command, string>((IEnumerable<Command>) script, (Func<Command, string>) (s => s.Name)).ToDictionary<Command, string>((Func<Command, string>) (_ => _.Name), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    List<Command> commands = new List<Command>();
    List<PX.Api.Models.Field> fieldList = new List<PX.Api.Models.Field>();
    int? nullable1 = new int?();
    string str = (string) null;
    foreach (RowClipboard row in this.Rows)
    {
      bool? active = row.Active;
      bool flag = true;
      if (active.GetValueOrDefault() == flag & active.HasValue)
      {
        int? nullable2 = nullable1;
        int? line = row.Line;
        if (!(nullable2.GetValueOrDefault() == line.GetValueOrDefault() & nullable2.HasValue == line.HasValue) || str != row.CName)
        {
          nullable1 = row.Line;
          str = row.CName;
          if (fieldList.Any<PX.Api.Models.Field>())
          {
            ScreenUtils.AddServiceCommands(this.ScreenId, (IReadOnlyList<PX.Api.Models.Field>) fieldList, withInvariantLocale);
            fieldList[fieldList.Count - 1].Commit = true;
            commands.AddRange((IEnumerable<Command>) fieldList);
            fieldList.Clear();
          }
        }
        Command command;
        if (dictionary.TryGetValue(row.ExternalName, out command))
        {
          PX.Api.Models.Value obj1 = new PX.Api.Models.Value();
          obj1.Commit = command.Commit;
          obj1.Value = PXCopyPasteData<TGraph>.EscapeValue(row.Value);
          obj1.FieldName = command.FieldName;
          obj1.Name = command.Name;
          obj1.ObjectName = command.ObjectName;
          obj1.IgnoreError = true;
          PX.Api.Models.Value obj2 = obj1;
          fieldList.Add((PX.Api.Models.Field) obj2);
        }
      }
    }
    if (fieldList.Any<PX.Api.Models.Field>())
    {
      ScreenUtils.AddServiceCommands(this.ScreenId, (IReadOnlyList<PX.Api.Models.Field>) fieldList, withInvariantLocale);
      commands.AddRange((IEnumerable<Command>) fieldList);
      fieldList.Clear();
    }
    foreach (string viewName in script.Select<Command, string>((Func<Command, string>) (field => PXCopyPasteData<TGraph>.GetViewName(field.ObjectName))).Distinct<string>().ToArray<string>())
      DialogManager.SetAnswer((PXGraph) graph, viewName, (string) null, WebDialogResult.Yes);
    PXGraph graph1 = (PXGraph) graph;
    string redirectContainerView = (string) null;
    string redirectScreen = (string) null;
    using (new PXInvariantCultureScope())
    {
      ScreenUtils.Submit(this.ScreenId, (IReadOnlyList<Command>) commands, SchemaMode.Basic, ref graph1, ref redirectContainerView, ref redirectScreen);
      graph.IsCopyPasteContext = false;
    }
  }

  private static string EscapeValue(string v)
  {
    if (v == null)
      return "=Null";
    v = SyCommand.EscapeValue(v);
    return $"='{v}'";
  }

  internal static string GetViewName(string v)
  {
    return Str.IsNullOrEmpty(v) ? "" : StringExtensions.FirstSegment(v, ':');
  }

  public void CopyFrom(TGraph graph) => this.CopyFrom(graph, false, MessageButtons.None);

  internal void CopyFrom(TGraph graph, bool showWarning, MessageButtons buttons)
  {
    graph.IsCopyPasteContext = true;
    PXView view = (PXView) null;
    PXCache cache = (PXCache) null;
    IBqlTable bqlTable = (IBqlTable) null;
    if (!string.IsNullOrWhiteSpace(graph.PrimaryView))
    {
      view = graph.Views[graph.PrimaryView];
      cache = view.Cache;
      bqlTable = (IBqlTable) cache.Current;
    }
    this.ScreenId = graph.Accessinfo.ScreenID.ToUpperInvariant().Replace(".", "");
    string primaryView = ScreenUtils.ScreenInfo.GetWithInvariantLocale(this.ScreenId).PrimaryView;
    if (cache == null)
    {
      view = graph.Views[primaryView];
      cache = view.Cache;
    }
    IBqlTable masterRow = (IBqlTable) cache.Current;
    if (bqlTable != null && bqlTable != masterRow)
    {
      using (new PXPreserveScope())
        graph = (TGraph) PXGraph.CreateInstance(graph.GetType());
      graph.Load();
      view = graph.Views[primaryView];
      cache = view.Cache;
      masterRow = (IBqlTable) cache.Current;
    }
    if (showWarning && view.Answer == WebDialogResult.OK)
    {
      graph.IsCopyPasteContext = false;
    }
    else
    {
      this.Rows.Clear();
      if (masterRow == null)
        throw new PXException("The current row is not selected.");
      List<Command> script;
      List<Container> containers;
      PXCopyPasteData<TGraph>.GetScript(graph, this.ScreenId, false, out script, out containers);
      PX.Api.Models.Filter[] array1 = cache.Keys.Select<string, PX.Api.Models.Filter>((Func<string, PX.Api.Models.Filter>) (key =>
      {
        return new PX.Api.Models.Filter()
        {
          Value = cache.GetValue((object) masterRow, key),
          Field = new PX.Api.Models.Field() { FieldName = key },
          Condition = FilterCondition.Equals,
          Operator = FilterOperator.And
        };
      })).ToArray<PX.Api.Models.Filter>();
      using (new PXInvariantCultureScope())
      {
        Command[] array2 = script.ToArray();
        string[][] list = ScreenUtils.ExportInternal(this.ScreenId, array2, array1, 0, 1000, false, false, (PXGraph) graph);
        Dictionary<string, string[]> requiredFields = PXCopyPasteEmptyFieldsAttribute.GetRequiredFields((PXGraph) graph);
        foreach (IndexedValue<string[]> indexedValue1 in ((IEnumerable<string[]>) list).EnumWithIndex<string[]>())
        {
          foreach (IndexedValue<string> indexedValue2 in ((IEnumerable<string>) indexedValue1.Value).EnumWithIndex<string>())
          {
            string str = indexedValue2.Value;
            Command cmd = array2[indexedValue2.Index];
            string viewName = PXCopyPasteData<TGraph>.GetViewName(cmd.ObjectName);
            bool flag1 = viewName.OrdinalEquals(primaryView);
            if ((!flag1 || indexedValue1.Index <= 0) && (!Str.IsNullOrEmpty(str) || requiredFields.ContainsKey(viewName) && ((IEnumerable<string>) requiredFields[viewName]).Any<string>((Func<string, bool>) (fieldName => cmd.FieldName.OrdinalEquals(fieldName))) && indexedValue1.Index <= 0))
            {
              bool flag2 = flag1 && (cmd.FieldName == "Hold" || cmd.FieldName == "Status");
              this.Rows.Add(new RowClipboard()
              {
                OrderId = new int?(this.Rows.Count),
                Active = new bool?(!flag2),
                Line = new int?(indexedValue1.Index),
                ExternalName = cmd.Name,
                FName = cmd.Descriptor.DisplayName,
                CName = containers[indexedValue2.Index].Name,
                Value = str
              });
            }
          }
        }
        if (showWarning)
        {
          if (list.Length != 0)
          {
            if (list.Length % 1000 == 0)
              this.showMaxDetailsCountWarning(view, buttons);
          }
        }
      }
      graph.IsCopyPasteContext = false;
    }
  }

  internal static void GetScript(
    TGraph graph,
    string screenID,
    bool isImportSimple,
    out List<Command> script,
    out List<Container> containers)
  {
    Content withServiceCommands = ScreenUtils.GetScreenInfoWithServiceCommands(true, false, screenID, true);
    script = new List<Command>();
    containers = new List<Container>();
    foreach (Container container in withServiceCommands.Containers)
    {
      PXViewDescription viewDescription = container.ViewDescription;
      if (viewDescription.IsGrid)
      {
        bool? pxGridAllowAddNew1 = viewDescription.PXGridAllowAddNew;
        bool flag1 = false;
        if (pxGridAllowAddNew1.GetValueOrDefault() == flag1 & pxGridAllowAddNew1.HasValue)
        {
          bool? pxGridAllowUpdate = viewDescription.PXGridAllowUpdate;
          bool flag2 = false;
          if (pxGridAllowUpdate.GetValueOrDefault() == flag2 & pxGridAllowUpdate.HasValue)
            continue;
        }
        bool? pxGridAllowAddNew2 = viewDescription.PXGridAllowAddNew;
        bool flag3 = false;
        if (pxGridAllowAddNew2.GetValueOrDefault() == flag3 & pxGridAllowAddNew2.HasValue && !viewDescription.HasSearchesByKey)
          continue;
      }
      bool flag = viewDescription.IsGrid && viewDescription.HasSearchesByKey;
      foreach (PX.Api.Models.Field field1 in container.Fields)
      {
        PX.Api.Models.Field field = field1;
        if (!(field.FieldName == "CuryViewState") && !field.FieldName.Contains("!") && (!field.FieldName.Contains("_") || (field.FieldName.EndsWith("_Date") || field.FieldName.EndsWith("_Time")) && field.FieldName.Length >= 6 && field.FieldName[field.FieldName.Length - 6] != '_'))
        {
          string viewName = PXCopyPasteData<TGraph>.GetViewName(field.ObjectName);
          if (!PXCopyPasteHiddenViewAttribute.IsHiddenView((PXGraph) graph, viewName, isImportSimple) && !graph._InactiveViews.ContainsKey(viewName) && !PXCopyPasteHiddenFieldsAttribute.IsHiddenField((PXGraph) graph, viewName, field.FieldName, isImportSimple) && !(viewName == "ChangeIDDialog") && (flag || graph.Views.ContainsKey(viewName) && !((IEnumerable<string>) graph.GetKeyNames(viewName)).Contains<string>(field.FieldName)))
          {
            PX.Data.Description.FieldInfo fieldInfo = ((IEnumerable<PX.Data.Description.FieldInfo>) viewDescription.Fields).FirstOrDefault<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (_ => _.FieldName == field.FieldName));
            if (fieldInfo == null || !fieldInfo.IsEditorControlDisabled)
            {
              field.Name = $"{field.ObjectName}/{field.FieldName}";
              field.Value = field.Name;
              script.Add((Command) field);
              containers.Add(container);
            }
          }
        }
      }
      if (!isImportSimple && container.ViewDescription.ViewName.OrdinalEquals(graph.PrimaryView))
      {
        PXCache cache = graph.Views[graph.PrimaryView].Cache;
        if (cache._KeyValueAttributeNames != null)
        {
          foreach (string key in cache._KeyValueAttributeNames.Keys)
          {
            PX.Api.Models.Field field2 = new PX.Api.Models.Field();
            field2.FieldName = key;
            field2.ObjectName = graph.PrimaryView;
            field2.Descriptor = new ElementDescriptor()
            {
              DisplayName = (cache.GetStateExt((object) null, key) is PXFieldState stateExt ? stateExt.DisplayName : (string) null) ?? key
            };
            PX.Api.Models.Field field3 = field2;
            field3.Value = field3.Name = $"{field3.ObjectName}/{field3.FieldName}";
            script.Add((Command) field3);
            containers.Add(container);
          }
        }
      }
    }
    graph.CopyPasteGetScript(isImportSimple, script, containers);
  }

  public AUTemplateController CreateTemplate(string title)
  {
    AUTemplateController template = new AUTemplateController();
    AUTemplate auTemplate1 = new AUTemplate()
    {
      ScreenID = this.ScreenId,
      Description = title
    };
    AUTemplate auTemplate2 = template.Filter.Insert(auTemplate1);
    foreach (RowClipboard row in this.Rows)
    {
      AUTemplateData auTemplateData = new AUTemplateData()
      {
        TemplateId = auTemplate2.TemplateID,
        Active = row.Active,
        Container = row.CName,
        Field = row.FName,
        FieldId = row.ExternalName,
        Line = row.Line,
        OrderId = row.OrderId,
        Value = row.Value
      };
      template.Items.Insert(auTemplateData);
    }
    return template;
  }

  /// <summary>
  /// Creates template in database<br />
  /// returns template id
  /// </summary>
  /// <param name="title"></param>
  /// <returns></returns>
  public int SaveAsTemplate(string title)
  {
    AUTemplateController template = this.CreateTemplate(title);
    template.Save.Press();
    return template.Filter.Current.TemplateID.Value;
  }

  public void ImportValues(IEnumerable<KeyValuePair<string, string>> values)
  {
    this.Rows.Clear();
    int val1 = 0;
    int count = -1;
    foreach (KeyValuePair<string, string> keyValuePair in values)
    {
      KeyValuePair<string, string> pair = keyValuePair;
      ++count;
      int val2 = values.Take<KeyValuePair<string, string>>(count).Count<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (_ => _.Key == pair.Key));
      val1 = System.Math.Max(val1, val2);
      this.Rows.Add(new RowClipboard()
      {
        Active = new bool?(true),
        ExternalName = pair.Key,
        Line = new int?(val1),
        OrderId = new int?(count),
        Value = pair.Value
      });
    }
  }

  public List<KeyValuePair<string, string>> ExportValues()
  {
    return this.Rows.Where<RowClipboard>((Func<RowClipboard, bool>) (row =>
    {
      bool? active = row.Active;
      bool flag = true;
      return active.GetValueOrDefault() == flag & active.HasValue;
    })).Select<RowClipboard, KeyValuePair<string, string>>((Func<RowClipboard, KeyValuePair<string, string>>) (row => new KeyValuePair<string, string>(row.ExternalName, row.Value))).ToList<KeyValuePair<string, string>>();
  }

  private void showMaxDetailsCountWarning(PXView view, MessageButtons buttons)
  {
    string message = string.Format(PXLocalizer.Localize("Only first {0} detail lines will be copied."), (object) 1000);
    int num = (int) view.Ask(view.Cache.Current, "Warning", message, buttons, MessageIcon.Information);
  }
}
