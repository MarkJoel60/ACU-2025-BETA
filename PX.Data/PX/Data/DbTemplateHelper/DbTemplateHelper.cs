// Decompiled with JetBrains decompiler
// Type: PX.Data.DbTemplateHelper.DbTemplateHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.DbServices.Commands;
using PX.DbServices.Commands.Data;
using PX.DbServices.Model;
using PX.DbServices.Points;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.QueryObjectModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

#nullable disable
namespace PX.Data.DbTemplateHelper;

/// <summary>
/// Helper class that removes all user-specific data from the tables keeping only system template.
/// </summary>
[PXInternalUseOnly]
public class DbTemplateHelper
{
  protected readonly PXGraph _graph;

  public DbTemplateHelper(PXGraph graph)
  {
    this._graph = graph != null ? graph : throw new ArgumentNullException(nameof (graph));
  }

  protected virtual void ClearTable(
    string tableName,
    int templateCompanyId,
    List<CommandBase> batch)
  {
    YaqlCondition yaqlCondition = Yaql.gt<YaqlScalar>((YaqlScalar) Yaql.column("CompanyID", (string) null), Yaql.constant<int>(templateCompanyId, SqlDbType.Variant));
    batch.Add((CommandBase) new CmdDelete(Yaql.schemaTable(tableName, (string) null), (List<YaqlJoin>) null)
    {
      Condition = yaqlCondition
    });
  }

  public void ResetToDefault(IEnumerable<System.Type> tables)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint((IDbTransaction) PXTransactionScope.GetTransaction());
      List<CompanyHeader> companies = dbServicesPoint.getCompanies(true);
      ExecutionContext executionContext = new ExecutionContext((IExecutionObserver) null);
      List<CommandBase> batch = new List<CommandBase>();
      foreach (System.Type table1 in tables)
      {
        PXCache cach = this._graph.Caches[table1];
        ITableAdapter table2 = dbServicesPoint.GetTable(table1.Name, FileMode.Open);
        int companyId = PXDatabase.Provider.getCompanyID(table2.TableName, out companySetting _);
        int templateCompanyId = this.GetTemplateCompanyId(companyId, (IEnumerable<CompanyHeader>) companies);
        if (table2.Header.HasCompanyMask())
        {
          int defaultBits = AcumaticaDb.getDefaultBits(table2.Header.getColumnByName("CompanyMask").Default, templateCompanyId);
          batch.AddRange(this.CreateResetCustomChangesCommands(table2.TableName, templateCompanyId, companyId, defaultBits, (Func<int, bool, string, YaqlCondition>) ((cid, skipSystem, alias) => this.CreateCompanyRestriction((IEnumerable<CompanyHeader>) companies, templateCompanyId, cid, skipSystem, alias))));
        }
        else
          this.ClearTable(table2.TableName, templateCompanyId, batch);
        cach.ClearQueryCacheObsolete();
      }
      dbServicesPoint.executeCommands((IEnumerable<CommandBase>) batch, executionContext, false);
      PXDatabase.Provider.SaveWatchDog(tables);
      transactionScope.Complete(this._graph);
    }
  }

  protected int GetTemplateCompanyId(int currentCompanyId, IEnumerable<CompanyHeader> companies)
  {
    if (companies == null)
      throw new ArgumentNullException(nameof (companies));
    Dictionary<int, CompanyHeader> dictionary = companies.ToDictionary<CompanyHeader, int, CompanyHeader>((System.Func<CompanyHeader, int>) (c => c.Id), (System.Func<CompanyHeader, CompanyHeader>) (c => c));
    CompanyHeader companyHeader1;
    if (!dictionary.TryGetValue(currentCompanyId, out companyHeader1))
      throw new ArgumentException($"Cannot find company header for id = {currentCompanyId}", nameof (currentCompanyId));
    CompanyHeader companyHeader2;
    for (; companyHeader1.ParentId.HasValue && !companyHeader1.IsReadonly; companyHeader1 = companyHeader2)
    {
      if (!dictionary.TryGetValue(companyHeader1.ParentId.Value, out companyHeader2))
        throw new ArgumentException($"Cannot find company header for id = {companyHeader1.ParentId.Value}", nameof (companies));
    }
    return companyHeader1.Id;
  }

  protected YaqlCondition CreateCompanyRestriction(
    IEnumerable<CompanyHeader> companies,
    int templateCompanyId,
    int companyId,
    bool skipSystem,
    string alias)
  {
    if (skipSystem)
      companies = companies.Where<CompanyHeader>((System.Func<CompanyHeader, bool>) (c => c.Id != templateCompanyId));
    return (YaqlCondition) Yaql.companyIdEq(companyId, companies.ToList<CompanyHeader>(), true, alias, false);
  }

  protected virtual IEnumerable<CommandBase> CreateResetCustomChangesCommands(
    string tableName,
    int templateCompanyId,
    int currentCompanyId,
    int defaultMask,
    Func<int, bool, string, YaqlCondition> condition)
  {
    yield return (CommandBase) new CmdDelete(YaqlSchemaTable.op_Implicit(tableName), (List<YaqlJoin>) null)
    {
      Condition = condition(currentCompanyId, true, tableName)
    };
    yield return (CommandBase) new CmdUpdate(YaqlSchemaTable.op_Implicit(tableName), (IEnumerable<YaqlJoin>) null)
    {
      Condition = condition(templateCompanyId, false, tableName),
      AssignValues = {
        {
          "CompanyMask",
          YaqlCompanyMask.set<int, int>(currentCompanyId, defaultMask, (string) null)
        }
      }
    };
  }
}
