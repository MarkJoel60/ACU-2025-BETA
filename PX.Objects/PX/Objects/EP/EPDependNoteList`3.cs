// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPDependNoteList`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.EP;

public class EPDependNoteList<Table, FRefNoteID, ParentTable> : PXSelect<Table>
  where Table : class, IBqlTable, new()
  where FRefNoteID : class, IBqlField
  where ParentTable : class, IBqlTable
{
  protected PXView _History;

  public EPDependNoteList(PXGraph graph)
    : base(graph)
  {
    PXDBDefaultAttribute.SetSourceType<FRefNoteID>(graph.Caches[typeof (Table)], this.SourceNoteID);
    ((PXSelectBase) this).View = new PXView(graph, false, BqlCommand.CreateInstance(new Type[1]
    {
      BqlCommand.Compose(new Type[3]
      {
        typeof (Select<,>),
        typeof (Table),
        this.ComposeWhere
      })
    }));
    this.Init(graph);
  }

  public EPDependNoteList(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
    this.Init(graph);
  }

  protected Type SourceNoteID
  {
    get => typeof (ParentTable).GetNestedType(EntityHelper.GetNoteField(typeof (ParentTable)));
  }

  protected Type RefNoteID => typeof (FRefNoteID);

  protected Type ComposeWhere
  {
    get
    {
      return BqlCommand.Compose(new Type[5]
      {
        typeof (Where<,>),
        this.RefNoteID,
        typeof (Equal<>),
        typeof (Current<>),
        this.SourceNoteID
      });
    }
  }

  protected virtual void Init(PXGraph graph)
  {
    PXGraph.RowInsertedEvents rowInserted = graph.RowInserted;
    Type itemType1 = BqlCommand.GetItemType(this.SourceNoteID);
    EPDependNoteList<Table, FRefNoteID, ParentTable> epDependNoteList1 = this;
    // ISSUE: virtual method pointer
    PXRowInserted pxRowInserted = new PXRowInserted((object) epDependNoteList1, __vmethodptr(epDependNoteList1, Source_RowInserted));
    rowInserted.AddHandler(itemType1, pxRowInserted);
    PXGraph.RowDeletedEvents rowDeleted = graph.RowDeleted;
    Type itemType2 = BqlCommand.GetItemType(this.SourceNoteID);
    EPDependNoteList<Table, FRefNoteID, ParentTable> epDependNoteList2 = this;
    // ISSUE: virtual method pointer
    PXRowDeleted pxRowDeleted = new PXRowDeleted((object) epDependNoteList2, __vmethodptr(epDependNoteList2, Source_RowDeleted));
    rowDeleted.AddHandler(itemType2, pxRowDeleted);
    if (!graph.Views.Caches.Contains(typeof (Note)))
      graph.Views.Caches.Add(typeof (Note));
    PXCache cach = graph.Caches[typeof (Table)];
    this._History = this.CreateView($"EPDependNoteList_{graph.GetType().FullName}_{typeof (Table).FullName}_{typeof (ParentTable).FullName}_{typeof (FRefNoteID).FullName}_History", graph, BqlCommand.Compose(new Type[7]
    {
      typeof (Select<,>),
      typeof (Table),
      typeof (Where<,>),
      this.RefNoteID,
      typeof (Equal<>),
      typeof (Required<>),
      this.SourceNoteID
    }));
  }

  protected virtual PXView CreateView(string viewName, PXGraph graph, Type command)
  {
    PXView view = new PXView(graph, false, BqlCommand.CreateInstance(new Type[1]
    {
      command
    }));
    graph.Views.Add(viewName, view);
    return view;
  }

  protected virtual void Source_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    Guid? nullable = (Guid?) sender.GetValue(e.Row, this.SourceNoteID.Name);
    foreach (Table able in this._History.SelectMulti(new object[1]
    {
      (object) nullable
    }))
      ((PXSelectBase) this).Cache.Delete((object) able);
  }

  protected virtual void Source_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    this.GetSourceNoteID(e.Row);
  }

  protected Guid? GetSourceNoteID(object source)
  {
    PXCache cach1 = ((PXSelectBase) this)._Graph.Caches[source.GetType()];
    PXCache cach2 = ((PXSelectBase) this)._Graph.Caches[typeof (Note)];
    bool isDirty = cach2.IsDirty;
    object obj = source;
    string name = this.SourceNoteID.Name;
    Guid? noteId = PXNoteAttribute.GetNoteID(cach1, obj, name);
    cach2.IsDirty = isDirty;
    return noteId;
  }

  protected Guid? GetRefNoteID(object source)
  {
    return (Guid?) ((PXSelectBase) this)._Graph.Caches[source.GetType()].GetValue(source, this.RefNoteID.Name);
  }
}
