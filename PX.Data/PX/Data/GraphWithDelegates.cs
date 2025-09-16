// Decompiled with JetBrains decompiler
// Type: PX.Data.GraphWithDelegates
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using System.Collections;

#nullable disable
namespace PX.Data;

public class GraphWithDelegates : PXGraph<GraphWithDelegates>
{
  public PXSelect<SYMapping> Items;
  public PXSelect<SYMapping> ItemsCached;

  protected virtual IEnumerable items()
  {
    return (IEnumerable) new PXDelegateResult()
    {
      IsResultFiltered = true,
      IsResultTruncated = true,
      IsResultSorted = true
    };
  }

  protected virtual IEnumerable itemsCached()
  {
    object[] parameters = new object[1]{ (object) "XXX" };
    PXDelegateCacheResult result = new PXDelegateCacheResult()
    {
      IsResultCachable = true,
      CacheKeys = parameters
    };
    result.OnEmitRows = (System.Action) (() =>
    {
      foreach (object obj in PXSelectBase<SYMapping, PXSelect<SYMapping>.Config>.Select((PXGraph) this, parameters))
        result.Add(obj);
    });
    return (IEnumerable) result;
  }
}
