// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.QuoteMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Models;
using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.DependencyInjection;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.SQLTree;
using PX.Data.WorkflowAPI;
using PX.LicensePolicy;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Discount;
using PX.Objects.CR.Extensions;
using PX.Objects.CR.Extensions.CRCreateInvoice;
using PX.Objects.CR.Extensions.CRCreateSalesOrder;
using PX.Objects.CR.Extensions.CROpportunityContactAddress;
using PX.Objects.CR.QuoteMaint_Extensions;
using PX.Objects.CR.Standalone;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.Extensions.Discount;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.Extensions.MultiCurrency.CR;
using PX.Objects.Extensions.PerUnitTax;
using PX.Objects.Extensions.SalesPrice;
using PX.Objects.Extensions.SalesTax;
using PX.Objects.IN;
using PX.Objects.IN.Attributes;
using PX.Objects.SO;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.CR;

public class QuoteMaint : 
  PXGraph<
  #nullable disable
  QuoteMaint>,
  PXImportAttribute.IPXPrepareItems,
  IGraphWithInitialization
{
  [PXHidden]
  public PXSelect<BAccount> BAccounts;
  [PXHidden]
  public PXSelect<BAccount> bAccountBasic;
  [PXHidden]
  public PXSelect<BAccountR> bAccountRBasic;
  [PXHidden]
  public PXSetupOptional<SOSetup> sosetup;
  [PXHidden]
  public PXSetup<CRSetup> Setup;
  [PXViewName("Sales Quote")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (CRQuote.approved), typeof (CRQuote.rejected)})]
  public PXSelect<CRQuote, Where<CRQuote.opportunityID, Equal<Optional<CRQuote.opportunityID>>, And<CRQuote.quoteType, Equal<CRQuoteTypeAttribute.distribution>>>> Quote;
  [PXHidden]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (CRQuote.approved), typeof (CRQuote.rejected)})]
  public PXSelect<CRQuote, Where<CRQuote.opportunityID, Equal<Current<CRQuote.opportunityID>>, And<CRQuote.quoteNbr, Equal<Current<CRQuote.quoteNbr>>>>> QuoteCurrent;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Address> Address;
  [PXHidden]
  public PXSelect<CROpportunityRevision> OpportunityRevision;
  [PXHidden]
  public PXSetup<Contact, Where<Contact.contactID, Equal<Optional<CRQuote.contactID>>>> Contacts;
  [PXHidden]
  public PXSetup<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Optional<CRQuote.bAccountID>>>> customer;
  [PXViewName("Answers")]
  public CRAttributeSourceList<CRQuote, CRQuote.contactID> Answers;
  [PXViewName("Quote Products")]
  [PXImport(typeof (CRQuote))]
  public PXOrderedSelect<CRQuote, CROpportunityProducts, Where<CROpportunityProducts.quoteID, Equal<Current<CRQuote.quoteID>>>, OrderBy<Asc<CROpportunityProducts.sortOrder>>> Products;
  [PXHidden]
  public PXSelect<CROpportunityRevision> FakeRevisionCache;
  public PXSelect<CROpportunityTax, Where<CROpportunityTax.quoteID, Equal<Current<CRQuote.quoteID>>, And<CROpportunityTax.lineNbr, Less<intMax>>>, OrderBy<Asc<CROpportunityTax.taxID>>> TaxLines;
  [PXViewName("Quote Tax")]
  public PXSelectJoin<CRTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<CRTaxTran.taxID>>>, Where<CRTaxTran.quoteID, Equal<Current<CRQuote.quoteID>>>, OrderBy<Asc<CRTaxTran.lineNbr, Asc<CRTaxTran.taxID>>>> Taxes;
  public PXSetup<Location, Where<Location.bAccountID, Equal<Current<CRQuote.bAccountID>>, And<Location.locationID, Equal<Optional<CRQuote.locationID>>>>> location;
  [PXViewName("Quote Contact")]
  public PXSelect<CRContact, Where<CRContact.contactID, Equal<Current<CRQuote.opportunityContactID>>>> Quote_Contact;
  [PXViewName("Quote Address")]
  public PXSelect<CRAddress, Where<CRAddress.addressID, Equal<Current<CRQuote.opportunityAddressID>>>> Quote_Address;
  [PXViewName("Shipping Contact")]
  public PXSelect<CRShippingContact, Where<CRShippingContact.contactID, Equal<Current<CRQuote.shipContactID>>>> Shipping_Contact;
  [PXViewName("Shipping Address")]
  public PXSelect<CRShippingAddress, Where<CRShippingAddress.addressID, Equal<Current<CRQuote.shipAddressID>>>> Shipping_Address;
  [PXViewName("Bill-To Contact")]
  public PXSelect<CRBillingContact, Where<CRBillingContact.contactID, Equal<Current<CRQuote.billContactID>>>> Billing_Contact;
  [PXViewName("Bill-To Address")]
  public PXSelect<CRBillingAddress, Where<CRBillingAddress.addressID, Equal<Current<CRQuote.billAddressID>>>> Billing_Address;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<CROpportunityProducts, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<BqlOperand<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  CROpportunityProducts.inventoryID>>>, FbqlJoins.Inner<PX.Objects.IN.INSite>.On<BqlOperand<
  #nullable enable
  PX.Objects.IN.INSite.siteID, IBqlInt>.IsEqual<
  #nullable disable
  CROpportunityProducts.siteID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CROpportunityProducts.quoteID, 
  #nullable disable
  Equal<P.AsGuid>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryCD, 
  #nullable disable
  Equal<P.AsString>>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.IN.INSite.siteCD, IBqlString>.IsEqual<
  #nullable disable
  P.AsString>>>>, CROpportunityProducts>.View ProductsByQuoteIDAndInventoryCD;
  [PXHidden]
  public PXSelectJoin<Contact, LeftJoin<PX.Objects.CR.Address, On<Contact.defAddressID, Equal<PX.Objects.CR.Address.addressID>>>, Where<Contact.contactID, Equal<Current<CRQuote.contactID>>>> CurrentContact;
  [PXHidden]
  public PXSelectJoin<PX.Objects.CR.Standalone.CROpportunity, LeftJoin<CROpportunityRevision, On<CROpportunityRevision.noteID, Equal<PX.Objects.CR.Standalone.CROpportunity.defQuoteID>>, LeftJoin<PX.Objects.CR.Standalone.CRQuote, On<PX.Objects.CR.Standalone.CRQuote.quoteID, Equal<CROpportunityRevision.noteID>>>>, Where<PX.Objects.CR.Standalone.CROpportunity.opportunityID, Equal<Optional<CRQuote.opportunityID>>>> Opportunity;
  [PXViewName("Opportunity")]
  public PXSelect<CROpportunity, Where<CROpportunity.opportunityID, Equal<Optional<CRQuote.opportunityID>>>> CurrentOpportunity;
  [PXHidden]
  public PXSelect<PX.Objects.AP.Vendor> Vendors;
  [PXViewName("Approval")]
  public EPApprovalAutomation<CRQuote, CRQuote.approved, CRQuote.rejected, CRQuote.hold, CRSetupQuoteApproval> Approval;
  [PXViewName("Create Account")]
  [PXCopyPasteHiddenView]
  public PXFilter<QuoteMaint.CopyQuoteFilter> CopyQuoteInfo;
  public PXSave<CRQuote> Save;
  public PXAction<CRQuote> cancel;
  public PXInsert<CRQuote> insert;
  public PXCopyPasteAction<CRQuote> CopyPaste;
  public PXDelete<CRQuote> Delete;
  public PXFirst<CRQuote> First;
  public PXPrevious<CRQuote> previous;
  public PXNext<CRQuote> next;
  public PXLast<CRQuote> Last;
  public PXAction<CRQuote> viewOnMap;
  public PXAction<CRQuote> validateAddresses;
  public PXAction<CRQuote> viewMainOnMap;
  public PXAction<CRQuote> ViewShippingOnMap;
  public PXAction<CRQuote> ViewBillingOnMap;
  public PXAction<CRQuote> primaryQuote;
  public PXAction<CRQuote> copyQuote;
  public PXAction<CRQuote> sendQuote;
  public PXAction<CRQuote> printQuote;
  public PXAction<CRQuote> requestApproval;
  public PXAction<CRQuote> editQuote;
  public PXAction<CRQuote> approve;
  public PXAction<CRQuote> reject;
  public PXAction<CRQuote> markAsConverted;
  public PXAction<CRQuote> decline;
  public PXAction<CRQuote> accept;
  public PXWorkflowEventHandler<CRQuote, PX.Objects.SO.SOOrder> OnSalesOrderCreatedFromQuote;
  public PXWorkflowEventHandler<CRQuote, PX.Objects.SO.SOOrder> OnSalesOrderDeleted;
  public PXWorkflowEventHandler<CRQuote, PX.Objects.AR.ARInvoice, CRQuote> OnARInvoiceCreatedFromQuote;
  public PXWorkflowEventHandler<CRQuote, PX.Objects.AR.ARInvoice> OnARInvoiceDeleted;
  public PXSelect<CROpportunityDiscountDetail, Where<CROpportunityDiscountDetail.quoteID, Equal<Current<CRQuote.quoteID>>>, OrderBy<Asc<CROpportunityDiscountDetail.lineNbr>>> _DiscountDetails;

  [InjectDependency]
  protected ILicenseLimitsService _licenseLimits { get; set; }

  public virtual bool ProviderInsert(System.Type table, params PXDataFieldAssign[] pars)
  {
    if (table == typeof (CROpportunityRevision))
    {
      foreach (PXDataFieldAssign par in pars)
      {
        Column column = new Column(GraphHelper.GetBqlField<CROpportunityRevision.noteID>(((PXGraph) this).Caches[typeof (CROpportunityRevision)]).Name, table.Name, (PXDbType) 100);
        if (((SQLExpression) ((PXDataFieldParam) par).Column).Equals((SQLExpression) column))
        {
          if (PXSelectBase<CROpportunityRevision, PXSelect<CROpportunityRevision, Where<CROpportunityRevision.noteID, Equal<Required<CROpportunityRevision.noteID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
          {
            (object) Guid.Parse(((PXDataFieldParam) par).Value.ToString())
          }).Count > 0)
            throw new PXDbOperationSwitchRequiredException(table.Name, "Need update instead of insert");
        }
      }
    }
    return ((PXGraph) this).ProviderInsert(table, pars);
  }

  public virtual bool ProviderDelete(System.Type table, params PXDataFieldRestrict[] pars)
  {
    if (table == typeof (CROpportunityRevision))
    {
      Column column = new Column(GraphHelper.GetBqlField<CROpportunityRevision.opportunityID>(((PXGraph) this).Caches[typeof (CROpportunityRevision)]).Name, table.Name, (PXDbType) 100);
      foreach (PXDataFieldRestrict par in pars)
      {
        if (((SQLExpression) ((PXDataFieldParam) par).Column).Equals((SQLExpression) column) && ((PXDataFieldParam) par).Value != null && this.IsSingleQuote(((PXDataFieldParam) par).Value.ToString()))
          return true;
      }
    }
    return ((PXGraph) this).ProviderDelete(table, pars);
  }

  public QuoteMaint()
  {
    if (string.IsNullOrEmpty(((PXSelectBase<CRSetup>) this.Setup).Current.QuoteNumberingID))
      throw new PXSetPropertyException("Numbering ID is not configured in the CR setup.", new object[1]
      {
        (object) "Customer Management Preferences"
      });
    ((PXGraph) this).Views.Caches.Remove(typeof (PX.Objects.CR.Standalone.CROpportunity));
    ((PXGraph) this).Views.Caches.Remove(typeof (CROpportunity));
  }

  void IGraphWithInitialization.Initialize()
  {
    if (this._licenseLimits == null)
      return;
    ((PXGraph) this).OnBeforeCommit += this._licenseLimits.GetCheckerDelegate<CRQuote>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (CROpportunityProducts), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[1]
      {
        (PXDataFieldValue) new PXDataFieldValue<CROpportunityProducts.quoteID>((object) (Guid?) ((PXSelectBase<CRQuote>) ((QuoteMaint) graph).Quote).Current?.QuoteID)
      }))
    });
  }

  [PXUIField]
  [PXCancelButton]
  protected virtual IEnumerable Cancel(PXAdapter adapter)
  {
    if (((PXSelectBase<CRQuote>) this.Quote).Current != null)
    {
      string opportunityId = ((PXSelectBase<CRQuote>) this.Quote).Current.OpportunityID;
    }
    ((PXSelectBase) this.Quote).Cache.Clear();
    IEnumerator enumerator = ((PXAction) new PXCancel<CRQuote>((PXGraph) this, nameof (Cancel))).Press(adapter).GetEnumerator();
    try
    {
      if (enumerator.MoveNext())
        return (IEnumerable) new object[1]
        {
          (object) (CRQuote) enumerator.Current
        };
    }
    finally
    {
      if (enumerator is IDisposable disposable)
        disposable.Dispose();
    }
    return (IEnumerable) new object[0];
  }

  [PXUIField]
  [PXPreviousButton]
  protected virtual IEnumerable Previous(PXAdapter adapter)
  {
    IEnumerator enumerator = ((PXAction) new PXPrevious<CRQuote>((PXGraph) this, "Prev")).Press(adapter).GetEnumerator();
    try
    {
      if (enumerator.MoveNext())
      {
        CRQuote current = (CRQuote) enumerator.Current;
        if (((PXSelectBase) this.Quote).Cache.GetStatus((object) current) == 2)
          return ((PXAction) this.Last).Press(adapter);
        return (IEnumerable) new object[1]
        {
          (object) current
        };
      }
    }
    finally
    {
      if (enumerator is IDisposable disposable)
        disposable.Dispose();
    }
    return (IEnumerable) new object[0];
  }

  [PXUIField]
  [PXNextButton]
  protected virtual IEnumerable Next(PXAdapter adapter)
  {
    IEnumerator enumerator = ((PXAction) new PXNext<CRQuote>((PXGraph) this, nameof (Next))).Press(adapter).GetEnumerator();
    try
    {
      if (enumerator.MoveNext())
      {
        CRQuote current = (CRQuote) enumerator.Current;
        if (((PXSelectBase) this.Quote).Cache.GetStatus((object) current) == 2)
          return ((PXAction) this.First).Press(adapter);
        return (IEnumerable) new object[1]
        {
          (object) current
        };
      }
    }
    finally
    {
      if (enumerator is IDisposable disposable)
        disposable.Dispose();
    }
    return (IEnumerable) new object[0];
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual void ViewMainOnMap()
  {
    CRAddress aAddr = ((PXSelectBase<CRAddress>) this.Quote_Address).SelectSingle(Array.Empty<object>());
    if (aAddr == null)
      return;
    BAccountUtility.ViewOnMap(aAddr);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual void viewShippingOnMap()
  {
    CRShippingAddress aAddr = ((PXSelectBase<CRShippingAddress>) this.Shipping_Address).SelectSingle(Array.Empty<object>());
    if (aAddr == null)
      return;
    BAccountUtility.ViewOnMap((CRAddress) aAddr);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual void viewBillingOnMap()
  {
    CRBillingAddress aAddr = ((PXSelectBase<CRBillingAddress>) this.Billing_Address).SelectSingle(Array.Empty<object>());
    if (aAddr == null)
      return;
    BAccountUtility.ViewOnMap((CRAddress) aAddr);
  }

  [PXUIField(DisplayName = "Set as Primary")]
  [PXButton]
  public virtual IEnumerable PrimaryQuote(PXAdapter adapter)
  {
    QuoteMaint quoteMaint = this;
    IEnumerable<CRQuote> array = (IEnumerable<CRQuote>) adapter.Get<CRQuote>().ToArray<CRQuote>();
    ((PXAction) quoteMaint.Save).Press();
    foreach (CRQuote crQuote1 in array)
    {
      ((PXSelectBase) quoteMaint.Opportunity).Cache.Clear();
      PXResult<PX.Objects.CR.Standalone.CROpportunity> pxResult = (PXResult<PX.Objects.CR.Standalone.CROpportunity>) ((PXSelectBase) quoteMaint.Opportunity).View.SelectSingleBound(new object[1]
      {
        (object) crQuote1
      }, Array.Empty<object>());
      ((PXSelectBase<PX.Objects.CR.Standalone.CROpportunity>) quoteMaint.Opportunity).Current = PXResult<PX.Objects.CR.Standalone.CROpportunity>.op_Implicit(pxResult);
      ((PXSelectBase<PX.Objects.CR.Standalone.CROpportunity>) quoteMaint.Opportunity).Current.DefQuoteID = crQuote1.QuoteID;
      crQuote1.DefQuoteID = crQuote1.QuoteID;
      ((PXSelectBase) quoteMaint.Opportunity).Cache.Update((object) ((PXSelectBase<PX.Objects.CR.Standalone.CROpportunity>) quoteMaint.Opportunity).Current);
      ((PXGraph) quoteMaint).Views.Caches.Add(typeof (PX.Objects.CR.Standalone.CROpportunity));
      CRQuote crQuote2 = ((PXSelectBase) quoteMaint.Quote).Cache.Update((object) crQuote1) as CRQuote;
      ((PXAction) quoteMaint.Save).Press();
      yield return (object) crQuote2;
    }
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CopyQuote(PXAdapter adapter)
  {
    List<CRQuote> crQuoteList = new List<CRQuote>(adapter.Get().Cast<CRQuote>());
    foreach (CRQuote crQuote in crQuoteList)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      QuoteMaint.\u003C\u003Ec__DisplayClass62_0 cDisplayClass620 = new QuoteMaint.\u003C\u003Ec__DisplayClass62_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass620.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass620.quote = crQuote;
      if (((PXSelectBase) this.CopyQuoteInfo).View.Answer == null)
      {
        ((PXSelectBase) this.CopyQuoteInfo).Cache.Clear();
        QuoteMaint.CopyQuoteFilter copyQuoteFilter = ((PXSelectBase) this.CopyQuoteInfo).Cache.Insert() as QuoteMaint.CopyQuoteFilter;
        // ISSUE: reference to a compiler-generated field
        copyQuoteFilter.Description = cDisplayClass620.quote.Subject + " - copy";
        copyQuoteFilter.RecalculatePrices = new bool?(false);
        copyQuoteFilter.RecalculateDiscounts = new bool?(false);
        copyQuoteFilter.OverrideManualPrices = new bool?(false);
        copyQuoteFilter.OverrideManualDiscounts = new bool?(false);
        copyQuoteFilter.OverrideManualDocGroupDiscounts = new bool?(false);
        // ISSUE: reference to a compiler-generated field
        copyQuoteFilter.OpportunityID = cDisplayClass620.quote.OpportunityID;
      }
      if (((PXSelectBase<QuoteMaint.CopyQuoteFilter>) this.CopyQuoteInfo).AskExt() != 6)
        return (IEnumerable) crQuoteList;
      ((PXAction) this.Save).Press();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass620, __methodptr(\u003CCopyQuote\u003Eb__0)));
    }
    return (IEnumerable) crQuoteList;
  }

  public virtual void CopyToQuote(CRQuote currentquote, QuoteMaint.CopyQuoteFilter param)
  {
    ((PXSelectBase<CRQuote>) this.Quote).Current = currentquote;
    QuoteMaint instance = PXGraph.CreateInstance<QuoteMaint>();
    ((PXGraph) instance).SelectTimeStamp();
    this.CopyToQuote(instance, currentquote, param);
    PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 0);
  }

  protected virtual void CopyToQuote(
    QuoteMaint graph,
    CRQuote currentquote,
    QuoteMaint.CopyQuoteFilter param)
  {
    CRQuote instance = (CRQuote) ((PXSelectBase) graph.Quote).Cache.CreateInstance();
    ((PXSelectBase<PX.Objects.CR.Standalone.CROpportunity>) graph.Opportunity).Current = ((PXSelectBase<PX.Objects.CR.Standalone.CROpportunity>) graph.Opportunity).SelectSingle(new object[1]
    {
      (object) param.OpportunityID
    });
    instance.OpportunityID = param.OpportunityID;
    instance.ShipAddressID = currentquote.ShipAddressID;
    instance.ShipContactID = currentquote.ShipContactID;
    instance.OpportunityAddressID = currentquote.OpportunityAddressID;
    instance.OpportunityContactID = currentquote.OpportunityContactID;
    instance.BillAddressID = currentquote.BillAddressID;
    instance.BillContactID = currentquote.BillContactID;
    CRQuote destData = ((PXSelectBase<CRQuote>) graph.Quote).Insert(instance);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<CRQuote.curyInfoID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    currencyInfo1.CuryInfoID = new long?();
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = GraphHelper.Caches<PX.Objects.CM.Extensions.CurrencyInfo>((PXGraph) graph).Insert(currencyInfo1);
    foreach (string field in (List<string>) ((PXSelectBase) this.Quote).Cache.Fields)
    {
      if (!((PXSelectBase) graph.Quote).Cache.Keys.Contains(field) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.quoteID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.status))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.isPrimary))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.expirationDate))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.approved))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.rejected))))
        ((PXSelectBase) graph.Quote).Cache.SetValue((object) destData, field, ((PXSelectBase) this.Quote).Cache.GetValue((object) currentquote, field));
    }
    UDFHelper.CopyAttributes(((PXSelectBase) this.Quote).Cache, (object) currentquote, ((PXSelectBase) graph.Quote).Cache, (object) destData, (string) null);
    destData.CuryInfoID = currencyInfo2.CuryInfoID;
    destData.Subject = param.Description;
    destData.DocumentDate = ((PXGraph) this).Accessinfo.BusinessDate;
    string note = PXNoteAttribute.GetNote(((PXSelectBase) this.Quote).Cache, (object) currentquote);
    Guid[] fileNotes = PXNoteAttribute.GetFileNotes(((PXSelectBase) this.Quote).Cache, (object) currentquote);
    if (!this.IsSingleQuote(destData.OpportunityID))
    {
      object obj;
      ((PXSelectBase) graph.Quote).Cache.RaiseFieldDefaulting<CRQuote.noteID>((object) destData, ref obj);
      destData.QuoteID = destData.NoteID = (Guid?) obj;
    }
    PXNoteAttribute.SetNote(((PXSelectBase) graph.Quote).Cache, (object) destData, note);
    PXNoteAttribute.SetFileNotes(((PXSelectBase) graph.Quote).Cache, (object) destData, fileNotes);
    ((PXSelectBase) this.Products).View.CloneView((PXGraph) graph, destData.QuoteID, currencyInfo2);
    ((PXGraph) this).GetExtension<QuoteMaint.Discount>();
    ((PXGraph) this).Views["DiscountDetails"].CloneView((PXGraph) graph, destData.QuoteID, currencyInfo2);
    ((PXSelectBase) this.TaxLines).View.CloneView((PXGraph) graph, destData.QuoteID, currencyInfo2);
    ((PXSelectBase) this.Taxes).View.CloneView((PXGraph) graph, destData.QuoteID, currencyInfo2, "RecordID");
    ((PXSelectBase) this.Quote_Contact).View.CloneView((PXGraph) graph, destData.QuoteID, currencyInfo2, "ContactID");
    destData.OpportunityContactID = ((PXSelectBase<CRContact>) graph.Quote_Contact).Current.ContactID;
    ((PXSelectBase) this.Quote_Address).View.CloneView((PXGraph) graph, destData.QuoteID, currencyInfo2, "AddressID");
    destData.OpportunityAddressID = ((PXSelectBase<CRAddress>) graph.Quote_Address).Current.AddressID;
    bool? nullable1 = ((PXSelectBase<CRShippingContact>) graph.Shipping_Contact).Current.OverrideContact;
    if (nullable1.HasValue && nullable1.GetValueOrDefault())
    {
      ((PXSelectBase) this.Shipping_Contact).View.CloneView((PXGraph) graph, destData.QuoteID, currencyInfo2, "ContactID");
      destData.ShipContactID = ((PXSelectBase<CRShippingContact>) graph.Shipping_Contact).Current.ContactID;
    }
    nullable1 = ((PXSelectBase<CRShippingAddress>) graph.Shipping_Address).Current.OverrideAddress;
    if (nullable1.HasValue && nullable1.GetValueOrDefault())
    {
      ((PXSelectBase) this.Shipping_Address).View.CloneView((PXGraph) graph, destData.QuoteID, currencyInfo2, "AddressID");
      destData.ShipAddressID = ((PXSelectBase<CRShippingAddress>) graph.Shipping_Address).Current.AddressID;
    }
    nullable1 = ((PXSelectBase<CRBillingContact>) graph.Billing_Contact).Current.OverrideContact;
    if (nullable1.HasValue && nullable1.GetValueOrDefault())
    {
      ((PXSelectBase) this.Billing_Contact).View.CloneView((PXGraph) graph, destData.QuoteID, currencyInfo2, "ContactID");
      destData.BillContactID = ((PXSelectBase<CRBillingContact>) graph.Billing_Contact).Current.ContactID;
    }
    nullable1 = ((PXSelectBase<CRBillingAddress>) graph.Billing_Address).Current.OverrideAddress;
    if (nullable1.HasValue && nullable1.GetValueOrDefault())
    {
      ((PXSelectBase) this.Billing_Address).View.CloneView((PXGraph) graph, destData.QuoteID, currencyInfo2, "AddressID");
      destData.BillAddressID = ((PXSelectBase<CRBillingAddress>) graph.Billing_Address).Current.AddressID;
    }
    ((PXSelectBase<CRQuote>) graph.Quote).Update(destData);
    QuoteMaint.Discount extension = ((PXGraph) graph).GetExtension<QuoteMaint.Discount>();
    RecalcDiscountsParamFilter current1 = ((PXSelectBase<RecalcDiscountsParamFilter>) extension.recalcdiscountsfilter).Current;
    nullable1 = param.OverrideManualDiscounts;
    bool? nullable2 = new bool?(nullable1.GetValueOrDefault());
    current1.OverrideManualDiscounts = nullable2;
    RecalcDiscountsParamFilter current2 = ((PXSelectBase<RecalcDiscountsParamFilter>) extension.recalcdiscountsfilter).Current;
    nullable1 = param.OverrideManualDocGroupDiscounts;
    bool? nullable3 = new bool?(nullable1.GetValueOrDefault());
    current2.OverrideManualDocGroupDiscounts = nullable3;
    RecalcDiscountsParamFilter current3 = ((PXSelectBase<RecalcDiscountsParamFilter>) extension.recalcdiscountsfilter).Current;
    nullable1 = param.OverrideManualPrices;
    bool? nullable4 = new bool?(nullable1.GetValueOrDefault());
    current3.OverrideManualPrices = nullable4;
    RecalcDiscountsParamFilter current4 = ((PXSelectBase<RecalcDiscountsParamFilter>) extension.recalcdiscountsfilter).Current;
    nullable1 = param.RecalculateDiscounts;
    bool? nullable5 = new bool?(nullable1.GetValueOrDefault());
    current4.RecalcDiscounts = nullable5;
    RecalcDiscountsParamFilter current5 = ((PXSelectBase<RecalcDiscountsParamFilter>) extension.recalcdiscountsfilter).Current;
    nullable1 = param.RecalculatePrices;
    bool? nullable6 = new bool?(nullable1.GetValueOrDefault());
    current5.RecalcUnitPrices = nullable6;
    ((PXGraph) graph).Actions["RecalculateDiscountsAction"].Press();
  }

  protected virtual string DefaultReportID => "CR604500";

  protected virtual string DefaultNotificationCD => "CRQUOTE";

  [PXUIField(DisplayName = "Send")]
  [PXButton]
  public virtual IEnumerable SendQuote(PXAdapter adapter)
  {
    List<CRQuote> list = adapter.Get<CRQuote>().ToList<CRQuote>();
    ((PXGraph) this).GetExtension<QuoteMaint_ActivityDetailsExt>().SendNotifications((Func<CRQuote, string>) (_ => "BAccount"), this.DefaultNotificationCD, (IList<CRQuote>) list, (Func<CRQuote, int?>) (doc => doc.BranchID), (Func<CRQuote, IDictionary<string, string>>) (doc => (IDictionary<string, string>) new Dictionary<string, string>()
    {
      ["CRQuote.OpportunityID"] = doc.OpportunityID,
      ["CRQuote.QuoteNbr"] = doc.QuoteNbr
    }), new MassEmailingActionParameters(adapter), (Func<CRQuote, object>) (doc => (object) doc.BAccountID));
    ((PXAction) this.Save).Press();
    return (IEnumerable) list;
  }

  [PXUIField(DisplayName = "Print Quote")]
  [PXButton]
  public virtual IEnumerable PrintQuote(PXAdapter adapter)
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    string defaultReportId = this.DefaultReportID;
    PXReportRequiredException requiredException = (PXReportRequiredException) null;
    using (IEnumerator<CRQuote> enumerator = adapter.Get<CRQuote>().GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        CRQuote current = enumerator.Current;
        ((PXAction) this.Save).Press();
        dictionary["OpportunityID"] = current.OpportunityID;
        dictionary["QuoteNbr"] = current.QuoteNbr;
        throw PXReportRequiredException.CombineReport(requiredException, defaultReportId, dictionary, OrganizationLocalizationHelper.GetCurrentLocalization((PXGraph) this));
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
  {
    QuoteMaint aGraph = this;
    foreach (CRQuote crQuote in adapter.Get<CRQuote>())
    {
      bool flag1 = false;
      ((PXAction) aGraph.Save).Press();
      if (crQuote != null)
      {
        CRAddress aAddress1 = PXResultset<CRAddress>.op_Implicit(((PXSelectBase<CRAddress>) aGraph.Quote_Address).Select(Array.Empty<object>()));
        bool? nullable;
        if (aAddress1 != null)
        {
          nullable = aAddress1.IsDefaultAddress;
          bool flag2 = false;
          if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
          {
            nullable = aAddress1.IsValidated;
            bool flag3 = false;
            if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue && PXAddressValidator.Validate<CRAddress>((PXGraph) aGraph, aAddress1, true, true))
              flag1 = true;
          }
        }
        CRShippingAddress aAddress2 = PXResultset<CRShippingAddress>.op_Implicit(((PXSelectBase<CRShippingAddress>) aGraph.Shipping_Address).Select(Array.Empty<object>()));
        if (aAddress2 != null)
        {
          nullable = aAddress2.IsDefaultAddress;
          bool flag4 = false;
          if (nullable.GetValueOrDefault() == flag4 & nullable.HasValue)
          {
            nullable = aAddress2.IsValidated;
            bool flag5 = false;
            if (nullable.GetValueOrDefault() == flag5 & nullable.HasValue && PXAddressValidator.Validate<CRShippingAddress>((PXGraph) aGraph, aAddress2, true, true))
              flag1 = true;
          }
        }
        CRBillingAddress aAddress3 = PXResultset<CRBillingAddress>.op_Implicit(((PXSelectBase<CRBillingAddress>) aGraph.Billing_Address).Select(Array.Empty<object>()));
        if (aAddress3 != null)
        {
          nullable = aAddress3.IsDefaultAddress;
          bool flag6 = false;
          if (nullable.GetValueOrDefault() == flag6 & nullable.HasValue)
          {
            nullable = aAddress3.IsValidated;
            bool flag7 = false;
            if (nullable.GetValueOrDefault() == flag7 & nullable.HasValue && PXAddressValidator.Validate<CRBillingAddress>((PXGraph) aGraph, aAddress3, true, true))
              flag1 = true;
          }
        }
        if (flag1)
          ((PXAction) aGraph.Save).Press();
      }
      yield return (object) crQuote;
    }
  }

  public virtual void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers)
  {
    EnumerableExtensions.ForEach<Command>(script.Where<Command>((Func<Command, bool>) (_ => _.ObjectName.StartsWith("Products"))), (Action<Command>) (_ => _.Commit = false));
    script.Where<Command>((Func<Command, bool>) (_ => _.ObjectName.StartsWith("Products"))).Last<Command>().Commit = true;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable RequestApproval(PXAdapter adapter)
  {
    foreach (CRQuote crQuote in adapter.Get<CRQuote>())
    {
      crQuote.Approved = new bool?(false);
      crQuote.Rejected = new bool?(false);
      ((PXSelectBase) this.QuoteCurrent).Cache.Update((object) crQuote);
      this.Approval.Assign(crQuote, (IEnumerable<PX.Objects.EP.ApprovalMap>) this.Approval.GetAssignedMaps(crQuote, ((PXSelectBase) this.QuoteCurrent).Cache));
      ((PXAction) this.Save).Press();
      yield return (object) crQuote;
    }
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable EditQuote(PXAdapter adapter)
  {
    foreach (CRQuote doc in adapter.Get<CRQuote>())
    {
      this.Approval.ClearRelatedApprovals(doc);
      doc.Approved = new bool?(false);
      doc.Rejected = new bool?(false);
      ((PXAction) this.Save).Press();
      yield return (object) doc;
    }
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Approve(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Reject(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable MarkAsConverted(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Decline(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Accept(PXAdapter adapter) => adapter.Get();

  [CRMBAccount(new System.Type[] {typeof (BAccountType.prospectType), typeof (BAccountType.customerType), typeof (BAccountType.combinedType), typeof (BAccountType.vendorType)}, null, null, null)]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<Contact.bAccountID> e)
  {
  }

  [PXDefault(typeof (CRQuote.documentDate))]
  [PXMergeAttributes]
  protected virtual void EPApproval_DocDate_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (CRQuote.subject))]
  [PXMergeAttributes]
  protected virtual void EPApproval_Descr_CacheAttached(PXCache sender)
  {
  }

  [CurrencyInfo(typeof (CRQuote.curyInfoID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_CuryInfoID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (CRQuote.curyProductsAmount))]
  [PXMergeAttributes]
  protected virtual void EPApproval_CuryTotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (CRQuote.productsAmount))]
  [PXMergeAttributes]
  protected virtual void EPApproval_TotalAmount_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (CRQuote.bAccountID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_BAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (CRQuote.ownerID))]
  [PXMergeAttributes]
  protected virtual void EPApproval_DocumentOwnerID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  protected virtual void _(PX.Data.Events.CacheAttached<CRShippingAddress.latitude> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CRShippingAddress.longitude> e)
  {
  }

  protected virtual void CopyQuoteFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is QuoteMaint.CopyQuoteFilter row))
      return;
    bool? nullable = row.RecalculatePrices;
    if ((nullable.HasValue ? new bool?(!nullable.GetValueOrDefault()) : new bool?()).GetValueOrDefault())
      ((PXSelectBase) this.CopyQuoteInfo).Cache.SetValue<QuoteMaint.CopyQuoteFilter.overrideManualPrices>((object) row, (object) false);
    nullable = row.RecalculateDiscounts;
    if (!(nullable.HasValue ? new bool?(!nullable.GetValueOrDefault()) : new bool?()).GetValueOrDefault())
      return;
    ((PXSelectBase) this.CopyQuoteInfo).Cache.SetValue<QuoteMaint.CopyQuoteFilter.overrideManualDiscounts>((object) row, (object) false);
    ((PXSelectBase) this.CopyQuoteInfo).Cache.SetValue<QuoteMaint.CopyQuoteFilter.overrideManualDocGroupDiscounts>((object) row, (object) false);
  }

  protected virtual void CopyQuoteFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is QuoteMaint.CopyQuoteFilter row))
      return;
    PXCache pxCache1 = sender;
    QuoteMaint.CopyQuoteFilter copyQuoteFilter1 = row;
    bool? nullable = row.RecalculatePrices;
    int num1 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<QuoteMaint.CopyQuoteFilter.overrideManualPrices>(pxCache1, (object) copyQuoteFilter1, num1 != 0);
    PXCache pxCache2 = sender;
    QuoteMaint.CopyQuoteFilter copyQuoteFilter2 = row;
    nullable = row.RecalculateDiscounts;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<QuoteMaint.CopyQuoteFilter.overrideManualDiscounts>(pxCache2, (object) copyQuoteFilter2, num2 != 0);
    PXCache pxCache3 = sender;
    QuoteMaint.CopyQuoteFilter copyQuoteFilter3 = row;
    nullable = row.RecalculateDiscounts;
    int num3 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<QuoteMaint.CopyQuoteFilter.overrideManualDocGroupDiscounts>(pxCache3, (object) copyQuoteFilter3, num3 != 0);
  }

  protected virtual void RecalcDiscountsParamFilter_RowUpdated(
    PXCache sender,
    PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is RecalcDiscountsParamFilter row))
      return;
    bool? nullable = row.RecalcUnitPrices;
    if (!nullable.GetValueOrDefault())
      ((PXSelectBase) this.CopyQuoteInfo).Cache.SetValue<RecalcDiscountsParamFilter.overrideManualPrices>((object) row, (object) false);
    nullable = row.RecalcDiscounts;
    if (nullable.GetValueOrDefault())
      return;
    ((PXSelectBase) this.CopyQuoteInfo).Cache.SetValue<RecalcDiscountsParamFilter.overrideManualDiscounts>((object) row, (object) false);
    ((PXSelectBase) this.CopyQuoteInfo).Cache.SetValue<RecalcDiscountsParamFilter.overrideManualDocGroupDiscounts>((object) row, (object) false);
  }

  protected virtual void RecalcDiscountsParamFilter_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    if (!(e.Row is RecalcDiscountsParamFilter row))
      return;
    PXCache pxCache1 = sender;
    RecalcDiscountsParamFilter discountsParamFilter1 = row;
    bool? nullable = row.RecalcUnitPrices;
    int num1 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<RecalcDiscountsParamFilter.overrideManualPrices>(pxCache1, (object) discountsParamFilter1, num1 != 0);
    PXCache pxCache2 = sender;
    RecalcDiscountsParamFilter discountsParamFilter2 = row;
    nullable = row.RecalcDiscounts;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<RecalcDiscountsParamFilter.overrideManualDiscounts>(pxCache2, (object) discountsParamFilter2, num2 != 0);
    PXCache pxCache3 = sender;
    RecalcDiscountsParamFilter discountsParamFilter3 = row;
    nullable = row.RecalcDiscounts;
    int num3 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<RecalcDiscountsParamFilter.overrideManualDocGroupDiscounts>(pxCache3, (object) discountsParamFilter3, num3 != 0);
  }

  protected virtual void CRQuote_TaxZoneID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is CRQuote row))
      return;
    e.NewValue = (object) this.GetDefaultTaxZone(row);
    if (sender.GetStatus(e.Row) == null)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual string GetDefaultTaxZone(CRQuote row)
  {
    string defaultTaxZone = (string) null;
    Location location1 = PXResultset<Location>.op_Implicit(PXSelectBase<Location, PXSelect<Location, Where<Location.bAccountID, Equal<Required<CRQuote.bAccountID>>, And<Location.locationID, Equal<Required<CRQuote.locationID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.BAccountID,
      (object) row.LocationID
    }));
    if (location1 != null)
    {
      if (!string.IsNullOrEmpty(location1.CTaxZoneID))
      {
        defaultTaxZone = location1.CTaxZoneID;
      }
      else
      {
        PX.Objects.CR.Address adrress = PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) location1.DefAddressID
        }));
        if (adrress != null)
          defaultTaxZone = TaxBuilderEngine.GetTaxZoneByAddress((PXGraph) this, (IAddressBase) adrress);
      }
    }
    if (location1 == null && defaultTaxZone == null)
    {
      Location location2 = PXResultset<Location>.op_Implicit(PXSelectBase<Location, PXSelectJoin<Location, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<Current<CRQuote.branchID>>>, InnerJoin<BAccount, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccount.bAccountID>>>>, Where<Location.locationID, Equal<BAccount.defLocationID>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      defaultTaxZone = location2 == null || location2.VTaxZoneID == null ? row.TaxZoneID : location2.VTaxZoneID;
    }
    return defaultTaxZone;
  }

  protected virtual void CRQuote_QuoteID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is CRQuote row))
      return;
    object noteId = (object) row.NoteID;
    if (noteId == null)
      sender.RaiseFieldDefaulting<CRQuote.noteID>((object) row, ref noteId);
    e.NewValue = noteId;
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<CRQuote.quoteType, Equal<CRQuoteTypeAttribute.distribution>>), "Only a sales quote can be selected.", new System.Type[] {})]
  protected virtual void CRQuote_QuoteNbr_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (Search<Location.taxRegistrationID, Where<Location.bAccountID, Equal<Current<CRQuote.bAccountID>>, And<Location.locationID, Equal<Current<CRQuote.locationID>>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<CRQuote.taxRegistrationID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (Search<Location.cAvalaraExemptionNumber, Where<Location.bAccountID, Equal<Current<CRQuote.bAccountID>>, And<Location.locationID, Equal<Current<CRQuote.locationID>>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CRQuote.externalTaxExemptionNumber> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault("0", typeof (Search<Location.cAvalaraCustomerUsageType, Where<Location.bAccountID, Equal<Current<CRQuote.bAccountID>>, And<Location.locationID, Equal<Current<CRQuote.locationID>>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CRQuote.avalaraCustomerUsageType> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (Search<BAccount.defLocationID, Where<BAccount.bAccountID, Equal<Current<CRQuote.bAccountID>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<CRQuote.locationID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (CRMBAccountAttribute), "Enabled", true)]
  protected virtual void _(PX.Data.Events.CacheAttached<CRQuote.bAccountID> e)
  {
  }

  protected virtual void CRQuote_OpportunityID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CRQuote row))
      return;
    ((PXSelectBase) this.Opportunity).Cache.Current = (object) ((PXSelectBase<PX.Objects.CR.Standalone.CROpportunity>) this.Opportunity).SelectSingle(new object[1]
    {
      (object) row.OpportunityID
    });
    if (((PXSelectBase) this.Opportunity).Cache.Current == null)
      return;
    row.Subject = ((PX.Objects.CR.Standalone.CROpportunity) ((PXSelectBase) this.Opportunity).Cache.Current).Subject;
    if (!this.IsSingleQuote(row.OpportunityID))
      return;
    CROpportunity crOpportunity = ((PXSelectBase<CROpportunity>) this.CurrentOpportunity).SelectSingle(new object[1]
    {
      (object) row.OpportunityID
    });
    if (crOpportunity == null)
      return;
    row.QuoteID = crOpportunity.QuoteNoteID;
    row.IsPrimary = new bool?(true);
  }

  [PXCustomizeBaseAttribute(typeof (PXSelectorAttribute), "DescriptionField", typeof (CROpportunity.subject))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CRQuote.opportunityID> e)
  {
  }

  protected virtual void CRQuote_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    PXNoteAttribute.SetTextFilesActivitiesRequired<CROpportunityProducts.noteID>(((PXSelectBase) this.Products).Cache, (object) null, true, true, false);
    if (!(e.Row is CRQuote row))
      return;
    PXCache pxCache1 = cache;
    CRQuote crQuote1 = row;
    int? nullable1;
    int num1;
    if (!row.BAccountID.HasValue)
    {
      nullable1 = row.ContactID;
      num1 = nullable1.HasValue ? 1 : 0;
    }
    else
      num1 = 1;
    PXUIFieldAttribute.SetEnabled<CRQuote.allowOverrideContactAddress>(pxCache1, (object) crQuote1, num1 != 0);
    ((PXGraph) this).Caches[typeof (CRContact)].AllowUpdate = row.AllowOverrideContactAddress.GetValueOrDefault();
    ((PXGraph) this).Caches[typeof (CRAddress)].AllowUpdate = row.AllowOverrideContactAddress.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<CRQuote.curyAmount>(cache, (object) row, row.ManualTotalEntry.GetValueOrDefault());
    bool flag1 = PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>();
    PXUIFieldAttribute.SetEnabled<CRQuote.curyDiscTot>(cache, (object) row, row.ManualTotalEntry.GetValueOrDefault() || !flag1);
    PXCache pxCache2 = cache;
    CRQuote crQuote2 = row;
    nullable1 = row.BAccountID;
    int num2 = nullable1.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CRQuote.locationID>(pxCache2, (object) crQuote2, num2 != 0);
    PXCache pxCache3 = cache;
    CRQuote crQuote3 = row;
    nullable1 = row.BAccountID;
    int num3 = !nullable1.HasValue ? 2 : 1;
    PXDefaultAttribute.SetPersistingCheck<CRQuote.locationID>(pxCache3, (object) crQuote3, (PXPersistingCheck) num3);
    PXUIFieldAttribute.SetVisible<CRQuote.curyID>(cache, (object) row, this.IsMultyCurrency);
    string str = $"Subtotal: {((PXSelectBase) this.Quote).Cache.GetValueExt<CRQuote.curyExtPriceTotal>((object) row)}, Line Discount Subtotal: {((PXSelectBase) this.Quote).Cache.GetValueExt<CRQuote.curyLineDiscountTotal>((object) row)}";
    foreach (PXResult<CROpportunityProducts> pxResult in ((PXSelectBase<CROpportunityProducts>) this.Products).Select(Array.Empty<object>()))
    {
      CROpportunityProducts opportunityProducts = PXResult<CROpportunityProducts>.op_Implicit(pxResult);
      opportunityProducts.TextForProductsGrid = str;
      PXEntryStatus status = ((PXSelectBase) this.Products).Cache.GetStatus((object) opportunityProducts);
      ((PXSelectBase) this.Products).Cache.SetStatus((object) opportunityProducts, (PXEntryStatus) 1);
      ((PXSelectBase) this.Products).Cache.SetStatus((object) opportunityProducts, status);
    }
    bool? opportunityIsActive = row.OpportunityIsActive;
    bool flag2 = false;
    if (opportunityIsActive.GetValueOrDefault() == flag2 & opportunityIsActive.HasValue)
    {
      cache.RaiseExceptionHandling<CRQuote.opportunityID>((object) row, (object) row.OpportunityID, (Exception) new PXSetPropertyException("The opportunity is not active.", (PXErrorLevel) 2));
      PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster = PXCacheEx.AdjustUI(cache, (object) row);
      attributeAdjuster = attributeAdjuster.ForAllFields((Action<PXUIFieldAttribute>) (f => f.Enabled = false));
      PXCacheEx.AttributeAdjuster<PXUIFieldAttribute>.Chained chained = attributeAdjuster.For<CRQuote.quoteID>((Action<PXUIFieldAttribute>) (f => f.Enabled = true));
      chained = chained.SameFor<CRQuote.quoteNbr>();
      chained = chained.SameFor<CRQuote.opportunityID>();
      chained = chained.SameFor<CRQuote.subject>();
      chained = chained.SameFor<CRQuote.documentDate>();
      chained.SameFor<CRQuote.expirationDate>();
      DisableFields<CRContact>();
      DisableFields<CRAddress>();
      DisableFields<CRBillingContact>();
      DisableFields<CRBillingAddress>();
      DisableFields<CRShippingContact>();
      DisableFields<CRShippingAddress>();
    }
    if (!((PXGraph) this).UnattendedMode)
    {
      CRShippingAddress crShippingAddress = PXResultset<CRShippingAddress>.op_Implicit(((PXSelectBase<CRShippingAddress>) this.Shipping_Address).Select(Array.Empty<object>()));
      CRBillingAddress crBillingAddress = PXResultset<CRBillingAddress>.op_Implicit(((PXSelectBase<CRBillingAddress>) this.Billing_Address).Select(Array.Empty<object>()));
      CRAddress crAddress = PXResultset<CRAddress>.op_Implicit(((PXSelectBase<CRAddress>) this.Quote_Address).Select(Array.Empty<object>()));
      if (crShippingAddress != null)
      {
        bool? isDefaultAddress = crShippingAddress.IsDefaultAddress;
        bool flag3 = false;
        if (isDefaultAddress.GetValueOrDefault() == flag3 & isDefaultAddress.HasValue)
        {
          bool? isValidated = crShippingAddress.IsValidated;
          bool flag4 = false;
          if (isValidated.GetValueOrDefault() == flag4 & isValidated.HasValue)
            goto label_27;
        }
      }
      if (crBillingAddress != null)
      {
        bool? isDefaultAddress = crBillingAddress.IsDefaultAddress;
        bool flag5 = false;
        if (isDefaultAddress.GetValueOrDefault() == flag5 & isDefaultAddress.HasValue)
        {
          bool? isValidated = crBillingAddress.IsValidated;
          bool flag6 = false;
          if (isValidated.GetValueOrDefault() == flag6 & isValidated.HasValue)
            goto label_27;
        }
      }
      int num4;
      if (crAddress != null)
      {
        bool? isDefaultAddress = crAddress.IsDefaultAddress;
        bool flag7 = false;
        if (!(isDefaultAddress.GetValueOrDefault() == flag7 & isDefaultAddress.HasValue))
        {
          int? nullable2 = row.BAccountID;
          if (!nullable2.HasValue)
          {
            nullable2 = row.ContactID;
            if (nullable2.HasValue)
              goto label_26;
          }
          else
            goto label_26;
        }
        bool? isValidated = crAddress.IsValidated;
        bool flag8 = false;
        num4 = isValidated.GetValueOrDefault() == flag8 & isValidated.HasValue ? 1 : 0;
        goto label_28;
      }
label_26:
      num4 = 0;
      goto label_28;
label_27:
      num4 = 1;
label_28:
      ((PXAction) this.validateAddresses).SetEnabled(num4 != 0);
    }
    this.VisibilityHandler(cache, row);

    void DisableFields<TTable>() where TTable : class, IBqlTable, new()
    {
      ((PXCache) GraphHelper.Caches<TTable>((PXGraph) this)).AllowUpdate = false;
    }
  }

  protected virtual void CRQuote_BAccountID_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is CRQuote row))
      return;
    int? baccountId = row.BAccountID;
    int num = 0;
    if (!(baccountId.GetValueOrDefault() < num & baccountId.HasValue))
      return;
    e.ReturnValue = (object) "";
  }

  protected virtual void CRQuote_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is CRQuote row))
      return;
    row.NoteID = row.QuoteID;
    bool flag = this.IsFirstQuote(row.OpportunityID);
    object contactId = (object) row.ContactID;
    if (contactId != null && !this.VerifyField<CRQuote.contactID>((object) row, contactId))
      row.ContactID = new int?();
    object locationId = (object) row.LocationID;
    if (locationId == null && !flag || !this.VerifyField<CRQuote.locationID>((object) row, locationId))
      cache.SetDefaultExt<CRQuote.locationID>((object) row);
    if (!row.ContactID.HasValue)
      cache.SetDefaultExt<CRQuote.contactID>((object) row);
    if (row.TaxZoneID == null && !flag)
      cache.SetDefaultExt<CRQuote.taxZoneID>((object) row);
    foreach (object obj in GraphHelper.RowCast<CROpportunityProducts>((IEnumerable) ((PXSelectBase<CROpportunityProducts>) this.Products).Select(Array.Empty<object>())))
      ((PXSelectBase) this.Products).Cache.Update(obj);
    if (!flag)
      return;
    CROpportunityRevision opportunityRevision = PXResultset<CROpportunityRevision>.op_Implicit(PXSelectBase<CROpportunityRevision, PXSelect<CROpportunityRevision, Where<CROpportunityRevision.opportunityID, Equal<Required<CROpportunityRevision.opportunityID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) row.OpportunityID
    }));
    if (opportunityRevision == null)
      return;
    cache.SetValueExt((object) row, typeof (CRQuote.curyInfoID).Name, (object) opportunityRevision.CuryInfoID);
    QuoteMaint.Discount extension = ((PXGraph) this).GetExtension<QuoteMaint.Discount>();
    extension.RefreshTotalsAndFreeItems(((PXSelectBase) extension.Details).Cache);
  }

  protected virtual void CRQuote_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    CRQuote oldRow = e.OldRow as CRQuote;
    CRQuote row = e.Row as CRQuote;
    if (oldRow == null || row == null)
      return;
    int? nullable1 = row.ContactID;
    if (nullable1.HasValue)
    {
      nullable1 = row.ContactID;
      int? contactId = oldRow.ContactID;
      if (!(nullable1.GetValueOrDefault() == contactId.GetValueOrDefault() & nullable1.HasValue == contactId.HasValue) && (ValueType) row.BAccountID == null)
        this.FillDefaultBAccountID(sender, row);
    }
    int? nullable2 = row.BAccountID;
    nullable1 = oldRow.BAccountID;
    bool flag1 = !(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue);
    object locationId = (object) row.LocationID;
    bool flag2 = !sender.ObjectsEqual<CRQuote.locationID>(e.Row, e.OldRow);
    if (flag2 | flag1 && (locationId == null || !this.VerifyField<CRQuote.locationID>((object) row, locationId)))
      sender.SetDefaultExt<CRQuote.locationID>((object) row);
    if (flag1)
      sender.SetDefaultExt<CRQuote.taxZoneID>((object) row);
    DateTime? documentDate1 = row.DocumentDate;
    DateTime? documentDate2 = oldRow.DocumentDate;
    bool flag3 = documentDate1.HasValue != documentDate2.HasValue || documentDate1.HasValue && documentDate1.GetValueOrDefault() != documentDate2.GetValueOrDefault();
    nullable1 = row.ProjectID;
    nullable2 = oldRow.ProjectID;
    bool flag4 = !(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue);
    if (flag2 | flag3 | flag4 | flag1)
    {
      PXCache cache = ((PXSelectBase) this.Products).Cache;
      foreach (CROpportunityProducts selectProduct in this.SelectProducts((object) row.QuoteID))
      {
        CROpportunityProducts copy = (CROpportunityProducts) cache.CreateCopy((object) selectProduct);
        copy.ProjectID = row.ProjectID;
        copy.CustomerID = row.BAccountID;
        cache.Update((object) copy);
      }
      sender.SetDefaultExt<CRQuote.taxCalcMode>((object) row);
    }
    foreach (PXResult<CROpportunityProducts> pxResult in ((PXSelectBase<CROpportunityProducts>) this.Products).Select(Array.Empty<object>()))
    {
      CROpportunityProducts opportunityProducts1 = PXResult<CROpportunityProducts>.op_Implicit(pxResult);
      CROpportunityProducts opportunityProducts2 = opportunityProducts1;
      Decimal? nullable3 = row.CuryExtPriceTotal;
      string str1 = nullable3.ToString();
      nullable3 = row.CuryLineDiscountTotal;
      string str2 = nullable3.ToString();
      string str3 = $"Subtotal: {str1}, Line Discount Subtotal: {str2}";
      opportunityProducts2.TextForProductsGrid = str3;
      PXEntryStatus status = ((PXSelectBase) this.Products).Cache.GetStatus((object) opportunityProducts1);
      ((PXSelectBase) this.Products).Cache.SetStatus((object) opportunityProducts1, (PXEntryStatus) 1);
      ((PXSelectBase) this.Products).Cache.SetStatus((object) opportunityProducts1, status);
    }
    if (!flag2)
      return;
    sender.SetDefaultExt<CRQuote.taxZoneID>((object) row);
    sender.SetDefaultExt<CRQuote.taxRegistrationID>((object) row);
    sender.SetDefaultExt<CRQuote.externalTaxExemptionNumber>((object) row);
    sender.SetDefaultExt<CRQuote.avalaraCustomerUsageType>((object) row);
  }

  protected virtual void CRQuote_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (e.Row is CRQuote row && (bool) sender.GetValue<CRQuote.isPrimary>((object) row) && !this.IsSingleQuote(row.OpportunityID))
      throw new PXException("The quote cannot be deleted because it is marked as primary for the opportunity. If you want to delete this quote, mark another quote as primary first.");
  }

  protected virtual void CRQuote_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    CRQuote row = (CRQuote) e.Row;
    if (row == null || e.Operation != 2 && e.Operation != 1 || !row.BAccountID.HasValue)
      return;
    PXDefaultAttribute.SetPersistingCheck<CRQuote.locationID>(sender, e.Row, (PXPersistingCheck) 1);
  }

  protected virtual void CRQuote_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (!(e.Row is CRQuote row) || e.Operation != 2 || ((PXSelectBase<PX.Objects.CR.Standalone.CROpportunity>) this.Opportunity).Current == null || e.TranStatus != null)
      return;
    PXDatabase.Update<PX.Objects.CR.Standalone.CROpportunity>(new PXDataFieldParam[2]
    {
      (PXDataFieldParam) new PXDataFieldAssign<PX.Objects.CR.Standalone.CROpportunity.defQuoteID>((object) (row.IsPrimary.GetValueOrDefault() ? row.QuoteID : ((PXSelectBase<PX.Objects.CR.Standalone.CROpportunity>) this.Opportunity).Current.DefQuoteID)),
      (PXDataFieldParam) new PXDataFieldRestrict<PX.Objects.CR.Standalone.CROpportunity.opportunityID>((PXDbType) 22, new int?((int) byte.MaxValue), (object) ((PXSelectBase<PX.Objects.CR.Standalone.CROpportunity>) this.Opportunity).Current.OpportunityID, (PXComp) 0)
    });
  }

  protected void SuppressCascadeDeletion(PXView view, object row)
  {
    PXCache cach = ((PXGraph) this).Caches[row.GetType()];
    foreach (object obj in view.Cache.Deleted)
    {
      if (view.Cache.GetStatus(obj) == 3)
      {
        bool flag = true;
        string[] strArray = new string[1]
        {
          typeof (CROpportunity.quoteNoteID).Name
        };
        foreach (string str in strArray)
        {
          if (!object.Equals(cach.GetValue(row, str), view.Cache.GetValue(obj, str)))
          {
            flag = false;
            break;
          }
        }
        if (flag)
          view.Cache.SetStatus(obj, (PXEntryStatus) 0);
      }
    }
  }

  protected virtual void VisibilityHandler(PXCache sender, CRQuote row)
  {
    PX.Objects.CR.Standalone.CROpportunity crOpportunity = PXResult<PX.Objects.CR.Standalone.CROpportunity>.op_Implicit(((IQueryable<PXResult<PX.Objects.CR.Standalone.CROpportunity>>) PXSelectBase<PX.Objects.CR.Standalone.CROpportunity, PXSelect<PX.Objects.CR.Standalone.CROpportunity, Where<PX.Objects.CR.Standalone.CROpportunity.opportunityID, Equal<Required<PX.Objects.CR.Standalone.CROpportunity.opportunityID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.OpportunityID
    })).FirstOrDefault<PXResult<PX.Objects.CR.Standalone.CROpportunity>>());
    if (crOpportunity == null)
      return;
    bool? nullable = row.IsDisabled;
    int num;
    if (!nullable.GetValueOrDefault())
    {
      nullable = crOpportunity.IsActive;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 0;
    bool flag = num != 0;
    System.Type[] typeArray = new System.Type[3]
    {
      typeof (CROpportunityDiscountDetail),
      typeof (CROpportunityProducts),
      typeof (CRTaxTran)
    };
    foreach (System.Type type in typeArray)
      ((PXGraph) this).Caches[type].AllowInsert = ((PXGraph) this).Caches[type].AllowUpdate = ((PXGraph) this).Caches[type].AllowDelete = flag;
    ((PXGraph) this).Caches[typeof (QuoteMaint.CopyQuoteFilter)].AllowUpdate = true;
    ((PXGraph) this).Caches[typeof (RecalcDiscountsParamFilter)].AllowUpdate = true;
  }

  protected virtual void CROpportunityRevision_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    CROpportunityRevision row = (CROpportunityRevision) e.Row;
    if (row == null || ((PXSelectBase<CRQuote>) this.Quote).Current == null)
      return;
    Guid? noteId = row.NoteID;
    Guid? quoteId = ((PXSelectBase<CRQuote>) this.Quote).Current.QuoteID;
    if ((noteId.HasValue == quoteId.HasValue ? (noteId.HasValue ? (noteId.GetValueOrDefault() == quoteId.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (CRQuote.curyInfoID))]
  protected virtual void CROpportunityProducts_CuryInfoID_CacheAttached(PXCache e)
  {
  }

  [PXDBGuid(false, IsKey = true)]
  [PXDBDefault(typeof (CRQuote.quoteID))]
  [PXParent(typeof (Select<CRQuote, Where<CRQuote.quoteID, Equal<Current<CROpportunityProducts.quoteID>>>>))]
  protected virtual void CROpportunityProducts_QuoteID_CacheAttached(PXCache e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.vendorID> e)
  {
    if (e.Row == null)
      return;
    bool? poCreate = e.Row.POCreate;
    bool flag = false;
    if (!(poCreate.GetValueOrDefault() == flag & poCreate.HasValue) && e.Row.InventoryID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.vendorID>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.pOCreate> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.pOCreate>>) e).Cache.SetDefaultExt<CROpportunityProducts.vendorID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.inventoryID> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.inventoryID>>) e).Cache.SetValueExt<CROpportunityProducts.pOCreate>((object) e.Row, (object) false);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CROpportunityProducts.projectID> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts.projectID>, object, object>) e).NewValue = (object) ((PXSelectBase<CRQuote>) this.QuoteCurrent).Current.ProjectID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CROpportunityProducts.customerID> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts.customerID>, object, object>) e).NewValue = (object) ((PXSelectBase<CRQuote>) this.QuoteCurrent).Current.BAccountID;
  }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (CRQuote.productCntr))]
  [PXMergeAttributes]
  [PXUIField(DisplayName = "Line Nbr.")]
  protected virtual void CROpportunityProducts_LineNbr_CacheAttached(PXCache e)
  {
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Price", Visible = false)]
  protected virtual void CROpportunityProducts_ManualPrice_CacheAttached(PXCache e)
  {
  }

  [PXDBInt]
  [PXDBDefault(typeof (CRQuote.bAccountID))]
  protected virtual void CROpportunityProducts_CustomerID_CacheAttached(PXCache e)
  {
  }

  protected virtual void CROpportunityProducts_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is CROpportunityProducts row))
      return;
    bool? nullable = row.ManualDisc;
    int num;
    if (!nullable.GetValueOrDefault())
    {
      nullable = row.IsFree;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 0;
    if (num != 0)
    {
      PXUIFieldAttribute.SetEnabled<CROpportunityProducts.taxCategoryID>(sender, e.Row);
      PXUIFieldAttribute.SetEnabled<CROpportunityProducts.descr>(sender, e.Row);
    }
    PXUIFieldAttribute.SetEnabled<CROpportunityProducts.skipLineDiscounts>(sender, (object) row, ((PXGraph) this).IsCopyPasteContext);
  }

  [PopupMessage]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noSales>>>>), "The inventory item is {0}.", new System.Type[] {typeof (PX.Objects.IN.InventoryItem.itemStatus)}, ShowWarning = true)]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.inventoryID> e)
  {
  }

  protected virtual void CROpportunityProducts_IsFree_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CROpportunityProducts row) || !row.InventoryID.HasValue)
      return;
    bool? isFree = row.IsFree;
    bool flag = false;
    if (!(isFree.GetValueOrDefault() == flag & isFree.HasValue))
      return;
    ((PXGraph) this).Caches[typeof (CROpportunityProducts)].SetDefaultExt<CROpportunityProducts.curyUnitPrice>((object) row);
  }

  [PXMergeAttributes]
  [RestrictSiteByBranch(typeof (CRQuote.branchID), null, ShowWarning = true)]
  [PXDefault(typeof (Coalesce<Search<Location.cSiteID, Where<Location.bAccountID, Equal<Current<CRQuote.bAccountID>>, And<Location.locationID, Equal<Current<CRQuote.locationID>>>>>, Search<InventoryItemCurySettings.dfltSiteID, Where<InventoryItemCurySettings.inventoryID, Equal<Current<CROpportunityProducts.inventoryID>>, And<InventoryItemCurySettings.curyID, EqualBaseCuryID<Current2<CRQuote.branchID>>>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.siteID> e)
  {
  }

  [PXDBGuid(false, IsKey = true)]
  [PXDBDefault(typeof (CRQuote.quoteID))]
  protected virtual void CROpportunityDiscountDetail_QuoteID_CacheAttached(PXCache sender)
  {
  }

  [PXDBUShort]
  [PXLineNbr(typeof (CRQuote))]
  protected virtual void CROpportunityDiscountDetail_LineNbr_CacheAttached(PXCache e)
  {
  }

  [PXDBGuid(false, IsKey = true)]
  [PXDBDefault(typeof (CRQuote.quoteID))]
  [PXParent(typeof (Select<CRQuote, Where<CRQuote.quoteID, Equal<Current<CROpportunityTax.quoteID>>>>))]
  protected virtual void CROpportunityTax_QuoteID_CacheAttached(PXCache sender)
  {
  }

  [PXDBLong]
  [CurrencyInfo(typeof (CRQuote.curyInfoID))]
  protected virtual void CROpportunityTax_CuryInfoID_CacheAttached(PXCache e)
  {
  }

  [PXDBGuid(false, IsKey = true)]
  [PXDBDefault(typeof (CRQuote.quoteID))]
  [PXParent(typeof (Select<CRQuote, Where<CRQuote.quoteID, Equal<Current<CRTaxTran.quoteID>>>>))]
  protected virtual void CRTaxTran_QuoteID_CacheAttached(PXCache sender)
  {
  }

  [PXDBLong]
  [CurrencyInfo(typeof (CRQuote.curyInfoID))]
  protected virtual void CRTaxTran_CuryInfoID_CacheAttached(PXCache e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Business Account")]
  protected virtual void _(PX.Data.Events.CacheAttached<BAccount.acctCD> e)
  {
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Business Account Name")]
  protected virtual void _(PX.Data.Events.CacheAttached<BAccount.acctName> e)
  {
  }

  [PXBool]
  [PXDefault(false)]
  [PXDBCalced(typeof (True), typeof (bool))]
  protected virtual void BAccountR_ViewInCrm_CacheAttached(PXCache sender)
  {
  }

  private BAccount SelectAccount(string acctCD)
  {
    if (string.IsNullOrEmpty(acctCD))
      return (BAccount) null;
    return PXResultset<BAccount>.op_Implicit(PXSelectBase<BAccount, PXSelectReadonly<BAccount, Where<BAccount.acctCD, Equal<Required<BAccount.acctCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) acctCD
    }));
  }

  private bool VerifyField<TField>(object row, object newValue) where TField : IBqlField
  {
    if (row == null)
      return true;
    bool flag = false;
    PXCache cach = ((PXGraph) this).Caches[row.GetType()];
    try
    {
      flag = cach.RaiseFieldVerifying<TField>(row, ref newValue);
    }
    catch (StackOverflowException ex)
    {
      throw;
    }
    catch (OutOfMemoryException ex)
    {
      throw;
    }
    catch (Exception ex)
    {
    }
    return flag;
  }

  private void FillDefaultBAccountID(PXCache cache, CRQuote row)
  {
    if (row == null || !row.ContactID.HasValue)
      return;
    Contact contact = PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelectReadonly<Contact, Where<Contact.contactID, Equal<Required<Contact.contactID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ContactID
    }));
    if (contact == null)
      return;
    row.ParentBAccountID = contact.ParentBAccountID;
    cache.SetValueExt<CRQuote.bAccountID>((object) row, (object) contact.BAccountID);
  }

  private bool IsMultyCurrency => PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();

  private bool IsSingleQuote(string opportunityId)
  {
    return PXSelectBase<CRQuote, PXSelect<CRQuote, Where<CRQuote.opportunityID, Equal<Optional<CRQuote.opportunityID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) opportunityId
    }).Count == 0;
  }

  private bool IsFirstQuote(string opportunityId)
  {
    return PXSelectBase<CRQuote, PXSelectReadonly<CRQuote, Where<CRQuote.opportunityID, Equal<Required<CRQuote.opportunityID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) opportunityId
    }).Count == 0;
  }

  private IEnumerable SelectProducts(object quoteId)
  {
    if (quoteId == null)
      return (IEnumerable) new CROpportunityProducts[0];
    return (IEnumerable) GraphHelper.RowCast<CROpportunityProducts>((IEnumerable) PXSelectBase<CROpportunityProducts, PXSelect<CROpportunityProducts, Where<CROpportunityProducts.quoteID, Equal<Required<CRQuote.quoteID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      quoteId
    }));
  }

  public virtual bool IsExternalTax(string taxZoneID) => false;

  public virtual CRQuote CalculateExternalTax(CRQuote quote) => quote;

  public virtual void Persist()
  {
    foreach (CRQuote row in ((PXSelectBase) this.Quote).Cache.Deleted)
    {
      if (this.IsSingleQuote(row.OpportunityID))
      {
        this.SuppressCascadeDeletion(((PXSelectBase) this.Products).View, (object) row);
        this.SuppressCascadeDeletion(((PXSelectBase) this.Taxes).View, (object) row);
        this.SuppressCascadeDeletion(((PXSelectBase) this.TaxLines).View, (object) row);
        this.SuppressCascadeDeletion(((PXSelectBase) this._DiscountDetails).View, (object) row);
      }
    }
    ((PXGraph) this).Persist();
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if ("Products".Equals(viewName, StringComparison.OrdinalIgnoreCase))
    {
      if (values.Contains((object) "opportunityID"))
        values[(object) "opportunityID"] = (object) ((PXSelectBase<CRQuote>) this.Quote).Current.OpportunityID;
      else
        values.Add((object) "opportunityID", (object) ((PXSelectBase<CRQuote>) this.Quote).Current.OpportunityID);
      object obj;
      if ((!PXExecutionContext.Current.Bag.TryGetValue("_DONT_UPDATE_EXIST_RECORDS", out obj) || !(obj is bool flag) || !flag) && values.Contains((object) "inventoryID") && values.Contains((object) "siteID"))
      {
        Guid? quoteId = ((PXSelectBase<CRQuote>) this.Quote).Current.QuoteID;
        string str1 = (string) values[(object) "inventoryID"];
        string str2 = (string) values[(object) "siteID"];
        CROpportunityProducts opportunityProducts = (CROpportunityProducts) null;
        if (quoteId.HasValue && !string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(str2))
          opportunityProducts = ((PXSelectBase<CROpportunityProducts>) this.ProductsByQuoteIDAndInventoryCD).SelectSingle(new object[3]
          {
            (object) quoteId,
            (object) str1,
            (object) str2
          });
        if (opportunityProducts != null)
        {
          keys[(object) "quoteID"] = (object) opportunityProducts.QuoteID;
          keys[(object) "lineNbr"] = (object) opportunityProducts.LineNbr;
        }
        else
        {
          keys[(object) "quoteID"] = (object) null;
          keys[(object) "lineNbr"] = (object) null;
        }
      }
    }
    return true;
  }

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  [Serializable]
  public class CopyQuoteFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _Description;

    [PXString]
    [PXUIField(DisplayName = "Opportunity ID", Visible = false)]
    [PXSelector(typeof (Search2<CROpportunity.opportunityID, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CROpportunity.bAccountID>>, LeftJoin<Contact, On<Contact.contactID, Equal<CROpportunity.contactID>>>>, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CROpportunity.isActive, Equal<True>>>>>.And<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccount.bAccountID, IsNull>>>>.Or<MatchUserFor<BAccount>>>>>, OrderBy<Desc<CROpportunity.opportunityID>>>), new System.Type[] {typeof (CROpportunity.opportunityID), typeof (CROpportunity.subject), typeof (CROpportunity.status), typeof (CROpportunity.stageID), typeof (CROpportunity.classID), typeof (BAccount.acctName), typeof (Contact.displayName), typeof (CROpportunity.subject), typeof (CROpportunity.externalRef), typeof (CROpportunity.closeDate)}, Filterable = true)]
    [PXDefault]
    public virtual string OpportunityID { get; set; }

    [PXDefault]
    [PXString(60, IsUnicode = true)]
    [PXUIField(DisplayName = "Description", Required = true)]
    public virtual string Description
    {
      get => this._Description;
      set => this._Description = value;
    }

    [PXBool]
    [PXUIField(DisplayName = "Set Current Unit Prices")]
    public virtual bool? RecalculatePrices { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Override Manual Prices", Enabled = false)]
    public virtual bool? OverrideManualPrices { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Recalculate Discounts")]
    public virtual bool? RecalculateDiscounts { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Override Manual Line Discounts", Enabled = false)]
    public virtual bool? OverrideManualDiscounts { get; set; }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Override Manual Group and Document Discounts")]
    public virtual bool? OverrideManualDocGroupDiscounts { get; set; }

    public abstract class opportunityId : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      QuoteMaint.CopyQuoteFilter.opportunityId>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      QuoteMaint.CopyQuoteFilter.description>
    {
    }

    public abstract class recalculatePrices : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      QuoteMaint.CopyQuoteFilter.recalculatePrices>
    {
    }

    public abstract class overrideManualPrices : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      QuoteMaint.CopyQuoteFilter.overrideManualPrices>
    {
    }

    public abstract class recalculateDiscounts : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      QuoteMaint.CopyQuoteFilter.recalculateDiscounts>
    {
    }

    public abstract class overrideManualDiscounts : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      QuoteMaint.CopyQuoteFilter.overrideManualDiscounts>
    {
    }

    public abstract class overrideManualDocGroupDiscounts : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      QuoteMaint.CopyQuoteFilter.overrideManualDocGroupDiscounts>
    {
    }
  }

  /// <exclude />
  public class MultiCurrency : CRMultiCurrencyGraph<QuoteMaint, CRQuote>
  {
    protected override MultiCurrencyGraph<QuoteMaint, CRQuote>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<QuoteMaint, CRQuote>.DocumentMapping(typeof (CRQuote))
      {
        DocumentDate = typeof (CRQuote.documentDate)
      };
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[5]
      {
        (PXSelectBase) this.Base.Quote,
        (PXSelectBase) this.Base.Products,
        (PXSelectBase) this.Base.TaxLines,
        (PXSelectBase) this.Base.Taxes,
        (PXSelectBase) ((PXGraph) this.Base).GetExtension<QuoteMaint.Discount>().DiscountDetails
      };
    }

    protected override BAccount GetRelatedBAccount()
    {
      return KeysRelation<Field<CRQuote.bAccountID>.IsRelatedTo<BAccount.bAccountID>.AsSimpleKey.WithTablesOf<BAccount, CRQuote>, BAccount, CRQuote>.Dirty.FindParent((PXGraph) this.Base, ((PXSelectBase<CRQuote>) this.Base.Quote).Current, (PKFindOptions) 0);
    }

    protected override System.Type BAccountField => typeof (CRQuote.bAccountID);

    protected override PXView DetailsView => ((PXSelectBase) this.Base.Products).View;

    protected override bool AllowOverrideCury()
    {
      bool? opportunityIsActive = (bool?) ((PXSelectBase<CRQuote>) this.Base.QuoteCurrent).Current?.OpportunityIsActive;
      return (!opportunityIsActive.HasValue || opportunityIsActive.GetValueOrDefault()) && base.AllowOverrideCury();
    }
  }

  /// <exclude />
  public class SalesPrice : SalesPriceGraph<QuoteMaint, CRQuote>
  {
    protected override SalesPriceGraph<QuoteMaint, CRQuote>.DocumentMapping GetDocumentMapping()
    {
      return new SalesPriceGraph<QuoteMaint, CRQuote>.DocumentMapping(typeof (CRQuote))
      {
        CuryInfoID = typeof (CRQuote.curyInfoID)
      };
    }

    protected override SalesPriceGraph<QuoteMaint, CRQuote>.DetailMapping GetDetailMapping()
    {
      return new SalesPriceGraph<QuoteMaint, CRQuote>.DetailMapping(typeof (CROpportunityProducts))
      {
        CuryLineAmount = typeof (CROpportunityProducts.curyExtPrice),
        Descr = typeof (CROpportunityProducts.descr)
      };
    }

    protected override SalesPriceGraph<QuoteMaint, CRQuote>.PriceClassSourceMapping GetPriceClassSourceMapping()
    {
      return new SalesPriceGraph<QuoteMaint, CRQuote>.PriceClassSourceMapping(typeof (Location))
      {
        PriceClassID = typeof (Location.cPriceClassID)
      };
    }
  }

  /// <exclude />
  public class Discount : DiscountGraph<QuoteMaint, CRQuote>
  {
    [PXViewName("Discount Details")]
    public PXSelect<CROpportunityDiscountDetail, Where<CROpportunityDiscountDetail.quoteID, Equal<Current<CRQuote.quoteID>>>, OrderBy<Asc<CROpportunityDiscountDetail.lineNbr>>> DiscountDetails;
    public PXAction<CRQuote> graphRecalculateDiscountsAction;

    protected override bool AddDocumentDiscount => true;

    protected override DiscountGraph<QuoteMaint, CRQuote>.DocumentMapping GetDocumentMapping()
    {
      return new DiscountGraph<QuoteMaint, CRQuote>.DocumentMapping(typeof (CRQuote))
      {
        CuryDiscTot = typeof (CRQuote.curyLineDocDiscountTotal)
      };
    }

    protected override DiscountGraph<QuoteMaint, CRQuote>.DetailMapping GetDetailMapping()
    {
      return new DiscountGraph<QuoteMaint, CRQuote>.DetailMapping(typeof (CROpportunityProducts))
      {
        CuryLineAmount = typeof (CROpportunityProducts.curyAmount),
        Quantity = typeof (CROpportunityProducts.quantity)
      };
    }

    protected override DiscountGraph<QuoteMaint, CRQuote>.DiscountMapping GetDiscountMapping()
    {
      return new DiscountGraph<QuoteMaint, CRQuote>.DiscountMapping(typeof (CROpportunityDiscountDetail));
    }

    [PXSelector(typeof (Search<ARDiscount.discountID, Where<ARDiscount.type, NotEqual<DiscountType.LineDiscount>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouse>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndCustomer>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndCustomerPrice>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndInventory>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndInventoryPrice>>>>>>>>))]
    [PXMergeAttributes]
    public override void Discount_DiscountID_CacheAttached(PXCache sender)
    {
    }

    [PXMergeAttributes]
    [CurrencyInfo(typeof (CRQuote.curyInfoID))]
    public override void Discount_CuryInfoID_CacheAttached(PXCache sender)
    {
    }

    protected virtual void Discount_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
    {
      if (!this.Base.IsExternalTax(((PXSelectBase<CRQuote>) this.Base.Quote).Current.TaxZoneID))
        return;
      ((PXSelectBase<CRQuote>) this.Base.Quote).Current.IsTaxValid = new bool?(false);
    }

    protected virtual void Discount_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
    {
      if (!this.Base.IsExternalTax(((PXSelectBase<CRQuote>) this.Base.Quote).Current.TaxZoneID))
        return;
      ((PXSelectBase<CRQuote>) this.Base.Quote).Current.IsTaxValid = new bool?(false);
    }

    protected virtual void Discount_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
    {
      if (!this.Base.IsExternalTax(((PXSelectBase<CRQuote>) this.Base.Quote).Current.TaxZoneID))
        return;
      ((PXSelectBase<CRQuote>) this.Base.Quote).Current.IsTaxValid = new bool?(false);
    }

    protected override void DefaultDiscountAccountAndSubAccount(PX.Objects.Extensions.Discount.Detail det)
    {
    }

    [PXUIField]
    [PXButton]
    public virtual IEnumerable GraphRecalculateDiscountsAction(PXAdapter adapter)
    {
      List<CRQuote> crQuoteList = new List<CRQuote>(adapter.Get().Cast<CRQuote>());
      foreach (CRQuote crQuote in crQuoteList)
      {
        if (((PXSelectBase) this.recalcdiscountsfilter).View.Answer == null)
        {
          ((PXSelectBase) this.recalcdiscountsfilter).Cache.Clear();
          RecalcDiscountsParamFilter discountsParamFilter = ((PXSelectBase) this.recalcdiscountsfilter).Cache.Insert() as RecalcDiscountsParamFilter;
          discountsParamFilter.RecalcUnitPrices = new bool?(true);
          discountsParamFilter.RecalcDiscounts = new bool?(true);
          discountsParamFilter.OverrideManualPrices = new bool?(false);
          discountsParamFilter.OverrideManualDiscounts = new bool?(false);
          discountsParamFilter.OverrideManualDocGroupDiscounts = new bool?(false);
        }
        if (((PXSelectBase<RecalcDiscountsParamFilter>) this.recalcdiscountsfilter).AskExt() != 1)
          return (IEnumerable) crQuoteList;
        this.RecalculateDiscountsAction(adapter);
      }
      return (IEnumerable) crQuoteList;
    }
  }

  /// <exclude />
  public class SalesTax : TaxGraph<QuoteMaint, CRQuote>
  {
    protected override bool CalcGrossOnDocumentLevel
    {
      get => true;
      set => base.CalcGrossOnDocumentLevel = value;
    }

    protected override PXView DocumentDetailsView => ((PXSelectBase) this.Base.Products).View;

    protected override TaxBaseGraph<QuoteMaint, CRQuote>.DocumentMapping GetDocumentMapping()
    {
      return new TaxBaseGraph<QuoteMaint, CRQuote>.DocumentMapping(typeof (CRQuote))
      {
        BranchID = typeof (CRQuote.branchID),
        CuryID = typeof (CRQuote.curyID),
        CuryInfoID = typeof (CRQuote.curyInfoID),
        DocumentDate = typeof (CRQuote.documentDate),
        TaxZoneID = typeof (CRQuote.taxZoneID),
        TermsID = typeof (CRQuote.termsID),
        CuryLinetotal = typeof (CRQuote.curyLineTotal),
        CuryDiscountLineTotal = typeof (CRQuote.curyLineDiscountTotal),
        CuryExtPriceTotal = typeof (CRQuote.curyExtPriceTotal),
        CuryDocBal = typeof (CRQuote.curyProductsAmount),
        CuryTaxTotal = typeof (CRQuote.curyTaxTotal),
        CuryDiscTot = typeof (CRQuote.curyDiscTot),
        TaxCalcMode = typeof (CRQuote.taxCalcMode)
      };
    }

    protected override TaxBaseGraph<QuoteMaint, CRQuote>.DetailMapping GetDetailMapping()
    {
      return new TaxBaseGraph<QuoteMaint, CRQuote>.DetailMapping(typeof (CROpportunityProducts))
      {
        CuryInfoID = typeof (CROpportunityProducts.curyInfoID),
        TaxCategoryID = typeof (CROpportunityProducts.taxCategoryID),
        GroupDiscountRate = typeof (CROpportunityProducts.groupDiscountRate),
        DocumentDiscountRate = typeof (CROpportunityProducts.documentDiscountRate),
        CuryTranAmt = typeof (CROpportunityProducts.curyAmount),
        CuryTranDiscount = typeof (CROpportunityProducts.curyDiscAmt),
        CuryTranExtPrice = typeof (CROpportunityProducts.curyExtPrice),
        InventoryID = typeof (CROpportunityProducts.inventoryID),
        Qty = typeof (CROpportunityProducts.quantity),
        UOM = typeof (CROpportunityProducts.uOM)
      };
    }

    protected override TaxBaseGraph<QuoteMaint, CRQuote>.TaxDetailMapping GetTaxDetailMapping()
    {
      return new TaxBaseGraph<QuoteMaint, CRQuote>.TaxDetailMapping(typeof (CROpportunityTax), typeof (CROpportunityTax.taxID));
    }

    protected override TaxBaseGraph<QuoteMaint, CRQuote>.TaxTotalMapping GetTaxTotalMapping()
    {
      return new TaxBaseGraph<QuoteMaint, CRQuote>.TaxTotalMapping(typeof (CRTaxTran), typeof (CRTaxTran.taxID));
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<CRQuote, CRQuote.curyDiscTot> e)
    {
      if (e.Row == null)
        return;
      bool? manualTotalEntry = e.Row.ManualTotalEntry;
      bool flag = false;
      if (!(manualTotalEntry.GetValueOrDefault() == flag & manualTotalEntry.HasValue) || PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>())
        return;
      this.CalcTotals((object) null, false);
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<CRQuote, CRQuote.manualTotalEntry> e)
    {
      if (e.Row == null)
        return;
      bool? manualTotalEntry = e.Row.ManualTotalEntry;
      bool flag = false;
      if (!(manualTotalEntry.GetValueOrDefault() == flag & manualTotalEntry.HasValue))
        return;
      this.CalcTotals((object) null, false);
    }

    protected virtual void Document_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
    {
      PX.Objects.Extensions.SalesTax.Document extension = sender.GetExtension<PX.Objects.Extensions.SalesTax.Document>(e.Row);
      if (extension == null || extension.TaxCalc.HasValue)
        return;
      extension.TaxCalc = new TaxCalc?(TaxCalc.Calc);
    }

    protected override void CalcDocTotals(
      object row,
      Decimal CuryTaxTotal,
      Decimal CuryInclTaxTotal,
      Decimal CuryWhTaxTotal)
    {
      base.CalcDocTotals(row, CuryTaxTotal, CuryInclTaxTotal, CuryWhTaxTotal);
      CRQuote main = (CRQuote) ((PXSelectBase) this.Documents).Cache.GetMain<PX.Objects.Extensions.SalesTax.Document>(((PXSelectBase<PX.Objects.Extensions.SalesTax.Document>) this.Documents).Current);
      bool valueOrDefault1 = main.ManualTotalEntry.GetValueOrDefault();
      Decimal valueOrDefault2 = main.CuryAmount.GetValueOrDefault();
      Decimal valueOrDefault3 = main.CuryDiscTot.GetValueOrDefault();
      Decimal num1 = (Decimal) (this.ParentGetValue<CROpportunity.curyLineTotal>() ?? (object) 0M);
      Decimal num2 = (Decimal) (this.ParentGetValue<CROpportunity.curyLineDiscountTotal>() ?? (object) 0M);
      Decimal num3 = (Decimal) (this.ParentGetValue<CROpportunity.curyExtPriceTotal>() ?? (object) 0M);
      Decimal num4 = (Decimal) (this.ParentGetValue<CROpportunity.curyDiscTot>() ?? (object) 0M);
      Decimal objA = valueOrDefault1 ? valueOrDefault2 - valueOrDefault3 : num1 - num4 + CuryTaxTotal - CuryInclTaxTotal;
      if (object.Equals((object) objA, (object) (Decimal) (this.ParentGetValue<CRQuote.curyProductsAmount>() ?? (object) 0M)))
        return;
      this.ParentSetValue<CRQuote.curyProductsAmount>((object) objA);
    }

    protected override string GetExtCostLabel(PXCache sender, object row)
    {
      return ((PXFieldState) sender.GetValueExt<CROpportunityProducts.curyExtPrice>(row)).DisplayName;
    }

    protected override void SetExtCostExt(PXCache sender, object child, Decimal? value)
    {
      if (!(child is PXResult<PX.Objects.Extensions.SalesTax.Detail> pxResult))
        return;
      ((CROpportunityProducts) PXResult.Unwrap<PX.Objects.Extensions.SalesTax.Detail>((object) pxResult).Base).CuryExtPrice = value;
      sender.Update((object) pxResult);
    }

    protected override List<object> SelectTaxes<Where>(
      PXGraph graph,
      object row,
      PXTaxCheck taxchk,
      params object[] parameters)
    {
      IComparer<PX.Objects.TX.Tax> calculationLevelComparer = this.GetTaxByCalculationLevelComparer();
      ExceptionExtensions.ThrowOnNull<IComparer<PX.Objects.TX.Tax>>(calculationLevelComparer, "taxComparer", (string) null);
      Dictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> dictionary = new Dictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>>();
      object[] objArray = new object[2]
      {
        row == null || !(row is PX.Objects.Extensions.SalesTax.Detail) ? (object) null : ((PXSelectBase) this.Details).Cache.GetMain<PX.Objects.Extensions.SalesTax.Detail>((PX.Objects.Extensions.SalesTax.Detail) row),
        (object) ((PXSelectBase<CRQuote>) ((QuoteMaint) graph).Quote).Current
      };
      foreach (PXResult<PX.Objects.TX.Tax, TaxRev> pxResult in PXSelectBase<PX.Objects.TX.Tax, PXSelectReadonly2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<boolFalse>, And<TaxRev.taxType, Equal<TaxType.sales>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.use>, And<PX.Objects.TX.Tax.reverseTax, Equal<boolFalse>, And<Current<CRQuote.documentDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>>>, Where>.Config>.SelectMultiBound(graph, objArray, parameters))
        dictionary[PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult).TaxID] = pxResult;
      List<object> objectList = new List<object>();
      switch (taxchk)
      {
        case PXTaxCheck.Line:
          foreach (PXResult<CROpportunityTax> pxResult1 in PXSelectBase<CROpportunityTax, PXSelect<CROpportunityTax, Where<CROpportunityTax.quoteID, Equal<Current<CRQuote.quoteID>>, And<CROpportunityTax.quoteID, Equal<Current<CROpportunityProducts.quoteID>>, And<CROpportunityTax.lineNbr, Equal<Current<CROpportunityProducts.lineNbr>>>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
          {
            CROpportunityTax crOpportunityTax = PXResult<CROpportunityTax>.op_Implicit(pxResult1);
            PXResult<PX.Objects.TX.Tax, TaxRev> pxResult2;
            if (dictionary.TryGetValue(crOpportunityTax.TaxID, out pxResult2))
            {
              int count = objectList.Count;
              while (count > 0 && calculationLevelComparer.Compare(PXResult<CROpportunityTax, PX.Objects.TX.Tax, TaxRev>.op_Implicit((PXResult<CROpportunityTax, PX.Objects.TX.Tax, TaxRev>) objectList[count - 1]), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult2)) > 0)
                --count;
              PX.Objects.TX.Tax tax = this.AdjustTaxLevel(PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult2));
              objectList.Insert(count, (object) new PXResult<CROpportunityTax, PX.Objects.TX.Tax, TaxRev>(crOpportunityTax, tax, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult2)));
            }
          }
          return objectList;
        case PXTaxCheck.RecalcLine:
          foreach (PXResult<CROpportunityTax> pxResult3 in PXSelectBase<CROpportunityTax, PXSelect<CROpportunityTax, Where<CROpportunityTax.quoteID, Equal<Current<CRQuote.quoteID>>, And<CROpportunityTax.lineNbr, Less<intMax>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
          {
            CROpportunityTax crOpportunityTax = PXResult<CROpportunityTax>.op_Implicit(pxResult3);
            PXResult<PX.Objects.TX.Tax, TaxRev> pxResult4;
            if (dictionary.TryGetValue(crOpportunityTax.TaxID, out pxResult4))
            {
              int count;
              for (count = objectList.Count; count > 0; --count)
              {
                int? lineNbr1 = PXResult<CROpportunityTax, PX.Objects.TX.Tax, TaxRev>.op_Implicit((PXResult<CROpportunityTax, PX.Objects.TX.Tax, TaxRev>) objectList[count - 1]).LineNbr;
                int? lineNbr2 = crOpportunityTax.LineNbr;
                if (!(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue) || calculationLevelComparer.Compare(PXResult<CROpportunityTax, PX.Objects.TX.Tax, TaxRev>.op_Implicit((PXResult<CROpportunityTax, PX.Objects.TX.Tax, TaxRev>) objectList[count - 1]), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult4)) <= 0)
                  break;
              }
              PX.Objects.TX.Tax tax = this.AdjustTaxLevel(PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult4));
              objectList.Insert(count, (object) new PXResult<CROpportunityTax, PX.Objects.TX.Tax, TaxRev>(crOpportunityTax, tax, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult4)));
            }
          }
          return objectList;
        case PXTaxCheck.RecalcTotals:
          foreach (PXResult<CRTaxTran> pxResult5 in PXSelectBase<CRTaxTran, PXSelect<CRTaxTran, Where<CRTaxTran.quoteID, Equal<Current<CRQuote.quoteID>>>, OrderBy<Asc<CRTaxTran.lineNbr, Asc<CRTaxTran.taxID>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
          {
            CRTaxTran crTaxTran = PXResult<CRTaxTran>.op_Implicit(pxResult5);
            PXResult<PX.Objects.TX.Tax, TaxRev> pxResult6;
            if (crTaxTran.TaxID != null && dictionary.TryGetValue(crTaxTran.TaxID, out pxResult6))
            {
              int count = objectList.Count;
              while (count > 0 && calculationLevelComparer.Compare(PXResult<CRTaxTran, PX.Objects.TX.Tax, TaxRev>.op_Implicit((PXResult<CRTaxTran, PX.Objects.TX.Tax, TaxRev>) objectList[count - 1]), PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult6)) > 0)
                --count;
              PX.Objects.TX.Tax tax = this.AdjustTaxLevel(PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult6));
              objectList.Insert(count, (object) new PXResult<CRTaxTran, PX.Objects.TX.Tax, TaxRev>(crTaxTran, tax, PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult6)));
            }
          }
          return objectList;
        default:
          return objectList;
      }
    }

    protected override List<object> SelectDocumentLines(PXGraph graph, object row)
    {
      return GraphHelper.RowCast<CROpportunityProducts>((IEnumerable) PXSelectBase<CROpportunityProducts, PXSelect<CROpportunityProducts, Where<CROpportunityProducts.quoteID, Equal<Current<CRQuote.quoteID>>>>.Config>.SelectMultiBound(graph, new object[1]
      {
        row
      }, Array.Empty<object>())).Select<CROpportunityProducts, object>((Func<CROpportunityProducts, object>) (_ => (object) _)).ToList<object>();
    }

    protected virtual void CRTaxTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
    {
      if (e.Row == null)
        return;
      PXUIFieldAttribute.SetEnabled<CRTaxTran.taxID>(sender, e.Row, sender.GetStatus(e.Row) == 2);
    }

    protected virtual void CRTaxTran_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
    {
      if (!(e.Row is CRTaxTran row))
        return;
      if (e.Operation == 3)
      {
        CROpportunityTax crOpportunityTax = (CROpportunityTax) ((PXSelectBase) this.Base.TaxLines).Cache.Locate((object) this.FindCROpportunityTax(row));
        if (((PXSelectBase) this.Base.TaxLines).Cache.GetStatus((object) crOpportunityTax) == 3 || ((PXSelectBase) this.Base.TaxLines).Cache.GetStatus((object) crOpportunityTax) == 4)
          ((CancelEventArgs) e).Cancel = true;
      }
      if (e.Operation != 1 || ((PXSelectBase) this.Base.TaxLines).Cache.GetStatus((object) (CROpportunityTax) ((PXSelectBase) this.Base.TaxLines).Cache.Locate((object) this.FindCROpportunityTax(row))) != 1)
        return;
      ((CancelEventArgs) e).Cancel = true;
    }

    protected virtual CROpportunityTax FindCROpportunityTax(CRTaxTran tran)
    {
      return GraphHelper.RowCast<CROpportunityTax>((IEnumerable) PXSelectBase<CROpportunityTax, PXSelect<CROpportunityTax, Where<CROpportunityTax.quoteID, Equal<Required<CRTaxTran.quoteID>>, And<CROpportunityTax.lineNbr, Equal<Required<CRTaxTran.lineNbr>>, And<CROpportunityTax.taxID, Equal<Required<CRTaxTran.taxID>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[0], new object[3]
      {
        (object) tran.QuoteID,
        (object) tran.LineNbr,
        (object) tran.TaxID
      })).FirstOrDefault<CROpportunityTax>();
    }
  }

  /// <summary>
  /// A per-unit tax graph extension for which will forbid edit of per-unit taxes in UI.
  /// </summary>
  public class PerUnitTaxDisableExt : PerUnitTaxDataEntryGraphExtension<QuoteMaint, CRTaxTran>
  {
    public static bool IsActive()
    {
      return PerUnitTaxDataEntryGraphExtension<QuoteMaint, CRTaxTran>.IsActiveBase();
    }
  }

  /// <exclude />
  public class ContactAddress : CROpportunityContactAddressExt<QuoteMaint>
  {
    public virtual void Initialize()
    {
      ((PXGraphExtension) this).Initialize();
      this.Addresses = new PXSelectExtension<DocumentAddress>((PXSelectBase) this.Base.Quote_Address);
      this.Contacts = new PXSelectExtension<DocumentContact>((PXSelectBase) this.Base.Quote_Contact);
    }

    protected override CROpportunityContactAddressExt<QuoteMaint>.DocumentMapping GetDocumentMapping()
    {
      return new CROpportunityContactAddressExt<QuoteMaint>.DocumentMapping(typeof (CRQuote))
      {
        DocumentAddressID = typeof (CRQuote.opportunityAddressID),
        DocumentContactID = typeof (CRQuote.opportunityContactID),
        ShipAddressID = typeof (CRQuote.shipAddressID),
        ShipContactID = typeof (CRQuote.shipContactID),
        BillAddressID = typeof (CRQuote.billAddressID),
        BillContactID = typeof (CRQuote.billContactID)
      };
    }

    protected override CROpportunityContactAddressExt<QuoteMaint>.DocumentContactMapping GetDocumentContactMapping()
    {
      return new CROpportunityContactAddressExt<QuoteMaint>.DocumentContactMapping(typeof (CRContact))
      {
        EMail = typeof (CRContact.email)
      };
    }

    protected override CROpportunityContactAddressExt<QuoteMaint>.DocumentAddressMapping GetDocumentAddressMapping()
    {
      return new CROpportunityContactAddressExt<QuoteMaint>.DocumentAddressMapping(typeof (CRAddress));
    }

    protected override PXCache GetContactCache() => ((PXSelectBase) this.Base.Quote_Contact).Cache;

    protected override PXCache GetAddressCache() => ((PXSelectBase) this.Base.Quote_Address).Cache;

    protected override PXCache GetShippingContactCache()
    {
      return ((PXSelectBase) this.Base.Shipping_Contact).Cache;
    }

    protected override PXCache GetShippingAddressCache()
    {
      return ((PXSelectBase) this.Base.Shipping_Address).Cache;
    }

    protected override PXCache GetBillingContactCache()
    {
      return ((PXSelectBase) this.Base.Billing_Contact).Cache;
    }

    protected override PXCache GetBillingAddressCache()
    {
      return ((PXSelectBase) this.Base.Billing_Address).Cache;
    }

    protected override IPersonalContact SelectContact()
    {
      return (IPersonalContact) ((PXSelectBase<CRContact>) this.Base.Quote_Contact).SelectSingle(Array.Empty<object>());
    }

    protected override IPersonalContact SelectShippingContact()
    {
      return (IPersonalContact) ((PXSelectBase<CRShippingContact>) this.Base.Shipping_Contact).SelectSingle(Array.Empty<object>());
    }

    protected override IPersonalContact SelectBillingContact()
    {
      return (IPersonalContact) ((PXSelectBase<CRBillingContact>) this.Base.Billing_Contact).SelectSingle(Array.Empty<object>());
    }

    protected override IPersonalContact GetEtalonContact()
    {
      return this.SafeGetEtalon(((PXSelectBase) this.Base.Quote_Contact).Cache) as IPersonalContact;
    }

    protected override IPersonalContact GetEtalonShippingContact()
    {
      return this.SafeGetEtalon(((PXSelectBase) this.Base.Shipping_Contact).Cache) as IPersonalContact;
    }

    protected override IPersonalContact GetEtalonBillingContact()
    {
      return this.SafeGetEtalon(((PXSelectBase) this.Base.Billing_Contact).Cache) as IPersonalContact;
    }

    protected override IAddress SelectAddress()
    {
      return (IAddress) ((PXSelectBase<CRAddress>) this.Base.Quote_Address).SelectSingle(Array.Empty<object>());
    }

    protected override IAddress SelectShippingAddress()
    {
      return (IAddress) ((PXSelectBase<CRShippingAddress>) this.Base.Shipping_Address).SelectSingle(Array.Empty<object>());
    }

    protected override IAddress SelectBillingAddress()
    {
      return (IAddress) ((PXSelectBase<CRBillingAddress>) this.Base.Billing_Address).SelectSingle(Array.Empty<object>());
    }

    protected override IAddress GetEtalonAddress()
    {
      return this.SafeGetEtalon(((PXSelectBase) this.Base.Quote_Address).Cache) as IAddress;
    }

    protected override IAddress GetEtalonShippingAddress()
    {
      return this.SafeGetEtalon(((PXSelectBase) this.Base.Shipping_Address).Cache) as IAddress;
    }

    protected override IAddress GetEtalonBillingAddress()
    {
      return this.SafeGetEtalon(((PXSelectBase) this.Base.Billing_Address).Cache) as IAddress;
    }

    [PXMergeAttributes]
    [PXDBDefault(typeof (CRQuote.bAccountID))]
    public virtual void _(PX.Data.Events.CacheAttached<CRContact.bAccountID> e)
    {
    }

    [PXMergeAttributes]
    [PXDBDefault(typeof (CRQuote.bAccountID))]
    public virtual void _(PX.Data.Events.CacheAttached<CRAddress.bAccountID> e)
    {
    }

    [PXMergeAttributes]
    [PXDBDefault(typeof (CRQuote.bAccountID))]
    public virtual void _(
      PX.Data.Events.CacheAttached<CRShippingContact.bAccountID> e)
    {
    }

    [PXMergeAttributes]
    [PXDBDefault(typeof (CRQuote.bAccountID))]
    public virtual void _(
      PX.Data.Events.CacheAttached<CRShippingAddress.bAccountID> e)
    {
    }

    [PXMergeAttributes]
    [PXDBDefault(typeof (CRQuote.bAccountID))]
    public virtual void _(
      PX.Data.Events.CacheAttached<CRBillingContact.bAccountID> e)
    {
    }

    [PXMergeAttributes]
    [PXDBDefault(typeof (CRQuote.bAccountID))]
    public virtual void _(
      PX.Data.Events.CacheAttached<CRBillingAddress.bAccountID> e)
    {
    }
  }

  /// <exclude />
  public class CRCreateSalesOrderExt : PX.Objects.CR.Extensions.CRCreateSalesOrder.CRCreateSalesOrder<QuoteMaint.Discount, QuoteMaint, CRQuote>
  {
    public static bool IsActive()
    {
      return PX.Objects.CR.Extensions.CRCreateSalesOrder.CRCreateSalesOrder<QuoteMaint.Discount, QuoteMaint, CRQuote>.IsExtensionActive();
    }

    public virtual void _(PX.Data.Events.RowSelected<CRQuote> e)
    {
      CRQuote row = e.Row;
      if (row == null)
        return;
      bool flag = e.Row.BAccountID.HasValue;
      if (flag)
      {
        flag = NonGenericIEnumerableExtensions.Any_((IEnumerable) ((PXSelectBase<CROpportunityProducts>) this.Base.Products).Select(Array.Empty<object>()));
        if (flag)
        {
          IEnumerable<CROpportunityProducts> source = GraphHelper.RowCast<CROpportunityProducts>((IEnumerable) ((PXSelectBase) this.Base.Products).View.SelectMultiBound(new object[1]
          {
            (object) row
          }, Array.Empty<object>()));
          flag = (!source.Any<CROpportunityProducts>((Func<CROpportunityProducts, bool>) (_ => !_.InventoryID.HasValue)) ? 0 : (!source.Any<CROpportunityProducts>((Func<CROpportunityProducts, bool>) (_ => _.InventoryID.HasValue)) ? 1 : 0)) == 0;
        }
      }
      ((PXAction) this.CreateSalesOrder).SetEnabled(flag);
    }

    [PXUIField(DisplayName = "Set Quote as Primary", Visible = true)]
    [PXMergeAttributes]
    public virtual void _(
      PX.Data.Events.CacheAttached<CreateSalesOrderFilter.makeQuotePrimary> e)
    {
    }

    public override CRQuote GetQuoteForWorkflowProcessing()
    {
      return ((PXSelectBase<CRQuote>) this.Base.QuoteCurrent).Current;
    }

    public override void DoCreateSalesOrder()
    {
      CreateSalesOrderFilter current1 = ((PXSelectBase<CreateSalesOrderFilter>) this.CreateOrderParams).Current;
      bool? nullable;
      int num1;
      if (current1 == null)
      {
        num1 = 0;
      }
      else
      {
        nullable = current1.MakeQuotePrimary;
        num1 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      if (num1 != 0)
      {
        CRQuote current2 = ((PXSelectBase<CRQuote>) this.Base.QuoteCurrent)?.Current;
        int num2;
        if (current2 == null)
        {
          num2 = 0;
        }
        else
        {
          nullable = current2.IsPrimary;
          bool flag = false;
          num2 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
        }
        if (num2 != 0)
          ((PXGraph) this.Base).Actions["PrimaryQuote"].Press();
      }
      base.DoCreateSalesOrder();
    }
  }

  /// <exclude />
  public class CRCreateInvoiceExt : PX.Objects.CR.Extensions.CRCreateInvoice.CRCreateInvoice<QuoteMaint.Discount, QuoteMaint, CRQuote>
  {
    public static bool IsActive()
    {
      return PX.Objects.CR.Extensions.CRCreateInvoice.CRCreateInvoice<QuoteMaint.Discount, QuoteMaint, CRQuote>.IsExtensionActive();
    }

    public virtual void _(PX.Data.Events.RowSelected<CRQuote> e)
    {
      CRQuote row = e.Row;
      if (row == null)
        return;
      bool flag = e.Row.BAccountID.HasValue;
      if (flag)
      {
        flag = NonGenericIEnumerableExtensions.Any_((IEnumerable) ((PXSelectBase<CROpportunityProducts>) this.Base.Products).Select(Array.Empty<object>()));
        if (flag)
        {
          IEnumerable<CROpportunityProducts> source = GraphHelper.RowCast<CROpportunityProducts>((IEnumerable) ((PXSelectBase) this.Base.Products).View.SelectMultiBound(new object[1]
          {
            (object) row
          }, Array.Empty<object>()));
          flag = (!source.Any<CROpportunityProducts>((Func<CROpportunityProducts, bool>) (_ => !_.InventoryID.HasValue)) ? 0 : (!source.Any<CROpportunityProducts>((Func<CROpportunityProducts, bool>) (_ => _.InventoryID.HasValue)) ? 1 : 0)) == 0;
        }
      }
      ((PXAction) this.CreateInvoice).SetEnabled(flag);
    }

    [PXUIField(DisplayName = "Set Quote as Primary", Visible = true)]
    [PXMergeAttributes]
    public virtual void _(
      PX.Data.Events.CacheAttached<CreateInvoicesFilter.makeQuotePrimary> e)
    {
    }

    public override CRQuote GetQuoteForWorkflowProcessing()
    {
      return ((PXSelectBase<CRQuote>) this.Base.QuoteCurrent).Current;
    }

    protected override void DoCreateInvoice()
    {
      CreateInvoicesFilter current1 = ((PXSelectBase<CreateInvoicesFilter>) this.CreateInvoicesParams).Current;
      bool? nullable;
      int num1;
      if (current1 == null)
      {
        num1 = 0;
      }
      else
      {
        nullable = current1.MakeQuotePrimary;
        num1 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      if (num1 != 0)
      {
        CRQuote current2 = ((PXSelectBase<CRQuote>) this.Base.QuoteCurrent)?.Current;
        int num2;
        if (current2 == null)
        {
          num2 = 0;
        }
        else
        {
          nullable = current2.IsPrimary;
          bool flag = false;
          num2 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
        }
        if (num2 != 0)
          ((PXGraph) this.Base).Actions["PrimaryQuote"].Press();
      }
      base.DoCreateInvoice();
    }
  }

  /// <exclude />
  public class QuoteMaintAddressLookupExtension : 
    AddressLookupExtension<QuoteMaint, CRQuote, CRAddress>
  {
    protected override string AddressView => "Quote_Address";

    protected override string ViewOnMap => "viewMainOnMap";
  }

  /// <exclude />
  public class QuoteMaintShippingAddressLookupExtension : 
    AddressLookupExtension<QuoteMaint, CRQuote, CRShippingAddress>
  {
    protected override string AddressView => "Shipping_Address";

    protected override string ViewOnMap => "ViewShippingOnMap";
  }

  /// <exclude />
  public class QuoteMaintBillingAddressLookupExtension : 
    AddressLookupExtension<QuoteMaint, CRQuote, CRBillingAddress>
  {
    protected override string AddressView => "Billing_Address";

    protected override string ViewOnMap => "ViewBillingOnMap";
  }

  /// <exclude />
  public class ExtensionSort : 
    SortExtensionsBy<TypeArrayOf<PXGraphExtension<QuoteMaint>>.FilledWith<QuoteMaint.ContactAddress, QuoteMaint.MultiCurrency, QuoteMaint.SalesPrice, QuoteMaint.Discount, QuoteMaint.SalesTax>>
  {
  }
}
