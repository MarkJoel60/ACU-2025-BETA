// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Utility.PXResultMapper
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
namespace PX.Objects.Common.Utility;

public class PXResultMapper
{
  private readonly Dictionary<Type, Type> fieldMap;
  private readonly Type[] resultDefinition;
  private readonly PXGraph graph;
  private readonly PXView view;
  public readonly HashSet<Type> ExtFilters;
  public readonly HashSet<Type> SuppressSorts;

  public PXResultMapper(
    PXGraph graph,
    Dictionary<Type, Type> fieldMap,
    params Type[] resultDefinition)
  {
    this.fieldMap = fieldMap;
    this.resultDefinition = resultDefinition;
    this.graph = graph;
    this.ExtFilters = new HashSet<Type>();
    this.SuppressSorts = new HashSet<Type>();
  }

  public PXResultMapper(
    PXView view,
    Dictionary<Type, Type> fieldMap,
    params Type[] resultDefinition)
    : this(view.Graph, fieldMap, resultDefinition)
  {
    this.view = view;
  }

  public string[] SortColumns => this.MapSort(PXView.SortColumns);

  public object[] Searches => this.MapSort<object>(PXView.SortColumns, PXView.Searches);

  public bool[] Descendings => this.MapSort<bool>(PXView.SortColumns, PXView.Descendings);

  public PXDelegateResult CreateDelegateResult(bool restricted = true)
  {
    PXDelegateResult delegateResult = new PXDelegateResult();
    if ((long) this.MappedFilters.Count<PXFilterRow>() != ((IEnumerable) PXView.Filters).Count())
      restricted = false;
    delegateResult.IsResultFiltered = restricted;
    delegateResult.IsResultSorted = restricted;
    delegateResult.IsResultTruncated = restricted;
    return delegateResult;
  }

  public List<object> Select(PXView view, params object[] prms)
  {
    int startRow = PXView.StartRow;
    int num = 0;
    return view.Select(PXView.Currents, prms, this.Searches, this.SortColumns, this.Descendings, PXView.PXFilterRowCollection.op_Implicit(this.Filters), ref startRow, PXView.MaximumRows, ref num);
  }

  protected TType[] MapSort<TType>(string[] source, TType[] extParams)
  {
    List<TType> typeList = new List<TType>();
    for (int index = 0; index < source.Length; ++index)
    {
      Type bqlField = this.GetBqlField(source[index]);
      if (!(bqlField != (Type) null) || !this.SuppressSorts.Contains(bqlField) && !this.SuppressSorts.Contains(bqlField.DeclaringType))
        typeList.Add(extParams[index]);
    }
    return typeList.ToArray();
  }

  protected string[] MapSort(string[] source)
  {
    List<string> stringList = new List<string>();
    for (int index = 0; index < source.Length; ++index)
    {
      Type bqlField = this.GetBqlField(source[index]);
      if (!(bqlField != (Type) null) || !this.SuppressSorts.Contains(bqlField) && !this.SuppressSorts.Contains(bqlField.DeclaringType))
        stringList.Add(this.MapField(source[index]));
    }
    return stringList.ToArray();
  }

  protected IEnumerable<PXFilterRow> MappedFilters
  {
    get
    {
      List<PXFilterRow> pxFilterRowList = new List<PXFilterRow>();
      bool flag1 = false;
      bool flag2 = false;
      foreach (PXFilterRow filter in PXView.Filters)
      {
        PXFilterRow pxFilterRow = (PXFilterRow) filter.Clone();
        Type bqlField = this.GetBqlField(pxFilterRow.DataField);
        if (bqlField != (Type) null)
        {
          if (this.ExtFilters.Contains(bqlField) || this.ExtFilters.Contains(bqlField.DeclaringType))
          {
            flag1 = true;
            continue;
          }
          Type result;
          if (this.fieldMap.TryGetValue(bqlField, out result))
          {
            if (this.ExtFilters.Contains(result) || this.ExtFilters.Contains(result.DeclaringType))
            {
              flag1 = true;
              continue;
            }
            pxFilterRow.DataField = this.GetMapFieldName(result);
          }
        }
        if (pxFilterRow.OrOperator)
          flag2 = true;
        pxFilterRowList.Add(pxFilterRow);
      }
      return !(flag1 & flag2) ? (IEnumerable<PXFilterRow>) pxFilterRowList : (IEnumerable<PXFilterRow>) new List<PXFilterRow>();
    }
  }

  public PXView.PXFilterRowCollection Filters
  {
    get => new PXView.PXFilterRowCollection(this.MappedFilters.ToArray<PXFilterRow>());
  }

  public string[] MapFields(string[] source)
  {
    string[] strArray = new string[source.Length];
    for (int index = 0; index < source.Length; ++index)
      strArray[index] = this.MapField(source[index]);
    return strArray;
  }

  public string MapField(string source)
  {
    Type result;
    return this.GetBqlField(source) != (Type) null && this.fieldMap.TryGetValue(this.GetBqlField(source), out result) ? this.GetMapFieldName(result) : source;
  }

  private string GetMapFieldName(Type result)
  {
    return this.view != null && PXViewExtensionsForMobile.CacheType(this.view) == result.DeclaringType ? result.Name : $"{result.DeclaringType.Name}__{result.Name}";
  }

  private Type GetBqlField(string source)
  {
    int length = source.IndexOf("__", StringComparison.InvariantCultureIgnoreCase);
    Type type = this.resultDefinition[0];
    if (length != -1)
    {
      string sourceDac = source.Substring(0, length);
      source = source.Substring(length + 2);
      type = ((IEnumerable<Type>) this.resultDefinition).FirstOrDefault<Type>((Func<Type, bool>) (_ => _.Name == sourceDac));
      if (type == (Type) null)
        return (Type) null;
    }
    return this.graph.Caches[type].GetBqlField(source);
  }

  public object CreateResult(PXResult source)
  {
    Dictionary<Type, object> dictionary = new Dictionary<Type, object>();
    foreach (Type key in this.resultDefinition)
    {
      object obj = (object) PXResult.Unwrap((object) source, key) ?? this.graph.Caches[key].CreateInstance();
      dictionary.Add(key, obj);
    }
    foreach (Type key in this.fieldMap.Keys)
    {
      Type field = this.fieldMap[key];
      Type c = field.DeclaringType;
      if (typeof (PXCacheExtension).IsAssignableFrom(c))
        c = c.BaseType.GetGenericArguments()[0];
      PXCache cach = this.graph.Caches[c];
      Type type = key.DeclaringType;
      if (typeof (PXCacheExtension).IsAssignableFrom(type))
        type = key.DeclaringType.BaseType.GetGenericArguments()[0];
      this.graph.Caches[type].SetValue(dictionary[type], key.Name, cach.GetValue((object) PXResult.Unwrap((object) source, c), field.Name));
    }
    if (this.resultDefinition.Length == 1)
      return dictionary[this.resultDefinition[0]];
    Type resultType = this.GetResultType();
    object[] objArray = new object[this.resultDefinition.Length];
    for (int index = 0; index < this.resultDefinition.Length; ++index)
      objArray[index] = dictionary[this.resultDefinition[index]];
    return Activator.CreateInstance(resultType.MakeGenericType(this.resultDefinition), objArray);
  }

  private Type GetResultType()
  {
    switch (this.resultDefinition.Length)
    {
      case 1:
        return typeof (PXResult<>);
      case 2:
        return typeof (PXResult<,>);
      case 3:
        return typeof (PXResult<,,>);
      case 4:
        return typeof (PXResult<,,,>);
      case 5:
        return typeof (PXResult<,,,,>);
      case 6:
        return typeof (PXResult<,,,,,>);
      case 7:
        return typeof (PXResult<,,,,,,>);
      case 8:
        return typeof (PXResult<,,,,,,,>);
      case 9:
        return typeof (PXResult<,,,,,,,,>);
      case 10:
        return typeof (PXResult<,,,,,,,,,>);
      case 11:
        return typeof (PXResult<,,,,,,,,,,>);
      case 12:
        return typeof (PXResult<,,,,,,,,,,,>);
      case 13:
        return typeof (PXResult<,,,,,,,,,,,,>);
      case 14:
        return typeof (PXResult<,,,,,,,,,,,,,>);
      case 15:
        return typeof (PXResult<,,,,,,,,,,,,,,>);
      case 16 /*0x10*/:
        return typeof (PXResult<,,,,,,,,,,,,,,,>);
      case 17:
        return typeof (PXResult<,,,,,,,,,,,,,,,,>);
      case 18:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,>);
      case 19:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,>);
      case 20:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,,>);
      case 21:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,,,>);
      case 22:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,,,,>);
      case 23:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,,,,,>);
      case 24:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,,,,,,>);
      case 25:
        return typeof (PXResult<,,,,,,,,,,,,,,,,,,,,,,,,>);
      default:
        return (Type) null;
    }
  }
}
