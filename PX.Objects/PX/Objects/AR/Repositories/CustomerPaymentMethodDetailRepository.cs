// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Repositories.CustomerPaymentMethodDetailRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.AR.Repositories;

public class CustomerPaymentMethodDetailRepository
{
  protected readonly PXGraph _graph;

  public CustomerPaymentMethodDetailRepository(PXGraph graph)
  {
    this._graph = graph != null ? graph : throw new ArgumentNullException(nameof (graph));
  }

  public CustomerPaymentMethodDetail GetCustomerPaymentMethodDetail(
    int? pMInstanceId,
    string detailID)
  {
    return PXResultset<CustomerPaymentMethodDetail>.op_Implicit(PXSelectBase<CustomerPaymentMethodDetail, PXSelect<CustomerPaymentMethodDetail, Where<CustomerPaymentMethodDetail.pMInstanceID, Equal<Required<CustomerPaymentMethodDetail.pMInstanceID>>, And<CustomerPaymentMethodDetail.detailID, Equal<Required<CustomerPaymentMethodDetail.detailID>>>>>.Config>.Select(this._graph, new object[2]
    {
      (object) pMInstanceId,
      (object) detailID
    }));
  }

  public void DeletePaymentMethodDetail(CustomerPaymentMethodDetail detail)
  {
    this._graph.Caches[typeof (CustomerPaymentMethodDetail)].Delete((object) detail);
  }
}
