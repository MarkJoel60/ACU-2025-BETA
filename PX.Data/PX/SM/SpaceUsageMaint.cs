// Decompiled with JetBrains decompiler
// Type: PX.SM.SpaceUsageMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.Update;
using PX.DbServices.Commands;
using PX.DbServices.Commands.Data;
using PX.DbServices.Model.Entities;
using PX.DbServices.Points;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.QueryObjectModel;
using PX.Licensing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

public class SpaceUsageMaint : PXGraph<SpaceUsageMaint>
{
  public PXSelect<SpaceUsageCalculationHistory> CalculationHistory;
  public PXSelectReadonly<UPCompany> Companies;
  public PXSelectReadonly<UPSnapshotSize> Snapshots;
  public PXSelect<TableSize> Tables;
  public PXSelectReadonly<TablesCompanySize> PopupCompanyTablesDefinition;
  public PXSelectReadonly<TablesSnapshotSize> PopupSnapshotTablesDefinition;
  public PXSelectReadonly<CompanyByTableSize> PopupCompaniesByTableDefinition;
  public PXSelectReadonly<TablesCompanySize> PopupCompanyTablesHeader;
  public PXSelectReadonly<TablesSnapshotSize> PopupSnapshotTablesHeader;
  public PXSelectReadonly<CompanyByTableSize> PopupCompaniesByTableHeader;
  private static PointDbmsBase _dbPoint;
  public PXAction<SpaceUsageCalculationHistory> CalculateUsedSpaceCommand;
  public PXAction<SpaceUsageCalculationHistory> ViewCompany;
  public PXAction<SpaceUsageCalculationHistory> ViewCompanyTables;
  public PXAction<SpaceUsageCalculationHistory> ViewSnapshotTables;
  public PXAction<SpaceUsageCalculationHistory> ViewCompaniesByTable;
  public PXAction<SpaceUsageCalculationHistory> ViewSnapshot;

  [InjectDependency]
  private ILicensingManager _licensingManager { get; set; }

  private static PointDbmsBase _point
  {
    get
    {
      if (SpaceUsageMaint._dbPoint == null)
        SpaceUsageMaint._dbPoint = PXDatabase.Provider.CreateDbServicesPoint();
      return SpaceUsageMaint._dbPoint;
    }
  }

  public SpaceUsageMaint()
  {
    this.Tables.AllowInsert = false;
    this.Tables.AllowUpdate = false;
    this.Tables.AllowDelete = false;
  }

  public static SpaceUsageCalculationHistory GetCalculatedSpaceUsage()
  {
    return PXGraph.CreateInstance<SpaceUsageMaint>().CalculationHistory.SelectSingle();
  }

  public static bool CalculateSpaceUsage()
  {
    if (WebConfig.CalculateUsedSpace)
    {
      SpaceUsageMaint instance = PXGraph.CreateInstance<SpaceUsageMaint>();
      if (instance._licensingManager.GetDbSizeQuota() > 0L)
      {
        instance.CalculateUsedSpaceCommand.Press();
        PXLongOperation.WaitCompletion(instance.UID);
        return true;
      }
    }
    return false;
  }

  protected IEnumerable calculationHistory()
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    SpaceUsageMaint graph = this;
    if (num != 0)
    {
      if (num != 1)
        return false;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      return false;
    }
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E2__current = (object) new PXSelect<SpaceUsageCalculationHistory>((PXGraph) graph).Select().AsEnumerable<PXResult<SpaceUsageCalculationHistory>>().LastOrDefault<PXResult<SpaceUsageCalculationHistory>>();
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = 1;
    return true;
  }

  protected IEnumerable companies()
  {
    return (IEnumerable) PXCompanyHelper.SelectCompanies(PXCompanySelectOptions.All, true).Where<UPCompany>((Func<UPCompany, bool>) (c =>
    {
      int? companyId = c.CompanyID;
      int num = 1;
      return companyId.GetValueOrDefault() > num & companyId.HasValue;
    }));
  }

  protected IEnumerable snapshots()
  {
    YaqlTableQuery yaqlTableQuery = new YaqlTableQuery((YaqlTable) Yaql.schemaTable("UPSnapshot", "s"), (YaqlJoin) Yaql.innerJoin((YaqlTable) Yaql.schemaTable("Company", "c"), Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column("LinkedCompany", "s"), Yaql.column("CompanyID", "c"))), (string) null);
    Dictionary<string, string> dictionary = new Dictionary<string, string>()
    {
      {
        "SourceCompany",
        "s"
      },
      {
        "LinkedCompany",
        "s"
      },
      {
        "SnapshotID",
        "s"
      },
      {
        "Name",
        "s"
      },
      {
        "Description",
        "s"
      },
      {
        "Date",
        "s"
      },
      {
        "ExportMode",
        "s"
      },
      {
        "Size",
        "c"
      }
    };
    List<TableColumn> tableColumnList = new List<TableColumn>();
    foreach (string key in dictionary.Keys)
    {
      string columnName = key;
      yaqlTableQuery.Columns.Add(new YaqlScalarAlilased((YaqlScalar) Yaql.column(columnName, dictionary[columnName]), (string) null));
      if (dictionary[columnName] == "s")
        tableColumnList.Add(SpaceUsageMaint._point.Schema.GetTable("UPSnapshot").Columns.Where<TableColumn>((Func<TableColumn, bool>) (col => ((TableEntityBase) col).Name == columnName)).FirstOrDefault<TableColumn>());
      if (dictionary[columnName] == "c")
        tableColumnList.Add(SpaceUsageMaint._point.Schema.GetTable("Company").Columns.Where<TableColumn>((Func<TableColumn, bool>) (col => ((TableEntityBase) col).Name == columnName)).FirstOrDefault<TableColumn>());
    }
    IEnumerable<object[]> source = SpaceUsageMaint._point.selectTable(yaqlTableQuery, tableColumnList, (SqlGenerationOptions) null);
    this.ViewSnapshotTables.SetEnabled(source.Any<object[]>());
    HashSet<Tuple<Guid?, int?>> distinct = new HashSet<Tuple<Guid?, int?>>();
    foreach (object[] objArray in source)
    {
      if (distinct.Add(new Tuple<Guid?, int?>((Guid?) objArray[2], (int?) objArray[1])))
      {
        UPSnapshotSize upSnapshotSize = new UPSnapshotSize();
        upSnapshotSize.SourceCompany = (int?) objArray[0];
        upSnapshotSize.LinkedCompany = (int?) objArray[1];
        upSnapshotSize.SnapshotID = (Guid?) objArray[2];
        upSnapshotSize.Name = (string) objArray[3];
        upSnapshotSize.Description = (string) objArray[4];
        upSnapshotSize.CreatedDateTime = objArray[5] is System.DateTime ? new System.DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc((System.DateTime) objArray[5], LocaleInfo.GetTimeZone())) : new System.DateTime?();
        upSnapshotSize.ExportMode = (string) objArray[6];
        long? nullable = (long?) objArray[7];
        upSnapshotSize.SizeInDb = nullable.HasValue ? new Decimal?((Decimal) nullable.GetValueOrDefault()) : new Decimal?();
        yield return (object) upSnapshotSize;
      }
    }
  }

  protected IEnumerable currentSnapshot()
  {
    yield return (object) this.Snapshots.Current;
  }

  protected IEnumerable tables()
  {
    return (IEnumerable) new PXSelectGroupBy<TableSize, Aggregate<GroupBy<TableSize.tableName, Sum<TableSize.countOfCompanyRecords, Sum<TableSize.fullSizeByCompany>>>>>((PXGraph) this).Select();
  }

  protected IEnumerable currentTable()
  {
    yield return (object) this.Tables.Current;
  }

  protected IEnumerable popupCompanyTablesDefinition()
  {
    UPCompany current = this.Companies.Current;
    if (current == null)
      return (IEnumerable) null;
    YaqlTableQuery yaqlTableQuery = new YaqlTableQuery((YaqlTable) Yaql.schemaTable("TableSize", "t"), (YaqlJoin) Yaql.innerJoin((YaqlTable) Yaql.schemaTable("Company", "c"), Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column("Company", "t"), Yaql.column("CompanyID", "c"))), (string) null);
    ((YaqlQueryBase) yaqlTableQuery).where(Yaql.eq<int?>((YaqlScalar) Yaql.column("Company", (string) null), current.CompanyID));
    Dictionary<string, string> columnNames = new Dictionary<string, string>()
    {
      {
        "TableName",
        "t"
      },
      {
        "SizeByCompany",
        "t"
      },
      {
        "IndexSizeByCompany",
        "t"
      },
      {
        "CountOfCompanyRecords",
        "t"
      },
      {
        "CompanyCD",
        "c"
      },
      {
        "Size",
        "c"
      }
    };
    foreach (string key in columnNames.Keys)
      yaqlTableQuery.Columns.Add(new YaqlScalarAlilased((YaqlScalar) Yaql.column(key, columnNames[key]), (string) null));
    List<TableColumn> list = SpaceUsageMaint._point.Schema.GetTable("TableSize").Columns.Where<TableColumn>((Func<TableColumn, bool>) (col => columnNames.Keys.Where<string>((Func<string, bool>) (n => columnNames[n] == "t")).Contains<string>(((TableEntityBase) col).Name))).ToList<TableColumn>();
    list.AddRange(SpaceUsageMaint._point.Schema.GetTable("Company").Columns.Where<TableColumn>((Func<TableColumn, bool>) (col => columnNames.Keys.Where<string>((Func<string, bool>) (n => columnNames[n] == "c")).Contains<string>(((TableEntityBase) col).Name))));
    IEnumerable<object[]> objArrays = SpaceUsageMaint._point.selectTable(yaqlTableQuery, list, (SqlGenerationOptions) null);
    List<TablesCompanySize> tablesCompanySizeList1 = new List<TablesCompanySize>();
    foreach (object[] objArray in objArrays)
    {
      List<TablesCompanySize> tablesCompanySizeList2 = tablesCompanySizeList1;
      TablesCompanySize tablesCompanySize = new TablesCompanySize();
      tablesCompanySize.TableName = (string) objArray[0];
      tablesCompanySize.SizeByCompany = (long?) objArray[1];
      tablesCompanySize.IndexSizeByCompany = (long?) objArray[2];
      long? nullable1 = (long?) objArray[1];
      long? nullable2 = (long?) objArray[2];
      tablesCompanySize.FullSizeByCompany = nullable1.HasValue & nullable2.HasValue ? new long?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new long?();
      tablesCompanySize.CountOfCompanyRecords = (long?) objArray[3];
      tablesCompanySize.CompanyName = (string) objArray[4];
      tablesCompanySize.Size = (long?) objArray[5];
      tablesCompanySizeList2.Add(tablesCompanySize);
    }
    return (IEnumerable) tablesCompanySizeList1;
  }

  protected IEnumerable popupCompanyTablesHeader()
  {
    yield return (object) this.PopupCompanyTablesDefinition.SelectSingle();
  }

  protected IEnumerable popupSnapshotTablesDefinition()
  {
    UPSnapshotSize current = this.Snapshots.Current;
    if (current == null)
      return (IEnumerable) null;
    YaqlTableQuery yaqlTableQuery = new YaqlTableQuery((YaqlTable) Yaql.schemaTable("TableSize", "t"), new List<YaqlJoin>()
    {
      (YaqlJoin) Yaql.innerJoin((YaqlTable) Yaql.schemaTable("UPSnapshot", "s"), Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column("Company", "t"), Yaql.column("LinkedCompany", "s"))),
      (YaqlJoin) Yaql.innerJoin((YaqlTable) Yaql.schemaTable("Company", "c"), Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column("Company", "t"), Yaql.column("CompanyID", "c")))
    }, (string) null);
    ((YaqlQueryBase) yaqlTableQuery).where(Yaql.eq<int?>((YaqlScalar) Yaql.column("Company", (string) null), current.LinkedCompany));
    Dictionary<string, string> columnNames = new Dictionary<string, string>()
    {
      {
        "TableName",
        "t"
      },
      {
        "SizeByCompany",
        "t"
      },
      {
        "IndexSizeByCompany",
        "t"
      },
      {
        "CountOfCompanyRecords",
        "t"
      },
      {
        "Name",
        "s"
      },
      {
        "Size",
        "c"
      }
    };
    foreach (string key in columnNames.Keys)
      yaqlTableQuery.Columns.Add(new YaqlScalarAlilased((YaqlScalar) Yaql.column(key, columnNames[key]), (string) null));
    List<TableColumn> list = SpaceUsageMaint._point.Schema.GetTable("TableSize").Columns.Where<TableColumn>((Func<TableColumn, bool>) (col => columnNames.Keys.Where<string>((Func<string, bool>) (n => columnNames[n] == "t")).Contains<string>(((TableEntityBase) col).Name))).ToList<TableColumn>();
    list.AddRange(SpaceUsageMaint._point.Schema.GetTable("UPSnapshot").Columns.Where<TableColumn>((Func<TableColumn, bool>) (col => columnNames.Keys.Where<string>((Func<string, bool>) (n => columnNames[n] == "s")).Contains<string>(((TableEntityBase) col).Name))));
    list.AddRange(SpaceUsageMaint._point.Schema.GetTable("Company").Columns.Where<TableColumn>((Func<TableColumn, bool>) (col => columnNames.Keys.Where<string>((Func<string, bool>) (n => columnNames[n] == "c")).Contains<string>(((TableEntityBase) col).Name))));
    IEnumerable<object[]> objArrays = SpaceUsageMaint._point.selectTable(yaqlTableQuery, list, (SqlGenerationOptions) null);
    List<TablesSnapshotSize> tablesSnapshotSizeList1 = new List<TablesSnapshotSize>();
    HashSet<string> stringSet = new HashSet<string>();
    foreach (object[] objArray in objArrays)
    {
      if (stringSet.Add((string) objArray[0]))
      {
        List<TablesSnapshotSize> tablesSnapshotSizeList2 = tablesSnapshotSizeList1;
        TablesSnapshotSize tablesSnapshotSize = new TablesSnapshotSize();
        tablesSnapshotSize.TableName = (string) objArray[0];
        tablesSnapshotSize.SizeByCompany = (long?) objArray[1];
        tablesSnapshotSize.IndexSizeByCompany = (long?) objArray[2];
        long? nullable1 = (long?) objArray[1];
        long? nullable2 = (long?) objArray[2];
        tablesSnapshotSize.FullSizeByCompany = nullable1.HasValue & nullable2.HasValue ? new long?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new long?();
        tablesSnapshotSize.CountOfCompanyRecords = (long?) objArray[3];
        tablesSnapshotSize.SnapshotName = (string) objArray[4];
        nullable2 = (long?) objArray[5];
        tablesSnapshotSize.Size = nullable2.HasValue ? new Decimal?((Decimal) nullable2.GetValueOrDefault()) : new Decimal?();
        tablesSnapshotSizeList2.Add(tablesSnapshotSize);
      }
    }
    return (IEnumerable) tablesSnapshotSizeList1;
  }

  protected IEnumerable popupSnapshotTablesHeader()
  {
    yield return (object) this.PopupSnapshotTablesDefinition.SelectSingle();
  }

  protected IEnumerable popupCompaniesByTableDefinition()
  {
    TableSize current = this.Tables.Current;
    if (current == null)
      return (IEnumerable) null;
    return (IEnumerable) new PXSelectGroupBy<CompanyByTableSize, Where<TableSize.tableName, Equal<Required<TableSize.tableName>>>, Aggregate<GroupBy<TableSize.tableName, GroupBy<TableSize.company, Sum<TableSize.countOfCompanyRecords, Sum<TableSize.fullSizeByCompany>>>>>>((PXGraph) this).Select((object) current.TableName);
  }

  protected IEnumerable popupCompaniesByTableHeader()
  {
    yield return (object) this.PopupCompaniesByTableDefinition.SelectSingle();
  }

  [PXButton(Tooltip = "Calculate Used Space")]
  [PXUIField(DisplayName = "Calculate Used Space", MapEnableRights = PXCacheRights.Delete, MapViewRights = PXCacheRights.Delete)]
  public IEnumerable calculateUsedSpaceCommand(PXAdapter adapter)
  {
    ILicensingManager licensingManager = this._licensingManager;
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => SpaceUsageMaint.CalculateUsedSpace(licensingManager)));
    return adapter.Get();
  }

  private static void CalculateUsedSpace(ILicensingManager licensingManager)
  {
    PXDatabase.Provider.Truncate(typeof (TableSize));
    SpaceUsageMaint._point.getDbKeeper().CalculateTablesSize();
    PXGraph graph = new PXGraph();
    SpaceUsageCalculationHistory calculationHistory1 = new PXSelect<SpaceUsageCalculationHistory>(graph).SelectSingle();
    bool flag = false;
    if (calculationHistory1 == null)
    {
      calculationHistory1 = new SpaceUsageCalculationHistory();
      calculationHistory1.PkID = new Guid?(Guid.NewGuid());
      flag = true;
    }
    calculationHistory1.QuotaSize = new long?(licensingManager.GetDbSizeQuota());
    calculationHistory1.UsedByCompanies = new long?(0L);
    calculationHistory1.UsedBySnapshots = new long?(0L);
    List<CommandBase> commandBaseList = new List<CommandBase>();
    foreach (PXResult<TableSize> pxResult in PXSelectBase<TableSize, PXSelectGroupBy<TableSize, Aggregate<GroupBy<TableSize.company, Sum<TableSize.fullSizeByCompany>>>>.Config>.Select(graph))
    {
      TableSize tableSize = (TableSize) pxResult;
      int? company = tableSize.Company;
      int num1 = 0;
      long? nullable1;
      long? nullable2;
      if (company.GetValueOrDefault() > num1 & company.HasValue)
      {
        SpaceUsageCalculationHistory calculationHistory2 = calculationHistory1;
        nullable1 = calculationHistory2.UsedByCompanies;
        nullable2 = tableSize.FullSizeByCompany;
        calculationHistory2.UsedByCompanies = nullable1.HasValue & nullable2.HasValue ? new long?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new long?();
      }
      else
      {
        SpaceUsageCalculationHistory calculationHistory3 = calculationHistory1;
        nullable2 = calculationHistory3.UsedBySnapshots;
        nullable1 = tableSize.FullSizeByCompany;
        calculationHistory3.UsedBySnapshots = nullable2.HasValue & nullable1.HasValue ? new long?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new long?();
      }
      CmdUpdate cmdUpdate1 = new CmdUpdate(YaqlSchemaTable.op_Implicit("Company"), (IEnumerable<YaqlJoin>) null).set<long?>("Size", tableSize.FullSizeByCompany);
      YaqlColumn yaqlColumn = Yaql.column("CompanyID", (string) null);
      company = tableSize.Company;
      int num2 = company.Value;
      YaqlCondition yaqlCondition = Yaql.eq<int>((YaqlScalar) yaqlColumn, num2);
      CmdUpdate cmdUpdate2 = cmdUpdate1.where(yaqlCondition);
      commandBaseList.Add((CommandBase) cmdUpdate2);
    }
    SpaceUsageMaint._point.executeCommands((IEnumerable<CommandBase>) commandBaseList, new ExecutionContext((IExecutionObserver) new SimpleExecutionObserver()), false);
    calculationHistory1.CalculationDate = new System.DateTime?(System.DateTime.Now);
    if (flag)
    {
      SpaceUsageCalculationHistory row = (SpaceUsageCalculationHistory) graph.Caches[typeof (SpaceUsageCalculationHistory)].Insert((object) calculationHistory1);
      graph.Caches[typeof (SpaceUsageCalculationHistory)].PersistInserted((object) row);
    }
    else
    {
      SpaceUsageCalculationHistory row = (SpaceUsageCalculationHistory) graph.Caches[typeof (SpaceUsageCalculationHistory)].Update((object) calculationHistory1);
      graph.Caches[typeof (SpaceUsageCalculationHistory)].PersistUpdated((object) row);
    }
  }

  protected virtual void SpaceUsageCalculationHistory_CalculationDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) System.DateTime.MinValue;
  }

  protected virtual void SpaceUsageCalculationHistory_QuotaSize_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    SpaceUsageCalculationHistory row = (SpaceUsageCalculationHistory) e.Row;
    if (row == null)
      return;
    e.ReturnState = (object) PXStringState.CreateInstance((object) SpaceUsageMaint.FormatSize((double) row.QuotaSize.GetValueOrDefault()), new int?(), new bool?(true), typeof (SpaceUsageCalculationHistory.quotaSize).Name, new bool?(false), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null);
  }

  protected virtual void SpaceUsageCalculationHistory_UsedTotal_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    SpaceUsageCalculationHistory row = (SpaceUsageCalculationHistory) e.Row;
    if (row == null)
      return;
    e.ReturnState = (object) PXStringState.CreateInstance((object) SpaceUsageMaint.FormatSize((double) row.UsedTotal.GetValueOrDefault()), new int?(), new bool?(true), typeof (SpaceUsageCalculationHistory.usedTotal).Name, new bool?(false), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null);
  }

  protected virtual void SpaceUsageCalculationHistory_UsedByCompanies_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    SpaceUsageCalculationHistory row = (SpaceUsageCalculationHistory) e.Row;
    if (row == null)
      return;
    e.ReturnState = (object) PXStringState.CreateInstance((object) SpaceUsageMaint.FormatSize((double) row.UsedByCompanies.GetValueOrDefault()), new int?(), new bool?(true), typeof (SpaceUsageCalculationHistory.usedByCompanies).Name, new bool?(false), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null);
  }

  protected virtual void SpaceUsageCalculationHistory_UsedBySnapshots_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    SpaceUsageCalculationHistory row = (SpaceUsageCalculationHistory) e.Row;
    if (row == null)
      return;
    e.ReturnState = (object) PXStringState.CreateInstance((object) SpaceUsageMaint.FormatSize((double) row.UsedBySnapshots.GetValueOrDefault()), new int?(), new bool?(true), typeof (SpaceUsageCalculationHistory.usedBySnapshots).Name, new bool?(false), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null);
  }

  protected virtual void SpaceUsageCalculationHistory_FreeSpace_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    SpaceUsageCalculationHistory row = (SpaceUsageCalculationHistory) e.Row;
    if (row == null)
      return;
    e.ReturnState = (object) PXStringState.CreateInstance((object) SpaceUsageMaint.FormatSize((double) row.FreeSpace.GetValueOrDefault()), new int?(), new bool?(true), typeof (SpaceUsageCalculationHistory.freeSpace).Name, new bool?(false), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null);
  }

  protected virtual void SpaceUsageCalculationHistory_CurrentStatus_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    SpaceUsageCalculationHistory row = (SpaceUsageCalculationHistory) e.Row;
    if (row == null)
      return;
    string empty = string.Empty;
    long num = row.CurrentStatus ?? -1L;
    string str;
    if (num < 0L)
      str = PXMessages.LocalizeFormatNoPrefix("Database space limit is not set");
    else if (num > 100L)
      str = PXMessages.LocalizeFormatNoPrefix("Database space limit is exceeded ({0}% is used)", (object) num);
    else
      str = PXMessages.LocalizeFormatNoPrefix("OK ({0}% is used)", (object) num);
    e.ReturnState = (object) PXStringState.CreateInstance((object) str, new int?(), new bool?(true), typeof (SpaceUsageCalculationHistory.currentStatus).Name, new bool?(false), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null);
  }

  protected virtual void CompanyByTableSize_Type_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    CompanyByTableSize row = (CompanyByTableSize) e.Row;
    if (row == null)
      return;
    int? nullable = row.Company;
    int num = 0;
    string str1 = !(nullable.GetValueOrDefault() < num & nullable.HasValue) ? "Tenant" : "Snapshot";
    PXFieldSelectingEventArgs selectingEventArgs = e;
    string str2 = str1;
    nullable = new int?();
    int? length = nullable;
    bool? isUnicode = new bool?(true);
    string name = typeof (CompanyByTableSize.type).Name;
    bool? isKey = new bool?(false);
    nullable = new int?();
    int? required = nullable;
    bool? exclusiveValues = new bool?();
    PXFieldState instance = PXStringState.CreateInstance((object) str2, length, isUnicode, name, isKey, required, (string) null, (string[]) null, (string[]) null, exclusiveValues, (string) null);
    selectingEventArgs.ReturnState = (object) instance;
  }

  protected virtual void CompanyByTableSize_Name_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    CompanyByTableSize row = (CompanyByTableSize) e.Row;
    if (row == null)
      return;
    int? company = row.Company;
    int num = 0;
    string str;
    if (company.GetValueOrDefault() < num & company.HasValue)
    {
      YaqlTableQuery yaqlTableQuery = new YaqlTableQuery((YaqlTable) Yaql.schemaTable("TableSize", "t"), (YaqlJoin) Yaql.innerJoin((YaqlTable) Yaql.schemaTable("UPSnapshot", "s"), Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column("Company", "t"), Yaql.column("LinkedCompany", "s"))), (string) null);
      ((YaqlQueryBase) yaqlTableQuery).where(Yaql.eq<int?>((YaqlScalar) Yaql.column("Company", (string) null), row.Company));
      Dictionary<string, string> columnNames = new Dictionary<string, string>()
      {
        {
          "Name",
          "s"
        }
      };
      foreach (string key in columnNames.Keys)
        yaqlTableQuery.Columns.Add(new YaqlScalarAlilased((YaqlScalar) Yaql.column(key, columnNames[key]), (string) null));
      List<TableColumn> list = SpaceUsageMaint._point.Schema.GetTable("TableSize").Columns.Where<TableColumn>((Func<TableColumn, bool>) (col => columnNames.Keys.Where<string>((Func<string, bool>) (n => columnNames[n] == "t")).Contains<string>(((TableEntityBase) col).Name))).ToList<TableColumn>();
      list.AddRange(SpaceUsageMaint._point.Schema.GetTable("UPSnapshot").Columns.Where<TableColumn>((Func<TableColumn, bool>) (col => columnNames.Keys.Where<string>((Func<string, bool>) (n => columnNames[n] == "s")).Contains<string>(((TableEntityBase) col).Name))));
      object[] objArray = SpaceUsageMaint._point.selectTable(yaqlTableQuery, list, (SqlGenerationOptions) null).FirstOrDefault<object[]>();
      str = objArray != null ? (string) objArray[0] : (string) null;
    }
    else
    {
      YaqlTableQuery yaqlTableQuery = new YaqlTableQuery((YaqlTable) Yaql.schemaTable("TableSize", "t"), (YaqlJoin) Yaql.innerJoin((YaqlTable) Yaql.schemaTable("Company", "c"), Yaql.eq<YaqlColumn>((YaqlScalar) Yaql.column("Company", "t"), Yaql.column("CompanyID", "c"))), (string) null);
      ((YaqlQueryBase) yaqlTableQuery).where(Yaql.eq<int?>((YaqlScalar) Yaql.column("Company", (string) null), row.Company));
      Dictionary<string, string> columnNames = new Dictionary<string, string>()
      {
        {
          "CompanyCD",
          "c"
        }
      };
      foreach (string key in columnNames.Keys)
        yaqlTableQuery.Columns.Add(new YaqlScalarAlilased((YaqlScalar) Yaql.column(key, columnNames[key]), (string) null));
      List<TableColumn> list = SpaceUsageMaint._point.Schema.GetTable("TableSize").Columns.Where<TableColumn>((Func<TableColumn, bool>) (col => columnNames.Keys.Where<string>((Func<string, bool>) (n => columnNames[n] == "t")).Contains<string>(((TableEntityBase) col).Name))).ToList<TableColumn>();
      list.AddRange(SpaceUsageMaint._point.Schema.GetTable("Company").Columns.Where<TableColumn>((Func<TableColumn, bool>) (col => columnNames.Keys.Where<string>((Func<string, bool>) (n => columnNames[n] == "c")).Contains<string>(((TableEntityBase) col).Name))));
      object[] objArray = SpaceUsageMaint._point.selectTable(yaqlTableQuery, list, (SqlGenerationOptions) null).FirstOrDefault<object[]>();
      str = objArray != null ? (string) objArray[0] : (string) null;
    }
    if (str == null)
      str = "[Orphaned]";
    e.ReturnState = (object) PXStringState.CreateInstance((object) str, new int?(), new bool?(true), typeof (CompanyByTableSize.type).Name, new bool?(false), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null);
  }

  protected virtual void CompanyByTableSize_Size_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if ((CompanyByTableSize) e.Row == null)
      return;
    TableSize current = this.Tables.Current;
    if (current == null)
      return;
    e.ReturnState = (object) PXDecimalState.CreateInstance((object) current.FullSizeByCompanyMB, new int?(2), typeof (CompanyByTableSize.size).Name, new bool?(), new int?(), new Decimal?(), new Decimal?());
  }

  [PXUIField(DisplayName = "Number of Records")]
  [PXDBLong]
  protected virtual void TableSize_CountOfCompanyRecords_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(DisplayName = "Size in DB (MB)", Enabled = false)]
  [PXDecimal]
  protected virtual void CompanyByTableSize_Size_CacheAttached(PXCache sender)
  {
  }

  [PXButton]
  protected virtual void viewCompany()
  {
    UPCompany row = this.Companies.Current;
    CompanyMaint instance = PXGraph.CreateInstance<CompanyMaint>();
    if (PXCompanyHelper.SelectCompanies().Where<UPCompany>((Func<UPCompany, bool>) (c =>
    {
      int? companyId1 = c.CompanyID;
      int? companyId2 = row.CompanyID;
      return companyId1.GetValueOrDefault() == companyId2.GetValueOrDefault() & companyId1.HasValue == companyId2.HasValue;
    })).FirstOrDefault<UPCompany>() == null)
      throw new PXNotEnoughRightsException(PXCacheRights.Select, "You don't have access rights for this company");
    instance.Companies.Current = (UPCompany) instance.Companies.Search<UPCompany.companyID>((object) row.CompanyID);
    if (instance.Companies.Current != null)
      throw new PXRedirectRequiredException((PXGraph) instance, true, "Companies");
  }

  [PXButton]
  [PXUIField(DisplayName = "View Tables")]
  public void viewCompanyTables()
  {
    int num = (int) this.PopupCompanyTablesDefinition.AskExt(true);
  }

  [PXButton]
  [PXUIField(DisplayName = "View Tables")]
  public void viewSnapshotTables()
  {
    int num = (int) this.PopupSnapshotTablesDefinition.AskExt(true);
  }

  [PXButton]
  [PXUIField(DisplayName = "View Used Space by Tenants and Snapshots")]
  public void viewCompaniesByTable()
  {
    int num = (int) this.PopupCompaniesByTableDefinition.AskExt(true);
  }

  [PXButton]
  protected virtual void viewSnapshot()
  {
    UPSnapshotSize row = this.Snapshots.Current;
    CompanyMaint instance = PXGraph.CreateInstance<CompanyMaint>();
    int? field0 = new int?();
    field0 = row.SourceCompany.HasValue ? (int?) PXCompanyHelper.SelectCompanies().Where<UPCompany>((Func<UPCompany, bool>) (c =>
    {
      int? companyId = c.CompanyID;
      int? sourceCompany = row.SourceCompany;
      return companyId.GetValueOrDefault() == sourceCompany.GetValueOrDefault() & companyId.HasValue == sourceCompany.HasValue;
    })).FirstOrDefault<UPCompany>()?.CompanyID : new int?(PXInstanceHelper.CurrentCompany);
    if (!field0.HasValue)
      throw new PXNotEnoughRightsException(PXCacheRights.Select, "You cannot view this snapshot on the Tenants (SM203520) form due to insufficient access rights.");
    instance.Companies.Current = (UPCompany) instance.Companies.Search<UPCompany.companyID>((object) field0);
    instance.Snapshots.Cache.ActiveRow = (IBqlTable) (UPSnapshot) instance.Snapshots.Search<UPSnapshot.snapshotID>((object) row.SnapshotID).FirstOrDefault<PXResult<UPSnapshot>>();
    if (instance.Snapshots.Cache.ActiveRow == null)
      throw new PXNotEnoughRightsException(PXCacheRights.Select, "The snapshot cannot be accessed in the current tenant. To view this snapshot on the Tenants (SM203520) form, you need to sign in to the tenant in which it was created.");
    if (instance.Companies.Current != null)
      throw new PXRedirectRequiredException((PXGraph) instance, true, "Companies");
  }

  public static string FormatSize(double size)
  {
    int y = (int) System.Math.Floor(System.Math.Log(System.Math.Abs(size), 1024.0));
    if (y < 0)
      y = 0;
    string[] strArray = new string[6]
    {
      PXLocalizer.Localize("B", typeof (Messages).FullName),
      PXLocalizer.Localize("kB", typeof (Messages).FullName),
      PXLocalizer.Localize("MB", typeof (Messages).FullName),
      PXLocalizer.Localize("GB", typeof (Messages).FullName),
      PXLocalizer.Localize("TB", typeof (Messages).FullName),
      PXLocalizer.Localize("PB", typeof (Messages).FullName)
    };
    if (y >= strArray.Length)
      y = strArray.Length - 1;
    double num = size / System.Math.Pow(1024.0, (double) y);
    if (System.Math.Abs(num) < 10.0)
      return num.ToString("0.### " + strArray[y]);
    return System.Math.Abs(num) < 100.0 ? num.ToString("00.## " + strArray[y]) : num.ToString("000.# " + strArray[y]);
  }
}
