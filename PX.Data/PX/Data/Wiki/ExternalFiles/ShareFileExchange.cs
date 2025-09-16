// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.ExternalFiles.ShareFileExchange
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Update;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace PX.Data.Wiki.ExternalFiles;

public class ShareFileExchange(
  string login,
  string password,
  PX.SM.FileInfo sshCertificate = null,
  string sshPassword = null) : BaseFileExchange(login, password), IFileExchange
{
  public string Name => "Shared Folder";

  public string Code => "S";

  private void WithImpersonation(System.Action action)
  {
    if (this._creds == null)
    {
      action();
    }
    else
    {
      using (new PXImpersonationScope(this._creds.UserName, this._creds.Domain, this._creds.Password))
        action();
    }
  }

  public override bool Exists(string path)
  {
    bool result = false;
    this.WithImpersonation((System.Action) (() =>
    {
      if (Directory.Exists(path))
      {
        result = true;
      }
      else
      {
        if (!File.Exists(path))
          return;
        result = true;
      }
    }));
    return result;
  }

  public bool HasAccess(string dir, out string message)
  {
    bool result = false;
    string exMessage = string.Empty;
    this.WithImpersonation((System.Action) (() =>
    {
      try
      {
        string path = Path.Combine(dir, "testWrite.txt");
        File.WriteAllText(path, "");
        File.Delete(path);
        result = true;
      }
      catch (Exception ex)
      {
        exMessage = ex.Message;
        result = false;
      }
    }));
    message = exMessage;
    return result;
  }

  public override ExternalFileInfo GetInfo(string path)
  {
    ExternalFileInfo result = new ExternalFileInfo();
    this.WithImpersonation((System.Action) (() =>
    {
      System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);
      result.Name = fileInfo.Exists ? fileInfo.Name : throw new PXException("The file '{0}' does not exist in the system.", new object[1]
      {
        (object) path
      });
      result.FullName = Path.Combine(path, fileInfo.Name);
      result.Size = fileInfo.Length;
      result.Date = fileInfo.LastWriteTimeUtc;
    }));
    return result;
  }

  public override IEnumerable<ExternalFileInfo> ListFiles(string path)
  {
    List<ExternalFileInfo> files = new List<ExternalFileInfo>();
    this.WithImpersonation((System.Action) (() =>
    {
      foreach (System.IO.FileInfo file in new DirectoryInfo(path).GetFiles())
        files.Add(new ExternalFileInfo()
        {
          Name = file.Name,
          FullName = Path.Combine(path, file.Name),
          Size = file.Length,
          Date = file.LastWriteTimeUtc
        });
    }));
    return (IEnumerable<ExternalFileInfo>) files;
  }

  public override Stream UploadStream(string path) => this.UploadStream(path, FileAccess.Write);

  public virtual Stream UploadStream(string path, FileAccess access)
  {
    Stream result = (Stream) null;
    this.WithImpersonation((System.Action) (() => result = (Stream) new FileStream(path, FileMode.Create, access, FileShare.None)));
    return result;
  }

  public override Stream DownloadStream(string path) => this.DownloadStream(path, FileAccess.Read);

  public virtual Stream DownloadStream(string path, FileAccess access)
  {
    Stream result = (Stream) null;
    this.WithImpersonation((System.Action) (() => result = (Stream) new FileStream(path, FileMode.Open, access, FileShare.Read)));
    return result;
  }

  public override void Delete(string path)
  {
    this.WithImpersonation((System.Action) (() =>
    {
      if (!File.Exists(path))
        return;
      File.Delete(path);
    }));
  }
}
