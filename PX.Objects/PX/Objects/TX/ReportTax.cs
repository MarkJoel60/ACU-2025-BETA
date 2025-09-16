// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.ReportTax
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.TX;

[TableAndChartDashboardType]
public class ReportTax : PXGraph<ReportTax>
{
  public PXFilter<TaxPeriodFilter> Period_Header;
  public PXCancel<TaxPeriodFilter> Cancel;
  public PXSelect<TaxYear> TaxYear_Current;
  public PXSelect<TaxPeriod, Where<TaxPeriod.organizationID, Equal<Current<TaxPeriodFilter.organizationID>>, And<TaxPeriod.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>, And<TaxPeriod.taxPeriodID, Equal<Current<TaxPeriodFilter.taxPeriodID>>>>>> TaxPeriod_Current;
  [Obsolete("The view is obsolete and will be removed in Acumatica 2018R1.")]
  public PXSelect<TaxPeriod, Where<TaxPeriod.organizationID, Equal<Required<TaxPeriodFilter.organizationID>>, And<TaxPeriod.vendorID, Equal<Required<TaxPeriod.vendorID>>, And<TaxPeriod.startDate, LessEqual<Required<TaxPeriod.startDate>>, And<TaxPeriod.endDate, Greater<Required<TaxPeriod.endDate>>>>>>> TaxPeriod_ByDate;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<TaxReportLine, LeftJoin<TaxHistory, On<TaxHistory.vendorID, Equal<TaxReportLine.vendorID>, And<TaxHistory.taxReportRevisionID, Equal<TaxReportLine.taxReportRevisionID>, And<TaxHistory.lineNbr, Equal<TaxReportLine.lineNbr>>>>>, Where<False, Equal<True>>, OrderBy<Asc<TaxReportLine.sortOrder, Asc<TaxReportLine.taxZoneID>>>> Period_Details;
  public PXSelectReadonly2<TaxReportLine, LeftJoin<TaxBucketLine, On<TaxBucketLine.vendorID, Equal<TaxReportLine.vendorID>, And<TaxBucketLine.taxReportRevisionID, Equal<TaxReportLine.taxReportRevisionID>, And<TaxBucketLine.lineNbr, Equal<TaxReportLine.lineNbr>>>>, LeftJoin<PX.Objects.CM.Extensions.Currency, On<PX.Objects.CM.Extensions.Currency.curyID, Equal<Current<PX.Objects.AP.Vendor.curyID>>>, LeftJoin<TaxTranRevReport, On<TaxTranRevReport.taxRevTaxVendorID, Equal<TaxBucketLine.vendorID>, And<TaxTranRevReport.taxRevTaxBucketID, Equal<TaxBucketLine.bucketID>, And<Where<TaxReportLine.taxZoneID, IsNull, And<TaxReportLine.tempLine, Equal<boolFalse>, Or<TaxReportLine.taxZoneID, Equal<TaxTranRevReport.taxZoneID>>>>>>>>>>, Where<TaxReportLine.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>>, OrderBy<Asc<TaxReportLine.vendorID, Asc<TaxReportLine.sortOrder, Asc<TaxReportLine.taxZoneID>>>>> Period_Details_Expanded;
  public PXSetup<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<TaxPeriodFilter.vendorID>>>> Vendor;
  [Obsolete("The view is obsolete and will be removed in Acumatica 8.0.")]
  public PXSelectJoin<TaxTranReport, InnerJoin<Tax, On<Tax.taxID, Equal<TaxTranReport.taxID>>, InnerJoin<TaxReportLine, On<TaxReportLine.vendorID, Equal<Tax.taxVendorID>>, InnerJoin<TaxBucketLine, On<TaxBucketLine.vendorID, Equal<TaxReportLine.vendorID>, And<TaxBucketLine.lineNbr, Equal<TaxReportLine.lineNbr>, And<TaxBucketLine.bucketID, Equal<TaxTranReport.taxBucketID>>>>>>>, Where<Tax.taxVendorID, Equal<Required<TaxPeriodFilter.vendorID>>, And<TaxTranReport.released, Equal<True>, And<TaxTranReport.voided, Equal<False>, And<TaxTranReport.taxPeriodID, IsNull, And<TaxTranReport.taxType, NotEqual<TaxType.pendingPurchase>, And<TaxTranReport.taxType, NotEqual<TaxType.pendingSales>, And<TaxTranReport.origRefNbr, Equal<Empty>>>>>>>>, OrderBy<Asc<TaxTranReport.tranDate>>> OldestNotReportedTaxTran;
  public PXSelect<TaxHistory, Where<TaxHistory.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>, And<TaxHistory.taxPeriodID, Equal<Current<TaxPeriodFilter.taxPeriodID>>>>, OrderBy<Desc<TaxHistory.revisionID>>> History_Last;
  public PXSetup<Company> company;
  protected PX.Objects.TX.TaxCalendar<TaxYear, TaxPeriod> TaxCalendar;
  public PXAction<TaxPeriodFilter> fileTax;
  public PXAction<TaxPeriodFilter> viewTaxPeriods;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public static int?[] GetBranchesForProcessing(
    PXGraph graph,
    int? organizationID,
    int? branchID,
    int? taxAgencyID,
    string taxPeriodID = null)
  {
    if (branchID.HasValue)
      return branchID.SingleToArray<int?>();
    IEnumerable<int?> nullables = (IEnumerable<int?>) null;
    using (new PXReadBranchRestrictedScope(organizationID.SingleToArrayOrNull<int?>(), (int?[]) null, true, false))
    {
      nullables = (IEnumerable<int?>) ((IQueryable<PXResult<TaxHistory>>) PXSelectBase<TaxHistory, PXSelectJoinGroupBy<TaxHistory, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<TaxHistory.branchID>, And<PX.Objects.GL.Branch.active, Equal<True>>>>, Where<TaxHistory.vendorID, Equal<Required<TaxHistory.vendorID>>, And<Current<TaxPeriodFilter.organizationID>, IsNotNull>>, Aggregate<GroupBy<TaxHistory.branchID>>>.Config>.Select(graph, new object[1]
      {
        (object) taxAgencyID
      })).Select<PXResult<TaxHistory>, int?>((Expression<Func<PXResult<TaxHistory>, int?>>) (t => ((TaxHistory) t).BranchID));
      nullables = nullables.Union<int?>((IEnumerable<int?>) ((IQueryable<PXResult<TaxTran>>) PXSelectBase<TaxTran, PXSelectGroupBy<TaxTran, Where<TaxTran.vendorID, Equal<Required<TaxPeriodFilter.vendorID>>, And<Current<TaxPeriodFilter.organizationID>, IsNotNull, And<TaxTran.released, Equal<True>, And<TaxTran.voided, Equal<False>, And<TaxTran.taxPeriodID, IsNull, And<TaxTran.taxType, NotEqual<TaxType.pendingPurchase>, And<TaxTran.taxType, NotEqual<TaxType.pendingSales>, And<TaxTran.origRefNbr, Equal<Empty>>>>>>>>>, Aggregate<GroupBy<TaxTran.branchID>>>.Config>.Select(graph, new object[1]
      {
        (object) taxAgencyID
      })).Select<PXResult<TaxTran>, int?>((Expression<Func<PXResult<TaxTran>, int?>>) (t => ((TaxTran) t).BranchID)));
    }
    return nullables.ToArray<int?>();
  }

  public static TaxTran GetEarliestNotReportedTaxTran(
    PXGraph graph,
    PX.Objects.AP.Vendor taxAgency,
    int? organizationID,
    int? branchID)
  {
    return ReportTax.GetEarliestNotReportedTaxTran(graph, taxAgency, organizationID, branchID, (string) null);
  }

  public static TaxTran GetEarliestNotReportedTaxTran(
    PXGraph graph,
    PX.Objects.AP.Vendor taxAgency,
    int? organizationID,
    int? branchID,
    string taxPeriodID)
  {
    int?[] branchesForProcessing = ReportTax.GetBranchesForProcessing(graph, organizationID, branchID, taxAgency.BAccountID, taxPeriodID);
    return ReportTax.GetEarliestNotReportedTaxTran(graph, taxAgency, organizationID, branchesForProcessing, taxPeriodID);
  }

  private static TaxTran GetEarliestNotReportedTaxTran(
    PXGraph graph,
    PX.Objects.AP.Vendor taxAgency,
    int? organizationID,
    int?[] branchIDs,
    string taxPeriodID)
  {
    return !taxAgency.TaxReportFinPeriod.GetValueOrDefault() ? ReportTax.GetEarliestNotReportedTaxTranByField<TaxTran.tranDate>(graph, taxAgency.BAccountID, organizationID, branchIDs, taxPeriodID) : ReportTax.GetEarliestNotReportedTaxTranByField<TaxTran.finDate>(graph, taxAgency.BAccountID, organizationID, branchIDs, taxPeriodID);
  }

  public static TaxTran GetEarliestNotReportedTaxTranByField<TOrderByField>(
    PXGraph graph,
    int? taxAgencyID,
    int? organizationID,
    int? branchID)
    where TOrderByField : IBqlField
  {
    int?[] branchesForProcessing = ReportTax.GetBranchesForProcessing(graph, organizationID, branchID, taxAgencyID);
    return ReportTax.GetEarliestNotReportedTaxTranByField<TOrderByField>(graph, taxAgencyID, organizationID, branchesForProcessing);
  }

  public static TaxTran GetEarliestNotReportedTaxTranByField<TOrderByField>(
    PXGraph graph,
    int? taxAgencyID,
    int? organizationID,
    int?[] branchIDs,
    string taxPeriodID = null)
    where TOrderByField : IBqlField
  {
    using (new PXReadBranchRestrictedScope(organizationID.SingleToArray<int?>(), branchIDs, true, true))
      return PXResultset<TaxTran>.op_Implicit(PXSelectBase<TaxTran, PXSelectJoin<TaxTran, InnerJoin<Tax, On<Tax.taxID, Equal<TaxTran.taxID>>>, Where<Tax.taxVendorID, Equal<Required<TaxPeriodFilter.vendorID>>, And<Current<TaxPeriodFilter.organizationID>, IsNotNull, And<TaxTran.released, Equal<True>, And<TaxTran.voided, Equal<False>, And<TaxTran.taxPeriodID, IsNull, And<TaxTran.taxType, NotEqual<TaxType.pendingPurchase>, And<TaxTran.taxType, NotEqual<TaxType.pendingSales>, And<TaxTran.origRefNbr, Equal<Empty>, And<Exists<Select2<TaxReportLine, InnerJoin<TaxBucketLine, On<TaxBucketLine.vendorID, Equal<Required<TaxPeriodFilter.vendorID>>, And<TaxBucketLine.lineNbr, Equal<TaxReportLine.lineNbr>, And<TaxBucketLine.lineNbr, Equal<TaxReportLine.lineNbr>>>>>, Where<TaxBucketLine.bucketID, Equal<TaxTran.taxBucketID>>>>>>>>>>>>>, OrderBy<Asc<TOrderByField>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
      {
        (object) taxAgencyID,
        (object) taxAgencyID
      }));
  }

  protected virtual void TaxPeriodFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    TaxPeriodFilter row = (TaxPeriodFilter) e.Row;
    if (row == null)
      return;
    row.StartDate = new DateTime?();
    row.EndDate = new DateTime?();
    ((PXAction) this.fileTax).SetEnabled(false);
    sender.RaiseExceptionHandling<TaxPeriodFilter.taxPeriodID>(e.Row, (object) row.TaxPeriodID, (Exception) null);
    sender.RaiseExceptionHandling<TaxPeriodFilter.vendorID>(e.Row, (object) row.VendorID, (Exception) null);
    PXUIFieldAttribute.SetEnabled<TaxPeriodFilter.taxPeriodID>(sender, (object) null, ((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor).Current != null && ((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor).Current.UpdClosedTaxPeriods.GetValueOrDefault());
    TaxPeriod taxper = ((PXSelectBase<TaxPeriod>) this.TaxPeriod_Current).SelectSingle(Array.Empty<object>());
    if (taxper == null || taxper.TaxPeriodID == null)
      return;
    PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor).SelectWindowed(0, 1, new object[1]
    {
      (object) taxper.VendorID
    }));
    if (vendor == null)
      return;
    ((PXAction) this.fileTax).SetEnabled(this.CheckProcessAllowable(row, taxper, vendor));
    this.CheckGLSychronizedPeriods(sender, row, vendor);
  }

  protected virtual void CheckGLSychronizedPeriods(
    PXCache sender,
    TaxPeriodFilter filter,
    PX.Objects.AP.Vendor vendor)
  {
    if (!string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<TaxPeriodFilter.taxPeriodID>(sender, (object) filter)) || this.IsSynchronizedWithFinPeriods(filter))
      return;
    sender.RaiseExceptionHandling<TaxPeriodFilter.taxPeriodID>((object) filter, (object) filter.TaxPeriodID, (Exception) new PXSetPropertyException("The tax report cannot be prepared and released, because the {0} tax period does not match the financial period in the general ledger for the {1} company. Use the {2} action on the Tax Periods (TX207000) form to correct the period structure for the {3} tax year.", (PXErrorLevel) 4, new object[4]
    {
      (object) FinPeriodIDFormattingAttribute.FormatForError(filter.TaxPeriodID),
      (object) PXAccess.GetOrganizationCD(filter.OrganizationID),
      (object) "Synchronize Periods with GL",
      (object) filter.TaxPeriodID.Substring(0, 4)
    }));
  }

  protected virtual bool CheckProcessAllowable(
    TaxPeriodFilter filter,
    TaxPeriod taxper,
    PX.Objects.AP.Vendor vendor)
  {
    PXCache cache = ((PXSelectBase) this.Period_Header).Cache;
    int? nullable1;
    int num1;
    if (filter != null)
    {
      nullable1 = taxper.VendorID;
      num1 = nullable1.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    bool allowProcess = num1 != 0;
    filter.StartDate = taxper.StartDateUI;
    TaxPeriodFilter taxPeriodFilter = filter;
    DateTime? nullable2 = taxper.EndDate;
    DateTime? nullable3 = new DateTime?(nullable2.Value.AddDays(-1.0));
    taxPeriodFilter.EndDate = nullable3;
    int?[] branchesForProcessing = ReportTax.GetBranchesForProcessing((PXGraph) this, filter.OrganizationID, filter.BranchID, filter.VendorID, filter.TaxPeriodID);
    if (allowProcess && taxper.Status == "P")
    {
      int? nullable4 = ReportTaxProcess.CurrentRevisionId(cache.Graph, filter.OrganizationID, branchesForProcessing, filter.VendorID, filter.TaxPeriodID);
      using (new PXReadBranchRestrictedScope(filter.OrganizationID.SingleToArray<int?>(), filter.BranchID.SingleToArrayOrNull<int?>(), false, false))
      {
        TaxHistory taxHistory = PXResultset<TaxHistory>.op_Implicit(((PXSelectBase<TaxHistory>) this.History_Last).SelectWindowed(0, 1, new object[2]
        {
          (object) filter.OrganizationID,
          (object) filter.BranchID
        }));
        int num2;
        if (taxHistory == null)
        {
          num2 = !nullable4.HasValue ? 1 : 0;
        }
        else
        {
          nullable1 = taxHistory.RevisionID;
          int? nullable5 = nullable4;
          num2 = nullable1.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable1.HasValue == nullable5.HasValue ? 1 : 0;
        }
        if (num2 != 0)
          allowProcess = false;
      }
    }
    string valueExt = (string) ((PXSelectBase<TaxPeriodFilter>) this.Period_Header).GetValueExt<TaxPeriodFilter.taxPeriodID>(filter);
    TaxPeriod preparedPeriod = TaxYearMaint.FindPreparedPeriod((PXGraph) this, filter.OrganizationID, filter.VendorID);
    if (preparedPeriod != null && preparedPeriod.TaxPeriodID != taxper.TaxPeriodID)
    {
      cache.RaiseExceptionHandling<TaxPeriodFilter.taxPeriodID>((object) filter, (object) valueExt, (Exception) new PXSetPropertyException<TaxPeriodFilter.taxPeriodID>("A tax report cannot be prepared for the selected company and tax agency because there is an unreleased report for the {0} tax period. To proceed, release or void the tax report for {0} on the Release Tax Report (TX502000) form.", new object[2]
      {
        (object) FinPeriodIDFormattingAttribute.FormatForError(preparedPeriod.TaxPeriodID),
        (object) (PXErrorLevel) 4
      }));
      allowProcess = false;
    }
    if (allowProcess && !filter.TaxReportRevisionID.HasValue)
    {
      cache.RaiseExceptionHandling<TaxPeriodFilter.taxPeriodID>((object) filter, (object) valueExt, (Exception) new PXSetPropertyException("A tax report cannot be prepared for the selected company and tax agency because the report version for the {0} tax period has not been found in the system.", (PXErrorLevel) 2, new object[1]
      {
        (object) FinPeriodIDFormattingAttribute.FormatForError(filter.TaxPeriodID)
      }));
      allowProcess = false;
    }
    if (allowProcess)
    {
      if (taxper.Status != "D")
      {
        int? organizationId = filter.OrganizationID;
        int? branchId = filter.BranchID;
        PX.Objects.AP.Vendor vendor1 = vendor;
        DateTime? endDate;
        if (taxper == null)
        {
          nullable2 = new DateTime?();
          endDate = nullable2;
        }
        else
          endDate = taxper.EndDate;
        if (ReportTaxProcess.CheckForUnprocessedSVAT((PXGraph) this, organizationId, branchId, vendor1, endDate))
          cache.RaiseExceptionHandling<TaxPeriodFilter.vendorID>((object) filter, (object) filter.VendorID, (Exception) new PXSetPropertyException("In the tax period, there are payment applications for which pending VAT amount has not been recognized yet. To recognize pending VAT, use the Recognize Output VAT (TX503000) form and the Recognize Input VAT (TX503500) form.", (PXErrorLevel) 2));
      }
      switch (taxper.Status)
      {
        case "D":
          allowProcess = this.CheckProcessAllowableOnDummyStatus(filter, taxper, vendor, cache, allowProcess, branchesForProcessing, valueExt);
          break;
        case "C":
          allowProcess = this.CheckProcessAllowableOnClosedStatus(filter, taxper, vendor, cache, allowProcess, branchesForProcessing, valueExt);
          break;
        case "P":
          allowProcess = this.CheckProcessAllowableOnPreparedStatus(filter, taxper, vendor, cache, allowProcess, branchesForProcessing, valueExt);
          break;
        default:
          allowProcess = this.CheckProcessAllowableOnOpenStatus(filter, taxper, vendor, cache, allowProcess, branchesForProcessing, valueExt);
          break;
      }
    }
    return allowProcess;
  }

  protected virtual bool CheckProcessAllowableOnDummyStatus(
    TaxPeriodFilter filter,
    TaxPeriod taxPeriod,
    PX.Objects.AP.Vendor vendor,
    PXCache filterCache,
    bool allowProcess,
    int?[] _branches,
    string taxPeriodIDForMessage)
  {
    filterCache.RaiseExceptionHandling<TaxPeriodFilter.vendorID>((object) filter, (object) taxPeriod.VendorID, (Exception) new PXSetPropertyException<TaxPeriodFilter.vendorID>("Tax Agency has no tax transactions.", (PXErrorLevel) 2));
    return allowProcess;
  }

  protected virtual bool CheckProcessAllowableOnOpenStatus(
    TaxPeriodFilter filter,
    TaxPeriod taxPeriod,
    PX.Objects.AP.Vendor vendor,
    PXCache filterCache,
    bool allowProcess,
    int?[] _branches,
    string taxPeriodIDForMessage)
  {
    TaxTran notReportedTaxTran = ReportTax.GetEarliestNotReportedTaxTran((PXGraph) this, vendor, filter.OrganizationID, _branches, filter.TaxPeriodID);
    if (notReportedTaxTran == null)
      return allowProcess;
    bool? nullable1;
    DateTime? nullable2;
    DateTime? nullable3;
    if (PXResultset<TaxPeriod>.op_Implicit(PXSelectBase<TaxPeriod, PXSelect<TaxPeriod, Where<TaxPeriod.organizationID, Equal<Required<TaxPeriod.organizationID>>, And<TaxPeriod.vendorID, Equal<Required<TaxPeriod.vendorID>>, And<TaxPeriod.taxPeriodID, Less<Required<TaxPeriod.taxPeriodID>>, And<TaxPeriod.status, Equal<TaxPeriodStatus.open>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
    {
      (object) filter.OrganizationID,
      (object) filter.VendorID,
      (object) filter.TaxPeriodID
    })) != null)
    {
      nullable1 = vendor.UpdClosedTaxPeriods;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = vendor.TaxReportFinPeriod;
        if (!nullable1.GetValueOrDefault())
        {
          nullable2 = taxPeriod.StartDate;
          nullable3 = notReportedTaxTran.TranDate;
          if ((nullable2.HasValue & nullable3.HasValue ? (nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            goto label_8;
        }
        nullable1 = vendor.TaxReportFinPeriod;
        if (nullable1.GetValueOrDefault())
        {
          nullable3 = taxPeriod.StartDate;
          nullable2 = notReportedTaxTran.FinDate;
          if ((nullable3.HasValue & nullable2.HasValue ? (nullable3.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
            goto label_9;
        }
        else
          goto label_9;
label_8:
        PXSetPropertyException<TaxPeriodFilter.taxPeriodID> propertyException = new PXSetPropertyException<TaxPeriodFilter.taxPeriodID>("Cannot prepare tax report for open period when previous period isn't closed.", (PXErrorLevel) 4);
        filterCache.RaiseExceptionHandling<TaxPeriodFilter.taxPeriodID>((object) filter, (object) taxPeriodIDForMessage, (Exception) propertyException);
        allowProcess = false;
      }
    }
label_9:
    if (allowProcess)
    {
      nullable2 = notReportedTaxTran.TranDate;
      if (nullable2.HasValue)
      {
        nullable1 = vendor.TaxReportFinPeriod;
        if (!nullable1.GetValueOrDefault())
        {
          nullable2 = taxPeriod.StartDate;
          nullable3 = notReportedTaxTran.TranDate;
          if ((nullable2.HasValue & nullable3.HasValue ? (nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            goto label_15;
        }
        nullable1 = vendor.TaxReportFinPeriod;
        if (nullable1.GetValueOrDefault())
        {
          nullable3 = taxPeriod.StartDate;
          nullable2 = notReportedTaxTran.FinDate;
          if ((nullable3.HasValue & nullable2.HasValue ? (nullable3.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
            goto label_16;
        }
        else
          goto label_16;
label_15:
        PXSetPropertyException<TaxPeriodFilter.taxPeriodID> propertyException = new PXSetPropertyException<TaxPeriodFilter.taxPeriodID>("One or more tax transactions from the previous periods will be reported into the current period.", (PXErrorLevel) 2);
        ((PXSelectBase) this.Period_Header).Cache.RaiseExceptionHandling<TaxPeriodFilter.taxPeriodID>((object) filter, (object) taxPeriodIDForMessage, (Exception) propertyException);
      }
    }
label_16:
    return allowProcess;
  }

  protected virtual bool CheckProcessAllowableOnPreparedStatus(
    TaxPeriodFilter filter,
    TaxPeriod taxPeriod,
    PX.Objects.AP.Vendor vendor,
    PXCache filterCache,
    bool allowProcess,
    int?[] _branches,
    string taxPeriodIDForMessage)
  {
    TaxTran notReportedTaxTran = ReportTax.GetEarliestNotReportedTaxTran((PXGraph) this, vendor, filter.OrganizationID, _branches, filter.TaxPeriodID);
    if (notReportedTaxTran != null)
    {
      DateTime? nullable1 = notReportedTaxTran.TranDate;
      if (nullable1.HasValue)
      {
        bool? taxReportFinPeriod = vendor.TaxReportFinPeriod;
        DateTime? nullable2;
        if (!taxReportFinPeriod.GetValueOrDefault())
        {
          nullable1 = taxPeriod.StartDate;
          nullable2 = notReportedTaxTran.TranDate;
          if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            goto label_6;
        }
        taxReportFinPeriod = vendor.TaxReportFinPeriod;
        if (taxReportFinPeriod.GetValueOrDefault())
        {
          nullable2 = taxPeriod.StartDate;
          nullable1 = notReportedTaxTran.FinDate;
          if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
            goto label_7;
        }
        else
          goto label_7;
label_6:
        PXSetPropertyException<TaxPeriodFilter.taxPeriodID> propertyException = new PXSetPropertyException<TaxPeriodFilter.taxPeriodID>("One or more tax transactions from the previous periods will be reported into the current period.", (PXErrorLevel) 2);
        ((PXSelectBase) this.Period_Header).Cache.RaiseExceptionHandling<TaxPeriodFilter.taxPeriodID>((object) filter, (object) taxPeriodIDForMessage, (Exception) propertyException);
      }
    }
label_7:
    return allowProcess;
  }

  protected virtual bool CheckProcessAllowableOnClosedStatus(
    TaxPeriodFilter filter,
    TaxPeriod taxPeriod,
    PX.Objects.AP.Vendor vendor,
    PXCache filterCache,
    bool allowProcess,
    int?[] _branches,
    string taxPeriodIDForMessage)
  {
    TaxTran notReportedTaxTran = ReportTax.GetEarliestNotReportedTaxTran((PXGraph) this, vendor, filter.OrganizationID, _branches, filter.TaxPeriodID);
    DateTime? nullable1;
    bool? taxReportFinPeriod;
    DateTime? nullable2;
    if (notReportedTaxTran != null)
    {
      nullable1 = notReportedTaxTran.TranDate;
      if (nullable1.HasValue)
      {
        taxReportFinPeriod = vendor.TaxReportFinPeriod;
        if (!taxReportFinPeriod.GetValueOrDefault())
        {
          nullable1 = taxPeriod.StartDate;
          nullable2 = notReportedTaxTran.TranDate;
          if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            goto label_6;
        }
        taxReportFinPeriod = vendor.TaxReportFinPeriod;
        if (taxReportFinPeriod.GetValueOrDefault())
        {
          nullable2 = taxPeriod.StartDate;
          nullable1 = notReportedTaxTran.FinDate;
          if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
            goto label_7;
        }
        else
          goto label_7;
label_6:
        filterCache.RaiseExceptionHandling<TaxPeriodFilter.taxPeriodID>((object) filter, (object) taxPeriodIDForMessage, (Exception) new PXSetPropertyException<TaxPeriodFilter.taxPeriodID>("One or more tax transactions from the previous periods will be reported into the current period.", (PXErrorLevel) 2));
      }
    }
label_7:
    if (notReportedTaxTran != null)
    {
      nullable1 = notReportedTaxTran.TranDate;
      if (nullable1.HasValue)
      {
        taxReportFinPeriod = vendor.TaxReportFinPeriod;
        if (!taxReportFinPeriod.GetValueOrDefault())
        {
          nullable1 = taxPeriod.EndDate;
          nullable2 = notReportedTaxTran.TranDate;
          if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            goto label_13;
        }
        taxReportFinPeriod = vendor.TaxReportFinPeriod;
        if (taxReportFinPeriod.GetValueOrDefault())
        {
          nullable2 = taxPeriod.EndDate;
          nullable1 = notReportedTaxTran.FinDate;
          if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
            goto label_14;
        }
        else
          goto label_14;
      }
    }
label_13:
    PXSetPropertyException<TaxPeriodFilter.taxPeriodID> propertyException = new PXSetPropertyException<TaxPeriodFilter.taxPeriodID>("There is no transactions to adjust in the selected reporting period.", (PXErrorLevel) 2);
    filterCache.RaiseExceptionHandling<TaxPeriodFilter.taxPeriodID>((object) filter, (object) taxPeriodIDForMessage, (Exception) propertyException);
    allowProcess = false;
label_14:
    return allowProcess;
  }

  protected virtual bool IsSynchronizedWithFinPeriods(TaxPeriodFilter filter)
  {
    TaxYear taxYear = PXResultset<TaxYear>.op_Implicit(PXSelectBase<TaxYear, PXViewOf<TaxYear>.BasedOn<SelectFromBase<TaxYear, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<TaxPeriod>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxYear.vendorID, Equal<TaxPeriod.vendorID>>>>, And<BqlOperand<TaxYear.year, IBqlString>.IsEqual<TaxPeriod.taxYear>>>>.And<BqlOperand<TaxYear.organizationID, IBqlInt>.IsEqual<TaxPeriod.organizationID>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<TaxPeriod.vendorID, Equal<P.AsInt>>>>, And<BqlOperand<TaxPeriod.taxPeriodID, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<TaxPeriod.organizationID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) filter.VendorID,
      (object) filter.TaxPeriodID,
      (object) filter.OrganizationID
    }));
    if (taxYear == null || taxYear.TaxPeriodType != "F")
      return true;
    DateTime dateTime1 = this.FinPeriodRepository.PeriodStartDate(filter.TaxPeriodID, filter.OrganizationID);
    DateTime dateTime2 = this.FinPeriodRepository.PeriodEndDate(filter.TaxPeriodID, filter.OrganizationID);
    DateTime? startDate = filter.StartDate;
    DateTime? nullable = filter.EndDate;
    if ((startDate.HasValue == nullable.HasValue ? (startDate.HasValue ? (startDate.GetValueOrDefault() == nullable.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      dateTime1 = dateTime1.AddDays(-1.0);
    nullable = filter.StartDate;
    DateTime dateTime3 = dateTime1;
    if ((nullable.HasValue ? (nullable.GetValueOrDefault() == dateTime3 ? 1 : 0) : 0) == 0)
      return false;
    nullable = filter.EndDate;
    DateTime dateTime4 = dateTime2;
    return nullable.HasValue && nullable.GetValueOrDefault() == dateTime4;
  }

  protected virtual void TaxPeriodFilter_VendorID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetValue<TaxPeriodFilter.taxPeriodID>(e.Row, (object) null);
  }

  protected virtual void TaxPeriodFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    TaxPeriodFilter row = (TaxPeriodFilter) e.Row;
    if (row == null)
      return;
    PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID((PXGraph) this, row.OrganizationID);
    int? nullable1;
    if (row.OrganizationID.HasValue)
    {
      nullable1 = row.VendorID;
      if (nullable1.HasValue)
        goto label_4;
    }
    row.TaxPeriodID = (string) null;
    row.EndDate = new DateTime?();
    row.StartDate = new DateTime?();
label_4:
    nullable1 = row.OrganizationID;
    bool? nullable2;
    if (nullable1.HasValue)
    {
      nullable1 = row.VendorID;
      if (nullable1.HasValue)
      {
        nullable1 = row.BranchID;
        if (!nullable1.HasValue && organizationById != null)
        {
          nullable2 = organizationById.FileTaxesByBranches;
          if (nullable2.GetValueOrDefault())
            goto label_8;
        }
        TaxPeriod taxPeriod = ((PXSelectBase<TaxPeriod>) this.TaxPeriod_Current).SelectSingle(Array.Empty<object>());
        if (!sender.ObjectsEqual<TaxPeriodFilter.organizationID, TaxPeriodFilter.branchID, TaxPeriodFilter.vendorID>(e.Row, e.OldRow) || row.TaxPeriodID == null)
        {
          foreach (PXView pxView in ((IEnumerable<KeyValuePair<string, PXView>>) ((PXGraph) this).Views).Select<KeyValuePair<string, PXView>, PXView>((Func<KeyValuePair<string, PXView>, PXView>) (view => view.Value)).ToList<PXView>())
            pxView.Clear();
          ((PXGraph) this).Caches[typeof (TaxPeriod)].Clear();
          ((PXGraph) this).Caches[typeof (TaxPeriod)].ClearQueryCacheObsolete();
          ((PXGraph) this).Caches[typeof (TaxYear)].Clear();
          ((PXSelectBase) this.History_Last).View.Clear();
          taxPeriod = this.TaxCalendar.GetOrCreateCurrentTaxPeriod(((PXSelectBase) this.TaxYear_Current).Cache, ((PXSelectBase) this.TaxPeriod_Current).Cache, row.OrganizationID, row.VendorID);
          row.TaxPeriodID = taxPeriod?.TaxPeriodID ?? row.TaxPeriodID;
        }
        sender.SetValue<TaxPeriodFilter.taxReportRevisionID>(e.Row, (object) (int?) TaxReportMaint.GetTaxReportVersionByDate((PXGraph) this, row.VendorID, new DateTime?(taxPeriod.EndDate.Value.AddDays(-1.0)))?.RevisionID);
        return;
      }
    }
label_8:
    TaxPeriodFilter taxPeriodFilter1 = row;
    nullable1 = new int?();
    int? nullable3 = nullable1;
    taxPeriodFilter1.RevisionId = nullable3;
    TaxPeriodFilter taxPeriodFilter2 = row;
    nullable2 = new bool?();
    bool? nullable4 = nullable2;
    taxPeriodFilter2.ShowDifference = nullable4;
    row.PreparedWarningMsg = (string) null;
  }

  protected virtual void TaxPeriodFilter_TaxPeriodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual bool IsDirty => false;

  protected virtual IEnumerable period_Details()
  {
    TaxReportLine taxReportLine = (TaxReportLine) null;
    TaxHistory taxHistory1 = (TaxHistory) null;
    PXResultset<TaxReportLine, TaxHistory> records = new PXResultset<TaxReportLine, TaxHistory>();
    ((PXSelectBase<TaxPeriod>) this.TaxPeriod_Current).Current = ((PXSelectBase<TaxPeriod>) this.TaxPeriod_Current).SelectSingle(Array.Empty<object>());
    TaxPeriodFilter filter = ((PXSelectBase<TaxPeriodFilter>) this.Period_Header).Current;
    if (filter.TaxPeriodID == null)
      return (IEnumerable) records;
    PX.Objects.AP.Vendor current = ((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor).Current;
    using (new PXFieldScope(((PXSelectBase) this.Period_Details_Expanded).View, new System.Type[38]
    {
      typeof (TaxTranRevReport.branchID),
      typeof (TaxTranRevReport.curyID),
      typeof (TaxTranRevReport.tranDate),
      typeof (TaxTranRevReport.finPeriodID),
      typeof (TaxTranRevReport.module),
      typeof (TaxTranRevReport.recordID),
      typeof (TaxTranRevReport.tranType),
      typeof (TaxTranRevReport.taxType),
      typeof (TaxTranRevReport.taxID),
      typeof (TaxTranRevReport.taxBucketID),
      typeof (TaxTranRevReport.taxZoneID),
      typeof (TaxTranRevReport.refNbr),
      typeof (TaxTranRevReport.taxAmt),
      typeof (TaxTranRevReport.curyTaxAmt),
      typeof (TaxTranRevReport.taxableAmt),
      typeof (TaxTranRevReport.curyTaxableAmt),
      typeof (TaxTranRevReport.exemptedAmt),
      typeof (TaxTranRevReport.curyExemptedAmt),
      typeof (TaxReportLine.vendorID),
      typeof (TaxReportLine.lineNbr),
      typeof (TaxReportLine.lineType),
      typeof (TaxReportLine.lineMult),
      typeof (TaxReportLine.taxZoneID),
      typeof (TaxReportLine.netTax),
      typeof (TaxReportLine.tempLine),
      typeof (TaxReportLine.tempLineNbr),
      typeof (TaxReportLine.descr),
      typeof (TaxReportLine.reportLineNbr),
      typeof (TaxReportLine.bucketSum),
      typeof (TaxReportLine.hideReportLine),
      typeof (TaxReportLine.sortOrder),
      typeof (TaxReportLine.taxReportRevisionID),
      typeof (PX.Objects.CM.Extensions.Currency.curyID),
      typeof (PX.Objects.CM.Extensions.Currency.decimalPlaces),
      typeof (TaxBucketLine.vendorID),
      typeof (TaxBucketLine.bucketID),
      typeof (TaxBucketLine.taxReportRevisionID),
      typeof (TaxBucketLine.lineNbr)
    }))
    {
      using (new PXReadBranchRestrictedScope(filter.OrganizationID.SingleToArray<int?>(), filter.BranchID.SingleToArrayOrNull<int?>(), true, false))
      {
        IEnumerable<PXResult<TaxReportLine>> pxResults = ((IEnumerable<PXResult<TaxReportLine>>) ((PXSelectBase<TaxReportLine>) this.Period_Details_Expanded).Select(Array.Empty<object>())).AsEnumerable<PXResult<TaxReportLine>>().Where<PXResult<TaxReportLine>>((Func<PXResult<TaxReportLine>, bool>) (row =>
        {
          int? reportRevisionId1 = PXResult<TaxReportLine>.op_Implicit(row).TaxReportRevisionID;
          int? reportRevisionId2 = filter.TaxReportRevisionID;
          return reportRevisionId1.GetValueOrDefault() == reportRevisionId2.GetValueOrDefault() & reportRevisionId1.HasValue == reportRevisionId2.HasValue && this.ShowTaxReportLine((PXResult<TaxReportLine, TaxBucketLine, PX.Objects.CM.Extensions.Currency, TaxTranRevReport>) row, ((PXSelectBase<TaxPeriodFilter>) this.Period_Header).Current.TaxPeriodID);
        }));
        IEnumerable<TaxTranRevReport> source1 = GraphHelper.RowCast<TaxTranRevReport>((IEnumerable) pxResults);
        IOrderedEnumerable<DateTime> source2 = source1.Where<TaxTranRevReport>((Func<TaxTranRevReport, bool>) (t => t.TranDate.HasValue)).Select<TaxTranRevReport, DateTime>((Func<TaxTranRevReport, DateTime>) (t => t.TranDate.Value)).OrderBy<DateTime, DateTime>((Func<DateTime, DateTime>) (t => t));
        CurrencyRatesProvider currencyRatesProvider1 = (CurrencyRatesProvider) null;
        bool flag = ((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor).Current.CuryRateTypeID != null && ((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor).Current.CuryID != null;
        if (flag)
        {
          currencyRatesProvider1 = new CurrencyRatesProvider(((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor).Current.CuryRateTypeID, ((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor).Current.CuryID);
          currencyRatesProvider1.Fill((PXGraph) this, source1.Select<TaxTranRevReport, string>((Func<TaxTranRevReport, string>) (t => t.CuryID)).Where<string>((Func<string, bool>) (v => v != null)).Distinct<string>(), source2.FirstOrDefault<DateTime>(), source2.LastOrDefault<DateTime>());
        }
        foreach (PXResult<TaxReportLine, TaxBucketLine, PX.Objects.CM.Extensions.Currency, TaxTranRevReport> pxResult in pxResults)
        {
          TaxReportLine line = PXResult<TaxReportLine, TaxBucketLine, PX.Objects.CM.Extensions.Currency, TaxTranRevReport>.op_Implicit(pxResult);
          TaxTranRevReport tran = PXResult<TaxReportLine, TaxBucketLine, PX.Objects.CM.Extensions.Currency, TaxTranRevReport>.op_Implicit(pxResult);
          PX.Objects.CM.Extensions.Currency currency = PXResult<TaxReportLine, TaxBucketLine, PX.Objects.CM.Extensions.Currency, TaxTranRevReport>.op_Implicit(pxResult);
          DateTime? tranDate = tran.TranDate;
          PX.Objects.CM.CurrencyRate currencyRate1;
          if (!tranDate.HasValue || !flag)
          {
            currencyRate1 = (PX.Objects.CM.CurrencyRate) null;
          }
          else
          {
            CurrencyRatesProvider currencyRatesProvider2 = currencyRatesProvider1;
            string curyId = tran.CuryID;
            tranDate = tran.TranDate;
            DateTime date = tranDate.Value;
            currencyRate1 = currencyRatesProvider2.GetRate(curyId, date);
          }
          PX.Objects.CM.CurrencyRate currencyRate2 = currencyRate1;
          int? nullable1 = (int?) taxReportLine?.VendorID;
          int? nullable2 = (int?) line?.VendorID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            nullable2 = (int?) taxReportLine?.LineNbr;
            nullable1 = (int?) line?.LineNbr;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
            {
              nullable1 = (int?) taxReportLine?.TaxReportRevisionID;
              nullable2 = (int?) line?.TaxReportRevisionID;
              if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue && taxHistory1 != null)
                goto label_17;
            }
          }
          if (taxHistory1 != null)
            ((PXResultset<TaxReportLine>) records).Add((PXResult<TaxReportLine>) new PXResult<TaxReportLine, TaxHistory>(taxReportLine, taxHistory1));
          TaxHistory taxHistory2 = new TaxHistory();
          nullable2 = new int?();
          taxHistory2.BranchID = nullable2;
          taxHistory2.VendorID = line.VendorID;
          taxHistory2.LineNbr = line.LineNbr;
          taxHistory2.TaxPeriodID = filter.TaxPeriodID;
          taxHistory2.UnfiledAmt = new Decimal?(0M);
          taxHistory2.ReportUnfiledAmt = new Decimal?(0M);
          taxHistory2.TaxReportRevisionID = line.TaxReportRevisionID;
          taxHistory1 = taxHistory2;
label_17:
          if (tran.RefNbr != null && tran.TaxType != "A" && tran.TaxType != "B")
          {
            Decimal mult = ReportTaxProcess.GetMult(tran.Module, tran.TranType, tran.TaxType, line.LineMult);
            Decimal tranAmt;
            Decimal curyTranAmt;
            this.GetReportTranAmt(line, tran, out tranAmt, out curyTranAmt);
            TaxHistory taxHistory3 = taxHistory1;
            Decimal? unfiledAmt = taxHistory3.UnfiledAmt;
            Decimal num1 = mult * tranAmt;
            taxHistory3.UnfiledAmt = unfiledAmt.HasValue ? new Decimal?(unfiledAmt.GetValueOrDefault() + num1) : new Decimal?();
            TaxHistory taxHistory4 = taxHistory1;
            Decimal? reportUnfiledAmt = taxHistory4.ReportUnfiledAmt;
            Decimal num2 = mult;
            Decimal num3;
            if (!(currency.CuryID == tran.CuryID))
            {
              if (!(currency.CuryID == ((PXSelectBase<Company>) this.company).Current.BaseCuryID) && currency.CuryID != null && currencyRate2 != null)
              {
                short? decimalPlaces1 = currency.DecimalPlaces;
                int? decimalPlaces2;
                if (!decimalPlaces1.HasValue)
                {
                  nullable2 = new int?();
                  decimalPlaces2 = nullable2;
                }
                else
                  decimalPlaces2 = new int?((int) decimalPlaces1.GetValueOrDefault());
                PX.Objects.CM.CurrencyRate rate = currencyRate2;
                Decimal num4 = curyTranAmt;
                num3 = TaxHistorySumManager.RecalcCurrency(decimalPlaces2, rate, num4);
              }
              else
                num3 = tranAmt;
            }
            else
              num3 = curyTranAmt;
            Decimal num5 = num2 * num3;
            taxHistory4.ReportUnfiledAmt = reportUnfiledAmt.HasValue ? new Decimal?(reportUnfiledAmt.GetValueOrDefault() + num5) : new Decimal?();
          }
          if (!line.TempLine.GetValueOrDefault())
          {
            nullable2 = line.TempLineNbr;
            if (!nullable2.HasValue || !(taxHistory1.ReportUnfiledAmt.GetValueOrDefault() == 0M))
            {
              taxReportLine = line;
              continue;
            }
          }
          taxHistory1 = (TaxHistory) null;
        }
      }
    }
    if (taxHistory1 != null)
      ((PXResultset<TaxReportLine>) records).Add((PXResult<TaxReportLine>) new PXResult<TaxReportLine, TaxHistory>(taxReportLine, taxHistory1));
    PXResultset<TaxReportLine, TaxHistory> pxResultset1 = current == null || current.TaxUseVendorCurPrecision.GetValueOrDefault() ? records : TaxHistorySumManager.GetPreviewReport((PXGraph) this, current, filter.TaxReportRevisionID.Value, records, (Func<PXResult<TaxReportLine, TaxBucketLine, PX.Objects.CM.Extensions.Currency, TaxTranRevReport>, bool>) (line => this.ShowTaxReportLine(line, filter.TaxPeriodID)));
    PXResultset<TaxReportLine, TaxHistory> pxResultset2 = new PXResultset<TaxReportLine, TaxHistory>();
    foreach (PXResult<TaxReportLine, TaxHistory> pxResult in (PXResultset<TaxReportLine>) pxResultset1)
    {
      if (!PXResult<TaxReportLine, TaxHistory>.op_Implicit(pxResult).HideReportLine.GetValueOrDefault())
        ((PXResultset<TaxReportLine>) pxResultset2).Add((PXResult<TaxReportLine>) pxResult);
    }
    return (IEnumerable) pxResultset2;
  }

  protected virtual void GetReportTranAmt(
    TaxReportLine line,
    TaxTranRevReport tran,
    out Decimal tranAmt,
    out Decimal curyTranAmt)
  {
    switch (line.LineType)
    {
      case "P":
        tranAmt = tran.TaxAmt.GetValueOrDefault();
        curyTranAmt = tran.CuryTaxAmt.GetValueOrDefault();
        break;
      case "A":
        tranAmt = tran.TaxableAmt.GetValueOrDefault();
        curyTranAmt = tran.CuryTaxableAmt.GetValueOrDefault();
        break;
      case "E":
        tranAmt = tran.ExemptedAmt.GetValueOrDefault();
        curyTranAmt = tran.CuryExemptedAmt.GetValueOrDefault();
        break;
      default:
        tranAmt = 0M;
        curyTranAmt = 0M;
        break;
    }
  }

  public virtual bool ShowTaxReportLine(
    PXResult<TaxReportLine, TaxBucketLine, PX.Objects.CM.Extensions.Currency, TaxTranRevReport> taxReportLineDetails,
    string taxPeriodID)
  {
    return true;
  }

  [PXUIField]
  [PXProcessButton(IsLockedOnToolbar = true)]
  public virtual IEnumerable FileTax(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ReportTax.\u003C\u003Ec__DisplayClass39_0 cDisplayClass390 = new ReportTax.\u003C\u003Ec__DisplayClass39_0();
    ((PXGraph) this).Actions.PressSave();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass390.p = ((PXSelectBase<TaxPeriodFilter>) this.Period_Header).Current;
    int? nullable;
    int BAccountID;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass390.p.VendorID.HasValue)
    {
      // ISSUE: reference to a compiler-generated field
      nullable = cDisplayClass390.p.VendorID;
      BAccountID = nullable.Value;
    }
    else
      BAccountID = 0;
    // ISSUE: reference to a compiler-generated field
    nullable = cDisplayClass390.p.TaxReportRevisionID;
    int TaxReportRevisionID = nullable.Value;
    TaxReportMaint.TaxBucketAnalizer.CheckTaxAgencySettings((PXGraph) this, BAccountID, TaxReportRevisionID);
    // ISSUE: reference to a compiler-generated field
    if (!this.IsSynchronizedWithFinPeriods(cDisplayClass390.p))
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      throw new PXException("The tax report cannot be prepared and released, because the {0} tax period does not match the financial period in the general ledger for the {1} company. Use the {2} action on the Tax Periods (TX207000) form to correct the period structure for the {3} tax year.", new object[4]
      {
        (object) FinPeriodIDFormattingAttribute.FormatForError(cDisplayClass390.p.TaxPeriodID),
        (object) PXAccess.GetOrganizationCD(cDisplayClass390.p.OrganizationID),
        (object) "Synchronize Periods with GL",
        (object) cDisplayClass390.p.TaxPeriodID.Substring(0, 4)
      });
    }
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass390, __methodptr(\u003CFileTax\u003Eb__0)));
    return adapter.Get();
  }

  public static void FileTaxProc(TaxPeriodFilter p)
  {
    ReportTaxProcess instance1 = PXGraph.CreateInstance<ReportTaxProcess>();
    string str = instance1.FileTaxProc(p);
    ReportTaxReview instance2 = PXGraph.CreateInstance<ReportTaxReview>();
    ((PXGraph) instance2).TimeStamp = ((PXGraph) instance1).TimeStamp;
    ((PXSelectBase<TaxPeriodFilter>) instance2.Period_Header).Insert();
    ((PXSelectBase<TaxPeriodFilter>) instance2.Period_Header).Current.OrganizationID = p.OrganizationID;
    ((PXSelectBase<TaxPeriodFilter>) instance2.Period_Header).Current.BranchID = p.BranchID;
    ((PXSelectBase<TaxPeriodFilter>) instance2.Period_Header).Current.VendorID = p.VendorID;
    ((PXSelectBase<TaxPeriodFilter>) instance2.Period_Header).Current.TaxReportRevisionID = p.TaxReportRevisionID;
    ((PXSelectBase<TaxPeriodFilter>) instance2.Period_Header).Current.TaxPeriodID = p.TaxPeriodID;
    ((PXSelectBase<TaxPeriodFilter>) instance2.Period_Header).Current.RevisionId = ReportTaxProcess.CurrentRevisionId((PXGraph) instance2, p.OrganizationID, p.BranchID, p.VendorID, p.TaxPeriodID);
    ((PXSelectBase<TaxPeriodFilter>) instance2.Period_Header).Current.PreparedWarningMsg = str;
    throw new PXRedirectRequiredException((PXGraph) instance2, "Review");
  }

  [PXUIField]
  [PXProcessButton(IsLockedOnToolbar = true)]
  public virtual IEnumerable ViewTaxPeriods(PXAdapter adapter)
  {
    TaxPeriodFilter current = ((PXSelectBase<TaxPeriodFilter>) this.Period_Header).Current;
    if (current.OrganizationID.HasValue && current.VendorID.HasValue)
    {
      TaxYearMaint instance = PXGraph.CreateInstance<TaxYearMaint>();
      TaxYearMaint.TaxYearFilter copy = PXCache<TaxYearMaint.TaxYearFilter>.CreateCopy(((PXSelectBase<TaxYearMaint.TaxYearFilter>) instance.TaxYearFilterSelectView).Current);
      copy.OrganizationID = current.OrganizationID;
      copy.VendorID = current.VendorID;
      if (((PXSelectBase<TaxPeriod>) this.TaxPeriod_Current).Current != null)
        copy.Year = ((PXSelectBase<TaxPeriod>) this.TaxPeriod_Current).Current.TaxYear;
      ((PXSelectBase<TaxYearMaint.TaxYearFilter>) instance.TaxYearFilterSelectView).Update(copy);
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, string.Empty);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
      throw requiredException;
    }
    return adapter.Get();
  }

  public ReportTax()
  {
    PX.Objects.AP.APSetup current = ((PXSelectBase<PX.Objects.AP.APSetup>) this.APSetup).Current;
    ((PXSelectBase) this.Period_Details).Cache.SetAllEditPermissions(false);
    PXUIFieldAttribute.SetVisible<TaxReportLine.lineNbr>(((PXSelectBase) this.Period_Details).Cache, (object) null, false);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(ReportTax.\u003C\u003Ec.\u003C\u003E9__43_0 ?? (ReportTax.\u003C\u003Ec.\u003C\u003E9__43_0 = new PXFieldDefaulting((object) ReportTax.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__43_0))));
    this.TaxCalendar = new PX.Objects.TX.TaxCalendar<TaxYear, TaxPeriod>((PXGraph) this);
  }
}
