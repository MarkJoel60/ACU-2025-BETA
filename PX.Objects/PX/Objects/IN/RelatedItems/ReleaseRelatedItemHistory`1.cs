// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.RelatedItems.ReleaseRelatedItemHistory`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable disable
namespace PX.Objects.IN.RelatedItems;

public class ReleaseRelatedItemHistory<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  public FbqlSelect<SelectFromBase<RelatedItemHistory, TypeArrayOf<IFbqlJoin>.Empty>, RelatedItemHistory>.View HistoryLines;

  protected virtual void ReleaseHistory(PX.Objects.AR.ARRegister ardoc)
  {
    this.ReleaseRelatedItemHistoryFromInvoice(ardoc);
    this.ReleaseRelatedItemHistoryFromOrder(ardoc);
  }

  protected virtual void ReleaseRelatedItemHistoryFromInvoice(PX.Objects.AR.ARRegister ardoc)
  {
    foreach (PXResult<PX.Objects.AR.ARTran, RelatedItemHistory> pxResult in ((PXSelectBase<PX.Objects.AR.ARTran>) new FbqlSelect<SelectFromBase<PX.Objects.AR.ARTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<RelatedItemHistory>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<CompositeKey<Field<RelatedItemHistory.invoiceDocType>.IsRelatedTo<PX.Objects.AR.ARTran.tranType>, Field<RelatedItemHistory.invoiceRefNbr>.IsRelatedTo<PX.Objects.AR.ARTran.refNbr>, Field<RelatedItemHistory.relatedInvoiceLineNbr>.IsRelatedTo<PX.Objects.AR.ARTran.lineNbr>>.WithTablesOf<PX.Objects.AR.ARTran, RelatedItemHistory>>, And<BqlOperand<RelatedItemHistory.isDraft, IBqlBool>.IsEqual<True>>>, And<BqlOperand<PX.Objects.AR.ARTran.sOOrderNbr, IBqlString>.IsNull>>, And<BqlOperand<PX.Objects.AR.ARTran.tranType, IBqlString>.IsEqual<P.AsString.ASCII>>>>.And<BqlOperand<PX.Objects.AR.ARTran.refNbr, IBqlString>.IsEqual<P.AsString>>>>>, PX.Objects.AR.ARTran>.View((PXGraph) this.Base)).Select(new object[2]
    {
      (object) ardoc.DocType,
      (object) ardoc.RefNbr
    }))
      this.ReleaseHistoryLine(PXResult<PX.Objects.AR.ARTran, RelatedItemHistory>.op_Implicit(pxResult), PXResult<PX.Objects.AR.ARTran, RelatedItemHistory>.op_Implicit(pxResult), ardoc.DocDate, false);
  }

  protected virtual void ReleaseRelatedItemHistoryFromOrder(PX.Objects.AR.ARRegister ardoc)
  {
    FbqlSelect<SelectFromBase<PX.Objects.AR.ARTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOLine>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<CompositeKey<Field<PX.Objects.AR.ARTran.sOOrderType>.IsRelatedTo<PX.Objects.SO.SOLine.orderType>, Field<PX.Objects.AR.ARTran.sOOrderNbr>.IsRelatedTo<PX.Objects.SO.SOLine.orderNbr>, Field<PX.Objects.AR.ARTran.sOOrderLineNbr>.IsRelatedTo<PX.Objects.SO.SOLine.lineNbr>>.WithTablesOf<PX.Objects.SO.SOLine, PX.Objects.AR.ARTran>>, And<BqlOperand<PX.Objects.AR.ARTran.tranType, IBqlString>.IsEqual<P.AsString.ASCII>>>>.And<BqlOperand<PX.Objects.AR.ARTran.refNbr, IBqlString>.IsEqual<P.AsString>>>>, FbqlJoins.Inner<PX.Objects.SO.SOOrder>.On<PX.Objects.SO.SOLine.FK.Order>>, FbqlJoins.Inner<RelatedItemHistory>.On<RelatedItemHistory.FK.RelatedSalesOrderLine>>>, PX.Objects.AR.ARTran>.View view = new FbqlSelect<SelectFromBase<PX.Objects.AR.ARTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.SO.SOLine>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<CompositeKey<Field<PX.Objects.AR.ARTran.sOOrderType>.IsRelatedTo<PX.Objects.SO.SOLine.orderType>, Field<PX.Objects.AR.ARTran.sOOrderNbr>.IsRelatedTo<PX.Objects.SO.SOLine.orderNbr>, Field<PX.Objects.AR.ARTran.sOOrderLineNbr>.IsRelatedTo<PX.Objects.SO.SOLine.lineNbr>>.WithTablesOf<PX.Objects.SO.SOLine, PX.Objects.AR.ARTran>>, And<BqlOperand<PX.Objects.AR.ARTran.tranType, IBqlString>.IsEqual<P.AsString.ASCII>>>>.And<BqlOperand<PX.Objects.AR.ARTran.refNbr, IBqlString>.IsEqual<P.AsString>>>>, FbqlJoins.Inner<PX.Objects.SO.SOOrder>.On<PX.Objects.SO.SOLine.FK.Order>>, FbqlJoins.Inner<RelatedItemHistory>.On<RelatedItemHistory.FK.RelatedSalesOrderLine>>>, PX.Objects.AR.ARTran>.View((PXGraph) this.Base);
    using (new PXFieldScope(((PXSelectBase) view).View, new Type[3]
    {
      typeof (PX.Objects.AR.ARTran),
      typeof (RelatedItemHistory),
      typeof (PX.Objects.SO.SOOrder.orderDate)
    }))
    {
      foreach (PXResult<PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, PX.Objects.SO.SOOrder, RelatedItemHistory> pxResult in ((PXSelectBase<PX.Objects.AR.ARTran>) view).Select(new object[2]
      {
        (object) ardoc.DocType,
        (object) ardoc.RefNbr
      }))
      {
        RelatedItemHistory relatedItemHistory = PXResult<PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, PX.Objects.SO.SOOrder, RelatedItemHistory>.op_Implicit(pxResult);
        this.ReleaseHistoryLine((RelatedItemHistory) ((PXSelectBase) this.HistoryLines).Cache.Locate((object) relatedItemHistory) ?? relatedItemHistory, PXResult<PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, PX.Objects.SO.SOOrder, RelatedItemHistory>.op_Implicit(pxResult), PXResult<PX.Objects.AR.ARTran, PX.Objects.SO.SOLine, PX.Objects.SO.SOOrder, RelatedItemHistory>.op_Implicit(pxResult).OrderDate, ardoc.IsCancellation.GetValueOrDefault());
      }
    }
  }

  protected virtual void ReleaseHistoryLine(
    RelatedItemHistory historyLine,
    PX.Objects.AR.ARTran tran,
    DateTime? documentDate,
    bool revert)
  {
    Decimal? nullable1;
    Decimal? nullable2;
    if (!(historyLine.RelatedInventoryUOM == tran.UOM))
    {
      PXCache cache = ((PXSelectBase) this.HistoryLines).Cache;
      int? relatedInventoryId = historyLine.RelatedInventoryID;
      string relatedInventoryUom = historyLine.RelatedInventoryUOM;
      nullable1 = tran.BaseQty;
      Decimal valueOrDefault = nullable1.GetValueOrDefault();
      nullable2 = new Decimal?(INUnitAttribute.ConvertFromBase(cache, relatedInventoryId, relatedInventoryUom, valueOrDefault, INPrecision.QUANTITY));
    }
    else
      nullable2 = tran.Qty;
    Decimal valueOrDefault1 = historyLine.SoldQty.GetValueOrDefault();
    Sign sign = Sign.MinusIf(revert);
    Decimal? nullable3 = nullable2;
    nullable1 = nullable3.HasValue ? new Decimal?(Sign.op_Multiply(sign, nullable3.GetValueOrDefault())) : new Decimal?();
    Decimal? nullable4;
    if (!nullable1.HasValue)
    {
      nullable3 = new Decimal?();
      nullable4 = nullable3;
    }
    else
      nullable4 = new Decimal?(valueOrDefault1 + nullable1.GetValueOrDefault());
    Decimal? nullable5 = nullable4;
    nullable1 = nullable5;
    Decimal num = 0M;
    if (nullable1.GetValueOrDefault() <= num & nullable1.HasValue)
    {
      if (historyLine.IsDraft.GetValueOrDefault())
        return;
      historyLine.IsDraft = new bool?(true);
    }
    else
      historyLine.IsDraft = new bool?(false);
    if (!revert)
      historyLine.DocumentDate = documentDate;
    historyLine.SoldQty = nullable5;
    ((PXSelectBase<RelatedItemHistory>) this.HistoryLines).Update(historyLine);
  }
}
