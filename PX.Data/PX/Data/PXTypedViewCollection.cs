// Decompiled with JetBrains decompiler
// Type: PX.Data.PXTypedViewCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXTypedViewCollection : Dictionary<System.Type, PXView>
{
  internal Dictionary<System.Type, PXView> _ReadOnly = new Dictionary<System.Type, PXView>();
  private PXGraph _Graph;
  /// <summary>Contains entries not stored between callbacks</summary>
  internal List<PXViewQueryCollection> _NonstandardViews = new List<PXViewQueryCollection>();

  public PXTypedViewCollection(PXGraph graph) => this._Graph = graph;

  public new virtual PXView this[System.Type key]
  {
    get => this.ContainsKey(key) ? base[key] : this._ReadOnly[key];
    set
    {
      if (!value.IsReadOnly)
        base[key] = value;
      else
        this._ReadOnly[key] = value;
    }
  }

  public virtual PXView this[BqlCommand command]
  {
    get => this[command.GetSelectType()];
    set => this[command.GetSelectType()] = value;
  }

  public new virtual void Add(System.Type key, PXView value) => base.Add(key, value);

  public new bool TryGetValue(System.Type key, out PXView value)
  {
    return base.TryGetValue(key, out value) || this._ReadOnly.TryGetValue(key, out value);
  }

  public PXView GetView(BqlCommand command, bool readOnly)
  {
    readOnly |= typeof (IBqlAggregate).IsAssignableFrom(command.GetSelectType());
    PXView view;
    if (!readOnly)
    {
      if (!base.TryGetValue(command.GetSelectType(), out view))
        base[command.GetSelectType()] = view = this.InitializeView(command, readOnly);
    }
    else if (!this._ReadOnly.TryGetValue(command.GetSelectType(), out view))
      this._ReadOnly[command.GetSelectType()] = view = this.InitializeView(command, readOnly);
    return view;
  }

  protected virtual PXView InitializeView(BqlCommand command, bool readOnly)
  {
    return new PXView(this._Graph, readOnly, command);
  }

  public Dictionary<System.Type, PXView>.ValueCollection ReadOnlyValues => this._ReadOnly.Values;

  public new void Clear()
  {
    base.Clear();
    this._ReadOnly.Clear();
  }
}
