// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.AP.Services.APTranCalculationService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.AP.Services;

public class APTranCalculationService
{
  protected readonly PXGraph Graph;

  public APTranCalculationService(PXGraph graph) => this.Graph = graph;

  public Dictionary<int?, Decimal?> CalcCuryOrigTranAmts(
    string docType,
    string refNbr,
    int?[] lineNbrs,
    List<APTran> trans = null)
  {
    IEnumerable<APTran> source;
    if (trans == null)
    {
      PXSelectBase<APTran> pxSelectBase = (PXSelectBase<APTran>) new PXSelect<APTran, Where<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>>>>(this.Graph);
      if (lineNbrs != null)
        pxSelectBase.WhereAnd<Where<APTran.lineNbr, In<Required<APTran.lineNbr>>>>();
      source = pxSelectBase.Select(new object[3]
      {
        (object) docType,
        (object) refNbr,
        (object) lineNbrs
      }).FirstTableItems;
    }
    else
      source = (IEnumerable<APTran>) trans;
    PXSelectBase<APTax> pxSelectBase1 = (PXSelectBase<APTax>) new PXSelect<APTax, Where<APTax.tranType, Equal<Required<APTax.tranType>>, And<APTax.refNbr, Equal<Required<APTax.refNbr>>>>>(this.Graph);
    if (lineNbrs != null)
      pxSelectBase1.WhereAnd<Where<APTax.lineNbr, In<Required<APTax.lineNbr>>>>();
    Dictionary<int?, APTran> dictionary1 = source.ToDictionary<APTran, int?, APTran>((Func<APTran, int?>) (row => row.LineNbr), (Func<APTran, APTran>) (row => row));
    Dictionary<int?, Decimal?> dictionary2 = new Dictionary<int?, Decimal?>();
    foreach (APTran apTran in dictionary1.Values)
      dictionary2[apTran.LineNbr] = apTran.CuryTranAmt;
    foreach (IGrouping<int?, APTax> grouping in ((IEnumerable<PXResult<APTax>>) pxSelectBase1.Select(new object[3]
    {
      (object) docType,
      (object) refNbr,
      (object) lineNbrs
    })).AsEnumerable<PXResult<APTax>>().GroupBy<PXResult<APTax>, int?, APTax>((Func<PXResult<APTax>, int?>) (row => PXResult<APTax>.op_Implicit(row).LineNbr), (Func<PXResult<APTax>, APTax>) (row => PXResult<APTax>.op_Implicit(row))))
    {
      APTran apTran = dictionary1[grouping.Key];
      Decimal? nullable1 = new Decimal?(0M);
      foreach (APTax apTax in GraphHelper.RowCast<APTax>((IEnumerable) grouping))
      {
        PX.Objects.TX.Tax tax = PXResultset<PX.Objects.TX.Tax>.op_Implicit(PXSelectBase<PX.Objects.TX.Tax, PXSelect<PX.Objects.TX.Tax, Where<PX.Objects.TX.Tax.taxID, Equal<Required<PX.Objects.TX.Tax.taxID>>>>.Config>.Select(this.Graph, new object[1]
        {
          (object) apTax.TaxID
        }));
        if ((apTax == null || apTax.TaxID == null ? 0 : (APReleaseProcess.IncludeTaxInLineBalance(tax) ? 1 : 0)) != 0)
        {
          Decimal num1 = tax.ReverseTax.GetValueOrDefault() ? -1M : 1M;
          Decimal? nullable2 = apTax.CuryTaxAmt;
          Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
          nullable2 = apTax.CuryExpenseAmt;
          Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
          Decimal num2 = (valueOrDefault1 + valueOrDefault2) * num1;
          nullable2 = nullable1;
          Decimal num3 = num2;
          nullable1 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num3) : new Decimal?();
        }
      }
      Dictionary<int?, Decimal?> dictionary3 = dictionary2;
      int? lineNbr = apTran.LineNbr;
      Dictionary<int?, Decimal?> dictionary4 = dictionary3;
      int? key = lineNbr;
      Decimal? nullable3 = dictionary3[lineNbr];
      Decimal? nullable4 = nullable1;
      Decimal? nullable5 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
      dictionary4[key] = nullable5;
    }
    return dictionary2;
  }
}
