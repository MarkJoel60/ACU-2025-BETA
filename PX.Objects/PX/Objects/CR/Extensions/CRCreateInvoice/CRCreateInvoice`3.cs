// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateInvoice.CRCreateInvoice`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.Common.Discount;
using PX.Objects.CS;
using PX.Objects.Extensions.Discount;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR.Extensions.CRCreateInvoice;

public abstract class CRCreateInvoice<TDiscountExt, TGraph, TMaster> : PXGraphExtension<TGraph>
  where TDiscountExt : DiscountGraph<TGraph, TMaster>, new()
  where TGraph : PXGraph, new()
  where TMaster : class, IBqlTable, new()
{
  public PXSelectExtension<Document> DocumentView;
  [PXCopyPasteHiddenView]
  [PXViewName("Create Invoice")]
  public CRValidationFilter<CreateInvoicesFilter> CreateInvoicesParams;
  public PXAction<TMaster> CreateInvoice;

  public TDiscountExt DiscountExtension { get; private set; }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.DiscountExtension = this.Base.GetExtension<TDiscountExt>() ?? throw new PXException("The graph does not have defined extension: {0}.", new object[1]
    {
      (object) typeof (TDiscountExt).Name
    });
  }

  protected virtual PX.Objects.CR.Extensions.CRCreateInvoice.CRCreateInvoice<TDiscountExt, TGraph, TMaster>.DocumentMapping GetDocumentMapping()
  {
    return new PX.Objects.CR.Extensions.CRCreateInvoice.CRCreateInvoice<TDiscountExt, TGraph, TMaster>.DocumentMapping(typeof (TMaster));
  }

  protected static bool IsExtensionActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.financialModule>();
  }

  public virtual void _(PX.Data.Events.RowUpdated<CreateInvoicesFilter> e)
  {
    CreateInvoicesFilter row = e.Row;
    if (row == null)
      return;
    if (!row.RecalculatePrices.GetValueOrDefault())
      ((PXSelectBase) this.CreateInvoicesParams).Cache.SetValue<CreateInvoicesFilter.overrideManualPrices>((object) row, (object) false);
    if (row.RecalculateDiscounts.GetValueOrDefault())
      return;
    ((PXSelectBase) this.CreateInvoicesParams).Cache.SetValue<CreateInvoicesFilter.overrideManualDiscounts>((object) row, (object) false);
    ((PXSelectBase) this.CreateInvoicesParams).Cache.SetValue<CreateInvoicesFilter.overrideManualDocGroupDiscounts>((object) row, (object) false);
  }

  public virtual void _(PX.Data.Events.RowSelected<CreateInvoicesFilter> e)
  {
    CreateInvoicesFilter row = e.Row;
    Document current = ((PXSelectBase<Document>) this.DocumentView).Current;
    if (row == null || current == null)
      return;
    PXCache cache1 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CreateInvoicesFilter>>) e).Cache;
    CreateInvoicesFilter createInvoicesFilter1 = row;
    bool? nullable = row.RecalculatePrices;
    int num1 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CreateInvoicesFilter.overrideManualPrices>(cache1, (object) createInvoicesFilter1, num1 != 0);
    PXCache cache2 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CreateInvoicesFilter>>) e).Cache;
    CreateInvoicesFilter createInvoicesFilter2 = row;
    nullable = row.RecalculateDiscounts;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CreateInvoicesFilter.overrideManualDiscounts>(cache2, (object) createInvoicesFilter2, num2 != 0);
    PXCache cache3 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CreateInvoicesFilter>>) e).Cache;
    CreateInvoicesFilter createInvoicesFilter3 = row;
    nullable = row.RecalculateDiscounts;
    int num3 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CreateInvoicesFilter.overrideManualDocGroupDiscounts>(cache3, (object) createInvoicesFilter3, num3 != 0);
    PXCache cache4 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CreateInvoicesFilter>>) e).Cache;
    CreateInvoicesFilter createInvoicesFilter4 = row;
    nullable = current.ManualTotalEntry;
    int num4 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetVisible<CreateInvoicesFilter.confirmManualAmount>(cache4, (object) createInvoicesFilter4, num4 != 0);
    PXCache cache5 = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CreateInvoicesFilter>>) e).Cache;
    CreateInvoicesFilter createInvoicesFilter5 = row;
    nullable = current.IsPrimary;
    bool flag1 = false;
    int num5 = nullable.GetValueOrDefault() == flag1 & nullable.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CreateInvoicesFilter.makeQuotePrimary>(cache5, (object) createInvoicesFilter5, num5 != 0);
    Numbering numbering = Numbering.PK.Find((PXGraph) this.Base, PXResultset<ARSetup>.op_Implicit(PXSelectBase<ARSetup, PXSelect<ARSetup>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))?.InvoiceNumberingID);
    int num6;
    if (numbering == null)
    {
      num6 = 0;
    }
    else
    {
      nullable = numbering.UserNumbering;
      num6 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    bool flag2 = num6 != 0;
    PXUIFieldAttribute.SetVisible<CreateInvoicesFilter.refNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CreateInvoicesFilter>>) e).Cache, (object) row, flag2);
    PXDefaultAttribute.SetPersistingCheck<CreateInvoicesFilter.refNbr>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CreateInvoicesFilter>>) e).Cache, (object) row, flag2 ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  public virtual void _(
    PX.Data.Events.FieldVerifying<CreateInvoicesFilter, CreateInvoicesFilter.confirmManualAmount> e)
  {
    CreateInvoicesFilter row = e.Row;
    Document current = ((PXSelectBase<Document>) this.DocumentView).Current;
    if (row == null || current == null)
      return;
    bool? nullable = current.ManualTotalEntry;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CreateInvoicesFilter, CreateInvoicesFilter.confirmManualAmount>, CreateInvoicesFilter, object>) e).NewValue as bool?;
    if (!nullable.GetValueOrDefault())
    {
      ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<CreateInvoicesFilter, CreateInvoicesFilter.confirmManualAmount>>) e).Cache.RaiseExceptionHandling<CreateInvoicesFilter.confirmManualAmount>((object) row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<CreateInvoicesFilter, CreateInvoicesFilter.confirmManualAmount>, CreateInvoicesFilter, object>) e).NewValue, (Exception) new PXSetPropertyException("Select this check box to confirm that you want to ignore the manual amount specified for the opportunity and create a sales order for the opportunity products.", (PXErrorLevel) 4));
      throw new PXSetPropertyException("Select this check box to confirm that you want to ignore the manual amount specified for the opportunity and create a sales order for the opportunity products.", (PXErrorLevel) 4);
    }
  }

  public virtual void _(
    PX.Data.Events.FieldUpdated<CreateInvoicesFilter, CreateInvoicesFilter.confirmManualAmount> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CreateInvoicesFilter, CreateInvoicesFilter.confirmManualAmount>>) e).Cache.RaiseExceptionHandling<CreateInvoicesFilter.confirmManualAmount>((object) e.Row, (object) null, (Exception) null);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable createInvoice(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PX.Objects.CR.Extensions.CRCreateInvoice.CRCreateInvoice<TDiscountExt, TGraph, TMaster>.\u003C\u003Ec__DisplayClass15_0 cDisplayClass150 = new PX.Objects.CR.Extensions.CRCreateInvoice.CRCreateInvoice<TDiscountExt, TGraph, TMaster>.\u003C\u003Ec__DisplayClass15_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass150.masterEntity = ((PXSelectBase<Document>) this.DocumentView).Current;
    Customer customer = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Current<Document.bAccountID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
    {
      (object) ((PXSelectBase<Document>) this.DocumentView).Current
    }, Array.Empty<object>()));
    if (customer == null)
      throw new PXException("Prior to creating an invoice or sales order, the opportunity must be associated with a customer account.");
    // ISSUE: reference to a compiler-generated field
    if (!NonGenericIEnumerableExtensions.Any_((IEnumerable) PXSelectBase<CROpportunityProducts, PXSelectJoin<CROpportunityProducts, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<CROpportunityProducts.inventoryID>>>, Where<PX.Objects.IN.InventoryItem.stkItem, Equal<False>, And<CROpportunityProducts.quoteID, Equal<Required<Document.quoteID>>>>>.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
    {
      (object) ((PXSelectBase<Document>) this.DocumentView).Current
    }, new object[1]
    {
      ((PXSelectBase<Document>) this.DocumentView).GetValueExt<Document.quoteID>(cDisplayClass150.masterEntity)
    })))
      throw new PXException("The opportunity contains one or multiple product lines with no non-stock inventory item specified. If you want to create an invoice, you may need to first review the product lines and specify non-stock inventory items where necessary because only lines with non-stock inventory items can be included in invoices.");
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass150.masterEntity.BAccountID.HasValue)
    {
      // ISSUE: reference to a compiler-generated field
      cDisplayClass150.baccount = PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelectJoin<PX.Objects.CR.BAccount, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.BAccount.defContactID>>>, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<Document.bAccountID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
      {
        (object) ((PXSelectBase<Document>) this.DocumentView).Current
      }, Array.Empty<object>()));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass150.baccount.Type == "VE" || cDisplayClass150.baccount.Type == "PR")
      {
        // ISSUE: reference to a compiler-generated field
        if (((PXSelectBase) this.DocumentView).View.Ask((object) cDisplayClass150.masterEntity, "Confirmation", "A customer account is required for creating an invoice. Would you like to convert the specified business account to a customer account?", (MessageButtons) 4, (MessageIcon) 2) == 6)
        {
          // ISSUE: reference to a compiler-generated field
          cDisplayClass150.graph = this.Base.CloneGraphState<TGraph>();
          // ISSUE: method pointer
          PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass150, __methodptr(\u003CcreateInvoice\u003Eb__1)));
        }
        return adapter.Get();
      }
    }
    if (customer != null)
    {
      if (((PXSelectBase) this.CreateInvoicesParams).View.Answer == null)
      {
        ((PXSelectBase) this.CreateInvoicesParams).Cache.Clear();
        ((PXSelectBase) this.CreateInvoicesParams).Cache.Insert();
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      if (this.CreateInvoicesParams.AskExtFullyValid(PX.Objects.CR.Extensions.CRCreateInvoice.CRCreateInvoice<TDiscountExt, TGraph, TMaster>.\u003C\u003Ec.\u003C\u003E9__15_0 ?? (PX.Objects.CR.Extensions.CRCreateInvoice.CRCreateInvoice<TDiscountExt, TGraph, TMaster>.\u003C\u003Ec.\u003C\u003E9__15_0 = new PXView.InitializePanel((object) PX.Objects.CR.Extensions.CRCreateInvoice.CRCreateInvoice<TDiscountExt, TGraph, TMaster>.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CcreateInvoice\u003Eb__15_0))), (DialogAnswerType) 1, true))
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        PX.Objects.CR.Extensions.CRCreateInvoice.CRCreateInvoice<TDiscountExt, TGraph, TMaster>.\u003C\u003Ec__DisplayClass15_1 cDisplayClass151 = new PX.Objects.CR.Extensions.CRCreateInvoice.CRCreateInvoice<TDiscountExt, TGraph, TMaster>.\u003C\u003Ec__DisplayClass15_1();
        this.Base.Actions.PressSave();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass151.graph = this.Base.CloneGraphState<TGraph>();
        // ISSUE: method pointer
        PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass151, __methodptr(\u003CcreateInvoice\u003Eb__2)));
      }
    }
    return adapter.Get();
  }

  protected virtual void DoCreateInvoice()
  {
    CreateInvoicesFilter current1 = ((PXSelectBase<CreateInvoicesFilter>) this.CreateInvoicesParams).Current;
    Document current2 = ((PXSelectBase<Document>) this.DocumentView).Current;
    if (current1 == null || current2 == null)
      return;
    this.DoCreateInvoice(PXGraph.CreateInstance<ARInvoiceEntry>(), current2, current1);
  }

  protected virtual void DoCreateInvoice(
    ARInvoiceEntry docgraph,
    Document masterEntity,
    CreateInvoicesFilter filter)
  {
    this.InitializeInvoice(docgraph, masterEntity, filter);
    CRQuote workflowProcessing = this.GetQuoteForWorkflowProcessing();
    if (workflowProcessing != null)
      GraphHelper.Hold(((PXGraph) docgraph).Caches[typeof (CRQuote)], (object) workflowProcessing);
    if (!this.Base.IsContractBasedAPI)
      throw new PXRedirectRequiredException((PXGraph) docgraph, "");
    ((PXAction) docgraph.Save).Press();
  }

  public virtual Customer GetCustomer()
  {
    return PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Current<Document.bAccountID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
  }

  public virtual bool NeedRecalculate(Document masterEntity, CreateInvoicesFilter filter)
  {
    if (masterEntity.ManualTotalEntry.GetValueOrDefault())
      return false;
    return filter.RecalculatePrices.GetValueOrDefault() || filter.RecalculateDiscounts.GetValueOrDefault() || filter.OverrideManualDiscounts.GetValueOrDefault() || filter.OverrideManualDocGroupDiscounts.GetValueOrDefault() || filter.OverrideManualPrices.GetValueOrDefault();
  }

  public virtual ARInvoice InitializeInvoice(
    ARInvoiceEntry docgraph,
    Document masterEntity,
    CreateInvoicesFilter filter)
  {
    bool needRecalculate = this.NeedRecalculate(masterEntity, filter);
    Customer customer = this.GetCustomer();
    ARInvoice invoiceEntity = this.CreateInvoiceEntity(docgraph, masterEntity, filter, customer, needRecalculate);
    this.FillShippingDetails(docgraph, invoiceEntity);
    this.FillBillingDetails(docgraph, invoiceEntity);
    this.FillTaxDetails(docgraph, invoiceEntity, masterEntity, needRecalculate);
    this.FillRelations(docgraph, invoiceEntity);
    this.FillTransactions(docgraph, invoiceEntity, masterEntity, filter, customer);
    this.FillNotesAndFiles(docgraph, invoiceEntity, masterEntity);
    this.FillDiscounts(docgraph, invoiceEntity, masterEntity, filter);
    this.FillTaxes(docgraph, masterEntity, needRecalculate);
    this.RecalculateInvoiceDiscounts(docgraph, filter, needRecalculate);
    this.FillInvoiceEntity(docgraph, invoiceEntity, masterEntity, customer);
    return invoiceEntity;
  }

  public virtual ARInvoice CreateInvoiceEntity(
    ARInvoiceEntry docgraph,
    Document masterEntity,
    CreateInvoicesFilter filter,
    Customer customer,
    bool needRecalculate)
  {
    ((PXSelectBase<Customer>) docgraph.customer).Current = customer;
    ARInvoice arInvoice = new ARInvoice();
    arInvoice.DocType = "INV";
    if (!string.IsNullOrWhiteSpace(filter.RefNbr))
      arInvoice.RefNbr = filter.RefNbr;
    arInvoice.CuryID = masterEntity.CuryID;
    arInvoice.CuryInfoID = masterEntity.CuryInfoID;
    arInvoice.DocDate = masterEntity.CloseDate;
    ARInvoice copy = PXCache<ARInvoice>.CreateCopy(((PXSelectBase<ARInvoice>) docgraph.Document).Insert(arInvoice));
    copy.Hold = new bool?(true);
    copy.BranchID = masterEntity.BranchID;
    copy.CustomerID = masterEntity.BAccountID;
    copy.ProjectID = masterEntity.ProjectID;
    copy.CuryDocumentDiscTotal = masterEntity.ManualTotalEntry.GetValueOrDefault() ? new Decimal?(0M) : masterEntity.CuryDiscTot;
    ARInvoice invoiceEntity = ((PXSelectBase<ARInvoice>) docgraph.Document).Update(copy);
    invoiceEntity.TermsID = masterEntity.TermsID ?? customer.TermsID;
    invoiceEntity.InvoiceNbr = masterEntity.OpportunityID;
    invoiceEntity.DocDesc = masterEntity.Subject;
    invoiceEntity.CustomerLocationID = masterEntity.LocationID ?? customer.DefLocationID;
    return invoiceEntity;
  }

  public virtual void FillShippingDetails(ARInvoiceEntry docgraph, ARInvoice invoice)
  {
    CRShippingContact current1 = this.Base.Caches[typeof (CRShippingContact)].Current as CRShippingContact;
    ARShippingContact arShippingContact = PXResultset<ARShippingContact>.op_Implicit(((PXSelectBase<ARShippingContact>) docgraph.Shipping_Contact).Select(Array.Empty<object>()));
    int? nullable1;
    if (arShippingContact != null && current1 != null)
    {
      current1.RevisionID = current1.RevisionID ?? arShippingContact.RevisionID;
      int? revisionId = arShippingContact.RevisionID;
      nullable1 = current1.RevisionID;
      if (!(revisionId.GetValueOrDefault() == nullable1.GetValueOrDefault() & revisionId.HasValue == nullable1.HasValue))
        current1.IsDefaultContact = new bool?(false);
      CRShippingContact crShippingContact = current1;
      nullable1 = current1.BAccountContactID;
      int? nullable2 = nullable1 ?? arShippingContact.CustomerContactID;
      crShippingContact.BAccountContactID = nullable2;
      SharedRecordAttribute.CopyRecord<ARInvoice.shipContactID>(((PXSelectBase) docgraph.Document).Cache, (object) invoice, (object) current1, true);
    }
    CRShippingAddress current2 = this.Base.Caches[typeof (CRShippingAddress)].Current as CRShippingAddress;
    ARShippingAddress arShippingAddress = PXResultset<ARShippingAddress>.op_Implicit(((PXSelectBase<ARShippingAddress>) docgraph.Shipping_Address).Select(Array.Empty<object>()));
    if (arShippingAddress == null || current2 == null)
      return;
    CRShippingAddress crShippingAddress1 = current2;
    nullable1 = current2.RevisionID;
    int? nullable3 = nullable1 ?? arShippingAddress.RevisionID;
    crShippingAddress1.RevisionID = nullable3;
    nullable1 = arShippingAddress.RevisionID;
    int? nullable4 = current2.RevisionID;
    if (!(nullable1.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable1.HasValue == nullable4.HasValue))
      current2.IsDefaultAddress = new bool?(false);
    CRShippingAddress crShippingAddress2 = current2;
    nullable4 = current2.BAccountAddressID;
    int? nullable5 = nullable4 ?? arShippingAddress.CustomerAddressID;
    crShippingAddress2.BAccountAddressID = nullable5;
    SharedRecordAttribute.CopyRecord<ARInvoice.shipAddressID>(((PXSelectBase) docgraph.Document).Cache, (object) invoice, (object) current2, true);
  }

  public virtual void FillBillingDetails(ARInvoiceEntry docgraph, ARInvoice invoice)
  {
    CRBillingContact current1 = this.Base.Caches[typeof (CRBillingContact)].Current as CRBillingContact;
    ARContact arContact = PXResultset<ARContact>.op_Implicit(((PXSelectBase<ARContact>) docgraph.Billing_Contact).Select(Array.Empty<object>()));
    int? nullable1;
    if (arContact != null && current1 != null)
    {
      current1.RevisionID = current1.RevisionID ?? arContact.RevisionID;
      int? revisionId = arContact.RevisionID;
      nullable1 = current1.RevisionID;
      if (!(revisionId.GetValueOrDefault() == nullable1.GetValueOrDefault() & revisionId.HasValue == nullable1.HasValue))
        current1.OverrideContact = new bool?(true);
      CRBillingContact crBillingContact = current1;
      nullable1 = current1.BAccountContactID;
      int? nullable2 = nullable1 ?? arContact.BAccountContactID;
      crBillingContact.BAccountContactID = nullable2;
      SharedRecordAttribute.CopyRecord<ARInvoice.billContactID>(((PXSelectBase) docgraph.Document).Cache, (object) invoice, (object) current1, true);
    }
    CRBillingAddress current2 = this.Base.Caches[typeof (CRBillingAddress)].Current as CRBillingAddress;
    ARAddress arAddress = PXResultset<ARAddress>.op_Implicit(((PXSelectBase<ARAddress>) docgraph.Billing_Address).Select(Array.Empty<object>()));
    if (arAddress == null || current2 == null)
      return;
    CRBillingAddress crBillingAddress1 = current2;
    nullable1 = current2.RevisionID;
    int? nullable3 = nullable1 ?? arAddress.RevisionID;
    crBillingAddress1.RevisionID = nullable3;
    nullable1 = arAddress.RevisionID;
    int? nullable4 = current2.RevisionID;
    if (!(nullable1.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable1.HasValue == nullable4.HasValue))
      current2.OverrideAddress = new bool?(true);
    CRBillingAddress crBillingAddress2 = current2;
    nullable4 = current2.BAccountAddressID;
    int? nullable5 = nullable4 ?? arAddress.BAccountAddressID;
    crBillingAddress2.BAccountAddressID = nullable5;
    SharedRecordAttribute.CopyRecord<ARInvoice.billAddressID>(((PXSelectBase) docgraph.Document).Cache, (object) invoice, (object) current2, true);
  }

  public virtual void FillTaxDetails(
    ARInvoiceEntry docgraph,
    ARInvoice invoice,
    Document masterEntity,
    bool needRecalculate)
  {
    if (masterEntity.TaxZoneID != null)
    {
      invoice.TaxZoneID = masterEntity.TaxZoneID;
      if (!needRecalculate && !masterEntity.ManualTotalEntry.GetValueOrDefault())
        TaxBaseAttribute.SetTaxCalc<PX.Objects.AR.ARTran.taxCategoryID>(((PXSelectBase) docgraph.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
    }
    invoice.TaxCalcMode = masterEntity.TaxCalcMode;
    invoice.ExternalTaxExemptionNumber = masterEntity.ExternalTaxExemptionNumber;
    invoice.AvalaraCustomerUsageType = masterEntity.AvalaraCustomerUsageType;
    invoice = ((PXSelectBase<ARInvoice>) docgraph.Document).Update(invoice);
  }

  public virtual void FillRelations(ARInvoiceEntry docgraph, ARInvoice invoice)
  {
    CROpportunity crOpportunity = PXResultset<CROpportunity>.op_Implicit(PXSelectBase<CROpportunity, PXSelectReadonly<CROpportunity, Where<CROpportunity.opportunityID, Equal<Current<Document.opportunityID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    CRRelation crRelation1 = ((PXSelectBase<CRRelation>) docgraph.RelationsLink).Insert();
    crRelation1.RefNoteID = invoice.NoteID;
    crRelation1.RefEntityType = ((object) invoice).GetType().FullName;
    crRelation1.Role = "SR";
    crRelation1.TargetType = "PX.Objects.CR.CROpportunity";
    crRelation1.TargetNoteID = crOpportunity.NoteID;
    crRelation1.DocNoteID = crOpportunity.NoteID;
    crRelation1.EntityID = crOpportunity.BAccountID;
    crRelation1.ContactID = crOpportunity.ContactID;
    ((PXSelectBase<CRRelation>) docgraph.RelationsLink).Update(crRelation1);
    CRQuote crQuote = PXResultset<CRQuote>.op_Implicit(PXSelectBase<CRQuote, PXSelectReadonly<CRQuote, Where<CRQuote.quoteID, Equal<Current<Document.quoteID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    if (crQuote == null)
      return;
    CRRelation crRelation2 = ((PXSelectBase<CRRelation>) docgraph.RelationsLink).Insert();
    crRelation2.RefNoteID = invoice.NoteID;
    crRelation2.RefEntityType = ((object) invoice).GetType().FullName;
    crRelation2.Role = "SR";
    crRelation2.TargetType = "PX.Objects.CR.CRQuote";
    crRelation2.TargetNoteID = crQuote.NoteID;
    crRelation2.DocNoteID = crQuote.NoteID;
    crRelation2.EntityID = crQuote.BAccountID;
    crRelation2.ContactID = crQuote.ContactID;
    ((PXSelectBase<CRRelation>) docgraph.RelationsLink).Update(crRelation2);
  }

  public virtual void FillTransactions(
    ARInvoiceEntry docgraph,
    ARInvoice invoice,
    Document masterEntity,
    CreateInvoicesFilter filter,
    Customer customer)
  {
    if (masterEntity.ManualTotalEntry.GetValueOrDefault())
    {
      PX.Objects.AR.ARTran arTran = ((PXSelectBase<PX.Objects.AR.ARTran>) docgraph.Transactions).Insert(new PX.Objects.AR.ARTran()
      {
        Qty = new Decimal?((Decimal) 1),
        CuryUnitPrice = masterEntity.CuryAmount
      });
      if (arTran != null)
      {
        arTran.CuryDiscAmt = masterEntity.CuryDiscTot;
        using (new PXLocaleScope(customer.LocaleName))
          arTran.TranDesc = PXMessages.LocalizeNoPrefix("Manual Amount");
      }
      ((PXSelectBase<PX.Objects.AR.ARTran>) docgraph.Transactions).Update(arTran);
    }
    else
    {
      foreach (PXResult<CROpportunityProducts, PX.Objects.IN.InventoryItem> pxResult in PXSelectBase<CROpportunityProducts, PXSelectJoin<CROpportunityProducts, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<CROpportunityProducts.inventoryID>>>, Where<CROpportunityProducts.quoteID, Equal<Current<Document.quoteID>>, And<PX.Objects.IN.InventoryItem.stkItem, Equal<False>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
      {
        CROpportunityProducts opportunityProducts = PXResult<CROpportunityProducts, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
        PX.Objects.IN.InventoryItem inventoryItem = PXResult<CROpportunityProducts, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
        PX.Objects.AR.ARTran arTran1 = new PX.Objects.AR.ARTran();
        PX.Objects.AR.ARTran arTran2 = ((PXSelectBase<PX.Objects.AR.ARTran>) docgraph.Transactions).Insert(arTran1);
        if (arTran2 != null)
        {
          arTran2.InventoryID = opportunityProducts.InventoryID;
          using (new PXLocaleScope(customer.LocaleName))
            arTran2.TranDesc = PXDBLocalizableStringAttribute.GetTranslation(this.Base.Caches[typeof (CROpportunityProducts)], (object) opportunityProducts, typeof (CROpportunityProducts.descr).Name, this.Base.Culture.Name);
          arTran2.Qty = opportunityProducts.Quantity;
          arTran2.UOM = opportunityProducts.UOM;
          arTran2.CuryUnitPrice = opportunityProducts.CuryUnitPrice;
          arTran2.IsFree = opportunityProducts.IsFree;
          arTran2.SortOrder = opportunityProducts.SortOrder;
          arTran2.CuryExtPrice = opportunityProducts.CuryExtPrice;
          arTran2.CuryTranAmt = opportunityProducts.CuryAmount;
          arTran2.TaxCategoryID = opportunityProducts.TaxCategoryID;
          arTran2.ProjectID = opportunityProducts.ProjectID;
          arTran2.TaskID = opportunityProducts.TaskID;
          arTran2.CostCodeID = opportunityProducts.CostCodeID;
          bool? nullable = filter.RecalculatePrices;
          if (!nullable.GetValueOrDefault())
          {
            arTran2.ManualPrice = new bool?(true);
          }
          else
          {
            nullable = filter.OverrideManualPrices;
            arTran2.ManualPrice = nullable.GetValueOrDefault() ? new bool?(false) : opportunityProducts.ManualPrice;
          }
          nullable = filter.RecalculateDiscounts;
          if (!nullable.GetValueOrDefault())
          {
            arTran2.ManualDisc = new bool?(true);
          }
          else
          {
            nullable = filter.OverrideManualDiscounts;
            arTran2.ManualDisc = nullable.GetValueOrDefault() ? new bool?(false) : opportunityProducts.ManualDisc;
          }
          arTran2.CuryDiscAmt = opportunityProducts.CuryDiscAmt;
          arTran2.DiscAmt = opportunityProducts.DiscAmt;
          arTran2.DiscPct = opportunityProducts.DiscPct;
          nullable = inventoryItem.Commisionable;
          if (nullable.HasValue)
            arTran2.Commissionable = inventoryItem.Commisionable;
        }
        PX.Objects.AR.ARTran arTran3 = ((PXSelectBase<PX.Objects.AR.ARTran>) docgraph.Transactions).Update(arTran2);
        PXNoteAttribute.CopyNoteAndFiles(this.Base.Caches[typeof (CROpportunityProducts)], (object) opportunityProducts, ((PXSelectBase) docgraph.Transactions).Cache, (object) arTran3, this.Base.Caches[typeof (CRSetup)].Current as PXNoteAttribute.IPXCopySettings);
      }
    }
  }

  public virtual void FillNotesAndFiles(
    ARInvoiceEntry docgraph,
    ARInvoice invoice,
    Document masterEntity)
  {
    PXNoteAttribute.CopyNoteAndFiles(this.Base.Caches[typeof (TMaster)], masterEntity.Base, ((PXSelectBase) docgraph.Document).Cache, (object) invoice, this.Base.Caches[typeof (CRSetup)].Current as PXNoteAttribute.IPXCopySettings);
  }

  public virtual void FillDiscounts(
    ARInvoiceEntry docgraph,
    ARInvoice invoice,
    Document masterEntity,
    CreateInvoicesFilter filter)
  {
    if (filter.RecalculateDiscounts.GetValueOrDefault() || filter.OverrideManualDiscounts.GetValueOrDefault())
      return;
    Dictionary<string, ARInvoiceDiscountDetail> dictionary = new Dictionary<string, ARInvoiceDiscountDetail>();
    foreach (PXResult<ARInvoiceDiscountDetail> pxResult in ((PXSelectBase<ARInvoiceDiscountDetail>) docgraph.ARDiscountDetails).Select(Array.Empty<object>()))
    {
      ARInvoiceDiscountDetail invoiceDiscountDetail = PXResult<ARInvoiceDiscountDetail>.op_Implicit(pxResult);
      ((PXSelectBase<ARInvoiceDiscountDetail>) docgraph.ARDiscountDetails).SetValueExt<ARInvoiceDiscountDetail.skipDiscount>(invoiceDiscountDetail, (object) true);
      string key = $"{invoiceDiscountDetail.Type}:{invoiceDiscountDetail.DiscountID}:{invoiceDiscountDetail.DiscountSequenceID}";
      dictionary.Add(key, invoiceDiscountDetail);
    }
    foreach (PXResult<PX.Objects.Extensions.Discount.Discount> pxResult in ((PXSelectBase<PX.Objects.Extensions.Discount.Discount>) this.DiscountExtension.Discounts).Select(Array.Empty<object>()))
    {
      CROpportunityDiscountDetail opportunityDiscountDetail = PXResult<PX.Objects.Extensions.Discount.Discount>.op_Implicit(pxResult).Base as CROpportunityDiscountDetail;
      string key = $"{opportunityDiscountDetail.Type}:{opportunityDiscountDetail.DiscountID}:{opportunityDiscountDetail.DiscountSequenceID}";
      ARInvoiceDiscountDetail invoiceDiscountDetail;
      if (dictionary.TryGetValue(key, out invoiceDiscountDetail))
      {
        ((PXSelectBase<ARInvoiceDiscountDetail>) docgraph.ARDiscountDetails).SetValueExt<ARInvoiceDiscountDetail.skipDiscount>(invoiceDiscountDetail, (object) false);
        if (opportunityDiscountDetail.IsManual.GetValueOrDefault() && opportunityDiscountDetail.Type == "D")
        {
          ((PXSelectBase<ARInvoiceDiscountDetail>) docgraph.ARDiscountDetails).SetValueExt<ARInvoiceDiscountDetail.extDiscCode>(invoiceDiscountDetail, (object) opportunityDiscountDetail.ExtDiscCode);
          ((PXSelectBase<ARInvoiceDiscountDetail>) docgraph.ARDiscountDetails).SetValueExt<ARInvoiceDiscountDetail.description>(invoiceDiscountDetail, (object) opportunityDiscountDetail.Description);
          ((PXSelectBase<ARInvoiceDiscountDetail>) docgraph.ARDiscountDetails).SetValueExt<ARInvoiceDiscountDetail.isManual>(invoiceDiscountDetail, (object) opportunityDiscountDetail.IsManual);
          ((PXSelectBase<ARInvoiceDiscountDetail>) docgraph.ARDiscountDetails).SetValueExt<ARInvoiceDiscountDetail.curyDiscountAmt>(invoiceDiscountDetail, (object) opportunityDiscountDetail.CuryDiscountAmt);
        }
      }
      else
      {
        ARInvoiceDiscountDetail instance = (ARInvoiceDiscountDetail) ((PXSelectBase) docgraph.ARDiscountDetails).Cache.CreateInstance();
        instance.Type = opportunityDiscountDetail.Type;
        instance.DiscountID = opportunityDiscountDetail.DiscountID;
        instance.DiscountSequenceID = opportunityDiscountDetail.DiscountSequenceID;
        instance.ExtDiscCode = opportunityDiscountDetail.ExtDiscCode;
        instance.Description = opportunityDiscountDetail.Description;
        invoiceDiscountDetail = (ARInvoiceDiscountDetail) ((PXSelectBase) docgraph.ARDiscountDetails).Cache.Insert((object) instance);
        if (opportunityDiscountDetail.IsManual.GetValueOrDefault() && (opportunityDiscountDetail.Type == "D" || opportunityDiscountDetail.Type == "B"))
        {
          invoiceDiscountDetail.CuryDiscountAmt = opportunityDiscountDetail.CuryDiscountAmt;
          invoiceDiscountDetail.IsManual = opportunityDiscountDetail.IsManual;
          ((PXSelectBase) docgraph.ARDiscountDetails).Cache.Update((object) invoiceDiscountDetail);
        }
      }
    }
    ARInvoice copy = PXCache<ARInvoice>.CreateCopy(((PXSelectBase<ARInvoice>) docgraph.Document).Current);
    ((PXSelectBase) docgraph.Document).Cache.SetValueExt<ARInvoice.curyDiscTot>((object) ((PXSelectBase<ARInvoice>) docgraph.Document).Current, (object) DiscountEngineProvider.GetEngineFor<PX.Objects.AR.ARTran, ARInvoiceDiscountDetail>().GetTotalGroupAndDocumentDiscount<ARInvoiceDiscountDetail>((PXSelectBase<ARInvoiceDiscountDetail>) docgraph.ARDiscountDetails));
    if (!PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>())
    {
      bool? manualTotalEntry = masterEntity.ManualTotalEntry;
      bool flag = false;
      if (manualTotalEntry.GetValueOrDefault() == flag & manualTotalEntry.HasValue)
        ((PXSelectBase) docgraph.Document).Cache.SetValueExt<ARInvoice.curyDiscTot>((object) ((PXSelectBase<ARInvoice>) docgraph.Document).Current, (object) masterEntity.CuryDiscTot);
    }
    ((PXSelectBase) docgraph.Document).Cache.RaiseRowUpdated((object) ((PXSelectBase<ARInvoice>) docgraph.Document).Current, (object) copy);
    invoice = ((PXSelectBase<ARInvoice>) docgraph.Document).Update(invoice);
  }

  public virtual void FillInvoiceEntity(
    ARInvoiceEntry docgraph,
    ARInvoice invoice,
    Document masterEntity,
    Customer customer)
  {
    invoice.CuryOrigDocAmt = invoice.CuryDocBal;
    invoice.Hold = new bool?(true);
    this.FillUDFs(docgraph, invoice, masterEntity);
    ((PXSelectBase<ARInvoice>) docgraph.Document).Update(invoice);
    ((PXSelectBase<Customer>) docgraph.customer).Current.CreditRule = customer.CreditRule;
  }

  public virtual void FillTaxes(
    ARInvoiceEntry docgraph,
    Document masterEntity,
    bool needRecalculate)
  {
    if (masterEntity.TaxZoneID == null || needRecalculate)
      return;
    foreach (PXResult<CRTaxTran> pxResult in PXSelectBase<CRTaxTran, PXSelect<CRTaxTran, Where<CRTaxTran.quoteID, Equal<Current<Document.quoteID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
    {
      CRTaxTran crTaxTran = PXResult<CRTaxTran>.op_Implicit(pxResult);
      if (masterEntity.TaxZoneID == null)
        this.Base.Caches[typeof (Document)].RaiseExceptionHandling<Document.taxZoneID>((object) masterEntity, (object) null, (Exception) new PXSetPropertyException<Document.taxZoneID>("'{0}' cannot be empty.", new object[1]
        {
          (object) "[taxZoneID]"
        }));
      ARTaxTran arTaxTran1 = new ARTaxTran();
      arTaxTran1.TaxID = crTaxTran.TaxID;
      ARTaxTran arTaxTran2 = ((PXSelectBase<ARTaxTran>) docgraph.Taxes).Insert(arTaxTran1);
      if (arTaxTran2 != null)
      {
        ARTaxTran copy = PXCache<ARTaxTran>.CreateCopy(arTaxTran2);
        copy.TaxRate = crTaxTran.TaxRate;
        copy.TaxBucketID = new int?(0);
        copy.CuryTaxableAmt = crTaxTran.CuryTaxableAmt;
        copy.CuryTaxAmt = crTaxTran.CuryTaxAmt;
        ((PXSelectBase<ARTaxTran>) docgraph.Taxes).Update(copy);
      }
    }
  }

  public virtual void RecalculateInvoiceDiscounts(
    ARInvoiceEntry docgraph,
    CreateInvoicesFilter filter,
    bool needRecalculate)
  {
    if (!needRecalculate)
      return;
    ((PXSelectBase<RecalcDiscountsParamFilter>) docgraph.recalcdiscountsfilter).Current.OverrideManualPrices = new bool?(filter.OverrideManualPrices.GetValueOrDefault());
    ((PXSelectBase<RecalcDiscountsParamFilter>) docgraph.recalcdiscountsfilter).Current.RecalcDiscounts = new bool?(filter.RecalculateDiscounts.GetValueOrDefault());
    ((PXSelectBase<RecalcDiscountsParamFilter>) docgraph.recalcdiscountsfilter).Current.RecalcUnitPrices = new bool?(filter.RecalculatePrices.GetValueOrDefault());
    ((PXSelectBase<RecalcDiscountsParamFilter>) docgraph.recalcdiscountsfilter).Current.OverrideManualDiscounts = new bool?(filter.OverrideManualDiscounts.GetValueOrDefault());
    ((PXSelectBase<RecalcDiscountsParamFilter>) docgraph.recalcdiscountsfilter).Current.OverrideManualDocGroupDiscounts = new bool?(filter.OverrideManualDocGroupDiscounts.GetValueOrDefault());
    ((PXGraph) docgraph).Actions["RecalculateDiscountsAction"].Press();
  }

  public virtual void FillUDFs(ARInvoiceEntry docgraph, ARInvoice invoice, Document masterEntity)
  {
    UDFHelper.CopyAttributes(this.Base.Caches[typeof (TMaster)], masterEntity.Base, ((PXSelectBase) docgraph.Document).Cache, ((PXSelectBase) docgraph.Document).Cache.Current, invoice.DocType);
  }

  public virtual void ConvertToCustomerAccount(PX.Objects.CR.BAccount account, Document opportunity)
  {
    BusinessAccountMaint instance = PXGraph.CreateInstance<BusinessAccountMaint>();
    ((PXSelectBase<PX.Objects.CR.BAccount>) instance.BAccount).Insert(account);
    ((PXSelectBase<PX.Objects.CR.BAccount>) instance.BAccount).Current = account;
    ((PXSelectBase) instance.BAccount).Cache.SetStatus((object) ((PXSelectBase<PX.Objects.CR.BAccount>) instance.BAccount).Current, (PXEntryStatus) 1);
    ((PXGraph) instance).Actions["extendToCustomer"].Press();
  }

  public virtual long? CopyCurrenfyInfo(PXGraph graph, long? SourceCuryInfoID)
  {
    PX.Objects.CM.CurrencyInfo currencyInfo = PX.Objects.CM.CurrencyInfo.PK.Find(graph, SourceCuryInfoID);
    currencyInfo.CuryInfoID = new long?();
    graph.Caches[typeof (PX.Objects.CM.CurrencyInfo)].Clear();
    return ((PX.Objects.CM.CurrencyInfo) graph.Caches[typeof (PX.Objects.CM.CurrencyInfo)].Insert((object) currencyInfo)).CuryInfoID;
  }

  public abstract CRQuote GetQuoteForWorkflowProcessing();

  protected class DocumentMapping : IBqlMapping
  {
    protected System.Type _table;
    public System.Type OpportunityID = typeof (Document.opportunityID);
    public System.Type QuoteID = typeof (Document.quoteID);
    public System.Type Subject = typeof (Document.subject);
    public System.Type BAccountID = typeof (Document.bAccountID);
    public System.Type LocationID = typeof (Document.locationID);
    public System.Type ContactID = typeof (Document.contactID);
    public System.Type TaxZoneID = typeof (Document.taxZoneID);
    public System.Type TaxCalcMode = typeof (Document.taxCalcMode);
    public System.Type ManualTotalEntry = typeof (Document.manualTotalEntry);
    public System.Type CuryID = typeof (Document.curyID);
    public System.Type CuryInfoID = typeof (Document.curyInfoID);
    public System.Type CuryAmount = typeof (Document.curyAmount);
    public System.Type CuryDiscTot = typeof (Document.curyDiscTot);
    public System.Type ProjectID = typeof (Document.projectID);
    public System.Type BranchID = typeof (Document.branchID);
    public System.Type NoteID = typeof (Document.noteID);
    public System.Type CloseDate = typeof (Document.closeDate);
    public System.Type TermsID = typeof (Document.termsID);
    public System.Type ExternalTaxExemptionNumber = typeof (Document.externalTaxExemptionNumber);
    public System.Type AvalaraCustomerUsageType = typeof (Document.avalaraCustomerUsageType);
    public System.Type IsPrimary = typeof (Document.isPrimary);

    public System.Type Extension => typeof (Document);

    public System.Type Table => this._table;

    public DocumentMapping(System.Type table) => this._table = table;
  }
}
