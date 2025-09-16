// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.OpportunityMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.DependencyInjection;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.LicensePolicy;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Discount;
using PX.Objects.CR.Extensions;
using PX.Objects.CR.Extensions.CRCreateActions;
using PX.Objects.CR.Extensions.CROpportunityContactAddress;
using PX.Objects.CR.OpportunityMaint_Extensions;
using PX.Objects.CR.Standalone;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.Extensions.Discount;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.Extensions.MultiCurrency.CR;
using PX.Objects.Extensions.PerUnitTax;
using PX.Objects.Extensions.SalesPrice;
using PX.Objects.Extensions.SalesTax;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.IN.Attributes;
using PX.Objects.PM;
using PX.Objects.SO;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.CR;

public class OpportunityMaint : 
  PXGraph<
  #nullable disable
  OpportunityMaint>,
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
  [PXHidden]
  public PXSelect<PX.Objects.AP.Vendor> vendorDummy;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (CROpportunity.stageID), typeof (CROpportunity.resolution)})]
  [PXViewName("Opportunity")]
  public PXSelectJoin<CROpportunity, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CROpportunity.bAccountID>>>, Where<BAccount.bAccountID, IsNull, Or<Match<BAccount, Current<AccessInfo.userName>>>>> Opportunity;
  [PXHidden]
  public PXSelect<CROpportunityRevision> OpportunityRevision;
  [PXHidden]
  public PXSelect<CRLead> Leads;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (CROpportunity.stageID), typeof (CROpportunity.resolution)})]
  public PXSelect<CROpportunity, Where<CROpportunity.opportunityID, Equal<Current<CROpportunity.opportunityID>>>> OpportunityCurrent;
  [PXHidden]
  public PXSelectReadonly<CRQuote, Where<CRQuote.quoteID, Equal<Current<CROpportunity.quoteNoteID>>>> PrimaryQuoteQuery;
  public PXSelect<CROpportunityProbability, Where<CROpportunityProbability.stageCode, Equal<Current<CROpportunity.stageID>>>> ProbabilityCurrent;
  [PXHidden]
  public PXSelect<PX.Objects.CR.Address> Address;
  [PXHidden]
  public PXSetup<Contact, Where<Contact.contactID, Equal<Optional<CROpportunity.contactID>>>> Contacts;
  [PXHidden]
  public PXSetup<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Optional<CROpportunity.bAccountID>>>> customer;
  [PXViewName("Answers")]
  public CRAttributeSourceList<CROpportunity, CROpportunity.contactID> Answers;
  public PXSetup<CROpportunityClass, Where<CROpportunityClass.cROpportunityClassID, Equal<Current<CROpportunity.classID>>>> OpportunityClass;
  [PXCopyPasteHiddenView]
  public PXSelect<CRActivityStatistics> ActivityStatistics;
  [PXCopyPasteHiddenView]
  public PXSelect<CRActivityStatistics, Where<CRActivityStatistics.noteID, Equal<Current<CROpportunity.noteID>>>> ActivityOpportunityStatistics;
  [PXCopyPasteHiddenView]
  public PXSelect<CRActivityStatistics, Where<CRActivityStatistics.noteID, Equal<Current<CRQuote.noteID>>>> ActivityQuoteStatistics;
  [PXCopyPasteHiddenView]
  [PXFilterable(new System.Type[] {})]
  public PXSelect<CRQuote, Where<CRQuote.opportunityID, Equal<Current<CROpportunity.opportunityID>>>> Quotes;
  [PXViewName("Copy Quote")]
  [PXCopyPasteHiddenView]
  public PXFilter<OpportunityMaint.CopyQuoteFilter> CopyQuoteInfo;
  [PXViewName("Opportunity Products")]
  [PXImport(typeof (CROpportunity))]
  public PXOrderedSelect<CROpportunity, CROpportunityProducts, Where<CROpportunityProducts.quoteID, Equal<Current<CROpportunity.defQuoteID>>>, OrderBy<Asc<CROpportunityProducts.sortOrder>>> Products;
  public PXSelect<CROpportunityTax, Where<CROpportunityTax.quoteID, Equal<Current<CROpportunity.quoteNoteID>>, And<CROpportunityTax.lineNbr, Less<intMax>>>, OrderBy<Asc<CROpportunityTax.taxID>>> TaxLines;
  [PXViewName("Opportunity Tax")]
  public PXSelectJoin<CRTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<CRTaxTran.taxID>>>, Where<CRTaxTran.quoteID, Equal<Current<CROpportunity.quoteNoteID>>>, OrderBy<Asc<CRTaxTran.lineNbr, Asc<CRTaxTran.taxID>>>> Taxes;
  public PXSetup<Location, Where<Location.bAccountID, Equal<Current<CROpportunity.bAccountID>>, And<Location.locationID, Equal<Optional<CROpportunity.locationID>>>>> location;
  [PXViewName("Create Quote")]
  [PXCopyPasteHiddenView]
  public PXFilter<OpportunityMaint.CreateQuotesFilter> QuoteInfo;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public CRValidationFilter<PopupUDFAttributes> CreateQuoteInfoUDF;
  [PXViewName("Opportunity Contact")]
  public PXSelect<CRContact, Where<CRContact.contactID, Equal<Current<CROpportunity.opportunityContactID>>>> Opportunity_Contact;
  [PXViewName("Opportunity Address")]
  public PXSelect<CRAddress, Where<CRAddress.addressID, Equal<Current<CROpportunity.opportunityAddressID>>>> Opportunity_Address;
  [PXViewName("Shipping Contact")]
  public PXSelect<CRShippingContact, Where<CRShippingContact.contactID, Equal<Current<CROpportunity.shipContactID>>>> Shipping_Contact;
  [PXViewName("Shipping Address")]
  public PXSelect<CRShippingAddress, Where<CRShippingAddress.addressID, Equal<Current<CROpportunity.shipAddressID>>>> Shipping_Address;
  [PXViewName("Bill-To Contact")]
  public PXSelect<CRBillingContact, Where<CRBillingContact.contactID, Equal<Current<CROpportunity.billContactID>>>> Billing_Contact;
  [PXViewName("Bill-To Address")]
  public PXSelect<CRBillingAddress, Where<CRBillingAddress.addressID, Equal<Current<CROpportunity.billAddressID>>>> Billing_Address;
  [PXHidden]
  public PXSelectJoin<Contact, LeftJoin<PX.Objects.CR.Address, On<Contact.defAddressID, Equal<PX.Objects.CR.Address.addressID>>>, Where<Contact.contactID, Equal<Current<CROpportunity.contactID>>>> CurrentContact;
  [PXHidden]
  public PXSelect<SOBillingContact> CurrentSOBillingContact;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<CROpportunityProducts, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<CROpportunityProducts.inventoryID>>, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.siteID, Equal<CROpportunityProducts.siteID>>>>, Where<CROpportunityProducts.quoteID, Equal<Required<CROpportunity.quoteNoteID>>, And<Where<PX.Objects.IN.InventoryItem.inventoryCD, Equal<Required<PX.Objects.IN.InventoryItem.inventoryCD>>, And<Where<PX.Objects.IN.INSite.siteCD, Equal<Required<PX.Objects.IN.INSite.siteCD>>>>>>>> ProductsByQuoteIDAndInventoryCD;
  public PXSelect<INItemSiteSettings, Where<INItemSiteSettings.inventoryID, Equal<Required<INItemSiteSettings.inventoryID>>, And<INItemSiteSettings.siteID, Equal<Required<INItemSiteSettings.siteID>>>>> initemsettings;
  public PXSave<CROpportunity> Save;
  public PXCancel<CROpportunity> Cancel;
  public PXInsert<CROpportunity> Insert;
  public PXCopyPasteAction<CROpportunity> CopyPaste;
  public PXDelete<CROpportunity> Delete;
  public PXFirst<CROpportunity> First;
  public PXPrevious<CROpportunity> Previous;
  public PXNext<CROpportunity> Next;
  public PXLast<CROpportunity> Last;
  public PXAction<CROpportunity> createQuote;
  public PXAction<CROpportunity> actionsFolder;
  public PXAction<CROpportunity> updateClosingDate;
  public PXAction<CROpportunity> viewMainOnMap;
  public PXAction<CROpportunity> ViewShippingOnMap;
  public PXAction<CROpportunity> ViewBillingOnMap;
  public PXAction<CROpportunity> primaryQuote;
  public PXAction<CROpportunity> copyQuote;
  public PXDBAction<CROpportunity> ViewQuote;
  public PXAction<CROpportunity> ViewProject;
  public PXAction<CROpportunity> validateAddresses;
  public PXAction<CROpportunity> Open;
  public PXAction<CROpportunity> OpenFromNew;
  public PXAction<CROpportunity> CloseAsWon;
  public PXAction<CROpportunity> CloseAsLost;
  public PXWorkflowEventHandler<CROpportunity> OnOpportunityCreatedFromLead;
  public PXWorkflowEventHandler<CROpportunity> OnOpportunityLost;
  public PXWorkflowEventHandler<CROpportunity> OnOpportunityWon;
  public PXWorkflowEventHandler<CROpportunity> OnOpportunityClosed;

  public virtual IEnumerable activityStatistics()
  {
    CRActivityStatistics activityStatistics1 = ((PXSelectBase<CRActivityStatistics>) this.ActivityOpportunityStatistics).SelectSingle(Array.Empty<object>());
    CRActivityStatistics activityStatistics2 = ((PXSelectBase<CRActivityStatistics>) this.ActivityQuoteStatistics).SelectSingle(Array.Empty<object>());
    DateTime? incomingActivityDate = (DateTime?) activityStatistics1?.LastIncomingActivityDate;
    DateTime? nullable1 = (DateTime?) activityStatistics2?.LastIncomingActivityDate;
    CRActivityStatistics activityStatistics3 = (incomingActivityDate.HasValue & nullable1.HasValue ? (incomingActivityDate.GetValueOrDefault() > nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? activityStatistics1 : activityStatistics2;
    nullable1 = (DateTime?) activityStatistics1?.LastOutgoingActivityDate;
    DateTime? nullable2 = (DateTime?) activityStatistics2?.LastOutgoingActivityDate;
    CRActivityStatistics activityStatistics4 = (nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? activityStatistics1 : activityStatistics2;
    CRActivityStatistics activityStatistics5;
    if (activityStatistics1 == null || activityStatistics2 == null)
    {
      activityStatistics5 = activityStatistics1 ?? activityStatistics2;
    }
    else
    {
      CRActivityStatistics activityStatistics6 = new CRActivityStatistics();
      nullable2 = activityStatistics3.LastActivityDate;
      nullable1 = activityStatistics4.LastActivityDate;
      activityStatistics6.LastActivityDate = (nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() > nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? activityStatistics3.LastActivityDate : activityStatistics4.LastActivityDate;
      activityStatistics6.LastIncomingActivityDate = activityStatistics3.LastIncomingActivityDate;
      activityStatistics6.LastIncomingActivityNoteID = activityStatistics3.LastIncomingActivityNoteID;
      activityStatistics6.LastOutgoingActivityDate = activityStatistics4.LastOutgoingActivityDate;
      activityStatistics6.LastOutgoingActivityNoteID = activityStatistics4.LastOutgoingActivityNoteID;
      activityStatistics5 = activityStatistics6;
    }
    yield return (object) activityStatistics5;
  }

  protected virtual IEnumerable createQuoteInfoUDF()
  {
    return ((OpportunityMaint.CreateQuotesFilter) ((PXCache) GraphHelper.Caches<OpportunityMaint.CreateQuotesFilter>((PXGraph) this))?.Current).QuoteType == "P" ? (IEnumerable) UDFHelper.GetRequiredUDFFields((PXCache) GraphHelper.Caches<CROpportunity>((PXGraph) this), (object) null, typeof (PMQuoteMaint)) : (IEnumerable) UDFHelper.GetRequiredUDFFields((PXCache) GraphHelper.Caches<CROpportunity>((PXGraph) this), (object) null, typeof (QuoteMaint));
  }

  [InjectDependency]
  protected ILicenseLimitsService _licenseLimits { get; set; }

  public OpportunityMaint()
  {
    CRSetup current = ((PXSelectBase<CRSetup>) this.Setup).Current;
    if (string.IsNullOrEmpty(((PXSelectBase<CRSetup>) this.Setup).Current.OpportunityNumberingID))
      throw new PXSetPropertyException("Numbering ID is not configured in the CR setup.", new object[1]
      {
        (object) "Customer Management Preferences"
      });
    ((PXGraph) this).Views.Caches.Remove(typeof (CRQuote));
    ((PXAction) this.actionsFolder).MenuAutoOpen = true;
    ((PXGraph) this).Caches[typeof (PX.Objects.SO.SOOrder)].AllowUpdate = ((PXGraph) this).Caches[typeof (PX.Objects.SO.SOOrder)].AllowInsert = ((PXGraph) this).Caches[typeof (PX.Objects.SO.SOOrder)].AllowDelete = false;
    ((PXGraph) this).Caches[typeof (PX.Objects.AR.ARInvoice)].AllowUpdate = ((PXGraph) this).Caches[typeof (PX.Objects.AR.ARInvoice)].AllowInsert = ((PXGraph) this).Caches[typeof (PX.Objects.AR.ARInvoice)].AllowDelete = false;
    PXUIFieldAttribute.SetVisible<Contact.languageID>(((PXSelectBase) this.OpportunityCurrent).Cache, (object) null, PXDBLocalizableStringAttribute.HasMultipleLocales);
  }

  void IGraphWithInitialization.Initialize()
  {
    if (this._licenseLimits == null)
      return;
    ((PXGraph) this).OnBeforeCommit += this._licenseLimits.GetCheckerDelegate<CROpportunity>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (CROpportunityProducts), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[1]
      {
        (PXDataFieldValue) new PXDataFieldValue<CROpportunityProducts.quoteID>((object) (Guid?) ((PXSelectBase<CROpportunity>) ((OpportunityMaint) graph).Opportunity).Current?.QuoteNoteID)
      }))
    });
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CreateQuote(PXAdapter adapter)
  {
    OpportunityMaint graph = this;
    foreach (CROpportunity crOpportunity in adapter.Get<CROpportunity>())
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      OpportunityMaint.\u003C\u003Ec__DisplayClass58_0 cDisplayClass580 = new OpportunityMaint.\u003C\u003Ec__DisplayClass58_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass580.opportunity = crOpportunity;
      if (((PXSelectBase) graph.QuoteInfo).View.Answer == null)
      {
        ((PXSelectBase) graph.QuoteInfo).Cache.Clear();
        ((PXSelectBase) graph.QuoteInfo).Cache.Insert();
      }
      // ISSUE: reference to a compiler-generated field
      cDisplayClass580.result = !((PXGraph) graph).IsImport || ((PXGraph) graph).IsMobile ? ((PXSelectBase<OpportunityMaint.CreateQuotesFilter>) graph.QuoteInfo).AskExt() : (WebDialogResult) (object) 7;
      if (((PXGraph) graph).IsMobile)
      {
        if (!graph.CreateQuoteInfoUDF.TryValidate())
          throw new PXException("The project quote cannot be created in the mobile app because it requires at least one user-defined field to be specified. To create the project quote from this opportunity, use the web version.");
      }
      else
        graph.CreateQuoteInfoUDF.Validate();
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass580.result == 2)
      {
        // ISSUE: reference to a compiler-generated field
        yield return (object) cDisplayClass580.opportunity;
      }
      // ISSUE: reference to a compiler-generated field
      ((PXSelectBase<CROpportunity>) graph.Opportunity).Current = cDisplayClass580.opportunity;
      ((PXGraph) graph).Actions.PressSave();
      if (((PXSelectBase<OpportunityMaint.CreateQuotesFilter>) graph.QuoteInfo).Current.QuoteType == "P" && !PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>() && ((PXSelectBase<CROpportunity>) graph.OpportunityCurrent).Current.CuryID != (((PXGraph) graph).Accessinfo.BaseCuryID ?? ((PXSelectBase<Company>) new PXSetup<Company>((PXGraph) graph)).Current?.BaseCuryID))
        throw new PXException("Cannot create a project quote for the opportunity in a non-base currency.");
      // ISSUE: reference to a compiler-generated field
      cDisplayClass580.clone = graph.CloneGraphState<OpportunityMaint>();
      if (((PXSelectBase<OpportunityMaint.CreateQuotesFilter>) graph.QuoteInfo).Current.QuoteType == "D")
      {
        // ISSUE: method pointer
        PXLongOperation.StartOperation((PXGraph) graph, new PXToggleAsyncDelegate((object) cDisplayClass580, __methodptr(\u003CCreateQuote\u003Eb__0)));
      }
      else
      {
        // ISSUE: method pointer
        PXLongOperation.StartOperation((PXGraph) graph, new PXToggleAsyncDelegate((object) cDisplayClass580, __methodptr(\u003CCreateQuote\u003Eb__1)));
      }
      // ISSUE: reference to a compiler-generated field
      yield return (object) cDisplayClass580.opportunity;
      cDisplayClass580 = (OpportunityMaint.\u003C\u003Ec__DisplayClass58_0) null;
    }
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable ActionsFolder(PXAdapter adapter) => adapter.Get();

  [PXUIField(Visible = false)]
  [PXButton]
  public virtual IEnumerable UpdateClosingDate(PXAdapter adapter)
  {
    CROpportunity current = ((PXSelectBase<CROpportunity>) this.Opportunity).Current;
    if (current != null)
    {
      current.ClosingDate = ((PXGraph) this).Accessinfo.BusinessDate;
      ((PXSelectBase) this.Opportunity).Cache.Update((object) current);
      ((PXAction) this.Save).Press();
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual void ViewMainOnMap()
  {
    CRAddress aAddr = ((PXSelectBase<CRAddress>) this.Opportunity_Address).SelectSingle(Array.Empty<object>());
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
    OpportunityMaint opportunityMaint = this;
    foreach (CROpportunity opp in adapter.Get<CROpportunity>())
    {
      CRQuote current = ((PXSelectBase<CRQuote>) opportunityMaint.Quotes).Current;
      if ((current != null ? (!current.IsPrimary.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      {
        CRQuote crQuote = PXResultset<CRQuote>.op_Implicit(((PXSelectBase<CRQuote>) new PXSelect<CRQuote, Where<CRQuote.quoteID, Equal<Required<CRQuote.quoteID>>>>((PXGraph) opportunityMaint)).Select(new object[1]
        {
          (object) opp.DefQuoteID
        }));
        if (crQuote != null)
        {
          Guid? quoteId1 = crQuote.QuoteID;
          Guid? quoteId2 = ((PXSelectBase<CRQuote>) opportunityMaint.Quotes).Current.QuoteID;
          if ((quoteId1.HasValue == quoteId2.HasValue ? (quoteId1.HasValue ? (quoteId1.GetValueOrDefault() != quoteId2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0 && crQuote.Status == "C")
            throw new PXException("The quote cannot be marked as the primary quote of the {0} opportunity because the opportunity is linked to the closed {1} project quote.", new object[2]
            {
              (object) opp.OpportunityID,
              (object) crQuote.QuoteNbr
            });
        }
        Guid? quoteId = ((PXSelectBase<CRQuote>) opportunityMaint.Quotes).Current.QuoteID;
        string opportunityId = ((PXSelectBase<CROpportunity>) opportunityMaint.Opportunity).Current.OpportunityID;
        ((PXGraph) opportunityMaint).Persist();
        PXDatabase.Update<PX.Objects.CR.Standalone.CROpportunity>(new PXDataFieldParam[2]
        {
          (PXDataFieldParam) new PXDataFieldAssign<PX.Objects.CR.Standalone.CROpportunity.defQuoteID>((object) quoteId),
          (PXDataFieldParam) new PXDataFieldRestrict<PX.Objects.CR.Standalone.CROpportunity.opportunityID>((PXDbType) 22, new int?((int) byte.MaxValue), (object) opportunityId, (PXComp) 0)
        });
        ((PXAction) opportunityMaint.Cancel).Press();
        yield return (object) PXResultset<CROpportunity>.op_Implicit(((PXSelectBase<CROpportunity>) opportunityMaint.Opportunity).Search<CROpportunity.opportunityID>((object) opportunityId, Array.Empty<object>()));
      }
      yield return (object) opp;
    }
  }

  public virtual void CreateNewQuote(
    CROpportunity opportunity,
    OpportunityMaint.CreateQuotesFilter param,
    WebDialogResult result)
  {
    QuoteMaint instance = PXGraph.CreateInstance<QuoteMaint>();
    ((PXGraph) instance).SelectTimeStamp();
    this.CreateNewSalesQuote(instance, opportunity, param, result);
    if (result == 6)
    {
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 4);
    }
    else
    {
      if (result != 7)
        return;
      ((PXGraph) instance).Actions.PressSave();
    }
  }

  protected virtual void CreateNewSalesQuote(
    QuoteMaint graph,
    CROpportunity opportunity,
    OpportunityMaint.CreateQuotesFilter param,
    WebDialogResult result)
  {
    ((PXSelectBase<PX.Objects.CR.Standalone.CROpportunity>) graph.Opportunity).Current = ((PXSelectBase<PX.Objects.CR.Standalone.CROpportunity>) graph.Opportunity).SelectSingle(new object[1]
    {
      (object) opportunity.OpportunityID
    });
    CRQuote instance = (CRQuote) ((PXSelectBase) graph.Quote).Cache.CreateInstance();
    if (!string.IsNullOrWhiteSpace(param.QuoteNbr))
      instance.QuoteNbr = param.QuoteNbr;
    instance.OpportunityID = opportunity.OpportunityID;
    instance.BillAddressID = opportunity.BillAddressID;
    instance.BillContactID = opportunity.BillContactID;
    CRQuote crQuote1 = ((PXSelectBase<CRQuote>) graph.Quote).Insert(instance);
    crQuote1.LocationID = opportunity.LocationID;
    crQuote1.ContactID = opportunity.ContactID;
    crQuote1.BAccountID = opportunity.BAccountID;
    crQuote1.OpportunityAddressID = opportunity.OpportunityAddressID;
    crQuote1.OpportunityContactID = opportunity.OpportunityContactID;
    crQuote1.ShipAddressID = opportunity.ShipAddressID;
    crQuote1.ShipContactID = opportunity.ShipContactID;
    CRQuote crQuote2 = ((PXSelectBase<CRQuote>) graph.Quote).Update(crQuote1);
    foreach (string field in (List<string>) ((PXSelectBase) this.Opportunity).Cache.Fields)
    {
      if (!((PXSelectBase) graph.Quote).Cache.Keys.Contains(field) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.quoteID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.status))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.approved))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.rejected))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.opportunityAddressID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.opportunityContactID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.shipAddressID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.shipContactID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.billAddressID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.billContactID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.createdByID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.createdByScreenID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.createdDateTime))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.lastModifiedByID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.lastModifiedByScreenID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (CRQuote.lastModifiedDateTime))))
        ((PXSelectBase) graph.Quote).Cache.SetValue((object) crQuote2, field, ((PXSelectBase) this.Opportunity).Cache.GetValue((object) opportunity, field));
    }
    ((PXSelectBase) graph.Quote).Cache.SetDefaultExt<CRQuote.termsID>((object) crQuote2);
    crQuote2.DocumentDate = ((PXGraph) this).Accessinfo.BusinessDate;
    if (this.IsSingleQuote(crQuote2.OpportunityID))
    {
      crQuote2.QuoteID = crQuote2.NoteID = opportunity.QuoteNoteID;
    }
    else
    {
      object obj;
      ((PXSelectBase) graph.Quote).Cache.RaiseFieldDefaulting<CRQuote.noteID>((object) crQuote2, ref obj);
      crQuote2.QuoteID = crQuote2.NoteID = (Guid?) obj;
    }
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<CROpportunity.curyInfoID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    currencyInfo1.CuryInfoID = new long?();
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = GraphHelper.Caches<PX.Objects.CM.Extensions.CurrencyInfo>((PXGraph) graph).Insert(currencyInfo1);
    crQuote2.CuryInfoID = currencyInfo2.CuryInfoID;
    crQuote2.Subject = opportunity.Subject;
    crQuote2.DocumentDate = ((PXGraph) this).Accessinfo.BusinessDate;
    crQuote2.IsPrimary = param.MakeNewQuotePrimary;
    crQuote2.TermsID = opportunity.TermsID;
    bool? nullable1 = param.MakeNewQuotePrimary;
    if (nullable1.GetValueOrDefault())
      crQuote2.DefQuoteID = crQuote2.QuoteID;
    nullable1 = param.AddProductsFromOpportunity;
    if (nullable1.GetValueOrDefault() && !this.IsSingleQuote(crQuote2.OpportunityID))
    {
      ((PXSelectBase) this.Products).View.CloneView((PXGraph) graph, crQuote2.QuoteID, currencyInfo2);
    }
    else
    {
      ((PXSelectBase) graph.Quote).Cache.SetDefaultExt<CRQuote.curyDiscTot>((object) crQuote2);
      ((PXSelectBase) graph.Quote).Cache.SetDefaultExt<CRQuote.curyLineDocDiscountTotal>((object) crQuote2);
      ((PXSelectBase) graph.Quote).Cache.SetDefaultExt<CRQuote.curyProductsAmount>((object) crQuote2);
    }
    string note = PXNoteAttribute.GetNote(((PXSelectBase) this.Opportunity).Cache, (object) opportunity);
    Guid[] fileNotes = PXNoteAttribute.GetFileNotes(((PXSelectBase) this.Opportunity).Cache, (object) opportunity);
    PXNoteAttribute.SetNote(((PXSelectBase) graph.Quote).Cache, (object) crQuote2, note);
    PXNoteAttribute.SetFileNotes(((PXSelectBase) graph.Quote).Cache, (object) crQuote2, fileNotes);
    nullable1 = param.AddProductsFromOpportunity;
    if (nullable1.GetValueOrDefault() && !this.IsSingleQuote(crQuote2.OpportunityID))
    {
      ((PXGraph) this).GetExtension<OpportunityMaint.Discount>();
      ((PXSelectBase) this.TaxLines).View.CloneView((PXGraph) graph, crQuote2.QuoteID, currencyInfo2);
      ((PXSelectBase) this.Taxes).View.CloneView((PXGraph) graph, crQuote2.QuoteID, currencyInfo2, "RecordID");
      ((PXGraph) this).Views["DiscountDetails"].CloneView((PXGraph) graph, crQuote2.QuoteID, currencyInfo2);
    }
    nullable1 = opportunity.AllowOverrideContactAddress;
    if (nullable1.GetValueOrDefault())
    {
      ((PXSelectBase) this.Opportunity_Contact).View.CloneView((PXGraph) graph, crQuote2.QuoteID, currencyInfo2, "ContactID");
      crQuote2.OpportunityContactID = ((PXSelectBase<CRContact>) graph.Quote_Contact).Current.ContactID;
      ((PXSelectBase) this.Opportunity_Address).View.CloneView((PXGraph) graph, crQuote2.QuoteID, currencyInfo2, "AddressID");
      crQuote2.OpportunityAddressID = ((PXSelectBase<CRAddress>) graph.Quote_Address).Current.AddressID;
    }
    CRContact crContact1 = (CRContact) ((PXSelectBase<CRShippingContact>) this.Shipping_Contact).SelectSingle(Array.Empty<object>());
    if (crContact1 != null)
    {
      nullable1 = crContact1.OverrideContact;
      if (nullable1.HasValue && nullable1.GetValueOrDefault())
      {
        ((PXSelectBase) this.Shipping_Contact).View.CloneView((PXGraph) graph, crQuote2.QuoteID, currencyInfo2, "ContactID");
        crQuote2.ShipContactID = crContact1.ContactID;
      }
    }
    CRAddress crAddress1 = (CRAddress) ((PXSelectBase<CRShippingAddress>) this.Shipping_Address).SelectSingle(Array.Empty<object>());
    if (crAddress1 != null)
    {
      nullable1 = crAddress1.OverrideAddress;
      if (nullable1.HasValue && nullable1.GetValueOrDefault())
      {
        ((PXSelectBase) this.Shipping_Address).View.CloneView((PXGraph) graph, crQuote2.QuoteID, currencyInfo2, "AddressID");
        crQuote2.ShipAddressID = crAddress1.AddressID;
      }
    }
    CRContact crContact2 = (CRContact) ((PXSelectBase<CRBillingContact>) this.Billing_Contact).SelectSingle(Array.Empty<object>());
    if (crContact2 != null)
    {
      nullable1 = crContact2.OverrideContact;
      if (nullable1.HasValue && nullable1.GetValueOrDefault())
      {
        ((PXSelectBase) this.Billing_Contact).View.CloneView((PXGraph) graph, crQuote2.QuoteID, currencyInfo2, "ContactID");
        crQuote2.BillContactID = crContact2.ContactID;
      }
    }
    CRAddress crAddress2 = (CRAddress) ((PXSelectBase<CRBillingAddress>) this.Billing_Address).SelectSingle(Array.Empty<object>());
    if (crAddress2 != null)
    {
      nullable1 = crAddress2.OverrideAddress;
      if (nullable1.HasValue && nullable1.GetValueOrDefault())
      {
        ((PXSelectBase) this.Billing_Address).View.CloneView((PXGraph) graph, crQuote2.QuoteID, currencyInfo2, "AddressID");
        crQuote2.BillAddressID = crAddress2.AddressID;
      }
    }
    UDFHelper.CopyAttributes((PXCache) GraphHelper.Caches<CROpportunity>((PXGraph) this), (object) opportunity, ((PXSelectBase) graph.Quote).Cache, (object) crQuote2, (string) null);
    UDFHelper.FillfromPopupUDF((PXCache) GraphHelper.Caches<CRQuote>((PXGraph) this), ((PXSelectBase) this.CreateQuoteInfoUDF).Cache, ((object) graph).GetType(), (object) crQuote2);
    ((PXSelectBase<CRQuote>) graph.Quote).Update(crQuote2);
    QuoteMaint.Discount extension = ((PXGraph) graph).GetExtension<QuoteMaint.Discount>();
    RecalcDiscountsParamFilter current1 = ((PXSelectBase<RecalcDiscountsParamFilter>) extension.recalcdiscountsfilter).Current;
    nullable1 = param.OverrideManualDocGroupDiscounts;
    bool? nullable2 = new bool?(nullable1.GetValueOrDefault());
    current1.OverrideManualDocGroupDiscounts = nullable2;
    RecalcDiscountsParamFilter current2 = ((PXSelectBase<RecalcDiscountsParamFilter>) extension.recalcdiscountsfilter).Current;
    nullable1 = param.OverrideManualDiscounts;
    bool? nullable3 = new bool?(nullable1.GetValueOrDefault());
    current2.OverrideManualDiscounts = nullable3;
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
    extension.RefreshTotalsAndFreeItems(((PXSelectBase) extension.Details).Cache);
  }

  public void CreateNewProjectQuote(
    CROpportunity opportunity,
    OpportunityMaint.CreateQuotesFilter param,
    WebDialogResult result)
  {
    PMQuoteMaint instance = PXGraph.CreateInstance<PMQuoteMaint>();
    ((PXGraph) instance).SelectTimeStamp();
    this.CreateNewProjectQuote(instance, opportunity, param, result);
    if (result == 6)
    {
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 4);
    }
    else
    {
      if (result != 7)
        return;
      ((PXGraph) instance).Actions.PressSave();
    }
  }

  protected virtual void CreateNewProjectQuote(
    PMQuoteMaint graph,
    CROpportunity opportunity,
    OpportunityMaint.CreateQuotesFilter param,
    WebDialogResult result)
  {
    ((PXSelectBase<PX.Objects.CR.Standalone.CROpportunity>) graph.Opportunity).Current = ((PXSelectBase<PX.Objects.CR.Standalone.CROpportunity>) graph.Opportunity).SelectSingle(new object[1]
    {
      (object) opportunity.OpportunityID
    });
    PMQuote instance = (PMQuote) ((PXSelectBase) graph.Quote).Cache.CreateInstance();
    if (!string.IsNullOrWhiteSpace(param.QuoteNbr))
      instance.QuoteNbr = param.QuoteNbr;
    if (this.IsSingleQuote(opportunity.OpportunityID))
    {
      instance.QuoteID = instance.NoteID = opportunity.QuoteNoteID;
    }
    else
    {
      object obj;
      ((PXSelectBase) graph.Quote).Cache.RaiseFieldDefaulting<PMQuote.noteID>((object) instance, ref obj);
      instance.QuoteID = instance.NoteID = (Guid?) obj;
    }
    PMQuote pmQuote1 = ((PXSelectBase<PMQuote>) graph.Quote).Insert(instance);
    pmQuote1.LocationID = opportunity.LocationID;
    pmQuote1.BAccountID = opportunity.BAccountID;
    pmQuote1.OpportunityID = opportunity.OpportunityID;
    pmQuote1.OpportunityAddressID = opportunity.OpportunityAddressID;
    pmQuote1.OpportunityContactID = opportunity.OpportunityContactID;
    pmQuote1.ShipAddressID = opportunity.ShipAddressID;
    pmQuote1.ShipContactID = opportunity.ShipContactID;
    PMQuote pmQuote2 = ((PXSelectBase<PMQuote>) graph.Quote).Update(pmQuote1);
    pmQuote2.ContactID = opportunity.ContactID;
    PMQuote pmQuote3 = ((PXSelectBase<PMQuote>) graph.Quote).Update(pmQuote2);
    foreach (string field in (List<string>) ((PXSelectBase) this.Opportunity).Cache.Fields)
    {
      if (!((PXSelectBase) graph.Quote).Cache.Keys.Contains(field) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.quoteProjectID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.manualTotalEntry))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.curyAmount))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.curyDiscTot))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.curyExtPriceTotal))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.curyProductsAmount))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.curyTaxTotal))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.curyLineTotal))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.curyVatExemptTotal))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.curyVatTaxableTotal))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.status))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.approved))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.rejected))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.noteID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.quoteID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.opportunityAddressID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.opportunityContactID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.shipAddressID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.shipContactID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.createdByID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.createdByScreenID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.createdDateTime))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.lastModifiedByID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.lastModifiedByScreenID))) && !(field == ((PXSelectBase) graph.Quote).Cache.GetField(typeof (PMQuote.lastModifiedDateTime))))
        ((PXSelectBase) graph.Quote).Cache.SetValue((object) pmQuote3, field, ((PXSelectBase) this.Opportunity).Cache.GetValue((object) opportunity, field));
    }
    ((PXSelectBase) graph.Quote).Cache.SetDefaultExt<PMQuote.termsID>((object) pmQuote3);
    pmQuote3.DocumentDate = ((PXGraph) this).Accessinfo.BusinessDate;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<CROpportunity.curyInfoID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    currencyInfo1.CuryInfoID = new long?();
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = GraphHelper.Caches<PX.Objects.CM.Extensions.CurrencyInfo>((PXGraph) graph).Insert(currencyInfo1);
    pmQuote3.CuryInfoID = currencyInfo2.CuryInfoID;
    pmQuote3.Subject = opportunity.Subject;
    pmQuote3.DocumentDate = ((PXGraph) this).Accessinfo.BusinessDate;
    pmQuote3.IsPrimary = param.MakeNewQuotePrimary;
    pmQuote3.TermsID = opportunity.TermsID;
    bool? nullable = param.MakeNewQuotePrimary;
    if (nullable.GetValueOrDefault())
      pmQuote3.DefQuoteID = pmQuote3.QuoteID;
    ((PXSelectBase) graph.Quote).Cache.SetDefaultExt<CRQuote.curyDiscTot>((object) pmQuote3);
    ((PXSelectBase) graph.Quote).Cache.SetDefaultExt<CRQuote.curyLineDocDiscountTotal>((object) pmQuote3);
    ((PXSelectBase) graph.Quote).Cache.SetDefaultExt<CRQuote.curyProductsAmount>((object) pmQuote3);
    string note = PXNoteAttribute.GetNote(((PXSelectBase) this.Opportunity).Cache, (object) opportunity);
    Guid[] fileNotes = PXNoteAttribute.GetFileNotes(((PXSelectBase) this.Opportunity).Cache, (object) opportunity);
    PXNoteAttribute.SetNote(((PXSelectBase) graph.Quote).Cache, (object) pmQuote3, note);
    PXNoteAttribute.SetFileNotes(((PXSelectBase) graph.Quote).Cache, (object) pmQuote3, fileNotes);
    nullable = opportunity.AllowOverrideContactAddress;
    if (nullable.GetValueOrDefault())
    {
      ((PXSelectBase) this.Opportunity_Contact).View.CloneView((PXGraph) graph, pmQuote3.QuoteID, currencyInfo2, "ContactID");
      pmQuote3.OpportunityContactID = ((PXSelectBase<CRContact>) graph.Quote_Contact).Current.ContactID;
      ((PXSelectBase) this.Opportunity_Address).View.CloneView((PXGraph) graph, pmQuote3.QuoteID, currencyInfo2, "AddressID");
      pmQuote3.OpportunityAddressID = ((PXSelectBase<CRAddress>) graph.Quote_Address).Current.AddressID;
    }
    CRContact crContact = (CRContact) ((PXSelectBase<CRShippingContact>) this.Shipping_Contact).SelectSingle(Array.Empty<object>());
    if (crContact != null)
    {
      nullable = crContact.OverrideContact;
      if (nullable.HasValue && nullable.GetValueOrDefault())
      {
        ((PXSelectBase) this.Shipping_Contact).View.CloneView((PXGraph) graph, pmQuote3.QuoteID, currencyInfo2, "ContactID");
        pmQuote3.ShipContactID = crContact.ContactID;
      }
    }
    CRAddress crAddress = (CRAddress) ((PXSelectBase<CRShippingAddress>) this.Shipping_Address).SelectSingle(Array.Empty<object>());
    if (crAddress != null)
    {
      nullable = crAddress.OverrideAddress;
      if (nullable.HasValue && nullable.GetValueOrDefault())
      {
        ((PXSelectBase) this.Shipping_Address).View.CloneView((PXGraph) graph, pmQuote3.QuoteID, currencyInfo2, "AddressID");
        pmQuote3.ShipAddressID = crAddress.AddressID;
      }
    }
    UDFHelper.CopyAttributes((PXCache) GraphHelper.Caches<CROpportunity>((PXGraph) this), (object) opportunity, ((PXSelectBase) graph.Quote).Cache, (object) pmQuote3, (string) null);
    UDFHelper.FillfromPopupUDF((PXCache) GraphHelper.Caches<PMQuote>((PXGraph) this), ((PXSelectBase) this.CreateQuoteInfoUDF).Cache, ((object) graph).GetType(), (object) pmQuote3);
    ((PXSelectBase<PMQuote>) graph.Quote).Update(pmQuote3);
    foreach (PXResult<CROpportunityProducts> pxResult in ((PXSelectBase<CROpportunityProducts>) graph.Products).Select(Array.Empty<object>()))
      ((PXSelectBase<CROpportunityProducts>) graph.Products).Delete(PXResult<CROpportunityProducts>.op_Implicit(pxResult));
    foreach (PXResult<CROpportunityTax> pxResult in ((PXSelectBase<CROpportunityTax>) graph.TaxLines).Select(Array.Empty<object>()))
      ((PXSelectBase<CROpportunityTax>) graph.TaxLines).Delete(PXResult<CROpportunityTax>.op_Implicit(pxResult));
    foreach (PXResult<CROpportunityDiscountDetail> pxResult in ((PXSelectBase<CROpportunityDiscountDetail>) graph._DiscountDetails).Select(Array.Empty<object>()))
      ((PXSelectBase<CROpportunityDiscountDetail>) graph._DiscountDetails).Delete(PXResult<CROpportunityDiscountDetail>.op_Implicit(pxResult));
    ((PXSelectBase) graph.Quote).Cache.SetDefaultExt<PMQuote.curyAmount>((object) pmQuote3);
    ((PXSelectBase) graph.Quote).Cache.SetDefaultExt<PMQuote.curyCostTotal>((object) pmQuote3);
    ((PXSelectBase) graph.Quote).Cache.SetDefaultExt<PMQuote.curyTaxTotal>((object) pmQuote3);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CopyQuote(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    OpportunityMaint.\u003C\u003Ec__DisplayClass76_0 cDisplayClass760 = new OpportunityMaint.\u003C\u003Ec__DisplayClass76_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass760.currentQuote = ((PXSelectBase) this.Quotes).Cache.Current as CRQuote;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass760.currentQuote == null)
      return adapter.Get();
    foreach (CROpportunity crOpportunity in adapter.Get<CROpportunity>())
    {
      if (((PXSelectBase) this.CopyQuoteInfo).View.Answer == null)
      {
        ((PXSelectBase) this.CopyQuoteInfo).Cache.Clear();
        if (((PXSelectBase) this.CopyQuoteInfo).Cache.Insert() is OpportunityMaint.CopyQuoteFilter copyQuoteFilter)
        {
          // ISSUE: reference to a compiler-generated field
          copyQuoteFilter.Description = cDisplayClass760.currentQuote.Subject + " - copy";
        }
      }
      if (((PXSelectBase<OpportunityMaint.CopyQuoteFilter>) this.CopyQuoteInfo).AskExt() != 6)
        return adapter.Get();
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      OpportunityMaint.\u003C\u003Ec__DisplayClass76_1 cDisplayClass761 = new OpportunityMaint.\u003C\u003Ec__DisplayClass76_1();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass761.CS\u0024\u003C\u003E8__locals1 = cDisplayClass760;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      switch (cDisplayClass761.CS\u0024\u003C\u003E8__locals1.currentQuote.QuoteType)
      {
        case "D":
          // ISSUE: reference to a compiler-generated field
          cDisplayClass761.quoteFilterData = new QuoteMaint.CopyQuoteFilter()
          {
            OpportunityID = ((PXSelectBase<OpportunityMaint.CopyQuoteFilter>) this.CopyQuoteInfo).Current.OpportunityID,
            Description = ((PXSelectBase<OpportunityMaint.CopyQuoteFilter>) this.CopyQuoteInfo).Current.Description,
            RecalculatePrices = ((PXSelectBase<OpportunityMaint.CopyQuoteFilter>) this.CopyQuoteInfo).Current.RecalculatePrices,
            RecalculateDiscounts = ((PXSelectBase<OpportunityMaint.CopyQuoteFilter>) this.CopyQuoteInfo).Current.RecalculateDiscounts,
            OverrideManualPrices = ((PXSelectBase<OpportunityMaint.CopyQuoteFilter>) this.CopyQuoteInfo).Current.OverrideManualPrices,
            OverrideManualDiscounts = ((PXSelectBase<OpportunityMaint.CopyQuoteFilter>) this.CopyQuoteInfo).Current.OverrideManualDiscounts,
            OverrideManualDocGroupDiscounts = ((PXSelectBase<OpportunityMaint.CopyQuoteFilter>) this.CopyQuoteInfo).Current.OverrideManualDocGroupDiscounts
          };
          // ISSUE: method pointer
          PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass761, __methodptr(\u003CCopyQuote\u003Eb__0)));
          continue;
        case "P":
          // ISSUE: reference to a compiler-generated field
          cDisplayClass761.pmQuoteFilterData = new PMQuoteMaint.CopyQuoteFilter()
          {
            Description = ((PXSelectBase<OpportunityMaint.CopyQuoteFilter>) this.CopyQuoteInfo).Current.Description,
            RecalculatePrices = ((PXSelectBase<OpportunityMaint.CopyQuoteFilter>) this.CopyQuoteInfo).Current.RecalculatePrices,
            RecalculateDiscounts = ((PXSelectBase<OpportunityMaint.CopyQuoteFilter>) this.CopyQuoteInfo).Current.RecalculateDiscounts,
            OverrideManualPrices = ((PXSelectBase<OpportunityMaint.CopyQuoteFilter>) this.CopyQuoteInfo).Current.OverrideManualPrices,
            OverrideManualDiscounts = ((PXSelectBase<OpportunityMaint.CopyQuoteFilter>) this.CopyQuoteInfo).Current.OverrideManualDiscounts,
            OverrideManualDocGroupDiscounts = ((PXSelectBase<OpportunityMaint.CopyQuoteFilter>) this.CopyQuoteInfo).Current.OverrideManualDocGroupDiscounts
          };
          // ISSUE: reference to a compiler-generated field
          cDisplayClass761.pmGraph = PXGraph.CreateInstance<PMQuoteMaint>();
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          cDisplayClass761.pmQuote = ((PXSelectBase<PMQuote>) cDisplayClass761.pmGraph.Quote).Search<PMQuote.quoteID>((object) cDisplayClass761.CS\u0024\u003C\u003E8__locals1.currentQuote.QuoteID, Array.Empty<object>());
          // ISSUE: method pointer
          PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass761, __methodptr(\u003CCopyQuote\u003Eb__1)));
          continue;
        default:
          throw new PXException("This type of quote is not supported.");
      }
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Quote", Visible = false)]
  [PXButton]
  public virtual IEnumerable viewQuote(PXAdapter adapter)
  {
    if (((PXSelectBase<CRQuote>) this.Quotes).Current != null)
    {
      CRQuote current = ((PXSelectBase<CRQuote>) this.Quotes).Current;
      switch (current.QuoteType)
      {
        case "D":
          QuoteMaint instance1 = PXGraph.CreateInstance<QuoteMaint>();
          ((PXSelectBase<CRQuote>) instance1.Quote).Current = PXResultset<CRQuote>.op_Implicit(((PXSelectBase<CRQuote>) instance1.Quote).Search<CRQuote.quoteNbr>((object) current.QuoteNbr, new object[1]
          {
            (object) current.OpportunityID
          }));
          if (((PXSelectBase<CRQuote>) instance1.Quote).Current != null)
          {
            PXRedirectHelper.TryRedirect((PXGraph) instance1, (PXRedirectHelper.WindowMode) 4);
            break;
          }
          break;
        case "P":
          PMQuoteMaint instance2 = PXGraph.CreateInstance<PMQuoteMaint>();
          ((PXSelectBase<PMQuote>) instance2.Quote).Current = PXResultset<PMQuote>.op_Implicit(((PXSelectBase<PMQuote>) instance2.Quote).Search<PMQuote.quoteNbr>((object) current.QuoteNbr, Array.Empty<object>()));
          if (((PXSelectBase<PMQuote>) instance2.Quote).Current != null)
          {
            PXRedirectHelper.TryRedirect((PXGraph) instance2, (PXRedirectHelper.WindowMode) 4);
            break;
          }
          break;
        default:
          throw new PXException("This type of quote is not supported.");
      }
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "View Quote", Visible = false)]
  [PXButton]
  public virtual IEnumerable viewProject(PXAdapter adapter)
  {
    int? quoteProjectId = (int?) ((PXSelectBase<CRQuote>) this.Quotes).Current?.QuoteProjectID;
    if (quoteProjectId.HasValue)
    {
      ((PXGraph) this).Persist();
      ProjectAccountingService.NavigateToProjectScreen(quoteProjectId);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
  {
    OpportunityMaint aGraph = this;
    foreach (CROpportunity crOpportunity in adapter.Get<CROpportunity>())
    {
      if (crOpportunity != null)
      {
        CRAddress aAddress1 = PXResultset<CRAddress>.op_Implicit(((PXSelectBase<CRAddress>) aGraph.Opportunity_Address).Select(Array.Empty<object>()));
        bool? nullable;
        if (aAddress1 != null && crOpportunity.AllowOverrideContactAddress.GetValueOrDefault())
        {
          nullable = aAddress1.IsValidated;
          bool flag = false;
          if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
            PXAddressValidator.Validate<CRAddress>((PXGraph) aGraph, aAddress1, true, true);
        }
        CRShippingAddress aAddress2 = PXResultset<CRShippingAddress>.op_Implicit(((PXSelectBase<CRShippingAddress>) aGraph.Shipping_Address).Select(Array.Empty<object>()));
        if (aAddress2 != null && !crOpportunity.BAccountID.HasValue && !crOpportunity.ContactID.HasValue)
        {
          nullable = aAddress2.IsDefaultAddress;
          if (nullable.HasValue && nullable.GetValueOrDefault())
          {
            aAddress2.IsValidated = aAddress1.IsValidated;
            GraphHelper.MarkUpdated(((PXSelectBase) aGraph.Shipping_Address).Cache, (object) aAddress2);
          }
        }
        if (aAddress2 != null)
        {
          nullable = aAddress2.IsDefaultAddress;
          if (nullable.HasValue && !nullable.GetValueOrDefault())
          {
            nullable = aAddress2.IsValidated;
            bool flag = false;
            if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
              PXAddressValidator.Validate<CRShippingAddress>((PXGraph) aGraph, aAddress2, true, true);
          }
        }
        CRBillingAddress aAddress3 = PXResultset<CRBillingAddress>.op_Implicit(((PXSelectBase<CRBillingAddress>) aGraph.Billing_Address).Select(Array.Empty<object>()));
        if (aAddress3 != null && !crOpportunity.BAccountID.HasValue && !crOpportunity.ContactID.HasValue)
        {
          nullable = aAddress3.IsDefaultAddress;
          if (nullable.HasValue && nullable.GetValueOrDefault())
          {
            aAddress3.IsValidated = aAddress1.IsValidated;
            GraphHelper.MarkUpdated(((PXSelectBase) aGraph.Billing_Address).Cache, (object) aAddress3);
          }
        }
        if (aAddress3 != null)
        {
          nullable = aAddress3.IsDefaultAddress;
          if (nullable.HasValue && !nullable.GetValueOrDefault())
          {
            nullable = aAddress3.IsValidated;
            bool flag = false;
            if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
              PXAddressValidator.Validate<CRBillingAddress>((PXGraph) aGraph, aAddress3, true, true);
          }
        }
      }
      yield return (object) crOpportunity;
    }
  }

  [PXButton]
  [PXUIField]
  protected virtual IEnumerable open(PXAdapter adapter)
  {
    return (IEnumerable) adapter.Get<CROpportunity>();
  }

  [PXButton]
  [PXUIField]
  protected virtual IEnumerable openFromNew(PXAdapter adapter)
  {
    return (IEnumerable) adapter.Get<CROpportunity>();
  }

  [PXButton]
  [PXUIField]
  protected virtual IEnumerable closeAsWon(PXAdapter adapter)
  {
    OpportunityMaint opportunityMaint = this;
    foreach (CROpportunity crOpportunity in adapter.Get<CROpportunity>())
    {
      if (crOpportunity != null)
      {
        ((SelectedEntityEvent<CROpportunity>) PXEntityEventBase<CROpportunity>.Container<CROpportunity.Events>.Select((Expression<Func<CROpportunity.Events, PXEntityEvent<CROpportunity.Events>>>) (ev => ev.OpportunityWon))).FireOn((PXGraph) opportunityMaint, crOpportunity);
        ((SelectedEntityEvent<CROpportunity>) PXEntityEventBase<CROpportunity>.Container<CROpportunity.Events>.Select((Expression<Func<CROpportunity.Events, PXEntityEvent<CROpportunity.Events>>>) (ev => ev.OpportunityClosed))).FireOn((PXGraph) opportunityMaint, crOpportunity);
      }
      yield return (object) crOpportunity;
    }
  }

  [PXButton]
  [PXUIField]
  protected virtual IEnumerable closeAsLost(PXAdapter adapter)
  {
    OpportunityMaint opportunityMaint = this;
    foreach (CROpportunity crOpportunity in adapter.Get<CROpportunity>())
    {
      if (crOpportunity != null)
      {
        ((SelectedEntityEvent<CROpportunity>) PXEntityEventBase<CROpportunity>.Container<CROpportunity.Events>.Select((Expression<Func<CROpportunity.Events, PXEntityEvent<CROpportunity.Events>>>) (ev => ev.OpportunityLost))).FireOn((PXGraph) opportunityMaint, crOpportunity);
        ((SelectedEntityEvent<CROpportunity>) PXEntityEventBase<CROpportunity>.Container<CROpportunity.Events>.Select((Expression<Func<CROpportunity.Events, PXEntityEvent<CROpportunity.Events>>>) (ev => ev.OpportunityClosed))).FireOn((PXGraph) opportunityMaint, crOpportunity);
      }
      yield return (object) crOpportunity;
    }
  }

  [PopupMessage]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CROpportunity.bAccountID> e)
  {
  }

  [PXDefault(typeof (Coalesce<Search<PX.Objects.AR.Customer.curyID, Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<CROpportunity.bAccountID>>>>, Search<PX.Objects.GL.Branch.baseCuryID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<CROpportunity.branchID>>>>>))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<CROpportunity.curyID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (Search<Location.taxRegistrationID, Where<Location.bAccountID, Equal<Current<CROpportunity.bAccountID>>, And<Location.locationID, Equal<Current<CROpportunity.locationID>>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunity.taxRegistrationID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (Search<Location.cAvalaraExemptionNumber, Where<Location.bAccountID, Equal<Current<CROpportunity.bAccountID>>, And<Location.locationID, Equal<Current<CROpportunity.locationID>>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunity.externalTaxExemptionNumber> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault("0", typeof (Search<Location.cAvalaraCustomerUsageType, Where<Location.bAccountID, Equal<Current<CROpportunity.bAccountID>>, And<Location.locationID, Equal<Current<CROpportunity.locationID>>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunity.avalaraCustomerUsageType> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Default<CROpportunity.branchID>))]
  [PXFormula(typeof (Default<CROpportunity.locationID>))]
  protected virtual void _(PX.Data.Events.CacheAttached<CROpportunity.taxZoneID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Sales Territory")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunity.salesTerritoryID> e)
  {
  }

  protected virtual void CROpportunity_TaxZoneID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    CROpportunity row = e.Row as CROpportunity;
    e.NewValue = (object) this.GetDefaultTaxZone(row);
  }

  public virtual string GetDefaultTaxZone(CROpportunity row)
  {
    string defaultTaxZone = (string) null;
    if (row == null)
      return defaultTaxZone;
    Location location1 = PXResultset<Location>.op_Implicit(PXSelectBase<Location, PXSelect<Location, Where<Location.bAccountID, Equal<Required<CROpportunity.bAccountID>>, And<Location.locationID, Equal<Required<CROpportunity.locationID>>>>>.Config>.Select((PXGraph) this, new object[2]
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
      Location location2 = PXResultset<Location>.op_Implicit(PXSelectBase<Location, PXSelectJoin<Location, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<Current<CROpportunity.branchID>>>, InnerJoin<BAccount, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccount.bAccountID>>>>, Where<Location.locationID, Equal<BAccount.defLocationID>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      defaultTaxZone = location2 == null || location2.VTaxZoneID == null ? row.TaxZoneID : location2.VTaxZoneID;
    }
    return defaultTaxZone;
  }

  protected virtual void CROpportunity_ClassID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) ((PXSelectBase<CRSetup>) this.Setup).Current.DefaultOpportunityClassID;
  }

  protected virtual void CROpportunity_LocationID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is CROpportunity row) || !row.BAccountID.HasValue)
      return;
    BAccount baccount = PXResultset<BAccount>.op_Implicit(PXSelectBase<BAccount, PXSelect<BAccount, Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.BAccountID
    }));
    if (baccount == null)
      return;
    e.NewValue = (object) baccount.DefLocationID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CROpportunity_DefQuoteID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    CROpportunity row = (CROpportunity) e.Row;
    if (row == null)
      return;
    e.NewValue = (object) row.QuoteNoteID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CROpportunity_ProjectID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    CROpportunity row = (CROpportunity) e.Row;
    if (row == null || row.CampaignSourceID == null)
      return;
    CRCampaign crCampaign = (CRCampaign) PXSelectorAttribute.Select<CROpportunity.campaignSourceID>(((PXSelectBase) this.Opportunity).Cache, (object) ((PXSelectBase<CROpportunity>) this.Opportunity).Current);
    object obj = PXSelectorAttribute.Select<CROpportunity.projectID>(((PXSelectBase) this.Opportunity).Cache, (object) ((PXSelectBase<CROpportunity>) this.Opportunity).Current);
    if (crCampaign == null || !crCampaign.ProjectID.HasValue || obj == null)
      return;
    e.NewValue = (object) crCampaign.ProjectID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CROpportunity_ProjectID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.UpdateProductsTasks();
  }

  protected virtual void CROpportunity_CampaignSourceID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    CROpportunity row = (CROpportunity) e.Row;
    if (row == null || !row.ProjectID.HasValue)
      return;
    PXResultset<CRCampaign> pxResultset = PXSelectBase<CRCampaign, PXSelect<CRCampaign, Where<CRCampaign.projectID, Equal<Required<CROpportunity.projectID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 2, new object[1]
    {
      (object) row.ProjectID
    });
    if (pxResultset.Count != 1)
      return;
    CRCampaign crCampaign = PXResult<CRCampaign>.op_Implicit(pxResultset[0]);
    e.NewValue = (object) crCampaign.CampaignID;
  }

  protected virtual void CROpportunity_CampaignSourceID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.UpdateProductsTasks();
  }

  protected virtual void UpdateProductsTasks()
  {
    if (((PXSelectBase<CROpportunity>) this.Opportunity).Current?.CampaignSourceID == null)
      return;
    CRCampaign crCampaign = (CRCampaign) PXSelectorAttribute.Select<CROpportunity.campaignSourceID>(((PXSelectBase) this.Opportunity).Cache, (object) ((PXSelectBase<CROpportunity>) this.Opportunity).Current);
    if (crCampaign == null)
      return;
    int? projectId1 = crCampaign.ProjectID;
    int? projectId2 = ((PXSelectBase<CROpportunity>) this.Opportunity).Current.ProjectID;
    if (!(projectId1.GetValueOrDefault() == projectId2.GetValueOrDefault() & projectId1.HasValue == projectId2.HasValue))
      return;
    foreach (PXResult<CROpportunityProducts> pxResult in ((PXSelectBase<CROpportunityProducts>) this.Products).Select(Array.Empty<object>()))
    {
      ((PXSelectBase) this.Products).Cache.SetDefaultExt<CROpportunityProducts.projectID>((object) pxResult);
      ((PXSelectBase) this.Products).Cache.SetDefaultExt<CROpportunityProducts.taskID>((object) pxResult);
    }
  }

  protected virtual void UpdateProductsCostCodes(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    CROpportunity current = ((PXSelectBase<CROpportunity>) this.Opportunity).Current;
    if (current == null)
      return;
    foreach (PXResult<CROpportunityProducts> pxResult in ((PXSelectBase<CROpportunityProducts>) this.Products).Select(Array.Empty<object>()))
    {
      CROpportunityProducts opportunityProducts = PXResult<CROpportunityProducts>.op_Implicit(pxResult);
      int? projectId = opportunityProducts.ProjectID;
      int? nullable = current.ProjectID;
      bool flag = !(projectId.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId.HasValue == nullable.HasValue);
      try
      {
        PMProject project;
        if (PXAccess.FeatureInstalled<FeaturesSet.costCodes>() && ProjectDefaultAttribute.IsProject((PXGraph) this, current.ProjectID, out project) && project.BudgetLevel == "T")
        {
          int defaultCostCode = CostCodeAttribute.GetDefaultCostCode();
          int num1;
          if (!flag)
          {
            nullable = opportunityProducts.CostCodeID;
            int num2 = defaultCostCode;
            num1 = !(nullable.GetValueOrDefault() == num2 & nullable.HasValue) ? 1 : 0;
          }
          else
            num1 = 1;
          flag = num1 != 0;
          opportunityProducts.CostCodeID = new int?(defaultCostCode);
        }
        opportunityProducts.ProjectID = current.ProjectID;
        if (flag)
          ((PXSelectBase<CROpportunityProducts>) this.Products).Update(opportunityProducts);
      }
      catch (PXException ex)
      {
        PXFieldState stateExt = (PXFieldState) sender.GetStateExt<CROpportunity.projectID>((object) current);
        ((PXSelectBase) this.Products).Cache.RaiseExceptionHandling<CROpportunityProducts.projectID>((object) opportunityProducts, stateExt.Value, (Exception) ex);
      }
    }
  }

  protected virtual void CROpportunity_CloseDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is CROpportunity row) || ((PXSelectBase<CRQuote>) this.PrimaryQuoteQuery).SelectSingle(Array.Empty<object>()) != null)
      return;
    ((PXSelectBase) this.Opportunity).Cache.SetValueExt<CROpportunity.documentDate>((object) row, (object) row.CloseDate);
  }

  protected virtual void CROpportunity_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXNoteAttribute.SetTextFilesActivitiesRequired<CROpportunityProducts.noteID>(((PXSelectBase) this.Products).Cache, (object) null, true, true, false);
    if (!(e.Row is CROpportunity row))
      return;
    CRQuote crQuote = ((PXSelectBase<CRQuote>) this.PrimaryQuoteQuery).SelectSingle(Array.Empty<object>());
    if (row.PrimaryQuoteType == null)
      row.PrimaryQuoteType = crQuote?.QuoteType ?? "D";
    if (crQuote != null && crQuote.IsDisabled.GetValueOrDefault())
      sender.RaiseExceptionHandling<CROpportunity.opportunityID>((object) row, (object) row.OpportunityID, (Exception) new PXSetPropertyException("Some of the opportunity settings cannot be modified because the primary quote status is other than Draft.", (PXErrorLevel) 2));
    bool isSalesQuote = row.PrimaryQuoteType == "D";
    System.Type[] typeArray = new System.Type[3]
    {
      typeof (CROpportunityDiscountDetail),
      typeof (CROpportunityProducts),
      typeof (CRTaxTran)
    };
    foreach (System.Type type in typeArray)
    {
      ((PXGraph) this).Caches[type].AllowSelect = isSalesQuote;
      PXCache cach1 = ((PXGraph) this).Caches[type];
      PXCache cach2 = ((PXGraph) this).Caches[type];
      bool flag1;
      ((PXGraph) this).Caches[type].AllowDelete = flag1 = crQuote == null || !crQuote.IsDisabled.GetValueOrDefault();
      int num1;
      bool flag2 = (num1 = flag1 ? 1 : 0) != 0;
      cach2.AllowUpdate = num1 != 0;
      int num2 = flag2 ? 1 : 0;
      cach1.AllowInsert = num2 != 0;
    }
    if (!isSalesQuote)
      row.ManualTotalEntry = new bool?(false);
    PXCacheEx.Adjust<SharedRecordAttribute>(sender, (object) row).For<CROpportunity.billContactID>((Action<SharedRecordAttribute>) (a => a.Required = isSalesQuote)).SameFor<CROpportunity.billAddressID>();
    PXCacheEx.AttributeAdjuster<PXUIFieldAttribute> attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.Billing_Contact).Cache, (object) null);
    attributeAdjuster.ForAllFields((Action<PXUIFieldAttribute>) (a => a.Visible = isSalesQuote));
    attributeAdjuster = PXCacheEx.Adjust<PXUIFieldAttribute>(((PXSelectBase) this.Billing_Address).Cache, (object) null);
    attributeAdjuster.ForAllFields((Action<PXUIFieldAttribute>) (a => a.Visible = isSalesQuote));
    PXUIFieldAttribute.SetEnabled<CROpportunity.manualTotalEntry>(sender, (object) row, (crQuote != null ? (!crQuote.IsDisabled.GetValueOrDefault() ? 1 : 0) : 1) != 0 && row.PrimaryQuoteType != "P");
    PXUIFieldAttribute.SetEnabled<CROpportunity.classID>(sender, (object) row, crQuote == null || !crQuote.IsDisabled.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CROpportunity.curyID>(sender, (object) row, (crQuote != null ? (!crQuote.IsDisabled.GetValueOrDefault() ? 1 : 0) : 1) != 0 && row.PrimaryQuoteType != "P");
    PXUIFieldAttribute.SetEnabled<CROpportunity.bAccountID>(sender, (object) row, crQuote == null || !crQuote.IsDisabled.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CROpportunity.locationID>(sender, (object) row, crQuote == null || !crQuote.IsDisabled.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CROpportunity.curyAmount>(sender, (object) row, crQuote == null || !crQuote.IsDisabled.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CROpportunity.curyDiscTot>(sender, (object) row, crQuote == null || !crQuote.IsDisabled.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CROpportunity.branchID>(sender, (object) row, crQuote == null || !crQuote.IsDisabled.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CROpportunity.taxZoneID>(sender, (object) row, crQuote == null || !crQuote.IsDisabled.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CROpportunity.taxCalcMode>(sender, (object) row, crQuote == null || !crQuote.IsDisabled.GetValueOrDefault());
    PXUIFieldAttribute.SetVisible<CROpportunity.taxCalcMode>(sender, (object) row, row.PrimaryQuoteType != "P");
    PXUIFieldAttribute.SetEnabled<CROpportunity.allowOverrideBillingContactAddress>(sender, (object) row, crQuote == null || !crQuote.IsDisabled.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<CROpportunity.allowOverrideShippingContactAddress>(sender, (object) row, crQuote == null || !crQuote.IsDisabled.GetValueOrDefault());
    ((PXGraph) this).Caches[typeof (CRContact)].AllowUpdate = row.AllowOverrideContactAddress.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled(((PXGraph) this).Caches[typeof (CRContact)], (string) null, ((PXGraph) this).Caches[typeof (CRContact)].AllowUpdate);
    ((PXGraph) this).Caches[typeof (CRAddress)].AllowUpdate = row.AllowOverrideContactAddress.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled(((PXGraph) this).Caches[typeof (CRAddress)], (string) null, ((PXGraph) this).Caches[typeof (CRAddress)].AllowUpdate);
    int? nullable1 = row.BAccountID;
    if (nullable1.HasValue && ((PXSelectBase<CRQuote>) this.PrimaryQuoteQuery).Current != null && ((PXSelectBase<CRQuote>) this.PrimaryQuoteQuery).Current.Status != "D")
      PXUIFieldAttribute.SetEnabled<CROpportunity.bAccountID>(sender, (object) row, false);
    PXUIFieldAttribute.SetEnabled<CROpportunity.curyAmount>(sender, e.Row, row.ManualTotalEntry.GetValueOrDefault());
    bool flag3 = PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>();
    PXUIFieldAttribute.SetEnabled<CROpportunity.curyDiscTot>(sender, e.Row, row.ManualTotalEntry.GetValueOrDefault() || !flag3);
    PXUIFieldAttribute.SetEnabled<CROpportunity.opportunityID>(sender, e.Row, true);
    PXCache pxCache = sender;
    CROpportunity crOpportunity = row;
    nullable1 = row.BAccountID;
    int num3;
    if (!nullable1.HasValue)
    {
      nullable1 = row.ContactID;
      num3 = nullable1.HasValue ? 1 : 0;
    }
    else
      num3 = 1;
    PXUIFieldAttribute.SetEnabled<CROpportunity.allowOverrideContactAddress>(pxCache, (object) crOpportunity, num3 != 0);
    PXUIFieldAttribute.SetEnabled<CROpportunity.projectID>(sender, e.Row, row.PrimaryQuoteType == "D");
    PXUIFieldAttribute.SetVisible<CROpportunity.curyID>(sender, (object) row, this.IsMultyCurrency);
    Decimal? nullable2 = new Decimal?();
    CROpportunityProbability opportunityProbability = row.StageID.With<string, CROpportunityProbability>((Func<string, CROpportunityProbability>) (_ => PXResultset<CROpportunityProbability>.op_Implicit(PXSelectBase<CROpportunityProbability, PXSelect<CROpportunityProbability, Where<CROpportunityProbability.stageCode, Equal<Required<CROpportunityProbability.stageCode>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) _
    }))));
    if (opportunityProbability != null)
    {
      nullable1 = opportunityProbability.Probability;
      if (nullable1.HasValue)
      {
        nullable1 = opportunityProbability.Probability;
        Decimal? nullable3 = nullable1.HasValue ? new Decimal?((Decimal) nullable1.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable4 = ((PXGraph) this).Accessinfo.CuryViewState ? row.ProductsAmount : row.CuryProductsAmount;
        Decimal? nullable5 = nullable3.HasValue & nullable4.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * nullable4.GetValueOrDefault()) : new Decimal?();
        Decimal num4 = (Decimal) 100;
        nullable2 = nullable5.HasValue ? new Decimal?(nullable5.GetValueOrDefault() / num4) : new Decimal?();
      }
    }
    row.CuryWgtAmount = nullable2;
    bool flag4 = crQuote != null;
    ((PXAction) this.createQuote).SetEnabled(((PXSelectBase<CROpportunity>) this.Opportunity).Current.IsActive.GetValueOrDefault());
    PXAction<CROpportunity> primaryQuote = this.primaryQuote;
    bool? nullable6;
    int num5;
    if (flag4)
    {
      nullable6 = ((PXSelectBase<CROpportunity>) this.Opportunity).Current.IsActive;
      num5 = nullable6.GetValueOrDefault() ? 1 : 0;
    }
    else
      num5 = 0;
    ((PXAction) primaryQuote).SetEnabled(num5 != 0);
    PXAction<CROpportunity> copyQuote = this.copyQuote;
    int num6;
    if (flag4)
    {
      nullable6 = ((PXSelectBase<CROpportunity>) this.Opportunity).Current.IsActive;
      num6 = nullable6.GetValueOrDefault() ? 1 : 0;
    }
    else
      num6 = 0;
    ((PXAction) copyQuote).SetEnabled(num6 != 0);
    if (!((PXGraph) this).UnattendedMode)
    {
      CRShippingAddress crShippingAddress = PXResultset<CRShippingAddress>.op_Implicit(((PXSelectBase<CRShippingAddress>) this.Shipping_Address).Select(Array.Empty<object>()));
      CRBillingAddress crBillingAddress = PXResultset<CRBillingAddress>.op_Implicit(((PXSelectBase<CRBillingAddress>) this.Billing_Address).Select(Array.Empty<object>()));
      CRAddress crAddress = PXResultset<CRAddress>.op_Implicit(((PXSelectBase<CRAddress>) this.Opportunity_Address).Select(Array.Empty<object>()));
      if (crShippingAddress != null)
      {
        nullable6 = crShippingAddress.IsDefaultAddress;
        bool flag5 = false;
        if (nullable6.GetValueOrDefault() == flag5 & nullable6.HasValue)
        {
          nullable6 = crShippingAddress.IsValidated;
          bool flag6 = false;
          if (nullable6.GetValueOrDefault() == flag6 & nullable6.HasValue)
            goto label_37;
        }
      }
      if (crBillingAddress != null)
      {
        nullable6 = crBillingAddress.IsDefaultAddress;
        bool flag7 = false;
        if (nullable6.GetValueOrDefault() == flag7 & nullable6.HasValue)
        {
          nullable6 = crBillingAddress.IsValidated;
          bool flag8 = false;
          if (nullable6.GetValueOrDefault() == flag8 & nullable6.HasValue)
            goto label_37;
        }
      }
      int num7;
      if (crAddress != null)
      {
        nullable6 = crAddress.IsDefaultAddress;
        bool flag9 = false;
        if (!(nullable6.GetValueOrDefault() == flag9 & nullable6.HasValue))
        {
          nullable1 = row.BAccountID;
          if (!nullable1.HasValue)
          {
            nullable1 = row.ContactID;
            if (nullable1.HasValue)
              goto label_36;
          }
          else
            goto label_36;
        }
        nullable6 = crAddress.IsValidated;
        bool flag10 = false;
        num7 = nullable6.GetValueOrDefault() == flag10 & nullable6.HasValue ? 1 : 0;
        goto label_38;
      }
label_36:
      num7 = 0;
      goto label_38;
label_37:
      num7 = 1;
label_38:
      ((PXAction) this.validateAddresses).SetEnabled(num7 != 0);
    }
    PXUIFieldAttribute.SetVisible<CROpportunityProducts.curyUnitCost>(((PXSelectBase) this.Products).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>() && PXAccess.FeatureInstalled<FeaturesSet.inventory>());
    PXUIFieldAttribute.SetVisible<CROpportunityProducts.vendorID>(((PXSelectBase) this.Products).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.inventory>());
    PXUIFieldAttribute.SetVisible<CROpportunityProducts.pOCreate>(((PXSelectBase) this.Products).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.inventory>());
  }

  protected virtual void CROpportunityRevision_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    CROpportunityRevision row = (CROpportunityRevision) e.Row;
    if (row == null || ((PXSelectBase<CROpportunity>) this.Opportunity).Current == null || !(row.OpportunityID == ((PXSelectBase<CROpportunity>) this.Opportunity).Current.OpportunityID))
      return;
    Guid? noteId = row.NoteID;
    Guid? defQuoteId = ((PXSelectBase<CROpportunity>) this.Opportunity).Current.DefQuoteID;
    if ((noteId.HasValue == defQuoteId.HasValue ? (noteId.HasValue ? (noteId.GetValueOrDefault() == defQuoteId.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CROpportunity_BAccountID_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is CROpportunity row))
      return;
    int? baccountId = row.BAccountID;
    int num = 0;
    if (!(baccountId.GetValueOrDefault() < num & baccountId.HasValue))
      return;
    e.ReturnValue = (object) "";
  }

  protected virtual void CROpportunity_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is CROpportunity row))
      return;
    object obj;
    ((PXGraph) this).Caches[typeof (CROpportunity)].RaiseFieldDefaulting<CROpportunity.quoteNoteID>((object) row, ref obj);
    if (obj != null)
      row.DefQuoteID = (Guid?) obj;
    object contactId1 = (object) row.ContactID;
    if (contactId1 != null && !this.VerifyField<CROpportunity.contactID>((object) row, contactId1))
      row.ContactID = new int?();
    int? contactId2 = row.ContactID;
    if (contactId2.HasValue && (ValueType) row.BAccountID == null)
      this.FillDefaultBAccountID(cache, row);
    object locationId = (object) row.LocationID;
    if (locationId == null || !this.VerifyField<CROpportunity.locationID>((object) row, locationId))
      cache.SetDefaultExt<CROpportunity.locationID>((object) row);
    contactId2 = row.ContactID;
    if (!contactId2.HasValue)
      cache.SetDefaultExt<CROpportunity.contactID>((object) row);
    if (row.TaxZoneID != null)
      return;
    cache.SetDefaultExt<CROpportunity.taxZoneID>((object) row);
  }

  protected virtual void CROpportunity_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    CROpportunity oldRow = e.OldRow as CROpportunity;
    CROpportunity row = e.Row as CROpportunity;
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
    bool flag2 = !sender.ObjectsEqual<CROpportunity.locationID>(e.Row, e.OldRow);
    if (flag2 | flag1 && (locationId == null || !this.VerifyField<CROpportunity.locationID>((object) row, locationId)))
      sender.SetDefaultExt<CROpportunity.locationID>((object) row);
    int num = !sender.ObjectsEqual<CROpportunity.campaignSourceID>(e.Row, e.OldRow) ? 1 : 0;
    bool flag3 = !sender.ObjectsEqual<CROpportunity.projectID>(e.Row, e.OldRow);
    if (num != 0)
    {
      CRCampaign crCampaign = (CRCampaign) PXSelectorAttribute.Select<CROpportunity.campaignSourceID>(((PXSelectBase) this.Opportunity).Cache, (object) ((PXSelectBase<CROpportunity>) this.Opportunity).Current);
      if (flag3)
      {
        nullable1 = crCampaign.ProjectID;
        nullable2 = row.ProjectID;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          goto label_11;
      }
      sender.SetDefaultExt<CROpportunity.projectID>((object) row);
label_11:
      flag3 = sender.ObjectsEqual<CROpportunity.projectID>(e.Row, e.OldRow);
    }
    else if (flag3)
    {
      CRCampaign crCampaign = (CRCampaign) PXSelectorAttribute.Select<CROpportunity.campaignSourceID>(((PXSelectBase) this.Opportunity).Cache, (object) ((PXSelectBase<CROpportunity>) this.Opportunity).Current);
      if (crCampaign != null)
      {
        nullable2 = crCampaign.ProjectID;
        nullable1 = row.ProjectID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          goto label_16;
      }
      sender.SetDefaultExt<CROpportunity.campaignSourceID>((object) row);
    }
label_16:
    if (!flag3 & flag1 && PXSelectorAttribute.Select<CROpportunity.projectID>(((PXSelectBase) this.Opportunity).Cache, (object) ((PXSelectBase<CROpportunity>) this.Opportunity).Current) == null)
    {
      nullable1 = row.ProjectID;
      if (nullable1.HasValue)
      {
        CROpportunity crOpportunity = row;
        nullable1 = new int?();
        int? nullable3 = nullable1;
        crOpportunity.ProjectID = nullable3;
        sender.SetDefaultExt<CROpportunity.projectID>((object) row);
      }
    }
    DateTime? closeDate1 = row.CloseDate;
    DateTime? closeDate2 = oldRow.CloseDate;
    bool flag4 = closeDate1.HasValue != closeDate2.HasValue || closeDate1.HasValue && closeDate1.GetValueOrDefault() != closeDate2.GetValueOrDefault();
    if (flag2 | flag4 | flag3 | flag1)
    {
      PXCache cache = ((PXSelectBase) this.Products).Cache;
      foreach (CROpportunityProducts selectProduct in this.SelectProducts((object) row.QuoteNoteID))
      {
        CROpportunityProducts copy = (CROpportunityProducts) cache.CreateCopy((object) selectProduct);
        copy.ProjectID = row.ProjectID;
        copy.CustomerID = row.BAccountID;
        cache.Update((object) copy);
      }
      sender.SetDefaultExt<CROpportunity.taxCalcMode>((object) row);
    }
    if (flag2)
    {
      sender.SetDefaultExt<CROpportunity.taxZoneID>((object) row);
      sender.SetDefaultExt<CROpportunity.taxRegistrationID>((object) row);
      sender.SetDefaultExt<CROpportunity.externalTaxExemptionNumber>((object) row);
      sender.SetDefaultExt<CROpportunity.avalaraCustomerUsageType>((object) row);
    }
    nullable1 = row.OwnerID;
    if (!nullable1.HasValue)
    {
      row.AssignDate = new DateTime?();
    }
    else
    {
      nullable1 = oldRow.OwnerID;
      if (!nullable1.HasValue)
        row.AssignDate = new DateTime?(PXTimeZoneInfo.Now);
    }
    if (!this.IsExternalTax(((PXSelectBase<CROpportunity>) this.Opportunity).Current.TaxZoneID) || sender.ObjectsEqual<CROpportunity.contactID, CROpportunity.taxZoneID, CROpportunity.branchID, CROpportunity.locationID, CROpportunity.curyAmount, CROpportunity.shipAddressID, CROpportunity.carrierID>(e.Row, e.OldRow) && (((PXSelectBase<CRQuote>) this.PrimaryQuoteQuery).SelectSingle(Array.Empty<object>()) != null || sender.ObjectsEqual<CROpportunity.closeDate>(e.Row, e.OldRow)))
      return;
    row.IsTaxValid = new bool?(false);
  }

  protected virtual void CROpportunity_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    CROpportunity row = (CROpportunity) e.Row;
    if (row == null || e.Operation != 2 && e.Operation != 1 || !row.BAccountID.HasValue)
      return;
    PXDefaultAttribute.SetPersistingCheck<CROpportunity.locationID>(sender, e.Row, (PXPersistingCheck) 1);
  }

  protected virtual void CROpportunity_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (!(e.Row is CROpportunity row) || e.Operation != 3 || e.TranStatus != null)
      return;
    List<Guid?> nullableList = new List<Guid?>();
    nullableList.Add(row.QuoteNoteID);
    PXView view = ((PXSelectBase) this.Quotes).View;
    object[] objArray1 = new object[1]{ (object) row };
    object[] objArray2 = Array.Empty<object>();
    foreach (CRQuote crQuote in view.SelectMultiBound(objArray1, objArray2))
    {
      PXDatabase.Delete<EPApproval>(new PXDataFieldRestrict[1]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<EPApproval.refNoteID>((object) crQuote.NoteID)
      });
      nullableList.Add(crQuote.QuoteID);
    }
    foreach (Guid? nullable in nullableList)
    {
      PXDatabase.Delete<CROpportunityRevision>(new PXDataFieldRestrict[1]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<CROpportunityRevision.noteID>((object) nullable)
      });
      PXDatabase.Delete<PX.Objects.CR.Standalone.CRQuote>(new PXDataFieldRestrict[1]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<PX.Objects.CR.Standalone.CRQuote.quoteID>((object) nullable)
      });
    }
  }

  protected virtual void CROpportunityProducts_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is CROpportunityProducts row))
      return;
    bool? nullable = row.ManualDisc;
    int num1;
    if (!nullable.GetValueOrDefault())
    {
      nullable = row.IsFree;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 0;
    if (num1 != 0)
    {
      PXUIFieldAttribute.SetEnabled<CROpportunityProducts.taxCategoryID>(sender, e.Row);
      PXUIFieldAttribute.SetEnabled<CROpportunityProducts.descr>(sender, e.Row);
    }
    PXCache pxCache = sender;
    CROpportunityProducts opportunityProducts = row;
    nullable = row.POCreate;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<CROpportunityProducts.vendorID>(pxCache, (object) opportunityProducts, num2 != 0);
    PXUIFieldAttribute.SetEnabled<CROpportunityProducts.skipLineDiscounts>(sender, (object) row, ((PXGraph) this).IsCopyPasteContext);
  }

  protected virtual void CROpportunityProducts_TaskID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<CROpportunity>) this.Opportunity).Current.CampaignSourceID == null)
      return;
    CRCampaign crCampaign = (CRCampaign) PXSelectorAttribute.Select<CROpportunity.campaignSourceID>(((PXSelectBase) this.Opportunity).Cache, (object) ((PXSelectBase<CROpportunity>) this.Opportunity).Current);
    if (crCampaign == null)
      return;
    int? projectId1 = crCampaign.ProjectID;
    int? projectId2 = ((PXSelectBase<CROpportunity>) this.Opportunity).Current.ProjectID;
    if (!(projectId1.GetValueOrDefault() == projectId2.GetValueOrDefault() & projectId1.HasValue == projectId2.HasValue))
      return;
    e.NewValue = (object) crCampaign.ProjectTaskID;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CROpportunityProducts_VendorID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row == null)
      return;
    CROpportunityProducts row = (CROpportunityProducts) e.Row;
    bool? poCreate = row.POCreate;
    bool flag = false;
    if (!(poCreate.GetValueOrDefault() == flag & poCreate.HasValue) && row.InventoryID.HasValue)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CROpportunityProducts_POCreate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    CROpportunityProducts row = (CROpportunityProducts) e.Row;
    sender.SetDefaultExt<CROpportunityProducts.vendorID>((object) row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.pOCreate> e)
  {
    if (e.Row == null)
      return;
    int? nullable = e.Row.InventoryID;
    if (!nullable.HasValue)
      return;
    nullable = e.Row.SiteID;
    if (!nullable.HasValue)
      return;
    bool flag1 = PXAccess.FeatureInstalled<FeaturesSet.dropShipments>();
    bool flag2 = PXAccess.FeatureInstalled<FeaturesSet.sOToPOLink>();
    INItemSiteSettings itemSiteSettings = ((PXSelectBase<INItemSiteSettings>) this.initemsettings).SelectSingle(new object[2]
    {
      (object) e.Row.InventoryID,
      (object) e.Row.SiteID
    });
    if (itemSiteSettings.ReplenishmentSource == "D" & flag1 || itemSiteSettings.ReplenishmentSource == "O" & flag2)
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.pOCreate>, CROpportunityProducts, object>) e).NewValue = (object) true;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.pOCreate>>) e).Cancel = true;
    }
    else
    {
      INItemSite inItemSite = PXResultset<INItemSite>.op_Implicit(PXSelectBase<INItemSite, PXSelect<INItemSite, Where<INItemSite.inventoryID, Equal<Required<INItemSite.inventoryID>>, And<INItemSite.siteID, Equal<Required<INItemSite.siteID>>>>>.Config>.Select(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.pOCreate>>) e).Cache.Graph, new object[2]
      {
        (object) e.Row.InventoryID,
        (object) e.Row.SiteID
      }));
      if (inItemSite == null)
        return;
      INReplenishmentClass replenishmentClass = INReplenishmentClass.PK.Find(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.pOCreate>>) e).Cache.Graph, inItemSite.ReplenishmentClassID);
      if (!(inItemSite.ReplenishmentSource == "D") || !(replenishmentClass?.ReplenishmentSource == "P") || !PXAccess.FeatureInstalled<FeaturesSet.dropShipments>())
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.pOCreate>, CROpportunityProducts, object>) e).NewValue = (object) true;
    }
  }

  protected virtual void CROpportunityProducts_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!this.IsExternalTax(((PXSelectBase<CROpportunity>) this.Opportunity).Current.TaxZoneID))
      return;
    ((PXSelectBase<CROpportunity>) this.Opportunity).Current.IsTaxValid = new bool?(false);
    ((PXSelectBase<CROpportunity>) this.Opportunity).Update(((PXSelectBase<CROpportunity>) this.Opportunity).Current);
  }

  protected virtual void CROpportunityProducts_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if (!this.IsExternalTax(((PXSelectBase<CROpportunity>) this.Opportunity).Current.TaxZoneID))
      return;
    ((PXSelectBase<CROpportunity>) this.Opportunity).Current.IsTaxValid = new bool?(false);
    ((PXSelectBase<CROpportunity>) this.Opportunity).Update(((PXSelectBase<CROpportunity>) this.Opportunity).Current);
  }

  protected virtual void CROpportunityProducts_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!this.IsExternalTax(((PXSelectBase<CROpportunity>) this.Opportunity).Current.TaxZoneID))
      return;
    ((PXSelectBase) this.Opportunity).Cache.SetValue((object) ((PXSelectBase<CROpportunity>) this.Opportunity).Current, typeof (CROpportunity.isTaxValid).Name, (object) false);
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

  [PopupMessage]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noSales>>>>), "The inventory item is {0}.", new System.Type[] {typeof (PX.Objects.IN.InventoryItem.itemStatus)}, ShowWarning = true)]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.inventoryID> e)
  {
  }

  protected virtual void CROpportunityProducts_InventoryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null)
      return;
    CROpportunityProducts row = (CROpportunityProducts) e.Row;
    sender.SetValueExt<CROpportunityProducts.pOCreate>((object) row, (object) false);
    sender.SetDefaultExt<CROpportunityProducts.pOCreate>(e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.AR.ARTran, CROpportunityProducts.costCodeID> e)
  {
    PMProject project;
    if (!CostCodeAttribute.UseCostCode() || !ProjectDefaultAttribute.IsProject((PXGraph) this, e.Row.ProjectID, out project) || !(project.BudgetLevel == "T"))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.AR.ARTran, CROpportunityProducts.costCodeID>, PX.Objects.AR.ARTran, object>) e).NewValue = (object) CostCodeAttribute.GetDefaultCostCode();
  }

  [PXMergeAttributes]
  [RestrictSiteByBranch(typeof (CROpportunity.branchID), null, ShowWarning = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.siteID> e)
  {
  }

  protected virtual void CreateQuotesFilter_AddProductsFromOpportunity_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is OpportunityMaint.CreateQuotesFilter row))
      return;
    e.NewValue = (object) (bool) (((PXSelectBase<CROpportunityProducts>) this.Products).SelectSingle(Array.Empty<object>()) == null || !(row.QuoteType == "D") ? 0 : (((PXSelectBase<CROpportunity>) this.Opportunity).Current.PrimaryQuoteType == "D" ? 1 : 0));
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CreateQuotesFilter_QuoteType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is OpportunityMaint.CreateQuotesFilter))
      return;
    e.NewValue = OpportunityMaint.SalesQuotesInstalled ? (object) "D" : (object) "P";
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CreateQuotesFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is OpportunityMaint.CreateQuotesFilter row))
      return;
    bool? nullable = row.RecalculatePrices;
    if (!nullable.GetValueOrDefault())
      ((PXSelectBase) this.QuoteInfo).Cache.SetValue<OpportunityMaint.CreateQuotesFilter.overrideManualPrices>((object) row, (object) false);
    nullable = row.RecalculateDiscounts;
    if (!nullable.GetValueOrDefault())
    {
      ((PXSelectBase) this.QuoteInfo).Cache.SetValue<OpportunityMaint.CreateQuotesFilter.overrideManualDiscounts>((object) row, (object) false);
      ((PXSelectBase) this.QuoteInfo).Cache.SetValue<OpportunityMaint.CreateQuotesFilter.overrideManualDocGroupDiscounts>((object) row, (object) false);
    }
    if (!(row.QuoteType == "P"))
      return;
    ((PXSelectBase) this.QuoteInfo).Cache.SetValue<OpportunityMaint.CreateQuotesFilter.addProductsFromOpportunity>((object) row, (object) false);
  }

  protected virtual void CreateQuotesFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is OpportunityMaint.CreateQuotesFilter row))
      return;
    bool flag1 = NonGenericIEnumerableExtensions.Any_((IEnumerable) ((PXSelectBase<CROpportunityProducts>) this.Products).Select(Array.Empty<object>()));
    bool flag2 = NonGenericIEnumerableExtensions.Any_((IEnumerable) ((PXSelectBase<CRQuote>) this.Quotes).Select(Array.Empty<object>()));
    bool flag3 = row.QuoteType == "D";
    int num = row.QuoteType == "P" ? 1 : 0;
    bool flag4 = ((PXSelectBase<CROpportunity>) this.Opportunity).Current.PrimaryQuoteType == "D";
    Numbering numbering = Numbering.PK.Find((PXGraph) this, ((PXSelectBase<CRSetup>) this.Setup).Current.QuoteNumberingID);
    bool flag5 = numbering != null && numbering.UserNumbering.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<OpportunityMaint.CreateQuotesFilter.quoteType>(sender, (object) row, OpportunityMaint.ProjectQuotesInstalled && OpportunityMaint.SalesQuotesInstalled);
    PXUIFieldAttribute.SetEnabled<OpportunityMaint.CreateQuotesFilter.addProductsFromOpportunity>(sender, (object) row, flag1 & flag2 & flag3 & flag4);
    PXUIFieldAttribute.SetEnabled<OpportunityMaint.CreateQuotesFilter.makeNewQuotePrimary>(sender, (object) row, flag2);
    PXUIFieldAttribute.SetEnabled<OpportunityMaint.CreateQuotesFilter.recalculatePrices>(sender, (object) row, flag3);
    PXUIFieldAttribute.SetEnabled<OpportunityMaint.CreateQuotesFilter.overrideManualPrices>(sender, (object) row, row.RecalculatePrices.GetValueOrDefault() & flag3);
    PXUIFieldAttribute.SetEnabled<OpportunityMaint.CreateQuotesFilter.recalculateDiscounts>(sender, (object) row, flag3);
    PXUIFieldAttribute.SetEnabled<OpportunityMaint.CreateQuotesFilter.overrideManualDiscounts>(sender, (object) row, row.RecalculateDiscounts.GetValueOrDefault() & flag3);
    PXUIFieldAttribute.SetEnabled<OpportunityMaint.CreateQuotesFilter.overrideManualDocGroupDiscounts>(sender, (object) row, row.RecalculateDiscounts.GetValueOrDefault());
    PXUIFieldAttribute.SetVisible<OpportunityMaint.CreateQuotesFilter.quoteNbr>(sender, (object) row, flag5);
    if (row.QuoteType == "P")
    {
      if (!PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>() && ((PXSelectBase<CROpportunity>) this.OpportunityCurrent).Current.CuryID != (((PXGraph) this).Accessinfo.BaseCuryID ?? ((PXSelectBase<Company>) new PXSetup<Company>((PXGraph) this)).Current?.BaseCuryID))
        sender.RaiseExceptionHandling<OpportunityMaint.CreateQuotesFilter.quoteType>((object) row, (object) row.QuoteType, (Exception) new PXSetPropertyException("Cannot create a project quote for the opportunity in a non-base currency.", (PXErrorLevel) 4));
      if (!((PXSelectBase<CROpportunity>) this.Opportunity).Current.ManualTotalEntry.GetValueOrDefault())
        return;
      sender.RaiseExceptionHandling<OpportunityMaint.CreateQuotesFilter.quoteType>((object) row, (object) row.QuoteType, (Exception) new PXSetPropertyException("The manual amount specified for the opportunity will be cleared.", (PXErrorLevel) 2));
    }
    else
      sender.RaiseExceptionHandling<OpportunityMaint.CreateQuotesFilter.quoteType>((object) row, (object) row.QuoteType, (Exception) null);
  }

  protected virtual void CopyQuoteFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is OpportunityMaint.CopyQuoteFilter row))
      return;
    bool? nullable = row.RecalculatePrices;
    if (!nullable.GetValueOrDefault())
      ((PXSelectBase) this.CopyQuoteInfo).Cache.SetValue<OpportunityMaint.CopyQuoteFilter.overrideManualPrices>((object) row, (object) false);
    nullable = row.RecalculateDiscounts;
    if (nullable.GetValueOrDefault())
      return;
    ((PXSelectBase) this.CopyQuoteInfo).Cache.SetValue<OpportunityMaint.CopyQuoteFilter.overrideManualDiscounts>((object) row, (object) false);
    ((PXSelectBase) this.CopyQuoteInfo).Cache.SetValue<OpportunityMaint.CopyQuoteFilter.overrideManualDocGroupDiscounts>((object) row, (object) false);
  }

  protected virtual void CopyQuoteFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is OpportunityMaint.CopyQuoteFilter row))
      return;
    PXUIFieldAttribute.SetEnabled<OpportunityMaint.CopyQuoteFilter.recalculatePrices>(sender, (object) row, ((PXSelectBase<CRQuote>) this.Quotes).Current?.QuoteType == "D");
    PXCache pxCache1 = sender;
    OpportunityMaint.CopyQuoteFilter copyQuoteFilter1 = row;
    bool? nullable = row.RecalculatePrices;
    int num1 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<OpportunityMaint.CopyQuoteFilter.overrideManualPrices>(pxCache1, (object) copyQuoteFilter1, num1 != 0);
    PXUIFieldAttribute.SetEnabled<OpportunityMaint.CopyQuoteFilter.recalculateDiscounts>(sender, (object) row, ((PXSelectBase<CRQuote>) this.Quotes).Current?.QuoteType == "D");
    PXCache pxCache2 = sender;
    OpportunityMaint.CopyQuoteFilter copyQuoteFilter2 = row;
    nullable = row.RecalculateDiscounts;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<OpportunityMaint.CopyQuoteFilter.overrideManualDiscounts>(pxCache2, (object) copyQuoteFilter2, num2 != 0);
    PXCache pxCache3 = sender;
    OpportunityMaint.CopyQuoteFilter copyQuoteFilter3 = row;
    nullable = row.RecalculateDiscounts;
    int num3 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<OpportunityMaint.CopyQuoteFilter.overrideManualDocGroupDiscounts>(pxCache3, (object) copyQuoteFilter3, num3 != 0);
  }

  public virtual void _(PX.Data.Events.RowSelected<CRShippingContact> e)
  {
    this.SetDisabledItemIfQuoteDisabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRShippingContact>>) e).Cache, (object) e.Row);
  }

  public virtual void _(PX.Data.Events.RowSelected<CRShippingAddress> e)
  {
    this.SetDisabledItemIfQuoteDisabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRShippingAddress>>) e).Cache, (object) e.Row);
  }

  public virtual void _(PX.Data.Events.RowSelected<CRBillingContact> e)
  {
    this.SetDisabledItemIfQuoteDisabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRBillingContact>>) e).Cache, (object) e.Row);
  }

  public virtual void _(PX.Data.Events.RowSelected<CRBillingAddress> e)
  {
    this.SetDisabledItemIfQuoteDisabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRBillingAddress>>) e).Cache, (object) e.Row);
  }

  private void SetDisabledItemIfQuoteDisabled(PXCache cache, object row)
  {
    if (row == null)
      return;
    CRQuote crQuote = ((PXSelectBase<CRQuote>) this.PrimaryQuoteQuery).SelectSingle(Array.Empty<object>());
    if ((crQuote != null ? (crQuote.IsDisabled.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    PXUIFieldAttribute.SetEnabled(cache, row, false);
  }

  public virtual void _(
    PX.Data.Events.FieldSelecting<PopupUDFAttributes, PopupUDFAttributes.value> e)
  {
    PopupUDFAttributes row = e.Row;
    if (row == null)
      return;
    OpportunityMaint.CreateQuotesFilter current = (OpportunityMaint.CreateQuotesFilter) ((PXCache) GraphHelper.Caches<OpportunityMaint.CreateQuotesFilter>((PXGraph) this))?.Current;
    System.Type graphType = typeof (QuoteMaint);
    if (current?.QuoteType == "P")
      graphType = typeof (PMQuoteMaint);
    string screenId = PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, graphType)?.ScreenID;
    if (row == null || screenId == null || !screenId.Equals(row.ScreenID))
      return;
    PXFieldState graphUdfFieldState = UDFHelper.GetGraphUDFFieldState(graphType, row.AttributeID);
    if (graphUdfFieldState == null)
      return;
    graphUdfFieldState.Required = new bool?(true);
    if (!string.IsNullOrEmpty(row.Value))
      graphUdfFieldState.Value = (object) row.Value;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PopupUDFAttributes, PopupUDFAttributes.value>>) e).ReturnState = (object) graphUdfFieldState;
    ((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<PopupUDFAttributes, PopupUDFAttributes.value>>) e).Cache.IsDirty = false;
  }

  [CRMBAccount(new System.Type[] {typeof (BAccountType.prospectType), typeof (BAccountType.customerType), typeof (BAccountType.combinedType), typeof (BAccountType.vendorType)}, null, null, null)]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<Contact.bAccountID> e)
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

  [LocationActive(typeof (Where<Location.bAccountID, Equal<Current<CROpportunity.bAccountID>>>), DisplayName = "Location", DescriptionField = typeof (Location.descr), BqlField = typeof (CROpportunityRevision.locationID))]
  [PXMergeAttributes]
  public virtual void CROpportunity_LocationID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(false)]
  [PXDBCalced(typeof (True), typeof (bool))]
  [PXMergeAttributes]
  protected virtual void BAccountR_ViewInCrm_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(BqlField = typeof (CROpportunityRevision.opportunityContactID))]
  [PXMergeAttributes]
  public virtual void CRQuote_OpportunityContactID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXMergeAttributes]
  public virtual void CROpportunityRevision_OpportunityContactID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(BqlField = typeof (CROpportunityRevision.opportunityAddressID))]
  [PXMergeAttributes]
  public virtual void CRQuote_OpportunityAddressID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXMergeAttributes]
  public virtual void CROpportunityRevision_OpportunityAddressID_CacheAttached(PXCache sender)
  {
  }

  [PXDBUShort]
  [PXLineNbr(typeof (CROpportunity))]
  protected virtual void CROpportunityDiscountDetail_LineNbr_CacheAttached(PXCache e)
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

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Business Account")]
  protected virtual void _(PX.Data.Events.CacheAttached<CRQuote.bAccountID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Contact")]
  protected virtual void _(PX.Data.Events.CacheAttached<CRQuote.contactID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Location")]
  protected virtual void _(PX.Data.Events.CacheAttached<CRQuote.locationID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Sequence")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityDiscountDetail.discountSequenceID> e)
  {
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

  private void FillDefaultBAccountID(PXCache cache, CROpportunity row)
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
    cache.SetValueExt<CROpportunity.bAccountID>((object) row, (object) contact.BAccountID);
  }

  private bool IsMultyCurrency => PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();

  private IEnumerable SelectProducts(object quoteId)
  {
    if (quoteId == null)
      return (IEnumerable) new CROpportunityProducts[0];
    return (IEnumerable) GraphHelper.RowCast<CROpportunityProducts>((IEnumerable) PXSelectBase<CROpportunityProducts, PXSelect<CROpportunityProducts, Where<CROpportunityProducts.quoteID, Equal<Required<CROpportunity.quoteNoteID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      quoteId
    }));
  }

  private bool IsSingleQuote(string opportunityId)
  {
    return PXSelectBase<CRQuote, PXSelect<CRQuote, Where<CRQuote.opportunityID, Equal<Optional<CRQuote.opportunityID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) opportunityId
    }).Count == 0;
  }

  public virtual bool IsExternalTax(string taxZoneID) => false;

  public virtual CROpportunity CalculateExternalTax(CROpportunity opportunity) => opportunity;

  public virtual void Persist()
  {
    object current = ((PXSelectBase) this.Quotes).Cache.Current;
    ((PXGraph) this).Persist();
    ((PXSelectBase) this.Quotes).Cache.Clear();
    ((PXSelectBase) this.Quotes).View.Clear();
    ((PXSelectBase) this.Quotes).Cache.ClearQueryCache();
    ((PXSelectBase) this.Quotes).View.RequestRefresh();
    ((PXSelectBase) this.Quotes).Cache.Current = current;
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    object obj;
    if ("Products".Equals(viewName, StringComparison.OrdinalIgnoreCase) && (!PXExecutionContext.Current.Bag.TryGetValue("_DONT_UPDATE_EXIST_RECORDS", out obj) || !(obj is bool flag) || !flag) && values.Contains((object) "inventoryID") && values.Contains((object) "siteID"))
    {
      Guid? quoteNoteId = ((PXSelectBase<CROpportunity>) this.Opportunity).Current.QuoteNoteID;
      string str1 = (string) values[(object) "inventoryID"];
      string str2 = (string) values[(object) "siteID"];
      CROpportunityProducts opportunityProducts = (CROpportunityProducts) null;
      if (quoteNoteId.HasValue && !string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(str2))
        opportunityProducts = ((PXSelectBase<CROpportunityProducts>) this.ProductsByQuoteIDAndInventoryCD).SelectSingle(new object[3]
        {
          (object) quoteNoteId,
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
    return true;
  }

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public static bool ProjectQuotesInstalled
  {
    get => PXAccess.FeatureInstalled<FeaturesSet.projectQuotes>();
  }

  public static bool SalesQuotesInstalled => PXAccess.FeatureInstalled<FeaturesSet.salesQuotes>();

  [Serializable]
  public class CreateQuotesFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    /// <inheritdoc cref="P:PX.Objects.CR.CRQuote.QuoteNbr" />
    [PXString(15, IsUnicode = true, InputMask = "")]
    [PXUnboundDefault]
    [PXUIField(DisplayName = "Quote Nbr.", TabOrder = 1)]
    public virtual string QuoteNbr { get; set; }

    [PXString(1, IsFixed = true)]
    [PXUIField(DisplayName = "Quote Type")]
    [CRQuoteType]
    [PXUnboundDefault("D")]
    public virtual string QuoteType { get; set; }

    [PXBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Add Details from Opportunity")]
    public virtual bool? AddProductsFromOpportunity { get; set; }

    [PXBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Set New Quote as Primary")]
    public virtual bool? MakeNewQuotePrimary { get; set; }

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Set Current Unit Prices")]
    public virtual bool? RecalculatePrices { get; set; }

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Override Manual Prices")]
    public virtual bool? OverrideManualPrices { get; set; }

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Recalculate Discounts")]
    public virtual bool? RecalculateDiscounts { get; set; }

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Override Manual Line Discounts")]
    public virtual bool? OverrideManualDiscounts { get; set; }

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Override Manual Group and Document Discounts")]
    public virtual bool? OverrideManualDocGroupDiscounts { get; set; }

    public abstract class quoteNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      OpportunityMaint.CreateQuotesFilter.quoteNbr>
    {
    }

    public abstract class quoteType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      OpportunityMaint.CreateQuotesFilter.quoteType>
    {
    }

    public abstract class addProductsFromOpportunity : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      OpportunityMaint.CreateQuotesFilter.addProductsFromOpportunity>
    {
    }

    public abstract class makeNewQuotePrimary : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      OpportunityMaint.CreateQuotesFilter.makeNewQuotePrimary>
    {
    }

    public abstract class recalculatePrices : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      OpportunityMaint.CreateQuotesFilter.recalculatePrices>
    {
    }

    public abstract class overrideManualPrices : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      OpportunityMaint.CreateQuotesFilter.overrideManualPrices>
    {
    }

    public abstract class recalculateDiscounts : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      OpportunityMaint.CreateQuotesFilter.recalculateDiscounts>
    {
    }

    public abstract class overrideManualDiscounts : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      OpportunityMaint.CreateQuotesFilter.overrideManualDiscounts>
    {
    }

    public abstract class overrideManualDocGroupDiscounts : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      OpportunityMaint.CreateQuotesFilter.overrideManualDocGroupDiscounts>
    {
    }
  }

  [Serializable]
  public class CopyQuoteFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _Description;

    [PXString]
    [PXUIField(DisplayName = "Opportunity ID", Visible = false)]
    [PXSelector(typeof (Search2<CROpportunity.opportunityID, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CROpportunity.bAccountID>>, LeftJoin<Contact, On<Contact.contactID, Equal<CROpportunity.contactID>>>>, Where<BqlOperand<CROpportunity.isActive, IBqlBool>.IsEqual<True>>, OrderBy<Desc<CROpportunity.opportunityID>>>), new System.Type[] {typeof (CROpportunity.opportunityID), typeof (CROpportunity.subject), typeof (CROpportunity.status), typeof (CROpportunity.stageID), typeof (CROpportunity.classID), typeof (BAccount.acctName), typeof (Contact.displayName), typeof (CROpportunity.subject), typeof (CROpportunity.externalRef), typeof (CROpportunity.closeDate)}, Filterable = true)]
    [PXDefault(typeof (CROpportunity.opportunityID))]
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
    [PXDefault(false)]
    public virtual bool? RecalculatePrices { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Override Manual Prices", Enabled = false)]
    [PXDefault(false)]
    public virtual bool? OverrideManualPrices { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Recalculate Discounts")]
    [PXDefault(false)]
    public virtual bool? RecalculateDiscounts { get; set; }

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Override Manual Line Discounts", Enabled = false)]
    public virtual bool? OverrideManualDiscounts { get; set; }

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Override Manual Group and Document Discounts")]
    public virtual bool? OverrideManualDocGroupDiscounts { get; set; }

    public abstract class opportunityId : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      OpportunityMaint.CopyQuoteFilter.opportunityId>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      OpportunityMaint.CopyQuoteFilter.description>
    {
    }

    public abstract class recalculatePrices : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      OpportunityMaint.CopyQuoteFilter.recalculatePrices>
    {
    }

    public abstract class overrideManualPrices : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      OpportunityMaint.CopyQuoteFilter.overrideManualPrices>
    {
    }

    public abstract class recalculateDiscounts : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      OpportunityMaint.CopyQuoteFilter.recalculateDiscounts>
    {
    }

    public abstract class overrideManualDiscounts : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      OpportunityMaint.CopyQuoteFilter.overrideManualDiscounts>
    {
    }

    public abstract class overrideManualDocGroupDiscounts : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      OpportunityMaint.CopyQuoteFilter.overrideManualDocGroupDiscounts>
    {
    }
  }

  /// <exclude />
  public class MultiCurrency : CRMultiCurrencyGraph<OpportunityMaint, CROpportunity>
  {
    protected override MultiCurrencyGraph<OpportunityMaint, CROpportunity>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<OpportunityMaint, CROpportunity>.DocumentMapping(typeof (CROpportunity))
      {
        DocumentDate = typeof (CROpportunityRevision.documentDate)
      };
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[5]
      {
        (PXSelectBase) this.Base.Opportunity,
        (PXSelectBase) this.Base.Products,
        (PXSelectBase) this.Base.TaxLines,
        (PXSelectBase) this.Base.Taxes,
        (PXSelectBase) ((PXGraph) this.Base).GetExtension<OpportunityMaint.Discount>().DiscountDetails
      };
    }

    protected override BAccount GetRelatedBAccount()
    {
      return KeysRelation<Field<CROpportunity.bAccountID>.IsRelatedTo<BAccount.bAccountID>.AsSimpleKey.WithTablesOf<BAccount, CROpportunity>, BAccount, CROpportunity>.Dirty.FindParent((PXGraph) this.Base, ((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current, (PKFindOptions) 0);
    }

    protected override System.Type BAccountField => typeof (CROpportunity.bAccountID);

    protected override PXView DetailsView => ((PXSelectBase) this.Base.Products).View;

    protected override CurySource CurrentSourceSelect()
    {
      CRQuote crQuote = ((PXSelectBase<CRQuote>) this.Base.PrimaryQuoteQuery).SelectSingle(Array.Empty<object>());
      if (crQuote == null)
        return base.CurrentSourceSelect();
      CurySource curySource1 = new CurySource();
      if (crQuote.QuoteType != "P")
      {
        CurySource curySource2 = base.CurrentSourceSelect();
        if (crQuote.Status != "D")
        {
          curySource2.AllowOverrideCury = new bool?(false);
          curySource2.AllowOverrideRate = new bool?(false);
        }
        return curySource2;
      }
      if (PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>() && ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current != null)
      {
        curySource1.CuryID = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current.CuryID;
        curySource1.CuryRateTypeID = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current.CuryRateTypeID;
      }
      if (crQuote.Status == "D")
      {
        if (((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current != null)
        {
          curySource1.AllowOverrideCury = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current.AllowOverrideCury;
          curySource1.AllowOverrideRate = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current.AllowOverrideRate;
        }
        else
        {
          curySource1.AllowOverrideCury = new bool?(true);
          curySource1.AllowOverrideRate = new bool?(true);
        }
      }
      else
      {
        curySource1.AllowOverrideCury = new bool?(false);
        curySource1.AllowOverrideRate = new bool?(false);
      }
      return curySource1;
    }
  }

  /// <exclude />
  public class SalesPrice : SalesPriceGraph<OpportunityMaint, CROpportunity>
  {
    protected override SalesPriceGraph<OpportunityMaint, CROpportunity>.DocumentMapping GetDocumentMapping()
    {
      return new SalesPriceGraph<OpportunityMaint, CROpportunity>.DocumentMapping(typeof (CROpportunity));
    }

    protected override SalesPriceGraph<OpportunityMaint, CROpportunity>.DetailMapping GetDetailMapping()
    {
      return new SalesPriceGraph<OpportunityMaint, CROpportunity>.DetailMapping(typeof (CROpportunityProducts))
      {
        CuryLineAmount = typeof (CROpportunityProducts.curyExtPrice),
        Descr = typeof (CROpportunityProducts.descr)
      };
    }

    protected override SalesPriceGraph<OpportunityMaint, CROpportunity>.PriceClassSourceMapping GetPriceClassSourceMapping()
    {
      return new SalesPriceGraph<OpportunityMaint, CROpportunity>.PriceClassSourceMapping(typeof (Location))
      {
        PriceClassID = typeof (Location.cPriceClassID)
      };
    }
  }

  /// <exclude />
  public class Discount : DiscountGraph<OpportunityMaint, CROpportunity>
  {
    [PXViewName("Discount Details")]
    public PXSelect<CROpportunityDiscountDetail, Where<CROpportunityDiscountDetail.quoteID, Equal<Current<CROpportunity.quoteNoteID>>>, OrderBy<Asc<CROpportunityDiscountDetail.lineNbr>>> DiscountDetails;
    public PXAction<CROpportunity> recalculatePrices;

    protected override DiscountGraph<OpportunityMaint, CROpportunity>.DocumentMapping GetDocumentMapping()
    {
      return new DiscountGraph<OpportunityMaint, CROpportunity>.DocumentMapping(typeof (CROpportunity))
      {
        CuryDiscTot = typeof (CROpportunity.curyLineDocDiscountTotal),
        DocumentDate = typeof (CROpportunity.documentDate)
      };
    }

    protected override DiscountGraph<OpportunityMaint, CROpportunity>.DetailMapping GetDetailMapping()
    {
      return new DiscountGraph<OpportunityMaint, CROpportunity>.DetailMapping(typeof (CROpportunityProducts))
      {
        CuryLineAmount = typeof (CROpportunityProducts.curyAmount),
        Quantity = typeof (CROpportunityProducts.quantity)
      };
    }

    protected override DiscountGraph<OpportunityMaint, CROpportunity>.DiscountMapping GetDiscountMapping()
    {
      return new DiscountGraph<OpportunityMaint, CROpportunity>.DiscountMapping(typeof (CROpportunityDiscountDetail));
    }

    [PXSelector(typeof (Search<ARDiscount.discountID, Where<ARDiscount.type, NotEqual<DiscountType.LineDiscount>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouse>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndCustomer>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndCustomerPrice>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndInventory>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndInventoryPrice>>>>>>>>))]
    [PXMergeAttributes]
    public override void Discount_DiscountID_CacheAttached(PXCache sender)
    {
    }

    [PXMergeAttributes]
    [CurrencyInfo(typeof (CROpportunity.curyInfoID))]
    public override void Discount_CuryInfoID_CacheAttached(PXCache sender)
    {
    }

    protected virtual void Discount_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
    {
      if (!this.Base.IsExternalTax(((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current.TaxZoneID))
        return;
      ((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current.IsTaxValid = new bool?(false);
    }

    protected virtual void Discount_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
    {
      if (!this.Base.IsExternalTax(((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current.TaxZoneID))
        return;
      ((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current.IsTaxValid = new bool?(false);
    }

    protected virtual void Discount_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
    {
      if (!this.Base.IsExternalTax(((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current.TaxZoneID))
        return;
      ((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current.IsTaxValid = new bool?(false);
    }

    protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.Extensions.Discount.Document> e)
    {
      CROpportunity main = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.Extensions.Discount.Document>>) e).Cache.GetMain<PX.Objects.Extensions.Discount.Document>(e.Row) as CROpportunity;
      CRQuote crQuote = ((PXSelectBase<CRQuote>) this.Base.PrimaryQuoteQuery).SelectSingle(Array.Empty<object>());
      ((PXAction) this.recalculatePrices).SetEnabled(main.IsActive.GetValueOrDefault() && main.PrimaryQuoteType == "D" && (crQuote == null || !crQuote.IsDisabled.GetValueOrDefault()));
    }

    [PXUIField]
    [PXButton]
    public virtual IEnumerable RecalculatePrices(PXAdapter adapter)
    {
      List<CROpportunity> crOpportunityList = new List<CROpportunity>(adapter.Get<CROpportunity>());
      foreach (CROpportunity crOpportunity in crOpportunityList)
      {
        if (((PXSelectBase) this.recalcdiscountsfilter).View.Answer == null)
        {
          ((PXSelectBase) this.recalcdiscountsfilter).Cache.Clear();
          if (((PXSelectBase) this.recalcdiscountsfilter).Cache.Insert() is RecalcDiscountsParamFilter discountsParamFilter)
          {
            discountsParamFilter.RecalcUnitPrices = new bool?(true);
            discountsParamFilter.RecalcDiscounts = new bool?(true);
            discountsParamFilter.OverrideManualPrices = new bool?(false);
            discountsParamFilter.OverrideManualDiscounts = new bool?(false);
            discountsParamFilter.OverrideManualDocGroupDiscounts = new bool?(false);
          }
        }
        if (((PXSelectBase<RecalcDiscountsParamFilter>) this.recalcdiscountsfilter).AskExt() != 1)
          return (IEnumerable) crOpportunityList;
        this.RecalculateDiscountsAction(adapter);
      }
      return (IEnumerable) crOpportunityList;
    }

    protected override bool AddDocumentDiscount => true;

    protected override void DefaultDiscountAccountAndSubAccount(PX.Objects.Extensions.Discount.Detail det)
    {
    }
  }

  /// <exclude />
  public class SalesTax : TaxGraph<OpportunityMaint, CROpportunity>
  {
    protected override bool CalcGrossOnDocumentLevel
    {
      get => true;
      set => base.CalcGrossOnDocumentLevel = value;
    }

    protected override PXView DocumentDetailsView => ((PXSelectBase) this.Base.Products).View;

    protected override TaxBaseGraph<OpportunityMaint, CROpportunity>.DocumentMapping GetDocumentMapping()
    {
      return new TaxBaseGraph<OpportunityMaint, CROpportunity>.DocumentMapping(typeof (CROpportunity))
      {
        BranchID = typeof (CROpportunity.branchID),
        CuryID = typeof (CROpportunity.curyID),
        CuryInfoID = typeof (CROpportunity.curyInfoID),
        DocumentDate = typeof (CROpportunity.documentDate),
        TaxZoneID = typeof (CROpportunity.taxZoneID),
        TermsID = typeof (CROpportunity.termsID),
        CuryLinetotal = typeof (CROpportunity.curyLineTotal),
        CuryDiscountLineTotal = typeof (CROpportunity.curyLineDiscountTotal),
        CuryExtPriceTotal = typeof (CROpportunity.curyExtPriceTotal),
        CuryDocBal = typeof (CROpportunity.curyProductsAmount),
        CuryTaxTotal = typeof (CROpportunity.curyTaxTotal),
        CuryDiscTot = typeof (CROpportunity.curyDiscTot),
        TaxCalcMode = typeof (CROpportunity.taxCalcMode)
      };
    }

    protected override TaxBaseGraph<OpportunityMaint, CROpportunity>.DetailMapping GetDetailMapping()
    {
      return new TaxBaseGraph<OpportunityMaint, CROpportunity>.DetailMapping(typeof (CROpportunityProducts))
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

    protected override TaxBaseGraph<OpportunityMaint, CROpportunity>.TaxDetailMapping GetTaxDetailMapping()
    {
      return new TaxBaseGraph<OpportunityMaint, CROpportunity>.TaxDetailMapping(typeof (CROpportunityTax), typeof (CROpportunityTax.taxID));
    }

    protected override TaxBaseGraph<OpportunityMaint, CROpportunity>.TaxTotalMapping GetTaxTotalMapping()
    {
      return new TaxBaseGraph<OpportunityMaint, CROpportunity>.TaxTotalMapping(typeof (CRTaxTran), typeof (CRTaxTran.taxID));
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<CROpportunity, CROpportunity.curyDiscTot> e)
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
      PX.Data.Events.FieldUpdated<CROpportunity, CROpportunity.manualTotalEntry> e)
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
      CROpportunity main = (CROpportunity) ((PXSelectBase) this.Documents).Cache.GetMain<PX.Objects.Extensions.SalesTax.Document>(((PXSelectBase<PX.Objects.Extensions.SalesTax.Document>) this.Documents).Current ?? ((PXSelectBase<PX.Objects.Extensions.SalesTax.Document>) this.Documents).SelectSingle(Array.Empty<object>()));
      bool valueOrDefault1 = main.ManualTotalEntry.GetValueOrDefault();
      Decimal valueOrDefault2 = main.CuryAmount.GetValueOrDefault();
      Decimal valueOrDefault3 = main.CuryDiscTot.GetValueOrDefault();
      Decimal num1 = (Decimal) (this.ParentGetValue<CROpportunity.curyLineTotal>() ?? (object) 0M);
      Decimal num2 = (Decimal) (this.ParentGetValue<CROpportunity.curyLineDiscountTotal>() ?? (object) 0M);
      Decimal num3 = (Decimal) (this.ParentGetValue<CROpportunity.curyExtPriceTotal>() ?? (object) 0M);
      Decimal num4 = (Decimal) (this.ParentGetValue<CROpportunity.curyDiscTot>() ?? (object) 0M);
      Decimal objA = valueOrDefault1 ? valueOrDefault2 - valueOrDefault3 : num1 - num4 + CuryTaxTotal - CuryInclTaxTotal;
      if (object.Equals((object) objA, (object) (Decimal) (this.ParentGetValue<CROpportunity.curyProductsAmount>() ?? (object) 0M)))
        return;
      this.ParentSetValue<CROpportunity.curyProductsAmount>((object) objA);
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
      Dictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>> dictionary = new Dictionary<string, PXResult<PX.Objects.TX.Tax, TaxRev>>();
      object[] objArray = new object[2]
      {
        row == null || !(row is PX.Objects.Extensions.SalesTax.Detail) ? (object) null : ((PXSelectBase) this.Details).Cache.GetMain<PX.Objects.Extensions.SalesTax.Detail>((PX.Objects.Extensions.SalesTax.Detail) row),
        (object) ((PXSelectBase<CROpportunity>) ((OpportunityMaint) graph).Opportunity).Current
      };
      IComparer<PX.Objects.TX.Tax> calculationLevelComparer = this.GetTaxByCalculationLevelComparer();
      ExceptionExtensions.ThrowOnNull<IComparer<PX.Objects.TX.Tax>>(calculationLevelComparer, "taxComparer", (string) null);
      foreach (PXResult<PX.Objects.TX.Tax, TaxRev> pxResult in PXSelectBase<PX.Objects.TX.Tax, PXSelectReadonly2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<boolFalse>, And<TaxRev.taxType, Equal<TaxType.sales>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.use>, And<PX.Objects.TX.Tax.reverseTax, Equal<boolFalse>, And<Current<CROpportunity.documentDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>>>, Where>.Config>.SelectMultiBound(graph, objArray, parameters))
        dictionary[PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult).TaxID] = pxResult;
      List<object> objectList = new List<object>();
      switch (taxchk)
      {
        case PXTaxCheck.Line:
          foreach (PXResult<CROpportunityTax> pxResult1 in PXSelectBase<CROpportunityTax, PXSelect<CROpportunityTax, Where<CROpportunityTax.quoteID, Equal<Current<CROpportunity.quoteNoteID>>, And<CROpportunityTax.lineNbr, Equal<Current<CROpportunityProducts.lineNbr>>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
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
          foreach (PXResult<CROpportunityTax> pxResult3 in PXSelectBase<CROpportunityTax, PXSelect<CROpportunityTax, Where<CROpportunityTax.quoteID, Equal<Current<CROpportunity.quoteNoteID>>, And<CROpportunityTax.lineNbr, Less<intMax>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
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
          foreach (PXResult<CRTaxTran> pxResult5 in PXSelectBase<CRTaxTran, PXSelect<CRTaxTran, Where<CRTaxTran.quoteID, Equal<Current<CROpportunity.quoteNoteID>>>, OrderBy<Asc<CRTaxTran.lineNbr, Asc<CRTaxTran.taxID>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
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
      return GraphHelper.RowCast<CROpportunityProducts>((IEnumerable) PXSelectBase<CROpportunityProducts, PXSelect<CROpportunityProducts, Where<CROpportunityProducts.quoteID, Equal<Current<CROpportunity.quoteNoteID>>>>.Config>.SelectMultiBound(graph, new object[1]
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
  public class PerUnitTaxDisableExt : PerUnitTaxDataEntryGraphExtension<OpportunityMaint, CRTaxTran>
  {
    public static bool IsActive()
    {
      return PerUnitTaxDataEntryGraphExtension<OpportunityMaint, CRTaxTran>.IsActiveBase();
    }
  }

  /// <exclude />
  public class ContactAddress : CROpportunityContactAddressExt<OpportunityMaint>
  {
    public virtual void Initialize()
    {
      ((PXGraphExtension) this).Initialize();
      this.Addresses = new PXSelectExtension<PX.Objects.CR.Extensions.CROpportunityContactAddress.DocumentAddress>((PXSelectBase) this.Base.Opportunity_Address);
      this.Contacts = new PXSelectExtension<PX.Objects.CR.Extensions.CROpportunityContactAddress.DocumentContact>((PXSelectBase) this.Base.Opportunity_Contact);
    }

    protected override CROpportunityContactAddressExt<OpportunityMaint>.DocumentMapping GetDocumentMapping()
    {
      return new CROpportunityContactAddressExt<OpportunityMaint>.DocumentMapping(typeof (CROpportunity))
      {
        DocumentAddressID = typeof (CROpportunity.opportunityAddressID),
        DocumentContactID = typeof (CROpportunity.opportunityContactID),
        ShipAddressID = typeof (CROpportunity.shipAddressID),
        ShipContactID = typeof (CROpportunity.shipContactID),
        BillAddressID = typeof (CROpportunity.billAddressID),
        BillContactID = typeof (CROpportunity.billContactID)
      };
    }

    protected override CROpportunityContactAddressExt<OpportunityMaint>.DocumentContactMapping GetDocumentContactMapping()
    {
      return new CROpportunityContactAddressExt<OpportunityMaint>.DocumentContactMapping(typeof (CRContact))
      {
        EMail = typeof (CRContact.email)
      };
    }

    protected override CROpportunityContactAddressExt<OpportunityMaint>.DocumentAddressMapping GetDocumentAddressMapping()
    {
      return new CROpportunityContactAddressExt<OpportunityMaint>.DocumentAddressMapping(typeof (CRAddress));
    }

    protected override PXCache GetContactCache()
    {
      return ((PXSelectBase) this.Base.Opportunity_Contact).Cache;
    }

    protected override PXCache GetAddressCache()
    {
      return ((PXSelectBase) this.Base.Opportunity_Address).Cache;
    }

    protected override PXCache GetBillingContactCache()
    {
      return !(((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current?.PrimaryQuoteType == "D") ? (PXCache) null : ((PXSelectBase) this.Base.Billing_Contact).Cache;
    }

    protected override PXCache GetBillingAddressCache()
    {
      return !(((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current?.PrimaryQuoteType == "D") ? (PXCache) null : ((PXSelectBase) this.Base.Billing_Address).Cache;
    }

    protected override PXCache GetShippingContactCache()
    {
      return ((PXSelectBase) this.Base.Shipping_Contact).Cache;
    }

    protected override PXCache GetShippingAddressCache()
    {
      return ((PXSelectBase) this.Base.Shipping_Address).Cache;
    }

    protected override IPersonalContact SelectContact()
    {
      return (IPersonalContact) ((PXSelectBase<CRContact>) this.Base.Opportunity_Contact).SelectSingle(Array.Empty<object>());
    }

    protected override IPersonalContact SelectBillingContact()
    {
      return (IPersonalContact) ((PXSelectBase<CRBillingContact>) this.Base.Billing_Contact).SelectSingle(Array.Empty<object>());
    }

    protected override IPersonalContact SelectShippingContact()
    {
      return (IPersonalContact) ((PXSelectBase<CRShippingContact>) this.Base.Shipping_Contact).SelectSingle(Array.Empty<object>());
    }

    protected override IAddress SelectAddress()
    {
      return (IAddress) ((PXSelectBase<CRAddress>) this.Base.Opportunity_Address).SelectSingle(Array.Empty<object>());
    }

    protected override IAddress SelectBillingAddress()
    {
      return (IAddress) ((PXSelectBase<CRBillingAddress>) this.Base.Billing_Address).SelectSingle(Array.Empty<object>());
    }

    protected override IAddress SelectShippingAddress()
    {
      return (IAddress) ((PXSelectBase<CRShippingAddress>) this.Base.Shipping_Address).SelectSingle(Array.Empty<object>());
    }

    protected override IPersonalContact GetEtalonContact()
    {
      return this.SafeGetEtalon(((PXSelectBase) this.Base.Opportunity_Contact).Cache) as IPersonalContact;
    }

    protected override IPersonalContact GetEtalonShippingContact()
    {
      return this.SafeGetEtalon(((PXSelectBase) this.Base.Shipping_Contact).Cache) as IPersonalContact;
    }

    protected override IPersonalContact GetEtalonBillingContact()
    {
      return this.SafeGetEtalon(((PXSelectBase) this.Base.Billing_Contact).Cache) as IPersonalContact;
    }

    protected override IAddress GetEtalonAddress()
    {
      return this.SafeGetEtalon(((PXSelectBase) this.Base.Opportunity_Address).Cache) as IAddress;
    }

    protected override IAddress GetEtalonShippingAddress()
    {
      return this.SafeGetEtalon(((PXSelectBase) this.Base.Shipping_Address).Cache) as IAddress;
    }

    protected override IAddress GetEtalonBillingAddress()
    {
      return this.SafeGetEtalon(((PXSelectBase) this.Base.Billing_Address).Cache) as IAddress;
    }

    protected override void _(
      PX.Data.Events.FieldUpdated<PX.Objects.CR.Extensions.CROpportunityContactAddress.Document, PX.Objects.CR.Extensions.CROpportunityContactAddress.Document.bAccountID> e)
    {
      if (e.Row == null)
      {
        base._(e);
      }
      else
      {
        CROpportunity crOpportunity = e.Row.Base as CROpportunity;
        ((PXSelectBase) this.Base.Opportunity).Cache.SetDefaultExt<CROpportunity.locationID>((object) crOpportunity);
        ((PXSelectBase) this.Base.Opportunity).Cache.SetDefaultExt<CROpportunity.taxCalcMode>((object) crOpportunity);
        foreach (CROpportunityRevision opportunityRevision in GraphHelper.RowCast<CROpportunityRevision>((IEnumerable) PXSelectBase<CROpportunityRevision, PXSelect<CROpportunityRevision, Where<CROpportunityRevision.opportunityID, Equal<Current<CROpportunity.opportunityID>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())))
        {
          ((PXSelectBase) this.Base.OpportunityRevision).Cache.SetValueExt<CROpportunityRevision.bAccountID>((object) opportunityRevision, (object) crOpportunity.BAccountID);
          ((PXSelectBase) this.Base.OpportunityRevision).Cache.SetDefaultExt<CROpportunityRevision.locationID>((object) opportunityRevision);
        }
        base._(e);
        bool flag = crOpportunity.AllowOverrideContactAddress.GetValueOrDefault() || !crOpportunity.BAccountID.HasValue && !crOpportunity.ContactID.HasValue;
        ((PXSelectBase) this.Base.Opportunity).Cache.SetValueExt<CROpportunity.allowOverrideContactAddress>((object) crOpportunity, (object) flag);
      }
    }

    protected override void _(
      PX.Data.Events.FieldUpdated<PX.Objects.CR.Extensions.CROpportunityContactAddress.Document, PX.Objects.CR.Extensions.CROpportunityContactAddress.Document.contactID> e)
    {
      base._(e);
      if (e.Row == null)
        return;
      CROpportunityRevision opportunityRevision = ((PXSelectBase<CROpportunityRevision>) new PXSelectJoin<CROpportunityRevision, InnerJoin<CRQuote, On<CRQuote.quoteID, Equal<CROpportunityRevision.noteID>>>, Where<CROpportunityRevision.opportunityID, Equal<Current<CROpportunity.opportunityID>>, And<CRQuote.quoteID, Equal<CRQuote.defQuoteID>>>>((PXGraph) this.Base)).SelectSingle(Array.Empty<object>());
      if (opportunityRevision != null)
        ((PXSelectBase) this.Base.OpportunityRevision).Cache.SetValueExt<CROpportunityRevision.contactID>((object) opportunityRevision, (object) e.Row.ContactID);
      if (((PXSelectBase) this.Documents).Cache.GetStatus((object) e.Row) != 1)
        return;
      bool flag = e.Row.AllowOverrideContactAddress.GetValueOrDefault() || !e.Row.BAccountID.HasValue && !e.Row.ContactID.HasValue;
      ((PXSelectBase) this.Documents).Cache.SetValueExt<PX.Objects.CR.Extensions.CROpportunityContactAddress.Document.allowOverrideContactAddress>((object) e.Row, (object) flag);
    }
  }

  /// <exclude />
  public class DefaultOpportunityOwner : 
    CRDefaultDocumentOwner<OpportunityMaint, CROpportunity, CROpportunity.classID, CROpportunity.ownerID, CROpportunity.workgroupID>
  {
  }

  /// <exclude />
  public class CreateBothAccountAndContactFromOpportunityGraphExt : 
    CRCreateBothContactAndAccountAction<OpportunityMaint, CROpportunity, OpportunityMaint.CreateAccountFromOpportunityGraphExt, OpportunityMaint.CreateContactFromOpportunityGraphExt>
  {
  }

  /// <exclude />
  public class CreateAccountFromOpportunityGraphExt : 
    CRCreateAccountAction<OpportunityMaint, CROpportunity>
  {
    protected override string TargetType => "PX.Objects.CR.CROpportunity";

    public override void Initialize()
    {
      base.Initialize();
      this.Addresses = new PXSelectExtension<PX.Objects.CR.Extensions.CRCreateActions.DocumentAddress>((PXSelectBase) this.Base.Opportunity_Address);
      this.Contacts = new PXSelectExtension<PX.Objects.CR.Extensions.CRCreateActions.DocumentContact>((PXSelectBase) this.Base.Opportunity_Contact);
    }

    protected override CRCreateActionBaseInit<OpportunityMaint, CROpportunity>.DocumentMapping GetDocumentMapping()
    {
      return new CRCreateActionBaseInit<OpportunityMaint, CROpportunity>.DocumentMapping(typeof (CROpportunity))
      {
        RefContactID = typeof (CROpportunity.contactID)
      };
    }

    protected override CRCreateActionBaseInit<OpportunityMaint, CROpportunity>.DocumentContactMapping GetDocumentContactMapping()
    {
      return new CRCreateActionBaseInit<OpportunityMaint, CROpportunity>.DocumentContactMapping(typeof (CRContact));
    }

    protected override CRCreateActionBaseInit<OpportunityMaint, CROpportunity>.DocumentAddressMapping GetDocumentAddressMapping()
    {
      return new CRCreateActionBaseInit<OpportunityMaint, CROpportunity>.DocumentAddressMapping(typeof (CRAddress));
    }

    protected override PXSelectBase<CRPMTimeActivity> Activities
    {
      get
      {
        return (PXSelectBase<CRPMTimeActivity>) ((PXGraph) this.Base).GetExtension<OpportunityMaint_ActivityDetailsExt>().Activities;
      }
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass> e)
    {
      BAccount baccount = ((PXSelectBase<BAccount>) this.ExistingAccount).SelectSingle(Array.Empty<object>());
      if (baccount != null)
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass>, AccountsFilter, object>) e).NewValue = (object) baccount.ClassID;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass>>) e).Cancel = true;
      }
      else
      {
        CROpportunity current = ((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current;
        if (current == null)
          return;
        CROpportunityClass opportunityClass = PXResultset<CROpportunityClass>.op_Implicit(PXSelectBase<CROpportunityClass, PXSelect<CROpportunityClass, Where<CROpportunityClass.cROpportunityClassID, Equal<Required<CROpportunity.classID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) current.ClassID
        }));
        if (opportunityClass != null && opportunityClass.TargetBAccountClassID != null)
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass>, AccountsFilter, object>) e).NewValue = (object) opportunityClass.TargetBAccountClassID;
        else
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass>, AccountsFilter, object>) e).NewValue = (object) ((PXSelectBase<CRSetup>) this.Base.Setup).Current?.DefaultCustomerClassID;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountClass>>) e).Cancel = true;
      }
    }

    protected override void _(PX.Data.Events.RowSelected<AccountsFilter> e)
    {
      base._(e);
      AccountsFilter row = e.Row;
      if (row == null || !((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current.ContactID.HasValue)
        return;
      PXUIFieldAttribute.SetVisible<AccountsFilter.linkContactToAccount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AccountsFilter>>) e).Cache, (object) row, true);
      Contact contact = ((PXSelectBase<Contact>) this.Base.CurrentContact).Current ?? ((PXSelectBase<Contact>) this.Base.CurrentContact).SelectSingle(Array.Empty<object>());
      if (contact == null)
        PXUIFieldAttribute.SetEnabled<AccountsFilter.linkContactToAccount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AccountsFilter>>) e).Cache, (object) row, false);
      else if (contact.BAccountID.HasValue)
        PXUIFieldAttribute.SetWarning<AccountsFilter.linkContactToAccount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AccountsFilter>>) e).Cache, (object) row, "Contact linked to another Business Account");
      else
        PXUIFieldAttribute.SetEnabled<AccountsFilter.linkContactToAccount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<AccountsFilter>>) e).Cache, (object) row, true);
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.linkContactToAccount> e)
    {
      if (e.Row == null)
        return;
      int? nullable = ((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current.ContactID;
      if (nullable.HasValue)
      {
        Contact contact = ((PXSelectBase<Contact>) this.Base.CurrentContact).Current ?? ((PXSelectBase<Contact>) this.Base.CurrentContact).SelectSingle(Array.Empty<object>());
        if (contact == null)
        {
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.linkContactToAccount>, AccountsFilter, object>) e).NewValue = (object) false;
        }
        else
        {
          nullable = contact.BAccountID;
          if (nullable.HasValue)
            ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.linkContactToAccount>, AccountsFilter, object>) e).NewValue = (object) false;
          else
            ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.linkContactToAccount>, AccountsFilter, object>) e).NewValue = (object) true;
        }
      }
      else
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.linkContactToAccount>, AccountsFilter, object>) e).NewValue = (object) false;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<AccountsFilter, AccountsFilter.linkContactToAccount>>) e).Cancel = true;
    }
  }

  /// <exclude />
  public class CreateContactFromOpportunityGraphExt : 
    CRCreateContactAction<OpportunityMaint, CROpportunity>
  {
    protected override string TargetType => "PX.Objects.CR.CROpportunity";

    public override void Initialize()
    {
      base.Initialize();
      this.Addresses = new PXSelectExtension<PX.Objects.CR.Extensions.CRCreateActions.DocumentAddress>((PXSelectBase) this.Base.Opportunity_Address);
      this.Contacts = new PXSelectExtension<PX.Objects.CR.Extensions.CRCreateActions.DocumentContact>((PXSelectBase) this.Base.Opportunity_Contact);
      this.ContactMethod = new PXSelectExtension<DocumentContactMethod>((PXSelectBase) this.Base.Opportunity_Contact);
    }

    protected override CRCreateActionBaseInit<OpportunityMaint, CROpportunity>.DocumentMapping GetDocumentMapping()
    {
      return new CRCreateActionBaseInit<OpportunityMaint, CROpportunity>.DocumentMapping(typeof (CROpportunity))
      {
        RefContactID = typeof (CROpportunity.contactID)
      };
    }

    protected override CRCreateActionBaseInit<OpportunityMaint, CROpportunity>.DocumentContactMapping GetDocumentContactMapping()
    {
      return new CRCreateActionBaseInit<OpportunityMaint, CROpportunity>.DocumentContactMapping(typeof (CRContact));
    }

    protected override CRCreateContactAction<OpportunityMaint, CROpportunity>.DocumentContactMethodMapping GetDocumentContactMethodMapping()
    {
      return new CRCreateContactAction<OpportunityMaint, CROpportunity>.DocumentContactMethodMapping(typeof (CRContact));
    }

    protected override CRCreateActionBaseInit<OpportunityMaint, CROpportunity>.DocumentAddressMapping GetDocumentAddressMapping()
    {
      return new CRCreateActionBaseInit<OpportunityMaint, CROpportunity>.DocumentAddressMapping(typeof (CRAddress));
    }

    protected override PXSelectBase<CRPMTimeActivity> Activities
    {
      get
      {
        return (PXSelectBase<CRPMTimeActivity>) ((PXGraph) this.Base).GetExtension<OpportunityMaint_ActivityDetailsExt>().Activities;
      }
    }

    protected virtual void _(
      PX.Data.Events.FieldDefaulting<ContactFilter, ContactFilter.contactClass> e)
    {
      Contact contact = ((PXSelectBase<Contact>) this.ExistingContact).SelectSingle(Array.Empty<object>());
      if (contact != null)
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ContactFilter, ContactFilter.contactClass>, ContactFilter, object>) e).NewValue = (object) contact.ClassID;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ContactFilter, ContactFilter.contactClass>>) e).Cancel = true;
      }
      else
      {
        CROpportunity current = ((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current;
        if (current == null)
          return;
        CROpportunityClass opportunityClass = PXResultset<CROpportunityClass>.op_Implicit(PXSelectBase<CROpportunityClass, PXSelect<CROpportunityClass, Where<CROpportunityClass.cROpportunityClassID, Equal<Required<CROpportunity.classID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) current.ClassID
        }));
        if (opportunityClass != null && opportunityClass.TargetContactClassID != null)
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ContactFilter, ContactFilter.contactClass>, ContactFilter, object>) e).NewValue = (object) opportunityClass.TargetContactClassID;
        else
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ContactFilter, ContactFilter.contactClass>, ContactFilter, object>) e).NewValue = (object) ((PXSelectBase<CRSetup>) this.Base.Setup).Current?.DefaultContactClassID;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ContactFilter, ContactFilter.contactClass>>) e).Cancel = true;
      }
    }

    public override void _(PX.Data.Events.RowSelected<ContactFilter> e)
    {
      base._(e);
      OpportunityMaint opportunityMaint = this.Base;
      int num;
      if (opportunityMaint == null)
      {
        num = 0;
      }
      else
      {
        PXSelectJoin<CROpportunity, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CROpportunity.bAccountID>>>, Where<BAccount.bAccountID, IsNull, Or<Match<BAccount, Current<AccessInfo.userName>>>>> opportunity = opportunityMaint.Opportunity;
        if (opportunity == null)
        {
          num = 0;
        }
        else
        {
          CROpportunity current = ((PXSelectBase<CROpportunity>) opportunity).Current;
          num = current != null ? (current.BAccountID.HasValue ? 1 : 0) : 0;
        }
      }
      bool flag = num != 0;
      PXUIFieldAttribute.SetReadOnly<ContactFilter.fullName>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<ContactFilter>>) e).Cache, (object) e.Row, flag);
    }

    protected override void MapContactMethod(DocumentContactMethod source, Contact target)
    {
    }

    protected override object GetDefaultFieldValueFromCache<TExistingField, TField>()
    {
      if (!(typeof (TExistingField) == typeof (Contact.fullName)))
      {
        OpportunityMaint opportunityMaint1 = this.Base;
        int num;
        if (opportunityMaint1 == null)
        {
          num = 1;
        }
        else
        {
          PXSelectJoin<CROpportunity, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CROpportunity.bAccountID>>>, Where<BAccount.bAccountID, IsNull, Or<Match<BAccount, Current<AccessInfo.userName>>>>> opportunity = opportunityMaint1.Opportunity;
          if (opportunity == null)
          {
            num = 1;
          }
          else
          {
            CROpportunity current = ((PXSelectBase<CROpportunity>) opportunity).Current;
            num = current != null ? (!current.BAccountID.HasValue ? 1 : 0) : 1;
          }
        }
        if (num == 0)
        {
          OpportunityMaint opportunityMaint2 = this.Base;
          if ((opportunityMaint2 != null ? (((bool?) ((PXSelectBase<CROpportunity>) opportunityMaint2.Opportunity)?.Current?.AllowOverrideContactAddress).GetValueOrDefault() ? 1 : 0) : 0) == 0)
            return (object) null;
        }
      }
      return base.GetDefaultFieldValueFromCache<TExistingField, TField>();
    }
  }

  /// <exclude />
  public class CRCreateSalesOrderExt : 
    PX.Objects.CR.Extensions.CRCreateSalesOrder.CRCreateSalesOrder<OpportunityMaint.Discount, OpportunityMaint, CROpportunity>
  {
    public static bool IsActive()
    {
      return PX.Objects.CR.Extensions.CRCreateSalesOrder.CRCreateSalesOrder<OpportunityMaint.Discount, OpportunityMaint, CROpportunity>.IsExtensionActive();
    }

    protected override PX.Objects.CR.Extensions.CRCreateSalesOrder.CRCreateSalesOrder<OpportunityMaint.Discount, OpportunityMaint, CROpportunity>.DocumentMapping GetDocumentMapping()
    {
      return new PX.Objects.CR.Extensions.CRCreateSalesOrder.CRCreateSalesOrder<OpportunityMaint.Discount, OpportunityMaint, CROpportunity>.DocumentMapping(typeof (CROpportunity))
      {
        QuoteID = typeof (CROpportunity.quoteNoteID)
      };
    }

    public virtual void _(PX.Data.Events.RowSelected<CROpportunity> e)
    {
      CROpportunity row = e.Row;
      if (row == null)
        return;
      bool flag1 = e.Row.BAccountID.HasValue;
      if (flag1)
      {
        flag1 = NonGenericIEnumerableExtensions.Any_((IEnumerable) ((PXSelectBase<CROpportunityProducts>) this.Base.Products).Select(Array.Empty<object>()));
        if (flag1)
        {
          CRQuote crQuote = ((PXSelectBase<CRQuote>) this.Base.PrimaryQuoteQuery).SelectSingle(Array.Empty<object>());
          IEnumerable<CROpportunityProducts> source = GraphHelper.RowCast<CROpportunityProducts>((IEnumerable) ((PXSelectBase) this.Base.Products).View.SelectMultiBound(new object[1]
          {
            (object) row
          }, Array.Empty<object>()));
          int num = !source.Any<CROpportunityProducts>((Func<CROpportunityProducts, bool>) (_ => !_.InventoryID.HasValue)) ? 0 : (!source.Any<CROpportunityProducts>((Func<CROpportunityProducts, bool>) (_ => _.InventoryID.HasValue)) ? 1 : 0);
          bool flag2 = crQuote != null;
          flag1 = num == 0 && (!flag2 || crQuote.Status == "A" || crQuote.Status == "S" || crQuote.Status == "T" || crQuote.Status == "D") && (!flag2 || crQuote.QuoteType == "D");
        }
      }
      ((PXAction) this.CreateSalesOrder).SetEnabled(flag1);
    }

    public override CRQuote GetQuoteForWorkflowProcessing()
    {
      return ((PXSelectBase<CRQuote>) this.Base.PrimaryQuoteQuery).SelectSingle(Array.Empty<object>());
    }
  }

  /// <exclude />
  public class CRCreateInvoiceExt : 
    PX.Objects.CR.Extensions.CRCreateInvoice.CRCreateInvoice<OpportunityMaint.Discount, OpportunityMaint, CROpportunity>
  {
    public static bool IsActive()
    {
      return PX.Objects.CR.Extensions.CRCreateInvoice.CRCreateInvoice<OpportunityMaint.Discount, OpportunityMaint, CROpportunity>.IsExtensionActive();
    }

    protected override PX.Objects.CR.Extensions.CRCreateInvoice.CRCreateInvoice<OpportunityMaint.Discount, OpportunityMaint, CROpportunity>.DocumentMapping GetDocumentMapping()
    {
      return new PX.Objects.CR.Extensions.CRCreateInvoice.CRCreateInvoice<OpportunityMaint.Discount, OpportunityMaint, CROpportunity>.DocumentMapping(typeof (CROpportunity))
      {
        QuoteID = typeof (CROpportunity.quoteNoteID)
      };
    }

    public virtual void _(PX.Data.Events.RowSelected<CROpportunity> e)
    {
      CROpportunity row = e.Row;
      if (row == null)
        return;
      bool flag1 = e.Row.BAccountID.HasValue;
      if (flag1)
      {
        flag1 = NonGenericIEnumerableExtensions.Any_((IEnumerable) ((PXSelectBase<CROpportunityProducts>) this.Base.Products).Select(Array.Empty<object>()));
        if (flag1)
        {
          CRQuote crQuote = ((PXSelectBase<CRQuote>) this.Base.PrimaryQuoteQuery).SelectSingle(Array.Empty<object>());
          IEnumerable<CROpportunityProducts> source = GraphHelper.RowCast<CROpportunityProducts>((IEnumerable) ((PXSelectBase) this.Base.Products).View.SelectMultiBound(new object[1]
          {
            (object) row
          }, Array.Empty<object>()));
          int num = !source.Any<CROpportunityProducts>((Func<CROpportunityProducts, bool>) (_ => !_.InventoryID.HasValue)) ? 0 : (!source.Any<CROpportunityProducts>((Func<CROpportunityProducts, bool>) (_ => _.InventoryID.HasValue)) ? 1 : 0);
          bool flag2 = crQuote != null;
          flag1 = num == 0 && (!flag2 || crQuote.Status == "A" || crQuote.Status == "S" || crQuote.Status == "T" || crQuote.Status == "D") && (!flag2 || crQuote.QuoteType == "D");
        }
      }
      ((PXAction) this.CreateInvoice).SetEnabled(flag1);
    }

    public override CRQuote GetQuoteForWorkflowProcessing()
    {
      return ((PXSelectBase<CRQuote>) this.Base.PrimaryQuoteQuery).SelectSingle(Array.Empty<object>());
    }
  }

  /// <exclude />
  public class OpportunityMaintAddressLookupExtension : 
    AddressLookupExtension<OpportunityMaint, CROpportunity, CRAddress>
  {
    protected override string AddressView => "Opportunity_Address";

    protected override string ViewOnMap => "viewMainOnMap";
  }

  /// <exclude />
  public class OpportunityMaintShippingAddressLookupExtension : 
    AddressLookupExtension<OpportunityMaint, CROpportunity, CRShippingAddress>
  {
    protected override string AddressView => "Shipping_Address";

    protected override string ViewOnMap => "ViewShippingOnMap";
  }

  /// <exclude />
  public class OpportunityMaintBillingAddressLookupExtension : 
    AddressLookupExtension<OpportunityMaint, CROpportunity, CRBillingAddress>
  {
    protected override string AddressView => "Billing_Address";

    protected override string ViewOnMap => "ViewBillingOnMap";

    public override void _(PX.Data.Events.RowSelected<CROpportunity> e)
    {
      base._(e);
      if (e.Row == null)
        return;
      bool flag = e.Row.PrimaryQuoteType == "D";
      ((PXAction) this.Base.ViewBillingOnMap).SetVisible(((PXAction) this.Base.ViewBillingOnMap).GetVisible() & flag);
      PXAction action = ((PXGraph) this.Base).Actions[this.ActionName];
      action.SetVisible(action.GetVisible() & flag);
    }
  }

  /// <exclude />
  public class ExtensionSort : 
    SortExtensionsBy<TypeArrayOf<PXGraphExtension<OpportunityMaint>>.FilledWith<OpportunityMaint.ContactAddress, OpportunityMaint.MultiCurrency, OpportunityMaint.SalesPrice, OpportunityMaint.Discount, OpportunityMaint.SalesTax>>
  {
  }
}
