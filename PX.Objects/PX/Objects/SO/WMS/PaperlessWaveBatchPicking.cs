// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.WMS.PaperlessWaveBatchPicking
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Common;
using PX.Data;
using PX.Objects.IN;
using PX.Objects.IN.WMS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.WMS;

public class PaperlessWaveBatchPicking : WaveBatchPicking.ScanExtension
{
  public static bool IsActive()
  {
    return WaveBatchPicking.ScanExtension.IsActiveBase() && PaperlessPicking.IsActive();
  }

  public virtual void InjectPaperlessWaveMode(WaveBatchPicking.WavePickMode wavePick)
  {
    ((MethodInterceptor<ScanMode<PickPackShip>, PickPackShip>.OfAction<bool>) ((ScanMode<PickPackShip>) ((MethodInterceptor<ScanMode<PickPackShip>, PickPackShip>.OfFunc<IEnumerable<ScanTransition<PickPackShip>>>) ((ScanMode<PickPackShip>) ((MethodInterceptor<ScanMode<PickPackShip>, PickPackShip>.OfFunc<IEnumerable<ScanState<PickPackShip>>>.AsAppendable) ((ScanMode<PickPackShip>) ((MethodInterceptor<ScanMode<PickPackShip>, PickPackShip>.OfFunc<IEnumerable<ScanCommand<PickPackShip>>>.AsAppendable) ((ScanMode<PickPackShip>) wavePick).Intercept.CreateCommands).ByAppend((Func<PickPackShip, IEnumerable<ScanCommand<PickPackShip>>>) (basis => (IEnumerable<ScanCommand<PickPackShip>>) new BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand[3]
    {
      (BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand) new PaperlessPicking.TakeNextPickListCommand(),
      (BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand) new PaperlessPicking.ConfirmPickListAndTakeNextCommand(),
      (BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand) new PaperlessPicking.ConfirmLineQtyCommand()
    }), new RelativeInject?())).Intercept.CreateStates).ByAppend((Func<PickPackShip, IEnumerable<ScanState<PickPackShip>>>) (basis => (IEnumerable<ScanState<PickPackShip>>) new ScanState<PickPackShip>[2]
    {
      (ScanState<PickPackShip>) new PaperlessPicking.WarehouseState(),
      (ScanState<PickPackShip>) new PaperlessPicking.NearestLocationState()
    }), new RelativeInject?())).Intercept.CreateTransitions).ByReplace((Func<PickPackShip, IEnumerable<ScanTransition<PickPackShip>>>) (basis => basis.StateFlow((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (flow => flow.ForkBy((Func<PickPackShip, bool>) (b => !b.Remove.GetValueOrDefault())).PositiveBranch((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (directFlow => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) directFlow.From<WaveBatchPicking.WavePickMode.PickListState>().NextTo<ToteSupport.AssignToteState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState>((Action<PickPackShip>) null)).NextTo<ToteSupport.SelectToteState>((Action<PickPackShip>) null)).NextTo<WaveBatchPicking.WavePickMode.ConfirmToteState>((Action<PickPackShip>) null))).NegativeBranch((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (removeFlow => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) removeFlow.From<WaveBatchPicking.WavePickMode.PickListState>().NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>((Action<PickPackShip>) null)))))), new RelativeInject?((RelativeInject) 0))).Intercept.ResetMode).ByReplace((Action<PickPackShip, bool>) ((basis, fullReset) =>
    {
      basis.Clear<WaveBatchPicking.WavePickMode.PickListState>(fullReset && !basis.IsWithinReset);
      basis.Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>(fullReset);
      basis.Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>(fullReset);
      basis.Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>(true);
      basis.Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState>(true);
      basis.Clear<ToteSupport.AssignToteState>(true);
      basis.Clear<ToteSupport.SelectToteState>(true);
    }), new RelativeInject?((RelativeInject) 0));
  }

  public virtual void InjectPaperlessBatchMode(WaveBatchPicking.BatchPickMode batchPick)
  {
    ((MethodInterceptor<ScanMode<PickPackShip>, PickPackShip>.OfAction<bool>) ((ScanMode<PickPackShip>) ((MethodInterceptor<ScanMode<PickPackShip>, PickPackShip>.OfFunc<IEnumerable<ScanTransition<PickPackShip>>>) ((ScanMode<PickPackShip>) ((MethodInterceptor<ScanMode<PickPackShip>, PickPackShip>.OfFunc<IEnumerable<ScanState<PickPackShip>>>.AsAppendable) ((ScanMode<PickPackShip>) ((MethodInterceptor<ScanMode<PickPackShip>, PickPackShip>.OfFunc<IEnumerable<ScanCommand<PickPackShip>>>.AsAppendable) ((ScanMode<PickPackShip>) batchPick).Intercept.CreateCommands).ByAppend((Func<PickPackShip, IEnumerable<ScanCommand<PickPackShip>>>) (basis => (IEnumerable<ScanCommand<PickPackShip>>) new BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand[3]
    {
      (BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand) new PaperlessPicking.TakeNextPickListCommand(),
      (BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand) new PaperlessPicking.ConfirmPickListAndTakeNextCommand(),
      (BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanCommand) new PaperlessPicking.ConfirmLineQtyCommand()
    }), new RelativeInject?())).Intercept.CreateStates).ByAppend((Func<PickPackShip, IEnumerable<ScanState<PickPackShip>>>) (basis => (IEnumerable<ScanState<PickPackShip>>) new ScanState<PickPackShip>[2]
    {
      (ScanState<PickPackShip>) new PaperlessPicking.WarehouseState(),
      (ScanState<PickPackShip>) new PaperlessPicking.NearestLocationState()
    }), new RelativeInject?())).Intercept.CreateTransitions).ByReplace((Func<PickPackShip, IEnumerable<ScanTransition<PickPackShip>>>) (basis => basis.StateFlow((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (flow => flow.ForkBy((Func<PickPackShip, bool>) (b => !b.Remove.GetValueOrDefault())).PositiveBranch((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (directFlow => directFlow.ForkBy((Func<PickPackShip, bool>) (b => b.Get<PPSCartSupport>().With<PPSCartSupport, bool>((Func<PPSCartSupport, bool>) (cs => cs.IsCartRequired())))).PositiveBranch((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (directCartFlow => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) directCartFlow.From<WaveBatchPicking.BatchPickMode.PickListState>().NextTo<CartSupport<PickPackShip, PickPackShip.Host>.CartState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState>((Action<PickPackShip>) null))).NegativeBranch((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (directNoCartFlow => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) directNoCartFlow.From<WaveBatchPicking.BatchPickMode.PickListState>().NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState>((Action<PickPackShip>) null))))).NegativeBranch((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (removeFlow => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) removeFlow.From<WaveBatchPicking.BatchPickMode.PickListState>().NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>((Action<PickPackShip>) null)))))), new RelativeInject?((RelativeInject) 0))).Intercept.ResetMode).ByReplace((Action<PickPackShip, bool>) ((basis, fullReset) =>
    {
      basis.Clear<WaveBatchPicking.BatchPickMode.PickListState>(fullReset && !basis.IsWithinReset);
      basis.Clear<CartSupport<PickPackShip, PickPackShip.Host>.CartState>(fullReset && !basis.IsWithinReset);
      basis.Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>(fullReset);
      basis.Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>(fullReset);
      basis.Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>(true);
      basis.Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState>(true);
    }), new RelativeInject?((RelativeInject) 0));
  }

  [PXOverride]
  public ScanMode<PickPackShip> DecorateScanMode(
    ScanMode<PickPackShip> original,
    Func<ScanMode<PickPackShip>, ScanMode<PickPackShip>> base_DecorateScanMode)
  {
    ScanMode<PickPackShip> scanMode = base_DecorateScanMode(original);
    switch (scanMode)
    {
      case WaveBatchPicking.WavePickMode wavePick:
        this.InjectPaperlessWaveMode(wavePick);
        break;
      case WaveBatchPicking.BatchPickMode batchPick:
        this.InjectPaperlessBatchMode(batchPick);
        break;
    }
    return scanMode;
  }

  [PXOverride]
  public ScanState<PickPackShip> DecorateScanState(
    ScanState<PickPackShip> original,
    Func<ScanState<PickPackShip>, ScanState<PickPackShip>> base_DecorateScanState)
  {
    PaperlessPicking paperlessPicking = this.Basis.Get<PaperlessPicking>();
    ScanState<PickPackShip> scanState = base_DecorateScanState(original);
    if (WaveBatchPicking.MatchMode(((ScanComponent<PickPackShip>) scanState).ModeCode))
    {
      switch (scanState)
      {
        case WorksheetPicking.PickListState pickListState:
          paperlessPicking.InjectPickListPaperless(pickListState);
          break;
        case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState locState:
          paperlessPicking.InjectNavigationOnLocation(locState);
          break;
        case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState itemState:
          paperlessPicking.InjectNavigationOnItem(itemState);
          break;
        case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState lsState:
          paperlessPicking.InjectNavigationOnLotSerial(lsState);
          break;
      }
    }
    return scanState;
  }

  [PXOverride]
  public ScanCommand<PickPackShip> DecorateScanCommand(
    ScanCommand<PickPackShip> original,
    Func<ScanCommand<PickPackShip>, ScanCommand<PickPackShip>> base_DecorateScanCommand)
  {
    PaperlessPicking paperlessPicking = this.Basis.Get<PaperlessPicking>();
    ScanCommand<PickPackShip> scanCommand = base_DecorateScanCommand(original);
    if (WaveBatchPicking.MatchMode(((ScanComponent<PickPackShip>) scanCommand).ModeCode))
    {
      switch (scanCommand)
      {
        case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.RemoveCommand remove:
          paperlessPicking.InjectRemoveClearLocationAndInventory(remove);
          break;
        case WorksheetPicking.ConfirmPickListCommand confirm:
          paperlessPicking.InjectConfirmPickListSuppressionOnCanPick(confirm);
          break;
      }
    }
    return scanCommand;
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.PickPackShip.InjectLocationSkippingOnPromptLocationForEveryLineOption(PX.Objects.IN.WMS.WarehouseManagementSystem{PX.Objects.SO.WMS.PickPackShip,PX.Objects.SO.WMS.PickPackShip.Host}.LocationState)" />
  [PXOverride]
  public void InjectLocationSkippingOnPromptLocationForEveryLineOption(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState locationState,
    Action<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState> base_InjectLocationSkippingOnPromptLocationForEveryLineOption)
  {
    if (WaveBatchPicking.MatchMode(((ScanComponent<PickPackShip>) locationState).ModeCode))
      return;
    base_InjectLocationSkippingOnPromptLocationForEveryLineOption(locationState);
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.PickPackShip.InjectItemAbsenceHandlingByLocation(PX.Objects.IN.WMS.WarehouseManagementSystem{PX.Objects.SO.WMS.PickPackShip,PX.Objects.SO.WMS.PickPackShip.Host}.InventoryItemState)" />
  [PXOverride]
  public void InjectItemAbsenceHandlingByLocation(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState inventoryState,
    Action<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState> base_InjectItemAbsenceHandlingByLocation)
  {
    if (WaveBatchPicking.MatchMode(((ScanComponent<PickPackShip>) inventoryState).ModeCode))
      return;
    base_InjectItemAbsenceHandlingByLocation(inventoryState);
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.WorksheetPicking.GetEntriesToPick" />
  [PXOverride]
  public IEnumerable<SOPickerListEntry> GetEntriesToPick(
    Func<IEnumerable<SOPickerListEntry>> base_GetEntriesToPick)
  {
    if (!WaveBatchPicking.MatchMode(this.Basis.CurrentMode.Code))
      return base_GetEntriesToPick();
    PaperlessPicking paperlessPicking = this.Basis.Get<PaperlessPicking>();
    return !this.Basis.Remove.GetValueOrDefault() ? paperlessPicking.GetWantedSplitsForIncrease() : paperlessPicking.GetSplitsForRemoval();
  }

  /// Overrides <see cref="T:PX.Objects.SO.WMS.WaveBatchPicking.ConfirmState.Logic" />
  public class AlterWaveBatchPickingConfirmStateLogic : 
    WaveBatchPicking.ScanExtension<WaveBatchPicking.ConfirmState.Logic>
  {
    public static bool IsActive() => PaperlessWaveBatchPicking.IsActive();

    /// Overrides <see cref="M:PX.Objects.SO.WMS.WaveBatchPicking.ConfirmState.Logic.Confirm" />
    [PXOverride]
    public FlowStatus Confirm(Func<FlowStatus> baseIgnored)
    {
      this.Basis.Get<PaperlessPicking>().EnsureLocationFromLastVisited();
      FlowStatus flowStatus = this.WSBasis.ConfirmSuitableSplits();
      bool? isError = ((FlowStatus) ref flowStatus).IsError;
      bool flag = false;
      if (!(isError.GetValueOrDefault() == flag & isError.HasValue))
        return flowStatus;
      SOPickerListEntry pickedSplit = ((PXCache) GraphHelper.Caches<SOPickerListEntry>((PXGraph) this.Graph)).Dirty.Cast<SOPickerListEntry>().First<SOPickerListEntry>();
      this.WSBasis.ReportSplitConfirmed(pickedSplit);
      this.WSBasis.EnsureShipmentUserLinkForWorksheetPick();
      this.Basis.Get<PaperlessPicking.ConfirmState.Logic>().VisitSplit(pickedSplit);
      return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
    }
  }

  /// Overrides <see cref="T:PX.Objects.SO.WMS.WorksheetPicking.ConfirmPickListCommand.Logic" />
  public class AlterConfirmPickListCommandLogic : 
    WorksheetPicking.ScanExtension<WorksheetPicking.ConfirmPickListCommand.Logic>
  {
    public static bool IsActive() => PaperlessWaveBatchPicking.IsActive();

    [PXOverride]
    public void ConfigureOnSuccessAction(
      ScanLongRunAwaiter<PickPackShip, SOPicker>.IResultProcessor onSuccess,
      Action<ScanLongRunAwaiter<PickPackShip, SOPicker>.IResultProcessor> base_ConfigureOnSuccessAction)
    {
      base_ConfigureOnSuccessAction(onSuccess);
      if (!(this.Basis.CurrentMode is WaveBatchPicking.BatchPickMode))
        return;
      PaperlessPicking.ConfirmPickListAndTakeNextCommand andTakeNextCommand = this.Basis.CurrentMode.Commands.OfType<PaperlessPicking.ConfirmPickListAndTakeNextCommand>().FirstOrDefault<PaperlessPicking.ConfirmPickListAndTakeNextCommand>();
      if (andTakeNextCommand == null)
        return;
      ScanHeader scanHeader = GraphHelper.RowCast<ScanLog>((IEnumerable) ((PXSelectBase<ScanLog>) this.Basis.Logs).Select(Array.Empty<object>())).Select<ScanLog, ScanHeader>((Func<ScanLog, ScanHeader>) (log => log.HeaderStateBefore)).SkipWhile<ScanHeader>((Func<ScanHeader, bool>) (h => h.InitialScanState == "SLOC")).FirstOrDefault<ScanHeader>();
      if (scanHeader == null || !(scanHeader.Barcode.Substring(1) == ((ScanCommand<PickPackShip>) andTakeNextCommand).Code))
        return;
      onSuccess.Do((Action<PickPackShip, SOPicker>) ((basis, picker) => ((ScanCommand<PickPackShip>) basis.CurrentMode.Commands.OfType<PaperlessPicking.TakeNextPickListCommand>().First<PaperlessPicking.TakeNextPickListCommand>()).Execute()));
    }
  }

  /// Overrides <see cref="T:PX.Objects.SO.WMS.ToteSupport" />
  public class AlterToteSupport : WorksheetPicking.ScanExtension<ToteSupport>
  {
    public static bool IsActive() => PaperlessWaveBatchPicking.IsActive();

    /// Overrides <see cref="P:PX.Objects.SO.WMS.ToteSupport.NoEmptyTotes" />
    [PXOverride]
    public bool get_NoEmptyTotes(Func<bool> base_NoEmptyTotes)
    {
      if (this.Basis.CurrentMode is WaveBatchPicking.WavePickMode)
      {
        SOPickerListEntry wantedSplit = this.Basis.Get<PaperlessPicking>().GetWantedSplit();
        if (wantedSplit != null)
        {
          int? toteId = wantedSplit.ToteID;
          int num1 = 0;
          if (!(toteId.GetValueOrDefault() == num1 & toteId.HasValue))
            return GraphHelper.RowCast<SOPickerListEntry>((IEnumerable) ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).SearchAll<Asc<SOPickerListEntry.toteID>>(new object[1]
            {
              (object) wantedSplit.ToteID
            }, Array.Empty<object>())).Any<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (s =>
            {
              Decimal? pickedQty = s.PickedQty;
              Decimal num2 = 0M;
              return pickedQty.GetValueOrDefault() > num2 & pickedQty.HasValue || s.ForceCompleted.GetValueOrDefault();
            }));
        }
      }
      return base_NoEmptyTotes();
    }

    /// Overrides <see cref="M:PX.Objects.SO.WMS.ToteSupport.GetShipmentToAddToteTo" />
    [PXOverride]
    public string GetShipmentToAddToteTo(Func<string> base_GetShipmentToAddToteTo)
    {
      if (!(this.Basis.CurrentMode is WaveBatchPicking.WavePickMode))
        return base_GetShipmentToAddToteTo();
      return this.Basis.Get<PaperlessPicking>().GetWantedSplit()?.ShipmentNbr;
    }

    /// Overrides <see cref="P:PX.Objects.SO.WMS.ToteSupport.IsToteSelectionNeeded" />
    [PXOverride]
    public bool get_IsToteSelectionNeeded(Func<bool> base_IsToteSelectionNeeded)
    {
      return this.Basis.CurrentMode is WaveBatchPicking.WavePickMode ? ((IEnumerable<SOPickerListEntry>) ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).SelectMain(Array.Empty<object>())).Where<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (s =>
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
      if (this.Basis.CurrentMode is WaveBatchPicking.WavePickMode)
      {
        PaperlessPicking paperlessPicking = this.Basis.Get<PaperlessPicking>();
        paperlessPicking.WantedLineNbr = paperlessPicking.GetNextWantedLineNbr();
      }
      return split;
    }

    [PXOverride]
    public ScanState<PickPackShip> DecorateScanState(
      ScanState<PickPackShip> original,
      Func<ScanState<PickPackShip>, ScanState<PickPackShip>> base_DecorateScanState)
    {
      ScanState<PickPackShip> scanState = base_DecorateScanState(original);
      if (((ScanComponent<PickPackShip>) scanState).ModeCode == "WAVE" && scanState is ToteSupport.SelectToteState selectTote)
        this.InjectSelectToteValidateAgainstWantedLine(selectTote);
      return scanState;
    }

    public virtual void InjectSelectToteValidateAgainstWantedLine(
      ToteSupport.SelectToteState selectTote)
    {
      ((MethodInterceptor<EntityState<PickPackShip, INTote>, PickPackShip>.OfFunc<INTote, Validation>.AsAppendable) ((EntityState<PickPackShip, INTote>) selectTote).Intercept.Validate).ByAppend((Func<Validation, INTote, Validation>) ((basis, tote) =>
      {
        PaperlessPicking paperlessPicking = basis.Get<PaperlessPicking>();
        bool? remove = basis.Remove;
        bool flag = false;
        if (remove.GetValueOrDefault() == flag & remove.HasValue)
        {
          SOPickerListEntry wantedSplit = paperlessPicking.GetWantedSplit();
          if (wantedSplit != null)
          {
            int? toteId1 = wantedSplit.ToteID;
            int? toteId2 = tote.ToteID;
            if (toteId1.GetValueOrDefault() == toteId2.GetValueOrDefault() & toteId1.HasValue == toteId2.HasValue)
              return Validation.Ok;
            HashSet<int?> hashSet = GraphHelper.RowCast<SOPickerToShipmentLink>((IEnumerable) ((PXSelectBase<SOPickerToShipmentLink>) paperlessPicking.WSBasis.ShipmentsOfPicker).SearchAll<Asc<SOPickerToShipmentLink.shipmentNbr>>(new object[1]
            {
              (object) wantedSplit.ShipmentNbr
            }, Array.Empty<object>())).Select<SOPickerToShipmentLink, int?>((Func<SOPickerToShipmentLink, int?>) (link => link.ToteID)).ToHashSet<int?>();
            if (!hashSet.Contains(tote.ToteID))
              return Validation.Fail("The {0} tote is not assigned to the {1} shipment. Available totes: {2}.", new object[3]
              {
                (object) tote.ToteCD,
                (object) wantedSplit.ShipmentNbr,
                (object) hashSet.Select<int?, string>((Func<int?, string>) (tid => INTote.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) basis), basis.SiteID, tid).ToteCD)).With<IEnumerable<string>, string>((Func<IEnumerable<string>, string>) (tcds => string.Join(", ", tcds)))
              });
          }
        }
        return Validation.Ok;
      }), new RelativeInject?());
    }

    /// Overrides <see cref="T:PX.Objects.SO.WMS.WaveBatchPicking.WavePickMode.AlterToteSupport" />
    public class SuppressWorksheetToteSupportChanges : 
      WorksheetPicking.ScanExtension<WaveBatchPicking.WavePickMode.AlterToteSupport>
    {
      public static bool IsActive() => PaperlessWaveBatchPicking.IsActive();

      /// Overrides <see cref="P:PX.Objects.SO.WMS.WaveBatchPicking.WavePickMode.AlterToteSupport.IsSuppressed" />
      [PXOverride]
      public bool get_IsSuppressed(Func<bool> base_IsSuppressed) => true;
    }
  }

  /// Overrides <see cref="T:PX.Objects.SO.WMS.PaperlessPicking" />
  public class AlterPaperlessPicking : PaperlessPicking.ScanExtension
  {
    public static bool IsActive() => PaperlessWaveBatchPicking.IsActive();

    /// Overrides <see cref="M:PX.Objects.SO.WMS.PaperlessPicking.EnsureShipmentUserLinkForPaperlessPick" />
    [PXOverride]
    public void EnsureShipmentUserLinkForPaperlessPick(
      Action base_EnsureShipmentUserLinkForPaperlessPick)
    {
      if (this.Basis.CurrentMode is WaveBatchPicking.WavePickMode || this.Basis.CurrentMode is WaveBatchPicking.BatchPickMode)
        return;
      base_EnsureShipmentUserLinkForPaperlessPick();
    }

    /// Overrides <see cref="M:PX.Objects.SO.WMS.WorksheetPicking.AssignUser(System.Boolean)" />
    [PXOverride]
    public bool AssignUser(bool startPicking, Func<bool, bool> base_AssignUser)
    {
      int num = base_AssignUser(startPicking) ? 1 : 0;
      if (num == 0)
        return num != 0;
      this.WSBasis.EnsureShipmentUserLinkForWorksheetPick();
      return num != 0;
    }
  }
}
