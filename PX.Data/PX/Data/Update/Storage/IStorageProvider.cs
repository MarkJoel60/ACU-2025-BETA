// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.Storage.IStorageProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace PX.Data.Update.Storage;

public interface IStorageProvider
{
  string Name { get; }

  IEnumerable<StorageSettings> Settings { get; set; }

  byte[] this[Guid key] { get; set; }

  byte[] this[string key] { get; set; }

  void Test();

  bool Exists(Guid id);

  bool Exists(string id);

  Stream OpenRead(Guid id);

  Stream OpenRead(string id);

  Stream OpenWrite(Guid id);

  Stream OpenWrite(string id);

  void Delete(Guid id);

  long GetSize(Guid id);

  long GetSize(string id);
}
