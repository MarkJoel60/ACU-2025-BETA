// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXSnapshotRestorator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Api;
using PX.Archiver;
using PX.BulkInsert;
using PX.BulkInsert.Provider;
using PX.BulkInsert.Provider.RowTransforms;
using PX.CloudServices.Tenants;
using PX.Common;
using PX.Data.Services;
using PX.Data.Update.Storage;
using PX.DbServices.Commands;
using PX.DbServices.Commands.Data;
using PX.DbServices.Model;
using PX.DbServices.Model.Entities;
using PX.DbServices.Points;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.Points.FileSystem;
using PX.DbServices.Points.ZipArchive;
using PX.DbServices.QueryObjectModel;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

#nullable disable
namespace PX.Data.Update;

internal class PXSnapshotRestorator : PXSnapshotBase
{
  protected ImportSnapshotSettings _Settings;
  protected readonly PXDatabaseProvider _Provider;

  public PXSnapshotRestorator(
    UPCompany company,
    ImportSnapshotSettings settings,
    UPSnapshot snapshot)
  {
    this._Company = company;
    this._Settings = settings;
    this._Snapshot = snapshot;
    this._Provider = PXDatabase.Provider;
  }

  public override void Start()
  {
    SpaceUsageCalculationHistory spaceUsage = (SpaceUsageCalculationHistory) null;
    bool calculateSpaceUsage = SpaceUsageMaint.CalculateSpaceUsage();
    if (calculateSpaceUsage)
      spaceUsage = SpaceUsageMaint.GetCalculatedSpaceUsage();
    int? nullable1 = this._Snapshot.LinkedCompany;
    if (nullable1.HasValue)
    {
      nullable1 = this._Snapshot.LinkedCompany;
      if (PXCompanyHelper.FindCompany(PXCompanySelectOptions.Negative, nullable1.Value) != null)
      {
        nullable1 = this._Company.CompanyID;
        int targetCompanyID = nullable1.Value;
        nullable1 = this._Snapshot.LinkedCompany;
        int linkedCompanyID = nullable1.Value;
        long? size1 = PXCompanyHelper.FindCompany(PXCompanySelectOptions.All, linkedCompanyID).Size;
        long? size2 = PXCompanyHelper.FindCompany(PXCompanySelectOptions.All, targetCompanyID).Size;
        if (calculateSpaceUsage)
        {
          long? nullable2 = size1;
          long? nullable3 = size2;
          if (nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue && spaceUsage != null)
          {
            long? freeSpace = spaceUsage.FreeSpace;
            long? nullable4 = size1;
            long? nullable5 = size2;
            nullable2 = nullable4.HasValue & nullable5.HasValue ? new long?(nullable4.GetValueOrDefault() - nullable5.GetValueOrDefault()) : new long?();
            if (freeSpace.GetValueOrDefault() < nullable2.GetValueOrDefault() & freeSpace.HasValue & nullable2.HasValue)
              throw new PXException("The system cannot restore the snapshot because the database size will exceed your limit. You can view used database size and limit on the Space Usage (SM203525) form.");
          }
        }
        this._Provider.DatabaseOperation((System.Action) (() => this.RestoreFromLinkedCompany(targetCompanyID, linkedCompanyID)));
        this.SaveSnapshot();
        return;
      }
    }
    if (!PXStorageHelper.IsStorageSetup() || !PXStorageHelper.GetProvider().Exists(this._Snapshot.SnapshotID.Value))
      throw new PXException("The snapshot is not found.");
    nullable1 = this._Company.CompanyID;
    int targetCompanyID1 = nullable1.Value;
    this._Provider.DatabaseOperation((System.Action) (() =>
    {
      using (Stream stream = PXStorageHelper.GetProvider().OpenRead(this._Snapshot.SnapshotID.Value))
      {
        using (ZipArchiveWrapper zip = new ZipArchiveWrapper(stream))
        {
          UPSnapshot upSnapshot = PXSnapshotRestorator.ReadSnapshotFromManifest(zip, true);
          long? size3 = PXCompanyHelper.FindCompany(PXCompanySelectOptions.All, targetCompanyID1).Size;
          if (calculateSpaceUsage)
          {
            long? nullable6 = upSnapshot.Size;
            long? nullable7 = size3;
            if (nullable6.GetValueOrDefault() > nullable7.GetValueOrDefault() & nullable6.HasValue & nullable7.HasValue && spaceUsage != null)
            {
              long? freeSpace = spaceUsage.FreeSpace;
              long? size4 = upSnapshot.Size;
              long? nullable8 = size3;
              nullable6 = size4.HasValue & nullable8.HasValue ? new long?(size4.GetValueOrDefault() - nullable8.GetValueOrDefault()) : new long?();
              if (freeSpace.GetValueOrDefault() < nullable6.GetValueOrDefault() & freeSpace.HasValue & nullable6.HasValue)
                throw new PXException("The system cannot restore the snapshot because the database size will exceed your limit. You can view used database size and limit on the Space Usage (SM203525) form.");
            }
          }
          this.RestoreFromZip(zip, targetCompanyID1, -targetCompanyID1, (FileFormat) 3);
        }
      }
    }));
    this.SaveSnapshot();
  }

  protected void RestoreFromLinkedCompany(int targetCompanyID, int linkedCompanyID)
  {
    PXTransactionScope transactionScope = (PXTransactionScope) null;
    try
    {
      if (this._Settings.Transaction.GetValueOrDefault())
        transactionScope = new PXTransactionScope();
      PointDbmsBase dbServicesPoint = this._Provider.CreateDbServicesPoint(transactionScope == null ? (IDbTransaction) null : (IDbTransaction) PXTransactionScope.GetTransaction());
      dbServicesPoint.SchemaReader.ClearCache();
      dbServicesPoint.SchemaReader.OmitTriggersAndFks = false;
      DbmsMaintenance maintenance = this._Provider.GetMaintenance(dbServicesPoint, this._snapshotService is ISnapshotServiceWithLogger snapshotService1 ? snapshotService1.GetExecutionObserver(SnapshotExecutionMode.DeleteCompany) : (IExecutionObserver) null);
      Dictionary<string, TableForSnapshot> tables = this.CollectExportScenariosInXml(dbServicesPoint, this._Snapshot.ExportMode, false, linkedCompanyID);
      int sourceCompany = this._Snapshot.LinkedCompany.Value;
      System.Action<int> action = this.CaptureCloudTenantId(dbServicesPoint, sourceCompany) ?? (System.Action<int>) (_ => { });
      if (((int) this._Settings.CreateNew ?? 1) != 0)
        maintenance.DeleteCompany(targetCompanyID, (DeleteCompanyPreservation) 2, tables);
      try
      {
        int targetCompany = this._Company.CompanyID.Value;
        PxExportUtils.SelectTablesToIncludeIntoSnapshot(dbServicesPoint, (System.Func<TableHeader, bool>) (t => t.HasCompanyId()), false, new int[1]
        {
          sourceCompany
        }).ToList<TableHeader>();
        List<CommandBase> commandBaseList = new List<CommandBase>();
        List<TableColumn> columns = dbServicesPoint.Schema.GetTable("Company").Columns;
        YaqlSchemaTable yaqlSchemaTable = Yaql.schemaTable("Company", (string) null);
        YaqlTableQuery yaqlTableQuery = new YaqlTableQuery((YaqlTable) yaqlSchemaTable, (List<YaqlJoin>) null, (string) null);
        ((YaqlQueryBase) yaqlTableQuery).where(Yaql.eq<int>((YaqlScalar) Yaql.column("CompanyID", (string) null), sourceCompany));
        Dictionary<string, object> dictionary = dbServicesPoint.selectTable(yaqlTableQuery, columns, (SqlGenerationOptions) null).Select<object[], Dictionary<string, object>>((System.Func<object[], Dictionary<string, object>>) (row => ((IEnumerable<object>) row).Select((c, i) => new
        {
          name = ((TableEntityBase) columns[i]).Name,
          value = c
        }).ToDictionary(c => c.name, c => c.value, (IEqualityComparer<string>) StringComparer.CurrentCultureIgnoreCase))).FirstOrDefault<Dictionary<string, object>>();
        commandBaseList.Add((CommandBase) new CmdUpdate(yaqlSchemaTable, (IEnumerable<YaqlJoin>) null)
        {
          Condition = Yaql.eq<int>((YaqlScalar) Yaql.column("CompanyID", (string) null), targetCompany),
          AssignValues = {
            {
              "BaseCuryID",
              Yaql.constant<object>(dictionary["BaseCuryID"], SqlDbType.Variant)
            },
            {
              "BAccountID",
              Yaql.constant<object>(dictionary["BAccountID"], SqlDbType.Variant)
            },
            {
              "CountryID",
              Yaql.constant<object>(dictionary["CountryID"], SqlDbType.Variant)
            },
            {
              "PhoneMask",
              Yaql.constant<object>(dictionary["PhoneMask"], SqlDbType.Variant)
            },
            {
              "Theme",
              Yaql.constant<object>(dictionary["Theme"], SqlDbType.Variant)
            },
            {
              "IsTemplate",
              Yaql.constant<object>(dictionary["IsTemplate"], SqlDbType.Variant)
            }
          }
        });
        List<CompanyHeader> companies = dbServicesPoint.getCompanies(true);
        List<int> targetParents = CompanyHeader.getParentsChain(targetCompany, companies);
        List<int> srcParents = new List<int>();
        bool clearDest = !tables.Values.Where<TableForSnapshot>((System.Func<TableForSnapshot, bool>) (t => t.ExcludeTable || t.Restriction != null)).Any<TableForSnapshot>() && !(this._Settings.AppendData ?? false);
        CommandBase[] emptyArr = new CommandBase[0];
        TableForSnapshot tableForSnapshot;
        System.Func<TableHeader, IEnumerable<CommandBase>> func = (System.Func<TableHeader, IEnumerable<CommandBase>>) (th => ((TableEntityBase) th).Name.Equals("Company", StringComparison.OrdinalIgnoreCase) || tables.TryGetValue(((TableEntityBase) th).Name, out tableForSnapshot) && tableForSnapshot.ExcludeTable || ((TableEntityBase) th).Name.StartsWith("UPSnapshot", StringComparison.OrdinalIgnoreCase) ? (IEnumerable<CommandBase>) emptyArr : DbmsMaintenance.ScriptGenerator.copyTable(th, sourceCompany, srcParents, targetCompany, targetParents, clearDest));
        commandBaseList.AddRange((IEnumerable<CommandBase>) maintenance.processTablesWithConstraintsOff((IEnumerable<TableHeader>) dbServicesPoint.Schema, func, (DbmsMaintenance.ConstraintsOffFlags) 83));
        IEnumerable<CommandBase> collection = (IEnumerable<CommandBase>) DbmsMaintenance.ScriptGenerator.fixRowsDeletedFromSnapshotsParents(dbServicesPoint.Schema, targetCompany, sourceCompany, targetParents);
        commandBaseList.AddRange(collection);
        dbServicesPoint.executeCommands((IEnumerable<CommandBase>) commandBaseList, new ExecutionContext(this._snapshotService is ISnapshotServiceWithLogger snapshotService2 ? snapshotService2.GetExecutionObserver(SnapshotExecutionMode.RestoreFromLinkedCompany) : (IExecutionObserver) null)
        {
          TimeoutMultiplier = 120
        }, false);
        action(targetCompany);
      }
      finally
      {
        dbServicesPoint.executeSingleCommand((CommandBase) new CmdTruncate("CustPublishideScripts"), new ExecutionContext((IExecutionObserver) null), false);
        maintenance.CorrectCompanyMask(new int?(this._Company.CompanyID.Value));
      }
      transactionScope?.Complete();
    }
    finally
    {
      transactionScope?.Dispose();
    }
  }

  private System.Action<int> CaptureCloudTenantId(PointDbmsBase point, int sourceCompanyID)
  {
    return !ServiceLocator.IsLocationProviderSet ? (System.Action<int>) null : ServiceLocator.Current.GetInstance<ICloudTenantManagementService>().OnApplySnapshot(point, sourceCompanyID, this._Snapshot, this._Company);
  }

  protected void RestoreFromZip(
    ZipArchiveWrapper zip,
    int targetCompanyID,
    int negativeCompanyID,
    FileFormat readFormats)
  {
    PXTransactionScope transactionScope = (PXTransactionScope) null;
    try
    {
      bool? nullable = this._Settings.Transaction;
      if (nullable.GetValueOrDefault())
      {
        nullable = this._Settings.CreateMissingCustomObjects;
        if (!(nullable ?? false))
          transactionScope = new PXTransactionScope();
      }
      PointDbmsBase dbServicesPoint = this._Provider.CreateDbServicesPoint(transactionScope == null ? (IDbTransaction) null : (IDbTransaction) PXTransactionScope.GetTransaction());
      dbServicesPoint.SchemaReader.ClearCache();
      dbServicesPoint.SchemaReader.OmitTriggersAndFks = false;
      BatchTransferExecutorSync transferExecutorSync = new BatchTransferExecutorSync((DataTransferObserver) new DtObserver(), (string) null);
      foreach (TransferTableTask dataTransferTask in this.getDataTransferTasks(zip, transactionScope != null ? targetCompanyID : negativeCompanyID, transactionScope == null, dbServicesPoint, readFormats))
        transferExecutorSync.Tasks.Enqueue(dataTransferTask);
      List<CompanyHeader> companies = dbServicesPoint.getCompanies(true);
      DbmsMaintenance maintenance = this._Provider.GetMaintenance(dbServicesPoint);
      try
      {
        maintenance.DeleteCompany(negativeCompanyID, (DeleteCompanyPreservation) 1, (Dictionary<string, TableForSnapshot>) null);
        transferExecutorSync.StartSync();
        if (((int) this._Settings.CreateNew ?? 1) != 0)
          maintenance.DeleteCompany(targetCompanyID, (DeleteCompanyPreservation) 2, (Dictionary<string, TableForSnapshot>) null);
        List<CommandBase> commandBaseList = new List<CommandBase>();
        PxExportUtils.SelectTablesToIncludeIntoSnapshot(dbServicesPoint, (System.Func<TableHeader, bool>) (t => t.HasCompanyId()), false, new int[1]
        {
          negativeCompanyID
        }).ToList<TableHeader>().Where<TableHeader>((System.Func<TableHeader, bool>) (pair => !((TableEntityBase) pair).Name.OrdinalEquals("Company"))).Select<TableHeader, string>((System.Func<TableHeader, string>) (p => ((TableEntityBase) p).Name)).Prepend<string>("Company");
        List<int> targetParents = CompanyHeader.getParentsChain(targetCompanyID, companies);
        List<int> srcParents = new List<int>();
        System.Func<TableHeader, IEnumerable<CommandBase>> func = (System.Func<TableHeader, IEnumerable<CommandBase>>) (th => DbmsMaintenance.ScriptGenerator.copyTable(th, negativeCompanyID, srcParents, targetCompanyID, targetParents, true));
        commandBaseList.AddRange((IEnumerable<CommandBase>) maintenance.processTablesWithConstraintsOff((IEnumerable<TableHeader>) dbServicesPoint.Schema, func, (DbmsMaintenance.ConstraintsOffFlags) 67));
        commandBaseList.AddRange((IEnumerable<CommandBase>) DbmsMaintenance.ScriptGenerator.fixRowsDeletedFromSnapshotsParents(dbServicesPoint.Schema, targetCompanyID, negativeCompanyID, targetParents));
        dbServicesPoint.executeCommands((IEnumerable<CommandBase>) commandBaseList, new ExecutionContext(this._snapshotService is ISnapshotServiceWithLogger snapshotService ? snapshotService.GetExecutionObserver(SnapshotExecutionMode.RestoreFromZip) : (IExecutionObserver) null)
        {
          TimeoutMultiplier = 120
        }, false);
        maintenance.DeleteCompany(negativeCompanyID, (DeleteCompanyPreservation) 1, (Dictionary<string, TableForSnapshot>) null);
      }
      finally
      {
        maintenance.InsertCompanyRecord(this._Company.CompanyID.Value, this._Company.CompanyCD, new int?(this._Company.ParentID.Value), "US", this._Company.Hidden.GetValueOrDefault(), this._Company.LoginName);
        maintenance.CorrectCompanyMask(new int?(this._Company.CompanyID.Value));
      }
      transactionScope?.Complete();
    }
    finally
    {
      transactionScope?.Dispose();
    }
  }

  private IEnumerable<TransferTableTask> getDataTransferTasks(
    ZipArchiveWrapper zip,
    int destCompany,
    bool useNativeImport,
    PointDbmsBase point,
    FileFormat readFormats = 1)
  {
    int num = PXCompanyHelper.SelectCompanies(PXCompanySelectOptions.All).Select<UPCompany, int>((System.Func<UPCompany, int>) (c => c.CompanyID.Value)).Max();
    List<TransferTableTask> tasks = new List<TransferTableTask>();
    IEnumerable<string> strings = PointFileSystem.formats.Where<KeyValuePair<FileFormat, FileFormatHandler>>((System.Func<KeyValuePair<FileFormat, FileFormatHandler>, bool>) (f => ((Enum) (object) readFormats).HasFlag((Enum) (object) f.Key))).Select<KeyValuePair<FileFormat, FileFormatHandler>, string>((System.Func<KeyValuePair<FileFormat, FileFormatHandler>, string>) (y => y.Value.defaultExtension));
    string[] strArray = new string[1]{ "Manifest.xml" };
    foreach (string enumerateTable in zip.EnumerateTables(strings, strArray))
    {
      TransferTableTask transferTableTask = new TransferTableTask()
      {
        AppendData = this._Settings.AppendData.GetValueOrDefault(),
        Transforms = {
          (IRowTransform) new FillCidMaskTransform(num, destCompany)
        },
        Destination = point.GetTable(enumerateTable, FileMode.Open)
      };
      transferTableTask.Source = (ITableAdapter) new ZipTableAdapter(enumerateTable, zip, readFormats, (FileFormat) 1, (string) null)
      {
        ImposedHeader = transferTableTask.Destination.Header
      };
      transferTableTask.Destination.UseNativeImport = useNativeImport;
      tasks.Add(transferTableTask);
    }
    TransferTableTask tc = tasks.FirstOrDefault<TransferTableTask>((System.Func<TransferTableTask, bool>) (t => t.Source.TableName.Equals("Company")));
    if (tc != null)
      yield return tc;
    foreach (TransferTableTask dataTransferTask in tasks)
    {
      if (dataTransferTask != tc)
        yield return dataTransferTask;
    }
  }

  internal static UPSnapshot ReadSnapshotFromManifest(ZipArchiveWrapper zip, bool checkVersionMatch)
  {
    XmlDocument xmlDocument = new XmlDocument()
    {
      XmlResolver = (XmlResolver) null
    };
    using (Stream stream = zip.GetStream("Manifest.xml"))
      xmlDocument.Load(stream);
    XmlNode xmlNode = xmlDocument.SelectSingleNode("//packageManifest/generalInfo");
    XmlAttribute xmlAttribute = xmlNode != null ? xmlNode.Attributes["version"] : throw new PXException("The package is not valid.");
    XmlAttribute attribute1 = xmlNode.Attributes["type"];
    XmlAttribute attribute2 = xmlNode.Attributes["date"];
    XmlAttribute attribute3 = xmlNode.Attributes["customization"];
    XmlAttribute attribute4 = xmlNode.Attributes["name"];
    XmlAttribute attribute5 = xmlNode.Attributes["description"];
    XmlAttribute attribute6 = xmlNode.Attributes["exportMode"];
    XmlAttribute attribute7 = xmlNode.Attributes["host"];
    XmlAttribute attribute8 = xmlNode.Attributes["master"];
    XmlAttribute attribute9 = xmlNode.Attributes["IsSafe"];
    XmlAttribute attribute10 = xmlNode.Attributes["Size"];
    if (xmlAttribute == null || attribute1 == null || attribute2 == null || attribute10 == null)
      throw new PXException("The package is not valid.");
    if (string.IsNullOrEmpty(xmlAttribute.Value) || string.IsNullOrEmpty(attribute1.Value) || string.IsNullOrEmpty(attribute2.Value))
      throw new PXException("The package is not valid.");
    if (checkVersionMatch && !object.Equals((object) PXVersionHelper.Convert(xmlAttribute.Value), (object) PXVersionHelper.Convert(IEnumerableExtensions.GetPriorityVersion(PXVersionHelper.GetDatabaseVersions()).Version)))
      throw new PXException("Package version '{0}' differs from application version '{1}'.", new object[2]
      {
        (object) xmlAttribute.Value,
        (object) IEnumerableExtensions.GetPriorityVersion(PXVersionHelper.GetDatabaseVersions()).Version
      });
    if (attribute1.Value != PXInstanceHelper.CurrentInstanceType.ToString())
      throw new PXException("The package is not valid.");
    SpaceUsageCalculationHistory calculationHistory = (SpaceUsageCalculationHistory) null;
    if (SpaceUsageMaint.CalculateSpaceUsage())
      calculationHistory = SpaceUsageMaint.GetCalculatedSpaceUsage();
    if (calculationHistory != null)
    {
      long? freeSpace = calculationHistory.FreeSpace;
      long num = long.Parse(attribute10.Value);
      if (freeSpace.GetValueOrDefault() <= num & freeSpace.HasValue)
        throw new PXException("The system cannot import the snapshot because the database size will exceed your limit. You can view used database size and limit on the Space Usage (SM203525) form.");
    }
    UPSnapshot upSnapshot = new UPSnapshot();
    if (attribute3 != null && !string.IsNullOrEmpty(attribute3.Value))
      upSnapshot.Customization = attribute3.Value;
    upSnapshot.Version = xmlAttribute.Value;
    upSnapshot.Date = new System.DateTime?(PXTimeZoneInfo.ConvertTimeFromUtc(System.DateTime.Parse(attribute2.Value, (IFormatProvider) CultureInfo.InvariantCulture), LocaleInfo.GetTimeZone()));
    upSnapshot.Name = (attribute4 ?? attribute5) == null ? (string) null : (attribute4 ?? attribute5).Value;
    upSnapshot.Description = attribute5 == null ? (string) null : attribute5.Value;
    upSnapshot.ExportMode = attribute6 == null ? (string) null : attribute6.Value;
    upSnapshot.Host = attribute7 == null ? (string) null : attribute7.Value;
    upSnapshot.MasterCompany = attribute8 == null ? (string) null : attribute8.Value;
    upSnapshot.IsSafe = new bool?(attribute9 != null && bool.Parse(attribute9.Value));
    upSnapshot.Size = new long?(long.Parse(attribute10.Value));
    return upSnapshot;
  }

  private void SaveSnapshot()
  {
    SpaceUsageMaint.CalculateSpaceUsage();
    CompanyMaint instance = PXGraph.CreateInstance<CompanyMaint>();
    instance.SnapshotsHistory.Insert(new UPSnapshotHistory()
    {
      SnapshotID = this._Snapshot.SnapshotID,
      TargetCompany = this._Company.CompanyID,
      IsSafe = this._Snapshot.IsSafe
    });
    instance.Actions.PressSave();
  }
}
