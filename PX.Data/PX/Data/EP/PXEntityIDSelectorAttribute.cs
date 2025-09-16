// Decompiled with JetBrains decompiler
// Type: PX.Data.EP.PXEntityIDSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

#nullable disable
namespace PX.Data.EP;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
[Obfuscation(Exclude = true)]
public class PXEntityIDSelectorAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldSelectingSubscriber,
  IPXFieldUpdatingSubscriber
{
  private const string _VIEW_NAME_PREFIX = "_ENTITYID_SELECTOR_";
  private const string _VIEW_SEARCH_PREFIX = "_ENTITYID_SEARCHSELECTOR_";
  private const string _DESCRIPTION_FIELD_POSTFIX = "_Description";
  private readonly System.Type _typeBqlField;
  private string _typeField;
  private string _descriptionFieldName;

  public PXEntityIDSelectorAttribute(System.Type typeBqlField) => this._typeBqlField = typeBqlField;

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._typeField = sender.GetField(this._typeBqlField);
    this._descriptionFieldName = this._FieldName + "_Description";
    sender.Fields.Add(this._descriptionFieldName);
    sender.Graph.FieldSelecting.AddHandler(sender.GetItemType(), this._descriptionFieldName, new PXFieldSelecting(this._Description_FieldSelecting));
  }

  private void _Description_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    Guid? nullable = (Guid?) sender.GetValue(e.Row ?? sender.Current, this._FieldOrdinal);
    string entityDescription = !nullable.HasValue || !(nullable.Value != Guid.Empty) ? (string) null : new EntityHelper(sender.Graph).GetEntityDescription(new Guid?(nullable.Value), sender.GetItemType());
    bool flag = !string.IsNullOrEmpty(entityDescription);
    e.ReturnState = (object) PXFieldState.CreateInstance((object) entityDescription, typeof (string), fieldName: this._descriptionFieldName, displayName: this._descriptionFieldName, enabled: new bool?(false), visible: new bool?(flag), visibility: PXUIVisibility.Visible);
  }

  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue is string)
    {
      Guid result;
      Guid? nullable = Guid.TryParse((string) e.NewValue, out result) ? new Guid?(result) : new Guid?();
      if (nullable.HasValue)
      {
        e.NewValue = (object) nullable;
      }
      else
      {
        System.Type type = (e.Row ?? sender.Current).With<object, string>((Func<object, string>) (row => sender.GetValue(row, this._typeField) as string)).With<string, System.Type>((Func<string, System.Type>) (typeName => PXBuildManager.GetType(typeName, false)));
        PXCache cach = sender.Graph.Caches[type];
        string[] strArray1 = cach.GetAttributes((string) null).OfType<PXDBFieldAttribute>().Where<PXDBFieldAttribute>((Func<PXDBFieldAttribute, bool>) (_ => _.IsKey)).Select<PXDBFieldAttribute, string>((Func<PXDBFieldAttribute, string>) (_ => _.FieldName)).ToArray<string>();
        string[] strArray2;
        if (strArray1.Length > 1)
        {
          strArray2 = ((IEnumerable<string>) ((string) e.NewValue).Split(',')).Select<string, string>((Func<string, string>) (_ => _.Trim())).ToArray<string>();
        }
        else
        {
          strArray1 = new string[1]
          {
            EntityHelper.GetNoteAttribute(type).DescriptionField.Name
          };
          strArray2 = new string[1]{ (string) e.NewValue };
        }
        if (strArray2.Length != strArray1.Length)
          return;
        PXFieldState stateExt = (PXFieldState) sender.GetStateExt(e.Row, this._FieldName);
        PXView view = sender.Graph.Views[stateExt.ViewName];
        int num1 = 0;
        int num2 = 0;
        object[] searches = (object[]) strArray2;
        string[] sortcolumns = strArray1;
        bool[] descendings = new bool[1]{ true };
        ref int local1 = ref num1;
        ref int local2 = ref num2;
        List<object> objectList = view.Select((object[]) null, (object[]) null, searches, sortcolumns, descendings, (PXFilterRow[]) null, ref local1, 1, ref local2);
        if (objectList.Count == 0)
          return;
        string noteField = EntityHelper.GetNoteField(type);
        e.NewValue = !(objectList[0] is PXResult) ? cach.GetValue(objectList?[0], noteField) : cach.GetValue(((PXResult) objectList[0])[type], noteField);
      }
    }
    if (e.NewValue == null)
      return;
    System.Type type1 = (e.Row ?? sender.Current).With<object, string>((Func<object, string>) (row => sender.GetValue(row, this._typeField) as string)).With<string, System.Type>((Func<string, System.Type>) (typeName => PXBuildManager.GetType(typeName, false)));
    if (!(type1 != (System.Type) null))
      return;
    PXGraph graph = sender.Graph;
    PXCache cach1 = graph.Caches[type1];
    object entityRow = new EntityHelper(graph).GetEntityRow(type1, e.NewValue as Guid?);
    e.NewValue = (object) PXNoteAttribute.GetNoteID(cach1, entityRow, EntityHelper.GetNoteField(type1));
    graph.Caches[typeof (Note)].ClearQueryCache();
    graph.EnsureRowPersistence(entityRow);
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    System.Type type = (e.Row ?? sender.Current).With<object, string>((Func<object, string>) (row => sender.GetValue(row, this._typeField) as string)).With<string, System.Type>((Func<string, System.Type>) (typeName => PXBuildManager.GetType(typeName, false)));
    if (type != (System.Type) null)
    {
      PXGraph graph = sender.Graph;
      PXCache cach = graph.Caches[type];
      PXNoteAttribute noteAtt = EntityHelper.GetNoteAttribute(type);
      foreach (System.Type entityType in ((IEnumerable<System.Type>) cach.GetExtensionTypes()).Reverse<System.Type>())
      {
        PXNoteAttribute noteAttribute = EntityHelper.GetNoteAttribute(entityType);
        if (noteAttribute != null && noteAttribute.ShowInReferenceSelector)
        {
          noteAtt = noteAttribute;
          break;
        }
      }
      string viewName = (string) null;
      string[] fieldList = (string[]) null;
      string[] headerList = (string[]) null;
      this.CreateSelectorView(graph, type, noteAtt, out viewName, out fieldList, out headerList);
      if (noteAtt?.FieldList != null && noteAtt != null && noteAtt.FieldList.Length != 0)
      {
        fieldList = new string[noteAtt.FieldList.Length];
        for (int index = 0; index < noteAtt.FieldList.Length; ++index)
          fieldList[index] = noteAtt.FieldList[index].Name;
        headerList = (string[]) null;
      }
      if (fieldList == null)
        fieldList = new EntityHelper(graph).GetFieldList(type);
      if (headerList == null)
        headerList = this.GetFieldDisplayNames(cach, fieldList);
      string[] array = cach.Keys.ToArray();
      string noteField = EntityHelper.GetNoteField(type);
      string str = (noteAtt != null ? noteAtt.DescriptionField.With<System.Type, string>((Func<System.Type, string>) (df => df.Name)) : (string) null) ?? ((IEnumerable<string>) array).Last<string>();
      PXFieldState instance = PXFieldState.CreateInstance(e.ReturnState, typeof (long), fieldName: this._FieldName, displayName: PXLocalizer.Localize("Entity", typeof (Messages).FullName), viewName: viewName, fieldList: fieldList, headerList: headerList);
      e.ReturnState = (object) new PXEntityState((object) instance)
      {
        Keys = array,
        ValueField = noteField,
        TextField = str
      };
    }
    else
      e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (string), nullable: new bool?(true), fieldName: this._FieldName, displayName: PXLocalizer.Localize("Entity", typeof (Messages).FullName), enabled: new bool?(false));
  }

  protected virtual void CreateSelectorView(
    PXGraph graph,
    System.Type itemType,
    PXNoteAttribute noteAtt,
    out string viewName,
    out string[] fieldList,
    out string[] headerList)
  {
    viewName = (string) null;
    fieldList = (string[]) null;
    headerList = (string[]) null;
    if (noteAtt != null)
    {
      PXFieldState pxFieldState;
      if (typeof (IBqlField).IsAssignableFrom(noteAtt.Selector) && (pxFieldState = PXEntityIDSelectorAttribute.AddFieldView(graph, noteAtt.Selector)) != null)
      {
        viewName = pxFieldState.ViewName;
        fieldList = pxFieldState.FieldList;
        headerList = pxFieldState.HeaderList;
      }
      if (typeof (IBqlSearch).IsAssignableFrom(noteAtt.Selector))
        viewName = PXEntityIDSelectorAttribute.AddSelectorView(graph, noteAtt.Selector);
    }
    if (viewName != null)
      return;
    viewName = PXEntityIDSelectorAttribute.AddView(graph, itemType);
  }

  private string[] GetFieldDisplayNames(PXCache itemCache, string[] fieldList)
  {
    string[] fieldDisplayNames = new string[fieldList.Length];
    for (int index = 0; index < fieldList.Length; ++index)
    {
      string field = fieldList[index];
      fieldDisplayNames[index] = !(itemCache.GetStateExt((object) null, field) is PXFieldState stateExt) || string.IsNullOrEmpty(stateExt.DisplayName) ? field : stateExt.DisplayName;
    }
    return fieldDisplayNames;
  }

  protected static PXFieldState AddFieldView(PXGraph graph, System.Type selectorField)
  {
    System.Type itemType = BqlCommand.GetItemType(selectorField);
    PXCache cach = graph.Caches[itemType];
    return cach.GetStateExt((object) null, cach.GetField(selectorField)) as PXFieldState;
  }

  protected static string AddSelectorView(PXGraph graph, System.Type search)
  {
    string key = "_ENTITYID_SEARCHSELECTOR_" + MainTools.GetLongName(search.GenericTypeArguments[0]);
    if (!graph.Views.ContainsKey(key))
    {
      BqlCommand instance = BqlCommand.CreateInstance(search);
      PXView pxView = new PXView(graph, true, instance);
      graph.Views.Add(key, pxView);
    }
    else
    {
      BqlCommand instance = BqlCommand.CreateInstance(search);
      PXView pxView = new PXView(graph, true, instance);
    }
    return key;
  }

  protected static string AddView(PXGraph graph, System.Type itemType)
  {
    string key = "_ENTITYID_SELECTOR_" + MainTools.GetLongName(itemType);
    if (!graph.Views.ContainsKey(key))
    {
      BqlCommand instance = BqlCommand.CreateInstance(typeof (Select<>), itemType);
      PXView pxView = new PXView(graph, true, instance);
      graph.Views.Add(key, pxView);
    }
    return key;
  }
}
