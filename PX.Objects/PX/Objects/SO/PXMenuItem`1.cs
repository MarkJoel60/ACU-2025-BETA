// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.PXMenuItem`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Reflection;

#nullable disable
namespace PX.Objects.SO;

[Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
public class PXMenuItem<TNode> : PXAction<TNode> where TNode : class, IBqlTable, new()
{
  public string Menu { get; private set; }

  public int MenuItemID { get; private set; }

  protected PXMenuItem(PXGraph graph)
    : base(graph)
  {
  }

  public PXMenuItem(PXGraph graph, string name)
    : base(graph, name)
  {
    PXMenuItem<TNode> pxMenuItem = this;
    // ISSUE: virtual method pointer
    this.SetProperties((Delegate) new PXButtonDelegate((object) pxMenuItem, __vmethodptr(pxMenuItem, Handler)));
  }

  public PXMenuItem(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
    this.SetProperties(this._Handler);
  }

  protected virtual void SetProperties(Delegate handler)
  {
    PXMenuItemAttribute customAttribute = handler.Method.GetCustomAttribute<PXMenuItemAttribute>(false);
    if (customAttribute == null)
      return;
    this.MenuItemID = customAttribute.MenuItemID;
    this.Menu = customAttribute.Menu;
  }
}
