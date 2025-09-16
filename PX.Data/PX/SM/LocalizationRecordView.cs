// Decompiled with JetBrains decompiler
// Type: PX.SM.LocalizationRecordView
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

/// <exclude />
internal class LocalizationRecordView(TranslationMaint graph, bool isReadOnly, BqlCommand select) : 
  PXView((PXGraph) graph, isReadOnly, select)
{
  public override List<object> Select(
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
    TranslationMaint graph = (TranslationMaint) this.Graph;
    if (graph.LocalizationRecordCache.Current != null && graph.LocalizationRecordCache.Current.SelectQueries != null)
    {
      foreach (PXCommandKey pxCommandKey in graph.LocalizationRecordCache.Current.SelectQueries.Keys.Where<PXCommandKey>((Func<PXCommandKey, bool>) (k => !this.SelectQueries.TryGetValue(k, out PXQueryResult _))))
      {
        PXQueryResult v;
        graph.LocalizationRecordCache.Current.SelectQueries.TryGetValue(pxCommandKey, out v);
        this.StoreCached(pxCommandKey, v.Items);
      }
    }
    int startRow1 = 0;
    List<object> list = base.Select(currents, parameters, (object[]) null, sortcolumns, descendings, (PXFilterRow[]) null, ref startRow1, maximumRows, ref totalRows);
    if (graph.LocalizationRecordCache.Current != null)
      graph.LocalizationRecordCache.Current.SelectQueries = this.SelectQueries;
    this.FilterResult(list, filters);
    bool resetTopCount = false;
    PXView.PXSearchColumn[] sorts = this.prepareSorts(sortcolumns, descendings, searches, maximumRows, out bool _, out bool _, ref resetTopCount);
    if (maximumRows == 0)
      maximumRows = -1;
    return this.SearchResult(list, sorts, false, false, ref startRow, maximumRows, ref totalRows, out bool _);
  }
}
