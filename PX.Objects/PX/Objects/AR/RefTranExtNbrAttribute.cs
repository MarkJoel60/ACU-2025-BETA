// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.RefTranExtNbrAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.AR;

public class RefTranExtNbrAttribute : PXCustomSelectorAttribute
{
  public const string PaymentMethod = "PaymentMethodID";
  public const string CustomerIdField = "CustomerID";

  public RefTranExtNbrAttribute()
    : base(typeof (Search3<ExternalTransaction.tranNumber, InnerJoin<ARPayment, On<ExternalTransaction.docType, Equal<ARPayment.docType>, And<ExternalTransaction.refNbr, Equal<ARPayment.refNbr>>>, LeftJoin<CustomerPaymentMethod, On<ExternalTransaction.pMInstanceID, Equal<CustomerPaymentMethod.pMInstanceID>>>>, OrderBy<Desc<ExternalTransaction.tranNumber>>>), new Type[6]
    {
      typeof (ExternalTransaction.docType),
      typeof (ExternalTransaction.refNbr),
      typeof (ARPayment.docDate),
      typeof (ExternalTransaction.amount),
      typeof (ExternalTransaction.tranNumber),
      typeof (CustomerPaymentMethod.descr)
    })
  {
    ((PXSelectorAttribute) this).ValidateValue = true;
  }

  public static ExternalTransaction GetStoredTran(string tranNbr, PXGraph graph, PXCache cache)
  {
    if (string.IsNullOrEmpty(tranNbr) || graph == null || cache == null)
      return (ExternalTransaction) null;
    int? nullable = cache.GetValue(cache.Current, "CustomerID") as int?;
    string str = cache.GetValue(cache.Current, "PaymentMethodID") as string;
    return ((PXSelectBase<ExternalTransaction>) new PXSelectJoin<ExternalTransaction, InnerJoin<ARPayment, On<ExternalTransaction.docType, Equal<ARPayment.docType>, And<ExternalTransaction.refNbr, Equal<ARPayment.refNbr>>>>, Where<ExternalTransaction.procStatus, Equal<ExtTransactionProcStatusCode.captureSuccess>, And<ExternalTransaction.tranNumber, Equal<Required<ExternalTransaction.tranNumber>>, And<ARPayment.customerID, Equal<Required<ARPayment.customerID>>, And<ARPayment.paymentMethodID, Equal<Required<ARPayment.paymentMethodID>>>>>>, OrderBy<Desc<ExternalTransaction.transactionID>>>(graph)).SelectSingle(new object[3]
    {
      (object) tranNbr,
      (object) nullable,
      (object) str
    });
  }

  protected virtual IEnumerable GetRecords()
  {
    RefTranExtNbrAttribute tranExtNbrAttribute = this;
    PXGraph graph = tranExtNbrAttribute._Graph;
    PXCache pxCache = (PXCache) null;
    if (graph != null)
      pxCache = GraphHelper.GetPrimaryCache(tranExtNbrAttribute._Graph);
    if (pxCache != null)
    {
      int? nullable = pxCache.GetValue(pxCache.Current, "CustomerID") as int?;
      string str = pxCache.GetValue(pxCache.Current, "PaymentMethodID") as string;
      PXSelectJoin<ExternalTransaction, InnerJoin<ARPayment, On<ExternalTransaction.docType, Equal<ARPayment.docType>, And<ExternalTransaction.refNbr, Equal<ARPayment.refNbr>>>, LeftJoin<CustomerPaymentMethod, On<ExternalTransaction.pMInstanceID, Equal<CustomerPaymentMethod.pMInstanceID>>>>, Where<ExternalTransaction.procStatus, Equal<ExtTransactionProcStatusCode.captureSuccess>, And<ARPayment.customerID, Equal<Required<ARPayment.customerID>>, And<ARPayment.paymentMethodID, Equal<Required<ARPayment.paymentMethodID>>>>>, OrderBy<Desc<ExternalTransaction.transactionID>>> pxSelectJoin = new PXSelectJoin<ExternalTransaction, InnerJoin<ARPayment, On<ExternalTransaction.docType, Equal<ARPayment.docType>, And<ExternalTransaction.refNbr, Equal<ARPayment.refNbr>>>, LeftJoin<CustomerPaymentMethod, On<ExternalTransaction.pMInstanceID, Equal<CustomerPaymentMethod.pMInstanceID>>>>, Where<ExternalTransaction.procStatus, Equal<ExtTransactionProcStatusCode.captureSuccess>, And<ARPayment.customerID, Equal<Required<ARPayment.customerID>>, And<ARPayment.paymentMethodID, Equal<Required<ARPayment.paymentMethodID>>>>>, OrderBy<Desc<ExternalTransaction.transactionID>>>(graph);
      object[] objArray = new object[2]
      {
        (object) nullable,
        (object) str
      };
      foreach (PXResult<ExternalTransaction, ARPayment, CustomerPaymentMethod> record in ((PXSelectBase<ExternalTransaction>) pxSelectJoin).SelectWithViewContext(objArray))
        yield return (object) record;
    }
  }
}
