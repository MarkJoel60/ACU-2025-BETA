// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXTableRow
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>Represents a single table row in memory.</summary>
public class PXTableRow : PXElement
{
  private string props;
  private List<PXTableCell> cells = new List<PXTableCell>();
  private Dictionary<string, string> attributes;
  internal bool IsSingleLine;

  /// <summary>Initializes a new instance of PXTableRow class.</summary>
  public PXTableRow()
  {
  }

  /// <summary>Initializes a new instance of PXTableRow class.</summary>
  /// <param name="props">A string with CSS properties for this table row.</param>
  public PXTableRow(string props) => this.props = props;

  /// <summary>Gets or sets CSS properties for this table row.</summary>
  public string Props
  {
    get => this.props;
    set => this.props = value;
  }

  /// <summary>
  /// Gets an array of cells contained inside of this PXTableRow object.
  /// </summary>
  public PXTableCell[] Cells => this.cells.ToArray();

  public Dictionary<string, string> Attributes
  {
    get => this.attributes;
    set => this.attributes = value;
  }

  /// <summary>Adds a new cell to this PXTableRow object.</summary>
  /// <param name="cell"></param>
  public void AddCell(PXTableCell cell) => this.cells.Add(cell);
}
