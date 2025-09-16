// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequestEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.DependencyInjection;
using PX.Data.WorkflowAPI;
using PX.LicensePolicy;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.Extensions;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.IN;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.RQ;

public class RQRequestEntry : PXGraph<RQRequestEntry, RQRequest>, IGraphWithInitialization
{
  [PXViewName("Request")]
  public PXSelectJoin<RQRequest, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<RQRequest.employeeID>>>, Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>> Document;
  public PXSelect<RQRequest, Where<RQRequest.orderNbr, Equal<Current<RQRequest.orderNbr>>>> CurrentDocument;
  public PXSelect<PX.Objects.IN.InventoryItem> invItems;
  public PXSelect<RQRequestLine, Where<RQRequestLine.orderNbr, Equal<Optional<RQRequest.orderNbr>>>> Lines;
  [PXViewName("Ship Address")]
  public PXSelect<POShipAddress, Where<POShipAddress.addressID, Equal<Current<RQRequest.shipAddressID>>>> Shipping_Address;
  [PXViewName("Ship Contact")]
  public PXSelect<POShipContact, Where<POShipContact.contactID, Equal<Current<RQRequest.shipContactID>>>> Shipping_Contact;
  [PXViewName("Remit Address")]
  public PXSelect<PX.Objects.PO.PORemitAddress, Where<PX.Objects.PO.PORemitAddress.addressID, Equal<Current<RQRequest.remitAddressID>>>> Remit_Address;
  [PXViewName("Remit Contact")]
  public PXSelect<PX.Objects.PO.PORemitContact, Where<PX.Objects.PO.PORemitContact.contactID, Equal<Current<RQRequest.remitContactID>>>> Remit_Contact;
  public PXSelectJoin<RQRequisitionContent, InnerJoin<RQRequisitionLineReceived, On<RQRequisitionLineReceived.reqNbr, Equal<RQRequisitionContent.reqNbr>, And<RQRequisitionLineReceived.lineNbr, Equal<RQRequisitionContent.reqLineNbr>>>, InnerJoin<RQRequisition, On<RQRequisition.reqNbr, Equal<RQRequisitionContent.reqNbr>>>>, Where<RQRequisitionContent.orderNbr, Equal<Optional<RQRequestLine.orderNbr>>, And<RQRequisitionContent.lineNbr, Equal<Optional<RQRequestLine.lineNbr>>>>> Contents;
  public PXSetup<RQSetup> Setup;
  public PXSetup<PX.Objects.GL.GLSetup> GLSetup;
  public PXSetup<Company> company;
  public PXSetup<MasterFinPeriod, Where<MasterFinPeriod.finPeriodID, Equal<Current<RQRequest.finPeriodID>>>> finperiod;
  public PXSelect<RQSetupApproval, Where<RQSetupApproval.type, Equal<RQType.requestItem>>> SetupApproval;
  [PXViewName("Approval")]
  public EPApprovalAutomation<RQRequest, RQRequest.approved, RQRequest.rejected, RQRequest.hold, RQSetupApproval> Approval;
  public PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Optional<RQRequest.vendorID>>>> bAccount;
  public PXSetup<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Optional<RQRequest.vendorID>>>> vendor;
  [PXViewName("Employee")]
  public PXSetup<EPEmployee, Where<EPEmployee.bAccountID, Equal<Optional<RQRequest.employeeID>>>> employee;
  [PXViewName("Department")]
  public PXSetup<EPDepartment, Where<EPDepartment.departmentID, Equal<Optional<RQRequest.departmentID>>>> depa;
  public PXSetup<RQRequestClass, Where<RQRequestClass.reqClassID, Equal<Optional<RQRequest.reqClassID>>>> reqclass;
  public PXSelect<RQBudget> Budget;
  public CMSetupSelect CMSetup;
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<RQRequest.curyInfoID>>>> currencyinfo;
  public PXSetup<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Optional<RQRequest.employeeID>>>> customer;
  public ToggleCurrency<RQRequest> CurrencyView;
  public PXAction<RQRequest> validateAddresses;
  public PXWorkflowEventHandler<RQRequest> OnOpenOrderQtyExhausted;
  public PXWorkflowEventHandler<RQRequest> OnOpenOrderQtyIncreased;
  public PXInitializeState<RQRequest> initializeState;
  public PXAction<RQRequest> cancelRequest;
  public PXAction<RQRequest> putOnHold;
  public PXAction<RQRequest> releaseFromHold;
  public PXAction<RQRequest> action;
  public PXAction<RQRequest> report;
  public PXAction<RQRequest> requestForm;
  public PXAction<RQRequest> ViewDetails;
  public PXAction<RQRequest> ViewRequisition;
  public PXAction<RQRequest> Assign;

  public virtual IEnumerable budget() => (IEnumerable) this.GetBudget();

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
  {
    RQRequestEntry rqRequestEntry = this;
    foreach (RQRequest rqRequest in adapter.Get<RQRequest>())
    {
      if (rqRequest != null)
        ((PXGraph) rqRequestEntry).FindAllImplementations<IAddressValidationHelper>().ValidateAddresses();
      yield return (object) rqRequest;
    }
  }

  [PXMergeAttributes]
  [InterBranchRestrictor(typeof (Where<SameOrganizationBranch<INSite.branchID, Current<RQRequest.branchID>>>))]
  protected virtual void POSiteStatusFilter_SiteID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (RQRequest.orderDate))]
  [PXMergeAttributes]
  protected virtual void EPApproval_DocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (RQRequest.employeeID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_BAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (RQRequest.ownerID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_DocumentOwnerID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (RQRequest.description))]
  [PXMergeAttributes]
  protected virtual void EPApproval_Descr_CacheAttached(PXCache sender)
  {
  }

  [CurrencyInfo(typeof (RQRequest.curyInfoID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (RQRequest.curyEstExtCostTotal))]
  [PXMergeAttributes]
  protected virtual void EPApproval_CuryTotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (RQRequest.estExtCostTotal))]
  [PXMergeAttributes]
  protected virtual void EPApproval_TotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXRestrictorAttribute), "ShowWarning", true)]
  protected virtual void RQRequestLine_ExpenseAcctID_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXRestrictorAttribute), "ShowWarning", true)]
  protected virtual void RQRequestLine_ExpenseSubID_CacheAttached(PXCache sender)
  {
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXMergeAttributes]
  protected virtual void RQRequisitionContent_ItemQty_CacheAttached(PXCache sender)
  {
  }

  public RQRequestEntry()
  {
    ((PXSelectBase) this.Contents).Cache.AllowInsert = false;
    ((PXSelectBase) this.Contents).Cache.AllowUpdate = false;
    ((PXSelectBase) this.Contents).Cache.AllowDelete = false;
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
    ((PXGraph) this).Views.Caches.Remove(typeof (RQBudget));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<PX.Objects.IN.InventoryItem.stkItem>(RQRequestEntry.\u003C\u003Ec.\u003C\u003E9__39_0 ?? (RQRequestEntry.\u003C\u003Ec.\u003C\u003E9__39_0 = new PXFieldDefaulting((object) RQRequestEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__39_0))));
  }

  [InjectDependency]
  protected ILicenseLimitsService _licenseLimits { get; set; }

  void IGraphWithInitialization.Initialize()
  {
    if (this._licenseLimits == null)
      return;
    ((PXGraph) this).OnBeforeCommit += this._licenseLimits.GetCheckerDelegate<RQRequest>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (RQRequestLine), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[1]
      {
        (PXDataFieldValue) new PXDataFieldValue<RQRequestLine.orderNbr>((object) ((PXSelectBase<RQRequest>) ((RQRequestEntry) graph).Document).Current?.OrderNbr)
      }))
    });
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable CancelRequest(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold")]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Remove Hold")]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter)
  {
    foreach (RQRequest rqRequest in adapter.Get<RQRequest>())
    {
      ((PXSelectBase<RQRequest>) this.Document).Current = rqRequest;
      if (!rqRequest.Hold.GetValueOrDefault() && !rqRequest.Approved.GetValueOrDefault())
      {
        rqRequest.CheckBudget = new bool?(false);
        bool? nullable = rqRequest.BudgetValidation;
        if (nullable.GetValueOrDefault())
        {
          foreach (PXResult<RQBudget> pxResult in ((PXSelectBase<RQBudget>) this.Budget).Select(Array.Empty<object>()))
          {
            RQBudget rqBudget = PXResult<RQBudget>.op_Implicit(pxResult);
            Decimal? requestAmt = rqBudget.RequestAmt;
            Decimal? budgetAmt = rqBudget.BudgetAmt;
            if (requestAmt.GetValueOrDefault() > budgetAmt.GetValueOrDefault() & requestAmt.HasValue & budgetAmt.HasValue)
            {
              rqRequest.CheckBudget = new bool?(true);
              break;
            }
          }
        }
        nullable = rqRequest.CheckBudget;
        if (nullable.GetValueOrDefault())
        {
          RQRequestClass rqRequestClass = PXResultset<RQRequestClass>.op_Implicit(((PXSelectBase<RQRequestClass>) this.reqclass).SelectWindowed(0, 1, new object[1]
          {
            (object) rqRequest.ReqClassID
          }));
          if (rqRequestClass != null && rqRequestClass.BudgetValidation.GetValueOrDefault() == 2)
            throw new PXRowPersistedException(typeof (RQRequest).Name, (object) rqRequest, "Check for budget exceed in request item.");
        }
        if (((PXSelectBase<RQSetup>) this.Setup).Current.RequestAssignmentMapID.HasValue)
        {
          PXGraph.CreateInstance<EPAssignmentProcessor<RQRequest>>().Assign(rqRequest, ((PXSelectBase<RQSetup>) this.Setup).Current.RequestAssignmentMapID);
          rqRequest.WorkgroupID = rqRequest.ApprovalWorkgroupID;
          rqRequest.OwnerID = rqRequest.ApprovalOwnerID;
        }
      }
      yield return (object) PXResultset<RQRequest>.op_Implicit(((PXSelectBase<RQRequest>) this.Document).Search<RQRequest.orderNbr>((object) rqRequest.OrderNbr, Array.Empty<object>()));
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
    List<RQRequest> rqRequestList = new List<RQRequest>();
    if (actionName != null)
    {
      PXAction action = ((PXGraph) this).Actions[actionName];
      if (action != null)
        rqRequestList.AddRange(action.Press(adapter).Cast<RQRequest>());
    }
    else
      rqRequestList.AddRange(adapter.Get<RQRequest>());
    if (refresh)
    {
      foreach (RQRequest rqRequest in rqRequestList)
        ((PXSelectBase<RQRequest>) this.Document).Search<RQRequest.orderNbr>((object) rqRequest.OrderNbr, Array.Empty<object>());
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
    return (IEnumerable) rqRequestList;
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Report(PXAdapter adapter, [PXString(8, InputMask = "CC.CC.CC.CC")] string reportID)
  {
    List<RQRequest> list = adapter.Get<RQRequest>().ToList<RQRequest>();
    if (!string.IsNullOrEmpty(reportID))
    {
      ((PXAction) this.Save).Press();
      int num = 0;
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (RQRequest rqRequest in list)
        dictionary["RQRequest.OrderNbr" + num.ToString()] = rqRequest.OrderNbr;
      throw new PXReportRequiredException(dictionary, reportID, (PXBaseRedirectException.WindowMode) 2, "Report " + reportID, (CurrentLocalization) null);
    }
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton(CommitChanges = true)]
  protected virtual IEnumerable RequestForm(PXAdapter adapter) => this.Report(adapter, "RQ641000");

  [PXButton(ImageKey = "DataEntry")]
  [PXUIField]
  public virtual IEnumerable viewDetails(PXAdapter adapter)
  {
    ((PXSelectBase<RQRequisitionContent>) this.Contents).AskExt();
    ((PXSelectBase<RQRequisitionContent>) this.Contents).ClearDialog();
    yield break;
  }

  [PXLookupButton]
  [PXUIField]
  public virtual IEnumerable viewRequisition(PXAdapter adapter)
  {
    // ISSUE: reference to a compiler-generated field
    int num = this.\u003C\u003E1__state;
    RQRequestEntry rqRequestEntry = this;
    if (num != 0)
      return false;
    // ISSUE: reference to a compiler-generated field
    this.\u003C\u003E1__state = -1;
    if (((PXSelectBase<RQRequisitionContent>) rqRequestEntry.Contents).Current != null)
      new EntityHelper((PXGraph) rqRequestEntry).NavigateToRow(typeof (RQRequisition).FullName, new object[1]
      {
        (object) ((PXSelectBase<RQRequisitionContent>) rqRequestEntry.Contents).Current.ReqNbr
      }, (PXRedirectHelper.WindowMode) 3);
    return false;
  }

  [PXButton(ImageKey = "DataEntry")]
  [PXUIField(DisplayName = "Assign", Visible = false)]
  public virtual IEnumerable assign(PXAdapter adapter)
  {
    foreach (RQRequest rqRequest in adapter.Get<RQRequest>())
    {
      if (((PXSelectBase<RQSetup>) this.Setup).Current.RequestAssignmentMapID.HasValue)
      {
        new EPAssignmentProcessor<RQRequest>().Assign(rqRequest, ((PXSelectBase<RQSetup>) this.Setup).Current.RequestAssignmentMapID);
        rqRequest.WorkgroupID = rqRequest.ApprovalWorkgroupID;
        rqRequest.OwnerID = rqRequest.ApprovalOwnerID;
      }
      yield return (object) rqRequest;
    }
  }

  protected virtual IEnumerable<RQBudget> GetBudget()
  {
    RQRequestEntry rqRequestEntry = this;
    ((PXSelectBase) rqRequestEntry.Budget).Cache.Clear();
    DateTime? nullable1 = new DateTime?();
    string startPeriod = string.Empty;
    string budgetPeriod = (string) null;
    if (((PXSelectBase<MasterFinPeriod>) rqRequestEntry.finperiod).Current != null)
    {
      ((PXSelectBase) rqRequestEntry.Budget).Cache.ForceExceptionHandling = true;
      switch (((PXSelectBase<RQSetup>) rqRequestEntry.Setup).Current.BudgetCalculation)
      {
        case "Y":
          DateTime dateTime1 = new DateTime(((PXSelectBase<MasterFinPeriod>) rqRequestEntry.finperiod).Current.EndDateUI.Value.Year, 1, 1);
          budgetPeriod = ((PXSelectBase<MasterFinPeriod>) rqRequestEntry.finperiod).Current.FinPeriodID;
          break;
        case "P":
          DateTime? startDate = ((PXSelectBase<MasterFinPeriod>) rqRequestEntry.finperiod).Current.StartDate;
          DateTime? endDateUi = ((PXSelectBase<MasterFinPeriod>) rqRequestEntry.finperiod).Current.EndDateUI;
          startPeriod = ((PXSelectBase<MasterFinPeriod>) rqRequestEntry.finperiod).Current.FinPeriodID;
          budgetPeriod = ((PXSelectBase<MasterFinPeriod>) rqRequestEntry.finperiod).Current.FinPeriodID;
          break;
        case "A":
          int year = ((PXSelectBase<MasterFinPeriod>) rqRequestEntry.finperiod).Current.StartDate.Value.Year;
          DateTime dateTime2 = new DateTime(year, 1, 1);
          nullable1 = new DateTime?(new DateTime(year, 12, 31 /*0x1F*/));
          MasterFinPeriod masterFinPeriod1 = PXResultset<MasterFinPeriod>.op_Implicit(PXSelectBase<MasterFinPeriod, PXSelect<MasterFinPeriod, Where<MasterFinPeriod.finYear, Equal<Required<MasterFinPeriod.finYear>>>, OrderBy<Desc<MasterFinPeriod.periodNbr>>>.Config>.SelectWindowed((PXGraph) rqRequestEntry, 0, 1, new object[1]
          {
            (object) ((PXSelectBase<MasterFinPeriod>) rqRequestEntry.finperiod).Current.FinYear
          }));
          MasterFinPeriod masterFinPeriod2 = PXResultset<MasterFinPeriod>.op_Implicit(PXSelectBase<MasterFinPeriod, PXSelect<MasterFinPeriod, Where<MasterFinPeriod.finYear, Equal<Required<MasterFinPeriod.finYear>>>, OrderBy<Asc<MasterFinPeriod.periodNbr>>>.Config>.SelectWindowed((PXGraph) rqRequestEntry, 0, 1, new object[1]
          {
            (object) ((PXSelectBase<MasterFinPeriod>) rqRequestEntry.finperiod).Current.FinYear
          }));
          if (masterFinPeriod2 != null)
            startPeriod = masterFinPeriod2.FinPeriodID;
          if (masterFinPeriod1 != null)
          {
            budgetPeriod = masterFinPeriod1.FinPeriodID;
            break;
          }
          break;
      }
      bool isoverbudget = false;
      foreach (PXResult<RQRequestLine> pxResult in ((PXSelectBase<RQRequestLine>) rqRequestEntry.Lines).Select(Array.Empty<object>()))
      {
        RQRequestLine line = PXResult<RQRequestLine>.op_Implicit(pxResult);
        int? nullable2 = line.ExpenseAcctID;
        if (nullable2.HasValue)
        {
          nullable2 = line.ExpenseSubID;
          if (nullable2.HasValue)
          {
            RQBudget item = new RQBudget();
            item.ExpenseAcctID = line.ExpenseAcctID;
            item.ExpenseSubID = line.ExpenseSubID;
            item = ((PXSelectBase<RQBudget>) rqRequestEntry.Budget).Locate(item);
            PX.Objects.GL.Account account = (PX.Objects.GL.Account) PXSelectorAttribute.Select<RQRequestLine.expenseAcctID>(((PXSelectBase) rqRequestEntry.Lines).Cache, (object) line);
            Decimal? nullable3;
            Decimal? nullable4;
            if (item == null)
            {
              item = new RQBudget();
              item.ExpenseAcctID = line.ExpenseAcctID;
              item.ExpenseSubID = line.ExpenseSubID;
              item.CuryID = account.CuryID ?? ((PXSelectBase<Company>) rqRequestEntry.company).Current.BaseCuryID;
              item.CuryInfoID = ((PXSelectBase<RQRequest>) rqRequestEntry.Document).Current.CuryInfoID;
              item.BudgetAmt = new Decimal?(0M);
              item.UsageAmt = new Decimal?(0M);
              item.DocRequestAmt = new Decimal?(0M);
              item.RequestAmt = new Decimal?(0M);
              item.AprovedAmt = new Decimal?(0M);
              item.UnaprovedAmt = new Decimal?(0M);
              RQBudget rqBudget1 = PXResultset<RQBudget>.op_Implicit(PXSelectBase<RQBudget, PXSelectJoinGroupBy<RQBudget, InnerJoin<MasterFinPeriod, On<MasterFinPeriod.finPeriodID, Equal<RQBudget.finPeriodID>>>, Where<RQBudget.expenseAcctID, Equal<Required<RQBudget.expenseAcctID>>, And<RQBudget.expenseSubID, Equal<Required<RQBudget.expenseSubID>>, And<MasterFinPeriod.finYear, Equal<Required<MasterFinPeriod.finYear>>, And<MasterFinPeriod.finPeriodID, Between<Required<MasterFinPeriod.finPeriodID>, Required<MasterFinPeriod.finPeriodID>>, And<RQBudget.orderNbr, NotEqual<Required<RQBudget.orderNbr>>>>>>>, Aggregate<GroupBy<RQBudget.expenseAcctID, GroupBy<RQBudget.expenseSubID, Sum<RQBudget.requestAmt, Sum<RQBudget.curyRequestAmt, Sum<RQBudget.aprovedAmt, Sum<RQBudget.curyAprovedAmt, Sum<RQBudget.unaprovedAmt, Sum<RQBudget.curyUnaprovedAmt>>>>>>>>>>.Config>.SelectWindowed((PXGraph) rqRequestEntry, 0, 1, new object[6]
              {
                (object) line.ExpenseAcctID,
                (object) line.ExpenseSubID,
                (object) ((PXSelectBase<MasterFinPeriod>) rqRequestEntry.finperiod).Current.FinYear,
                (object) startPeriod,
                (object) budgetPeriod,
                (object) ((PXSelectBase<RQRequest>) rqRequestEntry.Document).Current.OrderNbr
              }));
              if (rqBudget1 != null)
              {
                RQBudget rqBudget2 = item;
                nullable3 = rqBudget2.RequestAmt;
                nullable4 = account.CuryID == null ? rqBudget1.RequestAmt : rqBudget1.CuryRequestAmt;
                Decimal valueOrDefault1 = nullable4.GetValueOrDefault();
                Decimal? nullable5;
                if (!nullable3.HasValue)
                {
                  nullable4 = new Decimal?();
                  nullable5 = nullable4;
                }
                else
                  nullable5 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault1);
                rqBudget2.RequestAmt = nullable5;
                RQBudget rqBudget3 = item;
                nullable3 = rqBudget3.AprovedAmt;
                nullable4 = account.CuryID == null ? rqBudget1.AprovedAmt : rqBudget1.CuryAprovedAmt;
                Decimal valueOrDefault2 = nullable4.GetValueOrDefault();
                Decimal? nullable6;
                if (!nullable3.HasValue)
                {
                  nullable4 = new Decimal?();
                  nullable6 = nullable4;
                }
                else
                  nullable6 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault2);
                rqBudget3.AprovedAmt = nullable6;
                RQBudget rqBudget4 = item;
                nullable3 = rqBudget4.UnaprovedAmt;
                nullable4 = account.CuryID == null ? rqBudget1.UnaprovedAmt : rqBudget1.CuryUnaprovedAmt;
                Decimal valueOrDefault3 = nullable4.GetValueOrDefault();
                Decimal? nullable7;
                if (!nullable3.HasValue)
                {
                  nullable4 = new Decimal?();
                  nullable7 = nullable4;
                }
                else
                  nullable7 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault3);
                rqBudget4.UnaprovedAmt = nullable7;
              }
              GLBudgetLineDetail budgetLineDetail = PXResultset<GLBudgetLineDetail>.op_Implicit(PXSelectBase<GLBudgetLineDetail, PXSelectGroupBy<GLBudgetLineDetail, Where<GLBudgetLineDetail.ledgerID, Equal<Required<GLBudgetLineDetail.ledgerID>>, And<GLBudgetLineDetail.finYear, Equal<Required<GLBudgetLineDetail.finYear>>, And<GLBudgetLineDetail.accountID, Equal<Required<GLBudgetLineDetail.accountID>>, And<GLBudgetLineDetail.subID, Equal<Required<GLBudgetLineDetail.subID>>, And<GLBudgetLineDetail.finPeriodID, Between<Required<GLBudgetLineDetail.finPeriodID>, Required<GLBudgetLineDetail.finPeriodID>>>>>>>, Aggregate<Sum<GLBudgetLineDetail.amount, Sum<GLBudgetLineDetail.releasedAmount>>>>.Config>.SelectWindowed((PXGraph) rqRequestEntry, 0, 1, new object[6]
              {
                (object) ((PXSelectBase<RQSetup>) rqRequestEntry.Setup).Current.BudgetLedgerId,
                (object) ((PXSelectBase<MasterFinPeriod>) rqRequestEntry.finperiod).Current.FinYear,
                (object) line.ExpenseAcctID,
                (object) line.ExpenseSubID,
                (object) startPeriod,
                (object) budgetPeriod
              }));
              if (budgetLineDetail != null)
              {
                nullable3 = budgetLineDetail.ReleasedAmount;
                if (nullable3.HasValue)
                  item.BudgetAmt = budgetLineDetail.ReleasedAmount;
              }
              GLHistory glHistory = PXResultset<GLHistory>.op_Implicit(PXSelectBase<GLHistory, PXSelectJoin<GLHistory, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.ledgerID, Equal<GLHistory.ledgerID>, And<PX.Objects.GL.Branch.branchID, Equal<GLHistory.branchID>>>>, Where<GLHistory.branchID, Equal<Required<GLHistory.branchID>>, And<GLHistory.accountID, Equal<Required<GLHistory.accountID>>, And<GLHistory.subID, Equal<Required<GLHistory.subID>>, And<GLHistory.finPeriodID, LessEqual<Required<GLHistory.finPeriodID>>>>>>, OrderBy<Desc<GLHistory.finPeriodID>>>.Config>.SelectWindowed((PXGraph) rqRequestEntry, 0, 1, new object[4]
              {
                (object) line.BranchID,
                (object) line.ExpenseAcctID,
                (object) line.ExpenseSubID,
                (object) budgetPeriod
              }));
              if (glHistory != null && (account.Type == "A" || account.Type == "L" || FinPeriodUtils.FiscalYear(glHistory.FinPeriodID) == FinPeriodUtils.FiscalYear(budgetPeriod)))
                item.UsageAmt = account.CuryID == null ? glHistory.YtdBalance : glHistory.CuryYtdBalance;
              item = ((PXSelectBase<RQBudget>) rqRequestEntry.Budget).Insert(item);
              yield return item;
            }
            Decimal? nullable8 = account.CuryID == null ? line.EstExtCost : line.CuryEstExtCost;
            RQBudget rqBudget5 = item;
            nullable3 = rqBudget5.RequestAmt;
            nullable4 = nullable8;
            rqBudget5.RequestAmt = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            RQBudget rqBudget6 = item;
            nullable4 = rqBudget6.DocRequestAmt;
            nullable3 = nullable8;
            rqBudget6.DocRequestAmt = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
            if (((PXSelectBase<RQRequest>) rqRequestEntry.Document).Current.Approved.GetValueOrDefault())
            {
              RQBudget rqBudget7 = item;
              nullable3 = rqBudget7.AprovedAmt;
              nullable4 = nullable8;
              rqBudget7.AprovedAmt = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
            }
            else
            {
              RQBudget rqBudget8 = item;
              nullable4 = rqBudget8.UnaprovedAmt;
              nullable3 = nullable8;
              rqBudget8.UnaprovedAmt = nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
            }
            nullable3 = item.RequestAmt;
            nullable4 = item.BudgetAmt;
            if (nullable3.GetValueOrDefault() > nullable4.GetValueOrDefault() & nullable3.HasValue & nullable4.HasValue)
            {
              isoverbudget = true;
              PXCache cache = ((PXSelectBase) rqRequestEntry.Budget).Cache;
              RQBudget rqBudget9 = item;
              // ISSUE: variable of a boxed type
              __Boxed<Decimal?> budgetAmt = (ValueType) item.BudgetAmt;
              nullable2 = ((PXSelectBase<RQRequestClass>) rqRequestEntry.reqclass).Current.BudgetValidation;
              PXSetPropertyException propertyException = new PXSetPropertyException("The request amount exceeds the budget amount.", (PXErrorLevel) (nullable2.GetValueOrDefault() == 1 ? 2 : 4));
              cache.RaiseExceptionHandling<RQBudget.budgetAmt>((object) rqBudget9, (object) budgetAmt, (Exception) propertyException);
            }
            item = (RQBudget) null;
            account = (PX.Objects.GL.Account) null;
            line = (RQRequestLine) null;
          }
        }
      }
      ((PXSelectBase) rqRequestEntry.Document).Cache.SetValueExt<RQRequest.isOverbudget>((object) ((PXSelectBase<RQRequest>) rqRequestEntry.Document).Current, (object) isoverbudget);
      ((PXSelectBase) rqRequestEntry.Budget).Cache.IsDirty = false;
    }
  }

  protected virtual void CurrencyInfo_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    if (((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current != null && !string.IsNullOrEmpty(((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.CuryID))
    {
      e.NewValue = (object) ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.CuryID;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      if (((PXSelectBase<EPEmployee>) this.employee).Current == null || string.IsNullOrEmpty(((PXSelectBase<EPEmployee>) this.employee).Current.CuryID))
        return;
      e.NewValue = (object) ((PXSelectBase<EPEmployee>) this.employee).Current.CuryID;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void CurrencyInfo_CuryRateTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    if (((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current != null && !string.IsNullOrEmpty(((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.CuryRateTypeID))
    {
      e.NewValue = (object) ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.CuryRateTypeID;
      ((CancelEventArgs) e).Cancel = true;
    }
    else
    {
      if (((PXSelectBase<EPEmployee>) this.employee).Current == null || string.IsNullOrEmpty(((PXSelectBase<EPEmployee>) this.employee).Current.CuryRateTypeID))
        return;
      e.NewValue = (object) ((PXSelectBase<EPEmployee>) this.employee).Current.CuryRateTypeID;
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void CurrencyInfo_CuryEffDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase) this.Document).Cache.Current == null)
      return;
    e.NewValue = (object) ((RQRequest) ((PXSelectBase) this.Document).Cache.Current).OrderDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CM.CurrencyInfo row))
      return;
    bool flag = row.AllowUpdate(((PXSelectBase) this.Lines).Cache);
    bool? allowOverrideRate;
    if (((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current != null)
    {
      allowOverrideRate = ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.AllowOverrideRate;
      if (!allowOverrideRate.Value)
        flag = false;
    }
    if (((PXSelectBase<EPEmployee>) this.employee).Current != null)
    {
      allowOverrideRate = ((PXSelectBase<EPEmployee>) this.employee).Current.AllowOverrideRate;
      if (!allowOverrideRate.Value)
        flag = false;
    }
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyRateTypeID>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyEffDate>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.sampleCuryRate>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.sampleRecipRate>(sender, (object) row, flag);
  }

  protected virtual void RQRequest_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (e.Row == null)
      return;
    using (new ReadOnlyScope(new PXCache[2]
    {
      ((PXSelectBase) this.Shipping_Address).Cache,
      ((PXSelectBase) this.Shipping_Contact).Cache
    }))
    {
      SharedRecordAttribute.DefaultRecord<RQRequest.shipAddressID>(sender, e.Row);
      SharedRecordAttribute.DefaultRecord<RQRequest.shipContactID>(sender, e.Row);
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>() && (e.ExternalCall || sender.GetValuePending<RQRequest.curyID>(e.Row) == null))
    {
      PX.Objects.CM.CurrencyInfo currencyInfo = CurrencyInfoAttribute.SetDefaults<RQRequest.curyInfoID>(sender, e.Row);
      string error = PXUIFieldAttribute.GetError<PX.Objects.CM.CurrencyInfo.curyEffDate>(((PXSelectBase) this.currencyinfo).Cache, (object) currencyInfo);
      if (!string.IsNullOrEmpty(error))
        sender.RaiseExceptionHandling<RQRequest.orderDate>(e.Row, (object) ((RQRequest) e.Row).OrderDate, (Exception) new PXSetPropertyException(error, (PXErrorLevel) 2));
      if (currencyInfo != null)
        ((RQRequest) e.Row).CuryID = currencyInfo.CuryID;
    }
    sender.IsDirty = false;
    ((PXSelectBase) this.Remit_Address).Cache.IsDirty = false;
    ((PXSelectBase) this.Remit_Contact).Cache.IsDirty = false;
  }

  protected virtual void RQRequest_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    RQRequest row = (RQRequest) e.Row;
    if (row == null)
      return;
    RQRequestClass current1 = ((PXSelectBase<RQRequestClass>) this.reqclass).Current;
    bool flag1 = false;
    bool? nullable1;
    if (current1 != null)
    {
      bool? nullable2 = current1.CustomerRequest;
      if (nullable2.GetValueOrDefault())
        ((PXSelectBase<EPEmployee>) this.employee).Current = (EPEmployee) null;
      RQRequest rqRequest1 = row;
      RQRequest rqRequest2 = row;
      RQRequest rqRequest3 = row;
      nullable2 = new bool?(current1.VendorNotRequest.GetValueOrDefault());
      bool? nullable3 = new bool?(current1.CustomerRequest.GetValueOrDefault());
      ref bool? local = ref nullable1;
      int? budgetValidation1 = current1.BudgetValidation;
      int num1 = 0;
      int num2 = budgetValidation1.GetValueOrDefault() > num1 & budgetValidation1.HasValue ? 1 : 0;
      local = new bool?(num2 != 0);
      bool? nullable4 = nullable2;
      rqRequest1.VendorHidden = nullable4;
      rqRequest2.CustomerRequest = nullable3;
      rqRequest3.BudgetValidation = nullable1;
      nullable1 = current1.VendorMultiply;
      bool valueOrDefault = nullable1.GetValueOrDefault();
      nullable1 = row.VendorHidden;
      bool flag2 = !nullable1.GetValueOrDefault() & valueOrDefault;
      nullable1 = current1.HideInventoryID;
      flag1 = nullable1.GetValueOrDefault();
      PXUIFieldAttribute.SetVisible<RQRequestLine.vendorID>(((PXSelectBase) this.Lines).Cache, (object) null, flag2);
      PXUIFieldAttribute.SetVisible<RQRequestLine.vendorLocationID>(((PXSelectBase) this.Lines).Cache, (object) null, flag2);
      PXUIFieldAttribute.SetVisible<RQRequestLine.vendorRefNbr>(((PXSelectBase) this.Lines).Cache, (object) null, flag2);
      PXUIFieldAttribute.SetVisible<RQRequestLine.vendorName>(((PXSelectBase) this.Lines).Cache, (object) null, flag2);
      PXUIFieldAttribute.SetVisible<RQRequestLine.vendorDescription>(((PXSelectBase) this.Lines).Cache, (object) null, flag2);
      PXUIFieldAttribute.SetVisible<RQRequestLine.alternateID>(((PXSelectBase) this.Lines).Cache, (object) null, flag2);
    }
    nullable1 = row.CustomerRequest;
    bool flag3 = !nullable1.GetValueOrDefault();
    int? nullable5;
    int num3;
    if (current1 == null)
    {
      num3 = 0;
    }
    else
    {
      nullable5 = current1.BudgetValidation;
      int num4 = 0;
      num3 = nullable5.GetValueOrDefault() > num4 & nullable5.HasValue ? 1 : 0;
    }
    bool budgetValidation = num3 != 0;
    if (row.Status == "N" && this.Approval.ValidateAccess(row.WorkgroupID, row.OwnerID))
      flag1 = false;
    PX.Objects.CM.CMSetup current2 = ((PXSelectBase<PX.Objects.CM.CMSetup>) this.CMSetup).Current;
    PXUIFieldAttribute.SetVisible<RQRequest.departmentID>(sender, (object) row, flag3);
    PXUIFieldAttribute.SetVisible<RQRequest.curyID>(sender, (object) row, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
    bool flag4 = true;
    if (((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current != null)
    {
      nullable1 = ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current.AllowOverrideCury;
      if (!nullable1.GetValueOrDefault())
        flag4 = false;
    }
    if (((PXSelectBase<EPEmployee>) this.employee).Current != null)
    {
      nullable1 = ((PXSelectBase<EPEmployee>) this.employee).Current.AllowOverrideCury;
      if (!nullable1.GetValueOrDefault())
        flag4 = false;
    }
    if (flag4 && current1 != null)
    {
      nullable5 = current1.BudgetValidation;
      int num5 = 0;
      if (nullable5.GetValueOrDefault() > num5 & nullable5.HasValue && ((PXSelectBase<RQRequestLine>) this.Lines).Select(Array.Empty<object>()).Count > 0)
        flag4 = false;
    }
    PXUIFieldAttribute.SetEnabled<RQRequest.curyID>(sender, (object) row, flag4);
    PXCache pxCache1 = sender;
    nullable1 = row.VendorHidden;
    int num6 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<RQRequest.vendorID>(pxCache1, (object) null, num6 != 0);
    PXCache pxCache2 = sender;
    nullable1 = row.VendorHidden;
    int num7 = !nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<RQRequest.vendorLocationID>(pxCache2, (object) null, num7 != 0);
    PXUIFieldAttribute.SetVisible<RQRequestLine.expenseAcctID>(((PXSelectBase) this.Lines).Cache, (object) null, budgetValidation);
    PXUIFieldAttribute.SetVisible<RQRequestLine.expenseSubID>(((PXSelectBase) this.Lines).Cache, (object) null, budgetValidation);
    PXUIFieldAttribute.SetVisible<RQRequestLine.inventoryID>(((PXSelectBase) this.Lines).Cache, (object) null, !flag1);
    PXUIFieldAttribute.SetVisible<RQRequestLine.subItemID>(((PXSelectBase) this.Lines).Cache, (object) null, !flag1);
    PXPersistingCheck pxPersistingCheck = budgetValidation ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2;
    OpenPeriodAttribute.SetValidatePeriod<RQRequest.finPeriodID>(sender, (object) null, budgetValidation ? PeriodValidation.DefaultUpdate : PeriodValidation.Nothing);
    PXDefaultAttribute.SetPersistingCheck<RQRequest.departmentID>(sender, (object) row, pxPersistingCheck);
    PXDefaultAttribute.SetPersistingCheck<RQRequest.finPeriodID>(sender, (object) null, pxPersistingCheck);
    PXDefaultAttribute.SetPersistingCheck<RQRequestLine.expenseAcctID>(((PXSelectBase) this.Lines).Cache, (object) null, pxPersistingCheck);
    PXDefaultAttribute.SetPersistingCheck<RQRequestLine.expenseSubID>(((PXSelectBase) this.Lines).Cache, (object) null, pxPersistingCheck);
    PXUIFieldAttribute.SetEnabled<RQRequest.finPeriodID>(sender, (object) row, budgetValidation);
    PXCacheEx.Adjust<FinPeriodIDAttribute>(sender, (object) null).For<RQRequest.finPeriodID>((System.Action<FinPeriodIDAttribute>) (a => a.RedefaultOnDateChanged = budgetValidation));
    PXCache cache = ((PXSelectBase) this.Lines).Cache;
    nullable5 = row.EmployeeID;
    int num8;
    if (nullable5.HasValue)
    {
      nullable5 = row.LocationID;
      if (nullable5.HasValue)
      {
        nullable1 = row.CustomerRequest;
        num8 = nullable1.GetValueOrDefault() ? 1 : (row.DepartmentID != null ? 1 : 0);
        goto label_24;
      }
    }
    num8 = 0;
label_24:
    cache.AllowInsert = num8 != 0;
    PXAction<RQRequest> validateAddresses = this.validateAddresses;
    nullable1 = row.Cancelled;
    bool flag5 = false;
    int num9 = !(nullable1.GetValueOrDefault() == flag5 & nullable1.HasValue) ? 0 : (((PXGraph) this).FindAllImplementations<IAddressValidationHelper>().RequiresValidation() ? 1 : 0);
    ((PXAction) validateAddresses).SetEnabled(num9 != 0);
    PXUIFieldAttribute.SetEnabled<RQRequest.shipToBAccountID>(sender, (object) row, row.ShipDestType != "L" && row.ShipDestType != "S");
    PXUIFieldAttribute.SetEnabled<RQRequest.shipToLocationID>(sender, (object) row, row.ShipDestType != "S");
    PXUIFieldAttribute.SetEnabled<RQRequest.siteID>(sender, (object) row, row.ShipDestType == "S");
    PXUIFieldAttribute.SetRequired<RQRequest.siteID>(sender, true);
    PXUIFieldAttribute.SetRequired<RQRequest.shipToBAccountID>(sender, row.ShipDestType != "S");
    PXUIFieldAttribute.SetRequired<RQRequest.shipToLocationID>(sender, row.ShipDestType != "S");
    PXUIFieldAttribute.SetVisible<RQRequest.shipToBAccountID>(sender, (object) row, row.ShipDestType != "S");
    PXUIFieldAttribute.SetVisible<RQRequest.shipToLocationID>(sender, (object) row, row.ShipDestType != "S");
    PXUIFieldAttribute.SetVisible<RQRequest.siteID>(sender, (object) row, row.ShipDestType == "S");
    if (row == null || !(row.ShipDestType == "S") || PXUIFieldAttribute.GetError<RQRequest.siteID>(sender, e.Row) != null)
      return;
    string siteIdErrorMessage = row.SiteIdErrorMessage;
    if (string.IsNullOrWhiteSpace(siteIdErrorMessage))
      return;
    sender.RaiseExceptionHandling<RQRequest.siteID>(e.Row, sender.GetValueExt<RQRequest.siteID>(e.Row), (Exception) new PXSetPropertyException(siteIdErrorMessage, (PXErrorLevel) 4));
  }

  protected virtual void RQRequest_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    RQRequest row = (RQRequest) e.Row;
    RQRequest oldRow = (RQRequest) e.OldRow;
    if (!row.Hold.GetValueOrDefault())
    {
      if (((PXSelectBase<RQRequestClass>) this.reqclass).Current.BudgetValidation.GetValueOrDefault() == 2 && ((IEnumerable<PXResult<RQBudget>>) ((PXSelectBase<RQBudget>) this.Budget).Select(Array.Empty<object>())).AsEnumerable<PXResult<RQBudget>>().Any<PXResult<RQBudget>>((Func<PXResult<RQBudget>, bool>) (_ =>
      {
        Decimal? requestAmt = PXResult<RQBudget>.op_Implicit(_).RequestAmt;
        Decimal? budgetAmt = PXResult<RQBudget>.op_Implicit(_).BudgetAmt;
        return requestAmt.GetValueOrDefault() > budgetAmt.GetValueOrDefault() & requestAmt.HasValue & budgetAmt.HasValue;
      })))
        sender.RaiseExceptionHandling<RQRequest.hold>((object) row, (object) row.Hold, (Exception) new PXSetPropertyException("The request amount exceeds the budget amount.", (PXErrorLevel) 4));
      else
        sender.RaiseExceptionHandling<RQRequest.hold>((object) row, (object) row.Hold, (Exception) null);
    }
    if (!sender.ObjectsEqual<RQRequest.openOrderQty>(e.Row, e.OldRow))
      ((SelectedEntityEvent<RQRequest>) PXEntityEventBase<RQRequest>.Container<RQRequest.Events>.Select((Expression<Func<RQRequest.Events, PXEntityEvent<RQRequest.Events>>>) (ev => ev.OpenOrderQtyChanged))).FireOn((PXGraph) this, row);
    bool? cancelled = row.Cancelled;
    if (!cancelled.GetValueOrDefault())
      return;
    cancelled = oldRow.Cancelled;
    bool flag1 = false;
    if (!(cancelled.GetValueOrDefault() == flag1 & cancelled.HasValue))
      return;
    foreach (PXResult<RQRequestLine> pxResult in ((PXSelectBase<RQRequestLine>) this.Lines).Select(new object[1]
    {
      (object) row.OrderNbr
    }))
    {
      RQRequestLine copy = (RQRequestLine) ((PXSelectBase) this.Lines).Cache.CreateCopy((object) PXResult<RQRequestLine>.op_Implicit(pxResult));
      cancelled = copy.Cancelled;
      bool flag2 = false;
      if (cancelled.GetValueOrDefault() == flag2 & cancelled.HasValue)
      {
        copy.Cancelled = new bool?(true);
        ((PXSelectBase<RQRequestLine>) this.Lines).Update(copy);
      }
    }
    row.Status = "L";
  }

  protected virtual void RQRequest_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    RQRequest row = (RQRequest) e.Row;
    if ((e.Operation & 3) == 3)
      return;
    PXDefaultAttribute.SetPersistingCheck<RQRequest.siteID>(sender, (object) row, row.ShipDestType == "S" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<RQRequest.shipToLocationID>(sender, (object) row, row.ShipDestType != "S" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<RQRequest.shipToBAccountID>(sender, (object) row, row.ShipDestType != "S" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  protected virtual void RQRequest_ShipDestType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = ((PXSelectBase<EPEmployee>) this.employee).Current == null ? (object) "C" : (object) "L";
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void RQRequest_ShipDestType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    RQRequest row = (RQRequest) e.Row;
    if (row == null)
      return;
    if (row.ShipDestType == "S")
    {
      sender.SetDefaultExt<RQRequest.siteID>(e.Row);
      sender.SetValueExt<RQRequest.shipToBAccountID>(e.Row, (object) null);
      sender.SetValueExt<RQRequest.shipToLocationID>(e.Row, (object) null);
    }
    else
    {
      sender.SetValueExt<RQRequest.siteID>(e.Row, (object) null);
      sender.SetDefaultExt<RQRequest.shipToBAccountID>(e.Row);
      sender.SetDefaultExt<RQRequest.shipToLocationID>(e.Row);
    }
  }

  protected virtual void RQRequest_SiteID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    RQRequest row = (RQRequest) e.Row;
    if (row == null || !(row.ShipDestType == "S"))
      return;
    string str = string.Empty;
    try
    {
      SharedRecordAttribute.DefaultRecord<RQRequest.shipAddressID>(sender, e.Row);
    }
    catch (SharedRecordMissingException ex)
    {
      sender.RaiseExceptionHandling<RQRequest.siteID>(e.Row, sender.GetValueExt<RQRequest.siteID>(e.Row), (Exception) new PXSetPropertyException("The document cannot be saved, shipping address is not specified  for the warehouse selected  on the Shipping Instructions tab.", (PXErrorLevel) 4));
      sender.SetValueExt<RQRequest.shipAddressID>(e.Row, (object) null);
      str = "The document cannot be saved, shipping address is not specified  for the warehouse selected  on the Shipping Instructions tab.";
    }
    try
    {
      SharedRecordAttribute.DefaultRecord<RQRequest.shipContactID>(sender, e.Row);
    }
    catch (SharedRecordMissingException ex)
    {
      sender.RaiseExceptionHandling<RQRequest.siteID>(e.Row, sender.GetValueExt<RQRequest.siteID>(e.Row), (Exception) new PXSetPropertyException("The document cannot be saved, shipping contact is not specified  for the warehouse selected  on the Shipping Instructions tab.", (PXErrorLevel) 4));
      sender.SetValueExt<RQRequest.shipContactID>(e.Row, (object) null);
      str = "The document cannot be saved, shipping contact is not specified  for the warehouse selected  on the Shipping Instructions tab.";
    }
    sender.SetValueExt<RQRequest.siteIdErrorMessage>(e.Row, (object) str);
    if (!string.IsNullOrWhiteSpace(str))
      return;
    PXUIFieldAttribute.SetError<RQRequest.siteID>(sender, e.Row, (string) null);
  }

  protected virtual void RQRequest_ShipToBAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    RQRequest row = (RQRequest) e.Row;
    if (row == null || ((PXSelectBase<EPEmployee>) this.employee).Current != null || !(row.ShipDestType == "C"))
      return;
    e.NewValue = (object) row.EmployeeID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void RQRequest_ShipToBAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if ((RQRequest) e.Row == null)
      return;
    sender.SetDefaultExt<RQRequest.shipToLocationID>(e.Row);
  }

  protected virtual void RQRequest_ShipToLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if ((RQRequest) e.Row == null)
      return;
    try
    {
      SharedRecordAttribute.DefaultRecord<RQRequest.shipAddressID>(sender, e.Row);
    }
    catch (SharedRecordMissingException ex)
    {
      sender.RaiseExceptionHandling<RQRequest.siteID>(e.Row, sender.GetValueExt<RQRequest.siteID>(e.Row), (Exception) new PXSetPropertyException("The document cannot be saved, shipping address is not specified  for the warehouse selected  on the Shipping Instructions tab.", (PXErrorLevel) 4));
    }
    try
    {
      SharedRecordAttribute.DefaultRecord<RQRequest.shipContactID>(sender, e.Row);
    }
    catch (SharedRecordMissingException ex)
    {
      sender.RaiseExceptionHandling<RQRequest.siteID>(e.Row, sender.GetValueExt<RQRequest.siteID>(e.Row), (Exception) new PXSetPropertyException("The document cannot be saved, shipping contact is not specified  for the warehouse selected  on the Shipping Instructions tab.", (PXErrorLevel) 4));
    }
  }

  protected virtual void RQRequest_ShipToLocationID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    RQRequest row = (RQRequest) e.Row;
    if (row == null || !(row.ShipDestType == "S"))
      return;
    ((CancelEventArgs) e).Cancel = true;
    e.NewValue = (object) null;
  }

  protected virtual void RQRequest_ShipToBAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    RQRequest row = (RQRequest) e.Row;
    if (row == null || !(row.ShipDestType == "S"))
      return;
    ((CancelEventArgs) e).Cancel = true;
    e.NewValue = (object) null;
  }

  protected virtual void RQRequest_ReqClassID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    RQRequest row = (RQRequest) e.Row;
    RQRequestClass rqRequestClass = PXResultset<RQRequestClass>.op_Implicit(((PXSelectBase<RQRequestClass>) this.reqclass).Select(new object[1]
    {
      e.NewValue
    }));
    if (rqRequestClass == null)
      return;
    if (rqRequestClass.RestrictItemList.GetValueOrDefault())
    {
      foreach (PXResult<RQRequestLine> pxResult in ((PXSelectBase<RQRequestLine>) this.Lines).Select(new object[1]
      {
        (object) row.OrderNbr
      }))
      {
        RQRequestLine rqRequestLine = PXResult<RQRequestLine>.op_Implicit(pxResult);
        GraphHelper.MarkUpdated(((PXSelectBase) this.Lines).Cache, (object) rqRequestLine, true);
        if (PXResultset<RQRequestClassItem>.op_Implicit(PXSelectBase<RQRequestClassItem, PXSelect<RQRequestClassItem, Where<RQRequestClassItem.reqClassID, Equal<Required<RQRequestClassItem.reqClassID>>, And<RQRequestClassItem.inventoryID, Equal<Required<RQRequestClassItem.inventoryID>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[2]
        {
          e.NewValue,
          (object) rqRequestLine.InventoryID
        })) == null && rqRequestLine.InventoryID.HasValue)
        {
          PXFieldState valueExt = (PXFieldState) ((PXSelectBase) this.Lines).Cache.GetValueExt<RQRequestLine.inventoryID>((object) rqRequestLine);
          ((PXSelectBase) this.Lines).Cache.RaiseExceptionHandling<RQRequestLine.inventoryID>((object) rqRequestLine, (object) valueExt.ToString(), (Exception) new PXSetPropertyException("Inventory item '{0}' is restricted for selected request class", new object[1]
          {
            (object) valueExt.ToString()
          }));
          ((CancelEventArgs) e).Cancel = true;
        }
      }
    }
    if (((CancelEventArgs) e).Cancel)
      throw new PXSetPropertyException("Some of items in request are restricted in selected request class");
  }

  protected virtual void RQRequest_ReqClassID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    RQRequest row = (RQRequest) e.Row;
    if (row == null)
      return;
    PXResultset<RQRequestClass> pxResultset;
    if (e.OldValue != null)
      pxResultset = ((PXSelectBase<RQRequestClass>) this.reqclass).Select(new object[1]
      {
        e.OldValue
      });
    else
      pxResultset = (PXResultset<RQRequestClass>) null;
    RQRequestClass rqRequestClass1 = PXResultset<RQRequestClass>.op_Implicit(pxResultset);
    RQRequestClass rqRequestClass2 = PXResultset<RQRequestClass>.op_Implicit(((PXSelectBase<RQRequestClass>) this.reqclass).Select(new object[1]
    {
      (object) row.ReqClassID
    }));
    if (rqRequestClass2 == null)
      return;
    ((PXSelectBase<RQRequestClass>) this.reqclass).Current = rqRequestClass2;
    int? budgetValidation = (int?) rqRequestClass1?.BudgetValidation;
    int? nullable1 = rqRequestClass2.BudgetValidation;
    if (!(budgetValidation.GetValueOrDefault() == nullable1.GetValueOrDefault() & budgetValidation.HasValue == nullable1.HasValue))
    {
      nullable1 = rqRequestClass2.BudgetValidation;
      int num = 0;
      if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
        sender.SetValuePending<RQRequest.finPeriodID>((object) row, (object) null);
      else
        sender.SetDefaultExt<RQRequest.finPeriodID>((object) row);
    }
    bool? customerRequest1 = (bool?) rqRequestClass1?.CustomerRequest;
    bool? customerRequest2 = rqRequestClass2.CustomerRequest;
    if (!(customerRequest1.GetValueOrDefault() == customerRequest2.GetValueOrDefault() & customerRequest1.HasValue == customerRequest2.HasValue))
    {
      customerRequest2 = rqRequestClass2.CustomerRequest;
      if (customerRequest2.GetValueOrDefault())
      {
        sender.SetDefaultExt<RQRequest.locationID>((object) row);
        sender.SetValuePending<RQRequest.locationID>((object) row, (object) null);
        row.DepartmentID = (string) null;
        row.FinPeriodID = (string) null;
        RQRequest rqRequest1 = row;
        nullable1 = new int?();
        int? nullable2 = nullable1;
        rqRequest1.EmployeeID = nullable2;
        RQRequest rqRequest2 = row;
        nullable1 = new int?();
        int? nullable3 = nullable1;
        rqRequest2.LocationID = nullable3;
      }
      else
      {
        sender.SetDefaultExt<RQRequest.employeeID>((object) row);
        sender.SetDefaultExt<RQRequest.locationID>((object) row);
        sender.SetDefaultExt<RQRequest.departmentID>((object) row);
        sender.SetDefaultExt<RQRequest.finPeriodID>((object) row);
      }
    }
    if (this.DefaultExpenseAccount((RQRequest) e.Row, (string) null, "Q"))
      return;
    foreach (PXResult<RQRequestLine> pxResult in ((PXSelectBase<RQRequestLine>) this.Lines).Select(new object[1]
    {
      (object) row.OrderNbr
    }))
      GraphHelper.MarkUpdated(((PXSelectBase) this.Lines).Cache, (object) PXResult<RQRequestLine>.op_Implicit(pxResult), true);
  }

  protected virtual void RQRequest_VendorID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<RQRequest.vendorLocationID>(e.Row);
    this.UpdateLinesVendor((RQRequest) e.Row);
  }

  protected virtual void RQRequest_VendorLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<RQRequest.siteID>(e.Row);
    SharedRecordAttribute.DefaultRecord<RQRequest.remitAddressID>(sender, e.Row);
    SharedRecordAttribute.DefaultRecord<RQRequest.remitContactID>(sender, e.Row);
    this.UpdateLinesVendor((RQRequest) e.Row);
  }

  protected virtual void RQRequest_Hold_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    RQRequest row = (RQRequest) e.Row;
    if (!row.Hold.GetValueOrDefault() || !row.Cancelled.GetValueOrDefault())
      return;
    cache.SetValueExt<RQRequest.cancelled>((object) row, (object) false);
  }

  protected virtual void RQRequest_DepartmentID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    RQRequest row = (RQRequest) e.Row;
    if (row == null)
      return;
    foreach (PXResult<RQRequestLine> pxResult in ((PXSelectBase<RQRequestLine>) this.Lines).Select(new object[1]
    {
      (object) row.OrderNbr
    }))
    {
      RQRequestLine copy = (RQRequestLine) ((PXSelectBase) this.Lines).Cache.CreateCopy((object) PXResult<RQRequestLine>.op_Implicit(pxResult));
      copy.DepartmentID = row.DepartmentID;
      ((PXSelectBase<RQRequestLine>) this.Lines).Update(copy);
    }
    this.DefaultExpenseAccount(row, "D", "D");
  }

  protected virtual void RQRequest_EmployeeID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    ((PXSelectBase<EPEmployee>) this.employee).Current = (EPEmployee) null;
    ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current = (PX.Objects.AR.Customer) null;
    RQRequest row = (RQRequest) e.Row;
    row.LocationID = new int?();
    cache.SetDefaultExt<RQRequest.locationID>((object) row);
    RQRequestClass current = ((PXSelectBase<RQRequestClass>) this.reqclass).Current;
    if (row != null && current != null && !current.CustomerRequest.GetValueOrDefault())
      this.DefaultExpenseAccount(row, "R", "R");
    cache.SetDefaultExt<RQRequest.shipDestType>((object) row);
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>() || !e.ExternalCall && cache.GetValuePending<RQRequest.curyID>(e.Row) != null)
      return;
    PX.Objects.CM.CurrencyInfo currencyInfo = CurrencyInfoAttribute.SetDefaults<RQRequest.curyInfoID>(cache, e.Row);
    string error = PXUIFieldAttribute.GetError<PX.Objects.CM.CurrencyInfo.curyEffDate>(((PXSelectBase) this.currencyinfo).Cache, (object) currencyInfo);
    if (!string.IsNullOrEmpty(error))
      cache.RaiseExceptionHandling<RQRequest.orderDate>(e.Row, (object) ((RQRequest) e.Row).OrderDate, (Exception) new PXSetPropertyException(error, (PXErrorLevel) 2));
    if (currencyInfo == null)
      return;
    ((RQRequest) e.Row).CuryID = currencyInfo.CuryID;
  }

  protected virtual void RQRequest_LocationID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    RQRequest row = (RQRequest) e.Row;
    RQRequestClass current = ((PXSelectBase<RQRequestClass>) this.reqclass).Current;
    if (row != null && current != null && current.CustomerRequest.GetValueOrDefault())
      this.DefaultExpenseAccount(row, "R", "R");
    cache.SetDefaultExt<RQRequest.shipToLocationID>((object) row);
  }

  protected virtual void RQRequest_LocationID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    RQRequest row = (RQRequest) e.Row;
    if (row == null || row.EmployeeID.HasValue)
      return;
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<RQRequest.orderDate> e)
  {
    CurrencyInfoAttribute.SetEffectiveDate<RQRequest.orderDate>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<RQRequest.orderDate>>) e).Cache, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<RQRequest.orderDate>>) e).Args);
  }

  protected virtual void RQRequestLine_VendorID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<RQRequestLine.vendorLocationID>(e.Row);
    sender.SetDefaultExt<RQRequestLine.vendorName>(e.Row);
  }

  protected virtual void RQRequestLine_VendorName_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((RQRequestLine) e.Row).VendorID.HasValue)
      return;
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void RQRequestLine_InventoryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    RQRequestClass current = ((PXSelectBase<RQRequestClass>) this.reqclass).Current;
    if (!((PXSelectBase<RQRequest>) this.Document).Current.Hold.GetValueOrDefault())
      return;
    sender.SetDefaultExt<RQRequestLine.uOM>(e.Row);
    sender.SetDefaultExt<RQRequestLine.vendorID>(e.Row);
    sender.SetDefaultExt<RQRequestLine.subItemID>(e.Row);
    sender.SetDefaultExt<RQRequestLine.description>(e.Row);
    sender.SetDefaultExt<RQRequestLine.estUnitCost>(e.Row);
    sender.SetDefaultExt<RQRequestLine.curyEstUnitCost>(e.Row);
    sender.SetDefaultExt<RQRequestLine.promisedDate>(e.Row);
    if (current == null)
      return;
    if (current.ExpenseAccountDefault == "I")
    {
      sender.SetDefaultExt<RQRequestLine.expenseAcctID>(e.Row);
      sender.SetDefaultExt<RQRequestLine.expenseSubID>(e.Row);
    }
    else
    {
      if (current.ExpenseSubMask == null || !current.ExpenseSubMask.Contains("I"))
        return;
      sender.SetDefaultExt<RQRequestLine.expenseSubID>(e.Row);
    }
  }

  protected virtual void RQRequestLine_SubItemID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<RQRequestLine.curyEstUnitCost>(e.Row);
  }

  protected virtual void RQRequestLine_UOM_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<RQRequestLine.curyEstUnitCost>(e.Row);
  }

  protected virtual void RQRequestLine_VendorLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<RQRequestLine.curyEstUnitCost>(e.Row);
  }

  protected virtual void RQRequestLine_VendorLocationID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    RQRequestLine row = (RQRequestLine) e.Row;
    if (row != null && row.VendorID.HasValue)
      return;
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void RQRequestLine_ExpenseAcctID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<RQRequestLine.expenseSubID>(e.Row);
  }

  protected virtual void RQRequestLine_ExpenseAcctID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    RQRequestLine row = (RQRequestLine) e.Row;
    if (row == null || e.NewValue == null || !PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    PX.Objects.GL.Account account = (PX.Objects.GL.Account) PXSelectorAttribute.Select<RQRequestLine.expenseAcctID>(sender, (object) row, e.NewValue);
    if (account != null && account.CuryID != null && account.CuryID != ((PXSelectBase<RQRequest>) this.Document).Current.CuryID)
      throw new PXSetPropertyException("Denominated budget account  currency is different from Request currency");
  }

  protected virtual void RQRequestLine_ExpenseAcctID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    RQRequestLine row = (RQRequestLine) e.Row;
    RQRequestClass current = ((PXSelectBase<RQRequestClass>) this.reqclass).Current;
    if (row == null)
      return;
    if (current != null)
    {
      int? nullable1 = current.BudgetValidation;
      int num = 0;
      if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
      {
        switch (current.ExpenseAccountDefault)
        {
          case "D":
            EPDepartment epDepartment = PXResultset<EPDepartment>.op_Implicit(PXSelectBase<EPDepartment, PXSelect<EPDepartment, Where<EPDepartment.departmentID, Equal<Required<EPDepartment.departmentID>>>>.Config>.Select((PXGraph) this, new object[1]
            {
              (object) ((PXSelectBase<RQRequest>) this.Document).Current.DepartmentID
            }));
            PXFieldDefaultingEventArgs defaultingEventArgs1 = e;
            int? nullable2;
            if (epDepartment == null)
            {
              nullable1 = new int?();
              nullable2 = nullable1;
            }
            else
              nullable2 = epDepartment.ExpenseAccountID;
            // ISSUE: variable of a boxed type
            __Boxed<int?> local1 = (ValueType) nullable2;
            defaultingEventArgs1.NewValue = (object) local1;
            break;
          case "R":
            if (current.CustomerRequest.GetValueOrDefault())
            {
              PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select((PXGraph) this, new object[2]
              {
                (object) ((PXSelectBase<RQRequest>) this.Document).Current.EmployeeID,
                (object) ((PXSelectBase<RQRequest>) this.Document).Current.LocationID
              }));
              PXFieldDefaultingEventArgs defaultingEventArgs2 = e;
              int? nullable3;
              if (location == null)
              {
                nullable1 = new int?();
                nullable3 = nullable1;
              }
              else
                nullable3 = location.VExpenseAcctID;
              // ISSUE: variable of a boxed type
              __Boxed<int?> local2 = (ValueType) nullable3;
              defaultingEventArgs2.NewValue = (object) local2;
              break;
            }
            EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
            {
              (object) ((PXSelectBase<RQRequest>) this.Document).Current.EmployeeID
            }));
            PXFieldDefaultingEventArgs defaultingEventArgs3 = e;
            int? nullable4;
            if (epEmployee == null)
            {
              nullable1 = new int?();
              nullable4 = nullable1;
            }
            else
              nullable4 = epEmployee.ExpenseAcctID;
            // ISSUE: variable of a boxed type
            __Boxed<int?> local3 = (ValueType) nullable4;
            defaultingEventArgs3.NewValue = (object) local3;
            break;
          case "I":
            PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
            PXFieldDefaultingEventArgs defaultingEventArgs4 = e;
            nullable1 = (int?) inventoryItem?.COGSAcctID;
            // ISSUE: variable of a boxed type
            __Boxed<int?> local4 = (ValueType) (nullable1 ?? ((PXSelectBase<RQRequestClass>) this.reqclass).Current.ExpenseAcctID);
            defaultingEventArgs4.NewValue = (object) local4;
            break;
          case "Q":
            e.NewValue = (object) current.ExpenseAcctID;
            break;
        }
        if (e.NewValue != null && PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
        {
          PX.Objects.GL.Account account = (PX.Objects.GL.Account) PXSelectorAttribute.Select<RQRequestLine.expenseAcctID>(sender, (object) row, e.NewValue);
          if (account != null && account.CuryID != null && account.CuryID != ((PXSelectBase<RQRequest>) this.Document).Current.CuryID)
            e.NewValue = (object) null;
        }
        ((CancelEventArgs) e).Cancel = true;
        return;
      }
    }
    e.NewValue = (object) null;
  }

  protected virtual void RQRequestLine_ExpenseSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    RQRequestLine row = (RQRequestLine) e.Row;
    RQRequestClass current = ((PXSelectBase<RQRequestClass>) this.reqclass).Current;
    if (row == null)
      return;
    if (current != null)
    {
      int? nullable1 = current.BudgetValidation;
      int num = 0;
      if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
      {
        if (((PXSelectBase<RQRequest>) this.Document).Current == null)
          return;
        nullable1 = row.ExpenseAcctID;
        if (!nullable1.HasValue)
          return;
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
        EPDepartment epDepartment = PXResultset<EPDepartment>.op_Implicit(PXSelectBase<EPDepartment, PXSelect<EPDepartment, Where<EPDepartment.departmentID, Equal<Required<EPDepartment.departmentID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.DepartmentID
        }));
        int? nullable2 = new int?();
        bool? customerRequest;
        int? nullable3;
        if (current != null)
        {
          customerRequest = current.CustomerRequest;
          if (customerRequest.GetValueOrDefault())
          {
            PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select((PXGraph) this, new object[2]
            {
              (object) ((PXSelectBase<RQRequest>) this.Document).Current.EmployeeID,
              (object) ((PXSelectBase<RQRequest>) this.Document).Current.LocationID
            }));
            int? nullable4;
            if (location == null)
            {
              nullable1 = new int?();
              nullable4 = nullable1;
            }
            else
              nullable4 = location.VExpenseSubID;
            nullable3 = nullable4;
            goto label_17;
          }
        }
        EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) ((PXSelectBase<RQRequest>) this.Document).Current.EmployeeID
        }));
        int? nullable5;
        if (epEmployee == null)
        {
          nullable1 = new int?();
          nullable5 = nullable1;
        }
        else
          nullable5 = epEmployee.ExpenseSubID;
        nullable3 = nullable5;
label_17:
        int? nullable6;
        if (inventoryItem == null)
        {
          nullable1 = new int?();
          nullable6 = nullable1;
        }
        else
          nullable6 = inventoryItem.COGSSubID;
        int? nullable7 = nullable6;
        int? nullable8;
        if (epDepartment == null)
        {
          nullable1 = new int?();
          nullable8 = nullable1;
        }
        else
          nullable8 = epDepartment.ExpenseSubID;
        int? nullable9 = nullable8;
        if (current != null)
        {
          int? expenseSubId = current.ExpenseSubID;
          string expenseSubMask = current.ExpenseSubMask;
          object[] sources = new object[4]
          {
            (object) expenseSubId,
            (object) nullable9,
            (object) nullable7,
            (object) nullable3
          };
          System.Type[] fields = new System.Type[4]
          {
            typeof (RQRequestClass.expenseSubID),
            typeof (EPDepartment.expenseSubID),
            typeof (PX.Objects.IN.InventoryItem.cOGSSubID),
            null
          };
          customerRequest = current.CustomerRequest;
          fields[3] = customerRequest.GetValueOrDefault() ? typeof (PX.Objects.CR.Location.vExpenseSubID) : typeof (EPEmployee.expenseSubID);
          object obj = (object) SubAccountMaskAttribute.MakeSub<RQRequestClass.expenseSubMask>((PXGraph) this, expenseSubMask, sources, fields);
          try
          {
            sender.RaiseFieldUpdating<RQRequestLine.expenseSubID>(e.Row, ref obj);
            e.NewValue = (object) (int?) obj;
          }
          catch (PXSetPropertyException ex)
          {
            PXTrace.WriteWarning((Exception) ex);
            e.NewValue = (object) null;
          }
        }
        ((CancelEventArgs) e).Cancel = true;
        return;
      }
    }
    e.NewValue = (object) null;
  }

  protected virtual void RQRequestLine_PromisedDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    RQRequestClass current = ((PXSelectBase<RQRequestClass>) this.reqclass).Current;
    DateTime? orderDate = ((PXSelectBase<RQRequest>) this.Document).Current.OrderDate;
    if (current == null || !orderDate.HasValue)
      return;
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    short? promisedLeadTime = current.PromisedLeadTime;
    int? nullable1 = promisedLeadTime.HasValue ? new int?((int) promisedLeadTime.GetValueOrDefault()) : new int?();
    int num1 = 0;
    DateTime? nullable2;
    if (!(nullable1.GetValueOrDefault() > num1 & nullable1.HasValue))
    {
      nullable2 = orderDate;
    }
    else
    {
      DateTime dateTime = orderDate.Value;
      ref DateTime local = ref dateTime;
      promisedLeadTime = current.PromisedLeadTime;
      double num2 = (double) promisedLeadTime.Value;
      nullable2 = new DateTime?(local.AddDays(num2));
    }
    // ISSUE: variable of a boxed type
    __Boxed<DateTime?> local1 = (ValueType) nullable2;
    defaultingEventArgs.NewValue = (object) local1;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void RQRequestLine_Cancelled_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    RQRequestLine row = (RQRequestLine) e.Row;
    if (row.Cancelled.GetValueOrDefault())
      sender.SetValueExt<RQRequestLine.orderQty>((object) row, (object) row.IssuedQty);
    else
      sender.SetValueExt<RQRequestLine.orderQty>((object) row, (object) row.OriginQty);
  }

  protected virtual void RQRequestLine_OrderQty_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    RQRequestLine row = (RQRequestLine) e.Row;
    Decimal newValue = (Decimal) e.NewValue;
    Decimal? reqQty = row.ReqQty;
    Decimal valueOrDefault = reqQty.GetValueOrDefault();
    if (!(newValue < valueOrDefault & reqQty.HasValue))
      return;
    e.NewValue = (object) row.ReqQty;
    sender.RaiseExceptionHandling<RQRequestLine.orderQty>((object) row, (object) null, (Exception) new PXSetPropertyException("Insufficient quantity available. Line quantity was changed to match.", (PXErrorLevel) 2));
  }

  protected virtual void RQRequestLine_OrderQty_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    RQRequestLine row = (RQRequestLine) e.Row;
    if (row == null)
      return;
    PXCache pxCache = sender;
    RQRequestLine rqRequestLine = row;
    Decimal? nullable = row.OpenQty;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = row.OrderQty;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    nullable = (Decimal?) e.OldValue;
    Decimal valueOrDefault3 = nullable.GetValueOrDefault();
    Decimal num = valueOrDefault2 - valueOrDefault3;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal> local = (ValueType) (valueOrDefault1 + num);
    pxCache.SetValueExt<RQRequestLine.openQty>((object) rqRequestLine, (object) local);
  }

  protected virtual void RQRequestLine_ManualPrice_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    RQRequestLine row = (RQRequestLine) e.Row;
    if (row == null || row.ManualPrice.GetValueOrDefault() || sender.Graph.IsCopyPasteContext)
      return;
    sender.SetDefaultExt<RQRequestLine.curyEstUnitCost>(e.Row);
  }

  protected virtual void RQRequestLine_UOM_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    RQRequestLine row = (RQRequestLine) e.Row;
    if (row == null)
      return;
    Decimal? nullable1 = row.ReqQty;
    Decimal num = 0M;
    if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
      return;
    string newValue = (string) e.NewValue;
    if (!row.InventoryID.HasValue)
      return;
    RQRequestLine rqRequestLine1 = row;
    PXCache sender1 = sender;
    int? inventoryId1 = row.InventoryID;
    string ToUnit1 = newValue;
    nullable1 = row.BaseOrderQty;
    Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
    Decimal? nullable2 = new Decimal?(INUnitAttribute.ConvertFromBase(sender1, inventoryId1, ToUnit1, valueOrDefault1, INPrecision.QUANTITY));
    rqRequestLine1.OrderQty = nullable2;
    RQRequestLine rqRequestLine2 = row;
    PXCache sender2 = sender;
    int? inventoryId2 = row.InventoryID;
    string ToUnit2 = newValue;
    nullable1 = row.BaseReqQty;
    Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
    Decimal? nullable3 = new Decimal?(INUnitAttribute.ConvertFromBase(sender2, inventoryId2, ToUnit2, valueOrDefault2, INPrecision.QUANTITY));
    rqRequestLine2.ReqQty = nullable3;
    RQRequestLine rqRequestLine3 = row;
    PXCache sender3 = sender;
    int? inventoryId3 = row.InventoryID;
    string ToUnit3 = newValue;
    nullable1 = row.BaseOriginQty;
    Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
    Decimal? nullable4 = new Decimal?(INUnitAttribute.ConvertFromBase(sender3, inventoryId3, ToUnit3, valueOrDefault3, INPrecision.QUANTITY));
    rqRequestLine3.OriginQty = nullable4;
    RQRequestLine rqRequestLine4 = row;
    PXCache sender4 = sender;
    int? inventoryId4 = row.InventoryID;
    string ToUnit4 = newValue;
    nullable1 = row.BaseOpenQty;
    Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
    Decimal? nullable5 = new Decimal?(INUnitAttribute.ConvertFromBase(sender4, inventoryId4, ToUnit4, valueOrDefault4, INPrecision.QUANTITY));
    rqRequestLine4.OpenQty = nullable5;
    RQRequestLine rqRequestLine5 = row;
    PXCache sender5 = sender;
    int? inventoryId5 = row.InventoryID;
    string ToUnit5 = newValue;
    nullable1 = row.BaseIssuedQty;
    Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
    Decimal? nullable6 = new Decimal?(INUnitAttribute.ConvertFromBase(sender5, inventoryId5, ToUnit5, valueOrDefault5, INPrecision.QUANTITY));
    rqRequestLine5.IssuedQty = nullable6;
    foreach (PXResult<RQRequisitionContent> pxResult in PXSelectBase<RQRequisitionContent, PXSelect<RQRequisitionContent, Where<RQRequisitionContent.orderNbr, Equal<Required<RQRequestLine.orderNbr>>, And<RQRequisitionContent.lineNbr, Equal<Required<RQRequestLine.lineNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.OrderNbr,
      (object) row.LineNbr
    }))
    {
      RQRequisitionContent copy = PXCache<RQRequisitionContent>.CreateCopy(PXResult<RQRequisitionContent>.op_Implicit(pxResult));
      RQRequisitionContent requisitionContent = copy;
      PXCache sender6 = sender;
      int? inventoryId6 = row.InventoryID;
      string ToUnit6 = newValue;
      nullable1 = copy.BaseItemQty;
      Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
      Decimal? nullable7 = new Decimal?(INUnitAttribute.ConvertFromBase(sender6, inventoryId6, ToUnit6, valueOrDefault6, INPrecision.QUANTITY));
      requisitionContent.ItemQty = nullable7;
      ((PXSelectBase<RQRequisitionContent>) this.Contents).Update(copy);
    }
  }

  protected virtual void RQRequestLine_CuryEstUnitCost_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is RQRequestLine row1))
      return;
    Decimal? nullable1;
    if (row1.ManualPrice.GetValueOrDefault())
    {
      nullable1 = row1.CuryEstUnitCost;
      if (nullable1.HasValue)
      {
        e.NewValue = (object) row1.CuryEstUnitCost;
        return;
      }
    }
    ((CancelEventArgs) e).Cancel = true;
    PXFieldDefaultingEventArgs defaultingEventArgs = e;
    nullable1 = row1.CuryEstUnitCost;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal> valueOrDefault = (ValueType) nullable1.GetValueOrDefault();
    defaultingEventArgs.NewValue = (object) valueOrDefault;
    RQRequest current = ((PXSelectBase<RQRequest>) this.Document).Current;
    int? nullable2 = row1.InventoryID;
    if (!nullable2.HasValue || string.IsNullOrEmpty(row1.UOM) || current == null)
      return;
    PXGraph graph = sender.Graph;
    RQRequestLine row2 = row1;
    int? vendorId = row1.VendorID;
    int? vendorLocationId = row1.VendorLocationID;
    DateTime? docDate = new DateTime?();
    string curyId = current.CuryID;
    int? inventoryId = row1.InventoryID;
    int? subItemId = row1.SubItemID;
    nullable2 = new int?();
    int? siteID = nullable2;
    string uom = row1.UOM;
    Decimal? nullable3 = POItemCostManager.Fetch<RQRequestLine.inventoryID, RQRequestLine.curyInfoID>(graph, (object) row2, vendorId, vendorLocationId, docDate, curyId, inventoryId, subItemId, siteID, uom);
    nullable1 = nullable3;
    Decimal num = 0M;
    if (nullable1.GetValueOrDefault() >= num & nullable1.HasValue)
      e.NewValue = (object) nullable3;
    APVendorPriceMaint.CheckNewUnitCost<RQRequestLine, RQRequestLine.curyEstUnitCost>(sender, row1, e.NewValue);
  }

  protected virtual void RQRequestLine_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    RQRequestLine row = (RQRequestLine) e.Row;
    RQRequestLine newRow = (RQRequestLine) e.NewRow;
    int? inventoryId1 = row.InventoryID;
    int? inventoryId2 = newRow.InventoryID;
    if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue || ((PXSelectBase<RQRequest>) this.Document).Current.Hold.GetValueOrDefault())
      return;
    object uom = (object) newRow.UOM;
    try
    {
      sender.RaiseFieldVerifying<RQRequestLine.uOM>((object) newRow, ref uom);
    }
    catch (PXSetPropertyException ex)
    {
      sender.RaiseExceptionHandling<RQRequestLine.uOM>((object) newRow, (object) newRow.UOM, (Exception) ex);
      newRow.UOM = (string) null;
    }
  }

  protected virtual void RQRequestLine_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    RQRequestLine row = (RQRequestLine) e.Row;
    if (((PXSelectBase<RQRequest>) this.Document).Current != null && ((PXSelectBase<RQRequest>) this.Document).Current.Hold.GetValueOrDefault() && !row.Cancelled.GetValueOrDefault())
      row.OriginQty = row.OrderQty;
    if (!row.VendorLocationID.HasValue)
      row.AlternateID = (string) null;
    if (!e.ExternalCall && !sender.Graph.IsImport || !sender.ObjectsEqual<RQRequestLine.branchID, RQRequestLine.inventoryID, RQRequestLine.uOM, RQRequestLine.orderQty, RQRequestLine.manualPrice>(e.Row, e.OldRow) || sender.ObjectsEqual<RQRequestLine.curyEstUnitCost, RQRequestLine.curyEstExtCost>(e.Row, e.OldRow))
      return;
    row.ManualPrice = new bool?(true);
  }

  protected virtual void RQRequestLine_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    RQRequestLine row = (RQRequestLine) e.Row;
    if (row != null && PXResultset<RQRequisitionContent>.op_Implicit(((PXSelectBase<RQRequisitionContent>) this.Contents).Select(Array.Empty<object>())) != null)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXRowPersistingException(typeof (RQRequestLine.lineNbr).Name, (object) row, "Item has been used in a requisition. It can't be deleted.");
    }
  }

  protected virtual void RQRequestLine_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    RQRequestLine row = (RQRequestLine) e.Row;
    if (row == null || row.InventoryID.HasValue)
      return;
    row.Updatable = new bool?(true);
  }

  protected virtual void RQRequestLine_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    RQRequestLine row = (RQRequestLine) e.Row;
    if (row == null)
      return;
    RQRequestClass current = ((PXSelectBase<RQRequestClass>) this.reqclass).Current;
    PXUIFieldAttribute.SetEnabled<RQRequestLine.orderQty>(sender, (object) row, !row.Cancelled.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<RQRequestLine.curyEstExtCost>(sender, (object) row, !row.Cancelled.GetValueOrDefault());
    if (row == null)
      return;
    bool? nullable;
    int num1;
    if (current != null)
    {
      nullable = current.VendorMultiply;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag = num1 != 0;
    PXUIFieldAttribute.SetEnabled<RQRequestLine.vendorID>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<RQRequestLine.vendorLocationID>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<RQRequestLine.alternateID>(sender, (object) row, row.VendorLocationID.HasValue);
    PXCache pxCache1 = sender;
    RQRequestLine rqRequestLine1 = row;
    Decimal? reqQty1 = row.ReqQty;
    Decimal num2 = 0M;
    int num3 = reqQty1.GetValueOrDefault() == num2 & reqQty1.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<RQRequestLine.inventoryID>(pxCache1, (object) rqRequestLine1, num3 != 0);
    PXCache pxCache2 = sender;
    RQRequestLine rqRequestLine2 = row;
    Decimal? reqQty2 = row.ReqQty;
    Decimal num4 = 0M;
    int num5 = reqQty2.GetValueOrDefault() == num4 & reqQty2.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<RQRequestLine.subItemID>(pxCache2, (object) rqRequestLine2, num5 != 0);
    PXCache pxCache3 = sender;
    RQRequestLine rqRequestLine3 = row;
    reqQty2 = row.ReqQty;
    Decimal num6 = 0M;
    int num7 = reqQty2.GetValueOrDefault() == num6 & reqQty2.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<RQRequestLine.uOM>(pxCache3, (object) rqRequestLine3, num7 != 0);
    PXCache pxCache4 = sender;
    RQRequestLine rqRequestLine4 = row;
    reqQty2 = row.ReqQty;
    Decimal num8 = 0M;
    int num9 = reqQty2.GetValueOrDefault() == num8 & reqQty2.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<RQRequestLine.orderQty>(pxCache4, (object) rqRequestLine4, num9 != 0);
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
    PXCache pxCache5 = sender;
    RQRequestLine rqRequestLine5 = row;
    int num10;
    if (inventoryItem != null)
    {
      nullable = inventoryItem.StkItem;
      num10 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num10 = 1;
    PXUIFieldAttribute.SetEnabled<RQRequestLine.subItemID>(pxCache5, (object) rqRequestLine5, num10 != 0);
    if (((PXSelectBase<RQRequest>) this.Document).Current == null || !(((PXSelectBase<RQRequest>) this.Document).Current.Status == "N"))
      return;
    nullable = row.Updatable;
    if (nullable.GetValueOrDefault() && this.Approval.ValidateAccess(((PXSelectBase<RQRequest>) this.Document).Current.WorkgroupID, ((PXSelectBase<RQRequest>) this.Document).Current.OwnerID))
    {
      PXUIFieldAttribute.SetEnabled<RQRequestLine.inventoryID>(sender, (object) row, true);
      PXUIFieldAttribute.SetEnabled<RQRequestLine.subItemID>(sender, (object) row, true);
      PXUIFieldAttribute.SetEnabled<RQRequestLine.description>(sender, (object) row, true);
      PXUIFieldAttribute.SetEnabled<RQRequestLine.uOM>(sender, (object) row, true);
      PXUIFieldAttribute.SetEnabled<RQRequestLine.orderQty>(sender, (object) row, true);
      PXUIFieldAttribute.SetEnabled<RQRequestLine.curyEstUnitCost>(sender, (object) row, true);
      PXUIFieldAttribute.SetEnabled<RQRequestLine.estUnitCost>(sender, (object) row, true);
      PXUIFieldAttribute.SetEnabled<RQRequestLine.cancelled>(sender, (object) row, true);
    }
    else
    {
      PXUIFieldAttribute.SetEnabled(sender, (object) row, false);
      PXUIFieldAttribute.SetEnabled<RQRequestLine.description>(sender, (object) row, true);
      PXUIFieldAttribute.SetEnabled<RQRequestLine.cancelled>(sender, (object) row, true);
      PXUIFieldAttribute.SetEnabled<RQRequestLine.requestedDate>(sender, (object) row, true);
      PXUIFieldAttribute.SetEnabled<RQRequestLine.promisedDate>(sender, (object) row, true);
    }
  }

  protected virtual void RQBudget_ExpenseAcctID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  private bool DefaultExpenseAccount(RQRequest req, string accountSource, string subAccountMask)
  {
    RQRequestClass current = ((PXSelectBase<RQRequestClass>) this.reqclass).Current;
    if (current == null)
      return false;
    bool flag1 = current.ExpenseAccountDefault != accountSource;
    bool flag2 = current.ExpenseSubMask != null && current.ExpenseSubMask.Contains(subAccountMask);
    if (!flag1 && !flag2)
      return false;
    foreach (PXResult<RQRequestLine> pxResult in ((PXSelectBase<RQRequestLine>) this.Lines).Select(new object[1]
    {
      (object) req.OrderNbr
    }))
    {
      RQRequestLine copy = (RQRequestLine) ((PXSelectBase) this.Lines).Cache.CreateCopy((object) PXResult<RQRequestLine>.op_Implicit(pxResult));
      if (flag1)
        ((PXSelectBase) this.Lines).Cache.SetDefaultExt<RQRequestLine.expenseAcctID>((object) copy);
      if (flag1 | flag2)
      {
        try
        {
          ((PXSelectBase) this.Lines).Cache.SetDefaultExt<RQRequestLine.expenseSubID>((object) copy);
        }
        catch
        {
          copy.ExpenseAcctID = new int?();
          ((PXSelectBase) this.Lines).Cache.RaiseExceptionHandling<RQRequestLine.expenseSubID>((object) copy, (object) null, (Exception) null);
        }
      }
      ((PXSelectBase<RQRequestLine>) this.Lines).Update(copy);
    }
    return true;
  }

  private void UpdateLinesVendor(RQRequest row)
  {
    foreach (PXResult<RQRequestLine> pxResult in ((PXSelectBase<RQRequestLine>) this.Lines).Select(new object[1]
    {
      (object) row.OrderNbr
    }))
    {
      RQRequestLine copy = (RQRequestLine) ((PXSelectBase) this.Lines).Cache.CreateCopy((object) PXResult<RQRequestLine>.op_Implicit(pxResult));
      copy.VendorID = row.VendorID;
      copy.VendorLocationID = row.VendorLocationID;
      ((PXSelectBase<RQRequestLine>) this.Lines).Update(copy);
    }
  }

  public class PriceRecalcExt : 
    PX.Objects.RQ.PriceRecalcExt<RQRequestEntry, RQRequest, RQRequestLine, RQRequestLine.curyEstUnitCost>
  {
    protected override PXSelectBase<RQRequestLine> DetailSelect
    {
      get => (PXSelectBase<RQRequestLine>) this.Base.Lines;
    }

    protected override PX.Objects.RQ.PriceRecalcExt<RQRequestEntry, RQRequest, RQRequestLine, RQRequestLine.curyEstUnitCost>.IPricedLine WrapLine(
      RQRequestLine line)
    {
      return (PX.Objects.RQ.PriceRecalcExt<RQRequestEntry, RQRequest, RQRequestLine, RQRequestLine.curyEstUnitCost>.IPricedLine) new RQRequestEntry.PriceRecalcExt.PricedLine(line);
    }

    private class PricedLine : 
      PX.Objects.RQ.PriceRecalcExt<RQRequestEntry, RQRequest, RQRequestLine, RQRequestLine.curyEstUnitCost>.IPricedLine
    {
      private readonly RQRequestLine _line;

      public PricedLine(RQRequestLine line) => this._line = line;

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
  public class RQRequestEntryAddressLookupExtension : 
    AddressLookupExtension<RQRequestEntry, RQRequest, POShipAddress>
  {
    protected override string AddressView => "Shipping_Address";
  }

  public class RQRequestEntryShippingAddressCachingHelper : 
    AddressValidationExtension<RQRequestEntry, POShipAddress>
  {
    protected override IEnumerable<PXSelectBase<POShipAddress>> AddressSelects()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      RQRequestEntry.RQRequestEntryShippingAddressCachingHelper addressCachingHelper = this;
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

  public class RQRequestEntryRemitAddressCachingHelper : 
    AddressValidationExtension<RQRequestEntry, PX.Objects.PO.PORemitAddress>
  {
    protected override IEnumerable<PXSelectBase<PX.Objects.PO.PORemitAddress>> AddressSelects()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      RQRequestEntry.RQRequestEntryRemitAddressCachingHelper addressCachingHelper = this;
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

  public class AffectedInRequisition : 
    ProcessAffectedEntitiesInPrimaryGraphBase<RQRequestEntry.AffectedInRequisition, RQRequisitionEntry, RQRequest, RQRequestEntry>
  {
    protected override bool PersistInSameTransaction => true;

    protected override bool EntityIsAffected(RQRequest entity)
    {
      return this.WhenAnyFieldIsAffected(entity, (Expression<Func<RQRequest, object>>) (e => (object) e.OpenOrderQty));
    }

    protected override void ProcessAffectedEntity(RQRequestEntry primaryGraph, RQRequest entity)
    {
      ((SelectedEntityEvent<RQRequest>) PXEntityEventBase<RQRequest>.Container<RQRequest.Events>.Select((Expression<Func<RQRequest.Events, PXEntityEvent<RQRequest.Events>>>) (ev => ev.OpenOrderQtyChanged))).FireOn((PXGraph) primaryGraph, entity);
    }

    protected override RQRequest ActualizeEntity(RQRequestEntry primaryGraph, RQRequest entity)
    {
      return PXResultset<RQRequest>.op_Implicit(((PXSelectBase<RQRequest>) primaryGraph.Document).Search<RQRequest.orderNbr>((object) entity.OrderNbr, Array.Empty<object>()));
    }
  }

  public class RQRequestEntry_ActivityDetailsExt : 
    ActivityDetailsExt<RQRequestEntry, RQRequest, RQRequest.noteID>
  {
  }
}
