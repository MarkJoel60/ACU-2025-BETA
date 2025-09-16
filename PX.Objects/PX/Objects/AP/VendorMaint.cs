// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Export;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.Descriptor;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CR.Extensions.CRCreateActions;
using PX.Objects.CR.Extensions.Relational;
using PX.Objects.CR.GraphExtensions;
using PX.Objects.CS;
using PX.Objects.CS.Attributes;
using PX.Objects.GDPR;
using PX.Objects.GL;
using PX.Objects.GraphExtensions.ExtendBAccount;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AP;

public class VendorMaint : PXGraph<
#nullable disable
VendorMaint, VendorR>
{
  protected LocationValidator LocationValidator;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (Vendor.vStatus)})]
  [PXViewName("Customer")]
  public PXSelect<VendorR, Where2<Match<Current<AccessInfo.userName>>, PX.Data.And<Where<PX.Objects.CR.BAccount.type, Equal<BAccountType.vendorType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.combinedType>>>>>> BAccount;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Standalone.Location> BaseLocations;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Address> AddressDummy;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Contact> ContactDummy;
  public PXSelect<BAccountItself, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Optional<PX.Objects.CR.BAccount.bAccountID>>>> CurrentBAccountItself;
  public PXSetup<PX.Objects.GL.Company> cmpany;
  public PXSelect<Vendor, Where<Vendor.bAccountID, Equal<Current<Vendor.bAccountID>>>> CurrentVendor;
  public PXSetup<PX.Objects.AP.VendorClass, Where<PX.Objects.AP.VendorClass.vendorClassID, Equal<Optional<Vendor.vendorClassID>>>> VendorClass;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;
  [PXCopyPasteHiddenView]
  public PXSelect<VendorMaint.VendorBalanceSummary> VendorBalance;
  [PXCopyPasteHiddenView]
  public PXSelect<VendorBalanceSummaryByBaseCurrency> VendorBalanceByBaseCurrency;
  public CRNotificationSourceList<Vendor, Vendor.vendorClassID, APNotificationSource.vendor> NotificationSources;
  public CRNotificationRecipientList<Vendor, Vendor.vendorClassID> NotificationRecipients;
  public PXSelect<POVendorInventory, Where<POVendorInventory.vendorID, Equal<Current<Vendor.bAccountID>>>> VendorItems;
  [Obsolete("The view is obsolete and will be eliminated in Acumatica 8.0")]
  [PXCopyPasteHiddenView]
  public PXSelect<TaxPeriod> taxperiods;
  public PXSelectJoin<TaxTranReport, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<TaxTranReport.taxID>>, InnerJoin<TaxReportLine, On<TaxReportLine.vendorID, Equal<PX.Objects.TX.Tax.taxVendorID>>, InnerJoin<TaxBucketLine, On<TaxBucketLine.vendorID, Equal<TaxReportLine.vendorID>, And<TaxBucketLine.lineNbr, Equal<TaxReportLine.lineNbr>, And<TaxBucketLine.bucketID, Equal<TaxTranReport.taxBucketID>>>>>>>, Where<PX.Objects.TX.Tax.taxVendorID, Equal<Required<TaxPeriodFilter.vendorID>>, And<TaxTranReport.released, Equal<PX.Data.True>, And<TaxTranReport.voided, Equal<False>, And<TaxTranReport.taxPeriodID, PX.Data.IsNull, And<TaxTranReport.taxType, NotEqual<TaxType.pendingPurchase>, And<TaxTranReport.taxType, NotEqual<TaxType.pendingSales>, And<TaxTranReport.origRefNbr, Equal<PX.Data.Empty>>>>>>>>, PX.Data.OrderBy<Asc<TaxTranReport.tranDate>>> OldestNotReportedTaxTran;
  [PXViewName("Answers")]
  public CRAttributeList<Vendor> Answers;
  [PXCopyPasteHiddenView]
  public PXSelect<VendorMaint.SuppliedByVendor> SuppliedByVendors;
  [PXHidden]
  public PXSelect<INItemXRef> xrefs;
  [PXHidden]
  public PXSetupOptional<PX.Objects.CR.CRSetup> CRSetup;
  public PXAction<VendorR> viewRestrictionGroups;
  public PXDBAction<VendorR> viewBusnessAccount;
  public PXAction<VendorR> viewBalanceDetails;
  public PXAction<VendorR> newBillAdjustment;
  public PXAction<VendorR> newManualCheck;
  public PXAction<VendorR> vendorDetails;
  public PXAction<VendorR> approveBillsForPayments;
  public PXAction<VendorR> payBills;
  public PXAction<VendorR> vendorPrice;
  public PXAction<VendorR> balanceByVendor;
  public PXAction<VendorR> vendorHistory;
  public PXAction<VendorR> aPAgedPastDue;
  public PXAction<VendorR> aPAgedOutstanding;
  public PXAction<VendorR> aPDocumentRegister;
  public PXAction<VendorR> repVendorDetails;
  public PXAction<VendorR> action;
  public PXAction<VendorR> inquiry;
  public PXAction<VendorR> report;
  public PXChangeBAccountID<VendorR, VendorR.acctCD> ChangeID;
  private bool doCopyClassSettings;

  [InjectDependency]
  internal IBAccountRestrictionHelper BAccountRestrictionHelper { get; set; }

  public static Vendor FindByID(PXGraph graph, int? bAccountID)
  {
    return (Vendor) PXSelectBase<Vendor, PXSelect<Vendor, Where2<Where<Vendor.type, Equal<BAccountType.vendorType>, Or<Vendor.type, Equal<BAccountType.combinedType>>>, And<Vendor.bAccountID, Equal<Required<Vendor.bAccountID>>>>>.Config>.Select(graph, (object) bAccountID);
  }

  public static Vendor GetByID(PXGraph graph, int? bAccountID)
  {
    return VendorMaint.FindByID(graph, bAccountID) ?? throw new PXException("{0} with ID '{1}' does not exist", new object[2]
    {
      (object) EntityHelper.GetFriendlyEntityName(typeof (Vendor)),
      (object) bAccountID
    });
  }

  [PXSelector(typeof (Search<NotificationSetup.setupID, Where<NotificationSetup.sourceCD, Equal<APNotificationSource.vendor>>>), DescriptionField = typeof (NotificationSetup.notificationCD), SelectorMode = PXSelectorMode.NoAutocomplete | PXSelectorMode.DisplayModeText)]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void NotificationSource_SetupID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXCheckUnique(new System.Type[] {typeof (NotificationSource.setupID)}, IgnoreNulls = false, Where = typeof (Where<NotificationSource.refNoteID, Equal<Current<NotificationSource.refNoteID>>>))]
  protected virtual void NotificationSource_NBranchID_CacheAttached(PXCache sender)
  {
  }

  [PXSelector(typeof (Search<PX.SM.SiteMap.screenID, Where2<Where<PX.SM.SiteMap.url, Like<PX.Objects.Common.urlReports>, Or<PX.SM.SiteMap.url, Like<urlReportsInNewUi>>>, PX.Data.And<Where<PX.SM.SiteMap.screenID, Like<PXModule.ap_>, Or<PX.SM.SiteMap.screenID, Like<PXModule.cl_>, Or<PX.SM.SiteMap.screenID, Like<PXModule.po_>, Or<PX.SM.SiteMap.screenID, Like<PXModule.sc_>, Or<PX.SM.SiteMap.screenID, Like<PXModule.rq_>>>>>>>>, PX.Data.OrderBy<Asc<PX.SM.SiteMap.screenID>>>), new System.Type[] {typeof (PX.SM.SiteMap.screenID), typeof (PX.SM.SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (PX.SM.SiteMap.title))]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void NotificationSource_ReportID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [VendorContactType.List]
  [PXCheckDistinct(new System.Type[] {typeof (NotificationRecipient.contactID)}, Where = typeof (Where<NotificationRecipient.sourceID, Equal<Current<NotificationRecipient.sourceID>>, And<NotificationRecipient.refNoteID, Equal<Current<Vendor.noteID>>>>))]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void NotificationRecipient_ContactType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(10, IsUnicode = true)]
  protected virtual void NotificationRecipient_ClassID_CacheAttached(PXCache sender)
  {
  }

  public virtual PXSelectBase<VendorR> BAccountAccessor => (PXSelectBase<VendorR>) this.BAccount;

  public VendorMaint()
  {
    PX.Objects.AP.APSetup current = this.APSetup.Current;
    this.Views.Caches.Remove(typeof (Vendor));
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Contact.fullName>(this.Caches[typeof (PX.Objects.CR.Contact)], (object) null);
    PXUIFieldAttribute.SetVisible<Vendor.localeName>(this.BAccount.Cache, (object) null, PXDBLocalizableStringAttribute.HasMultipleLocales);
    this.FieldDefaulting.AddHandler<BAccountR.type>((PXFieldDefaulting) ((sender, e) =>
    {
      if (e.Row == null)
        return;
      e.NewValue = (object) "VE";
    }));
    PXUIFieldAttribute.SetDisplayName<VendorR.acctName>(this.BAccount.Cache, "Account Name");
  }

  [PXUIField(DisplayName = "Manage Restriction Groups", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable ViewRestrictionGroups(PXAdapter adapter)
  {
    if (this.CurrentVendor.Current != null)
    {
      APAccessDetail instance = PXGraph.CreateInstance<APAccessDetail>();
      instance.Vendor.Current = (Vendor) instance.Vendor.Search<Vendor.acctCD>((object) this.CurrentVendor.Current.AcctCD);
      throw new PXRedirectRequiredException((PXGraph) instance, false, "Restricted Groups");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Account", Enabled = false, Visible = true, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable ViewBusnessAccount(PXAdapter adapter)
  {
    PX.Objects.CR.BAccount current = (PX.Objects.CR.BAccount) this.BAccount.Current;
    if (current != null)
    {
      BusinessAccountMaint instance = PXGraph.CreateInstance<BusinessAccountMaint>();
      instance.Load();
      instance.Clear();
      instance.BAccount.Current = (PX.Objects.CR.BAccount) instance.BAccount.Search<PX.Objects.CR.BAccount.acctCD>((object) current.AcctCD);
      throw new PXRedirectRequiredException((PXGraph) instance, "Edit Business Account");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Balance Details", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable ViewBalanceDetails(PXAdapter adapter)
  {
    Vendor current = (Vendor) this.BAccount.Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      {
        APVendorBalanceEnq instance = PXGraph.CreateInstance<APVendorBalanceEnq>();
        instance.Clear();
        instance.Filter.Current.VendorID = current.BAccountID;
        throw new PXRedirectRequiredException((PXGraph) instance, nameof (ViewBalanceDetails));
      }
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Create Bill", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable NewBillAdjustment(PXAdapter adapter)
  {
    Vendor current = (Vendor) this.BAccountAccessor.Current;
    if (current != null)
    {
      int? nullable1 = current.BAccountID;
      long? nullable2 = nullable1.HasValue ? new long?((long) nullable1.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable2.GetValueOrDefault() > num & nullable2.HasValue)
      {
        APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
        instance.Clear();
        APInvoice data = instance.Document.Insert(new APInvoice());
        APInvoice apInvoice = data;
        nullable1 = new int?();
        int? nullable3 = nullable1;
        apInvoice.BranchID = nullable3;
        instance.Document.Cache.SetValueExt<APInvoice.vendorID>((object) data, (object) current.BAccountID);
        if (current.CuryID != null)
          instance.Document.Cache.SetValueExt<APInvoice.curyID>((object) data, (object) current.CuryID);
        instance.Document.Cache.SetDefaultExt<APInvoice.finPeriodID>((object) data);
        instance.Document.Cache.SetDefaultExt<APInvoice.tranPeriodID>((object) data);
        throw new PXRedirectRequiredException((PXGraph) instance, "Create Bill");
      }
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Create Payment", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable NewManualCheck(PXAdapter adapter)
  {
    Vendor current = (Vendor) this.BAccountAccessor.Current;
    if (current != null)
    {
      int? nullable1 = current.BAccountID;
      long? nullable2 = nullable1.HasValue ? new long?((long) nullable1.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable2.GetValueOrDefault() > num & nullable2.HasValue)
      {
        APPaymentEntry instance = PXGraph.CreateInstance<APPaymentEntry>();
        instance.Clear();
        APPayment data = instance.Document.Insert(new APPayment());
        APPayment apPayment = data;
        nullable1 = new int?();
        int? nullable3 = nullable1;
        apPayment.BranchID = nullable3;
        instance.Document.Cache.SetValueExt<APPayment.vendorID>((object) data, (object) current.BAccountID);
        instance.Document.Cache.SetDefaultExt<APPayment.adjFinPeriodID>((object) data);
        instance.Document.Cache.SetDefaultExt<APPayment.adjTranPeriodID>((object) data);
        instance.Document.Cache.SetDefaultExt<APPayment.finPeriodID>((object) data);
        instance.Document.Cache.SetDefaultExt<APPayment.tranPeriodID>((object) data);
        instance.Document.Cache.Update((object) data);
        throw new PXRedirectRequiredException((PXGraph) instance, "Create Payment");
      }
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Vendor Details", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable VendorDetails(PXAdapter adapter)
  {
    Vendor current = (Vendor) this.BAccountAccessor.Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      {
        APDocumentEnq instance = PXGraph.CreateInstance<APDocumentEnq>();
        instance.Clear();
        instance.Filter.Current.VendorID = current.BAccountID;
        instance.Filter.Select();
        throw new PXRedirectRequiredException((PXGraph) instance, "Vendor Details");
      }
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Approve Bills for Payment", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable ApproveBillsForPayments(PXAdapter adapter)
  {
    Vendor current = (Vendor) this.BAccountAccessor.Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      {
        APApproveBills instance = PXGraph.CreateInstance<APApproveBills>();
        instance.Clear();
        instance.Filter.Current.VendorID = current.BAccountID;
        throw new PXRedirectRequiredException((PXGraph) instance, "Approve Bills for Payment");
      }
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Pay Bills", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable PayBills(PXAdapter adapter)
  {
    Vendor current = (Vendor) this.BAccountAccessor.Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      {
        APPayBills instance = PXGraph.CreateInstance<APPayBills>();
        instance.Clear();
        instance.Filter.Current.VendorID = current.BAccountID;
        throw new PXRedirectRequiredException((PXGraph) instance, "Pay Bills");
      }
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Vendor Prices", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable VendorPrice(PXAdapter adapter)
  {
    Vendor current = (Vendor) this.BAccountAccessor.Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      {
        APVendorPriceMaint instance = PXGraph.CreateInstance<APVendorPriceMaint>();
        instance.Filter.Current.VendorID = current.BAccountID;
        throw new PXRedirectRequiredException((PXGraph) instance, "Vendor Prices");
      }
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "AP Balance by Vendor", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable BalanceByVendor(PXAdapter adapter)
  {
    Vendor current = (Vendor) this.BAccountAccessor.Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
        throw new PXReportRequiredException(new Dictionary<string, string>()
        {
          ["OrgBAccountID"] = (string) null,
          ["VendorID"] = current.AcctCD
        }, "AP632500", "AP Balance by Vendor");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Vendor History", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable VendorHistory(PXAdapter adapter)
  {
    Vendor current = (Vendor) this.BAccountAccessor.Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
        throw new PXReportRequiredException(new Dictionary<string, string>()
        {
          ["OrgBAccountID"] = (string) null,
          ["VendorID"] = current.AcctCD
        }, "AP652000", "Vendor History");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "AP Aging", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable APAgedPastDue(PXAdapter adapter)
  {
    Vendor current = (Vendor) this.BAccountAccessor.Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
        throw new PXReportRequiredException(new Dictionary<string, string>()
        {
          ["OrgBAccountID"] = (string) null,
          ["VendorID"] = current.AcctCD
        }, "AP631000", "AP Aging");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "AP Coming Due", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable APAgedOutstanding(PXAdapter adapter)
  {
    Vendor current = (Vendor) this.BAccountAccessor.Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
        throw new PXReportRequiredException(new Dictionary<string, string>()
        {
          ["OrgBAccountID"] = (string) null,
          ["VendorID"] = current.AcctCD
        }, "AP631500", "AP Coming Due");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "AP Register", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable APDocumentRegister(PXAdapter adapter)
  {
    Vendor current = (Vendor) this.BAccountAccessor.Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
        throw new PXReportRequiredException(new Dictionary<string, string>()
        {
          ["OrgBAccountID"] = (string) null,
          ["VendorID"] = current.AcctCD
        }, "AP621500", "AP Register");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Vendor Profile", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable RepVendorDetails(PXAdapter adapter)
  {
    Vendor current = (Vendor) this.BAccountAccessor.Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
        throw new PXReportRequiredException(new Dictionary<string, string>()
        {
          ["VendorID"] = current.AcctCD
        }, "AP655500", "Vendor Profile");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Actions", MapEnableRights = PXCacheRights.Select)]
  [PXButton(SpecialType = PXSpecialButtonType.ActionsFolder, MenuAutoOpen = true)]
  protected virtual IEnumerable Action(PXAdapter adapter) => adapter.Get();

  [PXUIField(DisplayName = "Inquiries", MapEnableRights = PXCacheRights.Select)]
  [PXButton(SpecialType = PXSpecialButtonType.InquiriesFolder, MenuAutoOpen = true)]
  protected virtual IEnumerable Inquiry(PXAdapter adapter) => adapter.Get();

  [PXUIField(DisplayName = "Reports", MapEnableRights = PXCacheRights.Select)]
  [PXButton(SpecialType = PXSpecialButtonType.ReportsFolder, MenuAutoOpen = true)]
  protected virtual IEnumerable Report(PXAdapter adapter) => adapter.Get();

  protected virtual IEnumerable vendorBalance()
  {
    Vendor current = (Vendor) this.BAccountAccessor.Current;
    List<VendorMaint.VendorBalanceSummary> vendorBalanceSummaryList = new List<VendorMaint.VendorBalanceSummary>(1);
    if (this.BAccountAccessor.Cache.GetStatus((object) current) != PXEntryStatus.Inserted)
    {
      PXSelectJoinGroupBy<APVendorBalanceEnq.APLatestHistory, LeftJoin<CuryAPHistory, On<APVendorBalanceEnq.APLatestHistory.branchID, Equal<CuryAPHistory.branchID>, And<APVendorBalanceEnq.APLatestHistory.accountID, Equal<CuryAPHistory.accountID>, And<APVendorBalanceEnq.APLatestHistory.vendorID, Equal<CuryAPHistory.vendorID>, And<APVendorBalanceEnq.APLatestHistory.subID, Equal<CuryAPHistory.subID>, And<APVendorBalanceEnq.APLatestHistory.curyID, Equal<CuryAPHistory.curyID>, And<APVendorBalanceEnq.APLatestHistory.lastActivityPeriod, Equal<CuryAPHistory.finPeriodID>>>>>>>>, Where<APVendorBalanceEnq.APLatestHistory.vendorID, Equal<Current<Vendor.bAccountID>>>, PX.Data.Aggregate<Sum<CuryAPHistory.finBegBalance, Sum<CuryAPHistory.curyFinBegBalance, Sum<CuryAPHistory.finYtdBalance, Sum<CuryAPHistory.curyFinYtdBalance, Sum<CuryAPHistory.tranBegBalance, Sum<CuryAPHistory.curyTranBegBalance, Sum<CuryAPHistory.tranYtdBalance, Sum<CuryAPHistory.curyTranYtdBalance, Sum<CuryAPHistory.finPtdPayments, Sum<CuryAPHistory.finPtdPurchases, Sum<CuryAPHistory.finPtdDiscTaken, Sum<CuryAPHistory.finPtdWhTax, Sum<CuryAPHistory.finPtdCrAdjustments, Sum<CuryAPHistory.finPtdDrAdjustments, Sum<CuryAPHistory.finPtdRGOL, Sum<CuryAPHistory.finPtdDeposits, Sum<CuryAPHistory.finYtdDeposits, Sum<CuryAPHistory.finPtdRetainageWithheld, Sum<CuryAPHistory.finYtdRetainageWithheld, Sum<CuryAPHistory.finPtdRetainageReleased, Sum<CuryAPHistory.finYtdRetainageReleased, Sum<CuryAPHistory.tranPtdPayments, Sum<CuryAPHistory.tranPtdPurchases, Sum<CuryAPHistory.tranPtdDiscTaken, Sum<CuryAPHistory.tranPtdWhTax, Sum<CuryAPHistory.tranPtdCrAdjustments, Sum<CuryAPHistory.tranPtdDrAdjustments, Sum<CuryAPHistory.tranPtdRGOL, Sum<CuryAPHistory.tranPtdDeposits, Sum<CuryAPHistory.tranYtdDeposits, Sum<CuryAPHistory.tranPtdRetainageWithheld, Sum<CuryAPHistory.tranYtdRetainageWithheld, Sum<CuryAPHistory.tranPtdRetainageReleased, Sum<CuryAPHistory.tranYtdRetainageReleased, Sum<CuryAPHistory.curyFinPtdPayments, Sum<CuryAPHistory.curyFinPtdPurchases, Sum<CuryAPHistory.curyFinPtdDiscTaken, Sum<CuryAPHistory.curyFinPtdWhTax, Sum<CuryAPHistory.curyFinPtdCrAdjustments, Sum<CuryAPHistory.curyFinPtdDrAdjustments, Sum<CuryAPHistory.curyFinPtdDeposits, Sum<CuryAPHistory.curyFinYtdDeposits, Sum<CuryAPHistory.curyFinPtdRetainageWithheld, Sum<CuryAPHistory.curyFinYtdRetainageWithheld, Sum<CuryAPHistory.curyFinPtdRetainageReleased, Sum<CuryAPHistory.curyFinYtdRetainageReleased, Sum<CuryAPHistory.curyTranPtdPayments, Sum<CuryAPHistory.curyTranPtdPurchases, Sum<CuryAPHistory.curyTranPtdDiscTaken, Sum<CuryAPHistory.curyTranPtdWhTax, Sum<CuryAPHistory.curyTranPtdCrAdjustments, Sum<CuryAPHistory.curyTranPtdDrAdjustments, Sum<CuryAPHistory.curyTranPtdDeposits, Sum<CuryAPHistory.curyTranYtdDeposits, Sum<CuryAPHistory.curyTranPtdRetainageWithheld, Sum<CuryAPHistory.curyTranYtdRetainageWithheld, Sum<CuryAPHistory.curyTranPtdRetainageReleased, Sum<CuryAPHistory.curyTranYtdRetainageReleased>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> selectJoinGroupBy = new PXSelectJoinGroupBy<APVendorBalanceEnq.APLatestHistory, LeftJoin<CuryAPHistory, On<APVendorBalanceEnq.APLatestHistory.branchID, Equal<CuryAPHistory.branchID>, And<APVendorBalanceEnq.APLatestHistory.accountID, Equal<CuryAPHistory.accountID>, And<APVendorBalanceEnq.APLatestHistory.vendorID, Equal<CuryAPHistory.vendorID>, And<APVendorBalanceEnq.APLatestHistory.subID, Equal<CuryAPHistory.subID>, And<APVendorBalanceEnq.APLatestHistory.curyID, Equal<CuryAPHistory.curyID>, And<APVendorBalanceEnq.APLatestHistory.lastActivityPeriod, Equal<CuryAPHistory.finPeriodID>>>>>>>>, Where<APVendorBalanceEnq.APLatestHistory.vendorID, Equal<Current<Vendor.bAccountID>>>, PX.Data.Aggregate<Sum<CuryAPHistory.finBegBalance, Sum<CuryAPHistory.curyFinBegBalance, Sum<CuryAPHistory.finYtdBalance, Sum<CuryAPHistory.curyFinYtdBalance, Sum<CuryAPHistory.tranBegBalance, Sum<CuryAPHistory.curyTranBegBalance, Sum<CuryAPHistory.tranYtdBalance, Sum<CuryAPHistory.curyTranYtdBalance, Sum<CuryAPHistory.finPtdPayments, Sum<CuryAPHistory.finPtdPurchases, Sum<CuryAPHistory.finPtdDiscTaken, Sum<CuryAPHistory.finPtdWhTax, Sum<CuryAPHistory.finPtdCrAdjustments, Sum<CuryAPHistory.finPtdDrAdjustments, Sum<CuryAPHistory.finPtdRGOL, Sum<CuryAPHistory.finPtdDeposits, Sum<CuryAPHistory.finYtdDeposits, Sum<CuryAPHistory.finPtdRetainageWithheld, Sum<CuryAPHistory.finYtdRetainageWithheld, Sum<CuryAPHistory.finPtdRetainageReleased, Sum<CuryAPHistory.finYtdRetainageReleased, Sum<CuryAPHistory.tranPtdPayments, Sum<CuryAPHistory.tranPtdPurchases, Sum<CuryAPHistory.tranPtdDiscTaken, Sum<CuryAPHistory.tranPtdWhTax, Sum<CuryAPHistory.tranPtdCrAdjustments, Sum<CuryAPHistory.tranPtdDrAdjustments, Sum<CuryAPHistory.tranPtdRGOL, Sum<CuryAPHistory.tranPtdDeposits, Sum<CuryAPHistory.tranYtdDeposits, Sum<CuryAPHistory.tranPtdRetainageWithheld, Sum<CuryAPHistory.tranYtdRetainageWithheld, Sum<CuryAPHistory.tranPtdRetainageReleased, Sum<CuryAPHistory.tranYtdRetainageReleased, Sum<CuryAPHistory.curyFinPtdPayments, Sum<CuryAPHistory.curyFinPtdPurchases, Sum<CuryAPHistory.curyFinPtdDiscTaken, Sum<CuryAPHistory.curyFinPtdWhTax, Sum<CuryAPHistory.curyFinPtdCrAdjustments, Sum<CuryAPHistory.curyFinPtdDrAdjustments, Sum<CuryAPHistory.curyFinPtdDeposits, Sum<CuryAPHistory.curyFinYtdDeposits, Sum<CuryAPHistory.curyFinPtdRetainageWithheld, Sum<CuryAPHistory.curyFinYtdRetainageWithheld, Sum<CuryAPHistory.curyFinPtdRetainageReleased, Sum<CuryAPHistory.curyFinYtdRetainageReleased, Sum<CuryAPHistory.curyTranPtdPayments, Sum<CuryAPHistory.curyTranPtdPurchases, Sum<CuryAPHistory.curyTranPtdDiscTaken, Sum<CuryAPHistory.curyTranPtdWhTax, Sum<CuryAPHistory.curyTranPtdCrAdjustments, Sum<CuryAPHistory.curyTranPtdDrAdjustments, Sum<CuryAPHistory.curyTranPtdDeposits, Sum<CuryAPHistory.curyTranYtdDeposits, Sum<CuryAPHistory.curyTranPtdRetainageWithheld, Sum<CuryAPHistory.curyTranYtdRetainageWithheld, Sum<CuryAPHistory.curyTranPtdRetainageReleased, Sum<CuryAPHistory.curyTranYtdRetainageReleased>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>((PXGraph) this);
      VendorMaint.VendorBalanceSummary aRes = new VendorMaint.VendorBalanceSummary();
      object[] objArray = Array.Empty<object>();
      foreach (PXResult<APVendorBalanceEnq.APLatestHistory, CuryAPHistory> pxResult in selectJoinGroupBy.Select(objArray))
      {
        CuryAPHistory aSrc = (CuryAPHistory) pxResult;
        this.Aggregate(aRes, aSrc);
      }
      vendorBalanceSummaryList.Add(aRes);
    }
    return (IEnumerable) vendorBalanceSummaryList;
  }

  protected virtual IEnumerable vendorBalanceByBaseCurrency()
  {
    yield break;
  }

  public override void InitCacheMapping(Dictionary<System.Type, System.Type> map)
  {
    base.InitCacheMapping(map);
    this.Caches.AddCacheMappingsWithInheritance((PXGraph) this, typeof (VendorR));
    this.Caches.AddCacheMappingsWithInheritance((PXGraph) this, typeof (PX.Objects.CR.Standalone.Location));
  }

  public override void Persist()
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      bool flag;
      try
      {
        this.BAccountRestrictionHelper.Persist();
        flag = this.Persist(typeof (Vendor), PXDBOperation.Update) > 0;
      }
      catch
      {
        this.Caches[typeof (Vendor)].Persisted(true);
        throw;
      }
      base.Persist();
      if (flag)
        this.SelectTimeStamp();
      transactionScope.Complete();
    }
  }

  protected virtual void Vendor_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    Vendor row = (Vendor) e.Row;
    if (row == null)
      return;
    this.VendorBalanceByBaseCurrency.AllowSelect = false;
    this.VendorBalanceByBaseCurrency.AllowDelete = false;
    this.VendorBalanceByBaseCurrency.AllowInsert = false;
    this.VendorBalanceByBaseCurrency.AllowUpdate = false;
    bool flag1 = cache.GetStatus((object) row) != PXEntryStatus.Inserted || row.AcctCD != null;
    this.viewBusnessAccount.SetEnabled(flag1);
    this.newBillAdjustment.SetEnabled(flag1);
    this.newManualCheck.SetEnabled(flag1);
    this.viewRestrictionGroups.SetEnabled(flag1);
    PXChangeBAccountID<VendorR, VendorR.acctCD> changeId = this.ChangeID;
    bool? nullable;
    int num1;
    if (flag1)
    {
      nullable = row.IsBranch;
      num1 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    changeId.SetEnabled(num1 != 0);
    this.vendorDetails.SetEnabled(flag1);
    this.approveBillsForPayments.SetEnabled(flag1);
    this.payBills.SetEnabled(flag1);
    this.vendorPrice.SetEnabled(flag1);
    this.balanceByVendor.SetEnabled(flag1);
    this.vendorHistory.SetEnabled(flag1);
    this.aPAgedPastDue.SetEnabled(flag1);
    this.aPAgedOutstanding.SetEnabled(flag1);
    this.aPDocumentRegister.SetEnabled(flag1);
    this.repVendorDetails.SetEnabled(flag1);
    PXCache cache1 = cache;
    Vendor data1 = row;
    nullable = row.IsBranch;
    int num2 = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<Vendor.taxAgency>(cache1, (object) data1, num2 != 0);
    bool isVisible = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>();
    PXUIFieldAttribute.SetVisible<Vendor.curyID>(cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<Vendor.curyRateTypeID>(cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<Vendor.allowOverrideCury>(cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<Vendor.allowOverrideRate>(cache, (object) null, isVisible);
    nullable = row.Vendor1099;
    bool valueOrDefault1 = nullable.GetValueOrDefault();
    PXUIFieldAttribute.SetVisible<VendorMaint.VendorBalanceSummary.depositsBalance>(this.VendorBalance.Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<VendorMaint.VendorBalanceSummary.balance>(this.VendorBalance.Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<VendorMaint.VendorBalanceSummary.retainageBalance>(this.VendorBalance.Cache, (object) null, flag1);
    PXUIFieldAttribute.SetEnabled<Vendor.taxReportFinPeriod>(cache, (object) null, row.TaxPeriodType != "F");
    PXCache cache2 = cache;
    nullable = row.TaxUseVendorCurPrecision;
    int num3 = !nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<Vendor.taxReportPrecision>(cache2, (object) null, num3 != 0);
    PXUIFieldAttribute.SetEnabled<Vendor.box1099>(cache, (object) null, valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<Vendor.foreignEntity>(cache, (object) null, valueOrDefault1);
    PXUIFieldAttribute.SetVisible<Vendor.box1099>(cache, (object) null, valueOrDefault1);
    PXUIFieldAttribute.SetVisible<Vendor.foreignEntity>(cache, (object) null, valueOrDefault1);
    PXUIFieldAttribute.SetVisible<Vendor.fATCA>(cache, (object) null, valueOrDefault1);
    nullable = row.RetainageApply;
    bool valueOrDefault2 = nullable.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<Vendor.retainagePct>(cache, (object) row, valueOrDefault2);
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.vATReporting>())
    {
      bool flag2 = row.SVATOutputTaxEntryRefNbr == "T";
      PXUIFieldAttribute.SetVisible<Vendor.sVATTaxInvoiceNumberingID>(cache, (object) null, flag2);
      PXUIFieldAttribute.SetRequired<Vendor.sVATTaxInvoiceNumberingID>(cache, flag2);
      PXDefaultAttribute.SetPersistingCheck<Vendor.sVATTaxInvoiceNumberingID>(cache, (object) null, flag2 ? PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing);
    }
    bool flag3 = PXSelectBase<Vendor, PXSelect<Vendor, Where<Vendor.payToVendorID, Equal<Current<Vendor.bAccountID>>, And<Vendor.bAccountID, NotEqual<Current<Vendor.bAccountID>>>>, PX.Data.OrderBy<Asc<Vendor.bAccountID>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    }).RowCast<Vendor>().Any<Vendor>();
    PXCache cache3 = cache;
    Vendor data2 = row;
    int num4;
    if (!flag3 && !valueOrDefault1)
    {
      nullable = row.TaxAgency;
      num4 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num4 = 0;
    PXUIFieldAttribute.SetEnabled<Vendor.payToVendorID>(cache3, (object) data2, num4 != 0);
    this.SuppliedByVendors.Cache.AllowSelect = flag3;
    this.SuppliedByVendors.Cache.AllowInsert = this.SuppliedByVendors.Cache.AllowUpdate = this.SuppliedByVendors.Cache.AllowDelete = false;
    VendorMaint.DefLocationExt extension = this.GetExtension<VendorMaint.DefLocationExt>();
    nullable = row.Vendor1099;
    if (!nullable.GetValueOrDefault() || extension.DefLocation.Current == null)
      return;
    if (row.TinType == null && extension.DefLocation.Current.TaxRegistrationID != null)
      cache.DisplayFieldWarning<Vendor.tinType>((object) row, (object) row.TinType, "Type of TIN cannot be empty because Tax Registration ID is not empty.");
    else if (row.TinType != null && extension.DefLocation.Current.TaxRegistrationID == null)
      cache.DisplayFieldWarning<Vendor.tinType>((object) row, (object) row.TinType, "Type of TIN must be empty because Tax Registration ID is empty.");
    else
      cache.ClearFieldErrors<Vendor.tinType>((object) row);
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXExcludeRowsFromReferentialIntegrityCheck(ForeignTableExcludingConditions = typeof (ExcludeWhen<BAccount2>.Joined<PX.Data.On<BqlOperand<BAccount2.bAccountID, IBqlInt>.IsEqual<PX.Objects.CR.BAccount.parentBAccountID>>>.Satisfies<PX.Data.Where<BqlOperand<BAccount2.isBranch, IBqlBool>.IsEqual<PX.Data.True>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.BAccount.parentBAccountID> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Replace)]
  [PXBool]
  [PXDefault(false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMCostCode.isProjectOverride> e)
  {
  }

  protected virtual void Vendor_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is Vendor row))
      return;
    List<string> stringList = new List<string>();
    foreach (PXResult<PX.Objects.TX.Tax> pxResult in PXSelectBase<PX.Objects.TX.Tax, PXSelect<PX.Objects.TX.Tax, Where<PX.Objects.TX.Tax.taxVendorID, Equal<Current<Vendor.bAccountID>>>>.Config>.Select((PXGraph) this))
    {
      PX.Objects.TX.Tax tax = (PX.Objects.TX.Tax) pxResult;
      stringList.Add(tax.TaxID);
    }
    if (stringList.Any<string>())
    {
      e.Cancel = true;
      throw new PXException("The vendor cannot be deleted because it is a tax agency and the following taxes are associated with this vendor: {0}.", new object[1]
      {
        (object) string.Join(", ", (IEnumerable<string>) stringList)
      });
    }
    if (!(row.Type == "VC") && !row.IsBranch.GetValueOrDefault())
      return;
    (cache.Interceptor as PXTableAttribute).BypassOnDelete(typeof (PX.Objects.CR.BAccount));
    PXNoteAttribute.ForceRetain<Vendor.noteID>(cache);
  }

  protected virtual void Vendor_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is Vendor row))
      return;
    string type = (string) null;
    if (row.Type == "VC")
      type = "CU";
    else if (row.Type == "VE" && row.IsBranch.GetValueOrDefault())
      type = "CP";
    if (string.IsNullOrEmpty(type))
      return;
    this.ChangeBAccountType((PX.Objects.CR.BAccount) row, type);
  }

  protected virtual void Vendor_CuryID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>())
      return;
    if (this.cmpany.Current == null || string.IsNullOrEmpty(this.cmpany.Current.BaseCuryID))
      throw new PXException();
    e.NewValue = (object) this.cmpany.Current.BaseCuryID;
    e.Cancel = true;
  }

  protected virtual void Vendor_TaxPeriodType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is Vendor row) || !(row.TaxPeriodType == "F"))
      return;
    row.TaxReportFinPeriod = new bool?(true);
  }

  protected virtual void Vendor_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    Vendor row = e.Row as Vendor;
    if (!row.TaxUseVendorCurPrecision.GetValueOrDefault())
    {
      CurrencyList currencyList = (CurrencyList) PXSelectBase<CurrencyList, PXSelect<CurrencyList, Where<CurrencyList.curyID, Equal<Required<CurrencyList.curyID>>>>.Config>.Select((PXGraph) this, (object) new PXSetup<PX.Objects.GL.Company>((PXGraph) this).Current.BaseCuryID);
      short? nullable1 = row.TaxReportPrecision;
      int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      nullable1 = currencyList.DecimalPlaces;
      int? nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
      {
        cache.SetValueExt<Vendor.taxUseVendorCurPrecision>((object) row, (object) true);
        cache.RaiseExceptionHandling<Vendor.taxUseVendorCurPrecision>((object) row, (object) true, (Exception) new PXSetPropertyException("The Use Currency Precision flag was set because the tax report precision is equal to the currency precision", PXErrorLevel.Warning));
      }
    }
    bool? nullable = row.TaxAgency;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.RetainageApply;
      if (nullable.GetValueOrDefault())
        cache.RaiseExceptionHandling<Vendor.retainageApply>((object) row, (object) true, (Exception) new PXSetPropertyException("The Apply Retainage check box cannot be selected for a vendor that is a tax agency.", PXErrorLevel.Error));
    }
    this.ValidateAPAndReclassificationAccountsAndSubs(cache, row);
    if (row == null || !(row.Type == "CU") && !(row.Type == "VC"))
      return;
    PX.Objects.AR.Customer customer = (PX.Objects.AR.Customer) PXSelectBase<PX.Objects.AR.Customer, PXViewOf<PX.Objects.AR.Customer>.BasedOn<SelectFromBase<PX.Objects.AR.Customer, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.AR.Customer.acctCD, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this, (object) row.AcctCD);
    CustomerMaint.VerifyParentBAccountID<PX.Objects.CR.BAccount.parentBAccountID>((PXGraph) this, cache, customer, (PX.Objects.CR.BAccount) row);
  }

  private void ValidateAPAndReclassificationAccountsAndSubs(PXCache sender, Vendor vendor)
  {
    VendorMaint.DefLocationExt extension = this.GetExtension<VendorMaint.DefLocationExt>();
    int? vapAccountId = (int?) extension.DefLocation.SelectSingle()?.VAPAccountID;
    if (!vapAccountId.HasValue)
      return;
    string message = (string) null;
    bool flag1 = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.subAccount>();
    int? nullable1 = vendor.PrebookAcctID;
    int? nullable2 = vapAccountId;
    bool flag2 = nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue;
    if (flag2 && !flag1)
      message = "The AP Account and the Reclassification Account boxes should not have the same accounts specified.";
    else if (flag2 & flag1)
    {
      nullable2 = vendor.PrebookSubID;
      nullable1 = extension.DefLocation.Current.VAPSubID;
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        message = "The AP Account (subaccount) and the Reclassification Account (subaccount) boxes should not have the same account-subaccount pairs specified.";
    }
    if (message == null)
      return;
    PXSetPropertyException propertyException = new PXSetPropertyException(message, PXErrorLevel.Error);
    PXFieldState stateExt1 = (PXFieldState) sender.GetStateExt<Vendor.prebookAcctID>((object) vendor);
    sender.RaiseExceptionHandling<Vendor.prebookAcctID>((object) vendor, stateExt1.Value, (Exception) propertyException);
    PXFieldState stateExt2 = (PXFieldState) sender.GetStateExt<Vendor.prebookSubID>((object) vendor);
    sender.RaiseExceptionHandling<Vendor.prebookSubID>((object) vendor, stateExt2.Value, (Exception) propertyException);
  }

  private void CheckPayToVendorRelations(Vendor vendor, bool? isUnsuitableType)
  {
    if (vendor != null && isUnsuitableType.GetValueOrDefault())
    {
      if (!vendor.PayToVendorID.HasValue)
      {
        if (!PXSelectBase<Vendor, PXSelect<Vendor, Where<Vendor.payToVendorID, Equal<Current<Vendor.bAccountID>>>, PX.Data.OrderBy<Asc<Vendor.bAccountID>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
        {
          (object) vendor
        }).Any<PXResult<Vendor>>())
          return;
      }
      throw new PXSetPropertyException("This setting cannot be specified for the selected vendor '{0}' because this vendor is involved in the Vendor Relations functionality.", new object[1]
      {
        (object) vendor.AcctCD
      });
    }
  }

  protected virtual void Vendor_Vendor1099_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    this.CheckPayToVendorRelations(e.Row as Vendor, e.NewValue as bool?);
  }

  protected virtual void Vendor_TaxAgency_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    this.CheckPayToVendorRelations(e.Row as Vendor, e.NewValue as bool?);
  }

  protected virtual void Vendor_CuryRateTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>())
      return;
    e.Cancel = true;
  }

  protected virtual void Vendor_CuryRateTypeID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>())
      return;
    e.Cancel = true;
  }

  public override IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    if (viewName == "PaymentDetails")
    {
      VendorMaint.DefLocationExt extension = this.GetExtension<VendorMaint.DefLocationExt>();
      extension.DefLocation.Current = (PX.Objects.CR.Standalone.Location) extension.DefLocation.Select();
    }
    if (viewName == "_LocationVCashAccountID_PX.Objects.CA.CashAccount+cashAccountID_" && (this.Caches[typeof (PX.Objects.CR.Standalone.Location)].Current is PX.Objects.CR.Standalone.Location current ? current.VPaymentMethodID : (string) null) == null)
    {
      VendorMaint.DefLocationExt extension = this.GetExtension<VendorMaint.DefLocationExt>();
      extension.DefLocation.Current = (PX.Objects.CR.Standalone.Location) extension.DefLocation.Select();
    }
    return base.ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }

  protected virtual void Vendor_VendorClassID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    Vendor row = (Vendor) e.Row;
    if (row == null)
      return;
    PX.Objects.AP.VendorClass vendorClass = (PX.Objects.AP.VendorClass) PXSelectorAttribute.Select<Vendor.vendorClassID>(cache, (object) row, e.NewValue);
    this.doCopyClassSettings = false;
    if (vendorClass == null)
      return;
    this.doCopyClassSettings = true;
    if (cache.GetStatus((object) row) == PXEntryStatus.Inserted || this.BAccount.Ask("Warning", "Please confirm if you want to update current Vendor settings with the Vendor Class defaults. Original settings will be preserved otherwise.", MessageButtons.YesNo, false) != WebDialogResult.No)
      return;
    this.doCopyClassSettings = false;
    this.BAccount.ClearDialog();
  }

  protected virtual void Vendor_VendorClassID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    VendorMaint.DefLocationExt extension1 = this.GetExtension<VendorMaint.DefLocationExt>();
    extension1.DefLocation.Current = (PX.Objects.CR.Standalone.Location) extension1.DefLocation.Select();
    this.VendorClass.RaiseFieldUpdated(cache, e.Row);
    if (this.VendorClass.Current != null && this.VendorClass.Current.DefaultLocationCDFromBranch.GetValueOrDefault())
    {
      PX.SM.Branch branch = (PX.SM.Branch) PXSelectBase<PX.SM.Branch, PXSelect<PX.SM.Branch, Where<PX.SM.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>.Config>.Select((PXGraph) this);
      if (branch != null && extension1.DefLocation.Current != null && extension1.DefLocation.Cache.GetStatus((object) extension1.DefLocation.Current) == PXEntryStatus.Inserted)
      {
        object branchCd = (object) branch.BranchCD;
        extension1.DefLocation.Cache.RaiseFieldUpdating<PX.Objects.CR.Standalone.Location.locationCD>((object) extension1.DefLocation.Current, ref branchCd);
        extension1.DefLocation.Current.LocationCD = (string) branchCd;
        extension1.DefLocation.Cache.Normalize();
      }
    }
    VendorMaint.DefContactAddressExt extension2 = this.GetExtension<VendorMaint.DefContactAddressExt>();
    extension2.DefAddress.Current = (PX.Objects.CR.Address) extension2.DefAddress.Select();
    if (extension2.DefAddress.Current != null && extension2.DefAddress.Current.AddressID.HasValue)
    {
      extension2.InitDefAddress(extension2.DefAddress.Current);
      extension2.DefAddress.Cache.MarkUpdated((object) extension2.DefAddress.Current);
    }
    Vendor row = (Vendor) e.Row;
    if (this.VendorClass.Current == null || !this.doCopyClassSettings)
      return;
    this.VendorClass.RaiseFieldUpdated(cache, e.Row);
    this.CopyAccounts(cache, row);
    if (extension1.DefLocation.Current != null)
      extension1.DefLocation.Cache.SetDefaultExt<PX.Objects.CR.Standalone.Location.vTaxZoneID>((object) extension1.DefLocation.Current);
    VendorMaint.LocationDetailsExt extension3 = this.GetExtension<VendorMaint.LocationDetailsExt>();
    foreach (PXResult<PX.Objects.CR.Standalone.Location> pxResult in extension3.Locations.Select())
    {
      PX.Objects.CR.Standalone.Location location = (PX.Objects.CR.Standalone.Location) pxResult;
      extension1.InitLocation((IBqlTable) location, location.LocType, true);
      extension3.Locations.Cache.MarkUpdated((object) location);
    }
  }

  protected virtual void Vendor_TaxAgency_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is Vendor row))
      return;
    bool? nullable = row.TaxAgency;
    if (nullable.GetValueOrDefault())
    {
      nullable = e.OldValue as bool?;
      if (!nullable.GetValueOrDefault())
        row.PaymentsByLinesAllowed = new bool?(false);
    }
    nullable = row.TaxAgency;
    if (nullable.GetValueOrDefault())
      return;
    nullable = e.OldValue as bool?;
    if (!nullable.GetValueOrDefault())
      return;
    row.SalesTaxAcctID = new int?();
    row.PurchTaxAcctID = new int?();
    row.TaxExpenseAcctID = new int?();
    row.SalesTaxSubID = new int?();
    row.PurchTaxSubID = new int?();
    row.TaxExpenseSubID = new int?();
  }

  protected virtual void _(PX.Data.Events.RowSelecting<NotificationRecipient> e)
  {
    using (new PXConnectionScope())
      this.CalculateNotificationRecipientEmail(e.Cache, e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<NotificationRecipient, NotificationRecipient.contactID> e)
  {
    this.CalculateNotificationRecipientEmail(e.Cache, e.Row);
  }

  protected virtual void CalculateNotificationRecipientEmail(
    PXCache cache,
    NotificationRecipient row)
  {
    if (row == null)
      return;
    if (!(PXSelectorAttribute.Select<NotificationRecipient.contactID>(cache, (object) row) is PX.Objects.CR.Contact contact))
    {
      switch (row.ContactType)
      {
        case "P":
          contact = (PX.Objects.CR.Contact) this.GetExtension<VendorMaint.DefContactAddressExt>().DefContact.SelectWindowed(0, 1);
          break;
        case "R":
          contact = (PX.Objects.CR.Contact) this.GetExtension<VendorMaint.DefLocationExt>().RemitContact.SelectWindowed(0, 1);
          break;
        case "S":
          contact = (PX.Objects.CR.Contact) this.GetExtension<VendorMaint.DefLocationExt>().DefLocationContact.SelectWindowed(0, 1);
          break;
      }
    }
    if (contact == null)
      return;
    row.Email = contact.EMail;
  }

  public virtual void CopyAccounts(PXCache sender, Vendor row)
  {
    sender.SetDefaultExt<Vendor.discTakenAcctID>((object) row);
    sender.SetDefaultExt<Vendor.discTakenSubID>((object) row);
    sender.SetDefaultExt<Vendor.prepaymentAcctID>((object) row);
    sender.SetDefaultExt<Vendor.prepaymentSubID>((object) row);
    sender.SetDefaultExt<Vendor.prebookAcctID>((object) row);
    sender.SetDefaultExt<Vendor.prebookSubID>((object) row);
    sender.SetDefaultExt<Vendor.pOAccrualAcctID>((object) row);
    sender.SetDefaultExt<Vendor.pOAccrualSubID>((object) row);
    sender.SetDefaultExt<Vendor.curyID>((object) row);
    sender.SetDefaultExt<Vendor.curyRateTypeID>((object) row);
    sender.SetDefaultExt<Vendor.priceListCuryID>((object) row);
    sender.SetDefaultExt<Vendor.vOrgBAccountID>((object) row);
    sender.SetDefaultExt<Vendor.allowOverrideCury>((object) row);
    sender.SetDefaultExt<Vendor.allowOverrideRate>((object) row);
    sender.SetDefaultExt<Vendor.termsID>((object) row);
    sender.SetDefaultExt<Vendor.localeName>((object) row);
    sender.SetDefaultExt<Vendor.groupMask>((object) row);
    sender.SetDefaultExt<Vendor.retainageApply>((object) row);
    sender.SetDefaultExt<Vendor.paymentsByLinesAllowed>((object) row);
  }

  protected virtual void Aggregate(VendorMaint.VendorBalanceSummary aRes, CuryAPHistory aSrc)
  {
    if (!aRes.Balance.HasValue)
      aRes.Balance = new Decimal?(0M);
    if (!aRes.DepositsBalance.HasValue)
      aRes.DepositsBalance = new Decimal?(0M);
    if (!aRes.RetainageBalance.HasValue)
      aRes.RetainageBalance = new Decimal?(0M);
    aRes.VendorID = aSrc.VendorID;
    VendorMaint.VendorBalanceSummary vendorBalanceSummary1 = aRes;
    Decimal? balance = vendorBalanceSummary1.Balance;
    Decimal valueOrDefault1 = aSrc.FinYtdBalance.GetValueOrDefault();
    vendorBalanceSummary1.Balance = balance.HasValue ? new Decimal?(balance.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
    VendorMaint.VendorBalanceSummary vendorBalanceSummary2 = aRes;
    Decimal? depositsBalance = vendorBalanceSummary2.DepositsBalance;
    Decimal valueOrDefault2 = aSrc.FinYtdDeposits.GetValueOrDefault();
    vendorBalanceSummary2.DepositsBalance = depositsBalance.HasValue ? new Decimal?(depositsBalance.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
    VendorMaint.VendorBalanceSummary vendorBalanceSummary3 = aRes;
    Decimal? retainageBalance = vendorBalanceSummary3.RetainageBalance;
    Decimal? retainageWithheld = aSrc.FinYtdRetainageWithheld;
    Decimal? nullable1 = aSrc.FinYtdRetainageReleased;
    Decimal? nullable2 = retainageWithheld.HasValue & nullable1.HasValue ? new Decimal?(retainageWithheld.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable3;
    if (!(retainageBalance.HasValue & nullable2.HasValue))
    {
      nullable1 = new Decimal?();
      nullable3 = nullable1;
    }
    else
      nullable3 = new Decimal?(retainageBalance.GetValueOrDefault() + nullable2.GetValueOrDefault());
    vendorBalanceSummary3.RetainageBalance = nullable3;
  }

  protected virtual void ChangeBAccountType(PX.Objects.CR.BAccount descendantEntity, string type)
  {
    BAccountItself baccountItself = this.CurrentBAccountItself.SelectSingle((object) descendantEntity.BAccountID);
    if (baccountItself == null)
      return;
    baccountItself.Type = type;
    this.CurrentBAccountItself.Update(baccountItself);
  }

  [PXCacheName("Balance Summary")]
  [Serializable]
  public class VendorBalanceSummary : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _VendorID;
    protected Decimal? _Balance;
    protected Decimal? _DepositsBalance;

    [PXDBInt(IsKey = true)]
    [PXDefault]
    public virtual int? VendorID
    {
      get => this._VendorID;
      set => this._VendorID = value;
    }

    [CurySymbol(null, null, typeof (Vendor.baseCuryID), null, null, null, null, true, true)]
    [PXBaseCury(null, typeof (Vendor.baseCuryID))]
    [PXUIField(DisplayName = "Balance", Visible = true, Enabled = false)]
    public virtual Decimal? Balance
    {
      get => this._Balance;
      set => this._Balance = value;
    }

    [CurySymbol(null, null, typeof (Vendor.baseCuryID), null, null, null, null, true, true)]
    [PXBaseCury(null, typeof (Vendor.baseCuryID))]
    [PXUIField(DisplayName = "Prepayment Balance", Enabled = false)]
    public virtual Decimal? DepositsBalance
    {
      get => this._DepositsBalance;
      set => this._DepositsBalance = value;
    }

    [CurySymbol(null, null, typeof (Vendor.baseCuryID), null, null, null, null, true, true)]
    [PXBaseCury(null, typeof (Vendor.baseCuryID))]
    [PXUIField(DisplayName = "Retained Balance", Visibility = PXUIVisibility.Visible, Enabled = false, FieldClass = "Retainage")]
    public virtual Decimal? RetainageBalance { get; set; }

    public abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      VendorMaint.VendorBalanceSummary.vendorID>
    {
    }

    public abstract class balance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      VendorMaint.VendorBalanceSummary.balance>
    {
    }

    public abstract class depositsBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      VendorMaint.VendorBalanceSummary.depositsBalance>
    {
    }

    public abstract class retainageBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      VendorMaint.VendorBalanceSummary.retainageBalance>
    {
    }
  }

  [PXProjection(typeof (PX.Data.Select<Vendor, Where<Vendor.payToVendorID, Equal<CurrentValue<Vendor.bAccountID>>>>))]
  [Serializable]
  public class SuppliedByVendor : Vendor
  {
  }

  /// <exclude />
  public class PaymentDetailsExt : PXGraphExtension<VendorMaint>
  {
    public PXSelectJoin<VendorPaymentMethodDetail, InnerJoin<PX.Objects.CA.PaymentMethod, On<VendorPaymentMethodDetail.paymentMethodID, Equal<PX.Objects.CA.PaymentMethod.paymentMethodID>>, InnerJoin<PaymentMethodDetail, On<PaymentMethodDetail.paymentMethodID, Equal<VendorPaymentMethodDetail.paymentMethodID>, And<PaymentMethodDetail.detailID, Equal<VendorPaymentMethodDetail.detailID>, PX.Data.And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForVendor>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>>>, Where<VendorPaymentMethodDetail.bAccountID, Equal<Optional<PX.Objects.CR.Standalone.Location.bAccountID>>, And<VendorPaymentMethodDetail.locationID, Equal<Optional<PX.Objects.CR.Standalone.Location.locationID>>, And<VendorPaymentMethodDetail.paymentMethodID, Equal<Optional<PX.Objects.CR.Standalone.Location.vPaymentMethodID>>>>>, PX.Data.OrderBy<Asc<PaymentMethodDetail.orderIndex>>> PaymentDetails;
    public PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Optional<PX.Objects.CR.Standalone.Location.vPaymentMethodID>>, PX.Data.And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForVendor>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>> PaymentTypeDetails;

    [PXOverride]
    public virtual IEnumerable ExecuteSelect(
      string viewName,
      object[] parameters,
      object[] searches,
      string[] sortcolumns,
      bool[] descendings,
      PXFilterRow[] filters,
      ref int startRow,
      int maximumRows,
      ref int totalRows,
      ExecuteSelectDelegate ExecuteSelect)
    {
      if (viewName == "PaymentDetails")
        this.FillPaymentDetails(this.Base.GetExtension<VendorMaint.DefLocationExt>().DefLocation.SelectSingle());
      return ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
    }

    [PXDBDefault(typeof (PX.Objects.CR.Standalone.Location.bAccountID))]
    [PXMergeAttributes(Method = MergeMethod.Append)]
    protected virtual void _(
      PX.Data.Events.CacheAttached<VendorPaymentMethodDetail.bAccountID> e)
    {
    }

    [PXDBDefault(typeof (PX.Objects.CR.Standalone.Location.locationID))]
    [PXMergeAttributes(Method = MergeMethod.Append)]
    protected virtual void _(
      PX.Data.Events.CacheAttached<VendorPaymentMethodDetail.locationID> e)
    {
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vPaymentMethodID> e)
    {
      PX.Objects.CR.Standalone.Location row = e.Row;
      string oldValue = (string) e.OldValue;
      if (!string.IsNullOrEmpty(oldValue))
        this.ClearPaymentDetails(row, oldValue, true);
      e.Cache.SetDefaultExt<PX.Objects.CR.Standalone.Location.vCashAccountID>((object) e.Row);
      this.FillPaymentDetails(row);
      this.PaymentDetails.View.RequestRefresh();
    }

    protected virtual void _(
      PX.Data.Events.FieldSelecting<VendorPaymentMethodDetail, VendorPaymentMethodDetail.detailValue> e)
    {
      PaymentMethodDetailHelper.VendorDetailValueFieldSelecting((PXGraph) this.Base, e);
    }

    protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.CR.Standalone.Location> e)
    {
      this.FillPaymentDetails(e.Row);
    }

    protected virtual void _(PX.Data.Events.RowSelected<VendorPaymentMethodDetail> e)
    {
      if (e.Row == null)
        return;
      VendorPaymentMethodDetail row = e.Row;
      PaymentMethodDetail template = this.FindTemplate(row);
      bool flag = template != null && template.IsRequired.GetValueOrDefault();
      PXDefaultAttribute.SetPersistingCheck<VendorPaymentMethodDetail.detailValue>(e.Cache, (object) row, flag ? PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing);
    }

    protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.CR.Standalone.Location> e)
    {
    }

    protected virtual void _(PX.Data.Events.RowPersisting<VendorPaymentMethodDetail> e)
    {
      if (e.Cancel || e.Row == null)
        return;
      string errorMessage = (string) null;
      if (this.ValidatePaymentDetail(e.Row, out errorMessage))
        return;
      if (this.Base.IsImport)
        throw new PXException(errorMessage);
      throw new PXException("The record cannot be saved because at least one error has occurred. Please review the errors.");
    }

    public virtual PaymentMethodDetail FindTemplate(VendorPaymentMethodDetail aDet)
    {
      return (PaymentMethodDetail) PXSelectBase<PaymentMethodDetail, PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Required<PaymentMethodDetail.paymentMethodID>>, And<PaymentMethodDetail.detailID, Equal<Required<PaymentMethodDetail.detailID>>, PX.Data.And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForVendor>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>>.Config>.Select((PXGraph) this.Base, (object) aDet.PaymentMethodID, (object) aDet.DetailID);
    }

    public virtual bool ValidateLocationPaymentMethod(PXCache cache, PX.Objects.CR.Standalone.Location location)
    {
      bool flag = true;
      VendorR current = this.Base.BAccountAccessor.Current;
      if (current != null && (current.Type == "VE" || current.Type == "VC"))
      {
        foreach (PXResult<VendorPaymentMethodDetail> aRow in this.PaymentDetails.Select())
        {
          if (!this.ValidatePaymentDetail((VendorPaymentMethodDetail) aRow))
            flag = false;
        }
      }
      return flag;
    }

    public virtual bool ValidatePaymentDetail(VendorPaymentMethodDetail aRow)
    {
      PaymentMethodDetail template = this.FindTemplate(aRow);
      if (template == null || !template.IsRequired.GetValueOrDefault() || !string.IsNullOrEmpty(aRow.DetailValue))
        return true;
      this.PaymentDetails.Cache.RaiseExceptionHandling<VendorPaymentMethodDetail.detailValue>((object) aRow, (object) aRow.DetailValue, (Exception) new PXSetPropertyException("This field is required."));
      return false;
    }

    protected virtual bool ValidateLocationPaymentMethod(
      PXCache cache,
      PX.Objects.CR.Standalone.Location location,
      out string errorMessage)
    {
      bool flag = true;
      errorMessage = (string) null;
      VendorR current = this.Base.BAccountAccessor.Current;
      if (current != null && (current.Type == "VE" || current.Type == "VC"))
      {
        foreach (PXResult<VendorPaymentMethodDetail> aRow in this.PaymentDetails.Select())
        {
          if (!this.ValidatePaymentDetail((VendorPaymentMethodDetail) aRow, out errorMessage))
          {
            flag = false;
            break;
          }
        }
      }
      return flag;
    }

    protected virtual bool ValidatePaymentDetail(
      VendorPaymentMethodDetail aRow,
      out string errorMessage)
    {
      errorMessage = (string) null;
      PaymentMethodDetail template = this.FindTemplate(aRow);
      if (template == null || !template.IsRequired.GetValueOrDefault() || !string.IsNullOrEmpty(aRow.DetailValue))
        return true;
      ref string local = ref errorMessage;
      string str;
      if (!string.IsNullOrEmpty(template.Descr))
        str = PXLocalizer.LocalizeFormat("The {0} payment instruction is required and cannot be empty.", (object) template.Descr);
      else
        str = "The payment instruction is required and cannot be empty.";
      local = str;
      this.PaymentDetails.Cache.RaiseExceptionHandling<VendorPaymentMethodDetail.detailValue>((object) aRow, (object) aRow.DetailValue, (Exception) new PXSetPropertyException<VendorPaymentMethodDetail.detailValue>("This field is required."));
      return false;
    }

    public virtual void FillPaymentDetails(PX.Objects.CR.Standalone.Location location)
    {
      if (location == null || string.IsNullOrEmpty(location.VPaymentMethodID))
        return;
      if ((PX.Objects.CA.PaymentMethod) PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this.Base, (object) location.VPaymentMethodID) == null)
        return;
      List<PaymentMethodDetail> paymentMethodDetailList = new List<PaymentMethodDetail>();
      foreach (PXResult<PaymentMethodDetail> pxResult1 in this.PaymentTypeDetails.Select((object) location.VPaymentMethodID))
      {
        PaymentMethodDetail paymentMethodDetail1 = (PaymentMethodDetail) pxResult1;
        VendorPaymentMethodDetail paymentMethodDetail2 = (VendorPaymentMethodDetail) null;
        foreach (PXResult<VendorPaymentMethodDetail> pxResult2 in this.PaymentDetails.Select((object) location.BAccountID, (object) location.LocationID, (object) location.VPaymentMethodID))
        {
          VendorPaymentMethodDetail paymentMethodDetail3 = (VendorPaymentMethodDetail) pxResult2;
          if (paymentMethodDetail3.DetailID == paymentMethodDetail1.DetailID)
          {
            paymentMethodDetail2 = paymentMethodDetail3;
            break;
          }
        }
        if (paymentMethodDetail2 == null)
          paymentMethodDetailList.Add(paymentMethodDetail1);
      }
      using (new ReadOnlyScope(new PXCache[1]
      {
        this.PaymentDetails.Cache
      }))
      {
        foreach (PaymentMethodDetail paymentMethodDetail4 in paymentMethodDetailList)
        {
          VendorPaymentMethodDetail paymentMethodDetail5 = new VendorPaymentMethodDetail();
          paymentMethodDetail5.BAccountID = location.BAccountID;
          paymentMethodDetail5.LocationID = location.LocationID;
          paymentMethodDetail5.PaymentMethodID = location.VPaymentMethodID;
          paymentMethodDetail5.DetailID = paymentMethodDetail4.DetailID;
          if (!string.IsNullOrEmpty(paymentMethodDetail4.DefaultValue))
            paymentMethodDetail5.DetailValue = paymentMethodDetail4.DefaultValue;
          this.PaymentDetails.Insert(paymentMethodDetail5);
        }
        if (paymentMethodDetailList.Count <= 0)
          return;
        this.PaymentDetails.View.RequestRefresh();
      }
    }

    public virtual void ClearPaymentDetails(
      PX.Objects.CR.Standalone.Location location,
      string paymentTypeID,
      bool clearNewOnly)
    {
      foreach (PXResult<VendorPaymentMethodDetail> pxResult in this.PaymentDetails.Select((object) location.BAccountID, (object) location.LocationID, (object) paymentTypeID))
      {
        VendorPaymentMethodDetail paymentMethodDetail = (VendorPaymentMethodDetail) pxResult;
        bool flag = true;
        if (clearNewOnly)
          flag = this.PaymentDetails.Cache.GetStatus((object) paymentMethodDetail) == PXEntryStatus.Inserted;
        if (flag)
          this.PaymentDetails.Delete(paymentMethodDetail);
      }
    }
  }

  /// <exclude />
  public class DefContactAddressExt : 
    PX.Objects.CR.Extensions.DefContactAddressExt<VendorMaint, VendorR, VendorR.acctName>.WithCombinedTypeValidation
  {
    protected virtual void _(PX.Data.Events.RowInserting<PX.Objects.CR.Address> e)
    {
      PX.Objects.CR.Address row = e.Row;
      if (row == null)
        return;
      if (!row.AddressID.HasValue)
        e.Cancel = true;
      else
        this.InitDefAddress(row);
    }

    public virtual void InitDefAddress(PX.Objects.CR.Address aAddress)
    {
      if (this.Base.CurrentVendor.Cache.GetStatus((object) this.Base.CurrentVendor.Current) != PXEntryStatus.Inserted)
        return;
      aAddress.CountryID = this.Base.VendorClass.Current?.CountryID ?? aAddress.CountryID;
    }
  }

  /// <exclude />
  public class DefLocationExt : 
    PX.Objects.CR.Extensions.DefLocationExt<VendorMaint, VendorMaint.DefContactAddressExt, VendorMaint.LocationDetailsExt, VendorR, VendorR.bAccountID, VendorR.defLocationID>.WithCombinedTypeValidation
  {
    [PXViewName("Remit Contact")]
    public PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.bAccountID, Equal<Current<PX.Objects.CR.Standalone.Location.bAccountID>>, And<PX.Objects.CR.Contact.contactID, Equal<Current<PX.Objects.CR.Standalone.Location.vRemitContactID>>>>> RemitContact;
    [PXViewName("Remit Address")]
    public PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.bAccountID, Equal<Current<PX.Objects.CR.Standalone.Location.bAccountID>>, And<PX.Objects.CR.Address.addressID, Equal<Current<PX.Objects.CR.Standalone.Location.vRemitAddressID>>>>> RemitAddress;
    [PXCopyPasteHiddenView]
    public PXSelect<LocationBranchSettings, Where<LocationBranchSettings.bAccountID, Equal<Current<PX.Objects.CR.Standalone.Location.bAccountID>>, And<LocationBranchSettings.locationID, Equal<Current<PX.Objects.CR.Standalone.Location.locationID>>, And<LocationBranchSettings.branchID, Equal<Current<AccessInfo.branchID>>>>>> DefLocationBranchSettings;
    public PXAction<VendorR> viewRemitOnMap;

    public override List<System.Type> InitLocationFields
    {
      get
      {
        return new List<System.Type>()
        {
          typeof (PX.Objects.CR.Standalone.Location.vCarrierID),
          typeof (PX.Objects.CR.Standalone.Location.vFOBPointID),
          typeof (PX.Objects.CR.Standalone.Location.vLeadTime),
          typeof (PX.Objects.CR.Standalone.Location.vShipTermsID),
          typeof (PX.Objects.CR.Standalone.Location.vExpenseAcctID),
          typeof (PX.Objects.CR.Standalone.Location.vExpenseSubID),
          typeof (PX.Objects.CR.Standalone.Location.vDiscountAcctID),
          typeof (PX.Objects.CR.Standalone.Location.vDiscountSubID),
          typeof (PX.Objects.CR.Standalone.Location.vFreightAcctID),
          typeof (PX.Objects.CR.Standalone.Location.vFreightSubID),
          typeof (PX.Objects.CR.Standalone.Location.vRcptQtyAction),
          typeof (PX.Objects.CR.Standalone.Location.vRcptQtyMin),
          typeof (PX.Objects.CR.Standalone.Location.vRcptQtyMax),
          typeof (PX.Objects.CR.Standalone.Location.vAPAccountID),
          typeof (PX.Objects.CR.Standalone.Location.vAPSubID),
          typeof (PX.Objects.CR.Standalone.Location.vCashAccountID),
          typeof (PX.Objects.CR.Standalone.Location.vPaymentMethodID),
          typeof (PX.Objects.CR.Standalone.Location.vPaymentByType),
          typeof (PX.Objects.CR.Standalone.Location.vShipTermsID),
          typeof (PX.Objects.CR.Standalone.Location.vRcptQtyAction),
          typeof (PX.Objects.CR.Standalone.Location.vPrintOrder),
          typeof (PX.Objects.CR.Standalone.Location.vEmailOrder),
          typeof (PX.Objects.CR.Standalone.Location.vRemitAddressID),
          typeof (PX.Objects.CR.Standalone.Location.vRemitContactID),
          typeof (PX.Objects.CR.Standalone.Location.vTaxCalcMode),
          typeof (PX.Objects.CR.Standalone.Location.vRetainageAcctID),
          typeof (PX.Objects.CR.Standalone.Location.vRetainageSubID)
        };
      }
    }

    [PXOptimizationBehavior(IgnoreBqlDelegate = true)]
    public virtual IEnumerable remitContact()
    {
      return this.SelectEntityByKey<PX.Objects.CR.Contact, PX.Objects.CR.Contact.contactID, PX.Objects.CR.Standalone.Location.vRemitContactID, PX.Objects.CR.Standalone.Location.overrideRemitContact>();
    }

    [PXOptimizationBehavior(IgnoreBqlDelegate = true)]
    public virtual IEnumerable remitAddress()
    {
      return this.SelectEntityByKey<PX.Objects.CR.Address, PX.Objects.CR.Address.addressID, PX.Objects.CR.Standalone.Location.vRemitAddressID, PX.Objects.CR.Standalone.Location.overrideRemitAddress>();
    }

    [PXUIField(DisplayName = "View on Map", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
    [PXButton(DisplayOnMainToolbar = false)]
    public virtual IEnumerable ViewRemitOnMap(PXAdapter adapter)
    {
      BAccountUtility.ViewOnMap(this.RemitAddress.SelectSingle());
      return adapter.Get();
    }

    public override void ValidateAddress()
    {
      base.ValidateAddress();
      PX.Objects.CR.Address aAddress = this.RemitAddress.SelectSingle();
      if (aAddress == null)
        return;
      bool? isValidated = aAddress.IsValidated;
      bool flag = false;
      if (!(isValidated.GetValueOrDefault() == flag & isValidated.HasValue))
        return;
      PXAddressValidator.Validate<PX.Objects.CR.Address>((PXGraph) this.Base, aAddress, true, true);
    }

    [PXDBInt]
    [PXDBChildIdentity(typeof (PX.Objects.CR.Standalone.Location.locationID))]
    [PXUIField(DisplayName = "Default Location", Visibility = PXUIVisibility.Invisible)]
    [PXSelector(typeof (Search<PX.Objects.CR.Location.locationID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<VendorR.bAccountID>>>>), DescriptionField = typeof (PX.Objects.CR.Location.locationCD), DirtyRead = true)]
    [PXMergeAttributes(Method = MergeMethod.Replace)]
    protected override void _(PX.Data.Events.CacheAttached<VendorR.defLocationID> e)
    {
    }

    [PXDBChildIdentity(typeof (PX.Objects.CR.Address.addressID))]
    [PXMergeAttributes(Method = MergeMethod.Append)]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vRemitAddressID> e)
    {
    }

    [PXDBChildIdentity(typeof (PX.Objects.CR.Contact.contactID))]
    [PXMergeAttributes(Method = MergeMethod.Append)]
    protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vRemitContactID> e)
    {
    }

    [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.CR.Standalone.Location.vTaxZoneID))]
    [PXUIField(DisplayName = "Tax Zone", Required = false)]
    [PXDefault(typeof (Search<PX.Objects.AP.VendorClass.taxZoneID, Where<PX.Objects.AP.VendorClass.vendorClassID, Equal<Current<Vendor.vendorClassID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
    [PXSelector(typeof (Search<PX.Objects.TX.TaxZone.taxZoneID>), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), CacheGlobal = true)]
    [PXMergeAttributes(Method = MergeMethod.Replace)]
    protected override void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vTaxZoneID> e)
    {
    }

    [PXActiveCarrierSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), new System.Type[] {typeof (PX.Objects.CS.Carrier.carrierID), typeof (PX.Objects.CS.Carrier.description), typeof (PX.Objects.CS.Carrier.isExternal), typeof (PX.Objects.CS.Carrier.confirmationRequired)}, CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.Carrier.description))]
    [PXUIField(DisplayName = "Ship Via")]
    [PXMergeAttributes(Method = MergeMethod.Replace)]
    protected override void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vCarrierID> e)
    {
    }

    [Account(null, typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<PX.Data.True>, PX.Data.And<Where<Current<GLSetup.ytdNetIncAccountID>, PX.Data.IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DisplayName = "AR Account", Required = true)]
    [PXMergeAttributes(Method = MergeMethod.Replace)]
    protected override void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cARAccountID> e)
    {
    }

    [Account(DisplayName = "Retainage Receivable Account", Visibility = PXUIVisibility.Visible, DescriptionField = typeof (PX.Objects.GL.Account.description), Required = false)]
    [PXMergeAttributes(Method = MergeMethod.Replace)]
    protected override void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cRetainageAcctID> e)
    {
    }

    [CashAccount(typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>>>, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.CA.CashAccount.clearingAccount, Equal<False>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<PX.Objects.CR.Standalone.Location.vPaymentMethodID>>, And<PaymentMethodAccount.useForAP, Equal<PX.Data.True>, PX.Data.And<Where2<Not<FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleBaseCurrencies>>, Or<PX.Objects.CA.CashAccount.baseCuryID, Equal<Current<Vendor.baseCuryID>>>>>>>>>>), Visibility = PXUIVisibility.Visible)]
    [PXMergeAttributes(Method = MergeMethod.Replace)]
    protected override void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vCashAccountID> e)
    {
    }

    [PXFormula(typeof (Switch<Case<Where<CurrentValue<Vendor.defAddressID>, PX.Data.IsNull>, Null, Case<Where<PX.Objects.CR.Standalone.Location.vRemitAddressID, Equal<CurrentValue<Vendor.defAddressID>>>, False>>, PX.Data.True>))]
    [PXMergeAttributes(Method = MergeMethod.Append)]
    protected virtual void _(
      PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.overrideRemitAddress> e)
    {
    }

    [PXFormula(typeof (Switch<Case<Where<CurrentValue<Vendor.defAddressID>, PX.Data.IsNull>, Null, Case<Where<PX.Objects.CR.Standalone.Location.vRemitContactID, Equal<CurrentValue<Vendor.defContactID>>>, False>>, PX.Data.True>))]
    [PXMergeAttributes(Method = MergeMethod.Append)]
    protected virtual void _(
      PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.overrideRemitContact> e)
    {
    }

    [PXMergeAttributes(Method = MergeMethod.Merge)]
    [PXDBDefault(typeof (PX.Objects.CR.Standalone.Location.bAccountID))]
    protected virtual void _(
      PX.Data.Events.CacheAttached<LocationBranchSettings.bAccountID> e)
    {
    }

    [PXMergeAttributes(Method = MergeMethod.Merge)]
    [PXDBDefault(typeof (PX.Objects.CR.Standalone.Location.locationID))]
    protected virtual void _(
      PX.Data.Events.CacheAttached<LocationBranchSettings.locationID> e)
    {
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vTaxZoneID> e)
    {
      PX.Objects.CR.Standalone.Location row = e.Row;
      if (row == null || !(this.Base.BAccount.Current.Type == "VE") || row.CTaxZoneID != null && !((string) e.OldValue == row.CTaxZoneID))
        return;
      this.DefLocation.Cache.SetValue<PX.Objects.CR.Standalone.Location.cTaxZoneID>((object) row, (object) row.VTaxZoneID);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vPaymentMethodID> e)
    {
      if (e.Row == null)
      {
        e.Cancel = true;
      }
      else
      {
        if (this.Base.VendorClass.Current == null || string.IsNullOrEmpty(this.Base.VendorClass.Current.PaymentMethodID))
          return;
        e.NewValue = (object) this.Base.VendorClass.Current.PaymentMethodID;
        e.Cancel = true;
      }
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vCashAccountID> e)
    {
      if (e.Row == null)
        return;
      if (this.Base.VendorClass.Current != null && this.Base.VendorClass.Current.CashAcctID.HasValue && e.Row.VPaymentMethodID == this.Base.VendorClass.Current.PaymentMethodID)
      {
        e.NewValue = (object) this.Base.VendorClass.Current.CashAcctID;
        e.Cancel = true;
      }
      else
      {
        e.NewValue = (object) null;
        e.Cancel = true;
      }
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vTaxZoneID> e)
    {
      PX.Objects.CR.BAccount current = (PX.Objects.CR.BAccount) this.Base.BAccountAccessor.Current;
      if (e.Row == null || current == null)
        return;
      if (current.IsBranch.GetValueOrDefault())
      {
        e.NewValue = (object) e.Row.VTaxZoneID;
        e.Cancel = true;
      }
      else
        this.DefaultFrom<PX.Objects.AP.VendorClass.taxZoneID>(e.Args, this.Base.VendorClass.Cache);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vExpenseSubID> e)
    {
      PX.Objects.CR.BAccount current = (PX.Objects.CR.BAccount) this.Base.BAccountAccessor.Current;
      if (e.Row == null || current == null)
        return;
      if (current.IsBranch.GetValueOrDefault())
      {
        e.NewValue = (object) e.Row.CMPExpenseSubID;
        e.Cancel = true;
      }
      else
        this.DefaultFrom<PX.Objects.AP.VendorClass.expenseSubID>(e.Args, this.Base.VendorClass.Cache, false);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vDiscountSubID> e)
    {
      PX.Objects.CR.BAccount current = (PX.Objects.CR.BAccount) this.Base.BAccountAccessor.Current;
      if (e.Row == null || current == null)
        return;
      if (current.IsBranch.GetValueOrDefault())
      {
        e.NewValue = (object) e.Row.CMPDiscountSubID;
        e.Cancel = true;
      }
      else
        this.DefaultFrom<PX.Objects.AP.VendorClass.discountSubID>(e.Args, this.Base.VendorClass.Cache);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cBranchID> e)
    {
      e.NewValue = (object) null;
      e.Cancel = true;
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vRemitAddressID> e)
    {
      this.DefaultFrom<Vendor.defAddressID>(e.Args, this.Base.BAccount.Cache);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vRemitContactID> e)
    {
      this.DefaultFrom<Vendor.defContactID>(e.Args, this.Base.BAccount.Cache);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vExpenseAcctID> e)
    {
      this.DefaultFrom<PX.Objects.AP.VendorClass.expenseAcctID>(e.Args, this.Base.VendorClass.Cache, false);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vRetainageAcctID> e)
    {
      this.DefaultFrom<PX.Objects.AP.VendorClass.retainageAcctID>(e.Args, this.Base.VendorClass.Cache, false);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vRetainageSubID> e)
    {
      this.DefaultFrom<PX.Objects.AP.VendorClass.retainageSubID>(e.Args, this.Base.VendorClass.Cache, false);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vPaymentByType> e)
    {
      this.DefaultFrom<PX.Objects.AP.VendorClass.paymentByType>(e.Args, this.Base.VendorClass.Cache);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vAPAccountID> e)
    {
      this.DefaultFrom<PX.Objects.AP.VendorClass.aPAcctID>(e.Args, this.Base.VendorClass.Cache);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vAPSubID> e)
    {
      this.DefaultFrom<PX.Objects.AP.VendorClass.aPSubID>(e.Args, this.Base.VendorClass.Cache);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vDiscountAcctID> e)
    {
      this.DefaultFrom<PX.Objects.AP.VendorClass.discountAcctID>(e.Args, this.Base.VendorClass.Cache);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vFreightAcctID> e)
    {
      this.DefaultFrom<PX.Objects.AP.VendorClass.freightAcctID>(e.Args, this.Base.VendorClass.Cache);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vShipTermsID> e)
    {
      this.DefaultFrom<PX.Objects.AP.VendorClass.shipTermsID>(e.Args, this.Base.VendorClass.Cache);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vRcptQtyAction> e)
    {
      this.DefaultFrom<PX.Objects.AP.VendorClass.rcptQtyAction>(e.Args, this.Base.VendorClass.Cache, true, (object) "W");
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vPrintOrder> e)
    {
      this.DefaultFrom<PX.Objects.AP.VendorClass.printPO>(e.Args, this.Base.VendorClass.Cache, false);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.vEmailOrder> e)
    {
      this.DefaultFrom<PX.Objects.AP.VendorClass.emailPO>(e.Args, this.Base.VendorClass.Cache, false);
    }

    protected override void _(PX.Data.Events.RowInserted<VendorR> e, PXRowInserted del)
    {
      VendorR row = e.Row;
      if (row != null)
      {
        PXRowInserting handler = (PXRowInserting) ((sender, args) =>
        {
          PX.SM.Branch branch = (PX.SM.Branch) PXSelectBase<PX.SM.Branch, PXSelect<PX.SM.Branch, Where<PX.SM.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>.Config>.Select((PXGraph) this.Base);
          if (branch == null)
            return;
          object branchCd = (object) branch.BranchCD;
          this.DefLocation.Cache.RaiseFieldUpdating<PX.Objects.CR.Standalone.Location.locationCD>(args.Row, ref branchCd);
          ((PX.Objects.CR.Standalone.Location) args.Row).LocationCD = (string) branchCd;
        });
        if (this.Base.VendorClass.Current != null && this.Base.VendorClass.Current.DefaultLocationCDFromBranch.GetValueOrDefault())
          this.Base.RowInserting.AddHandler<PX.Objects.CR.Standalone.Location>(handler);
        this.InsertLocation(e.Cache, row);
        if (this.Base.VendorClass.Current != null && this.Base.VendorClass.Current.DefaultLocationCDFromBranch.GetValueOrDefault())
          this.Base.RowInserting.RemoveHandler<PX.Objects.CR.Standalone.Location>(handler);
      }
      if (del == null)
        return;
      del(e.Cache, e.Args);
    }

    protected override void _(PX.Data.Events.RowSelected<PX.Objects.CR.Standalone.Location> e)
    {
      base._(e);
      if (e.Row == null)
        return;
      PX.Objects.CR.Standalone.Location row1 = e.Row;
      VendorR current1 = this.Base.BAccountAccessor.Current;
      bool? nullable;
      int num1;
      if (current1 == null)
      {
        num1 = 0;
      }
      else
      {
        nullable = current1.TaxAgency;
        num1 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      bool required1 = num1 != 0;
      PXUIFieldAttribute.SetRequired<PX.Objects.CR.Standalone.Location.vExpenseAcctID>(e.Cache, required1);
      PXUIFieldAttribute.SetRequired<PX.Objects.CR.Standalone.Location.vExpenseSubID>(e.Cache, required1);
      PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Standalone.Location.vCashAccountID>(e.Cache, (object) e.Row, !string.IsNullOrEmpty(row1.VPaymentMethodID));
      PXCache cache1 = e.Cache;
      PX.Objects.CR.Standalone.Location row2 = e.Row;
      VendorR current2 = this.Base.BAccountAccessor.Current;
      int num2;
      if (current2 == null)
      {
        num2 = 1;
      }
      else
      {
        nullable = current2.IsBranch;
        num2 = !nullable.GetValueOrDefault() ? 1 : 0;
      }
      PXUIFieldAttribute.SetVisible<PX.Objects.CR.Standalone.Location.vSiteID>(cache1, (object) row2, num2 != 0);
      PXCache cache2 = this.DefLocationBranchSettings.Cache;
      VendorR current3 = this.Base.BAccountAccessor.Current;
      int num3;
      if (current3 == null)
      {
        num3 = 0;
      }
      else
      {
        nullable = current3.IsBranch;
        num3 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      PXUIFieldAttribute.SetVisible<LocationBranchSettings.vSiteID>(cache2, (object) null, num3 != 0);
      if (this.Base.VendorClass.Current == null)
        return;
      nullable = this.Base.VendorClass.Current.RequireTaxZone;
      int num4;
      if (nullable.GetValueOrDefault())
      {
        nullable = row1.IsDefault;
        num4 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num4 = 0;
      bool required2 = num4 != 0;
      PXDefaultAttribute.SetPersistingCheck<PX.Objects.CR.Standalone.Location.vTaxZoneID>(this.DefLocation.Cache, (object) null, PXPersistingCheck.Nothing);
      PXDefaultAttribute.SetPersistingCheck<PX.Objects.CR.Standalone.Location.vTaxZoneID>(this.DefLocation.Cache, (object) e.Row, required2 ? PXPersistingCheck.Null : PXPersistingCheck.Nothing);
      PXUIFieldAttribute.SetRequired<PX.Objects.CR.Standalone.Location.vTaxZoneID>(this.DefLocation.Cache, required2);
    }

    protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.CR.Standalone.Location> e)
    {
      if (e.Cancel)
        return;
      PX.Objects.CR.Standalone.Location row = e.Row;
      if (row == null)
        return;
      int num;
      if (!(PXSelectorAttribute.Select<PX.Objects.CR.Standalone.Location.vCarrierID>(e.Cache, (object) row) is PX.Objects.CS.Carrier carrier))
      {
        num = 0;
      }
      else
      {
        bool? isActive = carrier.IsActive;
        bool flag = false;
        num = isActive.GetValueOrDefault() == flag & isActive.HasValue ? 1 : 0;
      }
      if (num != 0)
        e.Cache.RaiseExceptionHandling<PX.Objects.CR.Standalone.Location.vCarrierID>((object) row, (object) row.VCarrierID, (Exception) new PXSetPropertyException((IBqlTable) row, "The Ship Via code is not active.", PXErrorLevel.Warning));
      if (!this.ValidateLocation(e.Cache, row) && PXUIFieldAttribute.GetErrors(e.Cache, (object) row).Any<KeyValuePair<string, string>>())
        throw new PXException("The record cannot be saved because at least one error has occurred. Please review the errors.");
    }

    protected virtual void _(PX.Data.Events.RowPersisting<VendorR> e)
    {
      VendorR row = e.Row;
      if (row == null)
        return;
      PX.Objects.CR.Standalone.Location location = this.DefLocation.SelectSingle();
      if (location == null)
        return;
      this.CheckCury(row, location);
    }

    public override bool ValidateLocation(PXCache cache, PX.Objects.CR.Standalone.Location location)
    {
      bool flag = true;
      VendorR current = this.Base.BAccountAccessor.Current;
      if (current != null && (current.Type == "VE" || current.Type == "VC"))
      {
        if (this.Base.VendorClass.Current != null)
        {
          bool? nullable = this.Base.VendorClass.Current.RequireTaxZone;
          if (nullable.GetValueOrDefault())
          {
            nullable = location.IsDefault;
            if (nullable.GetValueOrDefault() && location.VTaxZoneID == null)
              flag &= ValidationHelper<ValidationHelper>.SetErrorEmptyIfNull<PX.Objects.CR.Standalone.Location.vTaxZoneID>(cache, (object) location, (object) location.VTaxZoneID);
          }
        }
        flag &= this.locationValidator.ValidateVendorLocation(cache, (Vendor) current, (ILocation) location);
        this.CheckCury(current, location);
      }
      return flag;
    }

    public virtual void CheckCury(VendorR vendor, PX.Objects.CR.Standalone.Location location)
    {
      PXSelectBase<PX.Objects.CA.CashAccount> pxSelectBase = (PXSelectBase<PX.Objects.CA.CashAccount>) new PXSelect<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Required<PX.Objects.CR.Standalone.Location.vCashAccountID>>>>((PXGraph) this.Base);
      if (string.IsNullOrEmpty(vendor.CuryID) || vendor.AllowOverrideCury.GetValueOrDefault())
        return;
      PX.Objects.CA.CashAccount cashAccount = (PX.Objects.CA.CashAccount) pxSelectBase.Select((object) location.VCashAccountID);
      if (cashAccount == null || !(vendor.CuryID != cashAccount.CuryID))
        return;
      PXUIFieldAttribute.SetWarning<PX.Objects.CR.Standalone.Location.vCashAccountID>(this.DefLocation.Cache, (object) location, "Vendor currency is different from the default Cash Account Currency.");
    }
  }

  /// <exclude />
  public class ContactDetailsExt : 
    BusinessAccountContactDetailsExt<VendorMaint, VendorMaint.CreateContactFromVendorGraphExt, VendorR, VendorR.bAccountID, VendorR.acctName>
  {
  }

  /// <exclude />
  public class LocationDetailsExt : PX.Objects.CR.Extensions.LocationDetailsExt<VendorMaint, VendorR, VendorR.bAccountID>
  {
    [PXOverride]
    public virtual void ChangeBAccountType(
      PX.Objects.CR.BAccount descendantEntity,
      string type,
      System.Action<PX.Objects.CR.BAccount, string> del)
    {
      if (del != null)
        del(descendantEntity, type);
      foreach (PXResult<PX.Objects.CR.Standalone.Location> pxResult in this.Locations.Select())
      {
        PX.Objects.CR.Standalone.Location location = (PX.Objects.CR.Standalone.Location) pxResult;
        location.LocType = type;
        if (!BAccountType.ActsAsVendor(type))
        {
          location.VExpenseAcctID = new int?();
          location.VExpenseSubID = new int?();
          location.VRemitContactID = new int?();
          location.VRemitAddressID = new int?();
          location.VPaymentInfoLocationID = location.LocationID;
          location.VAPAccountLocationID = location.LocationID;
          location.VTaxCalcMode = "T";
          location.VCarrierID = (string) null;
          location.VShipTermsID = (string) null;
          location.VFOBPointID = (string) null;
          location.VLeadTime = new short?();
          location.VFreightAcctID = new int?();
          location.VFreightSubID = new int?();
          location.VDiscountAcctID = new int?();
          location.VDiscountSubID = new int?();
          location.VCashAccountID = new int?();
          location.VPaymentMethodID = (string) null;
          location.VPaymentLeadTime = new short?((short) 0);
          location.VSeparateCheck = new bool?(false);
          location.VPaymentByType = new int?(0);
          location.VAPAccountID = new int?();
          location.VAPSubID = new int?();
          location.VDefProjectID = new int?();
          location.VRcptQtyMin = new Decimal?(0M);
          location.VRcptQtyMax = new Decimal?(100M);
          location.VRcptQtyAction = "W";
          location.VRcptQtyThreshold = new Decimal?(100M);
          location.VPrepaymentPct = new Decimal?(100M);
          location.VSiteID = new int?();
          location.VPrintOrder = new bool?(false);
          location.VEmailOrder = new bool?(false);
          location.VAllowAPBillBeforeReceipt = new bool?(false);
          location.VRetainageAcctID = new int?();
          location.VRetainageSubID = new int?();
        }
        this.Locations.Update(location);
      }
    }
  }

  /// <exclude />
  public class PrimaryContactGraphExt : 
    CRPrimaryContactGraphExt<VendorMaint, VendorMaint.ContactDetailsExt, VendorR, VendorR.bAccountID, VendorR.primaryContactID>
  {
    protected override PX.Data.PXView ContactsView => this.ContactDetailsExtension.Contacts.View;

    [PXUIField(DisplayName = "Name")]
    [PXMergeAttributes(Method = MergeMethod.Merge)]
    protected virtual void _(PX.Data.Events.CacheAttached<VendorR.primaryContactID> e)
    {
    }

    protected virtual void _(PX.Data.Events.FieldUpdated<VendorR, VendorR.acctName> e)
    {
      VendorR row = e.Row;
      if (!row.PrimaryContactID.HasValue)
        return;
      PX.Objects.CR.Contact contact = this.PrimaryContactCurrent.SelectSingle();
      if (contact == null || row.AcctName == null || row.AcctName.Equals(contact.FullName))
        return;
      contact.FullName = row.AcctName;
      this.PrimaryContactCurrent.Update(contact);
    }
  }

  /// <exclude />
  public class VendorMaintAddressLookupExtension : 
    AddressLookupExtension<VendorMaint, VendorR, PX.Objects.CR.Address>
  {
    protected override string AddressView => "DefAddress";

    protected override string ViewOnMap => "ViewMainOnMap";
  }

  /// <exclude />
  public class VendorMaintRemitAddressLookupExtension : 
    AddressLookupExtension<VendorMaint, VendorR, PX.Objects.CR.Address>
  {
    protected override string AddressView => "RemitAddress";

    protected override string ViewOnMap => "viewRemitOnMap";
  }

  /// <exclude />
  public class VendorMaintDefLocationAddressLookupExtension : 
    AddressLookupExtension<VendorMaint, VendorR, PX.Objects.CR.Address>
  {
    protected override string AddressView => "DefLocationAddress";

    protected override string ViewOnMap => "ViewDefLocationAddressOnMap";
  }

  /// <exclude />
  public class ExtendToCustomer : ExtendToCustomerGraph<VendorMaint, VendorR>
  {
    protected override ExtendToCustomerGraph<VendorMaint, VendorR>.SourceAccountMapping GetSourceAccountMapping()
    {
      return new ExtendToCustomerGraph<VendorMaint, VendorR>.SourceAccountMapping(typeof (VendorR));
    }
  }

  /// <exclude />
  public class VendorRemitSharedContactOverrideGraphExt : 
    SharedChildOverrideGraphExt<VendorMaint, VendorMaint.VendorRemitSharedContactOverrideGraphExt>
  {
    public override bool ViewHasADelegate => true;

    public override bool IsChildRequired(
      CRParentChild<VendorMaint, VendorMaint.VendorRemitSharedContactOverrideGraphExt>.Document document)
    {
      return LocTypeList.ActsAsVendor(document.Base is PX.Objects.CR.Standalone.Location location ? location.LocType : (string) null);
    }

    protected override CRParentChild<VendorMaint, VendorMaint.VendorRemitSharedContactOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<VendorMaint, VendorMaint.VendorRemitSharedContactOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Standalone.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Standalone.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Standalone.Location.vRemitContactID),
        IsOverrideRelated = typeof (PX.Objects.CR.Standalone.Location.overrideRemitContact)
      };
    }

    protected override SharedChildOverrideGraphExt<VendorMaint, VendorMaint.VendorRemitSharedContactOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<VendorMaint, VendorMaint.VendorRemitSharedContactOverrideGraphExt>.RelatedMapping(typeof (VendorR))
      {
        RelatedID = typeof (VendorR.bAccountID),
        ChildID = typeof (Vendor.defContactID)
      };
    }

    protected override CRParentChild<VendorMaint, VendorMaint.VendorRemitSharedContactOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<VendorMaint, VendorMaint.VendorRemitSharedContactOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Contact))
      {
        ChildID = typeof (PX.Objects.CR.Contact.contactID),
        RelatedID = typeof (PX.Objects.CR.Contact.bAccountID)
      };
    }
  }

  /// <exclude />
  public class VendorRemitSharedAddressOverrideGraphExt : 
    SharedChildOverrideGraphExt<VendorMaint, VendorMaint.VendorRemitSharedAddressOverrideGraphExt>
  {
    public override bool ViewHasADelegate => true;

    public override bool IsChildRequired(
      CRParentChild<VendorMaint, VendorMaint.VendorRemitSharedAddressOverrideGraphExt>.Document document)
    {
      return LocTypeList.ActsAsVendor(document.Base is PX.Objects.CR.Standalone.Location location ? location.LocType : (string) null);
    }

    protected override CRParentChild<VendorMaint, VendorMaint.VendorRemitSharedAddressOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<VendorMaint, VendorMaint.VendorRemitSharedAddressOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Standalone.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Standalone.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Standalone.Location.vRemitAddressID),
        IsOverrideRelated = typeof (PX.Objects.CR.Standalone.Location.overrideRemitAddress)
      };
    }

    protected override SharedChildOverrideGraphExt<VendorMaint, VendorMaint.VendorRemitSharedAddressOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<VendorMaint, VendorMaint.VendorRemitSharedAddressOverrideGraphExt>.RelatedMapping(typeof (VendorR))
      {
        RelatedID = typeof (VendorR.bAccountID),
        ChildID = typeof (Vendor.defAddressID)
      };
    }

    protected override CRParentChild<VendorMaint, VendorMaint.VendorRemitSharedAddressOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<VendorMaint, VendorMaint.VendorRemitSharedAddressOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Address))
      {
        ChildID = typeof (PX.Objects.CR.Address.addressID),
        RelatedID = typeof (PX.Objects.CR.Address.bAccountID)
      };
    }
  }

  /// <exclude />
  public class VendorDefSharedContactOverrideGraphExt : 
    SharedChildOverrideGraphExt<VendorMaint, VendorMaint.VendorDefSharedContactOverrideGraphExt>
  {
    public override bool ViewHasADelegate => true;

    protected override CRParentChild<VendorMaint, VendorMaint.VendorDefSharedContactOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<VendorMaint, VendorMaint.VendorDefSharedContactOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Standalone.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Standalone.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Standalone.Location.defContactID),
        IsOverrideRelated = typeof (PX.Objects.CR.Standalone.Location.overrideContact)
      };
    }

    protected override SharedChildOverrideGraphExt<VendorMaint, VendorMaint.VendorDefSharedContactOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<VendorMaint, VendorMaint.VendorDefSharedContactOverrideGraphExt>.RelatedMapping(typeof (VendorR))
      {
        RelatedID = typeof (VendorR.bAccountID),
        ChildID = typeof (Vendor.defContactID)
      };
    }

    protected override CRParentChild<VendorMaint, VendorMaint.VendorDefSharedContactOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<VendorMaint, VendorMaint.VendorDefSharedContactOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Contact))
      {
        ChildID = typeof (PX.Objects.CR.Contact.contactID),
        RelatedID = typeof (PX.Objects.CR.Contact.bAccountID)
      };
    }
  }

  /// <exclude />
  public class VendorDefSharedAddressOverrideGraphExt : 
    SharedChildOverrideGraphExt<VendorMaint, VendorMaint.VendorDefSharedAddressOverrideGraphExt>
  {
    public override bool ViewHasADelegate => true;

    protected override CRParentChild<VendorMaint, VendorMaint.VendorDefSharedAddressOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<VendorMaint, VendorMaint.VendorDefSharedAddressOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Standalone.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Standalone.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Standalone.Location.defAddressID),
        IsOverrideRelated = typeof (PX.Objects.CR.Standalone.Location.overrideAddress)
      };
    }

    protected override SharedChildOverrideGraphExt<VendorMaint, VendorMaint.VendorDefSharedAddressOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<VendorMaint, VendorMaint.VendorDefSharedAddressOverrideGraphExt>.RelatedMapping(typeof (VendorR))
      {
        RelatedID = typeof (VendorR.bAccountID),
        ChildID = typeof (Vendor.defAddressID)
      };
    }

    protected override CRParentChild<VendorMaint, VendorMaint.VendorDefSharedAddressOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<VendorMaint, VendorMaint.VendorDefSharedAddressOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Address))
      {
        ChildID = typeof (PX.Objects.CR.Address.addressID),
        RelatedID = typeof (PX.Objects.CR.Address.bAccountID)
      };
    }
  }

  /// <exclude />
  public class CreateContactFromVendorGraphExt : CRCreateContactActionBase<VendorMaint, VendorR>
  {
    protected override PXSelectBase<CRPMTimeActivity> Activities
    {
      get
      {
        return (PXSelectBase<CRPMTimeActivity>) this.Base.GetExtension<VendorMaint_ActivityDetailsExt>().Activities;
      }
    }

    public override void Initialize()
    {
      base.Initialize();
      this.Addresses = new PXSelectExtension<DocumentAddress>((PXSelectBase) this.Base.GetExtension<VendorMaint.DefContactAddressExt>().DefAddress);
      this.Contacts = new PXSelectExtension<DocumentContact>((PXSelectBase) this.Base.GetExtension<VendorMaint.DefContactAddressExt>().DefContact);
    }

    protected override CRCreateActionBaseInit<VendorMaint, VendorR>.DocumentContactMapping GetDocumentContactMapping()
    {
      return new CRCreateActionBaseInit<VendorMaint, VendorR>.DocumentContactMapping(typeof (PX.Objects.CR.Contact))
      {
        Email = typeof (PX.Objects.CR.Contact.eMail)
      };
    }

    protected override CRCreateActionBaseInit<VendorMaint, VendorR>.DocumentAddressMapping GetDocumentAddressMapping()
    {
      return new CRCreateActionBaseInit<VendorMaint, VendorR>.DocumentAddressMapping(typeof (PX.Objects.CR.Address));
    }

    public virtual void _(PX.Data.Events.RowSelected<ContactFilter> e)
    {
      PXUIFieldAttribute.SetReadOnly<ContactFilter.fullName>(e.Cache, (object) e.Row, true);
    }

    public virtual void _(
      PX.Data.Events.FieldDefaulting<ContactFilter, ContactFilter.fullName> e)
    {
      e.NewValue = (object) this.Contacts.SelectSingle()?.FullName;
    }

    protected override void FillRelations(PXGraph graph, PX.Objects.CR.Contact target)
    {
    }

    protected override void FillNotesAndAttachments(
      PXGraph graph,
      object src_row,
      PXCache dst_cache,
      PX.Objects.CR.Contact dst_row)
    {
    }

    protected override IConsentable MapConsentable(DocumentContact source, IConsentable target)
    {
      return target;
    }
  }

  public class VendorMaint_CRDuplicateBAccountIdentifier : 
    CRDuplicateBAccountIdentifier<VendorMaint, VendorR>
  {
  }
}
