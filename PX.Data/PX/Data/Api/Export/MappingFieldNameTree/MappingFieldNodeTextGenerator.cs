// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Export.MappingFieldNameTree.MappingFieldNodeTextGenerator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using System;

#nullable disable
namespace PX.Data.Api.Export.MappingFieldNameTree;

internal static class MappingFieldNodeTextGenerator
{
  internal const char ViewNameCommandNameSeparator = '.';
  internal const string ActionsNodeKey = "<ActionsNode>";
  internal const string AddAllFieldsNodeKey = "<Add All Fields>";
  internal const string ViewNameNodeKeyPrefix = "*";

  private static string GetNodeText(string name, string displayName) => $"{displayName} ({name})";

  internal static string ActionsNodeText => PXLocalizer.Localize("Actions");

  internal static string AddAllFieldsNodeText => PXLocalizer.Localize("<Add All Fields>");

  internal static string SetBranchNodeText => PXLocalizer.Localize("<Set: Branch>");

  internal static string GetSetBranchNodeKey(string viewName)
  {
    return $"{viewName}{(ValueType) '.'}{"<Set: Branch>"}";
  }

  internal static string GetViewNodeKey(string viewName) => "*" + viewName;

  internal static string GetViewNodeText(string viewName, string viewDisplayName)
  {
    return MappingFieldNodeTextGenerator.GetNodeText(viewName, viewDisplayName);
  }

  internal static string GetActionNodeKey(string viewName, string actionName)
  {
    return $"{viewName}{(ValueType) '.'}{MappingFieldNamer.CreateActionValue(actionName)}";
  }

  internal static string GetActionNodeText(string actionName, string actionDisplayName)
  {
    string actionLabel = MappingFieldNamer.CreateActionLabel(actionDisplayName);
    return MappingFieldNodeTextGenerator.GetNodeText(actionName, actionLabel);
  }

  internal static string GetFieldNodeKey(string viewName, string fieldName)
  {
    return $"{viewName}{(ValueType) '.'}{fieldName}";
  }

  internal static string GetFieldNodeText(string fieldKey, string fieldDisplayName)
  {
    return MappingFieldNodeTextGenerator.GetNodeText(fieldKey, fieldDisplayName);
  }

  internal static string GetParameterNodeKey(string viewName, string parameterName)
  {
    return $"{viewName}{(ValueType) '.'}{MappingFieldNamer.CreateParameterValue(parameterName)}";
  }

  internal static string GetParameterNodeText(string viewName, string parameterName)
  {
    return $"{MappingFieldNamer.CreateParameterLabel(parameterName)} ({viewName}.{parameterName})";
  }

  internal static string GetSearchNodeKey(string viewName, string searchField, string searchName)
  {
    return $"{viewName}{(ValueType) '.'}{MappingFieldNamer.CreateKeyValue(searchField, searchName)}";
  }

  internal static string GetSearchNodeText(string viewName, string searchName)
  {
    return $"{MappingFieldNamer.CreateKeyLabel(searchName)} ({viewName}.{searchName})";
  }

  internal static string GetExternalKeyNodeKey(string viewName)
  {
    return $"{viewName}{(ValueType) '.'}{MappingFieldNamer.CreateExternalKeyValue()}";
  }

  internal static string GetExternalKeyNodeText(string viewName)
  {
    return $"{MappingFieldNamer.CreateExternalKeyLabel()} ({viewName}.NoteID)";
  }

  internal static string GetLineNumberNodeKey(string viewName)
  {
    return $"{viewName}{(ValueType) '.'}{MappingFieldNamer.CreateLineNumberValue()}";
  }

  internal static string GetLineNumberNodeText() => MappingFieldNamer.CreateLineNumberLabel();

  internal static string GetDialogAnswerNodeKey(string viewName)
  {
    return $"{viewName}{(ValueType) '.'}{MappingFieldNamer.CreateDialogAnswerValue()}";
  }

  internal static string GetDialogAnswerNodeText() => MappingFieldNamer.CreateDialogAnswerLabel();

  internal static string GetSelectorFieldNodeKey(
    string viewName,
    string fieldName,
    string selectorFieldName)
  {
    return $"{viewName}{(ValueType) '.'}{fieldName}!{selectorFieldName}";
  }

  internal static string GetSelectorFieldNodeText(
    string fieldDisplayName,
    string selectorFieldKey,
    string selectorFieldDisplayName)
  {
    return $"{fieldDisplayName} -> {MappingFieldNodeTextGenerator.GetNodeText(selectorFieldKey, selectorFieldDisplayName)}";
  }
}
