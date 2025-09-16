// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR.Extensions.Cache;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.Extensions.SideBySideComparison;

/// <summary>
/// The base extension that provides comparison of two sets of items.
/// It shows differences line by line and allows you to select a record from the left or right set (<see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.EntitiesContext" />).
/// </summary>
/// <remarks>
/// The items should be aligned with each other (see <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.EntitiesContext" />).
/// For instance, it could be use to compare set of <see cref="T:PX.Objects.CR.CRLead" /> and related <see cref="T:PX.Objects.CS.CSAnswers" />.
/// Compared entities not have to be of the same type,
/// however it should be presented by same parent type and it must be <see cref="T:PX.Data.IBqlTable" />.
/// For instance, it is possible to compare <see cref="T:PX.Objects.CR.CRLead" /> with <see cref="T:PX.Objects.CR.Contact" />,
/// because <see cref="T:PX.Objects.CR.Contact" /> is a parent of <see cref="T:PX.Objects.CR.CRLead" />.
/// Use for link (see <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.Link.LinkEntitiesExt`3" />)
/// and for merge (see <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.Merge.MergeEntitiesExt`2" />).
/// </remarks>
/// <typeparam name="TGraph">The entry <see cref="T:PX.Data.PXGraph" /> type.</typeparam>
/// <typeparam name="TMain">The primary DAC (a <see cref="T:PX.Data.IBqlTable" /> type) of the <typeparam name="TGraph">graph</typeparam>.</typeparam>
/// <typeparam name="TComparisonRow">The type of <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow" /> that is used by the current extension.</typeparam>
public abstract class CompareEntitiesExt<TGraph, TMain, TComparisonRow> : PXGraphExtension<TGraph>
  where TGraph : PXGraph, new()
  where TMain : class, IBqlTable, new()
  where TComparisonRow : ComparisonRow, new()
{
  /// <summary>
  /// The prefix of generic views that is added by the current type of extension.
  /// </summary>
  /// <remarks>
  /// The property can be used to define hidden views in the base class for different child extensions.
  /// The resulting view name is a concatenation of <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3.ViewPrefix" /> and the <see cref="T:PX.Data.PXSelectBase" />'s property name.
  /// The property is used to built views for the following selects: <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3.ComparisonRows" />, <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3.VisibleComparisonRows" />.
  /// </remarks>
  /// <example>
  /// If ViewPrefix is "Prefix_", the following views are created:
  /// "Prefix_ComparisonRows" for <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3.ComparisonRows" />
  /// and "Prefix_VisibleComparisonRows" for <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3.VisibleComparisonRows" />.
  /// </example>
  protected abstract string ViewPrefix { get; }

  /// <summary>
  /// The view that represents all field comparisons (<see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3.ComparisonRows" />).
  /// </summary>
  public PXSelectBase<TComparisonRow> ComparisonRows { get; protected set; }

  public IEnumerable comparisonRows() => ((PXSelectBase) this.ComparisonRows).Cache.Cached;

  /// <summary>
  /// The view that represents all visible field comparisons (<see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3.ComparisonRows" />).
  /// </summary>
  public PXSelectBase<TComparisonRow> VisibleComparisonRows { get; protected set; }

  public IEnumerable visibleComparisonRows()
  {
    return (IEnumerable) ((IEnumerable<TComparisonRow>) this.ComparisonRows.SelectMain(Array.Empty<object>())).Where<TComparisonRow>((Func<TComparisonRow, bool>) (row => !row.Hidden.GetValueOrDefault()));
  }

  /// <summary>
  /// The display name for the column for <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.LeftValue" />.
  /// </summary>
  /// <remarks>
  /// The string that is provided here should be localized, but the class itself doesn't collect strings.
  /// </remarks>
  public virtual string LeftValueDescription => "Left record";

  /// <summary>
  /// The display name for the column for <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.RightValue" />.
  /// </summary>
  /// <remarks>
  /// The string that is provided here should be localized, but the class itself doesn't collect strings.
  /// </remarks>
  public virtual string RightValueDescription => "Right record";

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    PXGraph.RowSelectedEvents rowSelected = this.Base.RowSelected;
    System.Type primaryItemType = this.Base.PrimaryItemType;
    CompareEntitiesExt<TGraph, TMain, TComparisonRow> compareEntitiesExt = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected = new PXRowSelected((object) compareEntitiesExt, __vmethodptr(compareEntitiesExt, PrimaryRowSelected));
    rowSelected.AddHandler(primaryItemType, pxRowSelected);
    // ISSUE: method pointer
    this.ComparisonRows = this.Base.GetOrCreateSelectFromView<PXSelectBase<TComparisonRow>>(this.ViewPrefix + "ComparisonRows", (Func<PXSelectBase>) (() => (PXSelectBase) new FbqlSelect<SelectFromBase<TComparisonRow, TypeArrayOf<IFbqlJoin>.Empty>.Order<By<BqlField<ComparisonRow.order, IBqlInt>.Asc>>, TComparisonRow>.View((PXGraph) this.Base, (Delegate) new PXSelectDelegate((object) this, __methodptr(comparisonRows)))));
    // ISSUE: method pointer
    this.VisibleComparisonRows = this.Base.GetOrCreateSelectFromView<PXSelectBase<TComparisonRow>>(this.ViewPrefix + "VisibleComparisonRows", (Func<PXSelectBase>) (() => (PXSelectBase) new FbqlSelect<SelectFromBase<TComparisonRow, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<ComparisonRow.hidden, IBqlBool>.IsEqual<True>>.Order<By<BqlField<ComparisonRow.order, IBqlInt>.Asc>>, TComparisonRow>.View((PXGraph) this.Base, (Delegate) new PXSelectDelegate((object) this, __methodptr(visibleComparisonRows)))));
  }

  /// <summary>
  /// Initializes all <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow" />s and stores them in cache
  /// so that they can be viewed in the UI.
  /// </summary>
  public virtual void InitComparisonRowsViews()
  {
    this.StoreComparisonsInCache(this.GetPreparedComparisons());
  }

  /// <summary>
  /// Returns all <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow" />s and prepares them for displaying in the UI.
  /// </summary>
  /// <returns>The list of comparison rows.</returns>
  public virtual IEnumerable<TComparisonRow> GetPreparedComparisons()
  {
    return this.PrepareComparisons(this.GetComparisons());
  }

  /// <summary>Returns the context for the left set of items.</summary>
  /// <returns>Entities context.</returns>
  public abstract EntitiesContext GetLeftEntitiesContext();

  /// <summary>Returns the context for the right set of items.</summary>
  /// <returns>Entities context.</returns>
  public abstract EntitiesContext GetRightEntitiesContext();

  /// <summary>
  /// Returns the list of <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow" />s.
  /// </summary>
  /// <remarks>
  /// The returned list of rows can be filtered or updated by <see cref="M:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3.PrepareComparisons(System.Collections.Generic.IEnumerable{`2})" />.
  /// </remarks>
  /// <returns>The list or comparison rows.</returns>
  public virtual IEnumerable<TComparisonRow> GetComparisons()
  {
    EntitiesContext leftContext = this.GetLeftEntitiesContext();
    EntitiesContext rightContext = this.GetRightEntitiesContext();
    int order = 0;
    foreach (System.Type itemType in leftContext.Tables.Intersect<System.Type>((IEnumerable<System.Type>) rightContext.Tables))
    {
      EntityEntry leftEntry = leftContext[itemType];
      EntityEntry rightEntry = rightContext[itemType];
      foreach (string fieldName in this.GetFieldsForComparison(itemType, leftEntry.Cache, rightEntry.Cache))
      {
        foreach ((IBqlTable ibqlTable1, IBqlTable ibqlTable2) in this.MapEntries(leftEntry, rightEntry, leftContext, rightContext))
        {
          PXFieldState state1;
          PXFieldState state2;
          if (this.TryGetStateExt(leftEntry.Cache, ibqlTable1, fieldName, out state1) && this.TryGetStateExt(rightEntry.Cache, ibqlTable2, fieldName, out state2))
          {
            string stringValue1 = this.GetStringValue(leftEntry.Cache, ibqlTable1, fieldName, state1);
            string stringValue2 = this.GetStringValue(rightEntry.Cache, ibqlTable2, fieldName, state2);
            if ((!string.IsNullOrWhiteSpace(stringValue1) || !string.IsNullOrWhiteSpace(stringValue2)) && !(stringValue1 == stringValue2))
            {
              TComparisonRow comparisonRow = this.CreateComparisonRow(fieldName, itemType, ref order, (leftEntry.Cache, ibqlTable1, stringValue1, state1), (rightEntry.Cache, ibqlTable2, stringValue2, state2));
              if ((object) comparisonRow != null && !(comparisonRow.LeftValue == comparisonRow.RightValue))
                yield return comparisonRow;
            }
          }
        }
      }
      leftEntry = (EntityEntry) null;
      rightEntry = (EntityEntry) null;
    }
  }

  /// <summary>
  /// Prepares the list of <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow" />s (obtained by <see cref="M:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3.GetComparisons" />).
  /// </summary>
  /// <remarks>
  /// Calls the following methods for the incoming list:
  /// <see cref="M:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3.FilterComparisons(System.Collections.Generic.IEnumerable{`2})" />, <see cref="M:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3.UpdateComparisons(System.Collections.Generic.IEnumerable{`2})" />,
  /// <see cref="M:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3.SortComparisons(System.Collections.Generic.IEnumerable{`2})" />, and <see cref="M:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3.ClearUpComparisons(System.Collections.Generic.IEnumerable{`2})" />.
  /// </remarks>
  /// <param name="comparisons">The list of comparison rows.</param>
  /// <returns>The list of comparison rows.</returns>
  public virtual IEnumerable<TComparisonRow> PrepareComparisons(
    IEnumerable<TComparisonRow> comparisons)
  {
    comparisons = this.FilterComparisons(comparisons);
    comparisons = this.UpdateComparisons(comparisons);
    comparisons = this.SortComparisons(comparisons);
    comparisons = this.ClearUpComparisons(comparisons);
    return comparisons;
  }

  /// <summary>
  /// Filters the <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow" />s.
  /// </summary>
  /// <remarks>
  /// By default, the method excludes all invisible fields
  /// (for which <see cref="P:PX.Data.PXFieldState.Visible" /> is <see langword="false" /> for <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.LeftFieldState" />).
  /// </remarks>
  /// <param name="comparisons">The list of comparison rows.</param>
  /// <returns>The list of comparison rows.</returns>
  public virtual IEnumerable<TComparisonRow> FilterComparisons(
    IEnumerable<TComparisonRow> comparisons)
  {
    foreach (TComparisonRow comparison in comparisons)
    {
      if (comparison.LeftFieldState.Visible && comparison.RightFieldState.Visible)
        yield return comparison;
    }
  }

  /// <summary>
  /// Updates the <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow" />s.
  /// </summary>
  /// <remarks>
  /// The method can update fields of comparison rows.
  /// By default, the method does nothing.
  /// </remarks>
  /// <param name="comparisons">The list of comparison rows.</param>
  /// <returns>The list of comparison rows.</returns>
  public virtual IEnumerable<TComparisonRow> UpdateComparisons(
    IEnumerable<TComparisonRow> comparisons)
  {
    return comparisons;
  }

  /// <summary>
  /// Sorts the <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow" />s.
  /// </summary>
  /// <remarks>
  /// The method can sort fields of comparison rows.
  /// You could change the order of rows here.
  /// You should also set <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.Order" /> inside this method to show rows in the proper order in the UI.
  /// By default, the method does nothing.
  /// </remarks>
  /// <param name="comparisons">The list of comparison rows.</param>
  /// <returns>The list of comparison rows.</returns>
  public virtual IEnumerable<TComparisonRow> SortComparisons(IEnumerable<TComparisonRow> comparisons)
  {
    return comparisons;
  }

  /// <summary>
  /// Clears up the <see cref="T:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow" />s from intermediate fields, which are serialized between requests.
  /// </summary>
  /// <remarks>
  /// By default, the method resets <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.LeftCache" /> and <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.RightCache" /> from caches.
  /// This method should be called to avoid performance issues with multiple cache serialization.
  /// If you override this method, you should call the base method implementation.
  /// </remarks>
  /// <param name="comparisons">The list of comparison rows.</param>
  /// <returns>The list of comparison rows.</returns>
  public virtual IEnumerable<TComparisonRow> ClearUpComparisons(
    IEnumerable<TComparisonRow> comparisons)
  {
    foreach (TComparisonRow comparison in comparisons)
    {
      comparison.LeftCache = (PXCache) null;
      comparison.RightCache = (PXCache) null;
      yield return comparison;
    }
  }

  /// <summary>
  /// Stores comparison rows (obtained by <see cref="M:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3.PrepareComparisons(System.Collections.Generic.IEnumerable{`2})" />) in <see cref="T:PX.Data.PXCache" />.
  /// It is required to show comparison rows in the UI.
  /// </summary>
  /// <remarks>
  /// The <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3.ComparisonRows" /> and <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3.VisibleComparisonRows" /> views rely on cached comparison rows.
  /// </remarks>
  /// <param name="result">The list of comparison rows.</param>
  public virtual void StoreComparisonsInCache(IEnumerable<TComparisonRow> result)
  {
    using (new ReadOnlyScope(new PXCache[1]
    {
      ((PXSelectBase) this.ComparisonRows).Cache
    }))
    {
      ((PXSelectBase) this.ComparisonRows).Cache.Clear();
      foreach (TComparisonRow comparisonRow in result)
        GraphHelper.Hold(((PXSelectBase) this.ComparisonRows).Cache, (object) comparisonRow);
    }
  }

  /// <summary>
  /// Reprepares comparisons (see <see cref="M:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3.PrepareComparisons(System.Collections.Generic.IEnumerable{`2})" />) that are already stored in the cache (see <see cref="M:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3.StoreComparisonsInCache(System.Collections.Generic.IEnumerable{`2})" />).
  /// </summary>
  public virtual void ReprepareComparisonsInCache()
  {
    this.StoreComparisonsInCache(this.PrepareComparisons((IEnumerable<TComparisonRow>) this.ComparisonRows.SelectMain(Array.Empty<object>())));
  }

  /// <summary>
  /// Processes (see <see cref="M:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3.ProcessComparisons(System.Collections.Generic.IReadOnlyCollection{`2})" />) all <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.CompareEntitiesExt`3.ComparisonRows">comparison rows</see>
  /// and clears <see cref="T:PX.Data.PXCache" />.
  /// </summary>
  /// <returns>The resulting contexts for the left and right sets of entities.</returns>
  public virtual (EntitiesContext LeftContext, EntitiesContext RightContext) ProcessComparisonResult()
  {
    (EntitiesContext, EntitiesContext) valueTuple = this.ProcessComparisons((IReadOnlyCollection<TComparisonRow>) this.ComparisonRows.SelectMain(Array.Empty<object>()));
    ((PXSelectBase) this.ComparisonRows).Cache.Clear();
    return valueTuple;
  }

  public virtual (EntitiesContext LeftContext, EntitiesContext RightContext) ProcessComparisons(
    IReadOnlyCollection<TComparisonRow> comparisons)
  {
    EntitiesContext leftEntitiesContext = this.GetLeftEntitiesContext();
    EntitiesContext rightEntitiesContext = this.GetRightEntitiesContext();
    this.UpdateLeftEntitiesContext(leftEntitiesContext, (IEnumerable<TComparisonRow>) comparisons);
    this.UpdateRightEntitiesContext(rightEntitiesContext, (IEnumerable<TComparisonRow>) comparisons);
    return (leftEntitiesContext, rightEntitiesContext);
  }

  /// <summary>
  /// Updates the context for the left entity set according to the changed values in the comparison rows.
  /// </summary>
  /// <param name="context">The left entity context.</param>
  /// <param name="result">The list of comparison rows.</param>
  public virtual void UpdateLeftEntitiesContext(
    EntitiesContext context,
    IEnumerable<TComparisonRow> result)
  {
    this.UpdateEntitiesContext(context, result, ComparisonSelection.Left);
  }

  /// <summary>
  /// Updates the context for the right entity set according to the changed values in the comparison rows.
  /// </summary>
  /// <param name="context">The right entity context.</param>
  /// <param name="result">The list of comparison rows.</param>
  public virtual void UpdateRightEntitiesContext(
    EntitiesContext context,
    IEnumerable<TComparisonRow> result)
  {
    this.UpdateEntitiesContext(context, result, ComparisonSelection.Right);
  }

  /// <summary>
  /// Updates the context for the left or right (depending on <paramref name="sideToUpdate" />) entity set according to the changed values in the comparison rows.
  /// </summary>
  /// <param name="context">The right or left entity context.</param>
  /// <param name="result">The list of comparison rows.</param>
  /// <param name="sideToUpdate">Specifies which entity set (left or right) the <paramref name="context" /> parameter represents.</param>
  public virtual void UpdateEntitiesContext(
    EntitiesContext context,
    IEnumerable<TComparisonRow> result,
    ComparisonSelection sideToUpdate)
  {
    foreach (TComparisonRow comparisonRow in result)
    {
      TComparisonRow comparison = comparisonRow;
      string stringValue = comparison.Selection == ComparisonSelection.None ? (string) null : (comparison.Selection == ComparisonSelection.Right ? comparison.RightValue : comparison.LeftValue);
      if (comparison.Selection != sideToUpdate)
      {
        EntityEntry entry = context[comparison.ItemType];
        IBqlTable ibqlTable = entry.Items.FirstOrDefault<IBqlTable>((Func<IBqlTable, bool>) (i =>
        {
          int objectHashCode = entry.Cache.GetObjectHashCode((object) i);
          int? nullable = sideToUpdate == ComparisonSelection.Right ? comparison.RightHashCode : comparison.LeftHashCode;
          int valueOrDefault = nullable.GetValueOrDefault();
          return objectHashCode == valueOrDefault & nullable.HasValue;
        }));
        this.SetStringValue(entry.Cache, ibqlTable, comparison.FieldName, stringValue);
      }
    }
    using (context.PreserveCurrentsScope())
    {
      foreach (System.Type table in (IEnumerable<System.Type>) context.Tables)
      {
        EntityEntry entityEntry = context[table];
        foreach (IBqlTable ibqlTable in entityEntry.Items)
          entityEntry.Cache.Update((object) ibqlTable);
      }
    }
  }

  /// <summary>
  /// Tries to get <see cref="T:PX.Data.PXFieldState" /> for the <paramref name="fieldName" /> field.
  /// </summary>
  /// <param name="cache">The cache for the current entity.</param>
  /// <param name="item">The entity.</param>
  /// <param name="fieldName">The name of the field.</param>
  /// <param name="state">The resulting field state.</param>
  /// <returns>Returns <see langword="true" /> if <see cref="M:PX.Data.PXCache.GetStateExt(System.Object,System.String)" /> returns <see cref="T:PX.Data.PXFieldState" />.
  /// Returns <see langword="false" /> if <see cref="M:PX.Data.PXCache.GetStateExt(System.Object,System.String)" /> doesn't return <see cref="T:PX.Data.PXFieldState" />.</returns>
  public bool TryGetStateExt(
    PXCache cache,
    IBqlTable item,
    string fieldName,
    out PXFieldState state)
  {
    if (cache.GetStateExt((object) item, fieldName) is PXFieldState stateExt)
    {
      state = stateExt;
      return true;
    }
    state = (PXFieldState) null;
    return false;
  }

  /// <summary>
  /// Returns the fields of the entity presented in either the left cache or the right cache.
  /// </summary>
  /// <param name="itemType">The type of the entity.</param>
  /// <param name="leftCache">The cache for the left entity.</param>
  /// <param name="rightCache">The cache for the right entity.</param>
  /// <returns></returns>
  public virtual IEnumerable<string> GetFieldsForComparison(
    System.Type itemType,
    PXCache leftCache,
    PXCache rightCache)
  {
    return leftCache.GetFields_MassMergable().Union<string>(rightCache.GetFields_MassMergable());
  }

  /// <summary>
  /// Sets the value represented with a string to the field of the entity.
  /// </summary>
  /// <param name="cache">The cache for the entity.</param>
  /// <param name="item">The entity.</param>
  /// <param name="fieldName">The name of the field.</param>
  /// <param name="stringValue">The value in the string representation.</param>
  public virtual void SetStringValue(
    PXCache cache,
    IBqlTable item,
    string fieldName,
    string stringValue)
  {
    cache.SetValue((object) item, fieldName, cache.ValueFromString(fieldName, stringValue));
  }

  /// <summary>
  /// Gets the value represented with a string from the field of the entity.
  /// </summary>
  /// <param name="cache">The cache for the entity.</param>
  /// <param name="item">The entity.</param>
  /// <param name="fieldName">The name of the field.</param>
  /// <param name="state">The field state from which the value should be obtained.</param>
  /// <returns>The string representation of the value.</returns>
  public virtual string GetStringValue(
    PXCache cache,
    IBqlTable item,
    string fieldName,
    PXFieldState state)
  {
    return cache.ValueToString(fieldName, cache.GetValue((object) item, fieldName));
  }

  /// <summary>Maps the left and right entries to each other.</summary>
  /// <remarks>
  /// By default, the method just zips all items from <paramref name="leftEntry" /> and <paramref name="rightEntry" />.
  /// </remarks>
  /// <param name="leftEntry">The left entry of items.</param>
  /// <param name="rightEntry">The right entry of items.</param>
  /// <param name="leftContext">The left entity context.</param>
  /// <param name="rightContext">The right entity context.</param>
  /// <returns></returns>
  public virtual IEnumerable<(IBqlTable LeftItem, IBqlTable RightItem)> MapEntries(
    EntityEntry leftEntry,
    EntityEntry rightEntry,
    EntitiesContext leftContext,
    EntitiesContext rightContext)
  {
    return leftEntry.Items.Zip<IBqlTable, IBqlTable, (IBqlTable, IBqlTable)>(rightEntry.Items, (Func<IBqlTable, IBqlTable, (IBqlTable, IBqlTable)>) ((left, right) => (left, right)));
  }

  public virtual TComparisonRow CreateComparisonRow(
    string fieldName,
    System.Type itemType,
    ref int order,
    (PXCache Cache, IBqlTable Item, string Value, PXFieldState State) left,
    (PXCache Cache, IBqlTable Item, string Value, PXFieldState State) right)
  {
    TComparisonRow comparisonRow = new TComparisonRow();
    comparisonRow.ItemType = itemType.FullName;
    comparisonRow.FieldName = fieldName;
    comparisonRow.LeftHashCode = new int?(left.Cache.GetObjectHashCode((object) left.Item));
    comparisonRow.RightHashCode = new int?(right.Cache.GetObjectHashCode((object) right.Item));
    comparisonRow.LeftValue = left.Value;
    comparisonRow.RightValue = right.Value;
    comparisonRow.FieldDisplayName = left.State.DisplayName;
    comparisonRow.Selection = ComparisonSelection.Left;
    comparisonRow.Order = new int?(order++);
    comparisonRow.LeftCache = left.Cache;
    comparisonRow.LeftFieldState = left.State;
    comparisonRow.RightCache = right.Cache;
    comparisonRow.RightFieldState = right.State;
    TComparisonRow comparison = comparisonRow;
    this.TryAddSelectorDescription(left.Cache, left.Item, left.State, comparison, "LeftValue_description");
    this.TryAddSelectorDescription(right.Cache, right.Item, right.State, comparison, "RightValue_description");
    return comparison;
  }

  /// <summary>
  /// Adds a display name for a field if the field is a selector (that is, has <see cref="T:PX.Data.PXSelectorAttribute" /> assigned).
  /// </summary>
  /// <param name="cache">The cache for the entity.</param>
  /// <param name="item">The entity.</param>
  /// <param name="state">The field state.</param>
  /// <param name="comparison">The comparison row.</param>
  /// <param name="descriptionFieldName">
  /// The name of the description field:
  /// <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.LeftValue_description" /> or <see cref="P:PX.Objects.CR.Extensions.SideBySideComparison.ComparisonRow.RightValue_description" />.
  /// </param>
  public virtual void TryAddSelectorDescription(
    PXCache cache,
    IBqlTable item,
    PXFieldState state,
    TComparisonRow comparison,
    string descriptionFieldName)
  {
    if (state.DescriptionName == null || state.ViewName == null || state.Value == null)
      return;
    ((PXSelectBase) this.ComparisonRows).Cache.SetValue((object) comparison, descriptionFieldName, PXSelectorAttribute.GetField(cache, (object) item, state.Name, cache.GetValue((object) item, state.Name), state.DescriptionName));
  }

  protected virtual void PrimaryRowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    ((PXSelectBase) this.ComparisonRows).AllowDelete = ((PXSelectBase) this.ComparisonRows).AllowInsert = ((PXSelectBase) this.VisibleComparisonRows).AllowDelete = ((PXSelectBase) this.VisibleComparisonRows).AllowInsert = false;
    PXSelectBase<TComparisonRow> comparisonRows = this.ComparisonRows;
    PXSelectBase<TComparisonRow> visibleComparisonRows = this.VisibleComparisonRows;
    IEnumerable<TComparisonRow> source = ((PXSelectBase) this.ComparisonRows).Cache.Cached.Cast<TComparisonRow>();
    int num1;
    bool flag = (num1 = source.Any<TComparisonRow>((Func<TComparisonRow, bool>) (row => !row.Hidden.GetValueOrDefault())) ? 1 : 0) != 0;
    ((PXSelectBase) visibleComparisonRows).AllowSelect = num1 != 0;
    int num2 = flag ? 1 : 0;
    ((PXSelectBase) comparisonRows).AllowSelect = num2 != 0;
    PXCacheEx.AdjustUIReadonly(this.Base.Caches[typeof (TComparisonRow)], (object) null).For<ComparisonRow.leftValue>((Action<PXUIFieldAttribute>) (a => a.DisplayName = PXMessages.LocalizeNoPrefix(this.LeftValueDescription))).For<ComparisonRow.rightValue>((Action<PXUIFieldAttribute>) (a => a.DisplayName = PXMessages.LocalizeNoPrefix(this.RightValueDescription)));
  }

  protected virtual void _(
    Events.FieldSelecting<TComparisonRow, ComparisonRow.leftValue> e)
  {
    if ((object) e.Row == null)
      return;
    Events.FieldSelecting<TComparisonRow, ComparisonRow.leftValue> fieldSelecting = e;
    PXFieldState pxFieldState = FieldStateExtensions.Copy(e.Row.LeftFieldState);
    System.Type dataType = e.Row.LeftFieldState.DataType;
    bool? nullable1 = new bool?(false);
    bool? nullable2 = new bool?();
    bool? nullable3 = new bool?();
    int? nullable4 = new int?();
    int? nullable5 = new int?();
    int? nullable6 = new int?();
    bool? nullable7 = nullable1;
    bool? nullable8 = new bool?();
    bool? nullable9 = new bool?();
    PXFieldState instance = PXFieldState.CreateInstance((object) pxFieldState, dataType, nullable2, nullable3, nullable4, nullable5, nullable6, (object) null, (string) null, (string) null, (string) null, (string) null, (PXErrorLevel) 0, nullable7, nullable8, nullable9, (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    ((Events.FieldSelectingBase<Events.FieldSelecting<TComparisonRow, ComparisonRow.leftValue>>) fieldSelecting).ReturnState = (object) instance;
    ((Events.FieldSelectingBase<Events.FieldSelecting<TComparisonRow, ComparisonRow.leftValue>>) e).Cancel = true;
  }

  protected virtual void _(
    Events.FieldSelecting<TComparisonRow, ComparisonRow.rightValue> e)
  {
    if ((object) e.Row == null)
      return;
    Events.FieldSelecting<TComparisonRow, ComparisonRow.rightValue> fieldSelecting = e;
    PXFieldState pxFieldState = FieldStateExtensions.Copy(e.Row.RightFieldState);
    System.Type dataType = e.Row.RightFieldState.DataType;
    bool? nullable1 = new bool?(false);
    bool? nullable2 = new bool?();
    bool? nullable3 = new bool?();
    int? nullable4 = new int?();
    int? nullable5 = new int?();
    int? nullable6 = new int?();
    bool? nullable7 = nullable1;
    bool? nullable8 = new bool?();
    bool? nullable9 = new bool?();
    PXFieldState instance = PXFieldState.CreateInstance((object) pxFieldState, dataType, nullable2, nullable3, nullable4, nullable5, nullable6, (object) null, (string) null, (string) null, (string) null, (string) null, (PXErrorLevel) 0, nullable7, nullable8, nullable9, (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    ((Events.FieldSelectingBase<Events.FieldSelecting<TComparisonRow, ComparisonRow.rightValue>>) fieldSelecting).ReturnState = (object) instance;
    ((Events.FieldSelectingBase<Events.FieldSelecting<TComparisonRow, ComparisonRow.rightValue>>) e).Cancel = true;
  }

  protected virtual void _(Events.RowPersisting<TComparisonRow> e) => e.Cancel = true;
}
