// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Api.Export;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.Descriptor;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR.CCPaymentProcessing;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using PX.Objects.AR.Override;
using PX.Objects.AR.Repositories;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Scopes;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CR.Extensions.CRCreateActions;
using PX.Objects.CR.Extensions.Relational;
using PX.Objects.CR.GraphExtensions;
using PX.Objects.CS;
using PX.Objects.CS.Attributes;
using PX.Objects.GDPR;
using PX.Objects.GL;
using PX.Objects.GL.Helpers;
using PX.Objects.GraphExtensions.ExtendBAccount;
using PX.Objects.IN;
using PX.Objects.SO;
using PX.Objects.TX;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

#nullable enable
namespace PX.Objects.AR;

public class CustomerMaint : PXGraph<
#nullable disable
CustomerMaint, Customer>
{
  [PXViewName("Customer")]
  public PXSelect<Customer, Where2<Match<Current<AccessInfo.userName>>, And<Where<PX.Objects.CR.BAccount.type, Equal<BAccountType.customerType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.combinedType>>>>>> BAccount;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Standalone.Location> BaseLocations;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Address> AddressDummy;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Contact> ContactDummy;
  public PXSelect<BAccountItself, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Optional<PX.Objects.CR.BAccount.bAccountID>>>> CurrentBAccountItself;
  public PXSetup<Company> cmpany;
  public PXSelect<Customer, Where<Customer.bAccountID, Equal<Current<Customer.bAccountID>>>> CurrentCustomer;
  public PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.bAccountID, Equal<Current<Customer.bAccountID>>, And<PX.Objects.CR.Contact.contactID, Equal<Current<Customer.defBillContactID>>>>> BillContact;
  public PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.bAccountID, Equal<Current<Customer.bAccountID>>, And<PX.Objects.CR.Address.addressID, Equal<Current<Customer.defBillAddressID>>>>> BillAddress;
  [PXCopyPasteHiddenView]
  public PXSelect<CustSalesPeople, Where<CustSalesPeople.bAccountID, Equal<Current<Customer.bAccountID>>>, OrderBy<Asc<CustSalesPeople.salesPersonID, Asc<CustSalesPeople.locationID>>>> SalesPersons;
  [PXCopyPasteHiddenView]
  public PXSelect<ARBalancesByBaseCuryID, Where<ARBalancesByBaseCuryID.customerID, Equal<Current<Customer.bAccountID>>>, OrderBy<Asc<ARBalancesByBaseCuryID.baseCuryID>>> Balances;
  public PXSetup<PX.Objects.AR.CustomerClass, Where<PX.Objects.AR.CustomerClass.customerClassID, Equal<Optional<Customer.customerClassID>>>> CustomerClass;
  public PXSetup<PX.Objects.AR.ARSetup> ARSetup;
  public PXFilter<CustomerMaint.CustomerBalanceSummary> CustomerBalance;
  public PXSelect<ARBalances> Balance_for_auto_delete;
  public PXSelect<CarrierPluginCustomer, Where<CarrierPluginCustomer.customerID, Equal<Current<Customer.bAccountID>>>> Carriers;
  [PXViewName("Answers")]
  public CRAttributeList<Customer> Answers;
  public CRNotificationSourceList<Customer, Customer.customerClassID, ARNotificationSource.customer> NotificationSources;
  public CRNotificationRecipientList<Customer, Customer.customerClassID> NotificationRecipients;
  public PXSelectJoin<CCProcessingCenter, InnerJoin<CustomerPaymentMethod, On<CCProcessingCenter.processingCenterID, Equal<CustomerPaymentMethod.cCProcessingCenterID>>>, Where<CustomerPaymentMethod.pMInstanceID, Equal<Optional<Customer.defPMInstanceID>>>> PMProcessingCenter;
  public PXFilter<CustomerMaint.OnDemandStatementParameters> OnDemandStatementDialog;
  [PXHidden]
  public PXSelect<INItemXRef> xrefs;
  [PXHidden]
  public PXSetupOptional<PX.Objects.CR.CRSetup> CRSetup;
  public PXAction<Customer> viewRestrictionGroups;
  public PXDBAction<Customer> viewBusnessAccount;
  public PXAction<Customer> customerDocuments;
  public PXAction<Customer> statementForCustomer;
  public PXAction<Customer> newInvoiceMemo;
  public PXAction<Customer> newSalesOrder;
  public PXAction<Customer> newPayment;
  public PXAction<Customer> writeOffBalance;
  public PXAction<Customer> viewBillAddressOnMap;
  public PXAction<Customer> regenerateLastStatement;
  /// <summary>
  /// Generate an on-demand customer statement for the current customer
  /// without updating the statement cycle and customer's last statement date.
  /// </summary>
  public PXAction<Customer> generateOnDemandStatement;
  public PXAction<Customer> action;
  public PXAction<Customer> inquiry;
  public PXAction<Customer> report;
  public PXAction<Customer> aRBalanceByCustomer;
  public PXAction<Customer> customerHistory;
  public PXAction<Customer> aRAgedPastDue;
  public PXAction<Customer> aRAgedOutstanding;
  public PXAction<Customer> aRRegister;
  public PXAction<Customer> customerDetails;
  public PXAction<Customer> customerStatement;
  public PXAction<Customer> salesPrice;
  public PXChangeBAccountID<Customer, Customer.acctCD> ChangeID;
  [PXCopyPasteHiddenView]
  public PXSelectOrderBy<CustomerMaint.ChildCustomerBalanceSummary, OrderBy<Desc<CustomerMaint.ChildCustomerBalanceSummary.isParent>>> ChildAccounts;
  private bool doCopyClassSettings;

  [InjectDependency]
  internal IBAccountRestrictionHelper BAccountRestrictionHelper { get; set; }

  [PXSelector(typeof (Search<NotificationSetup.setupID, Where<NotificationSetup.sourceCD, Equal<ARNotificationSource.customer>>>))]
  [PXMergeAttributes]
  protected virtual void NotificationSource_SetupID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCheckUnique(new System.Type[] {typeof (NotificationSource.setupID)}, IgnoreNulls = false, Where = typeof (Where<NotificationSource.refNoteID, Equal<Current<NotificationSource.refNoteID>>>))]
  protected virtual void NotificationSource_NBranchID_CacheAttached(PXCache sender)
  {
  }

  [PXSelector(typeof (Search<SiteMap.screenID, Where2<Where<SiteMap.url, Like<PX.Objects.Common.urlReports>, Or<SiteMap.url, Like<urlReportsInNewUi>>>, And<Where<SiteMap.screenID, Like<PXModule.ar_>, Or<SiteMap.screenID, Like<PXModule.so_>, Or<SiteMap.screenID, Like<PXModule.cr_>>>>>>, OrderBy<Asc<SiteMap.screenID>>>), new System.Type[] {typeof (SiteMap.screenID), typeof (SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (SiteMap.title))]
  [PXMergeAttributes]
  protected virtual void NotificationSource_ReportID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [CustomerContactType.List]
  [PXCheckDistinct(new System.Type[] {typeof (NotificationRecipient.contactID)}, Where = typeof (Where<NotificationRecipient.sourceID, Equal<Current<NotificationRecipient.sourceID>>, And<NotificationRecipient.refNoteID, Equal<Current<Customer.noteID>>>>))]
  [PXMergeAttributes]
  protected virtual void NotificationRecipient_ContactType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(10, IsUnicode = true)]
  protected virtual void NotificationRecipient_ClassID_CacheAttached(PXCache sender)
  {
  }

  [SalesPerson(IsKey = true, DescriptionField = typeof (SalesPerson.descr))]
  [PXParent(typeof (Select<SalesPerson, Where<SalesPerson.salesPersonID, Equal<Current<CustSalesPeople.salesPersonID>>>>))]
  public virtual void CustSalesPeople_SalesPersonID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.CR.BAccount.bAccountID))]
  [PXParent(typeof (Select<Customer, Where<Customer.bAccountID, Equal<Current<CustSalesPeople.bAccountID>>>>))]
  public virtual void CustSalesPeople_BAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXDimensionSelector("LOCATION", typeof (Search<PX.Objects.CR.Standalone.Location.locationID, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<CustSalesPeople.bAccountID>>>>), typeof (PX.Objects.CR.Standalone.Location.locationCD), new System.Type[] {typeof (PX.Objects.CR.Location.locationCD), typeof (PX.Objects.CR.Location.descr)}, DirtyRead = true, DescriptionField = typeof (PX.Objects.CR.Standalone.Location.descr))]
  [PXDefault(typeof (Search<Customer.defLocationID, Where<Customer.bAccountID, Equal<Current<CustSalesPeople.bAccountID>>>>))]
  public virtual void CustSalesPeople_LocationID_CacheAttached(PXCache sender)
  {
  }

  [PXDBDecimal(6)]
  [PXDefault(typeof (Search<SalesPerson.commnPct, Where<SalesPerson.salesPersonID, Equal<Current<CustSalesPeople.salesPersonID>>>>))]
  [PXUIField(DisplayName = "Commission %")]
  public virtual void CustSalesPeople_CommisionPct_CacheAttached(PXCache sender)
  {
  }

  [Customer(DescriptionField = typeof (Customer.acctName), Filterable = true)]
  [PXUIField(DisplayName = "Customer ID")]
  [PXDBDefault(typeof (Customer.bAccountID))]
  [PXParent(typeof (Select<Customer, Where<Customer.bAccountID, Equal<Current<CarrierPluginCustomer.customerID>>>>))]
  public virtual void CarrierPluginCustomer_CustomerID_CacheAttached(PXCache sender)
  {
  }

  /// <summary>
  /// The cache attached field corresponds to the <see cref="P:PX.Objects.AR.Customer.StatementCustomerID" /> field.
  /// </summary>
  [PXMergeAttributes]
  [PXFormula(typeof (Switch<Case<Where<Customer.parentBAccountID, IsNotNull, And<Customer.consolidateStatements, Equal<True>>>, Customer.parentBAccountID>, Customer.bAccountID>))]
  protected virtual void Customer_StatementCustomerID_CacheAttached(PXCache sender)
  {
  }

  /// <summary>
  /// The cache attached field corresponds to the <see cref="P:PX.Objects.AR.Customer.SharedCreditCustomerID" /> field.
  /// </summary>
  [PXMergeAttributes]
  [PXFormula(typeof (Switch<Case<Where<Customer.parentBAccountID, IsNotNull, And<Customer.sharedCreditPolicy, Equal<True>>>, Customer.parentBAccountID>, Customer.bAccountID>))]
  protected virtual void Customer_SharedCreditCustomerID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Switch<Case<Where<Customer.defBillAddressID, Equal<Customer.defAddressID>>, True>, False>))]
  protected virtual void _(PX.Data.Events.CacheAttached<Customer.isBillSameAsMain> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Switch<Case<Where<Customer.defBillContactID, Equal<Customer.defContactID>>, True>, False>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<Customer.isBillContSameAsMain> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Switch<Case<Where<Customer.defBillAddressID, Equal<Customer.defAddressID>>, False>, True>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<Customer.overrideBillAddress> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Switch<Case<Where<Customer.defBillContactID, Equal<Customer.defContactID>>, False>, True>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<Customer.overrideBillContact> e)
  {
  }

  public virtual PXSelectBase<Customer> BAccountAccessor => (PXSelectBase<Customer>) this.BAccount;

  [PXOptimizationBehavior(IgnoreBqlDelegate = true)]
  protected virtual IEnumerable billContact()
  {
    return ((PXGraph) this).GetExtension<CustomerMaint.DefLocationExt>().SelectEntityByKey<PX.Objects.CR.Contact, PX.Objects.CR.Contact.contactID, Customer.defBillContactID, Customer.overrideBillContact>((object) ((PXSelectBase<Customer>) this.BAccount).Current);
  }

  [PXOptimizationBehavior(IgnoreBqlDelegate = true)]
  protected virtual IEnumerable billAddress()
  {
    return ((PXGraph) this).GetExtension<CustomerMaint.DefLocationExt>().SelectEntityByKey<PX.Objects.CR.Address, PX.Objects.CR.Address.addressID, Customer.defBillAddressID, Customer.overrideBillAddress>((object) ((PXSelectBase<Customer>) this.BAccount).Current);
  }

  public CustomerMaint()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(CustomerMaint.\u003C\u003Ec.\u003C\u003E9__48_0 ?? (CustomerMaint.\u003C\u003Ec.\u003C\u003E9__48_0 = new PXFieldDefaulting((object) CustomerMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__48_0))));
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Contact.fullName>(((PXGraph) this).Caches[typeof (PX.Objects.CR.Contact)], (object) null);
    PXUIFieldAttribute.SetVisible<Customer.localeName>(((PXSelectBase) this.BAccount).Cache, (object) null, PXDBLocalizableStringAttribute.HasMultipleLocales);
    PXUIFieldAttribute.SetDisplayName<Customer.acctName>(((PXSelectBase) this.BAccount).Cache, "Account Name");
    ((PXSelectBase) this.Balances).AllowSelect = false;
    ((PXSelectBase) this.Balances).AllowDelete = false;
    ((PXSelectBase) this.Balances).AllowInsert = false;
    ((PXSelectBase) this.Balances).AllowUpdate = false;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewRestrictionGroups(PXAdapter adapter)
  {
    if (((PXSelectBase<Customer>) this.CurrentCustomer).Current != null)
    {
      ARAccessDetail instance = PXGraph.CreateInstance<ARAccessDetail>();
      ((PXSelectBase<Customer>) instance.Customer).Current = PXResultset<Customer>.op_Implicit(((PXSelectBase<Customer>) instance.Customer).Search<Customer.acctCD>((object) ((PXSelectBase<Customer>) this.CurrentCustomer).Current.AcctCD, Array.Empty<object>()));
      throw new PXRedirectRequiredException((PXGraph) instance, false, "Restricted Groups");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewBusnessAccount(PXAdapter adapter)
  {
    PX.Objects.CR.BAccount current = (PX.Objects.CR.BAccount) ((PXSelectBase<Customer>) this.BAccount).Current;
    if (current != null)
    {
      BusinessAccountMaint instance = PXGraph.CreateInstance<BusinessAccountMaint>();
      ((PXGraph) instance).Load();
      ((PXGraph) instance).Clear();
      ((PXSelectBase<PX.Objects.CR.BAccount>) instance.BAccount).Current = PXResultset<PX.Objects.CR.BAccount>.op_Implicit(((PXSelectBase<PX.Objects.CR.BAccount>) instance.BAccount).Search<PX.Objects.CR.BAccount.acctCD>((object) current.AcctCD, Array.Empty<object>()));
      throw new PXRedirectRequiredException((PXGraph) instance, "Edit Business Account");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CustomerDocuments(PXAdapter adapter)
  {
    if (((PXSelectBase<Customer>) this.BAccount).Current != null)
    {
      int? baccountId = ((PXSelectBase<Customer>) this.BAccount).Current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      {
        ARDocumentEnq instance = PXGraph.CreateInstance<ARDocumentEnq>();
        ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) instance.Filter).Current.CustomerID = ((PXSelectBase<Customer>) this.BAccount).Current.BAccountID;
        ((PXSelectBase<ARDocumentEnq.ARDocumentFilter>) instance.Filter).Select(Array.Empty<object>());
        throw new PXRedirectRequiredException((PXGraph) instance, "Customer Details");
      }
    }
    return adapter.Get();
  }

  protected virtual void VerifyCanHaveSeparateStatement(Customer customer)
  {
    if (customer.ParentBAccountID.HasValue && customer.ConsolidateStatements.GetValueOrDefault())
      throw new PXException("This customer can't have separate statement. Please view the statement for the parent customer {0}", new object[1]
      {
        ((PXSelectBase) this.CurrentCustomer).Cache.GetValueExt<Customer.parentBAccountID>((object) customer)
      });
  }

  protected virtual void VerifyCanHaveOnDemandStatement(Customer customer)
  {
    if (customer.StatementType != "O")
      throw new PXException("The system cannot generate statements of the Balance Brought Forward type on demand.");
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable StatementForCustomer(PXAdapter adapter)
  {
    if (((PXSelectBase<Customer>) this.BAccount).Current != null)
    {
      int? baccountId = ((PXSelectBase<Customer>) this.BAccount).Current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      {
        this.VerifyCanHaveSeparateStatement(((PXSelectBase<Customer>) this.CurrentCustomer).Current);
        ARStatementForCustomer instance = PXGraph.CreateInstance<ARStatementForCustomer>();
        ((PXSelectBase<ARStatementForCustomer.ARStatementForCustomerParameters>) instance.Filter).Current.CustomerID = ((PXSelectBase<Customer>) this.BAccount).Current.BAccountID;
        ((PXSelectBase<ARStatementForCustomer.ARStatementForCustomerParameters>) instance.Filter).Select(Array.Empty<object>());
        throw new PXRedirectRequiredException((PXGraph) instance, "Statement For Customer");
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable NewInvoiceMemo(PXAdapter adapter)
  {
    Customer current = ((PXSelectBase<Customer>) this.BAccount).Current;
    if (current != null)
    {
      int? nullable1 = current.BAccountID;
      long? nullable2 = nullable1.HasValue ? new long?((long) nullable1.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable2.GetValueOrDefault() > num & nullable2.HasValue)
      {
        ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
        ((PXGraph) instance).Clear();
        ARInvoice arInvoice1 = ((PXSelectBase<ARInvoice>) instance.Document).Insert(new ARInvoice());
        ARInvoice arInvoice2 = arInvoice1;
        nullable1 = new int?();
        int? nullable3 = nullable1;
        arInvoice2.BranchID = nullable3;
        ((PXSelectBase) instance.Document).Cache.SetValueExt<ARInvoice.customerID>((object) arInvoice1, (object) current.BAccountID);
        if (current.CuryID != null)
          ((PXSelectBase) instance.Document).Cache.SetValueExt<ARInvoice.curyID>((object) arInvoice1, (object) current.CuryID);
        ((PXSelectBase<Customer>) instance.customer).Current.CreditRule = current.CreditRule;
        ((PXSelectBase) instance.Document).Cache.SetDefaultExt<ARInvoice.finPeriodID>((object) arInvoice1);
        ((PXSelectBase) instance.Document).Cache.SetDefaultExt<ARInvoice.tranPeriodID>((object) arInvoice1);
        throw new PXRedirectRequiredException((PXGraph) instance, "ARInvoiceEntry");
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable NewSalesOrder(PXAdapter adapter)
  {
    Customer current1 = ((PXSelectBase<Customer>) this.BAccount).Current;
    PX.Objects.CR.Standalone.Location current2 = ((PXSelectBase<PX.Objects.CR.Standalone.Location>) ((PXGraph) this).GetExtension<CustomerMaint.DefLocationExt>().DefLocation).Current;
    PX.Objects.GL.Branch branch = PX.Objects.GL.Branch.PK.Find((PXGraph) this, ((PXGraph) this).Accessinfo.BranchID);
    if (current1 != null)
    {
      int? nullable1 = current1.BAccountID;
      long? nullable2 = nullable1.HasValue ? new long?((long) nullable1.GetValueOrDefault()) : new long?();
      long num1 = 0;
      if (nullable2.GetValueOrDefault() > num1 & nullable2.HasValue)
      {
        SOOrderEntry instance = PXGraph.CreateInstance<SOOrderEntry>();
        ((PXGraph) instance).Clear();
        PX.Objects.SO.SOOrder soOrder1 = (PX.Objects.SO.SOOrder) ((PXSelectBase) instance.Document).Cache.Insert();
        PX.Objects.SO.SOOrder soOrder2 = soOrder1;
        int num2;
        if (current2 == null)
        {
          num2 = 1;
        }
        else
        {
          nullable1 = current2.CBranchID;
          num2 = !nullable1.HasValue ? 1 : 0;
        }
        int? nullable3;
        int? nullable4;
        if (num2 != 0)
        {
          nullable1 = current1.COrgBAccountID;
          int num3 = 0;
          if (!(nullable1.GetValueOrDefault() == num3 & nullable1.HasValue))
          {
            nullable1 = current1.COrgBAccountID;
            nullable3 = branch.BAccountID;
            if (!(nullable1.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable1.HasValue == nullable3.HasValue))
              goto label_8;
          }
          nullable4 = ((PXGraph) this).Accessinfo.BranchID;
          goto label_10;
        }
label_8:
        nullable3 = new int?();
        nullable4 = nullable3;
label_10:
        soOrder2.BranchID = nullable4;
        ((PXSelectBase) instance.Document).Cache.SetValueExt<PX.Objects.SO.SOOrder.customerID>((object) soOrder1, (object) current1.BAccountID);
        PX.Objects.SO.SOOrder soOrder3 = soOrder1;
        nullable3 = current1.PrimaryContactID;
        nullable2 = nullable3.HasValue ? new long?((long) nullable3.GetValueOrDefault()) : new long?();
        long num4 = 0;
        int? nullable5;
        if (!(nullable2.GetValueOrDefault() < num4 & nullable2.HasValue))
        {
          nullable5 = current1.PrimaryContactID;
        }
        else
        {
          nullable3 = new int?();
          nullable5 = nullable3;
        }
        soOrder3.ContactID = nullable5;
        ((PXSelectBase<Customer>) instance.customer).Current.CreditRule = current1.CreditRule;
        if (((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current.FreightTaxCategoryID != null)
        {
          PX.Objects.SO.SOOrder copy = (PX.Objects.SO.SOOrder) ((PXSelectBase) instance.Document).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current);
          ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Current.FreightTaxCategoryID = (string) null;
          ((PXSelectBase<PX.Objects.SO.SOOrder>) instance.Document).Update(copy);
        }
        throw new PXRedirectRequiredException((PXGraph) instance, "SOOrderEntry");
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable NewPayment(PXAdapter adapter)
  {
    Customer current = ((PXSelectBase<Customer>) this.BAccount).Current;
    if (current != null)
    {
      int? nullable1 = current.BAccountID;
      long? nullable2 = nullable1.HasValue ? new long?((long) nullable1.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable2.GetValueOrDefault() > num & nullable2.HasValue)
      {
        ARPaymentEntry instance = PXGraph.CreateInstance<ARPaymentEntry>();
        ((PXGraph) instance).Clear();
        ARPayment arPayment1 = ((PXSelectBase<ARPayment>) instance.Document).Insert(new ARPayment());
        ARPayment arPayment2 = arPayment1;
        nullable1 = new int?();
        int? nullable3 = nullable1;
        arPayment2.BranchID = nullable3;
        ((PXSelectBase) instance.Document).Cache.SetValueExt<ARPayment.customerID>((object) arPayment1, (object) current.BAccountID);
        ((PXSelectBase) instance.Document).Cache.SetDefaultExt<ARPayment.adjFinPeriodID>((object) arPayment1);
        ((PXSelectBase) instance.Document).Cache.SetDefaultExt<ARPayment.adjTranPeriodID>((object) arPayment1);
        ((PXSelectBase) instance.Document).Cache.SetDefaultExt<ARPayment.finPeriodID>((object) arPayment1);
        ((PXSelectBase) instance.Document).Cache.SetDefaultExt<ARPayment.tranPeriodID>((object) arPayment1);
        ((PXSelectBase<ARPayment>) instance.Document).Update(arPayment1);
        throw new PXRedirectRequiredException((PXGraph) instance, "ARPaymentEntry");
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable WriteOffBalance(PXAdapter adapter)
  {
    Customer current = ((PXSelectBase<Customer>) this.BAccount).Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      {
        ARCreateWriteOff instance = PXGraph.CreateInstance<ARCreateWriteOff>();
        ((PXGraph) instance).Clear();
        ((PXSelectBase<ARWriteOffFilter>) instance.Filter).Current.CustomerID = current.BAccountID;
        ((PXSelectBase<ARWriteOffFilter>) instance.Filter).Current.WOLimit = current.SmallBalanceLimit;
        throw new PXRedirectRequiredException((PXGraph) instance, nameof (WriteOffBalance));
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable ViewBillAddressOnMap(PXAdapter adapter)
  {
    BAccountUtility.ViewOnMap(PXResultset<PX.Objects.CR.Address>.op_Implicit(((PXSelectBase<PX.Objects.CR.Address>) this.BillAddress).Select(Array.Empty<object>())));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable RegenerateLastStatement(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CustomerMaint.\u003C\u003Ec__DisplayClass72_0 cDisplayClass720 = new CustomerMaint.\u003C\u003Ec__DisplayClass72_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass720.customer = ((PXSelectBase<Customer>) this.CurrentCustomer).Current;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass720.customer == null)
      return adapter.Get();
    // ISSUE: reference to a compiler-generated field
    this.VerifyCanHaveSeparateStatement(cDisplayClass720.customer);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass720.cycle = ARStatementCycle.PK.Find((PXGraph) this, ((PXSelectBase<Customer>) this.CurrentCustomer).Current?.StatementCycleId);
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass720.cycle == null)
      throw new PXException("Statement Cycle not specified for the Customer.");
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    IEnumerable<ARStatement> source1 = GraphHelper.RowCast<ARStatement>((IEnumerable) PXSelectBase<ARStatement, PXSelect<ARStatement, Where<ARStatement.customerID, Equal<Required<Customer.bAccountID>>, And<ARStatement.statementCustomerID, Equal<Required<Customer.statementCustomerID>>, And<ARStatement.statementCycleId, Equal<Required<ARStatementCycle.statementCycleId>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
    {
      (object) cDisplayClass720.customer.BAccountID,
      (object) cDisplayClass720.customer.BAccountID,
      (object) cDisplayClass720.cycle.StatementCycleId
    }));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    IEnumerable<ARStatement> source2 = GraphHelper.RowCast<ARStatement>((IEnumerable) PXSelectBase<ARStatement, PXSelect<ARStatement, Where<ARStatement.customerID, Equal<Required<Customer.bAccountID>>, And<ARStatement.statementCustomerID, Equal<Required<Customer.statementCustomerID>>, And<ARStatement.statementCycleId, Equal<Required<ARStatementCycle.statementCycleId>>, And<ARStatement.onDemand, NotEqual<True>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
    {
      (object) cDisplayClass720.customer.BAccountID,
      (object) cDisplayClass720.customer.BAccountID,
      (object) cDisplayClass720.cycle.StatementCycleId
    }));
    if (!source1.Any<ARStatement>())
      throw new PXException("No statements to regenerate. You can prepare a statement according to a statement cycle by using the Prepare Statements (AR503000) form.");
    if (!source2.Any<ARStatement>())
      throw new PXException("The customer has on-demand statements only. They cannot be regenerated. You can generate a new on-demand statement, or prepare a statement according to a statement cycle by using the Prepare Statements (AR503000) form.");
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass720, __methodptr(\u003CRegenerateLastStatement\u003Eb__0)));
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  public virtual IEnumerable GenerateOnDemandStatement(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CustomerMaint.\u003C\u003Ec__DisplayClass74_0 cDisplayClass740 = new CustomerMaint.\u003C\u003Ec__DisplayClass74_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass740.customer = ((PXSelectBase<Customer>) this.CurrentCustomer).Current;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass740.customer == null)
      return adapter.Get();
    // ISSUE: reference to a compiler-generated field
    this.VerifyCanHaveSeparateStatement(cDisplayClass740.customer);
    // ISSUE: reference to a compiler-generated field
    this.VerifyCanHaveOnDemandStatement(cDisplayClass740.customer);
    if (((PXSelectBase<CustomerMaint.OnDemandStatementParameters>) this.OnDemandStatementDialog).AskExt() == 1)
    {
      CustomerMaint.OnDemandStatementParameters current = ((PXSelectBase<CustomerMaint.OnDemandStatementParameters>) this.OnDemandStatementDialog).Current;
      DateTime? statementDate;
      int num;
      if (current == null)
      {
        num = 1;
      }
      else
      {
        statementDate = current.StatementDate;
        num = !statementDate.HasValue ? 1 : 0;
      }
      if (num == 0)
      {
        statementDate = ((PXSelectBase<CustomerMaint.OnDemandStatementParameters>) this.OnDemandStatementDialog).Current.StatementDate;
        DateTime dateTime = statementDate.Value;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass740.statementDate = dateTime;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass740.cycle = ARStatementCycle.PK.Find((PXGraph) this, ((PXSelectBase<Customer>) this.CurrentCustomer).Current?.StatementCycleId);
        // ISSUE: reference to a compiler-generated field
        if (cDisplayClass740.cycle == null)
          throw new PXException("Statement Cycle not specified for the Customer.");
        // ISSUE: method pointer
        PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass740, __methodptr(\u003CGenerateOnDemandStatement\u003Eb__0)));
        return adapter.Get();
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Action(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Inquiry(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Report(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ARBalanceByCustomer(PXAdapter adapter)
  {
    Customer current = this.BAccountAccessor.Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
        throw new PXReportRequiredException(new Dictionary<string, string>()
        {
          ["OrgBAccountID"] = (string) null,
          ["CustomerID"] = current.AcctCD
        }, "AR632500", "AR Balance by Customer", (CurrentLocalization) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CustomerHistory(PXAdapter adapter)
  {
    Customer current = this.BAccountAccessor.Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
        throw new PXReportRequiredException(new Dictionary<string, string>()
        {
          ["OrgBAccountID"] = (string) null,
          ["CustomerID"] = current.AcctCD
        }, "AR652000", "Customer History", (CurrentLocalization) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ARAgedPastDue(PXAdapter adapter)
  {
    Customer current = this.BAccountAccessor.Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
        throw new PXReportRequiredException(new Dictionary<string, string>()
        {
          ["OrgBAccountID"] = (string) null,
          ["CustomerID"] = current.AcctCD
        }, "AR631000", "AR Aging", (CurrentLocalization) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ARAgedOutstanding(PXAdapter adapter)
  {
    Customer current = this.BAccountAccessor.Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
        throw new PXReportRequiredException(new Dictionary<string, string>()
        {
          ["OrgBAccountID"] = (string) null,
          ["CustomerID"] = current.AcctCD
        }, "AR631500", "AR Coming Due", (CurrentLocalization) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ARRegister(PXAdapter adapter)
  {
    Customer current = this.BAccountAccessor.Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
        throw new PXReportRequiredException(new Dictionary<string, string>()
        {
          ["OrgBAccountID"] = (string) null,
          ["CustomerID"] = current.AcctCD
        }, "AR621500", "AR Register", (CurrentLocalization) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CustomerDetails(PXAdapter adapter)
  {
    Customer current = this.BAccountAccessor.Current;
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
        throw new PXReportRequiredException(new Dictionary<string, string>()
        {
          ["CustomerID"] = current.AcctCD
        }, "AR651000", "Customer Profile", (CurrentLocalization) null);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CustomerStatement(PXAdapter adapter)
  {
    Customer current = ((PXSelectBase<Customer>) this.CurrentCustomer).Current;
    if (current == null)
      return adapter.Get();
    this.VerifyCanHaveSeparateStatement(((PXSelectBase<Customer>) this.CurrentCustomer).Current);
    ARStatement arStatement = ((PXSelectBase<ARStatementCycle>) new PXSelect<ARStatementCycle, Where<ARStatementCycle.statementCycleId, Equal<Required<Customer.statementCycleId>>>>((PXGraph) this)).SelectSingle(new object[1]
    {
      (object) current.StatementCycleId
    }) != null ? new ARStatementRepository((PXGraph) this).FindLastStatement(current, includeOnDemand: true) : throw new PXException("Statement Cycle not specified for the Customer.");
    if (arStatement == null)
      throw new PXException("There is no Statement available for the Customer.  Go to Prepare Statement to create a Statement.");
    Dictionary<string, string> dictionary = ARStatementReportParams.FromCustomer(current);
    dictionary["StatementDate"] = Convert.ToString(arStatement.StatementDate.Value.Date, (IFormatProvider) CultureInfo.InvariantCulture);
    throw new PXReportRequiredException(dictionary, ARStatementReportParams.ReportIDForCustomer((PXGraph) this, current, new int?()), "Print Statement", (CurrentLocalization) null);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable SalesPrice(PXAdapter adapter)
  {
    if (((PXSelectBase<Customer>) this.CurrentCustomer).Current != null)
    {
      int? baccountId = ((PXSelectBase<Customer>) this.CurrentCustomer).Current.BAccountID;
      int num = 0;
      if (baccountId.GetValueOrDefault() > num & baccountId.HasValue)
      {
        ARSalesPriceMaint instance = PXGraph.CreateInstance<ARSalesPriceMaint>();
        ((PXSelectBase<ARSalesPriceFilter>) instance.Filter).Current.PriceType = "C";
        ((PXSelectBase<ARSalesPriceFilter>) instance.Filter).Current.PriceCode = ((PXSelectBase<Customer>) this.CurrentCustomer).Current.AcctCD;
        throw new PXRedirectRequiredException((PXGraph) instance, "Sales Prices");
      }
    }
    return adapter.Get();
  }

  [PXDependToCache(new System.Type[] {typeof (Customer), typeof (CuryARHistory), typeof (ARCustomerBalanceEnq)})]
  protected virtual IEnumerable customerBalance()
  {
    Customer current = this.BAccountAccessor.Current;
    List<CustomerMaint.CustomerBalanceSummary> customerBalanceSummaryList = new List<CustomerMaint.CustomerBalanceSummary>(1);
    if (current != null)
    {
      int? baccountId = current.BAccountID;
      long? nullable1 = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
      long num1 = 0;
      if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue && ((PXSelectBase) this.BAccountAccessor).Cache.GetStatus((object) current) != 2)
      {
        CustomerMaint.CustomerBalanceSummary customerBalanceSummary1 = new CustomerMaint.CustomerBalanceSummary();
        CuryARHistory curyArHistory = PXResultset<CuryARHistory>.op_Implicit(PXSelectBase<CuryARHistory, PXSelectJoinGroupBy<CuryARHistory, InnerJoin<ARCustomerBalanceEnq.ARLatestHistory, On<ARCustomerBalanceEnq.ARLatestHistory.accountID, Equal<CuryARHistory.accountID>, And<ARCustomerBalanceEnq.ARLatestHistory.branchID, Equal<CuryARHistory.branchID>, And<ARCustomerBalanceEnq.ARLatestHistory.customerID, Equal<Current<Customer.bAccountID>>, And<ARCustomerBalanceEnq.ARLatestHistory.subID, Equal<CuryARHistory.subID>, And<ARCustomerBalanceEnq.ARLatestHistory.curyID, Equal<CuryARHistory.curyID>, And<ARCustomerBalanceEnq.ARLatestHistory.lastActivityPeriod, Equal<CuryARHistory.finPeriodID>>>>>>>>, Where<CuryARHistory.customerID, Equal<Current<Customer.bAccountID>>>, Aggregate<Sum<CuryARHistory.finYtdDeposits, Sum<CuryARHistory.finYtdRetainageWithheld, Sum<CuryARHistory.finYtdRetainageReleased>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
        ARBalances customerBalances1 = CustomerMaint.GetCustomerBalances<PX.Objects.AR.Override.Customer.sharedCreditCustomerID>((PXGraph) this, current.BAccountID);
        Decimal? nullable2;
        if (PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>())
        {
          ParameterExpression parameterExpression1;
          // ISSUE: method reference
          // ISSUE: method reference
          nullable2 = ((IQueryable<PXResult<CustomerMaint.CustomerBalances>>) PXSelectBase<CustomerMaint.CustomerBalances, PXViewOf<CustomerMaint.CustomerBalances>.BasedOn<SelectFromBase<CustomerMaint.CustomerBalances, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AR.Override.BAccount>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Override.BAccount.bAccountID, Equal<CustomerMaint.CustomerBalances.customerID>>>>, And<BqlOperand<PX.Objects.AR.Override.BAccount.consolidateToParent, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<PX.Objects.AR.Override.BAccount.parentBAccountID, IBqlInt>.IsEqual<P.AsInt>>>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) current.BAccountID
          })).Sum<PXResult<CustomerMaint.CustomerBalances>>(Expression.Lambda<Func<PXResult<CustomerMaint.CustomerBalances>, Decimal?>>((Expression) Expression.Property((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CustomerMaint.CustomerBalances.get_Balance))), parameterExpression1));
          Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
          ParameterExpression parameterExpression2;
          // ISSUE: method reference
          // ISSUE: method reference
          nullable2 = ((IQueryable<PXResult<CustomerMaint.CustomerBalances>>) PXSelectBase<CustomerMaint.CustomerBalances, PXViewOf<CustomerMaint.CustomerBalances>.BasedOn<SelectFromBase<CustomerMaint.CustomerBalances, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CustomerMaint.CustomerBalances.customerID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) current.BAccountID
          })).Sum<PXResult<CustomerMaint.CustomerBalances>>(Expression.Lambda<Func<PXResult<CustomerMaint.CustomerBalances>, Decimal?>>((Expression) Expression.Property((Expression) Expression.Call(_, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CustomerMaint.CustomerBalances.get_Balance))), parameterExpression2));
          Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
          CustomerMaint.CustomerBalances customerBalances2 = PXResultset<CustomerMaint.CustomerBalances>.op_Implicit(PXSelectBase<CustomerMaint.CustomerBalances, PXSelect<CustomerMaint.CustomerBalances, Where<CustomerMaint.CustomerBalances.customerID, Equal<Required<Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) current.BAccountID
          }));
          CustomerMaint.CustomerBalanceSummary customerBalanceSummary2 = customerBalanceSummary1;
          Decimal? nullable3;
          if (customerBalances2 == null)
          {
            nullable2 = new Decimal?();
            nullable3 = nullable2;
          }
          else
            nullable3 = customerBalances2.Balance;
          nullable2 = nullable3;
          Decimal? nullable4 = new Decimal?(nullable2.GetValueOrDefault());
          customerBalanceSummary2.Balance = nullable4;
          customerBalanceSummary1.ConsolidatedBalance = new Decimal?(valueOrDefault2 + valueOrDefault1);
        }
        else
        {
          CustomerMaint.CustomerBalanceSummary customerBalanceSummary3 = customerBalanceSummary1;
          CustomerMaint.CustomerBalanceSummary customerBalanceSummary4 = customerBalanceSummary1;
          Decimal? nullable5 = new Decimal?(((Decimal?) customerBalances1?.CurrentBal).GetValueOrDefault());
          Decimal? nullable6 = nullable5;
          customerBalanceSummary4.Balance = nullable6;
          Decimal? nullable7 = nullable5;
          customerBalanceSummary3.ConsolidatedBalance = nullable7;
        }
        customerBalanceSummary1.UnreleasedBalance = customerBalances1.UnreleasedBal;
        nullable2 = customerBalanceSummary1.UnreleasedBalance;
        if (!nullable2.HasValue)
          ((PXSelectBase) this.CustomerBalance).Cache.SetValueExt<CustomerMaint.CustomerBalances.unreleasedBalance>((object) customerBalanceSummary1, (object) 0.0M);
        customerBalanceSummary1.OpenOrdersBalance = customerBalances1.TotalOpenOrders;
        nullable2 = customerBalanceSummary1.OpenOrdersBalance;
        if (!nullable2.HasValue)
          ((PXSelectBase) this.CustomerBalance).Cache.SetValueExt<CustomerMaint.CustomerBalances.openOrdersBalance>((object) customerBalanceSummary1, (object) 0.0M);
        customerBalanceSummary1.DepositsBalance = curyArHistory.FinYtdDeposits;
        CustomerMaint.CustomerBalanceSummary customerBalanceSummary5 = customerBalanceSummary1;
        nullable2 = curyArHistory.FinYtdRetainageWithheld;
        Decimal? retainageReleased = curyArHistory.FinYtdRetainageReleased;
        Decimal? nullable8 = nullable2.HasValue & retainageReleased.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - retainageReleased.GetValueOrDefault()) : new Decimal?();
        customerBalanceSummary5.RetainageBalance = nullable8;
        if (current.CreditRule == "D" || current.CreditRule == "B")
        {
          customerBalanceSummary1.OldInvoiceDate = customerBalances1.OldInvoiceDate;
          DateTime? nullable9 = ((PXGraph) this).Accessinfo.BusinessDate;
          DateTime dateTime1 = nullable9.Value;
          nullable9 = customerBalances1.OldInvoiceDate;
          DateTime dateTime2 = (nullable9 ?? ((PXGraph) this).Accessinfo.BusinessDate).Value;
          TimeSpan timeSpan = dateTime1 - dateTime2;
          if ((int) current.CreditDaysPastDue.GetValueOrDefault() < timeSpan.Days)
            ((PXSelectBase) this.CustomerBalance).Cache.RaiseExceptionHandling<CustomerMaint.CustomerBalanceSummary.oldInvoiceDate>((object) customerBalanceSummary1, (object) customerBalanceSummary1.OldInvoiceDate, (Exception) new PXSetPropertyException("The customer's Days Past Due number of days has been exceeded!", (PXErrorLevel) 2));
        }
        else
          customerBalanceSummary1.OldInvoiceDate = new DateTime?();
        if (current.CreditRule == "C" || current.CreditRule == "B")
        {
          ARBalances arBalances = customerBalances1;
          if (current.SharedCreditChild.GetValueOrDefault())
            arBalances = CustomerMaint.GetCustomerBalances<PX.Objects.AR.Override.Customer.sharedCreditCustomerID>((PXGraph) this, current.SharedCreditCustomerID);
          CustomerMaint.CustomerBalanceSummary customerBalanceSummary6 = customerBalanceSummary1;
          Decimal? nullable10 = current.CreditLimit;
          nullable2 = arBalances.CurrentBal;
          Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
          nullable2 = arBalances.UnreleasedBal;
          Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
          Decimal num2 = valueOrDefault3 + valueOrDefault4;
          nullable2 = arBalances.TotalOpenOrders;
          Decimal valueOrDefault5 = nullable2.GetValueOrDefault();
          Decimal num3 = num2 + valueOrDefault5;
          nullable2 = arBalances.TotalShipped;
          Decimal valueOrDefault6 = nullable2.GetValueOrDefault();
          Decimal num4 = num3 + valueOrDefault6;
          nullable2 = arBalances.TotalPrepayments;
          Decimal valueOrDefault7 = nullable2.GetValueOrDefault();
          Decimal num5 = num4 - valueOrDefault7;
          Decimal? nullable11;
          if (!nullable10.HasValue)
          {
            nullable2 = new Decimal?();
            nullable11 = nullable2;
          }
          else
            nullable11 = new Decimal?(nullable10.GetValueOrDefault() - num5);
          customerBalanceSummary6.RemainingCreditLimit = nullable11;
          nullable10 = customerBalanceSummary1.RemainingCreditLimit;
          Decimal num6 = 0M;
          if (nullable10.GetValueOrDefault() < num6 & nullable10.HasValue)
            ((PXSelectBase) this.CustomerBalance).Cache.RaiseExceptionHandling<CustomerMaint.CustomerBalanceSummary.remainingCreditLimit>((object) customerBalanceSummary1, (object) customerBalanceSummary1.RemainingCreditLimit, (Exception) new PXSetPropertyException("The customer's credit limit has been exceeded.", (PXErrorLevel) 2));
        }
        else
          customerBalanceSummary1.RemainingCreditLimit = new Decimal?(0M);
        customerBalanceSummaryList.Add(customerBalanceSummary1);
      }
    }
    return (IEnumerable) customerBalanceSummaryList;
  }

  public static ARBalances GetCustomerBalances<ParentField>(PXGraph graph, int? CustomerID) where ParentField : IBqlField
  {
    PXSelectJoinGroupBy<ARBalances, InnerJoin<PX.Objects.AR.Override.Customer, On<PX.Objects.AR.Override.Customer.bAccountID, Equal<ARBalances.customerID>>>, Where<ParentField, Equal<Required<ParentField>>, Or<PX.Objects.AR.Override.Customer.bAccountID, Equal<Required<ARBalances.customerID>>>>, Aggregate<Sum<ARBalances.currentBal, Sum<ARBalances.totalOpenOrders, Sum<ARBalances.totalPrepayments, Sum<ARBalances.totalShipped, Sum<ARBalances.unreleasedBal, Min<ARBalances.oldInvoiceDate>>>>>>>> selectJoinGroupBy = new PXSelectJoinGroupBy<ARBalances, InnerJoin<PX.Objects.AR.Override.Customer, On<PX.Objects.AR.Override.Customer.bAccountID, Equal<ARBalances.customerID>>>, Where<ParentField, Equal<Required<ParentField>>, Or<PX.Objects.AR.Override.Customer.bAccountID, Equal<Required<ARBalances.customerID>>>>, Aggregate<Sum<ARBalances.currentBal, Sum<ARBalances.totalOpenOrders, Sum<ARBalances.totalPrepayments, Sum<ARBalances.totalShipped, Sum<ARBalances.unreleasedBal, Min<ARBalances.oldInvoiceDate>>>>>>>>(graph);
    if (!ForceUseBranchRestrictionsScope.IsRunning)
    {
      using (new PXReadBranchRestrictedScope())
        return PXResultset<ARBalances>.op_Implicit(((PXSelectBase<ARBalances>) selectJoinGroupBy).Select(new object[2]
        {
          (object) CustomerID,
          (object) CustomerID
        }));
    }
    return PXResultset<ARBalances>.op_Implicit(((PXSelectBase<ARBalances>) selectJoinGroupBy).Select(new object[2]
    {
      (object) CustomerID,
      (object) CustomerID
    }));
  }

  [Obsolete]
  public static ARBalances GetCustomerBalances(PXGraph graph, int?[] familyIDs)
  {
    using (new PXReadBranchRestrictedScope())
      return PXResultset<ARBalances>.op_Implicit(PXSelectBase<ARBalances, PXSelectGroupBy<ARBalances, Where<ARBalances.customerID, In<Required<ARBalances.customerID>>>, Aggregate<Sum<ARBalances.currentBal, Sum<ARBalances.totalOpenOrders, Sum<ARBalances.totalPrepayments, Sum<ARBalances.totalShipped, Sum<ARBalances.unreleasedBal, Min<ARBalances.oldInvoiceDate>>>>>>>>.Config>.Select(graph, new object[1]
      {
        (object) familyIDs
      }));
  }

  protected virtual IEnumerable childAccounts() => (IEnumerable) null;

  protected virtual IEnumerable<Customer> GetChildAccounts(
    bool sharedCreditPolicy = false,
    bool consolidateToParent = false,
    bool consolidateStatements = false)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>())
      return Enumerable.Empty<Customer>();
    PXSelectBase<Customer> pxSelectBase = (PXSelectBase<Customer>) new PXSelect<Customer, Where<Customer.parentBAccountID, Equal<Current<Customer.bAccountID>>>>((PXGraph) this);
    if (sharedCreditPolicy)
      pxSelectBase.WhereAnd<Where<Customer.sharedCreditPolicy, Equal<True>>>();
    if (consolidateToParent)
      pxSelectBase.WhereAnd<Where<Customer.consolidateToParent, Equal<True>>>();
    if (consolidateStatements)
      pxSelectBase.WhereAnd<Where<Customer.consolidateStatements, Equal<True>>>();
    return GraphHelper.RowCast<Customer>((IEnumerable) pxSelectBase.Select(Array.Empty<object>()));
  }

  public static bool HasChildren<ParentField>(PXGraph graph, int? customerID) where ParentField : IBqlField
  {
    System.Type type = BqlCommand.Compose(new System.Type[6]
    {
      typeof (Select2<,,>),
      typeof (PX.Objects.AR.Override.Customer),
      typeof (InnerJoin<,>),
      typeof (PX.Objects.AR.Override.BAccount),
      typeof (On<PX.Objects.AR.Override.Customer.bAccountID, Equal<PX.Objects.AR.Override.BAccount.bAccountID>>),
      typeof (Where<ParentField, Equal<Required<ParentField>>>)
    });
    return new PXView(graph, true, BqlCommand.CreateInstance(new System.Type[1]
    {
      type
    })).SelectSingle(new object[1]{ (object) customerID }) != null;
  }

  public static IEnumerable<ExtendedCustomer> GetChildAccountsAndSelfStripped<ParentField>(
    PXGraph graph,
    int? customerID)
    where ParentField : IBqlField
  {
    System.Type type = BqlCommand.Compose(new System.Type[9]
    {
      typeof (Select2<,,>),
      typeof (PX.Objects.AR.Override.Customer),
      typeof (InnerJoin<,>),
      typeof (PX.Objects.AR.Override.BAccount),
      typeof (On<PX.Objects.AR.Override.Customer.bAccountID, Equal<PX.Objects.AR.Override.BAccount.bAccountID>>),
      typeof (Where<,,>),
      CustomerMaint.FetchEmptyGraph().Caches[BqlCommand.GetItemType(typeof (ParentField))].GetBqlField("bAccountID"),
      typeof (Equal<Required<Customer.bAccountID>>),
      typeof (Or<ParentField, Equal<Required<ParentField>>>)
    });
    return new PXView(graph, true, BqlCommand.CreateInstance(new System.Type[1]
    {
      type
    })).SelectMulti(new object[2]
    {
      (object) customerID,
      (object) customerID
    }).Cast<PXResult<PX.Objects.AR.Override.Customer, PX.Objects.AR.Override.BAccount>>().Select<PXResult<PX.Objects.AR.Override.Customer, PX.Objects.AR.Override.BAccount>, ExtendedCustomer>((Func<PXResult<PX.Objects.AR.Override.Customer, PX.Objects.AR.Override.BAccount>, ExtendedCustomer>) (result => new ExtendedCustomer(PXResult<PX.Objects.AR.Override.Customer, PX.Objects.AR.Override.BAccount>.op_Implicit(result), PXResult<PX.Objects.AR.Override.Customer, PX.Objects.AR.Override.BAccount>.op_Implicit(result))));
  }

  private static PXGraph FetchEmptyGraph()
  {
    PXGraph pxGraph = PXContext.GetSlot<PXGraph>(typeof (PX.Objects.AR.Override.Customer).Name);
    if (pxGraph == null)
      PXContext.SetSlot<PXGraph>(typeof (PX.Objects.AR.Override.Customer).Name, pxGraph = PXGraph.CreateInstance<PXGraph>());
    return pxGraph;
  }

  public virtual void Persist()
  {
    if (((PXSelectBase<Customer>) this.CurrentCustomer).Current != null && ((PXSelectBase) this.CurrentCustomer).Cache.GetStatus((object) ((PXSelectBase<Customer>) this.CurrentCustomer).Current) == 1)
    {
      bool flag = false;
      if (((PXSelectBase<Customer>) this.CurrentCustomer).Current != null)
      {
        Customer current = ((PXSelectBase<Customer>) this.CurrentCustomer).Current;
        AccountAndSubValidationHelper validationHelper = new AccountAndSubValidationHelper(((PXSelectBase) this.CurrentCustomer).Cache, (object) current);
        flag |= !validationHelper.SetErrorIfInactiveAccount<Customer.discTakenAcctID>((object) current.DiscTakenAcctID).SetErrorIfInactiveSubAccount<Customer.discTakenSubID>((object) current.DiscTakenSubID).SetErrorIfInactiveAccount<Customer.prepaymentAcctID>((object) current.PrepaymentAcctID).SetErrorIfInactiveSubAccount<Customer.prepaymentSubID>((object) current.PrepaymentSubID).IsValid;
      }
      if (flag)
        throw new PXException("The record cannot be saved because at least one error has occurred. Please review the errors.");
    }
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      try
      {
        this.BAccountRestrictionHelper.Persist();
        ((PXGraph) this).Persist(typeof (Customer), (PXDBOperation) 1);
      }
      catch
      {
        ((PXGraph) this).Caches[typeof (Customer)].Persisted(true);
        throw;
      }
      ((PXGraph) this).Persist();
      Customer current = ((PXSelectBase<Customer>) this.CurrentCustomer).Current;
      if (current != null)
      {
        int? nullable = current.StatementCustomerID;
        if (nullable.HasValue)
        {
          nullable = current.StatementCustomerID;
          int num = 0;
          if (!(nullable.GetValueOrDefault() < num & nullable.HasValue))
            goto label_12;
        }
        PXDatabase.Update<Customer>(new PXDataFieldParam[3]
        {
          (PXDataFieldParam) new PXDataFieldAssign<Customer.statementCustomerID>((object) current.BAccountID),
          (PXDataFieldParam) new PXDataFieldRestrict<Customer.bAccountID>((object) current.BAccountID),
          (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed
        });
        ((PXSelectBase) this.CurrentCustomer).Cache.SetValue<Customer.statementCustomerID>((object) current, (object) current.BAccountID);
        ((PXGraph) this).SelectTimeStamp();
label_12:
        nullable = current.ConsolidatingBAccountID;
        if (nullable.HasValue)
        {
          nullable = current.ConsolidatingBAccountID;
          int num = 0;
          if (!(nullable.GetValueOrDefault() < num & nullable.HasValue))
            goto label_15;
        }
        PXDatabase.Update<PX.Objects.CR.BAccount>(new PXDataFieldParam[3]
        {
          (PXDataFieldParam) new PXDataFieldAssign<PX.Objects.CR.BAccount.consolidatingBAccountID>((object) current.BAccountID),
          (PXDataFieldParam) new PXDataFieldRestrict<PX.Objects.CR.BAccount.bAccountID>((object) current.BAccountID),
          (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed
        });
        ((PXSelectBase) this.CurrentCustomer).Cache.SetValue<Customer.consolidatingBAccountID>((object) current, (object) current.BAccountID);
        ((PXGraph) this).SelectTimeStamp();
label_15:
        nullable = current.SharedCreditCustomerID;
        if (nullable.HasValue)
        {
          nullable = current.SharedCreditCustomerID;
          int num = 0;
          if (!(nullable.GetValueOrDefault() < num & nullable.HasValue))
            goto label_18;
        }
        PXDatabase.Update<Customer>(new PXDataFieldParam[3]
        {
          (PXDataFieldParam) new PXDataFieldAssign<Customer.sharedCreditCustomerID>((object) current.BAccountID),
          (PXDataFieldParam) new PXDataFieldRestrict<Customer.bAccountID>((object) current.BAccountID),
          (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed
        });
        ((PXSelectBase) this.CurrentCustomer).Cache.SetValue<Customer.sharedCreditCustomerID>((object) current, (object) current.BAccountID);
        ((PXGraph) this).SelectTimeStamp();
      }
label_18:
      transactionScope.Complete();
    }
  }

  protected virtual void Customer_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    bool flag = false;
    Customer row = (Customer) e.Row;
    if (!row.DefBillAddressID.HasValue)
    {
      row.DefBillAddressID = row.DefAddressID;
      flag = true;
    }
    if (!row.DefBillContactID.HasValue)
    {
      row.DefBillContactID = row.DefContactID;
      flag = true;
    }
    if (flag)
      ((PXSelectBase) this.BAccountAccessor).Cache.Update((object) row);
    this.CustomerClassDefaultInserting();
  }

  private void CustomerClassDefaultInserting()
  {
    PX.Objects.AR.CustomerClass current = ((PXSelectBase<PX.Objects.AR.CustomerClass>) this.CustomerClass).Current;
    if ((current != null ? (current.SalesPersonID.HasValue ? 1 : 0) : 0) == 0)
      return;
    CustSalesPeople custSalesPeople = new CustSalesPeople()
    {
      SalesPersonID = ((PXSelectBase<PX.Objects.AR.CustomerClass>) this.CustomerClass).Current.SalesPersonID,
      IsDefault = new bool?(true)
    };
    try
    {
      using (new ReadOnlyScope(new PXCache[1]
      {
        ((PXSelectBase) this.SalesPersons).Cache
      }))
        ((PXSelectBase<CustSalesPeople>) this.SalesPersons).Insert(custSalesPeople);
    }
    catch (CustomerMaint.PXCustSalesPersonException ex)
    {
    }
  }

  protected virtual void Customer_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    Customer row = (Customer) e.Row;
    if (row == null)
      return;
    int? nullable1 = row.DefBillContactID;
    if (!nullable1.HasValue)
      row.DefBillContactID = row.DefContactID;
    bool flag1 = cache.GetStatus((object) row) != 2 || row.AcctCD != null;
    if (!flag1)
    {
      ((PXAction) this.newInvoiceMemo).SetEnabled(false);
      ((PXAction) this.newSalesOrder).SetEnabled(false);
      ((PXAction) this.newPayment).SetEnabled(false);
      ((PXAction) this.regenerateLastStatement).SetEnabled(false);
      ((PXAction) this.customerDocuments).SetEnabled(false);
      ((PXAction) this.statementForCustomer).SetEnabled(false);
      ((PXAction) this.salesPrice).SetEnabled(false);
      ((PXAction) this.aRBalanceByCustomer).SetEnabled(false);
      ((PXAction) this.customerHistory).SetEnabled(false);
      ((PXAction) this.aRAgedPastDue).SetEnabled(false);
      ((PXAction) this.aRAgedOutstanding).SetEnabled(false);
      ((PXAction) this.aRRegister).SetEnabled(false);
      ((PXAction) this.customerDetails).SetEnabled(false);
      ((PXAction) this.customerStatement).SetEnabled(false);
      ((PXAction) this.viewRestrictionGroups).SetEnabled(false);
      ((PXAction) this.viewBusnessAccount).SetEnabled(false);
      ((PXAction) this.generateOnDemandStatement).SetEnabled(false);
    }
    if (flag1 && row.Status == "A")
    {
      ((PXAction) this.newInvoiceMemo).SetEnabled(true);
      ((PXAction) this.newSalesOrder).SetEnabled(true);
      ((PXAction) this.newPayment).SetEnabled(true);
      ((PXAction) this.regenerateLastStatement).SetEnabled(true);
      ((PXAction) this.customerDocuments).SetEnabled(true);
      ((PXAction) this.statementForCustomer).SetEnabled(true);
      ((PXAction) this.salesPrice).SetEnabled(true);
      ((PXAction) this.aRBalanceByCustomer).SetEnabled(true);
      ((PXAction) this.customerHistory).SetEnabled(true);
      ((PXAction) this.aRAgedPastDue).SetEnabled(true);
      ((PXAction) this.aRAgedOutstanding).SetEnabled(true);
      ((PXAction) this.aRRegister).SetEnabled(true);
      ((PXAction) this.customerDetails).SetEnabled(true);
      ((PXAction) this.customerStatement).SetEnabled(true);
      ((PXAction) this.viewRestrictionGroups).SetEnabled(true);
      ((PXAction) this.viewBusnessAccount).SetEnabled(true);
      ((PXAction) this.generateOnDemandStatement).SetEnabled(true);
    }
    PXChangeBAccountID<Customer, Customer.acctCD> changeId = this.ChangeID;
    bool? nullable2;
    int num1;
    if (flag1)
    {
      nullable2 = row.IsBranch;
      num1 = !nullable2.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    ((PXAction) changeId).SetEnabled(num1 != 0);
    nullable2 = row.SmallBalanceAllow;
    bool valueOrDefault1 = nullable2.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<Customer.smallBalanceLimit>(cache, (object) row, valueOrDefault1);
    if (!valueOrDefault1)
      row.SmallBalanceLimit = new Decimal?(0M);
    bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();
    PXUIFieldAttribute.SetVisible<Customer.curyID>(cache, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<Customer.curyRateTypeID>(cache, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<Customer.printCuryStatements>(cache, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<Customer.allowOverrideCury>(cache, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<Customer.allowOverrideRate>(cache, (object) null, flag2);
    bool flag3 = PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>() && CustomerMaint.HasChildren<PX.Objects.AR.Override.BAccount.parentBAccountID>((PXGraph) this, row.BAccountID);
    ((PXSelectBase) this.ChildAccounts).AllowSelect = flag3;
    ((PXSelectBase) this.ChildAccounts).AllowInsert = false;
    ((PXSelectBase) this.ChildAccounts).AllowUpdate = false;
    ((PXSelectBase) this.ChildAccounts).AllowDelete = false;
    nullable1 = row.ParentBAccountID;
    bool flag4 = nullable1.HasValue & flag3;
    PXCache pxCache1 = cache;
    Customer customer1 = row;
    nullable1 = row.ParentBAccountID;
    int num2 = nullable1.HasValue ? 1 : (!flag3 ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<Customer.parentBAccountID>(pxCache1, (object) customer1, num2 != 0);
    PXUIFieldAttribute.SetEnabled<Customer.consolidateToParent>(cache, (object) row, !flag4);
    PXUIFieldAttribute.SetEnabled<Customer.consolidateStatements>(cache, (object) row, !flag4);
    PXUIFieldAttribute.SetVisible<CustomerMaint.CustomerBalanceSummary.consolidatedbalance>(((PXSelectBase) this.CustomerBalance).Cache, (object) null, flag3);
    PXUIFieldAttribute.SetVisible<CustomerMaint.CustomerBalanceSummary.signedDepositsBalance>(((PXSelectBase) this.CustomerBalance).Cache, (object) null, !flag3);
    bool flag5 = row.CreditRule == "B";
    PXCache pxCache2 = cache;
    Customer customer2 = row;
    nullable2 = row.ConsolidateToParent;
    int num3 = nullable2.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<Customer.sharedCreditPolicy>(pxCache2, (object) customer2, num3 != 0);
    PXCache pxCache3 = cache;
    Customer customer3 = row;
    nullable2 = row.SharedCreditChild;
    int num4 = !nullable2.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<Customer.creditRule>(pxCache3, (object) customer3, num4 != 0);
    PXCache pxCache4 = cache;
    Customer customer4 = row;
    nullable2 = row.SharedCreditChild;
    int num5 = nullable2.GetValueOrDefault() ? 0 : (row.CreditRule == "C" | flag5 ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<Customer.creditLimit>(pxCache4, (object) customer4, num5 != 0);
    PXCache pxCache5 = cache;
    Customer customer5 = row;
    nullable2 = row.SharedCreditChild;
    int num6 = nullable2.GetValueOrDefault() ? 0 : (row.CreditRule == "D" | flag5 ? 1 : 0);
    PXUIFieldAttribute.SetEnabled<Customer.creditDaysPastDue>(pxCache5, (object) customer5, num6 != 0);
    PXCache pxCache6 = cache;
    Customer customer6 = row;
    nullable2 = row.SharedCreditChild;
    int num7 = !nullable2.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<Customer.printDunningLetters>(pxCache6, (object) customer6, num7 != 0);
    PXCache pxCache7 = cache;
    Customer customer7 = row;
    nullable2 = row.SharedCreditChild;
    int num8 = !nullable2.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<Customer.mailDunningLetters>(pxCache7, (object) customer7, num8 != 0);
    PXCache pxCache8 = cache;
    Customer customer8 = row;
    nullable2 = row.StatementChild;
    int num9 = !nullable2.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<Customer.sendStatementByEmail>(pxCache8, (object) customer8, num9 != 0);
    PXCache pxCache9 = cache;
    Customer customer9 = row;
    nullable2 = row.StatementChild;
    int num10 = !nullable2.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<Customer.printStatements>(pxCache9, (object) customer9, num10 != 0);
    PXCache pxCache10 = cache;
    Customer customer10 = row;
    nullable2 = row.StatementChild;
    int num11 = !nullable2.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<Customer.statementType>(pxCache10, (object) customer10, num11 != 0);
    PXCache pxCache11 = cache;
    Customer customer11 = row;
    nullable2 = row.StatementChild;
    int num12 = !nullable2.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<Customer.printCuryStatements>(pxCache11, (object) customer11, num12 != 0);
    PXCache cache1 = ((PXSelectBase) this.BAccount).Cache;
    Customer customer12 = row;
    int num13;
    if (PXAccess.FeatureInstalled<FeaturesSet.interBranch>())
    {
      nullable2 = row.IsBranch;
      num13 = nullable2.GetValueOrDefault() ? 1 : 0;
    }
    else
      num13 = 0;
    PXUIFieldAttribute.SetVisible<Customer.cOGSAcctID>(cache1, (object) customer12, num13 != 0);
    nullable2 = row.RetainageApply;
    bool valueOrDefault2 = nullable2.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<Customer.retainagePct>(cache, (object) row, valueOrDefault2);
    PXAction<Customer> writeOffBalance = this.writeOffBalance;
    nullable2 = row.SmallBalanceAllow;
    int num14 = !(nullable2.GetValueOrDefault() & flag1) || !(row.Status != "H") ? 0 : (row.Status != "I" ? 1 : 0);
    ((PXAction) writeOffBalance).SetEnabled(num14 != 0);
    nullable1 = row.ParentBAccountID;
    if (!nullable1.HasValue)
      return;
    PX.Objects.AR.Override.Customer customer13 = PXResultset<PX.Objects.AR.Override.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Override.Customer, PXSelect<PX.Objects.AR.Override.Customer, Where<PX.Objects.AR.Override.Customer.bAccountID, Equal<Required<PX.Objects.AR.Override.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ParentBAccountID
    }));
    PXCache pxCache12 = cache;
    Customer customer14 = row;
    // ISSUE: variable of a boxed type
    __Boxed<int?> parentBaccountId = (ValueType) row.ParentBAccountID;
    PXSetPropertyException<Customer.statementCycleId> propertyException;
    if (customer13 != null)
    {
      nullable2 = row.ConsolidateToParent;
      if (nullable2.GetValueOrDefault() && customer13.StatementCycleId != row.StatementCycleId)
      {
        propertyException = new PXSetPropertyException<Customer.statementCycleId>("We recommend setting the same statement cycle as for the parent account.", (PXErrorLevel) 2);
        goto label_21;
      }
    }
    propertyException = (PXSetPropertyException<Customer.statementCycleId>) null;
label_21:
    pxCache12.RaiseExceptionHandling<Customer.statementCycleId>((object) customer14, (object) parentBaccountId, (Exception) propertyException);
  }

  [PXMergeAttributes]
  [PXExcludeRowsFromReferentialIntegrityCheck(ForeignTableExcludingConditions = typeof (ExcludeWhen<BAccount2>.Joined<On<BqlOperand<BAccount2.bAccountID, IBqlInt>.IsEqual<PX.Objects.CR.BAccount.parentBAccountID>>>.Satisfies<Where<BqlOperand<BAccount2.isBranch, IBqlBool>.IsEqual<True>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.BAccount.parentBAccountID> e)
  {
  }

  protected virtual void Customer_RowDeleting(PXCache cache, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is Customer row) || !(row.Type == "VC") && !row.IsBranch.GetValueOrDefault())
      return;
    (cache.Interceptor as PXTableAttribute).BypassOnDelete(new System.Type[1]
    {
      typeof (PX.Objects.CR.BAccount)
    });
    PXNoteAttribute.ForceRetain<Customer.noteID>(cache);
  }

  protected virtual void Customer_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    if (!(e.Row is Customer row))
      return;
    string type = (string) null;
    if (row.Type == "VC")
      type = "VE";
    else if (row.Type == "CU" && row.IsBranch.GetValueOrDefault())
      type = "CP";
    if (string.IsNullOrEmpty(type))
      return;
    this.ChangeBAccountType((PX.Objects.CR.BAccount) row, type);
  }

  protected virtual void Customer_RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    Customer row = (Customer) e.Row;
    if (row == null || e.Operation == 3)
      return;
    bool? nullable = row.SendStatementByEmail;
    if (!nullable.GetValueOrDefault())
    {
      nullable = row.MailInvoices;
      if (!nullable.GetValueOrDefault())
      {
        nullable = row.MailDunningLetters;
        if (!nullable.GetValueOrDefault() || !PXAccess.FeatureInstalled<FeaturesSet.dunningLetter>())
          goto label_6;
      }
    }
    PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.bAccountID, Equal<Required<Customer.bAccountID>>, And<PX.Objects.CR.Contact.contactID, Equal<Required<Customer.defBillContactID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.BAccountID,
      (object) row.DefBillContactID
    }));
    PXCache cache1 = ((PXSelectBase) this.BillContact).Cache;
    if (contact != null && string.IsNullOrEmpty(contact.EMail))
    {
      GraphHelper.MarkUpdated(cache1, (object) contact);
      this.RaiseEmailErrors(cache, cache1, contact, row);
    }
label_6:
    int? parentBaccountId1 = row.ParentBAccountID;
    if (!parentBaccountId1.HasValue)
    {
      nullable = row.ConsolidateToParent;
      if (!nullable.GetValueOrDefault())
      {
        nullable = row.SharedCreditPolicy;
        if (nullable.GetValueOrDefault())
          row.SharedCreditPolicy = new bool?(false);
      }
    }
    parentBaccountId1 = row.ParentBAccountID;
    if (parentBaccountId1.HasValue)
    {
      nullable = row.ConsolidateToParent;
      if (!nullable.GetValueOrDefault())
      {
        nullable = row.ConsolidateStatements;
        if (!nullable.GetValueOrDefault())
          goto label_19;
      }
      PX.Objects.CR.BAccount baccount = (PX.Objects.CR.BAccount) PXSelectorAttribute.Select<Customer.parentBAccountID>(cache, (object) row);
      if (baccount != null)
      {
        nullable = baccount.IsCustomerOrCombined;
        if (!nullable.GetValueOrDefault())
          cache.RaiseExceptionHandling<Customer.parentBAccountID>((object) row, cache.GetValueExt<Customer.parentBAccountID>((object) row), (Exception) new PXSetPropertyException<Customer.parentBAccountID>("If either or both of the Consolidate Balance and Consolidate Statements check boxes are selected, only an account of the Customer or Customer & Vendor type can be used as a parent account."));
      }
      if (baccount != null)
      {
        parentBaccountId1 = baccount.ParentBAccountID;
        if (parentBaccountId1.HasValue)
          cache.RaiseExceptionHandling<Customer.parentBAccountID>((object) row, cache.GetValueExt<Customer.parentBAccountID>((object) row), (Exception) new PXSetPropertyException<Customer.parentBAccountID>("If either or both of the Consolidate Balance and Consolidate Statements check boxes are selected, only a customer account that has no parent account assigned can be specified as a parent account."));
      }
    }
label_19:
    parentBaccountId1 = row.ParentBAccountID;
    if (parentBaccountId1.HasValue)
    {
      nullable = row.ConsolidateToParent;
      if (nullable.GetValueOrDefault())
      {
        nullable = row.ConsolidateStatements;
        if (!nullable.GetValueOrDefault() && row.StatementType == "B")
          cache.RaiseExceptionHandling<Customer.consolidateStatements>((object) row, (object) row.ConsolidateStatements, (Exception) new PXSetPropertyException<Customer.consolidateStatements>("Child accounts that consolidate balance to parent and use Balance Brought Forward statement type must consolidate statements as well."));
      }
    }
    Customer customer1 = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelectReadonly<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.BAccountID
    }));
    if (customer1 != null)
    {
      parentBaccountId1 = customer1.ParentBAccountID;
      if (parentBaccountId1.HasValue)
      {
        nullable = customer1.ConsolidateToParent;
        if (nullable.GetValueOrDefault() && !cache.ObjectsEqual<Customer.consolidateToParent, Customer.parentBAccountID>((object) row, (object) customer1))
          this.VerifyUnreleasedParentChildApplications(cache, row);
      }
    }
    if (customer1 != null)
    {
      parentBaccountId1 = customer1.ParentBAccountID;
      if (parentBaccountId1.HasValue)
      {
        parentBaccountId1 = customer1.ParentBAccountID;
        int? parentBaccountId2 = row.ParentBAccountID;
        if (!(parentBaccountId1.GetValueOrDefault() == parentBaccountId2.GetValueOrDefault() & parentBaccountId1.HasValue == parentBaccountId2.HasValue))
        {
          nullable = customer1.ConsolidateStatements;
          if (nullable.GetValueOrDefault())
          {
            nullable = customer1.ConsolidateToParent;
            if (nullable.GetValueOrDefault() && string.IsNullOrEmpty(PXUIFieldAttribute.GetError<Customer.parentBAccountID>(cache, (object) row)))
            {
              Customer customer2 = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelectReadonly<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
              {
                (object) customer1.ParentBAccountID
              }));
              Customer customer3 = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelectReadonly<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
              {
                (object) row.ParentBAccountID
              }));
              if (customer2 != null && customer2.StatementType == "B" || customer3 != null && customer3.StatementType == "B" || row.StatementType == "B")
                cache.RaiseExceptionHandling<Customer.parentBAccountID>((object) row, (object) row.StatementType, (Exception) new PXSetPropertyException<Customer.parentBAccountID>("We recommend switching to Open Item statement type for both parent and child accounts.", (PXErrorLevel) 2));
            }
          }
        }
      }
    }
    CustomerMaint.VerifyParentBAccountID<Customer.parentBAccountID>((PXGraph) this, cache, row, (PX.Objects.CR.BAccount) row);
  }

  public static void VerifyParentBAccountID<ParentField>(
    PXGraph graph,
    PXCache cache,
    Customer customer,
    PX.Objects.CR.BAccount row)
    where ParentField : IBqlField
  {
    if (customer == null)
      return;
    int? parentBaccountId = row.ParentBAccountID;
    int? baccountId = row.BAccountID;
    if (parentBaccountId.GetValueOrDefault() == baccountId.GetValueOrDefault() & parentBaccountId.HasValue == baccountId.HasValue || !row.ConsolidateToParent.GetValueOrDefault() && !customer.ConsolidateStatements.GetValueOrDefault())
      return;
    PXResultset<Customer, CustomerMaster> pxResultset = PXSelectBase<Customer, PXViewOf<Customer>.BasedOn<SelectFromBase<Customer, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<CustomerMaster>.On<BqlOperand<Customer.parentBAccountID, IBqlInt>.IsEqual<CustomerMaster.bAccountID>>>>.Where<BqlOperand<Customer.bAccountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectWindowed<PXResultset<Customer, CustomerMaster>>(graph, 0, 1, new object[1]
    {
      (object) row.ParentBAccountID
    });
    Customer customer1 = PXResultset<Customer, CustomerMaster>.op_Implicit(pxResultset);
    CustomerMaster customerMaster = PXResultset<Customer, CustomerMaster>.op_Implicit(pxResultset);
    PXSetPropertyException propertyException = (PXSetPropertyException) null;
    if (customer1 != null && (customer1.ConsolidateToParent.GetValueOrDefault() || customer1.ConsolidateStatements.GetValueOrDefault()) && customerMaster != null)
    {
      baccountId = customerMaster.BAccountID;
      if (baccountId.HasValue)
      {
        propertyException = new PXSetPropertyException("{0} cannot be selected as the parent account for {1}, because {0} already has its parent account and balance consolidation is enabled for {0} in the system.", (PXErrorLevel) 5, new object[2]
        {
          (object) customer1.AcctCD,
          (object) customer.AcctCD
        });
        goto label_8;
      }
    }
    if (customer1 != null)
    {
      if (((IQueryable<PXResult<Customer>>) PXSelectBase<Customer, PXViewOf<Customer>.BasedOn<SelectFromBase<Customer, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Customer.parentBAccountID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Customer.consolidateToParent, Equal<True>>>>>.Or<BqlOperand<Customer.consolidateStatements, IBqlBool>.IsEqual<True>>>>>.Config>.SelectWindowed(graph, 0, 1, new object[1]
      {
        (object) customer.BAccountID
      })).Any<PXResult<Customer>>())
        propertyException = new PXSetPropertyException("{0} cannot be selected as the parent account for {1}, because {1} is associated with child accounts for which balance consolidation is enabled in the system.", (PXErrorLevel) 5, new object[2]
        {
          (object) customer1.AcctCD,
          (object) customer.AcctCD
        });
    }
label_8:
    if (propertyException == null)
      return;
    cache.RaiseExceptionHandling<ParentField>((object) row, (object) customer.ParentBAccountID, (Exception) propertyException);
  }

  private void VerifyUnreleasedParentChildApplications(PXCache cache, Customer customer)
  {
    IEnumerable<ARAdjust> source = GraphHelper.RowCast<ARAdjust>((IEnumerable) PXSelectBase<ARAdjust, PXSelectGroupBy<ARAdjust, Where<ARAdjust.adjdCustomerID, Equal<Required<ARAdjust.adjdCustomerID>>, And<ARAdjust.adjdCustomerID, NotEqual<ARAdjust.customerID>, And<ARAdjust.released, NotEqual<True>, And<ARAdjust.voided, NotEqual<True>>>>>, Aggregate<GroupBy<ARAdjust.adjgDocType, GroupBy<ARAdjust.adjgRefNbr>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) customer.BAccountID
    }));
    if (!source.Any<ARAdjust>())
      return;
    string str = string.Join(", ", source.Select<ARAdjust, string>((Func<ARAdjust, string>) (a => $"{PXMessages.LocalizeNoPrefix(a.AdjgDocType)} {a.AdjgRefNbr}")).ToArray<string>());
    cache.RaiseExceptionHandling<Customer.parentBAccountID>((object) customer, cache.GetValueExt<Customer.parentBAccountID>((object) customer), (Exception) new PXSetPropertyException<Customer.parentBAccountID>("Unreleased parent-child applications exist for this customer. Neither Parent Account nor the Consolidate Balance option can be changed until these are released or deleted. Check the following documents: {0}", new object[1]
    {
      (object) str
    }));
  }

  private void RaiseEmailErrors(
    PXCache persistingCache,
    PXCache contactCache,
    PX.Objects.CR.Contact contact,
    Customer customer)
  {
    bool flag = true;
    string displayName1 = PXUIFieldAttribute.GetDisplayName<Customer.sendStatementByEmail>(persistingCache);
    string displayName2 = PXUIFieldAttribute.GetDisplayName<Customer.mailInvoices>(persistingCache);
    string displayName3 = PXUIFieldAttribute.GetDisplayName<Customer.mailDunningLetters>(persistingCache);
    string str1;
    if (!PXAccess.FeatureInstalled<FeaturesSet.dunningLetter>())
      str1 = string.Join(", ", displayName1, displayName2);
    else
      str1 = string.Join(", ", displayName1, displayName2, displayName3);
    string str2 = str1;
    bool? nullable = customer.MailInvoices;
    if (nullable.GetValueOrDefault())
    {
      flag = false;
      persistingCache.RaiseExceptionHandling<Customer.mailInvoices>((object) customer, (object) customer.MailInvoices, (Exception) new PXSetPropertyException("Email address must be specified if '{0}' option is activated.", (PXErrorLevel) 4, new object[1]
      {
        (object) displayName2
      }));
    }
    nullable = customer.StatementChild;
    if (!nullable.GetValueOrDefault())
    {
      nullable = customer.SendStatementByEmail;
      if (nullable.GetValueOrDefault())
      {
        flag = false;
        persistingCache.RaiseExceptionHandling<Customer.sendStatementByEmail>((object) customer, (object) customer.SendStatementByEmail, (Exception) new PXSetPropertyException("Email address must be specified if '{0}' option is activated.", (PXErrorLevel) 4, new object[1]
        {
          (object) displayName1
        }));
      }
    }
    nullable = customer.SharedCreditChild;
    if (!nullable.GetValueOrDefault())
    {
      nullable = customer.MailDunningLetters;
      if (nullable.GetValueOrDefault() && PXAccess.FeatureInstalled<FeaturesSet.dunningLetter>())
      {
        flag = false;
        persistingCache.RaiseExceptionHandling<Customer.mailDunningLetters>((object) customer, (object) customer.MailDunningLetters, (Exception) new PXSetPropertyException("Email address must be specified if '{0}' option is activated.", (PXErrorLevel) 4, new object[1]
        {
          (object) displayName3
        }));
      }
    }
    if (flag)
      return;
    contactCache.RaiseExceptionHandling<PX.Objects.CR.Contact.eMail>((object) contact, (object) contact.EMail, (Exception) new PXSetPropertyException("Email address must be specified if any of the following options is activated: {0}.", (PXErrorLevel) 4, new object[1]
    {
      (object) str2
    }));
  }

  protected virtual void Customer_MailDunningLetters_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    Customer row = (Customer) e.Row;
    CustomerMaint.CheckExcludedFromDunning(cache, row);
  }

  protected virtual void Customer_PrintDunningLetters_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    Customer row = (Customer) e.Row;
    CustomerMaint.CheckExcludedFromDunning(cache, row);
  }

  protected virtual void Customer_MailDunningLetters_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    Customer row = (Customer) e.Row;
    CustomerMaint.CheckExcludedFromDunning(cache, row);
    this.UpdateChildAccounts<Customer.mailDunningLetters>(cache, row, this.GetChildAccounts(true));
  }

  protected virtual void Customer_PrintDunningLetters_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    Customer row = (Customer) e.Row;
    CustomerMaint.CheckExcludedFromDunning(cache, row);
    this.UpdateChildAccounts<Customer.printDunningLetters>(cache, row, this.GetChildAccounts(true));
  }

  private static void CheckExcludedFromDunning(PXCache cache, Customer row)
  {
    if (row == null)
      return;
    bool? mailDunningLetters = row.MailDunningLetters;
    bool flag1 = false;
    if (!(mailDunningLetters.GetValueOrDefault() == flag1 & mailDunningLetters.HasValue))
      return;
    bool? printDunningLetters = row.PrintDunningLetters;
    bool flag2 = false;
    if (!(printDunningLetters.GetValueOrDefault() == flag2 & printDunningLetters.HasValue) || !PXAccess.FeatureInstalled<FeaturesSet.dunningLetter>() || row.SharedCreditChild.GetValueOrDefault())
      return;
    cache.RaiseExceptionHandling<Customer.mailDunningLetters>((object) row, (object) row.MailDunningLetters, (Exception) new PXSetPropertyException("The Customer will be excluded from Dunning Letter Process if both Print and Send by Email check boxes are cleared.", (PXErrorLevel) 2));
    cache.RaiseExceptionHandling<Customer.printDunningLetters>((object) row, (object) row.PrintDunningLetters, (Exception) new PXSetPropertyException("The Customer will be excluded from Dunning Letter Process if both Print and Send by Email check boxes are cleared.", (PXErrorLevel) 2));
  }

  protected virtual void Customer_SendStatementByEmail_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    this.UpdateChildAccounts<Customer.sendStatementByEmail>(cache, (Customer) e.Row, this.GetChildAccounts(consolidateStatements: true));
  }

  protected virtual void Customer_PrintStatements_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    this.UpdateChildAccounts<Customer.printStatements>(cache, (Customer) e.Row, this.GetChildAccounts(consolidateStatements: true));
  }

  protected virtual void Customer_StatementType_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    this.UpdateChildAccounts<Customer.statementType>(cache, (Customer) e.Row, this.GetChildAccounts(consolidateStatements: true));
  }

  protected virtual void Customer_PrintCuryStatements_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    this.UpdateChildAccounts<Customer.printCuryStatements>(cache, (Customer) e.Row, this.GetChildAccounts(consolidateStatements: true));
  }

  protected virtual void Customer_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    if (((PXSelectBase<Company>) this.cmpany).Current == null || string.IsNullOrEmpty(((PXSelectBase<Company>) this.cmpany).Current.BaseCuryID))
      throw new PXException();
    e.NewValue = (object) ((PXSelectBase<Company>) this.cmpany).Current.BaseCuryID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void Customer_CuryRateTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void Customer_CuryRateTypeID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void Customer_ParentBAccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    Customer row = (Customer) e.Row;
    if (row.ParentBAccountID.HasValue)
    {
      Customer customerParent = this.GetCustomerParent(row);
      if (customerParent == null)
        return;
      sender.SetValueExt<Customer.consolidateToParent>((object) row, (object) customerParent.ConsolidateToParent);
      sender.SetValueExt<Customer.consolidateStatements>((object) row, (object) customerParent.ConsolidateStatements);
      sender.SetValueExt<Customer.sharedCreditPolicy>((object) row, (object) customerParent.SharedCreditPolicy);
    }
    else
    {
      if (e.OldValue == null)
        return;
      row.ConsolidateToParent = new bool?(false);
      row.ConsolidateStatements = new bool?(false);
      row.SharedCreditPolicy = new bool?(false);
      row.CreditLimit = new Decimal?(0M);
    }
  }

  protected virtual void Customer_ConsolidateToParent_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    Customer row = (Customer) e.Row;
    if (row == null)
      return;
    if (!row.ParentBAccountID.HasValue)
    {
      string str = PXMessages.LocalizeFormatNoPrefix("Do you wish to update the {0} setting for all child accounts of this customer?", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<Customer.consolidateToParent>(sender)
      });
      IEnumerable<Customer> childAccounts;
      if ((childAccounts = this.GetChildAccounts()).Any<Customer>() && e.ExternalCall && ((PXSelectBase<Customer>) this.CurrentCustomer).Ask(str, (MessageButtons) 4) == 6)
        this.UpdateChildAccounts<Customer.consolidateToParent>(sender, row, childAccounts);
      Customer customer = row;
      bool? sharedCreditPolicy = customer.SharedCreditPolicy;
      bool? consolidateToParent = row.ConsolidateToParent;
      customer.SharedCreditPolicy = sharedCreditPolicy.GetValueOrDefault() || !consolidateToParent.GetValueOrDefault() && !sharedCreditPolicy.HasValue ? consolidateToParent : sharedCreditPolicy;
    }
    else
    {
      bool? nullable = row.SharedCreditPolicy;
      if (!nullable.GetValueOrDefault())
        return;
      nullable = row.ConsolidateToParent;
      if (nullable.GetValueOrDefault())
        return;
      nullable = (bool?) e.OldValue;
      if (!nullable.GetValueOrDefault())
        return;
      sender.SetValueExt<Customer.sharedCreditPolicy>((object) row, (object) false);
    }
  }

  protected virtual void Customer_ConsolidateStatements_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    Customer row = (Customer) e.Row;
    if (row == null)
      return;
    if (!row.ParentBAccountID.HasValue)
    {
      string str = PXMessages.LocalizeFormatNoPrefix("Do you wish to update the {0} setting for all child accounts of this customer?", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<Customer.consolidateStatements>(sender)
      });
      IEnumerable<Customer> childAccounts;
      if (!(childAccounts = this.GetChildAccounts()).Any<Customer>() || !e.ExternalCall || ((PXSelectBase<Customer>) this.CurrentCustomer).Ask(str, (MessageButtons) 4) != 6)
        return;
      this.UpdateChildAccounts<Customer.consolidateStatements>(sender, row, childAccounts);
    }
    else
    {
      if (!row.ConsolidateStatements.GetValueOrDefault())
        return;
      Customer customerParent = this.GetCustomerParent(row);
      if (customerParent == null)
        return;
      row.SendStatementByEmail = customerParent.SendStatementByEmail;
      row.PrintStatements = customerParent.PrintStatements;
      row.StatementType = customerParent.StatementType;
      row.PrintCuryStatements = customerParent.PrintCuryStatements;
    }
  }

  protected virtual void Customer_SharedCreditPolicy_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    Customer row = (Customer) e.Row;
    if (row == null)
      return;
    if (!row.ParentBAccountID.HasValue)
    {
      string str = PXMessages.LocalizeFormatNoPrefix("Do you wish to update the {0} setting for all child accounts of this customer?", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<Customer.sharedCreditPolicy>(sender)
      });
      IEnumerable<Customer> childAccounts;
      if (!(childAccounts = this.GetChildAccounts(consolidateToParent: true)).Any<Customer>() || !e.ExternalCall || ((PXSelectBase<Customer>) this.CurrentCustomer).Ask(str, (MessageButtons) 4) != 6)
        return;
      this.UpdateChildAccounts<Customer.sharedCreditPolicy>(sender, row, childAccounts);
    }
    else
    {
      bool? nullable = row.SharedCreditPolicy;
      if (nullable.GetValueOrDefault())
      {
        Customer customerParent = this.GetCustomerParent(row);
        if (customerParent == null)
          return;
        Func<Customer, bool> func;
        string creditChildStatus = this.GetSharedCreditChildStatus(customerParent.Status, out func);
        row.Status = func(row) ? creditChildStatus : row.Status;
        row.CreditRule = customerParent.CreditRule;
        row.CreditLimit = customerParent.CreditLimit;
        row.CreditDaysPastDue = customerParent.CreditDaysPastDue;
        row.PrintDunningLetters = customerParent.PrintDunningLetters;
        row.MailDunningLetters = customerParent.MailDunningLetters;
      }
      else
      {
        nullable = (bool?) e.OldValue;
        if (!nullable.GetValueOrDefault())
          return;
        row.CreditLimit = new Decimal?(0M);
      }
    }
  }

  protected virtual void Customer_CustomerClassID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    Customer row = (Customer) e.Row;
    if (row == null)
      return;
    PX.Objects.AR.CustomerClass customerClass = (PX.Objects.AR.CustomerClass) PXSelectorAttribute.Select<Customer.customerClassID>(cache, (object) row, e.NewValue);
    this.doCopyClassSettings = false;
    if (customerClass == null)
      return;
    this.doCopyClassSettings = true;
    if (cache.GetStatus((object) row) == 2 || ((PXSelectBase<Customer>) this.BAccount).Ask("Warning", "The customer class will be changed. Click Yes if you want to replace the customer settings with the default settings provided by the selected customer class. Click No if you want to keep the original customer settings.", (MessageButtons) 4) != 7)
      return;
    this.doCopyClassSettings = false;
  }

  protected virtual void Customer_CustomerClassID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    Customer row = (Customer) e.Row;
    if (row == null)
      return;
    CustomerMaint.DefLocationExt extension1 = ((PXGraph) this).GetExtension<CustomerMaint.DefLocationExt>();
    ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension1.DefLocation).Current = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension1.DefLocation).Select(Array.Empty<object>()));
    this.CustomerClass.RaiseFieldUpdated(cache, e.Row);
    if (((PXSelectBase<PX.Objects.AR.CustomerClass>) this.CustomerClass).Current != null && ((PXSelectBase<PX.Objects.AR.CustomerClass>) this.CustomerClass).Current.DefaultLocationCDFromBranch.GetValueOrDefault())
    {
      PX.SM.Branch branch = PXResultset<PX.SM.Branch>.op_Implicit(PXSelectBase<PX.SM.Branch, PXSelect<PX.SM.Branch, Where<PX.SM.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (branch != null && ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension1.DefLocation).Current != null && ((PXSelectBase) extension1.DefLocation).Cache.GetStatus((object) ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension1.DefLocation).Current) == 2)
      {
        object branchCd = (object) branch.BranchCD;
        ((PXSelectBase) extension1.DefLocation).Cache.RaiseFieldUpdating<PX.Objects.CR.Standalone.Location.locationCD>((object) ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension1.DefLocation).Current, ref branchCd);
        ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension1.DefLocation).Current.LocationCD = (string) branchCd;
        ((PXSelectBase) extension1.DefLocation).Cache.Normalize();
      }
    }
    CustomerMaint.DefContactAddressExt extension2 = ((PXGraph) this).GetExtension<CustomerMaint.DefContactAddressExt>();
    ((PXSelectBase<PX.Objects.CR.Address>) extension2.DefAddress).Current = PXResultset<PX.Objects.CR.Address>.op_Implicit(((PXSelectBase<PX.Objects.CR.Address>) extension2.DefAddress).Select(Array.Empty<object>()));
    if (((PXSelectBase<PX.Objects.CR.Address>) extension2.DefAddress).Current != null && ((PXSelectBase<PX.Objects.CR.Address>) extension2.DefAddress).Current.AddressID.HasValue)
    {
      extension2.InitDefAddress(((PXSelectBase<PX.Objects.CR.Address>) extension2.DefAddress).Current);
      GraphHelper.MarkUpdated(((PXSelectBase) extension2.DefAddress).Cache, (object) ((PXSelectBase<PX.Objects.CR.Address>) extension2.DefAddress).Current);
    }
    if (!this.doCopyClassSettings)
      return;
    this.CustomerClassDefaultInserting();
    this.CopyAccounts(cache, row);
    if (((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension1.DefLocation).Current != null)
      ((PXSelectBase) extension1.DefLocation).Cache.SetDefaultExt<PX.Objects.CR.Standalone.Location.cTaxZoneID>((object) ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension1.DefLocation).Current);
    CustomerMaint.LocationDetailsExt extension3 = ((PXGraph) this).GetExtension<CustomerMaint.LocationDetailsExt>();
    foreach (PXResult<PX.Objects.CR.Standalone.Location> pxResult in ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension3.Locations).Select(Array.Empty<object>()))
    {
      PX.Objects.CR.Standalone.Location location = PXResult<PX.Objects.CR.Standalone.Location>.op_Implicit(pxResult);
      extension1.InitLocation((IBqlTable) location, location.LocType, true);
      GraphHelper.MarkUpdated(((PXSelectBase) extension3.Locations).Cache, (object) location);
    }
  }

  protected virtual void Customer_CreditRule_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    Customer row = (Customer) e.Row;
    bool flag = row.CreditRule == "N";
    if (row.CreditRule == "C" | flag)
      row.CreditDaysPastDue = new short?((short) 0);
    if (row.CreditRule == "D" | flag)
      row.CreditLimit = new Decimal?(0M);
    this.UpdateChildAccounts<Customer.creditRule>(cache, row, this.GetChildAccounts(true));
  }

  protected virtual void Customer_CreditLimit_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    this.UpdateChildAccounts<Customer.creditLimit>(cache, (Customer) e.Row, this.GetChildAccounts(true));
  }

  protected virtual void Customer_CreditDaysPastDue_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    this.UpdateChildAccounts<Customer.creditDaysPastDue>(cache, (Customer) e.Row, this.GetChildAccounts(true));
  }

  protected virtual void Customer_Status_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is Customer row) || !e.ExternalCall || !row.SharedCreditChild.GetValueOrDefault())
      return;
    Customer customerParent = this.GetCustomerParent(row);
    if (customerParent == null)
      return;
    string newValue = (string) e.NewValue;
    if ((customerParent.Status == "C" ? (newValue == "H" || newValue == "I" ? 1 : (newValue == "C" ? 1 : 0)) : (newValue != "C" ? 1 : 0)) == 0)
      throw new PXSetPropertyException((IBqlTable) row, "The status can not be changed. This is a child account that shares the credit policy with its parent account. Change the status of a parent account and the value will propagate to all child accounts.");
  }

  protected virtual void Customer_Status_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is Customer row) || row.ParentBAccountID.HasValue)
      return;
    Func<Customer, bool> func;
    string creditChildStatus = this.GetSharedCreditChildStatus(row.Status, out func);
    this.UpdateChildAccounts<Customer.status>(cache, row, this.GetChildAccounts(true).Where<Customer>(func), (object) creditChildStatus);
  }

  /// <summary>
  /// This method returns correct status for child customers with selected "Share Credit Policy" option
  /// according with parent customer status.
  /// Out parameter returns boolean function which include conditions for child customers,
  /// indicating whether it possible to set new status or not.
  /// </summary>
  protected virtual string GetSharedCreditChildStatus(
    string parentStatus,
    out Func<Customer, bool> func)
  {
    string creditChildStatus;
    switch (parentStatus)
    {
      case "C":
        creditChildStatus = "C";
        func = (Func<Customer, bool>) (child => child.Status != "H" && child.Status != "I");
        break;
      case "A":
      case "T":
        creditChildStatus = "A";
        func = (Func<Customer, bool>) (child => child.Status == "C");
        break;
      case "H":
      case "I":
        creditChildStatus = "H";
        func = (Func<Customer, bool>) (child => child.Status == "C");
        break;
      default:
        creditChildStatus = (string) null;
        func = (Func<Customer, bool>) (child => false);
        break;
    }
    return creditChildStatus;
  }

  protected virtual void UpdateChildAccounts<Field>(
    PXCache cache,
    Customer parent,
    IEnumerable<Customer> enumr,
    object sourceValue = null)
    where Field : IBqlField
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>() || parent == null || parent.ParentBAccountID.HasValue)
      return;
    object current = cache.Current;
    sourceValue = sourceValue ?? cache.GetValue<Field>((object) parent);
    foreach (Customer customer in enumr)
    {
      if (sourceValue != cache.GetValue<Field>((object) customer))
      {
        cache.SetValue<Field>((object) customer, sourceValue);
        cache.Update((object) customer);
      }
    }
    cache.Current = current;
  }

  protected virtual void Customer_SmallBalanceAllow_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    ((Customer) e.Row).SmallBalanceLimit = new Decimal?(0M);
  }

  protected virtual void _(PX.Data.Events.RowSelecting<NotificationRecipient> e)
  {
    using (new PXConnectionScope())
      this.CalculateNotificationRecipientEmail(((PX.Data.Events.Event<PXRowSelectingEventArgs, PX.Data.Events.RowSelecting<NotificationRecipient>>) e).Cache, e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<NotificationRecipient, NotificationRecipient.contactID> e)
  {
    this.CalculateNotificationRecipientEmail(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<NotificationRecipient, NotificationRecipient.contactID>>) e).Cache, e.Row);
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
          contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(((PXSelectBase<PX.Objects.CR.Contact>) ((PXGraph) this).GetExtension<CustomerMaint.DefContactAddressExt>().DefContact).SelectWindowed(0, 1, Array.Empty<object>()));
          break;
        case "B":
          contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(((PXSelectBase<PX.Objects.CR.Contact>) this.BillContact).SelectWindowed(0, 1, Array.Empty<object>()));
          break;
        case "S":
          contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(((PXSelectBase<PX.Objects.CR.Contact>) ((PXGraph) this).GetExtension<CustomerMaint.DefLocationExt>().DefLocationContact).SelectWindowed(0, 1, Array.Empty<object>()));
          break;
      }
    }
    if (contact == null)
      return;
    row.Email = contact.EMail;
  }

  public virtual void CustSalesPeople_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
  {
    CustSalesPeople row = (CustSalesPeople) e.Row;
    if (row == null)
      return;
    List<CustSalesPeople> custSalesPeopleList = new List<CustSalesPeople>();
    bool flag = false;
    foreach (PXResult<CustSalesPeople> pxResult in ((PXSelectBase<CustSalesPeople>) this.SalesPersons).Select(Array.Empty<object>()))
    {
      CustSalesPeople custSalesPeople = PXResult<CustSalesPeople>.op_Implicit(pxResult);
      int? nullable1 = row.SalesPersonID;
      int? nullable2 = custSalesPeople.SalesPersonID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        custSalesPeopleList.Add(custSalesPeople);
        nullable2 = row.LocationID;
        nullable1 = custSalesPeople.LocationID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          flag = true;
      }
    }
    if (flag)
    {
      PX.Objects.CR.Standalone.Location location = (PX.Objects.CR.Standalone.Location) null;
      foreach (PXResult<PX.Objects.CR.Standalone.Location> pxResult in ((PXSelectBase<PX.Objects.CR.Standalone.Location>) ((PXGraph) this).GetExtension<CustomerMaint.LocationDetailsExt>().Locations).Select(Array.Empty<object>()))
      {
        PX.Objects.CR.Standalone.Location iLoc = PXResult<PX.Objects.CR.Standalone.Location>.op_Implicit(pxResult);
        if (!custSalesPeopleList.Exists((Predicate<CustSalesPeople>) (op =>
        {
          int? locationId1 = op.LocationID;
          int? locationId2 = iLoc.LocationID;
          return locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue;
        })))
        {
          location = iLoc;
          break;
        }
      }
      row.LocationID = location != null ? location.LocationID : throw new CustomerMaint.PXCustSalesPersonException("This Sales Person is added for all the Customer locations already.");
    }
    Dictionary<int?, short> dictionary = new Dictionary<int?, short>();
    dictionary[row.LocationID] = (short) 0;
    foreach (PXResult<CustSalesPeople> pxResult in ((PXSelectBase<CustSalesPeople>) this.SalesPersons).Select(Array.Empty<object>()))
    {
      CustSalesPeople custSalesPeople = PXResult<CustSalesPeople>.op_Implicit(pxResult);
      if (custSalesPeople.IsDefault.GetValueOrDefault())
      {
        short num;
        dictionary[custSalesPeople.LocationID] = !dictionary.TryGetValue(custSalesPeople.LocationID, out num) ? (short) 1 : ++num;
      }
    }
    if (dictionary[row.LocationID] == (short) 0)
      row.IsDefault = new bool?(true);
    else
      this.CheckDoubleDefault(cache, row);
  }

  protected virtual void CheckDoubleDefault(PXCache sender, CustSalesPeople row)
  {
    if (row == null || !row.IsDefault.GetValueOrDefault())
      return;
    PXResultset<CustSalesPeople> pxResultset = PXSelectBase<CustSalesPeople, PXSelect<CustSalesPeople, Where<CustSalesPeople.bAccountID, Equal<Current<Customer.bAccountID>>, And<CustSalesPeople.isDefault, Equal<True>, And<CustSalesPeople.locationID, Equal<Required<CustSalesPeople.locationID>>, And<CustSalesPeople.salesPersonID, NotEqual<Required<CustSalesPeople.salesPersonID>>>>>>>.Config>.Select(sender.Graph, new object[2]
    {
      (object) row.LocationID,
      (object) row.SalesPersonID
    });
    foreach (PXResult<CustSalesPeople> pxResult in pxResultset)
    {
      CustSalesPeople custSalesPeople = PXResult<CustSalesPeople>.op_Implicit(pxResult);
      custSalesPeople.IsDefault = new bool?(false);
      ((PXSelectBase) this.SalesPersons).Cache.Update((object) custSalesPeople);
    }
    if (pxResultset.Count <= 0)
      return;
    ((PXSelectBase) this.SalesPersons).View.RequestRefresh();
  }

  protected virtual void CustSalesPeople_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    this.CheckDoubleDefault(sender, (CustSalesPeople) e.NewRow);
  }

  public virtual void CustSalesPeople_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    CustSalesPeople row = (CustSalesPeople) e.Row;
    if (row == null)
      return;
    bool flag = false;
    int num = 0;
    foreach (PXResult<PX.Objects.CR.Standalone.Location> pxResult in ((PXSelectBase<PX.Objects.CR.Standalone.Location>) ((PXGraph) this).GetExtension<CustomerMaint.LocationDetailsExt>().Locations).Select(Array.Empty<object>()))
    {
      PXResult<PX.Objects.CR.Standalone.Location>.op_Implicit(pxResult);
      if (num > 0)
      {
        flag = true;
        break;
      }
      ++num;
    }
    PXUIFieldAttribute.SetEnabled<CustSalesPeople.locationID>(((PXSelectBase) this.SalesPersons).Cache, (object) row, !row.LocationID.HasValue | flag);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CustSalesPeople> e)
  {
    CustSalesPeople row = e.Row;
    if (row == null || ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CustSalesPeople>>) e).Cache.GetStatus((object) row) != 2 || ((PXSelectBase) this.BAccount).Cache.GetStatus((object) ((PXSelectBase<Customer>) this.BAccount).Current) == 2)
      return;
    if (((IQueryable<PXResult<PX.Objects.CR.Location>>) PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.locationID, Equal<Required<CustSalesPeople.locationID>>, And<PX.Objects.CR.Location.bAccountID, Equal<Required<CustSalesPeople.bAccountID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.LocationID,
      (object) row.BAccountID
    })).Any<PXResult<PX.Objects.CR.Location>>())
      return;
    GraphHelper.MarkDeleted(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<CustSalesPeople>>) e).Cache, (object) row);
    e.Cancel = true;
  }

  protected virtual bool AllowChangeAccounts() => true;

  public virtual void CopyAccounts(PXCache cache, Customer row)
  {
    cache.SetDefaultExt<Customer.termsID>((object) row);
    cache.SetDefaultExt<Customer.curyID>((object) row);
    cache.SetDefaultExt<Customer.curyRateTypeID>((object) row);
    cache.SetDefaultExt<Customer.allowOverrideCury>((object) row);
    cache.SetDefaultExt<Customer.allowOverrideRate>((object) row);
    cache.SetDefaultExt<Customer.discTakenAcctID>((object) row);
    cache.SetDefaultExt<Customer.discTakenSubID>((object) row);
    cache.SetDefaultExt<Customer.localeName>((object) row);
    cache.SetDefaultExt<Customer.cOrgBAccountID>((object) row);
    cache.SetDefaultExt<Customer.cOGSAcctID>((object) row);
    cache.SetDefaultExt<Customer.smallBalanceAllow>((object) row);
    cache.SetDefaultExt<Customer.smallBalanceLimit>((object) row);
    cache.SetDefaultExt<Customer.autoApplyPayments>((object) row);
    cache.SetDefaultExt<Customer.printStatements>((object) row);
    cache.SetDefaultExt<Customer.printCuryStatements>((object) row);
    cache.SetDefaultExt<Customer.sendStatementByEmail>((object) row);
    cache.SetDefaultExt<Customer.creditLimit>((object) row);
    cache.SetDefaultExt<Customer.creditRule>((object) row);
    cache.SetDefaultExt<Customer.creditDaysPastDue>((object) row);
    cache.SetDefaultExt<Customer.statementCycleId>((object) row);
    cache.SetDefaultExt<Customer.statementType>((object) row);
    cache.SetDefaultExt<Customer.finChargeApply>((object) row);
    cache.SetDefaultExt<Customer.printInvoices>((object) row);
    cache.SetDefaultExt<Customer.mailInvoices>((object) row);
    cache.SetDefaultExt<Customer.printDunningLetters>((object) row);
    cache.SetDefaultExt<Customer.mailDunningLetters>((object) row);
    cache.SetDefaultExt<Customer.prepaymentAcctID>((object) row);
    cache.SetDefaultExt<Customer.prepaymentSubID>((object) row);
    cache.SetDefaultExt<Customer.groupMask>((object) row);
    cache.SetDefaultExt<Customer.retainageApply>((object) row);
    cache.SetDefaultExt<Customer.paymentsByLinesAllowed>((object) row);
  }

  public Customer GetCustomerParent(Customer customer)
  {
    return PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) customer.ParentBAccountID
    }));
  }

  protected virtual void ChangeBAccountType(PX.Objects.CR.BAccount descendantEntity, string type)
  {
    BAccountItself baccountItself = ((PXSelectBase<BAccountItself>) this.CurrentBAccountItself).SelectSingle(new object[1]
    {
      (object) descendantEntity.BAccountID
    });
    if (baccountItself == null)
      return;
    baccountItself.Type = type;
    ((PXSelectBase<BAccountItself>) this.CurrentBAccountItself).Update(baccountItself);
  }

  [PXCacheName("Balance Summary")]
  [Serializable]
  public class CustomerBalanceSummary : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _CustomerID;
    protected Decimal? _Balance;
    protected Decimal? _UnreleasedBalance;
    protected Decimal? _DepositsBalance;
    protected Decimal? _OpenOrdersBalance;
    protected Decimal? _RemainingCreditLimit;

    [PXDBInt]
    [PXDefault]
    public virtual int? CustomerID
    {
      get => this._CustomerID;
      set => this._CustomerID = value;
    }

    [PXBaseCury(null, typeof (Customer.baseCuryID))]
    [CurySymbol(null, null, typeof (Customer.baseCuryID), null, null, null, null, true, true)]
    [PXUIField(DisplayName = "Balance", Visible = true, Enabled = false)]
    public virtual Decimal? Balance
    {
      get => this._Balance;
      set => this._Balance = value;
    }

    [CurySymbol(null, null, typeof (Customer.baseCuryID), null, null, null, null, true, true)]
    [PXBaseCury(null, typeof (Customer.baseCuryID))]
    [PXUIField(DisplayName = "Consolidated Balance", Visible = true, Enabled = false)]
    public virtual Decimal? ConsolidatedBalance { get; set; }

    [CurySymbol(null, null, typeof (Customer.baseCuryID), null, null, null, null, true, true)]
    [PXBaseCury(null, typeof (Customer.baseCuryID))]
    [PXUIField(DisplayName = "Unreleased Balance", Visible = true, Enabled = false)]
    public virtual Decimal? UnreleasedBalance
    {
      get => this._UnreleasedBalance;
      set => this._UnreleasedBalance = value;
    }

    [CurySymbol(null, null, typeof (Customer.baseCuryID), null, null, null, null, true, true)]
    [PXBaseCury(null, typeof (Customer.baseCuryID))]
    [PXUIField(DisplayName = "Prepayments Balance", Visible = true, Enabled = false)]
    public virtual Decimal? DepositsBalance
    {
      get => this._DepositsBalance;
      set => this._DepositsBalance = value;
    }

    [CurySymbol(null, null, typeof (Customer.baseCuryID), null, null, null, null, true, true)]
    [PXBaseCury(null, typeof (Customer.baseCuryID))]
    [PXUIField(DisplayName = "Prepayment Balance", Visible = true, Enabled = false)]
    public virtual Decimal? SignedDepositsBalance
    {
      get
      {
        Decimal? depositsBalance = this._DepositsBalance;
        Decimal num = -1M;
        return !depositsBalance.HasValue ? new Decimal?() : new Decimal?(depositsBalance.GetValueOrDefault() * num);
      }
      set
      {
      }
    }

    [CurySymbol(null, null, typeof (Customer.baseCuryID), null, null, null, null, true, true)]
    [PXBaseCury(null, typeof (Customer.baseCuryID))]
    [PXUIField(DisplayName = "Open Order Balance", Visible = true, Enabled = false, FieldClass = "DISTR")]
    public virtual Decimal? OpenOrdersBalance
    {
      get => this._OpenOrdersBalance;
      set => this._OpenOrdersBalance = value;
    }

    [CurySymbol(null, null, typeof (Customer.baseCuryID), null, null, null, null, true, true)]
    [PXBaseCury(null, typeof (Customer.baseCuryID))]
    [PXUIField(DisplayName = "Available Credit", Visible = true, Enabled = false)]
    public virtual Decimal? RemainingCreditLimit
    {
      get => this._RemainingCreditLimit;
      set => this._RemainingCreditLimit = value;
    }

    [PXDate]
    [PXUIField(DisplayName = "First Due Date", Visible = true, Enabled = false)]
    public virtual DateTime? OldInvoiceDate { get; set; }

    [CurySymbol(null, null, typeof (Customer.baseCuryID), null, null, null, null, true, true)]
    [PXBaseCury(null, typeof (Customer.baseCuryID))]
    [PXUIField(DisplayName = "Retained Balance", Visible = true, Enabled = false, FieldClass = "Retainage")]
    public virtual Decimal? RetainageBalance { get; set; }

    public virtual void Init()
    {
      if (!this.Balance.HasValue)
        this.Balance = new Decimal?(0M);
      if (!this.UnreleasedBalance.HasValue)
        this.Balance = new Decimal?(0M);
      if (!this.RemainingCreditLimit.HasValue)
        this.Balance = new Decimal?(0M);
      if (!this.DepositsBalance.HasValue)
        this.DepositsBalance = new Decimal?(0M);
      if (!this.OpenOrdersBalance.HasValue)
        this.OpenOrdersBalance = new Decimal?(0M);
      if (this.RetainageBalance.HasValue)
        return;
      this.RetainageBalance = new Decimal?(0M);
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CustomerMaint.CustomerBalanceSummary.customerID>
    {
    }

    public abstract class balance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CustomerMaint.CustomerBalanceSummary.balance>
    {
    }

    public abstract class consolidatedbalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CustomerMaint.CustomerBalanceSummary.consolidatedbalance>
    {
    }

    public abstract class unreleasedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CustomerMaint.CustomerBalanceSummary.unreleasedBalance>
    {
    }

    public abstract class depositsBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CustomerMaint.CustomerBalanceSummary.depositsBalance>
    {
    }

    public abstract class signedDepositsBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CustomerMaint.CustomerBalanceSummary.signedDepositsBalance>
    {
    }

    public abstract class openOrdersBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CustomerMaint.CustomerBalanceSummary.openOrdersBalance>
    {
    }

    public abstract class remainingCreditLimit : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CustomerMaint.CustomerBalanceSummary.remainingCreditLimit>
    {
    }

    public abstract class oldInvoiceDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CustomerMaint.CustomerBalanceSummary.oldInvoiceDate>
    {
    }

    public abstract class retainageBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CustomerMaint.CustomerBalanceSummary.retainageBalance>
    {
    }
  }

  /// <summary>
  /// Used to fill the required statement date in the
  /// "Generate On-Demand Statement" dialog box.
  /// <seealso cref="M:PX.Objects.AR.CustomerMaint.GenerateOnDemandStatement(PX.Data.PXAdapter)" />
  /// </summary>
  [PXHidden]
  [Serializable]
  public class OnDemandStatementParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDate]
    [PXUIField(DisplayName = "Statement Date")]
    [PXDefault(typeof (AccessInfo.businessDate))]
    public virtual DateTime? StatementDate { get; set; }

    public abstract class statementDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CustomerMaint.OnDemandStatementParameters.statementDate>
    {
    }
  }

  public class PXCustSalesPersonException : PXException
  {
    public PXCustSalesPersonException(string message)
      : base(message)
    {
    }

    public PXCustSalesPersonException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }

  [PXProjection(typeof (Select4<CuryARHistory, Aggregate<GroupBy<CuryARHistory.branchID, GroupBy<CuryARHistory.customerID, GroupBy<CuryARHistory.accountID, GroupBy<CuryARHistory.subID, GroupBy<CuryARHistory.curyID, Max<CuryARHistory.finPeriodID>>>>>>>>))]
  [Serializable]
  public class ARLatestHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _BranchID;
    protected int? _CustomerID;

    [PXDBInt(IsKey = true, BqlField = typeof (CuryARHistory.branchID))]
    [PXSelector(typeof (PX.SM.Branch.branchID), SubstituteKey = typeof (PX.SM.Branch.branchCD))]
    public virtual int? BranchID { get; set; }

    [PXDBInt(IsKey = true, BqlField = typeof (CuryARHistory.customerID))]
    public virtual int? CustomerID { get; set; }

    [PXDBInt(IsKey = true, BqlField = typeof (CuryARHistory.accountID))]
    public virtual int? AccountID { get; set; }

    [PXDBInt(IsKey = true, BqlField = typeof (CuryARHistory.subID))]
    public virtual int? SubID { get; set; }

    [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (CuryARHistory.curyID))]
    public virtual string CuryID { get; set; }

    [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (CuryARHistory.finPeriodID))]
    public virtual string LastActivityPeriod { get; set; }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CustomerMaint.ARLatestHistory.branchID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CustomerMaint.ARLatestHistory.customerID>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CustomerMaint.ARLatestHistory.accountID>
    {
    }

    public abstract class subID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerMaint.ARLatestHistory.subID>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerMaint.ARLatestHistory.curyID>
    {
    }

    public abstract class lastActivityPeriod : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerMaint.ARLatestHistory.lastActivityPeriod>
    {
    }
  }

  [PXProjection(typeof (Select5<CuryARHistory, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<CuryARHistory.branchID>>, InnerJoin<CustomerMaint.ARLatestHistory, On<CustomerMaint.ARLatestHistory.accountID, Equal<CuryARHistory.accountID>, And<CustomerMaint.ARLatestHistory.branchID, Equal<CuryARHistory.branchID>, And<CustomerMaint.ARLatestHistory.customerID, Equal<CuryARHistory.customerID>, And<CustomerMaint.ARLatestHistory.subID, Equal<CuryARHistory.subID>, And<CustomerMaint.ARLatestHistory.curyID, Equal<CuryARHistory.curyID>, And<CustomerMaint.ARLatestHistory.lastActivityPeriod, Equal<CuryARHistory.finPeriodID>>>>>>>>>, Aggregate<GroupBy<CuryARHistory.customerID, GroupBy<PX.Objects.GL.Branch.baseCuryID, Sum<CuryARHistory.finYtdDeposits>>>>>))]
  [PXHidden]
  [Serializable]
  public class CustomerPrepaymentBalances : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt(IsKey = true, BqlField = typeof (CuryARHistory.customerID))]
    public virtual int? CustomerID { get; set; }

    [PXDBString(5, IsKey = true, IsUnicode = true, BqlTable = typeof (PX.Objects.GL.Branch))]
    public virtual string BaseCuryID { get; set; }

    [PXDBBaseCury(null, null, BqlField = typeof (CuryARHistory.finYtdDeposits))]
    public virtual Decimal? DepositsBalance { get; set; }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CustomerMaint.CustomerPrepaymentBalances.customerID>
    {
    }

    public abstract class baseCuryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerMaint.CustomerPrepaymentBalances.baseCuryID>
    {
    }

    public abstract class depositsBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CustomerMaint.CustomerPrepaymentBalances.depositsBalance>
    {
    }
  }

  [PXProjection(typeof (Select5<ARBalances, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<ARBalances.branchID>>>, Aggregate<GroupBy<ARBalances.customerID, GroupBy<PX.Objects.GL.Branch.baseCuryID, Sum<ARBalances.currentBal, Sum<ARBalances.totalOpenOrders, Sum<ARBalances.totalPrepayments, Sum<ARBalances.totalShipped, Sum<ARBalances.unreleasedBal, Min<ARBalances.oldInvoiceDate>>>>>>>>>>))]
  [PXHidden]
  [Serializable]
  public class CustomerBalances : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt(IsKey = true, BqlField = typeof (ARBalances.customerID))]
    public virtual int? CustomerID { get; set; }

    [PXDBString(5, IsKey = true, IsUnicode = true, BqlTable = typeof (PX.Objects.GL.Branch))]
    public virtual string BaseCuryID { get; set; }

    [PXDBBaseCury(null, null, BqlField = typeof (ARBalances.currentBal))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Balance", Visible = true, Enabled = false)]
    public virtual Decimal? Balance { get; set; }

    [PXDBBaseCury(null, null, BqlField = typeof (ARBalances.unreleasedBal))]
    [PXUIField(DisplayName = "Unreleased Balance", Visible = true, Enabled = false)]
    public virtual Decimal? UnreleasedBalance { get; set; }

    [PXDBBaseCury(null, null, BqlField = typeof (ARBalances.totalOpenOrders))]
    [PXUIField(DisplayName = "Open Orders Balance", Visible = true, Enabled = false, FieldClass = "DISTR")]
    public virtual Decimal? OpenOrdersBalance { get; set; }

    [PXDBDate(BqlField = typeof (ARBalances.oldInvoiceDate))]
    [PXUIField(DisplayName = "First Due Date", Visible = true, Enabled = false)]
    public virtual DateTime? OldInvoiceDate { get; set; }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CustomerMaint.CustomerBalances.customerID>
    {
    }

    public abstract class baseCuryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerMaint.CustomerBalances.baseCuryID>
    {
    }

    public abstract class balance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CustomerMaint.CustomerBalances.balance>
    {
    }

    public abstract class unreleasedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CustomerMaint.CustomerBalances.unreleasedBalance>
    {
    }

    public abstract class openOrdersBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CustomerMaint.CustomerBalances.openOrdersBalance>
    {
    }

    public abstract class oldInvoiceDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CustomerMaint.CustomerBalances.oldInvoiceDate>
    {
    }
  }

  [PXProjection(typeof (Select2<PX.Objects.AR.Override.Customer, InnerJoin<PX.Objects.AR.Override.BAccount, On<PX.Objects.AR.Override.Customer.bAccountID, Equal<PX.Objects.AR.Override.BAccount.bAccountID>>, LeftJoin<CustomerMaint.CustomerBalances, On<CustomerMaint.CustomerBalances.customerID, Equal<PX.Objects.AR.Override.BAccount.bAccountID>>, LeftJoin<CustomerMaint.CustomerPrepaymentBalances, On<CustomerMaint.CustomerPrepaymentBalances.customerID, Equal<PX.Objects.AR.Override.BAccount.bAccountID>, And<Where<CustomerMaint.CustomerPrepaymentBalances.baseCuryID, Equal<CustomerMaint.CustomerBalances.baseCuryID>, Or<CustomerMaint.CustomerBalances.baseCuryID, IsNull>>>>>>>, Where2<Where<PX.Objects.AR.Override.BAccount.bAccountID, Equal<CurrentValue<Customer.bAccountID>>, Or<PX.Objects.AR.Override.BAccount.parentBAccountID, Equal<CurrentValue<Customer.bAccountID>>>>, And<PX.Objects.AR.Override.Customer.bAccountID, IsNotNull>>>), Persistent = false)]
  [PXHidden]
  [Serializable]
  public class ChildCustomerBalanceSummary : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _CustomerID;
    protected Decimal? _Balance;
    protected Decimal? _UnreleasedBalance;
    protected Decimal? _OpenOrdersBalance;
    protected Decimal? _DepositsBalance;

    [Customer(Enabled = false, IsKey = true, BqlField = typeof (PX.Objects.AR.Override.Customer.bAccountID))]
    public virtual int? CustomerID
    {
      get => this._CustomerID;
      set => this._CustomerID = value;
    }

    [PXString(5, IsUnicode = true, IsKey = true)]
    [PXDBCalced(typeof (Switch<Case<Where<CustomerMaint.CustomerBalances.baseCuryID, IsNotNull>, CustomerMaint.CustomerBalances.baseCuryID, Case<Where<CustomerMaint.CustomerPrepaymentBalances.baseCuryID, IsNotNull>, CustomerMaint.CustomerPrepaymentBalances.baseCuryID>>, PX.Objects.AR.Override.BAccount.baseCuryID>), typeof (string))]
    [PXUIField(DisplayName = "Currency")]
    [PXUIVisible(typeof (Where<FeatureInstalled<FeaturesSet.multipleBaseCurrencies>>))]
    public virtual string BaseCuryID { get; set; }

    [PXDBString(60, IsUnicode = true, BqlField = typeof (PX.Objects.AR.Override.BAccount.acctName))]
    [PXUIField(DisplayName = "Customer Name", Enabled = false)]
    public virtual string CustomerName { get; set; }

    [PXBool]
    [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.AR.Override.Customer.bAccountID, Equal<CurrentValue<Customer.bAccountID>>>, True>, False>), typeof (bool))]
    public virtual bool? IsParent { get; set; }

    [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.AR.Override.Customer.statementCycleId))]
    [PXUIField(DisplayName = "Statement Cycle", Enabled = false)]
    [PXSelector(typeof (ARStatementCycle.statementCycleId))]
    public virtual string StatementCycleId { get; set; }

    [PXDBBool(BqlField = typeof (PX.Objects.AR.Override.BAccount.consolidateToParent))]
    [PXUIField(DisplayName = "Consolidate Balance", Enabled = false)]
    public virtual bool? ConsolidateToParent { get; set; }

    [PXDBBool(BqlField = typeof (PX.Objects.AR.Override.Customer.consolidateStatements))]
    [PXUIField(DisplayName = "Consolidate Statements", Enabled = false)]
    public virtual bool? ConsolidateStatements { get; set; }

    [PXDBBool(BqlField = typeof (PX.Objects.AR.Override.Customer.sharedCreditPolicy))]
    [PXUIField(DisplayName = "Share Credit Policy")]
    public virtual bool? SharedCreditPolicy { get; set; }

    [PXDBBaseCury(null, typeof (CustomerMaint.CustomerBalances.baseCuryID), BqlField = typeof (CustomerMaint.CustomerBalances.balance))]
    [PXUIField(DisplayName = "Balance", Visible = true, Enabled = false)]
    public virtual Decimal? Balance
    {
      get => new Decimal?(this._Balance.GetValueOrDefault());
      set => this._Balance = value;
    }

    [PXDBBaseCury(null, typeof (CustomerMaint.CustomerBalances.baseCuryID), BqlField = typeof (CustomerMaint.CustomerBalances.unreleasedBalance))]
    [PXUIField(DisplayName = "Unreleased Balance", Visible = true, Enabled = false)]
    public virtual Decimal? UnreleasedBalance
    {
      get => new Decimal?(this._UnreleasedBalance.GetValueOrDefault());
      set => this._UnreleasedBalance = value;
    }

    [PXDBBaseCury(null, typeof (CustomerMaint.CustomerBalances.baseCuryID), BqlField = typeof (CustomerMaint.CustomerBalances.openOrdersBalance))]
    [PXUIField(DisplayName = "Open Orders Balance", Visible = true, Enabled = false, FieldClass = "DISTR")]
    public virtual Decimal? OpenOrdersBalance
    {
      get => new Decimal?(this._OpenOrdersBalance.GetValueOrDefault());
      set => this._OpenOrdersBalance = value;
    }

    [PXDBBaseCury(null, typeof (CustomerMaint.CustomerBalances.baseCuryID), BqlField = typeof (CustomerMaint.CustomerPrepaymentBalances.depositsBalance))]
    [PXUIField(DisplayName = "Prepayments Balance", Visible = true, Enabled = false)]
    public virtual Decimal? DepositsBalance
    {
      get => new Decimal?(this._DepositsBalance.GetValueOrDefault());
      set => this._DepositsBalance = value;
    }

    [PXBaseCury(null, typeof (CustomerMaint.CustomerBalances.baseCuryID))]
    [PXUIField(DisplayName = "Prepayment Balance", Visible = true, Enabled = false)]
    public virtual Decimal? SignedDepositsBalance
    {
      get
      {
        Decimal? depositsBalance = this.DepositsBalance;
        Decimal num = -1M;
        return !depositsBalance.HasValue ? new Decimal?() : new Decimal?(depositsBalance.GetValueOrDefault() * num);
      }
      set
      {
      }
    }

    [PXDBDate(BqlField = typeof (CustomerMaint.CustomerBalances.oldInvoiceDate))]
    [PXUIField(DisplayName = "First Due Date", Visible = true, Enabled = false)]
    public virtual DateTime? OldInvoiceDate { get; set; }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CustomerMaint.ChildCustomerBalanceSummary.customerID>
    {
    }

    public abstract class baseCuryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerMaint.ChildCustomerBalanceSummary.baseCuryID>
    {
    }

    public abstract class customerName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerMaint.ChildCustomerBalanceSummary.customerName>
    {
    }

    public abstract class isParent : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CustomerMaint.ChildCustomerBalanceSummary.isParent>
    {
    }

    public abstract class statementCycleId : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerMaint.ChildCustomerBalanceSummary.statementCycleId>
    {
    }

    public abstract class consolidateToParent : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CustomerMaint.ChildCustomerBalanceSummary.consolidateToParent>
    {
    }

    public abstract class consolidateStatements : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CustomerMaint.ChildCustomerBalanceSummary.consolidateStatements>
    {
    }

    public abstract class sharedCreditPolicy : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      CustomerMaint.ChildCustomerBalanceSummary.sharedCreditPolicy>
    {
    }

    public abstract class balance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CustomerMaint.ChildCustomerBalanceSummary.balance>
    {
    }

    public abstract class unreleasedBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CustomerMaint.ChildCustomerBalanceSummary.unreleasedBalance>
    {
    }

    public abstract class openOrdersBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CustomerMaint.ChildCustomerBalanceSummary.openOrdersBalance>
    {
    }

    public abstract class depositsBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CustomerMaint.ChildCustomerBalanceSummary.depositsBalance>
    {
    }

    public abstract class signedDepositsBalance : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      CustomerMaint.ChildCustomerBalanceSummary.signedDepositsBalance>
    {
    }

    public abstract class oldInvoiceDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      CustomerMaint.ChildCustomerBalanceSummary.oldInvoiceDate>
    {
    }
  }

  /// <exclude />
  public class PaymentDetailsExt : PXGraphExtension<CustomerMaint>
  {
    [PXCopyPasteHiddenView(ShowInSimpleImport = true)]
    public PXSelect<CustomerPaymentMethod, Where<CustomerPaymentMethod.pMInstanceID, Equal<Optional<Customer.defPMInstanceID>>>> DefPaymentMethodInstance;
    public PXSelect<CustomerPaymentMethod, Where<CustomerPaymentMethod.bAccountID, Equal<Optional<Customer.bAccountID>>>> CustomerPaymentMethods;
    public PXSelect<CustomerPaymentMethodInfo, Where<CustomerPaymentMethodInfo.pMInstanceID, Equal<Current<Customer.defPMInstanceID>>>> DefPaymentMethodInstanceInfo;
    [PXCopyPasteHiddenView(ShowInSimpleImport = true)]
    public PXSelect<CustomerPaymentMethodInfo, Where<CustomerPaymentMethodInfo.pMInstanceID, Equal<Optional<Customer.defPMInstanceID>>>> DefPaymentMethod;
    [PXCopyPasteHiddenView(ShowInSimpleImport = true)]
    public PXSelect<CustomerPaymentMethodInfo, Where2<Where<CustomerPaymentMethodInfo.bAccountID, Equal<Current<Customer.bAccountID>>, Or<CustomerPaymentMethodInfo.bAccountID, IsNull>>, And<CustomerPaymentMethodInfo.isActive, IsNotNull>>> PaymentMethods;
    [PXCopyPasteHiddenView]
    public PXSelectJoin<CustomerPaymentMethodDetail, LeftJoin<PaymentMethodDetail, On<PaymentMethodDetail.paymentMethodID, Equal<CustomerPaymentMethodDetail.paymentMethodID>, And<PaymentMethodDetail.detailID, Equal<CustomerPaymentMethodDetail.detailID>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>>, Where<CustomerPaymentMethodDetail.pMInstanceID, Equal<Current<Customer.defPMInstanceID>>>, OrderBy<Asc<PaymentMethodDetail.orderIndex>>> DefPaymentMethodInstanceDetails;
    public PXSelectJoin<CustomerPaymentMethodDetail, LeftJoin<PaymentMethodDetail, On<PaymentMethodDetail.paymentMethodID, Equal<CustomerPaymentMethodDetail.paymentMethodID>, And<PaymentMethodDetail.detailID, Equal<CustomerPaymentMethodDetail.detailID>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>>, Where<CustomerPaymentMethodDetail.pMInstanceID, Equal<Current<Customer.defPMInstanceID>>>, OrderBy<Asc<PaymentMethodDetail.orderIndex>>> DefPaymentMethodInstanceDetailsAll;
    public PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Optional<CustomerPaymentMethod.paymentMethodID>>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>> PMDetails;
    public PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Optional<CustomerPaymentMethod.paymentMethodID>>>> PaymentMethodDef;
    public PXSelect<CustomerProcessingCenterID, Where<CustomerProcessingCenterID.bAccountID, Equal<Current<Customer.bAccountID>>, And<CustomerProcessingCenterID.cCProcessingCenterID, Equal<Required<CustomerPaymentMethod.cCProcessingCenterID>>, And<CustomerProcessingCenterID.customerCCPID, Equal<Required<CustomerPaymentMethod.customerCCPID>>>>>> CustomerProcessingID;
    public PXSelectJoin<CustomerPaymentMethodDetail, InnerJoin<PaymentMethodDetail, On<CustomerPaymentMethodDetail.paymentMethodID, Equal<PaymentMethodDetail.paymentMethodID>, And<CustomerPaymentMethodDetail.detailID, Equal<PaymentMethodDetail.detailID>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>>, Where<PaymentMethodDetail.isCCProcessingID, Equal<True>, And<CustomerPaymentMethodDetail.pMInstanceID, Equal<Optional<Customer.defPMInstanceID>>>>> ccpIdDet;
    public PXDBAction<Customer> viewPaymentMethod;
    public PXDBAction<Customer> addPaymentMethod;
    private int? mergedPMInstance;

    [InjectDependency]
    public ICCDisplayMaskService CCDisplayMaskService { get; set; }

    public virtual void Initialize()
    {
      ((PXGraphExtension) this).Initialize();
      ((PXSelectBase) this.PaymentMethods).Cache.AllowInsert = false;
      ((PXSelectBase) this.PaymentMethods).Cache.AllowDelete = false;
      ((PXSelectBase) this.DefPaymentMethodInstanceDetails).Cache.AllowInsert = false;
      ((PXSelectBase) this.DefPaymentMethodInstanceDetails).Cache.AllowDelete = false;
      ((PXSelectBase) this.DefPaymentMethodInstance).Cache.AllowUpdate = false;
      PXUIFieldAttribute.SetEnabled<CustomerPaymentMethod.cashAccountID>(((PXSelectBase) this.DefPaymentMethodInstance).Cache, (object) null, false);
      PXUIFieldAttribute.SetEnabled<CustomerPaymentMethodDetail.detailID>(((PXSelectBase) this.DefPaymentMethodInstanceDetails).Cache, (object) null, false);
    }

    public IEnumerable paymentMethods()
    {
      CustomerMaint.PaymentDetailsExt paymentDetailsExt = this;
      PXResultset<CustomerPaymentMethodInfo> pxResultset = PXSelectBase<CustomerPaymentMethodInfo, PXSelectJoin<CustomerPaymentMethodInfo, LeftJoin<CCProcessingCenter, On<CustomerPaymentMethodInfo.cCProcessingCenterID, Equal<CCProcessingCenter.processingCenterID>>>, Where2<Where<CustomerPaymentMethodInfo.bAccountID, Equal<Current<Customer.bAccountID>>, Or<CustomerPaymentMethodInfo.bAccountID, IsNull>>, And<CustomerPaymentMethodInfo.isActive, IsNotNull>>>.Config>.Select((PXGraph) paymentDetailsExt.Base, Array.Empty<object>());
      foreach (PXResult<CustomerPaymentMethodInfo, CCProcessingCenter> pxResult in pxResultset)
      {
        CustomerPaymentMethodInfo paymentMethodInfo = PXResult<CustomerPaymentMethodInfo, CCProcessingCenter>.op_Implicit(pxResult);
        CCProcessingCenter processingCenter = PXResult<CustomerPaymentMethodInfo, CCProcessingCenter>.op_Implicit(pxResult);
        CustomerMaint customerMaint = paymentDetailsExt.Base;
        List<object> objectList = new List<object>();
        objectList.Add(processingCenter.ProcessingCenterID != null ? (object) processingCenter : (object) (CCProcessingCenter) null);
        PXQueryParameters pxQueryParameters = PXQueryParameters.ExplicitParameters(new object[1]
        {
          (object) paymentMethodInfo.PMInstanceID
        });
        PXSelectBase<CCProcessingCenter, PXSelectJoin<CCProcessingCenter, InnerJoin<CustomerPaymentMethod, On<CustomerPaymentMethod.cCProcessingCenterID, Equal<CCProcessingCenter.processingCenterID>>>, Where<CustomerPaymentMethod.pMInstanceID, Equal<Required<CustomerPaymentMethod.pMInstanceID>>>>.Config>.StoreResult((PXGraph) customerMaint, objectList, pxQueryParameters);
      }
      Dictionary<string, List<CustomerPaymentMethodInfo>> dictionary = new Dictionary<string, List<CustomerPaymentMethodInfo>>();
      foreach (PXResult<CustomerPaymentMethodInfo> pxResult in pxResultset)
      {
        CustomerPaymentMethodInfo paymentMethodInfo = PXResult<CustomerPaymentMethodInfo>.op_Implicit(pxResult);
        string lower = paymentMethodInfo.PaymentMethodID.ToLower();
        List<CustomerPaymentMethodInfo> paymentMethodInfoList;
        if (!dictionary.TryGetValue(lower, out paymentMethodInfoList))
        {
          paymentMethodInfoList = new List<CustomerPaymentMethodInfo>();
          dictionary[lower] = paymentMethodInfoList;
        }
        paymentMethodInfoList.Add(paymentMethodInfo);
      }
      foreach (KeyValuePair<string, List<CustomerPaymentMethodInfo>> keyValuePair in dictionary)
      {
        if (keyValuePair.Value.Count > 1)
        {
          if (keyValuePair.Value.FindLast((Predicate<CustomerPaymentMethodInfo>) (info => info.ARIsOnePerCustomer.GetValueOrDefault())) != null)
          {
            yield return (object) keyValuePair.Value.FindLast((Predicate<CustomerPaymentMethodInfo>) (info => info.IsCustomerPaymentMethod.GetValueOrDefault()));
          }
          else
          {
            foreach (object obj in keyValuePair.Value)
              yield return obj;
          }
        }
        else
          yield return (object) keyValuePair.Value[0];
      }
    }

    [PXDependToCache(new System.Type[] {typeof (Customer), typeof (CustomerPaymentMethod), typeof (PX.Objects.CA.PaymentMethod), typeof (PaymentMethodDetail)})]
    public IEnumerable defPaymentMethodInstanceDetails()
    {
      CustomerPaymentMethod cpm = PXResultset<CustomerPaymentMethod>.op_Implicit(((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Select(new object[1]
      {
        (object) (int?) ((PXSelectBase<Customer>) this.Base.CurrentCustomer).Current?.DefPMInstanceID
      }));
      return cpm != null ? CCProcessingHelper.GetPMdetails((PXGraph) this.Base, cpm) : (IEnumerable) null;
    }

    [PXUIField]
    [PXButton(ImageKey = "DataEntry")]
    public virtual IEnumerable ViewPaymentMethod(PXAdapter adapter)
    {
      if (((PXSelectBase<CustomerPaymentMethodInfo>) this.PaymentMethods).Current != null)
      {
        CustomerPaymentMethodInfo current1 = ((PXSelectBase<CustomerPaymentMethodInfo>) this.PaymentMethods).Current;
        Customer current2 = ((PXSelectBase<Customer>) this.Base.BAccount).Current;
        if (current2 != null && current1 != null)
        {
          int? baccountId = ((PXSelectBase<Customer>) this.Base.BAccount).Current.BAccountID;
          long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
          long num = 0;
          if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          {
            if (current1.ARIsOnePerCustomer.GetValueOrDefault())
              throw new PXSetPropertyException("There is no Customer Payment Method associated with the given record. This Payment method does not require specific information for the given customer.", (PXErrorLevel) 1);
            CustomerPaymentMethodMaint instance = PXGraph.CreateInstance<CustomerPaymentMethodMaint>();
            ((PXSelectBase<CustomerPaymentMethod>) instance.CustomerPaymentMethod).Current = PXResultset<CustomerPaymentMethod>.op_Implicit(((PXSelectBase<CustomerPaymentMethod>) instance.CustomerPaymentMethod).Search<CustomerPaymentMethod.pMInstanceID>((object) current1.PMInstanceID, new object[1]
            {
              (object) current2.AcctCD
            }));
            PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
          }
        }
      }
      return adapter.Get();
    }

    [PXUIField]
    [PXButton(ImageKey = "DataEntry")]
    public virtual IEnumerable AddPaymentMethod(PXAdapter adapter)
    {
      if (((PXSelectBase<Customer>) this.Base.BAccount).Current != null)
      {
        int? baccountId = ((PXSelectBase<Customer>) this.Base.BAccount).Current.BAccountID;
        long? nullable = baccountId.HasValue ? new long?((long) baccountId.GetValueOrDefault()) : new long?();
        long num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
        {
          Customer current = ((PXSelectBase<Customer>) this.Base.BAccount).Current;
          CustomerPaymentMethodMaint instance = PXGraph.CreateInstance<CustomerPaymentMethodMaint>();
          ((PXSelectBase<CustomerPaymentMethod>) instance.CustomerPaymentMethod).Insert(new CustomerPaymentMethod()
          {
            BAccountID = current.BAccountID
          });
          PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
        }
      }
      return adapter.Get();
    }

    [PXDBInt]
    [PXDBDefault(typeof (PX.Objects.CR.BAccount.bAccountID))]
    [PXParent(typeof (Select<Customer, Where<Customer.bAccountID, Equal<Current<CustomerPaymentMethod.bAccountID>>>>))]
    public virtual void _(
      PX.Data.Events.CacheAttached<CustomerPaymentMethod.bAccountID> e)
    {
    }

    [PXDBString(10, IsUnicode = true, IsKey = true)]
    [PXUIField(DisplayName = "Payment Method", Enabled = false)]
    [PXDefault(typeof (Customer.defPaymentMethodID))]
    [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID, Where<PX.Objects.CA.PaymentMethod.isActive, Equal<True>, And<PX.Objects.CA.PaymentMethod.useForAR, Equal<True>>>>), DescriptionField = typeof (PX.Objects.CA.PaymentMethod.descr))]
    public virtual void _(
      PX.Data.Events.CacheAttached<CustomerPaymentMethod.paymentMethodID> e)
    {
    }

    [CashAccount(null, typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<PaymentMethodAccount.useForAR, Equal<True>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<CustomerPaymentMethod.paymentMethodID>>>>>>, Where<Match<Current<AccessInfo.userName>>>>))]
    [PXDefault(typeof (Search<PX.Objects.CA.PaymentMethod.defaultCashAccountID, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Current<CustomerPaymentMethod.paymentMethodID>>>>))]
    public virtual void _(
      PX.Data.Events.CacheAttached<CustomerPaymentMethod.cashAccountID> e)
    {
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<Customer, Customer.defPaymentMethodID> e)
    {
      Customer row = e.Row;
      if (row == null)
        return;
      if (row.DefPMInstanceID.HasValue)
      {
        if (((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Current != null)
        {
          int? pmInstanceId = ((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Current.PMInstanceID;
          int? defPmInstanceId = row.DefPMInstanceID;
          if (pmInstanceId.GetValueOrDefault() == defPmInstanceId.GetValueOrDefault() & pmInstanceId.HasValue == defPmInstanceId.HasValue)
            goto label_5;
        }
        ((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Current = PXResultset<CustomerPaymentMethod>.op_Implicit(((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Select(Array.Empty<object>()));
label_5:
        CustomerPaymentMethod current = ((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Current;
        if (current != null && current.PaymentMethodID != row.DefPaymentMethodID && ((PXSelectBase) this.DefPaymentMethodInstance).Cache.GetStatus((object) current) == 2)
        {
          ((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Delete(current);
          ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<Customer, Customer.defPaymentMethodID>>) e).Cache.SetValue<Customer.defPMInstanceID>((object) row, (object) null);
        }
        else if (current == null)
          ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<Customer, Customer.defPaymentMethodID>>) e).Cache.SetValue<Customer.defPMInstanceID>((object) row, (object) null);
      }
      PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) row.DefPaymentMethodID
      }));
      if (paymentMethod != null)
      {
        PX.Objects.AR.CustomerClass customerClass = ((PXSelectBase<PX.Objects.AR.CustomerClass>) this.Base.CustomerClass).SelectSingle(Array.Empty<object>());
        bool flag = false;
        if (((PXSelectBase) this.Base.CurrentCustomer).Cache.GetStatus((object) ((PXSelectBase<Customer>) this.Base.CurrentCustomer).Current) == 2 && customerClass?.SavePaymentProfiles == "F")
          flag = true;
        if (paymentMethod.ARIsOnePerCustomer.GetValueOrDefault() || !flag)
          ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<Customer, Customer.defPaymentMethodID>>) e).Cache.SetValueExt<Customer.defPMInstanceID>((object) row, (object) paymentMethod.PMInstanceID);
        else
          this.CreateDefPaymentMethod(row);
      }
      else
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<Customer, Customer.defPaymentMethodID>>) e).Cache.SetValueExt<Customer.defPMInstanceID>((object) row, (object) null);
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<Customer, Customer.defPMInstanceID> e)
    {
      Customer row = e.Row;
      if (row == null)
        return;
      if (row.DefPMInstanceID.HasValue)
      {
        CustomerPaymentMethodInfo paymentMethodInfo = PXResultset<CustomerPaymentMethodInfo>.op_Implicit(((PXSelectBase<CustomerPaymentMethodInfo>) this.DefPaymentMethod).Select(new object[1]
        {
          (object) row.DefPMInstanceID
        }));
        if (paymentMethodInfo == null || !(paymentMethodInfo.PaymentMethodID != row.DefPaymentMethodID))
          return;
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<Customer, Customer.defPMInstanceID>>) e).Cache.SetValue<Customer.defPaymentMethodID>((object) row, (object) paymentMethodInfo.PaymentMethodID);
      }
      else
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<Customer, Customer.defPMInstanceID>>) e).Cache.SetValue<Customer.defPaymentMethodID>((object) row, (object) null);
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<Customer, Customer.customerClassID> e)
    {
      Customer row = e.Row;
      if (row == null || ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<Customer, Customer.customerClassID>>) e).Cache.GetStatus((object) row) != 2 && !string.IsNullOrEmpty(row.DefPaymentMethodID))
        return;
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<Customer, Customer.customerClassID>>) e).Cache.SetDefaultExt<Customer.defPaymentMethodID>((object) row);
    }

    protected virtual void _(PX.Data.Events.RowInserted<Customer> e)
    {
      Customer row = e.Row;
      if (row == null || row.CustomerClassID == null)
        return;
      ((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<Customer>>) e).Cache.SetDefaultExt<Customer.defPaymentMethodID>((object) row);
      ((PXSelectBase) this.DefPaymentMethodInstance).Cache.IsDirty = false;
      ((PXSelectBase) this.DefPaymentMethodInstanceDetails).Cache.IsDirty = false;
    }

    protected virtual void _(PX.Data.Events.RowSelected<Customer> e)
    {
      if (e.Row == null)
        return;
      CustomerPaymentMethod customerPaymentMethod = PXResultset<CustomerPaymentMethod>.op_Implicit(((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Select(new object[1]
      {
        (object) ((PXSelectBase<Customer>) this.Base.CurrentCustomer).Current.DefPMInstanceID
      }));
      bool flag1 = ((PXSelectBase) this.DefPaymentMethodInstance).Cache.GetStatus((object) customerPaymentMethod) == 2;
      bool flag2 = customerPaymentMethod != null;
      ((PXSelectBase) this.PaymentMethodDef).Cache.RaiseRowSelected((object) ((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethodDef).Current);
      ((PXSelectBase) this.DefPaymentMethodInstance).Cache.AllowUpdate = flag2;
      ((PXSelectBase) this.DefPaymentMethodInstanceDetails).Cache.AllowUpdate = flag2 & flag1;
      PXUIFieldAttribute.SetRequired<CustomerPaymentMethod.cashAccountID>(((PXSelectBase) this.DefPaymentMethodInstance).Cache, false);
    }

    protected virtual void _(PX.Data.Events.RowSelected<CustomerPaymentMethod> e)
    {
      CustomerPaymentMethod row1 = e.Row;
      PXUIFieldAttribute.SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerPaymentMethod>>) e).Cache, (string) null, false);
      if (row1 == null)
        return;
      Customer current = ((PXSelectBase<Customer>) this.Base.BAccount).Current;
      if (current == null)
        return;
      int? defPmInstanceId = current.DefPMInstanceID;
      int? pmInstanceId = row1.PMInstanceID;
      if (!(defPmInstanceId.GetValueOrDefault() == pmInstanceId.GetValueOrDefault() & defPmInstanceId.HasValue == pmInstanceId.HasValue))
        return;
      PXUIFieldAttribute.SetEnabled<CustomerPaymentMethod.descr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerPaymentMethod>>) e).Cache, (object) row1, false);
      PXUIFieldAttribute.SetEnabled<CustomerPaymentMethod.cashAccountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerPaymentMethod>>) e).Cache, (object) row1, true);
      bool flag1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerPaymentMethod>>) e).Cache.GetStatus((object) e.Row) == 2;
      if (!string.IsNullOrEmpty(row1.PaymentMethodID))
      {
        PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethodDef).Select(Array.Empty<object>()));
        bool? nullable = paymentMethod.ARIsOnePerCustomer;
        bool valueOrDefault = nullable.GetValueOrDefault();
        bool flag2 = false;
        if (!valueOrDefault)
        {
          foreach (PXResult<PaymentMethodDetail> pxResult in ((PXSelectBase<PaymentMethodDetail>) this.PMDetails).Select(new object[1]
          {
            (object) row1.PaymentMethodID
          }))
          {
            PaymentMethodDetail paymentMethodDetail = PXResult<PaymentMethodDetail>.op_Implicit(pxResult);
            nullable = paymentMethodDetail.IsIdentifier;
            if (nullable.GetValueOrDefault() && !string.IsNullOrEmpty(paymentMethodDetail.DisplayMask))
            {
              flag2 = true;
              break;
            }
          }
        }
        if (!(flag2 | valueOrDefault))
          PXUIFieldAttribute.SetEnabled<CustomerPaymentMethod.descr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerPaymentMethod>>) e).Cache, (object) row1, true);
        bool flag3 = flag1 && paymentMethod.PaymentType == "CCD";
        PXUIFieldAttribute.SetVisible<CustomerPaymentMethod.cCProcessingCenterID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerPaymentMethod>>) e).Cache, (object) row1, flag3);
        PXUIFieldAttribute.SetEnabled<CustomerPaymentMethod.cCProcessingCenterID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerPaymentMethod>>) e).Cache, (object) row1, flag3);
        bool flag4 = flag1 && paymentMethod.PaymentType == "CCD" && CCProcessingHelper.IsTokenizedPaymentMethod((PXGraph) this.Base, row1.PMInstanceID);
        PXUIFieldAttribute.SetVisible<CustomerPaymentMethod.customerCCPID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerPaymentMethod>>) e).Cache, (object) row1, flag4);
        PXUIFieldAttribute.SetEnabled<CustomerPaymentMethod.customerCCPID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerPaymentMethod>>) e).Cache, (object) row1, flag4);
      }
      if (!flag1 && !string.IsNullOrEmpty(row1.PaymentMethodID))
      {
        this.MergeDetailsWithDefinition(row1);
        bool flag5 = ExternalTranHelper.HasTransactions((PXGraph) this.Base, row1.PMInstanceID);
        ((PXSelectBase) this.DefPaymentMethodInstanceDetails).Cache.AllowDelete = !flag5;
        PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.DefPaymentMethodInstanceDetails).Cache, (string) null, !flag5);
      }
      if (!row1.CashAccountID.HasValue)
        return;
      PaymentMethodAccount paymentMethodAccount = PXResultset<PaymentMethodAccount>.op_Implicit(PXSelectBase<PaymentMethodAccount, PXSelect<PaymentMethodAccount, Where<PaymentMethodAccount.cashAccountID, Equal<Required<PaymentMethodAccount.cashAccountID>>, And<PaymentMethodAccount.paymentMethodID, Equal<Required<PaymentMethodAccount.paymentMethodID>>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) row1.CashAccountID,
        (object) row1.PaymentMethodID
      }));
      PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerPaymentMethod>>) e).Cache;
      CustomerPaymentMethod row2 = e.Row;
      string str;
      if (paymentMethodAccount != null)
        str = (string) null;
      else
        str = PXMessages.LocalizeFormatNoPrefixNLA("The specified cash account is not configured for use in AR for the {0} payment method.", new object[1]
        {
          (object) row1.PaymentMethodID
        });
      PXUIFieldAttribute.SetWarning<CustomerPaymentMethod.cashAccountID>(cache, (object) row2, str);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<CustomerPaymentMethod, CustomerPaymentMethod.descr> e)
    {
      CustomerPaymentMethod row = e.Row;
      PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethodDef).Select(new object[1]
      {
        (object) row.PaymentMethodID
      }));
      if (paymentMethod == null || !paymentMethod.ARIsOnePerCustomer.GetValueOrDefault())
        return;
      row.Descr = paymentMethod.Descr;
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<CustomerPaymentMethod, CustomerPaymentMethod.descr> e)
    {
      CustomerPaymentMethod row = e.Row;
      if (PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethodDef).Select(new object[1]
      {
        (object) row.PaymentMethodID
      })).ARIsOnePerCustomer.GetValueOrDefault())
        return;
      if (PXResultset<CustomerPaymentMethod>.op_Implicit(PXSelectBase<CustomerPaymentMethod, PXSelect<CustomerPaymentMethod, Where<CustomerPaymentMethod.bAccountID, Equal<Required<CustomerPaymentMethod.bAccountID>>, And<CustomerPaymentMethod.paymentMethodID, Equal<Required<CustomerPaymentMethod.paymentMethodID>>, And<CustomerPaymentMethod.pMInstanceID, NotEqual<Required<CustomerPaymentMethod.pMInstanceID>>, And<CustomerPaymentMethod.descr, Equal<Required<CustomerPaymentMethod.descr>>>>>>>.Config>.Select((PXGraph) this.Base, new object[4]
      {
        (object) row.BAccountID,
        (object) row.PaymentMethodID,
        (object) row.PMInstanceID,
        (object) row.Descr
      })) == null)
        return;
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CustomerPaymentMethod, CustomerPaymentMethod.descr>>) e).Cache.RaiseExceptionHandling<CustomerPaymentMethod.descr>((object) row, (object) row.Descr, (Exception) new PXSetPropertyException("A card with this card number is already registered for the customer.", (PXErrorLevel) 2));
    }

    protected virtual void _(PX.Data.Events.RowPersisting<CustomerPaymentMethod> e)
    {
      CustomerPaymentMethod row = e.Row;
      Customer current = ((PXSelectBase<Customer>) this.Base.BAccount).Current;
      if (row == null || current == null)
        return;
      if (!string.IsNullOrEmpty(row.CustomerCCPID))
      {
        if (PXResultset<CustomerProcessingCenterID>.op_Implicit(((PXSelectBase<CustomerProcessingCenterID>) this.CustomerProcessingID).Select(new object[2]
        {
          (object) row.CCProcessingCenterID,
          (object) row.CustomerCCPID
        })) != null)
          return;
        ((PXSelectBase<CustomerProcessingCenterID>) this.CustomerProcessingID).Insert(new CustomerProcessingCenterID()
        {
          BAccountID = current.BAccountID,
          CCProcessingCenterID = row.CCProcessingCenterID,
          CustomerCCPID = row.CustomerCCPID
        });
      }
      else
      {
        if (!this.SkipInsertingCustomerWithNewCpm(current, row))
          return;
        PX.Objects.CA.PaymentMethod defPM = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethodDef).Select(Array.Empty<object>()));
        if (defPM == null)
          return;
        CustomerPaymentMethodInfo paymentMethodInfo = GraphHelper.RowCast<CustomerPaymentMethodInfo>((IEnumerable) ((PXSelectBase<CustomerPaymentMethodInfo>) this.PaymentMethods).Select(Array.Empty<object>())).Where<CustomerPaymentMethodInfo>((Func<CustomerPaymentMethodInfo, bool>) (i =>
        {
          int? pmInstanceId1 = i.PMInstanceID;
          int? pmInstanceId2 = defPM.PMInstanceID;
          return pmInstanceId1.GetValueOrDefault() == pmInstanceId2.GetValueOrDefault() & pmInstanceId1.HasValue == pmInstanceId2.HasValue;
        })).FirstOrDefault<CustomerPaymentMethodInfo>();
        if (paymentMethodInfo == null)
          return;
        ((PXSelectBase<CustomerPaymentMethodInfo>) this.PaymentMethods).SetValueExt<CustomerPaymentMethodInfo.isDefault>(paymentMethodInfo, (object) true);
        e.Cancel = true;
      }
    }

    protected virtual void _(PX.Data.Events.RowSelected<CustomerPaymentMethodDetail> e)
    {
      CustomerPaymentMethodDetail row = e.Row;
      if (row == null)
        return;
      PaymentMethodDetail template = this.FindTemplate(row);
      CustomerPaymentMethod customerPaymentMethod = PXResultset<CustomerPaymentMethod>.op_Implicit(((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Select(new object[1]
      {
        (object) row.PMInstanceID
      }));
      PXDefaultAttribute.SetPersistingCheck<CustomerPaymentMethodDetail.value>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerPaymentMethodDetail>>) e).Cache, (object) row, template == null || !template.IsRequired.GetValueOrDefault() ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
      PXDBCryptStringAttribute.SetDecrypted<CustomerPaymentMethodDetail.value>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerPaymentMethodDetail>>) e).Cache, (object) row, template == null || !template.IsEncrypted.GetValueOrDefault());
      bool flag = !string.IsNullOrEmpty(customerPaymentMethod?.CustomerCCPID) && template != null && template.IsCCProcessingID.GetValueOrDefault() || template == null || !template.IsCCProcessingID.GetValueOrDefault();
      PXUIFieldAttribute.SetEnabled<CustomerPaymentMethodDetail.value>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerPaymentMethodDetail>>) e).Cache, (object) row, flag);
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<CustomerPaymentMethodDetail, CustomerPaymentMethodDetail.value> e)
    {
      CustomerPaymentMethodDetail row = e.Row;
      PaymentMethodDetail template = this.FindTemplate(row);
      if (template == null)
        return;
      bool? nullable = template.IsIdentifier;
      if (nullable.GetValueOrDefault())
      {
        string str = this.CCDisplayMaskService.UseAdjustedDisplayMaskForCardNumber(row.Value, template.DisplayMask);
        if (((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Current.Descr != str)
        {
          CustomerPaymentMethod current = ((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Current;
          current.Descr = $"{current.PaymentMethodID}:{str}";
          ((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Update(current);
        }
      }
      nullable = template.IsExpirationDate;
      if (!nullable.GetValueOrDefault())
        return;
      CustomerPaymentMethod current1 = ((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Current;
      ((PXSelectBase) this.DefPaymentMethodInstance).Cache.SetValueExt<CustomerPaymentMethod.expirationDate>((object) current1, (object) CustomerPaymentMethodMaint.ParseExpiryDate((PXGraph) this.Base, current1, row.Value));
      ((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Update(current1);
    }

    protected virtual void _(PX.Data.Events.RowDeleted<CustomerPaymentMethodDetail> e)
    {
      CustomerPaymentMethodDetail row = e.Row;
      if (((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Current == null)
        return;
      PaymentMethodDetail template = this.FindTemplate(row);
      if (template == null || !template.IsIdentifier.GetValueOrDefault())
        return;
      ((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Current.Descr = (string) null;
    }

    protected virtual void _(PX.Data.Events.RowSelected<CustomerPaymentMethodInfo> e)
    {
      CustomerPaymentMethodInfo row = e.Row;
      if (row == null)
        return;
      bool flag = row.IsActive.Value && row.IsCustomerPaymentMethod.Value && row.PaymentType == "CCD" && !string.IsNullOrEmpty(row.CCProcessingCenterID) && PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>();
      PXUIFieldAttribute.SetEnabled<CustomerPaymentMethodInfo.availableOnPortals>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerPaymentMethodInfo>>) e).Cache, (object) row, flag);
      PXUIFieldAttribute.SetEnabled<CustomerPaymentMethodInfo.isPortalDefault>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerPaymentMethodInfo>>) e).Cache, (object) row, row.AvailableOnPortals.GetValueOrDefault());
      Customer current = ((PXSelectBase<Customer>) this.Base.CurrentCustomer).Current;
      if (current == null)
        return;
      PXUIFieldAttribute.SetEnabled<CustomerPaymentMethodInfo.isDefault>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerPaymentMethodInfo>>) e).Cache, (object) row, row.IsActive.GetValueOrDefault());
      CustomerPaymentMethodInfo paymentMethodInfo = row;
      int? pmInstanceId = row.PMInstanceID;
      int? defPmInstanceId = current.DefPMInstanceID;
      bool? nullable = new bool?(pmInstanceId.GetValueOrDefault() == defPmInstanceId.GetValueOrDefault() & pmInstanceId.HasValue == defPmInstanceId.HasValue);
      paymentMethodInfo.IsDefault = nullable;
      GraphHelper.MarkUpdated(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerPaymentMethodInfo>>) e).Cache, (object) row);
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<CustomerPaymentMethodInfo, CustomerPaymentMethodInfo.isDefault> e)
    {
      CustomerPaymentMethodInfo row = e.Row;
      if (row == null)
        return;
      Customer current = ((PXSelectBase<Customer>) this.Base.BAccount).Current;
      if (((PXSelectBase) this.DefPaymentMethodInstance).Cache.Inserted.Count() > 0L)
        ((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Delete(PXResultset<CustomerPaymentMethod>.op_Implicit(((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Select(Array.Empty<object>())));
      Customer customer = current;
      bool? isDefault = row.IsDefault;
      bool flag = false;
      int? nullable = isDefault.GetValueOrDefault() == flag & isDefault.HasValue ? new int?() : row.PMInstanceID;
      customer.DefPMInstanceID = nullable;
      ((PXSelectBase<Customer>) this.Base.BAccount).Update(current);
      ((PXSelectBase) this.PaymentMethods).View.RequestRefresh();
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<CustomerPaymentMethodInfo, CustomerPaymentMethodInfo.availableOnPortals> e)
    {
      if ((bool) e.NewValue)
        return;
      CustomerPaymentMethodInfo row = e.Row;
      if (row == null)
        return;
      row.IsPortalDefault = new bool?(false);
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<CustomerPaymentMethodInfo, CustomerPaymentMethodInfo.isPortalDefault> e)
    {
      CustomerPaymentMethodInfo row = e.Row;
      if (row == null || !(bool) e.NewValue)
        return;
      foreach (PXResult<CustomerPaymentMethod> pxResult in ((PXSelectBase<CustomerPaymentMethod>) this.CustomerPaymentMethods).Select(Array.Empty<object>()))
      {
        CustomerPaymentMethod customerPaymentMethod = PXResult<CustomerPaymentMethod>.op_Implicit(pxResult);
        int? pmInstanceId1 = customerPaymentMethod.PMInstanceID;
        int? pmInstanceId2 = row.PMInstanceID;
        customerPaymentMethod.IsPortalDefault = !(pmInstanceId1.GetValueOrDefault() == pmInstanceId2.GetValueOrDefault() & pmInstanceId1.HasValue == pmInstanceId2.HasValue) ? new bool?(false) : row.IsPortalDefault;
        ((PXSelectBase<CustomerPaymentMethod>) this.CustomerPaymentMethods).Update(customerPaymentMethod);
      }
      foreach (PXResult<CustomerPaymentMethodInfo> pxResult in ((PXSelectBase<CustomerPaymentMethodInfo>) this.PaymentMethods).Select(Array.Empty<object>()))
      {
        CustomerPaymentMethodInfo paymentMethodInfo = PXResult<CustomerPaymentMethodInfo>.op_Implicit(pxResult);
        int? pmInstanceId3 = paymentMethodInfo.PMInstanceID;
        int? pmInstanceId4 = row.PMInstanceID;
        if (!(pmInstanceId3.GetValueOrDefault() == pmInstanceId4.GetValueOrDefault() & pmInstanceId3.HasValue == pmInstanceId4.HasValue))
          paymentMethodInfo.IsPortalDefault = new bool?(false);
      }
      ((PXSelectBase) this.PaymentMethods).View.RequestRefresh();
    }

    protected virtual void _(PX.Data.Events.RowPersisting<CustomerPaymentMethodInfo> e)
    {
      e.Cancel = true;
    }

    [PXOverride]
    public virtual void Persist(System.Action del)
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        if (((PXSelectBase) this.DefPaymentMethodInstance).Cache.Inserted.Count() > 0L)
        {
          IEnumerator enumerator = ((PXSelectBase) this.DefPaymentMethodInstance).Cache.Inserted.GetEnumerator();
          if (enumerator.MoveNext() && enumerator.Current is CustomerPaymentMethod current && CCProcessingHelper.IsTokenizedPaymentMethod((PXGraph) this.Base, current.PMInstanceID))
          {
            CCCustomerInformationManagerGraph instance = PXGraph.CreateInstance<CCCustomerInformationManagerGraph>();
            ICCPaymentProfileAdapter paymentProfileAdapter1 = (ICCPaymentProfileAdapter) new GenericCCPaymentProfileAdapter<CustomerPaymentMethod>((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance);
            ICCPaymentProfileDetailAdapter profileDetailAdapter1 = (ICCPaymentProfileDetailAdapter) new GenericCCPaymentProfileDetailAdapter<CustomerPaymentMethodDetail, PaymentMethodDetail>((PXSelectBase<CustomerPaymentMethodDetail>) this.DefPaymentMethodInstanceDetailsAll, (PXSelectBase<PaymentMethodDetail>) this.PMDetails);
            CustomerMaint graph = this.Base;
            ICCPaymentProfileAdapter paymentProfileAdapter2 = paymentProfileAdapter1;
            ICCPaymentProfileDetailAdapter profileDetailAdapter2 = profileDetailAdapter1;
            instance.GetOrCreatePaymentProfile((PXGraph) graph, paymentProfileAdapter2, profileDetailAdapter2);
          }
        }
        del();
        transactionScope.Complete();
      }
    }

    public virtual void CreateDefPaymentMethod(Customer account)
    {
      PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelect<PX.Objects.CA.PaymentMethod, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Required<PX.Objects.CA.PaymentMethod.paymentMethodID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) account.DefPaymentMethodID
      }));
      if (account.DefPMInstanceID.HasValue || paymentMethod == null)
        return;
      bool? nullable = paymentMethod.ARIsProcessingRequired;
      if (!nullable.GetValueOrDefault())
        return;
      CustomerPaymentMethod customerPaymentMethod = ((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Insert(new CustomerPaymentMethod());
      if (!customerPaymentMethod.BAccountID.HasValue)
        customerPaymentMethod.BAccountID = account.BAccountID;
      account.DefPMInstanceID = customerPaymentMethod.PMInstanceID;
      if (!this.AddPMDetails())
        return;
      nullable = paymentMethod.ARIsOnePerCustomer;
      if (!(nullable.HasValue ? new bool?(!nullable.GetValueOrDefault()) : new bool?()).GetValueOrDefault())
        return;
      ((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Current.Descr = account.DefPaymentMethodID;
    }

    public virtual bool AddPMDetails()
    {
      string paymentMethodId = ((PXSelectBase<CustomerPaymentMethod>) this.DefPaymentMethodInstance).Current.PaymentMethodID;
      bool flag = true;
      if (!string.IsNullOrEmpty(paymentMethodId))
      {
        foreach (PXResult<PaymentMethodDetail> pxResult in ((PXSelectBase<PaymentMethodDetail>) this.PMDetails).Select(Array.Empty<object>()))
        {
          PaymentMethodDetail paymentMethodDetail = PXResult<PaymentMethodDetail>.op_Implicit(pxResult);
          if (paymentMethodDetail.IsIdentifier.GetValueOrDefault())
            flag = false;
          ((PXSelectBase<CustomerPaymentMethodDetail>) this.DefPaymentMethodInstanceDetails).Insert(new CustomerPaymentMethodDetail()
          {
            DetailID = paymentMethodDetail.DetailID
          });
        }
      }
      return flag;
    }

    public virtual void ClearPMDetails()
    {
      foreach (PXResult<CustomerPaymentMethodDetail> pxResult in ((PXSelectBase<CustomerPaymentMethodDetail>) this.DefPaymentMethodInstanceDetailsAll).Select(Array.Empty<object>()))
        ((PXSelectBase<CustomerPaymentMethodDetail>) this.DefPaymentMethodInstanceDetails).Delete(PXResult<CustomerPaymentMethodDetail>.op_Implicit(pxResult));
    }

    public virtual PaymentMethodDetail FindTemplate(CustomerPaymentMethodDetail aDet)
    {
      return PXResultset<PaymentMethodDetail>.op_Implicit(PXSelectBase<PaymentMethodDetail, PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Required<PaymentMethodDetail.paymentMethodID>>, And<PaymentMethodDetail.detailID, Equal<Required<PaymentMethodDetail.detailID>>, And<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForARCards>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) aDet.PaymentMethodID,
        (object) aDet.DetailID
      }));
    }

    public virtual void MergeDetailsWithDefinition(CustomerPaymentMethod aRow)
    {
      string paymentMethodId = aRow.PaymentMethodID;
      int? pmInstanceId = aRow.PMInstanceID;
      int? mergedPmInstance = this.mergedPMInstance;
      if (pmInstanceId.GetValueOrDefault() == mergedPmInstance.GetValueOrDefault() & pmInstanceId.HasValue == mergedPmInstance.HasValue)
        return;
      List<PaymentMethodDetail> paymentMethodDetailList1 = new List<PaymentMethodDetail>();
      List<CustomerPaymentMethodDetail> paymentMethodDetailList2 = new List<CustomerPaymentMethodDetail>();
      foreach (PXResult<PaymentMethodDetail> pxResult1 in ((PXSelectBase<PaymentMethodDetail>) this.PMDetails).Select(new object[1]
      {
        (object) paymentMethodId
      }))
      {
        PaymentMethodDetail paymentMethodDetail1 = PXResult<PaymentMethodDetail>.op_Implicit(pxResult1);
        CustomerPaymentMethodDetail paymentMethodDetail2 = (CustomerPaymentMethodDetail) null;
        foreach (PXResult<CustomerPaymentMethodDetail> pxResult2 in ((PXSelectBase<CustomerPaymentMethodDetail>) this.DefPaymentMethodInstanceDetailsAll).Select(Array.Empty<object>()))
        {
          CustomerPaymentMethodDetail paymentMethodDetail3 = PXResult<CustomerPaymentMethodDetail>.op_Implicit(pxResult2);
          if (paymentMethodDetail3.DetailID == paymentMethodDetail1.DetailID)
          {
            paymentMethodDetail2 = paymentMethodDetail3;
            break;
          }
        }
        if (paymentMethodDetail2 == null && (!(paymentMethodDetail1.DetailID == "CVV") || !aRow.CVVVerifyTran.HasValue))
          paymentMethodDetailList1.Add(paymentMethodDetail1);
      }
      using (new ReadOnlyScope(new PXCache[1]
      {
        ((PXSelectBase) this.DefPaymentMethodInstanceDetails).Cache
      }))
      {
        foreach (PaymentMethodDetail paymentMethodDetail in paymentMethodDetailList1)
          ((PXSelectBase<CustomerPaymentMethodDetail>) this.DefPaymentMethodInstanceDetails).Insert(new CustomerPaymentMethodDetail()
          {
            DetailID = paymentMethodDetail.DetailID
          });
        if (paymentMethodDetailList1.Count <= 0)
        {
          if (paymentMethodDetailList2.Count <= 0)
            goto label_30;
        }
        ((PXSelectBase) this.DefPaymentMethodInstanceDetails).View.RequestRefresh();
      }
label_30:
      this.mergedPMInstance = aRow.PMInstanceID;
    }

    private bool SkipInsertingCustomerWithNewCpm(Customer customer, CustomerPaymentMethod cpm)
    {
      bool flag = false;
      PXEntryStatus status = ((PXSelectBase) this.Base.BAccount).Cache.GetStatus((object) customer);
      if (string.IsNullOrEmpty(cpm.CustomerCCPID) && status == 2 && CCProcessingHelper.IsTokenizedPaymentMethod((PXGraph) this.Base, cpm.PMInstanceID))
      {
        PX.Objects.CA.PaymentMethod paymentMethod = PXResultset<PX.Objects.CA.PaymentMethod>.op_Implicit(((PXSelectBase<PX.Objects.CA.PaymentMethod>) this.PaymentMethodDef).Select(Array.Empty<object>()));
        if (paymentMethod != null)
        {
          int? pmInstanceId = paymentMethod.PMInstanceID;
          int? defPmInstanceId = customer.DefPMInstanceID;
          if (!(pmInstanceId.GetValueOrDefault() == defPmInstanceId.GetValueOrDefault() & pmInstanceId.HasValue == defPmInstanceId.HasValue))
            flag = true;
        }
      }
      return flag;
    }
  }

  /// <exclude />
  public class DefContactAddressExt : 
    PX.Objects.CR.Extensions.DefContactAddressExt<CustomerMaint, Customer, Customer.acctName>.WithCombinedTypeValidation
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
      if (((PXSelectBase) this.Base.CurrentCustomer).Cache.GetStatus((object) ((PXSelectBase<Customer>) this.Base.CurrentCustomer).Current) != 2)
        return;
      aAddress.CountryID = ((PXSelectBase<PX.Objects.AR.CustomerClass>) this.Base.CustomerClass).Current?.CountryID ?? aAddress.CountryID;
    }

    public override void ValidateAddress()
    {
      base.ValidateAddress();
      PX.Objects.CR.Address aAddress = ((PXSelectBase<PX.Objects.CR.Address>) this.Base.BillAddress).SelectSingle(Array.Empty<object>());
      if (aAddress == null)
        return;
      bool? isValidated = aAddress.IsValidated;
      bool flag = false;
      if (!(isValidated.GetValueOrDefault() == flag & isValidated.HasValue))
        return;
      PXAddressValidator.Validate<PX.Objects.CR.Address>((PXGraph) this.Base, aAddress, true, true);
    }
  }

  /// <exclude />
  public class DefLocationExt : 
    PX.Objects.CR.Extensions.DefLocationExt<CustomerMaint, CustomerMaint.DefContactAddressExt, CustomerMaint.LocationDetailsExt, Customer, Customer.bAccountID, Customer.defLocationID>.WithCombinedTypeValidation
  {
    public override List<System.Type> InitLocationFields
    {
      get
      {
        return new List<System.Type>()
        {
          typeof (PX.Objects.CR.Standalone.Location.cCarrierID),
          typeof (PX.Objects.CR.Standalone.Location.cFOBPointID),
          typeof (PX.Objects.CR.Standalone.Location.cResedential),
          typeof (PX.Objects.CR.Standalone.Location.cSaturdayDelivery),
          typeof (PX.Objects.CR.Standalone.Location.cLeadTime),
          typeof (PX.Objects.CR.Standalone.Location.cShipComplete),
          typeof (PX.Objects.CR.Standalone.Location.cShipTermsID),
          typeof (PX.Objects.CR.Standalone.Location.cTaxCalcMode),
          typeof (PX.Objects.CR.Standalone.Location.cAvalaraCustomerUsageType),
          typeof (PX.Objects.CR.Standalone.Location.cDiscountAcctID),
          typeof (PX.Objects.CR.Standalone.Location.cDiscountSubID),
          typeof (PX.Objects.CR.Standalone.Location.cFreightAcctID),
          typeof (PX.Objects.CR.Standalone.Location.cFreightSubID),
          typeof (PX.Objects.CR.Standalone.Location.cSalesSubID),
          typeof (PX.Objects.CR.Standalone.Location.cSalesAcctID),
          typeof (PX.Objects.CR.Standalone.Location.cARAccountID),
          typeof (PX.Objects.CR.Standalone.Location.cARSubID),
          typeof (PX.Objects.CR.Standalone.Location.cRetainageAcctID),
          typeof (PX.Objects.CR.Standalone.Location.cRetainageSubID),
          typeof (PX.Objects.CR.Standalone.Location.cPriceClassID)
        };
      }
    }

    [PXDBInt]
    [PXDBChildIdentity(typeof (PX.Objects.CR.Standalone.Location.locationID))]
    [PXUIField]
    [PXSelector(typeof (Search<PX.Objects.CR.Location.locationID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<Customer.bAccountID>>>>), DescriptionField = typeof (PX.Objects.CR.Location.locationCD), DirtyRead = true)]
    [PXMergeAttributes]
    protected override void _(PX.Data.Events.CacheAttached<Customer.defLocationID> e)
    {
    }

    [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.CR.Standalone.Location.cAvalaraCustomerUsageType))]
    [PXUIField(DisplayName = "Tax Exemption Type", Required = true)]
    [TXAvalaraCustomerUsageType.List]
    [PXDefault("0", typeof (Search<PX.Objects.AR.CustomerClass.avalaraCustomerUsageType, Where<PX.Objects.AR.CustomerClass.customerClassID, Equal<Current<Customer.customerClassID>>>>))]
    protected virtual void _(
      PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cAvalaraCustomerUsageType> e)
    {
    }

    [Account(null, typeof (Search<PX.Objects.GL.Account.accountID, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.active, Equal<True>, And<Where<Current<GLSetup.ytdNetIncAccountID>, IsNull, Or<PX.Objects.GL.Account.accountID, NotEqual<Current<GLSetup.ytdNetIncAccountID>>>>>>>>), DisplayName = "AP Account", Required = true)]
    [PXMergeAttributes]
    protected override void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vAPAccountID> e)
    {
    }

    [Account]
    [PXMergeAttributes]
    protected override void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.vRetainageAcctID> e)
    {
    }

    [PXActiveCarrierSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), new System.Type[] {typeof (PX.Objects.CS.Carrier.carrierID), typeof (PX.Objects.CS.Carrier.description), typeof (PX.Objects.CS.Carrier.isExternal), typeof (PX.Objects.CS.Carrier.confirmationRequired)}, CacheGlobal = true, DescriptionField = typeof (PX.Objects.CS.Carrier.description))]
    [PXUIField(DisplayName = "Ship Via")]
    [PXMergeAttributes]
    protected override void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Standalone.Location.cCarrierID> e)
    {
    }

    [PXMergeAttributes]
    [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
    public virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Address.latitude> e)
    {
    }

    [PXMergeAttributes]
    [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
    public virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Address.longitude> e)
    {
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cSalesSubID> e)
    {
      PX.Objects.CR.BAccount current = (PX.Objects.CR.BAccount) this.Base.BAccountAccessor.Current;
      if (e.Row == null || current == null)
        return;
      if (current.IsBranch.GetValueOrDefault())
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cSalesSubID>, PX.Objects.CR.Standalone.Location, object>) e).NewValue = (object) e.Row.CMPSalesSubID;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cSalesSubID>>) e).Cancel = true;
      }
      else
        this.DefaultFrom<PX.Objects.AR.CustomerClass.salesSubID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cSalesSubID>>) e).Args, ((PXSelectBase) this.Base.CustomerClass).Cache, false);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cDiscountSubID> e)
    {
      PX.Objects.CR.BAccount current = (PX.Objects.CR.BAccount) this.Base.BAccountAccessor.Current;
      if (e.Row == null || current == null)
        return;
      if (current.IsBranch.GetValueOrDefault())
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cDiscountSubID>, PX.Objects.CR.Standalone.Location, object>) e).NewValue = (object) e.Row.CMPDiscountSubID;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cDiscountSubID>>) e).Cancel = true;
      }
      else
        this.DefaultFrom<PX.Objects.AR.CustomerClass.discountSubID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cDiscountSubID>>) e).Args, ((PXSelectBase) this.Base.CustomerClass).Cache, false);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cFreightSubID> e)
    {
      PX.Objects.CR.BAccount current = (PX.Objects.CR.BAccount) this.Base.BAccountAccessor.Current;
      if (e.Row == null || current == null)
        return;
      if (current.IsBranch.GetValueOrDefault())
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cFreightSubID>, PX.Objects.CR.Standalone.Location, object>) e).NewValue = (object) e.Row.CMPFreightSubID;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cFreightSubID>>) e).Cancel = true;
      }
      else
        this.DefaultFrom<PX.Objects.AR.CustomerClass.freightSubID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cFreightSubID>>) e).Args, ((PXSelectBase) this.Base.CustomerClass).Cache, false);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cTaxZoneID> e)
    {
      PX.Objects.CR.BAccount current = (PX.Objects.CR.BAccount) this.Base.BAccountAccessor.Current;
      if (e.Row == null || current == null)
        return;
      if (current.IsBranch.GetValueOrDefault())
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cTaxZoneID>, PX.Objects.CR.Standalone.Location, object>) e).NewValue = (object) e.Row.VTaxZoneID;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cTaxZoneID>>) e).Cancel = true;
      }
      else
        this.DefaultFrom<PX.Objects.AR.CustomerClass.taxZoneID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cTaxZoneID>>) e).Args, ((PXSelectBase) this.Base.CustomerClass).Cache, false);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cShipComplete> e)
    {
      PX.Objects.CR.BAccount current = (PX.Objects.CR.BAccount) this.Base.BAccountAccessor.Current;
      if (e.Row == null || current == null)
        return;
      if (current.IsBranch.GetValueOrDefault())
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cShipComplete>, PX.Objects.CR.Standalone.Location, object>) e).NewValue = (object) e.Row.CShipComplete;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cShipComplete>>) e).Cancel = true;
      }
      else
        this.DefaultFrom<PX.Objects.AR.CustomerClass.shipComplete>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cShipComplete>>) e).Args, ((PXSelectBase) this.Base.CustomerClass).Cache, true, (object) "L");
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cSalesAcctID> e)
    {
      this.DefaultFrom<PX.Objects.AR.CustomerClass.salesAcctID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cSalesAcctID>>) e).Args, ((PXSelectBase) this.Base.CustomerClass).Cache, false);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cPriceClassID> e)
    {
      this.DefaultFrom<PX.Objects.AR.CustomerClass.priceClassID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cPriceClassID>>) e).Args, ((PXSelectBase) this.Base.CustomerClass).Cache, false);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cRetainageAcctID> e)
    {
      this.DefaultFrom<PX.Objects.AR.CustomerClass.retainageAcctID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cRetainageAcctID>>) e).Args, ((PXSelectBase) this.Base.CustomerClass).Cache, false);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cRetainageSubID> e)
    {
      this.DefaultFrom<PX.Objects.AR.CustomerClass.retainageSubID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cRetainageSubID>>) e).Args, ((PXSelectBase) this.Base.CustomerClass).Cache, false);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cDiscountAcctID> e)
    {
      this.DefaultFrom<PX.Objects.AR.CustomerClass.discountAcctID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cDiscountAcctID>>) e).Args, ((PXSelectBase) this.Base.CustomerClass).Cache, false);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cFreightAcctID> e)
    {
      this.DefaultFrom<PX.Objects.AR.CustomerClass.freightAcctID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cFreightAcctID>>) e).Args, ((PXSelectBase) this.Base.CustomerClass).Cache, false);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cARAccountID> e)
    {
      this.DefaultFrom<PX.Objects.AR.CustomerClass.aRAcctID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cARAccountID>>) e).Args, ((PXSelectBase) this.Base.CustomerClass).Cache);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cARSubID> e)
    {
      this.DefaultFrom<PX.Objects.AR.CustomerClass.aRSubID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cARSubID>>) e).Args, ((PXSelectBase) this.Base.CustomerClass).Cache);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cCarrierID> e)
    {
      this.DefaultFrom<PX.Objects.AR.CustomerClass.shipVia>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cCarrierID>>) e).Args, ((PXSelectBase) this.Base.CustomerClass).Cache);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cShipTermsID> e)
    {
      this.DefaultFrom<PX.Objects.AR.CustomerClass.shipTermsID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PX.Objects.CR.Standalone.Location, PX.Objects.CR.Standalone.Location.cShipTermsID>>) e).Args, ((PXSelectBase) this.Base.CustomerClass).Cache);
    }

    protected override void _(PX.Data.Events.RowSelected<PX.Objects.CR.Standalone.Location> e)
    {
      base._(e);
      PX.Objects.CR.Standalone.Location row = e.Row;
      if (row == null || !row.IsDefault.GetValueOrDefault() || PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.DefLocation).Select(Array.Empty<object>())) == null || ((PXSelectBase<PX.Objects.AR.CustomerClass>) this.Base.CustomerClass).Current == null)
        return;
      bool valueOrDefault = ((PXSelectBase<PX.Objects.AR.CustomerClass>) this.Base.CustomerClass).Current.RequireTaxZone.GetValueOrDefault();
      PXDefaultAttribute.SetPersistingCheck<PX.Objects.CR.Standalone.Location.cTaxZoneID>(((PXSelectBase) this.DefLocation).Cache, (object) row, valueOrDefault ? (PXPersistingCheck) 0 : (PXPersistingCheck) 2);
      PXUIFieldAttribute.SetRequired<PX.Objects.CR.Standalone.Location.cTaxZoneID>(((PXSelectBase) this.DefLocation).Cache, valueOrDefault);
      if (!valueOrDefault || !string.IsNullOrEmpty(row.CTaxZoneID))
        return;
      GraphHelper.MarkUpdated(((PXSelectBase) this.DefLocation).Cache, (object) row);
    }

    protected override void _(PX.Data.Events.RowInserted<Customer> e, PXRowInserted del)
    {
      Customer row = e.Row;
      if (row != null)
      {
        // ISSUE: method pointer
        PXRowInserting pxRowInserting = new PXRowInserting((object) this, __methodptr(\u003C_\u003Eb__25_0));
        if (((PXSelectBase<PX.Objects.AR.CustomerClass>) this.Base.CustomerClass).Current != null && ((PXSelectBase<PX.Objects.AR.CustomerClass>) this.Base.CustomerClass).Current.DefaultLocationCDFromBranch.GetValueOrDefault())
          ((PXGraph) this.Base).RowInserting.AddHandler<PX.Objects.CR.Standalone.Location>(pxRowInserting);
        this.InsertLocation(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<Customer>>) e).Cache, row);
        if (((PXSelectBase<PX.Objects.AR.CustomerClass>) this.Base.CustomerClass).Current != null && ((PXSelectBase<PX.Objects.AR.CustomerClass>) this.Base.CustomerClass).Current.DefaultLocationCDFromBranch.GetValueOrDefault())
          ((PXGraph) this.Base).RowInserting.RemoveHandler<PX.Objects.CR.Standalone.Location>(pxRowInserting);
      }
      del?.Invoke(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<Customer>>) e).Cache, ((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<Customer>>) e).Args);
    }

    protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.CR.Standalone.Location> e)
    {
      if (e.Cancel)
        return;
      PX.Objects.CR.Standalone.Location row = e.Row;
      if (row == null)
        return;
      int num;
      if (!(PXSelectorAttribute.Select<PX.Objects.CR.Standalone.Location.cCarrierID>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.CR.Standalone.Location>>) e).Cache, (object) row) is PX.Objects.CS.Carrier carrier))
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
        ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.CR.Standalone.Location>>) e).Cache.RaiseExceptionHandling<PX.Objects.CR.Standalone.Location.cCarrierID>((object) row, (object) row.CCarrierID, (Exception) new PXSetPropertyException((IBqlTable) row, "The Ship Via code is not active.", (PXErrorLevel) 2));
      this.ValidateLocation(((PXSelectBase) this.DefLocation).Cache, row);
      this.VerifyAvalaraUsageType(((PXSelectBase<PX.Objects.AR.CustomerClass>) this.Base.CustomerClass).Current, row);
    }

    protected override void _(PX.Data.Events.RowPersisted<PX.Objects.CR.Standalone.Location> e)
    {
      base._(e);
      if (e.TranStatus != 1)
        return;
      DiscountEngine.RemoveFromCachedCustomerPriceClasses(e.Row.BAccountID);
    }

    protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.CR.Standalone.Location> e)
    {
      PX.Objects.AR.CustomerClass current = ((PXSelectBase<PX.Objects.AR.CustomerClass>) this.Base.CustomerClass).Current;
      int? nullable1;
      int num;
      if (current == null)
      {
        num = 0;
      }
      else
      {
        nullable1 = current.SalesAcctID;
        num = nullable1.HasValue ? 1 : 0;
      }
      if (num != 0)
        return;
      PX.Objects.CR.Standalone.Location original = (PX.Objects.CR.Standalone.Location) ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.CR.Standalone.Location>>) e).Cache.GetOriginal((object) e.Row);
      if (original == null)
        return;
      nullable1 = original.CSalesAcctID;
      int? nullable2 = e.Row.CSalesAcctID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        nullable2 = original.CSalesSubID;
        nullable1 = e.Row.CSalesSubID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          return;
      }
      CustomerMaint.LocationDetailsExt extension = ((PXGraph) this.Base).GetExtension<CustomerMaint.LocationDetailsExt>();
      foreach (PXResult<PX.Objects.CR.Standalone.Location> pxResult in ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension.Locations).Select(Array.Empty<object>()))
      {
        PX.Objects.CR.Standalone.Location location = PXResult<PX.Objects.CR.Standalone.Location>.op_Implicit(pxResult);
        bool flag = false;
        if (!location.IsDefault.GetValueOrDefault())
        {
          nullable1 = location.CSalesAcctID;
          if (!nullable1.HasValue)
          {
            nullable1 = original.CSalesAcctID;
            nullable2 = e.Row.CSalesAcctID;
            if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
            {
              location.CSalesAcctID = e.Row.CSalesAcctID;
              flag = true;
            }
          }
          nullable2 = location.CSalesSubID;
          if (!nullable2.HasValue)
          {
            nullable2 = original.CSalesSubID;
            nullable1 = e.Row.CSalesSubID;
            if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
            {
              location.CSalesSubID = e.Row.CSalesSubID;
              flag = true;
            }
          }
          if (flag)
            ((PXSelectBase) extension.Locations).Cache.Update((object) location);
        }
      }
    }

    public virtual void VerifyAvalaraUsageType(PX.Objects.AR.CustomerClass customerClass, PX.Objects.CR.Standalone.Location location)
    {
      if (customerClass != null && customerClass.RequireAvalaraCustomerUsageType.GetValueOrDefault() && location.CAvalaraCustomerUsageType == "0")
        throw new PXRowPersistingException(typeof (PX.Objects.CR.Standalone.Location.cAvalaraCustomerUsageType).Name, (object) location.CAvalaraCustomerUsageType, "Select the entity usage type other than Default.");
    }

    public override bool ValidateLocation(PXCache cache, PX.Objects.CR.Standalone.Location location)
    {
      bool flag = true;
      Customer current = this.Base.BAccountAccessor.Current;
      if (current != null && (current.Type == "CU" || current.Type == "VC"))
        flag &= this.locationValidator.ValidateCustomerLocation(cache, (PX.Objects.CR.BAccount) current, (ILocation) location);
      return flag;
    }
  }

  /// <exclude />
  public class ContactDetailsExt : 
    BusinessAccountContactDetailsExt<CustomerMaint, CustomerMaint.CreateContactFromCustomerGraphExt, Customer, Customer.bAccountID, Customer.acctName>
  {
  }

  /// <exclude />
  public class LocationDetailsExt : PX.Objects.CR.Extensions.LocationDetailsExt<CustomerMaint, Customer, Customer.bAccountID>
  {
    [PXOverride]
    public virtual void ChangeBAccountType(
      PX.Objects.CR.BAccount descendantEntity,
      string type,
      System.Action<PX.Objects.CR.BAccount, string> del)
    {
      if (del != null)
        del(descendantEntity, type);
      foreach (PXResult<PX.Objects.CR.Standalone.Location> pxResult in ((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.Locations).Select(Array.Empty<object>()))
      {
        PX.Objects.CR.Standalone.Location location = PXResult<PX.Objects.CR.Standalone.Location>.op_Implicit(pxResult);
        location.LocType = type;
        if (!BAccountType.ActsAsCustomer(type))
        {
          location.CSalesAcctID = new int?();
          location.CSalesSubID = new int?();
          location.CARAccountID = new int?();
          location.CARAccountLocationID = location.LocationID;
          location.CARSubID = new int?();
          location.CDefProjectID = new int?();
          location.CDiscountAcctID = new int?();
          location.CDiscountSubID = new int?();
          location.CFreightAcctID = new int?();
          location.CFreightSubID = new int?();
          location.CPriceClassID = (string) null;
          location.CRetainageAcctID = new int?();
          location.CRetainageSubID = new int?();
          location.CTaxCalcMode = "T";
          location.CTaxZoneID = (string) null;
        }
        ((PXSelectBase<PX.Objects.CR.Standalone.Location>) this.Locations).Update(location);
      }
    }
  }

  /// <exclude />
  public class PrimaryContactGraphExt : 
    CRPrimaryContactGraphExt<CustomerMaint, CustomerMaint.ContactDetailsExt, Customer, Customer.bAccountID, Customer.primaryContactID>
  {
    protected override PXView ContactsView
    {
      get => ((PXSelectBase) this.ContactDetailsExtension.Contacts).View;
    }

    [PXUIField(DisplayName = "Name")]
    [PXMergeAttributes]
    protected virtual void _(PX.Data.Events.CacheAttached<Customer.primaryContactID> e)
    {
    }

    protected virtual void _(PX.Data.Events.FieldUpdated<Customer, Customer.acctName> e)
    {
      Customer row = e.Row;
      if (!row.PrimaryContactID.HasValue)
        return;
      PX.Objects.CR.Contact contact = ((PXSelectBase<PX.Objects.CR.Contact>) this.PrimaryContactCurrent).SelectSingle(Array.Empty<object>());
      if (contact == null || row.AcctName == null || row.AcctName.Equals(contact.FullName))
        return;
      contact.FullName = row.AcctName;
      ((PXSelectBase<PX.Objects.CR.Contact>) this.PrimaryContactCurrent).Update(contact);
    }
  }

  /// <exclude />
  public class CustomerMaintAddressLookupExtension : 
    AddressLookupExtension<CustomerMaint, Customer, PX.Objects.CR.Address>
  {
    protected override string AddressView => "DefAddress";

    protected override string ViewOnMap => "ViewMainOnMap";
  }

  /// <exclude />
  public class CustomerMaintBillingAddressLookupExtension : 
    AddressLookupExtension<CustomerMaint, Customer, PX.Objects.CR.Address>
  {
    protected override string AddressView => "BillAddress";

    protected override string ViewOnMap => "viewBillAddressOnMap";
  }

  /// <exclude />
  public class CustomerMaintDefLocationAddressLookupExtension : 
    AddressLookupExtension<CustomerMaint, Customer, PX.Objects.CR.Address>
  {
    protected override string AddressView => "DefLocationAddress";

    protected override string ViewOnMap => "ViewDefLocationAddressOnMap";
  }

  /// <exclude />
  public class ExtendToVendor : ExtendToVendorGraph<CustomerMaint, Customer>
  {
    protected override ExtendToVendorGraph<CustomerMaint, Customer>.SourceAccountMapping GetSourceAccountMapping()
    {
      return new ExtendToVendorGraph<CustomerMaint, Customer>.SourceAccountMapping(typeof (Customer));
    }
  }

  /// <exclude />
  public class CustomerBillSharedContactOverrideGraphExt : 
    SharedChildOverrideGraphExt<CustomerMaint, CustomerMaint.CustomerBillSharedContactOverrideGraphExt>
  {
    public override bool ViewHasADelegate => true;

    protected override CRParentChild<CustomerMaint, CustomerMaint.CustomerBillSharedContactOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<CustomerMaint, CustomerMaint.CustomerBillSharedContactOverrideGraphExt>.DocumentMapping(typeof (Customer))
      {
        RelatedID = typeof (Customer.bAccountID),
        ChildID = typeof (Customer.defBillContactID),
        IsOverrideRelated = typeof (Customer.overrideBillContact)
      };
    }

    protected override SharedChildOverrideGraphExt<CustomerMaint, CustomerMaint.CustomerBillSharedContactOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<CustomerMaint, CustomerMaint.CustomerBillSharedContactOverrideGraphExt>.RelatedMapping(typeof (Customer))
      {
        RelatedID = typeof (Customer.bAccountID),
        ChildID = typeof (Customer.defContactID)
      };
    }

    protected override CRParentChild<CustomerMaint, CustomerMaint.CustomerBillSharedContactOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<CustomerMaint, CustomerMaint.CustomerBillSharedContactOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Contact))
      {
        ChildID = typeof (PX.Objects.CR.Contact.contactID),
        RelatedID = typeof (PX.Objects.CR.Contact.bAccountID)
      };
    }
  }

  /// <exclude />
  public class CustomerBillSharedAddressOverrideGraphExt : 
    SharedChildOverrideGraphExt<CustomerMaint, CustomerMaint.CustomerBillSharedAddressOverrideGraphExt>
  {
    public override bool ViewHasADelegate => true;

    protected override CRParentChild<CustomerMaint, CustomerMaint.CustomerBillSharedAddressOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<CustomerMaint, CustomerMaint.CustomerBillSharedAddressOverrideGraphExt>.DocumentMapping(typeof (Customer))
      {
        RelatedID = typeof (Customer.bAccountID),
        ChildID = typeof (Customer.defBillAddressID),
        IsOverrideRelated = typeof (Customer.overrideBillAddress)
      };
    }

    protected override SharedChildOverrideGraphExt<CustomerMaint, CustomerMaint.CustomerBillSharedAddressOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<CustomerMaint, CustomerMaint.CustomerBillSharedAddressOverrideGraphExt>.RelatedMapping(typeof (Customer))
      {
        RelatedID = typeof (Customer.bAccountID),
        ChildID = typeof (Customer.defAddressID)
      };
    }

    protected override CRParentChild<CustomerMaint, CustomerMaint.CustomerBillSharedAddressOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<CustomerMaint, CustomerMaint.CustomerBillSharedAddressOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Address))
      {
        ChildID = typeof (PX.Objects.CR.Address.addressID),
        RelatedID = typeof (PX.Objects.CR.Address.bAccountID)
      };
    }
  }

  /// <exclude />
  public class CustomerDefSharedContactOverrideGraphExt : 
    SharedChildOverrideGraphExt<CustomerMaint, CustomerMaint.CustomerDefSharedContactOverrideGraphExt>
  {
    public override bool ViewHasADelegate => true;

    protected override CRParentChild<CustomerMaint, CustomerMaint.CustomerDefSharedContactOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<CustomerMaint, CustomerMaint.CustomerDefSharedContactOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Standalone.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Standalone.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Standalone.Location.defContactID),
        IsOverrideRelated = typeof (PX.Objects.CR.Standalone.Location.overrideContact)
      };
    }

    protected override SharedChildOverrideGraphExt<CustomerMaint, CustomerMaint.CustomerDefSharedContactOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<CustomerMaint, CustomerMaint.CustomerDefSharedContactOverrideGraphExt>.RelatedMapping(typeof (Customer))
      {
        RelatedID = typeof (Customer.bAccountID),
        ChildID = typeof (Customer.defContactID)
      };
    }

    protected override CRParentChild<CustomerMaint, CustomerMaint.CustomerDefSharedContactOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<CustomerMaint, CustomerMaint.CustomerDefSharedContactOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Contact))
      {
        ChildID = typeof (PX.Objects.CR.Contact.contactID),
        RelatedID = typeof (PX.Objects.CR.Contact.bAccountID)
      };
    }
  }

  /// <exclude />
  public class CustomerDefSharedAddressOverrideGraphExt : 
    SharedChildOverrideGraphExt<CustomerMaint, CustomerMaint.CustomerDefSharedAddressOverrideGraphExt>
  {
    public override bool ViewHasADelegate => true;

    protected override CRParentChild<CustomerMaint, CustomerMaint.CustomerDefSharedAddressOverrideGraphExt>.DocumentMapping GetDocumentMapping()
    {
      return new CRParentChild<CustomerMaint, CustomerMaint.CustomerDefSharedAddressOverrideGraphExt>.DocumentMapping(typeof (PX.Objects.CR.Standalone.Location))
      {
        RelatedID = typeof (PX.Objects.CR.Standalone.Location.bAccountID),
        ChildID = typeof (PX.Objects.CR.Standalone.Location.defAddressID),
        IsOverrideRelated = typeof (PX.Objects.CR.Standalone.Location.overrideAddress)
      };
    }

    protected override SharedChildOverrideGraphExt<CustomerMaint, CustomerMaint.CustomerDefSharedAddressOverrideGraphExt>.RelatedMapping GetRelatedMapping()
    {
      return new SharedChildOverrideGraphExt<CustomerMaint, CustomerMaint.CustomerDefSharedAddressOverrideGraphExt>.RelatedMapping(typeof (Customer))
      {
        RelatedID = typeof (Customer.bAccountID),
        ChildID = typeof (Customer.defAddressID)
      };
    }

    protected override CRParentChild<CustomerMaint, CustomerMaint.CustomerDefSharedAddressOverrideGraphExt>.ChildMapping GetChildMapping()
    {
      return new CRParentChild<CustomerMaint, CustomerMaint.CustomerDefSharedAddressOverrideGraphExt>.ChildMapping(typeof (PX.Objects.CR.Address))
      {
        ChildID = typeof (PX.Objects.CR.Address.addressID),
        RelatedID = typeof (PX.Objects.CR.Address.bAccountID)
      };
    }
  }

  /// <exclude />
  public class CreateContactFromCustomerGraphExt : CRCreateContactActionBase<CustomerMaint, Customer>
  {
    protected override PXSelectBase<CRPMTimeActivity> Activities
    {
      get
      {
        return (PXSelectBase<CRPMTimeActivity>) ((PXGraph) this.Base).GetExtension<CustomerMaint_ActivityDetailsExt>().Activities;
      }
    }

    public override void Initialize()
    {
      base.Initialize();
      this.Addresses = new PXSelectExtension<DocumentAddress>((PXSelectBase) ((PXGraph) this.Base).GetExtension<CustomerMaint.DefContactAddressExt>().DefAddress);
      this.Contacts = new PXSelectExtension<DocumentContact>((PXSelectBase) ((PXGraph) this.Base).GetExtension<CustomerMaint.DefContactAddressExt>().DefContact);
    }

    protected override CRCreateActionBaseInit<CustomerMaint, Customer>.DocumentContactMapping GetDocumentContactMapping()
    {
      return new CRCreateActionBaseInit<CustomerMaint, Customer>.DocumentContactMapping(typeof (PX.Objects.CR.Contact))
      {
        Email = typeof (PX.Objects.CR.Contact.eMail)
      };
    }

    protected override CRCreateActionBaseInit<CustomerMaint, Customer>.DocumentAddressMapping GetDocumentAddressMapping()
    {
      return new CRCreateActionBaseInit<CustomerMaint, Customer>.DocumentAddressMapping(typeof (PX.Objects.CR.Address));
    }

    public virtual void _(PX.Data.Events.RowSelected<ContactFilter> e)
    {
      PXUIFieldAttribute.SetReadOnly<ContactFilter.fullName>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ContactFilter>>) e).Cache, (object) e.Row, true);
    }

    public virtual void _(
      PX.Data.Events.FieldDefaulting<ContactFilter, ContactFilter.fullName> e)
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ContactFilter, ContactFilter.fullName>, ContactFilter, object>) e).NewValue = (object) ((PXSelectBase<DocumentContact>) this.Contacts).SelectSingle(Array.Empty<object>())?.FullName;
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

  public class CustomerMaint_CRDuplicateBAccountIdentifier : 
    CRDuplicateBAccountIdentifier<CustomerMaint, Customer>
  {
  }
}
