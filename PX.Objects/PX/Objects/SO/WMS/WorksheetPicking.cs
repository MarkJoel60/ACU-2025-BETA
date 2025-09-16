// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.WMS.WorksheetPicking
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.Extensions;
using PX.Objects.IN;
using PX.Objects.IN.WMS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

#nullable enable
namespace PX.Objects.SO.WMS;

public class WorksheetPicking : 
  BarcodeDrivenStateMachine<
  #nullable disable
  PickPackShip, PickPackShip.Host>.ScanExtension
{
  public FbqlSelect<SelectFromBase<SOPickingWorksheet, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  SOPickingWorksheet.worksheetNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  WorksheetScanHeader.worksheetNbr, IBqlString>.FromCurrent.NoDefault>>, 
  #nullable disable
  SOPickingWorksheet>.View Worksheet;
  public FbqlSelect<SelectFromBase<SOPicker, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  SOPicker.worksheetNbr, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  WorksheetScanHeader.worksheetNbr, IBqlString>.FromCurrent.NoDefault>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  SOPicker.pickerNbr, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  WorksheetScanHeader.pickerNbr, IBqlInt>.FromCurrent.NoDefault>>>, 
  #nullable disable
  SOPicker>.View Picker;
  public FbqlSelect<SelectFromBase<SOPickingJob, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<SOPickingJob.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickingJob.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickingJob>, SOPicker, SOPickingJob>.SameAsCurrent>, SOPickingJob>.View PickingJob;
  public FbqlSelect<SelectFromBase<SOPickerToShipmentLink, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<SOPickerToShipmentLink.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickerToShipmentLink.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickerToShipmentLink>, SOPicker, SOPickerToShipmentLink>.SameAsCurrent>, SOPickerToShipmentLink>.View ShipmentsOfPicker;
  public FbqlSelect<SelectFromBase<SOPickListEntryToCartSplitLink, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCartSplit>.On<SOPickListEntryToCartSplitLink.FK.CartSplit>>>.Where<KeysRelation<CompositeKey<Field<SOPickListEntryToCartSplitLink.siteID>.IsRelatedTo<INCart.siteID>, Field<SOPickListEntryToCartSplitLink.cartID>.IsRelatedTo<INCart.cartID>>.WithTablesOf<INCart, SOPickListEntryToCartSplitLink>, INCart, SOPickListEntryToCartSplitLink>.SameAsCurrent>, SOPickListEntryToCartSplitLink>.View PickerCartSplitLinks;
  public FbqlSelect<SelectFromBase<SOPickerListEntry, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INLocation>.On<SOPickerListEntry.FK.Location>>>.Where<KeysRelation<CompositeKey<Field<SOPickerListEntry.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickerListEntry.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickerListEntry>, SOPicker, SOPickerListEntry>.SameAsCurrent>, SOPickerListEntry>.View PickListOfPicker;
  public PXAction<ScanHeader> ReviewPickWS;

  public static bool IsActive() => WaveBatchPicking.IsActive() || PaperlessPicking.IsActive();

  protected virtual IEnumerable pickListOfPicker()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    pxDelegateResult.IsResultSorted = true;
    ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) this.GetListEntries(this.WorksheetNbr, this.PickerNbr));
    return (IEnumerable) pxDelegateResult;
  }

  [PXButton]
  [PXUIField(DisplayName = "Review")]
  protected virtual IEnumerable reviewPickWS(PXAdapter adapter) => adapter.Get();

  public WorksheetScanHeader WSHeader
  {
    get => ScanHeaderExt.Get<WorksheetScanHeader>(this.Basis.Header) ?? new WorksheetScanHeader();
  }

  public ValueSetter<ScanHeader>.Ext<WorksheetScanHeader> WSSetter
  {
    get => this.Basis.HeaderSetter.With<WorksheetScanHeader>();
  }

  public string WorksheetNbr
  {
    get => this.WSHeader.WorksheetNbr;
    set
    {
      ValueSetter<ScanHeader>.Ext<WorksheetScanHeader> wsSetter = this.WSSetter;
      (^ref wsSetter).Set<string>((Expression<Func<WorksheetScanHeader, string>>) (h => h.WorksheetNbr), value);
    }
  }

  public int? PickerNbr
  {
    get => this.WSHeader.PickerNbr;
    set
    {
      ValueSetter<ScanHeader>.Ext<WorksheetScanHeader> wsSetter = this.WSSetter;
      (^ref wsSetter).Set<int?>((Expression<Func<WorksheetScanHeader, int?>>) (h => h.PickerNbr), value);
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<ScanHeader> e)
  {
    if (e.Row == null)
      return;
    ((PXAction) this.ReviewPickWS).SetVisible(((PXGraph) ((PXGraphExtension<PickPackShip.Host>) this).Base).IsMobile && this.IsWorksheetMode(e.Row.Mode));
    bool isWorksheetMode = this.ShowWorksheetNbrForMode(e.Row.Mode);
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ScanHeader>>) e).Cache, (object) null).For<WMSScanHeader.refNbr>((Action<PXUIFieldAttribute>) (ui => ui.Visible = !isWorksheetMode));
    chained = chained.For<WorksheetScanHeader.worksheetNbr>((Action<PXUIFieldAttribute>) (ui => ui.Visible = isWorksheetMode));
    chained.SameFor<WorksheetScanHeader.pickerNbr>();
    if (string.IsNullOrEmpty(this.WorksheetNbr))
    {
      ((PXSelectBase<SOPickingWorksheet>) this.Worksheet).Current = (SOPickingWorksheet) null;
      ((PXSelectBase<SOPicker>) this.Picker).Current = (SOPicker) null;
      ((PXSelectBase<SOPickingJob>) this.PickingJob).Current = (SOPickingJob) null;
    }
    else
    {
      ((PXSelectBase<SOPickingWorksheet>) this.Worksheet).Current = PXResultset<SOPickingWorksheet>.op_Implicit(((PXSelectBase<SOPickingWorksheet>) this.Worksheet).Select(Array.Empty<object>()));
      ((PXSelectBase<SOPicker>) this.Picker).Current = PXResultset<SOPicker>.op_Implicit(((PXSelectBase<SOPicker>) this.Picker).Select(Array.Empty<object>()));
      ((PXSelectBase<SOPickingJob>) this.PickingJob).Current = PXResultset<SOPickingJob>.op_Implicit(((PXSelectBase<SOPickingJob>) this.PickingJob).Select(Array.Empty<object>()));
    }
  }

  [ShipmentAndWorksheetBorrowedNote]
  protected virtual void _(PX.Data.Events.CacheAttached<ScanHeader.noteID> e)
  {
  }

  public virtual IEnumerable<PXResult<SOPickerListEntry, INLocation>> GetListEntries(
    string worksheetNbr,
    int? pickerNbr)
  {
    return this.GetListEntries(worksheetNbr, pickerNbr, false);
  }

  public virtual IEnumerable<PXResult<SOPickerListEntry, INLocation>> GetListEntries(
    string worksheetNbr,
    int? pickerNbr,
    bool inverseList)
  {
    (IEnumerable<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>> source1, IEnumerable<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>> source2) = EnumerableExtensions.DisuniteBy<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>>(GraphHelper.QuickSelect(((PXSelectBase) new FbqlSelect<SelectFromBase<SOPickerListEntry, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPicker>.On<SOPickerListEntry.FK.Picker>>, FbqlJoins.Inner<INLocation>.On<SOPickerListEntry.FK.Location>>, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<SOPickerListEntry.FK.InventoryItem>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPicker.worksheetNbr, Equal<P.AsString>>>>>.And<BqlOperand<SOPicker.pickerNbr, IBqlInt>.IsEqual<P.AsInt>>>, SOPickerListEntry>.View(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis))).View, new object[2]
    {
      (object) worksheetNbr,
      (object) pickerNbr
    }).Cast<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>>(), (Func<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, bool>) (s => isProcessed(((PXResult) s).GetItem<SOPickerListEntry>())));
    List<PXResult<SOPickerListEntry, INLocation>> listEntries = new List<PXResult<SOPickerListEntry, INLocation>>();
    listEntries.AddRange(source2.OrderBy<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, int?>((Func<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, int?>) (r => ((PXResult) r).GetItem<INLocation>().PathPriority)).ThenBy<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, string>((Func<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, string>) (r => ((PXResult) r).GetItem<INLocation>().LocationCD)).ThenByAccordanceTo<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>>((Func<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, bool>) (r => ((PXResult) r).GetItem<SOPickerListEntry>().IsUnassigned.GetValueOrDefault())).ThenByAccordanceTo<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>>((Func<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, bool>) (r => ((PXResult) r).GetItem<SOPickerListEntry>().HasGeneratedLotSerialNbr.GetValueOrDefault())).ThenBy<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, string>((Func<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, string>) (r => ((PXResult) r).GetItem<PX.Objects.IN.InventoryItem>().InventoryCD)).ThenBy<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, string>((Func<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, string>) (r => ((PXResult) r).GetItem<SOPickerListEntry>().LotSerialNbr)).ThenByAccordanceTo<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>>((Func<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, bool>) (r =>
    {
      Decimal? pickedQty = ((PXResult) r).GetItem<SOPickerListEntry>().PickedQty;
      Decimal num = 0M;
      return pickedQty.GetValueOrDefault() > num & pickedQty.HasValue;
    })).ThenBy<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, Decimal?>((Func<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, Decimal?>) (r => ((PXResult) r).GetItem<SOPickerListEntry>().With<SOPickerListEntry, Decimal?>((Func<SOPickerListEntry, Decimal?>) (e =>
    {
      Decimal? qty = e.Qty;
      Decimal? pickedQty = e.PickedQty;
      return !(qty.HasValue & pickedQty.HasValue) ? new Decimal?() : new Decimal?(qty.GetValueOrDefault() - pickedQty.GetValueOrDefault());
    })))).Select<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, PXResult<SOPickerListEntry, INLocation>>((Func<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, PXResult<SOPickerListEntry, INLocation>>) (r => new PXResult<SOPickerListEntry, INLocation>(PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>.op_Implicit(r), PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>.op_Implicit(r)))).With<IEnumerable<PXResult<SOPickerListEntry, INLocation>>, IEnumerable<PXResult<SOPickerListEntry, INLocation>>>((Func<IEnumerable<PXResult<SOPickerListEntry, INLocation>>, IEnumerable<PXResult<SOPickerListEntry, INLocation>>>) (rs => !inverseList ? rs : rs.Reverse<PXResult<SOPickerListEntry, INLocation>>())));
    listEntries.AddRange(source1.OrderBy<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, int?>((Func<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, int?>) (r => ((PXResult) r).GetItem<INLocation>().PathPriority)).ThenBy<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, string>((Func<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, string>) (r => ((PXResult) r).GetItem<INLocation>().LocationCD)).ThenBy<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, string>((Func<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, string>) (r => ((PXResult) r).GetItem<PX.Objects.IN.InventoryItem>().InventoryCD)).ThenBy<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, string>((Func<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, string>) (r => ((PXResult) r).GetItem<SOPickerListEntry>().LotSerialNbr)).Select<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, PXResult<SOPickerListEntry, INLocation>>((Func<PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>, PXResult<SOPickerListEntry, INLocation>>) (r => new PXResult<SOPickerListEntry, INLocation>(PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>.op_Implicit(r), PXResult<SOPickerListEntry, SOPicker, INLocation, PX.Objects.IN.InventoryItem>.op_Implicit(r)))).With<IEnumerable<PXResult<SOPickerListEntry, INLocation>>, IEnumerable<PXResult<SOPickerListEntry, INLocation>>>((Func<IEnumerable<PXResult<SOPickerListEntry, INLocation>>, IEnumerable<PXResult<SOPickerListEntry, INLocation>>>) (rs => !inverseList ? rs.Reverse<PXResult<SOPickerListEntry, INLocation>>() : rs)));
    return (IEnumerable<PXResult<SOPickerListEntry, INLocation>>) listEntries;

    static bool isProcessed(SOPickerListEntry e)
    {
      Decimal? pickedQty = e.PickedQty;
      Decimal? qty = e.Qty;
      return pickedQty.GetValueOrDefault() >= qty.GetValueOrDefault() & pickedQty.HasValue & qty.HasValue || e.ForceCompleted.GetValueOrDefault();
    }
  }

  public virtual bool IsWorksheetMode(string modeCode) => false;

  protected virtual bool ShowWorksheetNbrForMode(string modeCode) => this.IsWorksheetMode(modeCode);

  public virtual ScanMode<PickPackShip> FindModeForWorksheet(SOPickingWorksheet sheet)
  {
    throw new InvalidOperationException($"Worksheet of the {this.Basis.SightOf<SOPickingWorksheet.worksheetType>((IBqlTable) sheet)} type is not supported");
  }

  public virtual SOPickingWorksheet PickWorksheet
  {
    get
    {
      return SOPickingWorksheet.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), this.WorksheetNbr);
    }
  }

  public virtual SOPicker PickList
  {
    get
    {
      return SOPicker.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), this.WorksheetNbr, this.PickerNbr);
    }
  }

  public virtual bool CanWSPick
  {
    get
    {
      return ((IEnumerable<SOPickerListEntry>) ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).SelectMain(Array.Empty<object>())).Any<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (s =>
      {
        Decimal? pickedQty = s.PickedQty;
        Decimal? qty = s.Qty;
        return pickedQty.GetValueOrDefault() < qty.GetValueOrDefault() & pickedQty.HasValue & qty.HasValue && !s.ForceCompleted.GetValueOrDefault();
      }));
    }
  }

  public virtual bool NotStarted
  {
    get
    {
      return ((IEnumerable<SOPickerListEntry>) ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).SelectMain(Array.Empty<object>())).All<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (s =>
      {
        Decimal? pickedQty = s.PickedQty;
        Decimal num = 0M;
        return pickedQty.GetValueOrDefault() == num & pickedQty.HasValue && !s.ForceCompleted.GetValueOrDefault();
      }));
    }
  }

  public string ShipmentSpecialPickType
  {
    get
    {
      PX.Objects.SO.SOShipment shipment = this.Basis.Shipment;
      if (shipment != null && shipment.PickedViaWorksheet.GetValueOrDefault() && shipment.CurrentWorksheetNbr != null)
      {
        SOPickingWorksheet pickingWorksheet = SOPickingWorksheet.PK.Find((PXGraph) ((PXGraphExtension<PickPackShip.Host>) this).Base, shipment.CurrentWorksheetNbr);
        if (pickingWorksheet != null)
          return pickingWorksheet.WorksheetType;
      }
      return (string) null;
    }
  }

  public virtual bool IsLocationMissing(INLocation location, out Validation error)
  {
    if (((IEnumerable<SOPickerListEntry>) ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).SelectMain(Array.Empty<object>())).All<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (t =>
    {
      int? locationId1 = t.LocationID;
      int? locationId2 = location.LocationID;
      return !(locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue);
    })))
    {
      error = Validation.Fail("{0} location not listed in pick list.", new object[1]
      {
        (object) location.LocationCD
      });
      return true;
    }
    error = Validation.Ok;
    return false;
  }

  public virtual bool IsItemMissing(PXResult<INItemXRef, PX.Objects.IN.InventoryItem> item, out Validation error)
  {
    INItemXRef inItemXref;
    PX.Objects.IN.InventoryItem inventoryItem1;
    item.Deconstruct(ref inItemXref, ref inventoryItem1);
    PX.Objects.IN.InventoryItem inventoryItem = inventoryItem1;
    if (((IEnumerable<SOPickerListEntry>) ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).SelectMain(Array.Empty<object>())).All<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (t =>
    {
      int? inventoryId1 = t.InventoryID;
      int? inventoryId2 = inventoryItem.InventoryID;
      return !(inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue);
    })))
    {
      error = Validation.Fail("{0} item not listed in pick list.", new object[1]
      {
        (object) inventoryItem.InventoryCD
      });
      return true;
    }
    error = Validation.Ok;
    return false;
  }

  public virtual bool IsLotSerialMissing(string lotSerialNbr, out Validation error)
  {
    if (!this.Basis.LotSerialTrack.IsEnterable && ((IEnumerable<SOPickerListEntry>) ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).SelectMain(Array.Empty<object>())).All<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (t => !string.Equals(t.LotSerialNbr, lotSerialNbr, StringComparison.OrdinalIgnoreCase))))
    {
      error = Validation.Fail("{0} lot or serial number not listed in pick list.", new object[1]
      {
        (object) lotSerialNbr
      });
      return true;
    }
    error = Validation.Ok;
    return false;
  }

  public virtual bool SetLotSerialNbrAndQty(SOPickerListEntry pickedSplit, Decimal deltaQty)
  {
    Decimal? pickedQty1 = pickedSplit.PickedQty;
    Decimal num1 = 0M;
    if (pickedQty1.GetValueOrDefault() == num1 & pickedQty1.HasValue)
    {
      bool? isUnassigned = pickedSplit.IsUnassigned;
      bool flag1 = false;
      if (isUnassigned.GetValueOrDefault() == flag1 & isUnassigned.HasValue)
      {
        LSConfig lotSerialTrack = this.Basis.LotSerialTrack;
        if (lotSerialTrack.IsTrackedSerial && this.Basis.SelectedLotSerialClass.LotSerIssueMethod == "U")
        {
          SOPickerListEntry soPickerListEntry1 = PXResultset<SOPickerListEntry>.op_Implicit(((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).Search<SOPickerListEntry.lotSerialNbr>((object) this.Basis.LotSerialNbr, Array.Empty<object>()));
          if (soPickerListEntry1 == null)
          {
            pickedSplit.LotSerialNbr = this.Basis.LotSerialNbr;
            SOPickerListEntry soPickerListEntry2 = pickedSplit;
            Decimal? pickedQty2 = soPickerListEntry2.PickedQty;
            Decimal num2 = deltaQty;
            soPickerListEntry2.PickedQty = pickedQty2.HasValue ? new Decimal?(pickedQty2.GetValueOrDefault() + num2) : new Decimal?();
            pickedSplit = ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).Update(pickedSplit);
            goto label_46;
          }
          if (string.Equals(soPickerListEntry1.LotSerialNbr, this.Basis.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
            return false;
          SOPickerListEntry copy1 = PXCache<SOPickerListEntry>.CreateCopy(soPickerListEntry1);
          SOPickerListEntry copy2 = PXCache<SOPickerListEntry>.CreateCopy(pickedSplit);
          soPickerListEntry1.Qty = new Decimal?(0M);
          soPickerListEntry1.LotSerialNbr = this.Basis.LotSerialNbr;
          SOPickerListEntry soPickerListEntry3 = ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).Update(soPickerListEntry1);
          soPickerListEntry3.Qty = copy1.Qty;
          SOPickerListEntry soPickerListEntry4 = soPickerListEntry3;
          Decimal? pickedQty3 = copy2.PickedQty;
          Decimal num3 = deltaQty;
          Decimal? nullable = pickedQty3.HasValue ? new Decimal?(pickedQty3.GetValueOrDefault() + num3) : new Decimal?();
          soPickerListEntry4.PickedQty = nullable;
          soPickerListEntry3.ExpireDate = copy2.ExpireDate;
          ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).Update(soPickerListEntry3);
          pickedSplit.Qty = new Decimal?(0M);
          pickedSplit.LotSerialNbr = copy1.LotSerialNbr;
          pickedSplit = ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).Update(pickedSplit);
          pickedSplit.Qty = copy2.Qty;
          pickedSplit.PickedQty = copy1.PickedQty;
          pickedSplit.ExpireDate = copy1.ExpireDate;
          pickedSplit = ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).Update(pickedSplit);
          goto label_46;
        }
        if (pickedSplit.HasGeneratedLotSerialNbr.GetValueOrDefault())
        {
          SOPickerListEntry copy3 = PXCache<SOPickerListEntry>.CreateCopy(pickedSplit);
          Decimal? nullable = copy3.Qty;
          Decimal num4 = deltaQty;
          if (nullable.GetValueOrDefault() == num4 & nullable.HasValue)
          {
            ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).Delete(copy3);
          }
          else
          {
            SOPickerListEntry soPickerListEntry5 = copy3;
            nullable = soPickerListEntry5.Qty;
            Decimal num5 = deltaQty;
            soPickerListEntry5.Qty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - num5) : new Decimal?();
            SOPickerListEntry soPickerListEntry6 = copy3;
            nullable = soPickerListEntry6.PickedQty;
            Decimal num6 = Math.Min(deltaQty, copy3.PickedQty.Value);
            soPickerListEntry6.PickedQty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - num6) : new Decimal?();
            ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).Update(copy3);
          }
          SOPickerListEntry soPickerListEntry7 = ((IEnumerable<SOPickerListEntry>) ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).SelectMain(Array.Empty<object>())).FirstOrDefault<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (s =>
          {
            bool? generatedLotSerialNbr = s.HasGeneratedLotSerialNbr;
            bool flag2 = false;
            return generatedLotSerialNbr.GetValueOrDefault() == flag2 & generatedLotSerialNbr.HasValue && string.Equals(s.LotSerialNbr, this.Basis.LotSerialNbr ?? s.LotSerialNbr, StringComparison.OrdinalIgnoreCase) && this.IsSelectedSplit(s);
          }));
          if (soPickerListEntry7 == null)
          {
            SOPickerListEntry copy4 = PXCache<SOPickerListEntry>.CreateCopy(pickedSplit);
            copy4.EntryNbr = new int?();
            copy4.LotSerialNbr = this.Basis.LotSerialNbr;
            if (this.Basis.ExpireDate.HasValue)
              copy4.ExpireDate = this.Basis.ExpireDate;
            copy4.Qty = new Decimal?(deltaQty);
            copy4.PickedQty = new Decimal?(deltaQty);
            copy4.HasGeneratedLotSerialNbr = new bool?(false);
            ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).Insert(copy4);
            goto label_46;
          }
          SOPickerListEntry soPickerListEntry8 = soPickerListEntry7;
          nullable = soPickerListEntry8.Qty;
          Decimal num7 = deltaQty;
          soPickerListEntry8.Qty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num7) : new Decimal?();
          SOPickerListEntry soPickerListEntry9 = soPickerListEntry7;
          nullable = soPickerListEntry9.PickedQty;
          Decimal num8 = deltaQty;
          soPickerListEntry9.PickedQty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num8) : new Decimal?();
          if (this.Basis.ExpireDate.HasValue)
            soPickerListEntry7.ExpireDate = this.Basis.ExpireDate;
          ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).Update(soPickerListEntry7);
          goto label_46;
        }
        pickedSplit.LotSerialNbr = this.Basis.LotSerialNbr;
        lotSerialTrack = this.Basis.LotSerialTrack;
        if (lotSerialTrack.HasExpiration)
        {
          if (this.Basis.SelectedLotSerialClass.LotSerAssign == "R")
            pickedSplit.ExpireDate = LSSelect.ExpireDateByLot(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), (ILSMaster) PropertyTransfer.Transfer<SOPickerListEntry, SOShipLineSplit>(pickedSplit, new SOShipLineSplit()), (ILSMaster) null);
          else if (this.Basis.ExpireDate.HasValue)
            pickedSplit.ExpireDate = this.Basis.ExpireDate;
        }
        SOPickerListEntry soPickerListEntry = pickedSplit;
        Decimal? pickedQty4 = soPickerListEntry.PickedQty;
        Decimal num9 = deltaQty;
        soPickerListEntry.PickedQty = pickedQty4.HasValue ? new Decimal?(pickedQty4.GetValueOrDefault() + num9) : new Decimal?();
        pickedSplit = ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).Update(pickedSplit);
        goto label_46;
      }
    }
    bool? isUnassigned1 = pickedSplit.IsUnassigned;
    LSConfig lotSerialTrack1;
    SOPickerListEntry soPickerListEntry10;
    if (!isUnassigned1.GetValueOrDefault())
    {
      lotSerialTrack1 = this.Basis.LotSerialTrack;
      if (!lotSerialTrack1.IsTrackedLot)
      {
        soPickerListEntry10 = (SOPickerListEntry) null;
        goto label_29;
      }
    }
    soPickerListEntry10 = ((IEnumerable<SOPickerListEntry>) ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).SelectMain(Array.Empty<object>())).FirstOrDefault<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (s =>
    {
      bool? isUnassigned2 = s.IsUnassigned;
      bool flag = false;
      if (isUnassigned2.GetValueOrDefault() == flag & isUnassigned2.HasValue && s.ShipmentNbr == pickedSplit.ShipmentNbr && string.Equals(s.LotSerialNbr, this.Basis.LotSerialNbr ?? s.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
      {
        int? locationId = s.LocationID;
        int? nullable = this.Basis.LocationID ?? pickedSplit.LocationID;
        if (locationId.GetValueOrDefault() == nullable.GetValueOrDefault() & locationId.HasValue == nullable.HasValue)
          return this.IsSelectedSplit(s);
      }
      return false;
    }));
label_29:
    SOPickerListEntry soPickerListEntry11 = soPickerListEntry10;
    isUnassigned1 = pickedSplit.IsUnassigned;
    bool flag3 = false;
    Decimal? nullable1;
    if (isUnassigned1.GetValueOrDefault() == flag3 & isUnassigned1.HasValue)
    {
      Decimal? qty = pickedSplit.Qty;
      Decimal num10 = deltaQty;
      nullable1 = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() - num10) : new Decimal?();
      Decimal num11 = 0M;
      if (nullable1.GetValueOrDefault() <= num11 & nullable1.HasValue)
      {
        pickedSplit = ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).Delete(pickedSplit);
      }
      else
      {
        SOPickerListEntry soPickerListEntry12 = pickedSplit;
        nullable1 = soPickerListEntry12.Qty;
        Decimal num12 = deltaQty;
        soPickerListEntry12.Qty = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - num12) : new Decimal?();
        pickedSplit = ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).Update(pickedSplit);
      }
    }
    if (soPickerListEntry11 != null)
    {
      SOPickerListEntry soPickerListEntry13 = soPickerListEntry11;
      nullable1 = soPickerListEntry13.PickedQty;
      Decimal num13 = deltaQty;
      soPickerListEntry13.PickedQty = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num13) : new Decimal?();
      nullable1 = soPickerListEntry11.PickedQty;
      Decimal? qty = soPickerListEntry11.Qty;
      if (nullable1.GetValueOrDefault() > qty.GetValueOrDefault() & nullable1.HasValue & qty.HasValue)
        soPickerListEntry11.Qty = soPickerListEntry11.PickedQty;
      ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).Update(soPickerListEntry11);
    }
    else
    {
      SOPickerListEntry copy = PXCache<SOPickerListEntry>.CreateCopy(pickedSplit);
      copy.EntryNbr = new int?();
      copy.LotSerialNbr = this.Basis.LotSerialNbr;
      Decimal? qty = pickedSplit.Qty;
      Decimal num14 = 0M;
      if (!(qty.GetValueOrDefault() > num14 & qty.HasValue))
      {
        isUnassigned1 = pickedSplit.IsUnassigned;
        if (!isUnassigned1.GetValueOrDefault())
        {
          copy.Qty = pickedSplit.Qty;
          copy.PickedQty = pickedSplit.PickedQty;
          goto label_45;
        }
      }
      copy.Qty = new Decimal?(deltaQty);
      copy.PickedQty = new Decimal?(deltaQty);
      copy.IsUnassigned = new bool?(false);
      lotSerialTrack1 = this.Basis.LotSerialTrack;
      if (lotSerialTrack1.HasExpiration)
      {
        if (this.Basis.SelectedLotSerialClass.LotSerAssign == "R")
          copy.ExpireDate = LSSelect.ExpireDateByLot(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), (ILSMaster) PropertyTransfer.Transfer<SOPickerListEntry, SOShipLineSplit>(copy, new SOShipLineSplit()), (ILSMaster) null);
        else if (this.Basis.ExpireDate.HasValue)
          copy.ExpireDate = this.Basis.ExpireDate;
      }
label_45:
      ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).Insert(copy);
    }
label_46:
    return true;
  }

  [Obsolete("Use the GetEntriesToPick method instead.")]
  public virtual SOPickerListEntry GetSelectedPickListEntry()
  {
    return this.GetEntriesToPick().FirstOrDefault<SOPickerListEntry>();
  }

  public virtual IEnumerable<SOPickerListEntry> GetEntriesToPick()
  {
    return ((IEnumerable<PXResult<SOPickerListEntry>>) ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).Select(Array.Empty<object>())).AsEnumerable<PXResult<SOPickerListEntry>>().Select<PXResult<SOPickerListEntry>, (SOPickerListEntry, INLocation)>((Func<PXResult<SOPickerListEntry>, (SOPickerListEntry, INLocation)>) (row => (((PXResult) row).GetItem<SOPickerListEntry>(), ((PXResult) row).GetItem<INLocation>()))).Where<(SOPickerListEntry, INLocation)>((Func<(SOPickerListEntry, INLocation), bool>) (r => this.IsSelectedSplit(r.Split))).With<IEnumerable<(SOPickerListEntry, INLocation)>, IOrderedEnumerable<(SOPickerListEntry, INLocation)>>(new Func<IEnumerable<(SOPickerListEntry, INLocation)>, IOrderedEnumerable<(SOPickerListEntry, INLocation)>>(this.PrioritizeEntries)).Select<(SOPickerListEntry, INLocation), SOPickerListEntry>((Func<(SOPickerListEntry, INLocation), SOPickerListEntry>) (r => r.Split));
  }

  public virtual IOrderedEnumerable<(SOPickerListEntry Split, INLocation Location)> PrioritizeEntries(
    IEnumerable<(SOPickerListEntry Split, INLocation Location)> entries)
  {
    bool remove = this.Basis.Remove.GetValueOrDefault();
    return entries.OrderByAccordanceTo<(SOPickerListEntry, INLocation)>((Func<(SOPickerListEntry, INLocation), bool>) (r =>
    {
      bool? isUnassigned = r.Split.IsUnassigned;
      bool flag1 = false;
      int num1;
      if (isUnassigned.GetValueOrDefault() == flag1 & isUnassigned.HasValue)
      {
        bool? generatedLotSerialNbr = r.Split.HasGeneratedLotSerialNbr;
        bool flag2 = false;
        num1 = generatedLotSerialNbr.GetValueOrDefault() == flag2 & generatedLotSerialNbr.HasValue ? 1 : 0;
      }
      else
        num1 = 0;
      int num2 = remove ? 1 : 0;
      if ((num1 & num2) == 0)
      {
        Decimal? qty = r.Split.Qty;
        Decimal? pickedQty = r.Split.PickedQty;
        return qty.GetValueOrDefault() > pickedQty.GetValueOrDefault() & qty.HasValue & pickedQty.HasValue;
      }
      Decimal? pickedQty1 = r.Split.PickedQty;
      Decimal num3 = 0M;
      return pickedQty1.GetValueOrDefault() > num3 & pickedQty1.HasValue;
    })).ThenByAccordanceTo<(SOPickerListEntry, INLocation)>((Func<(SOPickerListEntry, INLocation), bool>) (r =>
    {
      if (!remove)
      {
        Decimal? qty = r.Split.Qty;
        Decimal? pickedQty = r.Split.PickedQty;
        return qty.GetValueOrDefault() > pickedQty.GetValueOrDefault() & qty.HasValue & pickedQty.HasValue;
      }
      Decimal? pickedQty2 = r.Split.PickedQty;
      Decimal num = 0M;
      return pickedQty2.GetValueOrDefault() > num & pickedQty2.HasValue;
    })).ThenByAccordanceTo<(SOPickerListEntry, INLocation)>((Func<(SOPickerListEntry, INLocation), bool>) (r => string.Equals(r.Split.LotSerialNbr, this.Basis.LotSerialNbr ?? r.Split.LotSerialNbr, StringComparison.OrdinalIgnoreCase))).ThenByAccordanceTo<(SOPickerListEntry, INLocation)>((Func<(SOPickerListEntry, INLocation), bool>) (r => string.IsNullOrEmpty(r.Split.LotSerialNbr))).ThenByAccordanceTo<(SOPickerListEntry, INLocation)>((Func<(SOPickerListEntry, INLocation), bool>) (r =>
    {
      Decimal? qty = r.Split.Qty;
      Decimal? pickedQty3 = r.Split.PickedQty;
      if (!(qty.GetValueOrDefault() > pickedQty3.GetValueOrDefault() & qty.HasValue & pickedQty3.HasValue | remove))
        return false;
      Decimal? pickedQty4 = r.Split.PickedQty;
      Decimal num = 0M;
      return pickedQty4.GetValueOrDefault() > num & pickedQty4.HasValue;
    })).ThenBy<(SOPickerListEntry, INLocation), int?>((Func<(SOPickerListEntry, INLocation), int?>) (r =>
    {
      Sign sign = Sign.MinusIf(remove);
      int? pathPriority = r.Location.PathPriority;
      return !pathPriority.HasValue ? new int?() : new int?(Sign.op_Multiply(sign, pathPriority.GetValueOrDefault()));
    })).With<IOrderedEnumerable<(SOPickerListEntry, INLocation)>, IOrderedEnumerable<(SOPickerListEntry, INLocation)>>((Func<IOrderedEnumerable<(SOPickerListEntry, INLocation)>, IOrderedEnumerable<(SOPickerListEntry, INLocation)>>) (view => !remove ? view.ThenBy<(SOPickerListEntry, INLocation), string>((Func<(SOPickerListEntry, INLocation), string>) (r => r.Location.LocationCD)) : view.ThenByDescending<(SOPickerListEntry, INLocation), string>((Func<(SOPickerListEntry, INLocation), string>) (r => r.Location.LocationCD)))).ThenByDescending<(SOPickerListEntry, INLocation), Decimal?>((Func<(SOPickerListEntry, INLocation), Decimal?>) (r =>
    {
      Sign sign = Sign.MinusIf(remove);
      Decimal? qty = r.Split.Qty;
      Decimal? pickedQty = r.Split.PickedQty;
      Decimal? nullable = qty.HasValue & pickedQty.HasValue ? new Decimal?(qty.GetValueOrDefault() - pickedQty.GetValueOrDefault()) : new Decimal?();
      return !nullable.HasValue ? new Decimal?() : new Decimal?(Sign.op_Multiply(sign, nullable.GetValueOrDefault()));
    }));
  }

  public virtual bool IsSelectedSplit(SOPickerListEntry split)
  {
    int? inventoryId1 = split.InventoryID;
    int? inventoryId2 = this.Basis.InventoryID;
    if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
    {
      int? subItemId1 = split.SubItemID;
      int? subItemId2 = this.Basis.SubItemID;
      if (subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue)
      {
        int? siteId1 = split.SiteID;
        int? siteId2 = this.Basis.SiteID;
        if (siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue)
        {
          int? locationId = split.LocationID;
          int? nullable = this.Basis.LocationID ?? split.LocationID;
          if (locationId.GetValueOrDefault() == nullable.GetValueOrDefault() & locationId.HasValue == nullable.HasValue)
          {
            if (string.Equals(split.LotSerialNbr, this.Basis.LotSerialNbr ?? split.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
              return true;
            bool? remove = this.Basis.Remove;
            bool flag = false;
            return remove.GetValueOrDefault() == flag & remove.HasValue && this.Basis.LotSerialTrack.IsEnterable;
          }
        }
      }
    }
    return false;
  }

  public virtual bool AreSplitsSimilar(SOPickerListEntry left, SOPickerListEntry right)
  {
    if (left.ShipmentNbr == right.ShipmentNbr)
    {
      int? nullable1 = left.SiteID;
      int? nullable2 = right.SiteID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        nullable2 = left.LocationID;
        nullable1 = right.LocationID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          nullable1 = left.InventoryID;
          nullable2 = right.InventoryID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            nullable2 = left.SubItemID;
            nullable1 = right.SubItemID;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue && string.Equals(left.LotSerialNbr, right.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
              return left.OrderLineUOM == right.OrderLineUOM;
          }
        }
      }
    }
    return false;
  }

  public virtual FlowStatus ConfirmSuitableSplits()
  {
    Decimal restDeltaQty = Sign.op_Multiply(Sign.MinusIf(this.Basis.Remove.GetValueOrDefault()), this.Basis.BaseQty);
    bool hasSuitableSplits = false;
    if (restDeltaQty == 0M)
      return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
    bool? nullable;
    if (!this.Basis.LotSerialTrack.IsTrackedSerial)
    {
      nullable = this.Basis.SelectedInventoryItem.WeightItem;
      if (!nullable.GetValueOrDefault())
      {
        FlowStatus flowStatus = this.ConfirmAllSplits(this.GetEntriesToPick().Select<SOPickerListEntry, SOPickerListEntry>((Func<SOPickerListEntry, SOPickerListEntry>) (s =>
        {
          hasSuitableSplits = true;
          return s;
        })), ref restDeltaQty, false);
        nullable = ((FlowStatus) ref flowStatus).IsError;
        bool flag1 = false;
        if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue))
          return flowStatus;
        if (restDeltaQty != 0M)
        {
          nullable = this.Basis.Remove;
          bool flag2 = false;
          if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
          {
            nullable = this.Basis.SelectedInventoryItem.DecimalBaseUnit;
            nullable.GetValueOrDefault();
            goto label_15;
          }
          goto label_15;
        }
        goto label_15;
      }
    }
    SOPickerListEntry pickedSplit = this.GetEntriesToPick().FirstOrDefault<SOPickerListEntry>();
    if (pickedSplit != null)
    {
      hasSuitableSplits = true;
      Decimal threshold = 1M;
      nullable = this.Basis.Remove;
      Decimal num = nullable.GetValueOrDefault() ? -Math.Min(pickedSplit.PickedQty.Value, -restDeltaQty) : Math.Min(pickedSplit.Qty.Value * threshold - pickedSplit.PickedQty.Value, restDeltaQty);
      if (this.Basis.LotSerialTrack.IsTrackedSerial && EnumerableExtensions.IsNotIn<Decimal>(num, 1M, -1M))
        return FlowStatus.Fail("Serialized items can be processed only with the base UOM and the 1.00 quantity.", Array.Empty<object>());
      FlowStatus flowStatus = this.ConfirmSplit(pickedSplit, restDeltaQty, threshold);
      nullable = ((FlowStatus) ref flowStatus).IsError;
      bool flag = false;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        return flowStatus;
      restDeltaQty -= num;
    }
label_15:
    if (!hasSuitableSplits)
    {
      nullable = this.Basis.Remove;
      FlowStatus flowStatus = FlowStatus.Fail(nullable.GetValueOrDefault() ? "No items to remove from shipment." : "No items to pick.", Array.Empty<object>());
      return ((FlowStatus) ref flowStatus).WithModeReset;
    }
    if (!(Math.Abs(restDeltaQty) > 0M))
      return FlowStatus.Ok;
    nullable = this.Basis.Remove;
    FlowStatus flowStatus1 = FlowStatus.Fail(nullable.GetValueOrDefault() ? "The picked quantity cannot be negative." : "The picked quantity cannot be greater than the quantity in the pick list line.", Array.Empty<object>());
    flowStatus1 = ((FlowStatus) ref flowStatus1).WithModeReset;
    return ((FlowStatus) ref flowStatus1).WithChangesDiscard;
  }

  public virtual FlowStatus ConfirmAllSplits(
    IEnumerable<SOPickerListEntry> splitsToConfirm,
    ref Decimal restDeltaQty,
    bool withThresholds)
  {
    foreach (SOPickerListEntry pickedSplit in splitsToConfirm)
    {
      bool? nullable1;
      Decimal num1;
      if (withThresholds)
      {
        nullable1 = this.Basis.Remove;
        bool flag = false;
        if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
        {
          num1 = 1M;
          goto label_6;
        }
      }
      num1 = 1M;
label_6:
      Decimal threshold = num1;
      nullable1 = this.Basis.Remove;
      Decimal? nullable2;
      Decimal num2;
      if (!nullable1.GetValueOrDefault())
      {
        nullable2 = pickedSplit.Qty;
        Decimal num3 = nullable2.Value * threshold;
        nullable2 = pickedSplit.PickedQty;
        Decimal num4 = nullable2.Value;
        num2 = Math.Min(num3 - num4, restDeltaQty);
      }
      else
      {
        nullable2 = pickedSplit.PickedQty;
        num2 = -Math.Min(nullable2.Value, -restDeltaQty);
      }
      Decimal deltaQty = num2;
      FlowStatus flowStatus = this.ConfirmSplit(pickedSplit, deltaQty, threshold);
      nullable1 = ((FlowStatus) ref flowStatus).IsError;
      bool flag1 = false;
      if (!(nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue))
        return ((FlowStatus) ref flowStatus).WithChangesDiscard;
      restDeltaQty -= deltaQty;
      if (restDeltaQty == 0M)
        break;
    }
    return FlowStatus.Ok;
  }

  public virtual FlowStatus ConfirmSplit(SOPickerListEntry pickedSplit, Decimal deltaQty)
  {
    return this.ConfirmSplit(pickedSplit, deltaQty, 1M);
  }

  public virtual FlowStatus ConfirmSplit(
    SOPickerListEntry pickedSplit,
    Decimal deltaQty,
    Decimal threshold)
  {
    bool flag1 = false;
    Decimal? nullable1;
    Decimal? nullable2;
    bool? isError;
    if (deltaQty < 0M)
    {
      nullable1 = pickedSplit.PickedQty;
      Decimal num1 = deltaQty;
      Decimal? nullable3 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num1) : new Decimal?();
      Decimal num2 = 0M;
      if (nullable3.GetValueOrDefault() < num2 & nullable3.HasValue)
        return FlowStatus.Fail("The picked quantity cannot be negative.", Array.Empty<object>());
    }
    else if (deltaQty > 0M)
    {
      if (pickedSplit.HasGeneratedLotSerialNbr.GetValueOrDefault() && this.Basis.LotSerialNbr != null && !string.Equals(pickedSplit.LotSerialNbr, this.Basis.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
      {
        SOPickerListEntry soPickerListEntry = ((IEnumerable<SOPickerListEntry>) ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).SelectMain(Array.Empty<object>())).FirstOrDefault<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (s =>
        {
          bool? generatedLotSerialNbr = s.HasGeneratedLotSerialNbr;
          bool flag2 = false;
          if (generatedLotSerialNbr.GetValueOrDefault() == flag2 & generatedLotSerialNbr.HasValue && string.Equals(s.LotSerialNbr, this.Basis.LotSerialNbr ?? s.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
          {
            Decimal? pickedQty = s.PickedQty;
            Decimal? qty = s.Qty;
            if (pickedQty.GetValueOrDefault() < qty.GetValueOrDefault() & pickedQty.HasValue & qty.HasValue)
              return this.IsSelectedSplit(s);
          }
          return false;
        }));
        if (soPickerListEntry != null)
          pickedSplit = soPickerListEntry;
      }
      Decimal? nullable4 = pickedSplit.PickedQty;
      Decimal num3 = deltaQty;
      nullable2 = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + num3) : new Decimal?();
      nullable4 = pickedSplit.Qty;
      Decimal num4 = threshold;
      nullable1 = nullable4.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * num4) : new Decimal?();
      if (nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
        return FlowStatus.Fail("The picked quantity cannot be greater than the quantity in the pick list line.", Array.Empty<object>());
      if (this.Basis.LotSerialNbr != null && this.Basis.LotSerialTrack.IsEnterable)
      {
        FlowStatus flowStatus = this.CheckAvailability(deltaQty, pickedSplit);
        isError = ((FlowStatus) ref flowStatus).IsError;
        bool flag3 = false;
        if (!(isError.GetValueOrDefault() == flag3 & isError.HasValue))
          return flowStatus;
        if (!string.Equals(pickedSplit.LotSerialNbr, this.Basis.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
        {
          if (!this.SetLotSerialNbrAndQty(pickedSplit, deltaQty))
            return FlowStatus.Fail("The picked quantity cannot be greater than the quantity in the pick list line.", Array.Empty<object>());
          flag1 = true;
        }
      }
    }
    this.AssignUser(true);
    if (!flag1)
    {
      SOPickerListEntry soPickerListEntry = pickedSplit;
      nullable1 = soPickerListEntry.PickedQty;
      Decimal num5 = deltaQty;
      Decimal? nullable5;
      if (!nullable1.HasValue)
      {
        nullable2 = new Decimal?();
        nullable5 = nullable2;
      }
      else
        nullable5 = new Decimal?(nullable1.GetValueOrDefault() + num5);
      soPickerListEntry.PickedQty = nullable5;
      if (deltaQty < 0M && this.Basis.LotSerialTrack.IsEnterable)
      {
        nullable1 = pickedSplit.PickedQty;
        Decimal num6 = 0M;
        if (nullable1.GetValueOrDefault() == num6 & nullable1.HasValue)
        {
          ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).Delete(pickedSplit);
        }
        else
        {
          pickedSplit.Qty = pickedSplit.PickedQty;
          ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).Update(pickedSplit);
        }
      }
      else
        ((PXSelectBase<SOPickerListEntry>) this.PickListOfPicker).Update(pickedSplit);
    }
    PPSCartSupport cartSupport = this.Basis.Get<PPSCartSupport>();
    if (cartSupport != null && cartSupport.CartID.HasValue)
    {
      FlowStatus flowStatus = this.SyncWithCart(cartSupport, pickedSplit, deltaQty);
      isError = ((FlowStatus) ref flowStatus).IsError;
      bool flag4 = false;
      if (!(isError.GetValueOrDefault() == flag4 & isError.HasValue))
        return flowStatus;
    }
    return FlowStatus.Ok;
  }

  public virtual void EnsureShipmentUserLinkForWorksheetPick()
  {
    this.Graph.WorkLogExt.EnsureFor(this.WorksheetNbr, this.PickerNbr.Value, new Guid?(((PXGraph) this.Graph).Accessinfo.UserID), "PICK");
  }

  public virtual void ReportSplitConfirmed(SOPickerListEntry pickedSplit)
  {
    this.Basis.ReportInfo(this.Basis.Remove.GetValueOrDefault() ? "{0} x {1} {2} has been removed from the pick list." : "{0} x {1} {2} has been added to the pick list.", new object[3]
    {
      (object) this.Basis.SightOf<WMSScanHeader.inventoryID>(),
      (object) this.Basis.Qty,
      (object) this.Basis.UOM
    });
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R2.")]
  public virtual FlowStatus CheckAvailability(Decimal deltaQty)
  {
    return this.CheckAvailability(deltaQty, new SOPickerListEntry()
    {
      SiteID = this.Basis.SiteID,
      LocationID = this.Basis.LocationID,
      InventoryID = this.Basis.InventoryID,
      SubItemID = this.Basis.SubItemID,
      LotSerialNbr = this.Basis.LotSerialNbr
    });
  }

  public virtual FlowStatus CheckAvailability(Decimal deltaQty, SOPickerListEntry pickedSplit)
  {
    if (this.Basis.SelectedLotSerialClass.LotSerAssign == "U" && this.Basis.SelectedLotSerialClass.LotSerTrack == "L")
      return FlowStatus.Ok;
    Decimal valueOrDefault = PXSelectBase<SOShipLineSplit, PXViewOf<SOShipLineSplit>.BasedOn<SelectFromBase<SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOShipment>.On<SOShipLineSplit.FK.Shipment>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOShipment.currentWorksheetNbr, Equal<P.AsString>>>>, And<BqlOperand<SOShipLineSplit.siteID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<SOShipLineSplit.locationID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<SOShipLineSplit.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<SOShipLineSplit.subItemID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<SOShipLineSplit.lotSerialNbr, IBqlString>.IsEqual<P.AsString>>>.Aggregate<PX.Data.BQL.Fluent.To<Sum<SOShipLineSplit.baseQty>>>>.Config>.Select(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), new object[6]
    {
      (object) this.WorksheetNbr,
      (object) pickedSplit.SiteID,
      (object) pickedSplit.LocationID,
      (object) pickedSplit.InventoryID,
      (object) pickedSplit.SubItemID,
      (object) this.Basis.LotSerialNbr
    }).TopFirst.BaseQty.GetValueOrDefault();
    Decimal num = GraphHelper.RowCast<SOPickerListEntry>((IEnumerable) PXSelectBase<SOPickerListEntry, PXViewOf<SOPickerListEntry>.BasedOn<SelectFromBase<SOPickerListEntry, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickerListEntry.worksheetNbr, Equal<P.AsString>>>>, And<BqlOperand<SOPickerListEntry.siteID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<SOPickerListEntry.locationID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<SOPickerListEntry.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<SOPickerListEntry.subItemID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<SOPickerListEntry.lotSerialNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), new object[6]
    {
      (object) this.WorksheetNbr,
      (object) pickedSplit.SiteID,
      (object) pickedSplit.LocationID,
      (object) pickedSplit.InventoryID,
      (object) pickedSplit.SubItemID,
      (object) this.Basis.LotSerialNbr
    })).Sum<SOPickerListEntry>((Func<SOPickerListEntry, Decimal>) (e => e.BasePickedQty.GetValueOrDefault())) + ((Decimal?) PXSelectBase<SOPickerListEntry, PXViewOf<SOPickerListEntry>.BasedOn<SelectFromBase<SOPickerListEntry, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPickingWorksheet>.On<KeysRelation<Field<SOPickerListEntry.worksheetNbr>.IsRelatedTo<SOPickingWorksheet.worksheetNbr>.AsSimpleKey.WithTablesOf<SOPickingWorksheet, SOPickerListEntry>, SOPickingWorksheet, SOPickerListEntry>.And<BqlOperand<SOPickingWorksheet.status, IBqlString>.IsEqual<SOPickingWorksheet.status.picking>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickerListEntry.worksheetNbr, NotEqual<P.AsString>>>>, And<BqlOperand<SOPickerListEntry.siteID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<SOPickerListEntry.locationID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<SOPickerListEntry.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<SOPickerListEntry.subItemID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<SOPickerListEntry.lotSerialNbr, IBqlString>.IsEqual<P.AsString>>>.Aggregate<PX.Data.BQL.Fluent.To<Sum<SOPickerListEntry.basePickedQty>>>>.Config>.Select(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), new object[6]
    {
      (object) this.WorksheetNbr,
      (object) pickedSplit.SiteID,
      (object) pickedSplit.LocationID,
      (object) pickedSplit.InventoryID,
      (object) pickedSplit.SubItemID,
      (object) this.Basis.LotSerialNbr
    }).TopFirst?.BasePickedQty).GetValueOrDefault();
    INLotSerialStatus topFirst = PXSelectBase<INLotSerialStatus, PXViewOf<INLotSerialStatus>.BasedOn<SelectFromBase<INLotSerialStatus, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLotSerialStatus.siteID, Equal<P.AsInt>>>>, And<BqlOperand<INLotSerialStatus.locationID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INLotSerialStatus.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INLotSerialStatus.subItemID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<INLotSerialStatus.lotSerialNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), new object[5]
    {
      (object) pickedSplit.SiteID,
      (object) pickedSplit.LocationID,
      (object) pickedSplit.InventoryID,
      (object) pickedSplit.SubItemID,
      (object) this.Basis.LotSerialNbr
    }).TopFirst;
    if (this.Basis.SelectedLotSerialClass.LotSerAssign == "U" && this.Basis.SelectedLotSerialClass.LotSerTrack == "S")
    {
      if (num > 0M || ((Decimal?) topFirst?.QtyHardAvail).GetValueOrDefault() + valueOrDefault < 0M)
        return FlowStatus.Fail("Serial Number '{1}' for item '{0}' already issued.", new object[2]
        {
          (object) this.Basis.LotSerialNbr,
          (object) this.Basis.SightOf<SOPickerListEntry.inventoryID>((IBqlTable) pickedSplit)
        });
    }
    else
    {
      if (topFirst == null)
        return FlowStatus.Fail("The {1} lot or serial number does not exist for the {0} item in the {3} location of the {2} warehouse.", new object[4]
        {
          (object) this.Basis.SightOf<SOPickerListEntry.inventoryID>((IBqlTable) pickedSplit),
          (object) this.Basis.LotSerialNbr,
          (object) this.Basis.SightOf<SOPickerListEntry.siteID>((IBqlTable) pickedSplit),
          (object) this.Basis.SightOf<SOPickerListEntry.locationID>((IBqlTable) pickedSplit)
        });
      if (num + deltaQty > ((Decimal?) topFirst?.QtyHardAvail).GetValueOrDefault() + valueOrDefault)
        return FlowStatus.Fail("The picked quantity of the {0} {1} cannot be greater than the available quantity in the {3} location of {2}.", new object[4]
        {
          (object) this.Basis.SightOf<SOPickerListEntry.inventoryID>((IBqlTable) pickedSplit),
          (object) this.Basis.LotSerialNbr,
          (object) this.Basis.SightOf<SOPickerListEntry.siteID>((IBqlTable) pickedSplit),
          (object) this.Basis.SightOf<SOPickerListEntry.locationID>((IBqlTable) pickedSplit)
        });
    }
    return FlowStatus.Ok;
  }

  public virtual bool AssignUser(bool startPicking = false)
  {
    bool flag = false;
    if (!((PXSelectBase<SOPicker>) this.Picker).Current.UserID.HasValue)
    {
      ((PXSelectBase<SOPicker>) this.Picker).Current.UserID = new Guid?(((PXGraph) this.Graph).Accessinfo.UserID);
      ((PXSelectBase<SOPicker>) this.Picker).UpdateCurrent();
      flag = true;
    }
    if (startPicking && ((SOPickingWorksheet) PrimaryKeyOf<SOPickingWorksheet>.By<SOPickingWorksheet.worksheetNbr>.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), (SOPickingWorksheet.worksheetNbr) ((PXSelectBase<SOPickingWorksheet>) this.Worksheet).Current, (PKFindOptions) 0)).Status == "N")
    {
      ((PXSelectBase<SOPickingWorksheet>) this.Worksheet).Current.Status = "I";
      ((PXSelectBase<SOPickingWorksheet>) this.Worksheet).UpdateCurrent();
      flag = true;
    }
    if (flag)
      this.EnsureShipmentUserLinkForWorksheetPick();
    return flag;
  }

  protected virtual FlowStatus SyncWithCart(
    PPSCartSupport cartSupport,
    SOPickerListEntry entry,
    Decimal deltaQty)
  {
    INCartSplit inCartSplit1 = ((IEnumerable<INCartSplit>) ((IEnumerable<INCartSplit>) GraphHelper.RowCast<INCartSplit>((IEnumerable) PXSelectBase<SOPickListEntryToCartSplitLink, PXViewOf<SOPickListEntryToCartSplitLink>.BasedOn<SelectFromBase<SOPickListEntryToCartSplitLink, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCartSplit>.On<SOPickListEntryToCartSplitLink.FK.CartSplit>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<SOPickListEntryToCartSplitLink.worksheetNbr>.IsRelatedTo<SOPickerListEntry.worksheetNbr>, Field<SOPickListEntryToCartSplitLink.pickerNbr>.IsRelatedTo<SOPickerListEntry.pickerNbr>, Field<SOPickListEntryToCartSplitLink.entryNbr>.IsRelatedTo<SOPickerListEntry.entryNbr>>.WithTablesOf<SOPickerListEntry, SOPickListEntryToCartSplitLink>, SOPickerListEntry, SOPickListEntryToCartSplitLink>.SameAsCurrent>, And<BqlOperand<SOPickListEntryToCartSplitLink.siteID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<SOPickListEntryToCartSplitLink.cartID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.SelectMultiBound(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), new object[1]
    {
      (object) entry
    }, new object[2]
    {
      (object) this.Basis.SiteID,
      (object) cartSupport.CartID
    })).ToArray<INCartSplit>()).Concat<INCartSplit>((IEnumerable<INCartSplit>) GraphHelper.RowCast<INCartSplit>((IEnumerable) PXSelectBase<INCartSplit, PXViewOf<INCartSplit>.BasedOn<SelectFromBase<INCartSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCartSplit.cartID, Equal<P.AsInt>>>>, And<BqlOperand<INCartSplit.inventoryID, IBqlInt>.IsEqual<BqlField<SOPickerListEntry.inventoryID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INCartSplit.subItemID, IBqlInt>.IsEqual<BqlField<SOPickerListEntry.subItemID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INCartSplit.siteID, IBqlInt>.IsEqual<BqlField<SOPickerListEntry.siteID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INCartSplit.fromLocationID, IBqlInt>.IsEqual<BqlField<SOPickerListEntry.locationID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<INCartSplit.lotSerialNbr, IBqlString>.IsEqual<BqlField<SOPickerListEntry.lotSerialNbr, IBqlString>.FromCurrent>>>>.Config>.SelectMultiBound(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), new object[1]
    {
      (object) entry
    }, new object[1]{ (object) cartSupport.CartID })).ToArray<INCartSplit>()).ToArray<INCartSplit>()).FirstOrDefault<INCartSplit>((Func<INCartSplit, bool>) (s => string.Equals(s.LotSerialNbr, this.Basis.LotSerialNbr ?? s.LotSerialNbr, StringComparison.OrdinalIgnoreCase)));
    INCartSplit cartSplit;
    Decimal? qty;
    if (inCartSplit1 == null)
    {
      cartSplit = ((PXSelectBase<INCartSplit>) cartSupport.CartSplits).Insert(new INCartSplit()
      {
        CartID = cartSupport.CartID,
        InventoryID = entry.InventoryID,
        SubItemID = entry.SubItemID,
        LotSerialNbr = entry.LotSerialNbr,
        ExpireDate = entry.ExpireDate,
        UOM = entry.UOM,
        SiteID = entry.SiteID,
        FromLocationID = entry.LocationID,
        Qty = new Decimal?(deltaQty)
      });
    }
    else
    {
      INCartSplit inCartSplit2 = inCartSplit1;
      qty = inCartSplit2.Qty;
      Decimal num = deltaQty;
      inCartSplit2.Qty = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() + num) : new Decimal?();
      cartSplit = ((PXSelectBase<INCartSplit>) cartSupport.CartSplits).Update(inCartSplit1);
    }
    qty = cartSplit.Qty;
    Decimal num1 = 0M;
    if (!(qty.GetValueOrDefault() == num1 & qty.HasValue))
      return this.EnsurePickerCartSplitLink(cartSupport, entry, cartSplit, deltaQty);
    ((PXSelectBase<INCartSplit>) cartSupport.CartSplits).Delete(cartSplit);
    return FlowStatus.Ok;
  }

  protected virtual FlowStatus EnsurePickerCartSplitLink(
    PPSCartSupport cartSupport,
    SOPickerListEntry entry,
    INCartSplit cartSplit,
    Decimal deltaQty)
  {
    SOPickListEntryToCartSplitLink[] array = GraphHelper.RowCast<SOPickListEntryToCartSplitLink>((IEnumerable) PXSelectBase<SOPickListEntryToCartSplitLink, PXViewOf<SOPickListEntryToCartSplitLink>.BasedOn<SelectFromBase<SOPickListEntryToCartSplitLink, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<SOPickListEntryToCartSplitLink.siteID>.IsRelatedTo<INCartSplit.siteID>, Field<SOPickListEntryToCartSplitLink.cartID>.IsRelatedTo<INCartSplit.cartID>, Field<SOPickListEntryToCartSplitLink.cartSplitLineNbr>.IsRelatedTo<INCartSplit.splitLineNbr>>.WithTablesOf<INCartSplit, SOPickListEntryToCartSplitLink>, INCartSplit, SOPickListEntryToCartSplitLink>.SameAsCurrent.Or<KeysRelation<CompositeKey<Field<SOPickListEntryToCartSplitLink.worksheetNbr>.IsRelatedTo<SOPickerListEntry.worksheetNbr>, Field<SOPickListEntryToCartSplitLink.pickerNbr>.IsRelatedTo<SOPickerListEntry.pickerNbr>, Field<SOPickListEntryToCartSplitLink.entryNbr>.IsRelatedTo<SOPickerListEntry.entryNbr>>.WithTablesOf<SOPickerListEntry, SOPickListEntryToCartSplitLink>, SOPickerListEntry, SOPickListEntryToCartSplitLink>.SameAsCurrent>>>.Config>.SelectMultiBound(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), new object[2]
    {
      (object) cartSplit,
      (object) entry
    }, Array.Empty<object>())).ToArray<SOPickListEntryToCartSplitLink>();
    SOPickListEntryToCartSplitLink entryToCartSplitLink1 = ((IEnumerable<SOPickListEntryToCartSplitLink>) array).FirstOrDefault<SOPickListEntryToCartSplitLink>((Func<SOPickListEntryToCartSplitLink, bool>) (link => KeysRelation<CompositeKey<Field<SOPickListEntryToCartSplitLink.siteID>.IsRelatedTo<INCartSplit.siteID>, Field<SOPickListEntryToCartSplitLink.cartID>.IsRelatedTo<INCartSplit.cartID>, Field<SOPickListEntryToCartSplitLink.cartSplitLineNbr>.IsRelatedTo<INCartSplit.splitLineNbr>>.WithTablesOf<INCartSplit, SOPickListEntryToCartSplitLink>, INCartSplit, SOPickListEntryToCartSplitLink>.Match(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), cartSplit, link) && KeysRelation<CompositeKey<Field<SOPickListEntryToCartSplitLink.worksheetNbr>.IsRelatedTo<SOPickerListEntry.worksheetNbr>, Field<SOPickListEntryToCartSplitLink.pickerNbr>.IsRelatedTo<SOPickerListEntry.pickerNbr>, Field<SOPickListEntryToCartSplitLink.entryNbr>.IsRelatedTo<SOPickerListEntry.entryNbr>>.WithTablesOf<SOPickerListEntry, SOPickListEntryToCartSplitLink>, SOPickerListEntry, SOPickListEntryToCartSplitLink>.Match(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), entry, link)));
    Decimal num1 = ((IEnumerable<SOPickListEntryToCartSplitLink>) array).Where<SOPickListEntryToCartSplitLink>((Func<SOPickListEntryToCartSplitLink, bool>) (link => KeysRelation<CompositeKey<Field<SOPickListEntryToCartSplitLink.siteID>.IsRelatedTo<INCartSplit.siteID>, Field<SOPickListEntryToCartSplitLink.cartID>.IsRelatedTo<INCartSplit.cartID>, Field<SOPickListEntryToCartSplitLink.cartSplitLineNbr>.IsRelatedTo<INCartSplit.splitLineNbr>>.WithTablesOf<INCartSplit, SOPickListEntryToCartSplitLink>, INCartSplit, SOPickListEntryToCartSplitLink>.Match(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), cartSplit, link))).Sum<SOPickListEntryToCartSplitLink>((Func<SOPickListEntryToCartSplitLink, Decimal>) (_ => _.Qty.GetValueOrDefault())) + deltaQty;
    Decimal? nullable = cartSplit.Qty;
    Decimal valueOrDefault = nullable.GetValueOrDefault();
    if (num1 > valueOrDefault & nullable.HasValue)
      return FlowStatus.Fail("Link quantity cannot be greater than the quantity of a cart line split.", Array.Empty<object>());
    int num2;
    if (entryToCartSplitLink1 != null)
    {
      Decimal? qty = entryToCartSplitLink1.Qty;
      Decimal num3 = deltaQty;
      nullable = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() + num3) : new Decimal?();
      Decimal num4 = 0M;
      num2 = nullable.GetValueOrDefault() < num4 & nullable.HasValue ? 1 : 0;
    }
    else
      num2 = deltaQty < 0M ? 1 : 0;
    if (num2 != 0)
      return FlowStatus.Fail("Link quantity cannot be negative.", Array.Empty<object>());
    SOPickListEntryToCartSplitLink entryToCartSplitLink2;
    if (entryToCartSplitLink1 == null)
    {
      entryToCartSplitLink2 = ((PXSelectBase<SOPickListEntryToCartSplitLink>) this.PickerCartSplitLinks).Insert(new SOPickListEntryToCartSplitLink()
      {
        WorksheetNbr = entry.WorksheetNbr,
        PickerNbr = entry.PickerNbr,
        EntryNbr = entry.EntryNbr,
        SiteID = cartSplit.SiteID,
        CartID = cartSplit.CartID,
        CartSplitLineNbr = cartSplit.SplitLineNbr,
        Qty = new Decimal?(deltaQty)
      });
    }
    else
    {
      SOPickListEntryToCartSplitLink entryToCartSplitLink3 = entryToCartSplitLink1;
      nullable = entryToCartSplitLink3.Qty;
      Decimal num5 = deltaQty;
      entryToCartSplitLink3.Qty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num5) : new Decimal?();
      entryToCartSplitLink2 = ((PXSelectBase<SOPickListEntryToCartSplitLink>) this.PickerCartSplitLinks).Update(entryToCartSplitLink1);
    }
    nullable = entryToCartSplitLink2.Qty;
    Decimal num6 = 0M;
    if (nullable.GetValueOrDefault() == num6 & nullable.HasValue)
      ((PXSelectBase<SOPickListEntryToCartSplitLink>) this.PickerCartSplitLinks).Delete(entryToCartSplitLink2);
    return FlowStatus.Ok;
  }

  public virtual void SetPickList(PXResult<SOPickingWorksheet, SOPicker> pickList)
  {
    SOPickingWorksheet pickingWorksheet = PXResult<SOPickingWorksheet, SOPicker>.op_Implicit(pickList);
    SOPicker soPicker = PXResult<SOPickingWorksheet, SOPicker>.op_Implicit(pickList);
    this.WorksheetNbr = pickingWorksheet?.WorksheetNbr;
    ((PXSelectBase<SOPickingWorksheet>) this.Worksheet).Current = pickingWorksheet;
    this.PickerNbr = (int?) soPicker?.PickerNbr;
    ((PXSelectBase<SOPicker>) this.Picker).Current = soPicker;
    ((PXSelectBase<SOPickingJob>) this.PickingJob).Current = PXResultset<SOPickingJob>.op_Implicit(((PXSelectBase<SOPickingJob>) this.PickingJob).Select(Array.Empty<object>()));
    this.Basis.SiteID = (int?) pickingWorksheet?.SiteID;
    this.Basis.TranDate = (DateTime?) pickingWorksheet?.PickDate;
    this.Basis.TranType = pickingWorksheet == null ? (string) null : "III";
    this.Basis.NoteID = (Guid?) pickingWorksheet?.NoteID;
  }

  public virtual async System.Threading.Tasks.Task TryConfirmShipmentRightAfterPickList(
    string shipmentNbr,
    SOPackageDetailEx autoPackageToConfirm,
    CancellationToken cancellationToken)
  {
    WorksheetPicking worksheetPicking = this;
    try
    {
      await ((PXGraph) PXGraph.CreateInstance<SOShipmentEntry>()).GetExtension<PickPackShip.ConfirmShipmentCommand.PickPackShipShipmentConfirmation>().ApplyPickedQtyAndConfirmShipment(shipmentNbr, false, ((PXSelectBase<SOPickPackShipSetup>) worksheetPicking.Basis.Setup).Current, PXSetupBase<PickPackShip.UserSetup, PickPackShip.Host, ScanHeader, SOPickPackShipUserSetup, Where<SOPickPackShipUserSetup.userID, Equal<Current<AccessInfo.userID>>>>.For(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) worksheetPicking.Basis)), autoPackageToConfirm, cancellationToken);
    }
    catch (Exception ex) when (!(ex is PXRedirectToUrlException))
    {
      PXTrace.WriteError(ex);
      throw new PXOperationCompletedWithWarningException(ex, "The pick list has been confirmed but an error has occurred on the {0} shipment confirmation. Contact your manager.", new object[1]
      {
        (object) shipmentNbr
      });
    }
  }

  public virtual void InjectLocationPresenceValidation(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState locationState)
  {
    Validation error;
    ((MethodInterceptor<EntityState<PickPackShip, INLocation>, PickPackShip>.OfFunc<INLocation, Validation>.AsAppendable) ((EntityState<PickPackShip, INLocation>) locationState).Intercept.Validate).ByAppend((Func<Validation, INLocation, Validation>) ((basis, location) => !basis.Get<WorksheetPicking>().IsLocationMissing(location, out error) ? Validation.Ok : error), new RelativeInject?());
  }

  public virtual void InjectItemPresenceValidation(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState inventoryState)
  {
    Validation error;
    ((MethodInterceptor<EntityState<PickPackShip, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, PickPackShip>.OfFunc<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>, Validation>.AsAppendable) ((EntityState<PickPackShip, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>) inventoryState).Intercept.Validate).ByAppend((Func<Validation, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>, Validation>) ((basis, item) => !basis.Get<WorksheetPicking>().IsItemMissing(item, out error) ? Validation.Ok : error), new RelativeInject?());
  }

  public virtual void InjectLotSerialPresenceValidation(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState lotSerialState)
  {
    Validation error;
    ((MethodInterceptor<EntityState<PickPackShip, string>, PickPackShip>.OfFunc<string, Validation>.AsAppendable) ((EntityState<PickPackShip, string>) lotSerialState).Intercept.Validate).ByAppend((Func<Validation, string, Validation>) ((basis, lotSerialNbr) => !basis.Get<WorksheetPicking>().IsLotSerialMissing(lotSerialNbr, out error) ? Validation.Ok : error), new RelativeInject?());
  }

  public virtual void InjectExpireDateForWSPickDeactivationOnAlreadyEnteredLot(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState expireDateState)
  {
    ((MethodInterceptor<EntityState<PickPackShip, DateTime?>, PickPackShip>.OfPredicate) ((EntityState<PickPackShip, DateTime?>) expireDateState).Intercept.IsStateActive).ByConjoin((Func<PickPackShip, bool>) (basis => basis.SelectedLotSerialClass?.LotSerAssign == "U" && ((IEnumerable<SOPickerListEntry>) ((PXSelectBase<SOPickerListEntry>) basis.Get<WorksheetPicking>().PickListOfPicker).SelectMain(Array.Empty<object>())).Any<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (t =>
    {
      if (t.IsUnassigned.GetValueOrDefault())
        return true;
      if (!string.Equals(t.LotSerialNbr, basis.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
        return false;
      Decimal? pickedQty = t.PickedQty;
      Decimal num = 0M;
      return pickedQty.GetValueOrDefault() == num & pickedQty.HasValue;
    }))), false, new RelativeInject?());
  }

  public virtual void InjectShipmentAbsenceHandlingByWorksheet(
    PickPackShip.PickMode.ShipmentState pickShipment)
  {
    ((MethodInterceptor<EntityState<PickPackShip, PX.Objects.SO.SOShipment>, PickPackShip>.OfFunc<string, AbsenceHandling.Of<PX.Objects.SO.SOShipment>>.AsAppendable) ((EntityState<PickPackShip, PX.Objects.SO.SOShipment>) pickShipment).Intercept.HandleAbsence).ByAppend((Func<AbsenceHandling.Of<PX.Objects.SO.SOShipment>, string, AbsenceHandling.Of<PX.Objects.SO.SOShipment>>) ((basis, barcode) =>
    {
      if (barcode.Contains("/"))
      {
        string str;
        string s;
        ArrayDeconstruct.Deconstruct<string>(barcode.Split('/'), ref str, ref s);
        string groupNbr = str;
        if (int.TryParse(s, out int _))
        {
          SOPickingWorksheet sheet = SOPickingWorksheet.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) basis), groupNbr);
          if (sheet != null)
          {
            IScanMode modeForWorksheet = (IScanMode) basis.Get<WorksheetPicking>().FindModeForWorksheet(sheet);
            if (modeForWorksheet != null && basis.FindMode(modeForWorksheet.Code).TryProcessBy<WorksheetPicking.PickListState>(barcode, (StateSubstitutionRule) 0))
            {
              basis.SetScanMode(modeForWorksheet.Code);
              ((ScanState<PickPackShip>) basis.FindState<WorksheetPicking.PickListState>(false)).Process(barcode);
              return AbsenceHandling.Of<PX.Objects.SO.SOShipment>.op_Implicit(AbsenceHandling.Done);
            }
          }
        }
      }
      return AbsenceHandling.Of<PX.Objects.SO.SOShipment>.op_Implicit(AbsenceHandling.Skipped);
    }), new RelativeInject?());
  }

  public virtual void InjectShipmentValidationForSeparatePicking(
    PickPackShip.PickMode.ShipmentState pickShipment)
  {
    ((MethodInterceptor<EntityState<PickPackShip, PX.Objects.SO.SOShipment>, PickPackShip>.OfFunc<PX.Objects.SO.SOShipment, Validation>.AsAppendable) ((EntityState<PickPackShip, PX.Objects.SO.SOShipment>) pickShipment).Intercept.Validate).ByAppend((Func<Validation, PX.Objects.SO.SOShipment, Validation>) ((basis, shipment) =>
    {
      if (shipment.CurrentWorksheetNbr == null)
        return Validation.Ok;
      return Validation.Fail("The {0} shipment cannot be picked individually because the shipment is assigned to the {1} picking worksheet.", new object[2]
      {
        (object) shipment.ShipmentNbr,
        (object) shipment.CurrentWorksheetNbr
      });
    }), new RelativeInject?());
  }

  public virtual void InjectValidationPickFirst(PickPackShip.ShipmentState refNbrState)
  {
    ((MethodInterceptor<EntityState<PickPackShip, PX.Objects.SO.SOShipment>, PickPackShip>.OfFunc<PX.Objects.SO.SOShipment, Validation>.AsAppendable) ((EntityState<PickPackShip, PX.Objects.SO.SOShipment>) refNbrState).Intercept.Validate).ByAppend((Func<Validation, PX.Objects.SO.SOShipment, Validation>) ((basis, shipment) =>
    {
      if (shipment.CurrentWorksheetNbr != null)
      {
        bool? picked = shipment.Picked;
        bool flag = false;
        if (picked.GetValueOrDefault() == flag & picked.HasValue)
          return Validation.Fail("The {0} shipment cannot be packed because the items have not been picked.", new object[1]
          {
            (object) shipment.ShipmentNbr
          });
      }
      return Validation.Ok;
    }), new RelativeInject?());
  }

  public virtual void InjectPackAllToBoxCommand(PickPackShip.PackMode pack)
  {
    ((MethodInterceptor<ScanMode<PickPackShip>, PickPackShip>.OfFunc<IEnumerable<ScanCommand<PickPackShip>>>.AsAppendable) ((ScanMode<PickPackShip>) pack).Intercept.CreateCommands).ByAppend((Func<IEnumerable<ScanCommand<PickPackShip>>>) (() => (IEnumerable<ScanCommand<PickPackShip>>) new WorksheetPicking.PackAllIntoBoxCommand[1]
    {
      new WorksheetPicking.PackAllIntoBoxCommand()
    }), new RelativeInject?());
  }

  /// Overrides <see cref="P:PX.Objects.SO.WMS.PickPackShip.DocumentIsEditable" />
  [PXOverride]
  public bool get_DocumentIsEditable(Func<bool> base_DocumentIsEditable)
  {
    if (!base_DocumentIsEditable())
      return false;
    int num1 = this.IsWorksheetMode(this.Basis.CurrentMode?.Code) ? 1 : 0;
    SOPickingWorksheet pickWorksheet = this.PickWorksheet;
    int num2 = pickWorksheet != null ? (EnumerableExtensions.IsIn<string>(pickWorksheet.Status, "N", "I") ? 1 : 0) : 0;
    return (num1 != 0).Implies(num2 != 0);
  }

  /// Overrides <see cref="P:PX.Objects.SO.WMS.PickPackShip.DocumentIsConfirmed" />
  [PXOverride]
  public bool get_DocumentIsConfirmed(Func<bool> base_DocumentIsConfirmed)
  {
    if (!this.IsWorksheetMode(this.Basis.CurrentMode?.Code))
      return base_DocumentIsConfirmed();
    SOPicker pickList = this.PickList;
    return pickList != null && pickList.Confirmed.GetValueOrDefault();
  }

  /// Overrides <see cref="P:PX.Objects.IN.WMS.WarehouseManagementSystem`2.DocumentLoaded" />
  [PXOverride]
  public bool get_DocumentLoaded(Func<bool> base_DocumentLoaded)
  {
    return !this.IsWorksheetMode(this.Basis.CurrentMode?.Code) ? base_DocumentLoaded() : this.WorksheetNbr != null;
  }

  [PXOverride]
  public ScanState<PickPackShip> DecorateScanState(
    ScanState<PickPackShip> original,
    Func<ScanState<PickPackShip>, ScanState<PickPackShip>> base_DecorateScanState)
  {
    ScanState<PickPackShip> scanState = base_DecorateScanState(original);
    if (this.IsWorksheetMode(((ScanComponent<PickPackShip>) scanState).ModeCode))
    {
      switch (scanState)
      {
        case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState locationState:
          this.InjectLocationPresenceValidation(locationState);
          break;
        case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState inventoryState:
          this.InjectItemPresenceValidation(inventoryState);
          break;
        case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState lotSerialState:
          this.Basis.InjectLotSerialDeactivationOnDefaultLotSerialOption(lotSerialState, true);
          this.InjectLotSerialPresenceValidation(lotSerialState);
          break;
        case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState expireDateState:
          this.InjectExpireDateForWSPickDeactivationOnAlreadyEnteredLot(expireDateState);
          break;
      }
    }
    else
    {
      switch (scanState)
      {
        case PickPackShip.PickMode.ShipmentState pickShipment:
          this.InjectShipmentAbsenceHandlingByWorksheet(pickShipment);
          this.InjectShipmentValidationForSeparatePicking(pickShipment);
          break;
        case PickPackShip.PackMode.ShipmentState refNbrState1:
          this.InjectValidationPickFirst((PickPackShip.ShipmentState) refNbrState1);
          break;
        case PickPackShip.ShipMode.ShipmentState refNbrState2:
          this.InjectValidationPickFirst((PickPackShip.ShipmentState) refNbrState2);
          break;
      }
    }
    return scanState;
  }

  [PXOverride]
  public ScanMode<PickPackShip> DecorateScanMode(
    ScanMode<PickPackShip> original,
    Func<ScanMode<PickPackShip>, ScanMode<PickPackShip>> base_DecorateScanMode)
  {
    ScanMode<PickPackShip> scanMode = base_DecorateScanMode(original);
    if (!(scanMode is PickPackShip.PackMode pack))
      return scanMode;
    this.InjectPackAllToBoxCommand(pack);
    return scanMode;
  }

  public abstract class PickListState : 
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.RefNbrState<PXResult<SOPickingWorksheet, SOPicker>>
  {
    private int _pickerNbr;

    public WorksheetPicking WSBasis
    {
      get => ((ScanComponent<PickPackShip>) this).Basis.Get<WorksheetPicking>();
    }

    protected abstract string WorksheetType { get; }

    protected virtual string StatePrompt => "Scan the picking worksheet number.";

    protected override bool IsStateSkippable()
    {
      if (base.IsStateSkippable())
        return true;
      return this.WSBasis.WorksheetNbr != null && !((ScanComponent<PickPackShip>) this).Basis.Header.ProcessingSucceeded.GetValueOrDefault();
    }

    protected virtual PXResult<SOPickingWorksheet, SOPicker> GetByBarcode(string barcode)
    {
      if (!barcode.Contains("/"))
        return (PXResult<SOPickingWorksheet, SOPicker>) null;
      string str1;
      string s;
      ArrayDeconstruct.Deconstruct<string>(barcode.Split('/'), ref str1, ref s);
      string str2 = str1;
      this._pickerNbr = int.Parse(s);
      PXResult<SOPickingWorksheet, PX.Objects.IN.INSite, SOPicker> pxResult = (PXResult<SOPickingWorksheet, PX.Objects.IN.INSite, SOPicker>) PXResultset<SOPickingWorksheet>.op_Implicit(PXSelectBase<SOPickingWorksheet, PXViewOf<SOPickingWorksheet>.BasedOn<SelectFromBase<SOPickingWorksheet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<SOPickingWorksheet.FK.Site>>, FbqlJoins.Left<SOPicker>.On<KeysRelation<Field<SOPicker.worksheetNbr>.IsRelatedTo<SOPickingWorksheet.worksheetNbr>.AsSimpleKey.WithTablesOf<SOPickingWorksheet, SOPicker>, SOPickingWorksheet, SOPicker>.And<BqlOperand<SOPicker.pickerNbr, IBqlInt>.IsEqual<P.AsInt>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingWorksheet.worksheetNbr, Equal<P.AsString>>>>, And<BqlOperand<SOPickingWorksheet.worksheetType, IBqlString>.IsEqual<P.AsString>>>>.And<Match<PX.Objects.IN.INSite, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>.Config>.Select(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) ((ScanComponent<PickPackShip>) this).Basis), new object[3]
      {
        (object) this._pickerNbr,
        (object) str2,
        (object) this.WorksheetType
      }));
      return pxResult != null ? new PXResult<SOPickingWorksheet, SOPicker>(PXResult<SOPickingWorksheet, PX.Objects.IN.INSite, SOPicker>.op_Implicit(pxResult), PXResult<SOPickingWorksheet, PX.Objects.IN.INSite, SOPicker>.op_Implicit(pxResult)) : (PXResult<SOPickingWorksheet, SOPicker>) null;
    }

    protected virtual AbsenceHandling.Of<PXResult<SOPickingWorksheet, SOPicker>> HandleAbsence(
      string barcode)
    {
      PickPackShip.PickMode mode = ((ScanComponent<PickPackShip>) this).Basis.FindMode<PickPackShip.PickMode>();
      if (mode == null || !((ScanMode<PickPackShip>) mode).IsActive || !((ScanMode<PickPackShip>) mode).TryProcessBy<PickPackShip.PickMode.ShipmentState>(barcode, (StateSubstitutionRule) 0))
        return ((EntityState<PickPackShip, PXResult<SOPickingWorksheet, SOPicker>>) this).HandleAbsence(barcode);
      ((ScanComponent<PickPackShip>) this).Basis.SetScanMode<PickPackShip.PickMode>();
      ((ScanState<PickPackShip>) ((ScanComponent<PickPackShip>) this).Basis.FindState<PickPackShip.PickMode.ShipmentState>(false)).Process(barcode);
      return AbsenceHandling.Of<PXResult<SOPickingWorksheet, SOPicker>>.op_Implicit(AbsenceHandling.Done);
    }

    protected virtual Validation Validate(PXResult<SOPickingWorksheet, SOPicker> pickList)
    {
      SOPickingWorksheet pickingWorksheet1;
      SOPicker soPicker1;
      pickList.Deconstruct(ref pickingWorksheet1, ref soPicker1);
      SOPickingWorksheet pickingWorksheet2 = pickingWorksheet1;
      SOPicker soPicker2 = soPicker1;
      if (EnumerableExtensions.IsNotIn<string>(pickingWorksheet2.Status, "I", "N"))
        return Validation.Fail("The {0} picking worksheet cannot be processed because it has the {1} status.", new object[2]
        {
          (object) pickingWorksheet2.WorksheetNbr,
          (object) ((ScanComponent<PickPackShip>) this).Basis.SightOf<SOPickingWorksheet.status>((IBqlTable) pickingWorksheet2)
        });
      PPSCartSupport ppsCartSupport = ((ScanComponent<PickPackShip>) this).Basis.Get<PPSCartSupport>();
      int? nullable1;
      if (ppsCartSupport != null)
      {
        int? nullable2 = ppsCartSupport.CartID;
        if (nullable2.HasValue)
        {
          nullable2 = pickingWorksheet2.SiteID;
          nullable1 = ((ScanComponent<PickPackShip>) this).Basis.SiteID;
          if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
            return Validation.Fail("The warehouse specified in the {0} picking worksheet differs from the warehouse assigned to the selected cart.", new object[1]
            {
              (object) pickingWorksheet2.WorksheetNbr
            });
        }
      }
      int num;
      if (soPicker2 == null)
      {
        num = 1;
      }
      else
      {
        nullable1 = soPicker2.PickerNbr;
        num = !nullable1.HasValue ? 1 : 0;
      }
      if (num != 0)
        return Validation.Fail("Picker number {0} has not been found in the {1} picking worksheet.", new object[2]
        {
          (object) this._pickerNbr,
          (object) pickingWorksheet2.WorksheetNbr
        });
      if (!EnumerableExtensions.IsNotIn<Guid?>(soPicker2.UserID, new Guid?(), new Guid?(((PXGraph) ((ScanComponent<PickPackShip>) this).Basis.Graph).Accessinfo.UserID)))
        return ((EntityState<PickPackShip, PXResult<SOPickingWorksheet, SOPicker>>) this).Validate(pickList);
      return Validation.Fail("Picker number {0} has already been assigned to another user in the {1} picking worksheet.", new object[2]
      {
        (object) soPicker2.PickerNbr,
        (object) pickingWorksheet2.WorksheetNbr
      });
    }

    protected virtual void Apply(PXResult<SOPickingWorksheet, SOPicker> pickList)
    {
      ((ScanComponent<PickPackShip>) this).Basis.RefNbr = (string) null;
      ((PXSelectBase<PX.Objects.SO.SOShipment>) ((ScanComponent<PickPackShip>) this).Basis.Graph.Document).Current = (PX.Objects.SO.SOShipment) null;
      this.WSBasis.SetPickList(pickList);
    }

    protected virtual void ClearState()
    {
      this.WSBasis.SetPickList((PXResult<SOPickingWorksheet, SOPicker>) null);
    }

    protected virtual void ReportMissing(string barcode)
    {
      ((ScanComponent<PickPackShip>) this).Basis.ReportError("{0} picking worksheet not found.", new object[1]
      {
        (object) barcode
      });
    }

    protected virtual void ReportSuccess(PXResult<SOPickingWorksheet, SOPicker> pickList)
    {
      ((ScanComponent<PickPackShip>) this).Basis.ReportInfo("{0} picking worksheet loaded and ready to be processed.", new object[1]
      {
        (object) ((PXResult) pickList).GetItem<SOPicker>().PickListNbr
      });
    }

    protected virtual void SetNextState()
    {
      bool? remove = ((ScanComponent<PickPackShip>) this).Basis.Remove;
      bool flag = false;
      if (remove.GetValueOrDefault() == flag & remove.HasValue && !this.WSBasis.CanWSPick)
      {
        ((ScanComponent<PickPackShip>) this).Basis.ReportInfo("{0} {1}", new object[2]
        {
          (object) ((PXSelectBase<ScanInfo>) ((ScanComponent<PickPackShip>) this).Basis.Info).Current.Message,
          (object) ((ScanComponent<PickPackShip>) this).Basis.Localize("{0} pick list picked.", new object[1]
          {
            (object) (((PXSelectBase<SOPickingJob>) this.WSBasis.PickingJob).Current?.PickListNbr ?? ((PXSelectBase<SOPicker>) this.WSBasis.Picker).Current.PickListNbr)
          })
        });
        ((ScanComponent<PickPackShip>) this).Basis.SetScanState("NONE", (string) null, Array.Empty<object>());
      }
      else
        ((EntityState<PickPackShip, PXResult<SOPickingWorksheet, SOPicker>>) this).SetNextState();
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string Prompt = "Scan the picking worksheet number.";
      public const string Ready = "{0} picking worksheet loaded and ready to be processed.";
      public const string Missing = "{0} picking worksheet not found.";
      public const string InvalidStatus = "The {0} picking worksheet cannot be processed because it has the {1} status.";
      public const string InvalidSite = "The warehouse specified in the {0} picking worksheet differs from the warehouse assigned to the selected cart.";
      public const string PickerPositionMissing = "Picker number {0} has not been found in the {1} picking worksheet.";
      public const string PickerPositionOccupied = "Picker number {0} has already been assigned to another user in the {1} picking worksheet.";
    }
  }

  public sealed class ConfirmPickListCommand : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand
  {
    public virtual string Code => "CONFIRM*PICK";

    public virtual string ButtonName => "scanConfirmPickList";

    public virtual string DisplayName => "Confirm Pick List";

    protected virtual bool IsEnabled
    {
      get => ((ScanComponent<PickPackShip>) this).Basis.DocumentIsEditable;
    }

    protected virtual bool Process()
    {
      ((ScanComponent<PickPackShip>) this).Basis.Get<WorksheetPicking.ConfirmPickListCommand.Logic>().ConfirmPickList();
      return true;
    }

    public class Logic : WorksheetPicking.ScanExtension
    {
      public static bool IsActive() => WorksheetPicking.ScanExtension.IsActiveBase();

      public virtual void ConfirmPickList()
      {
        if (((IEnumerable<SOPickerListEntry>) ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).SelectMain(Array.Empty<object>())).All<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (s =>
        {
          Decimal? pickedQty = s.PickedQty;
          Decimal num = 0M;
          return pickedQty.GetValueOrDefault() == num & pickedQty.HasValue;
        })))
          this.Basis.ReportError("The pick list cannot be confirmed because no items have been picked.", Array.Empty<object>());
        else if (((PXSelectBase<ScanInfo>) this.Basis.Info).Current.MessageType != "WRN" && ((IEnumerable<SOPickerListEntry>) ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).SelectMain(Array.Empty<object>())).Any<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (s =>
        {
          Decimal? pickedQty = s.PickedQty;
          Decimal? qty = s.Qty;
          return pickedQty.GetValueOrDefault() < qty.GetValueOrDefault() & pickedQty.HasValue & qty.HasValue;
        })))
          this.ReportConfirmationInPart();
        else
          this.ConfirmPickList(new int?());
      }

      public virtual void ReportConfirmationInPart()
      {
        if (this.Basis.CannotConfirmPartialShipments)
          this.Basis.ReportError("The pick list cannot be confirmed because it is not complete.", Array.Empty<object>());
        else
          this.Basis.ReportWarning("The pick list is incomplete and should not be confirmed. Do you want to confirm the pick list?", Array.Empty<object>());
      }

      public virtual void ConfirmPickList(int? sortingLocationID)
      {
        SOPickingWorksheet worksheet = ((PXSelectBase<SOPickingWorksheet>) this.WSBasis.Worksheet).Current;
        SOPicker current1 = ((PXSelectBase<SOPicker>) this.WSBasis.Picker).Current;
        SOPickingJob current2 = ((PXSelectBase<SOPickingJob>) this.WSBasis.PickingJob).Current;
        this.Basis.Reset(false);
        this.Basis.Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>(true);
        this.Basis.AwaitFor<SOPicker>((Func<PickPackShip, SOPicker, CancellationToken, System.Threading.Tasks.Task>) ((basis, doc, ct) => WorksheetPicking.ConfirmPickListCommand.Logic.ConfirmPickListHandler(worksheet, doc, sortingLocationID, ct))).WithDescription("Confimation of {0} pick list in progress.", new object[1]
        {
          (object) (current2?.PickListNbr ?? current1.PickListNbr)
        }).ActualizeDataBy((Func<PickPackShip, SOPicker, SOPicker>) ((basis, doc) => (SOPicker) PrimaryKeyOf<SOPicker>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<SOPicker.worksheetNbr, SOPicker.pickerNbr>>.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) basis), (TypeArrayOf<IBqlField>.IFilledWith<SOPicker.worksheetNbr, SOPicker.pickerNbr>) doc, (PKFindOptions) 0))).OnSuccess(new Action<ScanLongRunAwaiter<PickPackShip, SOPicker>.ISuccessProcessor>(this.ConfigureOnSuccessAction)).OnFail((Action<ScanLongRunAwaiter<PickPackShip, SOPicker>.IResultProcessor>) (x => x.Say("Pick list not confirmed.", Array.Empty<object>()))).BeginAwait(current1);
      }

      public virtual void ConfigureOnSuccessAction(
        ScanLongRunAwaiter<PickPackShip, SOPicker>.IResultProcessor onSuccess)
      {
        onSuccess.Say("Pick list successfully confirmed.", Array.Empty<object>()).ChangeStateTo<WorksheetPicking.PickListState>();
      }

      protected static async System.Threading.Tasks.Task ConfirmPickListHandler(
        SOPickingWorksheet worksheet,
        SOPicker pickList,
        int? sortingLocationID,
        CancellationToken cancellationToken)
      {
        await ((PXGraph) PXGraph.CreateInstance<PickPackShip.Host>()).GetExtension<WorksheetPicking.ConfirmPickListCommand.Logic>().ConfirmPickList(worksheet, pickList, sortingLocationID, cancellationToken);
      }

      protected virtual async System.Threading.Tasks.Task ConfirmPickList(
        SOPickingWorksheet worksheet,
        SOPicker pickList,
        int? sortingLocationID,
        CancellationToken cancellationToken)
      {
        using (PXTransactionScope ts = new PXTransactionScope())
        {
          await BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.WithSuppressedRedirects((Func<System.Threading.Tasks.Task>) (async () =>
          {
            SOPickingWorksheetPickListConfirmation listConfirmation = PXGraph.CreateInstance<SOPickingWorksheetReview>().PickListConfirmation;
            listConfirmation.ConfirmPickList(pickList, sortingLocationID);
            await listConfirmation.FulfillShipmentsAndConfirmWorksheet(worksheet, cancellationToken);
          }));
          ts.Complete();
        }
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string DisplayName = "Confirm Pick List";
      public const string InProcess = "Confimation of {0} pick list in progress.";
      public const string Success = "Pick list successfully confirmed.";
      public const string Fail = "Pick list not confirmed.";
      public const string CannotBeConfirmed = "The pick list cannot be confirmed because no items have been picked.";
      public const string CannotBeConfirmedInPart = "The pick list cannot be confirmed because it is not complete.";
      public const string ShouldNotBeConfirmedInPart = "The pick list is incomplete and should not be confirmed. Do you want to confirm the pick list?";
    }
  }

  public sealed class PackAllIntoBoxCommand : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand
  {
    private const string ActionName = "scanPackAllIntoBox";

    public virtual string Code => "PACK*ALL*INTO*BOX";

    public virtual string ButtonName => "scanPackAllIntoBox";

    public virtual string DisplayName => "Pack All Into One Box";

    protected virtual bool IsEnabled
    {
      get
      {
        return ((ScanComponent<PickPackShip>) this).Basis.DocumentIsEditable && ((ScanComponent<PickPackShip>) this).Basis.Get<PickPackShip.PackMode.Logic>().With<PickPackShip.PackMode.Logic, bool>((Func<PickPackShip.PackMode.Logic, bool>) (pack => pack.CanPack && pack.SelectedPackage != null));
      }
    }

    protected virtual bool Process()
    {
      return this.Get<WorksheetPicking.PackAllIntoBoxCommand.Logic>().PutAllIntoBox();
    }

    public class Logic : WorksheetPicking.ScanExtension
    {
      public static bool IsActive() => WorksheetPicking.ScanExtension.IsActiveBase();

      public virtual bool PutAllIntoBox()
      {
        PickPackShip.PackMode.Logic logic = this.Basis.Get<PickPackShip.PackMode.Logic>();
        PickPackShip.PackMode.ConfirmState.Logic packConfirm = this.Basis.Get<PickPackShip.PackMode.ConfirmState.Logic>();
        SOPackageDetailEx selectedPackage = logic.SelectedPackage;
        IEnumerable<SOShipLineSplit> soShipLineSplits = ((IEnumerable<SOShipLineSplit>) ((PXSelectBase<SOShipLineSplit>) logic.PickedForPack).SelectMain(Array.Empty<object>())).Where<SOShipLineSplit>((Func<SOShipLineSplit, bool>) (r =>
        {
          Decimal? nullable = packConfirm.TargetQty(r);
          Decimal? packedQty = r.PackedQty;
          return nullable.GetValueOrDefault() > packedQty.GetValueOrDefault() & nullable.HasValue & packedQty.HasValue;
        }));
        bool flag = false;
        foreach (SOShipLineSplit split in soShipLineSplits)
        {
          Decimal? nullable = packConfirm.TargetQty(split);
          Decimal num1 = nullable.Value;
          nullable = split.PackedQty;
          Decimal num2 = nullable.Value;
          Decimal qty = num1 - num2;
          flag |= packConfirm.PackSplit(split, selectedPackage, qty);
        }
        if (!flag)
          return false;
        packConfirm.EnsureShipmentUserLinkForPack();
        logic.PackageLineNbrUI = logic.PackageLineNbr;
        this.Basis.SaveChanges();
        this.Basis.SetDefaultState((string) null, Array.Empty<object>());
        return true;
      }

      protected virtual void _(PX.Data.Events.RowSelected<ScanHeader> args)
      {
        ((PXGraph) this.Basis.Graph).Actions["scanPackAllIntoBox"]?.SetVisible(false);
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string DisplayName = "Pack All Into One Box";
    }
  }

  [PXLocalizable]
  public abstract class Msg : PickPackShip.Msg
  {
    public const string Completed = "{0} pick list picked.";
    public const string ShipmentCannotBePickedSeparately = "The {0} shipment cannot be picked individually because the shipment is assigned to the {1} picking worksheet.";
    public const string InventoryMissingInPickList = "{0} item not listed in pick list.";
    public const string LocationMissingInPickList = "{0} location not listed in pick list.";
    public const string LotSerialMissingInPickList = "{0} lot or serial number not listed in pick list.";
    public const string NothingToPick = "No items to pick.";
    public const string NothingToRemove = "No items to remove from shipment.";
    public const string Overpicking = "The picked quantity cannot be greater than the quantity in the pick list line.";
    public const string Underpicking = "The picked quantity cannot be negative.";
    public const string MissingLotSerailOnLocation = "The {1} lot or serial number does not exist for the {0} item in the {3} location of the {2} warehouse.";
    public const string ExceededAvailability = "The picked quantity of the {0} {1} cannot be greater than the available quantity in the {3} location of {2}.";
    public const string InventoryAdded = "{0} x {1} {2} has been added to the pick list.";
    public const string InventoryRemoved = "{0} x {1} {2} has been removed from the pick list.";
    public const string PickListConfirmedButShipmentDoesNot = "The pick list has been confirmed but an error has occurred on the {0} shipment confirmation. Contact your manager.";
  }

  [PXUIField(Visible = false)]
  public class ShowPickWS : 
    PXFieldAttachedTo<ScanHeader>.By<PickPackShip.Host>.AsBool.Named<WorksheetPicking.ShowPickWS>
  {
    public static bool IsActive() => WorksheetPicking.IsActive();

    public override bool? GetValue(ScanHeader row)
    {
      return new bool?(((PXSelectBase<SOPickPackShipSetup>) this.Base.WMS.Setup).Current.ShowPickTab.GetValueOrDefault() && this.Base.WMS.Get<WorksheetPicking>().IsWorksheetMode(row.Mode));
    }
  }

  [PXUIField(DisplayName = "Matched")]
  public class FitsWS : 
    PXFieldAttachedTo<SOPickerListEntry>.By<PickPackShip.Host>.AsBool.Named<WorksheetPicking.FitsWS>
  {
    public static bool IsActive() => WorksheetPicking.IsActive();

    public override bool? GetValue(SOPickerListEntry row)
    {
      bool flag = true;
      int? nullable1;
      if (this.Base.WMS.LocationID.HasValue)
      {
        int num1 = flag ? 1 : 0;
        nullable1 = this.Base.WMS.LocationID;
        int? locationId = row.LocationID;
        int num2 = nullable1.GetValueOrDefault() == locationId.GetValueOrDefault() & nullable1.HasValue == locationId.HasValue ? 1 : 0;
        flag = (num1 & num2) != 0;
      }
      if (this.Base.WMS.InventoryID.HasValue)
      {
        int num3 = flag ? 1 : 0;
        int? nullable2 = this.Base.WMS.InventoryID;
        nullable1 = row.InventoryID;
        int num4;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          nullable1 = this.Base.WMS.SubItemID;
          nullable2 = row.SubItemID;
          num4 = nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue ? 1 : 0;
        }
        else
          num4 = 0;
        flag = (num3 & num4) != 0;
      }
      if (this.Base.WMS.LotSerialNbr != null)
      {
        int num5 = flag ? 1 : 0;
        int num6;
        if (!string.Equals(this.Base.WMS.LotSerialNbr, row.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
        {
          if (this.Base.WMS.LotSerialTrack.IsEnterable)
          {
            Decimal? pickedQty = row.PickedQty;
            Decimal num7 = 0M;
            num6 = pickedQty.GetValueOrDefault() == num7 & pickedQty.HasValue ? 1 : 0;
          }
          else
            num6 = 0;
        }
        else
          num6 = 1;
        flag = (num5 & num6) != 0;
      }
      return new bool?(flag);
    }
  }

  public abstract class ScanExtension : 
    PXGraphExtension<WorksheetPicking, PickPackShip, PickPackShip.Host>
  {
    protected static bool IsActiveBase() => WorksheetPicking.IsActive();

    public PickPackShip.Host Graph
    {
      [DebuggerStepThrough, DebuggerStepperBoundary] get
      {
        return ((PXGraphExtension<PickPackShip.Host>) this).Base;
      }
    }

    public PickPackShip Basis
    {
      [DebuggerStepThrough, DebuggerStepperBoundary] get
      {
        return ((PXGraphExtension<PickPackShip, PickPackShip.Host>) this).Base1;
      }
    }

    public WorksheetPicking WSBasis
    {
      [DebuggerStepThrough, DebuggerStepperBoundary] get => this.Base2;
    }
  }

  public abstract class ScanExtension<TTargetExtension> : 
    PXGraphExtension<TTargetExtension, WorksheetPicking, PickPackShip, PickPackShip.Host>
    where TTargetExtension : PXGraphExtension<WorksheetPicking, PickPackShip, PickPackShip.Host>
  {
    protected static bool IsActiveBase() => WorksheetPicking.IsActive();

    public PickPackShip.Host Graph
    {
      [DebuggerStepThrough, DebuggerStepperBoundary] get
      {
        return ((PXGraphExtension<PickPackShip.Host>) this).Base;
      }
    }

    public PickPackShip Basis
    {
      [DebuggerStepThrough, DebuggerStepperBoundary] get
      {
        return ((PXGraphExtension<PickPackShip, PickPackShip.Host>) this).Base1;
      }
    }

    public WorksheetPicking WSBasis
    {
      [DebuggerStepThrough, DebuggerStepperBoundary] get
      {
        return ((PXGraphExtension<WorksheetPicking, PickPackShip, PickPackShip.Host>) this).Base2;
      }
    }

    public TTargetExtension Target
    {
      [DebuggerStepThrough, DebuggerStepperBoundary] get => this.Base3;
    }
  }
}
