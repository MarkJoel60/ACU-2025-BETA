// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.SnapshotConfigReader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Soap.Screen;
using PX.BulkInsert;
using PX.DbServices.Model;
using PX.DbServices.Model.Entities;
using PX.DbServices.Model.Schema;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.QueryObjectModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

#nullable disable
namespace PX.Data.Update;

internal class SnapshotConfigReader
{
  private const string ConfigFolder = "SnapshotConfigs";
  private const string ConfigFileExtension = ".esc";
  private const string ConfigRootElement = "Tables";
  private const string ConfigTableFillAttribute = "fill";
  private const string ConfigTableNameAttribute = "table";
  private const string ConfigIncludeTableElement = "Include";
  private const string ConfigExcludeTableElement = "Exclude";
  private const string ConfigPreserveAttribute = "preserve";
  private static readonly string[] _obsoleteAttributes = new string[3]
  {
    "screen",
    "module",
    "folder"
  };
  private readonly SnapshotConfigLogger _logger = new SnapshotConfigLogger();
  private readonly SnapshotConfigRuleManager _ruleManager = new SnapshotConfigRuleManager();

  private static string ConfigRootFolder
  {
    get => Path.Combine(PXInstanceHelper.AppDataFolder, "SnapshotConfigs");
  }

  internal static string[] GetExportModes(bool localized = false)
  {
    DirectoryInfo directoryInfo = new DirectoryInfo(SnapshotConfigReader.ConfigRootFolder);
    if (!directoryInfo.Exists)
      return Array.Empty<string>();
    IEnumerable<string> source = ((IEnumerable<FileInfo>) directoryInfo.GetFiles("*.esc")).Select<FileInfo, string>((Func<FileInfo, string>) (f => Path.GetFileNameWithoutExtension(f.Name)));
    return !localized ? source.ToArray<string>() : source.Select<string, string>((Func<string, string>) (m => PXMessages.LocalizeNoPrefix(m))).ToArray<string>();
  }

  public Dictionary<string, TableForSnapshot> ReadTableRules(
    string exportMode,
    PointDbmsBase point,
    bool forceMasked,
    int company,
    bool includeCompany)
  {
    string baseConfigFilePath = this.FindBaseConfigFilePath(exportMode);
    bool fillTablesMode;
    Dictionary<string, TableForSnapshot> baseConfigFileRules = this.ReadBaseTableRules(baseConfigFilePath, point, forceMasked, company, includeCompany, out fillTablesMode);
    if (string.IsNullOrEmpty(baseConfigFilePath))
      return baseConfigFileRules;
    IEnumerable<string> adjustmentsFilePaths = this.FindAdjustmentsFilePaths(baseConfigFilePath);
    Dictionary<string, Dictionary<string, TableForSnapshot>> adjustmentsRulesByFile = new Dictionary<string, Dictionary<string, TableForSnapshot>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (string str in adjustmentsFilePaths)
    {
      Dictionary<string, TableForSnapshot> dictionary = this.ReadAdjustmentsTableRules(str, fillTablesMode, point.Schema);
      adjustmentsRulesByFile.Add(str, dictionary);
    }
    if (!this.CheckAdjustmentFileRulesCompatibility(fillTablesMode, adjustmentsRulesByFile))
      throw new PXException("The snapshot configuration files contain incompatible elements. For details, see the trace log: Click Tools > Trace on the form title bar.");
    Dictionary<string, TableForSnapshot> rules = this.ExcludeKvExtTables(this.MergeRules(fillTablesMode, baseConfigFileRules, (IEnumerable<Dictionary<string, TableForSnapshot>>) adjustmentsRulesByFile.Values), point, fillTablesMode);
    this._logger.LogMergedRules(rules);
    return rules;
  }

  private Dictionary<string, TableForSnapshot> ExcludeKvExtTables(
    Dictionary<string, TableForSnapshot> rules,
    PointDbmsBase point,
    bool fillTablesMode)
  {
    string str1;
    HashSet<string> excludeRulesForBaseTables = rules.Where<KeyValuePair<string, TableForSnapshot>>((Func<KeyValuePair<string, TableForSnapshot>, bool>) (x => x.Value.ExcludeTable && !AcumaticaDb.IsKvExtTable(x.Key, ref str1) && point.Schema.TableExists(AcumaticaDb.GetKvExtTableName(x.Key)))).Select<KeyValuePair<string, TableForSnapshot>, string>((Func<KeyValuePair<string, TableForSnapshot>, string>) (x => x.Key)).ToHashSet<string>();
    string str2;
    HashSet<string> hashSet = rules.Keys.Where<string>((Func<string, bool>) (x => AcumaticaDb.IsKvExtTable(x, ref str2) && excludeRulesForBaseTables.Contains(str2))).ToHashSet<string>();
    Dictionary<string, TableForSnapshot> dictionary1 = new Dictionary<string, TableForSnapshot>();
    Dictionary<string, (TableForSnapshot, TableForSnapshot)> dictionary2 = new Dictionary<string, (TableForSnapshot, TableForSnapshot)>();
    foreach (KeyValuePair<string, TableForSnapshot> rule1 in rules)
    {
      string key1 = rule1.Key;
      TableForSnapshot tableForSnapshot = rule1.Value;
      if (!excludeRulesForBaseTables.Contains(key1) && !hashSet.Contains(key1))
      {
        string str3;
        if (!fillTablesMode && AcumaticaDb.IsKvExtTable(key1, ref str3) && tableForSnapshot.GetRuleType() != 4)
        {
          TableForSnapshot excluded = TableForSnapshot.Excluded;
          dictionary2.Add(key1, (tableForSnapshot, excluded));
          tableForSnapshot = excluded;
        }
        dictionary1.Add(key1, tableForSnapshot);
      }
      else
      {
        string key2;
        if (!AcumaticaDb.IsKvExtTable(key1, ref key2))
        {
          dictionary1.Add(key1, tableForSnapshot);
          dictionary1.Add(AcumaticaDb.GetKvExtTableName(key1), tableForSnapshot.Preserve ? TableForSnapshot.ExcludePreserve : TableForSnapshot.Excluded);
        }
        else
        {
          TableForSnapshot rule2 = rules[key2];
          if (!tableForSnapshot.AutomaticallyFilled && (!tableForSnapshot.ExcludeTable || tableForSnapshot.Preserve != rule2.Preserve))
            dictionary2.Add(key1, (tableForSnapshot, rule2.Preserve ? TableForSnapshot.ExcludePreserve : TableForSnapshot.Excluded));
        }
      }
    }
    if (dictionary2.Any<KeyValuePair<string, (TableForSnapshot, TableForSnapshot)>>())
      this._logger.LogConflictKvExtRules(dictionary2);
    return dictionary1;
  }

  private Dictionary<string, TableForSnapshot> MergeAdjustmentFiles(
    bool fillTablesMode,
    IEnumerable<Dictionary<string, TableForSnapshot>> adjustmentsRules)
  {
    Dictionary<string, TableForSnapshot> dict = new Dictionary<string, TableForSnapshot>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (Dictionary<string, TableForSnapshot> adjustmentsRule in adjustmentsRules)
    {
      if (dict.Count == 0)
      {
        dict.AddRange<string, TableForSnapshot>(adjustmentsRule);
      }
      else
      {
        foreach (string key in adjustmentsRule.Keys)
        {
          TableForSnapshot secondRule = adjustmentsRule[key];
          TableForSnapshot firstRule;
          if (!dict.TryGetValue(key, out firstRule))
            dict.Add(key, secondRule);
          else
            dict[key] = this._ruleManager.MergeAdjustmentRules(fillTablesMode, firstRule, secondRule);
        }
      }
    }
    return dict;
  }

  private Dictionary<string, TableForSnapshot> MergeRules(
    bool fillTablesMode,
    Dictionary<string, TableForSnapshot> baseConfigFileRules,
    IEnumerable<Dictionary<string, TableForSnapshot>> adjustmentsRules)
  {
    Dictionary<string, TableForSnapshot> dictionary = this.MergeAdjustmentFiles(fillTablesMode, adjustmentsRules);
    foreach (string key in baseConfigFileRules.Keys)
    {
      TableForSnapshot baseConfigFileRule = baseConfigFileRules[key];
      TableForSnapshot adjustmentRule;
      if (!dictionary.TryGetValue(key, out adjustmentRule))
        dictionary.Add(key, baseConfigFileRule);
      else
        dictionary[key] = this._ruleManager.MergeBaseRuleWithAdjustmentRule(baseConfigFileRule, adjustmentRule);
    }
    return dictionary;
  }

  private bool CheckAdjustmentFileRulesCompatibility(
    bool fillTablesMode,
    Dictionary<string, Dictionary<string, TableForSnapshot>> adjustmentsRulesByFile)
  {
    bool flag = true;
    foreach (KeyValuePair<string, Dictionary<string, TableForSnapshot>> keyValuePair1 in adjustmentsRulesByFile)
    {
      KeyValuePair<string, Dictionary<string, TableForSnapshot>> firstFile = keyValuePair1;
      foreach (KeyValuePair<string, Dictionary<string, TableForSnapshot>> keyValuePair2 in adjustmentsRulesByFile.Where<KeyValuePair<string, Dictionary<string, TableForSnapshot>>>((Func<KeyValuePair<string, Dictionary<string, TableForSnapshot>>, bool>) (other => !other.Key.Equals(firstFile.Key, StringComparison.OrdinalIgnoreCase))))
        flag &= this.CheckFileRulesCompatibility(fillTablesMode, firstFile.Key, firstFile.Value, keyValuePair2.Key, keyValuePair2.Value);
    }
    return flag;
  }

  private bool CheckFileRulesCompatibility(
    bool fillTablesMode,
    string firstFilePath,
    Dictionary<string, TableForSnapshot> firstFileRules,
    string secondFilePath,
    Dictionary<string, TableForSnapshot> secondFileRules)
  {
    bool flag1 = true;
    foreach (string key in firstFileRules.Keys)
    {
      TableForSnapshot secondRule;
      if (secondFileRules.TryGetValue(key, out secondRule))
      {
        TableForSnapshot firstFileRule = firstFileRules[key];
        bool flag2 = this._ruleManager.RulesCompatible(fillTablesMode, firstFileRule, secondRule);
        if (!flag2)
        {
          TableForSnapshot.RuleType ruleType1 = firstFileRule.GetRuleType();
          TableForSnapshot.RuleType ruleType2 = secondRule.GetRuleType();
          this._logger.LogIncompatibleFileRules(firstFilePath, secondFilePath, key, ruleType1, ruleType2);
        }
        flag1 &= flag2;
      }
    }
    return flag1;
  }

  private Dictionary<string, TableForSnapshot> ReadAdjustmentsTableRules(
    string configPath,
    bool fillTablesMode,
    IDataSchema schema)
  {
    XmlElement xmlElement = this.ReadConfigRootElement(configPath);
    Dictionary<string, TableForSnapshot> dictionary1 = this.ReadTableRules(configPath, xmlElement.ChildNodes, true);
    Dictionary<string, TableForSnapshot> dictionary2 = new Dictionary<string, TableForSnapshot>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (KeyValuePair<string, TableForSnapshot> keyValuePair in dictionary1)
    {
      TableForSnapshot.RuleType ruleType = keyValuePair.Value.GetRuleType();
      if (!this._ruleManager.RuleValid(fillTablesMode, keyValuePair.Key, schema, ruleType))
        this._logger.LogSkippedAdjustmentsRule(configPath, keyValuePair.Key, ruleType);
      else
        dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
    }
    return dictionary1;
  }

  private Dictionary<string, TableForSnapshot> ReadBaseTableRules(
    string configPath,
    PointDbmsBase point,
    bool forceMasked,
    int company,
    bool includeCompany,
    out bool fillTablesMode)
  {
    XmlElement xmlElement = string.IsNullOrEmpty(configPath) ? (XmlElement) null : this.ReadConfigRootElement(configPath);
    string str = (string) null;
    if (xmlElement != null)
      str = xmlElement.Attributes["fill"]?.Value ?? throw new PXException("The root node of the {0} snapshot configuration file must contain a value for the '{1}' attribute.", new object[1]
      {
        (object) "fill"
      });
    fillTablesMode = xmlElement == null || bool.TrueString.Equals(str, StringComparison.OrdinalIgnoreCase);
    Dictionary<string, TableForSnapshot> ruleByTable = new Dictionary<string, TableForSnapshot>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    if (fillTablesMode)
      this.FillIncludeTableRules(ruleByTable, point, forceMasked, company, includeCompany);
    if (xmlElement == null)
      return ruleByTable;
    foreach (KeyValuePair<string, TableForSnapshot> readTableRule in this.ReadTableRules(configPath, xmlElement.ChildNodes, false))
    {
      TableForSnapshot.RuleType ruleType = readTableRule.Value.GetRuleType();
      if (!this._ruleManager.RuleValid(fillTablesMode, readTableRule.Key, point.Schema, ruleType))
        this._logger.LogSkippedBaseRule(configPath, readTableRule.Key, ruleType);
      else
        ruleByTable[readTableRule.Key] = readTableRule.Value;
    }
    return ruleByTable;
  }

  private bool CheckObsoleteAttributes(string configPath, XmlNode node)
  {
    bool flag = false;
    foreach (string obsoleteAttribute in SnapshotConfigReader._obsoleteAttributes)
    {
      if (node.Attributes[obsoleteAttribute]?.Value != null)
      {
        flag = true;
        this._logger.LogObsoleteRuleElement(configPath, obsoleteAttribute);
      }
    }
    return flag;
  }

  private Dictionary<string, TableForSnapshot> ReadTableRules(
    string configPath,
    XmlNodeList nodes,
    bool ignorePreserve)
  {
    Dictionary<string, TableForSnapshot> dictionary = new Dictionary<string, TableForSnapshot>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    bool flag = false;
    foreach (XmlNode node in nodes)
    {
      if (node.NodeType == XmlNodeType.Element)
      {
        flag = this.CheckObsoleteAttributes(configPath, node);
        string str = node.Attributes["table"]?.Value;
        if (!string.IsNullOrEmpty(str))
        {
          bool obsoleteAliasElementFound = false;
          IEnumerable<(string, TableForSnapshot)> valueTuples1;
          switch (node.Name)
          {
            case "Exclude":
              valueTuples1 = this.ReadExcludeRule(node, str, ignorePreserve);
              break;
            case "Include":
              valueTuples1 = (IEnumerable<(string, TableForSnapshot)>) new (string, TableForSnapshot)[1]
              {
                (str, this.ReadIncludeRule(node, str, ignorePreserve, out obsoleteAliasElementFound))
              };
              break;
            default:
              throw new PXException("An unknown rule ({0}) is specified for the {1} table.", new object[2]
              {
                (object) node.Name,
                (object) str
              });
          }
          IEnumerable<(string, TableForSnapshot)> valueTuples2 = valueTuples1;
          if (obsoleteAliasElementFound)
          {
            flag = true;
            this._logger.LogObsoleteRuleElement(configPath, "alias");
          }
          foreach ((string key, TableForSnapshot tableForSnapshot) in valueTuples2)
            dictionary[key] = tableForSnapshot;
        }
      }
    }
    if (flag)
      throw new PXException("Error occurred during the merge of snapshot configuration files, and the snapshot was not created. For details, see the trace log: Click Tools > Trace on the form title bar");
    return dictionary;
  }

  private IEnumerable<(string Table, TableForSnapshot rule)> ReadExcludeRule(
    XmlNode node,
    string mainTable,
    bool ignorePreserve)
  {
    yield return (mainTable, node.Attributes["preserve"] == null | ignorePreserve ? TableForSnapshot.Excluded : TableForSnapshot.ExcludePreserve);
    foreach (string dependentTable in this.GetDependentTables(mainTable))
      yield return (dependentTable, TableForSnapshot.Excluded);
  }

  private IEnumerable<string> GetDependentTables(string mainTable)
  {
    System.Type tableType = ServiceManager.TryGetTableType(mainTable);
    return tableType == (System.Type) null ? Enumerable.Empty<string>() : PXCache._GetExtensions(tableType, false).Select<System.Type, string>((Func<System.Type, string>) (e => e.Name));
  }

  private TableForSnapshot ReadIncludeRule(
    XmlNode node,
    string table,
    bool ignorePreserve,
    out bool obsoleteAliasElementFound)
  {
    return PxExportUtils.ParseXmlNodeInclude(node, table, ignorePreserve, ref obsoleteAliasElementFound);
  }

  private void FillIncludeTableRules(
    Dictionary<string, TableForSnapshot> ruleByTable,
    PointDbmsBase point,
    bool forceMasked,
    int company,
    bool includeCompany)
  {
    HashSet<string> hashSet = PxExportUtils.SelectTablesToIncludeIntoSnapshot(point, (Func<TableHeader, bool>) (t => t.HasCompanyId()), (forceMasked ? 1 : 0) != 0, new int[1]
    {
      company
    }).Select<TableHeader, string>((Func<TableHeader, string>) (t => ((TableEntityBase) t).Name)).ToHashSet<string>();
    if (includeCompany)
      hashSet.Add("Company");
    foreach (string str in hashSet)
    {
      TableForSnapshot includeRule = this.CreateIncludeRule(str, true);
      ruleByTable.Add(str, includeRule);
    }
  }

  private TableForSnapshot CreateIncludeRule(string tableName, bool fromAutomaticFilling)
  {
    return new TableForSnapshot(true, fromAutomaticFilling)
    {
      TableQuery = (YaqlTable) Yaql.schemaTable(tableName, (string) null)
    };
  }

  internal XmlElement ReadConfigRootElement(string path)
  {
    XmlDocument xmlDocument = new XmlDocument()
    {
      XmlResolver = (XmlResolver) null
    };
    xmlDocument.Load(path);
    string name = xmlDocument.DocumentElement?.Name;
    if (!name.Equals("Tables", StringComparison.OrdinalIgnoreCase))
      throw new PXException("The snapshot configuration file contains an invalid root element ({0}). The valid value for the root element is {1}.", new object[2]
      {
        (object) name,
        (object) "Tables"
      });
    return xmlDocument.DocumentElement;
  }

  private IEnumerable<string> FindAdjustmentsFilePaths(string baseConfigFilePath)
  {
    DirectoryInfo directoryInfo = new DirectoryInfo(Path.ChangeExtension(baseConfigFilePath, (string) null));
    return !directoryInfo.Exists ? Enumerable.Empty<string>() : ((IEnumerable<FileInfo>) directoryInfo.GetFiles("*.esc")).Select<FileInfo, string>((Func<FileInfo, string>) (f => f.FullName));
  }

  private string GetConfigFileNameWithExtension(string fileName) => fileName + ".esc";

  private string FindBaseConfigFilePath(string exportMode)
  {
    string path = Path.Combine(SnapshotConfigReader.ConfigRootFolder, this.GetConfigFileNameWithExtension(exportMode));
    if (File.Exists(path))
      return path;
    string fileName;
    if (!((IEnumerable<string>) SnapshotConfigReader.GetExportModes(true)).Zip<string, string, Tuple<string, string>>((IEnumerable<string>) SnapshotConfigReader.GetExportModes(), (Func<string, string, Tuple<string, string>>) ((a1, a2) => Tuple.Create<string, string>(a1, a2))).ToDictionary<Tuple<string, string>, string, string>((Func<Tuple<string, string>, string>) (kv => kv.Item1), (Func<Tuple<string, string>, string>) (kv => kv.Item2)).TryGetValue(exportMode, out fileName))
      return (string) null;
    this.GetConfigFileNameWithExtension(fileName);
    return !File.Exists(path) ? (string) null : path;
  }
}
