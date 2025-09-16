// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNonSqlView`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

public class PXNonSqlView<TTable> : PXSelect<TTable>, IPXSelectInterceptor where TTable : class, IBqlTable, new()
{
  public Func<bool, IList> GetCollection;

  public IEnumerable<TTable> SelectResult()
  {
    foreach (TTable table in this.Getter())
      yield return table;
  }

  protected virtual IEnumerable Getter()
  {
    PXNonSqlView<TTable> pxNonSqlView = this;
    if (pxNonSqlView.GetCollection == null)
      throw new PXException("PXNonSqlView source is null:" + typeof (TTable).FullName);
    IList list = pxNonSqlView.GetCollection(false);
    if (list != null)
    {
      foreach (object obj in (IEnumerable) list)
      {
        if (pxNonSqlView.Cache.Locate(obj) == null)
          pxNonSqlView.Cache.SetStatus(obj, PXEntryStatus.Held);
      }
      foreach (object obj in pxNonSqlView.Cache.Cached)
      {
        switch (pxNonSqlView.Cache.GetStatus(obj))
        {
          case PXEntryStatus.Deleted:
          case PXEntryStatus.InsertedDeleted:
            continue;
          default:
            yield return obj;
            continue;
        }
      }
    }
  }

  public PXNonSqlView(PXGraph graph)
    : base(graph)
  {
    PXNonSqlView<TTable> pxNonSqlView = this;
    this.View = new PXView(graph, false, PXSelectBase<TTable, PXSelect<TTable>.Config>.GetCommand(), (Delegate) new PXSelectDelegate(this.Getter))
    {
      CacheType = typeof (TTable)
    };
    graph.Caches.SubscribeCacheCreated<TTable>((System.Action) (() =>
    {
      Interceptor interceptor = new Interceptor()
      {
        Source = (Func<bool, IList>) (b => pxNonSqlView.GetCollection(b)),
        ItemType = pxNonSqlView.GetItemType()
      };
      pxNonSqlView.Cache.Interceptor = (PXDBInterceptorAttribute) interceptor;
      pxNonSqlView.Cache.SelectInterceptor = (IPXSelectInterceptor) pxNonSqlView;
      pxNonSqlView.Cache.DisableReadItem = true;
      graph.OnBeforeCommit += (System.Action<PXGraph>) (_param1 =>
      {
        System.Action commit = interceptor.Commit;
        if (commit != null)
          commit();
        interceptor.Commit = (System.Action) null;
      });
      pxNonSqlView.Cache.RowUpdated += (PXRowUpdated) ((sender, args) =>
      {
        if (args.Row == null || args.OldRow == null)
          return;
        object row = args.Row;
        object oldRow = args.OldRow;
        int indexOf = interceptor.GetIndexOf(oldRow, sender);
        if (indexOf < 0)
          return;
        object copy = sender.CreateCopy(row);
        IList list = interceptor.Source(true);
        object obj = list[indexOf];
        if (!sender.ObjectsEqual(row, oldRow))
          sender.Remove(obj);
        list[indexOf] = copy;
      });
    }));
  }

  public IEnumerable<object> Select(
    PXGraph graph,
    BqlCommand command,
    int topCount,
    PXView view,
    PXDataValue[] pars)
  {
    return ((IEnumerable<object>) this.GetCollection(false)).Where<object>((Func<object, bool>) (it =>
    {
      BqlCommand bqlCommand = command;
      PXCache cache = view.Cache;
      object obj = it;
      PXDataValue[] source = pars;
      object[] array = source != null ? ((IEnumerable<PXDataValue>) source).Select<PXDataValue, object>((Func<PXDataValue, object>) (p => p.Value)).ToArray<object>() : (object[]) null;
      return bqlCommand.Meet(cache, obj, array);
    }));
  }
}
