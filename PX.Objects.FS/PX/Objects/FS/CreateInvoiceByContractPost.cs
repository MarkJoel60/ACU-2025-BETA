// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.CreateInvoiceByContractPost
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class CreateInvoiceByContractPost : PXGraph<CreateInvoiceByContractPost>
{
  [PXHidden]
  public PXSetup<FSSetup> SetupRecord;
  public PXFilter<InvoiceContractPeriodFilter> Filter;
  public PXCancel<InvoiceContractPeriodFilter> Cancel;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingJoin<ContractPeriodToPost, InvoiceContractPeriodFilter, InnerJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<ContractPeriodToPost.billCustomerID>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>, Where2<Where<CurrentValue<InvoiceContractPeriodFilter.customerID>, IsNull, Or<ContractPeriodToPost.billCustomerID, Equal<CurrentValue<InvoiceContractPeriodFilter.customerID>>>>, And<Where<CurrentValue<InvoiceContractPeriodFilter.serviceContractID>, IsNull, Or<ContractPeriodToPost.serviceContractID, Equal<CurrentValue<InvoiceContractPeriodFilter.serviceContractID>>>>>>> Contracts;

  protected virtual IEnumerable contracts()
  {
    CreateInvoiceByContractPost invoiceByContractPost = this;
    int? nullable1 = new int?(-1);
    foreach (PXResult<ContractPeriodToPost, FSServiceOrder, FSAppointment> pxResult in PXSelectBase<ContractPeriodToPost, PXSelectJoin<ContractPeriodToPost, LeftJoin<FSServiceOrder, On<FSServiceOrder.billServiceContractID, Equal<ContractPeriodToPost.serviceContractID>, And<FSServiceOrder.billContractPeriodID, Equal<ContractPeriodToPost.contractPeriodID>, And<FSServiceOrder.allowInvoice, Equal<False>, And<FSServiceOrder.openDoc, Equal<True>>>>>, LeftJoin<FSAppointment, On<FSAppointment.billServiceContractID, Equal<ContractPeriodToPost.serviceContractID>, And<FSAppointment.billContractPeriodID, Equal<ContractPeriodToPost.contractPeriodID>, And<FSAppointment.closed, Equal<False>, And<FSAppointment.canceled, Equal<False>>>>>, InnerJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<ContractPeriodToPost.billCustomerID>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>, Where2<Where<CurrentValue<InvoiceContractPeriodFilter.customerID>, IsNull, Or<ContractPeriodToPost.billCustomerID, Equal<CurrentValue<InvoiceContractPeriodFilter.customerID>>>>, And2<Where2<Where<ContractPeriodToPost.billingType, Equal<ListField.ServiceContractBillingType.standardizedBillings>, And<Where<CurrentValue<InvoiceContractPeriodFilter.upToDate>, IsNull, Or<ContractPeriodToPost.endPeriodDate, LessEqual<CurrentValue<InvoiceContractPeriodFilter.upToDate>>>>>>, Or<Where2<Where<ContractPeriodToPost.billingType, Equal<ListField.ServiceContractBillingType.fixedRateBillings>, Or<ContractPeriodToPost.billingType, Equal<ListField.ServiceContractBillingType.fixedRateAsPerformedBillings>>>, And<Where<CurrentValue<InvoiceContractPeriodFilter.upToDate>, IsNull, Or<ContractPeriodToPost.startPeriodDate, LessEqual<CurrentValue<InvoiceContractPeriodFilter.upToDate>>>>>>>>, And<Where<CurrentValue<InvoiceContractPeriodFilter.serviceContractID>, IsNull, Or<ContractPeriodToPost.serviceContractID, Equal<CurrentValue<InvoiceContractPeriodFilter.serviceContractID>>>>>>>, OrderBy<Asc<ContractPeriodToPost.serviceContractID, Asc<ContractPeriodToPost.contractPeriodID, Asc<FSServiceOrder.sOID, Asc<FSAppointment.appointmentID>>>>>>.Config>.Select((PXGraph) invoiceByContractPost, Array.Empty<object>()))
    {
      ContractPeriodToPost contractPeriodToPostRow = PXResult<ContractPeriodToPost, FSServiceOrder, FSAppointment>.op_Implicit(pxResult);
      FSServiceOrder fsServiceOrder = PXResult<ContractPeriodToPost, FSServiceOrder, FSAppointment>.op_Implicit(pxResult);
      FSAppointment fsAppointment = PXResult<ContractPeriodToPost, FSServiceOrder, FSAppointment>.op_Implicit(pxResult);
      if (!(contractPeriodToPostRow.BillingType == "STDB") || (fsServiceOrder == null || !fsServiceOrder.SOID.HasValue) && (fsAppointment == null || !fsAppointment.AppointmentID.HasValue))
      {
        int? nullable2 = nullable1;
        int? contractPeriodId = contractPeriodToPostRow.ContractPeriodID;
        if (!(nullable2.GetValueOrDefault() == contractPeriodId.GetValueOrDefault() & nullable2.HasValue == contractPeriodId.HasValue))
          yield return (object) new PXResult<ContractPeriodToPost>(PXResult<ContractPeriodToPost, FSServiceOrder, FSAppointment>.op_Implicit(pxResult));
        nullable1 = contractPeriodToPostRow.ContractPeriodID;
        contractPeriodToPostRow = (ContractPeriodToPost) null;
      }
    }
    ((PXSelectBase) invoiceByContractPost.Contracts).Cache.IsDirty = false;
    ((PXSelectBase) invoiceByContractPost.Contracts).View.RequestRefresh();
  }

  public CreateInvoiceByContractPost()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<ContractPeriodToPost>) this.Contracts).SetProcessDelegate(new PXProcessingBase<ContractPeriodToPost>.ProcessListDelegate((object) new CreateInvoiceByContractPost.\u003C\u003Ec__DisplayClass5_0()
    {
      graphCreateInvoiceByServiceOrderPost = (CreateInvoiceByContractPost) null,
      filter = ((PXSelectBase<InvoiceContractPeriodFilter>) this.Filter).Current
    }, __methodptr(\u003C\u002Ector\u003Eb__0)));
    OpenPeriodAttribute.SetValidatePeriod<InvoiceContractPeriodFilter.invoiceFinPeriodID>(((PXSelectBase) this.Filter).Cache, (object) null, ((PXGraph) this).IsContractBasedAPI || ((PXGraph) this).IsImport || ((PXGraph) this).IsExport || ((PXGraph) this).UnattendedMode ? PeriodValidation.DefaultUpdate : PeriodValidation.DefaultSelectUpdate);
  }

  public virtual void InsertFixedRateBillRelatedDocuments(
    FSServiceContract fsServiceContractRow,
    int? contractPeriodID)
  {
    if (fsServiceContractRow.BillingType != "FIRB")
      return;
    PXCache cach = ((PXGraph) this).Caches[typeof (FSBillHistory)];
    foreach (PXResult<FSAppointment, FSContractPostDoc, FSBillHistory> pxResult in PXSelectBase<FSAppointment, PXSelectJoin<FSAppointment, InnerJoin<FSContractPostDoc, On<FSContractPostDoc.serviceContractID, Equal<FSAppointment.billServiceContractID>, And<FSContractPostDoc.contractPeriodID, Equal<FSAppointment.billContractPeriodID>>>, InnerJoin<FSBillHistory, On<FSBillHistory.childDocType, Equal<FSContractPostDoc.postDocType>, And<FSBillHistory.childRefNbr, Equal<FSContractPostDoc.postRefNbr>>>>>, Where<FSAppointment.billServiceContractID, Equal<Required<FSAppointment.billServiceContractID>>, And<FSAppointment.billContractPeriodID, Equal<Required<FSAppointment.billContractPeriodID>>, And<FSBillHistory.srvOrdType, IsNull, And<FSBillHistory.appointmentRefNbr, IsNull>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) fsServiceContractRow.ServiceContractID,
      (object) contractPeriodID
    }))
    {
      FSAppointment fsAppointment = PXResult<FSAppointment, FSContractPostDoc, FSBillHistory>.op_Implicit(pxResult);
      FSBillHistory fsBillHistory = PXResult<FSAppointment, FSContractPostDoc, FSBillHistory>.op_Implicit(pxResult);
      cach.Insert((object) new FSBillHistory()
      {
        ServiceContractRefNbr = fsBillHistory.ServiceContractRefNbr,
        SrvOrdType = fsAppointment.SrvOrdType,
        AppointmentRefNbr = fsAppointment.RefNbr,
        ChildEntityType = fsBillHistory.ChildEntityType,
        ChildDocType = fsBillHistory.ChildDocType,
        ChildRefNbr = fsBillHistory.ChildRefNbr
      });
    }
    foreach (PXResult<FSServiceOrder, FSContractPostDoc, FSBillHistory> pxResult in PXSelectBase<FSServiceOrder, PXSelectJoin<FSServiceOrder, InnerJoin<FSContractPostDoc, On<FSContractPostDoc.serviceContractID, Equal<FSServiceOrder.billServiceContractID>, And<FSContractPostDoc.contractPeriodID, Equal<FSServiceOrder.billContractPeriodID>>>, InnerJoin<FSBillHistory, On<FSBillHistory.childDocType, Equal<FSContractPostDoc.postDocType>, And<FSBillHistory.childRefNbr, Equal<FSContractPostDoc.postRefNbr>>>>>, Where<FSServiceOrder.billServiceContractID, Equal<Required<FSServiceOrder.billServiceContractID>>, And<FSServiceOrder.billContractPeriodID, Equal<Required<FSServiceOrder.billContractPeriodID>>, And<FSBillHistory.srvOrdType, IsNull, And<FSBillHistory.serviceOrderRefNbr, IsNull>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) fsServiceContractRow.ServiceContractID,
      (object) contractPeriodID
    }))
    {
      FSServiceOrder fsServiceOrder = PXResult<FSServiceOrder, FSContractPostDoc, FSBillHistory>.op_Implicit(pxResult);
      FSBillHistory fsBillHistory = PXResult<FSServiceOrder, FSContractPostDoc, FSBillHistory>.op_Implicit(pxResult);
      cach.Insert((object) new FSBillHistory()
      {
        ServiceContractRefNbr = fsBillHistory.ServiceContractRefNbr,
        SrvOrdType = fsServiceOrder.SrvOrdType,
        ServiceOrderRefNbr = fsServiceOrder.RefNbr,
        ChildEntityType = fsBillHistory.ChildEntityType,
        ChildDocType = fsBillHistory.ChildDocType,
        ChildRefNbr = fsBillHistory.ChildRefNbr
      });
    }
    cach.Persist((PXDBOperation) 2);
  }

  public virtual void DeallocateItemsThatAreBeingPosted(
    ServiceOrderEntry graph,
    List<ContractInvoiceLine> docLines)
  {
    List<FSSODetSplit> splitsToDeallocate = new List<FSSODetSplit>();
    IEnumerable<IGrouping<(string, string), ContractInvoiceLine>> groupings = docLines.Where<ContractInvoiceLine>((Func<ContractInvoiceLine, bool>) (x => !string.IsNullOrEmpty(x.SrvOrdType) && !string.IsNullOrEmpty(x.RefNbr))).GroupBy<ContractInvoiceLine, (string, string)>((Func<ContractInvoiceLine, (string, string)>) (x => (x.SrvOrdType, x.RefNbr)));
    PXCache pxCache1 = (PXCache) new PXCache<FSAppointmentDet>((PXGraph) this);
    PXCache pxCache2 = (PXCache) new PXCache<FSApptLineSplit>((PXGraph) this);
    int? nullable1 = new int?();
    FSSODet soLine = (FSSODet) null;
    List<FSAppointmentDet> source1 = new List<FSAppointmentDet>();
    List<FSApptLineSplit> source2 = new List<FSApptLineSplit>();
    foreach (IGrouping<(string, string), ContractInvoiceLine> source3 in groupings)
    {
      ContractInvoiceLine contractInvoiceLine = source3.First<ContractInvoiceLine>();
      int? nullable2;
      if (contractInvoiceLine != null)
      {
        nullable2 = contractInvoiceLine.AppointmentID;
        if (nullable2.HasValue)
        {
          nullable2 = contractInvoiceLine.AppDetID;
          if (nullable2.HasValue && contractInvoiceLine.fsAppointmentDet != null)
          {
            int? nullable3 = new int?();
            soLine = (FSSODet) null;
            bool flag = false;
            source1.Clear();
            source2.Clear();
            using (IEnumerator<PXResult<FSSODetSplit>> enumerator = PXSelectBase<FSSODetSplit, PXSelect<FSSODetSplit, Where<FSSODetSplit.srvOrdType, Equal<Required<FSSODetSplit.srvOrdType>>, And<FSSODetSplit.refNbr, Equal<Required<FSSODetSplit.refNbr>>, And<FSSODetSplit.completed, Equal<False>, And<FSSODetSplit.pOCreate, Equal<False>, And<FSSODetSplit.inventoryID, IsNotNull>>>>>, OrderBy<Asc<FSSODetSplit.lineNbr, Asc<FSSODetSplit.splitLineNbr>>>>.Config>.Select((PXGraph) graph, new object[2]
            {
              (object) contractInvoiceLine.fsSODet.SrvOrdType,
              (object) contractInvoiceLine.fsSODet.RefNbr
            }).GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                FSSODetSplit soSplit = PXResult<FSSODetSplit>.op_Implicit(enumerator.Current);
                if (nullable3.HasValue)
                {
                  nullable2 = nullable3;
                  int? lineNbr = soSplit.LineNbr;
                  if (nullable2.GetValueOrDefault() == lineNbr.GetValueOrDefault() & nullable2.HasValue == lineNbr.HasValue)
                    goto label_25;
                }
                soLine = source3.Where<ContractInvoiceLine>((Func<ContractInvoiceLine, bool>) (x =>
                {
                  int? lineNbr1 = x.fsSODet.LineNbr;
                  int? lineNbr2 = soSplit.LineNbr;
                  return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
                })).FirstOrDefault<ContractInvoiceLine>()?.fsSODet;
                if (soLine != null)
                {
                  flag = SharedFunctions.IsLotSerialRequired(((PXSelectBase) graph.ServiceOrderDetails).Cache, soSplit.InventoryID);
                  nullable3 = soSplit.LineNbr;
                  source1.Clear();
                  source2.Clear();
                  foreach (FSAppointmentDet fsAppointmentDet in source3.Where<ContractInvoiceLine>((Func<ContractInvoiceLine, bool>) (x =>
                  {
                    int? soDetId1 = x.fsAppointmentDet.SODetID;
                    int? soDetId2 = soLine.SODetID;
                    return soDetId1.GetValueOrDefault() == soDetId2.GetValueOrDefault() & soDetId1.HasValue == soDetId2.HasValue;
                  })).Select<ContractInvoiceLine, FSAppointmentDet>((Func<ContractInvoiceLine, FSAppointmentDet>) (x => x.fsAppointmentDet)))
                    source1.Add((FSAppointmentDet) pxCache1.CreateCopy((object) fsAppointmentDet));
                  if (flag)
                  {
                    foreach (FSAppointmentDet fsAppointmentDet in source1)
                    {
                      foreach (FSApptLineSplit selectChild in PXParentAttribute.SelectChildren(pxCache2, (object) fsAppointmentDet, typeof (FSAppointmentDet)))
                        source2.Add((FSApptLineSplit) pxCache2.CreateCopy((object) selectChild));
                    }
                  }
                }
                else
                  continue;
label_25:
                if (flag)
                {
                  foreach (FSApptLineSplit fsApptLineSplit1 in source2.Where<FSApptLineSplit>((Func<FSApptLineSplit, bool>) (x => !string.IsNullOrEmpty(x.LotSerialNbr) && string.Equals(x.LotSerialNbr, soSplit.LotSerialNbr, StringComparison.OrdinalIgnoreCase))))
                  {
                    Decimal? baseQty1 = fsApptLineSplit1.BaseQty;
                    Decimal? baseQty2 = soSplit.BaseQty;
                    if (baseQty1.GetValueOrDefault() <= baseQty2.GetValueOrDefault() & baseQty1.HasValue & baseQty2.HasValue)
                    {
                      FSSODetSplit fssoDetSplit = soSplit;
                      Decimal? baseQty3 = fssoDetSplit.BaseQty;
                      Decimal? baseQty4 = fsApptLineSplit1.BaseQty;
                      fssoDetSplit.BaseQty = baseQty3.HasValue & baseQty4.HasValue ? new Decimal?(baseQty3.GetValueOrDefault() - baseQty4.GetValueOrDefault()) : new Decimal?();
                      fsApptLineSplit1.BaseQty = new Decimal?(0M);
                    }
                    else
                    {
                      FSApptLineSplit fsApptLineSplit2 = fsApptLineSplit1;
                      Decimal? baseQty5 = fsApptLineSplit2.BaseQty;
                      Decimal? baseQty6 = soSplit.BaseQty;
                      fsApptLineSplit2.BaseQty = baseQty5.HasValue & baseQty6.HasValue ? new Decimal?(baseQty5.GetValueOrDefault() - baseQty6.GetValueOrDefault()) : new Decimal?();
                      soSplit.BaseQty = new Decimal?(0M);
                    }
                    FSAppointmentDet fsAppointmentDet = FSAppointmentDet.PK.Find((PXGraph) graph, fsApptLineSplit1.SrvOrdType, fsApptLineSplit1.ApptNbr, fsApptLineSplit1.LineNbr);
                    if (fsAppointmentDet != null && !(fsAppointmentDet.SrvOrdType != fsApptLineSplit1.SrvOrdType) && !(fsAppointmentDet.RefNbr != fsApptLineSplit1.ApptNbr))
                    {
                      int? lineNbr3 = fsAppointmentDet.LineNbr;
                      int? lineNbr4 = fsApptLineSplit1.LineNbr;
                      if (lineNbr3.GetValueOrDefault() == lineNbr4.GetValueOrDefault() & lineNbr3.HasValue == lineNbr4.HasValue)
                        continue;
                    }
                    throw new PXException("The {0} record was not found.", new object[1]
                    {
                      (object) DACHelper.GetDisplayName(typeof (FSAppointmentDet))
                    });
                  }
                }
                else
                {
                  foreach (FSAppointmentDet fsAppointmentDet1 in source1.Where<FSAppointmentDet>((Func<FSAppointmentDet, bool>) (x =>
                  {
                    Decimal? baseEffTranQty = x.BaseEffTranQty;
                    Decimal num = 0M;
                    return baseEffTranQty.GetValueOrDefault() > num & baseEffTranQty.HasValue;
                  })))
                  {
                    Decimal? baseEffTranQty1 = fsAppointmentDet1.BaseEffTranQty;
                    Decimal? baseQty7 = soSplit.BaseQty;
                    if (baseEffTranQty1.GetValueOrDefault() <= baseQty7.GetValueOrDefault() & baseEffTranQty1.HasValue & baseQty7.HasValue)
                    {
                      FSSODetSplit fssoDetSplit = soSplit;
                      Decimal? baseQty8 = fssoDetSplit.BaseQty;
                      Decimal? baseEffTranQty2 = fsAppointmentDet1.BaseEffTranQty;
                      fssoDetSplit.BaseQty = baseQty8.HasValue & baseEffTranQty2.HasValue ? new Decimal?(baseQty8.GetValueOrDefault() - baseEffTranQty2.GetValueOrDefault()) : new Decimal?();
                      fsAppointmentDet1.BaseEffTranQty = new Decimal?(0M);
                    }
                    else
                    {
                      FSAppointmentDet fsAppointmentDet2 = fsAppointmentDet1;
                      Decimal? baseEffTranQty3 = fsAppointmentDet2.BaseEffTranQty;
                      Decimal? baseQty9 = soSplit.BaseQty;
                      fsAppointmentDet2.BaseEffTranQty = baseEffTranQty3.HasValue & baseQty9.HasValue ? new Decimal?(baseEffTranQty3.GetValueOrDefault() - baseQty9.GetValueOrDefault()) : new Decimal?();
                      soSplit.BaseQty = new Decimal?(0M);
                    }
                  }
                }
                splitsToDeallocate.Add(soSplit);
              }
              continue;
            }
          }
        }
      }
      if (contractInvoiceLine != null)
      {
        nullable2 = contractInvoiceLine.SOID;
        if (nullable2.HasValue)
        {
          nullable2 = contractInvoiceLine.SODetID;
          if (nullable2.HasValue && contractInvoiceLine.fsSODet != null)
          {
            foreach (PXResult<FSSODetSplit> pxResult in PXSelectBase<FSSODetSplit, PXSelect<FSSODetSplit, Where<FSSODetSplit.srvOrdType, Equal<Required<FSSODetSplit.srvOrdType>>, And<FSSODetSplit.refNbr, Equal<Required<FSSODetSplit.refNbr>>, And<FSSODetSplit.completed, Equal<False>, And<FSSODetSplit.pOCreate, Equal<False>, And<FSSODetSplit.inventoryID, IsNotNull>>>>>, OrderBy<Asc<FSSODetSplit.lineNbr, Asc<FSSODetSplit.splitLineNbr>>>>.Config>.Select((PXGraph) graph, new object[2]
            {
              (object) contractInvoiceLine.fsSODet.SrvOrdType,
              (object) contractInvoiceLine.fsSODet.RefNbr
            }))
            {
              FSSODetSplit fssoDetSplit = PXResult<FSSODetSplit>.op_Implicit(pxResult);
              FSSODetSplit copy = (FSSODetSplit) ((PXSelectBase) graph.Splits).Cache.CreateCopy((object) fssoDetSplit);
              copy.BaseQty = new Decimal?(0M);
              splitsToDeallocate.Add(copy);
            }
          }
        }
      }
    }
    FSAllocationProcess.DeallocateServiceOrderSplits(graph, splitsToDeallocate, false);
  }

  public virtual void InvoiceContractPeriodFilter_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    InvoiceContractPeriodFilter row = (InvoiceContractPeriodFilter) e.Row;
    bool flag = string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<InvoiceContractPeriodFilter.invoiceFinPeriodID>(cache, (object) row));
    ((PXProcessing<ContractPeriodToPost>) this.Contracts).SetProcessAllEnabled(flag);
    ((PXProcessing<ContractPeriodToPost>) this.Contracts).SetProcessEnabled(flag);
  }

  public virtual IInvoiceContractGraph GetInvoiceGraph(FSSetup fsSetupRow)
  {
    if (fsSetupRow.ContractPostTo == "SO")
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
        return (IInvoiceContractGraph) ((PXGraph) PXGraph.CreateInstance<SOOrderEntry>()).GetExtension<SM_SOOrderEntry>();
      throw new PXException("The Distribution module is disabled.");
    }
    if (fsSetupRow.ContractPostTo == "AR")
      return (IInvoiceContractGraph) ((PXGraph) PXGraph.CreateInstance<ARInvoiceEntry>()).GetExtension<SM_ARInvoiceEntry>();
    if (fsSetupRow.ContractPostTo == "SI")
      throw new PXSetPropertyException("The ability to generate SO invoices for service contracts has not been implemented yet. Please select another option in the Billing Settings section on the {0} form, or if you generate billing documents for route service contracts, on the {1} form.", (PXErrorLevel) 4, new object[2]
      {
        (object) DACHelper.GetDisplayName(typeof (FSEquipmentSetup)),
        (object) DACHelper.GetDisplayName(typeof (FSRouteSetup))
      });
    return (IInvoiceContractGraph) null;
  }

  public virtual void GetContractAndPeriod(
    int? serviceContractID,
    int? contractPeriodID,
    out FSServiceContract fsServiceContractRow,
    out FSContractPeriod fsContractPeriodRow)
  {
    fsContractPeriodRow = (FSContractPeriod) null;
    fsServiceContractRow = (FSServiceContract) null;
    PXResult<FSServiceContract, FSContractPeriod> pxResult = (PXResult<FSServiceContract, FSContractPeriod>) PXResultset<FSServiceContract>.op_Implicit(PXSelectBase<FSServiceContract, PXSelectJoin<FSServiceContract, InnerJoin<FSContractPeriod, On<FSContractPeriod.contractPeriodID, Equal<FSContractPeriod.contractPeriodID>>>, Where<FSServiceContract.serviceContractID, Equal<Required<FSServiceContract.serviceContractID>>, And<FSContractPeriod.contractPeriodID, Equal<Required<FSContractPeriod.contractPeriodID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) serviceContractID,
      (object) contractPeriodID
    }));
    if (pxResult == null)
      return;
    fsServiceContractRow = PXResult<FSServiceContract, FSContractPeriod>.op_Implicit(pxResult);
    fsContractPeriodRow = PXResult<FSServiceContract, FSContractPeriod>.op_Implicit(pxResult);
  }

  public virtual List<ContractInvoiceLine> GetContractPeriodLines(
    ContractPeriodToPost contractPeriodToPostRow)
  {
    PXResultset<FSContractPeriodDet> pxResultset = PXSelectBase<FSContractPeriodDet, PXSelectJoin<FSContractPeriodDet, InnerJoin<FSContractPeriod, On<FSContractPeriod.contractPeriodID, Equal<FSContractPeriodDet.contractPeriodID>>, InnerJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSContractPeriod.serviceContractID>>, InnerJoin<FSBranchLocation, On<FSBranchLocation.branchLocationID, Equal<FSServiceContract.branchLocationID>>>>>, Where<FSServiceContract.serviceContractID, Equal<Required<FSServiceContract.serviceContractID>>, And<FSContractPeriod.contractPeriodID, Equal<Required<FSContractPeriod.contractPeriodID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) contractPeriodToPostRow.ServiceContractID,
      (object) contractPeriodToPostRow.ContractPeriodID
    });
    List<ContractInvoiceLine> contractPeriodLines = new List<ContractInvoiceLine>();
    foreach (PXResult<FSContractPeriodDet, FSContractPeriod, FSServiceContract, FSBranchLocation> row in pxResultset)
      contractPeriodLines.Add(new ContractInvoiceLine(row));
    return contractPeriodLines;
  }

  public virtual List<ContractInvoiceLine> GetContractInvoiceLines(
    ContractPeriodToPost contractPeriodToPostRow)
  {
    List<ContractInvoiceLine> contractInvoiceLines = new List<ContractInvoiceLine>();
    foreach (PXResult<FSAppointmentDet, FSSODet, FSAppointment> row in PXSelectBase<FSAppointmentDet, PXSelectJoin<FSAppointmentDet, InnerJoin<FSSODet, On<FSSODet.sODetID, Equal<FSAppointmentDet.sODetID>>, InnerJoin<FSAppointment, On<FSAppointment.srvOrdType, Equal<FSAppointmentDet.srvOrdType>, And<FSAppointment.refNbr, Equal<FSAppointmentDet.refNbr>>>, InnerJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSAppointment.billServiceContractID>>>>>, Where<FSServiceContract.billingType, Equal<ListField.ServiceContractBillingType.standardizedBillings>, And<FSAppointment.billServiceContractID, Equal<Required<FSAppointment.billServiceContractID>>, And<FSAppointment.billContractPeriodID, Equal<Required<FSAppointment.billContractPeriodID>>, And<FSAppointmentDet.equipmentAction, Equal<ListField_EquipmentActionBase.None>, And<FSAppointmentDet.lineType, NotEqual<FSLineType.Comment>, And<FSAppointmentDet.lineType, NotEqual<FSLineType.Instruction>, And<FSAppointmentDet.isCanceledNotPerformed, NotEqual<True>, And<FSAppointmentDet.lineType, NotEqual<ListField_LineType_Pickup_Delivery.Pickup_Delivery>, And<FSAppointmentDet.isPrepaid, Equal<False>, And<FSAppointmentDet.isBillable, Equal<True>>>>>>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) contractPeriodToPostRow.ServiceContractID,
      (object) contractPeriodToPostRow.ContractPeriodID
    }))
      contractInvoiceLines.Add(new ContractInvoiceLine(row));
    foreach (PXResult<FSAppointmentDet, FSSODet, FSAppointment> row in PXSelectBase<FSAppointmentDet, PXSelectJoin<FSAppointmentDet, InnerJoin<FSSODet, On<FSSODet.sODetID, Equal<FSAppointmentDet.sODetID>>, InnerJoin<FSAppointment, On<FSAppointment.srvOrdType, Equal<FSAppointmentDet.srvOrdType>, And<FSAppointment.refNbr, Equal<FSAppointmentDet.refNbr>>>, InnerJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSAppointment.billServiceContractID>>>>>, Where<FSServiceContract.billingType, Equal<ListField.ServiceContractBillingType.standardizedBillings>, And<FSAppointment.billServiceContractID, Equal<Required<FSAppointment.billServiceContractID>>, And<FSAppointment.billContractPeriodID, Equal<Required<FSAppointment.billContractPeriodID>>, And<FSAppointmentDet.lineType, Equal<ListField_LineType_Pickup_Delivery.Pickup_Delivery>, And<FSAppointmentDet.equipmentAction, Equal<ListField_EquipmentActionBase.None>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) contractPeriodToPostRow.ServiceContractID,
      (object) contractPeriodToPostRow.ContractPeriodID
    }))
      contractInvoiceLines.Add(new ContractInvoiceLine(row));
    foreach (PXResult<FSSODet, FSServiceOrder> row in PXSelectBase<FSSODet, PXSelectJoin<FSSODet, InnerJoin<FSServiceOrder, On<FSServiceOrder.srvOrdType, Equal<FSSODet.srvOrdType>, And<FSServiceOrder.refNbr, Equal<FSSODet.refNbr>>>, InnerJoin<FSServiceContract, On<FSServiceContract.serviceContractID, Equal<FSServiceOrder.billServiceContractID>>>>, Where<FSServiceContract.billingType, Equal<ListField.ServiceContractBillingType.standardizedBillings>, And<FSServiceOrder.billServiceContractID, Equal<Required<FSServiceOrder.billServiceContractID>>, And<FSServiceOrder.billContractPeriodID, Equal<Required<FSServiceOrder.billContractPeriodID>>, And<FSSODet.equipmentAction, Equal<ListField_EquipmentActionBase.None>, And<FSSODet.lineType, NotEqual<FSLineType.Comment>, And<FSSODet.lineType, NotEqual<FSLineType.Instruction>, And<FSSODet.status, NotEqual<FSSODet.ListField_Status_SODet.Canceled>, And<FSSODet.isPrepaid, Equal<False>, And<FSSODet.isBillable, Equal<True>>>>>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) contractPeriodToPostRow.ServiceContractID,
      (object) contractPeriodToPostRow.ContractPeriodID
    }))
      contractInvoiceLines.Add(new ContractInvoiceLine(row));
    return contractInvoiceLines;
  }

  public virtual List<ContractInvoiceLine> GetEquipmentInvoiceLines(
    ContractPeriodToPost contractPeriodToPostRow)
  {
    List<ContractInvoiceLine> equipmentInvoiceLines = new List<ContractInvoiceLine>();
    foreach (PXResult<FSAppointmentDet, FSSODet, FSAppointment> row in PXSelectBase<FSAppointmentDet, PXSelectJoin<FSAppointmentDet, InnerJoin<FSSODet, On<FSSODet.sODetID, Equal<FSAppointmentDet.sODetID>>, InnerJoin<FSAppointment, On<FSAppointment.srvOrdType, Equal<FSAppointmentDet.srvOrdType>, And<FSAppointment.refNbr, Equal<FSAppointmentDet.refNbr>>>>>, Where<FSAppointment.billServiceContractID, Equal<Required<FSAppointment.billServiceContractID>>, And<FSAppointment.billContractPeriodID, Equal<Required<FSAppointment.billContractPeriodID>>, And<FSAppointmentDet.equipmentAction, NotEqual<ListField_EquipmentActionBase.None>, And<FSAppointmentDet.lineType, NotEqual<FSLineType.Comment>, And<FSAppointmentDet.lineType, NotEqual<FSLineType.Instruction>, And<FSAppointmentDet.isCanceledNotPerformed, NotEqual<True>, And<FSAppointmentDet.lineType, NotEqual<ListField_LineType_Pickup_Delivery.Pickup_Delivery>, And<FSAppointmentDet.isPrepaid, Equal<False>, And<FSAppointmentDet.isBillable, Equal<True>>>>>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) contractPeriodToPostRow.ServiceContractID,
      (object) contractPeriodToPostRow.ContractPeriodID
    }))
      equipmentInvoiceLines.Add(new ContractInvoiceLine(row));
    foreach (PXResult<FSAppointmentDet, FSSODet, FSAppointment> row in PXSelectBase<FSAppointmentDet, PXSelectJoin<FSAppointmentDet, InnerJoin<FSSODet, On<FSSODet.sODetID, Equal<FSAppointmentDet.sODetID>>, InnerJoin<FSAppointment, On<FSAppointment.srvOrdType, Equal<FSAppointmentDet.srvOrdType>, And<FSAppointment.refNbr, Equal<FSAppointmentDet.refNbr>>>>>, Where<FSAppointment.billServiceContractID, Equal<Required<FSAppointment.billServiceContractID>>, And<FSAppointment.billContractPeriodID, Equal<Required<FSAppointment.billContractPeriodID>>, And<FSAppointmentDet.lineType, Equal<ListField_LineType_Pickup_Delivery.Pickup_Delivery>, And<FSAppointmentDet.equipmentAction, NotEqual<ListField_EquipmentActionBase.None>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) contractPeriodToPostRow.ServiceContractID,
      (object) contractPeriodToPostRow.ContractPeriodID
    }))
      equipmentInvoiceLines.Add(new ContractInvoiceLine(row));
    foreach (PXResult<FSSODet, FSServiceOrder> row in PXSelectBase<FSSODet, PXSelectJoin<FSSODet, InnerJoin<FSServiceOrder, On<FSServiceOrder.srvOrdType, Equal<FSSODet.srvOrdType>, And<FSServiceOrder.refNbr, Equal<FSSODet.refNbr>>>>, Where<FSServiceOrder.billServiceContractID, Equal<Required<FSServiceOrder.billServiceContractID>>, And<FSServiceOrder.billContractPeriodID, Equal<Required<FSServiceOrder.billContractPeriodID>>, And<FSSODet.equipmentAction, NotEqual<ListField_EquipmentActionBase.None>, And<FSSODet.lineType, NotEqual<FSLineType.Comment>, And<FSSODet.lineType, NotEqual<FSLineType.Instruction>, And<FSSODet.status, NotEqual<FSSODet.ListField_Status_SODet.Canceled>, And<FSSODet.isPrepaid, Equal<False>, And<FSSODet.isBillable, Equal<True>>>>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) contractPeriodToPostRow.ServiceContractID,
      (object) contractPeriodToPostRow.ContractPeriodID
    }))
      equipmentInvoiceLines.Add(new ContractInvoiceLine(row));
    return equipmentInvoiceLines;
  }

  public virtual List<ContractInvoiceLine> GetInvoiceLines(
    List<ContractInvoiceLine> invoiceLine,
    List<ContractInvoiceLine> contractPeriodLines,
    List<ContractInvoiceLine> contractInvoiceDetLines,
    string PostTo)
  {
    Decimal? nullable1 = new Decimal?(0M);
    List<ContractInvoiceLine> list1 = contractPeriodLines.Select<ContractInvoiceLine, ContractInvoiceLine>((Func<ContractInvoiceLine, ContractInvoiceLine>) (cl => new ContractInvoiceLine(cl))).ToList<ContractInvoiceLine>();
    List<ContractInvoiceLine> list2 = contractInvoiceDetLines.Where<ContractInvoiceLine>((Func<ContractInvoiceLine, bool>) (_ =>
    {
      bool? contractRelated = _.ContractRelated;
      bool flag = false;
      return contractRelated.GetValueOrDefault() == flag & contractRelated.HasValue;
    })).ToList<ContractInvoiceLine>();
    List<ContractInvoiceLine> list3 = contractInvoiceDetLines.Where<ContractInvoiceLine>((Func<ContractInvoiceLine, bool>) (_ => _.ContractRelated.GetValueOrDefault())).ToList<ContractInvoiceLine>();
    List<ContractInvoiceLine> source = !(PostTo == "AR") ? list3.GroupBy(l => new
    {
      BillingRule = l.BillingRule,
      InventoryID = l.InventoryID,
      UOM = l.UOM,
      SMEquipmentID = l.SMEquipmentID,
      CuryUnitPrice = l.CuryUnitPrice,
      ContractRelated = l.ContractRelated,
      SubItemID = l.SubItemID,
      SiteID = l.SiteID,
      SiteLocationID = l.SiteLocationID,
      IsFree = l.IsFree
    }).Select<IGrouping<\u003C\u003Ef__AnonymousType8<string, int?, string, int?, Decimal?, bool?, int?, int?, int?, bool?>, ContractInvoiceLine>, ContractInvoiceLine>(cl => new ContractInvoiceLine(cl.First<ContractInvoiceLine>(), cl.Sum<ContractInvoiceLine>((Func<ContractInvoiceLine, Decimal?>) (p => p.Qty)))).ToList<ContractInvoiceLine>() : list3.GroupBy(l => new
    {
      BillingRule = l.BillingRule,
      InventoryID = l.InventoryID,
      UOM = l.UOM,
      SMEquipmentID = l.SMEquipmentID,
      CuryUnitPrice = l.CuryUnitPrice,
      ContractRelated = l.ContractRelated
    }).Select<IGrouping<\u003C\u003Ef__AnonymousType7<string, int?, string, int?, Decimal?, bool?>, ContractInvoiceLine>, ContractInvoiceLine>(cl => new ContractInvoiceLine(cl.First<ContractInvoiceLine>(), cl.Sum<ContractInvoiceLine>((Func<ContractInvoiceLine, Decimal?>) (p => p.Qty)))).ToList<ContractInvoiceLine>();
    int? nullable2;
    for (int index1 = 0; index1 < source.Count<ContractInvoiceLine>(); ++index1)
    {
      for (int index2 = 0; index2 < list1.Count<ContractInvoiceLine>(); ++index2)
      {
        int? inventoryId = source[index1].InventoryID;
        nullable2 = list1[index2].InventoryID;
        if (inventoryId.GetValueOrDefault() == nullable2.GetValueOrDefault() & inventoryId.HasValue == nullable2.HasValue)
        {
          nullable2 = list1[index2].SMEquipmentID;
          if (!nullable2.HasValue)
          {
            nullable2 = source[index1].SMEquipmentID;
            if (nullable2.HasValue)
            {
              ContractInvoiceLine contractInvoiceLine = source[index1];
              nullable2 = new int?();
              int? nullable3 = nullable2;
              contractInvoiceLine.SMEquipmentID = nullable3;
            }
          }
        }
      }
    }
    List<ContractInvoiceLine> second = !(PostTo == "AR") ? source.GroupBy(l => new
    {
      BillingRule = l.BillingRule,
      InventoryID = l.InventoryID,
      UOM = l.UOM,
      SMEquipmentID = l.SMEquipmentID,
      CuryUnitPrice = l.CuryUnitPrice,
      ContractRelated = l.ContractRelated,
      SubItemID = l.SubItemID,
      SiteID = l.SiteID,
      SiteLocationID = l.SiteLocationID,
      IsFree = l.IsFree
    }).Select<IGrouping<\u003C\u003Ef__AnonymousType8<string, int?, string, int?, Decimal?, bool?, int?, int?, int?, bool?>, ContractInvoiceLine>, ContractInvoiceLine>(cl => new ContractInvoiceLine(cl.First<ContractInvoiceLine>())).ToList<ContractInvoiceLine>() : source.GroupBy(l => new
    {
      BillingRule = l.BillingRule,
      InventoryID = l.InventoryID,
      UOM = l.UOM,
      SMEquipmentID = l.SMEquipmentID,
      CuryUnitPrice = l.CuryUnitPrice,
      ContractRelated = l.ContractRelated
    }).Select<IGrouping<\u003C\u003Ef__AnonymousType7<string, int?, string, int?, Decimal?, bool?>, ContractInvoiceLine>, ContractInvoiceLine>(cl => new ContractInvoiceLine(cl.First<ContractInvoiceLine>())).ToList<ContractInvoiceLine>();
    List<ContractInvoiceLine> list4 = list2.Concat<ContractInvoiceLine>((IEnumerable<ContractInvoiceLine>) second).ToList<ContractInvoiceLine>();
    List<ContractInvoiceLine> invoiceLines = new List<ContractInvoiceLine>();
    foreach (ContractInvoiceLine contractInvoiceLine1 in list4)
    {
      if (contractInvoiceLine1.ContractRelated.GetValueOrDefault())
      {
        for (int index = 0; index < list1.Count<ContractInvoiceLine>(); ++index)
        {
          nullable2 = contractInvoiceLine1.InventoryID;
          int? nullable4 = list1[index].InventoryID;
          if (nullable2.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable2.HasValue == nullable4.HasValue)
          {
            nullable4 = list1[index].SMEquipmentID;
            if (nullable4.HasValue)
            {
              nullable4 = list1[index].SMEquipmentID;
              nullable2 = contractInvoiceLine1.SMEquipmentID;
              if (!(nullable4.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable4.HasValue == nullable2.HasValue))
                continue;
            }
            if (contractInvoiceLine1.BillingRule == list1[index].BillingRule)
            {
              nullable1 = new Decimal?(0M);
              Decimal? qty1 = list1[index].Qty;
              ContractInvoiceLine contractInvoiceLine2 = list1[index];
              Decimal? qty2 = list1[index].Qty;
              Decimal? qty3 = contractInvoiceLine1.Qty;
              Decimal? nullable5 = qty2.HasValue & qty3.HasValue ? new Decimal?(qty2.GetValueOrDefault() - qty3.GetValueOrDefault()) : new Decimal?();
              contractInvoiceLine2.Qty = nullable5;
              list1[index].TranDescPrefix = "Contract Coverage: ";
              qty3 = list1[index].Qty;
              Decimal num1 = 0M;
              if (qty3.GetValueOrDefault() < num1 & qty3.HasValue)
              {
                ContractInvoiceLine contractInvoiceLine3 = list1[index];
                qty3 = list1[index].Qty;
                Decimal num2 = (Decimal) -1;
                Decimal? nullable6 = qty3.HasValue ? new Decimal?(qty3.GetValueOrDefault() * num2) : new Decimal?();
                contractInvoiceLine3.Qty = nullable6;
                list1[index].CuryUnitPrice = contractInvoiceLine1.OverageItemPrice;
                list1[index].TranDescPrefix = "Contract Overage: ";
                contractInvoiceLine1.Qty = qty1;
              }
              qty3 = list1[index].Qty;
              Decimal num3 = 0M;
              if (qty3.GetValueOrDefault() > num3 & qty3.HasValue)
              {
                list1[index].Processed = new bool?(true);
                invoiceLine.Add(list1[index]);
                invoiceLines.Add(list1[index]);
              }
              qty3 = list1[index].Qty;
              Decimal num4 = 0M;
              if (qty3.GetValueOrDefault() == num4 & qty3.HasValue)
                list1[index].Processed = new bool?(true);
              contractInvoiceLine1.TranDescPrefix = "Contract Usage: ";
            }
          }
        }
      }
      contractInvoiceLine1.Processed = new bool?(true);
      invoiceLines.Add(contractInvoiceLine1);
    }
    foreach (ContractInvoiceLine contractInvoiceLine in list1)
    {
      if (!contractInvoiceLine.Processed.GetValueOrDefault())
      {
        contractInvoiceLine.TranDescPrefix = "Contract Coverage: ";
        Decimal? qty = contractInvoiceLine.Qty;
        Decimal num = 0M;
        if (qty.GetValueOrDefault() >= num & qty.HasValue || contractInvoiceLine.BillingType == "FIRB" || contractInvoiceLine.BillingType == "FRPB")
        {
          contractInvoiceLine.Processed = new bool?(true);
          invoiceLine.Add(contractInvoiceLine);
          invoiceLines.Add(contractInvoiceLine);
        }
      }
    }
    return invoiceLines;
  }

  public virtual void CreatePostRegisterAndBillHistory(
    PXGraph graph,
    FSContractPostDoc fsContractPostDocRow,
    FSServiceContract fsServiceContractRow,
    List<ContractInvoiceLine> invoiceLines)
  {
    this.CreateContractPostRegisterAndBillHistory(graph, fsContractPostDocRow, fsServiceContractRow);
    foreach (IEnumerable<ContractInvoiceLine> source in invoiceLines.Where<ContractInvoiceLine>((Func<ContractInvoiceLine, bool>) (x => x.SOID.HasValue)).GroupBy<ContractInvoiceLine, (int?, int?)>((Func<ContractInvoiceLine, (int?, int?)>) (x => (x.SOID, x.AppointmentID))))
    {
      ContractInvoiceLine contractInvoiceLine = source.First<ContractInvoiceLine>();
      this.CreatePostRegisterAndBillHistory(graph, fsContractPostDocRow, fsServiceContractRow, contractInvoiceLine.fsSODet, contractInvoiceLine.fsAppointmentDet);
    }
  }

  public virtual void CreateContractPostRegisterAndBillHistory(
    PXGraph graph,
    FSContractPostDoc fsContractPostDocRow,
    FSServiceContract fsServiceContractRow)
  {
    PXCache cach = graph.Caches[typeof (FSContractPostRegister)];
    cach.Persist((object) (FSContractPostRegister) cach.Insert((object) new FSContractPostRegister()
    {
      ServiceContractID = fsContractPostDocRow.ServiceContractID,
      ContractPeriodID = fsContractPostDocRow.ContractPeriodID,
      ContractPostBatchID = fsContractPostDocRow.ContractPostBatchID,
      PostedTO = fsContractPostDocRow.PostedTO,
      PostDocType = fsContractPostDocRow.PostDocType,
      PostRefNbr = fsContractPostDocRow.PostRefNbr
    }), (PXDBOperation) 2);
    this.CreateBillHistory(graph, fsContractPostDocRow, fsServiceContractRow);
  }

  public virtual void CreateBillHistory(
    PXGraph graph,
    FSContractPostDoc fsContractPostDocRow,
    FSServiceContract fsServiceContractRow)
  {
    PXCache cach = graph.Caches[typeof (FSBillHistory)];
    FSBillHistory fsBillHistory = new FSBillHistory();
    fsBillHistory.BatchID = fsContractPostDocRow.ContractPostBatchID;
    fsBillHistory.ServiceContractRefNbr = fsServiceContractRow.RefNbr;
    if (fsContractPostDocRow.PostedTO == "SO")
      fsBillHistory.ChildEntityType = "PXSO";
    else if (fsContractPostDocRow.PostedTO == "AR")
      fsBillHistory.ChildEntityType = "PXAR";
    fsBillHistory.ChildDocType = fsContractPostDocRow.PostDocType;
    fsBillHistory.ChildRefNbr = fsContractPostDocRow.PostRefNbr;
    cach.Persist((object) (FSBillHistory) cach.Insert((object) fsBillHistory), (PXDBOperation) 2);
  }

  public virtual void CreatePostRegisterAndBillHistory(
    PXGraph graph,
    FSContractPostDoc fsContractPostDocRow,
    FSServiceContract fsServiceContractRow,
    FSSODet soDet,
    FSAppointmentDet apptDet)
  {
    PXCache cach1 = graph.Caches[typeof (FSPostRegister)];
    cach1.Persist((object) (FSPostRegister) cach1.Insert((object) new FSPostRegister()
    {
      Type = "INVCP",
      PostedTO = fsContractPostDocRow.PostedTO,
      PostDocType = fsContractPostDocRow.PostDocType,
      PostRefNbr = fsContractPostDocRow.PostRefNbr,
      EntityType = (apptDet == null ? "SO" : "AP"),
      SrvOrdType = soDet.SrvOrdType,
      RefNbr = (apptDet == null ? soDet.RefNbr : apptDet.RefNbr),
      BatchID = new int?()
    }), (PXDBOperation) 2);
    PXCache cach2 = graph.Caches[typeof (FSBillHistory)];
    FSBillHistory fsBillHistory = new FSBillHistory();
    fsBillHistory.BatchID = new int?();
    fsBillHistory.ServiceContractRefNbr = fsServiceContractRow.RefNbr;
    fsBillHistory.SrvOrdType = soDet.SrvOrdType;
    fsBillHistory.ServiceOrderRefNbr = soDet.RefNbr;
    fsBillHistory.AppointmentRefNbr = apptDet?.RefNbr;
    if (fsContractPostDocRow.PostedTO == "SO")
      fsBillHistory.ChildEntityType = "PXSO";
    else if (fsContractPostDocRow.PostedTO == "AR")
      fsBillHistory.ChildEntityType = "PXAR";
    fsBillHistory.ChildDocType = fsContractPostDocRow.PostDocType;
    fsBillHistory.ChildRefNbr = fsContractPostDocRow.PostRefNbr;
    cach2.Persist((object) (FSBillHistory) cach2.Insert((object) fsBillHistory), (PXDBOperation) 2);
  }

  public virtual FSContractPostDoc CreateContractPostDoc(
    ContractPostPeriodEntry graph,
    FSContractPostDoc fsContractPostDocRow)
  {
    ((PXSelectBase<FSContractPostDoc>) graph.ContractPostDocRecord).Current = ((PXSelectBase<FSContractPostDoc>) graph.ContractPostDocRecord).Insert(fsContractPostDocRow);
    ((PXAction) graph.Save).Press();
    return ((PXSelectBase<FSContractPostDoc>) graph.ContractPostDocRecord).Current;
  }

  public virtual void CreateContractPostDet(
    ContractPostPeriodEntry graph,
    FSContractPostDoc fsContractPostDocRow,
    List<ContractInvoiceLine> contractInvoiceDetLines)
  {
    foreach (ContractInvoiceLine contractInvoiceDetLine in contractInvoiceDetLines)
      ((PXSelectBase<FSContractPostDet>) graph.ContractPostDetRecords).Insert(this.CreatePostDet(fsContractPostDocRow, contractInvoiceDetLine));
    ((PXAction) graph.Save).Press();
  }

  public virtual FSContractPostDet CreatePostDet(
    FSContractPostDoc fsContractPostDocRow,
    ContractInvoiceLine contractInvoiceLine)
  {
    return new FSContractPostDet()
    {
      AppDetID = contractInvoiceLine.AppDetID,
      AppointmentID = contractInvoiceLine.AppointmentID,
      ContractPeriodDetID = contractInvoiceLine.ContractPeriodDetID,
      ContractPeriodID = contractInvoiceLine.ContractPeriodID,
      ContractPostBatchID = fsContractPostDocRow.ContractPostBatchID,
      ContractPostDocID = fsContractPostDocRow.ContractPostDocID,
      SODetID = contractInvoiceLine.SODetID,
      SOID = contractInvoiceLine.SOID
    };
  }

  public virtual void MarkBillingPeriodAsInvoiced(
    FSSetup fsSetupRow,
    FSContractPostDoc fsContractPostDocRow,
    FSServiceContract fsServiceContractRow)
  {
    if (fsServiceContractRow.RecordType == "NRSC")
    {
      ServiceContractEntry instance = PXGraph.CreateInstance<ServiceContractEntry>();
      ((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Search<FSServiceContract.serviceContractID>((object) fsContractPostDocRow.ServiceContractID, new object[1]
      {
        (object) fsServiceContractRow.CustomerID
      }));
      instance.MarkBillingPeriodAsInvoiced(fsSetupRow, fsContractPostDocRow);
    }
    else
    {
      RouteServiceContractEntry instance = PXGraph.CreateInstance<RouteServiceContractEntry>();
      ((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Current = PXResultset<FSServiceContract>.op_Implicit(((PXSelectBase<FSServiceContract>) instance.ServiceContractRecords).Search<FSServiceContract.serviceContractID>((object) fsContractPostDocRow.ServiceContractID, new object[1]
      {
        (object) fsServiceContractRow.CustomerID
      }));
      instance.MarkBillingPeriodAsInvoiced(fsSetupRow, fsContractPostDocRow);
    }
  }
}
