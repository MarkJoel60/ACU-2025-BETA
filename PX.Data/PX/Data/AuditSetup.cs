// Decompiled with JetBrains decompiler
// Type: PX.Data.AuditSetup
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.DbServices.Model.Entities;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public class AuditSetup : IPrefetchable, IPXCompanyDependent
{
  public List<string> _Screens = new List<string>();
  private Dictionary<string, Dictionary<string, AuditSetup.AuTableOptions>> _Schema = new Dictionary<string, Dictionary<string, AuditSetup.AuTableOptions>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

  public bool AuditAvailable(string screenID) => this._Screens.Contains(screenID);

  public bool AuditRequired(string tableName) => this._Schema.ContainsKey(tableName);

  public string[] GetKeyNames(string tableName, string screenId)
  {
    Dictionary<string, AuditSetup.AuTableOptions> source;
    if (this._Schema.TryGetValue(tableName, out source))
    {
      AuditSetup.AuTableOptions auTableOptions;
      if (!string.IsNullOrWhiteSpace(screenId) && source.TryGetValue(screenId, out auTableOptions))
        return auTableOptions.Key.ToArray<string>();
      if (source.Any<KeyValuePair<string, AuditSetup.AuTableOptions>>())
        return source.First<KeyValuePair<string, AuditSetup.AuTableOptions>>().Value.Key.ToArray<string>();
    }
    return new string[0];
  }

  public HashSet<string> GetFieldNames(string tableName, string screenId)
  {
    Dictionary<string, AuditSetup.AuTableOptions> source;
    if (this._Schema.TryGetValue(tableName, out source))
    {
      AuditSetup.AuTableOptions auTableOptions;
      if (!string.IsNullOrWhiteSpace(screenId) && source.TryGetValue(screenId, out auTableOptions))
        return auTableOptions.Value;
      if (source.Any<KeyValuePair<string, AuditSetup.AuTableOptions>>())
        return source.First<KeyValuePair<string, AuditSetup.AuTableOptions>>().Value.Value;
    }
    return new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  }

  void IPrefetchable.Prefetch()
  {
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<AUAuditSetup>((PXDataField) new PXDataField<AUAuditSetup.isActive>(), (PXDataField) new PXDataField<AUAuditSetup.screenID>()))
    {
      bool valueOrDefault = pxDataRecord.GetBoolean(0).GetValueOrDefault();
      string str = pxDataRecord.GetString(1);
      if (valueOrDefault && !string.IsNullOrEmpty(str) && !this._Screens.Contains(str))
        this._Screens.Add(str);
    }
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<AUAuditTable>((PXDataField) new PXDataField<AUAuditTable.isActive>(), (PXDataField) new PXDataField<AUAuditTable.screenID>(), (PXDataField) new PXDataField<AUAuditTable.tableName>(), (PXDataField) new PXDataField<AUAuditTable.keys>()))
    {
      bool valueOrDefault = pxDataRecord.GetBoolean(0).GetValueOrDefault();
      string str1 = pxDataRecord.GetString(1);
      string str2 = pxDataRecord.GetString(2);
      string str3 = pxDataRecord.GetString(3);
      if (valueOrDefault && !string.IsNullOrEmpty(str2) && this._Screens.Contains(str1))
      {
        Dictionary<string, AuditSetup.AuTableOptions> dictionary;
        if (!this._Schema.TryGetValue(str2, out dictionary))
          this._Schema[str2] = dictionary = new Dictionary<string, AuditSetup.AuTableOptions>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        AuditSetup.AuTableOptions auTableOptions;
        if (!dictionary.TryGetValue(str1, out auTableOptions))
          dictionary[str1] = auTableOptions = new AuditSetup.AuTableOptions();
        if (!string.IsNullOrEmpty(str3) && auTableOptions.Key.Count <= 0)
        {
          string str4 = str3;
          char[] separator = new char[1]
          {
            PXDatabase.Provider.SqlDialect.WildcardFieldSeparatorChar
          };
          foreach (string str5 in str4.Split(separator, StringSplitOptions.RemoveEmptyEntries))
          {
            if (!auTableOptions.Key.Contains(str5))
              auTableOptions.Key.Add(str5);
          }
          this.AddExtensionTables(str2, str1);
        }
      }
    }
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<AUAuditField>((PXDataField) new PXDataField<AUAuditField.tableName>(), (PXDataField) new PXDataField<AUAuditField.fieldName>(), (PXDataField) new PXDataField<AUAuditField.screenID>()))
    {
      string key1 = pxDataRecord.GetString(0);
      string str = pxDataRecord.GetString(1);
      string key2 = pxDataRecord.GetString(2);
      Dictionary<string, AuditSetup.AuTableOptions> dictionary;
      AuditSetup.AuTableOptions auTableOptions;
      if (this._Schema.TryGetValue(key1, out dictionary) && dictionary.TryGetValue(key2, out auTableOptions) && str != null && !auTableOptions.Value.Contains(str))
        auTableOptions.Value.Add(str);
    }
    foreach (KeyValuePair<string, Dictionary<string, AuditSetup.AuTableOptions>> keyValuePair1 in this._Schema.ToArray<KeyValuePair<string, Dictionary<string, AuditSetup.AuTableOptions>>>())
    {
      foreach (KeyValuePair<string, AuditSetup.AuTableOptions> keyValuePair2 in keyValuePair1.Value.ToArray<KeyValuePair<string, AuditSetup.AuTableOptions>>())
      {
        try
        {
          AuditSetup.AuTableOptions auTableOptions = keyValuePair2.Value;
          bool flag1 = auTableOptions.Key.Count <= 0;
          HashSet<string> stringSet1 = new HashSet<string>((IEnumerable<string>) auTableOptions.Value, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
          auTableOptions.Value.Clear();
          TableHeader tableStructure = PXDatabase.Provider.GetTableStructure(keyValuePair1.Key);
          HashSet<string> stringSet2 = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
          foreach (TableIndex tableIndex in tableStructure.Indices.Where<TableIndex>((Func<TableIndex, bool>) (x => x.IsUnique)))
          {
            foreach (TableIndexOnColumn column in tableIndex.Columns)
              stringSet2.Add(((TableEntityBase) column).Name);
          }
          foreach (TableColumn column in tableStructure.Columns)
          {
            bool flag2 = stringSet2.Contains(((TableEntityBase) column).Name);
            if (flag1 & flag2 && !string.Equals(((TableEntityBase) column).Name, "CompanyID", StringComparison.OrdinalIgnoreCase))
              auTableOptions.Key.Add(((TableEntityBase) column).Name);
            if (!flag2 && !stringSet1.Contains(((TableEntityBase) column).Name) && !string.Equals(((TableEntityBase) column).Name, "TStamp", StringComparison.OrdinalIgnoreCase) && !string.Equals(((TableEntityBase) column).Name, "GroupMask", StringComparison.OrdinalIgnoreCase) && !string.Equals(((TableEntityBase) column).Name, "NoteID", StringComparison.OrdinalIgnoreCase) && !string.Equals(((TableEntityBase) column).Name, "CreatedByID", StringComparison.OrdinalIgnoreCase) && !string.Equals(((TableEntityBase) column).Name, "CreatedByScreenID", StringComparison.OrdinalIgnoreCase) && !string.Equals(((TableEntityBase) column).Name, "CreatedDateTime", StringComparison.OrdinalIgnoreCase) && !string.Equals(((TableEntityBase) column).Name, "LastModifiedByID", StringComparison.OrdinalIgnoreCase) && !string.Equals(((TableEntityBase) column).Name, "LastModifiedByScreenID", StringComparison.OrdinalIgnoreCase) && !string.Equals(((TableEntityBase) column).Name, "LastModifiedDateTime", StringComparison.OrdinalIgnoreCase))
              auTableOptions.Value.Add(((TableEntityBase) column).Name);
          }
        }
        catch
        {
          keyValuePair1.Value.Remove(keyValuePair2.Key);
        }
      }
      if (!keyValuePair1.Value.Any<KeyValuePair<string, AuditSetup.AuTableOptions>>())
        this._Schema.Remove(keyValuePair1.Key);
    }
  }

  private void AddExtensionTables(string tableName, string screenId)
  {
    if (string.IsNullOrEmpty(tableName))
      return;
    List<KeyValuePair<string, List<string>>> extensionTables = PXCache.GetExtensionTables(tableName);
    if (extensionTables == null)
      return;
    foreach (KeyValuePair<string, List<string>> keyValuePair in extensionTables)
    {
      AuditSetup.AuTableOptions auTableOptions = new AuditSetup.AuTableOptions(keyValuePair.Value != null ? new HashSet<string>((IEnumerable<string>) keyValuePair.Value, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase) : this._Schema[tableName][screenId].Key);
      Dictionary<string, AuditSetup.AuTableOptions> dictionary;
      if (!this._Schema.TryGetValue(keyValuePair.Key, out dictionary))
        this._Schema[keyValuePair.Key] = dictionary = new Dictionary<string, AuditSetup.AuTableOptions>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      dictionary[screenId] = auTableOptions;
    }
  }

  /// <exclude />
  public class AuTableOptions
  {
    public readonly HashSet<string> Key;
    public readonly HashSet<string> Value = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

    public AuTableOptions(HashSet<string> keySet = null)
    {
      this.Key = keySet ?? new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    }
  }
}
