// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.GraphExtensions.SOCreateShipmentPayLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.SO;
using System;

#nullable disable
namespace PX.Objects.CC.GraphExtensions;

public class SOCreateShipmentPayLink : PXGraphExtension<SOCreateShipment>
{
  public const string CreateLinkAction = "SO301000$createLink";

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.acumaticaPayments>();

  [PXOverride]
  public PXSelectBase<PX.Objects.SO.SOOrder> GetSelectCommand(
    SOOrderFilter filter,
    Func<SOOrderFilter, PXSelectBase<PX.Objects.SO.SOOrder>> baseMethod)
  {
    return filter.Action == "SO301000$createLink" ? this.BuildCommandCreateLink() : baseMethod(filter);
  }

  protected virtual PXSelectBase<PX.Objects.SO.SOOrder> BuildCommandCreateLink()
  {
    return (PXSelectBase<PX.Objects.SO.SOOrder>) new FbqlSelect<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOOrderType>.On<PX.Objects.SO.SOOrder.FK.OrderType>>, FbqlJoins.Left<PX.Objects.CS.Carrier>.On<PX.Objects.SO.SOOrder.FK.Carrier>>, FbqlJoins.Left<PX.Objects.AR.Customer>.On<PX.Objects.SO.SOOrder.FK.Customer>.SingleTableOnly>, FbqlJoins.Left<CustomerClass>.On<PX.Objects.AR.Customer.FK.CustomerClass>>, FbqlJoins.Left<CCPayLink>.On<BqlOperand<CCPayLink.payLinkID, IBqlInt>.IsEqual<SOOrderPayLink.payLinkID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrderType.canHavePayments, Equal<True>>>>, And<BqlOperand<SOOrderPayLink.processingCenterID, IBqlString>.IsNotNull>>, And<BqlOperand<PX.Objects.SO.SOOrder.cancelled, IBqlBool>.IsEqual<False>>>, And<BqlOperand<PX.Objects.SO.SOOrder.completed, IBqlBool>.IsEqual<False>>>, And<BqlOperand<PX.Objects.SO.SOOrder.isExpired, IBqlBool>.IsEqual<False>>>, And<BqlOperand<PX.Objects.SO.SOOrder.curyUnpaidBalance, IBqlDecimal>.IsGreater<decimal0>>>, And<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.behavior, NotIn3<SOBehavior.iN, SOBehavior.mO>>>>>.Or<BqlOperand<PX.Objects.SO.SOOrder.billedCntr, IBqlInt>.IsEqual<Zero>>>>>, And<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.approved, Equal<True>>>>>.Or<BqlOperand<PX.Objects.SO.SOOrder.hold, IBqlBool>.IsEqual<True>>>>>>.And<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOOrderPayLink.payLinkID, IsNull>>>>.Or<BqlOperand<CCPayLink.linkStatus, IBqlString>.IsIn<PayLinkStatus.none, PayLinkStatus.closed>>>>>, PX.Objects.SO.SOOrder>.View((PXGraph) this.Base);
  }
}
