// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.ScreenSearchResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Diagnostics;

#nullable disable
namespace PX.Data.Search;

/// <exclude />
[DebuggerDisplay("{Path} > {Title}")]
public class ScreenSearchResult
{
  public string Url { get; private set; }

  public string Title { get; private set; }

  public string Path { get; private set; }

  public ScreenSearchResult(string url, string title, string path)
  {
    this.Url = url;
    this.Title = title;
    this.Path = path;
  }
}
