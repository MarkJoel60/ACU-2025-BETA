// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.WMS.PaperlessPicking
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
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.WMS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

#nullable enable
namespace PX.Objects.SO.WMS;

public class PaperlessPicking : WorksheetPicking.ScanExtension
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.wMSPaperlessPicking>();

  [PXOverride]
  public 
  #nullable disable
  IEnumerable<ScanMode<PickPackShip>> CreateScanModes(
    Func<IEnumerable<ScanMode<PickPackShip>>> base_CreateScanModes)
  {
    foreach (ScanMode<PickPackShip> scanMode in base_CreateScanModes())
      yield return scanMode;
    yield return (ScanMode<PickPackShip>) new PaperlessPicking.SinglePickMode();
  }

  public PaperlessScanHeader PPHeader
  {
    get => ScanHeaderExt.Get<PaperlessScanHeader>(this.Basis.Header) ?? new PaperlessScanHeader();
  }

  public ValueSetter<ScanHeader>.Ext<PaperlessScanHeader> PPSetter
  {
    get => this.Basis.HeaderSetter.With<PaperlessScanHeader>();
  }

  public int? LastVisitedLocationID
  {
    get => this.PPHeader.LastVisitedLocationID;
    set
    {
      ValueSetter<ScanHeader>.Ext<PaperlessScanHeader> ppSetter = this.PPSetter;
      (^ref ppSetter).Set<int?>((Expression<Func<PaperlessScanHeader, int?>>) (h => h.LastVisitedLocationID), value);
    }
  }

  public bool? PathInversedDirection
  {
    get => this.PPHeader.PathInversedDirection;
    set
    {
      ValueSetter<ScanHeader>.Ext<PaperlessScanHeader> ppSetter = this.PPSetter;
      (^ref ppSetter).Set<bool?>((Expression<Func<PaperlessScanHeader, bool?>>) (h => h.PathInversedDirection), value);
    }
  }

  public int? WantedLineNbr
  {
    get => this.PPHeader.WantedLineNbr;
    set
    {
      ValueSetter<ScanHeader>.Ext<PaperlessScanHeader> ppSetter = this.PPSetter;
      (^ref ppSetter).Set<int?>((Expression<Func<PaperlessScanHeader, int?>>) (h => h.WantedLineNbr), value);
    }
  }

  public string SingleShipmentNbr
  {
    get => this.PPHeader.SingleShipmentNbr;
    set
    {
      ValueSetter<ScanHeader>.Ext<PaperlessScanHeader> ppSetter = this.PPSetter;
      (^ref ppSetter).Set<string>((Expression<Func<PaperlessScanHeader, string>>) (h => h.SingleShipmentNbr), value);
    }
  }

  public ISet<int> IgnoredPickingJobs => (ISet<int>) this.PPHeader.IgnoredPickingJobs;

  protected virtual void _(PX.Data.Events.RowSelected<ScanHeader> e)
  {
    if (e.Row == null)
      return;
    PXCacheEx.AdjustUI(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ScanHeader>>) e).Cache, (object) null).For<PaperlessScanHeader.singleShipmentNbr>((Action<PXUIFieldAttribute>) (a => a.Visible = e.Row.Mode == "SNGL")).For<WMSScanHeader.refNbr>((Action<PXUIFieldAttribute>) (a => a.Visible &= e.Row.Mode != "SNGL"));
  }

  public virtual bool ReturnCurrentJobToQueue()
  {
    bool queue = false;
    if (((PXSelectBase<SOPickingJob>) this.WSBasis.PickingJob).Current != null)
    {
      Guid? actualAssigneeId = ((PXSelectBase<SOPickingJob>) this.WSBasis.PickingJob).Current.ActualAssigneeID;
      Guid userId = ((PXGraph) this.Graph).Accessinfo.UserID;
      if ((actualAssigneeId.HasValue ? (actualAssigneeId.GetValueOrDefault() == userId ? 1 : 0) : 0) != 0)
      {
        this.IgnoredPickingJobs.Add(((PXSelectBase<SOPickingJob>) this.WSBasis.PickingJob).Current.JobID.Value);
        ((PXSelectBase<SOPickingJob>) this.WSBasis.PickingJob).Current.ActualAssigneeID = new Guid?();
        if (((PXSelectBase<SOPickingJob>) this.WSBasis.PickingJob).Current.Status == "PNG")
          ((PXSelectBase<SOPickingJob>) this.WSBasis.PickingJob).Current.Status = "RNQ";
        ((PXSelectBase<SOPickingJob>) this.WSBasis.PickingJob).UpdateCurrent();
        queue = true;
      }
    }
    foreach (PXResult<SOPickerToShipmentLink> pxResult in ((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).Select(Array.Empty<object>()))
    {
      SOPickerToShipmentLink link = PXResult<SOPickerToShipmentLink>.op_Implicit(pxResult);
      int? toteId = link.ToteID;
      int num = 0;
      if (!(toteId.GetValueOrDefault() == num & toteId.HasValue))
      {
        if (!this.Basis.Get<ToteSupport>().TryRemoveToteOf(link))
        {
          SOPickerToShipmentLink copy = PXCache<SOPickerToShipmentLink>.CreateCopy(link);
          copy.ToteID = new int?(0);
          ((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).Delete(link);
          ((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).Insert(copy);
        }
        queue = true;
      }
    }
    if (((PXSelectBase<SOPicker>) this.WSBasis.Picker).Current != null)
    {
      Guid? userId1 = ((PXSelectBase<SOPicker>) this.WSBasis.Picker).Current.UserID;
      Guid userId2 = ((PXGraph) this.Graph).Accessinfo.UserID;
      if ((userId1.HasValue ? (userId1.GetValueOrDefault() == userId2 ? 1 : 0) : 0) != 0)
      {
        ((PXSelectBase<SOPicker>) this.WSBasis.Picker).Current.UserID = new Guid?();
        ((PXSelectBase<SOPicker>) this.WSBasis.Picker).UpdateCurrent();
        queue = true;
      }
    }
    if (((PXSelectBase<SOPickingWorksheet>) this.WSBasis.Worksheet).Current != null && ((PXSelectBase<SOPickingWorksheet>) this.WSBasis.Worksheet).Current.Status == "I" && PXResultset<SOPicker>.op_Implicit(PXSelectBase<SOPicker, PXViewOf<SOPicker>.BasedOn<SelectFromBase<SOPicker, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPicker.userID, IsNotNull>>>, And<BqlOperand<SOPicker.pickerNbr, IBqlInt>.IsNotEqual<BqlField<SOPicker.pickerNbr, IBqlInt>.FromCurrent>>>>.And<KeysRelation<Field<SOPicker.worksheetNbr>.IsRelatedTo<SOPickingWorksheet.worksheetNbr>.AsSimpleKey.WithTablesOf<SOPickingWorksheet, SOPicker>, SOPickingWorksheet, SOPicker>.SameAsCurrent>>>.ReadOnly.Config>.Select(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), Array.Empty<object>())) == null)
    {
      ((PXSelectBase<SOPickingWorksheet>) this.WSBasis.Worksheet).Current.Status = "N";
      ((PXSelectBase<SOPickingWorksheet>) this.WSBasis.Worksheet).UpdateCurrent();
      queue = true;
    }
    if (queue && this.Basis.CurrentMode is PaperlessPicking.SinglePickMode)
      this.Graph.WorkLogExt.SuspendFor(this.SingleShipmentNbr, new Guid?(((PXGraph) this.Graph).Accessinfo.UserID), "PICK");
    return queue;
  }

  public virtual int? GetNextWantedLineNbr()
  {
    return ((IEnumerable<SOPickerListEntry>) ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).SelectMain(Array.Empty<object>())).Where<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (ent =>
    {
      Decimal? pickedQty = ent.PickedQty;
      Decimal? qty = ent.Qty;
      return pickedQty.GetValueOrDefault() < qty.GetValueOrDefault() & pickedQty.HasValue & qty.HasValue && !ent.ForceCompleted.GetValueOrDefault();
    })).FirstOrDefault<SOPickerListEntry>()?.EntryNbr;
  }

  [Obsolete("Use the GetSplitsForRemoval method instead.")]
  public virtual SOPickerListEntry GetSplitForRemoval()
  {
    return this.GetSplitsForRemoval().FirstOrDefault<SOPickerListEntry>();
  }

  public virtual IEnumerable<SOPickerListEntry> GetSplitsForRemoval()
  {
    return !this.Basis.Remove.GetValueOrDefault() || !this.Basis.InventoryID.HasValue ? (IEnumerable<SOPickerListEntry>) Array.Empty<SOPickerListEntry>() : ((IEnumerable<SOPickerListEntry>) ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).SelectMain(Array.Empty<object>())).Where<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (ent =>
    {
      Decimal? pickedQty = ent.PickedQty;
      Decimal num = 0M;
      return pickedQty.GetValueOrDefault() > num & pickedQty.HasValue && !ent.ForceCompleted.GetValueOrDefault() && this.WSBasis.IsSelectedSplit(ent);
    }));
  }

  public virtual bool NeedInversedDirection(SOPicker picker, int nearestLocationID)
  {
    if (!((PXSelectBase<SOPickPackShipSetup>) this.Basis.Setup).Current.AllowBidirectionalPickLists.GetValueOrDefault())
      return false;
    PXResult<SOPickerListEntry, INLocation> pxResult1 = (PXResult<SOPickerListEntry, INLocation>) PXResultset<SOPickerListEntry>.op_Implicit(PXSelectBase<SOPickerListEntry, PXViewOf<SOPickerListEntry>.BasedOn<SelectFromBase<SOPickerListEntry, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INLocation>.On<SOPickerListEntry.FK.Location>>, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<SOPickerListEntry.FK.InventoryItem>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickerListEntry.pickedQty, Less<SOPickerListEntry.qty>>>>, And<BqlOperand<SOPickerListEntry.forceCompleted, IBqlBool>.IsNotEqual<True>>>>.And<KeysRelation<CompositeKey<Field<SOPickerListEntry.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickerListEntry.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickerListEntry>, SOPicker, SOPickerListEntry>.SameAsCurrent>>.Order<By<BqlField<INLocation.pathPriority, IBqlInt>.Asc, BqlField<INLocation.locationCD, IBqlString>.Asc, BqlField<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.Asc, BqlField<SOPickerListEntry.lotSerialNbr, IBqlString>.Asc>>>.Config>.SelectSingleBound(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), (object[]) new SOPicker[1]
    {
      picker
    }, Array.Empty<object>()));
    PXResult<SOPickerListEntry, INLocation> pxResult2 = (PXResult<SOPickerListEntry, INLocation>) PXResultset<SOPickerListEntry>.op_Implicit(PXSelectBase<SOPickerListEntry, PXViewOf<SOPickerListEntry>.BasedOn<SelectFromBase<SOPickerListEntry, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INLocation>.On<SOPickerListEntry.FK.Location>>, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<SOPickerListEntry.FK.InventoryItem>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickerListEntry.pickedQty, Less<SOPickerListEntry.qty>>>>, And<BqlOperand<SOPickerListEntry.forceCompleted, IBqlBool>.IsNotEqual<True>>>>.And<KeysRelation<CompositeKey<Field<SOPickerListEntry.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickerListEntry.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickerListEntry>, SOPicker, SOPickerListEntry>.SameAsCurrent>>.Order<By<BqlField<INLocation.pathPriority, IBqlInt>.Desc, BqlField<INLocation.locationCD, IBqlString>.Desc, BqlField<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.Desc, BqlField<SOPickerListEntry.lotSerialNbr, IBqlString>.Desc>>>.Config>.SelectSingleBound(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), (object[]) new SOPicker[1]
    {
      picker
    }, Array.Empty<object>()));
    if (pxResult1 == null || pxResult2 == null)
      return false;
    int betweenLocations = this.GetDistanceBetweenLocations(((PXResult) pxResult1).GetItem<SOPickerListEntry>().LocationID, new int?(nearestLocationID));
    return this.GetDistanceBetweenLocations(((PXResult) pxResult2).GetItem<SOPickerListEntry>().LocationID, new int?(nearestLocationID)) < betweenLocations;
  }

  public virtual int GetDistanceBetweenLocations(int? leftlocationID, int? rightLocationID)
  {
    INLocation inLocation1 = INLocation.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), leftlocationID);
    INLocation inLocation2 = INLocation.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), rightLocationID);
    int? pathPriority1 = inLocation1.PathPriority;
    int? pathPriority2 = inLocation2.PathPriority;
    return Math.Abs((pathPriority1.HasValue & pathPriority2.HasValue ? new int?(pathPriority1.GetValueOrDefault() - pathPriority2.GetValueOrDefault()) : new int?()).GetValueOrDefault());
  }

  public virtual SOPickerListEntry GetWantedSplit()
  {
    return PXResultset<SOPickerListEntry>.op_Implicit(!this.WantedLineNbr.HasValue ? (PXResultset<SOPickerListEntry>) null : ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).Search<SOPickerListEntry.entryNbr>((object) this.WantedLineNbr, Array.Empty<object>()));
  }

  [Obsolete("Use the GetWantedSplitsForIncrease method instead.")]
  public virtual SOPickerListEntry GetWantedSplitForIncrease()
  {
    return this.GetWantedSplitsForIncrease().FirstOrDefault<SOPickerListEntry>();
  }

  public virtual IEnumerable<SOPickerListEntry> GetWantedSplitsForIncrease()
  {
    SOPickerListEntry wantedSplit = this.GetWantedSplit();
    if (wantedSplit == null)
      return (IEnumerable<SOPickerListEntry>) null;
    IEnumerable<SOPickerListEntry> source = (IEnumerable<SOPickerListEntry>) ((IEnumerable<SOPickerListEntry>) ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).SelectMain(Array.Empty<object>())).Where<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (r => this.WSBasis.AreSplitsSimilar(r, wantedSplit) && !r.ForceCompleted.GetValueOrDefault())).OrderByAccordanceTo<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (s =>
    {
      int? entryNbr1 = s.EntryNbr;
      int? entryNbr2 = wantedSplit.EntryNbr;
      return entryNbr1.GetValueOrDefault() == entryNbr2.GetValueOrDefault() & entryNbr1.HasValue == entryNbr2.HasValue;
    }));
    ToteSupport toteBasis = this.Basis.Get<ToteSupport>();
    if (toteBasis != null && EnumerableExtensions.IsNotIn<int?>(toteBasis.ToteID, new int?(), wantedSplit.ToteID))
      source = source.Where<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (r =>
      {
        int? toteId1 = r.ToteID;
        int? toteId2 = toteBasis.ToteID;
        return toteId1.GetValueOrDefault() == toteId2.GetValueOrDefault() & toteId1.HasValue == toteId2.HasValue;
      }));
    return source;
  }

  public virtual bool EnsureLocationFromLastVisited()
  {
    if (this.Basis.LocationID.HasValue || this.Basis.DefaultLocation)
      return false;
    this.Basis.LocationID = this.LastVisitedLocationID;
    return true;
  }

  public virtual string PromptWantedItem()
  {
    SOPickerListEntry wantedSplit = this.GetWantedSplit();
    if (wantedSplit == null)
      return (string) null;
    IEnumerable<SOPickerListEntry> splitsForIncrease = this.GetWantedSplitsForIncrease();
    INLotSerClass lotSerialClassOf = this.Basis.GetLotSerialClassOf(PX.Objects.IN.InventoryItem.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), wantedSplit.InventoryID));
    bool flag = lotSerialClassOf == null || lotSerialClassOf.LotSerTrack == "N" || lotSerialClassOf.LotSerAssign == "U" || lotSerialClassOf.LotSerIssueMethod == "U";
    return this.Basis.Localize(this.Basis.DefaultLocation ? (flag ? "Go to {4} and pick {2}, quantity: {0} {1} left." : "Go to {4} and pick {2}, lot or serial: {3}, quantity: {0} {1} left.") : (flag ? "Pick {2}, quantity: {0} {1} left." : "Pick {2}, lot or serial: {3}, quantity: {0} {1} left."), new object[5]
    {
      (object) splitsForIncrease.Sum<SOPickerListEntry>((Func<SOPickerListEntry, Decimal?>) (s =>
      {
        Decimal? qty = s.Qty;
        Decimal? pickedQty = s.PickedQty;
        return !(qty.HasValue & pickedQty.HasValue) ? new Decimal?() : new Decimal?(qty.GetValueOrDefault() - pickedQty.GetValueOrDefault());
      })),
      (object) wantedSplit.UOM,
      (object) this.Basis.SightOf<SOPickerListEntry.inventoryID>((IBqlTable) wantedSplit),
      (object) wantedSplit.LotSerialNbr,
      (object) this.Basis.SightOf<SOPickerListEntry.locationID>((IBqlTable) wantedSplit)
    });
  }

  public virtual void EnsureShipmentUserLinkForPaperlessPick()
  {
    this.Graph.WorkLogExt.EnsureFor(this.SingleShipmentNbr, new Guid?(((PXGraph) this.Graph).Accessinfo.UserID), "PICK");
  }

  public virtual void InjectShipmentPromptWithTakeNext(PickPackShip.ShipmentState pickShipment)
  {
    ((MethodInterceptor<EntityState<PickPackShip, PX.Objects.SO.SOShipment>, PickPackShip>.OfFunc<string>) ((EntityState<PickPackShip, PX.Objects.SO.SOShipment>) pickShipment).Intercept.StatePrompt).ByReplace((Func<PickPackShip, string>) (basis => basis.Localize("Scan the pick list number, or click Next List.", Array.Empty<object>())), new RelativeInject?());
  }

  public virtual void InjectSuppressShipmentWithWorksheetOfSingleType(
    PickPackShip.ShipmentState pickShipment)
  {
    ((MethodInterceptor<EntityState<PickPackShip, PX.Objects.SO.SOShipment>, PickPackShip>.OfFunc<string, PX.Objects.SO.SOShipment>) ((EntityState<PickPackShip, PX.Objects.SO.SOShipment>) pickShipment).Intercept.GetByBarcode).ByOverride((Func<PX.Objects.SO.SOShipment, string, Func<string, PX.Objects.SO.SOShipment>, PX.Objects.SO.SOShipment>) ((basis, barcode, base_GetByBarcode) =>
    {
      PX.Objects.SO.SOShipment soShipment = base_GetByBarcode(barcode);
      if (soShipment != null && soShipment.CurrentWorksheetNbr != null)
      {
        SOPickingWorksheet pickingWorksheet = SOPickingWorksheet.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) basis), soShipment.CurrentWorksheetNbr);
        if (pickingWorksheet != null && pickingWorksheet.WorksheetType == "SS")
          return (PX.Objects.SO.SOShipment) null;
      }
      return soShipment;
    }), new RelativeInject?());
  }

  public virtual void InjectShipmentAbsenceHandlingByWorksheetOfSingleType(
    PickPackShip.PickMode.ShipmentState pickShipment)
  {
    ((MethodInterceptor<EntityState<PickPackShip, PX.Objects.SO.SOShipment>, PickPackShip>.OfFunc<string, AbsenceHandling.Of<PX.Objects.SO.SOShipment>>.AsAppendable) ((EntityState<PickPackShip, PX.Objects.SO.SOShipment>) pickShipment).Intercept.HandleAbsence).ByPrepend((Func<AbsenceHandling.Of<PX.Objects.SO.SOShipment>, string, AbsenceHandling.Of<PX.Objects.SO.SOShipment>>) ((basis, barcode) =>
    {
      if (!barcode.Contains("/"))
      {
        PaperlessPicking.SinglePickMode mode = basis.FindMode<PaperlessPicking.SinglePickMode>();
        if (mode != null && ((ScanMode<PickPackShip>) mode).IsActive && ((ScanMode<PickPackShip>) mode).TryProcessBy<PaperlessPicking.PickListState>(barcode, (StateSubstitutionRule) 1))
        {
          basis.SetScanMode<PaperlessPicking.SinglePickMode>();
          ((ScanState<PickPackShip>) basis.FindState<PaperlessPicking.PickListState>(false)).Process(barcode);
          return AbsenceHandling.Of<PX.Objects.SO.SOShipment>.op_Implicit(AbsenceHandling.Done);
        }
      }
      return AbsenceHandling.Of<PX.Objects.SO.SOShipment>.op_Implicit(AbsenceHandling.Skipped);
    }), new RelativeInject?());
  }

  public virtual void InjectPickListPaperless(WorksheetPicking.PickListState pickListState)
  {
    ((MethodInterceptor<EntityState<PickPackShip, PXResult<SOPickingWorksheet, SOPicker>>, PickPackShip>.OfAction) ((EntityState<PickPackShip, PXResult<SOPickingWorksheet, SOPicker>>) ((MethodInterceptor<EntityState<PickPackShip, PXResult<SOPickingWorksheet, SOPicker>>, PickPackShip>.OfAction<PXResult<SOPickingWorksheet, SOPicker>>) ((EntityState<PickPackShip, PXResult<SOPickingWorksheet, SOPicker>>) ((MethodInterceptor<EntityState<PickPackShip, PXResult<SOPickingWorksheet, SOPicker>>, PickPackShip>.OfFunc<PXResult<SOPickingWorksheet, SOPicker>, Validation>.AsAppendable) ((EntityState<PickPackShip, PXResult<SOPickingWorksheet, SOPicker>>) ((MethodInterceptor<EntityState<PickPackShip, PXResult<SOPickingWorksheet, SOPicker>>, PickPackShip>.OfFunc<string>) ((EntityState<PickPackShip, PXResult<SOPickingWorksheet, SOPicker>>) pickListState).Intercept.StatePrompt).ByReplace((Func<PickPackShip, string>) (basis => basis.Localize("Scan the pick list number, or click Next List.", Array.Empty<object>())), new RelativeInject?())).Intercept.Validate).ByAppend((Func<Validation, PXResult<SOPickingWorksheet, SOPicker>, Validation>) ((basis, pickList) =>
    {
      WorksheetPicking wsBasis = basis.Get<PaperlessPicking>().WSBasis;
      if (PaperlessPicking.PickListRejection(wsBasis, PXResult<SOPickingWorksheet, SOPicker>.op_Implicit(pickList)) && ((IEnumerable<SOPickerListEntry>) ((PXSelectBase<SOPickerListEntry>) wsBasis.PickListOfPicker).SelectMain(Array.Empty<object>())).Any<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (pl =>
      {
        Decimal? pickedQty = pl.PickedQty;
        Decimal num = 0M;
        return pickedQty.GetValueOrDefault() > num & pickedQty.HasValue;
      })))
        return Validation.Fail("The {0} pick list cannot be returned to the picking queue because it is in progress.", new object[1]
        {
          (object) ((PXSelectBase<SOPickingJob>) wsBasis.PickingJob).Current.PickListNbr
        });
      SOPickingJob soPickingJob = KeysRelation<CompositeKey<Field<SOPickingJob.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickingJob.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickingJob>, SOPicker, SOPickingJob>.SelectChildren(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) basis), PXResult<SOPickingWorksheet, SOPicker>.op_Implicit(pickList)).FirstOrDefault<SOPickingJob>();
      if (soPickingJob != null)
      {
        if (EnumerableExtensions.IsNotIn<string>(soPickingJob.Status, "ENQ", "RNQ", "ASG", "PNG"))
          return Validation.Fail("The {0} pick list cannot be processed because it has the {1} status.", new object[2]
          {
            (object) soPickingJob.PickListNbr,
            (object) basis.SightOf<SOPickingJob.status>((IBqlTable) soPickingJob)
          });
        if (EnumerableExtensions.IsNotIn<Guid?>(soPickingJob.PreferredAssigneeID, new Guid?(), new Guid?(((PXGraph) basis.Graph).Accessinfo.UserID)))
          return Validation.Fail("The {0} pick list cannot be processed because it is already assigned to another picker.", new object[1]
          {
            (object) soPickingJob.PickListNbr
          });
        if (EnumerableExtensions.IsNotIn<Guid?>(soPickingJob.ActualAssigneeID, new Guid?(), new Guid?(((PXGraph) basis.Graph).Accessinfo.UserID)))
        {
          int? lastModification = soPickingJob.MinutesSinceLastModification;
          int num = ((BqlConstant<PaperlessPicking.minutes15, IBqlInt, int>) new PaperlessPicking.minutes15()).Value;
          if (lastModification.GetValueOrDefault() < num & lastModification.HasValue)
            return Validation.Fail("The {0} pick list cannot be processed because it is already assigned to another picker.", new object[1]
            {
              (object) soPickingJob.PickListNbr
            });
        }
      }
      return Validation.Ok;
    }), new RelativeInject?())).Intercept.Apply).ByOverride((Action<PickPackShip, PXResult<SOPickingWorksheet, SOPicker>, Action<PXResult<SOPickingWorksheet, SOPicker>>>) ((basis, pickList, base_Apply) =>
    {
      bool flag = false;
      PaperlessPicking paperlessPicking1 = basis.Get<PaperlessPicking>();
      WorksheetPicking wsBasis = paperlessPicking1.WSBasis;
      if (PaperlessPicking.PickListRejection(wsBasis, PXResult<SOPickingWorksheet, SOPicker>.op_Implicit(pickList)))
        flag |= paperlessPicking1.ReturnCurrentJobToQueue();
      base_Apply(pickList);
      PaperlessPicking paperlessPicking2 = paperlessPicking1;
      int? visitedLocationId = paperlessPicking1.LastVisitedLocationID;
      int num;
      if (visitedLocationId.HasValue)
      {
        PaperlessPicking paperlessPicking3 = paperlessPicking1;
        SOPicker picker = PXResult<SOPickingWorksheet, SOPicker>.op_Implicit(pickList);
        visitedLocationId = paperlessPicking1.LastVisitedLocationID;
        int nearestLocationID = visitedLocationId.Value;
        num = paperlessPicking3.NeedInversedDirection(picker, nearestLocationID) ? 1 : 0;
      }
      else
        num = 0;
      bool? nullable = new bool?(num != 0);
      paperlessPicking2.PathInversedDirection = nullable;
      paperlessPicking1.WantedLineNbr = paperlessPicking1.GetNextWantedLineNbr();
      basis.Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>(true);
      basis.Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>(true);
      if (!(flag | wsBasis.AssignUser()))
        return;
      basis.SaveChanges();
    }), new RelativeInject?())).Intercept.ClearState).ByAppend((Action<PickPackShip>) (basis =>
    {
      PaperlessPicking paperlessPicking = basis.Get<PaperlessPicking>();
      paperlessPicking.PathInversedDirection = new bool?();
      paperlessPicking.WantedLineNbr = new int?();
      basis.Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>(true);
      basis.Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>(true);
    }), new RelativeInject?());
  }

  public virtual void InjectNavigationOnLocation(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState locState)
  {
    ((MethodInterceptor<EntityState<PickPackShip, INLocation>, PickPackShip>.OfAction<INLocation>) ((EntityState<PickPackShip, INLocation>) ((MethodInterceptor<EntityState<PickPackShip, INLocation>, PickPackShip>.OfFunc<INLocation, Validation>.AsAppendable) ((MethodInterceptor<EntityState<PickPackShip, INLocation>, PickPackShip>.OfPredicate) ((MethodInterceptor<EntityState<PickPackShip, INLocation>, PickPackShip>.OfPredicate) ((MethodInterceptor<EntityState<PickPackShip, INLocation>, PickPackShip>.OfPredicate) ((EntityState<PickPackShip, INLocation>) ((MethodInterceptor<EntityState<PickPackShip, INLocation>, PickPackShip>.OfFunc<string>.AsAppendable) ((EntityState<PickPackShip, INLocation>) locState).Intercept.StateInstructions).ByAppend((Func<PickPackShip, string>) (basis =>
    {
      SOPickerListEntry soPickerListEntry = basis.Get<WorksheetPicking>().GetEntriesToPick().FirstOrDefault<SOPickerListEntry>();
      if (soPickerListEntry == null)
        return (string) null;
      return basis.Localize("Go to the {0} location.", new object[1]
      {
        (object) basis.SightOf<SOPickerListEntry.locationID>((IBqlTable) soPickerListEntry)
      });
    }), new RelativeInject?())).Intercept.IsStateActive).ByConjoin((Func<PickPackShip, bool>) (basis => (!basis.Remove.GetValueOrDefault()).Implies(!basis.DefaultLocation)), false, new RelativeInject?()).Intercept.IsStateSkippable).ByDisjoin((Func<PickPackShip, bool>) (basis => !basis.Remove.GetValueOrDefault() && basis.LocationID.HasValue), false, new RelativeInject?()).Intercept.IsStateSkippable).ByDisjoin((Func<PickPackShip, bool>) (basis => !basis.Remove.GetValueOrDefault() && basis.Get<PaperlessPicking>().With<PaperlessPicking, bool>((Func<PaperlessPicking, bool>) (it =>
    {
      int? nullable = (int?) it.GetWantedSplit()?.LocationID;
      if (!nullable.HasValue)
        return false;
      int valueOrDefault = nullable.GetValueOrDefault();
      nullable = it.LastVisitedLocationID;
      int num = valueOrDefault;
      return nullable.GetValueOrDefault() == num & nullable.HasValue;
    }))), false, new RelativeInject?()).Intercept.Validate).ByAppend((Func<Validation, INLocation, Validation>) ((basis, location) =>
    {
      int? locationId1 = location.LocationID;
      int? locationId2 = (int?) basis.Get<WorksheetPicking>().GetEntriesToPick().FirstOrDefault<SOPickerListEntry>()?.LocationID;
      if (locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue)
        return Validation.Ok;
      return Validation.Fail("Incorrect location {0} scanned.", new object[1]
      {
        (object) location.LocationCD
      });
    }), new RelativeInject?())).Intercept.Apply).ByAppend((Action<PickPackShip, INLocation>) ((basis, location) => basis.Get<PaperlessPicking>().LastVisitedLocationID = location.LocationID), new RelativeInject?());
  }

  public virtual void InjectNavigationOnItem(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState itemState)
  {
    ((MethodInterceptor<EntityState<PickPackShip, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, PickPackShip>.OfFunc<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>, Validation>.AsAppendable) ((MethodInterceptor<EntityState<PickPackShip, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, PickPackShip>.OfPredicate) ((EntityState<PickPackShip, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>) ((MethodInterceptor<EntityState<PickPackShip, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, PickPackShip>.OfFunc<string>.AsAppendable) ((EntityState<PickPackShip, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>) itemState).Intercept.StateInstructions).ByAppend((Func<PickPackShip, string>) (basis => basis.Remove.GetValueOrDefault() ? (string) null : basis.Get<PaperlessPicking>().PromptWantedItem()), new RelativeInject?())).Intercept.IsStateSkippable).ByDisjoin((Func<PickPackShip, bool>) (basis => !basis.Remove.GetValueOrDefault() && basis.InventoryID.HasValue && basis.FindState<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>(false).With<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState, bool>((Func<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState, bool>) (ls => ((ScanState<PickPackShip>) ls).IsActive && !((ScanState<PickPackShip>) ls).IsSkippable))), false, new RelativeInject?()).Intercept.Validate).ByAppend((Func<Validation, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>, Validation>) ((basis, item) =>
    {
      if (basis.Remove.GetValueOrDefault() || basis.Get<PaperlessPicking>().GetWantedSplit().With<SOPickerListEntry, (int?, int?)>((Func<SOPickerListEntry, (int?, int?)>) (it => (it.InventoryID, it.SubItemID))).Equals(((PXResult) item).GetItem<INItemXRef>().With<INItemXRef, (int?, int?)>((Func<INItemXRef, (int?, int?)>) (it => (it.InventoryID, it.SubItemID)))))
        return Validation.Ok;
      return Validation.Fail("Incorrect item {0} scanned.", new object[1]
      {
        (object) ((PXResult) item).GetItem<INItemXRef>().AlternateID
      });
    }), new RelativeInject?());
  }

  public virtual void InjectNavigationOnLotSerial(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState lsState)
  {
    ((MethodInterceptor<EntityState<PickPackShip, string>, PickPackShip>.OfFunc<string, Validation>.AsAppendable) ((EntityState<PickPackShip, string>) ((MethodInterceptor<EntityState<PickPackShip, string>, PickPackShip>.OfFunc<string>.AsAppendable) ((EntityState<PickPackShip, string>) lsState).Intercept.StateInstructions).ByAppend((Func<PickPackShip, string>) (basis => basis.Remove.GetValueOrDefault() ? (string) null : basis.Get<PaperlessPicking>().PromptWantedItem()), new RelativeInject?())).Intercept.Validate).ByAppend((Func<Validation, string, Validation>) ((basis, lotSerialNbr) =>
    {
      if (basis.Remove.GetValueOrDefault() || basis.LotSerialTrack.IsEnterable || string.Equals(lotSerialNbr, basis.Get<PaperlessPicking>().GetWantedSplit().LotSerialNbr, StringComparison.OrdinalIgnoreCase))
        return Validation.Ok;
      return Validation.Fail("Incorrect lot or serial number {0} scanned.", new object[1]
      {
        (object) lotSerialNbr
      });
    }), new RelativeInject?());
  }

  public virtual void InjectRemoveClearLocationAndInventory(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.RemoveCommand remove)
  {
    ((MethodInterceptor<ScanCommand<PickPackShip>, PickPackShip>.OfFunc<bool>) ((ScanCommand<PickPackShip>) remove).Intercept.Process).ByOverride((Func<PickPackShip, Func<bool>, bool>) ((basis, base_Process) =>
    {
      basis.Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>(true);
      basis.Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>(true);
      return base_Process();
    }), new RelativeInject?());
  }

  public virtual void InjectConfirmPickListSuppressionOnCanPick(
    WorksheetPicking.ConfirmPickListCommand confirm)
  {
    ((MethodInterceptor<ScanCommand<PickPackShip>, PickPackShip>.OfPredicate) ((ScanCommand<PickPackShip>) confirm).Intercept.IsEnabled).ByConjoin((Func<PickPackShip, bool>) (basis => !basis.Get<WorksheetPicking>().CanWSPick), false, new RelativeInject?());
  }

  public static bool PickListRejection(WorksheetPicking wsBasis, SOPicker newPicker)
  {
    if (wsBasis.WorksheetNbr != null)
    {
      SOPicker soPicker = SOPicker.PK.Find((PXGraph) wsBasis.Graph, wsBasis.WorksheetNbr, wsBasis.PickerNbr);
      if (soPicker != null && soPicker.PickListNbr != newPicker?.PickListNbr)
        return !soPicker.Confirmed.GetValueOrDefault();
    }
    return false;
  }

  [PXOverride]
  public IEnumerable<PXResult<SOPickerListEntry, INLocation>> GetListEntries(
    string worksheetNbr,
    int? pickerNbr,
    Func<string, int?, IEnumerable<PXResult<SOPickerListEntry, INLocation>>> baseIgnored)
  {
    return this.WSBasis.GetListEntries(worksheetNbr, pickerNbr, this.PathInversedDirection.GetValueOrDefault());
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.WorksheetPicking.IsWorksheetMode(System.String)" />
  [PXOverride]
  public bool IsWorksheetMode(string modeCode, Func<string, bool> base_IsWorksheetMode)
  {
    return base_IsWorksheetMode(modeCode) || modeCode == "SNGL";
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.WorksheetPicking.ShowWorksheetNbrForMode(System.String)" />
  [PXOverride]
  public bool ShowWorksheetNbrForMode(
    string modeCode,
    Func<string, bool> base_ShowWorksheetNbrForMode)
  {
    return base_ShowWorksheetNbrForMode(modeCode) && modeCode != "SNGL";
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.WorksheetPicking.AssignUser(System.Boolean)" />
  [PXOverride]
  public bool AssignUser(bool startPicking, Func<bool, bool> base_AssignUser)
  {
    if (((PXSelectBase<SOPickingJob>) this.WSBasis.PickingJob).Current == null)
      return base_AssignUser(startPicking);
    bool flag = false;
    SOPickingJob current1 = ((PXSelectBase<SOPickingJob>) this.WSBasis.PickingJob).Current;
    SOPicker current2 = ((PXSelectBase<SOPicker>) this.WSBasis.Picker).Current;
    SOPickingWorksheet current3 = ((PXSelectBase<SOPickingWorksheet>) this.WSBasis.Worksheet).Current;
    SOPicker soPicker = current2;
    SOPickingJob soPickingJob = current1;
    if (startPicking && current3.Status == "N")
    {
      current3.Status = "I";
      ((PXSelectBase<SOPickingWorksheet>) this.WSBasis.Worksheet).Update(current3);
      flag = true;
    }
    Guid? nullable;
    if (startPicking)
    {
      nullable = soPicker.UserID;
      Guid userId = ((PXGraph) this.Graph).Accessinfo.UserID;
      if ((nullable.HasValue ? (nullable.GetValueOrDefault() != userId ? 1 : 0) : 1) != 0)
      {
        soPicker.UserID = new Guid?(((PXGraph) this.Graph).Accessinfo.UserID);
        ((PXSelectBase<SOPicker>) this.WSBasis.Picker).Update(soPicker);
        flag = true;
      }
    }
    if (startPicking && EnumerableExtensions.IsIn<string>(soPickingJob.Status, "ENQ", "RNQ", "ASG"))
    {
      soPickingJob.Status = "PNG";
      soPickingJob = ((PXSelectBase<SOPickingJob>) this.WSBasis.PickingJob).Update(soPickingJob);
      flag = true;
    }
    nullable = soPickingJob.ActualAssigneeID;
    Guid userId1 = ((PXGraph) this.Graph).Accessinfo.UserID;
    if ((nullable.HasValue ? (nullable.GetValueOrDefault() != userId1 ? 1 : 0) : 1) != 0)
    {
      soPickingJob.ActualAssigneeID = new Guid?(((PXGraph) this.Graph).Accessinfo.UserID);
      ((PXSelectBase<SOPickingJob>) this.WSBasis.PickingJob).Update(soPickingJob);
      flag = true;
    }
    if (startPicking)
      this.IgnoredPickingJobs.Clear();
    if (flag)
      this.EnsureShipmentUserLinkForPaperlessPick();
    return flag;
  }

  [PXOverride]
  public void SetPickList(
    PXResult<SOPickingWorksheet, SOPicker> pickList,
    Action<PXResult<SOPickingWorksheet, SOPicker>> base_SetPickList)
  {
    base_SetPickList(pickList);
    if (!(this.Basis.CurrentMode is PaperlessPicking.SinglePickMode))
      return;
    string singleShipmentNbr = ((PXResult) pickList)?.GetItem<SOPickingWorksheet>()?.SingleShipmentNbr;
    this.Basis.NoteID = (Guid?) PX.Objects.SO.SOShipment.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), singleShipmentNbr)?.NoteID;
  }

  [PXOverride]
  public ScanMode<PickPackShip> FindModeForWorksheet(
    SOPickingWorksheet sheet,
    Func<SOPickingWorksheet, ScanMode<PickPackShip>> base_FindModeForWorksheet)
  {
    return sheet.WorksheetType == "SS" ? (ScanMode<PickPackShip>) this.Basis.FindMode<PaperlessPicking.SinglePickMode>() : base_FindModeForWorksheet(sheet);
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.WorksheetPicking.GetEntriesToPick" />
  [PXOverride]
  public IEnumerable<SOPickerListEntry> GetEntriesToPick(
    Func<IEnumerable<SOPickerListEntry>> base_GetEntriesToPick)
  {
    if (!(this.Basis.CurrentMode is PaperlessPicking.SinglePickMode))
      return base_GetEntriesToPick();
    return !this.Basis.Remove.GetValueOrDefault() ? this.GetWantedSplitsForIncrease() : this.GetSplitsForRemoval();
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.WorksheetPicking.ReportSplitConfirmed(PX.Objects.SO.SOPickerListEntry)" />
  [PXOverride]
  public void ReportSplitConfirmed(
    SOPickerListEntry pickedSplit,
    Action<SOPickerListEntry> base_ReportSplitConfirmed)
  {
    if (this.Basis.CurrentMode is PaperlessPicking.SinglePickMode)
      this.Basis.ReportInfo(this.Basis.Remove.GetValueOrDefault() ? "{0} x {1} {2} removed from tote." : "{0} x {1} {2} added to tote.", new object[3]
      {
        (object) this.Basis.SightOf<WMSScanHeader.inventoryID>(),
        (object) this.Basis.Qty,
        (object) this.Basis.UOM
      });
    else
      base_ReportSplitConfirmed(pickedSplit);
  }

  [PXOverride]
  public ScanState<PickPackShip> DecorateScanState(
    ScanState<PickPackShip> original,
    Func<ScanState<PickPackShip>, ScanState<PickPackShip>> base_DecorateScanState)
  {
    ScanState<PickPackShip> scanState = base_DecorateScanState(original);
    if (((ScanComponent<PickPackShip>) scanState).ModeCode == "SNGL")
    {
      switch (scanState)
      {
        case PaperlessPicking.PickListState pickListState:
          this.InjectPickListPaperless((WorksheetPicking.PickListState) pickListState);
          break;
        case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState locState:
          this.InjectNavigationOnLocation(locState);
          break;
        case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState itemState:
          this.InjectNavigationOnItem(itemState);
          break;
        case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState lsState:
          this.InjectNavigationOnLotSerial(lsState);
          break;
      }
    }
    else if (scanState is PickPackShip.PickMode.ShipmentState pickShipment)
    {
      this.InjectShipmentPromptWithTakeNext((PickPackShip.ShipmentState) pickShipment);
      this.InjectSuppressShipmentWithWorksheetOfSingleType((PickPackShip.ShipmentState) pickShipment);
      this.InjectShipmentAbsenceHandlingByWorksheetOfSingleType(pickShipment);
    }
    return scanState;
  }

  [PXOverride]
  public ScanMode<PickPackShip> DecorateScanMode(
    ScanMode<PickPackShip> original,
    Func<ScanMode<PickPackShip>, ScanMode<PickPackShip>> base_DecorateScanMode)
  {
    ScanMode<PickPackShip> scanMode = base_DecorateScanMode(original);
    if (scanMode is PickPackShip.PickMode)
      ((MethodInterceptor<ScanMode<PickPackShip>, PickPackShip>.OfFunc<IEnumerable<ScanCommand<PickPackShip>>>.AsAppendable) scanMode.Intercept.CreateCommands).ByAppend((Func<PickPackShip, IEnumerable<ScanCommand<PickPackShip>>>) (basis => (IEnumerable<ScanCommand<PickPackShip>>) new PaperlessPicking.TakeNextPickListCommand[1]
      {
        new PaperlessPicking.TakeNextPickListCommand()
      }), new RelativeInject?());
    return scanMode;
  }

  [PXOverride]
  public ScanCommand<PickPackShip> DecorateScanCommand(
    ScanCommand<PickPackShip> original,
    Func<ScanCommand<PickPackShip>, ScanCommand<PickPackShip>> base_DecorateScanCommand)
  {
    ScanCommand<PickPackShip> scanCommand = base_DecorateScanCommand(original);
    if (((ScanComponent<PickPackShip>) scanCommand).ModeCode == "SNGL")
    {
      switch (scanCommand)
      {
        case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.RemoveCommand remove:
          this.InjectRemoveClearLocationAndInventory(remove);
          break;
        case WorksheetPicking.ConfirmPickListCommand confirm:
          this.InjectConfirmPickListSuppressionOnCanPick(confirm);
          break;
      }
    }
    return scanCommand;
  }

  /// Overrides <see cref="M:PX.BarcodeProcessing.BarcodeDrivenStateMachine`2.OnBeforeFullClear" />
  [PXOverride]
  public void OnBeforeFullClear(Action base_OnBeforeFullClear)
  {
    base_OnBeforeFullClear();
    if (!(this.Basis.CurrentMode is PaperlessPicking.SinglePickMode) || this.SingleShipmentNbr == null || !this.Graph.WorkLogExt.SuspendFor(this.SingleShipmentNbr, new Guid?(((PXGraph) this.Graph).Accessinfo.UserID), "PICK"))
      return;
    this.Graph.WorkLogExt.PersistWorkLog();
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.PickPackShip.UpdateWorkLogOnLogScan(PX.Objects.SO.SOShipmentEntry.WorkLog,System.Boolean)" />
  [PXOverride]
  public void UpdateWorkLogOnLogScan(
    SOShipmentEntry.WorkLog workLogger,
    bool isError,
    Action<SOShipmentEntry.WorkLog, bool> base_UpdateWorkLogOnLogScan)
  {
    base_UpdateWorkLogOnLogScan(workLogger, isError);
    if (!(this.Basis.CurrentMode is PaperlessPicking.SinglePickMode) || string.IsNullOrEmpty(this.SingleShipmentNbr))
      return;
    workLogger.LogScanFor(this.SingleShipmentNbr, new Guid?(((PXGraph) this.Graph).Accessinfo.UserID), "PICK", isError);
  }

  public sealed class SinglePickMode : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanMode
  {
    public const string Value = "SNGL";

    public PaperlessPicking PPBasis => this.Get<PaperlessPicking>();

    public virtual string Code => "SNGL";

    public virtual string Description => "Paperless Pick";

    protected virtual bool IsModeActive() => ((ScanMode<PickPackShip>) this).Basis.HasPick;

    protected virtual IEnumerable<ScanState<PickPackShip>> CreateStates()
    {
      yield return (ScanState<PickPackShip>) new PaperlessPicking.PickListState();
      yield return (ScanState<PickPackShip>) new ToteSupport.AssignToteState();
      yield return (ScanState<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState();
      yield return (ScanState<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState()
      {
        AlternateType = new INPrimaryAlternateType?(INPrimaryAlternateType.CPN),
        IsForIssue = true
      };
      yield return (ScanState<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState();
      yield return (ScanState<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState()
      {
        IsForIssue = true
      };
      yield return (ScanState<PickPackShip>) new ToteSupport.SelectToteState();
      yield return (ScanState<PickPackShip>) new PaperlessPicking.ConfirmState();
      yield return (ScanState<PickPackShip>) new PaperlessPicking.WarehouseState();
      yield return (ScanState<PickPackShip>) new PaperlessPicking.NearestLocationState();
    }

    protected virtual IEnumerable<ScanTransition<PickPackShip>> CreateTransitions()
    {
      return ((ScanMode<PickPackShip>) this).StateFlow((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (flow => flow.ForkBy((Func<PickPackShip, bool>) (basis => !basis.Remove.GetValueOrDefault())).PositiveBranch((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (pfl => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) pfl.From<PaperlessPicking.PickListState>().NextTo<ToteSupport.AssignToteState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState>((Action<PickPackShip>) null)).NextTo<ToteSupport.SelectToteState>((Action<PickPackShip>) null))).NegativeBranch((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (nfl => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) nfl.From<PaperlessPicking.PickListState>().NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>((Action<PickPackShip>) null)))));
    }

    protected virtual IEnumerable<ScanCommand<PickPackShip>> CreateCommands()
    {
      yield return (ScanCommand<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.RemoveCommand();
      yield return (ScanCommand<PickPackShip>) new BarcodeQtySupport<PickPackShip, PickPackShip.Host>.SetQtyCommand();
      yield return (ScanCommand<PickPackShip>) new WorksheetPicking.ConfirmPickListCommand();
      yield return (ScanCommand<PickPackShip>) new PaperlessPicking.TakeNextPickListCommand();
      yield return (ScanCommand<PickPackShip>) new PaperlessPicking.ConfirmPickListAndTakeNextCommand();
      yield return (ScanCommand<PickPackShip>) new ToteSupport.AddToteCommand();
      yield return (ScanCommand<PickPackShip>) new PaperlessPicking.ConfirmLineQtyCommand();
    }

    protected virtual IEnumerable<ScanRedirect<PickPackShip>> CreateRedirects()
    {
      return AllWMSRedirects.CreateFor<PickPackShip>();
    }

    protected virtual void ResetMode(bool fullReset)
    {
      ((ScanMode<PickPackShip>) this).ResetMode(fullReset);
      ((ScanMode<PickPackShip>) this).Clear<PaperlessPicking.PickListState>(fullReset && !((ScanMode<PickPackShip>) this).Basis.IsWithinReset);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>(fullReset);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>(fullReset);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>(true);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState>(true);
      ((ScanMode<PickPackShip>) this).Clear<ToteSupport.AssignToteState>(true);
      ((ScanMode<PickPackShip>) this).Clear<ToteSupport.SelectToteState>(true);
    }

    public class value : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    PaperlessPicking.SinglePickMode.value>
    {
      public value()
        : base("SNGL")
      {
      }
    }

    [PXLocalizable]
    public abstract class Msg : ScanMode<PickPackShip>.Msg
    {
      public const string DisplayName = "Paperless Pick";
    }
  }

  public sealed class PickListState : WorksheetPicking.PickListState
  {
    public PaperlessPicking PPBasis
    {
      get => ((ScanComponent<PickPackShip>) this).Basis.Get<PaperlessPicking>();
    }

    protected override string WorksheetType => "SS";

    protected override string StatePrompt => "Scan the pick list number, or click Next List.";

    protected override AbsenceHandling.Of<PXResult<SOPickingWorksheet, SOPicker>> HandleAbsence(
      string barcode)
    {
      if (!barcode.Contains("/"))
      {
        PXResult<SOPickingWorksheet, SOPicker> pxResult = (PXResult<SOPickingWorksheet, SOPicker>) PXResultset<SOPickingWorksheet>.op_Implicit(PXSelectBase<SOPickingWorksheet, PXViewOf<SOPickingWorksheet>.BasedOn<SelectFromBase<SOPickingWorksheet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPicker>.On<SOPicker.FK.Worksheet>>, FbqlJoins.Inner<PX.Objects.SO.SOShipment>.On<SOPickingWorksheet.FK.SingleShipment>>, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<PX.Objects.SO.SOShipment.FK.Site>>, FbqlJoins.Left<PX.Objects.AR.Customer>.On<PX.Objects.SO.SOShipment.FK.Customer>.SingleTableOnly>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingWorksheet.worksheetType, Equal<SOPickingWorksheet.worksheetType.single>>>>, And<BqlOperand<PX.Objects.SO.SOShipment.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>, And<MatchUserFor<PX.Objects.IN.INSite>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Customer.bAccountID, IsNull>>>>.Or<MatchUserFor<PX.Objects.AR.Customer>>>>>.ReadOnly.Config>.Select(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) ((ScanComponent<PickPackShip>) this).Basis), new object[1]
        {
          (object) barcode
        }));
        if (pxResult != null)
          return AbsenceHandling.ReplaceWith<PXResult<SOPickingWorksheet, SOPicker>>(pxResult);
      }
      return base.HandleAbsence(barcode);
    }

    protected override void Apply(PXResult<SOPickingWorksheet, SOPicker> pickList)
    {
      base.Apply(pickList);
      this.PPBasis.SingleShipmentNbr = ((PXResult) pickList).GetItem<SOPickingWorksheet>().SingleShipmentNbr;
    }

    protected override void ClearState()
    {
      base.ClearState();
      this.PPBasis.SingleShipmentNbr = (string) null;
    }

    protected override void ReportMissing(string barcode)
    {
      ((ScanComponent<PickPackShip>) this).Basis.ReportError("{0} pick list not found.", new object[1]
      {
        (object) barcode
      });
    }

    protected override void ReportSuccess(PXResult<SOPickingWorksheet, SOPicker> pickList)
    {
      ((ScanComponent<PickPackShip>) this).Basis.ReportInfo("{0} pick list loaded and ready to be processed.", new object[1]
      {
        (object) ((PXResult) pickList).GetItem<SOPickingWorksheet>().SingleShipmentNbr
      });
    }

    [PXLocalizable]
    public new abstract class Msg : WorksheetPicking.PickListState.Msg
    {
      public new const string Prompt = "Scan the pick list number, or click Next List.";
      public new const string Ready = "{0} pick list loaded and ready to be processed.";
      public new const string Missing = "{0} pick list not found.";
    }
  }

  public sealed class WarehouseState : 
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.WarehouseState
  {
    protected override bool UseDefaultWarehouse => true;

    protected virtual void SetNextState()
    {
      ((ScanComponent<PickPackShip>) this).Basis.SetDefaultState((string) null, Array.Empty<object>());
      ((ScanComponent<PickPackShip>) this).Basis.Get<PaperlessPicking.TakeNextPickListCommand.Logic>().TakeNext();
    }
  }

  public sealed class NearestLocationState : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.EntityState<INLocation>
  {
    public const string Value = "NLOC";

    public virtual string Code => "NLOC";

    protected virtual string StatePrompt => "Scan the nearest location.";

    public PaperlessPicking PPBasis
    {
      get => ((ScanComponent<PickPackShip>) this).Basis.Get<PaperlessPicking>();
    }

    protected virtual INLocation GetByBarcode(string barcode)
    {
      return PXResultset<INLocation>.op_Implicit(PXSelectBase<INLocation, PXViewOf<INLocation>.BasedOn<SelectFromBase<INLocation, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLocation.siteID, Equal<P.AsInt>>>>>.And<BqlOperand<INLocation.locationCD, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) ((ScanComponent<PickPackShip>) this).Basis), new object[2]
      {
        (object) ((ScanComponent<PickPackShip>) this).Basis.SiteID,
        (object) barcode
      }));
    }

    protected virtual Validation Validate(INLocation location)
    {
      if (location.Active.GetValueOrDefault())
        return Validation.Ok;
      return Validation.Fail("Location '{0}' is inactive", new object[1]
      {
        (object) location.LocationCD
      });
    }

    protected virtual void Apply(INLocation location)
    {
      this.PPBasis.LastVisitedLocationID = location.LocationID;
    }

    protected virtual void ClearState() => this.PPBasis.LastVisitedLocationID = new int?();

    protected virtual void ReportSuccess(INLocation location)
    {
    }

    protected virtual void ReportMissing(string barcode)
    {
      ((ScanComponent<PickPackShip>) this).Basis.Reporter.Error("{0} location not found in {1} warehouse.", new object[2]
      {
        (object) barcode,
        (object) ((ScanComponent<PickPackShip>) this).Basis.SightOf<WMSScanHeader.siteID>()
      });
    }

    protected virtual void SetNextState()
    {
      ((ScanComponent<PickPackShip>) this).Basis.SetDefaultState((string) null, Array.Empty<object>());
      ((ScanComponent<PickPackShip>) this).Basis.Get<PaperlessPicking.TakeNextPickListCommand.Logic>().TakeNext();
    }

    public class value : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      PaperlessPicking.NearestLocationState.value>
    {
      public value()
        : base("NLOC")
      {
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string Prompt = "Scan the nearest location.";
    }
  }

  public sealed class ConfirmState : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ConfirmationState
  {
    public virtual string Prompt
    {
      get
      {
        return ((ScanComponent<PickPackShip>) this).Basis.Localize("Confirm picking {0} x {1} {2}.", new object[3]
        {
          (object) ((ScanComponent<PickPackShip>) this).Basis.SightOf<WMSScanHeader.inventoryID>(),
          (object) ((ScanComponent<PickPackShip>) this).Basis.Qty,
          (object) ((ScanComponent<PickPackShip>) this).Basis.UOM
        });
      }
    }

    protected virtual FlowStatus PerformConfirmation()
    {
      return ((ScanComponent<PickPackShip>) this).Basis.Get<PaperlessPicking.ConfirmState.Logic>().Confirm();
    }

    public class Logic : PaperlessPicking.ScanExtension
    {
      public static bool IsActive() => PaperlessPicking.ScanExtension.IsActiveBase();

      public virtual FlowStatus Confirm()
      {
        this.PPBasis.EnsureLocationFromLastVisited();
        FlowStatus flowStatus = this.WSBasis.ConfirmSuitableSplits();
        bool? isError = ((FlowStatus) ref flowStatus).IsError;
        bool flag = false;
        if (!(isError.GetValueOrDefault() == flag & isError.HasValue))
          return flowStatus;
        SOPickerListEntry pickedSplit = ((PXCache) GraphHelper.Caches<SOPickerListEntry>((PXGraph) this.Graph)).Dirty.Cast<SOPickerListEntry>().First<SOPickerListEntry>();
        this.WSBasis.ReportSplitConfirmed(pickedSplit);
        this.PPBasis.EnsureShipmentUserLinkForPaperlessPick();
        this.VisitSplit(pickedSplit);
        return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
      }

      public virtual void VisitSplit(SOPickerListEntry pickedSplit)
      {
        this.VisitSplit(pickedSplit, false);
      }

      public virtual void VisitSplit(SOPickerListEntry pickedSplit, bool keepLocation)
      {
        int num = this.Basis.Remove.GetValueOrDefault() ? 1 : 0;
        if (!keepLocation)
          this.PPBasis.LastVisitedLocationID = pickedSplit.LocationID;
        this.PPBasis.WantedLineNbr = this.PPBasis.GetNextWantedLineNbr();
        SOPickerListEntry wantedSplit = this.PPBasis.GetWantedSplit();
        int? nullable;
        if (num == 0 && wantedSplit != null)
        {
          int? inventoryId = pickedSplit.InventoryID;
          nullable = wantedSplit.InventoryID;
          if (inventoryId.GetValueOrDefault() == nullable.GetValueOrDefault() & inventoryId.HasValue == nullable.HasValue)
            goto label_5;
        }
        this.Basis.Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>(true);
label_5:
        if (num == 0 && wantedSplit != null)
        {
          nullable = pickedSplit.LocationID;
          int? locationId = wantedSplit.LocationID;
          if (nullable.GetValueOrDefault() == locationId.GetValueOrDefault() & nullable.HasValue == locationId.HasValue)
            return;
        }
        this.Basis.Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>(true);
      }
    }
  }

  public sealed class TakeNextPickListCommand : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand
  {
    public virtual string Code => "NEXT*PICKLIST";

    public virtual string ButtonName => "scanTakeNextPickList";

    public virtual string DisplayName => "Next List";

    protected virtual bool IsEnabled
    {
      get
      {
        if (((ScanComponent<PickPackShip>) this).Basis.RefNbr != null)
          return false;
        return this.WSBasis.WorksheetNbr == null || ((ScanComponent<PickPackShip>) this).Basis.DocumentIsConfirmed || this.WSBasis.NotStarted;
      }
    }

    public WorksheetPicking WSBasis
    {
      get => ((ScanComponent<PickPackShip>) this).Basis.Get<WorksheetPicking>();
    }

    protected virtual bool Process()
    {
      return ((ScanComponent<PickPackShip>) this).Basis.Get<PaperlessPicking.TakeNextPickListCommand.Logic>().TakeNext();
    }

    public class Logic : PaperlessPicking.ScanExtension
    {
      public static bool IsActive() => PaperlessPicking.ScanExtension.IsActiveBase();

      public virtual bool TakeNext()
      {
        if (this.Basis.RefNbr == null && this.Basis.CurrentMode is PickPackShip.PickMode)
          this.Basis.SetScanMode<PaperlessPicking.SinglePickMode>();
        if (this.WSBasis.WorksheetNbr != null)
        {
          SOPickingJob current = ((PXSelectBase<SOPickingJob>) this.WSBasis.PickingJob).Current;
          if ((current != null ? (current.JobID.HasValue ? 1 : 0) : 0) != 0 && !this.Basis.DocumentIsConfirmed)
            this.PPBasis.IgnoredPickingJobs.Add(((PXSelectBase<SOPickingJob>) this.WSBasis.PickingJob).Current.JobID.Value);
        }
        int? nullable = this.PPBasis.LastVisitedLocationID;
        if (!nullable.HasValue)
        {
          this.Basis.ReportInfo("Your current picking location not defined.", Array.Empty<object>());
          nullable = this.Basis.SiteID;
          if (!nullable.HasValue)
            this.Basis.SetScanState<PaperlessPicking.WarehouseState>((string) null, Array.Empty<object>());
          else
            this.Basis.SetScanState<PaperlessPicking.NearestLocationState>((string) null, Array.Empty<object>());
          return true;
        }
        nullable = this.PPBasis.LastVisitedLocationID;
        if (this.TryTakeNext(nullable.Value))
          return true;
        if (this.PPBasis.IgnoredPickingJobs.Count != 0)
        {
          if (this.WSBasis.WorksheetNbr != null && this.PPBasis.ReturnCurrentJobToQueue())
            this.Basis.SaveChanges();
          this.PPBasis.IgnoredPickingJobs.Clear();
          this.Basis.Reset(true);
          this.Basis.SetDefaultState((string) null, Array.Empty<object>());
          this.Basis.ReportInfo("End of picking queue reached.", Array.Empty<object>());
        }
        else
          this.Basis.ReportInfo("The picking queue is empty.", Array.Empty<object>());
        return true;
      }

      public virtual bool TryTakeNext(int nearestLocationID)
      {
        return this.TryTakeIncomplete(nearestLocationID) || this.TryTakeDirectlyAssigned(nearestLocationID) || this.TryTakeFromSharedQueue(nearestLocationID);
      }

      public virtual bool TryTakeIncomplete(int nearestLocationID)
      {
        FbqlSelect<SelectFromBase<SOPickingJob, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPicker>.On<SOPickingJob.FK.Picker>>, FbqlJoins.Inner<SOPickingWorksheet>.On<SOPicker.FK.Worksheet>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingJob.actualAssigneeID, Equal<BqlField<AccessInfo.userID, IBqlGuid>.FromCurrent>>>>, And<BqlOperand<SOPickingJob.status, IBqlString>.IsIn<SOPickingJob.status.enqueued, SOPickingJob.status.reenqueued, SOPickingJob.status.picking>>>>.And<BqlOperand<SOPickingWorksheet.status, IBqlString>.IsIn<SOPickingWorksheet.status.open, SOPickingWorksheet.status.picking>>>.Order<By<Desc<TestIf<BqlOperand<SOPickingJob.status, IBqlString>.IsEqual<SOPickingJob.status.picking>>>>>, SOPickingJob>.View command = new FbqlSelect<SelectFromBase<SOPickingJob, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPicker>.On<SOPickingJob.FK.Picker>>, FbqlJoins.Inner<SOPickingWorksheet>.On<SOPicker.FK.Worksheet>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingJob.actualAssigneeID, Equal<BqlField<AccessInfo.userID, IBqlGuid>.FromCurrent>>>>, And<BqlOperand<SOPickingJob.status, IBqlString>.IsIn<SOPickingJob.status.enqueued, SOPickingJob.status.reenqueued, SOPickingJob.status.picking>>>>.And<BqlOperand<SOPickingWorksheet.status, IBqlString>.IsIn<SOPickingWorksheet.status.open, SOPickingWorksheet.status.picking>>>.Order<By<Desc<TestIf<BqlOperand<SOPickingJob.status, IBqlString>.IsEqual<SOPickingJob.status.picking>>>>>, SOPickingJob>.View(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis));
        this.ApplyCommonFilters((PXSelectBase<SOPickingJob>) command);
        foreach (PXResult<SOPickingJob, SOPicker, SOPickingWorksheet> selectedJob in ((PXSelectBase<SOPickingJob>) command).Select(Array.Empty<object>()))
        {
          if (!this.PPBasis.IgnoredPickingJobs.Contains(PXResult<SOPickingJob, SOPicker, SOPickingWorksheet>.op_Implicit(selectedJob).JobID.Value))
          {
            this.LoadPickingJob(selectedJob);
            return true;
          }
        }
        return false;
      }

      public virtual bool TryTakeDirectlyAssigned(int nearestLocationID)
      {
        FbqlSelect<SelectFromBase<SOPickingJob, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPicker>.On<SOPickingJob.FK.Picker>>, FbqlJoins.Inner<SOPickingWorksheet>.On<SOPicker.FK.Worksheet>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingJob.preferredAssigneeID, Equal<BqlField<AccessInfo.userID, IBqlGuid>.FromCurrent>>>>, And<BqlOperand<SOPickingJob.status, IBqlString>.IsEqual<SOPickingJob.status.assigned>>>, And<BqlOperand<SOPickingWorksheet.status, IBqlString>.IsIn<SOPickingWorksheet.status.open, SOPickingWorksheet.status.picking>>>>.And<BqlOperand<SOPickingJob.priority, IBqlInt>.IsEqual<P.AsInt>>>, SOPickingJob>.View view = new FbqlSelect<SelectFromBase<SOPickingJob, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPicker>.On<SOPickingJob.FK.Picker>>, FbqlJoins.Inner<SOPickingWorksheet>.On<SOPicker.FK.Worksheet>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingJob.preferredAssigneeID, Equal<BqlField<AccessInfo.userID, IBqlGuid>.FromCurrent>>>>, And<BqlOperand<SOPickingJob.status, IBqlString>.IsEqual<SOPickingJob.status.assigned>>>, And<BqlOperand<SOPickingWorksheet.status, IBqlString>.IsIn<SOPickingWorksheet.status.open, SOPickingWorksheet.status.picking>>>>.And<BqlOperand<SOPickingJob.priority, IBqlInt>.IsEqual<P.AsInt>>>, SOPickingJob>.View(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis));
        this.ApplyCommonFilters((PXSelectBase<SOPickingJob>) view);
        PXResult<SOPickingJob, SOPicker, SOPickingWorksheet> selectedJob = this.SelectJobFrom((PXSelectBase<SOPickingJob>) view, nearestLocationID);
        if (selectedJob == null)
          return false;
        this.LoadPickingJob(selectedJob);
        return true;
      }

      public virtual bool TryTakeFromSharedQueue(int nearestLocationID)
      {
        FbqlSelect<SelectFromBase<SOPickingJob, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPicker>.On<SOPickingJob.FK.Picker>>, FbqlJoins.Inner<SOPickingWorksheet>.On<SOPicker.FK.Worksheet>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingJob.preferredAssigneeID, IsNull>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingJob.actualAssigneeID, IsNull>>>>.Or<BqlOperand<WMSJob.minutesSinceLastModification, IBqlInt>.IsGreater<PaperlessPicking.minutes15>>>>, And<BqlOperand<SOPickingJob.status, IBqlString>.IsIn<SOPickingJob.status.enqueued, SOPickingJob.status.reenqueued>>>, And<BqlOperand<SOPickingWorksheet.status, IBqlString>.IsIn<SOPickingWorksheet.status.open, SOPickingWorksheet.status.picking>>>>.And<BqlOperand<SOPickingJob.priority, IBqlInt>.IsEqual<P.AsInt>>>, SOPickingJob>.View view = new FbqlSelect<SelectFromBase<SOPickingJob, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPicker>.On<SOPickingJob.FK.Picker>>, FbqlJoins.Inner<SOPickingWorksheet>.On<SOPicker.FK.Worksheet>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingJob.preferredAssigneeID, IsNull>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickingJob.actualAssigneeID, IsNull>>>>.Or<BqlOperand<WMSJob.minutesSinceLastModification, IBqlInt>.IsGreater<PaperlessPicking.minutes15>>>>, And<BqlOperand<SOPickingJob.status, IBqlString>.IsIn<SOPickingJob.status.enqueued, SOPickingJob.status.reenqueued>>>, And<BqlOperand<SOPickingWorksheet.status, IBqlString>.IsIn<SOPickingWorksheet.status.open, SOPickingWorksheet.status.picking>>>>.And<BqlOperand<SOPickingJob.priority, IBqlInt>.IsEqual<P.AsInt>>>, SOPickingJob>.View(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis));
        this.ApplyCommonFilters((PXSelectBase<SOPickingJob>) view);
        PXResult<SOPickingJob, SOPicker, SOPickingWorksheet> selectedJob = this.SelectJobFrom((PXSelectBase<SOPickingJob>) view, nearestLocationID);
        if (selectedJob == null)
          return false;
        this.LoadPickingJob(selectedJob);
        return true;
      }

      protected virtual void ApplyCommonFilters(PXSelectBase<SOPickingJob> command)
      {
      }

      protected virtual PXResult<SOPickingJob, SOPicker, SOPickingWorksheet> SelectJobFrom(
        PXSelectBase<SOPickingJob> queue,
        int nearestLocationID)
      {
        int[] numArray = new int[4]{ 4, 3, 2, 1 };
        foreach (int num in numArray)
        {
          List<(SOPickingJob, SOPicker, SOPickingWorksheet, int)> source = new List<(SOPickingJob, SOPicker, SOPickingWorksheet, int)>();
          foreach (PXResult<SOPickingJob, SOPicker, SOPickingWorksheet> pxResult in queue.Select(new object[1]
          {
            (object) num
          }))
          {
            SOPickingJob soPickingJob1;
            SOPicker soPicker;
            SOPickingWorksheet pickingWorksheet1;
            pxResult.Deconstruct(ref soPickingJob1, ref soPicker, ref pickingWorksheet1);
            SOPickingJob soPickingJob2 = soPickingJob1;
            SOPicker picker = soPicker;
            SOPickingWorksheet pickingWorksheet2 = pickingWorksheet1;
            if (!this.PPBasis.IgnoredPickingJobs.Contains(soPickingJob2.JobID.Value))
            {
              int distance = getDistance(picker);
              source.Add((soPickingJob2, picker, pickingWorksheet2, distance));
            }
          }
          if (source.Count > 0)
          {
            (SOPickingJob, SOPicker, SOPickingWorksheet, int) tuple = source.OrderBy<(SOPickingJob, SOPicker, SOPickingWorksheet, int), int>((Func<(SOPickingJob, SOPicker, SOPickingWorksheet, int), int>) (r => r.Distance)).ThenBy<(SOPickingJob, SOPicker, SOPickingWorksheet, int), DateTime?>((Func<(SOPickingJob, SOPicker, SOPickingWorksheet, int), DateTime?>) (r => r.Job.EnqueuedAt)).First<(SOPickingJob, SOPicker, SOPickingWorksheet, int)>();
            return new PXResult<SOPickingJob, SOPicker, SOPickingWorksheet>(tuple.Item1, tuple.Item2, tuple.Item3);
          }
        }
        return (PXResult<SOPickingJob, SOPicker, SOPickingWorksheet>) null;

        int getDistance(SOPicker picker)
        {
          int betweenLocations1 = this.PPBasis.GetDistanceBetweenLocations(picker.FirstLocationID, new int?(nearestLocationID));
          if (!((PXSelectBase<SOPickPackShipSetup>) this.Basis.Setup).Current.AllowBidirectionalPickLists.GetValueOrDefault())
            return betweenLocations1;
          int betweenLocations2 = this.PPBasis.GetDistanceBetweenLocations(picker.LastLocationID, new int?(nearestLocationID));
          return Math.Min(betweenLocations1, betweenLocations2);
        }
      }

      protected virtual void LoadPickingJob(
        PXResult<SOPickingJob, SOPicker, SOPickingWorksheet> selectedJob)
      {
        if (selectedJob == null)
          throw new ArgumentNullException(nameof (selectedJob));
        bool flag = false;
        Exception exception = (Exception) null;
        SOPickingJob soPickingJob1;
        SOPicker soPicker1;
        SOPickingWorksheet pickingWorksheet;
        selectedJob.Deconstruct(ref soPickingJob1, ref soPicker1, ref pickingWorksheet);
        SOPickingJob soPickingJob2 = soPickingJob1;
        SOPicker soPicker2 = soPicker1;
        SOPickingWorksheet sheet = pickingWorksheet;
        try
        {
          IScanMode modeForWorksheet = (IScanMode) this.WSBasis.FindModeForWorksheet(sheet);
          if (modeForWorksheet == null || !this.Basis.FindMode(modeForWorksheet.Code).TryProcessBy<WorksheetPicking.PickListState>(soPicker2.PickListNbr, (StateSubstitutionRule) 0))
            return;
          PXResult<SOPickingWorksheet, SOPicker> pickList = ((PXSelectBase<SOPicker>) this.WSBasis.Picker).Current != null ? new PXResult<SOPickingWorksheet, SOPicker>(((PXSelectBase<SOPickingWorksheet>) this.WSBasis.Worksheet).Current, ((PXSelectBase<SOPicker>) this.WSBasis.Picker).Current) : (PXResult<SOPickingWorksheet, SOPicker>) null;
          if (this.Basis.CurrentMode.Code != modeForWorksheet.Code)
          {
            this.Basis.SetScanMode(modeForWorksheet.Code);
          }
          else
          {
            this.Basis.Reset(true);
            this.Basis.SetDefaultState((string) null, Array.Empty<object>());
          }
          if (pickList != null)
            this.WSBasis.SetPickList(pickList);
          flag = ((ScanState<PickPackShip>) this.Basis.FindState<WorksheetPicking.PickListState>(false)).Process(soPicker2.PickListNbr);
        }
        catch (Exception ex)
        {
          exception = ex;
        }
        finally
        {
          if (!flag)
          {
            this.PPBasis.IgnoredPickingJobs.Add(soPickingJob2.JobID.Value);
            if (exception != null)
              PXTrace.WriteError(exception);
            else if (((PXSelectBase<ScanInfo>) this.Basis.Info).Current.MessageType == "ERR")
              PXTrace.WriteError(((PXSelectBase<ScanInfo>) this.Basis.Info).Current.Message);
            this.Basis.ReportError("The {0} pick list has been skipped because of an error. See the trace for details.", new object[1]
            {
              (object) soPickingJob2.PickListNbr
            });
          }
        }
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string DisplayName = "Next List";
      public const string NearestLocationIsNotSet = "Your current picking location not defined.";
      public const string QueueIsEmpty = "The picking queue is empty.";
      public const string QueueEnded = "End of picking queue reached.";
      public const string PickListSkipped = "The {0} pick list has been skipped because of an error. See the trace for details.";
    }
  }

  public sealed class ConfirmPickListAndTakeNextCommand : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand
  {
    private bool _inProcess;

    public virtual string Code => "CONFIRM*PICK*AND*NEXT";

    public virtual string ButtonName => "scanConfirmPickListAndTakeNext";

    public virtual string DisplayName => "Finish and Next";

    protected virtual bool IsEnabled
    {
      get
      {
        return ((ScanCommand<PickPackShip>) ((ScanComponent<PickPackShip>) this).Basis.CurrentMode.Commands.OfType<WorksheetPicking.ConfirmPickListCommand>().First<WorksheetPicking.ConfirmPickListCommand>()).IsApplicable;
      }
    }

    protected virtual bool Process()
    {
      try
      {
        this._inProcess = true;
        return ((ScanCommand<PickPackShip>) ((ScanComponent<PickPackShip>) this).Basis.CurrentMode.Commands.OfType<WorksheetPicking.ConfirmPickListCommand>().First<WorksheetPicking.ConfirmPickListCommand>()).Execute();
      }
      finally
      {
        this._inProcess = false;
      }
    }

    /// Overrides <see cref="T:PX.Objects.SO.WMS.WorksheetPicking.ConfirmPickListCommand.Logic" />
    public class AlterConfirmPickListCommandLogic : 
      WorksheetPicking.ScanExtension<WorksheetPicking.ConfirmPickListCommand.Logic>
    {
      public static bool IsActive() => PaperlessPicking.IsActive();

      [PXOverride]
      public void ConfigureOnSuccessAction(
        ScanLongRunAwaiter<PickPackShip, SOPicker>.IResultProcessor onSuccess,
        Action<ScanLongRunAwaiter<PickPackShip, SOPicker>.IResultProcessor> base_ConfigureOnSuccessAction)
      {
        base_ConfigureOnSuccessAction(onSuccess);
        PaperlessPicking.ConfirmPickListAndTakeNextCommand andTakeNextCommand = this.Basis.CurrentMode.Commands.OfType<PaperlessPicking.ConfirmPickListAndTakeNextCommand>().FirstOrDefault<PaperlessPicking.ConfirmPickListAndTakeNextCommand>();
        if ((andTakeNextCommand != null ? (andTakeNextCommand._inProcess ? 1 : 0) : 0) == 0)
          return;
        onSuccess.Do((Action<PickPackShip, SOPicker>) ((basis, picker) => ((ScanCommand<PickPackShip>) basis.CurrentMode.Commands.OfType<PaperlessPicking.TakeNextPickListCommand>().First<PaperlessPicking.TakeNextPickListCommand>()).Execute()));
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string DisplayName = "Finish and Next";
    }
  }

  public sealed class ConfirmLineQtyCommand : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand
  {
    public virtual string Code => "CONFIRM*LINE*QTY";

    public virtual string ButtonName => "scanConfirmLineQty";

    public virtual string DisplayName => "Confirm Line Quantity";

    protected virtual bool IsEnabled
    {
      get
      {
        return ((ScanComponent<PickPackShip>) this).Basis.DocumentIsEditable && !((ScanComponent<PickPackShip>) this).Basis.Remove.GetValueOrDefault() && ((ScanComponent<PickPackShip>) this).Basis.Get<PaperlessPicking>().WantedLineNbr.HasValue;
      }
    }

    protected virtual bool Process()
    {
      return this.Get<PaperlessPicking.ConfirmLineQtyCommand.Logic>().ConfirmQtyOfWantedSplit();
    }

    public class Logic : PaperlessPicking.ScanExtension
    {
      public PXAction<ScanHeader> ReopenLineQty;

      public static bool IsActive() => PaperlessPicking.ScanExtension.IsActiveBase();

      [PXButton(CommitChanges = true, DisplayOnMainToolbar = false)]
      [PXUIField(DisplayName = "Proceed Picking")]
      protected virtual void reopenLineQty()
      {
        this.Basis.Get<PaperlessPicking.ConfirmLineQtyCommand.Logic>().ReopenQtyOfCurrentSplit();
      }

      public virtual bool ConfirmQtyOfWantedSplit()
      {
        SOPickerListEntry wantedSplit = this.PPBasis.GetWantedSplit();
        if (wantedSplit == null)
          return false;
        this.SetForceCompletedOfSplit(wantedSplit, true);
        return true;
      }

      public virtual bool ReopenQtyOfCurrentSplit()
      {
        SOPickerListEntry current = ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).Current;
        if (current == null || !current.ForceCompleted.GetValueOrDefault())
          return false;
        this.SetForceCompletedOfSplit(current, false);
        return true;
      }

      public virtual void SetForceCompletedOfSplit(SOPickerListEntry entry, bool value)
      {
        entry.ForceCompleted = new bool?(value);
        ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).Update(entry);
        this.Basis.SaveChanges();
        this.Basis.ReportInfo(value ? "Line quantity confirmed." : "You can proceed to pick the {0} item.", new object[1]
        {
          (object) this.Basis.SightOf<SOPickerListEntry.inventoryID>((IBqlTable) entry)
        });
        this.Basis.Get<PaperlessPicking.ConfirmState.Logic>().VisitSplit(entry, !value);
        this.Basis.Reset(false);
        this.Basis.SetDefaultState((string) null, Array.Empty<object>());
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string DisplayName = "Confirm Line Quantity";
      public const string LineQtyConfirmed = "Line quantity confirmed.";
      public const string LineQtyReopened = "You can proceed to pick the {0} item.";
    }
  }

  /// Overrides <see cref="T:PX.Objects.SO.WMS.ToteSupport" />
  public class AlterToteSupport : WorksheetPicking.ScanExtension<ToteSupport>
  {
    public static bool IsActive() => PaperlessPicking.IsActive();

    /// Overrides <see cref="M:PX.Objects.SO.WMS.ToteSupport.GetShipmentToAddToteTo" />
    [PXOverride]
    public string GetShipmentToAddToteTo(Func<string> base_GetShipmentToAddToteTo)
    {
      return this.Basis.CurrentMode is PaperlessPicking.SinglePickMode ? this.Basis.Get<PaperlessPicking>().SingleShipmentNbr : base_GetShipmentToAddToteTo();
    }

    /// Overrides <see cref="P:PX.Objects.SO.WMS.ToteSupport.IsToteSelectionNeeded" />
    [PXOverride]
    public bool get_IsToteSelectionNeeded(Func<bool> base_IsToteSelectionNeeded)
    {
      return this.Basis.CurrentMode is PaperlessPicking.SinglePickMode ? ((IEnumerable<SOPickerListEntry>) ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).SelectMain(Array.Empty<object>())).Where<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (s =>
      {
        Decimal? pickedQty = s.PickedQty;
        Decimal num = 0M;
        return pickedQty.GetValueOrDefault() > num & pickedQty.HasValue && !s.ForceCompleted.GetValueOrDefault();
      })).GroupBy<SOPickerListEntry, int?>((Func<SOPickerListEntry, int?>) (s => s.ToteID)).Count<IGrouping<int?, SOPickerListEntry>>() > 1 : base_IsToteSelectionNeeded();
    }

    /// Overrides <see cref="M:PX.Objects.SO.WMS.ToteSupport.MoveSplitRestQtyToAnotherTote(PX.Objects.SO.SOPickerListEntry,System.Nullable{System.Int32})" />
    [PXOverride]
    public SOPickerListEntry MoveSplitRestQtyToAnotherTote(
      SOPickerListEntry split,
      int? toteID,
      Func<SOPickerListEntry, int?, SOPickerListEntry> base_MoveSplitRestQtyToAnotherTote)
    {
      split = base_MoveSplitRestQtyToAnotherTote(split, toteID);
      if (this.Basis.CurrentMode is PaperlessPicking.SinglePickMode)
      {
        PaperlessPicking paperlessPicking = this.Basis.Get<PaperlessPicking>();
        paperlessPicking.WantedLineNbr = paperlessPicking.GetNextWantedLineNbr();
      }
      return split;
    }

    [PXOverride]
    public ScanCommand<PickPackShip> DecorateScanCommand(
      ScanCommand<PickPackShip> original,
      Func<ScanCommand<PickPackShip>, ScanCommand<PickPackShip>> base_DecorateScanCommand)
    {
      ScanCommand<PickPackShip> scanCommand = base_DecorateScanCommand(original);
      if (((ScanComponent<PickPackShip>) scanCommand).ModeCode == "SNGL" && scanCommand is WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.RemoveCommand remove)
      {
        this.Target.InjectRemoveDisableWhenAssignTote(remove);
        this.Target.InjectRemoveMovesToRemoveFromTote(remove);
      }
      return scanCommand;
    }
  }

  /// Overrides <see cref="T:PX.Objects.SO.WMS.WorksheetPicking.ConfirmPickListCommand.Logic" />
  public class AlterConfirmPickListCommandLogic : 
    WorksheetPicking.ScanExtension<WorksheetPicking.ConfirmPickListCommand.Logic>
  {
    public static bool IsActive() => PaperlessPicking.IsActive();

    /// Overrides <see cref="M:PX.Objects.SO.WMS.WorksheetPicking.ConfirmPickListCommand.Logic.ConfirmPickList(PX.Objects.SO.SOPickingWorksheet,PX.Objects.SO.SOPicker,System.Nullable{System.Int32},System.Threading.CancellationToken)" />
    [PXOverride]
    public async System.Threading.Tasks.Task ConfirmPickList(
      SOPickingWorksheet worksheet,
      SOPicker pickList,
      int? sortingLocationID,
      CancellationToken cancellationToken,
      Func<SOPickingWorksheet, SOPicker, int?, CancellationToken, System.Threading.Tasks.Task> base_ConfirmPickList)
    {
      PaperlessPicking.AlterConfirmPickListCommandLogic listCommandLogic = this;
      await base_ConfirmPickList(worksheet, pickList, sortingLocationID, cancellationToken);
      if (worksheet.WorksheetType != "SS" || ((SOPickingWorksheet) PrimaryKeyOf<SOPickingWorksheet>.By<SOPickingWorksheet.worksheetNbr>.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) listCommandLogic.Basis), (SOPickingWorksheet.worksheetNbr) worksheet, (PKFindOptions) 0)).Status != "P" || !KeysRelation<Field<SOPickingJob.worksheetNbr>.IsRelatedTo<SOPickingWorksheet.worksheetNbr>.AsSimpleKey.WithTablesOf<SOPickingWorksheet, SOPickingJob>, SOPickingWorksheet, SOPickingJob>.SelectChildren(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) listCommandLogic.Basis), worksheet).First<SOPickingJob>().AutomaticShipmentConfirmation.GetValueOrDefault())
        return;
      await ((PXGraph) PXGraph.CreateInstance<PickPackShip.Host>()).GetExtension<WorksheetPicking>().TryConfirmShipmentRightAfterPickList(worksheet.SingleShipmentNbr, (SOPackageDetailEx) null, cancellationToken);
    }
  }

  [PXLocalizable]
  public abstract class Msg
  {
    public const string CannotReturnCurrentListToQueue = "The {0} pick list cannot be returned to the picking queue because it is in progress.";
    public const string PickingJobAlreadyTaken = "The {0} pick list cannot be processed because it is already assigned to another picker.";
    public const string PickingJobWrongStatus = "The {0} pick list cannot be processed because it has the {1} status.";
    public const string GoToLocation = "Go to the {0} location.";
    public const string PickItemNoLotSerial = "Pick {2}, quantity: {0} {1} left.";
    public const string PickItemWithLotSerial = "Pick {2}, lot or serial: {3}, quantity: {0} {1} left.";
    public const string PickItemFromLocationNoLotSerial = "Go to {4} and pick {2}, quantity: {0} {1} left.";
    public const string PickItemFromLocationWithLotSerial = "Go to {4} and pick {2}, lot or serial: {3}, quantity: {0} {1} left.";
    public const string WrongLocation = "Incorrect location {0} scanned.";
    public const string WrongItem = "Incorrect item {0} scanned.";
    public const string WrongLotSerial = "Incorrect lot or serial number {0} scanned.";
    public const string InventoryAdded = "{0} x {1} {2} added to tote.";
    public const string InventoryRemoved = "{0} x {1} {2} removed from tote.";
  }

  public abstract class ScanExtension : 
    PXGraphExtension<PaperlessPicking, WorksheetPicking, PickPackShip, PickPackShip.Host>
  {
    protected static bool IsActiveBase() => PaperlessPicking.IsActive();

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

    public PaperlessPicking PPBasis
    {
      [DebuggerStepThrough, DebuggerStepperBoundary] get => this.Base3;
    }
  }

  public abstract class ScanExtension<TTargetExtension> : 
    PXGraphExtension<TTargetExtension, PaperlessPicking, WorksheetPicking, PickPackShip, PickPackShip.Host>
    where TTargetExtension : PXGraphExtension<PaperlessPicking, WorksheetPicking, PickPackShip, PickPackShip.Host>
  {
    protected static bool IsActiveBase() => PaperlessPicking.IsActive();

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

    public PaperlessPicking PPBasis
    {
      [DebuggerStepThrough, DebuggerStepperBoundary] get
      {
        return ((PXGraphExtension<PaperlessPicking, WorksheetPicking, PickPackShip, PickPackShip.Host>) this).Base3;
      }
    }

    public TTargetExtension Target
    {
      [DebuggerStepThrough, DebuggerStepperBoundary] get => this.Base4;
    }
  }

  public class minutes15 : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  PaperlessPicking.minutes15>
  {
    public minutes15()
      : base(15)
    {
    }
  }
}
