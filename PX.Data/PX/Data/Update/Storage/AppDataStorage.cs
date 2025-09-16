// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.Storage.AppDataStorage
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.ExternalFiles;
using System.IO;

#nullable disable
namespace PX.Data.Update.Storage;

internal class AppDataStorage : LocalStorage, IStorageProvider
{
  protected override ShareFileExchange Provider
  {
    get
    {
      if (this._Provider == null)
        this._Provider = new ShareFileExchange((string) null, (string) null);
      return this._Provider;
    }
  }

  public AppDataStorage()
  {
    string path = Path.Combine(PXInstanceHelper.AppDataFolder, "Storage");
    if (!Directory.Exists(path))
      Directory.CreateDirectory(path);
    this.settings.Clear();
    this.settings.Add(new StorageSettings()
    {
      Key = "Folder",
      Value = path
    });
  }

  public virtual void Clear() => this.Clear((string) null);

  public virtual void Clear(string pattern)
  {
    if (string.IsNullOrEmpty(pattern))
      pattern = "*";
    foreach (string file in Directory.GetFiles(this.GetParameter("Folder"), pattern, SearchOption.TopDirectoryOnly))
      this.Provider.Delete(file);
  }
}
