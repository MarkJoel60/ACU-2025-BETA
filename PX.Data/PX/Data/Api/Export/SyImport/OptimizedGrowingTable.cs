// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Export.SyImport.OptimizedGrowingTable
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

internal class OptimizedGrowingTable : IGrowingTable
{
  private readonly LinkedList<OptimizedGrowingTable.TableRow> _rows;
  private readonly List<string> _viewNamesOrder = new List<string>();
  private readonly Dictionary<string, List<string>> _dependencyHierarchies;
  private const int CurrentRowIndex = 0;

  public OptimizedGrowingTable(
    IEnumerable<string> viewNamesOrder,
    Tuple<string, string>[] enumFieldValueViews,
    Dictionary<string, string[]> viewParents,
    string graphPrimaryView,
    HashSet<string> primaryViewAliases)
  {
    OptimizedGrowingTable optimizedGrowingTable = this;
    this._dependencyHierarchies = new Dictionary<string, List<string>>();
    foreach (string str1 in viewNamesOrder)
    {
      string cleanViewName = SyMappingUtils.CleanViewName(str1);
      string[] source;
      string str2 = viewParents.TryGetValue(cleanViewName, out source) ? ((IEnumerable<string>) source).LastOrDefault<string>((Func<string, bool>) (c => !string.Equals(cleanViewName, c, StringComparison.OrdinalIgnoreCase) && !primaryViewAliases.Contains(c) && closure_1._dependencyHierarchies.ContainsKey(c))) : (string) null;
      List<string> parents = new List<string>();
      AddAliasToHierarchy(str1, parents);
      this._viewNamesOrder.Add(str1);
      string cleanParentName;
      for (; str2 != null; str2 = viewParents.TryGetValue(cleanParentName, out source) ? ((IEnumerable<string>) source).LastOrDefault<string>((Func<string, bool>) (c => !string.Equals(c, cleanParentName, StringComparison.OrdinalIgnoreCase) && !primaryViewAliases.Contains(c) && closure_0._dependencyHierarchies.ContainsKey(c))) : (string) null)
      {
        List<string> collection;
        if (this._dependencyHierarchies.TryGetValue(str2, out collection) && collection.Count > 0)
        {
          parents.Add(str2);
          parents.AddRange((IEnumerable<string>) collection);
          break;
        }
        parents.Add(str2);
        cleanParentName = SyMappingUtils.CleanViewName(str2);
      }
      List<string> collection1;
      if (str1 != graphPrimaryView && parents.Count == 0 && this._dependencyHierarchies.TryGetValue(graphPrimaryView, out collection1))
      {
        parents.Add(graphPrimaryView);
        parents.AddRange((IEnumerable<string>) collection1);
      }
      this._dependencyHierarchies.Add(str1, parents);
    }
    this._rows = new LinkedList<OptimizedGrowingTable.TableRow>((IEnumerable<OptimizedGrowingTable.TableRow>) new OptimizedGrowingTable.TableRow[1]
    {
      new OptimizedGrowingTable.TableRow()
    });

    void AddAliasToHierarchy(string view, List<string> parents)
    {
      foreach (Tuple<string, string> enumFieldValueView in enumFieldValueViews)
      {
        if (view == enumFieldValueView.Item2)
        {
          optimizedGrowingTable._dependencyHierarchies.Add(enumFieldValueView.Item1, parents.ToList<string>());
          optimizedGrowingTable._viewNamesOrder.Add(enumFieldValueView.Item1);
          parents.Insert(0, enumFieldValueView.Item1);
        }
      }
    }
  }

  private bool MoveNextImpl(List<NativeRowWrapperDictionary> rows, bool isAlreadyDone)
  {
    NativeRowWrapperDictionary wrapperDictionary = rows.FirstOrDefault<NativeRowWrapperDictionary>();
    bool isAlreadyDone1 = isAlreadyDone;
    if (wrapperDictionary == null)
      return isAlreadyDone1;
    foreach (string key in this._viewNamesOrder)
    {
      List<NativeRowWrapperDictionary> rows1;
      if (wrapperDictionary.TryGetValue(key, out rows1) && rows1.Count > 0)
      {
        if (isAlreadyDone1)
          rows1[0].NativeRowWrapper.Select.IncrementRevision();
        if (!this.MoveNextImpl(rows1, isAlreadyDone1))
        {
          if (rows1.Count != 1)
            rows1.RemoveAt(0);
          else
            continue;
        }
        isAlreadyDone1 = true;
      }
    }
    return isAlreadyDone1;
  }

  public void MoveNextRow()
  {
    bool flag = true;
    IDictionary<string, List<NativeRowWrapperDictionary>> views = this._rows.First.Value.Views;
    foreach (string key in (IEnumerable<string>) views.GetKeys())
    {
      List<NativeRowWrapperDictionary> rows;
      if (views.TryGetValue(key, out rows) && rows.Count > 0)
      {
        if (!this.MoveNextImpl(rows, false))
        {
          if (rows.Count != 1)
            rows.RemoveAt(0);
          else
            continue;
        }
        flag = false;
        break;
      }
    }
    if (!flag)
      return;
    this._rows.RemoveFirst();
  }

  public bool IsLastRow()
  {
    if (this._rows.Count - 1 != 0)
      return false;
    OptimizedGrowingTable.TableRow tableRow = this._rows.First.Value;
    foreach (string key in (IEnumerable<string>) tableRow.Views.GetKeys())
    {
      List<NativeRowWrapperDictionary> wrapperDictionaryList;
      if (tableRow.Views.TryGetValue(key, out wrapperDictionaryList, this.GetViewDependencyHierarchy(key)) && wrapperDictionaryList.Count > 1)
        return false;
    }
    return true;
  }

  public void AddSelectResults(ViewSelectResults selectResults)
  {
    Stack<string> dependencyHierarchy = this.GetViewDependencyHierarchy(selectResults.ViewId);
    OptimizedGrowingTable.TableRow currentRow = this.GetCurrentRow();
    List<NativeRowWrapperDictionary> wrapperDictionaryList1;
    if (selectResults.Count != 0)
    {
      wrapperDictionaryList1 = this.TransformSelectResults((List<NativeRowWrapper>) selectResults);
    }
    else
    {
      wrapperDictionaryList1 = new List<NativeRowWrapperDictionary>();
      wrapperDictionaryList1.Add(new NativeRowWrapperDictionary(new NativeRowWrapper(selectResults, (object) null)));
    }
    List<NativeRowWrapperDictionary> wrapperDictionaryList2 = wrapperDictionaryList1;
    currentRow.Views.Add(dependencyHierarchy, wrapperDictionaryList2);
  }

  private List<NativeRowWrapperDictionary> TransformSelectResults(
    List<NativeRowWrapper> selectResults)
  {
    return selectResults.Select<NativeRowWrapper, NativeRowWrapperDictionary>((Func<NativeRowWrapper, NativeRowWrapperDictionary>) (c => new NativeRowWrapperDictionary(c))).ToList<NativeRowWrapperDictionary>();
  }

  private Stack<string> GetViewDependencyHierarchy(string targetViewId)
  {
    Stack<string> dependencyHierarchy = new Stack<string>();
    dependencyHierarchy.Push(targetViewId);
    List<string> stringList;
    if (!this._dependencyHierarchies.TryGetValue(targetViewId, out stringList))
      return dependencyHierarchy;
    foreach (string str in stringList)
      dependencyHierarchy.Push(str);
    return dependencyHierarchy;
  }

  private OptimizedGrowingTable.TableRow GetCurrentRow()
  {
    return this._rows.Count <= 0 ? (OptimizedGrowingTable.TableRow) null : this._rows.First.Value;
  }

  public bool HasEnumValues
  {
    get
    {
      OptimizedGrowingTable.TableRow currentRow = this.GetCurrentRow();
      return currentRow != null && currentRow.HasEnumValues;
    }
    set
    {
      OptimizedGrowingTable.TableRow currentRow = this.GetCurrentRow();
      if (currentRow == null)
        return;
      currentRow.HasEnumValues = value;
    }
  }

  public int? CountResultsForView(string viewId)
  {
    List<NativeRowWrapperDictionary> source = (List<NativeRowWrapperDictionary>) null;
    OptimizedGrowingTable.TableRow currentRow = this.GetCurrentRow();
    if ((currentRow != null ? (currentRow.Views.TryGetValue(viewId, out source, this.GetViewDependencyHierarchy(viewId)) ? 1 : 0) : 0) == 0)
      return new int?();
    if (source == null)
      return new int?();
    return source.First<NativeRowWrapperDictionary>()?.NativeRowWrapper?.Select?.Count;
  }

  public void ExportValues(SyImportProcessor.SyExternalValues dic)
  {
    foreach (FieldValue fieldValue in this.GetCurrentRow()?.Cells ?? Enumerable.Empty<FieldValue>())
      dic[fieldValue.FieldId] = Convert.ToString(fieldValue.Value);
  }

  public bool IsEmptyViewResults(string viewId)
  {
    return this.CountResultsForView(viewId).HasValue && !this.GetCurrentRow().Cells.Any<FieldValue>((Func<FieldValue, bool>) (c => string.Equals(c.ViewId, viewId, StringComparison.OrdinalIgnoreCase)));
  }

  public NativeRowWrapper GetNativeRow(string viewName)
  {
    List<NativeRowWrapperDictionary> source;
    if (!this.GetCurrentRow().Views.TryGetValue(viewName, out source, this.GetViewDependencyHierarchy(viewName)))
      return (NativeRowWrapper) null;
    return source.FirstOrDefault<NativeRowWrapperDictionary>()?.NativeRowWrapper.Clone();
  }

  public class TableRow
  {
    public readonly IDictionary<string, List<NativeRowWrapperDictionary>> Views = (IDictionary<string, List<NativeRowWrapperDictionary>>) new Dictionary<string, List<NativeRowWrapperDictionary>>();
    public bool HasEnumValues;

    public IEnumerable<FieldValue> Cells
    {
      get
      {
        return this.Views.Values.SelectMany<List<NativeRowWrapperDictionary>, FieldValue>((Func<List<NativeRowWrapperDictionary>, IEnumerable<FieldValue>>) (g => (g != null ? g.FirstOrDefault<NativeRowWrapperDictionary>()?.GetFieldValues() : (IEnumerable<FieldValue>) null) ?? Enumerable.Empty<FieldValue>()));
      }
    }
  }
}
