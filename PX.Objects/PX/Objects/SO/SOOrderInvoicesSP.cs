// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderInvoicesSP
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.SO;

[TableAndChartDashboardType]
public class SOOrderInvoicesSP : PXGraph<
#nullable disable
SOOrderInvoicesSP>
{
  public PXCancel<SOOrderInvoicesSPFilter> Cancel;
  public PXFilter<SOOrderInvoicesSPFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXViewOf<SOOrderInvoicesSPInqResult>.BasedOn<SelectFromBase<SOOrderInvoicesSPInqResult, TypeArrayOf<IFbqlJoin>.Empty>.Order<By<BqlField<
  #nullable enable
  SOOrderInvoicesSPInqResult.refNbr, IBqlString>.Desc, 
  #nullable disable
  BqlField<
  #nullable enable
  SOOrderInvoicesSPInqResult.orderNbr, IBqlString>.Desc>>>.ReadOnly Invoices;

  protected virtual 
  #nullable disable
  IEnumerable invoices()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultFiltered = true,
      IsResultTruncated = false,
      IsResultSorted = true
    };
    if (string.IsNullOrEmpty(((PXSelectBase<SOOrderInvoicesSPFilter>) this.Filter).Current.OrderNbr))
      return (IEnumerable) pxDelegateResult;
    SOOrder order = SOOrder.PK.Find((PXGraph) this, ((PXSelectBase<SOOrderInvoicesSPFilter>) this.Filter).Current.OrderType, ((PXSelectBase<SOOrderInvoicesSPFilter>) this.Filter).Current.OrderNbr);
    HashSet<SOOrderInvoicesSPInqResult> collection = new HashSet<SOOrderInvoicesSPInqResult>((IEqualityComparer<SOOrderInvoicesSPInqResult>) PXCacheEx.GetComparer(((PXSelectBase) this.Invoices).Cache));
    Type[] typeArray = new Type[12]
    {
      typeof (PX.Objects.AR.ARInvoice.docType),
      typeof (PX.Objects.AR.ARInvoice.refNbr),
      typeof (PX.Objects.AR.ARInvoice.status),
      typeof (PX.Objects.AR.ARInvoice.docDate),
      typeof (PX.Objects.AR.ARInvoice.dueDate),
      typeof (PX.Objects.AR.ARInvoice.finPeriodID),
      typeof (PX.Objects.AR.ARInvoice.curyOrigDocAmt),
      typeof (PX.Objects.AR.ARInvoice.curyDocBal),
      typeof (PX.Objects.AR.ARInvoice.curyID),
      typeof (PX.Objects.AR.ARInvoice.customerID),
      typeof (ARTran.sOOrderType),
      typeof (ARTran.sOOrderNbr)
    };
    bool flag1 = false;
    PXSelectBase<PX.Objects.AR.ARInvoice> invoicesQuery = this.GetInvoicesQuery(order);
    using (new PXFieldScope(((PXSelectBase) invoicesQuery).View, typeArray))
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      foreach (PXResult<PX.Objects.AR.ARInvoice> pxResult in ((PXSelectBase) invoicesQuery).View.Select((object[]) new SOOrder[1]
      {
        order
      }, (object[]) null, (object[]) null, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref num1, num2, ref num3).Cast<PXResult<PX.Objects.AR.ARInvoice>>())
      {
        ARTran arTran = ((PXResult) pxResult).GetItem<ARTran>();
        collection.Add(this.CreateUIRecord(PXResult<PX.Objects.AR.ARInvoice>.op_Implicit(pxResult), new SOOrder()
        {
          OrderType = arTran.SOOrderType,
          OrderNbr = arTran.SOOrderNbr
        }));
        flag1 = true;
      }
    }
    bool flag2 = false;
    if (EnumerableExtensions.IsIn<string>(order.Behavior, "RM", "CM", "BL"))
    {
      PXSelectBase<PX.Objects.AR.ARInvoice> returnsQuery = this.GetReturnsQuery(order);
      using (new PXFieldScope(((PXSelectBase) returnsQuery).View, typeArray))
      {
        int num4 = 0;
        int num5 = 0;
        int num6 = 0;
        foreach (PXResult<PX.Objects.AR.ARInvoice> pxResult in ((PXSelectBase) returnsQuery).View.Select((object[]) new SOOrder[1]
        {
          order
        }, (object[]) null, (object[]) null, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref num4, num5, ref num6).Cast<PXResult<PX.Objects.AR.ARInvoice>>())
        {
          ARTran arTran = ((PXResult) pxResult).GetItem<ARTran>();
          SOOrderInvoicesSPInqResult uiRecord = this.CreateUIRecord(PXResult<PX.Objects.AR.ARInvoice>.op_Implicit(pxResult), new SOOrder()
          {
            OrderType = arTran.SOOrderType,
            OrderNbr = arTran.SOOrderNbr
          });
          if (!collection.Contains(uiRecord))
          {
            collection.Add(uiRecord);
            flag2 = true;
          }
        }
      }
    }
    ((List<object>) pxDelegateResult).AddRange((IEnumerable<object>) collection);
    if (flag1 & flag2)
    {
      pxDelegateResult.IsResultFiltered = false;
      pxDelegateResult.IsResultSorted = false;
    }
    return (IEnumerable) pxDelegateResult;
  }

  protected PXSelectBase<PX.Objects.AR.ARInvoice> GetInvoicesQuery(SOOrder order)
  {
    PXViewOf<PX.Objects.AR.ARInvoice>.BasedOn<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ARTran>.On<ARTran.FK.Invoice>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleSO>>>>>.And<KeysRelation<CompositeKey<Field<ARTran.sOOrderType>.IsRelatedTo<SOOrder.orderType>, Field<ARTran.sOOrderNbr>.IsRelatedTo<SOOrder.orderNbr>>.WithTablesOf<SOOrder, ARTran>, SOOrder, ARTran>.SameAsCurrent>>.Aggregate<To<GroupBy<PX.Objects.AR.ARInvoice.docType, GroupBy<PX.Objects.AR.ARInvoice.refNbr>>>>.Order<By<BqlField<PX.Objects.AR.ARInvoice.refNbr, IBqlString>.Desc, BqlField<ARTran.sOOrderNbr, IBqlString>.Desc>>>.ReadOnly invoicesQuery = new PXViewOf<PX.Objects.AR.ARInvoice>.BasedOn<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ARTran>.On<ARTran.FK.Invoice>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleSO>>>>>.And<KeysRelation<CompositeKey<Field<ARTran.sOOrderType>.IsRelatedTo<SOOrder.orderType>, Field<ARTran.sOOrderNbr>.IsRelatedTo<SOOrder.orderNbr>>.WithTablesOf<SOOrder, ARTran>, SOOrder, ARTran>.SameAsCurrent>>.Aggregate<To<GroupBy<PX.Objects.AR.ARInvoice.docType, GroupBy<PX.Objects.AR.ARInvoice.refNbr>>>>.Order<By<BqlField<PX.Objects.AR.ARInvoice.refNbr, IBqlString>.Desc, BqlField<ARTran.sOOrderNbr, IBqlString>.Desc>>>.ReadOnly((PXGraph) this);
    if (order.Behavior == "BL")
    {
      ((PXSelectBase) invoicesQuery).View.Join<InnerJoin<SOBlanketOrderLink, On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOBlanketOrderLink.orderType, Equal<ARTran.sOOrderType>>>>>.And<BqlOperand<SOBlanketOrderLink.orderNbr, IBqlString>.IsEqual<ARTran.sOOrderNbr>>>>>();
      ((PXSelectBase) invoicesQuery).View.WhereNew<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleSO>>>>>.And<KeysRelation<CompositeKey<Field<SOBlanketOrderLink.blanketType>.IsRelatedTo<SOOrder.orderType>, Field<SOBlanketOrderLink.blanketNbr>.IsRelatedTo<SOOrder.orderNbr>>.WithTablesOf<SOOrder, SOBlanketOrderLink>, SOOrder, SOBlanketOrderLink>.SameAsCurrent>>>();
    }
    return (PXSelectBase<PX.Objects.AR.ARInvoice>) invoicesQuery;
  }

  protected PXSelectBase<PX.Objects.AR.ARInvoice> GetReturnsQuery(SOOrder order)
  {
    PXViewOf<PX.Objects.AR.ARInvoice>.BasedOn<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ARTran>.On<ARTran.FK.Invoice>>, FbqlJoins.Inner<SOLine>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLine.invoiceType, Equal<PX.Objects.AR.ARInvoice.docType>>>>>.And<BqlOperand<SOLine.invoiceNbr, IBqlString>.IsEqual<PX.Objects.AR.ARInvoice.refNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleSO>>>>>.And<KeysRelation<CompositeKey<Field<SOLine.orderType>.IsRelatedTo<SOOrder.orderType>, Field<SOLine.orderNbr>.IsRelatedTo<SOOrder.orderNbr>>.WithTablesOf<SOOrder, SOLine>, SOOrder, SOLine>.SameAsCurrent>>.Aggregate<To<GroupBy<PX.Objects.AR.ARInvoice.docType, GroupBy<PX.Objects.AR.ARInvoice.refNbr>>>>.Order<By<BqlField<PX.Objects.AR.ARInvoice.refNbr, IBqlString>.Desc, BqlField<ARTran.sOOrderNbr, IBqlString>.Desc>>>.ReadOnly returnsQuery = new PXViewOf<PX.Objects.AR.ARInvoice>.BasedOn<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ARTran>.On<ARTran.FK.Invoice>>, FbqlJoins.Inner<SOLine>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLine.invoiceType, Equal<PX.Objects.AR.ARInvoice.docType>>>>>.And<BqlOperand<SOLine.invoiceNbr, IBqlString>.IsEqual<PX.Objects.AR.ARInvoice.refNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleSO>>>>>.And<KeysRelation<CompositeKey<Field<SOLine.orderType>.IsRelatedTo<SOOrder.orderType>, Field<SOLine.orderNbr>.IsRelatedTo<SOOrder.orderNbr>>.WithTablesOf<SOOrder, SOLine>, SOOrder, SOLine>.SameAsCurrent>>.Aggregate<To<GroupBy<PX.Objects.AR.ARInvoice.docType, GroupBy<PX.Objects.AR.ARInvoice.refNbr>>>>.Order<By<BqlField<PX.Objects.AR.ARInvoice.refNbr, IBqlString>.Desc, BqlField<ARTran.sOOrderNbr, IBqlString>.Desc>>>.ReadOnly((PXGraph) this);
    if (order.Behavior == "BL")
    {
      ((PXSelectBase) returnsQuery).View.Join<InnerJoin<SOBlanketOrderLink, On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOBlanketOrderLink.orderType, Equal<ARTran.sOOrderType>>>>>.And<BqlOperand<SOBlanketOrderLink.orderNbr, IBqlString>.IsEqual<ARTran.sOOrderNbr>>>>>();
      ((PXSelectBase) returnsQuery).View.WhereNew<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.origModule, Equal<BatchModule.moduleSO>>>>>.And<KeysRelation<CompositeKey<Field<SOBlanketOrderLink.blanketType>.IsRelatedTo<SOOrder.orderType>, Field<SOBlanketOrderLink.blanketNbr>.IsRelatedTo<SOOrder.orderNbr>>.WithTablesOf<SOOrder, SOBlanketOrderLink>, SOOrder, SOBlanketOrderLink>.SameAsCurrent>>>();
    }
    return (PXSelectBase<PX.Objects.AR.ARInvoice>) returnsQuery;
  }

  protected virtual SOOrderInvoicesSPInqResult CreateUIRecord(PX.Objects.AR.ARInvoice invoice, SOOrder order)
  {
    return new SOOrderInvoicesSPInqResult()
    {
      DocType = invoice.DocType,
      RefNbr = invoice.RefNbr,
      Status = invoice.Status,
      DocDate = invoice.DocDate,
      CustomerID = invoice.CustomerID,
      DueDate = invoice.DueDate,
      FinPeriodID = invoice.FinPeriodID,
      CuryOrigDocAmt = invoice.CuryOrigDocAmt,
      CuryDocBal = invoice.CuryDocBal,
      CuryID = invoice.CuryID,
      OrderType = order.OrderType,
      OrderNbr = order.OrderNbr
    };
  }
}
