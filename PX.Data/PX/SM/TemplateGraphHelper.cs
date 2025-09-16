// Decompiled with JetBrains decompiler
// Type: PX.SM.TemplateGraphHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.ProjectDefinition.Workflow;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

[PXInternalUseOnly]
public static class TemplateGraphHelper
{
  private const string FieldsDisplaySplitter = "->";

  public static void EnumAssigneeFields(
    string internalPath,
    string displayPath,
    PXGraph graph,
    System.Type table,
    List<System.Type> tables,
    Dictionary<string, string> names,
    Dictionary<string, string> fields,
    Func<System.Type, IEnumerable<string>> getFieldsDelegate)
  {
    PXCache cach = graph.Caches[table];
    if (TemplateGraphHelper.EnumAssigneeFieldProjection(internalPath, displayPath, graph, table, tables, names, fields, getFieldsDelegate))
      return;
    foreach (string fieldName in getFieldsDelegate(table))
    {
      string internalName;
      string displayName;
      TemplateGraphHelper.GetFieldNames(cach, table, fields, internalPath, displayPath, fieldName, out internalName, out displayName);
      if (internalName != null && !names.ContainsKey(internalName))
        names.Add(internalName, displayName);
    }
    if (tables.Count >= 2)
      return;
    tables.Add(table);
    foreach (KeyValuePair<string, System.Type> keyValuePair in PXSelectorAttribute.GetSelectorFields(table).Where<KeyValuePair<string, System.Type>>((Func<KeyValuePair<string, System.Type>, bool>) (s => fields == null || fields.ContainsKey(s.Key))))
    {
      System.Type tableType;
      if (TemplateGraphHelper.TryGetAssigneeType(tables, keyValuePair.Value, out tableType))
      {
        string internalName;
        string displayName;
        TemplateGraphHelper.GetFieldNames(cach, table, fields, internalPath, displayPath, keyValuePair.Key, out internalName, out displayName);
        TemplateGraphHelper.EnumAssigneeFields(internalName, displayName, graph, tableType, tables, names, (Dictionary<string, string>) null, getFieldsDelegate);
      }
    }
    tables.Remove(table);
  }

  public static void EnumAssigneeFormFields(
    string screenId,
    string formName,
    PXGraph graph,
    List<System.Type> tables,
    Dictionary<string, string> names,
    Dictionary<string, string> fields,
    Func<System.Type, IEnumerable<string>> getFieldsDelegate)
  {
    FormDefinition definition = AUWorkflowFormsEngine.Slot.GetDefinition(screenId, formName);
    if (definition == null)
      return;
    foreach (AUWorkflowFormField field in definition.Fields)
    {
      System.Type dacType;
      string name;
      FormFieldHelper.TryGetFieldFromFormFieldName(graph, field.SchemaField, out dacType, out name);
      TemplateGraphHelper.EnumAssigneeFormField(formName, formName, graph, dacType, tables, names, fields, getFieldsDelegate, field.FieldName, field.DisplayName, name);
    }
  }

  private static void EnumAssigneeFormField(
    string internalPath,
    string displayPath,
    PXGraph graph,
    System.Type table,
    List<System.Type> tables,
    Dictionary<string, string> names,
    Dictionary<string, string> fields,
    Func<System.Type, IEnumerable<string>> getFieldsDelegate,
    string formFieldName,
    string fieldDisplayName,
    string originalFieldName)
  {
    if (TemplateGraphHelper.EnumAssigneeFormFieldProjection(internalPath, displayPath, graph, table, tables, names, fields, getFieldsDelegate, formFieldName, fieldDisplayName, originalFieldName))
      return;
    if (getFieldsDelegate(table).Any<string>((Func<string, bool>) (fieldName => fieldName.OrdinalEquals(originalFieldName))))
    {
      string fieldInternalName = TemplateGraphHelper.GetFieldInternalName(fields, internalPath, formFieldName);
      string fieldDisplayName1 = TemplateGraphHelper.GetFormFieldDisplayName(displayPath, fieldDisplayName);
      if (fieldInternalName != null && !names.ContainsKey(fieldInternalName))
        names.Add(fieldInternalName, fieldDisplayName1);
    }
    tables.Add(table);
    foreach (KeyValuePair<string, System.Type> keyValuePair in PXSelectorAttribute.GetSelectorFields(table).Where<KeyValuePair<string, System.Type>>((Func<KeyValuePair<string, System.Type>, bool>) (s => (fields == null || fields.ContainsKey(s.Key)) && s.Key.OrdinalEquals(originalFieldName))))
    {
      System.Type tableType;
      if (TemplateGraphHelper.TryGetAssigneeType(tables, keyValuePair.Value, out tableType))
        TemplateGraphHelper.EnumAssigneeFields(TemplateGraphHelper.GetFieldInternalName(fields, internalPath, keyValuePair.Key), TemplateGraphHelper.GetFormFieldDisplayName(displayPath, fieldDisplayName), graph, tableType, tables, names, (Dictionary<string, string>) null, getFieldsDelegate);
    }
    tables.Remove(table);
  }

  private static bool EnumAssigneeFormFieldProjection(
    string internalPath,
    string displayPath,
    PXGraph graph,
    System.Type table,
    List<System.Type> tables,
    Dictionary<string, string> names,
    Dictionary<string, string> fields,
    Func<System.Type, IEnumerable<string>> getFieldsDelegate,
    string formFieldName,
    string fieldDisplayName,
    string originalFieldName)
  {
    PXProjectionAttribute projectionAttribute = table.GetCustomAttributes(true).OfType<PXProjectionAttribute>().FirstOrDefault<PXProjectionAttribute>();
    if (projectionAttribute == null)
      return false;
    foreach (System.Type table1 in projectionAttribute.GetTables())
    {
      if (table1 != table)
        TemplateGraphHelper.EnumAssigneeFormField(internalPath, displayPath, graph, table1, tables, names, fields, getFieldsDelegate, formFieldName, fieldDisplayName, originalFieldName);
    }
    return true;
  }

  private static bool EnumAssigneeFieldProjection(
    string internalPath,
    string displayPath,
    PXGraph graph,
    System.Type table,
    List<System.Type> tables,
    Dictionary<string, string> names,
    Dictionary<string, string> fields,
    Func<System.Type, IEnumerable<string>> getFieldsDelegate)
  {
    PXProjectionAttribute projectionAttribute = table.GetCustomAttributes(true).OfType<PXProjectionAttribute>().FirstOrDefault<PXProjectionAttribute>();
    if (projectionAttribute == null)
      return false;
    foreach (System.Type table1 in projectionAttribute.GetTables())
    {
      if (table1 != table)
        TemplateGraphHelper.EnumAssigneeFields(internalPath, displayPath, graph, table1, tables, names, fields, getFieldsDelegate);
    }
    return true;
  }

  private static bool TryGetAssigneeType(List<System.Type> tables, System.Type selectorTable, out System.Type tableType)
  {
    tableType = BqlCommand.GetItemType(selectorTable);
    if (tableType == (System.Type) null)
      return false;
    switch (tables.IndexOf(tableType))
    {
      case -1:
        return true;
      case 0:
        return tables.Count > 1;
      default:
        return false;
    }
  }

  private static void GetFieldNames(
    PXCache cache,
    System.Type table,
    Dictionary<string, string> fields,
    string internalPath,
    string displayPath,
    string fieldName,
    out string internalName,
    out string displayName)
  {
    internalName = TemplateGraphHelper.GetFieldInternalName(fields, internalPath, fieldName);
    displayName = $"{(string.IsNullOrEmpty(displayPath) ? table.Name : displayPath)}->{(cache.GetStateExt((object) null, fieldName) is PXFieldState stateExt ? stateExt.DisplayName : (string) null) ?? fieldName}";
  }

  private static string GetFieldInternalName(
    Dictionary<string, string> fields,
    string internalPath,
    string fieldName)
  {
    if (!string.IsNullOrEmpty(internalPath))
      return $"{internalPath}!{fieldName}";
    string str;
    return fields != null && fields.TryGetValue(fieldName, out str) ? str : (string) null;
  }

  private static string GetFormFieldDisplayName(string displayPath, string fieldDisplayName)
  {
    return $"{displayPath}->{fieldDisplayName}";
  }
}
