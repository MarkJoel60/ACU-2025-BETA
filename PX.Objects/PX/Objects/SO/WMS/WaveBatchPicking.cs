// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.WMS.WaveBatchPicking
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.WMS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.SO.WMS;

public class WaveBatchPicking : WorksheetPicking.ScanExtension
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.wMSAdvancedPicking>();

  public static bool MatchMode(
  #nullable disable
  string mode) => EnumerableExtensions.IsIn<string>(mode, "WAVE", "BTCH");

  [PXOverride]
  public IEnumerable<ScanMode<PickPackShip>> CreateScanModes(
    Func<IEnumerable<ScanMode<PickPackShip>>> base_CreateScanModes)
  {
    foreach (ScanMode<PickPackShip> scanMode in base_CreateScanModes())
      yield return scanMode;
    yield return (ScanMode<PickPackShip>) new WaveBatchPicking.WavePickMode();
    yield return (ScanMode<PickPackShip>) new WaveBatchPicking.BatchPickMode();
  }

  protected virtual void _(PX.Data.Events.RowSelected<ScanHeader> e)
  {
    if (e.Row == null)
      return;
    PXCacheEx.AdjustUI(((PXSelectBase) this.WSBasis.PickListOfPicker).Cache, (object) null).For<SOPickerListEntry.shipmentNbr>((Action<PXUIFieldAttribute>) (a => a.Visible = ((PXSelectBase<SOPickingWorksheet>) this.WSBasis.Worksheet).Current?.WorksheetType == "WV"));
  }

  public virtual void InjectPackLocationDeactivatedBasedOnShipmentSpecialPickType(
    WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState locationState)
  {
    ((MethodInterceptor<EntityState<PickPackShip, INLocation>, PickPackShip>.OfPredicate) ((EntityState<PickPackShip, INLocation>) locationState).Intercept.IsStateActive).ByConjoin((Func<PickPackShip, bool>) (basis =>
    {
      bool flag;
      switch (basis.Get<WorksheetPicking>().ShipmentSpecialPickType)
      {
        case "BT":
          flag = true;
          break;
        case "WV":
          flag = false;
          break;
        case null:
          flag = true;
          break;
        default:
          // ISSUE: reference to a compiler-generated method
          \u003CPrivateImplementationDetails\u003E.ThrowInvalidOperationException();
          break;
      }
      return flag;
    }), false, new RelativeInject?());
  }

  /// Overrides <see cref="M:PX.Objects.SO.WMS.WorksheetPicking.IsWorksheetMode(System.String)" />
  [PXOverride]
  public bool IsWorksheetMode(string modeCode, Func<string, bool> base_IsWorksheetMode)
  {
    return base_IsWorksheetMode(modeCode) || EnumerableExtensions.IsIn<string>(modeCode, "WAVE", "BTCH");
  }

  [PXOverride]
  public ScanMode<PickPackShip> FindModeForWorksheet(
    SOPickingWorksheet sheet,
    Func<SOPickingWorksheet, ScanMode<PickPackShip>> base_FindModeForWorksheet)
  {
    if (sheet.WorksheetType == "WV")
      return (ScanMode<PickPackShip>) this.Basis.FindMode<WaveBatchPicking.WavePickMode>();
    return sheet.WorksheetType == "BT" ? (ScanMode<PickPackShip>) this.Basis.FindMode<WaveBatchPicking.BatchPickMode>() : base_FindModeForWorksheet(sheet);
  }

  [PXOverride]
  public ScanState<PickPackShip> DecorateScanState(
    ScanState<PickPackShip> original,
    Func<ScanState<PickPackShip>, ScanState<PickPackShip>> base_DecorateScanState)
  {
    ScanState<PickPackShip> scanState = base_DecorateScanState(original);
    if (WaveBatchPicking.MatchMode(((ScanComponent<PickPackShip>) scanState).ModeCode))
    {
      switch (scanState)
      {
        case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState locationState1:
          this.Basis.InjectLocationDeactivationOnDefaultLocationOption(locationState1);
          this.Basis.InjectLocationSkippingOnPromptLocationForEveryLineOption(locationState1);
          break;
        case WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState inventoryState:
          this.Basis.InjectItemAbsenceHandlingByLocation(inventoryState);
          break;
      }
    }
    else if (((ScanComponent<PickPackShip>) scanState).ModeCode == "PACK" && scanState is WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState locationState2)
      this.InjectPackLocationDeactivatedBasedOnShipmentSpecialPickType(locationState2);
    return scanState;
  }

  /// Overrides <see cref="M:PX.BarcodeProcessing.BarcodeDrivenStateMachine`2.OnBeforeFullClear" />
  [PXOverride]
  public void OnBeforeFullClear(Action base_OnBeforeFullClear)
  {
    base_OnBeforeFullClear();
    if (!(this.Basis.CurrentMode is WaveBatchPicking.WavePickMode) && !(this.Basis.CurrentMode is WaveBatchPicking.BatchPickMode) || this.WSBasis.WorksheetNbr == null || !this.WSBasis.PickerNbr.HasValue || !this.Graph.WorkLogExt.SuspendFor(this.WSBasis.WorksheetNbr, this.WSBasis.PickerNbr.Value, new Guid?(((PXGraph) this.Graph).Accessinfo.UserID), "PICK"))
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
    bool flag = this.WSBasis.PickerNbr.HasValue && !string.IsNullOrEmpty(this.WSBasis.WorksheetNbr);
    if (!(WaveBatchPicking.MatchMode(this.Basis.CurrentMode.Code) & flag))
      return;
    workLogger.LogScanFor(this.WSBasis.WorksheetNbr, this.WSBasis.PickerNbr.Value, ((PXGraph) this.Graph).Accessinfo.UserID, "PICK", isError);
  }

  public sealed class WavePickMode : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanMode
  {
    public const string Value = "WAVE";

    public WaveBatchPicking WBBasis => this.Get<WaveBatchPicking>();

    public virtual string Code => "WAVE";

    public virtual string Description => "Wave Pick";

    protected virtual bool IsModeActive() => ((ScanMode<PickPackShip>) this).Basis.HasPick;

    protected virtual IEnumerable<ScanState<PickPackShip>> CreateStates()
    {
      yield return (ScanState<PickPackShip>) new WaveBatchPicking.WavePickMode.PickListState();
      yield return (ScanState<PickPackShip>) new ToteSupport.AssignToteState();
      yield return (ScanState<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState();
      yield return (ScanState<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState()
      {
        AlternateType = new INPrimaryAlternateType?(INPrimaryAlternateType.CPN),
        IsForIssue = true,
        SuppressModuleItemStatusCheck = true
      };
      yield return (ScanState<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState();
      yield return (ScanState<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState()
      {
        IsForIssue = true
      };
      yield return (ScanState<PickPackShip>) new ToteSupport.SelectToteState();
      yield return (ScanState<PickPackShip>) new WaveBatchPicking.WavePickMode.ConfirmToteState();
      yield return (ScanState<PickPackShip>) new WaveBatchPicking.ConfirmState();
      yield return (ScanState<PickPackShip>) new WaveBatchPicking.WavePickMode.ShipmentToteState();
    }

    protected virtual IEnumerable<ScanTransition<PickPackShip>> CreateTransitions()
    {
      return ((ScanMode<PickPackShip>) this).StateFlow((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (flow => flow.ForkBy((Func<PickPackShip, bool>) (b => !b.Remove.GetValueOrDefault())).PositiveBranch((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (directFlow => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) directFlow.From<WaveBatchPicking.WavePickMode.PickListState>().NextTo<ToteSupport.AssignToteState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState>((Action<PickPackShip>) null)).NextTo<ToteSupport.SelectToteState>((Action<PickPackShip>) null)).NextTo<WaveBatchPicking.WavePickMode.ConfirmToteState>((Action<PickPackShip>) null))).NegativeBranch((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (removeFlow => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) removeFlow.From<WaveBatchPicking.WavePickMode.PickListState>().NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>((Action<PickPackShip>) null)))));
    }

    protected virtual IEnumerable<ScanCommand<PickPackShip>> CreateCommands()
    {
      yield return (ScanCommand<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.RemoveCommand();
      yield return (ScanCommand<PickPackShip>) new BarcodeQtySupport<PickPackShip, PickPackShip.Host>.SetQtyCommand();
      yield return (ScanCommand<PickPackShip>) new WorksheetPicking.ConfirmPickListCommand();
      yield return (ScanCommand<PickPackShip>) new ToteSupport.AddToteCommand();
    }

    protected virtual IEnumerable<ScanRedirect<PickPackShip>> CreateRedirects()
    {
      return AllWMSRedirects.CreateFor<PickPackShip>();
    }

    protected virtual void ResetMode(bool fullReset)
    {
      ((ScanMode<PickPackShip>) this).ResetMode(fullReset);
      ((ScanMode<PickPackShip>) this).Clear<WaveBatchPicking.WavePickMode.PickListState>(fullReset && !((ScanMode<PickPackShip>) this).Basis.IsWithinReset);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>(fullReset || ((ScanMode<PickPackShip>) this).Basis.PromptLocationForEveryLine);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>(fullReset);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>(true);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState>(true);
      ((ScanMode<PickPackShip>) this).Clear<ToteSupport.SelectToteState>(true);
      ((ScanMode<PickPackShip>) this).Clear<ToteSupport.AssignToteState>(true);
      ((ScanMode<PickPackShip>) this).Clear<WaveBatchPicking.WavePickMode.ShipmentToteState>(true);
    }

    public class value : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WaveBatchPicking.WavePickMode.value>
    {
      public value()
        : base("WAVE")
      {
      }
    }

    public sealed class PickListState : WorksheetPicking.PickListState
    {
      protected override string WorksheetType => "WV";
    }

    public sealed class ConfirmToteState : ToteSupport.ToteState
    {
      public const string Value = "CNFT";

      public virtual string Code => "CNFT";

      protected virtual string StatePrompt
      {
        get
        {
          return ((ScanComponent<PickPackShip>) this).Basis.Localize("Scan the {0} tote to confirm picking of the items.", new object[1]
          {
            (object) this.ToteBasis.GetProperTote()?.ToteCD
          });
        }
      }

      protected virtual bool IsStateActive()
      {
        return ((ScanComponent<PickPackShip>) this).Basis.Get<WaveBatchPicking.WavePickMode.ConfirmToteState.Logic>().ConfirmToteForEveryLine;
      }

      protected virtual bool IsStateSkippable() => this.ToteBasis.GetProperTote() == null;

      protected override Validation Validate(INTote tote)
      {
        Validation validation;
        if (((ScanComponent<PickPackShip>) this).Basis.HasFault<INTote>(tote, new Func<INTote, Validation>(((ToteSupport.ToteState) this).Validate), ref validation))
          return validation;
        int? toteId1 = this.ToteBasis.GetProperTote().ToteID;
        int? toteId2 = tote.ToteID;
        if (toteId1.GetValueOrDefault() == toteId2.GetValueOrDefault() & toteId1.HasValue == toteId2.HasValue)
          return Validation.Ok;
        return Validation.Fail("Incorrect tote {0} scanned.", new object[1]
        {
          (object) tote.ToteCD
        });
      }

      public class value : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        WaveBatchPicking.WavePickMode.ConfirmToteState.value>
      {
        public value()
          : base("CNFT")
        {
        }
      }

      public class Logic : WorksheetPicking.ScanExtension
      {
        public static bool IsActive() => WorksheetPicking.ScanExtension.IsActiveBase();

        public virtual bool ConfirmToteForEveryLine
        {
          get
          {
            if (((PXSelectBase<SOPickPackShipSetup>) this.Basis.Setup).Current.ConfirmToteForEachItem.GetValueOrDefault())
            {
              bool? remove = this.Basis.Remove;
              bool flag = false;
              if (remove.GetValueOrDefault() == flag & remove.HasValue && this.WSBasis.WorksheetNbr != null && ((PXSelectBase<SOPickingWorksheet>) this.WSBasis.Worksheet).Current?.WorksheetType == "WV")
                return ((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).Select(Array.Empty<object>()).Count > 1;
            }
            return false;
          }
        }
      }

      [PXLocalizable]
      public new abstract class Msg : ToteSupport.ToteState.Msg
      {
        public const string Prompt = "Scan the {0} tote to confirm picking of the items.";
        public const string Mismatch = "Incorrect tote {0} scanned.";
      }
    }

    public sealed class ShipmentToteState : ToteSupport.ToteState
    {
      public const string Value = "SHTO";

      public virtual string Code => "SHTO";

      protected virtual string StatePrompt
      {
        get
        {
          return ((ScanComponent<PickPackShip>) this).Basis.Localize("Scan the tote that is already assigned to a shipment to add another tote to this shipment.", Array.Empty<object>());
        }
      }

      protected virtual bool IsStateActive()
      {
        return ((EntityState<PickPackShip, INTote>) this).IsStateActive() && EnumerableExtensions.Distinct<SOPickerToShipmentLink, string>((IEnumerable<SOPickerToShipmentLink>) ((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).SelectMain(Array.Empty<object>()), (Func<SOPickerToShipmentLink, string>) (link => link.ShipmentNbr)).Count<SOPickerToShipmentLink>() > 1;
      }

      protected virtual bool IsStateSkippable()
      {
        return ((ScanComponent<PickPackShip>) this).Basis.Get<WaveBatchPicking.WavePickMode.AlterToteSupport>().ToteShipmentNbr != null;
      }

      protected override Validation Validate(INTote tote1)
      {
        Validation validation;
        if (((ScanComponent<PickPackShip>) this).Basis.HasFault<INTote>(tote1, new Func<INTote, Validation>(((ToteSupport.ToteState) this).Validate), ref validation))
          return validation;
        SOPickerToShipmentLink pickerToShipmentLink = PXResultset<SOPickerToShipmentLink>.op_Implicit(((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).Search<SOPickerToShipmentLink.toteID>((object) tote1.ToteID, Array.Empty<object>()));
        if (pickerToShipmentLink == null)
          return Validation.Fail("The {0} tote is not assigned to the current pick list. Available totes: {1}.", new object[2]
          {
            (object) tote1.ToteCD,
            (object) ((IEnumerable<SOPickerToShipmentLink>) ((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).SelectMain(Array.Empty<object>())).Select<SOPickerToShipmentLink, int?>((Func<SOPickerToShipmentLink, int?>) (link => link.ToteID)).Select<int?, string>((Func<int?, string>) (tid => INTote.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) ((ScanComponent<PickPackShip>) this).Basis), ((ScanComponent<PickPackShip>) this).Basis.SiteID, tid).ToteCD)).With<IEnumerable<string>, string>((Func<IEnumerable<string>, string>) (tcds => string.Join(", ", tcds)))
          });
        IGrouping<int?, SOPickerListEntry> grouping = GraphHelper.RowCast<SOPickerListEntry>((IEnumerable) ((PXSelectBase<SOPickerListEntry>) this.WSBasis.PickListOfPicker).SearchAll<Asc<SOPickerListEntry.shipmentNbr>>(new object[1]
        {
          (object) pickerToShipmentLink.ShipmentNbr
        }, Array.Empty<object>())).GroupBy<SOPickerListEntry, int?>((Func<SOPickerListEntry, int?>) (s => s.ToteID)).FirstOrDefault<IGrouping<int?, SOPickerListEntry>>((Func<IGrouping<int?, SOPickerListEntry>, bool>) (tote2 => tote2.All<SOPickerListEntry>((Func<SOPickerListEntry, bool>) (s =>
        {
          Decimal? pickedQty = s.PickedQty;
          Decimal num = 0M;
          return pickedQty.GetValueOrDefault() == num & pickedQty.HasValue;
        }))));
        if (grouping == null)
          return Validation.Ok;
        return Validation.Fail("The {0} shipment has an empty tote. Use the {1} tote instead of adding a new one.", new object[2]
        {
          (object) pickerToShipmentLink.ShipmentNbr,
          (object) INTote.PK.Find(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) ((ScanComponent<PickPackShip>) this).Basis), pickerToShipmentLink.SiteID, grouping.Key).ToteCD
        });
      }

      protected virtual void Apply(INTote tote)
      {
        ((ScanComponent<PickPackShip>) this).Basis.Get<WaveBatchPicking.WavePickMode.AlterToteSupport>().ToteShipmentNbr = PXResultset<SOPickerToShipmentLink>.op_Implicit(((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).Search<SOPickerToShipmentLink.toteID>((object) tote.ToteID, Array.Empty<object>())).ShipmentNbr;
      }

      protected virtual void ClearState()
      {
        ((ScanComponent<PickPackShip>) this).Basis.Get<WaveBatchPicking.WavePickMode.AlterToteSupport>().ToteShipmentNbr = (string) null;
      }

      protected virtual void SetNextState()
      {
        ((ScanComponent<PickPackShip>) this).Basis.SetScanState<ToteSupport.AssignToteState>((string) null, Array.Empty<object>());
      }

      public class value : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        WaveBatchPicking.WavePickMode.ShipmentToteState.value>
      {
        public value()
          : base("SHTO")
        {
        }
      }

      [PXLocalizable]
      public new abstract class Msg : ToteSupport.ToteState.Msg
      {
        public const string Prompt = "Scan the tote that is already assigned to a shipment to add another tote to this shipment.";
        public const string ThereIsNotStartedTote = "The {0} shipment has an empty tote. Use the {1} tote instead of adding a new one.";
      }
    }

    /// Overrides <see cref="T:PX.Objects.SO.WMS.ToteSupport" />
    public class AlterToteSupport : WorksheetPicking.ScanExtension<ToteSupport>
    {
      public static bool IsActive() => WorksheetPicking.ScanExtension<ToteSupport>.IsActiveBase();

      protected virtual bool IsSuppressed => false;

      /// Overrides <see cref="P:PX.Objects.SO.WMS.ToteSupport.CanAddNewTote" />
      [PXOverride]
      public bool get_CanAddNewTote(Func<bool> base_CanAddNewTote)
      {
        if (!(this.Basis.CurrentMode is WaveBatchPicking.WavePickMode) || this.IsSuppressed)
          return base_CanAddNewTote();
        return this.Target.AllowMultipleTotesPerShipment && this.WSBasis.CanWSPick && !this.Target.HasAnotherToAssign;
      }

      /// Overrides <see cref="M:PX.Objects.SO.WMS.ToteSupport.GetShipmentToAddToteTo" />
      [PXOverride]
      public string GetShipmentToAddToteTo(Func<string> base_GetShipmentToAddToteTo)
      {
        if (!(this.Basis.CurrentMode is WaveBatchPicking.WavePickMode) || this.IsSuppressed)
          return base_GetShipmentToAddToteTo();
        string toteShipmentNbr = this.ToteShipmentNbr;
        if (toteShipmentNbr != null)
          return toteShipmentNbr;
        return ((PXSelectBase<SOPickerToShipmentLink>) this.WSBasis.ShipmentsOfPicker).Select(Array.Empty<object>()).TopFirst?.ShipmentNbr;
      }

      /// Overrides <see cref="P:PX.Objects.SO.WMS.ToteSupport.IsToteSelectionNeeded" />
      [PXOverride]
      public bool get_IsToteSelectionNeeded(Func<bool> base_IsToteSelectionNeeded)
      {
        return this.Basis.CurrentMode is WaveBatchPicking.WavePickMode && !this.IsSuppressed || base_IsToteSelectionNeeded();
      }

      /// Overrides <see cref="M:PX.Objects.SO.WMS.WorksheetPicking.GetEntriesToPick" />
      [PXOverride]
      public IEnumerable<SOPickerListEntry> GetEntriesToPick(
        Func<IEnumerable<SOPickerListEntry>> base_GetEntriesToPick)
      {
        WaveBatchPicking.WavePickMode.AlterToteSupport alterToteSupport = this;
        bool hasExactRecords = false;
        foreach (SOPickerListEntry soPickerListEntry in base_GetEntriesToPick())
        {
          hasExactRecords = true;
          yield return soPickerListEntry;
        }
        if (alterToteSupport.Basis.CurrentMode is WaveBatchPicking.WavePickMode && !alterToteSupport.IsSuppressed && !hasExactRecords && alterToteSupport.Target.ToteID.HasValue)
        {
          int? originalToteID = alterToteSupport.Target.ToteID;
          try
          {
            alterToteSupport.Target.ToteID = new int?();
            foreach (SOPickerListEntry soPickerListEntry in base_GetEntriesToPick())
              yield return soPickerListEntry;
          }
          finally
          {
            this.Target.ToteID = originalToteID;
          }
          originalToteID = new int?();
        }
      }

      [PXOverride]
      public ScanState<PickPackShip> DecorateScanState(
        ScanState<PickPackShip> original,
        Func<ScanState<PickPackShip>, ScanState<PickPackShip>> base_DecorateScanState)
      {
        ScanState<PickPackShip> scanState = base_DecorateScanState(original);
        if (!(scanState is ToteSupport.AssignToteState assignTote) || !(((ScanComponent<PickPackShip>) assignTote).ModeCode == "WAVE") || this.IsSuppressed)
          return scanState;
        this.InjectAssignToteTakeOverToShipmentTote(assignTote);
        return scanState;
      }

      [PXOverride]
      public ScanCommand<PickPackShip> DecorateScanCommand(
        ScanCommand<PickPackShip> original,
        Func<ScanCommand<PickPackShip>, ScanCommand<PickPackShip>> base_DecorateScanCommand)
      {
        ScanCommand<PickPackShip> scanCommand = base_DecorateScanCommand(original);
        if (!(scanCommand is WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.RemoveCommand remove) || !(((ScanComponent<PickPackShip>) remove).ModeCode == "WAVE"))
          return scanCommand;
        this.Target.InjectRemoveDisableWhenAssignTote(remove);
        this.Target.InjectRemoveMovesToRemoveFromTote(remove);
        return scanCommand;
      }

      public virtual void InjectAssignToteTakeOverToShipmentTote(
        ToteSupport.AssignToteState assignTote)
      {
        ((MethodInterceptor<EntityState<PickPackShip, INTote>, PickPackShip>.OfAction) ((EntityState<PickPackShip, INTote>) assignTote).Intercept.OnTakingOver).ByOverride((Action<PickPackShip, Action>) ((basis, base_OnTakingOver) =>
        {
          WaveBatchPicking.WavePickMode.ShipmentToteState state = basis.FindState<WaveBatchPicking.WavePickMode.ShipmentToteState>(false);
          if (state == null || !((ScanState<PickPackShip>) state).IsActive || ((ScanState<PickPackShip>) state).IsSkippable || !this.Target.AddNewTote.GetValueOrDefault())
            return;
          basis.SetScanState<WaveBatchPicking.WavePickMode.ShipmentToteState>((string) null, Array.Empty<object>());
        }), new RelativeInject?());
      }

      public WaveBatchPicking.WavePickMode.AlterToteSupport.ShipmentToteScanHeader ShipmentToteHeader
      {
        get
        {
          return ScanHeaderExt.Get<WaveBatchPicking.WavePickMode.AlterToteSupport.ShipmentToteScanHeader>(this.Basis.Header) ?? new WaveBatchPicking.WavePickMode.AlterToteSupport.ShipmentToteScanHeader();
        }
      }

      public ValueSetter<ScanHeader>.Ext<WaveBatchPicking.WavePickMode.AlterToteSupport.ShipmentToteScanHeader> ShipmentToteSetter
      {
        get
        {
          return this.Basis.HeaderSetter.With<WaveBatchPicking.WavePickMode.AlterToteSupport.ShipmentToteScanHeader>();
        }
      }

      public string ToteShipmentNbr
      {
        get => this.ShipmentToteHeader.ToteShipmentNbr;
        set
        {
          ValueSetter<ScanHeader>.Ext<WaveBatchPicking.WavePickMode.AlterToteSupport.ShipmentToteScanHeader> shipmentToteSetter = this.ShipmentToteSetter;
          (^ref shipmentToteSetter).Set<string>((Expression<Func<WaveBatchPicking.WavePickMode.AlterToteSupport.ShipmentToteScanHeader, string>>) (h => h.ToteShipmentNbr), value);
        }
      }

      public sealed class ShipmentToteScanHeader : 
        PXCacheExtension<WorksheetScanHeader, WMSScanHeader, QtyScanHeader, ScanHeader>
      {
        public static bool IsActive() => WaveBatchPicking.WavePickMode.AlterToteSupport.IsActive();

        [PXString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXSelector(typeof (PX.Objects.SO.SOShipment.shipmentNbr))]
        public string ToteShipmentNbr { get; set; }

        public abstract class toteShipmentNbr : 
          BqlType<
          #nullable enable
          IBqlString, string>.Field<
          #nullable disable
          WaveBatchPicking.WavePickMode.AlterToteSupport.ShipmentToteScanHeader.toteShipmentNbr>
        {
        }
      }
    }

    [PXLocalizable]
    public abstract class Msg : ScanMode<PickPackShip>.Msg
    {
      public const string DisplayName = "Wave Pick";
    }
  }

  public sealed class BatchPickMode : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanMode
  {
    public const string Value = "BTCH";

    public WaveBatchPicking WBBasis => this.Get<WaveBatchPicking>();

    public virtual string Code => "BTCH";

    public virtual string Description => "Batch Pick";

    protected virtual bool IsModeActive() => ((ScanMode<PickPackShip>) this).Basis.HasPick;

    protected virtual IEnumerable<ScanState<PickPackShip>> CreateStates()
    {
      WaveBatchPicking.BatchPickMode batchPickMode = this;
      yield return (ScanState<PickPackShip>) new WaveBatchPicking.BatchPickMode.PickListState();
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
      yield return (ScanState<PickPackShip>) new WaveBatchPicking.ConfirmState();
      PPSCartSupport ppsCartSupport = batchPickMode.Get<PPSCartSupport>();
      if (ppsCartSupport != null && ppsCartSupport.IsCartRequired())
        yield return (ScanState<PickPackShip>) ((MethodInterceptor<EntityState<PickPackShip, INCart>, PickPackShip>.OfAction<INCart>) new CartSupport<PickPackShip, PickPackShip.Host>.CartState().Intercept.Apply).ByAppend((Action<PickPackShip, INCart>) ((basis, cart) =>
        {
          WorksheetPicking worksheetPicking = basis.Get<WorksheetPicking>();
          if (((PXSelectBase<SOPicker>) worksheetPicking.Picker).Current == null)
            return;
          ((PXSelectBase<SOPicker>) worksheetPicking.Picker).Current.CartID = cart.CartID;
          ((PXSelectBase<SOPicker>) worksheetPicking.Picker).UpdateCurrent();
        }), new RelativeInject?());
      yield return (ScanState<PickPackShip>) new WaveBatchPicking.BatchPickMode.SortingLocationState();
    }

    protected virtual IEnumerable<ScanTransition<PickPackShip>> CreateTransitions()
    {
      PPSCartSupport ppsCartSupport = this.Get<PPSCartSupport>();
      return ppsCartSupport != null && ppsCartSupport.IsCartRequired() ? ((ScanMode<PickPackShip>) this).StateFlow((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (flow => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) flow.From<WaveBatchPicking.BatchPickMode.PickListState>().NextTo<CartSupport<PickPackShip, PickPackShip.Host>.CartState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState>((Action<PickPackShip>) null))) : ((ScanMode<PickPackShip>) this).StateFlow((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (flow => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) flow.From<WaveBatchPicking.BatchPickMode.PickListState>().NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState>((Action<PickPackShip>) null)));
    }

    protected virtual IEnumerable<ScanCommand<PickPackShip>> CreateCommands()
    {
      yield return (ScanCommand<PickPackShip>) new WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.RemoveCommand();
      yield return (ScanCommand<PickPackShip>) new BarcodeQtySupport<PickPackShip, PickPackShip.Host>.SetQtyCommand();
      yield return (ScanCommand<PickPackShip>) new WorksheetPicking.ConfirmPickListCommand();
    }

    protected virtual IEnumerable<ScanRedirect<PickPackShip>> CreateRedirects()
    {
      return AllWMSRedirects.CreateFor<PickPackShip>();
    }

    protected virtual void ResetMode(bool fullReset)
    {
      ((ScanMode<PickPackShip>) this).ResetMode(fullReset);
      ((ScanMode<PickPackShip>) this).Clear<WaveBatchPicking.BatchPickMode.PickListState>(fullReset && !((ScanMode<PickPackShip>) this).Basis.IsWithinReset);
      ((ScanMode<PickPackShip>) this).Clear<CartSupport<PickPackShip, PickPackShip.Host>.CartState>(fullReset && !((ScanMode<PickPackShip>) this).Basis.IsWithinReset);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>(fullReset || ((ScanMode<PickPackShip>) this).Basis.PromptLocationForEveryLine);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>(fullReset);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>(true);
      ((ScanMode<PickPackShip>) this).Clear<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState>(true);
    }

    public class value : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WaveBatchPicking.BatchPickMode.value>
    {
      public value()
        : base("BTCH")
      {
      }
    }

    public sealed class PickListState : WorksheetPicking.PickListState
    {
      protected override string WorksheetType => "BT";
    }

    public sealed class SortingLocationState : 
      BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.EntityState<INLocation>
    {
      public const string Value = "SLOC";

      public virtual string Code => "SLOC";

      protected virtual string StatePrompt => "Scan the sorting location.";

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
        if (!location.Active.GetValueOrDefault())
          return Validation.Fail("Location '{0}' is inactive", new object[1]
          {
            (object) location.LocationCD
          });
        if (location.IsSorting.GetValueOrDefault())
          return Validation.Ok;
        return Validation.Fail("The {0} location cannot be selected because it is not a sorting location.", new object[1]
        {
          (object) location.LocationCD
        });
      }

      protected virtual void Apply(INLocation location)
      {
        ((ScanComponent<PickPackShip>) this).Basis.Get<WorksheetPicking.ConfirmPickListCommand.Logic>().ConfirmPickList(new int?(location.LocationID.Value));
      }

      protected virtual void ReportSuccess(INLocation location)
      {
        ((ScanComponent<PickPackShip>) this).Basis.Reporter.Info("{0} sorting location selected.", new object[1]
        {
          (object) location.LocationCD
        });
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
      }

      public class value : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        WaveBatchPicking.BatchPickMode.SortingLocationState.value>
      {
        public value()
          : base("SLOC")
        {
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string Prompt = "Scan the sorting location.";
        public const string Ready = "{0} sorting location selected.";
        public const string Missing = "{0} location not found in {1} warehouse.";
        public const string NotSorting = "The {0} location cannot be selected because it is not a sorting location.";
      }
    }

    [PXLocalizable]
    public abstract class Msg : ScanMode<PickPackShip>.Msg
    {
      public const string DisplayName = "Batch Pick";
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
      return ((ScanComponent<PickPackShip>) this).Basis.Get<WaveBatchPicking.ConfirmState.Logic>().Confirm();
    }

    public class Logic : WaveBatchPicking.ScanExtension
    {
      public static bool IsActive() => WaveBatchPicking.ScanExtension.IsActiveBase();

      public virtual FlowStatus Confirm()
      {
        FlowStatus flowStatus = this.WSBasis.ConfirmSuitableSplits();
        bool? isError = ((FlowStatus) ref flowStatus).IsError;
        bool flag = false;
        if (!(isError.GetValueOrDefault() == flag & isError.HasValue))
          return flowStatus;
        this.WSBasis.ReportSplitConfirmed(((PXCache) GraphHelper.Caches<SOPickerListEntry>((PXGraph) this.Graph)).Dirty.Cast<SOPickerListEntry>().First<SOPickerListEntry>());
        this.WSBasis.EnsureShipmentUserLinkForWorksheetPick();
        return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
      }
    }
  }

  /// Overrides <see cref="T:PX.Objects.SO.WMS.WorksheetPicking.ConfirmPickListCommand.Logic" />
  public class AlterConfirmPickListCommandLogic : 
    WorksheetPicking.ScanExtension<WorksheetPicking.ConfirmPickListCommand.Logic>
  {
    public static bool IsActive() => WaveBatchPicking.IsActive();

    /// Overrides <see cref="M:PX.Objects.SO.WMS.WorksheetPicking.ConfirmPickListCommand.Logic.ConfirmPickList(System.Nullable{System.Int32})" />
    [PXOverride]
    public void ConfirmPickList(int? sortingLocationID, Action<int?> base_ConfirmPickList)
    {
      if (!sortingLocationID.HasValue && ((PXSelectBase<SOPickingWorksheet>) ((PXGraphExtension<WorksheetPicking, PickPackShip, PickPackShip.Host>) this).Base2.Worksheet).Current.WorksheetType == "BT")
        ((PXGraphExtension<PickPackShip, PickPackShip.Host>) this).Base1.SetScanState<WaveBatchPicking.BatchPickMode.SortingLocationState>((string) null, Array.Empty<object>());
      else
        base_ConfirmPickList(sortingLocationID);
    }

    /// Overrides <see cref="M:PX.Objects.SO.WMS.WorksheetPicking.ConfirmPickListCommand.Logic.ReportConfirmationInPart" />
    [PXOverride]
    public void ReportConfirmationInPart(Action base_ReportConfirmationInPart)
    {
      if (this.Basis.CurrentMode is WaveBatchPicking.BatchPickMode)
        this.Basis.ReportError("The batch pick list cannot be confirmed because it is not complete.", Array.Empty<object>());
      else
        base_ReportConfirmationInPart();
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string BatchCannotBeConfirmedInPart = "The batch pick list cannot be confirmed because it is not complete.";
    }
  }

  /// Overrides <see cref="T:PX.Objects.SO.WMS.PickPackShip.PackMode.ConfirmState.Logic" />
  public class AlterPackConfirmLogic : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension<PickPackShip.PackMode.ConfirmState.Logic>
  {
    public static bool IsActive() => WaveBatchPicking.IsActive();

    protected WorksheetPicking WSBasis
    {
      get
      {
        return ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Get<WorksheetPicking>();
      }
    }

    /// Overrides <see cref="M:PX.Objects.SO.WMS.PickPackShip.PackMode.ConfirmState.Logic.TargetQty(PX.Objects.SO.SOShipLineSplit)" />
    [PXOverride]
    public Decimal? TargetQty(SOShipLineSplit split, Func<SOShipLineSplit, Decimal?> base_TargetQty)
    {
      Decimal? nullable;
      switch (this.WSBasis.ShipmentSpecialPickType)
      {
        case "BT":
          Decimal? pickedQty = split.PickedQty;
          Decimal qtyThreshold = this.Graph.GetQtyThreshold(split);
          nullable = pickedQty.HasValue ? new Decimal?(pickedQty.GetValueOrDefault() * qtyThreshold) : new Decimal?();
          break;
        case "WV":
          nullable = split.PickedQty;
          break;
        default:
          nullable = base_TargetQty(split);
          break;
      }
      return nullable;
    }
  }

  /// Overrides <see cref="T:PX.Objects.SO.WMS.PPSCartSupport" />
  public class AlterCartSupport : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension<PPSCartSupport>
  {
    public static bool IsActive() => WaveBatchPicking.IsActive() && PPSCartSupport.IsActive();

    /// Overrides <see cref="M:PX.Objects.SO.WMS.PPSCartSupport.IsCartRequired" />
    [PXOverride]
    public bool IsCartRequired(Func<bool> base_IsCartRequired)
    {
      if (base_IsCartRequired())
        return true;
      return ((PXSelectBase<SOPickPackShipSetup>) ((PickPackShip) this.Basis).Setup).Current.UseCartsForPick.GetValueOrDefault() && ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Header.Mode == "BTCH";
    }
  }

  public abstract class ScanExtension : 
    PXGraphExtension<WaveBatchPicking, WorksheetPicking, PickPackShip, PickPackShip.Host>
  {
    protected static bool IsActiveBase() => WaveBatchPicking.IsActive();

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

    public WaveBatchPicking WBBasis
    {
      [DebuggerStepThrough, DebuggerStepperBoundary] get => this.Base3;
    }
  }

  public abstract class ScanExtension<TTargetExtension> : 
    PXGraphExtension<TTargetExtension, WaveBatchPicking, WorksheetPicking, PickPackShip, PickPackShip.Host>
    where TTargetExtension : PXGraphExtension<WaveBatchPicking, WorksheetPicking, PickPackShip, PickPackShip.Host>
  {
    protected static bool IsActiveBase() => WaveBatchPicking.IsActive();

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

    public WaveBatchPicking WBBasis
    {
      [DebuggerStepThrough, DebuggerStepperBoundary] get
      {
        return ((PXGraphExtension<WaveBatchPicking, WorksheetPicking, PickPackShip, PickPackShip.Host>) this).Base3;
      }
    }

    public TTargetExtension Target
    {
      [DebuggerStepThrough, DebuggerStepperBoundary] get => this.Base4;
    }
  }
}
