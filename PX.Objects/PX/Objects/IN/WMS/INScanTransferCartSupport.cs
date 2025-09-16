// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.INScanTransferCartSupport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.IN.DAC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.IN.WMS;

public class INScanTransferCartSupport : CartSupport<INScanTransfer, INScanTransfer.Host>
{
  public FbqlSelect<SelectFromBase<INRegisterCart, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<INRegisterCart.siteID>.IsRelatedTo<INCart.siteID>, Field<INRegisterCart.cartID>.IsRelatedTo<INCart.cartID>>.WithTablesOf<INCart, INRegisterCart>, INCart, INRegisterCart>.SameAsCurrent.And<KeysRelation<CompositeKey<Field<INRegisterCart.docType>.IsRelatedTo<PX.Objects.IN.INRegister.docType>, Field<INRegisterCart.refNbr>.IsRelatedTo<PX.Objects.IN.INRegister.refNbr>>.WithTablesOf<PX.Objects.IN.INRegister, INRegisterCart>, PX.Objects.IN.INRegister, INRegisterCart>.SameAsCurrent>>, INRegisterCart>.View CartsLinks;
  public FbqlSelect<SelectFromBase<INRegisterCartLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCartSplit>.On<INRegisterCartLine.FK.CartSplit>>>.Where<KeysRelation<CompositeKey<Field<INRegisterCartLine.siteID>.IsRelatedTo<INCart.siteID>, Field<INRegisterCartLine.cartID>.IsRelatedTo<INCart.cartID>>.WithTablesOf<INCart, INRegisterCartLine>, INCart, INRegisterCartLine>.SameAsCurrent>, INRegisterCartLine>.View CartSplitLinks;

  public static bool IsActive() => CartSupport<INScanTransfer, INScanTransfer.Host>.IsActiveBase();

  public override bool IsCartRequired()
  {
    return ((PXSelectBase<INScanSetup>) this.Basis.Setup).Current.UseCartsForTransfers.GetValueOrDefault();
  }

  public virtual bool IsCartEmpty
  {
    get
    {
      return !((IEnumerable<INRegisterCartLine>) ((PXSelectBase<INRegisterCartLine>) this.CartSplitLinks).SelectMain(Array.Empty<object>())).Any<INRegisterCartLine>();
    }
  }

  public virtual INRegisterCart[] GetCartRegisters(INCart cart)
  {
    return GraphHelper.RowCast<INRegisterCart>((IEnumerable) PXSelectBase<INRegisterCart, PXViewOf<INRegisterCart>.BasedOn<SelectFromBase<INRegisterCart, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<INRegisterCart.siteID>.IsRelatedTo<INCart.siteID>, Field<INRegisterCart.cartID>.IsRelatedTo<INCart.cartID>>.WithTablesOf<INCart, INRegisterCart>, INCart, INRegisterCart>.SameAsCurrent>>.ReadOnly.Config>.SelectMultiBound(BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>.op_Implicit((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) this.Basis), new object[1]
    {
      (object) cart
    }, Array.Empty<object>())).ToArray<INRegisterCart>();
  }

  public Decimal? GetCartQty(INTran line)
  {
    if (line == null)
      return new Decimal?();
    return PXResultset<INRegisterCartLine>.op_Implicit(((PXSelectBase<INRegisterCartLine>) this.CartSplitLinks).Search<INRegisterCartLine.lineNbr>((object) line.LineNbr, Array.Empty<object>()))?.Qty;
  }

  /// <summary>
  /// Delegate for <see cref="F:PX.Objects.IN.INTransferEntry.transactions" />
  /// </summary>
  public IEnumerable Transactions()
  {
    if (!this.IsCartRequired())
      return (IEnumerable) null;
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    pxDelegateResult.IsResultSorted = true;
    pxDelegateResult.IsResultFiltered = true;
    pxDelegateResult.IsResultTruncated = true;
    PXView pxView = new PXView((PXGraph) ((PXGraphExtension<INScanTransfer.Host>) this).Base, false, ((PXSelectBase) ((PXGraphExtension<INScanTransfer.Host>) this).Base.transactions).View.BqlSelect);
    int startRow = PXView.StartRow;
    int num1 = 0;
    object[] currents = PXView.Currents;
    object[] parameters = PXView.Parameters;
    object[] searches = PXView.Searches;
    string[] sortColumns = PXView.SortColumns;
    bool[] descendings = PXView.Descendings;
    PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
    ref int local1 = ref startRow;
    int maximumRows = PXView.MaximumRows;
    ref int local2 = ref num1;
    ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) pxView.Select(currents, parameters, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2).OfType<INTran>().Select(x =>
    {
      INTran inTran = x;
      Decimal? cartQty = this.GetCartQty(x);
      Decimal num2 = 0M;
      int num3 = cartQty.GetValueOrDefault() > num2 & cartQty.HasValue ? 1 : 0;
      return new{ Line = inTran, IsInCart = num3 != 0 };
    }).OrderByDescending(x => x.IsInCart).Select(x => x.Line).ToList<INTran>());
    return (IEnumerable) pxDelegateResult;
  }

  [PXOverride]
  public virtual ScanMode<INScanTransfer> DecorateScanMode(
    ScanMode<INScanTransfer> original,
    Func<ScanMode<INScanTransfer>, ScanMode<INScanTransfer>> base_DecorateScanMode)
  {
    ScanMode<INScanTransfer> scanMode = base_DecorateScanMode(original);
    if (!this.IsCartRequired() || !(scanMode is INScanTransfer.TransferMode mode))
      return scanMode;
    this.InjectCartState((ScanMode<INScanTransfer>) mode);
    this.InjectCartCommands((ScanMode<INScanTransfer>) mode);
    ((MethodInterceptor<ScanMode<INScanTransfer>, INScanTransfer>.OfAction<bool>) ((ScanMode<INScanTransfer>) mode).Intercept.ResetMode).ByReplace((Action<INScanTransfer, bool>) ((basis, fullReset) =>
    {
      basis.Get<INScanTransferCartSupport>();
      basis.Clear<INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.WarehouseState>(fullReset);
      basis.Clear<CartSupport<INScanTransfer, INScanTransfer.Host>.CartState>(fullReset && !basis.IsWithinReset);
      basis.Clear<INScanTransfer.TransferMode.SourceLocationState>(fullReset || basis.PromptLocationForEveryLine);
      basis.Clear<INScanTransfer.TransferMode.TargetLocationState>(fullReset || basis.PromptLocationForEveryLine);
      basis.Clear<INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.ReasonCodeState>(fullReset || basis.PromptLocationForEveryLine);
      basis.Clear<WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.InventoryItemState>(fullReset);
      basis.Clear<WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.LotSerialState>(true);
    }), new RelativeInject?((RelativeInject) 0));
    ((MethodInterceptor<ScanMode<INScanTransfer>, INScanTransfer>.OfFunc<IEnumerable<ScanTransition<INScanTransfer>>>) ((ScanMode<INScanTransfer>) mode).Intercept.CreateTransitions).ByReplace((Func<INScanTransfer, IEnumerable<ScanTransition<INScanTransfer>>>) (basis => basis.PromptLocationForEveryLine ? basis.StateFlow((Func<ScanStateFlow<INScanTransfer>.IFrom, IEnumerable<ScanTransition<INScanTransfer>>>) (flow => (IEnumerable<ScanTransition<INScanTransfer>>) ((ScanStateFlow<INScanTransfer>.INextTo) ((ScanStateFlow<INScanTransfer>.INextTo) ((ScanStateFlow<INScanTransfer>.INextTo) ((ScanStateFlow<INScanTransfer>.INextTo) ((ScanStateFlow<INScanTransfer>.INextTo) flow.From<INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.WarehouseState>().NextTo<CartSupport<INScanTransfer, INScanTransfer.Host>.CartState>((Action<INScanTransfer>) null)).NextTo<INScanTransfer.TransferMode.SourceLocationState>((Action<INScanTransfer>) null)).NextTo<WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.InventoryItemState>((Action<INScanTransfer>) null)).NextTo<WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.LotSerialState>((Action<INScanTransfer>) null)).NextTo<INScanTransfer.TransferMode.TargetLocationState>((Action<INScanTransfer>) null)).NextTo<INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.ReasonCodeState>((Action<INScanTransfer>) null))) : basis.StateFlow((Func<ScanStateFlow<INScanTransfer>.IFrom, IEnumerable<ScanTransition<INScanTransfer>>>) (flow => (IEnumerable<ScanTransition<INScanTransfer>>) ((ScanStateFlow<INScanTransfer>.INextTo) ((ScanStateFlow<INScanTransfer>.INextTo) ((ScanStateFlow<INScanTransfer>.INextTo) ((ScanStateFlow<INScanTransfer>.INextTo) ((ScanStateFlow<INScanTransfer>.INextTo) flow.From<INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.WarehouseState>().NextTo<CartSupport<INScanTransfer, INScanTransfer.Host>.CartState>((Action<INScanTransfer>) null)).NextTo<INScanTransfer.TransferMode.SourceLocationState>((Action<INScanTransfer>) null)).NextTo<INScanTransfer.TransferMode.TargetLocationState>((Action<INScanTransfer>) null)).NextTo<INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.ReasonCodeState>((Action<INScanTransfer>) null)).NextTo<WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.InventoryItemState>((Action<INScanTransfer>) null)).NextTo<WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.LotSerialState>((Action<INScanTransfer>) null)))), new RelativeInject?());
    return scanMode;
  }

  [PXOverride]
  public virtual ScanCommand<INScanTransfer> DecorateScanCommand(
    ScanCommand<INScanTransfer> original,
    Func<ScanCommand<INScanTransfer>, ScanCommand<INScanTransfer>> base_DecorateScanCommand)
  {
    ScanCommand<INScanTransfer> scanCommand = base_DecorateScanCommand(original);
    if (!this.IsCartRequired())
      return scanCommand;
    if (scanCommand is INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.ReleaseCommand releaseCommand)
      ((MethodInterceptor<ScanCommand<INScanTransfer>, INScanTransfer>.OfPredicate) ((ScanCommand<INScanTransfer>) releaseCommand).Intercept.IsEnabled).ByConjoin((Func<INScanTransfer, bool>) (basis => basis.Get<INScanTransferCartSupport>().IsCartEmpty), false, new RelativeInject?());
    if (scanCommand is CartSupport<INScanTransfer, INScanTransfer.Host>.CartIn cartCommand1)
      ResetLocationsOn((ScanCommand<INScanTransfer>) cartCommand1);
    if (scanCommand is CartSupport<INScanTransfer, INScanTransfer.Host>.CartOut cartCommand2)
      ResetLocationsOn((ScanCommand<INScanTransfer>) cartCommand2);
    if (scanCommand is WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.RemoveCommand cartCommand3)
      ResetLocationsOn((ScanCommand<INScanTransfer>) cartCommand3);
    return scanCommand;

    static void ResetLocationsOn(ScanCommand<INScanTransfer> cartCommand)
    {
      ((MethodInterceptor<ScanCommand<INScanTransfer>, INScanTransfer>.OfFunc<bool>) cartCommand.Intercept.Process).ByOverride((Func<INScanTransfer, Func<bool>, bool>) ((basis, base_Process) =>
      {
        basis.LocationID = new int?();
        basis.TransferToLocationID = new int?();
        return base_Process();
      }), new RelativeInject?());
    }
  }

  [PXOverride]
  public virtual ScanState<INScanTransfer> DecorateScanState(
    ScanState<INScanTransfer> original,
    Func<ScanState<INScanTransfer>, ScanState<INScanTransfer>> base_DecorateScanState)
  {
    ScanState<INScanTransfer> scanState = base_DecorateScanState(original);
    if (!this.IsCartRequired())
      return scanState;
    if (scanState is CartSupport<INScanTransfer, INScanTransfer.Host>.CartState cartState)
      ((MethodInterceptor<EntityState<INScanTransfer, INCart>, INScanTransfer>.OfAction<INCart>) ((EntityState<INScanTransfer, INCart>) ((MethodInterceptor<EntityState<INScanTransfer, INCart>, INScanTransfer>.OfFunc<INCart, Validation>.AsAppendable) cartState.Intercept.Validate).ByAppend((Func<Validation, INCart, Validation>) ((basis, cart) =>
      {
        INRegisterCart[] cartRegisters = basis.Get<INScanTransferCartSupport>().GetCartRegisters(cart);
        if (cartRegisters.Length <= 1 && (cartRegisters.Length != 1 || !(cartRegisters[0].DocType != "T")))
          return Validation.Ok;
        return Validation.Fail("The {0} cart is already in use.", new object[1]
        {
          (object) cart.CartCD
        });
      }), new RelativeInject?())).Intercept.Apply).ByAppend((Action<INScanTransfer, INCart>) ((basis, cart) =>
      {
        INScanTransferCartSupport transferCartSupport = basis.Get<INScanTransferCartSupport>();
        INRegisterCart[] cartRegisters = transferCartSupport.GetCartRegisters(cart);
        if (cartRegisters.Length != 1)
          return;
        ((PXSelectBase<INRegisterCart>) transferCartSupport.CartsLinks).Current = cartRegisters[0];
        basis.RefNbr = cartRegisters[0].RefNbr;
        basis.TransferToSiteID = (int?) basis.Document?.ToSiteID;
      }), new RelativeInject?());
    if (scanState is WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.InventoryItemState inventoryItemState)
      ((MethodInterceptor<EntityState<INScanTransfer, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, INScanTransfer>.OfFunc<string, AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>>) ((EntityState<INScanTransfer, PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>) inventoryItemState).Intercept.HandleAbsence).ByOverride((Func<AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>, string, Func<string, AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>>, AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>>) ((basis, barcode, base_HandleAbsence) =>
      {
        if (!basis.Get<INScanTransferCartSupport>().CartLoaded.GetValueOrDefault())
          return base_HandleAbsence(barcode);
        return basis.TryProcessBy<INScanTransfer.TransferMode.TargetLocationState>(barcode, (StateSubstitutionRule) 18) ? AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>.op_Implicit(AbsenceHandling.Done) : AbsenceHandling.Of<PXResult<INItemXRef, PX.Objects.IN.InventoryItem>>.op_Implicit(AbsenceHandling.Skipped);
      }), new RelativeInject?());
    if (scanState is INScanTransfer.TransferMode.SourceLocationState sourceLocationState)
      ((MethodInterceptor<EntityState<INScanTransfer, INLocation[]>, INScanTransfer>.OfPredicate) ((EntityState<INScanTransfer, INLocation[]>) sourceLocationState).Intercept.IsStateActive).ByConjoin((Func<INScanTransfer, bool>) (basis =>
      {
        bool? cartLoaded = basis.Get<INScanTransferCartSupport>().CartLoaded;
        bool flag = false;
        return cartLoaded.GetValueOrDefault() == flag & cartLoaded.HasValue;
      }), false, new RelativeInject?());
    if (scanState is INScanTransfer.TransferMode.TargetLocationState targetLocationState)
      ((MethodInterceptor<EntityState<INScanTransfer, INLocation[]>, INScanTransfer>.OfPredicate) ((EntityState<INScanTransfer, INLocation[]>) targetLocationState).Intercept.IsStateActive).ByConjoin((Func<INScanTransfer, bool>) (basis => basis.Get<INScanTransferCartSupport>().CartLoaded.GetValueOrDefault()), false, new RelativeInject?());
    if (scanState is INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>.ReasonCodeState reasonCodeState)
      ((MethodInterceptor<EntityState<INScanTransfer, PX.Objects.CS.ReasonCode>, INScanTransfer>.OfPredicate) ((EntityState<INScanTransfer, PX.Objects.CS.ReasonCode>) reasonCodeState).Intercept.IsStateActive).ByConjoin((Func<INScanTransfer, bool>) (basis => basis.Get<INScanTransferCartSupport>().CartLoaded.GetValueOrDefault()), false, new RelativeInject?());
    return scanState;
  }

  /// Overrides <see cref="T:PX.Objects.IN.WMS.INScanTransfer.TransferMode.ConfirmState.Logic" />
  [PXProtectedAccess(typeof (INScanTransfer.TransferMode.ConfirmState.Logic))]
  public abstract class AlterConfirmStateLogic : 
    BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>.ScanExtension<INScanTransfer.TransferMode.ConfirmState.Logic>
  {
    public static bool IsActive() => INScanTransferCartSupport.IsActive();

    public INScanTransferCartSupport CartBasis
    {
      get
      {
        return ((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) this.Basis).Get<INScanTransferCartSupport>();
      }
    }

    /// Uses <see cref="M:PX.Objects.IN.WMS.INScanTransfer.TransferMode.ConfirmState.Logic.EnsureDocument" />
    [PXProtectedAccess(null)]
    protected abstract PX.Objects.IN.INRegister EnsureDocument();

    /// Uses <see cref="M:PX.Objects.IN.WMS.INScanTransfer.TransferMode.ConfirmState.Logic.HasErrors(PX.Objects.IN.INTran,PX.BarcodeProcessing.FlowStatus@)" />
    [PXProtectedAccess(null)]
    protected abstract bool HasErrors(INTran line, out FlowStatus error);

    /// Overrides <see cref="M:PX.Objects.IN.WMS.INScanTransfer.TransferMode.ConfirmState.Logic.Confirm" />
    [PXOverride]
    public virtual FlowStatus Confirm(Func<FlowStatus> base_Confirm)
    {
      return this.CartBasis.IsCartRequired() ? this.ConfirmCartProcess() : base_Confirm();
    }

    protected virtual FlowStatus ConfirmCartProcess()
    {
      FlowStatus error;
      if (!this.CanConfirmCartProcess(out error))
        return error;
      bool? remove = ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).Remove;
      bool flag1 = false;
      if (remove.GetValueOrDefault() == flag1 & remove.HasValue)
      {
        bool? cartLoaded = this.CartBasis.CartLoaded;
        bool flag2 = false;
        return cartLoaded.GetValueOrDefault() == flag2 & cartLoaded.HasValue ? this.MoveToCart() : this.MoveFromCart();
      }
      bool? cartLoaded1 = this.CartBasis.CartLoaded;
      bool flag3 = false;
      return cartLoaded1.GetValueOrDefault() == flag3 & cartLoaded1.HasValue ? this.RemoveFromCart() : this.ReturnToCart();
    }

    protected virtual bool CanConfirmCartProcess(out FlowStatus error)
    {
      if (((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) this.Basis).HasActive<WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.LotSerialState>() && ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).LotSerialNbr == null)
      {
        error = FlowStatus.Fail("Lot or serial number not selected.", Array.Empty<object>());
        return false;
      }
      if (((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) this.Basis).HasActive<INScanTransfer.TransferMode.TargetLocationState>() && this.CartBasis.CartLoaded.GetValueOrDefault() && !((INScanTransfer) this.Basis).TransferToLocationID.HasValue)
      {
        error = FlowStatus.Fail("Destination location not selected.", Array.Empty<object>());
        return false;
      }
      if (((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).LotSerialTrack.IsTrackedSerial)
      {
        Decimal? qty = ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).Qty;
        Decimal num = (Decimal) 1;
        if (!(qty.GetValueOrDefault() == num & qty.HasValue))
        {
          error = FlowStatus.Fail("Serialized items can be processed only with the base UOM and the 1.00 quantity.", Array.Empty<object>());
          return false;
        }
      }
      error = FlowStatus.Ok;
      return true;
    }

    protected virtual FlowStatus MoveToCart()
    {
      Decimal? qty1 = ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).Qty;
      Decimal? qty2 = qty1.HasValue ? new Decimal?(qty1.GetValueOrDefault()) : new Decimal?();
      this.EnsureDocument();
      INTran fixedLineFromCart = this.FindFixedLineFromCart();
      FlowStatus flowStatus;
      if (!this.SyncWithLines(qty2, ref fixedLineFromCart, out flowStatus))
        return flowStatus;
      INCartSplit cartSplit = this.SyncWithCart(qty2);
      this.SyncWithDocumentCart(fixedLineFromCart, cartSplit, qty2);
      ((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) this.Basis).ReportInfo("{0} x {1} {2} added to cart.", new object[3]
      {
        (object) ((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) this.Basis).SightOf<WMSScanHeader.inventoryID>(),
        (object) ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).Qty,
        (object) ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).UOM
      });
      return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
    }

    protected virtual FlowStatus RemoveFromCart()
    {
      Decimal? qty1 = ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).Qty;
      Decimal? qty2 = qty1.HasValue ? new Decimal?(-qty1.GetValueOrDefault()) : new Decimal?();
      this.EnsureDocument();
      INTran fixedLineFromCart = this.FindFixedLineFromCart();
      FlowStatus flowStatus;
      if (!this.SyncWithLines(qty2, ref fixedLineFromCart, out flowStatus))
        return flowStatus;
      INCartSplit cartSplit = this.SyncWithCart(qty2);
      this.SyncWithDocumentCart(fixedLineFromCart, cartSplit, qty2);
      ((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) this.Basis).ReportInfo("{0} x {1} {2} removed from cart.", new object[3]
      {
        (object) ((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) this.Basis).SightOf<WMSScanHeader.inventoryID>(),
        (object) ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).Qty,
        (object) ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).UOM
      });
      return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
    }

    protected virtual FlowStatus MoveFromCart()
    {
      Decimal? qty1 = ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).Qty;
      INTran anyLineFromCart = this.FindAnyLineFromCart();
      if (anyLineFromCart == null)
        return this.LineMissingStatus();
      ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).LocationID = anyLineFromCart.LocationID;
      Decimal? nullable = qty1;
      FlowStatus flowStatus1;
      if (!this.SyncWithLines(nullable.HasValue ? new Decimal?(-nullable.GetValueOrDefault()) : new Decimal?(), ref anyLineFromCart, out flowStatus1))
        return flowStatus1;
      nullable = qty1;
      INCartSplit inCartSplit = this.SyncWithCart(nullable.HasValue ? new Decimal?(-nullable.GetValueOrDefault()) : new Decimal?());
      INTran line = anyLineFromCart;
      INCartSplit cartSplit = inCartSplit;
      nullable = qty1;
      Decimal? qty2 = nullable.HasValue ? new Decimal?(-nullable.GetValueOrDefault()) : new Decimal?();
      this.SyncWithDocumentCart(line, cartSplit, qty2);
      ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).SiteID = anyLineFromCart.SiteID;
      ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).LotSerialNbr = anyLineFromCart.LotSerialNbr;
      ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).ReasonCodeID = anyLineFromCart.ReasonCode;
      ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).ExpireDate = anyLineFromCart.ExpireDate;
      ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).SubItemID = anyLineFromCart.SubItemID;
      INTran lineFromDocument = this.FindLineFromDocument();
      FlowStatus flowStatus2;
      if (!this.SyncWithLines(qty1, ref lineFromDocument, out flowStatus2))
        return flowStatus2;
      if (this.CartBasis.IsCartEmpty)
      {
        this.CartBasis.CartLoaded = new bool?(false);
        ((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) this.Basis).ReportInfo("The {0} cart is empty.", new object[1]
        {
          (object) ((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) this.Basis).SightOf<CartScanHeader.cartID>()
        });
      }
      else
        ((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) this.Basis).ReportInfo("{0} x {1} {2} added to transfer.", new object[3]
        {
          (object) ((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) this.Basis).SightOf<WMSScanHeader.inventoryID>(),
          (object) ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).Qty,
          (object) ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).UOM
        });
      return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
    }

    protected virtual FlowStatus ReturnToCart()
    {
      Decimal? qty = ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).Qty;
      INTran lineFromDocument = this.FindLineFromDocument();
      if (lineFromDocument == null)
        return this.LineMissingStatus();
      Decimal? nullable = qty;
      FlowStatus flowStatus1;
      if (!this.SyncWithLines(nullable.HasValue ? new Decimal?(-nullable.GetValueOrDefault()) : new Decimal?(), ref lineFromDocument, out flowStatus1))
        return flowStatus1;
      ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).LocationID = lineFromDocument.LocationID;
      ((INScanTransfer) this.Basis).TransferToLocationID = lineFromDocument.LocationID;
      ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).SiteID = lineFromDocument.SiteID;
      ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).LotSerialNbr = lineFromDocument.LotSerialNbr;
      ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).ReasonCodeID = lineFromDocument.ReasonCode;
      ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).ExpireDate = lineFromDocument.ExpireDate;
      ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).SubItemID = lineFromDocument.SubItemID;
      INTran fixedLineFromCart = this.FindFixedLineFromCart();
      FlowStatus flowStatus2;
      if (!this.SyncWithLines(qty, ref fixedLineFromCart, out flowStatus2))
        return flowStatus2;
      INCartSplit cartSplit = this.SyncWithCart(qty);
      this.SyncWithDocumentCart(fixedLineFromCart, cartSplit, qty);
      ((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) this.Basis).ReportInfo("{0} x {1} {2} removed from transfer.", new object[3]
      {
        (object) ((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) this.Basis).SightOf<WMSScanHeader.inventoryID>(),
        (object) ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).Qty,
        (object) ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).UOM
      });
      return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
    }

    protected virtual INTran FindAnyLineFromCart()
    {
      return this.FindLine((Func<INTran, bool>) (line => this.CartBasis.GetCartQty(line).HasValue));
    }

    protected virtual INTran FindFixedLineFromCart()
    {
      return this.FindLine((Func<INTran, bool>) (line =>
      {
        bool? cartLoaded = this.CartBasis.CartLoaded;
        bool flag = false;
        int num;
        if (!(cartLoaded.GetValueOrDefault() == flag & cartLoaded.HasValue))
        {
          int? toLocationId = line.ToLocationID;
          int? transferToLocationId = ((INScanTransfer) this.Basis).TransferToLocationID;
          num = toLocationId.GetValueOrDefault() == transferToLocationId.GetValueOrDefault() & toLocationId.HasValue == transferToLocationId.HasValue ? 1 : 0;
        }
        else
        {
          int? locationId1 = line.LocationID;
          int? locationId2 = ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).LocationID;
          num = locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue ? 1 : 0;
        }
        return num != 0 && this.CartBasis.GetCartQty(line).HasValue;
      }));
    }

    protected virtual INTran FindLineFromDocument()
    {
      return this.FindLine((Func<INTran, bool>) (line =>
      {
        bool? cartLoaded = this.CartBasis.CartLoaded;
        bool flag = false;
        int num;
        if (!(cartLoaded.GetValueOrDefault() == flag & cartLoaded.HasValue))
        {
          int? toLocationId = line.ToLocationID;
          int? transferToLocationId = ((INScanTransfer) this.Basis).TransferToLocationID;
          num = toLocationId.GetValueOrDefault() == transferToLocationId.GetValueOrDefault() & toLocationId.HasValue == transferToLocationId.HasValue ? 1 : 0;
        }
        else
        {
          int? locationId1 = line.LocationID;
          int? locationId2 = ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).LocationID;
          num = locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue ? 1 : 0;
        }
        return num != 0 && !this.CartBasis.GetCartQty(line).HasValue;
      }));
    }

    protected virtual INTran FindLine(Func<INTran, bool> search)
    {
      IEnumerable<INTran> source = ((IEnumerable<INTran>) ((PXSelectBase<INTran>) ((PXGraphExtension<INScanTransfer.Host>) this).Base.transactions).SelectMain(Array.Empty<object>())).Where<INTran>((Func<INTran, bool>) (t =>
      {
        int? inventoryId1 = t.InventoryID;
        int? inventoryId2 = ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).InventoryID;
        if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
        {
          int? siteId1 = t.SiteID;
          int? siteId2 = ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).SiteID;
          if (siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue)
          {
            int? toSiteId = t.ToSiteID;
            int? transferToSiteId = ((INScanTransfer) this.Basis).TransferToSiteID;
            if (toSiteId.GetValueOrDefault() == transferToSiteId.GetValueOrDefault() & toSiteId.HasValue == transferToSiteId.HasValue && t.ReasonCode == (((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).ReasonCodeID ?? t.ReasonCode) && t.UOM == ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).UOM)
              return search == null || search(t);
          }
        }
        return false;
      }));
      INTran line = (INTran) null;
      if (((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) this.Basis).HasActive<WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>.LotSerialState>())
      {
        foreach (INTran inTran in source)
        {
          ((PXSelectBase<INTran>) ((PXGraphExtension<INScanTransfer.Host>) this).Base.transactions).Current = inTran;
          if (((IEnumerable<INTranSplit>) ((PXSelectBase<INTranSplit>) ((PXGraphExtension<INScanTransfer.Host>) this).Base.splits).SelectMain(Array.Empty<object>())).Any<INTranSplit>((Func<INTranSplit, bool>) (t => string.Equals(t.LotSerialNbr ?? "", ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).LotSerialNbr ?? "", StringComparison.OrdinalIgnoreCase))))
          {
            line = inTran;
            break;
          }
        }
      }
      else
        line = source.FirstOrDefault<INTran>();
      return line;
    }

    protected virtual FlowStatus LineMissingStatus()
    {
      return FlowStatus.Fail("{0} item not found in transfer.", new object[1]
      {
        (object) ((BarcodeDrivenStateMachine<INScanTransfer, INScanTransfer.Host>) this.Basis).SightOf<WMSScanHeader.inventoryID>()
      });
    }

    protected virtual bool SyncWithLines(Decimal? qty, ref INTran line, out FlowStatus flowStatus)
    {
      INTran existLine = line;
      Action action;
      if (existLine != null)
      {
        Decimal? qty1 = existLine.Qty;
        Decimal? nullable1 = qty;
        Decimal? nullable2 = qty1.HasValue & nullable1.HasValue ? new Decimal?(qty1.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable3 = nullable2;
        Decimal num1 = 0M;
        if (nullable3.GetValueOrDefault() == num1 & nullable3.HasValue)
        {
          INTran backup = PXCache<INTran>.CreateCopy(existLine);
          ((PXSelectBase) ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).Details).Cache.SetValueExt<INTran.qty>((object) existLine, (object) nullable2);
          existLine = ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).Details.Delete(existLine);
          action = (Action) (() => ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).Details.Insert(backup));
        }
        else
        {
          if (((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).LotSerialTrack.IsTrackedSerial)
          {
            Decimal? nullable4 = nullable2;
            Decimal num2 = (Decimal) 1;
            if (!(nullable4.GetValueOrDefault() == num2 & nullable4.HasValue))
            {
              flowStatus = FlowStatus.Fail("Serialized items can be processed only with the base UOM and the 1.00 quantity.", Array.Empty<object>());
              return false;
            }
          }
          INTran backup = PXCache<INTran>.CreateCopy(existLine);
          ((PXSelectBase) ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).Details).Cache.SetValueExt<INTran.qty>((object) existLine, (object) nullable2);
          ((PXSelectBase) ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).Details).Cache.SetValueExt<INTran.lotSerialNbr>((object) existLine, (object) null);
          existLine = ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).Details.Update(existLine);
          ((PXSelectBase) ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).Details).Cache.SetValueExt<INTran.lotSerialNbr>((object) existLine, (object) ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).LotSerialNbr);
          existLine = ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).Details.Update(existLine);
          action = (Action) (() =>
          {
            ((PXSelectBase) ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).Details).Cache.Delete((object) existLine);
            ((PXSelectBase) ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).Details).Cache.Insert((object) backup);
          });
        }
      }
      else
      {
        Decimal? nullable = qty;
        Decimal num = 0M;
        if (nullable.GetValueOrDefault() < num & nullable.HasValue)
        {
          flowStatus = this.LineMissingStatus();
          return false;
        }
        existLine = ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).Details.Insert();
        ValueSetter<INTran> withEventFiring = PXCacheEx.GetSetterForCurrent<INTran>(((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).Details).WithEventFiring;
        // ISSUE: explicit reference operation
        (^ref withEventFiring).Set<int?>((Expression<Func<INTran, int?>>) (tr => tr.InventoryID), ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).InventoryID);
        // ISSUE: explicit reference operation
        (^ref withEventFiring).Set<int?>((Expression<Func<INTran, int?>>) (tr => tr.SiteID), ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).SiteID);
        // ISSUE: explicit reference operation
        (^ref withEventFiring).Set<int?>((Expression<Func<INTran, int?>>) (tr => tr.ToSiteID), ((INScanTransfer) this.Basis).TransferToSiteID);
        // ISSUE: explicit reference operation
        (^ref withEventFiring).Set<int?>((Expression<Func<INTran, int?>>) (tr => tr.LocationID), ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).LocationID);
        bool? cartLoaded = this.CartBasis.CartLoaded;
        bool flag = false;
        if (cartLoaded.GetValueOrDefault() == flag & cartLoaded.HasValue)
        {
          // ISSUE: explicit reference operation
          (^ref withEventFiring).Set<int?>((Expression<Func<INTran, int?>>) (tr => tr.ToLocationID), ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).LocationID);
        }
        else
        {
          // ISSUE: explicit reference operation
          (^ref withEventFiring).Set<int?>((Expression<Func<INTran, int?>>) (tr => tr.ToLocationID), ((INScanTransfer) this.Basis).TransferToLocationID);
        }
        // ISSUE: explicit reference operation
        (^ref withEventFiring).Set<string>((Expression<Func<INTran, string>>) (tr => tr.UOM), ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).UOM);
        // ISSUE: explicit reference operation
        (^ref withEventFiring).Set<string>((Expression<Func<INTran, string>>) (tr => tr.ReasonCode), ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).ReasonCodeID);
        existLine = ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).Details.Update(existLine);
        ((PXSelectBase) ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).Details).Cache.SetValueExt<INTran.qty>((object) existLine, (object) qty);
        existLine = ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).Details.Update(existLine);
        ((PXSelectBase) ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).Details).Cache.SetValueExt<INTran.lotSerialNbr>((object) existLine, (object) ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).LotSerialNbr);
        existLine = ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).Details.Update(existLine);
        action = (Action) (() => ((PXSelectBase) ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).Details).Cache.Delete((object) existLine));
      }
      bool? remove = ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).Remove;
      bool flag1 = false;
      if (remove.GetValueOrDefault() == flag1 & remove.HasValue && this.HasErrors(existLine, out flowStatus))
      {
        if (action != null)
          action();
        return false;
      }
      flowStatus = FlowStatus.Ok;
      line = existLine;
      return true;
    }

    protected virtual INCartSplit SyncWithCart(Decimal? qty)
    {
      INCartSplit inCartSplit1 = PXResultset<INCartSplit>.op_Implicit(((PXSelectBase<INCartSplit>) this.CartBasis.CartSplits).Search<INCartSplit.inventoryID, INCartSplit.subItemID, INCartSplit.fromLocationID, INCartSplit.lotSerialNbr>((object) ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).InventoryID, (object) ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).SubItemID, (object) ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).LocationID, string.IsNullOrEmpty(((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).LotSerialNbr) ? (object) (string) null : (object) ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).LotSerialNbr, Array.Empty<object>()));
      INCartSplit inCartSplit2;
      if (inCartSplit1 == null)
      {
        inCartSplit2 = ((PXSelectBase<INCartSplit>) this.CartBasis.CartSplits).Insert(new INCartSplit()
        {
          CartID = this.CartBasis.CartID,
          InventoryID = ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).InventoryID,
          SubItemID = ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).SubItemID,
          LotSerialNbr = ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).LotSerialNbr,
          ExpireDate = ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).ExpireDate,
          UOM = ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).UOM,
          SiteID = ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).SiteID,
          FromLocationID = ((WarehouseManagementSystem<INScanTransfer, INScanTransfer.Host>) this.Basis).LocationID,
          Qty = qty
        });
      }
      else
      {
        INCartSplit copy = (INCartSplit) ((PXSelectBase) this.CartBasis.CartSplits).Cache.CreateCopy((object) inCartSplit1);
        INCartSplit inCartSplit3 = copy;
        Decimal? qty1 = inCartSplit3.Qty;
        Decimal? nullable = qty;
        inCartSplit3.Qty = qty1.HasValue & nullable.HasValue ? new Decimal?(qty1.GetValueOrDefault() + nullable.GetValueOrDefault()) : new Decimal?();
        inCartSplit2 = ((PXSelectBase<INCartSplit>) this.CartBasis.CartSplits).Update(copy);
        nullable = inCartSplit2.Qty;
        Decimal num = 0M;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
          inCartSplit2 = ((PXSelectBase<INCartSplit>) this.CartBasis.CartSplits).Delete(inCartSplit2);
      }
      return inCartSplit2;
    }

    protected virtual void SyncWithDocumentCart(INTran line, INCartSplit cartSplit, Decimal? qty)
    {
      if (((PXSelectBase<INRegisterCart>) this.CartBasis.CartsLinks).Current == null)
        ((PXSelectBase<INRegisterCart>) this.CartBasis.CartsLinks).Insert();
      ((PXSelectBase) this.CartBasis.CartsLinks).Cache.SetValue<INRegisterCart.docType>((object) ((PXSelectBase<INRegisterCart>) this.CartBasis.CartsLinks).Current, (object) ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).Document.DocType);
      ((PXSelectBase) this.CartBasis.CartsLinks).Cache.SetValue<INRegisterCart.refNbr>((object) ((PXSelectBase<INRegisterCart>) this.CartBasis.CartsLinks).Current, (object) ((INScanRegisterBase<INScanTransfer, INScanTransfer.Host, INDocType.transfer>) this.Basis).Document.RefNbr);
      this.SyncWithDocumentCartLine(line, cartSplit, qty);
      if (!this.CartBasis.IsCartEmpty)
        return;
      ((PXSelectBase<INRegisterCart>) this.CartBasis.CartsLinks).DeleteCurrent();
    }

    protected virtual void SyncWithDocumentCartLine(
      INTran line,
      INCartSplit cartSplit,
      Decimal? qty)
    {
      int num1 = line.Qty.GetValueOrDefault() == 0M ? 1 : 0;
      INRegisterCartLine registerCartLine1 = PXResultset<INRegisterCartLine>.op_Implicit(((PXSelectBase<INRegisterCartLine>) this.CartBasis.CartSplitLinks).Search<INRegisterCartLine.lineNbr>((object) line.LineNbr, Array.Empty<object>()));
      Decimal? nullable1;
      if (registerCartLine1 == null)
      {
        nullable1 = qty;
        Decimal num2 = 0M;
        if (nullable1.GetValueOrDefault() <= num2 & nullable1.HasValue)
          throw new PXArgumentException(nameof (qty));
        registerCartLine1 = ((PXSelectBase<INRegisterCartLine>) this.CartBasis.CartSplitLinks).Insert();
        ((PXSelectBase) this.CartBasis.CartSplitLinks).Cache.SetValue<INRegisterCartLine.cartSplitLineNbr>((object) registerCartLine1, (object) cartSplit.SplitLineNbr);
      }
      INRegisterCartLine copy = (INRegisterCartLine) ((PXSelectBase) this.CartBasis.CartSplitLinks).Cache.CreateCopy((object) registerCartLine1);
      INRegisterCartLine registerCartLine2 = copy;
      nullable1 = registerCartLine2.Qty;
      Decimal? nullable2 = qty;
      registerCartLine2.Qty = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      ((PXSelectBase) this.CartBasis.CartSplitLinks).Cache.Update((object) copy);
      nullable2 = copy.Qty;
      Decimal num3 = 0M;
      if (!(nullable2.GetValueOrDefault() == num3 & nullable2.HasValue))
        return;
      ((PXSelectBase<INRegisterCartLine>) this.CartBasis.CartSplitLinks).Delete(copy);
    }
  }

  [PXUIField(DisplayName = "Cart Qty.")]
  public class CartQty : 
    PXFieldAttachedTo<INTran>.By<INScanTransfer.Host>.AsDecimal.Named<INScanTransferCartSupport.CartQty>
  {
    public override Decimal? GetValue(INTran row)
    {
      return ((PXGraph) this.Base).FindImplementation<INScanTransferCartSupport>()?.GetCartQty(row);
    }

    protected override bool? Visible
    {
      get
      {
        int num;
        if (INScanTransferCartSupport.IsActive())
        {
          INScanTransferCartSupport implementation = ((PXGraph) this.Base).FindImplementation<INScanTransferCartSupport>();
          num = implementation != null ? (implementation.IsCartRequired() ? 1 : 0) : 0;
        }
        else
          num = 0;
        return new bool?(num != 0);
      }
    }
  }
}
