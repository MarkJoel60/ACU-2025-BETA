// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceOrderProcess
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.FS;

public class ServiceOrderProcess : PXGraph<ServiceOrderProcess>
{
  public PXFilter<ServiceOrderFilter> Filter;
  public PXCancel<ServiceOrderFilter> Cancel;
  [PXFilterable(new Type[] {})]
  [PXViewDetailsButton(typeof (ServiceOrderFilter))]
  public PXFilteredProcessingJoin<FSServiceOrder, ServiceOrderFilter, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceOrder.customerID>>>, Where2<Where<CurrentValue<ServiceOrderFilter.soAction>, NotEqual<ListField_ServiceOrder_Action_Filter.Undefined>>, And2<Where<CurrentValue<ServiceOrderFilter.srvOrdType>, IsNull, Or<FSServiceOrder.srvOrdType, Equal<CurrentValue<ServiceOrderFilter.srvOrdType>>>>, And2<Where<CurrentValue<ServiceOrderFilter.branchID>, IsNull, Or<FSServiceOrder.branchID, Equal<CurrentValue<ServiceOrderFilter.branchID>>>>, And2<Where<CurrentValue<ServiceOrderFilter.branchLocationID>, IsNull, Or<FSServiceOrder.branchLocationID, Equal<CurrentValue<ServiceOrderFilter.branchLocationID>>>>, And2<Where<CurrentValue<ServiceOrderFilter.status>, IsNull, Or<FSServiceOrder.status, Equal<CurrentValue<ServiceOrderFilter.status>>>>, And2<Where<CurrentValue<ServiceOrderFilter.customerID>, IsNull, Or<FSServiceOrder.customerID, Equal<CurrentValue<ServiceOrderFilter.customerID>>>>, And2<Where<CurrentValue<ServiceOrderFilter.serviceContractID>, IsNull, Or<FSServiceOrder.serviceContractID, Equal<CurrentValue<ServiceOrderFilter.serviceContractID>>>>, And2<Where<CurrentValue<ServiceOrderFilter.fromDate>, IsNull, Or<FSServiceOrder.orderDate, GreaterEqual<CurrentValue<ServiceOrderFilter.fromDate>>>>, And2<Where<CurrentValue<ServiceOrderFilter.toDate>, IsNull, Or<FSServiceOrder.orderDate, LessEqual<CurrentValue<ServiceOrderFilter.toDate>>>>, And2<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>, And<Where<Where2<Where<CurrentValue<ServiceOrderFilter.soAction>, Equal<ListField_ServiceOrder_Action_Filter.Complete>, And<FSServiceOrder.openDoc, Equal<True>>>, Or2<Where<CurrentValue<ServiceOrderFilter.soAction>, Equal<ListField_ServiceOrder_Action_Filter.Cancel>, And<FSServiceOrder.openDoc, Equal<True>>>, Or2<Where<CurrentValue<ServiceOrderFilter.soAction>, Equal<ListField_ServiceOrder_Action_Filter.Reopen>, And<Where<FSServiceOrder.canceled, Equal<True>, Or<Where<FSServiceOrder.completed, Equal<True>, And<FSServiceOrder.closed, Equal<False>>>>>>>, Or2<Where<CurrentValue<ServiceOrderFilter.soAction>, Equal<ListField_ServiceOrder_Action_Filter.Close>, And<FSServiceOrder.completed, Equal<True>, And<FSServiceOrder.closed, Equal<False>>>>, Or2<Where<CurrentValue<ServiceOrderFilter.soAction>, Equal<ListField_ServiceOrder_Action_Filter.Unclose>, And<FSServiceOrder.closed, Equal<True>, And<FSServiceOrder.completed, Equal<True>>>>, Or<Where<CurrentValue<ServiceOrderFilter.soAction>, Equal<ListField_ServiceOrder_Action_Filter.AllowInvoice>, And<FSServiceOrder.allowInvoice, Equal<False>>>>>>>>>>>>>>>>>>>>>> ServiceOrderRecords;

  public ServiceOrderProcess()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ServiceOrderProcess.\u003C\u003Ec__DisplayClass0_0 cDisplayClass00 = new ServiceOrderProcess.\u003C\u003Ec__DisplayClass0_0();
    // ISSUE: explicit constructor call
    base.\u002Ector();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass00.\u003C\u003E4__this = this;
    // ISSUE: method pointer
    ((PXProcessingBase<FSServiceOrder>) this.ServiceOrderRecords).SetProcessDelegate(new PXProcessingBase<FSServiceOrder>.ProcessListDelegate((object) cDisplayClass00, __methodptr(\u003C\u002Ector\u003Eb__0)));
  }
}
