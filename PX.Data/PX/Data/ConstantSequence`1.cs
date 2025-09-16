// Decompiled with JetBrains decompiler
// Type: PX.Data.ConstantSequence`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

public class ConstantSequence<TField> : IBqlConstants where TField : IBqlField
{
  public IEnumerable<object> GetValues(PXGraph graph)
  {
    PXCache pxCache;
    if (graph == null || !graph.Caches.TryGetValue(BqlCommand.GetItemType<TField>(), out pxCache))
      return (IEnumerable<object>) Array.Empty<object>();
    return pxCache.GetValue<TField>(pxCache.Current) is IEnumerable<object> objects ? objects : (IEnumerable<object>) Array.Empty<object>();
  }
}
