// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.ViewExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.Extensions;

[PXInternalUseOnly]
public static class ViewExtensions
{
  /// <summary>
  /// The method that can be used to create a view from the specified select type or
  /// get an existing view with the specified name and create an instance of this select type in both cases as a result.
  /// </summary>
  /// <remarks>
  /// <typeparamref name="TSelect" /> must be non abstract or to be <see cref="T:PX.Data.PXSelectBase`1" /> or <see cref="T:PX.Data.PXSelectBase" />.
  /// If the <see cref="T:PX.Data.PXSelectBase`1" /> provided as <typeparamref name="TSelect" /> than
  /// <see cref="T:PX.Data.PXSelect`1" /> would be returned. If it is non generic <see cref="T:PX.Data.PXSelectBase" />
  /// than resulting <see cref="T:PX.Data.PXSelect`1" /> would be for the primary type of existing view,
  /// if view doesn't exist either, that it would be for the one lightweight DAC.
  /// In both cases if view doesn't exist it is considered as dummy and will return empty array (via view delegate).
  /// This method could be helpful if graph extension adds some hidden view that could (but not must) be overriden
  /// in multiple child extensions and all such views should be independent of each other.
  /// Such views could overriden by the common override views way:
  /// just add field of type <see cref="T:PX.Data.PXSelect`1" /> with name <paramref name="viewName" /> to the any graph extension for specified graph.
  /// </remarks>
  /// <typeparam name="TSelect">The desired select type, which
  /// must be non-abstract or be <see cref="T:PX.Data.PXSelectBase`1" /> or <see cref="T:PX.Data.PXSelectBase" />.
  /// Also it must have a constructor with one parameter of the <see cref="T:PX.Data.PXGraph" /> type.</typeparam>
  /// <param name="graph">The graph.</param>
  /// <param name="viewName">The view name on which the desired <typeparamref name="TSelect" /> is based.
  /// If this view is not presented in <see cref="F:PX.Data.PXGraph.Views" />, the new view with this name is created.</param>
  /// <param name="initializer">The initializer of the desired select.
  /// You can provide <see cref="T:PX.Data.PXSelectBase`1" /> as <typeparamref name="TSelect" />
  /// and create a concrete select via initializer.</param>
  /// <returns>The desired select.</returns>
  /// <exception cref="T:PX.Data.PXException">Is raised if <typeparamref name="TSelect" /> is abstract and is not <see cref="T:PX.Data.PXSelectBase`1" /> or <see cref="T:PX.Data.PXSelectBase" />
  /// or if the desired <typeparamref name="TSelect" /> cannot be instantiated for any reason.</exception>
  public static TSelect GetOrCreateSelectFromView<TSelect>(
    this PXGraph graph,
    string viewName,
    Func<PXSelectBase> initializer = null)
    where TSelect : PXSelectBase
  {
    try
    {
      PXView view;
      if (((Dictionary<string, PXView>) graph.Views).TryGetValue(viewName, out view))
        return DefaultInitializer(view);
      if (initializer == null)
        initializer = (Func<PXSelectBase>) (() => (PXSelectBase) DefaultInitializer());
      PXSelectBase pxSelectBase = initializer();
      graph.Views.Add(viewName, pxSelectBase.View);
      return pxSelectBase is TSelect select ? select : DefaultInitializer(pxSelectBase.View);
    }
    catch (Exception ex)
    {
      object[] objArray = new object[2]
      {
        (object) typeof (TSelect).Name,
        (object) viewName
      };
      throw new PXException(ex, "Cannot initialize select {0} for view {1}.", objArray);
    }

    TSelect DefaultInitializer(PXView view = null)
    {
      System.Type type1 = typeof (TSelect);
      bool flag = false;
      if (type1.IsAbstract)
      {
        System.Type type2;
        if (type1.IsGenericType && type1.GetGenericTypeDefinition() == typeof (PXSelectBase<>))
          type2 = type1.GenericTypeArguments[0];
        else if (type1 == typeof (PXSelectBase))
        {
          System.Type type3 = view?.GetItemType();
          if ((object) type3 == null)
            type3 = typeof (CRSetup);
          type2 = type3;
        }
        else
          throw new PXArgumentException(nameof (TSelect), PXMessages.LocalizeFormatNoPrefixNLA("Cannot initialize the following select for the {1} view because the select must be non-abstract, PXSelectBase, or PXSelectBase<Table>: {0}.", new object[2]
          {
            (object) typeof (TSelect),
            (object) viewName
          }));
        type1 = typeof (PXSelect<>).MakeGenericType(type2);
        flag = view == null;
      }
      TSelect instance = (TSelect) Activator.CreateInstance(type1, (object) graph);
      if (flag)
      {
        // ISSUE: method pointer
        view = new PXView(graph, true, instance.View.BqlSelect, (Delegate) new PXSelectDelegate((object) null, __methodptr(Empty<object>)));
      }
      if (view != null)
        instance.View = view;
      return instance;
    }
  }

  public static List<object> SelectWithViewContext(this PXView view, params object[] parameters)
  {
    int startRow = PXView.StartRow;
    int num = 0;
    List<object> objectList = view.Select(PXView.Currents, parameters ?? PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num);
    PXView.StartRow = 0;
    return objectList;
  }

  public static IEnumerable<PXResult<T>> SelectChunked<T>(
    this PXSelectBase<T> select,
    int chunkSize = 100,
    params object[] parameters)
    where T : class, IBqlTable, new()
  {
    if (chunkSize <= 0)
      throw new ArgumentOutOfRangeException(nameof (chunkSize));
    return select.SelectChunked<T>(parameters: parameters, chunkSize: chunkSize);
  }

  public static IEnumerable<PXResult<T>> SelectChunked<T>(
    this PXSelectBase<T> select,
    object[] currents = null,
    object[] parameters = null,
    object[] searches = null,
    string[] sortcolumns = null,
    bool[] descendings = null,
    PXFilterRow[] filters = null,
    int chunkSize = 100)
    where T : class, IBqlTable, new()
  {
    if (chunkSize <= 0)
      throw new ArgumentOutOfRangeException(nameof (chunkSize));
    return Select().SelectMany<IEnumerable<PXResult<T>>, PXResult<T>>((Func<IEnumerable<PXResult<T>>, IEnumerable<PXResult<T>>>) (i => i));

    IEnumerable<IEnumerable<PXResult<T>>> Select()
    {
      int startRow = 0;
      int limit = 0;
      int count;
      do
      {
        yield return select.SelectExtended<T>(out count, currents, parameters, searches, sortcolumns, descendings, filters, startRow, chunkSize);
        startRow = count;
        limit += chunkSize;
      }
      while (count >= limit);
    }
  }

  public static IEnumerable<PXResult<T>> SelectExtended<T>(
    this PXSelectBase<T> select,
    out int totalRows,
    object[] currents = null,
    object[] parameters = null,
    object[] searches = null,
    string[] sortcolumns = null,
    bool[] descendings = null,
    PXFilterRow[] filters = null,
    int startRow = 0,
    int maxRow = 0)
    where T : class, IBqlTable, new()
  {
    int num = startRow;
    totalRows = 0;
    List<object> list = ((PXSelectBase) select).View.Select(currents, parameters, searches, sortcolumns, descendings, filters, ref num, maxRow, ref totalRows);
    return Select();

    IEnumerable<PXResult<T>> Select()
    {
      foreach (object item in list)
      {
        if (item is PXResult<T> pxResult)
          yield return pxResult;
        if (item is T obj)
          yield return new PXResult<T>(obj);
      }
    }
  }

  public static IEnumerable<PXResult<T>> SelectExtended<T>(
    this PXSelectBase<T> select,
    object[] currents = null,
    object[] parameters = null,
    object[] searches = null,
    string[] sortcolumns = null,
    bool[] descendings = null,
    PXFilterRow[] filters = null,
    int startRow = 0,
    int maxRow = 0)
    where T : class, IBqlTable, new()
  {
    return select.SelectExtended<T>(out int _, currents, parameters, searches, sortcolumns, descendings, filters, startRow, maxRow);
  }

  /// <summary>
  /// Clones items of the view to caches of provided Quote graph
  /// </summary>
  /// <param name="view">View, which content should be copied</param>
  /// <param name="graph">target Quote graph</param>
  /// <param name="quoteId">ID of Quote we started to create</param>
  /// <param name="currencyInfo">Currency info, created for the Uuota</param>
  /// <param name="keyField">key field, which should be cleared before insertion</param>
  public static void CloneView(
    this PXView view,
    PXGraph graph,
    Guid? quoteId,
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo,
    string keyField = null)
  {
    System.Type itemType = view.Cache.GetItemType();
    PXCache cach = graph.Caches[view.Cache.GetItemType()];
    cach.Clear();
    foreach (object obj1 in view.SelectMulti(Array.Empty<object>()))
    {
      object obj2 = (object) PXResult.Unwrap(obj1, itemType);
      object obj3 = view.Cache.CreateCopy(obj2);
      view.Cache.SetValue<CROpportunityProducts.quoteID>(obj3, (object) quoteId);
      view.Cache.SetValue<CROpportunityProducts.curyInfoID>(obj3, (object) currencyInfo.CuryInfoID);
      if (view.Cache.Fields.Contains("NoteID"))
        view.Cache.SetValue(obj3, "NoteID", (object) Guid.NewGuid());
      if (!string.IsNullOrEmpty(keyField) && view.Cache.Fields.Contains(keyField))
      {
        view.Cache.SetValue(obj3, keyField, (object) null);
        obj3 = cach.Insert(obj3);
      }
      else
        cach.SetStatus(obj3, (PXEntryStatus) 2);
      cach.Current = obj3;
      if (PXNoteAttribute.GetNoteIDIfExists(view.Cache, obj2).HasValue)
      {
        string note = PXNoteAttribute.GetNote(view.Cache, obj2);
        Guid[] fileNotes = PXNoteAttribute.GetFileNotes(view.Cache, obj2);
        PXNoteAttribute.SetNote(cach, obj3, note);
        PXNoteAttribute.SetFileNotes(cach, obj3, fileNotes);
      }
    }
  }
}
