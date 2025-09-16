// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.Storage.BaseStorage
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace PX.Data.Update.Storage;

public abstract class BaseStorage
{
  protected BaseStorage.StorageSettingsCollection settings = new BaseStorage.StorageSettingsCollection();

  public IEnumerable<StorageSettings> Settings
  {
    get => (IEnumerable<StorageSettings>) this.settings;
    set
    {
      foreach (StorageSettings storageSettings1 in value)
      {
        StorageSettings setting = storageSettings1;
        StorageSettings storageSettings2 = this.settings.FirstOrDefault<StorageSettings>((Func<StorageSettings, bool>) (s => s.Key == setting.Key));
        if (storageSettings2 == null)
          throw new PXException("Wrong settings");
        storageSettings2.Value = setting.Value;
      }
      this.ValidateParameters();
    }
  }

  public virtual string Name => this.GetType().Name;

  public virtual byte[] this[Guid id]
  {
    get
    {
      using (Stream stream = this.OpenRead(id))
      {
        byte[] buffer = new byte[stream.Length];
        stream.Read(buffer, 0, buffer.Length);
        return buffer;
      }
    }
    set
    {
      if (value == null)
        return;
      using (Stream stream = this.OpenWrite(id))
        stream.Write(value, 0, value.Length);
    }
  }

  public virtual byte[] this[string id]
  {
    get
    {
      using (Stream stream = this.OpenRead(id))
      {
        byte[] buffer = new byte[stream.Length];
        stream.Read(buffer, 0, buffer.Length);
        return buffer;
      }
    }
    set
    {
      if (value == null)
        return;
      using (Stream stream = this.OpenWrite(id))
        stream.Write(value, 0, value.Length);
    }
  }

  protected virtual string GetParameter(string key)
  {
    return this.settings.FirstOrDefault<StorageSettings>((Func<StorageSettings, bool>) (s => s.Key == key))?.Value;
  }

  protected virtual void ValidateParameters()
  {
  }

  protected virtual void ValidateParameters(params string[] names)
  {
    foreach (string name in names)
    {
      if (this.settings[name] == null || string.IsNullOrEmpty(this.settings[name].Value))
        throw new PXException("The required parameter '{0}' is not set up for the storage provider '{0}'.", new object[2]
        {
          (object) name,
          (object) this.Name
        });
    }
  }

  protected string GuidToStringId(Guid id) => id.ToString() + ".zip";

  public abstract void Test();

  public virtual bool Exists(Guid id) => this.Exists(this.GuidToStringId(id));

  public abstract bool Exists(string id);

  public virtual Stream OpenWrite(Guid id) => this.OpenWrite(this.GuidToStringId(id));

  public abstract Stream OpenWrite(string id);

  public virtual Stream OpenRead(Guid id) => this.OpenRead(this.GuidToStringId(id));

  public abstract Stream OpenRead(string id);

  public abstract long GetSize(string id);

  public virtual long GetSize(Guid id) => this.GetSize(this.GuidToStringId(id));

  protected class StorageSettingsCollection : List<StorageSettings>
  {
    public StorageSettings this[string key]
    {
      get
      {
        return this.FirstOrDefault<StorageSettings>((Func<StorageSettings, bool>) (p => p.Key == key));
      }
      set
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].Key == key)
            this.RemoveAt(index);
        }
        this.Add(value);
      }
    }
  }
}
