// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXUpdate
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.BulkInsert;
using PX.BulkInsert.Installer;
using PX.BulkInsert.Installer.DatabaseSetup;
using PX.Data.Maintenance;
using PX.DbServices.Commands;
using PX.DbServices.Commands.Data;
using PX.DbServices.Model;
using PX.DbServices.Model.Schema;
using PX.DbServices.Points;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.Points.FileSystem;
using PX.Logging.Sinks.SystemEventsDbSink;
using PX.SM;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Data.Update;

internal class PXUpdate
{
  private readonly string appdataFolder;
  private readonly string updateFolder;
  private System.DateTime _startime;
  private System.DateTime _finishtime;
  private List<DataVersion> _fromVersions;
  private List<DataVersion> _toVersions;
  private string _hostName;
  private string _instanceID;
  private PXCheckDBResult databaseInfo;
  private PXDBVersCompareResults compareResult;
  private DatabasePayloadReader dbSetup;
  private List<PXUpdateEvent> errors = new List<PXUpdateEvent>();

  public bool ForceUpdate { get; set; }

  private bool NeedsUpdate
  {
    get
    {
      return this.ForceUpdate || this.compareResult == PXDBVersCompareResults.Early || this.compareResult == PXDBVersCompareResults.Empty;
    }
  }

  public PXUpdate(PXCheckDBResult checkResults)
  {
    this.appdataFolder = PXInstanceHelper.AppDataFolder;
    this.updateFolder = PXInstanceHelper.UpdateDataFolder;
    this.databaseInfo = checkResults;
    this.compareResult = checkResults.CompareResult;
    this._hostName = checkResults.HostName;
    this._instanceID = checkResults.InstanceID;
    this.dbSetup = new DatabasePayloadReader(this.updateFolder, "DatabaseSetup.adc", Path.Combine(this.updateFolder, "Data"));
    this._fromVersions = checkResults.DatabaseVersions.ToList<DataVersion>();
    this._toVersions = this.dbSetup.ComponentVersions;
  }

  public void Start()
  {
    PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint();
    UpdateExecutionObserver observer = new UpdateExecutionObserver(this.errors);
    DbmsMaintenance maintenance = PXDatabase.Provider.GetMaintenance(dbServicesPoint, (IExecutionObserver) observer);
    try
    {
      this.CheckState(maintenance);
      PointInstallationExtensions.ValidateSchemaBeforeUpgrade(dbServicesPoint).ToArray<string>();
      this.LockDatabase(maintenance, this._instanceID);
      dbServicesPoint.SchemaReader.OmitTriggersAndFks = false;
      dbServicesPoint.SchemaReader.ClearCache();
      DataVersion upgradeTo = (DataVersion) null;
      DataVersion upgradeFrom = (DataVersion) null;
      MaintenanceContext maintenanceContext = new MaintenanceContext((IExecutionObserver) observer);
      dbServicesPoint.executeSingleCommand((CommandBase) new CmdTruncate("DistributedCache"), (ExecutionContext) maintenanceContext, false);
      int num1 = DbmsMaintenance.UpdateStart(dbServicesPoint, (ExecutionContext) maintenanceContext, this._hostName, (IEnumerable<DataVersion>) this._toVersions, ref upgradeFrom);
      try
      {
        PXUpdate.WriteToSystemEventLog((LogEventLevel) 2, "System_DbUpgradeStartedEventId", "Database upgrade has started", upgradeFrom, upgradeTo);
        if (this.NeedsUpdate)
        {
          List<DatabaseScriptRequest> source = this.PrepareDbOperations();
          DatabaseScriptRequest databaseScriptRequest = source.FirstOrDefault<DatabaseScriptRequest>((Func<DatabaseScriptRequest, bool>) (r => r.ExecMethod == 3));
          if (databaseScriptRequest != null)
            upgradeTo = IEnumerableExtensions.GetPriorityVersion(PointFileSystem.ReadSchemaFromXml(databaseScriptRequest.FileName, (DataSchema) null).Versions);
          new ScriptExecutor(dbServicesPoint, new Action<float?, string>(this.UpdateStatus))
          {
            SqlEventHandler = PXDatabaseProvider.GetObserver((IExecutionObserver) new UpdateExecutionObserver(this.errors)),
            UpdateID = num1
          }.Execute(source);
        }
        this.CompaniesInstall(maintenance, 60, 100);
        try
        {
          int? updateId = (int?) PXDatabase.Select<UPErrors>().OrderByDescending<UPErrors, int?>((Expression<Func<UPErrors, int?>>) (e => e.UpdateID)).Take<UPErrors>(1).FirstOrDefault<UPErrors>()?.UpdateID;
          if (!this.errors.Any<PXUpdateEvent>())
          {
            if (updateId.HasValue)
            {
              int num2 = num1;
              int? nullable = updateId;
              int valueOrDefault = nullable.GetValueOrDefault();
              if (!(num2 == valueOrDefault & nullable.HasValue))
                goto label_10;
            }
            PXUpdate.WriteToSystemEventLog((LogEventLevel) 2, "System_DbUpgradeAppliedEventId", "Database upgrade has been applied successfully", upgradeFrom, upgradeTo);
            goto label_18;
          }
label_10:
          PXUpdate.WriteToSystemEventLog((LogEventLevel) 4, "System_DbUpgradeFailedEventId", "Database upgrade completed with errors", upgradeFrom, upgradeTo, (IEnumerable<PXUpdateEvent>) this.errors);
        }
        catch
        {
        }
      }
      catch (Exception ex)
      {
        PXUpdateEvent message = new PXUpdateEvent(EventLogEntryType.Error, ex);
        PXUpdateLog.WriteMessage(message);
        this.errors.Add(message);
        PXUpdate.WriteToSystemEventLog((LogEventLevel) 4, "System_DbUpgradeFailedEventId", "Database upgrade completed with errors", upgradeFrom, upgradeTo, (IEnumerable<PXUpdateEvent>) this.errors);
      }
      finally
      {
        if (upgradeTo != null)
        {
          IEnumerable<DataVersion> dataVersions = dbServicesPoint.getDataVersions(true);
          if ((dataVersions != null ? IEnumerableExtensions.GetPriorityVersion(dataVersions)?.Version : (string) null) != upgradeTo.Version)
            maintenance.execute(PointInstallationExtensions.getVersionUpdateCommand(dbServicesPoint, upgradeTo), (DbmsSession) null);
        }
        maintenance.UnlockDatabase();
        this._finishtime = System.DateTime.UtcNow;
      }
label_18:
      DbmsMaintenance.GetRidOfParentOrphants(dbServicesPoint, (ExecutionContext) maintenanceContext);
      this.PersistState();
    }
    catch (Exception ex)
    {
      PXUpdateLog.WriteMessage(ex);
      throw;
    }
    finally
    {
      PXAppRestartHelper.RestartApplication();
    }
  }

  private static void WriteToSystemEventLog(
    LogEventLevel logLevel,
    string eventId,
    string message,
    DataVersion upgradeFrom,
    DataVersion upgradeTo,
    IEnumerable<PXUpdateEvent> errors = null)
  {
    ILogger ilogger = PXTrace.Logger.ForSystemEvents("System", eventId).ForContext("ContextScreenId", (object) "SM203510", false);
    if (errors != null && errors.Any<PXUpdateEvent>())
      ilogger = ilogger.ForContext("Errors", (object) errors, true);
    ilogger.Write(logLevel, message + " FromComponent:{FromVersion_ComponentName}, FromVersion:{FromVersion}, ToComponent:{ToVersion_ComponentName}, ToVersion:{ToVersion}", new object[4]
    {
      (object) upgradeFrom?.ComponentName,
      (object) upgradeFrom?.Version,
      (object) upgradeTo?.ComponentName,
      (object) upgradeTo?.Version
    });
  }

  private void LockDatabase(DbmsMaintenance maint, string instanceId)
  {
    if (maint.IsDatabaseLocked(instanceId))
      throw new PXUnderMaintenanceException();
    this._startime = System.DateTime.UtcNow;
    maint.LockDatabase(instanceId, "Automatic Update");
    if (maint.IsDatabaseLocked(instanceId))
      throw new PXUnderMaintenanceException();
  }

  private void CheckState(DbmsMaintenance maint)
  {
    if (!Directory.Exists(this.updateFolder))
      throw new PXException("The update is not supported.");
    if (this.compareResult == PXDBVersCompareResults.Later || this.compareResult == PXDBVersCompareResults.Locked || this.compareResult == PXDBVersCompareResults.Minimum || this.compareResult == PXDBVersCompareResults.NotExist)
      throw new PXException("Wrong database");
    if (maint.IsDatabaseLocked(this._instanceID))
      throw new PXUnderMaintenanceException();
    foreach (SchemaFileInfo schemaFileInfo in this.dbSetup.SchemaFiles.Values)
    {
      if (!File.Exists(schemaFileInfo.Path) && schemaFileInfo.ExecMethod != 4 && schemaFileInfo.ExecMethod != 5 && schemaFileInfo.ExecMethod != 6)
        throw new PXException("The file {0} is not found.", new object[1]
        {
          (object) schemaFileInfo.Path
        });
    }
  }

  private void PersistState()
  {
    this.UpdateStatus(new float?(100f), "Finalizing update");
    PXDatabase.ResetCredentials();
    PXDatabase.ClearCompanyCache();
    UpdateMaint graph = new UpdateMaint();
    PXResultset<UPHistory> pxResultset = PXSelectBase<UPHistory, PXSelectOrderBy<UPHistory, OrderBy<Desc<UPHistory.updateID>>>.Config>.SelectSingleBound((PXGraph) graph, (object[]) null, (object[]) null);
    UPHistory upHistory = pxResultset != null ? (UPHistory) pxResultset : new UPHistory();
    upHistory.Finished = new System.DateTime?(this._finishtime);
    upHistory.Started = new System.DateTime?(this._startime);
    upHistory.Host = this._hostName;
    graph.UpdateHistoryRecords.Update(upHistory);
    UPHistoryComponents historyComponents = (UPHistoryComponents) PXSelectBase<UPHistoryComponents, PXSelect<UPHistoryComponents, Where<UPHistoryComponents.componentType, Equal<Required<UPHistoryComponents.componentType>>>, OrderBy<Desc<UPHistoryComponents.updateID>>>.Config>.SelectSingleBound((PXGraph) graph, (object[]) null, (object) 'P');
    if (historyComponents == null || historyComponents.FromVersion != IEnumerableExtensions.GetPrimaryVersion((IEnumerable<DataVersion>) this._fromVersions).Version || historyComponents.ToVersion != IEnumerableExtensions.GetPrimaryVersion((IEnumerable<DataVersion>) this._toVersions).Version)
    {
      historyComponents.FromVersion = IEnumerableExtensions.GetPrimaryVersion((IEnumerable<DataVersion>) this._fromVersions).Version;
      historyComponents.ToVersion = IEnumerableExtensions.GetPrimaryVersion((IEnumerable<DataVersion>) this._toVersions).Version;
    }
    PXDatabase.Update<UPHistoryComponents>((PXDataFieldParam) new PXDataFieldAssign(typeof (UPHistoryComponents.fromVersion).Name, (object) IEnumerableExtensions.GetPrimaryVersion((IEnumerable<DataVersion>) this._fromVersions).Version), (PXDataFieldParam) new PXDataFieldAssign(typeof (UPHistoryComponents.toVersion).Name, (object) IEnumerableExtensions.GetPrimaryVersion((IEnumerable<DataVersion>) this._toVersions).Version), (PXDataFieldParam) new PXDataFieldRestrict(typeof (UPHistoryComponents.updateID).Name, (object) upHistory.UpdateID), (PXDataFieldParam) new PXDataFieldRestrict(typeof (UPHistoryComponents.componentType).Name, (object) 'P'));
    foreach (PXUpdateEvent error in this.errors)
      graph.UpdateErrorRecords.Insert(new UPErrors()
      {
        Message = error.GetMessage(),
        Stack = error.GetStack(),
        Script = error.Script
      });
    graph.Actions.PressSave();
  }

  private void UpdateStatus(float? percent, string message)
  {
    PXLongOperation.SetCustomInfo((object) new PXUpdateStatus((int) percent.Value, message, (IEnumerable<PXUpdateEvent>) this.errors));
  }

  protected virtual List<DatabaseScriptRequest> PrepareDbOperations()
  {
    List<DatabaseScriptRequest> databaseScriptRequestList = new List<DatabaseScriptRequest>();
    string type = PXDatabase.Provider.GetConnectionDefinition().Type;
    foreach (SchemaFileInfo schemaFileInfo in this.dbSetup.SchemaFiles.Values)
    {
      if (schemaFileInfo.MeetsServerType(type))
        databaseScriptRequestList.Add(new DatabaseScriptRequest()
        {
          FileName = schemaFileInfo.Path,
          ExecMethod = schemaFileInfo.ExecMethod,
          Weight = (float) schemaFileInfo.Weight,
          IsCustomScript = schemaFileInfo.IsCustom
        });
    }
    return databaseScriptRequestList;
  }

  protected virtual void CompaniesInstall(DbmsMaintenance maint, int CPStartStep, int CPMaxSteps)
  {
    IEnumerable<CompanyInfo> source = PXCompanyHelper.LoadCompanies(this.dbSetup, this.compareResult == PXDBVersCompareResults.Empty);
    if (!source.Any<CompanyInfo>())
      return;
    int num1 = (int) System.Math.Floor((double) (CPMaxSteps - CPStartStep) / 6.0);
    int num2 = CPStartStep;
    bool flag = source.Any<CompanyInfo>((Func<CompanyInfo, bool>) (ci => !ci.Exist));
    if (flag || this.NeedsUpdate)
      maint.DisableFullText();
    this.UpdateStatus(new float?((float) num2), "Extending tenant masks width");
    int num3 = num2 + num1;
    maint.BeforeDataReplacement();
    maint.AdjustCompanyMaskWidth(source.Where<CompanyInfo>((Func<CompanyInfo, bool>) (ci => !ci.Exist)).Select<CompanyInfo, int>((Func<CompanyInfo, int>) (c => c.CompanyID)));
    this.UpdateStatus(new float?((float) num3), "Executing pp_BeforeDataReplacement");
    int num4 = num3 + num1;
    List<CompanyInfo> companies = new List<CompanyInfo>();
    switch (this.compareResult)
    {
      case PXDBVersCompareResults.Current:
      case PXDBVersCompareResults.Later:
      case PXDBVersCompareResults.Minimum:
        companies.AddRange(source.Where<CompanyInfo>((Func<CompanyInfo, bool>) (ci => !ci.Exist && ci.Updating || this.NeedsUpdate && ci.Updating || ci.Installing)));
        break;
      case PXDBVersCompareResults.Early:
      case PXDBVersCompareResults.Empty:
        companies.AddRange(source.Where<CompanyInfo>((Func<CompanyInfo, bool>) (ci => ci.Updating || ci.Installing)));
        break;
    }
    this.UpdateStatus(new float?((float) num4), "Inserting data");
    int num5 = num4 + num1;
    PointDbmsBase point = PXDatabase.Provider.CreateDbServicesPoint();
    point.SchemaReader.ClearCache();
    UpdateExecutionObserver executionObserver1 = new UpdateExecutionObserver(this.errors);
    BaseInstallProvider installProvider = ProviderLocator.EnumerateAll().First<DbmsProviderService>((Func<DbmsProviderService, bool>) (s => ((Enum) (object) point.Platform).HasFlag((Enum) (object) s.platform))).GetInstallProvider(point, (IExecutionObserver) executionObserver1);
    IEnumerable<MarketAdaptationManager> marketAdaptation = MarketAdaptationManager.GetMarketAdaptation(point);
    List<DataInsertionRequest> list1 = this.GetImportDataOptions((IEnumerable<CompanyInfo>) companies, marketAdaptation).Where<DataInsertionRequest>((Func<DataInsertionRequest, bool>) (dio => ((IEnumerable<int>) dio.TargetCompanies).Count<int>() > 0 && dio.HasAnyData())).ToList<DataInsertionRequest>();
    List<CompanyHeader> list2 = source.Select<CompanyInfo, CompanyHeader>((Func<CompanyInfo, CompanyHeader>) (c => new CompanyHeader()
    {
      Id = c.CompanyID,
      Cd = c.CompanyCD,
      ParentId = new int?(c.ParentID)
    })).ToList<CompanyHeader>();
    int[] array = this.NeedsUpdate ? source.Where<CompanyInfo>((Func<CompanyInfo, bool>) (ci => ci.Updating)).Select<CompanyInfo, int>((Func<CompanyInfo, int>) (c => c.CompanyID)).ToArray<int>() : (int[]) null;
    List<DataInsertionRequest> insertionRequestList = list1;
    int num6 = source.Max<CompanyInfo>((Func<CompanyInfo, int>) (c => c.CompanyID));
    List<CompanyHeader> companyHeaderList = list2;
    int[] numArray = array;
    UpdateExecutionObserver executionObserver2 = executionObserver1;
    installProvider.InsertData(insertionRequestList, num6, companyHeaderList, numArray, (IExecutionObserver) executionObserver2);
    foreach (CompanyInfo companyInfo in source)
      maint.InsertCompanyRecord(companyInfo.CompanyID, companyInfo.CompanyCD, new int?(companyInfo.ParentID), companyInfo.CurrentDataType.Name, companyInfo.Hidden, companyInfo.LoginName);
    this.UpdateStatus(new float?((float) num5), "Executing pp_AfterDataReplacement");
    int num7 = num5 + num1;
    if (this.NeedsUpdate)
      maint.AfterDataReplacement(source.Where<CompanyInfo>((Func<CompanyInfo, bool>) (ci => ci.Updating)).Select<CompanyInfo, int>((Func<CompanyInfo, int>) (c => c.CompanyID)).ToArray<int>());
    this.UpdateStatus(new float?((float) num7), "Executing pp_CorrectCompanyMask");
    if (companies.Count > 0 | flag)
      maint.CorrectCompanyMask(new int?());
    if (flag || this.NeedsUpdate)
      maint.EnableFullText();
    if (this.NeedsUpdate)
      maint.ReinitialiseVersion();
    companies.Clear();
  }

  protected virtual IEnumerable<DataInsertionRequest> GetImportDataOptions(
    IEnumerable<CompanyInfo> companies,
    IEnumerable<MarketAdaptationManager> currentLocales)
  {
    List<DataInsertionRequest> importDataOptions = new List<DataInsertionRequest>();
    foreach (DataTypeInfo filledDataType in this.dbSetup.DataTypes.FilledDataTypes)
    {
      DataTypeInfo info = filledDataType;
      DataInsertionRequest insertionRequest1 = new DataInsertionRequest(info.Name)
      {
        IncludeCustomization = info.Customization,
        FolderWithAdbs = info.Folder,
        TargetCompanies = companies.Where<CompanyInfo>((Func<CompanyInfo, bool>) (ci => object.Equals((object) ci.CurrentDataType, (object) info))).Select<CompanyInfo, int>((Func<CompanyInfo, int>) (ci => ci.CompanyID)).ToArray<int>()
      };
      importDataOptions.Add(insertionRequest1);
      if (info.Name == "System")
      {
        foreach (MarketAdaptationManager currentLocale in currentLocales)
        {
          DataInsertionRequest insertionRequest2 = new DataInsertionRequest(currentLocale.Region.Name)
          {
            IsLocale = true,
            IncludeCustomization = insertionRequest1.IncludeCustomization,
            FolderWithAdbs = Path.Combine(this.updateFolder, "LocalizationFiles", currentLocale.Region.Name),
            TargetCompanies = insertionRequest1.TargetCompanies,
            ExcludedTables = insertionRequest1.ExcludedTables,
            FolderWithDeltas = insertionRequest1.FolderWithDeltas
          };
          importDataOptions.Add(insertionRequest2);
        }
      }
    }
    return (IEnumerable<DataInsertionRequest>) importDataOptions;
  }

  protected virtual void SplitInstall(int STStartStep, int CPMaxSteps)
  {
    PXDatabase.Provider.GetMaintenance().SplitTables(PXUpdate.FillSplit().Where<SplitInfo>((Func<SplitInfo, bool>) (s => s.Changed)).ToDictionary<SplitInfo, string, int>((Func<SplitInfo, string>) (s1 => s1.Table), (Func<SplitInfo, int>) (s2 => (int) s2.Option)));
  }

  public static IEnumerable<SplitInfo> FillSplit()
  {
    foreach (string tableName in PXDatabase.Provider.GetTables().Where<string>((Func<string, bool>) (t => !t.Equals("Company", StringComparison.OrdinalIgnoreCase))))
    {
      int num = (int) PXDatabase.SelectTableSetting(tableName);
      SplitOptions splitOptions = (SplitOptions) 1;
      if (num == 0)
        splitOptions = (SplitOptions) 0;
      if (num == 1)
        splitOptions = (SplitOptions) 2;
      yield return new SplitInfo(tableName, splitOptions);
    }
  }
}
