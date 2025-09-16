// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.HashedList`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CS;

public class HashedList<T> : ICollection<T>, IEnumerable<T>, IEnumerable where T : class, IBqlTable, new()
{
  private readonly HashSet<T> _keys;
  private readonly List<T> _list;

  public HashedList(IEqualityComparer<T> comparer = null)
  {
    this._keys = new HashSet<T>(comparer);
    this._list = new List<T>();
  }

  public void Add(T item)
  {
    if (this._keys.Contains(item))
      return;
    this._keys.Add(item);
    this._list.Add(item);
  }

  public void AddRange(IEnumerable items)
  {
    foreach (T obj in items)
      this.Add(obj);
  }

  public void Sort(IComparer<T> comparer) => this._list.Sort(comparer);

  public void Sort(Comparison<T> comparison) => this._list.Sort(comparison);

  public void Clear()
  {
    this._keys.Clear();
    this._list.Clear();
  }

  public IEnumerator<T> GetEnumerator() => (IEnumerator<T>) this._list.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this._list.GetEnumerator();

  public bool Contains(T item) => this._keys.Contains(item);

  public void CopyTo(T[] array, int arrayIndex) => this._list.CopyTo(array, arrayIndex);

  public bool Remove(T item) => this._list.Remove(item);

  public int Count => this._list.Count<T>();

  public bool IsReadOnly => false;
}
