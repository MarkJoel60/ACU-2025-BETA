// Decompiled with JetBrains decompiler
// Type: PX.SM.SynchronizationProcess
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Data;
using PX.Data.Wiki.ExternalFiles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

#nullable disable
namespace PX.SM;

/// <exclude />
public class SynchronizationProcess : PXGraph<SynchronizationProcess>
{
  public PXCancel<SynchronizationFilter> Cancel;
  public PXFilter<SynchronizationFilter> filter;
  public PXFilteredProcessing<UploadFileWithIDSelector, SynchronizationFilter> SelectedFiles;
  protected BqlCommand SelectFilesCommand = BqlCommand.CreateInstance(typeof (PX.Data.Select<UploadFileWithIDSelector, Where<PX.SM.UploadFile.synchronizable, Equal<PX.Data.True>>, OrderBy<Asc<PX.SM.UploadFile.name>>>));

  public SynchronizationProcess()
  {
    SynchronizationFilter currentFilter = this.filter.Current;
    this.SelectedFiles.SetProcessDelegate((PXProcessingBase<UploadFileWithIDSelector>.ProcessItemDelegate) (uf => SynchronizationProcess.ProcessFile((PX.SM.UploadFile) uf, currentFilter)));
    this.SelectedFiles.SetProcessCaption("Process File");
    this.SelectedFiles.SetProcessAllCaption("Process All Files");
    this.SelectedFiles.SetSelected<UploadFileWithIDSelector.selected>();
  }

  protected virtual IEnumerable selectedFiles()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultSorted = true,
      IsResultFiltered = true
    };
    SynchronizationFilter current = this.filter.Current;
    int startRow = PXView.StartRow;
    int totalRows = 0;
    foreach (UploadFileWithIDSelector row in this.TypedViews.GetView(this.SelectFilesCommand, false).Select(PXView.Currents, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, (PXFilterRow[]) PXView.Filters, ref startRow, PXView.MaximumRows, ref totalRows))
    {
      if (!string.IsNullOrEmpty(row.SourceUri))
      {
        switch (current.Operation)
        {
          case "D":
            if (SynchronizationProcess.DownloadAllowed((PX.SM.UploadFile) row))
            {
              pxDelegateResult.Add((object) row);
              continue;
            }
            continue;
          case "U":
            if (SynchronizationProcess.UploadAllowed((PX.SM.UploadFile) row))
            {
              pxDelegateResult.Add((object) row);
              continue;
            }
            continue;
          default:
            continue;
        }
      }
    }
    PXView.StartRow = 0;
    return (IEnumerable) pxDelegateResult;
  }

  protected void SynchronizationFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    cache.IsDirty = false;
  }

  public static void ProcessFile(PX.SM.UploadFile file, SynchronizationFilter fileFilter)
  {
    switch (fileFilter.Operation)
    {
      case "D":
        SynchronizationProcess.DownloadFile(file);
        break;
      case "U":
        SynchronizationProcess.UploadFile(file);
        break;
    }
  }

  public static void DownloadFile(PX.SM.UploadFile file)
  {
    Tuple<string, FileInfo> sshCetificate = SynchronizationProcess.GetSshCetificate(file);
    IFileExchange provider = SynchronizationProcess.EnshureExchange(file.SourceType, file.SourceLogin, file.SourcePassword, sshCetificate.Item2, sshCetificate.Item1);
    IEnumerable<SynchronizationProcess.OrigUploadFileVersion> filesVersions = SynchronizationProcess.GetFilesVersions(file);
    SynchronizationProcess.DownloadFile(file, provider, filesVersions);
  }

  internal static void DownloadFile(
    PX.SM.UploadFile file,
    IFileExchange provider,
    IEnumerable<SynchronizationProcess.OrigUploadFileVersion> versions)
  {
    using (provider as IDisposable)
    {
      System.DateTime utcNow = System.DateTime.UtcNow;
      bool? sourceIsFolder = file.SourceIsFolder;
      bool flag = true;
      if (sourceIsFolder.GetValueOrDefault() == flag & sourceIsFolder.HasValue)
      {
        SynchronizationProcess.DownloadFolder(file, provider, versions);
      }
      else
      {
        try
        {
          ExternalFileInfo info = provider.AllowInfo ? provider.GetInfo(file.SourceUri) : (ExternalFileInfo) null;
          if (SynchronizationProcess.IsDownloadRequired(info, versions))
            SynchronizationProcess.DownloadSingleFile(file, provider, file.SourceUri, info);
        }
        catch (Exception ex)
        {
          if (ex.Message == "The requested URI is invalid for this FTP command.")
            SynchronizationProcess.DownloadFolder(file, provider, versions);
          else
            throw;
        }
      }
      file.SourceLastImportDate = new System.DateTime?(utcNow);
      PXDatabase.Update(typeof (PX.SM.UploadFile), (PXDataFieldParam) new PXDataFieldAssign("SourceLastImportDate", (object) utcNow), (PXDataFieldParam) new PXDataFieldRestrict("FileID", (object) file.FileID));
    }
  }

  internal static void DownloadFolder(
    PX.SM.UploadFile file,
    IFileExchange provider,
    IEnumerable<SynchronizationProcess.OrigUploadFileVersion> versions)
  {
    foreach (ExternalFileInfo info in (IEnumerable<ExternalFileInfo>) provider.ListFiles(file.SourceUri).Where<ExternalFileInfo>((Func<ExternalFileInfo, bool>) (fi => SynchronizationProcess.IsDownloadRequired(fi, versions))).OrderBy<ExternalFileInfo, System.DateTime>((Func<ExternalFileInfo, System.DateTime>) (fi => fi.Date)))
    {
      if (RegExpHelper.ValidateMask(info.Name, file.SourceMask))
        SynchronizationProcess.DownloadSingleFile(file, provider, info.FullName, info);
    }
  }

  /// <summary> Returns true if a file with the same name does not exist, or if its update date is less than that of the external file. </summary>
  internal static bool IsDownloadRequired(
    ExternalFileInfo externalVersion,
    IEnumerable<SynchronizationProcess.OrigUploadFileVersion> existingVersions)
  {
    return externalVersion == null || !existingVersions.Any<SynchronizationProcess.OrigUploadFileVersion>((Func<SynchronizationProcess.OrigUploadFileVersion, bool>) (v => v.Name.OrdinalEquals(externalVersion.Name) && SynchronizationProcess.CompareDates(externalVersion.Date, v.Timestamp)));
  }

  private static void DownloadSingleFile(
    PX.SM.UploadFile file,
    IFileExchange provider,
    string sourceUri,
    ExternalFileInfo info)
  {
    byte[] data = provider.Download(sourceUri);
    FileInfo finfo = new FileInfo(new Guid?(file.FileID.Value), new int?(), file.Name, info?.Name, (string) null, data, $"Get from {file.SourceUri}");
    UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
    if (!instance.SaveFile(finfo, FileExistsAction.CreateVersion) || !instance.dataUpdated)
      return;
    PXDatabase.Update(typeof (UploadFileRevision), (PXDataFieldParam) new PXDataFieldAssign("OriginalTimestamp", PXDbType.DateTime, (object) (info != null ? info.Date : System.DateTime.UtcNow)), (PXDataFieldParam) new PXDataFieldRestrict("FileID", PXDbType.UniqueIdentifier, (object) finfo.UID), (PXDataFieldParam) new PXDataFieldRestrict("FileRevisionID", PXDbType.Int, (object) finfo.RevisionId));
  }

  private static IEnumerable<SynchronizationProcess.OrigUploadFileVersion> GetFilesVersions(
    PX.SM.UploadFile file)
  {
    List<SynchronizationProcess.OrigUploadFileVersion> filesVersions = new List<SynchronizationProcess.OrigUploadFileVersion>();
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<UploadFileRevision>(new PXDataField("OriginalName"), new PXDataField("OriginalTimestamp"), (PXDataField) new PXDataFieldValue("FileID", PXDbType.UniqueIdentifier, (object) file.FileID)))
    {
      string name = pxDataRecord.GetString(0) ?? FileInfo.GetShortName(file.Name);
      System.DateTime? dateTime = pxDataRecord.GetDateTime(1);
      if (dateTime.HasValue)
        filesVersions.Add(new SynchronizationProcess.OrigUploadFileVersion(name, dateTime.Value));
    }
    return (IEnumerable<SynchronizationProcess.OrigUploadFileVersion>) filesVersions;
  }

  protected static bool CompareDates(System.DateTime file, System.DateTime stamp)
  {
    if (stamp.Year != file.Year)
      return stamp.Year > file.Year;
    if (stamp.Month != file.Month)
      return stamp.Month > file.Month;
    if (stamp.Day != file.Day)
      return stamp.Day > file.Day;
    if (stamp.Hour != file.Hour)
      return stamp.Hour > file.Hour;
    if (stamp.Minute != file.Minute)
      return stamp.Minute > file.Minute;
    return stamp.Second != file.Second ? stamp.Second > file.Second : System.Math.Truncate((double) stamp.Millisecond / 10.0) >= System.Math.Truncate((double) file.Millisecond / 10.0);
  }

  private static Tuple<string, FileInfo> GetSshCetificate(PX.SM.UploadFile file)
  {
    FileInfo fileInfo = (FileInfo) null;
    CetrificateFile cetrificateFile = (CetrificateFile) null;
    if (file.SourceType == "C" && !string.IsNullOrEmpty(file.SshCertificateName))
    {
      CertificateMaintenance instance1 = PXGraph.CreateInstance<CertificateMaintenance>();
      PXDBCryptStringAttribute.SetDecrypted<Certificate.password>(instance1.CertificateFile.Cache, true);
      cetrificateFile = (CetrificateFile) instance1.CertificateFile.Select((object) file.SshCertificateName);
      if (cetrificateFile != null)
      {
        Guid? fileId = cetrificateFile.FileID;
        if (fileId.HasValue)
        {
          UploadFileMaintenance instance2 = PXGraph.CreateInstance<UploadFileMaintenance>();
          fileId = cetrificateFile.FileID;
          Guid fileID = fileId.Value;
          fileInfo = instance2.GetFile(fileID);
        }
      }
    }
    return Tuple.Create<string, FileInfo>(cetrificateFile?.Password, fileInfo);
  }

  private static bool IsLocalPath(Uri path)
  {
    if (path.IsLoopback)
      return true;
    string host = path.Host;
    string hostName = Dns.GetHostName();
    if (hostName.OrdinalEquals(host))
      return true;
    foreach (object hostAddress in Dns.GetHostAddresses(hostName))
    {
      if (hostAddress.ToString().OrdinalEquals(host))
        return true;
    }
    return false;
  }

  public static void UploadFile(PX.SM.UploadFile file)
  {
    if (file.SourceType.OrdinalEquals("S"))
    {
      string uriString = Path.GetFullPath(file.SourceUri);
      Uri path = new Uri(uriString);
      if (path.IsAbsoluteUri && path.IsUnc && SynchronizationProcess.IsLocalPath(path))
        uriString = path.LocalPath.Substring(path.Host.Length + 3).Replace('$', Path.VolumeSeparatorChar);
      string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
      if (uriString.StartsWith(baseDirectory, StringComparison.OrdinalIgnoreCase) && !uriString.StartsWith(Path.Combine(baseDirectory, "App_Data"), StringComparison.OrdinalIgnoreCase))
        throw new PXException("You cannot upload files to folders inside the instance folder other than the App_Data folder and its subfolders.");
    }
    Tuple<string, FileInfo> sshCetificate = SynchronizationProcess.GetSshCetificate(file);
    System.DateTime utcNow = System.DateTime.UtcNow;
    IFileExchange provider = SynchronizationProcess.EnshureExchange(file.SourceType, file.SourceLogin, file.SourcePassword, sshCetificate.Item2, sshCetificate.Item1);
    using (provider as IDisposable)
    {
      UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
      bool? sourceIsFolder = file.SourceIsFolder;
      bool flag = true;
      if (sourceIsFolder.GetValueOrDefault() == flag & sourceIsFolder.HasValue)
      {
        SynchronizationProcess.UploadFolder(file, provider, instance);
      }
      else
      {
        UploadFileRevision revision = (UploadFileRevision) PXSelectBase<UploadFileRevision, PXSelect<UploadFileRevision, Where<UploadFileRevision.fileID, Equal<Required<PX.SM.UploadFile.fileID>>, And<UploadFileRevision.fileRevisionID, Equal<Required<PX.SM.UploadFile.lastRevisionID>>>>>.Config>.SelectSingleBound((PXGraph) instance, new object[0], (object) file.FileID, (object) file.LastRevisionID);
        try
        {
          SynchronizationProcess.UploadSingleFile(file, revision, provider);
        }
        catch (Exception ex)
        {
          if (ex.Message == "The requested URI is invalid for this FTP command.")
          {
            file.SourceIsFolder = new bool?(true);
            SynchronizationProcess.UploadFolder(file, provider, instance);
          }
          else
            throw;
        }
      }
      file.SourceLastExportDate = new System.DateTime?(utcNow);
      PXDatabase.Update(typeof (PX.SM.UploadFile), (PXDataFieldParam) new PXDataFieldAssign("SourceLastExportDate", (object) utcNow), (PXDataFieldParam) new PXDataFieldRestrict("FileID", (object) file.FileID));
    }
  }

  private static void UploadSingleFile(
    PX.SM.UploadFile file,
    UploadFileRevision revision,
    IFileExchange provider)
  {
    bool? sourceIsFolder = file.SourceIsFolder;
    bool flag = true;
    string path;
    if (sourceIsFolder.GetValueOrDefault() == flag & sourceIsFolder.HasValue)
    {
      string str = file.SourceNamingFormat == "R" ? revision.FileRevisionID.Value.ToString() : revision.CreatedDateTime.Value.ToString("yyyy_MM_dd_HH_mm_ss");
      path = $"{file.SourceUri}/{Path.GetFileNameWithoutExtension(file.Name)}_{str}{Path.GetExtension(file.Name)}";
    }
    else
      path = Path.HasExtension(file.SourceUri) ? file.SourceUri : $"{file.SourceUri}/{FileInfo.GetShortName(file.Name)}";
    provider.Upload(path, revision.Data);
  }

  private static void UploadFolder(
    PX.SM.UploadFile file,
    IFileExchange provider,
    UploadFileMaintenance graph)
  {
    foreach (PXResult<UploadFileRevision> pxResult in PXSelectBase<UploadFileRevision, PXSelect<UploadFileRevision, Where<UploadFileRevision.fileID, Equal<Required<UploadFileRevision.fileID>>, And<UploadFileRevision.createdDateTime, GreaterEqual<Required<UploadFileRevision.originalTimestamp>>>>>.Config>.Select((PXGraph) graph, (object) file.FileID, (object) (file.SourceLastExportDate ?? new System.DateTime(1945, 5, 9))))
    {
      UploadFileRevision revision = (UploadFileRevision) pxResult;
      SynchronizationProcess.UploadSingleFile(file, revision, provider);
    }
  }

  public static IFileExchange EnshureExchange(
    string name,
    string login,
    string password,
    FileInfo sshCertificate = null,
    string sshPassword = null)
  {
    return FileExchangeHelper.GetExchanger(name, login, password, sshCertificate, sshPassword) ?? throw new PXException("A provider with the name {0} has not been found.", new object[1]
    {
      (object) name
    });
  }

  public static bool DownloadAllowed(PX.SM.UploadFile row)
  {
    if (row == null || !row.Synchronizable.GetValueOrDefault())
      return false;
    IFileExchange exchanger = FileExchangeHelper.GetExchanger(row.SourceType, (string) null, (string) null);
    return exchanger != null && exchanger.AllowDownload;
  }

  public static bool UploadAllowed(PX.SM.UploadFile row)
  {
    if (row == null || !row.Synchronizable.GetValueOrDefault())
      return false;
    IFileExchange exchanger = FileExchangeHelper.GetExchanger(row.SourceType, (string) null, (string) null);
    return exchanger != null && exchanger.AllowUpload;
  }

  public static bool ListingAllowed(PX.SM.UploadFile row)
  {
    if (row == null || !row.Synchronizable.GetValueOrDefault())
      return false;
    IFileExchange exchanger = FileExchangeHelper.GetExchanger(row.SourceType, (string) null, (string) null);
    return exchanger != null && exchanger.AllowListing;
  }

  /// <exclude />
  internal class OrigUploadFileVersion
  {
    public string Name { get; set; }

    public System.DateTime Timestamp { get; set; }

    public OrigUploadFileVersion(string name, System.DateTime timestamp)
    {
      this.Name = name;
      this.Timestamp = timestamp;
    }
  }
}
