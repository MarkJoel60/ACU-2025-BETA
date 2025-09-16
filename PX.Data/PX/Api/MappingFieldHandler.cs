// Decompiled with JetBrains decompiler
// Type: PX.Api.MappingFieldHandler
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Description;
using PX.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Api;

internal static class MappingFieldHandler
{
  private const string ACTION_SAVE = "<Save>";
  private const string ACTION_CANCEL = "<Cancel>";
  private const string ACTION_INSERT = "<Insert>";
  private const string ACTION_DELETE = "<Delete>";
  internal const string ATTRIBUTE_OBJECT_NAME = "Attributes";
  internal const string ATTRIBUTE_ALTERNATIVE_OBJECT_NAME = "Answers";
  private const string ATTRIBUTE_FIELD_NAME = "Value";
  private const string SEARCH_PREFIX = "@@";
  private static readonly MappingFieldType[] _autoCommitfieldTypesImport = new MappingFieldType[8]
  {
    MappingFieldType.Answer,
    MappingFieldType.Every,
    MappingFieldType.Insert,
    MappingFieldType.Panel,
    MappingFieldType.AfterSearch,
    MappingFieldType.AttributeValue,
    MappingFieldType.Action,
    MappingFieldType.Selector
  };
  private static readonly MappingFieldType[] _autoCommitfieldTypesExport = new MappingFieldType[2]
  {
    MappingFieldType.Answer,
    MappingFieldType.Every
  };

  public static void AutoSetCommit(
    PXGraph graph,
    PXCache cache,
    SYMappingField field,
    string screenId,
    MappingFieldHandler.Mode mode)
  {
    if (graph == null || field == null || string.IsNullOrEmpty(field.FieldName) || string.IsNullOrEmpty(screenId))
      return;
    bool? needCommit = field.NeedCommit;
    bool flag = true;
    if (needCommit.GetValueOrDefault() == flag & needCommit.HasValue)
      return;
    if (mode != MappingFieldHandler.Mode.Import)
    {
      if (mode != MappingFieldHandler.Mode.Export)
        return;
      MappingFieldType fieldType = MappingFieldHandler.GetFieldType(graph, field, SyExportContext.ParseCommand(field), screenId);
      MappingFieldHandler.HandleExport(field, fieldType);
    }
    else
    {
      MappingFieldType fieldType = MappingFieldHandler.GetFieldType(graph, field, SyImportContext.ParseCommand(field), screenId);
      MappingFieldHandler.HandleImport(graph, cache, field, fieldType);
    }
  }

  private static void HandleImport(
    PXGraph graph,
    PXCache cache,
    SYMappingField field,
    MappingFieldType fieldType)
  {
    if (fieldType == MappingFieldType.Search)
    {
      string searchFieldName = MappingFieldHandler.GetSearchFieldName(field.FieldName);
      SYMappingField syMappingField = (SYMappingField) PXSelectBase<SYMappingField, PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Required<SYMappingField.mappingID>>, And<SYMappingField.objectName, Equal<Required<SYMappingField.objectName>>, And<SYMappingField.fieldName, Equal<Required<SYMappingField.fieldName>>, And<SYMappingField.lineNbr, Greater<Required<SYMappingField.lineNbr>>>>>>>.Config>.Select(graph, (object) field.MappingID, (object) field.ObjectName, (object) searchFieldName, (object) field.LineNbr);
      if (syMappingField != null)
      {
        bool? needCommit = syMappingField.NeedCommit;
        bool flag = true;
        if (!(needCommit.GetValueOrDefault() == flag & needCommit.HasValue))
        {
          syMappingField.NeedCommit = new bool?(true);
          cache.Update((object) syMappingField);
        }
      }
    }
    if (!((IEnumerable<MappingFieldType>) MappingFieldHandler._autoCommitfieldTypesImport).Contains<MappingFieldType>(fieldType))
      return;
    field.NeedCommit = new bool?(true);
  }

  private static void HandleExport(SYMappingField field, MappingFieldType fieldType)
  {
    if (!((IEnumerable<MappingFieldType>) MappingFieldHandler._autoCommitfieldTypesExport).Contains<MappingFieldType>(fieldType))
      return;
    field.NeedCommit = new bool?(true);
  }

  private static string GetSearchFieldName(string search) => search.Substring("@@".Length);

  private static MappingFieldType GetFieldType(
    PXGraph graph,
    SYMappingField field,
    SyCommand command,
    string screenId)
  {
    MappingFieldType fieldType = MappingFieldHandler.MapCommandTypeToFieldType(command);
    switch (fieldType)
    {
      case MappingFieldType.Action:
        return MappingFieldHandler.GetActionFieldType(field.FieldName);
      case MappingFieldType.Field:
        return MappingFieldHandler.GetOrdinaryFieldType(graph, field, screenId);
      default:
        return fieldType;
    }
  }

  private static MappingFieldType GetOrdinaryFieldType(
    PXGraph graph,
    SYMappingField field,
    string screenId)
  {
    if ((string.Equals(field.ObjectName, "Attributes", StringComparison.OrdinalIgnoreCase) || string.Equals(field.ObjectName, "Answers", StringComparison.OrdinalIgnoreCase)) && string.Equals(field.FieldName, "Value", StringComparison.OrdinalIgnoreCase))
      return MappingFieldType.AttributeValue;
    if (MappingFieldHandler.IsSelector(field.ObjectName, field.FieldName, screenId))
      return MappingFieldType.Selector;
    if (MappingFieldHandler.IsSmartPanel(field.ObjectName, screenId))
      return MappingFieldType.Panel;
    return MappingFieldHandler.IsAfterSearch(graph, field) ? MappingFieldType.AfterSearch : MappingFieldType.Field;
  }

  private static bool IsAfterSearch(PXGraph graph, SYMappingField field)
  {
    string search = MappingFieldHandler.GenerateSearch(field.FieldName);
    return (SYMappingField) PXSelectBase<SYMappingField, PXSelect<SYMappingField, Where<SYMappingField.mappingID, Equal<Required<SYMappingField.mappingID>>, And<SYMappingField.objectName, Equal<Required<SYMappingField.objectName>>, And<SYMappingField.fieldName, Equal<Required<SYMappingField.fieldName>>, And<SYMappingField.lineNbr, Less<Required<SYMappingField.lineNbr>>>>>>>.Config>.Select(graph, (object) field.MappingID, (object) field.ObjectName, (object) search, (object) field.LineNbr) != null;
  }

  private static string GenerateSearch(string fieldName) => "@@" + fieldName;

  private static MappingFieldType GetActionFieldType(string fieldName)
  {
    if (fieldName.Equals("<Save>", StringComparison.OrdinalIgnoreCase))
      return MappingFieldType.Save;
    if (fieldName.Equals("<Cancel>", StringComparison.OrdinalIgnoreCase))
      return MappingFieldType.Cancel;
    if (fieldName.Equals("<Insert>", StringComparison.OrdinalIgnoreCase))
      return MappingFieldType.Insert;
    return fieldName.Equals("<Delete>", StringComparison.OrdinalIgnoreCase) ? MappingFieldType.Delete : MappingFieldType.Action;
  }

  private static MappingFieldType MapCommandTypeToFieldType(SyCommand command)
  {
    switch (command.CommandType)
    {
      case SyCommandType.Parameter:
        return MappingFieldType.Parameter;
      case SyCommandType.Search:
        return MappingFieldType.Search;
      case SyCommandType.Action:
        return MappingFieldType.Action;
      case SyCommandType.NewRow:
        return MappingFieldType.NewRow;
      case SyCommandType.DeleteRow:
        return MappingFieldType.DeleteRow;
      case SyCommandType.RowNumber:
        return MappingFieldType.RowNumber;
      case SyCommandType.RowCount:
        return MappingFieldType.RowCount;
      case SyCommandType.Path:
        return MappingFieldType.Path;
      case SyCommandType.Answer:
        return MappingFieldType.Answer;
      case SyCommandType.EnumFieldValues:
        return MappingFieldType.Every;
      case SyCommandType.ExportField:
        return MappingFieldType.ExportField;
      case SyCommandType.ExportPath:
        return MappingFieldType.ExportPath;
      case SyCommandType.CustomDelegate:
        return MappingFieldType.CustomDelegate;
      default:
        return MappingFieldType.Field;
    }
  }

  private static bool IsSelector(string objectName, string fieldName, string screenId)
  {
    PXViewDescription viewDescription = MappingFieldHandler.GetViewDescription(objectName, screenId);
    PX.Data.Description.FieldInfo fieldInfo = (PX.Data.Description.FieldInfo) null;
    if (viewDescription != null && viewDescription.Fields != null)
      fieldInfo = ((IEnumerable<PX.Data.Description.FieldInfo>) viewDescription.Fields).FirstOrDefault<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (f => string.Equals(f.FieldName, fieldName, StringComparison.Ordinal)));
    return fieldInfo != null && fieldInfo.IsSelector;
  }

  private static bool IsSmartPanel(string objectName, string screenId)
  {
    PXViewDescription viewDescription = MappingFieldHandler.GetViewDescription(objectName, screenId);
    return viewDescription != null && viewDescription.IsInSmartPanel;
  }

  private static PXViewDescription GetViewDescription(string containerName, string screenId)
  {
    PXViewDescription viewDescription = (PXViewDescription) null;
    PXSiteMap.ScreenInfo screenInfo = ScreenUtils.ScreenInfo.TryGet(screenId);
    if (screenInfo != null && screenInfo.Containers != null && screenInfo.Containers.ContainsKey(containerName))
      viewDescription = screenInfo.Containers[containerName];
    return viewDescription;
  }

  public enum Mode
  {
    Import,
    Export,
  }
}
