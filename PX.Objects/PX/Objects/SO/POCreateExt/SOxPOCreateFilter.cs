// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.POCreateExt.SOxPOCreateFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.PO;

#nullable enable
namespace PX.Objects.SO.POCreateExt;

/// <summary>
/// Holds SO related fields of the <see cref="T:PX.Objects.PO.POCreate.POCreateFilter" /> filter object.
/// </summary>
public sealed class SOxPOCreateFilter : PXCacheExtension<
#nullable disable
POCreate.POCreateFilter>
{
  /// <summary>
  /// The field is used to filter demands by <see cref="P:PX.Objects.SO.SOOrder.CustomerID" />.
  /// </summary>
  [Customer]
  public int? CustomerID { get; set; }

  /// <summary>
  /// The field is used to filter demands by <see cref="P:PX.Objects.SO.SOOrder.OrderType" />.
  /// </summary>
  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXSelector(typeof (SearchFor<PX.Objects.SO.SOOrderType.orderType>.Where<BqlOperand<PX.Objects.SO.SOOrderType.active, IBqlBool>.IsEqual<True>>))]
  [PXUIField]
  public string OrderType { get; set; }

  /// <summary>
  /// The field is used to filter demands by <see cref="P:PX.Objects.SO.SOOrder.OrderNbr" />.
  /// </summary>
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PX.Objects.SO.SO.RefNbr(typeof (FbqlSelect<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.AR.Customer>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>>>.And<MatchUserFor<PX.Objects.AR.Customer>>>.SingleTableOnly>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.orderType, Equal<BqlField<SOxPOCreateFilter.orderType, IBqlString>.AsOptional>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.orderType, Equal<SOOrderTypeConstants.transferOrder>>>>>.Or<BqlOperand<PX.Objects.AR.Customer.bAccountID, IBqlInt>.IsNotNull>>>.Order<By<BqlField<PX.Objects.SO.SOOrder.orderNbr, IBqlString>.Desc>>, PX.Objects.SO.SOOrder>.SearchFor<PX.Objects.SO.SOOrder.orderNbr>))]
  [PXFormula(typeof (Default<SOxPOCreateFilter.orderType>))]
  [PXUIField]
  public string OrderNbr { get; set; }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOxPOCreateFilter.customerID>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOxPOCreateFilter.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOxPOCreateFilter.orderNbr>
  {
  }
}
