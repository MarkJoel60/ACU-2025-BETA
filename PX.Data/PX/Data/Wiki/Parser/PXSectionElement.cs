// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXSectionElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>Represents a section of elements in memory.</summary>
internal class PXSectionElement : PXContainerElement
{
  private PXHeaderElement header;
  private bool isCollapsable;
  private bool isCollapsed;
  private int startIndex;
  private int endIndex;

  /// <summary>Gets or sets a header for this section.</summary>
  public PXHeaderElement Header
  {
    get => this.header;
    set => this.header = value;
  }

  /// <summary>
  /// Gets or sets value indicating whether this section is collapsable.
  /// </summary>
  public bool IsCollapsable
  {
    get => this.isCollapsable || this.isCollapsed;
    set => this.isCollapsable = value;
  }

  /// <summary>
  /// Gets or sets value indicating whether this section is initially collapsed.
  /// </summary>
  public bool IsCollapsed
  {
    get => this.isCollapsed;
    set => this.isCollapsed = value;
  }

  public int StartIndex
  {
    get => this.startIndex;
    set => this.startIndex = value;
  }

  public int EndIndex
  {
    get => this.endIndex;
    set => this.endIndex = value;
  }
}
