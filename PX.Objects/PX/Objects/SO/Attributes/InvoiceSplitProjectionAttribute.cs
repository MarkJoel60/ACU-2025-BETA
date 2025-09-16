// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Attributes.InvoiceSplitProjectionAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.SO.DAC.Unbound;
using System;

#nullable disable
namespace PX.Objects.SO.Attributes;

[PXInternalUseOnly]
public class InvoiceSplitProjectionAttribute(bool expandByFilter) : PXProjectionAttribute(InvoiceSplitProjectionAttribute.GetSelect(expandByFilter))
{
  private static Type GetSelect(bool expandByFilter)
  {
    return ((IBqlTemplate) BqlTemplate.OfCommand<SelectFromBase<PX.Objects.AR.ARTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.SO.SOLine>.On<PX.Objects.AR.ARTran.FK.SOOrderLine>>, FbqlJoins.Left<INTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<CompositeKey<Field<INTran.aRDocType>.IsRelatedTo<PX.Objects.AR.ARTran.tranType>, Field<INTran.aRRefNbr>.IsRelatedTo<PX.Objects.AR.ARTran.refNbr>, Field<INTran.aRLineNbr>.IsRelatedTo<PX.Objects.AR.ARTran.lineNbr>>.WithTablesOf<PX.Objects.AR.ARTran, INTran>>, And<Where<BqlPlaceholder.E, Equal<True>, Or<BqlOperand<PX.Objects.AR.ARTran.inventoryID, IBqlInt>.IsEqual<INTran.inventoryID>>>>>>.And<BqlOperand<INTran.docType, IBqlString>.IsNotEqual<INDocType.adjustment>>>>, FbqlJoins.Left<INTranSplit>.On<INTranSplit.FK.Tran>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.released, Equal<True>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.invtReleased, Equal<True>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.released, IsNull>>>>.And<BqlOperand<PX.Objects.AR.ARTran.lineType, IBqlString>.IsIn<SOLineType.miscCharge, SOLineType.nonInventory>>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.qty, Equal<decimal0>>>>, Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.qty, Greater<decimal0>>>>>.And<BqlOperand<PX.Objects.AR.ARTran.tranType, IBqlString>.IsIn<ARDocType.debitMemo, ARDocType.cashSale, ARDocType.invoice>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.qty, Less<decimal0>>>>>.And<BqlOperand<PX.Objects.AR.ARTran.tranType, IBqlString>.IsIn<ARDocType.creditMemo, ARDocType.cashReturn>>>>>.OrderBy<BqlField<PX.Objects.AR.ARTran.inventoryID, IBqlInt>.Asc, BqlField<INTranSplit.subItemID, IBqlInt>.Asc>>.Replace<BqlPlaceholder.E>(expandByFilter ? typeof (CurrentValue<AddInvoiceFilter.expand>) : typeof (True))).ToType();
  }
}
