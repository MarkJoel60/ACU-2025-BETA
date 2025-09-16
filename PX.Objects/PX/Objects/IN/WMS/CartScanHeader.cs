// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.CartScanHeader
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

#nullable enable
namespace PX.Objects.IN.WMS;

public sealed class CartScanHeader : PXCacheExtension<
#nullable disable
WMSScanHeader, QtyScanHeader, ScanHeader>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.wMSCartTracking>();

  [PXInt]
  [PXUIField(DisplayName = "Cart ID", Enabled = false, Visible = false)]
  [PXSelector(typeof (FbqlSelect<SelectFromBase<INCart, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INSite>.On<INCart.FK.Site>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCart.active, Equal<True>>>>>.And<Match<INSite, BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>>, INCart>.SearchFor<INCart.cartID>), SubstituteKey = typeof (INCart.cartCD), DescriptionField = typeof (INCart.descr))]
  public int? CartID { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Cart Unloading", Enabled = false)]
  [PXUIVisible(typeof (CartScanHeader.cartLoaded))]
  public bool? CartLoaded { get; set; }

  public abstract class cartID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CartScanHeader.cartID>
  {
  }

  public abstract class cartLoaded : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CartScanHeader.cartLoaded>
  {
  }
}
