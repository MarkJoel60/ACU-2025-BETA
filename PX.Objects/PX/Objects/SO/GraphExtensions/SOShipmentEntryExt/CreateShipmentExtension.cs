// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.CreateShipmentExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using PX.Objects.SO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public class CreateShipmentExtension : PXGraphExtension<SOShipmentEntry>
{
  protected bool IsSyncUnassignedScope;

  public virtual void CreateShipment(CreateShipmentArgs args)
  {
    SiteLotSerial.AccumulatorAttribute.ForceAvailQtyValidation((PXGraph) this.Base, true);
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial.AccumulatorAttribute.ForceAvailQtyValidation((PXGraph) this.Base, true);
    if (args.QuickProcessFlow != null)
      ((PXSelectBase<SOSetup>) this.Base.sosetup).Current.HoldShipments = new bool?(false);
    this.ValidateCreateShipmentArgs(args);
    PX.Objects.SO.SOShipment shipment;
    bool newlyCreated;
    if (args.ShipmentList != null)
    {
      ((PXGraph) this.Base).Clear();
      shipment = this.FindOrCreateShipment(args);
      newlyCreated = shipment.ShipmentNbr == null;
      if (newlyCreated)
      {
        shipment = ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Insert(shipment);
      }
      else
      {
        ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current = PXResultset<PX.Objects.SO.SOShipment>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Search<PX.Objects.SO.SOShipment.shipmentNbr>((object) shipment.ShipmentNbr, Array.Empty<object>()));
        if (((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current.Confirmed.GetValueOrDefault())
          throw new PXException("Document Status is invalid for processing.");
      }
    }
    else
    {
      shipment = PXCache<PX.Objects.SO.SOShipment>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current);
      int? orderCntr = shipment.OrderCntr;
      int num = 0;
      newlyCreated = orderCntr.GetValueOrDefault() == num & orderCntr.HasValue;
    }
    bool flag = this.SetShipmentFieldsFromOrigDocument(shipment, args, newlyCreated);
    if (newlyCreated)
      this.SetShipAddressAndContactFromArgs(shipment, args);
    if (newlyCreated | flag)
      ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Update(shipment);
    this.CreateShipmentDetails(args);
    if (args.FilesAndNotesSource != null && args.CopyNotesAndFilesSettings != null)
      PXNoteAttribute.CopyNoteAndFiles(((PXGraph) this.Base).Caches[args.FilesAndNotesSource.GetType()], args.FilesAndNotesSource, ((PXSelectBase) this.Base.Document).Cache, (object) ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current, new bool?(PXNoteAttribute.GetNote(((PXSelectBase) this.Base.Document).Cache, (object) ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current) == null && args.CopyNotesAndFilesSettings.CopyNotes.GetValueOrDefault()), args.CopyNotesAndFilesSettings.CopyFiles);
    if (args.ShipmentList != null && this.ShouldSaveAfterCreateShipment(args))
    {
      using (new SOShipmentEntry.SkipShipCompleteValidationScope())
        ((PXAction) this.Base.Save).Press();
      this.AfterSaveCreateShipment(args);
    }
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial.AccumulatorAttribute.ForceAvailQtyValidation((PXGraph) this.Base, false);
    SiteLotSerial.AccumulatorAttribute.ForceAvailQtyValidation((PXGraph) this.Base, false);
  }

  protected virtual void ValidateCreateShipmentArgs(CreateShipmentArgs args)
  {
    args.ShipDate = this.GetShipmentDate(args);
    args.EndDate = args.EndDate ?? args.ShipDate;
  }

  /// <summary>Returns the date of the Shipment</summary>
  protected virtual DateTime? GetShipmentDate(CreateShipmentArgs args) => args.ShipDate;

  protected virtual PX.Objects.SO.SOShipment FindOrCreateShipment(CreateShipmentArgs args)
  {
    return args.ShipmentList.Find(this.GetShipmentFieldLookups(args)) ?? new PX.Objects.SO.SOShipment();
  }

  protected virtual FieldLookup[] GetShipmentFieldLookups(CreateShipmentArgs args)
  {
    return new FieldLookup[4]
    {
      (FieldLookup) new FieldLookup<PX.Objects.SO.SOShipment.shipDate>((object) args.ShipDate),
      (FieldLookup) new FieldLookup<PX.Objects.SO.SOShipment.siteID>((object) args.SiteID),
      (FieldLookup) new FieldLookup<PX.Objects.SO.SOShipment.shipmentType>((object) args.ShipmentType),
      (FieldLookup) new FieldLookup<PX.Objects.SO.SOShipment.hidden>((object) false)
    };
  }

  protected virtual bool SetShipmentFieldsFromOrigDocument(
    PX.Objects.SO.SOShipment shipment,
    CreateShipmentArgs args,
    bool newlyCreated)
  {
    if (!newlyCreated)
      return false;
    shipment.SiteID = args.SiteID;
    shipment.ShipmentType = args.ShipmentType;
    shipment.Operation = args.Operation;
    shipment.ShipDate = args.ShipDate;
    return true;
  }

  public virtual bool CreateShipmentFromSchedules(
    CreateShipmentArgs args,
    IShipLineSource res,
    SOShipLine newline)
  {
    bool shipmentFromSchedules = false;
    IShipLineSource plan = res;
    bool allocationUnallocated = res.RequireAllocationUnallocated;
    bool? nullable1;
    int num1;
    if (allocationUnallocated)
    {
      nullable1 = ((PXSelectBase<SOSetup>) this.Base.sosetup).Current.AddAllToShipment;
      num1 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag1 = num1 != 0;
    if (!plan.Selected)
    {
      if (args.ShipmentList != null)
      {
        if (allocationUnallocated)
        {
          nullable1 = ((PXSelectBase<SOSetup>) this.Base.sosetup).Current.AddAllToShipment;
          if (!nullable1.GetValueOrDefault())
            goto label_58;
        }
      }
      else
        goto label_58;
    }
    this.FillShipLineFromSource(newline, res);
    INLotSerClass inLotSerClass = res.INLotSerClass;
    Decimal? nullable2;
    Decimal? nullable3;
    if (inLotSerClass.LotSerTrack == null)
    {
      SOShipLine soShipLine = newline;
      Decimal? nullable4;
      if (newline.UOM == newline.OrderUOM)
      {
        nullable2 = plan.PlanQty;
        nullable3 = newline.BaseFullOrderQty;
        if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
        {
          nullable4 = newline.FullOrderQty;
          goto label_12;
        }
      }
      PXCache cache = ((PXSelectBase) this.Base.Transactions).Cache;
      int? inventoryId = newline.InventoryID;
      string uom = newline.UOM;
      nullable3 = plan.PlanQty;
      Decimal num2 = nullable3.Value;
      nullable4 = new Decimal?(INUnitAttribute.ConvertFromBase(cache, inventoryId, uom, num2, INPrecision.QUANTITY));
label_12:
      soShipLine.ShippedQty = nullable4;
      newline = this.Base.LineSplittingExt.InsertWithoutSplits(newline);
      try
      {
        this.ShipAvailable(plan, newline, new PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>(res.InventoryItem, res.INLotSerClass));
      }
      catch (PXException ex)
      {
        this.Base.LineSplittingExt.lsselect.Delete(newline);
        throw ex;
      }
    }
    else if (args.Operation == "R")
    {
      SOShipLine soShipLine = newline;
      Decimal? nullable5;
      if (newline.UOM == newline.OrderUOM)
      {
        nullable3 = plan.PlanQty;
        nullable2 = newline.BaseFullOrderQty;
        if (nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue)
        {
          nullable5 = newline.FullOrderQty;
          goto label_20;
        }
      }
      PXCache cache = ((PXSelectBase) this.Base.Transactions).Cache;
      int? inventoryId = newline.InventoryID;
      string uom = newline.UOM;
      nullable2 = plan.PlanQty;
      Decimal num3 = nullable2.Value;
      nullable5 = new Decimal?(INUnitAttribute.ConvertFromBase(cache, inventoryId, uom, num3, INPrecision.QUANTITY));
label_20:
      soShipLine.ShippedQty = nullable5;
      newline.LocationID = (int?) res.INSite?.ReturnLocationID;
      if (!newline.LocationID.HasValue && args.ShipmentList != null)
        throw new PXException("RMA Location is not configured for warehouse {0}", new object[1]
        {
          (object) res.INSite?.SiteCD
        });
      newline = ((PXSelectBase<SOShipLine>) this.Base.Transactions).Insert(newline);
      this.ReceiveLotSerial(plan, newline, new PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>(res.InventoryItem, res.INLotSerClass));
    }
    else
    {
      SOShipLine soShipLine1 = (SOShipLine) ((PXSelectBase) this.Base.Transactions).Cache.Locate((object) newline);
      if (soShipLine1 == null || EnumerableExtensions.IsIn<PXEntryStatus>(((PXSelectBase) this.Base.Transactions).Cache.GetStatus((object) soShipLine1), (PXEntryStatus) 3, (PXEntryStatus) 4))
      {
        newline.ShippedQty = new Decimal?(0M);
        newline = this.Base.LineSplittingExt.InsertWithoutSplits(newline);
      }
      if (!flag1)
      {
        SOShipLine soShipLine2 = newline;
        nullable1 = inLotSerClass.IsManualAssignRequired;
        int num4;
        if (nullable1.GetValueOrDefault())
        {
          nullable2 = plan.PlanQty;
          Decimal num5 = 0M;
          if (nullable2.GetValueOrDefault() > num5 & nullable2.HasValue && string.IsNullOrEmpty(plan.LotSerialNbr))
          {
            if (!(inLotSerClass.LotSerAssign != "U"))
            {
              if (newline.ShipmentType != "T")
              {
                nullable1 = newline.IsIntercompany;
                num4 = !nullable1.GetValueOrDefault() ? 1 : 0;
                goto label_34;
              }
              num4 = 0;
              goto label_34;
            }
            num4 = 1;
            goto label_34;
          }
        }
        num4 = 0;
label_34:
        bool? nullable6 = new bool?(num4 != 0);
        soShipLine2.IsUnassigned = nullable6;
        Decimal? nullable7 = this.ShipAvailable(plan, newline, new PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>(res.InventoryItem, res.INLotSerClass));
        nullable1 = newline.IsUnassigned;
        if (nullable1.GetValueOrDefault())
        {
          SOShipLine copy = (SOShipLine) ((PXSelectBase) this.Base.Transactions).Cache.CreateCopy((object) newline);
          SOShipLine soShipLine3 = newline;
          nullable2 = plan.PlanQty;
          Decimal? nullable8 = nullable7;
          Decimal? nullable9 = nullable2.HasValue & nullable8.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable8.GetValueOrDefault()) : new Decimal?();
          soShipLine3.UnassignedQty = nullable9;
          SOShipLine soShipLine4 = newline;
          nullable8 = plan.PlanQty;
          nullable2 = nullable7;
          Decimal? nullable10 = nullable8.HasValue & nullable2.HasValue ? new Decimal?(nullable8.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
          soShipLine4.BaseShippedQty = nullable10;
          SOShipLine soShipLine5 = newline;
          Decimal? nullable11;
          if (newline.UOM == newline.OrderUOM)
          {
            nullable2 = newline.BaseShippedQty;
            nullable8 = newline.BaseFullOrderQty;
            if (nullable2.GetValueOrDefault() == nullable8.GetValueOrDefault() & nullable2.HasValue == nullable8.HasValue)
            {
              nullable11 = newline.FullOrderQty;
              goto label_39;
            }
          }
          PXCache cache = ((PXSelectBase) this.Base.unassignedSplits).Cache;
          int? inventoryId = newline.InventoryID;
          string uom = newline.UOM;
          nullable8 = newline.BaseShippedQty;
          Decimal num6 = nullable8.Value;
          nullable11 = new Decimal?(INUnitAttribute.ConvertFromBase(cache, inventoryId, uom, num6, INPrecision.QUANTITY));
label_39:
          soShipLine5.ShippedQty = nullable11;
          using (this.Base.LineSplittingExt.SuppressedModeScope(true))
          {
            ((PXSelectBase) this.Base.Transactions).Cache.RaiseFieldUpdated<SOShipLine.shippedQty>((object) newline, (object) copy.ShippedQty);
            ((PXSelectBase) this.Base.Transactions).Cache.RaiseRowUpdated((object) newline, (object) copy);
          }
        }
      }
    }
    nullable3 = newline.BaseShippedQty;
    nullable2 = plan.PlanQty;
    if (nullable3.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable3.HasValue & nullable2.HasValue && string.IsNullOrEmpty(plan.LotSerialNbr) && !flag1)
      this.PromptReplenishment(((PXSelectBase) this.Base.Transactions).Cache, newline, res.InventoryItem, plan);
    nullable2 = newline.ShippedQty;
    Decimal num7 = 0M;
    if (nullable2.GetValueOrDefault() == num7 & nullable2.HasValue)
    {
      SOShipLine shipline = newline;
      int num8;
      if (args.ShipmentList != null)
      {
        nullable1 = ((PXSelectBase<SOSetup>) this.Base.sosetup).Current.AddAllToShipment;
        bool flag2 = false;
        num8 = nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue ? 1 : 0;
      }
      else
        num8 = 0;
      shipmentFromSchedules = this.RemoveLineFromShipment(shipline, num8 != 0);
    }
    nullable2 = newline.BaseShippedQty;
    nullable3 = res.MinRequiredBaseShippedQty;
    if (nullable2.GetValueOrDefault() < nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue && res.ShippingRule == "C")
      shipmentFromSchedules = this.RemoveLineFromShipment(newline, args.ShipmentList != null);
    if (!shipmentFromSchedules && res.FilesAndNotesSource != null && args.CopyLineNotesAndFilesSettings != null)
      PXNoteAttribute.CopyNoteAndFiles(((PXGraph) this.Base).Caches[res.FilesAndNotesSource.GetType()], res.FilesAndNotesSource, ((PXGraph) this.Base).Caches[typeof (SOShipLine)], (object) newline, args.CopyLineNotesAndFilesSettings);
    if (!shipmentFromSchedules && !flag1 && plan.RequireINItemPlanUpdate)
    {
      INItemPlan inItemPlan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXSelect<INItemPlan, Where<INItemPlan.planID, Equal<Required<INItemPlan.planID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) plan.PlanID
      }));
      if (inItemPlan != null)
      {
        inItemPlan.PlanType = plan.NewPlanType;
        ((PXGraph) this.Base).Caches[typeof (INItemPlan)].Update((object) inItemPlan);
      }
    }
label_58:
    return shipmentFromSchedules;
  }

  protected virtual void FillShipLineFromSource(SOShipLine newline, IShipLineSource line)
  {
    newline.OrigLineNbr = line.LineNbr;
    newline.IsStockItem = line.IsStockItem;
    newline.InventoryID = line.InventoryID;
    newline.SubItemID = line.SubItemID;
    newline.SiteID = line.SiteID;
    newline.TranDesc = line.TranDesc;
    newline.ProjectID = line.ProjectID;
    newline.TaskID = line.TaskID;
    newline.CostCodeID = line.CostCodeID;
    newline.UOM = line.UOM;
    newline.CostCenterID = line.CostCenterID;
  }

  public virtual Decimal? ShipAvailable(
    IShipLineSource plan,
    SOShipLine newline,
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> item)
  {
    INLotSerClass lotserclass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(item);
    PX.Objects.IN.InventoryItem inventoryItem = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(item);
    bool? stkItem = inventoryItem.StkItem;
    bool flag = false;
    if (stkItem.GetValueOrDefault() == flag & stkItem.HasValue && inventoryItem.KitItem.GetValueOrDefault())
    {
      Decimal? planQty = plan.PlanQty;
      object lastComponentID = (object) null;
      bool HasSerialComponents = false;
      this.ShipNonStockKit(plan, newline, ref planQty, ref lastComponentID, ref HasSerialComponents);
      SOShipLine copy = PXCache<SOShipLine>.CreateCopy(newline);
      SOShipLine soShipLine = copy;
      Decimal? nullable1;
      if (copy.UOM == copy.OrderUOM)
      {
        Decimal? nullable2 = planQty;
        Decimal? baseFullOrderQty = copy.BaseFullOrderQty;
        if (nullable2.GetValueOrDefault() == baseFullOrderQty.GetValueOrDefault() & nullable2.HasValue == baseFullOrderQty.HasValue)
        {
          nullable1 = copy.FullOrderQty;
          goto label_5;
        }
      }
      nullable1 = new Decimal?(INUnitAttribute.ConvertFromBase<SOShipLine.inventoryID>(((PXSelectBase) this.Base.Transactions).Cache, (object) copy, copy.UOM, planQty.Value, INPrecision.QUANTITY, INMidpointRounding.FLOOR));
label_5:
      soShipLine.ShippedQty = nullable1;
      this.Base.LineSplittingExt.LastComponentID = (int?) lastComponentID;
      try
      {
        ((PXSelectBase<SOShipLine>) this.Base.Transactions).Update(copy);
      }
      finally
      {
        this.Base.LineSplittingExt.LastComponentID = new int?();
      }
      return new Decimal?(0M);
    }
    if (lotserclass == null || lotserclass.LotSerTrack == null)
      return this.ShipNonStock(plan, newline);
    return lotserclass.LotSerTrack == "N" || lotserclass.LotSerAssign == "U" || newline.IsUnassigned.GetValueOrDefault() ? this.ShipAvailableNonLots(plan, newline, lotserclass) : this.ShipAvailableLots(plan, newline, lotserclass);
  }

  public virtual void ReceiveLotSerial(
    IShipLineSource plan,
    SOShipLine newline,
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> item)
  {
    PXSelectBase<INLotSerialStatusByCostCenter> pxSelectBase = (PXSelectBase<INLotSerialStatusByCostCenter>) new PXSelectReadonly2<INLotSerialStatusByCostCenter, InnerJoin<INLocation, On<INLotSerialStatusByCostCenter.FK.Location>>, Where<INLotSerialStatusByCostCenter.inventoryID, Equal<Required<INLotSerialStatusByCostCenter.inventoryID>>, And<INLotSerialStatusByCostCenter.subItemID, Equal<Required<INLotSerialStatusByCostCenter.subItemID>>, And<INLotSerialStatusByCostCenter.siteID, Equal<Required<INLotSerialStatusByCostCenter.siteID>>, And<INLotSerialStatusByCostCenter.costCenterID, Equal<Required<INLotSerialStatusByCostCenter.costCenterID>>, And<INLocation.salesValid, Equal<boolTrue>>>>>>>((PXGraph) this.Base);
    if (!string.IsNullOrEmpty(plan.LotSerialNbr))
      pxSelectBase.WhereAnd<Where<INLotSerialStatusByCostCenter.lotSerialNbr, Equal<Required<INLotSerialStatusByCostCenter.lotSerialNbr>>>>();
    INLotSerialStatusByCostCenter statusByCostCenter = PXResultset<INLotSerialStatusByCostCenter>.op_Implicit(pxSelectBase.SelectWindowed(0, 1, new object[5]
    {
      (object) newline.InventoryID,
      (object) newline.SubItemID,
      (object) newline.SiteID,
      (object) newline.CostCenterID,
      (object) plan.LotSerialNbr
    }));
    SOShipLineSplit soShipLineSplit = SOShipLineSplit.FromSOShipLine(newline);
    soShipLineSplit.UOM = (string) null;
    soShipLineSplit.Qty = soShipLineSplit.BaseQty;
    soShipLineSplit.SplitLineNbr = new int?();
    if (statusByCostCenter != null)
    {
      if (!soShipLineSplit.LocationID.HasValue)
        soShipLineSplit.LocationID = statusByCostCenter.LocationID;
      soShipLineSplit.LotSerialNbr = statusByCostCenter.LotSerialNbr;
      soShipLineSplit.ExpireDate = plan.ExpireDate ?? statusByCostCenter.ExpireDate;
    }
    else
    {
      PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, newline.SiteID);
      soShipLineSplit.LocationID = inSite.ReturnLocationID;
      soShipLineSplit.LotSerialNbr = plan.LotSerialNbr;
      soShipLineSplit.ExpireDate = plan.ExpireDate ?? newline.ExpireDate;
    }
    if (string.IsNullOrEmpty(plan.LotSerialNbr))
      return;
    ((PXSelectBase<SOShipLineSplit>) this.Base.splits).Update(soShipLineSplit);
  }

  public virtual void PromptReplenishment(
    PXCache sender,
    SOShipLine newline,
    PX.Objects.IN.InventoryItem item,
    IShipLineSource plan)
  {
    if (newline.ProjectID.HasValue && newline.TaskID.HasValue)
      return;
    Decimal valueOrDefault1 = plan.PlanQty.GetValueOrDefault();
    Decimal? nullable1 = newline.BaseShippedQty;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal num1 = valueOrDefault1 - valueOrDefault2;
    bool? stkItem = item.StkItem;
    bool flag = false;
    if (stkItem.GetValueOrDefault() == flag & stkItem.HasValue && item.KitItem.GetValueOrDefault())
    {
      if (newline.ShipComplete != "C")
        num1 = 1M;
      List<PX.Objects.IN.InventoryItem> inventoryItemList = new List<PX.Objects.IN.InventoryItem>();
      Decimal? nullable2 = new Decimal?();
      foreach (PXResult<INKitSpecStkDet, PX.Objects.IN.InventoryItem> pxResult in PXSelectBase<INKitSpecStkDet, PXSelectJoin<INKitSpecStkDet, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<INKitSpecStkDet.compInventoryID>>>, Where<INKitSpecStkDet.kitInventoryID, Equal<Required<INKitSpecStkDet.kitInventoryID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) newline.InventoryID
      }))
      {
        INKitSpecStkDet inKitSpecStkDet = PXResult<INKitSpecStkDet, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
        nullable1 = inKitSpecStkDet.DfltCompQty;
        if (!(nullable1.GetValueOrDefault() == 0M))
        {
          Tuple<Decimal, Decimal> itemAvailability = this.CalculateItemAvailability(inKitSpecStkDet.CompInventoryID, inKitSpecStkDet.CompSubItemID, newline.SiteID, newline.CostCenterID);
          Decimal num2 = num1;
          Decimal? dfltCompQty = inKitSpecStkDet.DfltCompQty;
          nullable1 = dfltCompQty.HasValue ? new Decimal?(num2 * dfltCompQty.GetValueOrDefault()) : new Decimal?();
          Decimal num3 = itemAvailability.Item1;
          if (nullable1.GetValueOrDefault() > num3 & nullable1.HasValue)
            return;
          Decimal num4 = itemAvailability.Item1;
          nullable1 = inKitSpecStkDet.DfltCompQty;
          Decimal num5 = nullable1.Value;
          Decimal num6 = Math.Floor(num4 / num5);
          if (nullable2.HasValue)
          {
            Decimal num7 = num6;
            nullable1 = nullable2;
            Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
            if (!(num7 < valueOrDefault3 & nullable1.HasValue))
              continue;
          }
          nullable2 = new Decimal?(num6);
        }
      }
      Decimal? nullable3 = nullable2;
      Decimal num8 = 0M;
      if (nullable3.GetValueOrDefault() <= num8 & nullable3.HasValue)
        return;
      foreach (PXResult<INKitSpecStkDet, PX.Objects.IN.InventoryItem> pxResult in PXSelectBase<INKitSpecStkDet, PXSelectJoin<INKitSpecStkDet, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<INKitSpecStkDet.compInventoryID>>>, Where<INKitSpecStkDet.kitInventoryID, Equal<Required<INKitSpecStkDet.kitInventoryID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) newline.InventoryID
      }))
      {
        INKitSpecStkDet inKitSpecStkDet = PXResult<INKitSpecStkDet, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
        nullable3 = inKitSpecStkDet.DfltCompQty;
        if (!(nullable3.GetValueOrDefault() == 0M))
        {
          Decimal num9 = this.CalculateItemAvailability(inKitSpecStkDet.CompInventoryID, inKitSpecStkDet.CompSubItemID, newline.SiteID, newline.CostCenterID).Item2;
          Decimal? nullable4 = nullable2;
          Decimal? dfltCompQty = inKitSpecStkDet.DfltCompQty;
          nullable3 = nullable4.HasValue & dfltCompQty.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * dfltCompQty.GetValueOrDefault()) : new Decimal?();
          Decimal valueOrDefault4 = nullable3.GetValueOrDefault();
          if (num9 < valueOrDefault4 & nullable3.HasValue)
            inventoryItemList.Add(PXResult<INKitSpecStkDet, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult));
        }
      }
      if (inventoryItemList.Count != 0)
      {
        StringBuilder stringBuilder = new StringBuilder(inventoryItemList[0].InventoryCD);
        for (int index = 1; index < inventoryItemList.Count; ++index)
          stringBuilder.Append(", " + inventoryItemList[index].InventoryCD);
        throw new PXException("Transfer from Sales-not-allowed location or Replenishment is required for item '{0}'.", new object[1]
        {
          (object) stringBuilder
        });
      }
    }
    else
    {
      Tuple<Decimal, Decimal> itemAvailability = this.CalculateItemAvailability(newline.InventoryID, newline.SubItemID, newline.SiteID, newline.CostCenterID);
      if (newline.ShipComplete != "C")
        num1 = 0M;
      if (!(num1 > itemAvailability.Item1) && itemAvailability.Item1 > 0M)
        throw new PXException("Transfer from Sales-not-allowed location or Replenishment is required for item '{0}'.", new object[1]
        {
          sender.GetValueExt<SOShipLine.inventoryID>((object) newline)
        });
    }
  }

  public virtual bool RemoveLineFromShipment(SOShipLine shipline, bool removeFlag)
  {
    if (removeFlag)
    {
      int? costCenterId = shipline.CostCenterID;
      int num = 0;
      if (!(costCenterId.GetValueOrDefault() == num & costCenterId.HasValue) && INCostCenter.PK.Find((PXGraph) this.Base, shipline.CostCenterID)?.CostLayerType == "S")
        PXTrace.WriteInformation("The quantity of the {0} special-order item in the {1} warehouse is not sufficient to fulfill the order.", new object[2]
        {
          ((PXSelectBase<SOShipLine>) this.Base.Transactions).GetValueExt<SOShipLine.inventoryID>(shipline),
          ((PXSelectBase<SOShipLine>) this.Base.Transactions).GetValueExt<SOShipLine.siteID>(shipline)
        });
      else if (PXAccess.FeatureInstalled<FeaturesSet.subItem>() && shipline != null && shipline.SubItemID.HasValue)
        PXTrace.WriteInformation("There is no stock available at the {1} warehouse for the {0} item with the {2} subitem.", new object[3]
        {
          ((PXSelectBase<SOShipLine>) this.Base.Transactions).GetValueExt<SOShipLine.inventoryID>(shipline),
          ((PXSelectBase<SOShipLine>) this.Base.Transactions).GetValueExt<SOShipLine.siteID>(shipline),
          ((PXSelectBase<SOShipLine>) this.Base.Transactions).GetValueExt<SOShipLine.subItemID>(shipline)
        });
      else
        PXTrace.WriteInformation("There is no stock available at the {1} warehouse for the {0} item.", new object[2]
        {
          ((PXSelectBase<SOShipLine>) this.Base.Transactions).GetValueExt<SOShipLine.inventoryID>(shipline),
          ((PXSelectBase<SOShipLine>) this.Base.Transactions).GetValueExt<SOShipLine.siteID>(shipline)
        });
      shipline.KeepManualFreight = new bool?(true);
      ((PXSelectBase<SOShipLine>) this.Base.Transactions).Delete(shipline);
      return true;
    }
    ((PXSelectBase) this.Base.Transactions).Cache.RaiseExceptionHandling<SOShipLine.shippedQty>((object) shipline, (object) null, (Exception) new PXSetPropertyException((IBqlTable) shipline, "There is no stock available for this item.", (PXErrorLevel) 3));
    return false;
  }

  public virtual void ShipNonStockKit(
    IShipLineSource plan,
    SOShipLine newline,
    ref Decimal? kitqty,
    ref object lastComponentID,
    ref bool HasSerialComponents)
  {
    object obj1 = (object) null;
    PX.Objects.IN.InventoryItem inventoryItem1;
    using (this.Base.LineSplittingExt.KitProcessingScope(PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, newline.InventoryID)))
    {
      foreach (PXResult<INKitSpecStkDet, PX.Objects.IN.InventoryItem, INLotSerClass> pxResult in PXSelectBase<INKitSpecStkDet, PXSelectJoin<INKitSpecStkDet, InnerJoin<PX.Objects.IN.InventoryItem, On<INKitSpecStkDet.FK.ComponentInventoryItem>, InnerJoin<INLotSerClass, On<PX.Objects.IN.InventoryItem.FK.LotSerialClass>>>, Where<INKitSpecStkDet.kitInventoryID, Equal<Required<INKitSpecStkDet.kitInventoryID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) newline.InventoryID
      }))
      {
        INKitSpecStkDet inKitSpecStkDet1;
        ((PXResult<INKitSpecStkDet, PX.Objects.IN.InventoryItem>) pxResult).Deconstruct(ref inKitSpecStkDet1, ref inventoryItem1);
        INKitSpecStkDet inKitSpecStkDet2 = inKitSpecStkDet1;
        PX.Objects.IN.InventoryItem inventoryItem2 = inventoryItem1;
        if (inventoryItem2.ItemStatus == "IN")
          throw new PXException("The '{0}' component of the kit is inactive.", new object[1]
          {
            (object) inventoryItem2.InventoryCD
          });
        SOShipLine soShipLine1 = this.Base.LineSplittingExt.Clone(newline);
        soShipLine1.IsStockItem = new bool?(true);
        soShipLine1.InventoryID = inKitSpecStkDet2.CompInventoryID;
        soShipLine1.SubItemID = inKitSpecStkDet2.CompSubItemID;
        soShipLine1.UOM = inKitSpecStkDet2.UOM;
        SOShipLine soShipLine2 = soShipLine1;
        Decimal? nullable1 = inKitSpecStkDet2.DfltCompQty;
        Decimal? nullable2 = plan.PlanQty;
        Decimal? nullable3;
        Decimal? nullable4;
        if (!(nullable1.HasValue & nullable2.HasValue))
        {
          nullable3 = new Decimal?();
          nullable4 = nullable3;
        }
        else
          nullable4 = new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault());
        soShipLine2.Qty = nullable4;
        this.Base.LineSplittingExt.RaiseRowDeleted(soShipLine1);
        IShipLineSource plan1 = (IShipLineSource) plan.Clone();
        IShipLineSource shipLineSource = plan1;
        PXCache cache = ((PXSelectBase) this.Base.Transactions).Cache;
        SOShipLine Row = soShipLine1;
        string uom = soShipLine1.UOM;
        nullable2 = soShipLine1.Qty;
        Decimal num1 = nullable2.Value;
        Decimal? nullable5 = new Decimal?(INUnitAttribute.ConvertToBase<SOShipLine.inventoryID>(cache, (object) Row, uom, num1, INPrecision.QUANTITY));
        shipLineSource.PlanQty = nullable5;
        if (soShipLine1.Operation == "R")
        {
          PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, soShipLine1.SiteID);
          if (inSite != null)
          {
            int? nullable6 = inSite.ReturnLocationID;
            if (!nullable6.HasValue)
              throw new PXException("RMA Location is not configured for warehouse {0}", new object[1]
              {
                (object) inSite.SiteCD
              });
            if (PXResult<INKitSpecStkDet, PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerTrack == "S")
            {
              int num2 = 0;
              while (true)
              {
                Decimal num3 = (Decimal) num2;
                nullable2 = soShipLine1.Qty;
                Decimal valueOrDefault = nullable2.GetValueOrDefault();
                if (num3 < valueOrDefault & nullable2.HasValue)
                {
                  SOShipLineSplit soShipLineSplit1 = SOShipLineSplit.FromSOShipLine(soShipLine1);
                  soShipLineSplit1.Qty = new Decimal?((Decimal) 1);
                  SOShipLineSplit soShipLineSplit2 = soShipLineSplit1;
                  nullable6 = new int?();
                  int? nullable7 = nullable6;
                  soShipLineSplit2.SplitLineNbr = nullable7;
                  soShipLineSplit1.LocationID = inSite.ReturnLocationID;
                  SOShipLineSplit soShipLineSplit3 = ((PXSelectBase<SOShipLineSplit>) this.Base.splits).Insert(soShipLineSplit1);
                  PXDefaultAttribute.SetPersistingCheck<SOShipLineSplit.lotSerialNbr>(((PXSelectBase) this.Base.splits).Cache, (object) soShipLineSplit3, (PXPersistingCheck) 2);
                  PXDefaultAttribute.SetPersistingCheck<SOShipLineSplit.expireDate>(((PXSelectBase) this.Base.splits).Cache, (object) soShipLineSplit3, (PXPersistingCheck) 2);
                  ++num2;
                }
                else
                  break;
              }
            }
            else
            {
              SOShipLineSplit soShipLineSplit4 = SOShipLineSplit.FromSOShipLine(soShipLine1);
              SOShipLineSplit soShipLineSplit5 = soShipLineSplit4;
              nullable6 = new int?();
              int? nullable8 = nullable6;
              soShipLineSplit5.SplitLineNbr = nullable8;
              soShipLineSplit4.LocationID = inSite.ReturnLocationID;
              SOShipLineSplit soShipLineSplit6 = ((PXSelectBase<SOShipLineSplit>) this.Base.splits).Insert(soShipLineSplit4);
              PXDefaultAttribute.SetPersistingCheck<SOShipLineSplit.lotSerialNbr>(((PXSelectBase) this.Base.splits).Cache, (object) soShipLineSplit6, (PXPersistingCheck) 2);
              PXDefaultAttribute.SetPersistingCheck<SOShipLineSplit.expireDate>(((PXSelectBase) this.Base.splits).Cache, (object) soShipLineSplit6, (PXPersistingCheck) 2);
            }
          }
        }
        else
        {
          Decimal? nullable9 = this.ShipAvailable(plan1, soShipLine1, new PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>(PXResult<INKitSpecStkDet, PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult), PXResult<INKitSpecStkDet, PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult)));
          nullable2 = plan1.PlanQty;
          Decimal num4 = 0M;
          if (!(nullable2.GetValueOrDefault() == num4 & nullable2.HasValue))
          {
            Decimal? planQty = plan1.PlanQty;
            Decimal? nullable10 = nullable9;
            Decimal? nullable11 = planQty.HasValue & nullable10.HasValue ? new Decimal?(planQty.GetValueOrDefault() - nullable10.GetValueOrDefault()) : new Decimal?();
            Decimal? nullable12 = plan.PlanQty;
            Decimal? nullable13;
            if (!(nullable11.HasValue & nullable12.HasValue))
            {
              nullable10 = new Decimal?();
              nullable13 = nullable10;
            }
            else
              nullable13 = new Decimal?(nullable11.GetValueOrDefault() * nullable12.GetValueOrDefault());
            nullable3 = nullable13;
            Decimal? nullable14 = plan1.PlanQty;
            Decimal? nullable15;
            if (!(nullable3.HasValue & nullable14.HasValue))
            {
              nullable12 = new Decimal?();
              nullable15 = nullable12;
            }
            else
              nullable15 = new Decimal?(nullable3.GetValueOrDefault() / nullable14.GetValueOrDefault());
            nullable2 = nullable15;
            nullable1 = kitqty;
            if (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
            {
              ref Decimal? local = ref kitqty;
              nullable12 = plan1.PlanQty;
              nullable11 = nullable9;
              Decimal? nullable16;
              if (!(nullable12.HasValue & nullable11.HasValue))
              {
                nullable10 = new Decimal?();
                nullable16 = nullable10;
              }
              else
                nullable16 = new Decimal?(nullable12.GetValueOrDefault() - nullable11.GetValueOrDefault());
              nullable14 = nullable16;
              nullable3 = plan.PlanQty;
              Decimal? nullable17;
              if (!(nullable14.HasValue & nullable3.HasValue))
              {
                nullable11 = new Decimal?();
                nullable17 = nullable11;
              }
              else
                nullable17 = new Decimal?(nullable14.GetValueOrDefault() * nullable3.GetValueOrDefault());
              nullable1 = nullable17;
              nullable2 = plan1.PlanQty;
              Decimal? nullable18;
              if (!(nullable1.HasValue & nullable2.HasValue))
              {
                nullable3 = new Decimal?();
                nullable18 = nullable3;
              }
              else
                nullable18 = new Decimal?(nullable1.GetValueOrDefault() / nullable2.GetValueOrDefault());
              local = nullable18;
              lastComponentID = (object) soShipLine1.InventoryID;
              obj1 = (object) soShipLine1.SubItemID;
            }
          }
        }
        HasSerialComponents |= PXResult<INKitSpecStkDet, PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerTrack == "S";
      }
    }
    foreach (PXResult<INKitSpecNonStkDet, PX.Objects.IN.InventoryItem> pxResult in PXSelectBase<INKitSpecNonStkDet, PXSelectJoin<INKitSpecNonStkDet, InnerJoin<PX.Objects.IN.InventoryItem, On<INKitSpecNonStkDet.FK.ComponentInventoryItem>>, Where<INKitSpecNonStkDet.kitInventoryID, Equal<Required<INKitSpecNonStkDet.kitInventoryID>>, And<Where<PX.Objects.IN.InventoryItem.kitItem, Equal<True>, Or<PX.Objects.IN.InventoryItem.nonStockShip, Equal<True>>>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) newline.InventoryID
    }))
    {
      INKitSpecNonStkDet kitSpecNonStkDet1;
      pxResult.Deconstruct(ref kitSpecNonStkDet1, ref inventoryItem1);
      INKitSpecNonStkDet kitSpecNonStkDet2 = kitSpecNonStkDet1;
      PX.Objects.IN.InventoryItem inventoryItem3 = inventoryItem1;
      SOShipLine soShipLine3 = this.Base.LineSplittingExt.Clone(newline);
      soShipLine3.IsStockItem = new bool?(false);
      soShipLine3.InventoryID = kitSpecNonStkDet2.CompInventoryID;
      soShipLine3.SubItemID = new int?();
      soShipLine3.UOM = kitSpecNonStkDet2.UOM;
      SOShipLine soShipLine4 = soShipLine3;
      Decimal? nullable19 = kitSpecNonStkDet2.DfltCompQty;
      Decimal? nullable20 = plan.PlanQty;
      Decimal? nullable21;
      Decimal? nullable22;
      if (!(nullable19.HasValue & nullable20.HasValue))
      {
        nullable21 = new Decimal?();
        nullable22 = nullable21;
      }
      else
        nullable22 = new Decimal?(nullable19.GetValueOrDefault() * nullable20.GetValueOrDefault());
      soShipLine4.Qty = nullable22;
      this.Base.LineSplittingExt.RaiseRowDeleted(soShipLine3);
      IShipLineSource plan2 = (IShipLineSource) plan.Clone();
      IShipLineSource shipLineSource = plan2;
      PXCache cache = ((PXSelectBase) this.Base.Transactions).Cache;
      SOShipLine Row = soShipLine3;
      string uom = soShipLine3.UOM;
      nullable20 = soShipLine3.Qty;
      Decimal num5 = nullable20.Value;
      Decimal? nullable23 = new Decimal?(INUnitAttribute.ConvertToBase<SOShipLine.inventoryID>(cache, (object) Row, uom, num5, INPrecision.QUANTITY));
      shipLineSource.PlanQty = nullable23;
      bool? nullable24 = inventoryItem3.StkItem;
      bool flag = false;
      if (nullable24.GetValueOrDefault() == flag & nullable24.HasValue)
      {
        nullable24 = inventoryItem3.KitItem;
        if (nullable24.GetValueOrDefault())
        {
          Decimal? planQty = plan2.PlanQty;
          this.ShipNonStockKit(plan2, soShipLine3, ref planQty, ref lastComponentID, ref HasSerialComponents);
          nullable20 = plan2.PlanQty;
          Decimal num6 = 0M;
          if (!(nullable20.GetValueOrDefault() == num6 & nullable20.HasValue))
          {
            Decimal? nullable25 = planQty;
            Decimal? nullable26 = plan.PlanQty;
            nullable21 = nullable25.HasValue & nullable26.HasValue ? new Decimal?(nullable25.GetValueOrDefault() * nullable26.GetValueOrDefault()) : new Decimal?();
            Decimal? nullable27 = plan2.PlanQty;
            Decimal? nullable28;
            if (!(nullable21.HasValue & nullable27.HasValue))
            {
              nullable26 = new Decimal?();
              nullable28 = nullable26;
            }
            else
              nullable28 = new Decimal?(nullable21.GetValueOrDefault() / nullable27.GetValueOrDefault());
            nullable20 = nullable28;
            nullable19 = kitqty;
            if (nullable20.GetValueOrDefault() < nullable19.GetValueOrDefault() & nullable20.HasValue & nullable19.HasValue)
            {
              ref Decimal? local = ref kitqty;
              nullable27 = planQty;
              nullable21 = plan.PlanQty;
              Decimal? nullable29;
              if (!(nullable27.HasValue & nullable21.HasValue))
              {
                nullable26 = new Decimal?();
                nullable29 = nullable26;
              }
              else
                nullable29 = new Decimal?(nullable27.GetValueOrDefault() * nullable21.GetValueOrDefault());
              nullable19 = nullable29;
              nullable20 = plan2.PlanQty;
              Decimal? nullable30;
              if (!(nullable19.HasValue & nullable20.HasValue))
              {
                nullable21 = new Decimal?();
                nullable30 = nullable21;
              }
              else
                nullable30 = new Decimal?(nullable19.GetValueOrDefault() / nullable20.GetValueOrDefault());
              local = nullable30;
              continue;
            }
            continue;
          }
          continue;
        }
      }
      this.ShipAvailable(plan2, soShipLine3, new PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>(PXResult<INKitSpecNonStkDet, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult), (INLotSerClass) null));
    }
    if (HasSerialComponents)
      kitqty = new Decimal?(Decimal.Floor(kitqty.Value));
    Decimal? nullable = kitqty;
    Decimal num = 0M;
    if (!(nullable.GetValueOrDefault() <= num & nullable.HasValue) || lastComponentID == null)
      return;
    object obj2 = lastComponentID;
    object obj3 = obj1;
    ((PXSelectBase) this.Base.Transactions).Cache.RaiseFieldSelecting<SOShipLine.inventoryID>((object) newline, ref obj2, true);
    ((PXSelectBase) this.Base.Transactions).Cache.RaiseFieldSelecting<SOShipLine.subItemID>((object) newline, ref obj3, true);
    if (PXAccess.FeatureInstalled<FeaturesSet.subItem>() && obj1 != null)
      PXTrace.WriteInformation("There is no stock available at the {1} warehouse for the {0} item with the {2} subitem.", new object[3]
      {
        obj2,
        ((PXSelectBase<SOShipLine>) this.Base.Transactions).GetValueExt<SOShipLine.siteID>(newline),
        obj3
      });
    else
      PXTrace.WriteInformation("There is no stock available at the {1} warehouse for the {0} item.", new object[2]
      {
        obj2,
        ((PXSelectBase<SOShipLine>) this.Base.Transactions).GetValueExt<SOShipLine.siteID>(newline)
      });
  }

  public virtual Decimal? ShipNonStock(IShipLineSource plan, SOShipLine newline)
  {
    Decimal? planQty = plan.PlanQty;
    SOShipLineSplit soShipLineSplit = SOShipLineSplit.FromSOShipLine(newline);
    soShipLineSplit.UOM = (string) null;
    soShipLineSplit.SplitLineNbr = new int?();
    soShipLineSplit.LocationID = (int?) PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, soShipLineSplit.SiteID)?.NonStockPickingLocationID;
    soShipLineSplit.Qty = planQty;
    soShipLineSplit.BaseQty = new Decimal?();
    ((PXSelectBase<SOShipLineSplit>) this.Base.splits).Insert(soShipLineSplit);
    return new Decimal?(0M);
  }

  public virtual Decimal? ShipAvailableNonLots(
    IShipLineSource plan,
    SOShipLine newline,
    INLotSerClass lotserclass)
  {
    return this.CreateSplitsForAvailableNonLots(plan.PlanQty, plan.PlanType, newline, lotserclass);
  }

  public virtual Decimal? ShipAvailableLots(
    IShipLineSource plan,
    SOShipLine newline,
    INLotSerClass lotserclass)
  {
    return this.CreateSplitsForAvailableLots(plan.PlanQty, plan.PlanType, plan.LotSerialNbr, newline, lotserclass);
  }

  private Tuple<Decimal, Decimal> CalculateItemAvailability(
    int? inventoryID,
    int? subItemID,
    int? siteID,
    int? costCenterID)
  {
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    INSiteStatusByCostCenter statusByCostCenter1 = PXResultset<INSiteStatusByCostCenter>.op_Implicit(PXSelectBase<INSiteStatusByCostCenter, PXSelectReadonly<INSiteStatusByCostCenter, Where<INSiteStatusByCostCenter.inventoryID, Equal<Required<INSiteStatusByCostCenter.inventoryID>>, And<INSiteStatusByCostCenter.siteID, Equal<Required<INSiteStatusByCostCenter.siteID>>, And<INSiteStatusByCostCenter.costCenterID, Equal<Required<INSiteStatusByCostCenter.costCenterID>>, And<Where<INSiteStatusByCostCenter.subItemID, Equal<Required<INSiteStatusByCostCenter.subItemID>>, Or<Required<INSiteStatusByCostCenter.subItemID>, IsNull>>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[0], new object[5]
    {
      (object) inventoryID,
      (object) siteID,
      (object) costCenterID,
      (object) subItemID,
      (object) subItemID
    }));
    Decimal num3 = 0M;
    Decimal? nullable;
    if (statusByCostCenter1 != null)
    {
      nullable = statusByCostCenter1.QtySOShipping;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = statusByCostCenter1.QtyFSSrvOrdAllocated;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num4 = valueOrDefault1 + valueOrDefault2;
      nullable = statusByCostCenter1.QtyProductionAllocated;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      num3 = -1M * (num4 + valueOrDefault3);
    }
    PXSelectReadonly2<INLocationStatusByCostCenter, InnerJoin<INLocation, On<INLocationStatusByCostCenter.FK.Location>>, Where<INLocationStatusByCostCenter.inventoryID, Equal<Required<INLocationStatusByCostCenter.inventoryID>>, And<INLocationStatusByCostCenter.siteID, Equal<Required<INLocationStatusByCostCenter.siteID>>, And<INLocationStatusByCostCenter.costCenterID, Equal<Required<INLocationStatusByCostCenter.costCenterID>>, And<INLocation.inclQtyAvail, Equal<boolTrue>>>>>, OrderBy<Asc<INLocation.pickPriority>>> pxSelectReadonly2 = new PXSelectReadonly2<INLocationStatusByCostCenter, InnerJoin<INLocation, On<INLocationStatusByCostCenter.FK.Location>>, Where<INLocationStatusByCostCenter.inventoryID, Equal<Required<INLocationStatusByCostCenter.inventoryID>>, And<INLocationStatusByCostCenter.siteID, Equal<Required<INLocationStatusByCostCenter.siteID>>, And<INLocationStatusByCostCenter.costCenterID, Equal<Required<INLocationStatusByCostCenter.costCenterID>>, And<INLocation.inclQtyAvail, Equal<boolTrue>>>>>, OrderBy<Asc<INLocation.pickPriority>>>((PXGraph) this.Base);
    object[] objArray;
    if (PXAccess.FeatureInstalled<FeaturesSet.subItem>())
    {
      ((PXSelectBase<INLocationStatusByCostCenter>) pxSelectReadonly2).WhereAnd<Where<INLocationStatusByCostCenter.subItemID, Equal<Required<INLocationStatusByCostCenter.subItemID>>>>();
      objArray = new object[4]
      {
        (object) inventoryID,
        (object) siteID,
        (object) costCenterID,
        (object) subItemID
      };
    }
    else
      objArray = new object[3]
      {
        (object) inventoryID,
        (object) siteID,
        (object) costCenterID
      };
    foreach (PXResult<INLocationStatusByCostCenter, INLocation> pxResult in ((PXSelectBase<INLocationStatusByCostCenter>) pxSelectReadonly2).Select(objArray))
    {
      INLocation inLocation = PXResult<INLocationStatusByCostCenter, INLocation>.op_Implicit(pxResult);
      INLocationStatusByCostCenter statusByCostCenter2 = PXResult<INLocationStatusByCostCenter, INLocation>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter statusByCostCenter3 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter();
      PXCache<INLocationStatusByCostCenter>.RestoreCopy((INLocationStatusByCostCenter) statusByCostCenter3, statusByCostCenter2);
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter statusByCostCenter4 = (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter) ((PXGraph) this.Base).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter)].Insert((object) statusByCostCenter3);
      Decimal num5 = num3;
      nullable = statusByCostCenter2.QtySOShipping;
      Decimal valueOrDefault4 = nullable.GetValueOrDefault();
      nullable = statusByCostCenter2.QtyFSSrvOrdAllocated;
      Decimal valueOrDefault5 = nullable.GetValueOrDefault();
      Decimal num6 = valueOrDefault4 + valueOrDefault5;
      nullable = statusByCostCenter2.QtyProductionAllocated;
      Decimal valueOrDefault6 = nullable.GetValueOrDefault();
      Decimal num7 = num6 + valueOrDefault6;
      num3 = num5 + num7;
      nullable = statusByCostCenter2.QtyHardAvail;
      Decimal valueOrDefault7 = nullable.GetValueOrDefault();
      nullable = statusByCostCenter4.QtyHardAvail;
      Decimal valueOrDefault8 = nullable.GetValueOrDefault();
      Decimal num8 = valueOrDefault7 + valueOrDefault8;
      num1 += num8;
      if (inLocation.SalesValid.GetValueOrDefault())
        num2 += num8;
    }
    return new Tuple<Decimal, Decimal>(num1 + num3, num2 + num3);
  }

  public virtual Decimal? CreateSplitsForAvailableNonLots(
    Decimal? plannedQty,
    string origPlanType,
    SOShipLine newline,
    INLotSerClass lotserclass)
  {
    List<PXResult> resultset = this.SelectLocationStatus(newline);
    this.ResortStockForShipment(newline, resultset);
    Decimal? nullable1 = plannedQty;
    Decimal? baseShippedQty = newline.BaseShippedQty;
    bool flag1 = nullable1.GetValueOrDefault() >= baseShippedQty.GetValueOrDefault() & nullable1.HasValue & baseShippedQty.HasValue;
    int num1 = 0;
    int? nullable2 = new int?();
    int? nullable3 = new int?();
    PXCache cach1 = ((PXGraph) this.Base).Caches[typeof (INLocationStatusByCostCenter)];
    PXCache cach2 = ((PXGraph) this.Base).Caches[typeof (INSiteStatusByCostCenter)];
    foreach (PXResult<INLocationStatusByCostCenter, INLocation, INSiteStatusByCostCenter> pxResult in resultset)
    {
      INLocation inLocation = PXResult<INLocationStatusByCostCenter, INLocation, INSiteStatusByCostCenter>.op_Implicit(pxResult);
      int? nullable4;
      int? nullable5;
      if (num1 > 0)
      {
        nullable4 = newline.TaskID;
        if (nullable4.HasValue)
        {
          nullable4 = nullable3;
          nullable5 = inLocation.TaskID;
          if (!(nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue))
            continue;
        }
      }
      INLocationStatusByCostCenter statusByCostCenter1 = PXResult<INLocationStatusByCostCenter, INLocation, INSiteStatusByCostCenter>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter statusByCostCenter2 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter();
      cach1.RestoreCopy((object) statusByCostCenter2, (object) statusByCostCenter1);
      INSiteStatusByCostCenter statusByCostCenter3 = PXResult<INLocationStatusByCostCenter, INLocation, INSiteStatusByCostCenter>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter4 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter();
      cach2.RestoreCopy((object) statusByCostCenter4, (object) statusByCostCenter3);
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter statusByCostCenter5 = (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter) ((PXGraph) this.Base).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter)].Insert((object) statusByCostCenter2);
      PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter6 = (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter) ((PXGraph) this.Base).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter)].Insert((object) statusByCostCenter4);
      Decimal? nullable6 = statusByCostCenter1.QtyHardAvail;
      Decimal? nullable7 = statusByCostCenter5.QtyHardAvail;
      Decimal? nullable8 = nullable6.HasValue & nullable7.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + nullable7.GetValueOrDefault()) : new Decimal?();
      nullable7 = statusByCostCenter3.QtyHardAvail;
      nullable6 = statusByCostCenter6.QtyHardAvail;
      Decimal? nullable9 = nullable7.HasValue & nullable6.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
      nullable6 = nullable9;
      nullable7 = nullable8;
      Decimal? availableQty = !(nullable6.GetValueOrDefault() < nullable7.GetValueOrDefault() & nullable6.HasValue & nullable7.HasValue) || INPlanConstants.IsAllocated(origPlanType) ? nullable8 : nullable9;
      nullable7 = availableQty;
      Decimal num2 = 0M;
      if (!(nullable7.GetValueOrDefault() <= num2 & nullable7.HasValue))
      {
        this.InsertSplitsForNonLotsOnLocation(newline, lotserclass, inLocation.LocationID, availableQty, plannedQty);
        if (num1 == 0)
        {
          nullable5 = newline.TaskID;
          if (nullable5.HasValue)
            nullable3 = inLocation.TaskID;
          nullable2 = inLocation.LocationID;
        }
        else
        {
          nullable5 = nullable2;
          nullable4 = inLocation.LocationID;
          if (!(nullable5.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable5.HasValue == nullable4.HasValue))
            nullable2 = new int?();
        }
        ++num1;
        nullable7 = availableQty;
        nullable6 = plannedQty;
        if (nullable7.GetValueOrDefault() < nullable6.GetValueOrDefault() & nullable7.HasValue & nullable6.HasValue)
        {
          nullable6 = plannedQty;
          nullable7 = availableQty;
          plannedQty = nullable6.HasValue & nullable7.HasValue ? new Decimal?(nullable6.GetValueOrDefault() - nullable7.GetValueOrDefault()) : new Decimal?();
        }
        else
        {
          plannedQty = new Decimal?(0M);
          break;
        }
      }
    }
    Decimal? nullable10 = plannedQty;
    Decimal num3 = 0M;
    bool? nullable11;
    if (nullable10.GetValueOrDefault() > num3 & nullable10.HasValue && (lotserclass.LotSerTrack == "N" || lotserclass.LotSerAssign == "U"))
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, newline.InventoryID);
      if (inventoryItem != null)
      {
        nullable11 = inventoryItem.NegQty;
        if (nullable11.GetValueOrDefault())
        {
          nullable11 = this.ShipFullIfNegQtyAllowed(newline);
          if (nullable11.GetValueOrDefault())
          {
            int? notAvailableStock = this.GetLocationIDForNotAvailableStock(inventoryItem, newline.SiteID);
            if (!notAvailableStock.HasValue)
              throw new PXException("The shipment cannot be created for the negative item quantity because the default shipping location is not defined for the item {0}.", new object[1]
              {
                (object) inventoryItem.InventoryCD
              });
            bool flag2 = true;
            int? nullable12;
            if (num1 > 0)
            {
              INLocation inLocation = INLocation.PK.Find((PXGraph) this.Base, notAvailableStock);
              int num4;
              if (inLocation == null)
              {
                num4 = !nullable3.HasValue ? 1 : 0;
              }
              else
              {
                nullable12 = inLocation.TaskID;
                int? nullable13 = nullable3;
                num4 = nullable12.GetValueOrDefault() == nullable13.GetValueOrDefault() & nullable12.HasValue == nullable13.HasValue ? 1 : 0;
              }
              flag2 = num4 != 0;
            }
            if (flag2)
            {
              this.InsertSplitsForNonLotsOnLocation(newline, lotserclass, notAvailableStock, plannedQty, plannedQty);
              plannedQty = new Decimal?(0M);
              if (num1 == 0)
              {
                nullable2 = notAvailableStock;
              }
              else
              {
                int? nullable14 = nullable2;
                nullable12 = notAvailableStock;
                if (!(nullable14.GetValueOrDefault() == nullable12.GetValueOrDefault() & nullable14.HasValue == nullable12.HasValue))
                  nullable2 = new int?();
              }
            }
          }
        }
      }
    }
    nullable11 = newline.IsUnassigned;
    if (nullable11.GetValueOrDefault() & flag1 && nullable2.HasValue)
      ((PXSelectBase) this.Base.Transactions).Cache.SetValue<SOShipLine.locationID>((object) newline, (object) nullable2);
    return plannedQty;
  }

  public virtual Decimal? CreateSplitsForAvailableLots(
    Decimal? plannedQty,
    string origPlanType,
    string origLotSerialNbr,
    SOShipLine newline,
    INLotSerClass lotserclass)
  {
    if (lotserclass.LotSerTrack == "S")
      plannedQty = new Decimal?(Math.Floor(plannedQty.Value));
    List<PXResult> resultset = this.SelectLotSerialStatus(origLotSerialNbr, newline, lotserclass);
    this.ResortStockForShipment(newline, resultset);
    PXCache cach1 = ((PXGraph) this.Base).Caches[typeof (INLotSerialStatusByCostCenter)];
    PXCache cach2 = ((PXGraph) this.Base).Caches[typeof (INSiteStatusByCostCenter)];
    PXCache cach3 = ((PXGraph) this.Base).Caches[typeof (INSiteLotSerial)];
    Decimal? nullable1 = plannedQty;
    Decimal? baseShippedQty = newline.BaseShippedQty;
    bool flag = nullable1.GetValueOrDefault() >= baseShippedQty.GetValueOrDefault() & nullable1.HasValue & baseShippedQty.HasValue;
    int num1 = 0;
    int? nullable2 = new int?();
    int? nullable3 = new int?();
    if (string.IsNullOrEmpty(origLotSerialNbr))
    {
      foreach (PXResult<INLotSerialStatusByCostCenter, INLocation, INSiteStatusByCostCenter, INSiteLotSerial> pxResult in resultset)
      {
        INLocation inLocation = PXResult<INLotSerialStatusByCostCenter, INLocation, INSiteStatusByCostCenter, INSiteLotSerial>.op_Implicit(pxResult);
        if (num1 > 0 && newline.TaskID.HasValue)
        {
          int? nullable4 = nullable3;
          int? taskId = inLocation.TaskID;
          if (!(nullable4.GetValueOrDefault() == taskId.GetValueOrDefault() & nullable4.HasValue == taskId.HasValue))
            continue;
        }
        INLotSerialStatusByCostCenter statusByCostCenter1 = PXResult<INLotSerialStatusByCostCenter, INLocation, INSiteStatusByCostCenter, INSiteLotSerial>.op_Implicit(pxResult);
        INSiteLotSerial inSiteLotSerial = PXResult<INLotSerialStatusByCostCenter, INLocation, INSiteStatusByCostCenter, INSiteLotSerial>.op_Implicit(pxResult);
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter statusByCostCenter2 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter();
        cach1.RestoreCopy((object) statusByCostCenter2, (object) statusByCostCenter1);
        SiteLotSerial siteLotSerial1 = new SiteLotSerial();
        cach3.RestoreCopy((object) siteLotSerial1, (object) inSiteLotSerial);
        SiteLotSerial siteLotSerial2 = (SiteLotSerial) ((PXGraph) this.Base).Caches[typeof (SiteLotSerial)].Insert((object) siteLotSerial1);
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter statusByCostCenter3 = (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter) ((PXGraph) this.Base).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter)].Insert((object) statusByCostCenter2);
        INSiteStatusByCostCenter statusByCostCenter4 = PXResult<INLotSerialStatusByCostCenter, INLocation, INSiteStatusByCostCenter, INSiteLotSerial>.op_Implicit(pxResult);
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter5 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter();
        cach2.RestoreCopy((object) statusByCostCenter5, (object) statusByCostCenter4);
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter6 = (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter) ((PXGraph) this.Base).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter)].Insert((object) statusByCostCenter5);
        Decimal? nullable5 = new Decimal?(0M);
        Decimal? nullable6 = inSiteLotSerial.QtyHardAvail;
        Decimal? nullable7 = siteLotSerial2.QtyHardAvail;
        Decimal? nullable8 = nullable6.HasValue & nullable7.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + nullable7.GetValueOrDefault()) : new Decimal?();
        nullable7 = statusByCostCenter1.QtyHardAvail;
        nullable6 = statusByCostCenter3.QtyHardAvail;
        Decimal? nullable9 = nullable7.HasValue & nullable6.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
        nullable6 = statusByCostCenter4.QtyHardAvail;
        nullable7 = statusByCostCenter6.QtyHardAvail;
        Decimal? nullable10 = nullable6.HasValue & nullable7.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + nullable7.GetValueOrDefault()) : new Decimal?();
        nullable5 = INPlanConstants.IsAllocated(origPlanType) ? new Decimal?(Math.Min(nullable8.GetValueOrDefault(), nullable9.GetValueOrDefault())) : new Decimal?(Math.Min(nullable10.GetValueOrDefault(), Math.Min(nullable8.GetValueOrDefault(), nullable9.GetValueOrDefault())));
        nullable7 = nullable5;
        Decimal num2 = 0M;
        if (!(nullable7.GetValueOrDefault() <= num2 & nullable7.HasValue))
        {
          bool? isUnassigned = newline.IsUnassigned;
          IBqlTable ibqlTable1 = isUnassigned.GetValueOrDefault() ? (IBqlTable) newline.ToUnassignedSplit() : (IBqlTable) SOShipLineSplit.FromSOShipLine(newline);
          isUnassigned = newline.IsUnassigned;
          PXCache pxCache = isUnassigned.GetValueOrDefault() ? ((PXSelectBase) this.Base.unassignedSplits).Cache : ((PXSelectBase) this.Base.splits).Cache;
          pxCache.SetValue<SOShipLineSplit.uOM>((object) ibqlTable1, (object) null);
          pxCache.SetValue<SOShipLineSplit.splitLineNbr>((object) ibqlTable1, (object) null);
          pxCache.SetValue<SOShipLineSplit.locationID>((object) ibqlTable1, (object) statusByCostCenter1.LocationID);
          IBqlTable ibqlTable2 = ibqlTable1;
          isUnassigned = newline.IsUnassigned;
          string str = isUnassigned.GetValueOrDefault() ? string.Empty : statusByCostCenter1.LotSerialNbr;
          pxCache.SetValue<SOShipLineSplit.lotSerialNbr>((object) ibqlTable2, (object) str);
          pxCache.SetValue<SOShipLineSplit.expireDate>((object) ibqlTable1, (object) statusByCostCenter1.ExpireDate);
          pxCache.SetValue<SOShipLineSplit.isUnassigned>((object) ibqlTable1, (object) newline.IsUnassigned);
          IBqlTable ibqlTable3 = ibqlTable1;
          nullable7 = nullable5;
          nullable6 = plannedQty;
          // ISSUE: variable of a boxed type
          __Boxed<Decimal?> local = (ValueType) (nullable7.GetValueOrDefault() < nullable6.GetValueOrDefault() & nullable7.HasValue & nullable6.HasValue ? nullable5 : plannedQty);
          pxCache.SetValue<SOShipLineSplit.qty>((object) ibqlTable3, (object) local);
          pxCache.SetValue<SOShipLineSplit.baseQty>((object) ibqlTable1, (object) null);
          pxCache.Insert((object) ibqlTable1);
          if (num1 == 0)
          {
            if (newline.TaskID.HasValue)
              nullable3 = inLocation.TaskID;
            nullable2 = inLocation.LocationID;
          }
          else
          {
            int? nullable11 = nullable2;
            int? locationId = inLocation.LocationID;
            if (!(nullable11.GetValueOrDefault() == locationId.GetValueOrDefault() & nullable11.HasValue == locationId.HasValue))
              nullable2 = new int?();
          }
          ++num1;
          nullable6 = nullable5;
          nullable7 = plannedQty;
          if (nullable6.GetValueOrDefault() < nullable7.GetValueOrDefault() & nullable6.HasValue & nullable7.HasValue)
          {
            nullable7 = plannedQty;
            nullable6 = nullable5;
            plannedQty = nullable7.HasValue & nullable6.HasValue ? new Decimal?(nullable7.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
          }
          else
          {
            plannedQty = new Decimal?(0M);
            break;
          }
        }
      }
    }
    else
    {
      foreach (PXResult<INLotSerialStatusByCostCenter, INLocation, INSiteStatusByCostCenter> pxResult in resultset)
      {
        INLocation inLocation = PXResult<INLotSerialStatusByCostCenter, INLocation, INSiteStatusByCostCenter>.op_Implicit(pxResult);
        if (num1 > 0 && newline.TaskID.HasValue)
        {
          int? nullable12 = nullable3;
          int? taskId = inLocation.TaskID;
          if (!(nullable12.GetValueOrDefault() == taskId.GetValueOrDefault() & nullable12.HasValue == taskId.HasValue))
            continue;
        }
        INLotSerialStatusByCostCenter statusByCostCenter7 = PXResult<INLotSerialStatusByCostCenter, INLocation, INSiteStatusByCostCenter>.op_Implicit(pxResult);
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter statusByCostCenter8 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter();
        cach1.RestoreCopy((object) statusByCostCenter8, (object) statusByCostCenter7);
        INSiteStatusByCostCenter statusByCostCenter9 = PXResult<INLotSerialStatusByCostCenter, INLocation, INSiteStatusByCostCenter>.op_Implicit(pxResult);
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter10 = new PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter();
        cach2.RestoreCopy((object) statusByCostCenter10, (object) statusByCostCenter9);
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter statusByCostCenter11 = (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter) ((PXGraph) this.Base).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter)].Insert((object) statusByCostCenter8);
        PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter12 = (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter) ((PXGraph) this.Base).Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter)].Insert((object) statusByCostCenter10);
        Decimal? nullable13 = statusByCostCenter7.QtyHardAvail;
        Decimal? nullable14 = statusByCostCenter11.QtyHardAvail;
        Decimal? nullable15 = nullable13.HasValue & nullable14.HasValue ? new Decimal?(nullable13.GetValueOrDefault() + nullable14.GetValueOrDefault()) : new Decimal?();
        nullable14 = statusByCostCenter9.QtyHardAvail;
        nullable13 = statusByCostCenter12.QtyHardAvail;
        Decimal? nullable16 = nullable14.HasValue & nullable13.HasValue ? new Decimal?(nullable14.GetValueOrDefault() + nullable13.GetValueOrDefault()) : new Decimal?();
        nullable13 = nullable16;
        nullable14 = nullable15;
        Decimal? nullable17 = !(nullable13.GetValueOrDefault() < nullable14.GetValueOrDefault() & nullable13.HasValue & nullable14.HasValue) || INPlanConstants.IsAllocated(origPlanType) ? nullable15 : nullable16;
        nullable14 = nullable17;
        Decimal num3 = 0M;
        if (!(nullable14.GetValueOrDefault() <= num3 & nullable14.HasValue))
        {
          bool? isUnassigned = newline.IsUnassigned;
          IBqlTable ibqlTable4 = isUnassigned.GetValueOrDefault() ? (IBqlTable) newline.ToUnassignedSplit() : (IBqlTable) SOShipLineSplit.FromSOShipLine(newline);
          isUnassigned = newline.IsUnassigned;
          PXCache pxCache = isUnassigned.GetValueOrDefault() ? ((PXSelectBase) this.Base.unassignedSplits).Cache : ((PXSelectBase) this.Base.splits).Cache;
          pxCache.SetValue<SOShipLineSplit.uOM>((object) ibqlTable4, (object) null);
          pxCache.SetValue<SOShipLineSplit.splitLineNbr>((object) ibqlTable4, (object) null);
          pxCache.SetValue<SOShipLineSplit.locationID>((object) ibqlTable4, (object) statusByCostCenter7.LocationID);
          pxCache.SetValue<SOShipLineSplit.lotSerialNbr>((object) ibqlTable4, (object) statusByCostCenter7.LotSerialNbr);
          pxCache.SetValue<SOShipLineSplit.expireDate>((object) ibqlTable4, (object) statusByCostCenter7.ExpireDate);
          pxCache.SetValue<SOShipLineSplit.isUnassigned>((object) ibqlTable4, (object) newline.IsUnassigned);
          IBqlTable ibqlTable5 = ibqlTable4;
          nullable14 = nullable17;
          nullable13 = plannedQty;
          // ISSUE: variable of a boxed type
          __Boxed<Decimal?> local = (ValueType) (nullable14.GetValueOrDefault() < nullable13.GetValueOrDefault() & nullable14.HasValue & nullable13.HasValue ? nullable17 : plannedQty);
          pxCache.SetValue<SOShipLineSplit.qty>((object) ibqlTable5, (object) local);
          pxCache.SetValue<SOShipLineSplit.baseQty>((object) ibqlTable4, (object) null);
          pxCache.Insert((object) ibqlTable4);
          if (num1 == 0)
          {
            if (newline.TaskID.HasValue)
              nullable3 = inLocation.TaskID;
            nullable2 = inLocation.LocationID;
          }
          else
          {
            int? nullable18 = nullable2;
            int? locationId = inLocation.LocationID;
            if (!(nullable18.GetValueOrDefault() == locationId.GetValueOrDefault() & nullable18.HasValue == locationId.HasValue))
              nullable2 = new int?();
          }
          ++num1;
          nullable13 = nullable17;
          nullable14 = plannedQty;
          if (nullable13.GetValueOrDefault() < nullable14.GetValueOrDefault() & nullable13.HasValue & nullable14.HasValue)
          {
            nullable14 = plannedQty;
            nullable13 = nullable17;
            plannedQty = nullable14.HasValue & nullable13.HasValue ? new Decimal?(nullable14.GetValueOrDefault() - nullable13.GetValueOrDefault()) : new Decimal?();
          }
          else
          {
            plannedQty = new Decimal?(0M);
            break;
          }
        }
      }
    }
    if (newline.IsUnassigned.GetValueOrDefault() & flag && nullable2.HasValue)
      ((PXSelectBase) this.Base.Transactions).Cache.SetValue<SOShipLine.locationID>((object) newline, (object) nullable2);
    return plannedQty;
  }

  protected virtual List<PXResult> SelectLocationStatus(SOShipLine newline)
  {
    PXSelectReadonly2<INLocationStatusByCostCenter, InnerJoin<INLocation, On<INLocationStatusByCostCenter.FK.Location>, LeftJoin<INSiteStatusByCostCenter, On<INSiteStatusByCostCenter.inventoryID, Equal<INLocationStatusByCostCenter.inventoryID>, And<INSiteStatusByCostCenter.subItemID, Equal<INLocationStatusByCostCenter.subItemID>, And<INSiteStatusByCostCenter.siteID, Equal<INLocationStatusByCostCenter.siteID>, And<INSiteStatusByCostCenter.costCenterID, Equal<INLocationStatusByCostCenter.costCenterID>>>>>>>, Where<INLocationStatusByCostCenter.inventoryID, Equal<Required<INLocationStatusByCostCenter.inventoryID>>, And<INLocationStatusByCostCenter.siteID, Equal<Required<INLocationStatusByCostCenter.siteID>>, And<INLocationStatusByCostCenter.costCenterID, Equal<Required<INLocationStatusByCostCenter.costCenterID>>, And<INLocation.salesValid, Equal<boolTrue>, And<INLocation.inclQtyAvail, Equal<boolTrue>>>>>>, OrderBy<Asc<INLocation.pickPriority, Asc<INLocation.locationCD>>>> select = new PXSelectReadonly2<INLocationStatusByCostCenter, InnerJoin<INLocation, On<INLocationStatusByCostCenter.FK.Location>, LeftJoin<INSiteStatusByCostCenter, On<INSiteStatusByCostCenter.inventoryID, Equal<INLocationStatusByCostCenter.inventoryID>, And<INSiteStatusByCostCenter.subItemID, Equal<INLocationStatusByCostCenter.subItemID>, And<INSiteStatusByCostCenter.siteID, Equal<INLocationStatusByCostCenter.siteID>, And<INSiteStatusByCostCenter.costCenterID, Equal<INLocationStatusByCostCenter.costCenterID>>>>>>>, Where<INLocationStatusByCostCenter.inventoryID, Equal<Required<INLocationStatusByCostCenter.inventoryID>>, And<INLocationStatusByCostCenter.siteID, Equal<Required<INLocationStatusByCostCenter.siteID>>, And<INLocationStatusByCostCenter.costCenterID, Equal<Required<INLocationStatusByCostCenter.costCenterID>>, And<INLocation.salesValid, Equal<boolTrue>, And<INLocation.inclQtyAvail, Equal<boolTrue>>>>>>, OrderBy<Asc<INLocation.pickPriority, Asc<INLocation.locationCD>>>>((PXGraph) this.Base);
    List<object> parameters = new List<object>(8)
    {
      (object) newline.InventoryID,
      (object) newline.SiteID,
      (object) newline.CostCenterID
    };
    if (PXAccess.FeatureInstalled<FeaturesSet.subItem>())
    {
      ((PXSelectBase<INLocationStatusByCostCenter>) select).WhereAnd<Where<INLocationStatusByCostCenter.subItemID, Equal<Required<INLocationStatusByCostCenter.subItemID>>>>();
      parameters.Add((object) newline.SubItemID);
    }
    this.AppendFiltersForStatusSelect<INLocationStatusByCostCenter>(newline, (PXSelectBase<INLocationStatusByCostCenter>) select, parameters);
    return ((IEnumerable<PXResult<INLocationStatusByCostCenter>>) ((PXSelectBase<INLocationStatusByCostCenter>) select).Select(parameters.ToArray())).AsEnumerable<PXResult<INLocationStatusByCostCenter>>().Cast<PXResult>().ToList<PXResult>();
  }

  protected virtual void ResortStockForShipment(SOShipLine newline, List<PXResult> resultset)
  {
    this.ResortStockForShipmentByDefaultItemLocation(newline, resultset);
    this.ResortStockForShipmentByProjectAndTask(newline, resultset);
  }

  public virtual void InsertSplitsForNonLotsOnLocation(
    SOShipLine newline,
    INLotSerClass lotserclass,
    int? locationID,
    Decimal? availableQty,
    Decimal? plannedQty)
  {
    IBqlTable ibqlTable = newline.IsUnassigned.GetValueOrDefault() ? (IBqlTable) newline.ToUnassignedSplit() : (IBqlTable) SOShipLineSplit.FromSOShipLine(newline);
    PXCache pxCache = newline.IsUnassigned.GetValueOrDefault() ? ((PXSelectBase) this.Base.unassignedSplits).Cache : ((PXSelectBase) this.Base.splits).Cache;
    pxCache.SetValue<SOShipLineSplit.uOM>((object) ibqlTable, (object) null);
    pxCache.SetValue<SOShipLineSplit.splitLineNbr>((object) ibqlTable, (object) null);
    pxCache.SetValue<SOShipLineSplit.locationID>((object) ibqlTable, (object) locationID);
    pxCache.SetValue<SOShipLineSplit.isUnassigned>((object) ibqlTable, (object) newline.IsUnassigned);
    bool? nullable1 = newline.IsUnassigned;
    if (nullable1.GetValueOrDefault())
      pxCache.SetValue<SOShipLineSplit.lotSerialNbr>((object) ibqlTable, (object) string.Empty);
    nullable1 = newline.IsClone;
    bool flag = false;
    if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
      PXParentAttribute.SetParent(pxCache, (object) ibqlTable, typeof (SOShipLine), (object) newline);
    Decimal? nullable2 = availableQty;
    Decimal? nullable3 = plannedQty;
    Decimal? nullable4 = nullable2.GetValueOrDefault() < nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue ? availableQty : plannedQty;
    if (lotserclass.LotSerTrack == "S")
    {
      if (!(lotserclass.LotSerAssign != "U"))
      {
        if (newline.ShipmentType != "T")
        {
          nullable1 = newline.IsIntercompany;
          if (nullable1.GetValueOrDefault())
            goto label_11;
        }
        else
          goto label_11;
      }
      pxCache.SetValue<SOShipLineSplit.baseQty>((object) ibqlTable, (object) 1M);
      pxCache.SetValue<SOShipLineSplit.qty>((object) ibqlTable, (object) 1M);
      for (int index = 0; index < (int) nullable4.Value; ++index)
        pxCache.Insert((object) ibqlTable);
      return;
    }
label_11:
    pxCache.SetValue<SOShipLineSplit.qty>((object) ibqlTable, (object) nullable4);
    pxCache.SetValue<SOShipLineSplit.baseQty>((object) ibqlTable, (object) null);
    pxCache.Insert((object) ibqlTable);
  }

  protected virtual bool? ShipFullIfNegQtyAllowed(SOShipLine newline) => new bool?(true);

  public virtual int? GetLocationIDForNotAvailableStock(PX.Objects.IN.InventoryItem item, int? siteID)
  {
    INItemSite inItemSite = PXResultset<INItemSite>.op_Implicit(PXSelectBase<INItemSite, PXSelectReadonly<INItemSite, Where<INItemSite.inventoryID, Equal<Required<INItemSite.inventoryID>>, And<INItemSite.siteID, Equal<Required<INItemSite.siteID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) item.InventoryID,
      (object) siteID
    }));
    int? nullable1;
    if (inItemSite != null)
    {
      nullable1 = inItemSite.DfltShipLocationID;
      if (nullable1.HasValue)
        return inItemSite.DfltShipLocationID;
    }
    PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, siteID);
    InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find((PXGraph) this.Base, item.InventoryID, inSite?.BaseCuryID);
    int num;
    if (itemCurySettings == null)
    {
      num = !siteID.HasValue ? 1 : 0;
    }
    else
    {
      nullable1 = itemCurySettings.DfltSiteID;
      int? nullable2 = siteID;
      num = nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue ? 1 : 0;
    }
    int? notAvailableStock;
    if (num != 0)
    {
      notAvailableStock = itemCurySettings.DfltShipLocationID;
      if (notAvailableStock.HasValue)
        return itemCurySettings.DfltShipLocationID;
    }
    if (inSite != null)
      return inSite.ShipLocationID;
    notAvailableStock = new int?();
    return notAvailableStock;
  }

  protected virtual List<PXResult> SelectLotSerialStatus(
    string origLotSerialNbr,
    SOShipLine newline,
    INLotSerClass lotserclass)
  {
    PXSelectBase<INLotSerialStatusByCostCenter> pxSelectBase = string.IsNullOrEmpty(origLotSerialNbr) ? (PXSelectBase<INLotSerialStatusByCostCenter>) new PXSelectReadonly2<INLotSerialStatusByCostCenter, InnerJoin<INLocation, On<INLotSerialStatusByCostCenter.FK.Location>, LeftJoin<INSiteStatusByCostCenter, On<INSiteStatusByCostCenter.inventoryID, Equal<INLotSerialStatusByCostCenter.inventoryID>, And<INSiteStatusByCostCenter.subItemID, Equal<INLotSerialStatusByCostCenter.subItemID>, And<INSiteStatusByCostCenter.siteID, Equal<INLotSerialStatusByCostCenter.siteID>, And<INSiteStatusByCostCenter.costCenterID, Equal<INLotSerialStatusByCostCenter.costCenterID>>>>>, InnerJoin<INSiteLotSerial, On<INSiteLotSerial.inventoryID, Equal<INLotSerialStatusByCostCenter.inventoryID>, And<INSiteLotSerial.siteID, Equal<INLotSerialStatusByCostCenter.siteID>, And<INSiteLotSerial.lotSerialNbr, Equal<INLotSerialStatusByCostCenter.lotSerialNbr>>>>>>>, Where<INLotSerialStatusByCostCenter.inventoryID, Equal<Required<INLotSerialStatusByCostCenter.inventoryID>>, And<INLotSerialStatusByCostCenter.subItemID, Equal<Required<INLotSerialStatusByCostCenter.subItemID>>, And<INLotSerialStatusByCostCenter.siteID, Equal<Required<INLotSerialStatusByCostCenter.siteID>>, And<INLotSerialStatusByCostCenter.costCenterID, Equal<Required<INLotSerialStatusByCostCenter.costCenterID>>, And<INLocation.salesValid, Equal<boolTrue>, And<INLocation.inclQtyAvail, Equal<boolTrue>, And<INLotSerialStatusByCostCenter.qtyOnHand, Greater<decimal0>, And<INSiteLotSerial.qtyHardAvail, Greater<decimal0>>>>>>>>>>((PXGraph) this.Base) : (PXSelectBase<INLotSerialStatusByCostCenter>) new PXSelectReadonly2<INLotSerialStatusByCostCenter, InnerJoin<INLocation, On<INLotSerialStatusByCostCenter.FK.Location>, LeftJoin<INSiteStatusByCostCenter, On<INSiteStatusByCostCenter.inventoryID, Equal<INLotSerialStatusByCostCenter.inventoryID>, And<INSiteStatusByCostCenter.subItemID, Equal<INLotSerialStatusByCostCenter.subItemID>, And<INSiteStatusByCostCenter.siteID, Equal<INLotSerialStatusByCostCenter.siteID>, And<INSiteStatusByCostCenter.costCenterID, Equal<INLotSerialStatusByCostCenter.costCenterID>>>>>>>, Where<INLotSerialStatusByCostCenter.inventoryID, Equal<Required<INLotSerialStatusByCostCenter.inventoryID>>, And<INLotSerialStatusByCostCenter.subItemID, Equal<Required<INLotSerialStatusByCostCenter.subItemID>>, And<INLotSerialStatusByCostCenter.siteID, Equal<Required<INLotSerialStatusByCostCenter.siteID>>, And<INLotSerialStatusByCostCenter.costCenterID, Equal<Required<INLotSerialStatusByCostCenter.costCenterID>>, And<INLocation.salesValid, Equal<boolTrue>, And<INLocation.inclQtyAvail, Equal<boolTrue>>>>>>>>((PXGraph) this.Base);
    List<object> parameters = new List<object>(8)
    {
      (object) newline.InventoryID,
      (object) newline.SubItemID,
      (object) newline.SiteID,
      (object) newline.CostCenterID
    };
    if (!string.IsNullOrEmpty(origLotSerialNbr))
    {
      pxSelectBase.WhereAnd<Where<INLotSerialStatusByCostCenter.lotSerialNbr, Equal<Required<INLotSerialStatusByCostCenter.lotSerialNbr>>>>();
      parameters.Add((object) origLotSerialNbr);
    }
    this.AppendFiltersForStatusSelect<INLotSerialStatusByCostCenter>(newline, pxSelectBase, parameters);
    this.Base.LineSplittingExt.AppendSerialStatusCmdOrderBy(pxSelectBase, newline, lotserclass);
    return ((IEnumerable<PXResult<INLotSerialStatusByCostCenter>>) pxSelectBase.Select(parameters.ToArray())).AsEnumerable<PXResult<INLotSerialStatusByCostCenter>>().Cast<PXResult>().ToList<PXResult>();
  }

  public virtual void AppendFiltersForStatusSelect<TStatus>(
    SOShipLine line,
    PXSelectBase<TStatus> select,
    List<object> parameters)
    where TStatus : class, IBqlTable, new()
  {
    if (line.ProjectID.HasValue && line.TaskID.HasValue)
    {
      select.WhereAnd<Where<INLocation.projectID, IsNull, Or<INLocation.projectID, Equal<Required<INLocation.projectID>>>>>();
      parameters.Add((object) line.ProjectID);
    }
    if (!this.IsSyncUnassignedScope || !this.UnassignedSplitsLocationID.HasValue)
      return;
    select.WhereAnd<Where<INLocation.locationID, Equal<Required<INLocation.locationID>>>>();
    parameters.Add((object) this.UnassignedSplitsLocationID);
  }

  protected virtual void ResortStockForShipmentByDefaultItemLocation(
    SOShipLine newline,
    List<PXResult> resultset)
  {
    PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, newline.SiteID);
    if ((inSite != null ? (!inSite.UseItemDefaultLocationForPicking.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      return;
    int? dfltShipLocationID = (int?) INItemSite.PK.Find((PXGraph) this.Base, newline.InventoryID, newline.SiteID)?.DfltShipLocationID;
    if (!dfltShipLocationID.HasValue)
      return;
    List<PXResult> list = resultset.OrderByDescending<PXResult, bool>((Func<PXResult, bool>) (r =>
    {
      int? locationId = PXResult.Unwrap<INLocation>((object) r).LocationID;
      int? nullable = dfltShipLocationID;
      return locationId.GetValueOrDefault() == nullable.GetValueOrDefault() & locationId.HasValue == nullable.HasValue;
    })).ToList<PXResult>();
    resultset.Clear();
    resultset.AddRange((IEnumerable<PXResult>) list);
  }

  protected virtual void ResortStockForShipmentByProjectAndTask(
    SOShipLine newline,
    List<PXResult> resultset)
  {
    if (!newline.ProjectID.HasValue || !newline.TaskID.HasValue)
      return;
    int count = resultset.Count;
    List<PXResult> collection1 = new List<PXResult>(count);
    List<PXResult> collection2 = new List<PXResult>(count);
    List<PXResult> collection3 = new List<PXResult>(count);
    List<PXResult> collection4 = new List<PXResult>(count);
    foreach (PXResult pxResult in resultset)
    {
      INLocation inLocation = PXResult.Unwrap<INLocation>((object) pxResult);
      int? nullable1 = inLocation.ProjectID;
      int? nullable2;
      if (nullable1.HasValue)
      {
        nullable1 = inLocation.ProjectID;
        nullable2 = newline.ProjectID;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          nullable2 = inLocation.TaskID;
          nullable1 = newline.TaskID;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          {
            collection1.Add(pxResult);
            continue;
          }
        }
      }
      nullable1 = inLocation.ProjectID;
      if (nullable1.HasValue)
      {
        nullable1 = inLocation.ProjectID;
        nullable2 = newline.ProjectID;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          nullable2 = inLocation.TaskID;
          if (!nullable2.HasValue)
          {
            collection2.Add(pxResult);
            continue;
          }
        }
      }
      nullable2 = inLocation.ProjectID;
      if (!nullable2.HasValue)
      {
        nullable2 = inLocation.TaskID;
        if (!nullable2.HasValue)
        {
          collection3.Add(pxResult);
          continue;
        }
      }
      nullable2 = inLocation.ProjectID;
      if (nullable2.HasValue)
      {
        nullable2 = inLocation.ProjectID;
        nullable1 = newline.ProjectID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          nullable1 = inLocation.TaskID;
          if (nullable1.HasValue)
            collection4.Add(pxResult);
        }
      }
    }
    resultset.Clear();
    resultset.AddRange((IEnumerable<PXResult>) collection1);
    resultset.AddRange((IEnumerable<PXResult>) collection2);
    resultset.AddRange((IEnumerable<PXResult>) collection3);
    resultset.AddRange((IEnumerable<PXResult>) collection4);
  }

  protected virtual void AfterSaveCreateShipment(CreateShipmentArgs args)
  {
    if (args.ShipmentList.Find((object) ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current) != null)
      return;
    args.ShipmentList.Add(((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current);
  }

  protected virtual void SetShipAddressAndContactFromArgs(
    PX.Objects.SO.SOShipment shipment,
    CreateShipmentArgs args)
  {
  }

  protected virtual void CreateShipmentDetails(CreateShipmentArgs args)
  {
  }

  protected virtual bool ShouldSaveAfterCreateShipment(CreateShipmentArgs args) => false;

  protected int? UnassignedSplitsLocationID { get; private set; }

  public Decimal? QuantityToCreate { get; private set; }

  public class SyncUnassignedScope : IDisposable
  {
    private readonly CreateShipmentExtension parent;

    public SyncUnassignedScope(
      CreateShipmentExtension extension,
      int? locationID,
      Decimal? quantity = null)
    {
      this.parent = extension;
      this.parent.IsSyncUnassignedScope = true;
      this.parent.UnassignedSplitsLocationID = locationID;
      this.parent.QuantityToCreate = quantity;
    }

    void IDisposable.Dispose()
    {
      this.parent.UnassignedSplitsLocationID = new int?();
      this.parent.IsSyncUnassignedScope = false;
      this.parent.QuantityToCreate = new Decimal?();
    }
  }
}
