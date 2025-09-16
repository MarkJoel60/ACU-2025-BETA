// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericFilterCache
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Description.GI;
using PX.Data.Maintenance.GI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data;

/// <exclude />
public class GenericFilterCache(PXGraph graph) : GICache<GenericFilter>(graph)
{
  private static readonly Lazy<string[]> _realFields = new Lazy<string[]>((Func<string[]>) (() => ((IEnumerable<PropertyInfo>) typeof (GenericFilter).GetProperties(BindingFlags.Instance | BindingFlags.Public)).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (p => p.Name)).ToArray<string>()));
  private Dictionary<string, GIFilter> _parameters;
  private static readonly IReadOnlyDictionary<string, GIFilter> _emptryParameters = (IReadOnlyDictionary<string, GIFilter>) new Dictionary<string, GIFilter>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

  public override PXFieldCollection Fields
  {
    get
    {
      if (this._Fields == null)
      {
        Dictionary<string, int> dict = new Dictionary<string, int>((IDictionary<string, int>) this._FieldsMap, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        this._Fields = new PXFieldCollection((IEnumerable<string>) this._ClassFields, dict);
        foreach (string key in GenericFilterCache._realFields.Value)
        {
          this._Fields.Remove(key);
          dict.Remove(key);
        }
      }
      return this._Fields;
    }
  }

  public override object GetValue(object data, string fieldName)
  {
    object obj;
    return (data is GenericFilter genericFilter ? genericFilter.Values : (IDictionary<string, object>) null) != null && genericFilter.Values.TryGetValue(fieldName, out obj) ? obj : base.GetValue(data, fieldName);
  }

  public override void SetValue(object data, string fieldName, object value)
  {
    if (data is GenericFilter genericFilter && this.Fields.Contains(fieldName))
      genericFilter.Values[fieldName] = value;
    else
      base.SetValue(data, fieldName, value);
  }

  internal override System.Type GetFieldType(string paramName)
  {
    return this.MapCall<System.Type>(paramName, (GenericFilterCache.CallDelegate<System.Type>) ((cache, field) => cache.GetFieldType(field)), (GenericFilterCache.FailureDelegate<System.Type>) (field => base.GetFieldType(field)));
  }

  private IReadOnlyDictionary<string, GIFilter> Parameters
  {
    get
    {
      if (this._parameters == null)
      {
        if (this._GenInqGraph.Description == null)
          return GenericFilterCache._emptryParameters;
        this._parameters = new Dictionary<string, GIFilter>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        foreach (GIFilter filter in this._GenInqGraph.Description.Filters)
          this._parameters[filter.Name] = filter;
      }
      return (IReadOnlyDictionary<string, GIFilter>) this._parameters;
    }
  }

  private T MapCall<T>(
    string paramName,
    GenericFilterCache.CallDelegate<T> callFunc,
    GenericFilterCache.FailureDelegate<T> failureFunc)
  {
    GIFilter giFilter;
    if (!string.IsNullOrEmpty(paramName) && this.Parameters.TryGetValue(paramName, out giFilter) && !string.IsNullOrEmpty(giFilter.FieldName))
    {
      string[] strArray = giFilter.FieldName.Split('.', 2);
      PXTable pxTable;
      if (strArray.Length == 2 && this._GenInqGraph.BaseQueryDescription.Tables.TryGetValue(strArray[0], out pxTable))
      {
        PXCache cach = this._GenInqGraph.Caches[pxTable.CacheType];
        return callFunc(cach, strArray[1]);
      }
    }
    return failureFunc(paramName);
  }

  /// <exclude />
  public delegate T CallDelegate<out T>(PXCache actualCache, string schemaField);

  public delegate T FailureDelegate<out T>(string paramName);
}
