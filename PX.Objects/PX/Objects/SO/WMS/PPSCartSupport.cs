// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.WMS.PPSCartSupport
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

#nullable disable
namespace PX.Objects.SO.WMS;

public class PPSCartSupport : CartSupport<PickPackShip, PickPackShip.Host>
{
  public FbqlSelect<SelectFromBase<SOCartShipment, TypeArrayOf<IFbqlJoin>.Empty>, SOCartShipment>.View CartsLinks;
  public FbqlSelect<SelectFromBase<INCartSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCart>.On<INCartSplit.FK.Cart>>, FbqlJoins.Inner<SOCartShipment>.On<SOCartShipment.FK.Cart>>>.Where<KeysRelation<Field<SOCartShipment.shipmentNbr>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentNbr>.AsSimpleKey.WithTablesOf<PX.Objects.SO.SOShipment, SOCartShipment>, PX.Objects.SO.SOShipment, SOCartShipment>.SameAsCurrent>, INCartSplit>.View AllCartSplits;
  public FbqlSelect<SelectFromBase<SOShipmentSplitToCartSplitLink, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCartSplit>.On<SOShipmentSplitToCartSplitLink.FK.CartSplit>>>.Where<KeysRelation<CompositeKey<Field<SOShipmentSplitToCartSplitLink.siteID>.IsRelatedTo<INCart.siteID>, Field<SOShipmentSplitToCartSplitLink.cartID>.IsRelatedTo<INCart.cartID>>.WithTablesOf<INCart, SOShipmentSplitToCartSplitLink>, INCart, SOShipmentSplitToCartSplitLink>.SameAsCurrent>, SOShipmentSplitToCartSplitLink>.View CartSplitLinks;
  public FbqlSelect<SelectFromBase<SOShipmentSplitToCartSplitLink, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCartSplit>.On<SOShipmentSplitToCartSplitLink.FK.CartSplit>>, FbqlJoins.Inner<INCart>.On<INCartSplit.FK.Cart>>, FbqlJoins.Inner<SOCartShipment>.On<SOCartShipment.FK.Cart>>>.Where<KeysRelation<Field<SOCartShipment.shipmentNbr>.IsRelatedTo<PX.Objects.SO.SOShipment.shipmentNbr>.AsSimpleKey.WithTablesOf<PX.Objects.SO.SOShipment, SOCartShipment>, PX.Objects.SO.SOShipment, SOCartShipment>.SameAsCurrent>, SOShipmentSplitToCartSplitLink>.View AllCartSplitLinks;

  public static bool IsActive() => CartSupport<PickPackShip, PickPackShip.Host>.IsActiveBase();

  public override bool IsCartRequired()
  {
    return ((PXSelectBase<SOPickPackShipSetup>) this.Basis.Setup).Current.UseCartsForPick.GetValueOrDefault() && this.Basis.Header.Mode == "PICK";
  }

  public void EnsureCartShipmentLink()
  {
    if (!this.CartID.HasValue || !this.Basis.SiteID.HasValue || this.Basis.RefNbr == null)
      return;
    SOCartShipment soCartShipment = new SOCartShipment()
    {
      SiteID = this.Basis.SiteID,
      CartID = this.CartID,
      ShipmentNbr = this.Basis.RefNbr
    };
    if (((IEnumerable<INCartSplit>) ((PXSelectBase<INCartSplit>) this.CartSplits).SelectMain(Array.Empty<object>())).Any<INCartSplit>())
      ((PXSelectBase<SOCartShipment>) this.CartsLinks).Update(soCartShipment);
    else
      ((PXSelectBase<SOCartShipment>) this.CartsLinks).Delete(soCartShipment);
  }

  protected Decimal GetCartQty(SOShipLineSplit sosplit)
  {
    return this.IsCartRequired() ? ((IEnumerable<SOShipmentSplitToCartSplitLink>) ((PXSelectBase<SOShipmentSplitToCartSplitLink>) this.CartSplitLinks).SelectMain(Array.Empty<object>())).Where<SOShipmentSplitToCartSplitLink>((Func<SOShipmentSplitToCartSplitLink, bool>) (link => KeysRelation<CompositeKey<Field<SOShipmentSplitToCartSplitLink.shipmentNbr>.IsRelatedTo<SOShipLineSplit.shipmentNbr>, Field<SOShipmentSplitToCartSplitLink.shipmentLineNbr>.IsRelatedTo<SOShipLineSplit.lineNbr>, Field<SOShipmentSplitToCartSplitLink.shipmentSplitLineNbr>.IsRelatedTo<SOShipLineSplit.splitLineNbr>>.WithTablesOf<SOShipLineSplit, SOShipmentSplitToCartSplitLink>, SOShipLineSplit, SOShipmentSplitToCartSplitLink>.Match(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), sosplit, link))).Sum<SOShipmentSplitToCartSplitLink>((Func<SOShipmentSplitToCartSplitLink, Decimal>) (_ => _.Qty.GetValueOrDefault())) : 0M;
  }

  protected Decimal GetOverallCartQty(SOShipLineSplit sosplit)
  {
    return this.IsCartRequired() ? ((IEnumerable<SOShipmentSplitToCartSplitLink>) ((PXSelectBase<SOShipmentSplitToCartSplitLink>) this.AllCartSplitLinks).SelectMain(Array.Empty<object>())).Where<SOShipmentSplitToCartSplitLink>((Func<SOShipmentSplitToCartSplitLink, bool>) (link => KeysRelation<CompositeKey<Field<SOShipmentSplitToCartSplitLink.shipmentNbr>.IsRelatedTo<SOShipLineSplit.shipmentNbr>, Field<SOShipmentSplitToCartSplitLink.shipmentLineNbr>.IsRelatedTo<SOShipLineSplit.lineNbr>, Field<SOShipmentSplitToCartSplitLink.shipmentSplitLineNbr>.IsRelatedTo<SOShipLineSplit.splitLineNbr>>.WithTablesOf<SOShipLineSplit, SOShipmentSplitToCartSplitLink>, SOShipLineSplit, SOShipmentSplitToCartSplitLink>.Match(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), sosplit, link))).Sum<SOShipmentSplitToCartSplitLink>((Func<SOShipmentSplitToCartSplitLink, Decimal>) (_ => _.Qty.GetValueOrDefault())) : 0M;
  }

  /// Overrides <see cref="T:PX.Objects.SO.WMS.PickPackShip.PickMode.Logic" />
  public class AlterPickModeLogic : 
    BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension<PickPackShip.PickMode.Logic>
  {
    public static bool IsActive() => PPSCartSupport.IsActive();

    public PPSCartSupport CartBasis
    {
      get
      {
        return ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Get<PPSCartSupport>();
      }
    }

    [PXOverride]
    public ScanMode<PickPackShip> DecorateScanMode(
      ScanMode<PickPackShip> original,
      Func<ScanMode<PickPackShip>, ScanMode<PickPackShip>> base_DecorateScanMode)
    {
      ScanMode<PickPackShip> scanMode = base_DecorateScanMode(original);
      if (!(scanMode is PickPackShip.PickMode mode))
        return scanMode;
      this.CartBasis.InjectCartState((ScanMode<PickPackShip>) mode);
      this.CartBasis.InjectCartCommands((ScanMode<PickPackShip>) mode);
      ((MethodInterceptor<ScanMode<PickPackShip>, PickPackShip>.OfFunc<IEnumerable<ScanTransition<PickPackShip>>>) ((ScanMode<PickPackShip>) mode).Intercept.CreateTransitions).ByOverride((Func<PickPackShip, Func<IEnumerable<ScanTransition<PickPackShip>>>, IEnumerable<ScanTransition<PickPackShip>>>) ((basis, base_GetTransitions) =>
      {
        PPSCartSupport ppsCartSupport = basis.Get<PPSCartSupport>();
        if (!ppsCartSupport.IsCartRequired())
          return base_GetTransitions();
        bool? cartLoaded = ppsCartSupport.CartLoaded;
        bool flag = false;
        return cartLoaded.GetValueOrDefault() == flag & cartLoaded.HasValue ? basis.StateFlow((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (flow => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) flow.From<PickPackShip.PickMode.ShipmentState>().NextTo<CartSupport<PickPackShip, PickPackShip.Host>.CartState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LocationState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.ExpireDateState>((Action<PickPackShip>) null))) : basis.StateFlow((Func<ScanStateFlow<PickPackShip>.IFrom, IEnumerable<ScanTransition<PickPackShip>>>) (flow => (IEnumerable<ScanTransition<PickPackShip>>) ((ScanStateFlow<PickPackShip>.INextTo) ((ScanStateFlow<PickPackShip>.INextTo) flow.From<PickPackShip.PickMode.ShipmentState>().NextTo<CartSupport<PickPackShip, PickPackShip.Host>.CartState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.InventoryItemState>((Action<PickPackShip>) null)).NextTo<WarehouseManagementSystem<PickPackShip, PickPackShip.Host>.LotSerialState>((Action<PickPackShip>) null)));
      }), new RelativeInject?());
      return scanMode;
    }

    [PXOverride]
    public ScanState<PickPackShip> DecorateScanState(
      ScanState<PickPackShip> original,
      Func<ScanState<PickPackShip>, ScanState<PickPackShip>> base_DecorateScanState)
    {
      ScanState<PickPackShip> scanState = base_DecorateScanState(original);
      if (!(scanState is PickPackShip.PickMode.ShipmentState shipmentState))
        return scanState;
      ((MethodInterceptor<EntityState<PickPackShip, PX.Objects.SO.SOShipment>, PickPackShip>.OfFunc<PX.Objects.SO.SOShipment, Validation>.AsAppendable) ((EntityState<PickPackShip, PX.Objects.SO.SOShipment>) shipmentState).Intercept.Validate).ByAppend((Func<Validation, PX.Objects.SO.SOShipment, Validation>) ((basis, shipment) =>
      {
        if (basis.Get<PPSCartSupport>().CartID.HasValue)
        {
          int? siteId1 = shipment.SiteID;
          int? siteId2 = basis.SiteID;
          if (!(siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue))
            return Validation.Fail("The warehouse specified in the {0} document differs from the warehouse assigned to the selected cart.", new object[1]
            {
              (object) shipment.ShipmentNbr
            });
        }
        return Validation.Ok;
      }), new RelativeInject?());
      return scanState;
    }

    /// Overrides <see cref="T:PX.Objects.SO.WMS.PickPackShip.PickMode.ConfirmState.Logic" />
    public class AlterConfirmStateLogic : 
      BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.ScanExtension<PickPackShip.PickMode.ConfirmState.Logic>
    {
      public static bool IsActive() => PPSCartSupport.IsActive();

      public PickPackShip.PickMode.Logic Mode
      {
        get
        {
          return ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Get<PickPackShip.PickMode.Logic>();
        }
      }

      public PPSCartSupport CartBasis
      {
        get
        {
          return ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).Get<PPSCartSupport>();
        }
      }

      /// Overrides <see cref="M:PX.Objects.SO.WMS.PickPackShip.PickMode.ConfirmState.Logic.ConfirmPicked" />
      [PXOverride]
      public FlowStatus ConfirmPicked(Func<FlowStatus> base_ConfirmPicked)
      {
        if (!this.CartBasis.IsCartRequired())
          return base_ConfirmPicked();
        bool? cartLoaded = this.CartBasis.CartLoaded;
        bool flag = false;
        return cartLoaded.GetValueOrDefault() == flag & cartLoaded.HasValue ? this.ConfirmPickedInCart() : this.ConfirmPickedOutCart();
      }

      protected virtual FlowStatus ConfirmPickedInCart()
      {
        bool remove = ((WarehouseManagementSystem<PickPackShip, PickPackShip.Host>) this.Basis).Remove.GetValueOrDefault();
        SOShipLineSplit soShipLineSplit = ((IEnumerable<SOShipLineSplit>) ((PXSelectBase<SOShipLineSplit>) this.Mode.Picked).SelectMain(Array.Empty<object>())).Where<SOShipLineSplit>((Func<SOShipLineSplit, bool>) (r => this.Target.IsSelectedSplit(r))).OrderByDescending<SOShipLineSplit, bool>((Func<SOShipLineSplit, bool>) (split =>
        {
          bool? isUnassigned = split.IsUnassigned;
          bool flag = false;
          if (!(isUnassigned.GetValueOrDefault() == flag & isUnassigned.HasValue & remove))
          {
            Decimal? qty = split.Qty;
            Decimal? pickedQty = split.PickedQty;
            return qty.GetValueOrDefault() > pickedQty.GetValueOrDefault() & qty.HasValue & pickedQty.HasValue;
          }
          Decimal? pickedQty1 = split.PickedQty;
          Decimal num = 0M;
          return pickedQty1.GetValueOrDefault() > num & pickedQty1.HasValue;
        })).OrderByDescending<SOShipLineSplit, bool>((Func<SOShipLineSplit, bool>) (split =>
        {
          if (!remove)
          {
            Decimal? qty = split.Qty;
            Decimal? pickedQty = split.PickedQty;
            return qty.GetValueOrDefault() > pickedQty.GetValueOrDefault() & qty.HasValue & pickedQty.HasValue;
          }
          Decimal? pickedQty2 = split.PickedQty;
          Decimal num = 0M;
          return pickedQty2.GetValueOrDefault() > num & pickedQty2.HasValue;
        })).ThenByDescending<SOShipLineSplit, bool>((Func<SOShipLineSplit, bool>) (split => string.Equals(split.LotSerialNbr, ((WarehouseManagementSystem<PickPackShip, PickPackShip.Host>) this.Basis).LotSerialNbr ?? split.LotSerialNbr, StringComparison.OrdinalIgnoreCase))).ThenByDescending<SOShipLineSplit, bool>((Func<SOShipLineSplit, bool>) (split => string.IsNullOrEmpty(split.LotSerialNbr))).ThenByDescending<SOShipLineSplit, bool>((Func<SOShipLineSplit, bool>) (split =>
        {
          Decimal? qty = split.Qty;
          Decimal? pickedQty3 = split.PickedQty;
          if (!(qty.GetValueOrDefault() > pickedQty3.GetValueOrDefault() & qty.HasValue & pickedQty3.HasValue | remove))
            return false;
          Decimal? pickedQty4 = split.PickedQty;
          Decimal num = 0M;
          return pickedQty4.GetValueOrDefault() > num & pickedQty4.HasValue;
        })).ThenByDescending<SOShipLineSplit, Decimal?>((Func<SOShipLineSplit, Decimal?>) (split =>
        {
          Sign sign = Sign.MinusIf(remove);
          Decimal? qty = split.Qty;
          Decimal? pickedQty = split.PickedQty;
          Decimal? nullable = qty.HasValue & pickedQty.HasValue ? new Decimal?(qty.GetValueOrDefault() - pickedQty.GetValueOrDefault()) : new Decimal?();
          return !nullable.HasValue ? new Decimal?() : new Decimal?(Sign.op_Multiply(sign, nullable.GetValueOrDefault()));
        })).FirstOrDefault<SOShipLineSplit>();
        if (soShipLineSplit == null)
        {
          FlowStatus flowStatus = FlowStatus.Fail(remove ? "No items to remove from shipment." : "No items to pick.", Array.Empty<object>());
          return ((FlowStatus) ref flowStatus).WithModeReset;
        }
        Decimal baseQty = ((PickPackShip) this.Basis).BaseQty;
        Decimal qtyThreshold = ((PXGraphExtension<PickPackShip.Host>) this).Base.GetQtyThreshold(soShipLineSplit);
        if (baseQty != 0M)
        {
          if (!remove)
          {
            Decimal? pickedQty = soShipLineSplit.PickedQty;
            Decimal overallCartQty = this.CartBasis.GetOverallCartQty(soShipLineSplit);
            Decimal? nullable1 = pickedQty.HasValue ? new Decimal?(pickedQty.GetValueOrDefault() + overallCartQty) : new Decimal?();
            Decimal num1 = baseQty;
            Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num1) : new Decimal?();
            Decimal? qty = soShipLineSplit.Qty;
            Decimal num2 = qtyThreshold;
            Decimal? nullable3 = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() * num2) : new Decimal?();
            if (nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue)
              return FlowStatus.Fail("The picked quantity cannot be greater than the quantity in the shipment line.", Array.Empty<object>());
          }
          if (remove && this.CartBasis.GetCartQty(soShipLineSplit) < baseQty)
            return FlowStatus.Fail("The cart quantity cannot be negative.", Array.Empty<object>());
          try
          {
            FlowStatus flowStatus = this.SyncWithCart(soShipLineSplit, Sign.op_Multiply(Sign.MinusIf(remove), baseQty));
            bool? isError = ((FlowStatus) ref flowStatus).IsError;
            bool flag = false;
            if (!(isError.GetValueOrDefault() == flag & isError.HasValue))
              return flowStatus;
          }
          finally
          {
            this.CartBasis.EnsureCartShipmentLink();
          }
        }
        this.Target.EnsureShipmentUserLinkForPick();
        ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).ReportInfo(remove ? "{0} x {1} {2} removed from cart." : "{0} x {1} {2} added to cart.", new object[3]
        {
          (object) ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).SightOf<WMSScanHeader.inventoryID>(),
          (object) ((WarehouseManagementSystem<PickPackShip, PickPackShip.Host>) this.Basis).Qty,
          (object) ((WarehouseManagementSystem<PickPackShip, PickPackShip.Host>) this.Basis).UOM
        });
        return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
      }

      protected virtual FlowStatus ConfirmPickedOutCart()
      {
        bool remove = ((WarehouseManagementSystem<PickPackShip, PickPackShip.Host>) this.Basis).Remove.GetValueOrDefault();
        IEnumerable<SOShipLineSplit> source = ((IEnumerable<SOShipLineSplit>) ((PXSelectBase<SOShipLineSplit>) this.Mode.Picked).SelectMain(Array.Empty<object>())).Where<SOShipLineSplit>((Func<SOShipLineSplit, bool>) (r =>
        {
          int? inventoryId1 = r.InventoryID;
          int? inventoryId2 = ((WarehouseManagementSystem<PickPackShip, PickPackShip.Host>) this.Basis).InventoryID;
          if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
          {
            int? subItemId1 = r.SubItemID;
            int? subItemId2 = ((WarehouseManagementSystem<PickPackShip, PickPackShip.Host>) this.Basis).SubItemID;
            if (subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue && string.Equals(r.LotSerialNbr, ((WarehouseManagementSystem<PickPackShip, PickPackShip.Host>) this.Basis).LotSerialNbr ?? r.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
            {
              if (!remove)
                return this.CartBasis.GetCartQty(r) > 0M;
              Decimal? pickedQty = r.PickedQty;
              Decimal num = 0M;
              return pickedQty.GetValueOrDefault() > num & pickedQty.HasValue;
            }
          }
          return false;
        }));
        if (!source.Any<SOShipLineSplit>())
        {
          FlowStatus flowStatus = FlowStatus.Fail(remove ? "No items to remove from shipment." : "No items to pick.", Array.Empty<object>());
          return ((FlowStatus) ref flowStatus).WithModeReset;
        }
        Decimal num1 = Sign.op_Multiply(Sign.MinusIf(remove), ((PickPackShip) this.Basis).BaseQty);
        if (num1 != 0M)
        {
          Decimal? nullable1;
          if (remove)
          {
            Decimal? nullable2 = source.Sum<SOShipLineSplit>((Func<SOShipLineSplit, Decimal?>) (_ => _.PickedQty));
            Decimal num2 = -num1;
            if (nullable2.GetValueOrDefault() < num2 & nullable2.HasValue)
              return FlowStatus.Fail("The picked quantity cannot be negative.", Array.Empty<object>());
            Decimal? nullable3 = source.Sum<SOShipLineSplit>((Func<SOShipLineSplit, Decimal?>) (_ =>
            {
              Decimal? pickedQty = _.PickedQty;
              Decimal? packedQty = _.PackedQty;
              return !(pickedQty.HasValue & packedQty.HasValue) ? new Decimal?() : new Decimal?(pickedQty.GetValueOrDefault() - packedQty.GetValueOrDefault());
            }));
            Decimal num3 = -num1;
            if (nullable3.GetValueOrDefault() < num3 & nullable3.HasValue)
              return FlowStatus.Fail("The picked quantity cannot be less than the already packed quantity.", Array.Empty<object>());
          }
          else
          {
            nullable1 = source.Sum<SOShipLineSplit>((Func<SOShipLineSplit, Decimal?>) (_ =>
            {
              Decimal? qty = _.Qty;
              Decimal qtyThreshold = ((PXGraphExtension<PickPackShip.Host>) this).Base.GetQtyThreshold(_);
              Decimal? nullable4 = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() * qtyThreshold) : new Decimal?();
              Decimal? pickedQty = _.PickedQty;
              return !(nullable4.HasValue & pickedQty.HasValue) ? new Decimal?() : new Decimal?(nullable4.GetValueOrDefault() - pickedQty.GetValueOrDefault());
            }));
            Decimal num4 = num1;
            if (nullable1.GetValueOrDefault() < num4 & nullable1.HasValue)
              return FlowStatus.Fail("The picked quantity cannot be greater than the quantity in the shipment line.", Array.Empty<object>());
            if (source.Sum<SOShipLineSplit>((Func<SOShipLineSplit, Decimal>) (_ => this.CartBasis.GetCartQty(_))) < num1)
              return FlowStatus.Fail("The cart quantity cannot be negative.", Array.Empty<object>());
          }
          try
          {
            Decimal val2 = num1;
            foreach (SOShipLineSplit soShipLineSplit1 in remove ? source.Reverse<SOShipLineSplit>() : source)
            {
              ((PickPackShip) this.Basis).EnsureAssignedSplitEditing(soShipLineSplit1);
              Decimal num5;
              if (!remove)
              {
                num5 = Math.Min(this.CartBasis.GetCartQty(soShipLineSplit1), val2);
              }
              else
              {
                nullable1 = soShipLineSplit1.PickedQty;
                num5 = -Math.Min(nullable1.Value, -val2);
              }
              Decimal num6 = num5;
              if (!(num6 == 0M))
              {
                FlowStatus flowStatus = this.SyncWithCart(soShipLineSplit1, -num6);
                bool? isError = ((FlowStatus) ref flowStatus).IsError;
                bool flag = false;
                if (!(isError.GetValueOrDefault() == flag & isError.HasValue))
                  return flowStatus;
                SOShipLineSplit soShipLineSplit2 = soShipLineSplit1;
                nullable1 = soShipLineSplit2.PickedQty;
                Decimal num7 = num6;
                soShipLineSplit2.PickedQty = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num7) : new Decimal?();
                ((PXSelectBase<SOShipLineSplit>) this.Mode.Picked).Update(soShipLineSplit1);
                val2 -= num6;
                if (val2 == 0M)
                  break;
              }
            }
          }
          finally
          {
            this.CartBasis.EnsureCartShipmentLink();
          }
        }
        this.Target.EnsureShipmentUserLinkForPick();
        if (!((IEnumerable<INCartSplit>) ((PXSelectBase<INCartSplit>) this.CartBasis.CartSplits).SelectMain(Array.Empty<object>())).Any<INCartSplit>())
        {
          this.CartBasis.CartLoaded = new bool?(false);
          ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).ReportInfo("The {0} cart is empty.", new object[1]
          {
            (object) ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).SightOf<CartScanHeader.cartID>()
          });
        }
        else
          ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).ReportInfo(remove ? "{0} x {1} {2} removed from shipment." : "{0} x {1} {2} added to shipment.", new object[3]
          {
            (object) ((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis).SightOf<WMSScanHeader.inventoryID>(),
            (object) ((WarehouseManagementSystem<PickPackShip, PickPackShip.Host>) this.Basis).Qty,
            (object) ((WarehouseManagementSystem<PickPackShip, PickPackShip.Host>) this.Basis).UOM
          });
        return ((FlowStatus) ref FlowStatus.Ok).WithDispatchNext;
      }

      protected virtual FlowStatus SyncWithCart(SOShipLineSplit pickedSplit, Decimal qty)
      {
        INCartSplit inCartSplit1 = ((IEnumerable<INCartSplit>) ((IEnumerable<INCartSplit>) GraphHelper.RowCast<INCartSplit>((IEnumerable) PXSelectBase<SOShipmentSplitToCartSplitLink, PXViewOf<SOShipmentSplitToCartSplitLink>.BasedOn<SelectFromBase<SOShipmentSplitToCartSplitLink, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INCartSplit>.On<SOShipmentSplitToCartSplitLink.FK.CartSplit>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<KeysRelation<CompositeKey<Field<SOShipmentSplitToCartSplitLink.shipmentNbr>.IsRelatedTo<SOShipLineSplit.shipmentNbr>, Field<SOShipmentSplitToCartSplitLink.shipmentLineNbr>.IsRelatedTo<SOShipLineSplit.lineNbr>, Field<SOShipmentSplitToCartSplitLink.shipmentSplitLineNbr>.IsRelatedTo<SOShipLineSplit.splitLineNbr>>.WithTablesOf<SOShipLineSplit, SOShipmentSplitToCartSplitLink>, SOShipLineSplit, SOShipmentSplitToCartSplitLink>.SameAsCurrent>, And<BqlOperand<SOShipmentSplitToCartSplitLink.siteID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<SOShipmentSplitToCartSplitLink.cartID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.SelectMultiBound(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), new object[1]
        {
          (object) pickedSplit
        }, new object[2]
        {
          (object) ((WarehouseManagementSystem<PickPackShip, PickPackShip.Host>) this.Basis).SiteID,
          (object) this.CartBasis.CartID
        })).ToArray<INCartSplit>()).Concat<INCartSplit>((IEnumerable<INCartSplit>) GraphHelper.RowCast<INCartSplit>((IEnumerable) PXSelectBase<INCartSplit, PXViewOf<INCartSplit>.BasedOn<SelectFromBase<INCartSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCartSplit.cartID, Equal<P.AsInt>>>>, And<BqlOperand<INCartSplit.inventoryID, IBqlInt>.IsEqual<BqlField<SOShipLineSplit.inventoryID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INCartSplit.subItemID, IBqlInt>.IsEqual<BqlField<SOShipLineSplit.subItemID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INCartSplit.siteID, IBqlInt>.IsEqual<BqlField<SOShipLineSplit.siteID, IBqlInt>.FromCurrent>>>, And<BqlOperand<INCartSplit.fromLocationID, IBqlInt>.IsEqual<BqlField<SOShipLineSplit.locationID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<INCartSplit.lotSerialNbr, IBqlString>.IsEqual<BqlField<SOShipLineSplit.lotSerialNbr, IBqlString>.FromCurrent>>>>.Config>.SelectMultiBound(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), new object[1]
        {
          (object) pickedSplit
        }, new object[1]{ (object) this.CartBasis.CartID })).ToArray<INCartSplit>()).ToArray<INCartSplit>()).FirstOrDefault<INCartSplit>((Func<INCartSplit, bool>) (s => string.Equals(s.LotSerialNbr, ((WarehouseManagementSystem<PickPackShip, PickPackShip.Host>) this.Basis).LotSerialNbr ?? s.LotSerialNbr, StringComparison.OrdinalIgnoreCase)));
        INCartSplit cartSplit;
        Decimal? qty1;
        if (inCartSplit1 == null)
        {
          cartSplit = ((PXSelectBase<INCartSplit>) this.CartBasis.CartSplits).Insert(new INCartSplit()
          {
            CartID = this.CartBasis.CartID,
            InventoryID = pickedSplit.InventoryID,
            SubItemID = pickedSplit.SubItemID,
            LotSerialNbr = pickedSplit.LotSerialNbr,
            ExpireDate = pickedSplit.ExpireDate,
            UOM = pickedSplit.UOM,
            SiteID = pickedSplit.SiteID,
            FromLocationID = pickedSplit.LocationID,
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
          return this.EnsureShipmentCartSplitLink(pickedSplit, cartSplit, qty);
        ((PXSelectBase<INCartSplit>) this.CartBasis.CartSplits).Delete(cartSplit);
        return FlowStatus.Ok;
      }

      protected virtual FlowStatus EnsureShipmentCartSplitLink(
        SOShipLineSplit soSplit,
        INCartSplit cartSplit,
        Decimal deltaQty)
      {
        SOShipmentSplitToCartSplitLink[] array = GraphHelper.RowCast<SOShipmentSplitToCartSplitLink>((IEnumerable) PXSelectBase<SOShipmentSplitToCartSplitLink, PXViewOf<SOShipmentSplitToCartSplitLink>.BasedOn<SelectFromBase<SOShipmentSplitToCartSplitLink, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<SOShipmentSplitToCartSplitLink.siteID>.IsRelatedTo<INCartSplit.siteID>, Field<SOShipmentSplitToCartSplitLink.cartID>.IsRelatedTo<INCartSplit.cartID>, Field<SOShipmentSplitToCartSplitLink.cartSplitLineNbr>.IsRelatedTo<INCartSplit.splitLineNbr>>.WithTablesOf<INCartSplit, SOShipmentSplitToCartSplitLink>, INCartSplit, SOShipmentSplitToCartSplitLink>.SameAsCurrent.Or<KeysRelation<CompositeKey<Field<SOShipmentSplitToCartSplitLink.shipmentNbr>.IsRelatedTo<SOShipLineSplit.shipmentNbr>, Field<SOShipmentSplitToCartSplitLink.shipmentLineNbr>.IsRelatedTo<SOShipLineSplit.lineNbr>, Field<SOShipmentSplitToCartSplitLink.shipmentSplitLineNbr>.IsRelatedTo<SOShipLineSplit.splitLineNbr>>.WithTablesOf<SOShipLineSplit, SOShipmentSplitToCartSplitLink>, SOShipLineSplit, SOShipmentSplitToCartSplitLink>.SameAsCurrent>>>.Config>.SelectMultiBound(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), new object[2]
        {
          (object) cartSplit,
          (object) soSplit
        }, Array.Empty<object>())).ToArray<SOShipmentSplitToCartSplitLink>();
        SOShipmentSplitToCartSplitLink splitToCartSplitLink1 = ((IEnumerable<SOShipmentSplitToCartSplitLink>) array).FirstOrDefault<SOShipmentSplitToCartSplitLink>((Func<SOShipmentSplitToCartSplitLink, bool>) (link => KeysRelation<CompositeKey<Field<SOShipmentSplitToCartSplitLink.siteID>.IsRelatedTo<INCartSplit.siteID>, Field<SOShipmentSplitToCartSplitLink.cartID>.IsRelatedTo<INCartSplit.cartID>, Field<SOShipmentSplitToCartSplitLink.cartSplitLineNbr>.IsRelatedTo<INCartSplit.splitLineNbr>>.WithTablesOf<INCartSplit, SOShipmentSplitToCartSplitLink>, INCartSplit, SOShipmentSplitToCartSplitLink>.Match(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), cartSplit, link) && KeysRelation<CompositeKey<Field<SOShipmentSplitToCartSplitLink.shipmentNbr>.IsRelatedTo<SOShipLineSplit.shipmentNbr>, Field<SOShipmentSplitToCartSplitLink.shipmentLineNbr>.IsRelatedTo<SOShipLineSplit.lineNbr>, Field<SOShipmentSplitToCartSplitLink.shipmentSplitLineNbr>.IsRelatedTo<SOShipLineSplit.splitLineNbr>>.WithTablesOf<SOShipLineSplit, SOShipmentSplitToCartSplitLink>, SOShipLineSplit, SOShipmentSplitToCartSplitLink>.Match(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), soSplit, link)));
        Decimal num1 = ((IEnumerable<SOShipmentSplitToCartSplitLink>) array).Where<SOShipmentSplitToCartSplitLink>((Func<SOShipmentSplitToCartSplitLink, bool>) (link => KeysRelation<CompositeKey<Field<SOShipmentSplitToCartSplitLink.siteID>.IsRelatedTo<INCartSplit.siteID>, Field<SOShipmentSplitToCartSplitLink.cartID>.IsRelatedTo<INCartSplit.cartID>, Field<SOShipmentSplitToCartSplitLink.cartSplitLineNbr>.IsRelatedTo<INCartSplit.splitLineNbr>>.WithTablesOf<INCartSplit, SOShipmentSplitToCartSplitLink>, INCartSplit, SOShipmentSplitToCartSplitLink>.Match(BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>.op_Implicit((BarcodeDrivenStateMachine<PickPackShip, PickPackShip.Host>) this.Basis), cartSplit, link))).Sum<SOShipmentSplitToCartSplitLink>((Func<SOShipmentSplitToCartSplitLink, Decimal>) (_ => _.Qty.GetValueOrDefault())) + deltaQty;
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
        SOShipmentSplitToCartSplitLink splitToCartSplitLink2;
        if (splitToCartSplitLink1 == null)
        {
          splitToCartSplitLink2 = ((PXSelectBase<SOShipmentSplitToCartSplitLink>) this.CartBasis.CartSplitLinks).Insert(new SOShipmentSplitToCartSplitLink()
          {
            ShipmentNbr = soSplit.ShipmentNbr,
            ShipmentLineNbr = soSplit.LineNbr,
            ShipmentSplitLineNbr = soSplit.SplitLineNbr,
            SiteID = cartSplit.SiteID,
            CartID = cartSplit.CartID,
            CartSplitLineNbr = cartSplit.SplitLineNbr,
            Qty = new Decimal?(deltaQty)
          });
        }
        else
        {
          SOShipmentSplitToCartSplitLink splitToCartSplitLink3 = splitToCartSplitLink1;
          nullable = splitToCartSplitLink3.Qty;
          Decimal num5 = deltaQty;
          splitToCartSplitLink3.Qty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num5) : new Decimal?();
          splitToCartSplitLink2 = ((PXSelectBase<SOShipmentSplitToCartSplitLink>) this.CartBasis.CartSplitLinks).Update(splitToCartSplitLink1);
        }
        nullable = splitToCartSplitLink2.Qty;
        Decimal num6 = 0M;
        if (nullable.GetValueOrDefault() == num6 & nullable.HasValue)
          ((PXSelectBase<SOShipmentSplitToCartSplitLink>) this.CartBasis.CartSplitLinks).Delete(splitToCartSplitLink2);
        return FlowStatus.Ok;
      }
    }
  }

  [PXUIField(DisplayName = "Cart Qty.")]
  public class CartQty : 
    PXFieldAttachedTo<SOShipLineSplit>.By<PickPackShip.Host>.AsDecimal.Named<PPSCartSupport.CartQty>
  {
    public override Decimal? GetValue(SOShipLineSplit row)
    {
      return this.Base.WMS?.Get<PPSCartSupport>()?.GetCartQty(row);
    }

    protected override bool? Visible
    {
      get
      {
        int num;
        if (PPSCartSupport.IsActive())
        {
          PickPackShip wms = this.Base.WMS;
          num = wms != null ? (wms.Get<PPSCartSupport>()?.IsCartRequired().GetValueOrDefault() ? 1 : 0) : 0;
        }
        else
          num = 0;
        return new bool?(num != 0);
      }
    }
  }

  [PXUIField(DisplayName = "Overall Cart Qty.")]
  public class OverallCartQty : 
    PXFieldAttachedTo<SOShipLineSplit>.By<PickPackShip.Host>.AsDecimal.Named<PPSCartSupport.OverallCartQty>
  {
    public override Decimal? GetValue(SOShipLineSplit row)
    {
      return this.Base.WMS?.Get<PPSCartSupport>()?.GetOverallCartQty(row);
    }

    protected override bool? Visible
    {
      get
      {
        int num;
        if (PPSCartSupport.IsActive())
        {
          PickPackShip wms = this.Base.WMS;
          num = wms != null ? (wms.Get<PPSCartSupport>()?.IsCartRequired().GetValueOrDefault() ? 1 : 0) : 0;
        }
        else
          num = 0;
        return new bool?(num != 0);
      }
    }
  }
}
