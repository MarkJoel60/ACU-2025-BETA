// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOpenTax2Attribute
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

public class SOOpenTax2Attribute : SOOpenTaxAttribute
{
  public SOOpenTax2Attribute(Type ParentType, Type TaxType, Type TaxSumType)
    : base(ParentType, TaxType, TaxSumType)
  {
    this._CuryTaxableAmt = typeof (SOTax.curyUnshippedTaxableAmt).Name;
    this._CuryTaxAmt = typeof (SOTax.curyUnshippedTaxAmt).Name;
    this.CuryTranAmt = typeof (SOLine2.curyOpenAmt);
    this._Attributes.Clear();
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Switch<Case<Where<SOLine2.operation, Equal<Parent<SOOrder.defaultOperation>>>, SOLine2.curyOpenAmt>, Minus<SOLine2.curyOpenAmt>>), typeof (SumCalc<SOOrder.curyOpenLineTotal>)));
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundFormulaAttribute(typeof (Mult<Mult<Switch<Case<Where<SOLine2.operation, Equal<Parent<SOOrder.defaultOperation>>>, decimal1>, Minus<decimal1>>, SOLine2.curyOpenAmt>, Sub<decimal1, Mult<SOLine2.groupDiscountRate, SOLine2.documentDiscountRate>>>), typeof (SumCalc<SOOrder.curyOpenDiscTotal>)));
  }

  protected override List<object> SelectTaxes<Where>(
    PXGraph graph,
    object row,
    PXTaxCheck taxchk,
    params object[] parameters)
  {
    return this.SelectTaxes<Where, Where<True, Equal<True>>, Current<SOLine2.lineNbr>>(graph, new object[2]
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
    SOOpenTax2Attribute openTax2Attribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) openTax2Attribute, __vmethodptr(openTax2Attribute, CuryOpenAmt_FieldUpdated));
    fieldUpdated.AddHandler(itemType, curyTranAmt, pxFieldUpdated);
  }

  public virtual void CuryOpenAmt_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
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
    return this.SelectDocumentLines<SOLine2, SOLine2.orderType, SOLine2.orderNbr>(graph, row).Union<SOLine2>(this.SelectDocumentLines<SOMiscLine2, SOMiscLine2.orderType, SOMiscLine2.orderNbr>(graph, row).Select<SOMiscLine2, SOLine2>((Func<SOMiscLine2, SOLine2>) (_ => Utilities.Clone<SOMiscLine2, SOLine2>(graph, _)))).Union<SOLine2>(SOTaxAttribute.FreightToSOLine<SOLine2>(graph)).Cast<object>().ToList<object>();
  }
}
