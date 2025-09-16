// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APQuickCheckEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.DependencyInjection;
using PX.LicensePolicy;
using PX.Objects.AP.Overrides.APDocumentRelease;
using PX.Objects.AP.Standalone;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Extensions;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;
using PX.Objects.Common.Interfaces;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.DR.Descriptor;
using PX.Objects.EP;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.Extensions.MultiCurrency.AP;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.Reclassification.UI;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.TX;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP;

public class APQuickCheckEntry : 
  APDataEntryGraph<APQuickCheckEntry, APQuickCheck>,
  IGraphWithInitialization
{
  public PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.stkItem, Equal<False>, And<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>> nonStockItem;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (APQuickCheck.extRefNbr), typeof (APQuickCheck.cleared), typeof (APQuickCheck.clearDate)})]
  [PXViewName("Cash Purchase")]
  public PXSelectJoin<APQuickCheck, LeftJoinSingleTable<Vendor, On<Vendor.bAccountID, Equal<APQuickCheck.vendorID>>>, Where<APQuickCheck.docType, Equal<Optional<APQuickCheck.docType>>, And<Where<Vendor.bAccountID, IsNull, Or<Match<Vendor, Current<AccessInfo.userName>>>>>>> Document;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (APQuickCheck.printCheck), typeof (APQuickCheck.cleared), typeof (APQuickCheck.clearDate)})]
  public PXSelect<APQuickCheck, Where<APQuickCheck.docType, Equal<Current<APQuickCheck.docType>>, And<APQuickCheck.refNbr, Equal<Current<APQuickCheck.refNbr>>>>> CurrentDocument;
  [PXViewName("Cash Purchase Line")]
  public PXSelect<APTran, Where<APTran.tranType, Equal<Current<APQuickCheck.docType>>, And<APTran.refNbr, Equal<Current<APQuickCheck.refNbr>>>>> Transactions;
  public PXSelect<APTax> ItemTaxes;
  [PXCopyPasteHiddenView]
  public PXSelect<APTax, Where<APTax.tranType, Equal<Current<APInvoice.docType>>, And<APTax.refNbr, Equal<Current<APInvoice.refNbr>>>>, OrderBy<Asc<APTax.tranType, Asc<APTax.refNbr, Asc<APTax.taxID>>>>> Tax_Rows;
  public PXSelectJoin<APTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.module, Equal<BatchModule.moduleAP>, And<APTaxTran.tranType, Equal<Current<APQuickCheck.docType>>, And<APTaxTran.refNbr, Equal<Current<APQuickCheck.refNbr>>>>>> Taxes;
  public PXSelectReadonly2<APTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.module, Equal<BatchModule.moduleAP>, And<APTaxTran.tranType, Equal<Current<APQuickCheck.docType>>, And<APTaxTran.refNbr, Equal<Current<APQuickCheck.refNbr>>, And<PX.Objects.TX.Tax.taxType, Equal<CSTaxType.use>>>>>> UseTaxes;
  [PXViewName("AP Address")]
  public PXSelect<APAddress, Where<APAddress.addressID, Equal<Current<APQuickCheck.remitAddressID>>>> Remittance_Address;
  [PXViewName("AP Contact")]
  public PXSelect<APContact, Where<APContact.contactID, Equal<Current<APQuickCheck.remitContactID>>>> Remittance_Contact;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<APQuickCheck.curyInfoID>>>> currencyinfo;
  public PXSelect<PaymentMethodAccount, Where<PaymentMethodAccount.cashAccountID, Equal<Current<APQuickCheck.cashAccountID>>, And<PaymentMethodAccount.useForAP, Equal<PX.Data.True>>>, OrderBy<Asc<PaymentMethodAccount.aPIsDefault>>> CashAcctDetail_AccountID;
  public APPaymentChargeSelect<APQuickCheck, APQuickCheck.paymentMethodID, APQuickCheck.cashAccountID, APQuickCheck.docDate, APQuickCheck.tranPeriodID, Where<APPaymentChargeTran.docType, Equal<Current<APQuickCheck.docType>>, And<APPaymentChargeTran.refNbr, Equal<Current<APQuickCheck.refNbr>>>>> PaymentCharges;
  public PXSetup<Vendor, Where<Vendor.bAccountID, Equal<Current<APQuickCheck.vendorID>>>> vendor;
  public PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Current<APQuickCheck.vendorID>>>> EmployeeByVendor;
  [PXViewName("Employee")]
  public PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.defContactID, Equal<Current<APRegister.employeeID>>>> employee;
  public PXSetup<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APQuickCheck.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Optional<APQuickCheck.vendorLocationID>>>>> location;
  public PXSetup<PX.Objects.CA.CashAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Optional<APQuickCheck.cashAccountID>>>> cashaccount;
  public PXSetup<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Optional<APQuickCheck.paymentMethodID>>>> paymenttype;
  public PXSetup<PaymentMethodAccount, Where<PaymentMethodAccount.cashAccountID, Equal<Optional<APQuickCheck.cashAccountID>>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<APQuickCheck.paymentMethodID>>>>> cashaccountdetail;
  public PXSetup<OrganizationFinPeriod, Where<OrganizationFinPeriod.finPeriodID, Equal<Current<APQuickCheck.adjFinPeriodID>>, And<EqualToOrganizationOfBranch<OrganizationFinPeriod.organizationID, Current<APQuickCheck.branchID>>>>> finperiod;
  public PXSetup<PX.Objects.TX.TaxZone, Where<PX.Objects.TX.TaxZone.taxZoneID, Equal<Current<APQuickCheck.taxZoneID>>>> taxzone;
  public PXSelect<AP1099Hist> ap1099hist;
  public PXSelect<AP1099Yr> ap1099year;
  public PXSetup<GLSetup> glsetup;
  public PXSelect<APSetupApproval, Where<Current<APQuickCheck.docType>, Equal<APDocType.quickCheck>, And<APSetupApproval.docType, Equal<APDocType.quickCheck>>>> SetupApproval;
  [PXViewName("Approval")]
  public EPApprovalAutomationWithoutHoldDefaulting<APQuickCheck, APRegister.approved, APRegister.rejected, APQuickCheck.hold, APSetupApproval> Approval;
  [PXCopyPasteHiddenView]
  public PXSelect<APAdjust> dummy_APAdjust;
  public PXSelect<CashAccountCheck> dummy_CashAccountCheck;
  public PXAction<APQuickCheck> printAPEdit;
  public PXAction<APQuickCheck> printAPRegister;
  public PXAction<APQuickCheck> printAPPayment;
  public PXAction<APQuickCheck> printCheck;
  public PXAction<APQuickCheck> prebook;
  public PXAction<APQuickCheck> cashReturn;
  public PXAction<APQuickCheck> reclassifyBatch;
  public PXAction<APQuickCheck> vendorDocuments;
  public PXAction<APQuickCheck> viewSchedule;
  public PXAction<APQuickCheck> validateAddresses;
  public PXAction<APQuickCheck> ViewOriginalDocument;
  protected bool InternalCall;
  private bool _IsVoidCheckInProgress;
  private bool _IsCashReturnInProgress;

  [InjectDependency]
  public IFinPeriodUtils FinPeriodUtils { get; set; }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "AP Edit Detailed", MapEnableRights = PXCacheRights.Select)]
  public virtual IEnumerable PrintAPEdit(PXAdapter adapter, string reportID = null)
  {
    return this.Report(adapter, reportID ?? "AP610500");
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "AP Register Detailed", MapEnableRights = PXCacheRights.Select)]
  public virtual IEnumerable PrintAPRegister(PXAdapter adapter, string reportID = null)
  {
    return this.Report(adapter, reportID ?? "AP622000");
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "AP Payment Register", MapEnableRights = PXCacheRights.Select)]
  public virtual IEnumerable PrintAPPayment(PXAdapter adapter, string reportID = null)
  {
    return this.Report(adapter, reportID ?? "AP622500");
  }

  [PXUIField(DisplayName = "Print/Process", MapEnableRights = PXCacheRights.Select)]
  [PXButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable PrintCheck(PXAdapter adapter)
  {
    if (this.IsDirty)
      this.Save.Press();
    APPayment apPayment = (APPayment) PXSelectBase<APPayment, PXSelect<APPayment, Where<APPayment.docType, Equal<Current<APQuickCheck.docType>>, And<APPayment.refNbr, Equal<Current<APQuickCheck.refNbr>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) this.Document.Current
    });
    APPrintChecks instance = PXGraph.CreateInstance<APPrintChecks>();
    PrintChecksFilter copy = PXCache<PrintChecksFilter>.CreateCopy(instance.Filter.Current);
    copy.BranchID = this.CurrentDocument.Current.BranchID;
    copy.PayAccountID = apPayment.CashAccountID;
    copy.PayTypeID = apPayment.PaymentMethodID;
    instance.Filter.Cache.Update((object) copy);
    apPayment.Selected = new bool?(true);
    apPayment.Passed = new bool?(true);
    instance.APPaymentList.Cache.Update((object) apPayment);
    instance.APPaymentList.Cache.SetStatus((object) apPayment, PXEntryStatus.Updated);
    instance.APPaymentList.Cache.IsDirty = false;
    throw new PXRedirectRequiredException((PXGraph) instance, "Preview");
  }

  [PXUIField(DisplayName = "Release", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton]
  [APMigrationModeDependentActionRestriction(false, true, true)]
  public override IEnumerable Release(PXAdapter adapter)
  {
    PXCache cache = this.Document.Cache;
    List<APRegister> list = new List<APRegister>();
    foreach (APQuickCheck row in adapter.Get<APQuickCheck>())
    {
      if (row.Status != "B" && row.Status != "P" && row.Status != "K")
        throw new PXException("Document Status is invalid for processing.");
      if (this.PaymentRefMustBeUnique && string.IsNullOrEmpty(row.ExtRefNbr))
        cache.RaiseExceptionHandling<APQuickCheck.extRefNbr>((object) row, (object) row.ExtRefNbr, (Exception) new PXRowPersistingException(typeof (APQuickCheck.extRefNbr).Name, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<APQuickCheck.extRefNbr>(cache)
        }));
      cache.Update((object) row);
      list.Add((APRegister) row);
    }
    this.Save.Press();
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => PX.Objects.AP.APDocumentRelease.ReleaseDoc(list, false)));
    return (IEnumerable) list;
  }

  [PXUIField(DisplayName = "Pre-release", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable Prebook(PXAdapter adapter)
  {
    PXCache cache = this.Document.Cache;
    List<APRegister> list = new List<APRegister>();
    foreach (APQuickCheck row in adapter.Get<APQuickCheck>())
    {
      if (row.Status != "B" && row.Status != "P")
        throw new PXException("Document Status is invalid for processing.");
      if (!row.PrebookAcctID.HasValue)
        cache.RaiseExceptionHandling<APQuickCheck.prebookAcctID>((object) row, (object) row.PrebookAcctID, (Exception) new PXSetPropertyException((IBqlTable) row, "To release the document, specify the reclassification account."));
      else if (!row.PrebookSubID.HasValue)
      {
        cache.RaiseExceptionHandling<APQuickCheck.prebookSubID>((object) row, (object) row.PrebookSubID, (Exception) new PXSetPropertyException((IBqlTable) row, "To release the document, specify the reclassification account."));
      }
      else
      {
        if (this.PaymentRefMustBeUnique && string.IsNullOrEmpty(row.ExtRefNbr))
          cache.RaiseExceptionHandling<APQuickCheck.extRefNbr>((object) row, (object) row.ExtRefNbr, (Exception) new PXRowPersistingException(typeof (APQuickCheck.extRefNbr).Name, (object) null, "'{0}' cannot be empty.", new object[1]
          {
            (object) PXUIFieldAttribute.GetDisplayName<APQuickCheck.extRefNbr>(cache)
          }));
        cache.Update((object) row);
        list.Add((APRegister) row);
      }
    }
    this.Save.Press();
    PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => PX.Objects.AP.APDocumentRelease.ReleaseDoc(list, false, true)));
    return (IEnumerable) list;
  }

  protected virtual bool AskUserApprovalToVoidQuickCheck(APQuickCheck apQuickCheck)
  {
    return !apQuickCheck.Deposited.GetValueOrDefault() || this.Document.View.Ask(PXMessages.LocalizeNoPrefix("The payment has already been deposited. To proceed, click OK."), MessageButtons.OKCancel) == WebDialogResult.OK;
  }

  [PXUIField(DisplayName = "Void", MapEnableRights = PXCacheRights.Update, MapViewRights = PXCacheRights.Update)]
  [PXProcessButton]
  [APMigrationModeDependentActionRestriction(false, true, true)]
  public override IEnumerable VoidCheck(PXAdapter adapter)
  {
    if (this.Document.Current != null)
    {
      bool? nullable = this.Document.Current.Released;
      if (!nullable.GetValueOrDefault())
      {
        nullable = this.Document.Current.Prebooked;
        if (!nullable.GetValueOrDefault())
          goto label_16;
      }
      nullable = this.Document.Current.Voided;
      bool flag = false;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue && this.Document.Current.DocType == "QCK")
      {
        APQuickCheck copy = PXCache<APQuickCheck>.CreateCopy(this.Document.Current);
        this.FinPeriodUtils.VerifyAndSetFirstOpenedFinPeriod<APQuickCheck.finPeriodID, APQuickCheck.branchID>(this.Document.Cache, (object) copy, (PXSelectBase<OrganizationFinPeriod>) this.finperiod, typeof (OrganizationFinPeriod.aPClosed));
        APQuickCheck apQuickCheck = (APQuickCheck) this.Document.Search<APQuickCheck.refNbr>((object) this.Document.Current.RefNbr, (object) APPaymentType.GetVoidingAPDocType(this.Document.Current.DocType));
        if (apQuickCheck != null)
        {
          if (this.IsContractBasedAPI)
            return (IEnumerable) new APQuickCheck[1]
            {
              apQuickCheck
            };
          this.Document.Current = apQuickCheck;
          throw new PXRedirectRequiredException((PXGraph) this, "Void");
        }
        if (!this.AskUserApprovalToVoidQuickCheck(this.Document.Current))
          return adapter.Get();
        try
        {
          this._IsVoidCheckInProgress = true;
          this.VoidCheckProc(copy);
        }
        catch (PXSetPropertyException ex)
        {
          this.Clear();
          this.Document.Current = copy;
          throw;
        }
        finally
        {
          this._IsVoidCheckInProgress = false;
        }
        this.Document.Cache.RaiseExceptionHandling<APQuickCheck.finPeriodID>((object) this.Document.Current, (object) this.Document.Current.FinPeriodID, (Exception) null);
        if (!this.IsContractBasedAPI && !this.IsImport)
          throw new PXRedirectRequiredException((PXGraph) this, "Voided");
        return (IEnumerable) new APQuickCheck[1]
        {
          this.Document.Current
        };
      }
    }
label_16:
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Return")]
  [PXProcessButton]
  [APMigrationModeDependentActionRestriction(false, true, true)]
  public virtual IEnumerable CashReturn(PXAdapter adapter)
  {
    if (this.Document.Current != null)
    {
      bool? nullable = this.Document.Current.Released;
      if (nullable.GetValueOrDefault())
      {
        nullable = this.Document.Current.Voided;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue && this.Document.Current.DocType == "QCK")
        {
          APQuickCheck copy = PXCache<APQuickCheck>.CreateCopy(this.Document.Current);
          this.FinPeriodUtils.VerifyAndSetFirstOpenedFinPeriod<APQuickCheck.finPeriodID, APQuickCheck.branchID>(this.Document.Cache, (object) copy, (PXSelectBase<OrganizationFinPeriod>) this.finperiod, typeof (OrganizationFinPeriod.aPClosed));
          if (!this.AskUserApprovalIfCashReturnDocumentAlreadyExists(this.Document.Current))
            return adapter.Get();
          try
          {
            this._IsCashReturnInProgress = true;
            this.CashReturnProc(copy);
          }
          catch (PXSetPropertyException ex)
          {
            this.Clear();
            this.Document.Current = copy;
            throw;
          }
          finally
          {
            this._IsCashReturnInProgress = false;
          }
          this.Document.Cache.RaiseExceptionHandling<APQuickCheck.finPeriodID>((object) this.Document.Current, (object) this.Document.Current.FinPeriodID, (Exception) null);
          if (!this.IsContractBasedAPI && !this.IsImport)
            throw new PXRedirectRequiredException((PXGraph) this, "Cash Return");
          return (IEnumerable) new APQuickCheck[1]
          {
            this.Document.Current
          };
        }
      }
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Reclassify GL Batch", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  [APMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable ReclassifyBatch(PXAdapter adapter)
  {
    APQuickCheck current = this.Document.Current;
    if (current != null)
      ReclassifyTransactionsProcess.TryOpenForReclassificationOfDocument(this.Document.View, "AP", current.BatchNbr, current.DocType, current.RefNbr);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Vendor Details", MapEnableRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable VendorDocuments(PXAdapter adapter)
  {
    if (this.vendor.Current != null)
    {
      APDocumentEnq instance = PXGraph.CreateInstance<APDocumentEnq>();
      instance.Filter.Current.VendorID = this.vendor.Current.BAccountID;
      instance.Filter.Select();
      throw new PXRedirectRequiredException((PXGraph) instance, "Vendor Details");
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Deferrals")]
  [PXButton(ImageKey = "Settings")]
  public virtual IEnumerable ViewSchedule(PXAdapter adapter)
  {
    APTran current = this.Transactions.Current;
    PXResultset<APInvoice> document = PXSelectBase<APInvoice, PXSelect<APInvoice, Where<APInvoice.docType, Equal<Current<APQuickCheck.docType>>, And<APInvoice.refNbr, Equal<Current<APQuickCheck.refNbr>>>>>.Config>.Select((PXGraph) this);
    if (current != null && document != null && this.Transactions.Cache.GetStatus((object) current) == PXEntryStatus.Notchanged)
    {
      this.Save.Press();
      APInvoiceEntry.ViewScheduleForLine((PXGraph) this, (APInvoice) document, this.Transactions.Current);
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Validate Addresses", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, FieldClass = "Validate Address")]
  [PXButton]
  [APMigrationModeDependentActionRestriction(false, true, true)]
  public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
  {
    APQuickCheckEntry apQuickCheckEntry = this;
    foreach (APQuickCheck apQuickCheck in adapter.Get<APQuickCheck>())
    {
      if (apQuickCheck != null)
        apQuickCheckEntry.FindAllImplementations<IAddressValidationHelper>().ValidateAddresses();
      yield return (object) apQuickCheck;
    }
  }

  [PXUIField(Visible = false, MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXLookupButton]
  protected virtual IEnumerable viewOriginalDocument(PXAdapter adapter)
  {
    RedirectionToOrigDoc.TryRedirect(this.Document.Current.OrigDocType, this.Document.Current.OrigRefNbr, this.Document.Current.OrigModule);
    return adapter.Get();
  }

  [InjectDependency]
  protected ILicenseLimitsService _licenseLimits { get; set; }

  public APQuickCheckEntry()
  {
    APSetup apSetup = (APSetup) this.apsetup.Select();
    GLSetup glSetup = (GLSetup) this.glsetup.Select();
    OpenPeriodAttribute.SetValidatePeriod<APQuickCheck.adjFinPeriodID>(this.Document.Cache, (object) null, PeriodValidation.DefaultSelectUpdate);
    PXUIFieldAttribute.SetVisible<APTran.projectID>(this.Transactions.Cache, (object) null, ProjectAttribute.IsPMVisible("AP"));
    PXUIFieldAttribute.SetVisible<APTran.taskID>(this.Transactions.Cache, (object) null, ProjectAttribute.IsPMVisible("AP"));
    PXUIFieldAttribute.SetVisible<APTran.nonBillable>(this.Transactions.Cache, (object) null, ProjectAttribute.IsPMVisible("AP"));
    this.FieldDefaulting.AddHandler<PX.Objects.IN.InventoryItem.stkItem>((PXFieldDefaulting) ((sender, e) =>
    {
      if (e.Row == null)
        return;
      e.NewValue = (object) false;
    }));
    TaxBaseAttribute.IncludeDirectTaxLine<APTran.taxCategoryID>(this.Transactions.Cache, (object) null, true);
  }

  void IGraphWithInitialization.Initialize()
  {
    if (this._licenseLimits == null)
      return;
    this.OnBeforeCommit += this._licenseLimits.GetCheckerDelegate<APQuickCheck>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (APTran), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[2]
      {
        (PXDataFieldValue) new PXDataFieldValue<APTran.tranType>(PXDbType.Char, new int?(3), (object) ((APQuickCheckEntry) graph).Document.Current?.DocType),
        (PXDataFieldValue) new PXDataFieldValue<APTran.refNbr>((object) ((APQuickCheckEntry) graph).Document.Current?.RefNbr)
      }))
    });
  }

  public override void Persist()
  {
    base.Persist();
    APTran current = this.Transactions.Current;
    this.Transactions.Cache.Clear();
    this.Transactions.View.Clear();
    if (current == null)
      return;
    this.Transactions.Current = APTran.PK.Find((PXGraph) this, current.TranType, current.RefNbr, current.LineNbr);
  }

  [PXDefault(typeof (Search<INPostClass.cOGSSubID, Where<INPostClass.postClassID, Equal<Current<PX.Objects.IN.InventoryItem.postClassID>>>>))]
  [SubAccount(typeof (PX.Objects.IN.InventoryItem.cOGSAcctID), DisplayName = "Expense Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
  public virtual void InventoryItem_COGSSubID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void APQuickCheck_Cleared_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    APQuickCheck row = (APQuickCheck) e.Row;
    if (row.Cleared.GetValueOrDefault())
    {
      if (row.ClearDate.HasValue)
        return;
      row.ClearDate = row.DocDate;
    }
    else
      row.ClearDate = new System.DateTime?();
  }

  protected virtual void APQuickCheck_DocType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) "QCK";
    e.Cancel = true;
  }

  protected virtual void APQuickCheck_DocDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null)
      return;
    bool? released = ((APRegister) e.Row).Released;
    bool flag = false;
    if (!(released.GetValueOrDefault() == flag & released.HasValue))
      return;
    e.NewValue = (object) ((APQuickCheck) e.Row).AdjDate;
    e.Cancel = true;
  }

  protected virtual void APQuickCheck_DocDesc_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    APQuickCheck row = (APQuickCheck) e.Row;
    int num;
    if (row == null)
    {
      num = 1;
    }
    else
    {
      bool? released = row.Released;
      bool flag = false;
      num = !(released.GetValueOrDefault() == flag & released.HasValue) ? 1 : 0;
    }
    if (num != 0)
      return;
    foreach (PXResult<APTaxTran> pxResult in this.Taxes.Select())
    {
      APTaxTran apTaxTran = (APTaxTran) pxResult;
      apTaxTran.Description = row.DocDesc;
      this.Taxes.Cache.Update((object) apTaxTran);
    }
  }

  protected virtual void APQuickCheck_FinPeriodID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null)
      return;
    e.NewValue = (object) ((APQuickCheck) e.Row).AdjFinPeriodID;
    e.Cancel = true;
  }

  protected virtual void APQuickCheck_TranPeriodID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null)
      return;
    e.NewValue = (object) ((APQuickCheck) e.Row).AdjTranPeriodID;
    e.Cancel = true;
  }

  protected virtual void APQuickCheck_VendorID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.vendor.RaiseFieldUpdated(sender, e.Row);
    sender.SetDefaultExt<APQuickCheck.vendorLocationID>(e.Row);
    sender.SetDefaultExt<APQuickCheck.termsID>(e.Row);
    if (!(this.vendor.Current?.Type == "EP"))
      return;
    sender.SetDefaultExt<APRegister.employeeID>(e.Row);
  }

  protected virtual void APQuickCheck_VendorLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.location.RaiseFieldUpdated(sender, e.Row);
    sender.SetDefaultExt<APQuickCheck.paymentMethodID>(e.Row);
    sender.SetDefaultExt<APQuickCheck.aPAccountID>(e.Row);
    sender.SetDefaultExt<APQuickCheck.aPSubID>(e.Row);
    SharedRecordAttribute.DefaultRecord<APPayment.remitAddressID>(sender, e.Row);
    SharedRecordAttribute.DefaultRecord<APPayment.remitContactID>(sender, e.Row);
    sender.SetDefaultExt<APQuickCheck.taxCalcMode>(e.Row);
    sender.SetDefaultExt<APQuickCheck.taxZoneID>(e.Row);
    sender.SetDefaultExt<APQuickCheck.prebookAcctID>(e.Row);
    sender.SetDefaultExt<APQuickCheck.prebookSubID>(e.Row);
  }

  protected virtual void APQuickCheck_CashAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    APQuickCheck row = (APQuickCheck) e.Row;
    this.cashaccount.RaiseFieldUpdated(sender, e.Row);
    row.Cleared = new bool?(false);
    row.ClearDate = new System.DateTime?();
    if (this.cashaccount.Current != null)
    {
      bool? reconcile = this.cashaccount.Current.Reconcile;
      bool flag = false;
      if (reconcile.GetValueOrDefault() == flag & reconcile.HasValue)
      {
        row.Cleared = new bool?(true);
        row.ClearDate = row.DocDate;
      }
    }
    sender.SetDefaultExt<APQuickCheck.depositAsBatch>(e.Row);
    sender.SetDefaultExt<APQuickCheck.depositAfter>(e.Row);
  }

  protected virtual void APQuickCheck_Hold_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    APQuickCheck row = (APQuickCheck) e.Row;
    if (!this.IsApprovalRequired(row))
      return;
    sender.SetValue<APQuickCheck.hold>((object) row, (object) true);
  }

  protected virtual void APQuickCheck_PaymentMethodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.paymenttype.RaiseFieldUpdated(sender, e.Row);
    sender.SetDefaultExt<APQuickCheck.cashAccountID>(e.Row);
    sender.SetDefaultExt<APQuickCheck.printCheck>(e.Row);
  }

  protected virtual void APQuickCheck_PrintCheck_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    string docType = ((APRegister) e.Row).DocType;
    if (!(docType == "REF") && !(docType == "PPM") && !(docType == "VQC"))
      return;
    e.NewValue = (object) false;
    e.Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<APQuickCheck, APQuickCheck.depositAfter> e)
  {
    if (!(e.Row.DocType == "RQC") || !e.Row.DepositAsBatch.GetValueOrDefault())
      return;
    e.NewValue = (object) e.Row.AdjDate;
    e.Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<APQuickCheck, APQuickCheck.depositAsBatch> e)
  {
    if (!(e.Row.DocType == "RQC"))
      return;
    e.Cache.SetDefaultExt<APQuickCheck.depositAfter>((object) e.Row);
  }

  /// <summary>
  /// </summary>
  /// <param name="payment"></param>
  /// <returns></returns>
  protected virtual bool MustPrintCheck(APQuickCheck quickCheck)
  {
    return APPaymentEntry.MustPrintCheck((IPrintCheckControlable) quickCheck, this.paymenttype.Current);
  }

  protected virtual void APQuickCheck_PrintCheck_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!this.MustPrintCheck(e.Row as APQuickCheck))
      return;
    sender.SetValueExt<APQuickCheck.extRefNbr>(e.Row, (object) null);
  }

  protected virtual void APQuickCheck_AdjDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is APQuickCheck row))
      return;
    bool? nullable = row.VoidAppl;
    if (nullable.GetValueOrDefault() || this.vendor.Current == null)
      return;
    nullable = this.vendor.Current.Vendor1099;
    if (!nullable.Value)
      return;
    AP1099Year ap1099Year = (AP1099Year) PXSelectBase<AP1099Year, PXSelect<AP1099Year, Where<AP1099Year.finYear, Equal<Required<AP1099Year.finYear>>, And<AP1099Year.organizationID, Equal<Required<AP1099Year.organizationID>>>>>.Config>.Select((PXGraph) this, (object) ((System.DateTime) e.NewValue).Year.ToString(), (object) PXAccess.GetParentOrganizationID(row.BranchID));
    if (ap1099Year != null && ap1099Year.Status != "N")
      throw new PXSetPropertyException("The cash purchase date must be within an open 1099 year.");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<APQuickCheck, APQuickCheck.branchID> e)
  {
    APQuickCheck row = e.Row;
    if (row == null || row.VoidAppl.GetValueOrDefault() || this.vendor.Current == null || !this.vendor.Current.Vendor1099.GetValueOrDefault())
      return;
    AP1099Year ap1099Year = (AP1099Year) PXSelectBase<AP1099Year, PXSelect<AP1099Year, Where<AP1099Year.finYear, Equal<Required<AP1099Year.finYear>>, And<AP1099Year.organizationID, Equal<Required<AP1099Year.organizationID>>>>>.Config>.Select((PXGraph) this, (object) row.AdjDate.Value.Year.ToString(), (object) PXAccess.GetParentOrganizationID((int?) e.NewValue));
    if (ap1099Year != null && ap1099Year.Status != "N")
    {
      PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this, (int?) e.NewValue);
      e.NewValue = (object) branch.BranchCD;
      throw new PXSetPropertyException("The cash purchase date must be within an open 1099 year.");
    }
  }

  protected virtual void APQuickCheck_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    APQuickCheck row = (APQuickCheck) e.Row;
    if (!row.CashAccountID.HasValue)
    {
      if (sender.RaiseExceptionHandling<APQuickCheck.cashAccountID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[cashAccountID]"
      })))
        throw new PXRowPersistingException(typeof (APQuickCheck.cashAccountID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) "cashAccountID"
        });
    }
    if (string.IsNullOrEmpty(row.PaymentMethodID))
    {
      if (sender.RaiseExceptionHandling<APQuickCheck.paymentMethodID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "[paymentMethodID]"
      })))
        throw new PXRowPersistingException(typeof (APQuickCheck.paymentMethodID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) "paymentMethodID"
        });
    }
    this.ValidateTaxConfiguration(sender, row);
    PX.Objects.CS.Terms terms = (PX.Objects.CS.Terms) PXSelectorAttribute.Select<APQuickCheck.termsID>(this.Document.Cache, (object) row);
    if (terms == null)
    {
      sender.SetValue<APQuickCheck.termsID>((object) row, (object) null);
    }
    else
    {
      if (!PX.Objects.CM.PXCurrencyAttribute.IsNullOrEmpty(terms.DiscPercent) && (PX.Objects.EP.EPEmployee) PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Current<APQuickCheck.vendorID>>>>.Config>.Select((PXGraph) this) != null)
        sender.RaiseExceptionHandling<APQuickCheck.termsID>((object) row, (object) row.TermsID, (Exception) new PXSetPropertyException("Terms discounts are not allowed for Employees.", new object[1]
        {
          (object) "[termsID]"
        }));
      if (terms.InstallmentType == "M")
        sender.RaiseExceptionHandling<APQuickCheck.termsID>((object) row, (object) row.TermsID, (Exception) new PXSetPropertyException("Multiple installments are not allowed for cash purchases.", new object[1]
        {
          (object) "[termsID]"
        }));
      if (string.IsNullOrEmpty(row.ExtRefNbr) && this.PaymentRefMustBeUnique && row.Status == "K")
        sender.RaiseExceptionHandling<APQuickCheck.extRefNbr>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty."));
      PaymentRefAttribute.SetUpdateCashManager<APQuickCheck.extRefNbr>(sender, e.Row, ((APRegister) e.Row).DocType != "VQC" && ((APRegister) e.Row).DocType != "REF");
    }
  }

  private void ValidateTaxConfiguration(PXCache cache, APQuickCheck cashSale)
  {
    bool flag1 = false;
    bool flag2 = false;
    foreach (PXResult<APTax, PX.Objects.TX.Tax> pxResult in PXSelectBase<APTax, PXSelectJoin<APTax, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTax.taxID>>>, Where<APTax.tranType, Equal<Current<APQuickCheck.docType>>, And<APTax.refNbr, Equal<Current<APQuickCheck.refNbr>>>>>.Config>.Select((PXGraph) this))
    {
      PX.Objects.TX.Tax tax = (PX.Objects.TX.Tax) pxResult;
      if (tax.TaxApplyTermsDisc == "P")
        flag1 = true;
      if (tax.TaxApplyTermsDisc == "X")
        flag2 = true;
      if (flag1 & flag2)
        cache.RaiseExceptionHandling<APQuickCheck.taxZoneID>((object) cashSale, (object) cashSale.TaxZoneID, (Exception) new PXSetPropertyException("Tax configuration is invalid. A document cannot contain both Reduce Taxable Amount and Reduce Taxable Amount On Early Payment taxes."));
    }
  }

  protected virtual bool PaymentRefMustBeUnique
  {
    get => PaymentRefAttribute.PaymentRefMustBeUnique(this.paymenttype.Current);
  }

  private bool IsApprovalRequired(APQuickCheck doc)
  {
    return EPApprovalSettings<APSetupApproval, APSetupApproval.docType, APDocType, APDocStatus.hold, APDocStatus.pendingApproval, APDocStatus.rejected>.ApprovableDocTypes.Contains(doc.DocType);
  }

  protected virtual void APQuickCheck_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is APQuickCheck row))
      return;
    this.release.SetEnabled(true);
    this.prebook.SetEnabled(true);
    this.reclassifyBatch.SetEnabled(true);
    bool flag1 = !this.IsApprovalRequired(row);
    bool? nullable1 = row.DontApprove;
    bool flag2 = flag1;
    if (!(nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue))
      cache.SetValueExt<APRegister.dontApprove>((object) row, (object) flag1);
    if (this.InternalCall)
      return;
    this.PaymentCharges.Cache.AllowSelect = true;
    bool isVisible1 = row.DocType != "ADR";
    PXUIFieldAttribute.SetVisible<APQuickCheck.curyID>(cache, (object) row, PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>());
    PXUIFieldAttribute.SetVisible<APQuickCheck.cashAccountID>(cache, (object) row, isVisible1);
    PXUIFieldAttribute.SetVisible<APQuickCheck.cleared>(cache, (object) row, isVisible1);
    PXUIFieldAttribute.SetVisible<APQuickCheck.clearDate>(cache, (object) row, isVisible1);
    PXUIFieldAttribute.SetVisible<APQuickCheck.paymentMethodID>(cache, (object) row, isVisible1);
    PXUIFieldAttribute.SetVisible<APQuickCheck.extRefNbr>(cache, (object) row, isVisible1);
    PXUIFieldAttribute.SetEnabled(this.Transactions.Cache, (string) null, true);
    nullable1 = row.Released;
    int num1;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = row.Prebooked;
      num1 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 1;
    bool flag3 = num1 != 0;
    nullable1 = row.Hold;
    int num2;
    if (nullable1.GetValueOrDefault() && this.cashaccount.Current != null)
    {
      nullable1 = this.cashaccount.Current.Reconcile;
      num2 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    bool isEnabled1 = num2 != 0;
    PXUIFieldAttribute.SetRequired<APQuickCheck.cashAccountID>(cache, !flag3);
    PXUIFieldAttribute.SetRequired<APQuickCheck.paymentMethodID>(cache, !flag3);
    PXUIFieldAttribute.SetRequired<APQuickCheck.extRefNbr>(cache, !flag3 && this.PaymentRefMustBeUnique);
    PaymentRefAttribute.SetUpdateCashManager<APQuickCheck.extRefNbr>(cache, e.Row, row.DocType != "VQC" && row.DocType != "REF");
    nullable1 = row.Prebooked;
    int num3;
    if (nullable1.GetValueOrDefault())
    {
      nullable1 = row.Released;
      bool flag4 = false;
      num3 = nullable1.GetValueOrDefault() == flag4 & nullable1.HasValue ? 1 : 0;
    }
    else
      num3 = 0;
    bool flag5 = num3 != 0;
    bool flag6 = APPaymentEntry.IsCheckReallyPrinted((IPrintCheckControlable) row);
    int? nullable2;
    if (row.DocType == "VQC" && !flag3)
    {
      PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<APQuickCheck.adjDate>(cache, (object) row, true);
      PXUIFieldAttribute.SetEnabled<APQuickCheck.adjFinPeriodID>(cache, (object) row, true);
      PXUIFieldAttribute.SetEnabled<APQuickCheck.docDesc>(cache, (object) row, true);
      PXUIFieldAttribute.SetEnabled<APQuickCheck.hold>(cache, (object) row, true);
      cache.AllowUpdate = true;
      cache.AllowDelete = true;
      this.Transactions.Cache.AllowDelete = false;
      this.Transactions.Cache.AllowUpdate = false;
      this.Transactions.Cache.AllowInsert = false;
      this.Taxes.Cache.AllowUpdate = false;
    }
    else
    {
      nullable1 = row.Released;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = row.Voided;
        if (!nullable1.GetValueOrDefault())
        {
          nullable1 = row.Prebooked;
          if (!nullable1.GetValueOrDefault())
          {
            PXUIFieldAttribute.SetEnabled(cache, (object) row, true);
            PXUIFieldAttribute.SetEnabled<APQuickCheck.status>(cache, (object) row, false);
            PXUIFieldAttribute.SetEnabled<APQuickCheck.curyID>(cache, (object) row, false);
            PXUIFieldAttribute.SetEnabled<APQuickCheck.printCheck>(cache, (object) row, !flag6 && row.DocType != "VQC" && row.DocType != "RQC" && row.DocType != "PPM" && row.DocType != "REF" && row.DocType != "ADR");
            cache.AllowDelete = !flag6;
            cache.AllowUpdate = true;
            this.Transactions.Cache.AllowDelete = true;
            this.Transactions.Cache.AllowUpdate = true;
            PXCache cache1 = this.Transactions.Cache;
            int num4;
            if (row.VendorID.HasValue)
            {
              nullable2 = row.VendorLocationID;
              num4 = nullable2.HasValue ? 1 : 0;
            }
            else
              num4 = 0;
            cache1.AllowInsert = num4 != 0;
            this.Remittance_Address.Cache.AllowUpdate = true;
            this.Remittance_Contact.Cache.AllowUpdate = true;
            this.Taxes.Cache.AllowUpdate = true;
            goto label_41;
          }
        }
      }
      int num5;
      if (this.vendor.Current != null)
      {
        nullable1 = this.vendor.Current.Vendor1099;
        if (nullable1.GetValueOrDefault())
        {
          nullable1 = row.Voided;
          bool flag7 = false;
          num5 = nullable1.GetValueOrDefault() == flag7 & nullable1.HasValue ? 1 : 0;
          goto label_23;
        }
      }
      num5 = 0;
label_23:
      bool flag8 = num5 != 0;
      foreach (PXResult<APAdjust> pxResult in PXSelectBase<APAdjust, PXSelect<APAdjust, Where<APAdjust.adjgDocType, Equal<Required<APAdjust.adjgDocType>>, And<APAdjust.adjgRefNbr, Equal<Required<APAdjust.adjgRefNbr>>, And<APAdjust.released, Equal<PX.Data.True>>>>>.Config>.Select((PXGraph) this, (object) row.DocType, (object) row.RefNbr))
      {
        APAdjust apAdjust = (APAdjust) pxResult;
        AP1099Year ap1099Year = (AP1099Year) PXSelectBase<AP1099Year, PXSelect<AP1099Year, Where<AP1099Year.finYear, Equal<Required<AP1099Year.finYear>>, And<AP1099Year.organizationID, Equal<Required<AP1099Year.organizationID>>>>>.Config>.Select((PXGraph) this, (object) apAdjust.AdjgDocDate.Value.Year.ToString(), (object) PXAccess.GetParentOrganizationID(apAdjust.AdjgBranchID));
        if (ap1099Year != null && ap1099Year.Status != "N")
          flag8 = false;
      }
      PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
      cache.AllowDelete = false;
      cache.AllowUpdate = flag8 | flag5;
      this.Transactions.Cache.AllowDelete = false;
      this.Transactions.Cache.AllowUpdate = flag8 | flag5;
      this.Transactions.Cache.AllowInsert = false;
      this.Remittance_Address.Cache.AllowUpdate = false;
      this.Remittance_Contact.Cache.AllowUpdate = false;
      if (flag8)
      {
        PXUIFieldAttribute.SetEnabled(this.Transactions.Cache, (string) null, false);
        PXUIFieldAttribute.SetEnabled<APTran.box1099>(this.Transactions.Cache, (object) null, true);
      }
      if (flag5)
      {
        PXUIFieldAttribute.SetEnabled(this.Transactions.Cache, (string) null, false);
        PXUIFieldAttribute.SetEnabled<APTran.accountID>(this.Transactions.Cache, (object) null, true);
        PXUIFieldAttribute.SetEnabled<APTran.subID>(this.Transactions.Cache, (object) null, true);
        PXUIFieldAttribute.SetEnabled<APTran.branchID>(this.Transactions.Cache, (object) null, true);
      }
      this.Taxes.Cache.AllowUpdate = false;
    }
label_41:
    bool isEnabled2 = !flag3 && (row.DocType == "PPM" || row.DocType == "QCK");
    PXUIFieldAttribute.SetEnabled<APQuickCheck.cashAccountID>(cache, (object) row, !flag3 && !flag6 && row.DocType != "VQC");
    PXUIFieldAttribute.SetEnabled<APQuickCheck.paymentMethodID>(cache, (object) row, !flag3 && !flag6 && row.DocType != "VQC");
    PXUIFieldAttribute.SetEnabled<APQuickCheck.cleared>(cache, (object) row, isEnabled1);
    PXCache cache2 = cache;
    APQuickCheck data1 = row;
    int num6;
    if (isEnabled1)
    {
      nullable1 = row.Cleared;
      num6 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num6 = 0;
    PXUIFieldAttribute.SetEnabled<APQuickCheck.clearDate>(cache2, (object) data1, num6 != 0);
    PXUIFieldAttribute.SetEnabled<APQuickCheck.docType>(cache, (object) row);
    PXUIFieldAttribute.SetEnabled<APQuickCheck.refNbr>(cache, (object) row);
    PXUIFieldAttribute.SetEnabled<APQuickCheck.batchNbr>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<APQuickCheck.aPAccountID>(cache, (object) row, isEnabled2);
    PXUIFieldAttribute.SetEnabled<APQuickCheck.aPSubID>(cache, (object) row, isEnabled2);
    PXUIFieldAttribute.SetEnabled<APQuickCheck.curyDocBal>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<APQuickCheck.curyLineTotal>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<APQuickCheck.curyTaxTotal>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<APQuickCheck.curyOrigWhTaxAmt>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<APQuickCheck.printed>(cache, (object) row, row.DocType != "VQC");
    PXUIFieldAttribute.SetEnabled<APQuickCheck.curyVatExemptTotal>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<APQuickCheck.curyVatTaxableTotal>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<APQuickCheck.adjDate>(cache, (object) row, !flag3 && !flag6);
    PXUIFieldAttribute.SetEnabled<APQuickCheck.adjFinPeriodID>(cache, (object) row, !flag3 && !flag6);
    PXUIFieldAttribute.SetEnabled<APQuickCheck.vendorLocationID>(cache, (object) row, !flag3 && !flag6 && row.DocType != "VQC");
    PXUIFieldAttribute.SetEnabled<APQuickCheck.curyOrigDocAmt>(cache, (object) row, !flag3 && !flag6 && row.DocType != "VQC");
    PXUIFieldAttribute.SetEnabled<APQuickCheck.curyOrigDiscAmt>(cache, (object) row, !flag3 && !flag6 && row.DocType != "VQC");
    PXUIFieldAttribute.SetEnabled<APQuickCheck.branchID>(cache, (object) row, !flag3 && !flag6 && row.DocType != "VQC");
    PXUIFieldAttribute.SetEnabled<APQuickCheck.taxZoneID>(cache, (object) row, !flag3 && !flag6 && row.DocType != "VQC");
    PXUIFieldAttribute.SetEnabled<APQuickCheck.termsID>(cache, (object) row, !flag3 && !flag6 && row.DocType != "VQC");
    PXAction<APQuickCheck> voidCheck = this.voidCheck;
    nullable1 = row.Released;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = row.Prebooked;
      if (!nullable1.GetValueOrDefault())
        goto label_48;
    }
    nullable1 = row.Voided;
    bool flag9 = false;
    int num7;
    if (nullable1.GetValueOrDefault() == flag9 & nullable1.HasValue)
    {
      num7 = row.DocType == "QCK" ? 1 : 0;
      goto label_49;
    }
label_48:
    num7 = 0;
label_49:
    voidCheck.SetEnabled(num7 != 0);
    PXAction<APQuickCheck> cashReturn = this.cashReturn;
    nullable1 = row.Released;
    int num8;
    if (nullable1.GetValueOrDefault())
    {
      nullable1 = row.Voided;
      bool flag10 = false;
      if (nullable1.GetValueOrDefault() == flag10 & nullable1.HasValue)
      {
        num8 = row.DocType == "QCK" ? 1 : 0;
        goto label_53;
      }
    }
    num8 = 0;
label_53:
    cashReturn.SetEnabled(num8 != 0);
    PXAction<APQuickCheck> printApPayment = this.printAPPayment;
    int num9;
    if (!(row.DocType != "RQC"))
    {
      if (row.DocType == "RQC")
      {
        nullable1 = row.Released;
        num9 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
      else
        num9 = 0;
    }
    else
      num9 = 1;
    printApPayment.SetEnabled(num9 != 0);
    nullable2 = row.VendorID;
    if (nullable2.HasValue && this.Transactions.Any<APTran>())
      PXUIFieldAttribute.SetEnabled<APQuickCheck.vendorID>(cache, (object) row, false);
    PXCache cache3 = this.Transactions.Cache;
    Vendor current1 = this.vendor.Current;
    int num10;
    if (current1 == null)
    {
      num10 = 0;
    }
    else
    {
      nullable1 = current1.Vendor1099;
      num10 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    PXUIFieldAttribute.SetVisible<APTran.box1099>(cache3, (object) null, num10 != 0);
    Vendor current2 = this.vendor.Current;
    int num11;
    if (current2 == null)
    {
      num11 = 1;
    }
    else
    {
      nullable1 = current2.Vendor1099;
      num11 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    if (num11 != 0)
      PXUIFieldAttribute.SetEnabled<APTran.box1099>(this.Transactions.Cache, (object) null, false);
    this.validateAddresses.SetEnabled(!flag3 && this.FindAllImplementations<IAddressValidationHelper>().RequiresValidation());
    PXCache cache4 = cache;
    APQuickCheck data2 = row;
    nullable1 = row.Prebooked;
    int num12 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<APQuickCheck.prebookBatchNbr>(cache4, (object) data2, num12 != 0);
    PXUIFieldAttribute.SetVisible<APQuickCheck.voidBatchNbr>(cache, (object) row, false);
    PXUIFieldAttribute.SetVisible<APTran.defScheduleID>(this.Transactions.Cache, (object) null, false);
    PXUIFieldAttribute.SetVisibility<APTran.defScheduleID>(this.Transactions.Cache, (object) null, PXUIVisibility.Invisible);
    PXCache cache5 = this.PaymentCharges.Cache;
    int num13;
    if (row.DocType == "QCK" || row.DocType == "VQC" || row.DocType == "RQC")
    {
      nullable1 = row.Released;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = row.Prebooked;
        num13 = !nullable1.GetValueOrDefault() ? 1 : 0;
        goto label_72;
      }
    }
    num13 = 0;
label_72:
    cache5.AllowInsert = num13 != 0;
    PXCache cache6 = this.PaymentCharges.Cache;
    int num14;
    if (row.DocType == "QCK" || row.DocType == "VQC" || row.DocType == "RQC")
    {
      nullable1 = row.Released;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = row.Prebooked;
        num14 = !nullable1.GetValueOrDefault() ? 1 : 0;
        goto label_76;
      }
    }
    num14 = 0;
label_76:
    cache6.AllowUpdate = num14 != 0;
    PXCache cache7 = this.PaymentCharges.Cache;
    int num15;
    if (row.DocType == "QCK" || row.DocType == "VQC" || row.DocType == "RQC")
    {
      nullable1 = row.Released;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = row.Prebooked;
        num15 = !nullable1.GetValueOrDefault() ? 1 : 0;
        goto label_80;
      }
    }
    num15 = 0;
label_80:
    cache7.AllowDelete = num15 != 0;
    this.Taxes.Cache.AllowDelete = this.Transactions.Cache.AllowDelete;
    this.Taxes.Cache.AllowInsert = this.Transactions.Cache.AllowInsert;
    PXCache cache8 = cache;
    APQuickCheck data3 = row;
    int num16;
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>())
    {
      nullable1 = this.apsetup.Current.RequireControlTaxTotal;
      num16 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num16 = 0;
    PXUIFieldAttribute.SetVisible<APQuickCheck.curyTaxAmt>(cache8, (object) data3, num16 != 0);
    Decimal? curyRoundDiff = row.CuryRoundDiff;
    Decimal num17 = 0M;
    bool isVisible2 = !(curyRoundDiff.GetValueOrDefault() == num17 & curyRoundDiff.HasValue) || PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.invoiceRounding>();
    PXUIFieldAttribute.SetVisible<APRegister.curyRoundDiff>(cache, (object) row, isVisible2);
    this.viewSchedule.SetEnabled(true);
    if (this.UseTaxes.Select().Count != 0)
      cache.RaiseExceptionHandling<APQuickCheck.curyTaxTotal>((object) row, (object) row.CuryTaxTotal, (Exception) new PXSetPropertyException("Use Tax is excluded from Tax Total.", PXErrorLevel.Warning));
    PXCache cache9 = cache;
    APQuickCheck data4 = row;
    PX.Objects.CA.PaymentMethod current3 = this.paymenttype.Current;
    int num18;
    if (current3 == null)
    {
      num18 = 0;
    }
    else
    {
      nullable1 = current3.PrintOrExport;
      num18 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    int num19 = num18 == 0 ? 0 : (!(row.DocType != "VQC") || !(row.DocType != "ADR") || !(row.DocType != "PPM") || !(row.DocType != "REF") || !(row.DocType != "RQC") ? (this.paymenttype.Current == null ? 1 : 0) : 1);
    PXUIFieldAttribute.SetVisible<APQuickCheck.printCheck>(cache9, (object) data4, num19 != 0);
    PXCache cache10 = cache;
    APQuickCheck data5 = row;
    nullable1 = row.Released;
    int num20;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = row.PrintCheck;
      num20 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num20 = 0;
    PXUIFieldAttribute.SetEnabled<APQuickCheck.extRefNbr>(cache10, (object) data5, num20 != 0);
    if (!this._IsVoidCheckInProgress && !this._IsCashReturnInProgress)
    {
      PXCache cache11 = this.Transactions.Cache;
      nullable1 = this.Document.Current.Released;
      int isTaxCalc = nullable1.GetValueOrDefault() ? 0 : 1;
      TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(cache11, (object) null, (TaxCalc) isTaxCalc);
    }
    nullable1 = row.IsMigratedRecord;
    bool valueOrDefault = nullable1.GetValueOrDefault();
    int num21;
    if (valueOrDefault)
    {
      nullable1 = row.Released;
      num21 = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num21 = 0;
    bool flag11 = num21 != 0;
    if (valueOrDefault)
    {
      cache.SetValue<APQuickCheck.printCheck>((object) row, (object) false);
      PXUIFieldAttribute.SetEnabled<APQuickCheck.printCheck>(cache, (object) row, false);
    }
    if (flag11)
      this.PaymentCharges.Cache.AllowSelect = false;
    APSetup current4 = this.apsetup.Current;
    int num22;
    if (current4 == null)
    {
      num22 = 0;
    }
    else
    {
      nullable1 = current4.MigrationMode;
      num22 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    if ((num22 != 0 ? (!valueOrDefault ? 1 : 0) : (flag11 ? 1 : 0)) != 0)
    {
      bool allowInsert = this.Document.Cache.AllowInsert;
      bool allowDelete = this.Document.Cache.AllowDelete;
      this.DisableCaches();
      this.Document.Cache.AllowInsert = allowInsert;
      this.Document.Cache.AllowDelete = allowDelete;
    }
    if (this.IsApprovalRequired(row))
    {
      if (row.DocType == "QCK")
      {
        if (!(row.Status == "E") && !(row.Status == "R") && !(row.Status == "C") && !(row.Status == "P") && !(row.Status == "V"))
        {
          if (row.Status == "G")
          {
            nullable1 = row.DontApprove;
            if (!nullable1.GetValueOrDefault())
            {
              nullable1 = row.Approved;
              if (!nullable1.GetValueOrDefault())
                goto label_112;
            }
            else
              goto label_112;
          }
          else
            goto label_112;
        }
        PXUIFieldAttribute.SetEnabled(cache, (object) row, false);
        this.Transactions.Cache.AllowInsert = false;
        this.Taxes.Cache.AllowInsert = false;
        this.Approval.Cache.AllowInsert = false;
        this.PaymentCharges.Cache.AllowInsert = false;
        this.Transactions.Cache.AllowUpdate = false;
        this.Taxes.Cache.AllowUpdate = false;
        this.Approval.Cache.AllowUpdate = false;
        this.PaymentCharges.Cache.AllowUpdate = false;
      }
label_112:
      nullable1 = row.Released;
      if (!nullable1.GetValueOrDefault())
        cache.AllowDelete = true;
    }
    if (row.Status == "E" || row.Status == "R" || row.Status == "B" || row.Status == "G" || row.Status == "H")
      PXUIFieldAttribute.SetEnabled<APQuickCheck.hold>(cache, (object) row, true);
    else
      PXUIFieldAttribute.SetEnabled<APQuickCheck.hold>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<APQuickCheck.docType>(cache, (object) row, true);
    PXUIFieldAttribute.SetEnabled<APQuickCheck.refNbr>(cache, (object) row, true);
    if (row.DocType == "VQC")
      PXUIFieldAttribute.SetEnabled<APQuickCheck.extRefNbr>(cache, (object) row, false);
    if (row.DocType == "RQC")
      PXUIFieldAttribute.SetEnabled<APQuickCheck.extRefNbr>(cache, (object) row, true);
    if (row.Status == "P")
    {
      PXUIFieldAttribute.SetEnabled<APQuickCheck.extRefNbr>(cache, (object) row, true);
      PXUIFieldAttribute.SetEnabled<APQuickCheck.docDesc>(cache, (object) row, true);
      this.Remittance_Address.Cache.AllowUpdate = false;
      this.Remittance_Contact.Cache.AllowUpdate = false;
    }
    int num23;
    if (row.DocType == "RQC")
    {
      nullable1 = row.DepositAsBatch;
      num23 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num23 = 0;
    bool flag12 = num23 != 0;
    PXDefaultAttribute.SetPersistingCheck<APQuickCheck.depositAfter>(cache, (object) row, flag12 ? PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing);
    PXUIFieldAttribute.SetVisible<APQuickCheck.depositAfter>(cache, (object) row, flag12);
    PXUIFieldAttribute.SetRequired<APQuickCheck.depositAfter>(cache, flag12);
    PXUIFieldAttribute.SetEnabled<APQuickCheck.depositAfter>(cache, (object) row, flag12);
    int num24 = string.IsNullOrEmpty(row.DepositNbr) ? 0 : (!string.IsNullOrEmpty(row.DepositType) ? 1 : 0);
    int num25;
    if (PXSelectorAttribute.Select<APQuickCheck.cashAccountID>(cache, (object) row) is PX.Objects.CA.CashAccount cashAccount)
    {
      nullable1 = cashAccount.ClearingAccount;
      num25 = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num25 = 0;
    bool flag13 = num25 != 0;
    bool isEnabled3 = ((num24 != 0 ? 0 : (cashAccount != null ? 1 : 0)) & (flag13 ? 1 : 0)) != 0;
    if (isEnabled3)
    {
      nullable1 = row.DepositAsBatch;
      bool flag14 = flag13;
      PXSetPropertyException propertyException = !(nullable1.GetValueOrDefault() == flag14 & nullable1.HasValue) ? new PXSetPropertyException("'Batch deposit' setting does not match 'Clearing Account' flag of the Cash Account", PXErrorLevel.Warning) : (PXSetPropertyException) null;
      cache.RaiseExceptionHandling<APQuickCheck.depositAsBatch>((object) row, (object) row.DepositAsBatch, (Exception) propertyException);
    }
    PXUIFieldAttribute.SetEnabled<APQuickCheck.depositAsBatch>(cache, (object) row, isEnabled3);
    PXUIFieldAttribute.SetEnabled<APQuickCheck.deposited>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<APQuickCheck.depositType>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<APQuickCheck.depositNbr>(cache, (object) null, false);
  }

  protected virtual void APQuickCheck_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    APQuickCheck row = e.Row as APQuickCheck;
    if (row.Released.GetValueOrDefault())
      return;
    bool? nullable1 = row.Prebooked;
    if (nullable1.GetValueOrDefault())
      return;
    row.DocDate = row.AdjDate;
    row.FinPeriodID = row.AdjFinPeriodID;
    row.TranPeriodID = row.AdjTranPeriodID;
    sender.RaiseExceptionHandling<APQuickCheck.finPeriodID>((object) row, (object) row.FinPeriodID, (Exception) null);
    this.PaymentCharges.UpdateChangesFromPayment(sender, e);
    Decimal? nullable2;
    Decimal? nullable3;
    Decimal? nullable4;
    if (!sender.ObjectsEqual<APQuickCheck.curyDocBal, APQuickCheck.curyOrigDiscAmt, APQuickCheck.curyOrigWhTaxAmt>(e.Row, e.OldRow))
    {
      Decimal? curyDocBal = row.CuryDocBal;
      Decimal? curyOrigDiscAmt1 = row.CuryOrigDiscAmt;
      Decimal? nullable5 = curyDocBal.HasValue & curyOrigDiscAmt1.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() - curyOrigDiscAmt1.GetValueOrDefault()) : new Decimal?();
      nullable2 = row.CuryOrigWhTaxAmt;
      nullable3 = nullable5.HasValue & nullable2.HasValue ? new Decimal?(nullable5.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      nullable4 = row.CuryOrigDocAmt;
      if (!(nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue) && !APPaymentEntry.IsCheckReallyPrinted((IPrintCheckControlable) row))
      {
        nullable4 = row.CuryDocBal;
        if (nullable4.HasValue)
        {
          nullable4 = row.CuryOrigDiscAmt;
          if (nullable4.HasValue)
          {
            nullable4 = row.CuryOrigWhTaxAmt;
            if (nullable4.HasValue)
            {
              nullable4 = row.CuryDocBal;
              Decimal num = 0M;
              if (!(nullable4.GetValueOrDefault() == num & nullable4.HasValue))
              {
                PXCache pxCache = sender;
                APQuickCheck data = row;
                nullable2 = row.CuryDocBal;
                Decimal? curyOrigDiscAmt2 = row.CuryOrigDiscAmt;
                nullable4 = nullable2.HasValue & curyOrigDiscAmt2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - curyOrigDiscAmt2.GetValueOrDefault()) : new Decimal?();
                nullable3 = row.CuryOrigWhTaxAmt;
                // ISSUE: variable of a boxed type
                __Boxed<Decimal?> local = (ValueType) (nullable4.HasValue & nullable3.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?());
                pxCache.SetValueExt<APQuickCheck.curyOrigDocAmt>((object) data, (object) local);
                goto label_20;
              }
            }
          }
        }
        sender.SetValueExt<APQuickCheck.curyOrigDocAmt>((object) row, (object) 0M);
        goto label_20;
      }
    }
    if (!sender.ObjectsEqual<APQuickCheck.curyOrigDocAmt>(e.Row, e.OldRow))
    {
      nullable3 = row.CuryDocBal;
      if (nullable3.HasValue)
      {
        nullable3 = row.CuryOrigDocAmt;
        if (nullable3.HasValue)
        {
          nullable3 = row.CuryOrigWhTaxAmt;
          if (nullable3.HasValue)
          {
            nullable3 = row.CuryDocBal;
            Decimal num = 0M;
            if (!(nullable3.GetValueOrDefault() == num & nullable3.HasValue))
            {
              PXCache pxCache = sender;
              APQuickCheck data = row;
              Decimal? curyDocBal = row.CuryDocBal;
              nullable2 = row.CuryOrigDocAmt;
              nullable3 = curyDocBal.HasValue & nullable2.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
              nullable4 = row.CuryOrigWhTaxAmt;
              Decimal? nullable6;
              if (!(nullable3.HasValue & nullable4.HasValue))
              {
                nullable2 = new Decimal?();
                nullable6 = nullable2;
              }
              else
                nullable6 = new Decimal?(nullable3.GetValueOrDefault() - nullable4.GetValueOrDefault());
              // ISSUE: variable of a boxed type
              __Boxed<Decimal?> local = (ValueType) nullable6;
              pxCache.SetValueExt<APQuickCheck.curyOrigDiscAmt>((object) data, (object) local);
              goto label_20;
            }
          }
        }
      }
      sender.SetValueExt<APQuickCheck.curyOrigDiscAmt>((object) row, (object) 0M);
    }
label_20:
    nullable1 = row.Hold;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = row.Released;
      if (!nullable1.GetValueOrDefault())
      {
        nullable1 = row.Prebooked;
        if (!nullable1.GetValueOrDefault())
        {
          nullable4 = row.CuryOrigDocAmt;
          Decimal valueOrDefault1 = nullable4.GetValueOrDefault();
          nullable4 = row.CuryOrigDiscAmt;
          Decimal valueOrDefault2 = nullable4.GetValueOrDefault();
          Decimal num1 = valueOrDefault1 + valueOrDefault2;
          nullable4 = row.CuryOrigWhTaxAmt;
          Decimal valueOrDefault3 = nullable4.GetValueOrDefault();
          Decimal num2 = num1 + valueOrDefault3;
          if (APPaymentEntry.IsCheckReallyPrinted((IPrintCheckControlable) row))
          {
            nullable4 = row.CuryDocBal;
            Decimal num3 = num2;
            if (!(nullable4.GetValueOrDefault() == num3 & nullable4.HasValue))
            {
              sender.RaiseExceptionHandling<APQuickCheck.curyOrigDocAmt>((object) row, (object) row.CuryOrigDocAmt, (Exception) new PXSetPropertyException("The printed quick check is out of the balance."));
              goto label_33;
            }
          }
          nullable4 = row.CuryDocBal;
          Decimal num4 = num2;
          if (nullable4.GetValueOrDefault() < num4 & nullable4.HasValue)
            sender.RaiseExceptionHandling<APQuickCheck.curyOrigDocAmt>((object) row, (object) row.CuryOrigDocAmt, (Exception) new PXSetPropertyException("The document is out of the balance."));
          else if (num2 < 0M)
          {
            sender.RaiseExceptionHandling<APQuickCheck.curyOrigDocAmt>((object) row, (object) row.CuryOrigDocAmt, (Exception) new PXSetPropertyException("Document balance will become negative. The document will not be released."));
          }
          else
          {
            nullable4 = row.CuryOrigDiscAmt;
            if (nullable4.GetValueOrDefault() < 0M)
              sender.RaiseExceptionHandling<APQuickCheck.curyOrigDiscAmt>((object) row, (object) row.CuryOrigDiscAmt, (Exception) new PXSetPropertyException("The document is out of the balance."));
            else
              sender.RaiseExceptionHandling<APQuickCheck.curyOrigDocAmt>((object) row, (object) row.CuryOrigDocAmt, (Exception) null);
          }
        }
      }
    }
label_33:
    nullable1 = this.apsetup.Current.RequireControlTaxTotal;
    bool flag = nullable1.GetValueOrDefault() && PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>();
    nullable1 = row.Hold;
    if (nullable1.GetValueOrDefault())
    {
      nullable1 = row.Printed;
      if (nullable1.GetValueOrDefault())
        goto label_46;
    }
    nullable1 = row.Released;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = row.Prebooked;
      if (!nullable1.GetValueOrDefault())
      {
        nullable4 = row.CuryTaxTotal;
        nullable3 = row.CuryTaxAmt;
        if (!(nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue) & flag)
          sender.RaiseExceptionHandling<APQuickCheck.curyTaxAmt>((object) row, (object) row.CuryTaxAmt, (Exception) new PXSetPropertyException("Tax Amount must be equal to Tax Total."));
        else if (flag)
        {
          sender.RaiseExceptionHandling<APQuickCheck.curyTaxAmt>((object) row, (object) null, (Exception) null);
        }
        else
        {
          PXCache pxCache = sender;
          APQuickCheck data = row;
          nullable3 = row.CuryTaxTotal;
          Decimal? nullable7;
          if (nullable3.HasValue)
          {
            nullable3 = row.CuryTaxTotal;
            Decimal num = 0M;
            if (!(nullable3.GetValueOrDefault() == num & nullable3.HasValue))
            {
              nullable7 = row.CuryTaxTotal;
              goto label_45;
            }
          }
          nullable7 = new Decimal?(0M);
label_45:
          // ISSUE: variable of a boxed type
          __Boxed<Decimal?> local = (ValueType) nullable7;
          pxCache.SetValueExt<APQuickCheck.curyTaxAmt>((object) data, (object) local);
        }
      }
    }
label_46:
    sender.RaiseExceptionHandling<APRegister.curyRoundDiff>((object) row, (object) null, (Exception) null);
    nullable1 = row.Hold;
    if (nullable1.GetValueOrDefault())
      return;
    nullable3 = row.RoundDiff;
    Decimal num5 = 0M;
    if (nullable3.GetValueOrDefault() == num5 & nullable3.HasValue)
      return;
    if (!flag)
    {
      if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.invoiceRounding>())
      {
        nullable3 = row.TaxRoundDiff;
        Decimal num6 = 0M;
        if (nullable3.GetValueOrDefault() == num6 & nullable3.HasValue)
          goto label_51;
      }
      if (!PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.netGrossEntryMode>())
      {
        sender.RaiseExceptionHandling<APRegister.curyRoundDiff>((object) row, (object) row.CuryRoundDiff, (Exception) new PXSetPropertyException("Tax Amount cannot be edited because the Net/Gross Entry Mode feature is not enabled."));
        return;
      }
      sender.RaiseExceptionHandling<APRegister.curyRoundDiff>((object) row, (object) row.CuryRoundDiff, (Exception) new PXSetPropertyException("Tax Amount cannot be edited because \"Validate Tax Totals on Entry\" is not selected on the AP Preferences form."));
      return;
    }
label_51:
    nullable3 = row.RoundDiff;
    Decimal num7 = System.Math.Abs(nullable3.Value);
    nullable3 = CurrencyCollection.GetCurrency(this.currencyinfo.Current.BaseCuryID).RoundingLimit;
    Decimal num8 = System.Math.Abs(nullable3.Value);
    if (!(num7 > num8))
      return;
    sender.RaiseExceptionHandling<APRegister.curyRoundDiff>((object) row, (object) row.CuryRoundDiff, (Exception) new PXSetPropertyException("The amount to be posted to the rounding account ({1} {0}) exceeds the limit ({2} {0}) specified on the Currencies (CM202000) form.", new object[3]
    {
      (object) this.currencyinfo.Current.BaseCuryID,
      (object) PXDBQuantityAttribute.Round(row.RoundDiff),
      (object) PXDBQuantityAttribute.Round(CurrencyCollection.GetCurrency(this.currencyinfo.Current.BaseCuryID).RoundingLimit)
    }));
  }

  protected virtual void _(PX.Data.Events.RowDeleting<APQuickCheck> e)
  {
    if (e.Row.OrigModule == "EP" && e.Row.OrigDocType == "ECD")
    {
      if (e.Row.DocType == "VQC")
      {
        bool? released = e.Row.Released;
        bool flag = false;
        if (released.GetValueOrDefault() == flag & released.HasValue)
          return;
      }
      throw new PXException("AP document created as a result of expense claim release cannot be deleted.");
    }
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXLineNbr(typeof (APQuickCheck.chargeCntr), DecrementOnDelete = false)]
  public virtual void APPaymentChargeTran_LineNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXDBDefault(typeof (APQuickCheck.cashAccountID))]
  public virtual void APPaymentChargeTran_CashAccountID_CacheAttached(PXCache sender)
  {
  }

  /// <summary>
  /// <see cref="P:PX.Objects.AP.APPaymentChargeTran.EntryTypeID" /> cache attached event.
  /// </summary>
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXSelector(typeof (Search2<CAEntryType.entryTypeId, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>, Where<CashAccountETDetail.cashAccountID, Equal<Current<APQuickCheck.cashAccountID>>, And<CAEntryType.drCr, Equal<CADrCr.cACredit>>>>))]
  public virtual void APPaymentChargeTran_EntryTypeID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXDBDefault(typeof (APQuickCheck.adjDate))]
  public virtual void APPaymentChargeTran_TranDate_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [FinPeriodID(null, typeof (APPaymentChargeTran.cashAccountID), typeof (Selector<APPaymentChargeTran.cashAccountID, PX.Objects.CA.CashAccount.branchID>), null, null, null, true, false, null, typeof (APPaymentChargeTran.tranPeriodID), typeof (APQuickCheck.adjTranPeriodID), true, true)]
  public virtual void APPaymentChargeTran_FinPeriodID_CacheAttached(PXCache sender)
  {
  }

  [PXDBDate]
  [PXDefault(typeof (APQuickCheck.docDate), PersistingCheck = PXPersistingCheck.Nothing)]
  protected virtual void EPApproval_DocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDefault(typeof (APQuickCheck.vendorID), PersistingCheck = PXPersistingCheck.Nothing)]
  protected virtual void EPApproval_BAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault(typeof (APQuickCheck.docDesc), PersistingCheck = PXPersistingCheck.Nothing)]
  protected virtual void EPApproval_Descr_CacheAttached(PXCache sender)
  {
  }

  [PXDBLong]
  [PX.Objects.CM.Extensions.CurrencyInfo(typeof (APQuickCheck.curyInfoID))]
  protected virtual void EPApproval_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDBDecimal]
  [PXDefault(typeof (APQuickCheck.curyOrigDocAmt), PersistingCheck = PXPersistingCheck.Nothing)]
  protected virtual void EPApproval_CuryTotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXDBDecimal]
  [PXDefault(typeof (APQuickCheck.origDocAmt), PersistingCheck = PXPersistingCheck.Nothing)]
  protected virtual void EPApproval_TotalAmount_CacheAttached(PXCache sender)
  {
  }

  protected virtual void EPApproval_SourceItemType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.Document.Current == null)
      return;
    e.NewValue = (object) new APDocType.ListAttribute().ValueLabelDic[this.Document.Current.DocType];
    e.Cancel = true;
  }

  protected virtual void EPApproval_Details_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.Document.Current == null)
      return;
    e.NewValue = (object) EPApprovalHelper.BuildEPApprovalDetailsString(sender, (IApprovalDescription) this.Document.Current);
  }

  protected virtual void APTran_AccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    APTran row = (APTran) e.Row;
    if (this.vendor.Current == null || row == null)
      return;
    APSetup current1 = this.apsetup.Current;
    int? nullable;
    if ((current1 != null ? (!current1.MigrationMode.GetValueOrDefault() ? 1 : 0) : 1) != 0)
    {
      nullable = row.InventoryID;
      if (nullable.HasValue)
      {
        PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) this.nonStockItem.Select((object) row.InventoryID);
        if ((inventoryItem != null ? (inventoryItem.StkItem.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        {
          e.NewValue = (object) null;
          e.Cancel = true;
          return;
        }
      }
    }
    nullable = row.InventoryID;
    if (!nullable.HasValue && (this.vendor.Current.Type == "VE" || this.vendor.Current.Type == "VC"))
    {
      PX.Objects.CR.Location current2 = this.location.Current;
      int num;
      if (current2 == null)
      {
        num = 0;
      }
      else
      {
        nullable = current2.VExpenseAcctID;
        num = nullable.HasValue ? 1 : 0;
      }
      if (num != 0)
        goto label_12;
    }
    nullable = row.InventoryID;
    if (!nullable.HasValue || !this.vendor.Current.IsBranch.GetValueOrDefault() || row.AccrueCost.GetValueOrDefault() || !(this.apsetup.Current?.IntercompanyExpenseAccountDefault == "L"))
    {
      nullable = row.InventoryID;
      if (nullable.HasValue || !(this.vendor.Current.Type == "EP"))
        return;
      PX.Objects.EP.EPEmployee epEmployee = (PX.Objects.EP.EPEmployee) this.EmployeeByVendor.Select();
      PXFieldDefaultingEventArgs defaultingEventArgs = e;
      nullable = epEmployee.ExpenseAcctID;
      object obj = (object) nullable ?? e.NewValue;
      defaultingEventArgs.NewValue = obj;
      e.Cancel = true;
      return;
    }
label_12:
    e.NewValue = (object) this.location.Current.VExpenseAcctID;
    e.Cancel = true;
  }

  protected virtual void APTran_AccountID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (this.vendor.Current != null && this.vendor.Current.Vendor1099.Value)
      sender.SetDefaultExt<APTran.box1099>(e.Row);
    if (!(e.Row is APTran row))
      return;
    int? projectId = row.ProjectID;
    if (projectId.HasValue)
    {
      projectId = row.ProjectID;
      int? nullable = ProjectDefaultAttribute.NonProject();
      if (!(projectId.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId.HasValue == nullable.HasValue))
        return;
    }
    sender.SetDefaultExt<APTran.projectID>(e.Row);
  }

  protected virtual void APTran_SubID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    APTran row = (APTran) e.Row;
    if (row == null || this.vendor.Current == null || this.vendor.Current.Type == null || !string.IsNullOrEmpty(row.PONbr) || !string.IsNullOrEmpty(row.ReceiptNbr))
      return;
    PX.Objects.IN.InventoryItem data1 = (PX.Objects.IN.InventoryItem) this.nonStockItem.Select((object) row.InventoryID);
    PX.Objects.EP.EPEmployee data2 = (PX.Objects.EP.EPEmployee) PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.userID, Equal<Required<PX.Objects.EP.EPEmployee.userID>>>>.Config>.Select((PXGraph) this, (object) PXAccess.GetUserID());
    PX.Objects.CR.Standalone.Location data3 = (PX.Objects.CR.Standalone.Location) PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelectJoin<PX.Objects.CR.Standalone.Location, InnerJoin<BAccountR, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<PX.Objects.GL.Branch, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<APTran.branchID>>>>.Config>.Select((PXGraph) this, (object) row.BranchID);
    PX.Objects.CT.Contract contract = (PX.Objects.CT.Contract) PXSelectBase<PX.Objects.CT.Contract, PXSelect<PX.Objects.CT.Contract, Where<PX.Objects.CT.Contract.contractID, Equal<Required<PX.Objects.CT.Contract.contractID>>>>.Config>.Select((PXGraph) this, (object) row.ProjectID);
    string expenseSubMask = this.apsetup.Current.ExpenseSubMask;
    int? nullable1 = new int?();
    if (contract == null || contract.BaseType == "C")
    {
      contract = (PX.Objects.CT.Contract) PXSelectBase<PX.Objects.CT.Contract, PXSelect<PX.Objects.CT.Contract, Where<PX.Objects.CT.Contract.nonProject, Equal<PX.Data.True>>>.Config>.Select((PXGraph) this);
      expenseSubMask.Replace("T", "J");
    }
    else
    {
      PMTask pmTask = (PMTask) PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskID, Equal<Required<PMTask.taskID>>>>>.Config>.Select((PXGraph) this, (object) row.ProjectID, (object) row.TaskID);
      if (pmTask != null)
        nullable1 = pmTask.DefaultExpenseSubID;
    }
    int? nullable2 = new int?();
    switch (this.vendor.Current.Type)
    {
      case "VE":
      case "VC":
        PX.Objects.CR.Location current = this.location.Current;
        if ((current != null ? (current.VExpenseSubID.HasValue ? 1 : 0) : 0) != 0)
        {
          nullable2 = (int?) this.Caches[typeof (PX.Objects.CR.Location)].GetValue<PX.Objects.CR.Location.vExpenseSubID>((object) this.location.Current);
          break;
        }
        break;
      case "EP":
        nullable2 = (int?) this.EmployeeByVendor.SelectSingle()?.ExpenseSubID ?? nullable2;
        break;
    }
    int? nullable3 = (int?) this.Caches[typeof (PX.Objects.IN.InventoryItem)].GetValue<PX.Objects.IN.InventoryItem.cOGSSubID>((object) data1);
    int? nullable4 = (int?) this.Caches[typeof (PX.Objects.EP.EPEmployee)].GetValue<PX.Objects.EP.EPEmployee.expenseSubID>((object) data2);
    int? nullable5 = (int?) this.Caches[typeof (PX.Objects.CR.Standalone.Location)].GetValue<PX.Objects.CR.Standalone.Location.cMPExpenseSubID>((object) data3);
    int? defaultExpenseSubId = contract.DefaultExpenseSubID;
    object newValue = (object) SubAccountMaskAttribute.MakeSub<APSetup.expenseSubMask>((PXGraph) this, this.apsetup.Current.ExpenseSubMask, new object[6]
    {
      (object) nullable2,
      (object) nullable3,
      (object) nullable4,
      (object) nullable5,
      (object) defaultExpenseSubId,
      (object) nullable1
    }, new System.Type[6]
    {
      typeof (PX.Objects.CR.Location.vExpenseSubID),
      typeof (PX.Objects.IN.InventoryItem.cOGSSubID),
      typeof (PX.Objects.EP.EPEmployee.expenseSubID),
      typeof (PX.Objects.CR.Location.cMPExpenseSubID),
      typeof (PMProject.defaultExpenseSubID),
      typeof (PMTask.defaultExpenseSubID)
    });
    if (newValue != null)
      sender.RaiseFieldUpdating<APTran.subID>((object) row, ref newValue);
    else
      newValue = (object) row.SubID;
    e.NewValue = (object) (int?) newValue;
    e.Cancel = true;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Category", Visibility = PXUIVisibility.Visible)]
  [APQuickCheckTax(typeof (APQuickCheck), typeof (APTax), typeof (APTaxTran), typeof (APQuickCheck.taxCalcMode), typeof (APQuickCheck.branchID), Inventory = typeof (APTran.inventoryID), UOM = typeof (APTran.uOM), LineQty = typeof (APTran.qty))]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<PX.Data.True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.taxCategoryID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<APTran.inventoryID>>>>), PersistingCheck = PXPersistingCheck.Nothing)]
  protected virtual void APTran_TaxCategoryID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void APTran_TaxCategoryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    APTran row = (APTran) e.Row;
    if (row == null || row.InventoryID.HasValue || this.vendor == null || this.vendor.Current == null || this.vendor.Current.TaxAgency.GetValueOrDefault() || TaxBaseAttribute.GetTaxCalc<APTran.taxCategoryID>(sender, (object) row) != TaxCalc.Calc || this.taxzone.Current == null || string.IsNullOrEmpty(this.taxzone.Current.DfltTaxCategoryID))
      return;
    e.NewValue = (object) this.taxzone.Current.DfltTaxCategoryID;
    e.Cancel = true;
  }

  protected virtual void APTran_UnitCost_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    APTran row = (APTran) e.Row;
    if (row == null || row.InventoryID.HasValue)
      return;
    e.NewValue = (object) 0M;
    e.Cancel = true;
  }

  protected virtual void APTran_CuryUnitCost_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    APTran row1 = (APTran) e.Row;
    if (row1 == null)
      return;
    Decimal? nullable1;
    if (!PX.Objects.CM.PXCurrencyAttribute.IsNullOrEmpty(row1.UnitCost))
    {
      PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = this.GetExtension<APQuickCheckEntry.MultiCurrency>().GetDefaultCurrencyInfo();
      nullable1 = row1.UnitCost;
      Decimal baseval = nullable1.Value;
      Decimal num = defaultCurrencyInfo.CuryConvCury(baseval);
      e.NewValue = (object) INUnitAttribute.ConvertToBase<APTran.inventoryID>(sender, (object) row1, row1.UOM, num, INPrecision.UNITCOST);
      e.Cancel = true;
    }
    APQuickCheck current = this.Document.Current;
    if (current == null)
      return;
    int? nullable2 = current.VendorID;
    if (!nullable2.HasValue || row1 == null)
      return;
    nullable2 = row1.InventoryID;
    if (!nullable2.HasValue || row1.UOM == null)
      return;
    if (row1.ManualPrice.GetValueOrDefault())
    {
      nullable1 = row1.CuryUnitCost;
      if (nullable1.HasValue)
      {
        PXFieldDefaultingEventArgs defaultingEventArgs = e;
        nullable1 = row1.CuryUnitCost;
        // ISSUE: variable of a boxed type
        __Boxed<Decimal> valueOrDefault = (ValueType) nullable1.GetValueOrDefault();
        defaultingEventArgs.NewValue = (object) valueOrDefault;
        goto label_15;
      }
    }
    Decimal? nullable3 = new Decimal?();
    nullable2 = row1.InventoryID;
    if (nullable2.HasValue && row1.UOM != null)
    {
      System.DateTime date = this.Document.Current.DocDate.Value;
      nullable3 = APVendorPriceMaint.CalculateUnitCost(sender, row1.VendorID, current.VendorLocationID, row1.InventoryID, row1.SiteID, this.currencyinfo.SelectSingle().GetCM(), row1.UOM, row1.Qty, date, row1.CuryUnitCost);
      e.NewValue = (object) nullable3;
    }
    if (!nullable3.HasValue)
    {
      PXFieldDefaultingEventArgs defaultingEventArgs = e;
      PXGraph graph = sender.Graph;
      APTran row2 = row1;
      int? vendorId = current.VendorID;
      int? vendorLocationId = current.VendorLocationID;
      System.DateTime? docDate = current.DocDate;
      string curyId = current.CuryID;
      int? inventoryId = row1.InventoryID;
      nullable2 = new int?();
      int? subItemID = nullable2;
      nullable2 = new int?();
      int? siteID = nullable2;
      string uom = row1.UOM;
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> local = (ValueType) POItemCostManager.Fetch<APTran.inventoryID, APTran.curyInfoID>(graph, (object) row2, vendorId, vendorLocationId, docDate, curyId, inventoryId, subItemID, siteID, uom);
      defaultingEventArgs.NewValue = (object) local;
    }
    APVendorPriceMaint.CheckNewUnitCost<APTran, APTran.curyUnitCost>(sender, row1, e.NewValue);
label_15:
    e.Cancel = true;
  }

  protected virtual void APTran_ManualPrice_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is APTran))
      return;
    sender.SetDefaultExt<APTran.curyUnitCost>(e.Row);
  }

  protected virtual void APTran_Qty_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is APTran row))
      return;
    Decimal? qty = row.Qty;
    Decimal num = 0M;
    if (qty.GetValueOrDefault() == num & qty.HasValue)
      return;
    sender.SetDefaultExt<APTran.curyUnitCost>(e.Row);
  }

  protected virtual void APTran_UOM_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    APTran row = (APTran) e.Row;
    sender.SetDefaultExt<APTran.unitCost>((object) row);
    sender.SetDefaultExt<APTran.curyUnitCost>((object) row);
    sender.SetValue<APTran.unitCost>((object) row, (object) null);
  }

  protected virtual void APTran_InventoryID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is APTran row) || !string.IsNullOrEmpty(row.ReceiptNbr) || !string.IsNullOrEmpty(row.PONbr))
      return;
    sender.SetDefaultExt<APTran.accountID>((object) row);
    sender.SetDefaultExt<APTran.subID>((object) row);
    sender.SetDefaultExt<APTran.taxCategoryID>((object) row);
    sender.SetDefaultExt<APTran.deferredCode>((object) row);
    sender.SetDefaultExt<APTran.uOM>((object) row);
    sender.SetDefaultExt<APTran.unitCost>((object) row);
    sender.SetDefaultExt<APTran.curyUnitCost>((object) row);
    sender.SetValue<APTran.unitCost>((object) row, (object) null);
    sender.SetDefaultExt<APTran.costCodeID>((object) row);
    PX.Objects.IN.InventoryItem data = (PX.Objects.IN.InventoryItem) this.nonStockItem.Select((object) row.InventoryID);
    if (data == null)
      return;
    row.TranDesc = PXDBLocalizableStringAttribute.GetTranslation(this.Caches[typeof (PX.Objects.IN.InventoryItem)], (object) data, "Descr", this.vendor.Current?.LocaleName);
  }

  protected virtual void APTran_ProjectID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is APTran row))
      return;
    sender.SetDefaultExt<APTran.subID>((object) row);
  }

  [FinPeriodID(null, typeof (APTran.branchID), null, null, null, null, true, false, null, typeof (APTran.tranPeriodID), typeof (APQuickCheck.adjTranPeriodID), true, true)]
  protected virtual void APTran_FinPeriodID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noPurchases>>), "The inventory item is {0}.", new System.Type[] {typeof (PX.Objects.IN.InventoryItem.itemStatus)}, ShowWarning = true)]
  protected virtual void APTran_InventoryID_CacheAttached(PXCache sender)
  {
  }

  [PXBool]
  [DRTerms.Dates(typeof (APTran.dRTermStartDate), typeof (APTran.dRTermEndDate), typeof (APTran.inventoryID), typeof (APTran.deferredCode), typeof (APQuickCheck.hold))]
  protected virtual void APTran_RequiresTerms_CacheAttached(PXCache sender)
  {
  }

  protected virtual void APTran_TaskID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is APTran row))
      return;
    sender.SetDefaultExt<APTran.subID>((object) row);
    sender.SetDefaultExt<APTran.costCodeID>((object) row);
  }

  protected virtual void APTran_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is APTran row))
      return;
    APQuickCheck current1 = this.Document.Current;
    Vendor current2 = this.vendor.Current;
    bool? nullable;
    int num1;
    if ((current2 != null ? (current2.Vendor1099.GetValueOrDefault() ? 1 : 0) : 0) != 0 && current1 != null)
    {
      nullable = current1.Voided;
      num1 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag1 = num1 != 0;
    int num2;
    if (current1 == null)
    {
      num2 = 0;
    }
    else
    {
      nullable = current1.Released;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    bool flag2 = num2 != 0;
    PXUIFieldAttribute.SetEnabled<APTran.deferredCode>(cache, (object) row, true);
    if (!string.IsNullOrEmpty(row.PONbr) || !string.IsNullOrEmpty(row.ReceiptNbr))
    {
      PXUIFieldAttribute.SetEnabled<APTran.inventoryID>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<APTran.uOM>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<APTran.accountID>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<APTran.subID>(cache, (object) row, false);
    }
    bool flag3 = string.IsNullOrEmpty(row.PONbr);
    PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select(cache, e.Row, cache.GetField(typeof (APTran.inventoryID)));
    if (inventoryItem != null)
    {
      nullable = inventoryItem.StkItem;
      if (!nullable.GetValueOrDefault())
      {
        nullable = inventoryItem.NonStockReceipt;
        if (!nullable.GetValueOrDefault())
          flag3 = true;
      }
    }
    bool isEnabled = flag3 && (!flag2 || !flag1);
    PXUIFieldAttribute.SetEnabled<APTran.projectID>(cache, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<APTran.taskID>(cache, (object) row, isEnabled);
    nullable = row.IsDirectTaxLine;
    if (nullable.GetValueOrDefault())
    {
      PXUIFieldAttribute.SetEnabled<APTran.deferredCode>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<APTran.dRTermStartDate>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<APTran.dRTermEndDate>(cache, (object) row, false);
    }
    if (current1 == null)
      return;
    nullable = current1.IsMigratedRecord;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = current1.Released;
    if (nullable.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetEnabled<APTran.defScheduleID>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<APTran.deferredCode>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<APTran.dRTermStartDate>(cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<APTran.dRTermEndDate>(cache, (object) null, false);
  }

  protected virtual void APTran_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!sender.ObjectsEqual<APTran.box1099>(e.Row, e.OldRow))
    {
      foreach (PXResult<APAdjust> pxResult in PXSelectBase<APAdjust, PXSelect<APAdjust, Where<APAdjust.adjdDocType, Equal<Required<APAdjust.adjdDocType>>, And<APAdjust.adjdRefNbr, Equal<Required<APAdjust.adjdRefNbr>>, And<APAdjust.released, Equal<PX.Data.True>>>>>.Config>.Select((PXGraph) this, (object) ((APTran) e.Row).TranType, (object) ((APTran) e.Row).RefNbr))
      {
        APAdjust adj = (APAdjust) pxResult;
        APReleaseProcess.Update1099Hist((PXGraph) this, -1M, adj, (APTran) e.OldRow, (APRegister) this.Document.Current, (APRegister) this.Document.Current);
        APReleaseProcess.Update1099Hist((PXGraph) this, 1M, adj, (APTran) e.Row, (APRegister) this.Document.Current, (APRegister) this.Document.Current);
      }
      if (this.Document.Current.Released.GetValueOrDefault())
      {
        Decimal? origDocAmt = this.Document.Current.OrigDocAmt;
        Decimal num = 0M;
        if (origDocAmt.GetValueOrDefault() == num & origDocAmt.HasValue)
        {
          APReleaseProcess.Update1099Hist((PXGraph) this, -1M, (APTran) e.OldRow, (APRegister) this.Document.Current, this.Document.Current.DocDate, this.Document.Current.BranchID, new Decimal?(0M));
          APReleaseProcess.Update1099Hist((PXGraph) this, 1M, (APTran) e.Row, (APRegister) this.Document.Current, this.Document.Current.DocDate, this.Document.Current.BranchID, new Decimal?(0M));
        }
      }
    }
    if (!(e.Row is APTran row) || !e.ExternalCall && !sender.Graph.IsImport || !sender.ObjectsEqual<APTran.inventoryID>(e.Row, e.OldRow) || !sender.ObjectsEqual<APTran.uOM>(e.Row, e.OldRow) || !sender.ObjectsEqual<APTran.qty>(e.Row, e.OldRow) || !sender.ObjectsEqual<APTran.branchID>(e.Row, e.OldRow) || !sender.ObjectsEqual<APTran.siteID>(e.Row, e.OldRow) || !sender.ObjectsEqual<APTran.manualPrice>(e.Row, e.OldRow) || sender.ObjectsEqual<APTran.curyUnitCost>(e.Row, e.OldRow) && sender.ObjectsEqual<APTran.curyLineAmt>(e.Row, e.OldRow))
      return;
    row.ManualPrice = new bool?(true);
  }

  protected virtual void APTran_Box1099_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.vendor.Current != null)
    {
      bool? vendor1099 = this.vendor.Current.Vendor1099;
      bool flag = false;
      if (!(vendor1099.GetValueOrDefault() == flag & vendor1099.HasValue))
        return;
    }
    e.NewValue = (object) null;
    e.Cancel = true;
  }

  protected virtual void APTran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Row == null)
      return;
    ScheduleHelper.DeleteAssociatedScheduleIfDeferralCodeChanged((PXGraph) this, (IDocumentLine) (e.Row as APTran));
  }

  protected virtual void AP1099Hist_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (((AP1099History) e.Row).BoxNbr.HasValue)
      return;
    e.Cancel = true;
  }

  protected virtual void APTran_DrCr_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (this.Document.Current == null)
      return;
    e.NewValue = (object) APInvoiceType.DrCr(this.Document.Current.DocType);
    e.Cancel = true;
  }

  protected virtual void APTaxTran_TaxZoneID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.Document.Current == null)
      return;
    e.NewValue = (object) this.Document.Current.TaxZoneID;
    e.Cancel = true;
  }

  protected virtual void APTaxTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is APTaxTran))
      return;
    PXUIFieldAttribute.SetEnabled<APTaxTran.taxID>(sender, e.Row, sender.GetStatus(e.Row) == PXEntryStatus.Inserted);
  }

  protected virtual void APTaxTran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (this.Document.Current == null || e.Operation != PXDBOperation.Insert && e.Operation != PXDBOperation.Update)
      return;
    ((TaxTran) e.Row).TaxZoneID = this.Document.Current.TaxZoneID;
  }

  [Branch(typeof (APRegister.branchID), null, true, true, true, Enabled = false)]
  protected virtual void APTaxTran_BranchID_CacheAttached(PXCache sender)
  {
  }

  [FinPeriodID(null, typeof (APTaxTran.branchID), null, null, null, null, true, false, null, null, typeof (APQuickCheck.adjTranPeriodID), true, true)]
  [PXDefault]
  protected virtual void APTaxTran_FinPeriodID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<APTaxTran, APTaxTran.taxID> e)
  {
    APTaxTran row = e.Row;
    if (row == null || e.OldValue == null || e.OldValue == e.NewValue)
      return;
    this.Taxes.Cache.SetDefaultExt<APTaxTran.accountID>((object) row);
    this.Taxes.Cache.SetDefaultExt<APTaxTran.taxType>((object) row);
    this.Taxes.Cache.SetDefaultExt<APTaxTran.taxBucketID>((object) row);
    this.Taxes.Cache.SetDefaultExt<APTaxTran.vendorID>((object) row);
    this.Taxes.Cache.SetDefaultExt<APTaxTran.subID>((object) row);
  }

  protected virtual void APQuickCheck_RefNbr_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!this._IsVoidCheckInProgress)
      return;
    e.Cancel = true;
  }

  protected virtual void APQuickCheck_FinPeriodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!this._IsVoidCheckInProgress)
      return;
    e.Cancel = true;
  }

  protected virtual void APQuickCheck_AdjFinPeriodID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!this._IsVoidCheckInProgress)
      return;
    e.Cancel = true;
  }

  protected virtual void APQuickCheck_EmployeeID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!this._IsVoidCheckInProgress)
      return;
    e.Cancel = true;
  }

  public virtual void VoidCheckProc(APQuickCheck doc)
  {
    this.Clear(PXClearOption.PreserveTimeStamp);
    this.Document.View.Answer = WebDialogResult.No;
    TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(this.Transactions.Cache, (object) null, TaxCalc.NoCalc);
    foreach (PXResult<APQuickCheck, PX.Objects.CM.Extensions.CurrencyInfo> pxResult in PXSelectBase<APQuickCheck, PXSelectJoin<APQuickCheck, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APQuickCheck.curyInfoID>>>, Where<APQuickCheck.docType, Equal<Required<APQuickCheck.docType>>, And<APQuickCheck.refNbr, Equal<Required<APQuickCheck.refNbr>>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr))
    {
      doc = (APQuickCheck) pxResult;
      PX.Objects.CM.Extensions.CurrencyInfo copy1 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy((PX.Objects.CM.Extensions.CurrencyInfo) pxResult);
      copy1.CuryInfoID = new long?();
      copy1.IsReadOnly = new bool?(false);
      PX.Objects.CM.Extensions.CurrencyInfo copy2 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(this.currencyinfo.Insert(copy1));
      APQuickCheck apQuickCheck1 = new APQuickCheck();
      apQuickCheck1.DocType = "VQC";
      apQuickCheck1.RefNbr = doc.RefNbr;
      apQuickCheck1.CuryInfoID = copy2.CuryInfoID;
      this.Document.Insert(apQuickCheck1);
      APQuickCheck copy3 = PXCache<APQuickCheck>.CreateCopy(doc);
      copy3.DocType = "VQC";
      copy3.CuryInfoID = copy2.CuryInfoID;
      copy3.CATranID = new long?();
      copy3.NoteID = new Guid?();
      copy3.OpenDoc = new bool?(true);
      copy3.Released = new bool?(false);
      if (doc.Released.GetValueOrDefault())
        copy3.PrebookBatchNbr = (string) null;
      copy3.Prebooked = new bool?(false);
      this.Document.Cache.SetDefaultExt<APQuickCheck.hold>((object) copy3);
      this.Document.Cache.SetDefaultExt<APRegister.isMigratedRecord>((object) copy3);
      this.Document.Cache.SetDefaultExt<APQuickCheck.status>((object) copy3);
      copy3.LineCntr = new int?(0);
      copy3.AdjCntr = new int?(0);
      copy3.BatchNbr = (string) null;
      copy3.AdjDate = doc.DocDate;
      copy3.AdjFinPeriodID = doc.AdjFinPeriodID;
      copy3.AdjTranPeriodID = doc.AdjTranPeriodID;
      APQuickCheck apQuickCheck2 = copy3;
      Decimal? curyOrigDocAmt = copy3.CuryOrigDocAmt;
      Decimal? curyOrigDiscAmt = copy3.CuryOrigDiscAmt;
      Decimal? nullable = curyOrigDocAmt.HasValue & curyOrigDiscAmt.HasValue ? new Decimal?(curyOrigDocAmt.GetValueOrDefault() + curyOrigDiscAmt.GetValueOrDefault()) : new Decimal?();
      apQuickCheck2.CuryDocBal = nullable;
      copy3.CuryLineTotal = new Decimal?(0M);
      copy3.CuryTaxTotal = new Decimal?(0M);
      copy3.CuryOrigWhTaxAmt = new Decimal?(0M);
      copy3.CuryChargeAmt = new Decimal?(0M);
      copy3.CuryVatExemptTotal = new Decimal?(0M);
      copy3.CuryVatTaxableTotal = new Decimal?(0M);
      copy3.ClosedDate = new System.DateTime?();
      copy3.ClosedFinPeriodID = (string) null;
      copy3.ClosedTranPeriodID = (string) null;
      copy3.Printed = new bool?(true);
      copy3.PrintCheck = new bool?(false);
      copy3.CashAccountID = new int?();
      this.Document.Cache.SetDefaultExt<APRegister.employeeID>((object) copy3);
      this.Document.Cache.SetDefaultExt<APRegister.employeeWorkgroupID>((object) copy3);
      copy3.ClearDate = !copy3.Cleared.GetValueOrDefault() ? new System.DateTime?() : copy3.DocDate;
      this.Document.Update(copy3);
      copy3.CashAccountID = doc.CashAccountID;
      this.Document.Update(copy3);
      using (new SuppressWorkflowAutoPersistScope((PXGraph) this))
        this.initializeState.Press();
      if (copy2 != null)
      {
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<APQuickCheck.curyInfoID>>>>.Config>.Select((PXGraph) this, (object[]) null);
        currencyInfo.CuryID = copy2.CuryID;
        currencyInfo.CuryEffDate = copy2.CuryEffDate;
        currencyInfo.CuryRateTypeID = copy2.CuryRateTypeID;
        currencyInfo.CuryRate = copy2.CuryRate;
        currencyInfo.RecipRate = copy2.RecipRate;
        currencyInfo.CuryMultDiv = copy2.CuryMultDiv;
        this.currencyinfo.Update(currencyInfo);
      }
    }
    TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(this.Transactions.Cache, (object) null, TaxCalc.ManualCalc);
    foreach (PXResult<APTran> pxResult in PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr))
    {
      APTran src_row = (APTran) pxResult;
      APTran copy = PXCache<APTran>.CreateCopy(src_row);
      copy.TranType = (string) null;
      copy.RefNbr = (string) null;
      copy.DrCr = (string) null;
      APTran apTran1 = copy;
      bool? nullable1 = new bool?();
      bool? nullable2 = nullable1;
      apTran1.Released = nullable2;
      copy.CuryInfoID = new long?();
      copy.NoteID = new Guid?();
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, copy.InventoryID);
      if (inventoryItem != null)
      {
        nullable1 = inventoryItem.IsConverted;
        if (nullable1.GetValueOrDefault())
        {
          nullable1 = copy.IsStockItem;
          if (nullable1.HasValue)
          {
            nullable1 = copy.IsStockItem;
            bool? nullable3 = inventoryItem.StkItem;
            if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
            {
              nullable3 = inventoryItem.StkItem;
              if (nullable3.GetValueOrDefault())
                copy.InventoryID = new int?();
              APTran apTran2 = copy;
              nullable3 = new bool?();
              bool? nullable4 = nullable3;
              apTran2.IsStockItem = nullable4;
            }
          }
        }
      }
      APTran dst_row = this.Transactions.Insert(copy);
      PXNoteAttribute.CopyNoteAndFiles(this.Transactions.Cache, (object) src_row, this.Transactions.Cache, (object) dst_row);
      short? nullable5 = src_row.Box1099;
      if (!nullable5.HasValue)
      {
        APTran apTran3 = dst_row;
        nullable5 = new short?();
        short? nullable6 = nullable5;
        apTran3.Box1099 = nullable6;
      }
      if (src_row.DeferredCode == null)
        dst_row.DeferredCode = (string) null;
    }
    List<APTaxTran> apTaxTranList = new List<APTaxTran>();
    foreach (PXResult<APTaxTran> pxResult in PXSelectBase<APTaxTran, PXSelect<APTaxTran, Where<APTaxTran.tranType, Equal<Required<APTaxTran.tranType>>, And<APTaxTran.refNbr, Equal<Required<APTaxTran.refNbr>>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr))
    {
      APTaxTran apTaxTran1 = (APTaxTran) pxResult;
      PXSelectJoin<APTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.module, Equal<BatchModule.moduleAP>, And<APTaxTran.tranType, Equal<Current<APQuickCheck.docType>>, And<APTaxTran.refNbr, Equal<Current<APQuickCheck.refNbr>>>>>> taxes = this.Taxes;
      APTaxTran apTaxTran2 = new APTaxTran();
      apTaxTran2.TaxID = apTaxTran1.TaxID;
      APTaxTran apTaxTran3 = taxes.Insert(apTaxTran2);
      if (apTaxTran3 != null)
      {
        APTaxTran copy = PXCache<APTaxTran>.CreateCopy(apTaxTran3);
        copy.TaxRate = apTaxTran1.TaxRate;
        copy.CuryTaxableAmt = apTaxTran1.CuryTaxableAmt;
        copy.CuryTaxAmt = apTaxTran1.CuryTaxAmt;
        copy.CuryTaxAmtSumm = apTaxTran1.CuryTaxAmtSumm;
        copy.CuryTaxDiscountAmt = apTaxTran1.CuryTaxDiscountAmt;
        copy.CuryTaxableDiscountAmt = apTaxTran1.CuryTaxableDiscountAmt;
        copy.CuryExpenseAmt = apTaxTran1.CuryExpenseAmt;
        apTaxTranList.Add(copy);
      }
    }
    foreach (APTaxTran apTaxTran in apTaxTranList)
      this.Taxes.Update(apTaxTran);
    foreach (PXResult<APTax> pxResult in PXSelectBase<APTax, PXSelect<APTax, Where<APTax.tranType, Equal<Required<APTax.tranType>>, And<APTax.refNbr, Equal<Required<APTax.refNbr>>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr))
    {
      APTax apTax1 = (APTax) pxResult;
      PXCache cache = this.Tax_Rows.Cache;
      APTax apTax2 = new APTax();
      apTax2.TranType = "VQC";
      apTax2.RefNbr = doc.RefNbr;
      apTax2.LineNbr = apTax1.LineNbr;
      apTax2.TaxID = apTax1.TaxID;
      if (cache.Locate((object) apTax2) is APTax data)
      {
        this.Tax_Rows.Cache.SetValueExt<APTax.taxRate>((object) data, (object) apTax1.TaxRate);
        this.Tax_Rows.Cache.SetValueExt<APTax.curyTaxableAmt>((object) data, (object) apTax1.CuryTaxableAmt);
        this.Tax_Rows.Cache.SetValueExt<APTax.curyTaxAmt>((object) data, (object) apTax1.CuryTaxAmt);
        this.Tax_Rows.Cache.SetValueExt<APTax.curyExpenseAmt>((object) data, (object) apTax1.CuryExpenseAmt);
      }
    }
    APQuickCheck current = this.Document.Current;
    current.CuryOrigDiscAmt = doc.CuryOrigDiscAmt;
    this.Document.Update(current);
    this.PaymentCharges.ReverseCharges((PX.Objects.CM.IRegister) doc, (PX.Objects.CM.IRegister) this.Document.Current);
  }

  public virtual void CashReturnProc(APQuickCheck doc)
  {
    this.Clear(PXClearOption.PreserveTimeStamp);
    this.Document.View.Answer = WebDialogResult.No;
    TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(this.Transactions.Cache, (object) null, TaxCalc.NoCalc);
    foreach (PXResult<APQuickCheck, PX.Objects.CM.Extensions.CurrencyInfo> pxResult in PXSelectBase<APQuickCheck, PXSelectJoin<APQuickCheck, InnerJoin<PX.Objects.CM.Extensions.CurrencyInfo, On<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<APQuickCheck.curyInfoID>>>, Where<APQuickCheck.docType, Equal<Required<APQuickCheck.docType>>, And<APQuickCheck.refNbr, Equal<Required<APQuickCheck.refNbr>>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr))
    {
      doc = (APQuickCheck) pxResult;
      PX.Objects.CM.Extensions.CurrencyInfo copy1 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy((PX.Objects.CM.Extensions.CurrencyInfo) pxResult);
      copy1.CuryInfoID = new long?();
      copy1.IsReadOnly = new bool?(false);
      PX.Objects.CM.Extensions.CurrencyInfo copy2 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(this.currencyinfo.Insert(copy1));
      PXSelectJoin<APQuickCheck, LeftJoinSingleTable<Vendor, On<Vendor.bAccountID, Equal<APQuickCheck.vendorID>>>, Where<APQuickCheck.docType, Equal<Optional<APQuickCheck.docType>>, And<Where<Vendor.bAccountID, IsNull, Or<Match<Vendor, Current<AccessInfo.userName>>>>>>> document = this.Document;
      APQuickCheck apQuickCheck1 = new APQuickCheck();
      apQuickCheck1.DocType = "RQC";
      apQuickCheck1.RefNbr = (string) null;
      apQuickCheck1.CuryInfoID = copy2.CuryInfoID;
      APQuickCheck apQuickCheck2 = document.Insert(apQuickCheck1);
      if (apQuickCheck2.RefNbr == null)
      {
        apQuickCheck2.RefNbr = (APQuickCheck) PXSelectBase<APQuickCheck, PXSelect<APQuickCheck>.Config>.Search<APQuickCheck.docType, APQuickCheck.refNbr>((PXGraph) this, (object) "RQC", (object) doc.RefNbr) == null ? doc.RefNbr : throw new PXException("The record already exists.");
        this.Document.Cache.Normalize();
        this.Document.Update(apQuickCheck2);
      }
      APQuickCheck copy3 = PXCache<APQuickCheck>.CreateCopy(doc);
      copy3.DocType = "RQC";
      copy3.RefNbr = this.Document.Current.RefNbr;
      copy3.CuryInfoID = copy2.CuryInfoID;
      copy3.CATranID = new long?();
      copy3.NoteID = new Guid?();
      copy3.OpenDoc = new bool?(true);
      copy3.Released = new bool?(false);
      copy3.PrebookBatchNbr = (string) null;
      copy3.Prebooked = new bool?(false);
      this.Document.Cache.SetDefaultExt<APQuickCheck.hold>((object) copy3);
      this.Document.Cache.SetDefaultExt<APRegister.isMigratedRecord>((object) copy3);
      this.Document.Cache.SetDefaultExt<APQuickCheck.status>((object) copy3);
      copy3.LineCntr = new int?(0);
      copy3.AdjCntr = new int?(0);
      copy3.BatchNbr = (string) null;
      copy3.AdjDate = doc.DocDate;
      copy3.AdjFinPeriodID = doc.AdjFinPeriodID;
      copy3.AdjTranPeriodID = doc.AdjTranPeriodID;
      APQuickCheck apQuickCheck3 = copy3;
      Decimal? curyOrigDocAmt = copy3.CuryOrigDocAmt;
      Decimal? curyOrigDiscAmt = copy3.CuryOrigDiscAmt;
      Decimal? nullable = curyOrigDocAmt.HasValue & curyOrigDiscAmt.HasValue ? new Decimal?(curyOrigDocAmt.GetValueOrDefault() + curyOrigDiscAmt.GetValueOrDefault()) : new Decimal?();
      apQuickCheck3.CuryDocBal = nullable;
      copy3.CuryLineTotal = new Decimal?(0M);
      copy3.CuryTaxTotal = new Decimal?(0M);
      copy3.CuryOrigWhTaxAmt = new Decimal?(0M);
      copy3.CuryChargeAmt = new Decimal?(0M);
      copy3.CuryVatExemptTotal = new Decimal?(0M);
      copy3.CuryVatTaxableTotal = new Decimal?(0M);
      copy3.ClosedDate = new System.DateTime?();
      copy3.ClosedFinPeriodID = (string) null;
      copy3.ClosedTranPeriodID = (string) null;
      copy3.Printed = new bool?(true);
      copy3.PrintCheck = new bool?(false);
      copy3.CashAccountID = new int?();
      copy3.ExtRefNbr = (string) null;
      copy3.EmployeeID = new int?();
      copy3.EmployeeWorkgroupID = new int?();
      copy3.Cleared = new bool?(false);
      copy3.ClearDate = new System.DateTime?();
      copy3.OrigDocType = doc.DocType;
      copy3.OrigRefNbr = doc.RefNbr;
      copy3.OrigModule = "AP";
      this.Document.Update(copy3);
      copy3.CashAccountID = doc.CashAccountID;
      this.Document.Update(copy3);
      using (new SuppressWorkflowAutoPersistScope((PXGraph) this))
        this.initializeState.Press();
      if (copy2 != null)
      {
        PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = (PX.Objects.CM.Extensions.CurrencyInfo) PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<APQuickCheck.curyInfoID>>>>.Config>.Select((PXGraph) this, (object[]) null);
        currencyInfo.CuryID = copy2.CuryID;
        currencyInfo.CuryEffDate = copy2.CuryEffDate;
        currencyInfo.CuryRateTypeID = copy2.CuryRateTypeID;
        currencyInfo.CuryRate = copy2.CuryRate;
        currencyInfo.RecipRate = copy2.RecipRate;
        currencyInfo.CuryMultDiv = copy2.CuryMultDiv;
        this.currencyinfo.Update(currencyInfo);
      }
    }
    TaxBaseAttribute.SetTaxCalc<APTran.taxCategoryID>(this.Transactions.Cache, (object) null, TaxCalc.ManualCalc);
    foreach (PXResult<APTran> pxResult in PXSelectBase<APTran, PXSelect<APTran, Where<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr))
    {
      APTran src_row = (APTran) pxResult;
      APTran copy = PXCache<APTran>.CreateCopy(src_row);
      copy.TranType = (string) null;
      copy.RefNbr = (string) null;
      copy.TranID = new int?();
      copy.DrCr = (string) null;
      APTran apTran1 = copy;
      bool? nullable1 = new bool?();
      bool? nullable2 = nullable1;
      apTran1.Released = nullable2;
      copy.CuryInfoID = new long?();
      copy.NoteID = new Guid?();
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, copy.InventoryID);
      if (inventoryItem != null)
      {
        nullable1 = inventoryItem.IsConverted;
        if (nullable1.GetValueOrDefault())
        {
          nullable1 = copy.IsStockItem;
          if (nullable1.HasValue)
          {
            nullable1 = copy.IsStockItem;
            bool? nullable3 = inventoryItem.StkItem;
            if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
            {
              nullable3 = inventoryItem.StkItem;
              if (nullable3.GetValueOrDefault())
                copy.InventoryID = new int?();
              APTran apTran2 = copy;
              nullable3 = new bool?();
              bool? nullable4 = nullable3;
              apTran2.IsStockItem = nullable4;
            }
          }
        }
      }
      APTran dst_row = this.Transactions.Insert(copy);
      PXNoteAttribute.CopyNoteAndFiles(this.Transactions.Cache, (object) src_row, this.Transactions.Cache, (object) dst_row);
      short? nullable5 = src_row.Box1099;
      if (!nullable5.HasValue)
      {
        APTran apTran3 = dst_row;
        nullable5 = new short?();
        short? nullable6 = nullable5;
        apTran3.Box1099 = nullable6;
      }
      dst_row.TaxCategoryID = src_row.TaxCategoryID;
      if (src_row.DeferredCode == null)
        dst_row.DeferredCode = (string) null;
    }
    List<APTaxTran> apTaxTranList = new List<APTaxTran>();
    foreach (PXResult<APTaxTran> pxResult in PXSelectBase<APTaxTran, PXSelect<APTaxTran, Where<APTaxTran.tranType, Equal<Required<APTaxTran.tranType>>, And<APTaxTran.refNbr, Equal<Required<APTaxTran.refNbr>>>>>.Config>.Select((PXGraph) this, (object) doc.DocType, (object) doc.RefNbr))
    {
      APTaxTran apTaxTran1 = (APTaxTran) pxResult;
      PXSelectJoin<APTaxTran, LeftJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<APTaxTran.taxID>>>, Where<APTaxTran.module, Equal<BatchModule.moduleAP>, And<APTaxTran.tranType, Equal<Current<APQuickCheck.docType>>, And<APTaxTran.refNbr, Equal<Current<APQuickCheck.refNbr>>>>>> taxes = this.Taxes;
      APTaxTran apTaxTran2 = new APTaxTran();
      apTaxTran2.TaxID = apTaxTran1.TaxID;
      APTaxTran apTaxTran3 = taxes.Insert(apTaxTran2);
      if (apTaxTran3 != null)
      {
        APTaxTran copy = PXCache<APTaxTran>.CreateCopy(apTaxTran3);
        copy.TaxRate = apTaxTran1.TaxRate;
        copy.CuryTaxableAmt = apTaxTran1.CuryTaxableAmt;
        copy.CuryTaxAmt = apTaxTran1.CuryTaxAmt;
        copy.CuryTaxAmtSumm = apTaxTran1.CuryTaxAmtSumm;
        copy.CuryTaxDiscountAmt = apTaxTran1.CuryTaxDiscountAmt;
        copy.CuryTaxableDiscountAmt = apTaxTran1.CuryTaxableDiscountAmt;
        apTaxTranList.Add(copy);
      }
    }
    foreach (APTaxTran apTaxTran in apTaxTranList)
      this.Taxes.Update(apTaxTran);
    APQuickCheck current = this.Document.Current;
    current.CuryOrigDiscAmt = doc.CuryOrigDiscAmt;
    this.Document.Update(current);
  }

  /// <summary>
  /// Ask user for approval for creation of another cash return if a cash return document already exists for the original document.
  /// </summary>
  /// <param name="origDoc">The original document.</param>
  /// <returns>True if user approves, false if not.</returns>
  protected virtual bool AskUserApprovalIfCashReturnDocumentAlreadyExists(APQuickCheck origDoc)
  {
    APRegister apRegister = (APRegister) PXSelectBase<APRegister, PXSelect<APRegister, Where<APRegister.docType, Equal<APDocType.cashReturn>, And<APRegister.origDocType, Equal<Required<APRegister.origDocType>>, And<APRegister.origRefNbr, Equal<Required<APRegister.origRefNbr>>>>>, OrderBy<Desc<APRegister.createdDateTime>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) origDoc.DocType, (object) origDoc.RefNbr);
    if (apRegister == null)
      return true;
    return this.Document.View.Ask(PXMessages.LocalizeFormatNoPrefix("A reversing {0} document with the {1} ref. number already exists. Do you want to continue?", (object) APDocType.GetDisplayName(apRegister.DocType), (object) apRegister.RefNbr), MessageButtons.YesNo) == WebDialogResult.Yes;
  }

  public class APQuickCheckEntryDocumentExtension : PaidInvoiceGraphExtension<APQuickCheckEntry>
  {
    public override PXSelectBase<PX.Objects.CR.Location> Location
    {
      get => (PXSelectBase<PX.Objects.CR.Location>) this.Base.location;
    }

    public override PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo> CurrencyInfo
    {
      get => (PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.Base.currencyinfo;
    }

    public override void Initialize()
    {
      base.Initialize();
      this.Documents = new PXSelectExtension<PaidInvoice>((PXSelectBase) this.Base.Document);
      this.Lines = new PXSelectExtension<PX.Objects.Common.GraphExtensions.Abstract.DAC.DocumentLine>((PXSelectBase) this.Base.Transactions);
      this.InvoiceTrans = new PXSelectExtension<InvoiceTran>((PXSelectBase) this.Base.Transactions);
      this.TaxTrans = new PXSelectExtension<GenericTaxTran>((PXSelectBase) this.Base.Taxes);
      this.LineTaxes = new PXSelectExtension<LineTax>((PXSelectBase) this.Base.Tax_Rows);
    }

    public override void SuppressApproval() => this.Base.Approval.SuppressApproval = true;

    protected override PaidInvoiceMapping GetDocumentMapping()
    {
      return new PaidInvoiceMapping(typeof (APQuickCheck))
      {
        HeaderFinPeriodID = typeof (APQuickCheck.adjFinPeriodID),
        HeaderTranPeriodID = typeof (APQuickCheck.adjTranPeriodID),
        HeaderDocDate = typeof (APQuickCheck.adjDate),
        ContragentID = typeof (APQuickCheck.vendorID),
        ContragentLocationID = typeof (APQuickCheck.vendorLocationID)
      };
    }

    protected override DocumentLineMapping GetDocumentLineMapping()
    {
      return new DocumentLineMapping(typeof (APTran));
    }

    protected override ContragentMapping GetContragentMapping()
    {
      return new ContragentMapping(typeof (Vendor));
    }

    protected override InvoiceTranMapping GetInvoiceTranMapping()
    {
      return new InvoiceTranMapping(typeof (APTran));
    }

    protected override GenericTaxTranMapping GetGenericTaxTranMapping()
    {
      return new GenericTaxTranMapping(typeof (APTaxTran));
    }

    protected override LineTaxMapping GetLineTaxMapping() => new LineTaxMapping(typeof (APTax));
  }

  public class MultiCurrency : APMultiCurrencyGraph<APQuickCheckEntry, APQuickCheck>
  {
    protected override string DocumentStatus => this.Base.Document.Current?.Status;

    protected override MultiCurrencyGraph<APQuickCheckEntry, APQuickCheck>.CurySourceMapping GetCurySourceMapping()
    {
      return new MultiCurrencyGraph<APQuickCheckEntry, APQuickCheck>.CurySourceMapping(typeof (PX.Objects.CA.CashAccount))
      {
        CuryID = typeof (PX.Objects.CA.CashAccount.curyID),
        CuryRateTypeID = typeof (PX.Objects.CA.CashAccount.curyRateTypeID)
      };
    }

    protected override CurySource CurrentSourceSelect()
    {
      CurySource curySource = base.CurrentSourceSelect();
      if (curySource != null)
        curySource.AllowOverrideRate = (bool?) this.Base.vendor?.Current?.AllowOverrideRate;
      return curySource;
    }

    protected override MultiCurrencyGraph<APQuickCheckEntry, APQuickCheck>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<APQuickCheckEntry, APQuickCheck>.DocumentMapping(typeof (APQuickCheck))
      {
        DocumentDate = typeof (APQuickCheck.adjDate),
        BAccountID = typeof (APQuickCheck.vendorID)
      };
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[5]
      {
        (PXSelectBase) this.Base.Document,
        (PXSelectBase) this.Base.Transactions,
        (PXSelectBase) this.Base.Tax_Rows,
        (PXSelectBase) this.Base.Taxes,
        (PXSelectBase) this.Base.PaymentCharges
      };
    }

    protected override bool ShouldBeDisabledDueToDocStatus()
    {
      return this.Base.Document.Current?.DocType == "VQC" || base.ShouldBeDisabledDueToDocStatus();
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<APQuickCheck, APQuickCheck.cashAccountID> e)
    {
      if (this.Base._IsVoidCheckInProgress || !PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>())
        return;
      this.SourceFieldUpdated<APQuickCheck.curyInfoID, APQuickCheck.curyID, APQuickCheck.adjDate>(e.Cache, (IBqlTable) e.Row);
      this.SetDetailCuryInfoID<APTran>((PXSelectBase<APTran>) this.Base.Transactions, e.Row.CuryInfoID);
    }
  }

  /// <exclude />
  public class APQuickCheckEntryAddressLookupExtension : 
    AddressLookupExtension<APQuickCheckEntry, APQuickCheck, APAddress>
  {
    protected override string AddressView => "Remittance_Address";
  }

  public class APQuickCheckEntryAddressCachingHelper : 
    AddressValidationExtension<APQuickCheckEntry, APAddress>
  {
    protected override IEnumerable<PXSelectBase<APAddress>> AddressSelects()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      APQuickCheckEntry.APQuickCheckEntryAddressCachingHelper addressCachingHelper = this;
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
      this.\u003C\u003E2__current = (PXSelectBase<APAddress>) addressCachingHelper.Base.Remittance_Address;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }
  }
}
