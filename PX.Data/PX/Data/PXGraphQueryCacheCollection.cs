// Decompiled with JetBrains decompiler
// Type: PX.Data.PXGraphQueryCacheCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <exclude />
[Serializable]
public sealed class PXGraphQueryCacheCollection : Dictionary<ViewKey, PXViewQueryCollection>
{
  public PXGraphQueryCacheCollection()
  {
  }

  private PXGraphQueryCacheCollection(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  internal PXGraphQueryCacheCollection Unload()
  {
    PXGraphQueryCacheCollection queryCacheCollection = new PXGraphQueryCacheCollection();
    foreach (KeyValuePair<ViewKey, PXViewQueryCollection> keyValuePair in this.ToArray<KeyValuePair<ViewKey, PXViewQueryCollection>>())
    {
      keyValuePair.Value.Unload();
      foreach (PXQueryResult pxQueryResult in keyValuePair.Value.Values)
      {
        if (pxQueryResult.Items is PXView.VersionedList items)
          items.MergedList = (PXView.VersionedList) null;
      }
      queryCacheCollection.Add(keyValuePair.Key, keyValuePair.Value);
    }
    return queryCacheCollection;
  }
}
