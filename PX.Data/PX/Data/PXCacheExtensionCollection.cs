// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCacheExtensionCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;

#nullable disable
namespace PX.Data;

internal class PXCacheExtensionCollection : ICollection, IEnumerable
{
  private static readonly object[] Empty = Array.Empty<object>();

  public static PXCacheExtensionCollection GetSlot(bool createIfNotExists)
  {
    PXCacheExtensionCollection slot = PXContext.GetSlot<PXCacheExtensionCollection>();
    if (createIfNotExists && slot == null)
      slot = PXContext.SetSlot<PXCacheExtensionCollection>(new PXCacheExtensionCollection());
    return slot;
  }

  public PXCacheExtension[] this[IBqlTable key]
  {
    set
    {
      if (value != null)
        key.SetExtensions(value);
      else
        key.ClearExtensions();
    }
  }

  public bool TryGetValue(IBqlTable key, out PXCacheExtension[] value)
  {
    return key.TryGetExtensions(out value);
  }

  object ICollection.SyncRoot { get; } = new object();

  void ICollection.CopyTo(Array array, int index)
  {
  }

  int ICollection.Count => 0;

  bool ICollection.IsSynchronized => false;

  IEnumerator IEnumerable.GetEnumerator() => PXCacheExtensionCollection.Empty.GetEnumerator();
}
