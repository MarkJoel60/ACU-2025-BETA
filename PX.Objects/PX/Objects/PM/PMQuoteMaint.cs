// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMQuoteMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Autofac;
using PX.Api.Models;
using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.SQLTree;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CR.Extensions.CROpportunityContactAddress;
using PX.Objects.CR.Standalone;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.EP;
using PX.Objects.Extensions.Discount;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.Extensions.PerUnitTax;
using PX.Objects.Extensions.SalesPrice;
using PX.Objects.Extensions.SalesTax;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.IN.Attributes;
using PX.Objects.SO;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Compilation;

#nullable enable
namespace PX.Objects.PM;

public class PMQuoteMaint : 
  PXGraph<
  #nullable disable
  PMQuoteMaint, PMQuote>,
  PXImportAttribute.IPXPrepareItems,
  PXImportAttribute.IPXProcess
{
  private const string DefaultTaskCD = "0";
  private string PersistingTaskCD;
  private readonly Dictionary<string, string> PersistingTaskMap = new Dictionary<string, string>();
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSetupOptional<SOSetup> sosetup;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSetup<PMSetup> Setup;
  [PXViewName("Project Quote")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (PMQuote.quoteNbr), typeof (PMQuote.isPrimary), typeof (PMQuote.status), typeof (PMQuote.documentDate), typeof (PMQuote.expirationDate), typeof (PMQuote.quoteProjectCD), typeof (PMQuote.curyAmount), typeof (PMQuote.curyCostTotal), typeof (PMQuote.curyGrossMarginAmount), typeof (PMQuote.grossMarginPct), typeof (PMQuote.curyTaxTotal), typeof (PMQuote.curyQuoteTotal), typeof (PMQuote.approved), typeof (PMQuote.rejected)})]
  public FbqlSelect<SelectFromBase<PMQuote, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PMProject>.On<BqlOperand<
  #nullable enable
  PMProject.contractID, IBqlInt>.IsEqual<
  #nullable disable
  PMQuote.quoteProjectID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMQuote.quoteProjectID, 
  #nullable disable
  IsNull>>>>.Or<MatchUserFor<PMProject>>>, PMQuote>.View Quote;
  [PXHidden]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (PMQuote.createdByID), typeof (PMQuote.approved), typeof (PMQuote.rejected)})]
  public PXSelect<PMQuote, Where<PMQuote.quoteID, Equal<Current<PMQuote.quoteID>>>> QuoteCurrent;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.CR.Address> Address;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSetup<PX.Objects.CR.Contact>.Where<BqlOperand<
  #nullable enable
  PX.Objects.CR.Contact.contactID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMQuote.contactID, IBqlInt>.AsOptional>> Contacts;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public 
  #nullable disable
  PXSetup<PX.Objects.AR.Customer>.Where<BqlOperand<
  #nullable enable
  PX.Objects.AR.Customer.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMQuote.bAccountID, IBqlInt>.AsOptional>> customer;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public 
  #nullable disable
  PXSetup<PX.Objects.CR.BAccount>.Where<BqlOperand<
  #nullable enable
  PX.Objects.CR.BAccount.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMQuote.bAccountID, IBqlInt>.AsOptional>> baccount;
  [PXViewName("Tasks")]
  [PXImport(typeof (PMQuote))]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (PMQuoteTask.plannedStartDate), typeof (PMQuoteTask.plannedEndDate)})]
  public 
  #nullable disable
  PXSelect<PMQuoteTask, Where<PMQuoteTask.quoteID, Equal<Current<PMQuote.quoteID>>>> Tasks;
  [PXViewName("Estimates")]
  [PXImport(typeof (PMQuote))]
  public ProductLinesSelect Products;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<CROpportunityRevision> FakeRevisionCache;
  [PXCopyPasteHiddenView]
  public PXSelect<CROpportunityTax, Where<CROpportunityTax.quoteID, Equal<Current<PMQuote.quoteID>>, And<CROpportunityTax.lineNbr, Less<intMax>>>, OrderBy<Asc<CROpportunityTax.taxID>>> TaxLines;
  [PXViewName("Quote Tax")]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<CRTaxTran, InnerJoin<PX.Objects.TX.Tax, On<PX.Objects.TX.Tax.taxID, Equal<CRTaxTran.taxID>>>, Where<CRTaxTran.quoteID, Equal<Current<PMQuote.quoteID>>>, OrderBy<Asc<CRTaxTran.lineNbr, Asc<CRTaxTran.taxID>>>> Taxes;
  [PXCopyPasteHiddenView]
  public PXSetup<PX.Objects.CR.Location>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.CR.Location.bAccountID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PMQuote.bAccountID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PX.Objects.CR.Location.locationID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMQuote.locationID, IBqlInt>.FromCurrent>>> location;
  [PXViewName("Quote Contact")]
  public 
  #nullable disable
  PXSelect<CRContact, Where<CRContact.contactID, Equal<Current<PMQuote.opportunityContactID>>>> Quote_Contact;
  [PXViewName("Quote Address")]
  public PXSelect<CRAddress, Where<CRAddress.addressID, Equal<Current<PMQuote.opportunityAddressID>>>> Quote_Address;
  [PXViewName("Shipping Contact")]
  public PXSelect<CRShippingContact, Where<CRShippingContact.contactID, Equal<Current<PMQuote.shipContactID>>>> Shipping_Contact;
  [PXViewName("Shipping Address")]
  public PXSelect<CRShippingAddress, Where<CRShippingAddress.addressID, Equal<Current<PMQuote.shipAddressID>>>> Shipping_Address;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<PX.Objects.CR.Contact, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Contact.defAddressID, Equal<PX.Objects.CR.Address.addressID>>>, Where<PX.Objects.CR.Contact.contactID, Equal<Current<PMQuote.contactID>>>> CurrentContact;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<PX.Objects.CR.Standalone.CROpportunity, LeftJoin<CROpportunityRevision, On<CROpportunityRevision.noteID, Equal<PX.Objects.CR.Standalone.CROpportunity.defQuoteID>>, LeftJoin<PX.Objects.CR.Standalone.CRQuote, On<PX.Objects.CR.Standalone.CRQuote.quoteID, Equal<CROpportunityRevision.noteID>>>>, Where<PX.Objects.CR.Standalone.CROpportunity.opportunityID, Equal<Optional<PMQuote.opportunityID>>>> Opportunity;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelectReadonly<PMQuote, Where<PMQuote.quoteID, Equal<Required<PMQuote.quoteID>>>> QuoteInDb;
  [PXHidden]
  public PXSelect<PX.Objects.AP.Vendor> Vendors;
  [PXViewName("Approval")]
  public EPApprovalAutomation<PMQuote, PMQuote.approved, PMQuote.rejected, PMQuote.hold, PMSetupQuoteApproval> Approval;
  [PXViewName("Copy Quote")]
  [PXCopyPasteHiddenView]
  public PXFilter<PMQuoteMaint.CopyQuoteFilter> CopyQuoteInfo;
  [PXViewName("Quote Answers")]
  public CRAttributeList<PMQuote> Answers;
  [PXViewName("Convert to Project")]
  [PXCopyPasteHiddenView]
  public PXFilter<PMQuoteMaint.ConvertToProjectFilter> ConvertQuoteInfo;
  [PXCopyPasteHiddenView]
  public PXSelect<CROpportunityDiscountDetail, Where<CROpportunityDiscountDetail.quoteID, Equal<Current<PMQuote.quoteID>>>, OrderBy<Asc<CROpportunityDiscountDetail.lineNbr>>> _DiscountDetails;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelectJoin<NotificationSource, InnerJoin<PMProject, On<NotificationSource.refNoteID, Equal<PMProject.noteID>>>, Where<PMProject.contractID, Equal<Current<PMQuote.templateID>>>> NotificationSources;
  [PXCopyPasteHiddenView]
  public PXSetup<EPSetup> epsetup;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<PMProject> DummyProject;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<NoteDoc, Where<NoteDoc.noteID, Equal<Current<PMQuote.noteID>>>> QuoteNotes;
  [PXCopyPasteHiddenView]
  [PXFilterable(new System.Type[] {})]
  public FbqlSelect<SelectFromBase<ProjectEntry.SelectedTask, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PMProject>.On<BqlOperand<
  #nullable enable
  ProjectEntry.SelectedTask.projectID, IBqlInt>.IsEqual<
  #nullable disable
  PMProject.contractID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMTask.autoIncludeInPrj, 
  #nullable disable
  NotEqual<True>>>>, And<BqlOperand<
  #nullable enable
  ProjectEntry.SelectedTask.projectID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMQuote.templateID, IBqlInt>.FromCurrent>>>>.Or<
  #nullable disable
  BqlOperand<
  #nullable enable
  PMProject.nonProject, IBqlBool>.IsEqual<
  #nullable disable
  True>>>, ProjectEntry.SelectedTask>.View TasksForAddition;
  public PXAction<PMQuote> submit;
  public PXAction<PMQuote> editQuote;
  public PXAction<PMQuote> convertToProject;
  public PXAction<PMQuote> copyQuote;
  public PXAction<PMQuote> accept;
  public PXAction<PMQuote> decline;
  public PXAction<PMQuote> sendQuote;
  public PXAction<PMQuote> printQuote;
  public PXAction<PMQuote> primaryQuote;
  public PXAction<PMQuote> validateAddresses;
  public PXAction<PMQuote> viewMainOnMap;
  public PXAction<PMQuote> viewShippingOnMap;
  public PXAction<PMQuote> addCommonTasks;
  public PXAction<PMProject> addNewProjectTemplate;
  public PXAction<PMQuote> viewProject;

  [InjectDependency]
  public IUnitRateService RateService { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (PMQuote.curyInfoID))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.curyInfoID> e)
  {
  }

  [PXDBGuid(false, IsKey = true)]
  [PXDBDefault(typeof (PMQuote.quoteID))]
  [PXParent(typeof (Select<PMQuote, Where<PMQuote.quoteID, Equal<Current<CROpportunityProducts.quoteID>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.quoteID> e)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (PMQuote.productCntr))]
  [PXUIField(DisplayName = "Line Nbr.", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.lineNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (PMQuote.bAccountID))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.customerID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (CROpportunityProductLineTypeAttribute.scopeOfWork))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.lineType> e)
  {
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Price")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.manualPrice> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Discount Amount")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.curyDiscAmt> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Discount (%)")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.discPct> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Amount")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.curyAmount> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Discount Code", Visible = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.discountID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Manual Discount", Visible = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.manualDisc> e)
  {
  }

  [PXDBCurrency(typeof (CROpportunityProducts.curyInfoID), typeof (CROpportunityProducts.extCost))]
  [PXUIField(DisplayName = "Ext. Cost")]
  [PXFormula(typeof (Mult<CROpportunityProducts.quantity, CROpportunityProducts.curyUnitCost>), typeof (SumCalc<PMQuote.curyCostTotal>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.curyExtCost> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new System.Type[] {typeof (PMAccountGroup.groupCD)})]
  [PXSelector(typeof (Search<PMAccountGroup.groupID, Where<PMAccountGroup.isExpense, Equal<True>>>), SubstituteKey = typeof (PMAccountGroup.groupCD))]
  [PXUIField(DisplayName = "Cost Account Group")]
  [PXDBInt]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.expenseAccountGroupID> e)
  {
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new System.Type[] {typeof (PMAccountGroup.groupCD)})]
  [PXSelector(typeof (Search<PMAccountGroup.groupID, Where<PMAccountGroup.type, Equal<AccountType.income>>>), SubstituteKey = typeof (PMAccountGroup.groupCD))]
  [PXUIField(DisplayName = "Revenue Account Group")]
  [PXDBInt]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.revenueAccountGroupID> e)
  {
  }

  [PXDBString]
  [PXUIField(DisplayName = "Project Task")]
  [PXDimensionSelector("PROTASK", typeof (Search<PMQuoteTask.taskCD, Where<PMQuoteTask.quoteID, Equal<Current<PMQuote.quoteID>>>>), DirtyRead = true, DescriptionField = typeof (PMQuoteTask.description))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.taskCD> e)
  {
  }

  [ProjectTask(typeof (CROpportunityProducts.projectID), Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.taskID> e)
  {
  }

  [CostCode(null, typeof (CROpportunityProducts.taskID), "E", typeof (CROpportunityProducts.expenseAccountGroupID), true, false, InventoryField = typeof (CROpportunityProducts.inventoryID), UseNewDefaulting = true, SkipVerification = true, AllowNullValue = true, Filterable = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.costCodeID> e)
  {
  }

  [PopupMessage]
  [PXMergeAttributes]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.inventoryID> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CROpportunityProducts.customerID> e)
  {
    PMQuote current = ((PXSelectBase<PMQuote>) this.Quote).Current;
    if (current == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts.customerID>, object, object>) e).NewValue = (object) current.BAccountID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CROpportunityProducts.projectID> e)
  {
    PMQuote current = ((PXSelectBase<PMQuote>) this.Quote).Current;
    if (current == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts.projectID>, object, object>) e).NewValue = (object) current.QuoteProjectID;
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.contractCD> e)
  {
  }

  [PXDBGuid(false, IsKey = true)]
  [PXDBDefault(typeof (PMQuote.quoteID))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityDiscountDetail.quoteID> e)
  {
  }

  [PXDBUShort]
  [PXLineNbr(typeof (PMQuote))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityDiscountDetail.lineNbr> e)
  {
  }

  [PXDBGuid(false, IsKey = true)]
  [PXDBDefault(typeof (PMQuote.quoteID))]
  [PXParent(typeof (Select<PMQuote, Where<PMQuote.quoteID, Equal<Current<CROpportunityTax.quoteID>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<CROpportunityTax.quoteID> e)
  {
  }

  [PXDBLong]
  [CurrencyInfo(typeof (PMQuote.curyInfoID))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityTax.curyInfoID> e)
  {
  }

  [PXDBGuid(false, IsKey = true)]
  [PXDBDefault(typeof (PMQuote.quoteID))]
  [PXParent(typeof (Select<PMQuote, Where<PMQuote.quoteID, Equal<Current<CRTaxTran.quoteID>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<CRTaxTran.quoteID> e)
  {
  }

  [PXDBLong]
  [CurrencyInfo(typeof (PMQuote.curyInfoID))]
  protected virtual void _(PX.Data.Events.CacheAttached<CRTaxTran.curyInfoID> e)
  {
  }

  [CRMBAccount(new System.Type[] {typeof (BAccountType.prospectType), typeof (BAccountType.customerType), typeof (BAccountType.combinedType), typeof (BAccountType.vendorType)}, null, null, null)]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.bAccountID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (Search<PX.Objects.GL.Branch.countryID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  public virtual void _(PX.Data.Events.CacheAttached<CRAddress.countryID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Override Bill-To Address")]
  public virtual void _(PX.Data.Events.CacheAttached<CRAddress.overrideAddress> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Override Contact")]
  public virtual void _(PX.Data.Events.CacheAttached<CRContact.overrideContact> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault(typeof (Search<PX.Objects.GL.Branch.countryID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  public virtual void _(
    PX.Data.Events.CacheAttached<CRShippingAddress.countryID> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  public virtual void _(PX.Data.Events.CacheAttached<CRShippingAddress.latitude> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  public virtual void _(
    PX.Data.Events.CacheAttached<CRShippingAddress.longitude> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Override Project Address")]
  public virtual void _(
    PX.Data.Events.CacheAttached<CRShippingAddress.overrideAddress> e)
  {
  }

  [PXDefault(typeof (PMQuote.documentDate))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.docDate> e)
  {
  }

  [PXDefault(typeof (PMQuote.bAccountID))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.bAccountID> e)
  {
  }

  [PXDefault(typeof (PMQuote.ownerID))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.documentOwnerID> e)
  {
  }

  [PXDefault(typeof (PMQuote.subject))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.descr> e)
  {
  }

  [CurrencyInfo(typeof (PMQuote.curyInfoID))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.curyInfoID> e)
  {
  }

  [PXDefault(typeof (PMQuote.curyProductsAmount))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.curyTotalAmount> e)
  {
  }

  [PXDefault(typeof (PMQuote.productsAmount))]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<EPApproval.totalAmount> e)
  {
  }

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

  protected IEnumerable tasksForAddition()
  {
    List<ProjectEntry.SelectedTask> list1 = GraphHelper.RowCast<ProjectEntry.SelectedTask>((IEnumerable) PXSelectBase<ProjectEntry.SelectedTask, PXViewOf<ProjectEntry.SelectedTask>.BasedOn<SelectFromBase<ProjectEntry.SelectedTask, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<ProjectEntry.SelectedTask.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlOperand<PMProject.nonProject, IBqlBool>.IsEqual<True>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).ToList<ProjectEntry.SelectedTask>();
    if (((PXSelectBase<PMQuote>) this.Quote).Current.TemplateID.HasValue)
    {
      List<ProjectEntry.SelectedTask> list2 = GraphHelper.RowCast<ProjectEntry.SelectedTask>((IEnumerable) PXSelectBase<ProjectEntry.SelectedTask, PXViewOf<ProjectEntry.SelectedTask>.BasedOn<SelectFromBase<ProjectEntry.SelectedTask, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<ProjectEntry.SelectedTask.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.contractID, Equal<BqlField<PMQuote.templateID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<PMTask.autoIncludeInPrj, IBqlBool>.IsNotEqual<True>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).ToList<ProjectEntry.SelectedTask>();
      if (list2 != null)
        list1.AddRange((IEnumerable<ProjectEntry.SelectedTask>) list2);
    }
    HashSet<string> existingTasksCDs = GraphHelper.RowCast<PMQuoteTask>((IEnumerable) ((PXSelectBase<PMQuoteTask>) this.Tasks).Select(Array.Empty<object>())).Where<PMQuoteTask>((Func<PMQuoteTask, bool>) (task => !string.IsNullOrWhiteSpace(task.TaskCD))).Select<PMQuoteTask, string>((Func<PMQuoteTask, string>) (task => task.TaskCD.Trim().ToUpperInvariant())).Distinct<string>().ToHashSet<string>();
    return (IEnumerable) GraphHelper.RowCast<ProjectEntry.SelectedTask>((IEnumerable) list1).Where<ProjectEntry.SelectedTask>((Func<ProjectEntry.SelectedTask, bool>) (task => !existingTasksCDs.Contains(task.TaskCD.Trim().ToUpperInvariant())));
  }

  public PMQuoteMaint()
  {
    if (string.IsNullOrEmpty(((PXSelectBase<PMSetup>) this.Setup).Current.QuoteNumberingID))
      throw new PXSetPropertyException("The quote numbering sequence is not specified in the preferences of the Projects module.", new object[1]
      {
        (object) "Project Management Setup"
      });
    PXCache cach1 = ((PXGraph) this).Caches[typeof (PX.Objects.CR.BAccount)];
    PXCache cach2 = ((PXGraph) this).Caches[typeof (PMQuote)];
    if (cach2.Current is PMQuote current)
    {
      PXUIFieldAttribute.SetDisplayName<PX.Objects.CR.BAccount.acctCD>(cach1, "Business Account");
      PXUIFieldAttribute.SetDisplayName<PX.Objects.CR.BAccount.acctName>(cach1, "Business Account Name");
      PXUIFieldAttribute.SetEnabled<PMQuote.quoteProjectID>(cach2, (object) null, false);
      PXDefaultAttribute.SetPersistingCheck<PMQuote.branchID>(cach2, (object) null, (PXPersistingCheck) 2);
      PXDefaultAttribute.SetPersistingCheck<PMQuote.quoteProjectID>(cach2, (object) null, (PXPersistingCheck) 2);
      PXUIFieldAttribute.SetEnabled<PMQuote.locationID>(cach2, (object) current, current.BAccountID.HasValue);
      PXDefaultAttribute.SetPersistingCheck<PMQuote.locationID>(cach2, (object) current, !current.BAccountID.HasValue ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    }
    ((PXGraph) this).Views.Caches.Remove(typeof (PX.Objects.CR.Standalone.CROpportunity));
    ((PXGraph) this).Views.Caches.Remove(typeof (PX.Objects.CR.CROpportunity));
    PXUIFieldAttribute.SetVisible<PMQuote.opportunityID>(cach2, (object) null, PXAccess.FeatureInstalled<FeaturesSet.customerModule>());
    PXUIFieldAttribute.SetVisible<PMQuote.isPrimary>(cach2, (object) null, PXAccess.FeatureInstalled<FeaturesSet.customerModule>());
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Submit")]
  public virtual IEnumerable Submit(PXAdapter adapter)
  {
    PMQuoteMaint pmQuoteMaint = this;
    foreach (PMQuote pmQuote in adapter.Get<PMQuote>())
    {
      PXCache cach = ((PXGraph) pmQuoteMaint).Caches[typeof (PMQuote)];
      cach.SetValue<PMQuote.submitCancelled>((object) pmQuote, (object) false);
      if (pmQuoteMaint.Approval.GetAssignedMaps(pmQuote, ((PXSelectBase) pmQuoteMaint.QuoteCurrent).Cache).Any<PX.Objects.EP.ApprovalMap>())
        pmQuoteMaint.Approval.Assign(pmQuote, (IEnumerable<PX.Objects.EP.ApprovalMap>) pmQuoteMaint.Approval.GetAssignedMaps(pmQuote, ((PXSelectBase) pmQuoteMaint.QuoteCurrent).Cache));
      else
        cach.SetValue<PMQuote.approved>((object) pmQuote, (object) true);
      int productErrorsCount;
      if (!pmQuoteMaint.ValidateQuoteBeforeSubmit(pmQuote, out productErrorsCount))
      {
        cach.SetValue<PMQuote.submitCancelled>((object) pmQuote, (object) true);
        if (productErrorsCount > 0)
          throw new PXException("{0} '{1}' record raised at least one error. Please review the errors.", new object[2]
          {
            (object) "Updating ",
            (object) "Estimates"
          });
      }
      ((PXAction) pmQuoteMaint.Save).Press();
      yield return (object) pmQuote;
    }
  }

  [PXUIField(DisplayName = "Edit")]
  [PXButton]
  protected virtual IEnumerable EditQuote(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ConvertToProject(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    PMQuoteMaint.\u003C\u003Ec__DisplayClass92_0 cDisplayClass920 = new PMQuoteMaint.\u003C\u003Ec__DisplayClass92_0();
    List<PMQuote> project = new List<PMQuote>(adapter.Get().Cast<PXResult<PMQuote, PMProject>>().Select<PXResult<PMQuote, PMProject>, PMQuote>((Func<PXResult<PMQuote, PMProject>, PMQuote>) (qp => PXResult<PMQuote, PMProject>.op_Implicit(qp))));
    foreach (PMQuote row in project)
    {
      int productErrorsCount;
      if (!this.ValidateQuoteBeforeConvertToProject(row, out productErrorsCount) && productErrorsCount > 0)
        throw new PXException("Inserting 'Project' record raised at least one error. {0} out of {1} records contain errors. Please review the errors.", new object[2]
        {
          (object) productErrorsCount,
          (object) ((PXSelectBase<CROpportunityProducts>) this.Products).Select(Array.Empty<object>()).Count
        });
    }
    ((PXAction) this.Save).Press();
    if (((PXSelectBase) this.ConvertQuoteInfo).View.Answer == null)
    {
      ((PXSelectBase) this.ConvertQuoteInfo).Cache.Clear();
      (((PXSelectBase) this.ConvertQuoteInfo).Cache.Insert() as PMQuoteMaint.ConvertToProjectFilter).TaskCD = this.GetDefaultTaskCD();
    }
    if (!WebDialogResultExtension.IsPositive(((PXSelectBase<PMQuoteMaint.ConvertToProjectFilter>) this.ConvertQuoteInfo).AskExt()))
      return (IEnumerable) project;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass920.currentQuote = ((PXSelectBase<PMQuote>) this.Quote).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass920.currentInfo = ((PXSelectBase<PMQuoteMaint.ConvertToProjectFilter>) this.ConvertQuoteInfo).Current;
    foreach (PMQuote pmQuote in project)
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, cDisplayClass920.\u003C\u003E9__1 ?? (cDisplayClass920.\u003C\u003E9__1 = new PXToggleAsyncDelegate((object) cDisplayClass920, __methodptr(\u003CConvertToProject\u003Eb__1))));
    }
    return (IEnumerable) project;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CopyQuote(PXAdapter adapter)
  {
    List<PMQuote> pmQuoteList = new List<PMQuote>(adapter.Get().Cast<PXResult<PMQuote, PMProject>>().Select<PXResult<PMQuote, PMProject>, PMQuote>((Func<PXResult<PMQuote, PMProject>, PMQuote>) (r => PXResult<PMQuote, PMProject>.op_Implicit(r))));
    foreach (PMQuote pmQuote in pmQuoteList)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PMQuoteMaint.\u003C\u003Ec__DisplayClass94_0 cDisplayClass940 = new PMQuoteMaint.\u003C\u003Ec__DisplayClass94_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass940.quote = pmQuote;
      if (((PXSelectBase) this.CopyQuoteInfo).View.Answer == null)
      {
        ((PXSelectBase) this.CopyQuoteInfo).Cache.Clear();
        PMQuoteMaint.CopyQuoteFilter copyQuoteFilter = ((PXSelectBase) this.CopyQuoteInfo).Cache.Insert() as PMQuoteMaint.CopyQuoteFilter;
        // ISSUE: reference to a compiler-generated field
        copyQuoteFilter.Description = cDisplayClass940.quote.Subject + " - copy";
        copyQuoteFilter.RecalculatePrices = new bool?(false);
        copyQuoteFilter.RecalculateDiscounts = new bool?(false);
        copyQuoteFilter.OverrideManualPrices = new bool?(false);
        copyQuoteFilter.OverrideManualDiscounts = new bool?(false);
        copyQuoteFilter.OverrideManualDocGroupDiscounts = new bool?(false);
      }
      if (!WebDialogResultExtension.IsPositive(((PXSelectBase<PMQuoteMaint.CopyQuoteFilter>) this.CopyQuoteInfo).AskExt()))
        return (IEnumerable) pmQuoteList;
      ((PXAction) this.Save).Press();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass940.clone = GraphHelper.Clone<PMQuoteMaint>(this);
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass940, __methodptr(\u003CCopyQuote\u003Eb__1)));
    }
    return (IEnumerable) pmQuoteList;
  }

  [PXUIField(DisplayName = "Mark as Accepted")]
  [PXButton]
  protected virtual IEnumerable Accept(PXAdapter adapter) => adapter.Get();

  [PXUIField(DisplayName = "Mark as Declined")]
  [PXButton]
  protected virtual IEnumerable Decline(PXAdapter adapter) => adapter.Get();

  /// <summary>
  /// Returns true both for source as well as target graph during copy-paste procedure.
  /// </summary>
  public bool IsCopyPaste { get; private set; }

  /// <summary>
  /// During Paste this propert holds the reference to the Graph with source data.
  /// </summary>
  public PMQuoteMaint CopySource { get; private set; }

  public virtual void CopyToQuote(PMQuote currentquote, PMQuoteMaint.CopyQuoteFilter param)
  {
    ((PXSelectBase<PMQuote>) this.Quote).Current = currentquote;
    PMQuoteMaint instance1 = PXGraph.CreateInstance<PMQuoteMaint>();
    instance1.IsCopyPaste = true;
    instance1.CopySource = this;
    ((PXGraph) instance1).SelectTimeStamp();
    PMQuote instance2 = (PMQuote) ((PXSelectBase) instance1.Quote).Cache.CreateInstance();
    PMQuote pmQuote = ((PXSelectBase<PMQuote>) instance1.Quote).Insert(instance2);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<PMQuote.curyInfoID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    currencyInfo1.CuryInfoID = new long?();
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = GraphHelper.Caches<PX.Objects.CM.Extensions.CurrencyInfo>((PXGraph) instance1).Insert(currencyInfo1);
    foreach (string field in (List<string>) ((PXSelectBase) this.Quote).Cache.Fields)
    {
      if (!((PXSelectBase) instance1.Quote).Cache.Keys.Contains(field) && !(field == ((PXSelectBase) instance1.Quote).Cache.GetField(typeof (PMQuote.isPrimary))) && !(field == ((PXSelectBase) instance1.Quote).Cache.GetField(typeof (PMQuote.quoteID))) && !(field == ((PXSelectBase) instance1.Quote).Cache.GetField(typeof (PMQuote.status))) && !(field == ((PXSelectBase) instance1.Quote).Cache.GetField(typeof (PMQuote.expirationDate))) && !(field == ((PXSelectBase) instance1.Quote).Cache.GetField(typeof (PMQuote.approved))) && !(field == ((PXSelectBase) instance1.Quote).Cache.GetField(typeof (PMQuote.rejected))) && !(field == ((PXSelectBase) instance1.Quote).Cache.GetField(typeof (PMQuote.quoteProjectID))) && !(field == ((PXSelectBase) instance1.Quote).Cache.GetField(typeof (PMQuote.quoteProjectCD))) && !(field == ((PXSelectBase) instance1.Quote).Cache.GetField(typeof (PMQuote.opportunityContactID))) && !(field == ((PXSelectBase) instance1.Quote).Cache.GetField(typeof (PMQuote.opportunityAddressID))) && !(field == ((PXSelectBase) instance1.Quote).Cache.GetField(typeof (PMQuote.shipContactID))) && !(field == ((PXSelectBase) instance1.Quote).Cache.GetField(typeof (PMQuote.shipAddressID))) && !(field == ((PXSelectBase) instance1.Quote).Cache.GetField(typeof (PMQuote.allowOverrideContactAddress))) && !(field == ((PXSelectBase) instance1.Quote).Cache.GetField(typeof (PMQuote.allowOverrideShippingContactAddress))))
        ((PXSelectBase) instance1.Quote).Cache.SetValue((object) pmQuote, field, ((PXSelectBase) this.Quote).Cache.GetValue((object) currentquote, field));
    }
    UDFHelper.CopyAttributes(((PXSelectBase) this.Quote).Cache, (object) currentquote, ((PXSelectBase) instance1.Quote).Cache, (object) pmQuote, pmQuote.ClassID);
    pmQuote.CuryInfoID = currencyInfo2.CuryInfoID;
    pmQuote.Subject = param.Description;
    pmQuote.DocumentDate = ((PXGraph) this).Accessinfo.BusinessDate;
    pmQuote.Hold = new bool?(true);
    string note = PXNoteAttribute.GetNote(((PXSelectBase) this.Quote).Cache, (object) currentquote);
    Guid[] fileNotes = PXNoteAttribute.GetFileNotes(((PXSelectBase) this.Quote).Cache, (object) currentquote);
    object obj;
    ((PXSelectBase) instance1.Quote).Cache.RaiseFieldDefaulting<PMQuote.noteID>((object) pmQuote, ref obj);
    pmQuote.QuoteID = pmQuote.NoteID = (Guid?) obj;
    PXNoteAttribute.SetNote(((PXSelectBase) instance1.Quote).Cache, (object) pmQuote, note);
    PXNoteAttribute.SetFileNotes(((PXSelectBase) instance1.Quote).Cache, (object) pmQuote, fileNotes);
    ((PXSelectBase) this.Tasks).View.CloneView((PXGraph) instance1, pmQuote.QuoteID, currencyInfo2);
    ((PXSelectBase) this.Products).View.CloneView((PXGraph) instance1, pmQuote.QuoteID, currencyInfo2);
    foreach (PXResult<CROpportunityProducts> pxResult in ((PXSelectBase<CROpportunityProducts>) instance1.Products).Select(Array.Empty<object>()))
    {
      CROpportunityProducts opportunityProducts = PXResult<CROpportunityProducts>.op_Implicit(pxResult);
      if (currentquote.QuoteProjectID.HasValue)
      {
        opportunityProducts.ProjectID = new int?();
        opportunityProducts.TaskID = new int?();
      }
      if (!param.OverrideManualPrices.GetValueOrDefault())
        opportunityProducts.ManualPrice = new bool?(true);
    }
    ((PXGraph) this).GetExtension<PMQuoteMaint.PMDiscount>();
    ((PXGraph) this).Views["DiscountDetails"].CloneView((PXGraph) instance1, pmQuote.QuoteID, currencyInfo2);
    ((PXSelectBase) this.TaxLines).View.CloneView((PXGraph) instance1, pmQuote.QuoteID, currencyInfo2);
    ((PXSelectBase) this.Taxes).View.CloneView((PXGraph) instance1, pmQuote.QuoteID, currencyInfo2, "RecordID");
    instance1.Answers.CopyAllAttributes((object) pmQuote, ((PXSelectBase) this.Quote).Cache.Current);
    foreach (CSAnswers csAnswers in ((PXSelectBase) instance1.Answers).Cache.Inserted)
    {
      Guid? refNoteId = csAnswers.RefNoteID;
      Guid? quoteId = pmQuote.QuoteID;
      if ((refNoteId.HasValue == quoteId.HasValue ? (refNoteId.HasValue ? (refNoteId.GetValueOrDefault() != quoteId.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        instance1.Answers.Delete(csAnswers);
    }
    PMQuote copy = (PMQuote) ((PXSelectBase) instance1.Quote).Cache.CreateCopy((object) pmQuote);
    copy.OpportunityContactID = currentquote.OpportunityContactID;
    copy.OpportunityAddressID = currentquote.OpportunityAddressID;
    copy.ShipContactID = currentquote.ShipContactID;
    copy.ShipAddressID = currentquote.ShipAddressID;
    ((PXSelectBase) this.Quote_Contact).View.CloneView((PXGraph) instance1, pmQuote.QuoteID, currencyInfo2, "ContactID");
    copy.OpportunityContactID = ((PXSelectBase<CRContact>) instance1.Quote_Contact).Current.ContactID;
    ((PXSelectBase) this.Quote_Address).View.CloneView((PXGraph) instance1, pmQuote.QuoteID, currencyInfo2, "AddressID");
    copy.OpportunityAddressID = ((PXSelectBase<CRAddress>) instance1.Quote_Address).Current.AddressID;
    CRShippingContact current1 = ((PXSelectBase<CRShippingContact>) this.Shipping_Contact).Current;
    if ((current1 != null ? (current1.OverrideContact.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      ((PXSelectBase) this.Shipping_Contact).View.CloneView((PXGraph) instance1, pmQuote.QuoteID, currencyInfo2, "ContactID");
      copy.ShipContactID = ((PXSelectBase<CRShippingContact>) instance1.Shipping_Contact).Current.ContactID;
    }
    CRShippingAddress current2 = ((PXSelectBase<CRShippingAddress>) this.Shipping_Address).Current;
    if ((current2 != null ? (current2.OverrideAddress.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      ((PXSelectBase) this.Shipping_Address).View.CloneView((PXGraph) instance1, pmQuote.QuoteID, currencyInfo2, "AddressID");
      copy.ShipAddressID = ((PXSelectBase<CRShippingAddress>) instance1.Shipping_Address).Current.AddressID;
    }
    ((PXSelectBase<PMQuote>) instance1.Quote).Update(copy);
    PMQuoteMaint.PMDiscount extension = ((PXGraph) instance1).GetExtension<PMQuoteMaint.PMDiscount>();
    RecalcDiscountsParamFilter current3 = ((PXSelectBase<RecalcDiscountsParamFilter>) extension.recalcdiscountsfilter).Current;
    bool? nullable1 = param.OverrideManualDiscounts;
    bool? nullable2 = new bool?(nullable1.GetValueOrDefault());
    current3.OverrideManualDiscounts = nullable2;
    RecalcDiscountsParamFilter current4 = ((PXSelectBase<RecalcDiscountsParamFilter>) extension.recalcdiscountsfilter).Current;
    nullable1 = param.OverrideManualDocGroupDiscounts;
    bool? nullable3 = new bool?(nullable1.GetValueOrDefault());
    current4.OverrideManualDocGroupDiscounts = nullable3;
    RecalcDiscountsParamFilter current5 = ((PXSelectBase<RecalcDiscountsParamFilter>) extension.recalcdiscountsfilter).Current;
    nullable1 = param.OverrideManualPrices;
    bool? nullable4 = new bool?(nullable1.GetValueOrDefault());
    current5.OverrideManualPrices = nullable4;
    RecalcDiscountsParamFilter current6 = ((PXSelectBase<RecalcDiscountsParamFilter>) extension.recalcdiscountsfilter).Current;
    nullable1 = param.RecalculateDiscounts;
    bool? nullable5 = new bool?(nullable1.GetValueOrDefault());
    current6.RecalcDiscounts = nullable5;
    RecalcDiscountsParamFilter current7 = ((PXSelectBase<RecalcDiscountsParamFilter>) extension.recalcdiscountsfilter).Current;
    nullable1 = param.RecalculatePrices;
    bool? nullable6 = new bool?(nullable1.GetValueOrDefault());
    current7.RecalcUnitPrices = nullable6;
    ((PXGraph) instance1).Actions["RecalculateDiscountsAction"].Press();
    PXRedirectHelper.TryRedirect((PXGraph) instance1, (PXRedirectHelper.WindowMode) 0);
  }

  protected virtual string DefaultReportID => "PM604500";

  protected virtual string DefaultNotificationCD => "PMQUOTE";

  [PXUIField(DisplayName = "Send")]
  [PXButton]
  public virtual IEnumerable SendQuote(PXAdapter adapter)
  {
    List<PMQuote> list = adapter.Get<PMQuote>().ToList<PMQuote>();
    ((PXGraph) this).GetExtension<PMQuoteMaint.PMQuoteMaint_ActivityDetailsExt>().SendNotifications((Func<PMQuote, string>) (_ => "Project"), this.DefaultNotificationCD, (IList<PMQuote>) list, (Func<PMQuote, int?>) (doc => doc.BranchID), (Func<PMQuote, IDictionary<string, string>>) (doc =>
    {
      if (!doc.TemplateID.HasValue)
        ((PXSelectBase) this.Quote).Cache.RaiseExceptionHandling<PMQuote.templateID>((object) doc, (object) doc.TemplateID, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<PMQuote.templateID>(((PXSelectBase) this.Quote).Cache)
        }));
      return (IDictionary<string, string>) new Dictionary<string, string>()
      {
        ["PMQuote.QuoteNbr"] = doc.QuoteNbr
      };
    }), new MassEmailingActionParameters(adapter), (Func<PMQuote, object>) (doc => (object) doc.BAccountID));
    ((PXAction) this.Save).Press();
    return (IEnumerable) list;
  }

  [PXUIField(DisplayName = "Print")]
  [PXButton]
  public virtual IEnumerable PrintQuote(PXAdapter adapter)
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    string defaultReportId = this.DefaultReportID;
    IEnumerator enumerator = adapter.Get().GetEnumerator();
    try
    {
      if (enumerator.MoveNext())
      {
        PMQuote pmQuote = PXResult<PMQuote, PMProject>.op_Implicit((PXResult<PMQuote, PMProject>) enumerator.Current);
        ((PXAction) this.Save).Press();
        dictionary["QuoteNbr"] = pmQuote.QuoteNbr;
        throw new PXReportRequiredException(dictionary, defaultReportId, "Report " + defaultReportId, (CurrentLocalization) null);
      }
    }
    finally
    {
      if (enumerator is IDisposable disposable)
        disposable.Dispose();
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Set as Primary")]
  [PXButton]
  public virtual IEnumerable PrimaryQuote(PXAdapter adapter)
  {
    PMQuoteMaint pmQuoteMaint = this;
    foreach (PXResult<PMQuote, PMProject> pxResult1 in adapter.Get())
    {
      PMQuote pmQuote1 = PXResult<PMQuote, PMProject>.op_Implicit(pxResult1);
      ((PXSelectBase) pmQuoteMaint.Opportunity).Cache.Clear();
      PXResult<PX.Objects.CR.Standalone.CROpportunity> pxResult2 = (PXResult<PX.Objects.CR.Standalone.CROpportunity>) ((PXSelectBase) pmQuoteMaint.Opportunity).View.SelectSingleBound(new object[1]
      {
        (object) pmQuote1
      }, Array.Empty<object>());
      if (((PXResult) pxResult2)[typeof (PX.Objects.CR.Standalone.CROpportunity)] is PX.Objects.CR.Standalone.CROpportunity crOpportunity)
      {
        Guid? defQuoteId = crOpportunity.DefQuoteID;
        Guid? quoteId = pmQuote1.QuoteID;
        if ((defQuoteId.HasValue == quoteId.HasValue ? (defQuoteId.HasValue ? (defQuoteId.GetValueOrDefault() != quoteId.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        {
          PMQuote pmQuote2 = PXResultset<PMQuote>.op_Implicit(PXSelectBase<PMQuote, PXSelect<PMQuote, Where<PMQuote.quoteID, Equal<Required<PMQuote.quoteID>>>>.Config>.Select((PXGraph) pmQuoteMaint, new object[1]
          {
            (object) crOpportunity.DefQuoteID
          }));
          if (pmQuote2 != null && pmQuote2.QuoteProjectID.HasValue)
            throw new PXException("The quote cannot be marked as the primary quote of the {0} opportunity because the opportunity is linked to the closed {1} project quote.", new object[2]
            {
              (object) crOpportunity.OpportunityID,
              (object) pmQuote2.QuoteNbr
            });
        }
      }
      ((PXSelectBase<PX.Objects.CR.Standalone.CROpportunity>) pmQuoteMaint.Opportunity).Current = PXResult<PX.Objects.CR.Standalone.CROpportunity>.op_Implicit(pxResult2);
      ((PXSelectBase<PX.Objects.CR.Standalone.CROpportunity>) pmQuoteMaint.Opportunity).Current.DefQuoteID = pmQuote1.QuoteID;
      pmQuote1.DefQuoteID = pmQuote1.QuoteID;
      pmQuote1.IsPrimary = new bool?(true);
      ((PXSelectBase) pmQuoteMaint.Opportunity).Cache.Update((object) ((PXSelectBase<PX.Objects.CR.Standalone.CROpportunity>) pmQuoteMaint.Opportunity).Current);
      ((PXGraph) pmQuoteMaint).Views.Caches.Add(typeof (PX.Objects.CR.Standalone.CROpportunity));
      PMQuote pmQuote3 = ((PXSelectBase) pmQuoteMaint.QuoteCurrent).Cache.Update((object) pmQuote1) as PMQuote;
      ((PXGraph) pmQuoteMaint).Persist();
      yield return (object) pmQuote3;
    }
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
  {
    PMQuoteMaint aGraph = this;
    foreach (PXResult<PMQuote, PMProject> pxResult in adapter.Get())
    {
      PMQuote pmQuote = PXResult<PMQuote, PMProject>.op_Implicit(pxResult);
      bool flag1 = false;
      ((PXAction) aGraph.Save).Press();
      if (pmQuote != null)
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
        if (flag1)
          ((PXAction) aGraph.Save).Press();
      }
      yield return (object) pmQuote;
    }
  }

  public virtual void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers)
  {
    EnumerableExtensions.ForEach<Command>(script.Where<Command>((Func<Command, bool>) (_ => _.ObjectName.StartsWith("Products"))), (Action<Command>) (_ => _.Commit = false));
    script.Where<Command>((Func<Command, bool>) (_ => _.ObjectName.StartsWith("Products"))).Last<Command>().Commit = true;
    script.Sort((Comparison<Command>) ((first, second) =>
    {
      if (first.ObjectName == "Products" && second.ObjectName == "Tasks")
        return 1;
      return first == second ? 0 : -1;
    }));
    containers.Sort((Comparison<Container>) ((first, second) =>
    {
      if (first.ViewName() == "Products" && second.ViewName() == "Tasks")
        return 1;
      return first == second ? 0 : -1;
    }));
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
  public virtual void ViewShippingOnMap()
  {
    CRShippingAddress aAddr = ((PXSelectBase<CRShippingAddress>) this.Shipping_Address).SelectSingle(Array.Empty<object>());
    if (aAddr == null)
      return;
    BAccountUtility.ViewOnMap((CRAddress) aAddr);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable AddCommonTasks(PXAdapter adapter)
  {
    if (((PXSelectBase<ProjectEntry.SelectedTask>) this.TasksForAddition).AskExt() == 1)
      this.AddSelectedCommonTasks();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual void AddNewProjectTemplate()
  {
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<TemplateMaint>(), "New Project Template");
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
    throw requiredException;
  }

  [PXButton]
  [PXUIField(Visible = false)]
  protected virtual IEnumerable ViewProject(PXAdapter adapter)
  {
    ProjectEntry instance = PXGraph.CreateInstance<ProjectEntry>();
    ((PXSelectBase<PMProject>) instance.Project).Current = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXViewOf<PMProject>.BasedOn<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<BqlField<PMQuote.quoteProjectID, IBqlInt>.FromCurrent>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 1);
    return adapter.Get();
  }

  protected virtual void CopyQuoteFilter_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is PMQuoteMaint.CopyQuoteFilter row))
      return;
    bool? nullable = row.RecalculatePrices;
    if ((nullable.HasValue ? new bool?(!nullable.GetValueOrDefault()) : new bool?()).GetValueOrDefault())
      ((PXSelectBase) this.CopyQuoteInfo).Cache.SetValue<PMQuoteMaint.CopyQuoteFilter.overrideManualPrices>((object) row, (object) false);
    nullable = row.RecalculateDiscounts;
    if (!(nullable.HasValue ? new bool?(!nullable.GetValueOrDefault()) : new bool?()).GetValueOrDefault())
      return;
    ((PXSelectBase) this.CopyQuoteInfo).Cache.SetValue<PMQuoteMaint.CopyQuoteFilter.overrideManualDiscounts>((object) row, (object) false);
    ((PXSelectBase) this.CopyQuoteInfo).Cache.SetValue<PMQuoteMaint.CopyQuoteFilter.overrideManualDocGroupDiscounts>((object) row, (object) false);
  }

  protected virtual void CopyQuoteFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PMQuoteMaint.CopyQuoteFilter row))
      return;
    PXCache pxCache1 = sender;
    PMQuoteMaint.CopyQuoteFilter copyQuoteFilter1 = row;
    bool? nullable = row.RecalculatePrices;
    int num1 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMQuoteMaint.CopyQuoteFilter.overrideManualPrices>(pxCache1, (object) copyQuoteFilter1, num1 != 0);
    PXCache pxCache2 = sender;
    PMQuoteMaint.CopyQuoteFilter copyQuoteFilter2 = row;
    nullable = row.RecalculateDiscounts;
    int num2 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMQuoteMaint.CopyQuoteFilter.overrideManualDiscounts>(pxCache2, (object) copyQuoteFilter2, num2 != 0);
    PXCache pxCache3 = sender;
    PMQuoteMaint.CopyQuoteFilter copyQuoteFilter3 = row;
    nullable = row.RecalculateDiscounts;
    int num3 = nullable.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<PMQuoteMaint.CopyQuoteFilter.overrideManualDocGroupDiscounts>(pxCache3, (object) copyQuoteFilter3, num3 != 0);
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
  }

  protected virtual void PMQuote_TaxZoneID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is PMQuote row))
      return;
    PX.Objects.CR.Location location1 = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PMQuote.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PMQuote.locationID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) row.BAccountID,
      (object) row.LocationID
    }));
    if (location1 != null)
    {
      if (!string.IsNullOrEmpty(location1.CTaxZoneID))
      {
        e.NewValue = (object) location1.CTaxZoneID;
      }
      else
      {
        PX.Objects.CR.Address adrress = PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) location1.DefAddressID
        }));
        if (adrress != null)
          e.NewValue = (object) TaxBuilderEngine.GetTaxZoneByAddress((PXGraph) this, (IAddressBase) adrress);
      }
    }
    if (e.NewValue == null)
    {
      PX.Objects.CR.Location location2 = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectJoin<PX.Objects.CR.Location, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<Current<PMQuote.branchID>>>, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.GL.Branch.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>>>>, Where<PX.Objects.CR.Location.locationID, Equal<PX.Objects.CR.BAccount.defLocationID>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      e.NewValue = location2 == null || location2.VTaxZoneID == null ? (object) row.TaxZoneID : (object) location2.VTaxZoneID;
    }
    if (sender.GetStatus(e.Row) == null)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PMQuote_BAccountID_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is PMQuote row))
      return;
    int? baccountId = row.BAccountID;
    int num = 0;
    if (!(baccountId.GetValueOrDefault() < num & baccountId.HasValue))
      return;
    e.ReturnValue = (object) "";
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMQuote, PMQuote.bAccountID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMQuote, PMQuote.bAccountID>>) e).Cache.SetDefaultExt<PMQuote.taxCalcMode>((object) e.Row);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMQuote, PMQuote.locationID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMQuote, PMQuote.locationID>>) e).Cache.SetDefaultExt<PMQuote.taxCalcMode>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMQuote, PMQuote.locationID>>) e).Cache.SetDefaultExt<PMQuote.externalTaxExemptionNumber>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMQuote, PMQuote.locationID>>) e).Cache.SetDefaultExt<PMQuote.avalaraCustomerUsageType>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMQuote, PMQuote.taxCalcMode> e)
  {
    if (e.Row == null || string.IsNullOrWhiteSpace(e.Row.OpportunityID))
      return;
    CROpportunityRevision opportunityRevision = PXResultset<CROpportunityRevision>.op_Implicit(PXSelectBase<CROpportunityRevision, PXViewOf<CROpportunityRevision>.BasedOn<SelectFromBase<CROpportunityRevision, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CROpportunityRevision.opportunityID, IBqlString>.IsEqual<BqlField<PMQuote.opportunityID, IBqlString>.FromCurrent>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) new PMQuote[1]
    {
      e.Row
    }, Array.Empty<object>()));
    if (opportunityRevision == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMQuote, PMQuote.taxCalcMode>, PMQuote, object>) e).NewValue = (object) opportunityRevision.TaxCalcMode;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMQuote, PMQuote.taxCalcMode>>) e).Cancel = true;
  }

  protected virtual void PMQuote_OpportunityID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMQuote row))
      return;
    if (row.OpportunityID == null)
    {
      row.IsPrimary = new bool?(false);
    }
    else
    {
      row.IsPrimary = new bool?(this.IsFirstQuote(row.OpportunityID));
      PX.Objects.CR.CROpportunity crOpportunity = PXResultset<PX.Objects.CR.CROpportunity>.op_Implicit(PXSelectBase<PX.Objects.CR.CROpportunity, PXSelect<PX.Objects.CR.CROpportunity, Where<PX.Objects.CR.CROpportunity.opportunityID, Equal<Required<PX.Objects.CR.CROpportunity.opportunityID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
      {
        (object) row.OpportunityID
      }));
      if (row.IsPrimary.GetValueOrDefault())
        ((PXSelectBase) this.Quote).Cache.RaiseExceptionHandling<PMQuote.opportunityID>((object) row, (object) row.OpportunityID, (Exception) new PXSetPropertyException<PMQuote.opportunityID>("The quote is the first quote for the opportunity. If you save the changes, the quote will become the primary quote of the opportunity and the product lines of the opportunity will be deleted.", (PXErrorLevel) 2));
      else
        row.BranchID = crOpportunity.BranchID;
      row.BAccountID = crOpportunity.BAccountID;
      object contactId1 = (object) row.ContactID;
      if (contactId1 != null && !this.VerifyField<PMQuote.contactID>((object) row, contactId1))
        row.ContactID = new int?();
      int? contactId2 = row.ContactID;
      if (contactId2.HasValue && (ValueType) row.BAccountID == null)
        this.FillDefaultBAccountID(sender, row);
      object locationId = (object) row.LocationID;
      if (locationId == null || !this.VerifyField<PMQuote.locationID>((object) row, locationId))
        sender.SetDefaultExt<PMQuote.locationID>((object) row);
      contactId2 = row.ContactID;
      if (!contactId2.HasValue)
        sender.SetDefaultExt<PMQuote.contactID>((object) row);
      if (row.TaxZoneID != null)
        return;
      sender.SetDefaultExt<PMQuote.taxZoneID>((object) row);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PMQuote, PMQuote.quoteProjectCD> e)
  {
    if (!DimensionMaint.IsAutonumbered((PXGraph) this, "PROJECT"))
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMQuote, PMQuote.quoteProjectCD>>) e).ReturnValue = (object) PXMessages.LocalizeNoPrefix("<NEW>");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMQuote, PMQuote.quoteProjectCD> e)
  {
    if (ProjectAttribute.IsAutonumbered((PXGraph) this, "PROJECT") || string.IsNullOrEmpty((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMQuote, PMQuote.quoteProjectCD>, PMQuote, object>) e).NewValue))
      return;
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractCD, Equal<Required<PMProject.contractCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMQuote, PMQuote.quoteProjectCD>, PMQuote, object>) e).NewValue
    }));
    if (pmProject != null)
      throw new PXSetPropertyException<PMQuote.quoteProjectCD>("The project with the {0} identifier already exists. Specify another project ID.", new object[1]
      {
        (object) pmProject.ContractCD.Trim()
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMQuote, PMQuote.locationID> e)
  {
    if (e.Row == null || !e.Row.BAccountID.HasValue)
      return;
    PX.Objects.CR.BAccount baccount = PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.BAccountID
    }));
    if (baccount == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMQuote, PMQuote.locationID>, PMQuote, object>) e).NewValue = (object) baccount.DefLocationID;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMQuote, PMQuote.locationID>>) e).Cancel = true;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMQuote, PMQuote.templateID> e)
  {
    if (this.IsCopyPaste || ((PXGraph) this).IsCopyPasteContext)
      return;
    if (((PXSelectBase<CROpportunityProducts>) this.Products).Select(Array.Empty<object>()).Count == 0)
    {
      this.DeleteNoteAndFilesOfTemplate(e.Row, (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMQuote, PMQuote.templateID>, PMQuote, object>) e).OldValue);
      this.RedefaultTasksFromTemplate(e.Row);
      this.DefaultFromTemplate(e.Row);
    }
    else
    {
      if (((PXSelectBase<PMQuote>) this.Quote).Ask("Update Quote by Template", "Replace quote settings with the settings of the project template? The project tasks, attributes, and project manager will be replaced.", (MessageButtons) 4, (MessageIcon) 2) != 6)
        return;
      this.DeleteNoteAndFilesOfTemplate(e.Row, (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMQuote, PMQuote.templateID>, PMQuote, object>) e).OldValue);
      this.RedefaultTasksFromTemplate(e.Row);
      this.DefaultFromTemplate(e.Row);
    }
  }

  protected virtual void PMQuote_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    PXNoteAttribute.SetTextFilesActivitiesRequired<CROpportunityProducts.noteID>(((PXSelectBase) this.Products).Cache, (object) null, true, true, false);
    if (!(e.Row is PMQuote row))
      return;
    this.UpdateAddCommonTasksActionAvailability(row);
    ((PXSelectBase) this.Shipping_Address).AllowUpdate = row.Status == "D";
    PXUIFieldAttribute.SetEnabled<PMQuote.curyAmount>(cache, (object) row, row.ManualTotalEntry.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<PMQuote.curyDiscTot>(cache, (object) row, row.ManualTotalEntry.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<PMQuote.locationID>(cache, (object) row, row.BAccountID.HasValue);
    PXDefaultAttribute.SetPersistingCheck<PMQuote.locationID>(cache, (object) row, !row.BAccountID.HasValue ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXUIFieldAttribute.SetEnabled<PMQuote.branchID>(cache, (object) row, row.OpportunityID == null);
    PXUIFieldAttribute.SetEnabled<PMQuote.quoteProjectID>(cache, (object) row, false);
    PXUIFieldAttribute.SetRequired<PMQuote.branchID>(cache, true);
    PXDefaultAttribute.SetPersistingCheck<PMQuote.quoteProjectID>(cache, (object) row, (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetVisible<PMQuote.curyID>(cache, (object) row, this.IsMultyCurrency);
    PXUIFieldAttribute.SetEnabled<PMQuote.curyID>(cache, (object) row, PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>());
    if (row.ManualTotalEntry.GetValueOrDefault())
    {
      Decimal? curyTaxTotal = row.CuryTaxTotal;
      Decimal num = 0M;
      if (curyTaxTotal.GetValueOrDefault() > num & curyTaxTotal.HasValue)
      {
        cache.RaiseExceptionHandling<PMQuote.curyTaxTotal>((object) row, (object) row.CuryTaxTotal, (Exception) new PXSetPropertyException<PMQuote.curyTaxTotal>("The tax total is excluded from the total because the Manual Amount check box is selected.", (PXErrorLevel) 2));
        goto label_6;
      }
    }
    cache.RaiseExceptionHandling<PMQuote.curyTaxTotal>((object) row, (object) row.CuryTaxTotal, (Exception) null);
label_6:
    bool hasValue = row.QuoteProjectID.HasValue;
    PXUIFieldAttribute.SetVisible<PMQuote.quoteProjectID>(cache, (object) row, hasValue);
    PXUIFieldAttribute.SetVisible<PMQuote.quoteProjectCD>(cache, (object) row, !hasValue);
    PXUIFieldAttribute.SetEnabled<PMQuote.quoteProjectCD>(cache, (object) row, !DimensionMaint.IsAutonumbered((PXGraph) this, "PROJECT"));
    PXUIFieldAttribute.SetEnabled<PMQuote.opportunityID>(cache, (object) row, row.OpportunityID == null || !this.IsReadonlyPrimaryQuote(row.QuoteID));
    if (row.OpportunityID == null || !(row.Status == "D"))
      PXUIFieldAttribute.SetEnabled<PMQuote.bAccountID>(cache, (object) row, row.OpportunityID == null);
    bool? opportunityIsActive = row.OpportunityIsActive;
    bool flag1 = false;
    if (opportunityIsActive.GetValueOrDefault() == flag1 & opportunityIsActive.HasValue)
      cache.RaiseExceptionHandling<PMQuote.opportunityID>((object) row, (object) row.OpportunityID, (Exception) new PXSetPropertyException("The opportunity is not active.", (PXErrorLevel) 2));
    if (((PXGraph) this).UnattendedMode)
      return;
    CRShippingAddress crShippingAddress = PXResultset<CRShippingAddress>.op_Implicit(((PXSelectBase<CRShippingAddress>) this.Shipping_Address).Select(Array.Empty<object>()));
    CRAddress crAddress = PXResultset<CRAddress>.op_Implicit(((PXSelectBase<CRAddress>) this.Quote_Address).Select(Array.Empty<object>()));
    int num1;
    if (crShippingAddress != null)
    {
      bool? isDefaultAddress = crShippingAddress.IsDefaultAddress;
      bool flag2 = false;
      if (isDefaultAddress.GetValueOrDefault() == flag2 & isDefaultAddress.HasValue)
      {
        bool? isValidated = crShippingAddress.IsValidated;
        bool flag3 = false;
        if (isValidated.GetValueOrDefault() == flag3 & isValidated.HasValue)
        {
          num1 = 1;
          goto label_21;
        }
      }
    }
    if (crAddress != null)
    {
      bool? isDefaultAddress = crAddress.IsDefaultAddress;
      bool flag4 = false;
      if (!(isDefaultAddress.GetValueOrDefault() == flag4 & isDefaultAddress.HasValue))
      {
        int? nullable = row.BAccountID;
        if (!nullable.HasValue)
        {
          nullable = row.ContactID;
          if (nullable.HasValue)
            goto label_19;
        }
        else
          goto label_19;
      }
      bool? isValidated = crAddress.IsValidated;
      bool flag5 = false;
      num1 = isValidated.GetValueOrDefault() == flag5 & isValidated.HasValue ? 1 : 0;
      goto label_21;
    }
label_19:
    num1 = 0;
label_21:
    ((PXAction) this.validateAddresses).SetEnabled(num1 != 0);
  }

  protected virtual void PMQuote_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is PMQuote row))
      return;
    foreach (object obj in GraphHelper.RowCast<CROpportunityProducts>((IEnumerable) ((PXSelectBase<CROpportunityProducts>) this.Products).Select(Array.Empty<object>())))
      ((PXSelectBase) this.Products).Cache.Update(obj);
    if (!row.TemplateID.HasValue)
      return;
    this.DefaultFromTemplate(row);
    this.RedefaultTasksFromTemplate(row);
  }

  protected virtual void PMQuote_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    PMQuote oldRow = e.OldRow as PMQuote;
    PMQuote row = e.Row as PMQuote;
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
    nullable1 = row.LocationID;
    nullable2 = oldRow.LocationID;
    bool flag2 = !(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue);
    DateTime? documentDate1 = row.DocumentDate;
    DateTime? documentDate2 = oldRow.DocumentDate;
    bool flag3 = documentDate1.HasValue != documentDate2.HasValue || documentDate1.HasValue && documentDate1.GetValueOrDefault() != documentDate2.GetValueOrDefault();
    nullable2 = row.QuoteProjectID;
    nullable1 = oldRow.QuoteProjectID;
    bool flag4 = !(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue);
    if (!(flag2 | flag3 | flag4 | flag1))
      return;
    PXCache cache = ((PXSelectBase) this.Products).Cache;
    foreach (CROpportunityProducts selectProduct in this.SelectProducts((object) row.QuoteID))
    {
      CROpportunityProducts copy = (CROpportunityProducts) cache.CreateCopy((object) selectProduct);
      copy.ProjectID = row.QuoteProjectID;
      copy.CustomerID = row.BAccountID;
      cache.Update((object) copy);
    }
  }

  protected virtual void PMQuote_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is PMQuote row))
      return;
    if (row.Status == "C")
    {
      sender.RaiseExceptionHandling<PMQuote.status>(e.Row, (object) null, (Exception) new PXSetPropertyException("Closed quote cannot be deleted."));
      ((CancelEventArgs) e).Cancel = true;
    }
    if (!row.IsPrimary.GetValueOrDefault() || this.IsSingleQuote(row.OpportunityID))
      return;
    sender.RaiseExceptionHandling<PMQuote.isPrimary>(e.Row, (object) null, (Exception) new PXSetPropertyException("The quote cannot be deleted because it is marked as primary for the opportunity. If you want to delete this quote, mark another quote as primary first."));
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PMQuote_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (!(e.Row is PMQuote row))
      return;
    bool? nullable;
    if ((e.Operation == 2 || e.Operation == 1) && row.OpportunityID != null && e.TranStatus == null)
    {
      nullable = row.IsFirstQuote;
      if (nullable.GetValueOrDefault())
      {
        PX.Objects.CR.Standalone.CROpportunity crOpportunity = PXResultset<PX.Objects.CR.Standalone.CROpportunity>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.CROpportunity, PXSelect<PX.Objects.CR.Standalone.CROpportunity, Where<PX.Objects.CR.Standalone.CROpportunity.opportunityID, Equal<Required<PX.Objects.CR.Standalone.CROpportunity.opportunityID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
        {
          (object) row.OpportunityID
        }));
        Guid? defQuoteId = crOpportunity.DefQuoteID;
        Guid? quoteId = row.QuoteID;
        if ((defQuoteId.HasValue == quoteId.HasValue ? (defQuoteId.HasValue ? (defQuoteId.GetValueOrDefault() != quoteId.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        {
          PXDatabase.Delete<CROpportunityDiscountDetail>(new PXDataFieldRestrict[1]
          {
            (PXDataFieldRestrict) new PXDataFieldRestrict<CROpportunityDiscountDetail.quoteID>((PXDbType) 14, (object) crOpportunity.DefQuoteID)
          });
          PXDatabase.Delete<CROpportunityTax>(new PXDataFieldRestrict[1]
          {
            (PXDataFieldRestrict) new PXDataFieldRestrict<CROpportunityTax.quoteID>((PXDbType) 14, (object) crOpportunity.DefQuoteID)
          });
          PXDatabase.Delete<CROpportunityProducts>(new PXDataFieldRestrict[1]
          {
            (PXDataFieldRestrict) new PXDataFieldRestrict<CROpportunityProducts.quoteID>((PXDbType) 14, (object) crOpportunity.DefQuoteID)
          });
          PXDatabase.Delete<CROpportunityRevision>(new PXDataFieldRestrict[1]
          {
            (PXDataFieldRestrict) new PXDataFieldRestrict<CROpportunityRevision.noteID>((PXDbType) 14, (object) crOpportunity.DefQuoteID)
          });
        }
        PXDatabase.Update<PX.Objects.CR.Standalone.CROpportunity>(new PXDataFieldParam[2]
        {
          (PXDataFieldParam) new PXDataFieldAssign<PX.Objects.CR.Standalone.CROpportunity.defQuoteID>((object) row.QuoteID),
          (PXDataFieldParam) new PXDataFieldRestrict<PX.Objects.CR.Standalone.CROpportunity.opportunityID>((PXDbType) 22, new int?((int) byte.MaxValue), (object) row.OpportunityID, (PXComp) 0)
        });
      }
    }
    if (e.Operation != 2)
      return;
    nullable = row.IsPrimary;
    if (!nullable.GetValueOrDefault() || row.OpportunityID == null || e.TranStatus != null)
      return;
    PXDatabase.Update<PX.Objects.CR.Standalone.CROpportunity>(new PXDataFieldParam[2]
    {
      (PXDataFieldParam) new PXDataFieldAssign<PX.Objects.CR.Standalone.CROpportunity.defQuoteID>((object) row.QuoteID),
      (PXDataFieldParam) new PXDataFieldRestrict<PX.Objects.CR.Standalone.CROpportunity.opportunityID>((PXDbType) 22, new int?((int) byte.MaxValue), (object) row.OpportunityID, (PXComp) 0)
    });
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMQuote> e)
  {
    PXDimensionAttribute.SuppressAutoNumbering<PMQuote.quoteProjectCD>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMQuote>>) e).Cache, true);
    PMQuote row1 = e.Row;
    if (row1 == null)
      return;
    if (!e.Row.BranchID.HasValue)
      ((PXSelectBase) this.Quote).Cache.RaiseExceptionHandling<PMQuote.branchID>((object) e.Row, (object) e.Row.BranchID, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<PMQuote.branchID>(((PXSelectBase) this.Quote).Cache)
      }));
    bool? nullable1;
    if (e.Operation == 2 || e.Operation == 1)
    {
      PMQuote row2 = e.Row;
      PXSelect<CRAddress, Where<CRAddress.addressID, Equal<Current<PMQuote.opportunityAddressID>>>> quoteAddress = this.Quote_Address;
      int num1;
      if (quoteAddress == null)
      {
        num1 = 0;
      }
      else
      {
        nullable1 = (bool?) ((PXSelectBase<CRAddress>) quoteAddress).Current?.OverrideAddress;
        num1 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
      int num2;
      if (num1 == 0)
      {
        PXSelect<CRContact, Where<CRContact.contactID, Equal<Current<PMQuote.opportunityContactID>>>> quoteContact = this.Quote_Contact;
        if (quoteContact == null)
        {
          num2 = 0;
        }
        else
        {
          CRContact current = ((PXSelectBase<CRContact>) quoteContact).Current;
          bool? nullable2;
          if (current == null)
          {
            nullable1 = new bool?();
            nullable2 = nullable1;
          }
          else
            nullable2 = current.OverrideContact;
          nullable1 = nullable2;
          num2 = nullable1.GetValueOrDefault() ? 1 : 0;
        }
      }
      else
        num2 = 1;
      bool? nullable3 = new bool?(num2 != 0);
      row2.AllowOverrideContactAddress = nullable3;
    }
    if (e.Operation == 2 || e.Operation == 1)
    {
      int? nullable4 = row1.BAccountID;
      if (nullable4.HasValue)
      {
        nullable4 = row1.LocationID;
        if (!nullable4.HasValue)
        {
          ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMQuote>>) e).Cache.RaiseExceptionHandling<PMQuote.locationID>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty."));
          e.Cancel = true;
        }
      }
    }
    PMQuote pmQuote = ((PXSelectBase<PMQuote>) this.QuoteInDb).SelectSingle(new object[1]
    {
      (object) row1.QuoteID
    });
    if (row1.OpportunityID != null && row1.OpportunityID != pmQuote?.OpportunityID)
    {
      PX.Objects.CR.Standalone.CROpportunity crOpportunity = PXResultset<PX.Objects.CR.Standalone.CROpportunity>.op_Implicit(((PXSelectBase<PX.Objects.CR.Standalone.CROpportunity>) this.Opportunity).Select(new object[1]
      {
        (object) row1.OpportunityID
      }));
      if (crOpportunity != null)
      {
        nullable1 = crOpportunity.IsActive;
        if (!nullable1.GetValueOrDefault())
        {
          ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMQuote>>) e).Cache.RaiseExceptionHandling<PMQuote.opportunityID>((object) row1, (object) row1.OpportunityID, (Exception) new PXSetPropertyException("The project quote cannot be linked to an opportunity that is not active.", (PXErrorLevel) 5));
          throw new PXSetPropertyException("The project quote cannot be linked to an opportunity that is not active.", (PXErrorLevel) 5);
        }
      }
    }
    if ((e.Operation == 2 || e.Operation == 1) && row1.OpportunityID != null)
    {
      row1.IsFirstQuote = new bool?(false);
      if (this.IsFirstQuote(row1.OpportunityID))
        row1.IsFirstQuote = new bool?(true);
    }
    object obj = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMQuote>>) e).Cache.GetValue<PMQuote.projectManager>((object) e.Row);
    object valueOriginal = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMQuote>>) e).Cache.GetValueOriginal<PMQuote.projectManager>((object) e.Row);
    if (obj == null || e.Operation != 2 && obj == valueOriginal)
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMQuote>>) e).Cache.VerifyFieldAndRaiseException<PMQuote.projectManager>((object) e.Row, true);
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PMQuote, PMQuote.projectManager> e)
  {
    PMQuote row = e.Row;
    if ((row != null ? (!row.ProjectManager.HasValue ? 1 : 0) : 1) != 0 || !string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PMQuote.projectManager>(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<PMQuote, PMQuote.projectManager>>) e).Cache, (object) e.Row)))
      return;
    PX.Objects.CR.Standalone.EPEmployee epEmployee = ((PXSelectBase<PX.Objects.CR.Standalone.EPEmployee>) new FbqlSelect<SelectFromBase<PX.Objects.CR.Standalone.EPEmployee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.CR.Standalone.EPEmployee.bAccountID, IBqlInt>.IsEqual<P.AsInt>>, PX.Objects.CR.Standalone.EPEmployee>.View((PXGraph) this)).SelectSingle(new object[1]
    {
      (object) e.Row.ProjectManager
    });
    if (epEmployee == null || !(epEmployee.VStatus != "A"))
      return;
    PXUIFieldAttribute.SetWarning<PMQuote.projectManager>(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<PMQuote, PMQuote.projectManager>>) e).Cache, (object) e.Row, "The employee is not active.");
  }

  protected virtual void CROpportunityRevision_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    CROpportunityRevision row = (CROpportunityRevision) e.Row;
    if (row == null || ((PXSelectBase<PMQuote>) this.Quote).Current == null)
      return;
    Guid? noteId = row.NoteID;
    Guid? quoteId = ((PXSelectBase<PMQuote>) this.Quote).Current.QuoteID;
    if ((noteId.HasValue == quoteId.HasValue ? (noteId.HasValue ? (noteId.GetValueOrDefault() == quoteId.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
      return;
    ((CancelEventArgs) e).Cancel = true;
  }

  [PXMergeAttributes]
  [RestrictSiteByBranch(typeof (PMQuote.branchID), null, ShowWarning = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CROpportunityProducts.siteID> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.inventoryID> e)
  {
    if (e.Row == null || !e.Row.EmployeeID.HasValue)
      return;
    PX.Objects.EP.EPEmployee epEmployee = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Required<PX.Objects.EP.EPEmployee.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.EmployeeID
    }));
    if (epEmployee == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.inventoryID>, CROpportunityProducts, object>) e).NewValue = (object) epEmployee.LabourItemID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.curyUnitCost> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.curyUnitCost>, CROpportunityProducts, object>) e).NewValue = (object) this.RateService.CalculateUnitCost(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.curyUnitCost>>) e).Cache, e.Row.ProjectID, e.Row.TaskID, e.Row.InventoryID, e.Row.UOM, e.Row.EmployeeID, ((PXSelectBase<PMQuote>) this.Quote).Current.DocumentDate, ((PXSelectBase<PMQuote>) this.Quote).Current.CuryInfoID);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.expenseAccountGroupID> e)
  {
    if (e.Row == null)
      return;
    int? nullable = e.Row.InventoryID;
    if (!nullable.HasValue)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select<CROpportunityProducts.inventoryID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.expenseAccountGroupID>>) e).Cache, (object) e.Row);
    if (inventoryItem == null)
      return;
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) inventoryItem.COGSAcctID
    }));
    if (account == null)
      return;
    nullable = account.AccountGroupID;
    if (!nullable.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.expenseAccountGroupID>, CROpportunityProducts, object>) e).NewValue = (object) account.AccountGroupID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.revenueAccountGroupID> e)
  {
    if (e.Row == null)
      return;
    PXResultset<PMAccountGroup> pxResultset = PXSelectBase<PMAccountGroup, PXViewOf<PMAccountGroup>.BasedOn<SelectFromBase<PMAccountGroup, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.type, Equal<AccountType.income>>>>>.And<BqlOperand<PMAccountGroup.isActive, IBqlBool>.IsEqual<True>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) this, 0, 2, Array.Empty<object>());
    if (pxResultset.Count == 1)
    {
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.revenueAccountGroupID>, CROpportunityProducts, object>) e).NewValue = (object) PXResultset<PMAccountGroup>.op_Implicit(pxResultset).GroupID;
    }
    else
    {
      int? nullable = e.Row.ExpenseAccountGroupID;
      if (!nullable.HasValue)
        return;
      PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find((PXGraph) this, e.Row.ExpenseAccountGroupID);
      if (pmAccountGroup == null)
        return;
      nullable = pmAccountGroup.RevenueAccountGroupID;
      if (!nullable.HasValue)
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.revenueAccountGroupID>, CROpportunityProducts, object>) e).NewValue = (object) pmAccountGroup.RevenueAccountGroupID;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.taskCD> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<CROpportunityProducts, CROpportunityProducts.taskCD>, CROpportunityProducts, object>) e).NewValue = (object) this.GetDefaultTaskCD();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.expenseAccountGroupID> e)
  {
    PXUIFieldAttribute.SetWarning<CROpportunityProducts.expenseAccountGroupID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.expenseAccountGroupID>>) e).Cache, (object) e.Row, (string) null);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.expenseAccountGroupID>>) e).Cache.SetDefaultExt<CROpportunityProducts.revenueAccountGroupID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<CROpportunityProducts, CROpportunityProducts.revenueAccountGroupID> e)
  {
    PXUIFieldAttribute.SetWarning<CROpportunityProducts.revenueAccountGroupID>(((PX.Data.Events.Event<PXFieldUpdatingEventArgs, PX.Data.Events.FieldUpdating<CROpportunityProducts, CROpportunityProducts.revenueAccountGroupID>>) e).Cache, (object) e.Row, (string) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.inventoryID> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.inventoryID>>) e).Cache.SetDefaultExt<CROpportunityProducts.expenseAccountGroupID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.inventoryID>>) e).Cache.SetDefaultExt<CROpportunityProducts.uOM>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.inventoryID>>) e).Cache.SetDefaultExt<CROpportunityProducts.curyUnitCost>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.inventoryID>>) e).Cache.SetDefaultExt<CROpportunityProducts.costCodeID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.uOM> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.uOM>>) e).Cache.SetDefaultExt<CROpportunityProducts.curyUnitCost>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.employeeID> e)
  {
    if (e.Row == null)
      return;
    int? inventoryId = e.Row.InventoryID;
    if (!inventoryId.HasValue)
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.employeeID>>) e).Cache.SetDefaultExt<CROpportunityProducts.inventoryID>((object) e.Row);
      inventoryId = e.Row.InventoryID;
      if (!inventoryId.HasValue)
        e.Row.UOM = "HOUR";
    }
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.employeeID>>) e).Cache.SetDefaultExt<CROpportunityProducts.curyUnitCost>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.curyExtCost> e)
  {
    if (e.Row == null || !(e.Row.CuryExtCost.GetValueOrDefault() == 0M) || e.Row.ExpenseAccountGroupID.HasValue || !(PXUIFieldAttribute.GetError<CROpportunityProducts.revenueAccountGroupID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.curyExtCost>>) e).Cache, (object) e.Row) == "The amount is non-zero and the revenue account group is empty. The line is not printed in the quote and cannot be converted to the project budget. You need to either specify the revenue account group or set the amount to zero to be able to convert the quote to a project."))
      return;
    PXUIFieldAttribute.SetWarning<CROpportunityProducts.expenseAccountGroupID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.curyExtCost>>) e).Cache, (object) e.Row, (string) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.curyAmount> e)
  {
    if (e.Row == null || !(e.Row.CuryAmount.GetValueOrDefault() == 0M) || e.Row.RevenueAccountGroupID.HasValue || !(PXUIFieldAttribute.GetError<CROpportunityProducts.expenseAccountGroupID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.curyAmount>>) e).Cache, (object) e.Row) == "The extended cost is non-zero and the cost account group is empty. The line cannot be converted to the project budget. You need to either specify the cost account group or set the extended cost to zero to be able to convert the quote to a project."))
      return;
    PXUIFieldAttribute.SetWarning<CROpportunityProducts.revenueAccountGroupID>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<CROpportunityProducts, CROpportunityProducts.curyAmount>>) e).Cache, (object) e.Row, (string) null);
  }

  protected virtual void _(PX.Data.Events.RowSelected<CROpportunityProducts> e)
  {
    if (e.Row == null)
      return;
    string error1 = PXUIFieldAttribute.GetError<CROpportunityProducts.revenueAccountGroupID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CROpportunityProducts>>) e).Cache, (object) e.Row);
    int? nullable;
    if (e.Row.CuryAmount.GetValueOrDefault() != 0M)
    {
      nullable = e.Row.RevenueAccountGroupID;
      if (!nullable.HasValue && string.IsNullOrEmpty(error1))
        PXUIFieldAttribute.SetWarning<CROpportunityProducts.revenueAccountGroupID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CROpportunityProducts>>) e).Cache, (object) e.Row, "The amount is non-zero and the revenue account group is empty. The line is not printed in the quote and cannot be converted to the project budget. You need to either specify the revenue account group or set the amount to zero to be able to convert the quote to a project.");
    }
    string error2 = PXUIFieldAttribute.GetError<CROpportunityProducts.expenseAccountGroupID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CROpportunityProducts>>) e).Cache, (object) e.Row);
    if (!(e.Row.CuryExtCost.GetValueOrDefault() != 0M))
      return;
    nullable = e.Row.ExpenseAccountGroupID;
    if (nullable.HasValue || !string.IsNullOrEmpty(error2))
      return;
    PXUIFieldAttribute.SetWarning<CROpportunityProducts.expenseAccountGroupID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CROpportunityProducts>>) e).Cache, (object) e.Row, "The extended cost is non-zero and the cost account group is empty. The line cannot be converted to the project budget. You need to either specify the cost account group or set the extended cost to zero to be able to convert the quote to a project.");
  }

  protected virtual void _(PX.Data.Events.RowPersisting<CROpportunityProducts> e)
  {
    CROpportunityProducts row = e.Row;
    string str;
    if (row == null || row == null || row.TaskCD == null || !this.PersistingTaskMap.TryGetValue(row.TaskCD, out str))
      return;
    row.TaskCD = str;
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<PMQuoteMaint.ConvertToProjectFilter> e)
  {
    if (e.Row == null)
      return;
    if (((PXSelectBase<PMSetup>) this.Setup).Current.AssignmentMapID.HasValue)
      PXUIFieldAttribute.SetDisplayName<PMQuoteMaint.ConvertToProjectFilter.activateProject>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMQuoteMaint.ConvertToProjectFilter>>) e).Cache, "Submit Project for Approval");
    PXUIFieldAttribute.SetVisible<PMQuoteMaint.ConvertToProjectFilter.taskCD>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMQuoteMaint.ConvertToProjectFilter>>) e).Cache, (object) e.Row, ((PXSelectBase<PMQuoteTask>) this.Tasks).Select(Array.Empty<object>()).Count > 1);
    PXUIFieldAttribute.SetEnabled<PMQuoteMaint.ConvertToProjectFilter.taskCD>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMQuoteMaint.ConvertToProjectFilter>>) e).Cache, (object) e.Row, e.Row.MoveActivities.GetValueOrDefault());
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMQuoteTask, PMQuoteTask.isDefault> e)
  {
    if (!e.Row.IsDefault.GetValueOrDefault())
      return;
    bool flag = false;
    foreach (PXResult<PMQuoteTask> pxResult in ((PXSelectBase<PMQuoteTask>) this.Tasks).Select(Array.Empty<object>()))
    {
      PMQuoteTask pmQuoteTask = PXResult<PMQuoteTask>.op_Implicit(pxResult);
      if (pmQuoteTask.IsDefault.GetValueOrDefault() && pmQuoteTask.TaskCD != e.Row.TaskCD)
      {
        ((PXSelectBase) this.Tasks).Cache.SetValue<PMQuoteTask.isDefault>((object) pmQuoteTask, (object) false);
        GraphHelper.SmartSetStatus(((PXSelectBase) this.Tasks).Cache, (object) pmQuoteTask, (PXEntryStatus) 1, (PXEntryStatus) 0);
        flag = true;
      }
    }
    if (flag)
      ((PXSelectBase) this.Tasks).View.RequestRefresh();
    foreach (PXResult<CROpportunityProducts> pxResult in ((PXSelectBase<CROpportunityProducts>) this.Products).Select(Array.Empty<object>()))
    {
      CROpportunityProducts opportunityProducts = PXResult<CROpportunityProducts>.op_Implicit(pxResult);
      if (string.IsNullOrWhiteSpace(opportunityProducts.TaskCD))
        ((PXSelectBase) this.Products).Cache.SetValue<CROpportunityProducts.taskCD>((object) opportunityProducts, (object) e.Row.TaskCD);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMQuoteTask, PMQuoteTask.taskCD> e)
  {
    foreach (PXResult<CROpportunityProducts> pxResult in ((PXSelectBase<CROpportunityProducts>) this.Products).Select(Array.Empty<object>()))
    {
      CROpportunityProducts opportunityProducts = PXResult<CROpportunityProducts>.op_Implicit(pxResult);
      if (!string.IsNullOrWhiteSpace(opportunityProducts.TaskCD) && opportunityProducts.TaskCD == (string) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PMQuoteTask, PMQuoteTask.taskCD>, PMQuoteTask, object>) e).OldValue)
      {
        ((PXSelectBase) this.Products).Cache.SetValue<CROpportunityProducts.taskCD>((object) opportunityProducts, (object) e.Row.TaskCD);
        GraphHelper.MarkUpdated(((PXSelectBase) this.Products).Cache, (object) opportunityProducts, true);
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PMQuoteTask> e)
  {
    if (((PXSelectBase<PMQuoteTask>) this.Tasks).Select(Array.Empty<object>()).Count <= 1)
      return;
    bool flag = false;
    foreach (PXResult<CROpportunityProducts> pxResult in ((PXSelectBase<CROpportunityProducts>) this.Products).Select(Array.Empty<object>()))
    {
      if (PXResult<CROpportunityProducts>.op_Implicit(pxResult).TaskCD == e.Row.TaskCD)
      {
        flag = true;
        break;
      }
    }
    if (flag)
      throw new PXException("Cannot delete a project task that is already in use on the Estimation tab.");
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PMQuoteTask> e)
  {
    PXResultset<PMQuoteTask> pxResultset = ((PXSelectBase<PMQuoteTask>) this.Tasks).Select(Array.Empty<object>());
    if (pxResultset.Count >= 2)
      return;
    string str = (string) null;
    if (pxResultset.Count > 0)
    {
      PMQuoteTask pmQuoteTask = PXResult<PMQuoteTask>.op_Implicit(pxResultset[0]);
      if (pmQuoteTask != null)
        str = pmQuoteTask.TaskCD;
    }
    foreach (PXResult<CROpportunityProducts> pxResult in ((PXSelectBase<CROpportunityProducts>) this.Products).Select(Array.Empty<object>()))
    {
      CROpportunityProducts opportunityProducts = PXResult<CROpportunityProducts>.op_Implicit(pxResult);
      ((PXSelectBase) this.Products).Cache.SetValue<CROpportunityProducts.taskCD>((object) opportunityProducts, (object) str);
      GraphHelper.MarkUpdated(((PXSelectBase) this.Products).Cache, (object) opportunityProducts, true);
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMQuoteTask> e)
  {
    PMQuoteTask row = e.Row;
    if (row == null)
      return;
    this.PersistingTaskCD = row.TaskCD;
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PMQuoteTask> e)
  {
    PMQuoteTask row = e.Row;
    if (row == null)
      return;
    this.PersistingTaskMap[this.PersistingTaskCD] = row.TaskCD;
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

  private void FillDefaultBAccountID(PXCache cache, PMQuote row)
  {
    if (row == null || !row.ContactID.HasValue)
      return;
    PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelectReadonly<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ContactID
    }));
    if (contact == null)
      return;
    row.ParentBAccountID = contact.ParentBAccountID;
    cache.SetValueExt<PMQuote.bAccountID>((object) row, (object) contact.BAccountID);
  }

  private bool IsMultyCurrency => PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();

  private IEnumerable SelectProducts(object quoteId)
  {
    if (quoteId == null)
      return (IEnumerable) new CROpportunityProducts[0];
    return (IEnumerable) GraphHelper.RowCast<CROpportunityProducts>((IEnumerable) PXSelectBase<CROpportunityProducts, PXSelect<CROpportunityProducts, Where<CROpportunityProducts.quoteID, Equal<Required<PMQuote.quoteID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      quoteId
    }));
  }

  private bool IsFirstQuote(string opportunityID)
  {
    return PXSelectBase<PX.Objects.CR.Standalone.CRQuote, PXSelectJoin<PX.Objects.CR.Standalone.CRQuote, InnerJoin<CROpportunityRevision, On<CROpportunityRevision.noteID, Equal<PX.Objects.CR.Standalone.CRQuote.quoteID>>>, Where<CROpportunityRevision.opportunityID, Equal<Required<CROpportunityRevision.opportunityID>>, And<PX.Objects.CR.Standalone.CRQuote.quoteID, NotEqual<Current<PMQuote.quoteID>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) opportunityID
    }).Count == 0;
  }

  private bool IsSingleQuote(string opportunityId)
  {
    return PXSelectBase<PX.Objects.CR.CRQuote, PXSelect<PX.Objects.CR.CRQuote, Where<PX.Objects.CR.CRQuote.opportunityID, Equal<Optional<PX.Objects.CR.CRQuote.opportunityID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 2, new object[1]
    {
      (object) opportunityId
    }).Count == 1;
  }

  public bool IsReadonlyPrimaryQuote(Guid? quoteID)
  {
    return PXSelectBase<PX.Objects.CR.Standalone.CROpportunity, PXSelectReadonly<PX.Objects.CR.Standalone.CROpportunity, Where<PX.Objects.CR.Standalone.CROpportunity.defQuoteID, Equal<Required<PX.Objects.CR.Standalone.CROpportunity.defQuoteID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) quoteID
    }).Count == 1;
  }

  public virtual bool IsExternalTax(string taxZoneID) => false;

  public virtual PMQuote CalculateExternalTax(PMQuote quote) => quote;

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    if (((PXSelectBase<PMQuote>) this.Quote).Current != null)
    {
      PX.Objects.Extensions.SalesTax.Document extension = ((PXSelectBase) this.Quote).Cache.GetExtension<PX.Objects.Extensions.SalesTax.Document>((object) ((PXSelectBase<PMQuote>) this.Quote).Current);
      if (extension != null)
        extension.TaxCalc = new TaxCalc?(TaxCalc.NoCalc);
    }
    if (string.Compare(viewName, "Products", true) == 0)
    {
      if (values.Contains((object) "opportunityID"))
        values[(object) "opportunityID"] = (object) ((PXSelectBase<PMQuote>) this.Quote).Current.OpportunityID;
      else
        values.Add((object) "opportunityID", (object) ((PXSelectBase<PMQuote>) this.Quote).Current.OpportunityID);
    }
    return true;
  }

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public void ImportDone(PXImportAttribute.ImportMode.Value mode)
  {
    if (((PXSelectBase<PMQuote>) this.Quote).Current == null)
      return;
    PX.Objects.Extensions.SalesTax.Document extension = ((PXSelectBase) this.Quote).Cache.GetExtension<PX.Objects.Extensions.SalesTax.Document>((object) ((PXSelectBase<PMQuote>) this.Quote).Current);
    if (extension != null)
      extension.TaxCalc = new TaxCalc?(TaxCalc.Calc);
    ((PXSelectBase) this.Quote).Cache.RaiseFieldUpdated<PMQuote.taxZoneID>((object) ((PXSelectBase<PMQuote>) this.Quote).Current, (object) null);
  }

  protected virtual void AddSelectedCommonTasks()
  {
    foreach (ProjectEntry.SelectedTask projectTask in ((PXSelectBase) this.TasksForAddition).Cache.Updated)
    {
      if (projectTask.Selected.GetValueOrDefault())
      {
        this.InsertNewTaskWithProjectTask(((PXSelectBase<PMQuote>) this.Quote).Current, (PMTask) projectTask);
        projectTask.Selected = new bool?(false);
      }
    }
  }

  protected virtual void UpdateAddCommonTasksActionAvailability(PMQuote quote)
  {
    ((PXAction) this.addCommonTasks).SetEnabled(quote?.Status == "D");
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
          typeof (PX.Objects.CR.CROpportunity.quoteNoteID).Name
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

  public virtual void ConvertQuoteToProject(
    PMQuote row,
    PMQuoteMaint.ConvertToProjectFilter settings)
  {
    if (!this.ValidateQuoteBeforeConvertToProject(row, out int _))
      throw new PXException("The quote cannot be converted.");
    ProjectEntry instance1 = PXGraph.CreateInstance<ProjectEntry>();
    instance1.SuppressTemplateIDUpdated = true;
    ((PXGraph) instance1).Clear();
    PMProject pmProject1 = new PMProject();
    pmProject1.BaseType = "P";
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ((PXGraph) instance1).GetExtension<ProjectEntry.MultiCurrency>().CloneCurrencyInfo(((PXGraph) this).GetExtension<PMQuoteMaint.MultiCurrency>().GetDefaultCurrencyInfo());
    pmProject1.CuryID = row.CuryID;
    pmProject1.CuryInfoID = currencyInfo.CuryInfoID;
    pmProject1.RateTypeID = currencyInfo.CuryRateTypeID;
    if (!DimensionMaint.IsAutonumbered((PXGraph) this, "PROJECT"))
      pmProject1.ContractCD = row.QuoteProjectCD;
    PMProject pmProject2;
    try
    {
      pmProject2 = ((PXSelectBase<PMProject>) instance1.Project).Insert(pmProject1);
    }
    catch (PXFieldProcessingException ex)
    {
      if (string.Equals(ex.FieldName, typeof (PMProject.contractCD).Name, StringComparison.OrdinalIgnoreCase))
        PMQuoteProjectCDDimensionAttribute.CheckProjectCD((PXGraph) this, row.QuoteProjectCD, ((Exception) ex).Message, false);
      throw ex;
    }
    pmProject2.CustomerID = row.BAccountID;
    int? nullable1 = row.BranchID;
    if (nullable1.HasValue)
      pmProject2.DefaultBranchID = row.BranchID;
    nullable1 = row.LocationID;
    if (nullable1.HasValue)
      pmProject2.LocationID = row.LocationID;
    if (row.TermsID != null)
      pmProject2.TermsID = row.TermsID;
    pmProject2.QuoteNbr = row.QuoteNbr;
    PMProject prj = ((PXSelectBase<PMProject>) instance1.Project).Update(pmProject2);
    prj.TemplateID = row.TemplateID;
    instance1.DefaultFromTemplate(prj, row.TemplateID, new ProjectEntry.DefaultFromTemplateSettings()
    {
      CopyProperties = true,
      CopyAttributes = true,
      CopyEmployees = true,
      CopyEquipment = true,
      CopyNotification = true,
      CopyCurrency = false,
      CopyNotes = settings.CopyNotes,
      CopyFiles = settings.CopyFiles
    });
    PX.Objects.CR.Standalone.EPEmployee epEmployee = PXResultset<PX.Objects.CR.Standalone.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.EPEmployee, PXSelect<PX.Objects.CR.Standalone.EPEmployee, Where<PX.Objects.CR.Standalone.EPEmployee.bAccountID, Equal<Required<PX.Objects.CR.Standalone.EPEmployee.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ProjectManager
    }));
    if (epEmployee != null)
    {
      prj.OwnerID = epEmployee.DefContactID;
      prj.ApproverID = row.ProjectManager;
    }
    prj.Description = row.Subject;
    if (PXDBLocalizableStringAttribute.IsEnabled)
      PXDBLocalizableStringAttribute.DefaultTranslationsFromMessage(((PXGraph) this).Caches[typeof (PMProject)], (object) prj, "Description", row.Subject);
    prj.TermsID = row.TermsID;
    prj.BaseCuryID = currencyInfo.BaseCuryID;
    nullable1 = row.BranchID;
    if (nullable1.HasValue)
      prj.DefaultBranchID = row.BranchID;
    PMProject row1 = ((PXSelectBase<PMProject>) instance1.Project).Update(prj);
    if (string.IsNullOrEmpty(((PXSelectBase<ContractBillingSchedule>) instance1.Billing).Current.Type))
      ((PXSelectBase<ContractBillingSchedule>) instance1.Billing).Current.Type = "D";
    PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.Quote).Cache, (object) row, ((PXSelectBase) instance1.Project).Cache, (object) row1, settings.CopyNotes, settings.CopyFiles);
    Dictionary<string, int> taskMap = new Dictionary<string, int>();
    Dictionary<int, int> templateToNewTaskMap = new Dictionary<int, int>();
    this.AddingTasksToProject(row, instance1, taskMap, templateToNewTaskMap, settings.CopyNotes, settings.CopyFiles);
    this.AddingRevenueBudgetToProject(instance1, taskMap, row1.BudgetLevel);
    this.AddingCostBudgetToProject(instance1, taskMap, row1.CostBudgetLevel);
    this.AddingBillingInfoToProject(row, instance1);
    this.AddingShippingInfoToProject(row, instance1);
    this.AddingDefaultTaskGLAccountToProject(instance1, templateToNewTaskMap);
    HashSet<int> intSet1 = new HashSet<int>();
    foreach (PXResult<CROpportunityProducts> pxResult in ((PXSelectBase<CROpportunityProducts>) this.Products).Select(Array.Empty<object>()))
    {
      CROpportunityProducts opportunityProducts = PXResult<CROpportunityProducts>.op_Implicit(pxResult);
      nullable1 = opportunityProducts.EmployeeID;
      if (nullable1.HasValue)
      {
        HashSet<int> intSet2 = intSet1;
        nullable1 = opportunityProducts.EmployeeID;
        int num = nullable1.Value;
        intSet2.Add(num);
      }
    }
    foreach (int num in intSet1)
      ((PXSelectBase<EPEmployeeContract>) instance1.EmployeeContract).Insert(new EPEmployeeContract()
      {
        ContractID = row1.ContractID,
        EmployeeID = new int?(num)
      });
    instance1.Answers.CopyAllAttributes((object) row1, (object) row);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (settings.ActivateTasks.GetValueOrDefault())
        ((PXAction) instance1.activateTasks).Press();
      if (settings.ActivateProject.GetValueOrDefault())
        ((PXAction) instance1.activate).Press();
      ((PXAction) instance1.Save).Press();
      row.QuoteProjectID = ((PXSelectBase<PMProject>) instance1.Project).Current.ContractID;
      ((PXSelectBase<PMQuote>) this.Quote).Update(row);
      Dictionary<string, int> dictionary1 = new Dictionary<string, int>();
      int? nullable2 = new int?();
      List<PMTask> pmTaskList = new List<PMTask>(GraphHelper.RowCast<PMTask>((IEnumerable) ((PXSelectBase<PMTask>) instance1.Tasks).Select(Array.Empty<object>())));
      foreach (PMTask pmTask in pmTaskList)
      {
        dictionary1.Add(pmTask.TaskCD, pmTask.TaskID.Value);
        if (pmTask.TaskCD == settings.TaskCD)
          nullable2 = pmTask.TaskID;
      }
      if (pmTaskList.Count == 1 || string.IsNullOrEmpty(settings.TaskCD))
        nullable2 = pmTaskList[0].TaskID;
      Dictionary<PMQuoteMaint.LaborRateKey, Decimal> dictionary2 = new Dictionary<PMQuoteMaint.LaborRateKey, Decimal>();
      foreach (PXResult<CROpportunityProducts> pxResult in ((PXSelectBase<CROpportunityProducts>) this.Products).Select(Array.Empty<object>()))
      {
        CROpportunityProducts opportunityProducts = PXResult<CROpportunityProducts>.op_Implicit(pxResult);
        string itemTaskCd = this.GetItemTaskCD(opportunityProducts);
        opportunityProducts.TaskID = new int?(dictionary1[itemTaskCd]);
        ((PXSelectBase<CROpportunityProducts>) this.Products).Update(opportunityProducts);
        if (settings.CreateLaborRates.GetValueOrDefault())
        {
          int? nullable3 = opportunityProducts.InventoryID;
          if (nullable3.HasValue)
          {
            nullable3 = opportunityProducts.EmployeeID;
            if (nullable3.HasValue && opportunityProducts.UnitCost.HasValue)
            {
              PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select<CROpportunityProducts.inventoryID>(((PXSelectBase) this.Products).Cache, (object) opportunityProducts);
              if (inventoryItem != null && inventoryItem.ItemType == "L")
              {
                PMQuoteMaint.LaborRateKey key;
                ref PMQuoteMaint.LaborRateKey local = ref key;
                nullable3 = opportunityProducts.ProjectID;
                int projectID = nullable3.Value;
                nullable3 = opportunityProducts.TaskID;
                int projectTaskID = nullable3.Value;
                nullable3 = opportunityProducts.InventoryID;
                int inventoryID = nullable3.Value;
                nullable3 = opportunityProducts.EmployeeID;
                int employeeID = nullable3.Value;
                local = new PMQuoteMaint.LaborRateKey(projectID, projectTaskID, inventoryID, employeeID);
                if (!dictionary2.ContainsKey(key))
                  dictionary2.Add(key, opportunityProducts.UnitCost.Value);
                else
                  dictionary2[key] = Math.Max(dictionary2[key], opportunityProducts.UnitCost.Value);
              }
            }
          }
        }
      }
      if (settings.MoveActivities.GetValueOrDefault())
      {
        CREmailActivityMaint instance2 = PXGraph.CreateInstance<CREmailActivityMaint>();
        List<PMTimeActivity> pmTimeActivityList = new List<PMTimeActivity>();
        PMQuoteMaint.PMQuoteMaint_ActivityDetailsExt extension = ((PXGraph) this).GetExtension<PMQuoteMaint.PMQuoteMaint_ActivityDetailsExt>();
        foreach (PXResult<CRPMTimeActivity> pxResult in ((PXSelectBase<CRPMTimeActivity>) extension.Activities).Select(Array.Empty<object>()))
        {
          CRPMTimeActivity crpmTimeActivity = PXResult<CRPMTimeActivity>.op_Implicit(pxResult);
          int? costCodeId;
          if (crpmTimeActivity.TimeActivityRefNoteID.HasValue)
          {
            crpmTimeActivity.ProjectID = row.QuoteProjectID;
            crpmTimeActivity.ProjectTaskID = nullable2;
            costCodeId = crpmTimeActivity.CostCodeID;
            if (!costCodeId.HasValue)
              crpmTimeActivity.CostCodeID = CostCodeAttribute.DefaultCostCode;
            ((PXSelectBase<CRPMTimeActivity>) extension.Activities).Update(crpmTimeActivity);
          }
          else
          {
            PMTimeActivity instance3 = ((PXSelectBase) instance2.TAct).Cache.CreateInstance() as PMTimeActivity;
            instance3.RefNoteID = crpmTimeActivity.NoteID;
            PMTimeActivity pmTimeActivity = ((PXSelectBase<PMTimeActivity>) instance2.TAct).Insert(instance3);
            ((PXSelectBase) instance2.TAct).Cache.SetValueExt<PMTimeActivity.trackTime>((object) pmTimeActivity, (object) true);
            ((PXSelectBase<PMTimeActivity>) instance2.TAct).Update(pmTimeActivity);
            pmTimeActivity.Summary = crpmTimeActivity.Subject;
            pmTimeActivity.IsBillable = new bool?(false);
            costCodeId = pmTimeActivity.CostCodeID;
            if (!costCodeId.HasValue)
              pmTimeActivity.CostCodeID = CostCodeAttribute.DefaultCostCode;
            ((PXSelectBase<PMTimeActivity>) instance2.TAct).Update(pmTimeActivity);
            pmTimeActivityList.Add(pmTimeActivity);
            ((PXAction) instance2.Save).Press();
          }
        }
        ((PXAction) this.Save).Press();
        if (pmTimeActivityList.Count > 0)
        {
          foreach (PMTimeActivity pmTimeActivity in pmTimeActivityList)
          {
            pmTimeActivity.ProjectID = row.QuoteProjectID;
            pmTimeActivity.ProjectTaskID = nullable2;
            ((PXSelectBase<PMTimeActivity>) instance2.TAct).Update(pmTimeActivity);
            ((PXAction) instance2.Save).Press();
          }
        }
      }
      if (settings.CreateLaborRates.GetValueOrDefault())
      {
        LaborCostRateMaint instance4 = PXGraph.CreateInstance<LaborCostRateMaint>();
        ((PXGraph) instance4).Clear();
        ((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) instance4.Filter).Insert();
        ((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) instance4.Filter).Current.ProjectID = row.QuoteProjectID;
        foreach (KeyValuePair<PMQuoteMaint.LaborRateKey, Decimal> keyValuePair in dictionary2)
          ((PXSelectBase<PMLaborCostRate>) instance4.Items).Insert(new PMLaborCostRate()
          {
            Type = "P",
            ProjectID = new int?(keyValuePair.Key.ProjectID),
            TaskID = new int?(keyValuePair.Key.ProjectTaskID),
            EmployeeID = new int?(keyValuePair.Key.EmployeeID),
            InventoryID = new int?(keyValuePair.Key.InventoryID),
            EffectiveDate = row.DocumentDate,
            Rate = new Decimal?(keyValuePair.Value)
          });
        ((PXAction) instance4.Save).Press();
      }
      ((PXAction) this.Save).Press();
      transactionScope.Complete();
    }
  }

  public virtual void DefaultFromTemplate(PMQuote row)
  {
    PMProject pmProject = PMProject.PK.Find((PXGraph) this, row.TemplateID);
    row.BranchID = this.GetBranchForQuoteAndTemplate(row, pmProject);
    if (pmProject == null)
      return;
    PX.Objects.CR.Standalone.EPEmployee epEmployee = PXResultset<PX.Objects.CR.Standalone.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.EPEmployee, PXSelect<PX.Objects.CR.Standalone.EPEmployee, Where<PX.Objects.CR.Standalone.EPEmployee.defContactID, Equal<Required<PX.Objects.CR.Standalone.EPEmployee.defContactID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) pmProject.OwnerID
    }));
    if (epEmployee != null)
      row.ProjectManager = epEmployee.BAccountID;
    this.Answers.CopyAllAttributes((object) row, (object) pmProject);
    foreach (CSAnswers csAnswers in ((PXSelectBase) this.Answers).Cache.Inserted)
    {
      Guid? refNoteId = csAnswers.RefNoteID;
      Guid? noteId = pmProject.NoteID;
      if ((refNoteId.HasValue == noteId.HasValue ? (refNoteId.HasValue ? (refNoteId.GetValueOrDefault() == noteId.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        this.Answers.Delete(csAnswers);
    }
    string note = PXNoteAttribute.GetNote(((PXSelectBase) this.Quote).Cache, (object) row);
    PXNoteAttribute.CopyNoteAndFiles(((PXGraph) this).Caches[typeof (PMProject)], (object) pmProject, ((PXSelectBase) this.Quote).Cache, (object) row, new bool?(string.IsNullOrWhiteSpace(note)), new bool?(true));
  }

  protected int? GetBranchForQuoteAndTemplate(PMQuote quote, PMProject template)
  {
    int? branchId = quote.BranchID;
    PX.Objects.CR.CROpportunity crOpportunity = PX.Objects.CR.CROpportunity.PK.Find((PXGraph) this, quote.OpportunityID);
    int? nullable1;
    int num;
    if (crOpportunity == null)
    {
      num = 0;
    }
    else
    {
      nullable1 = crOpportunity.BranchID;
      num = nullable1.HasValue ? 1 : 0;
    }
    if (num != 0)
      return branchId;
    int? nullable2;
    if (template == null)
    {
      nullable1 = new int?();
      nullable2 = nullable1;
    }
    else
      nullable2 = template.DefaultBranchID;
    return nullable2 ?? branchId ?? PXAccess.GetBranchID();
  }

  public virtual void DeleteNoteAndFilesOfTemplate(PMQuote quote, int? templateId)
  {
    if (!templateId.HasValue)
      return;
    PMProject pmProject = PMProject.PK.Find((PXGraph) this, templateId);
    if (pmProject == null)
      return;
    PXCache cach = ((PXGraph) this).Caches[typeof (PMProject)];
    string note1 = PXNoteAttribute.GetNote(((PXSelectBase) this.Quote).Cache, (object) quote);
    if (!string.IsNullOrWhiteSpace(note1))
    {
      string note2 = PXNoteAttribute.GetNote(cach, (object) pmProject);
      if (string.Equals(note1, note2, StringComparison.Ordinal))
        PXNoteAttribute.SetNote(((PXSelectBase) this.Quote).Cache, (object) quote, (string) null);
    }
    Guid[] fileNotes1 = PXNoteAttribute.GetFileNotes(cach, (object) pmProject);
    if (fileNotes1.Length == 0)
      return;
    Guid[] fileNotes2 = PXNoteAttribute.GetFileNotes(((PXSelectBase) this.Quote).Cache, (object) quote);
    if (fileNotes2.Length == 0)
      return;
    IEnumerable<Guid> source = ((IEnumerable<Guid>) fileNotes1).Intersect<Guid>((IEnumerable<Guid>) fileNotes2);
    if (!source.Any<Guid>())
      return;
    foreach (PXResult<NoteDoc> pxResult in ((PXSelectBase<NoteDoc>) new PXSelect<NoteDoc, Where<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>, And<NoteDoc.fileID, In<Required<NoteDoc.fileID>>>>>((PXGraph) this)).Select(new object[2]
    {
      (object) quote.NoteID,
      (object) source.ToArray<Guid>()
    }))
      ((PXSelectBase<NoteDoc>) this.QuoteNotes).Delete(PXResult<NoteDoc>.op_Implicit(pxResult));
  }

  public virtual void DeleteAllTasks()
  {
    foreach (PXResult<PMQuoteTask> pxResult in ((PXSelectBase<PMQuoteTask>) this.Tasks).Select(Array.Empty<object>()))
      ((PXSelectBase<PMQuoteTask>) this.Tasks).Delete(PXResult<PMQuoteTask>.op_Implicit(pxResult));
  }

  public virtual void RedefaultTasksFromTemplate(PMQuote row)
  {
    this.DeleteAllTasks();
    foreach (PXResult<PMTask> pxResult in ((PXSelectBase<PMTask>) new PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.autoIncludeInPrj, Equal<True>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) row.TemplateID
    }))
    {
      PMTask projectTask = PXResult<PMTask>.op_Implicit(pxResult);
      this.InsertNewTaskWithProjectTask(row, projectTask);
    }
  }

  public virtual PMQuoteTask InsertNewTaskWithProjectTask(PMQuote projectQuote, PMTask projectTask)
  {
    return this.InsertNewTaskWithProjectTask(projectQuote, projectTask, (Action<PMQuoteTask, PMTask>) null);
  }

  public virtual PMQuoteTask InsertNewTaskWithProjectTask(
    PMQuote projectQuote,
    PMTask projectTask,
    Action<PMQuoteTask, PMTask> extension)
  {
    PMQuoteTask pmQuoteTask1 = new PMQuoteTask()
    {
      QuoteID = projectQuote.QuoteID,
      TaskCD = projectTask.TaskCD,
      Description = projectTask.Description,
      IsDefault = projectTask.IsDefault,
      TaxCategoryID = projectTask.TaxCategoryID
    };
    if (extension != null)
      extension(pmQuoteTask1, projectTask);
    PMQuoteTask pmQuoteTask2 = ((PXSelectBase<PMQuoteTask>) this.Tasks).Insert(pmQuoteTask1);
    if (pmQuoteTask2 == null)
      return (PMQuoteTask) null;
    PXCache cach = ((PXGraph) this).Caches[typeof (PMTask)];
    PXNoteAttribute.CopyNoteAndFiles(cach, (object) projectTask, ((PXSelectBase) this.Tasks).Cache, (object) pmQuoteTask2, (PXNoteAttribute.IPXCopySettings) null);
    PXDBLocalizableStringAttribute.CopyTranslations<PMTask.description, PMQuoteTask.description>(cach, (object) projectTask, ((PXSelectBase) this.Tasks).Cache, (object) pmQuoteTask2);
    return pmQuoteTask2;
  }

  public virtual void AddingBillingInfoToProject(PMQuote row, ProjectEntry projectEntry)
  {
    CRContact crContact = ((PXSelectBase<CRContact>) this.Quote_Contact).SelectSingle(Array.Empty<object>());
    PMBillingContact pmBillingContact1 = PXResultset<PMBillingContact>.op_Implicit(((PXSelectBase<PMBillingContact>) projectEntry.Billing_Contact).Select(Array.Empty<object>()));
    if (crContact != null && pmBillingContact1 != null)
    {
      pmBillingContact1.FullName = crContact.FullName;
      pmBillingContact1.Salutation = crContact.Salutation;
      pmBillingContact1.Phone1 = crContact.Phone1;
      pmBillingContact1.Email = crContact.Email;
      PMBillingContact pmBillingContact2 = ((PXSelectBase<PMBillingContact>) projectEntry.Billing_Contact).Update(pmBillingContact1);
      pmBillingContact2.IsDefaultContact = crContact.IsDefaultContact;
      ((PXSelectBase<PMBillingContact>) projectEntry.Billing_Contact).Update(pmBillingContact2);
    }
    CRAddress crAddress = ((PXSelectBase<CRAddress>) this.Quote_Address).SelectSingle(Array.Empty<object>());
    PMBillingAddress pmBillingAddress1 = PXResultset<PMBillingAddress>.op_Implicit(((PXSelectBase<PMBillingAddress>) projectEntry.Billing_Address).Select(Array.Empty<object>()));
    if (crAddress == null || pmBillingAddress1 == null)
      return;
    pmBillingAddress1.AddressLine1 = crAddress.AddressLine1;
    pmBillingAddress1.AddressLine2 = crAddress.AddressLine2;
    pmBillingAddress1.City = crAddress.City;
    pmBillingAddress1.CountryID = crAddress.CountryID;
    pmBillingAddress1.State = crAddress.State;
    pmBillingAddress1.PostalCode = crAddress.PostalCode;
    pmBillingAddress1.Department = crAddress.Department;
    pmBillingAddress1.SubDepartment = crAddress.SubDepartment;
    pmBillingAddress1.StreetName = crAddress.StreetName;
    pmBillingAddress1.BuildingNumber = crAddress.BuildingNumber;
    pmBillingAddress1.BuildingName = crAddress.BuildingName;
    pmBillingAddress1.Floor = crAddress.Floor;
    pmBillingAddress1.UnitNumber = crAddress.UnitNumber;
    pmBillingAddress1.PostBox = crAddress.PostBox;
    pmBillingAddress1.Room = crAddress.Room;
    pmBillingAddress1.TownLocationName = crAddress.TownLocationName;
    pmBillingAddress1.DistrictName = crAddress.DistrictName;
    PMBillingAddress pmBillingAddress2 = ((PXSelectBase<PMBillingAddress>) projectEntry.Billing_Address).Update(pmBillingAddress1);
    pmBillingAddress2.IsDefaultAddress = crAddress.IsDefaultAddress;
    ((PXSelectBase<PMBillingAddress>) projectEntry.Billing_Address).Update(pmBillingAddress2);
  }

  public virtual void AddingShippingInfoToProject(PMQuote row, ProjectEntry projectEntry)
  {
    CRShippingAddress crShippingAddress = ((PXSelectBase<CRShippingAddress>) this.Shipping_Address).SelectSingle(Array.Empty<object>());
    PMSiteAddress pmSiteAddress = PXResultset<PMSiteAddress>.op_Implicit(((PXSelectBase<PMSiteAddress>) projectEntry.Site_Address).Select(Array.Empty<object>()));
    if (crShippingAddress == null || pmSiteAddress == null)
      return;
    pmSiteAddress.AddressLine1 = crShippingAddress.AddressLine1;
    pmSiteAddress.AddressLine2 = crShippingAddress.AddressLine2;
    pmSiteAddress.City = crShippingAddress.City;
    pmSiteAddress.CountryID = crShippingAddress.CountryID;
    pmSiteAddress.State = crShippingAddress.State;
    pmSiteAddress.PostalCode = crShippingAddress.PostalCode;
    pmSiteAddress.Latitude = crShippingAddress.Latitude;
    pmSiteAddress.Longitude = crShippingAddress.Longitude;
    pmSiteAddress.Department = crShippingAddress.Department;
    pmSiteAddress.SubDepartment = crShippingAddress.SubDepartment;
    pmSiteAddress.StreetName = crShippingAddress.StreetName;
    pmSiteAddress.BuildingNumber = crShippingAddress.BuildingNumber;
    pmSiteAddress.BuildingName = crShippingAddress.BuildingName;
    pmSiteAddress.Floor = crShippingAddress.Floor;
    pmSiteAddress.UnitNumber = crShippingAddress.UnitNumber;
    pmSiteAddress.PostBox = crShippingAddress.PostBox;
    pmSiteAddress.Room = crShippingAddress.Room;
    pmSiteAddress.TownLocationName = crShippingAddress.TownLocationName;
    pmSiteAddress.DistrictName = crShippingAddress.DistrictName;
    ((PXSelectBase<PMSiteAddress>) projectEntry.Site_Address).Update(pmSiteAddress);
  }

  public virtual void AddingTasksToProject(
    PMQuote row,
    ProjectEntry projectEntry,
    Dictionary<string, int> taskMap,
    bool? copyNotes,
    bool? copyFiles)
  {
    Dictionary<int, int> templateToNewTaskMap = new Dictionary<int, int>();
    this.AddingTasksToProject(row, projectEntry, taskMap, templateToNewTaskMap, copyNotes, copyFiles);
  }

  public virtual void AddingTasksToProject(
    PMQuote row,
    ProjectEntry projectEntry,
    Dictionary<string, int> taskMap,
    Dictionary<int, int> templateToNewTaskMap,
    bool? copyNotes,
    bool? copyFiles)
  {
    PXDimensionAttribute.SuppressAutoNumbering<PMTask.taskCD>(((PXSelectBase) projectEntry.Tasks).Cache, true);
    bool flag = false;
    PXResultset<PMQuoteTask> pxResultset = ((PXSelectBase<PMQuoteTask>) this.Tasks).Select(Array.Empty<object>());
    foreach (PXResult<PMQuoteTask> pxResult in pxResultset)
    {
      PMQuoteTask quoteTask = PXResult<PMQuoteTask>.op_Implicit(pxResult);
      flag = true;
      if (pxResultset.Count == 1)
        quoteTask.IsDefault = new bool?(true);
      PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.taskCD, Equal<Required<PMTask.taskCD>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) row.TemplateID,
        (object) quoteTask.TaskCD
      }));
      int? nullable;
      PMTask newTask;
      if (pmTask != null)
      {
        ProjectEntry projectEntry1 = projectEntry;
        PMTask task = pmTask;
        nullable = ((PXSelectBase<PMProject>) projectEntry.Project).Current.ContractID;
        int valueOrDefault1 = nullable.GetValueOrDefault();
        newTask = projectEntry1.CopyTask(task, valueOrDefault1, new ProjectEntry.DefaultFromTemplateSettings()
        {
          CopyProperties = true,
          CopyAttributes = true,
          CopyEmployees = false,
          CopyEquipment = false,
          CopyNotification = false,
          CopyRecurring = true,
          CopyNotes = copyNotes,
          CopyFiles = copyFiles
        });
        this.ConfigureNewTask(projectEntry, newTask, quoteTask);
        PXNoteAttribute.CopyNoteAndFiles(((PXSelectBase) this.Tasks).Cache, (object) quoteTask, ((PXSelectBase) projectEntry.Tasks).Cache, (object) newTask, copyNotes, copyFiles);
        Dictionary<int, int> dictionary = templateToNewTaskMap;
        nullable = pmTask.TaskID;
        int valueOrDefault2 = nullable.GetValueOrDefault();
        nullable = newTask.TaskID;
        int valueOrDefault3 = nullable.GetValueOrDefault();
        dictionary.Add(valueOrDefault2, valueOrDefault3);
      }
      else
        newTask = ((PXSelectBase<PMTask>) projectEntry.Tasks).Insert(this.ConfigureNewTask(projectEntry, new PMTask(), quoteTask));
      Dictionary<string, int> dictionary1 = taskMap;
      string taskCd = quoteTask.TaskCD;
      nullable = newTask.TaskID;
      int valueOrDefault = nullable.GetValueOrDefault();
      dictionary1.Add(taskCd, valueOrDefault);
    }
    if (flag)
      return;
    PMTask pmTask1 = ((PXSelectBase<PMTask>) projectEntry.Tasks).Insert(new PMTask()
    {
      TaskCD = "0",
      IsDefault = new bool?(true)
    });
    ((PXSelectBase) projectEntry.Tasks).Cache.SetValueExt<PMTask.description>((object) pmTask1, (object) "Default");
    taskMap.Add(pmTask1.TaskCD, pmTask1.TaskID.GetValueOrDefault());
  }

  protected virtual void AddingDefaultTaskGLAccountToProject(
    ProjectEntry projectEntry,
    Dictionary<int, int> templateToNewTaskMap)
  {
    int? templateId = ((PXSelectBase<PMProject>) projectEntry.Project).Current.TemplateID;
    int? contractId = ((PXSelectBase<PMProject>) projectEntry.Project).Current.ContractID;
    foreach (PMAccountTask pmAccountTask1 in GraphHelper.RowCast<PMAccountTask>((IEnumerable) PXSelectBase<PMAccountTask, PXViewOf<PMAccountTask>.BasedOn<SelectFromBase<PMAccountTask, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMAccountTask.projectID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) templateId
    })))
    {
      if (templateToNewTaskMap.ContainsKey(pmAccountTask1.TaskID.GetValueOrDefault()))
      {
        PMAccountTask pmAccountTask2 = new PMAccountTask()
        {
          ProjectID = contractId,
          AccountID = pmAccountTask1.AccountID,
          TaskID = new int?(templateToNewTaskMap[pmAccountTask1.TaskID.GetValueOrDefault()])
        };
        ((PXSelectBase<PMAccountTask>) projectEntry.Accounts).Insert(pmAccountTask2);
      }
    }
  }

  public virtual PMTask ConfigureNewTask(
    ProjectEntry projectEntry,
    PMTask newTask,
    PMQuoteTask quoteTask)
  {
    newTask.TaskCD = quoteTask.TaskCD;
    newTask.Description = quoteTask.Description;
    if (projectEntry != null)
      PXDBLocalizableStringAttribute.CopyTranslations<PMQuoteTask.description, PMTask.description>(((PXSelectBase) this.Tasks).Cache, (object) quoteTask, ((PXSelectBase) projectEntry.Tasks).Cache, (object) newTask);
    newTask.PlannedStartDate = quoteTask.PlannedStartDate;
    newTask.PlannedEndDate = quoteTask.PlannedEndDate;
    newTask.IsDefault = quoteTask.IsDefault;
    newTask.TaxCategoryID = quoteTask.TaxCategoryID;
    PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXViewOf<PMTask>.BasedOn<SelectFromBase<PMTask, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMTask.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.nonProject, Equal<True>>>>>.And<BqlOperand<PMTask.taskCD, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) quoteTask.TaskCD
    }));
    if (pmTask != null)
    {
      newTask.DefaultSalesAccountID = pmTask.DefaultSalesAccountID;
      newTask.DefaultSalesSubID = pmTask.DefaultSalesSubID;
      newTask.DefaultExpenseAccountID = pmTask.DefaultExpenseAccountID;
      newTask.DefaultExpenseSubID = pmTask.DefaultExpenseSubID;
      newTask.DefaultAccrualAccountID = pmTask.DefaultAccrualAccountID;
      newTask.DefaultAccrualSubID = pmTask.DefaultAccrualSubID;
    }
    return newTask;
  }

  public virtual void AddingCostBudgetToProject(
    ProjectEntry projectEntry,
    Dictionary<string, int> taskMap,
    string budgetLevel)
  {
    Dictionary<BudgetKeyTuple, PMCostBudget> dictionary = new Dictionary<BudgetKeyTuple, PMCostBudget>();
    HashSet<BudgetKeyTuple> budgetKeyTupleSet = new HashSet<BudgetKeyTuple>();
    foreach (PXResult<CROpportunityProducts> pxResult in ((PXSelectBase<CROpportunityProducts>) this.Products).Select(Array.Empty<object>()))
    {
      CROpportunityProducts opportunityProducts = PXResult<CROpportunityProducts>.op_Implicit(pxResult);
      int? nullable1 = opportunityProducts.ExpenseAccountGroupID;
      if (nullable1.HasValue)
      {
        string itemTaskCd = this.GetItemTaskCD(opportunityProducts);
        int emptyInventoryId1 = PMInventorySelectorAttribute.EmptyInventoryID;
        bool flag = true;
        if (EnumerableExtensions.IsIn<string>(budgetLevel, "I", "D"))
        {
          nullable1 = opportunityProducts.InventoryID;
          if (nullable1.HasValue)
          {
            nullable1 = opportunityProducts.InventoryID;
            emptyInventoryId1 = nullable1.Value;
            flag = emptyInventoryId1 == PMInventorySelectorAttribute.EmptyInventoryID;
          }
        }
        int defaultCostCode = CostCodeAttribute.GetDefaultCostCode();
        if (EnumerableExtensions.IsIn<string>(budgetLevel, "C", "D"))
        {
          nullable1 = opportunityProducts.CostCodeID;
          if (nullable1.HasValue)
          {
            nullable1 = opportunityProducts.CostCodeID;
            defaultCostCode = nullable1.Value;
          }
        }
        BudgetKeyTuple key;
        ref BudgetKeyTuple local1 = ref key;
        nullable1 = ((PXSelectBase<PMProject>) projectEntry.Project).Current.ContractID;
        int valueOrDefault1 = nullable1.GetValueOrDefault();
        int task = taskMap[itemTaskCd];
        nullable1 = opportunityProducts.ExpenseAccountGroupID;
        int valueOrDefault2 = nullable1.GetValueOrDefault();
        int inventoryID = emptyInventoryId1;
        int costCodeID = defaultCostCode;
        local1 = new BudgetKeyTuple(valueOrDefault1, task, valueOrDefault2, inventoryID, costCodeID);
        PMCostBudget pmCostBudget1;
        Decimal? nullable2;
        Decimal? nullable3;
        if (dictionary.TryGetValue(key, out pmCostBudget1))
        {
          budgetKeyTupleSet.Add(key);
          if (pmCostBudget1.UOM != null && pmCostBudget1.UOM != opportunityProducts.UOM)
          {
            string str = opportunityProducts.UOM;
            if (flag && string.IsNullOrEmpty(opportunityProducts.UOM))
              str = ((PXSelectBase<PMSetup>) this.Setup).Current.EmptyItemUOM;
            if (PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>())
            {
              nullable1 = pmCostBudget1.InventoryID;
              int emptyInventoryId2 = PMInventorySelectorAttribute.EmptyInventoryID;
              if (!(nullable1.GetValueOrDefault() == emptyInventoryId2 & nullable1.HasValue))
              {
                try
                {
                  PXCache cach = ((PXGraph) this).Caches[typeof (PMQuote)];
                  int? inventoryId = opportunityProducts.InventoryID;
                  string uom = opportunityProducts.UOM;
                  nullable2 = opportunityProducts.Qty;
                  Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
                  Decimal num1 = INUnitAttribute.ConvertToBase(cach, inventoryId, uom, valueOrDefault3, INPrecision.QUANTITY);
                  PMCostBudget pmCostBudget2 = pmCostBudget1;
                  nullable2 = pmCostBudget2.Qty;
                  Decimal num2 = INUnitAttribute.ConvertFromBase(((PXGraph) this).Caches[typeof (PMQuote)], opportunityProducts.InventoryID, pmCostBudget1.UOM, num1, INPrecision.QUANTITY);
                  Decimal? nullable4;
                  if (!nullable2.HasValue)
                  {
                    nullable3 = new Decimal?();
                    nullable4 = nullable3;
                  }
                  else
                    nullable4 = new Decimal?(nullable2.GetValueOrDefault() + num2);
                  pmCostBudget2.Qty = nullable4;
                  goto label_31;
                }
                catch (PXUnitConversionException ex)
                {
                  pmCostBudget1.Qty = new Decimal?(0M);
                  pmCostBudget1.UOM = (string) null;
                  goto label_31;
                }
              }
            }
            ProjectEntry graph = projectEntry;
            string from = str;
            string uom1 = pmCostBudget1.UOM;
            nullable2 = opportunityProducts.Qty;
            Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
            Decimal num3;
            ref Decimal local2 = ref num3;
            if (INUnitAttribute.TryConvertGlobalUnits((PXGraph) graph, from, uom1, valueOrDefault4, INPrecision.QUANTITY, out local2))
            {
              PMCostBudget pmCostBudget3 = pmCostBudget1;
              nullable2 = pmCostBudget3.Qty;
              Decimal num4 = num3;
              Decimal? nullable5;
              if (!nullable2.HasValue)
              {
                nullable3 = new Decimal?();
                nullable5 = nullable3;
              }
              else
                nullable5 = new Decimal?(nullable2.GetValueOrDefault() + num4);
              pmCostBudget3.Qty = nullable5;
            }
            else
            {
              pmCostBudget1.Qty = new Decimal?(0M);
              pmCostBudget1.UOM = (string) null;
            }
          }
          else if (pmCostBudget1.UOM != null)
          {
            PMCostBudget pmCostBudget4 = pmCostBudget1;
            nullable2 = pmCostBudget4.Qty;
            nullable3 = opportunityProducts.Qty;
            Decimal valueOrDefault5 = nullable3.GetValueOrDefault();
            Decimal? nullable6;
            if (!nullable2.HasValue)
            {
              nullable3 = new Decimal?();
              nullable6 = nullable3;
            }
            else
              nullable6 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault5);
            pmCostBudget4.Qty = nullable6;
          }
label_31:
          PMCostBudget pmCostBudget5 = pmCostBudget1;
          nullable2 = pmCostBudget5.CuryAmount;
          nullable3 = opportunityProducts.ExtCost;
          Decimal valueOrDefault6 = nullable3.GetValueOrDefault();
          Decimal? nullable7;
          if (!nullable2.HasValue)
          {
            nullable3 = new Decimal?();
            nullable7 = nullable3;
          }
          else
            nullable7 = new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault6);
          pmCostBudget5.CuryAmount = nullable7;
          PMCostBudget pmCostBudget6 = pmCostBudget1;
          nullable2 = pmCostBudget6.CuryMaxAmount;
          nullable3 = opportunityProducts.Qty;
          Decimal valueOrDefault7 = nullable3.GetValueOrDefault();
          nullable3 = opportunityProducts.CuryUnitPrice;
          Decimal valueOrDefault8 = nullable3.GetValueOrDefault();
          Decimal num = valueOrDefault7 * valueOrDefault8;
          Decimal? nullable8;
          if (!nullable2.HasValue)
          {
            nullable3 = new Decimal?();
            nullable8 = nullable3;
          }
          else
            nullable8 = new Decimal?(nullable2.GetValueOrDefault() + num);
          pmCostBudget6.CuryMaxAmount = nullable8;
        }
        else
        {
          pmCostBudget1 = new PMCostBudget();
          pmCostBudget1.ProjectID = new int?(key.ProjectID);
          pmCostBudget1.ProjectTaskID = new int?(key.ProjectTaskID);
          PMTask dirty = PMTask.PK.FindDirty((PXGraph) projectEntry, pmCostBudget1.ProjectID, pmCostBudget1.ProjectTaskID);
          if (dirty != null && dirty.Type != "Cost")
            pmCostBudget1.RevenueTaskID = new int?(key.ProjectTaskID);
          pmCostBudget1.AccountGroupID = new int?(key.AccountGroupID);
          pmCostBudget1.InventoryID = new int?(key.InventoryID);
          pmCostBudget1.CostCodeID = new int?(key.CostCodeID);
          PMCostBudget pmCostBudget7 = pmCostBudget1;
          nullable2 = opportunityProducts.CuryExtCost;
          Decimal? nullable9 = new Decimal?(nullable2.GetValueOrDefault());
          pmCostBudget7.CuryAmount = nullable9;
          PMCostBudget pmCostBudget8 = pmCostBudget1;
          nullable2 = opportunityProducts.DiscPct;
          Decimal num5 = 0M;
          Decimal? nullable10;
          if (nullable2.GetValueOrDefault() == num5 & nullable2.HasValue)
          {
            nullable10 = opportunityProducts.CuryUnitPrice;
          }
          else
          {
            nullable3 = opportunityProducts.CuryUnitPrice;
            Decimal num6 = (Decimal) 100;
            Decimal? nullable11 = opportunityProducts.DiscPct;
            Decimal? nullable12 = nullable11.HasValue ? new Decimal?(num6 - nullable11.GetValueOrDefault()) : new Decimal?();
            Decimal? nullable13;
            if (!(nullable3.HasValue & nullable12.HasValue))
            {
              nullable11 = new Decimal?();
              nullable13 = nullable11;
            }
            else
              nullable13 = new Decimal?(nullable3.GetValueOrDefault() * nullable12.GetValueOrDefault());
            nullable2 = nullable13;
            Decimal num7 = (Decimal) 100;
            if (!nullable2.HasValue)
            {
              nullable12 = new Decimal?();
              nullable10 = nullable12;
            }
            else
              nullable10 = new Decimal?(nullable2.GetValueOrDefault() / num7);
          }
          pmCostBudget8.CuryUnitPrice = nullable10;
          pmCostBudget1.Description = opportunityProducts.Descr;
          if (flag && string.IsNullOrEmpty(opportunityProducts.UOM))
            pmCostBudget1.UOM = ((PXSelectBase<PMSetup>) this.Setup).Current.EmptyItemUOM;
          else
            pmCostBudget1.UOM = opportunityProducts.UOM;
          PMCostBudget pmCostBudget9 = pmCostBudget1;
          nullable2 = opportunityProducts.Qty;
          Decimal? nullable14 = new Decimal?(nullable2.GetValueOrDefault());
          pmCostBudget9.Qty = nullable14;
          PMCostBudget pmCostBudget10 = pmCostBudget1;
          nullable2 = opportunityProducts.Qty;
          Decimal valueOrDefault9 = nullable2.GetValueOrDefault();
          nullable2 = pmCostBudget1.CuryUnitPrice;
          Decimal valueOrDefault10 = nullable2.GetValueOrDefault();
          Decimal? nullable15 = new Decimal?(valueOrDefault9 * valueOrDefault10);
          pmCostBudget10.CuryMaxAmount = nullable15;
          dictionary.Add(key, pmCostBudget1);
        }
      }
    }
    foreach (PMCostBudget budget in dictionary.Values)
    {
      Decimal? nullable16 = budget.Qty;
      if (nullable16.GetValueOrDefault() != 0M)
      {
        PMCostBudget pmCostBudget = budget;
        nullable16 = budget.CuryAmount;
        Decimal num = budget.Qty.Value;
        Decimal? nullable17 = nullable16.HasValue ? new Decimal?(nullable16.GetValueOrDefault() / num) : new Decimal?();
        pmCostBudget.CuryUnitRate = nullable17;
      }
      nullable16 = budget.Qty;
      if (nullable16.GetValueOrDefault() != 0M)
      {
        PMCostBudget pmCostBudget = budget;
        nullable16 = budget.CuryMaxAmount;
        Decimal num = budget.Qty.Value;
        Decimal? nullable18 = nullable16.HasValue ? new Decimal?(nullable16.GetValueOrDefault() / num) : new Decimal?();
        pmCostBudget.CuryUnitPrice = nullable18;
      }
      PMCostBudget pmCostBudget11 = budget;
      nullable16 = new Decimal?();
      Decimal? nullable19 = nullable16;
      pmCostBudget11.CuryMaxAmount = nullable19;
      string description = budget.Description;
      PMCostBudget pmCostBudget12 = ((PXSelectBase<PMCostBudget>) projectEntry.CostBudget).Insert(budget);
      pmCostBudget12.UOM = budget.UOM;
      if (!budgetKeyTupleSet.Contains(BudgetKeyTuple.Create((IProjectFilter) budget)))
      {
        pmCostBudget12.Description = description;
      }
      else
      {
        PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find((PXGraph) this, budget.AccountGroupID);
        if (pmAccountGroup != null)
          pmCostBudget12.Description = PXMessages.LocalizeNoPrefix("Aggregated: ") + pmAccountGroup.Description;
      }
    }
  }

  public virtual void AddingRevenueBudgetToProject(
    ProjectEntry projectEntry,
    Dictionary<string, int> taskMap,
    string budgetLevel)
  {
    Dictionary<BudgetKeyTuple, PMRevenueBudget> dictionary = new Dictionary<BudgetKeyTuple, PMRevenueBudget>();
    HashSet<BudgetKeyTuple> budgetKeyTupleSet = new HashSet<BudgetKeyTuple>();
    foreach (PXResult<CROpportunityProducts> pxResult in ((PXSelectBase<CROpportunityProducts>) this.Products).Select(Array.Empty<object>()))
    {
      CROpportunityProducts opportunityProducts = PXResult<CROpportunityProducts>.op_Implicit(pxResult);
      int? nullable1 = opportunityProducts.RevenueAccountGroupID;
      if (nullable1.HasValue)
      {
        string itemTaskCd = this.GetItemTaskCD(opportunityProducts);
        int emptyInventoryId1 = PMInventorySelectorAttribute.EmptyInventoryID;
        bool flag = true;
        if (EnumerableExtensions.IsIn<string>(budgetLevel, "I", "D"))
        {
          nullable1 = opportunityProducts.InventoryID;
          if (nullable1.HasValue)
          {
            nullable1 = opportunityProducts.InventoryID;
            emptyInventoryId1 = nullable1.Value;
            flag = emptyInventoryId1 == PMInventorySelectorAttribute.EmptyInventoryID;
          }
        }
        int defaultCostCode = CostCodeAttribute.GetDefaultCostCode();
        if (EnumerableExtensions.IsIn<string>(budgetLevel, "C", "D"))
        {
          nullable1 = opportunityProducts.CostCodeID;
          if (nullable1.HasValue)
          {
            nullable1 = opportunityProducts.CostCodeID;
            defaultCostCode = nullable1.Value;
          }
        }
        BudgetKeyTuple key;
        ref BudgetKeyTuple local1 = ref key;
        nullable1 = ((PXSelectBase<PMProject>) projectEntry.Project).Current.ContractID;
        int valueOrDefault1 = nullable1.GetValueOrDefault();
        int task = taskMap[itemTaskCd];
        nullable1 = opportunityProducts.RevenueAccountGroupID;
        int accountGroupID = nullable1.Value;
        int inventoryID = emptyInventoryId1;
        int costCodeID = defaultCostCode;
        local1 = new BudgetKeyTuple(valueOrDefault1, task, accountGroupID, inventoryID, costCodeID);
        PMRevenueBudget pmRevenueBudget1;
        Decimal? nullable2;
        if (dictionary.TryGetValue(key, out pmRevenueBudget1))
        {
          budgetKeyTupleSet.Add(key);
          if (pmRevenueBudget1.TaxCategoryID != null && opportunityProducts.TaxCategoryID != null && pmRevenueBudget1.TaxCategoryID != opportunityProducts.TaxCategoryID)
            pmRevenueBudget1.TaxCategoryID = (string) null;
          if (pmRevenueBudget1.UOM != null && pmRevenueBudget1.UOM != opportunityProducts.UOM)
          {
            string str = opportunityProducts.UOM;
            if (flag && string.IsNullOrEmpty(opportunityProducts.UOM))
              str = ((PXSelectBase<PMSetup>) this.Setup).Current.EmptyItemUOM;
            if (PXAccess.FeatureInstalled<FeaturesSet.multipleUnitMeasure>())
            {
              nullable1 = pmRevenueBudget1.InventoryID;
              int emptyInventoryId2 = PMInventorySelectorAttribute.EmptyInventoryID;
              if (!(nullable1.GetValueOrDefault() == emptyInventoryId2 & nullable1.HasValue))
              {
                try
                {
                  PXCache cach = ((PXGraph) this).Caches[typeof (PMQuote)];
                  int? inventoryId = opportunityProducts.InventoryID;
                  string uom = opportunityProducts.UOM;
                  nullable2 = opportunityProducts.Qty;
                  Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
                  Decimal num1 = INUnitAttribute.ConvertToBase(cach, inventoryId, uom, valueOrDefault2, INPrecision.QUANTITY);
                  PMRevenueBudget pmRevenueBudget2 = pmRevenueBudget1;
                  nullable2 = pmRevenueBudget2.Qty;
                  Decimal num2 = INUnitAttribute.ConvertFromBase(((PXGraph) this).Caches[typeof (PMQuote)], opportunityProducts.InventoryID, pmRevenueBudget1.UOM, num1, INPrecision.QUANTITY);
                  pmRevenueBudget2.Qty = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num2) : new Decimal?();
                  goto label_24;
                }
                catch (PXUnitConversionException ex)
                {
                  pmRevenueBudget1.Qty = new Decimal?(0M);
                  pmRevenueBudget1.UOM = (string) null;
                  goto label_24;
                }
              }
            }
            ProjectEntry graph = projectEntry;
            string from = str;
            string uom1 = pmRevenueBudget1.UOM;
            nullable2 = opportunityProducts.Qty;
            Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
            Decimal num3;
            ref Decimal local2 = ref num3;
            if (INUnitAttribute.TryConvertGlobalUnits((PXGraph) graph, from, uom1, valueOrDefault3, INPrecision.QUANTITY, out local2))
            {
              PMRevenueBudget pmRevenueBudget3 = pmRevenueBudget1;
              nullable2 = pmRevenueBudget3.Qty;
              Decimal num4 = num3;
              pmRevenueBudget3.Qty = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num4) : new Decimal?();
            }
            else
            {
              pmRevenueBudget1.Qty = new Decimal?(0M);
              pmRevenueBudget1.UOM = (string) null;
            }
          }
          else if (pmRevenueBudget1.UOM != null)
          {
            PMRevenueBudget pmRevenueBudget4 = pmRevenueBudget1;
            nullable2 = pmRevenueBudget4.Qty;
            Decimal valueOrDefault4 = opportunityProducts.Qty.GetValueOrDefault();
            pmRevenueBudget4.Qty = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault4) : new Decimal?();
          }
label_24:
          PMRevenueBudget pmRevenueBudget5 = pmRevenueBudget1;
          nullable2 = pmRevenueBudget5.CuryAmount;
          Decimal valueOrDefault5 = opportunityProducts.CuryAmount.GetValueOrDefault();
          pmRevenueBudget5.CuryAmount = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + valueOrDefault5) : new Decimal?();
        }
        else
        {
          pmRevenueBudget1 = new PMRevenueBudget();
          pmRevenueBudget1.ProjectID = new int?(key.ProjectID);
          pmRevenueBudget1.ProjectTaskID = new int?(key.ProjectTaskID);
          pmRevenueBudget1.AccountGroupID = new int?(key.AccountGroupID);
          pmRevenueBudget1.InventoryID = new int?(key.InventoryID);
          pmRevenueBudget1.CostCodeID = new int?(key.CostCodeID);
          PMRevenueBudget pmRevenueBudget6 = pmRevenueBudget1;
          nullable2 = opportunityProducts.CuryAmount;
          Decimal? nullable3 = new Decimal?(nullable2.GetValueOrDefault());
          pmRevenueBudget6.CuryAmount = nullable3;
          pmRevenueBudget1.TaxCategoryID = opportunityProducts.TaxCategoryID;
          pmRevenueBudget1.Description = opportunityProducts.Descr;
          if (flag && string.IsNullOrEmpty(opportunityProducts.UOM))
            pmRevenueBudget1.UOM = ((PXSelectBase<PMSetup>) this.Setup).Current.EmptyItemUOM;
          else
            pmRevenueBudget1.UOM = opportunityProducts.UOM;
          PMRevenueBudget pmRevenueBudget7 = pmRevenueBudget1;
          nullable2 = opportunityProducts.Qty;
          Decimal? nullable4 = new Decimal?(nullable2.GetValueOrDefault());
          pmRevenueBudget7.Qty = nullable4;
          dictionary.Add(key, pmRevenueBudget1);
        }
      }
    }
    foreach (PMRevenueBudget budget in dictionary.Values)
    {
      Decimal? nullable5 = budget.Qty;
      if (nullable5.GetValueOrDefault() != 0M)
      {
        PMRevenueBudget pmRevenueBudget = budget;
        nullable5 = budget.CuryAmount;
        Decimal num = budget.Qty.Value;
        Decimal? nullable6 = nullable5.HasValue ? new Decimal?(nullable5.GetValueOrDefault() / num) : new Decimal?();
        pmRevenueBudget.CuryUnitRate = nullable6;
      }
      PMRevenueBudget pmRevenueBudget8 = new PMRevenueBudget();
      pmRevenueBudget8.ProjectID = budget.ProjectID;
      pmRevenueBudget8.ProjectTaskID = budget.ProjectTaskID;
      pmRevenueBudget8.AccountGroupID = budget.AccountGroupID;
      pmRevenueBudget8.InventoryID = budget.InventoryID;
      pmRevenueBudget8.CostCodeID = budget.CostCodeID;
      PMRevenueBudget pmRevenueBudget9 = pmRevenueBudget8;
      PMRevenueBudget pmRevenueBudget10 = ((PXSelectBase<PMRevenueBudget>) projectEntry.RevenueBudget).Insert(pmRevenueBudget9);
      if (pmRevenueBudget10 != null)
      {
        pmRevenueBudget10.Qty = budget.Qty;
        pmRevenueBudget10.UOM = budget.UOM;
        pmRevenueBudget10.CuryAmount = budget.CuryAmount;
        nullable5 = budget.CuryUnitRate;
        if (nullable5.HasValue)
          pmRevenueBudget10.CuryUnitRate = budget.CuryUnitRate;
        pmRevenueBudget10.TaxCategoryID = budget.TaxCategoryID;
        if (!budgetKeyTupleSet.Contains(BudgetKeyTuple.Create((IProjectFilter) budget)))
        {
          pmRevenueBudget10.Description = budget.Description;
        }
        else
        {
          PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find((PXGraph) this, budget.AccountGroupID);
          if (pmAccountGroup != null)
            pmRevenueBudget10.Description = PXMessages.LocalizeNoPrefix("Aggregated: ") + pmAccountGroup.Description;
        }
        ((PXSelectBase<PMRevenueBudget>) projectEntry.RevenueBudget).Update(pmRevenueBudget10);
      }
    }
  }

  private string GetItemTaskCD(CROpportunityProducts item)
  {
    string itemTaskCd = item.TaskCD;
    if (string.IsNullOrEmpty(itemTaskCd))
    {
      itemTaskCd = this.GetDefaultTaskCD();
      if (string.IsNullOrEmpty(itemTaskCd))
        itemTaskCd = "0";
    }
    return itemTaskCd;
  }

  protected bool IsQuoteProjectCDEmptyOrNEW(string quoteProjectCD)
  {
    return string.IsNullOrWhiteSpace(quoteProjectCD) || quoteProjectCD.Trim().Equals(PXMessages.LocalizeNoPrefix("<NEW>"), StringComparison.InvariantCultureIgnoreCase);
  }

  public virtual bool ValidateQuoteBeforeSubmit(PMQuote row, out int productErrorsCount)
  {
    return this.ValidateQuote(row, true, out productErrorsCount);
  }

  public virtual bool ValidateQuoteBeforeConvertToProject(PMQuote row, out int productErrorsCount)
  {
    return this.ValidateQuote(row, false, out productErrorsCount);
  }

  public virtual bool ValidateQuote(PMQuote row, bool isSubmitting, out int productErrorsCount)
  {
    bool flag1 = true;
    if (string.IsNullOrEmpty(row.Subject))
    {
      flag1 = false;
      ((PXSelectBase) this.Quote).Cache.RaiseExceptionHandling<PMQuote.subject>((object) row, (object) row.Subject, (Exception) new PXSetPropertyException<PMQuote.subject>("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<PMQuote.subject>(((PXSelectBase) this.Quote).Cache)
      }));
    }
    if (!row.BAccountID.HasValue)
    {
      flag1 = false;
      ((PXSelectBase) this.Quote).Cache.RaiseExceptionHandling<PMQuote.bAccountID>((object) row, (object) row.BAccountID, (Exception) new PXSetPropertyException<PMQuote.bAccountID>("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<PMQuote.bAccountID>(((PXSelectBase) this.Quote).Cache)
      }));
    }
    else if (!isSubmitting && ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current == null)
    {
      flag1 = false;
      ((PXSelectBase) this.Quote).Cache.RaiseExceptionHandling<PMQuote.bAccountID>((object) row, (object) ((PXSelectBase<PX.Objects.CR.BAccount>) this.baccount).Current?.AcctCD, (Exception) new PXSetPropertyException<PMQuote.bAccountID>("You cannot convert the project quote to a project because the type of the business account of the project quote is not Customer. Select a business account of the Customer type to proceed."));
    }
    if (!row.TemplateID.HasValue)
    {
      flag1 = false;
      ((PXSelectBase) this.Quote).Cache.RaiseExceptionHandling<PMQuote.templateID>((object) row, (object) row.TemplateID, (Exception) new PXSetPropertyException<PMQuote.templateID>("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<PMQuote.templateID>(((PXSelectBase) this.Quote).Cache)
      }));
    }
    if (!isSubmitting && this.IsQuoteProjectCDEmptyOrNEW(row.QuoteProjectCD) && !ProjectAttribute.IsAutonumbered((PXGraph) this, "PROJECT"))
    {
      ((PXSelectBase) this.Quote).Cache.RaiseExceptionHandling<PMQuote.quoteProjectCD>((object) row, (object) row.QuoteProjectCD, (Exception) new PXSetPropertyException<PMQuote.quoteProjectCD>("'{0}' cannot be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<PMQuote.quoteProjectCD>(((PXSelectBase) this.Quote).Cache)
      }));
      throw new PXException("{0} '{1}' record raised at least one error. Please review the errors.", new object[2]
      {
        (object) "Updating ",
        (object) ((PXSelectBase) this.Quote).Cache.DisplayName
      });
    }
    bool flag2 = ((PXSelectBase<PMQuoteTask>) this.Tasks).Select(Array.Empty<object>()).Count > 1;
    productErrorsCount = 0;
    foreach (PXResult<CROpportunityProducts> pxResult in ((PXSelectBase<CROpportunityProducts>) this.Products).Select(Array.Empty<object>()))
    {
      CROpportunityProducts line = PXResult<CROpportunityProducts>.op_Implicit(pxResult);
      if (flag2 && string.IsNullOrEmpty(line.TaskCD))
      {
        ++productErrorsCount;
        ((PXSelectBase) this.Products).Cache.RaiseExceptionHandling<CROpportunityProducts.taskCD>((object) line, (object) null, (Exception) new PXSetPropertyException<CROpportunityProducts.taskCD>("'{0}' cannot be empty.", new object[1]
        {
          (object) PXUIFieldAttribute.GetDisplayName<CROpportunityProducts.taskCD>(((PXSelectBase) this.Products).Cache)
        }));
      }
      if (!isSubmitting && line.CuryAmount.GetValueOrDefault() != 0M && !line.RevenueAccountGroupID.HasValue)
      {
        ++productErrorsCount;
        ((PXSelectBase) this.Products).Cache.RaiseExceptionHandling<CROpportunityProducts.revenueAccountGroupID>((object) line, (object) null, (Exception) new PXSetPropertyException<CROpportunityProducts.revenueAccountGroupID>("The amount is non-zero and the revenue account group is empty. The line is not printed in the quote and cannot be converted to the project budget. You need to either specify the revenue account group or set the amount to zero to be able to convert the quote to a project."));
      }
      if (!isSubmitting && line.CuryExtCost.GetValueOrDefault() != 0M && !line.ExpenseAccountGroupID.HasValue)
      {
        ++productErrorsCount;
        ((PXSelectBase) this.Products).Cache.RaiseExceptionHandling<CROpportunityProducts.expenseAccountGroupID>((object) line, (object) null, (Exception) new PXSetPropertyException<CROpportunityProducts.expenseAccountGroupID>("The extended cost is non-zero and the cost account group is empty. The line cannot be converted to the project budget. You need to either specify the cost account group or set the extended cost to zero to be able to convert the quote to a project."));
      }
      if (!isSubmitting && this.GetTaskType(line) == "Cost" && line.RevenueAccountGroupID.HasValue)
      {
        ++productErrorsCount;
        ((PXSelectBase) this.Products).Cache.RaiseExceptionHandling<CROpportunityProducts.taskCD>((object) line, (object) line.TaskCD, (Exception) new PXSetPropertyException<CROpportunityProducts.taskCD>("The estimation line does not correspond to the type of the project task. Remove the revenue-related data in the estimation line or use a task of the cost and revenue type."));
      }
      if (!isSubmitting && this.GetTaskType(line) == "Rev" && line.ExpenseAccountGroupID.HasValue)
      {
        ++productErrorsCount;
        ((PXSelectBase) this.Products).Cache.RaiseExceptionHandling<CROpportunityProducts.taskCD>((object) line, (object) line.TaskCD, (Exception) new PXSetPropertyException<CROpportunityProducts.taskCD>("The estimation line does not correspond to the type of the project task. Remove the cost-related data in the estimation line or use a task of the cost and revenue type."));
      }
    }
    return flag1 && productErrorsCount == 0;
  }

  private string GetTaskType(CROpportunityProducts line)
  {
    if (!string.IsNullOrEmpty(line.TaskCD))
      return PXResultset<PMQuoteTask>.op_Implicit(PXSelectBase<PMQuoteTask, PXSelect<PMQuoteTask, Where<PMQuoteTask.taskCD, Equal<Required<PMQuoteTask.taskCD>>, And<PMQuoteTask.quoteID, Equal<Required<PMQuoteTask.quoteID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) line.TaskCD,
        (object) line.QuoteID
      })).Type;
    PXResultset<PMQuoteTask> pxResultset = ((PXSelectBase<PMQuoteTask>) this.Tasks).Select(Array.Empty<object>());
    if (pxResultset.Count == 0)
      return "CostRev";
    return pxResultset.Count == 1 ? PXResultset<PMQuoteTask>.op_Implicit(pxResultset).Type : (string) null;
  }

  public virtual string GetDefaultTaskCD()
  {
    PXResultset<PMQuoteTask> pxResultset = ((PXSelectBase<PMQuoteTask>) this.Tasks).Select(Array.Empty<object>());
    if (pxResultset.Count == 1)
      return PXResultset<PMQuoteTask>.op_Implicit(pxResultset).TaskCD;
    foreach (PXResult<PMQuoteTask> pxResult in ((PXSelectBase<PMQuoteTask>) this.Tasks).Select(Array.Empty<object>()))
    {
      PMQuoteTask pmQuoteTask = PXResult<PMQuoteTask>.op_Implicit(pxResult);
      if (pmQuoteTask.IsDefault.GetValueOrDefault())
        return pmQuoteTask.TaskCD;
    }
    PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.isDefault, Equal<True>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<PMQuote>) this.Quote).Current.TemplateID
    }));
    if (pmTask != null)
    {
      foreach (PXResult<PMQuoteTask> pxResult in pxResultset)
      {
        PMQuoteTask pmQuoteTask = PXResult<PMQuoteTask>.op_Implicit(pxResult);
        if (pmQuoteTask.TaskCD == pmTask.TaskCD)
          return pmQuoteTask.TaskCD;
      }
    }
    return (string) null;
  }

  public virtual EmployeeCostEngine CreateEmployeeCostEngine()
  {
    return new EmployeeCostEngine((PXGraph) this);
  }

  [PXHidden]
  [Serializable]
  public class CopyQuoteFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _Description;

    [PXDefault]
    [PXString(60, IsUnicode = true)]
    [PXUIField(DisplayName = "Description", Required = true)]
    public virtual string Description
    {
      get => this._Description;
      set => this._Description = value;
    }

    [PXBool]
    [PXUIField(DisplayName = "Recalculate Prices")]
    public virtual bool? RecalculatePrices { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Override Manual Prices", Enabled = false)]
    public virtual bool? OverrideManualPrices { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Recalculate Discounts")]
    public virtual bool? RecalculateDiscounts { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Override Manual Discounts", Enabled = false)]
    public virtual bool? OverrideManualDiscounts { get; set; }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Override Manual Group and Document Discounts")]
    public virtual bool? OverrideManualDocGroupDiscounts { get; set; }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PMQuoteMaint.CopyQuoteFilter.description>
    {
    }

    public abstract class recalculatePrices : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PMQuoteMaint.CopyQuoteFilter.recalculatePrices>
    {
    }

    public abstract class overrideManualPrices : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PMQuoteMaint.CopyQuoteFilter.overrideManualPrices>
    {
    }

    public abstract class recalculateDiscounts : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PMQuoteMaint.CopyQuoteFilter.recalculateDiscounts>
    {
    }

    public abstract class overrideManualDiscounts : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PMQuoteMaint.CopyQuoteFilter.overrideManualDiscounts>
    {
    }

    public abstract class overrideManualDocGroupDiscounts : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PMQuoteMaint.CopyQuoteFilter.overrideManualDocGroupDiscounts>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class ConvertToProjectFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXBool]
    [PXUIField(DisplayName = "Populate Labor Cost Rates")]
    public virtual bool? CreateLaborRates { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Activate Project")]
    public virtual bool? ActivateProject { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Activate Tasks")]
    public virtual bool? ActivateTasks { get; set; }

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Copy Notes to Project")]
    public virtual bool? CopyNotes { get; set; }

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Copy Files to Project")]
    public virtual bool? CopyFiles { get; set; }

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Link Activities to Project")]
    public virtual bool? MoveActivities { get; set; }

    [PXDBString]
    [PXUIField(DisplayName = "Project Task")]
    [PXDimension("PROTASK")]
    [PXSelector(typeof (Search<PMQuoteTask.taskCD, Where<PMQuoteTask.quoteID, Equal<Current<PMQuote.quoteID>>>>))]
    public virtual string TaskCD { get; set; }

    public abstract class createLaborRates : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PMQuoteMaint.ConvertToProjectFilter.createLaborRates>
    {
    }

    public abstract class activateProject : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PMQuoteMaint.ConvertToProjectFilter.activateProject>
    {
    }

    public abstract class activateTasks : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PMQuoteMaint.ConvertToProjectFilter.activateTasks>
    {
    }

    public abstract class copyNotes : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PMQuoteMaint.ConvertToProjectFilter.copyNotes>
    {
    }

    public abstract class copyFiles : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PMQuoteMaint.ConvertToProjectFilter.copyFiles>
    {
    }

    public abstract class moveActivities : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PMQuoteMaint.ConvertToProjectFilter.moveActivities>
    {
    }

    public abstract class taskCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PMQuoteMaint.ConvertToProjectFilter.taskCD>
    {
    }
  }

  [ExcludeFromCodeCoverage]
  [DebuggerDisplay("{ProjectID}.{ProjectTaskID}.{InventoryID}.{EmployeeID}")]
  private struct LaborRateKey(int projectID, int projectTaskID, int inventoryID, int employeeID)
  {
    public readonly int ProjectID = projectID;
    public readonly int ProjectTaskID = projectTaskID;
    public readonly int InventoryID = inventoryID;
    public readonly int EmployeeID = employeeID;

    public override int GetHashCode()
    {
      return (((17 * 23 + this.ProjectID.GetHashCode()) * 23 + this.ProjectTaskID.GetHashCode()) * 23 + this.InventoryID.GetHashCode()) * 23 + this.EmployeeID.GetHashCode();
    }
  }

  public class MultiCurrency : MultiCurrencyGraph<PMQuoteMaint, PMQuote>
  {
    protected override string Module => "PM";

    protected override MultiCurrencyGraph<PMQuoteMaint, PMQuote>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<PMQuoteMaint, PMQuote>.DocumentMapping(typeof (PMQuote))
      {
        DocumentDate = typeof (PMQuote.documentDate)
      };
    }

    protected override MultiCurrencyGraph<PMQuoteMaint, PMQuote>.CurySourceMapping GetCurySourceMapping()
    {
      return new MultiCurrencyGraph<PMQuoteMaint, PMQuote>.CurySourceMapping(typeof (PX.Objects.AR.Customer));
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[4]
      {
        (PXSelectBase) this.Base.Quote,
        (PXSelectBase) this.Base.Products,
        (PXSelectBase) this.Base.TaxLines,
        (PXSelectBase) this.Base.Taxes
      };
    }

    protected override CurySource CurrentSourceSelect()
    {
      CurySource curySource = new CurySource();
      if (PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>() && ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current != null)
      {
        curySource.CuryID = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current.CuryID;
        curySource.CuryRateTypeID = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current.CuryRateTypeID;
      }
      if (PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>() && ((PXSelectBase<PMQuote>) this.Base.QuoteCurrent).Current != null && ((PXSelectBase<PMQuote>) this.Base.QuoteCurrent).Current.Status == "D")
      {
        if (((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current != null)
        {
          curySource.AllowOverrideCury = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current.AllowOverrideCury;
          curySource.AllowOverrideRate = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current.AllowOverrideRate;
        }
        else
        {
          curySource.AllowOverrideCury = new bool?(true);
          curySource.AllowOverrideRate = new bool?(true);
        }
      }
      else
      {
        curySource.AllowOverrideCury = new bool?(false);
        curySource.AllowOverrideRate = new bool?(false);
      }
      return curySource;
    }
  }

  public class SalesPrice : SalesPriceGraph<PMQuoteMaint, PMQuote>
  {
    protected override SalesPriceGraph<PMQuoteMaint, PMQuote>.DocumentMapping GetDocumentMapping()
    {
      return new SalesPriceGraph<PMQuoteMaint, PMQuote>.DocumentMapping(typeof (PMQuote))
      {
        CuryInfoID = typeof (PMQuote.curyInfoID)
      };
    }

    protected override SalesPriceGraph<PMQuoteMaint, PMQuote>.DetailMapping GetDetailMapping()
    {
      return new SalesPriceGraph<PMQuoteMaint, PMQuote>.DetailMapping(typeof (CROpportunityProducts))
      {
        CuryLineAmount = typeof (CROpportunityProducts.curyAmount),
        Descr = typeof (CROpportunityProducts.descr)
      };
    }

    protected override SalesPriceGraph<PMQuoteMaint, PMQuote>.PriceClassSourceMapping GetPriceClassSourceMapping()
    {
      return new SalesPriceGraph<PMQuoteMaint, PMQuote>.PriceClassSourceMapping(typeof (PX.Objects.CR.Location))
      {
        PriceClassID = typeof (PX.Objects.CR.Location.cPriceClassID)
      };
    }
  }

  public class PMDiscount : DiscountGraph<PMQuoteMaint, PMQuote>
  {
    [PXCopyPasteHiddenView]
    [PXViewName("Discount Details")]
    public PXSelect<CROpportunityDiscountDetail, Where<CROpportunityDiscountDetail.quoteID, Equal<Current<PMQuote.quoteID>>>, OrderBy<Asc<CROpportunityDiscountDetail.lineNbr>>> DiscountDetails;
    public PXAction<PMQuote> graphRecalculateDiscountsAction;

    protected override DiscountGraph<PMQuoteMaint, PMQuote>.DocumentMapping GetDocumentMapping()
    {
      return new DiscountGraph<PMQuoteMaint, PMQuote>.DocumentMapping(typeof (PMQuote))
      {
        CuryDiscTot = typeof (PMQuote.curyLineDocDiscountTotal)
      };
    }

    protected override DiscountGraph<PMQuoteMaint, PMQuote>.DetailMapping GetDetailMapping()
    {
      return new DiscountGraph<PMQuoteMaint, PMQuote>.DetailMapping(typeof (CROpportunityProducts))
      {
        CuryLineAmount = typeof (CROpportunityProducts.curyAmount),
        Quantity = typeof (CROpportunityProducts.quantity)
      };
    }

    protected override DiscountGraph<PMQuoteMaint, PMQuote>.DiscountMapping GetDiscountMapping()
    {
      return new DiscountGraph<PMQuoteMaint, PMQuote>.DiscountMapping(typeof (CROpportunityDiscountDetail));
    }

    [PXSelector(typeof (Search<ARDiscount.discountID, Where<ARDiscount.type, NotEqual<DiscountType.LineDiscount>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouse>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndCustomer>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndCustomerPrice>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndInventory>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndInventoryPrice>>>>>>>>))]
    [PXMergeAttributes]
    public override void Discount_DiscountID_CacheAttached(PXCache sender)
    {
    }

    [PXMergeAttributes]
    [CurrencyInfo(typeof (PMQuote.curyInfoID))]
    public override void Discount_CuryInfoID_CacheAttached(PXCache sender)
    {
    }

    protected virtual void Discount_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
    {
      if (!this.Base.IsExternalTax((string) null))
        return;
      ((PXSelectBase<PMQuote>) this.Base.QuoteCurrent).Current.IsTaxValid = new bool?(false);
    }

    protected virtual void Discount_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
    {
      if (!this.Base.IsExternalTax((string) null))
        return;
      ((PXSelectBase<PMQuote>) this.Base.QuoteCurrent).Current.IsTaxValid = new bool?(false);
    }

    protected virtual void Discount_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
    {
      if (!this.Base.IsExternalTax((string) null))
        return;
      ((PXSelectBase<PMQuote>) this.Base.QuoteCurrent).Current.IsTaxValid = new bool?(false);
    }

    protected override bool AddDocumentDiscount => true;

    protected override void DefaultDiscountAccountAndSubAccount(PX.Objects.Extensions.Discount.Detail det)
    {
    }

    public override DiscountEngine.DiscountCalculationOptions DefaultCalculationOptions
    {
      get
      {
        return DiscountEngine.DiscountCalculationOptions.DisableGroupAndDocumentDiscounts | DiscountEngine.DiscountCalculationOptions.DisableAPDiscountsCalculation;
      }
    }

    [PXUIField]
    [PXButton]
    public virtual IEnumerable GraphRecalculateDiscountsAction(PXAdapter adapter)
    {
      List<PMQuote> pmQuoteList = new List<PMQuote>(adapter.Get().Cast<PXResult<PMQuote, PMProject>>().Select<PXResult<PMQuote, PMProject>, PMQuote>((Func<PXResult<PMQuote, PMProject>, PMQuote>) (r => PXResult<PMQuote, PMProject>.op_Implicit(r))));
      foreach (PMQuote pmQuote in pmQuoteList)
      {
        if (((PXSelectBase) this.recalcdiscountsfilter).View.Answer == null)
        {
          ((PXSelectBase) this.recalcdiscountsfilter).Cache.Clear();
          RecalcDiscountsParamFilter discountsParamFilter = ((PXSelectBase) this.recalcdiscountsfilter).Cache.Insert() as RecalcDiscountsParamFilter;
          discountsParamFilter.RecalcUnitPrices = new bool?(false);
          discountsParamFilter.RecalcDiscounts = new bool?(false);
          discountsParamFilter.OverrideManualPrices = new bool?(false);
          discountsParamFilter.OverrideManualDiscounts = new bool?(false);
        }
        if (((PXSelectBase<RecalcDiscountsParamFilter>) this.recalcdiscountsfilter).AskExt() != 1)
          return (IEnumerable) pmQuoteList;
        this.RecalculateDiscountsAction(adapter);
      }
      return (IEnumerable) pmQuoteList;
    }

    protected override void _(PX.Data.Events.RowDeleted<PX.Objects.Extensions.Discount.Detail> e)
    {
    }
  }

  public class SalesTax : TaxGraph<PMQuoteMaint, PMQuote>
  {
    protected override PXView DocumentDetailsView => ((PXSelectBase) this.Base.Products).View;

    protected override TaxBaseGraph<PMQuoteMaint, PMQuote>.DocumentMapping GetDocumentMapping()
    {
      return new TaxBaseGraph<PMQuoteMaint, PMQuote>.DocumentMapping(typeof (PMQuote))
      {
        DocumentDate = typeof (PMQuote.documentDate),
        CuryDocBal = typeof (PMQuote.curyProductsAmount),
        CuryDiscountLineTotal = typeof (PMQuote.curyLineDiscountTotal),
        CuryDiscTot = typeof (PMQuote.curyLineDocDiscountTotal),
        TaxCalcMode = typeof (PMQuote.taxCalcMode)
      };
    }

    protected override TaxBaseGraph<PMQuoteMaint, PMQuote>.DetailMapping GetDetailMapping()
    {
      return new TaxBaseGraph<PMQuoteMaint, PMQuote>.DetailMapping(typeof (CROpportunityProducts))
      {
        CuryTranAmt = typeof (CROpportunityProducts.curyAmount),
        CuryTranDiscount = typeof (CROpportunityProducts.curyDiscAmt),
        CuryTranExtPrice = typeof (CROpportunityProducts.curyExtPrice),
        DocumentDiscountRate = typeof (CROpportunityProducts.documentDiscountRate),
        GroupDiscountRate = typeof (CROpportunityProducts.groupDiscountRate)
      };
    }

    protected override TaxBaseGraph<PMQuoteMaint, PMQuote>.TaxDetailMapping GetTaxDetailMapping()
    {
      return new TaxBaseGraph<PMQuoteMaint, PMQuote>.TaxDetailMapping(typeof (CROpportunityTax), typeof (CROpportunityTax.taxID));
    }

    protected override TaxBaseGraph<PMQuoteMaint, PMQuote>.TaxTotalMapping GetTaxTotalMapping()
    {
      return new TaxBaseGraph<PMQuoteMaint, PMQuote>.TaxTotalMapping(typeof (CRTaxTran), typeof (CRTaxTran.taxID));
    }

    protected virtual void _(PX.Data.Events.FieldUpdated<PMQuote, PMQuote.curyAmount> e)
    {
      if (e.Row == null || !e.Row.ManualTotalEntry.GetValueOrDefault())
        return;
      PMQuote row = e.Row;
      Decimal? curyAmount = e.Row.CuryAmount;
      Decimal? curyDiscTot = e.Row.CuryDiscTot;
      Decimal? nullable = curyAmount.HasValue & curyDiscTot.HasValue ? new Decimal?(curyAmount.GetValueOrDefault() - curyDiscTot.GetValueOrDefault()) : new Decimal?();
      row.CuryProductsAmount = nullable;
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<PMQuote, PMQuote.curyDiscTot> e)
    {
      if (e.Row == null || !e.Row.ManualTotalEntry.GetValueOrDefault())
        return;
      PMQuote row = e.Row;
      Decimal? curyAmount = e.Row.CuryAmount;
      Decimal? curyDiscTot = e.Row.CuryDiscTot;
      Decimal? nullable = curyAmount.HasValue & curyDiscTot.HasValue ? new Decimal?(curyAmount.GetValueOrDefault() - curyDiscTot.GetValueOrDefault()) : new Decimal?();
      row.CuryProductsAmount = nullable;
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<PMQuote, PMQuote.manualTotalEntry> e)
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
      PMQuote main = (PMQuote) ((PXSelectBase) this.Documents).Cache.GetMain<PX.Objects.Extensions.SalesTax.Document>(((PXSelectBase<PX.Objects.Extensions.SalesTax.Document>) this.Documents).Current);
      bool valueOrDefault1 = main.ManualTotalEntry.GetValueOrDefault();
      Decimal valueOrDefault2 = main.CuryAmount.GetValueOrDefault();
      Decimal valueOrDefault3 = main.CuryDiscTot.GetValueOrDefault();
      Decimal num1 = (Decimal) (this.ParentGetValue<PX.Objects.CR.CROpportunity.curyLineTotal>() ?? (object) 0M);
      Decimal num2 = (Decimal) (this.ParentGetValue<PX.Objects.CR.CROpportunity.curyLineDiscountTotal>() ?? (object) 0M);
      Decimal num3 = (Decimal) (this.ParentGetValue<PX.Objects.CR.CROpportunity.curyExtPriceTotal>() ?? (object) 0M);
      Decimal num4 = (Decimal) (this.ParentGetValue<PX.Objects.CR.CROpportunity.curyLineDocDiscountTotal>() ?? (object) 0M);
      this.ParentSetValue<PMQuote.curyTaxInclTotal>((object) CuryInclTaxTotal);
      Decimal objA = valueOrDefault1 ? valueOrDefault2 - valueOrDefault3 : num1 - num4 + CuryTaxTotal - CuryInclTaxTotal;
      if (object.Equals((object) objA, (object) (Decimal) (this.ParentGetValue<PMQuote.curyProductsAmount>() ?? (object) 0M)))
        return;
      this.ParentSetValue<PMQuote.curyProductsAmount>((object) objA);
    }

    protected override string GetExtCostLabel(PXCache sender, object row)
    {
      return ((PXFieldState) sender.GetValueExt<CROpportunityProducts.curyExtPrice>(row)).DisplayName;
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
        (object) ((PXSelectBase<PMQuote>) ((PMQuoteMaint) graph).Quote).Current
      };
      foreach (PXResult<PX.Objects.TX.Tax, TaxRev> pxResult in PXSelectBase<PX.Objects.TX.Tax, PXSelectReadonly2<PX.Objects.TX.Tax, LeftJoin<TaxRev, On<TaxRev.taxID, Equal<PX.Objects.TX.Tax.taxID>, And<TaxRev.outdated, Equal<boolFalse>, And<TaxRev.taxType, Equal<TaxType.sales>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.withholding>, And<PX.Objects.TX.Tax.taxType, NotEqual<CSTaxType.use>, And<PX.Objects.TX.Tax.reverseTax, Equal<boolFalse>, And<Current<PMQuote.documentDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>>>>>, Where>.Config>.SelectMultiBound(graph, objArray, parameters))
        dictionary[PXResult<PX.Objects.TX.Tax, TaxRev>.op_Implicit(pxResult).TaxID] = pxResult;
      List<object> objectList = new List<object>();
      switch (taxchk)
      {
        case PXTaxCheck.Line:
          foreach (PXResult<CROpportunityTax> pxResult1 in PXSelectBase<CROpportunityTax, PXSelect<CROpportunityTax, Where<CROpportunityTax.quoteID, Equal<Current<PMQuote.quoteID>>, And<CROpportunityTax.quoteID, Equal<Current<CROpportunityProducts.quoteID>>, And<CROpportunityTax.lineNbr, Equal<Current<CROpportunityProducts.lineNbr>>>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
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
          foreach (PXResult<CROpportunityTax> pxResult3 in PXSelectBase<CROpportunityTax, PXSelect<CROpportunityTax, Where<CROpportunityTax.quoteID, Equal<Current<PMQuote.quoteID>>, And<CROpportunityTax.lineNbr, Less<intMax>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
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
          foreach (PXResult<CRTaxTran> pxResult5 in PXSelectBase<CRTaxTran, PXSelect<CRTaxTran, Where<CRTaxTran.quoteID, Equal<Current<PMQuote.quoteID>>>, OrderBy<Asc<CRTaxTran.lineNbr, Asc<CRTaxTran.taxID>>>>.Config>.SelectMultiBound(graph, objArray, Array.Empty<object>()))
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
      return GraphHelper.RowCast<CROpportunityProducts>((IEnumerable) PXSelectBase<CROpportunityProducts, PXViewOf<CROpportunityProducts>.BasedOn<SelectFromBase<CROpportunityProducts, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CROpportunityProducts.quoteID, IBqlGuid>.IsEqual<BqlField<PMQuote.quoteID, IBqlGuid>.FromCurrent>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).Select<CROpportunityProducts, object>((Func<CROpportunityProducts, object>) (_ => (object) _)).ToList<object>();
    }

    protected virtual void CRTaxTran_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
    {
      if (e.Row == null)
        return;
      PXUIFieldAttribute.SetEnabled<CRTaxTran.taxID>(sender, e.Row, sender.GetStatus(e.Row) == 2);
    }

    protected virtual void CROpportunityTax_RowPersisting(
      PXCache sender,
      PXRowPersistingEventArgs e)
    {
      CROpportunityTax row = e.Row as CROpportunityTax;
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

    /// <summary>
    /// A per-unit tax graph extension for which will forbid edit of per-unit taxes in UI.
    /// </summary>
    public class PerUnitTaxDisableExt : PerUnitTaxDataEntryGraphExtension<PMQuoteMaint, CRTaxTran>
    {
      public static bool IsActive()
      {
        return PerUnitTaxDataEntryGraphExtension<PMQuoteMaint, CRTaxTran>.IsActiveBase();
      }

      protected override void _(PX.Data.Events.RowSelected<CRTaxTran> e)
      {
        if (e.Row == null || !(this.GetTax(e.Row)?.TaxType == "Q"))
          return;
        PXUIFieldAttribute.SetEnabled(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CRTaxTran>>) e).Cache, (object) e.Row, false);
      }
    }
  }

  public class ContactAddress : CROpportunityContactAddressExt<PMQuoteMaint>
  {
    public virtual void Initialize()
    {
      ((PXGraphExtension) this).Initialize();
      this.Addresses = new PXSelectExtension<DocumentAddress>((PXSelectBase) this.Base.Quote_Address);
      this.Contacts = new PXSelectExtension<DocumentContact>((PXSelectBase) this.Base.Quote_Contact);
    }

    protected override CROpportunityContactAddressExt<PMQuoteMaint>.DocumentMapping GetDocumentMapping()
    {
      return new CROpportunityContactAddressExt<PMQuoteMaint>.DocumentMapping(typeof (PMQuote))
      {
        DocumentAddressID = typeof (PMQuote.opportunityAddressID),
        DocumentContactID = typeof (PMQuote.opportunityContactID),
        ShipAddressID = typeof (PMQuote.shipAddressID),
        ShipContactID = typeof (PMQuote.shipContactID),
        AllowOverrideContactAddress = typeof (PMQuote.allowOverrideContactAddress),
        AllowOverrideShippingContactAddress = typeof (PMQuote.allowOverrideShippingContactAddress)
      };
    }

    protected override CROpportunityContactAddressExt<PMQuoteMaint>.DocumentContactMapping GetDocumentContactMapping()
    {
      return new CROpportunityContactAddressExt<PMQuoteMaint>.DocumentContactMapping(typeof (CRContact))
      {
        EMail = typeof (CRContact.email)
      };
    }

    protected override CROpportunityContactAddressExt<PMQuoteMaint>.DocumentAddressMapping GetDocumentAddressMapping()
    {
      return new CROpportunityContactAddressExt<PMQuoteMaint>.DocumentAddressMapping(typeof (CRAddress));
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

    protected override IPersonalContact SelectContact()
    {
      return (IPersonalContact) ((PXSelectBase<CRContact>) this.Base.Quote_Contact).SelectSingle(Array.Empty<object>());
    }

    protected override IPersonalContact SelectShippingContact()
    {
      return (IPersonalContact) ((PXSelectBase<CRShippingContact>) this.Base.Shipping_Contact).SelectSingle(Array.Empty<object>());
    }

    protected override IPersonalContact GetEtalonContact()
    {
      return this.SafeGetEtalon(((PXSelectBase) this.Base.Quote_Contact).Cache) as IPersonalContact;
    }

    protected override IPersonalContact GetEtalonShippingContact()
    {
      return this.SafeGetEtalon(((PXSelectBase) this.Base.Shipping_Contact).Cache) as IPersonalContact;
    }

    protected override IAddress SelectAddress()
    {
      return (IAddress) ((PXSelectBase<CRAddress>) this.Base.Quote_Address).SelectSingle(Array.Empty<object>());
    }

    protected override IAddress SelectShippingAddress()
    {
      return (IAddress) ((PXSelectBase<CRShippingAddress>) this.Base.Shipping_Address).SelectSingle(Array.Empty<object>());
    }

    protected override IAddress GetEtalonAddress()
    {
      return this.SafeGetEtalon(((PXSelectBase) this.Base.Quote_Address).Cache) as IAddress;
    }

    protected override IAddress GetEtalonShippingAddress()
    {
      return this.SafeGetEtalon(((PXSelectBase) this.Base.Shipping_Address).Cache) as IAddress;
    }

    protected override string GetMessageForDefaultRecords(
      bool needAskFromContactAddress,
      bool needAskFromBillingContactAddress,
      bool needAskFromShippingContactAddress)
    {
      if (needAskFromContactAddress)
        return needAskFromShippingContactAddress ? "Would you like the overridden settings on the Billing Info and Shipping Info tabs to be replaced with the settings from the newly selected entity?" : "Would you like the overridden settings on the Billing Info tab to be replaced with the settings from the newly selected entity?";
      if (needAskFromShippingContactAddress)
        return "Would you like the overridden settings on the Shipping Info tab to be replaced with the settings from the newly selected entity?";
      throw new NotSupportedException();
    }

    protected override void _(
      PX.Data.Events.FieldUpdated<PX.Objects.CR.Extensions.CROpportunityContactAddress.Document, PX.Objects.CR.Extensions.CROpportunityContactAddress.Document.contactID> e)
    {
      if (e.Row == null)
      {
        base._(e);
      }
      else
      {
        if (PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<PMQuote.bAccountID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, Array.Empty<object>())) != null)
        {
          ((PXSelectBase) this.Base.Quote).Cache.SetDefaultExt<PMQuote.locationID>(e.Row.Base);
        }
        else
        {
          object obj1;
          ((PXSelectBase) this.Base.QuoteCurrent).Cache.RaiseFieldDefaulting<PMQuote.locationID>(e.Row.Base, ref obj1);
          object obj2;
          ((PXSelectBase) this.Base.QuoteCurrent).Cache.RaiseFieldDefaulting<PMQuote.taxZoneID>(e.Row.Base, ref obj2);
        }
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.CR.Extensions.CROpportunityContactAddress.Document, PX.Objects.CR.Extensions.CROpportunityContactAddress.Document.contactID>>) e).Cache.SetDefaultExt<PMQuote.locationID>((object) e.Row);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.CR.Extensions.CROpportunityContactAddress.Document, PX.Objects.CR.Extensions.CROpportunityContactAddress.Document.contactID>>) e).Cache.SetDefaultExt<PMQuote.taxZoneID>((object) e.Row);
        base._(e);
      }
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
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.CR.Extensions.CROpportunityContactAddress.Document, PX.Objects.CR.Extensions.CROpportunityContactAddress.Document.bAccountID>>) e).Cache.SetDefaultExt<PMQuote.locationID>((object) e.Row);
        ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PX.Objects.CR.Extensions.CROpportunityContactAddress.Document, PX.Objects.CR.Extensions.CROpportunityContactAddress.Document.bAccountID>>) e).Cache.SetDefaultExt<PMQuote.taxZoneID>((object) e.Row);
        base._(e);
      }
    }
  }

  public class PMQuoteMaint_ActivityDetailsExt_Actions : 
    ActivityDetailsExt_Actions<PMQuoteMaint.PMQuoteMaint_ActivityDetailsExt, PMQuoteMaint, PMQuote, PMQuote.noteID>
  {
  }

  public class PMQuoteMaint_ActivityDetailsExt : 
    ActivityDetailsExt<PMQuoteMaint, PMQuote, PMQuote.noteID>
  {
    public override System.Type GetBAccountIDCommand() => typeof (PMQuote.bAccountID);

    public override System.Type GetContactIDCommand() => typeof (PMQuote.contactID);

    public override System.Type GetEmailMessageTarget()
    {
      return typeof (Select<CRContact, Where<CRContact.contactID, Equal<Current<PMQuote.opportunityContactID>>>>);
    }

    public override string GetCustomMailTo()
    {
      PMQuote current = ((PXSelectBase<PMQuote>) this.Base.Quote).Current;
      if (current == null)
        return (string) null;
      CRContact crContact = current.OpportunityContactID.With<int?, CRContact>((Func<int?, CRContact>) (_ => CRContact.PK.Find((PXGraph) this.Base, new int?(_.Value))));
      return string.IsNullOrWhiteSpace(crContact?.Email) ? (string) null : PXDBEmailAttribute.FormatAddressesWithSingleDisplayName(crContact.Email, crContact.DisplayName);
    }

    protected virtual void _(PX.Data.Events.RowPersisted<PMQuote> e)
    {
      if (e.Row == null || e.TranStatus != null || e.Operation != 3)
        return;
      Guid? noteId = e.Row.NoteID;
      if (!noteId.HasValue)
        return;
      Guid valueOrDefault = noteId.GetValueOrDefault();
      foreach (PXResult<CRActivity> pxResult in PXSelectBase<CRActivity, PXViewOf<CRActivity>.BasedOn<SelectFromBase<CRActivity, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CRActivity.refNoteID, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) valueOrDefault
      }))
      {
        CRActivity crActivity = PXResult<CRActivity>.op_Implicit(pxResult);
        crActivity.RefNoteIDType = (string) null;
        crActivity.RefNoteID = new Guid?();
        ((PXGraph) this.Base).Caches[typeof (CRActivity)].Update((object) crActivity);
      }
      ((PXGraph) this.Base).Caches[typeof (CRActivity)].Persist((PXDBOperation) 1);
    }
  }

  public class ExtensionSorting : Module
  {
    private static readonly Dictionary<System.Type, int> _order = new Dictionary<System.Type, int>()
    {
      {
        typeof (PMQuoteMaint.ContactAddress),
        1
      },
      {
        typeof (PMQuoteMaint.MultiCurrency),
        2
      },
      {
        typeof (PMQuoteMaint.SalesPrice),
        3
      },
      {
        typeof (PMQuoteMaint.PMDiscount),
        4
      },
      {
        typeof (PMQuoteMaint.SalesTax),
        5
      }
    };

    protected virtual void Load(ContainerBuilder builder)
    {
      ApplicationStartActivation.RunOnApplicationStart(builder, (System.Action) (() => PXBuildManager.SortExtensions += (Action<List<System.Type>>) (list => PXBuildManager.PartialSort(list, PMQuoteMaint.ExtensionSorting._order))), (string) null);
    }
  }

  /// <exclude />
  public class PMQuoteMaintAddressLookupExtension : 
    AddressLookupExtension<PMQuoteMaint, PMQuote, CRAddress>
  {
    protected override string AddressView => "Quote_Address";

    protected override string ViewOnMap => "viewMainOnMap";
  }

  /// <exclude />
  public class PMQuoteMaintShippingAddressLookupExtension : 
    AddressLookupExtension<PMQuoteMaint, PMQuote, CRShippingAddress>
  {
    protected override string AddressView => "Shipping_Address";

    protected override string ViewOnMap => "viewShippingOnMap";
  }
}
