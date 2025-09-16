// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ExpenseClaimEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.EP.DAC;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.TX;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.EP;

public class ExpenseClaimEntry : 
  PXGraph<
  #nullable disable
  ExpenseClaimEntry, EPExpenseClaim>,
  PXImportAttribute.IPXPrepareItems
{
  public PXAction<EPExpenseClaim> ViewTaxes;
  public PXAction<EPExpenseClaim> CommitTaxes;
  public PXAction<EPExpenseClaim> submit;
  public PXAction<EPExpenseClaim> edit;
  public PXAction<EPExpenseClaim> release;
  public ToggleCurrency<EPExpenseClaim> CurrencyView;
  public PXAction<EPExpenseClaim> expenseClaimPrint;
  public PXAction<EPExpenseClaim> showSubmitReceipt;
  public PXAction<EPExpenseClaim> submitReceipt;
  public PXAction<EPExpenseClaim> cancelSubmitReceipt;
  public PXAction<EPExpenseClaim> createNew;
  public PXAction<EPExpenseClaim> editDetail;
  public PXAction<EPExpenseClaim> viewUnsubmitReceipt;
  public PXAction<EPExpenseClaim> changeOk;
  public PXAction<EPExpenseClaim> changeCancel;
  public PXAction<EPExpenseClaim> SaveTaxZone;
  public PXAction<EPExpenseClaim> viewProject;
  public PXAction<EPExpenseClaim> viewInvoice;
  public PXAction<EPExpenseClaim> viewAPInvoice;
  public PXWorkflowEventHandler<EPExpenseClaim> OnSubmit;
  public PXWorkflowEventHandler<EPExpenseClaim> OnUpdateStatus;
  [PXHidden]
  public PXSelect<PX.Objects.CT.Contract> Dummy;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (EPExpenseClaim.approved), typeof (EPExpenseClaim.released), typeof (EPExpenseClaim.hold), typeof (EPExpenseClaim.rejected), typeof (EPExpenseClaim.status)})]
  [PXViewName("Expense Claim")]
  public PXSelectJoin<EPExpenseClaim, InnerJoin<PX.Objects.EP.EPEmployee, On<PX.Objects.EP.EPEmployee.bAccountID, Equal<EPExpenseClaim.employeeID>>>, Where<EPExpenseClaim.createdByID, Equal<Current<AccessInfo.userID>>, Or<PX.Objects.EP.EPEmployee.defContactID, Equal<Current<AccessInfo.contactID>>, Or<PX.Objects.EP.EPEmployee.defContactID, IsSubordinateOfContact<Current<AccessInfo.contactID>>, Or<EPExpenseClaim.noteID, Approver<Current<AccessInfo.contactID>>, Or<EPExpenseClaim.employeeID, WingmanUser<Current<AccessInfo.userID>, EPDelegationOf.expenses>>>>>>> ExpenseClaim;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (EPExpenseClaim.branchID), typeof (EPExpenseClaim.taxZoneID)})]
  public PXSelect<EPExpenseClaim, Where<EPExpenseClaim.refNbr, Equal<Current<EPExpenseClaim.refNbr>>>> ExpenseClaimCurrent;
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.AP.APInvoice> APDocuments;
  [PXImport(typeof (EPExpenseClaim))]
  [PXViewName("Expense Claim Details")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (EPExpenseClaimDetails.curyTaxTotal)})]
  public PXSelect<EPExpenseClaimDetails, Where<EPExpenseClaimDetails.refNbr, Equal<Current<EPExpenseClaim.refNbr>>>, OrderBy<Asc<EPExpenseClaimDetails.submitedDate, Asc<EPExpenseClaimDetails.createdDateTime, Asc<EPExpenseClaimDetails.claimDetailID>>>>> ExpenseClaimDetails;
  public PXSelect<EPExpenseClaimDetails, Where<EPExpenseClaimDetails.claimDetailID, Equal<Current<EPExpenseClaimDetails.claimDetailID>>>> ExpenseClaimDetailsCurrent;
  public PXSelect<EPExpenseClaimDetails, Where<EPExpenseClaimDetails.refNbr, Equal<Current<EPExpenseClaim.refNbr>>, And<EPExpenseClaimDetails.paidWith, NotEqual<EPExpenseClaimDetails.paidWith.cash>>>> CardReceipts;
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<EPExpenseClaim.curyInfoID>>>> currencyinfo;
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<EPExpenseClaimDetails.curyInfoID>>>> CurrencyInfoReceipt;
  public PXSetup<EPSetup> epsetup;
  public PXSetup<APSetup> apsetup;
  [PXCopyPasteHiddenView]
  public PXSetup<GLSetup> glsetup;
  [PXViewName("Employee")]
  public PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Optional<EPExpenseClaim.employeeID>>>> EPEmployee;
  public PXSetup<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Current<EPExpenseClaim.employeeID>>>> EPEmployeeSetup;
  [PXCopyPasteHiddenView]
  public PXSelect<EPTax> Tax_Rows_Internal;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<EPTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<EPTaxTran.taxID>>>, Where<EPTaxTran.claimDetailID, Equal<Optional<EPExpenseClaimDetails.claimDetailID>>>> Tax_Rows;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<EPTaxAggregate, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<EPTaxAggregate.taxID>>>, Where<EPTaxAggregate.refNbr, Equal<Current<EPExpenseClaim.refNbr>>>> Taxes;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<EPTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<EPTaxTran.taxID>>>, Where<EPTaxTran.refNbr, Equal<Current<EPExpenseClaim.refNbr>>, And<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.use>>>> UseTaxes;
  [PXCopyPasteHiddenView]
  [PXViewName("Approval")]
  public ExpenseClaimEntry.ExpenceClaimApproval<EPExpenseClaim> Approval;
  [PXReadOnlyView]
  public PXSelect<ExpenseClaimEntry.EPExpenseClaimDetailsForSubmit, Where<True, Equal<False>>, OrderBy<Asc<EPExpenseClaimDetails.expenseDate>>> ReceiptsForSubmit;
  public PXFilter<ExpenseClaimEntry.EPCustomerUpdateAsk> CustomerUpdateAsk;
  public PXFilter<ExpenseClaimEntry.TaxZoneUpdateAsk> TaxZoneUpdateAskView;
  private bool CurrencyInfoRowUpdatedScope;
  private bool ignoreDetailReadOnly;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  [PXUIField(DisplayName = "View Taxes")]
  [PXButton]
  protected virtual IEnumerable viewTaxes(PXAdapter adapter)
  {
    int num = (int) this.Tax_Rows.AskExt(true);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Ok")]
  [PXButton]
  protected virtual IEnumerable commitTaxes(PXAdapter adapter)
  {
    this.ExpenseClaimDetails.Update(this.ExpenseClaimDetails.Current);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Submit")]
  [PXButton]
  protected virtual void Submit() => this.SubmitClaim(this.ExpenseClaim.Current);

  protected virtual void SubmitClaim(EPExpenseClaim claim)
  {
    if (claim == null)
      return;
    bool flag1 = false;
    foreach (PXResult<EPExpenseClaimDetails> pxResult in this.ExpenseClaimDetails.Select())
    {
      EPExpenseClaimDetails row = (EPExpenseClaimDetails) pxResult;
      bool? nullable = row.Rejected;
      bool flag2 = false;
      if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
      {
        nullable = row.Hold;
        bool flag3 = false;
        if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
        {
          nullable = row.Approved;
          bool flag4 = false;
          if (nullable.GetValueOrDefault() == flag4 & nullable.HasValue)
          {
            flag1 = true;
            this.ExpenseClaimDetails.Cache.RaiseExceptionHandling<EPExpenseClaimDetails.tranDesc>((object) row, (object) row.TranDesc, (Exception) new PXSetPropertyException("This receipt must be approved.", PXErrorLevel.RowError));
            continue;
          }
        }
      }
      nullable = row.Rejected;
      if (nullable.GetValueOrDefault())
      {
        flag1 = true;
        this.ExpenseClaimDetails.Cache.RaiseExceptionHandling<EPExpenseClaimDetails.tranDesc>((object) row, (object) row.TranDesc, (Exception) new PXSetPropertyException("This receipt must be removed from the claim.", PXErrorLevel.RowError));
      }
      else
      {
        nullable = row.Hold;
        if (nullable.GetValueOrDefault())
        {
          flag1 = true;
          this.ExpenseClaimDetails.Cache.RaiseExceptionHandling<EPExpenseClaimDetails.tranDesc>((object) row, (object) row.TranDesc, (Exception) new PXSetPropertyException("This receipt must be taken off hold.", PXErrorLevel.RowError));
        }
      }
    }
    if (flag1)
      throw new PXException("All receipts included in the claim must be in the Open status.");
    claim.Hold = new bool?(false);
    this.ExpenseClaim.Update(claim);
    this.Save.Press();
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold")]
  protected virtual IEnumerable Edit(PXAdapter adapter) => adapter.Get();

  [PXUIField(DisplayName = "Release", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXButton]
  [PXActionRestriction(typeof (Where<Current<APSetup.migrationMode>, Equal<True>>), "Migration mode is activated in the Accounts Payable module.")]
  public virtual IEnumerable Release(PXAdapter adapter)
  {
    foreach (object obj in adapter.Get())
    {
      this.TryValidateRequireUniqueVendorRefWhenCardInvolved();
      EPExpenseClaim claim = obj as EPExpenseClaim;
      if (claim == null)
        claim = (EPExpenseClaim) (PXResult<EPExpenseClaim>) obj;
      bool? nullable = claim.Approved;
      bool flag = false;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
      {
        nullable = claim.Released;
        if (!nullable.GetValueOrDefault())
        {
          this.Save.Press();
          PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => EPDocumentRelease.ReleaseDoc(claim)));
          continue;
        }
      }
      throw new PXException("Document Status is invalid for processing.");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Print", MapEnableRights = PXCacheRights.Select)]
  [PXButton(SpecialType = PXSpecialButtonType.Report)]
  protected virtual IEnumerable ExpenseClaimPrint(PXAdapter adapter, string reportID = null)
  {
    if (this.ExpenseClaim.Current != null)
      throw new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["RefNbr"] = this.ExpenseClaim.Current.RefNbr
      }, "EP612000", "Print Expense Claim");
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Add Receipts", MapEnableRights = PXCacheRights.Select)]
  [PXButton(SpecialType = PXSpecialButtonType.Report, Tooltip = "Add Receipts")]
  protected virtual IEnumerable ShowSubmitReceipt(PXAdapter adapter)
  {
    if (this.ReceiptsForSubmit.AskExt(true) == WebDialogResult.OK)
      return this.SubmitReceipt(adapter);
    this.ClearReceiptsForSubmit();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Add", MapEnableRights = PXCacheRights.Select)]
  [PXButton(SpecialType = PXSpecialButtonType.Report)]
  protected virtual IEnumerable SubmitReceipt(PXAdapter adapter)
  {
    if (this.ExpenseClaim.Current != null)
    {
      foreach (PXResult<ExpenseClaimEntry.EPExpenseClaimDetailsForSubmit> pxResult in this.ReceiptsForSubmit.Select())
      {
        EPExpenseClaimDetails expenseClaimDetails = (EPExpenseClaimDetails) (ExpenseClaimEntry.EPExpenseClaimDetailsForSubmit) pxResult;
        if (expenseClaimDetails.Selected.GetValueOrDefault())
        {
          expenseClaimDetails.Selected = new bool?(false);
          EPExpenseClaimDetails copy = (EPExpenseClaimDetails) this.ExpenseClaimDetails.Cache.CreateCopy((object) (this.ExpenseClaimDetails.Locate(new EPExpenseClaimDetails()
          {
            ClaimDetailCD = expenseClaimDetails.ClaimDetailCD,
            ClaimDetailID = expenseClaimDetails.ClaimDetailID
          }) ?? expenseClaimDetails));
          this.FindImplementation<ExpenseClaimEntry.ExpenseClaimEntryReceiptExt>().SubmitReceiptExt(this.ExpenseClaim.Cache, this.ExpenseClaimDetails.Cache, this.ExpenseClaim.Current, copy);
        }
      }
    }
    this.ClearReceiptsForSubmit();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Close", MapEnableRights = PXCacheRights.Select)]
  [PXButton(SpecialType = PXSpecialButtonType.Report)]
  protected virtual IEnumerable CancelSubmitReceipt(PXAdapter adapter)
  {
    this.ClearReceiptsForSubmit();
    return adapter.Get();
  }

  private void ClearReceiptsForSubmit()
  {
    this.ReceiptsForSubmit.Cache.Clear();
    this.ReceiptsForSubmit.Cache.ClearQueryCache();
    this.ReceiptsForSubmit.View.Clear();
  }

  [PXUIField(DisplayName = "Add New Receipt")]
  [PXButton(SpecialType = PXSpecialButtonType.Report, Tooltip = "Add New Receipt")]
  protected virtual void CreateNew()
  {
    ExpenseClaimDetailEntry instance = PXGraph.CreateInstance<ExpenseClaimDetailEntry>();
    instance.Clear(PXClearOption.ClearAll);
    EPExpenseClaimDetails expenseClaimDetails = (EPExpenseClaimDetails) instance.ClaimDetails.Cache.CreateInstance();
    EPExpenseClaim current = this.ExpenseClaim.Current;
    if (current == null || !current.EmployeeID.HasValue)
      return;
    this.Save.Press();
    expenseClaimDetails.RefNbr = current.RefNbr;
    expenseClaimDetails.EmployeeID = current.EmployeeID;
    expenseClaimDetails.BranchID = current.BranchID;
    expenseClaimDetails.CustomerID = current.CustomerID;
    expenseClaimDetails.CustomerLocationID = current.CustomerLocationID;
    bool flag = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.approvalWorkflow>() && this.epsetup.Current.ClaimDetailsAssignmentMapID.HasValue;
    expenseClaimDetails.Hold = new bool?(flag);
    expenseClaimDetails.Approved = new bool?(!flag);
    PXFieldVerifying handler = (PXFieldVerifying) ((sender, args) => args.Cancel = true);
    try
    {
      instance.FieldVerifying.AddHandler<EPExpenseClaimDetails.refNbr>(handler);
      expenseClaimDetails = instance.ClaimDetails.Insert(expenseClaimDetails);
    }
    finally
    {
      instance.FieldVerifying.RemoveHandler<EPExpenseClaimDetails.refNbr>(handler);
    }
    expenseClaimDetails.TaxZoneID = current.TaxZoneID;
    expenseClaimDetails.TaxCalcMode = current.TaxCalcMode;
    EPExpenseClaimDetails row = instance.ClaimDetails.Update(expenseClaimDetails);
    instance.ClaimDetails.SetValueExt<EPExpenseClaimDetails.refNbr>(row, (object) this.ExpenseClaim.Current.RefNbr);
    instance.ClaimDetails.Update(row);
    instance.CurrentClaim.Update(this.ExpenseClaim.Current);
    PXRedirectHelper.TryRedirect((PXGraph) instance, PXRedirectHelper.WindowMode.InlineWindow);
  }

  [PXUIField(DisplayName = "Edit")]
  [PXEditDetailButton]
  protected virtual void EditDetail()
  {
    if (this.ExpenseClaim.Current == null || this.ExpenseClaimDetails.Current == null)
      return;
    this.Save.Press();
    PXRedirectHelper.TryRedirect(this.ExpenseClaimDetails.Cache, (object) this.ExpenseClaimDetails.Current, "Open receipt", PXRedirectHelper.WindowMode.InlineWindow);
  }

  [PXButton]
  protected virtual void ViewUnsubmitReceipt()
  {
    if (this.ReceiptsForSubmit.Current == null)
      return;
    PXRedirectHelper.TryRedirect((PXGraph) PXGraph.CreateInstance<ExpenseClaimDetailEntry>(), (object) this.ReceiptsForSubmit.Current, PXRedirectHelper.WindowMode.NewWindow);
  }

  [PXUIField(DisplayName = "Change")]
  [PXButton]
  protected virtual IEnumerable ChangeOk(PXAdapter adapter)
  {
    if (this.CustomerUpdateAsk.Current.CustomerUpdateAnswer != "N")
    {
      foreach (PXResult<EPExpenseClaimDetails> pxResult in this.ExpenseClaimDetails.Select().AsEnumerable<PXResult<EPExpenseClaimDetails>>().Where<PXResult<EPExpenseClaimDetails>>((Func<PXResult<EPExpenseClaimDetails>, bool>) (_ => !((EPExpenseClaimDetails) _).ContractID.HasValue || ProjectDefaultAttribute.IsNonProject(((EPExpenseClaimDetails) _).ContractID))))
      {
        EPExpenseClaimDetails data = (EPExpenseClaimDetails) pxResult;
        if (!(this.CustomerUpdateAsk.Current.CustomerUpdateAnswer == "A"))
        {
          int? customerId = data.CustomerID;
          int? oldCustomerId = this.CustomerUpdateAsk.Current.OldCustomerID;
          if (!(customerId.GetValueOrDefault() == oldCustomerId.GetValueOrDefault() & customerId.HasValue == oldCustomerId.HasValue))
            continue;
        }
        this.ExpenseClaimDetails.Cache.SetValueExt<EPExpenseClaimDetails.customerID>((object) data, (object) this.CustomerUpdateAsk.Current.NewCustomerID);
        this.ExpenseClaimDetails.Cache.Update((object) data);
      }
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Cancel")]
  [PXButton]
  protected virtual IEnumerable ChangeCancel(PXAdapter adapter)
  {
    this.ExpenseClaim.Cache.SetValueExt<EPExpenseClaim.customerID>((object) this.ExpenseClaim.Current, (object) this.CustomerUpdateAsk.Current.OldCustomerID);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Yes")]
  [PXButton]
  protected virtual IEnumerable saveTaxZone(PXAdapter adapter)
  {
    this.EPEmployee.Cache.SetValue<PX.Objects.EP.EPEmployee.receiptAndClaimTaxZoneID>((object) this.EPEmployee.Current, (object) this.ExpenseClaim.Current.TaxZoneID);
    this.EPEmployee.Update(this.EPEmployee.Current);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Project", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable ViewProject(PXAdapter adapter)
  {
    if (this.ExpenseClaimDetails.Current != null)
    {
      if (ProjectDefaultAttribute.IsProject((PXGraph) this, this.ExpenseClaimDetails.Current.ContractID))
      {
        ProjectAccountingService.NavigateToProjectScreen(this.ExpenseClaimDetails.Current.ContractID);
      }
      else
      {
        ContractMaint instance = PXGraph.CreateInstance<ContractMaint>();
        instance.Contracts.Current = (PX.Objects.CT.Contract) instance.Contracts.Search<PX.Objects.CT.Contract.contractID>((object) this.ExpenseClaimDetails.Current.ContractID);
        ProjectAccountingService.NavigateToScreen((PXGraph) instance, "View Project");
      }
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Invoice", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable ViewInvoice(PXAdapter adapter)
  {
    if (this.APDocuments.Current != null)
      RedirectionToOrigDoc.TryRedirect(this.APDocuments.Current.DocType, this.APDocuments.Current.RefNbr, "AP", true);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Invoice", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable ViewAPInvoice(PXAdapter adapter)
  {
    if (this.ExpenseClaimDetails.Current != null)
      RedirectionToOrigDoc.TryRedirect(this.ExpenseClaimDetails.Current.APDocType, this.ExpenseClaimDetails.Current.APRefNbr, "AP", true);
    return adapter.Get();
  }

  public ExpenseClaimEntry.ExpenseClaimEntryReceiptExt ReceiptEntryExt
  {
    get => this.FindImplementation<ExpenseClaimEntry.ExpenseClaimEntryReceiptExt>();
  }

  protected virtual IEnumerable receiptsforsubmit()
  {
    ExpenseClaimEntry graph = this;
    PXSelectBase<ExpenseClaimEntry.EPExpenseClaimDetailsForSubmit> pxSelectBase = (PXSelectBase<ExpenseClaimEntry.EPExpenseClaimDetailsForSubmit>) new PXSelect<ExpenseClaimEntry.EPExpenseClaimDetailsForSubmit, Where2<Where<ExpenseClaimEntry.EPExpenseClaimDetailsForSubmit.refNbr, IsNull, Or<ExpenseClaimEntry.EPExpenseClaimDetailsForSubmit.refNbr, Equal<Current<EPExpenseClaim.refNbr>>>>, And<Where<EPExpenseClaimDetails.rejected, NotEqual<True>, And<EPExpenseClaimDetails.employeeID, Equal<Current<EPExpenseClaim.employeeID>>>>>>>((PXGraph) graph);
    if (!graph.epsetup.Current.AllowMixedTaxSettingInClaims.GetValueOrDefault())
      pxSelectBase.WhereAnd<Where<EPExpenseClaimDetails.taxCalcMode, Equal<Current2<EPExpenseClaim.taxCalcMode>>, And<Where<EPExpenseClaimDetails.taxZoneID, Equal<Current2<EPExpenseClaim.taxZoneID>>, Or<Where<EPExpenseClaimDetails.taxZoneID, IsNull, And<Current2<EPExpenseClaim.taxZoneID>, IsNull>>>>>>>();
    HashSet<int?> receiptsInClaim = new HashSet<int?>();
    foreach (PXResult<EPExpenseClaimDetails> pxResult in graph.ExpenseClaimDetails.Select())
      receiptsInClaim.Add(((EPExpenseClaimDetails) pxResult).ClaimDetailID);
    foreach (PXResult<ExpenseClaimEntry.EPExpenseClaimDetailsForSubmit> pxResult in pxSelectBase.Select())
    {
      ExpenseClaimEntry.EPExpenseClaimDetailsForSubmit detailsForSubmit = (ExpenseClaimEntry.EPExpenseClaimDetailsForSubmit) pxResult;
      if (!receiptsInClaim.Contains(detailsForSubmit.ClaimDetailID))
        yield return (object) detailsForSubmit;
    }
  }

  protected virtual IEnumerable apdocuments()
  {
    return this.ExpenseClaimDetails.SelectSingle() != null ? (IEnumerable) PXSelectBase<PX.Objects.AP.APInvoice, PXSelectJoinGroupBy<PX.Objects.AP.APInvoice, InnerJoin<EPExpenseClaimDetails, On<PX.Objects.AP.APInvoice.docType, Equal<EPExpenseClaimDetails.aPDocType>, And<PX.Objects.AP.APInvoice.refNbr, Equal<EPExpenseClaimDetails.aPRefNbr>>>>, Where<EPExpenseClaimDetails.refNbr, Equal<Current<EPExpenseClaim.refNbr>>>, Aggregate<GroupBy<PX.Objects.AP.APInvoice.docType, GroupBy<PX.Objects.AP.APInvoice.refNbr>>>>.Config>.Select((PXGraph) this) : (IEnumerable) PXSelectBase<PX.Objects.AP.APInvoice, PXSelectReadonly<PX.Objects.AP.APInvoice, Where<PX.Objects.AP.APRegister.origModule, Equal<BatchModule.moduleEP>, And<PX.Objects.AP.APInvoice.origRefNbr, Equal<Current<EPExpenseClaim.refNbr>>, And<PX.Objects.AP.APInvoice.origDocType, Equal<EPExpenseClaim.docType>>>>>.Config>.Select((PXGraph) this);
  }

  protected virtual IEnumerable taxes()
  {
    ExpenseClaimEntry expenseClaimEntry = this;
    if (expenseClaimEntry.ExpenseClaim.Current != null)
    {
      ExpenseClaimEntry graph1 = expenseClaimEntry;
      object[] objArray1 = new object[1]
      {
        (object) expenseClaimEntry.ExpenseClaim.Current.RefNbr
      };
      foreach (IGrouping<string, PXResult<EPTaxTran, PX.Objects.TX.Tax>> grouping in PXSelectBase<EPTaxTran, PXSelectJoin<EPTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<EPTaxTran.taxID>>>, Where<EPTaxTran.refNbr, Equal<Required<EPExpenseClaim.refNbr>>>>.Config>.Select((PXGraph) graph1, objArray1).AsEnumerable<PXResult<EPTaxTran>>().Select<PXResult<EPTaxTran>, PXResult<EPTaxTran, PX.Objects.TX.Tax>>((Func<PXResult<EPTaxTran>, PXResult<EPTaxTran, PX.Objects.TX.Tax>>) (_ => (PXResult<EPTaxTran, PX.Objects.TX.Tax>) _)).GroupBy<PXResult<EPTaxTran, PX.Objects.TX.Tax>, string>((Func<PXResult<EPTaxTran, PX.Objects.TX.Tax>, string>) (_ => ((EPTaxTran) _).TaxID)))
      {
        PX.Objects.TX.Tax i1 = (PX.Objects.TX.Tax) null;
        EPTaxAggregate epTaxAggregate1 = new EPTaxAggregate();
        epTaxAggregate1.RefNbr = expenseClaimEntry.ExpenseClaim.Current.RefNbr;
        epTaxAggregate1.CuryTaxableAmt = new Decimal?(0M);
        epTaxAggregate1.CuryTaxAmt = new Decimal?(0M);
        epTaxAggregate1.CuryExpenseAmt = new Decimal?(0M);
        epTaxAggregate1.TaxableAmt = new Decimal?(0M);
        epTaxAggregate1.TaxAmt = new Decimal?(0M);
        epTaxAggregate1.ExpenseAmt = new Decimal?(0M);
        epTaxAggregate1.CuryInfoID = expenseClaimEntry.ExpenseClaim.Current.CuryInfoID;
        EPTaxAggregate i0 = epTaxAggregate1;
        foreach (PXResult<EPTaxTran, PX.Objects.TX.Tax> pxResult in (IEnumerable<PXResult<EPTaxTran, PX.Objects.TX.Tax>>) grouping)
        {
          EPTaxTran epTaxTran = (EPTaxTran) pxResult;
          i1 = (PX.Objects.TX.Tax) pxResult;
          i0.TaxRate = epTaxTran.TaxRate;
          i0.TaxID = epTaxTran.TaxID;
          EPTaxAggregate epTaxAggregate2 = i0;
          Decimal? nullable1 = epTaxAggregate2.CuryTaxableAmt;
          Decimal? nullable2 = epTaxTran.ClaimCuryTaxableAmt;
          epTaxAggregate2.CuryTaxableAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
          EPTaxAggregate epTaxAggregate3 = i0;
          nullable2 = epTaxAggregate3.CuryTaxAmt;
          nullable1 = epTaxTran.ClaimCuryTaxAmt;
          epTaxAggregate3.CuryTaxAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
          EPTaxAggregate epTaxAggregate4 = i0;
          nullable1 = epTaxAggregate4.CuryExpenseAmt;
          nullable2 = epTaxTran.ClaimCuryExpenseAmt;
          epTaxAggregate4.CuryExpenseAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
          EPTaxAggregate epTaxAggregate5 = i0;
          nullable2 = epTaxAggregate5.TaxableAmt;
          nullable1 = epTaxTran.TaxableAmt;
          epTaxAggregate5.TaxableAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
          EPTaxAggregate epTaxAggregate6 = i0;
          nullable1 = epTaxAggregate6.TaxAmt;
          nullable2 = epTaxTran.TaxAmt;
          epTaxAggregate6.TaxAmt = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
          EPTaxAggregate epTaxAggregate7 = i0;
          nullable2 = epTaxAggregate7.ExpenseAmt;
          nullable1 = epTaxTran.ExpenseAmt;
          epTaxAggregate7.ExpenseAmt = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
          i0.NonDeductibleTaxRate = epTaxTran.NonDeductibleTaxRate;
        }
        if (expenseClaimEntry.Taxes.Locate(i0) == null)
          expenseClaimEntry.Taxes.Cache.SetStatus((object) i0, PXEntryStatus.Held);
        yield return (object) new PXResult<EPTaxAggregate, PX.Objects.TX.Tax>(i0, i1);
      }
      ExpenseClaimEntry graph2 = expenseClaimEntry;
      object[] objArray2 = new object[1]
      {
        (object) expenseClaimEntry.ExpenseClaim.Current.RefNbr
      };
      foreach (PXResult<EPTaxAggregate, PX.Objects.TX.Tax> pxResult in PXSelectBase<EPTaxAggregate, PXSelectReadonly2<EPTaxAggregate, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<EPTaxAggregate.taxID>>>, Where<EPTaxAggregate.refNbr, Equal<Required<EPExpenseClaimDetails.refNbr>>>>.Config>.Select((PXGraph) graph2, objArray2))
      {
        EPTaxAggregate epTaxAggregate = (EPTaxAggregate) pxResult;
        if (expenseClaimEntry.Taxes.Locate(epTaxAggregate) == null)
          expenseClaimEntry.Taxes.Cache.SetStatus((object) epTaxAggregate, PXEntryStatus.Held);
        yield return (object) pxResult;
      }
    }
  }

  public ExpenseClaimEntry()
  {
    PXUIFieldAttribute.SetVisible<PX.Objects.AP.APInvoice.taxZoneID>(this.APDocuments.Cache, (object) null, this.epsetup.Current.AllowMixedTaxSettingInClaims.GetValueOrDefault());
    PXUIFieldAttribute.SetVisible<PX.Objects.AP.APInvoice.taxCalcMode>(this.APDocuments.Cache, (object) null, this.epsetup.Current.AllowMixedTaxSettingInClaims.GetValueOrDefault());
    if (this.epsetup.Current == null)
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (EPSetup), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Time & Expenses Preferences")
      });
    PXUIFieldAttribute.SetVisible<EPExpenseClaimDetails.contractID>(this.ExpenseClaimDetails.Cache, (object) null, PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.contractManagement>() || PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.projectAccounting>());
    this.Taxes.Cache.SetAllEditPermissions(false);
    PXUIFieldAttribute.SetEnabled(this.APDocuments.Cache, (string) null, false);
  }

  public override void Persist()
  {
    List<EPExpenseClaim> epExpenseClaimList = (List<EPExpenseClaim>) null;
    if (!this.epsetup.Current.HoldEntry.GetValueOrDefault())
    {
      epExpenseClaimList = new List<EPExpenseClaim>();
      foreach (EPExpenseClaim epExpenseClaim in this.ExpenseClaim.Cache.Inserted)
        epExpenseClaimList.Add(epExpenseClaim);
    }
    base.Persist();
    if (epExpenseClaimList != null)
    {
      foreach (EPExpenseClaim claim in epExpenseClaimList)
      {
        if (this.ExpenseClaim.Cache.GetStatus((object) claim) != PXEntryStatus.Inserted)
        {
          this.SubmitClaim(claim);
          this.RaiseSubmitEvent(claim);
        }
      }
      base.Persist();
    }
    this.ClearReceiptsForSubmit();
  }

  protected virtual void RaiseSubmitEvent(EPExpenseClaim claim)
  {
    PXEntityEventBase<EPExpenseClaim>.Container<EPExpenseClaim.Events>.Select((Expression<Func<EPExpenseClaim.Events, PXEntityEvent<EPExpenseClaim>>>) (e => e.Submit)).FireOn((PXGraph) this, claim);
  }

  protected virtual void CurrencyInfo_CuryID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>())
      return;
    PX.Objects.EP.EPEmployee epEmployee = (PX.Objects.EP.EPEmployee) this.EPEmployee.Select();
    if (epEmployee == null || string.IsNullOrEmpty(epEmployee.CuryID))
      return;
    e.NewValue = (object) epEmployee.CuryID;
    e.Cancel = true;
  }

  protected virtual void CurrencyInfo_CuryRateTypeID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>())
      return;
    PX.Objects.EP.EPEmployee epEmployee = (PX.Objects.EP.EPEmployee) this.EPEmployee.Select();
    if (epEmployee == null || string.IsNullOrEmpty(epEmployee.CuryRateTypeID))
      return;
    e.NewValue = (object) epEmployee.CuryRateTypeID;
    e.Cancel = true;
  }

  protected virtual void CurrencyInfo_CuryEffDate_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (this.ExpenseClaim.Cache.Current == null)
      return;
    e.NewValue = (object) ((EPExpenseClaim) this.ExpenseClaim.Cache.Current).DocDate;
    e.Cancel = true;
  }

  protected virtual void CurrencyInfo_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CM.CurrencyInfo row))
      return;
    bool isEnabled = row.AllowUpdate(this.ExpenseClaimDetails.Cache);
    PX.Objects.EP.EPEmployee epEmployee = (PX.Objects.EP.EPEmployee) this.EPEmployee.Select();
    if (this.ExpenseClaim.Current != null && epEmployee != null && !epEmployee.AllowOverrideRate.Value)
      isEnabled = false;
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyRateTypeID>(cache, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyEffDate>(cache, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.sampleCuryRate>(cache, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.sampleRecipRate>(cache, (object) row, isEnabled);
  }

  protected virtual void CurrencyInfo_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    long? curyInfoId1 = this.ExpenseClaim.Current.CuryInfoID;
    long? curyInfoId2 = (e.Row as PX.Objects.CM.CurrencyInfo).CuryInfoID;
    if (!(curyInfoId1.GetValueOrDefault() == curyInfoId2.GetValueOrDefault() & curyInfoId1.HasValue == curyInfoId2.HasValue))
      return;
    PX.Objects.CM.CurrencyInfo currencyInfo1 = (PX.Objects.CM.CurrencyInfo) PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<EPExpenseClaim.curyInfoID>>>>.Config>.Select((PXGraph) this, (object) this.ExpenseClaim.Current.CuryInfoID);
    PX.Objects.CM.CurrencyInfo oldRow = e.OldRow as PX.Objects.CM.CurrencyInfo;
    foreach (PXResult<EPExpenseClaimDetails> pxResult in this.ExpenseClaimDetails.Select())
    {
      EPExpenseClaimDetails expenseClaimDetails = (EPExpenseClaimDetails) pxResult;
      PX.Objects.CM.CurrencyInfo currencyInfo2 = (PX.Objects.CM.CurrencyInfo) PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<EPExpenseClaimDetails.curyInfoID>>>>.Config>.Select((PXGraph) this, (object) expenseClaimDetails.CuryInfoID);
      if (expenseClaimDetails.CreatedFromClaim.GetValueOrDefault() && oldRow.CuryID == currencyInfo2.CuryID)
      {
        Decimal? curyRate1 = oldRow.CuryRate;
        Decimal? curyRate2 = currencyInfo2.CuryRate;
        if (curyRate1.GetValueOrDefault() == curyRate2.GetValueOrDefault() & curyRate1.HasValue == curyRate2.HasValue)
        {
          DateTime? curyEffDate1 = oldRow.CuryEffDate;
          DateTime? curyEffDate2 = currencyInfo2.CuryEffDate;
          if ((curyEffDate1.HasValue == curyEffDate2.HasValue ? (curyEffDate1.HasValue ? (curyEffDate1.GetValueOrDefault() == curyEffDate2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && oldRow.CuryRateTypeID == currencyInfo2.CuryRateTypeID)
          {
            if (!this.CurrencyInfoRowUpdatedScope)
            {
              try
              {
                this.CurrencyInfoRowUpdatedScope = true;
                PX.Objects.CM.CurrencyInfo copy = (PX.Objects.CM.CurrencyInfo) this.currencyinfo.Cache.CreateCopy((object) currencyInfo1);
                copy.CuryInfoID = currencyInfo2.CuryInfoID;
                this.currencyinfo.Cache.Update((object) copy);
                this.CurrencyInfoRowUpdatedScope = true;
                CurrencyInfoAttribute.SetEffectiveDate<EPExpenseClaim.docDate>(this.ExpenseClaim.Cache, (object) this.ExpenseClaim.Current, typeof (EPExpenseClaimDetails.cardCuryInfoID));
                continue;
              }
              finally
              {
                this.CurrencyInfoRowUpdatedScope = false;
              }
            }
          }
        }
      }
      EPExpenseClaimDetails copy1 = (EPExpenseClaimDetails) this.ExpenseClaimDetails.Cache.CreateCopy((object) expenseClaimDetails);
      this.ReceiptEntryExt.RecalcAmountInClaimCury(expenseClaimDetails);
      this.ExpenseClaimDetails.Cache.RaiseRowUpdated((object) expenseClaimDetails, (object) copy1);
    }
  }

  protected virtual void EPExpenseClaim_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is EPExpenseClaim row1))
      return;
    this.ExpenseClaimDetails.Cache.SetAllEditPermissions(true);
    this.Tax_Rows.Cache.SetAllEditPermissions(true);
    PXUIFieldAttribute.SetVisible<EPExpenseClaim.curyID>(cache, (object) row1, PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>());
    PX.Objects.EP.EPEmployee epEmployee = (PX.Objects.EP.EPEmployee) this.EPEmployee.Select();
    bool? nullable1;
    int? nullable2;
    if (row1.Released.GetValueOrDefault())
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) row1, false);
      cache.AllowDelete = false;
      cache.AllowUpdate = false;
      this.ExpenseClaimDetails.Cache.SetAllEditPermissions(false);
      this.Tax_Rows.Cache.SetAllEditPermissions(false);
      this.release.SetEnabled(false);
      PXUIFieldAttribute.SetEnabled<EPExpenseClaim.refNbr>(cache, (object) row1, true);
      this.editDetail.SetEnabled(true);
    }
    else
    {
      bool isEnabled1 = true;
      if (epEmployee != null)
      {
        nullable1 = epEmployee.AllowOverrideCury;
        if (!nullable1.GetValueOrDefault())
        {
          isEnabled1 = false;
          goto label_8;
        }
      }
      if (this.CardReceipts.SelectSingle() != null)
        isEnabled1 = false;
label_8:
      PXUIFieldAttribute.SetEnabled(cache, (object) row1, true);
      PXUIFieldAttribute.SetEnabled<EPExpenseClaim.status>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<EPExpenseClaim.approverID>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<EPExpenseClaim.workgroupID>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<EPExpenseClaim.curyDocBal>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<EPExpenseClaim.curyVatExemptTotal>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<EPExpenseClaim.curyVatTaxableTotal>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<EPExpenseClaim.curyID>(cache, (object) row1, isEnabled1);
      nullable1 = row1.Hold;
      if (!nullable1.GetValueOrDefault())
      {
        PXUIFieldAttribute.SetEnabled(cache, (object) row1, false);
        PXUIFieldAttribute.SetEnabled<EPExpenseClaim.refNbr>(cache, (object) row1, true);
        PXUIFieldAttribute.SetEnabled<EPExpenseClaim.docDesc>(cache, (object) row1, cache.GetStatus(e.Row) == PXEntryStatus.Inserted);
        this.ExpenseClaimDetails.Cache.AllowInsert = false;
        this.ExpenseClaimDetails.Cache.AllowDelete = false;
        this.ExpenseClaimDetails.Cache.AllowUpdate = this.ignoreDetailReadOnly;
      }
      PXCache cache1 = cache;
      object row2 = e.Row;
      nullable1 = row1.Released;
      int num1 = !nullable1.GetValueOrDefault() ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<EPExpenseClaim.hold>(cache1, row2, num1 != 0);
      bool flag = row1.Status == "O" && this.Approval.IsApprover(row1);
      PXUIFieldAttribute.SetEnabled<EPExpenseClaim.finPeriodID>(cache, e.Row, flag || row1.Status == "A");
      if (flag || row1.Status == "A")
      {
        nullable1 = row1.Released;
        if (!nullable1.GetValueOrDefault())
        {
          OpenPeriodAttribute.SetValidatePeriod<EPExpenseClaim.finPeriodID>(cache, e.Row, PeriodValidation.DefaultSelectUpdate);
          goto label_14;
        }
      }
      OpenPeriodAttribute.SetValidatePeriod<EPExpenseClaim.finPeriodID>(cache, e.Row, PeriodValidation.Nothing);
label_14:
      PXUIFieldAttribute.SetEnabled<EPExpenseClaim.approverID>(cache, (object) row1, flag && row1.Status == "O");
      PXUIFieldAttribute.SetEnabled<EPExpenseClaim.workgroupID>(cache, (object) row1, flag && row1.Status == "O");
      PXUIFieldAttribute.SetEnabled<EPExpenseClaim.approvedByID>(cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<EPExpenseClaim.approveDate>(cache, (object) row1, false);
      bool isEnabled2 = this.ExpenseClaimDetails.SelectSingle() == null;
      PXUIFieldAttribute.SetEnabled<EPExpenseClaim.employeeID>(cache, (object) row1, isEnabled2);
      cache.AllowDelete = true;
      cache.AllowUpdate = true;
      PXAction<EPExpenseClaim> release = this.release;
      nullable1 = row1.Approved;
      int num2 = nullable1.GetValueOrDefault() ? 1 : 0;
      release.SetEnabled(num2 != 0);
      if (row1.EmployeeID.HasValue)
      {
        nullable2 = row1.BranchID;
        if (nullable2.HasValue)
          goto label_17;
      }
      this.ExpenseClaimDetails.Cache.SetAllEditPermissions(false);
    }
label_17:
    PX.Objects.CM.CurrencyInfo currencyInfo = (PX.Objects.CM.CurrencyInfo) PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<EPExpenseClaim.curyInfoID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row1
    });
    if (currencyInfo != null && !currencyInfo.CuryPrecision.HasValue)
    {
      object newValue;
      this.Caches<PX.Objects.CM.CurrencyInfo>().RaiseFieldDefaulting<PX.Objects.CM.CurrencyInfo.curyPrecision>((object) currencyInfo, out newValue);
      currencyInfo.CuryPrecision = newValue as short?;
    }
    string message = PXUIFieldAttribute.GetError<PX.Objects.CM.CurrencyInfo.curyID>(this.currencyinfo.Cache, (object) currencyInfo);
    if (string.IsNullOrEmpty(message) && currencyInfo != null && !currencyInfo.CuryRate.HasValue)
      message = "Currency Rate is not defined.";
    if (string.IsNullOrEmpty(message))
      cache.RaiseExceptionHandling<EPExpenseClaimDetails.curyID>(e.Row, (object) null, (Exception) null);
    else
      cache.RaiseExceptionHandling<EPExpenseClaimDetails.curyID>(e.Row, (object) null, (Exception) new PXSetPropertyException(message, PXErrorLevel.Warning));
    Guid userId1 = this.Accessinfo.UserID;
    Guid? nullable3 = row1.CreatedByID;
    bool isEnabled = nullable3.HasValue && userId1 == nullable3.GetValueOrDefault();
    if (epEmployee != null)
    {
      if (!isEnabled)
      {
        Guid userId2 = this.Accessinfo.UserID;
        nullable3 = epEmployee.UserID;
        if ((nullable3.HasValue ? (userId2 == nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          isEnabled = true;
      }
      if (!isEnabled)
      {
        if ((EPWingmanForExpenses) PXSelectBase<EPWingmanForExpenses, PXSelectJoin<EPWingmanForExpenses, InnerJoin<PX.Objects.EP.EPEmployee, On<EPWingman.wingmanID, Equal<PX.Objects.EP.EPEmployee.bAccountID>>>, Where<EPWingman.employeeID, Equal<Required<EPWingman.employeeID>>, And<PX.Objects.EP.EPEmployee.userID, Equal<Required<PX.Objects.EP.EPEmployee.userID>>>>>.Config>.Select((PXGraph) this, (object) row1.EmployeeID, (object) this.Accessinfo.UserID) != null)
          isEnabled = true;
      }
    }
    bool isIncorrect = false;
    if (row1.TaxZoneID != null)
    {
      nullable1 = row1.Released;
      bool flag = false;
      if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
      {
        foreach (PXResult<EPExpenseClaimDetails> pxResult in this.ExpenseClaimDetails.Select())
        {
          nullable1 = ((EPExpenseClaimDetails) pxResult).LegacyReceipt;
          if (nullable1.GetValueOrDefault())
          {
            isIncorrect = true;
            break;
          }
        }
        if (!isIncorrect)
        {
          foreach (PXResult<EPTaxAggregate> pxResult in PXSelectBase<EPTaxAggregate, PXSelectReadonly<EPTaxAggregate, Where<EPTaxAggregate.refNbr, Equal<Required<EPExpenseClaim.refNbr>>>>.Config>.Select((PXGraph) this, (object) row1.RefNbr))
          {
            if ((EPTaxAggregate) pxResult != null)
            {
              isIncorrect = true;
              break;
            }
          }
        }
        if (isIncorrect)
        {
          this.ExpenseClaimDetails.Cache.SetAllEditPermissions(false);
          this.Tax_Rows.Cache.SetAllEditPermissions(false);
          cache.AllowUpdate = false;
        }
      }
    }
    UIState.RaiseOrHideError<EPExpenseClaim.refNbr>(cache, (object) row1, isIncorrect, "The expense claim cannot be edited due to an incompatible tax calculation model. Create the expense claim again from scratch to process it further.", PXErrorLevel.Warning);
    Numbering numbering = (Numbering) PXSelectBase<Numbering, PXSelect<Numbering, Where<Numbering.numberingID, Equal<Required<Numbering.numberingID>>>>.Config>.Select((PXGraph) this, (object) this.epsetup.Current.ReceiptNumberingID);
    PXAction<EPExpenseClaim> createNew = this.createNew;
    int num3;
    if (this.ExpenseClaimDetails.Cache.AllowInsert && cache.GetStatus((object) row1) != PXEntryStatus.Inserted)
    {
      if (numbering == null)
      {
        num3 = 0;
      }
      else
      {
        nullable1 = numbering.UserNumbering;
        bool flag = false;
        num3 = nullable1.GetValueOrDefault() == flag & nullable1.HasValue ? 1 : 0;
      }
    }
    else
      num3 = 0;
    createNew.SetEnabled(num3 != 0);
    this.editDetail.SetEnabled(cache.GetStatus((object) row1) != PXEntryStatus.Inserted);
    this.submit.SetEnabled(cache.GetStatus((object) row1) != PXEntryStatus.Inserted);
    this.expenseClaimPrint.SetEnabled(cache.GetStatus((object) row1) != PXEntryStatus.Inserted);
    this.submitReceipt.SetEnabled(this.ExpenseClaimDetails.Cache.AllowInsert);
    this.showSubmitReceipt.SetEnabled(this.ExpenseClaimDetails.Cache.AllowInsert);
    this.edit.SetEnabled(isEnabled);
    this.ViewTaxes.SetEnabled(true);
    PXCache cach = this.Caches[typeof (EPExpenseClaimDetails)];
    nullable2 = this.epsetup.Current.NonTaxableTipItem;
    int num4 = nullable2.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetVisible<EPExpenseClaimDetails.curyTipAmt>(cach, (object) null, num4 != 0);
    if (this.UseTaxes.Select().Count != 0)
      cache.RaiseExceptionHandling<EPExpenseClaim.curyTaxTotal>((object) row1, (object) row1.CuryTaxTotal, (Exception) new PXSetPropertyException("Use Tax is excluded from Tax Total.", PXErrorLevel.Warning));
    else
      cache.RaiseExceptionHandling<EPExpenseClaim.curyTaxTotal>((object) row1, (object) row1.CuryTaxTotal, (Exception) null);
  }

  protected virtual void EPExpenseClaim_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    CurrencyInfoAttribute.SetDefaults<EPExpenseClaim.curyInfoID>(cache, e.Row);
  }

  protected virtual void EPExpenseClaim_CustomerID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if ((e.Row is EPExpenseClaim row ? (!row.CustomerID.HasValue ? 1 : 0) : 1) == 0)
      return;
    cache.SetValueExt<EPExpenseClaim.customerLocationID>((object) row, (object) null);
  }

  protected virtual void EPExpenseClaim_LocationID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    cache.SetDefaultExt<EPExpenseClaim.taxZoneID>(e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<EPExpenseClaim.approved> e)
  {
    EPExpenseClaim row = (EPExpenseClaim) e.Row;
    if (row == null)
      return;
    DateTime? nullable = new DateTime?();
    if (row.Approved.GetValueOrDefault())
      nullable = new DateTime?(PXTimeZoneInfo.Now);
    this.ExpenseClaim.Cache.SetValueExt<EPExpenseClaim.approveDate>((object) row, (object) nullable);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<EPExpenseClaim, EPExpenseClaim.docDate> e)
  {
    if (!(e.NewValue is DateTime newValue))
      return;
    int? employeeId = (int?) this.ExpenseClaim.Current?.EmployeeID;
    if (!employeeId.HasValue)
      return;
    int valueOrDefault = employeeId.GetValueOrDefault();
    this.ValidateFinancialPeriod(newValue, valueOrDefault);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<EPExpenseClaim, EPExpenseClaim.employeeID> e)
  {
    DateTime? docDate = (DateTime?) this.ExpenseClaim.Current?.DocDate;
    if (!docDate.HasValue)
      return;
    DateTime valueOrDefault = docDate.GetValueOrDefault();
    if (!(e.NewValue is int newValue))
      return;
    this.ValidateFinancialPeriod(valueOrDefault, newValue);
  }

  protected virtual void EPExpenseClaim_TaxZoneID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    EPExpenseClaim row = (EPExpenseClaim) e.Row;
    if (row == null)
      return;
    foreach (PXResult<EPExpenseClaimDetails> pxResult in this.ExpenseClaimDetails.Select())
    {
      EPExpenseClaimDetails expenseClaimDetails = (EPExpenseClaimDetails) pxResult;
      if (expenseClaimDetails.TaxZoneID != (string) e.NewValue)
      {
        bool? nullable = expenseClaimDetails.CreatedFromClaim;
        if (!nullable.GetValueOrDefault())
        {
          nullable = this.epsetup.Current.AllowMixedTaxSettingInClaims;
          string format = !nullable.GetValueOrDefault() ? "Cannot change the tax zone in the expense claim because expense receipts and the expense claim must have the same tax zone." : "To change the tax zone and the tax calculation mode in an expense receipt, use the Expense Receipt (EP301010) form.";
          cache.RaiseExceptionHandling<EPExpenseClaim.taxZoneID>((object) row, (object) row.TaxZoneID, (Exception) new PXSetPropertyException(format, new object[1]
          {
            (object) expenseClaimDetails.ClaimDetailID
          }));
          e.Cancel = true;
          e.NewValue = (object) row.TaxZoneID;
        }
      }
    }
  }

  protected virtual void EPExpenseClaim_TaxCalcMode_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    EPExpenseClaim row = (EPExpenseClaim) e.Row;
    if (row == null)
      return;
    foreach (PXResult<EPExpenseClaimDetails> pxResult in this.ExpenseClaimDetails.Select())
    {
      EPExpenseClaimDetails expenseClaimDetails = (EPExpenseClaimDetails) pxResult;
      if (expenseClaimDetails.TaxCalcMode != (string) e.NewValue)
      {
        bool? nullable = expenseClaimDetails.CreatedFromClaim;
        if (!nullable.GetValueOrDefault())
        {
          nullable = this.epsetup.Current.AllowMixedTaxSettingInClaims;
          string format = !nullable.GetValueOrDefault() ? "Cannot change the tax calculation mode in the expense claim because expense receipts and the expense claim must have the same tax calculation mode." : "To change the tax zone and the tax calculation mode in an expense receipt, use the Expense Receipt (EP301010) form.";
          cache.RaiseExceptionHandling<EPExpenseClaim.taxCalcMode>((object) row, (object) row.TaxCalcMode, (Exception) new PXSetPropertyException(format, new object[1]
          {
            (object) expenseClaimDetails.ClaimDetailID
          }));
          e.Cancel = true;
          e.NewValue = (object) row.TaxCalcMode;
        }
      }
    }
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.corpCardID> e)
  {
    EPExpenseClaimDetails row = (EPExpenseClaimDetails) e.Row;
    this.ReceiptEntryExt.VerifyClaimAndCorpCardCurrencies(e.NewValue is int? ? (int?) e.NewValue : CACorpCard.UK.Find((PXGraph) this, (string) e.NewValue)?.CorpCardID, this.ExpenseClaim.Current, (System.Action) (() =>
    {
      PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.corpCardID> fieldVerifying = e;
      object obj;
      if (!(e.NewValue is int?))
      {
        obj = e.NewValue;
      }
      else
      {
        CACorpCard caCorpCard = CACorpCard.PK.Find((PXGraph) this, (int?) e.NewValue);
        obj = caCorpCard != null ? (object) caCorpCard.CorpCardCD : (object) null;
      }
      fieldVerifying.NewValue = obj;
    }));
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<EPExpenseClaimDetails.paidWith> e)
  {
    EPExpenseClaimDetails row = (EPExpenseClaimDetails) e.Row;
    string newValue = (string) e.NewValue;
    this.ReceiptEntryExt.VerifyEmployeeAndClaimCurrenciesForCash(row, newValue, this.ExpenseClaim.Current);
    Decimal? valuePendingOrRow1 = (Decimal?) BqlHelper.GetValuePendingOrRow<EPExpenseClaimDetails.curyEmployeePart>(e.Cache, (object) row);
    this.ReceiptEntryExt.VerifyEmployeePartIsZeroForCorpCardReceipt(newValue, valuePendingOrRow1);
    Decimal? valuePendingOrRow2 = (Decimal?) BqlHelper.GetValuePendingOrRow<EPExpenseClaimDetails.curyExtCost>(e.Cache, (object) row);
    this.ReceiptEntryExt.VerifyIsPositiveForCorpCardReceipt(newValue, valuePendingOrRow2);
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<ExpenseClaimEntry.EPExpenseClaimDetailsForSubmit.selected> e)
  {
    ExpenseClaimEntry.EPExpenseClaimDetailsForSubmit row = (ExpenseClaimEntry.EPExpenseClaimDetailsForSubmit) e.Row;
    if (!((bool?) e.NewValue).GetValueOrDefault())
      return;
    this.ReceiptEntryExt.VerifyClaimAndCorpCardCurrencies(row.CorpCardID, this.ExpenseClaim.Current);
    this.ReceiptEntryExt.VerifyEmployeeAndClaimCurrenciesForCash((EPExpenseClaimDetails) row, row.PaidWith, this.ExpenseClaim.Current);
  }

  protected virtual void EPExpenseClaim_DocDate_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    CurrencyInfoAttribute.SetEffectiveDate<EPExpenseClaim.docDate>(cache, e);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPExpenseClaim, EPExpenseClaim.curyDocBal> e)
  {
    foreach (PXResult<EPApproval> pxResult in this.Approval.Select())
    {
      EPApproval epApproval = (EPApproval) pxResult;
      this.Approval.Cache.SetDefaultExt<EPApproval.curyTotalAmount>((object) epApproval);
      this.Approval.Cache.SetDefaultExt<EPApproval.totalAmount>((object) epApproval);
      this.Approval.Cache.MarkUpdated((object) epApproval);
    }
  }

  protected virtual void EPExpenseClaim_EmployeeID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    cache.SetDefaultExt<EPExpenseClaim.locationID>(e.Row);
    cache.SetDefaultExt<EPExpenseClaim.departmentID>(e.Row);
    cache.SetDefaultExt<EPExpenseClaim.branchID>(e.Row);
    if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>())
      return;
    PX.Objects.CM.CurrencyInfo data = CurrencyInfoAttribute.SetDefaults<EPExpenseClaim.curyInfoID>(cache, e.Row);
    string error = PXUIFieldAttribute.GetError<PX.Objects.CM.CurrencyInfo.curyEffDate>(this.currencyinfo.Cache, (object) data);
    if (!string.IsNullOrEmpty(error))
      cache.RaiseExceptionHandling<EPExpenseClaim.docDate>(e.Row, (object) ((EPExpenseClaim) e.Row).DocDate, (Exception) new PXSetPropertyException(error, PXErrorLevel.Warning));
    if (data == null)
      return;
    ((EPExpenseClaim) e.Row).CuryID = data.CuryID;
  }

  protected virtual void EPExpenseClaim_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is EPExpenseClaim row) || e.Operation == PXDBOperation.Delete)
      return;
    bool? hold = row.Hold;
    bool flag = false;
    if (!(hold.GetValueOrDefault() == flag & hold.HasValue))
      return;
    PX.Objects.CM.CurrencyInfo currencyInfo = (PX.Objects.CM.CurrencyInfo) this.currencyinfo.View.SelectSingleBound(new object[1]
    {
      e.Row
    });
    if (currencyInfo == null)
      return;
    Decimal? roundingLimit = CurrencyCollection.GetCurrency(currencyInfo.BaseCuryID).RoundingLimit;
    Decimal num = Math.Abs(row.TaxRoundDiff.GetValueOrDefault());
    if (roundingLimit.GetValueOrDefault() < num & roundingLimit.HasValue)
      throw new PXException("The amount to be posted to the rounding account ({1} {0}) exceeds the limit ({2} {0}) specified on the Currencies (CM202000) form.", new object[3]
      {
        (object) currencyInfo.BaseCuryID,
        (object) row.TaxRoundDiff,
        (object) PXDBQuantityAttribute.Round(CurrencyCollection.GetCurrency(currencyInfo.BaseCuryID).RoundingLimit)
      });
  }

  protected virtual void EPExpenseClaim_CuryID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    PXFormulaAttribute.CalcAggregate<EPExpenseClaimDetails.claimCuryTranAmtWithTaxes>(this.ExpenseClaimDetails.Cache, e.Row);
  }

  protected virtual void EPExpenseClaim_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    EPExpenseClaim row = e.Row as EPExpenseClaim;
    EPExpenseClaim oldRow = e.OldRow as EPExpenseClaim;
    IEnumerable<PXResult<EPExpenseClaimDetails>> source = this.ExpenseClaimDetails.Select().AsEnumerable<PXResult<EPExpenseClaimDetails>>();
    if (row == null || oldRow == null)
      return;
    int? customerId1 = row.CustomerID;
    int? customerId2 = oldRow.CustomerID;
    if (!(customerId1.GetValueOrDefault() == customerId2.GetValueOrDefault() & customerId1.HasValue == customerId2.HasValue) && source.Where<PXResult<EPExpenseClaimDetails>>((Func<PXResult<EPExpenseClaimDetails>, bool>) (_ => !((EPExpenseClaimDetails) _).ContractID.HasValue || ProjectDefaultAttribute.IsNonProject(((EPExpenseClaimDetails) _).ContractID))).Count<PXResult<EPExpenseClaimDetails>>() != 0)
    {
      this.CustomerUpdateAsk.Current.NewCustomerID = row.CustomerID;
      this.CustomerUpdateAsk.Current.OldCustomerID = oldRow.CustomerID;
      int num = (int) this.CustomerUpdateAsk.AskExt();
    }
    if (this.ExpenseClaim.Current != null && this.ExpenseClaim.Current.Released.GetValueOrDefault())
    {
      foreach (PXResult<EPExpenseClaimDetails> pxResult in source)
      {
        EPExpenseClaimDetails expenseClaimDetails = (EPExpenseClaimDetails) pxResult;
        expenseClaimDetails.Status = "R";
        expenseClaimDetails.Released = new bool?(true);
      }
    }
    if (row.TaxCalcMode != oldRow.TaxCalcMode)
    {
      ExpenseClaimDetailGraphExtBase<ExpenseClaimDetailEntry>.DeleteLegacyTaxRows((PXGraph) this, row.RefNbr);
      foreach (PXResult<EPExpenseClaimDetails> pxResult in this.ExpenseClaimDetails.Select())
      {
        EPExpenseClaimDetails expenseClaimDetails = (EPExpenseClaimDetails) pxResult;
        expenseClaimDetails.TaxCalcMode = row.TaxCalcMode;
        this.ExpenseClaimDetails.Update(expenseClaimDetails);
      }
    }
    if (!(row.TaxZoneID != oldRow.TaxZoneID))
      return;
    ExpenseClaimDetailGraphExtBase<ExpenseClaimDetailEntry>.DeleteLegacyTaxRows((PXGraph) this, row.RefNbr);
    foreach (PXResult<EPExpenseClaimDetails> pxResult in this.ExpenseClaimDetails.Select())
    {
      EPExpenseClaimDetails expenseClaimDetails = (EPExpenseClaimDetails) pxResult;
      if (expenseClaimDetails.PaidWith != "CardPers")
        expenseClaimDetails.TaxZoneID = row.TaxZoneID;
      this.ExpenseClaimDetails.Update(expenseClaimDetails);
    }
    if (!e.ExternalCall || this.IsMobile || string.IsNullOrEmpty(row.TaxZoneID))
      return;
    PX.Objects.EP.EPEmployee employee = (PX.Objects.EP.EPEmployee) this.EPEmployee.Select();
    string taxZoneId = ExpenseClaimDetailGraphExtBase<ExpenseClaimDetailEntry>.GetTaxZoneID((PXGraph) this, employee);
    if (!(row.TaxZoneID != taxZoneId))
      return;
    this.EPEmployee.Current = employee;
    int num1 = (int) this.TaxZoneUpdateAskView.View.AskExt();
  }

  protected virtual void EPExpenseClaim_Hold_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is EPExpenseClaim row))
      return;
    foreach (PXResult<EPExpenseClaimDetails> data in this.ExpenseClaimDetails.Select())
      this.ExpenseClaimDetails.Cache.SetValueExt<EPExpenseClaimDetails.holdClaim>((object) (EPExpenseClaimDetails) data, (object) row.Hold);
  }

  public virtual void ValidateProjectAndProjectTask(EPExpenseClaimDetails info)
  {
    if (info == null)
      return;
    string error1 = PXUIFieldAttribute.GetError<EPExpenseClaimDetails.contractID>(this.ExpenseClaimDetails.Cache, (object) info);
    if (!string.IsNullOrEmpty(error1) && error1.Equals(PXLocalizer.Localize("The project is expired.")))
      PXUIFieldAttribute.SetError<EPExpenseClaimDetails.contractID>(this.ExpenseClaimDetails.Cache, (object) info, (string) null);
    int? nullable1 = info.ContractID;
    DateTime? nullable2;
    DateTime? nullable3;
    if (nullable1.HasValue)
    {
      PMProject pmProject = (PMProject) PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Required<EPExpenseClaimDetails.contractID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) info.ContractID);
      if (pmProject != null)
      {
        nullable2 = pmProject.ExpireDate;
        if (nullable2.HasValue)
        {
          nullable2 = info.ExpenseDate;
          if (nullable2.HasValue)
          {
            nullable2 = info.ExpenseDate;
            nullable3 = pmProject.ExpireDate;
            if ((nullable2.HasValue & nullable3.HasValue ? (nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              this.ExpenseClaimDetails.Cache.RaiseExceptionHandling<EPExpenseClaimDetails.contractID>((object) info, (object) info.ContractID, (Exception) new PXSetPropertyException("The project is expired.", PXErrorLevel.Warning));
          }
        }
      }
    }
    string error2 = PXUIFieldAttribute.GetError<EPExpenseClaimDetails.taskID>(this.ExpenseClaimDetails.Cache, (object) info);
    if (!string.IsNullOrEmpty(error2) && (error2.Equals(PXLocalizer.Localize("The project task is expired.")) || error2.Equals(PXLocalizer.Localize("The project task is completed."))))
      PXUIFieldAttribute.SetError<EPExpenseClaimDetails.taskID>(this.ExpenseClaimDetails.Cache, (object) info, (string) null);
    nullable1 = info.TaskID;
    if (!nullable1.HasValue)
      return;
    PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, info.ContractID, info.TaskID);
    if (dirty == null)
      return;
    nullable3 = dirty.EndDate;
    if (!nullable3.HasValue)
      return;
    nullable3 = info.ExpenseDate;
    if (!nullable3.HasValue)
      return;
    nullable3 = info.ExpenseDate;
    nullable2 = dirty.EndDate;
    if ((nullable3.HasValue & nullable2.HasValue ? (nullable3.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0 && dirty.Status != "F")
    {
      this.ExpenseClaimDetails.Cache.RaiseExceptionHandling<EPExpenseClaimDetails.taskID>((object) info, (object) info.TaskID, (Exception) new PXSetPropertyException("The project task is expired.", PXErrorLevel.Warning));
    }
    else
    {
      if (!(dirty.Status == "F"))
        return;
      this.ExpenseClaimDetails.Cache.RaiseExceptionHandling<EPExpenseClaimDetails.taskID>((object) info, (object) info.TaskID, (Exception) new PXSetPropertyException("The project task is completed.", PXErrorLevel.Warning));
    }
  }

  protected virtual void EPExpenseClaimDetails_CuryTipAmt_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    EPExpenseClaimDetails row = e.Row as EPExpenseClaimDetails;
    Decimal? newValue = e.NewValue as Decimal?;
    if (row == null || !newValue.HasValue)
      return;
    Decimal? nullable = newValue;
    Decimal num = 0M;
    if (nullable.GetValueOrDefault() == num & nullable.HasValue)
      return;
    if ((PX.Objects.IN.InventoryItem) PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, (object) this.epsetup.Current.NonTaxableTipItem) == null)
      throw new PXSetPropertyException("To be able to specify a nonzero tip amount in the expense receipt, specify an appropriate tip item in the Non-Taxable Tip Item box on the Time & Expenses Preferences (EP101000) form.");
  }

  protected virtual void EPExpenseClaimDetails_CuryTipAmt_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is EPExpenseClaimDetails row))
      return;
    Decimal? curyTipAmt = row.CuryTipAmt;
    Decimal num = 0M;
    if (!(curyTipAmt.GetValueOrDefault() == num & curyTipAmt.HasValue))
      this.ExpenseClaimDetails.SetValueExt<EPExpenseClaimDetails.taxTipCategoryID>(row, (object) ((PX.Objects.IN.InventoryItem) PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, (object) this.epsetup.Current.NonTaxableTipItem) ?? throw new PXSetPropertyException<EPExpenseClaimDetails.curyTipAmt>("To be able to specify a nonzero tip amount in the expense receipt, specify an appropriate tip item in the Non-Taxable Tip Item box on the Time & Expenses Preferences (EP101000) form.")).TaxCategoryID);
    else
      this.ExpenseClaimDetails.SetValueExt<EPExpenseClaimDetails.taxTipCategoryID>(row, (object) null);
  }

  protected virtual void EPExpenseClaimDetails_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    EPExpenseClaimDetails row = (EPExpenseClaimDetails) e.Row;
    if (row == null)
      return;
    if (this.ExpenseClaim.Current != null)
    {
      PX.Objects.CM.CurrencyInfo copy = (PX.Objects.CM.CurrencyInfo) this.currencyinfo.Cache.CreateCopy((object) (PX.Objects.CM.CurrencyInfo) PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<EPExpenseClaim.curyInfoID>>>>.Config>.Select((PXGraph) this, (object) this.ExpenseClaim.Current.CuryInfoID));
      copy.CuryInfoID = row.CuryInfoID;
      this.currencyinfo.Cache.Update((object) copy);
      this.ReceiptEntryExt.SubmitReceiptExt(this.ExpenseClaim.Cache, cache, this.ExpenseClaim.Current, row);
    }
    ExpenseClaimDetailGraphExtBase<ExpenseClaimDetailEntry>.DeleteLegacyTaxRows((PXGraph) this, row.RefNbr);
  }

  protected virtual void EPExpenseClaimDetails_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    EPExpenseClaimDetails row = (EPExpenseClaimDetails) e.Row;
    EPExpenseClaimDetails oldRow = (EPExpenseClaimDetails) e.OldRow;
    if (!(row.RefNbr != oldRow.RefNbr) && !(row.TaxCategoryID != oldRow.TaxCategoryID) && !(row.TaxCalcMode != oldRow.TaxCalcMode) && !(row.TaxZoneID != oldRow.TaxZoneID))
      return;
    ExpenseClaimDetailGraphExtBase<ExpenseClaimDetailEntry>.DeleteLegacyTaxRows((PXGraph) this, row.RefNbr);
  }

  protected virtual void EPExpenseClaimDetails_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is EPExpenseClaimDetails row))
      return;
    if (cache.GetStatus((object) row) != PXEntryStatus.InsertedDeleted)
    {
      this.ReceiptEntryExt.RemoveReceipt(this.ExpenseClaimDetails.Cache, row);
      e.Cancel = true;
      this.ExpenseClaimDetails.View.RequestRefresh();
    }
    else
      this.ReceiptEntryExt.RemoveReceipt(this.ExpenseClaimDetails.Cache, row, true);
  }

  protected virtual void EPExpenseClaimDetails_RowPersisting(
    PXCache cache,
    PXRowPersistingEventArgs e)
  {
    if (!(e.Row is EPExpenseClaimDetails row) || e.Operation == PXDBOperation.Delete)
      return;
    int? nullable1 = row.ContractID;
    if (nullable1.HasValue)
    {
      bool? nullable2 = row.Billable;
      if (nullable2.Value)
      {
        nullable1 = row.TaskID;
        if (nullable1.HasValue)
        {
          PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, row.ContractID, row.TaskID);
          if (dirty != null)
          {
            nullable2 = dirty.VisibleInAP;
            if (!nullable2.Value)
              cache.RaiseExceptionHandling<EPExpenseClaimDetails.taskID>(e.Row, (object) dirty.TaskCD, (Exception) new PXSetPropertyException("Project Task '{0}' is invisible in {1} module.", new object[2]
              {
                (object) dirty.TaskCD,
                (object) "AP"
              }));
          }
        }
      }
    }
    if (!(row.CuryTipAmt.GetValueOrDefault() != 0M))
      return;
    PXSetup<EPSetup> epsetup = this.epsetup;
    int num;
    if (epsetup == null)
    {
      num = 1;
    }
    else
    {
      EPSetup current = epsetup.Current;
      if (current == null)
      {
        num = 1;
      }
      else
      {
        nullable1 = current.NonTaxableTipItem;
        num = !nullable1.HasValue ? 1 : 0;
      }
    }
    if (num == 0)
      return;
    cache.RaiseExceptionHandling<EPExpenseClaimDetails.curyTipAmt>((object) row, (object) row.CuryTipAmt, (Exception) new PXRowPersistingException("EPExpenseClaim", (object) null, "To be able to specify a nonzero tip amount in the expense receipt, specify an appropriate tip item in the Non-Taxable Tip Item box on the Time & Expenses Preferences (EP101000) form."));
  }

  protected virtual void EPExpenseClaimDetails_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    EPExpenseClaimDetails row = (EPExpenseClaimDetails) e.Row;
    if (row == null)
      return;
    bool? nullable;
    int num1;
    if (row.Hold.GetValueOrDefault() || !this.enabledApprovalReceipt)
    {
      nullable = (bool?) this.ExpenseClaimCurrent.Current?.Hold;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    bool isEnabled = num1 != 0;
    nullable = row.Rejected;
    int num2;
    if (!nullable.GetValueOrDefault())
    {
      nullable = row.Released;
      if (!nullable.GetValueOrDefault())
      {
        nullable = row.HoldClaim;
        bool flag = false;
        num2 = !(nullable.GetValueOrDefault() == flag & nullable.HasValue) ? 1 : 0;
        goto label_8;
      }
    }
    num2 = 0;
label_8:
    bool enabledFinancialDetails = num2 != 0;
    PXUIFieldAttribute.SetEnabled(cache, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.curyTranAmtWithTaxes>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.claimCuryTranAmtWithTaxes>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.status>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.aRRefNbr>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.aPRefNbr>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.curyNetAmount>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.curyTaxTotal>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.expenseAccountID>(cache, (object) row, enabledFinancialDetails);
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.expenseSubID>(cache, (object) row, enabledFinancialDetails);
    PXCache cache1 = cache;
    EPExpenseClaimDetails data1 = row;
    int num3;
    if (enabledFinancialDetails)
    {
      nullable = row.Billable;
      num3 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 0;
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.salesAccountID>(cache1, (object) data1, num3 != 0);
    PXCache cache2 = cache;
    EPExpenseClaimDetails data2 = row;
    int num4;
    if (enabledFinancialDetails)
    {
      nullable = row.Billable;
      num4 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num4 = 0;
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.salesSubID>(cache2, (object) data2, num4 != 0);
    cache.Adjust<PXUIFieldAttribute>().For<EPExpenseClaimDetails.taxCategoryID>((Action<PXUIFieldAttribute>) (ui => ui.Enabled = enabledFinancialDetails && !row.BankTranDate.HasValue && row.PaidWith != "CardPers")).SameFor<EPExpenseClaimDetails.taxCalcMode>();
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.curyID>(cache, (object) row, this.IsCopyPasteContext);
    this.Tax_Rows.Cache.SetAllEditPermissions(isEnabled && !row.BankTranDate.HasValue);
    Numbering numbering = (Numbering) PXSelectBase<Numbering, PXSelect<Numbering, Where<Numbering.numberingID, Equal<Required<Numbering.numberingID>>>>.Config>.Select((PXGraph) this, (object) this.epsetup.Current.ReceiptNumberingID);
    PXCache cache3 = cache;
    EPExpenseClaimDetails data3 = row;
    int num5;
    if (numbering != null)
    {
      nullable = numbering.UserNumbering;
      if (nullable.GetValueOrDefault())
      {
        num5 = cache.GetStatus((object) row) == PXEntryStatus.Inserted ? 1 : 0;
        goto label_18;
      }
    }
    num5 = 0;
label_18:
    PXUIFieldAttribute.SetEnabled<EPExpenseClaimDetails.claimDetailCD>(cache3, (object) data3, num5 != 0);
  }

  protected virtual void EPExpenseClaimDetails_ExpenseSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ExpenseClaimDetailGraphExtBase<ExpenseClaimDetailEntry>.ExpenseSubID_FieldDefaulting(sender, e, this.epsetup.Current.ExpenseSubMask, this.epsetup.Current.ExpenseSubMaskNB);
  }

  protected virtual void EPExpenseClaimDetails_SalesSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ExpenseClaimDetailGraphExtBase<ExpenseClaimDetailEntry>.SalesSubID_FieldDefaulting(sender, e, this.epsetup.Current.SalesSubMask);
  }

  protected virtual void EPExpenseClaimDetails_Hold_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this.enabledApprovalReceipt;
    e.Cancel = true;
  }

  protected virtual void EPExpenseClaimDetails_Approved_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) !this.enabledApprovalReceipt;
    e.Cancel = true;
  }

  protected virtual void EPExpenseClaimDetails_Status_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = this.enabledApprovalReceipt ? (object) "H" : (object) "A";
    e.Cancel = true;
  }

  protected virtual void EPExpenseClaimDetails_CustomerID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    if ((e.Row is EPExpenseClaimDetails row ? (!row.CustomerID.HasValue ? 1 : 0) : 1) == 0)
      return;
    cache.SetValueExt<EPExpenseClaimDetails.customerLocationID>((object) row, (object) null);
  }

  protected virtual void EPExpenseClaimDetails_TaxZoneID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    EPExpenseClaimDetails row = (EPExpenseClaimDetails) e.Row;
    e.NewValue = (object) this.GetDefaultTaxZone(row);
  }

  protected virtual void EPExpenseClaimDetails_CustomerLocationID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this.ExpenseClaimCurrent.Current.CustomerLocationID;
    e.Cancel = true;
  }

  protected virtual void EPExpenseClaimDetails_TaxCalcMode_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this.ExpenseClaimCurrent.Current.TaxCalcMode;
  }

  protected virtual void EPExpenseClaimDetails_TaxCategoryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is EPExpenseClaimDetails row))
      return;
    row.CuryTaxableAmtFromTax = new Decimal?(0M);
    row.TaxableAmtFromTax = new Decimal?(0M);
    row.CuryTaxAmt = new Decimal?(0M);
    row.TaxAmt = new Decimal?(0M);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.inventoryID> e)
  {
    PXUIFieldAttribute.SetError<EPExpenseClaimDetails.uOM>(e.Cache, (object) e.Row, (string) null);
    object obj = PXFormulaAttribute.Evaluate<EPExpenseClaimDetails.uOM>(e.Cache, (object) e.Row);
    e.Cache.SetValueExt<EPExpenseClaimDetails.uOM>((object) e.Row, obj);
    e.Cache.SetDefaultExt<EPExpenseClaimDetails.costCodeID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.taskID> e)
  {
    e.Cache.SetDefaultExt<EPExpenseClaimDetails.costCodeID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPExpenseClaimDetails, EPExpenseClaimDetails.uOM> e)
  {
    if (e.Row == null)
      return;
    string oldValue = (string) e.OldValue;
    Decimal valueOrDefault = e.Row.CuryUnitCost.GetValueOrDefault();
    if (string.IsNullOrEmpty(oldValue) || string.IsNullOrEmpty(e.Row.UOM) || !(oldValue != e.Row.UOM) || !(valueOrDefault != 0M))
      return;
    Decimal num1 = INUnitAttribute.ConvertFromBase<EPExpenseClaimDetails.inventoryID>(e.Cache, (object) e.Row, oldValue, valueOrDefault, INPrecision.NOROUND);
    Decimal num2 = INUnitAttribute.ConvertToBase<EPExpenseClaimDetails.inventoryID>(e.Cache, (object) e.Row, e.Row.UOM, num1, INPrecision.UNITCOST);
    e.Cache.SetValueExt<EPExpenseClaimDetails.curyUnitCost>((object) e.Row, (object) num2);
  }

  protected virtual void EPTaxTran_CuryTaxAmt_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    EPTaxTran row = (EPTaxTran) e.Row;
    EPExpenseClaimDetails current = this.ExpenseClaimDetails.Current;
    if (e.NewValue == null)
      return;
    Decimal newValue = (Decimal) e.NewValue;
    if (row == null || current == null)
      return;
    if (newValue > 0M)
    {
      Decimal? curyExtCost = current.CuryExtCost;
      Decimal num = 0M;
      if (curyExtCost.GetValueOrDefault() < num & curyExtCost.HasValue)
        goto label_6;
    }
    if (!(newValue < 0M))
      return;
    Decimal? curyExtCost1 = current.CuryExtCost;
    Decimal num1 = 0M;
    if (!(curyExtCost1.GetValueOrDefault() > num1 & curyExtCost1.HasValue))
      return;
label_6:
    cache.RaiseExceptionHandling<EPTaxTran.curyTaxAmt>((object) row, (object) row.CuryTaxAmt, (Exception) new PXSetPropertyException("The tax amount must have the same sign as the total amount."));
    e.NewValue = (object) row.CuryTaxAmt;
  }

  protected virtual void EPTaxTran_CuryTaxableAmt_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    EPTaxTran row = (EPTaxTran) e.Row;
    EPExpenseClaimDetails current = this.ExpenseClaimDetails.Current;
    if (e.NewValue == null)
      return;
    Decimal newValue = (Decimal) e.NewValue;
    if (row == null || current == null)
      return;
    if (newValue > 0M)
    {
      Decimal? curyExtCost = current.CuryExtCost;
      Decimal num = 0M;
      if (curyExtCost.GetValueOrDefault() < num & curyExtCost.HasValue)
        goto label_6;
    }
    if (!(newValue < 0M))
      return;
    Decimal? curyExtCost1 = current.CuryExtCost;
    Decimal num1 = 0M;
    if (!(curyExtCost1.GetValueOrDefault() > num1 & curyExtCost1.HasValue))
      return;
label_6:
    cache.RaiseExceptionHandling<EPTaxTran.curyTaxableAmt>((object) row, (object) row.CuryTaxableAmt, (Exception) new PXSetPropertyException("The taxable amount must have the same sign as the total amount."));
    e.NewValue = (object) row.CuryTaxableAmt;
  }

  protected virtual void EPTaxTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<EPTaxTran.taxID>(sender, e.Row, sender.GetStatus(e.Row) == PXEntryStatus.Inserted);
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Receipt ID", Visibility = PXUIVisibility.Visible)]
  protected virtual void EPExpenseClaimDetails_ClaimDetailID_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [EPTax]
  protected virtual void EPExpenseClaimDetails_TaxCategoryID_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Tax Amount", Enabled = false, Visible = true)]
  protected virtual void EPExpenseClaimDetails_CuryTaxTotal_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Replace)]
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXUIField(DisplayName = "Currency")]
  protected virtual void EPExpenseClaimDetails_CuryID_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Tax Calculation Mode", Enabled = false, Visibility = PXUIVisibility.SelectorVisible, Visible = false)]
  protected virtual void EPExpenseClaimDetails_TaxCalcMode_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXDefault(true)]
  protected virtual void EPExpenseClaimDetails_CreatedFromClaim_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXDefault(typeof (EPExpenseClaim.taxZoneID), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Tax Zone", Required = false, Enabled = false, Visibility = PXUIVisibility.SelectorVisible, Visible = false)]
  protected virtual void EPExpenseClaimDetails_TaxZoneID_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Claimed by", Visible = false)]
  protected virtual void EPExpenseClaimDetails_EmployeeID_CacheAttached(PXCache cache)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Corporate Card")]
  [PXRestrictor(typeof (Where<CACorpCard.isActive, Equal<True>>), "The corporate card is inactive.", new System.Type[] {})]
  [PXSelector(typeof (Search2<CACorpCard.corpCardID, InnerJoin<EPEmployeeCorpCardLink, On<EPEmployeeCorpCardLink.employeeID, Equal<Current2<EPExpenseClaimDetails.employeeID>>, And<EPEmployeeCorpCardLink.corpCardID, Equal<CACorpCard.corpCardID>>>, InnerJoin<PX.Objects.CA.CashAccount, On<PX.Objects.CA.CashAccount.cashAccountID, Equal<CACorpCard.cashAccountID>>, InnerJoin<PX.Objects.GL.Account, On<PX.Objects.GL.Account.accountID, Equal<PX.Objects.CA.CashAccount.accountID>>>>>>), new System.Type[] {typeof (CACorpCard.corpCardCD), typeof (CACorpCard.name), typeof (CACorpCard.cardNumber), typeof (PX.Objects.GL.Account.curyID)}, SubstituteKey = typeof (CACorpCard.corpCardCD), DescriptionField = typeof (CACorpCard.name))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<EPExpenseClaimDetails.corpCardID> e)
  {
  }

  [PXDefault(typeof (EPExpenseClaim.docDate), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void EPApproval_DocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (EPExpenseClaim.employeeID), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void EPApproval_BAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (FbqlSelect<SelectFromBase<PX.Objects.CR.Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.EP.EPEmployee>.On<PX.Objects.EP.EPEmployee.FK.ContactInfo>>>.Where<BqlOperand<PX.Objects.EP.EPEmployee.bAccountID, IBqlInt>.IsEqual<BqlField<EPExpenseClaim.employeeID, IBqlInt>.FromCurrent>>, PX.Objects.CR.Contact>.SearchFor<PX.Objects.CR.Contact.contactID>), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void EPApproval_DocumentOwnerID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (EPExpenseClaim.docDesc), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void EPApproval_Descr_CacheAttached(PXCache sender)
  {
  }

  [CurrencyInfo(typeof (EPExpenseClaim.curyInfoID))]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void EPApproval_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (EPExpenseClaim.curyDocBal), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void EPApproval_CuryTotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (EPExpenseClaim.docBal), PersistingCheck = PXPersistingCheck.Nothing)]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void EPApproval_TotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Tax Zone", Visibility = PXUIVisibility.Visible)]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.taxZoneID), Filterable = true)]
  protected virtual void APInvoice_TaxZoneID_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [APDocType.List]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.AP.APInvoice.docType> e)
  {
  }

  protected bool enabledApprovalReceipt
  {
    get
    {
      return PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.approvalWorkflow>() && this.epsetup.Current.ClaimDetailsAssignmentMapID.HasValue;
    }
  }

  public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values) => true;

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public void PrepareItems(string viewName, IEnumerable items)
  {
  }

  private void TryValidateRequireUniqueVendorRefWhenCardInvolved()
  {
    bool flag = false;
    foreach (PXResult<EPExpenseClaimDetails> pxResult in this.ExpenseClaimDetails.Select())
    {
      EPExpenseClaimDetails row = (EPExpenseClaimDetails) pxResult;
      if (row.IsPaidWithCard)
      {
        PX.Objects.CA.CashAccount cashAccount = (PX.Objects.CA.CashAccount) PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.accountID, Equal<Required<EPExpenseClaimDetails.expenseAccountID>>>>.Config>.Select((PXGraph) this, (object) row.ExpenseAccountID);
        if (cashAccount == null)
          return;
        PX.Objects.CA.PaymentMethod paymentMethod = (PX.Objects.CA.PaymentMethod) PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelectJoin<PX.Objects.CA.PaymentMethod, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.CA.PaymentMethod.paymentMethodID>>>, Where<PaymentMethodAccount.cashAccountID, Equal<Required<PX.Objects.CA.CashAccount.cashAccountID>>>>.Config>.Select((PXGraph) this, (object) cashAccount.CashAccountID);
        if (paymentMethod != null && (paymentMethod.APRequirePaymentRef ?? false))
        {
          CACorpCard caCorpCard = CACorpCard.PK.Find((PXGraph) this, row.CorpCardID);
          if (caCorpCard == null)
            return;
          this.ExpenseClaimDetails.Cache.RaiseExceptionHandling<EPExpenseClaimDetails.expenseAccountID>((object) row, (object) row.ExpenseAccountID, (Exception) new PXSetPropertyException("The expense claim cannot be released because the {0} cash account of the {1} corporate card is associated with the {2} payment method that requires a unique payment reference number. Either clear the Require Unique Payment Ref. check box on the Settings for Use in AP tab of the Payment Methods (CA204000) form for the payment method or review the configuration of the cash account.", PXErrorLevel.RowError, new object[3]
          {
            (object) cashAccount.CashAccountCD,
            (object) caCorpCard.Name,
            (object) paymentMethod.PaymentMethodID
          }));
          flag = true;
        }
      }
    }
    if (flag)
      throw new PXException("The expense claim cannot be released. Please review the details of the expense claim.");
  }

  private void ValidateFinancialPeriod(DateTime date, int employeeID)
  {
    PX.Objects.GL.Branch branch = (PX.Objects.GL.Branch) PXSelectBase<PX.Objects.GL.Branch, PXSelectJoin<PX.Objects.GL.Branch, InnerJoin<PX.Objects.EP.EPEmployee, On<PX.Objects.EP.EPEmployee.bAccountID, Equal<Required<PX.Objects.EP.EPEmployee.bAccountID>>>>, Where<PX.Objects.GL.Branch.bAccountID, Equal<PX.Objects.EP.EPEmployee.parentBAccountID>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) employeeID);
    if (this.FinPeriodRepository.FindFinPeriodByDate(new DateTime?(date), PXAccess.GetParentOrganizationID((int?) branch?.BranchID)) == null)
      throw new PXSetPropertyException<EPExpenseClaim.docDate>("The financial period that corresponds to the {0} date does not exist in the master financial calendar.", PXErrorLevel.Error, new object[1]
      {
        (object) date.ToShortDateString()
      });
  }

  public virtual string GetDefaultTaxZone(EPExpenseClaimDetails row)
  {
    string str = (string) null;
    return row == null ? str : this.ExpenseClaimCurrent.Current.TaxZoneID;
  }

  public class ExpenseClaimEntryReceiptExt : ExpenseClaimDetailEntryExt<ExpenseClaimEntry>
  {
    public override bool UseClaimStatus => true;

    public override PXSelectBase<EPExpenseClaimDetails> Receipts
    {
      get => (PXSelectBase<EPExpenseClaimDetails>) this.Base.ExpenseClaimDetails;
    }

    public override PXSelectBase<EPExpenseClaim> Claim
    {
      get => (PXSelectBase<EPExpenseClaim>) this.Base.ExpenseClaimCurrent;
    }

    public override PXSelectBase<PX.Objects.CM.CurrencyInfo> CurrencyInfo
    {
      get => (PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.Base.currencyinfo;
    }
  }

  public class ExpenceClaimApproval<SourceAssign> : 
    EPApprovalAutomation<SourceAssign, EPExpenseClaim.approved, EPExpenseClaim.rejected, EPExpenseClaim.hold, EPSetupExpenseClaimApproval>
    where SourceAssign : EPExpenseClaim, new()
  {
    public ExpenceClaimApproval(PXGraph graph, Delegate @delegate)
      : base(graph, @delegate)
    {
      this.Initialize();
    }

    public ExpenceClaimApproval(PXGraph graph)
      : base(graph)
    {
      this.Initialize();
    }

    private void Initialize()
    {
      if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.approvalWorkflow>())
        return;
      this.Cache.Graph.FieldVerifying.AddHandler<EPApproval.ownerID>((PXFieldVerifying) ((sender, e) => e.Cancel = true));
    }

    protected override void AssignMaps(PXCache cache, SourceAssign doc)
    {
      if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.approvalWorkflow>())
      {
        EPSetup epSetup = (EPSetup) PXSelectBase<EPSetup, PXSelectReadonly<EPSetup>.Config>.SelectSingleBound(cache.Graph, new object[1]
        {
          (object) doc
        });
        try
        {
          this.Assign(doc, new int?(), epSetup.ClaimAssignmentNotificationID);
        }
        catch (PXSetPropertyException ex)
        {
          cache.SetValue<EPExpenseClaim.hold>((object) doc, (object) true);
          throw new PXException("Unable to process the approval.\n{0}", new object[1]
          {
            (object) (ex.InnerException ?? (Exception) ex).Message
          });
        }
      }
      else
        base.AssignMaps(cache, doc);
    }

    protected override IEnumerable<ApproveInfo> GetApproversFromNextStep(
      SourceAssign source,
      EPAssignmentMap map,
      int? currentStepSequence)
    {
      ExpenseClaimEntry.ExpenceClaimApproval<SourceAssign> expenceClaimApproval = this;
      if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.approvalWorkflow>())
      {
        // ISSUE: reference to a compiler-generated method
        foreach (ApproveInfo approveInfo in expenceClaimApproval.\u003C\u003En__0(source, map, currentStepSequence))
          yield return approveInfo;
      }
      else
      {
        PX.Objects.EP.EPEmployee current = (PX.Objects.EP.EPEmployee) expenceClaimApproval._Graph.Caches[typeof (PX.Objects.EP.EPEmployee)].Current;
        if (current.SupervisorID.HasValue)
        {
          PX.Objects.EP.EPEmployee epEmployee = (PX.Objects.EP.EPEmployee) PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Current<PX.Objects.EP.EPEmployee.supervisorID>>>>.Config>.Select(expenceClaimApproval._Graph, (object) current.BAccountID);
          if (epEmployee != null)
          {
            source.WorkgroupID = new int?();
            source.OwnerID = epEmployee.DefContactID;
            yield return new ApproveInfo()
            {
              OwnerID = epEmployee.DefContactID,
              WorkgroupID = new int?()
            };
          }
        }
      }
    }
  }

  [Serializable]
  public class EPExpenseClaimDetailsForSubmit : EPExpenseClaimDetails
  {
    [PXDBIdentity(IsKey = true)]
    public override int? ClaimDetailID { get; set; }

    [PXDBString(15, IsUnicode = true)]
    public override string RefNbr { get; set; }

    [PXDBString(15, IsUnicode = true)]
    public override string TaxCategoryID { get; set; }

    [PXDBCurrency(typeof (EPExpenseClaimDetails.curyInfoID), typeof (EPExpenseClaimDetails.tipAmt))]
    [PXUIField(DisplayName = "Tip Amount")]
    public override Decimal? CuryTipAmt { get; set; }

    public new abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ExpenseClaimEntry.EPExpenseClaimDetailsForSubmit.selected>
    {
    }

    public new abstract class claimDetailID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ExpenseClaimEntry.EPExpenseClaimDetailsForSubmit.claimDetailID>
    {
    }

    public new abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ExpenseClaimEntry.EPExpenseClaimDetailsForSubmit.refNbr>
    {
    }

    public new abstract class taxCategoryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ExpenseClaimEntry.EPExpenseClaimDetailsForSubmit.taxCategoryID>
    {
    }

    public new abstract class curyTipAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ExpenseClaimEntry.EPExpenseClaimDetailsForSubmit.curyTipAmt>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class EPCustomerUpdateAsk : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    public const string SelectedCustomer = "S";
    public const string AllLines = "A";
    public const string Nothing = "N";

    [PXDBString(1, IsFixed = true)]
    [PXUIField(DisplayName = "Date Based On", Visibility = PXUIVisibility.Visible)]
    [PXDefault("S")]
    [PXStringList(new string[] {"S", "A", "N"}, new string[] {"Lines with selected customer", "All lines", "Nothing"})]
    public virtual string CustomerUpdateAnswer { get; set; }

    [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
    [CustomerActive(DescriptionField = typeof (PX.Objects.AR.Customer.acctName))]
    public virtual int? NewCustomerID { get; set; }

    [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
    [CustomerActive(DescriptionField = typeof (PX.Objects.AR.Customer.acctName))]
    public virtual int? OldCustomerID { get; set; }

    public abstract class customerUpdateAnswer : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ExpenseClaimEntry.EPCustomerUpdateAsk.customerUpdateAnswer>
    {
    }

    public abstract class newCustomerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ExpenseClaimEntry.EPCustomerUpdateAsk.newCustomerID>
    {
    }

    public abstract class oldCustomerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ExpenseClaimEntry.EPCustomerUpdateAsk.oldCustomerID>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class TaxZoneUpdateAsk : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }
}
