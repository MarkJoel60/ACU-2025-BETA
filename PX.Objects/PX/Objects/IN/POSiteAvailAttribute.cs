// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.POSiteAvailAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

[PXDBInt]
[PXUIField]
public class POSiteAvailAttribute(Type InventoryType, Type SubItemType, Type costCenterType) : 
  SiteAvailAttribute(InventoryType, SubItemType, costCenterType)
{
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    Type itemType = sender.GetItemType();
    string name = this._inventoryType.Name;
    POSiteAvailAttribute siteAvailAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) siteAvailAttribute, __vmethodptr(siteAvailAttribute, InventoryID_FieldUpdated));
    fieldUpdated.RemoveHandler(itemType, name, pxFieldUpdated);
  }

  public override void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
  }
}
