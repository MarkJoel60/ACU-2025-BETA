// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.ExternalFiles.IFileExchange
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.IO;

#nullable disable
namespace PX.Data.Wiki.ExternalFiles;

public interface IFileExchange
{
  bool AllowInfo { get; }

  bool AllowListing { get; }

  bool AllowUpload { get; }

  bool AllowDownload { get; }

  string Name { get; }

  string Code { get; }

  ExternalFileInfo GetInfo(string path);

  IEnumerable<ExternalFileInfo> ListFiles(string path);

  Stream UploadStream(string path);

  Stream DownloadStream(string path);

  void Upload(string path, byte[] data);

  byte[] Download(string path);
}
