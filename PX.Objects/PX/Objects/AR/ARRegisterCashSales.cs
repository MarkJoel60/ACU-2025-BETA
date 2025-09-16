// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARRegisterCashSales
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;

#nullable disable
namespace PX.Objects.AR;

[PXProjection(typeof (SelectFromBase<ARRegisterSigned, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegisterSigned.released, Equal<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegisterSigned.docType, Equal<ARDocType.cashSale>>>>>.Or<BqlOperand<ARRegisterSigned.docType, IBqlString>.IsEqual<ARDocType.cashReturn>>>>.AggregateTo<GroupBy<ARRegister.finPeriodID>, Sum<ARRegisterSigned.signedOrigDocAmt>>))]
[PXCacheName("ARRegister Cash Sales")]
public class ARRegisterCashSales : ARRegisterSigned
{
}
