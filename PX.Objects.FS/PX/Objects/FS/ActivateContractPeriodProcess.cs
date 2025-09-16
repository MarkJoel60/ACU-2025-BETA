// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ActivateContractPeriodProcess
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class ActivateContractPeriodProcess : PXGraph<ActivateContractPeriodProcess>
{
  public ServiceContractEntry graphServiceContractEntry;
  public RouteServiceContractEntry graphRouteServiceContractEntry;
  public PXFilter<ServiceContractFilter> Filter;
  public PXCancel<ServiceContractFilter> Cancel;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingJoin<FSServiceContract, ServiceContractFilter, LeftJoin<FSContractPeriod, On<FSContractPeriod.serviceContractID, Equal<FSServiceContract.serviceContractID>, And<FSContractPeriod.status, Equal<ListField_Status_ContractPeriod.Active>>>>, Where2<Where<CurrentValue<ServiceContractFilter.customerID>, IsNull, Or<FSServiceContract.customerID, Equal<CurrentValue<ServiceContractFilter.customerID>>>>, And2<Where<CurrentValue<ServiceContractFilter.refNbr>, IsNull, Or<FSServiceContract.refNbr, Equal<CurrentValue<ServiceContractFilter.refNbr>>>>, And2<Where<CurrentValue<ServiceContractFilter.customerLocationID>, IsNull, Or<FSServiceContract.customerLocationID, Equal<CurrentValue<ServiceContractFilter.customerLocationID>>>>, And2<Where<CurrentValue<ServiceContractFilter.branchID>, IsNull, Or<FSServiceContract.branchID, Equal<CurrentValue<ServiceContractFilter.branchID>>>>, And2<Where<CurrentValue<ServiceContractFilter.branchLocationID>, IsNull, Or<FSServiceContract.branchLocationID, Equal<CurrentValue<ServiceContractFilter.branchLocationID>>>>, And<FSContractPeriod.contractPeriodID, IsNull, And<FSServiceContract.billingType, Equal<ListField.ServiceContractBillingType.standardizedBillings>, And<FSServiceContract.status, Equal<ListField_Status_ServiceContract.Active>>>>>>>>>, OrderBy<Asc<FSServiceContract.customerID, Asc<FSServiceContract.refNbr>>>> ServiceContracts;

  public ActivateContractPeriodProcess()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<FSServiceContract>) this.ServiceContracts).SetProcessDelegate(new PXProcessingBase<FSServiceContract>.ProcessItemDelegate((object) new ActivateContractPeriodProcess.\u003C\u003Ec__DisplayClass2_0()
    {
      graphActivateContractPeriodProcess = (ActivateContractPeriodProcess) null
    }, __methodptr(\u003C\u002Ector\u003Eb__0)));
  }

  public virtual void ProcessContractPeriod(
    ActivateContractPeriodProcess graphActivateContractPeriodProcess,
    FSServiceContract fsServiceContractRow)
  {
    if (fsServiceContractRow.RecordType == "NRSC")
    {
      ((PXSelectBase<FSServiceContract>) graphActivateContractPeriodProcess.graphServiceContractEntry.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) graphActivateContractPeriodProcess.graphServiceContractEntry.ServiceContractRecords).Search<FSServiceContract.serviceContractID>((object) fsServiceContractRow.ServiceContractID, new object[1]
      {
        (object) fsServiceContractRow.CustomerID
      }));
      ((PXSelectBase<FSContractPeriodFilter>) graphActivateContractPeriodProcess.graphServiceContractEntry.ContractPeriodFilter).SetValueExt<FSContractPeriodFilter.actions>(((PXSelectBase<FSContractPeriodFilter>) graphActivateContractPeriodProcess.graphServiceContractEntry.ContractPeriodFilter).Current, (object) "MBP");
      graphActivateContractPeriodProcess.graphServiceContractEntry.ActivatePeriod();
    }
    else
    {
      if (!(fsServiceContractRow.RecordType == "IRSC"))
        return;
      ((PXSelectBase<FSServiceContract>) graphActivateContractPeriodProcess.graphRouteServiceContractEntry.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) graphActivateContractPeriodProcess.graphRouteServiceContractEntry.ServiceContractRecords).Search<FSServiceContract.serviceContractID>((object) fsServiceContractRow.ServiceContractID, new object[1]
      {
        (object) fsServiceContractRow.CustomerID
      }));
      ((PXSelectBase<FSContractPeriodFilter>) graphActivateContractPeriodProcess.graphRouteServiceContractEntry.ContractPeriodFilter).SetValueExt<FSContractPeriodFilter.actions>(((PXSelectBase<FSContractPeriodFilter>) graphActivateContractPeriodProcess.graphServiceContractEntry.ContractPeriodFilter).Current, (object) "MBP");
      graphActivateContractPeriodProcess.graphRouteServiceContractEntry.ActivatePeriod();
    }
  }
}
