// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.ExternalFiles.SFTPFileExchange
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

#nullable disable
namespace PX.Data.Wiki.ExternalFiles;

public class SFTPFileExchange : BaseFileExchange, IFileExchange, IDisposable
{
  public string Name => "SFTP";

  public string Code => "C";

  private PX.SM.FileInfo SshCertificate { get; set; }

  private string SshPassword { get; set; }

  private SftpClient Client { get; set; }

  public SFTPFileExchange(
    string login,
    string password,
    PX.SM.FileInfo sshCertificate = null,
    string sshPassword = null)
    : base(login, password, sshCertificate, sshPassword)
  {
    this._creds = new NetworkCredential(login, password);
    this.SshCertificate = sshCertificate;
    this.SshPassword = sshPassword;
  }

  private SftpClient EnsureSftpClient(Uri url)
  {
    if (this.Client != null)
      return this.Client;
    List<AuthenticationMethod> authenticationMethodList = new List<AuthenticationMethod>();
    if (this.SshCertificate?.BinData != null)
    {
      PrivateKeyFile privateKeyFile = new PrivateKeyFile((Stream) new MemoryStream(this.SshCertificate.BinData), this.SshPassword);
      authenticationMethodList.Add((AuthenticationMethod) new PrivateKeyAuthenticationMethod(this._creds?.UserName, new IPrivateKeySource[1]
      {
        (IPrivateKeySource) privateKeyFile
      }));
    }
    if (authenticationMethodList.Count == 0 || !string.IsNullOrEmpty(this._creds?.Password))
      authenticationMethodList.Add((AuthenticationMethod) new PasswordAuthenticationMethod(this._creds?.UserName, this._creds?.Password));
    this.Client = new SftpClient(url.Port > 0 ? new ConnectionInfo(url.Host, url.Port, this._creds?.UserName, authenticationMethodList.ToArray()) : new ConnectionInfo(url.Host, this._creds?.UserName, authenticationMethodList.ToArray()));
    ((BaseClient) this.Client).Connect();
    return this.Client;
  }

  private Uri GetFileUri(string path, bool isFolder = false)
  {
    StringBuilder stringBuilder = new StringBuilder(path).Replace("\\", "/");
    if (!path.StartsWithCollation("sftp"))
      stringBuilder = stringBuilder.Insert(0, "sftp://");
    if (isFolder && !path.EndsWithCollation("/"))
      stringBuilder = stringBuilder.Append("/");
    return new Uri(stringBuilder.ToString());
  }

  public override ExternalFileInfo GetInfo(string path)
  {
    Uri fileUri = this.GetFileUri(path);
    System.DateTime lastWriteTimeUtc = this.EnsureSftpClient(fileUri).GetLastWriteTimeUtc(fileUri.LocalPath);
    return new ExternalFileInfo(Path.GetFileName(path))
    {
      FullName = path,
      Date = lastWriteTimeUtc
    };
  }

  public override IEnumerable<ExternalFileInfo> ListFiles(string path)
  {
    Uri fileUri = this.GetFileUri(path, true);
    return (IEnumerable<ExternalFileInfo>) this.EnsureSftpClient(fileUri).ListDirectory(fileUri?.LocalPath, (System.Action<int>) null).Where<ISftpFile>((Func<ISftpFile, bool>) (file => file.Length > 0L)).Select<ISftpFile, ExternalFileInfo>((Func<ISftpFile, ExternalFileInfo>) (file => new ExternalFileInfo()
    {
      Name = Path.GetFileName(file.FullName),
      FullName = file.FullName,
      Date = file.LastWriteTimeUtc
    })).ToArray<ExternalFileInfo>();
  }

  public override void Upload(string path, byte[] data)
  {
    if (data == null)
    {
      ArgumentNullException argumentNullException1 = new ArgumentNullException(nameof (data));
    }
    if (string.IsNullOrEmpty(path))
    {
      ArgumentNullException argumentNullException2 = new ArgumentNullException(nameof (path));
    }
    Uri fileUri = this.GetFileUri(path);
    SftpClient sftpClient = this.EnsureSftpClient(fileUri);
    using (MemoryStream memoryStream = new MemoryStream(data))
    {
      sftpClient.BufferSize = 4096U /*0x1000*/;
      sftpClient.UploadFile((Stream) memoryStream, fileUri?.LocalPath, (System.Action<ulong>) null);
    }
  }

  public override Stream UploadStream(string path) => (Stream) null;

  public override Stream DownloadStream(string path)
  {
    if (string.IsNullOrEmpty(path))
    {
      ArgumentNullException argumentNullException = new ArgumentNullException(nameof (path));
    }
    Uri fileUri = this.GetFileUri(path);
    SftpClient sftpClient = this.EnsureSftpClient(fileUri);
    MemoryStream memoryStream1 = new MemoryStream();
    string localPath = fileUri?.LocalPath;
    MemoryStream memoryStream2 = memoryStream1;
    sftpClient.DownloadFile(localPath, (Stream) memoryStream2, (System.Action<ulong>) null);
    memoryStream1.Position = 0L;
    return (Stream) memoryStream1;
  }

  public void Dispose()
  {
    if (this.Client == null)
      return;
    if (((BaseClient) this.Client).IsConnected)
      ((BaseClient) this.Client).Disconnect();
    ((BaseClient) this.Client).Dispose();
  }
}
