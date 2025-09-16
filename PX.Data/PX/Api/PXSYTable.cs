// Decompiled with JetBrains decompiler
// Type: PX.Api.PXSYTable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Api;

public class PXSYTable : IEnumerable<PXSYRow>, IEnumerable
{
  private readonly List<PXSYRow> _Rows = new List<PXSYRow>();
  private readonly List<string> _Columns;

  public List<PXSYRow> Rows => this._Rows;

  public string TimeStamp { get; set; }

  public string TimeStampUtc { get; set; }

  public string ObjectName { get; set; }

  public string Description { get; set; }

  public PXSYRow this[int index]
  {
    get => this._Rows[index];
    set
    {
      this.CheckRow(value);
      this._Rows[index] = value;
    }
  }

  public IEnumerator<PXSYRow> GetEnumerator()
  {
    foreach (PXSYRow row in this._Rows)
      yield return row;
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  public IEnumerable<string> Columns => (IEnumerable<string>) this._Columns;

  public PXSYTable(IEnumerable<string> columns)
  {
    this._Columns = new List<string>();
    foreach (string column in columns)
    {
      if (this._Columns.Contains(column))
        throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("PXSYTable can't contain duplicate columns '{0}'", (object) column));
      this._Columns.Add(column);
    }
  }

  public void Add(params PXSYRow[] items)
  {
    this.CheckRows((IEnumerable<PXSYRow>) items);
    this._Rows.AddRange((IEnumerable<PXSYRow>) items);
  }

  public void Add(IEnumerable<PXSYRow> items)
  {
    this.CheckRows(items);
    this._Rows.AddRange(items);
  }

  private void CheckRows(IEnumerable<PXSYRow> items)
  {
    foreach (PXSYRow pxsyRow in items)
      this.CheckRow(pxsyRow);
  }

  private void CheckRow(PXSYRow item)
  {
    if (item.Parent != this)
      throw new PXException("You should add a row that belongs to this table.");
  }

  public int IndexOfColumn(string column)
  {
    return this._Columns.FindIndex((Predicate<string>) (c => string.Equals(c, column, StringComparison.InvariantCultureIgnoreCase)));
  }

  public string GetValue(int columnIndex, int rowIndex)
  {
    if (rowIndex >= this._Rows.Count)
      throw new IndexOutOfRangeException("Row index is outside the bounds of the array.");
    if (columnIndex >= this.ColumnsCount)
      throw new IndexOutOfRangeException("Column index is outside the bounds of the array.");
    return this._Rows[rowIndex][columnIndex];
  }

  public int Count => this._Rows.Count;

  public int ColumnsCount => this._Columns.Count;

  public PXSYRow CreateRow() => new PXSYRow(this);

  public string[][] ToArray()
  {
    string[][] array = new string[this.Count][];
    for (int index = 0; index < this.Count; ++index)
      array[index] = this._Rows[index].ToArray();
    return array;
  }
}
