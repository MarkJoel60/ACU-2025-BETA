// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXSnapshotCreator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Archiver;
using PX.BulkInsert;
using PX.BulkInsert.DataTransfer;
using PX.BulkInsert.DataTransfer.RowTransforms;
using PX.BulkInsert.Provider;
using PX.BulkInsert.Provider.RowTransforms;
using PX.BulkInsert.Tools;
using PX.Common;
using PX.Data.Maintenance;
using PX.Data.Maintenance.TenantShapshotDeletion;
using PX.Data.Maintenance.TenantShapshotDeletion.DAC;
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
using PX.Logging.Sinks.SystemEventsDbSink;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Xml;

#nullable disable
namespace PX.Data.Update;

internal class PXSnapshotCreator : PXSnapshotBase
{
  protected string ExportMode;
  protected ExportSnapshotSettings _Settings;
  protected IStorageProvider _Provider;
  protected Func<PointDbmsBase> fnCreatePoint;

  public PXSnapshotCreator(UPCompany company, ExportSnapshotSettings settings)
  {
    this._Company = company;
    this._Settings = settings;
    this.ExportMode = settings.ExportMode;
    this.fnCreatePoint = (Func<PointDbmsBase>) (() => PXDatabase.Provider.CreateDbServicesPoint());
    if (!this._Settings.Prepare.HasValue)
      return;
    this._Provider = PXStorageHelper.GetProvider();
  }

  public PXSnapshotCreator(UPCompany company, UPSnapshot snapshot)
  {
    this._Company = company;
    this._Snapshot = snapshot;
    this.ExportMode = snapshot.ExportMode;
    this.fnCreatePoint = (Func<PointDbmsBase>) (() => PXDatabase.Provider.CreateDbServicesPoint());
    this._Provider = PXStorageHelper.GetProvider();
  }

  public override void Start() => this.Start((FileFormat) 1);

  public void Start(FileFormat format0)
  {
    try
    {
      PXGraph instance = PXGraph.CreateInstance<PXGraph>();
      PointDbmsBase pointDbmsBase = this.fnCreatePoint();
      Dictionary<string, TableForSnapshot> exportTables = EnumerableExtensions.ToDictionary<string, TableForSnapshot>(this.CollectExportScenariosInXml(pointDbmsBase, this.ExportMode, true, this._Company.CompanyID.Value).Where<KeyValuePair<string, TableForSnapshot>>((System.Func<KeyValuePair<string, TableForSnapshot>, bool>) (v => !v.Value.ExcludeTable)));
      long predictedSize = 0;
      SpaceUsageCalculationHistory calculationHistory = (SpaceUsageCalculationHistory) null;
      if (SpaceUsageMaint.CalculateSpaceUsage())
        calculationHistory = SpaceUsageMaint.GetCalculatedSpaceUsage();
      if (calculationHistory != null)
      {
        foreach (PXResult<TableSize> pxResult in new PXSelect<TableSize, Where<TableSize.company, Equal<Required<TableSize.company>>>>(instance).Select((object) this._Company.CompanyID).AsEnumerable<PXResult<TableSize>>().Where<PXResult<TableSize>>((System.Func<PXResult<TableSize>, bool>) (s => exportTables.Keys.Contains<string>(((TableSize) s).TableName))))
        {
          TableSize tableSize = (TableSize) pxResult;
          predictedSize += tableSize.FullSizeByCompany.GetValueOrDefault();
        }
        long? freeSpace = calculationHistory.FreeSpace;
        long num = predictedSize;
        if (freeSpace.GetValueOrDefault() <= num & freeSpace.HasValue && this._Settings != null)
          throw new PXException("The system cannot create a snapshot because the database size will exceed your limit. You can view used database size and limit on the Space Usage (SM203525) form.");
      }
      this.InitialiseSnapshot();
      try
      {
        UPSnapshot snapshot = this._Snapshot;
        if ((snapshot != null ? (snapshot.SnapshotID.HasValue ? 1 : 0) : 0) != 0)
        {
          if (this._Provider != null)
          {
            if (this._Provider.Exists(this._Snapshot.SnapshotID.Value))
              this._Provider.Delete(this._Snapshot.SnapshotID.Value);
          }
        }
      }
      catch (Exception ex)
      {
        PXTrace.WriteError(ex);
        object[] objArray = Array.Empty<object>();
        throw new PXException(ex, "The LocalStorage provider is not available. Please check the storage settings on the Update Preferences (SM203505) form.", objArray);
      }
      if (this._Settings != null)
        this.CreateSnapshot(pointDbmsBase, exportTables, predictedSize, instance);
    }
    catch (Exception ex) when (ex.GetBaseException().GetType() == typeof (ThreadAbortException))
    {
      if (!ex.Data.Contains((object) "EventID") || ex.Data.Contains((object) "EventID") && ex.Data[(object) "EventID"] != (object) "System_SnapshotCreationFailedEventId")
        PXTrace.Logger.ForSystemEvents("System", "System_SnapshotCreationFailedEventId").ForContext("ContextScreenId", (object) "SM203520", false).Error<string, string>(ex, "Failed to create a snapshot SourceTenant:{SourceTenant}, Snapshot:{Snapshot}", this._Settings?.Company, this._Settings?.Name);
      throw;
    }
    if (this._Settings == null)
    {
      this.PrepareSnapshot(format0);
    }
    else
    {
      bool? prepare = this._Settings.Prepare;
      bool flag = true;
      if (!(prepare.GetValueOrDefault() == flag & prepare.HasValue))
        return;
      FileFormat result;
      this.PrepareSnapshot(Enum.TryParse<FileFormat>(this._Settings.PrepareMode, true, out result) ? result : (FileFormat) (object) 1);
    }
  }

  protected virtual void InitialiseSnapshot()
  {
    if (this._Snapshot != null)
      return;
    System.DateTime utcNow = System.DateTime.UtcNow;
    this._Snapshot = new UPSnapshot();
    this._Snapshot.SnapshotID = new Guid?(Guid.NewGuid());
    this._Snapshot.Name = $"{this._Settings.Name}_{utcNow.Year:0000}-{utcNow.Month:00}-{utcNow.Day:00}_{utcNow.Hour:00}-{utcNow.Minute:00}";
    this._Snapshot.Description = this._Settings.Description;
    this._Snapshot.Customization = PXInstanceHelper.PublishedCustomizations;
    this._Snapshot.Date = new System.DateTime?(PXTimeZoneInfo.Now);
    this._Snapshot.SourceCompany = this._Company.CompanyID;
    this._Snapshot.ExportMode = this._Settings.ExportMode;
    this._Snapshot.Host = PXInstanceHelper.HostName;
    UPSnapshotHistory restoredSnapshot = CompanyMaint.GetLastRestoredSnapshot();
    if (restoredSnapshot != null)
    {
      bool? isSafe = restoredSnapshot.IsSafe;
      if (isSafe.HasValue)
      {
        isSafe = restoredSnapshot.IsSafe;
        if (!isSafe.Value)
        {
          this._Snapshot.IsSafe = new bool?(false);
          goto label_7;
        }
      }
    }
    this._Snapshot.IsSafe = new bool?(PXSiteLockout.GetStatus(true) == PXSiteLockout.Status.Locked);
label_7:
    CompanyInfo companyInfo = ((IEnumerable<CompanyInfo>) PXDatabase.Provider.SelectCompanies(false)).FirstOrDefault<CompanyInfo>((System.Func<CompanyInfo, bool>) (ci =>
    {
      int companyId = ci.CompanyID;
      int? parentId = this._Company.ParentID;
      int valueOrDefault = parentId.GetValueOrDefault();
      return companyId == valueOrDefault & parentId.HasValue;
    }));
    if (companyInfo == null || companyInfo.CurrentDataType == PXDataTypesHelper.SystemCompany)
      return;
    this._Snapshot.MasterCompany = PXCompanyHelper.FindCompany(PXCompanySelectOptions.All, this._Company.ParentID.Value).Description;
  }

  protected void CreateSnapshot(
    PointDbmsBase dest,
    Dictionary<string, TableForSnapshot> exportTables,
    long predictedSize,
    PXGraph graph)
  {
    try
    {
      lock (PXSnapshotBase.LOCKER)
      {
        int? nullable1 = this._Snapshot.LinkedCompany;
        if (!nullable1.HasValue)
        {
          UPSnapshot snapshot = this._Snapshot;
          PointDbmsBase point = dest;
          nullable1 = this._Company.CompanyID;
          int companyId = nullable1.Value;
          int? nullable2 = new int?(PXSnapshotBase.getNextFreeIdForCompany(point, companyId));
          snapshot.LinkedCompany = nullable2;
        }
        TableHeader table = dest.Schema.GetTable("Company");
        CmdInsertSelect cmdInsertSelect1 = new CmdInsertSelect(table, table, (string) null);
        Dictionary<string, YaqlScalar> assignValues = cmdInsertSelect1.AssignValues;
        nullable1 = this._Snapshot.LinkedCompany;
        YaqlScalar yaqlScalar = Yaql.constant<int>(nullable1.Value, SqlDbType.Variant);
        assignValues.Add("CompanyID", yaqlScalar);
        cmdInsertSelect1.AssignValues.Add("Size", Yaql.constant<long>(predictedSize, SqlDbType.Variant));
        YaqlColumn yaqlColumn = Yaql.column("CompanyID", (string) null);
        nullable1 = this._Company.CompanyID;
        int num = nullable1.Value;
        cmdInsertSelect1.Condition = Yaql.eq<int>((YaqlScalar) yaqlColumn, num);
        CmdInsertSelect cmdInsertSelect2 = cmdInsertSelect1;
        dest.executeSingleCommand((CommandBase) cmdInsertSelect2, new ExecutionContext((IExecutionObserver) null), false);
      }
      int num1 = this._Company.CompanyID.Value;
      byte[] numArray = new byte[0];
      List<CompanyHeader> companies = dest.getCompanies(true);
      List<CompanyHeader> companyHeaderList = companies;
      CompanyHeader.getParentsChain(num1, companyHeaderList);
      BatchTransferExecutorSync transferExecutorSync = new BatchTransferExecutorSync((DataTransferObserver) new DtObserver(), "Create snapshot");
      bool disableFullText = true;
      foreach (KeyValuePair<string, TableForSnapshot> exportTable in exportTables)
      {
        TransferTableTask transferTableTask = new TransferTableTask()
        {
          Source = (ITableAdapter) new DbTableAdapterForSnapshot(dest, exportTable.Value.TableQuery, dest.Schema.GetTable(exportTable.Key), companies, this._Company.CompanyID)
          {
            ReplaceNoteIdForRemovedRows = true
          },
          Destination = dest.GetTable(exportTable.Key, FileMode.Open)
        };
        if (exportTable.Value.Transform != null)
          transferTableTask.Transforms.Add((IRowTransform) exportTable.Value.Transform);
        transferTableTask.Transforms.Add((IRowTransform) new CidTransform(this._Snapshot.LinkedCompany.Value));
        TableHeader table = dest.Schema.GetTable(exportTable.Key);
        TableIndex fti = table.Indices.FirstOrDefault<TableIndex>((System.Func<TableIndex, bool>) (x => x.IsFullText));
        if (fti != null && fti.mssqlRelatedIndexName != null)
        {
          TableIndex tableIndex = table.Indices.FirstOrDefault<TableIndex>((System.Func<TableIndex, bool>) (x => ((TableEntityBase) x).Name.OrdinalEquals(fti.mssqlRelatedIndexName)));
          if (tableIndex != null)
          {
            disableFullText = false;
            string name = ((TableEntityBase) tableIndex.Columns[0]).Name;
            transferTableTask.Transforms.Add((IRowTransform) new AssignColumnFunction<Guid>(name, (Func<Guid>) (() => Guid.NewGuid())));
          }
        }
        transferExecutorSync.Tasks.Enqueue(transferTableTask);
      }
      PXDatabase.Provider.DatabaseOperation(new System.Action(transferExecutorSync.StartSync), disableFullText: disableFullText);
      this.SaveSnapshot(true);
      PXTrace.Logger.ForSystemEvents("System", "System_SnapshotCreatedEventId").ForContext("ContextScreenId", (object) "SM203520", false).Information<string, string>("A snapshot has been created SourceTenant:{SourceTenant}, Snapshot:{Snapshot}", this._Settings?.Company, this._Settings?.Name);
    }
    catch (Exception ex)
    {
      PXTrace.Logger.ForSystemEvents("System", "System_SnapshotCreationFailedEventId").ForContext("ContextScreenId", (object) "SM203520", false).Error<string, string>(ex, "Failed to create a snapshot SourceTenant:{SourceTenant}, Snapshot:{Snapshot}", this._Settings?.Company, this._Settings?.Name);
      ex.Data[(object) "EventID"] = (object) "System_SnapshotCreationFailedEventId";
      try
      {
        TenantSnapshotDeletion snapshotDeletion = new TenantSnapshotDeletion()
        {
          Id = new Guid?(Guid.NewGuid()),
          TenantId = this._Snapshot.LinkedCompany,
          SnapshotId = this._Snapshot.SnapshotID,
          DeletionStatus = "N",
          NoteID = new Guid?(Guid.NewGuid())
        };
        graph.Caches[typeof (TenantSnapshotDeletion)].GetExtension<PX.Data.Maintenance.TenantShapshotDeletion.DAC.Snapshot>((object) snapshotDeletion).SnapshotName = this._Snapshot.Name;
        TenantSnapshotDeletionProcess.PerformSnapshotsDeletion(new List<TenantSnapshotDeletion>()
        {
          snapshotDeletion
        }, false, new CancellationToken());
      }
      catch
      {
      }
      throw;
    }
  }

  protected void PrepareSnapshot(FileFormat ff)
  {
    if (this._Provider == null)
      throw new PXException("The storage provider is not set up.");
    this._Snapshot.Version = (string) null;
    this.SaveSnapshot(false);
    try
    {
      using (Stream stream = this._Provider.OpenWrite(this._Snapshot.SnapshotID.Value))
      {
        using (ZipArchiveWrapper zip = new ZipArchiveWrapper(stream, ZipArchiveMode.Create))
        {
          this.WriteManifest();
          this.WriteToArchive(this.fnCreatePoint(), zip, ff, ff);
        }
      }
    }
    catch
    {
      if (this._Provider.Exists(this._Snapshot.SnapshotID.Value))
        this._Provider.Delete(this._Snapshot.SnapshotID.Value);
      throw;
    }
    finally
    {
      if (Directory.Exists(this.TempFolder))
        Directory.Delete(this.TempFolder, true);
    }
    this._Snapshot.Version = IEnumerableExtensions.GetPriorityVersion(PXVersionHelper.GetDatabaseVersions()).Version;
    this.SaveSnapshot(false);
  }

  protected void WriteManifest()
  {
    using (Stream outStream = (Stream) File.OpenWrite(Path.Combine(this.TempFolder, "Manifest.xml")))
    {
      XmlDocument manifest = new XmlDocument()
      {
        XmlResolver = (XmlResolver) null
      };
      XmlElement element1 = manifest.CreateElement("packageManifest");
      manifest.AppendChild((XmlNode) element1);
      XmlElement element2 = manifest.CreateElement("generalInfo");
      XMLHelper.EnsureAttribute((XmlNode) element2, "version", IEnumerableExtensions.GetPriorityVersion(PXVersionHelper.GetDatabaseVersions()).Version);
      XMLHelper.EnsureAttribute((XmlNode) element2, "type", PXInstanceHelper.CurrentInstanceType.ToString());
      XMLHelper.EnsureAttribute((XmlNode) element2, "date", PXTimeZoneInfo.ConvertTimeToUtc(this._Snapshot.Date.Value, LocaleInfo.GetTimeZone()).ToString((IFormatProvider) CultureInfo.InvariantCulture));
      XMLHelper.EnsureAttribute((XmlNode) element2, "name", this._Snapshot.Name);
      XMLHelper.EnsureAttribute((XmlNode) element2, "description", this._Snapshot.Description);
      XMLHelper.EnsureAttribute((XmlNode) element2, "exportMode", this._Snapshot.ExportMode);
      XMLHelper.EnsureAttribute((XmlNode) element2, "host", this._Snapshot.Host);
      XMLHelper.EnsureAttribute((XmlNode) element2, "master", this._Snapshot.MasterCompany);
      XMLHelper.EnsureAttribute((XmlNode) element2, "IsSafe", this._Snapshot.IsSafe.ToString());
      XMLHelper.EnsureAttribute((XmlNode) element2, "Size", PXCompanyHelper.FindCompany(PXCompanySelectOptions.All, this._Snapshot.LinkedCompany.Value).Size.ToString());
      if (!string.IsNullOrEmpty(this._Snapshot.Customization))
        XMLHelper.EnsureAttribute((XmlNode) element2, "customization", PXInstanceHelper.PublishedCustomizations ?? "*");
      element1.AppendChild((XmlNode) element2);
      this._snapshotService.WriteManifest(manifest);
      manifest.Save(outStream);
    }
  }

  protected void WriteToArchive(
    PointDbmsBase point,
    ZipArchiveWrapper zip,
    FileFormat readFormats = 1,
    FileFormat writeFormat = 1)
  {
    BatchTransferExecutorSync transferExecutorSync = new BatchTransferExecutorSync((DataTransferObserver) new DtObserver(), (string) null);
    foreach (KeyValuePair<string, TableForSnapshot> keyValuePair in this.CollectExportScenariosInXml(point, this.ExportMode, false, this._Snapshot.LinkedCompany.Value, true))
    {
      if (!keyValuePair.Value.ExcludeTable)
      {
        TransferTableTask transferTableTask = new TransferTableTask()
        {
          Source = point.createTableAdapter(keyValuePair.Value.TableQuery, point.Schema.GetTable(keyValuePair.Key)),
          SourceFilter = Yaql.eq<int>((YaqlScalar) Yaql.column("CompanyID", (string) null), this._Snapshot.LinkedCompany.Value),
          Destination = (ITableAdapter) new ZipTableAdapter(keyValuePair.Key, zip, readFormats, writeFormat, this.TempFolder)
        };
        if (keyValuePair.Value.Transform != null)
          transferTableTask.Transforms.Add((IRowTransform) keyValuePair.Value.Transform);
        MaxDateTimeTransform dateTimeTransform = new MaxDateTimeTransform();
        transferTableTask.Transforms.Add((IRowTransform) dateTimeTransform);
        transferExecutorSync.Tasks.Enqueue(transferTableTask);
      }
    }
    transferExecutorSync.StartSync();
    string str = Path.Combine(this.SnapshotsFolder, this._Snapshot.SnapshotID.Value.ToString() + ".zip");
    zip.Compress(str, this.TempFolder, (string[]) null, CompressionLevel.Optimal);
  }

  private void SaveSnapshot(bool isNew)
  {
    CompanyMaint instance = PXGraph.CreateInstance<CompanyMaint>();
    this._Snapshot = isNew ? instance.Snapshots.Insert(this._Snapshot) : instance.Snapshots.Update(this._Snapshot);
    instance.Actions.PressSave();
    SpaceUsageMaint.CalculateSpaceUsage();
  }
}
