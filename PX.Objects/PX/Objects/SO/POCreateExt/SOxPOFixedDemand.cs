// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.POCreateExt.SOxPOFixedDemand
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.GL;
using PX.Objects.PO;

#nullable enable
namespace PX.Objects.SO.POCreateExt;

/// <summary>
/// Holds SO related fields of the <see cref="T:PX.Objects.PO.POFixedDemand" /> object.
/// </summary>
public sealed class SOxPOFixedDemand : PXCacheExtension<
#nullable disable
POFixedDemand>
{
  /// <inheritdoc cref="P:PX.Objects.PO.POFixedDemand.VendorID" />
  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOxPOFixedDemand.salesCustomerID>, IsNull>>>>.Or<BqlOperand<PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsNotEqual<BqlField<SOxPOFixedDemand.salesCustomerID, IBqlInt>.FromCurrent>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BranchAlias.branchID, IsNull>>>, Or<BqlOperand<Current<SOxPOFixedDemand.salesBranchID>, IBqlInt>.IsNull>>>.Or<BqlOperand<BranchAlias.branchID, IBqlInt>.IsNotEqual<BqlField<SOxPOFixedDemand.salesBranchID, IBqlInt>.FromCurrent>>>>), "The vendor cannot be specified because either it has been extended from the branch of the sales order or it coincides with the customer of this order.", new System.Type[] {})]
  public int? VendorID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.BranchID" />
  [PXInt]
  public int? SalesBranchID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOOrder.CustomerID" />
  [PXInt]
  [PXSelector(typeof (Search<BAccountR.bAccountID, Where<True, Equal<True>>>), DescriptionField = typeof (BAccountR.acctName), SubstituteKey = typeof (BAccountR.acctCD))]
  [PXUIField(DisplayName = "Customer", Enabled = false)]
  public int? SalesCustomerID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.IsSpecialOrder" />
  [PXBool]
  public bool? IsSpecialOrder { get; set; }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOxPOFixedDemand.vendorID>
  {
  }

  public abstract class salesBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOxPOFixedDemand.salesBranchID>
  {
  }

  public abstract class salesCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOxPOFixedDemand.salesCustomerID>
  {
  }

  public abstract class isSpecialOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOxPOFixedDemand.isSpecialOrder>
  {
  }
}
