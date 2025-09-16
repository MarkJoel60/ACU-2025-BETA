// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.INScanCount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Attributes;
using PX.Objects.CS;
using PX.Objects.IN.PhysicalInventory;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.IN.WMS;

public class INScanCount : WarehouseManagementSystem<
#nullable disable
INScanCount, INScanCount.Host>
{
  public PXSetupOptional<INScanSetup, Where<BqlOperand<
  #nullable enable
  INScanSetup.branchID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  AccessInfo.branchID, IBqlInt>.FromCurrent>>> Setup;
  public 
  #nullable disable
  PXViewOf<INPIClass>.BasedOn<SelectFromBase<INPIClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  INPIClass.pIClassID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INPIHeader.pIClassID, IBqlString>.FromCurrent>>>.ReadOnly Class;

  public virtual 
  #nullable disable
  INPIHeader Document => this.DocumentView.Current;

  public virtual PXSelectBase<INPIHeader> DocumentView
  {
    get => (PXSelectBase<INPIHeader>) this.Graph.PIHeader;
  }

  public virtual PXSelectBase<INPIDetail> Details => (PXSelectBase<INPIDetail>) this.Graph.PIDetail;

  public virtual bool ExplicitConfirmation
  {
    get
    {
      return ((PXSelectBase<INScanSetup>) this.Setup).Current.ExplicitLineConfirmation.GetValueOrDefault();
    }
  }

  protected override bool UseQtyCorrection
  {
    get
    {
      return !((PXSelectBase<INScanSetup>) this.Setup).Current.UseDefaultQtyInCount.GetValueOrDefault();
    }
  }

  public override bool DocumentIsEditable
  {
    get => base.DocumentIsEditable && this.IsDocumentStatusEditable(this.Document?.Status);
  }

  protected virtual bool IsDocumentStatusEditable(string status) => status == "N";

  protected override void _(Events.RowSelected<ScanHeader> e)
  {
    base._(e);
    ((PXSelectBase) this.Details).AllowInsert = false;
    ((PXSelectBase) this.Details).AllowUpdate = false;
    ((PXSelectBase) this.Details).AllowDelete = false;
  }

  protected virtual void _(
    Events.FieldUpdated<ScanHeader, WMSScanHeader.refNbr> e)
  {
    this.DocumentView.Current = PXResultset<INPIHeader>.op_Implicit(e.NewValue == null ? (PXResultset<INPIHeader>) null : this.DocumentView.Search<INPIHeader.pIID>(e.NewValue, Array.Empty<object>()));
  }

  protected virtual void _(
    Events.FieldVerifying<ScanHeader, WMSScanHeader.inventoryID> e)
  {
    INPIHeader document = this.Document;
    if (((Events.FieldVerifyingBase<Events.FieldVerifying<ScanHeader, WMSScanHeader.inventoryID>, ScanHeader, object>) e).NewValue == null || e.Row == null || (document != null ? (!document.SiteID.HasValue ? 1 : 0) : 1) != 0)
      return;
    PILocksInspector piLocksInspector = new PILocksInspector(document.SiteID.Value);
    int? newValue = (int?) ((Events.FieldVerifyingBase<Events.FieldVerifying<ScanHeader, WMSScanHeader.inventoryID>, ScanHeader, object>) e).NewValue;
    int? nullable;
    if (!this.HasActive<WarehouseManagementSystem<INScanCount, INScanCount.Host>.LocationState>())
      nullable = PXSelectBase<INLocation, PXViewOf<INLocation>.BasedOn<SelectFromBase<INLocation, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INLocation.siteID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(BarcodeDrivenStateMachine<INScanCount, INScanCount.Host>.op_Implicit((BarcodeDrivenStateMachine<INScanCount, INScanCount.Host>) this), new object[1]
      {
        (object) document.SiteID
      }).With<PXResultset<INLocation>, INLocation>((Func<PXResultset<INLocation>, INLocation>) (locs => PXResultset<INLocation>.op_Implicit(locs))).With<INLocation, int?>((Func<INLocation, int?>) (loc => loc.LocationID));
    else
      nullable = ScanHeaderExt.Get<WMSScanHeader>(e.Row).LocationID;
    int? locationID = nullable;
    if (!piLocksInspector.IsInventoryLocationIncludedInPI(newValue, locationID, document.PIID))
      throw new PXSetPropertyException("Combination of selected Inventory Item and Warehouse Location is not allowed for this Physical Count.");
  }

  [BorrowedNote(typeof (INPIHeader), typeof (INPICountEntry))]
  protected virtual void _(Events.CacheAttached<ScanHeader.noteID> e)
  {
  }

  [PXMergeAttributes]
  [PXUnboundDefault(typeof (INPIDetail.pIID))]
  [PXSelector(typeof (Search<INPIHeader.pIID>))]
  protected virtual void _(Events.CacheAttached<WMSScanHeader.refNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUnboundDefault(typeof (INTranType.adjustment))]
  protected virtual void _(Events.CacheAttached<WMSScanHeader.tranType> e)
  {
  }

  [PXMergeAttributes]
  [PXUnboundDefault(typeof (InventoryMultiplicator.increase))]
  protected virtual void _(
    Events.CacheAttached<WMSScanHeader.inventoryMultiplicator> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Enabled", false)]
  protected virtual void _(Events.CacheAttached<INPIDetail.physicalQty> e)
  {
  }

  protected virtual IEnumerable<ScanMode<INScanCount>> CreateScanModes()
  {
    yield return (ScanMode<INScanCount>) new INScanCount.CountMode();
  }

  public class Host : INPICountEntry, ICaptionable
  {
    public INScanCount WMS => ((PXGraph) this).FindImplementation<INScanCount>();

    string ICaptionable.Caption() => this.WMS.GetCaption();
  }

  public new class QtySupport : WarehouseManagementSystem<INScanCount, INScanCount.Host>.QtySupport
  {
  }

  public new class GS1Support : WarehouseManagementSystem<INScanCount, INScanCount.Host>.GS1Support
  {
  }

  public sealed class CountMode : BarcodeDrivenStateMachine<INScanCount, INScanCount.Host>.ScanMode
  {
    public const string Value = "INCO";

    public virtual string Code => "INCO";

    public virtual string Description => "Scan and Count";

    protected virtual IEnumerable<ScanState<INScanCount>> CreateStates()
    {
      yield return (ScanState<INScanCount>) new INScanCount.CountMode.RefNbrState();
      yield return (ScanState<INScanCount>) new INScanCount.CountMode.LocationState();
      yield return (ScanState<INScanCount>) new INScanCount.CountMode.InventoryItemState();
      yield return (ScanState<INScanCount>) ((MethodInterceptor<EntityState<INScanCount, string>, INScanCount>.OfPredicate) ((EntityState<INScanCount, string>) new WarehouseManagementSystem<INScanCount, INScanCount.Host>.LotSerialState()).Intercept.IsStateActive).ByConjoin((Func<INScanCount, bool>) (basis => basis.LotSerialTrack.IsEnterable), false, new RelativeInject?());
      yield return (ScanState<INScanCount>) new INScanCount.CountMode.ConfirmState();
    }

    protected virtual IEnumerable<ScanTransition<INScanCount>> CreateTransitions()
    {
      return ((ScanMode<INScanCount>) this).StateFlow((Func<ScanStateFlow<INScanCount>.IFrom, IEnumerable<ScanTransition<INScanCount>>>) (flow => (IEnumerable<ScanTransition<INScanCount>>) ((ScanStateFlow<INScanCount>.INextTo) ((ScanStateFlow<INScanCount>.INextTo) flow.From<INScanCount.CountMode.RefNbrState>().NextTo<INScanCount.CountMode.LocationState>((Action<INScanCount>) null)).NextTo<INScanCount.CountMode.InventoryItemState>((Action<INScanCount>) null)).NextTo<WarehouseManagementSystem<INScanCount, INScanCount.Host>.LotSerialState>((Action<INScanCount>) null)));
    }

    protected virtual IEnumerable<ScanCommand<INScanCount>> CreateCommands()
    {
      return (IEnumerable<ScanCommand<INScanCount>>) new ScanCommand<INScanCount>[3]
      {
        (ScanCommand<INScanCount>) new WarehouseManagementSystem<INScanCount, INScanCount.Host>.RemoveCommand(),
        (ScanCommand<INScanCount>) new BarcodeQtySupport<INScanCount, INScanCount.Host>.SetQtyCommand(),
        (ScanCommand<INScanCount>) new INScanCount.CountMode.ConfirmCommand()
      };
    }

    protected virtual IEnumerable<ScanQuestion<INScanCount>> CreateQuestions()
    {
      yield return (ScanQuestion<INScanCount>) new INScanCount.CountMode.ConfirmState.AddItemQuestion();
    }

    protected virtual IEnumerable<ScanRedirect<INScanCount>> CreateRedirects()
    {
      return AllWMSRedirects.CreateFor<INScanCount>();
    }

    protected virtual void ResetMode(bool fullReset)
    {
      ((ScanMode<INScanCount>) this).Clear<INScanCount.CountMode.RefNbrState>(fullReset && !((ScanMode<INScanCount>) this).Basis.IsWithinReset);
      ((ScanMode<INScanCount>) this).Clear<INScanCount.CountMode.LocationState>(fullReset);
      ((ScanMode<INScanCount>) this).Clear<INScanCount.CountMode.InventoryItemState>(fullReset);
      ((ScanMode<INScanCount>) this).Clear<WarehouseManagementSystem<INScanCount, INScanCount.Host>.LotSerialState>(true);
    }

    public class value : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INScanCount.CountMode.value>
    {
      public value()
        : base("INCO")
      {
      }
    }

    public sealed class RefNbrState : 
      WarehouseManagementSystem<INScanCount, INScanCount.Host>.RefNbrState<INPIHeader>
    {
      protected virtual string StatePrompt => "Scan the reference number of the PI count.";

      protected override bool IsStateSkippable()
      {
        return ((ScanComponent<INScanCount>) this).Basis.RefNbr != null;
      }

      protected virtual INPIHeader GetByBarcode(string barcode)
      {
        return INPIHeader.PK.Find(BarcodeDrivenStateMachine<INScanCount, INScanCount.Host>.op_Implicit((BarcodeDrivenStateMachine<INScanCount, INScanCount.Host>) ((ScanComponent<INScanCount>) this).Basis), barcode);
      }

      protected virtual void ReportMissing(string barcode)
      {
        ((ScanComponent<INScanCount>) this).Basis.Reporter.Error("{0} PI count not found.", new object[1]
        {
          (object) barcode
        });
      }

      protected virtual Validation Validate(INPIHeader entity)
      {
        if (((ScanComponent<INScanCount>) this).Basis.IsDocumentStatusEditable(entity.Status))
          return Validation.Ok;
        return Validation.Fail("The {0} document cannot be used for counting because it has the {1} status.", new object[2]
        {
          (object) entity.PIID,
          ((PXSelectBase) ((ScanComponent<INScanCount>) this).Basis.DocumentView).Cache.GetStateExt<INPIHeader.status>((object) entity)
        });
      }

      protected virtual void Apply(INPIHeader entity)
      {
        ((ScanComponent<INScanCount>) this).Basis.RefNbr = entity.PIID;
        ((ScanComponent<INScanCount>) this).Basis.SiteID = entity.SiteID;
        ((ScanComponent<INScanCount>) this).Basis.NoteID = entity.NoteID;
        ((ScanComponent<INScanCount>) this).Basis.DocumentView.Current = entity;
      }

      protected virtual void ClearState()
      {
        ((ScanComponent<INScanCount>) this).Basis.RefNbr = (string) null;
        ((ScanComponent<INScanCount>) this).Basis.SiteID = new int?();
        ((ScanComponent<INScanCount>) this).Basis.NoteID = new Guid?();
        ((ScanComponent<INScanCount>) this).Basis.DocumentView.Current = (INPIHeader) null;
      }

      protected virtual void ReportSuccess(INPIHeader entity)
      {
        ((ScanComponent<INScanCount>) this).Basis.Reporter.Info("{0} PI count loaded and ready for processing.", new object[1]
        {
          (object) entity.PIID
        });
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string Prompt = "Scan the reference number of the PI count.";
        public const string Ready = "{0} PI count loaded and ready for processing.";
        public const string Missing = "{0} PI count not found.";
        public const string NotSet = "Document number not selected.";
        public const string InvalidStatus = "The {0} document cannot be used for counting because it has the {1} status.";
      }
    }

    public sealed class InventoryItemState : 
      WarehouseManagementSystem<INScanCount, INScanCount.Host>.InventoryItemState
    {
      protected virtual AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>> HandleAbsence(
        string barcode)
      {
        return ((ScanComponent<INScanCount>) this).Basis.TryProcessBy<INScanCount.CountMode.LocationState>(barcode, (StateSubstitutionRule) 18) ? AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>.op_Implicit(AbsenceHandling.Done) : ((EntityState<INScanCount, PXResult<INItemXRef, InventoryItem>>) this).HandleAbsence(barcode);
      }

      protected override Validation Validate(PXResult<INItemXRef, InventoryItem> entity)
      {
        InventoryItem inventoryItem = PXResult<INItemXRef, InventoryItem>.op_Implicit(entity);
        string str;
        if (((ScanComponent<INScanCount>) this).Basis.IsValid<WMSScanHeader.inventoryID>((object) inventoryItem.InventoryID, ref str))
          return base.Validate(entity);
        return Validation.Fail("The {0} item is not in the list and cannot be added.", new object[1]
        {
          (object) inventoryItem.InventoryCD
        });
      }

      public new abstract class Msg : 
        WarehouseManagementSystem<INScanCount, INScanCount.Host>.InventoryItemState.Msg
      {
        public const string NotPresent = "The {0} item is not in the list and cannot be added.";
      }
    }

    public sealed class LocationState : 
      WarehouseManagementSystem<INScanCount, INScanCount.Host>.LocationState
    {
      protected virtual bool IsStateSkippable()
      {
        return ((ScanComponent<INScanCount>) this).Basis.LocationID.HasValue;
      }

      protected override Validation Validate(INLocation location)
      {
        if (PXResultset<INPIStatusLoc>.op_Implicit(PXSelectBase<INPIStatusLoc, PXViewOf<INPIStatusLoc>.BasedOn<SelectFromBase<INPIStatusLoc, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INPIStatusLoc.pIID, Equal<BqlField<WMSScanHeader.refNbr, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INPIStatusLoc.locationID, Equal<P.AsInt>>>>>.Or<BqlOperand<INPIStatusLoc.locationID, IBqlInt>.IsNull>>>>.ReadOnly.Config>.Select(BarcodeDrivenStateMachine<INScanCount, INScanCount.Host>.op_Implicit((BarcodeDrivenStateMachine<INScanCount, INScanCount.Host>) ((ScanComponent<INScanCount>) this).Basis), new object[1]
        {
          (object) location.LocationID
        })) != null)
          return base.Validate(location);
        return Validation.Fail("The {0} location is not in the list and cannot be added.", new object[1]
        {
          (object) location.LocationCD
        });
      }

      [PXLocalizable]
      public new abstract class Msg
      {
        public const string NotPresent = "The {0} location is not in the list and cannot be added.";
      }
    }

    public sealed class ConfirmState : 
      BarcodeDrivenStateMachine<INScanCount, INScanCount.Host>.ConfirmationState
    {
      public virtual string Prompt
      {
        get
        {
          return ((ScanComponent<INScanCount>) this).Basis.Localize("Confirm counting {0} x {1} {2}.", new object[3]
          {
            (object) ((ScanComponent<INScanCount>) this).Basis.SightOf<WMSScanHeader.inventoryID>(),
            (object) ((ScanComponent<INScanCount>) this).Basis.Qty,
            (object) ((ScanComponent<INScanCount>) this).Basis.UOM
          });
        }
      }

      protected virtual FlowStatus PerformConfirmation()
      {
        return this.Get<INScanCount.CountMode.ConfirmState.Logic>().Confirm();
      }

      public class AddItemQuestion : 
        BarcodeDrivenStateMachine<INScanCount, INScanCount.Host>.ScanQuestion
      {
        public virtual string Code => "NEWLINE";

        protected virtual string GetPrompt()
        {
          return "A new line will be added to the document. To confirm, click OK.";
        }

        protected virtual void Confirm()
        {
          if (!(((ScanComponent<INScanCount>) this).Basis.CurrentState is INScanCount.CountMode.ConfirmState currentState))
            return;
          ((ConfirmationState<INScanCount>) currentState).Confirm();
        }

        protected virtual void Reject()
        {
        }

        [PXLocalizable]
        public abstract class Msg
        {
          public const string Prompt = "A new line will be added to the document. To confirm, click OK.";
        }
      }

      [PXProtectedAccess(null)]
      public abstract class Logic : 
        BarcodeDrivenStateMachine<INScanCount, INScanCount.Host>.ScanExtension
      {
        [PXProtectedAccess(null)]
        protected abstract INPIDetail UpdatePhysicalQty(
          INPIDetail detail,
          (string PIID, int? LocationID, int? InventoryID, int? SubItemID, string LotSerialNbr, DateTime? ExpireDate, Decimal? DeltaBaseQty) item);

        public virtual FlowStatus Confirm()
        {
          FlowStatus error;
          return !this.CanConfirm(out error) ? error : this.ConfirmRow();
        }

        protected virtual bool CanConfirm(out FlowStatus error)
        {
          if (this.Basis.Document == null)
          {
            error = FlowStatus.Fail("Document number not selected.", Array.Empty<object>());
            return false;
          }
          if (!this.Basis.DocumentIsEditable)
          {
            error = FlowStatus.Fail("The {0} document cannot be used for counting because it has the {1} status.", new object[2]
            {
              (object) this.Basis.Document.PIID,
              (object) this.Basis.SightOf<INPIHeader.status>((IBqlTable) this.Basis.Document)
            });
            return false;
          }
          if (!this.Basis.InventoryID.HasValue)
          {
            error = FlowStatus.Fail("Item not selected.", Array.Empty<object>());
            return false;
          }
          if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<INScanCount, INScanCount.Host>.LotSerialState>() && this.Basis.LotSerialNbr == null)
          {
            error = FlowStatus.Fail("Lot or serial number not selected.", Array.Empty<object>());
            return false;
          }
          if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<INScanCount, INScanCount.Host>.LotSerialState>() && this.Basis.LotSerialTrack.IsTrackedSerial)
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

        protected virtual FlowStatus ConfirmRow()
        {
          INPIDetail detailRow = this.FindDetailRow();
          Decimal num1 = Sign.op_Multiply(Sign.MinusIf(this.Basis.Remove.GetValueOrDefault()), this.Basis.BaseQty.Value);
          INPIClass inpiClass = ((PXSelectBase<INPIClass>) this.Basis.Class).SelectSingle(Array.Empty<object>());
          bool? nullable;
          int num2;
          if (inpiClass == null)
          {
            num2 = 0;
          }
          else
          {
            nullable = inpiClass.IncludeZeroItems;
            bool flag = false;
            num2 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
          }
          if (num2 != 0 && detailRow == null && EnumerableExtensions.IsNotIn<string>(((PXSelectBase<ScanInfo>) this.Basis.Info).Current.MessageType, "WRN", "ERR"))
            return FlowStatus.Warn<INScanCount.CountMode.ConfirmState.AddItemQuestion>("{0} item not in list.", new object[1]
            {
              (object) this.Basis.SelectedInventoryItem.InventoryCD
            });
          if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<INScanCount, INScanCount.Host>.LotSerialState>() && this.Basis.LotSerialTrack.IsTrackedSerial && EnumerableExtensions.IsNotIn<Decimal>(((Decimal?) detailRow?.PhysicalQty).GetValueOrDefault() + num1, 0M, 1M))
            return FlowStatus.Fail("Serialized items can be processed only with the base UOM and the 1.00 quantity.", Array.Empty<object>());
          if (((Decimal?) detailRow?.PhysicalQty).GetValueOrDefault() + num1 < 0M)
            return FlowStatus.Fail("Picked quantity cannot be negative.", Array.Empty<object>());
          this.UpdatePhysicalQty(detailRow, (this.Basis.RefNbr, this.Basis.LocationID, this.Basis.InventoryID, this.Basis.SubItemID, this.Basis.LotSerialNbr, this.Basis.ExpireDate, new Decimal?(num1)));
          INScanCount basis = this.Basis;
          nullable = this.Basis.Remove;
          string str = nullable.GetValueOrDefault() ? "{0} x {1} {2} removed." : "{0} x {1} {2} added.";
          object[] objArray = new object[3]
          {
            (object) this.Basis.SightOf<WMSScanHeader.inventoryID>(),
            (object) this.Basis.Qty,
            (object) this.Basis.UOM
          };
          basis.ReportInfo(str, objArray);
          return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
        }

        protected virtual INPIDetail FindDetailRow()
        {
          BqlCommand bqlCommand = BqlCommand.CreateInstance(new Type[1]
          {
            ((PXSelectBase) this.Basis.Details).View.BqlSelect.GetType()
          }).WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INPIDetail.inventoryID, Equal<BqlField<WMSScanHeader.inventoryID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<INPIDetail.siteID, IBqlInt>.IsEqual<BqlField<WMSScanHeader.siteID, IBqlInt>.FromCurrent>>>>();
          if (this.Basis.CurrentMode.HasActive<INScanCount.CountMode.LocationState>() && this.Basis.LocationID.HasValue)
            bqlCommand = bqlCommand.WhereAnd<Where<BqlOperand<INPIDetail.locationID, IBqlInt>.IsEqual<BqlField<WMSScanHeader.locationID, IBqlInt>.FromCurrent>>>();
          if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<INScanCount, INScanCount.Host>.LotSerialState>() && this.Basis.LotSerialNbr != null)
            bqlCommand = bqlCommand.WhereAnd<Where<BqlOperand<INPIDetail.lotSerialNbr, IBqlString>.IsEqual<BqlField<WMSScanHeader.lotSerialNbr, IBqlString>.FromCurrent>>>();
          return PXResult<INPIDetail>.op_Implicit((PXResult<INPIDetail>) ((PXGraph) this.Basis.Graph).TypedViews.GetView(bqlCommand, false).SelectSingle(Array.Empty<object>()));
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string Prompt = "Confirm counting {0} x {1} {2}.";
        public const string Underpicking = "Picked quantity cannot be negative.";
        public const string InventoryAdded = "{0} x {1} {2} added.";
        public const string InventoryRemoved = "{0} x {1} {2} removed.";
        public const string InventoryQtyZero = "{0} item not in list.";
      }
    }

    public sealed class ConfirmCommand : 
      BarcodeDrivenStateMachine<INScanCount, INScanCount.Host>.ScanCommand
    {
      public virtual string Code => "CONFIRM";

      public virtual string ButtonName => "scanConfirmDocument";

      public virtual string DisplayName => "Confirm";

      protected virtual bool IsEnabled
      {
        get => ((ScanComponent<INScanCount>) this).Basis.DocumentIsEditable;
      }

      protected virtual bool Process()
      {
        this.Get<INScanCount.CountMode.ConfirmCommand.Logic>().ConfirmDocument();
        return true;
      }

      public class Logic : BarcodeDrivenStateMachine<INScanCount, INScanCount.Host>.ScanExtension
      {
        public virtual void ConfirmDocument()
        {
          if (this.Basis.Document == null)
            this.Basis.ReportError("Document number not selected.", Array.Empty<object>());
          if (!this.Basis.DocumentIsEditable)
            this.Basis.ReportError("The {0} document cannot be used for counting because it has the {1} status.", new object[2]
            {
              (object) this.Basis.Document.PIID,
              ((PXSelectBase) this.Basis.DocumentView).Cache.GetStateExt<INPIHeader.status>((object) this.Basis.Document)
            });
          foreach (INPIDetail inpiDetail in GraphHelper.RowCast<INPIDetail>((IEnumerable) ((PXGraph) this.Basis.Graph).TypedViews.GetView(((PXSelectBase) this.Basis.Details).View.BqlSelect.WhereAnd<Where<BqlOperand<INPIDetail.physicalQty, IBqlDecimal>.IsNull>>(), false).SelectMulti(Array.Empty<object>())))
          {
            this.Basis.Details.SetValueExt<INPIDetail.physicalQty>(inpiDetail, (object) 0M);
            this.Basis.Details.Update(inpiDetail);
          }
          this.Basis.SaveChanges();
          this.Basis.DocumentView.Current = (INPIHeader) null;
          this.Basis.Reset(true);
          this.Basis.Clear<INScanCount.CountMode.InventoryItemState>(true);
          this.Basis.SetDefaultState((string) null, Array.Empty<object>());
          this.Basis.ReportInfo("Count saved.", Array.Empty<object>());
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string DisplayName = "Confirm";
        public const string CountConfirmed = "Count saved.";
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string Description = "Scan and Count";
    }
  }

  public sealed class RedirectFrom<TForeignBasis> : 
    BarcodeDrivenStateMachine<INScanCount, INScanCount.Host>.RedirectFrom<TForeignBasis>
    where TForeignBasis : PXGraphExtension, IBarcodeDrivenStateMachine
  {
    public virtual string Code => "COUNT";

    public virtual string DisplayName => "PI Count";

    public virtual bool IsPossible => PXAccess.FeatureInstalled<FeaturesSet.wMSInventory>();

    [PXLocalizable]
    public abstract class Msg
    {
      public const string DisplayName = "PI Count";
    }
  }
}
