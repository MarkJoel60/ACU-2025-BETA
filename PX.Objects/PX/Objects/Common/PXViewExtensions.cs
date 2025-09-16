// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.PXViewExtensions
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
namespace PX.Objects.Common;

public static class PXViewExtensions
{
  public static IEnumerable SelectExternal(
    this PXView view,
    ref int startRow,
    ref int totalRows,
    object[] pars = null)
  {
    IEnumerable enumerable = PXViewExtensions.SelectWithExternalParameters(view, ref startRow, ref totalRows, pars);
    PXView.StartRow = 0;
    return enumerable;
  }

  public static IEnumerable SelectExternal(this PXView view, object[] pars = null)
  {
    int startRow = 0;
    int totalRows = 0;
    return PXViewExtensions.SelectWithExternalParameters(view, ref startRow, ref totalRows, pars);
  }

  public static IEnumerable SelectExternalWithPaging(this PXView view, object[] pars = null)
  {
    int startRow = PXView.StartRow;
    int totalRows = 0;
    IEnumerable enumerable = PXViewExtensions.SelectWithExternalParameters(view, ref startRow, ref totalRows, pars);
    PXView.StartRow = 0;
    return enumerable;
  }

  public static IEnumerable SelectWithinContext(this PXView view, object[] pars = null)
  {
    int startRow = PXView.StartRow;
    int num = 0;
    List<object> objectList = view.Select(PXView.Currents, pars ?? PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num);
    PXView.StartRow = 0;
    return (IEnumerable) objectList;
  }

  private static IEnumerable SelectWithExternalParameters(
    PXView view,
    ref int startRow,
    ref int totalRows,
    object[] pars = null)
  {
    return (IEnumerable) view.Select(PXView.Currents, pars ?? PXView.Parameters, PXView.Searches, view.GetExternalSorts(), view.GetExternalDescendings(), view.GetExternalFilters(), ref startRow, PXView.MaximumRows, ref totalRows);
  }

  public static List<PXView.PXSearchColumn> GetContextualExternalSearchColumns(
    this PXView view,
    IEnumerable<PXView.PXSearchColumn> contextualSorts = null)
  {
    string[] strArray = view.GetExternalSorts() ?? new string[0];
    bool[] second = view.GetExternalDescendings() ?? new bool[0];
    HashSet<string> existingSortColumns = new HashSet<string>((IEnumerable<string>) strArray);
    return ((IEnumerable<string>) strArray).Zip<string, bool, PXView.PXSearchColumn>((IEnumerable<bool>) second, (Func<string, bool, PXView.PXSearchColumn>) ((sortColumn, descending) => new PXView.PXSearchColumn(sortColumn, descending, (object) null))).Concat<PXView.PXSearchColumn>((contextualSorts ?? PXView.SearchColumns).Where<PXView.PXSearchColumn>((Func<PXView.PXSearchColumn, bool>) (searchColumn => !existingSortColumns.Contains(((PXView.PXSortColumn) searchColumn).Column)))).ToList<PXView.PXSearchColumn>();
  }

  public static object[] GetSearches(this List<PXView.PXSearchColumn> searchColumns)
  {
    return searchColumns.Select<PXView.PXSearchColumn, object>((Func<PXView.PXSearchColumn, object>) (column => column.SearchValue)).ToArray<object>();
  }

  public static string[] GetSortColumns(this List<PXView.PXSearchColumn> searchColumns)
  {
    return searchColumns.Select<PXView.PXSearchColumn, string>((Func<PXView.PXSearchColumn, string>) (column => ((PXView.PXSortColumn) column).Column)).ToArray<string>();
  }

  public static bool[] GetDescendings(this List<PXView.PXSearchColumn> searchColumns)
  {
    return searchColumns.Select<PXView.PXSearchColumn, bool>((Func<PXView.PXSearchColumn, bool>) (column => ((PXView.PXSortColumn) column).Descending)).ToArray<bool>();
  }

  [PXInternalUseOnly]
  public static WebDialogResult AskOKCancelWithCallback(
    this PXView view,
    object row,
    string header,
    string message,
    MessageIcon icon)
  {
    PXView pxView = view;
    object obj = row;
    string str1 = header;
    string str2 = message;
    Dictionary<WebDialogResult, string> dictionary = new Dictionary<WebDialogResult, string>();
    dictionary[(WebDialogResult) 6] = "OK";
    dictionary[(WebDialogResult) 7] = "Cancel";
    MessageIcon messageIcon = icon;
    WebDialogResult webDialogResult = pxView.Ask(obj, str1, str2, (MessageButtons) 4, (IReadOnlyDictionary<WebDialogResult, string>) dictionary, messageIcon);
    if (webDialogResult != 6)
    {
      if (webDialogResult == 7)
        view.Answer = (WebDialogResult) 2;
    }
    else
      view.Answer = (WebDialogResult) 1;
    return view.Answer;
  }
}
