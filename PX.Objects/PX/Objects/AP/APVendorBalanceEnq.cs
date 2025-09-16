// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APVendorBalanceEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Utility;
using PX.Objects.CR;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AP;

[TableAndChartDashboardType]
public class APVendorBalanceEnq : PXGraph<
#nullable disable
APVendorBalanceEnq>
{
  public PXFilter<APVendorBalanceEnq.APHistoryFilter> Filter;
  public PXCancel<APVendorBalanceEnq.APHistoryFilter> Cancel;
  [PXFilterable(new System.Type[] {})]
  public PXSelect<APVendorBalanceEnq.APHistoryResult> History;
  [PXVirtualDAC]
  public PXFilter<APVendorBalanceEnq.APHistorySummary> Summary;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;
  public PXSetup<PX.Objects.GL.Company> Company;
  [Obsolete("Will be removed in Acumatica 2019R1")]
  public PXAction<APVendorBalanceEnq.APHistoryFilter> viewDetails;
  public PXAction<APVendorBalanceEnq.APHistoryFilter> previousPeriod;
  public PXAction<APVendorBalanceEnq.APHistoryFilter> nextPeriod;
  public PXAction<APVendorBalanceEnq.APHistoryFilter> reports;
  public PXAction<APVendorBalanceEnq.APHistoryFilter> aPBalanceByVendorReport;
  public PXAction<APVendorBalanceEnq.APHistoryFilter> vendorHistoryReport;
  public PXAction<APVendorBalanceEnq.APHistoryFilter> aPAgedPastDueReport;
  public PXAction<APVendorBalanceEnq.APHistoryFilter> aPAgedOutstandingReport;
  protected readonly Dictionary<System.Type, System.Type> mapFin = new Dictionary<System.Type, System.Type>()
  {
    {
      typeof (APVendorBalanceEnq.APHistoryResult.acctCD),
      typeof (PX.Objects.CA.Light.Vendor.acctCD)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.acctName),
      typeof (PX.Objects.CA.Light.Vendor.acctName)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.vendorID),
      typeof (PX.Objects.CA.Light.Vendor.bAccountID)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.noteID),
      typeof (PX.Objects.CA.Light.BAccount.noteID)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyID),
      typeof (APHistoryByPeriod.curyID)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.finPeriodID),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.finPeriodID)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyBegBalance),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyFinBegBalance)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyEndBalance),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyFinYtdBalance)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyPurchases),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyFinPtdPurchases)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyPayments),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyFinPtdPayments)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyDiscount),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyFinPtdDiscTaken)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyWhTax),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyFinPtdWhTax)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyCrAdjustments),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyFinPtdCrAdjustments)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyDrAdjustments),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyFinPtdDrAdjustments)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyDeposits),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyFinPtdDeposits)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyDepositsBalance),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyFinYtdDeposits)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyRetainageWithheld),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyFinPtdRetainageWithheld)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyRetainageReleased),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyFinPtdRetainageReleased)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyBegRetainedBalance),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyFinBegRetainedBalance)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyEndRetainedBalance),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyFinYtdRetainedBalance)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.begBalance),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.finBegBalance)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.endBalance),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.finYtdBalance)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.purchases),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.finPtdPurchases)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.payments),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.finPtdPayments)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.discount),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.finPtdDiscTaken)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.whTax),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.finPtdWhTax)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.rGOL),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.finPtdRGOL)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.crAdjustments),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.finPtdCrAdjustments)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.drAdjustments),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.finPtdDrAdjustments)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.deposits),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.finPtdDeposits)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.depositsBalance),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.finYtdDeposits)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.retainageWithheld),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.finPtdRetainageWithheld)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.retainageReleased),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.finPtdRetainageReleased)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.begRetainedBalance),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.finBegRetainedBalance)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.endRetainedBalance),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.finYtdRetainedBalance)
    }
  };
  protected readonly Dictionary<System.Type, System.Type> mapTran = new Dictionary<System.Type, System.Type>()
  {
    {
      typeof (APVendorBalanceEnq.APHistoryResult.acctCD),
      typeof (PX.Objects.CA.Light.Vendor.acctCD)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.acctName),
      typeof (PX.Objects.CA.Light.Vendor.acctName)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.vendorID),
      typeof (PX.Objects.CA.Light.Vendor.bAccountID)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.noteID),
      typeof (PX.Objects.CA.Light.BAccount.noteID)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyID),
      typeof (APHistoryByPeriod.curyID)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.finPeriodID),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.finPeriodID)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyBegBalance),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyTranBegBalance)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyEndBalance),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyTranYtdBalance)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyPurchases),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyTranPtdPurchases)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyPayments),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyTranPtdPayments)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyDiscount),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyTranPtdDiscTaken)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyWhTax),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyTranPtdWhTax)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyCrAdjustments),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyTranPtdCrAdjustments)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyDrAdjustments),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyTranPtdDrAdjustments)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyDeposits),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyTranPtdDeposits)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyDepositsBalance),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyTranYtdDeposits)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyRetainageWithheld),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyTranPtdRetainageWithheld)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyRetainageReleased),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyTranPtdRetainageReleased)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyBegRetainedBalance),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyTranBegRetainedBalance)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.curyEndRetainedBalance),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.curyTranYtdRetainedBalance)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.begBalance),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.tranBegBalance)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.endBalance),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.tranYtdBalance)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.purchases),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.tranPtdPurchases)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.payments),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.tranPtdPayments)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.discount),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.tranPtdDiscTaken)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.whTax),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.tranPtdWhTax)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.rGOL),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.tranPtdRGOL)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.crAdjustments),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.tranPtdCrAdjustments)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.drAdjustments),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.tranPtdDrAdjustments)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.deposits),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.tranPtdDeposits)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.depositsBalance),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.tranYtdDeposits)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.retainageWithheld),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.tranPtdRetainageWithheld)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.retainageReleased),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.tranPtdRetainageReleased)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.begRetainedBalance),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.tranBegRetainedBalance)
    },
    {
      typeof (APVendorBalanceEnq.APHistoryResult.endRetainedBalance),
      typeof (APVendorBalanceEnq.CuryAPHistoryTran.tranYtdRetainedBalance)
    }
  };

  protected virtual IEnumerable history() => this.RetrieveHistoryForPeriod(this.Filter.Current);

  protected virtual IEnumerable summary()
  {
    bool? calculated = this.Summary.Current.Calculated;
    bool flag = false;
    if (calculated.GetValueOrDefault() == flag & calculated.HasValue)
    {
      APVendorBalanceEnq.APHistorySummary instance = this.Summary.Cache.CreateInstance() as APVendorBalanceEnq.APHistorySummary;
      instance.ClearSummary();
      this.Summary.Insert(instance);
      this.RetrieveHistoryForPeriod(this.Filter.Current, instance);
      this.Summary.Update(instance);
    }
    yield return (object) this.Summary.Current;
  }

  public APVendorBalanceEnq()
  {
    PX.Objects.AP.APSetup current1 = this.APSetup.Current;
    PX.Objects.GL.Company current2 = this.Company.Current;
    this.History.Cache.AllowDelete = false;
    this.History.Cache.AllowInsert = false;
    this.History.Cache.AllowUpdate = false;
    this.reports.MenuAutoOpen = true;
    this.reports.AddMenuAction((PXAction) this.aPBalanceByVendorReport);
    this.reports.AddMenuAction((PXAction) this.vendorHistoryReport);
    this.reports.AddMenuAction((PXAction) this.aPAgedPastDueReport);
    this.reports.AddMenuAction((PXAction) this.aPAgedOutstandingReport);
  }

  public override bool IsDirty => false;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = false)]
  [PXEditDetailButton]
  public virtual IEnumerable ViewDetails(PXAdapter adapter)
  {
    if (this.History.Current != null && this.Filter.Current != null)
    {
      APVendorBalanceEnq.APHistoryResult current1 = this.History.Current;
      APVendorBalanceEnq.APHistoryFilter current2 = this.Filter.Current;
      APDocumentEnq instance = PXGraph.CreateInstance<APDocumentEnq>();
      APDocumentEnq.APDocumentFilter current3 = instance.Filter.Current;
      APVendorBalanceEnq.Copy(current3, current2);
      current3.VendorID = current1.VendorID;
      current3.BalanceSummary = new Decimal?();
      instance.Filter.Update(current3);
      APDocumentEnq.APDocumentFilter apDocumentFilter = (APDocumentEnq.APDocumentFilter) instance.Filter.Select();
      throw new PXRedirectRequiredException((PXGraph) instance, "Vendor Details");
    }
    return (IEnumerable) this.Filter.Select();
  }

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXPreviousButton]
  public virtual IEnumerable PreviousPeriod(PXAdapter adapter)
  {
    APVendorBalanceEnq.APHistoryFilter current = this.Filter.Current;
    FinPeriod prevPeriod = this.FinPeriodRepository.FindPrevPeriod(this.FinPeriodRepository.GetCalendarOrganizationID(current.OrganizationID, current.BranchID, current.UseMasterCalendar), current.FinPeriodID, true);
    current.FinPeriodID = prevPeriod?.FinPeriodID;
    this.Summary.Current.ClearSummary();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXNextButton]
  public virtual IEnumerable NextPeriod(PXAdapter adapter)
  {
    APVendorBalanceEnq.APHistoryFilter current = this.Filter.Current;
    FinPeriod nextPeriod = this.FinPeriodRepository.FindNextPeriod(this.FinPeriodRepository.GetCalendarOrganizationID(current.OrganizationID, current.BranchID, current.UseMasterCalendar), current.FinPeriodID, true);
    current.FinPeriodID = nextPeriod?.FinPeriodID;
    this.Summary.Current.ClearSummary();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Reports", MapEnableRights = PXCacheRights.Select)]
  [PXButton(SpecialType = PXSpecialButtonType.ReportsFolder)]
  protected virtual IEnumerable Reports(PXAdapter adapter) => adapter.Get();

  [PXUIField(DisplayName = "AP Balance by Vendor", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  public virtual IEnumerable APBalanceByVendorReport(PXAdapter adapter)
  {
    APVendorBalanceEnq.APHistoryFilter current1 = this.Filter.Current;
    APVendorBalanceEnq.APHistoryResult current2 = this.History.Current;
    if (current1 != null && current2 != null)
    {
      Dictionary<string, string> reportParameters = this.GetBasicReportParameters(current1, current2);
      if (!string.IsNullOrEmpty(current1.FinPeriodID))
        reportParameters["PeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(current1.FinPeriodID);
      reportParameters["UseMasterCalendar"] = current1.UseMasterCalendar.GetValueOrDefault() ? true.ToString() : false.ToString();
      throw new PXReportRequiredException(reportParameters, "AP632500", "AP Balance by Vendor");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Vendor History", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  public virtual IEnumerable VendorHistoryReport(PXAdapter adapter)
  {
    APVendorBalanceEnq.APHistoryFilter current1 = this.Filter.Current;
    APVendorBalanceEnq.APHistoryResult current2 = this.History.Current;
    if (current1 != null && current2 != null)
    {
      Dictionary<string, string> reportParameters = this.GetBasicReportParameters(current1, current2);
      if (!string.IsNullOrEmpty(current1.FinPeriodID))
      {
        reportParameters["FromPeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(current1.FinPeriodID);
        reportParameters["ToPeriodID"] = FinPeriodIDFormattingAttribute.FormatForDisplay(current1.FinPeriodID);
      }
      throw new PXReportRequiredException(reportParameters, "AP652000", "Vendor History");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "AP Aging", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  public virtual IEnumerable APAgedPastDueReport(PXAdapter adapter)
  {
    APVendorBalanceEnq.APHistoryFilter current1 = this.Filter.Current;
    APVendorBalanceEnq.APHistoryResult current2 = this.History.Current;
    if (current1 != null && current2 != null)
      throw new PXReportRequiredException(this.GetBasicReportParameters(current1, current2), "AP631000", "AP Aging");
    return adapter.Get();
  }

  [PXUIField(DisplayName = "AP Coming Due", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  public virtual IEnumerable APAgedOutstandingReport(PXAdapter adapter)
  {
    APVendorBalanceEnq.APHistoryFilter current1 = this.Filter.Current;
    APVendorBalanceEnq.APHistoryResult current2 = this.History.Current;
    if (current1 != null && current2 != null)
      throw new PXReportRequiredException(this.GetBasicReportParameters(current1, current2), "AP631500", "AP Coming Due");
    return adapter.Get();
  }

  private Dictionary<string, string> GetBasicReportParameters(
    APVendorBalanceEnq.APHistoryFilter filter,
    APVendorBalanceEnq.APHistoryResult currentRow)
  {
    BAccountR baccountR = (BAccountR) null;
    if (filter.OrgBAccountID.HasValue)
      baccountR = (BAccountR) PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, (object) filter.OrgBAccountID);
    return new Dictionary<string, string>()
    {
      {
        "VendorID",
        VendorMaint.FindByID((PXGraph) this, currentRow.VendorID)?.AcctCD
      },
      {
        "OrgBAccountID",
        baccountR?.AcctCD
      }
    };
  }

  public virtual void APHistoryFilter_SubID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  public virtual void APHistoryFilter_AccountID_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    if (!(e.Row is APVendorBalanceEnq.APHistoryFilter row))
      return;
    e.Cancel = true;
    int? nullable = new int?();
    row.AccountID = nullable;
  }

  public virtual void APHistoryFilter_CashAcctID_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    if (!(e.Row is APVendorBalanceEnq.APHistoryFilter row))
      return;
    e.Cancel = true;
    int? nullable = new int?();
    row.CashAcctID = nullable;
  }

  public virtual void APHistoryFilter_CuryID_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    if (!(e.Row is APVendorBalanceEnq.APHistoryFilter row))
      return;
    e.Cancel = true;
    row.CuryID = (string) null;
  }

  public virtual void APHistoryFilter_FinPeriodID_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    if (!(e.Row is APVendorBalanceEnq.APHistoryFilter row))
      return;
    e.Cancel = true;
    row.FinPeriodID = (string) null;
  }

  public virtual void APHistoryFilter_VendorClassID_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    if (!(e.Row is APVendorBalanceEnq.APHistoryFilter row))
      return;
    e.Cancel = true;
    row.VendorClassID = (string) null;
  }

  public virtual void APHistoryFilter_CuryID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    cache.SetDefaultExt<APVendorBalanceEnq.APHistoryFilter.splitByCurrency>(e.Row);
  }

  protected virtual void APHistoryFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    this.Summary.Current.ClearSummary();
  }

  public virtual void APHistoryFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    APVendorBalanceEnq.APHistoryFilter row = (APVendorBalanceEnq.APHistoryFilter) e.Row;
    if (row == null)
      return;
    PX.Objects.GL.Company current = this.Company.Current;
    bool isVisible = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>();
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryFilter.showWithBalanceOnly>(cache, (object) row, true);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryFilter.curyID>(cache, (object) row, isVisible);
    bool flag1 = !string.IsNullOrEmpty(row.CuryID);
    bool flag2 = !string.IsNullOrEmpty(row.CuryID) && current.BaseCuryID == row.CuryID;
    bool valueOrDefault = row.SplitByCurrency.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryFilter.splitByCurrency>(cache, (object) row, isVisible && !flag1);
    PXUIFieldAttribute.SetEnabled<APVendorBalanceEnq.APHistoryFilter.splitByCurrency>(cache, (object) row, isVisible && !flag1);
    PXCache cache1 = this.History.Cache;
    bool flag3 = !isVisible | flag2 || !flag1 && !valueOrDefault;
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.curyID>(this.History.Cache, (object) null, isVisible && flag1 | valueOrDefault);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.curyBalance>(cache1, (object) null, !flag3);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.curyPayments>(cache1, (object) null, !flag3);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.curyPurchases>(cache1, (object) null, !flag3);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.curyWhTax>(cache1, (object) null, !flag3);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.curyDiscount>(cache1, (object) null, !flag3);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.curyCrAdjustments>(cache1, (object) null, !flag3);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.curyDrAdjustments>(cache1, (object) null, !flag3);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.curyDeposits>(cache1, (object) null, !flag3);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.curyDepositsBalance>(cache1, (object) null, !flag3);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.curyBegBalance>(cache1, (object) null, !flag3);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.curyEndBalance>(this.History.Cache, (object) null, !flag3);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.curyBegRetainedBalance>(cache1, (object) null, !flag3);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.curyEndRetainedBalance>(this.History.Cache, (object) null, !flag3);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.rGOL>(this.History.Cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.curyRetainageWithheld>(cache1, (object) null, !flag3);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.curyRetainageReleased>(cache1, (object) null, !flag3);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.balance>(cache1, (object) null, false);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.curyBalance>(cache1, (object) null, false);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.finPeriodID>(this.History.Cache, (object) null);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.begBalance>(this.History.Cache, (object) null);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.endBalance>(this.History.Cache, (object) null);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.begRetainedBalance>(this.History.Cache, (object) null);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistoryResult.endRetainedBalance>(this.History.Cache, (object) null);
    bool required = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleBaseCurrencies>();
    PXUIFieldAttribute.SetRequired<APVendorBalanceEnq.APHistoryFilter.orgBAccountID>(cache, required);
    bool isEnabled = required ? row.FinPeriodID != null && row.OrgBAccountID.HasValue : row.FinPeriodID != null;
    this.aPBalanceByVendorReport.SetEnabled(isEnabled);
    this.vendorHistoryReport.SetEnabled(isEnabled);
    this.aPAgedPastDueReport.SetEnabled(isEnabled);
    this.aPAgedOutstandingReport.SetEnabled(isEnabled);
  }

  public virtual void APHistorySummary_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if ((APVendorBalanceEnq.APHistorySummary) e.Row == null)
      return;
    bool isVisible = !string.IsNullOrEmpty(this.Filter.Current.CuryID) && this.Company.Current.BaseCuryID != this.Filter.Current.CuryID;
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistorySummary.curyBalanceSummary>(cache, (object) this.Summary.Current, isVisible);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistorySummary.curyDepositsSummary>(cache, (object) this.Summary.Current, isVisible);
    PXUIFieldAttribute.SetVisible<APVendorBalanceEnq.APHistorySummary.curyBalanceRetainedSummary>(cache, (object) this.Summary.Current, isVisible);
  }

  protected virtual IEnumerable RetrieveHistoryForPeriod(
    APVendorBalanceEnq.APHistoryFilter header,
    APVendorBalanceEnq.APHistorySummary summary = null)
  {
    bool flag1 = !string.IsNullOrEmpty(header.CuryID);
    bool valueOrDefault = header.SplitByCurrency.GetValueOrDefault();
    bool flag2 = !header.UseMasterCalendar.GetValueOrDefault();
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleBaseCurrencies>() && !header.OrgBAccountID.HasValue)
      return (IEnumerable) new object[0];
    System.Type type1 = (System.Type) null;
    if (header.ShowWithBalanceOnly.GetValueOrDefault() && summary == null)
      type1 = !flag2 ? typeof (PX.Data.Having<BqlChainableConditionHavingMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<FunctionWrapper<Sum<APVendorBalanceEnq.CuryAPHistoryTran.curyTranYtdBalance>>, NotEqual<FunctionWrapper<Zero>>>>>, PX.Data.Or<HavingConditionWrapper<BqlAggregatedOperand<Sum<APVendorBalanceEnq.CuryAPHistoryTran.tranYtdBalance>, IBqlDecimal>.IsNotEqual<Zero>>>>, PX.Data.Or<HavingConditionWrapper<BqlAggregatedOperand<Sum<APVendorBalanceEnq.CuryAPHistoryTran.finPtdRevalued>, IBqlDecimal>.IsNotEqual<Zero>>>>, PX.Data.Or<HavingConditionWrapper<BqlAggregatedOperand<Sum<APVendorBalanceEnq.CuryAPHistoryTran.curyTranYtdDeposits>, IBqlDecimal>.IsNotEqual<Zero>>>>>.Or<BqlAggregatedOperand<Sum<APVendorBalanceEnq.CuryAPHistoryTran.curyTranYtdRetainedBalance>, IBqlDecimal>.IsNotEqual<Zero>>>) : typeof (PX.Data.Having<BqlChainableConditionHavingMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<FunctionWrapper<Sum<APVendorBalanceEnq.CuryAPHistoryTran.curyFinYtdBalance>>, NotEqual<FunctionWrapper<Zero>>>>>, PX.Data.Or<HavingConditionWrapper<BqlAggregatedOperand<Sum<APVendorBalanceEnq.CuryAPHistoryTran.finYtdBalance>, IBqlDecimal>.IsNotEqual<Zero>>>>, PX.Data.Or<HavingConditionWrapper<BqlAggregatedOperand<Sum<APVendorBalanceEnq.CuryAPHistoryTran.finPtdRevalued>, IBqlDecimal>.IsNotEqual<Zero>>>>, PX.Data.Or<HavingConditionWrapper<BqlAggregatedOperand<Sum<APVendorBalanceEnq.CuryAPHistoryTran.curyFinYtdDeposits>, IBqlDecimal>.IsNotEqual<Zero>>>>>.Or<BqlAggregatedOperand<Sum<APVendorBalanceEnq.CuryAPHistoryTran.curyFinYtdRetainedBalance>, IBqlDecimal>.IsNotEqual<Zero>>>);
    List<System.Type> typeList = new List<System.Type>()
    {
      typeof (Select5<,,,,>),
      typeof (PX.Objects.CA.Light.Vendor),
      typeof (InnerJoin<APHistoryByPeriod, On<APHistoryByPeriod.vendorID, Equal<PX.Objects.CA.Light.Vendor.bAccountID>, PX.Data.And<Match<PX.Objects.CA.Light.Vendor, Current<AccessInfo.userName>>>>, LeftJoin<APVendorBalanceEnq.CuryAPHistoryTran, On<APHistoryByPeriod.accountID, Equal<APVendorBalanceEnq.CuryAPHistoryTran.accountID>, And<APHistoryByPeriod.branchID, Equal<APVendorBalanceEnq.CuryAPHistoryTran.branchID>, And<APHistoryByPeriod.vendorID, Equal<APVendorBalanceEnq.CuryAPHistoryTran.vendorID>, And<APHistoryByPeriod.subID, Equal<APVendorBalanceEnq.CuryAPHistoryTran.subID>, And<APHistoryByPeriod.curyID, Equal<APVendorBalanceEnq.CuryAPHistoryTran.curyID>, And<APHistoryByPeriod.lastActivityPeriod, Equal<APVendorBalanceEnq.CuryAPHistoryTran.finPeriodID>>>>>>>>>),
      typeof (Where<APHistoryByPeriod.finPeriodID, Equal<Current<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>, And<PX.Objects.CA.Light.BAccount.vOrgBAccountID, RestrictByUserBranches<Current<AccessInfo.userName>>>>),
      type1 == (System.Type) null ? typeof (PX.Data.Aggregate<>) : typeof (PX.Data.Aggregate<,>)
    };
    typeList.AddRange(BqlHelper.GetDecimalFieldsAggregate<APVendorBalanceEnq.CuryAPHistoryTran>((PXGraph) this));
    System.Type type2;
    System.Type type3;
    if (summary != null)
    {
      type2 = typeof (GroupBy<APHistoryByPeriod.curyID>);
      type3 = typeof (PX.Data.OrderBy<Asc<APHistoryByPeriod.curyID>>);
    }
    else if (!valueOrDefault)
    {
      type2 = typeof (GroupBy<PX.Objects.CA.Light.Vendor.acctCD>);
      type3 = typeof (PX.Data.OrderBy<Asc<PX.Objects.CA.Light.Vendor.acctCD>>);
    }
    else
    {
      type2 = typeof (GroupBy<PX.Objects.CA.Light.Vendor.acctCD, GroupBy<APHistoryByPeriod.curyID>>);
      type3 = typeof (PX.Data.OrderBy<Asc<PX.Objects.CA.Light.Vendor.acctCD, Asc<APHistoryByPeriod.curyID>>>);
    }
    typeList.Add(type2);
    if (type1 != (System.Type) null)
      typeList.Add(type1);
    typeList.Add(type3);
    System.Type command = BqlCommand.Compose(typeList.ToArray());
    if (!SubCDUtils.IsSubCDEmpty(header.SubID))
      command = BqlCommand.AppendJoin(command, BqlCommand.Compose(typeof (LeftJoin<PX.Objects.GL.Sub, On<APHistoryByPeriod.subID, Equal<PX.Objects.GL.Sub.subID>>>)));
    PXView pxView = new PXView((PXGraph) this, true, BqlCommand.CreateInstance(command));
    if (flag1)
      pxView.WhereAnd<Where<APHistoryByPeriod.curyID, Equal<Current<APVendorBalanceEnq.APHistoryFilter.curyID>>>>();
    if (header.OrgBAccountID.HasValue)
      pxView.WhereAnd<Where<APHistoryByPeriod.branchID, Inside<Current<APVendorBalanceEnq.APHistoryFilter.orgBAccountID>>>>();
    if (header.AccountID.HasValue)
      pxView.WhereAnd<Where<APHistoryByPeriod.accountID, Equal<Current<APVendorBalanceEnq.APHistoryFilter.accountID>>>>();
    if (!SubCDUtils.IsSubCDEmpty(header.SubID))
      pxView.WhereAnd<Where<PX.Objects.GL.Sub.subCD, Like<Current<APVendorBalanceEnq.APHistoryFilter.subCDWildcard>>>>();
    if (header.VendorClassID != null)
      pxView.WhereAnd<Where<PX.Objects.CA.Light.Vendor.vendorClassID, Equal<Current<APVendorBalanceEnq.APHistoryFilter.vendorClassID>>>>();
    if (header.VendorID.HasValue)
      pxView.WhereAnd<Where<PX.Objects.CA.Light.Vendor.bAccountID, Equal<Current<APVendorBalanceEnq.APHistoryFilter.vendorID>>>>();
    int startRow = PXView.StartRow;
    int totalRows = 0;
    PXResultMapper pxResultMapper = new PXResultMapper((PXGraph) this, flag2 ? this.mapFin : this.mapTran, new System.Type[1]
    {
      typeof (APVendorBalanceEnq.APHistoryResult)
    });
    pxResultMapper.ExtFilters.Add(typeof (APVendorBalanceEnq.CuryAPHistoryTran));
    List<object> objectList = summary == null ? pxView.Select((object[]) null, (object[]) null, PXView.Searches, pxResultMapper.SortColumns, PXView.Descendings, (PXFilterRow[]) pxResultMapper.Filters, ref startRow, PXView.MaximumRows, ref totalRows) : pxView.SelectMulti();
    PXDelegateResult delegateResult = pxResultMapper.CreateDelegateResult(summary == null);
    foreach (PXResult<PX.Objects.CA.Light.Vendor> source in objectList)
    {
      APVendorBalanceEnq.APHistoryResult result = pxResultMapper.CreateResult((PXResult) source) as APVendorBalanceEnq.APHistoryResult;
      result.RecalculateBalance();
      if (!flag1 && !valueOrDefault)
        result.CopyValueToCuryValue(this.Company.Current.BaseCuryID);
      delegateResult.Add((object) result);
      if (summary != null)
        this.Aggregate(summary, result);
    }
    PXView.StartRow = 0;
    return (IEnumerable) delegateResult;
  }

  public virtual string GetLastActivityPeriod(int? aVendorID)
  {
    return ((CuryAPHistory) new PXSelect<CuryAPHistory, Where<CuryAPHistory.vendorID, Equal<Required<CuryAPHistory.vendorID>>>, PX.Data.OrderBy<Desc<CuryAPHistory.finPeriodID>>>((PXGraph) this).View.SelectSingle((object) aVendorID))?.FinPeriodID;
  }

  protected virtual void CopyFrom(APVendorBalanceEnq.APHistoryResult aDest, PX.Objects.CA.Light.Vendor aVendor)
  {
    aDest.AcctCD = aVendor.AcctCD;
    aDest.AcctName = aVendor.AcctName;
    aDest.CuryID = aVendor.CuryID;
    aDest.VendorID = aVendor.BAccountID;
    aDest.NoteID = aVendor.NoteID;
  }

  protected virtual void CopyFrom(
    APVendorBalanceEnq.APHistoryResult aDest,
    APVendorBalanceEnq.CuryAPHistoryTran aHistory,
    bool aIsFinancial)
  {
    if (aIsFinancial)
    {
      aDest.CuryBegBalance = new Decimal?(aHistory.CuryFinBegBalance.GetValueOrDefault());
      aDest.CuryPurchases = new Decimal?(aHistory.CuryFinPtdPurchases.GetValueOrDefault());
      aDest.CuryPayments = new Decimal?(aHistory.CuryFinPtdPayments.GetValueOrDefault());
      aDest.CuryDiscount = new Decimal?(aHistory.CuryFinPtdDiscTaken.GetValueOrDefault());
      aDest.CuryWhTax = new Decimal?(aHistory.CuryFinPtdWhTax.GetValueOrDefault());
      aDest.CuryCrAdjustments = new Decimal?(aHistory.CuryFinPtdCrAdjustments.GetValueOrDefault());
      aDest.CuryDrAdjustments = new Decimal?(aHistory.CuryFinPtdDrAdjustments.GetValueOrDefault());
      aDest.CuryDeposits = new Decimal?(aHistory.CuryFinPtdDeposits.GetValueOrDefault());
      APVendorBalanceEnq.APHistoryResult apHistoryResult1 = aDest;
      Decimal? curyFinYtdDeposits = aHistory.CuryFinYtdDeposits;
      Decimal? nullable1 = curyFinYtdDeposits.HasValue ? new Decimal?(-curyFinYtdDeposits.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable2 = new Decimal?(nullable1.GetValueOrDefault());
      apHistoryResult1.CuryDepositsBalance = nullable2;
      APVendorBalanceEnq.APHistoryResult apHistoryResult2 = aDest;
      Decimal? nullable3 = aHistory.CuryFinPtdRetainageWithheld;
      Decimal? nullable4 = new Decimal?(nullable3.GetValueOrDefault());
      apHistoryResult2.CuryRetainageWithheld = nullable4;
      APVendorBalanceEnq.APHistoryResult apHistoryResult3 = aDest;
      nullable3 = aHistory.CuryFinPtdRetainageReleased;
      Decimal? nullable5 = new Decimal?(nullable3.GetValueOrDefault());
      apHistoryResult3.CuryRetainageReleased = nullable5;
      APVendorBalanceEnq.APHistoryResult apHistoryResult4 = aDest;
      nullable3 = aHistory.CuryFinYtdRetainageWithheld;
      Decimal valueOrDefault1 = nullable3.GetValueOrDefault();
      nullable3 = aHistory.CuryFinYtdRetainageReleased;
      Decimal valueOrDefault2 = nullable3.GetValueOrDefault();
      Decimal num1 = valueOrDefault1 - valueOrDefault2;
      nullable3 = aHistory.CuryFinPtdRetainageWithheld;
      Decimal valueOrDefault3 = nullable3.GetValueOrDefault();
      nullable3 = aHistory.CuryFinPtdRetainageReleased;
      Decimal valueOrDefault4 = nullable3.GetValueOrDefault();
      Decimal num2 = valueOrDefault3 - valueOrDefault4;
      Decimal? nullable6 = new Decimal?(num1 - num2);
      apHistoryResult4.CuryBegRetainedBalance = nullable6;
      APVendorBalanceEnq.APHistoryResult apHistoryResult5 = aDest;
      nullable3 = aHistory.CuryFinYtdRetainageWithheld;
      Decimal valueOrDefault5 = nullable3.GetValueOrDefault();
      nullable3 = aHistory.CuryFinYtdRetainageReleased;
      Decimal valueOrDefault6 = nullable3.GetValueOrDefault();
      Decimal? nullable7 = new Decimal?(valueOrDefault5 - valueOrDefault6);
      apHistoryResult5.CuryEndRetainedBalance = nullable7;
      APVendorBalanceEnq.APHistoryResult apHistoryResult6 = aDest;
      nullable3 = aHistory.FinBegBalance;
      Decimal? nullable8 = new Decimal?(nullable3.GetValueOrDefault());
      apHistoryResult6.BegBalance = nullable8;
      APVendorBalanceEnq.APHistoryResult apHistoryResult7 = aDest;
      nullable3 = aHistory.FinPtdPurchases;
      Decimal? nullable9 = new Decimal?(nullable3.GetValueOrDefault());
      apHistoryResult7.Purchases = nullable9;
      APVendorBalanceEnq.APHistoryResult apHistoryResult8 = aDest;
      nullable3 = aHistory.FinPtdPayments;
      Decimal? nullable10 = new Decimal?(nullable3.GetValueOrDefault());
      apHistoryResult8.Payments = nullable10;
      APVendorBalanceEnq.APHistoryResult apHistoryResult9 = aDest;
      nullable3 = aHistory.FinPtdDiscTaken;
      Decimal? nullable11 = new Decimal?(nullable3.GetValueOrDefault());
      apHistoryResult9.Discount = nullable11;
      APVendorBalanceEnq.APHistoryResult apHistoryResult10 = aDest;
      nullable3 = aHistory.FinPtdWhTax;
      Decimal? nullable12 = new Decimal?(nullable3.GetValueOrDefault());
      apHistoryResult10.WhTax = nullable12;
      APVendorBalanceEnq.APHistoryResult apHistoryResult11 = aDest;
      nullable3 = aHistory.FinPtdRGOL;
      Decimal? nullable13;
      if (!nullable3.HasValue)
      {
        nullable1 = new Decimal?();
        nullable13 = nullable1;
      }
      else
        nullable13 = new Decimal?(-nullable3.GetValueOrDefault());
      nullable1 = nullable13;
      Decimal? nullable14 = new Decimal?(nullable1.GetValueOrDefault());
      apHistoryResult11.RGOL = nullable14;
      APVendorBalanceEnq.APHistoryResult apHistoryResult12 = aDest;
      nullable3 = aHistory.FinPtdCrAdjustments;
      Decimal? nullable15 = new Decimal?(nullable3.GetValueOrDefault());
      apHistoryResult12.CrAdjustments = nullable15;
      APVendorBalanceEnq.APHistoryResult apHistoryResult13 = aDest;
      nullable3 = aHistory.FinPtdDrAdjustments;
      Decimal? nullable16 = new Decimal?(nullable3.GetValueOrDefault());
      apHistoryResult13.DrAdjustments = nullable16;
      APVendorBalanceEnq.APHistoryResult apHistoryResult14 = aDest;
      nullable3 = aHistory.FinPtdDeposits;
      Decimal? nullable17 = new Decimal?(nullable3.GetValueOrDefault());
      apHistoryResult14.Deposits = nullable17;
      APVendorBalanceEnq.APHistoryResult apHistoryResult15 = aDest;
      nullable3 = aHistory.FinYtdDeposits;
      Decimal? nullable18;
      if (!nullable3.HasValue)
      {
        nullable1 = new Decimal?();
        nullable18 = nullable1;
      }
      else
        nullable18 = new Decimal?(-nullable3.GetValueOrDefault());
      nullable1 = nullable18;
      Decimal? nullable19 = new Decimal?(nullable1.GetValueOrDefault());
      apHistoryResult15.DepositsBalance = nullable19;
      APVendorBalanceEnq.APHistoryResult apHistoryResult16 = aDest;
      nullable3 = aHistory.FinPtdRetainageWithheld;
      Decimal? nullable20 = new Decimal?(nullable3.GetValueOrDefault());
      apHistoryResult16.RetainageWithheld = nullable20;
      APVendorBalanceEnq.APHistoryResult apHistoryResult17 = aDest;
      nullable3 = aHistory.FinPtdRetainageReleased;
      Decimal? nullable21 = new Decimal?(nullable3.GetValueOrDefault());
      apHistoryResult17.RetainageReleased = nullable21;
      APVendorBalanceEnq.APHistoryResult apHistoryResult18 = aDest;
      nullable3 = aHistory.FinYtdRetainageWithheld;
      Decimal valueOrDefault7 = nullable3.GetValueOrDefault();
      nullable3 = aHistory.FinYtdRetainageReleased;
      Decimal valueOrDefault8 = nullable3.GetValueOrDefault();
      Decimal num3 = valueOrDefault7 - valueOrDefault8;
      nullable3 = aHistory.FinPtdRetainageWithheld;
      Decimal valueOrDefault9 = nullable3.GetValueOrDefault();
      nullable3 = aHistory.FinPtdRetainageReleased;
      Decimal valueOrDefault10 = nullable3.GetValueOrDefault();
      Decimal num4 = valueOrDefault9 - valueOrDefault10;
      Decimal? nullable22 = new Decimal?(num3 - num4);
      apHistoryResult18.BegRetainedBalance = nullable22;
      APVendorBalanceEnq.APHistoryResult apHistoryResult19 = aDest;
      nullable3 = aHistory.FinYtdRetainageWithheld;
      Decimal valueOrDefault11 = nullable3.GetValueOrDefault();
      nullable3 = aHistory.FinYtdRetainageReleased;
      Decimal valueOrDefault12 = nullable3.GetValueOrDefault();
      Decimal? nullable23 = new Decimal?(valueOrDefault11 - valueOrDefault12);
      apHistoryResult19.EndRetainedBalance = nullable23;
      aDest.CuryID = aHistory.CuryID;
    }
    else
    {
      aDest.CuryBegBalance = new Decimal?(aHistory.CuryTranBegBalance.GetValueOrDefault());
      aDest.CuryPurchases = new Decimal?(aHistory.CuryTranPtdPurchases.GetValueOrDefault());
      aDest.CuryPayments = new Decimal?(aHistory.CuryTranPtdPayments.GetValueOrDefault());
      aDest.CuryDiscount = new Decimal?(aHistory.CuryTranPtdDiscTaken.GetValueOrDefault());
      aDest.CuryWhTax = new Decimal?(aHistory.CuryTranPtdWhTax.GetValueOrDefault());
      aDest.CuryCrAdjustments = new Decimal?(aHistory.CuryTranPtdCrAdjustments.GetValueOrDefault());
      aDest.CuryDrAdjustments = new Decimal?(aHistory.CuryTranPtdDrAdjustments.GetValueOrDefault());
      aDest.CuryDeposits = new Decimal?(aHistory.CuryTranPtdDeposits.GetValueOrDefault());
      APVendorBalanceEnq.APHistoryResult apHistoryResult20 = aDest;
      Decimal? curyTranYtdDeposits = aHistory.CuryTranYtdDeposits;
      Decimal? nullable24 = curyTranYtdDeposits.HasValue ? new Decimal?(-curyTranYtdDeposits.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable25 = new Decimal?(nullable24.GetValueOrDefault());
      apHistoryResult20.CuryDepositsBalance = nullable25;
      APVendorBalanceEnq.APHistoryResult apHistoryResult21 = aDest;
      Decimal? nullable26 = aHistory.CuryTranPtdRetainageWithheld;
      Decimal? nullable27 = new Decimal?(nullable26.GetValueOrDefault());
      apHistoryResult21.CuryRetainageWithheld = nullable27;
      APVendorBalanceEnq.APHistoryResult apHistoryResult22 = aDest;
      nullable26 = aHistory.CuryTranPtdRetainageReleased;
      Decimal? nullable28 = new Decimal?(nullable26.GetValueOrDefault());
      apHistoryResult22.CuryRetainageReleased = nullable28;
      APVendorBalanceEnq.APHistoryResult apHistoryResult23 = aDest;
      nullable26 = aHistory.CuryTranYtdRetainageWithheld;
      Decimal valueOrDefault13 = nullable26.GetValueOrDefault();
      nullable26 = aHistory.CuryTranYtdRetainageReleased;
      Decimal valueOrDefault14 = nullable26.GetValueOrDefault();
      Decimal num5 = valueOrDefault13 - valueOrDefault14;
      nullable26 = aHistory.CuryTranPtdRetainageWithheld;
      Decimal valueOrDefault15 = nullable26.GetValueOrDefault();
      nullable26 = aHistory.CuryTranPtdRetainageReleased;
      Decimal valueOrDefault16 = nullable26.GetValueOrDefault();
      Decimal num6 = valueOrDefault15 - valueOrDefault16;
      Decimal? nullable29 = new Decimal?(num5 - num6);
      apHistoryResult23.CuryBegRetainedBalance = nullable29;
      APVendorBalanceEnq.APHistoryResult apHistoryResult24 = aDest;
      nullable26 = aHistory.CuryTranYtdRetainageWithheld;
      Decimal valueOrDefault17 = nullable26.GetValueOrDefault();
      nullable26 = aHistory.CuryTranYtdRetainageReleased;
      Decimal valueOrDefault18 = nullable26.GetValueOrDefault();
      Decimal? nullable30 = new Decimal?(valueOrDefault17 - valueOrDefault18);
      apHistoryResult24.CuryEndRetainedBalance = nullable30;
      APVendorBalanceEnq.APHistoryResult apHistoryResult25 = aDest;
      nullable26 = aHistory.TranBegBalance;
      Decimal? nullable31 = new Decimal?(nullable26.GetValueOrDefault());
      apHistoryResult25.BegBalance = nullable31;
      APVendorBalanceEnq.APHistoryResult apHistoryResult26 = aDest;
      nullable26 = aHistory.TranPtdPurchases;
      Decimal? nullable32 = new Decimal?(nullable26.GetValueOrDefault());
      apHistoryResult26.Purchases = nullable32;
      APVendorBalanceEnq.APHistoryResult apHistoryResult27 = aDest;
      nullable26 = aHistory.TranPtdPayments;
      Decimal? nullable33 = new Decimal?(nullable26.GetValueOrDefault());
      apHistoryResult27.Payments = nullable33;
      APVendorBalanceEnq.APHistoryResult apHistoryResult28 = aDest;
      nullable26 = aHistory.TranPtdDiscTaken;
      Decimal? nullable34 = new Decimal?(nullable26.GetValueOrDefault());
      apHistoryResult28.Discount = nullable34;
      APVendorBalanceEnq.APHistoryResult apHistoryResult29 = aDest;
      nullable26 = aHistory.TranPtdWhTax;
      Decimal? nullable35 = new Decimal?(nullable26.GetValueOrDefault());
      apHistoryResult29.WhTax = nullable35;
      APVendorBalanceEnq.APHistoryResult apHistoryResult30 = aDest;
      nullable26 = aHistory.TranPtdRGOL;
      Decimal? nullable36;
      if (!nullable26.HasValue)
      {
        nullable24 = new Decimal?();
        nullable36 = nullable24;
      }
      else
        nullable36 = new Decimal?(-nullable26.GetValueOrDefault());
      nullable24 = nullable36;
      Decimal? nullable37 = new Decimal?(nullable24.GetValueOrDefault());
      apHistoryResult30.RGOL = nullable37;
      APVendorBalanceEnq.APHistoryResult apHistoryResult31 = aDest;
      nullable26 = aHistory.TranPtdCrAdjustments;
      Decimal? nullable38 = new Decimal?(nullable26.GetValueOrDefault());
      apHistoryResult31.CrAdjustments = nullable38;
      APVendorBalanceEnq.APHistoryResult apHistoryResult32 = aDest;
      nullable26 = aHistory.TranPtdDrAdjustments;
      Decimal? nullable39 = new Decimal?(nullable26.GetValueOrDefault());
      apHistoryResult32.DrAdjustments = nullable39;
      APVendorBalanceEnq.APHistoryResult apHistoryResult33 = aDest;
      nullable26 = aHistory.TranPtdDeposits;
      Decimal? nullable40 = new Decimal?(nullable26.GetValueOrDefault());
      apHistoryResult33.Deposits = nullable40;
      APVendorBalanceEnq.APHistoryResult apHistoryResult34 = aDest;
      nullable26 = aHistory.TranYtdDeposits;
      Decimal? nullable41;
      if (!nullable26.HasValue)
      {
        nullable24 = new Decimal?();
        nullable41 = nullable24;
      }
      else
        nullable41 = new Decimal?(-nullable26.GetValueOrDefault());
      nullable24 = nullable41;
      Decimal? nullable42 = new Decimal?(nullable24.GetValueOrDefault());
      apHistoryResult34.DepositsBalance = nullable42;
      APVendorBalanceEnq.APHistoryResult apHistoryResult35 = aDest;
      nullable26 = aHistory.TranPtdRetainageWithheld;
      Decimal? nullable43 = new Decimal?(nullable26.GetValueOrDefault());
      apHistoryResult35.RetainageWithheld = nullable43;
      APVendorBalanceEnq.APHistoryResult apHistoryResult36 = aDest;
      nullable26 = aHistory.TranPtdRetainageReleased;
      Decimal? nullable44 = new Decimal?(nullable26.GetValueOrDefault());
      apHistoryResult36.RetainageReleased = nullable44;
      APVendorBalanceEnq.APHistoryResult apHistoryResult37 = aDest;
      nullable26 = aHistory.TranYtdRetainageWithheld;
      Decimal valueOrDefault19 = nullable26.GetValueOrDefault();
      nullable26 = aHistory.TranYtdRetainageReleased;
      Decimal valueOrDefault20 = nullable26.GetValueOrDefault();
      Decimal num7 = valueOrDefault19 - valueOrDefault20;
      nullable26 = aHistory.TranPtdRetainageWithheld;
      Decimal valueOrDefault21 = nullable26.GetValueOrDefault();
      nullable26 = aHistory.TranPtdRetainageReleased;
      Decimal valueOrDefault22 = nullable26.GetValueOrDefault();
      Decimal num8 = valueOrDefault21 - valueOrDefault22;
      Decimal? nullable45 = new Decimal?(num7 - num8);
      apHistoryResult37.BegRetainedBalance = nullable45;
      APVendorBalanceEnq.APHistoryResult apHistoryResult38 = aDest;
      nullable26 = aHistory.TranYtdRetainageWithheld;
      Decimal valueOrDefault23 = nullable26.GetValueOrDefault();
      nullable26 = aHistory.TranYtdRetainageReleased;
      Decimal valueOrDefault24 = nullable26.GetValueOrDefault();
      Decimal? nullable46 = new Decimal?(valueOrDefault23 - valueOrDefault24);
      apHistoryResult38.EndRetainedBalance = nullable46;
      aDest.CuryID = aHistory.CuryID;
    }
    aDest.RecalculateEndBalance();
  }

  protected virtual void Aggregate(
    APVendorBalanceEnq.APHistoryResult aDest,
    APVendorBalanceEnq.APHistoryResult aSrc)
  {
    APVendorBalanceEnq.APHistoryResult apHistoryResult1 = aDest;
    Decimal? nullable = apHistoryResult1.CuryBegBalance;
    Decimal valueOrDefault1 = aSrc.CuryBegBalance.GetValueOrDefault();
    apHistoryResult1.CuryBegBalance = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult2 = aDest;
    nullable = apHistoryResult2.CuryCrAdjustments;
    Decimal valueOrDefault2 = aSrc.CuryCrAdjustments.GetValueOrDefault();
    apHistoryResult2.CuryCrAdjustments = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult3 = aDest;
    nullable = apHistoryResult3.CuryDrAdjustments;
    Decimal valueOrDefault3 = aSrc.CuryDrAdjustments.GetValueOrDefault();
    apHistoryResult3.CuryDrAdjustments = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault3) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult4 = aDest;
    nullable = apHistoryResult4.CuryDiscount;
    Decimal valueOrDefault4 = aSrc.CuryDiscount.GetValueOrDefault();
    apHistoryResult4.CuryDiscount = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault4) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult5 = aDest;
    nullable = apHistoryResult5.CuryWhTax;
    Decimal valueOrDefault5 = aSrc.CuryWhTax.GetValueOrDefault();
    apHistoryResult5.CuryWhTax = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault5) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult6 = aDest;
    nullable = apHistoryResult6.CuryPurchases;
    Decimal valueOrDefault6 = aSrc.CuryPurchases.GetValueOrDefault();
    apHistoryResult6.CuryPurchases = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault6) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult7 = aDest;
    nullable = apHistoryResult7.CuryPayments;
    Decimal valueOrDefault7 = aSrc.CuryPayments.GetValueOrDefault();
    apHistoryResult7.CuryPayments = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault7) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult8 = aDest;
    nullable = apHistoryResult8.CuryDeposits;
    Decimal valueOrDefault8 = aSrc.CuryDeposits.GetValueOrDefault();
    apHistoryResult8.CuryDeposits = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault8) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult9 = aDest;
    nullable = apHistoryResult9.CuryDepositsBalance;
    Decimal valueOrDefault9 = aSrc.CuryDepositsBalance.GetValueOrDefault();
    apHistoryResult9.CuryDepositsBalance = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault9) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult10 = aDest;
    nullable = apHistoryResult10.CuryRetainageWithheld;
    Decimal valueOrDefault10 = aSrc.CuryRetainageWithheld.GetValueOrDefault();
    apHistoryResult10.CuryRetainageWithheld = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault10) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult11 = aDest;
    nullable = apHistoryResult11.CuryRetainageReleased;
    Decimal valueOrDefault11 = aSrc.CuryRetainageReleased.GetValueOrDefault();
    apHistoryResult11.CuryRetainageReleased = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault11) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult12 = aDest;
    nullable = apHistoryResult12.CuryBegRetainedBalance;
    Decimal valueOrDefault12 = aSrc.CuryBegRetainedBalance.GetValueOrDefault();
    apHistoryResult12.CuryBegRetainedBalance = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault12) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult13 = aDest;
    nullable = apHistoryResult13.CuryEndRetainedBalance;
    Decimal valueOrDefault13 = aSrc.CuryEndRetainedBalance.GetValueOrDefault();
    apHistoryResult13.CuryEndRetainedBalance = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault13) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult14 = aDest;
    nullable = apHistoryResult14.BegBalance;
    Decimal valueOrDefault14 = aSrc.BegBalance.GetValueOrDefault();
    apHistoryResult14.BegBalance = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault14) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult15 = aDest;
    nullable = apHistoryResult15.CrAdjustments;
    Decimal valueOrDefault15 = aSrc.CrAdjustments.GetValueOrDefault();
    apHistoryResult15.CrAdjustments = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault15) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult16 = aDest;
    nullable = apHistoryResult16.DrAdjustments;
    Decimal valueOrDefault16 = aSrc.DrAdjustments.GetValueOrDefault();
    apHistoryResult16.DrAdjustments = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault16) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult17 = aDest;
    nullable = apHistoryResult17.Discount;
    Decimal valueOrDefault17 = aSrc.Discount.GetValueOrDefault();
    apHistoryResult17.Discount = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault17) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult18 = aDest;
    nullable = apHistoryResult18.WhTax;
    Decimal valueOrDefault18 = aSrc.WhTax.GetValueOrDefault();
    apHistoryResult18.WhTax = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault18) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult19 = aDest;
    nullable = apHistoryResult19.Purchases;
    Decimal valueOrDefault19 = aSrc.Purchases.GetValueOrDefault();
    apHistoryResult19.Purchases = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault19) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult20 = aDest;
    nullable = apHistoryResult20.Payments;
    Decimal valueOrDefault20 = aSrc.Payments.GetValueOrDefault();
    apHistoryResult20.Payments = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault20) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult21 = aDest;
    nullable = apHistoryResult21.RGOL;
    Decimal valueOrDefault21 = aSrc.RGOL.GetValueOrDefault();
    apHistoryResult21.RGOL = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault21) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult22 = aDest;
    nullable = apHistoryResult22.Deposits;
    Decimal valueOrDefault22 = aSrc.Deposits.GetValueOrDefault();
    apHistoryResult22.Deposits = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault22) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult23 = aDest;
    nullable = apHistoryResult23.DepositsBalance;
    Decimal valueOrDefault23 = aSrc.DepositsBalance.GetValueOrDefault();
    apHistoryResult23.DepositsBalance = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault23) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult24 = aDest;
    nullable = apHistoryResult24.RetainageWithheld;
    Decimal valueOrDefault24 = aSrc.RetainageWithheld.GetValueOrDefault();
    apHistoryResult24.RetainageWithheld = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault24) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult25 = aDest;
    nullable = apHistoryResult25.RetainageReleased;
    Decimal valueOrDefault25 = aSrc.RetainageReleased.GetValueOrDefault();
    apHistoryResult25.RetainageReleased = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault25) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult26 = aDest;
    nullable = apHistoryResult26.BegRetainedBalance;
    Decimal valueOrDefault26 = aSrc.BegRetainedBalance.GetValueOrDefault();
    apHistoryResult26.BegRetainedBalance = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault26) : new Decimal?();
    APVendorBalanceEnq.APHistoryResult apHistoryResult27 = aDest;
    nullable = apHistoryResult27.EndRetainedBalance;
    Decimal valueOrDefault27 = aSrc.EndRetainedBalance.GetValueOrDefault();
    apHistoryResult27.EndRetainedBalance = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault27) : new Decimal?();
    aDest.RecalculateEndBalance();
  }

  protected virtual void Aggregate(
    APVendorBalanceEnq.APHistorySummary aDest,
    APVendorBalanceEnq.APHistoryResult aSrc)
  {
    APVendorBalanceEnq.APHistorySummary apHistorySummary1 = aDest;
    Decimal? nullable = apHistorySummary1.CuryBalanceSummary;
    Decimal valueOrDefault1 = aSrc.CuryEndBalance.GetValueOrDefault();
    apHistorySummary1.CuryBalanceSummary = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
    APVendorBalanceEnq.APHistorySummary apHistorySummary2 = aDest;
    nullable = apHistorySummary2.CuryDepositsSummary;
    Decimal valueOrDefault2 = aSrc.CuryDepositsBalance.GetValueOrDefault();
    apHistorySummary2.CuryDepositsSummary = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
    APVendorBalanceEnq.APHistorySummary apHistorySummary3 = aDest;
    nullable = apHistorySummary3.BalanceSummary;
    Decimal valueOrDefault3 = aSrc.EndBalance.GetValueOrDefault();
    apHistorySummary3.BalanceSummary = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault3) : new Decimal?();
    APVendorBalanceEnq.APHistorySummary apHistorySummary4 = aDest;
    nullable = apHistorySummary4.DepositsSummary;
    Decimal valueOrDefault4 = aSrc.DepositsBalance.GetValueOrDefault();
    apHistorySummary4.DepositsSummary = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault4) : new Decimal?();
    APVendorBalanceEnq.APHistorySummary apHistorySummary5 = aDest;
    nullable = apHistorySummary5.BalanceRetainedSummary;
    Decimal valueOrDefault5 = aSrc.EndRetainedBalance.GetValueOrDefault();
    apHistorySummary5.BalanceRetainedSummary = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault5) : new Decimal?();
    APVendorBalanceEnq.APHistorySummary apHistorySummary6 = aDest;
    nullable = apHistorySummary6.CuryBalanceRetainedSummary;
    Decimal valueOrDefault6 = aSrc.CuryEndRetainedBalance.GetValueOrDefault();
    apHistorySummary6.CuryBalanceRetainedSummary = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault6) : new Decimal?();
  }

  protected virtual void AggregateLatest(
    APVendorBalanceEnq.APHistoryResult aDest,
    APVendorBalanceEnq.APHistoryResult aSrc)
  {
    if (aSrc.FinPeriodID == aDest.FinPeriodID)
    {
      this.Aggregate(aDest, aSrc);
    }
    else
    {
      if (string.Compare(aSrc.FinPeriodID, aDest.FinPeriodID) < 0)
      {
        APVendorBalanceEnq.APHistoryResult apHistoryResult1 = aDest;
        Decimal? nullable1 = apHistoryResult1.BegBalance;
        Decimal valueOrDefault1 = aSrc.EndBalance.GetValueOrDefault();
        apHistoryResult1.BegBalance = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
        APVendorBalanceEnq.APHistoryResult apHistoryResult2 = aDest;
        nullable1 = apHistoryResult2.DepositsBalance;
        Decimal valueOrDefault2 = aSrc.DepositsBalance.GetValueOrDefault();
        apHistoryResult2.DepositsBalance = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
        APVendorBalanceEnq.APHistoryResult apHistoryResult3 = aDest;
        nullable1 = apHistoryResult3.BegRetainedBalance;
        Decimal valueOrDefault3 = aSrc.EndRetainedBalance.GetValueOrDefault();
        apHistoryResult3.BegRetainedBalance = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault3) : new Decimal?();
        APVendorBalanceEnq.APHistoryResult apHistoryResult4 = aDest;
        Decimal? begRetainedBalance = aDest.BegRetainedBalance;
        Decimal? nullable2 = aSrc.EndRetainedBalance;
        nullable1 = begRetainedBalance.HasValue & nullable2.HasValue ? new Decimal?(begRetainedBalance.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable3;
        Decimal valueOrDefault4;
        if (!nullable1.HasValue)
        {
          nullable2 = aSrc.BegRetainedBalance;
          nullable3 = nullable2.HasValue ? new Decimal?(0M - nullable2.GetValueOrDefault()) : new Decimal?();
          valueOrDefault4 = nullable3.GetValueOrDefault();
        }
        else
          valueOrDefault4 = nullable1.GetValueOrDefault();
        Decimal? nullable4 = new Decimal?(valueOrDefault4);
        apHistoryResult4.EndRetainedBalance = nullable4;
        APVendorBalanceEnq.APHistoryResult apHistoryResult5 = aDest;
        nullable2 = apHistoryResult5.CuryBegBalance;
        nullable1 = aSrc.CuryEndBalance;
        Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
        Decimal? nullable5;
        if (!nullable2.HasValue)
        {
          nullable1 = new Decimal?();
          nullable5 = nullable1;
        }
        else
          nullable5 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault5);
        apHistoryResult5.CuryBegBalance = nullable5;
        APVendorBalanceEnq.APHistoryResult apHistoryResult6 = aDest;
        nullable2 = apHistoryResult6.CuryDepositsBalance;
        nullable1 = aSrc.CuryDepositsBalance;
        Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
        Decimal? nullable6;
        if (!nullable2.HasValue)
        {
          nullable1 = new Decimal?();
          nullable6 = nullable1;
        }
        else
          nullable6 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault6);
        apHistoryResult6.CuryDepositsBalance = nullable6;
        APVendorBalanceEnq.APHistoryResult apHistoryResult7 = aDest;
        nullable2 = apHistoryResult7.CuryBegRetainedBalance;
        nullable1 = aSrc.CuryEndRetainedBalance;
        Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
        Decimal? nullable7;
        if (!nullable2.HasValue)
        {
          nullable1 = new Decimal?();
          nullable7 = nullable1;
        }
        else
          nullable7 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault7);
        apHistoryResult7.CuryBegRetainedBalance = nullable7;
        APVendorBalanceEnq.APHistoryResult apHistoryResult8 = aDest;
        nullable1 = aDest.CuryBegRetainedBalance;
        nullable3 = aSrc.CuryEndRetainedBalance;
        nullable2 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
        Decimal valueOrDefault8;
        if (!nullable2.HasValue)
        {
          nullable3 = aSrc.CuryBegRetainedBalance;
          Decimal? nullable8;
          if (!nullable3.HasValue)
          {
            nullable1 = new Decimal?();
            nullable8 = nullable1;
          }
          else
            nullable8 = new Decimal?(0M - nullable3.GetValueOrDefault());
          nullable1 = nullable8;
          valueOrDefault8 = nullable1.GetValueOrDefault();
        }
        else
          valueOrDefault8 = nullable2.GetValueOrDefault();
        Decimal? nullable9 = new Decimal?(valueOrDefault8);
        apHistoryResult8.CuryEndRetainedBalance = nullable9;
      }
      else
      {
        APVendorBalanceEnq.APHistoryResult apHistoryResult9 = aDest;
        Decimal valueOrDefault9 = aDest.EndBalance.GetValueOrDefault();
        Decimal? nullable10 = aSrc.BegBalance;
        Decimal valueOrDefault10 = nullable10.GetValueOrDefault();
        Decimal? nullable11 = new Decimal?(valueOrDefault9 + valueOrDefault10);
        apHistoryResult9.BegBalance = nullable11;
        APVendorBalanceEnq.APHistoryResult apHistoryResult10 = aDest;
        nullable10 = aSrc.CrAdjustments;
        Decimal? nullable12 = new Decimal?(nullable10.GetValueOrDefault());
        apHistoryResult10.CrAdjustments = nullable12;
        APVendorBalanceEnq.APHistoryResult apHistoryResult11 = aDest;
        nullable10 = aSrc.DrAdjustments;
        Decimal? nullable13 = new Decimal?(nullable10.GetValueOrDefault());
        apHistoryResult11.DrAdjustments = nullable13;
        APVendorBalanceEnq.APHistoryResult apHistoryResult12 = aDest;
        nullable10 = aSrc.Discount;
        Decimal? nullable14 = new Decimal?(nullable10.GetValueOrDefault());
        apHistoryResult12.Discount = nullable14;
        APVendorBalanceEnq.APHistoryResult apHistoryResult13 = aDest;
        nullable10 = aSrc.WhTax;
        Decimal? nullable15 = new Decimal?(nullable10.GetValueOrDefault());
        apHistoryResult13.WhTax = nullable15;
        APVendorBalanceEnq.APHistoryResult apHistoryResult14 = aDest;
        nullable10 = aSrc.Purchases;
        Decimal? nullable16 = new Decimal?(nullable10.GetValueOrDefault());
        apHistoryResult14.Purchases = nullable16;
        APVendorBalanceEnq.APHistoryResult apHistoryResult15 = aDest;
        nullable10 = aSrc.Payments;
        Decimal? nullable17 = new Decimal?(nullable10.GetValueOrDefault());
        apHistoryResult15.Payments = nullable17;
        APVendorBalanceEnq.APHistoryResult apHistoryResult16 = aDest;
        nullable10 = aSrc.RGOL;
        Decimal? nullable18 = new Decimal?(nullable10.GetValueOrDefault());
        apHistoryResult16.RGOL = nullable18;
        aDest.FinPeriodID = aSrc.FinPeriodID;
        aDest.Deposits = aSrc.Deposits;
        APVendorBalanceEnq.APHistoryResult apHistoryResult17 = aDest;
        nullable10 = aDest.DepositsBalance;
        Decimal valueOrDefault11 = nullable10.GetValueOrDefault();
        nullable10 = aSrc.DepositsBalance;
        Decimal valueOrDefault12 = nullable10.GetValueOrDefault();
        Decimal? nullable19 = new Decimal?(valueOrDefault11 + valueOrDefault12);
        apHistoryResult17.DepositsBalance = nullable19;
        APVendorBalanceEnq.APHistoryResult apHistoryResult18 = aDest;
        nullable10 = aSrc.RetainageWithheld;
        Decimal? nullable20 = new Decimal?(nullable10.GetValueOrDefault());
        apHistoryResult18.RetainageWithheld = nullable20;
        APVendorBalanceEnq.APHistoryResult apHistoryResult19 = aDest;
        nullable10 = aSrc.RetainageReleased;
        Decimal? nullable21 = new Decimal?(nullable10.GetValueOrDefault());
        apHistoryResult19.RetainageReleased = nullable21;
        APVendorBalanceEnq.APHistoryResult apHistoryResult20 = aDest;
        nullable10 = aDest.EndRetainedBalance;
        Decimal valueOrDefault13 = nullable10.GetValueOrDefault();
        nullable10 = aSrc.BegRetainedBalance;
        Decimal valueOrDefault14 = nullable10.GetValueOrDefault();
        Decimal? nullable22 = new Decimal?(valueOrDefault13 + valueOrDefault14);
        apHistoryResult20.BegRetainedBalance = nullable22;
        APVendorBalanceEnq.APHistoryResult apHistoryResult21 = aDest;
        Decimal? nullable23 = aDest.BegRetainedBalance;
        Decimal? nullable24 = aSrc.EndRetainedBalance;
        nullable10 = nullable23.HasValue & nullable24.HasValue ? new Decimal?(nullable23.GetValueOrDefault() + nullable24.GetValueOrDefault()) : new Decimal?();
        Decimal valueOrDefault15;
        if (!nullable10.HasValue)
        {
          nullable24 = aSrc.BegRetainedBalance;
          Decimal? nullable25;
          if (!nullable24.HasValue)
          {
            nullable23 = new Decimal?();
            nullable25 = nullable23;
          }
          else
            nullable25 = new Decimal?(0M - nullable24.GetValueOrDefault());
          nullable23 = nullable25;
          valueOrDefault15 = nullable23.GetValueOrDefault();
        }
        else
          valueOrDefault15 = nullable10.GetValueOrDefault();
        Decimal? nullable26 = new Decimal?(valueOrDefault15);
        apHistoryResult21.EndRetainedBalance = nullable26;
        APVendorBalanceEnq.APHistoryResult apHistoryResult22 = aDest;
        nullable24 = aDest.CuryEndBalance;
        Decimal valueOrDefault16 = nullable24.GetValueOrDefault();
        nullable24 = aSrc.CuryBegBalance;
        Decimal valueOrDefault17 = nullable24.GetValueOrDefault();
        Decimal? nullable27 = new Decimal?(valueOrDefault16 + valueOrDefault17);
        apHistoryResult22.CuryBegBalance = nullable27;
        APVendorBalanceEnq.APHistoryResult apHistoryResult23 = aDest;
        nullable24 = aSrc.CuryCrAdjustments;
        Decimal? nullable28 = new Decimal?(nullable24.GetValueOrDefault());
        apHistoryResult23.CuryCrAdjustments = nullable28;
        APVendorBalanceEnq.APHistoryResult apHistoryResult24 = aDest;
        nullable24 = aSrc.CuryDrAdjustments;
        Decimal? nullable29 = new Decimal?(nullable24.GetValueOrDefault());
        apHistoryResult24.CuryDrAdjustments = nullable29;
        APVendorBalanceEnq.APHistoryResult apHistoryResult25 = aDest;
        nullable24 = aSrc.CuryDiscount;
        Decimal? nullable30 = new Decimal?(nullable24.GetValueOrDefault());
        apHistoryResult25.CuryDiscount = nullable30;
        APVendorBalanceEnq.APHistoryResult apHistoryResult26 = aDest;
        nullable24 = aSrc.CuryWhTax;
        Decimal? nullable31 = new Decimal?(nullable24.GetValueOrDefault());
        apHistoryResult26.CuryWhTax = nullable31;
        APVendorBalanceEnq.APHistoryResult apHistoryResult27 = aDest;
        nullable24 = aSrc.CuryPurchases;
        Decimal? nullable32 = new Decimal?(nullable24.GetValueOrDefault());
        apHistoryResult27.CuryPurchases = nullable32;
        APVendorBalanceEnq.APHistoryResult apHistoryResult28 = aDest;
        nullable24 = aSrc.CuryPayments;
        Decimal? nullable33 = new Decimal?(nullable24.GetValueOrDefault());
        apHistoryResult28.CuryPayments = nullable33;
        aDest.CuryDeposits = aSrc.CuryDeposits;
        APVendorBalanceEnq.APHistoryResult apHistoryResult29 = aDest;
        nullable24 = aDest.CuryDepositsBalance;
        Decimal valueOrDefault18 = nullable24.GetValueOrDefault();
        nullable24 = aSrc.CuryDepositsBalance;
        Decimal valueOrDefault19 = nullable24.GetValueOrDefault();
        Decimal? nullable34 = new Decimal?(valueOrDefault18 + valueOrDefault19);
        apHistoryResult29.CuryDepositsBalance = nullable34;
        APVendorBalanceEnq.APHistoryResult apHistoryResult30 = aDest;
        nullable24 = aSrc.CuryRetainageWithheld;
        Decimal? nullable35 = new Decimal?(nullable24.GetValueOrDefault());
        apHistoryResult30.CuryRetainageWithheld = nullable35;
        APVendorBalanceEnq.APHistoryResult apHistoryResult31 = aDest;
        nullable24 = aSrc.CuryRetainageReleased;
        Decimal? nullable36 = new Decimal?(nullable24.GetValueOrDefault());
        apHistoryResult31.CuryRetainageReleased = nullable36;
        APVendorBalanceEnq.APHistoryResult apHistoryResult32 = aDest;
        nullable24 = aDest.CuryEndRetainedBalance;
        Decimal valueOrDefault20 = nullable24.GetValueOrDefault();
        nullable24 = aSrc.CuryBegRetainedBalance;
        Decimal valueOrDefault21 = nullable24.GetValueOrDefault();
        Decimal? nullable37 = new Decimal?(valueOrDefault20 + valueOrDefault21);
        apHistoryResult32.CuryBegRetainedBalance = nullable37;
        APVendorBalanceEnq.APHistoryResult apHistoryResult33 = aDest;
        nullable10 = aDest.CuryBegRetainedBalance;
        nullable23 = aSrc.CuryEndRetainedBalance;
        nullable24 = nullable10.HasValue & nullable23.HasValue ? new Decimal?(nullable10.GetValueOrDefault() + nullable23.GetValueOrDefault()) : new Decimal?();
        Decimal valueOrDefault22;
        if (!nullable24.HasValue)
        {
          nullable23 = aSrc.CuryBegRetainedBalance;
          Decimal? nullable38;
          if (!nullable23.HasValue)
          {
            nullable10 = new Decimal?();
            nullable38 = nullable10;
          }
          else
            nullable38 = new Decimal?(0M - nullable23.GetValueOrDefault());
          nullable10 = nullable38;
          valueOrDefault22 = nullable10.GetValueOrDefault();
        }
        else
          valueOrDefault22 = nullable24.GetValueOrDefault();
        Decimal? nullable39 = new Decimal?(valueOrDefault22);
        apHistoryResult33.CuryEndRetainedBalance = nullable39;
      }
      aDest.RecalculateEndBalance();
    }
  }

  protected virtual bool IsExcludedByZeroBalances(
    bool? showWithBalanceOnly,
    APVendorBalanceEnq.APHistoryResult historyResult)
  {
    return showWithBalanceOnly.GetValueOrDefault() && historyResult.EndBalance.GetValueOrDefault() == 0M && historyResult.DepositsBalance.GetValueOrDefault() == 0M && historyResult.CuryEndBalance.GetValueOrDefault() == 0M && historyResult.CuryDepositsBalance.GetValueOrDefault() == 0M && historyResult.EndRetainedBalance.GetValueOrDefault() == 0M && historyResult.CuryEndRetainedBalance.GetValueOrDefault() == 0M;
  }

  public static void Copy(
    APDocumentEnq.APDocumentFilter filter,
    APVendorBalanceEnq.APHistoryFilter histFilter)
  {
    filter.OrganizationID = histFilter.OrganizationID;
    filter.BranchID = histFilter.BranchID;
    filter.OrgBAccountID = histFilter.OrgBAccountID;
    filter.FinPeriodID = histFilter.FinPeriodID;
    filter.AccountID = histFilter.AccountID;
    filter.SubCD = histFilter.SubID;
    filter.CuryID = histFilter.CuryID;
    filter.UseMasterCalendar = histFilter.UseMasterCalendar;
  }

  public static void Copy(
    APVendorBalanceEnq.APHistoryFilter histFilter,
    APDocumentEnq.APDocumentFilter filter)
  {
    histFilter.OrganizationID = filter.OrganizationID;
    histFilter.BranchID = filter.BranchID;
    histFilter.OrgBAccountID = filter.OrgBAccountID;
    histFilter.VendorID = filter.VendorID;
    histFilter.FinPeriodID = filter.FinPeriodID;
    histFilter.AccountID = filter.AccountID;
    histFilter.SubID = filter.SubCD;
    histFilter.CuryID = filter.CuryID;
    histFilter.UseMasterCalendar = filter.UseMasterCalendar;
  }

  [Serializable]
  public class APHistoryFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _AccountID;
    protected string _SubID;
    protected string _CuryID;
    protected int? _CashAcctID;
    protected string _PaymentMethodID;
    protected string _VendorClassID;
    protected bool? _ShowWithBalanceOnly;
    protected string _FinPeriodID;
    protected bool? _ByFinancialPeriod;
    protected int? _VendorID;

    [Organization(false, Required = false)]
    public int? OrganizationID { get; set; }

    [BranchOfOrganization(typeof (APVendorBalanceEnq.APHistoryFilter.organizationID), false, null, null)]
    public int? BranchID { get; set; }

    [OrganizationTree(typeof (APVendorBalanceEnq.APHistoryFilter.organizationID), typeof (APVendorBalanceEnq.APHistoryFilter.branchID), null, false)]
    public int? OrgBAccountID { get; set; }

    [Account(null, typeof (Search5<PX.Objects.GL.Account.accountID, InnerJoin<APHistory, On<PX.Objects.GL.Account.accountID, Equal<APHistory.accountID>>>, PX.Data.Where<Match<Current<AccessInfo.userName>>>, PX.Data.Aggregate<GroupBy<PX.Objects.GL.Account.accountID>>>), DisplayName = "AP Account", DescriptionField = typeof (PX.Objects.GL.Account.description))]
    public virtual int? AccountID
    {
      get => this._AccountID;
      set => this._AccountID = value;
    }

    [PXDBString(30, IsUnicode = true)]
    [PXUIField(DisplayName = "AP Subaccount", Visibility = PXUIVisibility.Invisible, FieldClass = "SUBACCOUNT")]
    [PXDimension("SUBACCOUNT", ValidComboRequired = false)]
    public virtual string SubID
    {
      get => this._SubID;
      set => this._SubID = value;
    }

    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID), CacheGlobal = true)]
    [PXUIField(DisplayName = "Currency")]
    public virtual string CuryID
    {
      get => this._CuryID;
      set => this._CuryID = value;
    }

    [CashAccount]
    public virtual int? CashAcctID
    {
      get => this._CashAcctID;
      set => this._CashAcctID = value;
    }

    [PXDBString(10, IsUnicode = true)]
    [PXUIField(DisplayName = "Payment Method")]
    [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.useForAP, Equal<True>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
    public virtual string PaymentMethodID
    {
      get => this._PaymentMethodID;
      set => this._PaymentMethodID = value;
    }

    [PXDBString(10, IsUnicode = true)]
    [PXSelector(typeof (VendorClass.vendorClassID), DescriptionField = typeof (VendorClass.descr), CacheGlobal = true)]
    [PXUIField(DisplayName = "Vendor Class")]
    public virtual string VendorClassID
    {
      get => this._VendorClassID;
      set => this._VendorClassID = value;
    }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Vendors with Balance Only")]
    public virtual bool? ShowWithBalanceOnly
    {
      get => this._ShowWithBalanceOnly;
      set => this._ShowWithBalanceOnly = value;
    }

    [PXBool]
    [PXUIField(DisplayName = "Use Master Calendar")]
    [PXUIVisible(typeof (FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleCalendarsSupport>))]
    public bool? UseMasterCalendar { get; set; }

    [PXDefault(typeof (Coalesce<Search<FinPeriod.finPeriodID, Where<FinPeriod.organizationID, Equal<Current<APVendorBalanceEnq.APHistoryFilter.organizationID>>, And<FinPeriod.startDate, LessEqual<Current<AccessInfo.businessDate>>>>, PX.Data.OrderBy<Desc<FinPeriod.startDate, Desc<FinPeriod.endDate>>>>, Search<FinPeriod.finPeriodID, Where<FinPeriod.organizationID, Equal<Zero>, And<FinPeriod.startDate, LessEqual<Current<AccessInfo.businessDate>>>>, PX.Data.OrderBy<Desc<FinPeriod.startDate, Desc<FinPeriod.endDate>>>>>))]
    [AnyPeriodFilterable(null, null, typeof (APVendorBalanceEnq.APHistoryFilter.branchID), null, typeof (APVendorBalanceEnq.APHistoryFilter.organizationID), typeof (APVendorBalanceEnq.APHistoryFilter.useMasterCalendar), null, false, null, null)]
    [PXUIField(DisplayName = "Period", Visibility = PXUIVisibility.Visible)]
    public virtual string FinPeriodID
    {
      get => this._FinPeriodID;
      set => this._FinPeriodID = value;
    }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "By Financial Period")]
    public virtual bool? ByFinancialPeriod
    {
      get => this._ByFinancialPeriod;
      set => this._ByFinancialPeriod = value;
    }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Split by Currency")]
    public virtual bool? SplitByCurrency { get; set; }

    [PXDBString(30, IsUnicode = true)]
    public virtual string SubCDWildcard
    {
      get => SubCDUtils.CreateSubCDWildcard(this._SubID, "SUBACCOUNT");
    }

    [PXDBInt]
    public virtual int? VendorID
    {
      get => this._VendorID;
      set => this._VendorID = value;
    }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryFilter.organizationID>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryFilter.branchID>
    {
    }

    public abstract class orgBAccountID : IBqlField, IBqlOperand
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryFilter.accountID>
    {
    }

    public abstract class subID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryFilter.subID>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryFilter.curyID>
    {
    }

    public abstract class cashAcctID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryFilter.cashAcctID>
    {
    }

    public abstract class paymentMethodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryFilter.paymentMethodID>
    {
    }

    public abstract class vendorClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryFilter.vendorClassID>
    {
    }

    public abstract class showWithBalanceOnly : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryFilter.showWithBalanceOnly>
    {
    }

    public abstract class useMasterCalendar : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryFilter.useMasterCalendar>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryFilter.finPeriodID>
    {
    }

    public abstract class byFinancialPeriod : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryFilter.byFinancialPeriod>
    {
    }

    public abstract class splitByCurrency : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryFilter.splitByCurrency>
    {
    }

    public abstract class subCDWildcard : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryFilter.subCDWildcard>
    {
    }

    public abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryFilter.vendorID>
    {
    }
  }

  public class APHistorySummary : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [CurySymbol(null, null, typeof (APVendorBalanceEnq.APHistoryFilter.curyID), null, null, null, "Total Balance", true, false)]
    [PXBaseCury(typeof (APVendorBalanceEnq.APHistoryFilter.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Balance (Currency)", Enabled = false)]
    public virtual Decimal? CuryBalanceSummary { get; set; }

    [CurySymbol(typeof (APVendorBalanceEnq.APHistoryFilter.organizationID), typeof (APVendorBalanceEnq.APHistoryFilter.branchID), null, null, null, null, null, true, false)]
    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Balance", Enabled = false)]
    public virtual Decimal? BalanceSummary { get; set; }

    [CurySymbol(null, null, typeof (APVendorBalanceEnq.APHistoryFilter.curyID), null, null, null, "Total Prepayments", true, false)]
    [PXCury(typeof (APVendorBalanceEnq.APHistoryFilter.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Prepayments (Currency)", Enabled = false)]
    public virtual Decimal? CuryDepositsSummary { get; set; }

    [CurySymbol(typeof (APVendorBalanceEnq.APHistoryFilter.organizationID), typeof (APVendorBalanceEnq.APHistoryFilter.branchID), null, null, null, null, null, true, false)]
    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Prepayments", Enabled = false)]
    public virtual Decimal? DepositsSummary { get; set; }

    [CurySymbol(null, null, typeof (APVendorBalanceEnq.APHistoryFilter.curyID), null, null, null, "Total Retained Balance", true, false)]
    [PXBaseCury(typeof (APVendorBalanceEnq.APHistoryFilter.curyID))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Retained Balance (Currency)", Enabled = false, FieldClass = "Retainage")]
    public virtual Decimal? CuryBalanceRetainedSummary { get; set; }

    [CurySymbol(typeof (APVendorBalanceEnq.APHistoryFilter.organizationID), typeof (APVendorBalanceEnq.APHistoryFilter.branchID), null, null, null, null, null, true, false)]
    [PXBaseCury]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Retained Balance", Enabled = false, FieldClass = "Retainage")]
    public virtual Decimal? BalanceRetainedSummary { get; set; }

    /// <summary>
    /// Specifies (if set to <c>true</c>) that the <see cref="M:PX.Objects.AP.APVendorBalanceEnq.history" /> delegate calculated the summary.
    /// </summary>
    [PXBool]
    [PXDefault(false)]
    public virtual bool? Calculated { get; set; }

    public virtual void ClearSummary()
    {
      this.BalanceSummary = new Decimal?(0M);
      this.DepositsSummary = new Decimal?(0M);
      this.CuryBalanceSummary = new Decimal?(0M);
      this.CuryDepositsSummary = new Decimal?(0M);
      this.BalanceRetainedSummary = new Decimal?(0M);
      this.CuryBalanceRetainedSummary = new Decimal?(0M);
      this.Calculated = new bool?(false);
    }

    public abstract class curyBalanceSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistorySummary.curyBalanceSummary>
    {
    }

    public abstract class balanceSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistorySummary.balanceSummary>
    {
    }

    public abstract class curyDepositsSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistorySummary.curyDepositsSummary>
    {
    }

    public abstract class depositsSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistorySummary.depositsSummary>
    {
    }

    public abstract class curyBalanceRetainedSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistorySummary.curyBalanceRetainedSummary>
    {
    }

    public abstract class balanceRetainedSummary : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistorySummary.balanceRetainedSummary>
    {
    }

    public abstract class calculated : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistorySummary.calculated>
    {
    }
  }

  [Serializable]
  public class APHistoryResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _VendorID;
    protected string _AcctCD;
    protected string _AcctName;
    protected string _FinPeriodID;
    protected string _CuryID;
    protected Decimal? _CuryBegBalance;
    protected Decimal? _BegBalance;
    protected Decimal? _CuryEndBalance;
    protected Decimal? _EndBalance;
    protected Decimal? _CuryBalance;
    protected Decimal? _Balance;
    protected Decimal? _CuryPurchases;
    protected Decimal? _Purchases;
    protected Decimal? _CuryPayments;
    protected Decimal? _Payments;
    protected Decimal? _CuryDiscount;
    protected Decimal? _Discount;
    protected Decimal? _CuryWhTax;
    protected Decimal? _WhTax;
    protected Decimal? _RGOL;
    protected Decimal? _CuryCrAdjustments;
    protected Decimal? _CrAdjustments;
    protected Decimal? _CuryDrAdjustments;
    protected Decimal? _DrAdjustments;
    protected Decimal? _CuryDeposits;
    protected Decimal? _Deposits;
    protected Guid? _NoteID;
    protected bool? _Converted;

    [PXDBInt]
    [PXDefault]
    public virtual int? VendorID
    {
      get => this._VendorID;
      set => this._VendorID = value;
    }

    [PXDimensionSelector("VENDOR", typeof (Vendor.acctCD), typeof (APVendorBalanceEnq.APHistoryResult.acctCD), new System.Type[] {typeof (Vendor.acctCD), typeof (Vendor.acctName)})]
    [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
    [PXUIField(DisplayName = "Vendor ID", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string AcctCD
    {
      get => this._AcctCD;
      set => this._AcctCD = value;
    }

    [PXDBString(60, IsUnicode = true)]
    [PXUIField(DisplayName = "Vendor Name", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string AcctName
    {
      get => this._AcctName;
      set => this._AcctName = value;
    }

    [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
    [PXUIField(DisplayName = "Last Activity Period", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string FinPeriodID
    {
      get => this._FinPeriodID;
      set => this._FinPeriodID = value;
    }

    [PXDBString(5, IsUnicode = true)]
    [PXUIField(DisplayName = "Currency", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string CuryID
    {
      get => this._CuryID;
      set => this._CuryID = value;
    }

    [PXDBCury(typeof (APVendorBalanceEnq.APHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency Beginning Balance", Visible = false)]
    public virtual Decimal? CuryBegBalance
    {
      get => this._CuryBegBalance;
      set => this._CuryBegBalance = value;
    }

    [PXBaseCury]
    [PXUIField(DisplayName = "Beginning Balance", Visible = false)]
    public virtual Decimal? BegBalance
    {
      get => this._BegBalance;
      set => this._BegBalance = value;
    }

    [PXDBCury(typeof (APVendorBalanceEnq.APHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency Ending Balance", Visible = false, Visibility = PXUIVisibility.SelectorVisible)]
    public virtual Decimal? CuryEndBalance
    {
      get => this._CuryEndBalance;
      set => this._CuryEndBalance = value;
    }

    [PXBaseCury]
    [PXUIField(DisplayName = "Ending Balance", Visible = false, Visibility = PXUIVisibility.SelectorVisible)]
    public virtual Decimal? EndBalance
    {
      get => this._EndBalance;
      set => this._EndBalance = value;
    }

    [PXDBCury(typeof (APVendorBalanceEnq.APHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency Balance", Visible = false)]
    public virtual Decimal? CuryBalance
    {
      get => this._CuryBalance;
      set => this._CuryBalance = value;
    }

    [PXBaseCury]
    [PXUIField(DisplayName = "Balance", Visible = false)]
    public virtual Decimal? Balance
    {
      get => this._Balance;
      set => this._Balance = value;
    }

    [PXDBCury(typeof (APVendorBalanceEnq.APHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency PTD Purchases")]
    public virtual Decimal? CuryPurchases
    {
      get => this._CuryPurchases;
      set => this._CuryPurchases = value;
    }

    [PXBaseCury]
    [PXUIField(DisplayName = "PTD Purchases")]
    public virtual Decimal? Purchases
    {
      get => this._Purchases;
      set => this._Purchases = value;
    }

    [PXDBCury(typeof (APVendorBalanceEnq.APHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency PTD Payments")]
    public virtual Decimal? CuryPayments
    {
      get => this._CuryPayments;
      set => this._CuryPayments = value;
    }

    [PXBaseCury]
    [PXUIField(DisplayName = "PTD Payments")]
    public virtual Decimal? Payments
    {
      get => this._Payments;
      set => this._Payments = value;
    }

    [PXDBCury(typeof (APVendorBalanceEnq.APHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency PTD Cash Discount Taken")]
    public virtual Decimal? CuryDiscount
    {
      get => this._CuryDiscount;
      set => this._CuryDiscount = value;
    }

    [PXBaseCury]
    [PXUIField(DisplayName = "PTD Cash Discount Taken")]
    public virtual Decimal? Discount
    {
      get => this._Discount;
      set => this._Discount = value;
    }

    [PXDBCury(typeof (APVendorBalanceEnq.APHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency PTD Tax Withheld")]
    public virtual Decimal? CuryWhTax
    {
      get => this._CuryWhTax;
      set => this._CuryWhTax = value;
    }

    [PXBaseCury]
    [PXUIField(DisplayName = "PTD Tax Withheld")]
    public virtual Decimal? WhTax
    {
      get => this._WhTax;
      set => this._WhTax = value;
    }

    [PXBaseCury]
    [PXUIField(DisplayName = "PTD Realized Gain/Loss")]
    public virtual Decimal? RGOL
    {
      get => this._RGOL;
      set => this._RGOL = value;
    }

    [PXDBCury(typeof (APVendorBalanceEnq.APHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency PTD Credit Adjustments")]
    public virtual Decimal? CuryCrAdjustments
    {
      get => this._CuryCrAdjustments;
      set => this._CuryCrAdjustments = value;
    }

    [PXBaseCury]
    [PXUIField(DisplayName = "PTD Credit Adjustments")]
    public virtual Decimal? CrAdjustments
    {
      get => this._CrAdjustments;
      set => this._CrAdjustments = value;
    }

    [PXDBCury(typeof (APVendorBalanceEnq.APHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency PTD Debit Adjustments")]
    public virtual Decimal? CuryDrAdjustments
    {
      get => this._CuryDrAdjustments;
      set => this._CuryDrAdjustments = value;
    }

    [PXBaseCury]
    [PXUIField(DisplayName = "PTD Debit Adjustments")]
    public virtual Decimal? DrAdjustments
    {
      get => this._DrAdjustments;
      set => this._DrAdjustments = value;
    }

    [PXDBCury(typeof (APVendorBalanceEnq.APHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency PTD Prepayments")]
    public virtual Decimal? CuryDeposits
    {
      get => this._CuryDeposits;
      set => this._CuryDeposits = value;
    }

    [PXBaseCury]
    [PXUIField(DisplayName = "PTD Prepayments")]
    public virtual Decimal? Deposits
    {
      get => this._Deposits;
      set => this._Deposits = value;
    }

    [PXDBCury(typeof (APVendorBalanceEnq.APHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency Prepayment Balance")]
    public virtual Decimal? CuryDepositsBalance { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "Prepayment Balance")]
    public virtual Decimal? DepositsBalance { get; set; }

    [PXDBCury(typeof (APVendorBalanceEnq.APHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency PTD Retainage Withheld", FieldClass = "Retainage")]
    public virtual Decimal? CuryRetainageWithheld { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "PTD Retainage Withheld", FieldClass = "Retainage")]
    public virtual Decimal? RetainageWithheld { get; set; }

    [PXDBCury(typeof (APVendorBalanceEnq.APHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency PTD Retainage Released", FieldClass = "Retainage")]
    public virtual Decimal? CuryRetainageReleased { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "PTD Retainage Released", FieldClass = "Retainage")]
    public virtual Decimal? RetainageReleased { get; set; }

    [PXDBCury(typeof (APVendorBalanceEnq.APHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency Beginning Retained Balance", FieldClass = "Retainage")]
    public virtual Decimal? CuryBegRetainedBalance { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "Beginning Retained Balance", FieldClass = "Retainage")]
    public virtual Decimal? BegRetainedBalance { get; set; }

    [PXDBCury(typeof (APVendorBalanceEnq.APHistoryResult.curyID))]
    [PXUIField(DisplayName = "Currency Ending Retained Balance", FieldClass = "Retainage")]
    public virtual Decimal? CuryEndRetainedBalance { get; set; }

    [PXBaseCury]
    [PXUIField(DisplayName = "Ending Retained Balance", FieldClass = "Retainage")]
    public virtual Decimal? EndRetainedBalance { get; set; }

    [PXNote]
    public virtual Guid? NoteID
    {
      get => this._NoteID;
      set => this._NoteID = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Converted to Base Currency", Visible = false, Enabled = false)]
    public virtual bool? Converted
    {
      get => this._Converted;
      set => this._Converted = value;
    }

    public virtual void RecalculateEndBalance()
    {
      this.RecalculateBalance();
      Decimal? nullable1 = this.CuryBegBalance;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      nullable1 = this.CuryBalance;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      this.CuryEndBalance = new Decimal?(valueOrDefault1 + valueOrDefault2);
      Decimal? nullable2 = this.BegBalance;
      Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
      nullable2 = this.Balance;
      Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
      this.EndBalance = new Decimal?(valueOrDefault3 + valueOrDefault4);
    }

    public virtual void RecalculateBalance()
    {
      Decimal? nullable1 = this.Purchases;
      Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
      nullable1 = this.Payments;
      Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
      Decimal num1 = valueOrDefault1 - valueOrDefault2;
      nullable1 = this.Discount;
      Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
      Decimal num2 = num1 - valueOrDefault3;
      nullable1 = this.WhTax;
      Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
      Decimal num3 = num2 - valueOrDefault4;
      nullable1 = this.RGOL;
      Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
      Decimal num4 = num3 + valueOrDefault5;
      nullable1 = this.DrAdjustments;
      Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
      Decimal num5 = num4 - valueOrDefault6;
      nullable1 = this.CrAdjustments;
      Decimal valueOrDefault7 = nullable1.GetValueOrDefault();
      this.Balance = new Decimal?(num5 + valueOrDefault7);
      Decimal? nullable2 = this.CuryPurchases;
      Decimal valueOrDefault8 = nullable2.GetValueOrDefault();
      nullable2 = this.CuryPayments;
      Decimal valueOrDefault9 = nullable2.GetValueOrDefault();
      Decimal num6 = valueOrDefault8 - valueOrDefault9;
      nullable2 = this.CuryDiscount;
      Decimal valueOrDefault10 = nullable2.GetValueOrDefault();
      Decimal num7 = num6 - valueOrDefault10;
      nullable2 = this.CuryWhTax;
      Decimal valueOrDefault11 = nullable2.GetValueOrDefault();
      Decimal num8 = num7 - valueOrDefault11;
      nullable2 = this.CuryDrAdjustments;
      Decimal valueOrDefault12 = nullable2.GetValueOrDefault();
      Decimal num9 = num8 - valueOrDefault12;
      nullable2 = this.CuryCrAdjustments;
      Decimal valueOrDefault13 = nullable2.GetValueOrDefault();
      this.CuryBalance = new Decimal?(num9 + valueOrDefault13);
    }

    public virtual void CopyValueToCuryValue(string aBaseCuryID)
    {
      this.CuryBegBalance = new Decimal?(this.BegBalance.GetValueOrDefault());
      this.CuryPurchases = new Decimal?(this.Purchases.GetValueOrDefault());
      this.CuryPayments = new Decimal?(this.Payments.GetValueOrDefault());
      this.CuryDiscount = new Decimal?(this.Discount.GetValueOrDefault());
      this.CuryWhTax = new Decimal?(this.WhTax.GetValueOrDefault());
      this.CuryCrAdjustments = new Decimal?(this.CrAdjustments.GetValueOrDefault());
      this.CuryDrAdjustments = new Decimal?(this.DrAdjustments.GetValueOrDefault());
      this.CuryDeposits = new Decimal?(this.Deposits.GetValueOrDefault());
      this.CuryDepositsBalance = new Decimal?(this.DepositsBalance.GetValueOrDefault());
      this.CuryID = aBaseCuryID;
      this.CuryEndBalance = new Decimal?(this.EndBalance.GetValueOrDefault());
      this.CuryRetainageWithheld = new Decimal?(this.RetainageWithheld.GetValueOrDefault());
      this.CuryRetainageReleased = new Decimal?(this.RetainageReleased.GetValueOrDefault());
      this.CuryBegRetainedBalance = new Decimal?(this.BegRetainedBalance.GetValueOrDefault());
      this.CuryEndRetainedBalance = new Decimal?(this.EndRetainedBalance.GetValueOrDefault());
      this.Converted = new bool?(true);
    }

    public abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.vendorID>
    {
    }

    public abstract class acctCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.acctCD>
    {
    }

    public abstract class acctName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.acctName>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.finPeriodID>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.curyID>
    {
    }

    public abstract class curyBegBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.curyBegBalance>
    {
    }

    public abstract class begBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.begBalance>
    {
    }

    public abstract class curyEndBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.curyEndBalance>
    {
    }

    public abstract class endBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.endBalance>
    {
    }

    public abstract class curyBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.curyBalance>
    {
    }

    public abstract class balance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.balance>
    {
    }

    public abstract class curyPurchases : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.curyPurchases>
    {
    }

    public abstract class purchases : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.purchases>
    {
    }

    public abstract class curyPayments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.curyPayments>
    {
    }

    public abstract class payments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.payments>
    {
    }

    public abstract class curyDiscount : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.curyDiscount>
    {
    }

    public abstract class discount : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.discount>
    {
    }

    public abstract class curyWhTax : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.curyWhTax>
    {
    }

    public abstract class whTax : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.whTax>
    {
    }

    public abstract class rGOL : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.rGOL>
    {
    }

    public abstract class curyCrAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.curyCrAdjustments>
    {
    }

    public abstract class crAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.crAdjustments>
    {
    }

    public abstract class curyDrAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.curyDrAdjustments>
    {
    }

    public abstract class drAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.drAdjustments>
    {
    }

    public abstract class curyDeposits : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.curyDeposits>
    {
    }

    public abstract class deposits : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.deposits>
    {
    }

    public abstract class curyDepositsBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.curyDepositsBalance>
    {
    }

    public abstract class depositsBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.depositsBalance>
    {
    }

    public abstract class curyRetainageWithheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.curyRetainageWithheld>
    {
    }

    public abstract class retainageWithheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.retainageWithheld>
    {
    }

    public abstract class curyRetainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.curyRetainageReleased>
    {
    }

    public abstract class retainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.retainageReleased>
    {
    }

    public abstract class curyBegRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.curyBegRetainedBalance>
    {
    }

    public abstract class begRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.begRetainedBalance>
    {
    }

    public abstract class curyEndRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.curyEndRetainedBalance>
    {
    }

    public abstract class endRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.endRetainedBalance>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.noteID>
    {
    }

    public abstract class converted : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APVendorBalanceEnq.APHistoryResult.converted>
    {
    }
  }

  /// <summary>
  /// A projection DAC over <see cref="T:PX.Objects.AP.CuryAPHistory" /> that is intended to calculate the
  /// <see cref="P:PX.Objects.AP.APVendorBalanceEnq.APLatestHistory.LastActivityPeriod">latest available history period</see> for every
  /// combination of branch, vendor, account, subaccount, and currency.
  /// </summary>
  [PXProjection(typeof (Select4<CuryAPHistory, PX.Data.Aggregate<GroupBy<CuryAPHistory.branchID, GroupBy<CuryAPHistory.vendorID, GroupBy<CuryAPHistory.accountID, GroupBy<CuryAPHistory.subID, GroupBy<CuryAPHistory.curyID, Max<CuryAPHistory.finPeriodID>>>>>>>>))]
  [PXCacheName("AP Latest History")]
  [Serializable]
  public class APLatestHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _BranchID;
    protected int? _VendorID;
    protected int? _AccountID;
    protected int? _SubID;
    protected string _CuryID;
    protected string _LastActivityPeriod;

    [PXDBInt(IsKey = true, BqlField = typeof (CuryAPHistory.branchID))]
    [PXSelector(typeof (PX.Objects.GL.Branch.branchID), SubstituteKey = typeof (PX.Objects.GL.Branch.branchCD))]
    public virtual int? BranchID
    {
      get => this._BranchID;
      set => this._BranchID = value;
    }

    [PXDBInt(IsKey = true, BqlField = typeof (CuryAPHistory.vendorID))]
    public virtual int? VendorID
    {
      get => this._VendorID;
      set => this._VendorID = value;
    }

    [PXDBInt(IsKey = true, BqlField = typeof (CuryAPHistory.accountID))]
    public virtual int? AccountID
    {
      get => this._AccountID;
      set => this._AccountID = value;
    }

    [PXDBInt(IsKey = true, BqlField = typeof (CuryAPHistory.subID))]
    public virtual int? SubID
    {
      get => this._SubID;
      set => this._SubID = value;
    }

    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (CuryAPHistory.curyID))]
    public virtual string CuryID
    {
      get => this._CuryID;
      set => this._CuryID = value;
    }

    [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (CuryAPHistory.finPeriodID))]
    public virtual string LastActivityPeriod
    {
      get => this._LastActivityPeriod;
      set => this._LastActivityPeriod = value;
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APVendorBalanceEnq.APLatestHistory.branchID>
    {
    }

    public abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APVendorBalanceEnq.APLatestHistory.vendorID>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APVendorBalanceEnq.APLatestHistory.accountID>
    {
    }

    public abstract class subID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APVendorBalanceEnq.APLatestHistory.subID>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APVendorBalanceEnq.APLatestHistory.curyID>
    {
    }

    public abstract class lastActivityPeriod : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APVendorBalanceEnq.APLatestHistory.lastActivityPeriod>
    {
    }
  }

  [PXHidden]
  [PXProjection(typeof (PX.Data.Select<CuryAPHistory>))]
  [Serializable]
  public class CuryAPHistoryTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt(IsKey = true, BqlTable = typeof (CuryAPHistory))]
    public virtual int? BranchID { get; set; }

    [PXDBInt(IsKey = true, BqlTable = typeof (CuryAPHistory))]
    public virtual int? AccountID { get; set; }

    [PXDBInt(IsKey = true, BqlTable = typeof (CuryAPHistory))]
    public virtual int? SubID { get; set; }

    [PXDBInt(IsKey = true, BqlTable = typeof (CuryAPHistory))]
    public virtual int? VendorID { get; set; }

    [PXDBString(IsKey = true, BqlTable = typeof (CuryAPHistory))]
    public virtual string CuryID { get; set; }

    [PXDBString(IsKey = true, BqlTable = typeof (CuryAPHistory))]
    public virtual string FinPeriodID { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.finBegBalance>, CuryAPHistory.finYtdBalance>), typeof (Decimal))]
    public virtual Decimal? FinBegBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.finPtdPurchases>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdPurchases { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.finPtdPayments>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdPayments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.finPtdDrAdjustments>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdDrAdjustments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.finPtdCrAdjustments>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdCrAdjustments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.finPtdDiscTaken>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdDiscTaken { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (PX.Data.Minus<Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.finPtdRGOL>, Zero>>), typeof (Decimal))]
    public virtual Decimal? FinPtdRGOL { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryAPHistory))]
    public virtual Decimal? FinYtdBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.finPtdDeposits>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdDeposits { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (PX.Data.Minus<CuryAPHistory.finYtdDeposits>), typeof (Decimal))]
    public virtual Decimal? FinYtdDeposits { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.tranBegBalance>, CuryAPHistory.tranYtdBalance>), typeof (Decimal))]
    public virtual Decimal? TranBegBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.tranPtdPurchases>, Zero>), typeof (Decimal))]
    public virtual Decimal? TranPtdPurchases { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.tranPtdPayments>, Zero>), typeof (Decimal))]
    public virtual Decimal? TranPtdPayments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.tranPtdDrAdjustments>, Zero>), typeof (Decimal))]
    public virtual Decimal? TranPtdDrAdjustments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.tranPtdCrAdjustments>, Zero>), typeof (Decimal))]
    public virtual Decimal? TranPtdCrAdjustments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.tranPtdDiscTaken>, Zero>), typeof (Decimal))]
    public virtual Decimal? TranPtdDiscTaken { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (PX.Data.Minus<Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.tranPtdRGOL>, Zero>>), typeof (Decimal))]
    public virtual Decimal? TranPtdRGOL { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryAPHistory))]
    public virtual Decimal? TranYtdBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.tranPtdDeposits>, Zero>), typeof (Decimal))]
    public virtual Decimal? TranPtdDeposits { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (PX.Data.Minus<CuryAPHistory.tranYtdDeposits>), typeof (Decimal))]
    public virtual Decimal? TranYtdDeposits { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyFinBegBalance>, CuryAPHistory.curyFinYtdBalance>), typeof (Decimal))]
    public virtual Decimal? CuryFinBegBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyFinPtdPurchases>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryFinPtdPurchases { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyFinPtdPayments>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryFinPtdPayments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyFinPtdDrAdjustments>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryFinPtdDrAdjustments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyFinPtdCrAdjustments>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryFinPtdCrAdjustments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyFinPtdDiscTaken>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryFinPtdDiscTaken { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryAPHistory))]
    public virtual Decimal? CuryFinYtdBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyFinPtdDeposits>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryFinPtdDeposits { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (PX.Data.Minus<CuryAPHistory.curyFinYtdDeposits>), typeof (Decimal))]
    public virtual Decimal? CuryFinYtdDeposits { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyTranBegBalance>, CuryAPHistory.curyTranYtdBalance>), typeof (Decimal))]
    public virtual Decimal? CuryTranBegBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyTranPtdPurchases>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryTranPtdPurchases { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyTranPtdPayments>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryTranPtdPayments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyTranPtdDrAdjustments>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryTranPtdDrAdjustments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyTranPtdCrAdjustments>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryTranPtdCrAdjustments { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyTranPtdDiscTaken>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryTranPtdDiscTaken { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryAPHistory))]
    public virtual Decimal? CuryTranYtdBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyTranPtdDeposits>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryTranPtdDeposits { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (PX.Data.Minus<CuryAPHistory.curyTranYtdDeposits>), typeof (Decimal))]
    public virtual Decimal? CuryTranYtdDeposits { get; set; }

    [PXDBBool(BqlTable = typeof (CuryAPHistory))]
    public virtual bool? DetDeleted { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyFinPtdWhTax>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryFinPtdWhTax { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyTranPtdWhTax>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryTranPtdWhTax { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.finPtdWhTax>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdWhTax { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.tranPtdWhTax>, Zero>), typeof (Decimal))]
    public virtual Decimal? TranPtdWhTax { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyFinPtdRetainageWithheld>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryFinPtdRetainageWithheld { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.finPtdRetainageWithheld>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdRetainageWithheld { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyTranPtdRetainageWithheld>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryTranPtdRetainageWithheld { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.tranPtdRetainageWithheld>, Zero>), typeof (Decimal))]
    public virtual Decimal? TranPtdRetainageWithheld { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryAPHistory))]
    public virtual Decimal? CuryFinYtdRetainageWithheld { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryAPHistory))]
    public virtual Decimal? FinYtdRetainageWithheld { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Sub<CuryAPHistory.curyFinYtdRetainageWithheld, Add<CuryAPHistory.curyFinYtdRetainageReleased, Sub<Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyFinPtdRetainageWithheld>, Zero>, Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyFinPtdRetainageReleased>, Zero>>>>), typeof (Decimal))]
    public virtual Decimal? CuryFinBegRetainedBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Sub<CuryAPHistory.finYtdRetainageWithheld, Add<CuryAPHistory.finYtdRetainageReleased, Sub<Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.finPtdRetainageWithheld>, Zero>, Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.finPtdRetainageReleased>, Zero>>>>), typeof (Decimal))]
    public virtual Decimal? FinBegRetainedBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Sub<CuryAPHistory.curyTranYtdRetainageWithheld, Add<CuryAPHistory.curyTranYtdRetainageReleased, Sub<Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyTranPtdRetainageWithheld>, Zero>, Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyTranPtdRetainageReleased>, Zero>>>>), typeof (Decimal))]
    public virtual Decimal? CuryTranBegRetainedBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Sub<CuryAPHistory.tranYtdRetainageWithheld, Add<CuryAPHistory.tranYtdRetainageReleased, Sub<Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.tranPtdRetainageWithheld>, Zero>, Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.tranPtdRetainageReleased>, Zero>>>>), typeof (Decimal))]
    public virtual Decimal? TranBegRetainedBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Sub<CuryAPHistory.curyFinYtdRetainageWithheld, CuryAPHistory.curyFinYtdRetainageReleased>), typeof (Decimal))]
    public virtual Decimal? CuryFinYtdRetainedBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Sub<CuryAPHistory.finYtdRetainageWithheld, CuryAPHistory.finYtdRetainageReleased>), typeof (Decimal))]
    public virtual Decimal? FinYtdRetainedBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Sub<CuryAPHistory.curyTranYtdRetainageWithheld, CuryAPHistory.curyTranYtdRetainageReleased>), typeof (Decimal))]
    public virtual Decimal? CuryTranYtdRetainedBalance { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Sub<CuryAPHistory.tranYtdRetainageWithheld, CuryAPHistory.tranYtdRetainageReleased>), typeof (Decimal))]
    public virtual Decimal? TranYtdRetainedBalance { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryAPHistory))]
    public virtual Decimal? CuryTranYtdRetainageWithheld { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryAPHistory))]
    public virtual Decimal? TranYtdRetainageWithheld { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyFinPtdRetainageReleased>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryFinPtdRetainageReleased { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.finPtdRetainageReleased>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdRetainageReleased { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.curyTranPtdRetainageReleased>, Zero>), typeof (Decimal))]
    public virtual Decimal? CuryTranPtdRetainageReleased { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.tranPtdRetainageReleased>, Zero>), typeof (Decimal))]
    public virtual Decimal? TranPtdRetainageReleased { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryAPHistory))]
    public virtual Decimal? CuryFinYtdRetainageReleased { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryAPHistory))]
    public virtual Decimal? FinYtdRetainageReleased { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryAPHistory))]
    public virtual Decimal? CuryTranYtdRetainageReleased { get; set; }

    [PXDBDecimal(4, BqlTable = typeof (CuryAPHistory))]
    public virtual Decimal? TranYtdRetainageReleased { get; set; }

    [PXDecimal]
    [PXDBCalced(typeof (Switch<Case<Where<CuryAPHistory.finPeriodID, Equal<CurrentValue<APVendorBalanceEnq.APHistoryFilter.finPeriodID>>>, CuryAPHistory.finPtdRevalued>, Zero>), typeof (Decimal))]
    public virtual Decimal? FinPtdRevalued { get; set; }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.branchID>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.accountID>
    {
    }

    public abstract class subID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.subID>
    {
    }

    public abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.vendorID>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyID>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.finPeriodID>
    {
    }

    public abstract class finBegBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.finBegBalance>
    {
    }

    public abstract class finPtdPurchases : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.finPtdPurchases>
    {
    }

    public abstract class finPtdPayments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.finPtdPayments>
    {
    }

    public abstract class finPtdDrAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.finPtdDrAdjustments>
    {
    }

    public abstract class finPtdCrAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.finPtdCrAdjustments>
    {
    }

    public abstract class finPtdDiscTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.finPtdDiscTaken>
    {
    }

    public abstract class finPtdRGOL : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.finPtdRGOL>
    {
    }

    public abstract class finYtdBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.finYtdBalance>
    {
    }

    public abstract class finPtdDeposits : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.finPtdDeposits>
    {
    }

    public abstract class finYtdDeposits : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.finYtdDeposits>
    {
    }

    public abstract class tranBegBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.tranBegBalance>
    {
    }

    public abstract class tranPtdPurchases : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.tranPtdPurchases>
    {
    }

    public abstract class tranPtdPayments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.tranPtdPayments>
    {
    }

    public abstract class tranPtdDrAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.tranPtdDrAdjustments>
    {
    }

    public abstract class tranPtdCrAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.tranPtdCrAdjustments>
    {
    }

    public abstract class tranPtdDiscTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.tranPtdDiscTaken>
    {
    }

    public abstract class tranPtdRGOL : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.tranPtdRGOL>
    {
    }

    public abstract class tranYtdBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.tranYtdBalance>
    {
    }

    public abstract class tranPtdDeposits : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.tranPtdDeposits>
    {
    }

    public abstract class tranYtdDeposits : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.tranYtdDeposits>
    {
    }

    public abstract class curyFinBegBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyFinBegBalance>
    {
    }

    public abstract class curyFinPtdPurchases : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyFinPtdPurchases>
    {
    }

    public abstract class curyFinPtdPayments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyFinPtdPayments>
    {
    }

    public abstract class curyFinPtdDrAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyFinPtdDrAdjustments>
    {
    }

    public abstract class curyFinPtdCrAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyFinPtdCrAdjustments>
    {
    }

    public abstract class curyFinPtdDiscTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyFinPtdDiscTaken>
    {
    }

    public abstract class curyFinYtdBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyFinYtdBalance>
    {
    }

    public abstract class curyFinPtdDeposits : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyFinPtdDeposits>
    {
    }

    public abstract class curyFinYtdDeposits : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyFinYtdDeposits>
    {
    }

    public abstract class curyTranBegBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyTranBegBalance>
    {
    }

    public abstract class curyTranPtdPurchases : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyTranPtdPurchases>
    {
    }

    public abstract class curyTranPtdPayments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyTranPtdPayments>
    {
    }

    public abstract class curyTranPtdDrAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyTranPtdDrAdjustments>
    {
    }

    public abstract class curyTranPtdCrAdjustments : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyTranPtdCrAdjustments>
    {
    }

    public abstract class curyTranPtdDiscTaken : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyTranPtdDiscTaken>
    {
    }

    public abstract class curyTranYtdBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyTranYtdBalance>
    {
    }

    public abstract class curyTranPtdDeposits : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyTranPtdDeposits>
    {
    }

    public abstract class curyTranYtdDeposits : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyTranYtdDeposits>
    {
    }

    public abstract class detDeleted : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.detDeleted>
    {
    }

    public abstract class curyFinPtdWhTax : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyFinPtdWhTax>
    {
    }

    public abstract class curyTranPtdWhTax : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyTranPtdWhTax>
    {
    }

    public abstract class finPtdWhTax : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.finPtdWhTax>
    {
    }

    public abstract class tranPtdWhTax : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.tranPtdWhTax>
    {
    }

    public abstract class curyFinPtdRetainageWithheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyFinPtdRetainageWithheld>
    {
    }

    public abstract class finPtdRetainageWithheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.finPtdRetainageWithheld>
    {
    }

    public abstract class curyTranPtdRetainageWithheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyTranPtdRetainageWithheld>
    {
    }

    public abstract class tranPtdRetainageWithheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.tranPtdRetainageWithheld>
    {
    }

    public abstract class curyFinYtdRetainageWithheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyFinYtdRetainageWithheld>
    {
    }

    public abstract class finYtdRetainageWithheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.finYtdRetainageWithheld>
    {
    }

    public abstract class curyFinBegRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyFinBegRetainedBalance>
    {
    }

    public abstract class finBegRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.finBegRetainedBalance>
    {
    }

    public abstract class curyTranBegRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyTranBegRetainedBalance>
    {
    }

    public abstract class tranBegRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.tranBegRetainedBalance>
    {
    }

    public abstract class curyFinYtdRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyFinYtdRetainedBalance>
    {
    }

    public abstract class finYtdRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.finYtdRetainedBalance>
    {
    }

    public abstract class curyTranYtdRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyTranYtdRetainedBalance>
    {
    }

    public abstract class tranYtdRetainedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.tranYtdRetainedBalance>
    {
    }

    public abstract class curyTranYtdRetainageWithheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyTranYtdRetainageWithheld>
    {
    }

    public abstract class tranYtdRetainageWithheld : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.tranYtdRetainageWithheld>
    {
    }

    public abstract class curyFinPtdRetainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyFinPtdRetainageReleased>
    {
    }

    public abstract class finPtdRetainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.finPtdRetainageReleased>
    {
    }

    public abstract class curyTranPtdRetainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyTranPtdRetainageReleased>
    {
    }

    public abstract class tranPtdRetainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.tranPtdRetainageReleased>
    {
    }

    public abstract class curyFinYtdRetainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyFinYtdRetainageReleased>
    {
    }

    public abstract class finYtdRetainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.finYtdRetainageReleased>
    {
    }

    public abstract class curyTranYtdRetainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.curyTranYtdRetainageReleased>
    {
    }

    public abstract class tranYtdRetainageReleased : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.tranYtdRetainageReleased>
    {
    }

    public abstract class finPtdRevalued : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      APVendorBalanceEnq.CuryAPHistoryTran.finPtdRevalued>
    {
    }
  }

  private sealed class decimalZero : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Constant<
    #nullable disable
    APVendorBalanceEnq.decimalZero>
  {
    public decimalZero()
      : base(0M)
    {
    }
  }
}
