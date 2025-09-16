// Decompiled with JetBrains decompiler
// Type: PX.SM.TablesAndFieldsInfoExtractor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

public static class TablesAndFieldsInfoExtractor
{
  public static readonly string[] NotAuditableFields = new string[9]
  {
    "TStamp",
    "NoteID",
    "GroupMask",
    "CreatedByID",
    "CreatedByScreenID",
    "CreatedDateTime",
    "LastModifiedByID",
    "LastModifiedByScreenID",
    "LastModifiedDateTime"
  };

  public static string GetTableName(
    Dictionary<string, (System.Type, string)> tablesInView,
    string tableTypeStr)
  {
    (System.Type, string) tuple;
    return tablesInView.TryGetValue(tableTypeStr, out tuple) ? tuple.Item2 : (string) null;
  }

  public static string GetTableDisplayName(System.Type tableType, string tableName)
  {
    string tableDisplayName = (string) null;
    if (tableType != (System.Type) null && !string.IsNullOrEmpty(tableName))
    {
      object[] customAttributes = tableType.GetCustomAttributes(typeof (PXCacheNameAttribute), false);
      tableDisplayName = customAttributes.Length != 0 ? ((PXNameAttribute) customAttributes[0]).Name : tableName;
    }
    return tableDisplayName;
  }

  public static string GetTableKeys(PXCache tableCache)
  {
    string str = tableCache.Keys.Aggregate<string, string>(string.Empty, (Func<string, string, string>) ((current, key) => current + key + PXDatabase.Provider.SqlDialect.WildcardFieldSeparatorChar.ToString()));
    return !(str == string.Empty) ? str.Remove(str.Length - 1) : (string) null;
  }

  public static Dictionary<string, (System.Type, string)> GetTablesFromView(System.Type viewType)
  {
    Dictionary<string, (System.Type, string)> tablesFromView = new Dictionary<string, (System.Type, string)>();
    for (System.Type c = viewType; c != typeof (object); c = c.BaseType)
    {
      if ((c.BaseType == typeof (object) || !typeof (IBqlTable).IsAssignableFrom(c.BaseType)) && typeof (IBqlTable).IsAssignableFrom(c) || c.IsDefined(typeof (PXTableAttribute), false))
        tablesFromView.Add(c.ToString(), (c, c.Name));
    }
    return tablesFromView;
  }

  public static int GetFieldType(PXCache tableCache, string fieldName)
  {
    int fieldType = 0;
    if (tableCache.GetAttributesReadonly(fieldName).Where<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (item => item is PXUIFieldAttribute)).FirstOrDefault<PXEventSubscriberAttribute>() != null)
      fieldType = 1;
    return fieldType;
  }

  public static string GetTableName(PXCache cache)
  {
    string tableName = (string) null;
    if (cache != null)
    {
      System.Type type = cache.GetItemType();
      while (type.BaseType.IsBqlTable())
        type = type.BaseType;
      tableName = type.Name;
    }
    return tableName;
  }
}
