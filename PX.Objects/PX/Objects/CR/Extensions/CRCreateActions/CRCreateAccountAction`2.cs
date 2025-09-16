// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateActions.CRCreateAccountAction`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR.BusinessAccountMaint_Extensions;
using PX.Objects.CS;
using PX.Objects.GDPR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CR.Extensions.CRCreateActions;

/// <exclude />
public abstract class CRCreateAccountAction<TGraph, TMain> : 
  CRCreateActionBase<
  #nullable disable
  TGraph, TMain, BusinessAccountMaint, BAccount, AccountsFilter, AccountConversionOptions>
  where TGraph : PXGraph, new()
  where TMain : class, IBqlTable, new()
{
  public FbqlSelect<SelectFromBase<BAccount, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  BAccount.bAccountID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  Document.bAccountID, IBqlInt>.FromCurrent.NoDefault>>, 
  #nullable disable
  BAccount>.View ExistingAccount;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public CRValidationFilter<AccountsFilter> AccountInfo;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public CRValidationFilter<PopupAttributes> AccountInfoAttributes;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public CRValidationFilter<PopupUDFAttributes> AccountInfoUDF;
  public PXAction<TMain> CreateBAccount;
  public PXAction<TMain> CreateBAccountRedirect;

  protected override ICRValidationFilter[] AdditionalFilters
  {
    get
    {
      return new ICRValidationFilter[2]
      {
        (ICRValidationFilter) this.AccountInfoAttributes,
        (ICRValidationFilter) this.AccountInfoUDF
      };
    }
  }

  protected override CRValidationFilter<AccountsFilter> FilterInfo => this.AccountInfo;

  protected virtual IEnumerable accountInfoAttributes() => (IEnumerable) this.GetFilledAttributes();

  protected virtual IEnumerable<PopupUDFAttributes> accountInfoUDF() => this.GetRequiredUDFFields();

  protected override IEnumerable<CSAnswers> GetAttributesForMasterEntity()
  {
    BAccount baccount = ((PXSelectBase<BAccount>) this.ExistingAccount).SelectSingle(Array.Empty<object>());
    if (baccount == null)
      return base.GetAttributesForMasterEntity();
    return PXSelectBase<CSAnswers, PXSelect<CSAnswers, Where<CSAnswers.refNoteID, Equal<Required<BAccount.noteID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
    {
      (object) baccount.NoteID
    }).FirstTableItems;
  }

  protected override object GetMasterEntity()
  {
    return (object) ((PXSelectBase<BAccount>) this.ExistingAccount).SelectSingle(Array.Empty<object>());
  }

  protected virtual void _(
    Events.FieldDefaulting<AccountsFilter, AccountsFilter.bAccountID> e)
  {
    BAccount baccount = ((PXSelectBase<BAccount>) this.ExistingAccount).SelectSingle(Array.Empty<object>());
    if (baccount != null && baccount.AcctCD != null)
    {
      ((Events.FieldDefaultingBase<Events.FieldDefaulting<AccountsFilter, AccountsFilter.bAccountID>, AccountsFilter, object>) e).NewValue = (object) baccount.AcctCD;
    }
    else
    {
      if (!this.IsDimensionAutonumbered((PXGraph) this.Base, "BIZACCT"))
        return;
      ((Events.FieldDefaultingBase<Events.FieldDefaulting<AccountsFilter, AccountsFilter.bAccountID>, AccountsFilter, object>) e).NewValue = (object) this.GetDimensionAutonumberingNewValue((PXGraph) this.Base, "BIZACCT");
    }
  }

  protected virtual void _(
    Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountName> e)
  {
    string acctName = ((PXSelectBase<BAccount>) this.ExistingAccount).SelectSingle(Array.Empty<object>())?.AcctName;
    if (acctName != null)
    {
      ((Events.FieldDefaultingBase<Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountName>, AccountsFilter, object>) e).NewValue = (object) acctName;
    }
    else
    {
      DocumentContact documentContact = ((PXSelectBase<DocumentContact>) this.Contacts).SelectSingle(Array.Empty<object>());
      ((Events.FieldDefaultingBase<Events.FieldDefaulting<AccountsFilter, AccountsFilter.accountName>, AccountsFilter, object>) e).NewValue = (object) documentContact?.FullName;
    }
  }

  protected virtual void _(
    Events.FieldVerifying<AccountsFilter, AccountsFilter.bAccountID> e)
  {
    if (((PXSelectBase<BAccount>) this.ExistingAccount).SelectSingle(Array.Empty<object>()) != null)
      return;
    if (PXResultset<BAccount>.op_Implicit(PXSelectBase<BAccount, PXSelect<BAccount, Where<BAccount.acctCD, Equal<Required<BAccount.acctCD>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
    {
      ((Events.FieldVerifyingBase<Events.FieldVerifying<AccountsFilter, AccountsFilter.bAccountID>, AccountsFilter, object>) e).NewValue
    })) != null)
      ((PXSelectBase) this.AccountInfo).Cache.RaiseExceptionHandling<AccountsFilter.bAccountID>((object) e.Row, ((Events.FieldVerifyingBase<Events.FieldVerifying<AccountsFilter, AccountsFilter.bAccountID>, AccountsFilter, object>) e).NewValue, (Exception) new PXSetPropertyException("Business Account '{0}' already exists.", new object[1]
      {
        ((Events.FieldVerifyingBase<Events.FieldVerifying<AccountsFilter, AccountsFilter.bAccountID>, AccountsFilter, object>) e).NewValue
      }));
    else
      ((PXSelectBase) this.AccountInfo).Cache.RaiseExceptionHandling<AccountsFilter.bAccountID>((object) e.Row, ((Events.FieldVerifyingBase<Events.FieldVerifying<AccountsFilter, AccountsFilter.bAccountID>, AccountsFilter, object>) e).NewValue, (Exception) null);
  }

  protected virtual void _(
    Events.FieldUpdated<AccountsFilter, AccountsFilter.accountClass> e)
  {
    ((PXCache) GraphHelper.Caches<PopupAttributes>((PXGraph) this.Base)).Clear();
  }

  protected virtual void _(Events.RowSelected<AccountsFilter> e)
  {
    if (e.Row != null)
      e.Row.NeedToUse = new bool?(this.NeedToUse);
    BAccount existing = ((PXSelectBase<BAccount>) this.ExistingAccount).SelectSingle(Array.Empty<object>());
    PXCacheEx.AdjustUI(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<AccountsFilter>>) e).Cache, (object) e.Row).ForAllFields((Action<PXUIFieldAttribute>) (_ => _.Enabled = existing == null)).For<AccountsFilter.bAccountID>((Action<PXUIFieldAttribute>) (_ => _.Enabled = existing == null && !this.IsDimensionAutonumbered((PXGraph) this.Base, "BIZACCT")));
  }

  protected virtual void _(Events.RowSelected<Document> e)
  {
    BAccount baccount = ((PXSelectBase<BAccount>) this.ExistingAccount).SelectSingle(Array.Empty<object>());
    ((PXAction) this.CreateBAccount).SetEnabled(baccount == null);
    ((PXAction) this.CreateBAccount).SetVisible(PXAccess.FeatureInstalled<FeaturesSet.customerModule>());
    if (baccount == null)
      return;
    ((PXSelectBase) this.AccountInfoAttributes).AllowUpdate = ((PXSelectBase) this.AccountInfoUDF).AllowUpdate = false;
  }

  protected virtual bool IsDimensionAutonumbered(PXGraph graph, string dimension)
  {
    return GraphHelper.RowCast<Segment>((IEnumerable) PXSelectBase<Segment, PXSelect<Segment, Where<Segment.dimensionID, Equal<Required<Segment.dimensionID>>>>.Config>.Select(graph, new object[1]
    {
      (object) dimension
    })).All<Segment>((Func<Segment, bool>) (segment => segment.AutoNumber.GetValueOrDefault()));
  }

  protected virtual string GetDimensionAutonumberingNewValue(PXGraph graph, string dimension)
  {
    return PXResult<Dimension, Numbering>.op_Implicit((PXResult<Dimension, Numbering>) PXResultset<Dimension>.op_Implicit(PXSelectBase<Dimension, PXSelectJoin<Dimension, LeftJoin<Numbering, On<Dimension.numberingID, Equal<Numbering.numberingID>>>, Where<Dimension.dimensionID, Equal<Required<Dimension.dimensionID>>, And<Numbering.userNumbering, NotEqual<True>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
    {
      (object) dimension
    })))?.NewSymbol ?? "<NEW>";
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable createBAccount(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CRCreateAccountAction<TGraph, TMain>.\u003C\u003Ec__DisplayClass21_0 cDisplayClass210 = new CRCreateAccountAction<TGraph, TMain>.\u003C\u003Ec__DisplayClass21_0();
    // ISSUE: reference to a compiler-generated field
    if (this.AskExtConvert(out cDisplayClass210.redirect))
    {
      if (this.Base.IsDirty)
        this.Base.Actions.PressSave();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass210.processingGraph = this.Base.CloneGraphState<TGraph>();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass210, __methodptr(\u003CcreateBAccount\u003Eb__0)));
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable createBAccountRedirect(PXAdapter adapter)
  {
    BusinessAccountMaint targetGraph = this.CreateTargetGraph();
    BAccount master = this.CreateMaster(targetGraph, (AccountConversionOptions) null);
    ConversionResult<BAccount> result = new ConversionResult<BAccount>();
    result.Graph = (PXGraph) targetGraph;
    result.Entity = master;
    result.Converted = false;
    this.Redirect(result);
    return adapter.Get();
  }

  public override ConversionResult<BAccount> Convert(AccountConversionOptions options = null)
  {
    BAccount baccount = ((PXSelectBase<BAccount>) this.ExistingAccount).SelectSingle(Array.Empty<object>());
    if (baccount == null)
      return base.Convert(options);
    ConversionResult<BAccount> conversionResult = new ConversionResult<BAccount>();
    conversionResult.Converted = false;
    conversionResult.Entity = baccount;
    return conversionResult;
  }

  protected override BAccount CreateMaster(BusinessAccountMaint graph, AccountConversionOptions _)
  {
    AccountsFilter current1 = ((PXSelectBase<AccountsFilter>) this.AccountInfo).Current;
    Document current2 = ((PXSelectBase<Document>) this.Documents).Current;
    DocumentContact documentContact = ((PXSelectBase<DocumentContact>) this.Contacts).SelectSingle(Array.Empty<object>());
    DocumentAddress docAddress = ((PXSelectBase<DocumentAddress>) this.Addresses).SelectSingle(Array.Empty<object>());
    object baccountId = (object) current1.BAccountID;
    ((PXSelectBase) graph.BAccount).Cache.RaiseFieldUpdating<BAccount.acctCD>((object) null, ref baccountId);
    BAccount baccount1 = ((PXSelectBase<BAccount>) graph.BAccount).Insert(new BAccount()
    {
      AcctCD = (string) baccountId,
      AcctName = current1.AccountName,
      Type = "PR",
      ParentBAccountID = current2.ParentBAccountID,
      CampaignSourceID = current2.CampaignID,
      OverrideSalesTerritory = current2.OverrideSalesTerritory
    });
    baccount1.ClassID = current1.AccountClass;
    bool? nullable = baccount1.OverrideSalesTerritory;
    if (nullable.HasValue && nullable.GetValueOrDefault())
      baccount1.SalesTerritoryID = current2.SalesTerritoryID;
    baccount1.LocaleName = current2.LanguageID;
    if (PXResultset<CRCustomerClass>.op_Implicit(PXSelectBase<CRCustomerClass, PXSelect<CRCustomerClass, Where<CRCustomerClass.cRCustomerClassID, Equal<Required<CRCustomerClass.cRCustomerClassID>>>>.Config>.SelectSingleBound((PXGraph) graph, (object[]) null, new object[1]
    {
      (object) baccount1.ClassID
    }))?.DefaultOwner == "S")
    {
      baccount1.WorkgroupID = current2.WorkgroupID;
      baccount1.OwnerID = current2.OwnerID;
    }
    BAccount baccount2 = ((PXSelectBase<BAccount>) graph.BAccount).Update(baccount1);
    BusinessAccountMaint.DefContactAddressExt extension1 = ((PXGraph) graph).GetExtension<BusinessAccountMaint.DefContactAddressExt>();
    nullable = current1.LinkContactToAccount;
    if (nullable.GetValueOrDefault())
    {
      Contact src = PXResultset<Contact>.op_Implicit(PXSelectBase<Contact, PXSelect<Contact, Where<Contact.contactID, Equal<Required<PX.Objects.CR.CROpportunity.contactID>>>>.Config>.Select((PXGraph) graph, new object[1]
      {
        (object) current2.RefContactID
      }));
      if (src != null)
      {
        graph.Answers.CopyAttributes((object) baccount2, (object) src);
        src.BAccountID = baccount2.BAccountID;
        ((PXSelectBase<Contact>) extension1.DefContact).Update(src);
      }
    }
    Contact contact = ((PXSelectBase<Contact>) extension1.DefContact).SelectSingle(Array.Empty<object>()) ?? throw new InvalidOperationException("Cannot get Contact for Business Account.");
    this.MapContact(documentContact, baccount2, ref contact);
    this.MapConsentable(documentContact, (IConsentable) contact);
    ((PXSelectBase<Contact>) extension1.DefContact).Update(contact);
    PX.Objects.CR.Address address1 = ((PXSelectBase<PX.Objects.CR.Address>) extension1.DefAddress).SelectSingle(Array.Empty<object>()) ?? throw new InvalidOperationException("Cannot get Address for Business Account.");
    this.MapAddress(docAddress, baccount2, ref address1);
    PX.Objects.CR.Address address2 = ((PXSelectBase<PX.Objects.CR.Address>) extension1.DefAddress).Update(address1);
    BusinessAccountMaint.DefLocationExt extension2 = ((PXGraph) graph).GetExtension<BusinessAccountMaint.DefLocationExt>();
    PX.Objects.CR.Standalone.Location location = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension2.DefLocation).Select(Array.Empty<object>()));
    location.DefAddressID = address2.AddressID;
    location.CTaxZoneID = current2.TaxZoneID;
    ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension2.DefLocation).Update(location);
    BAccount master = ((PXSelectBase<BAccount>) graph.BAccount).Update(baccount2);
    this.ReverseDocumentUpdate(graph, master);
    this.FillRelations((PXGraph) graph, master);
    this.FillAttributes(graph.Answers, master);
    this.FillUDF(((PXSelectBase) this.AccountInfoUDF).Cache, (object) this.GetMain(current2), ((PXSelectBase) graph.BAccount).Cache, master, master.ClassID);
    this.TransferActivities(graph, master);
    this.FillNotesAndAttachments((PXGraph) graph, ((PXSelectBase) this.Documents).Cache.GetMain<Document>(current2), ((PXSelectBase) graph.CurrentBAccount).Cache, master);
    return master;
  }

  protected override void ReverseDocumentUpdate(BusinessAccountMaint graph, BAccount entity)
  {
    Document current = ((PXSelectBase<Document>) this.Documents).Current;
    ((PXSelectBase) this.Documents).Cache.SetValue<Document.bAccountID>((object) current, (object) entity.BAccountID);
    ((PXSelectBase) this.Documents).Cache.SetValue<Document.locationID>((object) current, (object) entity.DefLocationID);
    GraphHelper.Caches<TMain>((PXGraph) graph).Update(this.GetMain(current));
  }

  protected virtual void MapContact(
    DocumentContact docContact,
    BAccount account,
    ref Contact contact)
  {
    this.MapContact(docContact, (IPersonalContact) contact);
    contact.Title = (string) null;
    contact.FirstName = (string) null;
    contact.LastName = (string) null;
    contact.ContactType = "AP";
    contact.FullName = account.AcctName;
    contact.ContactID = account.DefContactID;
    contact.BAccountID = account.BAccountID;
  }

  protected virtual void MapAddress(
    DocumentAddress docAddress,
    BAccount account,
    ref PX.Objects.CR.Address address)
  {
    this.MapAddress(docAddress, (IAddressBase) address);
  }

  protected virtual void TransferActivities(BusinessAccountMaint graph, BAccount account)
  {
    foreach (PXResult<CRPMTimeActivity> pxResult in this.Activities.Select(Array.Empty<object>()))
    {
      CRPMTimeActivity crpmTimeActivity = PXResult<CRPMTimeActivity>.op_Implicit(pxResult);
      crpmTimeActivity.BAccountID = account.BAccountID;
      ((PXSelectBase<CRPMTimeActivity>) ((PXGraph) graph).GetExtension<BusinessAccountMaint_ActivityDetailsExt>().Activities).Update(crpmTimeActivity);
    }
  }
}
