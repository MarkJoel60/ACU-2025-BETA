// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.DataViewHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

public static class DataViewHelper
{
  private const string GenericInquiryViewName = "Results";

  public static void OpenGenericInquiry(
    string screenID,
    string message,
    PXBaseRedirectException.WindowMode windowMode,
    params DataViewHelper.DataViewFilter[] filters)
  {
    PXBaseRedirectException redirect = DataViewHelper.PrepareGenericInquiryRedirect(screenID, message, windowMode);
    if (redirect != null)
    {
      DataViewHelper.BuildGenericInquiryRedirectFilters(redirect, filters);
      throw redirect;
    }
  }

  public static void OpenDataView(
    PXGraph graph,
    string viewName,
    string message,
    PXBaseRedirectException.WindowMode windowMode,
    params DataViewHelper.DataViewFilter[] filters)
  {
    PXBaseRedirectException redirect = DataViewHelper.PrepareRedirect(graph, message, windowMode);
    if (redirect != null)
    {
      DataViewHelper.BuildRedirectFilters(redirect, viewName, filters);
      throw redirect;
    }
  }

  private static void BuildGenericInquiryRedirectFilters(
    PXBaseRedirectException redirect,
    DataViewHelper.DataViewFilter[] filters)
  {
    DataViewHelper.BuildRedirectFilters(redirect, "Results", filters);
  }

  private static void BuildRedirectFilters(
    PXBaseRedirectException redirect,
    string viewName,
    DataViewHelper.DataViewFilter[] filters)
  {
    if (filters == null)
      return;
    DataViewHelper.DataViewFilter[] array = ((IEnumerable<DataViewHelper.DataViewFilter>) filters).Where<DataViewHelper.DataViewFilter>((Func<DataViewHelper.DataViewFilter, bool>) (x => x.IsActive)).ToArray<DataViewHelper.DataViewFilter>();
    if (array.Length == 0)
      return;
    redirect.Filters.Add(new PXBaseRedirectException.Filter(viewName, ((IEnumerable<DataViewHelper.DataViewFilter>) array).SelectMany<DataViewHelper.DataViewFilter, PXFilterRow>(new Func<DataViewHelper.DataViewFilter, IEnumerable<PXFilterRow>>(DataViewHelper.BuildInquiryFilter)).ToArray<PXFilterRow>()));
  }

  public static PXFilterRow[] ToFilterArray(this DataViewHelper.DataViewFilter filter)
  {
    return DataViewHelper.BuildInquiryFilter(filter).ToArray<PXFilterRow>();
  }

  private static IEnumerable<PXFilterRow> BuildInquiryFilter(DataViewHelper.DataViewFilter filter)
  {
    if (filter.Values == null)
    {
      yield return new PXFilterRow(filter.ColumnName, filter.Condition, filter.Value);
    }
    else
    {
      bool flag1 = true;
      for (int i = 0; i < filter.Values.Length; ++i)
      {
        bool flag2 = i == filter.Values.Length - 1;
        yield return new PXFilterRow()
        {
          DataField = filter.ColumnName,
          Condition = filter.Condition,
          Value = filter.Values[i],
          OrOperator = !flag2,
          OpenBrackets = flag1 ? 1 : 0,
          CloseBrackets = flag2 ? 1 : 0
        };
        flag1 = false;
      }
    }
  }

  private static PXBaseRedirectException PrepareGenericInquiryRedirect(
    string screenID,
    string message,
    PXBaseRedirectException.WindowMode windowMode)
  {
    PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(screenID);
    return mapNodeByScreenId != null && mapNodeByScreenId.Url != null ? (PXBaseRedirectException) new PXRedirectRequiredException(mapNodeByScreenId.Url, (PXGraph) PXGenericInqGrph.CreateInstance(screenID), windowMode, message) : (PXBaseRedirectException) null;
  }

  private static PXBaseRedirectException PrepareRedirect(
    PXGraph graph,
    string message,
    PXBaseRedirectException.WindowMode windowMode)
  {
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException(graph, message);
    ((PXBaseRedirectException) requiredException).Mode = windowMode;
    return (PXBaseRedirectException) requiredException;
  }

  public class DataViewFilter
  {
    public bool IsActive { get; set; } = true;

    public string ColumnName { get; set; }

    public PXCondition Condition { get; set; }

    public object Value { get; set; }

    public object[] Values { get; set; }

    public static DataViewHelper.DataViewFilter Create<TTable, TField>(
      PXCondition condition,
      object value,
      bool isActive = true)
      where TTable : IBqlTable
      where TField : IBqlField
    {
      return DataViewHelper.DataViewFilter.Create($"{typeof (TTable).Name}_{typeof (TField).Name}", condition, value, isActive);
    }

    public static DataViewHelper.DataViewFilter Create<TTable, TField>(
      PXCondition condition,
      object[] values,
      bool isActive = true)
      where TTable : IBqlTable
      where TField : IBqlField
    {
      return DataViewHelper.DataViewFilter.Create($"{typeof (TTable).Name}_{typeof (TField).Name}", condition, values, isActive);
    }

    public static DataViewHelper.DataViewFilter Create(
      string columnName,
      PXCondition condition,
      object value,
      bool isActive = true)
    {
      return new DataViewHelper.DataViewFilter()
      {
        ColumnName = columnName,
        Condition = condition,
        Value = value,
        IsActive = isActive
      };
    }

    public static DataViewHelper.DataViewFilter Create(
      string columnName,
      PXCondition condition,
      object[] values,
      bool isActive = true)
    {
      return new DataViewHelper.DataViewFilter()
      {
        ColumnName = columnName,
        Condition = condition,
        Values = values,
        IsActive = isActive
      };
    }
  }
}
