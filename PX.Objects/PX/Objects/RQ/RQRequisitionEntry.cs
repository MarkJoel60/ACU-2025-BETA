// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequisitionEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Models;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.DependencyInjection;
using PX.Data.WorkflowAPI;
using PX.LicensePolicy;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Exceptions;
using PX.Objects.Common.Extensions;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.SO;
using PX.Objects.SO.GraphExtensions.SOOrderEntryExt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.RQ;

public class RQRequisitionEntry : 
  PXGraph<RQRequisitionEntry, RQRequisition>,
  IGraphWithInitialization
{
  public PXFilter<PX.Objects.CR.BAccount> cbaccount;
  public PXFilter<PX.Objects.AP.Vendor> cbendor;
  public PXFilter<EPEmployee> cemployee;
  [PXViewName("Requisition")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (RQRequisition.status), typeof (RQRequisition.hold), typeof (RQRequisition.quoted), typeof (RQRequisition.employeeID)})]
  public PXSelectJoin<RQRequisition, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<RQRequisition.customerID>>, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<RQRequisition.vendorID>>>>, Where2<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>, And<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>> Document;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (RQRequisition.vendorID), typeof (RQRequisition.vendorLocationID), typeof (RQRequisition.vendorRefNbr), typeof (RQRequisition.vendorRequestSent)})]
  public PXSelect<RQRequisition, Where<RQRequisition.reqNbr, Equal<Current<RQRequisition.reqNbr>>>> CurrentDocument;
  public PXSelect<PX.Objects.IN.InventoryItem> invItems;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (RQRequisitionLine.cancelled)})]
  public PXSelect<RQRequisitionLine, Where<RQRequisitionLine.reqNbr, Equal<Optional<RQRequisition.reqNbr>>>> Lines;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<RQBiddingVendor, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<RQBiddingVendor.vendorID>, And<PX.Objects.CR.Location.locationID, Equal<RQBiddingVendor.vendorLocationID>>>>, Where<RQBiddingVendor.reqNbr, Equal<Optional<RQRequisition.reqNbr>>>> Vendors;
  [PXViewName("Employee")]
  public PXSetup<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<RQRequisition.employeeID>>>> Employee;
  public PXSelect<RQBidding, Where<RQBidding.reqNbr, Equal<Current<RQRequisition.reqNbr>>>> Bidding;
  [PXViewName("Ship Address")]
  public PXSelect<POShipAddress, Where<POShipAddress.addressID, Equal<Current<RQRequisition.shipAddressID>>>> Shipping_Address;
  [PXViewName("Ship Contact")]
  public PXSelect<POShipContact, Where<POShipContact.contactID, Equal<Current<RQRequisition.shipContactID>>>> Shipping_Contact;
  [PXViewName("Remit Address")]
  public PXSelect<PX.Objects.PO.PORemitAddress, Where<PX.Objects.PO.PORemitAddress.addressID, Equal<Current<RQRequisition.remitAddressID>>>> Remit_Address;
  [PXViewName("Remit Contact")]
  public PXSelect<PX.Objects.PO.PORemitContact, Where<PX.Objects.PO.PORemitContact.contactID, Equal<Current<RQRequisition.remitContactID>>>> Remit_Contact;
  public PXFilter<RQRequisitionStatic> Filter;
  public PXFilter<RQRequestLineFilter> RequestFilter;
  public PXSelectJoin<RQRequisitionContent, InnerJoin<RQRequestLine, On<RQRequestLine.orderNbr, Equal<RQRequisitionContent.orderNbr>, And<RQRequestLine.lineNbr, Equal<RQRequisitionContent.lineNbr>>>, InnerJoin<RQRequest, On<RQRequest.orderNbr, Equal<RQRequisitionContent.orderNbr>>>>, Where<RQRequisitionContent.reqNbr, Equal<Current<RQRequisitionStatic.reqNbr>>, And<RQRequisitionContent.reqLineNbr, Equal<Current<RQRequisitionStatic.lineNbr>>>>> Contents;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<RQRequestLine, InnerJoin<RQRequest, On<RQRequest.orderNbr, Equal<RQRequestLine.orderNbr>, And<RQRequest.status, Equal<RQRequestStatus.open>>>>, Where<RQRequestLine.openQty, Greater<decimal0>, And<RQRequestLine.orderNbr, Equal<Required<RQRequestLine.orderNbr>>, And<RQRequestLine.lineNbr, Equal<Required<RQRequestLine.lineNbr>>>>>> SourceRequestLines;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<RQRequestLineSelect, InnerJoin<RQRequest, On<RQRequest.orderNbr, Equal<RQRequestLineSelect.orderNbr>, And<RQRequest.status, Equal<RQRequestStatus.open>>>, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<RQRequest.employeeID>>>>, Where<RQRequestLineSelect.openQty, Greater<decimal0>, And2<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>, And<Where<Current<RQRequisition.customerID>, IsNull, Or<RQRequest.employeeID, Equal<Current<RQRequisition.customerID>>, And<RQRequest.locationID, Equal<Current<RQRequisition.customerLocationID>>>>>>>>> SourceRequests;
  public PXSelect<RQRequisitionOrder, Where<RQRequisitionOrder.reqNbr, Equal<Optional<RQRequisition.reqNbr>>>> ReqOrders;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<PX.Objects.PO.POOrder, InnerJoin<RQRequisitionOrder, On<RQRequisitionOrder.orderCategory, Equal<RQOrderCategory.po>, And<RQRequisitionOrder.orderType, Equal<PX.Objects.PO.POOrder.orderType>, And<RQRequisitionOrder.orderNbr, Equal<PX.Objects.PO.POOrder.orderNbr>>>>>, Where<RQRequisitionOrder.reqNbr, Equal<Optional<RQRequisition.reqNbr>>, And<PX.Objects.PO.POOrder.orderType, NotEqual<POOrderType.regularSubcontract>>>> POOrders;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<PX.Objects.SO.SOOrder, InnerJoin<RQRequisitionOrder, On<RQRequisitionOrder.orderCategory, Equal<RQOrderCategory.so>, And<RQRequisitionOrder.orderType, Equal<PX.Objects.SO.SOOrder.orderType>, And<RQRequisitionOrder.orderNbr, Equal<PX.Objects.SO.SOOrder.orderNbr>>>>>, Where<RQRequisitionOrder.reqNbr, Equal<Optional<RQRequisition.reqNbr>>>> SOOrders;
  public PXSelect<PX.Objects.PO.POOrder, Where<PX.Objects.PO.POOrder.rQReqNbr, Equal<Current<RQRequisition.reqNbr>>, And<PX.Objects.PO.POOrder.orderType, NotEqual<POOrderType.regularSubcontract>>>> Orders;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<PX.Objects.PO.POLine, InnerJoin<PX.Objects.PO.POOrder, On<PX.Objects.PO.POOrder.orderType, Equal<PX.Objects.PO.POLine.orderType>, And<PX.Objects.PO.POOrder.orderNbr, Equal<PX.Objects.PO.POLine.orderNbr>>>>, Where<PX.Objects.PO.POLine.rQReqNbr, Equal<Current<RQRequisitionStatic.reqNbr>>, And<PX.Objects.PO.POLine.rQReqLineNbr, Equal<Current<RQRequisitionStatic.lineNbr>>, And<PX.Objects.PO.POLine.orderType, NotEqual<POOrderType.regularSubcontract>>>>> OrderLines;
  [PXHidden]
  public PXSelect<VendorContact, Where<VendorContact.contactID, Equal<Current<PX.Objects.AP.Vendor.defContactID>>>> VndrCont;
  public PXSelect<RQSetupApproval, Where<RQSetupApproval.type, Equal<RQType.requisition>>> SetupApproval;
  [PXViewName("Approval")]
  public EPApprovalAutomation<RQRequisition, RQRequisition.approved, RQRequisition.rejected, RQRequisition.hold, RQSetupApproval> Approval;
  public PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Optional<RQRequisition.vendorID>>>> bAccount;
  public PXSetup<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Optional<RQRequisition.vendorID>>>> vendor;
  public PXSetup<VendorClass, Where<VendorClass.vendorClassID, Equal<Current<PX.Objects.AP.Vendor.vendorClassID>>>> vendorclass;
  public PXSetup<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Optional<PX.Objects.PO.POOrder.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Optional<PX.Objects.PO.POOrder.vendorLocationID>>>>> location;
  public PXSetup<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Optional<RQRequisition.customerID>>>> customer;
  public PXSetup<CustomerClass, Where<CustomerClass.customerClassID, Equal<Optional<PX.Objects.AR.Customer.customerClassID>>>> customerclass;
  public PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<RQBiddingVendor.vendorID>>>> vendorBidder;
  public PXSelect<PX.Objects.PO.PORemitAddress, Where<PX.Objects.PO.PORemitAddress.addressID, Equal<Current<RQBiddingVendor.remitAddressID>>>> Bidding_Remit_Address;
  public PXSelect<PX.Objects.PO.PORemitContact, Where<PX.Objects.PO.PORemitContact.contactID, Equal<Current<RQBiddingVendor.remitContactID>>>> Bidding_Remit_Contact;
  public PXSetup<RQSetup> Setup;
  public PXSetup<PX.Objects.IN.INSetup> INSetup;
  public CMSetupSelect cmsetup;
  public PXSetup<Company> company;
  public PXSelect<RQRequest> request;
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Optional<RQRequisition.curyInfoID>>>> currencyinfo;
  public ToggleCurrency<RQRequisition> CurrencyView;
  private bool _useCustomerCurrency = true;
  public PXWorkflowEventHandler<RQRequisition> OnBiddingCompleted;
  public PXWorkflowEventHandler<RQRequisition, RQRequisitionOrder, PX.Objects.SO.SOOrder> OnSOOrderUnlinked;
  public PXWorkflowEventHandler<RQRequisition, RQRequisitionOrder, PX.Objects.PO.POOrder> OnPOOrderUnlinked;
  public PXWorkflowEventHandler<RQRequisition, PX.Objects.SO.SOOrder> OnSalesOrderDeleted;
  public PXWorkflowEventHandler<RQRequisition, PX.Objects.PO.POOrder> OnPurchaseOrderDeleted;
  public PXAction<RQRequisition> validateAddresses;
  public PXInitializeState<RQRequisition> initializeState;
  public PXAction<RQRequisition> ViewBidding;
  public PXAction<RQRequisition> Transfer;
  public PXAction<RQRequisition> Merge;
  public PXAction<RQRequisition> AddRequestLine;
  public PXAction<RQRequisition> AddRequestContent;
  public PXAction<RQRequisition> ViewDetails;
  public PXAction<RQRequisition> ViewRequest;
  public PXAction<RQRequisition> ViewPOOrder;
  public PXAction<RQRequisition> ViewSOOrder;
  public PXAction<RQRequisition> ViewOrderByLine;
  public PXAction<RQRequisition> Assign;
  public PXAction<RQRequisition> createPOOrder;
  public PXAction<RQRequisition> createQTOrder;
  public PXAction<RQRequisition> ViewLineDetails;
  public PXAction<RQRequisition> ChooseVendor;
  public PXAction<RQRequisition> ResponseVendor;
  public PXAction<RQRequisition> VendorInfo;
  public PXAction<RQRequisition> cancelRequest;
  public PXAction<RQRequisition> markQuoted;
  public PXAction<RQRequisition> putOnHold;
  public PXAction<RQRequisition> releaseFromHold;
  public PXAction<RQRequisition> action;
  public PXAction<RQRequisition> vendorNotifications;
  public PXAction<RQRequisition> sendRequestToCurrentVendor;
  public PXAction<RQRequisition> sendRequestToAllVendors;
  public PXAction<RQRequisition> report;
  public PXAction<RQRequisition> requestForProposal;

  public virtual void Persist()
  {
    ((PXGraph) this).Persist();
    ((PXSelectBase) this.currencyinfo).View.Clear();
  }

  [PXDefault(typeof (RQRequisition.orderDate))]
  [PXMergeAttributes]
  protected virtual void EPApproval_DocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (RQRequisition.employeeID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_BAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (RQRequisition.description))]
  [PXMergeAttributes]
  protected virtual void EPApproval_Descr_CacheAttached(PXCache sender)
  {
  }

  [CurrencyInfo(typeof (RQRequisition.curyInfoID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (RQRequisition.curyEstExtCostTotal))]
  [PXMergeAttributes]
  protected virtual void EPApproval_CuryTotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (RQRequisition.estExtCostTotal))]
  [PXMergeAttributes]
  protected virtual void EPApproval_TotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (RQRequisition.ownerID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_DocumentOwnerID_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  [PXUIField]
  protected virtual void RQBiddingVendor_EntryDate_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.PO.POOrder.orderType, NotEqual<POOrderType.regularSubcontract>>), "Subcontract orders are not supported.", new System.Type[] {})]
  protected virtual void POOrder_OrderNbr_CacheAttached(PXCache cache)
  {
  }

  [PXRemoveBaseAttribute(typeof (CurrencyInfoAttribute))]
  protected virtual void POOrder_CuryInfoID_CacheAttached(PXCache cache)
  {
  }

  [PXRemoveBaseAttribute(typeof (CurrencyInfoAttribute))]
  protected virtual void SOOrder_CuryInfoID_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.PO.POOrder.orderType, NotEqual<POOrderType.regularSubcontract>>), "Subcontract orders are not supported.", new System.Type[] {})]
  protected virtual void POLine_PONbr_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes]
  [InterBranchRestrictor(typeof (Where<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<RQRequisition.branchID>>>))]
  protected virtual void POSiteStatusFilter_SiteID_CacheAttached(PXCache sender)
  {
  }

  public RQRequisitionEntry()
  {
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.SourceRequests).Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<RQRequestLineSelect.selected>(((PXSelectBase) this.SourceRequests).Cache, (object) null, true);
    PXUIFieldAttribute.SetEnabled<RQRequestLineSelect.selectQty>(((PXSelectBase) this.SourceRequests).Cache, (object) null, true);
    ((PXGraph) this).Views.Caches.Add(typeof (RQRequestLine));
    ((PXSelectBase) this.SourceRequests).WhereAndCurrent<RQRequestLineFilter>(typeof (RQRequestSelection.ownerID).Name, typeof (RQRequestSelection.workGroupID).Name);
    if (PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      PXUIFieldAttribute.SetVisible<RQRequestLineFilter.subItemID>(((PXSelectBase) this.RequestFilter).Cache, (object) null, ((PXSelectBase<PX.Objects.IN.INSetup>) this.INSetup).Current.UseInventorySubItem.GetValueOrDefault());
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<PX.Objects.IN.InventoryItem.stkItem>(RQRequisitionEntry.\u003C\u003Ec.\u003C\u003E9__58_0 ?? (RQRequisitionEntry.\u003C\u003Ec.\u003C\u003E9__58_0 = new PXFieldDefaulting((object) RQRequisitionEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__58_0))));
    PXStringListAttribute.SetList<PX.Objects.IN.InventoryItem.itemType>(((PXSelectBase) this.invItems).Cache, (object) null, new string[8]
    {
      "F",
      "M",
      "A",
      "N",
      "L",
      "S",
      "C",
      "E"
    }, new string[8]
    {
      "Finished Good",
      "Component Part",
      "Subassembly",
      "Non-Stock Item",
      "Labor",
      "Service",
      "Charge",
      "Expense"
    });
    ((PXSelectBase) this.POOrders).Cache.AllowInsert = ((PXSelectBase) this.POOrders).Cache.AllowUpdate = ((PXSelectBase) this.POOrders).Cache.AllowDelete = false;
    ((PXSelectBase) this.SOOrders).Cache.AllowInsert = ((PXSelectBase) this.SOOrders).Cache.AllowUpdate = ((PXSelectBase) this.SOOrders).Cache.AllowDelete = false;
  }

  [InjectDependency]
  protected ILicenseLimitsService _licenseLimits { get; set; }

  void IGraphWithInitialization.Initialize()
  {
    if (this._licenseLimits == null)
      return;
    ((PXGraph) this).OnBeforeCommit += this._licenseLimits.GetCheckerDelegate<RQRequisition>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (RQRequisitionLine), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[1]
      {
        (PXDataFieldValue) new PXDataFieldValue<RQRequisitionLine.reqNbr>((object) ((PXSelectBase<RQRequisition>) ((RQRequisitionEntry) graph).Document).Current?.ReqNbr)
      }))
    });
  }

  [PXUIField]
  [PXButton(CommitChanges = true)]
  public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
  {
    RQRequisitionEntry requisitionEntry = this;
    foreach (RQRequisition rqRequisition in adapter.Get<RQRequisition>())
    {
      if (rqRequisition != null)
        ((PXGraph) requisitionEntry).FindAllImplementations<IAddressValidationHelper>().ValidateAddresses();
      yield return (object) rqRequisition;
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable viewBidding(PXAdapter adapter)
  {
    RQRequisitionEntry requisitionEntry = this;
    if (((PXGraph) requisitionEntry).IsDirty)
      ((PXAction) requisitionEntry.Save).Press();
    foreach (RQRequisition rqRequisition in adapter.Get<RQRequisition>())
    {
      RQBiddingProcess instance = PXGraph.CreateInstance<RQBiddingProcess>();
      ((PXSelectBase<RQRequisition>) instance.Document).Current = PXResultset<RQRequisition>.op_Implicit(((PXSelectBase<RQRequisition>) instance.Document).Search<RQRequisition.reqNbr>((object) rqRequisition.ReqNbr, Array.Empty<object>()));
      if (((PXSelectBase<RQRequisition>) instance.Document).Current != null)
      {
        ((PXSelectBase<RQBiddingState>) instance.State).Current.SingleMode = new bool?(true);
        throw new PXPopupRedirectException((PXGraph) instance, "View Bidding", true);
      }
      yield return (object) rqRequisition;
    }
  }

  [PXButton(ImageKey = "DataEntry")]
  [PXUIField(Visible = false)]
  public virtual IEnumerable transfer(PXAdapter adapter)
  {
    RQRequisitionEntry requisitionEntry = this;
    RQRequisition destination = (RQRequisition) null;
    bool flag;
    using (IEnumerator<RQRequisition> enumerator1 = adapter.Get<RQRequisition>().GetEnumerator())
    {
      while (true)
      {
        if (enumerator1.MoveNext())
        {
          RQRequisition current = enumerator1.Current;
          using (IEnumerator<PXResult<RQRequisitionLine>> enumerator2 = PXSelectBase<RQRequisitionLine, PXSelect<RQRequisitionLine, Where<RQRequisitionLine.reqNbr, Equal<Required<RQRequisitionLine.reqNbr>>, And<RQRequisitionLine.transferRequest, Equal<Required<RQRequisitionLine.transferRequest>>>>>.Config>.Select((PXGraph) requisitionEntry, new object[2]
          {
            (object) current.ReqNbr,
            (object) true
          }).GetEnumerator())
          {
            while (true)
            {
              if (enumerator2.MoveNext())
              {
                RQRequisitionLine rqRequisitionLine = PXResult<RQRequisitionLine>.op_Implicit(enumerator2.Current);
                if (destination == null)
                {
                  if (((PXSelectBase<RQRequisitionStatic>) requisitionEntry.Filter).AskExt() == 1)
                  {
                    string reqNbr = ((PXSelectBase<RQRequisitionStatic>) requisitionEntry.Filter).Current.ReqNbr;
                    if (reqNbr != null)
                      ((PXSelectBase<RQRequisition>) requisitionEntry.Document).Current = PXResultset<RQRequisition>.op_Implicit(((PXSelectBase<RQRequisition>) requisitionEntry.Document).Search<RQRequisition.reqNbr>((object) reqNbr, Array.Empty<object>()));
                    else
                      ((PXSelectBase) requisitionEntry.Document).Cache.Insert();
                    destination = ((PXSelectBase<RQRequisition>) requisitionEntry.Document).Current;
                  }
                  else
                    break;
                }
                ((PXSelectBase) requisitionEntry.Lines).Cache.Delete((object) rqRequisitionLine);
                RQRequisitionLine copy = (RQRequisitionLine) ((PXSelectBase) requisitionEntry.Lines).Cache.CreateCopy((object) rqRequisitionLine);
                copy.ReqNbr = destination.ReqNbr;
                copy.LineNbr = new int?();
                copy.AlternateID = (string) null;
                copy.TransferQty = new Decimal?(0M);
                copy.TransferRequest = new bool?(false);
                copy.TransferType = "T";
                copy.SourceTranReqNbr = rqRequisitionLine.ReqNbr;
                copy.SourceTranLineNbr = rqRequisitionLine.LineNbr;
                ((PXSelectBase<RQRequisitionLine>) requisitionEntry.Lines).Insert(copy);
              }
              else
                goto label_16;
            }
            flag = false;
            goto label_22;
          }
label_16:
          yield return (object) current;
        }
        else
          break;
      }
    }
    if (destination == null)
      throw new PXException("There are not selected lines to transfer.");
    yield break;
label_22:
    return flag;
  }

  [PXButton(ImageKey = "BoxIn")]
  [PXUIField(DisplayName = "Merge Lines")]
  public virtual IEnumerable merge(PXAdapter adapter)
  {
    RQRequisitionEntry requisitionEntry = this;
    foreach (RQRequisition rqRequisition in adapter.Get<RQRequisition>())
    {
      PXResultset<RQRequisitionLine> requisitionLines = PXSelectBase<RQRequisitionLine, PXSelect<RQRequisitionLine, Where<RQRequisitionLine.reqNbr, Equal<Required<RQRequisitionLine.reqNbr>>>, OrderBy<Asc<RQRequisitionLine.byRequest, Asc<RQRequisitionLine.inventoryID, Asc<RQRequisitionLine.uOM, Asc<RQRequisitionLine.expenseAcctID, Asc<RQRequisitionLine.expenseSubID>>>>>>>.Config>.Select((PXGraph) requisitionEntry, new object[2]
      {
        (object) rqRequisition.ReqNbr,
        (object) true
      });
      RequisitionLinesMergeResult linesMergeResult = requisitionEntry.MergeLinesOfRequisition(requisitionLines);
      if (!linesMergeResult.Merged && linesMergeResult.ResultLine != null)
      {
        PXSetPropertyException propertyException = new PXSetPropertyException(!linesMergeResult.ResultLine.InventoryID.HasValue ? "Line is excluded from merge, inventory is empty" : "Line cannot be merged with others, lines separated by line source, inventory, UOM and expense account", (PXErrorLevel) 2);
        ((PXSelectBase) requisitionEntry.Lines).Cache.RaiseExceptionHandling<RQRequisitionLine.selected>((object) linesMergeResult.ResultLine, (object) true, (Exception) propertyException);
      }
      yield return (object) rqRequisition;
    }
  }

  protected virtual RequisitionLinesMergeResult MergeLinesOfRequisition(
    PXResultset<RQRequisitionLine> requisitionLines)
  {
    RQRequisitionLine rqRequisitionLine1 = (RQRequisitionLine) null;
    bool merged = false;
    foreach (PXResult<RQRequisitionLine> requisitionLine in requisitionLines)
    {
      RQRequisitionLine lineToMerge = PXResult<RQRequisitionLine>.op_Implicit(requisitionLine);
      bool? nullable1 = lineToMerge.Selected;
      if (nullable1.GetValueOrDefault())
      {
        int? nullable2;
        if (rqRequisitionLine1 != null)
        {
          int? nullable3 = rqRequisitionLine1.InventoryID;
          nullable2 = lineToMerge.InventoryID;
          if (nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue)
          {
            nullable2 = rqRequisitionLine1.ExpenseAcctID;
            nullable3 = lineToMerge.ExpenseAcctID;
            if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
            {
              nullable3 = rqRequisitionLine1.ExpenseSubID;
              nullable2 = lineToMerge.ExpenseSubID;
              if (nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue)
              {
                nullable1 = rqRequisitionLine1.ByRequest;
                bool? byRequest = lineToMerge.ByRequest;
                if (nullable1.GetValueOrDefault() == byRequest.GetValueOrDefault() & nullable1.HasValue == byRequest.HasValue)
                {
                  merged = true;
                  Decimal? curyEstUnitCost = lineToMerge.CuryEstUnitCost;
                  Decimal? nullable4 = rqRequisitionLine1.CuryEstExtCost;
                  Decimal num = 0M;
                  Decimal? curyEstExtCost;
                  if (nullable4.GetValueOrDefault() == num & nullable4.HasValue)
                  {
                    RQRequisitionLine rqRequisitionLine2 = rqRequisitionLine1;
                    nullable4 = curyEstUnitCost;
                    Decimal? orderQty = rqRequisitionLine1.OrderQty;
                    Decimal? nullable5 = nullable4.HasValue & orderQty.HasValue ? new Decimal?(nullable4.GetValueOrDefault() * orderQty.GetValueOrDefault()) : new Decimal?();
                    rqRequisitionLine2.CuryEstExtCost = nullable5;
                    curyEstExtCost = rqRequisitionLine1.CuryEstExtCost;
                  }
                  else
                    curyEstExtCost = lineToMerge.CuryEstExtCost;
                  byRequest = lineToMerge.ByRequest;
                  rqRequisitionLine1 = !byRequest.GetValueOrDefault() ? this.MergeRequisitionLineCreatedFromDraft(rqRequisitionLine1, lineToMerge, curyEstUnitCost, curyEstExtCost) : this.MergeRequisitionLineCreatedFromRequest(rqRequisitionLine1, lineToMerge, curyEstUnitCost, curyEstExtCost);
                  ((PXSelectBase) this.Lines).Cache.Delete((object) lineToMerge);
                  continue;
                }
              }
            }
          }
        }
        if (rqRequisitionLine1 != null && !merged)
        {
          nullable2 = rqRequisitionLine1.InventoryID;
          string str = !nullable2.HasValue ? "Line is excluded from merge, inventory is empty" : "Line cannot be merged with others, lines separated by line source, inventory, UOM and expense account";
          ((PXSelectBase) this.Lines).Cache.RaiseExceptionHandling<RQRequisitionLine.selected>((object) rqRequisitionLine1, (object) true, (Exception) new PXSetPropertyException(str, (PXErrorLevel) 2));
        }
        rqRequisitionLine1 = lineToMerge;
        rqRequisitionLine1.Selected = new bool?(false);
        merged = false;
      }
    }
    return new RequisitionLinesMergeResult(merged, rqRequisitionLine1);
  }

  private RQRequisitionLine MergeRequisitionLineCreatedFromRequest(
    RQRequisitionLine destLine,
    RQRequisitionLine lineToMerge,
    Decimal? curyEstUnitCost,
    Decimal? curyEstExtCost)
  {
    Decimal? curyEstExtCost1 = destLine.CuryEstExtCost;
    Decimal? nullable1 = curyEstExtCost;
    Decimal? nullable2 = curyEstExtCost1.HasValue & nullable1.HasValue ? new Decimal?(curyEstExtCost1.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    foreach (PXResult<RQRequisitionContent, RQRequestLine> pxResult in PXSelectBase<RQRequisitionContent, PXSelectJoin<RQRequisitionContent, InnerJoin<RQRequestLine, On<RQRequestLine.orderNbr, Equal<RQRequisitionContent.orderNbr>, And<RQRequestLine.lineNbr, Equal<RQRequisitionContent.lineNbr>>>>, Where<RQRequisitionContent.reqNbr, Equal<Required<RQRequisitionContent.reqNbr>>, And<RQRequisitionContent.reqLineNbr, Equal<Required<RQRequisitionContent.reqLineNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) lineToMerge.ReqNbr,
      (object) lineToMerge.LineNbr
    }))
    {
      RQRequisitionContent requisitionContent1 = PXResult<RQRequisitionContent, RQRequestLine>.op_Implicit(pxResult);
      RQRequestLine rqRequestLine = PXResult<RQRequisitionContent, RQRequestLine>.op_Implicit(pxResult);
      ((PXSelectBase<RQRequisitionContent>) this.Contents).Delete(requisitionContent1);
      RQRequisitionLine rqLine = destLine;
      RQRequestLine requestLine = rqRequestLine;
      nullable1 = requisitionContent1.ItemQty;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      RQRequisitionContent requisitionContent2 = this.UpdateContent(rqLine, requestLine, valueOrDefault1);
      nullable1 = requisitionContent1.ItemQty;
      if (nullable1.GetValueOrDefault() > 0M)
      {
        nullable1 = requisitionContent2.ReqQty;
        if (nullable1.GetValueOrDefault() == 0M)
        {
          RQRequisitionContent requisitionContent3 = requisitionContent2;
          PXCache cache = ((PXSelectBase) this.Lines).Cache;
          int? inventoryId = lineToMerge.InventoryID;
          string uom = lineToMerge.UOM;
          nullable1 = requisitionContent2.BaseReqQty;
          Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
          Decimal? nullable3 = new Decimal?(INUnitAttribute.ConvertFromBase(cache, inventoryId, uom, valueOrDefault2, INPrecision.QUANTITY));
          requisitionContent3.ReqQty = nullable3;
          ((PXSelectBase<RQRequisitionContent>) this.Contents).Update(requisitionContent2);
        }
      }
      nullable1 = destLine.CuryEstUnitCost;
      Decimal? nullable4 = curyEstUnitCost;
      if (!(nullable1.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable1.HasValue == nullable4.HasValue))
      {
        destLine = PXCache<RQRequisitionLine>.CreateCopy(destLine);
        RQRequisitionLine rqRequisitionLine = destLine;
        nullable4 = nullable2;
        nullable1 = destLine.OrderQty;
        Decimal? nullable5 = nullable4.HasValue & nullable1.HasValue ? new Decimal?(nullable4.GetValueOrDefault() / nullable1.GetValueOrDefault()) : new Decimal?();
        rqRequisitionLine.CuryEstUnitCost = nullable5;
        destLine = (RQRequisitionLine) ((PXSelectBase) this.Lines).Cache.Update((object) destLine);
      }
    }
    return destLine;
  }

  private RQRequisitionLine MergeRequisitionLineCreatedFromDraft(
    RQRequisitionLine destLine,
    RQRequisitionLine lineToMerge,
    Decimal? curyEstUnitCost,
    Decimal? curyEstExtCostFromLineToMerge)
  {
    destLine = PXCache<RQRequisitionLine>.CreateCopy(destLine);
    Decimal? nullable1;
    Decimal? nullable2;
    if (!(destLine.UOM == lineToMerge.UOM))
    {
      PXCache cache = ((PXSelectBase) this.Lines).Cache;
      int? inventoryId = lineToMerge.InventoryID;
      string uom = lineToMerge.UOM;
      nullable1 = lineToMerge.BaseOrderQty;
      Decimal valueOrDefault = nullable1.GetValueOrDefault();
      nullable2 = new Decimal?(INUnitAttribute.ConvertFromBase(cache, inventoryId, uom, valueOrDefault, INPrecision.QUANTITY));
    }
    else
      nullable2 = lineToMerge.OrderQty;
    Decimal? nullable3 = nullable2;
    nullable1 = destLine.CuryEstUnitCost;
    Decimal? nullable4 = curyEstUnitCost;
    Decimal? nullable5;
    if (!(nullable1.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable1.HasValue == nullable4.HasValue))
    {
      RQRequisitionLine rqRequisitionLine = destLine;
      Decimal? curyEstExtCost = destLine.CuryEstExtCost;
      Decimal? nullable6 = curyEstExtCostFromLineToMerge;
      nullable4 = curyEstExtCost.HasValue & nullable6.HasValue ? new Decimal?(curyEstExtCost.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
      nullable6 = destLine.OrderQty;
      nullable5 = nullable3;
      nullable1 = nullable6.HasValue & nullable5.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable7;
      if (!(nullable4.HasValue & nullable1.HasValue))
      {
        nullable5 = new Decimal?();
        nullable7 = nullable5;
      }
      else
        nullable7 = new Decimal?(nullable4.GetValueOrDefault() / nullable1.GetValueOrDefault());
      rqRequisitionLine.CuryEstUnitCost = nullable7;
    }
    RQRequisitionLine rqRequisitionLine1 = destLine;
    nullable1 = rqRequisitionLine1.OrderQty;
    nullable4 = nullable3;
    Decimal? nullable8;
    if (!(nullable1.HasValue & nullable4.HasValue))
    {
      nullable5 = new Decimal?();
      nullable8 = nullable5;
    }
    else
      nullable8 = new Decimal?(nullable1.GetValueOrDefault() + nullable4.GetValueOrDefault());
    rqRequisitionLine1.OrderQty = nullable8;
    destLine = (RQRequisitionLine) ((PXSelectBase) this.Lines).Cache.Update((object) destLine);
    return destLine;
  }

  [PXButton(ImageKey = "DataEntry")]
  [PXUIField(DisplayName = "Add Requested Items")]
  public virtual IEnumerable addRequestLine(PXAdapter adapter)
  {
    RQRequisitionEntry requisitionEntry1 = this;
    // ISSUE: method pointer
    PXView.InitializePanel initAction = new PXView.InitializePanel((object) requisitionEntry1, __methodptr(\u003CaddRequestLine\u003Eb__82_0));
    foreach (RQRequisition rqRequisition in adapter.Get<RQRequisition>())
    {
      bool? nullable = rqRequisition.Hold;
      if (nullable.GetValueOrDefault() && ((PXSelectBase<RQRequestLineSelect>) requisitionEntry1.SourceRequests).AskExt(initAction) == 1)
      {
        foreach (PXResult<RQRequestLineSelect> pxResult in ((PXSelectBase<RQRequestLineSelect>) requisitionEntry1.SourceRequests).Select(Array.Empty<object>()))
        {
          RQRequestLineSelect requestLineSelect = PXResult<RQRequestLineSelect>.op_Implicit(pxResult);
          nullable = requestLineSelect.Selected;
          if (nullable.GetValueOrDefault())
          {
            Decimal? selectQty = requestLineSelect.SelectQty;
            Decimal num1 = 0M;
            if (selectQty.GetValueOrDefault() > num1 & selectQty.HasValue)
            {
              RQRequestLine rqRequestLine = PXResultset<RQRequestLine>.op_Implicit(((PXSelectBase<RQRequestLine>) requisitionEntry1.SourceRequestLines).SelectWindowed(0, 1, new object[2]
              {
                (object) requestLineSelect.OrderNbr,
                (object) requestLineSelect.LineNbr
              }));
              if (rqRequestLine != null)
              {
                RQRequisitionEntry requisitionEntry2 = requisitionEntry1;
                RQRequestLine line = rqRequestLine;
                selectQty = requestLineSelect.SelectQty;
                Decimal valueOrDefault = selectQty.GetValueOrDefault();
                nullable = ((PXSelectBase<RQRequestLineFilter>) requisitionEntry1.RequestFilter).Current.AddExists;
                int num2 = nullable.GetValueOrDefault() ? 1 : 0;
                requisitionEntry2.InsertRequestLine(line, valueOrDefault, num2 != 0);
              }
            }
          }
        }
        ((PXSelectBase) requisitionEntry1.SourceRequests).Cache.Clear();
        ((PXSelectBase) requisitionEntry1.SourceRequests).View.Clear();
        ((PXSelectBase) requisitionEntry1.SourceRequests).View.RequestRefresh();
      }
      yield return (object) rqRequisition;
    }
  }

  [PXButton(ImageKey = "AddNew")]
  [PXUIField(DisplayName = "Add Requested Items")]
  public virtual IEnumerable addRequestContent(PXAdapter adapter)
  {
    RQRequisitionEntry requisitionEntry1 = this;
    // ISSUE: method pointer
    PXView.InitializePanel initAction = new PXView.InitializePanel((object) requisitionEntry1, __methodptr(\u003CaddRequestContent\u003Eb__84_0));
    foreach (RQRequisition rqRequisition in adapter.Get<RQRequisition>())
    {
      bool? nullable = rqRequisition.Hold;
      if (nullable.GetValueOrDefault() && ((PXSelectBase<RQRequestLineSelect>) requisitionEntry1.SourceRequests).AskExt(initAction) == 1)
      {
        foreach (PXResult<RQRequestLineSelect> pxResult in ((PXSelectBase<RQRequestLineSelect>) requisitionEntry1.SourceRequests).Select(Array.Empty<object>()))
        {
          RQRequestLineSelect requestLineSelect = PXResult<RQRequestLineSelect>.op_Implicit(pxResult);
          nullable = requestLineSelect.Selected;
          if (nullable.GetValueOrDefault())
          {
            Decimal? selectQty = requestLineSelect.SelectQty;
            Decimal num = 0M;
            if (selectQty.GetValueOrDefault() > num & selectQty.HasValue)
            {
              RQRequestLine rqRequestLine = PXResultset<RQRequestLine>.op_Implicit(((PXSelectBase<RQRequestLine>) requisitionEntry1.SourceRequestLines).SelectWindowed(0, 1, new object[2]
              {
                (object) requestLineSelect.OrderNbr,
                (object) requestLineSelect.LineNbr
              }));
              RQRequisitionEntry requisitionEntry2 = requisitionEntry1;
              RQRequestLine line = rqRequestLine;
              selectQty = requestLineSelect.SelectQty;
              Decimal valueOrDefault = selectQty.GetValueOrDefault();
              requisitionEntry2.InsertRequestLine(line, valueOrDefault, true);
            }
          }
          requestLineSelect.Selected = new bool?(false);
          requestLineSelect.SelectQty = new Decimal?(0M);
          ((PXSelectBase) requisitionEntry1.SourceRequests).Cache.Update((object) requestLineSelect);
        }
        ((PXSelectBase) requisitionEntry1.Contents).View.RequestRefresh();
      }
      yield return (object) rqRequisition;
    }
  }

  [PXButton(ImageKey = "DataEntry")]
  [PXUIField]
  public virtual IEnumerable viewDetails(PXAdapter adapter)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    PXView.InitializePanel initializePanel = RQRequisitionEntry.\u003C\u003Ec.\u003C\u003E9__86_0 ?? (RQRequisitionEntry.\u003C\u003Ec.\u003C\u003E9__86_0 = new PXView.InitializePanel((object) RQRequisitionEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CviewDetails\u003Eb__86_0)));
    if (((PXSelectBase<RQRequisitionLine>) this.Lines).Current != null && ((PXSelectBase<RQRequisitionLine>) this.Lines).Current.ByRequest.GetValueOrDefault())
      ((PXSelectBase<RQRequisitionContent>) this.Contents).AskExt(initializePanel);
    ((PXSelectBase<RQRequisitionContent>) this.Contents).ClearDialog();
    return adapter.Get();
  }

  [PXLookupButton]
  [PXUIField]
  public virtual IEnumerable viewRequest(PXAdapter adapter)
  {
    if (((PXSelectBase<RQRequisitionContent>) this.Contents).Current != null)
      new EntityHelper((PXGraph) this).NavigateToRow(typeof (RQRequest).FullName, new object[1]
      {
        (object) ((PXSelectBase<RQRequisitionContent>) this.Contents).Current.OrderNbr
      }, (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  [PXLookupButton]
  [PXUIField]
  public virtual IEnumerable viewPOOrder(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.PO.POOrder>) this.POOrders).Current != null)
    {
      POOrderEntry instance = PXGraph.CreateInstance<POOrderEntry>();
      ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Current = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Search<PX.Objects.PO.POOrder.orderNbr>((object) ((PXSelectBase<PX.Objects.PO.POOrder>) this.POOrders).Current.OrderNbr, new object[1]
      {
        (object) ((PXSelectBase<PX.Objects.PO.POOrder>) this.POOrders).Current.OrderType
      }));
      if (((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Current != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Order");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [PXLookupButton]
  [PXUIField]
  public virtual IEnumerable viewSOOrder(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) this.SOOrders).Current != null)
    {
      SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
      ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.SOOrders).Current.OrderNbr, new object[1]
      {
        (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) this.SOOrders).Current.OrderType
      }));
      if (((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Order");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [PXLookupButton]
  [PXUIField]
  public virtual IEnumerable viewOrderByLine(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.PO.POLine>) this.OrderLines).Current != null)
    {
      POOrderEntry instance = PXGraph.CreateInstance<POOrderEntry>();
      ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Current = PXResultset<PX.Objects.PO.POOrder>.op_Implicit(((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Search<PX.Objects.PO.POOrder.orderNbr>((object) ((PXSelectBase<PX.Objects.PO.POLine>) this.OrderLines).Current.OrderNbr, new object[1]
      {
        (object) ((PXSelectBase<PX.Objects.PO.POLine>) this.OrderLines).Current.OrderType
      }));
      if (((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Current != null)
      {
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Order");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [PXButton(ImageKey = "DataEntry")]
  [PXUIField(DisplayName = "Assign", Visible = false)]
  public virtual IEnumerable assign(PXAdapter adapter)
  {
    foreach (RQRequisition rqRequisition in adapter.Get<RQRequisition>())
    {
      if (((PXSelectBase<RQSetup>) this.Setup).Current.RequisitionAssignmentMapID.HasValue)
      {
        new EPAssignmentProcessor<RQRequisition>().Assign(rqRequisition, ((PXSelectBase<RQSetup>) this.Setup).Current.RequisitionAssignmentMapID);
        rqRequisition.WorkgroupID = rqRequisition.ApprovalWorkgroupID;
        rqRequisition.OwnerID = rqRequisition.ApprovalOwnerID;
      }
      yield return (object) rqRequisition;
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable CreatePOOrder(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    RQRequisitionEntry.\u003C\u003Ec__DisplayClass98_0 cDisplayClass980 = new RQRequisitionEntry.\u003C\u003Ec__DisplayClass98_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass980.list = adapter.Get<RQRequisition>().ToList<RQRequisition>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass980.massProcess = adapter.MassProcess;
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass980, __methodptr(\u003CCreatePOOrder\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass980.list;
  }

  protected virtual void ProcessToPOOrder(RQRequisition item, bool massProcess)
  {
    try
    {
      if (massProcess)
        PXProcessing<RQRequisition>.SetCurrentItem((object) item);
      ((PXSelectBase<RQRequisition>) this.Document).Current = item;
      bool flag1 = true;
      foreach (PXResult<RQRequisitionLine> pxResult in ((PXSelectBase<RQRequisitionLine>) this.Lines).Select(new object[1]
      {
        (object) item.ReqNbr
      }))
      {
        if (!this.ValidateOpenState(PXResult<RQRequisitionLine>.op_Implicit(pxResult), (PXErrorLevel) 4))
          flag1 = false;
      }
      if (!flag1)
        throw new PXRowPersistingException(typeof (RQRequisition).Name, (object) item, "Unable to create orders, not all required fields are defined.");
      ((PXSelectBase<RQRequisition>) this.Document).Current = PXResultset<RQRequisition>.op_Implicit(((PXSelectBase<RQRequisition>) this.Document).Search<RQRequisition.reqNbr>((object) item.ReqNbr, Array.Empty<object>()));
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        POOrderEntry instance = PXGraph.CreateInstance<POOrderEntry>();
        ((PXGraph) instance).TimeStamp = ((PXGraph) this).TimeStamp;
        ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Current = (PX.Objects.PO.POOrder) null;
        RQRequisitionEntry.PO4SO po4so = new RQRequisitionEntry.PO4SO();
        PX.Objects.PO.POOrder[] array = GraphHelper.RowCast<PX.Objects.PO.POOrder>((IEnumerable) ((PXSelectBase) this.POOrders).View.SelectMultiBound((object[]) new RQRequisition[1]
        {
          item
        }, Array.Empty<object>())).ToArray<PX.Objects.PO.POOrder>();
        int? key1;
        if (item.VendorID.HasValue)
        {
          key1 = item.VendorLocationID;
          if (key1.HasValue && !((IEnumerable<PX.Objects.PO.POOrder>) array).Any<PX.Objects.PO.POOrder>((Func<PX.Objects.PO.POOrder, bool>) (order =>
          {
            int? vendorId1 = order.VendorID;
            int? vendorId2 = item.VendorID;
            if (!(vendorId1.GetValueOrDefault() == vendorId2.GetValueOrDefault() & vendorId1.HasValue == vendorId2.HasValue))
              return false;
            int? vendorLocationId1 = order.VendorLocationID;
            int? vendorLocationId2 = item.VendorLocationID;
            return vendorLocationId1.GetValueOrDefault() == vendorLocationId2.GetValueOrDefault() & vendorLocationId1.HasValue == vendorLocationId2.HasValue;
          })))
          {
            PX.Objects.PO.POOrder poOrder = (PX.Objects.PO.POOrder) ((PXSelectBase) instance.Document).Cache.CreateInstance();
            poOrder.OrderType = item.POType;
            poOrder.OrderDesc = item.Description;
            using (IEnumerator<PXResult<RQRequisitionLine>> enumerator = PXSelectBase<RQRequisitionLine, PXSelectJoin<RQRequisitionLine, LeftJoin<RQBidding, On<RQBidding.reqNbr, Equal<RQRequisitionLine.reqNbr>, And<RQBidding.lineNbr, Equal<RQRequisitionLine.lineNbr>, And<RQBidding.vendorID, Equal<Current<RQRequisition.vendorID>>, And<RQBidding.vendorLocationID, Equal<Current<RQRequisition.vendorLocationID>>>>>>, LeftJoin<RQBiddingVendor, On<RQBiddingVendor.reqNbr, Equal<Current<RQRequisition.reqNbr>>, And<RQBiddingVendor.vendorID, Equal<Current<RQRequisition.vendorID>>, And<RQBiddingVendor.vendorLocationID, Equal<Current<RQRequisition.vendorLocationID>>>>>, LeftJoin<RQRequisitionLineCustomers, On<RQRequisitionLineCustomers.reqNbr, Equal<RQRequisitionLine.reqNbr>, And<RQRequisitionLineCustomers.reqLineNbr, Equal<RQRequisitionLine.lineNbr>>>>>>, Where<RQRequisitionLine.reqNbr, Equal<Current<RQRequisition.reqNbr>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
            {
              (object) item
            }, Array.Empty<object>()).GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                PXResult<RQRequisitionLine, RQBidding, RQBiddingVendor, RQRequisitionLineCustomers> current = (PXResult<RQRequisitionLine, RQBidding, RQBiddingVendor, RQRequisitionLineCustomers>) enumerator.Current;
                RQRequisitionLine rqRequisitionLine = PXResult<RQRequisitionLine, RQBidding, RQBiddingVendor, RQRequisitionLineCustomers>.op_Implicit(current);
                RQRequisitionLineCustomers requisitionLineCustomers = PXResult<RQRequisitionLine, RQBidding, RQBiddingVendor, RQRequisitionLineCustomers>.op_Implicit(current);
                RQBiddingVendor rqBiddingVendor = PXResult<RQRequisitionLine, RQBidding, RQBiddingVendor, RQRequisitionLineCustomers>.op_Implicit(current);
                RQBidding costOriginDac = PXResult<RQRequisitionLine, RQBidding, RQBiddingVendor, RQRequisitionLineCustomers>.op_Implicit(current);
                Decimal num1 = 0M;
                Decimal? nullable = rqRequisitionLine.OrderQty;
                Decimal num2 = nullable.Value;
                nullable = costOriginDac.OrderQty;
                if (nullable.HasValue)
                {
                  nullable = costOriginDac.OrderQty;
                  Decimal num3 = 0M;
                  if (nullable.GetValueOrDefault() == num3 & nullable.HasValue)
                  {
                    Decimal num4 = num2;
                    nullable = costOriginDac.MinQty;
                    Decimal valueOrDefault = nullable.GetValueOrDefault();
                    if (!(num4 < valueOrDefault & nullable.HasValue))
                      goto label_22;
                  }
                  else
                    goto label_22;
                }
                costOriginDac = new RQBidding()
                {
                  MinQty = new Decimal?(0M),
                  QuoteQty = new Decimal?(0M),
                  OrderQty = new Decimal?(0M),
                  CuryQuoteUnitCost = new Decimal?(0M)
                };
label_22:
                nullable = costOriginDac.OrderQty;
                Decimal num5 = 0M;
                if (nullable.GetValueOrDefault() == num5 & nullable.HasValue)
                  costOriginDac.OrderQty = costOriginDac.QuoteQty;
                nullable = requisitionLineCustomers.ReqQty;
                Decimal num6 = 0M;
                if (nullable.GetValueOrDefault() > num6 & nullable.HasValue)
                {
                  nullable = requisitionLineCustomers.ReqQty;
                  num1 = nullable.Value;
                }
                key1 = item.CustomerLocationID;
                if (key1.HasValue)
                  num1 = num2;
                if (num1 > 0M)
                  num2 -= num1;
                if (poOrder != null)
                {
                  PX.Objects.PO.POOrder copy1 = PXCache<PX.Objects.PO.POOrder>.CreateCopy(((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Insert(poOrder));
                  if (costOriginDac.CuryInfoID.HasValue)
                  {
                    copy1.CuryID = (string) ((PXSelectBase<RQBidding>) this.Bidding).GetValueExt<RQBidding.curyID>(costOriginDac);
                    copy1.CuryInfoID = this.CopyCurrencyInfo((PXGraph) instance, costOriginDac.CuryInfoID, true);
                  }
                  else
                  {
                    copy1.CuryID = item.CuryID;
                    copy1.CuryInfoID = this.CopyCurrencyInfo((PXGraph) instance, item.CuryInfoID, true);
                  }
                  PX.Objects.PO.POOrder copy2 = PXCache<PX.Objects.PO.POOrder>.CreateCopy(((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Update(copy1));
                  copy2.VendorID = item.VendorID;
                  copy2.VendorLocationID = item.VendorLocationID;
                  copy2.RemitAddressID = item.RemitAddressID;
                  copy2.RemitContactID = item.RemitContactID;
                  copy2.VendorRefNbr = item.VendorRefNbr;
                  copy2.TermsID = item.TermsID;
                  PX.Objects.PO.POOrder copy3 = PXCache<PX.Objects.PO.POOrder>.CreateCopy(((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Update(copy2));
                  if (rqBiddingVendor != null && rqBiddingVendor.PromisedDate.HasValue)
                    copy3.ExpectedDate = rqBiddingVendor.PromisedDate;
                  copy3.BranchID = item.BranchID;
                  copy3.ShipDestType = item.ShipDestType;
                  copy3.SiteID = item.SiteID;
                  copy3.ShipToBAccountID = item.ShipToBAccountID;
                  copy3.ShipToLocationID = item.ShipToLocationID;
                  copy3.ShipContactID = item.ShipContactID;
                  copy3.ShipAddressID = item.ShipAddressID;
                  copy3.ShipContactID = item.ShipContactID;
                  copy3.FOBPoint = rqBiddingVendor.FOBPoint ?? item.FOBPoint;
                  copy3.ShipVia = rqBiddingVendor.ShipVia ?? item.ShipVia;
                  copy3.RQReqNbr = item.ReqNbr;
                  ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Update(copy3);
                  poOrder = (PX.Objects.PO.POOrder) null;
                }
                object obj;
                ((PXSelectBase) this.Lines).Cache.RaiseFieldDefaulting<RQRequisitionLine.lineType>((object) rqRequisitionLine, ref obj);
                string lineType1 = (string) obj;
                PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) instance, rqRequisitionLine.InventoryID);
                string lineType2 = !(item.POType == "DP") ? (inventoryItem == null || !inventoryItem.StkItem.GetValueOrDefault() ? lineType1 : "GS") : (inventoryItem == null || !inventoryItem.StkItem.GetValueOrDefault() ? (inventoryItem == null || !inventoryItem.NonStockReceipt.GetValueOrDefault() || inventoryItem == null || !inventoryItem.NonStockShip.GetValueOrDefault() ? rqRequisitionLine.LineType : "NP") : "GP");
                Decimal num7 = num1;
                nullable = costOriginDac.OrderQty;
                Decimal valueOrDefault1 = nullable.GetValueOrDefault();
                Decimal num8;
                if (!(num7 < valueOrDefault1 & nullable.HasValue))
                {
                  nullable = costOriginDac.OrderQty;
                  num8 = nullable.Value;
                }
                else
                  num8 = num1;
                Decimal num9 = num8;
                po4so.Add(rqRequisitionLine.LineNbr, this.InsertPOLine(instance, rqRequisitionLine, new Decimal?(num9), costOriginDac.CuryQuoteUnitCost, (IBqlTable) costOriginDac, lineType2, (DateTime?) rqBiddingVendor?.PromisedDate));
                Decimal num10 = num1 - num9;
                RQBidding rqBidding = costOriginDac;
                nullable = rqBidding.OrderQty;
                Decimal num11 = num9;
                rqBidding.OrderQty = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() - num11) : new Decimal?();
                if (num10 > 0M)
                  po4so.Add(rqRequisitionLine.LineNbr, this.InsertPOLine(instance, rqRequisitionLine, new Decimal?(num10), rqRequisitionLine.CuryEstUnitCost, (IBqlTable) rqRequisitionLine, lineType2, (DateTime?) rqBiddingVendor?.PromisedDate));
                Decimal num12 = num2;
                nullable = costOriginDac.OrderQty;
                Decimal valueOrDefault2 = nullable.GetValueOrDefault();
                Decimal num13;
                if (!(num12 < valueOrDefault2 & nullable.HasValue))
                {
                  nullable = costOriginDac.OrderQty;
                  num13 = nullable.Value;
                }
                else
                  num13 = num2;
                Decimal num14 = num13;
                this.InsertPOLine(instance, rqRequisitionLine, new Decimal?(num14), costOriginDac.CuryQuoteUnitCost, (IBqlTable) costOriginDac, lineType1, (DateTime?) rqBiddingVendor?.PromisedDate);
                Decimal num15 = num2 - num14;
                if (num15 > 0M)
                  this.InsertPOLine(instance, rqRequisitionLine, new Decimal?(num15), rqRequisitionLine.CuryEstUnitCost, (IBqlTable) rqRequisitionLine, lineType1, (DateTime?) rqBiddingVendor?.PromisedDate);
              }
              goto label_87;
            }
          }
        }
        key1 = item.VendorID;
        if (key1.HasValue)
        {
          key1 = item.VendorLocationID;
          if (key1.HasValue)
            goto label_87;
        }
        Dictionary<int?, Decimal> dictionary1 = new Dictionary<int?, Decimal>();
        foreach (PXResult<RQBidding, RQRequisitionLine, RQRequisitionLineCustomers, PX.Objects.AP.Vendor, RQBiddingVendor> pxResult in PXSelectBase<RQBidding, PXSelectJoin<RQBidding, InnerJoin<RQRequisitionLine, On<RQRequisitionLine.reqNbr, Equal<RQBidding.reqNbr>, And<RQRequisitionLine.lineNbr, Equal<RQBidding.lineNbr>>>, LeftJoin<RQRequisitionLineCustomers, On<RQRequisitionLineCustomers.reqNbr, Equal<RQRequisitionLine.reqNbr>, And<RQRequisitionLineCustomers.reqLineNbr, Equal<RQRequisitionLine.lineNbr>>>, InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<RQBidding.vendorID>>, LeftJoin<RQBiddingVendor, On<RQBiddingVendor.reqNbr, Equal<RQBidding.reqNbr>, And<RQBiddingVendor.vendorID, Equal<RQBidding.vendorID>, And<RQBiddingVendor.vendorLocationID, Equal<RQBidding.vendorLocationID>>>>>>>>, Where<RQBidding.reqNbr, Equal<Required<RQBidding.reqNbr>>, And<RQBidding.orderQty, Greater<decimal0>>>, OrderBy<Asc<RQBidding.vendorID, Asc<RQBidding.vendorLocationID, Asc<RQRequisitionLine.lineNbr>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) item.ReqNbr
        }))
        {
          RQBidding bidding = PXResult<RQBidding, RQRequisitionLine, RQRequisitionLineCustomers, PX.Objects.AP.Vendor, RQBiddingVendor>.op_Implicit(pxResult);
          RQRequisitionLine line = PXResult<RQBidding, RQRequisitionLine, RQRequisitionLineCustomers, PX.Objects.AP.Vendor, RQBiddingVendor>.op_Implicit(pxResult);
          RQBiddingVendor rqBiddingVendor = PXResult<RQBidding, RQRequisitionLine, RQRequisitionLineCustomers, PX.Objects.AP.Vendor, RQBiddingVendor>.op_Implicit(pxResult);
          RQRequisitionLineCustomers requisitionLineCustomers = PXResult<RQBidding, RQRequisitionLine, RQRequisitionLineCustomers, PX.Objects.AP.Vendor, RQBiddingVendor>.op_Implicit(pxResult);
          PXResult<RQBidding, RQRequisitionLine, RQRequisitionLineCustomers, PX.Objects.AP.Vendor, RQBiddingVendor>.op_Implicit(pxResult);
          PX.Objects.PO.POOrder poOrder1 = (PX.Objects.PO.POOrder) null;
          bool flag2 = ((IEnumerable<PX.Objects.PO.POOrder>) array).Any<PX.Objects.PO.POOrder>((Func<PX.Objects.PO.POOrder, bool>) (order =>
          {
            int? vendorId3 = order.VendorID;
            int? vendorId4 = bidding.VendorID;
            if (!(vendorId3.GetValueOrDefault() == vendorId4.GetValueOrDefault() & vendorId3.HasValue == vendorId4.HasValue))
              return false;
            int? vendorLocationId3 = order.VendorLocationID;
            int? vendorLocationId4 = bidding.VendorLocationID;
            return vendorLocationId3.GetValueOrDefault() == vendorLocationId4.GetValueOrDefault() & vendorLocationId3.HasValue == vendorLocationId4.HasValue;
          }));
          if (!flag2 && ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Current != null)
          {
            key1 = ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Current.VendorID;
            int? nullable = bidding.VendorID;
            if (key1.GetValueOrDefault() == nullable.GetValueOrDefault() & key1.HasValue == nullable.HasValue)
            {
              nullable = ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Current.VendorLocationID;
              key1 = bidding.VendorLocationID;
              if (nullable.GetValueOrDefault() == key1.GetValueOrDefault() & nullable.HasValue == key1.HasValue)
                goto label_62;
            }
          }
          if (((PXGraph) instance).IsDirty)
          {
            this.PersistOrder(instance);
            ((PXGraph) instance).Clear();
            ((PXGraph) instance).TimeStamp = ((PXGraph) this).TimeStamp;
          }
          if (!flag2)
            poOrder1 = (PX.Objects.PO.POOrder) ((PXSelectBase) instance.Document).Cache.CreateInstance();
          else
            continue;
label_62:
          Decimal num16 = 0M;
          Decimal? nullable1 = bidding.OrderQty;
          Decimal num17 = nullable1.Value;
          int? key2 = bidding != null ? bidding.LineID : line.LineNbr;
          nullable1 = requisitionLineCustomers.ReqQty;
          if (!nullable1.HasValue)
          {
            key1 = item.CustomerLocationID;
            if (key1.HasValue)
              num16 = num17;
          }
          nullable1 = requisitionLineCustomers.ReqQty;
          Decimal num18 = 0M;
          if (nullable1.GetValueOrDefault() > num18 & nullable1.HasValue)
          {
            nullable1 = requisitionLineCustomers.ReqQty;
            num16 = nullable1.Value;
          }
          if (num16 > 0M && dictionary1.ContainsKey(key2))
            num16 -= dictionary1[key2];
          if (num16 > num17)
            num16 = num17;
          if (num17 > 0M && poOrder1 != null)
          {
            poOrder1.OrderType = item.POType;
            poOrder1.OrderDesc = item.Description;
            PX.Objects.PO.POOrder copy4 = PXCache<PX.Objects.PO.POOrder>.CreateCopy(((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Insert(poOrder1));
            copy4.CuryID = (string) ((PXSelectBase<RQBidding>) this.Bidding).GetValueExt<RQBidding.curyID>(bidding);
            copy4.CuryInfoID = this.CopyCurrencyInfo((PXGraph) instance, bidding.CuryInfoID, true);
            PX.Objects.PO.POOrder copy5 = PXCache<PX.Objects.PO.POOrder>.CreateCopy(((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Update(copy4));
            copy5.VendorID = bidding.VendorID;
            copy5.VendorLocationID = bidding.VendorLocationID;
            PX.Objects.PO.POOrder copy6 = PXCache<PX.Objects.PO.POOrder>.CreateCopy(((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Update(copy5));
            if (rqBiddingVendor != null && rqBiddingVendor.PromisedDate.HasValue)
              copy6.ExpectedDate = rqBiddingVendor.PromisedDate;
            copy6.ShipDestType = item.ShipDestType;
            copy6.ShipToBAccountID = item.ShipToBAccountID;
            copy6.ShipToLocationID = item.ShipToLocationID;
            copy6.ShipContactID = item.ShipContactID;
            copy6.ShipAddressID = item.ShipAddressID;
            copy6.ShipVia = rqBiddingVendor.ShipVia;
            copy6.FOBPoint = rqBiddingVendor.FOBPoint;
            copy6.RemitAddressID = rqBiddingVendor.RemitAddressID;
            copy6.RemitContactID = rqBiddingVendor.RemitContactID;
            copy6.RQReqNbr = item.ReqNbr;
            PX.Objects.PO.POOrder poOrder2 = ((PXSelectBase<PX.Objects.PO.POOrder>) instance.Document).Update(copy6);
            poOrder2 = (PX.Objects.PO.POOrder) null;
          }
          object obj;
          ((PXSelectBase) this.Lines).Cache.RaiseFieldDefaulting<RQRequisitionLine.lineType>((object) line, ref obj);
          string lineType3 = (string) obj;
          if (num16 > 0M)
          {
            string lineType4 = lineType3;
            PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) instance, line.InventoryID);
            if (item.POType == "DP")
              lineType4 = inventoryItem == null || !inventoryItem.StkItem.GetValueOrDefault() ? (inventoryItem == null || !inventoryItem.NonStockReceipt.GetValueOrDefault() || inventoryItem == null || !inventoryItem.NonStockShip.GetValueOrDefault() ? line.LineType : "NP") : "GP";
            po4so.Add(line.LineNbr, this.InsertPOLine(instance, line, new Decimal?(num16), bidding.CuryQuoteUnitCost, (IBqlTable) bidding, lineType4, (DateTime?) rqBiddingVendor?.PromisedDate));
            if (!dictionary1.ContainsKey(key2))
              dictionary1[key2] = 0M;
            Dictionary<int?, Decimal> dictionary2 = dictionary1;
            key1 = key2;
            dictionary2[key1] += num16;
          }
          if (num17 > num16)
            this.InsertPOLine(instance, line, new Decimal?(num17 - num16), bidding.CuryQuoteUnitCost, (IBqlTable) bidding, lineType3, (DateTime?) rqBiddingVendor?.PromisedDate);
        }
label_87:
        if (((PXGraph) instance).IsDirty)
        {
          this.PersistOrder(instance);
          ((PXGraph) instance).Clear();
        }
        foreach (PX.Objects.PO.POOrder poOrder in array)
        {
          foreach (PXResult<PX.Objects.PO.POLine> pxResult in PXSelectBase<PX.Objects.PO.POLine, PXViewOf<PX.Objects.PO.POLine>.BasedOn<SelectFromBase<PX.Objects.PO.POLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POLine.orderType, Equal<BqlField<PX.Objects.PO.POOrder.orderType, IBqlString>.AsOptional>>>>>.And<BqlOperand<PX.Objects.PO.POLine.orderNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POOrder.orderNbr, IBqlString>.AsOptional>>>>.ReadOnly.Config>.SelectMultiBound((PXGraph) this, (object[]) new PX.Objects.PO.POOrder[1]
          {
            poOrder
          }, Array.Empty<object>()))
          {
            PX.Objects.PO.POLine line = PXResult<PX.Objects.PO.POLine>.op_Implicit(pxResult);
            po4so.Add(line.RQReqLineNbr, line);
          }
        }
        this.CreateSOOrderAndCloseAllQuotations(po4so, item, (IEnumerable<PX.Objects.PO.POOrder>) array);
        if (((PXGraph) this).IsDirty)
          ((PXAction) this.Save).Press();
        if (massProcess)
          PXProcessing<RQRequisition>.SetProcessed();
        transactionScope.Complete();
      }
    }
    catch (Exception ex)
    {
      PXTrace.WriteError((Exception) ErrorProcessingEntityException.Create(((PXGraph) this).Caches[typeof (RQRequisition)], (IBqlTable) item, ex));
      if (massProcess)
        PXProcessing<RQRequisition>.SetError(ex);
      else
        throw;
    }
    finally
    {
      ((PXGraph) this).Clear();
    }
  }

  private void CreateSOOrderAndCloseAllQuotations(
    RQRequisitionEntry.PO4SO po4so,
    RQRequisition requisition,
    IEnumerable<PX.Objects.PO.POOrder> existingPOOrders)
  {
    if (po4so.Count == 0)
      return;
    SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
    PX.Objects.SO.SOOrder[] array = GraphHelper.RowCast<PX.Objects.SO.SOOrder>((IEnumerable) ((PXSelectBase) this.SOOrders).View.SelectMultiBound((object[]) new RQRequisition[1]
    {
      requisition
    }, Array.Empty<object>()).AsEnumerable<object>()).Where<PX.Objects.SO.SOOrder>((Func<PX.Objects.SO.SOOrder, bool>) (order => order.Behavior != "QT")).ToArray<PX.Objects.SO.SOOrder>();
    ((PXGraph) instance).TimeStamp = ((PXGraph) this).TimeStamp;
    ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current = (PX.Objects.SO.SOOrder) null;
    instance.SOPOLinkShowDocumentsOnHold = true;
    foreach (RQSOSource rqSourceForSO in this.GetSOSource(requisition))
      this.CreateSOOrderAndSOLines(instance, requisition, rqSourceForSO, po4so, (IEnumerable<PX.Objects.SO.SOOrder>) array, existingPOOrders);
    if (((PXGraph) instance).IsDirty)
      ((PXAction) instance.Save).Press();
    foreach (PXResult<RQRequisitionOrder> pxResult in PXSelectBase<RQRequisitionOrder, PXSelect<RQRequisitionOrder, Where<RQRequisitionOrder.reqNbr, Equal<Required<RQRequisitionOrder.reqNbr>>, And<RQRequisitionOrder.orderCategory, Equal<RQOrderCategory.so>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) requisition.ReqNbr
    }))
    {
      RQRequisitionOrder requisitionOrder = PXResult<RQRequisitionOrder>.op_Implicit(pxResult);
      ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) requisitionOrder.OrderNbr, new object[1]
      {
        (object) requisitionOrder.OrderType
      }));
      if (((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current != null && ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current.OrderType == ((PXSelectBase<RQSetup>) this.Setup).Current.QTOrderType)
      {
        PX.Objects.SO.SOOrder copy = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current);
        copy.MarkCompleted();
        ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Update(copy);
        ((PXAction) instance.Save).Press();
      }
    }
  }

  private IEnumerable<RQSOSource> GetSOSource(RQRequisition requisition)
  {
    return requisition.CustomerLocationID.HasValue ? GraphHelper.RowCast<RQRequisitionLine>((IEnumerable) ((PXSelectBase<RQRequisitionLine>) this.Lines).Select(new object[1]
    {
      (object) requisition.ReqNbr
    })).Select<RQRequisitionLine, RQSOSource>((Func<RQRequisitionLine, RQSOSource>) (rqLine => this.CreateRQSOSourceFromRequisitionLine(requisition, rqLine))) : ((IEnumerable<PXResult<RQRequisitionContent>>) PXSelectBase<RQRequisitionContent, PXSelectJoin<RQRequisitionContent, InnerJoin<RQRequestLine, On<RQRequestLine.orderNbr, Equal<RQRequisitionContent.orderNbr>, And<RQRequestLine.lineNbr, Equal<RQRequisitionContent.lineNbr>>>, InnerJoin<RQRequest, On<RQRequest.orderNbr, Equal<RQRequisitionContent.orderNbr>>, InnerJoin<RQRequestClass, On<RQRequestClass.reqClassID, Equal<RQRequest.reqClassID>>, InnerJoin<RQRequisitionLine, On<RQRequisitionLine.reqNbr, Equal<RQRequisitionContent.reqNbr>, And<RQRequisitionLine.lineNbr, Equal<RQRequisitionContent.reqLineNbr>>>>>>>, Where<RQRequisitionContent.reqNbr, Equal<Required<RQRequisitionContent.reqNbr>>, And<RQRequestClass.customerRequest, Equal<boolTrue>>>, OrderBy<Asc<RQRequest.employeeID, Asc<RQRequisitionContent.reqLineNbr>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) requisition.ReqNbr
    })).AsEnumerable<PXResult<RQRequisitionContent>>().Select<PXResult<RQRequisitionContent>, PXResult<RQRequisitionContent, RQRequestLine, RQRequest, RQRequestClass, RQRequisitionLine>>((Func<PXResult<RQRequisitionContent>, PXResult<RQRequisitionContent, RQRequestLine, RQRequest, RQRequestClass, RQRequisitionLine>>) (res => (PXResult<RQRequisitionContent, RQRequestLine, RQRequest, RQRequestClass, RQRequisitionLine>) res)).Select<PXResult<RQRequisitionContent, RQRequestLine, RQRequest, RQRequestClass, RQRequisitionLine>, RQSOSource>((Func<PXResult<RQRequisitionContent, RQRequestLine, RQRequest, RQRequestClass, RQRequisitionLine>, RQSOSource>) (res => this.CreateRQSOSourceFromRequest(PXResult<RQRequisitionContent, RQRequestLine, RQRequest, RQRequestClass, RQRequisitionLine>.op_Implicit(res), PXResult<RQRequisitionContent, RQRequestLine, RQRequest, RQRequestClass, RQRequisitionLine>.op_Implicit(res), PXResult<RQRequisitionContent, RQRequestLine, RQRequest, RQRequestClass, RQRequisitionLine>.op_Implicit(res), PXResult<RQRequisitionContent, RQRequestLine, RQRequest, RQRequestClass, RQRequisitionLine>.op_Implicit(res))));
  }

  /// <summary>
  /// Creates <see cref="T:PX.Objects.RQ.RQSOSource" /> from requisition line on SO order creation. This method is an extension point for Lexware PriceUnit customization.
  /// </summary>
  /// <param name="requisition">The requisition.</param>
  /// <param name="rqLine">The requisition line.</param>
  /// <returns />
  protected virtual RQSOSource CreateRQSOSourceFromRequisitionLine(
    RQRequisition requisition,
    RQRequisitionLine rqLine)
  {
    return new RQSOSource()
    {
      LineNbr = new int?(rqLine.LineNbr.GetValueOrDefault()),
      CustomerID = requisition.CustomerID,
      CustomerLocationID = requisition.CustomerLocationID,
      InventoryID = rqLine.InventoryID,
      UOM = rqLine.UOM,
      SubItemID = rqLine.SubItemID,
      OrderQty = rqLine.OrderQty,
      IsUseMarkup = rqLine.IsUseMarkup,
      MarkupPct = rqLine.MarkupPct,
      EstUnitCost = rqLine.EstUnitCost,
      CuryEstUnitCost = rqLine.CuryEstUnitCost,
      Description = rqLine.Description
    };
  }

  /// <summary>
  /// Creates <see cref="T:PX.Objects.RQ.RQSOSource" /> from request line on SO order creation. This method is an extension point for Lexware PriceUnit customization.
  /// </summary>
  /// <param name="request">The request.</param>
  /// <param name="requestLine">The request line.</param>
  /// <param name="rqContent">The request content.</param>
  /// <param name="rqLine">The requisition line.</param>
  /// <returns />
  protected virtual RQSOSource CreateRQSOSourceFromRequest(
    RQRequest request,
    RQRequestLine requestLine,
    RQRequisitionContent rqContent,
    RQRequisitionLine rqLine)
  {
    return new RQSOSource()
    {
      UOM = requestLine.UOM,
      CustomerID = request.EmployeeID,
      CustomerLocationID = request.LocationID,
      LineNbr = new int?(rqContent.ReqLineNbr.GetValueOrDefault()),
      InventoryID = requestLine.InventoryID,
      SubItemID = requestLine.SubItemID,
      OrderQty = rqContent.ReqQty,
      IsUseMarkup = rqLine.IsUseMarkup,
      MarkupPct = rqLine.MarkupPct,
      EstUnitCost = requestLine.EstUnitCost,
      CuryEstUnitCost = requestLine.CuryEstUnitCost,
      Description = requestLine.Description
    };
  }

  /// <summary>Creates SO order and SO lines on PO order creation.</summary>
  /// <param name="sograph">The <see cref="T:PX.Objects.SO.SOOrderEntry" /> graph.</param>
  /// <param name="requisition">The requisition.</param>
  /// <param name="rqSourceForSO">The requisition DTO for SO documents.</param>
  /// <param name="po4so">The PO for SO DTO.</param>
  /// <param name="existingSOOrders">Sales orders that existed before the current POs and SOs creation</param>
  /// <param name="existingPOOrders">Purchase orders that existed before the current POs and SOs creation</param>
  private void CreateSOOrderAndSOLines(
    SOOrderEntry sograph,
    RQRequisition requisition,
    RQSOSource rqSourceForSO,
    RQRequisitionEntry.PO4SO po4so,
    IEnumerable<PX.Objects.SO.SOOrder> existingSOOrders,
    IEnumerable<PX.Objects.PO.POOrder> existingPOOrders)
  {
    PX.Objects.SO.SOOrder soOrder1 = existingSOOrders.FirstOrDefault<PX.Objects.SO.SOOrder>((Func<PX.Objects.SO.SOOrder, bool>) (order =>
    {
      int? customerId1 = order.CustomerID;
      int? customerId2 = rqSourceForSO.CustomerID;
      if (!(customerId1.GetValueOrDefault() == customerId2.GetValueOrDefault() & customerId1.HasValue == customerId2.HasValue))
        return false;
      int? customerLocationId1 = order.CustomerLocationID;
      int? customerLocationId2 = rqSourceForSO.CustomerLocationID;
      return customerLocationId1.GetValueOrDefault() == customerLocationId2.GetValueOrDefault() & customerLocationId1.HasValue == customerLocationId2.HasValue;
    }));
    List<PX.Objects.SO.SOLine> source = (List<PX.Objects.SO.SOLine>) null;
    int? nullable1;
    int? nullable2;
    if (soOrder1 != null)
    {
      if (((PXGraph) sograph).IsDirty)
        ((PXAction) sograph.Save).Press();
      ((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) soOrder1.OrderNbr, new object[1]
      {
        (object) soOrder1.OrderType
      }));
      source = GraphHelper.RowCast<PX.Objects.SO.SOLine>((IEnumerable) ((PXSelectBase<PX.Objects.SO.SOLine>) sograph.Transactions).Select(Array.Empty<object>())).ToList<PX.Objects.SO.SOLine>();
    }
    else
    {
      if (((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Current != null)
      {
        nullable1 = ((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Current.CustomerID;
        int? customerId = rqSourceForSO.CustomerID;
        if (nullable1.GetValueOrDefault() == customerId.GetValueOrDefault() & nullable1.HasValue == customerId.HasValue)
        {
          nullable2 = ((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Current.CustomerLocationID;
          nullable1 = rqSourceForSO.CustomerLocationID;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
            goto label_16;
        }
      }
      if (((PXGraph) sograph).IsDirty)
        ((PXAction) sograph.Save).Press();
      PXResultset<PX.Objects.SO.SOOrder> pxResultset = PXSelectBase<PX.Objects.SO.SOOrder, PXSelectJoin<PX.Objects.SO.SOOrder, InnerJoin<RQRequisitionOrder, On<RQRequisitionOrder.orderType, Equal<PX.Objects.SO.SOOrder.orderType>, And<RQRequisitionOrder.orderNbr, Equal<PX.Objects.SO.SOOrder.orderNbr>>>>, Where<RQRequisitionOrder.orderCategory, Equal<Required<RQRequisitionOrder.orderCategory>>, And<RQRequisitionOrder.reqNbr, Equal<Required<RQRequisitionOrder.reqNbr>>, And<PX.Objects.SO.SOOrder.status, Equal<SOOrderStatus.open>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 2, new object[2]
      {
        (object) "SO",
        (object) requisition.ReqNbr
      });
      PX.Objects.SO.SOOrder instance = (PX.Objects.SO.SOOrder) ((PXSelectBase) sograph.Document).Cache.CreateInstance();
      string orderType = ((PXSelectBase<RQSetup>) this.Setup).Current.SOOrderType;
      PX.Objects.SO.SOOrderType soOrderType = PX.Objects.SO.SOOrderType.PK.Find((PXGraph) this, orderType);
      if (!PXAccess.FeatureInstalled<FeaturesSet.inventory>() && soOrderType?.Behavior != "IN")
        orderType = "IN";
      if (soOrderType == null || !soOrderType.Active.GetValueOrDefault())
        throw new PXException("Unable to create a sales order. The {0} order type is inactive.", new object[1]
        {
          (object) orderType
        });
      instance.OrderType = orderType;
      PX.Objects.SO.SOOrder soOrder2 = ((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Insert(instance);
      PX.Objects.SO.SOOrder copy1 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) soOrder2.OrderNbr, Array.Empty<object>())));
      copy1.CustomerID = rqSourceForSO.CustomerID;
      copy1.CustomerLocationID = rqSourceForSO.CustomerLocationID;
      PX.Objects.SO.SOOrder copy2 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Update(copy1));
      copy2.CuryID = requisition.CuryID;
      copy2.CuryInfoID = this.CopyCurrencyInfo((PXGraph) sograph, requisition.CuryInfoID, false);
      if (pxResultset.Count == 1)
      {
        PX.Objects.SO.SOOrder soOrder3 = PXResult<PX.Objects.SO.SOOrder>.op_Implicit(pxResultset[0]);
        copy2.OrigOrderType = soOrder3.OrderType;
        copy2.OrigOrderNbr = soOrder3.OrderNbr;
      }
      ((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Update(copy2);
      ((PXAction) sograph.Save).Press();
      ((PXSelectBase<RQRequisitionOrder>) this.ReqOrders).Insert(new RQRequisitionOrder()
      {
        OrderCategory = "SO",
        OrderType = ((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Current.OrderType,
        OrderNbr = ((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Current.OrderNbr
      });
    }
label_16:
    if (!po4so.ContainsKey(rqSourceForSO.LineNbr))
      return;
    POLinkDialog extension = ((PXGraph) sograph).GetExtension<POLinkDialog>();
    Decimal num1 = Math.Max(0M, rqSourceForSO.OrderQty.Value - po4so[rqSourceForSO.LineNbr].Sum<PX.Objects.PO.POLine>((Func<PX.Objects.PO.POLine, Decimal>) (poLine => poLine.OrderQty.Value)));
    foreach (PX.Objects.PO.POLine poLine1 in (IEnumerable<PX.Objects.PO.POLine>) po4so[rqSourceForSO.LineNbr].Where<PX.Objects.PO.POLine>((Func<PX.Objects.PO.POLine, bool>) (po4soLine =>
    {
      Decimal? orderQty = po4soLine.OrderQty;
      Decimal num5 = 0M;
      return orderQty.GetValueOrDefault() > num5 & orderQty.HasValue;
    })).OrderByDescending<PX.Objects.PO.POLine, bool>((Func<PX.Objects.PO.POLine, bool>) (x => EnumerableExtensions.IsIn<string>(x.LineType, "GS", "GP", "NO", "NP"))).ThenByDescending<PX.Objects.PO.POLine, Decimal>((Func<PX.Objects.PO.POLine, Decimal>) (poLine => poLine.OrderQty.Value)))
    {
      PX.Objects.PO.POLine poLine = poLine1;
      if (!existingPOOrders.Any<PX.Objects.PO.POOrder>((Func<PX.Objects.PO.POOrder, bool>) (order => order.OrderType == poLine.OrderType && order.OrderType == poLine.OrderNbr)) || soOrder1 == null)
      {
        Decimal? orderQty1 = poLine.OrderQty;
        Decimal qty = orderQty1.Value;
        orderQty1 = rqSourceForSO.OrderQty;
        Decimal num2 = qty;
        if (orderQty1.GetValueOrDefault() < num2 & orderQty1.HasValue)
        {
          orderQty1 = rqSourceForSO.OrderQty;
          qty = orderQty1.Value;
        }
        if (!(qty <= 0M))
        {
          PX.Objects.SO.SOLine currentSOLine;
          if (soOrder1 == null)
          {
            currentSOLine = this.CreateSOLineFromPO4SOLine(sograph, poLine, qty + num1, requisition, rqSourceForSO);
            num1 = 0M;
          }
          else
          {
            currentSOLine = source.Where<PX.Objects.SO.SOLine>((Func<PX.Objects.SO.SOLine, bool>) (soLine =>
            {
              int? inventoryId1 = soLine.InventoryID;
              int? inventoryId2 = rqSourceForSO.InventoryID;
              if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
              {
                int? nullable3 = soLine.SiteID;
                int? nullable4 = poLine.SiteID;
                if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                {
                  nullable4 = soLine.VendorID;
                  nullable3 = poLine.VendorID;
                  if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue && soLine.UOM == poLine.UOM)
                  {
                    bool? nullable5 = soLine.POCreate;
                    if (nullable5.GetValueOrDefault())
                    {
                      nullable5 = soLine.POCreated;
                      bool flag = false;
                      return nullable5.GetValueOrDefault() == flag & nullable5.HasValue;
                    }
                  }
                }
              }
              return false;
            })).OrderBy<PX.Objects.SO.SOLine, Decimal>((Func<PX.Objects.SO.SOLine, Decimal>) (soLine => Math.Abs(soLine.Qty.GetValueOrDefault() - qty))).ThenByDescending<PX.Objects.SO.SOLine, Decimal>((Func<PX.Objects.SO.SOLine, Decimal>) (soLine => soLine.Qty.GetValueOrDefault())).FirstOrDefault<PX.Objects.SO.SOLine>();
            if (currentSOLine != null)
            {
              ((PXSelectBase<PX.Objects.SO.SOLine>) sograph.Transactions).Current = currentSOLine;
              source.Remove(currentSOLine);
            }
            else
              continue;
          }
          foreach (PXResult<SupplyPOLine> pxResult in ((PXSelectBase<SupplyPOLine>) extension.SupplyPOLines).Select(Array.Empty<object>()))
          {
            SupplyPOLine supplyPoLine = PXResult<SupplyPOLine>.op_Implicit(pxResult);
            if (supplyPoLine.OrderType == poLine.OrderType && supplyPoLine.OrderNbr == poLine.OrderNbr)
            {
              nullable1 = supplyPoLine.LineNbr;
              nullable2 = poLine.LineNbr;
              if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
              {
                supplyPoLine.Selected = new bool?(true);
                ((PXSelectBase<SupplyPOLine>) extension.SupplyPOLines).Update(supplyPoLine);
                break;
              }
            }
          }
          extension.LinkPOSupply(currentSOLine);
          if (((PXGraph) sograph).IsDirty)
            ((PXAction) sograph.Save).Press();
          PX.Objects.PO.POLine poLine2 = poLine;
          Decimal? orderQty2 = poLine2.OrderQty;
          Decimal num3 = qty;
          poLine2.OrderQty = orderQty2.HasValue ? new Decimal?(orderQty2.GetValueOrDefault() - num3) : new Decimal?();
          RQSOSource rqsoSource = rqSourceForSO;
          orderQty1 = rqsoSource.OrderQty;
          Decimal num4 = qty;
          rqsoSource.OrderQty = orderQty1.HasValue ? new Decimal?(orderQty1.GetValueOrDefault() - num4) : new Decimal?();
        }
      }
    }
  }

  /// <summary>
  /// Creates SO line from PO for SO (<see cref="T:PX.Objects.RQ.RQRequisitionEntry.PO4SO" />) line on Create Orders action. This is an extension point used by Lexware customization.
  /// </summary>
  /// <param name="soGraph">The SO graph.</param>
  /// <param name="poLine">The PO line.</param>
  /// <param name="qty">The quantity.</param>
  /// <param name="requisition">The requisition.</param>
  /// <param name="rqSourceForSO">The requisition DTO for SO documents.</param>
  /// <returns />
  protected virtual PX.Objects.SO.SOLine CreateSOLineFromPO4SOLine(
    SOOrderEntry soGraph,
    PX.Objects.PO.POLine poLine,
    Decimal qty,
    RQRequisition requisition,
    RQSOSource rqSourceForSO)
  {
    PX.Objects.SO.SOLine quote = PXResultset<PX.Objects.SO.SOLine>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLine, PXSelectJoin<PX.Objects.SO.SOLine, InnerJoin<RQRequisitionLine, On<RQRequisitionLine.qTOrderNbr, Equal<PX.Objects.SO.SOLine.orderNbr>, And<RQRequisitionLine.qTLineNbr, Equal<PX.Objects.SO.SOLine.lineNbr>>>, InnerJoin<PX.Objects.SO.SOOrder, On<PX.Objects.SO.SOOrder.orderType, Equal<PX.Objects.SO.SOLine.orderType>, And<PX.Objects.SO.SOOrder.orderNbr, Equal<PX.Objects.SO.SOLine.orderNbr>, And<PX.Objects.SO.SOOrder.status, Equal<SOOrderStatus.open>>>>>>, Where<PX.Objects.SO.SOLine.orderType, Equal<Required<PX.Objects.SO.SOLine.orderType>>, And<RQRequisitionLine.reqNbr, Equal<Required<RQRequisitionLine.reqNbr>>, And<RQRequisitionLine.lineNbr, Equal<Required<RQRequisitionLine.lineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) ((PXSelectBase<RQSetup>) this.Setup).Current.QTOrderType,
      (object) poLine.RQReqNbr,
      (object) poLine.RQReqLineNbr
    }));
    PX.Objects.SO.SOLine instance = (PX.Objects.SO.SOLine) ((PXSelectBase) soGraph.Transactions).Cache.CreateInstance();
    instance.OrderType = ((PXSelectBase<PX.Objects.SO.SOOrder>) soGraph.Document).Current.OrderType;
    instance.OrderNbr = ((PXSelectBase<PX.Objects.SO.SOOrder>) soGraph.Document).Current.OrderNbr;
    PX.Objects.SO.SOLine copy = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) soGraph.Transactions).Insert(instance));
    copy.InventoryID = rqSourceForSO.InventoryID;
    if (copy.InventoryID.HasValue)
      copy.SubItemID = rqSourceForSO.SubItemID;
    copy.UOM = rqSourceForSO.UOM;
    copy.Qty = new Decimal?(qty);
    copy.SiteID = poLine.SiteID;
    copy.TranDesc = rqSourceForSO.Description;
    if (quote != null)
      this.FillSOLineFromQuotation(copy, quote);
    else if (rqSourceForSO.IsUseMarkup.GetValueOrDefault())
    {
      Decimal profitMultiplier = 1M + rqSourceForSO.MarkupPct.GetValueOrDefault() / 100M;
      this.FillSOLine(soGraph, copy, rqSourceForSO, ((PXSelectBase<PX.Objects.SO.SOOrder>) soGraph.Document).Current.CuryID == requisition.CuryID, profitMultiplier);
    }
    copy.POCreate = new bool?(true);
    copy.POSource = requisition.POType == "DP" ? "D" : "O";
    copy.VendorID = poLine.VendorID;
    return PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) soGraph.Transactions).Update(copy));
  }

  /// <summary>
  /// Fill SO line from the quotation on POOrder creation. This is an extension point used by Lexware PriceUnit customization.
  /// </summary>
  /// <param name="newSoLine">The new SO line.</param>
  /// <param name="quote">The quotation.</param>
  protected virtual void FillSOLineFromQuotation(PX.Objects.SO.SOLine newSoLine, PX.Objects.SO.SOLine quote)
  {
    newSoLine.ManualPrice = new bool?(true);
    newSoLine.CuryUnitPrice = quote.CuryUnitPrice;
    newSoLine.OrigOrderType = quote.OrderType;
    newSoLine.OrigOrderNbr = quote.OrderNbr;
    newSoLine.OrigLineNbr = quote.LineNbr;
  }

  /// <summary>
  /// Fill SO line from the requisition source for SO on POOrder creation. This is an extension point used by Lexware PriceUnit customization.
  /// </summary>
  /// <param name="sograph">The SO graph.</param>
  /// <param name="newSoLine">The new SO line.</param>
  /// <param name="rqSourceForSO">The requisition source for SO.</param>
  /// <param name="areCurrenciesSame">True if are currencies same.</param>
  /// <param name="profitMultiplier">The profit multiplier.</param>
  protected virtual void FillSOLine(
    SOOrderEntry sograph,
    PX.Objects.SO.SOLine newSoLine,
    RQSOSource rqSourceForSO,
    bool areCurrenciesSame,
    Decimal profitMultiplier)
  {
    newSoLine.ManualPrice = new bool?(true);
    if (areCurrenciesSame)
    {
      PX.Objects.SO.SOLine soLine = newSoLine;
      Decimal? curyEstUnitCost = rqSourceForSO.CuryEstUnitCost;
      Decimal num = profitMultiplier;
      Decimal? nullable = curyEstUnitCost.HasValue ? new Decimal?(curyEstUnitCost.GetValueOrDefault() * num) : new Decimal?();
      soLine.CuryUnitPrice = nullable;
    }
    else
    {
      PX.Objects.SO.SOLine soLine = newSoLine;
      Decimal? estUnitCost = rqSourceForSO.EstUnitCost;
      Decimal num = profitMultiplier;
      Decimal? nullable = estUnitCost.HasValue ? new Decimal?(estUnitCost.GetValueOrDefault() * num) : new Decimal?();
      soLine.UnitPrice = nullable;
      PXCurrencyAttribute.CuryConvCury<PX.Objects.SO.SOLine.curyInfoID>(((PXSelectBase) sograph.Transactions).Cache, (object) newSoLine);
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable CreateQTOrder(PXAdapter adapter)
  {
    RQRequisitionEntry requisitionEntry = this;
    SOOrderEntry sograph = PXGraph.CreateInstance<SOOrderEntry>();
    List<PX.Objects.SO.SOOrder> newSOOrders = new List<PX.Objects.SO.SOOrder>();
    foreach (RQRequisition requisition in adapter.Get<RQRequisition>())
    {
      RQRequisition rqRequisition = requisition;
      RQRequisitionOrder requisitionOrder = PXResultset<RQRequisitionOrder>.op_Implicit(PXSelectBase<RQRequisitionOrder, PXSelectJoin<RQRequisitionOrder, InnerJoin<PX.Objects.SO.SOOrder, On<PX.Objects.SO.SOOrder.orderNbr, Equal<RQRequisitionOrder.orderNbr>, And<PX.Objects.SO.SOOrder.status, Equal<SOOrderStatus.open>>>>, Where<RQRequisitionOrder.reqNbr, Equal<Required<RQRequisitionOrder.reqNbr>>, And<RQRequisitionOrder.orderCategory, Equal<RQOrderCategory.so>>>>.Config>.Select((PXGraph) requisitionEntry, new object[1]
      {
        (object) requisition.ReqNbr
      }));
      if (requisition.CustomerID.HasValue && requisitionOrder == null)
      {
        ((PXSelectBase<RQRequisition>) requisitionEntry.Document).Current = requisition;
        bool flag = true;
        foreach (PXResult<RQRequisitionLine> pxResult in ((PXSelectBase<RQRequisitionLine>) requisitionEntry.Lines).Select(new object[1]
        {
          (object) requisition.ReqNbr
        }))
        {
          RQRequisitionLine row = PXResult<RQRequisitionLine>.op_Implicit(pxResult);
          if (!requisitionEntry.ValidateOpenState(row, (PXErrorLevel) 4))
            flag = false;
        }
        if (!flag)
          throw new PXRowPersistingException(typeof (RQRequisition).Name, (object) requisition, "Unable to create orders, not all required fields are defined.");
        ((PXGraph) sograph).TimeStamp = ((PXGraph) requisitionEntry).TimeStamp;
        ((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Current = (PX.Objects.SO.SOOrder) null;
        foreach (PXResult<RQRequisitionLine, PX.Objects.IN.InventoryItem> pxResult in PXSelectBase<RQRequisitionLine, PXSelectJoin<RQRequisitionLine, LeftJoin<PX.Objects.IN.InventoryItem, On<RQRequisitionLine.FK.InventoryItem>>, Where<RQRequisitionLine.reqNbr, Equal<Required<RQRequisition.reqNbr>>>>.Config>.Select((PXGraph) requisitionEntry, new object[1]
        {
          (object) requisition.ReqNbr
        }))
        {
          RQRequisitionLine rqLine = PXResult<RQRequisitionLine, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
          PX.Objects.IN.InventoryItem inventoryItem = PXResult<RQRequisitionLine, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
          requisitionEntry.CreateQTOrderAndLines(sograph, requisition, rqLine, inventoryItem, newSOOrders);
        }
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          try
          {
            if (((PXGraph) sograph).IsDirty)
            {
              bool externalTaxesSync = sograph.RecalculateExternalTaxesSync;
              try
              {
                sograph.RecalculateExternalTaxesSync = true;
                ((PXAction) sograph.Save).Press();
              }
              finally
              {
                sograph.RecalculateExternalTaxesSync = externalTaxesSync;
              }
            }
            RQRequisition copy = PXCache<RQRequisition>.CreateCopy(requisition);
            copy.Quoted = new bool?(true);
            rqRequisition = ((PXSelectBase<RQRequisition>) requisitionEntry.Document).Update(copy);
            ((PXAction) requisitionEntry.Save).Press();
          }
          catch
          {
            ((PXGraph) requisitionEntry).Clear();
            throw;
          }
          transactionScope.Complete();
        }
      }
      else
      {
        RQRequisition copy = PXCache<RQRequisition>.CreateCopy(requisition);
        copy.Quoted = new bool?(true);
        rqRequisition = ((PXSelectBase<RQRequisition>) requisitionEntry.Document).Update(copy);
        ((PXAction) requisitionEntry.Save).Press();
      }
      yield return (object) rqRequisition;
    }
    if (newSOOrders.Count == 1 && adapter.MassProcess)
    {
      ((PXGraph) sograph).Clear();
      ((PXGraph) sograph).SelectTimeStamp();
      ((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Current = newSOOrders[0];
      throw new PXRedirectRequiredException((PXGraph) sograph, "Sales Order");
    }
  }

  /// <summary>
  /// Creates quotation order and its lines. This is an extension point used by Lexware PriceUnit customization.
  /// </summary>
  /// <param name="sograph">The SO graph.</param>
  /// <param name="requisition">The requisition.</param>
  /// <param name="rqLine">The request line.</param>
  /// <param name="inventoryItem">The inventory item.</param>
  /// <param name="newSOOrders">The new SO orders.</param>
  protected virtual void CreateQTOrderAndLines(
    SOOrderEntry sograph,
    RQRequisition requisition,
    RQRequisitionLine rqLine,
    PX.Objects.IN.InventoryItem inventoryItem,
    List<PX.Objects.SO.SOOrder> newSOOrders)
  {
    RQBidding forQtOrderCreation = this.GetBiddingForQTOrderCreation(requisition, rqLine);
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Current == null)
    {
      PX.Objects.SO.SOOrder instance = (PX.Objects.SO.SOOrder) ((PXSelectBase) sograph.Document).Cache.CreateInstance();
      PX.Objects.SO.SOOrderType soOrderType = PX.Objects.SO.SOOrderType.PK.Find((PXGraph) this, ((PXSelectBase<RQSetup>) this.Setup).Current.QTOrderType);
      if (soOrderType == null || !soOrderType.Active.GetValueOrDefault())
        throw new PXException("Unable to create a quote. The {0} order type is inactive.", new object[1]
        {
          (object) ((PXSelectBase<RQSetup>) this.Setup).Current.QTOrderType
        });
      instance.OrderType = ((PXSelectBase<RQSetup>) this.Setup).Current.QTOrderType;
      PX.Objects.SO.SOOrder copy1 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Insert(instance));
      copy1.CustomerID = requisition.CustomerID;
      copy1.CustomerLocationID = requisition.CustomerLocationID;
      PX.Objects.SO.SOOrder copy2 = PXCache<PX.Objects.SO.SOOrder>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Update(copy1));
      copy2.CuryID = requisition.CuryID;
      copy2.CuryInfoID = this.CopyCurrencyInfo((PXGraph) sograph, requisition.CuryInfoID, false);
      ((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Update(copy2);
      SOOrderEntryExternalTax implementation = ((PXGraph) sograph).FindImplementation<SOOrderEntryExternalTax>();
      bool flag = false;
      try
      {
        if (implementation != null)
        {
          flag = implementation.skipExternalTaxCalcOnSave;
          implementation.skipExternalTaxCalcOnSave = true;
        }
        ((PXAction) sograph.Save).Press();
      }
      finally
      {
        if (implementation != null)
          implementation.skipExternalTaxCalcOnSave = flag;
      }
      PX.Objects.SO.SOOrder current = ((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Current;
      newSOOrders.Add(current);
      ((PXSelectBase<RQRequisitionOrder>) this.ReqOrders).Insert(new RQRequisitionOrder()
      {
        OrderCategory = "SO",
        OrderType = current.OrderType,
        OrderNbr = current.OrderNbr
      });
    }
    PX.Objects.SO.SOLine instance1 = (PX.Objects.SO.SOLine) ((PXSelectBase) sograph.Transactions).Cache.CreateInstance();
    instance1.OrderType = ((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Current.OrderType;
    instance1.OrderNbr = ((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Current.OrderNbr;
    PX.Objects.SO.SOLine copy3 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) sograph.Transactions).Insert(instance1));
    copy3.InventoryID = rqLine.InventoryID;
    int? nullable = copy3.InventoryID;
    if (nullable.HasValue)
      copy3.SubItemID = rqLine.SubItemID;
    copy3.UOM = rqLine.UOM;
    copy3.Qty = rqLine.OrderQty;
    nullable = rqLine.SiteID;
    if (nullable.HasValue)
      copy3.SiteID = rqLine.SiteID;
    if (rqLine.IsUseMarkup.GetValueOrDefault())
    {
      Decimal profitMultiplier = 1M + rqLine.MarkupPct.GetValueOrDefault() / 100M;
      this.FillSOLine(sograph, copy3, requisition, rqLine, forQtOrderCreation, profitMultiplier);
    }
    copy3.TranDesc = rqLine.Description;
    PX.Objects.SO.SOLine copy4 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) sograph.Transactions).Update(copy3));
    PXCache<RQRequisitionLine>.CreateCopy(rqLine);
    rqLine.QTOrderNbr = copy4.OrderNbr;
    rqLine.QTLineNbr = copy4.LineNbr;
    ((PXSelectBase<RQRequisitionLine>) this.Lines).Update(rqLine);
  }

  private RQBidding GetBiddingForQTOrderCreation(
    RQRequisition requisition,
    RQRequisitionLine rqLine)
  {
    return !requisition.VendorID.HasValue ? PXResultset<RQBidding>.op_Implicit(PXSelectBase<RQBidding, PXSelect<RQBidding, Where<RQBidding.reqNbr, Equal<Current<RQRequisitionLine.reqNbr>>, And<RQBidding.lineNbr, Equal<Current<RQRequisitionLine.lineNbr>>, And<RQBidding.orderQty, Greater<decimal0>>>>, OrderBy<Desc<RQBidding.quoteUnitCost>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) rqLine
    }, Array.Empty<object>())) : PXResultset<RQBidding>.op_Implicit(PXSelectBase<RQBidding, PXSelect<RQBidding, Where<RQBidding.reqNbr, Equal<Current<RQRequisitionLine.reqNbr>>, And<RQBidding.lineNbr, Equal<Current<RQRequisitionLine.lineNbr>>, And<RQBidding.vendorID, Equal<Current<RQRequisition.vendorID>>, And<RQBidding.vendorLocationID, Equal<Current<RQRequisition.vendorLocationID>>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[2]
    {
      (object) rqLine,
      (object) requisition
    }, Array.Empty<object>()));
  }

  /// <summary>Fill SO line during QT Order creation.</summary>
  /// <param name="sograph">The SO graph.</param>
  /// <param name="newSoLine">The new SO line.</param>
  /// <param name="requisition">The requisition.</param>
  /// <param name="rqLine">The requisition line.</param>
  /// <param name="bidding">The bidding.</param>
  /// <param name="profitMultiplier">The profit multiplier.</param>
  protected virtual void FillSOLine(
    SOOrderEntry sograph,
    PX.Objects.SO.SOLine newSoLine,
    RQRequisition requisition,
    RQRequisitionLine rqLine,
    RQBidding bidding,
    Decimal profitMultiplier)
  {
    newSoLine.ManualPrice = new bool?(true);
    string str = requisition.CuryID;
    Decimal valueOrDefault1 = rqLine.EstUnitCost.GetValueOrDefault();
    Decimal valueOrDefault2 = rqLine.CuryEstUnitCost.GetValueOrDefault();
    if (bidding != null)
    {
      Decimal? nullable = bidding.MinQty;
      Decimal? orderQty1 = newSoLine.OrderQty;
      if (nullable.GetValueOrDefault() <= orderQty1.GetValueOrDefault() & nullable.HasValue & orderQty1.HasValue)
      {
        Decimal? orderQty2 = bidding.OrderQty;
        nullable = newSoLine.OrderQty;
        if (orderQty2.GetValueOrDefault() >= nullable.GetValueOrDefault() & orderQty2.HasValue & nullable.HasValue)
        {
          str = (string) ((PXSelectBase<RQBidding>) this.Bidding).GetValueExt<RQBidding.curyID>(bidding);
          nullable = bidding.QuoteUnitCost;
          valueOrDefault1 = nullable.GetValueOrDefault();
          nullable = bidding.CuryQuoteUnitCost;
          valueOrDefault2 = nullable.GetValueOrDefault();
        }
      }
    }
    if (str == ((PXSelectBase<PX.Objects.SO.SOOrder>) sograph.Document).Current.CuryID)
    {
      newSoLine.CuryUnitPrice = new Decimal?(valueOrDefault2 * profitMultiplier);
    }
    else
    {
      newSoLine.UnitPrice = new Decimal?(valueOrDefault1 * profitMultiplier);
      PXCurrencyAttribute.CuryConvCury<PX.Objects.SO.SOLine.curyUnitPrice>(((PXSelectBase) sograph.Transactions).Cache, (object) newSoLine);
    }
  }

  [PXButton(ImageKey = "DataEntry")]
  [PXUIField(DisplayName = "Purchase Details")]
  public virtual IEnumerable viewLineDetails(PXAdapter adapter)
  {
    if (((PXSelectBase<RQRequisition>) this.Document).Current != null && ((PXSelectBase<RQRequisition>) this.Document).Current.Released.GetValueOrDefault() && ((PXSelectBase<RQRequisitionLine>) this.Lines).Current != null)
    {
      ((PXSelectBase<RQRequisitionStatic>) this.Filter).Current.ReqNbr = ((PXSelectBase<RQRequisitionLine>) this.Lines).Current.ReqNbr;
      ((PXSelectBase<RQRequisitionStatic>) this.Filter).Current.LineNbr = ((PXSelectBase<RQRequisitionLine>) this.Lines).Current.LineNbr;
      ((PXSelectBase<PX.Objects.PO.POLine>) this.OrderLines).AskExt();
      ((PXSelectBase<PX.Objects.PO.POLine>) this.OrderLines).ClearDialog();
    }
    return adapter.Get();
  }

  [PXButton(CommitChanges = true, VisibleOnDataSource = false)]
  [PXUIField]
  public virtual IEnumerable chooseVendor(PXAdapter adapter)
  {
    RQRequisitionEntry requisitionEntry = this;
    foreach (RQRequisition item in adapter.Get<RQRequisition>())
    {
      if (((PXSelectBase<RQBiddingVendor>) requisitionEntry.Vendors).Current != null && (item.Status == "B" || item.Status == "H"))
      {
        bool flag = false;
        PXView view = ((PXSelectBase) requisitionEntry.Lines).View;
        object[] objArray1 = new object[1]{ (object) item };
        object[] objArray2 = Array.Empty<object>();
        foreach (RQRequisitionLine rqRequisitionLine in view.SelectMultiBound(objArray1, objArray2))
        {
          RQRequisitionLine copy = (RQRequisitionLine) ((PXSelectBase) requisitionEntry.Lines).Cache.CreateCopy((object) rqRequisitionLine);
          RQBidding rqBidding = PXResultset<RQBidding>.op_Implicit(PXSelectBase<RQBidding, PXSelect<RQBidding, Where<RQBidding.reqNbr, Equal<Current<RQRequisitionLine.reqNbr>>, And<RQBidding.lineNbr, Equal<Current<RQRequisitionLine.lineNbr>>, And<RQBidding.vendorID, Equal<Current<RQBiddingVendor.vendorID>>, And<RQBidding.vendorLocationID, Equal<Current<RQBiddingVendor.vendorLocationID>>>>>>>.Config>.SelectSingleBound((PXGraph) requisitionEntry, new object[2]
          {
            (object) copy,
            (object) requisitionEntry.vendor
          }, Array.Empty<object>()));
          if (rqBidding != null)
          {
            Decimal? nullable1 = rqBidding.MinQty;
            Decimal num1 = 0M;
            if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
            {
              nullable1 = rqBidding.QuoteQty;
              Decimal num2 = 0M;
              if (nullable1.GetValueOrDefault() == num2 & nullable1.HasValue)
                continue;
            }
            nullable1 = rqBidding.MinQty;
            Decimal? nullable2 = rqRequisitionLine.OrderQty;
            if (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
            {
              ((PXSelectBase) requisitionEntry.Lines).Cache.RaiseExceptionHandling<RQRequisitionLine.orderQty>((object) rqRequisitionLine, (object) rqRequisitionLine.OrderQty, (Exception) new PXException("Order qty less than minimal qty specified by the vendor"));
              flag = true;
            }
            nullable2 = rqBidding.QuoteQty;
            nullable1 = rqRequisitionLine.OrderQty;
            if (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() & nullable2.HasValue & nullable1.HasValue)
            {
              ((PXSelectBase) requisitionEntry.Lines).Cache.RaiseExceptionHandling<RQRequisitionLine.orderQty>((object) rqRequisitionLine, (object) rqRequisitionLine.OrderQty, (Exception) new PXException("Order qty more than quote qty specified by the vendor"));
              flag = true;
            }
          }
        }
        if (flag)
          throw new PXException("Unable to process operation, some lines doesn't contain valid quotation information.");
        foreach (PXResult<RQBidding> pxResult in PXSelectBase<RQBidding, PXSelect<RQBidding, Where<RQBidding.reqNbr, Equal<Required<RQBidding.reqNbr>>, And<RQBidding.orderQty, Greater<Required<RQBidding.orderQty>>>>>.Config>.Select((PXGraph) requisitionEntry, new object[2]
        {
          (object) item.ReqNbr,
          (object) 0M
        }))
        {
          RQBidding rqBidding = PXResult<RQBidding>.op_Implicit(pxResult);
          RQBidding copy = (RQBidding) ((PXSelectBase) requisitionEntry.Bidding).Cache.CreateCopy((object) rqBidding);
          copy.OrderQty = new Decimal?(0M);
          ((PXSelectBase<RQBidding>) requisitionEntry.Bidding).Update(copy);
        }
        RQRequisition copy1 = (RQRequisition) ((PXSelectBase) requisitionEntry.Document).Cache.CreateCopy((object) item);
        ((PXSelectBase<RQRequisition>) requisitionEntry.Document).SetValueExt<RQRequisition.vendorID>(copy1, (object) ((PXSelectBase<RQBiddingVendor>) requisitionEntry.Vendors).Current.VendorID);
        int? vendorId = copy1.VendorID;
        int? nullable3 = ((PXSelectBase<RQBiddingVendor>) requisitionEntry.Vendors).Current.VendorID;
        if (vendorId.GetValueOrDefault() == nullable3.GetValueOrDefault() & vendorId.HasValue == nullable3.HasValue)
        {
          copy1.VendorLocationID = ((PXSelectBase<RQBiddingVendor>) requisitionEntry.Vendors).Current.VendorLocationID;
          RQRequisition rqRequisition1 = copy1;
          nullable3 = ((PXSelectBase<RQBiddingVendor>) requisitionEntry.Vendors).Current.RemitAddressID;
          long? nullable4 = nullable3.HasValue ? new long?((long) nullable3.GetValueOrDefault()) : new long?();
          long num3 = 0;
          int? nullable5 = nullable4.GetValueOrDefault() < num3 & nullable4.HasValue ? copy1.RemitAddressID : ((PXSelectBase<RQBiddingVendor>) requisitionEntry.Vendors).Current.RemitAddressID;
          rqRequisition1.RemitAddressID = nullable5;
          RQRequisition rqRequisition2 = copy1;
          nullable3 = ((PXSelectBase<RQBiddingVendor>) requisitionEntry.Vendors).Current.RemitContactID;
          long? nullable6 = nullable3.HasValue ? new long?((long) nullable3.GetValueOrDefault()) : new long?();
          long num4 = 0;
          int? nullable7 = nullable6.GetValueOrDefault() < num4 & nullable6.HasValue ? copy1.RemitContactID : ((PXSelectBase<RQBiddingVendor>) requisitionEntry.Vendors).Current.RemitContactID;
          rqRequisition2.RemitContactID = nullable7;
          RQRequisition rqRequisition3 = ((PXSelectBase<RQRequisition>) requisitionEntry.Document).Update(copy1);
          if (rqRequisition3.BiddingComplete.GetValueOrDefault())
            ((SelectedEntityEvent<RQRequisition>) PXEntityEventBase<RQRequisition>.Container<RQRequisition.Events>.Select((Expression<Func<RQRequisition.Events, PXEntityEvent<RQRequisition.Events>>>) (e => e.BiddingCompleted))).FireOn((PXGraph) requisitionEntry, rqRequisition3);
          yield return (object) PXResultset<RQRequisition>.op_Implicit(((PXSelectBase<RQRequisition>) requisitionEntry.Document).Search<RQRequisition.reqNbr>((object) rqRequisition3.ReqNbr, Array.Empty<object>()));
        }
      }
      yield return (object) item;
    }
  }

  [PXButton(CommitChanges = true, VisibleOnDataSource = false)]
  [PXUIField]
  public virtual IEnumerable responseVendor(PXAdapter adapter)
  {
    foreach (RQRequisition rqRequisition in adapter.Get<RQRequisition>())
    {
      if (((PXSelectBase<RQBiddingVendor>) this.Vendors).Current != null)
      {
        RQBiddingEntry instance = PXGraph.CreateInstance<RQBiddingEntry>();
        ((PXSelectBase<RQBiddingVendor>) instance.Vendor).Current = PXResultset<RQBiddingVendor>.op_Implicit(((PXSelectBase<RQBiddingVendor>) instance.Vendor).Search<RQBiddingVendor.reqNbr, RQBiddingVendor.lineID>((object) ((PXSelectBase<RQBiddingVendor>) this.Vendors).Current.ReqNbr, (object) ((PXSelectBase<RQBiddingVendor>) this.Vendors).Current.LineID, Array.Empty<object>()));
        if (((PXSelectBase<RQBiddingVendor>) instance.Vendor).Current != null)
          throw new PXRedirectRequiredException((PXGraph) instance, "Vendor Response");
      }
      yield return (object) rqRequisition;
    }
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable vendorInfo(PXAdapter adapter)
  {
    foreach (RQRequisition rqRequisition in adapter.Get<RQRequisition>())
    {
      if (((PXSelectBase<RQBiddingVendor>) this.Vendors).Current != null)
        ((PXSelectBase<RQBiddingVendor>) this.Vendors).AskExt();
      yield return (object) rqRequisition;
    }
  }

  protected virtual PX.Objects.PO.POLine InsertPOLine(
    POOrderEntry graph,
    RQRequisitionLine line,
    Decimal? qty,
    Decimal? unitCost,
    IBqlTable costOriginDac,
    string lineType,
    DateTime? bidPromisedDate)
  {
    Decimal? nullable = qty;
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() <= num & nullable.HasValue)
      return (PX.Objects.PO.POLine) null;
    PX.Objects.PO.POLine instance = (PX.Objects.PO.POLine) ((PXSelectBase) graph.Transactions).Cache.CreateInstance();
    instance.OrderType = ((PXSelectBase<PX.Objects.PO.POOrder>) graph.Document).Current.OrderType;
    instance.OrderNbr = ((PXSelectBase<PX.Objects.PO.POOrder>) graph.Document).Current.OrderNbr;
    PX.Objects.PO.POLine copy1 = PXCache<PX.Objects.PO.POLine>.CreateCopy(((PXSelectBase<PX.Objects.PO.POLine>) graph.Transactions).Insert(instance));
    copy1.LineType = lineType;
    copy1.InventoryID = line.InventoryID;
    if (copy1.InventoryID.HasValue)
      copy1.SubItemID = line.SubItemID;
    copy1.TranDesc = line.Description;
    copy1.UOM = line.UOM;
    copy1.AlternateID = line.AlternateID;
    if (unitCost.HasValue)
      copy1.ManualPrice = new bool?(true);
    PX.Objects.PO.POLine poLine1 = ((PXSelectBase<PX.Objects.PO.POLine>) graph.Transactions).Update(copy1);
    if (line.SiteID.HasValue)
      ((PXSelectBase) graph.Transactions).Cache.RaiseExceptionHandling<PX.Objects.PO.POLine.siteID>((object) poLine1, (object) null, (Exception) null);
    PX.Objects.PO.POLine copy2 = PXCache<PX.Objects.PO.POLine>.CreateCopy(poLine1);
    this.FillPOLineFromRequisitionLine(copy2, graph, line, qty, unitCost, costOriginDac, lineType);
    if (bidPromisedDate.HasValue)
      copy2.PromisedDate = bidPromisedDate;
    PX.Objects.PO.POLine poLine2 = ((PXSelectBase<PX.Objects.PO.POLine>) graph.Transactions).Update(copy2);
    PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.Lines).Cache, (object) line, ((PXSelectBase) graph.Transactions).Cache, (object) poLine2, (PXNoteAttribute.IPXCopySettings) null);
    poLine2.ProjectID = ProjectDefaultAttribute.NonProject();
    PXUIFieldAttribute.SetError<PX.Objects.PO.POLine.subItemID>(((PXSelectBase) graph.Transactions).Cache, (object) poLine2, (string) null);
    PXUIFieldAttribute.SetError<PX.Objects.PO.POLine.expenseSubID>(((PXSelectBase) graph.Transactions).Cache, (object) poLine2, (string) null);
    return poLine2;
  }

  /// <summary>
  /// Fill PO line from requisition line. This method is an extension point used by the PriceUnit Lexware customization.
  /// </summary>
  /// <param name="poline">The PO line to fill.</param>
  /// <param name="graph">The <see cref="T:PX.Objects.PO.POOrderEntry" /> graph.</param>
  /// <param name="rqLine">The request line.</param>
  /// <param name="qty">The quantity.</param>
  /// <param name="unitCost">The unit cost.</param>
  /// <param name="costOriginDac">The DAC from which the cost was taken.</param>
  /// <param name="lineType">Type of the line.</param>
  protected virtual void FillPOLineFromRequisitionLine(
    PX.Objects.PO.POLine poline,
    POOrderEntry graph,
    RQRequisitionLine rqLine,
    Decimal? qty,
    Decimal? unitCost,
    IBqlTable costOriginDac,
    string lineType)
  {
    if (rqLine.SiteID.HasValue)
    {
      ((PXSelectBase) graph.Transactions).Cache.RaiseExceptionHandling<PX.Objects.PO.POLine.siteID>((object) poline, (object) null, (Exception) null);
      poline.SiteID = rqLine.SiteID;
    }
    poline.OrderQty = qty;
    if (unitCost.HasValue)
      poline.CuryUnitCost = unitCost;
    poline.RcptQtyAction = rqLine.RcptQtyAction;
    poline.RcptQtyMin = rqLine.RcptQtyMin;
    poline.RcptQtyMax = rqLine.RcptQtyMax;
    poline.RcptQtyThreshold = rqLine.RcptQtyThreshold;
    poline.RQReqNbr = rqLine.ReqNbr;
    poline.RQReqLineNbr = new int?(rqLine.LineNbr.GetValueOrDefault());
    poline.ManualPrice = new bool?(true);
    if (lineType != "GI")
    {
      if (rqLine.ExpenseAcctID.HasValue)
        poline.ExpenseAcctID = rqLine.ExpenseAcctID;
      if (rqLine.ExpenseAcctID.HasValue && rqLine.ExpenseSubID.HasValue)
        poline.ExpenseSubID = rqLine.ExpenseSubID;
      poline.ProjectID = ProjectDefaultAttribute.NonProject();
    }
    if (rqLine.PromisedDate.HasValue)
      poline.PromisedDate = rqLine.PromisedDate;
    if (!rqLine.RequestedDate.HasValue)
      return;
    poline.RequestedDate = rqLine.RequestedDate;
  }

  private void PersistOrder(POOrderEntry graph)
  {
    PX.Objects.PO.POOrder copy = (PX.Objects.PO.POOrder) ((PXSelectBase) graph.Document).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.PO.POOrder>) graph.Document).Current);
    copy.CuryControlTotal = copy.CuryOrderTotal;
    ((PXSelectBase<PX.Objects.PO.POOrder>) graph.Document).Update(copy);
    if (copy.Hold.GetValueOrDefault() && !((PXSelectBase<RQSetup>) this.Setup).Current.POHold.GetValueOrDefault())
      ((PXAction) graph.releaseFromHold).Press();
    else
      ((PXAction) graph.Save).Press();
    ((PXSelectBase<RQRequisitionOrder>) this.ReqOrders).Insert(new RQRequisitionOrder()
    {
      OrderCategory = "PO",
      OrderType = ((PXSelectBase<PX.Objects.PO.POOrder>) graph.Document).Current.OrderType,
      OrderNbr = ((PXSelectBase<PX.Objects.PO.POOrder>) graph.Document).Current.OrderNbr
    });
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable CancelRequest(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable MarkQuoted(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold")]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Remove Hold")]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter)
  {
    foreach (RQRequisition rqRequisition in adapter.Get<RQRequisition>())
    {
      ((PXSelectBase<RQRequisition>) this.Document).Current = rqRequisition;
      if (!rqRequisition.Hold.GetValueOrDefault() && !rqRequisition.Approved.GetValueOrDefault() && ((PXSelectBase<RQSetup>) this.Setup).Current.RequisitionAssignmentMapID.HasValue)
      {
        PXGraph.CreateInstance<EPAssignmentProcessor<RQRequisition>>().Assign(rqRequisition, ((PXSelectBase<RQSetup>) this.Setup).Current.RequisitionAssignmentMapID);
        rqRequisition.WorkgroupID = rqRequisition.ApprovalWorkgroupID;
        rqRequisition.OwnerID = rqRequisition.ApprovalOwnerID;
      }
      yield return (object) PXResultset<RQRequisition>.op_Implicit(((PXSelectBase<RQRequisition>) this.Document).Search<RQRequisition.reqNbr>((object) rqRequisition.ReqNbr, Array.Empty<object>()));
    }
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Action(
    PXAdapter adapter,
    [PXInt, PXIntList(new int[] {1, 2}, new string[] {"Persist", "Update"})] int? actionID,
    [PXBool] bool refresh,
    [PXString] string actionName)
  {
    List<RQRequisition> rqRequisitionList = new List<RQRequisition>();
    if (actionName != null)
    {
      PXAction action = ((PXGraph) this).Actions[actionName];
      if (action != null)
        rqRequisitionList.AddRange(action.Press(adapter).Cast<RQRequisition>());
    }
    else
      rqRequisitionList.AddRange(adapter.Get<RQRequisition>());
    if (refresh)
    {
      foreach (RQRequisition rqRequisition in rqRequisitionList)
        ((PXSelectBase<RQRequisition>) this.Document).Search<RQRequisition.reqNbr>((object) rqRequisition.ReqNbr, Array.Empty<object>());
    }
    if (actionID.HasValue)
    {
      switch (actionID.GetValueOrDefault())
      {
        case 1:
          ((PXAction) this.Save).Press();
          break;
      }
    }
    return (IEnumerable) rqRequisitionList;
  }

  [PXUIField(DisplayName = "Vendor Notifications", Visible = false)]
  [PXButton(CommitChanges = true, MenuAutoOpen = true)]
  protected virtual IEnumerable VendorNotifications(
    PXAdapter adapter,
    [PXString] string notificationCD,
    [PXBool] bool currentVendor,
    [PXBool] bool updateVendorStatus)
  {
    RQRequisitionEntry requisitionEntry = this;
    foreach (RQRequisition rqRequisition in adapter.Get<RQRequisition>())
    {
      ((PXSelectBase) requisitionEntry.Document).Cache.Current = (object) rqRequisition;
      PXResultset<RQBiddingVendor> pxResultset1 = (PXResultset<RQBiddingVendor>) null;
      if (currentVendor)
      {
        pxResultset1 = new PXResultset<RQBiddingVendor>();
        pxResultset1.Add(new PXResult<RQBiddingVendor>(((PXSelectBase<RQBiddingVendor>) requisitionEntry.Vendors).Current));
      }
      bool flag1 = false;
      PXResultset<RQBiddingVendor> pxResultset2 = pxResultset1;
      if (pxResultset2 == null)
        pxResultset2 = ((PXSelectBase<RQBiddingVendor>) requisitionEntry.Vendors).Select(new object[1]
        {
          (object) rqRequisition.ReqNbr
        });
      foreach (PXResult<RQBiddingVendor> pxResult in pxResultset2)
      {
        RQBiddingVendor rqBiddingVendor = PXResult<RQBiddingVendor>.op_Implicit(pxResult);
        ((PXSelectBase<RQBiddingVendor>) requisitionEntry.Vendors).Current = rqBiddingVendor;
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        if (currentVendor || !rqBiddingVendor.Status.GetValueOrDefault())
        {
          bool flag2 = updateVendorStatus;
          try
          {
            string valueExt = (string) ((PXSelectBase<RQBiddingVendor>) requisitionEntry.Vendors).GetValueExt<RQBiddingVendor.vendorID>(rqBiddingVendor);
            parameters["ReqNbr"] = ((PXSelectBase<RQRequisition>) requisitionEntry.Document).Current.ReqNbr;
            parameters["VendorID"] = valueExt;
            ((PXGraph) requisitionEntry).GetExtension<RQRequisitionEntry.RQRequisitionEntry_ActivityDetailsExt>().SendNotification("Vendor", notificationCD, rqRequisition.BranchID, (IDictionary<string, string>) parameters, adapter.MassProcess, (IList<Guid?>) null);
          }
          catch (Exception ex)
          {
            if (currentVendor)
              throw;
            PXTrace.WriteError(ex);
            flag1 = true;
            flag2 = false;
            ((PXSelectBase) requisitionEntry.Vendors).Cache.RaiseExceptionHandling<RQBiddingVendor.status>((object) rqBiddingVendor, (object) false, ex);
          }
          if (flag2)
          {
            RQBiddingVendor copy = PXCache<RQBiddingVendor>.CreateCopy(rqBiddingVendor);
            copy.Status = new bool?(true);
            ((PXSelectBase<RQBiddingVendor>) requisitionEntry.Vendors).Update(copy);
            ((PXAction) requisitionEntry.Save).Press();
          }
        }
      }
      if (flag1)
        throw new PXException("At least one item has not been processed.");
      yield return (object) rqRequisition;
    }
  }

  [PXUIField]
  [PXButton(CommitChanges = true, VisibleOnDataSource = false)]
  protected virtual IEnumerable SendRequestToCurrentVendor(PXAdapter adapter)
  {
    return this.VendorNotifications(adapter, "RQPROPOSAL", true, true);
  }

  [PXUIField]
  [PXButton(CommitChanges = true)]
  protected virtual IEnumerable SendRequestToAllVendors(PXAdapter adapter)
  {
    return this.VendorNotifications(adapter, "RQPROPOSAL", false, true);
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Report(PXAdapter adapter, [PXString(8, InputMask = "CC.CC.CC.CC")] string reportID)
  {
    List<RQRequisition> list = adapter.Get<RQRequisition>().ToList<RQRequisition>();
    if (string.IsNullOrEmpty(reportID))
      return (IEnumerable) list;
    ((PXAction) this.Save).Press();
    throw new PXReportRequiredException(new Dictionary<string, string>()
    {
      ["ReqNbr"] = ((PXSelectBase<RQRequisition>) this.Document).Current.ReqNbr
    }, reportID, (PXBaseRedirectException.WindowMode) 2, "Report " + reportID, (CurrentLocalization) null);
  }

  [PXUIField]
  [PXButton(CommitChanges = true)]
  protected virtual IEnumerable RequestForProposal(PXAdapter adapter)
  {
    return this.Report(adapter, "RQ611000");
  }

  protected virtual void CurrencyInfo_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    PX.Objects.CM.CurrencyInfo row = (PX.Objects.CM.CurrencyInfo) e.Row;
    RQRequisition current = ((PXSelectBase<RQRequisition>) this.Document).Current;
    if (row != null && current != null)
    {
      long? curyInfoId1 = row.CuryInfoID;
      long? curyInfoId2 = current.CuryInfoID;
      if (curyInfoId1.GetValueOrDefault() == curyInfoId2.GetValueOrDefault() & curyInfoId1.HasValue == curyInfoId2.HasValue)
      {
        if (this._useCustomerCurrency && ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current != null && !string.IsNullOrEmpty(((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.CuryID))
        {
          e.NewValue = (object) ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.CuryID;
          ((CancelEventArgs) e).Cancel = true;
          return;
        }
        if (((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current == null || string.IsNullOrEmpty(((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current.CuryID))
          return;
        e.NewValue = (object) ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current.CuryID;
        ((CancelEventArgs) e).Cancel = true;
        return;
      }
    }
    if (((PXSelectBase<PX.Objects.AP.Vendor>) this.vendorBidder).Current == null || string.IsNullOrEmpty(((PXSelectBase<PX.Objects.AP.Vendor>) this.vendorBidder).Current.CuryID))
      return;
    e.NewValue = (object) ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendorBidder).Current.CuryID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_CuryRateTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    PX.Objects.CM.CurrencyInfo row = (PX.Objects.CM.CurrencyInfo) e.Row;
    RQRequisition current = ((PXSelectBase<RQRequisition>) this.Document).Current;
    if (row != null && current != null)
    {
      long? curyInfoId1 = row.CuryInfoID;
      long? curyInfoId2 = current.CuryInfoID;
      if (curyInfoId1.GetValueOrDefault() == curyInfoId2.GetValueOrDefault() & curyInfoId1.HasValue == curyInfoId2.HasValue)
      {
        if (this._useCustomerCurrency && ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current != null && !string.IsNullOrEmpty(((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.CuryID))
        {
          e.NewValue = (object) (((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.CuryRateTypeID ?? ((PXSelectBase<CMSetup>) this.cmsetup).Current.ARRateTypeDflt);
          ((CancelEventArgs) e).Cancel = true;
          return;
        }
        if (((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current == null || string.IsNullOrEmpty(((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current.CuryID))
          return;
        e.NewValue = (object) (((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current.CuryRateTypeID ?? ((PXSelectBase<CMSetup>) this.cmsetup).Current.APRateTypeDflt);
        ((CancelEventArgs) e).Cancel = true;
        return;
      }
    }
    if (((PXSelectBase<PX.Objects.AP.Vendor>) this.vendorBidder).Current != null)
    {
      e.NewValue = (object) (((PXSelectBase<PX.Objects.AP.Vendor>) this.vendorBidder).Current.CuryRateTypeID ?? ((PXSelectBase<CMSetup>) this.cmsetup).Current.APRateTypeDflt);
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      e.NewValue = (object) ((PXSelectBase<CMSetup>) this.cmsetup).Current.APRateTypeDflt;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void CurrencyInfo_CuryEffDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase) this.Document).Cache.Current == null)
      return;
    e.NewValue = (object) ((RQRequisition) ((PXSelectBase) this.Document).Cache.Current).OrderDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CM.CurrencyInfo row))
      return;
    bool? nullable = row.IsReadOnly;
    bool flag1 = !nullable.GetValueOrDefault() && ((PXSelectBase) this.Lines).Cache.AllowInsert && ((PXSelectBase) this.Lines).Cache.AllowDelete;
    bool flag2 = row.AllowUpdate(((PXSelectBase) this.Lines).Cache);
    RQRequisition current = ((PXSelectBase<RQRequisition>) this.Document).Current;
    if (current != null)
    {
      long? curyInfoId1 = row.CuryInfoID;
      long? curyInfoId2 = current.CuryInfoID;
      if (curyInfoId1.GetValueOrDefault() == curyInfoId2.GetValueOrDefault() & curyInfoId1.HasValue == curyInfoId2.HasValue)
      {
        if (((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current != null)
        {
          nullable = ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.AllowOverrideCury;
          if (!nullable.GetValueOrDefault())
            flag1 = false;
          nullable = ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.AllowOverrideRate;
          if (!nullable.GetValueOrDefault())
          {
            flag2 = false;
            goto label_21;
          }
          goto label_21;
        }
        if (((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current != null)
        {
          nullable = ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current.AllowOverrideCury;
          if (!nullable.GetValueOrDefault())
            flag1 = false;
          nullable = ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current.AllowOverrideRate;
          if (!nullable.GetValueOrDefault())
          {
            flag2 = false;
            goto label_21;
          }
          goto label_21;
        }
        goto label_21;
      }
    }
    if (current != null)
    {
      PXResult<RQBiddingVendor, PX.Objects.AP.Vendor> pxResult = (PXResult<RQBiddingVendor, PX.Objects.AP.Vendor>) PXResultset<RQBiddingVendor>.op_Implicit(PXSelectBase<RQBiddingVendor, PXSelectJoin<RQBiddingVendor, InnerJoin<PX.Objects.AP.Vendor, On<RQBiddingVendor.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>, Where<RQBiddingVendor.curyInfoID, Equal<Required<RQBiddingVendor.curyInfoID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) row.CuryInfoID
      }));
      PX.Objects.AP.Vendor vendor = pxResult != null ? PXResult<RQBiddingVendor, PX.Objects.AP.Vendor>.op_Implicit(pxResult) : (PX.Objects.AP.Vendor) null;
      int num1;
      if (vendor == null)
      {
        num1 = 0;
      }
      else
      {
        nullable = vendor.AllowOverrideCury;
        num1 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      flag1 = num1 != 0;
      int num2;
      if (vendor == null)
      {
        num2 = 0;
      }
      else
      {
        nullable = vendor.AllowOverrideRate;
        num2 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      flag2 = num2 != 0;
    }
label_21:
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyID>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyRateTypeID>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyEffDate>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.sampleCuryRate>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.sampleRecipRate>(sender, (object) row, flag2);
  }

  protected virtual void RQRequisition_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    RQRequisition row = (RQRequisition) e.Row;
    if (row == null)
      return;
    bool? nullable = row.Hold;
    bool valueOrDefault = nullable.GetValueOrDefault();
    ((PXAction) this.Transfer).SetEnabled(valueOrDefault);
    ((PXAction) this.Merge).SetEnabled(valueOrDefault);
    ((PXAction) this.ViewLineDetails).SetEnabled(row.Status == "E");
    ((PXAction) this.AddRequestLine).SetEnabled(valueOrDefault);
    ((PXAction) this.AddRequestContent).SetEnabled(valueOrDefault);
    CMSetup current = ((PXSelectBase<CMSetup>) this.cmsetup).Current;
    PXUIFieldAttribute.SetVisible<RQRequisition.curyID>(sender, (object) row, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
    bool flag1 = ((PXSelectBase<RQRequisitionLine>) this.Lines).Select(Array.Empty<object>()).Count == 0;
    bool flag2 = false;
    if (flag1)
    {
      if (((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current != null)
      {
        nullable = ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.AllowOverrideCury;
        flag2 = nullable.GetValueOrDefault();
      }
      else if (((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current != null)
      {
        nullable = ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current.AllowOverrideCury;
        flag2 = nullable.GetValueOrDefault();
      }
      else
        flag2 = true;
    }
    PXUIFieldAttribute.SetEnabled<RQRequisition.customerID>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<RQRequisition.customerLocationID>(sender, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<RQRequisition.curyID>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetVisible<RQRequisition.quoted>(sender, (object) row, row.CustomerLocationID.HasValue);
    PXAction<RQRequisition> validateAddresses = this.validateAddresses;
    nullable = row.Released;
    bool flag3 = false;
    int num;
    if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
    {
      nullable = row.Cancelled;
      bool flag4 = false;
      if (nullable.GetValueOrDefault() == flag4 & nullable.HasValue)
      {
        num = ((PXGraph) this).FindAllImplementations<IAddressValidationHelper>().RequiresValidation() ? 1 : 0;
        goto label_12;
      }
    }
    num = 0;
label_12:
    ((PXAction) validateAddresses).SetEnabled(num != 0);
    ((PXSelectBase) this.Vendors).Cache.AllowUpdate = ((PXSelectBase) this.Vendors).Cache.AllowInsert = ((PXSelectBase) this.Vendors).Cache.AllowDelete = row.Status != "E";
    PXUIFieldAttribute.SetEnabled<RQRequisition.shipToBAccountID>(sender, (object) row, row.ShipDestType != "L" && row.ShipDestType != "S");
    PXUIFieldAttribute.SetEnabled<RQRequisition.shipToLocationID>(sender, (object) row, row.ShipDestType != "S");
    PXUIFieldAttribute.SetEnabled<RQRequisition.siteID>(sender, (object) row, row.ShipDestType == "S");
    PXUIFieldAttribute.SetRequired<RQRequisition.siteID>(sender, true);
    PXUIFieldAttribute.SetRequired<RQRequisition.shipToBAccountID>(sender, row.ShipDestType != "S");
    PXUIFieldAttribute.SetRequired<RQRequisition.shipToLocationID>(sender, row.ShipDestType != "S");
    PXUIFieldAttribute.SetVisible<RQRequisition.shipToBAccountID>(sender, (object) row, row.ShipDestType != "S");
    PXUIFieldAttribute.SetVisible<RQRequisition.shipToLocationID>(sender, (object) row, row.ShipDestType != "S");
    PXUIFieldAttribute.SetVisible<RQRequisition.siteID>(sender, (object) row, row.ShipDestType == "S");
    ((PXSelectBase) this.Lines).Cache.AllowInsert = row.EmployeeID.HasValue;
    if (row == null || !(row.ShipDestType == "S") || PXUIFieldAttribute.GetError<RQRequisition.siteID>(sender, e.Row) != null)
      return;
    string siteIdErrorMessage = row.SiteIdErrorMessage;
    if (string.IsNullOrWhiteSpace(siteIdErrorMessage))
      return;
    sender.RaiseExceptionHandling<RQRequisition.siteID>(e.Row, sender.GetValueExt<RQRequisition.siteID>(e.Row), (Exception) new PXSetPropertyException(siteIdErrorMessage, (PXErrorLevel) 4));
  }

  protected virtual void RQRequisition_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row == null)
      return;
    using (new ReadOnlyScope(new PXCache[2]
    {
      ((PXSelectBase) this.Shipping_Address).Cache,
      ((PXSelectBase) this.Shipping_Contact).Cache
    }))
    {
      SharedRecordAttribute.DefaultRecord<RQRequisition.shipAddressID>(sender, e.Row);
      SharedRecordAttribute.DefaultRecord<RQRequisition.shipContactID>(sender, e.Row);
    }
  }

  protected virtual void RQRequisition_ShipDestType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    RQRequisition row = (RQRequisition) e.Row;
    if (row == null)
      return;
    if (row.ShipDestType == "S")
    {
      sender.SetDefaultExt<RQRequisition.siteID>(e.Row);
      sender.SetValueExt<RQRequisition.shipToBAccountID>(e.Row, (object) null);
      sender.SetValueExt<RQRequisition.shipToLocationID>(e.Row, (object) null);
    }
    else
    {
      sender.SetValueExt<RQRequisition.siteID>(e.Row, (object) null);
      sender.SetDefaultExt<RQRequisition.shipToBAccountID>(e.Row);
      sender.SetDefaultExt<RQRequisition.shipToLocationID>(e.Row);
    }
  }

  protected virtual void RQRequisition_SiteID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    RQRequisition row = (RQRequisition) e.Row;
    if (row == null || !(row.ShipDestType == "S"))
      return;
    string str = string.Empty;
    try
    {
      SharedRecordAttribute.DefaultRecord<RQRequisition.shipAddressID>(sender, e.Row);
    }
    catch (SharedRecordMissingException ex)
    {
      sender.RaiseExceptionHandling<RQRequisition.siteID>(e.Row, sender.GetValueExt<RQRequisition.siteID>(e.Row), (Exception) new PXSetPropertyException("The document cannot be saved, shipping address is not specified  for the warehouse selected  on the Shipping Instructions tab.", (PXErrorLevel) 4));
      sender.SetValueExt<RQRequisition.shipAddressID>(e.Row, (object) null);
      str = "The document cannot be saved, shipping address is not specified  for the warehouse selected  on the Shipping Instructions tab.";
    }
    try
    {
      SharedRecordAttribute.DefaultRecord<RQRequisition.shipContactID>(sender, e.Row);
    }
    catch (SharedRecordMissingException ex)
    {
      sender.RaiseExceptionHandling<RQRequisition.siteID>(e.Row, sender.GetValueExt<RQRequisition.siteID>(e.Row), (Exception) new PXSetPropertyException("The document cannot be saved, shipping contact is not specified  for the warehouse selected  on the Shipping Instructions tab.", (PXErrorLevel) 4));
      sender.SetValueExt<RQRequisition.shipContactID>(e.Row, (object) null);
      str = "The document cannot be saved, shipping contact is not specified  for the warehouse selected  on the Shipping Instructions tab.";
    }
    sender.SetValueExt<RQRequisition.siteIdErrorMessage>(e.Row, (object) str);
    if (!string.IsNullOrWhiteSpace(str))
      return;
    PXUIFieldAttribute.SetError<RQRequisition.siteID>(sender, e.Row, (string) null);
  }

  protected virtual void RQRequisition_ShipToBAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if ((RQRequisition) e.Row == null)
      return;
    sender.SetDefaultExt<RQRequisition.shipToLocationID>(e.Row);
  }

  protected virtual void RQRequisition_ShipToLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if ((RQRequisition) e.Row == null)
      return;
    try
    {
      SharedRecordAttribute.DefaultRecord<RQRequisition.shipAddressID>(sender, e.Row);
    }
    catch (SharedRecordMissingException ex)
    {
      sender.RaiseExceptionHandling<RQRequisition.siteID>(e.Row, sender.GetValueExt<RQRequisition.siteID>(e.Row), (Exception) new PXSetPropertyException("The document cannot be saved, shipping address is not specified  for the warehouse selected  on the Shipping Instructions tab.", (PXErrorLevel) 4));
    }
    try
    {
      SharedRecordAttribute.DefaultRecord<RQRequisition.shipContactID>(sender, e.Row);
    }
    catch (SharedRecordMissingException ex)
    {
      sender.RaiseExceptionHandling<RQRequisition.siteID>(e.Row, sender.GetValueExt<RQRequisition.siteID>(e.Row), (Exception) new PXSetPropertyException("The document cannot be saved, shipping contact is not specified  for the warehouse selected  on the Shipping Instructions tab.", (PXErrorLevel) 4));
    }
  }

  protected virtual void RQRequisition_ShipToLocationID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    RQRequisition row = (RQRequisition) e.Row;
    if (row == null || !(row.ShipDestType == "S"))
      return;
    ((CancelEventArgs) e).Cancel = true;
    e.NewValue = (object) null;
  }

  protected virtual void RQRequisition_ShipToBAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    RQRequisition row = (RQRequisition) e.Row;
    if (row == null || !(row.ShipDestType == "S"))
      return;
    ((CancelEventArgs) e).Cancel = true;
    e.NewValue = (object) null;
  }

  protected virtual void RQRequisition_ShipToLocationID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    RQRequisition row = (RQRequisition) e.Row;
    if (row == null || !(row.ShipDestType == "L"))
      return;
    int? nullable = row.VendorID;
    if (!nullable.HasValue)
      return;
    nullable = row.VendorLocationID;
    if (!nullable.HasValue)
      return;
    PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectReadonly<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<RQRequisition.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<RQRequisition.vendorLocationID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.VendorID,
      (object) row.VendorLocationID
    }));
    if (location == null)
      return;
    nullable = location.VBranchID;
    if (!nullable.HasValue)
      return;
    e.NewValue = (object) location.VBranchID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void RQRequisition_BiddingComplete_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    RQRequisition row = (RQRequisition) e.Row;
    e.NewValue = (object) row.VendorLocationID.HasValue;
  }

  protected virtual void RQRequisition_Quoted_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    RQRequisition row = (RQRequisition) e.Row;
    e.NewValue = (object) !row.CustomerLocationID.HasValue;
  }

  protected virtual void RQRequisition_CustomerID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current = (PX.Objects.AR.Customer) null;
    sender.SetDefaultExt<RQRequisition.customerLocationID>(e.Row);
    sender.SetDefaultExt<RQRequisition.curyID>(e.Row);
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>() || !e.ExternalCall && sender.GetValuePending<RQRequisition.curyID>(e.Row) != null || sender.GetValuePending<RQRequisition.vendorID>(e.Row) != PXCache.NotSetValue)
      return;
    PX.Objects.CM.CurrencyInfo currencyInfo = CurrencyInfoAttribute.SetDefaults<RQRequisition.curyInfoID>(sender, e.Row);
    string error = PXUIFieldAttribute.GetError<PX.Objects.CM.CurrencyInfo.curyEffDate>(((PXSelectBase) this.currencyinfo).Cache, (object) currencyInfo);
    if (!string.IsNullOrEmpty(error))
      sender.RaiseExceptionHandling<RQRequisition.orderDate>(e.Row, (object) ((RQRequisition) e.Row).OrderDate, (Exception) new PXSetPropertyException(error, (PXErrorLevel) 2));
    if (currencyInfo == null)
      return;
    ((RQRequisition) e.Row).CuryID = currencyInfo.CuryID;
  }

  protected virtual void RQRequisition_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    RQRequisition row = (RQRequisition) e.Row;
    RQRequisition newRow = (RQRequisition) e.NewRow;
    if (row == null)
      return;
    int? customerId1 = row.CustomerID;
    int? customerId2 = newRow.CustomerID;
    if (customerId1.GetValueOrDefault() == customerId2.GetValueOrDefault() & customerId1.HasValue == customerId2.HasValue)
    {
      int? customerLocationId1 = row.CustomerLocationID;
      int? customerLocationId2 = newRow.CustomerLocationID;
      if (customerLocationId1.GetValueOrDefault() == customerLocationId2.GetValueOrDefault() & customerLocationId1.HasValue == customerLocationId2.HasValue)
        return;
    }
    if (PXResultset<RQRequisitionContent>.op_Implicit(PXSelectBase<RQRequisitionContent, PXSelectJoin<RQRequisitionContent, InnerJoin<RQRequest, On<RQRequest.orderNbr, Equal<RQRequisitionContent.orderNbr>>>, Where<RQRequisitionContent.reqNbr, Equal<Required<RQRequisitionContent.reqNbr>>, And<Where<RQRequest.employeeID, NotEqual<Required<RQRequest.employeeID>>, Or<RQRequest.locationID, NotEqual<Required<RQRequest.locationID>>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
    {
      (object) row.ReqNbr,
      (object) newRow.CustomerID,
      (object) newRow.CustomerLocationID
    })) == null || ((PXSelectBase<RQRequisitionContent>) this.Contents).Ask("Confirmation", "There are requests from another customer or from emplyees in requisition. Continue to update customer for full requisition?", (MessageButtons) 4) != 7)
      return;
    newRow.CustomerID = row.CustomerID;
    newRow.CustomerLocationID = row.CustomerLocationID;
  }

  protected virtual void RQRequisition_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    RQRequisition oldRow = (RQRequisition) e.OldRow;
    RQRequisition row1 = (RQRequisition) e.Row;
    if (!row1.CustomerID.HasValue && row1.POType == "DP")
      row1.POType = "RO";
    if (oldRow.POType != row1.POType && row1.POType == "DP" && row1.ShipDestType != "C")
    {
      RQRequisition copy = PXCache<RQRequisition>.CreateCopy(row1);
      copy.ShipDestType = "C";
      copy.ShipToBAccountID = oldRow.CustomerID;
      copy.ShipToLocationID = oldRow.CustomerLocationID;
      ((PXSelectBase<RQRequisition>) this.Document).Update(copy);
    }
    else if (oldRow.POType != row1.POType && row1.POType != "DP" && row1.ShipDestType == "C")
    {
      RQRequisition copy = PXCache<RQRequisition>.CreateCopy(row1);
      copy.ShipDestType = "L";
      sender.SetDefaultExt<RQRequisition.shipDestType>((object) copy);
      ((PXSelectBase<RQRequisition>) this.Document).Update(copy);
    }
    int? vendorId1 = oldRow.VendorID;
    int? nullable1 = row1.VendorID;
    if (!(vendorId1.GetValueOrDefault() == nullable1.GetValueOrDefault() & vendorId1.HasValue == nullable1.HasValue) && PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
    {
      PX.Objects.AP.Vendor current = ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current;
      if (current != null)
      {
        nullable1 = current.BAccountID;
        int? vendorId2 = ((RQRequisition) e.Row).VendorID;
        if (nullable1.GetValueOrDefault() == vendorId2.GetValueOrDefault() & nullable1.HasValue == vendorId2.HasValue)
          goto label_10;
      }
      this.vendor.RaiseFieldUpdated(sender, e.Row);
      current = ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current;
label_10:
      if (!this._useCustomerCurrency && current != null)
      {
        PX.Objects.CM.CurrencyInfo currencyInfo1 = PXCache<PX.Objects.CM.CurrencyInfo>.CreateCopy(PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Select(new object[1]
        {
          (object) oldRow.CuryInfoID
        })));
        RQBiddingVendor rqBiddingVendor = PXResultset<RQBiddingVendor>.op_Implicit(PXSelectBase<RQBiddingVendor, PXSelect<RQBiddingVendor, Where<RQBiddingVendor.reqNbr, Equal<Required<RQBiddingVendor.reqNbr>>, And<RQBiddingVendor.vendorID, Equal<Required<RQBiddingVendor.vendorID>>, And<RQBiddingVendor.vendorLocationID, Equal<Required<RQBiddingVendor.vendorLocationID>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
        {
          (object) row1.ReqNbr,
          (object) row1.VendorID,
          (object) row1.VendorLocationID
        }));
        PXResultset<PX.Objects.CM.CurrencyInfo> pxResultset;
        if (rqBiddingVendor == null)
          pxResultset = (PXResultset<PX.Objects.CM.CurrencyInfo>) null;
        else
          pxResultset = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Select(new object[1]
          {
            (object) rqBiddingVendor.CuryInfoID
          });
        PX.Objects.CM.CurrencyInfo currencyInfo2 = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResultset);
        bool flag1 = false;
        if (currencyInfo2 == null)
        {
          row1.CuryID = ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current.CuryID ?? ((PXSelectBase<Company>) this.company).Current.BaseCuryID;
          try
          {
            PXDBCurrencyAttribute.SetBaseCalc<RQRequisitionLine.curyEstUnitCost>(((PXSelectBase) this.Lines).Cache, (object) null, false);
            currencyInfo1 = CurrencyInfoAttribute.SetDefaults<RQRequisition.curyInfoID>(sender, (object) row1);
          }
          finally
          {
            PXDBCurrencyAttribute.SetBaseCalc<RQRequisitionLine.curyEstUnitCost>(((PXSelectBase) this.Lines).Cache, (object) null, true);
          }
          flag1 = true;
        }
        else if (!((PXSelectBase) this.currencyinfo).Cache.ObjectsEqual<PX.Objects.CM.CurrencyInfo.curyID, PX.Objects.CM.CurrencyInfo.curyRateTypeID, PX.Objects.CM.CurrencyInfo.curyRate, PX.Objects.CM.CurrencyInfo.curyEffDate>((object) currencyInfo1, (object) currencyInfo2))
        {
          long? curyInfoId1 = currencyInfo1.CuryInfoID;
          PXCache<PX.Objects.CM.CurrencyInfo>.RestoreCopy(currencyInfo1, currencyInfo2);
          currencyInfo1.CuryInfoID = curyInfoId1;
          try
          {
            PXDBCurrencyAttribute.SetBaseCalc<RQRequisitionLine.curyEstUnitCost>(((PXSelectBase) this.Lines).Cache, (object) null, false);
            long? curyInfoId2 = (long?) CurrencyCollection.GetCurrency(currencyInfo1.BaseCuryID)?.CuryInfoID;
            long? curyInfoId3 = currencyInfo1.CuryInfoID;
            if (!(curyInfoId2.GetValueOrDefault() == curyInfoId3.GetValueOrDefault() & curyInfoId2.HasValue == curyInfoId3.HasValue))
            {
              long? curyInfoId4 = (long?) CurrencyCollection.GetBaseCurrency()?.CuryInfoID;
              long? curyInfoId5 = currencyInfo1.CuryInfoID;
              if (!(curyInfoId4.GetValueOrDefault() == curyInfoId5.GetValueOrDefault() & curyInfoId4.HasValue == curyInfoId5.HasValue))
              {
                ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Update(currencyInfo1);
                row1.CuryID = currencyInfo1.CuryID;
                goto label_26;
              }
            }
            currencyInfo1.CuryInfoID = new long?();
            currencyInfo1 = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Insert(currencyInfo1);
            sender.SetValueExt<RQRequisition.curyInfoID>((object) row1, (object) currencyInfo1.CuryInfoID);
            sender.SetValueExt<RQRequisition.curyID>((object) row1, (object) currencyInfo1.CuryID);
          }
          finally
          {
            PXDBCurrencyAttribute.SetBaseCalc<RQRequisitionLine.curyEstUnitCost>(((PXSelectBase) this.Lines).Cache, (object) null, true);
          }
label_26:
          flag1 = true;
        }
        if (flag1)
        {
          string error = PXUIFieldAttribute.GetError<PX.Objects.CM.CurrencyInfo.curyEffDate>(((PXSelectBase) this.currencyinfo).Cache, (object) currencyInfo1);
          if (!string.IsNullOrEmpty(error))
            sender.RaiseExceptionHandling<RQRequisition.orderDate>(e.Row, (object) ((RQRequisition) e.Row).OrderDate, (Exception) new PXSetPropertyException(error, (PXErrorLevel) 2));
          PXView view = ((PXSelectBase) this.Lines).View;
          object[] objArray1 = new object[1]{ e.Row };
          object[] objArray2 = Array.Empty<object>();
          foreach (RQRequisitionLine line in view.SelectMultiBound(objArray1, objArray2))
          {
            RQBidding bidding = PXResultset<RQBidding>.op_Implicit(PXSelectBase<RQBidding, PXSelect<RQBidding, Where<RQBidding.reqNbr, Equal<Current<RQRequisitionLine.reqNbr>>, And<RQBidding.lineNbr, Equal<Current<RQRequisitionLine.lineNbr>>, And<RQBidding.vendorID, Equal<Current<RQBiddingVendor.vendorID>>, And<RQBidding.vendorLocationID, Equal<Current<RQBiddingVendor.vendorLocationID>>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[2]
            {
              (object) line,
              (object) this.vendor
            }, Array.Empty<object>()));
            if (bidding != null)
            {
              this.CopyUnitCost(line, bidding);
            }
            else
            {
              RQRequisitionLine copy = (RQRequisitionLine) ((PXSelectBase) this.Lines).Cache.CreateCopy((object) line);
              bool? manualPrice = copy.ManualPrice;
              bool flag2 = false;
              if (manualPrice.GetValueOrDefault() == flag2 & manualPrice.HasValue)
              {
                PXCache cache1 = ((PXSelectBase) this.Lines).Cache;
                RQRequisitionLine row2 = copy;
                Decimal? nullable2 = copy.CuryEstUnitCost;
                Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
                Decimal num1;
                ref Decimal local1 = ref num1;
                PXCurrencyAttribute.CuryConvBase<RQRequisitionLine.curyInfoID>(cache1, (object) row2, valueOrDefault1, out local1);
                Decimal num2 = num1;
                nullable2 = copy.EstUnitCost;
                Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
                if (!(num2 == valueOrDefault2 & nullable2.HasValue))
                {
                  PXCache cache2 = ((PXSelectBase) this.Lines).Cache;
                  RQRequisitionLine row3 = copy;
                  nullable2 = copy.EstUnitCost;
                  Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
                  Decimal num3;
                  ref Decimal local2 = ref num3;
                  PXCurrencyAttribute.CuryConvCury<RQRequisitionLine.curyInfoID>(cache2, (object) row3, valueOrDefault3, out local2, true);
                  copy.CuryEstUnitCost = new Decimal?(num3);
                }
              }
              ((PXSelectBase) this.Lines).Cache.SetDefaultExt<RQRequisitionLine.curyEstUnitCost>((object) copy);
              ((PXSelectBase) this.Lines).Cache.SetDefaultExt<RQRequisitionLine.siteID>((object) copy);
              ((PXSelectBase<RQRequisitionLine>) this.Lines).Update(copy);
            }
          }
        }
      }
    }
    if (row1.Hold.GetValueOrDefault())
      return;
    if (((IEnumerable<PXResult<RQRequisitionLine>>) ((PXSelectBase<RQRequisitionLine>) this.Lines).Select(Array.Empty<object>())).AsEnumerable<PXResult<RQRequisitionLine>>().Any<PXResult<RQRequisitionLine>>((Func<PXResult<RQRequisitionLine>, bool>) (_ => !PXResult<RQRequisitionLine>.op_Implicit(_).InventoryID.HasValue)))
      sender.RaiseExceptionHandling<RQRequisition.hold>((object) row1, (object) row1.Hold, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[2]
      {
        (object) PXUIFieldAttribute.GetDisplayName<RQRequisitionLine.inventoryID>(((PXSelectBase) this.Lines).Cache),
        (object) (PXErrorLevel) 4
      }));
    else
      sender.RaiseExceptionHandling<RQRequisition.hold>((object) row1, (object) row1.Hold, (Exception) null);
  }

  protected virtual void RQRequisition_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    RQRequisition row = (RQRequisition) e.Row;
    if ((e.Operation & 3) == 3)
      return;
    PXDefaultAttribute.SetPersistingCheck<RQRequisition.siteID>(sender, (object) row, row.ShipDestType == "S" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<RQRequisition.shipToLocationID>(sender, (object) row, row.ShipDestType != "S" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<RQRequisition.shipToBAccountID>(sender, (object) row, row.ShipDestType != "S" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    if (row == null || string.IsNullOrEmpty(row.ShipVia))
      return;
    PX.Objects.CS.Carrier carrier = (PX.Objects.CS.Carrier) PXSelectorAttribute.Select<RQRequisition.shipVia>(sender, (object) row);
    int num;
    if (carrier == null)
    {
      num = 0;
    }
    else
    {
      bool? isActive = carrier.IsActive;
      bool flag = false;
      num = isActive.GetValueOrDefault() == flag & isActive.HasValue ? 1 : 0;
    }
    if (num == 0)
      return;
    sender.RaiseExceptionHandling<RQRequisition.shipVia>((object) row, (object) row.ShipVia, (Exception) new PXSetPropertyException((IBqlTable) row, "The Ship Via code is not active.", (PXErrorLevel) 2));
  }

  /// <summary>
  /// Copies the unit cost from <see cref="T:PX.Objects.RQ.RQBidding" /> to <see cref="T:PX.Objects.RQ.RQRequisitionLine" /> on RQRequisition row updated event. This is an extension point used by Lexware PriceUnit customization.
  /// </summary>
  /// <param name="line">The line.</param>
  /// <param name="bidding">The bidding.</param>
  public virtual void CopyUnitCost(RQRequisitionLine line, RQBidding bidding)
  {
    Decimal? minQty1 = bidding.MinQty;
    Decimal num1 = 0M;
    if (minQty1.GetValueOrDefault() == num1 & minQty1.HasValue)
    {
      Decimal? quoteQty = bidding.QuoteQty;
      Decimal num2 = 0M;
      if (quoteQty.GetValueOrDefault() == num2 & quoteQty.HasValue)
        goto label_4;
    }
    Decimal? minQty2 = bidding.MinQty;
    Decimal? orderQty1 = line.OrderQty;
    if (!(minQty2.GetValueOrDefault() <= orderQty1.GetValueOrDefault() & minQty2.HasValue & orderQty1.HasValue))
      return;
    Decimal? quoteQty1 = bidding.QuoteQty;
    Decimal? orderQty2 = line.OrderQty;
    if (!(quoteQty1.GetValueOrDefault() >= orderQty2.GetValueOrDefault() & quoteQty1.HasValue & orderQty2.HasValue))
      return;
label_4:
    RQRequisitionLine copy = (RQRequisitionLine) ((PXSelectBase) this.Lines).Cache.CreateCopy((object) line);
    Decimal curyval;
    PXCurrencyAttribute.CuryConvCury<RQRequisitionLine.curyInfoID>(((PXSelectBase) this.Lines).Cache, (object) copy, bidding.QuoteUnitCost.GetValueOrDefault(), out curyval, true);
    copy.CuryEstUnitCost = new Decimal?(curyval);
    ((PXSelectBase<RQRequisitionLine>) this.Lines).Update(copy);
  }

  protected virtual void RQRequisition_CustomerLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    RQRequisition row = (RQRequisition) e.Row;
    row.Quoted = new bool?(!row.CustomerLocationID.HasValue);
    if (row == null || !row.Hold.GetValueOrDefault())
      return;
    foreach (PXResult<RQRequisitionLine> pxResult in ((PXSelectBase<RQRequisitionLine>) this.Lines).Select(new object[1]
    {
      (object) row.ReqNbr
    }))
    {
      RQRequisitionLine copy = (RQRequisitionLine) ((PXSelectBase) this.Lines).Cache.CreateCopy((object) PXResult<RQRequisitionLine>.op_Implicit(pxResult));
      ((PXSelectBase) this.Lines).Cache.SetDefaultExt<RQRequisitionLine.curyEstUnitCost>((object) copy);
      ((PXSelectBase) this.Lines).Cache.SetDefaultExt<RQRequisitionLine.siteID>((object) copy);
      ((PXSelectBase<RQRequisitionLine>) this.Lines).Update(copy);
    }
  }

  protected virtual void RQRequisition_POType_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!((RQRequisition) e.Row).CustomerID.HasValue && (string) e.NewValue == "DP")
      throw new PXSetPropertyException("Drop ship order type allowed only for customer requisition.");
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<RQRequisition.vendorID> e)
  {
    RQRequisition row = (RQRequisition) e.Row;
    PX.Objects.AP.Vendor vendor = PX.Objects.AP.Vendor.PK.Find(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<RQRequisition.vendorID>>) e).Cache.Graph, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<RQRequisition.vendorID>, object, object>) e).NewValue);
    if (vendor == null || !PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    PX.Objects.CM.CurrencyInfo currencyInfo1 = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Select(new object[1]
    {
      (object) row.CuryInfoID
    }));
    RQBiddingVendor rqBiddingVendor = PXResultset<RQBiddingVendor>.op_Implicit(((PXSelectBase<RQBiddingVendor>) this.Vendors).Search<RQBiddingVendor.reqNbr, RQBiddingVendor.vendorID, RQBiddingVendor.vendorLocationID>((object) row.ReqNbr, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<RQRequisition.vendorID>, object, object>) e).NewValue, (object) vendor.DefLocationID, Array.Empty<object>()));
    PXResultset<PX.Objects.CM.CurrencyInfo> pxResultset;
    if (rqBiddingVendor == null)
      pxResultset = (PXResultset<PX.Objects.CM.CurrencyInfo>) null;
    else
      pxResultset = ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Select(new object[1]
      {
        (object) rqBiddingVendor.CuryInfoID
      });
    PX.Objects.CM.CurrencyInfo currencyInfo2 = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResultset);
    string str1;
    string str2;
    if (currencyInfo2 != null)
    {
      str1 = currencyInfo2.CuryID;
      str2 = currencyInfo2.CuryRateTypeID;
    }
    else
    {
      str1 = vendor.CuryID;
      str2 = vendor.CuryRateTypeID;
    }
    if (str1 == null)
      str1 = ((PXSelectBase<Company>) this.company).Current.BaseCuryID;
    if (str2 == null)
      str2 = ((PXSelectBase<CMSetup>) this.cmsetup).Current.APRateTypeDflt;
    string str3 = (string) null;
    if (str1 != currencyInfo1.CuryID)
      str3 = PXMessages.LocalizeFormatNoPrefixNLA("The currency of the vendor is {0}. Do you want to change the currency in the requisition document to the currency of the vendor?", new object[1]
      {
        (object) str1
      });
    else if (str1 != ((PXSelectBase<Company>) this.company).Current.BaseCuryID && str2 != null && str2 != currencyInfo1.CuryRateTypeID)
      str3 = PXMessages.LocalizeFormatNoPrefixNLA("The currency rate of the vendor is {0}. Do you want to change the currency rate in the requisition document to the currency rate of the vendor?", new object[1]
      {
        (object) str2
      });
    this._useCustomerCurrency = false;
    if (str3 == null)
      return;
    if (PXLongOperation.IsLongOperationContext())
    {
      bool? nullable = row.SkipValidateWithVendorCuryOrRate;
      if (nullable.GetValueOrDefault())
      {
        this._useCustomerCurrency = true;
      }
      else
      {
        nullable = vendor.AllowOverrideCury;
        int num;
        if (!nullable.GetValueOrDefault())
        {
          nullable = vendor.AllowOverrideRate;
          num = nullable.GetValueOrDefault() ? 1 : 0;
        }
        else
          num = 1;
        this._useCustomerCurrency = num != 0;
      }
    }
    else
    {
      if (((PXSelectBase<RQRequisition>) this.Document).Ask(row, "Warning", str3, (MessageButtons) 4) == 6)
        return;
      bool? nullable = vendor.AllowOverrideCury;
      if (!nullable.GetValueOrDefault())
      {
        nullable = vendor.AllowOverrideRate;
        if (!nullable.GetValueOrDefault())
        {
          ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<RQRequisition.vendorID>, object, object>) e).NewValue = (object) row.VendorID;
          ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<RQRequisition.vendorID>>) e).Cancel = true;
          return;
        }
      }
      this._useCustomerCurrency = true;
    }
  }

  protected virtual void RQRequisition_VendorID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<RQRequisition.vendorLocationID>(e.Row);
    sender.SetDefaultExt<RQRequisition.termsID>(e.Row);
    object vendorRefNbr = (object) ((RQRequisition) e.Row).VendorRefNbr;
    sender.RaiseFieldVerifying<RQRequisition.vendorRefNbr>(e.Row, ref vendorRefNbr);
  }

  protected virtual void RQRequisition_VendorLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    PX.Objects.CR.Location current = ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current;
    RQRequisition row = (RQRequisition) e.Row;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      int? vendorId = row.VendorID;
      if (baccountId.GetValueOrDefault() == vendorId.GetValueOrDefault() & baccountId.HasValue == vendorId.HasValue)
      {
        int? locationId = current.LocationID;
        int? vendorLocationId = row.VendorLocationID;
        if (locationId.GetValueOrDefault() == vendorLocationId.GetValueOrDefault() & locationId.HasValue == vendorLocationId.HasValue)
          goto label_4;
      }
    }
    ((PXSelectBase<PX.Objects.CR.Location>) this.location).Current = PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) this.location).Select(Array.Empty<object>()));
label_4:
    sender.SetDefaultExt<RQRequisition.shipVia>(e.Row);
    sender.SetDefaultExt<RQRequisition.fOBPoint>(e.Row);
    if (row.ShipDestType != "S")
      sender.SetDefaultExt<RQRequisition.siteID>(e.Row);
    if (row.ShipDestType == "V")
      sender.SetDefaultExt<RQRequisition.shipToLocationID>(e.Row);
    sender.SetDefaultExt<RQRequisition.biddingComplete>(e.Row);
    SharedRecordAttribute.DefaultRecord<RQRequisition.remitAddressID>(sender, e.Row);
    SharedRecordAttribute.DefaultRecord<RQRequisition.remitContactID>(sender, e.Row);
    foreach (PXResult<RQRequisitionLine> pxResult in ((PXSelectBase<RQRequisitionLine>) this.Lines).Select(new object[1]
    {
      (object) row.ReqNbr
    }))
    {
      RQRequisitionLine copy = (RQRequisitionLine) ((PXSelectBase) this.Lines).Cache.CreateCopy((object) PXResult<RQRequisitionLine>.op_Implicit(pxResult));
      ((PXSelectBase) this.Lines).Cache.SetDefaultExt<RQRequisitionLine.curyEstUnitCost>((object) copy);
      try
      {
        if (!copy.SiteID.HasValue)
          ((PXSelectBase) this.Lines).Cache.SetDefaultExt<RQRequisitionLine.siteID>((object) copy);
        ((PXSelectBase) this.Lines).Cache.SetDefaultExt<RQRequisitionLine.rcptQtyAction>((object) copy);
        ((PXSelectBase) this.Lines).Cache.SetDefaultExt<RQRequisitionLine.rcptQtyMin>((object) copy);
        ((PXSelectBase) this.Lines).Cache.SetDefaultExt<RQRequisitionLine.rcptQtyMax>((object) copy);
        ((PXSelectBase) this.Lines).Cache.SetDefaultExt<RQRequisitionLine.rcptQtyThreshold>((object) copy);
      }
      catch
      {
      }
      ((PXSelectBase<RQRequisitionLine>) this.Lines).Update(copy);
    }
  }

  protected virtual void RQRequisition_VendorRequestSent_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    RQRequisition row = (RQRequisition) e.Row;
    if (!row.VendorRequestSent.GetValueOrDefault())
      return;
    foreach (PXResult<RQBiddingVendor> pxResult in ((PXSelectBase<RQBiddingVendor>) this.Vendors).Select(new object[1]
    {
      (object) row.ReqNbr
    }))
    {
      RQBiddingVendor copy = PXCache<RQBiddingVendor>.CreateCopy(PXResult<RQBiddingVendor>.op_Implicit(pxResult));
      copy.Status = new bool?(true);
      ((PXSelectBase<RQBiddingVendor>) this.Vendors).Update(copy);
    }
  }

  protected virtual void RQRequisition_Hold_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    RQRequisition row = (RQRequisition) e.Row;
    if (!row.Hold.GetValueOrDefault())
      return;
    bool? hold = row.Hold;
    bool? nullable = (bool?) e.OldValue;
    if (hold.GetValueOrDefault() == nullable.GetValueOrDefault() & hold.HasValue == nullable.HasValue)
      return;
    cache.SetDefaultExt<RQRequisition.biddingComplete>(e.Row);
    cache.SetDefaultExt<RQRequisition.quoted>(e.Row);
    nullable = row.Cancelled;
    if (nullable.GetValueOrDefault())
      cache.SetValueExt<RQRequisition.cancelled>((object) row, (object) false);
    foreach (PXResult<RQBiddingVendor> pxResult in ((PXSelectBase<RQBiddingVendor>) this.Vendors).Select(new object[1]
    {
      (object) row.ReqNbr
    }))
    {
      RQBiddingVendor copy = PXCache<RQBiddingVendor>.CreateCopy(PXResult<RQBiddingVendor>.op_Implicit(pxResult));
      copy.Status = new bool?(false);
      ((PXSelectBase<RQBiddingVendor>) this.Vendors).Update(copy);
    }
  }

  protected virtual void RQRequisitionLine_InventoryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<RQRequisitionLine.uOM>(e.Row);
    sender.SetDefaultExt<RQRequisitionLine.description>(e.Row);
    sender.SetDefaultExt<RQRequisitionLine.subItemID>(e.Row);
    sender.SetDefaultExt<RQRequisitionLine.estUnitCost>(e.Row);
    RQRequisitionLine row = (RQRequisitionLine) e.Row;
    if ((row != null ? (!row.ManualPrice.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      sender.SetValue<RQRequisitionLine.curyEstUnitCost>(e.Row, (object) null);
    sender.SetDefaultExt<RQRequisitionLine.curyEstUnitCost>(e.Row);
    sender.SetDefaultExt<RQRequisitionLine.promisedDate>(e.Row);
    sender.SetDefaultExt<RQRequisitionLine.markupPct>(e.Row);
    sender.SetDefaultExt<RQRequisitionLine.rcptQtyAction>(e.Row);
    sender.SetDefaultExt<RQRequisitionLine.rcptQtyMax>(e.Row);
    sender.SetDefaultExt<RQRequisitionLine.rcptQtyMin>(e.Row);
  }

  protected virtual void RQRequisitionLine_SiteID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    RQRequisitionLine row = (RQRequisitionLine) e.Row;
    if (row == null)
      return;
    int? nullable1;
    if (((PXSelectBase<RQRequisition>) this.Document).Current != null)
    {
      nullable1 = ((PXSelectBase<RQRequisition>) this.Document).Current.CustomerLocationID;
      if (nullable1.HasValue)
        return;
    }
    if (!PXAccess.FeatureInstalled<FeaturesSet.inventory>())
    {
      e.NewValue = (object) null;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      int? nullable2 = new int?();
      foreach (PXResult<RQRequisitionContent> pxResult1 in PXSelectBase<RQRequisitionContent, PXSelect<RQRequisitionContent, Where<RQRequisitionContent.reqNbr, Equal<Required<RQRequisitionContent.reqNbr>>, And<RQRequisitionContent.reqLineNbr, Equal<Required<RQRequisitionContent.reqLineNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.ReqNbr,
        (object) row.LineNbr
      }))
      {
        PXResult<RQRequest, RQRequestClass, PX.Objects.CR.Location> pxResult2 = (PXResult<RQRequest, RQRequestClass, PX.Objects.CR.Location>) PXResultset<RQRequest>.op_Implicit(PXSelectBase<RQRequest, PXSelectJoin<RQRequest, InnerJoin<RQRequestClass, On<RQRequestClass.reqClassID, Equal<RQRequest.reqClassID>>, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<RQRequest.employeeID>, And<PX.Objects.CR.Location.locationID, Equal<RQRequest.locationID>>>>>, Where<RQRequest.orderNbr, Equal<Required<RQRequest.orderNbr>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
        {
          (object) PXResult<RQRequisitionContent>.op_Implicit(pxResult1).OrderNbr
        }));
        RQRequestClass rqRequestClass = PXResult<RQRequest, RQRequestClass, PX.Objects.CR.Location>.op_Implicit(pxResult2);
        PX.Objects.CR.Location location = PXResult<RQRequest, RQRequestClass, PX.Objects.CR.Location>.op_Implicit(pxResult2);
        if (location != null && location.LocationCD != null)
        {
          int? nullable3 = rqRequestClass.CustomerRequest.GetValueOrDefault() ? location.CSiteID : location.CMPSiteID;
          if (!nullable2.HasValue)
          {
            nullable2 = nullable3;
          }
          else
          {
            nullable1 = nullable2;
            int? nullable4 = nullable3;
            if (!(nullable1.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable1.HasValue == nullable4.HasValue))
            {
              nullable2 = new int?();
              break;
            }
          }
        }
      }
      if (!nullable2.HasValue)
        return;
      e.NewValue = (object) nullable2;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void RQRequisitionLine_Availability_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    RQRequisitionLine row = (RQRequisitionLine) e.Row;
    if (row == null)
    {
      e.ReturnValue = (object) string.Empty;
    }
    else
    {
      INSiteStatusByCostCenter statusByCostCenter = INSiteStatusByCostCenter.PK.Find((PXGraph) this, row.InventoryID, row.SubItemID, row.SiteID, new int?(0));
      if (statusByCostCenter != null)
      {
        statusByCostCenter.QtyOnHand = new Decimal?(INUnitAttribute.ConvertFromBase<RQRequisitionLine.inventoryID, RQRequisitionLine.uOM>(sender, e.Row, statusByCostCenter.QtyOnHand.Value, INPrecision.QUANTITY));
        statusByCostCenter.QtyAvail = new Decimal?(INUnitAttribute.ConvertFromBase<RQRequisitionLine.inventoryID, RQRequisitionLine.uOM>(sender, e.Row, statusByCostCenter.QtyAvail.Value, INPrecision.QUANTITY));
        statusByCostCenter.QtyNotAvail = new Decimal?(INUnitAttribute.ConvertFromBase<RQRequisitionLine.inventoryID, RQRequisitionLine.uOM>(sender, e.Row, statusByCostCenter.QtyNotAvail.Value, INPrecision.QUANTITY));
        statusByCostCenter.QtyHardAvail = new Decimal?(INUnitAttribute.ConvertFromBase<RQRequisitionLine.inventoryID, RQRequisitionLine.uOM>(sender, e.Row, statusByCostCenter.QtyHardAvail.Value, INPrecision.QUANTITY));
        e.ReturnValue = (object) PXMessages.LocalizeFormatNoPrefix("On Hand {1} {0}, Available {2} {0}, Available for Shipping {3} {0}", new object[4]
        {
          sender.GetValue<RQRequisitionLine.uOM>(e.Row),
          (object) this.FormatQty(statusByCostCenter.QtyOnHand),
          (object) this.FormatQty(statusByCostCenter.QtyAvail),
          (object) this.FormatQty(statusByCostCenter.QtyHardAvail)
        });
      }
      else
        e.ReturnValue = (object) string.Empty;
    }
  }

  protected virtual string FormatQty(Decimal? value)
  {
    return value.HasValue ? value.Value.ToString("N" + CommonSetupDecPl.Qty.ToString(), (IFormatProvider) NumberFormatInfo.CurrentInfo) : string.Empty;
  }

  protected virtual void RQRequisitionLine_SubItemID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!((PXSelectBase<RQRequisition>) this.Document).Current.Hold.GetValueOrDefault())
      return;
    if (!((RQRequisitionLine) e.Row).ManualPrice.GetValueOrDefault())
      sender.SetValue<RQRequisitionLine.curyEstUnitCost>(e.Row, (object) null);
    sender.SetDefaultExt<RQRequisitionLine.curyEstUnitCost>(e.Row);
  }

  protected virtual void RQRequisitionLine_VendorLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<RQRequisitionLine.curyEstUnitCost>(e.Row);
  }

  protected virtual void RQRequisitionLine_CuryEstUnitCost_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is RQRequisitionLine row1))
      return;
    e.NewValue = (object) row1.CuryEstUnitCost;
    RQRequisition current = ((PXSelectBase<RQRequisition>) this.Document).Current;
    RQBidding rqBidding = PXResultset<RQBidding>.op_Implicit(PXSelectBase<RQBidding, PXSelect<RQBidding, Where<RQBidding.reqNbr, Equal<Current<RQRequisitionLine.reqNbr>>, And<RQBidding.lineNbr, Equal<Current<RQRequisitionLine.lineNbr>>, And<RQBidding.vendorID, Equal<Current<RQRequisition.vendorID>>, And<RQBidding.vendorLocationID, Equal<Current<RQRequisition.vendorLocationID>>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[2]
    {
      (object) row1,
      (object) current
    }, Array.Empty<object>()));
    Decimal? nullable1;
    if (rqBidding != null)
    {
      nullable1 = rqBidding.MinQty;
      Decimal? orderQty1 = row1.OrderQty;
      if (nullable1.GetValueOrDefault() > orderQty1.GetValueOrDefault() & nullable1.HasValue & orderQty1.HasValue)
      {
        Decimal? orderQty2 = row1.OrderQty;
        nullable1 = rqBidding.QuoteQty;
        if (orderQty2.GetValueOrDefault() < nullable1.GetValueOrDefault() & orderQty2.HasValue & nullable1.HasValue)
        {
          if ((string) ((PXSelectBase<RQBidding>) this.Bidding).GetValueExt<RQBidding.curyID>(rqBidding) == current.CuryID)
          {
            e.NewValue = (object) rqBidding.CuryQuoteUnitCost;
            return;
          }
          PXCache cache = ((PXSelectBase) this.Lines).Cache;
          RQRequisitionLine row2 = row1;
          nullable1 = rqBidding.QuoteUnitCost;
          Decimal valueOrDefault = nullable1.GetValueOrDefault();
          Decimal num;
          ref Decimal local = ref num;
          PXCurrencyAttribute.CuryConvCury<RQRequisitionLine.curyInfoID>(cache, (object) row2, valueOrDefault, out local, true);
          e.NewValue = (object) num;
          return;
        }
      }
    }
    if (current == null || !row1.InventoryID.HasValue)
      return;
    bool? nullable2 = current.Hold;
    if (!nullable2.GetValueOrDefault())
      return;
    nullable2 = row1.ManualPrice;
    if (nullable2.GetValueOrDefault())
    {
      nullable1 = row1.CuryEstUnitCost;
      if (nullable1.HasValue)
      {
        e.NewValue = (object) row1.CuryEstUnitCost;
        return;
      }
    }
    Decimal? nullable3 = new Decimal?();
    if (row1.UOM != null)
    {
      DateTime date = ((PXSelectBase<RQRequisition>) this.Document).Current.OrderDate.Value;
      PX.Objects.CM.CurrencyInfo currencyinfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Search<PX.Objects.CM.CurrencyInfo.curyInfoID>((object) current.CuryInfoID, Array.Empty<object>()));
      nullable3 = APVendorPriceMaint.CalculateUnitCost(sender, current.VendorID, current.VendorLocationID, row1.InventoryID, row1.SiteID, currencyinfo, row1.UOM, row1.OrderQty, date, row1.CuryEstUnitCost);
      e.NewValue = (object) nullable3;
    }
    if (!nullable3.HasValue)
    {
      Decimal? nullable4 = POItemCostManager.Fetch<RQRequisitionLine.inventoryID, RQRequisitionLine.curyInfoID>(sender.Graph, (object) row1, current.VendorID, current.VendorLocationID, current.OrderDate, current.CuryID, row1.InventoryID, row1.SubItemID, row1.SiteID, row1.UOM, e.NewValue != null);
      nullable1 = nullable4;
      Decimal num = 0M;
      if (nullable1.GetValueOrDefault() >= num & nullable1.HasValue)
        e.NewValue = (object) nullable4;
    }
    APVendorPriceMaint.CheckNewUnitCost<RQRequisitionLine, RQRequisitionLine.curyEstUnitCost>(sender, row1, e.NewValue);
  }

  protected virtual void RQRequisitionLine_Cancelled_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((RQRequisitionLine) e.Row).OrderQty = new Decimal?(0M);
  }

  protected virtual void RQRequisitionLine_OrderQty_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null || object.Equals(e.OldValue, (object) ((RQRequisitionLine) e.Row).OrderQty))
      return;
    if (!((RQRequisitionLine) e.Row).ManualPrice.GetValueOrDefault())
      sender.SetValue<RQRequisitionLine.curyEstUnitCost>(e.Row, (object) null);
    sender.SetDefaultExt<RQRequisitionLine.curyEstUnitCost>(e.Row);
  }

  protected virtual void RQRequisitionLine_UOM_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null || object.Equals(e.OldValue, (object) ((RQRequisitionLine) e.Row).UOM))
      return;
    if (!((RQRequisitionLine) e.Row).ManualPrice.GetValueOrDefault())
      sender.SetValue<RQRequisitionLine.curyEstUnitCost>(e.Row, (object) null);
    sender.SetDefaultExt<RQRequisitionLine.curyEstUnitCost>(e.Row);
  }

  protected virtual void RQRequisitionLine_SiteID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null || object.Equals(e.OldValue, (object) ((RQRequisitionLine) e.Row).SiteID))
      return;
    if (!((RQRequisitionLine) e.Row).ManualPrice.GetValueOrDefault())
      sender.SetValue<RQRequisitionLine.curyEstUnitCost>(e.Row, (object) null);
    sender.SetDefaultExt<RQRequisitionLine.curyEstUnitCost>(e.Row);
  }

  protected virtual void RQRequisitionLine_ManualPrice_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    RQRequisitionLine row = (RQRequisitionLine) e.Row;
    if (row == null || row.ManualPrice.GetValueOrDefault() || sender.Graph.IsCopyPasteContext)
      return;
    sender.SetValue<RQRequisitionLine.curyEstUnitCost>(e.Row, (object) null);
    sender.SetDefaultExt<RQRequisitionLine.curyEstUnitCost>(e.Row);
  }

  protected virtual void RQRequisitionLine_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    RQRequisitionLine row = (RQRequisitionLine) e.Row;
    RQRequisitionLine oldRow = (RQRequisitionLine) e.OldRow;
    bool? nullable;
    if (((PXSelectBase<RQRequisition>) this.Document).Current != null && ((PXSelectBase<RQRequisition>) this.Document).Current.Hold.GetValueOrDefault())
    {
      nullable = row.Cancelled;
      if (!nullable.GetValueOrDefault())
        row.OriginQty = row.OrderQty;
    }
    if (row == null)
      return;
    nullable = row.ByRequest;
    if (nullable.GetValueOrDefault() && row.UOM != oldRow.UOM)
    {
      foreach (PXResult<RQRequisitionContent> pxResult in PXSelectBase<RQRequisitionContent, PXSelect<RQRequisitionContent, Where<RQRequisitionContent.reqNbr, Equal<Required<RQRequisitionLine.reqNbr>>, And<RQRequisitionContent.reqLineNbr, Equal<Required<RQRequisitionLine.lineNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.ReqNbr,
        (object) row.LineNbr
      }))
      {
        RQRequisitionContent copy = PXCache<RQRequisitionContent>.CreateCopy(PXResult<RQRequisitionContent>.op_Implicit(pxResult));
        copy.RecalcOnly = new bool?(true);
        ((PXSelectBase<RQRequisitionContent>) this.Contents).Update(copy);
      }
    }
    if (!e.ExternalCall && !sender.Graph.IsImport || !sender.ObjectsEqual<RQRequisitionLine.branchID, RQRequisitionLine.inventoryID, RQRequisitionLine.siteID, RQRequisitionLine.uOM, RQRequisitionLine.orderQty, RQRequisitionLine.manualPrice>(e.Row, e.OldRow) || sender.ObjectsEqual<RQRequisitionLine.curyEstUnitCost, RQRequisitionLine.curyEstExtCost>(e.Row, e.OldRow))
      return;
    row.ManualPrice = new bool?(true);
  }

  protected virtual void RQRequisitionLine_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    RQRequisitionLine row = (RQRequisitionLine) e.Row;
    if (row == null)
      return;
    bool? nullable1 = ((PXSelectBase<RQRequisition>) this.Document).Current.Hold;
    PXPersistingCheck pxPersistingCheck = nullable1.GetValueOrDefault() ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1;
    PXDefaultAttribute.SetPersistingCheck<RQRequisitionLine.inventoryID>(sender, (object) row, pxPersistingCheck);
    int? nullable2 = row.InventoryID;
    if (!nullable2.HasValue)
    {
      nullable1 = ((PXSelectBase<RQRequisition>) this.Document).Current.Hold;
      if (!nullable1.GetValueOrDefault())
        sender.DisplayFieldError<RQRequisitionLine.inventoryID>((object) row, "'{0}' cannot be empty.", (object) PXUIFieldAttribute.GetDisplayName<RQRequisitionLine.inventoryID>(sender));
      else
        sender.ClearFieldSpecificError<RQRequisitionLine.inventoryID>((object) row, "'{0}' cannot be empty.", (object) PXUIFieldAttribute.GetDisplayName<RQRequisitionLine.inventoryID>(sender));
    }
    PXCache pxCache1 = sender;
    RQRequisitionLine rqRequisitionLine1 = row;
    nullable1 = row.ByRequest;
    int num1 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<RQRequisitionLine.orderQty>(pxCache1, (object) rqRequisitionLine1, num1 != 0);
    PXCache pxCache2 = sender;
    RQRequisitionLine rqRequisitionLine2 = row;
    nullable2 = row.InventoryID;
    int num2 = !nullable2.HasValue ? 0 : (row.LineType == "GI" ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<RQRequisitionLine.subItemID>(pxCache2, (object) rqRequisitionLine2, num2 != 0);
    PXUIFieldAttribute.SetEnabled<RQRequisitionLine.siteID>(sender, (object) row, true);
    PXCache pxCache3 = sender;
    RQRequisitionLine rqRequisitionLine3 = row;
    nullable1 = row.IsUseMarkup;
    int num3 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<RQRequisitionLine.markupPct>(pxCache3, (object) rqRequisitionLine3, num3 != 0);
    PXUIFieldAttribute.SetEnabled<RQRequisitionLine.lineType>(sender, (object) row, PXAccess.FeatureInstalled<FeaturesSet.inventory>());
    if (((PXSelectBase<RQRequisition>) this.Document).Current.Status == "P" || ((PXSelectBase<RQRequisition>) this.Document).Current.Status == "N" || ((PXSelectBase<RQRequisition>) this.Document).Current.Status == "Q")
      this.ValidateOpenState(row, (PXErrorLevel) 2);
    PXCache pxCache4 = sender;
    RQRequisitionLine rqRequisitionLine4 = row;
    nullable2 = ((PXSelectBase<RQRequisition>) this.Document).Current.VendorLocationID;
    int num4 = nullable2.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<RQRequisitionLine.alternateID>(pxCache4, (object) rqRequisitionLine4, num4 != 0);
  }

  private bool ValidateOpenState(RQRequisitionLine row, PXErrorLevel level)
  {
    bool flag = true;
    System.Type[] typeArray;
    if (!(row.LineType == "GI") || !row.InventoryID.HasValue)
    {
      if (!(row.LineType == "NS"))
        typeArray = new System.Type[1]
        {
          typeof (RQRequisitionLine.uOM)
        };
      else
        typeArray = new System.Type[2]
        {
          typeof (RQRequisitionLine.uOM),
          typeof (RQRequisitionLine.siteID)
        };
    }
    else
      typeArray = new System.Type[3]
      {
        typeof (RQRequisitionLine.uOM),
        typeof (RQRequisitionLine.siteID),
        typeof (RQRequisitionLine.subItemID)
      };
    foreach (System.Type type in typeArray)
    {
      object obj = ((PXSelectBase) this.Lines).Cache.GetValue((object) row, type.Name);
      if (obj == null)
      {
        ((PXSelectBase) this.Lines).Cache.RaiseExceptionHandling(type.Name, (object) row, (object) null, (Exception) new PXSetPropertyException("Should be defined before order creation.", level));
        flag = false;
      }
      else
        ((PXSelectBase) this.Lines).Cache.RaiseExceptionHandling(type.Name, (object) row, obj, (Exception) null);
    }
    return flag;
  }

  protected virtual void RQRequestLineSelect_SelectQty_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    RQRequestLineSelect row = (RQRequestLineSelect) e.Row;
    if (e.NewValue != null)
    {
      Decimal newValue = (Decimal) e.NewValue;
      Decimal? openQty = row.OpenQty;
      Decimal valueOrDefault = openQty.GetValueOrDefault();
      if (!(newValue > valueOrDefault & openQty.HasValue))
        goto label_3;
    }
    e.NewValue = (object) row.OpenQty;
label_3:
    if (e.NewValue == null || !((Decimal) e.NewValue > 0M))
      return;
    row.Selected = new bool?(true);
  }

  protected virtual void RQRequestLineSelect_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    RQRequestLineSelect newRow = (RQRequestLineSelect) e.NewRow;
    RQRequestLineSelect row = (RQRequestLineSelect) e.Row;
    if (!newRow.Selected.GetValueOrDefault())
      return;
    bool? selected1 = row.Selected;
    bool? selected2 = newRow.Selected;
    if (selected1.GetValueOrDefault() == selected2.GetValueOrDefault() & selected1.HasValue == selected2.HasValue || !(newRow.SelectQty.GetValueOrDefault() == 0M))
      return;
    newRow.SelectQty = newRow.OpenQty;
    newRow.BaseSelectQty = newRow.OpenQty;
  }

  protected virtual void RQRequestLineFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    RQRequestLineFilter row = (RQRequestLineFilter) e.Row;
    if (row == null)
      return;
    PXCache pxCache1 = sender;
    bool? allowUpdate = row.AllowUpdate;
    int num1 = allowUpdate.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<RQRequestLineFilter.inventoryID>(pxCache1, (object) null, num1 != 0);
    PXCache pxCache2 = sender;
    allowUpdate = row.AllowUpdate;
    int num2 = allowUpdate.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<RQRequestLineFilter.subItemID>(pxCache2, (object) null, num2 != 0);
    PXCache pxCache3 = sender;
    allowUpdate = row.AllowUpdate;
    int num3 = allowUpdate.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<RQRequestSelection.addExists>(pxCache3, (object) null, num3 != 0);
  }

  protected virtual void RQRequisitionContent_ItemQty_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    RQRequisitionContent row = (RQRequisitionContent) e.Row;
    RQRequestLine rqRequestLine = PXResultset<RQRequestLine>.op_Implicit(PXSelectBase<RQRequestLine, PXSelect<RQRequestLine, Where<RQRequestLine.orderNbr, Equal<Required<RQRequestLine.orderNbr>>, And<RQRequestLine.lineNbr, Equal<Required<RQRequestLine.lineNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.OrderNbr,
      (object) row.LineNbr
    }));
    Decimal? nullable1 = (Decimal?) e.NewValue;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    nullable1 = row.ItemQty;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal num1 = valueOrDefault1 - valueOrDefault2;
    if (!(num1 > 0M))
      return;
    Decimal? nullable2 = rqRequestLine.OpenQty;
    Decimal num2 = num1;
    if (!(nullable2.GetValueOrDefault() < num2 & nullable2.HasValue))
      return;
    PXFieldVerifyingEventArgs verifyingEventArgs = e;
    nullable2 = row.ItemQty;
    Decimal? openQty = rqRequestLine.OpenQty;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> local = (ValueType) (nullable2.HasValue & openQty.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + openQty.GetValueOrDefault()) : new Decimal?());
    verifyingEventArgs.NewValue = (object) local;
    sender.RaiseExceptionHandling<RQRequisitionContent.itemQty>((object) row, (object) null, (Exception) new PXSetPropertyException("Insufficient quantity available. Line quantity was changed to match.", (PXErrorLevel) 2));
  }

  protected virtual void RQRequisitionContent_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    RQRequisitionContent newRow = (RQRequisitionContent) e.NewRow;
    RQRequisitionContent row = (RQRequisitionContent) e.Row;
    RQRequestLine rqRequestLine = PXResultset<RQRequestLine>.op_Implicit(PXSelectBase<RQRequestLine, PXSelect<RQRequestLine, Where<RQRequestLine.orderNbr, Equal<Required<RQRequestLine.orderNbr>>, And<RQRequestLine.lineNbr, Equal<Required<RQRequestLine.lineNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) newRow.OrderNbr,
      (object) newRow.LineNbr
    }));
    RQRequisitionLine rqRequisitionLine = PXResultset<RQRequisitionLine>.op_Implicit(PXSelectBase<RQRequisitionLine, PXSelect<RQRequisitionLine, Where<RQRequisitionLine.reqNbr, Equal<Required<RQRequisitionLine.reqNbr>>, And<RQRequisitionLine.lineNbr, Equal<Required<RQRequisitionLine.lineNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) newRow.ReqNbr,
      (object) newRow.ReqLineNbr
    }));
    if (newRow.RecalcOnly.GetValueOrDefault())
    {
      if (rqRequestLine.UOM != rqRequisitionLine.UOM)
        newRow.ReqQty = new Decimal?(INUnitAttribute.ConvertFromBase(sender, rqRequisitionLine.InventoryID, rqRequisitionLine.UOM, newRow.BaseReqQty.GetValueOrDefault(), INPrecision.QUANTITY));
      else
        newRow.ReqQty = newRow.ItemQty;
    }
    else
    {
      int? inventoryId1 = rqRequestLine.InventoryID;
      if (!inventoryId1.HasValue)
      {
        Decimal? itemQty = newRow.ItemQty;
        Decimal? nullable1 = row.ItemQty;
        if (!(itemQty.GetValueOrDefault() == nullable1.GetValueOrDefault() & itemQty.HasValue == nullable1.HasValue))
        {
          RQRequisitionContent requisitionContent = newRow;
          newRow.ReqQty = nullable1 = newRow.BaseReqQty = newRow.ItemQty;
          Decimal? nullable2 = nullable1;
          requisitionContent.BaseItemQty = nullable2;
        }
        else
        {
          nullable1 = newRow.ReqQty;
          Decimal? nullable3 = row.ReqQty;
          if (nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue)
            return;
          RQRequisitionContent requisitionContent1 = newRow;
          RQRequisitionContent requisitionContent2 = newRow;
          newRow.BaseReqQty = nullable1 = newRow.ReqQty;
          Decimal? nullable4;
          nullable3 = nullable4 = nullable1;
          requisitionContent2.ItemQty = nullable4;
          Decimal? nullable5 = nullable3;
          requisitionContent1.BaseItemQty = nullable5;
        }
      }
      else
      {
        inventoryId1 = rqRequestLine.InventoryID;
        int? inventoryId2 = rqRequisitionLine.InventoryID;
        if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
        {
          Decimal? itemQty = newRow.ItemQty;
          Decimal? nullable6 = row.ItemQty;
          if (!(itemQty.GetValueOrDefault() == nullable6.GetValueOrDefault() & itemQty.HasValue == nullable6.HasValue))
          {
            RQRequisitionContent requisitionContent3 = newRow;
            RQRequisitionContent requisitionContent4 = newRow;
            nullable6 = new Decimal?(INUnitAttribute.ConvertToBase(sender, rqRequestLine.InventoryID, rqRequestLine.UOM, newRow.ItemQty.GetValueOrDefault(), INPrecision.QUANTITY));
            Decimal? nullable7 = nullable6;
            requisitionContent4.BaseItemQty = nullable7;
            Decimal? nullable8 = nullable6;
            requisitionContent3.BaseReqQty = nullable8;
            if (rqRequestLine.UOM != rqRequisitionLine.UOM)
            {
              RQRequisitionContent requisitionContent5 = newRow;
              PXCache sender1 = sender;
              int? inventoryId3 = rqRequisitionLine.InventoryID;
              string uom = rqRequisitionLine.UOM;
              nullable6 = newRow.BaseReqQty;
              Decimal valueOrDefault = nullable6.GetValueOrDefault();
              Decimal? nullable9 = new Decimal?(INUnitAttribute.ConvertFromBase(sender1, inventoryId3, uom, valueOrDefault, INPrecision.QUANTITY));
              requisitionContent5.ReqQty = nullable9;
            }
            else
              newRow.ReqQty = newRow.ItemQty;
          }
          nullable6 = newRow.ReqQty;
          Decimal? nullable10 = row.ReqQty;
          if (nullable6.GetValueOrDefault() == nullable10.GetValueOrDefault() & nullable6.HasValue == nullable10.HasValue)
            return;
          RQRequisitionContent requisitionContent6 = newRow;
          RQRequisitionContent requisitionContent7 = newRow;
          ref Decimal? local1 = ref nullable10;
          PXCache sender2 = sender;
          int? inventoryId4 = rqRequisitionLine.InventoryID;
          string uom1 = rqRequisitionLine.UOM;
          nullable6 = newRow.ReqQty;
          Decimal valueOrDefault1 = nullable6.GetValueOrDefault();
          Decimal num1 = INUnitAttribute.ConvertToBase(sender2, inventoryId4, uom1, valueOrDefault1, INPrecision.QUANTITY);
          local1 = new Decimal?(num1);
          Decimal? nullable11 = nullable10;
          requisitionContent7.BaseItemQty = nullable11;
          Decimal? nullable12 = nullable10;
          requisitionContent6.BaseReqQty = nullable12;
          PXCache sender3 = sender;
          int? inventoryId5 = rqRequestLine.InventoryID;
          string uom2 = rqRequestLine.UOM;
          nullable10 = newRow.BaseReqQty;
          Decimal valueOrDefault2 = nullable10.GetValueOrDefault();
          Decimal num2 = INUnitAttribute.ConvertFromBase(sender3, inventoryId5, uom2, valueOrDefault2, INPrecision.QUANTITY);
          if (rqRequestLine.UOM == rqRequisitionLine.UOM)
          {
            nullable10 = newRow.ReqQty;
            num2 = nullable10.GetValueOrDefault();
          }
          object obj = (object) num2;
          sender.RaiseFieldVerifying<RQRequisitionContent.itemQty>((object) newRow, ref obj);
          newRow.ItemQty = new Decimal?((Decimal) obj);
          if (!((Decimal) obj != num2))
            return;
          RQRequisitionContent requisitionContent8 = newRow;
          RQRequisitionContent requisitionContent9 = newRow;
          ref Decimal? local2 = ref nullable10;
          PXCache sender4 = sender;
          int? inventoryId6 = rqRequestLine.InventoryID;
          string uom3 = rqRequestLine.UOM;
          nullable6 = newRow.ItemQty;
          Decimal valueOrDefault3 = nullable6.GetValueOrDefault();
          Decimal num3 = INUnitAttribute.ConvertToBase(sender4, inventoryId6, uom3, valueOrDefault3, INPrecision.QUANTITY);
          local2 = new Decimal?(num3);
          Decimal? nullable13 = nullable10;
          requisitionContent9.BaseItemQty = nullable13;
          Decimal? nullable14 = nullable10;
          requisitionContent8.BaseReqQty = nullable14;
          if (rqRequestLine.UOM != rqRequisitionLine.UOM)
          {
            RQRequisitionContent requisitionContent10 = newRow;
            PXCache sender5 = sender;
            int? inventoryId7 = rqRequisitionLine.InventoryID;
            string uom4 = rqRequisitionLine.UOM;
            nullable10 = newRow.BaseReqQty;
            Decimal valueOrDefault4 = nullable10.GetValueOrDefault();
            Decimal? nullable15 = new Decimal?(INUnitAttribute.ConvertFromBase(sender5, inventoryId7, uom4, valueOrDefault4, INPrecision.QUANTITY));
            requisitionContent10.ReqQty = nullable15;
          }
          else
            newRow.ReqQty = newRow.ItemQty;
        }
        else
        {
          Decimal? itemQty = newRow.ItemQty;
          Decimal? nullable16 = row.ItemQty;
          if (!(itemQty.GetValueOrDefault() == nullable16.GetValueOrDefault() & itemQty.HasValue == nullable16.HasValue))
          {
            RQRequisitionContent requisitionContent = newRow;
            PXCache sender6 = sender;
            int? inventoryId8 = rqRequestLine.InventoryID;
            string uom = rqRequestLine.UOM;
            nullable16 = newRow.ItemQty;
            Decimal valueOrDefault = nullable16.GetValueOrDefault();
            Decimal? nullable17 = new Decimal?(INUnitAttribute.ConvertToBase(sender6, inventoryId8, uom, valueOrDefault, INPrecision.QUANTITY));
            requisitionContent.BaseItemQty = nullable17;
          }
          nullable16 = newRow.ReqQty;
          Decimal? reqQty = row.ReqQty;
          if (nullable16.GetValueOrDefault() == reqQty.GetValueOrDefault() & nullable16.HasValue == reqQty.HasValue)
            return;
          RQRequisitionContent requisitionContent11 = newRow;
          PXCache sender7 = sender;
          int? inventoryId9 = rqRequisitionLine.InventoryID;
          string uom5 = rqRequisitionLine.UOM;
          reqQty = newRow.ReqQty;
          Decimal valueOrDefault5 = reqQty.GetValueOrDefault();
          Decimal? nullable18 = new Decimal?(INUnitAttribute.ConvertToBase(sender7, inventoryId9, uom5, valueOrDefault5, INPrecision.QUANTITY));
          requisitionContent11.BaseReqQty = nullable18;
        }
      }
    }
  }

  protected virtual void RQBiddingVendor_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    RQBiddingVendor row = (RQBiddingVendor) e.Row;
    if (row == null || !row.VendorLocationID.HasValue)
      return;
    row.ReqNbr = ((PXSelectBase<RQRequisition>) this.Document).Current.ReqNbr;
    SharedRecordAttribute.DefaultRecord<RQBiddingVendor.remitAddressID>(sender, e.Row);
    SharedRecordAttribute.DefaultRecord<RQBiddingVendor.remitContactID>(sender, e.Row);
    ((CancelEventArgs) e).Cancel = !this.ValidateBiddingVendorDuplicates(sender, row, (RQBiddingVendor) null);
  }

  protected virtual void RQBiddingVendor_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    RQBiddingVendor row = (RQBiddingVendor) e.Row;
    RQBiddingVendor newRow = (RQBiddingVendor) e.NewRow;
    if (row == null || newRow == null || row == newRow)
      return;
    int? nullable = row.VendorID;
    int? vendorId = newRow.VendorID;
    if (nullable.GetValueOrDefault() == vendorId.GetValueOrDefault() & nullable.HasValue == vendorId.HasValue)
    {
      int? vendorLocationId = row.VendorLocationID;
      nullable = newRow.VendorLocationID;
      if (vendorLocationId.GetValueOrDefault() == nullable.GetValueOrDefault() & vendorLocationId.HasValue == nullable.HasValue)
        return;
    }
    ((CancelEventArgs) e).Cancel = !this.ValidateBiddingVendorDuplicates(sender, newRow, row);
  }

  protected virtual void RQBiddingVendor_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    RQBiddingVendor row = (RQBiddingVendor) e.Row;
    if (row == null)
      return;
    int num;
    if (PXResultset<RQBidding>.op_Implicit(PXSelectBase<RQBidding, PXSelect<RQBidding, Where<RQBidding.reqNbr, Equal<Current<RQBiddingVendor.reqNbr>>, And<RQBidding.vendorID, Equal<Current<RQBiddingVendor.vendorID>>, And<RQBidding.vendorLocationID, Equal<Current<RQBiddingVendor.vendorLocationID>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }, Array.Empty<object>())) != null)
    {
      int? nullable = row.VendorID;
      if (nullable.HasValue)
      {
        nullable = row.VendorLocationID;
        num = !nullable.HasValue ? 1 : 0;
        goto label_5;
      }
    }
    num = 1;
label_5:
    bool flag = num != 0;
    PXUIFieldAttribute.SetEnabled<RQBiddingVendor.vendorID>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<RQBiddingVendor.vendorLocationID>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<RQBiddingVendor.curyID>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<RQBiddingVendor.curyInfoID>(sender, (object) row, flag);
  }

  protected virtual void RQBiddingVendor_VendorID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendorBidder).Current = (PX.Objects.AP.Vendor) ((PXSelectBase) this.vendorBidder).View.SelectSingleBound(new object[1]
    {
      e.Row
    }, Array.Empty<object>());
    sender.SetDefaultExt<RQBiddingVendor.curyID>(e.Row);
    if (!e.ExternalCall)
      return;
    PX.Objects.CM.CurrencyInfo currencyInfo = CurrencyInfoAttribute.SetDefaults<RQBiddingVendor.curyInfoID>(sender, e.Row);
    string error = PXUIFieldAttribute.GetError<RQBiddingVendor.curyID>(((PXSelectBase) this.currencyinfo).Cache, (object) currencyInfo);
    RQBiddingVendor row = (RQBiddingVendor) e.Row;
    if (!string.IsNullOrEmpty(error))
      sender.RaiseExceptionHandling<RQBiddingVendor.curyInfoID>(e.Row, (object) row.CuryInfoID, (Exception) new PXSetPropertyException((IBqlTable) row, error, (PXErrorLevel) 2));
    if (currencyInfo == null)
      return;
    row.CuryID = currencyInfo.CuryID;
    row.CuryInfoID = currencyInfo.CuryInfoID;
  }

  protected virtual void RQBiddingVendor_VendorLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    SharedRecordAttribute.DefaultRecord<RQBiddingVendor.remitAddressID>(sender, e.Row);
    SharedRecordAttribute.DefaultRecord<RQBiddingVendor.remitContactID>(sender, e.Row);
  }

  protected virtual void RQBiddingVendor_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendorBidder).Current = (PX.Objects.AP.Vendor) null;
  }

  protected virtual void RQBiddingVendor_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendorBidder).Current = (PX.Objects.AP.Vendor) null;
  }

  public virtual void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers)
  {
    ((PXGraph) this).CopyPasteGetScript(isImportSimple, script, containers);
    foreach (int index in EnumerableExtensions.SelectIndexesWhere<Command>((IEnumerable<Command>) script, (Func<Command, bool>) (s => s.ObjectName.StartsWith("_RQBiddingVendor_CurrencyInfo_"))).Reverse<int>())
    {
      script.RemoveAt(index);
      containers.RemoveAt(index);
    }
  }

  private bool ValidateBiddingVendorDuplicates(
    PXCache sender,
    RQBiddingVendor row,
    RQBiddingVendor oldRow)
  {
    if (row.VendorLocationID.HasValue)
    {
      foreach (PXResult<RQBiddingVendor> pxResult in ((PXSelectBase<RQBiddingVendor>) this.Vendors).Select(new object[1]
      {
        (object) (row.ReqNbr ?? ((PXSelectBase<RQRequisition>) this.Document).Current.ReqNbr)
      }))
      {
        RQBiddingVendor rqBiddingVendor = PXResult<RQBiddingVendor>.op_Implicit(pxResult);
        int? nullable1 = rqBiddingVendor.VendorID;
        int? nullable2 = row.VendorID;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        {
          nullable2 = rqBiddingVendor.VendorLocationID;
          nullable1 = row.VendorLocationID;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          {
            nullable1 = row.LineID;
            nullable2 = rqBiddingVendor.LineID;
            if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
            {
              if (oldRow != null)
              {
                nullable2 = oldRow.VendorID;
                nullable1 = row.VendorID;
                if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
                  goto label_9;
              }
              sender.RaiseExceptionHandling<RQBiddingVendor.vendorID>((object) row, (object) row.VendorID, (Exception) new PXSetPropertyException("An attempt was made to add a duplicate entry."));
label_9:
              if (oldRow != null)
              {
                nullable1 = oldRow.VendorLocationID;
                nullable2 = row.VendorLocationID;
                if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
                  goto label_12;
              }
              sender.RaiseExceptionHandling<RQBiddingVendor.vendorLocationID>((object) row, (object) row.VendorLocationID, (Exception) new PXSetPropertyException("An attempt was made to add a duplicate entry."));
label_12:
              return false;
            }
          }
        }
      }
    }
    PXUIFieldAttribute.SetError<RQBiddingVendor.vendorID>(sender, (object) row, (string) null);
    PXUIFieldAttribute.SetError<RQBiddingVendor.vendorLocationID>(sender, (object) row, (string) null);
    return true;
  }

  public virtual void InsertRequestLine(RQRequestLine line, Decimal selectQty, bool mergeLines)
  {
    RQRequisitionLine rqLine = (RQRequisitionLine) null;
    RQRequest request = PXResultset<RQRequest>.op_Implicit(PXSelectBase<RQRequest, PXSelect<RQRequest, Where<RQRequest.orderNbr, Equal<Required<RQRequest.orderNbr>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) line.OrderNbr
    }));
    if (request == null)
      return;
    int? nullable1;
    if (mergeLines)
    {
      nullable1 = line.InventoryID;
      if (nullable1.HasValue && !line.ManualPrice.GetValueOrDefault())
      {
        List<object> objectList = new List<object>()
        {
          (object) ((PXSelectBase<RQRequisition>) this.Document).Current.ReqNbr,
          (object) line.InventoryID,
          (object) line.SubItemID,
          (object) line.Description
        };
        PXSelect<RQRequisitionLine, Where<RQRequisitionLine.reqNbr, Equal<Required<RQRequisitionLine.reqNbr>>, And<RQRequisitionLine.inventoryID, Equal<Required<RQRequisitionLine.inventoryID>>, And<RQRequisitionLine.subItemID, Equal<Required<RQRequisitionLine.subItemID>>, And<RQRequisitionLine.description, Equal<Required<RQRequisitionLine.description>>, And<RQRequisitionLine.byRequest, Equal<True>, And<RQRequisitionLine.manualPrice, Equal<False>>>>>>>> pxSelect = new PXSelect<RQRequisitionLine, Where<RQRequisitionLine.reqNbr, Equal<Required<RQRequisitionLine.reqNbr>>, And<RQRequisitionLine.inventoryID, Equal<Required<RQRequisitionLine.inventoryID>>, And<RQRequisitionLine.subItemID, Equal<Required<RQRequisitionLine.subItemID>>, And<RQRequisitionLine.description, Equal<Required<RQRequisitionLine.description>>, And<RQRequisitionLine.byRequest, Equal<True>, And<RQRequisitionLine.manualPrice, Equal<False>>>>>>>>((PXGraph) this);
        nullable1 = line.ExpenseAcctID;
        if (nullable1.HasValue)
        {
          nullable1 = line.ExpenseSubID;
          if (nullable1.HasValue)
          {
            ((PXSelectBase<RQRequisitionLine>) pxSelect).WhereAnd<Where<RQRequisitionLine.expenseAcctID, Equal<Required<RQRequisitionLine.expenseAcctID>>, And<RQRequisitionLine.expenseSubID, Equal<Required<RQRequisitionLine.expenseSubID>>>>>();
            objectList.AddRange((IEnumerable<object>) new object[2]
            {
              (object) line.ExpenseAcctID,
              (object) line.ExpenseSubID
            });
            goto label_8;
          }
        }
        ((PXSelectBase<RQRequisitionLine>) pxSelect).WhereAnd<Where<RQRequisitionLine.expenseAcctID, IsNull, And<RQRequisitionLine.expenseSubID, IsNull>>>();
label_8:
        rqLine = ((PXSelectBase<RQRequisitionLine>) pxSelect).SelectSingle(objectList.ToArray());
      }
    }
    if (rqLine == null)
    {
      rqLine = ((PXSelectBase<RQRequisitionLine>) this.Lines).Update(this.CreateNewRequisitionLineFromRequestLine(request, line));
      PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.SourceRequestLines).Cache, (object) line, ((PXSelectBase) this.Lines).Cache, (object) rqLine, (PXNoteAttribute.IPXCopySettings) null);
    }
    else
    {
      Decimal? nullable2 = new Decimal?();
      Decimal? nullable3 = new Decimal?();
      Decimal? nullable4;
      if (((PXSelectBase<RQRequisition>) this.Document).Current.CuryID == request.CuryID)
      {
        nullable2 = line.CuryEstUnitCost;
        nullable3 = line.CuryEstExtCost;
      }
      else
      {
        Decimal curyval;
        PXCurrencyAttribute.CuryConvCury<RQRequisitionLine.curyInfoID>(((PXSelectBase) this.Lines).Cache, (object) line, line.EstUnitCost.GetValueOrDefault(), out curyval, true);
        nullable2 = new Decimal?(curyval);
        PXCache cache = ((PXSelectBase) this.Lines).Cache;
        RQRequestLine row = line;
        nullable4 = line.EstExtCost;
        Decimal valueOrDefault = nullable4.GetValueOrDefault();
        ref Decimal local = ref curyval;
        PXCurrencyAttribute.CuryConvCury<RQRequisitionLine.curyInfoID>(cache, (object) row, valueOrDefault, out local);
        nullable3 = new Decimal?(curyval);
      }
      nullable4 = nullable2;
      Decimal num1 = 0M;
      if (!(nullable4.GetValueOrDefault() == num1 & nullable4.HasValue))
      {
        nullable4 = nullable2;
        Decimal? curyEstExtCost1 = rqLine.CuryEstExtCost;
        if (!(nullable4.GetValueOrDefault() == curyEstExtCost1.GetValueOrDefault() & nullable4.HasValue == curyEstExtCost1.HasValue))
        {
          Decimal? curyEstUnitCost = rqLine.CuryEstUnitCost;
          Decimal num2 = 0M;
          if (curyEstUnitCost.GetValueOrDefault() == num2 & curyEstUnitCost.HasValue)
            rqLine.CuryEstUnitCost = nullable2;
          Decimal? curyEstExtCost2 = rqLine.CuryEstExtCost;
          nullable4 = nullable3;
          Decimal? totalCost = curyEstExtCost2.HasValue & nullable4.HasValue ? new Decimal?(curyEstExtCost2.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
          RQRequisitionLine copy = PXCache<RQRequisitionLine>.CreateCopy(rqLine);
          this.UpdateExistingRQRequisitionLineCosts(copy, line, selectQty, totalCost, ((PXSelectBase<RQRequisition>) this.Document).Current.CuryID == request.CuryID);
          rqLine = ((PXSelectBase<RQRequisitionLine>) this.Lines).Update(copy);
        }
      }
    }
    this.UpdateContent(rqLine, line, selectQty);
    if (rqLine == null)
      return;
    RQRequisitionLine copy1 = PXCache<RQRequisitionLine>.CreateCopy(rqLine);
    if (copy1.LineType == null)
    {
      nullable1 = copy1.InventoryID;
      if (!nullable1.HasValue)
        copy1.LineType = "SV";
    }
    ((PXSelectBase) this.Lines).Cache.SetDefaultExt<RQRequisitionLine.siteID>((object) copy1);
    ((PXSelectBase) this.Lines).Cache.Update((object) copy1);
  }

  /// <summary>
  /// Creates new requisition line from request line on insertion of request line. This is an extension point used by Lexware PriceUnit customization
  /// </summary>
  /// <param name="request">The request.</param>
  /// <param name="requestLine">The request line.</param>
  /// <returns>
  /// The new new requisition line from request line on insert request line.
  /// </returns>
  protected virtual RQRequisitionLine CreateNewRequisitionLineFromRequestLine(
    RQRequest request,
    RQRequestLine requestLine)
  {
    RQRequisitionLine lineFromRequestLine = new RQRequisitionLine()
    {
      ReqNbr = ((PXSelectBase<RQRequisition>) this.Document).Current.ReqNbr,
      InventoryID = requestLine.InventoryID,
      SubItemID = requestLine.SubItemID,
      Description = requestLine.Description,
      UOM = requestLine.UOM,
      OrderQty = new Decimal?(0M),
      ManualPrice = requestLine.ManualPrice,
      ExpenseAcctID = requestLine.ExpenseAcctID,
      ExpenseSubID = requestLine.ExpenseSubID,
      RequestedDate = requestLine.RequestedDate,
      PromisedDate = requestLine.PromisedDate,
      ByRequest = new bool?(true)
    };
    if (((PXSelectBase<RQRequisition>) this.Document).Current.CuryID == request.CuryID)
    {
      lineFromRequestLine.CuryEstUnitCost = requestLine.CuryEstUnitCost;
    }
    else
    {
      Decimal curyval;
      PXCurrencyAttribute.CuryConvCury<RQRequisitionLine.curyInfoID>(((PXSelectBase) this.Lines).Cache, (object) requestLine, requestLine.EstUnitCost.GetValueOrDefault(), out curyval, true);
      lineFromRequestLine.CuryEstUnitCost = new Decimal?(curyval);
    }
    return lineFromRequestLine;
  }

  /// <summary>
  /// Updates the existing requisition line costs on insertion of request lines. This is an extension point used in Lexware PriceUnit customization.
  /// </summary>
  /// <param name="requisitionLine">The requisition line.</param>
  /// <param name="selectQty">The selected quantity.</param>
  /// <param name="totalCost">The total cost of requisition and request lines</param>
  protected virtual void UpdateExistingRQRequisitionLineCosts(
    RQRequisitionLine requisitionLine,
    RQRequestLine requestLine,
    Decimal selectQty,
    Decimal? totalCost,
    bool areCurrenciesSame)
  {
    RQRequisitionLine rqRequisitionLine = requisitionLine;
    Decimal? nullable1 = totalCost;
    Decimal? nullable2 = requisitionLine.OrderQty;
    Decimal num = selectQty;
    Decimal? nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num) : new Decimal?();
    Decimal? nullable4;
    if (!(nullable1.HasValue & nullable3.HasValue))
    {
      nullable2 = new Decimal?();
      nullable4 = nullable2;
    }
    else
      nullable4 = new Decimal?(nullable1.GetValueOrDefault() / nullable3.GetValueOrDefault());
    rqRequisitionLine.CuryEstUnitCost = nullable4;
  }

  public RQRequisitionContent UpdateContent(
    RQRequisitionLine rqLine,
    RQRequestLine requestLine,
    Decimal selectQty)
  {
    RQRequisitionContent requisitionContent1 = PXResultset<RQRequisitionContent>.op_Implicit(PXSelectBase<RQRequisitionContent, PXSelect<RQRequisitionContent, Where<RQRequisitionContent.orderNbr, Equal<Required<RQRequisitionContent.orderNbr>>, And<RQRequisitionContent.lineNbr, Equal<Required<RQRequisitionContent.lineNbr>>, And<RQRequisitionContent.reqNbr, Equal<Required<RQRequisitionContent.reqNbr>>, And<RQRequisitionContent.reqLineNbr, Equal<Required<RQRequisitionContent.reqLineNbr>>>>>>>.Config>.Select((PXGraph) this, new object[4]
    {
      (object) requestLine.OrderNbr,
      (object) requestLine.LineNbr,
      (object) rqLine.ReqNbr,
      (object) rqLine.LineNbr
    }));
    if (requisitionContent1 == null)
      requisitionContent1 = ((PXSelectBase<RQRequisitionContent>) this.Contents).Insert(new RQRequisitionContent()
      {
        OrderNbr = requestLine.OrderNbr,
        LineNbr = requestLine.LineNbr,
        ReqNbr = rqLine.ReqNbr,
        ReqLineNbr = rqLine.LineNbr
      });
    RQRequisitionContent copy = (RQRequisitionContent) ((PXSelectBase) this.Contents).Cache.CreateCopy((object) requisitionContent1);
    RQRequisitionContent requisitionContent2 = copy;
    Decimal? itemQty = requisitionContent2.ItemQty;
    Decimal num = selectQty;
    requisitionContent2.ItemQty = itemQty.HasValue ? new Decimal?(itemQty.GetValueOrDefault() + num) : new Decimal?();
    return ((PXSelectBase<RQRequisitionContent>) this.Contents).Update(copy);
  }

  private long? CopyCurrencyInfo(PXGraph graph, long? sourceCuryInfoID, bool isMultiCurrencyGraph)
  {
    if (isMultiCurrencyGraph)
    {
      ICurrencyHelperEx implementation = graph.FindImplementation<ICurrencyHelperEx>();
      return implementation.CloneCurrencyInfo(implementation.GetCurrencyInfo(sourceCuryInfoID)).CuryInfoID;
    }
    PX.Objects.CM.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyinfo).Select(new object[1]
    {
      (object) sourceCuryInfoID
    }));
    currencyInfo.CuryInfoID = new long?();
    graph.Caches[typeof (PX.Objects.CM.CurrencyInfo)].Clear();
    return ((PX.Objects.CM.CurrencyInfo) graph.Caches[typeof (PX.Objects.CM.CurrencyInfo)].Insert((object) currencyInfo)).CuryInfoID;
  }

  private class PO4SO : Dictionary<int?, List<PX.Objects.PO.POLine>>
  {
    public virtual void Add(int? key, PX.Objects.PO.POLine line)
    {
      if (line == null)
        return;
      List<PX.Objects.PO.POLine> poLineList;
      if (!this.TryGetValue(key, out poLineList))
        this[key] = poLineList = new List<PX.Objects.PO.POLine>();
      poLineList.Add(line);
    }
  }

  public class PriceRecalcExt : 
    PX.Objects.RQ.PriceRecalcExt<RQRequisitionEntry, RQRequisition, RQRequisitionLine, RQRequisitionLine.curyEstUnitCost>
  {
    protected override PXSelectBase<RQRequisitionLine> DetailSelect
    {
      get => (PXSelectBase<RQRequisitionLine>) this.Base.Lines;
    }

    protected override PX.Objects.RQ.PriceRecalcExt<RQRequisitionEntry, RQRequisition, RQRequisitionLine, RQRequisitionLine.curyEstUnitCost>.IPricedLine WrapLine(
      RQRequisitionLine line)
    {
      return (PX.Objects.RQ.PriceRecalcExt<RQRequisitionEntry, RQRequisition, RQRequisitionLine, RQRequisitionLine.curyEstUnitCost>.IPricedLine) new RQRequisitionEntry.PriceRecalcExt.PricedLine(line);
    }

    private class PricedLine : 
      PX.Objects.RQ.PriceRecalcExt<RQRequisitionEntry, RQRequisition, RQRequisitionLine, RQRequisitionLine.curyEstUnitCost>.IPricedLine
    {
      private readonly RQRequisitionLine _line;

      public PricedLine(RQRequisitionLine line) => this._line = line;

      public bool? ManualPrice
      {
        get => this._line.ManualPrice;
        set => this._line.ManualPrice = value;
      }

      public int? InventoryID
      {
        get => this._line.InventoryID;
        set => this._line.InventoryID = value;
      }

      public Decimal? CuryUnitPrice
      {
        get => this._line.CuryEstUnitCost;
        set => this._line.CuryEstUnitCost = value;
      }

      public Decimal? CuryExtPrice
      {
        get => this._line.CuryEstExtCost;
        set => this._line.CuryEstExtCost = value;
      }
    }
  }

  /// <exclude />
  public class RQRequisitionEntryAddressLookupExtension : 
    AddressLookupExtension<RQRequisitionEntry, RQRequisition, POShipAddress>
  {
    protected override string AddressView => "Shipping_Address";
  }

  /// <exclude />
  public class RQRequisitionEntryRemitAddressLookupExtension : 
    AddressLookupExtension<RQRequisitionEntry, RQRequisition, PX.Objects.PO.PORemitAddress>
  {
    protected override string AddressView => "Remit_Address";
  }

  public class RQRequisitionEntryShippingAddressCachingHelper : 
    AddressValidationExtension<RQRequisitionEntry, POShipAddress>
  {
    protected override IEnumerable<PXSelectBase<POShipAddress>> AddressSelects()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      RQRequisitionEntry.RQRequisitionEntryShippingAddressCachingHelper addressCachingHelper = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (PXSelectBase<POShipAddress>) addressCachingHelper.Base.Shipping_Address;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }
  }

  public class RQRequisitionEntryRemitAddressCachingHelper : 
    AddressValidationExtension<RQRequisitionEntry, PX.Objects.PO.PORemitAddress>
  {
    protected override IEnumerable<PXSelectBase<PX.Objects.PO.PORemitAddress>> AddressSelects()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      RQRequisitionEntry.RQRequisitionEntryRemitAddressCachingHelper addressCachingHelper = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (PXSelectBase<PX.Objects.PO.PORemitAddress>) addressCachingHelper.Base.Remit_Address;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }
  }

  public class RQRequisitionEntry_ActivityDetailsExt : 
    ActivityDetailsExt<RQRequisitionEntry, RQRequisition, RQRequisition.noteID>
  {
    public override System.Type GetBAccountIDCommand()
    {
      return typeof (Select<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<RQBiddingVendor.vendorID>>>>);
    }

    public override System.Type GetEmailMessageTarget()
    {
      return typeof (Select2<VendorContact, InnerJoin<PX.Objects.AP.Vendor, On<VendorContact.contactID, Equal<PX.Objects.AP.Vendor.defContactID>>>, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<RQBiddingVendor.vendorID>>>>);
    }
  }
}
