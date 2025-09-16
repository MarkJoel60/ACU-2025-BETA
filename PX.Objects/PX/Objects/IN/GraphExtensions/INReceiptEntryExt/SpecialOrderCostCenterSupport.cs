// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INReceiptEntryExt.SpecialOrderCostCenterSupport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.GraphExtensions.INRegisterEntryBaseExt;
using PX.Objects.PM;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INReceiptEntryExt;

public class SpecialOrderCostCenterSupport : SpecialOrderCostCenterSupport<INReceiptEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.specialOrders>();

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (SpecialOrderCostCenterSelectorAttribute), "AllowEnabled", false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<INTran.specialOrderCostCenterID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<INRegister> e)
  {
    if (e.Row == null)
      return;
    bool inModule = e.Row.OrigModule == "IN";
    bool transfer = e.Row.TransferNbr != null;
    PXCacheEx.AdjustUI(((PXSelectBase) this.Base.transactions).Cache, (object) null).For<INTran.specialOrderCostCenterID>((Action<PXUIFieldAttribute>) (a => a.Visible = !inModule | transfer));
  }

  protected override void _(PX.Data.Events.RowSelected<INTran> e)
  {
    base._(e);
    if (e.Row == null)
      return;
    INRegister current = ((PXSelectBase<INRegister>) this.Base.CurrentDocument).Current;
    bool inModule = current?.OrigModule == "IN";
    bool released = current != null && current.Released.GetValueOrDefault();
    PXCacheEx.Adjust<InventorySourceType.ListAttribute>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row).For<INTran.inventorySource>((Action<InventorySourceType.ListAttribute>) (a => a.AllowSpecialOrders = !inModule));
    int? numberOfValues = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache.GetAttributes<INTran.inventorySource>((object) e.Row).OfType<InventorySourceType.ListAttribute>().FirstOrDefault<InventorySourceType.ListAttribute>()?.SetValues(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row);
    PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INTran>>) e).Cache, (object) e.Row).For<INTran.inventorySource>((Action<PXUIFieldAttribute>) (a =>
    {
      PXUIFieldAttribute pxuiFieldAttribute = a;
      int? nullable = numberOfValues;
      int num1 = 1;
      int num2 = (!(nullable.GetValueOrDefault() > num1 & nullable.HasValue) ? 0 : (!released ? 1 : 0)) & (inModule ? 1 : 0);
      pxuiFieldAttribute.Enabled = num2 != 0;
    }));
  }
}
