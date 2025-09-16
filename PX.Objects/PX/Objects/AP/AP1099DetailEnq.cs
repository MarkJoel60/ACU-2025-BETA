// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.AP1099DetailEnq
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AP;

public class AP1099DetailEnq : PXGraph<
#nullable disable
AP1099DetailEnq>
{
  public PXFilter<AP1099YearMaster> YearVendorHeader;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoinGroupBy<AP1099Box, LeftJoin<AP1099History, On<AP1099History.vendorID, Equal<Current<AP1099YearMaster.vendorID>>, And<AP1099History.boxNbr, Equal<AP1099Box.boxNbr>, And<AP1099History.finYear, Equal<Current<AP1099YearMaster.finYear>>>>>>, Where<boolTrue, Equal<boolTrue>>, PX.Data.Aggregate<GroupBy<AP1099Box.boxNbr, Sum<AP1099History.histAmt>>>, PX.Data.OrderBy<BqlField<
  #nullable enable
  AP1099Box.boxCD, IBqlString>.Asc>> YearVendorSummary;
  public 
  #nullable disable
  PXCancel<AP1099YearMaster> Cancel;
  public PXAction<AP1099YearMaster> firstVendor;
  public PXAction<AP1099YearMaster> previousVendor;
  public PXAction<AP1099YearMaster> nextVendor;
  public PXAction<AP1099YearMaster> lastVendor;
  public PXAction<AP1099YearMaster> reportsFolder;
  public PXAction<AP1099YearMaster> year1099SummaryReport;
  public PXAction<AP1099YearMaster> year1099DetailReport;
  public PXAction<AP1099YearMaster> year1099NECSummaryReport;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;

  public IEnumerable yearVendorSummary()
  {
    this.YearVendorSummary.Cache.ClearQueryCache();
    int?[] array1 = this.YearVendorHeader.Current.OrganizationID.SingleToArray<int?>();
    int? branchId = this.YearVendorHeader.Current.BranchID;
    ref int? local = ref branchId;
    int?[] array2 = local.HasValue ? local.GetValueOrDefault().SingleToArray<int>().Cast<int?>().ToArray<int?>() : (int?[]) null;
    using (new PXReadBranchRestrictedScope(array1, array2))
      return (IEnumerable) PXSelectBase<AP1099Box, PXSelectJoinGroupBy<AP1099Box, LeftJoin<AP1099History, On<AP1099History.vendorID, Equal<Current<AP1099YearMaster.vendorID>>, And<AP1099History.boxNbr, Equal<AP1099Box.boxNbr>, And<AP1099History.finYear, Equal<Current<AP1099YearMaster.finYear>>>>>>, Where<Current<AP1099YearMaster.organizationID>, PX.Data.IsNotNull>, PX.Data.Aggregate<GroupBy<AP1099Box.boxNbr, Sum<AP1099History.histAmt>>>>.Config>.Select((PXGraph) this);
  }

  [Box1099NumberSelector]
  [PXMergeAttributes(Method = MergeMethod.Append)]
  public virtual void AP1099Box_BoxNbr_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<AP1099YearMaster, AP1099YearMaster.organizationID> e)
  {
    e.Row.BranchID = new int?();
  }

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXFirstButton]
  public virtual IEnumerable FirstVendor(PXAdapter adapter)
  {
    AP1099YearMaster current = this.YearVendorHeader.Current;
    Vendor vendor = (Vendor) PXSelectBase<Vendor, PXSelect<Vendor, Where<Vendor.vendor1099, Equal<True>>, PX.Data.OrderBy<Asc<Vendor.acctCD>>>.Config>.Select((PXGraph) this);
    if (vendor != null)
      current.VendorID = vendor.BAccountID;
    return adapter.Get();
  }

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXPreviousButton]
  public virtual IEnumerable PreviousVendor(PXAdapter adapter)
  {
    AP1099YearMaster current = this.YearVendorHeader.Current;
    Vendor vendor1 = (Vendor) PXSelectBase<Vendor, PXSelect<Vendor, Where<Vendor.bAccountID, Equal<Current<AP1099YearMaster.vendorID>>>>.Config>.Select((PXGraph) this);
    if (vendor1 == null)
    {
      vendor1 = new Vendor();
      vendor1.AcctCD = "";
    }
    Vendor vendor2 = (Vendor) PXSelectBase<Vendor, PXSelect<Vendor, Where<Vendor.vendor1099, Equal<True>, And<Vendor.acctCD, Less<Required<Vendor.acctCD>>>>>.Config>.Select((PXGraph) this, (object) vendor1.AcctCD);
    if (vendor2 != null)
      current.VendorID = vendor2.BAccountID;
    return adapter.Get();
  }

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXNextButton]
  public virtual IEnumerable NextVendor(PXAdapter adapter)
  {
    AP1099YearMaster current = this.YearVendorHeader.Current;
    Vendor vendor1 = (Vendor) PXSelectBase<Vendor, PXSelect<Vendor, Where<Vendor.bAccountID, Equal<Current<AP1099YearMaster.vendorID>>>>.Config>.Select((PXGraph) this);
    if (vendor1 == null)
    {
      vendor1 = new Vendor();
      vendor1.AcctCD = "";
    }
    Vendor vendor2 = (Vendor) PXSelectBase<Vendor, PXSelect<Vendor, Where<Vendor.vendor1099, Equal<True>, And<Vendor.acctCD, Greater<Required<Vendor.acctCD>>>>>.Config>.Select((PXGraph) this, (object) vendor1.AcctCD);
    if (vendor2 != null)
      current.VendorID = vendor2.BAccountID;
    return adapter.Get();
  }

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXLastButton]
  public virtual IEnumerable LastVendor(PXAdapter adapter)
  {
    AP1099YearMaster current = this.YearVendorHeader.Current;
    Vendor vendor = (Vendor) PXSelectBase<Vendor, PXSelect<Vendor, Where<Vendor.vendor1099, Equal<True>>, PX.Data.OrderBy<Desc<Vendor.acctCD>>>.Config>.Select((PXGraph) this);
    if (vendor != null)
      current.VendorID = vendor.BAccountID;
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Reports", MapEnableRights = PXCacheRights.Select)]
  [PXButton(SpecialType = PXSpecialButtonType.ReportsFolder)]
  protected virtual IEnumerable Reportsfolder(PXAdapter adapter) => adapter.Get();

  public virtual void AP1099YearMaster_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    AP1099YearMaster row = (AP1099YearMaster) e.Row;
    if (row == null)
      return;
    bool isEnabled = row.VendorID.HasValue && row.FinYear != null;
    this.year1099SummaryReport.SetEnabled(isEnabled);
    this.year1099NECSummaryReport.SetEnabled(isEnabled);
    this.year1099DetailReport.SetEnabled(isEnabled);
  }

  [PXUIField(DisplayName = "1099-MISC Year Summary", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable Year1099SummaryReport(PXAdapter adapter)
  {
    AP1099YearMaster current = this.YearVendorHeader.Current;
    if (current != null)
    {
      BAccountR baccountR = (BAccountR) PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, (object) current.OrgBAccountID);
      throw new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["PayerBAccountID"] = baccountR?.AcctCD,
        ["FinYear"] = current.FinYear
      }, "AP654000", "1099-MISC Year Summary");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "1099-NEC Year Summary", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  public virtual IEnumerable Year1099NECSummaryReport(PXAdapter adapter)
  {
    AP1099YearMaster current = this.YearVendorHeader.Current;
    if (current != null)
    {
      BAccountR baccountR = (BAccountR) PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, (object) current.OrgBAccountID);
      throw new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["PayerBAccountID"] = baccountR?.AcctCD,
        ["FinYear"] = current.FinYear,
        ["Format"] = "NEC"
      }, "AP654000", "1099-NEC Year Summary");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "1099 Year Details", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable Year1099DetailReport(PXAdapter adapter)
  {
    AP1099YearMaster current = this.YearVendorHeader.Current;
    if (current != null)
    {
      BAccountR baccountR = (BAccountR) PXSelectBase<BAccountR, PXViewOf<BAccountR>.BasedOn<SelectFromBase<BAccountR, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, (object) current.OrgBAccountID);
      throw new PXReportRequiredException(new Dictionary<string, string>()
      {
        ["PayerBAccountID"] = baccountR?.AcctCD,
        ["FinYear"] = current.FinYear
      }, "AP654500", "1099 Year Details");
    }
    return adapter.Get();
  }

  public AP1099DetailEnq()
  {
    PX.Objects.AP.APSetup current = this.APSetup.Current;
    PXUIFieldAttribute.SetEnabled<AP1099Box.boxNbr>(this.YearVendorSummary.Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<AP1099Box.descr>(this.YearVendorSummary.Cache, (object) null, false);
    PXUIFieldAttribute.SetRequired<AP1099YearMaster.vendorID>(this.YearVendorHeader.Cache, true);
    this.reportsFolder.MenuAutoOpen = true;
    this.reportsFolder.AddMenuAction((PXAction) this.year1099SummaryReport);
    this.reportsFolder.AddMenuAction((PXAction) this.year1099NECSummaryReport);
    this.reportsFolder.AddMenuAction((PXAction) this.year1099DetailReport);
  }
}
