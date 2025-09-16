// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Extensions.EnumerableExtensions
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
namespace PX.Objects.Common.Extensions;

/// <summary>
/// This class contains extension methods for enumerable objects.
/// </summary>
public static class EnumerableExtensions
{
  /// <summary>
  /// Returns <c>true</c> if the collection is empty. If the collection is <c>null</c>,
  /// an exception is thrown.
  /// </summary>
  /// <typeparam name="T">Generic type parameter.</typeparam>
  /// <param name="sequence">The enumerable object.</param>
  /// <returns></returns>
  public static bool IsEmpty<T>(this IEnumerable<T> sequence) => !sequence.Any<T>();

  /// <summary>
  /// Returns <c>true</c> if <paramref name="sequence" /> contains exactly one element.
  /// Otherwise, the method returns <c>false</c>.
  /// </summary>
  /// <typeparam name="T">Generic type parameter.</typeparam>
  /// <param name="sequence">The enumerable object.</param>
  /// <returns></returns>
  public static bool IsSingleElement<T>(this IEnumerable<T> sequence)
  {
    if (sequence == null)
      throw new ArgumentNullException(nameof (sequence));
    using (IEnumerator<T> enumerator = sequence.GetEnumerator())
      return enumerator.MoveNext() && !enumerator.MoveNext();
  }

  /// <exclude />
  public static IEnumerable<TNode> DistinctByKeys<TNode>(
    this IEnumerable<TNode> sequence,
    PXGraph graph)
    where TNode : class, IBqlTable, new()
  {
    return sequence.DistinctByKeys<TNode>(graph?.Caches[typeof (TNode)]);
  }

  /// <summary>
  /// For a sequence of records and a <see cref="T:PX.Data.PXCache" /> object,
  /// returns a sequence of elements that have different keys.
  /// </summary>
  /// <remarks>The collection of keys is defined by <see cref="P:PX.Data.PXCache.Keys" />.</remarks>
  public static IEnumerable<TNode> DistinctByKeys<TNode>(
    this IEnumerable<TNode> sequence,
    PXCache cache)
    where TNode : class, IBqlTable, new()
  {
    if (sequence == null)
      throw new ArgumentNullException(nameof (sequence));
    return cache != null ? sequence.Distinct<TNode>((IEqualityComparer<TNode>) new RecordKeyComparer<TNode>(cache)) : throw new ArgumentNullException(nameof (cache));
  }

  /// <summary>
  /// Returns the index of the first element that satisfies the
  /// specified predicate, or a negative value if such an
  /// element cannot be found.
  /// </summary>
  /// <remarks>
  /// If no element satisfying the predicate can
  /// be found, the negative value returned by this method is
  /// the opposite of the number of elements in the sequence
  /// increased by one (namely, -(N+1)).
  /// </remarks>
  public static int FindIndex<T>(this IEnumerable<T> sequence, Predicate<T> predicate)
  {
    int index = 0;
    foreach (T obj in sequence)
    {
      if (predicate(obj))
        return index;
      ++index;
    }
    return -index - 1;
  }

  /// <summary>
  /// Returns <c>true</c> if <paramref name="sequence" /> contains two or more elements.
  /// Otherwise, the method returns <c>false</c>.
  /// </summary>
  /// <typeparam name="T">Generic type parameter.</typeparam>
  /// <param name="sequence">The enumerable object.</param>
  /// <returns></returns>
  public static bool HasAtLeastTwoItems<T>(this IEnumerable<T> sequence)
  {
    return EnumerableExtensions.HasAtLeast<T>(sequence, 2);
  }

  /// <summary>
  /// Flattens a sequence of element groups into a sequence of elements.
  /// </summary>
  public static IEnumerable<TValue> Flatten<TKey, TValue>(
    this IEnumerable<IGrouping<TKey, TValue>> sequenceOfGroups)
  {
    return sequenceOfGroups.SelectMany<IGrouping<TKey, TValue>, TValue>((Func<IGrouping<TKey, TValue>, IEnumerable<TValue>>) (x => (IEnumerable<TValue>) x));
  }

  /// <summary>
  /// Returns a row with the sum of all decimal fields and the identical value if all rows contains that value;
  /// otherwise, the method returns <c>null</c>.
  /// </summary>
  public static RecordType CalculateSumTotal<RecordType>(
    this IEnumerable<RecordType> rows,
    PXCache cache)
    where RecordType : IBqlTable, new()
  {
    RecordType sumTotal = new RecordType();
    List<\u003C\u003Ef__AnonymousType25<PXUIFieldAttribute, bool>> list1 = cache.GetAttributesOfType<PXUIFieldAttribute>((object) null, (string) null).Select(p => new
    {
      uiAttribute = p,
      sumGroupOperation = cache.GetAttributesOfType<PXDecimalAttribute>((object) null, ((PXEventSubscriberAttribute) p).FieldName).Any<PXDecimalAttribute>() || cache.GetAttributesOfType<PXDBDecimalAttribute>((object) null, ((PXEventSubscriberAttribute) p).FieldName).Any<PXDBDecimalAttribute>()
    }).ToList();
    List<\u003C\u003Ef__AnonymousType25<PXUIFieldAttribute, bool>> list2 = list1.Where(p => p.sumGroupOperation).ToList();
    List<\u003C\u003Ef__AnonymousType25<PXUIFieldAttribute, bool>> list3 = list1.Where(p => !p.sumGroupOperation).ToList();
    Decimal[] numArray = new Decimal[list2.Count];
    object[] objArray1 = new object[list3.Count];
    bool flag = true;
    foreach (RecordType row in rows)
    {
      for (int index = 0; index < list2.Count; ++index)
      {
        Decimal? nullable = (Decimal?) cache.GetValue((object) row, ((PXEventSubscriberAttribute) list2[index].uiAttribute).FieldName);
        numArray[index] += nullable.GetValueOrDefault();
      }
      for (int index1 = 0; index1 < list3.Count; ++index1)
      {
        object obj1 = cache.GetValue((object) row, ((PXEventSubscriberAttribute) list3[index1].uiAttribute).FieldName);
        object[] objArray2 = objArray1;
        int index2 = index1;
        object obj2;
        if (!flag)
        {
          object obj3 = objArray1[index1];
          if ((obj3 != null ? (obj3.Equals(obj1) ? 1 : 0) : 0) == 0)
          {
            obj2 = (object) null;
            goto label_10;
          }
        }
        obj2 = obj1;
label_10:
        objArray2[index2] = obj2;
      }
      flag = false;
    }
    for (int index = 0; index < list2.Count; ++index)
      cache.SetValue((object) sumTotal, ((PXEventSubscriberAttribute) list2[index].uiAttribute).FieldName, (object) numArray[index]);
    for (int index = 0; index < list3.Count; ++index)
      cache.SetValue((object) sumTotal, ((PXEventSubscriberAttribute) list3[index].uiAttribute).FieldName, objArray1[index]);
    return sumTotal;
  }

  /// <exclude />
  public static IEnumerable<TResult> Batch<TSource, TResult>(
    this IEnumerable<TSource> source,
    int size,
    Func<IEnumerable<TSource>, TResult> resultSelector)
  {
    if (source == null)
      throw new ArgumentNullException(nameof (source));
    if (size <= 0)
      throw new ArgumentOutOfRangeException(nameof (size));
    if (resultSelector == null)
      throw new ArgumentNullException(nameof (resultSelector));
    return _();

    IEnumerable<TResult> _()
    {
      TSource[] array = (TSource[]) null;
      int newSize = 0;
      foreach (TSource source in source)
      {
        if (array == null)
          array = new TSource[size];
        array[newSize++] = source;
        if (newSize == size)
        {
          yield return resultSelector((IEnumerable<TSource>) array);
          array = (TSource[]) null;
          newSize = 0;
        }
      }
      if (array != null && newSize > 0)
      {
        Array.Resize<TSource>(ref array, newSize);
        yield return resultSelector((IEnumerable<TSource>) array);
      }
    }
  }

  /// <summary>
  /// Sorts the elements of a sequence in accordance to the <paramref name="predicate" />, elements that meet the condition come first.
  /// </summary>
  public static IOrderedEnumerable<T> OrderByAccordanceTo<T>(
    this IEnumerable<T> source,
    Func<T, bool> predicate)
  {
    return source.OrderByDescending<T, bool>(predicate);
  }

  /// <summary>
  /// Performs a subsequent ordering of the elements in a sequence in accordance to the <paramref name="predicate" />, elements that meet the condition come first.
  /// </summary>
  public static IOrderedEnumerable<T> ThenByAccordanceTo<T>(
    this IOrderedEnumerable<T> source,
    Func<T, bool> predicate)
  {
    return source.ThenByDescending<T, bool>(predicate);
  }

  /// <exclude />
  public static IEnumerable<T> BeginWith<T>(this IEnumerable<T> source, Func<T, bool> predicate)
  {
    if (source == null)
      throw new ArgumentNullException(nameof (source));
    if (predicate == null)
      throw new ArgumentNullException(nameof (predicate));
    return source.Where<T>(predicate).Concat<T>(source.Where<T>((Func<T, bool>) (x => !predicate(x))));
  }
}
