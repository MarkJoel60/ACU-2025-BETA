// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.INScanTransfer
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
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.IN.WMS;

public class INScanTransfer : 
  INScanRegisterBase<
  #nullable disable
  INScanTransfer, INScanTransfer.Host, INDocType.transfer>
{
  public virtual bool ExplicitConfirmation
  {
    get
    {
      return ((PXSelectBase<INScanSetup>) this.Setup).Current.ExplicitLineConfirmation.GetValueOrDefault();
    }
  }

  public override bool PromptLocationForEveryLine
  {
    get
    {
      return ((PXSelectBase<INScanSetup>) this.Setup).Current.RequestLocationForEachItemInTransfer.GetValueOrDefault();
    }
  }

  public override bool UseDefaultReasonCode
  {
    get
    {
      return ((PXSelectBase<INScanSetup>) this.Setup).Current.UseDefaultReasonCodeInTransfer.GetValueOrDefault();
    }
  }

  public override bool UseDefaultWarehouse
  {
    get
    {
      return PXSetupBase<INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.UserSetup, INScanTransfer.Host, ScanHeader, INScanUserSetup, Where<INScanUserSetup.userID, Equal<Current<AccessInfo.userID>>, And<INScanUserSetup.mode, Equal<Current<INScanUserSetup.mode>>>>>.For(this.Graph).DefaultWarehouse.GetValueOrDefault();
    }
  }

  public virtual bool UseDefaultLotSerialNbr
  {
    get
    {
      return PXSetupBase<INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.UserSetup, INScanTransfer.Host, ScanHeader, INScanUserSetup, Where<INScanUserSetup.userID, Equal<Current<AccessInfo.userID>>, And<INScanUserSetup.mode, Equal<Current<INScanUserSetup.mode>>>>>.For(this.Graph).UseDefaultLotSerialNbr.GetValueOrDefault();
    }
  }

  protected override bool UseQtyCorrection
  {
    get
    {
      return !((PXSelectBase<INScanSetup>) this.Setup).Current.UseDefaultQtyInTransfer.GetValueOrDefault();
    }
  }

  protected override bool CanOverrideQty
  {
    get
    {
      return (!this.DocumentLoaded || this.NotReleasedAndHasLines) && !this.LotSerialTrack.IsTrackedSerial;
    }
  }

  public virtual void Initialize()
  {
    base.Initialize();
    ((PXGraphExtension<INScanTransfer.Host>) this).Base.SuppressLocationDefaultingForWMS = true;
  }

  public TransferScanHeader TransferHeader => ScanHeaderExt.Get<TransferScanHeader>(this.Header);

  public ValueSetter<ScanHeader>.Ext<TransferScanHeader> TransferSetter
  {
    get => this.HeaderSetter.With<TransferScanHeader>();
  }

  public int? TransferToSiteID
  {
    get => this.TransferHeader.TransferToSiteID;
    set
    {
      ValueSetter<ScanHeader>.Ext<TransferScanHeader> transferSetter = this.TransferSetter;
      (^ref transferSetter).Set<int?>((Expression<Func<TransferScanHeader, int?>>) (h => h.TransferToSiteID), value);
    }
  }

  public int? TransferToLocationID
  {
    get => this.TransferHeader.TransferToLocationID;
    set
    {
      ValueSetter<ScanHeader>.Ext<TransferScanHeader> transferSetter = this.TransferSetter;
      (^ref transferSetter).Set<int?>((Expression<Func<TransferScanHeader, int?>>) (h => h.TransferToLocationID), value);
    }
  }

  public string AmbiguousLocationCD
  {
    get => this.TransferHeader.AmbiguousLocationCD;
    set
    {
      ValueSetter<ScanHeader>.Ext<TransferScanHeader> transferSetter = this.TransferSetter;
      (^ref transferSetter).Set<string>((Expression<Func<TransferScanHeader, string>>) (h => h.AmbiguousLocationCD), value);
    }
  }

  public bool? AmbiguousSource
  {
    get => this.TransferHeader.AmbiguousSource;
    set
    {
      ValueSetter<ScanHeader>.Ext<TransferScanHeader> transferSetter = this.TransferSetter;
      (^ref transferSetter).Set<bool?>((Expression<Func<TransferScanHeader, bool?>>) (h => h.AmbiguousSource), value);
    }
  }

  public bool? SkipAvailabilityWarning
  {
    get => this.TransferHeader.SkipAvailabilityWarning;
    set
    {
      ValueSetter<ScanHeader>.Ext<TransferScanHeader> transferSetter = this.TransferSetter;
      (^ref transferSetter).Set<bool?>((Expression<Func<TransferScanHeader, bool?>>) (h => h.SkipAvailabilityWarning), value);
    }
  }

  [BorrowedNote(typeof (INRegister), typeof (INTransferEntry))]
  protected virtual void _(Events.CacheAttached<ScanHeader.noteID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (INSetup.transferReasonCode))]
  protected virtual void _(Events.CacheAttached<INTran.reasonCode> e)
  {
  }

  protected override void _(Events.RowSelected<INTran> e)
  {
    base._(e);
    PXCacheEx.AdjustUI(((PXSelectBase) this.Details).Cache, (object) null).For<INTran.toLocationID>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = ((PXGraph) this.Graph).IsMobile && (this.Document == null || !this.Document.Released.GetValueOrDefault())));
  }

  protected virtual IEnumerable<ScanMode<INScanTransfer>> CreateScanModes()
  {
    yield return (ScanMode<INScanTransfer>) new INScanTransfer.TransferMode();
  }

  public class Host : INTransferEntry, ICaptionable
  {
    public INScanTransfer WMS => ((PXGraph) this).FindImplementation<INScanTransfer>();

    string ICaptionable.Caption() => this.WMS.GetCaption();
  }

  public new class QtySupport : 
    WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.QtySupport
  {
  }

  public new class GS1Support : 
    WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.GS1Support
  {
  }

  public new class UserSetup : 
    INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.UserSetup
  {
  }

  public sealed class TransferMode : 
    BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>.ScanMode
  {
    public const string Value = "INTR";

    public virtual string Code => "INTR";

    public virtual string Description => "Scan and Transfer";

    protected virtual IEnumerable<ScanState<INScanTransfer>> CreateStates()
    {
      // ISSUE: reference to a compiler-generated method
      foreach (ScanState<INScanTransfer> state in this.\u003C\u003En__0())
        yield return state;
      yield return (ScanState<INScanTransfer>) ((MethodInterceptor<EntityState<INScanTransfer, INSite>, INScanTransfer>.OfAction) new INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.WarehouseState().Intercept.ClearState).ByAppend((Action<INScanTransfer>) (basis => basis.TransferToSiteID = new int?()), new RelativeInject?());
      yield return (ScanState<INScanTransfer>) new INScanTransfer.TransferMode.SourceLocationState();
      yield return (ScanState<INScanTransfer>) new INScanTransfer.TransferMode.TargetLocationState();
      yield return (ScanState<INScanTransfer>) new INScanTransfer.TransferMode.SpecifyWarehouseState();
      yield return (ScanState<INScanTransfer>) ((MethodInterceptor<EntityState<INScanTransfer, PXResult<INItemXRef, InventoryItem>>, INScanTransfer>.OfFunc<string, AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>>) ((EntityState<INScanTransfer, PXResult<INItemXRef, InventoryItem>>) new WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.InventoryItemState()).Intercept.HandleAbsence).ByOverride((Func<AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>, string, Func<string, AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>>, AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>>) ((basis, barcode, base_HandleAbsence) => basis.TryProcessBy<INScanTransfer.TransferMode.SourceLocationState>(barcode, (StateSubstitutionRule) 26) || basis.TryProcessBy<INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.ReasonCodeState>(barcode, (StateSubstitutionRule) 26) ? AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>.op_Implicit(AbsenceHandling.Done) : base_HandleAbsence(barcode)), new RelativeInject?());
      yield return (ScanState<INScanTransfer>) ((MethodInterceptor<EntityState<INScanTransfer, string>, INScanTransfer>.OfPredicate) ((EntityState<INScanTransfer, string>) new WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.LotSerialState()).Intercept.IsStateActive).ByConjoin((Func<INScanTransfer, bool>) (basis =>
      {
        if (!basis.UseDefaultLotSerialNbr)
          return basis.LotSerialTrack.IsEnterable;
        return !basis.Remove.GetValueOrDefault() && basis.SelectedLotSerialClass?.LotSerIssueMethod == "U";
      }), false, new RelativeInject?());
      yield return (ScanState<INScanTransfer>) ((MethodInterceptor<EntityState<INScanTransfer, PX.Objects.CS.ReasonCode>, INScanTransfer>.OfPredicate) ((EntityState<INScanTransfer, PX.Objects.CS.ReasonCode>) new INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.ReasonCodeState()).Intercept.IsStateSkippable).ByDisjoin((Func<INScanTransfer, bool>) (basis => !basis.PromptLocationForEveryLine && basis.ReasonCodeID != null), false, new RelativeInject?());
      yield return (ScanState<INScanTransfer>) new INScanTransfer.TransferMode.ConfirmState();
    }

    protected virtual IEnumerable<ScanTransition<INScanTransfer>> CreateTransitions()
    {
      return ((ScanMode<INScanTransfer>) this).Basis.PromptLocationForEveryLine ? ((ScanMode<INScanTransfer>) this).StateFlow((Func<ScanStateFlow<INScanTransfer>.IFrom, IEnumerable<ScanTransition<INScanTransfer>>>) (flow => (IEnumerable<ScanTransition<INScanTransfer>>) ((ScanStateFlow<INScanTransfer>.INextTo) ((ScanStateFlow<INScanTransfer>.INextTo) ((ScanStateFlow<INScanTransfer>.INextTo) ((ScanStateFlow<INScanTransfer>.INextTo) flow.From<INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.WarehouseState>().NextTo<INScanTransfer.TransferMode.SourceLocationState>((Action<INScanTransfer>) null)).NextTo<WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.InventoryItemState>((Action<INScanTransfer>) null)).NextTo<WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.LotSerialState>((Action<INScanTransfer>) null)).NextTo<INScanTransfer.TransferMode.TargetLocationState>((Action<INScanTransfer>) null)).NextTo<INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.ReasonCodeState>((Action<INScanTransfer>) null))) : ((ScanMode<INScanTransfer>) this).StateFlow((Func<ScanStateFlow<INScanTransfer>.IFrom, IEnumerable<ScanTransition<INScanTransfer>>>) (flow => (IEnumerable<ScanTransition<INScanTransfer>>) ((ScanStateFlow<INScanTransfer>.INextTo) ((ScanStateFlow<INScanTransfer>.INextTo) ((ScanStateFlow<INScanTransfer>.INextTo) ((ScanStateFlow<INScanTransfer>.INextTo) flow.From<INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.WarehouseState>().NextTo<INScanTransfer.TransferMode.SourceLocationState>((Action<INScanTransfer>) null)).NextTo<INScanTransfer.TransferMode.TargetLocationState>((Action<INScanTransfer>) null)).NextTo<INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.ReasonCodeState>((Action<INScanTransfer>) null)).NextTo<WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.InventoryItemState>((Action<INScanTransfer>) null)).NextTo<WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.LotSerialState>((Action<INScanTransfer>) null)));
    }

    protected virtual IEnumerable<ScanCommand<INScanTransfer>> CreateCommands()
    {
      return (IEnumerable<ScanCommand<INScanTransfer>>) new ScanCommand<INScanTransfer>[3]
      {
        (ScanCommand<INScanTransfer>) new WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.RemoveCommand(),
        (ScanCommand<INScanTransfer>) new BarcodeQtySupport<INScanTransfer, INScanTransfer.Host>.SetQtyCommand(),
        (ScanCommand<INScanTransfer>) new INScanTransfer.TransferMode.ReleaseCommand()
      };
    }

    protected virtual IEnumerable<ScanQuestion<INScanTransfer>> CreateQuestions()
    {
      yield return (ScanQuestion<INScanTransfer>) new INScanTransfer.TransferMode.ConfirmState.SkipAvailabilityQuestion();
    }

    protected virtual IEnumerable<ScanRedirect<INScanTransfer>> CreateRedirects()
    {
      return AllWMSRedirects.CreateFor<INScanTransfer>();
    }

    protected virtual void ResetMode(bool fullReset = false)
    {
      ((ScanMode<INScanTransfer>) this).ResetMode(fullReset);
      ((ScanMode<INScanTransfer>) this).Clear<INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.WarehouseState>(fullReset && ((ScanMode<INScanTransfer>) this).Basis.Document == null);
      ((ScanMode<INScanTransfer>) this).Clear<INScanTransfer.TransferMode.SourceLocationState>(fullReset || ((ScanMode<INScanTransfer>) this).Basis.PromptLocationForEveryLine);
      ((ScanMode<INScanTransfer>) this).Clear<INScanTransfer.TransferMode.TargetLocationState>(fullReset || ((ScanMode<INScanTransfer>) this).Basis.PromptLocationForEveryLine);
      ((ScanMode<INScanTransfer>) this).Clear<INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.ReasonCodeState>(fullReset || ((ScanMode<INScanTransfer>) this).Basis.PromptLocationForEveryLine);
      ((ScanMode<INScanTransfer>) this).Clear<WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.InventoryItemState>(fullReset);
      ((ScanMode<INScanTransfer>) this).Clear<WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.LotSerialState>(true);
    }

    public class value : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INScanTransfer.TransferMode.value>
    {
      public value()
        : base("INTR")
      {
      }
    }

    public abstract class AmbiguousLocationState : 
      BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>.EntityState<INLocation[]>
    {
      private readonly bool _isSource;
      private bool ambiguousLocation;
      private bool filteredByBuilding;

      protected AmbiguousLocationState(bool isSource) => this._isSource = isSource;

      protected virtual string StatePrompt
      {
        get => !this._isSource ? "Scan the destination location." : "Scan the origin location.";
      }

      protected virtual bool IsStateActive()
      {
        return PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>();
      }

      protected virtual bool IsStateSkippable()
      {
        if (((ScanComponent<INScanTransfer>) this).Basis.PromptLocationForEveryLine)
          return false;
        return !this._isSource ? ((ScanComponent<INScanTransfer>) this).Basis.TransferToLocationID.HasValue : ((ScanComponent<INScanTransfer>) this).Basis.LocationID.HasValue;
      }

      private int? SiteID
      {
        get
        {
          return ((ScanComponent<INScanTransfer>) this).Basis.Document != null ? (!this._isSource ? ((ScanComponent<INScanTransfer>) this).Basis.Document.ToSiteID : ((ScanComponent<INScanTransfer>) this).Basis.Document.SiteID) : (!this._isSource ? ((ScanComponent<INScanTransfer>) this).Basis.TransferToSiteID : ((ScanComponent<INScanTransfer>) this).Basis.SiteID);
        }
      }

      protected virtual INLocation[] GetByBarcode(string barcode)
      {
        if (((ScanComponent<INScanTransfer>) this).Basis.Document == null)
        {
          (INLocation, INSite)[] array = this.ReadLocationsByBarcode(this.SiteID, barcode).ToArray<(INLocation, INSite)>();
          if (!this._isSource)
          {
            int length = array.Length;
            int? siteID = ((ScanComponent<INScanTransfer>) this).Basis.SiteID;
            int? buildingID = ((ScanComponent<INScanTransfer>) this).Basis.SelectedSite.BuildingID;
            array = ((IEnumerable<(INLocation, INSite)>) array).Where<(INLocation, INSite)>((Func<(INLocation, INSite), bool>) (r =>
            {
              int? siteId = r.site.SiteID;
              int? nullable1 = siteID;
              if (siteId.GetValueOrDefault() == nullable1.GetValueOrDefault() & siteId.HasValue == nullable1.HasValue)
                return true;
              if (!r.site.BuildingID.HasValue)
                return false;
              int? buildingId = r.site.BuildingID;
              int? nullable2 = buildingID;
              return buildingId.GetValueOrDefault() == nullable2.GetValueOrDefault() & buildingId.HasValue == nullable2.HasValue;
            })).ToArray<(INLocation, INSite)>();
            this.filteredByBuilding = array.Length < length;
          }
          else if (!UserPreferenceExt.GetDefaultSite((PXGraph) ((ScanComponent<INScanTransfer>) this).Basis.Graph).HasValue)
            array = ((IEnumerable<(INLocation, INSite)>) array).Where<(INLocation, INSite)>((Func<(INLocation, INSite), bool>) (r =>
            {
              int? siteId1 = r.site.SiteID;
              int? siteId2 = this.SiteID;
              return siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue;
            })).ToArray<(INLocation, INSite)>();
          if (array.Length == 0)
            return (INLocation[]) null;
          this.ambiguousLocation = ((IEnumerable<(INLocation, INSite)>) array).Where<(INLocation, INSite)>((Func<(INLocation, INSite), bool>) (t => t.location.Active.GetValueOrDefault())).Count<(INLocation, INSite)>() > 1;
          return ((IEnumerable<(INLocation, INSite)>) array).Select<(INLocation, INSite), INLocation>((Func<(INLocation, INSite), INLocation>) (t => t.location)).ToArray<INLocation>();
        }
        int? siteId3 = ((ScanComponent<INScanTransfer>) this).Basis.SiteID;
        int? siteId4 = ((ScanComponent<INScanTransfer>) this).Basis.Document.SiteID;
        if (!(siteId3.GetValueOrDefault() == siteId4.GetValueOrDefault() & siteId3.HasValue == siteId4.HasValue))
          ((ScanComponent<INScanTransfer>) this).Basis.SiteID = ((ScanComponent<INScanTransfer>) this).Basis.Document.SiteID;
        int? transferToSiteId = ((ScanComponent<INScanTransfer>) this).Basis.TransferToSiteID;
        int? toSiteId = ((ScanComponent<INScanTransfer>) this).Basis.Document.ToSiteID;
        if (!(transferToSiteId.GetValueOrDefault() == toSiteId.GetValueOrDefault() & transferToSiteId.HasValue == toSiteId.HasValue))
          ((ScanComponent<INScanTransfer>) this).Basis.TransferToSiteID = ((ScanComponent<INScanTransfer>) this).Basis.Document.ToSiteID;
        return this.ReadLocationByBarcode(this._isSource ? ((ScanComponent<INScanTransfer>) this).Basis.Document.SiteID : ((ScanComponent<INScanTransfer>) this).Basis.Document.ToSiteID, barcode).With<INLocation, INLocation[]>((Func<INLocation, INLocation[]>) (loc => new INLocation[1]
        {
          loc
        }));
      }

      protected virtual void ReportMissing(string barcode)
      {
        if (this.filteredByBuilding)
        {
          ((ScanComponent<INScanTransfer>) this).Basis.Reporter.Error("You can perform one-step transfers only between warehouses located in the same building.", Array.Empty<object>());
        }
        else
        {
          INSite inSite = INSite.PK.Find(BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>.op_Implicit((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) ((ScanComponent<INScanTransfer>) this).Basis), this.SiteID);
          ((ScanComponent<INScanTransfer>) this).Basis.Reporter.Error("{0} location not found in {1} warehouse.", new object[2]
          {
            (object) barcode,
            (object) inSite.SiteCD
          });
        }
      }

      protected virtual void ReportSuccess(INLocation[] locations)
      {
        if (this.ambiguousLocation)
          return;
        ((ScanComponent<INScanTransfer>) this).Basis.Reporter.Info(this._isSource ? "{0} selected as origin location." : "{0} selected as destination location.", new object[1]
        {
          (object) locations[0].LocationCD
        });
      }

      protected virtual Validation Validate(INLocation[] locations)
      {
        if (!this.ambiguousLocation)
        {
          INLocation location = locations[0];
          if (!location.Active.GetValueOrDefault())
            return Validation.Fail("Location '{0}' is inactive", new object[1]
            {
              (object) location.LocationCD
            });
          if (((ScanComponent<INScanTransfer>) this).Basis.Document != null)
          {
            int? nullable;
            int? siteId;
            if (this._isSource)
            {
              nullable = location.SiteID;
              siteId = ((ScanComponent<INScanTransfer>) this).Basis.Document.SiteID;
              if (!(nullable.GetValueOrDefault() == siteId.GetValueOrDefault() & nullable.HasValue == siteId.HasValue))
                return Validation.Fail("The {0} location cannot be used because it does not belong to the origin warehouse.", new object[1]
                {
                  (object) location.LocationCD
                });
            }
            if (!this._isSource)
            {
              siteId = location.SiteID;
              nullable = ((ScanComponent<INScanTransfer>) this).Basis.Document.ToSiteID;
              if (!(siteId.GetValueOrDefault() == nullable.GetValueOrDefault() & siteId.HasValue == nullable.HasValue))
                return Validation.Fail("The {0} location cannot be used because it does not belong to the destination warehouse.", new object[1]
                {
                  (object) location.LocationCD
                });
            }
          }
        }
        return Validation.Ok;
      }

      protected virtual void Apply(INLocation[] locations)
      {
        if (this.ambiguousLocation)
        {
          ((ScanComponent<INScanTransfer>) this).Basis.AmbiguousLocationCD = ((ScanComponent<INScanTransfer>) this).Basis.Header.Barcode;
          ((ScanComponent<INScanTransfer>) this).Basis.AmbiguousSource = new bool?(this._isSource);
        }
        else
        {
          INLocation location = locations[0];
          if (this._isSource)
          {
            if (((ScanComponent<INScanTransfer>) this).Basis.Document == null)
            {
              ((ScanComponent<INScanTransfer>) this).Basis.SiteID = location.SiteID;
              ((ScanComponent<INScanTransfer>) this).Basis.TransferToSiteID = location.SiteID;
            }
            ((ScanComponent<INScanTransfer>) this).Basis.LocationID = location.LocationID;
            ((ScanComponent<INScanTransfer>) this).Basis.TransferToLocationID = new int?();
          }
          else
          {
            if (((ScanComponent<INScanTransfer>) this).Basis.Document == null)
              ((ScanComponent<INScanTransfer>) this).Basis.TransferToSiteID = location.SiteID;
            ((ScanComponent<INScanTransfer>) this).Basis.TransferToLocationID = location.LocationID;
          }
        }
      }

      protected virtual void ClearState()
      {
        if (this.ambiguousLocation)
        {
          ((ScanComponent<INScanTransfer>) this).Basis.AmbiguousLocationCD = (string) null;
          ((ScanComponent<INScanTransfer>) this).Basis.AmbiguousSource = new bool?();
        }
        else if (this._isSource)
          ((ScanComponent<INScanTransfer>) this).Basis.LocationID = new int?();
        else
          ((ScanComponent<INScanTransfer>) this).Basis.TransferToLocationID = new int?();
      }

      protected virtual void SetNextState()
      {
        if (this.ambiguousLocation)
          ((ScanComponent<INScanTransfer>) this).Basis.SetScanState<INScanTransfer.TransferMode.SpecifyWarehouseState>("The {0} location is defined in multiple warehouses.", new object[1]
          {
            (object) ((ScanComponent<INScanTransfer>) this).Basis.Header.Barcode
          });
        else
          ((EntityState<INScanTransfer, INLocation[]>) this).SetNextState();
      }

      protected virtual INLocation ReadLocationByBarcode(int? siteID, string locationCD)
      {
        return PXResultset<INLocation>.op_Implicit(PXSelectBase<INLocation, PXViewOf<INLocation>.BasedOn<SelectFromBase<INLocation, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLocation.siteID, Equal<P.AsInt>>>>>.And<BqlOperand<INLocation.locationCD, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select(BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>.op_Implicit((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) ((ScanComponent<INScanTransfer>) this).Basis), new object[2]
        {
          (object) siteID,
          (object) locationCD
        }));
      }

      protected virtual IEnumerable<(INLocation location, INSite site)> ReadLocationsByBarcode(
        int? siteID,
        string locationCD)
      {
        int? nullable = new int?((int?) INSite.PK.Find(BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>.op_Implicit((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) ((ScanComponent<INScanTransfer>) this).Basis), siteID)?.BuildingID ?? -1);
        return (IEnumerable<(INLocation, INSite)>) ((IEnumerable<PXResult<INLocation>>) PXSelectBase<INLocation, PXViewOf<INLocation>.BasedOn<SelectFromBase<INLocation, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INSite>.On<INLocation.FK.Site>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Match<INSite, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>, And<BqlOperand<INLocation.locationCD, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<INSite.active, IBqlBool>.IsEqual<True>>>.Order<By<Desc<TestIf<BqlOperand<INLocation.siteID, IBqlInt>.IsEqual<P.AsInt>>>, Desc<TestIf<BqlOperand<INSite.buildingID, IBqlInt>.IsEqual<P.AsInt>>>, Desc<INLocation.active>>>>.Config>.Select(BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>.op_Implicit((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) ((ScanComponent<INScanTransfer>) this).Basis), new object[3]
        {
          (object) locationCD,
          (object) siteID,
          (object) nullable
        })).AsEnumerable<PXResult<INLocation>>().Cast<PXResult<INLocation, INSite>>().Select<PXResult<INLocation, INSite>, (INLocation, INSite)>((Func<PXResult<INLocation, INSite>, (INLocation, INSite)>) (l => (PXResult<INLocation, INSite>.op_Implicit(l), PXResult<INLocation, INSite>.op_Implicit(l)))).ToArray<(INLocation, INSite)>();
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string PromptSource = "Scan the origin location.";
        public const string PromptTarget = "Scan the destination location.";
        public const string ReadySource = "{0} selected as origin location.";
        public const string ReadyTarget = "{0} selected as destination location.";
        public const string MismatchSource = "The {0} location cannot be used because it does not belong to the origin warehouse.";
        public const string MismatchTarget = "The {0} location cannot be used because it does not belong to the destination warehouse.";
        public const string Missing = "{0} location not found in {1} warehouse.";
        public const string Ambiguous = "The {0} location is defined in multiple warehouses.";
      }
    }

    public sealed class SourceLocationState : INScanTransfer.TransferMode.AmbiguousLocationState
    {
      public const string Value = "FLOC";

      public SourceLocationState()
        : base(true)
      {
      }

      public virtual string Code => "FLOC";

      public class value : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        INScanTransfer.TransferMode.SourceLocationState.value>
      {
        public value()
          : base("FLOC")
        {
        }
      }

      [PXLocalizable]
      public new abstract class Msg : INScanTransfer.TransferMode.AmbiguousLocationState.Msg
      {
        public const string NotSet = "Origin location not selected.";
      }
    }

    public sealed class TargetLocationState : INScanTransfer.TransferMode.AmbiguousLocationState
    {
      public const string Value = "TLOC";

      public TargetLocationState()
        : base(false)
      {
      }

      public virtual string Code => "TLOC";

      public class value : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        INScanTransfer.TransferMode.TargetLocationState.value>
      {
        public value()
          : base("TLOC")
        {
        }
      }

      [PXLocalizable]
      public new abstract class Msg : INScanTransfer.TransferMode.AmbiguousLocationState.Msg
      {
        public const string NotSet = "Destination location not selected.";
      }
    }

    public sealed class SpecifyWarehouseState : 
      BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>.EntityState<PXResult<INSite, INLocation>>
    {
      public const string Value = "SPWH";

      private bool AmbiguousSource
      {
        get => ((ScanComponent<INScanTransfer>) this).Basis.AmbiguousSource.Value;
      }

      public virtual string Code => "SPWH";

      protected virtual string StatePrompt
      {
        get
        {
          return ((ScanComponent<INScanTransfer>) this).Basis.Localize("Scan the barcode of the warehouse to which the {0} location belongs.", new object[1]
          {
            (object) ((ScanComponent<INScanTransfer>) this).Basis.AmbiguousLocationCD
          });
        }
      }

      protected virtual PXResult<INSite, INLocation> GetByBarcode(string barcode)
      {
        return ((IEnumerable<PXResult<INSite>>) PXSelectBase<INSite, PXViewOf<INSite>.BasedOn<SelectFromBase<INSite, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<INLocation>.On<KeysRelation<Field<INLocation.siteID>.IsRelatedTo<INSite.siteID>.AsSimpleKey.WithTablesOf<INSite, INLocation>, INSite, INLocation>.And<BqlOperand<INLocation.locationCD, IBqlString>.IsEqual<P.AsString>>>>>.Where<BqlChainableConditionLite<Match<INSite, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>.And<BqlOperand<INSite.siteCD, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select(BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>.op_Implicit((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) ((ScanComponent<INScanTransfer>) this).Basis), new object[2]
        {
          (object) ((ScanComponent<INScanTransfer>) this).Basis.AmbiguousLocationCD,
          (object) barcode
        })).AsEnumerable<PXResult<INSite>>().Cast<PXResult<INSite, INLocation>>().FirstOrDefault<PXResult<INSite, INLocation>>();
      }

      protected virtual void ReportMissing(string barcode)
      {
        ((ScanComponent<INScanTransfer>) this).Basis.Reporter.Error("{0} warehouse not found.", new object[1]
        {
          (object) barcode
        });
      }

      protected virtual Validation Validate(PXResult<INSite, INLocation> entity)
      {
        INSite inSite1;
        INLocation inLocation1;
        entity.Deconstruct(ref inSite1, ref inLocation1);
        INSite inSite2 = inSite1;
        INLocation inLocation2 = inLocation1;
        if (inSite2 == null || inLocation2 == null)
          return Validation.Fail("The {0} location does not belong to the {1} warehouse.", new object[2]
          {
            (object) ((ScanComponent<INScanTransfer>) this).Basis.AmbiguousLocationCD,
            (object) ((ScanComponent<INScanTransfer>) this).Basis.Header.Barcode
          });
        bool? active = inLocation2.Active;
        bool flag = false;
        if (active.GetValueOrDefault() == flag & active.HasValue)
          return Validation.Fail("Location '{0}' is inactive", new object[1]
          {
            (object) inLocation2.LocationCD
          });
        if (!this.AmbiguousSource)
        {
          int? buildingId1 = inSite2.BuildingID;
          if (buildingId1.HasValue)
          {
            buildingId1 = inSite2.BuildingID;
            int? buildingId2 = ((ScanComponent<INScanTransfer>) this).Basis.SelectedSite.BuildingID;
            if (buildingId1.GetValueOrDefault() == buildingId2.GetValueOrDefault() & buildingId1.HasValue == buildingId2.HasValue)
              goto label_8;
          }
          return Validation.Fail("You can perform one-step transfers only between warehouses located in the same building.", Array.Empty<object>());
        }
label_8:
        return Validation.Ok;
      }

      protected virtual void Apply(PXResult<INSite, INLocation> entity)
      {
        INSite inSite;
        INLocation inLocation1;
        entity.Deconstruct(ref inSite, ref inLocation1);
        INLocation inLocation2 = inLocation1;
        if (this.AmbiguousSource)
        {
          ((ScanComponent<INScanTransfer>) this).Basis.SiteID = inLocation2.SiteID;
          ((ScanComponent<INScanTransfer>) this).Basis.TransferToSiteID = inLocation2.SiteID;
          ((ScanComponent<INScanTransfer>) this).Basis.LocationID = inLocation2.LocationID;
        }
        else
        {
          ((ScanComponent<INScanTransfer>) this).Basis.TransferToSiteID = inLocation2.SiteID;
          ((ScanComponent<INScanTransfer>) this).Basis.TransferToLocationID = inLocation2.LocationID;
        }
      }

      protected virtual void ReportSuccess(PXResult<INSite, INLocation> entity)
      {
        ((ScanComponent<INScanTransfer>) this).Basis.Reporter.Info(this.AmbiguousSource ? "{0} selected as origin location." : "{0} selected as destination location.", new object[1]
        {
          (object) ((PXResult) entity).GetItem<INLocation>().LocationCD
        });
      }

      protected virtual void SetNextState()
      {
        if (this.AmbiguousSource)
          ((ScanState<INScanTransfer>) ((ScanComponent<INScanTransfer>) this).Basis.FindState<INScanTransfer.TransferMode.SourceLocationState>(false)).MoveToNextState();
        else
          ((ScanState<INScanTransfer>) ((ScanComponent<INScanTransfer>) this).Basis.FindState<INScanTransfer.TransferMode.TargetLocationState>(false)).MoveToNextState();
      }

      protected virtual void OnDismissing()
      {
        ((ScanComponent<INScanTransfer>) this).Basis.AmbiguousLocationCD = (string) null;
        ((ScanComponent<INScanTransfer>) this).Basis.AmbiguousSource = new bool?();
      }

      public class value : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        INScanTransfer.TransferMode.SpecifyWarehouseState.value>
      {
        public value()
          : base("SPWH")
        {
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string Prompt = "Scan the barcode of the warehouse to which the {0} location belongs.";
        public const string NoLocationSiteRelation = "The {0} location does not belong to the {1} warehouse.";
        public const string InterWarehouseTransferNotPossible = "You can perform one-step transfers only between warehouses located in the same building.";
      }
    }

    public sealed class ConfirmState : 
      BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>.ConfirmationState
    {
      public virtual string Prompt
      {
        get
        {
          return ((ScanComponent<INScanTransfer>) this).Basis.Localize("Confirm transferring {0} x {1} {2}.", new object[3]
          {
            (object) ((ScanComponent<INScanTransfer>) this).Basis.SightOf<WMSScanHeader.inventoryID>(),
            (object) ((ScanComponent<INScanTransfer>) this).Basis.Qty,
            (object) ((ScanComponent<INScanTransfer>) this).Basis.UOM
          });
        }
      }

      protected virtual FlowStatus PerformConfirmation()
      {
        return this.Get<INScanTransfer.TransferMode.ConfirmState.Logic>().Confirm();
      }

      public class Logic : 
        BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>.ScanExtension
      {
        public virtual FlowStatus Confirm()
        {
          FlowStatus error;
          if (!this.CanConfirm(out error))
            return error;
          return !this.Basis.Remove.GetValueOrDefault() ? this.ConfirmAdd() : this.ConfirmRemove();
        }

        protected virtual bool CanConfirm(out FlowStatus error)
        {
          INRegister document = this.Basis.Document;
          if ((document != null ? (document.Released.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          {
            error = FlowStatus.Fail("Document Status is invalid for processing.", Array.Empty<object>());
            return false;
          }
          if (this.Basis.CurrentMode.HasActive<INScanTransfer.TransferMode.SourceLocationState>() && !this.Basis.LocationID.HasValue)
          {
            error = FlowStatus.Fail("Origin location not selected.", Array.Empty<object>());
            return false;
          }
          if (this.Basis.CurrentMode.HasActive<INScanTransfer.TransferMode.TargetLocationState>() && !this.Basis.TransferToLocationID.HasValue)
          {
            error = FlowStatus.Fail("Destination location not selected.", Array.Empty<object>());
            return false;
          }
          if (!this.Basis.InventoryID.HasValue)
          {
            error = FlowStatus.Fail("Item not selected.", Array.Empty<object>());
            return false;
          }
          if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.LotSerialState>() && this.Basis.LotSerialNbr == null)
          {
            error = FlowStatus.Fail("Lot or serial number not selected.", Array.Empty<object>());
            return false;
          }
          if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.LotSerialState>() && this.Basis.LotSerialTrack.IsTrackedSerial)
          {
            Decimal? baseQty = this.Basis.BaseQty;
            Decimal num = (Decimal) 1;
            if (!(baseQty.GetValueOrDefault() == num & baseQty.HasValue))
            {
              error = FlowStatus.Fail("Serialized items can be processed only with the base UOM and the 1.00 quantity.", Array.Empty<object>());
              return false;
            }
          }
          error = FlowStatus.Ok;
          return true;
        }

        protected virtual FlowStatus ConfirmAdd()
        {
          bool newDocument = this.Basis.Document == null;
          this.EnsureDocument();
          INTran existTransaction = this.FindTransferRow();
          Action action;
          if (existTransaction != null)
          {
            Decimal? qty1 = existTransaction.Qty;
            Decimal? qty2 = this.Basis.Qty;
            Decimal? nullable1 = qty1.HasValue & qty2.HasValue ? new Decimal?(qty1.GetValueOrDefault() + qty2.GetValueOrDefault()) : new Decimal?();
            if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.LotSerialState>() && this.Basis.LotSerialTrack.IsTrackedSerial)
            {
              Decimal? nullable2 = nullable1;
              Decimal num = (Decimal) 1;
              if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
                return FlowStatus.Fail("Serialized items can be processed only with the base UOM and the 1.00 quantity.", Array.Empty<object>());
            }
            INTran backup = PXCache<INTran>.CreateCopy(existTransaction);
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.qty>((object) existTransaction, (object) nullable1);
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.lotSerialNbr>((object) existTransaction, (object) null);
            using (string.IsNullOrEmpty(this.Basis.LotSerialNbr) ? (IDisposable) null : this.Graph.LineSplittingExt.SkipUnassignedExceptionScope())
              existTransaction = this.Basis.Details.Update(existTransaction);
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.lotSerialNbr>((object) existTransaction, (object) this.Basis.LotSerialNbr);
            existTransaction = this.Basis.Details.Update(existTransaction);
            action = (Action) (() =>
            {
              this.Basis.Details.Delete(existTransaction);
              this.Basis.Details.Insert(backup);
            });
          }
          else
          {
            existTransaction = this.Basis.Details.Insert();
            ValueSetter<INTran> withEventFiring = PXCacheEx.GetSetterForCurrent<INTran>(this.Basis.Details).WithEventFiring;
            // ISSUE: explicit reference operation
            (^ref withEventFiring).Set<int?>((Expression<Func<INTran, int?>>) (tr => tr.InventoryID), this.Basis.InventoryID);
            // ISSUE: explicit reference operation
            (^ref withEventFiring).Set<int?>((Expression<Func<INTran, int?>>) (tr => tr.SiteID), this.Basis.Document.SiteID);
            // ISSUE: explicit reference operation
            (^ref withEventFiring).Set<int?>((Expression<Func<INTran, int?>>) (tr => tr.ToSiteID), this.Basis.Document.ToSiteID);
            existTransaction = this.Basis.Details.Update(existTransaction);
            // ISSUE: explicit reference operation
            (^ref withEventFiring).Set<int?>((Expression<Func<INTran, int?>>) (tr => tr.LocationID), this.Basis.LocationID);
            // ISSUE: explicit reference operation
            (^ref withEventFiring).Set<int?>((Expression<Func<INTran, int?>>) (tr => tr.ToLocationID), this.Basis.TransferToLocationID);
            // ISSUE: explicit reference operation
            (^ref withEventFiring).Set<string>((Expression<Func<INTran, string>>) (tr => tr.UOM), this.Basis.UOM);
            if (this.Basis.CurrentMode.HasActive<INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.ReasonCodeState>())
            {
              // ISSUE: explicit reference operation
              (^ref withEventFiring).Set<string>((Expression<Func<INTran, string>>) (tr => tr.ReasonCode), this.Basis.ReasonCodeID);
            }
            existTransaction = this.Basis.Details.Update(existTransaction);
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.qty>((object) existTransaction, (object) this.Basis.Qty);
            using (string.IsNullOrEmpty(this.Basis.LotSerialNbr) ? (IDisposable) null : ((PXGraphExtension<INScanTransfer.Host>) this.Basis).Base.LineSplittingExt.SkipUnassignedExceptionScope())
              existTransaction = this.Basis.Details.Update(existTransaction);
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.lotSerialNbr>((object) existTransaction, (object) this.Basis.LotSerialNbr);
            existTransaction = this.Basis.Details.Update(existTransaction);
            action = (Action) (() =>
            {
              if (newDocument)
                this.Basis.DocumentView.DeleteCurrent();
              else
                this.Basis.Details.Delete(existTransaction);
            });
          }
          FlowStatus error;
          if (this.HasErrors(existTransaction, out error))
          {
            action();
            return error;
          }
          this.Basis.ReportInfo("{0} x {1} {2} added to transfer.", new object[3]
          {
            (object) this.Basis.SightOf<WMSScanHeader.inventoryID>(),
            (object) this.Basis.Qty,
            (object) this.Basis.UOM
          });
          if (((PXSelectBase) this.Basis.DocumentView).Cache.GetStatus((object) this.Basis.DocumentView.Current) != 2)
            return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
          FlowStatus withDispatchNext = ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
          return ((FlowStatus) ref withDispatchNext).WithSaveSkip;
        }

        protected virtual INRegister EnsureDocument()
        {
          if (this.Basis.Document == null)
          {
            this.Basis.DocumentView.Insert();
            this.Basis.DocumentView.Current.Hold = new bool?(false);
            this.Basis.DocumentView.Current.Status = "B";
            this.Basis.DocumentView.Current.NoteID = this.Basis.NoteID;
            this.Basis.DocumentView.SetValueExt<INRegister.siteID>(this.Basis.Document, (object) this.Basis.SiteID);
            this.Basis.DocumentView.SetValueExt<INRegister.toSiteID>(this.Basis.Document, (object) this.Basis.TransferToSiteID);
          }
          return this.Basis.DocumentView.Update(this.Basis.Document);
        }

        protected virtual bool HasErrors(INTran tran, out FlowStatus error)
        {
          if (this.Basis.HasUIErrors<INTran>(tran, ref error) || this.HasLotSerialError(tran, out error) || this.HasLocationError(tran, out error) || this.HasAvailabilityError(tran, out error))
            return true;
          error = FlowStatus.Ok;
          return false;
        }

        protected virtual bool HasLotSerialError(INTran tran, out FlowStatus error)
        {
          if (this.Basis.HasActive<WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.LotSerialState>() && !string.IsNullOrEmpty(this.Basis.LotSerialNbr) && ((IEnumerable<INTranSplit>) ((PXSelectBase<INTranSplit>) this.Basis.Graph.splits).SelectMain(Array.Empty<object>())).Any<INTranSplit>((Func<INTranSplit, bool>) (s => !string.Equals(s.LotSerialNbr, this.Basis.LotSerialNbr, StringComparison.OrdinalIgnoreCase))))
          {
            error = FlowStatus.Fail("The quantity of the {1} item in the transfer exceeds the item's quantity in the {0} lot.", new object[2]
            {
              (object) this.Basis.LotSerialNbr,
              (object) this.Basis.SightOf<WMSScanHeader.inventoryID>()
            });
            return true;
          }
          error = FlowStatus.Ok;
          return false;
        }

        protected virtual bool HasLocationError(INTran tran, out FlowStatus error)
        {
          if (this.Basis.HasActive<INScanTransfer.TransferMode.SourceLocationState>() && ((IEnumerable<INTranSplit>) ((PXSelectBase<INTranSplit>) this.Basis.Graph.splits).SelectMain(Array.Empty<object>())).Any<INTranSplit>((Func<INTranSplit, bool>) (s =>
          {
            int? locationId1 = s.LocationID;
            int? locationId2 = this.Basis.LocationID;
            return !(locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue);
          })))
          {
            error = FlowStatus.Fail("The quantity of the {1} item in the transfer exceeds the item's quantity in the {0} location.", new object[2]
            {
              (object) this.Basis.SightOf<WMSScanHeader.locationID>(),
              (object) this.Basis.SightOf<WMSScanHeader.inventoryID>()
            });
            return true;
          }
          error = FlowStatus.Ok;
          return false;
        }

        protected virtual bool HasAvailabilityError(INTran tran, out FlowStatus error)
        {
          PXExceptionInfo pxExceptionInfo = this.Basis.Graph.ItemAvailabilityExt.GetCheckErrors((ILSMaster) tran, tran.CostCenterID).FirstOrDefault<PXExceptionInfo>();
          bool isWarning = pxExceptionInfo != null && PXErrorLevelUtils.IsWarning(pxExceptionInfo.ErrorLevel.Value);
          Func<string, object[], FlowStatus> func = (Func<string, object[], FlowStatus>) ((msg, args) => !isWarning ? FlowStatus.Fail(msg, args) : FlowStatus.Warn<INScanTransfer.TransferMode.ConfirmState.SkipAvailabilityQuestion>(msg, args));
          if (pxExceptionInfo != null && (!isWarning || !this.Basis.SkipAvailabilityWarning.GetValueOrDefault()))
          {
            PXCache cache = ((PXSelectBase) this.Basis.Graph.transactions).Cache;
            error = func(pxExceptionInfo.MessageFormat, new object[5]
            {
              cache.GetStateExt<INTran.inventoryID>((object) tran),
              cache.GetStateExt<INTran.subItemID>((object) tran),
              cache.GetStateExt<INTran.siteID>((object) tran),
              cache.GetStateExt<INTran.locationID>((object) tran),
              cache.GetValue<INTran.lotSerialNbr>((object) tran)
            });
            return true;
          }
          error = FlowStatus.Ok;
          return false;
        }

        protected virtual FlowStatus ConfirmRemove()
        {
          INTran transferRow = this.FindTransferRow();
          if (transferRow == null)
            return FlowStatus.Fail("{0} item not found in transfer.", new object[1]
            {
              (object) this.Basis.SightOf<WMSScanHeader.inventoryID>()
            });
          Decimal? qty1 = transferRow.Qty;
          Decimal? qty2 = this.Basis.Qty;
          if (qty1.GetValueOrDefault() == qty2.GetValueOrDefault() & qty1.HasValue == qty2.HasValue)
          {
            this.Basis.Details.Delete(transferRow);
          }
          else
          {
            Decimal? qty3 = transferRow.Qty;
            Decimal? qty4 = this.Basis.Qty;
            Decimal? nullable = qty3.HasValue & qty4.HasValue ? new Decimal?(qty3.GetValueOrDefault() - qty4.GetValueOrDefault()) : new Decimal?();
            string str;
            if (!this.Basis.IsValid<INTran.qty, INTran>(transferRow, (object) nullable, ref str))
              return FlowStatus.Fail(str, Array.Empty<object>());
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.qty>((object) transferRow, (object) nullable);
            this.Basis.Details.Update(transferRow);
          }
          this.Basis.ReportInfo("{0} x {1} {2} removed from transfer.", new object[3]
          {
            (object) this.Basis.SightOf<WMSScanHeader.inventoryID>(),
            (object) this.Basis.Qty,
            (object) this.Basis.UOM
          });
          if (((PXSelectBase) this.Basis.DocumentView).Cache.GetStatus((object) this.Basis.DocumentView.Current) != 2)
            return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
          FlowStatus withDispatchNext = ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
          return ((FlowStatus) ref withDispatchNext).WithSaveSkip;
        }

        protected virtual INTran FindTransferRow()
        {
          IEnumerable<INTran> source = ((IEnumerable<INTran>) this.Basis.Details.SelectMain(Array.Empty<object>())).Where<INTran>((Func<INTran, bool>) (t =>
          {
            int? inventoryId1 = t.InventoryID;
            int? inventoryId2 = this.Basis.InventoryID;
            if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
            {
              int? nullable1 = t.LocationID;
              int? nullable2 = this.Basis.LocationID;
              int? nullable3 = nullable2 ?? t.LocationID;
              if (nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue)
              {
                nullable3 = t.ToLocationID;
                nullable2 = this.Basis.TransferToLocationID;
                nullable1 = nullable2 ?? t.ToLocationID;
                if (nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue && t.ReasonCode == (this.Basis.ReasonCodeID ?? t.ReasonCode))
                  return t.UOM == this.Basis.UOM;
              }
            }
            return false;
          }));
          INTran transferRow;
          if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.LotSerialState>())
          {
            transferRow = (INTran) null;
            foreach (INTran inTran in source)
            {
              this.Basis.Details.Current = inTran;
              if (((IEnumerable<INTranSplit>) ((PXSelectBase<INTranSplit>) this.Basis.Graph.splits).SelectMain(Array.Empty<object>())).Any<INTranSplit>((Func<INTranSplit, bool>) (t => string.Equals(t.LotSerialNbr ?? "", this.Basis.LotSerialNbr ?? "", StringComparison.OrdinalIgnoreCase))))
              {
                transferRow = inTran;
                break;
              }
            }
          }
          else
          {
            LSConfig lotSerialTrack = this.Basis.LotSerialTrack;
            if (lotSerialTrack.IsTracked && this.Basis.UseDefaultLotSerialNbr)
            {
              if (this.Basis.Remove.GetValueOrDefault())
              {
                transferRow = source.LastOrDefault<INTran>();
              }
              else
              {
                lotSerialTrack = this.Basis.LotSerialTrack;
                transferRow = !lotSerialTrack.IsEnterable ? source.FirstOrDefault<INTran>() : (INTran) null;
              }
            }
            else
              transferRow = source.FirstOrDefault<INTran>();
          }
          return transferRow;
        }
      }

      public sealed class SkipAvailabilityQuestion : 
        BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>.ScanQuestion
      {
        public INScanTransfer.TransferMode.ConfirmState.Logic Mode
        {
          get => this.Get<INScanTransfer.TransferMode.ConfirmState.Logic>();
        }

        public virtual string Code => "SKIPAVAILABILITY";

        protected virtual void Confirm()
        {
          ((ScanComponent<INScanTransfer>) this).Basis.SkipAvailabilityWarning = new bool?(true);
          this.Mode.Confirm();
        }

        protected virtual void Reject()
        {
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string Prompt = "Confirm transferring {0} x {1} {2}.";
        public const string LineMissing = "{0} item not found in transfer.";
        public const string InventoryAdded = "{0} x {1} {2} added to transfer.";
        public const string InventoryRemoved = "{0} x {1} {2} removed from transfer.";
        public const string QtyExceedsOnLot = "The quantity of the {1} item in the transfer exceeds the item's quantity in the {0} lot.";
        public const string QtyExceedsOnLocation = "The quantity of the {1} item in the transfer exceeds the item's quantity in the {0} location.";
      }
    }

    public sealed class ReleaseCommand : 
      INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.ReleaseCommand
    {
      protected override string DocumentReleasing => "Release of {0} transfer in progress.";

      protected override string DocumentIsReleased => "Transfer successfully released.";

      protected override string DocumentReleaseFailed => "Transfer not released.";

      public override void ConfigureOnSuccessAction(
        ScanLongRunAwaiter<INScanTransfer, INRegister>.ISuccessProcessor onSuccess)
      {
        base.ConfigureOnSuccessAction(onSuccess);
        onSuccess.ResetFull().Do((Action<INScanTransfer, INRegister>) ((basis, doc) => basis.ReportComplete(this.DocumentIsReleased, Array.Empty<object>())));
      }

      [PXLocalizable]
      public new abstract class Msg : 
        INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.ReleaseCommand.Msg
      {
        public const string DocumentReleasing = "Release of {0} transfer in progress.";
        public const string DocumentIsReleased = "Transfer successfully released.";
        public const string DocumentReleaseFailed = "Transfer not released.";
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string Description = "Scan and Transfer";
    }
  }

  public new sealed class RedirectFrom<TForeignBasis> : 
    INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.RedirectFrom<TForeignBasis>
    where TForeignBasis : PXGraphExtension, IBarcodeDrivenStateMachine
  {
    public virtual string Code => "INTRANSFER";

    public virtual string DisplayName => "IN Transfer";

    [PXLocalizable]
    public abstract class Msg
    {
      public const string DisplayName = "IN Transfer";
    }
  }
}
