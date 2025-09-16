// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ProcessServiceContracts
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

public class ProcessServiceContracts : PXGraph<ProcessServiceContracts>
{
  public ServiceContractEntry graphServiceContractEntry;
  public RouteServiceContractEntry graphRouteServiceContractEntry;
  public PXFilter<ServiceContractFilter> Filter;
  public PXCancel<ServiceContractFilter> Cancel;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingJoin<FSServiceContract, ServiceContractFilter, InnerJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<FSServiceContract.customerID>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>> ServiceContracts;

  [PXUIField]
  [PXMergeAttributes]
  protected virtual void FSServiceContract_CustomerID_CacheAttached(PXCache sender)
  {
  }

  [PXString]
  protected virtual void FSServiceContract_FormCaptionDescription_CacheAttached(PXCache sender)
  {
  }

  [PXUIField]
  [PXMergeAttributes]
  protected virtual void FSServiceContract_CustomerLocationID_CacheAttached(PXCache sender)
  {
  }

  public ProcessServiceContracts()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ProcessServiceContracts.\u003C\u003Ec__DisplayClass5_0 cDisplayClass50 = new ProcessServiceContracts.\u003C\u003Ec__DisplayClass5_0();
    // ISSUE: explicit constructor call
    base.\u002Ector();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass50.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass50.graphServiceContractStatusProcess = (ProcessServiceContracts) null;
    // ISSUE: method pointer
    ((PXProcessingBase<FSServiceContract>) this.ServiceContracts).SetProcessDelegate(new PXProcessingBase<FSServiceContract>.ProcessItemDelegate((object) cDisplayClass50, __methodptr(\u003C\u002Ector\u003Eb__0)));
  }

  public virtual void processServiceContract(
    ProcessServiceContracts graphServiceContractStatusProcess,
    FSServiceContract fsServiceContractRow,
    bool isRenew = false)
  {
    if (fsServiceContractRow.RecordType == "NRSC")
    {
      ((PXSelectBase<FSServiceContract>) graphServiceContractStatusProcess.graphServiceContractEntry.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) graphServiceContractStatusProcess.graphServiceContractEntry.ServiceContractRecords).Search<FSServiceContract.serviceContractID>((object) fsServiceContractRow.ServiceContractID, new object[1]
      {
        (object) fsServiceContractRow.CustomerID
      }));
      PXAdapter adapter = new PXAdapter((PXSelectBase) graphServiceContractStatusProcess.graphServiceContractEntry.ServiceContractRecords, Array.Empty<object>());
      adapter.SortColumns = new string[1]
      {
        typeof (FSServiceContract.refNbr).Name
      };
      adapter.Searches = new object[1]
      {
        (object) ((PXSelectBase<FSServiceContract>) graphServiceContractStatusProcess.graphServiceContractEntry.ServiceContractRecords).Current.RefNbr
      };
      adapter.MaximumRows = 1;
      if (!isRenew)
      {
        if (fsServiceContractRow.UpcomingStatus == "A")
          graphServiceContractStatusProcess.graphServiceContractEntry.ActivateContract(adapter);
        else if (fsServiceContractRow.UpcomingStatus == "X")
          graphServiceContractStatusProcess.graphServiceContractEntry.CancelContract(adapter);
        else if (fsServiceContractRow.UpcomingStatus == "S")
        {
          graphServiceContractStatusProcess.graphServiceContractEntry.SuspendContract(adapter);
        }
        else
        {
          if (!(fsServiceContractRow.UpcomingStatus == "E"))
            return;
          graphServiceContractStatusProcess.graphServiceContractEntry.ExpireContract();
        }
      }
      else
        graphServiceContractStatusProcess.graphServiceContractEntry.RenewContract(adapter);
    }
    else
    {
      if (!(fsServiceContractRow.RecordType == "IRSC"))
        return;
      ((PXSelectBase<FSServiceContract>) graphServiceContractStatusProcess.graphRouteServiceContractEntry.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) graphServiceContractStatusProcess.graphRouteServiceContractEntry.ServiceContractRecords).Search<FSServiceContract.serviceContractID>((object) fsServiceContractRow.ServiceContractID, new object[1]
      {
        (object) fsServiceContractRow.CustomerID
      }));
      PXAdapter adapter = new PXAdapter((PXSelectBase) graphServiceContractStatusProcess.graphRouteServiceContractEntry.ServiceContractRecords, Array.Empty<object>());
      adapter.SortColumns = new string[1]
      {
        typeof (FSServiceContract.refNbr).Name
      };
      adapter.Searches = new object[1]
      {
        (object) ((PXSelectBase<FSServiceContract>) graphServiceContractStatusProcess.graphRouteServiceContractEntry.ServiceContractRecords).Current.RefNbr
      };
      adapter.MaximumRows = 1;
      if (!isRenew)
      {
        if (fsServiceContractRow.UpcomingStatus == "A")
          graphServiceContractStatusProcess.graphRouteServiceContractEntry.ActivateContract(adapter);
        else if (fsServiceContractRow.UpcomingStatus == "X")
          graphServiceContractStatusProcess.graphRouteServiceContractEntry.CancelContract(adapter);
        else if (fsServiceContractRow.UpcomingStatus == "S")
        {
          graphServiceContractStatusProcess.graphRouteServiceContractEntry.SuspendContract(adapter);
        }
        else
        {
          if (!(fsServiceContractRow.UpcomingStatus == "E"))
            return;
          graphServiceContractStatusProcess.graphRouteServiceContractEntry.ExpireContract();
        }
      }
      else
        graphServiceContractStatusProcess.graphServiceContractEntry.RenewContract(adapter);
    }
  }

  public virtual void ProcessContractPeriod(
    ProcessServiceContracts graphServiceContractStatusProcess,
    FSServiceContract fsServiceContractRow)
  {
    if (fsServiceContractRow.RecordType == "NRSC")
    {
      ((PXSelectBase<FSServiceContract>) graphServiceContractStatusProcess.graphServiceContractEntry.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) graphServiceContractStatusProcess.graphServiceContractEntry.ServiceContractRecords).Search<FSServiceContract.serviceContractID>((object) fsServiceContractRow.ServiceContractID, new object[1]
      {
        (object) fsServiceContractRow.CustomerID
      }));
      ((PXSelectBase<FSContractPeriodFilter>) graphServiceContractStatusProcess.graphServiceContractEntry.ContractPeriodFilter).SetValueExt<FSContractPeriodFilter.actions>(((PXSelectBase<FSContractPeriodFilter>) graphServiceContractStatusProcess.graphServiceContractEntry.ContractPeriodFilter).Current, (object) "MBP");
      graphServiceContractStatusProcess.graphServiceContractEntry.ActivatePeriod();
    }
    else
    {
      if (!(fsServiceContractRow.RecordType == "IRSC"))
        return;
      ((PXSelectBase<FSServiceContract>) graphServiceContractStatusProcess.graphRouteServiceContractEntry.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) graphServiceContractStatusProcess.graphRouteServiceContractEntry.ServiceContractRecords).Search<FSServiceContract.serviceContractID>((object) fsServiceContractRow.ServiceContractID, new object[1]
      {
        (object) fsServiceContractRow.CustomerID
      }));
      ((PXSelectBase<FSContractPeriodFilter>) graphServiceContractStatusProcess.graphRouteServiceContractEntry.ContractPeriodFilter).SetValueExt<FSContractPeriodFilter.actions>(((PXSelectBase<FSContractPeriodFilter>) graphServiceContractStatusProcess.graphServiceContractEntry.ContractPeriodFilter).Current, (object) "MBP");
      graphServiceContractStatusProcess.graphRouteServiceContractEntry.ActivatePeriod();
    }
  }

  public virtual void ProcessContractForecast(
    ProcessServiceContracts graphServiceContractStatusProcess,
    FSServiceContract fsServiceContractRow)
  {
    if (fsServiceContractRow.RecordType == "NRSC")
    {
      ((PXSelectBase<FSServiceContract>) graphServiceContractStatusProcess.graphServiceContractEntry.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) graphServiceContractStatusProcess.graphServiceContractEntry.ServiceContractRecords).Search<FSServiceContract.serviceContractID>((object) fsServiceContractRow.ServiceContractID, new object[1]
      {
        (object) fsServiceContractRow.CustomerID
      }));
      graphServiceContractStatusProcess.graphServiceContractEntry.ContractForecastProc(fsServiceContractRow, graphServiceContractStatusProcess.graphServiceContractEntry.GetDfltForecastFilterStartDate(), graphServiceContractStatusProcess.graphServiceContractEntry.GetDfltForecastFilterEndDate());
    }
    else
    {
      if (!(fsServiceContractRow.RecordType == "IRSC"))
        return;
      ((PXSelectBase<FSServiceContract>) graphServiceContractStatusProcess.graphRouteServiceContractEntry.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) graphServiceContractStatusProcess.graphRouteServiceContractEntry.ServiceContractRecords).Search<FSServiceContract.serviceContractID>((object) fsServiceContractRow.ServiceContractID, new object[1]
      {
        (object) fsServiceContractRow.CustomerID
      }));
      graphServiceContractStatusProcess.graphRouteServiceContractEntry.ContractForecastProc(fsServiceContractRow, graphServiceContractStatusProcess.graphRouteServiceContractEntry.GetDfltForecastFilterStartDate(), graphServiceContractStatusProcess.graphRouteServiceContractEntry.GetDfltForecastFilterEndDate());
    }
  }

  public virtual void ProcessContractEmailQuote(
    ProcessServiceContracts graphServiceContractStatusProcess,
    FSServiceContract fsServiceContractRow)
  {
    if (fsServiceContractRow.RecordType == "NRSC")
    {
      ((PXSelectBase<FSServiceContract>) graphServiceContractStatusProcess.graphServiceContractEntry.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) graphServiceContractStatusProcess.graphServiceContractEntry.ServiceContractRecords).Search<FSServiceContract.serviceContractID>((object) fsServiceContractRow.ServiceContractID, new object[1]
      {
        (object) fsServiceContractRow.CustomerID
      }));
      graphServiceContractStatusProcess.graphServiceContractEntry.EmailQuoteContract(new PXAdapter((PXSelectBase) graphServiceContractStatusProcess.graphServiceContractEntry.ServiceContractRecords, Array.Empty<object>())
      {
        SortColumns = new string[1]
        {
          typeof (FSServiceContract.refNbr).Name
        },
        Searches = new object[1]
        {
          (object) ((PXSelectBase<FSServiceContract>) graphServiceContractStatusProcess.graphServiceContractEntry.ServiceContractRecords).Current.RefNbr
        },
        MaximumRows = 1
      });
    }
    else
    {
      if (!(fsServiceContractRow.RecordType == "IRSC"))
        return;
      ((PXSelectBase<FSServiceContract>) graphServiceContractStatusProcess.graphRouteServiceContractEntry.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) graphServiceContractStatusProcess.graphRouteServiceContractEntry.ServiceContractRecords).Search<FSServiceContract.serviceContractID>((object) fsServiceContractRow.ServiceContractID, new object[1]
      {
        (object) fsServiceContractRow.CustomerID
      }));
      graphServiceContractStatusProcess.graphRouteServiceContractEntry.EmailQuoteContract(new PXAdapter((PXSelectBase) graphServiceContractStatusProcess.graphRouteServiceContractEntry.ServiceContractRecords, Array.Empty<object>())
      {
        SortColumns = new string[1]
        {
          typeof (FSServiceContract.refNbr).Name
        },
        Searches = new object[1]
        {
          (object) ((PXSelectBase<FSServiceContract>) graphServiceContractStatusProcess.graphRouteServiceContractEntry.ServiceContractRecords).Current.RefNbr
        },
        MaximumRows = 1
      });
    }
  }

  public virtual IEnumerable serviceContracts()
  {
    BqlCommand bqlCommand = (BqlCommand) null;
    switch (((PXSelectBase<ServiceContractFilter>) this.Filter).Current.ActionType)
    {
      case "CS":
        bqlCommand = (BqlCommand) new Select<FSServiceContract, Where2<Where<CurrentValue<ServiceContractFilter.customerID>, IsNull, Or<FSServiceContract.customerID, Equal<CurrentValue<ServiceContractFilter.customerID>>>>, And2<Where<CurrentValue<ServiceContractFilter.refNbr>, IsNull, Or<FSServiceContract.refNbr, Equal<CurrentValue<ServiceContractFilter.refNbr>>>>, And2<Where<CurrentValue<ServiceContractFilter.customerLocationID>, IsNull, Or<FSServiceContract.customerLocationID, Equal<CurrentValue<ServiceContractFilter.customerLocationID>>>>, And2<Where<CurrentValue<ServiceContractFilter.branchID>, IsNull, Or<FSServiceContract.branchID, Equal<CurrentValue<ServiceContractFilter.branchID>>>>, And2<Where<CurrentValue<ServiceContractFilter.branchLocationID>, IsNull, Or<FSServiceContract.branchLocationID, Equal<CurrentValue<ServiceContractFilter.branchLocationID>>>>, And<FSServiceContract.upcomingStatus, IsNotNull, And<FSServiceContract.statusEffectiveUntilDate, LessEqual<Current<AccessInfo.businessDate>>>>>>>>>, OrderBy<Asc<FSServiceContract.customerID, Asc<FSServiceContract.refNbr>>>>();
        break;
      case "CP":
        bqlCommand = (BqlCommand) new Select2<FSServiceContract, InnerJoin<FSContractPeriod, On<FSContractPeriod.serviceContractID, Equal<FSServiceContract.serviceContractID>, And<FSContractPeriod.status, Equal<ListField_Status_ContractPeriod.Inactive>>>>, Where2<Where<CurrentValue<ServiceContractFilter.customerID>, IsNull, Or<FSServiceContract.customerID, Equal<CurrentValue<ServiceContractFilter.customerID>>>>, And2<Where<CurrentValue<ServiceContractFilter.refNbr>, IsNull, Or<FSServiceContract.refNbr, Equal<CurrentValue<ServiceContractFilter.refNbr>>>>, And2<Where<CurrentValue<ServiceContractFilter.customerLocationID>, IsNull, Or<FSServiceContract.customerLocationID, Equal<CurrentValue<ServiceContractFilter.customerLocationID>>>>, And2<Where<CurrentValue<ServiceContractFilter.branchID>, IsNull, Or<FSServiceContract.branchID, Equal<CurrentValue<ServiceContractFilter.branchID>>>>, And2<Where<CurrentValue<ServiceContractFilter.branchLocationID>, IsNull, Or<FSServiceContract.branchLocationID, Equal<CurrentValue<ServiceContractFilter.branchLocationID>>>>, And<FSServiceContract.activePeriodID, IsNull, And<FSServiceContract.billingType, NotEqual<ListField.ServiceContractBillingType.performedBillings>, And<FSServiceContract.status, Equal<ListField_Status_ServiceContract.Active>>>>>>>>>, OrderBy<Asc<FSServiceContract.customerID, Asc<FSServiceContract.refNbr>>>>();
        break;
      case "RW":
        bqlCommand = (BqlCommand) new Select<FSServiceContract, Where2<Where<CurrentValue<ServiceContractFilter.customerID>, IsNull, Or<FSServiceContract.customerID, Equal<CurrentValue<ServiceContractFilter.customerID>>>>, And2<Where<CurrentValue<ServiceContractFilter.refNbr>, IsNull, Or<FSServiceContract.refNbr, Equal<CurrentValue<ServiceContractFilter.refNbr>>>>, And2<Where<CurrentValue<ServiceContractFilter.customerLocationID>, IsNull, Or<FSServiceContract.customerLocationID, Equal<CurrentValue<ServiceContractFilter.customerLocationID>>>>, And2<Where<CurrentValue<ServiceContractFilter.branchID>, IsNull, Or<FSServiceContract.branchID, Equal<CurrentValue<ServiceContractFilter.branchID>>>>, And2<Where<CurrentValue<ServiceContractFilter.branchLocationID>, IsNull, Or<FSServiceContract.branchLocationID, Equal<CurrentValue<ServiceContractFilter.branchLocationID>>>>, And<FSServiceContract.expirationType, Equal<ListField_Contract_ExpirationType.Renewable>, And<FSServiceContract.status, NotEqual<ListField_Status_ServiceContract.Draft>>>>>>>>, OrderBy<Asc<FSServiceContract.customerID, Asc<FSServiceContract.refNbr>>>>();
        break;
      case "FC":
        bqlCommand = (BqlCommand) new Select<FSServiceContract, Where2<Where<CurrentValue<ServiceContractFilter.customerID>, IsNull, Or<FSServiceContract.customerID, Equal<CurrentValue<ServiceContractFilter.customerID>>>>, And2<Where<CurrentValue<ServiceContractFilter.refNbr>, IsNull, Or<FSServiceContract.refNbr, Equal<CurrentValue<ServiceContractFilter.refNbr>>>>, And2<Where<CurrentValue<ServiceContractFilter.customerLocationID>, IsNull, Or<FSServiceContract.customerLocationID, Equal<CurrentValue<ServiceContractFilter.customerLocationID>>>>, And2<Where<CurrentValue<ServiceContractFilter.branchID>, IsNull, Or<FSServiceContract.branchID, Equal<CurrentValue<ServiceContractFilter.branchID>>>>, And2<Where<CurrentValue<ServiceContractFilter.branchLocationID>, IsNull, Or<FSServiceContract.branchLocationID, Equal<CurrentValue<ServiceContractFilter.branchLocationID>>>>, And<FSServiceContract.status, NotEqual<ListField_Status_ServiceContract.Canceled>, And<FSServiceContract.status, NotEqual<ListField_Status_ServiceContract.Expired>>>>>>>>, OrderBy<Asc<FSServiceContract.customerID, Asc<FSServiceContract.refNbr>>>>();
        break;
      case "EQ":
        bqlCommand = (BqlCommand) new Select2<FSServiceContract, InnerJoin<FSContractForecast, On<FSContractForecast.serviceContractID, Equal<FSServiceContract.serviceContractID>, And<FSContractForecast.active, Equal<True>>>>, Where2<Where<CurrentValue<ServiceContractFilter.customerID>, IsNull, Or<FSServiceContract.customerID, Equal<CurrentValue<ServiceContractFilter.customerID>>>>, And2<Where<CurrentValue<ServiceContractFilter.refNbr>, IsNull, Or<FSServiceContract.refNbr, Equal<CurrentValue<ServiceContractFilter.refNbr>>>>, And2<Where<CurrentValue<ServiceContractFilter.customerLocationID>, IsNull, Or<FSServiceContract.customerLocationID, Equal<CurrentValue<ServiceContractFilter.customerLocationID>>>>, And2<Where<CurrentValue<ServiceContractFilter.branchID>, IsNull, Or<FSServiceContract.branchID, Equal<CurrentValue<ServiceContractFilter.branchID>>>>, And<Where<CurrentValue<ServiceContractFilter.branchLocationID>, IsNull, Or<FSServiceContract.branchLocationID, Equal<CurrentValue<ServiceContractFilter.branchLocationID>>>>>>>>>, OrderBy<Asc<FSServiceContract.customerID, Asc<FSServiceContract.refNbr>>>>();
        break;
    }
    PXView pxView = new PXView((PXGraph) this, true, bqlCommand);
    int startRow = PXView.StartRow;
    int num = 0;
    object[] currents = PXView.Currents;
    object[] parameters = PXView.Parameters;
    object[] searches = PXView.Searches;
    string[] sortColumns = PXView.SortColumns;
    bool[] descendings = PXView.Descendings;
    PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
    ref int local1 = ref startRow;
    int maximumRows = PXView.MaximumRows;
    ref int local2 = ref num;
    List<object> objectList = pxView.Select(currents, parameters, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2);
    PXView.StartRow = 0;
    return (IEnumerable) objectList;
  }
}
