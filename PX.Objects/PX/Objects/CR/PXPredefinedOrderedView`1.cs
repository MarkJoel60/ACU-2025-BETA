// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PXPredefinedOrderedView`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR;

/// <summary>
/// View extension that is used for overriding the sort order.
/// </summary>
/// <typeparam name="TBqlField">The field that is used for sorting with the highest priority (cannot be overridden)</typeparam>
public class PXPredefinedOrderedView<TBqlField> : PXView where TBqlField : IBqlField
{
  public PXPredefinedOrderedView(PXGraph graph, bool isReadOnly, BqlCommand select)
    : base(graph, isReadOnly, select)
  {
  }

  public PXPredefinedOrderedView(
    PXGraph graph,
    bool isReadOnly,
    BqlCommand select,
    Delegate handler)
    : base(graph, isReadOnly, select, handler)
  {
  }

  public bool IsCompare { get; set; }

  protected virtual int Compare(object a, object b, PXView.compareDelegate[] comparisons)
  {
    this.IsCompare = true;
    int num = base.Compare(a, b, comparisons);
    this.IsCompare = false;
    return num;
  }

  public virtual List<object> Select(
    object[] currents,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    if (searches != null && ((IEnumerable<object>) searches).Where<object>((Func<object, bool>) (x => x != null)).Any<object>())
      return base.Select(currents, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
    List<object> objectList = new List<object>();
    List<string> stringList = new List<string>();
    List<bool> boolList = new List<bool>();
    stringList.Add(typeof (TBqlField).Name);
    boolList.Add(false);
    objectList.Add((object) null);
    if (searches != null)
      objectList.AddRange((IEnumerable<object>) searches);
    if (sortcolumns != null)
      stringList.AddRange((IEnumerable<string>) sortcolumns);
    if (descendings != null)
      boolList.AddRange((IEnumerable<bool>) descendings);
    return base.Select(currents, parameters, objectList.ToArray(), stringList.ToArray(), boolList.ToArray(), filters, ref startRow, maximumRows, ref totalRows);
  }
}
