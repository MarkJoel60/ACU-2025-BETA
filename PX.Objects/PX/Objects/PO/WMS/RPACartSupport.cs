// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.WMS.RPACartSupport
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

#nullable enable
namespace PX.Objects.PO.WMS;

public class RPACartSupport : CartSupport<
#nullable disable
ReceivePutAway, ReceivePutAway.Host>
{
  public FbqlSelect<SelectFromBase<POCartReceipt, TypeArrayOf<IFbqlJoin>.Empty>, POCartReceipt>.View CartsLinks;
  public FbqlSelect<SelectFromBase<INCartSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCart>.On<INCartSplit.FK.Cart>>, FbqlJoins.Inner<POCartReceipt>.On<POCartReceipt.FK.Cart>>>.Where<KeysRelation<CompositeKey<Field<POCartReceipt.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<POCartReceipt.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, POCartReceipt>, PX.Objects.PO.POReceipt, POCartReceipt>.SameAsCurrent>, INCartSplit>.View AllCartSplits;
  public FbqlSelect<SelectFromBase<POReceiptSplitToCartSplitLink, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCartSplit>.On<POReceiptSplitToCartSplitLink.FK.CartSplit>>>.Where<KeysRelation<CompositeKey<Field<POReceiptSplitToCartSplitLink.siteID>.IsRelatedTo<INCart.siteID>, Field<POReceiptSplitToCartSplitLink.cartID>.IsRelatedTo<INCart.cartID>>.WithTablesOf<INCart, POReceiptSplitToCartSplitLink>, INCart, POReceiptSplitToCartSplitLink>.SameAsCurrent>, POReceiptSplitToCartSplitLink>.View CartSplitLinks;
  public FbqlSelect<SelectFromBase<POReceiptSplitToCartSplitLink, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCartSplit>.On<POReceiptSplitToCartSplitLink.FK.CartSplit>>, FbqlJoins.Inner<INCart>.On<INCartSplit.FK.Cart>>, FbqlJoins.Inner<POCartReceipt>.On<POCartReceipt.FK.Cart>>>.Where<KeysRelation<CompositeKey<Field<POCartReceipt.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<POCartReceipt.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, POCartReceipt>, PX.Objects.PO.POReceipt, POCartReceipt>.SameAsCurrent>, POReceiptSplitToCartSplitLink>.View AllCartSplitLinks;

  public static bool IsActive() => CartSupport<ReceivePutAway, ReceivePutAway.Host>.IsActiveBase();

  public override bool IsCartRequired()
  {
    return ((PXSelectBase<POReceivePutAwaySetup>) this.Basis.Setup).Current.UseCartsForPutAway.GetValueOrDefault() && this.Basis.Header.Mode == "PTAW";
  }

  public void EnsureCartReceiptLink()
  {
    if (!this.CartID.HasValue || !this.Basis.SiteID.HasValue || this.Basis.RefNbr == null)
      return;
    POCartReceipt poCartReceipt = new POCartReceipt()
    {
      SiteID = this.Basis.SiteID,
      CartID = this.CartID,
      ReceiptType = this.Basis.ReceiptType,
      ReceiptNbr = this.Basis.RefNbr,
      TransferNbr = this.Basis.TransferRefNbr
    };
    if (!((IEnumerable<INCartSplit>) ((PXSelectBase<INCartSplit>) this.CartSplits).SelectMain(Array.Empty<object>())).Any<INCartSplit>())
    {
      PX.Objects.IN.INRegister transfer = this.Basis.Get<ReceivePutAway.PutAwayMode.Logic>().GetTransfer();
      if ((transfer != null ? (!transfer.Released.GetValueOrDefault() ? 1 : 0) : 1) == 0)
      {
        ((PXSelectBase<POCartReceipt>) this.CartsLinks).Delete(poCartReceipt);
        return;
      }
    }
    ((PXSelectBase<POCartReceipt>) this.CartsLinks).Update(poCartReceipt);
  }

  public Decimal GetCartQty(POReceiptLineSplit posplit)
  {
    return this.IsCartRequired() ? ((IEnumerable<POReceiptSplitToCartSplitLink>) ((PXSelectBase<POReceiptSplitToCartSplitLink>) this.CartSplitLinks).SelectMain(Array.Empty<object>())).Where<POReceiptSplitToCartSplitLink>((Func<POReceiptSplitToCartSplitLink, bool>) (link => KeysRelation<CompositeKey<Field<POReceiptSplitToCartSplitLink.receiptType>.IsRelatedTo<POReceiptLineSplit.receiptType>, Field<POReceiptSplitToCartSplitLink.receiptNbr>.IsRelatedTo<POReceiptLineSplit.receiptNbr>, Field<POReceiptSplitToCartSplitLink.receiptLineNbr>.IsRelatedTo<POReceiptLineSplit.lineNbr>, Field<POReceiptSplitToCartSplitLink.receiptSplitLineNbr>.IsRelatedTo<POReceiptLineSplit.splitLineNbr>>.WithTablesOf<POReceiptLineSplit, POReceiptSplitToCartSplitLink>, POReceiptLineSplit, POReceiptSplitToCartSplitLink>.Match((PXGraph) ((PXGraphExtension<ReceivePutAway.Host>) this).Base, posplit, link))).Sum<POReceiptSplitToCartSplitLink>((Func<POReceiptSplitToCartSplitLink, Decimal>) (_ => _.Qty.GetValueOrDefault())) : 0M;
  }

  public Decimal GetOverallCartQty(POReceiptLineSplit posplit)
  {
    return this.IsCartRequired() ? ((IEnumerable<POReceiptSplitToCartSplitLink>) ((PXSelectBase<POReceiptSplitToCartSplitLink>) this.AllCartSplitLinks).SelectMain(Array.Empty<object>())).Where<POReceiptSplitToCartSplitLink>((Func<POReceiptSplitToCartSplitLink, bool>) (link => KeysRelation<CompositeKey<Field<POReceiptSplitToCartSplitLink.receiptType>.IsRelatedTo<POReceiptLineSplit.receiptType>, Field<POReceiptSplitToCartSplitLink.receiptNbr>.IsRelatedTo<POReceiptLineSplit.receiptNbr>, Field<POReceiptSplitToCartSplitLink.receiptLineNbr>.IsRelatedTo<POReceiptLineSplit.lineNbr>, Field<POReceiptSplitToCartSplitLink.receiptSplitLineNbr>.IsRelatedTo<POReceiptLineSplit.splitLineNbr>>.WithTablesOf<POReceiptLineSplit, POReceiptSplitToCartSplitLink>, POReceiptLineSplit, POReceiptSplitToCartSplitLink>.Match((PXGraph) ((PXGraphExtension<ReceivePutAway.Host>) this).Base, posplit, link))).Sum<POReceiptSplitToCartSplitLink>((Func<POReceiptSplitToCartSplitLink, Decimal>) (_ => _.Qty.GetValueOrDefault())) : 0M;
  }

  /// Overrides <see cref="T:PX.Objects.PO.WMS.ReceivePutAway.PutAwayMode.Logic" />
  public class AlterPutAwayModeLogic : 
    PXGraphExtension<ReceivePutAway.PutAwayMode.Logic, ReceivePutAway, ReceivePutAway.Host>
  {
    public static bool IsActive() => RPACartSupport.IsActive();

    public ReceivePutAway Basis
    {
      get => ((PXGraphExtension<ReceivePutAway, ReceivePutAway.Host>) this).Base1;
    }

    public ReceivePutAway.PutAwayMode.Logic Mode => this.Base2;

    public RPACartSupport CartBasis => this.Basis.Get<RPACartSupport>();

    /// Overrides <see cref="P:PX.Objects.PO.WMS.ReceivePutAway.PutAwayMode.Logic.CanSwitchReceipt" />
    [PXOverride]
    public virtual bool get_CanSwitchReceipt(Func<bool> base_CanSwitchReceipt)
    {
      return base_CanSwitchReceipt() && !this.CartBasis.IsCartRequired();
    }

    /// Overrides <see cref="P:PX.Objects.PO.WMS.ReceivePutAway.PutAwayMode.Logic.CanPutAway" />
    [PXOverride]
    public virtual bool get_CanPutAway(Func<bool> base_CanPutAway)
    {
      if (this.CartBasis.IsCartRequired())
      {
        bool? cartLoaded = this.CartBasis.CartLoaded;
        bool flag = false;
        if (!(cartLoaded.GetValueOrDefault() == flag & cartLoaded.HasValue))
          return ((IEnumerable<POReceiptLineSplit>) ((PXSelectBase<POReceiptLineSplit>) this.Mode.PutAway).SelectMain(Array.Empty<object>())).Any<POReceiptLineSplit>((Func<POReceiptLineSplit, bool>) (s => this.CartBasis.GetCartQty(s) > 0M));
      }
      return base_CanPutAway();
    }

    /// Overrides <see cref="M:PX.Objects.PO.WMS.ReceivePutAway.PutAwayMode.Logic.GetTransfer" />
    [PXOverride]
    public virtual PX.Objects.IN.INRegister GetTransfer(Func<PX.Objects.IN.INRegister> base_GetTransfer)
    {
      if (!this.CartBasis.IsCartRequired())
        return base_GetTransfer();
      return PXResultset<PX.Objects.IN.INRegister>.op_Implicit(PXSelectBase<PX.Objects.IN.INRegister, PXViewOf<PX.Objects.IN.INRegister>.BasedOn<SelectFromBase<PX.Objects.IN.INRegister, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<POCartReceipt>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.INRegister.docType, Equal<INDocType.transfer>>>>>.And<BqlOperand<POCartReceipt.transferNbr, IBqlString>.IsEqual<PX.Objects.IN.INRegister.refNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.INRegister.transferType, Equal<INTransferType.oneStep>>>>, And<BqlOperand<PX.Objects.IN.INRegister.released, IBqlBool>.IsEqual<False>>>, And<BqlOperand<POCartReceipt.receiptType, IBqlString>.IsEqual<BqlField<RPAScanHeader.receiptType, IBqlString>.FromCurrent>>>, And<BqlOperand<POCartReceipt.receiptNbr, IBqlString>.IsEqual<BqlField<WMSScanHeader.refNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<POCartReceipt.siteID, IBqlInt>.IsEqual<BqlField<WMSScanHeader.siteID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<POCartReceipt.cartID, IBqlInt>.IsEqual<BqlField<CartScanHeader.cartID, IBqlInt>.FromCurrent>>>>.ReadOnly.Config>.SelectSingleBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), (object[]) new ScanHeader[1]
      {
        this.Basis.Header
      }, Array.Empty<object>()));
    }

    /// Overrides <see cref="M:PX.Objects.PO.WMS.ReceivePutAway.PutAwayMode.Logic.OnTransferEntryCreated(PX.Objects.IN.INTransferEntry)" />
    [PXOverride]
    public virtual void OnTransferEntryCreated(
      INTransferEntry transferEntry,
      Action<INTransferEntry> base_OnTransferEntryCreated)
    {
      base_OnTransferEntryCreated(transferEntry);
      if (this.Basis.TransferRefNbr == null)
        return;
      this.CartBasis.EnsureCartReceiptLink();
    }

    [PXOverride]
    public virtual ScanMode<ReceivePutAway> DecorateScanMode(
      ScanMode<ReceivePutAway> original,
      Func<ScanMode<ReceivePutAway>, ScanMode<ReceivePutAway>> base_DecorateScanMode)
    {
      ScanMode<ReceivePutAway> scanMode = base_DecorateScanMode(original);
      if (!(scanMode is ReceivePutAway.PutAwayMode mode))
        return scanMode;
      this.CartBasis.InjectCartState((ScanMode<ReceivePutAway>) mode, true);
      this.CartBasis.InjectCartCommands((ScanMode<ReceivePutAway>) mode);
      ((MethodInterceptor<ScanMode<ReceivePutAway>, ReceivePutAway>.OfAction<bool>) ((ScanMode<ReceivePutAway>) mode).Intercept.ResetMode).ByOverride((Action<ReceivePutAway, bool, Action<bool>>) ((basis, fullReset, base_ResetMode) =>
      {
        RPACartSupport rpaCartSupport = basis.Get<RPACartSupport>();
        ReceivePutAway.PutAwayMode.Logic logic = basis.Get<ReceivePutAway.PutAwayMode.Logic>();
        if (rpaCartSupport.IsCartRequired())
        {
          basis.Clear<CartSupport<ReceivePutAway, ReceivePutAway.Host>.CartState>(fullReset && !basis.IsWithinReset);
          basis.Clear<ReceivePutAway.PutAwayMode.ReceiptState>(fullReset && !basis.IsWithinReset);
          ReceivePutAway receivePutAway1 = basis;
          bool? cartLoaded;
          int num1;
          if (!fullReset)
          {
            cartLoaded = rpaCartSupport.CartLoaded;
            bool flag = false;
            num1 = !(cartLoaded.GetValueOrDefault() == flag & cartLoaded.HasValue) || !logic.PromptLocationForEveryLine ? 0 : (!logic.IsSingleLocation ? 1 : 0);
          }
          else
            num1 = 1;
          receivePutAway1.Clear<ReceivePutAway.PutAwayMode.SourceLocationState>(num1 != 0);
          ReceivePutAway receivePutAway2 = basis;
          int num2;
          if (!fullReset)
          {
            cartLoaded = rpaCartSupport.CartLoaded;
            num2 = !cartLoaded.GetValueOrDefault() ? 0 : (logic.PromptLocationForEveryLine ? 1 : 0);
          }
          else
            num2 = 1;
          receivePutAway2.Clear<ReceivePutAway.PutAwayMode.TargetLocationState>(num2 != 0);
          basis.Clear<ReceivePutAway.PutAwayMode.InventoryItemState>(fullReset);
          basis.Clear<ReceivePutAway.PutAwayMode.LotSerialState>(true);
        }
        else
          base_ResetMode(fullReset);
      }), new RelativeInject?((RelativeInject) 0));
      ((MethodInterceptor<ScanMode<ReceivePutAway>, ReceivePutAway>.OfFunc<IEnumerable<ScanTransition<ReceivePutAway>>>) ((ScanMode<ReceivePutAway>) mode).Intercept.CreateTransitions).ByOverride((Func<ReceivePutAway, Func<IEnumerable<ScanTransition<ReceivePutAway>>>, IEnumerable<ScanTransition<ReceivePutAway>>>) ((basis, base_CreateTransitions) =>
      {
        RPACartSupport rpaCartSupport = basis.Get<RPACartSupport>();
        ReceivePutAway.PutAwayMode.Logic logic = basis.Get<ReceivePutAway.PutAwayMode.Logic>();
        if (!rpaCartSupport.IsCartRequired())
          return base_CreateTransitions();
        bool? cartLoaded = rpaCartSupport.CartLoaded;
        bool flag = false;
        if (cartLoaded.GetValueOrDefault() == flag & cartLoaded.HasValue)
          return basis.StateFlow((Func<ScanStateFlow<ReceivePutAway>.IFrom, IEnumerable<ScanTransition<ReceivePutAway>>>) (flow => (IEnumerable<ScanTransition<ReceivePutAway>>) ((ScanStateFlow<ReceivePutAway>.INextTo) ((ScanStateFlow<ReceivePutAway>.INextTo) ((ScanStateFlow<ReceivePutAway>.INextTo) flow.From<CartSupport<ReceivePutAway, ReceivePutAway.Host>.CartState>().NextTo<ReceivePutAway.PutAwayMode.ReceiptState>((Action<ReceivePutAway>) null)).NextTo<ReceivePutAway.PutAwayMode.SourceLocationState>((Action<ReceivePutAway>) null)).NextTo<ReceivePutAway.PutAwayMode.InventoryItemState>((Action<ReceivePutAway>) null)).NextTo<ReceivePutAway.PutAwayMode.LotSerialState>((Action<ReceivePutAway>) null)));
        return logic.PromptLocationForEveryLine ? basis.StateFlow((Func<ScanStateFlow<ReceivePutAway>.IFrom, IEnumerable<ScanTransition<ReceivePutAway>>>) (flow => (IEnumerable<ScanTransition<ReceivePutAway>>) ((ScanStateFlow<ReceivePutAway>.INextTo) ((ScanStateFlow<ReceivePutAway>.INextTo) ((ScanStateFlow<ReceivePutAway>.INextTo) flow.From<CartSupport<ReceivePutAway, ReceivePutAway.Host>.CartState>().NextTo<ReceivePutAway.PutAwayMode.ReceiptState>((Action<ReceivePutAway>) null)).NextTo<ReceivePutAway.PutAwayMode.InventoryItemState>((Action<ReceivePutAway>) null)).NextTo<ReceivePutAway.PutAwayMode.LotSerialState>((Action<ReceivePutAway>) null)).NextTo<ReceivePutAway.PutAwayMode.TargetLocationState>((Action<ReceivePutAway>) null))) : basis.StateFlow((Func<ScanStateFlow<ReceivePutAway>.IFrom, IEnumerable<ScanTransition<ReceivePutAway>>>) (flow => (IEnumerable<ScanTransition<ReceivePutAway>>) ((ScanStateFlow<ReceivePutAway>.INextTo) ((ScanStateFlow<ReceivePutAway>.INextTo) ((ScanStateFlow<ReceivePutAway>.INextTo) flow.From<CartSupport<ReceivePutAway, ReceivePutAway.Host>.CartState>().NextTo<ReceivePutAway.PutAwayMode.ReceiptState>((Action<ReceivePutAway>) null)).NextTo<ReceivePutAway.PutAwayMode.TargetLocationState>((Action<ReceivePutAway>) null)).NextTo<ReceivePutAway.PutAwayMode.InventoryItemState>((Action<ReceivePutAway>) null)).NextTo<ReceivePutAway.PutAwayMode.LotSerialState>((Action<ReceivePutAway>) null)));
      }), new RelativeInject?());
      return scanMode;
    }

    /// Overrides <see cref="T:PX.Objects.PO.WMS.ReceivePutAway.PutAwayMode.ReceiptState.Logic" />
    public class AlterRefNbrStateLogic : 
      PXGraphExtension<ReceivePutAway.PutAwayMode.ReceiptState.Logic, ReceivePutAway, ReceivePutAway.Host>
    {
      public static bool IsActive() => RPACartSupport.IsActive();

      public ReceivePutAway Basis
      {
        get => ((PXGraphExtension<ReceivePutAway, ReceivePutAway.Host>) this).Base1;
      }

      public RPACartSupport CartBasis => this.Basis.Get<RPACartSupport>();

      public ReceivePutAway.PutAwayMode.ReceiptState.Logic Target => this.Base2;

      /// Overrides <see cref="M:PX.Objects.PO.WMS.ReceivePutAway.PutAwayMode.ReceiptState.Logic.CanBePutAway(PX.Objects.PO.POReceipt,PX.BarcodeProcessing.Validation@)" />
      [PXOverride]
      public virtual bool CanBePutAway(
        PX.Objects.PO.POReceipt receipt,
        out Validation error,
        RPACartSupport.AlterPutAwayModeLogic.AlterRefNbrStateLogic.CanBePutAwayDelegate base_CanBePutAway)
      {
        int? cartId1 = this.CartBasis.CartID;
        int num1 = 0;
        if (cartId1.GetValueOrDefault() > num1 & cartId1.HasValue)
        {
          int? siteId1 = this.Basis.SiteID;
          int? siteId2 = this.Target.SiteID;
          if (!(siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue))
          {
            error = Validation.Fail("The warehouse of the {0} cart differs from the warehouse of the selected document.", new object[1]
            {
              (object) this.CartBasis.SelectedCart?.CartCD
            });
            return false;
          }
        }
        Lazy<POReceiptSplitToCartSplitLink> lazy = Lazy.By<POReceiptSplitToCartSplitLink>((Func<POReceiptSplitToCartSplitLink>) (() => PXResultset<POReceiptSplitToCartSplitLink>.op_Implicit(((PXSelectBase<POReceiptSplitToCartSplitLink>) this.CartBasis.CartSplitLinks).Select(Array.Empty<object>()))));
        int? cartId2 = this.CartBasis.CartID;
        int num2 = 0;
        if (cartId2.GetValueOrDefault() > num2 & cartId2.HasValue && lazy.Value != null && lazy.Value.ReceiptNbr != receipt.ReceiptNbr)
        {
          error = Validation.Fail("The {0} receipt has already been added to the cart. Remove all items from the cart before adding items from another receipt.", new object[1]
          {
            (object) lazy.Value.ReceiptNbr
          });
          return false;
        }
        if (PXResultset<POReceiptLineSplit>.op_Implicit(PXSelectBase<POReceiptLineSplit, PXViewOf<POReceiptLineSplit>.BasedOn<SelectFromBase<POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<POReceiptLineSplit.receiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<POReceiptLineSplit.receiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, POReceiptLineSplit>, PX.Objects.PO.POReceipt, POReceiptLineSplit>.SameAsCurrent.And<BqlOperand<POReceiptLineSplit.putAwayQty, IBqlDecimal>.IsLess<POReceiptLineSplit.qty>>>>.Config>.SelectSingleBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), (object[]) new PX.Objects.PO.POReceipt[1]
        {
          receipt
        }, Array.Empty<object>())) == null)
        {
          PX.Objects.IN.INRegister inRegister = PXResultset<PX.Objects.IN.INRegister>.op_Implicit(PXSelectBase<PX.Objects.IN.INRegister, PXViewOf<PX.Objects.IN.INRegister>.BasedOn<SelectFromBase<PX.Objects.IN.INRegister, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.INRegister.docType, Equal<INDocType.transfer>>>>, And<BqlOperand<PX.Objects.IN.INRegister.transferType, IBqlString>.IsEqual<INTransferType.oneStep>>>, And<BqlOperand<PX.Objects.IN.INRegister.released, IBqlBool>.IsEqual<False>>>>.And<KeysRelation<CompositeKey<Field<PX.Objects.IN.INRegister.pOReceiptType>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptType>, Field<PX.Objects.IN.INRegister.pOReceiptNbr>.IsRelatedTo<PX.Objects.PO.POReceipt.receiptNbr>>.WithTablesOf<PX.Objects.PO.POReceipt, PX.Objects.IN.INRegister>, PX.Objects.PO.POReceipt, PX.Objects.IN.INRegister>.SameAsCurrent>>>.ReadOnly.Config>.SelectSingleBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), (object[]) new PX.Objects.PO.POReceipt[1]
          {
            receipt
          }, Array.Empty<object>()));
          ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Basis.Graph.Document).Current = receipt;
          Decimal num3 = this.CartBasis.IsCartRequired() ? ((IEnumerable<POReceiptSplitToCartSplitLink>) ((PXSelectBase<POReceiptSplitToCartSplitLink>) this.CartBasis.AllCartSplitLinks).SelectMain(Array.Empty<object>())).Sum<POReceiptSplitToCartSplitLink>((Func<POReceiptSplitToCartSplitLink, Decimal>) (_ => _.Qty.GetValueOrDefault())) : 0M;
          ((PXSelectBase<PX.Objects.PO.POReceipt>) this.Basis.Graph.Document).Current = (PX.Objects.PO.POReceipt) null;
          if (inRegister == null && num3 == 0M)
          {
            error = Validation.Fail("The {0} receipt has already been put away in full.", new object[1]
            {
              (object) receipt.ReceiptNbr
            });
            return false;
          }
        }
        error = Validation.Ok;
        return true;
      }

      public delegate bool CanBePutAwayDelegate(PX.Objects.PO.POReceipt receipt, out Validation error);
    }

    /// Overrides <see cref="T:PX.Objects.PO.WMS.ReceivePutAway.PutAwayMode.ConfirmState.Logic" />
    public class AlterConfirmStateLogic : 
      PXGraphExtension<ReceivePutAway.PutAwayMode.ConfirmState.Logic, ReceivePutAway, ReceivePutAway.Host>
    {
      public static bool IsActive() => RPACartSupport.IsActive();

      public ReceivePutAway Basis
      {
        get => ((PXGraphExtension<ReceivePutAway, ReceivePutAway.Host>) this).Base1;
      }

      public ReceivePutAway.PutAwayMode.ConfirmState.Logic State => this.Base2;

      public ReceivePutAway.PutAwayMode.Logic Mode
      {
        get => this.Basis.Get<ReceivePutAway.PutAwayMode.Logic>();
      }

      public RPACartSupport CartBasis => this.Basis.Get<RPACartSupport>();

      /// Overrides <see cref="M:PX.Objects.PO.WMS.ReceivePutAway.PutAwayMode.ConfirmState.Logic.ProcessPutAway" />
      [PXOverride]
      public virtual FlowStatus ProcessPutAway(Func<FlowStatus> base_ProcessPutAway)
      {
        if (!this.CartBasis.IsCartRequired())
          return base_ProcessPutAway();
        bool? cartLoaded = this.CartBasis.CartLoaded;
        bool flag = false;
        return cartLoaded.GetValueOrDefault() == flag & cartLoaded.HasValue ? this.ProcessPutAwayInCart() : this.ProcessPutAwayOutCart();
      }

      protected virtual FlowStatus ProcessPutAwayInCart()
      {
        bool valueOrDefault = this.Basis.Remove.GetValueOrDefault();
        if (this.Mode.IsSingleLocation)
          this.Basis.LocationID = (int?) ((IEnumerable<POReceiptLineSplit>) ((PXSelectBase<POReceiptLineSplit>) this.Mode.PutAway).SelectMain(Array.Empty<object>())).FirstOrDefault<POReceiptLineSplit>()?.LocationID;
        IEnumerable<POReceiptLineSplit> source = ((IEnumerable<POReceiptLineSplit>) ((PXSelectBase<POReceiptLineSplit>) this.Mode.PutAway).SelectMain(Array.Empty<object>())).Where<POReceiptLineSplit>((Func<POReceiptLineSplit, bool>) (r =>
        {
          int? nullable1 = r.LocationID;
          int? nullable2 = this.Basis.LocationID ?? r.LocationID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            int? nullable3 = r.InventoryID;
            nullable1 = this.Basis.InventoryID;
            if (nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue)
            {
              nullable1 = r.SubItemID;
              nullable3 = this.Basis.SubItemID;
              if (nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue)
                return string.Equals(r.LotSerialNbr, this.Basis.LotSerialNbr ?? r.LotSerialNbr, StringComparison.OrdinalIgnoreCase);
            }
          }
          return false;
        }));
        if (!source.Any<POReceiptLineSplit>())
        {
          FlowStatus flowStatus = FlowStatus.Fail("No items to put away.", Array.Empty<object>());
          return ((FlowStatus) ref flowStatus).WithModeReset;
        }
        if (!this.Basis.EnsureLocationPrimaryItem(this.Basis.InventoryID, this.Basis.LocationID))
          return FlowStatus.Fail("Selected item is not allowed in this location.", Array.Empty<object>());
        Decimal num1 = Sign.op_Multiply(Sign.MinusIf(valueOrDefault), this.Basis.BaseQty);
        if (num1 != 0M)
        {
          if (!valueOrDefault)
          {
            Decimal? nullable = source.Sum<POReceiptLineSplit>((Func<POReceiptLineSplit, Decimal?>) (s =>
            {
              Decimal? qty = s.Qty;
              Decimal? putAwayQty = s.PutAwayQty;
              return !(qty.HasValue & putAwayQty.HasValue) ? new Decimal?() : new Decimal?(qty.GetValueOrDefault() - putAwayQty.GetValueOrDefault());
            }));
            Decimal num2 = num1;
            if (nullable.GetValueOrDefault() < num2 & nullable.HasValue)
              return FlowStatus.Fail("The put away quantity cannot be greater than the received quantity.", Array.Empty<object>());
          }
          if (valueOrDefault && source.Sum<POReceiptLineSplit>((Func<POReceiptLineSplit, Decimal>) (s => this.CartBasis.GetCartQty(s))) + num1 < 0M)
            return FlowStatus.Fail("The cart quantity cannot be negative.", Array.Empty<object>());
          try
          {
            Decimal val2 = num1;
            foreach (POReceiptLineSplit receiptLineSplit1 in valueOrDefault ? source.Reverse<POReceiptLineSplit>() : source)
            {
              Decimal? nullable;
              Decimal num3;
              if (!valueOrDefault)
              {
                nullable = receiptLineSplit1.Qty;
                Decimal num4 = nullable.Value;
                nullable = receiptLineSplit1.PutAwayQty;
                Decimal num5 = nullable.Value;
                num3 = Math.Min(num4 - num5, val2);
              }
              else
                num3 = -Math.Min(this.CartBasis.GetCartQty(receiptLineSplit1), -val2);
              Decimal qty = num3;
              if (!(qty == 0M))
              {
                FlowStatus flowStatus = this.SyncWithCart(receiptLineSplit1, qty);
                bool? isError = ((FlowStatus) ref flowStatus).IsError;
                bool flag = false;
                if (!(isError.GetValueOrDefault() == flag & isError.HasValue))
                  return flowStatus;
                POReceiptLineSplit receiptLineSplit2 = receiptLineSplit1;
                nullable = receiptLineSplit2.PutAwayQty;
                Decimal num6 = qty;
                receiptLineSplit2.PutAwayQty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num6) : new Decimal?();
                ((PXSelectBase<POReceiptLineSplit>) this.Mode.PutAway).Update(receiptLineSplit1);
                val2 -= qty;
                if (val2 == 0M)
                  break;
              }
            }
          }
          finally
          {
            this.CartBasis.EnsureCartReceiptLink();
          }
        }
        this.Basis.ReportInfo(valueOrDefault ? "{0} x {1} {2} removed from cart." : "{0} x {1} {2} added to cart.", new object[3]
        {
          (object) this.Basis.SelectedInventoryItem.InventoryCD,
          (object) this.Basis.Qty,
          (object) this.Basis.UOM
        });
        return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
      }

      protected virtual FlowStatus ProcessPutAwayOutCart()
      {
        bool remove = this.Basis.Remove.GetValueOrDefault();
        if (this.Basis.HasActive<ReceivePutAway.PutAwayMode.TargetLocationState>() && !this.Basis.PutAwayToLocationID.HasValue)
        {
          FlowStatus flowStatus = FlowStatus.Fail("Destination location not selected.", Array.Empty<object>());
          return ((FlowStatus) ref flowStatus).WithModeReset;
        }
        IEnumerable<POReceiptLineSplit> source = ((IEnumerable<POReceiptLineSplit>) ((PXSelectBase<POReceiptLineSplit>) this.Mode.PutAway).SelectMain(Array.Empty<object>())).Where<POReceiptLineSplit>((Func<POReceiptLineSplit, bool>) (r =>
        {
          int? inventoryId1 = r.InventoryID;
          int? inventoryId2 = this.Basis.InventoryID;
          if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
          {
            int? subItemId1 = r.SubItemID;
            int? subItemId2 = this.Basis.SubItemID;
            if (subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue && string.Equals(r.LotSerialNbr, this.Basis.LotSerialNbr ?? r.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
            {
              if (!remove)
                return this.CartBasis.GetCartQty(r) > 0M;
              Decimal? putAwayQty = r.PutAwayQty;
              Decimal num = 0M;
              return putAwayQty.GetValueOrDefault() > num & putAwayQty.HasValue;
            }
          }
          return false;
        }));
        if (!source.Any<POReceiptLineSplit>())
        {
          FlowStatus flowStatus = FlowStatus.Fail("No items to put away.", Array.Empty<object>());
          return ((FlowStatus) ref flowStatus).WithModeReset;
        }
        Decimal num1 = Sign.op_Multiply(Sign.MinusIf(remove), this.Basis.BaseQty);
        if (num1 != 0M)
        {
          if (remove)
          {
            Decimal? nullable1 = source.Sum<POReceiptLineSplit>((Func<POReceiptLineSplit, Decimal?>) (s => s.PutAwayQty));
            Decimal num2 = num1;
            Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num2) : new Decimal?();
            Decimal num3 = 0M;
            if (nullable2.GetValueOrDefault() < num3 & nullable2.HasValue)
              return FlowStatus.Fail("The put away quantity cannot be negative.", Array.Empty<object>());
          }
          if (!remove && source.Sum<POReceiptLineSplit>((Func<POReceiptLineSplit, Decimal>) (s => this.CartBasis.GetCartQty(s))) - num1 < 0M)
            return FlowStatus.Fail("The cart quantity cannot be negative.", Array.Empty<object>());
          try
          {
            Decimal val2 = num1;
            foreach (POReceiptLineSplit receiptLineSplit in remove ? source.Reverse<POReceiptLineSplit>() : source)
            {
              Decimal qty = remove ? -Math.Min(receiptLineSplit.PutAwayQty.Value, -val2) : Math.Min(this.CartBasis.GetCartQty(receiptLineSplit), val2);
              if (!(qty == 0M))
              {
                FlowStatus flowStatus1 = this.SyncWithCart(receiptLineSplit, -qty);
                bool? isError1 = ((FlowStatus) ref flowStatus1).IsError;
                bool flag1 = false;
                if (!(isError1.GetValueOrDefault() == flag1 & isError1.HasValue))
                  return flowStatus1;
                FlowStatus flowStatus2 = this.State.SyncWithTransfer(receiptLineSplit, qty);
                bool? isError2 = ((FlowStatus) ref flowStatus2).IsError;
                bool flag2 = false;
                if (!(isError2.GetValueOrDefault() == flag2 & isError2.HasValue))
                  return flowStatus2;
                val2 -= qty;
                if (val2 == 0M)
                  break;
              }
            }
          }
          finally
          {
            this.CartBasis.EnsureCartReceiptLink();
          }
        }
        if (!((IEnumerable<INCartSplit>) ((PXSelectBase<INCartSplit>) this.CartBasis.CartSplits).SelectMain(Array.Empty<object>())).Any<INCartSplit>())
        {
          this.CartBasis.CartLoaded = new bool?(false);
          this.Basis.ReportInfo("The {0} cart is empty.", new object[1]
          {
            (object) this.Basis.SightOf<CartScanHeader.cartID>()
          });
        }
        else
          this.Basis.ReportInfo(remove ? "{0} x {1} {2} removed from target location." : "{0} x {1} {2} added to target location.", new object[3]
          {
            (object) this.Basis.SelectedInventoryItem.InventoryCD,
            (object) this.Basis.Qty,
            (object) this.Basis.UOM
          });
        return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
      }

      protected virtual FlowStatus SyncWithCart(POReceiptLineSplit receivedSplit, Decimal qty)
      {
        int? cartId = this.CartBasis.CartID;
        INCartSplit inCartSplit1 = ((IEnumerable<INCartSplit>) ((IEnumerable<INCartSplit>) GraphHelper.RowCast<INCartSplit>((IEnumerable) PXSelectBase<POReceiptSplitToCartSplitLink, PXViewOf<POReceiptSplitToCartSplitLink>.BasedOn<SelectFromBase<POReceiptSplitToCartSplitLink, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCartSplit>.On<POReceiptSplitToCartSplitLink.FK.CartSplit>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<POReceiptSplitToCartSplitLink.receiptType>.IsRelatedTo<POReceiptLineSplit.receiptType>, Field<POReceiptSplitToCartSplitLink.receiptNbr>.IsRelatedTo<POReceiptLineSplit.receiptNbr>, Field<POReceiptSplitToCartSplitLink.receiptLineNbr>.IsRelatedTo<POReceiptLineSplit.lineNbr>, Field<POReceiptSplitToCartSplitLink.receiptSplitLineNbr>.IsRelatedTo<POReceiptLineSplit.splitLineNbr>>.WithTablesOf<POReceiptLineSplit, POReceiptSplitToCartSplitLink>, POReceiptLineSplit, POReceiptSplitToCartSplitLink>.SameAsCurrent>, And<BqlOperand<POReceiptSplitToCartSplitLink.siteID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<POReceiptSplitToCartSplitLink.cartID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.SelectMultiBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), new object[1]
        {
          (object) receivedSplit
        }, new object[2]
        {
          (object) this.Basis.SiteID,
          (object) cartId
        })).ToArray<INCartSplit>()).Concat<INCartSplit>((IEnumerable<INCartSplit>) GraphHelper.RowCast<INCartSplit>((IEnumerable) PXSelectBase<INCartSplit, PXViewOf<INCartSplit>.BasedOn<SelectFromBase<INCartSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCartSplit.cartID, Equal<P.AsInt>>>>, And<BqlOperand<INCartSplit.inventoryID, IBqlInt>.IsEqual<BqlField<POReceiptLineSplit.inventoryID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INCartSplit.subItemID, IBqlInt>.IsEqual<BqlField<POReceiptLineSplit.subItemID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INCartSplit.siteID, IBqlInt>.IsEqual<BqlField<POReceiptLineSplit.siteID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INCartSplit.fromLocationID, IBqlInt>.IsEqual<BqlField<POReceiptLineSplit.locationID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<INCartSplit.lotSerialNbr, IBqlString>.IsEqual<BqlField<POReceiptLineSplit.lotSerialNbr, IBqlString>.FromCurrent>>>>.Config>.SelectMultiBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), new object[1]
        {
          (object) receivedSplit
        }, new object[1]{ (object) cartId })).ToArray<INCartSplit>()).ToArray<INCartSplit>()).FirstOrDefault<INCartSplit>((Func<INCartSplit, bool>) (s => string.Equals(s.LotSerialNbr, this.Basis.LotSerialNbr ?? s.LotSerialNbr, StringComparison.OrdinalIgnoreCase)));
        INCartSplit cartSplit;
        Decimal? qty1;
        if (inCartSplit1 == null)
        {
          cartSplit = ((PXSelectBase<INCartSplit>) this.CartBasis.CartSplits).Insert(new INCartSplit()
          {
            CartID = cartId,
            InventoryID = receivedSplit.InventoryID,
            SubItemID = receivedSplit.SubItemID,
            LotSerialNbr = receivedSplit.LotSerialNbr,
            ExpireDate = receivedSplit.ExpireDate,
            UOM = receivedSplit.UOM,
            SiteID = receivedSplit.SiteID,
            FromLocationID = receivedSplit.LocationID,
            Qty = new Decimal?(qty)
          });
        }
        else
        {
          INCartSplit inCartSplit2 = inCartSplit1;
          qty1 = inCartSplit2.Qty;
          Decimal num = qty;
          inCartSplit2.Qty = qty1.HasValue ? new Decimal?(qty1.GetValueOrDefault() + num) : new Decimal?();
          cartSplit = ((PXSelectBase<INCartSplit>) this.CartBasis.CartSplits).Update(inCartSplit1);
        }
        qty1 = cartSplit.Qty;
        Decimal num1 = 0M;
        if (!(qty1.GetValueOrDefault() == num1 & qty1.HasValue))
          return this.EnsureReceiptCartSplitLink(receivedSplit, cartSplit, qty);
        ((PXSelectBase<INCartSplit>) this.CartBasis.CartSplits).Delete(cartSplit);
        return FlowStatus.Ok;
      }

      protected virtual FlowStatus EnsureReceiptCartSplitLink(
        POReceiptLineSplit poSplit,
        INCartSplit cartSplit,
        Decimal deltaQty)
      {
        PXSelectBase<POReceiptSplitToCartSplitLink> cartSplitLinks = (PXSelectBase<POReceiptSplitToCartSplitLink>) this.CartBasis.CartSplitLinks;
        POReceiptSplitToCartSplitLink[] array = GraphHelper.RowCast<POReceiptSplitToCartSplitLink>((IEnumerable) PXSelectBase<POReceiptSplitToCartSplitLink, PXViewOf<POReceiptSplitToCartSplitLink>.BasedOn<SelectFromBase<POReceiptSplitToCartSplitLink, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<POReceiptSplitToCartSplitLink.siteID>.IsRelatedTo<INCartSplit.siteID>, Field<POReceiptSplitToCartSplitLink.cartID>.IsRelatedTo<INCartSplit.cartID>, Field<POReceiptSplitToCartSplitLink.cartSplitLineNbr>.IsRelatedTo<INCartSplit.splitLineNbr>>.WithTablesOf<INCartSplit, POReceiptSplitToCartSplitLink>, INCartSplit, POReceiptSplitToCartSplitLink>.SameAsCurrent.Or<KeysRelation<CompositeKey<Field<POReceiptSplitToCartSplitLink.receiptType>.IsRelatedTo<POReceiptLineSplit.receiptType>, Field<POReceiptSplitToCartSplitLink.receiptNbr>.IsRelatedTo<POReceiptLineSplit.receiptNbr>, Field<POReceiptSplitToCartSplitLink.receiptLineNbr>.IsRelatedTo<POReceiptLineSplit.lineNbr>, Field<POReceiptSplitToCartSplitLink.receiptSplitLineNbr>.IsRelatedTo<POReceiptLineSplit.splitLineNbr>>.WithTablesOf<POReceiptLineSplit, POReceiptSplitToCartSplitLink>, POReceiptLineSplit, POReceiptSplitToCartSplitLink>.SameAsCurrent>>>.Config>.SelectMultiBound(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), new object[2]
        {
          (object) cartSplit,
          (object) poSplit
        }, Array.Empty<object>())).ToArray<POReceiptSplitToCartSplitLink>();
        POReceiptSplitToCartSplitLink splitToCartSplitLink1 = ((IEnumerable<POReceiptSplitToCartSplitLink>) array).FirstOrDefault<POReceiptSplitToCartSplitLink>((Func<POReceiptSplitToCartSplitLink, bool>) (link => KeysRelation<CompositeKey<Field<POReceiptSplitToCartSplitLink.siteID>.IsRelatedTo<INCartSplit.siteID>, Field<POReceiptSplitToCartSplitLink.cartID>.IsRelatedTo<INCartSplit.cartID>, Field<POReceiptSplitToCartSplitLink.cartSplitLineNbr>.IsRelatedTo<INCartSplit.splitLineNbr>>.WithTablesOf<INCartSplit, POReceiptSplitToCartSplitLink>, INCartSplit, POReceiptSplitToCartSplitLink>.Match(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), cartSplit, link) && KeysRelation<CompositeKey<Field<POReceiptSplitToCartSplitLink.receiptType>.IsRelatedTo<POReceiptLineSplit.receiptType>, Field<POReceiptSplitToCartSplitLink.receiptNbr>.IsRelatedTo<POReceiptLineSplit.receiptNbr>, Field<POReceiptSplitToCartSplitLink.receiptLineNbr>.IsRelatedTo<POReceiptLineSplit.lineNbr>, Field<POReceiptSplitToCartSplitLink.receiptSplitLineNbr>.IsRelatedTo<POReceiptLineSplit.splitLineNbr>>.WithTablesOf<POReceiptLineSplit, POReceiptSplitToCartSplitLink>, POReceiptLineSplit, POReceiptSplitToCartSplitLink>.Match(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), poSplit, link)));
        Decimal num1 = ((IEnumerable<POReceiptSplitToCartSplitLink>) array).Where<POReceiptSplitToCartSplitLink>((Func<POReceiptSplitToCartSplitLink, bool>) (link => KeysRelation<CompositeKey<Field<POReceiptSplitToCartSplitLink.siteID>.IsRelatedTo<INCartSplit.siteID>, Field<POReceiptSplitToCartSplitLink.cartID>.IsRelatedTo<INCartSplit.cartID>, Field<POReceiptSplitToCartSplitLink.cartSplitLineNbr>.IsRelatedTo<INCartSplit.splitLineNbr>>.WithTablesOf<INCartSplit, POReceiptSplitToCartSplitLink>, INCartSplit, POReceiptSplitToCartSplitLink>.Match(BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>.op_Implicit((BarcodeDrivenStateMachine<ReceivePutAway, ReceivePutAway.Host>) this.Basis), cartSplit, link))).Sum<POReceiptSplitToCartSplitLink>((Func<POReceiptSplitToCartSplitLink, Decimal>) (link => link.Qty.GetValueOrDefault())) + deltaQty;
        Decimal? nullable = cartSplit.Qty;
        Decimal valueOrDefault = nullable.GetValueOrDefault();
        if (num1 > valueOrDefault & nullable.HasValue)
          return FlowStatus.Fail("Link quantity cannot be greater than the quantity of a cart line split.", Array.Empty<object>());
        int num2;
        if (splitToCartSplitLink1 != null)
        {
          Decimal? qty = splitToCartSplitLink1.Qty;
          Decimal num3 = deltaQty;
          nullable = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() + num3) : new Decimal?();
          Decimal num4 = 0M;
          num2 = nullable.GetValueOrDefault() < num4 & nullable.HasValue ? 1 : 0;
        }
        else
          num2 = deltaQty < 0M ? 1 : 0;
        if (num2 != 0)
          return FlowStatus.Fail("Link quantity cannot be negative.", Array.Empty<object>());
        POReceiptSplitToCartSplitLink splitToCartSplitLink2;
        if (splitToCartSplitLink1 == null)
        {
          splitToCartSplitLink2 = cartSplitLinks.Insert(new POReceiptSplitToCartSplitLink()
          {
            ReceiptType = poSplit.ReceiptType,
            ReceiptNbr = poSplit.ReceiptNbr,
            ReceiptLineNbr = poSplit.LineNbr,
            ReceiptSplitLineNbr = poSplit.SplitLineNbr,
            SiteID = cartSplit.SiteID,
            CartID = cartSplit.CartID,
            CartSplitLineNbr = cartSplit.SplitLineNbr,
            Qty = new Decimal?(deltaQty)
          });
        }
        else
        {
          POReceiptSplitToCartSplitLink splitToCartSplitLink3 = splitToCartSplitLink1;
          nullable = splitToCartSplitLink3.Qty;
          Decimal num5 = deltaQty;
          splitToCartSplitLink3.Qty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num5) : new Decimal?();
          splitToCartSplitLink2 = cartSplitLinks.Update(splitToCartSplitLink1);
        }
        nullable = splitToCartSplitLink2.Qty;
        Decimal num6 = 0M;
        if (nullable.GetValueOrDefault() == num6 & nullable.HasValue)
          cartSplitLinks.Delete(splitToCartSplitLink2);
        return FlowStatus.Ok;
      }
    }
  }

  [PXUIField(DisplayName = "Cart Qty.")]
  public class CartQty : 
    PXFieldAttachedTo<POReceiptLineSplit>.By<ReceivePutAway.Host>.AsDecimal.Named<RPACartSupport.CartQty>
  {
    public override Decimal? GetValue(POReceiptLineSplit row)
    {
      return this.Base.WMS?.Get<RPACartSupport>()?.GetCartQty(row);
    }

    protected override bool? Visible
    {
      get
      {
        int num;
        if (RPACartSupport.IsActive())
        {
          ReceivePutAway wms = this.Base.WMS;
          num = wms != null ? (wms.Get<RPACartSupport>()?.IsCartRequired().GetValueOrDefault() ? 1 : 0) : 0;
        }
        else
          num = 0;
        return new bool?(num != 0);
      }
    }
  }

  [PXUIField(DisplayName = "Overall Cart Qty.")]
  public class OverallCartQty : 
    PXFieldAttachedTo<POReceiptLineSplit>.By<ReceivePutAway.Host>.AsDecimal.Named<RPACartSupport.OverallCartQty>
  {
    public override Decimal? GetValue(POReceiptLineSplit row)
    {
      return this.Base.WMS?.Get<RPACartSupport>()?.GetOverallCartQty(row);
    }

    protected override bool? Visible
    {
      get
      {
        int num;
        if (RPACartSupport.IsActive())
        {
          ReceivePutAway wms = this.Base.WMS;
          num = wms != null ? (wms.Get<RPACartSupport>()?.IsCartRequired().GetValueOrDefault() ? 1 : 0) : 0;
        }
        else
          num = 0;
        return new bool?(num != 0);
      }
    }
  }
}
