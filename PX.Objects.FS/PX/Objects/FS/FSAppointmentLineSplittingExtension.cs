// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentLineSplittingExtension
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.FS;

public class FSAppointmentLineSplittingExtension : 
  LineSplittingExtension<
  #nullable disable
  AppointmentEntry, FSAppointment, FSAppointmentDet, FSApptLineSplit>
{
  public int? lastComponentID;
  protected FSINLotSerialNbrAttribute _LotSerialSelector;

  public virtual bool IsLotSerialRequired
  {
    get
    {
      PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(this.LineCurrent.InventoryID);
      return pxResult != null && EnumerableExtensions.IsNotIn<string>(PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerTrack, (string) null, "N");
    }
  }

  protected PXView LotSerOptionsView { get; private set; }

  protected override Type SplitsToDocumentCondition
  {
    get
    {
      return typeof (KeysRelation<CompositeKey<Field<FSApptLineSplit.srvOrdType>.IsRelatedTo<FSAppointment.srvOrdType>, Field<FSApptLineSplit.apptNbr>.IsRelatedTo<FSAppointment.refNbr>>.WithTablesOf<FSAppointment, FSApptLineSplit>, FSAppointment, FSApptLineSplit>.SameAsCurrent);
    }
  }

  protected override Type LineQtyField => typeof (FSAppointmentDet.effTranQty);

  public override FSApptLineSplit LineToSplit(FSAppointmentDet line)
  {
    return FSAppointmentLineSplittingExtension.StaticConvert(line);
  }

  public static FSApptLineSplit StaticConvert(FSAppointmentDet line)
  {
    using (new LineSplittingExtension<AppointmentEntry, FSAppointment, FSAppointmentDet, FSApptLineSplit>.InvtMultScope(line))
    {
      FSApptLineSplit fsApptLineSplit = (FSApptLineSplit) line;
      Decimal? baseQty = line.BaseQty;
      Decimal? unassignedQty = line.UnassignedQty;
      fsApptLineSplit.BaseQty = baseQty.HasValue & unassignedQty.HasValue ? new Decimal?(baseQty.GetValueOrDefault() - unassignedQty.GetValueOrDefault()) : new Decimal?();
      fsApptLineSplit.LotSerialNbr = string.Empty;
      fsApptLineSplit.SplitLineNbr = new int?();
      return fsApptLineSplit;
    }
  }

  public override void Initialize()
  {
    base.Initialize();
    this.showSplits?.SetVisible(PXAccess.FeatureInstalled<FeaturesSet.inventory>());
    string name = this.TypePrefixed("GenerateNumbersMobile");
    FSAppointmentLineSplittingExtension splittingExtension = this;
    // ISSUE: virtual method pointer
    PXButtonDelegate handler = new PXButtonDelegate((object) splittingExtension, __vmethodptr(splittingExtension, GenerateNumbersMobile));
    this.generateNumbersMobile = (PXAction<FSAppointment>) this.AddAction(name, "Generate", false, handler, (PXCacheRights) 2);
    this.LotSerOptionsView = ((PXGraph) this.Base).Views[this.TypePrefixed("LotSerOptions")];
  }

  public override IEnumerable ShowSplits(PXAdapter adapter)
  {
    if (this.LineCurrent == null)
      return adapter.Get();
    if (!this.LineCurrent.InventoryID.HasValue)
      throw new PXException("This action cannot be used for a line with the Instruction or Comment type.");
    if (EnumerableExtensions.IsIn<string>(this.LineCurrent.LineType, "SERVI", "NSTKI"))
    {
      bool? enablePo = this.LineCurrent.EnablePO;
      bool flag = false;
      if (enablePo.GetValueOrDefault() == flag & enablePo.HasValue)
        throw new PXException("Shipment Scheduling and Bin/Lot/Serial assignment are not possible for non-stock items.");
    }
    this.LineCurrent.IsLotSerialRequired = new bool?(this.IsLotSerialRequired);
    return base.ShowSplits(adapter);
  }

  public PXAction<FSAppointment> generateNumbersMobile { get; private set; }

  public virtual IEnumerable GenerateNumbersMobile(PXAdapter adapter)
  {
    FSAppointmentLineSplittingExtension.LotSerOptionsExt extension = PXCacheEx.GetExtension<FSAppointmentLineSplittingExtension.LotSerOptionsExt>((IBqlTable) ((PXCache) GraphHelper.Caches<FSAppointment>((PXGraph) this.Base)).Current);
    if (extension != null)
      extension.LastSelectedSplitSource = new int?();
    if (this.LotSerOptionsView.AskExt() == 1)
      this.GenerateNumbers(adapter);
    return adapter.Get();
  }

  public override IEnumerable GetLotSerialOpts()
  {
    LSSelect.LotSerOptions current = (LSSelect.LotSerOptions) ((PXCache) GraphHelper.Caches<LSSelect.LotSerOptions>((PXGraph) this.Base)).Current;
    FSAppointmentLineSplittingExtension.LotSerOptionsExt extension = PXCacheEx.GetExtension<FSAppointmentLineSplittingExtension.LotSerOptionsExt>((IBqlTable) ((PXCache) GraphHelper.Caches<FSAppointment>((PXGraph) this.Base)).Current);
    if (((PXGraph) this.Base).IsMobile && current != null && this.LineCurrent != null)
    {
      int? selectedSplitSource = (int?) extension?.LastSelectedSplitSource;
      int? lineNbr = this.LineCurrent.LineNbr;
      if (selectedSplitSource.GetValueOrDefault() == lineNbr.GetValueOrDefault() & selectedSplitSource.HasValue == lineNbr.HasValue)
        return (IEnumerable) Enumerable.Repeat<LSSelect.LotSerOptions>(current, 1);
    }
    if (extension != null && this.LineCurrent != null)
      extension.LastSelectedSplitSource = this.LineCurrent.LineNbr;
    return base.GetLotSerialOpts();
  }

  protected override void EventHandlerInternal(AbstractEvents.IRowUpdated<FSAppointmentDet> e)
  {
    int? lastComponentId = this.lastComponentID;
    int? inventoryId1 = e.Row.InventoryID;
    if (lastComponentId.GetValueOrDefault() == inventoryId1.GetValueOrDefault() & lastComponentId.HasValue == inventoryId1.HasValue)
      return;
    INLotSerClass lotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(this.ReadInventoryItem(e.Row.InventoryID));
    if (lotSerClass.LotSerAssign == "U")
      base.EventHandlerInternal(e);
    else if (e.Row.IsCanceledNotPerformed.GetValueOrDefault())
    {
      if (!((PXGraph) this.Base).IsContractBasedAPI)
      {
        int? inventoryId2 = e.OldRow.InventoryID;
        int? inventoryId3 = e.Row.InventoryID;
        if (!(inventoryId2.GetValueOrDefault() == inventoryId3.GetValueOrDefault() & inventoryId2.HasValue == inventoryId3.HasValue))
        {
          e.Row.LotSerialNbr = (string) null;
          e.Row.ExpireDate = new DateTime?();
        }
        else
        {
          short? invtMult1 = e.OldRow.InvtMult;
          int? nullable1 = invtMult1.HasValue ? new int?((int) invtMult1.GetValueOrDefault()) : new int?();
          short? invtMult2 = e.Row.InvtMult;
          int? nullable2 = invtMult2.HasValue ? new int?((int) invtMult2.GetValueOrDefault()) : new int?();
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
          {
            if (string.Equals(e.Row.LotSerialNbr, e.OldRow.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
              e.Row.LotSerialNbr = (string) null;
            DateTime? expireDate = e.Row.ExpireDate;
            DateTime? nullable3 = e.OldRow.ExpireDate;
            if ((expireDate.HasValue == nullable3.HasValue ? (expireDate.HasValue ? (expireDate.GetValueOrDefault() == nullable3.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
            {
              FSAppointmentDet row = e.Row;
              nullable3 = new DateTime?();
              DateTime? nullable4 = nullable3;
              row.ExpireDate = nullable4;
            }
          }
        }
      }
      this.RaiseRowDeleted(e.OldRow);
    }
    else
    {
      if (!string.IsNullOrEmpty(e.Row.LotSerialNbr) && !string.Equals(e.Row.LotSerialNbr, e.OldRow.LotSerialNbr, StringComparison.OrdinalIgnoreCase) && (lotSerClass.LotSerTrack == "S" || lotSerClass.LotSerTrack == "L"))
        this.UpdateLotSerialSplitsBasedOnLineLotSerial(e.Row, lotSerClass.LotSerTrack, lotSerClass.LotSerTrackExpiration);
      this.InsertLotSerialsFromServiceOrder(e.Row, lotSerClass);
      this._TruncateNumbers(e.Row, e.Row.BaseQty.Value);
      this._UpdateParent(e.Row);
    }
  }

  protected override void EventHandler(AbstractEvents.IRowSelected<FSAppointmentDet> e)
  {
    FSAppointmentDet lineCurrent = this.LineCurrent;
    bool flag = lineCurrent != null && lineCurrent.IsLotSerialRequired.GetValueOrDefault();
    ((PXCache) this.SplitCache).AllowInsert = flag;
    ((PXCache) this.SplitCache).AllowDelete = flag;
    ((PXCache) this.SplitCache).AllowUpdate = flag;
    base.EventHandler(e);
  }

  protected override void EventHandler(AbstractEvents.IRowUpdated<FSAppointmentDet> e)
  {
    using (this.ResolveNotDecimalUnitErrorRedirectorScope<FSApptLineSplit.qty>((object) e.Row))
    {
      base.EventHandler(e);
      if (string.Equals(e.Row.LotSerialNbr, e.OldRow.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
        return;
      ((IGenericEventWith<PXRowUpdatedEventArgs>) e).Cache.RaiseFieldUpdated<FSAppointmentDet.lotSerialNbr>((object) e.Row, (object) e.OldRow.LotSerialNbr);
    }
  }

  protected override void EventHandler(AbstractEvents.IRowPersisting<FSAppointmentDet> e)
  {
    if (e.Row.InventoryID.HasValue && e.Row.Status == "CP" && EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
    {
      PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(e.Row.InventoryID);
      if (pxResult == null)
        throw new PXException("The {0} record was not found.", new object[1]
        {
          (object) DACHelper.GetDisplayName(typeof (PX.Objects.IN.InventoryItem))
        });
      if (EnumerableExtensions.IsIn<string>(PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult).LotSerTrack, "S", "L"))
      {
        Decimal baseExistingSplitTotalQty;
        Decimal existingSplitTotalQty;
        this.GetExistingSplits(e.Row, out baseExistingSplitTotalQty, out existingSplitTotalQty);
        PX.Objects.IN.InventoryItem inventoryItem = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult);
        Decimal num = INUnitAttribute.ConvertFromTo<FSAppointmentDet.inventoryID>(((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache, (object) e.Row, inventoryItem.BaseUnit, e.Row.UOM, existingSplitTotalQty, INPrecision.QUANTITY);
        Decimal? effTranQty = e.Row.EffTranQty;
        baseExistingSplitTotalQty = num;
        if (!(effTranQty.GetValueOrDefault() == baseExistingSplitTotalQty & effTranQty.HasValue))
          ((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache.RaiseExceptionHandling<FSAppointmentDet.status>((object) e.Row, (object) e.Row.Status, (Exception) new PXSetPropertyException("You cannot complete this line, because the quantity on the Lot Serials does not match quantity on the line.", (PXErrorLevel) 4));
      }
    }
    base.EventHandler(e);
    this.VerifyLotSerialTotalQty(e.Row, 0M, false);
  }

  public override void EventHandlerQty(
    AbstractEvents.IFieldVerifying<FSAppointmentDet, IBqlField, Decimal?> e)
  {
    this.Base.VerifySrvOrdLineQty(((IGenericEventWith<PXFieldVerifyingEventArgs>) e).Cache, e.Row, (object) e.NewValue, this.LineQtyField, true);
    base.EventHandlerQty(e);
  }

  protected override void SubscribeForSplitEvents()
  {
    base.SubscribeForSplitEvents();
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<FSApptLineSplit, FSApptLineSplit.invtMult, short>(new Action<AbstractEvents.IFieldDefaulting<FSApptLineSplit, FSApptLineSplit.invtMult, short?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<FSApptLineSplit, FSApptLineSplit.subItemID, int>(new Action<AbstractEvents.IFieldDefaulting<FSApptLineSplit, FSApptLineSplit.subItemID, int?>>(this.EventHandler));
    ((FieldDefaultingEvents) ((PXGraph) this.Base).FieldDefaulting).AddAbstractHandler<FSApptLineSplit, FSApptLineSplit.locationID, int>(new Action<AbstractEvents.IFieldDefaulting<FSApptLineSplit, FSApptLineSplit.locationID, int?>>(this.EventHandler));
    ((FieldUpdatedEvents) ((PXGraph) this.Base).FieldUpdated).AddAbstractHandler<FSApptLineSplit, FSApptLineSplit.lotSerialNbr, string>(new Action<AbstractEvents.IFieldUpdated<FSApptLineSplit, FSApptLineSplit.lotSerialNbr, string>>(this.EventHandler));
    ((RowSelectedEvents) ((PXGraph) this.Base).RowSelected).AddAbstractHandler<FSApptLineSplit>(new Action<AbstractEvents.IRowSelected<FSApptLineSplit>>(this.EventHandler));
    ((RowUpdatingEvents) ((PXGraph) this.Base).RowUpdating).AddAbstractHandler<FSApptLineSplit>(new Action<AbstractEvents.IRowUpdating<FSApptLineSplit>>(this.EventHandler));
    ((RowDeletingEvents) ((PXGraph) this.Base).RowDeleting).AddAbstractHandler<FSApptLineSplit>(new Action<AbstractEvents.IRowDeleting<FSApptLineSplit>>(this.EventHandler));
    ((RowSelectedEvents) ((PXGraph) this.Base).RowSelected).AddAbstractHandler<FSAppointment>(new Action<AbstractEvents.IRowSelected<FSAppointment>>(this.EventHandler));
  }

  protected virtual void EventHandler(AbstractEvents.IRowSelected<FSAppointment> e)
  {
    this.showSplits.SetEnabled(((PXSelectBase<FSAppointmentDet>) this.Base.AppointmentDetails).Any<FSAppointmentDet>());
  }

  protected virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<FSApptLineSplit, FSApptLineSplit.invtMult, short?> e)
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
    using (new LineSplittingExtension<AppointmentEntry, FSAppointment, FSAppointmentDet, FSApptLineSplit>.InvtMultScope(this.LineCurrent))
    {
      e.NewValue = this.LineCurrent.InvtMult;
      ((ICancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void EventHandler(
    AbstractEvents.IFieldDefaulting<FSApptLineSplit, FSApptLineSplit.subItemID, int?> e)
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
    AbstractEvents.IFieldDefaulting<FSApptLineSplit, FSApptLineSplit.locationID, int?> e)
  {
    if (this.LineCurrent == null)
      return;
    int? nullable;
    if (e.Row != null)
    {
      int? lineNbr = this.LineCurrent.LineNbr;
      nullable = e.Row.LineNbr;
      if (!(lineNbr.GetValueOrDefault() == nullable.GetValueOrDefault() & lineNbr.HasValue == nullable.HasValue) || !e.Row.IsStockItem.GetValueOrDefault())
        return;
    }
    e.NewValue = this.LineCurrent.LocationID;
    AbstractEvents.IFieldDefaulting<FSApptLineSplit, FSApptLineSplit.locationID, int?> ifieldDefaulting = e;
    int num;
    if (!this.SuppressedMode)
    {
      nullable = e.NewValue;
      num = nullable.HasValue ? 1 : 0;
    }
    else
      num = 1;
    ((ICancelEventArgs) ifieldDefaulting).Cancel = num != 0;
  }

  protected virtual void EventHandler(
    AbstractEvents.IFieldUpdated<FSApptLineSplit, FSApptLineSplit.lotSerialNbr, string> e)
  {
    e.Row.OrigLineNbr = new int?();
    e.Row.OrigSplitLineNbr = new int?();
    e.Row.OrigSplitLineNbr = new int?();
    if (this.LineCurrent == null || !this.LineCurrent.SODetID.HasValue)
      return;
    int? soDetId = this.LineCurrent.SODetID;
    int num = 0;
    if (soDetId.GetValueOrDefault() < num & soDetId.HasValue)
      return;
    FSSODet fssoDet = PXResultset<FSSODet>.op_Implicit(PXSelectBase<FSSODet, PXViewOf<FSSODet>.BasedOn<SelectFromBase<FSSODet, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FSSODet.sODetID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) this.LineCurrent.SODetID
    }));
    if (fssoDet == null)
      return;
    e.Row.OrigLineNbr = fssoDet.LineNbr;
    if (string.IsNullOrEmpty(e.Row.LotSerialNbr))
      return;
    FSSODetSplit soDetSplit = PXResultset<FSSODetSplit>.op_Implicit(PXSelectBase<FSSODetSplit, PXViewOf<FSSODetSplit>.BasedOn<SelectFromBase<FSSODetSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODetSplit.srvOrdType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<FSSODetSplit.refNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<FSSODetSplit.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<FSSODetSplit.lotSerialNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this.Base, new object[4]
    {
      (object) fssoDet.SrvOrdType,
      (object) fssoDet.RefNbr,
      (object) fssoDet.LineNbr,
      (object) e.Row.LotSerialNbr
    }));
    if (soDetSplit == null)
      return;
    this.FillLotSerialAndPOFields(e.Row, soDetSplit);
  }

  protected virtual void EventHandler(AbstractEvents.IRowSelected<FSApptLineSplit> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<FSApptLineSplit.subItemID>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<FSApptLineSplit.siteID>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, false);
    PXUIFieldAttribute.SetEnabled<FSApptLineSplit.locationID>(((IGenericEventWith<PXRowSelectedEventArgs>) e).Cache, (object) e.Row, false);
  }

  protected virtual void EventHandler(AbstractEvents.IRowUpdating<FSApptLineSplit> e)
  {
    if (e.ExternalCall && !this.IsLotSerialRequired)
      throw new PXException("A lot or serial number cannot be selected for stock items with the Not Tracked method and non-stock items.");
  }

  protected override void EventHandler(AbstractEvents.IRowUpdated<FSApptLineSplit> e)
  {
    if (!string.Equals(e.Row.LotSerialNbr, e.OldRow.LotSerialNbr, StringComparison.OrdinalIgnoreCase) && e.Row.LotSerialNbr != null && e.Row.Operation == "I")
      this.LotSerialNbrUpdated(e.Row);
    int? locationId1 = e.Row.LocationID;
    int? locationId2 = e.OldRow.LocationID;
    if (!(locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue) && e.Row.LotSerialNbr != null && e.ExternalCall)
      this.LocationUpdated(e.Row);
    base.EventHandler(e);
    this.MarkParentAsUpdated();
  }

  protected override void EventHandler(AbstractEvents.IRowInserting<FSApptLineSplit> e)
  {
    if (e.ExternalCall && !this.IsLotSerialRequired)
      throw new PXException("A lot or serial number cannot be selected for stock items with the Not Tracked method and non-stock items.");
    PX.Objects.IN.InventoryItem inventoryItem1;
    INLotSerClass inLotSerClass1;
    this.ReadInventoryItem(e.Row.InventoryID).Deconstruct(ref inventoryItem1, ref inLotSerClass1);
    PX.Objects.IN.InventoryItem inventoryItem2 = inventoryItem1;
    INLotSerClass inLotSerClass2 = inLotSerClass1;
    if (inventoryItem2.KitItem.GetValueOrDefault())
    {
      bool? stkItem = inventoryItem2.StkItem;
      bool flag = false;
      if (stkItem.GetValueOrDefault() == flag & stkItem.HasValue)
        e.Row.InventoryID = new int?();
    }
    using (this.SuppressedModeScope(EnumerableExtensions.IsIn<string>(inLotSerClass2.LotSerTrack, "S", "L")))
      base.EventHandler(e);
  }

  protected override void EventHandler(AbstractEvents.IRowInserted<FSApptLineSplit> e)
  {
    base.EventHandler(e);
    this.MarkParentAsUpdated();
  }

  protected virtual void EventHandler(AbstractEvents.IRowDeleting<FSApptLineSplit> e)
  {
    if (e.ExternalCall && !this.IsLotSerialRequired)
      throw new PXException("A lot or serial number cannot be selected for stock items with the Not Tracked method and non-stock items.");
  }

  protected override void EventHandler(AbstractEvents.IRowDeleted<FSApptLineSplit> e)
  {
    base.EventHandler(e);
    this.MarkParentAsUpdated();
  }

  public override void EventHandler(AbstractEvents.IRowPersisting<FSApptLineSplit> e)
  {
    base.EventHandler(e);
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    int num1;
    if (e.Row.IsStockItem.GetValueOrDefault())
    {
      Decimal? baseQty = e.Row.BaseQty;
      Decimal num2 = 0M;
      num1 = !(baseQty.GetValueOrDefault() == num2 & baseQty.HasValue) ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag1 = num1 != 0;
    PXDefaultAttribute.SetPersistingCheck<FSApptLineSplit.subItemID>(((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache, (object) e.Row, flag1 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<FSApptLineSplit.locationID>(((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache, (object) e.Row, flag1 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    bool flag2 = e.Row.POReceiptNbr == null;
    PXDefaultAttribute.SetPersistingCheck<FSApptLineSplit.lotSerialNbr>(((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache, (object) e.Row, flag2 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    Decimal? qty = e.Row.Qty;
    Decimal num3 = 0M;
    if (!(qty.GetValueOrDefault() == num3 & qty.HasValue))
      return;
    PXSetPropertyException propertyException = new PXSetPropertyException("Quantity must be different from zero on all Lot/Serial lines.", (PXErrorLevel) 4);
    ((IGenericEventWith<PXRowPersistingEventArgs>) e).Cache.RaiseExceptionHandling<FSApptLineSplit.lotSerialNbr>((object) e.Row, (object) null, (Exception) propertyException);
  }

  public override void EventHandlerQty(
    AbstractEvents.IFieldVerifying<FSApptLineSplit, IBqlField, Decimal?> e)
  {
    base.EventHandlerQty(e);
    if (EnumerableExtensions.IsIn<Decimal?>(e.NewValue, new Decimal?(), new Decimal?(0M)))
      return;
    FSApptLineSplit row = e.Row;
    Decimal? nullable1 = e.NewValue;
    Decimal num1 = nullable1.Value;
    nullable1 = e.Row.Qty;
    Decimal num2 = nullable1.Value;
    Decimal newIncrease = num1 - num2;
    this.VerifyLotSerialTotalQty(row, newIncrease);
    int? nullable2 = e.Row.InventoryID;
    if (!nullable2.HasValue)
      return;
    PX.Objects.IN.InventoryItem inventoryItem;
    INLotSerClass inLotSerClass1;
    this.ReadInventoryItem(e.Row.InventoryID).Deconstruct(ref inventoryItem, ref inLotSerClass1);
    INLotSerClass inLotSerClass2 = inLotSerClass1;
    if (!(inLotSerClass2.LotSerAssign != "U") || string.IsNullOrEmpty(e.Row.LotSerialNbr) || !(inLotSerClass2.LotSerTrack != "N"))
      return;
    FSAppointmentDet lineCurrent = this.LineCurrent;
    string lotSerialNbr = e.Row.LotSerialNbr;
    nullable2 = new int?();
    int? splitLineNbr = nullable2;
    Decimal num3;
    ref Decimal local1 = ref num3;
    Decimal num4;
    ref Decimal local2 = ref num4;
    bool flag;
    ref bool local3 = ref flag;
    this.GetLotSerialAvailability(lineCurrent, lotSerialNbr, splitLineNbr, true, out local1, out local2, out local3);
    Decimal num5 = num3 - num4;
    nullable1 = e.NewValue;
    Decimal num6 = nullable1.Value;
    if (!(num5 < num6))
      return;
    if (inLotSerClass2.LotSerTrack == "S")
    {
      if (flag)
        throw new PXSetPropertyException("The lot or serial number is already used in another appointment.");
      throw new PXSetPropertyException("The lot or serial number is not available at the specified warehouse.");
    }
    if (flag)
    {
      if (num4 == 0M)
      {
        object[] objArray = new object[2];
        nullable1 = e.NewValue;
        objArray[0] = (object) nullable1.Value.ToString("0");
        objArray[1] = (object) num3.ToString("0");
        throw new PXSetPropertyException("The quantity entered ({0}) for the lot number is greater than the quantity allocated in the service order ({1}).", objArray);
      }
      object[] objArray1 = new object[3];
      nullable1 = e.NewValue;
      objArray1[0] = (object) nullable1.Value.ToString("0");
      objArray1[1] = (object) num4.ToString("0");
      objArray1[2] = (object) num3.ToString("0");
      throw new PXSetPropertyException("The quantity entered ({0}) for the lot number along with the quantity used in other appointments ({1}) is greater than the quantity allocated in the service order ({2}).", objArray1);
    }
    object[] objArray2 = new object[2];
    nullable1 = e.NewValue;
    objArray2[0] = (object) nullable1.Value.ToString("0");
    objArray2[1] = (object) num3.ToString("0");
    throw new PXSetPropertyException("The quantity entered ({0}) for the lot number is greater than the quantity available in the Inventory ({1}).", objArray2);
  }

  protected virtual bool LotSerialNbrUpdated(FSApptLineSplit split)
  {
    PXResult<PX.Objects.IN.InventoryItem, INLotSerClass> pxResult = this.ReadInventoryItem(split.InventoryID);
    INSiteLotSerial inSiteLotSerial = PXResultset<INSiteLotSerial>.op_Implicit(PXSelectBase<INSiteLotSerial, PXViewOf<INSiteLotSerial>.BasedOn<SelectFromBase<INSiteLotSerial, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSiteLotSerial.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<INSiteLotSerial.siteID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INSiteLotSerial.lotSerialNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) split.InventoryID,
      (object) split.SiteID,
      (object) split.LotSerialNbr
    }));
    INLotSerClass lotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(pxResult);
    string tranType = split.TranType;
    short? invtMult = split.InvtMult;
    int? invMult = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    if (INLotSerialNbrAttribute.IsTrackSerial(lotSerClass, tranType, invMult) && split.LotSerialNbr != null && inSiteLotSerial != null && inSiteLotSerial.LotSerAssign != "U")
    {
      Decimal? baseQty = split.BaseQty;
      Decimal num1 = 0M;
      if (baseQty.GetValueOrDefault() <= num1 & baseQty.HasValue)
      {
        split.BaseQty = new Decimal?((Decimal) 1);
        PXCache<FSApptLineSplit> splitCache1 = this.SplitCache;
        FSApptLineSplit fsApptLineSplit = split;
        PXCache<FSApptLineSplit> splitCache2 = this.SplitCache;
        int? inventoryId = split.InventoryID;
        string uom = split.UOM;
        baseQty = split.BaseQty;
        Decimal num2 = baseQty.Value;
        // ISSUE: variable of a boxed type
        __Boxed<Decimal> local = (ValueType) INUnitAttribute.ConvertFromBase((PXCache) splitCache2, inventoryId, uom, num2, INPrecision.QUANTITY);
        ((PXCache) splitCache1).SetValueExt<FSApptLineSplit.qty>((object) fsApptLineSplit, (object) local);
      }
    }
    return true;
  }

  protected virtual void LocationUpdated(FSApptLineSplit split)
  {
    INLotSerClass lotSerClass = PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(this.ReadInventoryItem(split.InventoryID));
    string tranType = split.TranType;
    short? invtMult = split.InvtMult;
    int? invMult = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
    if (!INLotSerialNbrAttribute.IsTrack(lotSerClass, tranType, invMult) || split.LotSerialNbr == null)
      return;
    if (PXSelectBase<INLotSerialStatusByCostCenter, PXViewOf<INLotSerialStatusByCostCenter>.BasedOn<SelectFromBase<INLotSerialStatusByCostCenter, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLotSerialStatusByCostCenter.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<INLotSerialStatusByCostCenter.subItemID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INLotSerialStatusByCostCenter.siteID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INLotSerialStatusByCostCenter.lotSerialNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<INLotSerialStatusByCostCenter.locationID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INLotSerialStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<PX.Objects.IN.CostCenter.freeStock>>>>.Config>.Select((PXGraph) this.Base, new object[5]
    {
      (object) split.InventoryID,
      (object) split.SubItemID,
      (object) split.SiteID,
      (object) split.LotSerialNbr,
      (object) split.LocationID
    }) != null)
      return;
    split.LotSerialNbr = (string) null;
  }

  protected override void EventHandler(
    AbstractEvents.IRowSelected<LSSelect.LotSerOptions> e)
  {
    base.EventHandler(e);
    ((PXAction) this.generateNumbersMobile).SetEnabled(this.generateNumbers.GetEnabled());
  }

  protected override void AppendSerialStatusCmdWhere(
    PXSelectBase<INLotSerialStatusByCostCenter> cmd,
    FSAppointmentDet apptLine,
    INLotSerClass lotSerClass)
  {
    if (apptLine.SubItemID.HasValue)
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.subItemID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.subItemID, IBqlInt>.FromCurrent>>>();
    if (apptLine.LocationID.HasValue)
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.locationID, IBqlInt>.IsEqual<BqlField<INLotSerialStatusByCostCenter.locationID, IBqlInt>.FromCurrent>>>();
    else if (apptLine.TranType == "TRX")
      cmd.WhereAnd<Where<BqlOperand<INLocation.transfersValid, IBqlBool>.IsEqual<True>>>();
    else
      cmd.WhereAnd<Where<BqlOperand<INLocation.salesValid, IBqlBool>.IsEqual<True>>>();
    if (!lotSerClass.IsManualAssignRequired.GetValueOrDefault())
      return;
    if (string.IsNullOrEmpty(apptLine.LotSerialNbr))
      cmd.WhereAnd<Where<BqlOperand<True, IBqlBool>.IsEqual<False>>>();
    else
      cmd.WhereAnd<Where<BqlOperand<INLotSerialStatusByCostCenter.lotSerialNbr, IBqlString>.IsEqual<BqlField<INLotSerialStatusByCostCenter.lotSerialNbr, IBqlString>.FromCurrent>>>();
  }

  public override FSAppointmentDet Clone(FSAppointmentDet line)
  {
    FSAppointmentDet fsAppointmentDet = base.Clone(line);
    fsAppointmentDet.OrigSrvOrdNbr = (string) null;
    fsAppointmentDet.OrigLineNbr = new int?();
    return fsAppointmentDet;
  }

  protected override INLotSerTrack.Mode GetTranTrackMode(ILSMaster row, INLotSerClass lotSerClass)
  {
    return !(lotSerClass.LotSerAssign == "U") ? INLotSerTrack.Mode.Manual : base.GetTranTrackMode(row, lotSerClass);
  }

  public virtual void _TruncateNumbers(FSAppointmentDet apptLine, Decimal deltaBaseQty)
  {
    Decimal baseExistingSplitTotalQty;
    this.GetExistingSplits(apptLine, out baseExistingSplitTotalQty);
    if (!(baseExistingSplitTotalQty > deltaBaseQty))
      return;
    apptLine.UnassignedQty = new Decimal?(0M);
    this.TruncateNumbers(apptLine, baseExistingSplitTotalQty - deltaBaseQty);
  }

  public virtual void _UpdateParent(FSAppointmentDet apptLine)
  {
    int? locationId1 = apptLine.LocationID;
    Decimal baseExistingSplitTotalQty;
    Decimal existingSplitTotalQty;
    List<FSApptLineSplit> existingSplits = this.GetExistingSplits(apptLine, out baseExistingSplitTotalQty, out existingSplitTotalQty);
    if (existingSplits.Count < 2)
    {
      this.UpdateParent(apptLine);
      int? nullable = locationId1;
      int? locationId2 = apptLine.LocationID;
      if (!(nullable.GetValueOrDefault() == locationId2.GetValueOrDefault() & nullable.HasValue == locationId2.HasValue))
      {
        FSApptLineSplit fsApptLineSplit = existingSplits.FirstOrDefault<FSApptLineSplit>((Func<FSApptLineSplit, bool>) (spl =>
        {
          int? locationId3 = spl.LocationID;
          int? locationId4 = apptLine.LocationID;
          if (!(locationId3.GetValueOrDefault() == locationId4.GetValueOrDefault() & locationId3.HasValue == locationId4.HasValue))
            return false;
          int? inventoryId1 = spl.InventoryID;
          int? inventoryId2 = apptLine.InventoryID;
          return inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue;
        }));
        if (fsApptLineSplit != null)
        {
          int? siteId = fsApptLineSplit.SiteID;
          nullable = apptLine.SiteID;
          if (!(siteId.GetValueOrDefault() == nullable.GetValueOrDefault() & siteId.HasValue == nullable.HasValue))
            apptLine.SiteID = fsApptLineSplit.SiteID;
        }
      }
    }
    PX.Objects.IN.InventoryItem inventoryItem;
    INLotSerClass inLotSerClass1;
    this.ReadInventoryItem(apptLine.InventoryID).Deconstruct(ref inventoryItem, ref inLotSerClass1);
    INLotSerClass inLotSerClass2 = inLotSerClass1;
    if (inLotSerClass2.LotSerTrack == "S")
    {
      Decimal? baseEffTranQty = apptLine.BaseEffTranQty;
      Decimal num = 1M;
      if (baseEffTranQty.GetValueOrDefault() > num & baseEffTranQty.HasValue)
      {
        apptLine.LotSerialNbr = (string) null;
        goto label_10;
      }
    }
    if (inLotSerClass2.LotSerTrack == "L" && existingSplits.Count > 1)
      apptLine.LotSerialNbr = (string) null;
label_10:
    this.UpdateLineStatusBasedOnReceivedPurchaseItems(((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current, apptLine, this.MustHaveRequestPOStatus(apptLine), existingSplits, new Decimal?(baseExistingSplitTotalQty), new Decimal?(existingSplitTotalQty), true);
  }

  public virtual bool MustHaveRequestPOStatus(FSAppointmentDet apptLine)
  {
    return FSPOReceiptProcess.MustHaveRequestPOStatusStatic(apptLine);
  }

  public virtual void UpdateLineStatusBasedOnReceivedPurchaseItems(
    FSAppointment appt,
    FSAppointmentDet apptLine,
    bool rowMustHaveRequestPOStatus,
    List<FSApptLineSplit> existingSplits,
    Decimal? baseExistingSplitTotalQty,
    Decimal? existingSplitTotalQty,
    bool runSetValueExt)
  {
    FSPOReceiptProcess.UpdateLineStatusBasedOnReceivedPurchaseItemsStatic(appt, (PXCache) this.LineCache, apptLine, rowMustHaveRequestPOStatus, existingSplits, baseExistingSplitTotalQty, existingSplitTotalQty, runSetValueExt);
  }

  protected virtual List<FSApptLineSplit> GetExistingSplits(
    FSAppointmentDet apptLine,
    out Decimal baseExistingSplitTotalQty)
  {
    return this.GetExistingSplits(apptLine, out baseExistingSplitTotalQty, out Decimal _);
  }

  protected virtual List<FSApptLineSplit> GetExistingSplits(
    FSAppointmentDet apptLine,
    out Decimal baseExistingSplitTotalQty,
    out Decimal existingSplitTotalQty)
  {
    baseExistingSplitTotalQty = 0M;
    existingSplitTotalQty = 0M;
    List<FSApptLineSplit> existingSplits = new List<FSApptLineSplit>();
    foreach (FSApptLineSplit selectChild in PXParentAttribute.SelectChildren((PXCache) this.SplitCache, (object) apptLine, typeof (FSAppointmentDet)))
    {
      baseExistingSplitTotalQty += selectChild.BaseQty.Value;
      existingSplitTotalQty += selectChild.Qty.Value;
      existingSplits.Add(selectChild);
    }
    return existingSplits;
  }

  public virtual void FillLotSerialAndPOFields(FSApptLineSplit split, FSSODetSplit soDetSplit)
  {
    FSPOReceiptProcess.FillLotSerialAndPOFieldsStatic(split, soDetSplit);
  }

  public virtual bool VerifyLotSerialTotalQty(FSApptLineSplit split, Decimal newIncrease)
  {
    return newIncrease < 0M || this.VerifyLotSerialTotalQty(PXParentAttribute.SelectParent<FSAppointmentDet>((PXCache) this.SplitCache, (object) split), newIncrease, true);
  }

  public virtual bool VerifyLotSerialTotalQty(
    FSAppointmentDet apptLine,
    Decimal newIncrease,
    bool runningFieldVerifying)
  {
    Decimal existingSplitTotalQty;
    this.GetExistingSplits(apptLine, out Decimal _, out existingSplitTotalQty);
    Decimal num1 = existingSplitTotalQty + newIncrease;
    if (apptLine.InventoryID.HasValue)
      num1 = INUnitAttribute.ConvertFromTo<FSAppointmentDet.inventoryID>((PXCache) this.LineCache, (object) apptLine, PXResult<PX.Objects.IN.InventoryItem, INLotSerClass>.op_Implicit(this.ReadInventoryItem(apptLine.InventoryID) ?? throw new PXException("The {0} record was not found.", new object[1]
      {
        (object) DACHelper.GetDisplayName(typeof (PX.Objects.IN.InventoryItem))
      })).BaseUnit, apptLine.UOM, num1, INPrecision.QUANTITY);
    Decimal num2 = num1;
    Decimal? effTranQty = apptLine.EffTranQty;
    Decimal valueOrDefault = effTranQty.GetValueOrDefault();
    if (!(num2 > valueOrDefault & effTranQty.HasValue))
      return true;
    PXSetPropertyException propertyException = new PXSetPropertyException("The total quantity of Lots/Serials specified ({0}) exceeds the quantity required by the item line ({1}).", (PXErrorLevel) 4, new object[2]
    {
      (object) num1.ToString("0"),
      (object) apptLine.EffTranQty.Value.ToString("0")
    });
    if (runningFieldVerifying)
      throw propertyException;
    ((PXCache) this.LineCache).RaiseExceptionHandling<FSAppointmentDet.effTranQty>((object) apptLine, (object) apptLine.EffTranQty, (Exception) propertyException);
    return false;
  }

  protected virtual void InsertLotSerialsFromServiceOrder(
    FSAppointmentDet apptLine,
    INLotSerClass lotSerClass)
  {
    if (!apptLine.SODetID.HasValue)
      return;
    int? soDetId = apptLine.SODetID;
    int num1 = 0;
    if (soDetId.GetValueOrDefault() < num1 & soDetId.HasValue)
      return;
    bool flag1 = false;
    bool? nullable = apptLine.EnablePO;
    if (nullable.GetValueOrDefault() && !this.MustHaveRequestPOStatus(apptLine) && EnumerableExtensions.IsNotIn<string>(lotSerClass.LotSerTrack, "S", "L"))
      flag1 = true;
    if (!flag1)
    {
      FSSrvOrdType current = (FSSrvOrdType) ((PXCache) GraphHelper.Caches<FSSrvOrdType>((PXGraph) this.Base)).Current;
      if (current == null)
        return;
      nullable = current.SetLotSerialNbrInAppts;
      bool flag2 = false;
      if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
        return;
    }
    Decimal existingSplitTotalQty;
    List<FSApptLineSplit> existingSplits = this.GetExistingSplits(apptLine, out Decimal _, out existingSplitTotalQty);
    Decimal num2 = apptLine.EffTranQty.Value - existingSplitTotalQty;
    if (!(num2 > 0M))
      return;
    FSSODet fssoDet = PXResultset<FSSODet>.op_Implicit(PXSelectBase<FSSODet, PXViewOf<FSSODet>.BasedOn<SelectFromBase<FSSODet, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<FSSODet.sODetID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) apptLine.SODetID
    }));
    if (fssoDet == null)
      throw new PXException("The {0} record was not found.", new object[1]
      {
        (object) DACHelper.GetDisplayName(typeof (FSSODet))
      });
    foreach (FSSODetSplit fssoDetSplit in GraphHelper.RowCast<FSSODetSplit>((IEnumerable) PXSelectBase<FSSODetSplit, PXViewOf<FSSODetSplit>.BasedOn<SelectFromBase<FSSODetSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODetSplit.srvOrdType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<FSSODetSplit.refNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<FSSODetSplit.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<FSSODetSplit.pOCreate, IBqlBool>.IsEqual<False>>>.Order<PX.Data.BQL.Fluent.By<BqlField<FSSODetSplit.splitLineNbr, IBqlInt>.Asc>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) fssoDet.SrvOrdType,
      (object) fssoDet.RefNbr,
      (object) fssoDet.LineNbr
    })).ToList<FSSODetSplit>())
    {
      FSSODetSplit soSplit = fssoDetSplit;
      if (!string.IsNullOrEmpty(soSplit.LotSerialNbr) || soSplit.POReceiptNbr != null)
      {
        Decimal lotSerialAvailQty;
        Decimal lotSerialUsedQty;
        this.GetLotSerialAvailability(apptLine, soSplit.LotSerialNbr, soSplit.SplitLineNbr, true, out lotSerialAvailQty, out lotSerialUsedQty, out bool _);
        Decimal num3 = lotSerialAvailQty - lotSerialUsedQty;
        FSApptLineSplit fsApptLineSplit1 = (FSApptLineSplit) null;
        if (num3 > 0M)
        {
          fsApptLineSplit1 = existingSplits.Find((Predicate<FSApptLineSplit>) (x =>
          {
            if (string.Equals(x.LotSerialNbr, soSplit.LotSerialNbr, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(soSplit.LotSerialNbr))
              return true;
            int? origSplitLineNbr = x.OrigSplitLineNbr;
            int? splitLineNbr = soSplit.SplitLineNbr;
            return origSplitLineNbr.GetValueOrDefault() == splitLineNbr.GetValueOrDefault() & origSplitLineNbr.HasValue == splitLineNbr.HasValue;
          }));
          if (fsApptLineSplit1 != null)
            num3 -= fsApptLineSplit1.Qty.Value;
        }
        if (num3 > 0M)
        {
          if (fsApptLineSplit1 == null)
          {
            fsApptLineSplit1 = this.LineToSplit(apptLine);
            fsApptLineSplit1.UOM = soSplit.UOM;
            fsApptLineSplit1.BaseQty = new Decimal?(0M);
            fsApptLineSplit1.Qty = new Decimal?(0M);
            this.FillLotSerialAndPOFields(fsApptLineSplit1, soSplit);
          }
          Decimal num4 = INUnitAttribute.ConvertFromTo<FSApptLineSplit.inventoryID>((PXCache) this.SplitCache, (object) fsApptLineSplit1, fsApptLineSplit1.UOM, apptLine.UOM, num3, INPrecision.NOROUND);
          if (num4 > num2)
            num3 = INUnitAttribute.ConvertFromTo<FSApptLineSplit.inventoryID>((PXCache) this.SplitCache, (object) fsApptLineSplit1, apptLine.UOM, fsApptLineSplit1.UOM, num2, INPrecision.QUANTITY);
          FSApptLineSplit fsApptLineSplit2 = fsApptLineSplit1;
          Decimal? qty = fsApptLineSplit2.Qty;
          Decimal num5 = num3;
          fsApptLineSplit2.Qty = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() + num5) : new Decimal?();
          fsApptLineSplit1.BaseQty = new Decimal?(INUnitAttribute.ConvertToBase((PXCache) this.SplitCache, fsApptLineSplit1.InventoryID, fsApptLineSplit1.UOM, fsApptLineSplit1.Qty.Value, fsApptLineSplit1.BaseQty, INPrecision.QUANTITY));
          num2 -= num4 > num2 ? num2 : num4;
          this.SplitCache.Update(fsApptLineSplit1);
        }
        Decimal num6 = num2;
        if (fsApptLineSplit1 != null)
          INUnitAttribute.ConvertFromTo<FSApptLineSplit.inventoryID>((PXCache) this.SplitCache, (object) fsApptLineSplit1, apptLine.UOM, fsApptLineSplit1.UOM, num2, INPrecision.QUANTITY);
        if (num6 <= 0M)
          break;
      }
    }
  }

  protected virtual void UpdateLotSerialSplitsBasedOnLineLotSerial(
    FSAppointmentDet apptLine,
    string lotSerTrack,
    bool? lotSerTrackExpiration)
  {
    if (string.IsNullOrEmpty(apptLine.LotSerialNbr) || lotSerTrack == "N")
      return;
    List<FSApptLineSplit> existingSplits = this.GetExistingSplits(apptLine, out Decimal _);
    FSApptLineSplit fsApptLineSplit1 = (FSApptLineSplit) null;
    foreach (FSApptLineSplit fsApptLineSplit2 in existingSplits)
    {
      if (string.Equals(fsApptLineSplit2.LotSerialNbr, apptLine.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
        fsApptLineSplit1 = fsApptLineSplit2;
      else
        this.SplitCache.Delete(fsApptLineSplit2);
    }
    Decimal? qtyDefault;
    this.GetLotSerialQtyDefault(apptLine, apptLine.LotSerialNbr, lotSerTrack, out qtyDefault);
    Decimal? nullable1 = qtyDefault;
    Decimal? effTranQty = apptLine.EffTranQty;
    if (nullable1.GetValueOrDefault() > effTranQty.GetValueOrDefault() & nullable1.HasValue & effTranQty.HasValue)
      qtyDefault = apptLine.EffTranQty;
    Decimal? nullable2;
    if (fsApptLineSplit1 == null)
    {
      nullable2 = qtyDefault;
      Decimal num = 0M;
      if (nullable2.GetValueOrDefault() > num & nullable2.HasValue)
      {
        fsApptLineSplit1 = this.SplitCache.Rows.CreateCopy(this.SplitCache.Insert(new FSApptLineSplit()));
        fsApptLineSplit1.LotSerialNbr = apptLine.LotSerialNbr;
        if (lotSerTrackExpiration.GetValueOrDefault())
          fsApptLineSplit1.ExpireDate = this.ExpireDateByLot((ILSMaster) fsApptLineSplit1, (ILSMaster) apptLine);
      }
    }
    if (fsApptLineSplit1 == null)
      return;
    nullable2 = qtyDefault;
    Decimal num1 = 0M;
    if (nullable2.GetValueOrDefault() > num1 & nullable2.HasValue)
    {
      fsApptLineSplit1.Qty = qtyDefault;
      FSApptLineSplit fsApptLineSplit3 = fsApptLineSplit1;
      PXCache<FSApptLineSplit> splitCache = this.SplitCache;
      int? inventoryId = fsApptLineSplit1.InventoryID;
      string uom = fsApptLineSplit1.UOM;
      nullable2 = fsApptLineSplit1.Qty;
      Decimal valueOrDefault = nullable2.GetValueOrDefault();
      Decimal? nullable3 = new Decimal?(INUnitAttribute.ConvertToBase((PXCache) splitCache, inventoryId, uom, valueOrDefault, INPrecision.QUANTITY));
      fsApptLineSplit3.BaseQty = nullable3;
      try
      {
        this.Base.SkipLotSerialFieldVerifying = true;
        this.SplitCache.Update(fsApptLineSplit1);
      }
      finally
      {
        this.Base.SkipLotSerialFieldVerifying = false;
      }
    }
    else
      this.SplitCache.Delete(fsApptLineSplit1);
  }

  protected virtual void GetLotSerialQtyDefault(
    FSAppointmentDet apptLine,
    string lotSerialNbr,
    string lotSerTrack,
    out Decimal? qtyDefault)
  {
    Decimal lotSerialAvailQty;
    Decimal lotSerialUsedQty;
    this.GetLotSerialAvailability(apptLine, lotSerialNbr, new int?(), true, out lotSerialAvailQty, out lotSerialUsedQty, out bool _);
    qtyDefault = new Decimal?(lotSerialAvailQty - lotSerialUsedQty);
  }

  public virtual void GetLotSerialAvailability(
    FSAppointmentDet apptLine,
    string lotSerialNbr,
    int? splitLineNbr,
    bool ignoreUseByApptLine,
    out Decimal lotSerialAvailQty,
    out Decimal lotSerialUsedQty,
    out bool foundServiceOrderAllocation)
  {
    FSApptLotSerialNbrAttribute.GetLotSerialAvailabilityStatic((PXGraph) this.Base, apptLine, lotSerialNbr, splitLineNbr, ignoreUseByApptLine, out lotSerialAvailQty, out lotSerialUsedQty, out foundServiceOrderAllocation);
  }

  protected virtual FSINLotSerialNbrAttribute GetLotSerialSelector()
  {
    if (this._LotSerialSelector != null)
      return this._LotSerialSelector;
    FSINLotSerialNbrAttribute serialNbrAttribute = ((PXCache) this.SplitCache).GetAttributes<FSApptLineSplit.lotSerialNbr>().OfType<FSINLotSerialNbrAttribute>().FirstOrDefault<FSINLotSerialNbrAttribute>();
    return serialNbrAttribute != null ? (this._LotSerialSelector = serialNbrAttribute) : (FSINLotSerialNbrAttribute) null;
  }

  public virtual void MarkParentAsUpdated()
  {
    if (this.LineCurrent == null)
      return;
    GraphHelper.MarkUpdated((PXCache) this.LineCache, (object) this.LineCurrent);
    if (((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current == null)
      return;
    ((PXSelectBase<FSAppointment>) this.Base.AppointmentRecords).Current.MustUpdateServiceOrder = new bool?(true);
  }

  /// <summary>
  /// Inserts FSAppointmentDet into cache without adding the splits.
  /// The Splits have to be added manually.
  /// </summary>
  /// <param name="apptLine">Master record.</param>
  public virtual FSAppointmentDet InsertWithoutSplits(FSAppointmentDet apptLine)
  {
    using (this.SuppressedModeScope(true))
    {
      FSAppointmentDet key = this.LineCache.Insert(apptLine);
      this.LineCounters.Remove(key);
      return key;
    }
  }

  public override FSApptLineSplit EnsureSplit(ILSMaster row)
  {
    FSApptLineSplit fsApptLineSplit = base.EnsureSplit(row);
    if (fsApptLineSplit != null)
    {
      int? nullable = fsApptLineSplit.InventoryID;
      if (nullable.HasValue)
      {
        nullable = fsApptLineSplit.SubItemID;
        if (!nullable.HasValue)
          fsApptLineSplit.SubItemID = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, fsApptLineSplit.InventoryID).DefaultSubItemID;
      }
    }
    return fsApptLineSplit;
  }

  /// <exclude />
  public sealed class LotSerOptionsExt : PXCacheExtension<FSAppointment>
  {
    public static bool IsActive()
    {
      return PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();
    }

    /// <exclude />
    [PXInt]
    public int? LastSelectedSplitSource { get; set; }

    public abstract class lastSelectedLineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      FSAppointmentLineSplittingExtension.LotSerOptionsExt.lastSelectedLineNbr>
    {
    }
  }
}
