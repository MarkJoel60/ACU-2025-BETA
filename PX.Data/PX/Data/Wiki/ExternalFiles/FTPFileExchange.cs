// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.ExternalFiles.FTPFileExchange
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.ExternalFiles;

public class FTPFileExchange : BaseFileExchange, IFileExchange
{
  public string Name => "FTP";

  public string Code => "F";

  public FTPFileExchange(
    string login,
    string password,
    PX.SM.FileInfo sshCertificate = null,
    string sshPassword = null)
    : base(login, password)
  {
    this._creds = new NetworkCredential(login, password);
  }

  private FtpWebRequest GetFTPRequest(string path, string method, bool isFolder)
  {
    FtpWebRequest ftpRequest = (FtpWebRequest) WebRequest.Create(this.GetFileUri(path, isFolder));
    if (this._creds != null)
      ftpRequest.Credentials = (ICredentials) this._creds;
    ftpRequest.KeepAlive = false;
    ftpRequest.UseBinary = true;
    ftpRequest.Method = method;
    return ftpRequest;
  }

  private Uri GetFileUri(string path, bool isFolder)
  {
    StringBuilder stringBuilder = new StringBuilder(path).Replace("\\", "/");
    if (!path.StartsWithCollation("ftp"))
      stringBuilder = stringBuilder.Insert(0, "ftp://");
    if (isFolder && !path.EndsWithCollation("/"))
      stringBuilder = stringBuilder.Append("/");
    return new Uri(stringBuilder.ToString());
  }

  public override ExternalFileInfo GetInfo(string path)
  {
    ExternalFileInfo info = new ExternalFileInfo(Path.GetFileName(path))
    {
      FullName = path
    };
    info.Date = this.GetTimeStamp(info.FullName);
    return info;
  }

  public override IEnumerable<ExternalFileInfo> ListFiles(string path)
  {
    List<ExternalFileInfo> externalFileInfoList = new List<ExternalFileInfo>();
    using (WebResponse response = this.GetFTPRequest(path, "LIST", true).GetResponse())
    {
      using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
      {
        string line;
        while ((line = streamReader.ReadLine()) != null)
        {
          ExternalFileInfo ftpLine = RegExpHelper.ParseFtpLine(line);
          if (ftpLine != null)
          {
            ftpLine.FullName = this.GetFileUri(path, true)?.ToString() + ftpLine.Name;
            externalFileInfoList.Add(ftpLine);
          }
        }
      }
    }
    foreach (ExternalFileInfo externalFileInfo in externalFileInfoList)
      externalFileInfo.Date = this.GetTimeStamp(externalFileInfo.FullName);
    return (IEnumerable<ExternalFileInfo>) externalFileInfoList;
  }

  public override Stream UploadStream(string path)
  {
    return this.GetFTPRequest(path, "STOR", false).GetRequestStream();
  }

  public override Stream DownloadStream(string path)
  {
    return this.GetFTPRequest(path, "RETR", false).GetResponse().GetResponseStream();
  }

  private System.DateTime GetTimeStamp(string path)
  {
    using (FtpWebResponse response = (FtpWebResponse) this.GetFTPRequest(path, "MDTM", false).GetResponse())
      return System.DateTime.ParseExact(response.StatusDescription.Substring(4, 14), "yyyyMMddHHmmss", (IFormatProvider) CultureInfo.InvariantCulture.DateTimeFormat);
  }
}
