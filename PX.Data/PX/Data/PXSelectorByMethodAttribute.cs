// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelectorByMethodAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Data;

/// <summary>
/// Selector that extracts records by calling provided static method of a provided type. Method must take no parameters and return IEnumerable implementor
/// </summary>
public class PXSelectorByMethodAttribute : PXCustomSelectorAttribute
{
  /// <summary>
  /// Caches compiled functions. Later the compiled function will be avaliable by the function key which is a tuple of function name and the type than contains it
  /// </summary>
  private static readonly ConcurrentDictionary<Tuple<System.Type, string>, Func<IEnumerable>> FunctionCache = new ConcurrentDictionary<Tuple<System.Type, string>, Func<IEnumerable>>();
  /// <summary>
  /// Key to get the data providing function from the function cache
  /// </summary>
  private readonly Tuple<System.Type, string> _functionCacheKey;

  public PXSelectorByMethodAttribute(
    System.Type dataProviderType,
    string dataProvidingMethodName,
    System.Type selectingField,
    params System.Type[] displayingFieldList)
    : base(selectingField, displayingFieldList)
  {
    if (dataProviderType == (System.Type) null)
      throw new ArgumentNullException(nameof (dataProviderType));
    this._functionCacheKey = dataProvidingMethodName != null ? Tuple.Create<System.Type, string>(dataProviderType, dataProvidingMethodName) : throw new ArgumentNullException(nameof (dataProvidingMethodName));
    if (PXSelectorByMethodAttribute.FunctionCache.ContainsKey(this._functionCacheKey))
      return;
    MethodInfo method = ((IEnumerable<MethodInfo>) dataProviderType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == dataProvidingMethodName && typeof (IEnumerable).IsAssignableFrom(m.ReturnType) && !((IEnumerable<ParameterInfo>) m.GetParameters()).Any<ParameterInfo>()));
    Func<IEnumerable> func = !(method == (MethodInfo) null) ? ((Expression<Func<IEnumerable>>) (() => Expression.Call((Expression) null, method))).Compile() : throw new ArgumentException($"Static method \"IEnumerable {dataProvidingMethodName}()\" does not exist in {dataProviderType.FullName} type", nameof (dataProvidingMethodName));
    PXSelectorByMethodAttribute.FunctionCache.TryAdd(this._functionCacheKey, func);
  }

  protected virtual IEnumerable GetRecords()
  {
    return PXSelectorByMethodAttribute.FunctionCache[this._functionCacheKey]();
  }
}
