// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.ReportTaxReview
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.TX;

[TableAndChartDashboardType]
public class ReportTaxReview : PXGraph<ReportTax>
{
  public PXFilter<TaxPeriodFilter> Period_Header;
  public PXCancel<TaxPeriodFilter> Cancel;
  public PXSelect<TaxPeriod, Where<TaxPeriod.organizationID, Equal<Current<TaxPeriodFilter.organizationID>>, And<TaxPeriod.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>, And<TaxPeriod.taxPeriodID, Equal<Current<TaxPeriodFilter.taxPeriodID>>>>>> Period;
  public PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<TaxPeriodFilter.vendorID>>>> Vendor;
  public PXSelectJoin<TaxReportLine, LeftJoin<TaxHistory, On<TaxHistory.vendorID, Equal<TaxReportLine.vendorID>, And<TaxReportLine.taxReportRevisionID, Equal<TaxHistory.taxReportRevisionID>, And<TaxHistory.lineNbr, Equal<TaxReportLine.lineNbr>>>>>, Where<False, Equal<True>>, OrderBy<Asc<TaxReportLine.sortOrder, Asc<TaxReportLine.taxZoneID>>>> Period_Details;
  public PXSelectJoinGroupBy<TaxReportLine, LeftJoin<TaxHistory, On<TaxHistory.vendorID, Equal<TaxReportLine.vendorID>, And<TaxHistory.lineNbr, Equal<TaxReportLine.lineNbr>, And<TaxHistory.taxPeriodID, Equal<Current<TaxPeriodFilter.taxPeriodID>>, And2<Where<Current<TaxPeriodFilter.showDifference>, NotEqual<boolTrue>, Or<TaxHistory.revisionID, Equal<Current<TaxPeriodFilter.revisionId>>>>, And<Where<Current<TaxPeriodFilter.showDifference>, Equal<boolTrue>, Or<TaxHistory.revisionID, LessEqual<Current<TaxPeriodFilter.revisionId>>>>>>>>>>, Where<TaxReportLine.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>, And<TaxReportLine.tempLine, Equal<False>, And2<Where<TaxReportLine.tempLineNbr, IsNull, Or<TaxHistory.vendorID, IsNotNull>>, And<Where<TaxReportLine.hideReportLine, IsNull, Or<TaxReportLine.hideReportLine, Equal<False>>>>>>>, Aggregate<GroupBy<TaxReportLine.taxReportRevisionID, GroupBy<TaxReportLine.lineNbr, Sum<TaxHistory.filedAmt, Sum<TaxHistory.reportFiledAmt>>>>>> Period_Details_Expanded;
  [PXFilterable(new System.Type[] {})]
  public PXSelect<PX.Objects.AP.APInvoice> APDocuments;
  public PXAction<TaxPeriodFilter> adjustTax;
  public PXAction<TaxPeriodFilter> voidReport;
  public PXAction<TaxPeriodFilter> closePeriod;
  public PXAction<TaxPeriodFilter> viewDocument;
  public PXAction<TaxPeriodFilter> checkDocument;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  public virtual bool IsDirty => false;

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

  protected virtual void TaxPeriodFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    int? nullable1;
    int num1;
    if (!(e.Row is TaxPeriodFilter row))
    {
      num1 = 1;
    }
    else
    {
      nullable1 = row.OrganizationID;
      num1 = !nullable1.HasValue ? 1 : 0;
    }
    if (num1 != 0)
      return;
    PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID((PXGraph) this, row.OrganizationID);
    if (organizationById.FileTaxesByBranches.GetValueOrDefault())
    {
      nullable1 = row.BranchID;
      if (!nullable1.HasValue)
        return;
    }
    nullable1 = row.VendorID;
    if (!nullable1.HasValue || row.TaxPeriodID == null)
      return;
    TaxPeriod taxPeriod = PXResultset<TaxPeriod>.op_Implicit(((PXSelectBase<TaxPeriod>) this.Period).Select(Array.Empty<object>()));
    bool flag = taxPeriod?.Status == "P";
    int?[] branchesForProcessing = ReportTax.GetBranchesForProcessing((PXGraph) this, row.OrganizationID, row.BranchID, row.VendorID, row.TaxPeriodID);
    int? nullable2 = ReportTaxProcess.CurrentRevisionId(sender.Graph, row.OrganizationID, branchesForProcessing, row.VendorID, row.TaxPeriodID);
    if (nullable2.HasValue)
    {
      int num2 = nullable2.Value;
      nullable1 = row.RevisionId;
      int valueOrDefault = nullable1.GetValueOrDefault();
      if (!(num2 == valueOrDefault & nullable1.HasValue))
        flag = false;
    }
    row.StartDate = (DateTime?) taxPeriod?.StartDateUI;
    TaxPeriodFilter taxPeriodFilter = row;
    DateTime? nullable3;
    DateTime valueOrDefault1;
    DateTime? nullable4;
    if (taxPeriod != null)
    {
      nullable3 = taxPeriod.EndDate;
      if (nullable3.HasValue)
      {
        nullable3 = taxPeriod.EndDate;
        valueOrDefault1 = nullable3.Value;
        nullable4 = new DateTime?(valueOrDefault1.AddDays(-1.0));
        goto label_16;
      }
    }
    nullable3 = new DateTime?();
    nullable4 = nullable3;
label_16:
    taxPeriodFilter.EndDate = nullable4;
    PXCache pxCache1 = sender;
    nullable1 = nullable2;
    int num3 = 1;
    int num4 = nullable1.GetValueOrDefault() > num3 & nullable1.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<TaxPeriodFilter.revisionId>(pxCache1, (object) null, num4 != 0);
    PXCache pxCache2 = sender;
    nullable1 = nullable2;
    int num5 = 1;
    int num6;
    if (nullable1.GetValueOrDefault() > num5 & nullable1.HasValue)
    {
      nullable1 = row.RevisionId;
      int num7 = 1;
      num6 = nullable1.GetValueOrDefault() > num7 & nullable1.HasValue ? 1 : 0;
    }
    else
      num6 = 0;
    PXUIFieldAttribute.SetVisible<TaxPeriodFilter.showDifference>(pxCache2, (object) null, num6 != 0);
    PXUIFieldAttribute.SetEnabled<TaxPeriodFilter.taxPeriodID>(sender, (object) null, true);
    ((PXAction) this.voidReport).SetEnabled(flag);
    ((PXAction) this.adjustTax).SetEnabled(flag);
    ((PXAction) this.closePeriod).SetEnabled(flag);
    FinPeriod finPeriodByDate = this.FinPeriodRepository.FindFinPeriodByDate(row.EndDate, organizationById.OrganizationID);
    if (string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<TaxPeriodFilter.taxPeriodID>(sender, (object) row)))
    {
      sender.ClearFieldErrors<TaxPeriodFilter.taxPeriodID>((object) row);
      if (!string.IsNullOrEmpty(row.PreparedWarningMsg))
        sender.DisplayFieldWarning<TaxPeriodFilter.taxPeriodID>((object) row, (object) row.TaxPeriodID, row.PreparedWarningMsg);
      else if (flag && finPeriodByDate == null)
      {
        PXCache cache = sender;
        TaxPeriodFilter record = row;
        string taxPeriodId = row.TaxPeriodID;
        object[] objArray = new object[3];
        nullable3 = row.EndDate;
        ref DateTime? local = ref nullable3;
        string str;
        if (!local.HasValue)
        {
          str = (string) null;
        }
        else
        {
          valueOrDefault1 = local.GetValueOrDefault();
          str = valueOrDefault1.ToShortDateString();
        }
        objArray[0] = (object) str;
        objArray[1] = (object) PXAccess.GetOrganizationCD(organizationById.OrganizationID);
        objArray[2] = (object) PeriodIDAttribute.FormatForError(row.TaxPeriodID);
        cache.DisplayFieldWarning<TaxPeriodFilter.taxPeriodID>((object) record, (object) taxPeriodId, "The tax report cannot be released, and the tax period cannot be closed because the financial period related to the {0} end date and {2} tax period is not defined for the {1} company. To proceed, create and activate the necessary financial periods on the Company Financial Calendar (GL201010) form.", objArray);
      }
      else if (flag && (finPeriodByDate.Status == "Inactive" || finPeriodByDate.Status == "Locked"))
      {
        PXCache cache = sender;
        TaxPeriodFilter record = row;
        string taxPeriodId = row.TaxPeriodID;
        object[] objArray = new object[3];
        nullable3 = row.EndDate;
        ref DateTime? local = ref nullable3;
        string str;
        if (!local.HasValue)
        {
          str = (string) null;
        }
        else
        {
          valueOrDefault1 = local.GetValueOrDefault();
          str = valueOrDefault1.ToShortDateString();
        }
        objArray[0] = (object) str;
        objArray[1] = (object) PXAccess.GetOrganizationCD(organizationById.OrganizationID);
        objArray[2] = (object) PeriodIDAttribute.FormatForError(row.TaxPeriodID);
        cache.DisplayFieldWarning<TaxPeriodFilter.taxPeriodID>((object) record, (object) taxPeriodId, "The financial period corresponding to the {0} end date of the {2} tax period is inactive or locked in the {1} company.", objArray);
      }
      else if (flag && finPeriodByDate.APClosed.GetValueOrDefault() && !this.FinPeriodUtils.CanPostToClosedPeriod())
      {
        PXCache cache = sender;
        TaxPeriodFilter record = row;
        string taxPeriodId = row.TaxPeriodID;
        object[] objArray = new object[3];
        nullable3 = row.EndDate;
        ref DateTime? local = ref nullable3;
        string str;
        if (!local.HasValue)
        {
          str = (string) null;
        }
        else
        {
          valueOrDefault1 = local.GetValueOrDefault();
          str = valueOrDefault1.ToShortDateString();
        }
        objArray[0] = (object) str;
        objArray[1] = (object) PXAccess.GetOrganizationCD(organizationById.OrganizationID);
        objArray[2] = (object) PeriodIDAttribute.FormatForError(row.TaxPeriodID);
        cache.DisplayFieldWarning<TaxPeriodFilter.taxPeriodID>((object) record, (object) taxPeriodId, "The financial period corresponding to the {0} end date of the {2} tax period is closed in the Accounts Payable module in the {1} company. The generated documents will be posted to the first available open period.", objArray);
      }
      if (!this.IsSynchronizedWithFinPeriods(row))
        sender.RaiseExceptionHandling<TaxPeriodFilter.taxPeriodID>((object) row, (object) row.TaxPeriodID, (Exception) new PXSetPropertyException("The tax report cannot be prepared and released, because the {0} tax period does not match the financial period in the general ledger for the {1} company. Void the tax report, then use the {2} action on the Tax Periods (TX207000) form to correct the period structure for the {3} tax year.", (PXErrorLevel) 4, new object[4]
        {
          (object) FinPeriodIDFormattingAttribute.FormatForError(row.TaxPeriodID),
          (object) PXAccess.GetOrganizationCD(row.OrganizationID),
          (object) "Synchronize Periods with GL",
          (object) row.TaxPeriodID.Substring(0, 4)
        }));
    }
    if (!string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<TaxPeriodFilter.vendorID>(sender, (object) row)))
      return;
    int? organizationId = row.OrganizationID;
    int? branchId = row.BranchID;
    PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(((PXSelectBase<PX.Objects.AP.Vendor>) this.Vendor).Select(Array.Empty<object>()));
    DateTime? endDate;
    if (taxPeriod == null)
    {
      nullable3 = new DateTime?();
      endDate = nullable3;
    }
    else
      endDate = taxPeriod.EndDate;
    if (ReportTaxProcess.CheckForUnprocessedSVAT((PXGraph) this, organizationId, branchId, vendor, endDate))
      sender.RaiseExceptionHandling<TaxPeriodFilter.vendorID>(e.Row, (object) row.VendorID, (Exception) new PXSetPropertyException("In the tax period, there are payment applications for which pending VAT amount has not been recognized yet. To recognize pending VAT, use the Recognize Output VAT (TX503000) form and the Recognize Input VAT (TX503500) form.", (PXErrorLevel) 2));
    else
      sender.RaiseExceptionHandling<TaxPeriodFilter.vendorID>(e.Row, (object) row.VendorID, (Exception) null);
  }

  protected virtual void TaxPeriodFilter_VendorID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetValue<TaxPeriodFilter.taxPeriodID>(e.Row, (object) null);
  }

  protected virtual void TaxPeriodFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    TaxPeriodFilter row1 = (TaxPeriodFilter) e.Row;
    if (row1 == null)
      return;
    if (!sender.ObjectsEqual<TaxPeriodFilter.organizationID, TaxPeriodFilter.branchID>(e.Row, e.OldRow))
    {
      foreach (PXView pxView in ((IEnumerable<KeyValuePair<string, PXView>>) ((PXGraph) this).Views).Select<KeyValuePair<string, PXView>, PXView>((Func<KeyValuePair<string, PXView>, PXView>) (view => view.Value)).ToList<PXView>())
        pxView.Clear();
    }
    int? nullable1;
    if (!sender.ObjectsEqual<TaxPeriodFilter.organizationID>(e.Row, e.OldRow) || !sender.ObjectsEqual<TaxPeriodFilter.branchID>(e.Row, e.OldRow) || !sender.ObjectsEqual<TaxPeriodFilter.vendorID>(e.Row, e.OldRow))
    {
      nullable1 = row1.OrganizationID;
      if (nullable1.HasValue)
      {
        nullable1 = row1.VendorID;
        if (nullable1.HasValue)
        {
          TaxPeriod preparedPeriod = TaxYearMaint.FindPreparedPeriod((PXGraph) this, row1.OrganizationID, row1.VendorID);
          if (preparedPeriod != null)
          {
            row1.TaxPeriodID = preparedPeriod.TaxPeriodID;
            goto label_16;
          }
          TaxPeriod lastClosedPeriod = TaxYearMaint.FindLastClosedPeriod((PXGraph) this, row1.OrganizationID, row1.VendorID);
          row1.TaxPeriodID = lastClosedPeriod?.TaxPeriodID;
          goto label_16;
        }
      }
      row1.TaxPeriodID = (string) null;
    }
label_16:
    PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID((PXGraph) this, row1.OrganizationID);
    if (!sender.ObjectsEqual<TaxPeriodFilter.organizationID>(e.Row, e.OldRow) || !sender.ObjectsEqual<TaxPeriodFilter.branchID>(e.Row, e.OldRow) || !sender.ObjectsEqual<TaxPeriodFilter.vendorID>(e.Row, e.OldRow) || !sender.ObjectsEqual<TaxPeriodFilter.taxPeriodID>(e.Row, e.OldRow))
    {
      nullable1 = row1.OrganizationID;
      if (nullable1.HasValue)
      {
        nullable1 = row1.BranchID;
        bool? fileTaxesByBranches;
        if (nullable1.HasValue)
        {
          fileTaxesByBranches = organizationById.FileTaxesByBranches;
          if (fileTaxesByBranches.GetValueOrDefault())
            goto label_21;
        }
        fileTaxesByBranches = organizationById.FileTaxesByBranches;
        if (fileTaxesByBranches.GetValueOrDefault())
          goto label_23;
label_21:
        nullable1 = row1.VendorID;
        if (nullable1.HasValue && row1.TaxPeriodID != null)
        {
          row1.RevisionId = ReportTaxProcess.CurrentRevisionId((PXGraph) this, row1.OrganizationID, row1.BranchID, row1.VendorID, row1.TaxPeriodID);
          goto label_24;
        }
      }
label_23:
      TaxPeriodFilter taxPeriodFilter = row1;
      nullable1 = new int?();
      int? nullable2 = nullable1;
      taxPeriodFilter.RevisionId = nullable2;
    }
label_24:
    if (row1.TaxPeriodID == null)
      return;
    TaxPeriod taxPeriodByKey = TaxYearMaint.FindTaxPeriodByKey((PXGraph) this, row1.OrganizationID, row1.VendorID, row1.TaxPeriodID);
    PXCache pxCache = sender;
    object row2 = e.Row;
    int? nullable3;
    if (taxPeriodByKey != null)
    {
      TaxReport reportVersionByDate = TaxReportMaint.GetTaxReportVersionByDate((PXGraph) this, row1.VendorID, new DateTime?(taxPeriodByKey.EndDate.Value.AddDays(-1.0)));
      if (reportVersionByDate == null)
      {
        nullable1 = new int?();
        nullable3 = nullable1;
      }
      else
        nullable3 = reportVersionByDate.RevisionID;
    }
    else
    {
      nullable1 = new int?();
      nullable3 = nullable1;
    }
    // ISSUE: variable of a boxed type
    __Boxed<int?> local = (ValueType) nullable3;
    pxCache.SetValue<TaxPeriodFilter.taxReportRevisionID>(row2, (object) local);
  }

  protected virtual IEnumerable period_Details()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ReportTaxReview.\u003C\u003Ec__DisplayClass21_0 cDisplayClass210 = new ReportTaxReview.\u003C\u003Ec__DisplayClass21_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass210.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass210.filter = ((PXSelectBase<TaxPeriodFilter>) this.Period_Header).Current;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    using (new PXReadBranchRestrictedScope(cDisplayClass210.filter.OrganizationID.SingleToArray<int?>(), cDisplayClass210.filter.BranchID.SingleToArrayOrNull<int?>(), true, false))
    {
      ParameterExpression parameterExpression;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      return (IEnumerable) ((IQueryable<PXResult<TaxReportLine>>) ((PXSelectBase<TaxReportLine>) this.Period_Details_Expanded).Select(new object[2]
      {
        (object) cDisplayClass210.filter.OrganizationID,
        (object) cDisplayClass210.filter.BranchID
      })).Where<PXResult<TaxReportLine>>((Expression<Func<PXResult<TaxReportLine>, bool>>) (row => ((TaxReportLine) row).TaxReportRevisionID == cDisplayClass210.filter.TaxReportRevisionID)).Where<PXResult<TaxReportLine>>(Expression.Lambda<Func<PXResult<TaxReportLine>, bool>>((Expression) Expression.Call(this, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ReportTaxReview.ShowTaxReportLine)), new Expression[2]
      {
        (Expression) Expression.Call(line, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()),
        (Expression) Expression.Property((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass210, typeof (ReportTaxReview.\u003C\u003Ec__DisplayClass21_0)), FieldInfo.GetFieldFromHandle(__fieldref (ReportTaxReview.\u003C\u003Ec__DisplayClass21_0.filter))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (TaxPeriodFilter.get_TaxPeriodID)))
      }), parameterExpression));
    }
  }

  protected virtual IEnumerable apdocuments()
  {
    TaxPeriodFilter current = ((PXSelectBase<TaxPeriodFilter>) this.Period_Header).Current;
    if (current == null)
      return (IEnumerable) new object[0];
    PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID((PXGraph) this, current.OrganizationID);
    if (organizationById != null)
    {
      int? branchId;
      if (organizationById.FileTaxesByBranches.GetValueOrDefault())
      {
        branchId = current.BranchID;
        if (!branchId.HasValue)
          goto label_5;
      }
      branchId = current.BranchID;
      int[] numArray;
      if (!branchId.HasValue)
      {
        numArray = PXAccess.GetChildBranchIDs(current.OrganizationID, true);
      }
      else
      {
        numArray = new int[1];
        branchId = current.BranchID;
        numArray[0] = branchId.Value;
      }
      return (IEnumerable) PXSelectBase<PX.Objects.AP.APInvoice, PXSelect<PX.Objects.AP.APInvoice, Where<PX.Objects.AP.APInvoice.docDate, GreaterEqual<Current<TaxPeriodFilter.startDate>>, And<PX.Objects.AP.APInvoice.docDate, LessEqual<Current<TaxPeriodFilter.endDate>>, And<PX.Objects.AP.APInvoice.vendorID, Equal<Current<TaxPeriodFilter.vendorID>>, And<PX.Objects.AP.APInvoice.branchID, In<Required<PX.Objects.AP.APInvoice.branchID>>, And<Where<PX.Objects.AP.APInvoice.docType, Equal<APDocType.invoice>, Or<PX.Objects.AP.APInvoice.docType, Equal<APDocType.debitAdj>>>>>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) numArray
      });
    }
label_5:
    return (IEnumerable) new object[0];
  }

  public virtual bool ShowTaxReportLine(TaxReportLine taxReportLine, string taxPeriodID) => true;

  [PXUIField]
  [PXButton]
  public virtual IEnumerable AdjustTax(PXAdapter adapter)
  {
    TaxAdjustmentEntry instance = PXGraph.CreateInstance<TaxAdjustmentEntry>();
    ((PXGraph) instance).Clear();
    TaxPeriodFilter current = ((PXSelectBase<TaxPeriodFilter>) this.Period_Header).Current;
    TaxAdjustment taxAdjustment = ((PXSelectBase<TaxAdjustment>) instance.Document).Insert(new TaxAdjustment());
    taxAdjustment.VendorID = current.VendorID;
    ((PXSelectBase) instance.Document).Cache.RaiseFieldUpdated<TaxAdjustment.vendorID>((object) taxAdjustment, (object) null);
    PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID((PXGraph) this, current.OrganizationID);
    if (current.OrganizationID.HasValue)
    {
      int? nullable = current.BranchID;
      bool? fileTaxesByBranches;
      if (nullable.HasValue)
      {
        fileTaxesByBranches = organizationById.FileTaxesByBranches;
        if (fileTaxesByBranches.GetValueOrDefault())
          goto label_4;
      }
      fileTaxesByBranches = organizationById.FileTaxesByBranches;
      if (fileTaxesByBranches.GetValueOrDefault())
        goto label_11;
label_4:
      nullable = current.BranchID;
      if (nullable.HasValue)
      {
        taxAdjustment.BranchID = current.BranchID;
      }
      else
      {
        PX.Objects.GL.Branch branchById = BranchMaint.FindBranchByID((PXGraph) this, ((PXGraph) this).Accessinfo.BranchID);
        if (branchById != null)
        {
          nullable = branchById.OrganizationID;
          int? organizationId = organizationById.OrganizationID;
          if (nullable.GetValueOrDefault() == organizationId.GetValueOrDefault() & nullable.HasValue == organizationId.HasValue)
          {
            taxAdjustment.BranchID = branchById.BranchID;
            goto label_10;
          }
        }
        taxAdjustment.BranchID = new int?();
      }
label_10:
      ((PXSelectBase) instance.Document).Cache.RaiseFieldUpdated<TaxAdjustment.branchID>((object) taxAdjustment, (object) null);
    }
label_11:
    throw new PXRedirectRequiredException((PXGraph) instance, "New Adjustment");
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable VoidReport(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) new ReportTaxReview.\u003C\u003Ec__DisplayClass27_0()
    {
      tp = ((PXSelectBase<TaxPeriodFilter>) this.Period_Header).Current
    }, __methodptr(\u003CVoidReport\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable ClosePeriod(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ReportTaxReview.\u003C\u003Ec__DisplayClass29_0 cDisplayClass290 = new ReportTaxReview.\u003C\u003Ec__DisplayClass29_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass290.tp = ((PXSelectBase<TaxPeriodFilter>) this.Period_Header).Current;
    // ISSUE: reference to a compiler-generated field
    if (!this.IsSynchronizedWithFinPeriods(cDisplayClass290.tp))
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      throw new PXException("The tax report cannot be prepared and released, because the {0} tax period does not match the financial period in the general ledger for the {1} company. Void the tax report, then use the {2} action on the Tax Periods (TX207000) form to correct the period structure for the {3} tax year.", new object[4]
      {
        (object) FinPeriodIDFormattingAttribute.FormatForError(cDisplayClass290.tp.TaxPeriodID),
        (object) PXAccess.GetOrganizationCD(cDisplayClass290.tp.OrganizationID),
        (object) "Synchronize Periods with GL",
        (object) cDisplayClass290.tp.TaxPeriodID.Substring(0, 4)
      });
    }
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass290, __methodptr(\u003CClosePeriod\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<TaxReportLine>) this.Period_Details).Current != null)
    {
      ReportTaxDetail instance = PXGraph.CreateInstance<ReportTaxDetail>();
      TaxHistoryMaster copy = PXCache<TaxHistoryMaster>.CreateCopy(((PXSelectBase<TaxHistoryMaster>) instance.History_Header).Current);
      copy.OrganizationID = ((PXSelectBase<TaxPeriodFilter>) this.Period_Header).Current.OrganizationID;
      copy.BranchID = ((PXSelectBase<TaxPeriodFilter>) this.Period_Header).Current.BranchID;
      copy.VendorID = ((PXSelectBase<TaxPeriodFilter>) this.Period_Header).Current.VendorID;
      copy.TaxPeriodID = ((PXSelectBase<TaxPeriodFilter>) this.Period_Header).Current.TaxPeriodID;
      copy.LineNbr = ((PXSelectBase<TaxReportLine>) this.Period_Details).Current.LineNbr;
      ((PXSelectBase<TaxHistoryMaster>) instance.History_Header).Update(copy);
      throw new PXRedirectRequiredException((PXGraph) instance, "View Documents");
    }
    return (IEnumerable) ((PXSelectBase<TaxPeriodFilter>) this.Period_Header).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXButton(ImageKey = "Inquiry")]
  public virtual IEnumerable CheckDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.AP.APInvoice>) this.APDocuments).Current != null)
    {
      APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
      PX.Objects.AP.APInvoice apInvoice = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Search<PX.Objects.AP.APInvoice.refNbr>((object) ((PXSelectBase<PX.Objects.AP.APInvoice>) this.APDocuments).Current.RefNbr, new object[1]
      {
        (object) ((PXSelectBase<PX.Objects.AP.APInvoice>) this.APDocuments).Current.DocType
      }));
      if (apInvoice != null)
      {
        ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Current = apInvoice;
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Document");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return (IEnumerable) ((PXSelectBase<TaxPeriodFilter>) this.Period_Header).Select(Array.Empty<object>());
  }

  public static void VoidReportProc(TaxPeriodFilter p)
  {
    ReportTaxProcess instance1 = PXGraph.CreateInstance<ReportTaxProcess>();
    instance1.VoidReportProc(p);
    ReportTax instance2 = PXGraph.CreateInstance<ReportTax>();
    ((PXGraph) instance2).TimeStamp = ((PXGraph) instance1).TimeStamp;
    TaxPeriodFilter copy = PXCache<TaxPeriodFilter>.CreateCopy(((PXSelectBase<TaxPeriodFilter>) instance2.Period_Header).Current);
    copy.VendorID = p.VendorID;
    copy.BranchID = p.BranchID;
    copy.OrganizationID = p.OrganizationID;
    ((PXSelectBase<TaxPeriodFilter>) instance2.Period_Header).Update(copy);
    throw new PXRedirectRequiredException((PXGraph) instance2, "Report");
  }

  public static void ClosePeriodProc(TaxPeriodFilter p)
  {
    PXGraph.CreateInstance<ReportTaxProcess>().ClosePeriodProc(p);
  }

  public static void ReleaseDoc(List<TaxAdjustment> list)
  {
    ReportTaxProcess instance1 = PXGraph.CreateInstance<ReportTaxProcess>();
    JournalEntry instance2 = PXGraph.CreateInstance<JournalEntry>();
    instance2.Mode |= JournalEntry.Modes.TaxReporting;
    PostGraph instance3 = PXGraph.CreateInstance<PostGraph>();
    List<Batch> batchList = new List<Batch>();
    List<int> intList = new List<int>();
    list.Sort((Comparison<TaxAdjustment>) ((a, b) =>
    {
      int num = ((IComparable) a.BranchID).CompareTo((object) b.BranchID);
      return num != 0 ? num : a.FinPeriodID.CompareTo((object) b.FinPeriodID);
    }));
    for (int index = 0; index < list.Count; ++index)
    {
      TaxAdjustment doc = list[index];
      instance1.ReleaseDocProc(instance2, doc);
      ((PXGraph) instance1).Clear();
      if (((PXSelectBase<Batch>) instance2.BatchModule).Current != null && !batchList.Contains(((PXSelectBase<Batch>) instance2.BatchModule).Current))
      {
        batchList.Add(((PXSelectBase<Batch>) instance2.BatchModule).Current);
        intList.Add(index);
      }
    }
    foreach (Batch b in batchList)
    {
      ((PXGraph) instance3).TimeStamp = b.tstamp;
      instance3.PostBatchProc(b);
      ((PXGraph) instance3).Clear();
    }
  }

  public ReportTaxReview()
  {
    PX.Objects.AP.APSetup current = ((PXSelectBase<PX.Objects.AP.APSetup>) this.APSetup).Current;
    ((PXSelectBase) this.Period_Details).Cache.SetAllEditPermissions(false);
    ((PXSelectBase) this.APDocuments).Cache.SetAllEditPermissions(false);
    PXUIFieldAttribute.SetVisible<TaxReportLine.lineNbr>(((PXSelectBase) this.Period_Details).Cache, (object) null, false);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(ReportTaxReview.\u003C\u003Ec.\u003C\u003E9__37_0 ?? (ReportTaxReview.\u003C\u003Ec.\u003C\u003E9__37_0 = new PXFieldDefaulting((object) ReportTaxReview.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__37_0))));
  }
}
