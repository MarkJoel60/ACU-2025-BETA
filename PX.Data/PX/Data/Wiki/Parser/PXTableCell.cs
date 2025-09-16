// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXTableCell
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>Represents a table cell in memory.</summary>
public class PXTableCell : PXContainerElement
{
  private bool isHeader;
  private string props;
  private Dictionary<string, string> attributes;

  /// <summary>Initializes a new instance of PXTableCell class.</summary>
  /// <param name="isHeader">Value indicating whether header or ordinary cell should created.</param>
  public PXTableCell(bool isHeader) => this.isHeader = isHeader;

  /// <summary>Initializes a new instance of PXTableCell class.</summary>
  /// <param name="isHeader">Value indicating whether header or ordinary cell should created.</param>
  /// <param name="props">Gets or sets CSS properties for this table row.</param>
  public PXTableCell(bool isHeader, string props)
  {
    this.isHeader = isHeader;
    this.props = props;
  }

  /// <summary>
  /// Gets a value indicating whether this is a header cell.
  /// </summary>
  public bool IsHeader => this.isHeader;

  /// <summary>Gets or sets CSS properties for this cell.</summary>
  public string Props
  {
    get => this.props;
    set => this.props = value;
  }

  public Dictionary<string, string> Attributes
  {
    get => this.attributes;
    set => this.attributes = value;
  }
}
