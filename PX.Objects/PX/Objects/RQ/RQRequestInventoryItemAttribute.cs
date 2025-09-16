// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequestInventoryItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.BQLConstants;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.RQ;

/// <summary>
/// Selector.<br />
/// Show inventory items available for using in request.<br />
/// Hide Inventory Items witch restricted for current logged in user, <br />
/// With status inactive, marked for deletion, no purchases and no request,
/// Restricted for request class.<br />
/// </summary>
[PXDBInt]
[PXUIField]
[PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noPurchases>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noRequest>>>), "Item cannot be purchased", new Type[] {})]
[PXRestrictor(typeof (Where<RQRequestClass.restrictItemList, Equal<BitOff>, Or<RQRequestClassItem.inventoryID, IsNotNull, Or<RQRequestClass.reqClassID, IsNull>>>), "Item is not in list of Request Class {0} items.", new Type[] {typeof (RQRequestClass.reqClassID)})]
/// <summary>Constructor.</summary>
/// <param name="classID">Request class field.</param>
public class RQRequestInventoryItemAttribute(Type classID) : CrossItemAttribute(BqlCommand.Compose(new Type[11]
{
  typeof (Search2<,,>),
  typeof (PX.Objects.IN.InventoryItem.inventoryID),
  typeof (LeftJoin<,,>),
  typeof (RQRequestClass),
  typeof (On<,>),
  typeof (RQRequestClass.reqClassID),
  typeof (Equal<>),
  typeof (Current<>),
  classID,
  typeof (LeftJoin<RQRequestClassItem, On2<RQRequestClassItem.FK.InventoryItem, And<RQRequestClassItem.reqClassID, Equal<RQRequestClass.reqClassID>, And<RQRequestClass.restrictItemList, Equal<BitOn>>>>>),
  typeof (Where<Match<Current<AccessInfo.userName>>>)
}), typeof (PX.Objects.IN.InventoryItem.inventoryCD), typeof (PX.Objects.IN.InventoryItem.descr), INPrimaryAlternateType.VPN)
{
}
