// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.OrganizationMaintExt.QuantityDecimalPlacesCheckExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.OrganizationMaintExt;

public class QuantityDecimalPlacesCheckExt : PXGraphExtension<OrganizationMaint>
{
  protected virtual void _(Events.FieldVerifying<CommonSetup.decPlQty> e)
  {
    QuantityDecimalPlacesCheckExt.CommonSetupInBalanceCheckExt extension;
    QuantityDecimalPlacesCheckExt.CommonSetupInBalanceCheckExt inBalanceCheckExt1 = extension = PXCacheEx.GetExtension<QuantityDecimalPlacesCheckExt.CommonSetupInBalanceCheckExt>((IBqlTable) e.Row);
    bool? inventoryBalancesExist = inBalanceCheckExt1.InventoryBalancesExist;
    inventoryBalancesExist.GetValueOrDefault();
    if (!inventoryBalancesExist.HasValue)
    {
      bool flag = ((IQueryable<PXResult<INLocationStatusByCostCenter>>) PXSelectBase<INLocationStatusByCostCenter, PXViewOf<INLocationStatusByCostCenter>.BasedOn<SelectFromBase<INLocationStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INLocationStatusByCostCenter.qtyOnHand, IBqlDecimal>.IsGreater<Zero>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).Any<PXResult<INLocationStatusByCostCenter>>();
      inBalanceCheckExt1.InventoryBalancesExist = new bool?(flag);
    }
    QuantityDecimalPlacesCheckExt.CommonSetupInBalanceCheckExt inBalanceCheckExt2 = extension;
    if (inBalanceCheckExt2.InitialDecPlQty.HasValue)
      return;
    short? oldValue;
    inBalanceCheckExt2.InitialDecPlQty = oldValue = (short?) e.OldValue;
  }

  protected virtual void _(Events.RowSelected<CommonSetup> e)
  {
    CommonSetup row = e.Row;
    if (row == null)
      return;
    QuantityDecimalPlacesCheckExt.CommonSetupInBalanceCheckExt extension = PXCacheEx.GetExtension<QuantityDecimalPlacesCheckExt.CommonSetupInBalanceCheckExt>((IBqlTable) row);
    int num;
    if (extension.InventoryBalancesExist.GetValueOrDefault())
    {
      short? initialDecPlQty = extension.InitialDecPlQty;
      int? nullable1 = initialDecPlQty.HasValue ? new int?((int) initialDecPlQty.GetValueOrDefault()) : new int?();
      short? decPlQty = row.DecPlQty;
      int? nullable2 = decPlQty.HasValue ? new int?((int) decPlQty.GetValueOrDefault()) : new int?();
      num = !(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) ? 1 : 0;
    }
    else
      num = 0;
    if (num == 0)
      return;
    ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CommonSetup>>) e).Cache.RaiseExceptionHandling<CommonSetup.decPlQty>((object) row, (object) row.DecPlQty, (Exception) new PXSetPropertyException("Some stock items in the system have a nonzero quantity on hand. Changing the decimal places of quantity may lead to unexpected consequences in the processing of inventory transactions with these items.", (PXErrorLevel) 3, new object[2]
    {
      (object) extension.InitialDecPlQty,
      (object) e.Row.DecPlQty
    }));
  }

  /// <summary>
  /// An extension for <see cref="T:PX.Objects.CS.CommonSetup" /> that is used to prevent
  /// <see cref="T:PX.Objects.CS.CommonSetup.decPlQty" /> change when inventory balances exist.
  /// </summary>
  public sealed class CommonSetupInBalanceCheckExt : PXCacheExtension<CommonSetup>
  {
    /// <summary>
    /// Indicates that inventory balances exist if True, or not exist if False
    /// </summary>
    internal bool? InventoryBalancesExist { get; set; }

    /// <summary>
    /// The buffer keeping the before-edit value of <see cref="T:PX.Objects.CS.CommonSetup.decPlQty" /> field.
    /// </summary>
    internal short? InitialDecPlQty { get; set; }
  }
}
