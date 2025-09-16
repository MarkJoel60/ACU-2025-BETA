// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.PXMenu`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;

#nullable disable
namespace PX.Objects.SO;

[Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
public class PXMenu<TNode> : PXAction<TNode> where TNode : class, IBqlTable, new()
{
  protected PXMenu(PXGraph graph)
    : base(graph)
  {
  }

  public PXMenu(PXGraph graph, string name)
    : base(graph, name)
  {
  }

  public PXMenu(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  public virtual IEnumerable Press(PXAdapter adapter, int? menuItemID, string actionName = null)
  {
    PXMenuItem<TNode> action;
    return menuItemID.HasValue && this.TryGetMenuItem(adapter.View.Graph, menuItemID.Value, out action) ? ((PXAction) action).Press(adapter) : adapter.Get();
  }

  public virtual bool TryGetMenuItem(PXGraph graph, int menuItemID, out PXMenuItem<TNode> action)
  {
    action = ((OrderedDictionary) graph.Actions).Values.OfType<PXMenuItem<TNode>>().FirstOrDefault<PXMenuItem<TNode>>((Func<PXMenuItem<TNode>, bool>) (x => x.MenuItemID == menuItemID && string.Equals(x.Menu, this._Name, StringComparison.OrdinalIgnoreCase)));
    return action != null;
  }
}
