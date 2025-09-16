// Decompiled with JetBrains decompiler
// Type: <>z__ReadOnlyArray`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
internal sealed class \u003C\u003Ez__ReadOnlyArray<T> : 
  IEnumerable,
  IEnumerable<T>,
  IReadOnlyCollection<T>,
  IReadOnlyList<T>,
  ICollection<T>,
  IList<T>
{
  public \u003C\u003Ez__ReadOnlyArray(T[] items) => this._items = items;

  IEnumerator IEnumerable.GetEnumerator() => this._items.GetEnumerator();

  IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>) this._items).GetEnumerator();

  int IReadOnlyCollection<T>.Count => this._items.Length;

  [IndexerName("System.Collections.Generic.IReadOnlyList<T>.this[]")]
  T IReadOnlyList<T>.this[int index] => this._items[index];

  int ICollection<T>.Count => this._items.Length;

  bool ICollection<T>.IsReadOnly => true;

  void ICollection<T>.Add(T item) => throw new NotSupportedException();

  void ICollection<T>.Clear() => throw new NotSupportedException();

  bool ICollection<T>.Contains(T item) => ((ICollection<T>) this._items).Contains(item);

  void ICollection<T>.CopyTo(T[] array, int arrayIndex)
  {
    // ISSUE: reference to a compiler-generated field
    ((ICollection<T>) this._items).CopyTo(array, arrayIndex);
  }

  bool ICollection<T>.Remove(T item) => throw new NotSupportedException();

  [IndexerName("System.Collections.Generic.IList<T>.this[]")]
  T IList<T>.this[int index]
  {
    get => this._items[index];
    set => throw new NotSupportedException();
  }

  int IList<T>.IndexOf(T item) => ((IList<T>) this._items).IndexOf(item);

  void IList<T>.Insert(int index, T item) => throw new NotSupportedException();

  void IList<T>.RemoveAt(int index) => throw new NotSupportedException();
}
