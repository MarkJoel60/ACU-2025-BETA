// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPageFileWatcher
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Caching;
using PX.Common;
using PX.Metadata;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Web.Hosting;

#nullable disable
namespace PX.Data;

internal class PXPageFileWatcher : IDisposable
{
  private readonly IScreenInfoCacheControl _screenInfoCacheControl;
  private readonly CompositeDisposable _watchers = new CompositeDisposable();
  private readonly ILogger _logger;
  private readonly IPXPageIndexingService _pageIndexingService;

  public PXPageFileWatcher(
    IScreenInfoCacheControl screenInfoCacheControl,
    ILogger logger,
    IPXPageIndexingService pageIndexingService)
  {
    PXPageFileWatcher pxPageFileWatcher = this;
    this._screenInfoCacheControl = screenInfoCacheControl;
    this._logger = logger;
    this._pageIndexingService = pageIndexingService;
    PXCodeDirectoryCompiler.NotifyOnChange((System.Action) (() => pxPageFileWatcher.TryForEachTenant(new System.Action(((ICacheControl) screenInfoCacheControl).InvalidateCache))));
    this.CreateWatcher("~/Pages", "*.aspx*", (System.Action<string>) (fileName => pxPageFileWatcher.TryForEachTenant((System.Action) (() => pxPageFileWatcher.PageChanged(fileName)))));
    this.CreateWatcher("~/CstPublished", "*.aspx*", (System.Action<string>) (fileName => pxPageFileWatcher.TryForEachTenant((System.Action) (() => pxPageFileWatcher.PageChanged(fileName)))));
    this.CreateWatcher("~/Frames", "*.aspx*", (System.Action<string>) (fileName => pxPageFileWatcher.TryForEachTenant((System.Action) (() => pxPageFileWatcher.FramePageChanged(fileName)))));
    this.CreateWatcher("~/GenericInquiry", "*.aspx*", (System.Action<string>) (fileName => pxPageFileWatcher.TryForEachTenant((System.Action) (() => pxPageFileWatcher.FramePageChanged(fileName)))));
    this.CreateWatcher("~/MasterPages", "*.master*", (System.Action<string>) (_ => pxPageFileWatcher.TryForEachTenant(new System.Action(((ICacheControl) screenInfoCacheControl).InvalidateCache))));
  }

  private void TryForEachTenant(System.Action action)
  {
    try
    {
      PXAccess.ForEachTenantAsAdmin(action);
    }
    catch (AggregateException ex)
    {
      this._logger.Error((Exception) ex, "File watcher failed for at least one tenant");
    }
  }

  private void CreateWatcher(string relativePath, string filter, System.Action<string> fileChanged)
  {
    string path = HostingEnvironment.MapPath(relativePath);
    if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
      return;
    FileSystemWatcher fileSystemWatcher = new FileSystemWatcher(path, filter)
    {
      EnableRaisingEvents = true,
      IncludeSubdirectories = true,
      NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
    };
    fileSystemWatcher.Created += (FileSystemEventHandler) ((s, e) => OnFileChanged(new string[1]
    {
      e.Name
    }));
    fileSystemWatcher.Changed += (FileSystemEventHandler) ((s, e) => OnFileChanged(new string[1]
    {
      e.Name
    }));
    fileSystemWatcher.Deleted += (FileSystemEventHandler) ((s, e) => OnFileChanged(new string[1]
    {
      e.Name
    }));
    fileSystemWatcher.Renamed += (RenamedEventHandler) ((s, e) => OnFileChanged(new string[2]
    {
      e.OldName,
      e.Name
    }));
    this._watchers.Add((IDisposable) fileSystemWatcher);

    void OnFileChanged(string[] fileNames)
    {
      this._pageIndexingService.Clear();
      foreach (string fileName in fileNames)
        fileChanged(fileName);
    }
  }

  private void PageChanged(string fileName)
  {
    string withoutExtension = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(fileName));
    if (string.IsNullOrEmpty(withoutExtension) || withoutExtension.Length != 8)
      return;
    this._screenInfoCacheControl.InvalidateCache(withoutExtension);
  }

  private void FramePageChanged(string fileName)
  {
    fileName = Path.GetFileName(fileName);
    if (string.Equals(Path.GetExtension(fileName), ".cs", StringComparison.OrdinalIgnoreCase))
      fileName = Path.GetFileNameWithoutExtension(fileName);
    foreach (string screenId in PXSiteMap.Provider.Definitions.Nodes.Where<PXSiteMapNode>((Func<PXSiteMapNode, bool>) (n => !string.IsNullOrEmpty(n.Url) && !string.IsNullOrEmpty(n.ScreenID) && Str.Contains(n.Url, fileName, StringComparison.OrdinalIgnoreCase))).Select<PXSiteMapNode, string>((Func<PXSiteMapNode, string>) (n => n.ScreenID)))
      this._screenInfoCacheControl.InvalidateCache(screenId);
  }

  void IDisposable.Dispose() => this._watchers?.Dispose();
}
