// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.Relational.CRAddressActions`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System.Collections;

#nullable disable
namespace PX.Objects.CR.Extensions.Relational;

/// <exclude />
public abstract class CRAddressActions<TGraph, TMain> : 
  CRParentChild<TGraph, CRAddressActions<TGraph, TMain>>
  where TGraph : PXGraph
  where TMain : class, IBqlTable, new()
{
  public PXAction<TMain> ValidateAddress;
  public PXAction<TMain> ViewOnMap;

  protected virtual void _(
    Events.RowSelected<CRParentChild<TGraph, CRAddressActions<TGraph, TMain>>.Document> e)
  {
    CRParentChild<TGraph, CRAddressActions<TGraph, TMain>>.Document row = e.Row;
    if (row == null)
      return;
    Address address = this.GetChildByID(row.ChildID)?.Base as Address;
    ((PXAction) this.ValidateAddress).SetEnabled(address == null || !address.IsValidated.GetValueOrDefault());
  }

  [PXUIField(DisplayName = "Validate Address", FieldClass = "Validate Address")]
  [PXButton]
  protected virtual IEnumerable validateAddress(PXAdapter adapter)
  {
    CRParentChild<TGraph, CRAddressActions<TGraph, TMain>>.Document current = ((PXSelectBase<CRParentChild<TGraph, CRAddressActions<TGraph, TMain>>.Document>) this.PrimaryDocument).Current;
    if (current == null || !(((PXSelectBase) this.ChildDocument).Cache.GetMain<CRParentChild<TGraph, CRAddressActions<TGraph, TMain>>.Child>(this.GetChildByID(current.ChildID)) is Address main) || main.IsValidated.GetValueOrDefault())
      return adapter.Get();
    PXAddressValidator.Validate<Address>((PXGraph) this.Base, main, true, true);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  protected virtual IEnumerable viewOnMap(PXAdapter adapter)
  {
    CRParentChild<TGraph, CRAddressActions<TGraph, TMain>>.Document current = ((PXSelectBase<CRParentChild<TGraph, CRAddressActions<TGraph, TMain>>.Document>) this.PrimaryDocument).Current;
    if (current == null || !(((PXSelectBase) this.ChildDocument).Cache.GetMain<CRParentChild<TGraph, CRAddressActions<TGraph, TMain>>.Child>(this.GetChildByID(current.ChildID)) is Address main))
      return adapter.Get();
    BAccountUtility.ViewOnMap(main);
    return adapter.Get();
  }
}
