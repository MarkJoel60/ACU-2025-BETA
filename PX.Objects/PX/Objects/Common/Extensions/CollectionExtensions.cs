// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Extensions.CollectionExtensions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common.Extensions;

/// <summary>
/// This class contains extension methods for collections.
/// </summary>
public static class CollectionExtensions
{
  /// <exclude />
  public static string JoinIntoStringForMessageNoQuotes<T>(
    this ICollection<T> items,
    int maxCount = 0,
    string separator = ",")
  {
    return items.JoinIntoStringForMessage<T>(maxCount, separator, string.Empty);
  }

  /// <exclude />
  public static string JoinIntoStringForMessage<T>(
    this ICollection<T> items,
    int maxCount = 0,
    string separator = ",",
    string edgingSymbol = "'")
  {
    string str = string.Empty;
    IEnumerable<T> source;
    if (items.Count > maxCount && maxCount != 0)
    {
      source = items.Take<T>(maxCount);
      str = "...";
    }
    else
      source = (IEnumerable<T>) items;
    IEnumerable<string> values = source.Select<T, string>((Func<T, string>) (item => edgingSymbol + (object) item + edgingSymbol));
    return string.Join(separator, values) + str;
  }

  /// <summary>
  /// Performs an index-dependent action upon each element of the sequence.
  /// The action takes the index of an element as an input.
  /// </summary>
  public static void ForEach<T>(this IEnumerable<T> sequence, Action<T, int> action)
  {
    int num = 0;
    foreach (T obj in sequence)
    {
      action(obj, num);
      ++num;
    }
  }

  /// <summary>
  /// The method returns a new sequence in which the specified number of elements
  /// is dropped from the tail of the original sequence.
  /// The method performs this efficiently, without enumerating the sequence several times.
  /// </summary>
  /// <param name="elementsToSkip">The number of elements to drop from the tail.</param>
  public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source, int elementsToSkip)
  {
    if (elementsToSkip < 0)
      throw new ArgumentOutOfRangeException(nameof (elementsToSkip));
    IEnumerator<T> enumerator = source.GetEnumerator();
    Queue<T> elementCache = new Queue<T>(elementsToSkip + 1);
    bool hasRemainingItems;
    do
    {
      hasRemainingItems = enumerator.MoveNext();
      if (hasRemainingItems)
      {
        elementCache.Enqueue(enumerator.Current);
        if (elementCache.Count > elementsToSkip)
          yield return elementCache.Dequeue();
      }
    }
    while (hasRemainingItems);
  }

  /// <summary>
  /// Returns a collection consisting of elements of the <paramref name="source" /> collection
  /// that has even indices (starting from 0).
  /// </summary>
  public static IEnumerable<T> EvenElements<T>(this IEnumerable<T> source)
  {
    int index = 0;
    foreach (T obj in source)
    {
      if (index % 2 == 0)
        yield return obj;
      ++index;
    }
  }

  /// <summary>
  /// Returns a collection consisting of elements of the <paramref name="source" /> collection
  /// that has odd indices (starting from 0).
  /// </summary>
  public static IEnumerable<T> OddElements<T>(this IEnumerable<T> source)
  {
    int index = 0;
    foreach (T obj in source)
    {
      if (index % 2 != 0)
        yield return obj;
      ++index;
    }
  }

  public static TItem GetItemWithMax<TItem, TValue>(
    this IReadOnlyCollection<TItem> items,
    Func<TItem, TValue> getValue)
    where TItem : class
    where TValue : IComparable<TValue>
  {
    return items.GetPreferredItem<TItem, TValue>(getValue, (Func<TValue, TValue, bool>) ((curValue, maxValue) => curValue.CompareTo(maxValue) > 0));
  }

  public static TItem GetItemWithMin<TItem, TValue>(
    this IReadOnlyCollection<TItem> items,
    Func<TItem, TValue> getValue)
    where TItem : class
    where TValue : IComparable<TValue>
  {
    return items.GetPreferredItem<TItem, TValue>(getValue, (Func<TValue, TValue, bool>) ((curValue, minValue) => curValue.CompareTo(minValue) < 0));
  }

  public static TItem GetPreferredItem<TItem, TValue>(
    this IReadOnlyCollection<TItem> items,
    Func<TItem, TValue> getValue,
    Func<TValue, TValue, bool> compareFunc)
    where TItem : class
    where TValue : IComparable<TValue>
  {
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    if (items == null)
      throw new ArgumentNullException(nameof (getValue));
    if (!items.Any<TItem>())
      return default (TItem);
    TItem preferredItem = items.First<TItem>();
    TValue obj1 = getValue(preferredItem);
    foreach (TItem obj2 in (IEnumerable<TItem>) items)
    {
      TValue obj3 = getValue(obj2);
      if (compareFunc(obj3, obj1))
      {
        obj1 = obj3;
        preferredItem = obj2;
      }
    }
    return preferredItem;
  }

  /// <exclude />
  public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
  {
    if (collection == null)
      throw new ArgumentNullException(nameof (collection));
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    foreach (T obj in items)
      collection.Add(obj);
  }

  /// <exclude />
  public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
    IEnumerable<TSource> source,
    Func<TSource, TKey> keySelector)
  {
    HashSet<TKey> seenKeys = new HashSet<TKey>();
    foreach (TSource source1 in source)
    {
      if (seenKeys.Add(keySelector(source1)))
        yield return source1;
    }
  }

  /// <exclude />
  public static Dictionary<TSource, HashSet<TSource>> ReverseDictionary<TSource>(
    this Dictionary<TSource, TSource> source,
    bool keppAllKeys = false)
  {
    Dictionary<TSource, HashSet<TSource>> dictionary = new Dictionary<TSource, HashSet<TSource>>();
    if (keppAllKeys)
    {
      foreach (TSource key in source.Keys)
        dictionary.Add(key, new HashSet<TSource>());
    }
    foreach (KeyValuePair<TSource, TSource> keyValuePair in source)
    {
      if ((object) keyValuePair.Value != null)
      {
        if (dictionary.ContainsKey(keyValuePair.Value))
          dictionary[keyValuePair.Value].Add(keyValuePair.Key);
        else
          dictionary[keyValuePair.Value] = new HashSet<TSource>()
          {
            keyValuePair.Key
          };
      }
    }
    return dictionary;
  }

  /// <exclude />
  public static IEnumerable<IGrouping<int, TElement>> BreakInto<TElement>(
    this IEnumerable<TElement> source,
    int batchSize)
  {
    int num = source.Count<TElement>();
    int capacity = num / batchSize + (num % batchSize > 0 ? 1 : 0);
    List<IGrouping<int, TElement>> groupingList = new List<IGrouping<int, TElement>>(capacity);
    IEnumerable<TElement> source1 = source;
    for (int index = 0; index < capacity; ++index)
    {
      groupingList.Add((IGrouping<int, TElement>) new CollectionExtensions.Grouping<int, TElement>(0, source1.Take<TElement>(batchSize)));
      source1 = source1.Skip<TElement>(batchSize);
    }
    return (IEnumerable<IGrouping<int, TElement>>) groupingList;
  }

  public static bool TryGetTypedValue<TKey, TValue, TActual>(
    this IDictionary<TKey, TValue> data,
    TKey key,
    out TActual value)
    where TActual : TValue
  {
    TValue obj;
    if (data.TryGetValue(key, out obj) && (object) obj is TActual)
    {
      TActual actual = (TActual) (object) obj;
      value = actual;
      return true;
    }
    value = default (TActual);
    return false;
  }

  public class Grouping<TKey, TElement> : 
    IGrouping<TKey, TElement>,
    IEnumerable<TElement>,
    IEnumerable
  {
    private readonly TKey key;
    private readonly IEnumerable<TElement> values;

    public Grouping(TKey key, IEnumerable<TElement> values)
    {
      if (values == null)
        throw new ArgumentNullException(nameof (values));
      this.key = key;
      this.values = values;
    }

    public TKey Key => this.key;

    public IEnumerator<TElement> GetEnumerator() => this.values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
  }
}
