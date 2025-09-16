// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSEntityIDSelectorAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Objects.CR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.FS;

public class FSEntityIDSelectorAttribute : EntityIDSelectorAttribute
{
  private const string _DESCRIPTION_FIELD_POSTFIX = "_Description";
  private readonly System.Type _typeBqlField;
  private string _typeField;
  private string _descriptionFieldName;

  public FSEntityIDSelectorAttribute(System.Type typeBqlField)
    : base(typeBqlField)
  {
    this._typeBqlField = typeBqlField;
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._typeField = sender.GetField(this._typeBqlField);
    this._descriptionFieldName = this._FieldName + "_Description";
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    System.Type itemType = (e.Row ?? sender.Current).With<object, string>((Func<object, string>) (row => sender.GetValue(row, this._typeField) as string)).With<string, System.Type>((Func<string, System.Type>) (typeName => PXBuildManager.GetType(typeName, false)));
    if (itemType != (System.Type) null)
    {
      PXGraph graph = sender.Graph;
      PXCache cach = graph.Caches[itemType];
      PXNoteAttribute noteAttribute = EntityHelper.GetNoteAttribute(itemType, true);
      string viewName = (string) null;
      string[] fieldList = (string[]) null;
      string[] headerList = (string[]) null;
      this.CreateSelectorView(graph, itemType, noteAttribute, out viewName, out fieldList, out headerList);
      if (noteAttribute.FieldList != null && noteAttribute.FieldList.Length != 0)
      {
        fieldList = new string[noteAttribute.FieldList.Length];
        for (int index = 0; index < noteAttribute.FieldList.Length; ++index)
          fieldList[index] = noteAttribute.FieldList[index].Name;
        headerList = (string[]) null;
      }
      if (fieldList == null)
        fieldList = new EntityHelper(graph).GetFieldList(itemType);
      if (headerList == null)
        headerList = this.GetFieldDisplayNames(cach, fieldList);
      string[] array = cach.Keys.ToArray();
      string noteField = EntityHelper.GetNoteField(itemType);
      string str = noteAttribute.DescriptionField.With<System.Type, string>((Func<System.Type, string>) (df => df.Name)) ?? ((IEnumerable<string>) array).Last<string>();
      PXFieldState instance = PXFieldState.CreateInstance(e.ReturnState, (System.Type) null, new bool?(), new bool?(), new int?(), new int?(), new int?(), (object) null, this._FieldName, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, viewName, fieldList, headerList);
      instance.ValueField = noteField;
      instance.DescriptionName = str;
      if (sender.Graph.IsContractBasedAPI)
        return;
      e.ReturnState = (object) instance;
    }
    else
    {
      e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (System.Type) null, new bool?(), new bool?(), new int?(), new int?(), new int?(), (object) null, this._FieldName, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(e.Row == null), new bool?(), new bool?(), (PXUIVisibility) 0, sender.Graph.PrimaryView, (string[]) null, (string[]) null);
      ((PXFieldState) e.ReturnState).ValueField = "noteID";
      ((PXFieldState) e.ReturnState).SelectorMode = (PXSelectorMode) 1;
      ((PXFieldState) e.ReturnState).DescriptionName = this._descriptionFieldName;
    }
  }

  public string[] GetFieldDisplayNames(PXCache itemCache, string[] fieldList)
  {
    string[] fieldDisplayNames = new string[fieldList.Length];
    for (int index = 0; index < fieldList.Length; ++index)
    {
      string field = fieldList[index];
      fieldDisplayNames[index] = !(itemCache.GetStateExt((object) null, field) is PXFieldState stateExt) || string.IsNullOrEmpty(stateExt.DisplayName) ? field : stateExt.DisplayName;
    }
    return fieldDisplayNames;
  }
}
