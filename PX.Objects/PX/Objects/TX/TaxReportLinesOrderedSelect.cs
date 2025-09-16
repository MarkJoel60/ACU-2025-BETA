// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxReportLinesOrderedSelect
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.TX;

/// <summary>
/// A <see cref="T:PX.Objects.TX.TaxReportMaint" /> graph's custom view for tax report lines which extends the <see cref="T:PX.Data.PXOrderedSelect`4" />.
/// The base view allows drag and drop functionality in the grid but auto reenumerate lines which is undesirable for tax report lines, so this derived view customize enumeration process.
/// </summary>
public class TaxReportLinesOrderedSelect : 
  PXOrderedSelect<TaxReport, TaxReportLine, Where<TaxReportLine.vendorID, Equal<Current<TaxReport.vendorID>>, And<TaxReportLine.taxReportRevisionID, Equal<Current<TaxReport.revisionID>>, And<Where2<Where<Current<TaxReport.showNoTemp>, Equal<False>, And<TaxReportLine.tempLineNbr, IsNull>>, Or<Where<Current<TaxReport.showNoTemp>, Equal<True>, And2<Where<TaxReportLine.tempLineNbr, IsNull, And<TaxReportLine.tempLine, Equal<False>>>, Or<TaxReportLine.tempLineNbr, IsNotNull>>>>>>>>, OrderBy<Asc<TaxReportLine.sortOrder, Asc<TaxReportLine.taxZoneID>>>>
{
  private readonly TaxReportMaint taxReportGraph;

  public bool AllowDragDrop { get; set; } = true;

  public bool AllowResetOrder { get; set; }

  public bool AllowAutoRenumbering { get; set; }

  public TaxReportLinesOrderedSelect(PXGraph graph)
    : base(graph)
  {
    ((PXOrderedSelectBase<TaxReport, TaxReportLine>) this).RenumberTailOnDelete = false;
    this.taxReportGraph = graph as TaxReportMaint;
  }

  public TaxReportLinesOrderedSelect(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
    ((PXOrderedSelectBase<TaxReport, TaxReportLine>) this).RenumberTailOnDelete = false;
    this.taxReportGraph = graph as TaxReportMaint;
  }

  public void ArrowUpForCurrentRow() => this.MoveRowByArrow(true);

  public void ArrowDownForCurrentRow() => this.MoveRowByArrow(false);

  private void MoveRowByArrow(bool moveUp)
  {
    TaxReportLine currentLine = ((PXSelectBase<TaxReportLine>) this).Current;
    if (!this.AllowDragDrop || currentLine == null)
      return;
    IEnumerable<TaxReportLine> source = GraphHelper.RowCast<TaxReportLine>((IEnumerable) ((PXSelectBase<TaxReportLine>) this).Select(Array.Empty<object>())).Where<TaxReportLine>((Func<TaxReportLine, bool>) (line => !((PXSelectBase) this).Cache.ObjectsEqual((object) line, (object) currentLine)));
    TaxReportLine taxReportLine = !moveUp ? source.Where<TaxReportLine>((Func<TaxReportLine, bool>) (line =>
    {
      int? sortOrder1 = line.SortOrder;
      int? sortOrder2 = currentLine.SortOrder;
      return sortOrder1.GetValueOrDefault() > sortOrder2.GetValueOrDefault() & sortOrder1.HasValue & sortOrder2.HasValue;
    })).OrderBy<TaxReportLine, int?>((Func<TaxReportLine, int?>) (line => line.SortOrder)).FirstOrDefault<TaxReportLine>() : source.Where<TaxReportLine>((Func<TaxReportLine, bool>) (line =>
    {
      int? sortOrder3 = line.SortOrder;
      int? sortOrder4 = currentLine.SortOrder;
      return sortOrder3.GetValueOrDefault() < sortOrder4.GetValueOrDefault() & sortOrder3.HasValue & sortOrder4.HasValue;
    })).OrderByDescending<TaxReportLine, int?>((Func<TaxReportLine, int?>) (line => line.SortOrder)).FirstOrDefault<TaxReportLine>();
    if (taxReportLine == null)
      return;
    int? sortOrder = taxReportLine.SortOrder;
    taxReportLine.SortOrder = currentLine.SortOrder;
    currentLine.SortOrder = sortOrder;
    GraphHelper.SmartSetStatus(((PXSelectBase) this).Cache, (object) currentLine, (PXEntryStatus) 1, (PXEntryStatus) 0);
    GraphHelper.SmartSetStatus(((PXSelectBase) this).Cache, (object) taxReportLine, (PXEntryStatus) 1, (PXEntryStatus) 0);
    this.RenumberDetailTaxLines((IList<TaxReportLine>) new TaxReportLine[0], (IList<TaxReportLine>) new TaxReportLine[2]
    {
      currentLine,
      taxReportLine
    });
    ((PXSelectBase) this).Cache.IsDirty = true;
  }

  protected virtual IEnumerable ResetOrder(PXAdapter adapter)
  {
    return !this.AllowResetOrder ? adapter.Get() : ((PXOrderedSelectBase<TaxReport, TaxReportLine>) this).ResetOrder(adapter);
  }

  protected virtual IEnumerable PasteLine(PXAdapter adapter)
  {
    return !this.AllowDragDrop ? adapter.Get() : ((PXOrderedSelectBase<TaxReport, TaxReportLine>) this).PasteLine(adapter);
  }

  protected virtual void PasteLines(TaxReportLine focus, IList<TaxReportLine> moved)
  {
    if (!((PXSelectBase) this).Cache.AllowUpdate)
      return;
    int? nullable;
    int num1;
    if (focus == null)
    {
      num1 = 1;
    }
    else
    {
      nullable = focus.SortOrder;
      num1 = !nullable.HasValue ? 1 : 0;
    }
    if (num1 != 0 || moved.Count == 0)
      return;
    nullable = focus.SortOrder;
    int num2 = nullable.Value;
    int num3 = moved.Min<TaxReportLine>((Func<TaxReportLine, int>) (line => line.SortOrder.Value));
    if (num2 == num3)
      return;
    int num4;
    int num5;
    if (num2 > num3)
    {
      num4 = num3;
      num5 = num2;
    }
    else
    {
      num4 = num2;
      num5 = moved.Max<TaxReportLine>((Func<TaxReportLine, int>) (line => line.SortOrder.Value));
    }
    HashSet<int> hashSet = moved.Select<TaxReportLine, int>((Func<TaxReportLine, int>) (line => line.LineNbr.Value)).ToHashSet<int>();
    List<TaxReportLine> taxReportLineList1 = new List<TaxReportLine>();
    List<TaxReportLine> taxReportLineList2 = new List<TaxReportLine>(moved.Count);
    List<TaxReportLine> taxReportLineList3 = new List<TaxReportLine>();
    foreach (PXResult<TaxReportLine> pxResult in ((PXSelectBase<TaxReportLine>) this).Select(Array.Empty<object>()))
    {
      TaxReportLine taxReportLine = PXResult<TaxReportLine>.op_Implicit(pxResult);
      HashSet<int> intSet = hashSet;
      nullable = taxReportLine.LineNbr;
      int num6 = nullable.Value;
      if (intSet.Contains(num6))
      {
        taxReportLineList2.Add(taxReportLine);
      }
      else
      {
        nullable = taxReportLine.SortOrder;
        int num7 = num4;
        if (nullable.GetValueOrDefault() >= num7 & nullable.HasValue)
        {
          if (((PXSelectBase) this).Cache.InsertPositionMode)
          {
            nullable = taxReportLine.SortOrder;
            int num8 = num5;
            if (nullable.GetValueOrDefault() <= num8 & nullable.HasValue)
              taxReportLineList1.Add(taxReportLine);
            else
              taxReportLineList3.Add(taxReportLine);
          }
          else
          {
            nullable = taxReportLine.SortOrder;
            int num9 = num5;
            if (nullable.GetValueOrDefault() < num9 & nullable.HasValue)
              taxReportLineList1.Add(taxReportLine);
            else
              taxReportLineList3.Add(taxReportLine);
          }
        }
      }
    }
    ((PXOrderedSelectBase<TaxReport, TaxReportLine>) this).ReEnumerateOnPasteLines(taxReportLineList1, taxReportLineList2, taxReportLineList3, num3, num2);
    ((PXSelectBase) this).View.Clear();
  }

  protected virtual void ReEnumerateOnPasteLines(
    List<TaxReportLine> betweenLines,
    List<TaxReportLine> movedLines,
    List<TaxReportLine> restLines,
    int firstRowPos,
    int insertPos)
  {
    if (this.AllowAutoRenumbering)
    {
      ((PXOrderedSelectBase<TaxReport, TaxReportLine>) this).ReEnumerateOnPasteLines(betweenLines, movedLines, restLines, firstRowPos, insertPos);
    }
    else
    {
      this.RenumberMainTaxLines(betweenLines, movedLines, firstRowPos > insertPos);
      this.RenumberDetailTaxLines((IList<TaxReportLine>) betweenLines, (IList<TaxReportLine>) movedLines);
      if (movedLines.Count <= 0 || betweenLines.Count <= 0)
        return;
      ((PXSelectBase) this).Cache.IsDirty = true;
    }
  }

  private void RenumberMainTaxLines(
    List<TaxReportLine> betweenLines,
    List<TaxReportLine> movedLines,
    bool moveUp)
  {
    IOrderedEnumerable<TaxReportLine> source = moveUp ? betweenLines.OrderByDescending<TaxReportLine, int?>((Func<TaxReportLine, int?>) (line => line.SortOrder)) : betweenLines.OrderBy<TaxReportLine, int?>((Func<TaxReportLine, int?>) (line => line.SortOrder));
    IEnumerable<TaxReportLine> taxReportLines = (IEnumerable<TaxReportLine>) movedLines;
    if (movedLines.Count > 1)
      taxReportLines = moveUp ? (IEnumerable<TaxReportLine>) movedLines.OrderBy<TaxReportLine, int?>((Func<TaxReportLine, int?>) (line => line.SortOrder)) : (IEnumerable<TaxReportLine>) movedLines.OrderByDescending<TaxReportLine, int?>((Func<TaxReportLine, int?>) (line => line.SortOrder));
    Func<TaxReportLine, TaxReportLine, bool> betweenLinesFilterBySortOrder = !moveUp ? (Func<TaxReportLine, TaxReportLine, bool>) ((betweenLine, movedLine) =>
    {
      int? sortOrder1 = betweenLine.SortOrder;
      int? sortOrder2 = movedLine.SortOrder;
      return sortOrder1.GetValueOrDefault() > sortOrder2.GetValueOrDefault() & sortOrder1.HasValue & sortOrder2.HasValue;
    }) : (Func<TaxReportLine, TaxReportLine, bool>) ((betweenLine, movedLine) =>
    {
      int? sortOrder3 = betweenLine.SortOrder;
      int? sortOrder4 = movedLine.SortOrder;
      return sortOrder3.GetValueOrDefault() < sortOrder4.GetValueOrDefault() & sortOrder3.HasValue & sortOrder4.HasValue;
    });
    foreach (TaxReportLine taxReportLine1 in taxReportLines)
    {
      TaxReportLine movedLine = taxReportLine1;
      foreach (TaxReportLine taxReportLine2 in source.Where<TaxReportLine>((Func<TaxReportLine, bool>) (line => betweenLinesFilterBySortOrder(line, movedLine))))
      {
        int? sortOrder = taxReportLine2.SortOrder;
        taxReportLine2.SortOrder = movedLine.SortOrder;
        movedLine.SortOrder = sortOrder;
      }
    }
    EnumerableExtensions.ForEach<TaxReportLine>(betweenLines.Concat<TaxReportLine>((IEnumerable<TaxReportLine>) movedLines), (Action<TaxReportLine>) (line => GraphHelper.SmartSetStatus(((PXSelectBase) this).Cache, (object) line, (PXEntryStatus) 1, (PXEntryStatus) 0)));
  }

  private void RenumberDetailTaxLines(
    IList<TaxReportLine> betweenLines,
    IList<TaxReportLine> movedLines)
  {
    TaxReport current = ((PXSelectBase<TaxReport>) this.taxReportGraph.Report).Current;
    if (current == null)
      return;
    Dictionary<int, TaxReportLine> dictionary = betweenLines.Concat<TaxReportLine>((IEnumerable<TaxReportLine>) movedLines).Where<TaxReportLine>((Func<TaxReportLine, bool>) (line => !line.TempLineNbr.HasValue && line.TempLine.GetValueOrDefault())).ToDictionary<TaxReportLine, int>((Func<TaxReportLine, int>) (line => line.LineNbr.Value));
    if (dictionary.Count == 0)
      return;
    foreach (IGrouping<int, TaxReportLine> grouping in GraphHelper.RowCast<TaxReportLine>((IEnumerable) PXSelectBase<TaxReportLine, PXSelect<TaxReportLine, Where<TaxReportLine.vendorID, Equal<Required<TaxReport.vendorID>>, And<TaxReportLine.tempLineNbr, IsNotNull, And<TaxReportLine.tempLineNbr, In<Required<TaxReportLine.tempLineNbr>>>>>>.Config>.Select((PXGraph) this.taxReportGraph, new object[2]
    {
      (object) current.VendorID,
      (object) dictionary.Keys.ToArray<int>()
    })).GroupBy<TaxReportLine, int>((Func<TaxReportLine, int>) (line => line.TempLineNbr.Value)))
    {
      TaxReportLine taxReportLine1;
      if (dictionary.TryGetValue(grouping.Key, out taxReportLine1))
      {
        foreach (TaxReportLine taxReportLine2 in (IEnumerable<TaxReportLine>) grouping)
        {
          taxReportLine2.SortOrder = taxReportLine1.SortOrder;
          GraphHelper.SmartSetStatus(((PXSelectBase) this).Cache, (object) taxReportLine2, (PXEntryStatus) 1, (PXEntryStatus) 0);
        }
      }
    }
  }

  protected virtual void OnRowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    TaxReportLine row = e.Row as TaxReportLine;
    if (((PXSelectBase<TaxReport>) this.taxReportGraph.Report).Current == null || row == null)
      return;
    int? nullable1 = row.SortOrder;
    if (nullable1.HasValue)
      return;
    List<TaxReportLine> list = GraphHelper.RowCast<TaxReportLine>((IEnumerable) ((PXSelectBase<TaxReportLine>) this).Select(Array.Empty<object>())).Where<TaxReportLine>((Func<TaxReportLine, bool>) (line => line.SortOrder.HasValue)).ToList<TaxReportLine>();
    TaxReportLine taxReportLine = row;
    int? nullable2;
    if (list.Count <= 0)
    {
      nullable2 = row.LineNbr;
    }
    else
    {
      nullable1 = list.Max<TaxReportLine>((Func<TaxReportLine, int?>) (line => line.SortOrder));
      nullable2 = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + 1) : new int?();
    }
    taxReportLine.SortOrder = nullable2;
  }

  protected virtual void OnBeforeGraphPersist(PXGraph graph)
  {
    if (!this.AllowAutoRenumbering)
      return;
    ((PXOrderedSelectBase<TaxReport, TaxReportLine>) this).OnBeforeGraphPersist(graph);
  }

  public virtual void RenumberAll()
  {
    if (!this.AllowAutoRenumbering)
      return;
    ((PXOrderedSelectBase<TaxReport, TaxReportLine>) this).RenumberAll();
  }

  public virtual void RenumberAll(IComparer<PXResult> comparer)
  {
    if (!this.AllowAutoRenumbering)
      return;
    ((PXOrderedSelectBase<TaxReport, TaxReportLine>) this).RenumberAll(comparer);
  }

  public virtual void RenumberTail()
  {
    if (!this.AllowAutoRenumbering)
      return;
    ((PXOrderedSelectBase<TaxReport, TaxReportLine>) this).RenumberTail();
  }
}
