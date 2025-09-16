// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.GIConstants
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.GenericInquiry;

/// <summary>Common constants used across different GI modules</summary>
internal class GIConstants
{
  private const char BqlSafeFieldNameDelimiter = '_';
  /// <summary>Delimiter between a table name and a field name</summary>
  internal const char TableAndFieldNameDelimiter = '_';
  /// <summary>
  /// When parameter from sub GI is used, parameter name and GI alias are delimited with this value
  /// </summary>
  internal const char ParameterNameAndAliasDelimiter = '_';
}
