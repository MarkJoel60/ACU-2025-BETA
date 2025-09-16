// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Repositories.CustomerPaymentMethodRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.Repositories;

public class CustomerPaymentMethodRepository
{
  protected readonly PXGraph _graph;

  public CustomerPaymentMethodRepository(PXGraph graph)
  {
    this._graph = graph != null ? graph : throw new ArgumentNullException(nameof (graph));
  }

  public PX.Objects.AR.CustomerPaymentMethod UpdateCustomerPaymentMethod(
    PX.Objects.AR.CustomerPaymentMethod paymentMethod)
  {
    return (PX.Objects.AR.CustomerPaymentMethod) this._graph.Caches[typeof (PX.Objects.AR.CustomerPaymentMethod)].Update((object) paymentMethod);
  }

  public PXResult<PX.Objects.AR.CustomerPaymentMethod, PX.Objects.AR.Customer> FindCustomerAndPaymentMethod(
    int? pMInstanceID)
  {
    return (PXResult<PX.Objects.AR.CustomerPaymentMethod, PX.Objects.AR.Customer>) PXResultset<PX.Objects.AR.CustomerPaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.AR.CustomerPaymentMethod, PXSelectJoin<PX.Objects.AR.CustomerPaymentMethod, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PX.Objects.AR.CustomerPaymentMethod.bAccountID>>>, Where<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>.Config>.Select(this._graph, new object[1]
    {
      (object) pMInstanceID
    }));
  }

  public Tuple<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail> GetCustomerPaymentMethodWithProfileDetail(
    string procCenter,
    string customerProfile,
    string paymentProfile)
  {
    foreach (PXResult<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail> pxResult in ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) new PXSelectReadonly2<PX.Objects.AR.CustomerPaymentMethod, InnerJoin<CustomerPaymentMethodDetail, On<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<CustomerPaymentMethodDetail.pMInstanceID>>, InnerJoin<PaymentMethodDetail, On<CustomerPaymentMethodDetail.detailID, Equal<PaymentMethodDetail.detailID>, And<CustomerPaymentMethodDetail.paymentMethodID, Equal<PaymentMethodDetail.paymentMethodID>>>>>, Where<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID>>, And<PX.Objects.AR.CustomerPaymentMethod.customerCCPID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.customerCCPID>>, And<PaymentMethodDetail.isCCProcessingID, Equal<True>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>>, OrderBy<Desc<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>(this._graph)).Select(new object[2]
    {
      (object) procCenter,
      (object) customerProfile
    }))
    {
      CustomerPaymentMethodDetail paymentMethodDetail = PXResult<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail>.op_Implicit(pxResult);
      if (paymentMethodDetail.Value == paymentProfile)
        return new Tuple<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail>(PXResult<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail>.op_Implicit(pxResult), paymentMethodDetail);
    }
    return (Tuple<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail>) null;
  }

  public CustomerProcessingCenterID GetCustomerProcessingCenterByAccountAndProcCenterIDs(
    int? bAccountId,
    string procCenterId)
  {
    return PXResultset<CustomerProcessingCenterID>.op_Implicit(PXSelectBase<CustomerProcessingCenterID, PXSelect<CustomerProcessingCenterID, Where<CustomerProcessingCenterID.bAccountID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.bAccountID>>, And<CustomerProcessingCenterID.cCProcessingCenterID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.cCProcessingCenterID>>>>, OrderBy<Desc<CustomerProcessingCenterID.createdDateTime>>>.Config>.Select(this._graph, new object[2]
    {
      (object) bAccountId,
      (object) procCenterId
    }));
  }

  public Tuple<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail> GetCustomerPaymentMethodWithProfileDetail(
    int? pmInstanceId)
  {
    using (IEnumerator<PXResult<PX.Objects.AR.CustomerPaymentMethod>> enumerator = ((PXSelectBase<PX.Objects.AR.CustomerPaymentMethod>) new PXSelectReadonly2<PX.Objects.AR.CustomerPaymentMethod, InnerJoin<CustomerPaymentMethodDetail, On<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<CustomerPaymentMethodDetail.pMInstanceID>>, InnerJoin<PaymentMethodDetail, On<CustomerPaymentMethodDetail.detailID, Equal<PaymentMethodDetail.detailID>, And<CustomerPaymentMethodDetail.paymentMethodID, Equal<PaymentMethodDetail.paymentMethodID>>>>>, Where<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<Required<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>, And<PaymentMethodDetail.isCCProcessingID, Equal<True>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>>(this._graph)).Select(new object[1]
    {
      (object) pmInstanceId
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXResult<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail> current = (PXResult<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail>) enumerator.Current;
        CustomerPaymentMethodDetail paymentMethodDetail = PXResult<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail>.op_Implicit(current);
        return new Tuple<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail>(PXResult<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail>.op_Implicit(current), paymentMethodDetail);
      }
    }
    return (Tuple<PX.Objects.AR.CustomerPaymentMethod, CustomerPaymentMethodDetail>) null;
  }
}
