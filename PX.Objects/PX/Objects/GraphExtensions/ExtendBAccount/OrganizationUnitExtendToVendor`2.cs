// Decompiled with JetBrains decompiler
// Type: PX.Objects.GraphExtensions.ExtendBAccount.OrganizationUnitExtendToVendor`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.GraphExtensions.ExtendBAccount;

public abstract class OrganizationUnitExtendToVendor<TGraph, TPrimary> : 
  ExtendToVendorGraph<TGraph, TPrimary>
  where TGraph : PXGraph, IActionsMenuGraph
  where TPrimary : class, IBqlTable, new()
{
  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.Base.ActionsMenuItem.AddMenuAction((PXAction) this.viewVendor, "ChangeID", false);
    this.Base.ActionsMenuItem.AddMenuAction((PXAction) this.extendToVendor, "ChangeID", false);
  }
}
