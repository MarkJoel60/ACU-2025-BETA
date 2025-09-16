// Decompiled with JetBrains decompiler
// Type: PX.Data.PXHashtable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXHashtable
{
  private int[] buckets;
  private int count;
  private PXHashtable.Entry[] entries;
  private int freeCount;
  private int freeList;

  private bool objectEquals(object[] itemKeys, object[] keys)
  {
    if (itemKeys.Length != keys.Length)
      return false;
    for (int index = 0; index < keys.Length; ++index)
    {
      if (!object.Equals(itemKeys[index], keys[index]))
        return false;
    }
    return true;
  }

  private object[] getKeys(PXCache sender, object item)
  {
    object[] keys = new object[sender.Keys.Count];
    for (int index = 0; index < keys.Length; ++index)
      keys[index] = sender.GetValue(item, sender.Keys[index]);
    return keys;
  }

  public void Put(PXCache sender, object item, params object[] values)
  {
    if (this.buckets == null)
      this.Initialize(0);
    int num = sender.GetObjectHashCode(item) & int.MaxValue;
    object[] keys = this.getKeys(sender, item);
    for (int index = this.buckets[num % this.buckets.Length]; index >= 0; index = this.entries[index].next)
    {
      if (this.entries[index].hashCode == num && this.objectEquals(keys, this.entries[index].keys))
      {
        this.entries[index].values = values;
        return;
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
    this.entries[index1].keys = keys;
    this.entries[index1].values = values;
    this.buckets[index2] = index1;
  }

  public object[] Get(PXCache sender, object item)
  {
    if (this.buckets == null)
      this.Initialize(0);
    int num = sender.GetObjectHashCode(item) & int.MaxValue;
    object[] keys = this.getKeys(sender, item);
    for (int index = this.buckets[num % this.buckets.Length]; index >= 0; index = this.entries[index].next)
    {
      if (this.entries[index].hashCode == num && this.objectEquals(keys, this.entries[index].keys))
        return this.entries[index].values;
    }
    return (object[]) null;
  }

  private void Initialize(int capacity)
  {
    int prime = HashHelpers.GetPrime(capacity);
    this.buckets = new int[prime];
    for (int index = 0; index < this.buckets.Length; ++index)
      this.buckets[index] = -1;
    this.entries = new PXHashtable.Entry[prime];
    this.freeList = -1;
  }

  private void Resize()
  {
    int prime = HashHelpers.GetPrime(this.count * 2);
    int[] numArray = new int[prime];
    for (int index = 0; index < numArray.Length; ++index)
      numArray[index] = -1;
    PXHashtable.Entry[] destinationArray = new PXHashtable.Entry[prime];
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

  /// <exclude />
  private struct Entry
  {
    public int hashCode;
    public int next;
    public object[] keys;
    public object[] values;
  }
}
