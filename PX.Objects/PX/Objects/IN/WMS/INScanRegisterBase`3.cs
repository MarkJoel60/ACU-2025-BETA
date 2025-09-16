// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.INScanRegisterBase`3
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

#nullable enable
namespace PX.Objects.IN.WMS;

public abstract class INScanRegisterBase<TSelf, TGraph, TDocType> : 
  WarehouseManagementSystem<
  #nullable disable
  TSelf, TGraph>
  where TSelf : INScanRegisterBase<TSelf, TGraph, TDocType>
  where TGraph : INRegisterEntryBase, new()
  where TDocType : IConstant, IBqlOperand, new()
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
  RegisterScanHeader RegisterHeader
  {
    get => ScanHeaderExt.Get<RegisterScanHeader>(this.Header) ?? new RegisterScanHeader();
  }

  public ValueSetter<ScanHeader>.Ext<RegisterScanHeader> RegisterSetter
  {
    get => this.HeaderSetter.With<RegisterScanHeader>();
  }

  public string DocType => this.RegisterHeader.DocType;

  public string ReasonCodeID
  {
    get => this.RegisterHeader.ReasonCodeID;
    set
    {
      ValueSetter<ScanHeader>.Ext<RegisterScanHeader> registerSetter = this.RegisterSetter;
      (^ref registerSetter).Set<string>((Expression<Func<RegisterScanHeader, string>>) (h => h.ReasonCodeID), value);
    }
  }

  public PX.Objects.CS.ReasonCode SelectedReasonCode
  {
    get => PX.Objects.CS.ReasonCode.PK.Find((PXGraph) (object) this.Graph, this.ReasonCodeID);
  }

  public INRegister Document => this.DocumentView.Current;

  public PXSelectBase<INRegister> DocumentView => this.Graph.INRegisterDataMember;

  public PXSelectBase<INTran> Details => this.Graph.INTranDataMember;

  public bool NotReleasedAndHasLines
  {
    get
    {
      INRegister document = this.Document;
      return (document != null ? (!document.Released.GetValueOrDefault() ? 1 : 0) : 1) != 0 && ((IEnumerable<INTran>) this.Details.SelectMain(Array.Empty<object>())).Any<INTran>();
    }
  }

  public abstract bool PromptLocationForEveryLine { get; }

  public abstract bool UseDefaultReasonCode { get; }

  public abstract bool UseDefaultWarehouse { get; }

  public override bool DocumentLoaded => this.Document != null;

  public override bool DocumentIsEditable
  {
    get
    {
      if (!base.DocumentIsEditable)
        return false;
      TypeArrayOf<IBqlField>.IFilledWith<INRegister.docType, INRegister.refNbr> ifilledWith = PrimaryKeyOf<INRegister>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<INRegister.docType, INRegister.refNbr>>.Find((PXGraph) (object) ((PXGraphExtension<TGraph>) this).Base, (TypeArrayOf<IBqlField>.IFilledWith<INRegister.docType, INRegister.refNbr>) this.Document, (PKFindOptions) 0);
      return ifilledWith == null || !((INRegister) ifilledWith).Released.GetValueOrDefault();
    }
  }

  protected override void _(Events.RowSelected<ScanHeader> e)
  {
    base._(e);
    if (this.Document == null && !string.IsNullOrEmpty(this.RefNbr))
    {
      if (this.Header.ProcessingSucceeded.HasValue)
      {
        GraphHelper.Caches<ScanHeader>((PXGraph) (object) this.Graph).RaiseFieldUpdated<string, WMSScanHeader>((Expression<Func<WMSScanHeader, string>>) (w => w.RefNbr), this.Header, this.RefNbr);
      }
      else
      {
        this.RefNbr = (string) null;
        this.NoteID = new Guid?();
      }
    }
    else if (this.Document != null && string.IsNullOrEmpty(this.RefNbr) && ((PXSelectBase) this.DocumentView).Cache.GetStatus((object) this.Document) != 2)
    {
      this.RefNbr = this.Document.RefNbr;
      this.NoteID = this.Document.NoteID;
    }
    ((PXSelectBase) this.Details).Cache.SetAllEditPermissions(this.Document == null || !this.Document.Released.GetValueOrDefault());
    ((PXSelectBase) this.Details).Cache.AllowInsert = false;
  }

  protected virtual void _(
    Events.FieldDefaulting<ScanHeader, RegisterScanHeader.docType> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<ScanHeader, RegisterScanHeader.docType>, ScanHeader, object>) e).NewValue = new TDocType().Value;
  }

  protected virtual void _(
    Events.FieldUpdated<ScanHeader, WMSScanHeader.refNbr> e)
  {
    this.DocumentView.Current = PXResultset<INRegister>.op_Implicit(e.NewValue == null ? (PXResultset<INRegister>) null : this.DocumentView.Search<INRegister.refNbr>(e.NewValue, Array.Empty<object>()));
  }

  protected virtual void _(Events.RowSelected<INTran> e)
  {
    bool isMobileAndNotReleased = ((PXGraph) (object) this.Graph).IsMobile && (this.Document == null || !this.Document.Released.GetValueOrDefault());
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = PXCacheEx.AdjustUI(((PXSelectBase) this.Details).Cache, (object) null).For<INTran.inventoryID>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = false));
    chained = chained.SameFor<INTran.tranDesc>();
    chained = chained.SameFor<INTran.qty>();
    chained = chained.SameFor<INTran.uOM>();
    chained = chained.For<INTran.lotSerialNbr>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = isMobileAndNotReleased));
    chained = chained.SameFor<INTran.expireDate>();
    chained = chained.SameFor<INTran.reasonCode>();
    chained.SameFor<INTran.locationID>();
  }

  protected virtual void _(Events.RowUpdated<INScanUserSetup> e)
  {
    e.Row.IsOverridden = new bool?(!e.Row.SameAs(((PXSelectBase<INScanSetup>) this.Setup).Current));
  }

  protected virtual void _(Events.RowInserted<INScanUserSetup> e)
  {
    e.Row.IsOverridden = new bool?(!e.Row.SameAs(((PXSelectBase<INScanSetup>) this.Setup).Current));
  }

  [PXMergeAttributes]
  [PXUnboundDefault(typeof (INRegister.refNbr))]
  [PXSelector(typeof (SearchFor<INRegister.refNbr>.Where<BqlOperand<INRegister.docType, IBqlString>.IsEqual<BqlField<RegisterScanHeader.docType, IBqlString>.FromCurrent>>))]
  protected virtual void _(Events.CacheAttached<WMSScanHeader.refNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUnboundDefault(typeof (SwitchMirror<IBqlString, TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Empty, Case<Where<BqlOperand<Current<RegisterScanHeader.docType>, IBqlString>.IsEqual<INDocType.transfer>>, INTranType.transfer>>, Case<Where<BqlOperand<Current<RegisterScanHeader.docType>, IBqlString>.IsEqual<INDocType.issue>>, INTranType.issue>, INTranType.receipt>.When<BqlOperand<Current<RegisterScanHeader.docType>, IBqlString>.IsEqual<INDocType.receipt>>.ElseNull))]
  protected virtual void _(Events.CacheAttached<WMSScanHeader.tranType> e)
  {
  }

  [PXMergeAttributes]
  [PXUnboundDefault(typeof (Switch<IBqlShort, TypeArrayOf<IBqlCase>.Empty, Case<Where<BqlOperand<Current<RegisterScanHeader.docType>, IBqlString>.IsIn<INDocType.issue, INDocType.transfer>>, InventoryMultiplicator.decrease>, InventoryMultiplicator.increase>.When<BqlOperand<Current<RegisterScanHeader.docType>, IBqlString>.IsEqual<INDocType.receipt>>.ElseNull))]
  protected virtual void _(
    Events.CacheAttached<WMSScanHeader.inventoryMultiplicator> e)
  {
  }

  protected virtual bool ProcessSingleBarcode(string barcode)
  {
    if (this.Header.ProcessingSucceeded.GetValueOrDefault())
    {
      TypeArrayOf<IBqlField>.IFilledWith<INRegister.docType, INRegister.refNbr> ifilledWith = PrimaryKeyOf<INRegister>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<INRegister.docType, INRegister.refNbr>>.Find((PXGraph) (object) this.Graph, (TypeArrayOf<IBqlField>.IFilledWith<INRegister.docType, INRegister.refNbr>) this.Document, (PKFindOptions) 0);
      if ((ifilledWith != null ? (((INRegister) ifilledWith).Released.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        this.RefNbr = (string) null;
        this.NoteID = new Guid?();
      }
    }
    return base.ProcessSingleBarcode(barcode);
  }

  protected virtual ScanCommand<TSelf> DecorateScanCommand(ScanCommand<TSelf> original)
  {
    ScanCommand<TSelf> scanCommand = base.DecorateScanCommand(original);
    if (scanCommand is WarehouseManagementSystem<TSelf, TGraph>.RemoveCommand removeCommand)
      ((MethodInterceptor<ScanCommand<TSelf>, TSelf>.OfPredicate) ((ScanCommand<TSelf>) removeCommand).Intercept.IsEnabled).ByConjoin((Func<TSelf, bool>) (basis => basis.NotReleasedAndHasLines), false, new RelativeInject?());
    if (!(scanCommand is BarcodeQtySupport<TSelf, TGraph>.SetQtyCommand setQtyCommand))
      return scanCommand;
    ((MethodInterceptor<ScanCommand<TSelf>, TSelf>.OfPredicate) ((ScanCommand<TSelf>) setQtyCommand).Intercept.IsEnabled).ByConjoin((Func<TSelf, bool>) (basis => basis.UseQtyCorrection.Implies(basis.DocumentIsEditable && basis.NotReleasedAndHasLines)), false, new RelativeInject?());
    return scanCommand;
  }

  /// Overrides <see cref="M:PX.Data.PXGraph.Persist" />
  [PXOverride]
  public virtual void Persist(Action base_Persist)
  {
    base_Persist();
    this.RefNbr = this.Document?.RefNbr;
    this.NoteID = (Guid?) this.Document?.NoteID;
    ((PXSelectBase) this.Details).Cache.Clear();
    ((PXSelectBase) this.Details).Cache.ClearQueryCacheObsolete();
  }

  public abstract class UserSetup : 
    PXUserSetupPerMode<INScanRegisterBase<TSelf, TGraph, TDocType>.UserSetup, TGraph, ScanHeader, INScanUserSetup, INScanUserSetup.userID, INScanUserSetup.mode, TDocType>
  {
  }

  public new sealed class WarehouseState : WarehouseManagementSystem<TSelf, TGraph>.WarehouseState
  {
    protected override bool UseDefaultWarehouse
    {
      get => ((ScanComponent<TSelf>) this).Basis.UseDefaultWarehouse;
    }

    protected override int? DefaultSiteID
    {
      get => (int?) ((ScanComponent<TSelf>) this).Basis.Document?.SiteID ?? base.DefaultSiteID;
    }
  }

  public sealed class ReasonCodeState : 
    BarcodeDrivenStateMachine<TSelf, TGraph>.EntityState<PX.Objects.CS.ReasonCode>
  {
    public const string Value = "RSNC";

    public virtual string Code => "RSNC";

    protected virtual string StatePrompt => "Scan the reason code.";

    protected virtual bool IsStateActive()
    {
      return !((ScanComponent<TSelf>) this).Basis.UseDefaultReasonCode;
    }

    protected virtual PX.Objects.CS.ReasonCode GetByBarcode(string barcode)
    {
      return PX.Objects.CS.ReasonCode.PK.Find(BarcodeDrivenStateMachine<TSelf, TGraph>.op_Implicit((BarcodeDrivenStateMachine<TSelf, TGraph>) ((ScanComponent<TSelf>) this).Basis), barcode);
    }

    protected virtual void ReportMissing(string barcode)
    {
      ((ScanComponent<TSelf>) this).Basis.Reporter.Error("{0} reason code not found.", new object[1]
      {
        (object) barcode
      });
    }

    protected virtual Validation Validate(PX.Objects.CS.ReasonCode reasonCode)
    {
      string str;
      return !((ScanComponent<TSelf>) this).Basis.IsValid<RegisterScanHeader.reasonCodeID>((object) reasonCode.ReasonCodeID, ref str) ? Validation.Fail(str, Array.Empty<object>()) : Validation.Ok;
    }

    protected virtual void Apply(PX.Objects.CS.ReasonCode reasonCode)
    {
      ((ScanComponent<TSelf>) this).Basis.ReasonCodeID = reasonCode.ReasonCodeID;
    }

    protected virtual void ReportSuccess(PX.Objects.CS.ReasonCode reasonCode)
    {
      ((ScanComponent<TSelf>) this).Basis.Reporter.Info("{0} reason code selected.", new object[1]
      {
        (object) (reasonCode.Descr ?? reasonCode.ReasonCodeID)
      });
    }

    protected virtual void ClearState()
    {
      ((ScanComponent<TSelf>) this).Basis.ReasonCodeID = (string) null;
    }

    public class value : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      INScanRegisterBase<TSelf, TGraph, TDocType>.ReasonCodeState.value>
    {
      public value()
        : base("RSNC")
      {
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string Prompt = "Scan the reason code.";
      public const string Ready = "{0} reason code selected.";
      public const string Missing = "{0} reason code not found.";
      public const string NotSet = "Reason code not selected.";
    }
  }

  public abstract class ReleaseCommand : BarcodeDrivenStateMachine<TSelf, TGraph>.ScanCommand
  {
    public virtual string Code => "RELEASE";

    public virtual string ButtonName => "scanRelease";

    public virtual string DisplayName => "Release";

    protected virtual bool IsEnabled
    {
      get
      {
        return ((ScanComponent<TSelf>) this).Basis.DocumentIsEditable && ((ScanComponent<TSelf>) this).Basis.NotReleasedAndHasLines;
      }
    }

    protected virtual bool Process()
    {
      if (((ScanComponent<TSelf>) this).Basis.Document == null)
        return false;
      bool? nullable = ((ScanComponent<TSelf>) this).Basis.Document.Released;
      if (nullable.GetValueOrDefault())
      {
        ((ScanComponent<TSelf>) this).Basis.ReportError("Document Status is invalid for processing.", Array.Empty<object>());
        return true;
      }
      nullable = ((ScanComponent<TSelf>) this).Basis.Document.Hold;
      bool flag = false;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        ((ScanComponent<TSelf>) this).Basis.DocumentView.SetValueExt<INRegister.hold>(((ScanComponent<TSelf>) this).Basis.Document, (object) false);
      ((PXAction) ((ScanComponent<TSelf>) this).Basis.Save).Press();
      ((ScanComponent<TSelf>) this).Basis.Reset(false);
      ((ScanComponent<TSelf>) this).Basis.Clear<WarehouseManagementSystem<TSelf, TGraph>.InventoryItemState>(true);
      (string, string) msg = (this.DocumentIsReleased, this.DocumentReleaseFailed);
      ((ScanComponent<TSelf>) this).Basis.AwaitFor<INRegister>((Func<TSelf, INRegister, CancellationToken, System.Threading.Tasks.Task>) (async (basis, doc, ct) =>
      {
        INDocumentRelease.ReleaseDoc(new List<INRegister>()
        {
          doc
        }, false);
        await basis.CurrentMode.Commands.OfType<INScanRegisterBase<TSelf, TGraph, TDocType>.ReleaseCommand>().FirstOrDefault<INScanRegisterBase<TSelf, TGraph, TDocType>.ReleaseCommand>()?.OnAfterRelease(doc, ct);
      })).WithDescription(this.DocumentReleasing, new object[1]
      {
        (object) ((ScanComponent<TSelf>) this).Basis.Document.RefNbr
      }).ActualizeDataBy((Func<TSelf, INRegister, INRegister>) ((basis, doc) => (INRegister) PrimaryKeyOf<INRegister>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<INRegister.docType, INRegister.refNbr>>.Find(BarcodeDrivenStateMachine<TSelf, TGraph>.op_Implicit((BarcodeDrivenStateMachine<TSelf, TGraph>) basis), (TypeArrayOf<IBqlField>.IFilledWith<INRegister.docType, INRegister.refNbr>) doc, (PKFindOptions) 0))).OnSuccess(new Action<ScanLongRunAwaiter<TSelf, INRegister>.ISuccessProcessor>(this.ConfigureOnSuccessAction)).OnFail((Action<ScanLongRunAwaiter<TSelf, INRegister>.IResultProcessor>) (x => x.Say(msg.Item2, Array.Empty<object>()))).BeginAwait(((ScanComponent<TSelf>) this).Basis.Document);
      return true;
    }

    protected virtual System.Threading.Tasks.Task OnAfterRelease(
      INRegister doc,
      CancellationToken cancellationToken)
    {
      return System.Threading.Tasks.Task.CompletedTask;
    }

    public virtual void ConfigureOnSuccessAction(
      ScanLongRunAwaiter<TSelf, INRegister>.ISuccessProcessor onSuccess)
    {
      onSuccess.Say(this.DocumentIsReleased, Array.Empty<object>());
    }

    protected abstract string DocumentReleasing { get; }

    protected abstract string DocumentIsReleased { get; }

    protected abstract string DocumentReleaseFailed { get; }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string DisplayName = "Release";
    }
  }

  public abstract class RedirectFrom<TForeignBasis> : 
    BarcodeDrivenStateMachine<TSelf, TGraph>.RedirectFrom<TForeignBasis>
    where TForeignBasis : PXGraphExtension, IBarcodeDrivenStateMachine
  {
    public virtual bool IsPossible => PXAccess.FeatureInstalled<FeaturesSet.wMSInventory>();
  }
}
