// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectRevenueTaxAmountProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.TX;
using System;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// Provides the project revenue tax amount calculations in project and base currencies calculated from the data of <see cref="P:PX.Objects.AR.ARTran.CuryTaxAmt" />
/// and from the data of <see cref="P:PX.Objects.AR.ARTax.CuryRetainedTaxAmt" />.
/// </summary>
public static class ProjectRevenueTaxAmountProvider
{
  /// <summary>
  /// Calculates the inclusive tax amount from the data of <see cref="P:PX.Objects.AR.ARTran.CuryTaxAmt" />.
  /// </summary>
  public static (Decimal? CuryAmount, Decimal? Amount) GetInclusiveTaxAmount(
    PXGraph graph,
    ARTran tran)
  {
    return ((Decimal?) tran?.CuryTaxAmt, (Decimal?) tran?.TaxAmt);
  }

  /// <summary>
  /// Calculates the inclusive retained tax amount from the data of <see cref="P:PX.Objects.AR.ARTax.CuryRetainedTaxAmt" />.
  /// </summary>
  public static (Decimal? CuryAmount, Decimal? Amount) GetRetainedInclusiveTaxAmount(
    PXGraph graph,
    ARTran tran)
  {
    if (tran == null)
      return (new Decimal?(), new Decimal?());
    ARTax arTax = PXResultset<ARTax>.op_Implicit(PXSelectBase<ARTax, PXViewOf<ARTax>.BasedOn<SelectFromBase<ARTax, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ARTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTax.tranType, Equal<ARTran.tranType>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTax.refNbr, Equal<ARTran.refNbr>>>>>.And<BqlOperand<ARTax.lineNbr, IBqlInt>.IsEqual<ARTran.lineNbr>>>>>, FbqlJoins.Inner<PX.Objects.TX.Tax>.On<BqlOperand<PX.Objects.TX.Tax.taxID, IBqlString>.IsEqual<ARTax.taxID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTran.tranType, Equal<BqlField<ARTran.tranType, IBqlString>.FromCurrent>>>>, And<BqlOperand<ARTran.refNbr, IBqlString>.IsEqual<BqlField<ARTran.refNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<ARTran.lineNbr, IBqlInt>.IsEqual<BqlField<ARTran.lineNbr, IBqlInt>.FromCurrent>>>, And<BqlOperand<PX.Objects.TX.Tax.taxType, IBqlString>.IsIn<CSTaxType.sales, CSTaxType.vat>>>>.And<BqlOperand<PX.Objects.TX.Tax.taxCalcLevel, IBqlString>.IsEqual<CSTaxCalcLevel.inclusive>>>.Aggregate<To<GroupBy<ARTran.lineNbr>, Sum<ARTax.curyRetainedTaxAmt>, Sum<ARTax.retainedTaxAmt>>>>.Config>.SelectSingleBound(graph, (object[]) new ARTran[1]
    {
      tran
    }, Array.Empty<object>()));
    return ((Decimal?) arTax?.CuryRetainedTaxAmt, (Decimal?) arTax?.RetainedTaxAmt);
  }
}
