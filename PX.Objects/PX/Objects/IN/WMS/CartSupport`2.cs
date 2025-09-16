// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.CartSupport`2
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
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.IN.WMS;

public abstract class CartSupport<TScanBasis, TScanGraph> : PXGraphExtension<
#nullable disable
TScanBasis, TScanGraph>
  where TScanBasis : WarehouseManagementSystem<TScanBasis, TScanGraph>
  where TScanGraph : PXGraph, new()
{
  public FbqlSelect<SelectFromBase<INCart, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INCart.siteID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  WMSScanHeader.siteID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  INCart.cartID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  CartScanHeader.cartID, IBqlInt>.FromCurrent>>>, 
  #nullable disable
  INCart>.View Cart;
  public FbqlSelect<SelectFromBase<INCartSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<CompositeKey<Field<INCartSplit.siteID>.IsRelatedTo<INCart.siteID>, Field<INCartSplit.cartID>.IsRelatedTo<INCart.cartID>>.WithTablesOf<INCart, INCartSplit>, INCart, INCartSplit>.SameAsCurrent>, INCartSplit>.View CartSplits;

  public TScanBasis Basis => this.Base1;

  protected static bool IsActiveBase() => PXAccess.FeatureInstalled<FeaturesSet.wMSCartTracking>();

  public static CartSupport<TScanBasis, TScanGraph> GetSelf(TScanBasis basis)
  {
    return basis.Get<CartSupport<TScanBasis, TScanGraph>>();
  }

  public abstract bool IsCartRequired();

  public CartScanHeader CartHeader
  {
    get => ScanHeaderExt.Get<CartScanHeader>(this.Basis.Header) ?? new CartScanHeader();
  }

  public ValueSetter<ScanHeader>.Ext<CartScanHeader> CartSetter
  {
    get => this.Basis.HeaderSetter.With<CartScanHeader>();
  }

  public int? CartID
  {
    get => this.CartHeader.CartID;
    set
    {
      int? cartId = this.CartID;
      int? nullable = value;
      if (cartId.GetValueOrDefault() == nullable.GetValueOrDefault() & cartId.HasValue == nullable.HasValue)
        return;
      ValueSetter<ScanHeader>.Ext<CartScanHeader> cartSetter = this.CartSetter;
      (^ref cartSetter).Set<int?>((Expression<Func<CartScanHeader, int?>>) (h => h.CartID), value);
      ((PXSelectBase<INCart>) this.Cart).Current = PXResultset<INCart>.op_Implicit(((PXSelectBase<INCart>) this.Cart).Select(Array.Empty<object>()));
    }
  }

  public bool? CartLoaded
  {
    get => this.CartHeader.CartLoaded;
    set
    {
      ValueSetter<ScanHeader>.Ext<CartScanHeader> cartSetter = this.CartSetter;
      (^ref cartSetter).Set<bool?>((Expression<Func<CartScanHeader, bool?>>) (h => h.CartLoaded), value);
    }
  }

  public INCart SelectedCart
  {
    get
    {
      return INCart.PK.Find(BarcodeDrivenStateMachine<TScanBasis, TScanGraph>.op_Implicit((BarcodeDrivenStateMachine<TScanBasis, TScanGraph>) this.Basis), this.Basis.SiteID, this.CartID);
    }
  }

  protected virtual void _(Events.RowSelected<ScanHeader> e)
  {
    if (e.Row == null)
      return;
    bool flag = this.IsCartRequired();
    PXUIFieldAttribute.SetVisible<CartScanHeader.cartID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<ScanHeader>>) e).Cache, (object) e.Row, flag);
    if (flag)
      ((PXSelectBase<INCart>) this.Cart).Current = PXResultset<INCart>.op_Implicit(((PXSelectBase<INCart>) this.Cart).Select(Array.Empty<object>()));
    if (flag || this.Basis.CurrentMode == null)
      return;
    foreach (ScanCommand<TScanBasis> scanCommand in this.Basis.CurrentMode.Commands.OfType<CartSupport<TScanBasis, TScanGraph>.CartCommand>())
      this.Basis.Graph.Actions[scanCommand.ButtonName]?.SetVisible(false);
  }

  public virtual ScanMode<TScanBasis> InjectCartState(ScanMode<TScanBasis> mode, bool makeItDefault = false)
  {
    ((MethodInterceptor<ScanMode<TScanBasis>, TScanBasis>.OfAction<bool>) ((ScanMode<TScanBasis>) ((MethodInterceptor<ScanMode<TScanBasis>, TScanBasis>.OfFunc<IEnumerable<ScanState<TScanBasis>>>.AsAppendable) mode.Intercept.CreateStates).ByAppend((Func<TScanBasis, IEnumerable<ScanState<TScanBasis>>>) (basis => (IEnumerable<ScanState<TScanBasis>>) new CartSupport<TScanBasis, TScanGraph>.CartState[1]
    {
      new CartSupport<TScanBasis, TScanGraph>.CartState()
    }), new RelativeInject?())).Intercept.ResetMode).ByAppend((Action<TScanBasis, bool>) ((basis, fullReset) => basis.Clear<CartSupport<TScanBasis, TScanGraph>.CartState>(fullReset && !basis.IsWithinReset)), new RelativeInject?());
    if (makeItDefault)
      ((MethodInterceptor<ScanMode<TScanBasis>, TScanBasis>.OfFunc<ScanState<TScanBasis>>) mode.Intercept.GetDefaultState).ByOverride((Func<TScanBasis, Func<ScanState<TScanBasis>>, ScanState<TScanBasis>>) ((basis, base_GetDefaultState) => CartSupport<TScanBasis, TScanGraph>.GetSelf(basis).IsCartRequired() ? (ScanState<TScanBasis>) basis.FindState<CartSupport<TScanBasis, TScanGraph>.CartState>(false) : base_GetDefaultState()), new RelativeInject?());
    return mode;
  }

  public virtual ScanMode<TScanBasis> InjectCartCommands(ScanMode<TScanBasis> mode)
  {
    return (ScanMode<TScanBasis>) ((MethodInterceptor<ScanMode<TScanBasis>, TScanBasis>.OfFunc<IEnumerable<ScanCommand<TScanBasis>>>.AsAppendable) mode.Intercept.CreateCommands).ByAppend((Func<TScanBasis, IEnumerable<ScanCommand<TScanBasis>>>) (basis => (IEnumerable<ScanCommand<TScanBasis>>) new ScanCommand<TScanBasis>[2]
    {
      (ScanCommand<TScanBasis>) new CartSupport<TScanBasis, TScanGraph>.CartIn(),
      (ScanCommand<TScanBasis>) new CartSupport<TScanBasis, TScanGraph>.CartOut()
    }), new RelativeInject?());
  }

  public abstract class CartCommand : ScanCommand<TScanBasis>
  {
    public CartSupport<TScanBasis, TScanGraph> CartBasis
    {
      get => CartSupport<TScanBasis, TScanGraph>.GetSelf(((ScanComponent<TScanBasis>) this).Basis);
    }

    protected virtual bool IsEnabled
    {
      get
      {
        return ((ScanComponent<TScanBasis>) this).Basis.RefNbr != null && this.CartBasis.IsCartRequired() && this.CartBasis.CartID.HasValue;
      }
    }
  }

  public class CartOut : CartSupport<TScanBasis, TScanGraph>.CartCommand
  {
    public const string Value = "CART*OUT";

    public virtual string Code => "CART*OUT";

    public virtual string ButtonName => "scanCartOut";

    public virtual string DisplayName => "Cart Out";

    protected override bool IsEnabled
    {
      get
      {
        if (!base.IsEnabled)
          return false;
        bool? cartLoaded = this.CartBasis.CartLoaded;
        bool flag = false;
        return cartLoaded.GetValueOrDefault() == flag & cartLoaded.HasValue;
      }
    }

    protected virtual bool Process()
    {
      ((ScanComponent<TScanBasis>) this).Basis.Reset(false);
      this.CartBasis.CartLoaded = new bool?(true);
      ((ScanComponent<TScanBasis>) this).Basis.SetDefaultState((string) null, Array.Empty<object>());
      ((ScanComponent<TScanBasis>) this).Basis.Reporter.Info("Cart unloading mode activated.", Array.Empty<object>());
      return true;
    }

    public class value : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      CartSupport<TScanBasis, TScanGraph>.CartOut.value>
    {
      public value()
        : base("CART*OUT")
      {
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string DisplayName = "Cart Out";
      public const string Done = "Cart unloading mode activated.";
    }
  }

  public class CartIn : CartSupport<TScanBasis, TScanGraph>.CartCommand
  {
    public const string Value = "CART*IN";

    public virtual string Code => "CART*IN";

    public virtual string ButtonName => "scanCartIn";

    public virtual string DisplayName => "Cart In";

    protected override bool IsEnabled
    {
      get => base.IsEnabled && this.CartBasis.CartLoaded.GetValueOrDefault();
    }

    protected virtual bool Process()
    {
      ((ScanComponent<TScanBasis>) this).Basis.Reset(false);
      this.CartBasis.CartLoaded = new bool?(false);
      ((ScanComponent<TScanBasis>) this).Basis.SetDefaultState((string) null, Array.Empty<object>());
      ((ScanComponent<TScanBasis>) this).Basis.Reporter.Info("Cart loading mode activated.", Array.Empty<object>());
      return true;
    }

    public class value : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      CartSupport<TScanBasis, TScanGraph>.CartIn.value>
    {
      public value()
        : base("CART*IN")
      {
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string DisplayName = "Cart In";
      public const string Done = "Cart loading mode activated.";
    }
  }

  public class CartState : EntityState<TScanBasis, INCart>
  {
    public const string Value = "CART";

    public CartSupport<TScanBasis, TScanGraph> CartBasis
    {
      get => CartSupport<TScanBasis, TScanGraph>.GetSelf(((ScanComponent<TScanBasis>) this).Basis);
    }

    public virtual string Code => "CART";

    protected virtual string StatePrompt => "Scan the cart.";

    protected virtual bool IsStateActive()
    {
      return PXAccess.FeatureInstalled<FeaturesSet.wMSCartTracking>();
    }

    protected virtual bool IsStateSkippable() => this.CartBasis.CartID.HasValue;

    protected virtual INCart GetByBarcode(string barcode)
    {
      return PXResultset<INCart>.op_Implicit(PXSelectBase<INCart, PXViewOf<INCart>.BasedOn<SelectFromBase<INCart, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INSite>.On<INCart.FK.Site>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCart.cartCD, Equal<P.AsString>>>>>.And<Match<INSite, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>>.ReadOnly.Config>.Select(BarcodeDrivenStateMachine<TScanBasis, TScanGraph>.op_Implicit((BarcodeDrivenStateMachine<TScanBasis, TScanGraph>) ((ScanComponent<TScanBasis>) this).Basis), new object[1]
      {
        (object) barcode
      }));
    }

    protected virtual Validation Validate(INCart cart)
    {
      if (((ScanComponent<TScanBasis>) this).Basis.RefNbr != null)
      {
        int? siteId1 = cart.SiteID;
        int? siteId2 = ((ScanComponent<TScanBasis>) this).Basis.SiteID;
        if (!(siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue))
          return Validation.Fail("The warehouse of the {0} cart differs from the warehouse of the selected document.", new object[1]
          {
            (object) cart.CartCD
          });
      }
      return Validation.Ok;
    }

    protected virtual void Apply(INCart entity)
    {
      this.CartBasis.CartID = entity.CartID;
      if (((ScanComponent<TScanBasis>) this).Basis.SiteID.HasValue)
        return;
      ((ScanComponent<TScanBasis>) this).Basis.SiteID = entity.SiteID;
    }

    protected virtual void ClearState()
    {
      this.CartBasis.CartLoaded = new bool?(false);
      this.CartBasis.CartID = new int?();
      ((PXSelectBase<INCart>) this.CartBasis.Cart).Current = (INCart) null;
    }

    protected virtual void ReportMissing(string barcode)
    {
      ((ScanComponent<TScanBasis>) this).Basis.Reporter.Error("{0} cart not found.", new object[1]
      {
        (object) barcode
      });
    }

    protected virtual void ReportSuccess(INCart entity)
    {
      ((ScanComponent<TScanBasis>) this).Basis.Reporter.Info("{0} cart selected.", new object[1]
      {
        (object) entity.CartCD
      });
    }

    public class value : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      CartSupport<TScanBasis, TScanGraph>.CartState.value>
    {
      public value()
        : base("CART")
      {
      }
    }

    [PXLocalizable]
    public abstract class Msg
    {
      public const string Prompt = "Scan the cart.";
      public const string Ready = "{0} cart selected.";
      public const string Missing = "{0} cart not found.";
      public const string IsOccupied = "The {0} cart is already in use.";
      public const string InvalidSite = "The warehouse of the {0} cart differs from the warehouse of the selected document.";
      public const string CartAlreadyHasReceipt = "The {0} receipt has already been added to the cart. Remove all items from the cart before adding items from another receipt.";
    }
  }

  [PXLocalizable]
  public abstract class Msg
  {
    public const string CartIsEmpty = "The {0} cart is empty.";
    public const string LinkCartOverpicking = "Link quantity cannot be greater than the quantity of a cart line split.";
    public const string LinkUnderpicking = "Link quantity cannot be negative.";
    public const string CartOverpicking = "The overall cart quantity cannot be greater than the difference between the expected quantity and already picked quantity.";
    public const string CartUnderpicking = "The cart quantity cannot be negative.";
    public const string InventoryAdded = "{0} x {1} {2} added to cart.";
    public const string InventoryRemoved = "{0} x {1} {2} removed from cart.";
    public const string CartQty = "Cart Qty.";
    public const string CartOverallQty = "Overall Cart Qty.";
    public const string DocumentInvalidSite = "The warehouse specified in the {0} document differs from the warehouse assigned to the selected cart.";
  }
}
