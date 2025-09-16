// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POUnbilledTaxRAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.TX;
using System;

#nullable disable
namespace PX.Objects.PO;

public class POUnbilledTaxRAttribute(Type ParentType, Type TaxType, Type TaxSumType) : 
  POUnbilledTaxAttribute(ParentType, TaxType, TaxSumType)
{
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.TaxCalc = TaxCalc.Calc;
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    Type itemType = sender.GetItemType();
    string curyTranAmt = this._CuryTranAmt;
    POUnbilledTaxRAttribute unbilledTaxRattribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) unbilledTaxRattribute, __vmethodptr(unbilledTaxRattribute, CuryUnbilledAmt_FieldUpdated));
    fieldUpdated.AddHandler(itemType, curyTranAmt, pxFieldUpdated);
  }

  protected override bool EnableTaxCalcOn(PXGraph aGraph)
  {
    switch (aGraph)
    {
      case POOrderEntry _:
      case POReceiptEntry _:
        return true;
      default:
        return aGraph is APReleaseProcess;
    }
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
}
