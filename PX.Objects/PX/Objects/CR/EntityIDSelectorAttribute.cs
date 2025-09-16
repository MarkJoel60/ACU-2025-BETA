// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.EntityIDSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.EP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.CR;

public class EntityIDSelectorAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldSelectingSubscriber,
  IPXFieldUpdatingSubscriber,
  IPXDependsOnFields
{
  protected PXGraph Graph;

  public bool LastKeyOnly { get; set; }

  protected static string ViewNamePrefix => "_ENTITYID_SELECTOR_";

  protected static string ViewSearchPrefix => "_ENTITYID_SEARCHSELECTOR_";

  protected string DescriptionFieldPostfix => "_Description";

  protected System.Type _typeBqlField { get; set; }

  protected string _typeFieldName { get; set; }

  protected string _descriptionFieldName { get; set; }

  public EntityIDSelectorAttribute(System.Type typeBqlField) => this._typeBqlField = typeBqlField;

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.Graph = sender.Graph;
    this._typeFieldName = sender.GetField(this._typeBqlField);
    this._descriptionFieldName = this._FieldName + this.DescriptionFieldPostfix;
    sender.Fields.Add(this._descriptionFieldName);
    EntityIDSelectorAttribute.AddView(this.Graph, typeof (Note));
    PXGraph.FieldSelectingEvents fieldSelecting = this.Graph.FieldSelecting;
    System.Type itemType = sender.GetItemType();
    string descriptionFieldName = this._descriptionFieldName;
    EntityIDSelectorAttribute selectorAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting = new PXFieldSelecting((object) selectorAttribute, __vmethodptr(selectorAttribute, _Description_FieldSelecting));
    fieldSelecting.AddHandler(itemType, descriptionFieldName, pxFieldSelecting);
  }

  public virtual void _Description_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    string description = this.GetDescription(sender, e.Row);
    PXFieldSelectingEventArgs selectingEventArgs = e;
    string str1 = description;
    System.Type type = typeof (string);
    string descriptionFieldName1 = this._descriptionFieldName;
    string descriptionFieldName2 = this._descriptionFieldName;
    bool? nullable1 = new bool?(false);
    bool? nullable2 = new bool?(!string.IsNullOrEmpty(description));
    bool? nullable3 = new bool?();
    bool? nullable4 = new bool?();
    int? nullable5 = new int?();
    int? nullable6 = new int?();
    int? nullable7 = new int?();
    string str2 = descriptionFieldName1;
    string str3 = descriptionFieldName2;
    bool? nullable8 = nullable1;
    bool? nullable9 = nullable2;
    bool? nullable10 = new bool?();
    PXFieldState instance = PXFieldState.CreateInstance((object) str1, type, nullable3, nullable4, nullable5, nullable6, nullable7, (object) null, str2, (string) null, str3, (string) null, (PXErrorLevel) 0, nullable8, nullable9, nullable10, (PXUIVisibility) 3, (string) null, (string[]) null, (string[]) null);
    selectingEventArgs.ReturnState = (object) instance;
  }

  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!(e.NewValue is string))
      return;
    Guid result;
    Guid? nullable = Guid.TryParse((string) e.NewValue, out result) ? new Guid?(result) : new Guid?();
    if (nullable.HasValue)
    {
      e.NewValue = (object) nullable;
    }
    else
    {
      System.Type type = (e.Row ?? sender.Current).With<object, string>((Func<object, string>) (row => sender.GetValue(row, this._typeFieldName) as string)).With<string, System.Type>((Func<string, System.Type>) (typeName => PXBuildManager.GetType(typeName, false)));
      PXCache cach1 = this.Graph.Caches[type];
      string[] strArray1 = cach1.GetAttributes((string) null).OfType<PXDBFieldAttribute>().Where<PXDBFieldAttribute>((Func<PXDBFieldAttribute, bool>) (_ => _.IsKey)).Select<PXDBFieldAttribute, string>((Func<PXDBFieldAttribute, string>) (_ => ((PXEventSubscriberAttribute) _).FieldName)).ToArray<string>();
      string[] strArray2;
      if (strArray1.Length > 1)
      {
        strArray2 = ((IEnumerable<string>) ((string) e.NewValue).Split(',')).Select<string, string>((Func<string, string>) (_ => _.Trim())).ToArray<string>();
      }
      else
      {
        strArray1 = new string[1]
        {
          EntityHelper.GetNoteAttribute(type, true).DescriptionField.Name
        };
        strArray2 = new string[1]{ (string) e.NewValue };
      }
      if (strArray2.Length != strArray1.Length)
        return;
      PXView view = this.Graph.Views[((PXFieldState) sender.GetStateExt(e.Row, this._FieldName)).ViewName];
      int num1 = 0;
      int num2 = 0;
      object[] objArray = (object[]) strArray2;
      string[] strArray3 = strArray1;
      bool[] flagArray = new bool[1]{ true };
      ref int local1 = ref num1;
      ref int local2 = ref num2;
      List<object> objectList = view.Select((object[]) null, (object[]) null, objArray, strArray3, flagArray, (PXFilterRow[]) null, ref local1, 1, ref local2);
      if (objectList.Count == 0)
        return;
      string noteField = EntityHelper.GetNoteField(type);
      if (objectList[0] is PXResult)
      {
        if (((PXResult) objectList[0])[type] != null)
        {
          e.NewValue = cach1.GetValue(((PXResult) objectList[0])[type], noteField);
        }
        else
        {
          object obj = ((PXResult) objectList[0])[0];
          PXCache cach2 = this.Graph.Caches[obj.GetType()];
          e.NewValue = cach2.GetValue(obj, noteField);
        }
      }
      else
        e.NewValue = cach1.GetValue(objectList?[0], noteField);
    }
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    System.Type itemType = (e.Row ?? sender.Current).With<object, string>((Func<object, string>) (row => sender.GetValue(row, this._typeFieldName) as string)).With<string, System.Type>((Func<string, System.Type>) (typeName => PXBuildManager.GetType(typeName, false)));
    if (itemType != (System.Type) null)
    {
      PXCache cach = this.Graph.Caches[itemType];
      PXNoteAttribute noteAtt = EntityHelper.GetNoteAttribute(itemType, false);
      foreach (System.Type type in ((IEnumerable<System.Type>) cach.GetExtensionTypes()).Reverse<System.Type>())
      {
        PXNoteAttribute noteAttribute = EntityHelper.GetNoteAttribute(type, true);
        if (noteAttribute != null && noteAttribute.ShowInReferenceSelector)
        {
          noteAtt = noteAttribute;
          break;
        }
      }
      string viewName;
      string[] fieldList1;
      string[] headerList;
      this.CreateSelectorView(this.Graph, itemType, noteAtt, out viewName, out fieldList1, out headerList);
      if (noteAtt.FieldList != null && noteAtt.FieldList.Length != 0)
      {
        fieldList1 = new string[noteAtt.FieldList.Length];
        for (int index = 0; index < noteAtt.FieldList.Length; ++index)
          fieldList1[index] = noteAtt.FieldList[index].Name;
        headerList = (string[]) null;
      }
      string[] fieldList2 = fieldList1 ?? new EntityHelper(this.Graph).GetFieldList(itemType);
      headerList = headerList ?? this.GetFieldDisplayNames(cach, fieldList2);
      string[] array = cach.Keys.ToArray();
      string noteField = EntityHelper.GetNoteField(itemType);
      string str1 = noteAtt.DescriptionField.With<System.Type, string>((Func<System.Type, string>) (df => df.Name)) ?? ((IEnumerable<string>) array).Last<string>();
      object returnState = e.ReturnState;
      string fieldName = this._FieldName;
      string str2 = viewName;
      string[] strArray1 = fieldList2;
      string[] strArray2 = headerList;
      bool? nullable1 = new bool?();
      bool? nullable2 = new bool?();
      int? nullable3 = new int?();
      int? nullable4 = new int?();
      int? nullable5 = new int?();
      string str3 = fieldName;
      bool? nullable6 = new bool?();
      bool? nullable7 = new bool?();
      bool? nullable8 = new bool?();
      string str4 = str2;
      string[] strArray3 = strArray1;
      string[] strArray4 = strArray2;
      PXFieldState instance = PXFieldState.CreateInstance(returnState, (System.Type) null, nullable1, nullable2, nullable3, nullable4, nullable5, (object) null, str3, (string) null, (string) null, (string) null, (PXErrorLevel) 0, nullable6, nullable7, nullable8, (PXUIVisibility) 0, str4, strArray3, strArray4);
      instance.ValueField = noteField;
      instance.DescriptionName = str1;
      instance.SelectorMode = (PXSelectorMode) 17;
      if (this.Graph.IsContractBasedAPI && !this.Graph.IsImport)
        return;
      e.ReturnState = (object) instance;
    }
    else
    {
      string str5 = EntityIDSelectorAttribute.AddView(this.Graph, typeof (Note));
      PXFieldSelectingEventArgs selectingEventArgs = e;
      object returnState = e.ReturnState;
      string fieldName = this._FieldName;
      bool? nullable9 = new bool?(e.Row == null);
      string str6 = str5;
      bool? nullable10 = new bool?();
      bool? nullable11 = new bool?();
      int? nullable12 = new int?();
      int? nullable13 = new int?();
      int? nullable14 = new int?();
      string str7 = fieldName;
      bool? nullable15 = nullable9;
      bool? nullable16 = new bool?();
      bool? nullable17 = new bool?();
      string str8 = str6;
      PXFieldState instance = PXFieldState.CreateInstance(returnState, (System.Type) null, nullable10, nullable11, nullable12, nullable13, nullable14, (object) null, str7, (string) null, (string) null, (string) null, (PXErrorLevel) 0, nullable15, nullable16, nullable17, (PXUIVisibility) 0, str8, (string[]) null, (string[]) null);
      selectingEventArgs.ReturnState = (object) instance;
      ((PXFieldState) e.ReturnState).ValueField = "noteID";
      ((PXFieldState) e.ReturnState).DescriptionName = this._descriptionFieldName;
      ((PXFieldState) e.ReturnState).SelectorMode = (PXSelectorMode) 17;
    }
  }

  protected virtual string GetDescription(PXCache sender, object row)
  {
    if (row == null)
      return (string) null;
    Guid? nullable = (Guid?) sender.GetValue(row, this._FieldOrdinal);
    System.Type type = sender.GetValue(row, this._typeFieldName) is string typeName ? PXBuildManager.GetType(typeName, false) : (System.Type) null;
    if (!nullable.HasValue || !(nullable.Value != Guid.Empty) || !(type != (System.Type) null))
      return (string) null;
    EntityHelper entityHelper = new EntityHelper(this.Graph);
    object entityRow = entityHelper.GetEntityRow(type, nullable, false);
    string entityDescription = EntityHelper.GetEntityDescription(this.Graph, entityRow);
    if (this.LastKeyOnly)
      return entityHelper.GetEntityRowID(type, entityRow, (string) null);
    return !string.IsNullOrEmpty(entityDescription) ? entityDescription : entityHelper.GetEntityRowID(type, entityRow, ", ");
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
    PXCache cach1 = graph.Caches[typeof (BAccount)];
    PXCache cach2 = graph.Caches[typeof (PX.Objects.AR.Customer)];
    PXCache cach3 = graph.Caches[typeof (PX.Objects.AP.Vendor)];
    PXCache cach4 = graph.Caches[typeof (EPEmployee)];
    PXFieldState pxFieldState;
    if (typeof (IBqlField).IsAssignableFrom(noteAtt.Selector) && (pxFieldState = EntityIDSelectorAttribute.AddFieldView(graph, noteAtt.Selector)) != null)
    {
      viewName = pxFieldState.ViewName;
      fieldList = pxFieldState.FieldList;
      headerList = pxFieldState.HeaderList;
    }
    if (typeof (IBqlSearch).IsAssignableFrom(noteAtt.Selector))
      viewName = EntityIDSelectorAttribute.AddSelectorView(graph, noteAtt.Selector);
    if (viewName != null)
      return;
    viewName = EntityIDSelectorAttribute.AddView(graph, itemType);
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

  protected static string GetSelectorViewName(System.Type nameType)
  {
    return $"{EntityIDSelectorAttribute.ViewSearchPrefix}{nameType?.DeclaringType?.FullName}.{nameType?.Name}";
  }

  protected static string AddSelectorView(PXGraph graph, System.Type search)
  {
    string selectorViewName = EntityIDSelectorAttribute.GetSelectorViewName(search.GenericTypeArguments[0]);
    if (!((Dictionary<string, PXView>) graph.Views).ContainsKey(selectorViewName))
    {
      BqlCommand instance = BqlCommand.CreateInstance(new System.Type[1]
      {
        search
      });
      PXView pxView = new PXView(graph, true, instance);
      graph.Views.Add(selectorViewName, pxView);
    }
    else
    {
      BqlCommand instance = BqlCommand.CreateInstance(new System.Type[1]
      {
        search
      });
      PXView pxView = new PXView(graph, true, instance);
    }
    return selectorViewName;
  }

  protected static string AddView(PXGraph graph, System.Type itemType)
  {
    string key = EntityIDSelectorAttribute.ViewNamePrefix + MainTools.GetLongName(itemType);
    if (((Dictionary<string, PXView>) graph.Views).ContainsKey(key))
      return key;
    BqlCommand instance = BqlCommand.CreateInstance(new System.Type[2]
    {
      typeof (Select<>),
      itemType
    });
    PXView pxView = new PXView(graph, true, instance);
    graph.Views.Add(key, pxView);
    return key;
  }

  public ISet<System.Type> GetDependencies(PXCache cache)
  {
    return (ISet<System.Type>) new HashSet<System.Type>()
    {
      this._typeBqlField
    };
  }
}
