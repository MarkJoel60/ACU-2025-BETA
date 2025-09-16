// Decompiled with JetBrains decompiler
// Type: PX.Data.XLSXReader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Export.Excel.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>Reads XLSX format</summary>
public class XLSXReader : IContentReader, IDisposable
{
  private readonly Worksheet _worksheet;
  private Worksheet.Row _headerRow;
  private IEnumerator<Worksheet.Row> _rowIterator;
  private IDictionary<int, string> _keys;

  public XLSXReader(byte[] content)
  {
    Package package = new Package();
    using (MemoryStream memoryStream = new MemoryStream(content, false))
    {
      package.Read((Stream) memoryStream);
      Workbook workbook = package.Workbook;
      if (workbook.Sheets.Count <= 0)
        return;
      this._worksheet = workbook.Sheets.GetByPosition(1);
    }
  }

  public bool MoveNext()
  {
    if (this._rowIterator != null)
    {
      while (this._rowIterator.MoveNext())
      {
        if (this._rowIterator.Current.Cells.Count > 0)
          return true;
      }
    }
    return false;
  }

  public string GetValue(int index)
  {
    if (this._rowIterator == null || this._headerRow == this._rowIterator.Current)
      return (string) null;
    return index < 0 || this._rowIterator.Current == null ? (string) null : this.GetValue(this._rowIterator.Current.Cells[index]);
  }

  public void Reset()
  {
    this._headerRow = (Worksheet.Row) null;
    this._keys = (IDictionary<int, string>) null;
    if (this._worksheet.Rows.Count <= 0)
      return;
    int startOfData = this.FindStartOfData(this._worksheet.Rows.GetEnumerator());
    this._rowIterator = this._worksheet.Rows.GetEnumerator();
    if (this._rowIterator == null)
      return;
    this.MoveToStart(this._rowIterator, startOfData);
    do
    {
      this._headerRow = this._rowIterator.Current;
    }
    while (this._headerRow != null && this._headerRow.Cells.Count == 0 && this._rowIterator.MoveNext());
  }

  public IDictionary<int, string> IndexKeyPairs
  {
    get
    {
      if (this._keys == null)
      {
        this._keys = (IDictionary<int, string>) new Dictionary<int, string>();
        if (this._headerRow != null)
        {
          foreach (Worksheet.Cell cell in this._headerRow.Cells)
            this._keys.Add(new KeyValuePair<int, string>(cell.Address.Column, this.GetValue(cell)));
        }
      }
      return this._keys;
    }
  }

  public void Dispose()
  {
  }

  private int FindStartOfData(IEnumerator<Worksheet.Row> rows)
  {
    int startOfData = 0;
    int num = 0;
    while (rows != null && rows.MoveNext())
    {
      if (rows.Current.Cells.Count >= 1)
      {
        int row = ((IEnumerable<Worksheet.Cell>) rows.Current.Cells).First<Worksheet.Cell>().Address.Row;
        if ((row > num + 1 || startOfData == 0) && this.FindDataInRow(rows.Current))
          startOfData = row;
        num = row;
      }
    }
    return startOfData;
  }

  private bool FindDataInRow(Worksheet.Row row)
  {
    foreach (Worksheet.Cell cell in row.Cells)
    {
      object obj = this._worksheet.GetValue(cell);
      if (obj != null && !string.IsNullOrWhiteSpace(obj.ToString()))
        return true;
    }
    return false;
  }

  private void MoveToStart(IEnumerator<Worksheet.Row> rows, int startIndex)
  {
    if (startIndex < 1)
      return;
    int num = 0;
    rows.MoveNext();
    if (rows.Current.Cells.Count > 0)
      num = ((IEnumerable<Worksheet.Cell>) rows.Current.Cells).First<Worksheet.Cell>().Address.Row;
    while (num < startIndex && rows.MoveNext())
    {
      if (rows.Current.Cells.Count >= 1)
        num = ((IEnumerable<Worksheet.Cell>) rows.Current.Cells).First<Worksheet.Cell>().Address.Row;
    }
  }

  private string GetValue(Worksheet.Cell cell)
  {
    object obj;
    return cell != null && (obj = this._worksheet.GetValue(cell)) != null ? obj.ToString() : string.Empty;
  }
}
