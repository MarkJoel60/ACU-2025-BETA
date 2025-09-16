// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.ConversionType2
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
public class ConversionType2 : IWikiExport
{
  public IEnumerable<KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>>> GetChildFilesWithFiles(
    Guid guid,
    string language,
    string type)
  {
    throw new NotImplementedException();
  }

  public IEnumerable<KeyValuePair<CutWikiPage, IEnumerable<Package.MyFileInfo>>> GetFileWithFiles(
    Guid guid,
    string language,
    string type)
  {
    throw new NotImplementedException();
  }
}
