// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractInventoryItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CT;

[PXDBInt]
[PXUIField(DisplayName = "Contract Item")]
[PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.stkItem, NotEqual<True>>), "The {0} stock item cannot be used on the Contract Items (CT201000) form.", new Type[] {typeof (PX.Objects.IN.InventoryItem.inventoryCD)})]
[PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>>), "The {0} item cannot be used on the Contract Items (CT201000) form.", new Type[] {typeof (PX.Objects.IN.InventoryItem.inventoryCD)})]
[PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.isTemplate, Equal<False>>), "The inventory item is a template item.", new Type[] {})]
public class ContractInventoryItemAttribute : PXEntityAttribute
{
  public const string DimensionName = "INVENTORY";

  public ContractInventoryItemAttribute()
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("INVENTORY", BqlCommand.Compose(new Type[4]
    {
      typeof (Search5<,,>),
      typeof (PX.Objects.IN.InventoryItem.inventoryID),
      typeof (LeftJoin<ARSalesPrice, On<ARSalesPrice.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<ARSalesPrice.uOM, Equal<PX.Objects.IN.InventoryItem.baseUnit>, And<ARSalesPrice.siteID, IsNull, And<ARSalesPrice.curyID, Equal<Current<ContractItem.curyID>>, And<ARSalesPrice.priceType, Equal<PriceTypes.basePrice>, And<ARSalesPrice.breakQty, Equal<decimal0>, And<ARSalesPrice.isPromotionalPrice, Equal<False>, And<ARSalesPrice.isFairValue, Equal<False>>>>>>>>>>),
      typeof (Aggregate<GroupBy<PX.Objects.IN.InventoryItem.inventoryID, GroupBy<PX.Objects.IN.InventoryItem.stkItem>>>)
    }), typeof (PX.Objects.IN.InventoryItem.inventoryCD))
    {
      DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr)
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }
}
