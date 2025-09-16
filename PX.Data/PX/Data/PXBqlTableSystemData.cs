// Decompiled with JetBrains decompiler
// Type: PX.Data.PXBqlTableSystemData
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
[Serializable]
public struct PXBqlTableSystemData
{
  internal PXBqlTableSystemData.CollectionValue<PXCacheExtension[]> Extensions;
  internal PXBqlTableSystemData.CollectionValue<PX.Data.BqlTablePair> BqlTablePair;

  [Serializable]
  internal struct CollectionValue<T> where T : class
  {
    private T value;

    public bool Remove()
    {
      int num = this.Contains() ? 1 : 0;
      this.value = default (T);
      return num != 0;
    }

    public bool Contains() => (object) this.value != null;

    public bool TryGetValue(out T value)
    {
      value = this.value;
      return this.Contains();
    }

    public static implicit operator T(
      PXBqlTableSystemData.CollectionValue<T> collectionValue)
    {
      return collectionValue.value;
    }

    public static implicit operator PXBqlTableSystemData.CollectionValue<T>(T value)
    {
      return new PXBqlTableSystemData.CollectionValue<T>()
      {
        value = value
      };
    }
  }
}
