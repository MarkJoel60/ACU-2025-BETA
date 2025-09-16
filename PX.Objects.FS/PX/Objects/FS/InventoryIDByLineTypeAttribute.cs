// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.InventoryIDByLineTypeAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.FS;

[PXDBInt]
[PXUIField]
[PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noSales>>>>), "The inventory item is {0}.", new Type[] {typeof (PX.Objects.IN.InventoryItem.itemStatus)})]
[PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>>), "Item reserved for Project Module to represent N/A item.", new Type[] {})]
public class InventoryIDByLineTypeAttribute(Type whereType, Type lineType, Type[] headers = null) : 
  FSInventoryAttribute(((IBqlTemplate) BqlTemplate.OfCommand<Search2<PX.Objects.IN.InventoryItem.inventoryID, LeftJoin<INItemClass, On<INItemClass.itemClassID, Equal<PX.Objects.IN.InventoryItem.itemClassID>>, LeftJoin<FSServiceInventoryItem, On<FSServiceInventoryItem.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<Current<InventoryIDByLineTypeAttribute.LineType>, Equal<ListField_LineType_Pickup_Delivery.Pickup_Delivery>>>>>, Where2<Where<Match<Current<AccessInfo.userName>>>, And2<Where2<Where<Current<InventoryIDByLineTypeAttribute.LineType>, IsNull>, Or<Where2<Where<Current<InventoryIDByLineTypeAttribute.LineType>, Equal<ListField_LineType_ALL.Inventory_Item>, And<PX.Objects.IN.InventoryItem.stkItem, Equal<True>>>, Or2<Where<Current<InventoryIDByLineTypeAttribute.LineType>, Equal<ListField_LineType_ALL.Service>, And<PX.Objects.IN.InventoryItem.stkItem, Equal<False>, And<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.serviceItem>>>>, Or2<Where<Current<InventoryIDByLineTypeAttribute.LineType>, Equal<ListField_LineType_ALL.NonStockItem>, And<PX.Objects.IN.InventoryItem.stkItem, Equal<False>, And<PX.Objects.IN.InventoryItem.itemType, NotEqual<INItemTypes.serviceItem>>>>, Or<Where<Current<InventoryIDByLineTypeAttribute.LineType>, Equal<ListField_LineType_Pickup_Delivery.Pickup_Delivery>, And<Current<FSAppointmentDet.pickupDeliveryServiceID>, Equal<FSServiceInventoryItem.serviceID>>>>>>>>>, And<InventoryIDByLineTypeAttribute.WhereType>>>>>.Replace<InventoryIDByLineTypeAttribute.LineType>(lineType).Replace<InventoryIDByLineTypeAttribute.WhereType>(whereType)).ToType(), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr), headers ?? InventoryIDByLineTypeAttribute.defaultHeaders)
{
  private static Type[] defaultHeaders = new Type[10]
  {
    typeof (PX.Objects.IN.InventoryItem.inventoryCD),
    typeof (PX.Objects.IN.InventoryItem.itemClassID),
    typeof (FSxServiceClass.mem_RouteService),
    typeof (PX.Objects.IN.InventoryItem.itemStatus),
    typeof (PX.Objects.IN.InventoryItem.descr),
    typeof (PX.Objects.IN.InventoryItem.itemType),
    typeof (PX.Objects.IN.InventoryItem.baseUnit),
    typeof (PX.Objects.IN.InventoryItem.salesUnit),
    typeof (PX.Objects.IN.InventoryItem.purchaseUnit),
    typeof (FSxService.actionType)
  };

  public InventoryIDByLineTypeAttribute(Type lineType, Type[] headers = null)
    : this(typeof (Where<True, Equal<True>>), lineType, headers)
  {
  }

  [PXHidden]
  public class LineType : BqlPlaceholderBase
  {
  }

  [PXHidden]
  public class WhereType : BqlPlaceholderBase
  {
  }
}
