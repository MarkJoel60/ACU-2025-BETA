// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.NonStockItemMaintRedirectExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions;
using PX.Objects.PO;
using System.Collections;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public class NonStockItemMaintRedirectExt : 
  PXGraphExtension<RedirectExtension<NonStockItemMaint>, NonStockItemMaint>
{
  public PXAction<POVendorInventory> viewVendorEmployee;
  public PXAction<INItemXRef> viewBAccount;

  private RedirectExtension<NonStockItemMaint> BaseRedirect { get; set; }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false, VisibleOnProcessingResults = false, PopupVisible = false)]
  public virtual IEnumerable ViewVendorEmployee(PXAdapter adapter)
  {
    this.BaseRedirect = ((PXGraph) ((PXGraphExtension<NonStockItemMaint>) this).Base).GetExtension<RedirectExtension<NonStockItemMaint>>();
    return this.BaseRedirect.ViewCustomerVendorEmployee<POVendorInventory.vendorID>(adapter);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false, VisibleOnProcessingResults = false, PopupVisible = false)]
  public virtual IEnumerable ViewBAccount(PXAdapter adapter)
  {
    this.BaseRedirect = ((PXGraph) ((PXGraphExtension<NonStockItemMaint>) this).Base).GetExtension<RedirectExtension<NonStockItemMaint>>();
    return this.BaseRedirect.ViewCustomerVendorEmployee<INItemXRef.bAccountID>(adapter);
  }
}
