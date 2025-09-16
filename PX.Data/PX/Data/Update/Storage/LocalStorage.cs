// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.Storage.LocalStorage
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.ExternalFiles;
using System;
using System.IO;

#nullable disable
namespace PX.Data.Update.Storage;

public class LocalStorage : BaseStorage, IStorageProvider
{
  protected internal const string FOLDER = "Folder";
  protected internal const string LOGIN = "Login";
  protected internal const string PASSWORD = "Password";
  protected ShareFileExchange _Provider;

  protected virtual ShareFileExchange Provider
  {
    get
    {
      if (this._Provider == null)
        this._Provider = new ShareFileExchange(this.GetParameter("Login"), this.GetParameter("Password"));
      return this._Provider;
    }
  }

  public LocalStorage()
  {
    this.settings.Add(new StorageSettings()
    {
      Key = "Folder"
    });
    this.settings.Add(new StorageSettings()
    {
      Key = "Login"
    });
    this.settings.Add(new StorageSettings()
    {
      Key = "Password",
      Password = true
    });
  }

  protected override void ValidateParameters() => this.ValidateParameters("Folder");

  public virtual string GetPath(Guid id) => this.GetPath(this.GuidToStringId(id));

  public virtual string GetPath(string id) => Path.Combine(this.GetParameter("Folder"), id);

  public override void Test()
  {
    string parameter = this.GetParameter("Folder");
    if (!this.Provider.Exists(parameter))
      throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("Directory not found {0}", (object) parameter));
    string message;
    if (!this.Provider.HasAccess(parameter, out message))
      throw new PXException(message);
  }

  public override bool Exists(string id) => this.Provider.Exists(this.GetPath(id));

  public override Stream OpenWrite(string id)
  {
    return this.Provider.UploadStream(this.GetPath(id), FileAccess.ReadWrite);
  }

  public override Stream OpenRead(string id)
  {
    return this.Provider.DownloadStream(Path.Combine(this.GetParameter("Folder"), id), FileAccess.Read);
  }

  public void Delete(Guid id) => this.Delete(this.GuidToStringId(id));

  public void Delete(string id)
  {
    string path = this.GetPath(id);
    if (!this.Provider.Exists(path))
      throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("File not found {0}", (object) path));
    this.Provider.Delete(path);
  }

  public override long GetSize(string Id) => new FileInfo(this.GetPath(Id)).Length;
}
