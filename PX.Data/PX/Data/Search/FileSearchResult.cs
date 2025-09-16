// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.FileSearchResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Diagnostics;

#nullable disable
namespace PX.Data.Search;

/// <exclude />
[DebuggerDisplay("{Path} > {Title}")]
public class FileSearchResult
{
  public string Url { get; private set; }

  public string Title { get; private set; }

  public string Line1 { get; private set; }

  public FileSearchResult(string url, string title, string line1)
  {
    this.Url = url;
    this.Title = title;
    this.Line1 = line1;
  }
}
