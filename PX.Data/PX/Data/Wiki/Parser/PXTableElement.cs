// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.PXTableElement
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

/// <summary>Represents a table element in memory.</summary>
public class PXTableElement : PXElement
{
  private string props;
  private string caption;
  private List<PXTableRow> rows = new List<PXTableRow>();
  private Dictionary<string, string> attributes;

  /// <summary>Initializes a new instance of PXTableElement class.</summary>
  public PXTableElement()
  {
  }

  /// <summary>Initializes a new instance of PXTableElement class.</summary>
  /// <param name="props">A string with CSS properties for this table.</param>
  public PXTableElement(string props) => this.props = props;

  /// <summary>Gets an array of rows contained by this table.</summary>
  public PXTableRow[] Rows => this.rows.ToArray();

  /// <summary>Gets or sets CSS table properties.</summary>
  public string Props
  {
    get => this.props;
    set => this.props = value;
  }

  /// <summary>Gets or sets table caption text.</summary>
  public string Caption
  {
    get => this.caption;
    set => this.caption = value;
  }

  public Dictionary<string, string> Attributes
  {
    get => this.attributes;
    set => this.attributes = value;
  }

  public int[] ColumnMaxWidth { get; set; }

  public double[] ColumnMaxWidthPercent { get; set; }

  /// <summary>Adds a new row to the table.</summary>
  /// <param name="row">A row to add.</param>
  public void AddRow(PXTableRow row) => this.rows.Add(row);
}
