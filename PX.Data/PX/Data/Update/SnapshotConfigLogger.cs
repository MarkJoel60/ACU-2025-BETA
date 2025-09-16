// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.SnapshotConfigLogger
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.BulkInsert;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.Points.MsSql;
using PX.DbServices.QueryObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Data.Update;

internal class SnapshotConfigLogger
{
  internal void LogSkippedBaseRule(
    string configPath,
    string tableName,
    TableForSnapshot.RuleType ruleType)
  {
    PXTrace.Logger.Warning<string, TableForSnapshot.RuleType, string>("The {ConfigPath} base snapshot configuration file contains invalid {RuleType} rule for the {RuleTable} table.", configPath, ruleType, tableName);
  }

  internal void LogSkippedAdjustmentsRule(
    string configPath,
    string tableName,
    TableForSnapshot.RuleType ruleType)
  {
    PXTrace.Logger.Warning<string, TableForSnapshot.RuleType, string>("The {ConfigPath} adjustments snapshot configuration file contains redundant {RuleType} rule for the {RuleTable} table. The rule was skipped.", configPath, ruleType, tableName);
  }

  internal void LogIncompatibleFileRules(
    string firstFilePath,
    string secondFilePath,
    string tableName,
    TableForSnapshot.RuleType firstRuleType,
    TableForSnapshot.RuleType secondRuleType)
  {
    PXTrace.Logger.Error("Found incompatible rules {FirstRuleType} and {SecondRuleType} for the {TableName} table in the {FirstFile} and {SecondFile} files.", new object[5]
    {
      (object) firstRuleType,
      (object) secondRuleType,
      (object) tableName,
      (object) firstFilePath,
      (object) secondFilePath
    });
  }

  internal void LogObsoleteRuleElement(string configPath, string elementName)
  {
    PXTrace.Logger.Error<string, string>("The {ConfigPath} snapshot configuration file contains obsolete rule element {ObsoleteElement}", configPath, elementName);
  }

  internal void LogConflictKvExtRules(
    Dictionary<string, (TableForSnapshot oldRule, TableForSnapshot newRule)> rules)
  {
    StringBuilder stringBuilder = new StringBuilder("The rules for particular tables and their KvExt tables were inconsistent. The following rules were replaced for KvExt tables:\n");
    foreach (string key in (IEnumerable<string>) rules.Keys.OrderBy<string, string>((Func<string, string>) (k => k)))
    {
      (TableForSnapshot oldRule, TableForSnapshot newRule) = rules[key];
      stringBuilder.AppendLine();
      stringBuilder.AppendLine("Table: " + key);
      stringBuilder.AppendLine($"Rule: {oldRule.GetRuleType()} -> {newRule.GetRuleType()}");
    }
    PXTrace.WriteInformation(stringBuilder.ToString());
  }

  internal void LogMergedRules(Dictionary<string, TableForSnapshot> rules)
  {
    StringBuilder stringBuilder = new StringBuilder("Here are the merged snapshot config rules:\n");
    IOrderedEnumerable<string> orderedEnumerable = rules.Keys.OrderBy<string, string>((Func<string, string>) (k => k));
    MsSqlScripter genericScripter = PointMsSqlServer.GenericScripter;
    foreach (string key in (IEnumerable<string>) orderedEnumerable)
    {
      TableForSnapshot rule = rules[key];
      TableForSnapshot.RuleType ruleType = rule.GetRuleType();
      stringBuilder.AppendLine();
      stringBuilder.AppendLine("table: " + key);
      stringBuilder.AppendLine($"rule: {ruleType}");
      if (rule.Restriction != null)
      {
        stringBuilder.AppendLine("preserve restriction:");
        stringBuilder.AppendLine(((YaqlArgument) rule.Restriction).ToSql((CommandScripter) genericScripter, (SqlGenerationOptions) null));
      }
      if (rule.Transform != null)
      {
        stringBuilder.AppendLine("reset transformation(s):");
        stringBuilder.Append(rule.Transform.ResetsToString());
      }
      if (rule.TableQuery is YaqlTableQuery tableQuery)
      {
        stringBuilder.AppendLine("include condition:");
        stringBuilder.AppendLine(((YaqlArgument) ((YaqlQueryBase) tableQuery).Condition).ToSql((CommandScripter) genericScripter, (SqlGenerationOptions) null));
      }
    }
    PXTrace.WriteInformation(stringBuilder.ToString());
  }
}
