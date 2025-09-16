// Decompiled with JetBrains decompiler
// Type: PX.Data.PXWikiFilesLoader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

#nullable disable
namespace PX.Data;

/// <summary>
/// Performs loading of files from wiki articles to web site folder on file system.
/// </summary>
public class PXWikiFilesLoader
{
  private const string SlotName = "WikiDynamicFiles";

  public static void EnsureLoaded()
  {
    if (PXDatabase.GetSlot<PXWikiFilesLoader.Definition>("WikiDynamicFiles", typeof (WikiSitePath), typeof (UploadFile), typeof (UploadFileRevision)) != null)
      return;
    PXWikiFilesLoader.Definition definition = PXWikiFilesLoader.Definition.Default;
  }

  private class Definition : IPrefetchable, IPXCompanyDependent
  {
    public static Dictionary<Guid, int?> filesRevisions = new Dictionary<Guid, int?>();
    private List<WikiSitePath> paths = new List<WikiSitePath>();
    public static readonly PXWikiFilesLoader.Definition Default = new PXWikiFilesLoader.Definition();

    public void Prefetch()
    {
      foreach (PXResult<WikiSitePath, UploadFile, UploadFileRevision> pxResult in new PXSelectJoin<WikiSitePath, InnerJoin<UploadFile, On<WikiSitePath.pageID, Equal<UploadFile.primaryPageID>>, InnerJoin<UploadFileRevision, On<UploadFile.fileID, Equal<UploadFileRevision.fileID>, And<UploadFile.lastRevisionID, Equal<UploadFileRevision.fileRevisionID>>>>>>(new PXGraph()).View.SelectMulti((object) this.paths))
      {
        UploadFile file = (UploadFile) pxResult;
        Dictionary<Guid, int?> filesRevisions1 = PXWikiFilesLoader.Definition.filesRevisions;
        Guid? fileId = file.FileID;
        Guid key1 = fileId.Value;
        if (filesRevisions1.ContainsKey(key1))
        {
          Dictionary<Guid, int?> filesRevisions2 = PXWikiFilesLoader.Definition.filesRevisions;
          fileId = file.FileID;
          Guid key2 = fileId.Value;
          int? nullable = filesRevisions2[key2];
          int? lastRevisionId = file.LastRevisionID;
          if (nullable.GetValueOrDefault() == lastRevisionId.GetValueOrDefault() & nullable.HasValue == lastRevisionId.HasValue)
            goto label_5;
        }
        this.DoLoad((WikiSitePath) pxResult, file, (UploadFileRevision) pxResult);
label_5:
        Dictionary<Guid, int?> filesRevisions3 = PXWikiFilesLoader.Definition.filesRevisions;
        fileId = file.FileID;
        Guid key3 = fileId.Value;
        int? lastRevisionId1 = file.LastRevisionID;
        filesRevisions3[key3] = lastRevisionId1;
      }
    }

    private void DoLoad(WikiSitePath sitePath, UploadFile file, UploadFileRevision rev)
    {
      string path = this.GetPath(sitePath.Path, file.Name);
      if (path == null || rev.Data == null)
        return;
      System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);
      if (fileInfo.Exists)
        fileInfo.IsReadOnly = false;
      FileStream fileStream = fileInfo.OpenWrite();
      fileStream.Write(rev.Data, 0, rev.Data.Length);
      fileStream.Close();
    }

    private string GetPath(string destination, string filename)
    {
      string str = $"{HttpContext.Current.Request.PhysicalApplicationPath}{destination}\\";
      int num1 = filename.IndexOf('\\');
      if (num1 == filename.Length - 1)
        return (string) null;
      if (num1 != -1)
        filename = filename.Substring(num1 + 1);
      int num2 = filename.IndexOf('/');
      if (num2 == filename.Length - 1)
        return (string) null;
      if (num2 != -1)
        filename = filename.Substring(num2 + 1);
      return str + filename;
    }
  }
}
