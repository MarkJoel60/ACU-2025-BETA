// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.PXSnapshotUploader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Archiver;
using PX.BulkInsert;
using PX.BulkInsert.Provider;
using PX.BulkInsert.Provider.RowTransforms;
using PX.BulkInsert.Provider.TransferTriggers;
using PX.DbServices.Commands;
using PX.DbServices.Commands.Data;
using PX.DbServices.Model.Entities;
using PX.DbServices.Points;
using PX.DbServices.Points.DataValidator;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.Points.FileSystem;
using PX.DbServices.Points.ZipArchive;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Data.Update;

internal class PXSnapshotUploader : PXSnapshotBase
{
  protected bool IncludeCustomData;
  protected bool TestPass;
  protected byte[] _Content;
  public static Dictionary<ValiadtionIssueType, string> SnapshotValidationMessages = new Dictionary<ValiadtionIssueType, string>()
  {
    {
      (ValiadtionIssueType) 2,
      "{0}: The field value ({1}, {2}) exceeds the maximum length."
    },
    {
      (ValiadtionIssueType) 0,
      "{0}: The Null value ({1}, {2}) is not allowed."
    }
  };
  private const int MaxMessagesPerTable = 4;

  public PXSnapshotUploader(
    UPCompany company,
    byte[] content,
    bool includeCustomData = false,
    bool makeDataTestPass = false)
  {
    this.IncludeCustomData = includeCustomData;
    this.TestPass = makeDataTestPass;
    this._Company = company;
    this._Content = content;
  }

  public override void Start()
  {
    using (MemoryStream memoryStream = new MemoryStream(this._Content, false))
    {
      using (ZipArchiveWrapper zipPoint = new ZipArchiveWrapper((Stream) memoryStream))
      {
        try
        {
          this._Snapshot = PXSnapshotRestorator.ReadSnapshotFromManifest(zipPoint, true);
          if (!this.IncludeCustomData)
            this._Snapshot.Customization = "";
          this._Snapshot.SnapshotID = new Guid?(Guid.NewGuid());
        }
        catch (PXException ex)
        {
          PXUpdateLog.WriteMessage(new PXUpdateEvent(EventLogEntryType.Error, ex.MessageNoPrefix, (Exception) ex));
          throw;
        }
        catch (Exception ex)
        {
          PXUpdateLog.WriteMessage(new PXUpdateEvent(EventLogEntryType.Error, "Invalid snapshot format", ex));
          throw new PXException("Invalid snapshot format", ex);
        }
        PointDbmsBase point = PXDatabase.Provider.CreateDbServicesPoint();
        if (this.TestPass)
        {
          PointDataValidator pointDataValidator = new PointDataValidator(point.Schema);
          this.UploadSnapshot(zipPoint, (Point) pointDataValidator, (FileFormat) 3, (FileFormat) 1);
          bool flag = false;
          StringBuilder stringBuilder = new StringBuilder("The data from the snapshot cannot be uploaded:");
          stringBuilder.AppendLine();
          foreach (KeyValuePair<string, ValidationReport> allProblem in pointDataValidator.AllProblems)
          {
            flag = true;
            int num = 0;
            foreach (ValidationProblem problem in allProblem.Value.Problems)
            {
              stringBuilder.AppendFormat(PXSnapshotUploader.SnapshotValidationMessages[problem.Type], (object) allProblem.Key, (object) ((TableEntityBase) problem.relatedColumn).Name, (object) problem.iRow);
              stringBuilder.AppendLine();
              ++num;
              if (num >= 4)
              {
                stringBuilder.AppendFormat(" +{0} more similar issue(s) in table {1}", (object) (allProblem.Value.TotalProblemsCount - num), (object) allProblem.Key);
                stringBuilder.AppendLine();
                break;
              }
            }
          }
          if (flag)
            throw new PXOperationCompletedWithErrorException(stringBuilder.ToString());
        }
        PXDatabase.Provider.DatabaseOperation((System.Action) (() => this.UploadSnapshot(zipPoint, (Point) point, (FileFormat) 3, (FileFormat) 1)));
        memoryStream.Seek(0L, SeekOrigin.Begin);
        SnapshotDownloader.Write((Stream) memoryStream, this._Snapshot.SnapshotID.Value);
      }
    }
    this.SaveSnapshot();
  }

  protected void UploadSnapshot(
    ZipArchiveWrapper zip,
    Point point,
    FileFormat readFormats = 1,
    FileFormat writeFormats = 1)
  {
    IEnumerable<string> strings = PointFileSystem.formats.Where<KeyValuePair<FileFormat, FileFormatHandler>>((Func<KeyValuePair<FileFormat, FileFormatHandler>, bool>) (f => ((Enum) (object) readFormats).HasFlag((Enum) (object) f.Key))).Select<KeyValuePair<FileFormat, FileFormatHandler>, string>((Func<KeyValuePair<FileFormat, FileFormatHandler>, string>) (y => y.Value.defaultExtension));
    string[] strArray = new string[1]{ "Manifest.xml" };
    List<string> list = zip.EnumerateTables(strings, strArray).ToList<string>();
    string str1 = list.FirstOrDefault<string>((Func<string, bool>) (e => PXLocalesProvider.CollationComparer.Equals(e, "Company")));
    if (str1 != null)
      list.Remove(str1);
    HashSet<string> stringSet1 = new HashSet<string>(PXDatabase.Provider.SchemaCache.GetTableNames(), (IEqualityComparer<string>) PXLocalesProvider.CollationComparer);
    HashSet<string> stringSet2 = new HashSet<string>();
    for (int index = list.Count - 1; index >= 0; --index)
    {
      string str2 = list[index];
      if (!stringSet1.Contains(str2))
      {
        if ((!this.IncludeCustomData ? 0 : (str2.StartsWith("Usr", StringComparison.OrdinalIgnoreCase) ? 1 : 0)) != 0)
        {
          stringSet2.Add(str2);
          PXTrace.WithSourceLocation(nameof (UploadSnapshot), "C:\\build\\code_repo\\NetTools\\PX.Data\\Update\\Snapshots\\PXSnapshotUploader.cs", 138).Warning<string>("{Warning}", PXMessages.LocalizeFormatNoPrefix("Your database does not contain the custom table '{0}' defined in the snapshot. This table will be created.", (object) str2));
        }
        else
        {
          PXTrace.WithSourceLocation(nameof (UploadSnapshot), "C:\\build\\code_repo\\NetTools\\PX.Data\\Update\\Snapshots\\PXSnapshotUploader.cs", 140).Warning<string>("{Warning}", PXMessages.LocalizeFormatNoPrefix("Your database does not contain the table '{0}' defined in the snapshot; this table won't be imported.", (object) str2));
          list.RemoveAt(index);
        }
      }
    }
    BatchTransferExecutorSync transferExecutorSync = new BatchTransferExecutorSync((DataTransferObserver) new DtObserver(), (string) null);
    DbmsMaintenance maintenance = point is PointDbmsBase ? PXDatabase.Provider.GetMaintenance((PointDbmsBase) point) : (DbmsMaintenance) null;
    try
    {
      int num1 = -99899;
      lock (PXSnapshotBase.LOCKER)
      {
        if (!this._Snapshot.LinkedCompany.HasValue && maintenance != null)
          this._Snapshot.LinkedCompany = new int?(PXSnapshotBase.getNextFreeIdForCompany((PointDbmsBase) point, this._Company.CompanyID.Value));
        int? nullable1 = this._Snapshot.LinkedCompany;
        num1 = nullable1 ?? num1;
        if (str1 != null)
        {
          TransferTableTask transferTableTask = new TransferTableTask()
          {
            Destination = point.GetTable(str1, FileMode.Open)
          };
          transferTableTask.Destination.UseNativeImport = true;
          transferTableTask.Source = (ITableAdapter) new ZipTableAdapter(str1, zip, readFormats, writeFormats, (string) null)
          {
            ImposedHeader = transferTableTask.Destination.Header
          };
          List<IRowTransform> transforms = transferTableTask.Transforms;
          CidPidTransform cidPidTransform = new CidPidTransform(num1);
          nullable1 = PXCompanyHelper.FindCompany(PXInstanceHelper.CurrentCompany).ParentID;
          cidPidTransform.pid = nullable1.Value;
          transforms.Add((IRowTransform) cidPidTransform);
          transferTableTask.Transforms.Add((IRowTransform) new ColumnsMatchByName(this.IncludeCustomData, transferTableTask.Destination.Header));
          transferExecutorSync.Tasks.Enqueue(transferTableTask);
          transferExecutorSync.StartSync();
        }
        else if (maintenance != null)
        {
          DbmsMaintenance dbmsMaintenance = maintenance;
          int num2 = num1;
          string name1 = this._Snapshot.Name;
          nullable1 = PXCompanyHelper.FindCompany(PXInstanceHelper.CurrentCompany).ParentID;
          int? nullable2 = new int?(nullable1.Value);
          string name2 = PXDataTypesHelper.UserCompany.Name;
          dbmsMaintenance.InsertCompanyRecord(num2, name1, nullable2, name2, false, "Company");
        }
      }
      bool needMetadataUpdate = false;
      foreach (string str3 in list)
      {
        if (!str3.EndsWith("KvExt") || this.IncludeCustomData)
        {
          stringSet2.Contains(str3);
          TransferTableTask transferTableTask = new TransferTableTask()
          {
            Destination = point.GetTable(str3, FileMode.Open)
          };
          transferTableTask.Source = (ITableAdapter) new ZipTableAdapter(str3, zip, readFormats, writeFormats, this.TempFolder)
          {
            ImposedHeader = transferTableTask.Destination.Header
          };
          transferTableTask.Transforms.Add((IRowTransform) new CidTransform(num1));
          transferTableTask.Transforms.Add((IRowTransform) new ColumnsMatchByName(this.IncludeCustomData, transferTableTask.Destination.Header));
          if (this.IncludeCustomData && point is PointDbmsBase)
            transferTableTask.Triggers.Add((ITransferTrigger) new CreateColumnsTrigger(transferTableTask.Destination.Header, point as PointDbmsBase, (System.Action<string>) (tn => needMetadataUpdate = true)));
          transferTableTask.Destination.UseNativeImport = true;
          transferExecutorSync.Tasks.Enqueue(transferTableTask);
        }
      }
      transferExecutorSync.StartSync();
      if (!needMetadataUpdate)
        return;
      PXDatabase.Provider.SchemaCacheInvalidate();
    }
    catch
    {
      if (maintenance == null)
        return;
      try
      {
        maintenance.DeleteCompany(this._Snapshot.LinkedCompany.Value, (DeleteCompanyPreservation) 1, (Dictionary<string, TableForSnapshot>) null);
      }
      catch
      {
      }
      throw;
    }
    finally
    {
      if (point is PointDbmsBase)
        (point as PointDbmsBase).executeSingleCommand((CommandBase) new CmdTruncate("CustPublishideScripts"), new ExecutionContext((IExecutionObserver) null), false);
      if (Directory.Exists(this.TempFolder))
        Directory.Delete(this.TempFolder, true);
    }
  }

  private void SaveSnapshot()
  {
    SpaceUsageMaint.CalculateSpaceUsage();
    CompanyMaint instance = PXGraph.CreateInstance<CompanyMaint>();
    this._Snapshot = instance.Snapshots.Insert(this._Snapshot);
    instance.Actions.PressSave();
  }
}
