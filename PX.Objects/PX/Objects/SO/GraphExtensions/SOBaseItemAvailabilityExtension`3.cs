// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOBaseItemAvailabilityExtension`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.SO.DAC.Projections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.SO.GraphExtensions;

public abstract class SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit> : 
  ItemAvailabilityExtension<
  #nullable disable
  TGraph, TLine, TSplit>
  where TGraph : PXGraph
  where TLine : class, IBqlTable, ILSPrimary, new()
  where TSplit : class, IBqlTable, ILSDetail, new()
{
  public virtual SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnedQtyResult MemoCheckQty(
    int? inventoryID,
    string arDocType,
    string arRefNbr,
    int? arTranLineNbr,
    string orderType,
    string orderNbr,
    int? orderLineNbr,
    InvoiceSplit split = null)
  {
    SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnedQtyResult returnedQtyResult = new SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnedQtyResult(true);
    bool flag1 = orderType != null && orderNbr != null && orderLineNbr.HasValue;
    bool flag2 = arDocType != null && arRefNbr != null && arTranLineNbr.HasValue;
    if (!flag1 && !flag2)
      return returnedQtyResult;
    IEnumerable<InvoiceSplit> invoiceSplits1;
    if (split != null)
    {
      bool? isKit = split.IsKit;
      bool flag3 = false;
      if (isKit.GetValueOrDefault() == flag3 & isKit.HasValue)
      {
        invoiceSplits1 = EnumerableExtensions.AsSingleEnumerable<InvoiceSplit>(split);
        goto label_6;
      }
    }
    invoiceSplits1 = this.SelectInvoicedRecords(arDocType, arRefNbr, arTranLineNbr);
label_6:
    IEnumerable<InvoiceSplit> invoiceSplits2 = invoiceSplits1;
    PX.Objects.SO.SOLine[] source1 = this.SelectReturnSOLines(arDocType, arRefNbr, arTranLineNbr);
    IEnumerable<PX.Objects.AR.ARTran> source2 = this.SelectReturnARTrans(arDocType, arRefNbr);
    if (flag1)
    {
      IEnumerable<InvoiceSplit> invoiced = invoiceSplits2.Where<InvoiceSplit>((Func<InvoiceSplit, bool>) (r =>
      {
        if (!(r.SOOrderType == orderType) || !(r.SOOrderNbr == orderNbr))
          return false;
        int? soLineNbr = r.SOLineNbr;
        int? nullable = orderLineNbr;
        return soLineNbr.GetValueOrDefault() == nullable.GetValueOrDefault() & soLineNbr.HasValue == nullable.HasValue;
      }));
      IEnumerable<SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord> returned = ((IEnumerable<PX.Objects.SO.SOLine>) source1).Where<PX.Objects.SO.SOLine>((Func<PX.Objects.SO.SOLine, bool>) (l =>
      {
        if (!(l.OrigOrderType == orderType) || !(l.OrigOrderNbr == orderNbr))
          return false;
        int? origLineNbr = l.OrigLineNbr;
        int? nullable = orderLineNbr;
        return origLineNbr.GetValueOrDefault() == nullable.GetValueOrDefault() & origLineNbr.HasValue == nullable.HasValue;
      })).Select<PX.Objects.SO.SOLine, SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord>(new Func<PX.Objects.SO.SOLine, SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord>(SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord.FromSOLine));
      returnedQtyResult = this.CheckInvoicedAndReturnedQty(inventoryID, invoiced, returned);
    }
    if (returnedQtyResult.Success & flag2)
    {
      IEnumerable<SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord> returned = source2.Where<PX.Objects.AR.ARTran>((Func<PX.Objects.AR.ARTran, bool>) (t =>
      {
        int? origInvoiceLineNbr = t.OrigInvoiceLineNbr;
        int? nullable = arTranLineNbr;
        return origInvoiceLineNbr.GetValueOrDefault() == nullable.GetValueOrDefault() & origInvoiceLineNbr.HasValue == nullable.HasValue;
      })).Select<PX.Objects.AR.ARTran, SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord>(new Func<PX.Objects.AR.ARTran, SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord>(SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord.FromARTran)).Concat<SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord>(((IEnumerable<PX.Objects.SO.SOLine>) source1).Where<PX.Objects.SO.SOLine>((Func<PX.Objects.SO.SOLine, bool>) (l =>
      {
        int? invoiceLineNbr = l.InvoiceLineNbr;
        int? nullable = arTranLineNbr;
        return invoiceLineNbr.GetValueOrDefault() == nullable.GetValueOrDefault() & invoiceLineNbr.HasValue == nullable.HasValue;
      })).Select<PX.Objects.SO.SOLine, SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord>(new Func<PX.Objects.SO.SOLine, SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord>(SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord.FromSOLine)));
      returnedQtyResult = this.CheckInvoicedAndReturnedQty(inventoryID, invoiceSplits2, returned);
    }
    return returnedQtyResult;
  }

  protected virtual PX.Objects.SO.SOLine[] SelectReturnSOLines(
    string arDocType,
    string arRefNbr,
    int? arTranLineNbr)
  {
    if (string.IsNullOrEmpty(arDocType) || string.IsNullOrEmpty(arRefNbr) || !arTranLineNbr.HasValue)
      return Array<PX.Objects.SO.SOLine>.Empty;
    PXViewOf<PX.Objects.SO.SOLine>.BasedOn<SelectFromBase<PX.Objects.SO.SOLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOOrder>.On<PX.Objects.SO.SOLine.FK.Order>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.invoiceType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<PX.Objects.SO.SOLine.invoiceNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<PX.Objects.SO.SOLine.invoiceLineNbr, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<PX.Objects.SO.SOLine.operation, IBqlString>.IsEqual<SOOperation.receipt>>>>.And<BqlOperand<PX.Objects.SO.SOOrder.cancelled, IBqlBool>.IsEqual<False>>>>.ReadOnly readOnly = new PXViewOf<PX.Objects.SO.SOLine>.BasedOn<SelectFromBase<PX.Objects.SO.SOLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOOrder>.On<PX.Objects.SO.SOLine.FK.Order>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.invoiceType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<PX.Objects.SO.SOLine.invoiceNbr, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<PX.Objects.SO.SOLine.invoiceLineNbr, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<PX.Objects.SO.SOLine.operation, IBqlString>.IsEqual<SOOperation.receipt>>>>.And<BqlOperand<PX.Objects.SO.SOOrder.cancelled, IBqlBool>.IsEqual<False>>>>.ReadOnly((PXGraph) this.Base);
    PX.Objects.SO.SOLine[] other;
    using (new PXFieldScope(((PXSelectBase) readOnly).View, new Type[1]
    {
      typeof (PX.Objects.SO.SOLine)
    }))
      other = ((PXSelectBase<PX.Objects.SO.SOLine>) readOnly).SelectMain(new object[3]
      {
        (object) arDocType,
        (object) arRefNbr,
        (object) arTranLineNbr
      });
    PXCache<PX.Objects.SO.SOLine> linesCache = GraphHelper.Caches<PX.Objects.SO.SOLine>((PXGraph) this.Base);
    if (((PXCache) linesCache).IsInsertedUpdatedDeleted)
    {
      Func<string, string, PX.Objects.SO.SOOrder> GetOrder = Func.Memorize<string, string, PX.Objects.SO.SOOrder>((Func<string, string, PX.Objects.SO.SOOrder>) ((orderType, orderNbr) => PXParentAttribute.SelectParent<PX.Objects.SO.SOOrder>((PXCache) linesCache, (object) new PX.Objects.SO.SOLine()
      {
        OrderType = orderType,
        OrderNbr = orderNbr
      }) ?? (PX.Objects.SO.SOOrder) ((PXCache) GraphHelper.Caches<PX.Objects.SO.SOOrder>((PXGraph) this.Base)).Current));
      HashSet<PX.Objects.SO.SOLine> source = new HashSet<PX.Objects.SO.SOLine>((IEqualityComparer<PX.Objects.SO.SOLine>) new KeyValuesComparer<PX.Objects.SO.SOLine>((PXCache) linesCache, (IEnumerable<Type>) ((PXCache) linesCache).BqlKeys));
      EnumerableExtensions.AddRange<PX.Objects.SO.SOLine>((ISet<PX.Objects.SO.SOLine>) source, (IEnumerable<PX.Objects.SO.SOLine>) GraphHelper.RowCast<PX.Objects.SO.SOLine>(NonGenericIEnumerableExtensions.Concat_(((PXCache) linesCache).Inserted, ((PXCache) linesCache).Updated)).Where<PX.Objects.SO.SOLine>(new Func<PX.Objects.SO.SOLine, bool>(IsReturnLine)).ToArray<PX.Objects.SO.SOLine>());
      source.UnionWith((IEnumerable<PX.Objects.SO.SOLine>) other);
      source.ExceptWith(GraphHelper.RowCast<PX.Objects.SO.SOLine>(((PXCache) linesCache).Deleted).Where<PX.Objects.SO.SOLine>(new Func<PX.Objects.SO.SOLine, bool>(IsReturnLine)));
      other = source.ToArray<PX.Objects.SO.SOLine>();

      bool IsReturnLine(PX.Objects.SO.SOLine line)
      {
        if (line.InvoiceType == arDocType && line.InvoiceNbr == arRefNbr)
        {
          int? invoiceLineNbr = line.InvoiceLineNbr;
          int? nullable = arTranLineNbr;
          if (invoiceLineNbr.GetValueOrDefault() == nullable.GetValueOrDefault() & invoiceLineNbr.HasValue == nullable.HasValue && line.Operation == "R")
          {
            PX.Objects.SO.SOOrder soOrder = GetOrder(line.OrderType, line.OrderNbr);
            if (soOrder == null)
              return false;
            bool? cancelled = soOrder.Cancelled;
            bool flag = false;
            return cancelled.GetValueOrDefault() == flag & cancelled.HasValue;
          }
        }
        return false;
      }
    }
    return other;
  }

  protected virtual IEnumerable<PX.Objects.AR.ARTran> SelectReturnARTrans(
    string arDocType,
    string arRefNbr)
  {
    if (string.IsNullOrEmpty(arDocType) || string.IsNullOrEmpty(arRefNbr))
      return (IEnumerable<PX.Objects.AR.ARTran>) Array<PX.Objects.AR.ARTran>.Empty;
    return GraphHelper.RowCast<PX.Objects.AR.ARTran>((IEnumerable) ((PXSelectBase<PX.Objects.AR.ARTran>) new FbqlSelect<SelectFromBase<PX.Objects.AR.ARTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.sOOrderNbr, IsNull>>>, And<BqlOperand<PX.Objects.AR.ARTran.origInvoiceType, IBqlString>.IsEqual<P.AsString.ASCII>>>, And<BqlOperand<PX.Objects.AR.ARTran.origInvoiceNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<Mult<PX.Objects.AR.ARTran.qty, PX.Objects.AR.ARTran.invtMult>, IBqlDecimal>.IsGreater<decimal0>>>, PX.Objects.AR.ARTran>.View((PXGraph) this.Base)).Select(new object[2]
    {
      (object) arDocType,
      (object) arRefNbr
    }));
  }

  public virtual IEnumerable<InvoiceSplit> SelectInvoicedRecords(
    string arDocType,
    string arRefNbr,
    int? arLineNbr)
  {
    return this.SelectInvoicedRecords(arDocType, arRefNbr, arLineNbr, false);
  }

  protected virtual IEnumerable<InvoiceSplit> SelectInvoicedRecords(
    string arDocType,
    string arRefNbr,
    int? arLineNbr,
    bool includeDirectLines)
  {
    PXViewOf<InvoiceSplitExpanded>.BasedOn<SelectFromBase<InvoiceSplitExpanded, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<InvoiceSplitExpanded.aRDocType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<InvoiceSplitExpanded.aRRefNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<InvoiceSplitExpanded.aRLineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly readOnly = new PXViewOf<InvoiceSplitExpanded>.BasedOn<SelectFromBase<InvoiceSplitExpanded, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<InvoiceSplitExpanded.aRDocType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<InvoiceSplitExpanded.aRRefNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<InvoiceSplitExpanded.aRLineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly((PXGraph) this.Base);
    if (!includeDirectLines)
      ((PXSelectBase<InvoiceSplitExpanded>) readOnly).WhereAnd<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<InvoiceSplitExpanded.aRlineType, Equal<InvoiceSplitExpanded.sOlineType>>>>>.And<BqlOperand<InvoiceSplitExpanded.sOOrderNbr, IBqlString>.IsNotNull>>>();
    return (IEnumerable<InvoiceSplit>) GraphHelper.RowCast<InvoiceSplitExpanded>((IEnumerable) ((PXSelectBase<InvoiceSplitExpanded>) readOnly).Select(new object[3]
    {
      (object) arDocType,
      (object) arRefNbr,
      (object) arLineNbr
    })).ToArray<InvoiceSplitExpanded>();
  }

  protected virtual SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnedQtyResult CheckInvoicedAndReturnedQty(
    int? returnInventoryID,
    IEnumerable<InvoiceSplit> invoiced,
    IEnumerable<SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord> returned)
  {
    if (!returnInventoryID.HasValue)
      return new SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnedQtyResult(true);
    int num1 = 0;
    Decimal num2 = 0M;
    Dictionary<int, Decimal> dictionary1 = new Dictionary<int, Decimal>();
    Dictionary<int, Decimal> dictionary2 = new Dictionary<int, Decimal>();
    foreach (IGrouping<(string, string, int?, int?, Decimal?, string, string), InvoiceSplit> source in invoiced.GroupBy<InvoiceSplit, (string, string, int?, int?, Decimal?, string, string)>((Func<InvoiceSplit, (string, string, int?, int?, Decimal?, string, string)>) (r => (r.ARDocType, r.ARRefNbr, r.ARLineNbr, r.InventoryID, r.ARTranQty, r.ARTranUOM, r.ARTranDrCr))))
    {
      num1 = source.Key.Item4.Value;
      Decimal num3 = (source.Key.Item7 == "D" ? -1M : 1M) * source.Key.Item5.Value;
      num2 += INUnitAttribute.ConvertToBase((PXCache) GraphHelper.Caches<PX.Objects.AR.ARTran>((PXGraph) this.Base), new int?(num1), source.Key.Item6, num3, INPrecision.QUANTITY);
      foreach (IGrouping<(string, string, int?, int?, string, Decimal?), InvoiceSplit> grouping in source.Where<InvoiceSplit>((Func<InvoiceSplit, bool>) (r => r.INTranQty.HasValue)).GroupBy<InvoiceSplit, (string, string, int?, int?, string, Decimal?)>((Func<InvoiceSplit, (string, string, int?, int?, string, Decimal?)>) (r => (r.INDocType, r.INRefNbr, r.INLineNbr, r.ComponentID, r.INTranUOM, r.INTranQty))))
      {
        int key = grouping.Key.Item4 ?? source.Key.Item4.Value;
        if (!dictionary1.ContainsKey(key))
          dictionary1[key] = 0M;
        dictionary1[key] += INUnitAttribute.ConvertToBase((PXCache) GraphHelper.Caches<INTran>((PXGraph) this.Base), new int?(key), grouping.Key.Item5, grouping.Key.Item6.Value, INPrecision.QUANTITY);
      }
    }
    Decimal invoiceQtySign = num2 > 0M ? 1M : -1M;
    foreach (KeyValuePair<int, Decimal> keyValuePair in dictionary1)
      dictionary2[keyValuePair.Key] = keyValuePair.Value / num2;
    foreach (SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord returnRecord in returned)
    {
      int? inventoryId = returnRecord.InventoryID;
      int num4 = num1;
      if (inventoryId.GetValueOrDefault() == num4 & inventoryId.HasValue || dictionary1.Count == 0)
      {
        Decimal num5 = INUnitAttribute.ConvertToBase((PXCache) this.LineCache, returnRecord.InventoryID, returnRecord.UOM, returnRecord.Qty, INPrecision.QUANTITY);
        num2 -= num5;
        if (PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, returnRecord.InventoryID).KitItem.GetValueOrDefault())
        {
          foreach (KeyValuePair<int, Decimal> keyValuePair in dictionary2)
            dictionary1[keyValuePair.Key] -= dictionary2[keyValuePair.Key] * num5;
        }
      }
      else
        dictionary1[returnRecord.InventoryID.Value] -= INUnitAttribute.ConvertToBase((PXCache) this.LineCache, returnRecord.InventoryID, returnRecord.UOM, returnRecord.Qty, INPrecision.QUANTITY);
    }
    bool success = true;
    int? nullable = returnInventoryID;
    int num6 = num1;
    Decimal num7;
    if (nullable.GetValueOrDefault() == num6 & nullable.HasValue)
    {
      if (invoiceQtySign * num2 < 0M || dictionary1.Values.Any<Decimal>((Func<Decimal, bool>) (v => invoiceQtySign * v < 0M)))
        success = false;
      num7 = invoiceQtySign * num2;
      foreach (KeyValuePair<int, Decimal> keyValuePair in dictionary1)
      {
        int key = keyValuePair.Key;
        Decimal num8 = keyValuePair.Value * invoiceQtySign;
        Decimal num9;
        if (dictionary2.TryGetValue(key, out num9))
          num7 = Math.Min(num7, num8 / num9);
      }
    }
    else
    {
      if (invoiceQtySign * num2 < 0M)
        success = false;
      num7 = invoiceQtySign * num2;
      Decimal num10;
      if (dictionary1.TryGetValue(returnInventoryID.Value, out num10))
      {
        if (invoiceQtySign * num10 < 0M)
          success = false;
        num7 = invoiceQtySign * num10;
      }
    }
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, returnInventoryID);
    Decimal num11 = !inventoryItem.DecimalBaseUnit.GetValueOrDefault() || !inventoryItem.DecimalSalesUnit.GetValueOrDefault() ? Decimal.Truncate(num7) : Math.Round(num7, CommonSetupDecPl.Qty, MidpointRounding.AwayFromZero);
    return new SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnedQtyResult(success, success ? (SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord[]) null : returned.ToArray<SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord>(), new Decimal?(num11));
  }

  [PXInternalUseOnly]
  public abstract class ReturnRecord
  {
    public abstract int? InventoryID { get; }

    public abstract string UOM { get; }

    public abstract Decimal Qty { get; }

    public abstract string DocumentNbr { get; }

    public static SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord FromSOLine(
      PX.Objects.SO.SOLine l)
    {
      return (SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord) new SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord.ReturnSOLine(l);
    }

    public static SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord FromARTran(
      PX.Objects.AR.ARTran t)
    {
      return (SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord) new SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord.ReturnARTran(t);
    }

    private class ReturnSOLine : SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord
    {
      public ReturnSOLine(PX.Objects.SO.SOLine line) => this.Line = line;

      public PX.Objects.SO.SOLine Line { get; }

      public override string DocumentNbr => this.Line.OrderNbr;

      public override int? InventoryID => this.Line.InventoryID;

      public override string UOM => this.Line.UOM;

      public override Decimal Qty
      {
        get
        {
          short? invtMult = this.Line.InvtMult;
          Decimal? nullable1 = invtMult.HasValue ? new Decimal?((Decimal) invtMult.GetValueOrDefault()) : new Decimal?();
          Decimal? nullable2 = !(this.Line.LineType != "MI") || !this.Line.RequireShipping.GetValueOrDefault() || !this.Line.Completed.GetValueOrDefault() ? this.Line.OrderQty : this.Line.ShippedQty;
          return (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * nullable2.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
        }
      }
    }

    private class ReturnARTran : SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord
    {
      public ReturnARTran(PX.Objects.AR.ARTran tran) => this.Tran = tran;

      public PX.Objects.AR.ARTran Tran { get; }

      public override string DocumentNbr => this.Tran.RefNbr;

      public override int? InventoryID => this.Tran.InventoryID;

      public override string UOM => this.Tran.UOM;

      public override Decimal Qty => Math.Abs(this.Tran.Qty.GetValueOrDefault());
    }
  }

  public class ReturnedQtyResult
  {
    public ReturnedQtyResult(
      bool success,
      SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord[] returnRecords = null,
      Decimal? qtyAvailForReturn = null)
    {
      this.Success = success;
      this.ReturnRecords = returnRecords;
      this.QtyAvailForReturn = qtyAvailForReturn;
    }

    public bool Success { get; private set; }

    public SOBaseItemAvailabilityExtension<TGraph, TLine, TSplit>.ReturnRecord[] ReturnRecords { get; private set; }

    public Decimal? QtyAvailForReturn { get; private set; }
  }
}
