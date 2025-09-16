// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.LinkInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>
/// Information about link which is to be filled by a LinkCreatorDelegate method.
/// </summary>
public class LinkInfo
{
  /// <summary>Link to a file.</summary>
  public string Url;
  /// <summary>
  /// Path to a screen which displays this file's revisions.
  /// </summary>
  public string FileInfoPath;
  /// <summary>Text to be displayed as a link caption.</summary>
  public string DefaultCaption;
  /// <summary>Whether location to which this link refers exists.</summary>
  public bool IsExisting;
  /// <summary>File extension.</summary>
  public string Extension;
  /// <summary>
  /// Whether link is invalid (refers to guid for which no page found).
  /// </summary>
  public bool IsInvalid;
  /// <summary>ID of the requested topic.</summary>
  public Guid? PageID;
  /// <summary>Reference to an array of file bytes.</summary>
  public byte[] BinData;
}
