// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDelegationViewCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

internal class PXDelegationViewCollection : PXViewCollection
{
  public PXDelegationViewCollection(PXGraph parent)
    : base(parent)
  {
  }

  public PXDelegationViewCollection(PXGraph parent, int capacity)
    : base(parent, capacity)
  {
  }

  public PXGraph DelegateTo { get; set; }

  public override PXView this[string key]
  {
    get
    {
      if (!this.DelegateContainsKey(key))
        return base[key];
      PXView view = this.DelegateTo.Views[key];
      if (this.ContainsKey(key))
      {
        PXView pxView = base[key];
        if (pxView.GetItemType() != view.GetItemType())
          return pxView;
      }
      return view;
    }
    set => base[key] = value;
  }

  protected bool DelegateContainsKey(string viewName)
  {
    if (this.DelegateTo == null)
      return false;
    if (this.DelegateTo.Views.ContainsKey(viewName))
      return true;
    return this.DelegateTo.Views is PXDelegationViewCollection views && views.DelegateContainsKey(viewName);
  }
}
