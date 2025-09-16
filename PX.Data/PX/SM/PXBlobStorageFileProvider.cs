// Decompiled with JetBrains decompiler
// Type: PX.SM.PXBlobStorageFileProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace PX.SM;

public class PXBlobStorageFileProvider : IBlobStorageProvider
{
  private string Folder;
  private const string PARAM_FOLDER = "LocalFolder";

  public Guid Save(byte[] data, PXBlobStorageContext saveContext)
  {
    Guid guid = Guid.NewGuid();
    File.WriteAllBytes(Path.Combine(this.Folder, guid.ToString() + ".bin"), data);
    return guid;
  }

  public byte[] Load(Guid id)
  {
    string path = Path.Combine(this.Folder, id.ToString() + ".bin");
    return File.Exists(path) ? File.ReadAllBytes(path) : throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("The file is not found: {0}", (object) path));
  }

  public void Remove(Guid id)
  {
    string path = Path.Combine(this.Folder, id.ToString() + ".bin");
    if (!File.Exists(path))
      return;
    File.Delete(path);
  }

  public void CleanUp(int companyId)
  {
  }

  public IEnumerable<BlobProviderSettings> GetSettings()
  {
    yield return new BlobProviderSettings()
    {
      Name = "LocalFolder",
      Value = this.Folder
    };
  }

  public void Init(IEnumerable<BlobProviderSettings> settings)
  {
    foreach (BlobProviderSettings setting in settings)
    {
      if (setting.Name == "LocalFolder")
      {
        string str = setting.Value;
        if (!Directory.Exists(str))
          throw new PXProviderConfigException("An invalid path has been set up for the external file storage.")
          {
            Row = setting
          };
        try
        {
          File.WriteAllText(Path.Combine(str, "testWrite.txt"), "");
        }
        catch (Exception ex)
        {
          throw new PXProviderConfigException(ex.Message)
          {
            Row = setting
          };
        }
        this.Folder = str;
      }
    }
  }

  public string GetIdentity() => "LocalFolder:" + this.Folder;

  public string Update(BlobProviderSettings newRow)
  {
    if (newRow.Name == "LocalFolder")
    {
      if (Str.IsNullOrEmpty(newRow.Value))
        return "The value cannot be null.";
      if (!Directory.Exists(newRow.Value))
        return "The path does not exist.";
      this.Folder = newRow.Value;
    }
    return (string) null;
  }
}
