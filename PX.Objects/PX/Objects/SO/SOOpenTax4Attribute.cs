// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOpenTax4Attribute
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

public class SOOpenTax4Attribute : SOOpenTaxAttribute
{
  public SOOpenTax4Attribute(Type ParentType, Type TaxType, Type TaxSumType)
    : base(ParentType, TaxType, TaxSumType)
  {
    this._CuryTaxableAmt = typeof (SOTax.curyUnshippedTaxableAmt).Name;
    this._CuryTaxAmt = typeof (SOTax.curyUnshippedTaxAmt).Name;
    this.CuryTranAmt = typeof (SOLine4.curyOpenAmt);
    this._Attributes.Clear();
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (SOLine4.curyOpenAmt), typeof (SumCalc<SOOrder.curyOpenLineTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Mult<SOLine4.curyOpenAmt, Sub<decimal1, Mult<SOLine4.groupDiscountRate, SOLine4.documentDiscountRate>>>), typeof (SumCalc<SOOrder.curyOpenDiscTotal>)));
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

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._ChildType = sender.GetItemType();
    this.TaxCalc = TaxCalc.Calc;
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    Type itemType = sender.GetItemType();
    string curyTranAmt = this._CuryTranAmt;
    SOOpenTax4Attribute openTax4Attribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) openTax4Attribute, __vmethodptr(openTax4Attribute, CuryUnbilledAmt_FieldUpdated));
    fieldUpdated.AddHandler(itemType, curyTranAmt, pxFieldUpdated);
  }

  public virtual void CuryUnbilledAmt_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.Graph.Caches[this._ParentType].Current = PXParentAttribute.SelectParent(sender, e.Row, this._ParentType);
    this.CalcTaxes(sender, e.Row, PXTaxCheck.Line);
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
    return this.SelectDocumentLines<SOLine4, SOLine4.orderType, SOLine4.orderNbr>(graph, row).Union<SOLine4>(this.SelectDocumentLines<SOMiscLine2, SOMiscLine2.orderType, SOMiscLine2.orderNbr>(graph, row).Select<SOMiscLine2, SOLine4>((Func<SOMiscLine2, SOLine4>) (_ => Utilities.Clone<SOMiscLine2, SOLine4>(graph, _)))).Union<SOLine4>(SOTaxAttribute.FreightToSOLine<SOLine4>(graph)).Cast<object>().ToList<object>();
  }
}
