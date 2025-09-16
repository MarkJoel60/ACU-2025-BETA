// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.Feedback.TableContext
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition.Feedback;

internal class TableContext
{
  public short Page { get; }

  public short Table { get; }

  private SortedDictionary<short, short> _rowByDetailRow { get; } = new SortedDictionary<short, short>();

  private Dictionary<string, List<short>> _columnByDetailColumn { get; } = new Dictionary<string, List<short>>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

  private List<PX.Objects.AP.InvoiceRecognition.Feedback.ColumnSelected> _columnSelected { get; } = new List<PX.Objects.AP.InvoiceRecognition.Feedback.ColumnSelected>();

  private List<PX.Objects.AP.InvoiceRecognition.Feedback.ColumnUnbound> _columnUnbound { get; } = new List<PX.Objects.AP.InvoiceRecognition.Feedback.ColumnUnbound>();

  public IEnumerable<KeyValuePair<short, short>> RowByDetailRow
  {
    get => (IEnumerable<KeyValuePair<short, short>>) this._rowByDetailRow;
  }

  public IEnumerable<PX.Objects.AP.InvoiceRecognition.Feedback.ColumnSelected> ColumnSelected
  {
    get => (IEnumerable<PX.Objects.AP.InvoiceRecognition.Feedback.ColumnSelected>) this._columnSelected;
  }

  public IEnumerable<PX.Objects.AP.InvoiceRecognition.Feedback.ColumnUnbound> ColumnUnbound
  {
    get => (IEnumerable<PX.Objects.AP.InvoiceRecognition.Feedback.ColumnUnbound>) this._columnUnbound;
  }

  public TableContext(short page, short table)
  {
    this.Page = page;
    this.Table = table;
  }

  public void RegisterColumnSelected(string detailColumn, List<short> columns)
  {
    List<short> shortList;
    if (this._columnByDetailColumn.TryGetValue(detailColumn, out shortList))
    {
      if (columns.Count <= shortList.Count)
        return;
      this._columnByDetailColumn[detailColumn] = columns;
      this._columnSelected.Add(new PX.Objects.AP.InvoiceRecognition.Feedback.ColumnSelected(detailColumn, columns));
    }
    else
    {
      this._columnByDetailColumn.Add(detailColumn, columns);
      this._columnSelected.Add(new PX.Objects.AP.InvoiceRecognition.Feedback.ColumnSelected(detailColumn, columns));
    }
  }

  public void RegisterColumnUnbound(string detailColumn)
  {
    if (this._columnByDetailColumn.ContainsKey(detailColumn))
      this._columnByDetailColumn.Remove(detailColumn);
    this._columnUnbound.Add(new PX.Objects.AP.InvoiceRecognition.Feedback.ColumnUnbound(detailColumn));
  }

  public void RegisterRowBound(short detailRow, short row) => this._rowByDetailRow[detailRow] = row;

  public bool CanBeBounded(CellBound cellBound)
  {
    short num;
    List<short> existingColumns;
    return (int) cellBound.Page == (int) this.Page && (int) cellBound.Table == (int) this.Table && (!this._rowByDetailRow.TryGetValue(cellBound.DetailRow, out num) || (int) num == (int) cellBound.Row) && (!this._columnByDetailColumn.TryGetValue(cellBound.DetailColumn, out existingColumns) || (cellBound.Columns.Count != 1 ? 0 : (cellBound.Columns[0] == (short) -1 ? 1 : 0)) != 0 || existingColumns.TrueForAll((Predicate<short>) (c => cellBound.Columns.Contains(c))) || cellBound.Columns.TrueForAll((Predicate<short>) (c => existingColumns.Contains(c))));
  }
}
