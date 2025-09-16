// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.DropShipLinksExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common.DAC;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class DropShipLinksExt : PXGraphExtension<PurchaseSupplyBaseExt, SOOrderEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.dropShipments>();

  [PXCustomizeBaseAttribute(typeof (PXDBStringAttribute), "IsKey", true)]
  public virtual void _(PX.Data.Events.CacheAttached<DropShipLink.sOOrderType> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXDBStringAttribute), "IsKey", true)]
  public virtual void _(PX.Data.Events.CacheAttached<DropShipLink.sOOrderNbr> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXDBIntAttribute), "IsKey", true)]
  public virtual void _(PX.Data.Events.CacheAttached<DropShipLink.sOLineNbr> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXDBStringAttribute), "IsKey", false)]
  public virtual void _(PX.Data.Events.CacheAttached<DropShipLink.pOOrderType> e)
  {
  }

  [PXParent(typeof (DropShipLink.FK.SupplyPOOrder))]
  [PXFormula(null, typeof (CountCalc<PX.Objects.SO.SupplyPOOrder.dropShipLinkedLinesCount>))]
  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXDBStringAttribute), "IsKey", false)]
  public virtual void _(PX.Data.Events.CacheAttached<DropShipLink.pOOrderNbr> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXDBIntAttribute), "IsKey", false)]
  public virtual void _(PX.Data.Events.CacheAttached<DropShipLink.pOLineNbr> e)
  {
  }

  [PXUnboundFormula(typeof (Switch<Case<Where<DropShipLink.active, Equal<True>, And<DropShipLink.poCompleted, Equal<False>>>, int1>, int0>), typeof (AddCalc<PX.Objects.SO.SupplyPOOrder.dropShipActiveLinksCount>))]
  [PXMergeAttributes]
  public virtual void _(PX.Data.Events.CacheAttached<DropShipLink.active> e)
  {
  }

  public virtual void _(
    PX.Data.Events.FieldSelecting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOOrderStatus> e)
  {
    PX.Objects.SO.SupplyPOOrder supplyPoOrder = this.Base1.IsOriginalDSLinkVisible(e.Row) ? this.Base1.GetOriginalSupplyOrder(e.Row) : this.Base1.GetSupplyOrder(e.Row);
    if (e.Row != null && supplyPoOrder != null && e.Row.POOrderStatus != supplyPoOrder.Status)
      e.Row.POOrderStatus = supplyPoOrder.Status;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOOrderStatus>>) e).ReturnState = (object) supplyPoOrder?.Status;
  }

  public virtual void _(PX.Data.Events.RowSelecting<PX.Objects.SO.SOLine> e)
  {
    DropShipLink dropShipLink = this.Base1.IsOriginalDSLinkVisible(e.Row) ? this.Base1.GetOriginalDropShipLink(e.Row) : this.Base1.GetDropShipLink(e.Row);
    if (dropShipLink == null)
      return;
    e.Row.POOrderType = dropShipLink.POOrderType;
    e.Row.POOrderNbr = dropShipLink.POOrderNbr;
    e.Row.POLineNbr = dropShipLink.POLineNbr;
    e.Row.POLinkActive = dropShipLink.Active;
  }

  [Obsolete("Event handler is kept to avoid breaking changes.")]
  public virtual void _(PX.Data.Events.FieldSelecting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOOrderNbr> e)
  {
  }

  [Obsolete("Event handler is kept to avoid breaking changes.")]
  public virtual void _(PX.Data.Events.FieldSelecting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOLineNbr> e)
  {
  }

  [Obsolete("Event handler is kept to avoid breaking changes.")]
  public virtual void _(
    PX.Data.Events.FieldSelecting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOLinkActive> e)
  {
  }

  public virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOSource> e)
  {
    if (e.Row == null || e.Row.POSource != "D" || e.Row.IsLegacyDropShip.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOSource>>) e).Cache.SetValue<PX.Objects.SO.SOLine.pOSiteID>((object) e.Row, (object) e.Row.SiteID);
  }

  public virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.siteID> e)
  {
    if (e.Row == null || e.Row.POSource != "D" || e.Row.IsLegacyDropShip.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.siteID>>) e).Cache.SetValue<PX.Objects.SO.SOLine.pOSiteID>((object) e.Row, (object) e.Row.SiteID);
  }

  public virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOCreate> e)
  {
    if (e.Row == null || e.Row.POSource != "D")
      return;
    bool? nullable = e.Row.IsLegacyDropShip;
    if (nullable.GetValueOrDefault() || this.Base1.IsDropshipReturn(e.Row))
      return;
    DropShipLink dropShipLink = this.Base1.GetDropShipLink(e.Row);
    if (dropShipLink == null)
      return;
    nullable = dropShipLink.Active;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOCreate>, PX.Objects.SO.SOLine, object>) e).NewValue;
    if (nullable.GetValueOrDefault())
      return;
    nullable = e.Row.POCreate;
    if (nullable.GetValueOrDefault())
      throw new PXSetPropertyException("The line has an active link to a line of the {0} purchase order. To make changes, clear the PO Linked check box.", new object[1]
      {
        (object) dropShipLink.POOrderNbr
      });
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventoryID> e)
  {
    if (e.Row == null || e.Row.POSource != "D")
      return;
    bool? nullable = e.Row.IsLegacyDropShip;
    if (nullable.GetValueOrDefault())
      return;
    DropShipLink dropShipLink = this.Base1.GetDropShipLink(e.Row);
    if (dropShipLink != null)
    {
      nullable = dropShipLink.Active;
      if (nullable.GetValueOrDefault())
      {
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventoryID>, PX.Objects.SO.SOLine, object>) e).NewValue);
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventoryID>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) inventoryItem?.InventoryCD;
        throw new PXSetPropertyException((IBqlTable) e.Row, "The line has an active link to a line of the {0} purchase order. To make changes, clear the PO Linked check box.", new object[1]
        {
          (object) dropShipLink.POOrderNbr
        });
      }
    }
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventoryID>, PX.Objects.SO.SOLine, object>) e).NewValue == null)
      return;
    PX.Objects.IN.InventoryItem inventoryItem1 = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventoryID>, PX.Objects.SO.SOLine, object>) e).NewValue);
    if (inventoryItem1 == null)
      return;
    nullable = inventoryItem1.StkItem;
    if (!nullable.GetValueOrDefault())
    {
      nullable = inventoryItem1.NonStockReceipt;
      if (nullable.GetValueOrDefault())
      {
        nullable = inventoryItem1.NonStockShip;
        if (nullable.GetValueOrDefault())
          return;
      }
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.inventoryID>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) inventoryItem1?.InventoryCD;
      throw new PXSetPropertyException((IBqlTable) e.Row, "Only items for which both 'Require Receipt' and 'Require Shipment' are selected can be drop shipped.");
    }
  }

  public virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.siteID> e)
  {
    if (e.Row == null || e.Row.POSource != "D")
      return;
    bool? nullable = e.Row.IsLegacyDropShip;
    if (nullable.GetValueOrDefault() || object.Equals(e.OldValue, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.siteID>, PX.Objects.SO.SOLine, object>) e).NewValue))
      return;
    DropShipLink dropShipLink = this.Base1.GetDropShipLink(e.Row);
    if (dropShipLink == null)
      return;
    nullable = dropShipLink.Active;
    if (nullable.GetValueOrDefault())
    {
      PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.siteID>, PX.Objects.SO.SOLine, object>) e).NewValue);
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.siteID>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) inSite?.SiteCD;
      throw new PXSetPropertyException("The line has an active link to a line of the {0} purchase order. To make changes, clear the PO Linked check box.", new object[1]
      {
        (object) dropShipLink.POOrderNbr
      });
    }
  }

  public virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.orderQty> e)
  {
    if (e.Row == null || e.Row.POSource != "D")
      return;
    bool? nullable1 = e.Row.IsLegacyDropShip;
    if (nullable1.GetValueOrDefault())
      return;
    DropShipLink dropShipLink = this.Base1.GetDropShipLink(e.Row);
    if (dropShipLink == null)
      return;
    nullable1 = dropShipLink.Active;
    if (nullable1.GetValueOrDefault())
      throw new PXSetPropertyException("The line has an active link to a line of the {0} purchase order. To make changes, clear the PO Linked check box.", new object[1]
      {
        (object) dropShipLink.POOrderNbr
      });
    Decimal? baseReceivedQty1 = dropShipLink.BaseReceivedQty;
    Decimal num1 = 0M;
    if (!(baseReceivedQty1.GetValueOrDefault() > num1 & baseReceivedQty1.HasValue))
      return;
    Decimal? nullable2 = new Decimal?(0M);
    Decimal? nullable3 = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.orderQty>, PX.Objects.SO.SOLine, object>) e).NewValue;
    Decimal num2 = 0M;
    if (nullable3.GetValueOrDefault() > num2 & nullable3.HasValue)
      nullable2 = new Decimal?(INUnitAttribute.ConvertToBase<PX.Objects.SO.SOLine.inventoryID, PX.Objects.SO.SOLine.uOM>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.orderQty>>) e).Cache, (object) e.Row, (Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.orderQty>, PX.Objects.SO.SOLine, object>) e).NewValue, INPrecision.QUANTITY));
    nullable1 = dropShipLink.Active;
    if (nullable1.GetValueOrDefault())
      return;
    nullable3 = nullable2;
    Decimal? baseReceivedQty2 = dropShipLink.BaseReceivedQty;
    if (nullable3.GetValueOrDefault() < baseReceivedQty2.GetValueOrDefault() & nullable3.HasValue & baseReceivedQty2.HasValue)
    {
      PXCache cache = ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.orderQty>>) e).Cache;
      PX.Objects.SO.SOLine row = e.Row;
      baseReceivedQty2 = dropShipLink.BaseReceivedQty;
      Decimal num3 = baseReceivedQty2.Value;
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
      {
        (object) INUnitAttribute.ConvertFromBase<PX.Objects.SO.SOLine.inventoryID, PX.Objects.SO.SOLine.uOM>(cache, (object) row, num3, INPrecision.QUANTITY)
      });
    }
  }

  public virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.uOM> e)
  {
    if (e.Row == null || e.Row.POSource != "D")
      return;
    bool? nullable = e.Row.IsLegacyDropShip;
    if (nullable.GetValueOrDefault())
      return;
    DropShipLink dropShipLink = this.Base1.GetDropShipLink(e.Row);
    if (dropShipLink == null)
      return;
    nullable = dropShipLink.Active;
    if (nullable.GetValueOrDefault())
      throw new PXSetPropertyException("The line has an active link to a line of the {0} purchase order. To make changes, clear the PO Linked check box.", new object[1]
      {
        (object) dropShipLink.POOrderNbr
      });
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOLinkActive> e)
  {
    if (e.Row == null || e.Row.POSource != "D")
      return;
    bool? nullable1 = e.Row.IsLegacyDropShip;
    if (nullable1.GetValueOrDefault())
      return;
    DropShipLink dropShipLink = this.Base1.GetDropShipLink(e.Row);
    if (dropShipLink == null)
      return;
    nullable1 = dropShipLink.Active;
    if (nullable1.GetValueOrDefault())
    {
      nullable1 = dropShipLink.InReceipt;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOLinkActive>, PX.Objects.SO.SOLine, object>) e).NewValue;
        if (!nullable1.GetValueOrDefault())
          throw new PXSetPropertyException("The check box cannot be cleared because there are one or more unreleased PO receipts that contain lines of the linked {0} purchase order.", new object[1]
          {
            (object) dropShipLink.POOrderNbr
          });
      }
    }
    nullable1 = dropShipLink.Active;
    if (nullable1.GetValueOrDefault())
      return;
    nullable1 = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOLinkActive>, PX.Objects.SO.SOLine, object>) e).NewValue;
    if (!nullable1.GetValueOrDefault())
      return;
    string str = (string) null;
    int? inventoryId = e.Row.InventoryID;
    int? nullable2 = dropShipLink.POInventoryID;
    if (!(inventoryId.GetValueOrDefault() == nullable2.GetValueOrDefault() & inventoryId.HasValue == nullable2.HasValue))
    {
      str = PXUIFieldAttribute.GetDisplayName<PX.Objects.SO.SOLine.inventoryID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOLinkActive>>) e).Cache);
    }
    else
    {
      nullable2 = e.Row.SiteID;
      int? poSiteId = dropShipLink.POSiteID;
      if (!(nullable2.GetValueOrDefault() == poSiteId.GetValueOrDefault() & nullable2.HasValue == poSiteId.HasValue))
      {
        str = PXUIFieldAttribute.GetDisplayName<PX.Objects.SO.SOLine.siteID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOLinkActive>>) e).Cache);
      }
      else
      {
        Decimal? baseOrderQty = e.Row.BaseOrderQty;
        Decimal? poBaseOrderQty = dropShipLink.POBaseOrderQty;
        if (!(baseOrderQty.GetValueOrDefault() == poBaseOrderQty.GetValueOrDefault() & baseOrderQty.HasValue == poBaseOrderQty.HasValue))
          str = PXUIFieldAttribute.GetDisplayName<PX.Objects.SO.SOLine.orderQty>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOLinkActive>>) e).Cache);
      }
    }
    if (str != null)
      throw new PXSetPropertyException("The {0} column value in the line does not match the {0} column value in the linked line of the {1} purchase order.", new object[2]
      {
        (object) str,
        (object) dropShipLink.POOrderNbr
      });
  }

  public virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOLine> e)
  {
    if (!e.Row.POCreate.GetValueOrDefault() && e.OldRow.POCreate.GetValueOrDefault() && e.OldRow.POSource == "D" && !e.OldRow.IsLegacyDropShip.GetValueOrDefault() && !this.Base1.IsDropshipReturn(e.Row))
    {
      DropShipLink dropShipLink = this.Base1.GetDropShipLink(e.OldRow);
      if (dropShipLink == null)
        return;
      ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).GetExtension<DropShipLinkDialog>().UnlinkSOLineFromSupplyLine(dropShipLink, e.Row);
    }
    else if (e.Row.POSource == "D" && !e.Row.IsLegacyDropShip.GetValueOrDefault() && !this.Base1.IsDropshipReturn(e.Row))
    {
      DropShipLink dropShipLink = this.Base1.GetDropShipLink(e.Row);
      if (dropShipLink == null || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.SO.SOLine>>) e).Cache.ObjectsEqual<PX.Objects.SO.SOLine.pOLinkActive, PX.Objects.SO.SOLine.inventoryID, PX.Objects.SO.SOLine.siteID, PX.Objects.SO.SOLine.baseOrderQty, PX.Objects.SO.SOLine.completed>((object) e.Row, (object) e.OldRow))
        return;
      bool? poLinkActive1 = e.Row.POLinkActive;
      bool? poLinkActive2 = e.OldRow.POLinkActive;
      if (!(poLinkActive1.GetValueOrDefault() == poLinkActive2.GetValueOrDefault() & poLinkActive1.HasValue == poLinkActive2.HasValue))
        dropShipLink.Active = e.Row.POLinkActive;
      dropShipLink.SOInventoryID = e.Row.InventoryID;
      dropShipLink.SOSiteID = e.Row.SiteID;
      dropShipLink.SOBaseOrderQty = e.Row.BaseOrderQty;
      dropShipLink.SOCompleted = e.Row.Completed;
      ((PXSelectBase<DropShipLink>) this.Base1.DropShipLinks).Update(dropShipLink);
      poLinkActive2 = e.Row.POLinkActive;
      poLinkActive1 = e.OldRow.POLinkActive;
      if (poLinkActive2.GetValueOrDefault() == poLinkActive1.GetValueOrDefault() & poLinkActive2.HasValue == poLinkActive1.HasValue)
        return;
      ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).View.RequestRefresh();
    }
    else
    {
      bool? poCreate1 = e.Row.POCreate;
      bool? poCreate2 = e.OldRow.POCreate;
      if (poCreate1.GetValueOrDefault() == poCreate2.GetValueOrDefault() & poCreate1.HasValue == poCreate2.HasValue || !this.Base1.IsDropshipReturn(e.Row))
        return;
      poCreate2 = e.Row.POCreate;
      if (poCreate2.GetValueOrDefault())
      {
        DropShipLink originalDropShipLink = this.Base1.GetOriginalDropShipLink(e.Row);
        if (originalDropShipLink == null)
          return;
        e.Row.POOrderType = originalDropShipLink.POOrderType;
        e.Row.POOrderNbr = originalDropShipLink.POOrderNbr;
        e.Row.POLineNbr = originalDropShipLink.POLineNbr;
        e.Row.POLinkActive = originalDropShipLink.Active;
      }
      else
      {
        e.Row.POOrderType = (string) null;
        e.Row.POOrderNbr = (string) null;
        e.Row.POLineNbr = new int?();
        e.Row.POLinkActive = new bool?(false);
      }
    }
  }

  public virtual void _(PX.Data.Events.RowDeleting<PX.Objects.SO.SOLine> e)
  {
    PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current;
    if (e.Row == null || e.Row.POSource != "D")
      return;
    bool? nullable = e.Row.IsLegacyDropShip;
    if (nullable.GetValueOrDefault() || this.Base1.IsDropshipReturn(e.Row))
      return;
    DropShipLink dropShipLink = this.Base1.GetDropShipLink(e.Row);
    int num;
    if (dropShipLink == null)
    {
      num = 0;
    }
    else
    {
      nullable = dropShipLink.Active;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num != 0 && EnumerableExtensions.IsNotIn<PXEntryStatus>(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Cache.GetStatus((object) current), (PXEntryStatus) 3, (PXEntryStatus) 4))
    {
      e.Cancel = true;
      throw new PXException("The line cannot be deleted because there is an active link to a purchase order line.");
    }
  }

  public virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder> e)
  {
    if (e.Row == null)
      return;
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache, (object) null).For<PX.Objects.SO.SOLine.pOOrderStatus>((Action<PXUIFieldAttribute>) (fa => fa.Visible = ((PXSelectBase<PX.Objects.SO.SOOrderType>) ((PXGraphExtension<SOOrderEntry>) this).Base.soordertype).Current.RequireShipping.GetValueOrDefault())).SameFor<PX.Objects.SO.SOLine.pOOrderType>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.pOOrderNbr>();
    chained = chained.SameFor<PX.Objects.SO.SOLine.pOLineNbr>();
    chained.SameFor<PX.Objects.SO.SOLine.pOLinkActive>();
  }

  public virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOLine> e)
  {
    if (e.Row == null || e.Row.POSource != "D")
      return;
    bool? nullable1 = e.Row.IsLegacyDropShip;
    if (nullable1.GetValueOrDefault())
      return;
    DropShipLink dropShipLink = this.Base1.GetDropShipLink(e.Row);
    Decimal? nullable2;
    int num1;
    if (dropShipLink != null)
    {
      nullable1 = dropShipLink.Active;
      if (nullable1.GetValueOrDefault())
      {
        Decimal? baseReceivedQty = dropShipLink.BaseReceivedQty;
        nullable2 = dropShipLink.POBaseOrderQty;
        num1 = baseReceivedQty.GetValueOrDefault() == nullable2.GetValueOrDefault() & baseReceivedQty.HasValue == nullable2.HasValue ? 1 : 0;
        goto label_7;
      }
    }
    num1 = 0;
label_7:
    bool flag = num1 != 0;
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLine.pOSiteID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache, (object) e.Row, false);
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache;
    PX.Objects.SO.SOLine row = e.Row;
    int num2;
    if (e.Row.Operation == "I" && this.Base1.IsPOCreateEnabled(e.Row))
    {
      nullable1 = e.Row.POCreate;
      if (nullable1.GetValueOrDefault() && dropShipLink != null)
      {
        num2 = !flag ? 1 : 0;
        goto label_11;
      }
    }
    num2 = 0;
label_11:
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLine.pOLinkActive>(cache, (object) row, num2 != 0);
    if (PXUIFieldAttribute.GetErrorOnly<PX.Objects.SO.SOLine.pOLinkActive>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache, (object) e.Row) != null)
      return;
    nullable1 = e.Row.POCreate;
    if (nullable1.GetValueOrDefault() && dropShipLink != null)
    {
      nullable1 = dropShipLink.POCompleted;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = e.Row.Completed;
        if (!nullable1.GetValueOrDefault())
        {
          nullable2 = dropShipLink.BaseReceivedQty;
          Decimal? poBaseOrderQty = dropShipLink.POBaseOrderQty;
          if (nullable2.GetValueOrDefault() < poBaseOrderQty.GetValueOrDefault() & nullable2.HasValue & poBaseOrderQty.HasValue)
          {
            ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLine.pOLinkActive>((object) e.Row, (object) e.Row.POLinkActive, (Exception) new PXSetPropertyException<PX.Objects.SO.SOLine.pOLinkActive>("The linked {0} purchase order line has been completed.", (PXErrorLevel) 2, new object[1]
            {
              (object) dropShipLink.POOrderNbr
            }));
            return;
          }
        }
      }
    }
    nullable1 = e.Row.POCreate;
    if (nullable1.GetValueOrDefault())
    {
      int num3;
      if (dropShipLink == null)
      {
        num3 = 1;
      }
      else
      {
        nullable1 = dropShipLink.Active;
        num3 = !nullable1.GetValueOrDefault() ? 1 : 0;
      }
      if (num3 != 0)
      {
        nullable1 = e.Row.Completed;
        if (!nullable1.GetValueOrDefault() && !this.Base1.IsDropshipReturn(e.Row))
        {
          ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLine.pOLinkActive>((object) e.Row, (object) e.Row.POLinkActive, (Exception) new PXSetPropertyException<PX.Objects.SO.SOLine.pOLinkActive>("The sales order line has no active link to a line of a purchase order.", (PXErrorLevel) 2));
          return;
        }
      }
    }
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLine.pOLinkActive>((object) e.Row, (object) e.Row.POLinkActive, (Exception) null);
  }

  public virtual void _(PX.Data.Events.RowInserted<DropShipLink> e)
  {
    PX.Objects.SO.SupplyPOOrder supplyOrder = this.Base1.GetSupplyOrder(e.Row);
    if (supplyOrder == null)
      return;
    bool? poCompleted = e.Row.POCompleted;
    bool flag = false;
    if (!(poCompleted.GetValueOrDefault() == flag & poCompleted.HasValue) || !e.Row.Active.GetValueOrDefault())
      return;
    PX.Objects.SO.SupplyPOOrder supplyPoOrder = supplyOrder;
    int? notLinkedLinesCntr = supplyPoOrder.DropShipNotLinkedLinesCntr;
    supplyPoOrder.DropShipNotLinkedLinesCntr = notLinkedLinesCntr.HasValue ? new int?(notLinkedLinesCntr.GetValueOrDefault() - 1) : new int?();
    ((PXSelectBase<PX.Objects.SO.SupplyPOOrder>) this.Base1.SupplyPOOrders).Update(supplyOrder);
  }

  public virtual void _(PX.Data.Events.RowUpdated<DropShipLink> e)
  {
    PX.Objects.SO.SupplyPOOrder supplyOrder = this.Base1.GetSupplyOrder(e.Row);
    if (supplyOrder == null || ((PXSelectBase) this.Base1.DropShipLinks).Cache.ObjectsEqual<DropShipLink.poCompleted, DropShipLink.active>((object) e.Row, (object) e.OldRow))
      return;
    bool? nullable = e.OldRow.POCompleted;
    bool flag1 = false;
    int? notLinkedLinesCntr;
    if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
    {
      nullable = e.OldRow.Active;
      if (nullable.GetValueOrDefault())
      {
        PX.Objects.SO.SupplyPOOrder supplyPoOrder = supplyOrder;
        notLinkedLinesCntr = supplyPoOrder.DropShipNotLinkedLinesCntr;
        supplyPoOrder.DropShipNotLinkedLinesCntr = notLinkedLinesCntr.HasValue ? new int?(notLinkedLinesCntr.GetValueOrDefault() + 1) : new int?();
      }
    }
    nullable = e.Row.POCompleted;
    bool flag2 = false;
    if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
    {
      nullable = e.Row.Active;
      if (nullable.GetValueOrDefault())
      {
        PX.Objects.SO.SupplyPOOrder supplyPoOrder = supplyOrder;
        notLinkedLinesCntr = supplyPoOrder.DropShipNotLinkedLinesCntr;
        supplyPoOrder.DropShipNotLinkedLinesCntr = notLinkedLinesCntr.HasValue ? new int?(notLinkedLinesCntr.GetValueOrDefault() - 1) : new int?();
      }
    }
    ((PXSelectBase<PX.Objects.SO.SupplyPOOrder>) this.Base1.SupplyPOOrders).Update(supplyOrder);
  }

  public virtual void _(PX.Data.Events.RowDeleted<DropShipLink> e)
  {
    PX.Objects.SO.SupplyPOOrder supplyOrder = this.Base1.GetSupplyOrder(e.Row);
    if (supplyOrder == null)
      return;
    bool? poCompleted = e.Row.POCompleted;
    bool flag = false;
    if (!(poCompleted.GetValueOrDefault() == flag & poCompleted.HasValue) || !e.Row.Active.GetValueOrDefault())
      return;
    PX.Objects.SO.SupplyPOOrder supplyPoOrder = supplyOrder;
    int? notLinkedLinesCntr = supplyPoOrder.DropShipNotLinkedLinesCntr;
    supplyPoOrder.DropShipNotLinkedLinesCntr = notLinkedLinesCntr.HasValue ? new int?(notLinkedLinesCntr.GetValueOrDefault() + 1) : new int?();
    ((PXSelectBase<PX.Objects.SO.SupplyPOOrder>) this.Base1.SupplyPOOrders).Update(supplyOrder);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.SOOrderEntry.SOOrder_RowDeleting(PX.Data.PXCache,PX.Data.PXRowDeletingEventArgs)" />
  /// </summary>
  [PXOverride]
  public virtual void SOOrder_RowDeleting(
    PXCache sender,
    PXRowDeletingEventArgs e,
    PXRowDeleting baseMethod)
  {
    baseMethod.Invoke(sender, e);
    PX.Objects.SO.SOOrder order = (PX.Objects.SO.SOOrder) e.Row;
    DropShipLink dropShipLink = PXResultset<DropShipLink>.op_Implicit(PXSelectBase<DropShipLink, PXSelectReadonly<DropShipLink, Where<DropShipLink.sOOrderType, Equal<Required<PX.Objects.SO.SOLine.orderType>>, And<DropShipLink.sOOrderNbr, Equal<Required<PX.Objects.SO.SOLine.orderNbr>>>>, OrderBy<Desc<DropShipLink.inReceipt>>>.Config>.SelectWindowed((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, 0, 1, new object[2]
    {
      (object) order.OrderType,
      (object) order.OrderNbr
    })) ?? ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (DropShipLink)].Inserted.Cast<DropShipLink>().FirstOrDefault<DropShipLink>((Func<DropShipLink, bool>) (l => l.SOOrderType == order.OrderType && l.SOOrderNbr == order.OrderNbr));
    if (dropShipLink == null)
      return;
    if (dropShipLink.InReceipt.GetValueOrDefault())
      throw new PXException("The {0} sales order cannot be deleted because there are one or more unreleased PO receipts that contain lines of the linked {1} purchase order.", new object[2]
      {
        (object) order.OrderNbr,
        (object) dropShipLink.POOrderNbr
      });
    if (((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).View.Ask(PXMessages.LocalizeFormatNoPrefixNLA("The {0} sales order has a link to a drop-ship purchase order. Do you want to remove the link and delete the {0} sales order?", new object[1]
    {
      (object) order.OrderNbr
    }), (MessageButtons) 1) != 2)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.SO.SOOrder> e)
  {
    if (!e.Row.Cancelled.GetValueOrDefault() || e.OldRow.Cancelled.GetValueOrDefault())
      return;
    PXResultset<PX.Objects.SO.SOLine> pxResultset = PXSelectBase<PX.Objects.SO.SOLine, PXSelect<PX.Objects.SO.SOLine, Where<PX.Objects.SO.SOLine.orderType, Equal<Required<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOLine.orderNbr, Equal<Required<PX.Objects.SO.SOOrder.orderNbr>>, And<PX.Objects.SO.SOLine.pOCreate, Equal<True>, And<PX.Objects.SO.SOLine.pOSource, Equal<INReplenishmentSource.dropShipToOrder>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, new object[2]
    {
      (object) e.Row.OrderType,
      (object) e.Row.OrderNbr
    });
    DropShipLinkDialog extension = ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).GetExtension<DropShipLinkDialog>();
    foreach (PXResult<PX.Objects.SO.SOLine> pxResult in pxResultset)
    {
      PX.Objects.SO.SOLine soLine = PXResult<PX.Objects.SO.SOLine>.op_Implicit(pxResult);
      DropShipLink dropShipLink = this.Base1.GetDropShipLink(soLine);
      extension.UnlinkSOLineFromSupplyLine(dropShipLink, soLine);
    }
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.SOOrderEntry.SOOrder_Cancelled_FieldVerifying(PX.Data.PXCache,PX.Data.PXFieldVerifyingEventArgs)" />
  /// </summary>
  [PXOverride]
  public virtual void SOOrder_Cancelled_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e,
    PXFieldVerifying baseMethod)
  {
    baseMethod.Invoke(sender, e);
    PX.Objects.SO.SOOrder order = (PX.Objects.SO.SOOrder) e.Row;
    if (e.Row == null || !((bool?) e.NewValue).GetValueOrDefault())
      return;
    DropShipLink dropShipLink = PXResultset<DropShipLink>.op_Implicit(PXSelectBase<DropShipLink, PXSelectReadonly<DropShipLink, Where<DropShipLink.sOOrderType, Equal<Required<PX.Objects.SO.SOOrder.orderType>>, And<DropShipLink.sOOrderNbr, Equal<Required<PX.Objects.SO.SOOrder.orderNbr>>>>, OrderBy<Desc<DropShipLink.inReceipt>>>.Config>.SelectWindowed((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, 0, 1, new object[2]
    {
      (object) order.OrderType,
      (object) order.OrderNbr
    })) ?? ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Caches[typeof (DropShipLink)].Inserted.Cast<DropShipLink>().FirstOrDefault<DropShipLink>((Func<DropShipLink, bool>) (l => l.SOOrderType == order.OrderType && l.SOOrderNbr == order.OrderNbr));
    if (dropShipLink == null)
      return;
    if (dropShipLink.InReceipt.GetValueOrDefault())
      throw new PXException("The {0} sales order cannot be canceled because there are one or more unreleased PO receipts that contain lines of the linked {1} purchase order.", new object[2]
      {
        (object) order.OrderNbr,
        (object) dropShipLink.POOrderNbr
      });
    if (((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).View.Ask(PXMessages.LocalizeFormatNoPrefixNLA("The {0} sales order has a link to a drop-ship purchase order. Do you want to remove the link and cancel the {0} sales order?", new object[1]
    {
      (object) order.OrderNbr
    }), (MessageButtons) 1) != 2)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.SupplyPOOrder, PX.Objects.SO.SupplyPOOrder.dropShipLinkedLinesCount> e)
  {
    int? oldValue = (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.SO.SupplyPOOrder, PX.Objects.SO.SupplyPOOrder.dropShipLinkedLinesCount>, PX.Objects.SO.SupplyPOOrder, object>) e).OldValue;
    if (e.Row == null)
      return;
    int? linkedLinesCount = e.Row.DropShipLinkedLinesCount;
    int? nullable = oldValue;
    if (linkedLinesCount.GetValueOrDefault() == nullable.GetValueOrDefault() & linkedLinesCount.HasValue == nullable.HasValue)
      return;
    nullable = e.Row.DropShipLinkedLinesCount;
    int num = 0;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
      return;
    ((PXSelectBase) this.Base1.SupplyPOOrders).Cache.SetValue<PX.Objects.SO.SupplyPOOrder.sOOrderType>((object) e.Row, (object) null);
    ((PXSelectBase) this.Base1.SupplyPOOrders).Cache.SetValue<PX.Objects.SO.SupplyPOOrder.sOOrderNbr>((object) e.Row, (object) null);
  }
}
