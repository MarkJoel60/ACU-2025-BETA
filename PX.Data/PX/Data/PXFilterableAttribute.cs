// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFilterableAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>Enables the <strong>Filter Settings</strong> dialog for the grid,
/// in which the user can define and save custom filters and then use them every time this user
/// opens the form.</summary>
/// <remarks>
/// <para>The attribute is placed on the view declaration. If you specify this view as the data member of a grid control,
/// the grid will include a control which can be used to create reusable filters and save them in
/// the database. A reusable filter is a set of conditions checked for the fields selected by the view.
/// When a grid applies a filter it displays only the data records that satisfy the filter's conditions.</para>
/// <para>Reusable filters are frequently enabled in the grid on inquiry and processing forms
/// so that users can customize these forms to show specific data that is
/// most relevant to their needs and responsibilities.</para>
/// </remarks>
/// <example>
///   <code title="" description="" lang="CS">
/// [PXFilterable]
/// public PXSelect&lt;APInvoice&gt; APDocumentList;</code>
/// </example>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class PXFilterableAttribute : PXViewExtensionAttribute
{
  /// <exclude />
  public const string FilterHeaderName = "$FilterHeader";
  /// <exclude />
  public const string FilterRowName = "$FilterRow";
  /// <exclude />
  public const string GlobalFilterRow = "$GlobalFilterRow$";
  protected readonly System.Type[] _autoFill;

  /// <summary>Initializes a new instance of the attribute. The parameter is optional and is not used in most cases. You can specify the DACs whose <tt>Current</tt> objects
  /// will be used to fill in the filter parameters before showing the data view to the user.</summary>
  /// <param name="autoFill">The DACs to be used to fill in the filter parameters.</param>
  public PXFilterableAttribute(params System.Type[] autoFill) => this._autoFill = autoFill;

  /// <exclude />
  public override void ViewCreated(PXGraph graph, string viewName)
  {
    PXFilterView filterView = new PXFilterView(graph, this.NodeId ?? graph.Accessinfo.ScreenID, viewName);
    PXFilterableAttribute.AddFilterView(graph, filterView, viewName);
    PXFilterDetailView filterDetailView = new PXFilterDetailView(graph, viewName, this._autoFill.Length == 0 ? new System.Type[0] : this._autoFill);
    PXFilterableAttribute.AddFilterDetailView(graph, filterDetailView, viewName);
  }

  /// <exclude />
  public static void AddFilterView(PXGraph graph, PXFilterView filterView, string viewName)
  {
    graph.Views[viewName + "$FilterHeader"] = (PXView) filterView;
    PXFilterableAttribute.GiveFullAccessRights(graph.Caches[typeof (FilterHeader)]);
  }

  /// <exclude />
  public static void AddFilterDetailView(
    PXGraph graph,
    PXFilterDetailView filterDetailView,
    string viewName)
  {
    graph.Views[viewName + "$FilterRow"] = (PXView) filterDetailView;
    PXCache cache = filterDetailView.Cache;
    PXFilterableAttribute.GiveFullAccessRights(cache);
    PXUIFieldAttribute.SetEnabled(cache, (string) null, true);
    PXUIFieldAttribute.SetReadOnly(cache, (object) null, false);
  }

  private static void GiveFullAccessRights(PXCache cache)
  {
    cache.SelectRights = true;
    cache.InsertRights = true;
    cache.UpdateRights = true;
    cache.DeleteRights = true;
    cache.AllowSelect = true;
    cache.AllowInsert = true;
    cache.AllowUpdate = true;
    cache.AllowDelete = true;
    foreach (string field in (List<string>) cache.Fields)
    {
      foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes(field))
      {
        if (attribute is PXUIFieldAttribute pxuiFieldAttribute)
          pxuiFieldAttribute.EnableRights = true;
      }
    }
  }

  /// <exclude />
  public string NodeId { get; set; }

  /// <exclude />
  public static void AddFilter(
    string graphType,
    string viewName,
    string filterName,
    PXFilterRow[] filterRows)
  {
    DynamicFilterManager.AddFilter(graphType, viewName, filterName, filterRows);
  }

  /// <summary>Retrieves the filter by the filter ID.</summary>
  /// <param name="graph">A graph instance.</param>
  /// <param name="viewName">The data view name to which the PXFilterable attribute is assigned.</param>
  /// <param name="filterId">The ID of the filter in Acumatica ERP.</param>
  public static PXFilterRow[] GetFilterRows(
    PXGraph graph,
    string viewName,
    Guid filterId,
    bool encloseInBrackets = false)
  {
    List<PXFilterRow> source = new List<PXFilterRow>();
    viewName += "$FilterRow";
    if (!graph.Views.ContainsKey(viewName))
      return source.ToArray();
    PXView view = graph.Views[viewName];
    List<(PXFilterRow, FilterRow.FilterTypeEnum, int)> filterTypes = new List<(PXFilterRow, FilterRow.FilterTypeEnum, int)>();
    PXView pxView = view;
    object[] objArray = new object[1]{ (object) filterId };
    foreach (FilterRow data in pxView.SelectMulti(objArray))
    {
      bool? isUsed = data.IsUsed;
      bool flag = true;
      if (isUsed.GetValueOrDefault() == flag & isUsed.HasValue)
      {
        object obj1 = view.Cache.GetValueExt<FilterRow.valueSt>((object) data);
        object obj2 = view.Cache.GetValueExt<FilterRow.valueSt2>((object) data);
        if (RelativeDatesManager.IsRelativeDatesString(data.ValueSt))
          obj1 = (object) RelativeDatesManager.EvaluateAsDateTime(data.ValueSt);
        if (RelativeDatesManager.IsRelativeDatesString(data.ValueSt2))
          obj2 = (object) RelativeDatesManager.EvaluateAsDateTime(data.ValueSt2);
        string dataField = data.DataField;
        byte? nullable1 = data.Condition;
        int condition = (int) nullable1.Value;
        object obj3 = obj1;
        object obj4 = obj2;
        PXFilterRow pxFilterRow1 = new PXFilterRow(dataField, (PXCondition) condition, obj3, obj4);
        int? nullable2 = data.OpenBrackets;
        if (nullable2.HasValue)
        {
          PXFilterRow pxFilterRow2 = pxFilterRow1;
          nullable2 = data.OpenBrackets;
          int num = nullable2.Value;
          pxFilterRow2.OpenBrackets = num;
        }
        nullable2 = data.CloseBrackets;
        if (nullable2.HasValue)
        {
          PXFilterRow pxFilterRow3 = pxFilterRow1;
          nullable2 = data.CloseBrackets;
          int num = nullable2.Value;
          pxFilterRow3.CloseBrackets = num;
        }
        PXFilterRow pxFilterRow4 = pxFilterRow1;
        nullable2 = data.Operator;
        int num1 = 1;
        int num2 = nullable2.GetValueOrDefault() == num1 & nullable2.HasValue ? 1 : 0;
        pxFilterRow4.OrOperator = num2 != 0;
        source.Add(pxFilterRow1);
        nullable1 = data.FilterType;
        FilterRow.FilterTypeEnum valueOrDefault = (FilterRow.FilterTypeEnum) nullable1.GetValueOrDefault();
        int count = source.Count;
        filterTypes.Add((pxFilterRow1, valueOrDefault, count));
      }
    }
    if (source.Count > 0 & encloseInBrackets)
      PXFilterableAttribute.EncloseInBrackets(source.First<PXFilterRow>(), source.Last<PXFilterRow>());
    PXFilterableAttribute.EncloseAdvancedFilterRows(filterTypes);
    return source.ToArray();
  }

  private static void EncloseInBrackets(PXFilterRow first, PXFilterRow last)
  {
    ++first.OpenBrackets;
    ++last.CloseBrackets;
    last.OrOperator = false;
  }

  private static void EncloseAdvancedFilterRows(
    List<(PXFilterRow FilterRow, FilterRow.FilterTypeEnum FilterType, int Position)> filterTypes)
  {
    (PXFilterRow, FilterRow.FilterTypeEnum, int) firstAdvancedFilter = filterTypes.FirstOrDefault<(PXFilterRow, FilterRow.FilterTypeEnum, int)>((Func<(PXFilterRow, FilterRow.FilterTypeEnum, int), bool>) (x => x.FilterType == FilterRow.FilterTypeEnum.Advanced));
    (PXFilterRow, FilterRow.FilterTypeEnum, int) lastAdvancedFilter = filterTypes.LastOrDefault<(PXFilterRow, FilterRow.FilterTypeEnum, int)>((Func<(PXFilterRow, FilterRow.FilterTypeEnum, int), bool>) (x => x.FilterType == FilterRow.FilterTypeEnum.Advanced));
    if (firstAdvancedFilter.Item3 == lastAdvancedFilter.Item3 || filterTypes.All<(PXFilterRow, FilterRow.FilterTypeEnum, int)>((Func<(PXFilterRow, FilterRow.FilterTypeEnum, int), bool>) (x => x.FilterType == FilterRow.FilterTypeEnum.Advanced)) || filterTypes.Any<(PXFilterRow, FilterRow.FilterTypeEnum, int)>((Func<(PXFilterRow, FilterRow.FilterTypeEnum, int), bool>) (x => x.FilterType != FilterRow.FilterTypeEnum.Advanced && x.Position > firstAdvancedFilter.Item3 && x.Position < lastAdvancedFilter.Item3)))
      return;
    PXFilterableAttribute.EncloseInBrackets(firstAdvancedFilter.Item1, lastAdvancedFilter.Item1);
  }
}
