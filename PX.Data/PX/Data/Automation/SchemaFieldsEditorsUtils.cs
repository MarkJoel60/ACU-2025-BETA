// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.SchemaFieldsEditorsUtils
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Automation;

internal static class SchemaFieldsEditorsUtils
{
  public static bool TryParseSchemaFieldEditor(
    this string fieldName,
    out SchemaFieldEditors? schemaFieldEditors)
  {
    if (!string.IsNullOrEmpty(fieldName))
    {
      SchemaFieldEditors result;
      if (Enum.TryParse<SchemaFieldEditors>(fieldName.Trim('[', ']'), out result))
      {
        schemaFieldEditors = new SchemaFieldEditors?(result);
        return true;
      }
    }
    schemaFieldEditors = new SchemaFieldEditors?();
    return false;
  }
}
