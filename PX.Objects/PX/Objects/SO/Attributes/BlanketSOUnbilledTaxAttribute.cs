// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.Attributes.BlanketSOUnbilledTaxAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.SO.DAC.Projections;
using PX.Objects.TX;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO.Attributes;

public class BlanketSOUnbilledTaxAttribute : SOUnbilledTax2Attribute
{
  public BlanketSOUnbilledTaxAttribute(Type ParentType, Type TaxType, Type TaxSumType)
    : base(ParentType, TaxType, TaxSumType)
  {
    this.CuryTranAmt = typeof (BlanketSOLine.curyUnbilledAmt);
    this.GroupDiscountRate = typeof (BlanketSOLine.groupDiscountRate);
    this.DocumentDiscountRate = typeof (BlanketSOLine.documentDiscountRate);
    this._Attributes.Clear();
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<BlanketSOLine.lineType, NotEqual<SOLineType.miscCharge>>, BlanketSOLine.curyUnbilledAmt>, decimal0>), typeof (SumCalc<SOOrder.curyUnbilledLineTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<BlanketSOLine.lineType, Equal<SOLineType.miscCharge>>, BlanketSOLine.curyUnbilledAmt>, decimal0>), typeof (SumCalc<SOOrder.curyUnbilledMiscTot>)));
  }

  protected override List<object> SelectTaxes<Where>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
  {
    return this.SelectTaxes<Where, Where<True, Equal<True>>, Current<BlanketSOLine.lineNbr>>(graph, new object[2]
    {
      row,
      graph.Caches[this._ParentType].Current
    }, taxchk, parameters);
  }
}
