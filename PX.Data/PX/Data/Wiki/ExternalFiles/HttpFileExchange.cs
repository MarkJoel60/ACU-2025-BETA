// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.ExternalFiles.HttpFileExchange
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.IO;
using System.Net;

#nullable disable
namespace PX.Data.Wiki.ExternalFiles;

public class HttpFileExchange : BaseFileExchange, IFileExchange
{
  public string Name => "HTTP";

  public string Code => "H";

  public HttpFileExchange(
    string login,
    string password,
    PX.SM.FileInfo sshCertificate = null,
    string sshPassword = null)
    : base(login, password)
  {
    if (this._creds == null)
      return;
    this._creds = new NetworkCredential(login, password);
  }

  private Uri GetFileUri(string path)
  {
    path = path.Replace("\\", "/");
    return new Uri(path);
  }

  public override Stream DownloadStream(string path)
  {
    WebRequest webRequest = WebRequest.Create(this.GetFileUri(path));
    if (this._creds != null)
      webRequest.Credentials = (ICredentials) this._creds;
    webRequest.Timeout = 3600000;
    return webRequest.GetResponse().GetResponseStream();
  }
}
