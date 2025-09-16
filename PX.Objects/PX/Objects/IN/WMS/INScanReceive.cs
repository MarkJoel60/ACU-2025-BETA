// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.INScanReceive
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.Common.Attributes;
using PX.Objects.CS;
using PX.Objects.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#nullable enable
namespace PX.Objects.IN.WMS;

public class INScanReceive : INScanRegisterBase<
#nullable disable
INScanReceive, INScanReceive.Host, INDocType.receipt>
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
      return ((PXSelectBase<INScanSetup>) this.Setup).Current.RequestLocationForEachItemInReceipt.GetValueOrDefault();
    }
  }

  public override bool UseDefaultReasonCode
  {
    get
    {
      return ((PXSelectBase<INScanSetup>) this.Setup).Current.UseDefaultReasonCodeInReceipt.GetValueOrDefault();
    }
  }

  public override bool UseDefaultWarehouse
  {
    get
    {
      return PXSetupBase<INScanRegisterBase<INScanReceive, INScanReceive.Host, INDocType.receipt>.UserSetup, INScanReceive.Host, ScanHeader, INScanUserSetup, Where<INScanUserSetup.userID, Equal<Current<AccessInfo.userID>>, And<INScanUserSetup.mode, Equal<Current<INScanUserSetup.mode>>>>>.For(this.Graph).DefaultWarehouse.GetValueOrDefault();
    }
  }

  protected override bool UseQtyCorrection
  {
    get
    {
      return !((PXSelectBase<INScanSetup>) this.Setup).Current.UseDefaultQtyInReceipt.GetValueOrDefault();
    }
  }

  protected override bool CanOverrideQty
  {
    get
    {
      return (!this.DocumentLoaded || this.NotReleasedAndHasLines) && !this.LotSerialTrack.IsTrackedSerial;
    }
  }

  [BorrowedNote(typeof (INRegister), typeof (INReceiptEntry))]
  protected virtual void _(PX.Data.Events.CacheAttached<ScanHeader.noteID> e)
  {
  }

  protected virtual IEnumerable<ScanMode<INScanReceive>> CreateScanModes()
  {
    yield return (ScanMode<INScanReceive>) new INScanReceive.ReceiptMode();
  }

  public class Host : INReceiptEntry, ICaptionable
  {
    public INScanReceive WMS => ((PXGraph) this).FindImplementation<INScanReceive>();

    string ICaptionable.Caption() => this.WMS.GetCaption();
  }

  public new class QtySupport : 
    WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.QtySupport
  {
  }

  public new class GS1Support : 
    WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.GS1Support
  {
  }

  public new class UserSetup : 
    INScanRegisterBase<INScanReceive, INScanReceive.Host, INDocType.receipt>.UserSetup
  {
  }

  public sealed class ReceiptMode : 
    BarcodeDrivenStateMachine<INScanReceive, INScanReceive.Host>.ScanMode
  {
    public const string Value = "INRE";

    public virtual string Code => "INRE";

    public virtual string Description => "Scan and Receive";

    protected virtual IEnumerable<ScanState<INScanReceive>> CreateStates()
    {
      // ISSUE: reference to a compiler-generated method
      foreach (ScanState<INScanReceive> state in this.\u003C\u003En__0())
        yield return state;
      yield return (ScanState<INScanReceive>) new INScanRegisterBase<INScanReceive, INScanReceive.Host, INDocType.receipt>.WarehouseState();
      yield return (ScanState<INScanReceive>) ((MethodInterceptor<EntityState<INScanReceive, INLocation>, INScanReceive>.OfPredicate) ((EntityState<INScanReceive, INLocation>) new WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.LocationState()).Intercept.IsStateSkippable).ByDisjoin((Func<INScanReceive, bool>) (basis => !basis.PromptLocationForEveryLine && basis.LocationID.HasValue), false, new RelativeInject?());
      yield return (ScanState<INScanReceive>) ((MethodInterceptor<EntityState<INScanReceive, PX.Objects.CS.ReasonCode>, INScanReceive>.OfPredicate) ((EntityState<INScanReceive, PX.Objects.CS.ReasonCode>) new INScanRegisterBase<INScanReceive, INScanReceive.Host, INDocType.receipt>.ReasonCodeState()).Intercept.IsStateSkippable).ByDisjoin((Func<INScanReceive, bool>) (basis => !basis.PromptLocationForEveryLine && basis.ReasonCodeID != null), false, new RelativeInject?());
      yield return (ScanState<INScanReceive>) ((MethodInterceptor<EntityState<INScanReceive, PXResult<INItemXRef, InventoryItem>>, INScanReceive>.OfFunc<string, AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>>) ((EntityState<INScanReceive, PXResult<INItemXRef, InventoryItem>>) new WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.InventoryItemState()).Intercept.HandleAbsence).ByOverride((Func<AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>, string, Func<string, AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>>, AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>>) ((basis, barcode, base_HandleAbsence) => basis.TryProcessBy<WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.LocationState>(barcode, (StateSubstitutionRule) 18) || basis.TryProcessBy<INScanRegisterBase<INScanReceive, INScanReceive.Host, INDocType.receipt>.ReasonCodeState>(barcode, (StateSubstitutionRule) 18) ? AbsenceHandling.Of<PXResult<INItemXRef, InventoryItem>>.op_Implicit(AbsenceHandling.Done) : base_HandleAbsence(barcode)), new RelativeInject?());
      yield return (ScanState<INScanReceive>) ((MethodInterceptor<EntityState<INScanReceive, string>, INScanReceive>.OfPredicate) ((EntityState<INScanReceive, string>) new WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.LotSerialState()).Intercept.IsStateActive).ByConjoin((Func<INScanReceive, bool>) (basis => basis.LotSerialTrack.IsEnterable), false, new RelativeInject?());
      yield return (ScanState<INScanReceive>) ((MethodInterceptor<EntityState<INScanReceive, DateTime?>, INScanReceive>.OfPredicate) ((EntityState<INScanReceive, DateTime?>) new WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.ExpireDateState()).Intercept.IsStateActive).ByConjoin((Func<INScanReceive, bool>) (basis => !basis.EnsureExpireDateDefault().HasValue), false, new RelativeInject?());
      yield return (ScanState<INScanReceive>) new INScanReceive.ReceiptMode.ConfirmState();
    }

    protected virtual IEnumerable<ScanTransition<INScanReceive>> CreateTransitions()
    {
      return ((ScanMode<INScanReceive>) this).Basis.PromptLocationForEveryLine ? ((ScanMode<INScanReceive>) this).StateFlow((Func<ScanStateFlow<INScanReceive>.IFrom, IEnumerable<ScanTransition<INScanReceive>>>) (flow => (IEnumerable<ScanTransition<INScanReceive>>) ((ScanStateFlow<INScanReceive>.INextTo) ((ScanStateFlow<INScanReceive>.INextTo) ((ScanStateFlow<INScanReceive>.INextTo) ((ScanStateFlow<INScanReceive>.INextTo) flow.From<INScanRegisterBase<INScanReceive, INScanReceive.Host, INDocType.receipt>.WarehouseState>().NextTo<WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.InventoryItemState>((Action<INScanReceive>) null)).NextTo<WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.LotSerialState>((Action<INScanReceive>) null)).NextTo<WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.ExpireDateState>((Action<INScanReceive>) null)).NextTo<WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.LocationState>((Action<INScanReceive>) null)).NextTo<INScanRegisterBase<INScanReceive, INScanReceive.Host, INDocType.receipt>.ReasonCodeState>((Action<INScanReceive>) null))) : ((ScanMode<INScanReceive>) this).StateFlow((Func<ScanStateFlow<INScanReceive>.IFrom, IEnumerable<ScanTransition<INScanReceive>>>) (flow => (IEnumerable<ScanTransition<INScanReceive>>) ((ScanStateFlow<INScanReceive>.INextTo) ((ScanStateFlow<INScanReceive>.INextTo) ((ScanStateFlow<INScanReceive>.INextTo) ((ScanStateFlow<INScanReceive>.INextTo) flow.From<INScanRegisterBase<INScanReceive, INScanReceive.Host, INDocType.receipt>.WarehouseState>().NextTo<WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.LocationState>((Action<INScanReceive>) null)).NextTo<INScanRegisterBase<INScanReceive, INScanReceive.Host, INDocType.receipt>.ReasonCodeState>((Action<INScanReceive>) null)).NextTo<WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.InventoryItemState>((Action<INScanReceive>) null)).NextTo<WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.LotSerialState>((Action<INScanReceive>) null)).NextTo<WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.ExpireDateState>((Action<INScanReceive>) null)));
    }

    protected virtual IEnumerable<ScanCommand<INScanReceive>> CreateCommands()
    {
      return (IEnumerable<ScanCommand<INScanReceive>>) new ScanCommand<INScanReceive>[3]
      {
        (ScanCommand<INScanReceive>) new WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.RemoveCommand(),
        (ScanCommand<INScanReceive>) new BarcodeQtySupport<INScanReceive, INScanReceive.Host>.SetQtyCommand(),
        (ScanCommand<INScanReceive>) new INScanReceive.ReceiptMode.ReleaseCommand()
      };
    }

    protected virtual IEnumerable<ScanRedirect<INScanReceive>> CreateRedirects()
    {
      return AllWMSRedirects.CreateFor<INScanReceive>();
    }

    protected virtual void ResetMode(bool fullReset = false)
    {
      ((ScanMode<INScanReceive>) this).ResetMode(fullReset);
      ((ScanMode<INScanReceive>) this).Clear<INScanRegisterBase<INScanReceive, INScanReceive.Host, INDocType.receipt>.WarehouseState>(fullReset && ((ScanMode<INScanReceive>) this).Basis.Document == null);
      ((ScanMode<INScanReceive>) this).Clear<WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.LocationState>(fullReset || ((ScanMode<INScanReceive>) this).Basis.PromptLocationForEveryLine);
      ((ScanMode<INScanReceive>) this).Clear<INScanRegisterBase<INScanReceive, INScanReceive.Host, INDocType.receipt>.ReasonCodeState>(fullReset || ((ScanMode<INScanReceive>) this).Basis.PromptLocationForEveryLine);
      ((ScanMode<INScanReceive>) this).Clear<WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.InventoryItemState>(fullReset);
      ((ScanMode<INScanReceive>) this).Clear<WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.LotSerialState>(true);
      ((ScanMode<INScanReceive>) this).Clear<WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.ExpireDateState>(true);
    }

    public class value : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INScanReceive.ReceiptMode.value>
    {
      public value()
        : base("INRE")
      {
      }
    }

    public sealed class ConfirmState : 
      BarcodeDrivenStateMachine<INScanReceive, INScanReceive.Host>.ConfirmationState
    {
      public virtual string Prompt
      {
        get
        {
          return ((ScanComponent<INScanReceive>) this).Basis.Localize("Confirm receiving {0} x {1} {2}.", new object[3]
          {
            (object) ((ScanComponent<INScanReceive>) this).Basis.SightOf<WMSScanHeader.inventoryID>(),
            (object) ((ScanComponent<INScanReceive>) this).Basis.Qty,
            (object) ((ScanComponent<INScanReceive>) this).Basis.UOM
          });
        }
      }

      protected virtual FlowStatus PerformConfirmation()
      {
        return this.Get<INScanReceive.ReceiptMode.ConfirmState.Logic>().Confirm();
      }

      public class Logic : BarcodeDrivenStateMachine<INScanReceive, INScanReceive.Host>.ScanExtension
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
          if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.LotSerialState>() && this.Basis.LotSerialNbr == null)
          {
            error = FlowStatus.Fail("Lot or serial number not selected.", Array.Empty<object>());
            return false;
          }
          if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.ExpireDateState>() && !this.Basis.ExpireDate.HasValue)
          {
            error = FlowStatus.Fail("Expiration date not selected.", Array.Empty<object>());
            return false;
          }
          if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.LotSerialState>() && this.Basis.LotSerialTrack.IsTrackedSerial)
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
          if (this.Basis.Document == null)
          {
            this.Basis.DocumentView.Insert();
            this.Basis.DocumentView.Current.Hold = new bool?(false);
            this.Basis.DocumentView.Current.Status = "B";
            this.Basis.DocumentView.Current.NoteID = this.Basis.NoteID;
          }
          INTran receiptRow = this.FindReceiptRow();
          INTran tran;
          if (receiptRow != null)
          {
            Decimal? qty1 = receiptRow.Qty;
            Decimal? qty2 = this.Basis.Qty;
            Decimal? nullable1 = qty1.HasValue & qty2.HasValue ? new Decimal?(qty1.GetValueOrDefault() + qty2.GetValueOrDefault()) : new Decimal?();
            if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.LotSerialState>() && this.Basis.LotSerialTrack.IsTrackedSerial)
            {
              Decimal? nullable2 = nullable1;
              Decimal num = (Decimal) 1;
              if (!(nullable2.GetValueOrDefault() == num & nullable2.HasValue))
                return FlowStatus.Fail("Serialized items can be processed only with the base UOM and the 1.00 quantity.", Array.Empty<object>());
            }
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.qty>((object) receiptRow, (object) nullable1);
            tran = this.Basis.Details.Update(receiptRow);
          }
          else
          {
            INTran inTran1 = this.Basis.Details.Insert();
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.inventoryID>((object) inTran1, (object) this.Basis.InventoryID);
            INTran inTran2 = this.Basis.Details.Update(inTran1);
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.siteID>((object) inTran2, (object) this.Basis.SiteID);
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.locationID>((object) inTran2, (object) this.Basis.LocationID);
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.uOM>((object) inTran2, (object) this.Basis.UOM);
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.qty>((object) inTran2, (object) this.Basis.Qty);
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.expireDate>((object) inTran2, (object) this.Basis.ExpireDate);
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.lotSerialNbr>((object) inTran2, (object) this.Basis.LotSerialNbr);
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.reasonCode>((object) inTran2, (object) this.Basis.ReasonCodeID);
            tran = this.Basis.Details.Update(inTran2);
            FlowStatus error;
            if (this.HasErrors(tran, out error))
            {
              ((PXSelectBase<INTran>) ((PXGraphExtension<INScanReceive.Host>) this).Base.transactions).Delete(tran);
              return error;
            }
          }
          if (!string.IsNullOrEmpty(this.Basis.LotSerialNbr))
          {
            foreach (PXResult<INTranSplit> pxResult in ((PXSelectBase<INTranSplit>) this.Basis.Graph.splits).Select(Array.Empty<object>()))
            {
              INTranSplit inTranSplit = PXResult<INTranSplit>.op_Implicit(pxResult);
              ((PXSelectBase) this.Basis.Graph.splits).Cache.SetValueExt<INTranSplit.expireDate>((object) inTranSplit, (object) (this.Basis.ExpireDate ?? tran.ExpireDate));
              ((PXSelectBase) this.Basis.Graph.splits).Cache.SetValueExt<INTranSplit.lotSerialNbr>((object) inTranSplit, (object) this.Basis.LotSerialNbr);
              ((PXSelectBase<INTranSplit>) this.Basis.Graph.splits).Update(inTranSplit);
            }
          }
          this.Basis.ReportInfo("{0} x {1} {2} added to receipt.", new object[3]
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

        protected virtual FlowStatus ConfirmRemove()
        {
          INTran receiptRow = this.FindReceiptRow();
          if (receiptRow == null)
            return FlowStatus.Fail("{0} item not found in receipt.", new object[1]
            {
              (object) this.Basis.SelectedInventoryItem.InventoryCD
            });
          Decimal? qty1 = receiptRow.Qty;
          Decimal? qty2 = this.Basis.Qty;
          if (qty1.GetValueOrDefault() == qty2.GetValueOrDefault() & qty1.HasValue == qty2.HasValue)
          {
            this.Basis.Details.Delete(receiptRow);
          }
          else
          {
            Decimal? qty3 = receiptRow.Qty;
            Decimal? qty4 = this.Basis.Qty;
            Decimal? nullable = qty3.HasValue & qty4.HasValue ? new Decimal?(qty3.GetValueOrDefault() - qty4.GetValueOrDefault()) : new Decimal?();
            string str;
            if (!this.Basis.IsValid<INTran.qty, INTran>(receiptRow, (object) nullable, ref str))
              return FlowStatus.Fail(str, Array.Empty<object>());
            ((PXSelectBase) this.Basis.Details).Cache.SetValueExt<INTran.qty>((object) receiptRow, (object) nullable);
            this.Basis.Details.Update(receiptRow);
          }
          this.Basis.ReportInfo("{0} x {1} {2} removed from receipt.", new object[3]
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
          if (this.Basis.HasUIErrors<INTran>(tran, ref error))
            return true;
          error = FlowStatus.Ok;
          return false;
        }

        protected virtual INTran FindReceiptRow()
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
          INTran receiptRow = (INTran) null;
          if (this.Basis.CurrentMode.HasActive<WarehouseManagementSystem<INScanReceive, INScanReceive.Host>.LotSerialState>())
          {
            foreach (INTran inTran in source)
            {
              this.Basis.Details.Current = inTran;
              if (((IEnumerable<INTranSplit>) ((PXSelectBase<INTranSplit>) this.Basis.Graph.splits).SelectMain(Array.Empty<object>())).Any<INTranSplit>((Func<INTranSplit, bool>) (t => string.Equals(t.LotSerialNbr ?? "", this.Basis.LotSerialNbr ?? "", StringComparison.OrdinalIgnoreCase))))
              {
                receiptRow = inTran;
                break;
              }
            }
          }
          else
            receiptRow = source.FirstOrDefault<INTran>();
          return receiptRow;
        }
      }

      [PXLocalizable]
      public abstract class Msg
      {
        public const string Prompt = "Confirm receiving {0} x {1} {2}.";
        public const string LineMissing = "{0} item not found in receipt.";
        public const string InventoryAdded = "{0} x {1} {2} added to receipt.";
        public const string InventoryRemoved = "{0} x {1} {2} removed from receipt.";
      }
    }

    public sealed class ReleaseCommand : 
      INScanRegisterBase<INScanReceive, INScanReceive.Host, INDocType.receipt>.ReleaseCommand
    {
      protected override string DocumentReleasing => "Release of {0} receipt in progress.";

      protected override string DocumentIsReleased => "Receipt successfully released.";

      protected override string DocumentReleaseFailed => "Receipt not released.";

      protected override async System.Threading.Tasks.Task OnAfterRelease(
        INRegister doc,
        CancellationToken cancellationToken)
      {
        INScanReceive.ReceiptMode.ReleaseCommand releaseCommand = this;
        // ISSUE: reference to a compiler-generated method
        await releaseCommand.\u003C\u003En__0(doc, cancellationToken);
        if (!PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() || doc.RefNbr == null)
          return;
        INScanUserSetup inScanUserSetup = PXSetupBase<INScanRegisterBase<INScanReceive, INScanReceive.Host, INDocType.receipt>.UserSetup, INScanReceive.Host, ScanHeader, INScanUserSetup, Where<INScanUserSetup.userID, Equal<Current<AccessInfo.userID>>, And<INScanUserSetup.mode, Equal<Current<INScanUserSetup.mode>>>>>.For(BarcodeDrivenStateMachine<INScanReceive, INScanReceive.Host>.op_Implicit((BarcodeDrivenStateMachine<INScanReceive, INScanReceive.Host>) ((ScanComponent<INScanReceive>) releaseCommand).Basis));
        bool valueOrDefault = inScanUserSetup.PrintInventoryLabelsAutomatically.GetValueOrDefault();
        string inventoryLabelsReportId = inScanUserSetup.InventoryLabelsReportID;
        if (!valueOrDefault || string.IsNullOrEmpty(inventoryLabelsReportId))
          return;
        Dictionary<string, string> reportParameters = new Dictionary<string, string>()
        {
          ["RefNbr"] = doc.RefNbr
        };
        await DeviceHubTools.PrintReportViaDeviceHub<PX.Objects.CR.BAccount>(BarcodeDrivenStateMachine<INScanReceive, INScanReceive.Host>.op_Implicit((BarcodeDrivenStateMachine<INScanReceive, INScanReceive.Host>) ((ScanComponent<INScanReceive>) releaseCommand).Basis), inventoryLabelsReportId, reportParameters, "None", (PX.Objects.CR.BAccount) null, cancellationToken);
      }

      [PXLocalizable]
      public new abstract class Msg : 
        INScanRegisterBase<INScanReceive, INScanReceive.Host, INDocType.receipt>.ReleaseCommand.Msg
      {
        public const string DocumentReleasing = "Release of {0} receipt in progress.";
        public const string DocumentIsReleased = "Receipt successfully released.";
        public const string DocumentReleaseFailed = "Receipt not released.";
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string Description = "Scan and Receive";
    }
  }

  public new sealed class RedirectFrom<TForeignBasis> : 
    INScanRegisterBase<INScanReceive, INScanReceive.Host, INDocType.receipt>.RedirectFrom<TForeignBasis>
    where TForeignBasis : PXGraphExtension, IBarcodeDrivenStateMachine
  {
    public virtual string Code => "INRECEIVE";

    public virtual string DisplayName => "IN Receive";

    [PXLocalizable]
    public abstract class Msg
    {
      public const string DisplayName = "IN Receive";
    }
  }
}
