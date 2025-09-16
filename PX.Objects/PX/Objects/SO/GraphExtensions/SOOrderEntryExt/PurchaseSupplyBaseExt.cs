// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.PurchaseSupplyBaseExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.DAC;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public class PurchaseSupplyBaseExt : PXGraphExtension<SOOrderEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelect<DropShipLink, Where<DropShipLink.sOOrderType, Equal<Required<PX.Objects.SO.SOLine.orderType>>, And<DropShipLink.sOOrderNbr, Equal<Required<PX.Objects.SO.SOLine.orderNbr>>, And<DropShipLink.sOLineNbr, Equal<Required<PX.Objects.SO.SOLine.lineNbr>>>>>> DropShipLinks;
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.SO.SupplyPOOrder, Where<PX.Objects.SO.SupplyPOOrder.orderType, Equal<Required<PX.Objects.SO.SupplyPOOrder.orderType>>, And<PX.Objects.SO.SupplyPOOrder.orderNbr, Equal<Required<PX.Objects.SO.SupplyPOOrder.orderNbr>>>>> SupplyPOOrders;
  protected PurchaseSupplyBaseExt.SOLinePrefetchedWarnings soLineWarnings;
  protected HashSet<string> prefetched = new HashSet<string>();

  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.sOToPOLink>() || PXAccess.FeatureInstalled<FeaturesSet.dropShipments>() || PXAccess.FeatureInstalled<FeaturesSet.purchaseRequisitions>();
  }

  public virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOOrder> e)
  {
    if (e.Row == null)
      return;
    bool flag = ((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current.RequireShipping.GetValueOrDefault() || ((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current.Behavior == "BL";
    PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLine.pOCreate>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLineSplit.pOCreate>(((PXSelectBase) this.Base.splits).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLine.pOSource>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLineSplit.pOSource>(((PXSelectBase) this.Base.splits).Cache, (object) null, flag);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOLineSplit> e)
  {
    if (e.Row?.PONbr == null)
      return;
    bool? nullable = e.Row.Completed;
    if (nullable.GetValueOrDefault())
      return;
    nullable = e.Row.POCancelled;
    if (nullable.GetValueOrDefault())
    {
      ((PXSelectBase) this.Base.splits).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLineSplit.refNoteID>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("The linked {0} purchase order line has been canceled.", (PXErrorLevel) 3, new object[1]
      {
        (object) e.Row.PONbr
      }));
    }
    else
    {
      nullable = e.Row.POCompleted;
      if (!nullable.GetValueOrDefault())
        return;
      ((PXSelectBase) this.Base.splits).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLineSplit.refNoteID>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("The linked {0} purchase order line has been completed.", (PXErrorLevel) 3, new object[1]
      {
        (object) e.Row.PONbr
      }));
    }
  }

  public virtual void _(PX.Data.Events.RowSelected<PX.Objects.SO.SOLine> e)
  {
    if (e.Row == null)
      return;
    this.POCreateVerifyValue(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache, e.Row, e.Row.POCreate);
    bool enabled = this.IsPOCreateEnabled(e.Row);
    if (e.Row.POSource == null)
    {
      PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache;
      PX.Objects.SO.SOLine row1 = e.Row;
      bool? nullable;
      int num1;
      if (enabled)
      {
        nullable = e.Row.Completed;
        num1 = !nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num1 = 0;
      PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLine.pOCreate>(cache1, (object) row1, num1 != 0);
      PXCache cache2 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache;
      PX.Objects.SO.SOLine row2 = e.Row;
      int num2;
      if (enabled)
      {
        nullable = e.Row.POCreate;
        if (nullable.GetValueOrDefault())
        {
          num2 = !this.IsDropshipReturn(e.Row) ? 1 : 0;
          goto label_9;
        }
      }
      num2 = 0;
label_9:
      PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLine.pOSource>(cache2, (object) row2, num2 != 0);
    }
    else
    {
      bool? nullable;
      if (!this.IsPoToSoOrBlanket(e.Row.POSource))
      {
        nullable = e.Row.IsLegacyDropShip;
        if (!nullable.GetValueOrDefault())
        {
          if (e.Row.POSource == "D")
          {
            DropShipLink dropShipLink = this.GetDropShipLink(e.Row);
            int num3;
            if (dropShipLink != null)
            {
              Decimal? baseReceivedQty = dropShipLink.BaseReceivedQty;
              Decimal num4 = 0M;
              num3 = baseReceivedQty.GetValueOrDefault() > num4 & baseReceivedQty.HasValue ? 1 : 0;
            }
            else
              num3 = 0;
            bool flag = num3 != 0;
            PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLine.pOCreate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache, (object) e.Row, enabled && !flag || this.IsDropshipReturn(e.Row));
            PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache;
            PX.Objects.SO.SOLine row = e.Row;
            int num5;
            if (enabled)
            {
              nullable = e.Row.POCreate;
              if (nullable.GetValueOrDefault() && dropShipLink == null)
              {
                num5 = !this.IsDropshipReturn(e.Row) ? 1 : 0;
                goto label_32;
              }
            }
            num5 = 0;
label_32:
            PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLine.pOSource>(cache, (object) row, num5 != 0);
            goto label_33;
          }
          goto label_33;
        }
      }
      if (!enabled)
      {
        e.Row.POCreate = new bool?(false);
        e.Row.POSource = (string) null;
      }
      this.SetPOCreateEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache, e.Row, enabled);
      PXCache cache3 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.SO.SOLine>>) e).Cache;
      PX.Objects.SO.SOLine row3 = e.Row;
      int num6;
      if (enabled)
      {
        nullable = e.Row.POCreate;
        if (nullable.GetValueOrDefault())
        {
          nullable = e.Row.POCreated;
          if (!nullable.GetValueOrDefault())
          {
            Decimal? shippedQty = e.Row.ShippedQty;
            Decimal num7 = 0M;
            if (!(shippedQty.GetValueOrDefault() == num7 & shippedQty.HasValue))
            {
              nullable = e.Row.IsLegacyDropShip;
              if (!nullable.GetValueOrDefault())
                goto label_22;
            }
            if (((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current.Behavior != "BL")
            {
              nullable = e.Row.IsSpecialOrder;
              if (!nullable.GetValueOrDefault())
              {
                num6 = !this.IsIntercompanyIssue(e.Row) ? 1 : 0;
                goto label_23;
              }
            }
          }
        }
      }
label_22:
      num6 = 0;
label_23:
      PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLine.pOSource>(cache3, (object) row3, num6 != 0);
    }
label_33:
    this.soLineWarnings?.ShowLineWarning(((PXSelectBase) this.Base.Transactions).Cache, e.Row);
  }

  protected virtual void SetPOCreateEnabled(PXCache cache, PX.Objects.SO.SOLine soline, bool poCreateEnabled)
  {
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLine.pOCreate>(cache, (object) soline, poCreateEnabled);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.SO.SOLine> e)
  {
    if (EnumerableExtensions.IsNotIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    PXDefaultAttribute.SetPersistingCheck<PX.Objects.SO.SOLine.pOSource>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.SO.SOLine>>) e).Cache, (object) e.Row, e.Row.POCreate.GetValueOrDefault() ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  public virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOCreate> e)
  {
    if (e.Row == null)
      return;
    bool? newValue = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOCreate>, PX.Objects.SO.SOLine, object>) e).NewValue;
    this.POCreateVerifyValue(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOCreate>>) e).Cache, e.Row, newValue);
  }

  public virtual void _(PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOCreate> e)
  {
    if (e.Row == null)
      return;
    if (!this.IsPOCreateEnabled(e.Row))
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOCreate>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) false;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOCreate>>) e).Cancel = true;
    }
    else
    {
      int? nullable = e.Row.InventoryID;
      if (!nullable.HasValue)
        return;
      nullable = e.Row.SiteID;
      if (!nullable.HasValue)
        return;
      bool flag1 = PXAccess.FeatureInstalled<FeaturesSet.dropShipments>() && ((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current.Behavior != "BL" && !this.IsIntercompanyIssue(e.Row);
      bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.sOToPOLink>();
      INItemSiteSettings itemSiteSettings = ((PXSelectBase<INItemSiteSettings>) this.Base.initemsettings).SelectSingle(new object[2]
      {
        (object) e.Row.InventoryID,
        (object) e.Row.SiteID
      });
      if ((!(itemSiteSettings.ReplenishmentSource == "D" & flag1) || this.IsIssueWithARNoUpdate(e.Row)) && !(itemSiteSettings.ReplenishmentSource == "O" & flag2))
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOCreate>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) true;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOCreate>>) e).Cancel = true;
    }
  }

  public virtual void _(PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOSiteID> e)
  {
    if (e.Row == null || !e.Row.POCreate.GetValueOrDefault())
      return;
    if (e.Row.POSource == "D" && !e.Row.IsLegacyDropShip.GetValueOrDefault() || e.Row.Behavior == "BL")
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOSiteID>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) e.Row.SiteID;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOSiteID>>) e).Cancel = true;
    }
    else
    {
      INItemSiteSettings itemSiteSettings = ((PXSelectBase<INItemSiteSettings>) this.Base.initemsettings).SelectSingle(new object[2]
      {
        (object) e.Row.InventoryID,
        (object) e.Row.SiteID
      });
      object obj = (object) null;
      if (itemSiteSettings != null && EnumerableExtensions.IsIn<string>(itemSiteSettings.ReplenishmentSource, "P", "O", "D"))
        obj = (object) itemSiteSettings.ReplenishmentSourceSiteID;
      if (obj == null)
        obj = (object) e.Row.SiteID;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOSiteID>, PX.Objects.SO.SOLine, object>) e).NewValue = obj;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOSiteID>>) e).Cancel = true;
    }
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.vendorID> e)
  {
    if (e.Row == null || !e.Row.POCreate.GetValueOrDefault())
      return;
    bool? requireLocation = ((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current.RequireLocation;
    bool flag = false;
    if (!(requireLocation.GetValueOrDefault() == flag & requireLocation.HasValue) || (!(e.Row.TranType != "0") || !((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current.RequireShipping.GetValueOrDefault()) && !(((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current.Behavior == "BL"))
      return;
    INItemSiteSettings itemSiteSettings = ((PXSelectBase<INItemSiteSettings>) this.Base.initemsettings).SelectSingle(new object[2]
    {
      (object) e.Row.InventoryID,
      (object) e.Row.SiteID
    });
    PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this.Base, ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.BranchID);
    int? preferredVendorId = (int?) itemSiteSettings?.PreferredVendorID;
    int? nullable1 = ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.CustomerID;
    if (preferredVendorId.GetValueOrDefault() == nullable1.GetValueOrDefault() & preferredVendorId.HasValue == nullable1.HasValue)
      return;
    nullable1 = (int?) itemSiteSettings?.PreferredVendorID;
    int? nullable2 = branch.BAccountID;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return;
    PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.vendorID> fieldDefaulting = e;
    int? nullable3;
    if (itemSiteSettings == null)
    {
      nullable2 = new int?();
      nullable3 = nullable2;
    }
    else
      nullable3 = itemSiteSettings.PreferredVendorID;
    // ISSUE: variable of a boxed type
    __Boxed<int?> local = (ValueType) nullable3;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.vendorID>, PX.Objects.SO.SOLine, object>) fieldDefaulting).NewValue = (object) local;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.vendorID>>) e).Cancel = true;
  }

  public virtual void _(PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOSource> e)
  {
    if (e.Row == null)
      return;
    bool flag1 = PXAccess.FeatureInstalled<FeaturesSet.dropShipments>() && ((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current.Behavior != "BL" && !this.IsIntercompanyIssue(e.Row);
    bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.sOToPOLink>();
    bool? nullable = e.Row.POCreate;
    if (!nullable.GetValueOrDefault())
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOSource>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) null;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOSource>>) e).Cancel = true;
    }
    else
    {
      if (flag1)
      {
        if (!this.IsDropshipReturn(e.Row))
        {
          PX.Objects.IN.InventoryItem inventoryItem;
          if (!this.IsIssueWithARNoUpdate(e.Row) && (inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, e.Row.InventoryID)) != null)
          {
            nullable = inventoryItem.StkItem;
            if (!nullable.GetValueOrDefault())
            {
              nullable = inventoryItem.NonStockReceipt;
              if (nullable.GetValueOrDefault())
              {
                nullable = inventoryItem.NonStockShip;
                if (!nullable.GetValueOrDefault())
                  goto label_11;
              }
              else
                goto label_11;
            }
            else
              goto label_11;
          }
          else
            goto label_11;
        }
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOSource>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) "D";
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOSource>>) e).Cancel = true;
        return;
      }
label_11:
      if (flag2 && (((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current.Behavior == "BL" || this.IsIntercompanyIssue(e.Row)))
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOSource>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) "O";
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOSource>>) e).Cancel = true;
      }
      else
      {
        INItemSiteSettings itemSiteSettings = ((PXSelectBase<INItemSiteSettings>) this.Base.initemsettings).SelectSingle(new object[2]
        {
          (object) e.Row.InventoryID,
          (object) e.Row.SiteID
        });
        if (!(itemSiteSettings?.POSource == "O" & flag2) && (!(itemSiteSettings?.POSource == "D" & flag1) || this.IsIssueWithARNoUpdate(e.Row)))
          return;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOSource>, PX.Objects.SO.SOLine, object>) e).NewValue = (object) itemSiteSettings.POSource;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOSource>>) e).Cancel = true;
      }
    }
  }

  public virtual void _(PX.Data.Events.FieldUpdating<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOSource> e)
  {
    if (e.Row == null || e.Row.POCreate.GetValueOrDefault() || e.Row.POSource == null)
      return;
    e.Row.POSource = (string) null;
  }

  public virtual void _(PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOSource> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOSource>, PX.Objects.SO.SOLine, object>) e).NewValue == null)
      return;
    string newValue = (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOSource>, PX.Objects.SO.SOLine, object>) e).NewValue;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, e.Row.InventoryID);
    if (inventoryItem != null && !inventoryItem.StkItem.GetValueOrDefault() && (newValue == "D" || newValue == "L") && (!inventoryItem.NonStockReceipt.GetValueOrDefault() || !inventoryItem.NonStockShip.GetValueOrDefault() || e.Row.LineType == "MI"))
      throw new PXSetPropertyException<PX.Objects.SO.SOLine.pOSource>("Only items for which both 'Require Receipt' and 'Require Shipment' are selected can be drop shipped.");
    if (this.IsIssueWithARNoUpdate(e.Row) && EnumerableExtensions.IsIn<string>(newValue, "D", "L"))
      throw new PXSetPropertyException<PX.Objects.SO.SOLine.pOSource>("Drop shipping is not allowed for the {0} order type.", new object[1]
      {
        (object) e.Row.OrderType
      });
    if (EnumerableExtensions.IsIn<string>(newValue, "D", "L") && !this.Base.LineSplittingAllocatedExt.IsLotSerialsAllowedForDropShipLine(e.Row) && this.Base.LineSplittingAllocatedExt.HasMultipleSplitsOrAllocation(e.Row))
      throw new PXSetPropertyException<PX.Objects.SO.SOLine.pOSource>("The line cannot be drop-shipped because it is split into multiple lines or allocated in the Line Details dialog box.");
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.tranType> e)
  {
    if (e.Row == null || (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.tranType>, PX.Objects.SO.SOLine, object>) e).OldValue == (string) e.NewValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.tranType>>) e).Cache.SetDefaultExt<PX.Objects.SO.SOLine.pOCreate>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.operation> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.operation>>) e).Cache.SetDefaultExt<PX.Objects.SO.SOLine.pOCreate>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.operation>>) e).Cache.SetDefaultExt<PX.Objects.SO.SOLine.pOSource>((object) e.Row);
  }

  public virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.siteID> e)
  {
    if (e.Row == null || e.Row.POSource == "D" && !e.Row.IsLegacyDropShip.GetValueOrDefault() && this.GetDropShipLink(e.Row) != null)
      return;
    this.ClearPOFieldsOnWarehouseChange(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.siteID>>) e).Cache, e.Row);
  }

  protected virtual void ClearPOFieldsOnWarehouseChange(PXCache cache, PX.Objects.SO.SOLine line)
  {
    cache.SetDefaultExt<PX.Objects.SO.SOLine.pOCreate>((object) line);
    cache.SetDefaultExt<PX.Objects.SO.SOLine.pOSource>((object) line);
    cache.SetDefaultExt<PX.Objects.SO.SOLine.pOCreated>((object) line);
  }

  public virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOCreate> e)
  {
    if (!e.Row.POCreated.GetValueOrDefault())
    {
      if (e.Row.POCreate.GetValueOrDefault())
      {
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOCreate>>) e).Cache.SetDefaultExt<PX.Objects.SO.SOLine.pOSource>((object) e.Row);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOCreate>>) e).Cache.SetDefaultExt<PX.Objects.SO.SOLine.pOSiteID>((object) e.Row);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOCreate>>) e).Cache.SetDefaultExt<PX.Objects.SO.SOLine.vendorID>((object) e.Row);
      }
      else
      {
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOCreate>>) e).Cache.SetValueExt<PX.Objects.SO.SOLine.pOSource>((object) e.Row, (object) null);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOCreate>>) e).Cache.SetValueExt<PX.Objects.SO.SOLine.pOSiteID>((object) e.Row, (object) null);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLine, PX.Objects.SO.SOLine.pOCreate>>) e).Cache.SetValueExt<PX.Objects.SO.SOLine.vendorID>((object) e.Row, (object) null);
      }
    }
    SOOrderLineSplittingAllocatedExtension.ResetAvailabilityCounters(e.Row);
  }

  public virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.pOCreate> e)
  {
    if (e.Row.POCreate.GetValueOrDefault())
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.pOCreate>>) e).Cache.SetDefaultExt<PX.Objects.SO.SOLineSplit.pOSource>((object) e.Row);
    else
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.pOCreate>>) e).Cache.SetValueExt<PX.Objects.SO.SOLineSplit.pOSource>((object) e.Row, (object) null);
  }

  public virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.pOSource> e)
  {
    if (e.Row == null || e.Row.POCreate.GetValueOrDefault())
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.pOSource>, PX.Objects.SO.SOLineSplit, object>) e).NewValue = (object) null;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.pOSource>>) e).Cancel = true;
  }

  public virtual void POCreateVerifyValue(PXCache sender, PX.Objects.SO.SOLine row, bool? value)
  {
    if (row == null || !row.InventoryID.HasValue || !value.GetValueOrDefault())
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, row.InventoryID);
    if (inventoryItem == null || inventoryItem.StkItem.GetValueOrDefault())
      return;
    if (inventoryItem.KitItem.GetValueOrDefault())
    {
      sender.RaiseExceptionHandling<PX.Objects.SO.SOLine.pOCreate>((object) row, (object) value, (Exception) new PXSetPropertyException("Non-Stock kit items cannot be linked with purchase order.", (PXErrorLevel) 4));
    }
    else
    {
      if (inventoryItem.NonStockShip.GetValueOrDefault() && inventoryItem.NonStockReceipt.GetValueOrDefault() || !PXAccess.FeatureInstalled<FeaturesSet.sOToPOLink>())
        return;
      sender.RaiseExceptionHandling<PX.Objects.SO.SOLine.pOCreate>((object) row, (object) value, (Exception) new PXSetPropertyException("Require Ship/Receipt is OFF in the Non-Stock settings.", (PXErrorLevel) 2));
    }
  }

  public virtual bool IsPOCreateEnabled(PX.Objects.SO.SOLine row)
  {
    if ((!((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current.RequireShipping.GetValueOrDefault() || !(row.TranType != "0")) && !(((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current.Behavior == "BL") || !(((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current.ARDocType != "UND") && !PXAccess.FeatureInstalled<FeaturesSet.sOToPOLink>())
      return false;
    if (row.Operation == "I")
      return true;
    return this.IsDropshipReturn(row) && PXAccess.FeatureInstalled<FeaturesSet.dropShipments>();
  }

  [PXInternalUseOnly]
  protected virtual bool IsIntercompanyIssue(PX.Objects.SO.SOLine row)
  {
    if (!(row.Operation == "I"))
      return false;
    return !string.IsNullOrEmpty(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.IntercompanyPONbr) || !string.IsNullOrEmpty(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.IntercompanyPOReturnNbr);
  }

  public virtual bool IsIssueWithARNoUpdate(PX.Objects.SO.SOLine row)
  {
    return row.Operation == "I" && ((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current.ARDocType == "UND";
  }

  public virtual bool IsDropshipReturn(PX.Objects.SO.SOLine row)
  {
    return row.Operation == "R" && row.Behavior == "RM" && row.OrigShipmentType == "H" && ((PXSelectBase<PX.Objects.SO.SOOrderType>) this.Base.soordertype).Current.ARDocType != "UND";
  }

  public virtual bool IsPoToSoOrBlanket(string poSource)
  {
    return poSource != null && EnumerableExtensions.IsIn<string>(poSource, "O", "L", "B");
  }

  public virtual bool IsOriginalDSLinkVisible(PX.Objects.SO.SOLine row)
  {
    return row != null && row.POCreate.GetValueOrDefault() && row.Operation == "R" && row.OrigShipmentType == "H";
  }

  public virtual void FillInsertingSchedule(PXCache sender, PX.Objects.SO.SOLineSplit split)
  {
    PX.Objects.SO.SOLine line = split != null ? PXParentAttribute.SelectParent<PX.Objects.SO.SOLine>(sender, (object) split) : (PX.Objects.SO.SOLine) null;
    if (split == null || split.POLineNbr.HasValue || line == null || line.POSource != "D" || line.IsLegacyDropShip.GetValueOrDefault())
      return;
    DropShipLink dropShipLink = this.GetDropShipLink(line);
    if (dropShipLink == null)
      return;
    split.POType = dropShipLink.POOrderType;
    split.PONbr = dropShipLink.POOrderNbr;
    split.POLineNbr = dropShipLink.POLineNbr;
  }

  [PXOverride]
  public virtual void PrefetchWithDetails()
  {
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current == null || ((PXSelectBase) this.DropShipLinks).Cache.IsDirty || this.prefetched.Contains(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderType + ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderNbr) || PXView.MaximumRows == 1)
      return;
    this.PrefetchWarnings();
    if (PXResultset<SOOrderTypeOperation>.op_Implicit(PXSelectBase<SOOrderTypeOperation, PXSelect<SOOrderTypeOperation, Where<SOOrderTypeOperation.orderType, Equal<Current<PX.Objects.SO.SOOrderType.orderType>>, And<SOOrderTypeOperation.operation, Equal<SOOperation.receipt>, And<SOOrderTypeOperation.active, Equal<True>>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())) != null)
      this.DoPrefetch((PXSelectBase<PX.Objects.SO.SOLine>) new PXSelectReadonly2<PX.Objects.SO.SOLine, LeftJoin<DropShipLink, On<DropShipLink.sOOrderType, Equal<PX.Objects.SO.SOLine.origOrderType>, And<DropShipLink.sOOrderNbr, Equal<PX.Objects.SO.SOLine.origOrderNbr>, And<DropShipLink.sOLineNbr, Equal<PX.Objects.SO.SOLine.origLineNbr>>>>, LeftJoin<PX.Objects.SO.SupplyPOOrder, On<DropShipLink.FK.SupplyPOOrder>>>, Where<PX.Objects.SO.SOLine.orderType, Equal<Current<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOLine.orderNbr, Equal<Current<PX.Objects.SO.SOOrder.orderNbr>>, And<PX.Objects.SO.SOLine.operation, Equal<SOOperation.receipt>, And<PX.Objects.SO.SOLine.origShipmentType, Equal<INDocType.dropShip>>>>>>((PXGraph) this.Base));
    this.DoPrefetch((PXSelectBase<PX.Objects.SO.SOLine>) new PXSelectReadonly2<PX.Objects.SO.SOLine, LeftJoin<DropShipLink, On<DropShipLink.FK.SOLine>, LeftJoin<PX.Objects.SO.SupplyPOOrder, On<DropShipLink.FK.SupplyPOOrder>>>, Where<PX.Objects.SO.SOLine.orderType, Equal<Current<PX.Objects.SO.SOOrder.orderType>>, And<PX.Objects.SO.SOLine.orderNbr, Equal<Current<PX.Objects.SO.SOOrder.orderNbr>>, And<PX.Objects.SO.SOLine.operation, Equal<SOOperation.issue>>>>>((PXGraph) this.Base));
    this.prefetched.Add(((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderType + ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current.OrderNbr);
  }

  protected virtual void DoPrefetch(PXSelectBase<PX.Objects.SO.SOLine> detailsWithLinksQuery)
  {
    Type[] typeArray = new Type[5]
    {
      typeof (PX.Objects.SO.SOLine.orderType),
      typeof (PX.Objects.SO.SOLine.orderNbr),
      typeof (PX.Objects.SO.SOLine.lineNbr),
      typeof (DropShipLink),
      typeof (PX.Objects.SO.SupplyPOOrder)
    };
    using (new PXFieldScope(((PXSelectBase) detailsWithLinksQuery).View, typeArray))
    {
      int startRow = PXView.StartRow;
      int num = 0;
      foreach (PXResult<PX.Objects.SO.SOLine, DropShipLink, PX.Objects.SO.SupplyPOOrder> pxResult in ((PXSelectBase) detailsWithLinksQuery).View.Select(PXView.Currents, PXView.Parameters, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num))
      {
        PX.Objects.SO.SOLine line = PXResult<PX.Objects.SO.SOLine, DropShipLink, PX.Objects.SO.SupplyPOOrder>.op_Implicit(pxResult);
        DropShipLink link = PXResult<PX.Objects.SO.SOLine, DropShipLink, PX.Objects.SO.SupplyPOOrder>.op_Implicit(pxResult);
        PX.Objects.SO.SupplyPOOrder order = PXResult<PX.Objects.SO.SOLine, DropShipLink, PX.Objects.SO.SupplyPOOrder>.op_Implicit(pxResult);
        this.DropShipLinkStoreCached(link, line);
        if (order != null && order.OrderNbr != null)
          this.SupplyOrderStoreCached(order);
      }
    }
  }

  public virtual void DropShipLinkStoreCached(DropShipLink link, PX.Objects.SO.SOLine line)
  {
    List<object> objectList = new List<object>(1);
    if (link != null && link.SOOrderType != null)
      objectList.Add((object) link);
    ((PXSelectBase<DropShipLink>) this.DropShipLinks).StoreResult(objectList, PXQueryParameters.ExplicitParameters(new object[3]
    {
      (object) line.OrderType,
      (object) line.OrderNbr,
      (object) line.LineNbr
    }));
  }

  public virtual DropShipLink GetDropShipLink(PX.Objects.SO.SOLine line)
  {
    if (line == null || line.POSource != "D" || line.IsLegacyDropShip.GetValueOrDefault())
      return (DropShipLink) null;
    return PXResultset<DropShipLink>.op_Implicit(((PXSelectBase<DropShipLink>) this.DropShipLinks).SelectWindowed(0, 1, new object[3]
    {
      (object) line.OrderType,
      (object) line.OrderNbr,
      (object) line.LineNbr
    }));
  }

  public virtual DropShipLink GetOriginalDropShipLink(PX.Objects.SO.SOLine line)
  {
    if (line == null)
      return (DropShipLink) null;
    return PXResultset<DropShipLink>.op_Implicit(((PXSelectBase<DropShipLink>) this.DropShipLinks).SelectWindowed(0, 1, new object[3]
    {
      (object) line.OrigOrderType,
      (object) line.OrigOrderNbr,
      (object) line.OrigLineNbr
    }));
  }

  public virtual void SupplyOrderStoreCached(PX.Objects.SO.SupplyPOOrder order)
  {
    ((PXSelectBase<PX.Objects.SO.SupplyPOOrder>) this.SupplyPOOrders).StoreResult((IBqlTable) order);
  }

  public virtual PX.Objects.SO.SupplyPOOrder GetSupplyOrder(PX.Objects.SO.SOLine line)
  {
    return line == null || line.POSource != "D" || line.IsLegacyDropShip.GetValueOrDefault() ? (PX.Objects.SO.SupplyPOOrder) null : this.GetSupplyOrder(this.GetDropShipLink(line));
  }

  public virtual PX.Objects.SO.SupplyPOOrder GetSupplyOrder(DropShipLink link)
  {
    if (link == null)
      return (PX.Objects.SO.SupplyPOOrder) null;
    return PXResultset<PX.Objects.SO.SupplyPOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SupplyPOOrder>) this.SupplyPOOrders).SelectWindowed(0, 1, new object[2]
    {
      (object) link.POOrderType,
      (object) link.POOrderNbr
    }));
  }

  public virtual PX.Objects.SO.SupplyPOOrder GetOriginalSupplyOrder(PX.Objects.SO.SOLine line)
  {
    if (line == null || line.POSource != "D" || line.IsLegacyDropShip.GetValueOrDefault())
      return (PX.Objects.SO.SupplyPOOrder) null;
    DropShipLink originalDropShipLink = this.GetOriginalDropShipLink(line);
    if (originalDropShipLink == null)
      return (PX.Objects.SO.SupplyPOOrder) null;
    return PXResultset<PX.Objects.SO.SupplyPOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SupplyPOOrder>) this.SupplyPOOrders).SelectWindowed(0, 1, new object[2]
    {
      (object) originalDropShipLink.POOrderType,
      (object) originalDropShipLink.POOrderNbr
    }));
  }

  protected virtual void PrefetchWarnings()
  {
    if (this.soLineWarnings == null)
      this.soLineWarnings = new PurchaseSupplyBaseExt.SOLinePrefetchedWarnings();
    PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.orderType, Equal<BqlField<PX.Objects.SO.SOOrder.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.orderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pOCreate, IBqlBool>.IsEqual<True>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pONbr, IBqlString>.IsNotNull>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.completed, IBqlBool>.IsNotEqual<True>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.pOCancelled, Equal<True>>>>>.Or<BqlOperand<PX.Objects.SO.SOLineSplit.pOCompleted, IBqlBool>.IsEqual<True>>>>.Order<By<Asc<PX.Objects.SO.SOLineSplit.orderType>, Asc<PX.Objects.SO.SOLineSplit.orderNbr>, Asc<PX.Objects.SO.SOLineSplit.lineNbr>>>>.ReadOnly readOnly = new PXViewOf<PX.Objects.SO.SOLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.SOLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.orderType, Equal<BqlField<PX.Objects.SO.SOOrder.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.orderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pOCreate, IBqlBool>.IsEqual<True>>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.pONbr, IBqlString>.IsNotNull>>, And<BqlOperand<PX.Objects.SO.SOLineSplit.completed, IBqlBool>.IsNotEqual<True>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLineSplit.pOCancelled, Equal<True>>>>>.Or<BqlOperand<PX.Objects.SO.SOLineSplit.pOCompleted, IBqlBool>.IsEqual<True>>>>.Order<By<Asc<PX.Objects.SO.SOLineSplit.orderType>, Asc<PX.Objects.SO.SOLineSplit.orderNbr>, Asc<PX.Objects.SO.SOLineSplit.lineNbr>>>>.ReadOnly((PXGraph) this.Base);
    Type[] typeArray = new Type[7]
    {
      typeof (PX.Objects.SO.SOLineSplit.orderType),
      typeof (PX.Objects.SO.SOLineSplit.orderNbr),
      typeof (PX.Objects.SO.SOLineSplit.lineNbr),
      typeof (PX.Objects.SO.SOLineSplit.splitLineNbr),
      typeof (PX.Objects.SO.SOLineSplit.pONbr),
      typeof (PX.Objects.SO.SOLineSplit.pOCancelled),
      typeof (PX.Objects.SO.SOLineSplit.pOCompleted)
    };
    using (new PXFieldScope(((PXSelectBase) readOnly).View, typeArray))
    {
      IEnumerable<PX.Objects.SO.SOLineSplit> soLineSplits = GraphHelper.RowCast<PX.Objects.SO.SOLineSplit>((IEnumerable) ((PXSelectBase) readOnly).View.SelectMultiBound((object[]) new PX.Objects.SO.SOOrder[1]
      {
        ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current
      }, Array.Empty<object>()));
      PX.Objects.SO.SOLineSplit soLineSplit1 = (PX.Objects.SO.SOLineSplit) null;
      foreach (PX.Objects.SO.SOLineSplit soLineSplit2 in soLineSplits)
      {
        if (soLineSplit1 != null)
        {
          int? lineNbr1 = soLineSplit1.LineNbr;
          int? lineNbr2 = soLineSplit2.LineNbr;
          if (lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue)
            continue;
        }
        this.soLineWarnings.PrefetchLineWarning(new PX.Objects.SO.SOLine()
        {
          OrderType = soLineSplit2.OrderType,
          OrderNbr = soLineSplit2.OrderNbr,
          LineNbr = soLineSplit2.LineNbr
        }, (Exception) new PXSetPropertyException(soLineSplit2.POCancelled.GetValueOrDefault() ? "The linked {0} purchase order line has been canceled." : "The linked {0} purchase order line has been completed.", (PXErrorLevel) 3, new object[1]
        {
          (object) soLineSplit2.PONbr
        }));
        soLineSplit1 = soLineSplit2;
      }
    }
  }

  public sealed class SOLinePrefetchedWarnings
  {
    private Dictionary<(string, string, int?), Exception> prefetched = new Dictionary<(string, string, int?), Exception>();

    public void PrefetchLineWarning(PX.Objects.SO.SOLine line, Exception warning)
    {
      this.prefetched[this.GetKey(line)] = warning;
    }

    public void ShowLineWarning(PXCache cache, PX.Objects.SO.SOLine line)
    {
      Exception exception;
      if (!this.prefetched.TryGetValue(this.GetKey(line), out exception))
        return;
      cache.RaiseExceptionHandling<PX.Objects.SO.SOLine.lineNbr>((object) line, (object) null, exception);
    }

    private (string, string, int?) GetKey(PX.Objects.SO.SOLine line)
    {
      return (line.OrderType, line.OrderNbr, line.LineNbr);
    }
  }
}
