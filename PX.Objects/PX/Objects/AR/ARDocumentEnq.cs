// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDocumentEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR.Override;
using PX.Objects.AR.Repositories;
using PX.Objects.AR.Standalone;
using PX.Objects.CM;
using PX.Objects.Common.MigrationMode;
using PX.Objects.Common.Utility;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.PM;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class ARDocumentEnq : PXGraph<
#nullable disable
ARDocumentEnq>
{
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> dummy_view;
  public PXAction<ARDocumentEnq.ARDocumentFilter> refresh;
  public PXCancel<ARDocumentEnq.ARDocumentFilter> Cancel;
  [Obsolete("Will be removed in Acumatica 2019R1")]
  public PXAction<ARDocumentEnq.ARDocumentFilter> viewDocument;
  public PXAction<ARDocumentEnq.ARDocumentFilter> viewOriginalDocument;
  public PXAction<ARDocumentEnq.ARDocumentFilter> previousPeriod;
  public PXAction<ARDocumentEnq.ARDocumentFilter> nextPeriod;
  public PXAction<ARDocumentEnq.ARDocumentFilter> actionsfolder;
  public PXAction<ARDocumentEnq.ARDocumentFilter> createInvoice;
  public PXAction<ARDocumentEnq.ARDocumentFilter> createPayment;
  public PXAction<ARDocumentEnq.ARDocumentFilter> payDocument;
  public PXAction<ARDocumentEnq.ARDocumentFilter> reportsfolder;
  public PXAction<ARDocumentEnq.ARDocumentFilter> aRBalanceByCustomerReport;
  public PXAction<ARDocumentEnq.ARDocumentFilter> customerHistoryReport;
  public PXAction<ARDocumentEnq.ARDocumentFilter> aRAgedPastDueReport;
  public PXAction<ARDocumentEnq.ARDocumentFilter> aRAgedOutstandingReport;
  public PXAction<ARDocumentEnq.ARDocumentFilter> aRRegisterReport;
  protected CustomerRepository CustomerRepository;
  public PXFilter<ARDocumentEnq.ARDocumentFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXSelectOrderBy<ARDocumentEnq.ARDocumentResult, OrderBy<Desc<ARDocumentEnq.ARDocumentResult.docDate>>> Documents;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public PXSetup<PX.Objects.GL.Company> Company;

  public ARDocumentEnq()
  {
    PX.Objects.AR.ARSetup current1 = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    PX.Objects.GL.Company current2 = ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current;
    ((PXSelectBase) this.Documents).Cache.AllowDelete = false;
    ((PXSelectBase) this.Documents).Cache.AllowInsert = false;
    ((PXSelectBase) this.Documents).Cache.AllowUpdate = false;
    PXUIFieldAttribute.SetVisibility<ARDocumentEnq.ARRegister.finPeriodID>(((PXSelectBase) this.Documents).Cache, (object) null, (PXUIVisibility) 3);
    PXUIFieldAttribute.SetVisibility<ARDocumentEnq.ARRegister.customerID>(((PXSelectBase) this.Documents).Cache, (object) null, (PXUIVisibility) 3);
    PXUIFieldAttribute.SetVisibility<ARDocumentEnq.ARRegister.curyDiscBal>(((PXSelectBase) this.Documents).Cache, (object) null, (PXUIVisibility) 3);
    PXUIFieldAttribute.SetVisibility<ARDocumentEnq.ARRegister.curyOrigDocAmt>(((PXSelectBase) this.Documents).Cache, (object) null, (PXUIVisibility) 3);
    PXUIFieldAttribute.SetVisibility<ARDocumentEnq.ARRegister.curyDiscTaken>(((PXSelectBase) this.Documents).Cache, (object) null, (PXUIVisibility) 3);
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARRegister.customerID>(((PXSelectBase) this.Documents).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>());
    ((PXAction) this.actionsfolder).MenuAutoOpen = true;
    ((PXAction) this.actionsfolder).AddMenuAction((PXAction) this.createInvoice);
    ((PXAction) this.actionsfolder).AddMenuAction((PXAction) this.createPayment);
    ((PXAction) this.actionsfolder).AddMenuAction((PXAction) this.payDocument);
    ((PXAction) this.reportsfolder).MenuAutoOpen = true;
    ((PXAction) this.reportsfolder).AddMenuAction((PXAction) this.aRBalanceByCustomerReport);
    ((PXAction) this.reportsfolder).AddMenuAction((PXAction) this.customerHistoryReport);
    ((PXAction) this.reportsfolder).AddMenuAction((PXAction) this.aRAgedPastDueReport);
    ((PXAction) this.reportsfolder).AddMenuAction((PXAction) this.aRAgedOutstandingReport);
    ((PXAction) this.reportsfolder).AddMenuAction((PXAction) this.aRRegisterReport);
    this.CustomerRepository = new CustomerRepository((PXGraph) this);
  }

  public virtual bool IsDirty => false;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [PXUIField]
  [PXButton(ImageKey = "Refresh", IsLockedOnToolbar = true)]
  public IEnumerable Refresh(PXAdapter adapter)
  {
    ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current.RefreshTotals = new bool?(true);
    return adapter.Get();
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<ARDocumentEnq.ARDocumentResult>) this.Documents).Current != null)
      PXRedirectHelper.TryRedirect(((PXSelectBase) this.Documents).Cache, (object) ((PXSelectBase<ARDocumentEnq.ARDocumentResult>) this.Documents).Current, "Document", (PXRedirectHelper.WindowMode) 3);
    return (IEnumerable) ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewOriginalDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<ARDocumentEnq.ARDocumentResult>) this.Documents).Current != null)
    {
      ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
      PX.Objects.AR.ARRegister arRegister = PXResultset<PX.Objects.AR.ARRegister>.op_Implicit(PXSelectBase<PX.Objects.AR.ARRegister, PXSelect<PX.Objects.AR.ARRegister, Where<PX.Objects.AR.ARRegister.refNbr, Equal<Required<PX.Objects.AR.ARRegister.origRefNbr>>, And<PX.Objects.AR.ARRegister.docType, Equal<Required<PX.Objects.AR.ARRegister.origDocType>>>>>.Config>.SelectSingleBound((PXGraph) instance, (object[]) null, new object[2]
      {
        (object) ((PXSelectBase<ARDocumentEnq.ARDocumentResult>) this.Documents).Current.OrigRefNbr,
        (object) ((PXSelectBase<ARDocumentEnq.ARDocumentResult>) this.Documents).Current.OrigDocType
      }));
      if (arRegister != null)
        PXRedirectHelper.TryRedirect(((PXSelectBase) instance.Document).Cache, (object) arRegister, "Document", (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXPreviousButton]
  public virtual IEnumerable PreviousPeriod(PXAdapter adapter)
  {
    ARDocumentEnq.ARDocumentFilter current = ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current;
    FinPeriod prevPeriod = this.FinPeriodRepository.FindPrevPeriod(this.FinPeriodRepository.GetCalendarOrganizationID(current.OrganizationID, current.BranchID, current.UseMasterCalendar), current.Period, true);
    current.Period = prevPeriod?.FinPeriodID;
    current.RefreshTotals = new bool?(true);
    return adapter.Get();
  }

  [PXUIField]
  [PXNextButton]
  public virtual IEnumerable NextPeriod(PXAdapter adapter)
  {
    ARDocumentEnq.ARDocumentFilter current = ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current;
    FinPeriod nextPeriod = this.FinPeriodRepository.FindNextPeriod(this.FinPeriodRepository.GetCalendarOrganizationID(current.OrganizationID, current.BranchID, current.UseMasterCalendar), current.Period, true);
    current.Period = nextPeriod?.FinPeriodID;
    current.RefreshTotals = new bool?(true);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Actionsfolder(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Reportsfolder(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton(Category = "Document Processing")]
  public virtual IEnumerable CreateInvoice(PXAdapter adapter)
  {
    if (((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current != null && ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current.CustomerID.HasValue)
    {
      CustomerMaint instance = PXGraph.CreateInstance<CustomerMaint>();
      ((PXSelectBase<Customer>) instance.BAccount).Current = PXResultset<Customer>.op_Implicit(((PXSelectBase<Customer>) instance.BAccount).Search<PX.Objects.CR.BAccount.bAccountID>((object) ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current.CustomerID, Array.Empty<object>()));
      ((PXAction) instance.newInvoiceMemo).Press();
    }
    return (IEnumerable) ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXButton(Category = "Document Processing")]
  public virtual IEnumerable CreatePayment(PXAdapter adapter)
  {
    if (((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current != null && ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current.CustomerID.HasValue)
    {
      CustomerMaint instance = PXGraph.CreateInstance<CustomerMaint>();
      ((PXSelectBase<Customer>) instance.BAccount).Current = PXResultset<Customer>.op_Implicit(((PXSelectBase<Customer>) instance.BAccount).Search<PX.Objects.CR.BAccount.bAccountID>((object) ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current.CustomerID, Array.Empty<object>()));
      ((PXAction) instance.newPayment).Press();
    }
    return (IEnumerable) ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXButton(Category = "Document Processing")]
  public virtual IEnumerable PayDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<ARDocumentEnq.ARDocumentResult>) this.Documents).Current != null)
    {
      if (((PXSelectBase<ARDocumentEnq.ARDocumentResult>) this.Documents).Current.Status != "N" && ((PXSelectBase<ARDocumentEnq.ARDocumentResult>) this.Documents).Current.Status != "U" && ((PXSelectBase<ARDocumentEnq.ARDocumentResult>) this.Documents).Current.Status != "Y")
        throw new PXException("Only open documents can be selected for payment.");
      if (ARDocType.Payable(((PXSelectBase<ARDocumentEnq.ARDocumentResult>) this.Documents).Current.DocType).GetValueOrDefault())
      {
        ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
        ARInvoice doc = this.FindDoc<ARInvoice>(((PXSelectBase<ARDocumentEnq.ARDocumentResult>) this.Documents).Current);
        if (doc != null)
        {
          ((PXSelectBase<ARInvoice>) instance.Document).Current = doc;
          instance.PayInvoice(adapter);
        }
      }
      else
      {
        ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
        ARPayment arPayment = PXResultset<ARPayment>.op_Implicit(((PXSelectBase<ARPayment>) instance.Document).Search<ARPayment.refNbr>((object) ((PXSelectBase<ARDocumentEnq.ARDocumentResult>) this.Documents).Current.RefNbr, new object[1]
        {
          (object) ((PXSelectBase<ARDocumentEnq.ARDocumentResult>) this.Documents).Current.DocType
        }));
        if (arPayment != null)
        {
          ((PXSelectBase<ARPayment>) instance.Document).Current = arPayment;
          throw new PXRedirectRequiredException((PXGraph) instance, "View Document");
        }
      }
    }
    return (IEnumerable) ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  public virtual IEnumerable ARBalanceByCustomerReport(PXAdapter adapter)
  {
    ARDocumentEnq.ARDocumentFilter current = ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current;
    if (current != null)
    {
      Customer customer = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Current<ARDocumentEnq.ARDocumentFilter.customerID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      if (!string.IsNullOrEmpty(current.Period))
        dictionary["PeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(current.Period);
      dictionary["CustomerID"] = customer.AcctCD;
      dictionary["UseMasterCalendar"] = current.UseMasterCalendar.GetValueOrDefault() ? true.ToString() : false.ToString();
      if (current.OrgBAccountID.HasValue)
      {
        BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) current.OrgBAccountID
        }));
        dictionary["OrgBAccountID"] = baccountR?.AcctCD;
      }
      throw new PXReportRequiredException(dictionary, "AR632500", (PXBaseRedirectException.WindowMode) 3, "AR Balance by Customer", (CurrentLocalization) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  public virtual IEnumerable CustomerHistoryReport(PXAdapter adapter)
  {
    ARDocumentEnq.ARDocumentFilter current = ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current;
    if (current != null)
    {
      Customer customer = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Current<ARDocumentEnq.ARDocumentFilter.customerID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      if (!string.IsNullOrEmpty(current.Period))
      {
        dictionary["FromPeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(current.Period);
        dictionary["ToPeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(current.Period);
      }
      dictionary["CustomerID"] = customer.AcctCD;
      if (current.OrgBAccountID.HasValue)
      {
        BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) current.OrgBAccountID
        }));
        dictionary["OrgBAccountID"] = baccountR?.AcctCD;
      }
      throw new PXReportRequiredException(dictionary, "AR652000", (PXBaseRedirectException.WindowMode) 3, "Customer History", (CurrentLocalization) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  public virtual IEnumerable ARAgedPastDueReport(PXAdapter adapter)
  {
    ARDocumentEnq.ARDocumentFilter current = ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current;
    if (current != null)
    {
      Customer customer = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Current<ARDocumentEnq.ARDocumentFilter.customerID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary["CustomerID"] = customer.AcctCD;
      if (current.OrgBAccountID.HasValue)
      {
        BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) current.OrgBAccountID
        }));
        dictionary["OrgBAccountID"] = baccountR?.AcctCD;
      }
      throw new PXReportRequiredException(dictionary, "AR631000", (PXBaseRedirectException.WindowMode) 3, "AR Aging", (CurrentLocalization) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  public virtual IEnumerable ARAgedOutstandingReport(PXAdapter adapter)
  {
    ARDocumentEnq.ARDocumentFilter current = ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current;
    if (current != null)
    {
      Customer customer = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Current<ARDocumentEnq.ARDocumentFilter.customerID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary["CustomerID"] = customer.AcctCD;
      if (current.OrgBAccountID.HasValue)
      {
        BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) current.OrgBAccountID
        }));
        dictionary["OrgBAccountID"] = baccountR?.AcctCD;
      }
      throw new PXReportRequiredException(dictionary, "AR631500", (PXBaseRedirectException.WindowMode) 3, "AR Coming Due", (CurrentLocalization) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  public virtual IEnumerable ARRegisterReport(PXAdapter adapter)
  {
    ARDocumentEnq.ARDocumentFilter current = ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current;
    if (current != null)
    {
      Customer customer = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Current<ARDocumentEnq.ARDocumentFilter.customerID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      if (!string.IsNullOrEmpty(current.Period))
      {
        dictionary["StartPeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(current.Period);
        dictionary["EndPeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(current.Period);
      }
      dictionary["CustomerID"] = customer.AcctCD;
      if (current.OrgBAccountID.HasValue)
      {
        BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) current.OrgBAccountID
        }));
        dictionary["OrgBAccountID"] = baccountR?.AcctCD;
      }
      throw new PXReportRequiredException(dictionary, "AR621500", (PXBaseRedirectException.WindowMode) 3, "AR Register", (CurrentLocalization) null);
    }
    return adapter.Get();
  }

  public virtual IEnumerable documents()
  {
    if (((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current == null)
      return (IEnumerable) new List<object>();
    PXDelegateResult pxDelegateResult = this.SelectDetails();
    ((PXAction) this.viewDocument).SetEnabled(((List<object>) pxDelegateResult).Count > 0);
    return (IEnumerable) pxDelegateResult;
  }

  protected virtual IEnumerable filter()
  {
    ARDocumentEnq arDocumentEnq = this;
    PXCache cache = ((PXGraph) arDocumentEnq).Caches[typeof (ARDocumentEnq.ARDocumentFilter)];
    if (cache != null)
    {
      if (cache.Current is ARDocumentEnq.ARDocumentFilter current1)
      {
        if (current1.RefreshTotals.GetValueOrDefault())
        {
          current1.ClearSummary();
          foreach (ARDocumentEnq.ARDocumentResult selectDetail in (List<object>) arDocumentEnq.SelectDetails(true))
            arDocumentEnq.Aggregate(current1, selectDetail);
          current1.RefreshTotals = new bool?(false);
        }
        if (current1.CustomerID.HasValue)
        {
          ARCustomerBalanceEnq instance = PXGraph.CreateInstance<ARCustomerBalanceEnq>();
          ARCustomerBalanceEnq.ARHistoryFilter current = ((PXSelectBase<ARCustomerBalanceEnq.ARHistoryFilter>) instance.Filter).Current;
          ARCustomerBalanceEnq.Copy(current, current1);
          if (current.Period == null)
            current.Period = instance.GetLastActivityPeriod(current1.CustomerID, current1.IncludeChildAccounts.GetValueOrDefault());
          ((PXSelectBase<ARCustomerBalanceEnq.ARHistoryFilter>) instance.Filter).Update(current);
          ARCustomerBalanceEnq.ARHistorySummary aSrc = PXResultset<ARCustomerBalanceEnq.ARHistorySummary>.op_Implicit(((PXSelectBase<ARCustomerBalanceEnq.ARHistorySummary>) instance.Summary).Select(Array.Empty<object>()));
          arDocumentEnq.SetSummary(current1, aSrc);
        }
      }
      yield return cache.Current;
      cache.IsDirty = false;
    }
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Original Document")]
  protected virtual void ARDocumentResult_OrigRefNbr_CacheAttached(PXCache sender)
  {
  }

  public virtual void ARDocumentFilter_ARAcctID_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    if (!(e.Row is ARDocumentEnq.ARDocumentFilter row))
      return;
    ((CancelEventArgs) e).Cancel = true;
    int? nullable = new int?();
    row.ARAcctID = nullable;
  }

  public virtual void ARDocumentFilter_ARSubID_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    if (!(e.Row is ARDocumentEnq.ARDocumentFilter row))
      return;
    ((CancelEventArgs) e).Cancel = true;
    int? nullable = new int?();
    row.ARSubID = nullable;
  }

  public virtual void ARDocumentFilter_CuryID_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    if (!(e.Row is ARDocumentEnq.ARDocumentFilter row))
      return;
    ((CancelEventArgs) e).Cancel = true;
    row.CuryID = (string) null;
  }

  public virtual void ARDocumentFilter_SubCD_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void ARDocumentFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (cache.ObjectsEqual<ARDocumentEnq.ARDocumentFilter.orgBAccountID>(e.Row, e.OldRow) && cache.ObjectsEqual<ARDocumentEnq.ARDocumentFilter.organizationID>(e.Row, e.OldRow) && cache.ObjectsEqual<ARDocumentEnq.ARDocumentFilter.branchID>(e.Row, e.OldRow) && cache.ObjectsEqual<ARDocumentEnq.ARDocumentFilter.customerID>(e.Row, e.OldRow) && cache.ObjectsEqual<ARDocumentEnq.ARDocumentFilter.period>(e.Row, e.OldRow) && cache.ObjectsEqual<ARDocumentEnq.ARDocumentFilter.masterFinPeriodID>(e.Row, e.OldRow) && cache.ObjectsEqual<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>(e.Row, e.OldRow) && cache.ObjectsEqual<ARDocumentEnq.ARDocumentFilter.showAllDocs>(e.Row, e.OldRow) && cache.ObjectsEqual<ARDocumentEnq.ARDocumentFilter.includeUnreleased>(e.Row, e.OldRow) && cache.ObjectsEqual<ARDocumentEnq.ARDocumentFilter.aRAcctID>(e.Row, e.OldRow) && cache.ObjectsEqual<ARDocumentEnq.ARDocumentFilter.aRSubID>(e.Row, e.OldRow) && cache.ObjectsEqual<ARDocumentEnq.ARDocumentFilter.subCD>(e.Row, e.OldRow) && cache.ObjectsEqual<ARDocumentEnq.ARDocumentFilter.subCDWildcard>(e.Row, e.OldRow) && cache.ObjectsEqual<ARDocumentEnq.ARDocumentFilter.docType>(e.Row, e.OldRow) && cache.ObjectsEqual<ARDocumentEnq.ARDocumentFilter.includeChildAccounts>(e.Row, e.OldRow) && cache.ObjectsEqual<ARDocumentEnq.ARDocumentFilter.curyID>(e.Row, e.OldRow))
      return;
    (e.Row as ARDocumentEnq.ARDocumentFilter).RefreshTotals = new bool?(true);
  }

  public virtual void ARDocumentFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    ARDocumentEnq.ARDocumentFilter row = (ARDocumentEnq.ARDocumentFilter) e.Row;
    if (row == null)
      return;
    PXCache cache1 = ((PXSelectBase) this.Documents).Cache;
    bool flag1 = row.Period != null;
    bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();
    bool flag3 = !string.IsNullOrEmpty(row.CuryID) && row.CuryID != ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID;
    bool flag4 = !string.IsNullOrEmpty(row.CuryID) && row.CuryID == ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID;
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARDocumentFilter.showAllDocs>(cache, (object) row, !flag1);
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARDocumentFilter.includeChildAccounts>(cache, (object) row, PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>());
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARDocumentFilter.curyID>(cache, (object) row, flag2);
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARDocumentFilter.curyBalanceSummary>(cache, (object) row, flag2 & flag3);
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARDocumentFilter.curyDifference>(cache, (object) row, flag2 & flag3);
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARDocumentFilter.curyCustomerBalance>(cache, (object) row, flag2 & flag3);
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARDocumentFilter.curyCustomerRetainedBalance>(cache, (object) row, flag2 & flag3);
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARDocumentFilter.curyCustomerDepositsBalance>(cache, (object) row, flag2 & flag3);
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARDocumentResult.curyID>(cache1, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARDocumentResult.rGOLAmt>(cache1, (object) null, flag2 && !flag4);
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARDocumentResult.curyBegBalance>(cache1, (object) null, flag1 & flag2 && !flag4);
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARDocumentResult.begBalance>(cache1, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARDocumentResult.curyOrigDocAmt>(cache1, (object) null, flag2 && !flag4);
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARDocumentResult.curyDocBal>(cache1, (object) null, flag2 && !flag4);
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARDocumentResult.curyDiscActTaken>(cache1, (object) null, flag2 && !flag4);
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARDocumentResult.curyWOAmt>(cache1, (object) null, ((!flag2 ? 0 : (!flag4 ? 1 : 0)) & (flag1 ? 1 : 0)) != 0);
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARDocumentResult.curyRetainageTotal>(cache1, (object) null, flag2 && !flag4);
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARDocumentResult.curyOrigDocAmtWithRetainageTotal>(cache1, (object) null, flag2 && !flag4);
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARDocumentResult.curyRetainageUnreleasedAmt>(cache1, (object) null, flag2 && !flag4);
    PXUIFieldAttribute.SetVisible<ARDocumentEnq.ARDocumentResult.woAmt>(cache1, (object) null, flag1);
    Customer customer = (Customer) null;
    int? nullable = row.CustomerID;
    if (nullable.HasValue)
      customer = this.CustomerRepository.FindByID(row.CustomerID);
    ((PXAction) this.createInvoice).SetEnabled(customer != null && (customer.Status == "A" || customer.Status == "T"));
    bool flag5 = customer != null && customer.Status != "I";
    ((PXAction) this.createPayment).SetEnabled(flag5);
    ((PXAction) this.payDocument).SetEnabled(flag5);
    bool flag6 = PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
    PXUIFieldAttribute.SetRequired<ARDocumentEnq.ARDocumentFilter.orgBAccountID>(cache, flag6);
    int num;
    if (!flag6)
    {
      nullable = row.CustomerID;
      num = nullable.HasValue ? 1 : 0;
    }
    else
    {
      nullable = row.CustomerID;
      if (nullable.HasValue)
      {
        nullable = row.OrgBAccountID;
        num = nullable.HasValue ? 1 : 0;
      }
      else
        num = 0;
    }
    bool flag7 = num != 0;
    ((PXAction) this.aRBalanceByCustomerReport).SetEnabled(flag7);
    ((PXAction) this.customerHistoryReport).SetEnabled(flag7);
    ((PXAction) this.aRAgedPastDueReport).SetEnabled(flag7);
    ((PXAction) this.aRAgedOutstandingReport).SetEnabled(flag7);
    ((PXAction) this.aRRegisterReport).SetEnabled(flag7);
  }

  protected virtual PXDelegateResult SelectDetails(bool summarize = false)
  {
    ARDocumentEnq.ARDocumentFilter current1 = ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current;
    int?[] nullableArray = (int?[]) null;
    Dictionary<Tuple<string, string>, Decimal?> dictionary = (Dictionary<Tuple<string, string>, Decimal?>) null;
    bool flag1 = current1.Period != null;
    ARDocumentEnq.ARDocumentFilter current2 = ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current;
    if ((current2 != null ? (current2.CustomerID.HasValue ? 1 : 0) : 0) != 0)
    {
      ARDocumentEnq.ARDocumentFilter current3 = ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current;
      if ((current3 != null ? (current3.IncludeChildAccounts.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        nullableArray = CustomerFamilyHelper.GetCustomerFamily<PX.Objects.AR.Override.BAccount.consolidatingBAccountID>((PXGraph) this, ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current.CustomerID).Where<ExtendedCustomer>((Func<ExtendedCustomer, bool>) (customerInfo => customerInfo.BusinessAccount.BAccountID.HasValue)).Select<ExtendedCustomer, int?>((Func<ExtendedCustomer, int?>) (customerInfo => customerInfo.BusinessAccount.BAccountID)).ToArray<int?>();
        goto label_5;
      }
    }
    ARDocumentEnq.ARDocumentFilter current4 = ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current;
    if ((current4 != null ? (current4.CustomerID.HasValue ? 1 : 0) : 0) != 0)
      nullableArray = new int?[1]
      {
        ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current.CustomerID
      };
label_5:
    BqlCommand bqlCommand = ((PXSelectBase) new PXSelectReadonly<ARDocumentEnq.ARDocumentResult, Where<ARDocumentEnq.ARDocumentResult.customerID, In<Required<ARDocumentEnq.ARDocumentResult.customerID>>>, OrderBy<Asc<ARDocumentEnq.ARDocumentResult.docType, Asc<ARDocumentEnq.ARDocumentResult.refNbr>>>>((PXGraph) this)).View.BqlSelect;
    int? nullable1 = current1.OrgBAccountID;
    if (nullable1.HasValue)
      bqlCommand = bqlCommand.WhereAnd<Where<ARDocumentEnq.ARDocumentResult.branchID, Inside<Current<ARDocumentEnq.ARDocumentFilter.orgBAccountID>>>>();
    nullable1 = current1.ARAcctID;
    if (nullable1.HasValue)
      bqlCommand = !PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>() ? bqlCommand.WhereAnd<Where<ARDocumentEnq.ARDocumentResult.aRAccountID, Equal<Current<ARDocumentEnq.ARDocumentFilter.aRAcctID>>>>() : bqlCommand.WhereAnd<Where<ARDocumentEnq.ARDocumentResult.accountID, Equal<Current<ARDocumentEnq.ARDocumentFilter.aRAcctID>>>>();
    if (PXAccess.FeatureInstalled<FeaturesSet.subAccount>())
    {
      nullable1 = current1.ARSubID;
      if (nullable1.HasValue)
        bqlCommand = !PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>() ? bqlCommand.WhereAnd<Where<ARDocumentEnq.ARDocumentResult.aRSubID, Equal<Current<ARDocumentEnq.ARDocumentFilter.aRSubID>>>>() : bqlCommand.WhereAnd<Where<ARDocumentEnq.ARDocumentResult.subID, Equal<Current<ARDocumentEnq.ARDocumentFilter.aRSubID>>>>();
    }
    bool? nullable2 = current1.IncludeUnreleased;
    BqlCommand sel1 = nullable2.GetValueOrDefault() ? bqlCommand.WhereAnd<Where<ARDocumentEnq.ARDocumentResult.scheduled, Equal<False>, And<Where<ARDocumentEnq.ARDocumentResult.voided, Equal<False>, Or<ARDocumentEnq.ARDocumentResult.released, Equal<True>>>>>>() : bqlCommand.WhereAnd<Where<ARDocumentEnq.ARDocumentResult.released, Equal<True>>>();
    nullable2 = current1.ShowAllDocs;
    if (!nullable2.GetValueOrDefault())
      sel1 = sel1.WhereAnd<Where<ARDocumentEnq.ARDocumentResult.installmentCntr, IsNull>>();
    if (!SubCDUtils.IsSubCDEmpty(current1.SubCD))
      sel1 = BqlCommand.AppendJoin<InnerJoin<PX.Objects.GL.Sub, On<PX.Objects.GL.Sub.subID, Equal<ARDocumentEnq.ARDocumentResult.aRSubID>>>>(sel1).WhereAnd<Where<PX.Objects.GL.Sub.subCD, Like<Current<ARDocumentEnq.ARDocumentFilter.subCDWildcard>>>>();
    if (current1.DocType != null)
      sel1 = sel1.WhereAnd<Where<ARDocumentEnq.ARDocumentResult.docType, Equal<Current<ARDocumentEnq.ARDocumentFilter.docType>>>>();
    if (current1.CuryID != null)
      sel1 = sel1.WhereAnd<Where<ARDocumentEnq.ARDocumentResult.curyID, Equal<Current<ARDocumentEnq.ARDocumentFilter.curyID>>>>();
    int[] childBranchIds = PXAccess.GetChildBranchIDs(current1.OrganizationID, false);
    nullable1 = current1.BranchID;
    if (nullable1.HasValue)
    {
      sel1 = sel1.WhereAnd<Where<ARDocumentEnq.ARDocumentResult.branchID, Equal<Current<ARDocumentEnq.ARDocumentFilter.branchID>>>>();
    }
    else
    {
      nullable1 = current1.OrganizationID;
      if (nullable1.HasValue)
        sel1 = sel1.WhereAnd<Where<ARDocumentEnq.ARDocumentResult.branchID, In<Required<ARDocumentEnq.ARDocumentResult.branchID>>, And<MatchWithBranch<ARDocumentEnq.ARDocumentResult.branchID>>>>();
    }
    List<System.Type> typeList;
    if (!summarize)
    {
      typeList = new List<System.Type>()
      {
        typeof (ARDocumentEnq.ARDocumentResult)
      };
    }
    else
    {
      typeList = new List<System.Type>();
      typeList.Add(typeof (ARDocumentEnq.ARDocumentResult.released));
      typeList.Add(typeof (ARDocumentEnq.ARDocumentResult.curyOrigDocAmt));
      typeList.Add(typeof (ARDocumentEnq.ARDocumentResult.origDocAmt));
      typeList.Add(typeof (ARDocumentEnq.ARDocumentResult.curyDocBal));
      typeList.Add(typeof (ARDocumentEnq.ARDocumentResult.docBal));
      typeList.Add(typeof (ARDocumentEnq.ARDocumentResult.curyRetainageUnreleasedAmt));
      typeList.Add(typeof (ARDocumentEnq.ARDocumentResult.retainageUnreleasedAmt));
    }
    List<System.Type> restrictedFields = typeList;
    System.Type queryType = typeof (ARDocumentEnq.ARDocumentResult);
    BqlCommand sel2;
    if (flag1)
    {
      nullable2 = current1.UseMasterCalendar;
      BqlCommand sel3 = !nullable2.GetValueOrDefault() ? sel1.WhereAnd<Where<ARDocumentEnq.ARDocumentResult.finPeriodID, LessEqual<Current<ARDocumentEnq.ARDocumentFilter.period>>>>().WhereAnd<Where<ARDocumentEnq.ARDocumentResult.finPostPeriodID, IsNull, Or<ARDocumentEnq.ARDocumentResult.finPostPeriodID, LessEqual<Current<ARDocumentEnq.ARDocumentFilter.period>>>>>() : sel1.WhereAnd<Where<ARDocumentEnq.ARDocumentResult.tranPeriodID, LessEqual<Current<ARDocumentEnq.ARDocumentFilter.period>>>>().WhereAnd<Where<ARDocumentEnq.ARDocumentResult.tranPostPeriodID, IsNull, Or<ARDocumentEnq.ARDocumentResult.tranPostPeriodID, LessEqual<Current<ARDocumentEnq.ARDocumentFilter.period>>>>>();
      queryType = typeof (ARDocumentEnq.ARDocumentPeriodResult);
      System.Type aggregate = summarize ? typeof (PX.Data.Aggregate<GroupBy<ARDocumentEnq.ARDocumentPeriodResult.released, Sum<ARDocumentEnq.ARDocumentPeriodResult.curyDocBal, Sum<ARDocumentEnq.ARDocumentPeriodResult.docBal, Sum<ARDocumentEnq.ARDocumentPeriodResult.curyRetainageUnreleasedAmt, Sum<ARDocumentEnq.ARDocumentPeriodResult.retainageUnreleasedAmt>>>>>>) : typeof (PX.Data.Aggregate<GroupBy<ARDocumentEnq.ARDocumentPeriodResult.docType, GroupBy<ARDocumentEnq.ARDocumentPeriodResult.refNbr, GroupBy<ARDocumentEnq.ARDocumentPeriodResult.accountID, Sum<ARDocumentEnq.ARDocumentPeriodResult.curyBegBalance, Sum<ARDocumentEnq.ARDocumentPeriodResult.begBalance, Sum<ARDocumentEnq.ARDocumentPeriodResult.curyDocBal, Sum<ARDocumentEnq.ARDocumentPeriodResult.docBal, Sum<ARDocumentEnq.ARDocumentPeriodResult.curyRetainageUnreleasedAmt, Sum<ARDocumentEnq.ARDocumentPeriodResult.retainageUnreleasedAmt, Sum<ARDocumentEnq.ARDocumentPeriodResult.curyDiscActTaken, Sum<ARDocumentEnq.ARDocumentPeriodResult.discActTaken, Sum<ARDocumentEnq.ARDocumentPeriodResult.curyWOAmt, Sum<ARDocumentEnq.ARDocumentPeriodResult.woAmt, Sum<ARDocumentEnq.ARDocumentPeriodResult.rGOLAmt, Sum<ARDocumentEnq.ARDocumentPeriodResult.aRTurnover>>>>>>>>>>>>>>>, Having<BqlChainableConditionHavingBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FunctionWrapper<Sum<ARDocumentEnq.ARDocumentPeriodResult.begBalance>>, NotEqual<FunctionWrapper<Zero>>>>>, Or<HavingConditionWrapper<BqlAggregatedOperand<Sum<ARDocumentEnq.ARDocumentPeriodResult.docBal>, IBqlDecimal>.IsNotEqual<Zero>>>>, Or<HavingConditionWrapper<BqlAggregatedOperand<Sum<ARDocumentEnq.ARDocumentPeriodResult.retainageUnreleasedAmt>, IBqlDecimal>.IsNotEqual<Zero>>>>>.Or<BqlChainableConditionHavingMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FunctionWrapper<Sum<ARDocumentEnq.ARDocumentPeriodResult.turn>>, NotEqual<FunctionWrapper<Zero>>>>>, Or<HavingConditionWrapper<BqlAggregatedOperand<Sum<ARDocumentEnq.ARDocumentPeriodResult.retainageUnreleasedAmt>, IBqlDecimal>.IsNotEqual<Zero>>>>>.Or<BqlAggregatedOperand<Max<ARDocumentEnq.ARDocumentPeriodResult.released>, IBqlBool>.IsEqual<False>>>>>);
      nullable2 = current1.IncludeGLTurnover;
      if (nullable2.GetValueOrDefault() && !summarize)
      {
        dictionary = this.SelectGLTurn();
        queryType = typeof (ARDocumentEnq.GLDocumentPeriodResult);
        aggregate = typeof (PX.Data.Aggregate<GroupBy<ARDocumentEnq.GLDocumentPeriodResult.docType, GroupBy<ARDocumentEnq.GLDocumentPeriodResult.refNbr, Sum<ARDocumentEnq.GLDocumentPeriodResult.aRTurnover>>>>);
      }
      restrictedFields = new List<System.Type>() { queryType };
      sel2 = this.AdjustBqlCommand(queryType, sel3, summarize, aggregate, ref restrictedFields);
    }
    else if (PXAccess.FeatureInstalled<FeaturesSet.vATRecognitionOnPrepaymentsAR>())
    {
      queryType = typeof (ARDocumentEnq.ARDocumentPPIPeriodResult);
      nullable2 = current1.ShowAllDocs;
      bool flag2 = false;
      if (nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue)
        sel1 = sel1.WhereAnd<Where<ARDocumentEnq.ARDocumentResult.openDoc, Equal<True>>>();
      System.Type aggregate = summarize ? typeof (PX.Data.Aggregate<GroupBy<ARDocumentEnq.ARDocumentPPIPeriodResult.released, Sum<ARDocumentEnq.ARDocumentPPIPeriodResult.curyDocBal, Sum<ARDocumentEnq.ARDocumentPPIPeriodResult.docBal, Sum<ARDocumentEnq.ARDocumentPPIPeriodResult.curyRetainageUnreleasedAmt, Sum<ARDocumentEnq.ARDocumentPPIPeriodResult.retainageUnreleasedAmt>>>>>>) : typeof (PX.Data.Aggregate<GroupBy<ARDocumentEnq.ARDocumentPPIPeriodResult.docType, GroupBy<ARDocumentEnq.ARDocumentPPIPeriodResult.refNbr, GroupBy<ARDocumentEnq.ARDocumentPPIPeriodResult.accountID, Sum<ARDocumentEnq.ARDocumentPPIPeriodResult.curyOrigDocAmt, Sum<ARDocumentEnq.ARDocumentPPIPeriodResult.origDocAmt, Sum<ARDocumentEnq.ARDocumentPPIPeriodResult.curyDocBal, Sum<ARDocumentEnq.ARDocumentPPIPeriodResult.docBal, Sum<ARDocumentEnq.ARDocumentPPIPeriodResult.curyRetainageUnreleasedAmt, Sum<ARDocumentEnq.ARDocumentPPIPeriodResult.retainageUnreleasedAmt, Sum<ARDocumentEnq.ARDocumentPPIPeriodResult.curyOrigDocAmtWithRetainageTotal, Sum<ARDocumentEnq.ARDocumentPPIPeriodResult.origDocAmtWithRetainageTotal, Sum<ARDocumentEnq.ARDocumentPPIPeriodResult.curyDiscActTaken, Sum<ARDocumentEnq.ARDocumentPPIPeriodResult.discActTaken, Sum<ARDocumentEnq.ARDocumentPPIPeriodResult.curyWOAmt, Sum<ARDocumentEnq.ARDocumentPPIPeriodResult.woAmt, Sum<ARDocumentEnq.ARDocumentPPIPeriodResult.rGOLAmt, Sum<ARDocumentEnq.ARDocumentPPIPeriodResult.aRTurnover>>>>>>>>>>>>>>>>>>);
      restrictedFields = new List<System.Type>() { queryType };
      sel2 = this.AdjustBqlCommand(queryType, sel1, summarize, aggregate, ref restrictedFields);
    }
    else
    {
      nullable2 = current1.ShowAllDocs;
      bool flag3 = false;
      if (nullable2.GetValueOrDefault() == flag3 & nullable2.HasValue)
        sel1 = sel1.WhereAnd<Where<ARDocumentEnq.ARDocumentResult.openDoc, Equal<True>>>();
      System.Type aggregate = summarize ? typeof (PX.Data.Aggregate<GroupBy<ARDocumentEnq.ARDocumentResult.released, Sum<ARDocumentEnq.ARDocumentResult.curyOrigDocAmt, Sum<ARDocumentEnq.ARDocumentResult.origDocAmt, Sum<ARDocumentEnq.ARDocumentResult.curyDocBal, Sum<ARDocumentEnq.ARDocumentResult.docBal, Sum<ARDocumentEnq.ARDocumentResult.curyRetainageUnreleasedAmt, Sum<ARDocumentEnq.ARDocumentResult.retainageUnreleasedAmt>>>>>>>>) : (System.Type) null;
      sel2 = this.AdjustBqlCommand(queryType, sel1, summarize, aggregate, ref restrictedFields);
    }
    PXResultMapper pxResultMapper = new PXResultMapper((PXGraph) this, new Dictionary<System.Type, System.Type>()
    {
      [typeof (ARDocumentEnq.ARDocumentResult)] = queryType
    }, new System.Type[1]
    {
      typeof (ARDocumentEnq.ARDocumentResult)
    });
    if (flag1)
      pxResultMapper.ExtFilters.Add<System.Type>((IEnumerable<System.Type>) new System.Type[11]
      {
        typeof (ARDocumentEnq.ARDocumentResult.curyBegBalance),
        typeof (ARDocumentEnq.ARDocumentResult.begBalance),
        typeof (ARDocumentEnq.ARDocumentResult.curyDocBal),
        typeof (ARDocumentEnq.ARDocumentResult.docBal),
        typeof (ARDocumentEnq.ARDocumentResult.curyRetainageUnreleasedAmt),
        typeof (ARDocumentEnq.ARDocumentResult.retainageUnreleasedAmt),
        typeof (ARDocumentEnq.ARDocumentResult.curyDiscActTaken),
        typeof (ARDocumentEnq.ARDocumentResult.discActTaken),
        typeof (ARDocumentEnq.ARDocumentResult.curyWOAmt),
        typeof (ARDocumentEnq.ARDocumentResult.woAmt),
        typeof (ARDocumentEnq.ARDocumentResult.rGOLAmt)
      });
    int startRow = PXView.StartRow;
    int num = 0;
    PXDelegateResult delegateResult = pxResultMapper.CreateDelegateResult(!summarize && dictionary == null);
    PXView documentView = this.CreateDocumentView(sel2, summarize);
    using (new PXFieldScope(documentView, (IEnumerable<System.Type>) restrictedFields, true))
    {
      List<object> objectList;
      if (!summarize && dictionary == null)
        objectList = documentView.Select((object[]) null, new object[2]
        {
          (object) nullableArray,
          (object) childBranchIds
        }, PXView.Searches, pxResultMapper.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(pxResultMapper.Filters), ref startRow, PXView.MaximumRows, ref num);
      else
        objectList = documentView.SelectMulti(new object[2]
        {
          (object) nullableArray,
          (object) childBranchIds
        });
      foreach (object obj1 in objectList)
      {
        object obj2 = obj1 is PXResult pxResult ? pxResult[0] : obj1;
        if (obj2 is ARDocumentEnq.ARDocumentResult)
        {
          ARDocumentEnq.ARDocumentResult arDocumentResult = (ARDocumentEnq.ARDocumentResult) obj2;
          if (arDocumentResult != null)
          {
            nullable1 = arDocumentResult.AccountID;
            if (nullable1.HasValue)
            {
              nullable1 = arDocumentResult.SubID;
              if (nullable1.HasValue)
              {
                arDocumentResult.ARAccountID = arDocumentResult.AccountID;
                arDocumentResult.ARSubID = arDocumentResult.SubID;
              }
            }
          }
          ((List<object>) delegateResult).Add((object) arDocumentResult);
        }
        else
        {
          ARDocumentEnq.ARDocumentResult instance = (ARDocumentEnq.ARDocumentResult) ((PXSelectBase) this.Documents).Cache.CreateInstance();
          foreach (string field in (List<string>) ((PXSelectBase) this.Documents).Cache.Fields)
            ((PXSelectBase) this.Documents).Cache.SetValue((object) instance, field, documentView.Cache.GetValue(obj2, field));
          nullable1 = instance.AccountID;
          if (nullable1.HasValue)
          {
            nullable1 = instance.SubID;
            if (nullable1.HasValue)
            {
              instance.ARAccountID = instance.AccountID;
              instance.ARSubID = instance.SubID;
            }
          }
          instance.GLTurnover = new Decimal?(0M);
          Decimal? nullable3;
          if (dictionary != null && dictionary.Count != 0 && dictionary != null && dictionary.TryGetValue(new Tuple<string, string>(instance.DocType, instance.RefNbr), out nullable3))
            instance.GLTurnover = new Decimal?(nullable3.GetValueOrDefault());
          ((List<object>) delegateResult).Add((object) instance);
        }
      }
    }
    return delegateResult;
  }

  protected virtual PXView CreateDocumentView(BqlCommand sel, bool summarize)
  {
    return new PXView((PXGraph) this, true, sel);
  }

  protected virtual BqlCommand AdjustBqlCommand(
    System.Type queryType,
    BqlCommand sel,
    bool summarize,
    System.Type aggregate,
    ref List<System.Type> restrictedFields)
  {
    if (queryType == typeof (ARDocumentEnq.ARDocumentResult) && aggregate == (System.Type) null && !summarize)
      return sel;
    List<System.Type> types = new List<System.Type>((IEnumerable<System.Type>) BqlCommand.Decompose(sel.GetSelectType()));
    if (queryType != typeof (ARDocumentEnq.ARDocumentResult))
      this.ChangeQueryType(types, queryType);
    if (aggregate != (System.Type) null)
    {
      this.AddAggregate(types, aggregate);
      this.AdjustSelectAddAggregate(types);
    }
    if (summarize)
    {
      this.RemoveOrderBy(types);
      this.AdjustSelectRemoveOrderby(types);
    }
    sel = BqlCommand.CreateInstance(new System.Type[1]
    {
      BqlCommand.Compose(types.ToArray())
    });
    return sel;
  }

  protected virtual Dictionary<Tuple<string, string>, Decimal?> SelectGLTurn()
  {
    BqlCommand bqlSelect1 = ((PXSelectBase) new PXSelectGroupBy<ARTranPost, Where<ARTranPost.accountID, IsNotNull>, PX.Data.Aggregate<GroupBy<ARTranPost.accountID>>>((PXGraph) this)).View.BqlSelect;
    BqlCommand bqlSelect2 = ((PXSelectBase) new PXSelect<ARDocumentEnq.GLTran, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.GLTran.module, In3<BatchModule.moduleGL, BatchModule.moduleAR>>>>, And<BqlOperand<ARDocumentEnq.GLTran.branchID, IBqlInt>.IsEqual<BqlField<ARDocumentEnq.ARDocumentFilter.branchID, IBqlInt>.FromCurrent>>>, And<BqlOperand<ARDocumentEnq.GLTran.referenceID, IBqlInt>.IsEqual<BqlField<ARDocumentEnq.ARDocumentFilter.customerID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<ARDocumentEnq.GLTran.posted, IBqlBool>.IsEqual<True>>>>((PXGraph) this)).View.BqlSelect;
    BqlCommand bqlCommand1;
    BqlCommand bqlCommand2;
    if (((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current.UseMasterCalendar.GetValueOrDefault())
    {
      bqlCommand1 = bqlSelect1.WhereAnd<Where<ARTranPost.tranPeriodID, Equal<Current<ARDocumentEnq.ARDocumentFilter.period>>>>();
      bqlCommand2 = bqlSelect2.WhereAnd<Where<PX.Objects.GL.GLTran.tranPeriodID, Equal<Current<ARDocumentEnq.ARDocumentFilter.period>>>>();
    }
    else
    {
      bqlCommand1 = bqlSelect1.WhereAnd<Where<ARTranPost.finPeriodID, Equal<Current<ARDocumentEnq.ARDocumentFilter.period>>>>();
      bqlCommand2 = bqlSelect2.WhereAnd<Where<PX.Objects.GL.GLTran.finPeriodID, Equal<Current<ARDocumentEnq.ARDocumentFilter.period>>>>();
    }
    BqlCommand bqlCommand3 = (((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) this.Filter).Current.ARAcctID.HasValue ? bqlCommand2.WhereAnd<Where<ARDocumentEnq.GLTran.accountID, Equal<Current<ARDocumentEnq.ARDocumentFilter.aRAcctID>>>>() : bqlCommand2.WhereAnd<Where<BqlOperand<ARDocumentEnq.GLTran.accountID, IBqlInt>.IsInSubselect<FbqlSelect<SelectFromBase<ARTranPost, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTranPost.tranType, Equal<ARDocumentEnq.GLTran.tranType>>>>>.And<BqlOperand<ARTranPost.tranRefNbr, IBqlString>.IsEqual<ARDocumentEnq.GLTran.refNbr>>>, ARTranPost>.SearchFor<ARTranPost.accountID>>>>()).AggregateNew<PX.Data.Aggregate<GroupBy<ARDocumentEnq.GLTran.tranType, GroupBy<ARDocumentEnq.GLTran.refNbr, Sum<ARDocumentEnq.GLTran.gLTurnover>>>>>();
    List<System.Type> typeList = new List<System.Type>()
    {
      typeof (ARDocumentEnq.GLTran.tranType),
      typeof (ARDocumentEnq.GLTran.refNbr),
      typeof (ARDocumentEnq.GLTran.gLTurnover)
    };
    PXView pxView = new PXView((PXGraph) this, true, bqlCommand3);
    using (new PXFieldScope(pxView, (IEnumerable<System.Type>) typeList, true))
      return GraphHelper.RowCast<ARDocumentEnq.GLTran>((IEnumerable) pxView.SelectMulti(Array.Empty<object>()).AsEnumerable<object>()).ToDictionary<ARDocumentEnq.GLTran, Tuple<string, string>, Decimal?>((Func<ARDocumentEnq.GLTran, Tuple<string, string>>) (t => new Tuple<string, string>(t.TranType, t.RefNbr)), (Func<ARDocumentEnq.GLTran, Decimal?>) (t => t.GLTurnover));
  }

  protected virtual void SetSummary(
    ARDocumentEnq.ARDocumentFilter aDest,
    ARCustomerBalanceEnq.ARHistorySummary aSrc)
  {
    aDest.CustomerBalance = aSrc.BalanceSummary;
    aDest.CustomerDepositsBalance = aSrc.DepositsSummary;
    aDest.CuryCustomerBalance = aSrc.CuryBalanceSummary;
    aDest.CuryCustomerDepositsBalance = aSrc.CuryDepositsSummary;
  }

  protected virtual void Aggregate(
    ARDocumentEnq.ARDocumentFilter aDest,
    ARDocumentEnq.ARDocumentResult aSrc)
  {
    ARDocumentEnq.ARDocumentFilter arDocumentFilter1 = aDest;
    Decimal? nullable = arDocumentFilter1.BalanceSummary;
    Decimal valueOrDefault1 = aSrc.DocBal.GetValueOrDefault();
    arDocumentFilter1.BalanceSummary = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
    ARDocumentEnq.ARDocumentFilter arDocumentFilter2 = aDest;
    nullable = arDocumentFilter2.CuryBalanceSummary;
    Decimal valueOrDefault2 = aSrc.CuryDocBal.GetValueOrDefault();
    arDocumentFilter2.CuryBalanceSummary = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
    ARDocumentEnq.ARDocumentFilter arDocumentFilter3 = aDest;
    nullable = arDocumentFilter3.CustomerRetainedBalance;
    Decimal valueOrDefault3 = aSrc.RetainageUnreleasedAmt.GetValueOrDefault();
    arDocumentFilter3.CustomerRetainedBalance = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault3) : new Decimal?();
    ARDocumentEnq.ARDocumentFilter arDocumentFilter4 = aDest;
    nullable = arDocumentFilter4.CuryCustomerRetainedBalance;
    Decimal valueOrDefault4 = aSrc.CuryRetainageUnreleasedAmt.GetValueOrDefault();
    arDocumentFilter4.CuryCustomerRetainedBalance = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault4) : new Decimal?();
  }

  protected TDoc FindDoc<TDoc>(ARDocumentEnq.ARDocumentResult aRes) where TDoc : PX.Objects.AR.ARRegister, new()
  {
    return ARDocumentEnq.FindDoc<TDoc>((PXGraph) this, aRes.DocType, aRes.RefNbr);
  }

  protected virtual void AdjustSelectRemoveOrderbyAddAggregate(List<System.Type> types)
  {
    if (types[0] == typeof (Select<,,>))
      types[0] = typeof (Select4<,,>);
    if (!(types[0] == typeof (Select2<,,,>)))
      return;
    types[0] = typeof (Select5<,,,>);
  }

  protected virtual void AdjustSelectRemoveOrderby(List<System.Type> types)
  {
    if (types[0] == typeof (Select4<,,,>))
      types[0] = typeof (Select4<,,>);
    if (!(types[0] == typeof (Select5<,,,,>)))
      return;
    types[0] = typeof (Select5<,,,>);
  }

  protected virtual void AdjustSelectAddAggregate(List<System.Type> types)
  {
    if (types[0] == typeof (Select<,,>))
      types[0] = typeof (Select4<,,,>);
    if (!(types[0] == typeof (Select2<,,,>)))
      return;
    types[0] = typeof (Select5<,,,,>);
  }

  protected virtual void AddAggregate(List<System.Type> types, System.Type aggregate)
  {
    int index = types.IndexOf(typeof (OrderBy<>));
    if (index != -1)
      types.Insert(index, aggregate);
    else
      types.Add(aggregate);
  }

  protected virtual void RemoveOrderBy(List<System.Type> types)
  {
    int index = types.IndexOf(typeof (OrderBy<>));
    types.RemoveRange(index, types.Count - index);
  }

  protected virtual void ChangeQueryType(List<System.Type> types, System.Type queryType)
  {
    for (int index = 0; index < types.Count; ++index)
    {
      System.Type type = types[index];
      if (type == typeof (ARDocumentEnq.ARDocumentResult))
        types[index] = queryType;
      else if (type.DeclaringType == typeof (ARDocumentEnq.ARDocumentResult))
      {
        System.Type nestedType = queryType.GetNestedType(type.Name);
        types[index] = nestedType;
      }
    }
  }

  public static TDoc FindDoc<TDoc>(PXGraph aGraph, string aDocType, string apRefNbr) where TDoc : PX.Objects.AR.ARRegister, new()
  {
    return PXResultset<TDoc>.op_Implicit(PXSelectBase<TDoc, PXSelect<TDoc, Where<PX.Objects.AR.ARRegister.docType, Equal<Required<PX.Objects.AR.ARRegister.docType>>, And<PX.Objects.AR.ARRegister.refNbr, Equal<Required<PX.Objects.AR.ARRegister.refNbr>>>>>.Config>.Select(aGraph, new object[2]
    {
      (object) aDocType,
      (object) apRefNbr
    }));
  }

  [Serializable]
  public class ARDocumentFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _CustomerID;
    protected int? _ARAcctID;
    protected int? _ARSubID;
    protected string _SubCD;
    protected string _DocType;
    protected bool? _ShowAllDocs;
    protected bool? _IncludeUnreleased;
    protected string _CuryID;
    protected Decimal? _CuryBalanceSummary;
    protected Decimal? _BalanceSummary;
    protected Decimal? _CuryCustomerBalance;
    protected Decimal? _CustomerBalance;
    protected Decimal? _CuryCustomerDepositsBalance;
    protected Decimal? _CustomerDepositsBalance;
    protected bool? _IncludeGLTurnover;

    [Organization(false, Required = false)]
    public int? OrganizationID { get; set; }

    [BranchOfOrganization(typeof (ARDocumentEnq.ARDocumentFilter.organizationID), false, null, null)]
    public int? BranchID { get; set; }

    [OrganizationTree(typeof (ARDocumentEnq.ARDocumentFilter.organizationID), typeof (ARDocumentEnq.ARDocumentFilter.branchID), null, false)]
    public int? OrgBAccountID { get; set; }

    [PXDefault]
    [Customer(DescriptionField = typeof (Customer.acctName))]
    public virtual int? CustomerID
    {
      get => this._CustomerID;
      set => this._CustomerID = value;
    }

    [Account(null, typeof (Search5<PX.Objects.GL.Account.accountID, InnerJoin<ARHistory, On<PX.Objects.GL.Account.accountID, Equal<ARHistory.accountID>>>, Where<Match<Current<AccessInfo.userName>>>, PX.Data.Aggregate<GroupBy<PX.Objects.GL.Account.accountID>>>), DisplayName = "AR Account", DescriptionField = typeof (PX.Objects.GL.Account.description))]
    public virtual int? ARAcctID
    {
      get => this._ARAcctID;
      set => this._ARAcctID = value;
    }

    [SubAccount(DisplayName = "AR Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
    public virtual int? ARSubID
    {
      get => this._ARSubID;
      set => this._ARSubID = value;
    }

    [PXDBString(30, IsUnicode = true)]
    [PXUIField]
    [PXDimension("SUBACCOUNT", ValidComboRequired = false)]
    public virtual string SubCD
    {
      get => this._SubCD;
      set => this._SubCD = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Use Master Calendar")]
    [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.multipleCalendarsSupport>))]
    public bool? UseMasterCalendar { get; set; }

    [AnyPeriodFilterable(null, null, typeof (ARDocumentEnq.ARDocumentFilter.branchID), null, typeof (ARDocumentEnq.ARDocumentFilter.organizationID), typeof (ARDocumentEnq.ARDocumentFilter.useMasterCalendar), null, false, null, null)]
    [PXUIField]
    public virtual string Period { get; set; }

    [Obsolete("This is an absolete field. It will be removed in 2019R2")]
    [PeriodID(null, null, null, true)]
    public virtual string MasterFinPeriodID { get; set; }

    [PXDBString(3, IsFixed = true)]
    [PXDefault]
    [ARDocType.List]
    [PXUIField(DisplayName = "Type")]
    public virtual string DocType
    {
      get => this._DocType;
      set => this._DocType = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Show All Documents")]
    public virtual bool? ShowAllDocs
    {
      get => this._ShowAllDocs;
      set => this._ShowAllDocs = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Include Unreleased Documents")]
    public virtual bool? IncludeUnreleased
    {
      get => this._IncludeUnreleased;
      set => this._IncludeUnreleased = value;
    }

    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID), CacheGlobal = true)]
    [PXUIField(DisplayName = "Currency")]
    public virtual string CuryID
    {
      get => this._CuryID;
      set => this._CuryID = value;
    }

    [PXDBString(30, IsUnicode = true)]
    public virtual string SubCDWildcard
    {
      get => SubCDUtils.CreateSubCDWildcard(this._SubCD, "SUBACCOUNT");
    }

    [PXDBBool]
    [PXDefault(true)]
    public bool? RefreshTotals { get; set; }

    [CurySymbol(null, null, typeof (ARDocumentEnq.ARDocumentFilter.curyID), null, null, null, "Balance by Documents", true, false)]
    [PXCury(typeof (ARDocumentEnq.ARDocumentFilter.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Balance by Documents (Currency)", Enabled = false)]
    public virtual Decimal? CuryBalanceSummary
    {
      get => this._CuryBalanceSummary;
      set => this._CuryBalanceSummary = value;
    }

    [CurySymbol(null, null, null, null, null, typeof (ARDocumentEnq.ARDocumentFilter.customerID), null, false, false)]
    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Balance by Documents", Enabled = false)]
    public virtual Decimal? BalanceSummary
    {
      get => this._BalanceSummary;
      set => this._BalanceSummary = value;
    }

    [CurySymbol(null, null, typeof (ARDocumentEnq.ARDocumentFilter.curyID), null, null, null, "Current Balance", true, false)]
    [PXCury(typeof (ARDocumentEnq.ARDocumentFilter.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Current Balance (Currency)", Enabled = false)]
    public virtual Decimal? CuryCustomerBalance
    {
      get => this._CuryCustomerBalance;
      set => this._CuryCustomerBalance = value;
    }

    [CurySymbol(null, null, null, null, null, typeof (ARDocumentEnq.ARDocumentFilter.customerID), null, false, false)]
    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Current Balance", Enabled = false)]
    public virtual Decimal? CustomerBalance
    {
      get => this._CustomerBalance;
      set => this._CustomerBalance = value;
    }

    [CurySymbol(null, null, typeof (ARDocumentEnq.ARDocumentFilter.curyID), null, null, null, "Retained Balance", true, false)]
    [PXCury(typeof (ARDocumentEnq.ARDocumentFilter.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Retained Balance (Currency)", Enabled = false, FieldClass = "Retainage")]
    public virtual Decimal? CuryCustomerRetainedBalance { get; set; }

    [CurySymbol(null, null, null, null, null, typeof (ARDocumentEnq.ARDocumentFilter.customerID), null, false, false)]
    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Retained Balance", Enabled = false, FieldClass = "Retainage")]
    public virtual Decimal? CustomerRetainedBalance { get; set; }

    [CurySymbol(null, null, typeof (ARDocumentEnq.ARDocumentFilter.curyID), null, null, null, "Prepayment Balance", true, false)]
    [PXCury(typeof (ARDocumentEnq.ARDocumentFilter.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Prepayments Balance (Currency)", Enabled = false)]
    public virtual Decimal? CuryCustomerDepositsBalance
    {
      get => this._CuryCustomerDepositsBalance;
      set => this._CuryCustomerDepositsBalance = value;
    }

    [CurySymbol(null, null, null, null, null, typeof (ARDocumentEnq.ARDocumentFilter.customerID), null, false, false)]
    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Prepayment Balance", Enabled = false)]
    public virtual Decimal? CustomerDepositsBalance
    {
      get => this._CustomerDepositsBalance;
      set => this._CustomerDepositsBalance = value;
    }

    [CurySymbol(null, null, typeof (ARDocumentEnq.ARDocumentFilter.curyID), null, null, null, "Balance Discrepancy", true, false)]
    [PXCury(typeof (ARDocumentEnq.ARDocumentFilter.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Balance Discrepancy (Currency)", Enabled = false)]
    public virtual Decimal? CuryDifference
    {
      [PXDependsOnFields(new System.Type[] {typeof (ARDocumentEnq.ARDocumentFilter.curyCustomerBalance), typeof (ARDocumentEnq.ARDocumentFilter.curyBalanceSummary), typeof (ARDocumentEnq.ARDocumentFilter.curyCustomerDepositsBalance)})] get
      {
        Decimal? curyCustomerBalance = this._CuryCustomerBalance;
        Decimal? curyDifference = this._CuryBalanceSummary;
        Decimal? nullable = curyCustomerBalance.HasValue & curyDifference.HasValue ? new Decimal?(curyCustomerBalance.GetValueOrDefault() - curyDifference.GetValueOrDefault()) : new Decimal?();
        Decimal? customerDepositsBalance = this._CuryCustomerDepositsBalance;
        if (nullable.HasValue & customerDepositsBalance.HasValue)
          return new Decimal?(nullable.GetValueOrDefault() + customerDepositsBalance.GetValueOrDefault());
        curyDifference = new Decimal?();
        return curyDifference;
      }
    }

    [CurySymbol(null, null, null, null, null, typeof (ARDocumentEnq.ARDocumentFilter.customerID), null, false, false)]
    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Balance Discrepancy", Enabled = false)]
    public virtual Decimal? Difference
    {
      [PXDependsOnFields(new System.Type[] {typeof (ARDocumentEnq.ARDocumentFilter.customerBalance), typeof (ARDocumentEnq.ARDocumentFilter.balanceSummary), typeof (ARDocumentEnq.ARDocumentFilter.customerDepositsBalance)})] get
      {
        Decimal? customerBalance = this._CustomerBalance;
        Decimal? difference = this._BalanceSummary;
        Decimal? nullable = customerBalance.HasValue & difference.HasValue ? new Decimal?(customerBalance.GetValueOrDefault() - difference.GetValueOrDefault()) : new Decimal?();
        Decimal? customerDepositsBalance = this._CustomerDepositsBalance;
        if (nullable.HasValue & customerDepositsBalance.HasValue)
          return new Decimal?(nullable.GetValueOrDefault() + customerDepositsBalance.GetValueOrDefault());
        difference = new Decimal?();
        return difference;
      }
    }

    [PXDBBool]
    [PXDefault(typeof (Search<FeaturesSet.parentChildAccount>))]
    [PXUIField(DisplayName = "Include Child Accounts")]
    public virtual bool? IncludeChildAccounts { get; set; }

    [PXDBBool]
    [PXDefault(false)]
    public virtual bool? IncludeGLTurnover
    {
      get => this._IncludeGLTurnover;
      set => this._IncludeGLTurnover = value;
    }

    public virtual void ClearSummary()
    {
      this.CustomerBalance = new Decimal?(0M);
      this.BalanceSummary = new Decimal?(0M);
      this.CustomerDepositsBalance = new Decimal?(0M);
      this.CuryCustomerBalance = new Decimal?(0M);
      this.CuryBalanceSummary = new Decimal?(0M);
      this.CuryCustomerDepositsBalance = new Decimal?(0M);
      this.CuryCustomerRetainedBalance = new Decimal?(0M);
      this.CustomerRetainedBalance = new Decimal?(0M);
    }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.organizationID>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.branchID>
    {
    }

    public abstract class orgBAccountID : IBqlField, IBqlOperand
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.customerID>
    {
    }

    public abstract class aRAcctID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.aRAcctID>
    {
    }

    public abstract class aRSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.aRSubID>
    {
    }

    public abstract class subCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.subCD>
    {
    }

    public abstract class useMasterCalendar : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.useMasterCalendar>
    {
    }

    public abstract class period : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.period>
    {
    }

    public abstract class masterFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.masterFinPeriodID>
    {
    }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.docType>
    {
    }

    public abstract class showAllDocs : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.showAllDocs>
    {
    }

    public abstract class includeUnreleased : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.includeUnreleased>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.curyID>
    {
    }

    public abstract class subCDWildcard : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.subCDWildcard>
    {
    }

    public abstract class refreshTotals : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.refreshTotals>
    {
    }

    public abstract class curyBalanceSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.curyBalanceSummary>
    {
    }

    public abstract class balanceSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.balanceSummary>
    {
    }

    public abstract class curyCustomerBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.curyCustomerBalance>
    {
    }

    public abstract class customerBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.customerBalance>
    {
    }

    public abstract class curyCustomerRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.curyCustomerRetainedBalance>
    {
    }

    public abstract class customerRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.customerRetainedBalance>
    {
    }

    public abstract class curyCustomerDepositsBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.curyCustomerDepositsBalance>
    {
    }

    public abstract class customerDepositsBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.customerDepositsBalance>
    {
    }

    public abstract class curyDifference : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.curyDifference>
    {
    }

    public abstract class difference : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.difference>
    {
    }

    public abstract class includeChildAccounts : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.includeChildAccounts>
    {
    }

    public abstract class includeGLTurnover : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentFilter.includeGLTurnover>
    {
    }
  }

  [PXPrimaryGraph(typeof (ARDocumentEnq), Filter = typeof (ARDocumentEnq.ARDocumentFilter))]
  [PXCacheName("AR History for Report")]
  [Serializable]
  public class ARHistoryForReport : ARHistory
  {
  }

  [PXHidden]
  [Serializable]
  public class ARRegister : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected Guid? _NoteID;
    protected Decimal? _CuryDiscActTaken;
    protected Decimal? _DiscActTaken;

    /// <summary>
    /// The type of the document.
    /// This field is a part of the compound key of the document.
    /// </summary>
    /// <value>
    /// The field can have one of the values described in <see cref="T:PX.Objects.AR.ARDocType.ListAttribute" />.
    /// </value>
    [PXDBString(3, IsKey = true, IsFixed = true, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault]
    [ARDocType.List]
    [PXUIField]
    public virtual string DocType { get; set; }

    /// <summary>
    /// The reference number of the document.
    /// This field is a part of the compound key of the document.
    /// </summary>
    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault]
    [PXUIField]
    [PXSelector(typeof (Search<ARDocumentEnq.ARRegister.refNbr, Where<ARDocumentEnq.ARRegister.docType, Equal<Optional<ARDocumentEnq.ARRegister.docType>>>>), Filterable = true)]
    public virtual string RefNbr { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.GL.Branch">branch</see> to which the document belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
    /// </value>
    [Branch(null, null, true, true, true, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    public virtual int? BranchID { get; set; }

    /// <summary>The date of the document.</summary>
    /// <value>
    /// Defaults to the current <see cref="P:PX.Data.AccessInfo.BusinessDate">Business Date</see>.
    /// </value>
    [PXDBDate(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? DocDate { get; set; }

    /// <summary>The description of the document.</summary>
    [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField]
    public virtual string DocDesc { get; set; }

    [PXDBDate(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField]
    public virtual DateTime? DueDate { get; set; }

    /// <summary>
    /// The code of the <see cref="T:PX.Objects.CM.Currency" /> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// Corresponds to the <see cref="!:Currency.CuryID" /> field.
    /// </value>
    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField]
    [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string CuryID { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the document.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="!:CurrencyInfoID" /> field.
    /// </value>
    [PXDBLong(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [CurrencyInfo]
    public virtual long? CuryInfoID { get; set; }

    /// <summary>The type of the original (source) document.</summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.DocType" /> field.
    /// </value>
    [PXDBString(3, IsFixed = true, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [ARDocType.List]
    [PXUIField(DisplayName = "Orig. Doc. Type")]
    public virtual string OrigDocType { get; set; }

    /// <summary>
    /// The reference number of the original (source) document.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.RefNbr" /> field.
    /// </value>
    [PXDBString(15, IsUnicode = true, InputMask = "", BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField]
    public virtual string OrigRefNbr { get; set; }

    /// <summary>
    /// The status of the document.
    /// The value of the field is determined by the values of the status flags,
    /// such as <see cref="!:Hold" />, <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.Released" />, <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.Voided" />, <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.Scheduled" />.
    /// </summary>
    /// <value>
    /// The field can have one of the values described in <see cref="T:PX.Objects.AR.ARDocStatus.ListAttribute" />.
    /// Defaults to <see cref="F:PX.Objects.AR.ARDocStatus.Hold" />.
    /// </value>
    [PXDBString(1, IsFixed = true, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault("H")]
    [PXUIField]
    [ARDocStatus.List]
    public virtual string Status { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Determined by the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.DocDate">date of the document</see>. Unlike <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.FinPeriodID" />
    /// the value of this field can't be overriden by user.
    /// </value>
    [PeriodID(null, null, null, true, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    public virtual string TranPeriodID { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the period, to which the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.DocDate" /> belongs, but can be overriden by user.
    /// </value>
    [AROpenPeriod(typeof (ARDocumentEnq.ARRegister.docDate), typeof (ARDocumentEnq.ARRegister.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (ARDocumentEnq.ARRegister.tranPeriodID), IsHeader = true, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField]
    public virtual string FinPeriodID { get; set; }

    /// <summary>
    /// The <see cref="!:FinancialPeriod">Financial Period</see>, in which the document was closed.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.FinPeriodID" /> field.
    /// </value>
    [FinPeriodID(null, typeof (ARDocumentEnq.ARRegister.branchID), null, null, null, null, true, false, null, typeof (ARDocumentEnq.ARRegister.closedTranPeriodID), null, true, true, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField]
    public virtual string ClosedFinPeriodID { get; set; }

    /// <summary>
    /// The <see cref="!:FinancialPeriod">Financial Period</see>, in which the document was closed.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.TranPeriodID" /> field.
    /// </value>
    [PeriodID(null, null, null, true, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField]
    public virtual string ClosedTranPeriodID { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
    /// </value>
    [PXNote(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    public virtual Guid? NoteID
    {
      get => this._NoteID;
      set => this._NoteID = value;
    }

    /// <summary>
    /// When set to <c>true</c>, indicates that the document is open.
    /// </summary>
    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(true)]
    public virtual bool? OpenDoc { get; set; }

    /// <summary>
    /// When set to <c>true</c>, indicates that the document has been released.
    /// </summary>
    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(false)]
    public virtual bool? Released { get; set; }

    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField(DisplayName = "Retainage Document", Enabled = false, FieldClass = "Retainage")]
    [PXDefault(false)]
    public virtual bool? IsRetainageDocument { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.GL.Account">AR account</see> to which the document should be posted.
    /// The Cash account and Year-to-Date Net Income account cannot be selected as the value of this field.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
    /// </value>
    [PXDefault]
    [Account(typeof (ARDocumentEnq.ARRegister.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<PX.Objects.GL.Account.isCashAccount, Equal<False>, And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>>), DisplayName = "AR Account", BqlTable = typeof (ARDocumentEnq.ARRegister))]
    public virtual int? ARAccountID { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.GL.Sub">subaccount</see> to which the document should be posted.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
    /// </value>
    [PXDefault]
    [SubAccount(typeof (ARDocumentEnq.ARRegister.aRAccountID))]
    public virtual int? ARSubID { get; set; }

    [PXDefault]
    [Account(typeof (ARDocumentEnq.ARRegister.branchID), DisplayName = "Prepayment Account", BqlTable = typeof (ARDocumentEnq.ARRegister))]
    public virtual int? PrepaymentAccountID { get; set; }

    [PXDefault]
    [SubAccount(typeof (ARDocumentEnq.ARRegister.prepaymentAccountID))]
    public virtual int? PrepaymentSubID { get; set; }

    /// <summary>
    /// Specifies (if set to <c>true</c>) that the record has been created
    /// in migration mode without affecting GL module.
    /// </summary>
    [MigratedRecord(typeof (PX.Objects.AR.ARSetup.migrationMode))]
    public virtual bool? IsMigratedRecord { get; set; }

    /// <summary>
    /// The number of the <see cref="T:PX.Objects.GL.Batch" /> created from the document on release.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Batch.BatchNbr" /> field.
    /// </value>
    [PXDBString(15, IsUnicode = true, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField]
    [BatchNbr(typeof (Search<PX.Objects.GL.Batch.batchNbr, Where<PX.Objects.GL.Batch.module, Equal<BatchModule.moduleAR>>>), IsMigratedRecordField = typeof (ARDocumentEnq.ARRegister.isMigratedRecord))]
    public virtual string BatchNbr { get; set; }

    /// <summary>
    /// The cash discount entered for the document.
    /// Given in the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.CuryID">currency of the document</see>.
    /// </summary>
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (ARDocumentEnq.ARRegister.curyInfoID), typeof (ARDocumentEnq.ARRegister.origDiscAmt), BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField]
    public virtual Decimal? CuryOrigDiscAmt { get; set; }

    /// <summary>
    /// The cash discount entered for the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXDBBaseCury(null, null, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? OrigDiscAmt { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document is part of a <c>Schedule</c> and serves as a template for generating other documents according to it.
    /// </summary>
    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(false)]
    public virtual bool? Scheduled { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document has been voided.
    /// </summary>
    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(false)]
    public virtual bool? Voided { get; set; }

    /// <summary>
    /// The amount of the document.
    /// Given in the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.CuryID">currency of the document</see>.
    /// </summary>
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (ARDocumentEnq.ARRegister.curyInfoID), typeof (ARDocumentEnq.ARRegister.origDocAmt), BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField]
    public virtual Decimal? CuryOrigDocAmt { get; set; }

    /// <summary>
    /// The amount of the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXDBBaseCury(null, null, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField]
    public virtual Decimal? OrigDocAmt { get; set; }

    /// <summary>
    /// The <see cref="P:PX.Objects.AR.ARAdjust.CuryAdjdDiscAmt">cash discount amount</see> actually applied to the document.
    /// Given in the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.CuryID">currency of the document</see>.
    /// </summary>
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (ARDocumentEnq.ARRegister.curyInfoID), typeof (ARDocumentEnq.ARRegister.discTaken), BqlTable = typeof (ARDocumentEnq.ARRegister))]
    public virtual Decimal? CuryDiscTaken { get; set; }

    /// <summary>
    /// The <see cref="P:PX.Objects.AR.ARAdjust.CuryAdjdDiscAmt">cash discount amount</see> actually applied to the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXDBBaseCury(null, null, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? DiscTaken { get; set; }

    /// <summary>
    /// The cash discount balance of the document.
    /// Given in the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.CuryID">currency of the document</see>.
    /// </summary>
    [PXUIField]
    [PXDBCurrency(typeof (ARDocumentEnq.ARRegister.curyInfoID), typeof (ARDocumentEnq.ARRegister.discBal), BaseCalc = false, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? CuryDiscBal { get; set; }

    /// <summary>
    /// The cash discount balance of the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXDBBaseCury(null, null, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? DiscBal { get; set; }

    [PXDBCurrency(typeof (ARDocumentEnq.ARRegister.curyInfoID), typeof (ARDocumentEnq.ARRegister.retainageTotal), BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? CuryRetainageTotal { get; set; }

    [PXDBBaseCury(null, null, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
    public virtual Decimal? RetainageTotal { get; set; }

    [PXDBCurrency(typeof (ARDocumentEnq.ARRegister.curyInfoID), typeof (ARDocumentEnq.ARRegister.retainageUnreleasedAmt), BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? CuryRetainageUnreleasedAmt { get; set; }

    [PXDBBaseCury(null, null, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
    public virtual Decimal? RetainageUnreleasedAmt { get; set; }

    [PXDBCurrency(typeof (ARDocumentEnq.ARRegister.curyInfoID), typeof (ARDocumentEnq.ARRegister.retainedTaxTotal), BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField(DisplayName = "Tax on Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? CuryRetainedTaxTotal { get; set; }

    [PXDBBaseCury(null, null, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? RetainedTaxTotal { get; set; }

    [PXDBCurrency(typeof (ARDocumentEnq.ARRegister.curyInfoID), typeof (ARDocumentEnq.ARRegister.retainedDiscTotal), BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField(DisplayName = "Discount on Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? CuryRetainedDiscTotal { get; set; }

    [PXDBBaseCury(null, null, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? RetainedDiscTotal { get; set; }

    [PXCury(typeof (ARDocumentEnq.ARRegister.curyID))]
    [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXFormula(typeof (Add<ARDocumentEnq.ARRegister.curyOrigDocAmt, ARDocumentEnq.ARRegister.curyRetainageTotal>))]
    public virtual Decimal? CuryOrigDocAmtWithRetainageTotal { get; set; }

    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
    [PXFormula(typeof (Add<ARDocumentEnq.ARRegister.origDocAmt, ARDocumentEnq.ARRegister.retainageTotal>))]
    public virtual Decimal? OrigDocAmtWithRetainageTotal { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (ARDocumentEnq.ARRegister.curyInfoID), typeof (ARDocumentEnq.ARRegister.curyDocBal), BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField(DisplayName = "Currency Balance")]
    public virtual Decimal? CuryDocBal { get; set; }

    [PXDBBaseCury(null, null, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Balance")]
    public virtual Decimal? DocBal { get; set; }

    [PXDBBaseCury(null, null, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "RGOL Amount")]
    public virtual Decimal? RGOLAmt { get; set; }

    [PXString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
    [PXUIField(DisplayName = "Payment Method")]
    public virtual string PaymentMethodID { get; set; }

    [Customer(Enabled = false, Visible = false, DescriptionField = typeof (Customer.acctName), BqlTable = typeof (ARDocumentEnq.ARRegister))]
    public virtual int? CustomerID { get; set; }

    [PXCury(typeof (ARDocumentEnq.ARRegister.curyID))]
    [PXUIField(DisplayName = "Currency Cash Discount Taken")]
    public virtual Decimal? CuryDiscActTaken { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "Cash Discount Taken")]
    public virtual Decimal? DiscActTaken { get; set; }

    [PXDecimal]
    [PXDependsOnFields(new System.Type[] {typeof (ARDocumentEnq.ARRegister.docType)})]
    [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARDocumentEnq.ARRegister.docType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO, ARDocType.cashSale, ARDocType.prepaymentInvoice>>, decimal1>, decimal_1>), typeof (Decimal))]
    public virtual Decimal? SignBalance { get; set; }

    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    public virtual bool? PendingPayment { get; set; }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.docType>
    {
      public const int Length = 3;
    }

    public abstract class refNbr : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARDocumentEnq.ARRegister.refNbr>
    {
    }

    public abstract class branchID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARDocumentEnq.ARRegister.branchID>
    {
    }

    public abstract class docDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.docDate>
    {
    }

    public abstract class docDesc : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.docDesc>
    {
    }

    public abstract class dueDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.dueDate>
    {
    }

    public abstract class curyID : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARDocumentEnq.ARRegister.curyID>
    {
    }

    public abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.curyInfoID>
    {
    }

    public abstract class origDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.origDocType>
    {
    }

    public abstract class origRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.origRefNbr>
    {
    }

    public abstract class status : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARDocumentEnq.ARRegister.status>
    {
    }

    public abstract class tranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.tranPeriodID>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.finPeriodID>
    {
    }

    public abstract class closedFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.closedFinPeriodID>
    {
    }

    public abstract class closedTranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.closedTranPeriodID>
    {
    }

    public abstract class noteID : BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARDocumentEnq.ARRegister.noteID>
    {
      [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2022 R1.")]
      public class NoteAttribute : PXNoteAttribute
      {
        public NoteAttribute()
        {
          ((PXEventSubscriberAttribute) this).BqlTable = typeof (ARDocumentEnq.ARRegister);
        }

        protected virtual bool IsVirtualTable(System.Type table) => false;
      }
    }

    public abstract class openDoc : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARDocumentEnq.ARRegister.openDoc>
    {
    }

    public abstract class released : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARDocumentEnq.ARRegister.released>
    {
    }

    public abstract class isRetainageDocument : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.isRetainageDocument>
    {
    }

    public abstract class aRAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.aRAccountID>
    {
    }

    public abstract class aRSubID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARDocumentEnq.ARRegister.aRSubID>
    {
    }

    public abstract class prepaymentAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.prepaymentAccountID>
    {
    }

    public abstract class prepaymentSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.prepaymentSubID>
    {
    }

    public abstract class isMigratedRecord : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.isMigratedRecord>
    {
    }

    public abstract class batchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.batchNbr>
    {
    }

    public abstract class curyOrigDiscAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.curyOrigDiscAmt>
    {
    }

    public abstract class origDiscAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.origDiscAmt>
    {
    }

    public abstract class scheduled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.scheduled>
    {
    }

    public abstract class voided : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARDocumentEnq.ARRegister.voided>
    {
    }

    public abstract class curyOrigDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.curyOrigDocAmt>
    {
    }

    public abstract class origDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.origDocAmt>
    {
    }

    public abstract class curyDiscTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.curyDiscTaken>
    {
    }

    public abstract class discTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.discTaken>
    {
    }

    public abstract class curyDiscBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.curyDiscBal>
    {
    }

    public abstract class discBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.discBal>
    {
    }

    public abstract class curyRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.curyRetainageTotal>
    {
    }

    public abstract class retainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.retainageTotal>
    {
    }

    public abstract class curyRetainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.curyRetainageUnreleasedAmt>
    {
    }

    public abstract class retainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.retainageUnreleasedAmt>
    {
    }

    public abstract class curyRetainedTaxTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.curyRetainedTaxTotal>
    {
    }

    public abstract class retainedTaxTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.retainedTaxTotal>
    {
    }

    public abstract class curyRetainedDiscTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.curyRetainedDiscTotal>
    {
    }

    public abstract class retainedDiscTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.retainedDiscTotal>
    {
    }

    public abstract class curyOrigDocAmtWithRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.curyOrigDocAmtWithRetainageTotal>
    {
    }

    public abstract class origDocAmtWithRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.origDocAmtWithRetainageTotal>
    {
    }

    public abstract class curyDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.curyDocBal>
    {
    }

    public abstract class docBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.docBal>
    {
    }

    public abstract class rGOLAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.rGOLAmt>
    {
    }

    public abstract class paymentMethodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.paymentMethodID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.customerID>
    {
    }

    public abstract class curyDiscActTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.curyDiscActTaken>
    {
    }

    public abstract class discActTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.discActTaken>
    {
    }

    public abstract class signBalance : IBqlField, IBqlOperand
    {
    }

    public abstract class pendingPayment : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARRegister.pendingPayment>
    {
    }
  }

  [PXProjection(typeof (Select2<ARDocumentEnq.ARRegister, LeftJoin<ARDocumentEnq.Ref.ARInvoice, On<ARDocumentEnq.Ref.ARInvoice.docType, Equal<ARDocumentEnq.ARRegister.docType>, And<ARDocumentEnq.Ref.ARInvoice.refNbr, Equal<ARDocumentEnq.ARRegister.refNbr>>>, LeftJoin<ARDocumentEnq.Ref.ARPayment, On<ARDocumentEnq.Ref.ARPayment.docType, Equal<ARDocumentEnq.ARRegister.docType>, And<ARDocumentEnq.Ref.ARPayment.refNbr, Equal<ARDocumentEnq.ARRegister.refNbr>>>>>>))]
  [PXPrimaryGraph(new System.Type[] {typeof (SOInvoiceEntry), typeof (ARCashSaleEntry), typeof (ARInvoiceEntry), typeof (ARPaymentEntry)}, new System.Type[] {typeof (Select<ARInvoice, Where<ARInvoice.docType, Equal<Current<ARDocumentEnq.ARDocumentResult.docType>>, And<ARInvoice.refNbr, Equal<Current<ARDocumentEnq.ARDocumentResult.refNbr>>, And<ARInvoice.origModule, Equal<BatchModule.moduleSO>, And<ARInvoice.released, Equal<False>>>>>>), typeof (Select<ARCashSale, Where<ARCashSale.docType, Equal<Current<ARDocumentEnq.ARDocumentResult.docType>>, And<ARCashSale.refNbr, Equal<Current<ARDocumentEnq.ARDocumentResult.refNbr>>>>>), typeof (Select<ARInvoice, Where<ARInvoice.docType, Equal<Current<ARDocumentEnq.ARDocumentResult.docType>>, And<ARInvoice.refNbr, Equal<Current<ARDocumentEnq.ARDocumentResult.refNbr>>>>>), typeof (Select<ARPayment, Where<ARPayment.docType, Equal<Current<ARDocumentEnq.ARDocumentResult.docType>>, And<ARPayment.refNbr, Equal<Current<ARDocumentEnq.ARDocumentResult.refNbr>>>>>)})]
  [PXCacheName("Customer Details")]
  [Serializable]
  public class ARDocumentResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected Guid? _NoteID;
    protected Decimal? _CuryBegBalance;
    protected Decimal? _CuryDiscActTaken;
    protected Decimal? _DiscActTaken;

    /// <summary>
    /// The type of the document.
    /// This field is a part of the compound key of the document.
    /// </summary>
    /// <value>
    /// The field can have one of the values described in <see cref="T:PX.Objects.AR.ARDocType.ListAttribute" />.
    /// </value>
    [PXDBString(3, IsKey = true, IsFixed = true, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault]
    [ARDocType.List]
    [PXUIField]
    public virtual string DocType { get; set; }

    /// <summary>
    /// The reference number of the document.
    /// This field is a part of the compound key of the document.
    /// </summary>
    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault]
    [PXUIField]
    [PXSelector(typeof (Search<ARDocumentEnq.ARRegister.refNbr, Where<ARDocumentEnq.ARRegister.docType, Equal<Optional<ARDocumentEnq.ARRegister.docType>>>>), Filterable = true)]
    public virtual string RefNbr { get; set; }

    /// <summary>
    /// The counter of <see cref="!:TermsInstallment">installments</see> associated with the document.
    /// </summary>
    [PXDBShort(BqlTable = typeof (ARDocumentEnq.Ref.ARInvoice))]
    public virtual short? InstallmentCntr { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.GL.Branch">branch</see> to which the document belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
    /// </value>
    [Branch(null, null, true, true, true, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    public virtual int? BranchID { get; set; }

    [Customer(Enabled = false, Visible = false, DescriptionField = typeof (Customer.acctName), BqlTable = typeof (ARDocumentEnq.ARRegister))]
    public virtual int? CustomerID { get; set; }

    /// <summary>The date of the document.</summary>
    /// <value>
    /// Defaults to the current <see cref="P:PX.Data.AccessInfo.BusinessDate">Business Date</see>.
    /// </value>
    [PXDBDate(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? DocDate { get; set; }

    /// <summary>The description of the document.</summary>
    [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField]
    public virtual string DocDesc { get; set; }

    [PXDBDate(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField]
    public virtual DateTime? DueDate { get; set; }

    /// <summary>
    /// The code of the <see cref="T:PX.Objects.CM.Currency" /> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// Corresponds to the <see cref="!:Currency.CuryID" /> field.
    /// </value>
    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField]
    [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string CuryID { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the document.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="!:CurrencyInfoID" /> field.
    /// </value>
    [PXDBLong(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [CurrencyInfo]
    public virtual long? CuryInfoID { get; set; }

    /// <summary>The type of the original (source) document.</summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARDocumentResult.DocType" /> field.
    /// </value>
    [PXDBString(3, IsFixed = true, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [ARDocType.List]
    [PXUIField(DisplayName = "Orig. Doc. Type")]
    public virtual string OrigDocType { get; set; }

    /// <summary>
    /// The reference number of the original (source) document.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARDocumentResult.RefNbr" /> field.
    /// </value>
    [PXDBString(15, IsUnicode = true, InputMask = "", BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField]
    public virtual string OrigRefNbr { get; set; }

    /// <summary>
    /// The status of the document.
    /// The value of the field is determined by the values of the status flags,
    /// such as <see cref="!:Hold" />, <see cref="P:PX.Objects.AR.ARDocumentEnq.ARDocumentResult.Released" />, <see cref="P:PX.Objects.AR.ARDocumentEnq.ARDocumentResult.Voided" />, <see cref="P:PX.Objects.AR.ARDocumentEnq.ARDocumentResult.Scheduled" />.
    /// </summary>
    /// <value>
    /// The field can have one of the values described in <see cref="T:PX.Objects.AR.ARDocStatus.ListAttribute" />.
    /// Defaults to <see cref="F:PX.Objects.AR.ARDocStatus.Hold" />.
    /// </value>
    [PXDBString(1, IsFixed = true, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault("H")]
    [PXUIField]
    [ARDocStatus.List]
    public virtual string Status { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Determined by the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.DocDate">date of the document</see>. Unlike <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.FinPeriodID" />
    /// the value of this field can't be overriden by user.
    /// </value>
    [PeriodID(null, null, null, true, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    public virtual string TranPeriodID { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the period, to which the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.DocDate" /> belongs, but can be overriden by user.
    /// </value>
    [AROpenPeriod(typeof (ARDocumentEnq.ARDocumentResult.docDate), typeof (ARDocumentEnq.ARDocumentResult.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (ARDocumentEnq.ARDocumentResult.tranPeriodID), IsHeader = true, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField]
    public virtual string FinPeriodID { get; set; }

    /// <summary>
    /// The <see cref="!:FinancialPeriod">Financial Period</see>, in which the document was closed.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARDocumentResult.FinPeriodID" /> field.
    /// </value>
    [FinPeriodID(null, typeof (ARDocumentEnq.ARDocumentResult.branchID), null, null, null, null, true, false, null, typeof (ARDocumentEnq.ARDocumentResult.closedTranPeriodID), null, true, true, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField]
    public virtual string ClosedFinPeriodID { get; set; }

    /// <summary>
    /// The <see cref="!:FinancialPeriod">Financial Period</see>, in which the document was closed.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARDocumentResult.TranPeriodID" /> field.
    /// </value>
    [PeriodID(null, null, null, true, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField]
    public virtual string ClosedTranPeriodID { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
    /// </value>
    [ARDocumentEnq.ARDocumentResult.noteID.Note]
    public virtual Guid? NoteID
    {
      get => this._NoteID;
      set => this._NoteID = value;
    }

    /// <summary>
    /// When set to <c>true</c>, indicates that the document is open.
    /// </summary>
    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(true)]
    public virtual bool? OpenDoc { get; set; }

    /// <summary>
    /// When set to <c>true</c>, indicates that the document has been released.
    /// </summary>
    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(false)]
    public virtual bool? Released { get; set; }

    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField(DisplayName = "Retainage Document", Enabled = false, FieldClass = "Retainage")]
    [PXDefault(false)]
    public virtual bool? IsRetainageDocument { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.GL.Account">AR account</see> to which the document should be posted.
    /// The Cash account and Year-to-Date Net Income account cannot be selected as the value of this field.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
    /// </value>
    [PXDefault]
    [Account(typeof (ARDocumentEnq.ARDocumentResult.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<PX.Objects.GL.Account.isCashAccount, Equal<False>, And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>>), DisplayName = "AR Account", BqlTable = typeof (ARDocumentEnq.ARRegister))]
    public virtual int? ARAccountID { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.GL.Sub">subaccount</see> to which the document should be posted.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
    /// </value>
    [PXDefault]
    [SubAccount(typeof (ARDocumentEnq.ARDocumentResult.aRAccountID))]
    public virtual int? ARSubID { get; set; }

    /// <summary>
    /// Specifies (if set to <c>true</c>) that the record has been created
    /// in migration mode without affecting GL module.
    /// </summary>
    [MigratedRecord(typeof (PX.Objects.AR.ARSetup.migrationMode), BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual bool? IsMigratedRecord { get; set; }

    /// <summary>
    /// The number of the <see cref="T:PX.Objects.GL.Batch" /> created from the document on release.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Batch.BatchNbr" /> field.
    /// </value>
    [PXDBString(15, IsUnicode = true, BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField]
    [BatchNbr(typeof (Search<PX.Objects.GL.Batch.batchNbr, Where<PX.Objects.GL.Batch.module, Equal<BatchModule.moduleAR>>>), IsMigratedRecordField = typeof (ARDocumentEnq.ARRegister.isMigratedRecord))]
    public virtual string BatchNbr { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document is part of a <c>Schedule</c> and serves as a template for generating other documents according to it.
    /// </summary>
    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(false)]
    public virtual bool? Scheduled { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document has been voided.
    /// </summary>
    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(false)]
    public virtual bool? Voided { get; set; }

    [PXString(30, IsUnicode = true)]
    [PXUIField(DisplayName = "Customer Order Nbr./Payment Nbr.")]
    [PXDBCalced(typeof (IsNull<ARDocumentEnq.Ref.ARInvoice.invoiceNbr, ARDocumentEnq.Ref.ARPayment.extRefNbr>), typeof (string))]
    public virtual string ExtRefNbr { get; set; }

    [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa", BqlField = typeof (ARDocumentEnq.Ref.ARPayment.paymentMethodID))]
    [PXUIField(DisplayName = "Payment Method")]
    public virtual string PaymentMethodID { get; set; }

    [PXDecimal(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    public virtual Decimal? SignBalance { get; set; }

    /// <summary>
    /// The amount of the document.
    /// Given in the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARDocumentResult.CuryID">currency of the document</see>.
    /// </summary>
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (ARDocumentEnq.ARRegister.curyInfoID), typeof (ARDocumentEnq.ARRegister.origDocAmt))]
    [PXUIField]
    [PXDBCalced(typeof (Mult<ARDocumentEnq.ARRegister.signBalance, ARDocumentEnq.ARRegister.curyOrigDocAmt>), typeof (Decimal))]
    public virtual Decimal? CuryOrigDocAmt { get; set; }

    /// <summary>
    /// The amount of the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField]
    [PXDBCalced(typeof (Mult<ARDocumentEnq.ARRegister.signBalance, ARDocumentEnq.ARRegister.origDocAmt>), typeof (Decimal))]
    public virtual Decimal? OrigDocAmt { get; set; }

    /// <summary>
    /// The cash discount entered for the document.
    /// Given in the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARDocumentResult.CuryID">currency of the document</see>.
    /// </summary>
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (ARDocumentEnq.ARDocumentResult.curyInfoID), typeof (ARDocumentEnq.ARDocumentResult.origDiscAmt))]
    [PXUIField]
    [PXDBCalced(typeof (Mult<ARDocumentEnq.ARRegister.signBalance, ARDocumentEnq.ARRegister.curyOrigDiscAmt>), typeof (Decimal))]
    public virtual Decimal? CuryOrigDiscAmt { get; set; }

    /// <summary>
    /// The cash discount entered for the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCalced(typeof (Mult<ARDocumentEnq.ARRegister.signBalance, ARDocumentEnq.ARRegister.origDiscAmt>), typeof (Decimal))]
    public virtual Decimal? OrigDiscAmt { get; set; }

    [PXCurrency(typeof (ARDocumentEnq.ARDocumentResult.curyInfoID), typeof (ARDocumentEnq.ARDocumentResult.retainageTotal))]
    [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCalced(typeof (Mult<ARDocumentEnq.ARRegister.signBalance, ARDocumentEnq.ARRegister.curyRetainageTotal>), typeof (Decimal))]
    public virtual Decimal? CuryRetainageTotal { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
    [PXDBCalced(typeof (Mult<ARDocumentEnq.ARRegister.signBalance, ARDocumentEnq.ARRegister.retainageTotal>), typeof (Decimal))]
    public virtual Decimal? RetainageTotal { get; set; }

    [PXCury(typeof (ARDocumentEnq.ARDocumentResult.curyID))]
    [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCalced(typeof (Mult<ARDocumentEnq.ARRegister.signBalance, Add<ARDocumentEnq.ARRegister.curyOrigDocAmt, ARDocumentEnq.ARRegister.curyRetainageTotal>>), typeof (Decimal))]
    public virtual Decimal? CuryOrigDocAmtWithRetainageTotal { get; set; }

    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
    [PXDBCalced(typeof (Mult<ARDocumentEnq.ARRegister.signBalance, Add<ARDocumentEnq.ARRegister.origDocAmt, ARDocumentEnq.ARRegister.retainageTotal>>), typeof (Decimal))]
    public virtual Decimal? OrigDocAmtWithRetainageTotal { get; set; }

    [PXCury(typeof (ARDocumentEnq.ARDocumentResult.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Currency Period Beg. Balance")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? CuryBegBalance { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Period Beg. Balance")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? BegBalance { get; set; }

    [PXCury(typeof (ARDocumentEnq.ARRegister.curyID))]
    [PXUIField(DisplayName = "Currency Cash Discount Taken")]
    [PXDBCalced(typeof (Mult<ARDocumentEnq.ARRegister.signBalance, ARDocumentEnq.ARRegister.curyDiscTaken>), typeof (Decimal))]
    public virtual Decimal? CuryDiscActTaken { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "Cash Discount Taken")]
    [PXDBCalced(typeof (Mult<ARDocumentEnq.ARRegister.signBalance, ARDocumentEnq.ARRegister.discTaken>), typeof (Decimal))]
    public virtual Decimal? DiscActTaken { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCury(typeof (ARDocumentEnq.ARRegister.curyID))]
    [PXUIField(DisplayName = "Currency Write-Off Amount")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? CuryWOAmt { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Write-Off Amount")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? WOAmt { get; set; }

    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "RGOL Amount")]
    [PXDBCalced(typeof (Mult<decimal_1, ARDocumentEnq.ARRegister.rGOLAmt>), typeof (Decimal))]
    public virtual Decimal? RGOLAmt { get; set; }

    /// <summary>
    /// Expected GL turnover for the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField(DisplayName = "AR Turnover")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? ARTurnover { get; set; }

    /// <summary>
    /// Expected GL turnover for the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXBaseCury]
    [PXUIField(DisplayName = "GL Turnover")]
    public virtual Decimal? GLTurnover { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (ARDocumentEnq.ARDocumentResult.curyInfoID), typeof (ARDocumentEnq.ARDocumentResult.curyDocBal))]
    [PXUIField(DisplayName = "Currency Balance")]
    [PXDBCalced(typeof (Switch<Case<Where<ARDocumentEnq.ARRegister.voided, NotEqual<True>, And<BqlOperand<ARDocumentEnq.ARRegister.docType, IBqlString>.IsNotIn<ARDocType.cashSale, ARDocType.cashReturn>>>, Mult<ARDocumentEnq.ARRegister.signBalance, ARDocumentEnq.ARRegister.curyDocBal>>, decimal0>), typeof (Decimal))]
    public virtual Decimal? CuryDocBal { get; set; }

    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Balance")]
    [PXDBCalced(typeof (Switch<Case<Where<ARDocumentEnq.ARRegister.voided, NotEqual<True>, And<BqlOperand<ARDocumentEnq.ARRegister.docType, IBqlString>.IsNotIn<ARDocType.cashSale, ARDocType.cashReturn>>>, Mult<ARDocumentEnq.ARRegister.signBalance, ARDocumentEnq.ARRegister.docBal>>, decimal0>), typeof (Decimal))]
    public virtual Decimal? DocBal { get; set; }

    [PXCurrency(typeof (ARDocumentEnq.ARDocumentResult.curyInfoID), typeof (ARDocumentEnq.ARDocumentResult.retainageUnreleasedAmt), BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCalced(typeof (Mult<ARDocumentEnq.ARRegister.signBalance, ARDocumentEnq.ARRegister.curyRetainageUnreleasedAmt>), typeof (Decimal))]
    public virtual Decimal? CuryRetainageUnreleasedAmt { get; set; }

    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
    [PXDBCalced(typeof (Mult<ARDocumentEnq.ARRegister.signBalance, ARDocumentEnq.ARRegister.retainageUnreleasedAmt>), typeof (Decimal))]
    public virtual Decimal? RetainageUnreleasedAmt { get; set; }

    [PXString]
    [PXDBCalced(typeof (ARDocumentEnq.ARRegister.tranPeriodID), typeof (string))]
    public virtual string TranPostPeriodID { get; set; }

    [PXString]
    [PXDBCalced(typeof (ARDocumentEnq.ARRegister.finPeriodID), typeof (string))]
    public virtual string FinPostPeriodID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.ARAccountID" />
    [Account(typeof (ARDocumentEnq.ARDocumentResult.branchID), DisplayName = "Account", BqlField = typeof (ARDocumentEnq.ARRegister.aRAccountID))]
    public virtual int? AccountID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.ARSubID" />
    /// .
    [SubAccount(typeof (ARDocumentEnq.ARDocumentResult.accountID))]
    public virtual int? SubID { get; set; }

    /// <inheritdoc cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.PendingPayment" />
    /// .
    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    public virtual bool? PendingPayment { get; set; }

    /// <summary>Project ID</summary>
    [PXDBCalced(typeof (IsNull<ARDocumentEnq.Ref.ARInvoice.projectID, ARDocumentEnq.Ref.ARPayment.projectID>), typeof (int?))]
    [PXSelector(typeof (Search<PMProject.contractID>), SubstituteKey = typeof (PMProject.contractCD))]
    [PXUIField(DisplayName = "Project")]
    [PXInt]
    public virtual int? ProjectID { get; set; }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.docType>
    {
      public const int Length = 3;
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.refNbr>
    {
    }

    public abstract class installmentCntr : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.installmentCntr>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.branchID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.customerID>
    {
    }

    public abstract class docDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.docDate>
    {
    }

    public abstract class docDesc : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.docDesc>
    {
    }

    public abstract class dueDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.dueDate>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.curyID>
    {
    }

    public abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.curyInfoID>
    {
    }

    public abstract class origDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.origDocType>
    {
    }

    public abstract class origRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.origRefNbr>
    {
    }

    public abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.status>
    {
    }

    public abstract class tranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.tranPeriodID>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.finPeriodID>
    {
    }

    public abstract class closedFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.closedFinPeriodID>
    {
    }

    public abstract class closedTranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.closedTranPeriodID>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.noteID>
    {
      public class NoteAttribute : PXNoteAttribute
      {
        public NoteAttribute()
        {
          ((PXEventSubscriberAttribute) this).BqlTable = typeof (ARDocumentEnq.ARRegister);
        }

        protected virtual bool IsVirtualTable(System.Type table) => false;
      }
    }

    public abstract class openDoc : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.openDoc>
    {
    }

    public abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.released>
    {
    }

    public abstract class isRetainageDocument : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.isRetainageDocument>
    {
    }

    public abstract class aRAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.aRAccountID>
    {
    }

    public abstract class aRSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.aRSubID>
    {
    }

    public abstract class isMigratedRecord : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.isMigratedRecord>
    {
    }

    public abstract class batchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.batchNbr>
    {
    }

    public abstract class scheduled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.scheduled>
    {
    }

    public abstract class voided : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.voided>
    {
    }

    public abstract class extRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.extRefNbr>
    {
    }

    public abstract class paymentMethodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.paymentMethodID>
    {
    }

    public abstract class signBalance : IBqlField, IBqlOperand
    {
    }

    public abstract class curyOrigDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.curyOrigDocAmt>
    {
    }

    public abstract class origDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.origDocAmt>
    {
    }

    public abstract class curyOrigDiscAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.curyOrigDiscAmt>
    {
    }

    public abstract class origDiscAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.origDiscAmt>
    {
    }

    public abstract class curyRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.curyRetainageTotal>
    {
    }

    public abstract class retainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.retainageTotal>
    {
    }

    public abstract class curyOrigDocAmtWithRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.curyOrigDocAmtWithRetainageTotal>
    {
    }

    public abstract class origDocAmtWithRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.origDocAmtWithRetainageTotal>
    {
    }

    public abstract class curyBegBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.curyBegBalance>
    {
    }

    public abstract class begBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.begBalance>
    {
    }

    public abstract class curyDiscActTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.curyDiscActTaken>
    {
    }

    public abstract class discActTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.discActTaken>
    {
    }

    public abstract class curyWOAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.curyWOAmt>
    {
    }

    public abstract class woAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.woAmt>
    {
    }

    public abstract class rGOLAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.rGOLAmt>
    {
    }

    public abstract class aRTurnover : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.aRTurnover>
    {
    }

    public abstract class gLTurnover : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.gLTurnover>
    {
    }

    public abstract class curyDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.curyDocBal>
    {
    }

    public abstract class docBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.docBal>
    {
    }

    public abstract class curyRetainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.curyRetainageUnreleasedAmt>
    {
    }

    public abstract class retainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.retainageUnreleasedAmt>
    {
    }

    public abstract class tranPostPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.tranPostPeriodID>
    {
    }

    public abstract class finPostPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.finPostPeriodID>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.accountID>
    {
    }

    public abstract class subID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARDocumentEnq.ARDocumentResult.subID>
    {
    }

    public abstract class pendingPayment : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.pendingPayment>
    {
    }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentResult.projectID>
    {
    }
  }

  [PXProjection(typeof (Select2<ARDocumentEnq.ARDocumentResult, LeftJoin<ARTranPostGL, On<ARTranPostGL.docType, Equal<ARDocumentEnq.ARDocumentResult.docType>, And<ARTranPostGL.refNbr, Equal<ARDocumentEnq.ARDocumentResult.refNbr>>>>>))]
  [PXHidden]
  [Serializable]
  public class ARDocumentPeriodResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected Guid? _NoteID;
    protected Decimal? _CuryBegBalance;

    /// <summary>
    /// The type of the document.
    /// This field is a part of the compound key of the document.
    /// </summary>
    /// <value>
    /// The field can have one of the values described in <see cref="T:PX.Objects.AR.ARDocType.ListAttribute" />.
    /// </value>
    [PXDBString(3, IsKey = true, IsFixed = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault]
    [ARDocType.List]
    [PXUIField]
    public virtual string DocType { get; set; }

    /// <summary>
    /// The reference number of the document.
    /// This field is a part of the compound key of the document.
    /// </summary>
    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault]
    [PXUIField]
    [PXSelector(typeof (Search<ARDocumentEnq.ARRegister.refNbr, Where<ARDocumentEnq.ARRegister.docType, Equal<Optional<ARDocumentEnq.ARRegister.docType>>>>), Filterable = true)]
    public virtual string RefNbr { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.GL.Branch">branch</see> to which the document belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
    /// </value>
    [Branch(null, null, true, true, true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual int? BranchID { get; set; }

    [Customer(Enabled = false, Visible = false, DescriptionField = typeof (Customer.acctName), BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual int? CustomerID { get; set; }

    /// <summary>The date of the document.</summary>
    /// <value>
    /// Defaults to the current <see cref="P:PX.Data.AccessInfo.BusinessDate">Business Date</see>.
    /// </value>
    [PXDBDate(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? DocDate { get; set; }

    /// <summary>The description of the document.</summary>
    [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual string DocDesc { get; set; }

    [PXDBDate(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual DateTime? DueDate { get; set; }

    /// <summary>
    /// The code of the <see cref="T:PX.Objects.CM.Currency" /> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// Corresponds to the <see cref="!:Currency.CuryID" /> field.
    /// </value>
    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string CuryID { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the document.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="!:CurrencyInfoID" /> field.
    /// </value>
    [PXDBLong(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [CurrencyInfo]
    public virtual long? CuryInfoID { get; set; }

    /// <summary>The type of the original (source) document.</summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARDocumentPeriodResult.DocType" /> field.
    /// </value>
    [PXDBString(3, IsFixed = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [ARDocType.List]
    [PXUIField(DisplayName = "Orig. Doc. Type")]
    public virtual string OrigDocType { get; set; }

    /// <summary>
    /// The reference number of the original (source) document.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARDocumentPeriodResult.RefNbr" /> field.
    /// </value>
    [PXDBString(15, IsUnicode = true, InputMask = "", BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual string OrigRefNbr { get; set; }

    /// <summary>
    /// The status of the document.
    /// The value of the field is determined by the values of the status flags,
    /// such as <see cref="!:Hold" />, <see cref="P:PX.Objects.AR.ARDocumentEnq.ARDocumentPeriodResult.Released" />, <see cref="P:PX.Objects.AR.ARDocumentEnq.ARDocumentPeriodResult.Voided" />, <see cref="P:PX.Objects.AR.ARDocumentEnq.ARDocumentPeriodResult.Scheduled" />.
    /// </summary>
    /// <value>
    /// The field can have one of the values described in <see cref="T:PX.Objects.AR.ARDocStatus.ListAttribute" />.
    /// Defaults to <see cref="F:PX.Objects.AR.ARDocStatus.Hold" />.
    /// </value>
    [PXDBString(1, IsFixed = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault("H")]
    [PXUIField]
    [ARDocStatus.List]
    public virtual string Status { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Determined by the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.DocDate">date of the document</see>. Unlike <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.FinPeriodID" />
    /// the value of this field can't be virtualn by user.
    /// </value>
    [PeriodID(null, null, null, true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual string TranPeriodID { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the period, to which the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.DocDate" /> belongs, but can be virtualn by user.
    /// </value>
    [AROpenPeriod(typeof (ARDocumentEnq.ARDocumentPeriodResult.docDate), typeof (ARDocumentEnq.ARDocumentPeriodResult.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (ARDocumentEnq.ARDocumentPeriodResult.tranPeriodID), IsHeader = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual string FinPeriodID { get; set; }

    /// <summary>
    /// The <see cref="!:FinancialPeriod">Financial Period</see>, in which the document was closed.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARDocumentPeriodResult.FinPeriodID" /> field.
    /// </value>
    [FinPeriodID(null, typeof (ARDocumentEnq.ARDocumentPeriodResult.branchID), null, null, null, null, true, false, null, typeof (ARDocumentEnq.ARDocumentPeriodResult.closedTranPeriodID), null, true, true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual string ClosedFinPeriodID { get; set; }

    /// <summary>
    /// The <see cref="!:FinancialPeriod">Financial Period</see>, in which the document was closed.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARDocumentPeriodResult.TranPeriodID" /> field.
    /// </value>
    [PeriodID(null, null, null, true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual string ClosedTranPeriodID { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
    /// </value>
    [PXNote(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual Guid? NoteID
    {
      get => this._NoteID;
      set => this._NoteID = value;
    }

    /// <summary>
    /// When set to <c>true</c>, indicates that the document is open.
    /// </summary>
    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(true)]
    public virtual bool? OpenDoc { get; set; }

    /// <summary>
    /// When set to <c>true</c>, indicates that the document has been released.
    /// </summary>
    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(false)]
    public virtual bool? Released { get; set; }

    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField(DisplayName = "Retainage Document", Enabled = false, FieldClass = "Retainage")]
    [PXDefault(false)]
    public virtual bool? IsRetainageDocument { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.GL.Account">AR account</see> to which the document should be posted.
    /// The Cash account and Year-to-Date Net Income account cannot be selected as the value of this field.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
    /// </value>
    [PXDefault]
    [Account(typeof (ARDocumentEnq.ARDocumentPeriodResult.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<PX.Objects.GL.Account.isCashAccount, Equal<False>, And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>>), DisplayName = "AR Account", BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual int? ARAccountID { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.GL.Sub">subaccount</see> to which the document should be posted.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
    /// </value>
    [PXDefault]
    [SubAccount(typeof (ARDocumentEnq.ARDocumentPeriodResult.aRAccountID))]
    public virtual int? ARSubID { get; set; }

    /// <summary>
    /// Specifies (if set to <c>true</c>) that the record has been created
    /// in migration mode without affecting GL module.
    /// </summary>
    [MigratedRecord(typeof (PX.Objects.AR.ARSetup.migrationMode), BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual bool? IsMigratedRecord { get; set; }

    /// <summary>
    /// The counter of <see cref="!:TermsInstallment">installments</see> associated with the document.
    /// </summary>
    [PXDBShort(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual short? InstallmentCntr { get; set; }

    /// <summary>
    /// The number of the <see cref="T:PX.Objects.GL.Batch" /> created from the document on release.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Batch.BatchNbr" /> field.
    /// </value>
    [PXDBString(15, IsUnicode = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    [BatchNbr(typeof (Search<PX.Objects.GL.Batch.batchNbr, Where<PX.Objects.GL.Batch.module, Equal<BatchModule.moduleAR>>>), IsMigratedRecordField = typeof (ARDocumentEnq.ARRegister.isMigratedRecord))]
    public virtual string BatchNbr { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document is part of a <c>Schedule</c> and serves as a template for generating other documents according to it.
    /// </summary>
    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(false)]
    public virtual bool? Scheduled { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document has been voided.
    /// </summary>
    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(false)]
    public virtual bool? Voided { get; set; }

    [PXDBString(30, IsUnicode = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField(DisplayName = "Customer Order Nbr./Payment Nbr.")]
    public virtual string ExtRefNbr { get; set; }

    [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa", BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField(DisplayName = "Payment Method")]
    public virtual string PaymentMethodID { get; set; }

    /// <summary>
    /// The amount of the document.
    /// Given in the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARDocumentPeriodResult.CuryID">currency of the document</see>.
    /// </summary>
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (ARDocumentEnq.ARRegister.curyInfoID), typeof (ARDocumentEnq.ARRegister.origDocAmt), BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARTranPostGL.type, IBqlString>.IsEqual<ARTranPost.type.origin>>>, BqlOperand<ARTranPostGL.glSign, IBqlShort>.Multiply<ARDocumentEnq.ARDocumentResult.curyOrigDocAmt>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARDocumentEnq.ARDocumentResult.released, IBqlBool>.IsEqual<True>>>, Null>>, ARDocumentEnq.ARDocumentResult.curyOrigDocAmt>), typeof (Decimal))]
    [PXUIField]
    public virtual Decimal? CuryOrigDocAmt { get; set; }

    /// <summary>
    /// The amount of the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARTranPostGL.type, IBqlString>.IsEqual<ARTranPost.type.origin>>>, BqlOperand<ARTranPostGL.glSign, IBqlShort>.Multiply<ARDocumentEnq.ARDocumentResult.origDocAmt>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARDocumentEnq.ARDocumentResult.released, IBqlBool>.IsEqual<True>>>, Null>>, ARDocumentEnq.ARDocumentResult.origDocAmt>), typeof (Decimal))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField]
    public virtual Decimal? OrigDocAmt { get; set; }

    /// <summary>
    /// The cash discount entered for the document.
    /// Given in the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARDocumentPeriodResult.CuryID">currency of the document</see>.
    /// </summary>
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (ARDocumentEnq.ARDocumentPeriodResult.curyInfoID), typeof (ARDocumentEnq.ARDocumentPeriodResult.origDiscAmt), BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARTranPostGL.type, IBqlString>.IsEqual<ARTranPost.type.origin>>>, BqlOperand<ARTranPostGL.glSign, IBqlShort>.Multiply<ARDocumentEnq.ARDocumentResult.curyOrigDiscAmt>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARDocumentEnq.ARDocumentResult.released, IBqlBool>.IsEqual<True>>>, Null>>, ARDocumentEnq.ARDocumentResult.curyOrigDiscAmt>), typeof (Decimal))]
    public virtual Decimal? CuryOrigDiscAmt { get; set; }

    /// <summary>
    /// The cash discount entered for the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARTranPostGL.type, IBqlString>.IsEqual<ARTranPost.type.origin>>>, BqlOperand<ARTranPostGL.glSign, IBqlShort>.Multiply<ARDocumentEnq.ARDocumentResult.origDiscAmt>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARDocumentEnq.ARDocumentResult.released, IBqlBool>.IsEqual<True>>>, Null>>, ARDocumentEnq.ARDocumentResult.origDiscAmt>), typeof (Decimal))]
    public virtual Decimal? OrigDiscAmt { get; set; }

    [PXDBCurrency(typeof (ARDocumentEnq.ARDocumentPeriodResult.curyInfoID), typeof (ARDocumentEnq.ARDocumentPeriodResult.retainageTotal), BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? CuryRetainageTotal { get; set; }

    [PXDBBaseCury(null, null, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
    public virtual Decimal? RetainageTotal { get; set; }

    [PXCury(typeof (ARDocumentEnq.ARDocumentPeriodResult.curyID), BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARTranPostGL.type, IBqlString>.IsEqual<ARTranPost.type.origin>>>, BqlOperand<ARTranPostGL.glSign, IBqlShort>.Multiply<ARDocumentEnq.ARDocumentResult.curyOrigDocAmtWithRetainageTotal>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARDocumentEnq.ARDocumentResult.released, IBqlBool>.IsEqual<True>>>, Null>>, ARDocumentEnq.ARDocumentResult.curyOrigDocAmtWithRetainageTotal>), typeof (Decimal))]
    public virtual Decimal? CuryOrigDocAmtWithRetainageTotal { get; set; }

    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
    [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARTranPostGL.type, IBqlString>.IsEqual<ARTranPost.type.origin>>>, BqlOperand<ARTranPostGL.glSign, IBqlShort>.Multiply<ARDocumentEnq.ARDocumentResult.origDocAmtWithRetainageTotal>, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARDocumentEnq.ARDocumentResult.released, IBqlBool>.IsEqual<True>>>, Null>>, ARDocumentEnq.ARDocumentResult.origDocAmtWithRetainageTotal>), typeof (Decimal))]
    public virtual Decimal? OrigDocAmtWithRetainageTotal { get; set; }

    [PXCury(typeof (ARDocumentEnq.ARDocumentPeriodResult.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Currency Period Beg. Balance")]
    [PXDBCalced(typeof (Switch<Case<Where<ARDocumentEnq.ARDocumentResult.released, Equal<False>, And<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<False>, And<ARDocumentEnq.ARDocumentResult.finPeriodID, Less<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>>, ARDocumentEnq.ARDocumentResult.curyOrigDocAmt, Case<Where<ARDocumentEnq.ARDocumentResult.released, Equal<False>, And<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<True>, And<ARDocumentEnq.ARDocumentResult.tranPeriodID, Less<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>>, ARDocumentEnq.ARDocumentResult.curyOrigDocAmt, Case<Where<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<False>, And<ARTranPostGL.finPeriodID, Less<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>, ARTranPostGL.curyBalanceAmt, Case<Where<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<True>, And<ARTranPostGL.tranPeriodID, Less<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>, ARTranPostGL.curyBalanceAmt, Case<Where<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<False>, And<ARTranPostGL.finPeriodID, Less<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>, ARTranPostGL.curyBalanceAmt>>>>>, decimal0>), typeof (Decimal))]
    public virtual Decimal? CuryBegBalance { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Period Beg. Balance")]
    [PXDBCalced(typeof (Switch<Case<Where<ARDocumentEnq.ARDocumentResult.released, Equal<False>, And<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<False>, And<ARDocumentEnq.ARDocumentResult.finPeriodID, Less<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>>, ARDocumentEnq.ARDocumentResult.origDocAmt, Case<Where<ARDocumentEnq.ARDocumentResult.released, Equal<False>, And<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<True>, And<ARDocumentEnq.ARDocumentResult.tranPeriodID, Less<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>>, ARDocumentEnq.ARDocumentResult.origDocAmt, Case<Where<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<True>, And<ARTranPostGL.tranPeriodID, Less<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>, ARTranPostGL.balanceAmt, Case<Where<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<False>, And<ARTranPostGL.finPeriodID, Less<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>, ARTranPostGL.balanceAmt>>>>, decimal0>), typeof (Decimal))]
    public virtual Decimal? BegBalance { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<True>, And<ARTranPostGL.tranPeriodID, Equal<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>, decimal1, Case<Where<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<False>, And<ARTranPostGL.finPeriodID, Equal<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>, decimal1>>, decimal0>), typeof (Decimal))]
    public virtual Decimal? Turn { get; set; }

    /// <summary>
    /// Expected AR turnover for the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField(DisplayName = "AR Turnover")]
    public virtual Decimal? ARTurnover { get; set; }

    /// <summary>
    /// Expected GL turnover for the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXBaseCury]
    [PXUIField(DisplayName = "GL Turnover")]
    public virtual Decimal? GLTurnover { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCury(typeof (ARDocumentEnq.ARRegister.curyID))]
    [PXUIField(DisplayName = "Currency Cash Discount Taken")]
    [PXDBCalced(typeof (BqlFunction<Mult<ARTranPostGL.curyTurnDiscAmt, ARTranPostGL.glSign>, IBqlDecimal>.IfNullThen<decimal0>), typeof (Decimal))]
    public virtual Decimal? CuryDiscActTaken { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "Cash Discount Taken")]
    [PXDBCalced(typeof (BqlFunction<Mult<ARTranPostGL.turnDiscAmt, ARTranPostGL.glSign>, IBqlDecimal>.IfNullThen<decimal0>), typeof (Decimal))]
    public virtual Decimal? DiscActTaken { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCury(typeof (ARDocumentEnq.ARRegister.curyID))]
    [PXUIField(DisplayName = "Currency Write-Off Amount")]
    [PXDBCalced(typeof (IsNull<ARTranPostGL.curyTurnWOAmt, decimal0>), typeof (Decimal))]
    public virtual Decimal? CuryWOAmt { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Write-Off Amount")]
    [PXDBCalced(typeof (IsNull<ARTranPostGL.turnWOAmt, decimal0>), typeof (Decimal))]
    public virtual Decimal? WOAmt { get; set; }

    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "RGOL Amount")]
    [PXDBCalced(typeof (Switch<Case<Where<ARTranPostGL.type, NotEqual<ARTranPost.type.application>>, decimal0>, IsNull<Minus<ARTranPostGL.rGOLAmt>, decimal0>>), typeof (Decimal))]
    public virtual Decimal? RGOLAmt { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (ARDocumentEnq.ARDocumentPeriodResult.curyInfoID), typeof (ARDocumentEnq.ARDocumentPeriodResult.curyDocBal))]
    [PXUIField(DisplayName = "Currency Balance")]
    [PXDBCalced(typeof (Switch<Case<Where<ARDocumentEnq.ARDocumentResult.released, Equal<False>, And<BqlOperand<ARDocumentEnq.ARDocumentResult.docType, IBqlString>.IsNotIn<ARDocType.cashSale, ARDocType.cashReturn>>>, ARDocumentEnq.ARDocumentResult.curyOrigDocAmt>, ARTranPostGL.curyBalanceAmt>), typeof (Decimal))]
    public virtual Decimal? CuryDocBal { get; set; }

    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Balance")]
    [PXDBCalced(typeof (Switch<Case<Where<ARDocumentEnq.ARDocumentResult.released, Equal<False>, And<BqlOperand<ARDocumentEnq.ARDocumentResult.docType, IBqlString>.IsNotIn<ARDocType.cashSale, ARDocType.cashReturn>>>, ARDocumentEnq.ARDocumentResult.origDocAmt>, ARTranPostGL.balanceAmt>), typeof (Decimal))]
    public virtual Decimal? DocBal { get; set; }

    [PXCurrency(typeof (ARDocumentEnq.ARDocumentPeriodResult.curyInfoID), typeof (ARDocumentEnq.ARDocumentPeriodResult.retainageUnreleasedAmt), BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCalced(typeof (BqlFunction<Mult<ARTranPostGL.curyRetainageUnreleasedAmt, ARTranPostGL.balanceSign>, IBqlDecimal>.IfNullThen<decimal0>), typeof (Decimal))]
    public virtual Decimal? CuryRetainageUnreleasedAmt { get; set; }

    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
    [PXDBCalced(typeof (BqlFunction<Mult<ARTranPostGL.retainageUnreleasedAmt, ARTranPostGL.balanceSign>, IBqlDecimal>.IfNullThen<decimal0>), typeof (Decimal))]
    public virtual Decimal? RetainageUnreleasedAmt { get; set; }

    [PeriodID(null, null, null, true, BqlField = typeof (ARTranPostGL.tranPeriodID))]
    public virtual string TranPostPeriodID { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the period, to which the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.DocDate" /> belongs, but can be virtualn by user.
    /// </value>
    [AROpenPeriod(typeof (ARDocumentEnq.ARDocumentPeriodResult.docDate), typeof (ARDocumentEnq.ARDocumentPeriodResult.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (ARDocumentEnq.ARDocumentPeriodResult.tranPostPeriodID), IsHeader = true, BqlField = typeof (ARTranPostGL.finPeriodID))]
    public virtual string FinPostPeriodID { get; set; }

    [Account(typeof (ARDocumentEnq.ARDocumentPeriodResult.branchID), IsDBField = false, DisplayName = "Account", BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARDocumentEnq.ARDocumentResult.docType, IBqlString>.IsEqual<ARDocType.prepaymentInvoice>>, ARTranPostGL.accountID>, ARDocumentEnq.ARDocumentResult.aRAccountID>), typeof (int?))]
    public virtual int? AccountID { get; set; }

    [SubAccount(typeof (ARDocumentEnq.ARDocumentPeriodResult.accountID))]
    [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARDocumentEnq.ARDocumentResult.docType, IBqlString>.IsEqual<ARDocType.prepaymentInvoice>>, ARTranPostGL.subID>, ARDocumentEnq.ARDocumentResult.aRSubID>), typeof (int?))]
    public virtual int? SubID { get; set; }

    [Project(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual int? ProjectID { get; set; }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.docType>
    {
      public const int Length = 3;
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.refNbr>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.branchID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.customerID>
    {
    }

    public abstract class docDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.docDate>
    {
    }

    public abstract class docDesc : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.docDesc>
    {
    }

    public abstract class dueDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.dueDate>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.curyID>
    {
    }

    public abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.curyInfoID>
    {
    }

    public abstract class origDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.origDocType>
    {
    }

    public abstract class origRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.origRefNbr>
    {
    }

    public abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.status>
    {
    }

    public abstract class tranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.tranPeriodID>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.finPeriodID>
    {
    }

    public abstract class closedFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.closedFinPeriodID>
    {
    }

    public abstract class closedTranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.closedTranPeriodID>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.noteID>
    {
    }

    public abstract class openDoc : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.openDoc>
    {
    }

    public abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.released>
    {
    }

    public abstract class isRetainageDocument : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.isRetainageDocument>
    {
    }

    public abstract class aRAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.aRAccountID>
    {
    }

    public abstract class aRSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.aRSubID>
    {
    }

    public abstract class isMigratedRecord : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.isMigratedRecord>
    {
    }

    public abstract class installmentCntr : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.installmentCntr>
    {
    }

    public abstract class batchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.batchNbr>
    {
    }

    public abstract class scheduled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.scheduled>
    {
    }

    public abstract class voided : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.voided>
    {
    }

    public abstract class extRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.extRefNbr>
    {
    }

    public abstract class paymentMethodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.paymentMethodID>
    {
    }

    public abstract class curyOrigDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.curyOrigDocAmt>
    {
    }

    public abstract class origDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.origDocAmt>
    {
    }

    public abstract class curyOrigDiscAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.curyOrigDiscAmt>
    {
    }

    public abstract class origDiscAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.origDiscAmt>
    {
    }

    public abstract class curyRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.curyRetainageTotal>
    {
    }

    public abstract class retainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.retainageTotal>
    {
    }

    public abstract class curyOrigDocAmtWithRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.curyOrigDocAmtWithRetainageTotal>
    {
    }

    public abstract class origDocAmtWithRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.origDocAmtWithRetainageTotal>
    {
    }

    public abstract class curyBegBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.curyBegBalance>
    {
    }

    public abstract class begBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.begBalance>
    {
    }

    public abstract class turn : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.turn>
    {
    }

    public abstract class aRTurnover : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.aRTurnover>
    {
    }

    public abstract class gLTurnover : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.gLTurnover>
    {
    }

    public abstract class curyDiscActTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.curyDiscActTaken>
    {
    }

    public abstract class discActTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.discActTaken>
    {
    }

    public abstract class curyWOAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.curyWOAmt>
    {
    }

    public abstract class woAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.woAmt>
    {
    }

    public abstract class rGOLAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.rGOLAmt>
    {
    }

    public abstract class curyDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.curyDocBal>
    {
    }

    public abstract class docBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.docBal>
    {
    }

    public abstract class curyRetainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.curyRetainageUnreleasedAmt>
    {
    }

    public abstract class retainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.retainageUnreleasedAmt>
    {
    }

    public abstract class tranPostPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.tranPostPeriodID>
    {
    }

    public abstract class finPostPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.finPostPeriodID>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.accountID>
    {
    }

    public abstract class subID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.subID>
    {
    }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPeriodResult.projectID>
    {
    }
  }

  [PXProjection(typeof (Select2<ARDocumentEnq.ARDocumentResult, LeftJoin<ARTranPostGL, On<ARTranPostGL.docType, Equal<ARDocType.prepaymentInvoice>, And<ARTranPostGL.docType, Equal<ARDocumentEnq.ARDocumentResult.docType>, And<ARTranPostGL.refNbr, Equal<ARDocumentEnq.ARDocumentResult.refNbr>>>>>>))]
  [PXHidden]
  [Serializable]
  public class ARDocumentPPIPeriodResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected Decimal? _CuryBegBalance;

    [PXDBString(3, IsKey = true, IsFixed = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault]
    [ARDocType.List]
    [PXUIField]
    public virtual string DocType { get; set; }

    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault]
    [PXUIField]
    [PXSelector(typeof (Search<ARDocumentEnq.ARRegister.refNbr, Where<ARDocumentEnq.ARRegister.docType, Equal<Optional<ARDocumentEnq.ARRegister.docType>>>>), Filterable = true)]
    public virtual string RefNbr { get; set; }

    [Branch(null, null, true, true, true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual int? BranchID { get; set; }

    [Customer(Enabled = false, Visible = false, DescriptionField = typeof (Customer.acctName), BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual int? CustomerID { get; set; }

    [PXDBDate(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? DocDate { get; set; }

    [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual string DocDesc { get; set; }

    [PXDBDate(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual DateTime? DueDate { get; set; }

    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string CuryID { get; set; }

    [PXDBLong(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [CurrencyInfo]
    public virtual long? CuryInfoID { get; set; }

    [PXDBString(3, IsFixed = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [ARDocType.List]
    [PXUIField(DisplayName = "Orig. Doc. Type")]
    public virtual string OrigDocType { get; set; }

    [PXDBString(15, IsUnicode = true, InputMask = "", BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual string OrigRefNbr { get; set; }

    [PXDBString(1, IsFixed = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault("H")]
    [PXUIField]
    [ARDocStatus.List]
    public virtual string Status { get; set; }

    [PeriodID(null, null, null, true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual string TranPeriodID { get; set; }

    [AROpenPeriod(typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.docDate), typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.tranPeriodID), IsHeader = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual string FinPeriodID { get; set; }

    [FinPeriodID(null, typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.branchID), null, null, null, null, true, false, null, typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.closedTranPeriodID), null, true, true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual string ClosedFinPeriodID { get; set; }

    [PeriodID(null, null, null, true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual string ClosedTranPeriodID { get; set; }

    [PXNote(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual Guid? NoteID { get; set; }

    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(true)]
    public virtual bool? OpenDoc { get; set; }

    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(false)]
    public virtual bool? Released { get; set; }

    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField(DisplayName = "Retainage Document", Enabled = false, FieldClass = "Retainage")]
    [PXDefault(false)]
    public virtual bool? IsRetainageDocument { get; set; }

    [PXDefault]
    [Account(typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<PX.Objects.GL.Account.isCashAccount, Equal<False>, And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>>), DisplayName = "AR Account", BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual int? ARAccountID { get; set; }

    [PXDefault]
    [SubAccount(typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.aRAccountID))]
    public virtual int? ARSubID { get; set; }

    [MigratedRecord(typeof (PX.Objects.AR.ARSetup.migrationMode), BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual bool? IsMigratedRecord { get; set; }

    [PXDBShort(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual short? InstallmentCntr { get; set; }

    [PXDBString(15, IsUnicode = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    [BatchNbr(typeof (Search<PX.Objects.GL.Batch.batchNbr, Where<PX.Objects.GL.Batch.module, Equal<BatchModule.moduleAR>>>), IsMigratedRecordField = typeof (ARDocumentEnq.ARRegister.isMigratedRecord))]
    public virtual string BatchNbr { get; set; }

    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(false)]
    public virtual bool? Scheduled { get; set; }

    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(false)]
    public virtual bool? Voided { get; set; }

    [PXDBString(30, IsUnicode = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField(DisplayName = "Customer Order Nbr./Payment Nbr.")]
    public virtual string ExtRefNbr { get; set; }

    [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa", BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField(DisplayName = "Payment Method")]
    public virtual string PaymentMethodID { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (ARDocumentEnq.ARRegister.curyInfoID), typeof (ARDocumentEnq.ARRegister.origDocAmt), BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARTranPostGL.type, IBqlString>.IsEqual<ARTranPost.type.origin>>>, ARTranPostGL.curyTurnAmt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARDocumentEnq.ARDocumentResult.released, IBqlBool>.IsEqual<True>>>, Null>>, ARDocumentEnq.ARDocumentResult.curyOrigDocAmt>), typeof (Decimal))]
    [PXUIField]
    public virtual Decimal? CuryOrigDocAmt { get; set; }

    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARTranPostGL.type, IBqlString>.IsEqual<ARTranPost.type.origin>>>, ARTranPostGL.turnAmt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARDocumentEnq.ARDocumentResult.released, IBqlBool>.IsEqual<True>>>, Null>>, ARDocumentEnq.ARDocumentResult.origDocAmt>), typeof (Decimal))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField]
    public virtual Decimal? OrigDocAmt { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.curyInfoID), typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.origDiscAmt), BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual Decimal? CuryOrigDiscAmt { get; set; }

    [PXDBBaseCury(null, null, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? OrigDiscAmt { get; set; }

    [PXDBCurrency(typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.curyInfoID), typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.retainageTotal), BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? CuryRetainageTotal { get; set; }

    [PXDBBaseCury(null, null, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
    public virtual Decimal? RetainageTotal { get; set; }

    [PXCury(typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.curyID), BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARTranPostGL.type, IBqlString>.IsEqual<ARTranPost.type.origin>>>, ARTranPostGL.curyTurnAmt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARDocumentEnq.ARDocumentResult.released, IBqlBool>.IsEqual<True>>>, Null>>, ARDocumentEnq.ARDocumentResult.curyOrigDocAmtWithRetainageTotal>), typeof (Decimal))]
    public virtual Decimal? CuryOrigDocAmtWithRetainageTotal { get; set; }

    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
    [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARTranPostGL.type, IBqlString>.IsEqual<ARTranPost.type.origin>>>, ARTranPostGL.turnAmt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARDocumentEnq.ARDocumentResult.released, IBqlBool>.IsEqual<True>>>, Null>>, ARDocumentEnq.ARDocumentResult.origDocAmtWithRetainageTotal>), typeof (Decimal))]
    public virtual Decimal? OrigDocAmtWithRetainageTotal { get; set; }

    [PXCury(typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Currency Period Beg. Balance")]
    [PXDBCalced(typeof (Switch<Case<Where<ARDocumentEnq.ARDocumentResult.released, Equal<False>, And<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<False>, And<ARDocumentEnq.ARDocumentResult.finPeriodID, Less<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>>, ARDocumentEnq.ARDocumentResult.curyOrigDocAmt, Case<Where<ARDocumentEnq.ARDocumentResult.released, Equal<False>, And<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<True>, And<ARDocumentEnq.ARDocumentResult.tranPeriodID, Less<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>>, ARDocumentEnq.ARDocumentResult.curyOrigDocAmt, Case<Where<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<False>, And<ARTranPostGL.finPeriodID, Less<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>, ARTranPostGL.curyBalanceAmt, Case<Where<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<True>, And<ARTranPostGL.tranPeriodID, Less<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>, ARTranPostGL.curyBalanceAmt, Case<Where<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<False>, And<ARTranPostGL.finPeriodID, Less<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>, ARTranPostGL.curyBalanceAmt>>>>>, decimal0>), typeof (Decimal))]
    public virtual Decimal? CuryBegBalance { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Period Beg. Balance")]
    [PXDBCalced(typeof (Switch<Case<Where<ARDocumentEnq.ARDocumentResult.released, Equal<False>, And<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<False>, And<ARDocumentEnq.ARDocumentResult.finPeriodID, Less<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>>, ARDocumentEnq.ARDocumentResult.origDocAmt, Case<Where<ARDocumentEnq.ARDocumentResult.released, Equal<False>, And<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<True>, And<ARDocumentEnq.ARDocumentResult.tranPeriodID, Less<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>>, ARDocumentEnq.ARDocumentResult.origDocAmt, Case<Where<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<True>, And<ARTranPostGL.tranPeriodID, Less<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>, ARTranPostGL.balanceAmt, Case<Where<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<False>, And<ARTranPostGL.finPeriodID, Less<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>, ARTranPostGL.balanceAmt>>>>, decimal0>), typeof (Decimal))]
    public virtual Decimal? BegBalance { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<True>, And<ARTranPostGL.tranPeriodID, Equal<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>, decimal1, Case<Where<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<False>, And<ARTranPostGL.finPeriodID, Equal<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>, decimal1>>, decimal0>), typeof (Decimal))]
    public virtual Decimal? Turn { get; set; }

    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField(DisplayName = "AR Turnover")]
    public virtual Decimal? ARTurnover { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "GL Turnover")]
    public virtual Decimal? GLTurnover { get; set; }

    [PXCury(typeof (ARDocumentEnq.ARRegister.curyID))]
    [PXUIField(DisplayName = "Currency Cash Discount Taken")]
    [PXDBCalced(typeof (BqlFunction<Mult<ARTranPostGL.curyTurnDiscAmt, ARTranPostGL.glSign>, IBqlDecimal>.IfNullThen<ARDocumentEnq.ARDocumentResult.curyDiscActTaken>), typeof (Decimal))]
    public virtual Decimal? CuryDiscActTaken { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "Cash Discount Taken")]
    [PXDBCalced(typeof (BqlFunction<Mult<ARTranPostGL.turnDiscAmt, ARTranPostGL.glSign>, IBqlDecimal>.IfNullThen<ARDocumentEnq.ARDocumentResult.discActTaken>), typeof (Decimal))]
    public virtual Decimal? DiscActTaken { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCury(typeof (ARDocumentEnq.ARRegister.curyID))]
    [PXUIField(DisplayName = "Currency Write-Off Amount")]
    [PXDBCalced(typeof (IsNull<ARTranPostGL.curyTurnWOAmt, decimal0>), typeof (Decimal))]
    public virtual Decimal? CuryWOAmt { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Write-Off Amount")]
    [PXDBCalced(typeof (IsNull<ARTranPostGL.turnWOAmt, decimal0>), typeof (Decimal))]
    public virtual Decimal? WOAmt { get; set; }

    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "RGOL Amount")]
    [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARTranPostGL.type, IBqlString>.IsEqual<ARTranPost.type.application>>>, Minus<ARTranPostGL.rGOLAmt>>, IsNull<ARDocumentEnq.ARDocumentResult.rGOLAmt, decimal0>>), typeof (Decimal))]
    public virtual Decimal? RGOLAmt { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.curyInfoID), typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.curyDocBal))]
    [PXUIField(DisplayName = "Currency Balance")]
    [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARDocumentEnq.ARDocumentResult.released, IBqlBool>.IsEqual<True>>>, ARTranPostGL.curyBalanceAmt>, ARDocumentEnq.ARDocumentResult.curyDocBal>), typeof (Decimal))]
    public virtual Decimal? CuryDocBal { get; set; }

    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Balance")]
    [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARDocumentEnq.ARDocumentResult.released, IBqlBool>.IsEqual<True>>>, ARTranPostGL.balanceAmt>, ARDocumentEnq.ARDocumentResult.docBal>), typeof (Decimal))]
    public virtual Decimal? DocBal { get; set; }

    [PXCurrency(typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.curyInfoID), typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.retainageUnreleasedAmt), BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARDocumentEnq.ARDocumentResult.released, IBqlBool>.IsEqual<True>>>, BqlOperand<ARTranPostGL.curyRetainageUnreleasedAmt, IBqlDecimal>.Multiply<ARTranPostGL.balanceSign>>, ARDocumentEnq.ARDocumentResult.curyRetainageUnreleasedAmt>), typeof (Decimal))]
    public virtual Decimal? CuryRetainageUnreleasedAmt { get; set; }

    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
    [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARDocumentEnq.ARDocumentResult.released, IBqlBool>.IsEqual<True>>>, BqlOperand<ARTranPostGL.retainageUnreleasedAmt, IBqlDecimal>.Multiply<ARTranPostGL.balanceSign>>, ARDocumentEnq.ARDocumentResult.retainageUnreleasedAmt>), typeof (Decimal))]
    public virtual Decimal? RetainageUnreleasedAmt { get; set; }

    [PeriodID(null, null, null, true, BqlField = typeof (ARTranPostGL.tranPeriodID))]
    public virtual string TranPostPeriodID { get; set; }

    [AROpenPeriod(typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.docDate), typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.tranPostPeriodID), IsHeader = true, BqlField = typeof (ARTranPostGL.finPeriodID))]
    public virtual string FinPostPeriodID { get; set; }

    [Account(typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.branchID), IsDBField = false, DisplayName = "Account", BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARDocumentEnq.ARDocumentResult.released, IBqlBool>.IsEqual<True>>>, ARTranPostGL.accountID>, ARDocumentEnq.ARDocumentResult.aRAccountID>), typeof (int?))]
    public virtual int? AccountID { get; set; }

    [SubAccount(typeof (ARDocumentEnq.ARDocumentPPIPeriodResult.accountID))]
    [PXDBCalced(typeof (Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARDocumentEnq.ARDocumentResult.docType, Equal<ARDocType.prepaymentInvoice>>>>>.And<BqlOperand<ARDocumentEnq.ARDocumentResult.released, IBqlBool>.IsEqual<True>>>, ARTranPostGL.subID>, ARDocumentEnq.ARDocumentResult.aRSubID>), typeof (int?))]
    public virtual int? SubID { get; set; }

    [Project(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual int? ProjectID { get; set; }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.docType>
    {
      public const int Length = 3;
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.refNbr>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.branchID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.customerID>
    {
    }

    public abstract class docDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.docDate>
    {
    }

    public abstract class docDesc : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.docDesc>
    {
    }

    public abstract class dueDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.dueDate>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.curyID>
    {
    }

    public abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.curyInfoID>
    {
    }

    public abstract class origDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.origDocType>
    {
    }

    public abstract class origRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.origRefNbr>
    {
    }

    public abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.status>
    {
    }

    public abstract class tranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.tranPeriodID>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.finPeriodID>
    {
    }

    public abstract class closedFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.closedFinPeriodID>
    {
    }

    public abstract class closedTranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.closedTranPeriodID>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.noteID>
    {
    }

    public abstract class openDoc : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.openDoc>
    {
    }

    public abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.released>
    {
    }

    public abstract class isRetainageDocument : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.isRetainageDocument>
    {
    }

    public abstract class aRAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.aRAccountID>
    {
    }

    public abstract class aRSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.aRSubID>
    {
    }

    public abstract class isMigratedRecord : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.isMigratedRecord>
    {
    }

    public abstract class installmentCntr : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.installmentCntr>
    {
    }

    public abstract class batchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.batchNbr>
    {
    }

    public abstract class scheduled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.scheduled>
    {
    }

    public abstract class voided : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.voided>
    {
    }

    public abstract class extRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.extRefNbr>
    {
    }

    public abstract class paymentMethodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.paymentMethodID>
    {
    }

    public abstract class curyOrigDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.curyOrigDocAmt>
    {
    }

    public abstract class origDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.origDocAmt>
    {
    }

    public abstract class curyOrigDiscAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.curyOrigDiscAmt>
    {
    }

    public abstract class origDiscAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.origDiscAmt>
    {
    }

    public abstract class curyRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.curyRetainageTotal>
    {
    }

    public abstract class retainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.retainageTotal>
    {
    }

    public abstract class curyOrigDocAmtWithRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.curyOrigDocAmtWithRetainageTotal>
    {
    }

    public abstract class origDocAmtWithRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.origDocAmtWithRetainageTotal>
    {
    }

    public abstract class curyBegBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.curyBegBalance>
    {
    }

    public abstract class begBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.begBalance>
    {
    }

    public abstract class turn : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.turn>
    {
    }

    public abstract class aRTurnover : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.aRTurnover>
    {
    }

    public abstract class gLTurnover : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.gLTurnover>
    {
    }

    public abstract class curyDiscActTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.curyDiscActTaken>
    {
    }

    public abstract class discActTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.discActTaken>
    {
    }

    public abstract class curyWOAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.curyWOAmt>
    {
    }

    public abstract class woAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.woAmt>
    {
    }

    public abstract class rGOLAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.rGOLAmt>
    {
    }

    public abstract class curyDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.curyDocBal>
    {
    }

    public abstract class docBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.docBal>
    {
    }

    public abstract class curyRetainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.curyRetainageUnreleasedAmt>
    {
    }

    public abstract class retainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.retainageUnreleasedAmt>
    {
    }

    public abstract class tranPostPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.tranPostPeriodID>
    {
    }

    public abstract class finPostPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.finPostPeriodID>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.accountID>
    {
    }

    public abstract class subID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.subID>
    {
    }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.ARDocumentPPIPeriodResult.projectID>
    {
    }
  }

  [PXProjection(typeof (Select2<ARDocumentEnq.ARDocumentResult, LeftJoin<ARTranPostGL, On<ARTranPostGL.tranType, Equal<ARDocumentEnq.ARDocumentResult.docType>, And<ARTranPostGL.tranRefNbr, Equal<ARDocumentEnq.ARDocumentResult.refNbr>>>>>))]
  [PXHidden]
  [Serializable]
  public class GLDocumentPeriodResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected Guid? _NoteID;
    protected Decimal? _CuryBegBalance;

    /// <summary>
    /// The type of the document.
    /// This field is a part of the compound key of the document.
    /// </summary>
    /// <value>
    /// The field can have one of the values described in <see cref="T:PX.Objects.AR.ARDocType.ListAttribute" />.
    /// </value>
    [PXDBString(3, IsKey = true, IsFixed = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault]
    [ARDocType.List]
    [PXUIField]
    public virtual string DocType { get; set; }

    /// <summary>
    /// The reference number of the document.
    /// This field is a part of the compound key of the document.
    /// </summary>
    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault]
    [PXUIField]
    [PXSelector(typeof (Search<ARDocumentEnq.ARRegister.refNbr, Where<ARDocumentEnq.ARRegister.docType, Equal<Optional<ARDocumentEnq.ARRegister.docType>>>>), Filterable = true)]
    public virtual string RefNbr { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.GL.Branch">branch</see> to which the document belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
    /// </value>
    [Branch(null, null, true, true, true, BqlTable = typeof (ARTranPostGL))]
    public virtual int? BranchID { get; set; }

    [Customer(Enabled = false, Visible = false, DescriptionField = typeof (Customer.acctName), BqlField = typeof (ARTranPostGL.referenceID))]
    public virtual int? CustomerID { get; set; }

    /// <summary>The date of the document.</summary>
    /// <value>
    /// Defaults to the current <see cref="P:PX.Data.AccessInfo.BusinessDate">Business Date</see>.
    /// </value>
    [PXDBDate(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? DocDate { get; set; }

    /// <summary>The description of the document.</summary>
    [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual string DocDesc { get; set; }

    [PXDBDate(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual DateTime? DueDate { get; set; }

    /// <summary>
    /// The code of the <see cref="T:PX.Objects.CM.Currency" /> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// Corresponds to the <see cref="!:Currency.CuryID" /> field.
    /// </value>
    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string CuryID { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the document.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="!:CurrencyInfoID" /> field.
    /// </value>
    [PXDBLong(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [CurrencyInfo]
    public virtual long? CuryInfoID { get; set; }

    /// <summary>The type of the original (source) document.</summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AR.ARDocumentEnq.GLDocumentPeriodResult.DocType" /> field.
    /// </value>
    [PXDBString(3, IsFixed = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [ARDocType.List]
    [PXUIField(DisplayName = "Orig. Doc. Type")]
    public virtual string OrigDocType { get; set; }

    /// <summary>
    /// The reference number of the original (source) document.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AR.ARDocumentEnq.GLDocumentPeriodResult.RefNbr" /> field.
    /// </value>
    [PXDBString(15, IsUnicode = true, InputMask = "", BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual string OrigRefNbr { get; set; }

    /// <summary>
    /// The status of the document.
    /// The value of the field is determined by the values of the status flags,
    /// such as <see cref="!:Hold" />, <see cref="P:PX.Objects.AR.ARDocumentEnq.GLDocumentPeriodResult.Released" />, <see cref="P:PX.Objects.AR.ARDocumentEnq.GLDocumentPeriodResult.Voided" />, <see cref="P:PX.Objects.AR.ARDocumentEnq.GLDocumentPeriodResult.Scheduled" />.
    /// </summary>
    /// <value>
    /// The field can have one of the values described in <see cref="T:PX.Objects.AR.ARDocStatus.ListAttribute" />.
    /// Defaults to <see cref="F:PX.Objects.AR.ARDocStatus.Hold" />.
    /// </value>
    [PXDBString(1, IsFixed = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault("H")]
    [PXUIField]
    [ARDocStatus.List]
    public virtual string Status { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Determined by the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.DocDate">date of the document</see>. Unlike <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.FinPeriodID" />
    /// the value of this field can't be virtualn by user.
    /// </value>
    [PeriodID(null, null, null, true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual string TranPeriodID { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the period, to which the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.DocDate" /> belongs, but can be virtualn by user.
    /// </value>
    [AROpenPeriod(typeof (ARDocumentEnq.GLDocumentPeriodResult.docDate), typeof (ARDocumentEnq.GLDocumentPeriodResult.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (ARDocumentEnq.GLDocumentPeriodResult.tranPeriodID), IsHeader = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual string FinPeriodID { get; set; }

    /// <summary>
    /// The <see cref="!:FinancialPeriod">Financial Period</see>, in which the document was closed.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AR.ARDocumentEnq.GLDocumentPeriodResult.FinPeriodID" /> field.
    /// </value>
    [FinPeriodID(null, typeof (ARDocumentEnq.GLDocumentPeriodResult.branchID), null, null, null, null, true, false, null, typeof (ARDocumentEnq.GLDocumentPeriodResult.closedTranPeriodID), null, true, true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual string ClosedFinPeriodID { get; set; }

    /// <summary>
    /// The <see cref="!:FinancialPeriod">Financial Period</see>, in which the document was closed.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.AR.ARDocumentEnq.GLDocumentPeriodResult.TranPeriodID" /> field.
    /// </value>
    [PeriodID(null, null, null, true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual string ClosedTranPeriodID { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Data.Note">Note</see> object, associated with the document.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Data.Note.NoteID">Note.NoteID</see> field.
    /// </value>
    [PXNote(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual Guid? NoteID
    {
      get => this._NoteID;
      set => this._NoteID = value;
    }

    /// <summary>
    /// When set to <c>true</c>, indicates that the document is open.
    /// </summary>
    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(true)]
    public virtual bool? OpenDoc { get; set; }

    /// <summary>
    /// When set to <c>true</c>, indicates that the document has been released.
    /// </summary>
    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(false)]
    public virtual bool? Released { get; set; }

    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField(DisplayName = "Retainage Document", Enabled = false, FieldClass = "Retainage")]
    [PXDefault(false)]
    public virtual bool? IsRetainageDocument { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.GL.Account">AR account</see> to which the document should be posted.
    /// The Cash account and Year-to-Date Net Income account cannot be selected as the value of this field.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Account.AccountID" /> field.
    /// </value>
    [PXDefault]
    [Account(typeof (ARDocumentEnq.GLDocumentPeriodResult.branchID), typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<PX.Objects.GL.Account.isCashAccount, Equal<False>, And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>>), DisplayName = "AR Account", BqlField = typeof (ARTranPostGL.accountID))]
    public virtual int? ARAccountID { get; set; }

    /// <summary>
    /// The identifier of the <see cref="T:PX.Objects.GL.Sub">subaccount</see> to which the document should be posted.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Sub.SubID" /> field.
    /// </value>
    [PXDefault]
    [SubAccount(typeof (ARDocumentEnq.GLDocumentPeriodResult.aRAccountID))]
    public virtual int? ARSubID { get; set; }

    /// <summary>
    /// Specifies (if set to <c>true</c>) that the record has been created
    /// in migration mode without affecting GL module.
    /// </summary>
    [MigratedRecord(typeof (PX.Objects.AR.ARSetup.migrationMode), BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual bool? IsMigratedRecord { get; set; }

    /// <summary>
    /// The counter of <see cref="!:TermsInstallment">installments</see> associated with the document.
    /// </summary>
    [PXDBShort(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual short? InstallmentCntr { get; set; }

    /// <summary>
    /// The number of the <see cref="T:PX.Objects.GL.Batch" /> created from the document on release.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Batch.BatchNbr" /> field.
    /// </value>
    [PXDBString(15, IsUnicode = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    [BatchNbr(typeof (Search<PX.Objects.GL.Batch.batchNbr, Where<PX.Objects.GL.Batch.module, Equal<BatchModule.moduleAR>>>), IsMigratedRecordField = typeof (ARDocumentEnq.ARRegister.isMigratedRecord))]
    public virtual string BatchNbr { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document is part of a <c>Schedule</c> and serves as a template for generating other documents according to it.
    /// </summary>
    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(false)]
    public virtual bool? Scheduled { get; set; }

    /// <summary>
    /// When set to <c>true</c> indicates that the document has been voided.
    /// </summary>
    [PXDBBool(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(false)]
    public virtual bool? Voided { get; set; }

    [PXDBString(30, IsUnicode = true, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField(DisplayName = "Customer Order Nbr./Payment Nbr.")]
    public virtual string ExtRefNbr { get; set; }

    [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa", BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField(DisplayName = "Payment Method")]
    public virtual string PaymentMethodID { get; set; }

    /// <summary>
    /// The amount of the document.
    /// Given in the <see cref="P:PX.Objects.AR.ARDocumentEnq.GLDocumentPeriodResult.CuryID">currency of the document</see>.
    /// </summary>
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (ARDocumentEnq.ARRegister.curyInfoID), typeof (ARDocumentEnq.ARRegister.origDocAmt), BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual Decimal? CuryOrigDocAmt { get; set; }

    /// <summary>
    /// The amount of the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXDBBaseCury(null, null, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField]
    public virtual Decimal? OrigDocAmt { get; set; }

    /// <summary>
    /// The cash discount entered for the document.
    /// Given in the <see cref="P:PX.Objects.AR.ARDocumentEnq.GLDocumentPeriodResult.CuryID">currency of the document</see>.
    /// </summary>
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCurrency(typeof (ARDocumentEnq.GLDocumentPeriodResult.curyInfoID), typeof (ARDocumentEnq.GLDocumentPeriodResult.origDiscAmt), BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField]
    public virtual Decimal? CuryOrigDiscAmt { get; set; }

    /// <summary>
    /// The cash discount entered for the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXDBBaseCury(null, null, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? OrigDiscAmt { get; set; }

    [PXDBCurrency(typeof (ARDocumentEnq.GLDocumentPeriodResult.curyInfoID), typeof (ARDocumentEnq.GLDocumentPeriodResult.retainageTotal), BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? CuryRetainageTotal { get; set; }

    [PXDBBaseCury(null, null, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Original Retainage", FieldClass = "Retainage")]
    public virtual Decimal? RetainageTotal { get; set; }

    [PXDBCury(typeof (ARDocumentEnq.GLDocumentPeriodResult.curyID), BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public virtual Decimal? CuryOrigDocAmtWithRetainageTotal { get; set; }

    [PXDBBaseCury(null, null, BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Amount", FieldClass = "Retainage")]
    public virtual Decimal? OrigDocAmtWithRetainageTotal { get; set; }

    [PXCury(typeof (ARDocumentEnq.GLDocumentPeriodResult.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Currency Period Beg. Balance")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? CuryBegBalance { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Period Beg. Balance")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? BegBalance { get; set; }

    [PXCury(typeof (ARDocumentEnq.ARRegister.curyID))]
    [PXUIField(DisplayName = "Currency Cash Discount Taken")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? CuryDiscActTaken { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "Cash Discount Taken")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? DiscActTaken { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCury(typeof (ARDocumentEnq.ARRegister.curyID))]
    [PXUIField(DisplayName = "Currency Write-Off Amount")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? CuryWOAmt { get; set; }

    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Write-Off Amount")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? WOAmt { get; set; }

    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "RGOL Amount")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? RGOLAmt { get; set; }

    /// <summary>
    /// Expected GL turnover for the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField(DisplayName = "AR Turnover")]
    [PXDBCalced(typeof (Switch<Case<Where<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<True>, And<ARTranPostGL.tranPeriodID, Equal<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>, Sub<ARTranPostGL.debitARAmt, ARTranPostGL.creditARAmt>, Case<Where<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<False>, And<ARTranPostGL.finPeriodID, Equal<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>, Sub<ARTranPostGL.debitARAmt, ARTranPostGL.creditARAmt>>>, decimal0>), typeof (Decimal))]
    public virtual Decimal? ARTurnover { get; set; }

    /// <summary>
    /// Expected GL turnover for the document.
    /// Given in the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
    /// </summary>
    [PXBaseCury]
    [PXUIField(DisplayName = "GL Turnover")]
    public virtual Decimal? GLTurnover { get; set; }

    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXCurrency(typeof (ARDocumentEnq.GLDocumentPeriodResult.curyInfoID), typeof (ARDocumentEnq.GLDocumentPeriodResult.curyDocBal))]
    [PXUIField(DisplayName = "Currency Balance")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? CuryDocBal { get; set; }

    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Balance")]
    [PXDBCalced(typeof (decimal0), typeof (Decimal))]
    public virtual Decimal? DocBal { get; set; }

    [PXCurrency(typeof (ARDocumentEnq.GLDocumentPeriodResult.curyInfoID), typeof (ARDocumentEnq.GLDocumentPeriodResult.retainageUnreleasedAmt), BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXDBCalced(typeof (IsNull<ARTranPostGL.curyRetainageUnreleasedAmt, decimal0>), typeof (Decimal))]
    public virtual Decimal? CuryRetainageUnreleasedAmt { get; set; }

    [PXBaseCury(BqlTable = typeof (ARDocumentEnq.ARRegister))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Unreleased Retainage", FieldClass = "Retainage")]
    [PXDBCalced(typeof (IsNull<ARTranPostGL.retainageUnreleasedAmt, decimal0>), typeof (Decimal))]
    public virtual Decimal? RetainageUnreleasedAmt { get; set; }

    [PeriodID(null, null, null, true, BqlField = typeof (ARTranPostGL.tranPeriodID))]
    public virtual string TranPostPeriodID { get; set; }

    /// <summary>
    /// <see cref="T:PX.Objects.GL.FinPeriods.TableDefinition.FinPeriod">Financial Period</see> of the document.
    /// </summary>
    /// <value>
    /// Defaults to the period, to which the <see cref="P:PX.Objects.AR.ARDocumentEnq.ARRegister.DocDate" /> belongs, but can be virtualn by user.
    /// </value>
    [AROpenPeriod(typeof (ARDocumentEnq.GLDocumentPeriodResult.docDate), typeof (ARDocumentEnq.GLDocumentPeriodResult.branchID), null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (ARDocumentEnq.GLDocumentPeriodResult.tranPostPeriodID), IsHeader = true, BqlField = typeof (ARTranPostGL.finPeriodID))]
    public virtual string FinPostPeriodID { get; set; }

    [Account(typeof (ARDocumentEnq.GLDocumentPeriodResult.branchID), IsDBField = false, DisplayName = "Account", BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARDocumentEnq.ARDocumentResult.docType, IBqlString>.IsEqual<ARDocType.prepaymentInvoice>>, ARTranPostGL.accountID>, ARDocumentEnq.ARDocumentResult.aRAccountID>), typeof (int?))]
    public virtual int? AccountID { get; set; }

    [SubAccount(typeof (ARDocumentEnq.GLDocumentPeriodResult.accountID))]
    [PXDBCalced(typeof (Switch<Case<Where<BqlOperand<ARDocumentEnq.ARDocumentResult.docType, IBqlString>.IsEqual<ARDocType.prepaymentInvoice>>, ARTranPostGL.subID>, ARDocumentEnq.ARDocumentResult.aRSubID>), typeof (int?))]
    public virtual int? SubID { get; set; }

    [Project(BqlTable = typeof (ARDocumentEnq.ARDocumentResult))]
    public virtual int? ProjectID { get; set; }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.docType>
    {
      public const int Length = 3;
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.refNbr>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.branchID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.customerID>
    {
    }

    public abstract class docDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.docDate>
    {
    }

    public abstract class docDesc : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.docDesc>
    {
    }

    public abstract class dueDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.dueDate>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.curyID>
    {
    }

    public abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.curyInfoID>
    {
    }

    public abstract class origDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.origDocType>
    {
    }

    public abstract class origRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.origRefNbr>
    {
    }

    public abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.status>
    {
    }

    public abstract class tranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.tranPeriodID>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.finPeriodID>
    {
    }

    public abstract class closedFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.closedFinPeriodID>
    {
    }

    public abstract class closedTranPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.closedTranPeriodID>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.noteID>
    {
    }

    public abstract class openDoc : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.openDoc>
    {
    }

    public abstract class released : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.released>
    {
    }

    public abstract class isRetainageDocument : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.isRetainageDocument>
    {
    }

    public abstract class aRAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.aRAccountID>
    {
    }

    public abstract class aRSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.aRSubID>
    {
    }

    public abstract class isMigratedRecord : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.isMigratedRecord>
    {
    }

    public abstract class installmentCntr : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.installmentCntr>
    {
    }

    public abstract class batchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.batchNbr>
    {
    }

    public abstract class scheduled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.scheduled>
    {
    }

    public abstract class voided : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.voided>
    {
    }

    public abstract class extRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.extRefNbr>
    {
    }

    public abstract class paymentMethodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.paymentMethodID>
    {
    }

    public abstract class curyOrigDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.curyOrigDocAmt>
    {
    }

    public abstract class origDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.origDocAmt>
    {
    }

    public abstract class curyOrigDiscAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.curyOrigDiscAmt>
    {
    }

    public abstract class origDiscAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.origDiscAmt>
    {
    }

    public abstract class curyRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.curyRetainageTotal>
    {
    }

    public abstract class retainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.retainageTotal>
    {
    }

    public abstract class curyOrigDocAmtWithRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.curyOrigDocAmtWithRetainageTotal>
    {
    }

    public abstract class origDocAmtWithRetainageTotal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.origDocAmtWithRetainageTotal>
    {
    }

    public abstract class curyBegBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.curyBegBalance>
    {
    }

    public abstract class begBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.begBalance>
    {
    }

    public abstract class curyDiscActTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.curyDiscActTaken>
    {
    }

    public abstract class discActTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.discActTaken>
    {
    }

    public abstract class curyWOAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.curyWOAmt>
    {
    }

    public abstract class woAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.woAmt>
    {
    }

    public abstract class rGOLAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.rGOLAmt>
    {
    }

    public abstract class aRTurnover : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.aRTurnover>
    {
    }

    public abstract class gLTurnover : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.gLTurnover>
    {
    }

    public abstract class curyDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.curyDocBal>
    {
    }

    public abstract class docBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.docBal>
    {
    }

    public abstract class curyRetainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.curyRetainageUnreleasedAmt>
    {
    }

    public abstract class retainageUnreleasedAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.retainageUnreleasedAmt>
    {
    }

    public abstract class tranPostPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.tranPostPeriodID>
    {
    }

    public abstract class finPostPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.finPostPeriodID>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.accountID>
    {
    }

    public abstract class subID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.subID>
    {
    }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.GLDocumentPeriodResult.projectID>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class GLTran : PX.Objects.GL.GLTran
  {
    [PXBaseCury]
    [PXDBCalced(typeof (Mult<Switch<Case<Where<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<True>, And<PX.Objects.GL.GLTran.tranPeriodID, Equal<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>, decimal1, Case<Where<CurrentValue<ARDocumentEnq.ARDocumentFilter.useMasterCalendar>, Equal<False>, And<PX.Objects.GL.GLTran.finPeriodID, Equal<CurrentValue<ARDocumentEnq.ARDocumentFilter.period>>>>, decimal1>>, decimal0>, Sub<ARDocumentEnq.GLTran.debitAmt, ARDocumentEnq.GLTran.creditAmt>>), typeof (Decimal))]
    public virtual Decimal? GLTurnover { get; set; }

    public new abstract class branchID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARDocumentEnq.GLTran.branchID>
    {
    }

    public new abstract class module : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARDocumentEnq.GLTran.module>
    {
    }

    public new abstract class batchNbr : IBqlField, IBqlOperand
    {
    }

    public new abstract class lineNbr : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARDocumentEnq.GLTran.lineNbr>
    {
    }

    public new abstract class ledgerID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARDocumentEnq.GLTran.ledgerID>
    {
    }

    public new abstract class accountID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARDocumentEnq.GLTran.accountID>
    {
    }

    public new abstract class subID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARDocumentEnq.GLTran.subID>
    {
    }

    public new abstract class creditAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLTran.creditAmt>
    {
    }

    public new abstract class debitAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLTran.debitAmt>
    {
    }

    public new abstract class posted : BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARDocumentEnq.GLTran.posted>
    {
    }

    public new abstract class tranType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDocumentEnq.GLTran.tranType>
    {
    }

    public new abstract class refNbr : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARDocumentEnq.GLTran.refNbr>
    {
    }

    public new abstract class referenceID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDocumentEnq.GLTran.referenceID>
    {
    }

    public abstract class gLTurnover : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDocumentEnq.GLTran.gLTurnover>
    {
    }
  }

  public class Ref
  {
    [PXHidden]
    public class ARInvoice : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
    {
      protected string _InvoiceNbr;
      protected short? _InstallmentCntr;

      /// <summary>
      /// The type of the document.
      /// This field is a part of the compound key of the document.
      /// </summary>
      /// <value>
      /// The field can have one of the values described in <see cref="T:PX.Objects.AR.ARDocType.ListAttribute" />.
      /// </value>
      [PXDBString(3, IsKey = true, IsFixed = true)]
      [PXDefault]
      [ARDocType.List]
      [PXUIField]
      public virtual string DocType { get; set; }

      /// <summary>
      /// The reference number of the document.
      /// This field is a part of the compound key of the document.
      /// </summary>
      [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
      [PXDefault]
      [PXUIField]
      [PXSelector(typeof (Search<ARDocumentEnq.ARRegister.refNbr, Where<ARDocumentEnq.ARRegister.docType, Equal<Optional<ARDocumentEnq.ARRegister.docType>>>>), Filterable = true)]
      public virtual string RefNbr { get; set; }

      /// <summary>
      /// The original reference number or ID assigned by the customer to the customer document.
      /// </summary>
      [PXDBString(40, IsUnicode = true)]
      [PXUIField]
      public virtual string InvoiceNbr
      {
        get => this._InvoiceNbr;
        set => this._InvoiceNbr = value;
      }

      /// <summary>
      /// The counter of <see cref="!:TermsInstallment">installments</see> associated with the document.
      /// </summary>
      [PXDBShort]
      public virtual short? InstallmentCntr
      {
        get => this._InstallmentCntr;
        set => this._InstallmentCntr = value;
      }

      [PXDBInt]
      public virtual int? ProjectID { get; set; }

      public abstract class docType : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        ARDocumentEnq.Ref.ARInvoice.docType>
      {
        public const int Length = 3;
      }

      public abstract class refNbr : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        ARDocumentEnq.Ref.ARInvoice.refNbr>
      {
      }

      public abstract class invoiceNbr : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        ARDocumentEnq.Ref.ARInvoice.invoiceNbr>
      {
      }

      public abstract class installmentCntr : 
        BqlType<
        #nullable enable
        IBqlShort, short>.Field<
        #nullable disable
        ARDocumentEnq.Ref.ARInvoice.installmentCntr>
      {
      }

      public abstract class projectID : 
        BqlType<
        #nullable enable
        IBqlInt, int>.Field<
        #nullable disable
        ARDocumentEnq.Ref.ARInvoice.projectID>
      {
      }
    }

    [PXHidden]
    public class ARPayment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
    {
      protected string _PaymentMethodID;
      protected string _ExtRefNbr;

      /// <summary>
      /// The type of the document.
      /// This field is a part of the compound key of the document.
      /// </summary>
      /// <value>
      /// The field can have one of the values described in <see cref="T:PX.Objects.AR.ARDocType.ListAttribute" />.
      /// </value>
      [PXDBString(3, IsKey = true, IsFixed = true)]
      [PXDefault]
      [ARDocType.List]
      [PXUIField]
      public virtual string DocType { get; set; }

      /// <summary>
      /// The reference number of the document.
      /// This field is a part of the compound key of the document.
      /// </summary>
      [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
      [PXDefault]
      [PXUIField]
      [PXSelector(typeof (Search<ARDocumentEnq.ARRegister.refNbr, Where<ARDocumentEnq.ARRegister.docType, Equal<Optional<ARDocumentEnq.ARRegister.docType>>>>), Filterable = true)]
      public virtual string RefNbr { get; set; }

      [PXDBString(10, IsUnicode = true)]
      [PXSelector(typeof (PX.Objects.CA.PaymentMethod.paymentMethodID), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
      [PXUIField(DisplayName = "Payment Method", Enabled = false)]
      public virtual string PaymentMethodID
      {
        get => this._PaymentMethodID;
        set => this._PaymentMethodID = value;
      }

      [PXDBString(40, IsUnicode = true)]
      [PXUIField]
      public virtual string ExtRefNbr
      {
        get => this._ExtRefNbr;
        set => this._ExtRefNbr = value;
      }

      [PXDBInt]
      public virtual int? ProjectID { get; set; }

      public abstract class docType : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        ARDocumentEnq.Ref.ARPayment.docType>
      {
        public const int Length = 3;
      }

      public abstract class refNbr : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        ARDocumentEnq.Ref.ARPayment.refNbr>
      {
      }

      public abstract class paymentMethodID : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        ARDocumentEnq.Ref.ARPayment.paymentMethodID>
      {
      }

      public abstract class extRefNbr : 
        BqlType<
        #nullable enable
        IBqlString, string>.Field<
        #nullable disable
        ARDocumentEnq.Ref.ARPayment.extRefNbr>
      {
      }

      public abstract class projectID : 
        BqlType<
        #nullable enable
        IBqlInt, int>.Field<
        #nullable disable
        ARDocumentEnq.Ref.ARPayment.projectID>
      {
      }
    }
  }

  private sealed class ARDisplayDocType : ARDocType
  {
    public const string CashReturnInvoice = "RCI";
    public const string CashSaleInvoice = "CSI";

    public new class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[15]
        {
          "INV",
          "DRM",
          "CRM",
          "PMT",
          "RPM",
          "PPM",
          "REF",
          "VRF",
          "FCH",
          "SMB",
          "SMC",
          "CSL",
          "RCS",
          "CSI",
          "RCI"
        }, new string[15]
        {
          "Invoice",
          "Debit Memo",
          "Credit Memo",
          "Payment",
          "Voided Payment",
          "Prepayment",
          "Refund",
          "Voided Refund",
          "Overdue Charge",
          "Balance WO",
          "Credit WO",
          "Cash Sale",
          "Cash Return",
          "Cash Sale Invoice",
          "Cash Return Invoice"
        })
      {
      }
    }
  }

  private sealed class decimalZero : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Constant<
    #nullable disable
    ARDocumentEnq.decimalZero>
  {
    public decimalZero()
      : base(0M)
    {
    }
  }
}
