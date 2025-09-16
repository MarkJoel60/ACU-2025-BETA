// Decompiled with JetBrains decompiler
// Type: PX.Api.ImportSimple.FieldNameAndValueGenerator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Extensions;

#nullable disable
namespace PX.Api.ImportSimple;

public static class FieldNameAndValueGenerator
{
  public const string ACTION_FIELD_NAME_PREFIX = "<";
  public const string ACTION_FIELD_NAME_POSTFIX = ">";
  public const string COMMAND_PREFIX = "@@";
  public const string EXT_REFERENCE_POSTFIX = "NoteID";
  public const string KEY_COMMAND_VALUE_PREFIX = "=[";
  public const string KEY_COMMAND_VALUE_POSTFIX = "]";
  public const string KEY_COMMAND_VALUE_SEPARATOR = ".";
  public const string ATTRIBUTE_FIELD_VALUE_PREFIX = "='";
  public const string ATTRIBUTE_FIELD_VALUE_POSTFIX = "'";
  public const string NEW_LINE_FIELD_NAME = "##";
  public const string NEW_LINE_FIELD_VALUE = "=-1";

  public static string NewLineFieldName => "##";

  public static string NewLineFieldValue => "=-1";

  public static string GenerateObjectNameFromLinkedCommand(string commandObjectName)
  {
    return $"{"@@"}{commandObjectName}";
  }

  public static string GenerateExtRefValue() => $"{"@@"}{"NoteID"}";

  public static string GenerateKeyFieldName(string fieldName) => $"{"@@"}{fieldName}";

  public static string GenerateKeyValue(string objectName, string fieldName)
  {
    return $"{"=["}{StringExtensions.FirstSegment(objectName, ':')}{"."}{fieldName}{"]"}";
  }

  public static string GenerateFieldNameFromAction(string actionName) => $"{"<"}{actionName}{">"}";

  public static string GenerateAttributeFieldValue(string attributeKey)
  {
    return $"{"='"}{attributeKey}{"'"}";
  }
}
