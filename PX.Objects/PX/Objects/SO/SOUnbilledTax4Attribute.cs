// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOUnbilledTax4Attribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.TX;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO;

public class SOUnbilledTax4Attribute : SOUnbilledTax2Attribute
{
  public SOUnbilledTax4Attribute(Type ParentType, Type TaxType, Type TaxSumType)
    : base(ParentType, TaxType, TaxSumType)
  {
    this._CuryTaxableAmt = typeof (SOTax.curyUnbilledTaxableAmt).Name;
    this._CuryExemptedAmt = typeof (SOTax.curyUnbilledTaxableAmt).Name;
    this._CuryTaxAmt = typeof (SOTax.curyUnbilledTaxAmt).Name;
    this.CuryTranAmt = typeof (SOLine4.curyUnbilledAmt);
    this._Attributes.Clear();
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (SOLine4.curyUnbilledAmt), typeof (SumCalc<SOOrder.curyUnbilledLineTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Mult<SOLine4.curyUnbilledAmt, Sub<decimal1, Mult<SOLine4.groupDiscountRate, SOLine4.documentDiscountRate>>>), typeof (SumCalc<SOOrder.curyUnbilledDiscTotal>)));
  }

  protected override List<object> SelectTaxes<Where>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
  {
    return this.SelectTaxes<Where, Where<True, Equal<True>>, Current<SOLine4.lineNbr>>(graph, new object[2]
    {
      row,
      graph.Caches[this._ParentType].Current
    }, taxchk, parameters);
  }

  protected override List<object> SelectDocumentLines(PXGraph graph, object row)
  {
    return this.SelectDocumentLines<SOLine4, SOLine4.orderType, SOLine4.orderNbr>(graph, row).Union<SOLine4>(this.SelectDocumentLines<SOMiscLine2, SOMiscLine2.orderType, SOMiscLine2.orderNbr>(graph, row).Select<SOMiscLine2, SOLine4>((Func<SOMiscLine2, SOLine4>) (_ => Utilities.Clone<SOMiscLine2, SOLine4>(graph, _)))).Union<SOLine4>(SOTaxAttribute.FreightToSOLine<SOLine4>(graph)).Cast<object>().ToList<object>();
  }
}
