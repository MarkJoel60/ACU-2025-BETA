// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.ExternalFiles.BaseFileExchange
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;

#nullable disable
namespace PX.Data.Wiki.ExternalFiles;

public abstract class BaseFileExchange
{
  protected NetworkCredential _creds;

  public bool AllowInfo => this.IsMethodOverride("GetInfo");

  public bool AllowListing => this.IsMethodOverride("ListFiles");

  public bool AllowUpload => this.IsMethodOverride("UploadStream");

  public bool AllowDownload => this.IsMethodOverride("DownloadStream");

  protected BaseFileExchange(
    string login,
    string password,
    PX.SM.FileInfo sshCertificate = null,
    string sshPassword = null)
  {
    if (string.IsNullOrEmpty(login))
      return;
    this._creds = new NetworkCredential(PXCredentials.GetUser(login), password, PXCredentials.GetDomain(login));
  }

  [MethodDisabled]
  public virtual bool Exists(string path)
  {
    throw new PXException("This provider doesn't support directory listing.");
  }

  [MethodDisabled]
  public virtual ExternalFileInfo GetInfo(string path)
  {
    throw new PXException("This provider doesn't support directory listing.");
  }

  [MethodDisabled]
  public virtual IEnumerable<ExternalFileInfo> ListFiles(string path)
  {
    throw new PXException("This provider doesn't support directory listing.");
  }

  [MethodDisabled]
  public virtual Stream UploadStream(string path)
  {
    throw new PXException("This provider doesn't support file uploading.");
  }

  [MethodDisabled]
  public virtual Stream DownloadStream(string path)
  {
    throw new PXException("This provider doesn't support file downloading.");
  }

  [MethodDisabled]
  public virtual void Delete(string path)
  {
    throw new PXException("This provider doesn't support file removal.");
  }

  public virtual void Upload(string path, byte[] data)
  {
    if (data == null)
    {
      ArgumentNullException argumentNullException1 = new ArgumentNullException(nameof (data));
    }
    if (string.IsNullOrEmpty(path))
    {
      ArgumentNullException argumentNullException2 = new ArgumentNullException(nameof (path));
    }
    using (Stream stream = this.UploadStream(path))
      stream.Write(data, 0, data.Length);
  }

  public virtual byte[] Download(string path)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (Stream stream = this.DownloadStream(path))
      {
        byte[] buffer = new byte[1024 /*0x0400*/];
        for (int count = stream.Read(buffer, 0, buffer.Length); count != 0; count = stream.Read(buffer, 0, buffer.Length))
          memoryStream.Write(buffer, 0, count);
      }
      memoryStream.Flush();
      return memoryStream.ToArray();
    }
  }

  private bool IsMethodOverride(string Name)
  {
    return this.GetType().GetMethod(Name, BindingFlags.Instance | BindingFlags.Public, (Binder) null, new System.Type[1]
    {
      typeof (string)
    }, (ParameterModifier[]) null).GetCustomAttributes(typeof (MethodDisabledAttribute), false).Length == 0;
  }
}
