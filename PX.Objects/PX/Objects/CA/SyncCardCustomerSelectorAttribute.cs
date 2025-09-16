// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.SyncCardCustomerSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class SyncCardCustomerSelectorAttribute : PXCustomSelectorAttribute
{
  public SyncCardCustomerSelectorAttribute(params Type[] fields)
    : base(typeof (PX.Objects.AR.Customer.bAccountID), fields)
  {
    ((PXSelectorAttribute) this).SubstituteKey = typeof (PX.Objects.AR.Customer.acctCD);
  }

  public IEnumerable GetRecords()
  {
    if (!(this._Graph is CCSynchronizeCards graph) || !(((PXGraph) graph).Caches[typeof (CCSynchronizeCard)].Current is CCSynchronizeCard current) || current.CCProcessingCenterID == null || current.CustomerCCPID == null)
      return this.GetAllCustomers();
    string customerCcpid = current.CustomerCCPID;
    IEnumerable<PX.Objects.AR.Customer> customersWithSameCcpid = this.GetCustomersWithSameCCPID(current.CCProcessingCenterID, customerCcpid);
    return !customersWithSameCcpid.Any<PX.Objects.AR.Customer>() ? this.GetAllCustomers() : (IEnumerable) customersWithSameCcpid;
  }

  private IEnumerable<PX.Objects.AR.Customer> GetCustomersWithSameCCPID(
    string processingCenterID,
    string customerCCPID)
  {
    PXSelectBase<PX.Objects.AR.Customer> pxSelectBase = (PXSelectBase<PX.Objects.AR.Customer>) new PXSelectReadonly2<PX.Objects.AR.Customer, InnerJoin<CustomerProcessingCenterID, On<PX.Objects.AR.Customer.bAccountID, Equal<CustomerProcessingCenterID.bAccountID>>>, Where<CustomerProcessingCenterID.cCProcessingCenterID, Equal<Required<CustomerProcessingCenterID.cCProcessingCenterID>>, And<CustomerProcessingCenterID.customerCCPID, Equal<Required<CustomerProcessingCenterID.customerCCPID>>>>>(this._Graph);
    foreach (PXRestrictorAttribute restrictorAttribute in this._Graph.Caches[typeof (CCSynchronizeCard)].GetAttributesReadonly(((PXEventSubscriberAttribute) this)._FieldName).OfType<PXRestrictorAttribute>())
      pxSelectBase.WhereAnd(restrictorAttribute.RestrictingCondition);
    return GraphHelper.RowCast<PX.Objects.AR.Customer>((IEnumerable) pxSelectBase.Select(new object[2]
    {
      (object) processingCenterID,
      (object) customerCCPID
    }));
  }

  private IEnumerable GetAllCustomers()
  {
    PXSelectBase<PX.Objects.AR.Customer> pxSelectBase = (PXSelectBase<PX.Objects.AR.Customer>) new PXSelectReadonly<PX.Objects.AR.Customer>(this._Graph);
    foreach (PXRestrictorAttribute restrictorAttribute in this._Graph.Caches[typeof (CCSynchronizeCard)].GetAttributesReadonly(((PXEventSubscriberAttribute) this)._FieldName).OfType<PXRestrictorAttribute>())
      pxSelectBase.WhereAnd(restrictorAttribute.RestrictingCondition);
    return (IEnumerable) pxSelectBase.Select(Array.Empty<object>());
  }
}
