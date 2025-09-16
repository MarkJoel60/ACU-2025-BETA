// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCancelDirtyKeys`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Sets IsDirty to true if key fields have been changed (to enable Save button without changing non-key fields).
/// </summary>
/// <typeparam name="TNode"></typeparam>
public class PXCancelDirtyKeys<TNode> : PXCancel<TNode> where TNode : class, IBqlTable, new()
{
  public PXCancelDirtyKeys(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public PXCancelDirtyKeys(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  [PXUIField(DisplayName = "Cancel", MapEnableRights = PXCacheRights.Select)]
  [PXCancelButton]
  protected override IEnumerable Handler(PXAdapter adapter)
  {
    IEnumerable enumerable = base.Handler(adapter);
    if (adapter.MaximumRows == 1)
    {
      PXCache cache = adapter.View.Cache;
      foreach (object data in enumerable)
      {
        if (!cache.IsDirty && cache.GetStatus(data) == PXEntryStatus.Inserted)
        {
          foreach (string key in (IEnumerable<string>) cache.Keys)
          {
            if (cache.GetValue(data, key) != null)
            {
              cache.IsDirty = true;
              break;
            }
          }
        }
        yield return data;
      }
      cache = (PXCache) null;
    }
  }
}
