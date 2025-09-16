// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSetup`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
[Obsolete("Use PXSetup<Table>.Where<TCondition> instead")]
public class PXSetup<Table, Where> : PXSelectReadonly<Table, Where>, IPXNonUpdateable
  where Table : class, IBqlTable, new()
  where Where : IBqlWhere, new()
{
  protected List<IBqlParameter> _bqlparams = new List<IBqlParameter>();
  protected Table _Record;
  protected List<object> _Pars = new List<object>();
  private bool _raiseError = true;

  public bool IsChanged
  {
    get
    {
      PXSetup<Table, Where>.Watcher slot = PXDatabase.GetSlot<PXSetup<Table, Where>.Watcher>(typeof (PXSetup<Table, Where>.Watcher).FullName, typeof (Table));
      if (slot == null)
        return false;
      bool isChanged = slot.IsChanged;
      slot.IsChanged = false;
      return isChanged;
    }
  }

  public PXSetup(PXGraph graph)
    : base(graph)
  {
    this.View.CacheType = typeof (Table);
    if (!graph.Caches.CanInitLazyCache())
    {
      this.View._Cache = graph.Caches[typeof (Table)];
    }
    else
    {
      this.View.CacheType = typeof (Table);
      graph.Caches.ProcessCacheMapping(graph, typeof (Table));
    }
    graph.OnClear += (PXGraphClearDelegate) ((graph_, option) => this.Clear());
    if (this.IsChanged)
      this.View.Cache._Current = (object) null;
    graph.Defaults[typeof (Table)] = new PXGraph.GetDefaultDelegate(this.getRecord);
    IBqlParameter[] parameters = this.View.BqlSelect.GetParameters();
    for (int index = 0; index < parameters.Length; ++index)
    {
      if (parameters[index].HasDefault)
      {
        System.Type rt = parameters[index].GetReferencedType();
        if (typeof (IBqlField).IsAssignableFrom(rt) && rt.IsNested)
        {
          if (this._bqlparams.Count == 0)
          {
            graph.RowSelected.AddHandler(BqlCommand.GetItemType(rt), new PXRowSelected(this.Top_RowSelected));
            graph.RowSelected.AddHandler(this.GetItemType(), new PXRowSelected(this.Self_RowSelected));
          }
          if (parameters[index].IsVisible)
          {
            if (graph.Caches[BqlCommand.GetItemType(rt)].BqlKeys.Contains(rt))
            {
              graph.FieldUpdated.AddHandler(BqlCommand.GetItemType(rt), rt.Name.ToLower(), new PXFieldUpdated(this.Top_KeyUpdated));
            }
            else
            {
              graph.FieldUpdated.AddHandler(BqlCommand.GetItemType(rt), rt.Name.ToLower(), new PXFieldUpdated(this.Top_KeyUpdated_Later));
              graph.FieldDefaulting.AddHandler(BqlCommand.GetItemType(rt), rt.Name.ToLower(), (PXFieldDefaulting) ((sender, e) => this.Top_KeyDefaulting_Later(sender, e, rt)));
            }
          }
          this._bqlparams.Add(parameters[index]);
        }
      }
    }
  }

  public void RaiseFieldUpdated(PXCache sender, object data)
  {
    this.Top_KeyUpdated_Later(sender, new PXFieldUpdatedEventArgs(data, (object) null, false));
  }

  public void Top_KeyDefaulting_Later(PXCache sender, PXFieldDefaultingEventArgs e, System.Type field)
  {
    int index = 0;
    foreach (IBqlParameter bqlparam in this._bqlparams)
    {
      if (e.Row == null && bqlparam.IsVisible && bqlparam.GetReferencedType() == field && this._Pars.Count > index)
      {
        object par = this._Pars[index];
        sender.RaiseFieldUpdating(field.Name, (object) null, ref par);
        e.NewValue = par;
        e.Cancel = true;
        break;
      }
      ++index;
    }
  }

  public void Top_KeyUpdated_Later(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.Current = default (Table);
    this._Record = default (Table);
    this.Clear();
    foreach (IBqlParameter bqlparam in this._bqlparams)
    {
      if (bqlparam.IsVisible)
      {
        System.Type referencedType = bqlparam.GetReferencedType();
        object valueExt = sender.GetValueExt(e.Row, referencedType.Name.ToLower());
        this._Pars.Add(valueExt is PXFieldState ? ((PXFieldState) valueExt).Value : valueExt);
      }
    }
  }

  public void Top_KeyUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.Clear();
    foreach (IBqlParameter bqlparam in this._bqlparams)
    {
      if (bqlparam.IsVisible)
      {
        System.Type referencedType = bqlparam.GetReferencedType();
        object valueExt = sender.GetValueExt(e.Row, referencedType.Name.ToLower());
        this._Pars.Add(valueExt is PXFieldState ? ((PXFieldState) valueExt).Value : valueExt);
      }
    }
    this.Current = (Table) this.Select(this._Pars.ToArray());
  }

  public void Top_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (this.Cache._Current == null)
    {
      this._Record = default (Table);
    }
    else
    {
      object obj = (object) null;
      bool? result = new bool?();
      List<object> pars = new List<object>(this._bqlparams.Count);
      foreach (IBqlParameter bqlparam in this._bqlparams)
      {
        System.Type referencedType = bqlparam.GetReferencedType();
        PXCache cach = sender.Graph.Caches[BqlCommand.GetItemType(referencedType)];
        pars.Add(cach.GetValue(cach.Current, referencedType.Name.ToLower()));
      }
      ((IBqlUnary) Activator.CreateInstance(typeof (Where))).Verify(this.Cache, this.Cache.Current, pars, ref result, ref obj);
      bool? nullable = result;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        this.Cache.Current = (object) null;
        this._Record = default (Table);
      }
    }
    this.Clear();
  }

  public void Self_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (this.Cache._Current != null)
      return;
    this._Record = default (Table);
  }

  public bool RaiseError
  {
    get => this._raiseError;
    set => this._raiseError = value;
  }

  private object getRecord()
  {
    if ((object) this._Record == null)
    {
      this._Record = (Table) this.Select(this._Pars.ToArray());
      if ((object) this._Record == null && this._bqlparams.Count > 0)
      {
        bool flag = true;
        System.Type field1 = (System.Type) null;
        foreach (IBqlParameter bqlparam in this._bqlparams)
        {
          if (bqlparam.IsVisible)
            flag = false;
          if (bqlparam.HasDefault && !bqlparam.IsVisible)
          {
            System.Type referencedType = bqlparam.GetReferencedType();
            field1 = referencedType;
            if (flag)
            {
              PXCache cach = this._Graph.Caches[BqlCommand.GetItemType(referencedType)];
              if (cach.GetValue(cach.Current, referencedType.Name) == null)
                flag = false;
            }
          }
        }
        if (flag && field1 != (System.Type) null)
        {
          PXCache cach = this._Graph.Caches[BqlCommand.GetItemType(field1)];
          if (this.RaiseError)
          {
            object valueExt = cach.GetValueExt(cach.Current, field1.Name);
            cach.RaiseExceptionHandling(field1.Name, cach.Current, (object) valueExt.ToString(), (Exception) new PXSetPropertyException("'{0}' cannot be found in the system.", new object[1]
            {
              (object) typeof (Table).Name
            }));
          }
          this._Record = default (Table);
          Table record = new Table();
          foreach (string field2 in (List<string>) this.Cache.Fields)
          {
            object newValue = (object) null;
            this.Cache.RaiseFieldDefaulting(field2, (object) record, out newValue);
            if (newValue != null)
              this.Cache.SetValue((object) record, field2, newValue);
          }
          return (object) record;
        }
      }
    }
    return (object) this._Record;
  }

  public virtual void Clear()
  {
    this._Pars.Clear();
    this._Record = default (Table);
  }

  /// <exclude />
  public class Watcher : IPrefetchable, IPXCompanyDependent
  {
    public bool IsChanged;

    void IPrefetchable.Prefetch() => this.IsChanged = true;
  }
}
