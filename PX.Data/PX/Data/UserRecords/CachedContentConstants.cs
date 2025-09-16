// Decompiled with JetBrains decompiler
// Type: PX.Data.UserRecords.CachedContentConstants
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.UserRecords;

/// <summary>
/// A collection of constants used as tags and attributes names for user records cached XML content.
/// </summary>
[PXInternalUseOnly]
public static class CachedContentConstants
{
  public const string UserRecordXmlElement = "record";
  public const string EntityNameXmlAttribute = "Name";
  public const string EntityDescriptionXmlAttribute = "Description";
  public const string SearchableFieldXmlElement = "pair";
  public const string SearchableFieldNameXmlAttribute = "Name";
  public const string SearchableFieldValueXmlAttribute = "Value";
}
