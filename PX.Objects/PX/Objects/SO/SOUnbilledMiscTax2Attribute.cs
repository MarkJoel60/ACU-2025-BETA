// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOUnbilledMiscTax2Attribute
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

public class SOUnbilledMiscTax2Attribute : SOUnbilledTaxAttribute
{
  public SOUnbilledMiscTax2Attribute(Type ParentType, Type TaxType, Type TaxSumType)
    : base(ParentType, TaxType, TaxSumType)
  {
    this._CuryTaxableAmt = typeof (SOTax.curyUnbilledTaxableAmt).Name;
    this._CuryExemptedAmt = typeof (SOTax.curyUnbilledTaxableAmt).Name;
    this._CuryTaxAmt = typeof (SOTax.curyUnbilledTaxAmt).Name;
    this.CuryDocBal = (Type) null;
    this.CuryLineTotal = typeof (SOOrder.curyUnbilledMiscTot);
    this.CuryTaxTotal = typeof (SOOrder.curyUnbilledTaxTotal);
    this.DocDate = typeof (SOOrder.orderDate);
    this.CuryTranAmt = typeof (SOMiscLine2.curyUnbilledAmt);
    this._Attributes.Clear();
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (SOMiscLine2.curyUnbilledAmt), typeof (SumCalc<SOOrder.curyUnbilledMiscTot>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Mult<SOMiscLine2.curyUnbilledAmt, Sub<decimal1, Mult<SOMiscLine2.groupDiscountRate, SOMiscLine2.documentDiscountRate>>>), typeof (SumCalc<SOOrder.curyUnbilledDiscTotal>)));
  }

  protected override List<object> SelectTaxes<Where>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
  {
    return this.SelectTaxes<Where, Where<True, Equal<True>>, Current<SOMiscLine2.lineNbr>>(graph, new object[2]
    {
      row,
      graph.Caches[this._ParentType].Current
    }, taxchk, parameters);
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._ChildType = sender.GetItemType();
    this.TaxCalc = TaxCalc.Calc;
  }

  public override void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    sender.Graph.Caches[this._ParentType].Current = PXParentAttribute.SelectParent(sender, e.Row, this._ParentType);
    base.RowInserted(sender, e);
  }

  public override void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    sender.Graph.Caches[this._ParentType].Current = PXParentAttribute.SelectParent(sender, e.Row, this._ParentType);
    base.RowUpdated(sender, e);
  }

  public override void RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    sender.Graph.Caches[this._ParentType].Current = PXParentAttribute.SelectParent(sender, e.Row, this._ParentType);
    base.RowDeleted(sender, e);
  }

  protected override List<object> SelectDocumentLines(PXGraph graph, object row)
  {
    return this.SelectDocumentLines<SOMiscLine2, SOMiscLine2.orderType, SOMiscLine2.orderNbr>(graph, row).Union<SOMiscLine2>(this.SelectDocumentLines<SOLine2, SOLine2.orderType, SOLine2.orderNbr>(graph, row).Select<SOLine2, SOMiscLine2>((Func<SOLine2, SOMiscLine2>) (_ => Utilities.Clone<SOLine2, SOMiscLine2>(graph, _)))).Union<SOMiscLine2>(SOTaxAttribute.FreightToSOLine<SOMiscLine2>(graph)).Cast<object>().ToList<object>();
  }
}
