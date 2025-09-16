// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.ViewSettings
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

public class ViewSettings
{
  private int? _previousMaximumRows;
  private int? _previousStartRow;

  public static ViewSettings FromCurrentContext()
  {
    return new ViewSettings()
    {
      Currents = ((IEnumerable<object>) PXView.Currents).ToList<object>(),
      Parameters = ((IEnumerable<object>) PXView.Parameters).ToList<object>(),
      Searches = ((IEnumerable<object>) PXView.Searches).ToList<object>(),
      SortColumns = ((IEnumerable<string>) PXView.SortColumns).ToList<string>(),
      Descendings = ((IEnumerable<bool>) PXView.Descendings).ToList<bool>(),
      Filters = PXView.Filters,
      StartRow = PXView.StartRow,
      ReverseOrder = PXView.ReverseOrder,
      MaximumRows = PXView.MaximumRows
    };
  }

  public ViewSettings()
  {
    this.FieldsMap = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
  }

  public List<object> Currents { get; set; }

  public List<object> Parameters { get; set; }

  public List<object> Searches { get; set; }

  public List<string> SortColumns { get; set; }

  public List<bool> Descendings { get; set; }

  public PXView.PXFilterRowCollection Filters { get; set; }

  public bool ReverseOrder { get; set; }

  public int MaximumRows { get; set; }

  public int StartRow { get; set; }

  public Dictionary<string, string> FieldsMap { get; set; }

  public void SelectFromFirstPage()
  {
    this._previousMaximumRows = new int?(this.MaximumRows);
    this._previousStartRow = new int?(this.StartRow);
    this.MaximumRows = !this.ReverseOrder ? this.StartRow + this.MaximumRows : 0;
    this.StartRow = 0;
  }

  public List<TRow> GetCurrentPageRange<TRow>(List<TRow> rows)
  {
    if (!this._previousMaximumRows.HasValue || !this._previousStartRow.HasValue || rows.Count == 0)
      return rows;
    int index;
    int count;
    if (!this.ReverseOrder)
    {
      index = this._previousStartRow.GetValueOrDefault();
      if (index > rows.Count)
        index = rows.Count;
      if (index < 0)
        index = 0;
      count = rows.Count - index;
    }
    else
    {
      index = rows.Count + this._previousStartRow.GetValueOrDefault();
      int num = index + this._previousMaximumRows.GetValueOrDefault();
      if (index < 0)
        index = 0;
      if (num < 0)
        num = 0;
      count = num - index;
    }
    rows = rows.GetRange(index, count);
    return rows;
  }
}
