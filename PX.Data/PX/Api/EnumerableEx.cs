// Decompiled with JetBrains decompiler
// Type: PX.Api.EnumerableEx
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace PX.Api;

public static class EnumerableEx
{
  public static IEnumerable<T> Select<T>(this IEnumerable src)
  {
    foreach (object obj1 in src)
    {
      if (obj1 is T obj2)
        yield return obj2;
    }
  }

  public static IEnumerable<object> Cast(this IEnumerable src)
  {
    foreach (object obj in src)
      yield return obj;
  }

  public static string JoinToString<T>(this IEnumerable<T>? src, string separator)
  {
    if (src == null)
      return "";
    StringBuilder stringBuilder = new StringBuilder();
    foreach (T obj in src)
    {
      if (stringBuilder.Length > 0)
        stringBuilder.Append(separator);
      stringBuilder.Append(Convert.ToString((object) obj));
    }
    return stringBuilder.ToString();
  }

  public static string JoinToString<T>(
    this IEnumerable<T>? src,
    string prefix,
    string separator,
    string suffix)
  {
    string str = src.JoinToString<T>(separator);
    return Str.IsNullOrEmpty(str) ? str : prefix + str + suffix;
  }

  public static IEnumerable<IndexedValue<T>> EnumWithIndex<T>(this IEnumerable<T> list)
  {
    int index = 0;
    foreach (T obj in list)
      yield return new IndexedValue<T>()
      {
        Index = index++,
        Value = obj
      };
  }

  public static IEnumerable<Pair<T>> GetPairs<T>(this IEnumerable<T> list)
  {
    bool flag = false;
    T prev = default (T);
    foreach (T obj in list)
    {
      Pair<T> pair = new Pair<T>() { A = prev, B = obj };
      prev = obj;
      if (flag)
        yield return pair;
      flag = true;
    }
  }

  public static IEnumerable<int> EnumLessThan(this int from, int upper)
  {
    for (int x = from; x < upper; ++x)
      yield return x;
  }

  public static TValue Ensure<TKey, TValue>(
    this IDictionary<TKey, TValue> dictionary,
    TKey key,
    Func<TValue> valueGetter)
  {
    TValue obj1;
    if (dictionary.TryGetValue(key, out obj1))
      return obj1;
    TValue obj2 = valueGetter();
    dictionary.Add(key, obj2);
    return obj2;
  }

  public static TValue? GetOrDefault<TKey, TValue>(
    this IDictionary<TKey, TValue> dictionary,
    TKey key,
    TValue? defValue)
  {
    TValue obj;
    return dictionary.TryGetValue(key, out obj) ? obj : defValue;
  }

  public static TValue GetOrAdd<TKey, TValue>(
    this IDictionary<TKey, TValue> dictionary,
    TKey key,
    TValue value)
  {
    TValue orAdd;
    if (dictionary.TryGetValue(key, out orAdd))
      return orAdd;
    dictionary.Add(key, value);
    return value;
  }

  public static TValue GetOrAdd<TKey, TValue>(
    this IDictionary<TKey, TValue> dictionary,
    TKey key,
    Func<TKey, TValue> valueFactory)
  {
    TValue orAdd1;
    if (dictionary.TryGetValue(key, out orAdd1))
      return orAdd1;
    TValue orAdd2 = valueFactory(key);
    dictionary.Add(key, orAdd2);
    return orAdd2;
  }

  public static IEnumerable<TValue> CreateList<TValue>(this TValue start, Func<TValue, TValue> next) where TValue : class
  {
    TValue p;
    for (p = start; (object) p != null; p = next(p))
      yield return p;
    p = default (TValue);
  }

  public static IEnumerable<TValue> EnumDescendantsOrSelf<TValue>(
    this TValue start,
    Func<TValue, IEnumerable<TValue>> getChildren)
    where TValue : class
  {
    yield return start;
    foreach (TValue start1 in getChildren(start))
    {
      foreach (TValue obj in start1.EnumDescendantsOrSelf<TValue>(getChildren))
        yield return obj;
    }
  }

  public static V? FirstOrDefaultValue<T, V>(
    this IEnumerable<T> list,
    Func<T, bool> where,
    Func<T, V> convert)
    where T : class
  {
    T obj = list.FirstOrDefault<T>(where);
    return (object) obj == null ? default (V) : convert(obj);
  }

  public static IEnumerable<T> Copy<T>(this IEnumerable<T> src, int start, int afterEnd)
  {
    foreach (IndexedValue<T> indexedValue in src.EnumWithIndex<T>())
    {
      if (indexedValue.Index >= start && indexedValue.Index < afterEnd)
        yield return indexedValue.Value;
    }
  }

  public static OUT? IfNotNull<IN, OUT>(this IN? obj, Func<IN, OUT> fn) where IN : class
  {
    return (object) obj == null ? default (OUT) : fn(obj);
  }

  public static string IfNullOrEmpty(this string? str, string def)
  {
    return !Str.IsNullOrEmpty(str) ? str : def;
  }

  public static IEnumerable<T> Ensure<T>(this IEnumerable<T>? src)
  {
    return src ?? (IEnumerable<T>) Array.Empty<T>();
  }
}
