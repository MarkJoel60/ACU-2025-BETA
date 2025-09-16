// Decompiled with JetBrains decompiler
// Type: PX.Reports.ReportFileWatcher
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Web.Hosting;

#nullable disable
namespace PX.Reports;

internal class ReportFileWatcher : IDisposable
{
  private readonly IScreenInfoCacheControl _screenInfoCacheControl;
  private readonly List<FileSystemWatcher> _watchers = new List<FileSystemWatcher>();

  public ReportFileWatcher(IScreenInfoCacheControl screenInfoCacheControl)
  {
    this._screenInfoCacheControl = screenInfoCacheControl;
    this.CreateWatcher(ReportFileManager.ReportsDir);
    this.CreateWatcher(ReportFileManager.CustomReportsDir);
  }

  private void CreateWatcher(string relativePath)
  {
    string path = HostingEnvironment.MapPath(relativePath);
    if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
      return;
    FileSystemWatcher fileSystemWatcher = new FileSystemWatcher(path, "*.rpx")
    {
      EnableRaisingEvents = true,
      IncludeSubdirectories = true,
      NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
    };
    fileSystemWatcher.Created += new FileSystemEventHandler(this.Watcher_Changed);
    fileSystemWatcher.Changed += new FileSystemEventHandler(this.Watcher_Changed);
    fileSystemWatcher.Deleted += new FileSystemEventHandler(this.Watcher_Changed);
    fileSystemWatcher.Renamed += new RenamedEventHandler(this.Watcher_Renamed);
    this._watchers.Add(fileSystemWatcher);
  }

  private void Watcher_Changed(object sender, FileSystemEventArgs e)
  {
    this.InvalidateCache(e.Name);
  }

  private void Watcher_Renamed(object sender, RenamedEventArgs e)
  {
    this.InvalidateCache(e.OldName);
    this.InvalidateCache(e.Name);
  }

  private void InvalidateCache(string reportName)
  {
    string screenId = ReportStorageHelper.GetScreenId(reportName);
    if (string.IsNullOrEmpty(screenId))
      return;
    this._screenInfoCacheControl.InvalidateCache(screenId);
  }

  public void Dispose()
  {
    foreach (Component watcher in this._watchers)
      watcher.Dispose();
  }
}
