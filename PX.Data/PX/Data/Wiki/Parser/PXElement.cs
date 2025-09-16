// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>
/// Represents a base class for all wiki elements in memory.
/// </summary>
public abstract class PXElement
{
  private bool isError;
  private string wikiTag;
  private bool needClear;

  internal PXElement parent { get; set; }

  /// <summary>
  /// Gets or sets value indicating whether an error occured during parsing of this element.
  /// </summary>
  public bool IsError
  {
    get => this.isError;
    set => this.isError = value;
  }

  /// <summary>
  /// Gets or sets wiki tag to be added to element in design mode.
  /// </summary>
  public string WikiTag
  {
    get => this.wikiTag;
    set => this.wikiTag = value;
  }

  /// <summary>
  /// Determines whether div with class="clear" should be inserted before element.
  /// </summary>
  public bool NeedClear
  {
    get => this.needClear;
    set => this.needClear = value;
  }
}
