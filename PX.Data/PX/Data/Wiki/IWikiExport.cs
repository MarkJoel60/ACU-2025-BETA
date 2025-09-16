// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.IWikiExport
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.DITA;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki;

/// <exclude />
public interface IWikiExport
{
  IEnumerable<KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>>> GetChildFilesWithFiles(
    Guid guid,
    string language,
    string type);

  IEnumerable<KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>>> GetFileWithFiles(
    Guid guid,
    string language,
    string type);
}
