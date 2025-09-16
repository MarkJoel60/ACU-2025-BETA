// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.SOShipmentLineSplittingExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public class SOShipmentLineSplittingExtension : 
  LineSplittingExtension<SOShipmentEntry, PX.Objects.SO.SOShipment, SOShipLine, PX.Objects.SO.SOShipLineSplit>
{
  public bool IsLocationEnabled
  {
    get
    {
      PX.Objects.SO.SOOrderType soOrderType = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSetup<PX.Objects.SO.SOOrderType>.Select((PXGraph) this.Base, Array.Empty<object>()));
      if (soOrderType == null)
        return true;
      bool? nullable = soOrderType.RequireShipping;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      {
        nullable = soOrderType.RequireLocation;
        if (nullable.GetValueOrDefault())
          return soOrderType.INDocType != "UND";
      }
      return false;
    }
  }

  public bool IsLotSerialRequired
  {
    get
    {
      PX.Objects.SO.SOOrderType soOrderType = PXResultset<PX.Objects.SO.SOOrderType>.op_Implicit(PXSetup<PX.Objects.SO.SOOrderType>.Select((PXGraph) this.Base, Array.Empty<object>()));
      return soOrderType == null || soOrderType.RequireLotSerial.GetValueOrDefault();
    }
  }

  protected virtual SOShipmentItemAvailabilityExtension Availability
  {
    get => ((PXGraph) this.Base).FindImplementation<SOShipmentItemAvailabilityExtension>();
  }

  protected override Type SplitsToDocumentCondition
  {
    get
    {
      return typeof (KeysRelation<Field<PX.Objects.SO.SOShipLineSplit.shipmentNbr>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentNbr>.AsSimpleKey.WithTablesOf<PX.Objects.SO.SOShipment, PX.Objects.SO.SOShipLineSplit>, PX.Objects.SO.SOShipment, PX.Objects.SO.SOShipLineSplit>.SameAsCurrent);
    }
  }

  protected override Type LineQtyField => typeof (SOShipLine.shippedQty);

  public override PX.Objects.SO.SOShipLineSplit LineToSplit(SOShipLine line)
  {
    using (this.InvtMultModeScope(line))
    {
      PX.Objects.SO.SOShipLineSplit split = PX.Objects.SO.SOShipLineSplit.FromSOShipLine(line);
      Decimal? baseQty = line.BaseQty;
      Decimal? unassignedQty = line.UnassignedQty;
      split.BaseQty = baseQty.HasValue & unassignedQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() - unassignedQty.GetValueOrDefault()) : new Decimal?();
      split.LotSerialNbr = string.Empty;
      return split;
    }
  }

  public override void Initialize()
  {
    base.Initialize();
    ((RowUpdatedEvents) ((PXGraph) this.Base).RowUpdated).AddAbstractHandler<PX.Objects.SO.SOShipment>(new Action<AbstractEvents.IRowUpdated<PX.Objects.SO.SOShipment>>(this.EventHandler));
  }

  protected virtual void EventHandler(AbstractEvents.IRowUpdated<PX.Objects.SO.SOShipment> e)
  {
    bool? confirmed1 = e.Row.Confirmed;
    bool? confirmed2 = e.OldRow.Confirmed;
    if (confirmed1.GetValueOrDefault() == confirmed2.GetValueOrDefault() & confirmed1.HasValue == confirmed2.HasValue || !e.Row.Confirmed.GetValueOrDefault())
      return;
    foreach (SOShipLine selectSibling in PXParentAttribute.SelectSiblings((PXCache) this.LineCache, (object) null, typeof (PX.Objects.SO.SOShipment)))
    {
      Decimal? nullable = selectSibling.BaseQty;
      if (Math.Abs(nullable.Value) >= 0.0000005M)
      {
        nullable = selectSibling.UnassignedQty;
        Decimal num = 0.0000005M;
        if (!(nullable.GetValueOrDefault() >= num & nullable.HasValue))
        {
          nullable = selectSibling.UnassignedQty;
          num = -0.0000005M;
          if (!(nullable.GetValueOrDefault() <= num & nullable.HasValue))
            continue;
        }
        ((PXCache) this.LineCache).RaiseExceptionHandling<SOShipLine.unassignedQty>((object) selectSibling, (object) selectSibling.UnassignedQty, (Exception) new PXSetPropertyException("One or more lines have unassigned Location and/or Lot/Serial Number"));
        GraphHelper.MarkUpdated((PXCache) this.LineCache, (object) selectSibling, true);
      }
    }
  }

  protected override void EventHandler(AbstractEvents.IRowSelected<SOShipLine> e)
  {
    if (e.Row == null)
      return;
    bool flag = false;
    if (e.Row.InventoryID.HasValue && Math.Abs(e.Row.BaseQty.GetValueOrDefault()) >= 0.0000005M && Math.Abs(e.Row.UnassignedQty.GetValueOrDefault()) >= 0.0000005M)
    {
      flag = true;
      ((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache.RaiseExceptionHandling<SOShipLine.unassignedQty>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("There are '{0}' items in the document that do not have a location code or a lot/serial number assigned. Use the Line Details pop-up window to correct the items.", (PXErrorLevel) 2, new object[1]
      {
        ((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache.GetValueExt<SOShipLine.inventoryID>((object) e.Row)
      }));
    }
    if (flag)
      return;
    ((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache.RaiseExceptionHandling<SOShipLine.unassignedQty>((object) e.Row, (object) null, (Exception) null);
  }

  protected override void EventHandler(AbstractEvents.IRowUpdated<SOShipLine> e)
  {
    UpdateIfFieldsChangedScope fieldsChangedScope;
    if (!e.ExternalCall || !(e.Row.Operation == "I"))
      fieldsChangedScope = (UpdateIfFieldsChangedScope) null;
    else
      fieldsChangedScope = UpdateIfFieldsChangedScope.Create(((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache, (object) e.OldRow, (object) e.Row, typeof (SOShipLine.locationID), typeof (SOShipLine.lotSerialNbr), typeof (SOShipLine.expireDate));
    using (fieldsChangedScope)
    {
      using (this.ResolveNotDecimalUnitErrorRedirectorScope<PX.Objects.SO.SOShipLineSplit.qty>((object) e.Row))
        base.EventHandler(e);
    }
  }

  protected override void EventHandler(AbstractEvents.IRowPersisting<SOShipLine> e)
  {
    if (EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
    {
      Decimal? unassignedQty;
      Decimal num;
      if ((PXParentAttribute.SelectParent<PX.Objects.SO.SOShipment>(((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache, (object) e.Row) ?? ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current).Confirmed.GetValueOrDefault() && Math.Abs(e.Row.BaseQty.Value) >= 0.0000005M)
      {
        unassignedQty = e.Row.UnassignedQty;
        num = 0.0000005M;
        if (unassignedQty.GetValueOrDefault() >= num & unassignedQty.HasValue)
          goto label_4;
      }
      unassignedQty = e.Row.UnassignedQty;
      num = -0.0000005M;
      if (!(unassignedQty.GetValueOrDefault() <= num & unassignedQty.HasValue))
        goto label_6;
label_4:
      if (((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache.RaiseExceptionHandling<SOShipLine.unassignedQty>((object) e.Row, (object) e.Row.UnassignedQty, (Exception) new PXSetPropertyException("One or more lines have unassigned Location and/or Lot/Serial Number")))
        throw new PXRowPersistingException(typeof (SOShipLine.unassignedQty).Name, (object) e.Row.UnassignedQty, "One or more lines have unassigned Location and/or Lot/Serial Number");
label_6:
      try
      {
        this.Availability.OrderCheck(e.Row);
      }
      catch (PXSetPropertyException ex)
      {
        ((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache.RaiseExceptionHandling<SOShipLine.shippedQty>((object) e.Row, (object) e.Row.ShippedQty, (Exception) ex);
      }
    }
    base.EventHandler(e);
  }

  public int? LastComponentID { get; set; }

  protected override void EventHandlerInternal(AbstractEvents.IRowUpdated<SOShipLine> e)
  {
    int? lastComponentId = this.LastComponentID;
    int? inventoryId = e.Row.InventoryID;
    if (lastComponentId.GetValueOrDefault() == inventoryId.GetValueOrDefault() & lastComponentId.HasValue == inventoryId.HasValue)
      return;
    base.EventHandlerInternal(e);
  }

  protected override void SubscribeForSplitEvents()
  {
    base.SubscribeForSplitEvents();
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<PX.Objects.SO.SOShipLineSplit, PX.Objects.SO.SOShipLineSplit.invtMult, short>(new Action<AbstractEvents.IFieldDefaulting<PX.Objects.SO.SOShipLineSplit, PX.Objects.SO.SOShipLineSplit.invtMult, short?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<PX.Objects.SO.SOShipLineSplit, PX.Objects.SO.SOShipLineSplit.subItemID, int>(new Action<AbstractEvents.IFieldDefaulting<PX.Objects.SO.SOShipLineSplit, PX.Objects.SO.SOShipLineSplit.subItemID, int?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<PX.Objects.SO.SOShipLineSplit, PX.Objects.SO.SOShipLineSplit.locationID, int>(new Action<AbstractEvents.IFieldDefaulting<PX.Objects.SO.SOShipLineSplit, PX.Objects.SO.SOShipLineSplit.locationID, int?>>(this.EventHandler));
    ((RowUpdatingEvents) ((PXGraph) this.Base).RowUpdating).AddAbstractHandler<PX.Objects.SO.SOShipLineSplit>(new Action<AbstractEvents.IRowUpdating<PX.Objects.SO.SOShipLineSplit>>(this.EventHandler));
  }

  protected virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<PX.Objects.SO.SOShipLineSplit, PX.Objects.SO.SOShipLineSplit.invtMult, short?> e)
  {
    if (this.LineCurrent == null)
      return;
    if (e.Row != null)
    {
      int? lineNbr1 = this.LineCurrent.LineNbr;
      int? lineNbr2 = e.Row.LineNbr;
      if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue))
        return;
    }
    using (this.InvtMultModeScope(this.LineCurrent))
    {
      e.NewValue = this.LineCurrent.InvtMult;
      ((ICancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<PX.Objects.SO.SOShipLineSplit, PX.Objects.SO.SOShipLineSplit.subItemID, int?> e)
  {
    if (this.LineCurrent == null)
      return;
    if (e.Row != null)
    {
      int? lineNbr1 = this.LineCurrent.LineNbr;
      int? lineNbr2 = e.Row.LineNbr;
      if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue) || !e.Row.IsStockItem.GetValueOrDefault())
        return;
    }
    e.NewValue = this.LineCurrent.SubItemID;
    ((ICancelEventArgs) e).Cancel = true;
  }

  protected virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<PX.Objects.SO.SOShipLineSplit, PX.Objects.SO.SOShipLineSplit.locationID, int?> e)
  {
    if (this.LineCurrent == null || this.LineCurrent.IsUnassigned.GetValueOrDefault())
      return;
    if (e.Row != null)
    {
      int? lineNbr1 = this.LineCurrent.LineNbr;
      int? lineNbr2 = e.Row.LineNbr;
      if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue) || !e.Row.IsStockItem.GetValueOrDefault())
        return;
    }
    e.NewValue = this.LineCurrent.LocationID;
    ((ICancelEventArgs) e).Cancel = this.SuppressedMode || e.NewValue.HasValue;
  }

  protected override void EventHandler(AbstractEvents.IRowInserting<PX.Objects.SO.SOShipLineSplit> e)
  {
    PX.Objects.IN.InventoryItem inventoryItem1;
    INLotSerClass inLotSerClass;
    this.ReadInventoryItem(e.Row.InventoryID).Deconstruct(ref inventoryItem1, ref inLotSerClass);
    PX.Objects.IN.InventoryItem inventoryItem2 = inventoryItem1;
    if (inventoryItem2.KitItem.GetValueOrDefault())
    {
      bool? stkItem = inventoryItem2.StkItem;
      bool flag = false;
      if (stkItem.GetValueOrDefault() == flag & stkItem.HasValue)
        e.Row.InventoryID = new int?();
    }
    base.EventHandler(e);
  }

  protected override void EventHandler(AbstractEvents.IRowInserted<PX.Objects.SO.SOShipLineSplit> e)
  {
    base.EventHandler(e);
    if (this.SuppressedMode || this.UnattendedMode)
      return;
    this.UpdateKit(e.Row);
  }

  protected virtual void EventHandler(AbstractEvents.IRowUpdating<PX.Objects.SO.SOShipLineSplit> e)
  {
    if (this.SuppressedMode || this.UnattendedMode)
      return;
    this.UpdateKit(e.Row);
  }

  protected override void EventHandler(AbstractEvents.IRowUpdated<PX.Objects.SO.SOShipLineSplit> e)
  {
    base.EventHandler(e);
    if (e.Row.LotSerialNbr != e.OldRow.LotSerialNbr && e.Row.LotSerialNbr != null && e.Row.Operation == "I")
      this.LotSerialNbrUpdated(e.Row);
    int? locationId1 = e.Row.LocationID;
    int? locationId2 = e.OldRow.LocationID;
    if (!(locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue) && e.Row.LotSerialNbr != null && e.ExternalCall)
      this.LocationUpdated(e.Row);
    if (this.SuppressedMode || this.UnattendedMode)
      return;
    this.UpdateKit(e.Row);
  }

  protected override void EventHandler(AbstractEvents.IRowDeleted<PX.Objects.SO.SOShipLineSplit> e)
  {
    base.EventHandler(e);
    if (this.SuppressedMode || this.UnattendedMode)
      return;
    this.UpdateKit(e.Row);
  }

  protected virtual bool LotSerialNbrUpdated(PX.Objects.SO.SOShipLineSplit split)
  {
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(split.InventoryID);
    INLotSerClass lotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult);
    string tranType = split.TranType;
    short? invtMult = split.InvtMult;
    int? invMult = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    if (INLotSerialNbrAttribute.IsTrackSerial(lotSerClass, tranType, invMult) && split.LotSerialNbr != null && PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerAssign != "U")
    {
      SiteLotSerial siteLotSerial1 = new SiteLotSerial();
      siteLotSerial1.InventoryID = split.InventoryID;
      siteLotSerial1.SiteID = split.SiteID;
      siteLotSerial1.LotSerialNbr = split.LotSerialNbr;
      SiteLotSerial copy = PXCache<SiteLotSerial>.CreateCopy(PXCache<SiteLotSerial>.Insert((PXGraph) this.Base, siteLotSerial1));
      INSiteLotSerial inSiteLotSerial = INSiteLotSerial.PK.Find((PXGraph) this.Base, split.InventoryID, split.SiteID, split.LotSerialNbr);
      Decimal? nullable;
      if (inSiteLotSerial != null)
      {
        SiteLotSerial siteLotSerial2 = copy;
        nullable = siteLotSerial2.QtyAvail;
        Decimal? qtyAvail = inSiteLotSerial.QtyAvail;
        siteLotSerial2.QtyAvail = nullable.HasValue & qtyAvail.HasValue ? new Decimal?(nullable.GetValueOrDefault() + qtyAvail.GetValueOrDefault()) : new Decimal?();
        SiteLotSerial siteLotSerial3 = copy;
        Decimal? qtyHardAvail = siteLotSerial3.QtyHardAvail;
        nullable = inSiteLotSerial.QtyHardAvail;
        siteLotSerial3.QtyHardAvail = qtyHardAvail.HasValue & nullable.HasValue ? new Decimal?(qtyHardAvail.GetValueOrDefault() + nullable.GetValueOrDefault()) : new Decimal?();
      }
      nullable = copy.QtyHardAvail;
      Decimal num1 = nullable.GetValueOrDefault();
      foreach (PX.Objects.SO.Unassigned.SOShipLineSplit unassignedDetail in this.Availability.SelectUnassignedDetails(split))
      {
        if (EnumerableExtensions.IsIn<int?>(split.LocationID, new int?(), unassignedDetail.LocationID) && (string.IsNullOrEmpty(unassignedDetail.LotSerialNbr) || split.LotSerialNbr == null || string.Equals(split.LotSerialNbr, unassignedDetail.LotSerialNbr, StringComparison.InvariantCultureIgnoreCase)))
        {
          Decimal num2 = num1;
          nullable = split.BaseQty;
          Decimal valueOrDefault = nullable.GetValueOrDefault();
          num1 = num2 + valueOrDefault;
        }
      }
      if (num1 < 0M)
      {
        split.LotSerialNbr = (string) null;
        ((PXCache) this.SplitCache).RaiseExceptionHandling<PX.Objects.SO.SOShipLineSplit.lotSerialNbr>((object) split, (object) null, (Exception) new PXSetPropertyException((IBqlTable) split, "Inventory quantity will go negative."));
        return false;
      }
    }
    return true;
  }

  protected virtual void LocationUpdated(PX.Objects.SO.SOShipLineSplit split)
  {
    INLotSerClass lotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(this.ReadInventoryItem(split.InventoryID));
    string tranType = split.TranType;
    short? invtMult = split.InvtMult;
    int? invMult = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    if (!INLotSerialNbrAttribute.IsTrack(lotSerClass, tranType, invMult) || split.LotSerialNbr == null)
      return;
    SOShipLine soShipLine = PXParentAttribute.SelectParent<SOShipLine>(((PXSelectBase) this.Base.splits).Cache, (object) split);
    if (PXResultset<INLotSerialStatusByCostCenter>.op_Implicit(PXSelectBase<INLotSerialStatusByCostCenter, PXViewOf<INLotSerialStatusByCostCenter>.BasedOn<SelectFromBase<INLotSerialStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLotSerialStatusByCostCenter.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<INLotSerialStatusByCostCenter.subItemID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INLotSerialStatusByCostCenter.siteID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INLotSerialStatusByCostCenter.lotSerialNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<INLotSerialStatusByCostCenter.locationID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INLotSerialStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this.Base, new object[6]
    {
      (object) split.InventoryID,
      (object) split.SubItemID,
      (object) split.SiteID,
      (object) split.LotSerialNbr,
      (object) split.LocationID,
      (object) (int?) soShipLine?.CostCenterID
    })) != null)
      return;
    split.LotSerialNbr = (string) null;
  }

  protected override PXSelectBase<INLotSerialStatusByCostCenter> GetSerialStatusCmdBase(
    SOShipLine line,
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> item)
  {
    return !this.IsLocationEnabled && this.IsLotSerialRequired ? (PXSelectBase<INLotSerialStatusByCostCenter>) new FbqlSelect<SelectFromBase<INLotSerialStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INLocation>.On<INLotSerialStatusByCostCenter.FK.Location>>, FbqlJoins.Inner<INSiteLotSerial>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteLotSerial.inventoryID, Equal<INLotSerialStatusByCostCenter.inventoryID>>>>, And<BqlOperand<INSiteLotSerial.siteID, IBqlInt>.IsEqual<INLotSerialStatusByCostCenter.siteID>>>>.And<BqlOperand<INSiteLotSerial.lotSerialNbr, IBqlString>.IsEqual<INLotSerialStatusByCostCenter.lotSerialNbr>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLotSerialStatusByCostCenter.inventoryID, Equal<BqlField<INLotSerialStatusByCostCenter.inventoryID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<INLotSerialStatusByCostCenter.siteID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.siteID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INLotSerialStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.costCenterID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INLotSerialStatusByCostCenter.qtyOnHand, IBqlDecimal>.IsGreater<decimal0>>>>.And<BqlOperand<INSiteLotSerial.qtyHardAvail, IBqlDecimal>.IsGreater<decimal0>>>, INLotSerialStatusByCostCenter>.View((PXGraph) this.Base) : base.GetSerialStatusCmdBase(line, item);
  }

  protected override void AppendSerialStatusCmdWhere(
    PXSelectBase<INLotSerialStatusByCostCenter> cmd,
    SOShipLine line,
    INLotSerClass lotSerClass)
  {
    if (line.SubItemID.HasValue)
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.subItemID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.subItemID, IBqlInt>.FromCurrent>>>();
    if (line.LocationID.HasValue)
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.locationID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.locationID, IBqlInt>.FromCurrent>>>();
    else if (line.TranType == "TRX")
      cmd.WhereAnd<Where<BqlOperand<INLocation.transfersValid, IBqlBool>.IsEqual<True>>>();
    else
      cmd.WhereAnd<Where<BqlOperand<INLocation.salesValid, IBqlBool>.IsEqual<True>>>();
    if (!lotSerClass.IsManualAssignRequired.GetValueOrDefault())
      return;
    if (string.IsNullOrEmpty(line.LotSerialNbr))
      cmd.WhereAnd<Where<BqlOperand<True, IBqlBool>.IsEqual<False>>>();
    else
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.lotSerialNbr, IBqlString>.IsEqual<BqlField<INLotSerialStatusByCostCenter.lotSerialNbr, IBqlString>.FromCurrent>>>();
  }

  protected override bool IsLotSerOptionsEnabled(LSSelect.LotSerOptions opt)
  {
    if (!base.IsLotSerOptionsEnabled(opt))
      return false;
    PX.Objects.SO.SOShipment current = ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Base.Document).Current;
    return current == null || !current.Confirmed.GetValueOrDefault();
  }

  public override SOShipLine Clone(SOShipLine item)
  {
    SOShipLine soShipLine = base.Clone(item);
    soShipLine.OrigOrderType = (string) null;
    soShipLine.OrigOrderNbr = (string) null;
    soShipLine.OrigLineNbr = new int?();
    soShipLine.OrigSplitLineNbr = new int?();
    soShipLine.IsClone = new bool?(true);
    return soShipLine;
  }

  protected override void SetLineQtyFromBase(SOShipLine line)
  {
    if (line.UOM == line.OrderUOM)
    {
      Decimal? baseQty = line.BaseQty;
      Decimal? baseFullOrderQty = line.BaseFullOrderQty;
      if (baseQty.GetValueOrDefault() == baseFullOrderQty.GetValueOrDefault() & baseQty.HasValue == baseFullOrderQty.HasValue)
      {
        line.Qty = line.FullOrderQty;
        return;
      }
    }
    base.SetLineQtyFromBase(line);
  }

  protected virtual void UpdateKit(PX.Objects.SO.SOShipLineSplit split)
  {
    SOShipLine soShipLine1 = this.SelectLine(split);
    if (soShipLine1 == null)
      return;
    Decimal? nullable1 = soShipLine1.BaseQty;
    Decimal num1 = nullable1.Value;
    int? inventoryId = soShipLine1.InventoryID;
    int? nullable2 = split.InventoryID;
    if (!(inventoryId.GetValueOrDefault() == nullable2.GetValueOrDefault() & inventoryId.HasValue == nullable2.HasValue))
    {
      if (split.IsStockItem.GetValueOrDefault())
      {
        foreach (PXResult<INKitSpecStkDet> pxResult in PXSelectBase<INKitSpecStkDet, PXViewOf<INKitSpecStkDet>.BasedOn<SelectFromBase<INKitSpecStkDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<INKitSpecStkDet.FK.ComponentInventoryItem>>>.Where<BqlOperand<INKitSpecStkDet.kitInventoryID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Search<INKitSpecStkDet.compInventoryID>((PXGraph) this.Base, (object) split.InventoryID, new object[1]
        {
          (object) soShipLine1.InventoryID
        }))
        {
          INKitSpecStkDet inKitSpecStkDet = PXResult<INKitSpecStkDet>.op_Implicit(pxResult);
          PXCache<PX.Objects.SO.SOShipLineSplit> splitCache = this.SplitCache;
          PX.Objects.SO.SOShipLineSplit Row = split;
          string uom = inKitSpecStkDet.UOM;
          Decimal num2 = num1;
          nullable1 = inKitSpecStkDet.DfltCompQty;
          Decimal num3 = nullable1.Value;
          Decimal num4 = num2 * num3;
          Decimal num5 = INUnitAttribute.ConvertToBase<PX.Objects.SO.SOShipLineSplit.inventoryID>((PXCache) splitCache, (object) Row, uom, num4, INPrecision.NOROUND);
          SOShipLine soShipLine2 = this.Clone(soShipLine1);
          soShipLine2.IsStockItem = new bool?(true);
          soShipLine2.InventoryID = split.InventoryID;
          ConvertedInventoryItemAttribute.ValidateRow((PXCache) this.LineCache, (object) soShipLine2);
          LSSelect.Counters counters;
          if (!this.LineCounters.TryGetValue(soShipLine2, out counters))
          {
            this.LineCounters[soShipLine2] = counters = new LSSelect.Counters();
            foreach (PX.Objects.SO.SOShipLineSplit selectSplit in this.SelectSplits(soShipLine2, true))
              this.UpdateCounters(counters, selectSplit);
          }
          if (num5 != 0M && counters.BaseQty != num5)
          {
            num1 = PXDBQuantityAttribute.Round(new Decimal?(num1 * counters.BaseQty / num5));
            this.LastComponentID = inKitSpecStkDet.CompInventoryID;
          }
        }
      }
      else
      {
        foreach (PXResult<INKitSpecNonStkDet> pxResult in PXSelectBase<INKitSpecNonStkDet, PXViewOf<INKitSpecNonStkDet>.BasedOn<SelectFromBase<INKitSpecNonStkDet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<INKitSpecNonStkDet.FK.ComponentInventoryItem>>>.Where<BqlOperand<INKitSpecNonStkDet.kitInventoryID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Search<INKitSpecNonStkDet.compInventoryID>((PXGraph) this.Base, (object) split.InventoryID, new object[1]
        {
          (object) soShipLine1.InventoryID
        }))
        {
          INKitSpecNonStkDet kitSpecNonStkDet = PXResult<INKitSpecNonStkDet>.op_Implicit(pxResult);
          PXCache<PX.Objects.SO.SOShipLineSplit> splitCache = this.SplitCache;
          PX.Objects.SO.SOShipLineSplit Row = split;
          string uom = kitSpecNonStkDet.UOM;
          nullable1 = kitSpecNonStkDet.DfltCompQty;
          Decimal num6 = nullable1.Value;
          Decimal num7 = INUnitAttribute.ConvertToBase<PX.Objects.SO.SOShipLineSplit.inventoryID>((PXCache) splitCache, (object) Row, uom, num6, INPrecision.NOROUND);
          if (num7 != 0M)
          {
            nullable1 = split.BaseQty;
            Decimal num8 = num7;
            if (!(nullable1.GetValueOrDefault() == num8 & nullable1.HasValue))
            {
              nullable1 = split.BaseQty;
              num1 = PXDBQuantityAttribute.Round(new Decimal?(nullable1.Value / num7));
              this.LastComponentID = kitSpecNonStkDet.CompInventoryID;
            }
          }
        }
      }
    }
    nullable2 = this.LastComponentID;
    if (!nullable2.HasValue)
      return;
    SOShipLine copy = PXCache<SOShipLine>.CreateCopy(soShipLine1);
    copy.ShippedQty = new Decimal?(INUnitAttribute.ConvertFromBase<SOShipLine.inventoryID>((PXCache) this.LineCache, (object) soShipLine1, soShipLine1.UOM, num1, INPrecision.QUANTITY));
    try
    {
      this.LineCache.Update(copy);
    }
    finally
    {
      this.LastComponentID = new int?();
    }
    ((PXSelectBase) this.Base.splits).View.RequestRefresh();
  }

  /// <summary>
  /// Inserts SOShipLine into cache without adding the splits.
  /// The Splits have to be added manually.
  /// </summary>
  /// <param name="line">Master record.</param>
  public virtual SOShipLine InsertWithoutSplits(SOShipLine line)
  {
    using (this.SuppressedModeScope(true))
    {
      SOShipLine key = this.LineCache.Insert(line);
      this.LineCounters.Remove(key);
      return key;
    }
  }

  protected override void SetUnassignedQty(
    SOShipLine line,
    Decimal detailsBaseQty,
    bool allowNegative)
  {
    base.SetUnassignedQty(line, detailsBaseQty, allowNegative);
    this.UpdateUnassigned(line);
  }

  protected virtual void UpdateUnassigned(SOShipLine line)
  {
    Decimal? unassignedQty = line.UnassignedQty;
    Decimal num = 0M;
    if (!(unassignedQty.GetValueOrDefault() > num & unassignedQty.HasValue) || !(line.Operation == "I"))
      return;
    if (UpdateIfFieldsChangedScope.Any(new Func<Type, bool>(((Enumerable) new Type[3]
    {
      typeof (SOShipLine.locationID),
      typeof (SOShipLine.lotSerialNbr),
      typeof (SOShipLine.expireDate)
    }).Contains<Type>)))
      return;
    if (line.LocationID.HasValue)
      line.LocationID = new int?();
    if (line.LotSerialNbr != null)
      line.LotSerialNbr = (string) null;
    if (!line.ExpireDate.HasValue)
      return;
    line.ExpireDate = new DateTime?();
  }
}
