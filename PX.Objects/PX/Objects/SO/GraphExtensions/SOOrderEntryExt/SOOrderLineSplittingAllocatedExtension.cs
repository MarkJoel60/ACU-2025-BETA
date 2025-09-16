// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOOrderLineSplittingAllocatedExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.Common.Scopes;
using PX.Objects.IN;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

[PXProtectedAccess(typeof (SOOrderLineSplittingExtension))]
public abstract class SOOrderLineSplittingAllocatedExtension : 
  PXGraphExtension<SOOrderLineSplittingExtension, SOOrderEntry>
{
  protected PX.Objects.SO.SOOrder _LastSelected;

  protected virtual SOOrderItemAvailabilityExtension Availability
  {
    get
    {
      return ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).FindImplementation<SOOrderItemAvailabilityExtension>();
    }
  }

  public bool IsAllocationEntryEnabled
  {
    get
    {
      return ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).FindImplementation<SOOrderItemAvailabilityAllocatedExtension>().IsAllocationEntryEnabled;
    }
  }

  [PXProtectedAccess(null)]
  protected abstract Dictionary<PX.Objects.SO.SOLine, LSSelect.Counters> LineCounters { get; }

  [PXProtectedAccess(null)]
  protected abstract PXDBOperation CurrentOperation { get; }

  [PXProtectedAccess(null)]
  protected abstract PXCache<PX.Objects.SO.SOLine> LineCache { get; }

  [PXProtectedAccess(null)]
  protected abstract PXCache<PX.Objects.SO.SOLineSplit> SplitCache { get; }

  [PXProtectedAccess(null)]
  protected abstract IDisposable OperationModeScope(
    PXDBOperation alterCurrentOperation,
    bool restoreToNormal = false);

  [PXProtectedAccess(null)]
  protected abstract PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> ReadInventoryItem(
    int? inventoryID);

  [PXProtectedAccess(null)]
  protected abstract void SetSplitQtyWithLine(PX.Objects.SO.SOLineSplit split, PX.Objects.SO.SOLine line);

  [PXProtectedAccess(null)]
  protected abstract void SetLineQtyFromBase(PX.Objects.SO.SOLine line);

  [PXProtectedAccess(null)]
  protected abstract PX.Objects.SO.SOLine SelectLine(PX.Objects.SO.SOLineSplit split);

  [PXProtectedAccess(null)]
  protected abstract PX.Objects.SO.SOLineSplit[] SelectAllSplits(
    PX.Objects.SO.SOLine line,
    bool compareInventoryID = true);

  [PXProtectedAccess(null)]
  protected abstract PX.Objects.SO.SOLineSplit[] SelectSplits(
    PX.Objects.SO.SOLineSplit split,
    bool compareInventoryID = true);

  [PXProtectedAccess(null)]
  protected abstract PX.Objects.SO.SOLineSplit[] SelectSplitsReversed(PX.Objects.SO.SOLineSplit split);

  [PXProtectedAccess(null)]
  protected abstract PX.Objects.SO.SOLineSplit[] SelectSplitsReversed(
    PX.Objects.SO.SOLineSplit split,
    bool excludeCompleted = true);

  [PXProtectedAccess(null)]
  protected abstract PX.Objects.SO.SOLineSplit[] SelectSplitsReversedforTruncate(
    PX.Objects.SO.SOLineSplit split,
    bool excludeCompleted = true);

  [PXProtectedAccess(null)]
  protected abstract IDisposable InvtMultModeScope(PX.Objects.SO.SOLine line);

  [PXProtectedAccess(null)]
  protected abstract bool ForceLineSingleLotSerialPopulation(int? inventoryID);

  [PXOverride]
  public virtual void EventHandler(
    AbstractEvents.IRowSelected<PX.Objects.SO.SOOrder> e,
    Action<AbstractEvents.IRowSelected<PX.Objects.SO.SOOrder>> base_EventHandler)
  {
    base_EventHandler(e);
    if (this._LastSelected == null || this._LastSelected != e.Row)
    {
      PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLineSplit.shipDate>((PXCache) this.Base1.SplitCache, (object) null, this.IsAllocationEntryEnabled && !this.Base1.IsBlanketOrder);
      PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLineSplit.isAllocated>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLineSplit.completed>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLineSplit.shippedQty>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled && !this.Base1.IsBlanketOrder);
      PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLineSplit.shipmentNbr>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLineSplit.pOType>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLineSplit.pONbr>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLineSplit.pOReceiptNbr>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLineSplit.pOSource>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLineSplit.pOCreate>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLineSplit.receivedQty>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      PXUIFieldAttribute.SetVisible<PX.Objects.SO.SOLineSplit.refNoteID>((PXCache) this.SplitCache, (object) null, this.IsAllocationEntryEnabled);
      if (e.Row != null)
        this._LastSelected = e.Row;
    }
    if (!this.IsAllocationEntryEnabled)
      return;
    this.Base1.showSplits.SetEnabled(true);
  }

  [PXOverride]
  public virtual void EventHandlerInternal(
    AbstractEvents.IRowInserted<PX.Objects.SO.SOLine> e,
    Action<AbstractEvents.IRowInserted<PX.Objects.SO.SOLine>> base_EventHandlerInternal)
  {
    if (e.Row == null)
      return;
    if (this.IsSplitRequired(e.Row))
    {
      base_EventHandlerInternal(e);
    }
    else
    {
      ((IGenericEventWith<PXRowInsertedEventArgs>) e).Cache.SetValue<PX.Objects.SO.SOLine.locationID>((object) e.Row, (object) null);
      ((IGenericEventWith<PXRowInsertedEventArgs>) e).Cache.SetValue<PX.Objects.SO.SOLine.lotSerialNbr>((object) e.Row, (object) null);
      ((IGenericEventWith<PXRowInsertedEventArgs>) e).Cache.SetValue<PX.Objects.SO.SOLine.expireDate>((object) e.Row, (object) null);
      if (this.IsAllocationEntryEnabled && e.Row != null)
      {
        Decimal? baseOpenQty = e.Row.BaseOpenQty;
        Decimal num = 0M;
        if (!(baseOpenQty.GetValueOrDefault() == num & baseOpenQty.HasValue) && this.ReadInventoryItem(e.Row.InventoryID) != null && (EnumerableExtensions.IsIn<string>(e.Row.LineType, "GI", "GN") || this.Base1.IsBlanketOrder))
        {
          this.IssueAvailable(e.Row);
          this.Base1.UpdateParent(e.Row);
        }
      }
      this.Availability.Check((ILSMaster) e.Row, e.Row.CostCenterID);
    }
  }

  [PXOverride]
  public virtual void EventHandlerInternal(
    AbstractEvents.IRowUpdated<PX.Objects.SO.SOLine> e,
    Action<AbstractEvents.IRowUpdated<PX.Objects.SO.SOLine>> base_EventHandlerInternal)
  {
    if (e.Row == null)
      return;
    PX.Objects.IN.InventoryItem inventoryItem;
    if (this.IsSplitRequired(e.Row, out inventoryItem))
    {
      base_EventHandlerInternal(e);
      if (inventoryItem == null)
        return;
      bool? nullable = inventoryItem.KitItem;
      if (!nullable.GetValueOrDefault())
      {
        nullable = inventoryItem.StkItem;
        if (!nullable.GetValueOrDefault())
          return;
      }
      this.Availability.Check((ILSMaster) e.Row, e.Row.CostCenterID);
    }
    else
    {
      ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.SetValue<PX.Objects.SO.SOLine.locationID>((object) e.Row, (object) null);
      ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.SetValue<PX.Objects.SO.SOLine.lotSerialNbr>((object) e.Row, (object) null);
      ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.SetValue<PX.Objects.SO.SOLine.expireDate>((object) e.Row, (object) null);
      if (this.IsAllocationEntryEnabled)
      {
        PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(e.Row.InventoryID);
        int? siteId1;
        if (e.OldRow != null)
        {
          if (((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.ObjectsEqual<PX.Objects.SO.SOLine.inventoryID, PX.Objects.SO.SOLine.subItemID, PX.Objects.SO.SOLine.invtMult, PX.Objects.SO.SOLine.uOM, PX.Objects.SO.SOLine.projectID, PX.Objects.SO.SOLine.taskID, PX.Objects.SO.SOLine.costCenterID>((object) e.Row, (object) e.OldRow))
          {
            int? siteId2 = e.Row.SiteID;
            int? siteId3 = e.OldRow.SiteID;
            if (!(siteId2.GetValueOrDefault() == siteId3.GetValueOrDefault() & siteId2.HasValue == siteId3.HasValue))
            {
              if (!(e.Row.Operation == "I"))
              {
                siteId1 = e.OldRow.SiteID;
                if (siteId1.HasValue)
                  goto label_14;
              }
            }
            else
              goto label_14;
          }
          this.Base1.RaiseRowDeleted(e.OldRow);
          this.Base1.RaiseRowInserted(e.Row);
          goto label_50;
        }
label_14:
        if (e.OldRow != null)
        {
          siteId1 = e.Row.SiteID;
          int? siteId4 = e.OldRow.SiteID;
          if (!(siteId1.GetValueOrDefault() == siteId4.GetValueOrDefault() & siteId1.HasValue == siteId4.HasValue) && e.Row.Operation == "R")
          {
            foreach (PX.Objects.SO.SOLineSplit selectSplit in this.SelectSplits(PX.Objects.SO.SOLineSplit.FromSOLine(e.Row)))
            {
              selectSplit.SiteID = e.Row.SiteID;
              this.SplitCache.Update(selectSplit);
            }
            goto label_50;
          }
        }
        if (pxResult != null && (EnumerableExtensions.IsIn<string>(e.Row.LineType, "GI", "GN") || this.Base1.IsBlanketOrder))
        {
          Decimal? nullable1;
          if (((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).IsMobile)
          {
            nullable1 = e.Row.OrderQty;
            if (!nullable1.HasValue)
              e.Row.OrderQty = e.OldRow.OrderQty;
          }
          bool? nullable2;
          if (!((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.ObjectsEqual<PX.Objects.SO.SOLine.orderQty, PX.Objects.SO.SOLine.completed>((object) e.Row, (object) e.OldRow))
          {
            PX.Objects.SO.SOLine row1 = e.Row;
            PXCache cache = ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache;
            int? inventoryId = e.Row.InventoryID;
            string uom = e.Row.UOM;
            nullable1 = e.Row.OpenQty;
            Decimal num1 = nullable1.Value;
            Decimal? baseOpenQty1 = e.Row.BaseOpenQty;
            Decimal? nullable3 = new Decimal?(INUnitAttribute.ConvertToBase(cache, inventoryId, uom, num1, baseOpenQty1, INPrecision.QUANTITY));
            row1.BaseOpenQty = nullable3;
            nullable2 = e.Row.Completed;
            if (nullable2.GetValueOrDefault())
            {
              nullable2 = e.OldRow.Completed;
              bool flag = false;
              if (nullable2.GetValueOrDefault() == flag & nullable2.HasValue)
              {
                this.CompleteSchedules(e.Row);
                this.Base1.UpdateParent(e.Row);
                goto label_35;
              }
            }
            nullable2 = e.Row.Completed;
            bool flag1 = false;
            if (nullable2.GetValueOrDefault() == flag1 & nullable2.HasValue)
            {
              nullable2 = e.OldRow.Completed;
              if (nullable2.GetValueOrDefault())
              {
                this.UncompleteSchedules(e.Row);
                this.Base1.UpdateParent(e.Row);
                goto label_35;
              }
            }
            nullable1 = e.Row.BaseOpenQty;
            Decimal? baseOpenQty2 = e.OldRow.BaseOpenQty;
            if (nullable1.GetValueOrDefault() > baseOpenQty2.GetValueOrDefault() & nullable1.HasValue & baseOpenQty2.HasValue)
            {
              this.IssueAvailable(e.Row, new Decimal?(e.Row.BaseOpenQty.Value - e.OldRow.BaseOpenQty.Value));
              this.Base1.UpdateParent(e.Row);
            }
            else
            {
              Decimal? baseOpenQty3 = e.Row.BaseOpenQty;
              nullable1 = e.OldRow.BaseOpenQty;
              if (baseOpenQty3.GetValueOrDefault() < nullable1.GetValueOrDefault() & baseOpenQty3.HasValue & nullable1.HasValue)
              {
                PX.Objects.SO.SOLine row2 = e.Row;
                nullable1 = e.OldRow.BaseOpenQty;
                Decimal num2 = nullable1.Value;
                nullable1 = e.Row.BaseOpenQty;
                Decimal num3 = nullable1.Value;
                Decimal baseQty = num2 - num3;
                this.TruncateSchedules(row2, baseQty);
                this.Base1.UpdateParent(e.Row);
              }
            }
          }
label_35:
          if (!((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.ObjectsEqual<PX.Objects.SO.SOLine.pOCreate, PX.Objects.SO.SOLine.pOSource, PX.Objects.SO.SOLine.vendorID, PX.Objects.SO.SOLine.pOSiteID>((object) e.Row, (object) e.OldRow))
          {
            foreach (PX.Objects.SO.SOLineSplit selectSplit in this.SelectSplits(PX.Objects.SO.SOLineSplit.FromSOLine(e.Row)))
            {
              nullable2 = selectSplit.IsAllocated;
              bool flag2 = false;
              if (nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue)
              {
                nullable2 = selectSplit.Completed;
                bool flag3 = false;
                if (nullable2.GetValueOrDefault() == flag3 & nullable2.HasValue && selectSplit.PONbr == null)
                {
                  PX.Objects.SO.SOLineSplit copy = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(selectSplit);
                  copy.POCreate = e.Row.POCreate;
                  copy.POSource = e.Row.POSource;
                  copy.VendorID = e.Row.VendorID;
                  copy.POSiteID = e.Row.POSiteID;
                  this.SplitCache.Update(copy);
                }
              }
            }
          }
          DateTime? shipDate1 = e.Row.ShipDate;
          DateTime? shipDate2 = e.OldRow.ShipDate;
          if ((shipDate1.HasValue == shipDate2.HasValue ? (shipDate1.HasValue ? (shipDate1.GetValueOrDefault() != shipDate2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0 || e.Row.ShipComplete != e.OldRow.ShipComplete && e.Row.ShipComplete != "B")
          {
            foreach (PX.Objects.SO.SOLineSplit selectSplit in this.SelectSplits(PX.Objects.SO.SOLineSplit.FromSOLine(e.Row)))
            {
              selectSplit.ShipDate = e.Row.ShipDate;
              this.SplitCache.Update(selectSplit);
            }
          }
        }
      }
      else if (e.OldRow != null)
      {
        int? inventoryId1 = e.OldRow.InventoryID;
        int? inventoryId2 = e.Row.InventoryID;
        if (!(inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue))
          this.Base1.RaiseRowDeleted(e.OldRow);
      }
label_50:
      this.Availability.Check((ILSMaster) e.Row, e.Row.CostCenterID);
    }
  }

  protected virtual void SOLine_SiteID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    PX.Objects.SO.SOLine row = (PX.Objects.SO.SOLine) e.Row;
    if (!row.POCreated.GetValueOrDefault())
      return;
    int? newValue = e.NewValue as int?;
    int? siteId = row.SiteID;
    if (newValue.GetValueOrDefault() == siteId.GetValueOrDefault() & newValue.HasValue == siteId.HasValue)
      return;
    foreach (PX.Objects.SO.SOLineSplit selectSplit in this.SelectSplits(PX.Objects.SO.SOLineSplit.FromSOLine(row)))
    {
      if (selectSplit != null)
      {
        Decimal? receivedQty = selectSplit.ReceivedQty;
        Decimal num = 0M;
        if (receivedQty.GetValueOrDefault() > num & receivedQty.HasValue || selectSplit.PONbr != null)
        {
          PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, row.SiteID);
          e.NewValue = (object) inSite.SiteCD;
          throw new PXSetPropertyException<PX.Objects.SO.SOLine.siteID>("The warehouse cannot be changed because the line is linked to the {0} purchase order.", new object[1]
          {
            (object) selectSplit.PONbr
          });
        }
      }
    }
  }

  protected virtual void SOLineSplit_SiteID_Updated(
    PX.Data.Events.FieldUpdated<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit.siteID> e)
  {
    if (!(e.Row.Operation == "R"))
      return;
    e.Row.ToSiteID = e.Row.SiteID;
  }

  [PXOverride]
  public void EventHandler(
    AbstractEvents.IRowSelected<PX.Objects.SO.SOLineSplit> e,
    Action<AbstractEvents.IRowSelected<PX.Objects.SO.SOLineSplit>> base_EventHandler)
  {
    base_EventHandler(e);
    if (e.Row == null)
      return;
    PX.Objects.SO.SOLine line = PXParentAttribute.SelectParent<PX.Objects.SO.SOLine>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row);
    PXUIFieldAttribute.SetEnabled<PX.Objects.SO.SOLineSplit.isAllocated>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, this.AllowToManualAllocate(line, e.Row));
  }

  [PXOverride]
  public virtual void EventHandler(
    AbstractEvents.IRowInserting<PX.Objects.SO.SOLineSplit> e,
    Action<AbstractEvents.IRowInserting<PX.Objects.SO.SOLineSplit>> base_EventHandler)
  {
    base_EventHandler(e);
    if (this.Base1.IsLSEntryEnabled || !this.IsAllocationEntryEnabled)
      return;
    if (!e.ExternalCall && this.CurrentOperation == 1)
    {
      if (this.IsDropShipNotLegacy(e.Row))
        ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).FindImplementation<PurchaseSupplyBaseExt>().FillInsertingSchedule(((IGenericEventWith<PXRowInsertingEventArgs>) e).Cache, e.Row);
      if (this.TryUpdateSameSchedule(((IGenericEventWith<PXRowInsertingEventArgs>) e).Cache, e.Row) != null)
      {
        ((ICancelEventArgs) e).Cancel = true;
        PXCacheEx.Adjust<PXLineNbrAttribute>(((IGenericEventWith<PXRowInsertingEventArgs>) e).Cache, (object) null).For<PX.Objects.SO.SOLineSplit.splitLineNbr>((Action<PXLineNbrAttribute>) (a => a.ClearLastDefaultValue()));
      }
    }
    if (e.Row.InventoryID.HasValue && !string.IsNullOrEmpty(e.Row.UOM))
      return;
    ((ICancelEventArgs) e).Cancel = true;
    PXCacheEx.Adjust<PXLineNbrAttribute>(((IGenericEventWith<PXRowInsertingEventArgs>) e).Cache, (object) null).For<PX.Objects.SO.SOLineSplit.splitLineNbr>((Action<PXLineNbrAttribute>) (a => a.ClearLastDefaultValue()));
  }

  [PXOverride]
  public virtual void EventHandler(
    AbstractEvents.IRowInserted<PX.Objects.SO.SOLineSplit> e,
    Action<AbstractEvents.IRowInserted<PX.Objects.SO.SOLineSplit>> base_EventHandler)
  {
    base_EventHandler(e);
    if (!this.IsAllocationEntryEnabled || (this.Base1.SuppressedMode || !e.Row.IsAllocated.GetValueOrDefault()) && (string.IsNullOrEmpty(e.Row.LotSerialNbr) || e.Row.IsAllocated.GetValueOrDefault()))
      return;
    this.AllocatedUpdated(e.Row, e.ExternalCall);
    ((IGenericEventWith<PXRowInsertedEventArgs>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLineSplit.qty>((object) e.Row, (object) null, (Exception) null);
    this.Availability.Check((ILSMaster) e.Row, e.Row.CostCenterID);
  }

  [PXOverride]
  public virtual void EventHandler(
    AbstractEvents.IRowUpdated<PX.Objects.SO.SOLineSplit> e,
    Action<AbstractEvents.IRowUpdated<PX.Objects.SO.SOLineSplit>> base_EventHandler)
  {
    base_EventHandler(e);
    if (this.Base1.SuppressedMode || !this.IsAllocationEntryEnabled)
      return;
    bool? isAllocated1 = e.Row.IsAllocated;
    bool? isAllocated2 = e.OldRow.IsAllocated;
    int? nullable;
    bool? isAllocated3;
    if (isAllocated1.GetValueOrDefault() == isAllocated2.GetValueOrDefault() & isAllocated1.HasValue == isAllocated2.HasValue)
    {
      nullable = e.Row.POLineNbr;
      int? poLineNbr = e.OldRow.POLineNbr;
      if (!(nullable.GetValueOrDefault() == poLineNbr.GetValueOrDefault() & nullable.HasValue == poLineNbr.HasValue) && !e.Row.POLineNbr.HasValue)
      {
        isAllocated3 = e.Row.IsAllocated;
        bool flag = false;
        if (!(isAllocated3.GetValueOrDefault() == flag & isAllocated3.HasValue))
          goto label_11;
      }
      else
        goto label_11;
    }
    isAllocated3 = e.Row.IsAllocated;
    if (isAllocated3.GetValueOrDefault())
    {
      this.AllocatedUpdated(e.Row, e.ExternalCall);
      ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLineSplit.qty>((object) e.Row, (object) null, (Exception) null);
      this.Availability.Check((ILSMaster) e.Row, e.Row.CostCenterID);
    }
    else
    {
      PX.Objects.SO.SOLineSplit copy = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(e.Row);
      e.Row.ClearSOReferences();
      e.Row.LotSerialNbr = (string) null;
      using (this.Base1.SuppressedModeScope(true))
      {
        ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.RaiseRowUpdated((object) e.Row, (object) copy);
        this.MergeSplitsWhenDeallocated(e.Row);
      }
    }
label_11:
    if (string.Equals(e.Row.LotSerialNbr, e.OldRow.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
      return;
    if (e.Row.LotSerialNbr != null)
    {
      this.LotSerialNbrUpdated(e.Row, e.ExternalCall);
      ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.RaiseExceptionHandling<PX.Objects.SO.SOLineSplit.qty>((object) e.Row, (object) null, (Exception) null);
      this.Availability.Check((ILSMaster) e.Row, e.Row.CostCenterID);
    }
    else
    {
      foreach (PX.Objects.SO.SOLineSplit a in this.SelectSplitsReversed(e.Row))
      {
        int? splitLineNbr = a.SplitLineNbr;
        nullable = e.Row.SplitLineNbr;
        if (splitLineNbr.GetValueOrDefault() == nullable.GetValueOrDefault() & splitLineNbr.HasValue == nullable.HasValue && this.SchedulesEqual(a, e.Row, (PXDBOperation) 1))
        {
          PX.Objects.SO.SOLineSplit copy = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(a);
          e.Row.IsAllocated = new bool?(false);
          ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.RaiseFieldUpdated<PX.Objects.SO.SOLineSplit.isAllocated>((object) e.Row, (object) e.Row.IsAllocated);
          ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.RaiseRowUpdated((object) a, (object) copy);
        }
      }
    }
  }

  private void MergeSplitsWhenDeallocated(PX.Objects.SO.SOLineSplit mainSplit)
  {
    if (!this.MergeEqualSplits(mainSplit))
      return;
    PX.Objects.SO.SOLine line = this.SelectLine(mainSplit);
    if (line == null || string.Equals(mainSplit.UOM, line.UOM, StringComparison.OrdinalIgnoreCase))
      return;
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(mainSplit.InventoryID);
    INUnit unit = INUnit.UK.ByInventory.Find((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, mainSplit.InventoryID, line.UOM);
    if (this.Base1.UseBaseUnitInSplit(mainSplit, line, pxResult) || !this.UseBaseUnitRemainder(PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult), unit) || !string.Equals(mainSplit.UOM, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).BaseUnit, StringComparison.OrdinalIgnoreCase))
      return;
    Decimal? qty = mainSplit.Qty;
    Decimal? unitRate = unit.UnitRate;
    Decimal? nullable = qty.HasValue & unitRate.HasValue ? new Decimal?(qty.GetValueOrDefault() % unitRate.GetValueOrDefault()) : new Decimal?();
    Decimal num = 0M;
    if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
      return;
    PX.Objects.SO.SOLineSplit copy = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(mainSplit);
    mainSplit.UOM = line.UOM;
    this.SetSplitQtyWithLine(mainSplit, line);
    ((PXCache) this.SplitCache).RaiseFieldUpdated<PX.Objects.SO.SOLineSplit.uOM>((object) mainSplit, (object) copy.UOM);
    ((PXCache) this.SplitCache).RaiseFieldUpdated<PX.Objects.SO.SOLineSplit.qty>((object) mainSplit, (object) copy.Qty);
    this.SplitCache.RaiseRowUpdated(mainSplit, copy);
    this.MergeEqualSplits(mainSplit);
  }

  private bool MergeEqualSplits(PX.Objects.SO.SOLineSplit mainSplit)
  {
    bool flag = false;
    foreach (PX.Objects.SO.SOLineSplit soLineSplit1 in ((IEnumerable<PX.Objects.SO.SOLineSplit>) this.SelectSplitsReversed(mainSplit)).Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (siblingSplit =>
    {
      int? splitLineNbr1 = siblingSplit.SplitLineNbr;
      int? splitLineNbr2 = mainSplit.SplitLineNbr;
      return !(splitLineNbr1.GetValueOrDefault() == splitLineNbr2.GetValueOrDefault() & splitLineNbr1.HasValue == splitLineNbr2.HasValue) && this.SchedulesEqual(siblingSplit, mainSplit, (PXDBOperation) 1);
    })))
    {
      PX.Objects.SO.SOLineSplit copy = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(mainSplit);
      PX.Objects.SO.SOLineSplit soLineSplit2 = mainSplit;
      Decimal? baseQty1 = soLineSplit2.BaseQty;
      Decimal? baseQty2 = soLineSplit1.BaseQty;
      soLineSplit2.BaseQty = baseQty1.HasValue & baseQty2.HasValue ? new Decimal?(baseQty1.GetValueOrDefault() + baseQty2.GetValueOrDefault()) : new Decimal?();
      this.SetSplitQtyWithLine(mainSplit, (PX.Objects.SO.SOLine) null);
      this.SplitCache.Delete(soLineSplit1);
      ((PXCache) this.SplitCache).RaiseFieldUpdated<PX.Objects.SO.SOLineSplit.qty>((object) mainSplit, (object) copy.Qty);
      this.SplitCache.RaiseRowUpdated(mainSplit, copy);
      this.RefreshViewOf((PXCache) this.SplitCache);
      flag = true;
    }
    return flag;
  }

  [PXOverride]
  public virtual void EventHandlerQty(
    AbstractEvents.IFieldVerifying<PX.Objects.SO.SOLineSplit, IBqlField, Decimal?> e,
    Action<AbstractEvents.IFieldVerifying<PX.Objects.SO.SOLineSplit, IBqlField, Decimal?>> base_EventHandlerQty)
  {
    if (this.IsAllocationEntryEnabled)
      e.NewValue = this.Base1.VerifySNQuantity(((IGenericEventWith<PXFieldVerifyingEventArgs>) e).Cache, (ILSMaster) e.Row, e.NewValue, "qty");
    else
      base_EventHandlerQty(e);
  }

  [PXOverride]
  public virtual void EventHandlerUOM(
    AbstractEvents.IFieldDefaulting<PX.Objects.SO.SOLineSplit, IBqlField, string> e,
    Action<AbstractEvents.IFieldDefaulting<PX.Objects.SO.SOLineSplit, IBqlField, string>> base_EventHandlerUOM)
  {
    if (!this.IsAllocationEntryEnabled)
    {
      base_EventHandlerUOM(e);
    }
    else
    {
      PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(e.Row.InventoryID);
      if (pxResult == null || !(PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerTrack == "S"))
        return;
      PX.Objects.SO.SOLine current = ((PXSelectBase<PX.Objects.SO.SOLine>) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Current;
      if ((current != null ? (!current.IsSpecialOrder.GetValueOrDefault() ? 1 : 0) : 1) == 0)
        return;
      e.NewValue = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).BaseUnit;
      ((ICancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual bool AllocatedUpdated(PX.Objects.SO.SOLineSplit split, bool externalCall)
  {
    INLotSerClass lotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(this.ReadInventoryItem(split.InventoryID));
    string tranType = split.TranType;
    short? invtMult = split.InvtMult;
    int? invMult = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    if (INLotSerialNbrAttribute.IsTrack(lotSerClass, tranType, invMult) && split.LotSerialNbr != null)
    {
      this.LotSerialNbrUpdated(split, externalCall);
      return true;
    }
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter1 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter();
    statusByCostCenter1.InventoryID = split.InventoryID;
    statusByCostCenter1.SiteID = split.SiteID;
    statusByCostCenter1.SubItemID = split.SubItemID;
    statusByCostCenter1.CostCenterID = split.CostCenterID;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter copy = PXCache<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>.CreateCopy(PXCache<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>.Insert((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, statusByCostCenter1));
    INSiteStatusByCostCenter statusByCostCenter2 = INSiteStatusByCostCenter.PK.Find((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, split.InventoryID, split.SubItemID, split.SiteID, split.CostCenterID);
    Decimal? nullable1;
    if (statusByCostCenter2 != null)
    {
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter3 = copy;
      nullable1 = statusByCostCenter3.QtyAvail;
      Decimal? nullable2 = statusByCostCenter2.QtyAvail;
      statusByCostCenter3.QtyAvail = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter4 = copy;
      nullable2 = statusByCostCenter4.QtyHardAvail;
      nullable1 = statusByCostCenter2.QtyHardAvail;
      statusByCostCenter4.QtyHardAvail = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    }
    nullable1 = copy.QtyHardAvail;
    Decimal num = 0M;
    if (!(nullable1.GetValueOrDefault() < num & nullable1.HasValue))
      return false;
    this.BreakupAllocatedSplit(split, copy.QtyHardAvail, false);
    return true;
  }

  protected virtual bool UseBaseUnitRemainder(PX.Objects.IN.InventoryItem item, INUnit unit)
  {
    if (!this.Base1.IsBlanketOrder)
    {
      bool? decimalSalesUnit = item.DecimalSalesUnit;
      bool flag = false;
      if (decimalSalesUnit.GetValueOrDefault() == flag & decimalSalesUnit.HasValue && unit?.UnitMultDiv == "M")
      {
        Decimal? unitRate = unit.UnitRate;
        Decimal num = 1M;
        return unitRate.GetValueOrDefault() > num & unitRate.HasValue;
      }
    }
    return false;
  }

  public virtual void BreakupAllocatedSplit(
    PX.Objects.SO.SOLineSplit split,
    Decimal? negQtyHardAvail,
    bool lotSerialUpdate,
    PX.Objects.SO.SOLineSplit splitCopy = null)
  {
    if (FlaggedModeScopeBase<Blanket.ChildOrderCreationFromBlanketScope>.IsActive)
      return;
    PX.Objects.SO.SOLineSplit copy1 = splitCopy ?? PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(split);
    Decimal? nullable1 = split.BaseQty;
    Decimal? nullable2 = negQtyHardAvail;
    Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    nullable2 = nullable3;
    Decimal num1 = 0M;
    if (nullable2.GetValueOrDefault() <= num1 & nullable2.HasValue)
    {
      split.IsAllocated = new bool?(false);
      if (!lotSerialUpdate)
        ((PXCache) this.SplitCache).RaiseExceptionHandling<PX.Objects.SO.SOLineSplit.isAllocated>((object) split, (object) true, (Exception) new PXSetPropertyException<PX.Objects.SO.SOLineSplit.isAllocated>("Inventory quantity will go negative."));
      ((PXCache) this.SplitCache).RaiseFieldUpdated<PX.Objects.SO.SOLineSplit.isAllocated>((object) split, (object) copy1.IsAllocated);
      if (lotSerialUpdate)
      {
        split.LotSerialNbr = (string) null;
        this.UpdateParentAfterLotSerialNbrCleared(split);
        ((PXCache) this.SplitCache).RaiseExceptionHandling<PX.Objects.SO.SOLineSplit.lotSerialNbr>((object) split, (object) null, (Exception) new PXSetPropertyException<PX.Objects.SO.SOLineSplit.lotSerialNbr>("Inventory quantity will go negative."));
        ((PXCache) this.SplitCache).RaiseFieldUpdated<PX.Objects.SO.SOLineSplit.lotSerialNbr>((object) split, (object) copy1.LotSerialNbr);
      }
      using (this.Base1.SuppressedModeScope(true))
        this.SplitCache.RaiseRowUpdated(split, copy1);
    }
    else
    {
      nullable2 = negQtyHardAvail;
      Decimal? nullable4;
      if (!nullable2.HasValue)
      {
        nullable1 = new Decimal?();
        nullable4 = nullable1;
      }
      else
        nullable4 = new Decimal?(-nullable2.GetValueOrDefault());
      Decimal? baseQty1 = nullable4;
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(this.ReadInventoryItem(split.InventoryID));
      INUnit unit = INUnit.UK.ByInventory.Find((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, split.InventoryID, split.UOM);
      Decimal? nullable5;
      int num2;
      if (!string.Equals(split.UOM, inventoryItem.BaseUnit, StringComparison.OrdinalIgnoreCase) && this.UseBaseUnitRemainder(inventoryItem, unit))
      {
        nullable1 = nullable3;
        nullable5 = unit.UnitRate;
        nullable2 = nullable1.HasValue & nullable5.HasValue ? new Decimal?(nullable1.GetValueOrDefault() % nullable5.GetValueOrDefault()) : new Decimal?();
        Decimal num3 = 0M;
        num2 = !(nullable2.GetValueOrDefault() == num3 & nullable2.HasValue) ? 1 : 0;
      }
      else
        num2 = 0;
      if (num2 != 0)
      {
        nullable2 = nullable3;
        nullable5 = unit.UnitRate;
        Decimal? nullable6;
        if (!(nullable2.HasValue & nullable5.HasValue))
        {
          nullable1 = new Decimal?();
          nullable6 = nullable1;
        }
        else
          nullable6 = new Decimal?(nullable2.GetValueOrDefault() / nullable5.GetValueOrDefault());
        nullable1 = nullable6;
        Decimal num4 = Math.Floor(nullable1.GetValueOrDefault());
        if (num4 == 0M)
        {
          split.UOM = inventoryItem.BaseUnit;
          split.BaseQty = nullable3;
        }
        else
        {
          PX.Objects.SO.SOLineSplit soLineSplit = split;
          Decimal num5 = num4;
          nullable2 = unit.UnitRate;
          Decimal? nullable7;
          if (!nullable2.HasValue)
          {
            nullable5 = new Decimal?();
            nullable7 = nullable5;
          }
          else
            nullable7 = new Decimal?(num5 * nullable2.GetValueOrDefault());
          soLineSplit.BaseQty = nullable7;
        }
        this.SetSplitQtyWithLine(split, (PX.Objects.SO.SOLine) null);
        ((PXCache) this.SplitCache).RaiseFieldUpdated<PX.Objects.SO.SOLineSplit.qty>((object) split, (object) copy1.Qty);
        ((PXCache) this.SplitCache).RaiseFieldUpdated<PX.Objects.SO.SOLineSplit.isAllocated>((object) split, (object) copy1.IsAllocated);
        using (this.Base1.SuppressedModeScope(true))
        {
          this.SplitCache.RaiseRowUpdated(split, copy1);
          nullable2 = nullable3;
          nullable5 = split.BaseQty;
          Decimal? nullable8;
          if (!(nullable2.HasValue & nullable5.HasValue))
          {
            nullable1 = new Decimal?();
            nullable8 = nullable1;
          }
          else
            nullable8 = new Decimal?(nullable2.GetValueOrDefault() - nullable5.GetValueOrDefault());
          Decimal? baseQty2 = nullable8;
          nullable5 = baseQty2;
          Decimal num6 = 0M;
          if (!(nullable5.GetValueOrDefault() == num6 & nullable5.HasValue))
          {
            PX.Objects.SO.SOLineSplit copy2 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(copy1);
            copy2.UOM = inventoryItem.BaseUnit;
            this.InsertAllocationRemainder(copy2, baseQty2, true);
          }
          nullable5 = baseQty1;
          nullable2 = unit.UnitRate;
          Decimal? nullable9;
          if (!(nullable5.HasValue & nullable2.HasValue))
          {
            nullable1 = new Decimal?();
            nullable9 = nullable1;
          }
          else
            nullable9 = new Decimal?(nullable5.GetValueOrDefault() / nullable2.GetValueOrDefault());
          nullable1 = nullable9;
          Decimal num7 = Math.Floor(nullable1.GetValueOrDefault());
          if (num7 != 0M)
          {
            PX.Objects.SO.SOLineSplit copy3 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(copy1);
            if (lotSerialUpdate)
              copy3.LotSerialNbr = (string) null;
            PX.Objects.SO.SOLineSplit copy4 = copy3;
            Decimal num8 = num7;
            nullable5 = unit.UnitRate;
            Decimal? baseQty3;
            if (!nullable5.HasValue)
            {
              nullable2 = new Decimal?();
              baseQty3 = nullable2;
            }
            else
              baseQty3 = new Decimal?(num8 * nullable5.GetValueOrDefault());
            this.InsertAllocationRemainder(copy4, baseQty3, false);
          }
          nullable5 = baseQty1;
          Decimal num9 = num7;
          nullable1 = unit.UnitRate;
          nullable2 = nullable1.HasValue ? new Decimal?(num9 * nullable1.GetValueOrDefault()) : new Decimal?();
          Decimal? nullable10;
          if (!(nullable5.HasValue & nullable2.HasValue))
          {
            nullable1 = new Decimal?();
            nullable10 = nullable1;
          }
          else
            nullable10 = new Decimal?(nullable5.GetValueOrDefault() - nullable2.GetValueOrDefault());
          Decimal? baseQty4 = nullable10;
          nullable2 = baseQty4;
          Decimal num10 = 0M;
          if (!(nullable2.GetValueOrDefault() == num10 & nullable2.HasValue))
          {
            PX.Objects.SO.SOLineSplit copy5 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(copy1);
            copy5.UOM = inventoryItem.BaseUnit;
            if (lotSerialUpdate)
              copy5.LotSerialNbr = (string) null;
            this.InsertAllocationRemainder(copy5, baseQty4, false);
          }
        }
      }
      else
      {
        split.BaseQty = nullable3;
        this.SetSplitQtyWithLine(split, (PX.Objects.SO.SOLine) null);
        ((PXCache) this.SplitCache).RaiseFieldUpdated<PX.Objects.SO.SOLineSplit.qty>((object) split, (object) copy1.Qty);
        ((PXCache) this.SplitCache).RaiseFieldUpdated<PX.Objects.SO.SOLineSplit.isAllocated>((object) split, (object) copy1.IsAllocated);
        using (this.Base1.SuppressedModeScope(true))
        {
          this.SplitCache.RaiseRowUpdated(split, copy1);
          if (split.IsAllocated.GetValueOrDefault())
          {
            if (lotSerialUpdate)
              copy1.LotSerialNbr = (string) null;
            this.InsertAllocationRemainder(copy1, baseQty1, false);
          }
        }
      }
      this.RefreshViewOf((PXCache) this.SplitCache);
    }
  }

  protected virtual bool LotSerialNbrUpdated(PX.Objects.SO.SOLineSplit split, bool externalCall)
  {
    SiteLotSerial siteLotSerial1 = new SiteLotSerial();
    siteLotSerial1.InventoryID = split.InventoryID;
    siteLotSerial1.SiteID = split.SiteID;
    siteLotSerial1.LotSerialNbr = split.LotSerialNbr;
    SiteLotSerial copy1 = PXCache<SiteLotSerial>.CreateCopy(PXCache<SiteLotSerial>.Insert((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, siteLotSerial1));
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(split.InventoryID);
    INSiteLotSerial inSiteLotSerial = INSiteLotSerial.PK.Find((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, split.InventoryID, split.SiteID, split.LotSerialNbr);
    Decimal? nullable1;
    Decimal? nullable2;
    if (inSiteLotSerial != null)
    {
      SiteLotSerial siteLotSerial2 = copy1;
      nullable1 = siteLotSerial2.QtyAvail;
      Decimal? qtyAvail = inSiteLotSerial.QtyAvail;
      siteLotSerial2.QtyAvail = nullable1.HasValue & qtyAvail.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + qtyAvail.GetValueOrDefault()) : new Decimal?();
      SiteLotSerial siteLotSerial3 = copy1;
      nullable2 = siteLotSerial3.QtyHardAvail;
      nullable1 = inSiteLotSerial.QtyHardAvail;
      siteLotSerial3.QtyHardAvail = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    }
    INLotSerClass lotSerClass1 = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult);
    string tranType1 = split.TranType;
    short? invtMult = split.InvtMult;
    int? invMult1 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    if (INLotSerialNbrAttribute.IsTrackSerial(lotSerClass1, tranType1, invMult1) && split.LotSerialNbr != null)
    {
      PX.Objects.SO.SOLineSplit copy2 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(split);
      if (inSiteLotSerial != null)
      {
        nullable1 = inSiteLotSerial.QtyAvail;
        Decimal num1 = 0M;
        if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue)
        {
          nullable1 = inSiteLotSerial.QtyHardAvail;
          Decimal num2 = 0M;
          if (nullable1.GetValueOrDefault() > num2 & nullable1.HasValue)
          {
            if (split.Operation != "R")
            {
              split.BaseQty = new Decimal?((Decimal) 1);
              this.SetSplitQtyWithLine(split, (PX.Objects.SO.SOLine) null);
              split.IsAllocated = new bool?(true);
              goto label_14;
            }
            split.IsAllocated = new bool?(false);
            ((PXCache) this.SplitCache).RaiseExceptionHandling<PX.Objects.SO.SOLineSplit.lotSerialNbr>((object) split, (object) null, (Exception) new PXSetPropertyException(PXMessages.LocalizeFormatNoPrefixNLA("Serial Number '{1}' for item '{0}' is already received.", new object[2]
            {
              (object) PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).InventoryCD,
              (object) split.LotSerialNbr
            })));
            goto label_14;
          }
        }
      }
      bool? isAllocated;
      if (split.Operation != "R")
      {
        if (externalCall)
        {
          split.IsAllocated = new bool?(false);
          split.LotSerialNbr = (string) null;
          this.UpdateParentAfterLotSerialNbrCleared(split);
          ((PXCache) this.SplitCache).RaiseExceptionHandling<PX.Objects.SO.SOLineSplit.lotSerialNbr>((object) split, (object) null, (Exception) new PXSetPropertyException("Inventory quantity will go negative."));
          isAllocated = split.IsAllocated;
          if (isAllocated.GetValueOrDefault())
            return false;
        }
      }
      else
      {
        split.BaseQty = new Decimal?((Decimal) 1);
        this.SetSplitQtyWithLine(split, (PX.Objects.SO.SOLine) null);
      }
label_14:
      ((PXCache) this.SplitCache).RaiseFieldUpdated<PX.Objects.SO.SOLineSplit.qty>((object) split, (object) copy2.Qty);
      ((PXCache) this.SplitCache).RaiseFieldUpdated<PX.Objects.SO.SOLineSplit.isAllocated>((object) split, (object) copy2.IsAllocated);
      nullable1 = copy2.BaseQty;
      Decimal num = 1M;
      bool suppress = nullable1.GetValueOrDefault() > num & nullable1.HasValue;
      using (this.Base1.SuppressedModeScope(suppress))
      {
        this.SplitCache.RaiseRowUpdated(split, copy2);
        if (suppress)
        {
          isAllocated = split.IsAllocated;
          if (!isAllocated.GetValueOrDefault())
          {
            isAllocated = split.IsAllocated;
            if (!isAllocated.GetValueOrDefault())
            {
              if (!(split.Operation == "R"))
                goto label_26;
            }
            else
              goto label_26;
          }
          copy2.LotSerialNbr = (string) null;
          PX.Objects.SO.SOLineSplit copy3 = copy2;
          nullable1 = copy2.BaseQty;
          nullable2 = split.BaseQty;
          Decimal? baseQty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
          PX.Objects.SO.SOLineSplit soLineSplit = this.InsertAllocationRemainder(copy3, baseQty, false);
          if (this.Base1.IsLotSerialRequired)
          {
            PX.Objects.SO.SOLine line = PXParentAttribute.SelectParent<PX.Objects.SO.SOLine>((PXCache) this.SplitCache, (object) soLineSplit);
            if (line != null)
            {
              if (this.IsLotSerialItem((ILSMaster) line))
              {
                PX.Objects.SO.SOLine soLine = line;
                nullable2 = soLine.UnassignedQty;
                nullable1 = soLineSplit.BaseQty;
                soLine.UnassignedQty = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
                GraphHelper.MarkUpdated((PXCache) this.LineCache, (object) line, true);
              }
            }
          }
        }
      }
label_26:
      if (suppress)
      {
        this.RefreshViewOf((PXCache) this.SplitCache);
        return true;
      }
    }
    else
    {
      INLotSerClass lotSerClass2 = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult);
      string tranType2 = split.TranType;
      invtMult = split.InvtMult;
      int? invMult2 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
      if (INLotSerialNbrAttribute.IsTrack(lotSerClass2, tranType2, invMult2) && split.LotSerialNbr != null)
      {
        INLotSerClass lotSerClass3 = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult);
        string tranType3 = split.TranType;
        invtMult = split.InvtMult;
        int? invMult3 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
        if (!INLotSerialNbrAttribute.IsTrackSerial(lotSerClass3, tranType3, invMult3))
        {
          nullable1 = split.BaseQty;
          Decimal num3 = 0M;
          if (nullable1.GetValueOrDefault() > num3 & nullable1.HasValue)
          {
            bool? isAllocated = split.IsAllocated;
            bool flag1 = false;
            if (isAllocated.GetValueOrDefault() == flag1 & isAllocated.HasValue)
            {
              if (inSiteLotSerial != null)
              {
                nullable1 = inSiteLotSerial.QtyHardAvail;
                Decimal num4 = 0M;
                if (!(nullable1.GetValueOrDefault() <= num4 & nullable1.HasValue))
                {
                  nullable1 = copy1.QtyHardAvail;
                  Decimal num5 = 0M;
                  if (!(nullable1.GetValueOrDefault() <= num5 & nullable1.HasValue))
                    goto label_43;
                }
              }
              if (split.Operation != "R")
              {
                if (externalCall)
                {
                  PX.Objects.SO.SOLineSplit copy4 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(split);
                  bool flag2 = this.NegativeInventoryError(split);
                  using (this.Base1.SuppressedModeScope(true))
                    this.SplitCache.RaiseRowUpdated(split, copy4);
                  return flag2;
                }
                goto label_44;
              }
label_43:
              PX.Objects.SO.SOLineSplit copy5 = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(split);
              split.IsAllocated = new bool?(true);
              ((PXCache) this.SplitCache).RaiseFieldUpdated<PX.Objects.SO.SOLineSplit.isAllocated>((object) split, (object) copy5.IsAllocated);
              this.SplitCache.RaiseRowUpdated(split, copy5);
label_44:
              return true;
            }
            if (split.IsAllocated.GetValueOrDefault())
            {
              if (split.Operation != "R")
              {
                nullable1 = copy1.QtyHardAvail;
                Decimal num6 = 0M;
                if (nullable1.GetValueOrDefault() < num6 & nullable1.HasValue)
                  this.BreakupAllocatedSplit(split, copy1.QtyHardAvail, true);
              }
              return true;
            }
          }
        }
      }
    }
    return false;
  }

  private void UpdateParentAfterLotSerialNbrCleared(PX.Objects.SO.SOLineSplit split)
  {
    if (!this.ForceLineSingleLotSerialPopulation(split.InventoryID))
      return;
    PX.Objects.SO.SOLine soLine = this.SelectLine(split);
    if (soLine == null)
      return;
    this.LineCounters.Remove(soLine);
    this.Base1.UpdateParent(soLine);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOOrderLineSplittingExtension.ShowSplits(PX.Data.PXAdapter)" />
  /// </summary>
  [PXOverride]
  public virtual IEnumerable ShowSplits(
    PXAdapter adapter,
    Func<PXAdapter, IEnumerable> base_ShowSplits)
  {
    PX.Objects.SO.SOLine lineCurrent = this.Base1.LineCurrent;
    if (this.IsAllocationEntryEnabled && lineCurrent != null)
    {
      bool? nullable = lineCurrent.POCreate;
      if (nullable.GetValueOrDefault())
      {
        nullable = lineCurrent.IsLegacyDropShip;
        if (!nullable.GetValueOrDefault() && EnumerableExtensions.IsIn<string>(lineCurrent.POSource, "D", "L") && !this.IsLotSerialsAllowedForDropShipLine(lineCurrent))
          throw new PXSetPropertyException("The Line Details dialog box cannot be opened because the line with the {0} item is marked for drop-shipping.", new object[1]
          {
            (object) PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, lineCurrent.InventoryID).InventoryCD
          });
      }
    }
    return base_ShowSplits(adapter);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.LineSplittingExtension`4.UpdateParent(`2)" />
  /// </summary>
  [PXOverride]
  public virtual void UpdateParent(PX.Objects.SO.SOLine line, Action<PX.Objects.SO.SOLine> base_UpdateParent)
  {
    if (line != null && line.RequireShipping.GetValueOrDefault() && !this.IsSplitRequired(line))
      this.Base1.UpdateParent(line, (PX.Objects.SO.SOLineSplit) null, (PX.Objects.SO.SOLineSplit) null, out Decimal _);
    else
      base_UpdateParent(line);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.LineSplittingExtension`4.UpdateParent(`3,`3)" />
  /// </summary>
  [PXOverride]
  public virtual PX.Objects.SO.SOLine UpdateParent(
    PX.Objects.SO.SOLineSplit newSplit,
    PX.Objects.SO.SOLineSplit oldSplit,
    Func<PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLineSplit, PX.Objects.SO.SOLine> base_UpdateParent)
  {
    PX.Objects.SO.SOLineSplit row = newSplit ?? oldSplit;
    PX.Objects.SO.SOLine line1 = (PX.Objects.SO.SOLine) LSParentAttribute.SelectParent((PXCache) this.SplitCache, (object) row, typeof (PX.Objects.SO.SOLine));
    if (line1 != null)
    {
      bool? nullable1 = line1.RequireShipping;
      if (nullable1.GetValueOrDefault())
      {
        if (row != null)
        {
          int? inventoryId1 = row.InventoryID;
          int? inventoryId2 = line1.InventoryID;
          if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
          {
            PX.Objects.SO.SOLine copy = PXCache<PX.Objects.SO.SOLine>.CreateCopy(line1);
            SOOrderLineSplittingExtension base1 = this.Base1;
            PX.Objects.SO.SOLine line2 = line1;
            PX.Objects.SO.SOLineSplit newSplit1;
            if (newSplit != null)
            {
              nullable1 = newSplit.Completed;
              bool flag = false;
              if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
              {
                newSplit1 = newSplit;
                goto label_8;
              }
            }
            newSplit1 = (PX.Objects.SO.SOLineSplit) null;
label_8:
            PX.Objects.SO.SOLineSplit oldSplit1;
            if (oldSplit != null)
            {
              nullable1 = oldSplit.Completed;
              bool flag = false;
              if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
              {
                oldSplit1 = oldSplit;
                goto label_12;
              }
            }
            oldSplit1 = (PX.Objects.SO.SOLineSplit) null;
label_12:
            Decimal num1;
            ref Decimal local = ref num1;
            base1.UpdateParent(line2, newSplit1, oldSplit1, out local);
            using (this.InvtMultModeScope(line1))
            {
              if (this.Base1.IsLotSerialRequired && newSplit != null)
                line1.UnassignedQty = !this.IsLotSerialItem((ILSMaster) newSplit) ? new Decimal?(0M) : ((IEnumerable<PX.Objects.SO.SOLineSplit>) this.SelectSplits(newSplit)).Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s => s.LotSerialNbr == null)).Sum<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, Decimal?>) (s => s.BaseQty));
              PX.Objects.SO.SOLine soLine = line1;
              Decimal num2 = num1;
              Decimal? baseClosedQty = line1.BaseClosedQty;
              Decimal? nullable2 = baseClosedQty.HasValue ? new Decimal?(num2 + baseClosedQty.GetValueOrDefault()) : new Decimal?();
              soLine.BaseQty = nullable2;
              this.SetLineQtyFromBase(line1);
            }
            GraphHelper.MarkUpdated((PXCache) this.LineCache, (object) line1, true);
            Decimal? qty1 = line1.Qty;
            Decimal? qty2 = copy.Qty;
            if (!(qty1.GetValueOrDefault() == qty2.GetValueOrDefault() & qty1.HasValue == qty2.HasValue))
              ((PXCache) this.LineCache).RaiseFieldUpdated<PX.Objects.SO.SOLine.orderQty>((object) line1, (object) copy.Qty);
            if (line1.LotSerialNbr != copy.LotSerialNbr)
              ((PXCache) this.LineCache).RaiseFieldUpdated<PX.Objects.SO.SOLine.lotSerialNbr>((object) line1, (object) copy.LotSerialNbr);
            if (this.LineCache.RaiseRowUpdating(copy, line1))
              this.LineCache.RaiseRowUpdated(line1, copy);
            else
              PXCache<PX.Objects.SO.SOLine>.RestoreCopy(line1, copy);
          }
        }
        return line1;
      }
    }
    return base_UpdateParent(newSplit, oldSplit);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.LineSplittingExtension`4.UpdateParent(`2,`3,`3,System.Decimal@)" />
  /// </summary>
  [PXOverride]
  public virtual void UpdateParent(
    PX.Objects.SO.SOLine line,
    PX.Objects.SO.SOLineSplit newSplit,
    PX.Objects.SO.SOLineSplit oldSplit,
    out Decimal baseQty,
    SOOrderLineSplittingAllocatedExtension.UpdateParentDelegate base_UpdateParent)
  {
    SOOrderLineSplittingAllocatedExtension.ResetAvailabilityCounters(line);
    int num1 = this.LineCounters.ContainsKey(line) ? 1 : 0;
    base_UpdateParent(line, newSplit, oldSplit, out baseQty);
    LSSelect.Counters counters1;
    if (num1 == 0 || oldSplit == null || !this.LineCounters.TryGetValue(line, out counters1))
      return;
    bool? nullable1 = oldSplit.POCreate;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = oldSplit.AMProdCreate;
      if (!nullable1.GetValueOrDefault())
      {
        if (oldSplit.Behavior == "BL")
        {
          counters1.BaseQty += oldSplit.BaseShippedQty.Value;
          goto label_6;
        }
        goto label_6;
      }
    }
    LSSelect.Counters counters2 = counters1;
    Decimal baseQty1 = counters2.BaseQty;
    Decimal? nullable2 = oldSplit.BaseReceivedQty;
    Decimal num2 = nullable2.Value;
    nullable2 = oldSplit.BaseShippedQty;
    Decimal num3 = nullable2.Value;
    Decimal num4 = num2 + num3;
    counters2.BaseQty = baseQty1 + num4;
label_6:
    baseQty = counters1.BaseQty;
  }

  public static void ResetAvailabilityCounters(PX.Objects.SO.SOLine line)
  {
    line.LineQtyAvail = new Decimal?();
    line.LineQtyHardAvail = new Decimal?();
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SOOrderLineSplittingExtension.UpdateCounters(PX.Objects.IN.LSSelect.Counters,PX.Objects.SO.SOLineSplit)" />
  /// </summary>
  [PXOverride]
  public virtual void UpdateCounters(
    LSSelect.Counters counters,
    PX.Objects.SO.SOLineSplit split,
    Action<LSSelect.Counters, PX.Objects.SO.SOLineSplit> base_UpdateCounters)
  {
    base_UpdateCounters(counters, split);
    PX.Objects.SO.SOOrderType soOrderType = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSetup<PX.Objects.SO.SOOrderType>.Select((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, Array.Empty<object>()));
    if (!this.IsAllocationEntryEnabled || soOrderType.RequireLotSerial.GetValueOrDefault() || this.ForceLineSingleLotSerialPopulation(split.InventoryID))
      return;
    counters.LotSerNumbersNull = -1;
    counters.LotSerNumber = (string) null;
    counters.LotSerNumbers.Clear();
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.LineSplittingExtension`4.DefaultLotSerialNbr(`3)" />
  /// </summary>
  [PXOverride]
  public virtual void DefaultLotSerialNbr(
    PX.Objects.SO.SOLineSplit row,
    Action<PX.Objects.SO.SOLineSplit> base_DefaultLotSerialNbr)
  {
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(row.InventoryID);
    if ((pxResult == null || this.IsAllocationEntryEnabled) && !(PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerAssign != "U"))
      return;
    base_DefaultLotSerialNbr(row);
  }

  public virtual void UncompleteSchedules(PX.Objects.SO.SOLine line)
  {
    this.LineCounters.Remove(line);
    Decimal? nullable1 = line.BaseOpenQty;
    foreach (PX.Objects.SO.SOLineSplit soLineSplit in ((IEnumerable<PX.Objects.SO.SOLineSplit>) this.SelectSplitsReversed(PX.Objects.SO.SOLineSplit.FromSOLine(line), false)).Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s => this.ShouldUncompleteSchedule(line, s))))
    {
      Decimal? nullable2 = nullable1;
      Decimal? baseQty = soLineSplit.BaseQty;
      nullable1 = nullable2.HasValue & baseQty.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - baseQty.GetValueOrDefault()) : new Decimal?();
      PX.Objects.SO.SOLineSplit copy = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(soLineSplit);
      copy.Completed = new bool?(false);
      this.SplitCache.Update(copy);
    }
    if (this.IsDropShipNotLegacy(line))
    {
      Decimal num1 = this.UncompleteDropShipSchedules(line);
      Decimal? nullable3 = nullable1;
      Decimal num2 = num1;
      nullable1 = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - num2) : new Decimal?();
    }
    this.IssueAvailable(line, new Decimal?(nullable1.Value), true);
  }

  protected virtual bool ShouldUncompleteSchedule(PX.Objects.SO.SOLine line, PX.Objects.SO.SOLineSplit split)
  {
    return split.ShipmentNbr == null;
  }

  public virtual Decimal UncompleteDropShipSchedules(PX.Objects.SO.SOLine line)
  {
    Decimal num = 0M;
    foreach (PX.Objects.SO.SOLineSplit soLineSplit in ((IEnumerable<PX.Objects.SO.SOLineSplit>) this.SelectAllSplits(line)).Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s => s.Completed.GetValueOrDefault() && s.PONbr != null && s.BaseQty.HasValue)))
    {
      num += soLineSplit.BaseQty.Value;
      PX.Objects.SO.SOLineSplit copy = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(soLineSplit);
      copy.Completed = new bool?(false);
      this.SplitCache.Update(copy);
    }
    return num;
  }

  public virtual void CompleteSchedules(PX.Objects.SO.SOLine line)
  {
    this.LineCounters.Remove(line);
    string str = (string) null;
    Decimal? nullable1 = new Decimal?(0M);
    HashSet<int?> lastUnshippedSchedules = new HashSet<int?>();
    foreach (PX.Objects.SO.SOLineSplit soLineSplit in this.SelectSplitsReversed(PX.Objects.SO.SOLineSplit.FromSOLine(line), false))
    {
      if (str == null && soLineSplit.ShipmentNbr != null)
        str = soLineSplit.ShipmentNbr;
      if (str != null && soLineSplit.ShipmentNbr == str)
      {
        Decimal? nullable2 = nullable1;
        Decimal? baseOpenQty = soLineSplit.BaseOpenQty;
        nullable1 = nullable2.HasValue & baseOpenQty.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + baseOpenQty.GetValueOrDefault()) : new Decimal?();
        lastUnshippedSchedules.Add(soLineSplit.SplitLineNbr);
      }
    }
    this.TruncateSchedules(line, nullable1.Value, lastUnshippedSchedules);
    foreach (PX.Objects.SO.SOLineSplit soLineSplit in this.SelectSplitsReversed(PX.Objects.SO.SOLineSplit.FromSOLine(line)))
    {
      PX.Objects.SO.SOLineSplit copy = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(soLineSplit);
      copy.Completed = new bool?(true);
      this.SplitCache.Update(copy);
    }
  }

  private void TruncateSchedules(PX.Objects.SO.SOLine line, Decimal baseQty)
  {
    this.TruncateSchedules(line, baseQty, (HashSet<int?>) null);
  }

  public virtual void TruncateSchedules(
    PX.Objects.SO.SOLine line,
    Decimal baseQty,
    HashSet<int?> lastUnshippedSchedules)
  {
    this.LineCounters.Remove(line);
    foreach (PX.Objects.SO.SOLineSplit soLineSplit1 in (IEnumerable<PX.Objects.SO.SOLineSplit>) ((IEnumerable<PX.Objects.SO.SOLineSplit>) this.SelectSplitsReversedforTruncate(PX.Objects.SO.SOLineSplit.FromSOLine(line))).OrderBy<PX.Objects.SO.SOLineSplit, int>((Func<PX.Objects.SO.SOLineSplit, int>) (_ => lastUnshippedSchedules == null || !lastUnshippedSchedules.Contains(_.ParentSplitLineNbr) ? 1 : 0)))
    {
      Decimal num1 = baseQty;
      Decimal? baseQty1 = soLineSplit1.BaseQty;
      Decimal valueOrDefault = baseQty1.GetValueOrDefault();
      if (num1 >= valueOrDefault & baseQty1.HasValue)
      {
        Decimal num2 = baseQty;
        baseQty1 = soLineSplit1.BaseQty;
        Decimal num3 = baseQty1.Value;
        baseQty = num2 - num3;
        this.SplitCache.Delete(soLineSplit1);
      }
      else
      {
        PX.Objects.SO.SOLineSplit copy = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(soLineSplit1);
        PX.Objects.SO.SOLineSplit soLineSplit2 = copy;
        baseQty1 = soLineSplit2.BaseQty;
        Decimal num4 = baseQty;
        soLineSplit2.BaseQty = baseQty1.HasValue ? new Decimal?(baseQty1.GetValueOrDefault() - num4) : new Decimal?();
        this.SetSplitQtyWithLine(copy, line);
        this.SplitCache.Update(copy);
        break;
      }
    }
  }

  public virtual bool SchedulesEqual(PX.Objects.SO.SOLineSplit a, PX.Objects.SO.SOLineSplit b, PXDBOperation operation)
  {
    if (a == null || b == null)
      return a != null;
    int? nullable1 = a.InventoryID;
    int? inventoryId = b.InventoryID;
    if (nullable1.GetValueOrDefault() == inventoryId.GetValueOrDefault() & nullable1.HasValue == inventoryId.HasValue)
    {
      int? nullable2 = a.SubItemID;
      nullable1 = b.SubItemID;
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
      {
        nullable1 = a.SiteID;
        nullable2 = b.SiteID;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          nullable2 = a.ToSiteID;
          nullable1 = b.ToSiteID;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          {
            DateTime? shipDate1 = a.ShipDate;
            DateTime? shipDate2 = b.ShipDate;
            if ((shipDate1.HasValue == shipDate2.HasValue ? (shipDate1.HasValue ? (shipDate1.GetValueOrDefault() == shipDate2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
            {
              bool? nullable3 = a.IsAllocated;
              bool? isAllocated = b.IsAllocated;
              if (nullable3.GetValueOrDefault() == isAllocated.GetValueOrDefault() & nullable3.HasValue == isAllocated.HasValue)
              {
                bool? nullable4 = a.IsMergeable;
                bool flag1 = false;
                if (!(nullable4.GetValueOrDefault() == flag1 & nullable4.HasValue))
                {
                  nullable4 = b.IsMergeable;
                  bool flag2 = false;
                  if (!(nullable4.GetValueOrDefault() == flag2 & nullable4.HasValue) && a.ShipmentNbr == b.ShipmentNbr)
                  {
                    nullable1 = a.ParentSplitLineNbr;
                    nullable2 = b.ParentSplitLineNbr;
                    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
                    {
                      nullable2 = a.SplitLineNbr;
                      nullable1 = b.ParentSplitLineNbr;
                      if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
                      {
                        nullable1 = b.SplitLineNbr;
                        nullable2 = a.ParentSplitLineNbr;
                        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
                          goto label_20;
                      }
                    }
                    if (string.Equals(a.LotSerialNbr, b.LotSerialNbr, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(a.LotSerialNbr) && string.IsNullOrEmpty(b.LotSerialNbr))
                    {
                      nullable4 = a.Completed;
                      nullable3 = b.Completed;
                      if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                      {
                        nullable3 = a.POCreate;
                        nullable4 = b.POCreate;
                        if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                        {
                          nullable4 = a.POCompleted;
                          int num1 = !nullable4.GetValueOrDefault() ? 1 : 0;
                          nullable4 = b.POCompleted;
                          int num2 = !nullable4.GetValueOrDefault() ? 1 : 0;
                          if (num1 == num2 && a.POType == b.POType && a.PONbr == b.PONbr)
                          {
                            nullable2 = a.POLineNbr;
                            nullable1 = b.POLineNbr;
                            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue && a.SOOrderType == b.SOOrderType && a.SOOrderNbr == b.SOOrderNbr)
                            {
                              nullable1 = a.SOLineNbr;
                              nullable2 = b.SOLineNbr;
                              if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
                              {
                                nullable2 = a.SOSplitLineNbr;
                                nullable1 = b.SOSplitLineNbr;
                                if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
                                  return string.Equals(a.UOM, b.UOM, StringComparison.OrdinalIgnoreCase);
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
label_20:
    return false;
  }

  protected virtual PX.Objects.SO.SOLineSplit TryUpdateSameSchedule(PXCache cache, PX.Objects.SO.SOLineSplit row)
  {
    foreach (PX.Objects.SO.SOLineSplit selectSplit in this.SelectSplits(row))
    {
      if (this.SchedulesEqual(row, selectSplit, (PXDBOperation) 2))
      {
        PX.Objects.SO.SOLineSplit copy = PXCache<PX.Objects.SO.SOLineSplit>.CreateCopy(selectSplit);
        PX.Objects.SO.SOLineSplit soLineSplit = selectSplit;
        Decimal? baseQty1 = soLineSplit.BaseQty;
        Decimal? baseQty2 = row.BaseQty;
        soLineSplit.BaseQty = baseQty1.HasValue & baseQty2.HasValue ? new Decimal?(baseQty1.GetValueOrDefault() + baseQty2.GetValueOrDefault()) : new Decimal?();
        this.SetSplitQtyWithLine(selectSplit, (PX.Objects.SO.SOLine) null);
        cache.Current = (object) selectSplit;
        cache.RaiseFieldUpdated<PX.Objects.SO.SOLineSplit.qty>((object) selectSplit, (object) copy.Qty);
        cache.RaiseRowUpdated((object) selectSplit, (object) copy);
        GraphHelper.MarkUpdated(cache, (object) selectSplit, true);
        PXDBQuantityAttribute.VerifyForDecimal(cache, (object) selectSplit);
        return selectSplit;
      }
    }
    return (PX.Objects.SO.SOLineSplit) null;
  }

  protected virtual void IssueAvailable(PX.Objects.SO.SOLine line)
  {
    this.IssueAvailable(line, line.BaseOpenQty);
  }

  protected virtual void IssueAvailable(PX.Objects.SO.SOLine line, Decimal? baseQty)
  {
    this.IssueAvailable(line, baseQty, false);
  }

  protected virtual void IssueAvailable(PX.Objects.SO.SOLine line, Decimal? baseQty, bool isUncomplete)
  {
    int? nullable1 = new int?();
    this.LineCounters.Remove(line);
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(line.InventoryID);
    INSiteStatusByCostCenter statusByCostCenter1 = INSiteStatusByCostCenter.PK.Find((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, line.InventoryID, line.SubItemID, line.SiteID, line.CostCenterID);
    Decimal? nullable2;
    if (statusByCostCenter1 != null)
    {
      PX.Objects.SO.SOLineSplit soLineSplit1 = PX.Objects.SO.SOLineSplit.FromSOLine(line);
      if (this.Base1.UseBaseUnitInSplit(soLineSplit1, line, pxResult))
        soLineSplit1.UOM = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).BaseUnit;
      SetDefaultShippedQtyForSplit(soLineSplit1);
      soLineSplit1.SplitLineNbr = new int?();
      soLineSplit1.IsAllocated = line.RequireAllocation;
      soLineSplit1.SiteID = line.SiteID;
      this.AssignNewSplitFields(soLineSplit1, line);
      object obj1;
      ((PXCache) this.SplitCache).RaiseFieldDefaulting<PX.Objects.SO.SOLineSplit.allocatedPlanType>((object) soLineSplit1, ref obj1);
      ((PXCache) this.SplitCache).SetValue<PX.Objects.SO.SOLineSplit.allocatedPlanType>((object) soLineSplit1, obj1);
      object obj2;
      ((PXCache) this.SplitCache).RaiseFieldDefaulting<PX.Objects.SO.SOLineSplit.backOrderPlanType>((object) soLineSplit1, ref obj2);
      ((PXCache) this.SplitCache).SetValue<PX.Objects.SO.SOLineSplit.backOrderPlanType>((object) soLineSplit1, obj2);
      Sign signQtyHardAvail = this.Availability.GetAvailabilitySigns<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>(soLineSplit1).SignQtyHardAvail;
      if (((Sign) ref signQtyHardAvail).IsMinus)
      {
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter2 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter();
        PXCache<INSiteStatusByCostCenter>.RestoreCopy((INSiteStatusByCostCenter) statusByCostCenter2, statusByCostCenter1);
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter3 = PXCache<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>.Insert((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, statusByCostCenter2);
        nullable2 = statusByCostCenter1.QtyHardAvail;
        Decimal? nullable3 = statusByCostCenter3.QtyHardAvail;
        Decimal? nullable4 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
        nullable3 = nullable4;
        Decimal num = 0M;
        if (nullable3.GetValueOrDefault() > num & nullable3.HasValue)
        {
          nullable3 = nullable4;
          nullable2 = baseQty;
          if (nullable3.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable3.HasValue & nullable2.HasValue)
          {
            bool valueOrDefault = soLineSplit1.IsAllocated.GetValueOrDefault();
            soLineSplit1.BaseQty = valueOrDefault ? baseQty : nullable4;
            this.SetSplitQtyWithLine(soLineSplit1, line);
            PX.Objects.SO.SOLineSplit soLineSplit2 = (PX.Objects.SO.SOLineSplit) null;
            if (!this.Base1.IsLSEntryEnabled && this.CurrentOperation == 1)
              soLineSplit2 = this.TryUpdateSameSchedule((PXCache) this.SplitCache, soLineSplit1);
            PX.Objects.SO.SOLineSplit soLineSplit3 = soLineSplit2 ?? this.SplitCache.Insert(soLineSplit1);
            if (valueOrDefault)
            {
              PX.Objects.SO.SOLineSplit split = soLineSplit3;
              nullable2 = nullable4;
              nullable3 = baseQty;
              Decimal? negQtyHardAvail = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
              this.BreakupAllocatedSplit(split, negQtyHardAvail, false);
              return;
            }
            nullable1 = (int?) soLineSplit3?.SplitLineNbr;
            nullable3 = baseQty;
            nullable2 = nullable4;
            baseQty = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
          }
          else
          {
            soLineSplit1.BaseQty = baseQty;
            this.SetSplitQtyWithLine(soLineSplit1, line);
            this.SplitCache.Insert(soLineSplit1);
            baseQty = new Decimal?(0M);
          }
        }
      }
    }
    nullable2 = baseQty;
    Decimal num1 = 0M;
    if (!(nullable2.GetValueOrDefault() > num1 & nullable2.HasValue))
      return;
    int? nullable5 = line.InventoryID;
    if (!nullable5.HasValue)
      return;
    nullable5 = line.SiteID;
    if (!nullable5.HasValue && !(line.LineType == "MI"))
      return;
    nullable5 = line.SubItemID;
    if (!nullable5.HasValue)
    {
      nullable5 = line.SubItemID;
      if (!nullable5.HasValue)
      {
        bool? nullable6 = line.IsStockItem;
        if (!nullable6.GetValueOrDefault())
        {
          nullable6 = line.IsKit;
          if (nullable6.GetValueOrDefault())
            goto label_20;
        }
      }
      if (!EnumerableExtensions.IsIn<string>(line.LineType, "GN", "MI"))
        return;
    }
label_20:
    PX.Objects.SO.SOLineSplit soLineSplit = PX.Objects.SO.SOLineSplit.FromSOLine(line);
    SetDefaultShippedQtyForSplit(soLineSplit);
    if (this.Base1.UseBaseUnitInSplit(soLineSplit, line, pxResult))
      soLineSplit.UOM = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).BaseUnit;
    soLineSplit.SplitLineNbr = nullable1;
    soLineSplit.IsAllocated = new bool?(false);
    this.AssignNewSplitFields(soLineSplit, line);
    soLineSplit.BaseQty = baseQty;
    this.SetSplitQtyWithLine(soLineSplit, line);
    if (isUncomplete)
    {
      soLineSplit.POCreate = new bool?(false);
      soLineSplit.POSource = (string) null;
    }
    this.InsertAllocationRemainder(soLineSplit, baseQty, false);

    void SetDefaultShippedQtyForSplit(PX.Objects.SO.SOLineSplit split)
    {
      ((PXCache) this.SplitCache).SetDefaultExt<PX.Objects.SO.SOLineSplit.shipComplete>((object) split);
      ((PXCache) this.SplitCache).SetDefaultExt<PX.Objects.SO.SOLineSplit.baseShippedQty>((object) split);
      ((PXCache) this.SplitCache).SetDefaultExt<PX.Objects.SO.SOLineSplit.shippedQty>((object) split);
    }
  }

  protected virtual void AssignNewSplitFields(PX.Objects.SO.SOLineSplit split, PX.Objects.SO.SOLine line)
  {
  }

  protected virtual bool IsSplitRequired(PX.Objects.SO.SOLine line)
  {
    return this.IsSplitRequired(line, out PX.Objects.IN.InventoryItem _);
  }

  protected virtual bool IsSplitRequired(PX.Objects.SO.SOLine line, out PX.Objects.IN.InventoryItem item)
  {
    if (line == null)
    {
      item = (PX.Objects.IN.InventoryItem) null;
      return false;
    }
    bool flag1 = false;
    item = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, line.InventoryID);
    bool? nullable;
    if (this.Base1.IsLocationEnabled && item != null)
    {
      nullable = item.StkItem;
      bool flag2 = false;
      if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
      {
        nullable = item.KitItem;
        bool flag3 = false;
        if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
        {
          nullable = item.NonStockShip;
          bool flag4 = false;
          if (nullable.GetValueOrDefault() == flag4 & nullable.HasValue)
            flag1 = true;
        }
      }
    }
    if (item != null)
    {
      nullable = item.StkItem;
      bool flag5 = false;
      if (nullable.GetValueOrDefault() == flag5 & nullable.HasValue)
      {
        nullable = item.KitItem;
        if (nullable.GetValueOrDefault() && EnumerableExtensions.IsNotIn<string>(line.Behavior, "CM", "IN", "MO"))
          flag1 = true;
      }
    }
    if (flag1)
      return false;
    if (this.Base1.IsLocationEnabled)
      return true;
    if (this.Base1.IsLotSerialRequired || this.ForceLineSingleLotSerialPopulation(line.InventoryID))
    {
      nullable = line.POCreate;
      if (!nullable.GetValueOrDefault())
        return this.IsLotSerialItem((ILSMaster) line);
    }
    return false;
  }

  protected virtual bool IsDropShipNotLegacy(PX.Objects.SO.SOLineSplit split)
  {
    return this.IsDropShipNotLegacy(split != null ? PXParentAttribute.SelectParent<PX.Objects.SO.SOLine>((PXCache) this.Base1.SplitCache, (object) split) : (PX.Objects.SO.SOLine) null);
  }

  protected virtual bool IsDropShipNotLegacy(PX.Objects.SO.SOLine line)
  {
    return line != null && line.POCreate.GetValueOrDefault() && line.POSource == "D" && !line.IsLegacyDropShip.GetValueOrDefault();
  }

  protected virtual bool IsLotSerialItem(ILSMaster line)
  {
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(line.InventoryID);
    if (pxResult == null)
      return false;
    INLotSerClass lotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult);
    string tranType = line.TranType;
    short? invtMult = line.InvtMult;
    int? invMult = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    return INLotSerialNbrAttribute.IsTrack(lotSerClass, tranType, invMult);
  }

  protected virtual bool NegativeInventoryError(PX.Objects.SO.SOLineSplit split)
  {
    split.IsAllocated = new bool?(false);
    split.LotSerialNbr = (string) null;
    this.UpdateParentAfterLotSerialNbrCleared(split);
    ((PXCache) this.SplitCache).RaiseExceptionHandling<PX.Objects.SO.SOLineSplit.lotSerialNbr>((object) split, (object) null, (Exception) new PXSetPropertyException("Inventory quantity will go negative."));
    return split.IsAllocated.GetValueOrDefault();
  }

  public virtual bool IsLotSerialsAllowedForDropShipLine(PX.Objects.SO.SOLine line)
  {
    return line.Operation == "R" && ((PXResult) this.ReadInventoryItem(line.InventoryID)).GetItem<INLotSerClass>().RequiredForDropship.GetValueOrDefault();
  }

  public virtual PX.Objects.SO.SOLineSplit[] SelectSplitsForDropShip(PX.Objects.SO.SOLine line)
  {
    return ((IEnumerable<PX.Objects.SO.SOLineSplit>) this.SelectAllSplits(line)).Where<PX.Objects.SO.SOLineSplit>((Func<PX.Objects.SO.SOLineSplit, bool>) (s =>
    {
      if (s.ShipmentNbr == null)
        return true;
      Decimal? shippedQty = s.ShippedQty;
      Decimal num = 0M;
      return !(shippedQty.GetValueOrDefault() == num & shippedQty.HasValue);
    })).ToArray<PX.Objects.SO.SOLineSplit>();
  }

  public virtual bool HasMultipleSplitsOrAllocation(PX.Objects.SO.SOLine line)
  {
    PX.Objects.SO.SOLineSplit[] source = this.SelectSplitsForDropShip(line);
    if (source.Length > 1)
      return true;
    PX.Objects.SO.SOLineSplit soLineSplit = ((IEnumerable<PX.Objects.SO.SOLineSplit>) source).FirstOrDefault<PX.Objects.SO.SOLineSplit>();
    return soLineSplit != null && soLineSplit.IsAllocated.GetValueOrDefault();
  }

  [Obsolete]
  protected virtual void SetUnreceivedQty(PX.Objects.SO.SOLineSplit split)
  {
    PX.Objects.SO.SOLine soLine = this.SelectLine(split);
    int? inventoryId1 = split.InventoryID;
    int? inventoryId2 = (int?) soLine?.InventoryID;
    Decimal? nullable1;
    if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
    {
      Decimal? baseUnreceivedQty = split.BaseUnreceivedQty;
      nullable1 = (Decimal?) soLine?.BaseQty;
      if (baseUnreceivedQty.GetValueOrDefault() == nullable1.GetValueOrDefault() & baseUnreceivedQty.HasValue == nullable1.HasValue && string.Equals(split.UOM, soLine?.UOM, StringComparison.OrdinalIgnoreCase))
      {
        split.UnreceivedQty = soLine.Qty;
        return;
      }
    }
    PX.Objects.SO.SOLineSplit soLineSplit = split;
    PXCache<PX.Objects.SO.SOLineSplit> splitCache = this.SplitCache;
    int? inventoryId3 = split.InventoryID;
    string uom = split.UOM;
    nullable1 = split.BaseUnreceivedQty;
    Decimal num = nullable1.Value;
    Decimal? nullable2 = new Decimal?(INUnitAttribute.ConvertFromBase((PXCache) splitCache, inventoryId3, uom, num, INPrecision.QUANTITY));
    soLineSplit.UnreceivedQty = nullable2;
  }

  protected virtual void RefreshViewOf(PXCache cache)
  {
    foreach (KeyValuePair<string, PXView> view in (Dictionary<string, PXView>) ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).Views)
    {
      PXView pxView = view.Value;
      if (!pxView.IsReadOnly && pxView.GetItemType() == cache.GetItemType())
        pxView.RequestRefresh();
    }
  }

  protected virtual PX.Objects.SO.SOLineSplit InsertAllocationRemainder(
    PX.Objects.SO.SOLineSplit copy,
    Decimal? baseQty,
    bool allocated)
  {
    PX.Objects.SO.SOLine line = this.SelectLine(copy);
    if (line != null && line.IsSpecialOrder.GetValueOrDefault() && line.Operation == "I" && line.Behavior != "TR")
      copy.POCreate = new bool?(true);
    copy.SplitLineNbr = new int?();
    copy.PlanID = new long?();
    copy.IsAllocated = new bool?(allocated);
    copy.BaseQty = baseQty;
    this.SetSplitQtyWithLine(copy, line);
    copy.OpenQty = new Decimal?();
    copy.BaseOpenQty = new Decimal?();
    copy.UnreceivedQty = new Decimal?();
    copy.BaseUnreceivedQty = new Decimal?();
    copy = PXCache<PX.Objects.SO.SOLineSplit>.Insert((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base, copy);
    return copy;
  }

  public virtual PX.Objects.SO.SOLineSplit InsertShipmentRemainder(PX.Objects.SO.SOLineSplit copy)
  {
    using (this.OperationModeScope((PXDBOperation) 1))
    {
      using (this.Base1.SuppressedModeScope(true))
        return this.SplitCache.Insert(copy);
    }
  }

  public virtual bool AllowToManualAllocate(PX.Objects.SO.SOLine line, PX.Objects.SO.SOLineSplit split)
  {
    if (!(split.LineType == "GI") || !(split.Operation == "I") || split.Completed.GetValueOrDefault() || split.POCreate.GetValueOrDefault() && !line.IsSpecialOrder.GetValueOrDefault() || split.POCompleted.GetValueOrDefault())
      return false;
    int? childLineCntr = split.ChildLineCntr;
    int num = 0;
    return childLineCntr.GetValueOrDefault() == num & childLineCntr.HasValue;
  }

  public delegate void UpdateParentDelegate(
    PX.Objects.SO.SOLine line,
    PX.Objects.SO.SOLineSplit newSplit,
    PX.Objects.SO.SOLineSplit oldSplit,
    out Decimal baseQty);
}
