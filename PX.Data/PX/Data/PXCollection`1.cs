// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCollection`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXCollection<T> : IEnumerable<T>, IEnumerable where T : class
{
  private PXCache _Cache;
  private int[] buckets;
  private int count;
  private PXCollection<T>.Entry[] entries;
  private int freeCount;
  private int freeList;
  private const string HashSizeName = "HashSize";
  private int version;
  private int _version;
  private const string VersionName = "Version";

  public PXCollection(PXCache cache) => this._Cache = cache;

  internal int Version
  {
    get => this._version;
    set => this._version = value;
  }

  public bool Contains(T value)
  {
    if (this.buckets == null)
      this.Initialize(0);
    int num = this._Cache.GetObjectHashCode((object) value) & int.MaxValue;
    for (int index = this.buckets[num % this.buckets.Length]; index >= 0; index = this.entries[index].next)
    {
      if (this.entries[index].hashCode == num && this._Cache.ObjectsEqual((object) this.entries[index].value, (object) value))
        return true;
    }
    return false;
  }

  public void Add(T value)
  {
    T obj = this.PlaceNotChanged(value, out bool _);
    if ((object) value != (object) obj)
      throw new PXArgumentException((string) null, "An attempt was made to add a duplicate entry.");
  }

  public PXEntryStatus GetStatus(T value)
  {
    if (this.buckets == null)
      this.Initialize(0);
    int num = this._Cache.GetObjectHashCode((object) value) & int.MaxValue;
    for (int index = this.buckets[num % this.buckets.Length]; index >= 0; index = this.entries[index].next)
    {
      if (this.entries[index].hashCode == num && this._Cache.ObjectsEqual((object) this.entries[index].value, (object) value))
        return this.entries[index].status != PXEntryStatus.Modified ? this.entries[index].status : PXEntryStatus.Updated;
    }
    return PXEntryStatus.Notchanged;
  }

  public T Locate(T value)
  {
    if (this.buckets == null)
      this.Initialize(0);
    int num = this._Cache.GetObjectHashCode((object) value) & int.MaxValue;
    for (int index = this.buckets[num % this.buckets.Length]; index >= 0; index = this.entries[index].next)
    {
      if (this.entries[index].hashCode == num && this._Cache.ObjectsEqual((object) this.entries[index].value, (object) value))
        return this.entries[index].value;
    }
    return default (T);
  }

  public void Remove(T value)
  {
    if (this.buckets == null)
      return;
    int num = this._Cache.GetObjectHashCode((object) value) & int.MaxValue;
    int index1 = -1;
    for (int index2 = this.buckets[num % this.buckets.Length]; index2 >= 0; index2 = this.entries[index2].next)
    {
      if (this.entries[index2].hashCode == num && this._Cache.ObjectsEqual((object) this.entries[index2].value, (object) value))
      {
        if (index1 < 0)
          this.buckets[num % this.buckets.Length] = this.entries[index2].next;
        else
          this.entries[index1].next = this.entries[index2].next;
        this.entries[index2].hashCode = -1;
        this.entries[index2].next = this.freeList;
        this.entries[index2].value = default (T);
        this.freeList = index2;
        ++this.freeCount;
        ++this.version;
        break;
      }
      index1 = index2;
    }
  }

  public PXEntryStatus? SetStatus(T value, PXEntryStatus status)
  {
    if (this.buckets == null)
      this.Initialize(0);
    int num = this._Cache.GetObjectHashCode((object) value) & int.MaxValue;
    for (int index = this.buckets[num % this.buckets.Length]; index >= 0; index = this.entries[index].next)
    {
      if (this.entries[index].hashCode == num && this._Cache.ObjectsEqual((object) this.entries[index].value, (object) value))
      {
        if ((status == PXEntryStatus.Deleted || status == PXEntryStatus.Inserted || status == PXEntryStatus.Modified || status == PXEntryStatus.Updated) && (this.entries[index].status == PXEntryStatus.Held || this.entries[index].status == PXEntryStatus.Notchanged))
          ++this._version;
        int status1 = (int) this.entries[index].status;
        if (status != PXEntryStatus.Modified || this.entries[index].status == PXEntryStatus.Notchanged || this.entries[index].status == PXEntryStatus.Held)
          this.entries[index].status = status != PXEntryStatus.Modified ? status : PXEntryStatus.Updated;
        return new PXEntryStatus?((PXEntryStatus) status1);
      }
    }
    int index1;
    if (this.freeCount > 0)
    {
      index1 = this.freeList;
      this.freeList = this.entries[index1].next;
      --this.freeCount;
    }
    else
    {
      if (this.count == this.entries.Length)
        this.Resize();
      index1 = this.count;
      ++this.count;
    }
    int index2 = num % this.buckets.Length;
    this.entries[index1].hashCode = num;
    this.entries[index1].next = this.buckets[index2];
    this.entries[index1].value = value;
    this.entries[index1].status = status;
    this.buckets[index2] = index1;
    ++this.version;
    if (status == PXEntryStatus.Deleted || status == PXEntryStatus.Inserted || status == PXEntryStatus.Modified || status == PXEntryStatus.Updated)
      ++this._version;
    return new PXEntryStatus?();
  }

  public T PlaceNotChanged(T value, out bool wasUpdated)
  {
    return this.PlaceNotChanged(value, false, out wasUpdated);
  }

  internal T PlaceNotChanged(T value, bool selecting, out bool wasUpdated)
  {
    wasUpdated = false;
    if (this.buckets == null)
      this.Initialize(0);
    int num = this._Cache.GetObjectHashCode((object) value) & int.MaxValue;
    for (int index = this.buckets[num % this.buckets.Length]; index >= 0; index = this.entries[index].next)
    {
      if (this.entries[index].hashCode == num && this._Cache.ObjectsEqual((object) this.entries[index].value, (object) value))
      {
        if (this.entries[index].status == PXEntryStatus.Deleted)
          return default (T);
        if (this.entries[index].status == PXEntryStatus.InsertedDeleted)
        {
          if (selecting)
            this.entries[index].status = PXEntryStatus.Deleted;
          return default (T);
        }
        if (this.entries[index].status == PXEntryStatus.Updated || this.entries[index].status == PXEntryStatus.Inserted)
          wasUpdated = true;
        else if (this.entries[index].status == PXEntryStatus.Modified)
        {
          wasUpdated = true;
          this.entries[index].status = PXEntryStatus.Updated;
        }
        return this.entries[index].value;
      }
    }
    int index1;
    if (this.freeCount > 0)
    {
      index1 = this.freeList;
      this.freeList = this.entries[index1].next;
      --this.freeCount;
    }
    else
    {
      if (this.count == this.entries.Length)
        this.Resize();
      index1 = this.count;
      ++this.count;
    }
    int index2 = num % this.buckets.Length;
    this.entries[index1].hashCode = num;
    this.entries[index1].next = this.buckets[index2];
    this.entries[index1].value = value;
    this.entries[index1].status = PXEntryStatus.Notchanged;
    this.buckets[index2] = index1;
    ++this.version;
    return value;
  }

  public T PlaceUpdated(T value, bool bypassCheck)
  {
    if (this.buckets == null)
      this.Initialize(0);
    int num = this._Cache.GetObjectHashCode((object) value) & int.MaxValue;
    for (int index = this.buckets[num % this.buckets.Length]; index >= 0; index = this.entries[index].next)
    {
      if (this.entries[index].hashCode == num && this._Cache.ObjectsEqual((object) this.entries[index].value, (object) value))
      {
        if (!bypassCheck && (this.entries[index].status == PXEntryStatus.Deleted || this.entries[index].status == PXEntryStatus.InsertedDeleted))
          return default (T);
        if (this.entries[index].status != PXEntryStatus.Inserted)
          this.entries[index].status = PXEntryStatus.Updated;
        ++this._version;
        return this.entries[index].value;
      }
    }
    if (!bypassCheck)
      return default (T);
    int index1;
    if (this.freeCount > 0)
    {
      index1 = this.freeList;
      this.freeList = this.entries[index1].next;
      --this.freeCount;
    }
    else
    {
      if (this.count == this.entries.Length)
        this.Resize();
      index1 = this.count;
      ++this.count;
    }
    int index2 = num % this.buckets.Length;
    this.entries[index1].hashCode = num;
    this.entries[index1].next = this.buckets[index2];
    this.entries[index1].value = value;
    this.entries[index1].status = PXEntryStatus.Updated;
    this.buckets[index2] = index1;
    ++this.version;
    ++this._version;
    return value;
  }

  /// <summary>
  /// places row into collection, returns the same value on success.<br />
  /// returns null if cache contains row with incompatible status
  /// </summary>
  /// <param name="value"></param>
  /// <param name="wasDeleted"></param>
  /// <returns></returns>
  public T PlaceInserted(T value, out bool wasDeleted)
  {
    wasDeleted = false;
    if (this.buckets == null)
      this.Initialize(0);
    int num1 = this._Cache.GetObjectHashCode((object) value) & int.MaxValue;
    for (int index = this.buckets[num1 % this.buckets.Length]; index >= 0; index = this.entries[index].next)
    {
      if (this.entries[index].hashCode == num1 && this._Cache.ObjectsEqual((object) this.entries[index].value, (object) value))
      {
        bool flag1;
        switch (this.entries[index].status)
        {
          case PXEntryStatus.Deleted:
          case PXEntryStatus.InsertedDeleted:
            flag1 = true;
            break;
          default:
            flag1 = false;
            break;
        }
        bool flag2 = flag1;
        if (this.entries[index].status != PXEntryStatus.Deleted && this.entries[index].status != PXEntryStatus.InsertedDeleted && this.entries[index].status != PXEntryStatus.Modified)
          return default (T);
        if (this.entries[index].status == PXEntryStatus.InsertedDeleted || this.entries[index].status == PXEntryStatus.Modified)
        {
          this.entries[index].status = PXEntryStatus.Inserted;
        }
        else
        {
          this.entries[index].status = PXEntryStatus.Updated;
          if (!string.IsNullOrEmpty(this._Cache._Identity))
          {
            object obj = this._Cache.GetValue((object) value, this._Cache._Identity);
            if (obj == null || obj is int num2 && num2 < 0 || obj is long num3 && num3 < 0L)
              this._Cache.SetValue((object) value, this._Cache._Identity, this._Cache.GetValue((object) this.entries[index].value, this._Cache._Identity));
          }
          if (!string.IsNullOrEmpty(this._Cache._Timestamp) && this._Cache.GetValue((object) value, this._Cache._Timestamp) == null)
            this._Cache.SetValue((object) value, this._Cache._Timestamp, this._Cache.GetValue((object) this.entries[index].value, this._Cache._Timestamp));
          wasDeleted = true;
        }
        if (flag2)
        {
          this._Cache.RestoreCopy((object) this.entries[index].value, (object) value);
          value = this.entries[index].value;
        }
        else
          this.entries[index].value = value;
        ++this.version;
        ++this._version;
        return value;
      }
    }
    int index1;
    if (this.freeCount > 0)
    {
      index1 = this.freeList;
      this.freeList = this.entries[index1].next;
      --this.freeCount;
    }
    else
    {
      if (this.count == this.entries.Length)
        this.Resize();
      index1 = this.count;
      ++this.count;
    }
    int index2 = num1 % this.buckets.Length;
    this.entries[index1].hashCode = num1;
    this.entries[index1].next = this.buckets[index2];
    this.entries[index1].value = value;
    this.entries[index1].status = PXEntryStatus.Inserted;
    this.buckets[index2] = index1;
    ++this.version;
    ++this._version;
    return value;
  }

  public T PlaceDeleted(T value, bool bypassCheck)
  {
    if (this.buckets == null)
      this.Initialize(0);
    int num = this._Cache.GetObjectHashCode((object) value) & int.MaxValue;
    for (int index = this.buckets[num % this.buckets.Length]; index >= 0; index = this.entries[index].next)
    {
      if (this.entries[index].hashCode == num && this._Cache.ObjectsEqual((object) this.entries[index].value, (object) value))
      {
        if (!bypassCheck && (this.entries[index].status == PXEntryStatus.Deleted || this.entries[index].status == PXEntryStatus.InsertedDeleted))
          return default (T);
        this.entries[index].status = this.entries[index].status == PXEntryStatus.Inserted ? PXEntryStatus.InsertedDeleted : PXEntryStatus.Deleted;
        ++this._version;
        return this.entries[index].value;
      }
    }
    if (!bypassCheck)
      return default (T);
    int index1;
    if (this.freeCount > 0)
    {
      index1 = this.freeList;
      this.freeList = this.entries[index1].next;
      --this.freeCount;
    }
    else
    {
      if (this.count == this.entries.Length)
        this.Resize();
      index1 = this.count;
      ++this.count;
    }
    int index2 = num % this.buckets.Length;
    this.entries[index1].hashCode = num;
    this.entries[index1].next = this.buckets[index2];
    this.entries[index1].value = value;
    this.entries[index1].status = PXEntryStatus.Deleted;
    this.buckets[index2] = index1;
    ++this.version;
    ++this._version;
    return value;
  }

  public bool IsDirty => this.Dirty.Any<T>();

  public IEnumerable<T> Inserted
  {
    get
    {
      for (int i = 0; i < this.count; ++i)
      {
        if (this.entries[i].hashCode >= 0 && this.entries[i].status == PXEntryStatus.Inserted)
          yield return this.entries[i].value;
      }
    }
  }

  public IEnumerable<T> NotChanged
  {
    get
    {
      for (int i = 0; i < this.count; ++i)
      {
        if (this.entries[i].hashCode >= 0 && (this.entries[i].status == PXEntryStatus.Notchanged || this.entries[i].status == PXEntryStatus.Held))
          yield return this.entries[i].value;
      }
    }
  }

  public IEnumerable<T> Cached
  {
    get
    {
      for (int i = 0; i < this.count; ++i)
      {
        if (this.entries[i].hashCode >= 0)
          yield return this.entries[i].value;
      }
    }
  }

  public IEnumerable<T> Dirty
  {
    get
    {
      for (int i = 0; i < this.count; ++i)
      {
        if (this.entries[i].hashCode >= 0 && (this.entries[i].status == PXEntryStatus.Inserted || this.entries[i].status == PXEntryStatus.Updated || this.entries[i].status == PXEntryStatus.Deleted || this.entries[i].status == PXEntryStatus.Modified))
          yield return this.entries[i].value;
      }
    }
  }

  public IEnumerable<T> Updated
  {
    get
    {
      for (int i = 0; i < this.count; ++i)
      {
        if (this.entries[i].hashCode >= 0 && (this.entries[i].status == PXEntryStatus.Updated || this.entries[i].status == PXEntryStatus.Modified))
          yield return this.entries[i].value;
      }
    }
  }

  public IEnumerable<T> Deleted
  {
    get
    {
      for (int i = 0; i < this.count; ++i)
      {
        if (this.entries[i].hashCode >= 0 && this.entries[i].status == PXEntryStatus.Deleted)
          yield return this.entries[i].value;
      }
    }
  }

  public IEnumerable<T> Held
  {
    get
    {
      for (int i = 0; i < this.count; ++i)
      {
        if (this.entries[i].hashCode >= 0 && this.entries[i].status == PXEntryStatus.Held)
          yield return this.entries[i].value;
      }
    }
  }

  public int CachedCount => this.count;

  public IEnumerator GetEnumerator() => (IEnumerator) new PXCollection<T>.Enumerator(this);

  IEnumerator<T> IEnumerable<T>.GetEnumerator()
  {
    return (IEnumerator<T>) new PXCollection<T>.Enumerator(this);
  }

  private void Initialize(int capacity)
  {
    int prime = HashHelpers.GetPrime(capacity);
    this.buckets = new int[prime];
    for (int index = 0; index < this.buckets.Length; ++index)
      this.buckets[index] = -1;
    this.entries = new PXCollection<T>.Entry[prime];
    this.freeList = -1;
  }

  private void Resize()
  {
    int prime = HashHelpers.GetPrime(this.count * 2);
    int[] numArray = new int[prime];
    for (int index = 0; index < numArray.Length; ++index)
      numArray[index] = -1;
    PXCollection<T>.Entry[] destinationArray = new PXCollection<T>.Entry[prime];
    Array.Copy((Array) this.entries, 0, (Array) destinationArray, 0, this.count);
    for (int index1 = 0; index1 < this.count; ++index1)
    {
      int index2 = destinationArray[index1].hashCode % prime;
      destinationArray[index1].next = numArray[index2];
      numArray[index2] = index1;
    }
    this.buckets = numArray;
    this.entries = destinationArray;
  }

  public void Normalize(T item) => this.Normalize(item, PXEntryStatus.Inserted);

  internal void Normalize(T item, PXEntryStatus status)
  {
    if (this.count <= 0)
      return;
    int num = -1;
    for (int index = 0; index < this.buckets.Length; ++index)
      this.buckets[index] = -1;
    for (int index1 = 0; index1 < this.count; ++index1)
    {
      if (this.entries[index1].hashCode >= 0)
      {
        if ((object) item != null && (object) item == (object) this.entries[index1].value)
          this.entries[index1].hashCode = num = this._Cache.GetObjectHashCode((object) this.entries[index1].value) & int.MaxValue;
        else if (this.entries[index1].status == status)
          this.entries[index1].hashCode = this._Cache.GetObjectHashCode((object) this.entries[index1].value) & int.MaxValue;
      }
      int index2 = this.entries[index1].hashCode % this.buckets.Length;
      if (index2 != -1)
      {
        this.entries[index1].next = this.buckets[index2];
        this.buckets[index2] = index1;
      }
    }
    if ((object) item == null || num < 0)
      return;
    for (int index = this.buckets[num % this.buckets.Length]; index >= 0; index = this.entries[index].next)
    {
      if (this.entries[index].hashCode == num && (object) this.entries[index].value != (object) item && this._Cache.ObjectsEqual((object) this.entries[index].value, (object) item))
        throw new PXBadDictinaryException();
    }
  }

  /// <exclude />
  [DebuggerDisplay("[{status}] {value}")]
  private struct Entry
  {
    public int hashCode;
    public int next;
    public PXEntryStatus status;
    public T value;
  }

  /// <exclude />
  public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
  {
    private PXCollection<T> collection;
    private int version;
    private int index;
    private T current;

    internal Enumerator(PXCollection<T> collection)
    {
      this.collection = collection;
      this.version = collection.version;
      this.index = 0;
      this.current = default (T);
    }

    public bool MoveNext()
    {
      if (this.version != this.collection.version)
        throw new PXInvalidOperationException("The collection has been changed.");
      for (; this.index < this.collection.count; ++this.index)
      {
        if (this.collection.entries[this.index].hashCode >= 0)
        {
          this.current = this.collection.entries[this.index].value;
          ++this.index;
          return true;
        }
      }
      this.index = this.collection.count + 1;
      this.current = default (T);
      return false;
    }

    public T Current => this.current;

    public void Dispose()
    {
    }

    object IEnumerator.Current
    {
      get
      {
        if (this.index == 0 || this.index == this.collection.count + 1)
          throw new PXInvalidOperationException("Enumeration cannot be started.");
        return (object) this.current;
      }
    }

    void IEnumerator.Reset()
    {
      if (this.version != this.collection.version)
        throw new PXInvalidOperationException("The collection has been changed.");
      this.index = 0;
      this.current = default (T);
    }
  }
}
