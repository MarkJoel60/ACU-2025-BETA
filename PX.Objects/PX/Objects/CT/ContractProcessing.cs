// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractProcessing
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CT;

public class ContractProcessing
{
  protected ARInvoiceEntry graph;
  protected Contract contract;
  protected Contract template;
  protected ContractBillingSchedule schedule;
  protected PX.Objects.AR.Customer customer;
  protected CommonSetup commonsetup;
  protected PX.Objects.CR.Location location;

  protected ContractProcessing()
  {
  }

  protected ContractProcessing(int? contractID)
  {
    int? nullable1 = contractID;
    int num = 0;
    if (!(nullable1.GetValueOrDefault() > num & nullable1.HasValue))
      return;
    this.graph = PXGraph.CreateInstance<ARInvoiceEntry>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this.graph).FieldVerifying.AddHandler<ARInvoice.projectID>(ContractProcessing.\u003C\u003Ec.\u003C\u003E9__8_0 ?? (ContractProcessing.\u003C\u003Ec.\u003C\u003E9__8_0 = new PXFieldVerifying((object) ContractProcessing.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__8_0))));
    this.contract = PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelect<Contract, Where<Contract.contractID, Equal<Required<Contract.contractID>>>>.Config>.Select((PXGraph) this.graph, new object[1]
    {
      (object) contractID
    }));
    this.commonsetup = PXResultset<CommonSetup>.op_Implicit(PXSelectBase<CommonSetup, PXSelect<CommonSetup>.Config>.Select((PXGraph) this.graph, Array.Empty<object>()));
    this.template = PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelect<Contract, Where<Contract.contractID, Equal<Required<Contract.contractID>>>>.Config>.Select((PXGraph) this.graph, new object[1]
    {
      (object) this.contract.TemplateID
    }));
    this.schedule = PXResultset<ContractBillingSchedule>.op_Implicit(PXSelectBase<ContractBillingSchedule, PXSelect<ContractBillingSchedule>.Config>.Search<ContractBillingSchedule.contractID>((PXGraph) this.graph, (object) this.contract.ContractID, Array.Empty<object>()));
    int? nullable2 = this.contract.CustomerID;
    if (nullable2.HasValue)
    {
      if (this.schedule != null)
      {
        nullable2 = this.schedule.AccountID;
        if (nullable2.HasValue)
        {
          this.customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<ContractBillingSchedule.accountID>>>>.Config>.Select((PXGraph) this.graph, new object[1]
          {
            (object) this.schedule.AccountID
          }));
          nullable2 = this.schedule.LocationID;
          if (nullable2.HasValue)
          {
            this.location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<ContractBillingSchedule.accountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<ContractBillingSchedule.locationID>>>>>.Config>.Select((PXGraph) this.graph, new object[2]
            {
              (object) this.customer.BAccountID,
              (object) this.schedule.LocationID
            }));
            goto label_10;
          }
          this.location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.AR.Customer.defLocationID>>>>>.Config>.Select((PXGraph) this.graph, new object[2]
          {
            (object) this.customer.BAccountID,
            (object) this.customer.DefLocationID
          }));
          goto label_10;
        }
      }
      this.customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this.graph, new object[1]
      {
        (object) this.contract.CustomerID
      }));
      nullable2 = this.contract.LocationID;
      if (nullable2.HasValue)
        this.location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<ContractBillingSchedule.accountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<ContractBillingSchedule.locationID>>>>>.Config>.Select((PXGraph) this.graph, new object[2]
        {
          (object) this.customer.BAccountID,
          (object) this.contract.LocationID
        }));
      else
        this.location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.AR.Customer.defLocationID>>>>>.Config>.Select((PXGraph) this.graph, new object[2]
        {
          (object) this.customer.BAccountID,
          (object) this.customer.DefLocationID
        }));
    }
label_10:
    this.SetupGraph();
  }

  protected int DecPlQty
  {
    get
    {
      int decPlQty = 4;
      if (this.commonsetup != null && this.commonsetup.DecPlQty.HasValue)
        decPlQty = Convert.ToInt32((object) this.commonsetup.DecPlQty);
      return decPlQty;
    }
  }

  protected int DecPlPrcCst
  {
    get
    {
      int decPlPrcCst = 2;
      if (this.commonsetup != null && this.commonsetup.DecPlPrcCst.HasValue)
        decPlPrcCst = Convert.ToInt32((object) this.commonsetup.DecPlPrcCst);
      return decPlPrcCst;
    }
  }

  protected virtual void SetupGraph()
  {
    ((PXGraph) this.graph).Clear();
    ((PXGraph) this.graph).Views.Caches.Add(typeof (Contract));
    ((PXGraph) this.graph).Views.Caches.Add(typeof (ContractDetail));
    ((PXGraph) this.graph).Views.Caches.Add(typeof (ContractBillingSchedule));
    PXDBDefaultAttribute.SetDefaultForUpdate<ContractBillingSchedule.contractID>(((PXGraph) this.graph).Caches[typeof (ContractBillingSchedule)], (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<ContractDetail.contractID>(((PXGraph) this.graph).Caches[typeof (ContractDetail)], (object) null, false);
  }
}
