// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.AP1099SummaryEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AP;

[TableAndChartDashboardType]
public class AP1099SummaryEnq : PXGraph<
#nullable disable
AP1099SummaryEnq>, ICaptionable
{
  public PXSelect<AP1099Year> Year_Header;
  public PXCancel<AP1099Year> Cancel;
  public PXFirst<AP1099Year> First;
  public PXPrevious<AP1099Year> Prev;
  public PXNext<AP1099Year> Next;
  public PXLast<AP1099Year> Last;
  public PXAction<AP1099Year> close1099Year;
  public PXAction<AP1099Year> reopen1099Year;
  public PXAction<AP1099Year> reportsFolder;
  public PXAction<AP1099Year> year1099SummaryReport;
  public PXAction<AP1099Year> year1099DetailReport;
  public PXAction<AP1099Year> open1099PaymentsReport;
  public PXAction<AP1099Year> year1099NECSummaryReport;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoinGroupBy<AP1099Box, LeftJoin<AP1099History, On<AP1099History.boxNbr, Equal<AP1099Box.boxNbr>, And<AP1099History.finYear, Equal<Current<AP1099Year.finYear>>>>, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<AP1099History.branchID>>>>, Where<PX.Objects.GL.Branch.organizationID, Equal<Current<AP1099Year.organizationID>>, Or<PX.Objects.GL.Branch.organizationID, PX.Data.IsNull>>, PX.Data.Aggregate<GroupBy<AP1099Box.boxNbr, Sum<AP1099History.histAmt>>>, PX.Data.OrderBy<BqlField<
  #nullable enable
  AP1099Box.boxCD, IBqlString>.Asc>> Year_Summary;
  public 
  #nullable disable
  PXSetup<PX.Objects.AP.APSetup> APSetup;

  [Box1099NumberSelector]
  [PXMergeAttributes(Method = MergeMethod.Append)]
  public virtual void AP1099Box_BoxNbr_CacheAttached(PXCache sender)
  {
  }

  protected virtual void AP1099Year_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    AP1099Year row = (AP1099Year) e.Row;
    if (row == null)
      return;
    this.close1099Year.SetEnabled(!string.IsNullOrEmpty(row.FinYear) && row.Status == "N");
    this.reopen1099Year.SetEnabled(!string.IsNullOrEmpty(row.FinYear) && row.Status == "C" && !string.IsNullOrEmpty(PredefinedRoles.FinancialSupervisor) && PXContext.PXIdentity.User.IsInRole(PredefinedRoles.FinancialSupervisor));
    int[] childBranchIds = PXAccess.GetChildBranchIDs(this.Year_Header.Current.OrganizationID);
    if (childBranchIds.Length == 0)
      return;
    PXSetPropertyException propertyException1;
    if (!PXSelectBase<APRegister, PXSelectJoin<APRegister, InnerJoin<Vendor, On<APRegister.vendorID, Equal<Vendor.bAccountID>>, InnerJoin<OrganizationFinPeriod, On<APRegister.finPeriodID, Equal<OrganizationFinPeriod.finPeriodID>>>>, Where<Vendor.vendor1099, Equal<True>, And<APRegister.docType, Equal<APDocType.prepayment>, And<APRegister.status, NotEqual<APDocStatus.closed>, And<OrganizationFinPeriod.finYear, Equal<Current<AP1099Year.finYear>>, And<OrganizationFinPeriod.organizationID, Equal<Required<OrganizationFinPeriod.organizationID>>, And<APRegister.branchID, PX.Data.In<Required<APRegister.branchID>>>>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) this.Year_Header.Current.OrganizationID, (object) childBranchIds).AsEnumerable<PXResult<APRegister>>().Any<PXResult<APRegister>>())
      propertyException1 = (PXSetPropertyException) null;
    else
      propertyException1 = new PXSetPropertyException("There are open payments to 1099 vendors dated {0} that will not be included into the 1099-MISC Form for this year.", PXErrorLevel.Warning, new object[1]
      {
        (object) row.FinYear
      });
    PXSetPropertyException propertyException2 = propertyException1;
    sender.RaiseExceptionHandling<AP1099Year.finYear>((object) row, (object) row.FinYear, (Exception) propertyException2);
  }

  protected virtual void AP1099Year_BranchID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is AP1099Year row))
      return;
    row.FinYear = (string) null;
  }

  [PXUIField(DisplayName = "Close Year", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton(Category = "Processing", DisplayOnMainToolbar = true)]
  public virtual IEnumerable Close1099Year(PXAdapter adapter)
  {
    PXCache cache = this.Year_Header.Cache;
    List<AP1099Year> list = adapter.Get().Cast<AP1099Year>().ToList<AP1099Year>();
    foreach (AP1099Year ap1099Year in list.Where<AP1099Year>((Func<AP1099Year, bool>) (year =>
    {
      if (string.IsNullOrEmpty(year.FinYear) || !(year.Status == "N"))
        return false;
      int? organizationId1 = this.Year_Header.Current.OrganizationID;
      int? organizationId2 = year.OrganizationID;
      return organizationId1.GetValueOrDefault() == organizationId2.GetValueOrDefault() & organizationId1.HasValue == organizationId2.HasValue;
    })))
    {
      ap1099Year.Status = "C";
      cache.Update((object) ap1099Year);
    }
    if (cache.IsInsertedUpdatedDeleted)
    {
      this.Actions.PressSave();
      PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => { }));
    }
    return (IEnumerable) list;
  }

  [PXUIField(DisplayName = "Reopen Year", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton(Category = "Processing", DisplayOnMainToolbar = true)]
  public virtual IEnumerable Reopen1099Year(PXAdapter adapter)
  {
    PXCache cache = this.Year_Header.Cache;
    List<AP1099Year> list = adapter.Get().Cast<AP1099Year>().ToList<AP1099Year>();
    foreach (AP1099Year ap1099Year in list.Where<AP1099Year>((Func<AP1099Year, bool>) (year =>
    {
      if (string.IsNullOrEmpty(year.FinYear) || !(year.Status == "C"))
        return false;
      int? organizationId1 = this.Year_Header.Current.OrganizationID;
      int? organizationId2 = year.OrganizationID;
      return organizationId1.GetValueOrDefault() == organizationId2.GetValueOrDefault() & organizationId1.HasValue == organizationId2.HasValue;
    })))
    {
      ap1099Year.Status = "N";
      cache.Update((object) ap1099Year);
    }
    if (cache.IsInsertedUpdatedDeleted)
    {
      this.Actions.PressSave();
      PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => { }));
    }
    return (IEnumerable) list;
  }

  public AP1099SummaryEnq()
  {
    PX.Objects.AP.APSetup current = this.APSetup.Current;
    PXUIFieldAttribute.SetEnabled<AP1099Box.boxNbr>(this.Year_Summary.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<AP1099Box.descr>(this.Year_Summary.Cache, (object) null, false);
    this.reportsFolder.MenuAutoOpen = true;
    this.reportsFolder.AddMenuAction((PXAction) this.year1099SummaryReport);
    this.reportsFolder.AddMenuAction((PXAction) this.year1099NECSummaryReport);
    this.reportsFolder.AddMenuAction((PXAction) this.year1099DetailReport);
    this.reportsFolder.AddMenuAction((PXAction) this.open1099PaymentsReport);
  }

  public string Caption() => string.Empty;

  [PXUIField(DisplayName = "Reports", MapEnableRights = PXCacheRights.Select)]
  [PXButton(SpecialType = PXSpecialButtonType.ReportsFolder)]
  protected virtual IEnumerable Reportsfolder(PXAdapter adapter) => adapter.Get();

  [PXUIField(DisplayName = "1099-MISC Year Summary", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable Year1099SummaryReport(PXAdapter adapter)
  {
    if (this.Year_Header.Current == null)
      return adapter.Get();
    PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID((PXGraph) this, this.Year_Header.Current.OrganizationID);
    BAccountR baccountR = organizationById == null || !organizationById.Reporting1099ByBranches.GetValueOrDefault() ? (BAccountR) PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<BqlField<PX.Objects.GL.DAC.Organization.bAccountID, IBqlInt>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) organizationById
    }) : throw new PXException("The report cannot be run from this form for a company that reports 1099-MISC by branches.");
    throw new PXReportRequiredException(new Dictionary<string, string>()
    {
      ["PayerBAccountID"] = baccountR?.AcctCD,
      ["FinYear"] = this.Year_Header.Current.FinYear
    }, "AP654000", "1099-MISC Year Summary");
  }

  [PXUIField(DisplayName = "1099-NEC Year Summary", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  public virtual IEnumerable Year1099NECSummaryReport(PXAdapter adapter)
  {
    if (this.Year_Header.Current == null)
      return adapter.Get();
    PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID((PXGraph) this, this.Year_Header.Current.OrganizationID);
    BAccountR baccountR = organizationById == null || !organizationById.Reporting1099ByBranches.GetValueOrDefault() ? (BAccountR) PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<BqlField<PX.Objects.GL.DAC.Organization.bAccountID, IBqlInt>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) organizationById
    }) : throw new PXException("The report cannot be run from this form for a company that reports 1099-MISC by branches.");
    throw new PXReportRequiredException(new Dictionary<string, string>()
    {
      ["PayerBAccountID"] = baccountR?.AcctCD,
      ["FinYear"] = this.Year_Header.Current.FinYear,
      ["Format"] = "NEC"
    }, "AP654000", "1099-NEC Year Summary");
  }

  [PXUIField(DisplayName = "1099 Year Details", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable Year1099DetailReport(PXAdapter adapter)
  {
    if (this.Year_Header.Current == null)
      return adapter.Get();
    PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID((PXGraph) this, this.Year_Header.Current.OrganizationID);
    BAccountR baccountR = organizationById == null || !organizationById.Reporting1099ByBranches.GetValueOrDefault() ? (BAccountR) PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<BqlField<PX.Objects.GL.DAC.Organization.bAccountID, IBqlInt>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) organizationById
    }) : throw new PXException("The report cannot be run from this form for a company that reports 1099-MISC by branches.");
    throw new PXReportRequiredException(new Dictionary<string, string>()
    {
      ["PayerBAccountID"] = baccountR?.AcctCD,
      ["FinYear"] = this.Year_Header.Current.FinYear
    }, "AP654500", "1099 Year Details");
  }

  [PXUIField(DisplayName = "Open 1099 Payments", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable Open1099PaymentsReport(PXAdapter adapter)
  {
    if (this.Year_Header.Current != null)
    {
      BAccountR baccountR = (BAccountR) PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<BqlField<PX.Objects.GL.DAC.Organization.bAccountID, IBqlInt>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) OrganizationMaint.FindOrganizationByID((PXGraph) this, this.Year_Header.Current.OrganizationID)
      });
      throw new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["OrgBAccountID"] = baccountR?.AcctCD,
        ["FinYear"] = this.Year_Header.Current.FinYear
      }, "AP656500", "Open 1099 Payments");
    }
    return adapter.Get();
  }
}
