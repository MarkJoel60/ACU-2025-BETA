// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderRelatedReturnsSP
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PO;
using PX.Objects.SO.DAC.Projections;
using PX.Objects.SO.DAC.Unbound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO;

[TableAndChartDashboardType]
public class SOOrderRelatedReturnsSP : PXGraph<SOOrderRelatedReturnsSP>
{
  private const string EmptyKey = "~";
  public PXCancel<SOOrderRelatedReturnsSPFilter> Cancel;
  public PXFilter<SOOrderRelatedReturnsSPFilter> Filter;
  public PXViewOf<SOOrderRelatedReturnsSPResultDoc>.BasedOn<SelectFromBase<SOOrderRelatedReturnsSPResultDoc, TypeArrayOf<IFbqlJoin>.Empty>>.ReadOnly RelatedReturnDocuments;
  public PXViewOf<SOOrderRelatedReturnsSPResultLine>.BasedOn<SelectFromBase<SOOrderRelatedReturnsSPResultLine, TypeArrayOf<IFbqlJoin>.Empty>>.ReadOnly ReturnsByLine;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXViewOf<SOReturnShipped>.BasedOn<SelectFromBase<SOReturnShipped, TypeArrayOf<IFbqlJoin>.Empty>>.ReadOnly ShippedReturns;

  protected virtual IEnumerable filter()
  {
    SOOrderRelatedReturnsSP relatedReturnsSp = this;
    if (((PXSelectBase<SOOrderRelatedReturnsSPFilter>) relatedReturnsSp.Filter).Current != null)
    {
      if (string.IsNullOrEmpty(((PXSelectBase<SOOrderRelatedReturnsSPFilter>) relatedReturnsSp.Filter).Current.OrderType) || string.IsNullOrEmpty(((PXSelectBase<SOOrderRelatedReturnsSPFilter>) relatedReturnsSp.Filter).Current.OrderNbr))
      {
        ((PXSelectBase<SOOrderRelatedReturnsSPFilter>) relatedReturnsSp.Filter).Current.ShippedQty = new Decimal?(0M);
        ((PXSelectBase<SOOrderRelatedReturnsSPFilter>) relatedReturnsSp.Filter).Current.ReturnedQty = new Decimal?(0M);
      }
      else
      {
        SOLine soLine = PXResultset<SOLine>.op_Implicit(PXSelectBase<SOLine, PXViewOf<SOLine>.BasedOn<SelectFromBase<SOLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLine.orderType, Equal<BqlField<SOOrderRelatedReturnsSPFilter.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<SOLine.orderNbr, IBqlString>.IsEqual<BqlField<SOOrderRelatedReturnsSPFilter.orderNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<SOLine.operation, IBqlString>.IsEqual<SOOperation.issue>>>.Aggregate<To<Sum<SOLine.shippedQty>, Sum<SOLine.baseShippedQty>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) relatedReturnsSp, (object[]) null, Array.Empty<object>()));
        SOOrderRelatedReturnsSPFilter current = ((PXSelectBase<SOOrderRelatedReturnsSPFilter>) relatedReturnsSp.Filter).Current;
        short? lineSign = (short?) soLine?.LineSign;
        Decimal? nullable1 = lineSign.HasValue ? new Decimal?((Decimal) lineSign.GetValueOrDefault()) : new Decimal?();
        Decimal? baseShippedQty = (Decimal?) soLine?.BaseShippedQty;
        Decimal? nullable2 = new Decimal?((nullable1.HasValue & baseShippedQty.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * baseShippedQty.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault());
        current.ShippedQty = nullable2;
        ((PXSelectBase<SOOrderRelatedReturnsSPFilter>) relatedReturnsSp.Filter).Current.ReturnedQty = new Decimal?(relatedReturnsSp.GetReturnsByLine().Sum<SOOrderRelatedReturnsSPResultLine>((Func<SOOrderRelatedReturnsSPResultLine, Decimal?>) (l => l.ReturnedQty)).GetValueOrDefault());
      }
    }
    yield return (object) ((PXSelectBase<SOOrderRelatedReturnsSPFilter>) relatedReturnsSp.Filter).Current;
    ((PXSelectBase) relatedReturnsSp.Filter).Cache.IsDirty = false;
  }

  protected virtual IEnumerable relatedReturnDocuments()
  {
    if (NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.RelatedReturnDocuments).Cache.Cached))
      return (IEnumerable) ((PXSelectBase) this.RelatedReturnDocuments).Cache.Cached.Cast<SOOrderRelatedReturnsSPResultDoc>();
    int num = 0;
    SOOrderRelatedReturnsSPResultLine[] array1 = this.GetReturnsByLine().ToArray<SOOrderRelatedReturnsSPResultLine>();
    if (((IEnumerable<SOOrderRelatedReturnsSPResultLine>) array1).Any<SOOrderRelatedReturnsSPResultLine>((Func<SOOrderRelatedReturnsSPResultLine, bool>) (rl => rl.ReturnOrderNbr != "~")))
    {
      SOReturnShipped[] array2 = GraphHelper.RowCast<SOReturnShipped>((IEnumerable) ((PXSelectBase<SOReturnShipped>) this.ShippedReturns).Select(Array.Empty<object>())).ToArray<SOReturnShipped>();
      foreach (SOReturnShipped returnShipped in array2)
      {
        if (!this.IsDuplicatedMiscLinesOnly(returnShipped, array2, array1))
        {
          bool flag = !returnShipped.ShippingRefNoteID.HasValue && returnShipped.ARNoteID.HasValue;
          GraphHelper.Hold(((PXSelectBase) this.RelatedReturnDocuments).Cache, (object) new SOOrderRelatedReturnsSPResultDoc()
          {
            GridLineNbr = new int?(num++),
            OrderType = returnShipped.OrigOrderType,
            OrderNbr = returnShipped.OrigOrderNbr,
            ReturnOrderType = returnShipped.OrderType,
            ReturnOrderNbr = returnShipped.OrderNbr,
            ShipmentType = (flag ? "N" : returnShipped.ShipmentType),
            ShipmentNbr = (flag ? "<NEW>" : returnShipped.ShipmentNbr),
            ARDocType = (flag ? returnShipped.ARDocType : returnShipped.InvoiceType),
            ARRefNbr = (flag ? returnShipped.ARRefNbr : returnShipped.InvoiceNbr),
            APDocType = returnShipped.APDocType,
            APRefNbr = returnShipped.APRefNbr
          });
        }
      }
    }
    foreach (IGrouping<\u003C\u003Ef__AnonymousType109<string, string>, SOOrderRelatedReturnsSPResultLine> source in ((IEnumerable<SOOrderRelatedReturnsSPResultLine>) array1).Where<SOOrderRelatedReturnsSPResultLine>((Func<SOOrderRelatedReturnsSPResultLine, bool>) (rl => rl.ReturnInvoiceNbr != "~")).GroupBy(rl => new
    {
      ReturnInvoiceType = rl.ReturnInvoiceType,
      ReturnInvoiceNbr = rl.ReturnInvoiceNbr
    }))
      GraphHelper.Hold(((PXSelectBase) this.RelatedReturnDocuments).Cache, (object) new SOOrderRelatedReturnsSPResultDoc()
      {
        GridLineNbr = new int?(num++),
        OrderType = source.First<SOOrderRelatedReturnsSPResultLine>().OrderType,
        OrderNbr = source.First<SOOrderRelatedReturnsSPResultLine>().OrderNbr,
        ReturnInvoiceType = source.Key.ReturnInvoiceType,
        ReturnInvoiceNbr = source.Key.ReturnInvoiceNbr,
        ARDocType = source.Key.ReturnInvoiceType,
        ARRefNbr = source.Key.ReturnInvoiceNbr
      });
    return (IEnumerable) ((PXSelectBase) this.RelatedReturnDocuments).Cache.Cached.Cast<SOOrderRelatedReturnsSPResultDoc>();
  }

  protected virtual IEnumerable returnsByLine() => (IEnumerable) this.GetReturnsByLine();

  protected virtual IEnumerable<SOOrderRelatedReturnsSPResultLine> GetReturnsByLine()
  {
    if (NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.ReturnsByLine).Cache.Cached))
      return ((PXSelectBase) this.ReturnsByLine).Cache.Cached.Cast<SOOrderRelatedReturnsSPResultLine>();
    if (string.IsNullOrEmpty(((PXSelectBase<SOOrderRelatedReturnsSPFilter>) this.Filter).Current.OrderType) || string.IsNullOrEmpty(((PXSelectBase<SOOrderRelatedReturnsSPFilter>) this.Filter).Current.OrderNbr))
      return (IEnumerable<SOOrderRelatedReturnsSPResultLine>) Array<SOOrderRelatedReturnsSPResultLine>.Empty;
    foreach (PXResult<SOLine> pxResult in PXSelectBase<SOLine, PXViewOf<SOLine>.BasedOn<SelectFromBase<SOLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLine.origOrderType, Equal<BqlField<SOOrderRelatedReturnsSPFilter.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<SOLine.origOrderNbr, IBqlString>.IsEqual<BqlField<SOOrderRelatedReturnsSPFilter.orderNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<SOLine.operation, IBqlString>.IsEqual<SOOperation.receipt>>>, And<BqlOperand<SOLine.cancelled, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<SOLine.orderQty, IBqlDecimal>.IsNotEqual<decimal0>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      SOLine soLine = PXResult<SOLine>.op_Implicit(pxResult);
      SOOrderRelatedReturnsSPResultLine returnsSpResultLine1 = new SOOrderRelatedReturnsSPResultLine()
      {
        OrderType = soLine.OrigOrderType,
        OrderNbr = soLine.OrigOrderNbr,
        LineNbr = soLine.OrigLineNbr,
        ReturnOrderType = soLine.OrderType,
        ReturnOrderNbr = soLine.OrderNbr,
        ReturnLineType = soLine.LineType,
        DisplayReturnOrderType = soLine.OrderType,
        DisplayReturnOrderNbr = soLine.OrderNbr,
        ReturnInvoiceType = "~",
        ReturnInvoiceNbr = "~"
      };
      SOOrderRelatedReturnsSPResultLine returnsSpResultLine2 = ((PXSelectBase<SOOrderRelatedReturnsSPResultLine>) this.ReturnsByLine).Locate(returnsSpResultLine1);
      if (returnsSpResultLine2 == null)
      {
        returnsSpResultLine1.InventoryID = soLine.InventoryID;
        returnsSpResultLine1.BaseUnit = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, soLine.InventoryID).BaseUnit;
        returnsSpResultLine1.ReturnedQty = new Decimal?(0M);
        GraphHelper.Hold(((PXSelectBase) this.ReturnsByLine).Cache, (object) returnsSpResultLine1);
      }
      else
        returnsSpResultLine1 = returnsSpResultLine2;
      SOOrderRelatedReturnsSPResultLine returnsSpResultLine3 = returnsSpResultLine1;
      Decimal? returnedQty = returnsSpResultLine3.ReturnedQty;
      short? invtMult = soLine.InvtMult;
      Decimal? nullable1 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable2 = !(soLine.LineType != "MI") || !soLine.RequireShipping.GetValueOrDefault() || !soLine.Completed.GetValueOrDefault() ? soLine.BaseOrderQty : soLine.BaseShippedQty;
      Decimal? nullable3 = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?();
      returnsSpResultLine3.ReturnedQty = returnedQty.HasValue & nullable3.HasValue ? new Decimal?(returnedQty.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    }
    foreach (PXResult<PX.Objects.AR.ARTran, SOLineInvoiced> pxResult in PXSelectBase<PX.Objects.AR.ARTran, PXViewOf<PX.Objects.AR.ARTran>.BasedOn<SelectFromBase<PX.Objects.AR.ARTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOLineInvoiced>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLineInvoiced.tranType, Equal<PX.Objects.AR.ARTran.origInvoiceType>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLineInvoiced.refNbr, Equal<PX.Objects.AR.ARTran.origInvoiceNbr>>>>>.And<BqlOperand<SOLineInvoiced.lineNbr, IBqlInt>.IsEqual<PX.Objects.AR.ARTran.origInvoiceLineNbr>>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLineInvoiced.sOOrderType, Equal<BqlField<SOOrderRelatedReturnsSPFilter.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<SOLineInvoiced.sOOrderNbr, IBqlString>.IsEqual<BqlField<SOOrderRelatedReturnsSPFilter.orderNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<PX.Objects.AR.ARTran.sOOrderType, IBqlString>.IsNull>>, And<BqlOperand<PX.Objects.AR.ARTran.sOOrderNbr, IBqlString>.IsNull>>>.And<BqlOperand<Mult<PX.Objects.AR.ARTran.qty, PX.Objects.AR.ARTran.invtMult>, IBqlDecimal>.IsGreater<decimal0>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran, SOLineInvoiced>.op_Implicit(pxResult);
      SOLineInvoiced soLineInvoiced = PXResult<PX.Objects.AR.ARTran, SOLineInvoiced>.op_Implicit(pxResult);
      SOOrderRelatedReturnsSPResultLine returnsSpResultLine4 = new SOOrderRelatedReturnsSPResultLine()
      {
        OrderType = soLineInvoiced.SOOrderType,
        OrderNbr = soLineInvoiced.SOOrderNbr,
        LineNbr = soLineInvoiced.SOOrderLineNbr,
        ReturnOrderType = "~",
        ReturnOrderNbr = "~",
        ReturnInvoiceType = arTran.TranType,
        ReturnInvoiceNbr = arTran.RefNbr,
        DisplayReturnInvoiceType = arTran.TranType,
        DisplayReturnInvoiceNbr = arTran.RefNbr
      };
      SOOrderRelatedReturnsSPResultLine returnsSpResultLine5 = ((PXSelectBase<SOOrderRelatedReturnsSPResultLine>) this.ReturnsByLine).Locate(returnsSpResultLine4);
      if (returnsSpResultLine5 == null)
      {
        returnsSpResultLine4.InventoryID = arTran.InventoryID;
        returnsSpResultLine4.BaseUnit = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, arTran.InventoryID).BaseUnit;
        returnsSpResultLine4.ReturnedQty = new Decimal?(0M);
        GraphHelper.Hold(((PXSelectBase) this.ReturnsByLine).Cache, (object) returnsSpResultLine4);
      }
      else
        returnsSpResultLine4 = returnsSpResultLine5;
      SOOrderRelatedReturnsSPResultLine returnsSpResultLine6 = returnsSpResultLine4;
      Decimal? returnedQty = returnsSpResultLine6.ReturnedQty;
      short? invtMult = arTran.InvtMult;
      Decimal? nullable4 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable5 = arTran.BaseQty;
      Decimal? nullable6 = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * nullable5.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable7;
      if (!(returnedQty.HasValue & nullable6.HasValue))
      {
        nullable5 = new Decimal?();
        nullable7 = nullable5;
      }
      else
        nullable7 = new Decimal?(returnedQty.GetValueOrDefault() + nullable6.GetValueOrDefault());
      returnsSpResultLine6.ReturnedQty = nullable7;
    }
    return ((PXSelectBase) this.ReturnsByLine).Cache.Cached.Cast<SOOrderRelatedReturnsSPResultLine>();
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SOOrderRelatedReturnsSPFilter> e)
  {
    this.ClearCaches();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<SOOrderRelatedReturnsSPFilter.orderType> e)
  {
    this.ClearCaches();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<SOOrderRelatedReturnsSPFilter.orderNbr> e)
  {
    this.ClearCaches();
  }

  private void ClearCaches()
  {
    ((PXSelectBase) this.RelatedReturnDocuments).Cache.Clear();
    ((PXSelectBase) this.RelatedReturnDocuments).View.Clear();
    ((PXSelectBase) this.ReturnsByLine).Cache.Clear();
    ((PXSelectBase) this.ReturnsByLine).View.Clear();
    ((PXSelectBase) this.ShippedReturns).Cache.Clear();
    ((PXSelectBase) this.ShippedReturns).View.Clear();
  }

  private bool IsDuplicatedMiscLinesOnly(
    SOReturnShipped returnShipped,
    SOReturnShipped[] soReturns,
    SOOrderRelatedReturnsSPResultLine[] returnsByLine)
  {
    Lazy<bool> lazy = Lazy.By<bool>((Func<bool>) (() => ((IEnumerable<SOOrderRelatedReturnsSPResultLine>) returnsByLine).Any<SOOrderRelatedReturnsSPResultLine>((Func<SOOrderRelatedReturnsSPResultLine, bool>) (l => l.ReturnOrderType == returnShipped.OrderType && l.ReturnOrderNbr == returnShipped.OrderNbr && l.ReturnLineType == "MI"))));
    if (!string.IsNullOrEmpty(returnShipped.OrderNbr) && !returnShipped.ShippingRefNoteID.HasValue && ((IEnumerable<SOReturnShipped>) soReturns).Any<SOReturnShipped>((Func<SOReturnShipped, bool>) (r => r.OrderType == returnShipped.OrderType && r.OrderNbr == returnShipped.OrderNbr && r.ShippingRefNoteID.HasValue && string.IsNullOrEmpty(r.InvoiceNbr))) && lazy.Value)
    {
      if (PXResultset<SOLine>.op_Implicit(PXSelectBase<SOLine, PXViewOf<SOLine>.BasedOn<SelectFromBase<SOLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<SOShipLine>.On<SOShipLine.FK.OrderLine>>, FbqlJoins.Left<PX.Objects.PO.POReceiptLine>.On<PX.Objects.PO.POReceiptLine.FK.SOLine>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLine.orderType, Equal<BqlField<SOReturnShipped.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<SOLine.orderNbr, IBqlString>.IsEqual<BqlField<SOReturnShipped.orderNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<SOLine.origOrderType, IBqlString>.IsEqual<BqlField<SOReturnShipped.origOrderType, IBqlString>.FromCurrent>>>, And<BqlOperand<SOLine.origOrderNbr, IBqlString>.IsEqual<BqlField<SOReturnShipped.origOrderNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<SOLine.lineType, IBqlString>.IsNotEqual<SOLineType.miscCharge>>>, And<BqlOperand<SOShipLine.shipmentNbr, IBqlString>.IsNull>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.IsNull>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) new SOReturnShipped[1]
      {
        returnShipped
      }, Array.Empty<object>())) == null)
        return true;
    }
    if (!string.IsNullOrEmpty(returnShipped.OrderNbr) && returnShipped.ShipmentType == "H" && !string.IsNullOrEmpty(returnShipped.ShipmentNbr) && string.IsNullOrEmpty(returnShipped.APRefNbr) && ((IEnumerable<SOReturnShipped>) soReturns).Any<SOReturnShipped>((Func<SOReturnShipped, bool>) (r => r.OrderType == returnShipped.OrderType && r.OrderNbr == returnShipped.OrderNbr && r.ShipmentType == "H" && r.ShipmentNbr == returnShipped.ShipmentNbr && !string.IsNullOrEmpty(r.APRefNbr))) && lazy.Value)
    {
      if (PXResultset<SOLine>.op_Implicit(PXSelectBase<SOLine, PXViewOf<SOLine>.BasedOn<SelectFromBase<SOLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.PO.POReceiptLine>.On<PX.Objects.PO.POReceiptLine.FK.SOLine>>, FbqlJoins.Left<PX.Objects.AP.APTran>.On<PX.Objects.AP.APTran.FK.POReceiptLine>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLine.orderType, Equal<BqlField<SOReturnShipped.orderType, IBqlString>.FromCurrent>>>>, And<BqlOperand<SOLine.orderNbr, IBqlString>.IsEqual<BqlField<SOReturnShipped.orderNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<SOLine.origOrderType, IBqlString>.IsEqual<BqlField<SOReturnShipped.origOrderType, IBqlString>.FromCurrent>>>, And<BqlOperand<SOLine.origOrderNbr, IBqlString>.IsEqual<BqlField<SOReturnShipped.origOrderNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<SOLine.lineType, IBqlString>.IsNotEqual<SOLineType.miscCharge>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptType, IBqlString>.IsEqual<POReceiptType.poreturn>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.IsEqual<BqlField<SOReturnShipped.shipmentNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.AP.APTran.refNbr, IBqlString>.IsNull>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) this, (object[]) new SOReturnShipped[1]
      {
        returnShipped
      }, Array.Empty<object>())) == null)
        return true;
    }
    return false;
  }
}
