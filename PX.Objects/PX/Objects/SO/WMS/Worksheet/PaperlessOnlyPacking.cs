// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.WMS.Worksheet.PaperlessOnlyPacking
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
using PX.Objects.IN;
using PX.Objects.IN.WMS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable enable
namespace PX.Objects.SO.WMS.Worksheet;

public class PaperlessOnlyPacking : PaperlessPicking.ScanExtension
{
  [Obsolete]
  [PXInternalUseOnly]
  public 
  #nullable disable
  PXAction<ScanHeader> ReviewPackLines;

  public static bool IsActive() => PaperlessPicking.ScanExtension.IsActiveBase();

  public bool IsPackOnly => !this.Basis.HasPick && this.Basis.HasPack;

  [PXOverride]
  public IEnumerable<ScanMode<PickPackShip>> CreateScanModes(
    Func<IEnumerable<ScanMode<PickPackShip>>> base_CreateScanModes)
  {
    foreach (ScanMode<PickPackShip> scanMode in base_CreateScanModes())
      yield return scanMode;
    yield return (ScanMode<PickPackShip>) new PaperlessOnlyPacking.PaperlessPackOnlyMode();
  }

  [PXButton]
  [PXUIField(DisplayName = "Pick Review")]
  [Obsolete]
  [PXInternalUseOnly]
  protected IEnumerable reviewPackLines(PXAdapter adapter) => adapter.Get();

  [Obsolete]
  [PXInternalUseOnly]
  protected void _(PX.Data.Events.RowSelected<ScanHeader> e)
  {
    if (e.Row == null)
      return;
    ((PXAction) this.ReviewPackLines).SetVisible(((PXGraph) ((PXGraphExtension<PickPackShip.Host>) this).Base).IsMobile && e.Row.Mode == "PPAO");
  }

  public virtual void InjectPickListHandleAbsenceByPackShipment(
    PaperlessPicking.PickListState pickList)
  {
    ((MethodInterceptor<EntityState<PickPackShip, PXResult<SOPickingWorksheet, SOPicker>>, PickPackShip>.OfFunc<string, AbsenceHandling.Of<PXResult<SOPickingWorksheet, SOPicker>>>.AsAppendable) ((EntityState<PickPackShip, PXResult<SOPickingWorksheet, SOPicker>>) pickList).Intercept.HandleAbsence).ByAppend((Func<AbsenceHandling.Of<PXResult<SOPickingWorksheet, SOPicker>>, string, AbsenceHandling.Of<PXResult<SOPickingWorksheet, SOPicker>>>) ((basis, barcode) =>
    {
      PickPackShip.PackMode mode = basis.FindMode<PickPackShip.PackMode>();
      if (mode == null || !((ScanMode<PickPackShip>) mode).IsActive || !((ScanMode<PickPackShip>) mode).TryProcessBy<PickPackShip.PackMode.ShipmentState>(barcode, (StateSubstitutionRule) 0))
        return AbsenceHandling.Of<PXResult<SOPickingWorksheet, SOPicker>>.op_Implicit(AbsenceHandling.Skipped);
      basis.SetScanMode<PickPackShip.PackMode>();
      ((ScanState<PickPackShip>) basis.FindState<PickPackShip.PackMode.ShipmentState>(false)).Process(barcode);
      return AbsenceHandling.Of<PXResult<SOPickingWorksheet, SOPicker>>.op_Implicit(AbsenceHandling.Done);
    }), new RelativeInject?());
  }

  public virtual void InjectPickListShipmentValidation(PaperlessPicking.PickListState pickListState)
  {
    ((MethodInterceptor<EntityState<PickPackShip, PXResult<SOPickingWorksheet, SOPicker>>, PickPackShip>.OfFunc<PXResult<SOPickingWorksheet, SOPicker>, Validation>.AsAppendable) ((EntityState<PickPackShip, PXResult<SOPickingWorksheet, SOPicker>>) pickListState).Intercept.Validate).ByAppend((Func<Validation, PXResult<SOPickingWorksheet, SOPicker>, Validation>) ((basis, pickList) =>
    {
      PX.Objects.SO.SOShipment parent = (PX.Objects.SO.SOShipment) PrimaryKeyOf<PX.Objects.SO.SOShipment>.By<PX.Objects.SO.SOShipment.shipmentNbr>.ForeignKeyOf<SOPickingWorksheet>.By<SOPickingWorksheet.singleShipmentNbr>.FindParent(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) basis), (SOPickingWorksheet.singleShipmentNbr) PXResult<SOPickingWorksheet, SOPicker>.op_Implicit(pickList), (PKFindOptions) 0);
      return (Validation?) ((ScanMode<PickPackShip>) basis.FindMode<PickPackShip.PackMode>())?.TryValidate<PX.Objects.SO.SOShipment>(parent).By<PickPackShip.PackMode.ShipmentState>() ?? Validation.Ok;
    }), new RelativeInject?());
  }

  public virtual void InjectPickListDispatchToCommandStateOnCantPack(
    PaperlessPicking.PickListState pickListState)
  {
    ((MethodInterceptor<EntityState<PickPackShip, PXResult<SOPickingWorksheet, SOPicker>>, PickPackShip>.OfAction) ((EntityState<PickPackShip, PXResult<SOPickingWorksheet, SOPicker>>) pickListState).Intercept.SetNextState).ByReplace((Action<PickPackShip>) (basis =>
    {
      PickPackShip.PackMode.Logic logic = basis.Get<PickPackShip.PackMode.Logic>();
      if (basis.Remove.GetValueOrDefault() || logic.CanPack || logic.HasConfirmableBoxes)
      {
        basis.DispatchNext((string) null, Array.Empty<object>());
      }
      else
      {
        basis.ReportInfo("{0} {1}", new object[2]
        {
          (object) ((PXSelectBase<ScanInfo>) basis.Info).Current.Message,
          (object) this.Basis.Localize("{0} shipment packed.", new object[1]
          {
            (object) basis.RefNbr
          })
        });
        basis.SetScanState("NONE", (string) null, Array.Empty<object>());
      }
    }), new RelativeInject?());
  }

  public virtual void InjectShipmentAbsenceHandlingByWorksheetOfSingleType(
    PickPackShip.PackMode.ShipmentState packShipment)
  {
    ((MethodInterceptor<EntityState<PickPackShip, PX.Objects.SO.SOShipment>, PickPackShip>.OfFunc<string, AbsenceHandling.Of<PX.Objects.SO.SOShipment>>.AsAppendable) ((EntityState<PickPackShip, PX.Objects.SO.SOShipment>) packShipment).Intercept.HandleAbsence).ByPrepend((Func<AbsenceHandling.Of<PX.Objects.SO.SOShipment>, string, AbsenceHandling.Of<PX.Objects.SO.SOShipment>>) ((basis, barcode) =>
    {
      if (!barcode.Contains("/"))
      {
        PaperlessOnlyPacking.PaperlessPackOnlyMode mode = basis.FindMode<PaperlessOnlyPacking.PaperlessPackOnlyMode>();
        if (mode != null && ((ScanMode<PickPackShip>) mode).IsActive && ((ScanMode<PickPackShip>) mode).TryProcessBy<WorksheetPicking.PickListState>(barcode, (StateSubstitutionRule) 1))
        {
          basis.SetScanMode<PaperlessOnlyPacking.PaperlessPackOnlyMode>();
          ((ScanState<PickPackShip>) basis.FindState<WorksheetPicking.PickListState>(false)).Process(barcode);
          return AbsenceHandling.Of<PX.Objects.SO.SOShipment>.op_Implicit(AbsenceHandling.Done);
        }
      }
      return AbsenceHandling.Of<PX.Objects.SO.SOShipment>.op_Implicit(AbsenceHandling.Skipped);
    }), new RelativeInject?());
  }

  public virtual void InjectItemPromptForPackageConfirmOnPaperlessPack(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState itemState)
  {
    ((MethodInterceptor<EntityState<PickPackShip, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, PickPackShip>.OfFunc<string>) ((EntityState<PickPackShip, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>) itemState).Intercept.StatePrompt).ByOverride((Func<PickPackShip, Func<string>, string>) ((basis, base_StatePrompt) => basis.Get<PickPackShip.PackMode.Logic>().With<PickPackShip.PackMode.Logic, string>((Func<PickPackShip.PackMode.Logic, string>) (mode => basis.Remove.GetValueOrDefault() || !mode.CanConfirmPackage ? (string) null : "Confirm the package, or scan the next item.")) ?? base_StatePrompt()), new RelativeInject?());
  }

  public virtual void InjectLocationPromptForPackageConfirmOnPaperlessPack(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState locationState)
  {
    ((MethodInterceptor<EntityState<PickPackShip, INLocation>, PickPackShip>.OfFunc<string>) ((EntityState<PickPackShip, INLocation>) locationState).Intercept.StatePrompt).ByOverride((Func<PickPackShip, Func<string>, string>) ((basis, base_StatePrompt) => basis.Get<PickPackShip.PackMode.Logic>().With<PickPackShip.PackMode.Logic, string>((Func<PickPackShip.PackMode.Logic, string>) (mode => basis.Remove.GetValueOrDefault() || !mode.CanConfirmPackage ? (string) null : "Confirm the package, or scan the next location.")) ?? base_StatePrompt()), new RelativeInject?());
  }

  public virtual void InjectLocationAbsenceHandlingByBox(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState locationState)
  {
    ((MethodInterceptor<EntityState<PickPackShip, INLocation>, PickPackShip>.OfFunc<string, AbsenceHandling.Of<INLocation>>.AsAppendable) ((EntityState<PickPackShip, INLocation>) locationState).Intercept.HandleAbsence).ByAppend((Func<AbsenceHandling.Of<INLocation>, string, AbsenceHandling.Of<INLocation>>) ((basis, barcode) =>
    {
      bool? nullable = basis.Get<PickPackShip.PackMode.Logic>().TryAutoConfirmCurrentPackageAndLoadNext(barcode);
      bool flag = false;
      return AbsenceHandling.Of<INLocation>.op_Implicit(nullable.GetValueOrDefault() == flag & nullable.HasValue ? AbsenceHandling.Skipped : AbsenceHandling.Done);
    }), new RelativeInject?());
  }

  public virtual void InjectConfirmCombinedFromPackAndWorksheet(
    PickPackShip.PackMode.ConfirmState confirmState)
  {
    ((MethodInterceptor<ConfirmationState<PickPackShip>, PickPackShip>.OfFunc<FlowStatus>) ((ConfirmationState<PickPackShip>) confirmState).Intercept.PerformConfirmation).ByOverride((Func<PickPackShip, Func<FlowStatus>, FlowStatus>) ((basis, base_PerformConfirmation) =>
    {
      basis.Get<PaperlessPicking>().EnsureLocationFromLastVisited();
      FlowStatus flowStatus1 = basis.Get<WorksheetPicking>().ConfirmSuitableSplits();
      bool? isError1 = ((FlowStatus) ref flowStatus1).IsError;
      bool flag1 = false;
      if (!(isError1.GetValueOrDefault() == flag1 & isError1.HasValue))
        return flowStatus1;
      SOPickerListEntry pickedSplit = ((PXCache) GraphHelper.Caches<SOPickerListEntry>((PXGraph) basis.Graph)).Dirty.Cast<SOPickerListEntry>().First<SOPickerListEntry>();
      FlowStatus flowStatus2 = base_PerformConfirmation();
      bool? isError2 = ((FlowStatus) ref flowStatus2).IsError;
      bool flag2 = false;
      if (!(isError2.GetValueOrDefault() == flag2 & isError2.HasValue))
        return ((FlowStatus) ref flowStatus2).WithChangesDiscard;
      basis.Get<PaperlessPicking.ConfirmState.Logic>().VisitSplit(pickedSplit);
      return flowStatus2;
    }), new RelativeInject?());
  }

  public virtual void InjectTakeNextEnablingForPaperlessPackOnly(
    PaperlessPicking.TakeNextPickListCommand takeNext)
  {
    ((MethodInterceptor<ScanCommand<PickPackShip>, PickPackShip>.OfPredicate) ((ScanCommand<PickPackShip>) takeNext).Intercept.IsEnabled).ByDisjoin((Func<PickPackShip, bool>) (basis =>
    {
      if (!(basis.CurrentMode is PaperlessOnlyPacking.PaperlessPackOnlyMode))
        return false;
      return basis.RefNbr == null || basis.DocumentIsConfirmed || basis.Get<WorksheetPicking>().NotStarted;
    }), false, new RelativeInject?());
  }

  [PXOverride]
  public ScanMode<PickPackShip> DecorateScanMode(
    ScanMode<PickPackShip> original,
    Func<ScanMode<PickPackShip>, ScanMode<PickPackShip>> base_DecorateScanMode)
  {
    ScanMode<PickPackShip> scanMode = base_DecorateScanMode(original);
    if (!(scanMode is PickPackShip.PackMode packMode) || !this.IsPackOnly)
      return scanMode;
    ((MethodInterceptor<ScanMode<PickPackShip>, PickPackShip>.OfFunc<IEnumerable<ScanCommand<PickPackShip>>>.AsAppendable) ((ScanMode<PickPackShip>) packMode).Intercept.CreateCommands).ByAppend((Func<PickPackShip, IEnumerable<ScanCommand<PickPackShip>>>) (basis => (IEnumerable<ScanCommand<PickPackShip>>) new PaperlessPicking.TakeNextPickListCommand[1]
    {
      new PaperlessPicking.TakeNextPickListCommand()
    }), new RelativeInject?());
    return scanMode;
  }

  [PXOverride]
  public ScanState<PickPackShip> DecorateScanState(
    ScanState<PickPackShip> original,
    Func<ScanState<PickPackShip>, ScanState<PickPackShip>> base_DecorateScanState)
  {
    ScanState<PickPackShip> scanState = base_DecorateScanState(original);
    if (((ScanComponent<PickPackShip>) scanState).ModeCode == "PPAO")
    {
      switch (scanState)
      {
        case PaperlessPicking.PickListState pickListState:
          this.InjectPickListShipmentValidation(pickListState);
          this.InjectPickListDispatchToCommandStateOnCantPack(pickListState);
          this.InjectPickListHandleAbsenceByPackShipment(pickListState);
          this.PPBasis.InjectPickListPaperless((WorksheetPicking.PickListState) pickListState);
          break;
        case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState locationState:
          this.PPBasis.InjectNavigationOnLocation(locationState);
          this.InjectLocationPromptForPackageConfirmOnPaperlessPack(locationState);
          break;
        case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState itemState:
          this.PPBasis.InjectNavigationOnItem(itemState);
          this.InjectItemPromptForPackageConfirmOnPaperlessPack(itemState);
          this.Basis.Get<PickPackShip.PackMode.Logic>().InjectItemAbsenceHandlingByBox(itemState);
          break;
        case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState lsState:
          this.PPBasis.InjectNavigationOnLotSerial(lsState);
          this.Basis.InjectLotSerialDeactivationOnDefaultLotSerialOption(lsState, true);
          break;
        case PickPackShip.PackMode.ConfirmState confirmState:
          this.InjectConfirmCombinedFromPackAndWorksheet(confirmState);
          break;
      }
    }
    else if (scanState is PickPackShip.PackMode.ShipmentState shipmentState && this.IsPackOnly)
    {
      this.PPBasis.InjectShipmentPromptWithTakeNext((PickPackShip.ShipmentState) shipmentState);
      this.PPBasis.InjectSuppressShipmentWithWorksheetOfSingleType((PickPackShip.ShipmentState) shipmentState);
      this.InjectShipmentAbsenceHandlingByWorksheetOfSingleType(shipmentState);
    }
    return scanState;
  }

  [PXOverride]
  public ScanCommand<PickPackShip> DecorateScanCommand(
    ScanCommand<PickPackShip> original,
    Func<ScanCommand<PickPackShip>, ScanCommand<PickPackShip>> base_DecorateScanCommand)
  {
    ScanCommand<PickPackShip> scanCommand = base_DecorateScanCommand(original);
    if (((ScanComponent<PickPackShip>) scanCommand).ModeCode == "PPAO")
    {
      switch (scanCommand)
      {
        case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.RemoveCommand remove:
          this.PPBasis.InjectRemoveClearLocationAndInventory(remove);
          break;
        case PaperlessPicking.TakeNextPickListCommand takeNext:
          this.InjectTakeNextEnablingForPaperlessPackOnly(takeNext);
          break;
      }
    }
    return scanCommand;
  }

  [PXOverride]
  public void SetPickList(
    PXResult<SOPickingWorksheet, SOPicker> pickList,
    Action<PXResult<SOPickingWorksheet, SOPicker>> base_SetPickList)
  {
    base_SetPickList(pickList);
    if (!(this.Basis.CurrentMode is PaperlessOnlyPacking.PaperlessPackOnlyMode))
      return;
    this.Basis.RefNbr = ((PXResult) pickList)?.GetItem<SOPickingWorksheet>()?.SingleShipmentNbr;
    ((PXSelectBase<PX.Objects.SO.SOShipment>) this.Basis.Graph.Document).Current = PX.Objects.SO.SOShipment.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), this.Basis.RefNbr, (PKFindOptions) 1);
    this.Basis.NoteID = (Guid?) this.Basis.Shipment?.NoteID;
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.WorksheetPicking.CheckAvailability(System.Decimal)" />
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R2.")]
  [PXOverride]
  public FlowStatus CheckAvailability(
    Decimal deltaQty,
    Func<Decimal, FlowStatus> base_CheckAvailability)
  {
    return !(this.Basis.CurrentMode is PaperlessOnlyPacking.PaperlessPackOnlyMode) ? base_CheckAvailability(deltaQty) : FlowStatus.Ok;
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.WorksheetPicking.CheckAvailability(System.Decimal,PX.Objects.SO.SOPickerListEntry)" />
  [PXOverride]
  public virtual FlowStatus CheckAvailability(
    Decimal deltaQty,
    SOPickerListEntry pickedSplit,
    Func<Decimal, SOPickerListEntry, FlowStatus> base_CheckAvailability)
  {
    return !(this.Basis.CurrentMode is PaperlessOnlyPacking.PaperlessPackOnlyMode) ? base_CheckAvailability(deltaQty, pickedSplit) : FlowStatus.Ok;
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.WorksheetPicking.ShowWorksheetNbrForMode(System.String)" />
  [PXOverride]
  public bool ShowWorksheetNbrForMode(
    string modeCode,
    Func<string, bool> base_ShowWorksheetNbrForMode)
  {
    return base_ShowWorksheetNbrForMode(modeCode) && modeCode != "PPAO";
  }

  [PXOverride]
  public ScanMode<PickPackShip> FindModeForWorksheet(
    SOPickingWorksheet sheet,
    Func<SOPickingWorksheet, ScanMode<PickPackShip>> base_FindModeForWorksheet)
  {
    return sheet.WorksheetType == "SS" && this.IsPackOnly ? (ScanMode<PickPackShip>) this.Basis.FindMode<PaperlessOnlyPacking.PaperlessPackOnlyMode>() : base_FindModeForWorksheet(sheet);
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.WorksheetPicking.InjectValidationPickFirst(PX.Objects.SO.WMS.PickPackShip.ShipmentState)" />
  [PXOverride]
  public void InjectValidationPickFirst(
    PickPackShip.ShipmentState refNbrState,
    Action<PickPackShip.ShipmentState> base_InjectValidationPickFirst)
  {
    if (this.Basis.Get<PaperlessOnlyPacking>().IsPackOnly)
      ((MethodInterceptor<EntityState<PickPackShip, PX.Objects.SO.SOShipment>, PickPackShip>.OfFunc<PX.Objects.SO.SOShipment, Validation>.AsAppendable) ((EntityState<PickPackShip, PX.Objects.SO.SOShipment>) refNbrState).Intercept.Validate).ByAppend((Func<Validation, PX.Objects.SO.SOShipment, Validation>) ((basis, shipment) =>
      {
        if (shipment.CurrentWorksheetNbr != null)
        {
          bool? picked = shipment.Picked;
          bool flag = false;
          if (picked.GetValueOrDefault() == flag & picked.HasValue && SOPickingWorksheet.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) basis), shipment.CurrentWorksheetNbr).With<SOPickingWorksheet, bool>((Func<SOPickingWorksheet, bool>) (w => w.WorksheetType != "SS")))
            return Validation.Fail("The {0} shipment cannot be packed because the items have not been picked.", new object[1]
            {
              (object) shipment.ShipmentNbr
            });
        }
        return Validation.Ok;
      }), new RelativeInject?());
    else
      base_InjectValidationPickFirst(refNbrState);
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.WorksheetPicking.GetEntriesToPick" />
  [PXOverride]
  public IEnumerable<SOPickerListEntry> GetEntriesToPick(
    Func<IEnumerable<SOPickerListEntry>> base_GetEntriesToPick)
  {
    if (!(this.Basis.CurrentMode is PaperlessOnlyPacking.PaperlessPackOnlyMode))
      return base_GetEntriesToPick();
    PaperlessPicking paperlessPicking = this.Basis.Get<PaperlessPicking>();
    return !this.Basis.Remove.GetValueOrDefault() ? paperlessPicking.GetWantedSplitsForIncrease() : paperlessPicking.GetSplitsForRemoval();
  }

  /// Overrides <see cref="P:PX.Objects.SO.WMS.PickPackShip.DocumentIsConfirmed" />
  [PXOverride]
  public bool get_DocumentIsConfirmed(Func<bool> base_DocumentIsConfirmed)
  {
    if (!(this.Basis.CurrentMode is PaperlessOnlyPacking.PaperlessPackOnlyMode))
      return base_DocumentIsConfirmed();
    PX.Objects.SO.SOShipment shipment = this.Basis.Shipment;
    bool? confirmed;
    int num;
    if (shipment == null)
    {
      num = 0;
    }
    else
    {
      confirmed = shipment.Confirmed;
      num = confirmed.GetValueOrDefault() ? 1 : 0;
    }
    if (num != 0)
      return true;
    SOPicker pickList = this.WSBasis.PickList;
    if (pickList == null)
      return false;
    confirmed = pickList.Confirmed;
    return confirmed.GetValueOrDefault();
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.PaperlessPicking.EnsureShipmentUserLinkForPaperlessPick" />
  [PXOverride]
  public void EnsureShipmentUserLinkForPaperlessPick(
    Action base_EnsureShipmentUserLinkForPaperlessPick)
  {
    if (this.Basis.CurrentMode is PaperlessOnlyPacking.PaperlessPackOnlyMode)
      this.Graph.WorkLogExt.EnsureFor(this.PPBasis.SingleShipmentNbr, new Guid?(((PXGraph) this.Graph).Accessinfo.UserID), "PPCK");
    else
      base_EnsureShipmentUserLinkForPaperlessPick();
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.PaperlessPicking.ReturnCurrentJobToQueue" />
  [PXOverride]
  public bool ReturnCurrentJobToQueue(Func<bool> base_ReturnCurrentJobToQueue)
  {
    int num = base_ReturnCurrentJobToQueue() ? 1 : 0;
    if (num == 0)
      return num != 0;
    if (!(this.Basis.CurrentMode is PaperlessOnlyPacking.PaperlessPackOnlyMode))
      return num != 0;
    this.Graph.WorkLogExt.SuspendFor(this.PPBasis.SingleShipmentNbr, new Guid?(((PXGraph) this.Graph).Accessinfo.UserID), "PPCK");
    return num != 0;
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.PickPackShip.UpdateWorkLogOnLogScan(PX.Objects.SO.SOShipmentEntry.WorkLog,System.Boolean)" />
  [PXOverride]
  public void UpdateWorkLogOnLogScan(
    SOShipmentEntry.WorkLog workLogger,
    bool isError,
    Action<SOShipmentEntry.WorkLog, bool> base_UpdateWorkLogOnLogScan)
  {
    base_UpdateWorkLogOnLogScan(workLogger, isError);
    if (!(this.Basis.CurrentMode is PaperlessOnlyPacking.PaperlessPackOnlyMode) || string.IsNullOrEmpty(this.PPBasis.SingleShipmentNbr))
      return;
    workLogger.LogScanFor(this.PPBasis.SingleShipmentNbr, new Guid?(((PXGraph) this.Graph).Accessinfo.UserID), "PPCK", isError);
  }

  /// Overrides <see cref="M:PX.BarcodeProcessing.BarcodeDrivenStateMachine`2.OnBeforeFullClear" />
  [PXOverride]
  public void OnBeforeFullClear(Action base_OnBeforeFullClear)
  {
    base_OnBeforeFullClear();
    if (!(this.Basis.CurrentMode is PaperlessOnlyPacking.PaperlessPackOnlyMode) || this.PPBasis.SingleShipmentNbr == null || !this.Graph.WorkLogExt.SuspendFor(this.PPBasis.SingleShipmentNbr, new Guid?(((PXGraph) this.Graph).Accessinfo.UserID), "PPCK"))
      return;
    this.Graph.WorkLogExt.PersistWorkLog();
  }

  public sealed class PaperlessPackOnlyMode : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanMode
  {
    public const string Value = "PPAO";

    public virtual string Code => "PPAO";

    public virtual string Description => "Paperless Pack";

    protected virtual bool IsModeActive()
    {
      return ((ScanMode<PickPackShip>) this).Basis.Get<PaperlessOnlyPacking>().IsPackOnly;
    }

    protected virtual IEnumerable<ScanState<PickPackShip>> CreateStates()
    {
      yield return (ScanState<PickPackShip>) new PaperlessPicking.PickListState();
      yield return (ScanState<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState();
      yield return (ScanState<PickPackShip>) new PickPackShip.PackMode.BoxState();
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
      yield return (ScanState<PickPackShip>) new PickPackShip.PackMode.ConfirmState();
      yield return (ScanState<PickPackShip>) new PickPackShip.CommandOrShipmentOnlyState();
      yield return (ScanState<PickPackShip>) new PickPackShip.PackMode.BoxConfirming.StartState();
      yield return (ScanState<PickPackShip>) new PickPackShip.PackMode.BoxConfirming.WeightState();
      yield return (ScanState<PickPackShip>) new PickPackShip.PackMode.BoxConfirming.DimensionsState();
      yield return (ScanState<PickPackShip>) new PickPackShip.PackMode.BoxConfirming.CompleteState();
      yield return (ScanState<PickPackShip>) new PaperlessPicking.WarehouseState();
      yield return (ScanState<PickPackShip>) new PaperlessPicking.NearestLocationState();
    }

    protected virtual IEnumerable<ScanTransition<PickPackShip>> CreateTransitions()
    {
      return ((ScanMode<PickPackShip>) this).StateFlow((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (flow => flow.ForkBy((Func<PickPackShip, bool>) (basis => !basis.Remove.GetValueOrDefault())).PositiveBranch((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (pfl => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) pfl.From<PaperlessPicking.PickListState>().NextTo<PickPackShip.PackMode.BoxState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState>((Action<PickPackShip>) null))).NegativeBranch((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (nfl => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) nfl.From<PaperlessPicking.PickListState>().NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>((Action<PickPackShip>) null))))).Concat<ScanTransition<PickPackShip>>(((ScanMode<PickPackShip>) this).StateFlow((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (flow => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) flow.From<PickPackShip.PackMode.BoxConfirming.StartState>().NextTo<PickPackShip.PackMode.BoxConfirming.WeightState>((Action<PickPackShip>) null)).NextTo<PickPackShip.PackMode.BoxConfirming.DimensionsState>((Action<PickPackShip>) null)).NextTo<PickPackShip.PackMode.BoxConfirming.CompleteState>((Action<PickPackShip>) null))));
    }

    protected virtual IEnumerable<ScanCommand<PickPackShip>> CreateCommands()
    {
      yield return (ScanCommand<PickPackShip>) new PickPackShip.PackMode.RemoveCommand();
      yield return (ScanCommand<PickPackShip>) new BarcodeQtySupport<PickPackShip, PickPackShip.Host>.SetQtyCommand();
      yield return (ScanCommand<PickPackShip>) new PickPackShip.PackMode.ConfirmPackageCommand();
      yield return (ScanCommand<PickPackShip>) new PaperlessOnlyPacking.PaperlessPackOnlyMode.ConfirmPackListCommand();
      yield return (ScanCommand<PickPackShip>) new PaperlessPicking.TakeNextPickListCommand();
      yield return (ScanCommand<PickPackShip>) new PaperlessOnlyPacking.PaperlessPackOnlyMode.ConfirmPackListAndTakeNextCommand();
      yield return (ScanCommand<PickPackShip>) new PaperlessPicking.ConfirmLineQtyCommand();
    }

    protected virtual IEnumerable<ScanQuestion<PickPackShip>> CreateQuestions()
    {
      yield return (ScanQuestion<PickPackShip>) new PickPackShip.PackMode.BoxConfirming.WeightState.SkipQuestion();
      yield return (ScanQuestion<PickPackShip>) new PickPackShip.PackMode.BoxConfirming.WeightState.SkipScalesQuestion();
      yield return (ScanQuestion<PickPackShip>) new PickPackShip.PackMode.BoxConfirming.DimensionsState.SkipQuestion();
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
      ((ScanMode<PickPackShip>) this).Clear<PickPackShip.PackMode.BoxState>(fullReset);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>(fullReset);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>(true);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState>(true);
      ((ScanMode<PickPackShip>) this).Clear<PickPackShip.PackMode.BoxConfirming.WeightState>(true);
      ((ScanMode<PickPackShip>) this).Clear<PickPackShip.PackMode.BoxConfirming.DimensionsState>(true);
      if (!fullReset)
        return;
      this.Get<PickPackShip.PackMode.Logic>().PackageLineNbrUI = new int?();
    }

    public class value : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      PaperlessOnlyPacking.PaperlessPackOnlyMode.value>
    {
      public value()
        : base("PPAO")
      {
      }
    }

    public sealed class ConfirmPackListCommand : 
      BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand
    {
      public virtual string Code => "CONFIRM*PACK";

      public virtual string ButtonName => "scanConfirmPackList";

      public virtual string DisplayName => "Confirm Pack List";

      protected virtual bool IsEnabled
      {
        get => ((ScanComponent<PickPackShip>) this).Basis.DocumentIsEditable;
      }

      protected virtual bool Process()
      {
        ((ScanComponent<PickPackShip>) this).Basis.Get<PaperlessOnlyPacking.PaperlessPackOnlyMode.ConfirmPackListCommand.Logic>().ConfirmPackList();
        return true;
      }

      [PXProtectedAccess(null)]
      public abstract class Logic : 
        BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension<PickPackShip.ConfirmShipmentCommand.Logic>
      {
        public static bool IsActive() => PaperlessOnlyPacking.IsActive();

        public virtual void ConfirmPackList()
        {
          if (!this.CanConfirm(false))
            return;
          PickPackShip.PackMode.Logic logic = ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Get<PickPackShip.PackMode.Logic>();
          SOPackageDetailEx selectedPackage = logic.SelectedPackage;
          int num;
          if (selectedPackage == null)
          {
            num = 0;
          }
          else
          {
            bool? confirmed = selectedPackage.Confirmed;
            bool flag = false;
            num = confirmed.GetValueOrDefault() == flag & confirmed.HasValue ? 1 : 0;
          }
          if (num != 0 && !((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Get<PickPackShip.PackMode.BoxConfirming.CompleteState.Logic>().TryAutoConfirm())
            return;
          int? packageLineNbr = logic.PackageLineNbr;
          ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Reset(false);
          ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>(true);
          logic.PackageLineNbr = packageLineNbr;
          SOPackageDetailEx autoPackageToConfirm;
          logic.HasSingleAutoPackage(((WarehouseManagementSystem<PickPackShip, PickPackShip.Host>) this.Basis).RefNbr, out autoPackageToConfirm);
          ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).SaveChanges();
          ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).AwaitFor<PX.Objects.SO.SOShipment>((Func<PickPackShip, PX.Objects.SO.SOShipment, CancellationToken, System.Threading.Tasks.Task>) ((basis, doc, ct) => PaperlessOnlyPacking.PaperlessPackOnlyMode.ConfirmPackListCommand.Logic.ConfirmSinglePickListHandler(doc.ShipmentNbr, autoPackageToConfirm, ct))).WithDescription("Confimation of {0} pick list in progress.", new object[1]
          {
            (object) ((WarehouseManagementSystem<PickPackShip, PickPackShip.Host>) this.Basis).RefNbr
          }).ActualizeDataBy((Func<PickPackShip, PX.Objects.SO.SOShipment, PX.Objects.SO.SOShipment>) ((basis, doc) => (PX.Objects.SO.SOShipment) PrimaryKeyOf<PX.Objects.SO.SOShipment>.By<PX.Objects.SO.SOShipment.shipmentNbr>.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) basis), (PX.Objects.SO.SOShipment.shipmentNbr) doc, (PKFindOptions) 0))).OnSuccess(new Action<ScanLongRunAwaiter<PickPackShip, PX.Objects.SO.SOShipment>.ISuccessProcessor>(this.ConfigureOnSuccessAction)).OnFail((Action<ScanLongRunAwaiter<PickPackShip, PX.Objects.SO.SOShipment>.IResultProcessor>) (x => x.Say("Pick list not confirmed.", Array.Empty<object>()))).BeginAwait(((PickPackShip) this.Basis).Shipment);
        }

        public virtual void ConfigureOnSuccessAction(
          ScanLongRunAwaiter<PickPackShip, PX.Objects.SO.SOShipment>.IResultProcessor onSuccess)
        {
          onSuccess.Say("Pick list successfully confirmed.", Array.Empty<object>()).ChangeStateTo<WorksheetPicking.PickListState>();
        }

        [Obsolete("Use the ConfirmSinglePickListHandler method instead.")]
        protected static System.Threading.Tasks.Task ConfirmShipmentHandler(
          string shipmentNbr,
          SOPickPackShipSetup setup,
          SOPickPackShipUserSetup userSetup,
          SOPackageDetailEx autoPackageToConfirm,
          CancellationToken cancellationToken)
        {
          return PaperlessOnlyPacking.PaperlessPackOnlyMode.ConfirmPackListCommand.Logic.ConfirmSinglePickListHandler(shipmentNbr, autoPackageToConfirm, cancellationToken);
        }

        protected static async System.Threading.Tasks.Task ConfirmSinglePickListHandler(
          string shipmentNbr,
          SOPackageDetailEx autoPackageToConfirm,
          CancellationToken cancellationToken)
        {
          await BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.WithSuppressedRedirects((Func<System.Threading.Tasks.Task>) (async () =>
          {
            SOPickingWorksheetReview instance = PXGraph.CreateInstance<SOPickingWorksheetReview>();
            SOPickingWorksheet pickingWorksheet;
            SOPicker soPicker;
            SOPickingJob soPickingJob;
            ((IEnumerable<PXResult<SOPickingWorksheet>>) PXSelectBase<SOPickingWorksheet, PXViewOf<SOPickingWorksheet>.BasedOn<SelectFromBase<SOPickingWorksheet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPicker>.On<SOPicker.FK.Worksheet>>, FbqlJoins.Inner<SOPickingJob>.On<SOPickingJob.FK.Picker>>>.Where<BqlOperand<SOPickingWorksheet.singleShipmentNbr, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) instance, new object[1]
            {
              (object) shipmentNbr
            })).AsEnumerable<PXResult<SOPickingWorksheet>>().Cast<PXResult<SOPickingWorksheet, SOPicker, SOPickingJob>>().Single<PXResult<SOPickingWorksheet, SOPicker, SOPickingJob>>().Deconstruct(ref pickingWorksheet, ref soPicker, ref soPickingJob);
            SOPickingWorksheet worksheet = pickingWorksheet;
            SOPicker pickList = soPicker;
            SOPickingJob pickingJob = soPickingJob;
            using (PXTransactionScope pickListTransaction = new PXTransactionScope())
            {
              instance.PickListConfirmation.ConfirmPickList(pickList, new int?());
              await instance.PickListConfirmation.FulfillShipmentsAndConfirmWorksheet(worksheet, cancellationToken);
              pickListTransaction.Complete();
            }
            if (!pickingJob.AutomaticShipmentConfirmation.GetValueOrDefault())
            {
              pickingJob = (SOPickingJob) null;
            }
            else
            {
              await ((PXGraph) PXGraph.CreateInstance<PickPackShip.Host>()).GetExtension<WorksheetPicking>().TryConfirmShipmentRightAfterPickList(shipmentNbr, autoPackageToConfirm, cancellationToken);
              pickingJob = (SOPickingJob) null;
            }
          }));
        }

        /// Uses <see cref="M:PX.Objects.SO.WMS.PickPackShip.ConfirmShipmentCommand.Logic.CanConfirm(System.Boolean)" />
        [PXProtectedAccess(typeof (PickPackShip.ConfirmShipmentCommand.Logic))]
        protected abstract bool CanConfirm(bool confirmAsIs);
      }

      [PXLocalizable]
      public abstract class Msg : WorksheetPicking.ConfirmPickListCommand.Msg
      {
        public new const string DisplayName = "Confirm Pack List";
      }
    }

    public sealed class ConfirmPackListAndTakeNextCommand : 
      BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand
    {
      private bool _inProcess;

      public virtual string Code => "CONFIRM*PACK*AND*NEXT";

      public virtual string ButtonName => "scanConfirmPackListAndTakeNext";

      public virtual string DisplayName => "Finish and Next";

      protected virtual bool IsEnabled
      {
        get
        {
          return ((ScanCommand<PickPackShip>) ((ScanComponent<PickPackShip>) this).Basis.CurrentMode.Commands.OfType<PaperlessOnlyPacking.PaperlessPackOnlyMode.ConfirmPackListCommand>().First<PaperlessOnlyPacking.PaperlessPackOnlyMode.ConfirmPackListCommand>()).IsApplicable;
        }
      }

      protected virtual bool Process()
      {
        try
        {
          this._inProcess = true;
          return ((ScanCommand<PickPackShip>) ((ScanComponent<PickPackShip>) this).Basis.CurrentMode.Commands.OfType<PaperlessOnlyPacking.PaperlessPackOnlyMode.ConfirmPackListCommand>().First<PaperlessOnlyPacking.PaperlessPackOnlyMode.ConfirmPackListCommand>()).Execute();
        }
        finally
        {
          this._inProcess = false;
        }
      }

      /// Overrides <see cref="T:PX.Objects.SO.WMS.Worksheet.PaperlessOnlyPacking.PaperlessPackOnlyMode.ConfirmPackListCommand.Logic" />
      public class AlterConfirmPackListCommandLogic : 
        BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension<PaperlessOnlyPacking.PaperlessPackOnlyMode.ConfirmPackListCommand.Logic>
      {
        public static bool IsActive() => PaperlessPicking.IsActive();

        [PXOverride]
        public void ConfigureOnSuccessAction(
          ScanLongRunAwaiter<PickPackShip, PX.Objects.SO.SOShipment>.IResultProcessor onSuccess,
          Action<ScanLongRunAwaiter<PickPackShip, PX.Objects.SO.SOShipment>.IResultProcessor> base_ConfigureOnSuccessAction)
        {
          base_ConfigureOnSuccessAction(onSuccess);
          PaperlessOnlyPacking.PaperlessPackOnlyMode.ConfirmPackListAndTakeNextCommand andTakeNextCommand = ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).CurrentMode.Commands.OfType<PaperlessOnlyPacking.PaperlessPackOnlyMode.ConfirmPackListAndTakeNextCommand>().FirstOrDefault<PaperlessOnlyPacking.PaperlessPackOnlyMode.ConfirmPackListAndTakeNextCommand>();
          if ((andTakeNextCommand != null ? (andTakeNextCommand._inProcess ? 1 : 0) : 0) == 0)
            return;
          onSuccess.Do((Action<PickPackShip, PX.Objects.SO.SOShipment>) ((basis, picker) => ((ScanCommand<PickPackShip>) basis.CurrentMode.Commands.OfType<PaperlessPicking.TakeNextPickListCommand>().First<PaperlessPicking.TakeNextPickListCommand>()).Execute()));
        }
      }
    }

    [PXLocalizable]
    public abstract class Msg : ScanMode<PickPackShip>.Msg
    {
      public const string DisplayName = "Paperless Pack";
      public const string BoxConfirmOrContinueByLocationPrompt = "Confirm the package, or scan the next location.";
    }
  }

  public class AlterTakeNextPickListCommandLogic : 
    PaperlessPicking.ScanExtension<PaperlessPicking.TakeNextPickListCommand.Logic>
  {
    public static bool IsActive()
    {
      return PaperlessPicking.ScanExtension<PaperlessPicking.TakeNextPickListCommand.Logic>.IsActiveBase();
    }

    /// Overrides <see cref="M:PX.Objects.SO.WMS.PaperlessPicking.TakeNextPickListCommand.Logic.TakeNext" />
    [PXOverride]
    public bool TakeNext(Func<bool> base_TakeNext)
    {
      if (this.Basis.RefNbr == null && this.Basis.CurrentMode is PickPackShip.PackMode && this.Basis.Get<PaperlessOnlyPacking>().IsPackOnly)
        this.Basis.SetScanMode<PaperlessOnlyPacking.PaperlessPackOnlyMode>();
      return base_TakeNext();
    }

    [PXOverride]
    public void ApplyCommonFilters(
      PXSelectBase<SOPickingJob> command,
      Action<PXSelectBase<SOPickingJob>> base_ApplyCommonFilters)
    {
      base_ApplyCommonFilters(command);
      if (!(this.Basis.CurrentMode is PickPackShip.PackMode) && !(this.Basis.CurrentMode is PaperlessOnlyPacking.PaperlessPackOnlyMode))
        return;
      command.WhereAnd<Where<BqlOperand<SOPickingWorksheet.worksheetType, IBqlString>.IsEqual<SOPickingWorksheet.worksheetType.single>>>();
    }
  }

  public class AlterPackModeLogic : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension<PickPackShip.PackMode.Logic>
  {
    public static bool IsActive() => PaperlessOnlyPacking.IsActive();

    /// Overrides <see cref="P:PX.Objects.SO.WMS.PickPackShip.PackMode.Logic.CanPack" />
    [PXOverride]
    public bool get_CanPack(Func<bool> base_CanPack)
    {
      return ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Get<PaperlessOnlyPacking>().IsPackOnly && ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).CurrentMode is PaperlessOnlyPacking.PaperlessPackOnlyMode ? ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Target.PickedForPack).SelectMain(Array.Empty<object>())).Any<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
      {
        Decimal? packedQty = s.PackedQty;
        Decimal? qty = s.Qty;
        return packedQty.GetValueOrDefault() < qty.GetValueOrDefault() & packedQty.HasValue & qty.HasValue && !PXFieldAttachedTo<PX.Objects.SO.SOShipLineSplit>.By<PickPackShip.Host>.AsBool.Named<PaperlessOnlyPacking.RelatedPickListSplitForceCompleted>.GetValue(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), s).GetValueOrDefault();
      })) : base_CanPack();
    }

    /// Overrides <see cref="M:PX.Objects.SO.WMS.PickPackShip.PackMode.Logic.ShowPackTab(PX.BarcodeProcessing.ScanHeader)" />
    [PXOverride]
    public bool ShowPackTab(ScanHeader row, Func<ScanHeader, bool> base_ShowPackTab)
    {
      if (base_ShowPackTab(row))
        return true;
      return ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Get<PaperlessOnlyPacking>().IsPackOnly && row.Mode == "PPAO";
    }

    protected virtual void _(PX.Data.Events.RowSelected<ScanHeader> args)
    {
      if (!(args.Row?.Mode == "PPAO"))
        return;
      ((PXAction) this.Target.ReviewPack).SetVisible(((PXGraph) ((PXGraphExtension<PickPackShip.Host>) this).Base).IsMobile);
    }

    public class AlterCommandOrShipmentOnlyStatePrompt : 
      BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension<PickPackShip.CommandOrShipmentOnlyState.Logic>
    {
      public static bool IsActive() => PaperlessOnlyPacking.IsActive();

      /// Overrides <see cref="!:PickPackShip.CommandOrShipmentOnlyState.Logic.GetCommandOrShipmentOnlyPrompt" />
      [PXOverride]
      public string GetCommandOrShipmentOnlyPrompt(Func<string> base_GetCommandOrShipmentOnlyPrompt)
      {
        if (((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).CurrentMode is PaperlessOnlyPacking.PaperlessPackOnlyMode)
        {
          PickPackShip.PackMode.Logic logic = ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Get<PickPackShip.PackMode.Logic>();
          if (logic != null && logic.CanConfirmPackage)
            return "Confirm the package.";
        }
        return base_GetCommandOrShipmentOnlyPrompt();
      }
    }

    public class AlterConfirmStateLogic : 
      BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension<PickPackShip.PackMode.ConfirmState.Logic>
    {
      public static bool IsActive() => PaperlessOnlyPacking.IsActive();

      /// Overrides <see cref="M:PX.Objects.SO.WMS.PickPackShip.PackMode.ConfirmState.Logic.GetSplitsToPack" />
      [PXOverride]
      public IEnumerable<PX.Objects.SO.SOShipLineSplit> GetSplitsToPack(
        Func<IEnumerable<PX.Objects.SO.SOShipLineSplit>> base_GetSplitsToPack)
      {
        if (!(((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).CurrentMode is PaperlessOnlyPacking.PaperlessPackOnlyMode))
          return base_GetSplitsToPack();
        SOPickerListEntry lastPickedSplit = ((PXCache) GraphHelper.Caches<SOPickerListEntry>((PXGraph) this.Graph)).Dirty.Cast<SOPickerListEntry>().First<SOPickerListEntry>();
        return ((IEnumerable<PX.Objects.SO.SOShipLineSplit>) ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Get<PickPackShip.PackMode.Logic>().PickedForPack).SelectMain(Array.Empty<object>())).Where<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
        {
          int? locationId1 = s.LocationID;
          int? locationId2 = lastPickedSplit.LocationID;
          if (locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue)
          {
            int? inventoryId1 = s.InventoryID;
            int? inventoryId2 = lastPickedSplit.InventoryID;
            if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
            {
              int? subItemId1 = s.SubItemID;
              int? subItemId2 = lastPickedSplit.SubItemID;
              if (subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue && s.LotSerialNbr == lastPickedSplit.LotSerialNbr)
              {
                if (!((WarehouseManagementSystem<PickPackShip, PickPackShip.Host>) this.Basis).Remove.GetValueOrDefault())
                {
                  Decimal? nullable = this.Target.TargetQty(s);
                  Decimal? packedQty = s.PackedQty;
                  return nullable.GetValueOrDefault() > packedQty.GetValueOrDefault() & nullable.HasValue & packedQty.HasValue;
                }
                Decimal? packedQty1 = s.PackedQty;
                Decimal num = 0M;
                return packedQty1.GetValueOrDefault() > num & packedQty1.HasValue;
              }
            }
          }
          return false;
        }));
      }
    }
  }

  public class AlterPaperlessPickingConfirmLineQtyCommandLogic : 
    PaperlessPicking.ScanExtension<PaperlessPicking.ConfirmLineQtyCommand.Logic>
  {
    public static bool IsActive() => PaperlessOnlyPacking.IsActive();

    /// Overrides <see cref="M:PX.Objects.SO.WMS.PaperlessPicking.ConfirmLineQtyCommand.Logic.ReopenQtyOfCurrentSplit" />
    [PXOverride]
    public bool ReopenQtyOfCurrentSplit(Func<bool> base_ReopenQtyOfCurrentSplit)
    {
      if (this.Basis.CurrentMode is PaperlessOnlyPacking.PaperlessPackOnlyMode)
      {
        PX.Objects.SO.SOShipLineSplit current = ((PXSelectBase<PX.Objects.SO.SOShipLineSplit>) this.Basis.Get<PickPackShip.PackMode.Logic>().PickedForPack).Current;
        if (current != null)
        {
          SOPickerListEntry relatedPickListEntry = this.Basis.Get<PaperlessOnlyPacking.RelatedPickListSplitForceCompleted>().GetRelatedPickListEntry(current);
          if (relatedPickListEntry != null)
            ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).Current = relatedPickListEntry;
        }
      }
      return base_ReopenQtyOfCurrentSplit();
    }
  }

  [PXUIField(DisplayName = "Quantity Confirmed")]
  public class RelatedPickListSplitForceCompleted : 
    PXFieldAttachedTo<PX.Objects.SO.SOShipLineSplit>.By<PickPackShip.Host>.AsBool.Named<PaperlessOnlyPacking.RelatedPickListSplitForceCompleted>
  {
    private Dictionary<int, int> splitsToEntries;

    protected override bool? Visible
    {
      get
      {
        return new bool?(PaperlessOnlyPacking.IsActive() && this.Base.WMS.CurrentMode is PaperlessOnlyPacking.PaperlessPackOnlyMode);
      }
    }

    public override bool? GetValue(PX.Objects.SO.SOShipLineSplit row)
    {
      bool? visible = this.Visible;
      bool flag = false;
      if (visible.GetValueOrDefault() == flag & visible.HasValue || row == null || !row.SplitLineNbr.HasValue)
        return new bool?();
      return this.GetRelatedPickListEntry(row)?.ForceCompleted;
    }

    public virtual SOPickerListEntry GetRelatedPickListEntry(PX.Objects.SO.SOShipLineSplit row)
    {
      if (this.splitsToEntries == null || !this.splitsToEntries.ContainsKey(row.SplitLineNbr.Value))
        this.splitsToEntries = ((IEnumerable<PX.Objects.SO.Table.SOShipLineSplit>) GraphHelper.RowCast<PX.Objects.SO.Table.SOShipLineSplit>((IEnumerable) PXSelectBase<PX.Objects.SO.Table.SOShipLineSplit, PXViewOf<PX.Objects.SO.Table.SOShipLineSplit>.BasedOn<SelectFromBase<PX.Objects.SO.Table.SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<PX.Objects.SO.Table.SOShipLineSplit.shipmentNbr>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentNbr>.AsSimpleKey.WithTablesOf<PX.Objects.SO.SOShipment, PX.Objects.SO.Table.SOShipLineSplit>, PX.Objects.SO.SOShipment, PX.Objects.SO.Table.SOShipLineSplit>.SameAsCurrent>.Order<By<BqlField<PX.Objects.SO.Table.SOShipLineSplit.locationID, IBqlInt>.Asc, BqlField<PX.Objects.SO.Table.SOShipLineSplit.inventoryID, IBqlInt>.Asc, BqlField<PX.Objects.SO.Table.SOShipLineSplit.subItemID, IBqlInt>.Asc, BqlField<PX.Objects.SO.Table.SOShipLineSplit.lotSerialNbr, IBqlString>.Asc, BqlField<PX.Objects.SO.Table.SOShipLineSplit.baseQty, IBqlDecimal>.Asc, BqlField<PX.Objects.SO.Table.SOShipLineSplit.basePickedQty, IBqlDecimal>.Asc, BqlField<PX.Objects.SO.Table.SOShipLineSplit.splitLineNbr, IBqlInt>.Asc>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).ToArray<PX.Objects.SO.Table.SOShipLineSplit>()).Zip<PX.Objects.SO.Table.SOShipLineSplit, SOPickerListEntry, (int, int)>((IEnumerable<SOPickerListEntry>) GraphHelper.RowCast<SOPickerListEntry>((IEnumerable) PXSelectBase<SOPickerListEntry, PXViewOf<SOPickerListEntry>.BasedOn<SelectFromBase<SOPickerListEntry, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<SOPickerListEntry.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickerListEntry.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickerListEntry>, SOPicker, SOPickerListEntry>.SameAsCurrent>.Order<By<BqlField<SOPickerListEntry.locationID, IBqlInt>.Asc, BqlField<SOPickerListEntry.inventoryID, IBqlInt>.Asc, BqlField<SOPickerListEntry.subItemID, IBqlInt>.Asc, BqlField<SOPickerListEntry.lotSerialNbr, IBqlString>.Asc, BqlField<SOPickerListEntry.baseQty, IBqlDecimal>.Asc, BqlField<SOPickerListEntry.basePickedQty, IBqlDecimal>.Asc, BqlField<SOPickerListEntry.entryNbr, IBqlInt>.Asc>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).ToArray<SOPickerListEntry>(), (Func<PX.Objects.SO.Table.SOShipLineSplit, SOPickerListEntry, (int, int)>) ((s, e) =>
        {
          int? nullable = s.SplitLineNbr;
          int num1 = nullable.Value;
          nullable = e.EntryNbr;
          int num2 = nullable.Value;
          return (num1, num2);
        })).ToDictionary<(int, int), int, int>((Func<(int, int), int>) (pair => pair.SplitKey), (Func<(int, int), int>) (pair => pair.EntryNbr));
      int num;
      return this.splitsToEntries.TryGetValue(row.SplitLineNbr.Value, out num) ? PXResultset<SOPickerListEntry>.op_Implicit(((PXSelectBase<SOPickerListEntry>) this.Base.WMS.Get<WorksheetPicking>().PickListOfPicker).Search<SOPickerListEntry.entryNbr>((object) num, Array.Empty<object>())) : (SOPickerListEntry) null;
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string FieldDisplayName = "Quantity Confirmed";
    }
  }
}
