// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.EntitySearchResult
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
public class EntitySearchResult
{
  public string ScreenId { get; private set; }

  public Guid NoteID { get; private set; }

  public string Title { get; private set; }

  public string Path { get; private set; }

  public string Line1 { get; private set; }

  public string Line2 { get; private set; }

  public string EntityType { get; private set; }

  public string Content { get; private set; }

  public EntitySearchResult(
    string screenId,
    Guid noteID,
    string title,
    string path,
    string line1,
    string line2)
  {
    this.NoteID = noteID;
    this.Title = title;
    this.Path = path;
    this.Line1 = line1;
    this.Line2 = line2;
    this.ScreenId = screenId;
  }

  public EntitySearchResult(
    string screenId,
    Guid noteID,
    string title,
    string path,
    string line1,
    string line2,
    string entityType,
    string content)
    : this(screenId, noteID, title, path, line1, line2)
  {
    this.EntityType = entityType;
    this.Content = content;
  }
}
