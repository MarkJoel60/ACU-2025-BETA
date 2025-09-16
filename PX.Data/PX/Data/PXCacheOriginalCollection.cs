// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCacheOriginalCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

internal sealed class PXCacheOriginalCollection
{
  internal static readonly PXCacheOriginalCollection Instance = new PXCacheOriginalCollection();

  private PXCacheOriginalCollection()
  {
  }

  public bool TryGetValue(IBqlTable key, out BqlTablePair value)
  {
    return key.GetBqlTableSystemData().BqlTablePair.TryGetValue(out value);
  }

  public BqlTablePair this[IBqlTable key]
  {
    set
    {
      key.GetBqlTableSystemData().BqlTablePair = (PXBqlTableSystemData.CollectionValue<BqlTablePair>) value;
    }
  }

  public void Remove(IBqlTable key) => key.GetBqlTableSystemData().BqlTablePair.Remove();
}
