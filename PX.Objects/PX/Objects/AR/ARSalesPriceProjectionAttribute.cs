// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSalesPriceProjectionAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.AR;

/// <summary>
/// This class contains <see cref="T:PX.Objects.AR.ARSalesPrice" /> projection definition.
/// For <see cref="T:PX.Objects.AR.ARSalesPriceMaint" /> graph, the projection contains joined <see cref="T:PX.Objects.IN.InventoryItem" />, <see cref="T:PX.Objects.AR.ARPriceClass" /> and <see cref="T:PX.Objects.AR.Customer" /> entities.
/// </summary>
public class ARSalesPriceProjectionAttribute : PXProjectionAttribute
{
  public ARSalesPriceProjectionAttribute()
    : base(typeof (Select<ARSalesPrice>), new Type[1]
    {
      typeof (ARSalesPrice)
    })
  {
  }

  protected virtual Type GetSelect(PXCache sender)
  {
    return sender.Graph is ARSalesPriceMaint ? typeof (Select2<ARSalesPrice, InnerJoin<PX.Objects.IN.InventoryItem, On<ARSalesPrice.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>, LeftJoin<ARPriceClass, On<ARSalesPrice.priceType, Equal<PriceTypes.customerPriceClass>, And<ARSalesPrice.custPriceClassID, Equal<ARPriceClass.priceClassID>>>, LeftJoin<Customer, On<ARSalesPrice.priceType, Equal<PriceTypes.customer>, And<ARSalesPrice.customerID, Equal<Customer.bAccountID>>>>>>>) : base.GetSelect(sender);
  }
}
