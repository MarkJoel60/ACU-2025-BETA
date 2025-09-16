// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.SnapshotConfigRuleManager
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.BulkInsert;
using PX.DbServices.Model.Entities;
using PX.DbServices.Model.Schema;
using PX.DbServices.QueryObjectModel;

#nullable disable
namespace PX.Data.Update;

internal class SnapshotConfigRuleManager
{
  internal bool RuleValid(
    bool fillTablesMode,
    string tableName,
    IDataSchema schema,
    TableForSnapshot.RuleType ruleType)
  {
    if (!fillTablesMode)
      return ruleType != 4;
    if (ruleType != null)
      return true;
    TableHeader table = schema.GetTable(tableName);
    return table != null && table.HasCompanyId() && !table.HasCompanyMask();
  }

  internal bool RulesCompatible(
    bool fillTablesMode,
    TableForSnapshot firstRule,
    TableForSnapshot secondRule)
  {
    TableForSnapshot.RuleType ruleType1 = firstRule.GetRuleType();
    TableForSnapshot.RuleType ruleType2 = secondRule.GetRuleType();
    if (fillTablesMode)
    {
      if (ruleType1 == 3 && ruleType2 == 2 || ruleType1 == 2 && ruleType2 == 3 || ruleType1 == 2 && ruleType2 == 2 && !firstRule.Transform.ResetsEquals(secondRule.Transform))
        return false;
    }
    else if (ruleType1 == 3 && ruleType2 == 2 || ruleType1 == 2 && ruleType2 == 3 || ruleType1 == 2 && ruleType2 == 2 && !firstRule.Transform.ResetsEquals(secondRule.Transform))
      return false;
    return true;
  }

  internal TableForSnapshot MergeBaseRuleWithAdjustmentRule(
    TableForSnapshot baseRule,
    TableForSnapshot adjustmentRule)
  {
    TableForSnapshot.RuleType ruleType1 = baseRule.GetRuleType();
    TableForSnapshot.RuleType ruleType2 = adjustmentRule.GetRuleType();
    switch ((int) ruleType1)
    {
      case 0:
        TableForSnapshot tableForSnapshot1;
        switch ((int) ruleType2)
        {
          case 0:
            tableForSnapshot1 = adjustmentRule;
            break;
          case 2:
            tableForSnapshot1 = adjustmentRule;
            break;
          case 3:
            tableForSnapshot1 = adjustmentRule;
            break;
          case 4:
            tableForSnapshot1 = adjustmentRule;
            break;
          default:
            throw new PXException("The following incompatible rules have been detected in the snapshot configuration files: {0}, {1}.", new object[2]
            {
              (object) ruleType1,
              (object) ruleType2
            });
        }
        return tableForSnapshot1;
      case 1:
        TableForSnapshot tableForSnapshot2;
        switch ((int) ruleType2)
        {
          case 0:
            tableForSnapshot2 = baseRule;
            break;
          case 2:
            tableForSnapshot2 = this.MergeBaseRuleWithPreserveAdjustmentRule(baseRule, adjustmentRule);
            break;
          case 3:
            tableForSnapshot2 = this.MergeBaseRuleWithPreserveAdjustmentRule(baseRule, adjustmentRule);
            break;
          case 4:
            tableForSnapshot2 = TableForSnapshot.ExcludePreserve;
            break;
          default:
            throw new PXException("The following incompatible rules have been detected in the snapshot configuration files: {0}, {1}.", new object[2]
            {
              (object) ruleType1,
              (object) ruleType2
            });
        }
        return tableForSnapshot2;
      case 2:
        TableForSnapshot tableForSnapshot3;
        switch ((int) ruleType2)
        {
          case 0:
            tableForSnapshot3 = adjustmentRule;
            break;
          case 2:
            tableForSnapshot3 = adjustmentRule;
            break;
          case 3:
            tableForSnapshot3 = adjustmentRule;
            break;
          case 4:
            tableForSnapshot3 = adjustmentRule;
            break;
          default:
            throw new PXException("The following incompatible rules have been detected in the snapshot configuration files: {0}, {1}.", new object[2]
            {
              (object) ruleType1,
              (object) ruleType2
            });
        }
        return tableForSnapshot3;
      case 3:
        TableForSnapshot tableForSnapshot4;
        switch ((int) ruleType2)
        {
          case 0:
            tableForSnapshot4 = adjustmentRule;
            break;
          case 2:
            tableForSnapshot4 = adjustmentRule;
            break;
          case 3:
            tableForSnapshot4 = adjustmentRule;
            break;
          case 4:
            tableForSnapshot4 = adjustmentRule;
            break;
          default:
            throw new PXException("The following incompatible rules have been detected in the snapshot configuration files: {0}, {1}.", new object[2]
            {
              (object) ruleType1,
              (object) ruleType2
            });
        }
        return tableForSnapshot4;
      case 4:
        TableForSnapshot tableForSnapshot5;
        switch ((int) ruleType2)
        {
          case 0:
            tableForSnapshot5 = adjustmentRule;
            break;
          case 2:
            tableForSnapshot5 = adjustmentRule;
            break;
          case 3:
            tableForSnapshot5 = adjustmentRule;
            break;
          case 4:
            tableForSnapshot5 = adjustmentRule;
            break;
          default:
            throw new PXException("The following incompatible rules have been detected in the snapshot configuration files: {0}, {1}.", new object[2]
            {
              (object) ruleType1,
              (object) ruleType2
            });
        }
        return tableForSnapshot5;
      case 5:
        TableForSnapshot tableForSnapshot6;
        switch ((int) ruleType2)
        {
          case 0:
            tableForSnapshot6 = adjustmentRule;
            break;
          case 2:
            tableForSnapshot6 = adjustmentRule;
            break;
          case 3:
            tableForSnapshot6 = adjustmentRule;
            break;
          case 4:
            tableForSnapshot6 = baseRule;
            break;
          default:
            throw new PXException("The following incompatible rules have been detected in the snapshot configuration files: {0}, {1}.", new object[2]
            {
              (object) ruleType1,
              (object) ruleType2
            });
        }
        return tableForSnapshot6;
      default:
        throw new PXException("The following incompatible rules have been detected in the snapshot configuration files: {0}, {1}.", new object[2]
        {
          (object) ruleType1,
          (object) ruleType2
        });
    }
  }

  private TableForSnapshot MergeBaseRuleWithPreserveAdjustmentRule(
    TableForSnapshot baseRule,
    TableForSnapshot adjustmentRule)
  {
    TableForSnapshot.RuleType ruleType1 = baseRule.GetRuleType();
    TableForSnapshot.RuleType ruleType2 = adjustmentRule.GetRuleType();
    if (ruleType2 != 3 && ruleType2 != 2)
      throw new PXException("The following incompatible rules have been detected in the snapshot configuration files: {0}, {1}.", new object[2]
      {
        (object) ruleType1,
        (object) ruleType2
      });
    if (ruleType1 == 1)
      return new TableForSnapshot(true, false)
      {
        TableQuery = adjustmentRule.TableQuery,
        Transform = adjustmentRule.Transform,
        Restriction = baseRule.Restriction
      };
    if (ruleType1 == 5)
      return adjustmentRule;
    throw new PXException("The following incompatible rules have been detected in the snapshot configuration files: {0}, {1}.", new object[2]
    {
      (object) ruleType1,
      (object) ruleType2
    });
  }

  internal TableForSnapshot MergeAdjustmentRules(
    bool fillTablesMode,
    TableForSnapshot firstRule,
    TableForSnapshot secondRule)
  {
    return !fillTablesMode ? this.MergeAdjustmentRulesForNotFillMode(firstRule, secondRule) : this.MergeAdjustmentRulesForFillMode(firstRule, secondRule);
  }

  private TableForSnapshot MergeAdjustmentRulesForFillMode(
    TableForSnapshot firstRule,
    TableForSnapshot secondRule)
  {
    TableForSnapshot.RuleType ruleType1 = firstRule.GetRuleType();
    TableForSnapshot.RuleType ruleType2 = secondRule.GetRuleType();
    switch ((int) ruleType1)
    {
      case 0:
        TableForSnapshot tableForSnapshot1;
        switch ((int) ruleType2)
        {
          case 0:
            tableForSnapshot1 = secondRule;
            break;
          case 2:
            tableForSnapshot1 = secondRule;
            break;
          case 3:
            tableForSnapshot1 = secondRule;
            break;
          case 4:
            tableForSnapshot1 = secondRule;
            break;
          default:
            throw new PXException("The following incompatible rules have been detected in the snapshot configuration files: {0}, {1}.", new object[2]
            {
              (object) ruleType1,
              (object) ruleType2
            });
        }
        return tableForSnapshot1;
      case 2:
        TableForSnapshot tableForSnapshot2;
        switch ((int) ruleType2)
        {
          case 0:
            tableForSnapshot2 = firstRule;
            break;
          case 2:
            tableForSnapshot2 = this.MergeIncludeResetColumnRules(firstRule, secondRule);
            break;
          case 4:
            tableForSnapshot2 = secondRule;
            break;
          default:
            throw new PXException("The following incompatible rules have been detected in the snapshot configuration files: {0}, {1}.", new object[2]
            {
              (object) ruleType1,
              (object) ruleType2
            });
        }
        return tableForSnapshot2;
      case 3:
        TableForSnapshot tableForSnapshot3;
        switch ((int) ruleType2)
        {
          case 0:
            tableForSnapshot3 = firstRule;
            break;
          case 3:
            tableForSnapshot3 = this.MergeIncludeConditionRules(true, firstRule, secondRule);
            break;
          case 4:
            tableForSnapshot3 = secondRule;
            break;
          default:
            throw new PXException("The following incompatible rules have been detected in the snapshot configuration files: {0}, {1}.", new object[2]
            {
              (object) ruleType1,
              (object) ruleType2
            });
        }
        return tableForSnapshot3;
      case 4:
        TableForSnapshot tableForSnapshot4;
        switch ((int) ruleType2)
        {
          case 0:
            tableForSnapshot4 = firstRule;
            break;
          case 2:
            tableForSnapshot4 = firstRule;
            break;
          case 3:
            tableForSnapshot4 = firstRule;
            break;
          case 4:
            tableForSnapshot4 = firstRule;
            break;
          default:
            throw new PXException("The following incompatible rules have been detected in the snapshot configuration files: {0}, {1}.", new object[2]
            {
              (object) ruleType1,
              (object) ruleType2
            });
        }
        return tableForSnapshot4;
      default:
        throw new PXException("The following incompatible rules have been detected in the snapshot configuration files: {0}, {1}.", new object[2]
        {
          (object) ruleType1,
          (object) ruleType2
        });
    }
  }

  private TableForSnapshot MergeIncludeConditionRules(
    bool fillTablesMode,
    TableForSnapshot firstRule,
    TableForSnapshot secondRule)
  {
    YaqlTableQuery tableQuery1 = (YaqlTableQuery) firstRule.TableQuery;
    YaqlTableQuery tableQuery2 = (YaqlTableQuery) secondRule.TableQuery;
    YaqlTableQuery yaqlTableQuery = tableQuery1;
    ((YaqlQueryBase) yaqlTableQuery).Condition = fillTablesMode ? Yaql.and(((YaqlQueryBase) tableQuery1).Condition, ((YaqlQueryBase) tableQuery2).Condition) : Yaql.or(((YaqlQueryBase) tableQuery1).Condition, ((YaqlQueryBase) tableQuery2).Condition);
    return new TableForSnapshot(true, false)
    {
      TableQuery = (YaqlTable) yaqlTableQuery
    };
  }

  private TableForSnapshot MergeIncludeResetColumnRules(
    TableForSnapshot firstRule,
    TableForSnapshot secondRule)
  {
    if (!firstRule.Transform.ResetsEquals(secondRule.Transform))
      throw new PXException("The following incompatible rules have been detected in the snapshot configuration files: {0}, {1}.", new object[2]
      {
        (object) firstRule.GetRuleType(),
        (object) secondRule.GetRuleType()
      });
    return firstRule;
  }

  private TableForSnapshot MergeAdjustmentRulesForNotFillMode(
    TableForSnapshot firstRule,
    TableForSnapshot secondRule)
  {
    TableForSnapshot.RuleType ruleType1 = firstRule.GetRuleType();
    TableForSnapshot.RuleType ruleType2 = secondRule.GetRuleType();
    switch ((int) ruleType1)
    {
      case 0:
        TableForSnapshot tableForSnapshot1;
        switch ((int) ruleType2)
        {
          case 0:
            tableForSnapshot1 = firstRule;
            break;
          case 2:
            tableForSnapshot1 = firstRule;
            break;
          case 3:
            tableForSnapshot1 = firstRule;
            break;
          case 4:
            tableForSnapshot1 = firstRule;
            break;
          default:
            throw new PXException("The following incompatible rules have been detected in the snapshot configuration files: {0}, {1}.", new object[2]
            {
              (object) ruleType1,
              (object) ruleType2
            });
        }
        return tableForSnapshot1;
      case 2:
        TableForSnapshot tableForSnapshot2;
        switch ((int) ruleType2)
        {
          case 0:
            tableForSnapshot2 = secondRule;
            break;
          case 2:
            tableForSnapshot2 = this.MergeIncludeResetColumnRules(firstRule, secondRule);
            break;
          case 4:
            tableForSnapshot2 = firstRule;
            break;
          default:
            throw new PXException("The following incompatible rules have been detected in the snapshot configuration files: {0}, {1}.", new object[2]
            {
              (object) ruleType1,
              (object) ruleType2
            });
        }
        return tableForSnapshot2;
      case 3:
        TableForSnapshot tableForSnapshot3;
        switch ((int) ruleType2)
        {
          case 0:
            tableForSnapshot3 = secondRule;
            break;
          case 3:
            tableForSnapshot3 = this.MergeIncludeConditionRules(false, firstRule, secondRule);
            break;
          case 4:
            tableForSnapshot3 = firstRule;
            break;
          default:
            throw new PXException("The following incompatible rules have been detected in the snapshot configuration files: {0}, {1}.", new object[2]
            {
              (object) ruleType1,
              (object) ruleType2
            });
        }
        return tableForSnapshot3;
      case 4:
        TableForSnapshot tableForSnapshot4;
        switch ((int) ruleType2)
        {
          case 0:
            tableForSnapshot4 = secondRule;
            break;
          case 2:
            tableForSnapshot4 = secondRule;
            break;
          case 3:
            tableForSnapshot4 = secondRule;
            break;
          case 4:
            tableForSnapshot4 = secondRule;
            break;
          default:
            throw new PXException("The following incompatible rules have been detected in the snapshot configuration files: {0}, {1}.", new object[2]
            {
              (object) ruleType1,
              (object) ruleType2
            });
        }
        return tableForSnapshot4;
      default:
        throw new PXException("The following incompatible rules have been detected in the snapshot configuration files: {0}, {1}.", new object[2]
        {
          (object) ruleType1,
          (object) ruleType2
        });
    }
  }
}
