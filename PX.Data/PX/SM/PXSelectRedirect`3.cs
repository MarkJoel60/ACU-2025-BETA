// Decompiled with JetBrains decompiler
// Type: PX.SM.PXSelectRedirect`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.SM;

public class PXSelectRedirect<Table, Where, PView> : PXSelect<Table, Where>
  where Table : class, IBqlTable, new()
  where Where : class, IBqlWhere, new()
  where PView : class, IBqlTable, new()
{
  public virtual PXRedirectHelper.WindowMode NewItemWindowMode
  {
    get => PXRedirectHelper.WindowMode.NewWindow;
  }

  public virtual PXRedirectHelper.WindowMode ViewItemWindowMode
  {
    get => PXRedirectHelper.WindowMode.NewWindow;
  }

  public PXSelectRedirect(PXGraph graph)
    : base(graph)
  {
    this.AddActions(graph);
  }

  public PXSelectRedirect(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
    this.AddActions(graph);
  }

  private void AddActions(PXGraph graph)
  {
    string name = typeof (Table).Name;
    this.AddAction(graph, name + "_View", new PXButtonDelegate(this.ViewItem));
    this.AddAction(graph, name + "_New", new PXButtonDelegate(this.NewItem));
    this.AddAction(graph, name + "_Delete", new PXButtonDelegate(this.DeleteItem));
  }

  [PXButton]
  [PXUIField(DisplayName = "New Line")]
  public virtual IEnumerable NewItem(PXAdapter adapter)
  {
    PXGraph primaryGraph = this.GetPrimaryGraph(default (Table));
    if (primaryGraph != null)
    {
      PXCache cach = primaryGraph.Caches[typeof (Table)];
      if (cach.AllowInsert)
      {
        object obj = this.Cache.Insert();
        if (obj != null)
        {
          this.Cache.SetStatus(obj, PXEntryStatus.Notchanged);
          this.Cache.IsDirty = false;
          if (cach.Insert(obj) != null)
            PXRedirectHelper.TryRedirect(cach.Graph, (object) this.Current, this.NewItemWindowMode);
        }
      }
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "View", Visible = false)]
  public virtual IEnumerable ViewItem(PXAdapter adapter)
  {
    if ((object) this.Current == null)
      return adapter.Get();
    PXRedirectHelper.TryRedirect(this.Cache.Graph, (object) this.Current, this.ViewItemWindowMode);
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Delete")]
  public virtual IEnumerable DeleteItem(PXAdapter adapter)
  {
    if ((object) this.Current == null)
      return adapter.Get();
    PXGraph primaryGraph = this.GetPrimaryGraph(this.Current);
    if (primaryGraph != null)
    {
      primaryGraph.TimeStamp = this.View.Graph.TimeStamp;
      PXCache cach = primaryGraph.Caches[typeof (Table)];
      cach.Current = (object) this.Current;
      if (cach.AllowDelete && this.View.Ask(typeof (Table).Name, "The current record will be deleted.", MessageButtons.OKCancel) == WebDialogResult.OK && cach.Delete((object) this.Current) != null)
      {
        primaryGraph.Actions.PressSave();
        this.View.RequestRefresh();
      }
    }
    return adapter.Get();
  }

  internal void AddAction(PXGraph graph, string name, PXButtonDelegate handler)
  {
    graph.Actions[name] = (PXAction) Activator.CreateInstance(typeof (PXNamedAction<>).MakeGenericType(typeof (PView)), (object) graph, (object) name, (object) handler);
  }

  public PXGraph GetPrimaryGraph(Table row)
  {
    return new EntityHelper(this.View.Graph).GetPrimaryGraph(typeof (Table), (object) row, true);
  }
}
