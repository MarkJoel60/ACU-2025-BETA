// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSetup`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data;

/// <summary>
/// A data view intended for retrieving configuration data from the table.<br />
/// If a record with the configuration data exists in the DB, it is available in the <see cref="P:PX.Data.PXSelectBase`1.Current">Current</see> property;
/// otherwise, <see cref="T:PX.Data.PXSetupNotEnteredException" /> is thrown that typically leads to a redirect to the corresponding configuration form.
/// </summary>
/// <typeparam name="Table">The DAC that represents the table from which
/// data records are retrieved.</typeparam>
/// <exception cref="T:PX.Data.PXSetupNotEnteredException"><typeparamref name="Table" /> is empty (the record is <see langword="null" />).</exception>
/// <exception cref="T:PX.Data.PXNotEnoughRightsException">The current user does not have enough access rights for <typeparamref name="Table" />.</exception>
/// <exception cref="T:PX.Data.PXSetPropertyException">Validation failed.</exception>
public class PXSetup<Table> : PXSelectReadonly<Table>, IPXNonUpdateable where Table : class, IBqlTable, new()
{
  protected Table _Record;
  protected static Dictionary<System.Type, string> _members = new Dictionary<System.Type, string>();

  public PXSetup(PXGraph graph)
    : base(graph)
  {
    graph.Defaults[typeof (Table)] = new PXGraph.GetDefaultDelegate(this.getRecord);
  }

  private protected virtual object getRecord()
  {
    if ((object) this._Record == null)
    {
      try
      {
        PXSetup<Table>.SetupData slot = PXSetup<Table>.GetSlot();
        this._Record = slot.GetRecord(this._Graph);
        if ((object) this._Record == null)
        {
          if (slot?.Exception != null)
            throw slot?.Exception;
          if (!this.Cache.AllowSelect)
            throw new PXNotEnoughRightsException(PXCacheRights.Select);
          throw new PXSetupNotEnteredException<Table>();
        }
      }
      catch (PXSetPropertyException ex)
      {
        throw;
      }
      catch
      {
      }
    }
    return (object) this._Record;
  }

  private static PXSetup<Table>.SetupData GetSlot()
  {
    return PXDatabase.GetSlot<PXSetup<Table>.SetupData>(typeof (Table).FullName + "Prefetch", PXCache.GetBqlTable(typeof (Table)));
  }

  public new static PXResultset<Table> Select(PXGraph graph, params object[] pars)
  {
    return PXSetup<Table>.SelectMultiBound(graph, (object[]) null, pars);
  }

  public new static PXResultset<Table> SelectMultiBound(
    PXGraph graph,
    object[] currents,
    params object[] pars)
  {
    PXCache cach = graph.Caches[typeof (Table)];
    object i0_1 = cach.Keys.Count == 0 ? cach.InternalCurrent ?? (object) cach.Inserted.Cast<Table>().FirstOrDefault<Table>() : (object) null;
    if (i0_1 == null)
    {
      PXGraph.GetDefaultDelegate getDefaultDelegate;
      if (graph.Defaults.TryGetValue(typeof (Table), out getDefaultDelegate))
      {
        cach.Current = (object) null;
        try
        {
          i0_1 = getDefaultDelegate();
        }
        catch (PXSetPropertyException ex)
        {
        }
      }
      else if (cach.Keys.Count == 0)
        i0_1 = (object) PXSetup<Table>.GetSlot().GetRecord(graph);
    }
    if (i0_1 != null)
      return new PXResultset<Table>()
      {
        new PXResult<Table>(i0_1 as Table)
      };
    string key1 = (string) null;
    lock (((ICollection) PXSetup<Table>._members).SyncRoot)
    {
      if (!PXSetup<Table>._members.TryGetValue(graph.GetType(), out key1))
      {
        foreach (string key2 in (IEnumerable<string>) graph.Views.Keys.OrderBy<string, string>((Func<string, string>) (s => s), (IComparer<string>) new PXSetup<Table>.ViewNameComparer()))
        {
          PXView view = graph.Views[key2];
          if (!view.IsReadOnly && (view.GetItemType() == typeof (Table) || view.GetItemType().IsAssignableFrom(typeof (Table)) && !Attribute.IsDefined((MemberInfo) typeof (Table), typeof (PXBreakInheritanceAttribute), false)))
          {
            key1 = key2;
            break;
          }
        }
        PXSetup<Table>._members[graph.GetType()] = key1;
      }
      if (!string.IsNullOrEmpty(key1))
      {
        PXView view = graph.Views[key1];
        if (PXSetup<Table>.GetViewExternalMember(graph, view) is PXSelectBase<Table> viewExternalMember)
          return viewExternalMember.selectBound(currents, pars);
        PXResultset<Table> pxResultset = new PXResultset<Table>();
        foreach (object obj in view.SelectMultiBound(currents, pars))
        {
          if (!(obj is PXResult<Table>))
          {
            if (obj is Table i0_2)
              pxResultset.Add(new PXResult<Table>(i0_2));
          }
          else
            pxResultset.Add((PXResult<Table>) obj);
        }
        return pxResultset;
      }
    }
    return (PXResultset<Table>) null;
  }

  public static PXSelectBase GetViewExternalMember(PXGraph graph, PXView view)
  {
    return graph.Views.GetExternalMember(view);
  }

  private class SetupData : IPrefetchable, IPXCompanyDependent
  {
    private Table _record;
    private PXCacheExtension[] _extensions;
    public Exception Exception;

    void IPrefetchable.Prefetch()
    {
      try
      {
        this.Store((Table) PXSelectBase<Table, PXSelect<Table>.Config>.SelectSingleBound(new PXGraph(), (object[]) null));
      }
      catch (Exception ex)
      {
        this.Exception = ex;
      }
    }

    public Table GetRecord(PXGraph graph)
    {
      if ((object) this._record == null)
        return default (Table);
      if (this._extensions != null)
      {
        IBqlTable record = (IBqlTable) this._record;
        PXCacheExtensionCollection slot = PXCacheExtensionCollection.GetSlot(true);
        lock (((ICollection) slot).SyncRoot)
          slot[record] = this._extensions;
      }
      return graph.Caches[typeof (Table)].CreateCopy((object) this._record) as Table;
    }

    private void Store(Table record)
    {
      this._record = record;
      if ((object) record == null)
        return;
      lock (((ICollection) PXCacheExtensionCollection.GetSlot(true)).SyncRoot)
        this._extensions = record.GetExtensions();
    }
  }

  public sealed class Where<TCondtion>(PXGraph graph) : PXSetup<Table, Where<TCondtion>>(graph) where TCondtion : IBqlUnary, new()
  {
  }

  protected class ViewNameComparer : IComparer<string>
  {
    public int Compare(string x, string y)
    {
      if (x.StartsWith("_", StringComparison.OrdinalIgnoreCase) && !y.StartsWith("_", StringComparison.OrdinalIgnoreCase))
        return 1;
      return !x.StartsWith("_", StringComparison.OrdinalIgnoreCase) && y.StartsWith("_", StringComparison.OrdinalIgnoreCase) ? -1 : 0;
    }
  }
}
