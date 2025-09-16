// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.NonStockItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.IN;

[PXDBInt]
[PXUIField]
public class NonStockItemAttribute : InventoryAttribute
{
  public static Type Search
  {
    get => typeof (PX.Data.Search<InventoryItem.inventoryID, Where<Match<Current<AccessInfo.userName>>>>);
  }

  public static PXRestrictorAttribute CreateRestrictor()
  {
    return new PXRestrictorAttribute(typeof (Where<InventoryItem.stkItem, Equal<boolFalse>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<InventoryItem.isTemplate, Equal<False>>>>), "The inventory item is a stock item.", Array.Empty<Type>());
  }

  public static PXRestrictorAttribute CreateRestrictorDependingOnFeature<TFeature>() where TFeature : IBqlField
  {
    return new PXRestrictorAttribute(typeof (Where2<FeatureInstalled<TFeature>, Or<InventoryItem.stkItem, Equal<boolFalse>>>), "The inventory item is a stock item.", Array.Empty<Type>());
  }

  public NonStockItemAttribute()
    : base(NonStockItemAttribute.Search, typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr))
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) NonStockItemAttribute.CreateRestrictor());
  }
}
