// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOLocationAvailAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.SO;

/// <summary>
/// Extends <see cref="T:PX.Objects.IN.LocationAvailAttribute" /> and shows the availability of Inventory Item for the given location.
/// </summary>
/// <example>
/// [SOLocationAvail(typeof(SOLine.inventoryID), typeof(SOLine.subItemID), typeof(SOLine.siteID), typeof(SOLine.tranType), typeof(SOLine.invtMult))]
/// </example>
public class SOLocationAvailAttribute : LocationAvailAttribute
{
  public SOLocationAvailAttribute(
    Type InventoryType,
    Type SubItemType,
    Type CostCenterID,
    Type SiteIDType,
    Type TranType,
    Type InvtMultType)
    : base(InventoryType, SubItemType, CostCenterID, SiteIDType, (Type) null, (Type) null, (Type) null)
  {
    this._IsSalesType = BqlCommand.Compose(new Type[9]
    {
      typeof (Where<,,>),
      TranType,
      typeof (Equal<INTranType.issue>),
      typeof (Or<,,>),
      TranType,
      typeof (Equal<INTranType.invoice>),
      typeof (Or<,>),
      TranType,
      typeof (Equal<INTranType.debitMemo>)
    });
    this._IsReceiptType = BqlCommand.Compose(new Type[9]
    {
      typeof (Where<,,>),
      TranType,
      typeof (Equal<INTranType.receipt>),
      typeof (Or<,,>),
      TranType,
      typeof (Equal<INTranType.return_>),
      typeof (Or<,>),
      TranType,
      typeof (Equal<INTranType.creditMemo>)
    });
    this._IsTransferType = BqlCommand.Compose(new Type[12]
    {
      typeof (Where<,,>),
      TranType,
      typeof (Equal<INTranType.transfer>),
      typeof (And<,,>),
      InvtMultType,
      typeof (Equal<short1>),
      typeof (Or<,,>),
      TranType,
      typeof (Equal<INTranType.transfer>),
      typeof (And<,>),
      InvtMultType,
      typeof (Equal<shortMinus1>)
    });
    this._IsStandardCostAdjType = BqlCommand.Compose(new Type[6]
    {
      typeof (Where<,,>),
      TranType,
      typeof (Equal<INTranType.standardCostAdjustment>),
      typeof (Or<,>),
      TranType,
      typeof (Equal<INTranType.negativeCostAdjustment>)
    });
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PrimaryItemRestrictorAttribute(InventoryType, this._IsReceiptType, this._IsSalesType, this._IsTransferType));
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new LocationRestrictorAttribute(this._IsReceiptType, this._IsSalesType, this._IsTransferType));
  }

  public override void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    SOShipLine soShipLine = PXParentAttribute.SelectParent<SOShipLine>(sender, e.Row);
    if (soShipLine != null && soShipLine.IsUnassigned.GetValueOrDefault())
      return;
    base.FieldDefaulting(sender, e);
  }
}
