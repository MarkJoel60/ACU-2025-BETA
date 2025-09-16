// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.INScanIssue
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Exceptions;
using PX.Objects.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.IN.WMS;

public class INScanIssue : INScanRegisterBase<
#nullable disable
INScanIssue, INScanIssue.Host, INDocType.issue>
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
      return ((PXSelectBase<INScanSetup>) this.Setup).Current.RequestLocationForEachItemInIssue.GetValueOrDefault();
    }
  }

  public override bool UseDefaultReasonCode
  {
    get
    {
      return ((PXSelectBase<INScanSetup>) this.Setup).Current.UseDefaultReasonCodeInIssue.GetValueOrDefault();
    }
  }

  public override bool UseDefaultWarehouse
  {
    get
    {
      return PXSetupBase<INScanRegisterBase<INScanIssue, INScanIssue.Host, INDocType.issue>.UserSetup, INScanIssue.Host, ScanHeader, INScanUserSetup, Where<INScanUserSetup.userID, Equal<Current<AccessInfo.userID>>, And<INScanUserSetup.mode, Equal<Current<INScanUserSetup.mode>>>>>.For(this.Graph).DefaultWarehouse.GetValueOrDefault();
    }
  }

  protected override bool UseQtyCorrection
  {
    get
    {
      return !((PXSelectBase<INScanSetup>) this.Setup).Current.UseDefaultQtyInIssue.GetValueOrDefault();
    }
  }

  protected override bool CanOverrideQty
  {
    get
    {
      return (!this.DocumentLoaded || this.NotReleasedAndHasLines) && !this.LotSerialTrack.IsTrackedSerial;
    }
  }

  [BorrowedNote(typeof (INRegister), typeof (INIssueEntry))]
  protected virtual void _(Events.CacheAttached<ScanHeader.noteID> e)
  {
  }

  protected virtual IEnumerable<ScanMode<INScanIssue>> CreateScanModes()
  {
    yield return (ScanMode<INScanIssue>) new INScanIssue.IssueMode();
  }

  public class Host : INIssueEntry, ICaptionable
  {
    public INScanIssue WMS => ((PXGraph) this).FindImplementation<INScanIssue>();

    string ICaptionable.Caption() => this.WMS.GetCaption();
  }

  public new class QtySupport : WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.QtySupport
  {
  }

  public new class GS1Support : WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.GS1Support
  {
  }

  public new class UserSetup : 
    INScanRegisterBase<INScanIssue, INScanIssue.Host, INDocType.issue>.UserSetup
  {
  }

  public sealed class IssueMode : BarcodeDrivenStateMachine<INScanIssue, INScanIssue.Host>.ScanMode
  {
    public const string Value = "ISSU";

    public virtual string Code => "ISSU";

    public virtual string Description => "Scan and Issue";

    protected virtual IEnumerable<ScanState<INScanIssue>> CreateStates()
    {
      // ISSUE: reference to a compiler-generated method
      foreach (ScanState<INScanIssue> state in this.\u003C\u003En__0())
        yield return state;
      yield return (ScanState<INScanIssue>) new INScanRegisterBase<INScanIssue, INScanIssue.Host, INDocType.issue>.WarehouseState();
      yield return (ScanState<INScanIssue>) ((MethodInterceptor<EntityState<INScanIssue, INLocation>, INScanIssue>.OfPredicate) ((EntityState<INScanIssue, INLocation>) new WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.LocationState()).Intercept.IsStateSkippable).ByDisjoin((Func<INScanIssue, bool>) (basis => !basis.PromptLocationForEveryLine && basis.LocationID.HasValue), false, new RelativeInject?());
      yield return (ScanState<INScanIssue>) ((MethodInterceptor<EntityState<INScanIssue, PXResult<INItemXRef, InventoryItem>>, INScanIssue>.OfFunc<string, AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>>) ((EntityState<INScanIssue, PXResult<INItemXRef, InventoryItem>>) new WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.InventoryItemState()
      {
        IsForIssue = true
      }).Intercept.HandleAbsence).ByOverride((Func<AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>, string, Func<string, AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>>, AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>>) ((basis, barcode, base_HandleAbsence) => basis.TryProcessBy<WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.LocationState>(barcode, (StateSubstitutionRule) 18) || basis.TryProcessBy<INScanRegisterBase<INScanIssue, INScanIssue.Host, INDocType.issue>.ReasonCodeState>(barcode, (StateSubstitutionRule) 18) ? AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>.op_Implicit(AbsenceHandling.Done) : base_HandleAbsence(barcode)), new RelativeInject?());
      yield return (ScanState<INScanIssue>) new WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.LotSerialState();
      yield return (ScanState<INScanIssue>) ((MethodInterceptor<EntityState<INScanIssue, DateTime?>, INScanIssue>.OfPredicate) ((EntityState<INScanIssue, DateTime?>) new WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.ExpireDateState()
      {
        IsForIssue = true
      }).Intercept.IsStateActive).ByConjoin((Func<INScanIssue, bool>) (basis => !basis.EnsureExpireDateDefault().HasValue), false, new RelativeInject?());
      yield return (ScanState<INScanIssue>) ((MethodInterceptor<EntityState<INScanIssue, PX.Objects.CS.ReasonCode>, INScanIssue>.OfPredicate) ((EntityState<INScanIssue, PX.Objects.CS.ReasonCode>) new INScanRegisterBase<INScanIssue, INScanIssue.Host, INDocType.issue>.ReasonCodeState()).Intercept.IsStateSkippable).ByDisjoin((Func<INScanIssue, bool>) (basis => !basis.PromptLocationForEveryLine && basis.ReasonCodeID != null), false, new RelativeInject?());
      yield return (ScanState<INScanIssue>) new INScanIssue.IssueMode.ConfirmState();
    }

    protected virtual IEnumerable<ScanTransition<INScanIssue>> CreateTransitions()
    {
      return ((ScanMode<INScanIssue>) this).Basis.PromptLocationForEveryLine ? ((ScanMode<INScanIssue>) this).StateFlow((Func<ScanStateFlow<INScanIssue>.IFrom, IEnumerable<ScanTransition<INScanIssue>>>) (flow => (IEnumerable<ScanTransition<INScanIssue>>) ((ScanStateFlow<INScanIssue>.INextTo) ((ScanStateFlow<INScanIssue>.INextTo) ((ScanStateFlow<INScanIssue>.INextTo) ((ScanStateFlow<INScanIssue>.INextTo) flow.From<INScanRegisterBase<INScanIssue, INScanIssue.Host, INDocType.issue>.WarehouseState>().NextTo<WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.LocationState>((Action<INScanIssue>) null)).NextTo<WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.InventoryItemState>((Action<INScanIssue>) null)).NextTo<WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.LotSerialState>((Action<INScanIssue>) null)).NextTo<WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.ExpireDateState>((Action<INScanIssue>) null)).NextTo<INScanRegisterBase<INScanIssue, INScanIssue.Host, INDocType.issue>.ReasonCodeState>((Action<INScanIssue>) null))) : ((ScanMode<INScanIssue>) this).StateFlow((Func<ScanStateFlow<INScanIssue>.IFrom, IEnumerable<ScanTransition<INScanIssue>>>) (flow => (IEnumerable<ScanTransition<INScanIssue>>) ((ScanStateFlow<INScanIssue>.INextTo) ((ScanStateFlow<INScanIssue>.INextTo) ((ScanStateFlow<INScanIssue>.INextTo) ((ScanStateFlow<INScanIssue>.INextTo) flow.From<INScanRegisterBase<INScanIssue, INScanIssue.Host, INDocType.issue>.WarehouseState>().NextTo<WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.LocationState>((Action<INScanIssue>) null)).NextTo<INScanRegisterBase<INScanIssue, INScanIssue.Host, INDocType.issue>.ReasonCodeState>((Action<INScanIssue>) null)).NextTo<WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.InventoryItemState>((Action<INScanIssue>) null)).NextTo<WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.LotSerialState>((Action<INScanIssue>) null)).NextTo<WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.ExpireDateState>((Action<INScanIssue>) null)));
    }

    protected virtual IEnumerable<ScanCommand<INScanIssue>> CreateCommands()
    {
      return (IEnumerable<ScanCommand<INScanIssue>>) new ScanCommand<INScanIssue>[3]
      {
        (ScanCommand<INScanIssue>) new WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.RemoveCommand(),
        (ScanCommand<INScanIssue>) new BarcodeQtySupport<INScanIssue, INScanIssue.Host>.SetQtyCommand(),
        (ScanCommand<INScanIssue>) new INScanIssue.IssueMode.ReleaseCommand()
      };
    }

    protected virtual IEnumerable<ScanRedirect<INScanIssue>> CreateRedirects()
    {
      return AllWMSRedirects.CreateFor<INScanIssue>();
    }

    protected virtual void ResetMode(bool fullReset = false)
    {
      ((ScanMode<INScanIssue>) this).ResetMode(fullReset);
      ((ScanMode<INScanIssue>) this).Clear<INScanRegisterBase<INScanIssue, INScanIssue.Host, INDocType.issue>.WarehouseState>(fullReset && ((ScanMode<INScanIssue>) this).Basis.Document == null);
      ((ScanMode<INScanIssue>) this).Clear<WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.LocationState>(fullReset || ((ScanMode<INScanIssue>) this).Basis.PromptLocationForEveryLine);
      ((ScanMode<INScanIssue>) this).Clear<INScanRegisterBase<INScanIssue, INScanIssue.Host, INDocType.issue>.ReasonCodeState>(fullReset || ((ScanMode<INScanIssue>) this).Basis.PromptLocationForEveryLine);
      ((ScanMode<INScanIssue>) this).Clear<WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.InventoryItemState>(fullReset);
      ((ScanMode<INScanIssue>) this).Clear<WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.LotSerialState>(true);
      ((ScanMode<INScanIssue>) this).Clear<WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.ExpireDateState>(true);
    }

    public class value : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INScanIssue.IssueMode.value>
    {
      public value()
        : base("ISSU")
      {
      }
    }

    public sealed class ConfirmState : 
      BarcodeDrivenStateMachine<INScanIssue, INScanIssue.Host>.ConfirmationState
    {
      public virtual string Prompt
      {
        get
        {
          return ((ScanComponent<INScanIssue>) this).Basis.Localize("Confirm issuing {0} x {1} {2}.", new object[3]
          {
            (object) ((ScanComponent<INScanIssue>) this).Basis.SightOf<WMSScanHeader.inventoryID>(),
            (object) ((ScanComponent<INScanIssue>) this).Basis.Qty,
            (object) ((ScanComponent<INScanIssue>) this).Basis.UOM
          });
        }
      }

      protected virtual FlowStatus PerformConfirmation()
      {
        return this.Get<INScanIssue.IssueMode.ConfirmState.Logic>().Confirm();
      }

      public class Logic : BarcodeDrivenStateMachine<INScanIssue, INScanIssue.Host>.ScanExtension
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
          if (!this.Basis.InventoryID.HasValue)
          {
            error = FlowStatus.Fail("Item not selected.", Array.Empty<object>());
            return false;
          }
          if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.LotSerialState>() && this.Basis.LotSerialNbr == null)
          {
            error = FlowStatus.Fail("Lot or serial number not selected.", Array.Empty<object>());
            return false;
          }
          if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.ExpireDateState>() && !this.Basis.ExpireDate.HasValue)
          {
            error = FlowStatus.Fail("Expiration date not selected.", Array.Empty<object>());
            return false;
          }
          if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.LotSerialState>() && this.Basis.LotSerialTrack.IsTrackedSerial)
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
          if (newDocument)
          {
            this.Basis.DocumentView.Insert();
            this.Basis.DocumentView.Current.Hold = new bool?(false);
            this.Basis.DocumentView.Current.Status = "B";
            this.Basis.DocumentView.Current.NoteID = this.Basis.NoteID;
          }
          INTran existTransaction = this.FindIssueRow();
          Action action;
          if (existTransaction != null)
          {
            Decimal? qty1 = existTransaction.Qty;
            Decimal? qty2 = this.Basis.Qty;
            Decimal? nullable1 = qty1.HasValue & qty2.HasValue ? new Decimal?(qty1.GetValueOrDefault() + qty2.GetValueOrDefault()) : new Decimal?();
            LSConfig lotSerialTrack;
            if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.LotSerialState>())
            {
              lotSerialTrack = this.Basis.LotSerialTrack;
              if (lotSerialTrack.IsTrackedSerial)
              {
                Decimal? nullable2 = nullable1;
                Decimal num = (Decimal) 1;
                if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
                  return FlowStatus.Fail("Serialized items can be processed only with the base UOM and the 1.00 quantity.", Array.Empty<object>());
              }
            }
            INTran backup = PXCache<INTran>.CreateCopy(existTransaction);
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.qty>((object) existTransaction, (object) nullable1);
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.lotSerialNbr>((object) existTransaction, (object) null);
            existTransaction = this.Basis.Details.Update(existTransaction);
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.lotSerialNbr>((object) existTransaction, (object) this.Basis.LotSerialNbr);
            lotSerialTrack = this.Basis.LotSerialTrack;
            if (lotSerialTrack.HasExpiration && this.Basis.ExpireDate.HasValue)
              ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.expireDate>((object) existTransaction, (object) this.Basis.ExpireDate);
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
            (^ref withEventFiring).Set<int?>((Expression<Func<INTran, int?>>) (tr => tr.SiteID), this.Basis.SiteID);
            // ISSUE: explicit reference operation
            (^ref withEventFiring).Set<int?>((Expression<Func<INTran, int?>>) (tr => tr.LocationID), this.Basis.LocationID);
            // ISSUE: explicit reference operation
            (^ref withEventFiring).Set<string>((Expression<Func<INTran, string>>) (tr => tr.UOM), this.Basis.UOM);
            // ISSUE: explicit reference operation
            (^ref withEventFiring).Set<string>((Expression<Func<INTran, string>>) (tr => tr.ReasonCode), this.Basis.ReasonCodeID);
            existTransaction = this.Basis.Details.Update(existTransaction);
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.qty>((object) existTransaction, (object) this.Basis.Qty);
            existTransaction = this.Basis.Details.Update(existTransaction);
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.lotSerialNbr>((object) existTransaction, (object) this.Basis.LotSerialNbr);
            if (this.Basis.LotSerialTrack.HasExpiration && this.Basis.ExpireDate.HasValue)
              ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.expireDate>((object) existTransaction, (object) this.Basis.ExpireDate);
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
          this.Basis.ReportInfo("{0} x {1} {2} added to issue.", new object[3]
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

        protected virtual bool HasErrors(INTran tran, out FlowStatus error)
        {
          if (this.Basis.HasUIErrors<INTran>(tran, ref error) || this.HasLotSerialError(tran, out error) || this.HasLocationError(tran, out error) || this.HasAvailabilityError(tran, out error))
            return true;
          error = FlowStatus.Ok;
          return false;
        }

        protected virtual bool HasLotSerialError(INTran tran, out FlowStatus error)
        {
          if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.LotSerialState>() && !string.IsNullOrEmpty(this.Basis.LotSerialNbr) && ((IEnumerable<INTranSplit>) ((PXSelectBase<INTranSplit>) this.Basis.Graph.splits).SelectMain(Array.Empty<object>())).Any<INTranSplit>((Func<INTranSplit, bool>) (s => s.LotSerialNbr != this.Basis.LotSerialNbr)))
          {
            error = FlowStatus.Fail("The quantity of the {1} item in the issue exceeds the item's quantity in the {0} lot.", new object[2]
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
          if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.LocationState>() && ((IEnumerable<INTranSplit>) ((PXSelectBase<INTranSplit>) this.Basis.Graph.splits).SelectMain(Array.Empty<object>())).Any<INTranSplit>((Func<INTranSplit, bool>) (s =>
          {
            int? locationId1 = s.LocationID;
            int? locationId2 = this.Basis.LocationID;
            return !(locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue);
          })))
          {
            error = FlowStatus.Fail("The quantity of the {1} item in the issue exceeds the item's quantity in the {0} location.", new object[2]
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
          if (pxExceptionInfo != null)
          {
            PXCache cache = ((PXSelectBase) this.Basis.Graph.transactions).Cache;
            error = FlowStatus.Fail(pxExceptionInfo.MessageFormat, new object[5]
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
          INTran issueRow = this.FindIssueRow();
          if (issueRow == null)
            return FlowStatus.Fail("{0} item not found in issue.", new object[1]
            {
              (object) this.Basis.SelectedInventoryItem.InventoryCD
            });
          Decimal? qty1 = issueRow.Qty;
          Decimal? qty2 = this.Basis.Qty;
          if (qty1.GetValueOrDefault() == qty2.GetValueOrDefault() & qty1.HasValue == qty2.HasValue)
          {
            this.Basis.Details.Delete(issueRow);
          }
          else
          {
            Decimal? qty3 = issueRow.Qty;
            Decimal? qty4 = this.Basis.Qty;
            Decimal? nullable = qty3.HasValue & qty4.HasValue ? new Decimal?(qty3.GetValueOrDefault() - qty4.GetValueOrDefault()) : new Decimal?();
            string str;
            if (!this.Basis.IsValid<INTran.qty, INTran>(issueRow, (object) nullable, ref str))
              return FlowStatus.Fail(str, Array.Empty<object>());
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.qty>((object) issueRow, (object) nullable);
            this.Basis.Details.Update(issueRow);
          }
          this.Basis.ReportInfo("{0} x {1} {2} removed from issue.", new object[3]
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

        protected virtual INTran FindIssueRow()
        {
          IEnumerable<INTran> source = ((IEnumerable<INTran>) this.Basis.Details.SelectMain(Array.Empty<object>())).Where<INTran>((Func<INTran, bool>) (t =>
          {
            int? inventoryId1 = t.InventoryID;
            int? inventoryId2 = this.Basis.InventoryID;
            if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
            {
              int? siteId1 = t.SiteID;
              int? siteId2 = this.Basis.SiteID;
              if (siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue)
              {
                int? locationId = t.LocationID;
                int? nullable = this.Basis.LocationID ?? t.LocationID;
                if (locationId.GetValueOrDefault() == nullable.GetValueOrDefault() & locationId.HasValue == nullable.HasValue && t.ReasonCode == (this.Basis.ReasonCodeID ?? t.ReasonCode))
                  return t.UOM == this.Basis.UOM;
              }
            }
            return false;
          }));
          INTran issueRow = (INTran) null;
          if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<INScanIssue, INScanIssue.Host>.LotSerialState>())
          {
            foreach (INTran inTran in source)
            {
              this.Basis.Details.Current = inTran;
              if (((IEnumerable<INTranSplit>) ((PXSelectBase<INTranSplit>) this.Basis.Graph.splits).SelectMain(Array.Empty<object>())).Any<INTranSplit>((Func<INTranSplit, bool>) (t => string.Equals(t.LotSerialNbr ?? "", this.Basis.LotSerialNbr ?? "", StringComparison.OrdinalIgnoreCase))))
              {
                issueRow = inTran;
                break;
              }
            }
          }
          else
            issueRow = source.FirstOrDefault<INTran>();
          return issueRow;
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string Prompt = "Confirm issuing {0} x {1} {2}.";
        public const string LineMissing = "{0} item not found in issue.";
        public const string InventoryAdded = "{0} x {1} {2} added to issue.";
        public const string InventoryRemoved = "{0} x {1} {2} removed from issue.";
        public const string QtyExceedsOnLot = "The quantity of the {1} item in the issue exceeds the item's quantity in the {0} lot.";
        public const string QtyExceedsOnLocation = "The quantity of the {1} item in the issue exceeds the item's quantity in the {0} location.";
      }
    }

    public sealed class ReleaseCommand : 
      INScanRegisterBase<INScanIssue, INScanIssue.Host, INDocType.issue>.ReleaseCommand
    {
      protected override string DocumentReleasing => "Release of {0} issue in progress.";

      protected override string DocumentIsReleased => "Issue successfully released.";

      protected override string DocumentReleaseFailed => "Issue not released.";

      [PXLocalizable]
      public new abstract class Msg : 
        INScanRegisterBase<INScanIssue, INScanIssue.Host, INDocType.issue>.ReleaseCommand.Msg
      {
        public const string DocumentReleasing = "Release of {0} issue in progress.";
        public const string DocumentIsReleased = "Issue successfully released.";
        public const string DocumentReleaseFailed = "Issue not released.";
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string Description = "Scan and Issue";
    }
  }

  public new sealed class RedirectFrom<TForeignBasis> : 
    INScanRegisterBase<INScanIssue, INScanIssue.Host, INDocType.issue>.RedirectFrom<TForeignBasis>
    where TForeignBasis : PXGraphExtension, IBarcodeDrivenStateMachine
  {
    public virtual string Code => "INISSUE";

    public virtual string DisplayName => "IN Issue";

    [PXLocalizable]
    public abstract class Msg
    {
      public const string DisplayName = "IN Issue";
    }
  }
}
