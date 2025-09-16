// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.SubItemStatusVeryfierAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN;

public class SubItemStatusVeryfierAttribute : PXEventSubscriberAttribute
{
  protected readonly Type inventoryID;
  protected readonly Type siteID;
  protected readonly string[] statusrestricted;

  public SubItemStatusVeryfierAttribute(
    Type inventoryID,
    Type siteID,
    params string[] statusrestricted)
  {
    this.inventoryID = inventoryID;
    this.siteID = siteID;
    this.statusrestricted = statusrestricted;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldVerifyingEvents fieldVerifying1 = sender.Graph.FieldVerifying;
    Type itemType1 = sender.GetItemType();
    string fieldName = this._FieldName;
    SubItemStatusVeryfierAttribute veryfierAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying1 = new PXFieldVerifying((object) veryfierAttribute1, __vmethodptr(veryfierAttribute1, OnSubItemFieldVerifying));
    fieldVerifying1.AddHandler(itemType1, fieldName, pxFieldVerifying1);
    PXGraph.FieldVerifyingEvents fieldVerifying2 = sender.Graph.FieldVerifying;
    Type itemType2 = sender.GetItemType();
    string name = this.siteID.Name;
    SubItemStatusVeryfierAttribute veryfierAttribute2 = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying2 = new PXFieldVerifying((object) veryfierAttribute2, __vmethodptr(veryfierAttribute2, OnSiteFieldVerifying));
    fieldVerifying2.AddHandler(itemType2, name, pxFieldVerifying2);
  }

  protected virtual void OnSubItemFieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!this.Validate(sender, (int?) sender.GetValue(e.Row, this.inventoryID.Name), (int?) e.NewValue, (int?) sender.GetValue(e.Row, this.siteID.Name)))
      throw new PXSetPropertyException("Subitem status restricts using it for selected site.");
  }

  protected virtual void OnSiteFieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!this.Validate(sender, (int?) sender.GetValue(e.Row, this.inventoryID.Name), (int?) sender.GetValue(e.Row, this._FieldName), (int?) e.NewValue))
      throw new PXSetPropertyException("Subitem status restricts using it for selected site.");
  }

  private bool Validate(PXCache sender, int? invetroyID, int? subitemID, int? siteID)
  {
    INItemSiteReplenishment siteReplenishment = PXResultset<INItemSiteReplenishment>.op_Implicit(PXSelectBase<INItemSiteReplenishment, PXSelect<INItemSiteReplenishment, Where<INItemSiteReplenishment.inventoryID, Equal<Required<INItemSiteReplenishment.inventoryID>>, And<INItemSiteReplenishment.subItemID, Equal<Required<INItemSiteReplenishment.subItemID>>, And<INItemSiteReplenishment.siteID, Equal<Required<INItemSiteReplenishment.siteID>>>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, new object[3]
    {
      (object) invetroyID,
      (object) subitemID,
      (object) siteID
    }));
    if (siteReplenishment != null)
    {
      foreach (string str in this.statusrestricted)
      {
        if (str == siteReplenishment.ItemStatus)
          return false;
      }
    }
    return true;
  }
}
