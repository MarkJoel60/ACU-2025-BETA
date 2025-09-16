// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Export.SyImport.GrowingTable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Api.Export.SyImport;

internal class GrowingTable : IGrowingTable
{
  private readonly List<GrowingTable.TableRow> Rows = new List<GrowingTable.TableRow>()
  {
    new GrowingTable.TableRow()
  };
  private const int CurrentRowIndex = 0;

  public void MoveNextRow() => this.Rows.RemoveAt(0);

  public bool IsLastRow() => this.Rows.Count - 1 == 0;

  public void AddSelectResults(ViewSelectResults selectResults)
  {
    GrowingTable.TableRow currentRow = this.CurrentRow;
    if (selectResults.Count == 0)
      currentRow.Views.Add(new NativeRowWrapper(selectResults, (object) null));
    else if (selectResults.Count == 1)
    {
      currentRow.Views.Add(selectResults.First<NativeRowWrapper>());
    }
    else
    {
      List<GrowingTable.TableRow> collection = new List<GrowingTable.TableRow>();
      foreach (NativeRowWrapper selectResult in (List<NativeRowWrapper>) selectResults)
      {
        GrowingTable.TableRow tableRow = currentRow.Clone();
        collection.Add(tableRow);
        tableRow.Views.Add(selectResult);
      }
      this.Rows.RemoveAt(0);
      this.Rows.InsertRange(0, (IEnumerable<GrowingTable.TableRow>) collection);
    }
  }

  private GrowingTable.TableRow CurrentRow
  {
    get => 0 < this.Rows.Count ? this.Rows[0] : (GrowingTable.TableRow) null;
  }

  public bool HasEnumValues
  {
    get => this.CurrentRow != null && this.CurrentRow.HasEnumValues;
    set
    {
      if (this.CurrentRow == null)
        return;
      this.CurrentRow.HasEnumValues = value;
    }
  }

  public int? CountResultsForView(string viewId)
  {
    GrowingTable.TableRow currentRow = this.CurrentRow;
    if (currentRow == null)
      return new int?();
    return currentRow.Views.FirstOrDefault<NativeRowWrapper>((Func<NativeRowWrapper, bool>) (g => string.Equals(g.ViewId, viewId, StringComparison.OrdinalIgnoreCase)))?.Select.Count;
  }

  public void ExportValues(SyImportProcessor.SyExternalValues dic)
  {
    if (this.CurrentRow == null)
      return;
    foreach (FieldValue cell in this.CurrentRow.Cells)
      dic[cell.FieldId] = Convert.ToString(cell.Value);
  }

  public List<NativeRowWrapper> GetRowList() => this.CurrentRow.Views;

  public bool IsEmptyViewResults(string viewId)
  {
    return this.CountResultsForView(viewId).HasValue && !this.CurrentRow.Cells.Any<FieldValue>((Func<FieldValue, bool>) (c => string.Equals(c.ViewId, viewId, StringComparison.OrdinalIgnoreCase)));
  }

  public NativeRowWrapper GetNativeRow(string viewName)
  {
    return this.CurrentRow.Views.First<NativeRowWrapper>((Func<NativeRowWrapper, bool>) (v => string.Equals(v.ViewId, viewName, StringComparison.OrdinalIgnoreCase)));
  }

  public class TableRow
  {
    public readonly List<NativeRowWrapper> Views = new List<NativeRowWrapper>();
    public bool HasEnumValues;

    public IEnumerable<FieldValue> Cells
    {
      get
      {
        return this.Views.SelectMany<NativeRowWrapper, FieldValue>((Func<NativeRowWrapper, IEnumerable<FieldValue>>) (g => (IEnumerable<FieldValue>) g.FieldValues));
      }
    }

    public GrowingTable.TableRow Clone()
    {
      GrowingTable.TableRow tableRow = new GrowingTable.TableRow();
      tableRow.HasEnumValues = this.HasEnumValues;
      tableRow.Views.AddRange((IEnumerable<NativeRowWrapper>) this.Views);
      return tableRow;
    }
  }
}
