// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.FS;

[PXDBInt]
[PXUIField]
public class ServiceAttribute(Type whereType, Type[] headers = null) : FSInventoryAttribute(BqlCommand.Compose(new Type[11]
{
  typeof (Search2<,,>),
  typeof (PX.Objects.IN.InventoryItem.inventoryID),
  typeof (InnerJoin<,>),
  typeof (INItemClass),
  typeof (On<INItemClass.itemClassID, Equal<PX.Objects.IN.InventoryItem.itemClassID>>),
  typeof (Where2<,>),
  typeof (Match<Current<AccessInfo.userName>>),
  typeof (And2<,>),
  typeof (Where<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.serviceItem>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noSales>>>>>),
  typeof (And<>),
  whereType
}), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr), headers ?? ServiceAttribute.defaultHeaders)
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

  public ServiceAttribute(Type[] headers = null)
    : this(typeof (Where<True, Equal<True>>), headers)
  {
  }
}
