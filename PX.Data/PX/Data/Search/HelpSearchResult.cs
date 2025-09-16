// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.HelpSearchResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Diagnostics;

#nullable disable
namespace PX.Data.Search;

/// <exclude />
[DebuggerDisplay("{Title} / {Line1}; {Line2}")]
public class HelpSearchResult
{
  public Guid ID { get; private set; }

  public Guid PageID { get; private set; }

  public string Title { get; private set; }

  public string Path { get; private set; }

  public string Text { get; private set; }

  public HelpSearchResult(Guid id, Guid pageID, string title, string path, string text)
  {
    this.ID = id;
    this.PageID = pageID;
    this.Title = title;
    this.Path = path;
    this.Text = text;
  }
}
